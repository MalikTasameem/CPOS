Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq

Friend Module AuditEmployeeValuesTests
    Private checks As Integer
    Private connection As String
    Private candidate As String
    Private context As AuditEventContext
    Private ReadOnly BusinessObjects As String() = {"Others_Values_Insert", "Others_Values_Update", "Others_Values_Void", "EMP_Insert_Salaries", "Network_Audit_Request_Begin", "Network_Audit_Event_Write"}
    Public Function Main(args As String()) As Integer
        Try
            Check(AuditContract.EmployeeValueFields().Length = 10, "complete value row")
            Check(AuditContract.Domain("EmployeeValue.Created") = "People.EmployeeValues", "closed employee domain")
            Reject(Sub() AuditContract.EmployeeValueInputs("EmployeeValue.DeleteAll"), "unknown operation")
            If args.Length >= 2 Then
                If args.Length = 4 Then candidate = File.ReadAllText(args(3))
                Live(args(1))
            End If
            Console.WriteLine("PASS " & checks & " employee value checks.") : Return 0
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
            If expected <> "" AndAlso Not ex.ToString().Contains(expected) Then Throw New Exception("Unexpected rejection " & label, ex)
            Check(True, label) : Return
        End Try
        Throw New Exception("Expected rejection " & label)
    End Sub
    Private Function Q(cn As SqlConnection, tx As SqlTransaction, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn, tx) With {.CommandTimeout = 30}
            Return cmd.ExecuteScalar()
        End Using
    End Function
    Private Function Finger(cn As SqlConnection, sql As String) As String
        Dim dt = AuditSalesQuantity.Rows(cn, Nothing, sql)
        dt.TableName = "Baseline"
        Using sw As New StringWriter(CultureInfo.InvariantCulture)
            dt.WriteXml(sw, XmlWriteMode.WriteSchema)
            Return BitConverter.ToString(AuditCanonical.Hash(Text.Encoding.UTF8.GetBytes(sw.ToString()))).Replace("-", "")
        End Using
    End Function
    Private Function ResultId(r As AuditRequestResult) As Integer
        Return CInt(AuditCanonical.ParseXml(r.ResultXml).Element("Values").Elements("Value").Single(Function(x) CStr(x.Attribute("name")) = "EntityId").Value)
    End Function
    Private Function Request(cn As SqlConnection, tx As SqlTransaction, Optional id As Integer = 0, Optional cancel As Boolean = False, Optional amount As Decimal = 1.2345D, Optional valueType As Integer = 0) As AuditOperationRequest
        Dim cmd As New SqlCommand(If(id = 0, "Others_Values_Insert", If(cancel, "Others_Values_Void", "Others_Values_Update")))
        If id <> 0 Then cmd.Parameters.AddWithValue("@T_ID", id)
        If Not cancel Then
            cmd.Parameters.AddWithValue("@DateOF_Order", New DateTime(2026, 10, 27, 1, 2, 3, 127, DateTimeKind.Unspecified))
            cmd.Parameters.AddWithValue("@YEAR", 2026) : cmd.Parameters.AddWithValue("@MONTH", 10)
            cmd.Parameters.AddWithValue("@AG_ID", 4) : cmd.Parameters.AddWithValue("@ValType", valueType)
            cmd.Parameters.AddWithValue("@Value", amount) : cmd.Parameters.AddWithValue("@About", "قيمة اختبار 027")
            cmd.Parameters.AddWithValue("@User_ID", 1)
        End If
        Return AuditEmployeeValues.Request(context, cmd, If(id = 0, Nothing, AuditEmployeeValues.Capture(cn, tx, id)))
    End Function
    Private Sub Fixture(work As Action(Of SqlConnection, SqlTransaction))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    If candidate IsNot Nothing Then Q(cn, tx, candidate)
                    context.ScreenCode = "Emp_OtherValues"
                    work(cn, tx)
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
    End Sub
    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim b As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS Employee027 rollback", .ConnectTimeout = 5, .Pooling = False}
        If b.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database")
        connection = b.ConnectionString
        context = New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "027", .ScreenCode = "Emp_OtherValues"}
        Dim data As String(), defs As String(), domain As String
        Dim sqls = {"SELECT * FROM Others_Values ORDER BY T_ID", "SELECT * FROM Agents_Balance_MV ORDER BY T_ID", "SELECT * FROM Network_Audit_Request ORDER BY RequestId", "SELECT * FROM Network_Audit_Event ORDER BY EventId", "SELECT * FROM Network_Audit_Change ORDER BY EventId,ChangeNo", "SELECT User_ID,is_Allow,Valid_ID FROM Users ORDER BY User_ID", "SELECT T_ID,Balance FROM Users_Validations ORDER BY T_ID"}
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other clients")
            context.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM Network_Audit_Source WHERE ConfigId=1"), Guid)
            Check(context.SourceId = Guid.Parse("3c520084-0214-43d4-a5fb-797a179c7a7c"), "approved source")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM Agents WHERE AG_ID=4 AND Type_ID=4")) = 1, "employee fixture")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('AuditEmployeeFail027')") Is DBNull.Value, "unused trigger")
            data = sqls.Select(Function(s) Finger(cn, s)).ToArray()
            defs = BusinessObjects.Select(Function(s) CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & s & "'))"))).ToArray()
            domain = CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM Network_Audit_Source WHERE ConfigId=1"))
        End Using
        For Each valueType In {0, 1}
            For Each amount In {0D, 1.2345D, -7.2355D}
                Dim kind = valueType, value = amount
                Fixture(Sub(cn, tx)
                            Dim r = Request(cn, tx, amount:=value, valueType:=kind), id = ResultId(AuditEmployeeValues.ExecuteInTransaction(cn, tx, r))
                            Check(id > 0, "actual identity " & kind & "/" & value)
                            Dim row = AuditEmployeeValues.Capture(cn, tx, id).Rows(0)
                            Check(CDec(row("Value")) = Decimal.Round(value, 3, MidpointRounding.AwayFromZero), "SQL rounding")
                            Check(CInt(row("ValType")) = kind AndAlso CInt(row("AG_ID")) = 4, "employee and kind")
                            Check(ResultId(AuditEmployeeValues.ExecuteInTransaction(cn, tx, r)) = id, "create replay")
                            Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM Network_Audit_Change c JOIN Network_Audit_Event e ON c.EventId=e.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "'")) = 10, "all ten fields")
                            Dim update = Request(cn, tx, id, amount:=9.8765D, valueType:=1 - kind), stale = Request(cn, tx, id, amount:=2D)
                            AuditEmployeeValues.ExecuteInTransaction(cn, tx, update)
                            Check(CDec(Q(cn, tx, "SELECT Value FROM Others_Values WHERE T_ID=" & id)) = 9.877D, "update value")
                            Check(AuditEmployeeValues.ExecuteInTransaction(cn, tx, update).Status = "Completed", "update replay")
                            Reject(Sub() AuditEmployeeValues.ExecuteInTransaction(cn, tx, stale), "stale update", "تغيرت")
                            Dim cancel = Request(cn, tx, id, True)
                            AuditEmployeeValues.ExecuteInTransaction(cn, tx, cancel)
                            Check(CInt(Q(cn, tx, "SELECT isVoid FROM Others_Values WHERE T_ID=" & id)) = 1, "void state")
                            Check(AuditEmployeeValues.ExecuteInTransaction(cn, tx, cancel).Status = "Completed", "void replay")
                            Reject(Sub() Request(cn, tx, id, True), "already void")
                        End Sub)
            Next
        Next
        For Each mode In {"create", "update", "void"}
            Dim current = mode
            Fixture(Sub(cn, tx)
                        Dim id = If(current = "create", 0, ResultId(AuditEmployeeValues.ExecuteInTransaction(cn, tx, Request(cn, tx))))
                        Dim r = Request(cn, tx, id, current = "void")
                        Q(cn, tx, "CREATE TRIGGER dbo.AuditEmployeeFail027 ON Network_Audit_Event AFTER INSERT AS BEGIN THROW 51127,'forced employee audit failure',1; END")
                        Reject(Sub() AuditEmployeeValues.ExecuteInTransaction(cn, tx, r), "audit rollback " & current, "forced employee audit failure")
                    End Sub)
        Next
        For Each mode In {"permission", "disabled", "domain", "wrong employee", "period", "screen", "long note"}
            Dim current = mode
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx)
                        Select Case current
                            Case "permission" : Q(cn, tx, "UPDATE Users_Validations SET Balance=0 WHERE T_ID=(SELECT Valid_ID FROM Users WHERE User_ID=1)")
                            Case "disabled" : Q(cn, tx, "UPDATE Users SET is_Allow=0 WHERE User_ID=1")
                            Case "domain" : Q(cn, tx, "UPDATE Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code=""People.EmployeeValues""]') WHERE ConfigId=1")
                            Case "wrong employee" : r.Inputs.Single(Function(x) x.Name = "EVEmployeeId").Value = AuditValue.Capture("Integer", 1)
                            Case "period" : r.Inputs.Single(Function(x) x.Name = "EVMonth").Value = AuditValue.Capture("Integer", 9)
                            Case "screen" : r.Context.ScreenCode = "Sales"
                            Case "long note"
                                Reject(Sub() r.Inputs.Single(Function(x) x.Name = "EVNotes").Value = AuditValue.Capture("String", New String("x"c, 4097)), "long note no truncation")
                                Return
                        End Select
                        Reject(Sub() AuditEmployeeValues.ExecuteInTransaction(cn, tx, r), "deny " & current)
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx)
                    AuditEmployeeValues.ExecuteInTransaction(cn, tx, r)
                    r.Inputs.Single(Function(x) x.Name = "EVAmount").Value = AuditValue.Capture("Decimal", 55D)
                    Reject(Sub() AuditEmployeeValues.ExecuteInTransaction(cn, tx, r), "request collision")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx)
                    Q(cn, tx, "CREATE TRIGGER dbo.AuditEmployeeFail027 ON Others_Values AFTER INSERT AS BEGIN THROW 51127,'forced employee business failure',1; END")
                    Reject(Sub() AuditEmployeeValues.ExecuteInTransaction(cn, tx, r), "business rollback", "forced employee business failure")
                End Sub)
        Using cn As New SqlConnection(connection)
            cn.Open()
            For i = 0 To sqls.Length - 1
                Check(Finger(cn, sqls(i)) = data(i), "baseline unchanged " & i)
            Next
            For i = 0 To BusinessObjects.Length - 1
                Check(CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & BusinessObjects(i) & "'))")) = defs(i), "definition unchanged " & BusinessObjects(i))
            Next
            Check(CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM Network_Audit_Source WHERE ConfigId=1")) = domain, "domains restored")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('AuditEmployeeFail027')") Is DBNull.Value, "failure trigger removed")
        End Using
    End Sub
End Module
