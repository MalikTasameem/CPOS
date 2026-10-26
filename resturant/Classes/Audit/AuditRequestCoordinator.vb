Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Xml.Linq

Friend NotInheritable Class AuditRequestLease
    Friend ReadOnly Connection As SqlConnection
    Friend ReadOnly Transaction As SqlTransaction
    Friend ReadOnly Request As AuditOperationRequest
    Friend ReadOnly Canonical As Byte()
    Friend ReadOnly Events As New SortedDictionary(Of Integer, Guid)()
    Friend ReadOnly Result As AuditRequestResult
    Friend Faulted As Boolean
    Friend Finished As Boolean

    Friend Sub New(cn As SqlConnection, tx As SqlTransaction, operation As AuditOperationRequest, bytes As Byte(), resultValue As AuditRequestResult)
        Connection = cn
        Transaction = tx
        Request = operation
        Canonical = bytes
        Result = resultValue
    End Sub

    Friend ReadOnly Property MustExecute As Boolean
        Get
            Return Result.Status = "NewPendingCommit" AndAlso Not Finished AndAlso Not Faulted
        End Get
    End Property
End Class

Friend NotInheritable Class AuditRequestCoordinator
    Friend Shared Sub RequireTransaction(cn As SqlConnection, tx As SqlTransaction)
        If cn Is Nothing OrElse cn.State <> ConnectionState.Open OrElse tx Is Nothing OrElse Not Object.ReferenceEquals(tx.Connection, cn) Then
            Throw New InvalidOperationException("يلزم نفس الاتصال المفتوح ومعاملة الأعمال النشطة؛ لا يُنشأ اتصال بديل.")
        End If
    End Sub

    Friend Shared Sub RequireLease(cn As SqlConnection, tx As SqlTransaction, lease As AuditRequestLease)
        RequireTransaction(cn, tx)
        If lease Is Nothing OrElse Not Object.ReferenceEquals(lease.Connection, cn) OrElse Not Object.ReferenceEquals(lease.Transaction, tx) OrElse Not lease.MustExecute Then Throw New InvalidOperationException("الطلب غير نشط/فاشل أو لا يتبع هذه المعاملة.")
        If Not AuditCanonical.RequestBytes(lease.Request).SequenceEqual(lease.Canonical) Then
            lease.Faulted = True
            Throw New InvalidOperationException("تغيرت هوية/مدخلات الطلب بعد حجزه؛ يجب إلغاء المعاملة.")
        End If
    End Sub

    Friend Shared Function Command(name As String, cn As SqlConnection, tx As SqlTransaction) As SqlCommand
        Return New SqlCommand(name, cn, tx) With {.CommandType = CommandType.StoredProcedure, .CommandTimeout = 30}
    End Function

    Friend Shared Sub P(cmd As SqlCommand, name As String, type As SqlDbType, size As Integer, value As Object)
        Dim parameter As SqlParameter = If(size = 0, cmd.Parameters.Add(name, type), cmd.Parameters.Add(name, type, size))
        parameter.Value = If(value, DBNull.Value)
    End Sub

    Friend Shared Sub Identity(cmd As SqlCommand, request As AuditOperationRequest)
        P(cmd, "@SourceId", SqlDbType.UniqueIdentifier, 0, request.Context.SourceId)
        P(cmd, "@RequestId", SqlDbType.UniqueIdentifier, 0, request.RequestId)
    End Sub

    Private Shared Function Snapshot(request As AuditOperationRequest) As AuditOperationRequest
        AuditContract.Request(request)
        Dim copy As New AuditOperationRequest With {.RequestId = request.RequestId, .OperationCode = request.OperationCode,
            .Context = request.Context.Copy(), .EntityType = request.EntityType, .EntityId = request.EntityId}
        For Each input As AuditInput In request.Inputs
            copy.Inputs.Add(New AuditInput With {.Name = input.Name, .ValueType = input.ValueType, .Value = input.Value})
        Next
        Return copy
    End Function

    Friend Shared Function Begin(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest, Optional lockTimeout As Integer = 5000) As AuditRequestLease
        RequireTransaction(cn, tx)
        If lockTimeout < 0 OrElse lockTimeout > 10000 Then Throw New ArgumentOutOfRangeException("lockTimeout")
        Dim copy = Snapshot(request)
        Dim bytes = AuditCanonical.RequestBytes(copy)
        Using cmd = Command("dbo.Network_Audit_Request_Begin", cn, tx)
            Identity(cmd, copy)
            P(cmd, "@OperationCode", SqlDbType.VarChar, 64, copy.OperationCode)
            P(cmd, "@DomainCode", SqlDbType.VarChar, 64, AuditContract.Domain(copy.OperationCode))
            P(cmd, "@ActorKind", SqlDbType.TinyInt, 0, CByte(copy.Context.ActorKind))
            P(cmd, "@ActorUserId", SqlDbType.Int, 0, copy.Context.ActorUserId)
            P(cmd, "@ActorServiceCode", SqlDbType.VarChar, 64, copy.Context.ActorServiceCode)
            P(cmd, "@SessionId", SqlDbType.UniqueIdentifier, 0, copy.Context.SessionId)
            P(cmd, "@EntityType", SqlDbType.VarChar, 64, copy.EntityType)
            P(cmd, "@EntityId", SqlDbType.NVarChar, 128, copy.EntityId)
            Fingerprint(cmd, bytes)
            P(cmd, "@LockTimeout", SqlDbType.Int, 0, lockTimeout)
            Return New AuditRequestLease(cn, tx, copy, bytes, ReadResult(cmd))
        End Using
    End Function

    Private Shared Sub Fingerprint(cmd As SqlCommand, bytes As Byte())
        P(cmd, "@RequestFingerprint", SqlDbType.VarBinary, 32, AuditCanonical.Hash(bytes))
        P(cmd, "@CanonicalRequest", SqlDbType.VarBinary, -1, bytes)
    End Sub

    Private Shared Function ReadResult(cmd As SqlCommand) As AuditRequestResult
        Using rd = cmd.ExecuteReader()
            If Not rd.Read() Then Throw New InvalidOperationException("لم يعد إجراء المراقبة نتيجة صريحة.")
            Return New AuditRequestResult With {.Status = CStr(rd("Status")),
                .ResultCode = If(IsDBNull(rd("ResultCode")), CType(Nothing, Byte?), CByte(rd("ResultCode"))),
                .ResultXml = If(IsDBNull(rd("ResultXml")), Nothing, AuditCanonical.LimitedXml(AuditCanonical.ParseXml(CStr(rd("ResultXml")))))}
        End Using
    End Function

    Friend Shared Function Complete(cn As SqlConnection, tx As SqlTransaction, lease As AuditRequestLease, resultCode As Byte,
                                    Optional values As IDictionary(Of String, String) = Nothing) As String
        RequireLease(cn, tx, lease)
        Try
            If resultCode < 1 OrElse resultCode > 5 Then Throw New ArgumentException("نتيجة طلب غير معتمدة.")
            If resultCode = 5 AndAlso (lease.Events.Count > 0 OrElse AuditContract.Kind(lease.Request.OperationCode) <> 1) Then Throw New ArgumentException("لا تغيير متاح لأعمال بلا أحداث تغيير فقط.")
            Dim root As New XElement("Result", New XAttribute("version", "1"), New XAttribute("code", resultCode))
            Dim fields As New XElement("Values")
            If values IsNot Nothing Then
                For Each field In values.OrderBy(Function(x) x.Key, StringComparer.Ordinal)
                    If Not {"EntityId", "DocumentNumber", "LineId"}.Contains(field.Key, StringComparer.Ordinal) Then Throw New ArgumentException("حقل نتيجة غير معتمد.")
                    AuditContract.Text(field.Value, 128, "ResultValue", True)
                    fields.Add(New XElement("Value", New XAttribute("name", field.Key), field.Value))
                Next
            End If
            root.Add(fields)
            root.Add(New XElement("Events", lease.Events.Select(Function(x) New XElement("Event", New XAttribute("ordinal", x.Key), New XAttribute("id", x.Value.ToString("D"))))))
            Dim xml = AuditCanonical.LimitedXml(root)
            Using cmd = Command("dbo.Network_Audit_Request_Complete", cn, tx)
                Identity(cmd, lease.Request)
                P(cmd, "@ResultCode", SqlDbType.TinyInt, 0, resultCode)
                P(cmd, "@ResultXml", SqlDbType.Xml, -1, xml)
                cmd.ExecuteNonQuery()
            End Using
            lease.Finished = True
            Return xml ' Still pending the CALLER's Commit; never report success to UI yet.
        Catch
            lease.Faulted = True
            Throw
        End Try
    End Function

    Friend Shared Function Status(connectionString As String, request As AuditOperationRequest, Optional lockTimeout As Integer = 3000) As AuditRequestResult
        Dim bytes = AuditCanonical.RequestBytes(request)
        If lockTimeout < 0 OrElse lockTimeout > 10000 Then Throw New ArgumentOutOfRangeException("lockTimeout")
        Using cn As New SqlConnection(connectionString)
            cn.Open()
            Using cmd = Command("dbo.Network_Audit_Request_Status", cn, Nothing)
                Identity(cmd, request)
                Fingerprint(cmd, bytes)
                P(cmd, "@LockTimeout", SqlDbType.Int, 0, lockTimeout)
                Return ReadResult(cmd)
            End Using
        End Using
        ' Connection/SQL errors propagate: never turn an unavailable status into NotFound.
    End Function

    Friend Shared Function ServerUtcNow(cn As SqlConnection, tx As SqlTransaction) As DateTime
        RequireTransaction(cn, tx)
        Using cmd As New SqlCommand("SELECT CONVERT(datetime2(3),SYSUTCDATETIME())", cn, tx)
            Return DateTime.SpecifyKind(CDate(cmd.ExecuteScalar()), DateTimeKind.Utc)
        End Using
    End Function
End Class
