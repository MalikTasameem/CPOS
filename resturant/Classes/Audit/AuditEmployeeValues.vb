Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

Friend NotInheritable Class AuditEmployeeValues
    Friend Shared Function Operation(command As SqlCommand) As String
        Select Case command.CommandText.Trim("["c, "]"c).ToLowerInvariant()
            Case "others_values_insert" : Return "EmployeeValue.Created"
            Case "others_values_update" : Return "EmployeeValue.Updated"
            Case "others_values_void" : Return "EmployeeValue.Voided"
            Case Else : Throw New ArgumentException("إجراء قيمة موظف غير معتمد.")
        End Select
    End Function
    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, id As Integer) As DataTable
        Return AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID," & String.Join(",", AuditContract.EmployeeValueFields().Select(Function(x) "[" & x.Split("|"c)(1) & "]")) & " FROM dbo.Others_Values WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@P0", id)
    End Function
    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, context As AuditEventContext)
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@User AND u.is_Allow=1 AND v.Balance=1", cn, tx)
            cmd.Parameters.Add("@User", SqlDbType.Int).Value = context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("لا تملك صلاحية الحسابات وقيم الموظفين.")
        End Using
    End Sub
    Private Shared Sub Editable(data As DataTable)
        If data Is Nothing OrElse data.Rows.Count <> 1 Then Throw New InvalidOperationException("قيمة الموظف غير موجودة.")
        If Not IsDBNull(data.Rows(0)("isVoid")) AndAlso CInt(data.Rows(0)("isVoid")) <> 0 Then Throw New InvalidOperationException("قيمة الموظف ملغاة بالفعل.")
    End Sub
    Friend Shared Function Read(connection As String, context As AuditEventContext, command As SqlCommand) As DataTable
        AuditContract.Context(context, Operation(command))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Permission(cn, tx, context)
                Dim data = Capture(cn, tx, Convert.ToInt32(AuditFinance.Parameter(command, "T_ID")))
                Editable(data)
                tx.Commit() : Return data
            End Using
        End Using
    End Function
    Friend Shared Function Request(context As AuditEventContext, command As SqlCommand, before As DataTable) As AuditOperationRequest
        Dim op = Operation(command), create = op = "EmployeeValue.Created"
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = op, .EntityType = "EmployeeValue",
            .EntityId = If(create, "New", Convert.ToInt32(AuditFinance.Parameter(command, "T_ID")).ToString(CultureInfo.InvariantCulture))}
        If op <> "EmployeeValue.Voided" AndAlso Convert.ToInt32(AuditFinance.Parameter(command, "User_ID")) <> context.ActorUserId.Value Then Throw New ArgumentException("مستخدم القيمة لا يطابق الجلسة.")
        For Each name In AuditContract.EmployeeValueInputs(op)
            Dim value As Object
            If name = "ExpectedEmployeeValueSnapshot" Then
                Editable(before) : value = AuditSalesQuantity.Fingerprint(before)
            Else
                Dim field = AuditContract.EmployeeValueFields().Single(Function(x) x.StartsWith(name & "|", StringComparison.Ordinal)).Split("|"c)
                value = AuditFinance.Parameter(command, field(1))
                Select Case field(2)
                    Case "Integer" : value = Convert.ToInt32(value)
                    Case "DateTime" : value = DateTime.SpecifyKind(Convert.ToDateTime(value), DateTimeKind.Unspecified)
                    Case "Decimal" : value = Convert.ToDecimal(value, CultureInfo.CurrentCulture)
                    Case "String" : value = Convert.ToString(value)
                End Select
            End If
            Dim type = AuditContract.FieldType(op, name)
            r.Inputs.Add(New AuditInput With {.Name = name, .ValueType = type, .Value = AuditValue.Capture(type, value)})
        Next
        AuditContract.Request(r) : Return r
    End Function
    Private Shared Function V(r As AuditOperationRequest, name As String) As String
        Return r.Inputs.Single(Function(x) x.Name = name).Value.Text
    End Function
    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse Not AuditContract.IsEmployeeValue(request.OperationCode) Then Throw New ArgumentException("يلزم عقد قيمة موظف ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, op = r.OperationCode, create = op = "EmployeeValue.Created", cancel = op = "EmployeeValue.Voided"
            Permission(cn, tx, r.Context)
            Dim id = If(create, 0, Integer.Parse(r.EntityId, CultureInfo.InvariantCulture)), before As DataTable = Nothing, maximum As Integer = 0
            If create Then
                ' The existing procedure does not return its ID. Lock the table, then identify the one new row.
                maximum = CInt(AuditSalesQuantity.Rows(cn, tx, "SELECT ISNULL(MAX(T_ID),0) FROM dbo.Others_Values WITH(TABLOCKX,HOLDLOCK)").Rows(0)(0))
            Else
                before = Capture(cn, tx, id) : Editable(before)
                If AuditSalesQuantity.Fingerprint(before) <> V(r, "ExpectedEmployeeValueSnapshot") Then Throw New InvalidOperationException("تغيرت قيمة الموظف؛ حدّث العرض.")
            End If
            If Not cancel Then
                Dim employee = Integer.Parse(V(r, "EVEmployeeId"), CultureInfo.InvariantCulture)
                If AuditSalesQuantity.Rows(cn, tx, "SELECT AG_ID FROM dbo.Agents WITH(HOLDLOCK) WHERE AG_ID=@P0 AND Type_ID=4", employee).Rows.Count <> 1 Then Throw New InvalidOperationException("الحساب المحدد ليس موظفًا.")
            End If
            Using cmd = AuditRequestCoordinator.Command("dbo." & If(create, "Others_Values_Insert", If(cancel, "Others_Values_Void", "Others_Values_Update")), cn, tx)
                If Not create Then cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = id
                If Not cancel Then
                    cmd.Parameters.Add("@DateOF_Order", SqlDbType.DateTime).Value = DateTime.Parse(V(r, "EVBusinessDate"), CultureInfo.InvariantCulture)
                    For Each name In {"EVYear", "EVMonth", "EVEmployeeId", "EVType"}
                        Dim field = AuditContract.EmployeeValueFields().Single(Function(x) x.StartsWith(name & "|", StringComparison.Ordinal)).Split("|"c)
                        cmd.Parameters.Add("@" & field(1), SqlDbType.Int).Value = Integer.Parse(V(r, name), CultureInfo.InvariantCulture)
                    Next
                    cmd.Parameters.AddWithValue("@Value", Decimal.Parse(V(r, "EVAmount"), CultureInfo.InvariantCulture))
                    cmd.Parameters.Add("@About", SqlDbType.NVarChar, -1).Value = V(r, "EVNotes")
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = r.Context.ActorUserId.Value
                End If
                cmd.ExecuteNonQuery()
            End Using
            AuditRequestCoordinator.RequireTransaction(cn, tx)
            If create Then
                Dim added = AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID FROM dbo.Others_Values WITH(UPDLOCK,HOLDLOCK) WHERE T_ID>@P0 ORDER BY T_ID", maximum)
                If added.Rows.Count <> 1 Then Throw New InvalidOperationException("تعذر تحديد رقم قيمة الموظف الجديدة بشكل منفرد.")
                id = CInt(added.Rows(0)(0))
            End If
            Dim after = Capture(cn, tx, id)
            If after.Rows.Count <> 1 Then Throw New InvalidOperationException("لم يؤكد الإجراء حفظ قيمة الموظف.")
            If cancel Then
                If IsDBNull(after.Rows(0)("isVoid")) OrElse CInt(after.Rows(0)("isVoid")) <> 1 Then Throw New InvalidOperationException("لم يؤكد الإجراء إلغاء القيمة.")
                Dim comparison = after.Copy()
                comparison.Rows(0)("isVoid") = before.Rows(0)("isVoid")
                If AuditSalesQuantity.Fingerprint(comparison) <> AuditSalesQuantity.Fingerprint(before) Then Throw New InvalidOperationException("تغيرت حقول خارج إلغاء القيمة.")
            Else
                Editable(after)
                Dim row = after.Rows(0)
                If CDate(row("DateOF_Order")) <> New SqlTypes.SqlDateTime(DateTime.Parse(V(r, "EVBusinessDate"), CultureInfo.InvariantCulture)).Value Then Throw New InvalidOperationException("تاريخ قيمة الموظف المحفوظ لا يطابق الطلب.")
                If CInt(row("AG_ID")) <> CInt(V(r, "EVEmployeeId")) OrElse CInt(row("ValType")) <> CInt(V(r, "EVType")) OrElse CInt(row("YEAR")) <> CInt(V(r, "EVYear")) OrElse CInt(row("MONTH")) <> CInt(V(r, "EVMonth")) OrElse CDec(row("Value")) <> Decimal.Round(Decimal.Parse(V(r, "EVAmount"), CultureInfo.InvariantCulture), 3, MidpointRounding.AwayFromZero) OrElse CStr(row("About")) <> V(r, "EVNotes") Then Throw New InvalidOperationException("القيمة المحفوظة لا تطابق الطلب.")
                If create Then
                    If CLng(row("User_ID")) <> r.Context.ActorUserId.Value OrElse CInt(row("isVoid")) <> 0 Then Throw New InvalidOperationException("هوية إنشاء القيمة غير مطابقة.")
                Else
                    For Each name In {"Date", "User_ID", "isVoid"}
                        If Not Object.Equals(row(name), before.Rows(0)(name)) Then Throw New InvalidOperationException("تغيرت هوية إنشاء القيمة خارج المسار.")
                    Next
                End If
            End If
            Dim e As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = 1, .EventCode = op, .Outcome = 1, .EntityType = "EmployeeValue", .EntityId = id.ToString(CultureInfo.InvariantCulture),
                .DocumentNumber = id.ToString(CultureInfo.InvariantCulture), .OccurredAtUtc = AuditRequestCoordinator.ServerUtcNow(cn, tx),
                .BusinessDate = If(IsDBNull(after.Rows(0)("DateOF_Order")), CType(Nothing, DateTime?), CDate(after.Rows(0)("DateOF_Order")))}
            For Each definition In AuditContract.EmployeeValueFields()
                Dim p = definition.Split("|"c)
                e.Changes.Add(New AuditFieldChange With {.ChangeNo = e.Changes.Count + 1, .EntityType = e.EntityType, .EntityId = e.EntityId, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1,
                    .Before = If(create, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), before.Rows(0)(p(1)))), .After = AuditValue.Capture(p(2), after.Rows(0)(p(1)))})
            Next
            AuditWriter.WriteInTransaction(cn, tx, lease, e)
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", e.EntityId}})}
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
