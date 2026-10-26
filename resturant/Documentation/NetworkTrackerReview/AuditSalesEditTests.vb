Option Strict On
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq

Friend Module AuditSalesEditTests
    Private checks As Integer
    Private connection As String
    Private context As AuditEventContext
    Private Const FixtureId As Integer = 2147482500

    Public Function Main(args As String()) As Integer
        Try
            context = New AuditEventContext With {.SourceId = Guid.NewGuid(), .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "4A", .ScreenCode = "Sales"}
            Check(AuditContract.Domain("Sales.EditOpened") = "Sales.EditOpening", "narrow operation-specific domain")
            Check(AuditSalesEdit.Request(context, 1).Inputs.Count = 0, "opening never accepts financial changes")
            Reject(Sub() AuditSalesEdit.Request(context, 0), "missing document rejected", "رقم")
            Dim missingSession = context.Copy() : missingSession.SessionId = Nothing
            Reject(Sub() AuditSalesEdit.Request(missingSession, 1), "confirmed login required", "جلسة")
            Dim pending As New AuditSalesEditAttempt()
            Reject(Sub() pending.Open("", context, 1), "unknown connection retains pending request", "غير محسومة")
            Reject(Sub() pending.Open("", context, 2), "unknown request cannot move to another document", "غير محسومة")
            Check(Not pending.CanClose(), "unknown status prevents silently discarding request")
            If args.Length = 2 AndAlso args(0) = "--settings" Then Live(args(1))
            Console.WriteLine("PASS: " & checks & " sales edit opening checks.")
            Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString())
            Return 1
        End Try
    End Function

    Private Sub Check(value As Boolean, name As String)
        If Not value Then Throw New Exception("FAILED: " & name)
        checks += 1 : Console.WriteLine("PASS " & name)
    End Sub

    Private Sub Reject(work As Action, name As String, expected As String)
        Try
            work()
        Catch ex As Exception
            If Not ex.Message.Contains(expected) Then Throw New Exception("Unexpected rejection: " & name, ex)
            Check(True, name) : Return
        End Try
        Throw New Exception("Expected rejection: " & name)
    End Sub

    Private Function Scalar(cn As SqlConnection, tx As SqlTransaction, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn, tx)
            Return cmd.ExecuteScalar()
        End Using
    End Function

    Private Sub Fixture(work As Action(Of SqlConnection, SqlTransaction))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction()
                Try
                    Scalar(cn, tx, "INSERT dbo.Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Discount,Pure,SB_ID,NeedRepost) VALUES(" & FixtureId & ",1,GETDATE(),1,1,0,1,100,5,95," & FixtureId & ",0)")
                    work(cn, tx)
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
    End Sub

    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim b As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS Audit4A rollback", .ConnectTimeout = 5}
        If b.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database.")
        connection = b.ConnectionString
        Dim tables = {"Agents_Balance_MV", "SB_Contents", "Treasury_Balance_MV", "Network_Edit_Tracker", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change"}
        Dim before As Long(), domains As String
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND database_id=DB_ID() AND session_id<>@@SPID")) = 0, "no other clients")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID IN(" & FixtureId & "," & (FixtureId - 1) & ")")) = 0, "fixture and missing-document keys unused")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE parent_id IN(OBJECT_ID('dbo.Agents_Balance_MV'),OBJECT_ID('dbo.Users'),OBJECT_ID('dbo.Network_Audit_Event')) AND is_disabled=0")) = 0, "no unreviewed triggers")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Users u JOIN dbo.Users_Validations_V v ON v.User_ID=u.User_ID WHERE u.User_ID=1 AND u.is_Allow=1 AND v.SB_Update=1")) = 1, "test actor has actual sales-edit permission")
            context.SourceId = CType(Scalar(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid)
            domains = CStr(Scalar(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1"))
            before = tables.Select(Function(t) CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & t))).ToArray()
        End Using
        Fixture(Sub(cn, tx)
                    Dim request = AuditSalesEdit.Request(context, FixtureId)
                    Check(AuditSalesEdit.ExecuteInTransaction(cn, tx, request).Status = "NewPendingCommit", "open edit and event are pending the same commit")
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=" & FixtureId & " AND EditOpenedBy=1 AND EditOpenedAt IS NOT NULL AND NeedRepost=0 AND Total=100 AND Discount=5 AND Pure=95 AND isDepended=1 AND isVoid=0")) = 1, "actual actor and time captured without financial changes")
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & request.RequestId.ToString() & "' AND c.BeforeState IN(2,3) AND c.AfterState IN(2,3)")) = 10, "ten actual before and after fields")
                    Check(AuditSalesEdit.ExecuteInTransaction(cn, tx, request).Status = "Completed", "retry resolves to original operation")
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & request.RequestId.ToString() & "'")) = 1, "retry creates only one event")
                End Sub)
        Fixture(Sub(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET JournalId=2147482501,OriginalJournalId=2147482502 WHERE T_ID=" & FixtureId)
                    AuditSalesEdit.ExecuteInTransaction(cn, tx, AuditSalesEdit.Request(context, FixtureId))
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=" & FixtureId & " AND NeedRepost=1 AND OriginalJournalId=2147482502 AND JournalId=2147482501")) = 1, "existing accounting repost semantics preserved")
                End Sub)
        Fixture(Sub(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.Users SET is_Allow=0 WHERE User_ID=1")
                    Reject(Sub() AuditSalesEdit.ExecuteInTransaction(cn, tx, AuditSalesEdit.Request(context, FixtureId)), "blocked actor rejected within transaction", "صلاحية")
                End Sub)
        Fixture(Sub(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isVoid=1 WHERE T_ID=" & FixtureId)
                    Reject(Sub() AuditSalesEdit.ExecuteInTransaction(cn, tx, AuditSalesEdit.Request(context, FixtureId)), "void invoice cannot open", "ملغاة")
                End Sub)
        Fixture(Sub(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isDepended=0 WHERE T_ID=" & FixtureId)
                    Reject(Sub() AuditSalesEdit.ExecuteInTransaction(cn, tx, AuditSalesEdit.Request(context, FixtureId)), "unapproved invoice rejected", "معتمدة")
                End Sub)
        Fixture(Sub(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET BsType_ID=7 WHERE T_ID=" & FixtureId)
                    Reject(Sub() AuditSalesEdit.ExecuteInTransaction(cn, tx, AuditSalesEdit.Request(context, FixtureId)), "wrong invoice type rejected", "نوع آخر")
                End Sub)
        Fixture(Sub(cn, tx)
                    Reject(Sub() AuditSalesEdit.ExecuteInTransaction(cn, tx, AuditSalesEdit.Request(context, FixtureId - 1)), "missing invoice rejected", "غير موجودة")
                End Sub)
        Fixture(Sub(cn, tx)
                    Scalar(cn, tx, "CREATE TRIGGER dbo.AuditTest4AReject ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN THROW 51106,'Intentional Audit4A failure',1; END")
                    Reject(Sub() AuditSalesEdit.ExecuteInTransaction(cn, tx, AuditSalesEdit.Request(context, FixtureId)), "event failure rolls back edit opening", "Intentional Audit4A")
                End Sub)
        Using cn As New SqlConnection(connection)
            cn.Open()
            For i = 0 To tables.Length - 1
                Check(CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & tables(i))) = before(i), "rollback preserved " & tables(i))
            Next
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=" & FixtureId)) = 0, "fixture header fully rolled back")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE name='AuditTest4AReject'")) = 0, "failure trigger rolled back")
            Check(CStr(Scalar(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = domains, "activation unchanged by tests")
            Check(CInt(Scalar(cn, Nothing, "SELECT is_Allow FROM dbo.Users WHERE User_ID=1")) = 1, "actor activation restored")
        End Using
    End Sub
End Module
