Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq

Friend Module AuditPurchaseAddTests
    Private checks As Integer
    Private connection As String
    Private candidate As String
    Private context As AuditEventContext
    Private itemId, unitId, storeId As Integer
    Private Const First As Integer = 2147475000
    Private ReadOnly Tables As String() = {"Agents_Balance_MV", "Pch_Details", "IM_Menu", "IM_Units", "Valid_Notes", "ST_Balance", "Pch_Exp_Values", "Treasury_Balance_MV", "Agents_Balance_MV_RCT", "SERVERS", "Users", "Users_Validations", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change", "Network_Edit_Tracker"}
    Private ReadOnly Procedures As String() = {"Pch_Details_Insert", "Valid_Notes_Insert", "AG_Balance_DependingUpdatedBill", "AG_Balance_Update_Total", "AG_Balance_Update_Pied_On_Update", "Recalculate_Agent_Bill_Total", "AA_GET_MAX_ID", "Network_Audit_Request_Begin", "Network_Audit_Event_Write"}

    Public Function Main(args As String()) As Integer
        Try
            Check(AuditContract.Domain("Purchase.LineAdded") = "Inventory.PurchaseAddition", "closed purchase addition domain")
            Check(AuditContract.PurchaseFields("PurchaseLine").Length = 28, "complete purchase row")
            Reject(Sub() AuditContract.PurchaseFields("Receipt"), "receipt entity excluded")
            StaticChecks()
            If args.Length >= 2 Then
                If args.Length = 4 Then candidate = File.ReadAllText(args(3))
                Live(args(1))
            End If
            Console.WriteLine("PASS " & checks & " purchase addition checks.") : Return 0
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
    Private Function Request(cn As SqlConnection, tx As SqlTransaction, Optional kind As Integer = 7, Optional edit As Boolean = False, Optional price As Double = 3.4567R, Optional options As Boolean = True) As AuditOperationRequest
        context.ScreenCode = If(kind = 7, "Pch_IM_card_11", "Invoice_IM_card")
        Dim cmd As New SqlCommand("Pch_Details_Insert")
        cmd.Parameters.AddWithValue("@Pch_T_ID", First + 20)
        cmd.Parameters.AddWithValue("@IM_ID", itemId) : cmd.Parameters.AddWithValue("@U_ID", unitId) : cmd.Parameters.AddWithValue("@ST_ID", storeId)
        cmd.Parameters.AddWithValue("@Price", price) : cmd.Parameters.AddWithValue("@QYT", 2.1256D)
        cmd.Parameters.AddWithValue("@Total", 2.1256R * price) : cmd.Parameters.AddWithValue("@On_Update", edit)
        If options Then
            cmd.Parameters.AddWithValue("@D_Vaild", New DateTime(2028, 3, 4))
            cmd.Parameters.AddWithValue("@Barcode", "اختبار 031")
            For Each pair In {"NewSale", "NewSaleByOne", "Min_SP", "Min_SP_ByOne", "Min_SP_2", "Min_SP_2_ByOne"}
                cmd.Parameters.AddWithValue("@" & pair, 7.4567D)
            Next
            cmd.Parameters.AddWithValue("@Price_By_Foriegn_Cr", 1.2345R)
            cmd.Parameters.AddWithValue("@Price_By_Equal", 3.3333R)
            cmd.Parameters.AddWithValue("@Cr_ID", 1)
            cmd.Parameters.AddWithValue("@Invoice_Type", 0)
        Else
            For Each p In AuditContract.PurchaseInputs().Where(Function(x) x.Split("|"c)(4) = "O")
                cmd.Parameters.AddWithValue("@" & p.Split("|"c)(1), DBNull.Value)
            Next
        End If
        Dim r = AuditPurchaseAdd.Request(context, cmd, First + 20, kind, If(options, {New DateTime(2028, 4, 5), New DateTime(2028, 5, 6)}, New DateTime() {}))
        r.Inputs.Single(Function(x) x.Name = "ExpectedPurchaseSnapshot").Value = AuditValue.Capture("String", AuditPurchaseAdd.Fingerprint(AuditPurchaseAdd.Capture(cn, tx, r)))
        Return r
    End Function
    Private Sub Fixture(work As Action(Of SqlConnection, SqlTransaction), Optional kind As Integer = 7, Optional edit As Boolean = False)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    If candidate IsNot Nothing Then Q(cn, tx, candidate)
                    Q(cn, tx, "UPDATE SERVERS SET START_ID=" & First & ",END_ID=" & (First + 99))
                    Q(cn, tx, "INSERT Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Pure,Discount,Pch_ID,Jrd_ID,EditOpenedAt,EditOpenedBy) VALUES(" & (First + 20) & ",1,'20261027'," & kind & "," & If(edit, 1, 0) & ",0,1,0,0,0,31,31," & If(edit, "GETDATE(),1", "NULL,NULL") & ")")
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

    Private Function StaticRequest(Optional kind As Integer = 7, Optional expiryCount As Integer = 0) As AuditOperationRequest
        Dim c As New AuditEventContext With {.SourceId = Guid.NewGuid(), .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "StaticTests", .SessionId = Guid.NewGuid(), .MachineName = "StaticTests", .ApplicationVersion = "031", .ScreenCode = If(kind = 7, "Pch_IM_card_11", "Invoice_IM_card")}
        Dim cmd As New SqlCommand("Pch_Details_Insert")
        For Each pair In {"Pch_T_ID|1", "IM_ID|2", "U_ID|3", "ST_ID|4"}
            Dim p = pair.Split("|"c) : cmd.Parameters.AddWithValue("@" & p(0), CInt(p(1)))
        Next
        cmd.Parameters.AddWithValue("@QYT", 2.1256D) : cmd.Parameters.AddWithValue("@Price", 3.4567R)
        cmd.Parameters.AddWithValue("@Total", 7.34757032R) : cmd.Parameters.AddWithValue("@On_Update", False)
        Return AuditPurchaseAdd.Request(c, cmd, 1, kind, Enumerable.Range(0, expiryCount).Select(Function(x) New DateTime(2028, 1, 1).AddDays(x)))
    End Function
    Private Function StaticEvent(r As AuditOperationRequest) As AuditEventRequest
        Dim e As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = 1, .EventCode = "Purchase.LineAdded", .Outcome = 1, .EntityType = r.EntityType, .EntityId = r.EntityId, .DocumentNumber = "31", .BusinessDate = New DateTime(2026, 10, 27), .OccurredAtUtc = New DateTime(2026, 10, 27, 12, 0, 0, DateTimeKind.Utc)}
        For Each entity In {"PurchaseDocument", "PurchaseLine"}
            For Each field In AuditContract.PurchaseFields(entity)
                Dim p = field.Split("|"c), value As Object
                Select Case p(2)
                    Case "String" : value = ""
                    Case "Integer" : value = 1
                    Case "Decimal" : value = 1D
                    Case "Float" : value = 1R
                    Case "Boolean" : value = False
                    Case Else : value = New DateTime(2026, 10, 27)
                End Select
                If p(0) = "DocumentType" Then value = CInt(AuditPurchaseAdd.V(r, "DocumentType"))
                If p(0) = "DocumentNumber" Then value = 31
                If p(0) = "PurchaseItemId" Then value = 2
                e.Changes.Add(New AuditFieldChange With {.ChangeNo = e.Changes.Count + 1, .EntityType = entity, .EntityId = If(entity = "PurchaseDocument", "1", "10"), .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1, .Before = If(entity = "PurchaseLine", AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), value)), .After = AuditValue.Capture(p(2), value)})
            Next
        Next
        Return e
    End Function
    Private Sub StaticChecks()
        For Each kind In {7, 9}
            Dim r = StaticRequest(kind)
            Check(r.Inputs.Count = 22, "closed input count")
            Check(r.Inputs.Where(Function(x) x.Value.State = AuditValueState.SqlNull).Count() = 12, "omitted optional inputs remain SQL NULL")
            Check(AuditPurchaseAdd.V(r, "RequestedQuantity") = "2.1256", "quantity precision retained")
            Check(AuditPurchaseAdd.V(r, "RequestedPrice") = "3.4567", "float precision retained")
            Dim e = StaticEvent(r)
            AuditContract.EventData(r, e) : Check(True, "complete static event type " & kind)
            For Each entity In AuditContract.PurchaseEntities()
                For Each field In AuditContract.PurchaseFields(entity)
                    Dim p = field.Split("|"c)
                    Check(AuditContract.FieldType("Purchase.LineAdded", p(0)) = p(2), "field catalogue " & kind & " " & p(0))
                Next
            Next
        Next
        For Each name In {"SelectedItemId", "SelectedUnitId", "SelectedStoreId"}
            Dim r = StaticRequest()
            r.Inputs.Single(Function(x) x.Name = name).Value = AuditValue.Capture("Integer", 0)
            Reject(Sub() AuditContract.Request(r), "zero target " & name)
        Next
        For Each name In {"DocumentType", "AdditionalExpiries", "ExpectedPurchaseSnapshot", "OnUpdate", "RequestedQuantity"}
            Dim r = StaticRequest()
            r.Inputs.Remove(r.Inputs.Single(Function(x) x.Name = name))
            Reject(Sub() AuditContract.Request(r), "missing input " & name)
        Next
        For Each name In {"DocumentType", "ExpectedPurchaseSnapshot", "SelectedItemId"}
            Dim r = StaticRequest()
            r.Inputs.Single(Function(x) x.Name = name).Value = AuditValue.Missing(AuditValueState.SqlNull)
            Reject(Sub() AuditContract.Request(r), "null required " & name)
        Next
        For Each quantity In {0D, -1D, 0.004D}
            Dim r = StaticRequest()
            r.Inputs.Single(Function(x) x.Name = "RequestedQuantity").Value = AuditValue.Capture("Decimal", quantity)
            Reject(Sub() AuditContract.Request(r), "nonpositive SQL quantity")
        Next
        For Each kind In {1, 10}
            Reject(Sub() StaticRequest(kind), "unsupported invoice kind")
        Next
        Check(StaticRequest(7, 100).Inputs.Count = 22, "100 expiry dates accepted")
        Reject(Sub() StaticRequest(7, 101), "101 expiry dates rejected")
        For Each bad In {"broken", "2028-99-99"}
            Dim r = StaticRequest()
            r.Inputs.Single(Function(x) x.Name = "RequestedExpiry").Value = AuditValue.Capture("String", bad)
            Reject(Sub() AuditContract.Request(r), "invalid expiry format")
        Next
        Dim longBarcode = StaticRequest()
        longBarcode.Inputs.Single(Function(x) x.Name = "RequestedBarcode").Value = AuditValue.Capture("String", New String("a"c, 101))
        Reject(Sub() AuditContract.Request(longBarcode), "barcode SQL length")
        For Each bad In {"screen", "parent", "missing", "sqltype", "origin", "state", "number", "kind", "lineparent"}
            Dim r = StaticRequest(), e = StaticEvent(r)
            Select Case bad
                Case "screen" : r.Context.ScreenCode = "Sales"
                Case "parent" : e.EntityId = "2"
                Case "missing" : e.Changes.RemoveAt(0)
                Case "sqltype" : e.Changes(0).SourceSqlType = "float"
                Case "origin" : e.Changes(0).ValueOrigin = 3
                Case "state" : e.Changes(23).Before = AuditValue.Capture("Integer", 1)
                Case "number" : e.DocumentNumber = "32"
                Case "kind" : e.Changes.Single(Function(x) x.FieldCode = "DocumentType").After = AuditValue.Capture("Integer", 9)
                Case "lineparent" : e.Changes.Single(Function(x) x.FieldCode = "PurchaseDocumentId").After = AuditValue.Capture("Integer", 2)
            End Select
            Reject(Sub() AuditContract.EventData(r, e), "invalid event " & bad)
        Next
    End Sub

    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim builder As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS Purchase031 rollback", .Pooling = False, .ConnectTimeout = 5}
        If builder.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database")
        connection = builder.ConnectionString
        context = New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "031", .ScreenCode = "Pch_IM_card_11"}
        Dim fingerprints As String(), definitions As String(), domains As String
        Dim queries As New List(Of String)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other clients")
            context.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM Network_Audit_Source WHERE ConfigId=1"), Guid)
            Check(context.SourceId = Guid.Parse("3c520084-0214-43d4-a5fb-797a179c7a7c"), "approved source")
            Dim reference = AuditSalesQuantity.Rows(cn, Nothing, "SELECT TOP(1) m.IM_ID,u.U_ID FROM IM_Menu m JOIN IM_Units x ON x.IM_ID=m.IM_ID JOIN Units u ON u.U_ID=x.U_ID WHERE m.isStore=1 AND u.U_Cargo=1 ORDER BY m.IM_ID,u.U_ID")
            Check(reference.Rows.Count = 1, "existing stock item and single unit")
            itemId = CInt(reference.Rows(0)(0)) : unitId = CInt(reference.Rows(0)(1))
            storeId = CInt(Q(cn, Nothing, "SELECT MIN(ST_ID) FROM Stores"))
            Check(storeId > 0, "existing store")
            For Each table In {"Agents_Balance_MV", "Pch_Details", "Valid_Notes"}
                Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM [" & table & "] WHERE T_ID BETWEEN " & First & " AND " & (First + 99))) = 0, "test range free " & table)
            Next
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditPurchaseFail031')") Is DBNull.Value, "test trigger name free")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('Pch_Details'),OBJECT_ID('Agents_Balance_MV'),OBJECT_ID('IM_Menu'),OBJECT_ID('IM_Units'),OBJECT_ID('Valid_Notes'))")) = 0, "no unreviewed business triggers")
            For Each table In Tables
                Dim key As String = If(table = "Network_Audit_Change", "EventId,ChangeNo", If(table = "Network_Audit_Event", "EventId", If(table = "Network_Audit_Request", "RequestId", If(table = "SERVERS", "START_ID", If(table = "Users", "User_ID", If(table = "IM_Menu", "IM_ID", If(table = "IM_Units", "U_IM_ID", "T_ID")))))))
                queries.Add("SELECT * FROM dbo.[" & table & "] ORDER BY " & key)
            Next
            fingerprints = queries.Select(Function(qry) Finger(cn, qry)).ToArray()
            definitions = Procedures.Select(Function(p) CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & p & "'))"))).ToArray()
            domains = CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM Network_Audit_Source WHERE ConfigId=1"))
        End Using
        Try
            For Each kind In {7, 9}
                Dim k = kind
                For Each edit In {False, True}
                    Dim updating = edit
                    For Each price In {0R, 3.4567R}
                        Dim p = price
                        For Each options In {False, True}
                            Dim opt = options
                            Fixture(Sub(cn, tx)
                                        Dim r = Request(cn, tx, k, updating, p, opt), before = AuditPurchaseAdd.Capture(cn, tx, r)
                                        Dim id = ResultId(AuditPurchaseAdd.ExecuteInTransaction(cn, tx, r)), after = AuditPurchaseAdd.Capture(cn, tx, r)
                                        Check(id = First + 1, "locked line allocator")
                                        Dim line = after.Lines.Rows.Cast(Of DataRow)().Single(Function(x) CLng(x("T_ID")) = id)
                                        Check(CDbl(line("QYT")) = 2.13R, "SQL quantity rounding")
                                        Check(Math.Abs(CDbl(line("Total")) - 2.1256R * p) < 0.000001R, "original supplied total preserved")
                                        Check(CBool(line("isDepended")), "immediate stock effect")
                                        Dim expectedTotal = If(k = 7, 2.13R * p, 2.1256R * p)
                                        Check(CDec(after.Header.Rows(0)("Total")) = Decimal.Round(CDec(expectedTotal), 3, MidpointRounding.AwayFromZero), "original header formula")
                                        Check(CInt(after.Item.Rows(0)("Last_PchT_ID")) = id, "last purchase pointer")
                                        Check(CDec(after.Item.Rows(0)("Last_Pch_Cargo")) = Decimal.Round(CDec(p), 3, MidpointRounding.AwayFromZero), "last purchase cargo cost")
                                        Dim preQty = CDbl(Decimal.Round(CDec(before.Stock.Rows.Cast(Of DataRow)().Sum(Function(x) CDbl(x("AuditStock")))), 2, MidpointRounding.AwayFromZero)), priorCost = If(IsDBNull(before.Item.Rows(0)("Cost")), 0R, CDbl(before.Item.Rows(0)("Cost")))
                                        Dim expectedCost = If(preQty > 0R, (priorCost * preQty + p * 2.13R) / (preQty + 2.13R), p)
                                        Check(Math.Abs(CDbl(after.Item.Rows(0)("Cost")) - expectedCost) < 0.000001R, "original weighted average")
                                        Check(after.Expiries.Rows.Count = If(opt, 2, 0), "additional expiry notes complete")
                                        If opt Then
                                            Check(CDate(after.Expiries.Rows(0)("VALID_DATE")) = New DateTime(2028, 4, 5), "first expiry")
                                            Check(CDec(line("NewSale")) = 7.46D, "optional price SQL rounding")
                                            Check(after.Units.Rows.Cast(Of DataRow)().Where(Function(x) CInt(x("U_ID")) = unitId).All(Function(x) CDec(x("Price")) = 7.46D), "single unit sale prices")
                                        Else
                                            Check(IsDBNull(line("NewSale")) AndAlso IsDBNull(line("Cr_ID")), "explicit DBNull optionals")
                                            Check(AuditSalesQuantity.Fingerprint(before.Units) = AuditSalesQuantity.Fingerprint(after.Units), "no optional pricing changes")
                                        End If
                                        Check(ResultId(AuditPurchaseAdd.ExecuteInTransaction(cn, tx, r)) = id, "retry same result no duplicate")
                                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM Network_Audit_Change c JOIN Network_Audit_Event e ON c.EventId=e.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND e.EventOrdinal=1")) = 51, "complete header and line event")
                                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) >= 2, "cost and price stock detail events")
                                    End Sub, k, updating)
                        Next
                    Next
                Next
            Next
            For Each table In {"Pch_Details", "IM_Menu", "IM_Units", "Valid_Notes", "Network_Audit_Event", "Network_Audit_Change", "Network_Audit_Request"}
                Dim target = table
                Fixture(Sub(cn, tx)
                            Dim r = Request(cn, tx)
                            Dim action = If(target = "IM_Menu" OrElse target = "IM_Units" OrElse target = "Network_Audit_Request", "UPDATE", "INSERT")
                            Q(cn, tx, "CREATE TRIGGER dbo.AuditPurchaseFail031 ON dbo.[" & target & "] AFTER " & action & " AS BEGIN THROW 51131,'forced purchase failure',1; END")
                            Reject(Sub() AuditPurchaseAdd.ExecuteInTransaction(cn, tx, r), "atomic failure " & target, "forced purchase failure")
                        End Sub)
            Next
            For Each mutation In {"UPDATE Agents_Balance_MV SET Discount=1 WHERE T_ID=" & (First + 20), "UPDATE Agents_Balance_MV SET isVoid=1 WHERE T_ID=" & (First + 20), "UPDATE IM_Menu SET Cost=ISNULL(Cost,0)+1 WHERE IM_ID=" & itemId, "UPDATE IM_Units SET Price=ISNULL(Price,0)+1 WHERE IM_ID=" & itemId}
                Dim sql = mutation
                Fixture(Sub(cn, tx)
                            Dim r = Request(cn, tx)
                            Q(cn, tx, sql)
                            Reject(Sub() AuditPurchaseAdd.ExecuteInTransaction(cn, tx, r), "stale snapshot " & sql)
                        End Sub)
            Next
            For Each edit In {False, True}
                Dim updating = edit
                Fixture(Sub(cn, tx)
                            Dim r = Request(cn, tx, edit:=updating)
                            Q(cn, tx, "UPDATE Users_Validations SET [" & If(updating, "Pch_Update", "Pch") & "]=0 WHERE T_ID=(SELECT Valid_ID FROM Users WHERE User_ID=1)")
                            Reject(Sub() AuditPurchaseAdd.ExecuteInTransaction(cn, tx, r), "revoked permission")
                        End Sub, edit:=updating)
            Next
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx)
                        Q(cn, tx, "UPDATE Users SET is_Allow=0 WHERE User_ID=1")
                        Reject(Sub() AuditPurchaseAdd.ExecuteInTransaction(cn, tx, r), "disabled actor")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx)
                        Q(cn, tx, "UPDATE Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code=""Inventory.PurchaseAddition""]') WHERE ConfigId=1")
                        Reject(Sub() AuditPurchaseAdd.ExecuteInTransaction(cn, tx, r), "domain disabled no legacy fallback")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx)
                        r.Context.ScreenCode = "Sales"
                        Reject(Sub() AuditPurchaseAdd.ExecuteInTransaction(cn, tx, r), "wrong screen")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx)
                        AuditPurchaseAdd.ExecuteInTransaction(cn, tx, r)
                        r.Inputs.Single(Function(x) x.Name = "RequestedTotal").Value = AuditValue.Capture("Float", 999R)
                        Reject(Sub() AuditPurchaseAdd.ExecuteInTransaction(cn, tx, r), "request conflict")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx)
                        Q(cn, tx, "UPDATE SERVERS SET END_ID=START_ID")
                        Reject(Sub() AuditPurchaseAdd.ExecuteInTransaction(cn, tx, r), "line allocator exhausted")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx)
                        Using other As New SqlConnection(connection)
                            other.Open()
                            Reject(Sub() Q(other, Nothing, "SET LOCK_TIMEOUT 800; UPDATE IM_Menu SET Cost=99 WHERE IM_ID=" & itemId), "concurrent cost change locked", "Lock request time out")
                        End Using
                        Check(ResultId(AuditPurchaseAdd.ExecuteInTransaction(cn, tx, r)) > 0, "locked request saved")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Q(cn, tx, "INSERT Pch_Details(T_ID,[Date],Pch_T_ID,IM_ID,U_ID,ST_ID,Price,D_Vaild,QYT,Total,U_Cargo,ST_QTY,isDepended,Org_Price) VALUES(" & (First + 2) & ",GETDATE()," & (First + 20) & "," & itemId & "," & unitId & "," & storeId & ",1,'',1,1,1,1,0,1)")
                        Dim r = Request(cn, tx, edit:=True)
                        AuditPurchaseAdd.ExecuteInTransaction(cn, tx, r)
                        Check(CBool(Q(cn, tx, "SELECT isDepended FROM Pch_Details WHERE T_ID=" & (First + 2))), "existing line approved by original edit procedure")
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM Network_Audit_Change c JOIN Network_Audit_Event e ON c.EventId=e.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND c.EntityType='PurchaseLine' AND c.EntityId='" & (First + 2) & "'")) = 28, "existing line approval audited")
                    End Sub, edit:=True)
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
                Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditPurchaseFail031')") Is DBNull.Value, "no trigger left")
            End Using
        End Try
    End Sub
End Module
