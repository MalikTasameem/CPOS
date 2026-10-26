Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Xml.Linq

Friend Module AuditSecurityTests
    Private assertions As Integer
    Private ReadOnly fixedUtc As New DateTime(2026, 10, 24, 12, 0, 0, DateTimeKind.Utc)

    Public Function Main(args As String()) As Integer
        Try
            UnitTests()
            Dim settings As String = Nothing, output As String = "."
            For i = 0 To args.Length - 2
                If args(i) = "--settings" Then settings = args(i + 1)
                If args(i) = "--output" Then output = args(i + 1)
            Next
            If settings IsNot Nothing Then LiveTests(settings, output)
            Console.WriteLine("PASS: " & assertions & " security checks.")
            Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.GetType().Name & ": " & ex.Message & Environment.NewLine & ex.StackTrace)
            Return 1
        End Try
    End Function

    Private Sub Check(ok As Boolean, name As String)
        If Not ok Then Throw New Exception("FAILED: " & name)
        assertions += 1
        Console.WriteLine("PASS " & name)
    End Sub

    Private Sub Reject(action As Action, name As String)
        Try
            action()
        Catch ex As InvalidOperationException
            Check(True, name)
            Return
        End Try
        Throw New Exception("Expected failure: " & name)
    End Sub

    Private Class FakeStore
        Implements IAuditSecurityStore
        Friend Enabled As Boolean = True
        Friend PolicyFails As Boolean
        Friend FailBefore As Boolean
        Friend LoseReply As Boolean
        Friend StatusFails As Boolean
        Friend BlockStatus As Boolean
        Friend FailEnd As Boolean
        Friend ReadOnly Writes As New List(Of AuditOperationRequest)()
        Friend ReadOnly Events As New List(Of AuditEventRequest)()
        Friend ReadOnly Completed As New Dictionary(Of Guid, AuditRequestResult)()
        Public Function Policy(connection As String) As AuditSecurityPolicy Implements IAuditSecurityStore.Policy
            If PolicyFails Then Throw New InvalidOperationException("test policy unavailable")
            Return New AuditSecurityPolicy With {.Enabled = Enabled, .SourceId = Guid.Parse("11111111-1111-1111-1111-111111111111")}
        End Function
        Public Function UtcNow(connection As String) As DateTime Implements IAuditSecurityStore.UtcNow
            Return fixedUtc
        End Function
        Public Function Write(connection As String, request As AuditOperationRequest, item As AuditEventRequest) As AuditRequestResult Implements IAuditSecurityStore.Write
            AuditCanonical.RequestBytes(request)
            AuditContract.EventData(request, item)
            Writes.Add(request)
            Events.Add(item)
            If FailBefore OrElse Not Enabled OrElse (FailEnd AndAlso request.OperationCode = "Security.SessionEnded") Then Throw New InvalidOperationException("test write failure")
            If Not Completed.ContainsKey(request.RequestId) Then Completed.Add(request.RequestId, New AuditRequestResult With {.Status = "Completed", .ResultCode = item.Outcome})
            If LoseReply Then Throw New InvalidOperationException("test lost commit reply")
            Return Completed(request.RequestId)
        End Function
        Public Function Status(connection As String, request As AuditOperationRequest) As AuditRequestResult Implements IAuditSecurityStore.Status
            If StatusFails Then Throw New InvalidOperationException("test status unavailable")
            If BlockStatus Then Return New AuditRequestResult With {.Status = "InProgressOrUnknown"}
            If Completed.ContainsKey(request.RequestId) Then Return Completed(request.RequestId)
            Return New AuditRequestResult With {.Status = "NotFound"}
        End Function
    End Class

    Private Sub Start(session As AuditSecuritySession, Optional userId As Integer = 1)
        session.Start("test", userId, "مستخدم اختبار", "login", "test", "3A")
    End Sub

    Private Sub UnitTests()
        Dim storage As New FakeStore With {.Enabled = False}
        Dim session As New AuditSecuritySession(storage)
        Start(session)
        Check(storage.Writes.Count = 0, "explicitly disabled policy does not write")
        Reject(Sub() session.ContextFor("test", 1, "Settlement"), "disabled policy creates no fictional audited session")
        storage.Enabled = True
        Start(session)
        Dim context = session.ContextFor("test", 1, "Settlement")
        Check(context.SessionId.HasValue AndAlso context.ActorUserId.GetValueOrDefault() = 1, "start confirmed before session is published")
        context.ActorUserId = 99
        Check(session.ContextFor("test", 1, "Settlement").ActorUserId.GetValueOrDefault() = 1, "context is an isolated copy")
        Reject(Sub() session.ContextFor("test", 2, "Settlement"), "stale global user cannot reuse session")
        Reject(Sub() session.ContextFor("other", 1, "Settlement"), "different database cannot reuse session")
        Dim sessionId = session.ContextFor("test", 1, "Settlement").SessionId.Value
        Check(session.RejectLogin("test", Nothing, Nothing, "login", "test", "3A") Is Nothing, "unknown login rejection saved")
        Dim unknown = storage.Writes.Last().Context
        Check(unknown.ActorKind = AuditActorKind.Unknown AndAlso Not unknown.ActorUserId.HasValue AndAlso unknown.ActorName Is Nothing AndAlso Not unknown.SessionId.HasValue, "unknown rejection never inherits old user or session")
        session.RejectLogin("test", 2, "ممنوع", "Saleslogin", "test", "3A")
        Check(storage.Writes.Last().Context.ActorUserId.GetValueOrDefault() = 2 AndAlso Not storage.Writes.Last().Context.SessionId.HasValue, "known rejection has candidate identity but no session")
        Check(session.ContextFor("test", 1, "Settlement").SessionId.Value = sessionId, "rejection preserves previous active session")
        For Each reason In {"NoOpenPeriod", "SalesPermissionDenied"}
            Check(session.RejectLogin("test", 2, "مستخدم اختبار", "Saleslogin", "test", "3A", reason) Is Nothing, "specific POS rejection accepted " & reason)
            Check(storage.Events.Last().ReasonCode = reason AndAlso AuditMessageFormatter.Format(storage.Writes.Last(), storage.Events.Last()).Contains(If(reason = "NoOpenPeriod", "وردية", "صلاحية")), "POS rejection message explains the actual restriction")
        Next
        Check(storage.Writes.All(Function(x) x.Inputs.All(Function(i) i.Name = "ReasonCode")), "security API accepts no credential data")
        Start(session, 2)
        Check(storage.Writes.Last().OperationCode = "Security.SessionEnded" AndAlso storage.Writes.Last().Context.SessionId.GetValueOrDefault() = sessionId, "user switch ends the old actor snapshot")
        Check(session.ContextFor("test", 2, "Settlement").SessionId.Value <> sessionId, "user switch gets a distinct session")
        session.Finish("MainForm")
        Dim count = storage.Writes.Count
        Check(session.Finish("login") Is Nothing AndAlso storage.Writes.Count = count, "duplicate exit hooks do not duplicate end event")
        Reject(Sub() session.ContextFor("test", 2, "Settlement"), "ended session cannot be reused")
        Start(session)
        session.Abandon()
        count = storage.Writes.Count
        session.Finish("login")
        Check(storage.Writes.Count = count, "OS shutdown does not fabricate explicit logout")
        Start(session)
        storage.FailBefore = True
        Reject(Sub() Start(session, 2), "failed user switch cannot activate candidate")
        Check(session.ContextFor("test", 1, "Settlement").ActorUserId.GetValueOrDefault() = 1, "failed user switch keeps old confirmed actor")

        storage = New FakeStore With {.FailBefore = True}
        session = New AuditSecuritySession(storage)
        Reject(Sub() Start(session), "write failure blocks login")
        Reject(Sub() session.ContextFor("test", 1, "Settlement"), "failed start never publishes session")
        Dim requestId = storage.Writes.Single().RequestId, eventId = storage.Events.Single().EventId
        storage.Enabled = False
        Reject(Sub() Start(session), "disabling domain cannot bypass unresolved start")
        Check(storage.Writes.Last().RequestId = requestId, "disabled retry keeps original request identity")
        storage.Enabled = True
        Reject(Sub() Start(session, 2), "unresolved start cannot be replaced by another user")
        storage.BlockStatus = True
        Reject(Sub() Start(session), "unknown status does not replay")
        Check(storage.Writes.Count = 2, "in-progress request was not executed again")
        storage.BlockStatus = False
        storage.StatusFails = True
        Reject(Sub() Start(session), "status connection failure is not interpreted as NotFound")
        storage.StatusFails = False
        storage.FailBefore = False
        Start(session)
        Check(storage.Writes.Last().RequestId = requestId AndAlso storage.Events.Last().EventId = eventId AndAlso storage.Events.Last().OccurredAtUtc = fixedUtc, "safe retry retains request event and time")
        Check(storage.Completed.Count = 1, "retry stores exactly one start")
        storage.FailEnd = True
        Check(session.Finish("MainForm") IsNot Nothing, "end failure is returned as warning")
        Reject(Sub() session.ContextFor("test", 1, "Settlement"), "end failure still clears local session")

        storage = New FakeStore With {.LoseReply = True}
        session = New AuditSecuritySession(storage)
        Start(session)
        Check(storage.Writes.Count = 1 AndAlso session.ContextFor("test", 1, "Settlement").SessionId.HasValue, "lost commit reply recovered by same identity")
        storage.FailBefore = True
        Check(session.RejectLogin("test", Nothing, Nothing, "login", "test", "3A") IsNot Nothing, "audit failure never changes rejection into acceptance")
        storage = New FakeStore With {.PolicyFails = True}
        session = New AuditSecuritySession(storage)
        Reject(Sub() Start(session), "unavailable policy fails closed rather than falling back")
        Check(storage.Writes.Count = 0, "unavailable policy has no write side effect")
    End Sub

    Private Class RecordingStore
        Implements IAuditSecurityStore
        Friend ReadOnly Inner As New SqlAuditSecurityStore()
        Friend ReadOnly Requests As New List(Of AuditOperationRequest)()
        Friend Manifest As String
        Friend OriginalDomains As String
        Friend Source As Guid
        Friend LoseReply As Boolean
        Friend Sub Save(status As String)
            Dim document As New XDocument(New XElement("AuditSecurityTestRecovery", New XAttribute("database", "CPOS_ACCOUNTING"), New XAttribute("source", Source), New XAttribute("status", status),
                New XElement("OriginalDomains", OriginalDomains), New XElement("Requests", Requests.Select(Function(x) New XElement("Request", New XAttribute("id", x.RequestId))))))
            document.Save(Manifest)
        End Sub
        Public Function Policy(connection As String) As AuditSecurityPolicy Implements IAuditSecurityStore.Policy
            Return Inner.Policy(connection)
        End Function
        Public Function UtcNow(connection As String) As DateTime Implements IAuditSecurityStore.UtcNow
            Return Inner.UtcNow(connection)
        End Function
        Public Function Write(connection As String, request As AuditOperationRequest, item As AuditEventRequest) As AuditRequestResult Implements IAuditSecurityStore.Write
            If Not Requests.Any(Function(x) x.RequestId = request.RequestId) Then Requests.Add(request)
            Save("Running")
            Dim result = Inner.Write(connection, request, item)
            If LoseReply Then Throw New InvalidOperationException("test: lost reply after real audit Commit")
            Return result
        End Function
        Public Function Status(connection As String, request As AuditOperationRequest) As AuditRequestResult Implements IAuditSecurityStore.Status
            Return Inner.Status(connection, request)
        End Function
    End Class

    Private Function Scalar(cn As SqlConnection, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn)
            Return cmd.ExecuteScalar()
        End Using
    End Function

    Private Sub LiveTests(settings As String, output As String)
        Dim line = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim builder As New SqlConnectionStringBuilder(line.Substring("SqlConStr:".Length)) With {.InitialCatalog = "CPOS_ACCOUNTING", .ApplicationName = "CPOS Audit 3A Tests", .ConnectTimeout = 5}
        Dim connection = builder.ConnectionString
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CStr(Scalar(cn, "SELECT DB_NAME()")) = "CPOS_ACCOUNTING", "test database verified")
            Dim storage As New RecordingStore With {.Source = CType(Scalar(cn, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid),
                .OriginalDomains = CStr(Scalar(cn, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1")),
                .Manifest = Path.Combine(output, "Recovery_" & Guid.NewGuid().ToString("N") & ".xml")}
            If AuditCanonical.ParseXml(storage.OriginalDomains).Elements().Any() Then Throw New InvalidOperationException("Tests require all domains disabled; do not run during an active pilot.")
            Dim legacyRows = CInt(Scalar(cn, "SELECT COUNT(*) FROM dbo.Network_Edit_Tracker"))
            storage.Save("Prepared")
            Try
                Check(Not storage.Policy(connection).Enabled, "live disabled policy recognized")
                Scalar(cn, "UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml=N'<Domains><Domain code=""Security""/></Domains>' WHERE ConfigId=1")
                Check(storage.Policy(connection).Enabled, "only Security enabled temporarily")
                Dim session As New AuditSecuritySession(storage)
                Check(session.RejectLogin(connection, Nothing, Nothing, "login", "AuditTests", "3A") Is Nothing, "live unknown rejection")
                Check(session.RejectLogin(connection, 2147483645, "AuditTest forbidden", "Saleslogin", "AuditTests", "3A") Is Nothing, "live known rejection")
                storage.LoseReply = True
                Check(session.Start(connection, 2147483645, "AuditTest synthetic", "login", "AuditTests", "3A") Is Nothing, "live lost-commit reply recovered without duplicate")
                storage.LoseReply = False
                Dim firstSession = session.ContextFor(connection, 2147483645, "Settlement").SessionId.Value
                Check(session.Start(connection, 2147483644, "AuditTest switched", "Saleslogin", "AuditTests", "3A") Is Nothing, "live user switch start and previous end")
                Dim secondSession = session.ContextFor(connection, 2147483644, "Settlement").SessionId.Value
                Check(firstSession <> secondSession, "live distinct sessions")
                Check(session.Finish("MainForm") Is Nothing, "live explicit end")
                session.Finish("login")
                Check(storage.Requests.Count = 6, "six distinct security requests no duplicate exit")
                For Each request In storage.Requests
                    Check(storage.Status(connection, request).Status = "Completed", "live request completed " & request.OperationCode)
                    Using cmd As New SqlCommand("SELECT ActorKind,ActorUserId,ActorName,SessionId,EventCode,Outcome,ScreenCode FROM dbo.Network_Audit_Event WHERE SourceId=@Source AND RequestId=@Request", cn)
                        cmd.Parameters.Add("@Source", SqlDbType.UniqueIdentifier).Value = storage.Source
                        cmd.Parameters.Add("@Request", SqlDbType.UniqueIdentifier).Value = request.RequestId
                        Using reader = cmd.ExecuteReader()
                            Check(reader.Read() AndAlso CStr(reader("EventCode")) = request.OperationCode AndAlso CStr(reader("ScreenCode")) = request.Context.ScreenCode, "stored event code and entry screen")
                            If request.Context.ActorKind = AuditActorKind.Unknown Then
                                Check(IsDBNull(reader("ActorUserId")) AndAlso IsDBNull(reader("ActorName")) AndAlso IsDBNull(reader("SessionId")), "live unknown has no old identity")
                            ElseIf request.OperationCode = "Security.LoginRejected" Then
                                Check(CInt(reader("ActorUserId")) = request.Context.ActorUserId.Value AndAlso IsDBNull(reader("SessionId")), "live rejected user has no session")
                            Else
                                Check(CType(reader("SessionId"), Guid) = request.Context.SessionId.Value, "stored session matches confirmed session")
                            End If
                            Check(Not reader.Read(), "exactly one event per security request")
                        End Using
                    End Using
                Next
            Finally
                ' Only fixtures owned by this run; no business or legacy rows are removed.
                Using tx = cn.BeginTransaction()
                    For Each request In storage.Requests
                        Using cmd As New SqlCommand("DELETE c FROM dbo.Network_Audit_Change c INNER JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.SourceId=@Source AND e.RequestId=@Request; DELETE FROM dbo.Network_Audit_Event WHERE SourceId=@Source AND RequestId=@Request; DELETE FROM dbo.Network_Audit_Request WHERE SourceId=@Source AND RequestId=@Request;", cn, tx)
                            cmd.Parameters.Add("@Source", SqlDbType.UniqueIdentifier).Value = storage.Source
                            cmd.Parameters.Add("@Request", SqlDbType.UniqueIdentifier).Value = request.RequestId
                            cmd.ExecuteNonQuery()
                        End Using
                    Next
                    Using cmd As New SqlCommand("IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND (EnabledDomainsXml.value('count(/Domains/*)','int')>1 OR EnabledDomainsXml.exist('/Domains/*[not(self::Domain) or not(@code=""Security"")]')=1)) THROW 51102,'Test domain configuration changed concurrently; manual recovery required.',1; UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml=@Domains WHERE ConfigId=1 AND SourceId=@Source", cn, tx)
                        cmd.Parameters.Add("@Domains", SqlDbType.Xml).Value = storage.OriginalDomains
                        cmd.Parameters.Add("@Source", SqlDbType.UniqueIdentifier).Value = storage.Source
                        If cmd.ExecuteNonQuery() <> 1 Then Throw New InvalidOperationException("Test source changed; review recovery manifest.")
                    End Using
                    tx.Commit()
                End Using
                storage.Save("Cleaned")
            End Try
            Check(CInt(Scalar(cn, "SELECT COUNT(*) FROM dbo.Network_Edit_Tracker")) = legacyRows, "legacy rows unchanged")
            Check(Not storage.Policy(connection).Enabled, "Security gate restored to disabled")
            For Each request In storage.Requests
                Check(storage.Status(connection, request).Status = "NotFound", "owned fixture cleaned")
            Next
        End Using
    End Sub
End Module
