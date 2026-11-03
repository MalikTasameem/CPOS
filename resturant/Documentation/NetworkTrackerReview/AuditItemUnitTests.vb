Option Strict On
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq

Friend Module AuditItemUnitTests
    Private checks As Integer
    Private connection As String
    Private candidate As String
    Private context As AuditEventContext
    Private Const ItemId As Integer = 2147482100
    Private Const LinkId As Integer = 2147482101
    Private Const RecipeId As Integer = 2147482150
    Private ReadOnly Tables As String() = {"IM_Menu", "IM_Units", "IM_Struct_Menu", "Units", "SERVERS", "Users", "Users_Validations", "Network_Edit_Tracker", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change"}

    Public Function Main(args As String()) As Integer
        Try
            Check(AuditContract.ItemUnitFields("CatalogItem").Length = 2, "2 item context fields")
            Check(AuditContract.ItemUnitFields("ItemUnit").Length = 10, "10 actual item-unit fields")
            Check(AuditContract.ItemUnitFields("ItemUnitRecipe").Length = 7, "7 recipe fields")
            Check(AuditContract.Domain("ItemUnit.OfferPriceChanged") = "Catalog.ItemUnits", "closed item-unit domain")
            Reject(Sub() AuditContract.ItemUnitInputs("ItemUnit.Arbitrary"), "unknown operation denied", "غير معتمدة")
            If args.Length >= 2 Then
                If args.Length = 4 Then candidate = File.ReadAllText(args(3))
                Live(args(1))
            End If
            Console.WriteLine("PASS " & checks & " item-unit checks.") : Return 0
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

    Private Function SavedId(result As AuditRequestResult) As Integer
        Return Integer.Parse(AuditCanonical.ParseXml(result.ResultXml).Element("Values").Elements("Value").Single(Function(x) CStr(x.Attribute("name")) = "EntityId").Value, CultureInfo.InvariantCulture)
    End Function

    Private Function Command(name As String, Optional target As Integer = LinkId) As SqlCommand
        Dim cmd As New SqlCommand(name), create = name = "IM_Units_insert"
        cmd.Parameters.Add(If(create, "@IM_ID", "@U_IM_ID"), SqlDbType.Int).Value = If(create, ItemId, target)
        If name <> "IM_Units_DELETE" Then cmd.Parameters.AddWithValue("@Price", 12.345D)
        If create Then cmd.Parameters.Add("@U_ID", SqlDbType.Int).Value = 1
        If name = "IM_Units_UpdatePrice" Then cmd.Parameters.Add("@is_Default", SqlDbType.Bit).Value = True
        Return cmd
    End Function

    Private Function Request(cn As SqlConnection, tx As SqlTransaction, cmd As SqlCommand, Optional checkBarcode As Boolean = False) As AuditOperationRequest
        Dim op = AuditItemUnits.Operation(cmd)
        context.ScreenCode = AuditContract.ItemUnitScreens(op)(0)
        Return AuditItemUnits.Request(context, cmd, AuditItemUnits.Capture(cn, tx, ItemId, If(op = "ItemUnit.Created", 0, CInt(cmd.Parameters("@U_IM_ID").Value))), checkBarcode)
    End Function

    Private Sub Fixture(work As Action(Of SqlConnection, SqlTransaction))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    If candidate IsNot Nothing Then Q(cn, tx, candidate)
                    Q(cn, tx, "INSERT dbo.IM_Menu(IM_ID,item_name,item_nameSales,GM_ID,isValid,Row_Enabled) VALUES(" & ItemId & ",N'صنف اختبار',N'صنف اختبار بيع',1,0,1)")
                    Q(cn, tx, "INSERT dbo.IM_Units(U_IM_ID,IM_ID,U_ID,Barcode,Price,Min_SP,Min_SP_2,is_Default,Row_Enabled,User_ID,OFFER_PRICE) VALUES(" & LinkId & "," & ItemId & ",1,N'AuditItemUnit021',7.125,NULL,6,0,1,1,NULL),(" & (LinkId + 1) & "," & ItemId & ",2,N'AuditItemUnit021B',9.125,8,NULL,1,0,1,2.375)")
                    Q(cn, tx, "INSERT dbo.IM_Struct_Menu(T_ID,IM_ID,U_IM_ID,Struct_IM_ID,U_ID,Qty,Row_Enabled,User_ID) VALUES(" & RecipeId & "," & ItemId & "," & LinkId & ",1,1,1.234567890123,1,1)")
                    work(cn, tx)
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
    End Sub

    Private Function Graph(cn As SqlConnection, tx As SqlTransaction) As String
        Return AuditItemUnits.Fingerprint(AuditItemUnits.Capture(cn, tx, ItemId, LinkId))
    End Function

    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim builder As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS ItemUnits021 rollback", .ConnectTimeout = 5, .Pooling = False}
        If builder.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database.")
        connection = builder.ConnectionString
        context = New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "021", .ScreenCode = "ItemsMenu"}
        Dim counts As Long(), definitions As String(), domains As String, unitsHash As String, recipeHash As String
        Dim procedures = {"IM_Units_insert", "IM_Units_UpdatePrice", "IM_Units_DELETE", "IM_Units_UpdatePriceBy_Val", "IM_Units_Update_OFFER_Price", "AA_GET_MAX_IM_UNITS", "AG_BalanceMV_GetSerial_Barcode", "IM_Check_Barcode", "Network_Audit_Request_Begin", "Network_Audit_Event_Write"}
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other clients")
            context.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid)
            Check(context.SourceId = Guid.Parse("3c520084-0214-43d4-a5fb-797a179c7a7c"), "approved source")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(" & String.Join(",", Tables.Select(Function(x) "OBJECT_ID('dbo." & x & "')")) & ")")) = 0, "no unreviewed triggers")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.IM_Menu WHERE IM_ID=" & ItemId)) = 0, "unused item fixture")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.IM_Units WHERE U_IM_ID BETWEEN " & LinkId & " AND " & (LinkId + 25))) = 0, "unused unit fixtures")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.IM_Struct_Menu WHERE T_ID BETWEEN " & RecipeId & " AND " & (RecipeId + 35))) = 0, "unused recipe fixtures")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.Units WHERE U_ID IN(1,2)")) = 2, "base units present")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditItemUnitFail021')") Is DBNull.Value, "failure trigger name unused")
            counts = Tables.Select(Function(x) CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.[" & x & "]"))).ToArray()
            definitions = procedures.Select(Function(x) CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & x & "'))"))).ToArray()
            domains = CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1"))
            unitsHash = AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, Nothing, "SELECT * FROM dbo.IM_Units ORDER BY U_IM_ID"))
            recipeHash = AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, Nothing, "SELECT * FROM dbo.IM_Struct_Menu ORDER BY T_ID"))
        End Using
        For Each screen In {"ItemsMenu", "Add_Unit"}
            Dim activeScreen = screen
            Fixture(Sub(cn, tx)
                        Dim cmd = Command("IM_Units_insert")
                        If activeScreen = "Add_Unit" Then cmd.Parameters.Add("@Row_Enabled", SqlDbType.Bit).Value = True
                        Dim before = AuditItemUnits.Capture(cn, tx, ItemId, 0)
                        context.ScreenCode = activeScreen
                        Dim r = AuditItemUnits.Request(context, cmd, before, False)
                        Dim result = AuditItemUnits.ExecuteInTransaction(cn, tx, r), id = SavedId(result)
                        Dim row = AuditItemUnits.Capture(cn, tx, ItemId, id).Units.Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("U_IM_ID")) = id)
                        Check(id > 0 AndAlso id < 2000000 AndAlso CDec(row("Price")) = 12.345D, "create actual id/price " & activeScreen)
                        Check(Not String.IsNullOrWhiteSpace(CStr(row("Barcode"))) AndAlso IsDBNull(row("Min_SP")) AndAlso IsDBNull(row("OFFER_PRICE")), "actual generated barcode/nulls " & activeScreen)
                        Check(CBool(row("Row_Enabled")) = (activeScreen = "Add_Unit"), "original row-enabled policy " & activeScreen)
                        Check(SavedId(AuditItemUnits.ExecuteInTransaction(cn, tx, r)) = id, "same request returns same id " & activeScreen)
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND c.BeforeState=1")) = 10, "all 10 created values " & activeScreen)
                        Dim priceInput = r.Inputs.Single(Function(x) x.Name = "UnitPrice")
                        priceInput.Value = AuditValue.Capture("Decimal", 99D)
                        Reject(Sub() AuditItemUnits.ExecuteInTransaction(cn, tx, r), "same id changed payload denied " & activeScreen, "")
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Dim cmd = Command("IM_Units_UpdatePrice")
                    cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, -1).Value = New String("X"c, 60)
                    cmd.Parameters.AddWithValue("@Min_SP_2", 1.234D)
                    Dim r = Request(cn, tx, cmd)
                    AuditItemUnits.ExecuteInTransaction(cn, tx, r)
                    Dim s = AuditItemUnits.Capture(cn, tx, ItemId, LinkId), row = s.Units.Rows(0)
                    Check(CStr(row("Barcode")).Length = 50 AndAlso CDec(row("Price")) = 12.345D AndAlso IsDBNull(row("Min_SP")), "original barcode length and nullable minima")
                    Check(CInt(row("is_Default")) = 1 AndAlso CInt(s.Units.Rows(1)("is_Default")) = 0, "default transfers across sibling units")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND c.FieldCode='DefaultFlag' AND c.BeforeValue<>c.AfterValue")) = 2, "both default changes audited")
                    cmd.Parameters("@is_Default").Value = False
                    AuditItemUnits.ExecuteInTransaction(cn, tx, Request(cn, tx, cmd))
                    Check(CInt(Q(cn, tx, "SELECT is_Default FROM dbo.IM_Units WHERE U_IM_ID=" & LinkId)) = 1, "unchecked default retains original procedure behavior")
                End Sub)
        For Each procedure In {"IM_Units_UpdatePriceBy_Val", "IM_Units_Update_OFFER_Price"}
            Dim proc = procedure
            Fixture(Sub(cn, tx)
                        Dim cmd = Command(proc)
                        cmd.Parameters("@Price").Value = 0D
                        Dim r = Request(cn, tx, cmd)
                        AuditItemUnits.ExecuteInTransaction(cn, tx, r)
                        Dim column = If(proc = "IM_Units_UpdatePriceBy_Val", "Price", "OFFER_PRICE")
                        Check(CDec(Q(cn, tx, "SELECT " & column & " FROM dbo.IM_Units WHERE U_IM_ID=" & LinkId)) = 0D, "zero value " & proc)
                        Check(CDec(Q(cn, tx, "SELECT Price FROM dbo.IM_Units WHERE U_IM_ID=" & (LinkId + 1))) = 9.125D, "sibling price unchanged " & proc)
                        Check(AuditItemUnits.ExecuteInTransaction(cn, tx, r).Status = "Completed", "replay price " & proc)
                        cmd.Parameters("@Price").Value = -1.125D
                        AuditItemUnits.ExecuteInTransaction(cn, tx, Request(cn, tx, cmd))
                        Check(CDec(Q(cn, tx, "SELECT " & column & " FROM dbo.IM_Units WHERE U_IM_ID=" & LinkId)) = -1.125D, "original negative-price policy " & proc)
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "DECLARE @i int=1; WHILE @i<=30 BEGIN INSERT dbo.IM_Struct_Menu(T_ID,IM_ID,U_IM_ID,Struct_IM_ID,U_ID,Qty,Row_Enabled,User_ID) VALUES(" & RecipeId & "+@i," & ItemId & "," & LinkId & ",1,1,1,1,1); SET @i+=1; END")
                    Dim r = Request(cn, tx, Command("IM_Units_DELETE"))
                    AuditItemUnits.ExecuteInTransaction(cn, tx, r)
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.IM_Units WHERE U_IM_ID=" & LinkId)) = 0, "target link removed")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.IM_Struct_Menu WHERE U_IM_ID=" & LinkId)) = 0, "31 dependent recipe rows removed")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND c.AfterState=1")) = 227, "all deletion values captured")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) = 4, "graph split into bounded event parts")
                    Check(AuditItemUnits.ExecuteInTransaction(cn, tx, r).Status = "Completed", "delete replay")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, Command("IM_Units_UpdatePrice"))
                    Q(cn, tx, "UPDATE dbo.IM_Units SET Price=8 WHERE U_IM_ID=" & (LinkId + 1))
                    Reject(Sub() AuditItemUnits.ExecuteInTransaction(cn, tx, r), "stale sibling graph rejected", "تغير الصنف")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, Command("IM_Units_DELETE"))
                    Q(cn, tx, "UPDATE dbo.IM_Struct_Menu SET Qty=2 WHERE T_ID=" & RecipeId)
                    Reject(Sub() AuditItemUnits.ExecuteInTransaction(cn, tx, r), "stale recipe graph rejected", "تغير الصنف")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim nextId = CInt(Q(cn, tx, "DECLARE @i int; EXEC @i=dbo.AA_GET_MAX_IM_UNITS; SELECT @i;"))
                    Q(cn, tx, "INSERT dbo.IM_Struct_Menu(T_ID,IM_ID,U_IM_ID,Struct_IM_ID,U_ID,Qty,Row_Enabled,User_ID) VALUES(" & (RecipeId + 1) & "," & ItemId & "," & nextId & ",1,1,1,1,1)")
                    Reject(Sub() AuditItemUnits.ExecuteInTransaction(cn, tx, Request(cn, tx, Command("IM_Units_insert"))), "orphan recipe cannot silently become a new link child", "مكونات يتيمة")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim cmd = Command("IM_Units_insert")
                    cmd.Parameters.AddWithValue("@Barcode", "AuditItemUnit021")
                    Reject(Sub() AuditItemUnits.ExecuteInTransaction(cn, tx, Request(cn, tx, cmd, True)), "barcode policy repeated in transaction", "باركود متكرر")
                    Check(AuditItemUnits.ExecuteInTransaction(cn, tx, Request(cn, tx, cmd, False)).ResultCode.GetValueOrDefault() = 1, "multiple barcode option preserved")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, Command("IM_Units_UpdatePrice"))
                    r.Context.ScreenCode = "POS"
                    Reject(Sub() AuditContract.Request(r), "unreviewed screen denied", "غير معتمدة")
                    r.Context.ScreenCode = "Update_IM_Unit"
                    r.Inputs.Add(New AuditInput With {.Name = "Password", .ValueType = "String", .Value = AuditValue.Capture("String", "not-a-secret")})
                    Reject(Sub() AuditContract.Request(r), "arbitrary input denied", "")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, Command("IM_Units_UpdatePrice"))
                    Q(cn, tx, "UPDATE dbo.Users SET is_Allow=0 WHERE User_ID=1")
                    Reject(Sub() AuditItemUnits.ExecuteInTransaction(cn, tx, r), "disabled actor denied", "غير مسموح")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, Command("IM_Units_UpdatePrice"))
                    Q(cn, tx, "UPDATE dbo.Users_Validations SET IM=0 WHERE T_ID=1")
                    Reject(Sub() AuditItemUnits.ExecuteInTransaction(cn, tx, r), "revoked item permission denied", "صلاحية")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, Command("IM_Units_DELETE"))
                    Q(cn, tx, "UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code=""Catalog.ItemUnits""]') WHERE ConfigId=1")
                    Reject(Sub() AuditItemUnits.ExecuteInTransaction(cn, tx, r), "disabled domain blocks mutation", "")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.IM_Units WHERE U_IM_ID=" & LinkId)) = If(tx.Connection Is Nothing, 0, 1), "domain failure blocks mutation or rolls back fixture")
                End Sub)
        For Each procedure In {"IM_Units_DELETE", "IM_Units_UpdatePrice", "IM_Units_insert"}
            Dim proc = procedure
            Fixture(Sub(cn, tx)
                        Dim before = Graph(cn, tx), r = Request(cn, tx, Command(proc))
                        Q(cn, tx, "CREATE TRIGGER dbo.AuditItemUnitFail021 ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN IF EXISTS(SELECT 1 FROM inserted WHERE EventCode LIKE 'ItemUnit.%' AND EventOrdinal=2) THROW 51121,'forced detail failure',1; END")
                        Reject(Sub() AuditItemUnits.ExecuteInTransaction(cn, tx, r), "audit detail failure " & proc, "forced detail failure")
                        If tx.Connection IsNot Nothing Then tx.Rollback()
                    End Sub)
            Using cn As New SqlConnection(connection)
                cn.Open()
                Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.IM_Units WHERE U_IM_ID=" & LinkId)) = 0, "failed fixture rolled back " & proc)
            End Using
        Next
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, Command("IM_Units_DELETE"))
                    Q(cn, tx, "CREATE TRIGGER dbo.AuditItemUnitFail021 ON dbo.IM_Units AFTER DELETE AS BEGIN THROW 51121,'forced legacy delete failure',1; END")
                    Reject(Sub() AuditItemUnits.ExecuteInTransaction(cn, tx, r), "legacy rollback cannot report completed", "")
                End Sub)
        Using cn As New SqlConnection(connection)
            cn.Open()
            For i = 0 To Tables.Length - 1
                Check(CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.[" & Tables(i) & "]")) = counts(i), "rollback count " & Tables(i))
            Next
            For i = 0 To procedures.Length - 1
                Check(CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & procedures(i) & "'))")) = definitions(i), "procedure unchanged " & procedures(i))
            Next
            Check(AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, Nothing, "SELECT * FROM dbo.IM_Units ORDER BY U_IM_ID")) = unitsHash, "all original unit data unchanged")
            Check(AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, Nothing, "SELECT * FROM dbo.IM_Struct_Menu ORDER BY T_ID")) = recipeHash, "all original recipe data unchanged")
            Check(CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = domains, "domain rollback")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.Network_Audit_Request WHERE State<>1")) = 0, "no incomplete requests")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditItemUnitFail021')") Is DBNull.Value, "failure trigger removed")
        End Using
    End Sub
End Module
