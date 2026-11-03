Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Xml.Linq

Friend Module AuditWriterTests
    Private assertions As Integer
    Private source As Guid = Guid.Parse("11111111-1111-1111-1111-111111111111")
    Private ReadOnly requests As New List(Of Guid)()
    Private manifest As String
    Private originalDomains As String

    Public Function Main(args As String()) As Integer
        Try
            Dim settings As String = Nothing, output As String = "."
            For i = 0 To args.Length - 2
                If args(i) = "--settings" Then settings = args(i + 1)
                If args(i) = "--output" Then output = args(i + 1)
            Next
            UnitTests()
            If settings IsNot Nothing Then LiveTests(ReadConnection(settings), output)
            Console.WriteLine("PASS: " & assertions & " writer checks.")
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

    Private Sub Reject(Of T As Exception)(action As Action, name As String)
        Try
            action()
        Catch ex As T
            If TypeOf ex Is SqlException Then
                Dim sqlError = DirectCast(DirectCast(ex, Object), SqlException)
                If sqlError.Number < 51100 OrElse sqlError.Number > 51105 Then Throw New Exception("Unexpected SQL failure instead of audit contract rejection: " & sqlError.Number, ex)
            End If
            Check(True, name)
            Return
        End Try
        Throw New Exception("Expected rejection: " & name)
    End Sub

    Private Function Operation(Optional quantity As Decimal = 2D) As AuditOperationRequest
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .OperationCode = "AuditTest.Changed", .EntityType = "AuditTestCounter", .EntityId = "1",
            .Context = New AuditEventContext With {.SourceId = source, .ActorKind = AuditActorKind.Service, .ActorServiceCode = "AuditTests",
                .MachineName = "AuditTests", .ApplicationVersion = "2B", .ScreenCode = "AuditTests"}}
        r.Inputs.Add(New AuditInput With {.Name = "Quantity", .ValueType = "Decimal", .Value = AuditValue.Capture("Decimal", quantity)})
        Return r
    End Function

    Private Function EventFor(r As AuditOperationRequest, at As DateTime) As AuditEventRequest
        Dim e As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = 1, .EventCode = r.OperationCode,
            .Outcome = 1, .EntityType = "AuditTestCounter", .EntityId = "1", .DocumentNumber = "AuditTest", .OccurredAtUtc = at}
        e.Changes.Add(New AuditFieldChange With {.ChangeNo = 1, .EntityType = "AuditTestCounter", .EntityId = "1", .FieldCode = "Quantity", .ValueType = "Decimal",
            .SourceSqlType = "decimal(18,3)", .ValueOrigin = 1, .Before = AuditValue.Capture("Decimal", 1D), .After = AuditValue.Capture("Decimal", 2D)})
        Return e
    End Function

    Private Sub UnitTests()
        Check(BitConverter.ToString(AuditCanonical.Encode({"A", Nothing, ""})).Replace("-", "") = "0100000041FFFFFFFF00000000", "fixed canonical token vector/null/empty")
        Check(BitConverter.ToString(AuditCanonical.Hash(Text.Encoding.UTF8.GetBytes("abc"))).Replace("-", "").ToLowerInvariant() = "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad", "SHA256 known vector")
        Dim r = Operation(), oldCulture = Thread.CurrentThread.CurrentCulture
        r.RequestId = Guid.Parse("22222222-2222-2222-2222-222222222222")
        Dim canonical As Byte() = AuditCanonical.RequestBytes(r)
        Check(BitConverter.ToString(AuditCanonical.Hash(canonical)).Replace("-", "").ToLowerInvariant() = "6423e46f6d89ecc736c0dcc6c29357797308330a732f0b1604852c30d12aab5c", "full request version-1 fixed vector")
        Try
            Thread.CurrentThread.CurrentCulture = New CultureInfo("ar-LY")
            Check(canonical.SequenceEqual(AuditCanonical.RequestBytes(r)), "request invariant under Arabic culture")
            Check(AuditValue.Capture("Decimal", 6.528D).Text = "6.528", "Decimal no display rounding")
            Dim d As Double = 1.2345678901234567R
            Check(Double.Parse(AuditValue.Capture("Float", d).Text, CultureInfo.InvariantCulture) = d, "Float roundtrip")
        Finally
            Thread.CurrentThread.CurrentCulture = oldCulture
        End Try
        Check(AuditValue.Capture("Decimal", 2.000D).Text = AuditValue.Capture("Decimal", 2D).Text, "Decimal scale normalized")
        r.Inputs.Add(New AuditInput With {.Name = "Name", .ValueType = "String", .Value = AuditValue.Capture("String", "صنف تجريبي")})
        canonical = AuditCanonical.RequestBytes(r)
        r.Inputs.Reverse()
        Check(canonical.SequenceEqual(AuditCanonical.RequestBytes(r)), "input order canonicalized")
        Reject(Of ArgumentException)(Sub() AuditValue.Capture("Float", Double.NaN), "NaN rejected")
        Reject(Of ArgumentException)(Sub() AuditValue.Capture("String", New String("x"c, 4097)), "oversized value rejected")
        Reject(Of ArgumentException)(Sub() AuditContract.Value(AuditValue.Capture("String", "bad"), "Decimal"), "typed value cannot masquerade as Decimal")
        Reject(Of ArgumentException)(Sub() AuditValue.Capture("DateTime", DateTime.UtcNow), "SQL date timezone not guessed")
        Reject(Of ArgumentException)(Sub() AuditCanonical.Encode({New String("x"c, AuditContract.MaxPayload)}), "oversized canonical envelope rejected")
        Reject(Of Xml.XmlException)(Sub() AuditCanonical.ParseXml("<!DOCTYPE x [<!ENTITY a 'bad'>]><x>&a;</x>"), "XML DTD and entities prohibited")
        r.Inputs.Add(New AuditInput With {.Name = "Password", .ValueType = "String", .Value = AuditValue.Capture("String", "never persisted")})
        Reject(Of ArgumentException)(Sub() AuditCanonical.RequestBytes(r), "credential field not allowed")
        r = Operation()
        r.Context.ActorKind = AuditActorKind.Unknown
        r.Context.ActorServiceCode = Nothing
        Reject(Of ArgumentException)(Sub() AuditCanonical.RequestBytes(r), "unknown business actor rejected")
        Reject(Of InvalidOperationException)(Sub() AuditRequestCoordinator.Begin(Nothing, Nothing, Operation()), "missing caller transaction rejected")
        Reject(Of ArgumentException)(Sub() AuditWriter.WriteIndependent("", Operation(), Nothing), "independent path rejects business events")
        Dim security As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .OperationCode = "Security.LoginRejected",
            .Context = New AuditEventContext With {.SourceId = source, .ActorKind = AuditActorKind.Unknown, .MachineName = "test", .ApplicationVersion = "2B", .ScreenCode = "login"}}
        security.Inputs.Add(New AuditInput With {.Name = "ReasonCode", .ValueType = "String", .Value = AuditValue.Capture("String", "InvalidCredential")})
        AuditCanonical.RequestBytes(security)
        Check(True, "unknown login rejection contains no old user")
        security.Context.SessionId = Guid.NewGuid()
        Reject(Of ArgumentException)(Sub() AuditCanonical.RequestBytes(security), "rejected login cannot inherit old session")
        Dim namedEvent As New AuditEventRequest()
        namedEvent.Changes.Add(New AuditFieldChange With {.FieldCode = "PurchaseItemName", .Before = AuditValue.Missing(AuditValueState.Absent),
            .After = AuditValue.Capture("String", "  صنف   محفوظ  ")})
        Dim humanDescription = AuditDisplayNameResolver.DecorateCaptured(namedEvent, "إضافة الصنف 153")
        Check(humanDescription.Contains("التعريف المحفوظ") AndAlso humanDescription.Contains("صنف محفوظ"), "captured entity name is normalized into the persisted human description")
        Check(AuditDisplayNameResolver.DecorateCaptured(namedEvent, "إضافة الصنف 153؛ صنف محفوظ") = "إضافة الصنف 153؛ صنف محفوظ", "entity name is not duplicated when the formatter already displays it")
    End Sub

    Private Function ReadConnection(path As String) As String
        Dim line = File.ReadAllLines(path).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim b As New SqlConnectionStringBuilder(line.Substring("SqlConStr:".Length)) With {.InitialCatalog = "CPOS_ACCOUNTING", .ApplicationName = "CPOS Audit 2B Tests", .ConnectTimeout = 10}
        Return b.ConnectionString
    End Function

    Private Function Run(cn As SqlConnection, sql As String, Optional tx As SqlTransaction = Nothing) As Object
        Using cmd As New SqlCommand(sql, cn, tx)
            Return cmd.ExecuteScalar()
        End Using
    End Function

    Private Function OwnedRequest() As AuditOperationRequest
        Dim r = Operation()
        requests.Add(r.RequestId)
        SaveManifest("Running")
        Return r
    End Function

    Private Sub SaveManifest(status As String)
        Dim root As New XElement("AuditTestRecovery", New XAttribute("database", "CPOS_ACCOUNTING"), New XAttribute("source", source), New XAttribute("status", status),
            New XElement("OriginalDomains", originalDomains), New XElement("Requests", requests.Select(Function(x) New XElement("Request", New XAttribute("id", x)))))
        root.Save(manifest) ' Generated test recovery artifact: never contains connection strings or passwords.
    End Sub

    Private Sub LiveTests(connection As String, output As String)
        manifest = Path.Combine(output, "Recovery_" & Guid.NewGuid().ToString("N") & ".xml")
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(cn.Database = "CPOS_ACCOUNTING", "approved test database only")
            source = CType(Run(cn, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid)
            originalDomains = CStr(Run(cn, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1"))
            Check(CInt(Run(cn, "SELECT EnabledDomainsXml.value('count(/Domains/*)','int') FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = 0, "precondition: all domains disabled")
            Dim legacy = CLng(Run(cn, "SELECT COUNT_BIG(*) FROM dbo.Network_Edit_Tracker"))
            Dim prior = CLng(Run(cn, "SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Event"))
            Dim tableName As String = "##CPOSAuditTest_" & Guid.NewGuid().ToString("N")
            Run(cn, "CREATE TABLE " & tableName & "(Id int PRIMARY KEY, Value decimal(18,3) NOT NULL); INSERT " & tableName & " VALUES(1,0);")
            SaveManifest("Running")
            Try
                Using tx = cn.BeginTransaction()
                    Reject(Of SqlException)(Sub() AuditRequestCoordinator.Begin(cn, tx, OwnedRequest()), "disabled domain blocks begin")
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Using
                Run(cn, "UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml=N'<Domains><Domain code=""AuditTest"" /></Domains>' WHERE ConfigId=1")
                CommitReplayTests(cn, connection, tableName)
                RollbackTests(cn, connection, tableName)
                ConcurrencyTests(cn, connection, tableName)
                NoChangeTests(cn, connection)
                LargePayloadTests(cn, connection)
                IndependentSecurityTests(cn, connection)
            Finally
                Cleanup(cn)
            End Try
            Check(CLng(Run(cn, "SELECT COUNT_BIG(*) FROM dbo.Network_Edit_Tracker")) = legacy, "legacy data unchanged")
            Check(CLng(Run(cn, "SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Event")) = prior, "only owned synthetic audit events removed")
            Check(CInt(Run(cn, "SELECT EnabledDomainsXml.value('count(/Domains/*)','int') FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = 0, "domain activation restored to disabled")
            SaveManifest("Cleaned")
            ' Global temporary business fixture is automatically removed when its owning connection closes.
        End Using
    End Sub

    Private Sub Cleanup(cn As SqlConnection)
        Using tx = cn.BeginTransaction()
            For Each id In requests
                Using cmd As New SqlCommand("DELETE c FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.SourceId=@Source AND e.RequestId=@Request; DELETE dbo.Network_Audit_Event WHERE SourceId=@Source AND RequestId=@Request; DELETE dbo.Network_Audit_Request WHERE SourceId=@Source AND RequestId=@Request AND OperationCode IN('AuditTest.Changed','Security.LoginRejected','Security.SessionStarted');", cn, tx)
                    cmd.Parameters.Add("@Source", SqlDbType.UniqueIdentifier).Value = source
                    cmd.Parameters.Add("@Request", SqlDbType.UniqueIdentifier).Value = id
                    cmd.ExecuteNonQuery()
                End Using
            Next
            Using cmd As New SqlCommand("IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND (EnabledDomainsXml.value('count(/Domains/*)','int')>2 OR EnabledDomainsXml.exist('/Domains/*[not(self::Domain) or not(@code=""AuditTest"" or @code=""Security"")]')=1)) THROW 51102,'Test domain configuration changed concurrently; manual recovery required.',1; UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml=@Original WHERE ConfigId=1 AND SourceId=@Source", cn, tx)
                cmd.Parameters.Add("@Original", SqlDbType.Xml).Value = originalDomains
                cmd.Parameters.Add("@Source", SqlDbType.UniqueIdentifier).Value = source
                cmd.ExecuteNonQuery()
            End Using
            tx.Commit()
        End Using
        SaveManifest("Cleaned")
    End Sub

    Private Sub CommitReplayTests(cn As SqlConnection, connection As String, tableName As String)
        Dim r = OwnedRequest()
        Dim before As Decimal = CDec(Run(cn, "SELECT Value FROM " & tableName))
        Dim firstId As Guid
        Dim xml As String
        Using tx = cn.BeginTransaction()
            Dim lease = AuditRequestCoordinator.Begin(cn, tx, r)
            Check(lease.MustExecute, "new request must execute")
            Run(cn, "UPDATE " & tableName & " SET Value=Value+1", tx)
            Dim item = EventFor(r, AuditRequestCoordinator.ServerUtcNow(cn, tx))
            firstId = item.EventId
            Check(AuditWriter.WriteInTransaction(cn, tx, lease, item) = AuditWriteResult.AddedPendingCommit, "event added pending caller commit")
            Check(AuditWriter.WriteInTransaction(cn, tx, lease, item) = AuditWriteResult.AlreadyPresent, "same event retry is idempotent")
            Dim second = EventFor(r, item.OccurredAtUtc)
            second.EventOrdinal = 2
            AuditWriter.WriteInTransaction(cn, tx, lease, second)
            xml = AuditRequestCoordinator.Complete(cn, tx, lease, 1)
            Check(tx.Connection IsNot Nothing, "writer/complete do not commit caller transaction")
            tx.Commit()
        End Using
        Dim result = AuditRequestCoordinator.Status(connection, r)
        Check(result.Status = "Completed" AndAlso XNode.DeepEquals(XElement.Parse(result.ResultXml), XElement.Parse(xml)), "status recovers committed result after lost reply")
        Using tx = cn.BeginTransaction()
            Dim replay = AuditRequestCoordinator.Begin(cn, tx, r)
            Check(Not replay.MustExecute AndAlso replay.Result.ResultXml = result.ResultXml, "completed request returns original result without business replay")
            tx.Rollback()
        End Using
        Check(CDec(Run(cn, "SELECT Value FROM " & tableName)) = before + 1D, "business fixture changed exactly once")
        r.Inputs(0).Value = AuditValue.Capture("Decimal", 999D)
        Using tx = cn.BeginTransaction()
            Reject(Of SqlException)(Sub() AuditRequestCoordinator.Begin(cn, tx, r), "same request ID different content rejected")
            If tx.Connection IsNot Nothing Then tx.Rollback()
        End Using
        Dim other = OwnedRequest()
        Using tx = cn.BeginTransaction()
            Dim lease = AuditRequestCoordinator.Begin(cn, tx, other)
            Run(cn, "UPDATE " & tableName & " SET Value=Value+100", tx)
            Dim item = EventFor(other, AuditRequestCoordinator.ServerUtcNow(cn, tx))
            item.EventId = firstId
            Reject(Of SqlException)(Sub() AuditWriter.WriteInTransaction(cn, tx, lease, item), "SQL event identity conflict fails write")
            Reject(Of InvalidOperationException)(Sub() AuditRequestCoordinator.Complete(cn, tx, lease, 1), "failed lease cannot complete")
            If tx.Connection IsNot Nothing Then tx.Rollback()
        End Using
        Check(CDec(Run(cn, "SELECT Value FROM " & tableName)) = before + 1D, "SQL audit failure rolls back associated business fixture")
    End Sub

    Private Sub RollbackTests(cn As SqlConnection, connection As String, tableName As String)
        Dim r = OwnedRequest(), before = CDec(Run(cn, "SELECT Value FROM " & tableName))
        Using tx = cn.BeginTransaction()
            Dim lease = AuditRequestCoordinator.Begin(cn, tx, r)
            Run(cn, "UPDATE " & tableName & " SET Value=Value+10", tx)
            AuditWriter.WriteInTransaction(cn, tx, lease, EventFor(r, AuditRequestCoordinator.ServerUtcNow(cn, tx)))
            AuditRequestCoordinator.Complete(cn, tx, lease, 1)
            tx.Rollback()
        End Using
        Check(CDec(Run(cn, "SELECT Value FROM " & tableName)) = before, "caller rollback removes fixture mutation")
        Check(AuditRequestCoordinator.Status(connection, r).Status = "NotFound", "caller rollback removes request and event")
        r = OwnedRequest()
        Using tx = cn.BeginTransaction()
            Dim lease = AuditRequestCoordinator.Begin(cn, tx, r)
            Dim item = EventFor(r, AuditRequestCoordinator.ServerUtcNow(cn, tx))
            item.Changes(0).FieldCode = "Password"
            Reject(Of ArgumentException)(Sub() AuditWriter.WriteInTransaction(cn, tx, lease, item), "invalid event field rejected before insert")
            Reject(Of InvalidOperationException)(Sub() AuditRequestCoordinator.Complete(cn, tx, lease, 5), "validation failure poisons lease")
            tx.Rollback()
        End Using
        r = OwnedRequest()
        Using tx = cn.BeginTransaction()
            Dim lease = AuditRequestCoordinator.Begin(cn, tx, r)
            Using other As New SqlConnection(connection)
                other.Open()
                Using wrong = other.BeginTransaction()
                    Reject(Of InvalidOperationException)(Sub() AuditWriter.WriteInTransaction(other, wrong, lease, EventFor(r, DateTime.UtcNow)), "different connection/transaction cannot write lease")
                    wrong.Rollback()
                End Using
            End Using
            tx.Rollback()
        End Using
    End Sub

    Private Sub ConcurrencyTests(cn As SqlConnection, connection As String, tableName As String)
        Dim r = OwnedRequest(), before = CDec(Run(cn, "SELECT Value FROM " & tableName))
        Using ready As New ManualResetEventSlim(False), tx = cn.BeginTransaction()
            Dim lease = AuditRequestCoordinator.Begin(cn, tx, r)
            Run(cn, "UPDATE " & tableName & " SET Value=Value+1", tx)
            AuditWriter.WriteInTransaction(cn, tx, lease, EventFor(r, AuditRequestCoordinator.ServerUtcNow(cn, tx)))
            AuditRequestCoordinator.Complete(cn, tx, lease, 1)
            Dim competing As Task(Of Boolean) = Task.Run(Function()
                                                            Using other As New SqlConnection(connection)
                                                                other.Open()
                                                                Using another = other.BeginTransaction()
                                                                    ready.Set()
                                                                    Dim replay = AuditRequestCoordinator.Begin(other, another, r)
                                                                    Dim shouldExecute = replay.MustExecute
                                                                    another.Rollback()
                                                                    Return shouldExecute
                                                                End Using
                                                            End Using
                                                        End Function)
            Check(ready.Wait(3000), "competing request started")
            Check(AuditRequestCoordinator.Status(connection, r, 100).Status = "InProgressOrUnknown", "in-flight request status is not falsely NotFound")
            Check(Not competing.IsCompleted, "same-ID concurrent request waits for owner")
            tx.Commit()
            Check(competing.Wait(6000) AndAlso Not competing.Result, "concurrent duplicate receives committed result")
        End Using
        Check(CDec(Run(cn, "SELECT Value FROM " & tableName)) = before + 1D, "concurrent same-ID work executes once")
    End Sub

    Private Sub NoChangeTests(cn As SqlConnection, connection As String)
        Dim r = OwnedRequest()
        Using tx = cn.BeginTransaction()
            Dim lease = AuditRequestCoordinator.Begin(cn, tx, r)
            AuditRequestCoordinator.Complete(cn, tx, lease, 5)
            tx.Commit()
        End Using
        Check(AuditRequestCoordinator.Status(connection, r).ResultCode.GetValueOrDefault() = 5, "NoChange completes without fictitious event")
        r = OwnedRequest()
        Using tx = cn.BeginTransaction()
            AuditRequestCoordinator.Begin(cn, tx, r)
            tx.Commit() ' Deliberate caller contract violation on synthetic request only.
        End Using
        Check(AuditRequestCoordinator.Status(connection, r).Status = "IncompleteCommitted", "committed pending request requires review")
        Using tx = cn.BeginTransaction()
            Reject(Of SqlException)(Sub() AuditRequestCoordinator.Begin(cn, tx, r), "committed pending request cannot re-execute")
            If tx.Connection IsNot Nothing Then tx.Rollback()
        End Using
    End Sub

    Private Sub LargePayloadTests(cn As SqlConnection, connection As String)
        Dim r = OwnedRequest()
        r.Inputs.Add(New AuditInput With {.Name = "Name", .ValueType = "String", .Value = AuditValue.Capture("String", New String(ChrW(&H633), 4000))})
        Check(AuditCanonical.RequestBytes(r).Length > 8000, "large request exceeds SQL2014 HASHBYTES input limit")
        Using tx = cn.BeginTransaction()
            Dim lease = AuditRequestCoordinator.Begin(cn, tx, r)
            Dim item = EventFor(r, AuditRequestCoordinator.ServerUtcNow(cn, tx))
            item.Changes.Add(New AuditFieldChange With {.ChangeNo = 2, .EntityType = "AuditTestCounter", .EntityId = "1", .FieldCode = "Name", .ValueType = "String",
                .SourceSqlType = "nvarchar(max)", .ValueOrigin = 1, .Before = AuditValue.Missing(AuditValueState.SqlNull), .After = r.Inputs(1).Value})
            AuditWriter.WriteInTransaction(cn, tx, lease, item)
            AuditRequestCoordinator.Complete(cn, tx, lease, 1)
            tx.Commit()
        End Using
        Check(AuditRequestCoordinator.Status(connection, r).Status = "Completed", "large Arabic payload writes and recovers")
        Dim bytes = AuditCanonical.RequestBytes(r)
        Dim changed = DirectCast(bytes.Clone(), Byte())
        changed(changed.Length - 1) = CByte(changed.Last() Xor 1)
        Using cmd = AuditRequestCoordinator.Command("dbo.Network_Audit_Request_Status", cn, Nothing)
            AuditRequestCoordinator.Identity(cmd, r)
            AuditRequestCoordinator.P(cmd, "@RequestFingerprint", SqlDbType.VarBinary, 32, AuditCanonical.Hash(bytes))
            AuditRequestCoordinator.P(cmd, "@CanonicalRequest", SqlDbType.VarBinary, -1, changed)
            Reject(Of SqlException)(Sub() cmd.ExecuteScalar(), "same supplied hash with different bytes is rejected")
        End Using
        r = OwnedRequest()
        Using tx = cn.BeginTransaction()
            Dim lease = AuditRequestCoordinator.Begin(cn, tx, r)
            Dim item = EventFor(r, AuditRequestCoordinator.ServerUtcNow(cn, tx))
            AuditWriter.WriteInTransaction(cn, tx, lease, item)
            item.Changes(0).After = AuditValue.Capture("Decimal", 999D)
            Reject(Of SqlException)(Sub() AuditWriter.WriteInTransaction(cn, tx, lease, item), "same event ID with changed details rejected")
            If tx.Connection IsNot Nothing Then tx.Rollback()
        End Using
    End Sub

    Private Sub IndependentSecurityTests(cn As SqlConnection, connection As String)
        ' No application login path calls V2 yet. Only these synthetic test attempts use this temporary gate.
        Run(cn, "UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml=N'<Domains><Domain code=""AuditTest""/><Domain code=""Security""/></Domains>' WHERE ConfigId=1")
        Dim r = OwnedRequest()
        r.OperationCode = "Security.LoginRejected"
        r.EntityType = Nothing
        r.EntityId = Nothing
        r.Inputs.Clear()
        r.Context.ActorKind = AuditActorKind.Unknown
        r.Context.ActorServiceCode = Nothing
        r.Inputs.Add(New AuditInput With {.Name = "ReasonCode", .ValueType = "String", .Value = AuditValue.Capture("String", "InvalidCredential")})
        Dim now As DateTime = DateTime.SpecifyKind(CDate(Run(cn, "SELECT CONVERT(datetime2(3),SYSUTCDATETIME())")), DateTimeKind.Utc)
        Dim item As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = 1, .EventCode = r.OperationCode, .Outcome = 2, .ReasonCode = "InvalidCredential", .OccurredAtUtc = now}
        Dim result = AuditWriter.WriteIndependent(connection, r, item)
        Check(result.Status = "Completed" AndAlso result.ResultCode.GetValueOrDefault() = 2, "independent rejection commits only its audit transaction")
        Check(AuditWriter.WriteIndependent(connection, r, item).ResultXml = result.ResultXml, "independent security retry returns same result")
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE EventId=@Id AND ActorKind=2 AND ActorUserId IS NULL AND ActorName IS NULL AND SessionId IS NULL", cn)
            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = item.EventId
            Check(CInt(cmd.ExecuteScalar()) = 1, "unknown login has no inherited user/session in stored event")
        End Using
        Dim started = OwnedRequest()
        started.OperationCode = "Security.SessionStarted"
        started.EntityType = Nothing
        started.EntityId = Nothing
        started.Inputs.Clear()
        started.Context.ActorKind = AuditActorKind.User
        started.Context.ActorServiceCode = Nothing
        started.Context.ActorUserId = 2147483646 ' Synthetic ID; no Users row is created or modified.
        started.Context.ActorName = "AuditTest synthetic user"
        started.Context.SessionId = Guid.NewGuid()
        Dim startEvent As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = 1, .EventCode = started.OperationCode, .Outcome = 1, .OccurredAtUtc = now}
        Check(AuditWriter.WriteIndependent(connection, started, startEvent).Status = "Completed", "session-start audit can commit independently")
        Run(cn, "UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml=N'<Domains><Domain code=""AuditTest""/></Domains>' WHERE ConfigId=1")
        Check(AuditWriter.WriteIndependent(connection, r, item).Status = "Completed", "completed request still recoverable after domain disabled")
    End Sub
End Module
