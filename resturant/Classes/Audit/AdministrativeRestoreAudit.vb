Option Strict On
Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Security.Cryptography
Imports System.Security.Principal
Imports System.Text
Imports System.Xml
Imports System.Xml.Linq

' Local administrative journal, deliberately independent of the database being replaced.
' Checksums detect damaged records, not deliberate changes by the Windows account/administrator.
Friend NotInheritable Class AdministrativeRestoreAudit
    Implements IDisposable

    Private ReadOnly root As String
    Private ReadOnly gate As FileStream
    Private attemptDirectory As String
    Private attemptId As Guid
    Private dispatched As Boolean
    Private finished As Boolean

    Friend Shared ReadOnly Property DefaultDirectory As String
        Get
            Return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CPOS", "AdministrativeAudit", "Restore")
        End Get
    End Property

    Friend Sub New(auditDirectory As String)
        root = Path.GetFullPath(auditDirectory)
        ' Do not follow a redirected audit directory or an ancestor junction.
        Dim current As New DirectoryInfo(root)
        While current IsNot Nothing
            If current.Exists AndAlso (current.Attributes And FileAttributes.ReparsePoint) <> 0 Then Throw New IOException("مسار سجل الاستعادة يعبر رابطًا أو مجلدًا معاد توجيهه.")
            current = current.Parent
        End While
        If Not Directory.Exists(root) Then Directory.CreateDirectory(root)
        Dim lockPath = Path.Combine(root, "writer.lock")
        If File.Exists(lockPath) AndAlso (File.GetAttributes(lockPath) And FileAttributes.ReparsePoint) <> 0 Then Throw New IOException("ملف قفل سجل الاستعادة غير صالح.")
        gate = New FileStream(lockPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)
    End Sub

    Friend ReadOnly Property Reference As String
        Get
            Return attemptId.ToString("D")
        End Get
    End Property

    Friend Shared Function ReadRecord(file As String) As XElement
        If (New FileInfo(file)).Length > 32768 Then Throw New InvalidDataException("حجم سجل الاستعادة غير صالح.")
        Dim settings As New XmlReaderSettings With {.DtdProcessing = DtdProcessing.Prohibit, .XmlResolver = Nothing, .MaxCharactersInDocument = 32768}
        Dim record As XElement
        Using reader = XmlReader.Create(file, settings)
            record = XElement.Load(reader)
        End Using
        Dim payload = record.Element("Payload")
        If record.Name <> "RestoreAudit" OrElse payload Is Nothing OrElse record.Elements().Count() <> 1 OrElse CStr(record.Attribute("sha256")) <> Digest(payload.ToString(SaveOptions.DisableFormatting)) Then Throw New InvalidDataException("سجل الاستعادة تالف؛ لم يتم تجاهله أو حذفه.")
        Return payload
    End Function

    Private Shared Function Digest(value As String) As String
        Using hash = SHA256.Create()
            Return BitConverter.ToString(hash.ComputeHash(Encoding.UTF8.GetBytes(value))).Replace("-", "")
        End Using
    End Function

    Private Shared Sub WriteRecord(file As String, payload As XElement)
        Dim envelope As New XElement("RestoreAudit", New XAttribute("sha256", Digest(payload.ToString(SaveOptions.DisableFormatting))), payload)
        Dim bytes = New UTF8Encoding(False).GetBytes(envelope.ToString(SaveOptions.DisableFormatting))
        ' CreateNew never overwrites history. A failed/partial record remains evidence and blocks reuse.
        Using output As New FileStream(file, FileMode.CreateNew, FileAccess.Write, FileShare.Read, 4096, FileOptions.WriteThrough)
            output.Write(bytes, 0, bytes.Length)
            output.Flush(True)
        End Using
        ReadRecord(file)
    End Sub

    Private Function Payload(state As String) As XElement
        Return New XElement("Payload", New XAttribute("version", 1), New XAttribute("attempt", Reference),
            New XAttribute("state", state), New XAttribute("utc", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture)),
            New XAttribute("clock", "LocalComputerUtc"))
    End Function

    Friend Function Unresolved() As List(Of String)
        Dim result As New List(Of String)()
        For Each folder In Directory.GetDirectories(root)
            Dim id As Guid
            If Not Guid.TryParseExact(Path.GetFileName(folder), "N", id) Then Continue For
            If (File.GetAttributes(folder) And FileAttributes.ReparsePoint) <> 0 Then Throw New IOException("مجلد محاولة الاستعادة غير صالح.")
            Dim records = Directory.GetFiles(folder, "*.xml")
            For Each file In records
                Dim data = ReadRecord(file)
                If CStr(data.Attribute("attempt")) <> id.ToString("D") Then Throw New InvalidDataException("هوية سجل الاستعادة مختلفة عن مجلده.")
            Next
            If Not File.Exists(Path.Combine(folder, "Started.xml")) Then Throw New InvalidDataException("محاولة استعادة محلية غير مكتملة؛ يلزم فحص سجلها.")
            If Not File.Exists(Path.Combine(folder, "Finished.xml")) Then
                If Not File.Exists(Path.Combine(folder, "Reviewed.xml")) Then result.Add(id.ToString("D"))
            Else
                Dim outcome = CStr(ReadRecord(Path.Combine(folder, "Finished.xml")).Attribute("state"))
                If outcome = "Unknown" AndAlso Not File.Exists(Path.Combine(folder, "Reviewed.xml")) Then result.Add(id.ToString("D"))
            End If
        Next
        Return result
    End Function

    ' Records a human's decision to allow a NEW attempt; never relabels the previous result as success.
    Friend Sub AcknowledgeUnresolved(ids As IEnumerable(Of String), context As AuditEventContext)
        RequireContext(context)
        Dim pending = Unresolved()
        For Each id In ids
            If Not pending.Contains(id) Then Throw New InvalidOperationException("محاولة المراجعة غير معلقة.")
            Dim data As New XElement("Payload", New XAttribute("version", 1), New XAttribute("attempt", id),
                New XAttribute("state", "ReviewedForNewAttempt"), New XAttribute("utc", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture)),
                New XAttribute("clock", "LocalComputerUtc"), New XElement("UserId", context.ActorUserId.Value),
                New XElement("SessionId", context.SessionId.Value), New XElement("WindowsSid", WindowsIdentity.GetCurrent().User.Value))
            WriteRecord(Path.Combine(root, Guid.Parse(id).ToString("N"), "Reviewed.xml"), data)
        Next
    End Sub

    Private Shared Sub RequireContext(context As AuditEventContext)
        If context Is Nothing OrElse context.ActorKind <> AuditActorKind.User OrElse context.ActorUserId.GetValueOrDefault() <= 0 OrElse context.SourceId = Guid.Empty OrElse context.SessionId.GetValueOrDefault() = Guid.Empty Then Throw New ArgumentException("تتطلب الاستعادة جلسة مستخدم مراقبة مؤكدة.")
    End Sub

    Friend Sub Start(context As AuditEventContext, server As String, database As String, backupFile As String)
        RequireContext(context)
        If attemptDirectory IsNot Nothing Then Throw New InvalidOperationException("لا تعاد محاولة الاستعادة بالهوية نفسها.")
        If Unresolved().Count <> 0 Then Throw New InvalidOperationException("توجد استعادة غير محسومة تحتاج مراجعة الخادم قبل محاولة جديدة.")
        If String.IsNullOrWhiteSpace(server) OrElse String.IsNullOrWhiteSpace(database) OrElse database.Length > 128 OrElse Not Path.IsPathRooted(backupFile) Then Throw New ArgumentException("بيانات وجهة الاستعادة غير مكتملة.")
        attemptId = Guid.NewGuid()
        attemptDirectory = Path.Combine(root, attemptId.ToString("N"))
        Directory.CreateDirectory(attemptDirectory)
        Dim record = Payload("Started")
        record.Add(New XElement("Server", server), New XElement("Database", database), New XElement("BackupFile", backupFile),
            New XElement("SourceBeforeRestore", context.SourceId), New XElement("UserId", context.ActorUserId.Value),
            New XElement("UserName", context.ActorName), New XElement("SessionId", context.SessionId.Value),
            New XElement("Machine", context.MachineName), New XElement("ApplicationVersion", context.ApplicationVersion),
            New XElement("WindowsSid", WindowsIdentity.GetCurrent().User.Value), New XElement("WindowsUser", WindowsIdentity.GetCurrent().Name))
        WriteRecord(Path.Combine(attemptDirectory, "Started.xml"), record)
    End Sub

    Friend Sub MarkDispatched()
        If attemptDirectory Is Nothing OrElse dispatched OrElse finished Then Throw New InvalidOperationException("ترتيب تنفيذ الاستعادة غير صالح.")
        WriteRecord(Path.Combine(attemptDirectory, "Dispatched.xml"), Payload("Dispatched"))
        dispatched = True
    End Sub

    Friend Sub Finish(state As String, Optional errorNumber As Integer? = Nothing)
        If attemptDirectory Is Nothing OrElse finished OrElse Not {"Succeeded", "Unknown", "NotDispatched"}.Contains(state) Then Throw New InvalidOperationException("نتيجة سجل الاستعادة غير صالحة.")
        If (state = "NotDispatched") = dispatched Then Throw New InvalidOperationException("النتيجة لا تتوافق مع إرسال أمر الاستعادة.")
        Dim record = Payload(state)
        If errorNumber.HasValue Then record.Add(New XElement("SqlErrorNumber", errorNumber.Value))
        ' No raw exception, connection string, SQL password or credential fingerprint in this journal.
        WriteRecord(Path.Combine(attemptDirectory, "Finished.xml"), record)
        finished = True
    End Sub

    Friend ReadOnly Property WasDispatched As Boolean
        Get
            Return dispatched
        End Get
    End Property

    Public Sub Dispose() Implements IDisposable.Dispose
        If gate IsNot Nothing Then gate.Dispose()
    End Sub
End Class

Friend NotInheritable Class AdministrativeRestoreExecutor
    Friend Shared Function CreateCommand(connection As SqlConnection, database As String, backupFile As String) As SqlCommand
        If String.IsNullOrWhiteSpace(database) OrElse database.Length > 128 OrElse {"master", "model", "msdb", "tempdb"}.Contains(database, StringComparer.OrdinalIgnoreCase) Then Throw New ArgumentException("لا يمكن استعادة قاعدة نظام من هذه الشاشة.")
        Dim identifier = "[" & database.Replace("]", "]]") & "]"
        Dim command As New SqlCommand("ALTER DATABASE " & identifier & " SET SINGLE_USER WITH ROLLBACK IMMEDIATE; RESTORE DATABASE " & identifier & " FROM DISK=@BackupFile WITH REPLACE;", connection)
        command.CommandTimeout = 0 ' Administrative operation; never retry automatically on timeout/transport failure.
        command.Parameters.Add("@BackupFile", System.Data.SqlDbType.NVarChar, 4000).Value = backupFile
        Return command
    End Function
End Class
