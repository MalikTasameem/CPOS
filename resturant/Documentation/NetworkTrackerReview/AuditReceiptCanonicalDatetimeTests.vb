Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Reflection
Imports System.Text.RegularExpressions

' Marked test restores only. Business changes and the old-writer reproduction always roll back.
Friend Module AuditReceiptCanonicalDatetimeTests
    Private checks As Integer
    Private Sub Check(ok As Boolean, label As String)
        If Not ok Then Throw New Exception("FAIL " & label)
        checks += 1
        Console.WriteLine("PASS " & label)
    End Sub
    Private Function Q(cn As SqlConnection, tx As SqlTransaction, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn, tx)
            cmd.CommandTimeout = 60
            Return cmd.ExecuteScalar()
        End Using
    End Function
    Private Function CallService(name As String, ParamArray args As Object()) As Object
        Try
            Return GetType(AuditReceipt).GetMethod(name, BindingFlags.Static Or BindingFlags.NonPublic).Invoke(Nothing, args)
        Catch ex As TargetInvocationException
            Throw ex.InnerException
        End Try
    End Function
    Private Function Digest(cn As SqlConnection) As String
        Dim parts As New List(Of String)
        For Each table In {"Agents_Balance_MV_RCT", "Treasury_Balance_MV", "Agents_Balance_MV", "Custody", "SERVERS", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change", "Network_Audit_Source"}
            Using cmd As New SqlCommand("SELECT * FROM dbo.[" & table & "] ORDER BY 1", cn), reader = cmd.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(reader)
                parts.Add(table & ":" & AuditReceipt.Fingerprint(dt))
            End Using
        Next
        Return String.Join("|", parts)
    End Function
    Private Function Request(context As AuditEventContext, op As String, kind As Integer, id As Integer, agent As Integer, treasury As Integer, payment As Integer) As AuditOperationRequest
        Dim proc = If(op = "Receipt.Created", "Agents_BalanceMV_insert_RCT", If(op = "Receipt.Updated", "Agents_BalanceMV_Update_RCT", If(op = "Receipt.EditOpened", "Agents_Balance_MV_RCT_OpenForEdit", "Agents_Balance_MV_RCT_VOID")))
        Using cmd As New SqlCommand(proc) With {.CommandType = CommandType.StoredProcedure}
            cmd.Parameters.AddWithValue("@T_ID", id)
            If op <> "Receipt.Voided" Then cmd.Parameters.AddWithValue("@User_ID", context.ActorUserId.Value)
            If op = "Receipt.Created" OrElse op = "Receipt.Updated" Then
                For Each p In New Dictionary(Of String, Object) From {
                    {"Date", New DateTime(2026, 11, 10, 20, 0, 0, If(op = "Receipt.Created", 293, 997))},
                    {"AG_ID", agent}, {"Pr_ID", 0}, {"Receipt_Tran_ID", 0}, {"Receipt_Title", "اختبار الإيصال 046"},
                    {"Pure", If(op = "Receipt.Created", 50D, 75.1235D)}, {"Discount", 0D}, {"About", DBNull.Value},
                    {"BsType_ID", kind}, {"Tr_ID", treasury}, {"Pay_ID", payment}}
                    cmd.Parameters.AddWithValue("@" & p.Key, p.Value)
                Next
            End If
            Return AuditReceipt.Request(context, cmd)
        End Using
    End Function
    ' Equivalent read-only preparation on the SAME transaction, so test receipts need no commit.
    Private Sub Prepare(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest)
        CallService("Guard", cn, tx)
        If r.OperationCode = "Receipt.Created" Then
            Dim ids = DirectCast(CallService("Allocation", cn, tx), Integer())
            Dim names = {"PlannedReceiptId", "PlannedReceiptNumber", "PlannedTreasuryId"}
            For i = 0 To names.Length - 1
                Dim name = names(i)
                r.Inputs.Single(Function(x) x.Name = name).Value = AuditValue.Capture("Integer", ids(i))
            Next
        End If
        Dim before = AuditReceipt.Capture(cn, tx, r)
        CallService("Permission", cn, tx, r, before)
        r.Inputs.Single(Function(x) x.Name = "ExpectedReceipt").Value = AuditValue.Capture("String", AuditReceipt.Fingerprint(before))
        AuditContract.Request(r)
    End Sub
    Private Sub LegacyReceiptComparison(cn As SqlConnection, tx As SqlTransaction, writer As String)
        Dim startAt = writer.IndexOf("-- AuditReceipts041: ordinary receipts only", StringComparison.Ordinal)
        Dim endAt = writer.IndexOf("INSERT dbo.Network_Audit_Event(EventId", startAt, StringComparison.Ordinal)
        Check(startAt >= 0 AndAlso endAt > startAt, "receipt writer anchors")
        Dim receipt = writer.Substring(startAt, endAt - startAt)
        Dim fixedValue = "(CASE WHEN TRY_CONVERT(datetime2(7),c.AfterValue,126)<=TRY_CONVERT(datetime2(7),'9999-12-31T23:59:59.9970000',126) THEN TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) END)"
        Check((receipt.Length - receipt.Replace(fixedValue, "").Length) \ fixedValue.Length = 20, "all 20 receipt date comparisons covered by 046")
        receipt = receipt.Replace(fixedValue, "TRY_CONVERT(datetime,c.AfterValue,126)")
        Dim trial = writer.Substring(0, startAt) & receipt & writer.Substring(endAt)
        trial = Regex.Replace(trial, "\b(?:CREATE|ALTER)\s+PROCEDURE\b", "ALTER PROCEDURE", RegexOptions.IgnoreCase)
        Using cmd As New SqlCommand(trial, cn, tx)
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Sub Main(args As String())
        If args.Length <> 1 Then Throw New ArgumentException("Supply the connection environment variable name.")
        Dim connection = Environment.GetEnvironmentVariable(args(0))
        If String.IsNullOrWhiteSpace(connection) Then Throw New ArgumentException("Connection environment variable is empty.")
        Dim builder As New SqlConnectionStringBuilder(connection)
        If Not Regex.IsMatch(builder.InitialCatalog, "\ACPOS_AUDIT_GATE_[a-f0-9]{32}\z") Then Throw New Exception("Production database refused.")
        builder.Pooling = False
        AuditReceiptServiceTests.Main(New String() {})
        Using cn As New SqlConnection(builder.ConnectionString)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.extended_properties WHERE class=0 AND name=N'CPOS.AuditReleaseSandbox' AND CONVERT(nvarchar(100),value)=DB_NAME()")) = 1, "owned test restore")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other test clients")
            Dim writer = CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))"))
            Check(writer.Contains("-- Audit046: canonical seven-digit datetime validation."), "046 installed")
            Dim original = Digest(cn)
            Dim context As New AuditEventContext With {.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid), .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "Audit046ReceiptTest", .SessionId = Guid.NewGuid(), .MachineName = "Audit046ReceiptTest", .ApplicationVersion = "046", .ScreenCode = "Receipt"}
            Dim agent = CInt(Q(cn, Nothing, "SELECT MIN(AG_ID) FROM dbo.Agents")), treasury = CInt(Q(cn, Nothing, "SELECT MIN(Tr_ID) FROM dbo.TreasuryCard")), payment = CInt(Q(cn, Nothing, "SELECT ISNULL(MIN(P_ID),1) FROM dbo.PAYMENT_METHOD"))
            For Each legacy In {True, False}
                For Each kind In {3, 4}
                    Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                        Try
                            If legacy Then LegacyReceiptComparison(cn, tx, writer)
                            Dim create = Request(context, "Receipt.Created", kind, 0, agent, treasury, payment)
                            Prepare(cn, tx, create)
                            If legacy Then
                                Dim rejected As Boolean
                                Try
                                    AuditReceipt.ExecuteInTransaction(cn, tx, create)
                                Catch ex As SqlException
                                    If Not ex.Message.Contains("ReceiptHeader after-state differs from persistent receipt data.") Then Throw
                                    rejected = True
                                End Try
                                Check(rejected, "reproduced exact pre-046 header error, receipt type " & kind)
                            Else
                                Dim id = Integer.Parse(create.Inputs.Single(Function(x) x.Name = "PlannedReceiptId").Value.Text, CultureInfo.InvariantCulture)
                                For Each op In {"Receipt.Created", "Receipt.EditOpened", "Receipt.Updated", "Receipt.Voided"}
                                    Dim r = If(op = "Receipt.Created", create, Request(context, op, kind, id, agent, treasury, payment))
                                    If op <> "Receipt.Created" Then Prepare(cn, tx, r)
                                    Dim result = AuditReceipt.ExecuteInTransaction(cn, tx, r)
                                    Check(result.ResultCode.GetValueOrDefault() = 1, op & " success, type " & kind)
                                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) = 2, op & " complete header and treasury events")
                                    Check(AuditReceipt.ExecuteInTransaction(cn, tx, r).ResultCode.GetValueOrDefault() = 1 AndAlso CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) = 2, op & " replay does not duplicate")
                                Next
                                Check(CInt(Q(cn, tx, "SELECT isVoid FROM dbo.Agents_Balance_MV_RCT WHERE T_ID=" & id.ToString(CultureInfo.InvariantCulture))) = 1, "void business state present, type " & kind)
                            End If
                        Finally
                            If tx.Connection IsNot Nothing Then tx.Rollback()
                        End Try
                    End Using
                    Check(Digest(cn) = original, "all business and audit data rolled back")
                    Check(CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))")) = writer, "writer unchanged after rollback")
                Next
            Next
        End Using
        Console.WriteLine("PASS " & checks & " live receipt datetime checks; no persistent changes.")
    End Sub
End Module
