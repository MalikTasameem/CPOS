Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

' Only the three reviewed finance screens. Existing business procedures remain authoritative.
Friend NotInheritable Class AuditFinance
    Friend Shared Function Parameter(command As SqlCommand, name As String, Optional fallback As Object = Nothing) As Object
        Dim p = command.Parameters.Cast(Of SqlParameter)().SingleOrDefault(Function(x) String.Equals(x.ParameterName.Trim().TrimStart("@"c), name, StringComparison.OrdinalIgnoreCase))
        If p Is Nothing Then
            If fallback Is Nothing Then Throw New ArgumentException("معامل مالي مطلوب: " & name)
            Return fallback
        End If
        If p.Value Is Nothing OrElse IsDBNull(p.Value) Then Throw New ArgumentException("معامل مالي غير محدد: " & name)
        Return p.Value
    End Function

    Friend Shared Function Operation(command As SqlCommand) As String
        Select Case command.CommandText.Trim("["c, "]"c).ToLowerInvariant()
            Case "treasury_balancemv_insert" : Return "TreasuryMovement.Created"
            Case "treasury_balancemv_isvoid" : Return "TreasuryMovement.Voided"
            Case "treasury_balancemv_transfer" : Return "TreasuryTransfer.Created"
            Case "treasury_balancemv_trans_isvoid" : Return "TreasuryTransfer.Voided"
            Case "ag_mv_transaction_pros"
                Dim process = CStr(Parameter(command, "Process"))
                If process <> "" AndAlso process <> "DELETE" Then Throw New ArgumentException("مسار تحويل الحساب غير معتمد.")
                If process = "" AndAlso Convert.ToInt32(Parameter(command, "T_ID")) <> 0 Then Throw New ArgumentException("تعديل تحويل محفوظ ليس ضمن مسار الإنشاء.")
                Return If(process = "DELETE", "AccountTransfer.Voided", "AccountTransfer.Created")
            Case Else : Throw New ArgumentException("إجراء مالي غير معتمد.")
        End Select
    End Function

    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, op As String, id As Integer) As DataTable
        Dim account = op.StartsWith("AccountTransfer.", StringComparison.Ordinal)
        Dim columns = String.Join(",", AuditContract.FinanceFields(If(account, "FinanceAccountRow", "FinanceTreasuryRow")).Select(Function(x) "[" & x.Split("|"c)(1) & "]"))
        Dim predicate = "T_ID=@P0"
        If op.StartsWith("TreasuryTransfer.", StringComparison.Ordinal) Then predicate = "Tran_ID=@P0"
        If op = "TreasuryMovement.Voided" Then predicate = "T_ID=@P0 OR Tran_ID=(SELECT Tran_ID FROM dbo.Treasury_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@P0)"
        Return AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID," & columns & " FROM dbo." & If(account, "AG_MV_TRANSACTION", "Treasury_Balance_MV") & " WITH(UPDLOCK,HOLDLOCK) WHERE " & predicate & " ORDER BY T_ID", id)
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, context As AuditEventContext, op As String, movementType As Integer)
        Dim flag = If(op.StartsWith("TreasuryMovement.", StringComparison.Ordinal), If(movementType = 1, "Tr_Widraw", "Tr_Deposit"), "Tr_Convert")
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@User AND u.is_Allow=1 AND v.[" & flag & "]=1", cn, tx)
            cmd.Parameters.Add("@User", SqlDbType.Int).Value = context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("المستخدم غير مسموح له أو لا يملك صلاحية الحركة المالية.")
        End Using
    End Sub

    Private Shared Function MovementType(op As String, data As DataTable, id As Integer) As Integer
        If Not op.StartsWith("TreasuryMovement.", StringComparison.Ordinal) Then Return 0
        Dim row = data.Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = id)
        If row Is Nothing OrElse Not {1, 2}.Contains(CInt(row("Tr_Type_ID"))) Then Throw New InvalidOperationException("حركة الخزينة غير موجودة أو نوعها غير مدعوم.")
        Return CInt(row("Tr_Type_ID"))
    End Function

    Friend Shared Function Read(connection As String, context As AuditEventContext, command As SqlCommand) As DataTable
        Dim op = Operation(command), id = Convert.ToInt32(Parameter(command, If(op = "TreasuryTransfer.Voided", "Tran_ID", "T_ID")))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Dim data = Capture(cn, tx, op, id)
                Permission(cn, tx, context, op, MovementType(op, data, id))
                ValidateVoid(op, data, id)
                tx.Commit() : Return data
            End Using
        End Using
    End Function

    Friend Shared Function Request(context As AuditEventContext, command As SqlCommand, before As DataTable) As AuditOperationRequest
        Dim op = Operation(command), create = op.EndsWith(".Created", StringComparison.Ordinal)
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = op,
            .EntityType = AuditContract.FinanceEntity(op), .EntityId = If(create, "New", Convert.ToInt32(Parameter(command, If(op = "TreasuryTransfer.Voided", "Tran_ID", "T_ID"))).ToString(CultureInfo.InvariantCulture))}
        If command.Parameters.Cast(Of SqlParameter)().Any(Function(x) x.ParameterName.TrimStart("@"c).Equals("User_ID", StringComparison.OrdinalIgnoreCase)) AndAlso Convert.ToInt32(Parameter(command, "User_ID")) <> context.ActorUserId.Value Then Throw New ArgumentException("المستخدم المالي لا يطابق الجلسة.")
        Dim movement = If(op = "TreasuryMovement.Created", Convert.ToInt32(Parameter(command, "Tr_Type_ID")), 0)
        For Each name In AuditContract.FinanceInputs(op)
            Dim value As Object
            Select Case name
                Case "ExpectedFinanceSnapshot"
                    ValidateVoid(op, before, CInt(r.EntityId)) : value = AuditSalesQuantity.Fingerprint(before)
                Case "RequestedDate" : value = DateTime.SpecifyKind(Convert.ToDateTime(Parameter(command, "Date")), DateTimeKind.Unspecified)
                Case "RequestedAmount" : value = Convert.ToDecimal(Parameter(command, If(op = "TreasuryMovement.Created", If(movement = 1, "Debit", "Credit"), "Value")), CultureInfo.CurrentCulture)
                Case "MovementType" : value = movement
                Case "TreasuryId" : value = Convert.ToInt32(Parameter(command, "Tr_ID"))
                Case "OpeningFlag" : value = If(Convert.ToBoolean(Parameter(command, "isFirst_Time")), 1, 0)
                Case "FromId" : value = Convert.ToInt32(Parameter(command, If(op = "AccountTransfer.Created", "AG_ID_FROM", "F_Tr_ID")))
                Case "ToId" : value = Convert.ToInt32(Parameter(command, If(op = "AccountTransfer.Created", "AG_ID_TO", "T_Tr_ID")))
                Case "Description" : value = Convert.ToString(Parameter(command, "About"))
                Case "Title" : value = Convert.ToString(Parameter(command, "Title"))
                Case Else : Throw New ArgumentException("مدخل مالي غير معتمد.")
            End Select
            Dim type = AuditContract.FieldType(op, name)
            r.Inputs.Add(New AuditInput With {.Name = name, .ValueType = type, .Value = AuditValue.Capture(type, value)})
        Next
        AuditContract.Request(r) : Return r
    End Function

    Private Shared Function V(r As AuditOperationRequest, name As String) As String
        Return r.Inputs.Single(Function(x) x.Name = name).Value.Text
    End Function

    Private Shared Sub ValidateVoid(op As String, data As DataTable, id As Integer)
        If data Is Nothing OrElse data.Rows.Count = 0 Then Throw New InvalidOperationException("الحركة المالية غير موجودة.")
        Dim account = op.StartsWith("AccountTransfer.", StringComparison.Ordinal)
        If account AndAlso (data.Rows.Count <> 1 OrElse CInt(data.Rows(0)("BsType_ID")) <> 40) Then Throw New InvalidOperationException("نوع تحويل الحساب غير صالح.")
        If Not account AndAlso data.Rows.Cast(Of DataRow)().Any(Function(x) Not IsDBNull(x("AGBalance_T_ID")) AndAlso CInt(x("AGBalance_T_ID")) <> 0) Then Throw New InvalidOperationException("الحركة مرتبطة بإيصال؛ ألغها من مسار الإيصال.")
        If data.Rows.Cast(Of DataRow)().All(Function(x) Convert.ToBoolean(x(If(account, "is_Void", "isVoid")))) Then Throw New InvalidOperationException("الحركة ملغاة بالفعل؛ حدّث العرض.")
        If data.Rows.Count > 300 Then Throw New InvalidOperationException("عدد أطراف الحركة يتجاوز سعة طلب المراقبة.")
        MovementType(op, data, id)
    End Sub

    Private Shared Function LockAllocation(cn As SqlConnection, tx As SqlTransaction, account As Boolean, transfer As Boolean) As Integer
        Dim ranges = AuditSalesQuantity.Rows(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)")
        If ranges.Rows.Count <> 1 OrElse IsDBNull(ranges.Rows(0)(0)) OrElse IsDBNull(ranges.Rows(0)(1)) Then Throw New InvalidOperationException("نطاق ترقيم الحركات غير محدد بشكل منفرد.")
        Dim first = CInt(ranges.Rows(0)(0)), last = CInt(ranges.Rows(0)(1))
        Dim table = If(account, "AG_MV_TRANSACTION", "Treasury_Balance_MV")
        If account AndAlso AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID FROM dbo.AG_MV_TRANSACTION WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=0").Rows.Count <> 0 Then Throw New InvalidOperationException("يوجد تحويل برقم صفر؛ لا يمكن ضمان مسار الإنشاء.")
        Dim ids = AuditSalesQuantity.Rows(cn, tx, "SELECT ISNULL(MAX(T_ID),@P0) FROM dbo." & table & " WITH(UPDLOCK,HOLDLOCK) WHERE T_ID BETWEEN @P0 AND @P1", first, last)
        If first < 0 OrElse last <= first OrElse CLng(ids.Rows(0)(0)) + If(transfer, 2L, 1L) > last Then Throw New InvalidOperationException("نطاق ترقيم الحركات ممتلئ أو غير صالح.")
        Dim expected = CInt(ids.Rows(0)(0)) + 1
        If transfer Then
            ids = AuditSalesQuantity.Rows(cn, tx, "SELECT ISNULL(MAX(Tran_ID),@P0) FROM dbo.Treasury_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE Tran_ID BETWEEN @P0 AND @P1", first, last)
            If CLng(ids.Rows(0)(0)) >= last Then Throw New InvalidOperationException("نطاق أرقام التحويل ممتلئ.")
            expected = CInt(ids.Rows(0)(0)) + 1
        End If
        Return expected
    End Function

    Private Shared Sub CheckReferences(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest)
        Dim account = r.OperationCode = "AccountTransfer.Created", direct = r.OperationCode = "TreasuryMovement.Created"
        Dim table = If(account, "Agents", "TreasuryCard"), key = If(account, "AG_ID", "Tr_ID")
        Dim fromId = Integer.Parse(V(r, If(direct, "TreasuryId", "FromId")), CultureInfo.InvariantCulture)
        Dim toId = If(direct, fromId, Integer.Parse(V(r, "ToId"), CultureInfo.InvariantCulture))
        If AuditSalesQuantity.Rows(cn, tx, "SELECT " & key & " FROM dbo." & table & " WITH(HOLDLOCK) WHERE " & key & " IN(@P0,@P1)", fromId, toId).Rows.Count <> If(direct, 1, 2) Then Throw New InvalidOperationException("أحد حسابات الحركة غير موجود.")
    End Sub

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse Not AuditContract.IsFinance(request.OperationCode) Then Throw New ArgumentException("يلزم عقد مالي ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, op = r.OperationCode, create = op.EndsWith(".Created", StringComparison.Ordinal)
            Dim account = op.StartsWith("AccountTransfer.", StringComparison.Ordinal), transfer = op.StartsWith("TreasuryTransfer.", StringComparison.Ordinal)
            Dim id = If(create, 0, Integer.Parse(r.EntityId, CultureInfo.InvariantCulture)), before As DataTable = Nothing
            Dim movement = If(op = "TreasuryMovement.Created", Integer.Parse(V(r, "MovementType"), CultureInfo.InvariantCulture), 0)
            If Not create Then
                before = Capture(cn, tx, op, id)
                If AuditSalesQuantity.Fingerprint(before) <> V(r, "ExpectedFinanceSnapshot") Then Throw New InvalidOperationException("تغيرت الحركة المالية؛ حدّث العرض قبل الإلغاء.")
                ValidateVoid(op, before, id) : movement = MovementType(op, before, id)
            End If
            Permission(cn, tx, r.Context, op, movement)
            Dim expectedId As Integer = 0
            If create Then
                CheckReferences(cn, tx, r) : expectedId = LockAllocation(cn, tx, account, transfer)
            End If
            Dim procedure = If(account, "AG_MV_TRANSACTION_pros", If(transfer, If(create, "Treasury_BalanceMV_Transfer", "Treasury_BalanceMV_Trans_isVoid"), If(create, "Treasury_BalanceMV_insert", "Treasury_BalanceMV_isVoid")))
            Using cmd = AuditRequestCoordinator.Command("dbo." & procedure, cn, tx)
                Dim p = cmd.Parameters.Add(If(transfer, "@Tran_ID", "@T_ID"), SqlDbType.Int)
                p.Value = id
                If create Then p.Direction = ParameterDirection.InputOutput
                If create Then
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Parse(V(r, "RequestedDate"), CultureInfo.InvariantCulture, DateTimeStyles.None)
                    ' Preserve SQL rounding: no client-side Scale=2/3 truncation.
                    cmd.Parameters.AddWithValue(If(account OrElse transfer, "@Value", If(movement = 1, "@Debit", "@Credit")), Decimal.Parse(V(r, "RequestedAmount"), CultureInfo.InvariantCulture))
                    cmd.Parameters.Add("@About", SqlDbType.NVarChar, -1).Value = V(r, "Description")
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = r.Context.ActorUserId.Value
                    If account OrElse transfer Then
                        cmd.Parameters.Add(If(account, "@AG_ID_FROM", "@F_Tr_ID"), SqlDbType.Int).Value = Integer.Parse(V(r, "FromId"), CultureInfo.InvariantCulture)
                        cmd.Parameters.Add(If(account, "@AG_ID_TO", "@T_Tr_ID"), SqlDbType.Int).Value = Integer.Parse(V(r, "ToId"), CultureInfo.InvariantCulture)
                    Else
                        cmd.Parameters.Add("@Tr_ID", SqlDbType.Int).Value = Integer.Parse(V(r, "TreasuryId"), CultureInfo.InvariantCulture)
                        cmd.Parameters.Add("@Tr_Type_ID", SqlDbType.Int).Value = movement
                        cmd.Parameters.Add("@isFirst_Time", SqlDbType.Int).Value = Integer.Parse(V(r, "OpeningFlag"), CultureInfo.InvariantCulture)
                    End If
                End If
                If account Then
                    cmd.Parameters.Add("@Process", SqlDbType.VarChar, 20).Value = If(create, "", "DELETE")
                    If create Then
                        cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 2500).Value = V(r, "Title")
                    Else
                        ' DELETE ignores the other inputs; bind actual persisted values, not UI text.
                        cmd.Parameters.AddWithValue("@Value", before.Rows(0)("VALUE"))
                        cmd.Parameters.AddWithValue("@Date", before.Rows(0)("DATE"))
                        cmd.Parameters.AddWithValue("@AG_ID_FROM", before.Rows(0)("AG_ID_FROM"))
                        cmd.Parameters.AddWithValue("@AG_ID_TO", before.Rows(0)("AG_ID_TO"))
                        cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 2500).Value = before.Rows(0)("TITLE")
                        cmd.Parameters.Add("@About", SqlDbType.NVarChar, -1).Value = before.Rows(0)("ABOUT")
                        cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = r.Context.ActorUserId.Value
                    End If
                End If
                cmd.ExecuteNonQuery()
                AuditRequestCoordinator.RequireTransaction(cn, tx)
                If create Then id = Convert.ToInt32(p.Value, CultureInfo.InvariantCulture)
            End Using
            If id <= 0 Then Throw New InvalidOperationException("لم يُرجع الإجراء رقم حركة صالحًا.")
            If create AndAlso id <> expectedId Then Throw New InvalidOperationException("رقم الحركة المنشأة خارج التخصيص المحجوز.")
            Dim after = Capture(cn, tx, op, id)
            If create Then
                If after.Rows.Count <> If(transfer, 2, 1) Then Throw New InvalidOperationException("نتيجة إنشاء الحركة غير مكتملة.")
                Dim amount = Decimal.Parse(V(r, "RequestedAmount"), CultureInfo.InvariantCulture)
                ' The treasury transfer proc accepts money (4dp), then stores numeric(18,3).
                Dim expected = If(account, Decimal.Round(amount, 3, MidpointRounding.AwayFromZero), If(transfer, Decimal.Round(Decimal.Round(amount, 4, MidpointRounding.AwayFromZero), 3, MidpointRounding.AwayFromZero), Decimal.Round(amount, 2, MidpointRounding.AwayFromZero)))
                For Each row As DataRow In after.Rows
                    If Convert.ToBoolean(row(If(account, "is_Void", "isVoid"))) OrElse CInt(row(If(account, "USER_ID", "User_ID"))) <> r.Context.ActorUserId.Value Then Throw New InvalidOperationException("حالة الحركة المنشأة غير متوقعة.")
                    If account Then
                        If CInt(row("AG_ID_FROM")) <> CInt(V(r, "FromId")) OrElse CInt(row("AG_ID_TO")) <> CInt(V(r, "ToId")) OrElse CDec(row("VALUE")) <> expected OrElse CInt(row("BsType_ID")) <> 40 Then Throw New InvalidOperationException("نتيجة تحويل الحسابات غير مطابقة.")
                    Else
                        Dim debit = If(transfer, CInt(row("Tr_Type_ID")) = 1, movement = 1)
                        Dim treasury = CInt(V(r, If(transfer, If(debit, "FromId", "ToId"), "TreasuryId")))
                        If CInt(row("Tr_ID")) <> treasury OrElse IsDBNull(row(If(debit, "Debit", "Credit"))) OrElse CDec(row(If(debit, "Debit", "Credit"))) <> expected OrElse Not IsDBNull(row("AGBalance_T_ID")) Then Throw New InvalidOperationException("طرف الخزينة غير مطابق.")
                        If Not IsDBNull(row(If(debit, "Credit", "Debit"))) OrElse (transfer AndAlso (IsDBNull(row("Pure")) OrElse CDec(row("Pure")) <> expected)) Then Throw New InvalidOperationException("قيم طرف الخزينة غير متوقعة.")
                    End If
                Next
                If transfer AndAlso after.Rows.Cast(Of DataRow)().Count(Function(x) CInt(x("Tr_Type_ID")) = 1) <> 1 Then Throw New InvalidOperationException("طرفا التحويل غير متوازنين.")
            Else
                If after.Rows.Count <> before.Rows.Count OrElse after.Rows.Cast(Of DataRow)().Any(Function(x) Not Convert.ToBoolean(x(If(account, "is_Void", "isVoid")))) Then Throw New InvalidOperationException("لم يؤكد الإجراء إلغاء جميع أطراف الحركة.")
                Dim withoutVoid = after.Copy()
                For Each row As DataRow In withoutVoid.Rows
                    row(If(account, "is_Void", "isVoid")) = before.Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("T_ID")) = CInt(row("T_ID")))(If(account, "is_Void", "isVoid"))
                Next
                If AuditSalesQuantity.Fingerprint(withoutVoid) <> AuditSalesQuantity.Fingerprint(before) Then Throw New InvalidOperationException("تغيرت حقول مالية خارج أثر الإلغاء المعتمد.")
            End If
            Dim entity = If(account, "FinanceAccountRow", "FinanceTreasuryRow"), fields = AuditContract.FinanceFields(entity), occurred = AuditRequestCoordinator.ServerUtcNow(cn, tx)
            Dim events As New List(Of AuditEventRequest), item As AuditEventRequest = Nothing
            For Each row As DataRow In after.Rows
                If item Is Nothing OrElse item.Changes.Count + fields.Length > 100 Then
                    item = New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = events.Count + 1, .EventCode = op, .Outcome = 1, .EntityType = r.EntityType, .EntityId = id.ToString(CultureInfo.InvariantCulture), .DocumentNumber = id.ToString(CultureInfo.InvariantCulture), .OccurredAtUtc = occurred}
                    events.Add(item)
                End If
                Dim oldRow = If(create, Nothing, before.Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("T_ID")) = CInt(row("T_ID"))))
                For Each definition In fields
                    Dim p = definition.Split("|"c)
                    item.Changes.Add(New AuditFieldChange With {.ChangeNo = item.Changes.Count + 1, .EntityType = entity, .EntityId = CInt(row("T_ID")).ToString(CultureInfo.InvariantCulture), .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1,
                        .Before = If(create, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), oldRow(p(1)))), .After = AuditValue.Capture(p(2), row(p(1)))})
                Next
            Next
            If events.Count > 100 Then Throw New InvalidOperationException("تفاصيل الحركة تتجاوز سعة المراقبة.")
            For Each entry In events
                AuditContract.EventData(r, entry)
            Next
            For Each entry In events
                AuditWriter.WriteInTransaction(cn, tx, lease, entry)
            Next
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", id.ToString(CultureInfo.InvariantCulture)}})}
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
