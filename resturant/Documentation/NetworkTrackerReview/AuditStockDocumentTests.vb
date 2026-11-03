Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq

Friend Module AuditStockDocumentTests
    Private checks As Integer
    Private connection As String
    Private candidate As String
    Private context As AuditEventContext
    Private Const First As Integer = 2147478000
    Private ReadOnly Tables As String() = {"Agents_Balance_MV", "Outsale_Details", "InSale_Details", "IMEX_Details", "Store_Trans_Details", "ST_Balance", "SERVERS", "Stores", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change", "Network_Edit_Tracker"}
    Private ReadOnly Procedures As String() = {"Outsale_Details_Insert", "Outsale_Details_Delete", "InSale_Details_Insert", "InSale_Details_Delete", "IMEX_Details_Insert", "IMEX_Details_Delete", "ST_Trans_Details_insert", "ST_Trans_Details_Delete", "Recalculate_Agent_Bill_Total", "Network_Audit_Request_Begin", "Network_Audit_Event_Write"}

    Public Function Main(args As String()) As Integer
        Try
            If args.Length = 1 AndAlso args(0) = "--catalog" Then
                For Each entity In {"StockDocument", "StockReceiptLine", "StockIssueLine", "StockDamageLine", "StockTransferLine", "StockBucket"}
                    For Each field In AuditContract.StockDocumentFields(entity)
                        Console.WriteLine(entity & "|" & field)
                    Next
                Next
                Return 0
            End If
            Check(AuditContract.Domain("StockDocument.LineAdded") = "Inventory.DocumentLines", "closed domain")
            For Each kind In {8, 12, 17, 35}
                Check(AuditContract.StockDocumentFields(AuditContract.StockDocumentSpec(kind)(2)).Length = If(kind = 8, 13, If(kind = 12, 15, 14)), "complete line fields " & kind)
            Next
            Reject(Sub() AuditContract.StockDocumentSpec(1), "sales not accepted")
            Reject(Sub() AuditStockDocumentLines.Operation(New SqlCommand("DELETE FROM IM_Menu")), "raw SQL not accepted")
            If args.Length >= 2 Then
                If args.Length = 4 Then candidate = File.ReadAllText(args(3))
                Live(args(1))
            End If
            Console.WriteLine("PASS " & checks & " stock document checks.") : Return 0
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
    Private Function Request(cn As SqlConnection, tx As SqlTransaction, kind As Integer, Optional line As Integer = 0, Optional quantity As Decimal = 2.123D, Optional cost As Decimal = 3.456D, Optional expiry As String = Nothing, Optional touch As Boolean = False, Optional delta As Integer = 0) As AuditOperationRequest
        Dim spec = AuditContract.StockDocumentSpec(kind), create = line = 0, prefix = If(kind = 35, "Outsale", If(kind = 17, "InSale", If(kind = 8, "IMEX", "ST_Trans")))
        Dim cmd As New SqlCommand(If(delta <> 0, "StockReceipt.QuantityStep", prefix & "_Details_" & If(create, "Insert", "Delete")))
        If create Then
            cmd.Parameters.AddWithValue("@" & spec(1), First + kind)
            cmd.Parameters.AddWithValue("@IM_ID", 1) : cmd.Parameters.AddWithValue("@U_ID", 2)
            cmd.Parameters.AddWithValue(If(kind = 12, "@ST_F_ID", "@ST_ID"), 1)
            If kind = 12 Then cmd.Parameters.AddWithValue("@ST_To_ID", First + 90)
            cmd.Parameters.AddWithValue(If(kind = 12, "@QTY", "@QYT"), quantity)
            cmd.Parameters.AddWithValue("@Price", cost)
            If kind <> 12 Then cmd.Parameters.AddWithValue("@Total", quantity * cost)
            cmd.Parameters.AddWithValue("@Barcode", "audit-026")
            cmd.Parameters.AddWithValue("@On_Update", True)
            If expiry IsNot Nothing OrElse kind = 12 Then cmd.Parameters.AddWithValue("@D_Vaild", If(expiry, ""))
            If kind = 35 Then cmd.Parameters.AddWithValue("@SPRICE", 7.5R)
        Else
            cmd.Parameters.AddWithValue("@T_ID", line)
            If delta <> 0 Then
                cmd.Parameters.AddWithValue("@DisplayedQuantity", quantity)
                cmd.Parameters.AddWithValue("@QuantityDelta", delta)
            End If
        End If
        context.ScreenCode = If(touch, "Outside_Sales_Touch", spec(If(create, 5, 6)).Split(","c)(0))
        Dim r = AuditStockDocumentLines.Request(context, cmd, First + kind, kind)
        r.Inputs.Single(Function(x) x.Name = "ExpectedStockDocumentSnapshot").Value = AuditValue.Capture("String", AuditStockDocumentLines.Fingerprint(AuditStockDocumentLines.Capture(cn, tx, r)))
        Return r
    End Function

    Private Sub Fixture(work As Action(Of SqlConnection, SqlTransaction))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    If candidate IsNot Nothing Then Q(cn, tx, candidate)
                    Q(cn, tx, "UPDATE dbo.SERVERS SET START_ID=" & First & ",END_ID=" & (First + 99))
                    Q(cn, tx, "INSERT dbo.Stores(ST_ID,ST_Name) VALUES(" & (First + 90) & ",N'Audit026 target')")
                    For Each kind In {8, 12, 17, 35}
                        Dim number = AuditContract.StockDocumentSpec(kind)(3)
                        Q(cn, tx, "INSERT dbo.Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Pure,Discount,[" & number & "]) VALUES(" & (First + kind) & ",1,'20261027'," & kind & ",0,0,1,0,0,0,26)")
                    Next
                    work(cn, tx)
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
    End Sub

    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim builder As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS StockLines026 rollback", .ConnectTimeout = 5, .Pooling = False}
        If builder.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database")
        connection = builder.ConnectionString
        context = New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "026", .ScreenCode = "Outside_Sales_Touch"}
        Dim fingerprints As String(), definitions As String(), domains As String
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other clients")
            context.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM Network_Audit_Source WHERE ConfigId=1"), Guid)
            Check(context.SourceId = Guid.Parse("3c520084-0214-43d4-a5fb-797a179c7a7c"), "approved source")
            For Each table In {"Agents_Balance_MV", "Outsale_Details", "InSale_Details", "IMEX_Details", "Store_Trans_Details"}
                Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.[" & table & "] WHERE T_ID BETWEEN " & First & " AND " & (First + 99))) = 0, "free test range " & table)
            Next
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM Stores WHERE ST_ID=" & (First + 90))) = 0, "free target store")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditStockFail026')") Is DBNull.Value, "unused failure trigger")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('Outsale_Details'),OBJECT_ID('InSale_Details'),OBJECT_ID('IMEX_Details'),OBJECT_ID('Store_Trans_Details'),OBJECT_ID('Agents_Balance_MV'))")) = 0, "no unreviewed triggers")
            fingerprints = Tables.Select(Function(t) Finger(cn, "SELECT * FROM dbo.[" & t & "] ORDER BY " & If(t = "SERVERS", "START_ID", If(t = "Stores", "ST_ID", If(t = "Network_Audit_Change", "EventId,ChangeNo", If(t = "Network_Audit_Event", "EventId", If(t = "Network_Audit_Request", "RequestId", "T_ID"))))))).ToArray()
            definitions = Procedures.Select(Function(p) CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & p & "'))"))).ToArray()
            domains = CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM Network_Audit_Source WHERE ConfigId=1"))
        End Using
        For Each kind In {8, 12, 17, 35}
            For Each cost In {0D, 3.456D}
                Dim k = kind, price = cost
                Fixture(Sub(cn, tx)
                            Dim r = Request(cn, tx, k, cost:=price), result = AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r), id = ResultId(result), spec = AuditContract.StockDocumentSpec(k)
                            Check(id = First + 1, "allocated line " & k & "/" & price)
                            Dim rows = AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM dbo.[" & spec(0) & "] WHERE T_ID=@P0", id)
                            Check(rows.Rows.Count = 1 AndAlso CDec(rows.Rows(0)(If(k = 12, "QTY", "QYT"))) = 2.12D, "quantity SQL rounding " & k)
                            Check(CDec(rows.Rows(0)(If(k = 12, "Price", "Cost"))) = Decimal.Round(price, 2, MidpointRounding.AwayFromZero), "cost rounding " & k)
                            Check(ResultId(AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r)) = id, "idempotent addition " & k)
                            Dim header = AuditSalesQuantity.Rows(cn, tx, "SELECT Total,Pure FROM dbo.Agents_Balance_MV WHERE T_ID=@P0", First + k).Rows(0)
                            Check(CDec(header("Total")) = CDec(rows.Rows(0)("Total")) AndAlso CDec(header("Pure")) = CDec(header("Total")), "header recalculated " & k)
                            Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) >= 2, "stock captured " & k)
                            Dim deletion = Request(cn, tx, k, id), stale = Request(cn, tx, k, id)
                            AuditStockDocumentLines.ExecuteInTransaction(cn, tx, deletion)
                            Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM [" & spec(0) & "] WHERE T_ID=" & id)) = 0, "deleted " & k)
                            Check(CDec(Q(cn, tx, "SELECT Total FROM Agents_Balance_MV WHERE T_ID=" & (First + k))) = 0D, "delete total " & k)
                            Check(AuditStockDocumentLines.ExecuteInTransaction(cn, tx, deletion).Status = "Completed", "delete replay " & k)
                            Reject(Sub() AuditStockDocumentLines.ExecuteInTransaction(cn, tx, stale), "stale deletion rejected " & k)
                        End Sub)
            Next
            Dim current = kind
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, current)
                        Q(cn, tx, "UPDATE dbo.Agents_Balance_MV SET Discount=1 WHERE T_ID=" & r.EntityId)
                        Reject(Sub() AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r), "changed header rejected " & current, "تغير")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, current)
                        Q(cn, tx, "UPDATE dbo.Users SET is_Allow=0 WHERE User_ID=1")
                        Reject(Sub() AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r), "disabled actor " & current)
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, current)
                        r.Context.ScreenCode = "Sales"
                        Reject(Sub() AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r), "wrong screen " & current)
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, current)
                        Q(cn, tx, "CREATE TRIGGER dbo.AuditStockFail026 ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN THROW 51126,'forced stock audit failure',1; END")
                        Reject(Sub() AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r), "audit failure atomic " & current, "forced stock audit failure")
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Dim added = Request(cn, tx, 35, touch:=True), id = ResultId(AuditStockDocumentLines.ExecuteInTransaction(cn, tx, added))
                    Dim stepRequest = Request(cn, tx, 35, id, 2.12D, touch:=True, delta:=1)
                    AuditStockDocumentLines.ExecuteInTransaction(cn, tx, stepRequest)
                    Check(CDec(Q(cn, tx, "SELECT QYT FROM Outsale_Details WHERE T_ID=" & id)) = 3.12D, "touch increment")
                    Check(AuditStockDocumentLines.ExecuteInTransaction(cn, tx, stepRequest).Status = "Completed", "touch retry no duplicate")
                    Dim down = Request(cn, tx, 35, id, 3.12D, touch:=True, delta:=-1)
                    AuditStockDocumentLines.ExecuteInTransaction(cn, tx, down)
                    Check(CDec(Q(cn, tx, "SELECT QYT FROM Outsale_Details WHERE T_ID=" & id)) = 2.12D, "touch decrement")
                    Dim wrong = Request(cn, tx, 35, id, 1D, touch:=True, delta:=1)
                    Reject(Sub() AuditStockDocumentLines.ExecuteInTransaction(cn, tx, wrong), "displayed quantity stale", "تغيرت")
                End Sub)
        For Each kind In {8, 12, 17, 35}
            Dim k = kind
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, k), flag = AuditContract.StockDocumentSpec(k)(4)
                        Q(cn, tx, "UPDATE dbo.Users_Validations SET [" & flag & "]=0 WHERE T_ID=(SELECT Valid_ID FROM dbo.Users WHERE User_ID=1)")
                        Reject(Sub() AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r), "permission revoked " & k)
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, k)
                        Q(cn, tx, "UPDATE Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code=""Inventory.DocumentLines""]') WHERE ConfigId=1")
                        Reject(Sub() AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r), "domain disabled " & k)
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, k), table = AuditContract.StockDocumentSpec(k)(0)
                        Q(cn, tx, "CREATE TRIGGER dbo.AuditStockFail026 ON dbo.[" & table & "] AFTER INSERT AS BEGIN THROW 51126,'forced line write',1; END")
                        Reject(Sub() AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r), "business failure rollback " & k)
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim id = ResultId(AuditStockDocumentLines.ExecuteInTransaction(cn, tx, Request(cn, tx, k)))
                        Dim r = Request(cn, tx, k, id), table = AuditContract.StockDocumentSpec(k)(0)
                        Q(cn, tx, "CREATE TRIGGER dbo.AuditStockFail026 ON dbo.[" & table & "] AFTER DELETE AS BEGIN THROW 51126,'forced line delete',1; END")
                        Reject(Sub() AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r), "delete failure including swallowed rollback " & k)
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim id = ResultId(AuditStockDocumentLines.ExecuteInTransaction(cn, tx, Request(cn, tx, k))), r = Request(cn, tx, k, id)
                        Q(cn, tx, "CREATE TRIGGER dbo.AuditStockFail026 ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN IF EXISTS(SELECT 1 FROM inserted WHERE EventOrdinal=2) THROW 51126,'forced stock balance event',1; END")
                        Reject(Sub() AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r), "second event rollback " & k, "forced stock balance event")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, k)
                        AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r)
                        r.Inputs.Single(Function(x) x.Name = "RequestedCost").Value = AuditValue.Capture("Decimal", 99D)
                        Reject(Sub() AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r), "request identity conflict " & k)
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, k)
                        Q(cn, tx, "UPDATE dbo.SERVERS SET END_ID=START_ID")
                        Reject(Sub() AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r), "exhausted allocation " & k)
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, k)
                        r.Inputs.Single(Function(x) x.Name = "RequestedBarcode").Value = AuditValue.Capture("String", New String("x"c, 51))
                        Reject(Sub() AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r), "barcode not truncated " & k)
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, 35, expiry:="2027-05-06"), id = ResultId(AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r))
                    Check(CStr(Q(cn, tx, "SELECT D_Vaild FROM Outsale_Details WHERE T_ID=" & id)) = "2027-05-06", "expiry persisted")
                    Dim issue = Request(cn, tx, 17, expiry:="2027-05-06")
                    AuditStockDocumentLines.ExecuteInTransaction(cn, tx, issue)
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM Network_Audit_Change c JOIN Network_Audit_Event e ON c.EventId=e.EventId WHERE e.RequestId='" & issue.RequestId.ToString() & "' AND c.EntityType='StockBucket' AND c.FieldCode='ExpiryText' AND c.AfterValue='2027-05-06'")) = 1, "expiry balance tracked")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, 12)
                    r.Inputs.Single(Function(x) x.Name = "DestinationStoreId").Value = AuditValue.Capture("Integer", 1)
                    Reject(Sub() AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r), "same store transfer rejected")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim id = ResultId(AuditStockDocumentLines.ExecuteInTransaction(cn, tx, Request(cn, tx, 35))), r = Request(cn, tx, 35, id)
                    Using other As New SqlConnection(connection)
                        other.Open()
                        Reject(Sub() Q(other, Nothing, "SET LOCK_TIMEOUT 800; UPDATE dbo.Outsale_Details SET Total=99 WHERE T_ID=" & id), "competing mutation blocked", "Lock request time out")
                    End Using
                    AuditStockDocumentLines.ExecuteInTransaction(cn, tx, r)
                    Check(True, "locked deletion succeeds")
                End Sub)
        Using cn As New SqlConnection(connection)
            cn.Open()
            For i = 0 To Tables.Length - 1
                Dim t = Tables(i), order = If(t = "SERVERS", "START_ID", If(t = "Stores", "ST_ID", If(t = "Network_Audit_Change", "EventId,ChangeNo", If(t = "Network_Audit_Event", "EventId", If(t = "Network_Audit_Request", "RequestId", "T_ID")))))
                Check(Finger(cn, "SELECT * FROM dbo.[" & t & "] ORDER BY " & order) = fingerprints(i), "baseline restored " & t)
            Next
            For i = 0 To Procedures.Length - 1
                Check(CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & Procedures(i) & "'))")) = definitions(i), "definition unchanged " & Procedures(i))
            Next
            Check(CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM Network_Audit_Source WHERE ConfigId=1")) = domains, "domains restored")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditStockFail026')") Is DBNull.Value, "failure trigger removed")
        End Using
    End Sub
End Module
