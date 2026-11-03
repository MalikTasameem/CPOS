Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

Friend NotInheritable Class AuditTableSnapshot
    Friend Tables As DataTable
    Friend Invoices As DataTable
    Friend Lines As DataTable
End Class

Friend NotInheritable Class AuditTables
    Friend Shared Function Operation(command As SqlCommand) As String
        Select Case command.CommandText.ToLowerInvariant()
            Case "transform_tables" : Return "Table.Transferred"
            Case "sb_contents_change_tb_im_qty" : Return "Table.LineQuantityChanged"
            Case "sb_contents_delete_tb_im" : Return "Table.LineDeleted"
            Case Else : Throw New ArgumentException("مسار الطاولة غير معتمد.")
        End Select
    End Function

    Friend Shared Function Parameter(command As SqlCommand, name As String) As Integer
        Dim p = command.Parameters.Cast(Of SqlParameter)().Single(Function(x) String.Equals(x.ParameterName.TrimStart("@"c), name, StringComparison.OrdinalIgnoreCase))
        If p.Value Is Nothing OrElse IsDBNull(p.Value) Then Throw New ArgumentException("معامل طاولة غير محدد.")
        Return Convert.ToInt32(p.Value, CultureInfo.InvariantCulture)
    End Function

    Private Shared Function Columns(entity As String) As String
        Return String.Join(",", AuditContract.TableFields(entity).Select(Function(x) "[" & x.Split("|"c)(1) & "]"))
    End Function

    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, operation As String, id As Integer, destination As Integer) As AuditTableSnapshot
        Dim transfer = operation = "Table.Transferred", result As New AuditTableSnapshot
        result.Tables = AuditSalesQuantity.Rows(cn, tx, "SELECT TB_ID," & Columns("TableState") & " FROM dbo.[Tables] WITH(UPDLOCK,HOLDLOCK) WHERE TB_ID IN(@P0,@P1) ORDER BY TB_ID", If(transfer, id, 0), If(transfer, destination, 0))
        Dim predicate = If(transfer, "Table_ID IN(@P0,@P1) AND isPied=0", "T_ID=@P0")
        result.Invoices = AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID," & Columns("TableInvoice") & " FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE " & predicate & " ORDER BY T_ID", id, destination)
        result.Lines = AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID," & Columns("TableLine") & " FROM dbo.SB_Contents WITH(UPDLOCK,HOLDLOCK) WHERE Bill_T_ID IN(SELECT T_ID FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE " & predicate & ") ORDER BY T_ID", id, destination)
        Return result
    End Function

    Friend Shared Function Fingerprint(s As AuditTableSnapshot) As String
        Return BitConverter.ToString(AuditCanonical.Hash(AuditCanonical.Encode({AuditSalesQuantity.Fingerprint(s.Tables), AuditSalesQuantity.Fingerprint(s.Invoices), AuditSalesQuantity.Fingerprint(s.Lines)}))).Replace("-", "")
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, context As AuditEventContext, transfer As Boolean)
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@User AND u.is_Allow=1 AND v.Sales=1" & If(transfer, " AND v.Transfer_Table=1", ""), cn, tx)
            cmd.Parameters.Add("@User", SqlDbType.Int).Value = context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("المستخدم غير مسموح له أو لا يملك صلاحية البيع/نقل الطاولات.")
        End Using
    End Sub

    Friend Shared Function Read(connection As String, context As AuditEventContext, command As SqlCommand) As AuditTableSnapshot
        Dim op = Operation(command), transfer = op = "Table.Transferred"
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Permission(cn, tx, context, transfer)
                Dim result = Capture(cn, tx, op, Parameter(command, If(transfer, "Table_From", "Bill_T_ID")), If(transfer, Parameter(command, "Table_To"), 0))
                tx.Commit() : Return result
            End Using
        End Using
    End Function

    Friend Shared Function Request(context As AuditEventContext, command As SqlCommand, before As AuditTableSnapshot, Optional displayedQuantity As Decimal = 0D) As AuditOperationRequest
        Dim op = Operation(command), transfer = op = "Table.Transferred"
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = op,
            .EntityType = If(transfer, "TableState", "TableInvoice"), .EntityId = Parameter(command, If(transfer, "Table_From", "Bill_T_ID")).ToString(CultureInfo.InvariantCulture)}
        Dim values As New Dictionary(Of String, Object) From {{"ExpectedTableSnapshot", Fingerprint(before)}}
        If transfer Then
            values.Add("DestinationTableId", Parameter(command, "Table_To"))
        Else
            values.Add("TargetLineId", Parameter(command, "T_ID")) : values.Add("DisplayedQuantity", displayedQuantity)
            If op = "Table.LineQuantityChanged" Then values.Add("QuantityDelta", Parameter(command, "Def"))
        End If
        For Each p In values
            Dim type = AuditContract.FieldType(op, p.Key)
            r.Inputs.Add(New AuditInput With {.Name = p.Key, .ValueType = type, .Value = AuditValue.Capture(type, p.Value)})
        Next
        AuditContract.Request(r) : ValidateTarget(r, before) : Return r
    End Function

    Private Shared Function V(r As AuditOperationRequest, name As String) As String
        Return r.Inputs.Single(Function(x) x.Name = name).Value.Text
    End Function

    Private Shared Sub ValidateTarget(r As AuditOperationRequest, s As AuditTableSnapshot)
        If r.OperationCode = "Table.Transferred" Then
            If s.Tables.Rows.Count <> 2 Then Throw New InvalidOperationException("يجب وجود الطاولتين قبل النقل.")
            If Not s.Invoices.Rows.Cast(Of DataRow)().Any(Function(x) Not IsDBNull(x("Table_ID")) AndAlso CInt(x("Table_ID")) = CInt(r.EntityId)) Then Throw New InvalidOperationException("لا توجد فاتورة غير مدفوعة على الطاولة المصدر؛ حدّث العرض.")
        Else
            If s.Invoices.Rows.Count <> 1 Then Throw New InvalidOperationException("فاتورة الطاولة غير موجودة.")
            Dim h = s.Invoices.Rows(0)
            If CInt(h("BsType_ID")) <> 1 OrElse CInt(h("isVoid")) <> 0 OrElse CInt(h("isPied")) <> 0 OrElse IsDBNull(h("Table_ID")) OrElse CInt(h("Table_ID")) <= 0 Then Throw New InvalidOperationException("الفاتورة ليست فاتورة طاولة مفتوحة وصالحة.")
            Dim row = s.Lines.Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CLng(x("T_ID")) = CLng(V(r, "TargetLineId")))
            If row Is Nothing Then Throw New InvalidOperationException("السطر غير موجود في فاتورة الطاولة المحددة.")
            If CDec(row("QTY")) <> Decimal.Parse(V(r, "DisplayedQuantity"), CultureInfo.InvariantCulture) Then Throw New InvalidOperationException("تغيرت الكمية منذ عرضها؛ حدّث الفاتورة.")
            If r.OperationCode = "Table.LineQuantityChanged" AndAlso V(r, "QuantityDelta") = "-1" AndAlso CDec(row("QTY")) < 2D Then Throw New InvalidOperationException("لا يمكن إنقاص الكمية المعروضة؛ استخدم الحذف عند الحاجة.")
        End If
    End Sub

    Private Shared Sub Append(item As AuditEventRequest, entity As String, id As String, before As DataRow, after As DataRow)
        For Each definition In AuditContract.TableFields(entity)
            Dim p = definition.Split("|"c)
            item.Changes.Add(New AuditFieldChange With {.ChangeNo = item.Changes.Count + 1, .EntityType = entity, .EntityId = id,
                .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1,
                .Before = If(before Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), before(p(1)))),
                .After = If(after Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), after(p(1))))})
        Next
    End Sub

    Private Shared Function Queue(cn As SqlConnection, tx As SqlTransaction) As DataTable
        ' No unique key exists. Lock the queue; compare multisets and assign request-local Q1..Qn only.
        Return AuditSalesQuantity.Rows(cn, tx, "SELECT " & Columns("TablePrintRow") & " FROM dbo.TB_AutoPrint_tmp WITH(UPDLOCK,HOLDLOCK) ORDER BY T_ID,[DATE],Printer_IP,Notes")
    End Function

    Friend Shared Function AddedQueue(before As DataTable, after As DataTable) As List(Of DataRow)
        Dim remaining As New Dictionary(Of String, Integer)(StringComparer.Ordinal), added As New List(Of DataRow)
        For Each row As DataRow In before.Rows
            Dim key = AuditSalesLineEdit.Fingerprint(row)
            If Not remaining.ContainsKey(key) Then remaining.Add(key, 0)
            remaining(key) += 1
        Next
        For Each row As DataRow In after.Rows
            Dim key = AuditSalesLineEdit.Fingerprint(row)
            If remaining.ContainsKey(key) AndAlso remaining(key) > 0 Then
                remaining(key) -= 1
            Else
                added.Add(row)
            End If
        Next
        If remaining.Values.Any(Function(x) x <> 0) Then Throw New InvalidOperationException("تغير طابور الطباعة خارج الإضافة المتوقعة.")
        Return added
    End Function

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse Not AuditContract.IsTableOperation(request.OperationCode) Then Throw New ArgumentException("يلزم عقد الطاولة ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, transfer = r.OperationCode = "Table.Transferred", delete = r.OperationCode = "Table.LineDeleted"
            Permission(cn, tx, r.Context, transfer)
            Dim id = Integer.Parse(r.EntityId, CultureInfo.InvariantCulture), destination = If(transfer, Integer.Parse(V(r, "DestinationTableId"), CultureInfo.InvariantCulture), 0)
            Dim before = Capture(cn, tx, r.OperationCode, id, destination)
            If Fingerprint(before) <> V(r, "ExpectedTableSnapshot") Then Throw New InvalidOperationException("تغيرت الطاولة أو الفاتورة أو أصنافها؛ حدّث العرض قبل المتابعة.")
            ValidateTarget(r, before)
            ' Never execute a mutation whose mandatory audit would exceed the existing contract limits.
            If transfer AndAlso before.Invoices.Rows.Count > 594 Then Throw New InvalidOperationException("عدد فواتير الطاولة يتجاوز سعة طلب المراقبة؛ لم ينفذ النقل.")
            Dim queueBefore = If(transfer, Nothing, Queue(cn, tx))
            Using cmd = AuditRequestCoordinator.Command(If(transfer, "dbo.Transform_Tables", If(delete, "dbo.SB_Contents_Delete_TB_IM", "dbo.SB_Contents_Change_TB_IM_Qty")), cn, tx)
                If transfer Then
                    cmd.Parameters.Add("@Table_From", SqlDbType.Int).Value = id
                    cmd.Parameters.Add("@Table_To", SqlDbType.Int).Value = destination
                Else
                    cmd.Parameters.Add("@Bill_T_ID", SqlDbType.Int).Value = id
                    cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = Integer.Parse(V(r, "TargetLineId"), CultureInfo.InvariantCulture)
                    If Not delete Then cmd.Parameters.Add("@Def", SqlDbType.Int).Value = Integer.Parse(V(r, "QuantityDelta"), CultureInfo.InvariantCulture)
                End If
                cmd.ExecuteNonQuery()
            End Using
            AuditRequestCoordinator.RequireTransaction(cn, tx)
            Dim after = Capture(cn, tx, r.OperationCode, id, destination)
            Dim occurred = AuditRequestCoordinator.ServerUtcNow(cn, tx)
            Dim documentNumber As String = Nothing
            If Not transfer AndAlso Not IsDBNull(before.Invoices.Rows(0)("SB_ID")) Then documentNumber = Convert.ToString(before.Invoices.Rows(0)("SB_ID"), CultureInfo.InvariantCulture)
            Dim part As Func(Of Integer, AuditEventRequest) = Function(n) New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = n, .EventCode = r.OperationCode,
                .Outcome = 1, .EntityType = r.EntityType, .EntityId = r.EntityId, .DocumentNumber = documentNumber, .OccurredAtUtc = occurred}
            Dim events As New List(Of AuditEventRequest) From {part(1)}
            If transfer Then
                If after.Tables.Rows.Count <> 2 OrElse after.Invoices.Rows.Count <> before.Invoices.Rows.Count Then Throw New InvalidOperationException("نتيجة نقل الطاولة غير مكتملة.")
                For Each row As DataRow In before.Tables.Rows
                    Append(events(0), "TableState", CInt(row("TB_ID")).ToString(CultureInfo.InvariantCulture), row, after.Tables.Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("TB_ID")) = CInt(row("TB_ID"))))
                Next
                Dim detail As AuditEventRequest = Nothing
                For Each row As DataRow In before.Invoices.Rows
                    Dim current = after.Invoices.Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("T_ID")) = CInt(row("T_ID")))
                    If CInt(current("Table_ID")) <> destination Then Throw New InvalidOperationException("لم يؤكد الإجراء نقل الفاتورة إلى الطاولة الهدف.")
                    If detail Is Nothing OrElse detail.Changes.Count + 15 > 100 Then
                        detail = part(events.Count + 1) : events.Add(detail)
                    End If
                    Append(detail, "TableInvoice", CInt(row("T_ID")).ToString(CultureInfo.InvariantCulture), row, current)
                Next
            Else
                Dim line = Integer.Parse(V(r, "TargetLineId"), CultureInfo.InvariantCulture)
                Dim oldRow = before.Lines.Rows.Cast(Of DataRow)().Single(Function(x) CLng(x("T_ID")) = line)
                Dim newRow = after.Lines.Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CLng(x("T_ID")) = line)
                If after.Invoices.Rows.Count <> 1 OrElse If(delete, newRow IsNot Nothing, newRow Is Nothing) Then Throw New InvalidOperationException("لم يؤكد الإجراء نتيجة صنف الطاولة.")
                If Not delete AndAlso CDec(newRow("QTY")) <> CDec(oldRow("QTY")) + Integer.Parse(V(r, "QuantityDelta"), CultureInfo.InvariantCulture) Then Throw New InvalidOperationException("كمية الطاولة اللاحقة غير متوقعة.")
                Append(events(0), "TableInvoice", r.EntityId, before.Invoices.Rows(0), after.Invoices.Rows(0))
                Append(events(0), "TableLine", line.ToString(CultureInfo.InvariantCulture), oldRow, newRow)
                Dim detail As AuditEventRequest = Nothing, n As Integer = 0
                For Each row In AddedQueue(queueBefore, Queue(cn, tx))
                    If detail Is Nothing OrElse detail.Changes.Count + 4 > 100 Then
                        detail = part(events.Count + 1) : events.Add(detail)
                    End If
                    n += 1 : Append(detail, "TablePrintRow", "Q" & n.ToString(CultureInfo.InvariantCulture), Nothing, row)
                Next
            End If
            If events.Count > 100 Then Throw New InvalidOperationException("تفاصيل الطاولة تتجاوز سعة طلب المراقبة؛ ستُلغى المعاملة كاملة.")
            For Each item In events
                AuditContract.EventData(r, item)
            Next
            For Each item In events
                AuditWriter.WriteInTransaction(cn, tx, lease, item)
            Next
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1,
                .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", r.EntityId}})}
        Catch
            lease.Faulted = True : Throw
        End Try
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
