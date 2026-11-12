Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Text.RegularExpressions

Friend Module AuditGateLiveTests
    Private Function Q(cn As SqlConnection, tx As SqlTransaction, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn, tx) With {.CommandTimeout = 30}
            Return cmd.ExecuteScalar()
        End Using
    End Function

    Private Sub Check(value As Boolean, label As String)
        If Not value Then Throw New Exception(label)
    End Sub

    Private Function Id(result As AuditRequestResult) As Integer
        Return Integer.Parse(AuditCanonical.ParseXml(result.ResultXml).Element("Values").Elements("Value").Single(Function(v) CStr(v.Attribute("name")) = "EntityId").Value, CultureInfo.InvariantCulture)
    End Function

    Private Sub Verify(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest)
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Network_Audit_Request r WHERE r.RequestId=@Id AND r.State=1 AND EXISTS(SELECT 1 FROM dbo.Network_Audit_Event e JOIN dbo.Network_Audit_Change c ON c.EventId=e.EventId WHERE e.RequestId=r.RequestId AND e.EventCode=@Code)", cn, tx)
            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = request.RequestId
            cmd.Parameters.Add("@Code", SqlDbType.VarChar, 64).Value = request.OperationCode
            Check(CInt(cmd.ExecuteScalar()) = 1, "completed request, event and changes: " & request.OperationCode)
        End Using
    End Sub

    Public Function Main(args As String()) As Integer
        Try
            Dim connection = Environment.GetEnvironmentVariable(args(0))
            Dim builder As New SqlConnectionStringBuilder(connection) With {.Pooling = False, .ApplicationName = "CPOS Audit Gate Live Tests"}
            If Not Regex.IsMatch(builder.InitialCatalog, "\ACPOS_AUDIT_GATE_[A-Za-z0-9_]+\z") Then Throw New Exception("Production database refused")
            Dim operations As New HashSet(Of String)(StringComparer.Ordinal)
            Using cn As New SqlConnection(builder.ConnectionString)
                cn.Open()
                Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.extended_properties WHERE class=0 AND name=N'CPOS.AuditReleaseSandbox' AND CONVERT(nvarchar(100),value)=DB_NAME()")) = 1, "sandbox ownership marker required")
                Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "close candidate database clients")
                Dim context As New AuditEventContext With {.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid),
                    .ActorKind = AuditActorKind.User, .ActorUserId = CInt(Q(cn, Nothing, "SELECT TOP(1) u.User_ID FROM dbo.Users u JOIN dbo.Users_Validations_V v ON v.User_ID=u.User_ID WHERE u.is_Allow=1 AND v.IM=1 ORDER BY u.User_ID")),
                    .ActorName = "AuditReleaseTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditReleaseTests", .ApplicationVersion = "ReleaseGate1", .ScreenCode = "Units"}
                Dim initialUnits = CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.Units"))
                Dim initialRequests = CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Request"))
                For Each culture In {"ar-LY", "en-US", "fr-FR"}
                    Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(culture)
                    Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                        Try
                            Dim request = AuditUnits.Request(context, "Unit.Created", Nothing, "اختبار بوابة " & Guid.NewGuid().ToString("N"), 6.528R)
                            Dim result = AuditUnits.ExecuteInTransaction(cn, tx, request), unitId = Id(result)
                            Dim row = AuditUnits.Capture(cn, tx, unitId)
                            Check(row IsNot Nothing AndAlso CDbl(row("U_Cargo")) = 6.528R, "create actual unit")
                            Verify(cn, tx, request) : operations.Add(request.OperationCode)
                            Check(Id(AuditUnits.ExecuteInTransaction(cn, tx, request)) = unitId, "idempotent retry identity")
                            request = AuditUnits.Request(context, "Unit.Updated", row, "1000", 3R)
                            AuditUnits.ExecuteInTransaction(cn, tx, request)
                            row = AuditUnits.Capture(cn, tx, unitId)
                            Check(CStr(row("U_Name")) = "1000" AndAlso CDbl(row("U_Cargo")) = 3R, "update numeric-looking name and value")
                            Verify(cn, tx, request) : operations.Add(request.OperationCode)
                            request = AuditUnits.Request(context, "Unit.Deleted", row, "", 1R)
                            AuditUnits.ExecuteInTransaction(cn, tx, request)
                            Check(AuditUnits.Capture(cn, tx, unitId) Is Nothing, "delete actual unit")
                            Verify(cn, tx, request) : operations.Add(request.OperationCode)
                        Finally
                            If tx.Connection IsNot Nothing Then tx.Rollback()
                        End Try
                    End Using
                Next
                Check(CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.Units")) = initialUnits, "all unit fixtures rolled back")
                Check(CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Request")) = initialRequests, "all audit fixtures rolled back")
            End Using
            For Each operation In operations.OrderBy(Function(v) v)
                Console.WriteLine("COVERED=" & operation)
            Next
            Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString())
            Return 1
        End Try
    End Function
End Module
