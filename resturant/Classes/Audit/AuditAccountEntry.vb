Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

' Manual account entries only: types 5 (salary), 6 (credit) and 37 (debit).
Friend NotInheritable Class AuditAccountEntry
    Friend Const Operation As String = "AccountEntry.Created"
    Friend Shared Function V(r As AuditOperationRequest, name As String) As String
        Return r.Inputs.Single(Function(x) x.Name = name).Value.Text
    End Function
    Friend Shared Function Request(context As AuditEventContext, command As SqlCommand) As AuditOperationRequest
        If Not command.CommandText.Trim("["c, "]"c).Equals("Agents_insert_Prev_Balance", StringComparison.OrdinalIgnoreCase) Then Throw New ArgumentException("إجراء حركة حساب غير معتمد.")
        If CInt(AuditFinance.Parameter(command, "User_ID")) <> context.ActorUserId.Value Then Throw New ArgumentException("المستخدم لا يطابق الجلسة.")
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = Operation, .EntityType = "AccountEntry", .EntityId = "New"}
        For Each pair In {"AccountId|AG_ID", "DocumentType|Type_ID", "DocumentPure|Balance", "DocumentDate|Date", "EntryTitle|NOTES"}
            Dim p = pair.Split("|"c), type = AuditContract.FieldType(Operation, p(0)), value = AuditFinance.Parameter(command, p(1))
            If type = "DateTime" Then value = DateTime.SpecifyKind(CDate(value), DateTimeKind.Unspecified)
            If type = "Decimal" Then value = Convert.ToDecimal(value, CultureInfo.CurrentCulture)
            r.Inputs.Add(New AuditInput With {.Name = p(0), .ValueType = type, .Value = AuditValue.Capture(type, value)})
        Next
        AuditContract.Request(r) : Return r
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, context As AuditEventContext)
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@Id AND u.is_Allow=1 AND v.Agents=1", cn, tx)
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("لا تملك صلاحية إضافة حركة من بطاقة الحساب.")
        End Using
    End Sub

    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, id As Integer) As DataTable
        Dim columns = AuditContract.AccountEntryFields().Select(Function(x) x.Split("|"c)(1)).Where(Function(x) x <> "AuditDocumentNumber").Select(Function(x) "[" & x & "]")
        Return AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID,T_ID AS AuditDocumentNumber," & String.Join(",", columns) & " FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@P0", id)
    End Function

    Private Shared Function Allocate(cn As SqlConnection, tx As SqlTransaction) As Integer
        Dim ranges = AuditSalesQuantity.Rows(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)")
        If ranges.Rows.Count <> 1 OrElse IsDBNull(ranges.Rows(0)(0)) OrElse IsDBNull(ranges.Rows(0)(1)) Then Throw New InvalidOperationException("نطاق ترقيم الحساب غير محدد.")
        Dim first = CInt(ranges.Rows(0)(0)), last = CInt(ranges.Rows(0)(1))
        Dim maximum = AuditSalesQuantity.Rows(cn, tx, "SELECT ISNULL(MAX(T_ID),@P0) FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID BETWEEN @P0 AND @P1", first, last)
        If first < 0 OrElse last <= first OrElse CLng(maximum.Rows(0)(0)) >= last Then Throw New InvalidOperationException("نطاق ترقيم حركات الحساب ممتلئ.")
        Return CInt(maximum.Rows(0)(0)) + 1
    End Function

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse request.OperationCode <> Operation Then Throw New ArgumentException("يلزم عقد حركة حساب ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, account = CInt(V(r, "AccountId")), kind = CInt(V(r, "DocumentType"))
            Permission(cn, tx, r.Context)
            If AuditSalesQuantity.Rows(cn, tx, "SELECT AG_ID FROM dbo.Agents WITH(HOLDLOCK) WHERE AG_ID=@P0", account).Rows.Count <> 1 Then Throw New InvalidOperationException("الحساب غير موجود.")
            Dim id = Allocate(cn, tx)
            If Capture(cn, tx, id).Rows.Count <> 0 Then Throw New InvalidOperationException("رقم الحركة مستخدم بالفعل.")
            Using cmd = AuditRequestCoordinator.Command("dbo.Agents_insert_Prev_Balance", cn, tx)
                cmd.Parameters.Add("@AG_ID", SqlDbType.Int).Value = account
                cmd.Parameters.Add("@Type_ID", SqlDbType.Int).Value = kind
                cmd.Parameters.AddWithValue("@Balance", Decimal.Parse(V(r, "DocumentPure"), CultureInfo.InvariantCulture))
                cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Parse(V(r, "DocumentDate"), CultureInfo.InvariantCulture)
                cmd.Parameters.Add("@NOTES", SqlDbType.NVarChar, -1).Value = V(r, "EntryTitle")
                cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = r.Context.ActorUserId.Value
                cmd.ExecuteNonQuery()
            End Using
            AuditRequestCoordinator.RequireTransaction(cn, tx)
            Dim after = Capture(cn, tx, id)
            If after.Rows.Count <> 1 Then Throw New InvalidOperationException("لم يؤكد الإجراء إنشاء حركة الحساب.")
            Dim row = after.Rows(0), amount = Decimal.Round(Decimal.Parse(V(r, "DocumentPure"), CultureInfo.InvariantCulture), 3, MidpointRounding.AwayFromZero)
            If CInt(row("BsType_ID")) <> kind OrElse CInt(row("AG_ID")) <> account OrElse CInt(row("User_ID")) <> r.Context.ActorUserId.Value OrElse CInt(row("isVoid")) <> 0 OrElse CInt(row("isDepended")) <> 1 Then Throw New InvalidOperationException("حالة حركة الحساب المنشأة غير مطابقة.")
            If CDec(row("Total")) <> amount OrElse CDec(row("Pure")) <> amount OrElse CDec(row(If(kind = 6, "Credit", "Debit"))) <> amount OrElse (Not IsDBNull(row(If(kind = 6, "Debit", "Credit"))) AndAlso CDec(row(If(kind = 6, "Debit", "Credit"))) <> 0D) Then Throw New InvalidOperationException("طرفا حركة الحساب غير مطابقين.")
            Dim e As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = 1, .EventCode = Operation, .Outcome = 1, .EntityType = "AccountEntry", .EntityId = id.ToString(CultureInfo.InvariantCulture), .DocumentNumber = id.ToString(CultureInfo.InvariantCulture), .BusinessDate = CDate(row("Date")), .OccurredAtUtc = AuditRequestCoordinator.ServerUtcNow(cn, tx)}
            For Each field In AuditContract.AccountEntryFields()
                Dim p = field.Split("|"c)
                e.Changes.Add(New AuditFieldChange With {.ChangeNo = e.Changes.Count + 1, .EntityType = e.EntityType, .EntityId = e.EntityId, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1, .Before = AuditValue.Missing(AuditValueState.Absent), .After = AuditValue.Capture(p(2), row(p(1)))})
            Next
            AuditWriter.WriteInTransaction(cn, tx, lease, e)
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", e.EntityId}})}
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
