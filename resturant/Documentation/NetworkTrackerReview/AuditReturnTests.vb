Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq

Friend Module AuditReturnTests
    Private checks As Integer
    Private connection As String
    Private candidate As String
    Private context As AuditEventContext
    Private Const First As Integer = 2147477000
    Private ReadOnly Tables As String() = {"Agents_Balance_MV", "Rtn_Contents", "SB_Contents", "Treasury_Balance_MV", "Agents_Balance_MV_RCT", "ST_Balance", "SERVERS", "Stores", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change", "Network_Edit_Tracker"}
    Private ReadOnly Procedures As String() = {"Rtn_Contents_INSERT", "Rtn_Contents_Delete_IM", "SB_Contents_Change_IM_Qty", "Recalculate_Agent_Bill_Total", "Network_Audit_Request_Begin", "Network_Audit_Event_Write"}

    Public Function Main(args As String()) As Integer
        Try
            If args.Length = 1 AndAlso args(0) = "--catalog" Then
                For Each entity In {"ReturnDocument", "ReturnLine", "StockBucket"}
                    For Each field In AuditContract.ReturnFields(entity)
                        Console.WriteLine(entity & "|" & field)
                    Next
                Next
                Return 0
            End If
            Check(AuditContract.Domain("Return.LineAdded") = "Sales.ReturnLines", "closed domain")
            For Each kind In {10, 11}
                Check(AuditContract.ReturnFields(AuditContract.ReturnSpec(kind)(2)).Length = 23, "complete line fields " & kind)
            Next
            Reject(Sub() AuditContract.ReturnSpec(1), "sales not accepted")
            Reject(Sub() AuditReturnLines.Operation(New SqlCommand("DELETE FROM IM_Menu")), "raw SQL not accepted")
            If args.Length >= 2 Then
                If args.Length = 4 Then candidate = File.ReadAllText(args(3))
                Live(args(1))
            End If
            Console.WriteLine("PASS " & checks & " return line checks.") : Return 0
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
    Private Function Request(cn As SqlConnection, tx As SqlTransaction, kind As Integer, Optional line As Integer = 0, Optional quantity As Double = 2.125R, Optional price As Double = 3.456R, Optional expiry As String = Nothing, Optional delta As Integer = 0) As AuditOperationRequest
        Dim create = line = 0
        Dim cmd As New SqlCommand(If(delta <> 0, "Return.QuantityStep", If(create, "Rtn_Contents_INSERT", "Rtn_Contents_Delete_IM")))
        cmd.Parameters.AddWithValue("@On_Update", True)
        cmd.Parameters.AddWithValue("@AllowNegative", True)
        If create Then
            cmd.Parameters.AddWithValue("@SB_T_ID", First + kind)
            cmd.Parameters.AddWithValue("@Rtn_Type_ID", If(kind = 10, 1, 7))
            cmd.Parameters.AddWithValue("@IM_ID", 1) : cmd.Parameters.AddWithValue("@U_ID", 2) : cmd.Parameters.AddWithValue("@ST_ID", 1)
            cmd.Parameters.AddWithValue("@QYT", quantity) : cmd.Parameters.AddWithValue("@Price", price)
            cmd.Parameters.AddWithValue("@Cost", 1.234R) : cmd.Parameters.AddWithValue("@Barcode", "audit-028")
            If expiry IsNot Nothing Then
                cmd.Parameters.AddWithValue("@D_Vaild", expiry)
            Else
                cmd.Parameters.AddWithValue("@D_Vaild", DBNull.Value)
            End If
            For Each name In AuditContract.ReturnOptionalInputs()
                cmd.Parameters.AddWithValue("@" & AuditContract.ReturnParameter(name), DBNull.Value)
            Next
        Else
            cmd.Parameters.AddWithValue("@T_ID", line)
            If delta <> 0 Then
                cmd.Parameters.AddWithValue("@DisplayedQuantity", quantity) : cmd.Parameters.AddWithValue("@QuantityDelta", delta)
            End If
        End If
        Dim r = AuditReturnLines.Request(context, cmd, First + kind, kind)
        r.Inputs.Single(Function(x) x.Name = "ExpectedReturnSnapshot").Value = AuditValue.Capture("String", AuditReturnLines.Fingerprint(AuditReturnLines.Capture(cn, tx, r)))
        Return r
    End Function

    Private Sub Fixture(work As Action(Of SqlConnection, SqlTransaction))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    If candidate IsNot Nothing Then Q(cn, tx, candidate)
                    Q(cn, tx, "UPDATE dbo.SERVERS SET START_ID=" & First & ",END_ID=" & (First + 99))
                    For Each kind In {10, 11}
                        Dim number = AuditContract.ReturnSpec(kind)(3)
                        Q(cn, tx, "INSERT dbo.Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Pure,Discount,[" & number & "]) VALUES(" & (First + kind) & ",1,'20261027'," & kind & ",0,0,1,0,0,0,28)")
                    Next
                    work(cn, tx)
                Finally
                    Try
                        If tx.Connection IsNot Nothing Then tx.Rollback()
                    Catch
                        ' SQL THROW may have already rolled back the outer fixture.
                    End Try
                End Try
            End Using
        End Using
    End Sub

    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim builder As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS ReturnLines028 rollback", .ConnectTimeout = 5, .Pooling = False}
        If builder.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database")
        connection = builder.ConnectionString
        context = New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "028", .ScreenCode = "Returns"}
        Dim fingerprints As String(), definitions As String(), domains As String
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other clients")
            context.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM Network_Audit_Source WHERE ConfigId=1"), Guid)
            Check(context.SourceId = Guid.Parse("3c520084-0214-43d4-a5fb-797a179c7a7c"), "approved source")
            For Each table In {"Agents_Balance_MV", "Rtn_Contents", "SB_Contents"}
                Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.[" & table & "] WHERE T_ID BETWEEN " & First & " AND " & (First + 99))) = 0, "free test range " & table)
            Next
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM Stores WHERE ST_ID=" & (First + 90))) = 0, "free target store")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditReturnFail028')") Is DBNull.Value, "unused failure trigger")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('Rtn_Contents'),OBJECT_ID('SB_Contents'),OBJECT_ID('Agents_Balance_MV'))")) = 0, "no unreviewed triggers")
            fingerprints = Tables.Select(Function(t) Finger(cn, "SELECT * FROM dbo.[" & t & "] ORDER BY " & If(t = "SERVERS", "START_ID", If(t = "Stores", "ST_ID", If(t = "Network_Audit_Change", "EventId,ChangeNo", If(t = "Network_Audit_Event", "EventId", If(t = "Network_Audit_Request", "RequestId", "T_ID"))))))).ToArray()
            definitions = Procedures.Select(Function(p) CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & p & "'))"))).ToArray()
            domains = CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM Network_Audit_Source WHERE ConfigId=1"))
        End Using
        For Each kind In {10, 11}
            Dim k = kind
            For Each price In {0R, 3.456R}
                Dim p = price
                Fixture(Sub(cn, tx)
                            Dim r = Request(cn, tx, k, price:=p), result = AuditReturnLines.ExecuteInTransaction(cn, tx, r), id = ResultId(result)
                            Check(id = First + 1, "return allocation " & k)
                            Dim row = AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM Rtn_Contents WHERE T_ID=@P0", id).Rows(0)
                            Check(CDbl(row("QTY")) = 2.125R, "float quantity preserved")
                            Check(CDec(row("Price")) = Decimal.Round(CDec(p), 2, MidpointRounding.AwayFromZero), "SQL price rounding")
                            Check(CDec(row("Cost")) = 1.23D, "SQL cost rounding")
                            Check(CDec(row("T_Price")) = Decimal.Round(CDec(p * 2.125R), 2, MidpointRounding.AwayFromZero), "original SQL total rounding")
                            Check(CInt(row("Rtn_Type_ID")) = If(k = 10, 1, 7), "return type mapping")
                            Check(CInt(row("isDepended")) = 1, "immediate line effect retained")
                            Check(IsDBNull(row("Serial_Code")) AndAlso IsDBNull(row("Orginal_Bill_T_ID")), "optional null retained")
                            Check(ResultId(AuditReturnLines.ExecuteInTransaction(cn, tx, r)) = id, "idempotent add")
                            Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM Network_Audit_Change c JOIN Network_Audit_Event e ON c.EventId=e.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND e.EventOrdinal=1")) = 46, "complete header and all 23 line fields")
                            Check(CDec(Q(cn, tx, "SELECT Total FROM Agents_Balance_MV WHERE T_ID=" & (First + k))) = CDec(row("T_Price")), "own header recalculated")
                            Dim up = Request(cn, tx, k, id, delta:=1)
                            AuditReturnLines.ExecuteInTransaction(cn, tx, up)
                            Check(CDbl(Q(cn, tx, "SELECT QTY FROM Rtn_Contents WHERE T_ID=" & id)) = 3.125R, "return increment")
                            Check(AuditReturnLines.ExecuteInTransaction(cn, tx, up).Status = "Completed", "quantity replay")
                            AuditReturnLines.ExecuteInTransaction(cn, tx, Request(cn, tx, k, id, 3.125R, delta:=-1))
                            Check(CDbl(Q(cn, tx, "SELECT QTY FROM Rtn_Contents WHERE T_ID=" & id)) = 2.125R, "return decrement")
                            Dim deletion = Request(cn, tx, k, id), stale = Request(cn, tx, k, id)
                            AuditReturnLines.ExecuteInTransaction(cn, tx, deletion)
                            Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM Rtn_Contents WHERE T_ID=" & id)) = 0, "return delete")
                            Check(CDec(Q(cn, tx, "SELECT Total FROM Agents_Balance_MV WHERE T_ID=" & (First + k))) = 0D, "delete recalculates own header")
                            Check(AuditReturnLines.ExecuteInTransaction(cn, tx, deletion).Status = "Completed", "delete replay")
                            Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, stale), "stale delete")
                        End Sub)
            Next
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, k)
                        Q(cn, tx, "UPDATE Agents_Balance_MV SET Discount=1 WHERE T_ID=" & r.EntityId)
                        Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, r), "changed header")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, k)
                        Q(cn, tx, "UPDATE Users SET is_Allow=0 WHERE User_ID=1")
                        Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, r), "disabled user")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, k), flag = AuditContract.ReturnSpec(k)(4)
                        Q(cn, tx, "UPDATE Users_Validations SET [" & flag & "]=0 WHERE T_ID=(SELECT Valid_ID FROM Users WHERE User_ID=1)")
                        Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, r), "permission revoked")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, k)
                        Q(cn, tx, "UPDATE Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code=""Sales.ReturnLines""]') WHERE ConfigId=1")
                        Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, r), "domain disabled, no old fallback")
                    End Sub)
            For Each mutation In {"add", "delete", "quantity"}
                Dim action = mutation
                Fixture(Sub(cn, tx)
                            Dim r = Request(cn, tx, k)
                            If action <> "add" Then
                                Dim id = ResultId(AuditReturnLines.ExecuteInTransaction(cn, tx, r))
                                r = Request(cn, tx, k, id, delta:=If(action = "quantity", 1, 0))
                            End If
                            Q(cn, tx, "CREATE TRIGGER dbo.AuditReturnFail028 ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN THROW 51128,'forced audit failure',1; END")
                            Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, r), "audit failure " & action, "forced audit failure")
                        End Sub)
                Fixture(Sub(cn, tx)
                            Dim r = Request(cn, tx, k)
                            If action <> "add" Then
                                Dim id = ResultId(AuditReturnLines.ExecuteInTransaction(cn, tx, r))
                                r = Request(cn, tx, k, id, delta:=If(action = "quantity", 1, 0))
                            End If
                            Q(cn, tx, "CREATE TRIGGER dbo.AuditReturnFail028 ON dbo.Rtn_Contents AFTER " & If(action = "add", "INSERT", If(action = "delete", "DELETE", "UPDATE")) & " AS BEGIN THROW 51128,'forced business failure',1; END")
                            Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, r), "business failure " & action)
                        End Sub)
            Next
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, k)
                        Q(cn, tx, "CREATE TRIGGER dbo.AuditReturnFail028 ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN IF EXISTS(SELECT 1 FROM inserted WHERE EventOrdinal=2) THROW 51128,'forced stock event',1; END")
                        Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, r), "later stock event atomic", "forced stock event")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, k)
                        r.Context.ScreenCode = "Sales"
                        Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, r), "wrong screen")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, k)
                        AuditReturnLines.ExecuteInTransaction(cn, tx, r)
                        r.Inputs.Single(Function(x) x.Name = "RequestedPrice").Value = AuditValue.Capture("Float", 99R)
                        Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, r), "request identity conflict")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, k)
                        Q(cn, tx, "UPDATE Agents_Balance_MV SET isDepended=1 WHERE T_ID=" & r.EntityId)
                        Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, r), "approved requires edit opening")
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Dim id = ResultId(AuditReturnLines.ExecuteInTransaction(cn, tx, Request(cn, tx, 10))), r = Request(cn, tx, 10, id, delta:=1)
                    Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, Request(cn, tx, 11, id)), "line belongs to other return")
                    Dim wrong = Request(cn, tx, 10, id, quantity:=1R, delta:=1)
                    Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, wrong), "display stale")
                    Using other As New SqlConnection(connection)
                        other.Open()
                        Reject(Sub() Q(other, Nothing, "SET LOCK_TIMEOUT 800; UPDATE Rtn_Contents SET QTY=99 WHERE T_ID=" & id), "concurrent mutation locked", "Lock request time out")
                    End Using
                    AuditReturnLines.ExecuteInTransaction(cn, tx, r)
                    Check(True, "locked quantity succeeds")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim id = ResultId(AuditReturnLines.ExecuteInTransaction(cn, tx, Request(cn, tx, 10, quantity:=1R)))
                    Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, Request(cn, tx, 10, id, quantity:=1R, delta:=-1)), "cannot step to zero")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, 10, expiry:="2027-05-06")
                    For Each name In AuditContract.ReturnOptionalInputs()
                        Dim type = AuditContract.FieldType(r.OperationCode, name)
                        r.Inputs.Single(Function(x) x.Name = name).Value = AuditValue.Capture(type, If(type = "String", CType("serial-028", Object), If(type = "Integer", CType(1, Object), CType(1.125D, Object))))
                    Next
                    Dim id = ResultId(AuditReturnLines.ExecuteInTransaction(cn, tx, r))
                    Dim row = AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM Rtn_Contents WHERE T_ID=@P0", id).Rows(0)
                    Check(CStr(row("D_Vaild")) = "2027-05-06", "expiry persisted")
                    Check(CStr(row("Serial_Code")) = "serial-028" AndAlso CDec(row("Markter_Val")) = 1.125D AndAlso CDec(row("Saler_Percent")) = 1.13D, "optional values and rounding")
                    AuditReturnLines.ExecuteInTransaction(cn, tx, Request(cn, tx, 10, id, delta:=1))
                    Check(CStr(Q(cn, tx, "SELECT Serial_Code FROM Rtn_Contents WHERE T_ID=" & id)) = "serial-028", "quantity retains source metadata")
                End Sub)
        Fixture(Sub(cn, tx)
                    ' Deliberately overlap IDs in the two detail tables, under different bill headers.
                    Q(cn, tx, "INSERT Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Pure,Discount,SB_ID) VALUES(" & (First + 40) & ",1,'20261027',1,0,0,1,21,21,0,28)")
                    Q(cn, tx, "INSERT SB_Contents(T_ID,Bill_T_ID,IM_ID,U_ID,ST_ID,[Date],Cost,Price,D_Vaild,QTY,T_Price,U_Cargo,ST_QTY,isDepended) VALUES(" & (First + 1) & "," & (First + 40) & ",1,2,1,'20261027',2,7,'',3,21,1,3,0)")
                    Dim salesBefore = AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM SB_Contents WHERE T_ID=@P0", First + 1))
                    Dim headerBefore = AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM Agents_Balance_MV WHERE T_ID=@P0", First + 40))
                    Dim id = ResultId(AuditReturnLines.ExecuteInTransaction(cn, tx, Request(cn, tx, 10)))
                    Check(id = First + 1, "sales and return share detail ID")
                    AuditReturnLines.ExecuteInTransaction(cn, tx, Request(cn, tx, 10, id, delta:=1))
                    Check(CDbl(Q(cn, tx, "SELECT QTY FROM Rtn_Contents WHERE T_ID=" & id)) = 3.125R, "overlap updates return")
                    Check(AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM SB_Contents WHERE T_ID=@P0", id)) = salesBefore, "overlap never updates sales line")
                    Check(AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM Agents_Balance_MV WHERE T_ID=@P0", First + 40)) = headerBefore, "overlap never recalculates other bill")
                End Sub)
        For Each kind In {10, 11}
            Dim k = kind
            Fixture(Sub(cn, tx)
                        Q(cn, tx, "UPDATE Agents_Balance_MV SET isDepended=1,EditOpenedBy=1,EditOpenedAt=GETDATE() WHERE T_ID=" & (First + k))
                        Dim r = Request(cn, tx, k), id = ResultId(AuditReturnLines.ExecuteInTransaction(cn, tx, r))
                        Check(id > 0, "approved with current edit allowed")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, k)
                        Q(cn, tx, "UPDATE Agents_Balance_MV SET isVoid=1 WHERE T_ID=" & (First + k))
                        Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, r), "void return blocked")
                    End Sub)
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, k)
                        r.Inputs.Single(Function(x) x.Name = "RequestedBarcode").Value = AuditValue.Capture("String", New String("x"c, 51))
                        Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, r), "no barcode truncation")
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "UPDATE IM_Menu SET isStore=1 WHERE IM_ID=1")
                    Dim r = Request(cn, tx, 11, quantity:=100000000R, price:=0R)
                    r.Inputs.Single(Function(x) x.Name = "AllowNegative").Value = AuditValue.Capture("Boolean", False)
                    Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, r), "negative stock forbidden inside transaction", "بالسالب")
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "UPDATE IM_Menu SET isStore=1 WHERE IM_ID=1")
                    Dim id = ResultId(AuditReturnLines.ExecuteInTransaction(cn, tx, Request(cn, tx, 11, quantity:=100000000R, price:=0R)))
                    Check(id > 0, "configured negative stock allowed")
                    Dim r = Request(cn, tx, 11, id, quantity:=100000000R, delta:=1)
                    r.Inputs.Single(Function(x) x.Name = "AllowNegative").Value = AuditValue.Capture("Boolean", False)
                    Reject(Sub() AuditReturnLines.ExecuteInTransaction(cn, tx, r), "quantity respects negative-stock policy", "بالسالب")
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
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditReturnFail028')") Is DBNull.Value, "failure trigger removed")
        End Using
    End Sub
End Module
