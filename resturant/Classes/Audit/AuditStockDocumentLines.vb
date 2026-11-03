Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Text

Friend NotInheritable Class AuditStockDocumentSnapshot
    Friend Header As DataTable
    Friend Lines As DataTable
    Friend Stock As DataTable
    Friend References As DataTable
End Class

' Closed, reviewed stock-line paths. No arbitrary SQL/procedure names enter a request.
Friend NotInheritable Class AuditStockDocumentLines
    Friend Shared Function V(r As AuditOperationRequest, name As String) As String
        Return r.Inputs.Single(Function(x) x.Name = name).Value.Text
    End Function

    Friend Shared Function Operation(command As SqlCommand) As String
        Select Case command.CommandText.Trim("["c, "]"c).ToLowerInvariant()
            Case "outsale_details_insert", "insale_details_insert", "imex_details_insert", "st_trans_details_insert" : Return "StockDocument.LineAdded"
            Case "outsale_details_delete", "insale_details_delete", "imex_details_delete", "st_trans_details_delete" : Return "StockDocument.LineDeleted"
            Case "stockreceipt.quantitystep" : Return "StockDocument.LineQuantityChanged"
            Case Else : Throw New ArgumentException("إجراء سطر مخزني غير معتمد.")
        End Select
    End Function

    Private Shared Function Procedure(kind As Integer, create As Boolean) As String
        Select Case kind
            Case 35 : Return If(create, "Outsale_Details_Insert", "Outsale_Details_Delete")
            Case 17 : Return If(create, "InSale_Details_Insert", "InSale_Details_Delete")
            Case 8 : Return If(create, "IMEX_Details_Insert", "IMEX_Details_Delete")
            Case 12 : Return If(create, "ST_Trans_Details_insert", "ST_Trans_Details_Delete")
            Case Else : Throw New ArgumentException("نوع الإذن غير معتمد.")
        End Select
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, kind As Integer)
        Dim spec = AuditContract.StockDocumentSpec(kind)
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@User AND u.is_Allow=1 AND v.[" & spec(4) & "]=1", cn, tx)
            cmd.Parameters.Add("@User", SqlDbType.Int).Value = r.Context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("لا تملك صلاحية العملية المخزنية: " & spec(4))
        End Using
    End Sub

    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, Optional old As AuditStockDocumentSnapshot = Nothing) As AuditStockDocumentSnapshot
        Dim kind = CInt(V(r, "DocumentType")), spec = AuditContract.StockDocumentSpec(kind), bill = CInt(r.EntityId), lineId = CInt(V(r, "TargetLineId"))
        Dim fields = AuditContract.StockDocumentFields("StockDocument").Select(Function(x) x.Split("|"c)(1)).Where(Function(x) x <> "AuditDocumentNumber").Select(Function(x) "[" & x & "]")
        Dim s As New AuditStockDocumentSnapshot
        s.Header = AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID AS AuditRowId,[" & spec(3) & "] AS AuditDocumentNumber," & String.Join(",", fields) & " FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@P0", bill)
        If s.Header.Rows.Count <> 1 OrElse CInt(s.Header.Rows(0)("BsType_ID")) <> kind OrElse CInt(s.Header.Rows(0)("isVoid")) <> 0 Then Throw New InvalidOperationException("الإذن غير موجود أو ملغى أو نوعه مختلف.")
        s.Lines = AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM dbo.[" & spec(0) & "] WITH(UPDLOCK,HOLDLOCK) WHERE [" & spec(1) & "]=@P0 ORDER BY T_ID", bill)
        Dim itemId As Integer, storeId As Integer, destination As Integer, unitId As Integer
        If lineId = 0 Then
            itemId = CInt(V(r, "SelectedItemId")) : unitId = CInt(V(r, "SelectedUnitId"))
            storeId = CInt(V(r, "SelectedStoreId")) : destination = If(kind = 12, CInt(V(r, "DestinationStoreId")), storeId)
        Else
            Dim source = If(old Is Nothing, s, old)
            Dim line = source.Lines.Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = lineId)
            If line Is Nothing Then Throw New InvalidOperationException("السطر لا يتبع الإذن المعروض أو حُذف.")
            itemId = CInt(line("IM_ID")) : unitId = CInt(line("U_ID"))
            storeId = CInt(line(If(kind = 12, "ST_F_ID", "ST_ID"))) : destination = If(kind = 12, CInt(line("ST_To_ID")), storeId)
        End If
        If kind = 12 AndAlso storeId = destination Then Throw New InvalidOperationException("لا يمكن التحويل إلى المخزن نفسه.")
        s.References = AuditSalesQuantity.Rows(cn, tx, "SELECT m.IM_ID,m.item_name,u.U_ID,u.U_Cargo,(SELECT COUNT(*) FROM dbo.Stores WITH(HOLDLOCK) WHERE ST_ID IN(@P2,@P3)) AS StoreCount FROM dbo.IM_Menu m WITH(HOLDLOCK) CROSS JOIN dbo.Units u WITH(HOLDLOCK) WHERE m.IM_ID=@P0 AND u.U_ID=@P1", itemId, unitId, storeId, destination)
        If s.References.Rows.Count <> 1 OrElse CInt(s.References.Rows(0)("StoreCount")) <> If(kind = 12, 2, 1) Then Throw New InvalidOperationException("الصنف أو الوحدة أو المخزن غير موجود.")
        ' Include all expiry buckets for the affected item/stores. IMEX may choose another expiry.
        s.Stock = AuditSalesQuantity.Rows(cn, tx, "SELECT IM_ID,CONVERT(bigint,ST_ID) AS ST_ID,ISNULL(D_Vaild,'') AS D_Vaild,CONVERT(float,ISNULL(SUM(QTY),0)) AS AuditStock FROM dbo.ST_Balance_V WITH(HOLDLOCK) WHERE IM_ID=@P0 AND ST_ID IN(@P1,@P2) GROUP BY IM_ID,ST_ID,ISNULL(D_Vaild,'') ORDER BY IM_ID,ST_ID,ISNULL(D_Vaild,'')", itemId, storeId, destination)
        If s.Lines.Rows.Count > 1000 OrElse s.Stock.Rows.Count > 2000 Then Throw New InvalidOperationException("تفاصيل الإذن تتجاوز سعة المراجعة الآمنة.")
        Return s
    End Function

    Friend Shared Function Fingerprint(s As AuditStockDocumentSnapshot) As String
        Dim value = String.Join("|", {AuditSalesQuantity.Fingerprint(s.Header), AuditSalesQuantity.Fingerprint(s.Lines), AuditSalesQuantity.Fingerprint(s.Stock), AuditSalesQuantity.Fingerprint(s.References)})
        Return BitConverter.ToString(AuditCanonical.Hash(Encoding.UTF8.GetBytes(value))).Replace("-", "")
    End Function

    Friend Shared Function Request(context As AuditEventContext, command As SqlCommand, billId As Integer, kind As Integer) As AuditOperationRequest
        Dim op = Operation(command), create = op = "StockDocument.LineAdded", spec = AuditContract.StockDocumentSpec(kind)
        If op <> "StockDocument.LineQuantityChanged" AndAlso Not command.CommandText.Trim("["c, "]"c).Equals(Procedure(kind, create), StringComparison.OrdinalIgnoreCase) Then Throw New ArgumentException("الإجراء لا يطابق نوع الإذن.")
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = op, .EntityType = "StockDocument", .EntityId = billId.ToString(CultureInfo.InvariantCulture)}
        If create AndAlso Convert.ToInt32(AuditFinance.Parameter(command, spec(1))) <> billId Then Throw New ArgumentException("إذن السطر لا يطابق الشاشة.")
        For Each name In AuditContract.StockDocumentInputs(op, kind)
            Dim value As Object
            Select Case name
                Case "DocumentType" : value = kind
                Case "TargetLineId" : value = If(create, 0, Convert.ToInt32(AuditFinance.Parameter(command, "T_ID")))
                Case "ExpectedStockDocumentSnapshot" : value = New String("0"c, 64)
                Case "SelectedItemId" : value = Convert.ToInt32(AuditFinance.Parameter(command, "IM_ID"))
                Case "SelectedUnitId" : value = Convert.ToInt32(AuditFinance.Parameter(command, "U_ID"))
                Case "SelectedStoreId" : value = Convert.ToInt32(AuditFinance.Parameter(command, If(kind = 12, "ST_F_ID", "ST_ID")))
                Case "DestinationStoreId" : value = Convert.ToInt32(AuditFinance.Parameter(command, "ST_To_ID"))
                Case "RequestedQuantity" : value = Convert.ToDecimal(AuditFinance.Parameter(command, If(kind = 12, "QTY", "QYT")), CultureInfo.CurrentCulture)
                Case "RequestedCost" : value = Convert.ToDecimal(AuditFinance.Parameter(command, "Price"), CultureInfo.CurrentCulture)
                Case "RequestedTotal" : value = Convert.ToDecimal(AuditFinance.Parameter(command, "Total"), CultureInfo.CurrentCulture)
                Case "RequestedSalePrice" : value = Convert.ToDouble(AuditFinance.Parameter(command, "SPRICE", 0R), CultureInfo.CurrentCulture)
                Case "RequestedBarcode" : value = Convert.ToString(AuditFinance.Parameter(command, "Barcode", ""))
                Case "RequestedExpiry"
                    value = AuditFinance.Parameter(command, "D_Vaild", DBNull.Value)
                    If Not IsDBNull(value) Then value = If(kind = 12, Convert.ToString(value), Convert.ToDateTime(value, CultureInfo.CurrentCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
                Case "OnUpdate" : value = If(Convert.ToBoolean(AuditFinance.Parameter(command, "On_Update")), 1, 0)
                Case "DisplayedQuantity" : value = Convert.ToDecimal(AuditFinance.Parameter(command, "DisplayedQuantity"))
                Case "QuantityDelta" : value = Convert.ToInt32(AuditFinance.Parameter(command, "QuantityDelta"))
                Case Else : Throw New ArgumentException("مدخل إذن غير معتمد.")
            End Select
            Dim type = AuditContract.FieldType(op, name)
            r.Inputs.Add(New AuditInput With {.Name = name, .ValueType = type, .Value = AuditValue.Capture(type, value)})
        Next
        AuditContract.Request(r) : Return r
    End Function

    Friend Shared Sub Prepare(connection As String, r As AuditOperationRequest)
        AuditContract.Request(r)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Permission(cn, tx, r, CInt(V(r, "DocumentType")))
                Dim s = Capture(cn, tx, r)
                CheckQuantity(r, s)
                r.Inputs.Single(Function(x) x.Name = "ExpectedStockDocumentSnapshot").Value = AuditValue.Capture("String", Fingerprint(s))
                tx.Commit()
            End Using
        End Using
        AuditContract.Request(r)
    End Sub

    Private Shared Sub CheckQuantity(r As AuditOperationRequest, s As AuditStockDocumentSnapshot)
        If r.OperationCode <> "StockDocument.LineQuantityChanged" Then Return
        Dim row = s.Lines.Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("T_ID")) = CInt(V(r, "TargetLineId")))
        Dim shown = Decimal.Parse(V(r, "DisplayedQuantity"), CultureInfo.InvariantCulture)
        If shown <> CDec(row("QYT")) Then Throw New InvalidOperationException("تغيرت كمية الصنف؛ حدّث الإذن أولًا.")
        If shown + CInt(V(r, "QuantityDelta")) <= 0D Then Throw New InvalidOperationException("الكمية غير موجبة؛ استخدم حذف الصنف.")
    End Sub

    Private Shared Function Allocate(cn As SqlConnection, tx As SqlTransaction, table As String) As Integer
        Dim ranges = AuditSalesQuantity.Rows(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)")
        If ranges.Rows.Count <> 1 OrElse IsDBNull(ranges.Rows(0)(0)) OrElse IsDBNull(ranges.Rows(0)(1)) Then Throw New InvalidOperationException("نطاق ترقيم الإذن غير محدد.")
        Dim first = CInt(ranges.Rows(0)(0)), last = CInt(ranges.Rows(0)(1))
        Dim maximum = AuditSalesQuantity.Rows(cn, tx, "SELECT ISNULL(MAX(T_ID),@P0) FROM dbo.[" & table & "] WITH(UPDLOCK,HOLDLOCK) WHERE T_ID BETWEEN @P0 AND @P1", first, last)
        If first < 0 OrElse last <= first OrElse CLng(maximum.Rows(0)(0)) >= last Then Throw New InvalidOperationException("نطاق ترقيم سطور الإذن ممتلئ أو غير صالح.")
        Return CInt(maximum.Rows(0)(0)) + 1
    End Function

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse Not AuditContract.IsStockDocumentLine(request.OperationCode) Then Throw New ArgumentException("يلزم عقد سطر مخزني ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, kind = CInt(V(r, "DocumentType")), spec = AuditContract.StockDocumentSpec(kind), create = r.OperationCode = "StockDocument.LineAdded", delete = r.OperationCode = "StockDocument.LineDeleted"
            Permission(cn, tx, r, kind)
            Dim before = Capture(cn, tx, r)
            If Fingerprint(before) <> V(r, "ExpectedStockDocumentSnapshot") Then Throw New InvalidOperationException("تغير الإذن أو رصيده؛ حدّث العرض قبل إعادة العملية.")
            CheckQuantity(r, before)
            Dim id = If(create, Allocate(cn, tx, spec(0)), CInt(V(r, "TargetLineId")))
            If create OrElse delete Then
                Using cmd = AuditRequestCoordinator.Command("dbo." & Procedure(kind, create), cn, tx)
                    If create Then
                        cmd.Parameters.Add("@" & spec(1), SqlDbType.Int).Value = CInt(r.EntityId)
                        cmd.Parameters.Add("@IM_ID", SqlDbType.Int).Value = CInt(V(r, "SelectedItemId"))
                        cmd.Parameters.Add("@U_ID", SqlDbType.Int).Value = CInt(V(r, "SelectedUnitId"))
                        cmd.Parameters.Add(If(kind = 12, "@ST_F_ID", "@ST_ID"), SqlDbType.Int).Value = CInt(V(r, "SelectedStoreId"))
                        If kind = 12 Then cmd.Parameters.Add("@ST_To_ID", SqlDbType.Int).Value = CInt(V(r, "DestinationStoreId"))
                        cmd.Parameters.AddWithValue(If(kind = 12, "@QTY", "@QYT"), Decimal.Parse(V(r, "RequestedQuantity"), CultureInfo.InvariantCulture))
                        cmd.Parameters.AddWithValue("@Price", Decimal.Parse(V(r, "RequestedCost"), CultureInfo.InvariantCulture))
                        If kind <> 12 Then cmd.Parameters.AddWithValue("@Total", Decimal.Parse(V(r, "RequestedTotal"), CultureInfo.InvariantCulture))
                        cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 50).Value = V(r, "RequestedBarcode")
                        cmd.Parameters.Add("@ON_Update", SqlDbType.Bit).Value = CInt(V(r, "OnUpdate")) = 1
                        Dim expiry = r.Inputs.Single(Function(x) x.Name = "RequestedExpiry").Value
                        If kind = 12 Then
                            cmd.Parameters.Add("@D_Vaild", SqlDbType.NVarChar, 50).Value = expiry.Text
                        Else
                            cmd.Parameters.Add("@D_Vaild", SqlDbType.Date).Value = If(expiry.State = AuditValueState.SqlNull, CType(DBNull.Value, Object), DateTime.ParseExact(expiry.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture))
                        End If
                        If kind = 35 Then cmd.Parameters.Add("@SPRICE", SqlDbType.Float).Value = Double.Parse(V(r, "RequestedSalePrice"), CultureInfo.InvariantCulture)
                    Else
                        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = id
                    End If
                    cmd.ExecuteNonQuery()
                End Using
            Else
                Using cmd As New SqlCommand("UPDATE dbo.Outsale_Details SET QYT=@Qty,ST_QTY=U_Cargo*@Qty,Total=Cost*@Qty WHERE T_ID=@Line AND Outsale_T_ID=@Bill; IF @@ROWCOUNT<>1 THROW 51126,'Stock receipt line changed.',1; EXEC dbo.Recalculate_Agent_Bill_Total @Bill;", cn, tx)
                    Dim p = cmd.Parameters.Add("@Qty", SqlDbType.Decimal)
                    p.Precision = 18 : p.Scale = 3 : p.Value = Decimal.Parse(V(r, "DisplayedQuantity"), CultureInfo.InvariantCulture) + CInt(V(r, "QuantityDelta"))
                    cmd.Parameters.Add("@Line", SqlDbType.Int).Value = id : cmd.Parameters.Add("@Bill", SqlDbType.Int).Value = CInt(r.EntityId)
                    cmd.ExecuteNonQuery()
                End Using
            End If
            AuditRequestCoordinator.RequireTransaction(cn, tx)
            Dim after = Capture(cn, tx, r, before)
            Dim oldLine = before.Lines.Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = id), newLine = after.Lines.Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = id)
            If If(create, oldLine IsNot Nothing OrElse newLine Is Nothing, If(delete, oldLine Is Nothing OrElse newLine IsNot Nothing, oldLine Is Nothing OrElse newLine Is Nothing)) Then Throw New InvalidOperationException("لم يؤكد الإجراء أثر السطر المطلوب.")
            Dim otherBefore = before.Lines.Copy(), otherAfter = after.Lines.Copy()
            For Each dt In {otherBefore, otherAfter}
                For Each row In dt.Rows.Cast(Of DataRow)().Where(Function(x) CInt(x("T_ID")) = id).ToArray()
                    dt.Rows.Remove(row)
                Next
            Next
            If AuditSalesQuantity.Fingerprint(otherBefore) <> AuditSalesQuantity.Fingerprint(otherAfter) Then Throw New InvalidOperationException("تغيرت سطور أخرى خارج العملية المعتمدة.")
            If create AndAlso (CInt(newLine("IM_ID")) <> CInt(V(r, "SelectedItemId")) OrElse CInt(newLine("U_ID")) <> CInt(V(r, "SelectedUnitId"))) Then Throw New InvalidOperationException("السطر المنشأ لا يطابق الصنف والوحدة.")
            Dim events As New List(Of AuditEventRequest), occurred = AuditRequestCoordinator.ServerUtcNow(cn, tx)
            Dim main = EventPart(r, after, occurred, 1)
            AddChanges(main, "StockDocument", r.EntityId, before.Header.Rows(0), after.Header.Rows(0))
            AddChanges(main, spec(2), id.ToString(CultureInfo.InvariantCulture), oldLine, newLine)
            events.Add(main)
            Dim buckets = MergeStock(before.Stock, after.Stock), part As AuditEventRequest = Nothing
            For Each pair In buckets
                If part Is Nothing OrElse part.Changes.Count + 4 > 100 Then
                    part = EventPart(r, after, occurred, events.Count + 1) : events.Add(part)
                End If
                AddChanges(part, "StockBucket", pair.Key, pair.Value(0), pair.Value(1))
            Next
            If events.Count > 100 Then Throw New InvalidOperationException("تفاصيل الإذن تتجاوز سعة طلب المراقبة.")
            For Each e In events
                AuditContract.EventData(r, e)
            Next
            For Each e In events
                AuditWriter.WriteInTransaction(cn, tx, lease, e)
            Next
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", r.EntityId}, {"LineId", id.ToString(CultureInfo.InvariantCulture)}})}
        Catch
            lease.Faulted = True : Throw
        End Try
    End Function

    Private Shared Function EventPart(r As AuditOperationRequest, s As AuditStockDocumentSnapshot, occurred As DateTime, ordinal As Integer) As AuditEventRequest
        Return New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = ordinal, .EventCode = r.OperationCode, .Outcome = 1, .EntityType = r.EntityType, .EntityId = r.EntityId,
            .DocumentNumber = Convert.ToString(s.Header.Rows(0)("AuditDocumentNumber"), CultureInfo.InvariantCulture), .BusinessDate = CDate(s.Header.Rows(0)("Date")), .OccurredAtUtc = occurred}
    End Function

    Private Shared Sub AddChanges(e As AuditEventRequest, entity As String, id As String, before As DataRow, after As DataRow)
        For Each definition In AuditContract.StockDocumentFields(entity)
            Dim p = definition.Split("|"c)
            e.Changes.Add(New AuditFieldChange With {.ChangeNo = e.Changes.Count + 1, .EntityType = entity, .EntityId = id, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = CByte(If(entity = "StockBucket", 3, 1)),
                .Before = If(before Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), before(p(1)))),
                .After = If(after Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), after(p(1))))})
        Next
    End Sub

    Private Shared Function MergeStock(before As DataTable, after As DataTable) As SortedDictionary(Of String, DataRow())
        Dim result As New SortedDictionary(Of String, DataRow())(StringComparer.Ordinal)
        For side = 0 To 1
            For Each row As DataRow In If(side = 0, before, after).Rows
                Dim key = BitConverter.ToString(AuditCanonical.Hash(Encoding.UTF8.GetBytes(CStr(row("IM_ID")) & "|" & CStr(row("ST_ID")) & "|" & CStr(row("D_Vaild"))))).Replace("-", "")
                If Not result.ContainsKey(key) Then result.Add(key, New DataRow(1) {})
                result(key)(side) = row
            Next
        Next
        For Each pair In result
            For side = 0 To 1
                If pair.Value(side) Is Nothing Then
                    Dim dt = before.Clone(), row = dt.NewRow()
                    row.ItemArray = CType(pair.Value(1 - side).ItemArray.Clone(), Object()) : row("AuditStock") = 0R
                    dt.Rows.Add(row) : pair.Value(side) = row
                End If
            Next
        Next
        Return result
    End Function

    Friend Shared Function Save(connection As String, request As AuditOperationRequest) As AuditRequestResult
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Dim result = ExecuteInTransaction(cn, tx, request)
                    tx.Commit() : Return result
                Catch
                    Try
                        If tx.Connection IsNot Nothing Then tx.Rollback()
                    Catch
                    End Try
                    Throw
                End Try
            End Using
        End Using
    End Function
End Class

