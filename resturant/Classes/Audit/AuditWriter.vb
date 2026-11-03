Option Strict On
Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml.Linq

Friend NotInheritable Class AuditWriter
    Private Shared Function OptionalAttribute(name As String, value As Object) As XAttribute
        Return If(value Is Nothing, Nothing, New XAttribute(name, AuditCanonical.S(value)))
    End Function

    Friend Shared Function WriteInTransaction(cn As SqlConnection, tx As SqlTransaction, lease As AuditRequestLease, item As AuditEventRequest) As AuditWriteResult
        AuditRequestCoordinator.RequireLease(cn, tx, lease)
        Try
            AuditContract.EventData(lease.Request, item)
            Dim context = lease.Request.Context
            Dim notes = AuditMessageFormatter.Format(lease.Request, item)
            notes = AuditDisplayNameResolver.Decorate(cn, tx, item, notes)
            Dim changes = AuditCanonical.ChangesXml(item)
            Dim relations = AuditCanonical.RelationsXml(item)
            Dim bytes = AuditCanonical.EventBytes(lease.Request, item, notes, changes, relations)
            Dim envelope As New XElement("Event", New XAttribute("version", "1"), New XAttribute("code", item.EventCode),
                New XAttribute("domain", AuditContract.Domain(item.EventCode)), New XAttribute("kind", AuditContract.Kind(item.EventCode)),
                New XAttribute("outcome", item.Outcome), New XAttribute("screen", context.ScreenCode),
                New XAttribute("occurred", AuditCanonical.S(item.OccurredAtUtc)), OptionalAttribute("correlation", item.CorrelationId),
                OptionalAttribute("businessDate", item.BusinessDate), OptionalAttribute("entityType", item.EntityType),
                OptionalAttribute("entityId", item.EntityId), OptionalAttribute("reason", item.ReasonCode),
                If(context.ActorName Is Nothing, Nothing, New XElement("ActorName", context.ActorName)),
                New XElement("Machine", context.MachineName), New XElement("Application", context.ApplicationVersion),
                If(item.DocumentNumber Is Nothing, Nothing, New XElement("Document", item.DocumentNumber)),
                New XElement("Notes", notes), AuditCanonical.ParseXml(relations)) ' Generated locally; no DTD or external entities.
            Using cmd = AuditRequestCoordinator.Command("dbo.Network_Audit_Event_Write", cn, tx)
                AuditRequestCoordinator.Identity(cmd, lease.Request)
                AuditRequestCoordinator.P(cmd, "@EventId", SqlDbType.UniqueIdentifier, 0, item.EventId)
                AuditRequestCoordinator.P(cmd, "@EventOrdinal", SqlDbType.Int, 0, item.EventOrdinal)
                AuditRequestCoordinator.P(cmd, "@EventXml", SqlDbType.Xml, -1, AuditCanonical.LimitedXml(envelope))
                AuditRequestCoordinator.P(cmd, "@ChangesXml", SqlDbType.Xml, -1, changes)
                AuditRequestCoordinator.P(cmd, "@EventFingerprint", SqlDbType.VarBinary, 32, AuditCanonical.Hash(bytes))
                AuditRequestCoordinator.P(cmd, "@CanonicalEvent", SqlDbType.VarBinary, -1, bytes)
                Dim result = CType(CInt(cmd.ExecuteScalar()), AuditWriteResult)
                If result <> AuditWriteResult.AddedPendingCommit AndAlso result <> AuditWriteResult.AlreadyPresent Then Throw New InvalidOperationException("نتيجة كتابة غير معروفة.")
                lease.Events(item.EventOrdinal) = item.EventId
                Return result
            End Using
        Catch
            lease.Faulted = True
            Throw ' Caller must Rollback; this class neither commits nor replaces the transaction.
        End Try
    End Function

    Friend Shared Function WriteIndependent(connectionString As String, request As AuditOperationRequest, item As AuditEventRequest) As AuditRequestResult
        If request Is Nothing OrElse AuditContract.Kind(request.OperationCode) <> 2 Then Throw New ArgumentException("الحفظ المستقل للأحداث الأمنية فقط؛ لا يستخدم مع تغييرات الأعمال.")
        Using cn As New SqlConnection(connectionString)
            cn.Open()
            Using tx = cn.BeginTransaction()
                Try
                    Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
                    If Not lease.MustExecute Then
                        tx.Rollback()
                        Return lease.Result
                    End If
                    WriteInTransaction(cn, tx, lease, item)
                    Dim xml = AuditRequestCoordinator.Complete(cn, tx, lease, item.Outcome)
                    Try
                        tx.Commit()
                    Catch ex As Exception
                        Throw New InvalidOperationException("نتيجة Commit غير مؤكدة؛ استعلم بهوية الطلب نفسها قبل أي إعادة محاولة.", ex)
                    End Try
                    Return New AuditRequestResult With {.Status = "Completed", .ResultCode = item.Outcome, .ResultXml = xml}
                Catch
                    Try
                        If tx.Connection IsNot Nothing Then tx.Rollback()
                    Catch
                        ' Preserve the original exception, not a secondary rollback/connection error.
                    End Try
                    Throw
                End Try
            End Using
        End Using
    End Function
End Class
