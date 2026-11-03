Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq

Friend Module AuditExpenseTests
    Private checks As Integer
    Private connection As String
    Private candidate As String
    Private context As AuditEventContext
    Private Const First As Integer = 2147476000
    Private expense As Integer
    Private ReadOnly Tables As String() = {"Agents_Balance_MV", "EXP_Details", "Treasury_Balance_MV", "Agents_Balance_MV_RCT", "SERVERS", "Expenses_Card", "Users", "Users_Validations", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change", "Network_Edit_Tracker"}
    Private ReadOnly Procedures As String() = {"EXP_Details_Insert", "EXP_Details_Delete", "AA_GET_MAX_EXP_Details", "AG_Balance_Update_Total", "AG_Balance_Update_Pied_On_Update", "Recalculate_Agent_Bill_Total", "Network_Audit_Request_Begin", "Network_Audit_Event_Write"}

    Public Function Main(args As String()) As Integer
        Try
            Check(AuditContract.Domain("Expense.LineAdded") = "Expenses.Lines", "closed expense domain")
            Check(AuditContract.ExpenseFields("ExpenseLine").Length = 7, "all seven expense row values")
            Reject(Sub() AuditContract.ExpenseFields("SB_Contents"), "unrelated entity rejected")
            If args.Length >= 2 Then
                If args.Length = 4 Then candidate = File.ReadAllText(args(3))
                Live(args(1))
            End If
            Console.WriteLine("PASS " & checks & " expense line checks.") : Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString()) : Return 1
        End Try
    End Function

    Private Sub Check(ok As Boolean, label As String)
        If Not ok Then Throw New Exception("FAILED " & label)
        checks += 1 : Console.WriteLine("PASS " & label)
    End Sub
    Private Sub Reject(work As Action, label As String, Optional expected As String = "")
        Try
            work()
        Catch ex As Exception
            If expected <> "" AndAlso Not ex.ToString().Contains(expected) Then Throw New Exception("Unexpected failure " & label, ex)
            Check(True, label) : Return
        End Try
        Throw New Exception("Expected rejection " & label)
    End Sub
    Private Function Q(cn As SqlConnection, tx As SqlTransaction, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn, tx) With {.CommandTimeout = 60}
            Return cmd.ExecuteScalar()
        End Using
    End Function
    Private Function Finger(cn As SqlConnection, sql As String) As String
        Dim dt = AuditSalesQuantity.Rows(cn, Nothing, sql)
        dt.TableName = "Baseline"
        Using writer As New StringWriter(CultureInfo.InvariantCulture)
            dt.WriteXml(writer, XmlWriteMode.WriteSchema)
            Return BitConverter.ToString(AuditCanonical.Hash(Text.Encoding.UTF8.GetBytes(writer.ToString()))).Replace("-", "")
        End Using
    End Function
    Private Function ResultId(r As AuditRequestResult) As Integer
        Return Integer.Parse(AuditCanonical.ParseXml(r.ResultXml).Element("Values").Elements("Value").Single(Function(x) CStr(x.Attribute("name")) = "LineId").Value, CultureInfo.InvariantCulture)
    End Function
    Private Function Request(cn As SqlConnection, tx As SqlTransaction, Optional line As Integer = 0, Optional price As Decimal = 3.4567D, Optional update As Boolean = False) As AuditOperationRequest
        Dim cmd As New SqlCommand(If(line = 0, "EXP_Details_Insert", "EXP_Details_Delete"))
        cmd.Parameters.AddWithValue("@On_Update", update)
        If line = 0 Then
            cmd.Parameters.AddWithValue("@IMEX_T_ID", First + 20)
            cmd.Parameters.AddWithValue("@EX_ID", expense)
            cmd.Parameters.AddWithValue("@QYT", 2.1256D)
            cmd.Parameters.AddWithValue("@Price", price)
            cmd.Parameters.AddWithValue("@Total", price * 2.1256D)
        Else
            cmd.Parameters.AddWithValue("@T_ID", line)
        End If
        Dim r = AuditExpenseLines.Request(context, cmd, First + 20)
        r.Inputs.Single(Function(x) x.Name = "ExpectedExpenseSnapshot").Value = AuditValue.Capture("String", AuditExpenseLines.Fingerprint(AuditExpenseLines.Capture(cn, tx, r)))
        Return r
    End Function

    Private Sub Fixture(work As Action(Of SqlConnection, SqlTransaction))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    If candidate IsNot Nothing Then Q(cn, tx, candidate)
                    Q(cn, tx, "UPDATE SERVERS SET START_ID=" & First & ",END_ID=" & (First + 99))
                    Q(cn, tx, "INSERT Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Pure,Discount,EXP_ID) VALUES(" & (First + 20) & ",1,'20261027',2,0,0,1,0,0,0,29)")
                    work(cn, tx)
                Finally
                    Try
                        If tx.Connection IsNot Nothing Then tx.Rollback()
                    Catch
                    End Try
                End Try
            End Using
        End Using
    End Sub

    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim builder As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS Expense029 rollback", .Pooling = False, .ConnectTimeout = 5}
        If builder.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database")
        connection = builder.ConnectionString
        context = New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "029", .ScreenCode = "EXP_Details"}
        Dim fingerprints As String(), definitions As String(), domains As String
        Dim queries = Tables.Select(Function(t) "SELECT * FROM dbo.[" & t & "] ORDER BY " & If(t = "SERVERS", "START_ID", If(t = "Expenses_Card", "Ex_ID", If(t = "Users", "User_ID", If(t = "Network_Audit_Change", "EventId,ChangeNo", If(t = "Network_Audit_Event", "EventId", If(t = "Network_Audit_Request", "RequestId", "T_ID"))))))).ToArray()
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other clients")
            context.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM Network_Audit_Source WHERE ConfigId=1"), Guid)
            Check(context.SourceId = Guid.Parse("3c520084-0214-43d4-a5fb-797a179c7a7c"), "approved source")
            expense = CInt(Q(cn, Nothing, "SELECT MIN(Ex_ID) FROM Expenses_Card"))
            Check(expense > 0, "existing expense reference")
            For Each table In {"Agents_Balance_MV", "EXP_Details"}
                Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM [" & table & "] WHERE T_ID BETWEEN " & First & " AND " & (First + 99))) = 0, "test range free " & table)
            Next
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditExpenseFail029')") Is DBNull.Value, "test trigger name free")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('EXP_Details'),OBJECT_ID('Agents_Balance_MV'))")) = 0, "no unreviewed triggers")
            fingerprints = queries.Select(Function(qry) Finger(cn, qry)).ToArray()
            definitions = Procedures.Select(Function(p) CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & p & "'))"))).ToArray()
            domains = CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM Network_Audit_Source WHERE ConfigId=1"))
        End Using
        Try
            For Each update In {False, True}
                Dim edit = update
                For Each price In {0D, 3.4567D}
                    Dim p = price
                    Fixture(Sub(cn, tx)
                                If edit Then Q(cn, tx, "UPDATE Agents_Balance_MV SET isDepended=1,EditOpenedAt=GETDATE(),EditOpenedBy=1 WHERE T_ID=" & (First + 20))
                                Dim r = Request(cn, tx, price:=p, update:=edit), id = ResultId(AuditExpenseLines.ExecuteInTransaction(cn, tx, r))
                                Check(id = First + 1, "locked allocator")
                                Dim row = AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM EXP_Details WHERE T_ID=@P0", id).Rows(0)
                                Check(CDec(row("QYT")) = 2.126D, "SQL quantity rounding")
                                Check(CDec(row("Price")) = Decimal.Round(p, 3, MidpointRounding.AwayFromZero), "SQL price rounding")
                                Check(CDec(row("Total")) = Decimal.Round(p * 2.1256D, 3, MidpointRounding.AwayFromZero), "original total rounding")
                                Check(CBool(row("isDepended")) = edit, "immediate approval effect")
                                Check(CDec(Q(cn, tx, "SELECT Total FROM Agents_Balance_MV WHERE T_ID=" & r.EntityId)) = CDec(row("Total")), "header recalculated")
                                Check(ResultId(AuditExpenseLines.ExecuteInTransaction(cn, tx, r)) = id, "add retry same result")
                                Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM Network_Audit_Change c JOIN Network_Audit_Event e ON c.EventId=e.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "'")) = 30, "complete header and line")
                                Dim deletion = Request(cn, tx, id, update:=edit), stale = Request(cn, tx, id, update:=edit)
                                AuditExpenseLines.ExecuteInTransaction(cn, tx, deletion)
                                Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM EXP_Details WHERE T_ID=" & id)) = 0, "own line removed")
                                Check(CDec(Q(cn, tx, "SELECT Total FROM Agents_Balance_MV WHERE T_ID=" & r.EntityId)) = 0D, "deleted total recalculated")
                                Check(AuditExpenseLines.ExecuteInTransaction(cn, tx, deletion).Status = "Completed", "delete retry")
                                Reject(Sub() AuditExpenseLines.ExecuteInTransaction(cn, tx, stale), "stale deletion")
                            End Sub)
                Next
            Next
            For Each mutation In {"add", "delete"}
                Dim action = mutation
                For Each failure In {"audit", "business"}
                    Dim side = failure
                    Fixture(Sub(cn, tx)
                                Dim r = Request(cn, tx)
                                If action = "delete" Then r = Request(cn, tx, ResultId(AuditExpenseLines.ExecuteInTransaction(cn, tx, r)))
                                Q(cn, tx, "CREATE TRIGGER dbo.AuditExpenseFail029 ON dbo." & If(side = "audit", "Network_Audit_Event AFTER INSERT", "EXP_Details AFTER " & If(action = "add", "INSERT", "DELETE")) & " AS BEGIN THROW 51129,'forced expense failure',1; END")
                                Reject(Sub() AuditExpenseLines.ExecuteInTransaction(cn, tx, r), side & " failure " & action)
                            End Sub)
                Next
            Next
            For Each change In {"Discount=1", "isVoid=1", "BsType_ID=7", "isDepended=1"}
                Dim sql = change
                Fixture(Sub(cn, tx)
                            Dim r = Request(cn, tx)
                            Q(cn, tx, "UPDATE Agents_Balance_MV SET " & sql & " WHERE T_ID=" & r.EntityId)
                            Reject(Sub() AuditExpenseLines.ExecuteInTransaction(cn, tx, r), "stale or invalid header " & sql)
                        End Sub)
            Next
            For Each edit In {False, True}
                Dim isEdit = edit
                Fixture(Sub(cn, tx)
                            Dim r = Request(cn, tx, update:=isEdit)
                            Q(cn, tx, "UPDATE Users_Validations SET [" & If(isEdit, "ExpEdit", "Exp") & "]=0 WHERE T_ID=(SELECT Valid_ID FROM Users WHERE User_ID=1)")
                            Reject(Sub() AuditExpenseLines.ExecuteInTransaction(cn, tx, r), "permission revoked")
                        End Sub)
            Next
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx)
                        Q(cn, tx, "UPDATE Users SET is_Allow=0 WHERE User_ID=1")
                        Reject(Sub() AuditExpenseLines.ExecuteInTransaction(cn, tx, r), "disabled user")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx)
                        Q(cn, tx, "UPDATE Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code=""Expenses.Lines""]') WHERE ConfigId=1")
                        Reject(Sub() AuditExpenseLines.ExecuteInTransaction(cn, tx, r), "domain disabled no legacy fallback")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx)
                        r.Context.ScreenCode = "Sales"
                        Reject(Sub() AuditExpenseLines.ExecuteInTransaction(cn, tx, r), "wrong screen")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx)
                        AuditExpenseLines.ExecuteInTransaction(cn, tx, r)
                        r.Inputs.Single(Function(x) x.Name = "ExpenseTotal").Value = AuditValue.Capture("Decimal", 999D)
                        Reject(Sub() AuditExpenseLines.ExecuteInTransaction(cn, tx, r), "request identity conflict")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx)
                        Q(cn, tx, "UPDATE SERVERS SET END_ID=START_ID")
                        Reject(Sub() AuditExpenseLines.ExecuteInTransaction(cn, tx, r), "allocation exhausted")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx)
                        Using other As New SqlConnection(connection)
                            other.Open()
                            Reject(Sub() Q(other, Nothing, "SET LOCK_TIMEOUT 800; UPDATE Agents_Balance_MV SET Discount=99 WHERE T_ID=" & r.EntityId), "concurrent change locked", "Lock request time out")
                        End Using
                        Check(ResultId(AuditExpenseLines.ExecuteInTransaction(cn, tx, r)) > 0, "locked request saved")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim id = ResultId(AuditExpenseLines.ExecuteInTransaction(cn, tx, Request(cn, tx))), r = Request(cn, tx, id)
                        Q(cn, tx, "UPDATE EXP_Details SET IMEX_T_ID=" & (First + 30) & " WHERE T_ID=" & id)
                        Reject(Sub() AuditExpenseLines.ExecuteInTransaction(cn, tx, r), "line moved to another invoice")
                    End Sub)
        Finally
            Using cn As New SqlConnection(connection)
                cn.Open()
                For i = 0 To Tables.Length - 1
                    Check(Finger(cn, queries(i)) = fingerprints(i), "rollback table " & Tables(i))
                Next
                For i = 0 To Procedures.Length - 1
                    Check(CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & Procedures(i) & "'))")) = definitions(i), "procedure unchanged " & Procedures(i))
                Next
                Check(CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM Network_Audit_Source WHERE ConfigId=1")) = domains, "domains unchanged")
                Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditExpenseFail029')") Is DBNull.Value, "no trigger left")
            End Using
        End Try
    End Sub
End Module
