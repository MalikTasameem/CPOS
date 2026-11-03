Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq

Friend Module AuditFinanceTests
    Private checks As Integer
    Private connection As String
    Private candidate As String
    Private context As AuditEventContext
    Private Const First As Integer = 2147480000
    Private ReadOnly VerifySql As String() = {
        "SELECT * FROM dbo.Treasury_Balance_MV ORDER BY T_ID", "SELECT * FROM dbo.AG_MV_TRANSACTION ORDER BY T_ID",
        "SELECT * FROM dbo.Agents_Balance_MV ORDER BY T_ID", "SELECT * FROM dbo.Agents_Balance_MV_RCT ORDER BY T_ID",
        "SELECT * FROM dbo.TreasuryCard ORDER BY Tr_ID", "SELECT * FROM dbo.SERVERS ORDER BY START_ID",
        "SELECT User_ID,is_Allow,Valid_ID FROM dbo.Users ORDER BY User_ID",
        "SELECT T_ID,Tr_Widraw,Tr_Deposit,Tr_Convert FROM dbo.Users_Validations ORDER BY T_ID"}

    Public Function Main(args As String()) As Integer
        Try
            Check(AuditContract.FinanceFields("FinanceTreasuryRow").Length = 33, "33 treasury fields")
            Check(AuditContract.FinanceFields("FinanceAccountRow").Length = 25, "25 account fields")
            Check(AuditContract.Domain("TreasuryTransfer.Created") = "Finance.Transfers", "closed finance domain")
            Reject(Sub() AuditContract.FinanceInputs("Finance.Arbitrary"), "unknown operation denied")
            If args.Length >= 2 Then
                If args.Length = 4 Then candidate = File.ReadAllText(args(3))
                Live(args(1))
            End If
            Console.WriteLine("PASS " & checks & " finance checks.") : Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString()) : Return 1
        End Try
    End Function
    Private Sub Check(value As Boolean, label As String)
        If Not value Then Throw New Exception("FAILED " & label)
        checks += 1 : Console.WriteLine("PASS " & label)
    End Sub
    Private Sub Reject(work As Action, label As String, Optional expected As String = "")
        Try
            work()
        Catch ex As Exception
            If expected.Length > 0 AndAlso Not ex.ToString().Contains(expected) Then Throw New Exception("Unexpected rejection " & label, ex)
            Check(True, label) : Return
        End Try
        Throw New Exception("Expected rejection " & label)
    End Sub
    Private Function Q(cn As SqlConnection, tx As SqlTransaction, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn, tx) With {.CommandTimeout = 30}
            Return cmd.ExecuteScalar()
        End Using
    End Function
    Private Function Finger(cn As SqlConnection, tx As SqlTransaction, sql As String) As String
        Return AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, tx, sql))
    End Function
    Private Function ResultId(r As AuditRequestResult) As Integer
        Return Integer.Parse(AuditCanonical.ParseXml(r.ResultXml).Element("Values").Elements("Value").Single(Function(x) CStr(x.Attribute("name")) = "EntityId").Value, CultureInfo.InvariantCulture)
    End Function
    Private Function Command(mode As String, Optional id As Integer = 0, Optional amount As Decimal = 5.55555D) As SqlCommand
        Dim account = mode = "account", transfer = mode = "transfer", create = id = 0
        Dim c As New SqlCommand(If(account, "[AG_MV_TRANSACTION_pros]", If(transfer, If(create, "Treasury_BalanceMV_Transfer", "[Treasury_BalanceMV_Trans_isVoid]"), If(create, "Treasury_BalanceMV_insert", "[Treasury_BalanceMV_isVoid]"))))
        c.Parameters.AddWithValue(If(transfer, "@Tran_ID", "@T_ID"), id)
        If account Then c.Parameters.AddWithValue("@Process", If(create, "", "DELETE"))
        If create Then
            c.Parameters.AddWithValue("Date", New DateTime(2026, 10, 1, 12, 34, 56, DateTimeKind.Unspecified))
            c.Parameters.AddWithValue("@User_ID", 1)
            c.Parameters.AddWithValue(If(account, "@ABOUT ", "@About"), "تفاصيل اختبار مالي")
            If account Then c.Parameters.AddWithValue("@Title", "تحويل اختبار")
            If account OrElse transfer Then
                c.Parameters.AddWithValue(If(account, "@AG_ID_FROM", "@F_Tr_ID"), 1)
                c.Parameters.AddWithValue(If(account, "@AG_ID_TO", "@T_Tr_ID"), 2)
                c.Parameters.AddWithValue("@Value", amount)
            Else
                c.Parameters.AddWithValue("@Tr_ID", 1)
                c.Parameters.AddWithValue("@Tr_Type_ID", If(mode = "withdraw", 1, 2))
                c.Parameters.AddWithValue(If(mode = "withdraw", "@Debit", "@Credit"), amount)
                c.Parameters.AddWithValue("@isFirst_Time", mode = "opening")
            End If
        End If
        Return c
    End Function
    Private Function Request(cn As SqlConnection, tx As SqlTransaction, mode As String, Optional id As Integer = 0, Optional amount As Decimal = 5.55555D) As AuditOperationRequest
        Dim cmd = Command(mode, id, amount), op = AuditFinance.Operation(cmd)
        context.ScreenCode = AuditContract.FinanceScreen(op)
        Return AuditFinance.Request(context, cmd, If(id = 0, Nothing, AuditFinance.Capture(cn, tx, op, id)))
    End Function
    Private Sub Fixture(work As Action(Of SqlConnection, SqlTransaction))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    If candidate IsNot Nothing Then Q(cn, tx, candidate)
                    Q(cn, tx, "UPDATE dbo.SERVERS SET START_ID=" & First & ",END_ID=" & (First + 99))
                    work(cn, tx)
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
    End Sub
    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim builder As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS Finance025 rollback", .ConnectTimeout = 5, .Pooling = False}
        If builder.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database")
        connection = builder.ConnectionString
        context = New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "025", .ScreenCode = "Tr_Transfers"}
        Dim definitions As String(), stored As String(), domains As String, counts As Long()
        Dim objects = {"Treasury_BalanceMV_insert", "Treasury_BalanceMV_isVoid", "Treasury_BalanceMV_Transfer", "Treasury_BalanceMV_Trans_isVoid", "AG_MV_TRANSACTION_pros", "AA_GET_MAX_ID", "AA_GET_MAX_Treasury_Balance_MV", "AG_BalanceMV_GetMax_Treasury_Tran_ID", "Network_Audit_Request_Begin", "Network_Audit_Event_Write"}
        Dim counted = {"Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change", "Network_Edit_Tracker"}
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other clients")
            context.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid)
            Check(context.SourceId = Guid.Parse("3c520084-0214-43d4-a5fb-797a179c7a7c"), "approved source")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.Treasury_Balance_MV WHERE T_ID BETWEEN " & First & " AND " & (First + 99) & " OR Tran_ID BETWEEN " & First & " AND " & (First + 99))) = 0, "unused treasury range")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.AG_MV_TRANSACTION WHERE T_ID BETWEEN " & First & " AND " & (First + 99))) = 0, "unused account range")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditFinanceFail025')") Is DBNull.Value, "failure trigger unused")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('Treasury_Balance_MV'),OBJECT_ID('AG_MV_TRANSACTION'))")) = 0, "no unknown finance triggers")
            definitions = objects.Select(Function(x) CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & x & "'))"))).ToArray()
            stored = VerifySql.Select(Function(x) Finger(cn, Nothing, x)).ToArray()
            counts = counted.Select(Function(x) CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & x))).ToArray()
            domains = CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1"))
        End Using
        For Each mode In {"deposit", "withdraw", "opening", "transfer", "account"}
            For Each amount In {5.55555D, 1.23449D, -7.235D}
                Dim current = mode, value = amount
                Fixture(Sub(cn, tx)
                            Dim r = Request(cn, tx, current, 0, value), result = AuditFinance.ExecuteInTransaction(cn, tx, r), id = ResultId(result)
                            Dim data = AuditFinance.Capture(cn, tx, r.OperationCode, id), account = current = "account", transfer = current = "transfer"
                            Dim expected = If(account, Decimal.Round(value, 3, MidpointRounding.AwayFromZero), If(transfer, Decimal.Round(Decimal.Round(value, 4, MidpointRounding.AwayFromZero), 3, MidpointRounding.AwayFromZero), Decimal.Round(value, 2, MidpointRounding.AwayFromZero)))
                            Check(id = First + 1 AndAlso data.Rows.Count = If(transfer, 2, 1), "allocated result " & current)
                            Check(CDec(data.Rows(0)(If(account, "VALUE", If(current = "withdraw" OrElse transfer, "Debit", "Credit")))) = expected, "legacy SQL rounding " & current & value.ToString(CultureInfo.InvariantCulture))
                            If transfer Then Check(CDec(data.Rows(1)("Credit")) = expected AndAlso CInt(data.Rows(0)("Tr_ID")) = 1 AndAlso CInt(data.Rows(1)("Tr_ID")) = 2, "paired transfer directions")
                            If current = "opening" Then Check(CInt(data.Rows(0)("isFirst_Time")) = 1, "opening flag preserved")
                            Dim captured = If(account, 25, If(transfer, 66, 33))
                            Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "'")) = captured, "all persisted fields " & current)
                            Check(ResultId(AuditFinance.ExecuteInTransaction(cn, tx, r)) = id, "idempotent allocation " & current)
                            Dim cancel = Request(cn, tx, current, id), stale = Request(cn, tx, current, id)
                            AuditFinance.ExecuteInTransaction(cn, tx, cancel)
                            Check(AuditFinance.Capture(cn, tx, cancel.OperationCode, id).Rows.Cast(Of DataRow)().All(Function(x) Convert.ToBoolean(x(If(account, "is_Void", "isVoid")))), "all sides void " & current)
                            Check(AuditFinance.ExecuteInTransaction(cn, tx, cancel).Status = "Completed", "void replay " & current)
                            Reject(Sub() AuditFinance.ExecuteInTransaction(cn, tx, stale), "stale cancellation " & current, "تغير")
                            Reject(Sub() Request(cn, tx, current, id), "already void " & current, "ملغاة")
                            r.Inputs.Single(Function(x) x.Name = "RequestedAmount").Value = AuditValue.Capture("Decimal", value + 1D)
                            Reject(Sub() AuditFinance.ExecuteInTransaction(cn, tx, r), "replay payload mismatch " & current)
                        End Sub)
            Next
        Next
        For Each mode In {"deposit", "withdraw", "transfer", "account"}
            For Each test In {"disabled", "permission", "domain", "screen", "reference", "zero"}
                Dim current = mode, scenario = test
                Fixture(Sub(cn, tx)
                            Dim r = Request(cn, tx, current)
                            Select Case scenario
                                Case "disabled" : Q(cn, tx, "UPDATE dbo.Users SET is_Allow=0 WHERE User_ID=1")
                                Case "permission"
                                    Q(cn, tx, "UPDATE dbo.Users_Validations SET " & If(current = "deposit", "Tr_Deposit", If(current = "withdraw", "Tr_Widraw", "Tr_Convert")) & "=0 WHERE T_ID=(SELECT Valid_ID FROM dbo.Users WHERE User_ID=1)")
                                Case "domain" : Q(cn, tx, "UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code=""Finance.Transfers""]') WHERE ConfigId=1")
                                Case "screen" : r.Context.ScreenCode = "POS"
                                Case "reference" : r.Inputs.Single(Function(x) x.Name = If(current = "deposit" OrElse current = "withdraw", "TreasuryId", "ToId")).Value = AuditValue.Capture("Integer", First)
                                Case "zero" : r.Inputs.Single(Function(x) x.Name = "RequestedAmount").Value = AuditValue.Capture("Decimal", 0D)
                            End Select
                            Reject(Sub() AuditFinance.ExecuteInTransaction(cn, tx, r), current & " reject " & scenario)
                        End Sub)
            Next
        Next
        For Each mode In {"transfer", "account"}
            Dim current = mode
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, current)
                        r.Inputs.Single(Function(x) x.Name = "ToId").Value = AuditValue.Capture("Integer", 1)
                        Reject(Sub() AuditFinance.ExecuteInTransaction(cn, tx, r), "same account " & current)
                    End Sub)
        Next
        For Each mode In {"deposit", "withdraw", "transfer", "account"}
            Dim current = mode
            For Each stage In {"create-event", "void-event", "legacy"}
                Dim failure = stage
                Fixture(Sub(cn, tx)
                            Dim r = Request(cn, tx, current)
                            If failure = "void-event" Then r = Request(cn, tx, current, ResultId(AuditFinance.ExecuteInTransaction(cn, tx, r)))
                            Dim target = If(failure = "legacy", If(current = "account", "AG_MV_TRANSACTION", "Treasury_Balance_MV"), "Network_Audit_Event")
                            Q(cn, tx, "CREATE TRIGGER dbo.AuditFinanceFail025 ON dbo." & target & " AFTER INSERT AS BEGIN THROW 51125,'forced finance failure',1; END")
                            Reject(Sub() AuditFinance.ExecuteInTransaction(cn, tx, r), "atomic failure " & current & " " & failure)
                        End Sub)
            Next
        Next
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, "transfer"), id = ResultId(AuditFinance.ExecuteInTransaction(cn, tx, r))
                    ' Direct movement screen must capture and void both sides, as its old proc does.
                    Dim cancel = Request(cn, tx, "withdraw", id)
                    AuditFinance.ExecuteInTransaction(cn, tx, cancel)
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Treasury_Balance_MV WHERE Tran_ID=" & id & " AND isVoid=1")) = 2, "direct cancellation captures linked transfer")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & cancel.RequestId.ToString() & "'")) = 66, "linked direct cancellation both rows")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim id = ResultId(AuditFinance.ExecuteInTransaction(cn, tx, Request(cn, tx, "deposit")))
                    Dim r = Request(cn, tx, "deposit", id)
                    Q(cn, tx, "UPDATE dbo.Treasury_Balance_MV SET Credit=99 WHERE T_ID=" & id)
                    Reject(Sub() AuditFinance.ExecuteInTransaction(cn, tx, r), "stale amount")
                    Q(cn, tx, "UPDATE dbo.Treasury_Balance_MV SET AGBalance_T_ID=1 WHERE T_ID=" & id)
                    Reject(Sub() Request(cn, tx, "deposit", id), "receipt-linked movement denied")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim id = ResultId(AuditFinance.ExecuteInTransaction(cn, tx, Request(cn, tx, "account")))
                    Dim r = Request(cn, tx, "account", id)
                    Using other As New SqlConnection(connection)
                        other.Open()
                        Reject(Sub() Q(other, Nothing, "SET LOCK_TIMEOUT 800; UPDATE dbo.AG_MV_TRANSACTION SET VALUE=99 WHERE T_ID=" & id), "competing update blocked", "Lock request time out")
                    End Using
                    AuditFinance.ExecuteInTransaction(cn, tx, r)
                    Check(CInt(Q(cn, tx, "SELECT is_Void FROM dbo.AG_MV_TRANSACTION WHERE T_ID=" & id)) = 1, "locked cancel succeeded")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, "account")
                    Q(cn, tx, "UPDATE dbo.SERVERS SET END_ID=START_ID")
                    Reject(Sub() AuditFinance.ExecuteInTransaction(cn, tx, r), "bad allocation range")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim id = ResultId(AuditFinance.ExecuteInTransaction(cn, tx, Request(cn, tx, "transfer")))
                    For n = 3 To 4
                        Q(cn, tx, "INSERT dbo.Treasury_Balance_MV(T_ID,Tran_ID,[date],Tr_ID,notice_move,Debit,Tr_Type_ID,User_ID,isVoid) VALUES(" & (First + n) & "," & id & ",'20261001',1,N'طرف قديم إضافي',1,1,1,0)")
                    Next
                    Dim r = Request(cn, tx, "transfer", id)
                    AuditFinance.ExecuteInTransaction(cn, tx, r)
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Treasury_Balance_MV WHERE Tran_ID=" & id & " AND isVoid=1")) = 4, "legacy transfer group preserved")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) = 2, "large cancellation paged")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "'")) = 132, "all four rows captured")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim id = ResultId(AuditFinance.ExecuteInTransaction(cn, tx, Request(cn, tx, "transfer")))
                    For n = 3 To 4
                        Q(cn, tx, "INSERT dbo.Treasury_Balance_MV(T_ID,Tran_ID,Tr_ID,Debit,Tr_Type_ID,User_ID,isVoid) VALUES(" & (First + n) & "," & id & ",1,1,1,1,0)")
                    Next
                    Dim r = Request(cn, tx, "transfer", id)
                    Q(cn, tx, "CREATE TRIGGER dbo.AuditFinanceFail025 ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN IF EXISTS(SELECT 1 FROM inserted WHERE EventOrdinal=2) THROW 51125,'forced second finance part',1; END")
                    Reject(Sub() AuditFinance.ExecuteInTransaction(cn, tx, r), "second part failure atomic", "forced second finance part")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim cmd = Command("account")
                    cmd.Parameters("@T_ID").Value = 1
                    Reject(Sub() AuditFinance.Operation(cmd), "upsert not exposed")
                    cmd.Parameters("@T_ID").Value = 0
                    cmd.Parameters("@User_ID").Value = 2
                    context.ScreenCode = "AG_Transfers"
                    Reject(Sub() AuditFinance.Request(context, cmd, Nothing), "actor substitution denied")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, "deposit")
                    Reject(Sub()
                               r.Inputs.Single(Function(x) x.Name = "Description").Value = AuditValue.Capture("String", New String("x"c, 4097))
                               AuditFinance.ExecuteInTransaction(cn, tx, r)
                           End Sub, "long text never truncated")
                End Sub)
        Using cn As New SqlConnection(connection)
            cn.Open()
            For i = 0 To VerifySql.Length - 1
                Check(Finger(cn, Nothing, VerifySql(i)) = stored(i), "original data unchanged " & i)
            Next
            For i = 0 To objects.Length - 1
                Check(CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & objects(i) & "'))")) = definitions(i), "definition unchanged " & objects(i))
            Next
            For i = 0 To counted.Length - 1
                Check(CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & counted(i))) = counts(i), "rollback count " & counted(i))
            Next
            Check(CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = domains, "domain rollback")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.Network_Audit_Request WHERE State<>1")) = 0, "no incomplete request")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditFinanceFail025')") Is DBNull.Value, "failure trigger removed")
        End Using
    End Sub
End Module
