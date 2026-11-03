Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq

Friend Module AuditItemMasterTests
    Private checks As Integer
    Private connection As String
    Private candidate As String
    Private context As AuditEventContext
    Private Const ItemId As Integer = 2147481700
    Private Const LinkId As Integer = 2147481701
    Private Const ChildId As Integer = 2147481710
    Private ReadOnly Tables As String() = {"IM_Menu", "IM_Units", "IM_QTY_ALERT", "IM_Formating_Menu", "ST_QTY_Deferences", "IM_Struct_Menu", "Units", "SERVERS", "Users", "Users_Validations", "Network_Edit_Tracker", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change"}

    Public Function Main(args As String()) As Integer
        Try
            Check(AuditContract.ItemMasterFields("CatalogItemRecord").Length = 29, "27 item values plus 2 derived photo metadata")
            Check(AuditContract.ItemMasterFields("ItemFormat").Length = 6, "all format values")
            Check(AuditContract.ItemMasterFields("ItemDifference").Length = 10, "all difference values")
            Check(AuditContract.Domain("Item.Deleted") = "Catalog.Items", "closed item domain")
            Reject(Sub() AuditContract.ItemMasterInputs("Item.Arbitrary"), "unknown operation denied", "")
            If args.Length >= 2 Then
                If args.Length = 4 Then candidate = File.ReadAllText(args(3))
                Live(args(1))
            End If
            Console.WriteLine("PASS " & checks & " item-master checks.") : Return 0
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
            If expected.Length > 0 AndAlso Not ex.ToString().Contains(expected) Then Throw New Exception("Unexpected rejection: " & label, ex)
            Check(True, label) : Return
        End Try
        Throw New Exception("Expected rejection: " & label)
    End Sub
    Private Function Q(cn As SqlConnection, tx As SqlTransaction, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn, tx) With {.CommandTimeout = 30}
            Return cmd.ExecuteScalar()
        End Using
    End Function
    Private Function Command(Optional delete As Boolean = False) As SqlCommand
        Dim cmd As New SqlCommand(If(delete, "item_menu_delete", "item_menu_update_On_Mange_Form"))
        cmd.Parameters.AddWithValue("@IM_ID", ItemId)
        If Not delete Then
            cmd.Parameters.AddWithValue("@item_name", "تعديل اسم اختبار")
            cmd.Parameters.AddWithValue("@item_nameSales", "اسم بيع اختبار")
            cmd.Parameters.AddWithValue("@GM_ID", 1)
            cmd.Parameters.AddWithValue("@isActive", True)
            cmd.Parameters.AddWithValue("@IM_NUM", "Test023")
            cmd.Parameters.AddWithValue("@Def_U_ID", 1)
            cmd.Parameters.AddWithValue("@User_ID", 999999)
            cmd.Parameters.AddWithValue("@Price_ONE", 12.3456D)
        End If
        Return cmd
    End Function
    Private Function Request(cn As SqlConnection, tx As SqlTransaction, cmd As SqlCommand, Optional screen As String = "IM_Fast_Mang") As AuditOperationRequest
        context.ScreenCode = screen
        Return AuditItemMaster.Request(context, cmd, AuditItemMaster.Capture(cn, tx, ItemId))
    End Function
    Private Sub Fixture(work As Action(Of SqlConnection, SqlTransaction))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    If candidate IsNot Nothing Then Q(cn, tx, candidate)
                    Q(cn, tx, "INSERT dbo.IM_Menu(IM_ID,item_name,item_nameSales,GM_ID,isValid,Row_Enabled,Cost,Photo,Notes) VALUES(" & ItemId & ",N'صنف اختبار',N'اسم بيع',1,0,0,6.1234567890123,0x0102030405,N'ملاحظة')")
                    Q(cn, tx, "INSERT dbo.IM_Units(U_IM_ID,IM_ID,U_ID,Barcode,Price,Min_SP,Min_SP_2,is_Default,Row_Enabled,User_ID,OFFER_PRICE) VALUES(" & LinkId & "," & ItemId & ",1,N'ItemMaster023',7.125,NULL,6,1,0,1,NULL)")
                    work(cn, tx)
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
    End Sub
    Private Function AllItems(cn As SqlConnection) As String
        Dim ids = AuditSalesQuantity.Rows(cn, Nothing, "SELECT IM_ID FROM dbo.IM_Menu ORDER BY IM_ID")
        Return String.Join("|", ids.Rows.Cast(Of DataRow)().Select(Function(x) AuditItemMaster.Fingerprint(AuditItemMaster.Capture(cn, Nothing, CInt(x(0))))))
    End Function
    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim builder As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS Item023 rollback", .ConnectTimeout = 5, .Pooling = False}
        If builder.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database.")
        connection = builder.ConnectionString
        context = New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "023", .ScreenCode = "IM_Fast_Mang"}
        Dim counts As Long(), definitions As String(), domains As String, itemHash As String, stored As String()
        Dim objects = {"item_menu_delete", "item_menu_update_On_Mange_Form", "AA_GET_MAX_IM_UNITS", "AG_BalanceMV_GetSerial_Barcode", "Network_Audit_Request_Begin", "Network_Audit_Event_Write"}
        Dim verifySql = {"SELECT * FROM dbo.IM_Units ORDER BY U_IM_ID", "SELECT * FROM dbo.IM_QTY_ALERT ORDER BY T_ID", "SELECT * FROM dbo.IM_Formating_Menu ORDER BY T_ID", "SELECT * FROM dbo.ST_QTY_Deferences ORDER BY T_ID", "SELECT * FROM dbo.IM_Struct_Menu ORDER BY T_ID", "SELECT * FROM dbo.SB_Contents ORDER BY T_ID", "SELECT * FROM dbo.Agents_Balance_MV ORDER BY T_ID", "SELECT * FROM dbo.Units ORDER BY U_ID"}
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other clients")
            context.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid)
            Check(context.SourceId = Guid.Parse("3c520084-0214-43d4-a5fb-797a179c7a7c"), "approved source")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(" & String.Join(",", Tables.Select(Function(x) "OBJECT_ID('dbo." & x & "')")) & ")")) = 0, "no unreviewed triggers")
            For Each pair In {"IM_Menu|IM_ID", "IM_Units|U_IM_ID", "IM_QTY_ALERT|T_ID", "IM_Formating_Menu|T_ID", "ST_QTY_Deferences|T_ID", "IM_Struct_Menu|T_ID"}
                Dim p = pair.Split("|"c)
                Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo." & p(0) & " WHERE " & p(1) & " BETWEEN " & ItemId & " AND " & (ItemId + 100))) = 0, "unused fixtures " & p(0))
            Next
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.Units WHERE U_ID IN(1,2)")) = 2, "base units present")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditItemFail023')") Is DBNull.Value, "failure trigger name unused")
            counts = Tables.Select(Function(x) CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.[" & x & "]"))).ToArray()
            definitions = objects.Select(Function(x) CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & x & "'))"))).ToArray()
            domains = CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1"))
            itemHash = AllItems(cn)
            stored = verifySql.Select(Function(x) AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, Nothing, x))).ToArray()
        End Using
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, Command()), stale = Request(cn, tx, Command())
                    AuditItemMaster.ExecuteInTransaction(cn, tx, r)
                    Dim after = AuditItemMaster.Capture(cn, tx, ItemId)
                    Check(CStr(after("CatalogItemRecord").Rows(0)("item_name")) = "تعديل اسم اختبار", "name persisted")
                    Check(CBool(after("CatalogItemRecord").Rows(0)("Row_Enabled")), "placeholder becomes enabled")
                    Check(IsDBNull(after("CatalogItemRecord").Rows(0)("PhotoSha256")), "original photo clearing policy preserved")
                    Check(CDec(after("ItemUnit").Rows(0)("Price")) = 12.346D, "server rounds price without client truncation")
                    Check(CBool(after("ItemUnit").Rows(0)("Row_Enabled")), "units enabled")
                    Check(CDbl(after("CatalogItemRecord").Rows(0)("Cost")) = CDbl(Q(cn, tx, "SELECT CAST(6.1234567890123 AS float)")), "cost not changed or rounded")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND FieldCode='ItemPhotoSha256' AND ValueOrigin=3 AND BeforeState=3 AND LEN(BeforeValue)=64 AND AfterState=2")) = 1, "image metadata clearly derived with NULL after")
                    Check(AuditItemMaster.ExecuteInTransaction(cn, tx, r).Status = "Completed", "fast update replay")
                    Reject(Sub() AuditItemMaster.ExecuteInTransaction(cn, tx, stale), "stale update denied", "تغير")
                    r.Inputs.Single(Function(x) x.Name = "ItemName").Value = AuditValue.Capture("String", "different")
                    Reject(Sub() AuditItemMaster.ExecuteInTransaction(cn, tx, r), "same identity different payload denied", "")
                End Sub)
        For Each screen In {"ItemsMenu", "IM_Fast_Mang"}
            Dim currentScreen = screen
            Fixture(Sub(cn, tx)
                        Q(cn, tx, "INSERT dbo.IM_Formating_Menu(T_ID,IM_ID,FRM_IM_ID,U_ID,Qty,Row_Enabled,User_ID) VALUES(" & ChildId & "," & ItemId & ",1,1,1.23456789123,1,1)")
                        Q(cn, tx, "INSERT dbo.ST_QTY_Deferences(T_ID,Bill_T_ID,[DATE],IM_ID,ST_ID,Valid_D,ST_QTY,Cost,U_Cargo,Barcode,TOTAL) VALUES(" & ChildId & ",1,GETDATE()," & ItemId & ",1,N'',1.1234567890123,2.125,1,N'Fixture',2.125)")
                        Q(cn, tx, "INSERT dbo.IM_Struct_Menu(T_ID,IM_ID,U_IM_ID,Struct_IM_ID,U_ID,Qty,Row_Enabled,User_ID) VALUES(" & ChildId & "," & ItemId & "," & LinkId & ",1,1,1,1,1)")
                        Dim r = Request(cn, tx, Command(True), currentScreen)
                        AuditItemMaster.ExecuteInTransaction(cn, tx, r)
                        Dim after = AuditItemMaster.Capture(cn, tx, ItemId)
                        Check(after.Values.All(Function(x) x.Rows.Count = 0), "all original deletion targets removed " & currentScreen)
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND AfterState=1")) = 55, "all deleted fields captured " & currentScreen)
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.IM_Struct_Menu WHERE T_ID=" & ChildId)) = 1, "unmodified legacy recipe retention " & currentScreen)
                        Check(AuditItemMaster.ExecuteInTransaction(cn, tx, r).Status = "Completed", "delete replay " & currentScreen)
                        Reject(Sub() AuditItemMaster.Request(context, Command(True), AuditItemMaster.Capture(cn, tx, ItemId)), "deleted item cannot form a new request", "")
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "DELETE dbo.IM_Units WHERE IM_ID=" & ItemId)
                    AuditItemMaster.ExecuteInTransaction(cn, tx, Request(cn, tx, Command()))
                    Dim row = AuditItemMaster.Capture(cn, tx, ItemId)("ItemUnit").Rows(0)
                    Check(CInt(row("U_ID")) = 1 AndAlso CInt(row("User_ID")) = 1, "new default unit uses actual actor")
                    Check(CDec(row("Price")) = 12.346D AndAlso Not IsDBNull(row("Barcode")), "new default price and generated barcode")
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "UPDATE dbo.Units SET U_Cargo=12 WHERE U_ID=2; UPDATE dbo.IM_Units SET U_ID=2,is_Default=0,Price=120 WHERE U_IM_ID=" & LinkId)
                    AuditItemMaster.ExecuteInTransaction(cn, tx, Request(cn, tx, Command()))
                    Dim units = AuditItemMaster.Capture(cn, tx, ItemId)("ItemUnit")
                    Check(units.Rows.Count = 2, "package creates base unit")
                    Check(units.Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("U_ID")) = 1).Field(Of Decimal)("Price") = 10D, "base price follows original package calculation")
                    Check(units.Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("U_ID")) = 2).Field(Of Decimal)("Price") = 12.346D, "requested price applies only default package")
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "INSERT dbo.IM_QTY_ALERT(T_ID,IM_ID,ST_ID,MIN_QTY,MAX_QTY,Row_Enabled,User_ID) VALUES(" & ChildId & "," & ItemId & ",1,2,9,0,1)")
                    AuditItemMaster.ExecuteInTransaction(cn, tx, Request(cn, tx, Command()))
                    Check(CBool(Q(cn, tx, "SELECT Row_Enabled FROM dbo.IM_QTY_ALERT WHERE T_ID=" & ChildId)), "alerts enabled by fast update")
                    Reject(Sub() AuditItemMaster.ExecuteInTransaction(cn, tx, Request(cn, tx, Command(True))), "existing alerts still prevent deletion", "لايمكن")
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "INSERT dbo.ST_Balance(T_ID,ST_ID,IM_ID,D_Valid,ST_QTY) VALUES(" & ChildId & ",1," & ItemId & ",N'',0)")
                    Reject(Sub() AuditItemMaster.ExecuteInTransaction(cn, tx, Request(cn, tx, Command(True))), "stock rows still prevent deletion even zero", "لايمكن")
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "DECLARE @i int=1; WHILE @i<=25 BEGIN INSERT dbo.IM_Units(U_IM_ID,IM_ID,U_ID,Price,is_Default,Row_Enabled,User_ID) VALUES(" & LinkId & "+@i," & ItemId & ",1,5,0,0,1); SET @i+=1; END")
                    Dim r = Request(cn, tx, Command())
                    AuditItemMaster.ExecuteInTransaction(cn, tx, r)
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) = 4, "26 units split into complete pages")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "'")) = 289, "no field loss during paging")
                End Sub)
        For Each kind In {"image", "unit", "alert", "format", "difference"}
            Dim changed = kind
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, Command())
                        Select Case changed
                            Case "image" : Q(cn, tx, "UPDATE dbo.IM_Menu SET Photo=0x0504030201 WHERE IM_ID=" & ItemId)
                            Case "unit" : Q(cn, tx, "UPDATE dbo.IM_Units SET Price=10 WHERE U_IM_ID=" & LinkId)
                            Case "alert" : Q(cn, tx, "INSERT dbo.IM_QTY_ALERT(T_ID,IM_ID,ST_ID,MIN_QTY,MAX_QTY,Row_Enabled,User_ID) VALUES(" & ChildId & "," & ItemId & ",1,2,9,0,1)")
                            Case "format" : Q(cn, tx, "INSERT dbo.IM_Formating_Menu(T_ID,IM_ID,FRM_IM_ID,U_ID,Qty,Row_Enabled,User_ID) VALUES(" & ChildId & "," & ItemId & ",1,1,1,1,1)")
                            Case "difference" : Q(cn, tx, "INSERT dbo.ST_QTY_Deferences(T_ID,Bill_T_ID,[DATE],IM_ID,ST_ID,Valid_D,ST_QTY,Cost,U_Cargo,Barcode,TOTAL) VALUES(" & ChildId & ",1,GETDATE()," & ItemId & ",1,N'',1,2,1,N'Fixture',2)")
                        End Select
                        Reject(Sub() AuditItemMaster.ExecuteInTransaction(cn, tx, r), "stale " & changed & " rejected", "تغير")
                    End Sub)
        Next
        For Each mode In {"user", "permission", "gate", "screen"}
            Dim testMode = mode
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, Command())
                        Select Case testMode
                            Case "user" : Q(cn, tx, "UPDATE dbo.Users SET is_Allow=0 WHERE User_ID=1")
                            Case "permission" : Q(cn, tx, "UPDATE dbo.Users_Validations SET IM=0 WHERE T_ID=1")
                            Case "gate" : Q(cn, tx, "UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code=""Catalog.Items""]') WHERE ConfigId=1")
                            Case "screen" : r.Context.ScreenCode = "ItemsMenu"
                        End Select
                        Reject(Sub() AuditItemMaster.ExecuteInTransaction(cn, tx, r), "denied " & testMode, "")
                    End Sub)
        Next
        For Each delete In {False, True}
            Dim isDelete = delete
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, Command(isDelete))
                        Q(cn, tx, "CREATE TRIGGER dbo.AuditItemFail023 ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN IF EXISTS(SELECT 1 FROM inserted WHERE EventCode IN('Item.FastUpdated','Item.Deleted') AND EventOrdinal=2) THROW 51123,'forced item detail failure',1; END")
                        Reject(Sub() AuditItemMaster.ExecuteInTransaction(cn, tx, r), "detail failure rolls back " & r.OperationCode, "forced item detail failure")
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "INSERT dbo.ST_QTY_Deferences(T_ID,Bill_T_ID,[DATE],IM_ID,ST_ID,Valid_D,ST_QTY,Cost,U_Cargo,Barcode,TOTAL) VALUES(" & ChildId & ",1,GETDATE()," & ItemId & ",1,N'',1,2,1,N'Fixture',2)")
                    Dim r = Request(cn, tx, Command(True))
                    Q(cn, tx, "CREATE TRIGGER dbo.AuditItemFail023 ON dbo.IM_Units AFTER DELETE AS BEGIN THROW 51123,'forced business deletion failure',1; END")
                    Reject(Sub() AuditItemMaster.ExecuteInTransaction(cn, tx, r), "partial business deletion rolls back", "forced business deletion failure")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, Command())
                    Q(cn, tx, "CREATE TRIGGER dbo.AuditItemFail023 ON dbo.IM_Menu AFTER UPDATE AS BEGIN THROW 51123,'forced business update failure',1; END")
                    Reject(Sub() AuditItemMaster.ExecuteInTransaction(cn, tx, r), "legacy rollback cannot report success", "")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim cmd = Command()
                    cmd.Parameters("@Price_ONE").Value = DBNull.Value
                    cmd.Parameters("@IM_NUM").Value = DBNull.Value
                    cmd.Parameters("@isActive").Value = DBNull.Value
                    AuditItemMaster.ExecuteInTransaction(cn, tx, Request(cn, tx, cmd))
                    Dim after = AuditItemMaster.Capture(cn, tx, ItemId)
                    Check(IsDBNull(after("CatalogItemRecord").Rows(0)("IM_Num")) AndAlso IsDBNull(after("CatalogItemRecord").Rows(0)("isActive")), "nullable item inputs preserved")
                    Check(CDec(after("ItemUnit").Rows(0)("Price")) = 7.125D, "NULL requested price leaves existing price")
                End Sub)
        Fixture(Sub(cn, tx)
                    Using cmd As New SqlCommand("UPDATE dbo.IM_Menu SET Photo=@photo WHERE IM_ID=" & ItemId, cn, tx)
                        cmd.Parameters.Add("@photo", SqlDbType.Image).Value = New Byte(50000) {}
                        cmd.ExecuteNonQuery()
                    End Using
                    Dim snapshot = AuditItemMaster.Capture(cn, tx, ItemId)
                    Check(CLng(snapshot("CatalogItemRecord").Rows(0)("PhotoBytes")) = 50001, "large image length without payload copy")
                    Check(CStr(snapshot("CatalogItemRecord").Rows(0)("PhotoSha256")).Length = 64 AndAlso Not snapshot("CatalogItemRecord").Columns.Contains("Photo"), "large image digest only")
                    AuditItemMaster.ExecuteInTransaction(cn, tx, AuditItemMaster.Request(context, Command(True), snapshot))
                End Sub)
        Using cn As New SqlConnection(connection)
            cn.Open()
            For i = 0 To Tables.Length - 1
                Check(CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.[" & Tables(i) & "]")) = counts(i), "rollback count " & Tables(i))
            Next
            For i = 0 To objects.Length - 1
                Check(CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & objects(i) & "'))")) = definitions(i), "definition unchanged " & objects(i))
            Next
            Check(AllItems(cn) = itemHash, "all item graphs and images unchanged")
            For i = 0 To verifySql.Length - 1
                Check(AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, Nothing, verifySql(i))) = stored(i), "original data unchanged " & i)
            Next
            Check(CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = domains, "domain rollback")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.Network_Audit_Request WHERE State<>1")) = 0, "no incomplete requests")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditItemFail023')") Is DBNull.Value, "failure trigger removed")
        End Using
    End Sub
End Module
