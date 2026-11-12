Option Strict On
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Friend Module AgentCardAccessTests
    Private checks As Integer
    Private Sub Check(ok As Boolean, label As String)
        If Not ok Then Throw New Exception("FAILED " & label)
        checks += 1
        Console.WriteLine("PASS " & label)
    End Sub
    Private Sub Denied(work As Action, label As String)
        Try
            work()
        Catch ex As UnauthorizedAccessException
            Check(True, label)
            Return
        End Try
        Throw New Exception("Expected permission denial: " & label)
    End Sub
    Private Function Query(cn As SqlConnection, tx As SqlTransaction, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn, tx)
            Return cmd.ExecuteScalar()
        End Using
    End Function
    Private Sub LegacySave(cn As SqlConnection, tx As SqlTransaction, procedure As String, Optional roleId As Integer = 0)
        Using cmd As New SqlCommand(procedure, cn, tx) With {.CommandType = CommandType.StoredProcedure}
            SqlCommandBuilder.DeriveParameters(cmd)
            For Each parameter As SqlParameter In cmd.Parameters
                If parameter.Direction <> ParameterDirection.Input Then Continue For
                Select Case parameter.ParameterName.ToLowerInvariant()
                    Case "@valid_name" : parameter.Value = "Audit045 permission test"
                    Case "@t_id" : parameter.Value = roleId
                    Case Else
                        If parameter.SqlDbType <> SqlDbType.Bit Then Throw New Exception("Unexpected legacy permission parameter")
                        parameter.Value = True
                End Select
            Next
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function Main(args As String()) As Integer
        Try
            Dim connection = Environment.GetEnvironmentVariable(args(0))
            Dim builder As New SqlConnectionStringBuilder(connection)
            If Not Text.RegularExpressions.Regex.IsMatch(builder.InitialCatalog, "^CPOS_AUDIT_GATE_[a-f0-9]{32}$") Then Throw New Exception("Sandbox only")
            Using cn As New SqlConnection(connection)
                cn.Open()
                Check(CInt(Query(cn, Nothing, "SELECT COUNT(*) FROM sys.extended_properties WHERE class=0 AND name=N'CPOS.AuditReleaseSandbox' AND CONVERT(nvarchar(100),value)=DB_NAME()")) = 1, "sandbox ownership")
                Dim user = CInt(Query(cn, Nothing, "SELECT TOP(1) User_ID FROM dbo.Users ORDER BY User_ID"))
                Dim role = CInt(Query(cn, Nothing, "SELECT TOP(1) T_ID FROM dbo.Users_Validations ORDER BY T_ID"))
                Dim baseline = CBool(Query(cn, Nothing, "SELECT Open_Agent_Card FROM dbo.Users_Validations WHERE T_ID=" & role))
                Check(AgentCardAccessPolicy.LoadValidation(connection, role) = baseline, "load saved permission")
                Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                    Try
                        Query(cn, tx, "UPDATE dbo.Users SET isAdmin=0,is_Allow=1,Valid_ID=" & role & " WHERE User_ID=" & user)
                        AgentCardAccessPolicy.SaveValidation(cn, tx, role, True)
                        AgentCardAccessPolicy.EnsureAllowed(cn, tx, user)
                        Check(True, "active non-admin allowed")
                        Dim otherFlags = Convert.ToString(Query(cn, tx, "SELECT CONCAT(Agents,':',Balance,':',Sales,':',Pch) FROM dbo.Users_Validations WHERE T_ID=" & role))
                        AgentCardAccessPolicy.SaveValidation(cn, tx, role, False)
                        Denied(Sub() AgentCardAccessPolicy.EnsureAllowed(cn, tx, user), "revocation rechecked without relogin")
                        Check(otherFlags = Convert.ToString(Query(cn, tx, "SELECT CONCAT(Agents,':',Balance,':',Sales,':',Pch) FROM dbo.Users_Validations WHERE T_ID=" & role)), "other permissions unchanged")
                        Dim migration = File.ReadAllText(args(1)).Replace("DB_NAME()<>N'CPOS_ACCOUNTING'", "DB_NAME()<>N'" & builder.InitialCatalog & "'")
                        Query(cn, tx, migration)
                        Check(Not CBool(Query(cn, tx, "SELECT Open_Agent_Card FROM dbo.Users_Validations WHERE T_ID=" & role)), "migration rerun preserves denied permission")
                        Query(cn, tx, "UPDATE dbo.Users SET isAdmin=1 WHERE User_ID=" & user)
                        AgentCardAccessPolicy.EnsureAllowed(cn, tx, user)
                        Check(True, "administrator override retained")
                        Query(cn, tx, "UPDATE dbo.Users SET is_Allow=0 WHERE User_ID=" & user)
                        Denied(Sub() AgentCardAccessPolicy.EnsureAllowed(cn, tx, user), "disabled administrator denied")
                        Denied(Sub() AgentCardAccessPolicy.EnsureAllowed(cn, tx, -1), "invalid user denied")
                        Denied(Sub() AgentCardAccessPolicy.EnsureAllowed(cn, tx, Integer.MaxValue), "missing user denied")
                        Query(cn, tx, "UPDATE dbo.Users_Validations SET Open_Agent_Card=DEFAULT WHERE T_ID=" & role)
                        Check(CBool(Query(cn, tx, "SELECT Open_Agent_Card FROM dbo.Users_Validations WHERE T_ID=" & role)), "default allows access")
                        Query(cn, tx, "UPDATE dbo.Users SET isAdmin=0,is_Allow=1 WHERE User_ID=" & user)
                        AgentCardAccessPolicy.EnsureAllowed(cn, tx, user)
                        Check(True, "permission can be restored")
                        Dim previousMax = CInt(Query(cn, tx, "SELECT MAX(T_ID) FROM dbo.Users_Validations WITH(UPDLOCK,HOLDLOCK)"))
                        LegacySave(cn, tx, "dbo.User_Valid_Insert")
                        Dim newRole = CInt(Query(cn, tx, "SELECT MAX(T_ID) FROM dbo.Users_Validations WITH(UPDLOCK,HOLDLOCK)"))
                        Check(newRole > previousMax, "legacy role creation still works")
                        Check(CBool(Query(cn, tx, "SELECT Open_Agent_Card FROM dbo.Users_Validations WHERE T_ID=" & newRole)), "new role defaults to allowed")
                        AgentCardAccessPolicy.SaveValidation(cn, tx, newRole, False)
                        LegacySave(cn, tx, "dbo.User_Valid_Update", newRole)
                        Check(Not CBool(Query(cn, tx, "SELECT Open_Agent_Card FROM dbo.Users_Validations WHERE T_ID=" & newRole)), "legacy role update preserves independent restriction")
                        AgentCardAccessPolicy.SaveValidation(cn, tx, newRole, True)
                        Check(CBool(Query(cn, tx, "SELECT Open_Agent_Card FROM dbo.Users_Validations WHERE T_ID=" & newRole)), "new role permission saved in same transaction")
                    Finally
                        tx.Rollback()
                    End Try
                End Using
                Check(AgentCardAccessPolicy.LoadValidation(connection, role) = baseline, "all permission test writes rolled back")
            End Using
            Console.WriteLine("PASS " & checks & " agent card permission checks")
            Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString())
            Return 1
        End Try
    End Function
End Module
