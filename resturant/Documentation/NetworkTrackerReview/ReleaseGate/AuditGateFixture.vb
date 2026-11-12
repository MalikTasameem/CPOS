' Included ONLY in copied test sources; never compiled into CPOS.
Friend NotInheritable Class AuditGateFixture
    Friend Shared Function Settings() As String()
        Return {"SqlConStr:" & System.Environment.GetEnvironmentVariable("CPOS_AUDIT_GATE_FIXTURE_CONNECTION")}
    End Function

    Friend Shared Sub Record(cn As System.Data.SqlClient.SqlConnection, tx As System.Data.SqlClient.SqlTransaction, lease As AuditRequestLease, resultCode As Byte)
        Dim path = System.Environment.GetEnvironmentVariable("CPOS_AUDIT_GATE_FIXTURE_COVERAGE")
        If String.IsNullOrEmpty(path) OrElse lease.Request.OperationCode = "AuditTest.Changed" OrElse resultCode = 5 Then Return
        If Not System.Text.RegularExpressions.Regex.IsMatch(cn.Database, "\ACPOS_AUDIT_GATE_[a-f0-9]{32}\z") Then Throw New System.Exception("Fixture escaped sandbox")
        Using cmd As New System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM dbo.Network_Audit_Request r WHERE r.RequestId=@id AND r.State=1 AND EXISTS(SELECT 1 FROM dbo.Network_Audit_Event e WHERE e.RequestId=r.RequestId AND e.EventCode=@code)", cn, tx)
            cmd.Parameters.Add("@id", System.Data.SqlDbType.UniqueIdentifier).Value = lease.Request.RequestId
            cmd.Parameters.Add("@code", System.Data.SqlDbType.VarChar, 64).Value = lease.Request.OperationCode
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New System.Exception("Completed operation has no persisted matching event")
        End Using
        SyncLock GetType(AuditGateFixture)
            System.IO.File.AppendAllText(path, lease.Request.OperationCode & System.Environment.NewLine)
        End SyncLock
    End Sub
End Class
