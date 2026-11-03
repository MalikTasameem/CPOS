Option Strict On
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq

Friend Module AuditUnitTests
    Private checks As Integer
    Private connection As String
    Private candidate As String
    Private context As AuditEventContext
    Private Const TestId As Integer = 2147482400
    Private ReadOnly Tables As String() = {"Units", "IM_Units", "SERVERS", "Users", "Users_Validations", "Network_Edit_Tracker", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change"}

    Public Function Main(args As String()) As Integer
        Try
            Check(AuditUnits.Cargo("") = 1 AndAlso AuditUnits.Cargo("0") = 1, "empty/zero cargo retains normalization")
            Check(AuditUnits.Cargo("-2") = -2, "no unapproved positive-only rule")
            Reject(Sub() AuditUnits.Cargo(Double.NaN.ToString(CultureInfo.CurrentCulture)), "nonfinite cargo rejected", "غير صالحة")
            Check(AuditContract.UnitFields().Length = 3, "complete unit master")
            Check(AuditContract.Domain("Unit.Created") = "Catalog.Units", "closed domain")
            If args.Length >= 2 Then
                If args.Length = 4 Then candidate = File.ReadAllText(args(3))
                Live(args(1))
            End If
            Console.WriteLine("PASS " & checks & " unit checks.") : Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString()) : Return 1
        End Try
    End Function

    Private Sub Check(value As Boolean, label As String)
        If Not value Then Throw New Exception("FAILED " & label)
        checks += 1 : Console.WriteLine("PASS " & label)
    End Sub

    Private Sub Reject(work As Action, label As String, expected As String)
        Try
            work()
        Catch ex As Exception
            If Not ex.Message.Contains(expected) Then Throw New Exception("Unexpected rejection: " & label, ex)
            Check(True, label) : Return
        End Try
        Throw New Exception("Expected rejection: " & label)
    End Sub

    Private Function Q(cn As SqlConnection, tx As SqlTransaction, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn, tx) With {.CommandTimeout = 30}
            Return cmd.ExecuteScalar()
        End Using
    End Function

    Private Function Id(result As AuditRequestResult) As Integer
        Return Integer.Parse(AuditCanonical.ParseXml(result.ResultXml).Element("Values").Elements("Value").Single(Function(x) CStr(x.Attribute("name")) = "EntityId").Value, CultureInfo.InvariantCulture)
    End Function

    Private Sub Fixture(work As Action(Of SqlConnection, SqlTransaction))
        context.ScreenCode = "Units"
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    If candidate IsNot Nothing Then Q(cn, tx, candidate)
                    Q(cn, tx, "INSERT dbo.Units(U_ID,U_Name,U_Cargo,isDefault) VALUES(" & TestId & ",N'Audit fixture',2.125,0)")
                    work(cn, tx)
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
    End Sub

    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim builder As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS Units019 rollback", .ConnectTimeout = 5, .Pooling = False}
        If builder.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database.")
        connection = builder.ConnectionString
        context = New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "019", .ScreenCode = "Units"}
        Dim counts As Long(), definitions As String(), domains As String
        Dim procedures = {"Unit_Insert", "Unit_Update", "Unit_Delete", "AA_GET_MAX_UNITS", "Network_Audit_Request_Begin", "Network_Audit_Event_Write"}
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other clients")
            context.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid)
            Check(context.SourceId = Guid.Parse("3c520084-0214-43d4-a5fb-797a179c7a7c"), "approved source")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(" & String.Join(",", Tables.Select(Function(x) "OBJECT_ID('dbo." & x & "')")) & ")")) = 0, "no unreviewed triggers")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.Units WHERE U_ID=" & TestId)) = 0, "unused unit fixture")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.IM_Units WHERE U_IM_ID=" & TestId)) = 0, "unused item-unit fixture")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditUnitFail019')") Is DBNull.Value, "failure trigger name unused")
            counts = Tables.Select(Function(x) CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.[" & x & "]"))).ToArray()
            definitions = procedures.Select(Function(x) CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & x & "'))"))).ToArray()
            domains = CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1"))
        End Using
        For Each screen In {"Units", "ItemsMenu", "IM_ADD_New", "Add_Unit"}
            Fixture(Sub(cn, tx)
                        context.ScreenCode = screen
                        Dim r = AuditUnits.Request(context, "Unit.Created", Nothing, "وحدة اختبار " & screen, 1.23456789012345)
                        Dim first = AuditUnits.ExecuteInTransaction(cn, tx, r), saved = Id(first), row = AuditUnits.Capture(cn, tx, saved)
                        Check(saved > 2 AndAlso CStr(row("U_Name")) = "وحدة اختبار " & screen, "create actual id/name " & screen)
                        Check(CDbl(row("U_Cargo")) = 1.23456789012345 AndAlso Not CBool(row("isDefault")), "float/default preserved " & screen)
                        Dim replay = AuditUnits.ExecuteInTransaction(cn, tx, r)
                        Check(replay.Status = "Completed" AndAlso Id(replay) = saved, "replay returns original identity " & screen)
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) = 1, "one event " & screen)
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND c.BeforeState=1 AND c.AfterState=3")) = 3, "three actual created fields " & screen)
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Dim r = AuditUnits.Request(context, "Unit.Updated", AuditUnits.Capture(cn, tx, TestId), New String("أ"c, 120), -3.5)
                    AuditUnits.ExecuteInTransaction(cn, tx, r)
                    Dim row = AuditUnits.Capture(cn, tx, TestId)
                    Check(CStr(row("U_Name")).Length = 100 AndAlso CDbl(row("U_Cargo")) = -3.5, "actual procedure truncation and negative cargo unchanged")
                    Dim deletion = AuditUnits.Request(context, "Unit.Deleted", row, "", 1)
                    AuditUnits.ExecuteInTransaction(cn, tx, deletion)
                    Check(AuditUnits.Capture(cn, tx, TestId) Is Nothing, "unused unit deleted")
                    Check(AuditUnits.ExecuteInTransaction(cn, tx, deletion).Status = "Completed", "delete replay survives missing row")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & deletion.RequestId.ToString() & "' AND c.BeforeState=3 AND c.AfterState=1")) = 3, "deletion actual row retained in audit")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = AuditUnits.Request(context, "Unit.Updated", AuditUnits.Capture(cn, tx, TestId), "new", 1)
                    Q(cn, tx, "UPDATE dbo.Units SET U_Cargo=7 WHERE U_ID=" & TestId)
                    Reject(Sub() AuditUnits.ExecuteInTransaction(cn, tx, r), "stale row rejected", "تغيرت الوحدة")
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "INSERT dbo.IM_Units(U_IM_ID,IM_ID,U_ID,Price,Row_Enabled,User_ID) VALUES(" & TestId & ",1," & TestId & ",0,1,1)")
                    Dim r = AuditUnits.Request(context, "Unit.Deleted", AuditUnits.Capture(cn, tx, TestId), "", 1)
                    Reject(Sub() AuditUnits.ExecuteInTransaction(cn, tx, r), "referenced unit cannot be deleted", "لايمكن حذف وحدة")
                End Sub)
        Fixture(Sub(cn, tx)
                    Reject(Sub() AuditUnits.Request(context, "Unit.Deleted", AuditUnits.Capture(cn, tx, 2), "", 1), "protected default id", "محمي")
                    context.ScreenCode = "Add_Unit"
                    Reject(Sub() AuditUnits.Request(context, "Unit.Updated", AuditUnits.Capture(cn, tx, TestId), "bad", 1), "auxiliary update not allowed", "غير معتمدة")
                    context.ScreenCode = "Backup"
                    Reject(Sub() AuditUnits.Request(context, "Unit.Created", Nothing, "bad", 1), "unreviewed screen rejected", "غير معتمدة")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = AuditUnits.Request(context, "Unit.Created", Nothing, "bad", 1)
                    Q(cn, tx, "UPDATE dbo.Users SET is_Allow=0 WHERE User_ID=1")
                    Reject(Sub() AuditUnits.ExecuteInTransaction(cn, tx, r), "disabled actor rejected", "غير مسموح")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = AuditUnits.Request(context, "Unit.Created", Nothing, "bad", 1)
                    Q(cn, tx, "UPDATE dbo.Users_Validations SET IM=0 WHERE T_ID=1")
                    Reject(Sub() AuditUnits.ExecuteInTransaction(cn, tx, r), "item-management permission rechecked", "صلاحية")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = AuditUnits.Request(context, "Unit.Created", Nothing, "range", 1)
                    Q(cn, tx, "UPDATE dbo.SERVERS SET END_ID=START_ID")
                    Reject(Sub() AuditUnits.ExecuteInTransaction(cn, tx, r), "invalid range fails before insert", "نطاق")
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "CREATE TRIGGER dbo.AuditUnitFail019 ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN IF EXISTS(SELECT 1 FROM inserted WHERE EventCode LIKE 'Unit.%') THROW 51119,'forced unit audit failure',1; END")
                    Dim r = AuditUnits.Request(context, "Unit.Updated", AuditUnits.Capture(cn, tx, TestId), "must rollback", 9)
                    Reject(Sub() AuditUnits.ExecuteInTransaction(cn, tx, r), "event failure rejects business mutation", "forced unit audit failure")
                End Sub)
        Using cn As New SqlConnection(connection)
            cn.Open()
            For i = 0 To Tables.Length - 1
                Check(CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.[" & Tables(i) & "]")) = counts(i), "rollback count " & Tables(i))
            Next
            For i = 0 To procedures.Length - 1
                Check(CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & procedures(i) & "'))")) = definitions(i), "procedure unchanged " & procedures(i))
            Next
            Check(CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = domains, "domain rollback")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.Network_Audit_Request WHERE State<>1")) = 0, "no incomplete requests")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditUnitFail019')") Is DBNull.Value, "failure trigger removed by rollback")
        End Using
    End Sub
End Module
