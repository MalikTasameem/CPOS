Option Strict On
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq

Friend Class AuditSecurityPolicy
    Public Property Enabled As Boolean
    Public Property SourceId As Guid
End Class

' No password, encrypted password or credential fingerprint crosses this boundary.
Friend Interface IAuditSecurityStore
    Function Policy(connection As String) As AuditSecurityPolicy
    Function UtcNow(connection As String) As DateTime
    Function Write(connection As String, request As AuditOperationRequest, item As AuditEventRequest) As AuditRequestResult
    Function Status(connection As String, request As AuditOperationRequest) As AuditRequestResult
End Interface

Friend NotInheritable Class SqlAuditSecurityStore
    Implements IAuditSecurityStore

    Private Shared Function BoundedConnection(connection As String) As String
        Dim builder As New SqlConnectionStringBuilder(connection)
        builder.ConnectTimeout = 5
        Return builder.ConnectionString
    End Function

    Public Function Policy(connection As String) As AuditSecurityPolicy Implements IAuditSecurityStore.Policy
        Using cn As New SqlConnection(BoundedConnection(connection))
            cn.Open()
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM sys.tables WHERE schema_id=SCHEMA_ID(N'dbo') AND name IN (N'Network_Audit_Source',N'Network_Audit_Request',N'Network_Audit_Event',N'Network_Audit_Change')", cn)
                cmd.CommandTimeout = 5
                Dim count = CInt(cmd.ExecuteScalar())
                If count = 0 Then
                    cmd.CommandText = "SELECT HAS_PERMS_BY_NAME(DB_NAME(),N'DATABASE',N'VIEW DEFINITION')"
                    If Not Object.Equals(cmd.ExecuteScalar(), 1) Then Throw New InvalidOperationException("تعذر التحقق من وجود بنية المراقبة.")
                    Return New AuditSecurityPolicy() ' A verified legacy database, not a failed connection.
                End If
                If count <> 4 Then Throw New InvalidOperationException("بنية المراقبة غير مكتملة؛ لا يمكن تجاوزها أثناء الدخول.")
                cmd.CommandText = "SELECT SourceId,SchemaVersion,EnabledDomainsXml FROM dbo.Network_Audit_Source WHERE ConfigId=1"
                Dim policyValue As AuditSecurityPolicy
                Using reader = cmd.ExecuteReader()
                    If Not reader.Read() OrElse CInt(reader("SchemaVersion")) <> 1 Then Throw New InvalidOperationException("إعداد مصدر المراقبة غير صالح.")
                    Dim domains = AuditCanonical.ParseXml(CStr(reader("EnabledDomainsXml")))
                    If domains.Name <> "Domains" Then Throw New InvalidOperationException("إعداد مجالات المراقبة غير صالح.")
                    policyValue = New AuditSecurityPolicy With {.SourceId = CType(reader("SourceId"), Guid),
                        .Enabled = domains.Elements("Domain").Any(Function(x) CStr(x.Attribute("code")) = "Security")}
                    If policyValue.SourceId = Guid.Empty Then Throw New InvalidOperationException("هوية مصدر المراقبة فارغة.")
                End Using
                If policyValue.Enabled Then
                    cmd.CommandText = "SELECT COUNT(*) FROM sys.procedures WHERE schema_id=SCHEMA_ID(N'dbo') AND name IN (N'Network_Audit_Request_Begin',N'Network_Audit_Event_Write',N'Network_Audit_Request_Complete',N'Network_Audit_Request_Status')"
                    If CInt(cmd.ExecuteScalar()) <> 4 Then Throw New InvalidOperationException("إجراءات المراقبة غير مكتملة؛ لم يبدأ الدخول.")
                End If
                Return policyValue
            End Using
        End Using
    End Function

    Public Function UtcNow(connection As String) As DateTime Implements IAuditSecurityStore.UtcNow
        Using cn As New SqlConnection(BoundedConnection(connection)), cmd As New SqlCommand("SELECT CONVERT(datetime2(3),SYSUTCDATETIME())", cn)
            cmd.CommandTimeout = 5
            cn.Open()
            Return DateTime.SpecifyKind(CDate(cmd.ExecuteScalar()), DateTimeKind.Utc)
        End Using
    End Function

    Public Function Write(connection As String, request As AuditOperationRequest, item As AuditEventRequest) As AuditRequestResult Implements IAuditSecurityStore.Write
        Return AuditWriter.WriteIndependent(BoundedConnection(connection), request, item)
    End Function

    Public Function Status(connection As String, request As AuditOperationRequest) As AuditRequestResult Implements IAuditSecurityStore.Status
        Return AuditRequestCoordinator.Status(BoundedConnection(connection), request)
    End Function
