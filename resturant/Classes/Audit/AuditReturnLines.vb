Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Text

Friend NotInheritable Class AuditReturnSnapshot
    Friend Header As DataTable
    Friend Lines As DataTable
    Friend Stock As DataTable
    Friend References As DataTable
End Class

' Closed return-line paths. Add/delete preserve existing procedures; quantity targets returns only.
Friend NotInheritable Class AuditReturnLines
    ' Optional SQL values may be explicitly DBNull (for example from a DataGridView).
    ' Keep AuditFinance.Parameter strict for mandatory fields and other callers.
    Private Shared Function OptionalParameter(command As SqlCommand, name As String) As Object
        Dim p = command.Parameters.Cast(Of SqlParameter)().SingleOrDefault(Function(x) x.ParameterName.TrimStart("@"c).Equals(name, StringComparison.OrdinalIgnoreCase))
        If p Is Nothing OrElse p.Value Is Nothing OrElse IsDBNull(p.Value) Then Return DBNull.Value
        Return p.Value
    End Function

    Friend Shared Function V(r As AuditOperationRequest, name As String) As String
        Return r.Inputs.Single(Function(x) x.Name = name).Value.Text
    End Function

    Friend Shared Function Operation(command As SqlCommand) As String
        Select Case command.CommandText.Trim("["c, "]"c).ToLowerInvariant()
            Case "rtn_contents_insert" : Return "Return.LineAdded"
            Case "rtn_contents_delete_im" : Return "Return.LineDeleted"
            Case "return.quantitystep" : Return "Return.LineQuantityChanged"
            Case Else : Throw New ArgumentException("إجراء مرتجع غير معتمد.")
        End Select
    End Function

    Private Shared Function Procedure(kind As Integer, create As Boolean) As String
        AuditContract.ReturnSpec(kind)
        Return If(create, "Rtn_Contents_INSERT", "Rtn_Contents_Delete_IM")
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, kind As Integer)
        Dim spec = AuditContract.ReturnSpec(kind)
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@User AND u.is_Allow=1 AND v.[" & spec(4) & "]=1", cn, tx)
            cmd.Parameters.Add("@User", SqlDbType.Int).Value = r.Context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("لا تملك صلاحية العملية المخزنية: " & spec(4))
        End Using
    End Sub

    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, Optional old As AuditReturnSnapshot = Nothing) As AuditReturnSnapshot
        Dim kind = CInt(V(r, "DocumentType")), spec = AuditContract.ReturnSpec(kind), bill = CInt(r.EntityId), lineId = CInt(V(r, "TargetLineId"))
        Dim fields = AuditContract.ReturnFields("ReturnDocument").Select(Function(x) x.Split("|"c)(1)).Where(Function(x) x <> "AuditDocumentNumber").Select(Function(x) "[" & x & "]")
        Dim s As New AuditReturnSnapshot
        s.Header = AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID AS AuditRowId,EditOpenedAt,EditOpenedBy,[" & spec(3) & "] AS AuditDocumentNumber," & String.Join(",", fields) & " FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@P0", bill)
        If s.Header.Rows.Count <> 1 OrElse CInt(s.Header.Rows(0)("BsType_ID")) <> kind OrElse CInt(s.Header.Rows(0)("isVoid")) <> 0 Then Throw New InvalidOperationException("المرتجع غير موجود أو ملغى أو نوعه مختلف.")
        Dim header = s.Header.Rows(0)
        If Not {0, 1}.Contains(CInt(header("isDepended"))) Then Throw New InvalidOperationException("حالة اعتماد المرتجع غير معروفة.")
        If CInt(header("isDepended")) = 1 AndAlso (V(r, "OnUpdate") <> "1" OrElse IsDBNull(header("EditOpenedAt")) OrElse IsDBNull(header("EditOpenedBy")) OrElse CInt(header("EditOpenedBy")) <> r.Context.ActorUserId.Value) Then Throw New InvalidOperationException("افتح تعديل المرتجع بالمستخدم الحالي أولًا.")
        s.Lines = AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM dbo.[" & spec(0) & "] WITH(UPDLOCK,HOLDLOCK) WHERE [" & spec(1) & "]=@P0 ORDER BY T_ID", bill)
        If s.Lines.Rows.Cast(Of DataRow)().Any(Function(x) CInt(x("Rtn_Type_ID")) <> If(kind = 10, 1, 7)) Then Throw New InvalidOperationException("نوع أحد سطور المرتجع لا يطابق رأس الفاتورة.")
        Dim itemId As Integer, storeId As Integer, destination As Integer, unitId As Integer
        If lineId = 0 Then
            itemId = CInt(V(r, "SelectedItemId")) : unitId = CInt(V(r, "SelectedUnitId"))
            storeId = CInt(V(r, "SelectedStoreId")) : destination = storeId
        Else
            Dim source = If(old Is Nothing, s, old)
            Dim line = source.Lines.Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = lineId)
            If line Is Nothing Then Throw New InvalidOperationException("السطر لا يتبع المرتجع المعروض أو حُذف.")
            itemId = CInt(line("IM_ID")) : unitId = CInt(line("U_ID"))
            storeId = CInt(line("ST_ID")) : destination = storeId
        End If
        s.References = AuditSalesQuantity.Rows(cn, tx, "SELECT m.IM_ID,m.item_name,m.isStore,u.U_ID,u.U_Cargo,(SELECT COUNT(*) FROM dbo.Stores WITH(HOLDLOCK) WHERE ST_ID IN(@P2,@P3)) AS StoreCount FROM dbo.IM_Menu m WITH(HOLDLOCK) CROSS JOIN dbo.Units u WITH(HOLDLOCK) WHERE m.IM_ID=@P0 AND u.U_ID=@P1", itemId, unitId, storeId, destination)
        If s.References.Rows.Count <> 1 OrElse CInt(s.References.Rows(0)("StoreCount")) <> 1 Then Throw New InvalidOperationException("الصنف أو الوحدة أو المخزن غير موجود.")
        ' Capture all expiry buckets for the affected return item/store.
        s.Stock = AuditSalesQuantity.Rows(cn, tx, "SELECT IM_ID,CONVERT(bigint,ST_ID) AS ST_ID,ISNULL(D_Vaild,'') AS D_Vaild,CONVERT(float,ISNULL(SUM(QTY),0)) AS AuditStock FROM dbo.ST_Balance_V WITH(HOLDLOCK) WHERE IM_ID=@P0 AND ST_ID IN(@P1,@P2) GROUP BY IM_ID,ST_ID,ISNULL(D_Vaild,'') ORDER BY IM_ID,ST_ID,ISNULL(D_Vaild,'')", itemId, storeId, destination)
        If s.Lines.Rows.Count > 1000 OrElse s.Stock.Rows.Count > 2000 Then Throw New InvalidOperationException("تفاصيل المرتجع تتجاوز سعة المراجعة الآمنة.")
        Return s
    End Function

    Friend Shared Function Fingerprint(s As AuditReturnSnapshot) As String
        Dim value = String.Join("|", {AuditSalesQuantity.Fingerprint(s.Header), AuditSalesQuantity.Fingerprint(s.Lines), AuditSalesQuantity.Fingerprint(s.Stock), AuditSalesQuantity.Fingerprint(s.References)})
        Return BitConverter.ToString(AuditCanonical.Hash(Encoding.UTF8.GetBytes(value))).Replace("-", "")
    End Function

    Friend Shared Function Request(context As AuditEventContext, command As SqlCommand, billId As Integer, kind As Integer) As AuditOperationRequest
        Dim op = Operation(command), create = op = "Return.LineAdded", spec = AuditContract.ReturnSpec(kind)
        If op <> "Return.LineQuantityChanged" AndAlso Not command.CommandText.Trim("["c, "]"c).Equals(Procedure(kind, create), StringComparison.OrdinalIgnoreCase) Then Throw New ArgumentException("الإجراء لا يطابق نوع المرتجع.")
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = op, .EntityType = "ReturnDocument", .EntityId = billId.ToString(CultureInfo.InvariantCulture)}
        If create AndAlso Convert.ToInt32(AuditFinance.Parameter(command, "SB_T_ID")) <> billId Then Throw New ArgumentException("إذن السطر لا يطابق الشاشة.")
        If create AndAlso Convert.ToInt32(AuditFinance.Parameter(command, "Rtn_Type_ID")) <> If(kind = 10, 1, 7) Then Throw New ArgumentException("نوع سطر المرتجع لا يطابق الشاشة.")
        For Each name In AuditContract.ReturnInputs(op)
            Dim value As Object
            Select Case name
                Case "DocumentType" : value = kind
                Case "TargetLineId" : value = If(create, 0, Convert.ToInt32(AuditFinance.Parameter(command, "T_ID")))
                Case "ExpectedReturnSnapshot" : value = New String("0"c, 64)
                Case "OnUpdate" : value = If(Convert.ToBoolean(AuditFinance.Parameter(command, "On_Update", False)), 1, 0)
                Case "AllowNegative" : value = Convert.ToBoolean(AuditFinance.Parameter(command, "AllowNegative"))
                Case "DisplayedQuantity" : value = Convert.ToDouble(AuditFinance.Parameter(command, name))
                Case "QuantityDelta" : value = Convert.ToInt32(AuditFinance.Parameter(command, name))
                Case "SelectedItemId" : value = Convert.ToInt32(AuditFinance.Parameter(command, "IM_ID"))
                Case "SelectedUnitId" : value = Convert.ToInt32(AuditFinance.Parameter(command, "U_ID"))
                Case "SelectedStoreId" : value = Convert.ToInt32(AuditFinance.Parameter(command, "ST_ID"))
                Case "RequestedQuantity" : value = Convert.ToDouble(AuditFinance.Parameter(command, "QYT", 1R), CultureInfo.CurrentCulture)
                Case "RequestedCost" : value = Convert.ToDouble(AuditFinance.Parameter(command, "Cost", 0R), CultureInfo.CurrentCulture)
                Case "RequestedPrice" : value = Convert.ToDouble(AuditFinance.Parameter(command, "Price"), CultureInfo.CurrentCulture)
                Case "RequestedBarcode" : value = Convert.ToString(AuditFinance.Parameter(command, "Barcode", ""))
                Case "RequestedExpiry"
                    value = OptionalParameter(command, "D_Vaild")
                    If Not IsDBNull(value) Then value = Convert.ToDateTime(value, CultureInfo.CurrentCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                Case Else : value = OptionalParameter(command, AuditContract.ReturnParameter(name))
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
                r.Inputs.Single(Function(x) x.Name = "ExpectedReturnSnapshot").Value = AuditValue.Capture("String", Fingerprint(s))
                tx.Commit()
            End Using
        End Using
        AuditContract.Request(r)
    End Sub

    Private Shared Sub CheckQuantity(r As AuditOperationRequest, s As AuditReturnSnapshot)
        If r.OperationCode <> "Return.LineQuantityChanged" Then Return
        Dim row = s.Lines.Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("T_ID")) = CInt(V(r, "TargetLineId")))
        Dim shown = Double.Parse(V(r, "DisplayedQuantity"), CultureInfo.InvariantCulture)
        If shown <> CDbl(row("QTY")) Then Throw New InvalidOperationException("تغيرت كمية الصنف؛ حدّث المرتجع أولًا.")
        If shown + CInt(V(r, "QuantityDelta")) < 1R Then Throw New InvalidOperationException("الكمية غير موجبة؛ استخدم حذف الصنف.")
    End Sub

    Private Shared Function Allocate(cn As SqlConnection, tx As SqlTransaction, table As String) As Integer
        Dim ranges = AuditSalesQuantity.Rows(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)")
        If ranges.Rows.Count <> 1 OrElse IsDBNull(ranges.Rows(0)(0)) OrElse IsDBNull(ranges.Rows(0)(1)) Then Throw New InvalidOperationException("نطاق ترقيم المرتجع غير محدد.")
        Dim first = CInt(ranges.Rows(0)(0)), last = CInt(ranges.Rows(0)(1))
        Dim maximum = AuditSalesQuantity.Rows(cn, tx, "SELECT ISNULL(MAX(T_ID),@P0) FROM dbo.[" & table & "] WITH(UPDLOCK,HOLDLOCK) WHERE T_ID BETWEEN @P0 AND @P1", first, last)
        If first < 0 OrElse last <= first OrElse CLng(maximum.Rows(0)(0)) >= last Then Throw New InvalidOperationException("نطاق ترقيم سطور المرتجع ممتلئ أو غير صالح.")
        Return CInt(maximum.Rows(0)(0)) + 1
    End Function

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse Not AuditContract.IsReturnLine(request.OperationCode) Then Throw New ArgumentException("يلزم عقد سطر مرتجع ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, kind = CInt(V(r, "DocumentType")), spec = AuditContract.ReturnSpec(kind), create = r.OperationCode = "Return.LineAdded", delete = r.OperationCode = "Return.LineDeleted"
            Permission(cn, tx, r, kind)
            Dim before = Capture(cn, tx, r)
            If Fingerprint(before) <> V(r, "ExpectedReturnSnapshot") Then Throw New InvalidOperationException("تغير المرتجع أو رصيده؛ حدّث العرض قبل إعادة العملية.")
            CheckQuantity(r, before)
            Dim id = If(create, Allocate(cn, tx, spec(0)), CInt(V(r, "TargetLineId")))
            If create OrElse delete Then
                Using cmd = AuditRequestCoordinator.Command("dbo." & Procedure(kind, create), cn, tx)
                    If create Then
                        cmd.Parameters.Add("@SB_T_ID", SqlDbType.Int).Value = CInt(r.EntityId)
                        cmd.Parameters.Add("@Rtn_Type_ID", SqlDbType.Int).Value = If(kind = 10, 1, 7)
                        cmd.Parameters.Add("@IM_ID", SqlDbType.Int).Value = CInt(V(r, "SelectedItemId"))
                        cmd.Parameters.Add("@U_ID", SqlDbType.Int).Value = CInt(V(r, "SelectedUnitId"))
                        cmd.Parameters.Add("@ST_ID", SqlDbType.Int).Value = CInt(V(r, "SelectedStoreId"))
                        cmd.Parameters.Add("@QYT", SqlDbType.Float).Value = Double.Parse(V(r, "RequestedQuantity"), CultureInfo.InvariantCulture)
                        cmd.Parameters.Add("@Cost", SqlDbType.Float).Value = Double.Parse(V(r, "RequestedCost"), CultureInfo.InvariantCulture)
                        cmd.Parameters.Add("@Price", SqlDbType.Float).Value = Double.Parse(V(r, "RequestedPrice"), CultureInfo.InvariantCulture)
                        cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 50).Value = V(r, "RequestedBarcode")
                        Dim expiry = r.Inputs.Single(Function(x) x.Name = "RequestedExpiry").Value
                        cmd.Parameters.Add("@D_Vaild", SqlDbType.Date).Value = If(expiry.State = AuditValueState.SqlNull, CType(DBNull.Value, Object), DateTime.ParseExact(expiry.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture))
                        For Each name In AuditContract.ReturnOptionalInputs()
                            Dim input = r.Inputs.Single(Function(x) x.Name = name)
                            Dim type = AuditContract.FieldType(r.OperationCode, name)
                            Dim p = cmd.Parameters.Add("@" & AuditContract.ReturnParameter(name), If(type = "Integer", SqlDbType.Int, If(type = "String", SqlDbType.NVarChar, SqlDbType.Decimal)))
                            If type = "String" Then p.Size = 100
                            ' Infer decimal scale from the value, as the original AddWithValue path did.
                            ' Let the stored procedure perform SQL rounding, not client-side truncation.
                            p.Value = If(input.Value.State = AuditValueState.SqlNull, CType(DBNull.Value, Object), If(type = "Integer", CType(Integer.Parse(If(input.Value.Text, "0"), CultureInfo.InvariantCulture), Object), If(type = "Decimal", CType(Decimal.Parse(If(input.Value.Text, "0"), CultureInfo.InvariantCulture), Object), CType(input.Value.Text, Object))))
                        Next
                    Else
                        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = id
                    End If
                    cmd.ExecuteNonQuery()
                End Using
            Else
                ' A return line ID is never a sales line ID: qualify by its actual parent and type.
                Using cmd As New SqlCommand("UPDATE dbo.Rtn_Contents SET QTY=@Qty,ST_QTY=U_Cargo*@Qty,T_Price=Price*@Qty WHERE T_ID=@Line AND Bill_T_ID=@Bill AND Rtn_Type_ID=@Type; IF @@ROWCOUNT<>1 THROW 51128,'Return line changed.',1; EXEC dbo.Recalculate_Agent_Bill_Total @Bill;", cn, tx)
                    cmd.Parameters.Add("@Qty", SqlDbType.Float).Value = Double.Parse(V(r, "DisplayedQuantity"), CultureInfo.InvariantCulture) + CInt(V(r, "QuantityDelta"))
                    cmd.Parameters.Add("@Line", SqlDbType.Int).Value = id : cmd.Parameters.Add("@Bill", SqlDbType.Int).Value = CInt(r.EntityId)
                    cmd.Parameters.Add("@Type", SqlDbType.Int).Value = If(kind = 10, 1, 7)
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
            AddChanges(main, "ReturnDocument", r.EntityId, before.Header.Rows(0), after.Header.Rows(0))
            AddChanges(main, spec(2), id.ToString(CultureInfo.InvariantCulture), oldLine, newLine)
            events.Add(main)
            Dim buckets = MergeStock(before.Stock, after.Stock), part As AuditEventRequest = Nothing
            For Each pair In buckets
                If V(r, "AllowNegative") = "0" AndAlso Not IsDBNull(after.References.Rows(0)("isStore")) AndAlso {1, 2}.Contains(CInt(after.References.Rows(0)("isStore"))) Then
                    Dim oldBalance = CDbl(pair.Value(0)("AuditStock")), newBalance = CDbl(pair.Value(1)("AuditStock"))
                    If newBalance < -0.0000001R AndAlso newBalance < oldBalance - 0.0000001R Then Throw New InvalidOperationException("الرصيد الحالي للصنف لا يسمح بعملية المرتجع دون سحب كمية بالسالب.")
                End If
                If part Is Nothing OrElse part.Changes.Count + 4 > 100 Then
                    part = EventPart(r, after, occurred, events.Count + 1) : events.Add(part)
                End If
                AddChanges(part, "StockBucket", pair.Key, pair.Value(0), pair.Value(1))
            Next
            If events.Count > 100 Then Throw New InvalidOperationException("تفاصيل المرتجع تتجاوز سعة طلب المراقبة.")
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

    Private Shared Function EventPart(r As AuditOperationRequest, s As AuditReturnSnapshot, occurred As DateTime, ordinal As Integer) As AuditEventRequest
        Return New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = ordinal, .EventCode = r.OperationCode, .Outcome = 1, .EntityType = r.EntityType, .EntityId = r.EntityId,
            .DocumentNumber = Convert.ToString(s.Header.Rows(0)("AuditDocumentNumber"), CultureInfo.InvariantCulture), .BusinessDate = CDate(s.Header.Rows(0)("Date")), .OccurredAtUtc = occurred}
    End Function

    Private Shared Sub AddChanges(e As AuditEventRequest, entity As String, id As String, before As DataRow, after As DataRow)
        For Each definition In AuditContract.ReturnFields(entity)
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
