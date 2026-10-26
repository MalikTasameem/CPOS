Option Strict On
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq

Friend Module AuditDocumentEditTests
    Private checks As Integer
    Private connection As String
    Private context As AuditEventContext
    Private Const FixtureId As Integer = 2147482400
    Private ReadOnly Kinds As Integer() = {2, 7, 8, 9, 10, 11, 12, 13, 17, 18, 35}

    Public Function Main(args As String()) As Integer
        Try
            context = New AuditEventContext With {.SourceId = Guid.NewGuid(), .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "4B", .ScreenCode = "Pch"}
            Check(AuditContract.Domain(AuditDocumentEdit.OperationCode) = "Documents.EditOpening", "narrow opening-only domain")
            For Each kind In Kinds
                Dim spec = AuditDocumentEditSpec.Find(kind)
                For Each screen In spec.Screens
                    context.ScreenCode = screen
                    Dim request = AuditDocumentEdit.Request(context, FixtureId, kind)
                    Check(request.EntityType = spec.EntityType AndAlso request.Inputs.Count = 1 AndAlso request.Inputs(0).Value.Text = kind.ToString(), "closed type and screen mapping " & kind & " / " & screen)
                    Dim item As New AuditEventRequest With {.EventCode = request.OperationCode, .EntityType = spec.EntityType, .EntityId = request.EntityId, .DocumentNumber = "123"}
                    Check(AuditMessageFormatter.Format(request, item).Contains("لا يعني حفظ"), "message distinguishes opening from saving " & screen)
                Next
            Next
            context.ScreenCode = "Pch"
            Reject(Sub() AuditDocumentEdit.Request(context, 0, 7), "missing document ID rejected", "رقم")
            Reject(Sub() AuditDocumentEdit.Request(context, 1, 1), "sales cannot bypass its dedicated path", "غير معتمد")
            Reject(Sub() AuditDocumentEdit.Request(context, 1, 10), "screen/type mismatch rejected", "الشاشة")
            Dim noSession = context.Copy() : noSession.SessionId = Nothing
            Reject(Sub() AuditDocumentEdit.Request(noSession, 1, 7), "confirmed session required", "جلسة")
            Dim input = AuditDocumentEdit.Request(context, 1, 7)
            input.Inputs.Add(New AuditInput With {.Name = "Discount", .ValueType = "Decimal", .Value = AuditValue.Capture("Decimal", 5D)})
            Reject(Sub() AuditContract.Request(input), "no financial mutations in opening inputs", "نوع المستند فقط")
            Dim attempt As New AuditDocumentEditAttempt()
            Reject(Sub() attempt.Open("", context, 1, 7), "unknown connection retains pending request", "غير محسومة")
            Reject(Sub() attempt.Open("", context, 2, 7), "pending request cannot change document", "غير محسومة")
            Dim other = context.Copy() : other.ActorUserId = 2
            Reject(Sub() attempt.Open("", other, 1, 7), "pending request cannot change actor", "غير محسومة")
            other = context.Copy() : other.SessionId = Guid.NewGuid()
            Reject(Sub() attempt.Open("", other, 1, 7), "pending request cannot change session", "غير محسومة")
            Check(Not attempt.CanClose(), "unknown result cannot be silently discarded")
            If args.Length = 2 AndAlso args(0) = "--settings" Then Live(args(1))
            Console.WriteLine("PASS: " & checks & " document edit opening checks.")
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

    Private Sub Fixture(kind As Integer, work As Action(Of SqlConnection, SqlTransaction))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction()
                Try
                    Dim spec = AuditDocumentEditSpec.Find(kind)
                    Scalar(cn, tx, "INSERT dbo.Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Discount,Pure,[" & spec.NumberColumn & "],NeedRepost) VALUES(" & FixtureId & ",1,GETDATE()," & kind & ",1,0,1,100,5,95," & FixtureId & ",0)")
                    work(cn, tx)
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
    End Sub

    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim b As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS Audit4B rollback", .ConnectTimeout = 5}
        If b.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database.")
        connection = b.ConnectionString
        Dim tables = {"Agents_Balance_MV", "SB_Contents", "Treasury_Balance_MV", "Network_Edit_Tracker", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change"}
        Dim before As Long(), domains As String, business As String
        Dim permissions = Kinds.Select(Function(k) AuditDocumentEditSpec.Find(k).PermissionColumn).Distinct().ToArray()
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND database_id=DB_ID() AND session_id<>@@SPID")) = 0, "no other clients")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID IN(" & FixtureId & "," & (FixtureId - 1) & ")")) = 0, "fixture keys unused")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE parent_id IN(OBJECT_ID('dbo.Agents_Balance_MV'),OBJECT_ID('dbo.Users'),OBJECT_ID('dbo.Users_Validations'),OBJECT_ID('dbo.Network_Audit_Event')) AND is_disabled=0")) = 0, "no unreviewed triggers")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Users u JOIN dbo.Users_Validations_V v ON v.User_ID=u.User_ID WHERE u.User_ID=1 AND u.Valid_ID=1 AND u.is_Allow=1 AND " & String.Join(" AND ", permissions.Select(Function(p) "v.[" & p & "]=1")))) = 1, "actual permissions and role key confirmed")
            context.SourceId = CType(Scalar(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid)
            domains = CStr(Scalar(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1"))
            business = CStr(Scalar(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_Balance_MV_OpenForEdit'))"))
            before = tables.Select(Function(t) CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & t))).ToArray()
        End Using
        For Each kind In Kinds
            Dim spec = AuditDocumentEditSpec.Find(kind)
            For Each screen In spec.Screens
                context.ScreenCode = screen
                Fixture(kind, Sub(cn, tx)
                                  Dim request = AuditDocumentEdit.Request(context, FixtureId, kind)
                                  Check(AuditDocumentEdit.ExecuteInTransaction(cn, tx, request).Status = "NewPendingCommit", "same transaction " & screen & " / " & kind)
                                  Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=" & FixtureId & " AND EditOpenedBy=1 AND EditOpenedAt IS NOT NULL AND NeedRepost=0 AND Total=100 AND Discount=5 AND Pure=95 AND isDepended=1 AND isVoid=0")) = 1, "actual actor/time; financial values unchanged " & screen)
                                  Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & request.RequestId.ToString() & "' AND c.BeforeState IN(2,3) AND c.AfterState IN(2,3) AND c.ValueOrigin=1 AND c.EntityType='" & spec.EntityType & "'")) = 12, "twelve actual fields " & screen)
                                  Check(AuditDocumentEdit.ExecuteInTransaction(cn, tx, request).Status = "Completed", "same request replays " & screen)
                                  Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & request.RequestId.ToString() & "'")) = 1, "one event after retry " & screen)
                              End Sub)
            Next
            context.ScreenCode = spec.Screens(0)
            Fixture(kind, Sub(cn, tx)
                              Scalar(cn, tx, "UPDATE dbo.Users_Validations SET [" & spec.PermissionColumn & "]=0 WHERE T_ID=1")
                              Reject(Sub() AuditDocumentEdit.ExecuteInTransaction(cn, tx, AuditDocumentEdit.Request(context, FixtureId, kind)), "denied permission " & kind, "صلاحية")
                              Check(IsDBNull(Scalar(cn, tx, "SELECT EditOpenedAt FROM dbo.Agents_Balance_MV WHERE T_ID=" & FixtureId)), "permission denial did not open " & kind)
                          End Sub)
        Next
        context.ScreenCode = "Pch"
        Fixture(7, Sub(cn, tx)
                       Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET JournalId=2147482401,OriginalJournalId=2147482402 WHERE T_ID=" & FixtureId)
                       AuditDocumentEdit.ExecuteInTransaction(cn, tx, AuditDocumentEdit.Request(context, FixtureId, 7))
                       Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=" & FixtureId & " AND NeedRepost=1 AND JournalId=2147482401 AND OriginalJournalId=2147482402")) = 1, "original repost semantics preserved")
                   End Sub)
        Fixture(7, Sub(cn, tx)
                       Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isDepended=0 WHERE T_ID=" & FixtureId)
                       AuditDocumentEdit.ExecuteInTransaction(cn, tx, AuditDocumentEdit.Request(context, FixtureId, 7))
                       Check(CInt(Scalar(cn, tx, "SELECT isDepended FROM dbo.Agents_Balance_MV WHERE T_ID=" & FixtureId)) = 0, "persisted unapproved header retains its state")
                   End Sub)
        Fixture(7, Sub(cn, tx)
                       Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isVoid=1 WHERE T_ID=" & FixtureId)
                       Reject(Sub() AuditDocumentEdit.ExecuteInTransaction(cn, tx, AuditDocumentEdit.Request(context, FixtureId, 7)), "void document rejected", "ملغى")
                   End Sub)
        Fixture(7, Sub(cn, tx)
                       Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET BsType_ID=10 WHERE T_ID=" & FixtureId)
                       Reject(Sub() AuditDocumentEdit.ExecuteInTransaction(cn, tx, AuditDocumentEdit.Request(context, FixtureId, 7)), "actual type mismatch rejected", "نوع المستند الفعلي")
                   End Sub)
        Fixture(7, Sub(cn, tx)
                       Reject(Sub() AuditDocumentEdit.ExecuteInTransaction(cn, tx, AuditDocumentEdit.Request(context, FixtureId - 1, 7)), "missing actual document rejected", "غير موجود")
                   End Sub)
        Fixture(7, Sub(cn, tx)
                       Scalar(cn, tx, "UPDATE dbo.Users SET is_Allow=0 WHERE User_ID=1")
                       Reject(Sub() AuditDocumentEdit.ExecuteInTransaction(cn, tx, AuditDocumentEdit.Request(context, FixtureId, 7)), "disabled actor rejected", "صلاحية")
                   End Sub)
        Fixture(7, Sub(cn, tx)
                       Scalar(cn, tx, "CREATE TRIGGER dbo.AuditTest4BReject ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN THROW 51106,'Intentional Audit4B failure',1; END")
                       Reject(Sub() AuditDocumentEdit.ExecuteInTransaction(cn, tx, AuditDocumentEdit.Request(context, FixtureId, 7)), "writer failure prevents success", "Intentional Audit4B")
                       Dim state = CInt(Scalar(cn, tx, "SELECT XACT_STATE()"))
                       Check(state <= 0, "writer failure leaves no committable transaction (state=" & state & ")")
                       If state = 0 Then Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=" & FixtureId)) = 0, "server rollback also removed the edit fixture")
                   End Sub)
        Using cn As New SqlConnection(connection)
            cn.Open()
            For i = 0 To tables.Length - 1
                Check(CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & tables(i))) = before(i), "rollback preserved " & tables(i))
            Next
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=" & FixtureId)) = 0, "fixture header removed by rollback")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE name='AuditTest4BReject'")) = 0, "test failure trigger rolled back")
            Check(CStr(Scalar(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = domains, "activation unchanged by tests")
            Check(CStr(Scalar(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_Balance_MV_OpenForEdit'))")) = business, "business procedure unchanged")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Users u JOIN dbo.Users_Validations_V v ON v.User_ID=u.User_ID WHERE u.User_ID=1 AND u.is_Allow=1 AND " & String.Join(" AND ", permissions.Select(Function(p) "v.[" & p & "]=1")))) = 1, "actor and all permission flags restored")
        End Using
    End Sub
End Module
