Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq

Friend Module AuditAccountEntryTests
    Private checks As Integer
    Private connection As String
    Private candidate As String
    Private context As AuditEventContext
    Private Const First As Integer = 2147475000
    Private ReadOnly Tables As String() = {"Agents_Balance_MV", "Treasury_Balance_MV", "Agents_Balance_MV_RCT", "SERVERS", "Users", "Users_Validations", "Tables", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change", "Network_Edit_Tracker"}
    Private ReadOnly Procedures As String() = {"Agents_insert_Prev_Balance", "AA_GET_MAX_ID", "AG_Balance_Void_Row", "Agents_Balance_MV_Cancel", "Agents_Balance_MV_RCT_VOID", "Network_Audit_Request_Begin", "Network_Audit_Event_Write"}
    Public Function Main(args As String()) As Integer
        Try
            Check(AuditContract.Domain("AccountEntry.Created") = "Finance.AccountEntries", "closed entry domain")
            Check(AuditContract.AccountEntryFields().Length = 25, "complete entry fields")
            For Each kind In {5, 6, 37}
                Check(AuditContract.VoidSpec(kind)(2) = "Add_Prev_Balance", "closed cancellation kind " & kind)
            Next
            If args.Length >= 2 Then
                If args.Length = 4 Then candidate = File.ReadAllText(args(3))
                Live(args(1))
            End If
            Console.WriteLine("PASS " & checks & " account entry checks.") : Return 0
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
        Return Integer.Parse(AuditCanonical.ParseXml(r.ResultXml).Element("Values").Elements("Value").Single(Function(x) CStr(x.Attribute("name")) = "EntityId").Value, CultureInfo.InvariantCulture)
    End Function

    Private Function Request(Optional kind As Integer = 37, Optional amount As Decimal = 17.1236D) As AuditOperationRequest
        Dim cmd As New SqlCommand("Agents_insert_Prev_Balance")
        cmd.Parameters.AddWithValue("@AG_ID", 4) : cmd.Parameters.AddWithValue("@Type_ID", kind)
        cmd.Parameters.AddWithValue("@Balance", amount) : cmd.Parameters.AddWithValue("@Date", New DateTime(2026, 10, 27, 1, 2, 3, 127, DateTimeKind.Unspecified))
        cmd.Parameters.AddWithValue("@NOTES", "رصيد اختبار المراقبة 030") : cmd.Parameters.AddWithValue("@User_ID", 1)
        Return AuditAccountEntry.Request(context, cmd)
    End Function
    Private Function VoidRequest(cn As SqlConnection, tx As SqlTransaction, id As Integer, kind As Integer) As AuditOperationRequest
        Return AuditDocumentVoid.Request(context, AuditDocumentVoid.Capture(cn, tx, id, kind))
    End Function
    Private Sub Fixture(work As Action(Of SqlConnection, SqlTransaction))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    If candidate IsNot Nothing Then Q(cn, tx, candidate)
                    Q(cn, tx, "UPDATE SERVERS SET START_ID=" & First & ",END_ID=" & (First + 99))
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
        Dim b As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS AccountEntry030 rollback", .Pooling = False, .ConnectTimeout = 5}
        If b.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database")
        connection = b.ConnectionString
        context = New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "030", .ScreenCode = "Add_Prev_Balance"}
        Dim fingerprints As String(), definitions As String(), domains As String
        Dim queries = Tables.Select(Function(t) "SELECT * FROM dbo.[" & t & "] ORDER BY " & If(t = "SERVERS", "START_ID", If(t = "Tables", "TB_ID", If(t = "Users", "User_ID", If(t = "Network_Audit_Change", "EventId,ChangeNo", If(t = "Network_Audit_Event", "EventId", If(t = "Network_Audit_Request", "RequestId", "T_ID"))))))).ToArray()
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other clients")
            context.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM Network_Audit_Source WHERE ConfigId=1"), Guid)
            Check(context.SourceId = Guid.Parse("3c520084-0214-43d4-a5fb-797a179c7a7c"), "approved source")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM Agents WHERE AG_ID=4")) = 1, "reference account")
            For Each table In {"Agents_Balance_MV", "Agents_Balance_MV_RCT", "Treasury_Balance_MV"}
                Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM [" & table & "] WHERE T_ID BETWEEN " & First & " AND " & (First + 99))) = 0, "test range " & table)
            Next
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditAccountFail030')") Is DBNull.Value, "trigger name free")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('Agents_Balance_MV'),OBJECT_ID('Agents_Balance_MV_RCT'),OBJECT_ID('Treasury_Balance_MV'))")) = 0, "no unreviewed triggers")
            fingerprints = queries.Select(Function(sql) Finger(cn, sql)).ToArray()
            definitions = Procedures.Select(Function(p) CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & p & "'))"))).ToArray()
            domains = CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM Network_Audit_Source WHERE ConfigId=1"))
        End Using
        Try
            For Each kind In {5, 6, 37}
                Dim k = kind
                Fixture(Sub(cn, tx)
                            Dim r = Request(k), id = ResultId(AuditAccountEntry.ExecuteInTransaction(cn, tx, r))
                            Check(id = First + 1, "allocation " & k)
                            Dim row = AuditAccountEntry.Capture(cn, tx, id).Rows(0)
                            Check(CDec(row("Total")) = 17.124D AndAlso CDec(row("Pure")) = 17.124D, "rounded amount")
                            Check(CDec(row(If(k = 6, "Credit", "Debit"))) = 17.124D, "proper side")
                            Check(CInt(row("BsType_ID")) = k AndAlso CInt(row("AG_ID")) = 4 AndAlso CInt(row("isDepended")) = 1, "stored type account approval")
                            Check(CStr(row("Receipt_Title")) = "رصيد اختبار المراقبة 030", "stored title")
                            Check(ResultId(AuditAccountEntry.ExecuteInTransaction(cn, tx, r)) = id, "create replay")
                            Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM Network_Audit_Change c JOIN Network_Audit_Event e ON c.EventId=e.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "'")) = 25, "complete changes")
                            Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM Treasury_Balance_MV WHERE AGBalance_T_ID=" & id)) = 0, "no treasury creation")
                            Dim vr = VoidRequest(cn, tx, id, k), stale = VoidRequest(cn, tx, id, k)
                            AuditDocumentVoid.ExecuteInTransaction(cn, tx, vr)
                            Check(CInt(Q(cn, tx, "SELECT isVoid FROM Agents_Balance_MV WHERE T_ID=" & id)) = 1, "cancellation")
                            Check(AuditDocumentVoid.ExecuteInTransaction(cn, tx, vr).Status = "Completed", "cancellation replay")
                            Reject(Sub() AuditDocumentVoid.ExecuteInTransaction(cn, tx, stale), "stale cancellation")
                        End Sub)
                For Each action In {"create", "void"}
                    For Each failure In {"audit", "business"}
                        Dim act = action, fail = failure
                        Fixture(Sub(cn, tx)
                                    Dim r = Request(k)
                                    If act = "void" Then r = VoidRequest(cn, tx, ResultId(AuditAccountEntry.ExecuteInTransaction(cn, tx, r)), k)
                                    Q(cn, tx, "CREATE TRIGGER dbo.AuditAccountFail030 ON dbo." & If(fail = "audit", "Network_Audit_Event AFTER INSERT", "Agents_Balance_MV AFTER " & If(act = "create", "INSERT", "UPDATE")) & " AS BEGIN THROW 51130,'forced entry failure',1; END")
                                    Reject(Sub()
                                               If act = "create" Then
                                                   AuditAccountEntry.ExecuteInTransaction(cn, tx, r)
                                               Else
                                                   AuditDocumentVoid.ExecuteInTransaction(cn, tx, r)
                                               End If
                                           End Sub, act & " " & fail & " failure " & k)
                                End Sub)
                    Next
                Next
                Fixture(Sub(cn, tx)
                            Dim id = ResultId(AuditAccountEntry.ExecuteInTransaction(cn, tx, Request(k)))
                            Q(cn, tx, "UPDATE Agents_Balance_MV SET JournalId=123,NeedRepost=1 WHERE T_ID=" & id)
                            AuditDocumentVoid.ExecuteInTransaction(cn, tx, VoidRequest(cn, tx, id, k))
                            Check(CBool(Q(cn, tx, "SELECT NeedCancelReverse FROM Agents_Balance_MV WHERE T_ID=" & id)), "posted entry needs reverse")
                            Check(CInt(Q(cn, tx, "SELECT VoidBy FROM Agents_Balance_MV WHERE T_ID=" & id)) = 1, "deferred cancellation semantics retained")
                        End Sub)
            Next
            For Each mutation In {"create", "void"}
                Dim act = mutation
                Fixture(Sub(cn, tx)
                            Dim r = Request()
                            If act = "void" Then r = VoidRequest(cn, tx, ResultId(AuditAccountEntry.ExecuteInTransaction(cn, tx, r)), 37)
                            Q(cn, tx, "UPDATE Users_Validations SET [" & If(act = "create", "Agents", "Balance") & "]=0 WHERE T_ID=(SELECT Valid_ID FROM Users WHERE User_ID=1)")
                            Reject(Sub()
                                       If act = "create" Then
                                           AuditAccountEntry.ExecuteInTransaction(cn, tx, r)
                                       Else
                                           AuditDocumentVoid.ExecuteInTransaction(cn, tx, r)
                                       End If
                                   End Sub, "permission revoked " & act)
                        End Sub)
            Next
            Fixture(Sub(cn, tx)
                        Dim r = Request()
                        Q(cn, tx, "UPDATE Users SET is_Allow=0 WHERE User_ID=1")
                        Reject(Sub() AuditAccountEntry.ExecuteInTransaction(cn, tx, r), "disabled user")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request()
                        Q(cn, tx, "UPDATE Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code=""Finance.AccountEntries""]') WHERE ConfigId=1")
                        Reject(Sub() AuditAccountEntry.ExecuteInTransaction(cn, tx, r), "disabled domain no fallback")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request()
                        r.Context.ScreenCode = "Receipt"
                        Reject(Sub() AuditAccountEntry.ExecuteInTransaction(cn, tx, r), "wrong screen")
                        Reject(Sub() Request(34), "custody excluded")
                        Reject(Sub() Request(amount:=0D), "zero rejected")
                        Reject(Sub() Request(amount:=-1D), "negative rejected")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request()
                        AuditAccountEntry.ExecuteInTransaction(cn, tx, r)
                        r.Inputs.Single(Function(x) x.Name = "DocumentPure").Value = AuditValue.Capture("Decimal", 9D)
                        Reject(Sub() AuditAccountEntry.ExecuteInTransaction(cn, tx, r), "request identity conflict")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request()
                        r.Inputs.Single(Function(x) x.Name = "AccountId").Value = AuditValue.Capture("Integer", First)
                        Reject(Sub() AuditAccountEntry.ExecuteInTransaction(cn, tx, r), "missing account")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Q(cn, tx, "UPDATE SERVERS SET END_ID=START_ID")
                        Reject(Sub() AuditAccountEntry.ExecuteInTransaction(cn, tx, Request()), "exhausted allocation")
                    End Sub)
            Fixture(Sub(cn, tx)
                        AuditAccountEntry.ExecuteInTransaction(cn, tx, Request())
                        Using other As New SqlConnection(connection)
                            other.Open()
                            Reject(Sub() Q(other, Nothing, "SET LOCK_TIMEOUT 800; INSERT Agents_Balance_MV(T_ID,AG_ID,Date,BsType_ID,isDepended,isVoid,User_ID) VALUES(" & (First + 2) & ",4,GETDATE(),37,1,0,1)"), "concurrent allocation locked", "Lock request time out")
                        End Using
                    End Sub)
        Finally
            Using cn As New SqlConnection(connection)
                cn.Open()
                For i = 0 To Tables.Length - 1
                    Check(Finger(cn, queries(i)) = fingerprints(i), "rollback table " & Tables(i))
                Next
                For i = 0 To Procedures.Length - 1
                    Check(CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & Procedures(i) & "'))")) = definitions(i), "unchanged procedure " & Procedures(i))
                Next
                Check(CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM Network_Audit_Source WHERE ConfigId=1")) = domains, "domains unchanged")
                Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditAccountFail030')") Is DBNull.Value, "no trigger left")
            End Using
        End Try
    End Sub
End Module