End Class

' UI-thread owned. A pending start retains its identities until its outcome is resolved.
' This is NOT an offline queue and nothing is serialized to local files.
Friend NotInheritable Class AuditSecuritySession
    Friend Shared ReadOnly Current As New AuditSecuritySession(New SqlAuditSecurityStore())
    Private ReadOnly store As IAuditSecurityStore
    Private active As SecurityAttempt
    Private pendingStart As SecurityAttempt

    Private Class SecurityAttempt
        Friend Connection As String
        Friend Request As AuditOperationRequest
        Friend Item As AuditEventRequest
        Friend Attempted As Boolean
    End Class

    Friend Sub New(storage As IAuditSecurityStore)
        If storage Is Nothing Then Throw New ArgumentNullException("storage")
        store = storage
    End Sub

    Private Function CreateAttempt(connection As String, context As AuditEventContext, operation As String, Optional reason As String = Nothing) As SecurityAttempt
        Dim request As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .OperationCode = operation, .Context = context.Copy()}
        If reason IsNot Nothing Then request.Inputs.Add(New AuditInput With {.Name = "ReasonCode", .ValueType = "String", .Value = AuditValue.Capture("String", reason)})
        Dim item As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = 1, .EventCode = operation,
            .Outcome = If(operation = "Security.LoginRejected", CByte(2), CByte(1)), .ReasonCode = reason, .OccurredAtUtc = store.UtcNow(connection)}
        AuditContract.Request(request)
        AuditContract.EventData(request, item)
        Return New SecurityAttempt With {.Connection = connection, .Request = request, .Item = item}
    End Function

    Private Sub Persist(attempt As SecurityAttempt)
        If attempt.Attempted Then
            Dim previous = store.Status(attempt.Connection, attempt.Request)
            If previous.Status = "Completed" Then
                RequireCompleted(previous, attempt)
                Return
            End If
            If previous.Status <> "NotFound" Then Throw New InvalidOperationException("نتيجة الطلب السابق غير محسومة؛ لا تنشأ محاولة بديلة.")
        End If
        attempt.Attempted = True
        Try
            RequireCompleted(store.Write(attempt.Connection, attempt.Request, attempt.Item), attempt)
        Catch
            ' A lost Commit reply is not failure. Confirm by the SAME identity, never by user/time.
            Try
                Dim recovered = store.Status(attempt.Connection, attempt.Request)
                If recovered.Status = "Completed" Then
                    RequireCompleted(recovered, attempt)
                    Return
                End If
            Catch
                ' Preserve the write failure. A failed status read is not NotFound.
            End Try
            Throw
        End Try
    End Sub

    Private Shared Sub RequireCompleted(result As AuditRequestResult, attempt As SecurityAttempt)
        If result Is Nothing OrElse result.Status <> "Completed" OrElse result.ResultCode.GetValueOrDefault() <> attempt.Item.Outcome Then
            Throw New InvalidOperationException("لم تؤكد قاعدة البيانات اكتمال حدث المراقبة.")
        End If
    End Sub

    Friend Function Start(connection As String, userId As Integer, userName As String, screen As String, machine As String, version As String) As String
        If userId <= 0 OrElse String.IsNullOrWhiteSpace(userName) Then Throw New ArgumentException("هوية المستخدم المؤكد غير صالحة.")
        If pendingStart IsNot Nothing Then
            If pendingStart.Connection <> connection OrElse pendingStart.Request.Context.ActorUserId.GetValueOrDefault() <> userId OrElse pendingStart.Request.Context.ScreenCode <> screen Then
                Throw New InvalidOperationException("توجد محاولة دخول غير محسومة؛ أعد المحاولة بالمستخدم والاتصال السابقين. مرجع الطلب: " & pendingStart.Request.RequestId.ToString("D"))
            End If
        Else
            Dim policyValue = store.Policy(connection)
            If Not policyValue.Enabled Then Return Finish(screen) ' Explicitly disabled/verified legacy only.
            Dim context As New AuditEventContext With {.SourceId = policyValue.SourceId, .ActorKind = AuditActorKind.User,
                .ActorUserId = userId, .ActorName = userName, .SessionId = Guid.NewGuid(), .ScreenCode = screen,
                .MachineName = machine, .ApplicationVersion = version}
            pendingStart = CreateAttempt(connection, context, "Security.SessionStarted")
        End If
        Try
            Persist(pendingStart)
        Catch ex As Exception
            Throw New InvalidOperationException("لم يتأكد حفظ بدء الجلسة؛ لن يتم الدخول. أعد المحاولة بنفس المستخدم. مرجع الطلب: " & pendingStart.Request.RequestId.ToString("D"), ex)
        End Try
        Dim previous = active
        active = pendingStart
        pendingStart = Nothing
        Return FinishAttempt(previous, screen)
    End Function

    Friend Function RejectLogin(connection As String, userId As Integer?, userName As String, screen As String, machine As String, version As String, Optional reason As String = Nothing) As String
        Dim attempt As SecurityAttempt = Nothing
        Try
            Dim policyValue = store.Policy(connection)
            If Not policyValue.Enabled Then Return Nothing
            Dim context As New AuditEventContext With {.SourceId = policyValue.SourceId, .ActorKind = If(userId.HasValue, AuditActorKind.User, AuditActorKind.Unknown),
                .ActorUserId = userId, .ActorName = If(userId.HasValue, userName, Nothing), .ScreenCode = screen, .MachineName = machine, .ApplicationVersion = version}
            attempt = CreateAttempt(connection, context, "Security.LoginRejected", If(reason, If(userId.HasValue, "UserNotAllowed", "InvalidCredential")))
            Persist(attempt)
            Return Nothing
        Catch
            Return "رُفض الدخول، لكن تعذر تأكيد تسجيل الرفض في المراقبة." & ReferenceText(attempt)
        End Try
    End Function

    Friend Function Finish(screen As String) As String
        Dim previous = active
        active = Nothing ' Never prevent exit or leave a usable session after a failed end write.
        Return FinishAttempt(previous, screen)
    End Function

    Private Function FinishAttempt(previous As SecurityAttempt, screen As String) As String
        If previous Is Nothing Then Return Nothing
        Dim attempt As SecurityAttempt = Nothing
        Try
            Dim context = previous.Request.Context.Copy()
            context.ScreenCode = screen
            attempt = CreateAttempt(previous.Connection, context, "Security.SessionEnded")
            Persist(attempt)
            Return Nothing
        Catch
            Return "تم إنهاء الجلسة محليًا، لكن تعذر تأكيد تسجيل نهايتها في المراقبة." & ReferenceText(attempt)
        End Try
    End Function

    Private Shared Function ReferenceText(attempt As SecurityAttempt) As String
        Return If(attempt Is Nothing, "", " مرجع الطلب: " & attempt.Request.RequestId.ToString("D"))
    End Function

    Friend Sub Abandon()
        active = Nothing ' OS shutdown/crash: no fabricated explicit logout event.
    End Sub

    Friend Function ContextFor(connection As String, expectedUserId As Integer, screen As String) As AuditEventContext
        If active Is Nothing OrElse active.Connection <> connection OrElse active.Request.Context.ActorUserId.GetValueOrDefault() <> expectedUserId Then
            Throw New InvalidOperationException("لا توجد جلسة مراقبة مؤكدة لهذا المستخدم والاتصال؛ أعد تسجيل الدخول.")
        End If
        Dim result = active.Request.Context.Copy()
        result.ScreenCode = screen
        Return result
    End Function

    Friend Sub InvalidateAfterAdministrativeRestore()
        ' The database may have been replaced. Never write an end event into a different history.
        active = Nothing
        pendingStart = Nothing
    End Sub
End Class
