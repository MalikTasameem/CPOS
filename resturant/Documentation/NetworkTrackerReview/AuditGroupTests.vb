Option Strict On
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq

Friend Module AuditGroupTests
    Private checks As Integer
    Private connection As String
    Private candidate As String
    Private context As AuditEventContext
    Private Const TestId As Integer = 2147482300
    Private ReadOnly Tables As String() = {"General_menu", "GM_Printer", "IM_Menu", "SERVERS", "Users", "Users_Validations", "Network_Edit_Tracker", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change"}

    Public Function Main(args As String()) As Integer
        Try
            Check(AuditContract.GroupFields("ProductGroup").Length = 13, "13 master fields")
            Check(AuditContract.GroupFields("GroupPrinter").Length = 2, "2 link fields")
            Check(AuditContract.Domain("Group.PrinterLinked") = "Catalog.Groups", "closed domain")
            Reject(Sub() AuditContract.GroupInputs("Group.Arbitrary"), "unknown operation denied", "غير معتمدة")
            If args.Length >= 2 Then
                If args.Length = 4 Then candidate = File.ReadAllText(args(3))
                Live(args(1))
            End If
            Console.WriteLine("PASS " & checks & " group checks.") : Return 0
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

    Private Function Command(name As String, Optional groupId As Integer = TestId) As SqlCommand
        Dim cmd As New SqlCommand(name)
        cmd.Parameters.Add("@GM_ID", SqlDbType.Int).Value = groupId
        If name = "GM_insert" OrElse name = "GM_update" Then
            cmd.Parameters.Add("@GM_Name", SqlDbType.NVarChar, 200).Value = "تصنيف اختبار"
            cmd.Parameters.Add("@Printer_ID", SqlDbType.Int).Value = 1
            cmd.Parameters.Add("@POS_isShow", SqlDbType.Int).Value = 1
            cmd.Parameters.Add("@Ksh_Screen", SqlDbType.NVarChar, 200).Value = "AuditTests"
        End If
        Return cmd
    End Function

    Private Function Request(cn As SqlConnection, tx As SqlTransaction, cmd As SqlCommand) As AuditOperationRequest
        Return AuditGroups.Request(context, cmd, If(cmd.CommandText = "GM_insert", Nothing, AuditGroups.Capture(cn, tx, Convert.ToInt32(cmd.Parameters("@GM_ID").Value))))
    End Function

    Private Sub Fixture(work As Action(Of SqlConnection, SqlTransaction))
        context.ScreenCode = "GeneralMenu"
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    If candidate IsNot Nothing Then Q(cn, tx, candidate)
                    Q(cn, tx, "INSERT dbo.General_menu(GM_ID,GM_Name,Printer_ID,POS_isShow,Rank_Num,POS_ST_ID,Ksh_Screen,Cat_ID) VALUES(" & TestId & ",N'Audit fixture',1,1,2,1,NULL,NULL)")
                    work(cn, tx)
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
    End Sub

    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim builder As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS Groups020 rollback", .ConnectTimeout = 5, .Pooling = False}
        If builder.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database.")
        connection = builder.ConnectionString
        context = New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "020", .ScreenCode = "GeneralMenu"}
        Dim counts As Long(), definitions As String(), domains As String, itemGroup As Object
        Dim procedures = {"GM_insert", "GM_update", "GM_delete", "AA_GET_MAX_General_menu", "Network_Audit_Request_Begin", "Network_Audit_Event_Write"}
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other clients")
            context.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid)
            Check(context.SourceId = Guid.Parse("3c520084-0214-43d4-a5fb-797a179c7a7c"), "approved source")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(" & String.Join(",", Tables.Select(Function(x) "OBJECT_ID('dbo." & x & "')")) & ")")) = 0, "no unreviewed triggers")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.General_menu WHERE GM_ID=" & TestId)) = 0, "unused group fixture")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.GM_Printer WHERE GM_ID=" & TestId)) = 0, "no existing fixture links")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditGroupFail020')") Is DBNull.Value, "failure trigger name unused")
            itemGroup = Q(cn, Nothing, "SELECT GM_ID FROM dbo.IM_Menu WHERE IM_ID=1")
            Check(itemGroup IsNot Nothing, "item available for rollback-only reference test")
            counts = Tables.Select(Function(x) CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.[" & x & "]"))).ToArray()
            definitions = procedures.Select(Function(x) CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & x & "'))"))).ToArray()
            domains = CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1"))
        End Using
        For Each screen In {"GeneralMenu", "ItemsMenu", "IM_ADD_New"}
            Fixture(Sub(cn, tx)
                        context.ScreenCode = screen
                        Dim cmd = Command("GM_insert"), r = Request(cn, tx, cmd), first = AuditGroups.ExecuteInTransaction(cn, tx, r), saved = Id(first)
                        Dim row = AuditGroups.Capture(cn, tx, saved).Header.Rows(0)
                        Check(saved > 1 AndAlso CStr(row("GM_Name")) = "تصنيف اختبار", "actual create id/name " & screen)
                        Check(IsDBNull(row("BK_R")) AndAlso IsDBNull(row("Cat_ID")) AndAlso CInt(row("POS_ST_ID")) = 1, "null/default values " & screen)
                        Dim replay = AuditGroups.ExecuteInTransaction(cn, tx, r)
                        Check(replay.Status = "Completed" AndAlso Id(replay) = saved, "replay original id " & screen)
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND c.BeforeState=1")) = 13, "complete created master " & screen)
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Dim cmd = Command("GM_update")
                    cmd.Parameters.Add("@Rank_Num", SqlDbType.Int).Value = 17
                    cmd.Parameters.Add("@BK_R", SqlDbType.Int).Value = 123
                    Dim r = Request(cn, tx, cmd)
                    AuditGroups.ExecuteInTransaction(cn, tx, r)
                    Dim row = AuditGroups.Capture(cn, tx, TestId).Header.Rows(0)
                    Check(CDbl(row("Rank_Num")) = 17 AndAlso CInt(row("BK_R")) = 123 AndAlso IsDBNull(row("FK_R")), "actual explicit rank/color/null")
                    cmd.Parameters.RemoveAt("@Rank_Num") : cmd.Parameters.RemoveAt("@BK_R")
                    Dim nextRank = Convert.ToInt32(Q(cn, tx, "SELECT MAX(Rank_Num) FROM dbo.General_menu")) + 1
                    AuditGroups.ExecuteInTransaction(cn, tx, Request(cn, tx, cmd))
                    row = AuditGroups.Capture(cn, tx, TestId).Header.Rows(0)
                    Check(CDbl(row("Rank_Num")) = nextRank AndAlso IsDBNull(row("BK_R")), "original implicit rank and color reset")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim link = Command("GroupPrinter_Insert") : link.Parameters.Add("@PTR_ID", SqlDbType.Int).Value = 1
                    Dim r = Request(cn, tx, link)
                    AuditGroups.ExecuteInTransaction(cn, tx, r)
                    Dim after = AuditGroups.Capture(cn, tx, TestId)
                    Check(after.Printers.Rows.Count = 1, "printer link saved")
                    Check(AuditGroups.ExecuteInTransaction(cn, tx, r).Status = "Completed", "printer retry does not duplicate")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) = 2, "master and link detail events")
                    Reject(Sub() AuditGroups.ExecuteInTransaction(cn, tx, Request(cn, tx, link)), "new duplicate link rejected", "مسبقًا")
                    Dim unlink = Command("GroupPrinter_Delete") : unlink.Parameters.Add("@T_ID", SqlDbType.Int).Value = CInt(after.Printers.Rows(0)("T_ID"))
                    Dim removal = Request(cn, tx, unlink)
                    AuditGroups.ExecuteInTransaction(cn, tx, removal)
                    Check(AuditGroups.Capture(cn, tx, TestId).Printers.Rows.Count = 0, "link removed from selected group")
                    Check(AuditGroups.ExecuteInTransaction(cn, tx, removal).Status = "Completed", "unlink replay")
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "DECLARE @i int=0; WHILE @i<53 BEGIN INSERT dbo.GM_Printer(GM_ID,PTR_ID) VALUES(" & TestId & ",1); SET @i+=1; END")
                    Dim r = Request(cn, tx, Command("GM_delete"))
                    AuditGroups.ExecuteInTransaction(cn, tx, r)
                    Dim after = AuditGroups.Capture(cn, tx, TestId)
                    Check(after.Header.Rows.Count = 0 AndAlso after.Printers.Rows.Count = 0, "group and all linked printers deleted atomically")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) = 3, "53 links paged without dropping")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND c.AfterState=1")) = 119, "13 header plus 106 link values")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, Command("GM_update"))
                    Q(cn, tx, "INSERT dbo.GM_Printer(GM_ID,PTR_ID) VALUES(" & TestId & ",1)")
                    Reject(Sub() AuditGroups.ExecuteInTransaction(cn, tx, r), "concurrent link change rejects stale graph", "تغير التصنيف")
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "UPDATE dbo.IM_Menu SET GM_ID=" & TestId & " WHERE IM_ID=1")
                    Reject(Sub() AuditGroups.ExecuteInTransaction(cn, tx, Request(cn, tx, Command("GM_delete"))), "used group deletion denied", "لايمكن حذف مجموعة")
                End Sub)
        Fixture(Sub(cn, tx)
                    Reject(Sub() Request(cn, tx, Command("GM_delete", 1)), "protected group", "محمي")
                    context.ScreenCode = "ItemsMenu"
                    Reject(Sub() Request(cn, tx, Command("GM_update")), "auxiliary update denied", "غير معتمدة")
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "UPDATE dbo.Users SET is_Allow=0 WHERE User_ID=1")
                    Reject(Sub() AuditGroups.ExecuteInTransaction(cn, tx, Request(cn, tx, Command("GM_update"))), "disabled user", "غير مسموح")
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "UPDATE dbo.Users_Validations SET IM=0 WHERE T_ID=1")
                    Reject(Sub() AuditGroups.ExecuteInTransaction(cn, tx, Request(cn, tx, Command("GM_update"))), "permission revoked", "صلاحية")
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "INSERT dbo.GM_Printer(GM_ID,PTR_ID) VALUES(" & TestId & ",1)")
                    Q(cn, tx, "CREATE TRIGGER dbo.AuditGroupFail020 ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN IF EXISTS(SELECT 1 FROM inserted WHERE EventCode LIKE 'Group.%' AND EventOrdinal=2) THROW 51120,'forced group detail failure',1; END")
                    Reject(Sub() AuditGroups.ExecuteInTransaction(cn, tx, Request(cn, tx, Command("GM_delete"))), "detail event failure rolls back deletion", "forced group detail failure")
                End Sub)
        Using cn As New SqlConnection(connection)
            cn.Open()
            For i = 0 To Tables.Length - 1
                Check(CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.[" & Tables(i) & "]")) = counts(i), "rollback count " & Tables(i))
            Next
            For i = 0 To procedures.Length - 1
                Check(CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & procedures(i) & "'))")) = definitions(i), "procedure unchanged " & procedures(i))
            Next
            Check(Object.Equals(Q(cn, Nothing, "SELECT GM_ID FROM dbo.IM_Menu WHERE IM_ID=1"), itemGroup), "item reference restored")
            Check(CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = domains, "domain rollback")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.Network_Audit_Request WHERE State<>1")) = 0, "no incomplete requests")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditGroupFail020')") Is DBNull.Value, "failure trigger removed")
        End Using
        Console.WriteLine("Rollback tests leave no business rows; SQL identity allocation may retain normal gaps in GM_Printer.")
    End Sub
End Module
