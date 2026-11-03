Option Strict On
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Xml.Linq

Friend Module AdministrativeRestoreTests
    Private checks As Integer
    Private root As String
    Private context As New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 7,
        .ActorName = "AuditTest", .SourceId = Guid.NewGuid(), .SessionId = Guid.NewGuid(),
        .MachineName = "TestMachine", .ScreenCode = "Backup", .ApplicationVersion = "Test"}

    Public Function Main(args As String()) As Integer
        Try
            root = Path.Combine(Path.GetFullPath(args(0)), "LocalJournal_" & Guid.NewGuid().ToString("N"))
            Directory.CreateDirectory(root)
            SuccessAndReplay()
            UnknownAndReview()
            BeforeDispatch()
            DamagedAndOversized()
            Using cn As New SqlConnection(), cmd = AdministrativeRestoreExecutor.CreateCommand(cn, "A]B", "C:\backup\it's-a-file.bak")
                Check(cmd.CommandText.Contains("[A]]B]"), "escaped database identifier")
                Check(Not cmd.CommandText.Contains("it's-a-file"), "backup path is parameterized")
                Check(CStr(cmd.Parameters("@BackupFile").Value).Contains("it's-a-file"), "path unchanged")
                Check(cmd.CommandTimeout = 0, "no automatic timeout retry")
                Reject(Sub() AdministrativeRestoreExecutor.CreateCommand(cn, "master", "C:\x.bak"), "system database denied")
            End Using
            Console.WriteLine("PASS " & checks & " administrative restore checks. No SQL command executed. Fixtures: " & root)
            Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString()) : Return 1
        End Try
    End Function

    Private Sub Check(value As Boolean, label As String)
        If Not value Then Throw New Exception("FAILED " & label)
        checks += 1 : Console.WriteLine("PASS " & label)
    End Sub

    Private Sub Reject(work As Action, label As String)
        Try
            work()
        Catch
            Check(True, label) : Return
        End Try
        Throw New Exception("Expected rejection " & label)
    End Sub

    Private Sub Start(journal As AdministrativeRestoreAudit)
        journal.Start(context, "TestServer", "TestDatabase", "C:\backup\test.bak")
    End Sub

    Private Sub SuccessAndReplay()
        Dim dir = Path.Combine(root, "Success")
        Dim reference As String
        Using j As New AdministrativeRestoreAudit(dir)
            Check(j.Unresolved().Count = 0, "new store empty")
            Reject(Sub()
                       Using second As New AdministrativeRestoreAudit(dir)
                       End Using
                   End Sub, "second writer blocked")
            Reject(Sub() j.MarkDispatched(), "dispatch before start denied")
            Start(j) : reference = j.Reference
            Dim folder = Path.Combine(dir, Guid.Parse(reference).ToString("N"))
            Dim record = AdministrativeRestoreAudit.ReadRecord(Path.Combine(folder, "Started.xml"))
            Check(CStr(record.Element("UserId")) = "7", "real application actor")
            Check(CStr(record.Attribute("clock")) = "LocalComputerUtc", "local clock explicit")
            Check(CStr(record.Element("SourceBeforeRestore")) = context.SourceId.ToString(), "pre-restore source retained")
            Check(record.Element("WindowsSid") IsNot Nothing, "Windows identity retained")
            Check(Not record.ToString().Contains("SqlConStr") AndAlso Not record.ToString().Contains("Password"), "no connection credentials")
            Reject(Sub() Start(j), "no same-instance replay")
            j.MarkDispatched()
            Reject(Sub() j.MarkDispatched(), "no dispatch retry")
            Reject(Sub() j.Finish("NotDispatched"), "false pre-dispatch result denied")
            j.Finish("Succeeded")
            Reject(Sub() j.Finish("Unknown"), "cannot overwrite successful outcome")
            Check(j.Unresolved().Count = 0, "successful attempt resolved")
        End Using
        Using reopened As New AdministrativeRestoreAudit(dir)
            Check(reopened.Unresolved().Count = 0, "completion survives new instance")
        End Using
    End Sub

    Private Sub UnknownAndReview()
        Dim dir = Path.Combine(root, "Unknown")
        Dim reference As String
        Using j As New AdministrativeRestoreAudit(dir)
            Start(j) : reference = j.Reference : j.MarkDispatched() : j.Finish("Unknown", -2)
            Check(j.Unresolved().SequenceEqual({reference}), "transport error remains unknown")
        End Using
        Using j As New AdministrativeRestoreAudit(dir)
            Reject(Sub() Start(j), "unknown attempt blocks new dispatch")
            Reject(Sub() j.AcknowledgeUnresolved({Guid.NewGuid().ToString()}, context), "cannot review nonexistent attempt")
            j.AcknowledgeUnresolved({reference}, context)
            Check(j.Unresolved().Count = 0, "explicit administrative review permits new attempt")
            Dim file = Path.Combine(dir, Guid.Parse(reference).ToString("N"), "Finished.xml")
            Check(CStr(AdministrativeRestoreAudit.ReadRecord(file).Attribute("state")) = "Unknown", "review does not forge success")
            Start(j)
            Check(j.Reference <> reference, "new attempt has new identity")
            j.Finish("NotDispatched")
        End Using
    End Sub

    Private Sub BeforeDispatch()
        Dim dir = Path.Combine(root, "EarlyCrash")
        Dim reference As String
        Using j As New AdministrativeRestoreAudit(dir)
            Start(j) : reference = j.Reference
            Reject(Sub() j.Finish("Succeeded"), "success before dispatch denied")
        End Using
        Using j As New AdministrativeRestoreAudit(dir)
            Check(j.Unresolved().SequenceEqual({reference}), "crash between start and dispatch retained")
        End Using
        Using j As New AdministrativeRestoreAudit(Path.Combine(root, "ConnectionFailure"))
            Start(j) : j.Finish("NotDispatched")
            Check(j.Unresolved().Count = 0 AndAlso Not j.WasDispatched, "connection failure never reports a restore")
        End Using
        Using j As New AdministrativeRestoreAudit(Path.Combine(root, "InvalidActor"))
            Reject(Sub() j.Start(New AuditEventContext(), "Server", "DB", "C:\x.bak"), "unconfirmed actor denied")
        End Using
        Using j As New AdministrativeRestoreAudit(Path.Combine(root, "OutcomeFailure"))
            Start(j) : j.MarkDispatched()
            Dim file = Path.Combine(root, "OutcomeFailure", Guid.Parse(j.Reference).ToString("N"), "Finished.xml")
            Directory.CreateDirectory(file)
            Reject(Sub() j.Finish("Succeeded"), "final write failure visible")
            Check(j.Unresolved().Count = 1, "durable start remains after final log failure")
        End Using
    End Sub

    Private Sub DamagedAndOversized()
        Dim dir = Path.Combine(root, "Corruption")
        Using j As New AdministrativeRestoreAudit(dir)
            Start(j)
            Dim file = Path.Combine(dir, Guid.Parse(j.Reference).ToString("N"), "Started.xml")
            System.IO.File.WriteAllText(file, System.IO.File.ReadAllText(file).Replace("TestServer", "ChangedServer"))
            Reject(Sub() j.Unresolved(), "checksum corruption blocks new attempt")
        End Using
        Dim largeFile = Path.Combine(root, "Oversized.xml")
        File.WriteAllText(largeFile, New String("x"c, 32769))
        Reject(Sub() AdministrativeRestoreAudit.ReadRecord(largeFile), "oversized record denied")
        Dim dtdFile = Path.Combine(root, "Dtd.xml")
        File.WriteAllText(dtdFile, "<!DOCTYPE x [<!ENTITY a SYSTEM 'file:///never-read'>]><x>&a;</x>")
        Reject(Sub() AdministrativeRestoreAudit.ReadRecord(dtdFile), "DTD denied")
    End Sub
End Module
