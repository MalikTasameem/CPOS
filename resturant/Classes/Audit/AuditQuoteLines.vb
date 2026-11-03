Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Text

Friend NotInheritable Class AuditQuoteSnapshot
    Friend Header As DataTable
    Friend Lines As DataTable
    Friend References As DataTable
    Friend Components As DataTable
    Friend ExpiryReference As DataTable
End Class

' Quote rows are not sale rows. No sales stock, receipts or component mutations belong here.
Friend NotInheritable Class AuditQuoteLines
    Friend Shared Function V(r As AuditOperationRequest, name As String) As String
        Return r.Inputs.Single(Function(x) x.Name = name).Value.Text
    End Function

    Friend Shared Function Procedure(operation As String) As String
        Select Case operation
            Case "Quote.LineAdded" : Return "ViewBill_Contents_INSERT_2"
            Case "Quote.LineDeleted" : Return "ViewBill_Contents_Delete_IM"
            Case "Quote.LineChanged" : Return "View_Contents_IM_Unit_Change"
            Case "Quote.LineQuantityStepped" : Return "ViewBill_Contents_Change_IM_Qty"
            Case Else : Throw New ArgumentException("عملية عرض سعر غير معتمدة.")
        End Select
    End Function

    Friend Shared Function Request(context As AuditEventContext, command As SqlCommand, bill As Integer, onUpdate As Boolean) As AuditOperationRequest
        Dim op = {"Quote.LineAdded", "Quote.LineDeleted", "Quote.LineChanged", "Quote.LineQuantityStepped"}.SingleOrDefault(Function(x) Procedure(x).Equals(command.CommandText.Trim("["c, "]"c), StringComparison.OrdinalIgnoreCase))
        If op Is Nothing Then Throw New ArgumentException("إجراء عرض غير معتمد.")
        Dim create = op = "Quote.LineAdded"
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = op, .EntityType = "QuoteDocument", .EntityId = bill.ToString(CultureInfo.InvariantCulture)}
        If create AndAlso Convert.ToInt32(AuditFinance.Parameter(command, "SB_T_ID")) <> bill Then Throw New ArgumentException("رأس الصنف لا يطابق العرض.")
        For Each definition In AuditContract.QuoteInputs(op)
            Dim p = definition.Split("|"c), value As Object
            Select Case p(0)
                Case "DocumentType" : value = 16
                Case "OnUpdate" : value = onUpdate
                Case "TargetLineId" : value = If(create, 0, Convert.ToInt32(AuditFinance.Parameter(command, If(op = "Quote.LineChanged", "IM_T_ID", "T_ID"))))
                Case "ExpectedQuoteSnapshot" : value = New String("0"c, 64)
                Case "DisplayedQuantity" : value = AuditFinance.Parameter(command, "DisplayedQuantity")
                Case Else
                    Dim parameter = command.Parameters.Cast(Of SqlParameter)().SingleOrDefault(Function(x) x.ParameterName.TrimStart("@"c).Equals(p(1), StringComparison.OrdinalIgnoreCase))
                    If parameter Is Nothing OrElse parameter.Value Is Nothing OrElse IsDBNull(parameter.Value) Then
                        If p(0) = "RequestedQuantity" AndAlso create Then
                            value = 1R
                        ElseIf p(3) = "O" Then
                            value = DBNull.Value
                        Else
                            Throw New ArgumentException("معامل عرض سعر مطلوب: " & p(1))
                        End If
                    Else
                        value = parameter.Value
                    End If
            End Select
            If Not IsDBNull(value) Then
                Select Case p(2)
                    Case "Integer" : value = Convert.ToInt32(value, CultureInfo.CurrentCulture)
                    Case "Float" : value = Convert.ToDouble(value, CultureInfo.CurrentCulture)
                    Case "Decimal" : value = Convert.ToDecimal(value, CultureInfo.CurrentCulture)
                    Case "Date" : value = Convert.ToDateTime(value, CultureInfo.CurrentCulture).Date
                    Case "String" : value = Convert.ToString(value, CultureInfo.CurrentCulture)
                End Select
            End If
            r.Inputs.Add(New AuditInput With {.Name = p(0), .ValueType = p(2), .Value = AuditValue.Capture(p(2), value)})
        Next
        AuditContract.Request(r) : Return r
    End Function

    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, Optional old As AuditQuoteSnapshot = Nothing) As AuditQuoteSnapshot
        Dim s As New AuditQuoteSnapshot With {.Header = AuditDocumentFields.Capture(cn, tx, r)}
        s.Lines = AuditSalesQuantity.Rows(cn, tx, "SELECT q.*,m.item_name AS AuditItemName FROM dbo.ViewBill_Contents q WITH(UPDLOCK,HOLDLOCK) LEFT JOIN dbo.IM_Menu m WITH(HOLDLOCK) ON m.IM_ID=q.IM_ID WHERE q.Bill_T_ID=@P0 ORDER BY q.T_ID", CInt(r.EntityId))
        Dim create = r.OperationCode = "Quote.LineAdded", item As Integer, unit As Integer, storeId As Integer
        If create Then
            item = CInt(V(r, "SelectedItemId")) : unit = CInt(V(r, "SelectedUnitId")) : storeId = CInt(V(r, "SelectedStoreId"))
        Else
            Dim line = If(old, s).Lines.Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = CInt(V(r, "TargetLineId")))
            If line Is Nothing Then Throw New InvalidOperationException("الصنف لا يتبع عرض السعر الحالي أو حُذف.")
            item = CInt(line("IM_ID")) : unit = If(r.OperationCode = "Quote.LineChanged", CInt(V(r, "SelectedUnitId")), CInt(line("U_ID"))) : storeId = CInt(line("ST_ID"))
        End If
        s.References = AuditSalesQuantity.Rows(cn, tx, "SELECT u.IM_ID,u.U_ID,u.U_Cargo,u.Cost,u.Price,m.item_name,(SELECT COUNT(*) FROM dbo.Stores WITH(HOLDLOCK) WHERE ST_ID=@P2) AS StoreCount FROM dbo.IM_Menu_Units_V u WITH(HOLDLOCK) JOIN dbo.IM_Menu m WITH(HOLDLOCK) ON m.IM_ID=u.IM_ID WHERE u.IM_ID=@P0 AND u.U_ID=@P1", item, unit, storeId)
        If s.References.Rows.Count <> 1 OrElse CInt(s.References.Rows(0)("StoreCount")) <> 1 OrElse IsDBNull(s.References.Rows(0)("U_Cargo")) OrElse CDbl(s.References.Rows(0)("U_Cargo")) <= 0R Then Throw New InvalidOperationException("الصنف أو رابط الوحدة أو المخزن غير صالح.")
        s.Components = AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM dbo.SB_IM_Components WITH(UPDLOCK,HOLDLOCK) WHERE IM_T_ID=@P0 ORDER BY T_ID", If(create, 0, CInt(V(r, "TargetLineId"))))
        s.ExpiryReference = New DataTable()
        If create AndAlso r.Inputs.Single(Function(x) x.Name = "RequestedExpiry").Value.State = AuditValueState.Present AndAlso r.Inputs.Single(Function(x) x.Name = "CurrentQuantity").Value.State = AuditValueState.Present AndAlso Double.Parse(V(r, "CurrentQuantity"), CultureInfo.InvariantCulture) <= 0R Then
            s.ExpiryReference = AuditSalesQuantity.Rows(cn, tx, "SELECT TOP(1) T_ID,D_Vaild FROM dbo.ViewBill_Contents WITH(UPDLOCK,HOLDLOCK) WHERE IM_ID=@P0 AND ST_ID=@P1 ORDER BY T_ID DESC", item, storeId)
        End If
        If s.Lines.Rows.Count > 2000 Then Throw New InvalidOperationException("عرض السعر يتجاوز سعة المراجعة الآمنة.")
        Return s
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, s As AuditQuoteSnapshot)
        Dim row = s.Header.Rows(0)
        If IsDBNull(row("isVoid")) OrElse CInt(row("isVoid")) <> 0 OrElse IsDBNull(row("isDepended")) OrElse Not {0, 1}.Contains(CInt(row("isDepended"))) Then Throw New InvalidOperationException("عرض السعر ملغى أو حالته غير معروفة.")
        If CInt(row("isDepended")) = 1 AndAlso V(r, "OnUpdate") <> "1" Then Throw New InvalidOperationException("افتح تعديل عرض السعر أولًا.")
        Dim editing = CInt(row("isDepended")) = 1 OrElse V(r, "OnUpdate") = "1"
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@User AND u.is_Allow=1 AND v.Sales=1" & If(editing, " AND v.SB_Update=1", ""), cn, tx)
            cmd.Parameters.Add("@User", SqlDbType.Int).Value = r.Context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("لا تملك صلاحية إضافة/تعديل عرض السعر.")
        End Using
        If r.OperationCode = "Quote.LineDeleted" Then
            Using cmd As New SqlCommand("SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.ViewBill_Contents_Delete_IM'))", cn, tx)
                Dim definition = Convert.ToString(cmd.ExecuteScalar(), CultureInfo.InvariantCulture)
                If Not definition.Contains("QuoteDelete033:") OrElse definition.IndexOf("DELETE FROM SB_IM_Components", StringComparison.OrdinalIgnoreCase) >= 0 Then Throw New InvalidOperationException("لم يثبت تصحيح عزل حذف عرض السعر 033؛ لا يمكن متابعة الحذف.")
            End Using
        End If
        If r.OperationCode = "Quote.LineQuantityStepped" Then
            Dim line = s.Lines.Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("T_ID")) = CInt(V(r, "TargetLineId")))
            If CDbl(line("QTY")) <> Double.Parse(V(r, "DisplayedQuantity"), CultureInfo.InvariantCulture) Then Throw New InvalidOperationException("تغيرت الكمية؛ حدّث عرض السعر.")
        End If
    End Sub

    Friend Shared Function Fingerprint(s As AuditQuoteSnapshot) As String
        Dim value = String.Join("|", {AuditSalesQuantity.Fingerprint(s.Header), AuditSalesQuantity.Fingerprint(s.Lines), AuditSalesQuantity.Fingerprint(s.References), AuditSalesQuantity.Fingerprint(s.Components), AuditSalesQuantity.Fingerprint(s.ExpiryReference)})
        Return BitConverter.ToString(AuditCanonical.Hash(Encoding.UTF8.GetBytes(value))).Replace("-", "")
    End Function

    Friend Shared Sub Prepare(connection As String, r As AuditOperationRequest)
        AuditContract.Request(r)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Dim s = Capture(cn, tx, r) : Permission(cn, tx, r, s)
                r.Inputs.Single(Function(x) x.Name = "ExpectedQuoteSnapshot").Value = AuditValue.Capture("String", Fingerprint(s))
                tx.Commit()
            End Using
        End Using
    End Sub

    Private Shared Function Allocate(cn As SqlConnection, tx As SqlTransaction) As Integer
        Dim ranges = AuditSalesQuantity.Rows(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)")
        If ranges.Rows.Count <> 1 OrElse IsDBNull(ranges.Rows(0)(0)) OrElse IsDBNull(ranges.Rows(0)(1)) Then Throw New InvalidOperationException("نطاق ترقيم العرض غير محدد.")
        Dim first = CInt(ranges.Rows(0)(0)), last = CInt(ranges.Rows(0)(1))
        Dim maximum = AuditSalesQuantity.Rows(cn, tx, "SELECT ISNULL(MAX(T_ID),@P0) FROM dbo.ViewBill_Contents WITH(UPDLOCK,HOLDLOCK) WHERE T_ID BETWEEN @P0 AND @P1", first, last)
        If first < 0 OrElse last <= first OrElse CLng(maximum.Rows(0)(0)) >= last Then Throw New InvalidOperationException("نطاق ترقيم أصناف العرض ممتلئ.")
        Return CInt(maximum.Rows(0)(0)) + 1
    End Function

    Friend Shared Function EventData(r As AuditOperationRequest, before As AuditQuoteSnapshot, after As AuditQuoteSnapshot, lineId As Integer, occurred As DateTime) As AuditEventRequest
        Dim oldLine = before.Lines.Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = lineId), newLine = after.Lines.Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = lineId)
        Dim create = r.OperationCode = "Quote.LineAdded", delete = r.OperationCode = "Quote.LineDeleted"
        If If(create, oldLine IsNot Nothing OrElse newLine Is Nothing, If(delete, oldLine Is Nothing OrElse newLine IsNot Nothing, oldLine Is Nothing OrElse newLine Is Nothing)) Then Throw New InvalidOperationException("لم يتأكد أثر الصنف المطلوب.")
        For Each pair In {New DataTable() {before.Lines, after.Lines}}
            Dim b = pair(0).Copy(), a = pair(1).Copy()
            For Each dt In {b, a}
                For Each row In dt.Rows.Cast(Of DataRow)().Where(Function(x) CInt(x("T_ID")) = lineId).ToArray()
                    dt.Rows.Remove(row)
                Next
            Next
            If AuditSalesQuantity.Fingerprint(b) <> AuditSalesQuantity.Fingerprint(a) Then Throw New InvalidOperationException("تغيرت أصناف أخرى خارج العملية.")
        Next
        If AuditSalesQuantity.Fingerprint(before.Components) <> AuditSalesQuantity.Fingerprint(after.Components) Then Throw New InvalidOperationException("تغيرت مكونات بيع لا تتبع عرض السعر؛ أُلغيت العملية.")
        If create AndAlso (CInt(newLine("IM_ID")) <> CInt(V(r, "SelectedItemId")) OrElse CInt(newLine("U_ID")) <> CInt(V(r, "SelectedUnitId")) OrElse CInt(newLine("ST_ID")) <> CInt(V(r, "SelectedStoreId"))) Then Throw New InvalidOperationException("الصنف المنشأ لا يطابق الاختيار.")
        If Not delete Then
            Dim qty As Double = If(r.OperationCode = "Quote.LineQuantityStepped", Double.Parse(V(r, "DisplayedQuantity"), CultureInfo.InvariantCulture) + CInt(V(r, "QuantityDelta")), If(create, Double.Parse(V(r, "RequestedQuantity"), CultureInfo.InvariantCulture), CDbl(Decimal.Round(Decimal.Parse(V(r, "RequestedQuantity"), CultureInfo.InvariantCulture), 2, MidpointRounding.AwayFromZero))))
            If CDbl(newLine("QTY")) <> qty Then Throw New InvalidOperationException("الكمية المحفوظة لا تطابق الطلب.")
            If r.OperationCode = "Quote.LineChanged" AndAlso CInt(newLine("U_ID")) <> CInt(V(r, "SelectedUnitId")) Then Throw New InvalidOperationException("الوحدة المحفوظة لا تطابق الطلب.")
        End If
        Dim header = after.Header.Rows(0)
        Dim e As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = 1, .EventCode = r.OperationCode, .EntityType = r.EntityType, .EntityId = r.EntityId, .DocumentNumber = Convert.ToString(header("AuditDocumentNumber"), CultureInfo.InvariantCulture), .OccurredAtUtc = occurred, .Outcome = 1}
        If Not IsDBNull(header("Date")) Then e.BusinessDate = CDate(header("Date"))
        For Each entity In {"QuoteDocument", "QuoteLine"}
            Dim b = If(entity = "QuoteDocument", before.Header.Rows(0), oldLine), a = If(entity = "QuoteDocument", header, newLine)
            For Each d In AuditContract.QuoteFields(entity)
                Dim p = d.Split("|"c)
                If b IsNot Nothing AndAlso a IsNot Nothing Then
                    Dim allowed = If(entity = "QuoteDocument", (create OrElse delete) AndAlso {"DocumentTotal", "DocumentPure"}.Contains(p(0)), If(r.OperationCode = "Quote.LineQuantityStepped", {"QuoteQuantity", "QuoteLineTotal", "QuoteStockQuantity"}.Contains(p(0)), {"QuoteUnitId", "QuoteQuantity", "QuotePrice", "QuoteLineTotal", "QuoteCargo", "QuoteStockQuantity"}.Contains(p(0))))
                    Dim bv = AuditValue.Capture(p(2), b(p(1))), av = AuditValue.Capture(p(2), a(p(1)))
                    If Not allowed AndAlso (bv.State <> av.State OrElse bv.Text <> av.Text) Then Throw New InvalidOperationException("تغير حقل خارج نطاق صنف العرض: " & p(0))
                End If
                e.Changes.Add(New AuditFieldChange With {.ChangeNo = e.Changes.Count + 1, .EntityType = entity, .EntityId = If(entity = "QuoteDocument", r.EntityId, lineId.ToString(CultureInfo.InvariantCulture)), .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1,
                    .Before = If(b Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), b(p(1)))),
                    .After = If(a Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), a(p(1))))})
            Next
        Next
        AuditContract.EventData(r, e) : Return e
    End Function

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse Not AuditContract.IsQuoteLine(request.OperationCode) Then Throw New ArgumentException("يلزم عقد عرض السعر ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, before = Capture(cn, tx, r), create = r.OperationCode = "Quote.LineAdded"
            Permission(cn, tx, r, before)
            If Fingerprint(before) <> V(r, "ExpectedQuoteSnapshot") Then Throw New InvalidOperationException("تغير العرض أو مراجع الصنف؛ حدّث العرض.")
            Dim id = If(create, Allocate(cn, tx), CInt(V(r, "TargetLineId")))
            Using cmd = AuditRequestCoordinator.Command("dbo." & Procedure(r.OperationCode), cn, tx)
                cmd.Parameters.Add(If(create, "@SB_T_ID", If(r.OperationCode = "Quote.LineChanged", "@IM_T_ID", "@T_ID")), SqlDbType.Int).Value = If(create, CInt(r.EntityId), id)
                For Each definition In AuditContract.QuoteInputs(r.OperationCode)
                    Dim p = definition.Split("|"c)
                    If p(1) = "" Then Continue For
                    Dim input = r.Inputs.Single(Function(x) x.Name = p(0)).Value, value As Object = DBNull.Value
                    If input.State = AuditValueState.Present Then
                        Select Case p(2)
                            Case "Integer" : value = Integer.Parse(input.Text, CultureInfo.InvariantCulture)
                            Case "Float" : value = Double.Parse(input.Text, CultureInfo.InvariantCulture)
                            Case "Decimal" : value = Decimal.Parse(input.Text, CultureInfo.InvariantCulture)
                            Case "Date" : value = DateTime.ParseExact(input.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                            Case "String" : value = input.Text
                        End Select
                    End If
                    cmd.Parameters.AddWithValue("@" & p(1), value)
                Next
                cmd.ExecuteNonQuery()
            End Using
            AuditRequestCoordinator.RequireTransaction(cn, tx)
            Dim after = Capture(cn, tx, r, before)
            Dim e = EventData(r, before, after, id, AuditRequestCoordinator.ServerUtcNow(cn, tx))
            AuditWriter.WriteInTransaction(cn, tx, lease, e)
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", r.EntityId}, {"LineId", id.ToString(CultureInfo.InvariantCulture)}})}
        Catch
            lease.Faulted = True : Throw
        End Try
    End Function

    Friend Shared Function Save(connection As String, r As AuditOperationRequest) As AuditRequestResult
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Dim result = ExecuteInTransaction(cn, tx, r)
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
