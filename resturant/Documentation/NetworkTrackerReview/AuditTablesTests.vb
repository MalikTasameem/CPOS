Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq

Friend Module AuditTablesTests
    Private checks As Integer
    Private connection As String
    Private candidate As String
    Private context As AuditEventContext
    Private Const Id As Integer = 2147481400
    Private Const Line As Integer = Id + 10
    Private ReadOnly VerifySql As String() = {
        "SELECT * FROM dbo.[Tables] ORDER BY TB_ID", "SELECT * FROM dbo.Agents_Balance_MV ORDER BY T_ID",
        "SELECT * FROM dbo.SB_Contents ORDER BY T_ID", "SELECT * FROM dbo.TB_AutoPrint_tmp ORDER BY T_ID,[DATE],Printer_IP,Notes",
        "SELECT * FROM dbo.ST_Balance ORDER BY T_ID", "SELECT * FROM dbo.Agents_Balance_MV_RCT ORDER BY T_ID",
        "SELECT * FROM dbo.Treasury_Balance_MV ORDER BY T_ID", "SELECT * FROM dbo.SB_Contents_Structers ORDER BY T_ID",
        "SELECT * FROM dbo.GM_Printer ORDER BY T_ID", "SELECT * FROM dbo.Printers ORDER BY Printer_ID",
        "SELECT User_ID,is_Allow,Valid_ID FROM dbo.Users ORDER BY User_ID",
        "SELECT T_ID,Sales,Transfer_Table FROM dbo.Users_Validations ORDER BY T_ID",
        "SELECT T_ID,TB_Auto_Print FROM dbo.Sys_Features ORDER BY T_ID"}

    Public Function Main(args As String()) As Integer
        Try
            Check(AuditContract.TableFields("TableInvoice").Length = 15, "15 invoice fields")
            Check(AuditContract.TableFields("TableLine").Length = 24, "24 line fields")
            Check(AuditContract.TableFields("TableState").Length = 9, "9 table fields")
            Check(AuditContract.Domain("Table.Transferred") = "Sales.Tables", "closed table domain")
            Reject(Sub() AuditContract.TableInputs("Table.Arbitrary"), "unknown operation denied")
            If args.Length >= 2 Then
                If args.Length = 4 Then candidate = File.ReadAllText(args(3))
                Live(args(1))
            End If
            Console.WriteLine("PASS " & checks & " table-operation checks.") : Return 0
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
            If expected.Length > 0 AndAlso Not ex.ToString().Contains(expected) Then Throw New Exception("Unexpected rejection " & label, ex)
            Check(True, label) : Return
        End Try
        Throw New Exception("Expected rejection " & label)
    End Sub
    Private Function Q(cn As SqlConnection, tx As SqlTransaction, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn, tx) With {.CommandTimeout = 30}
            Return cmd.ExecuteScalar()
        End Using
    End Function
    Private Function Finger(cn As SqlConnection, tx As SqlTransaction, sql As String) As String
        Return AuditSalesQuantity.Fingerprint(AuditSalesQuantity.Rows(cn, tx, sql))
    End Function
    Private Function Command(operation As String) As SqlCommand
        Dim c As New SqlCommand(If(operation = "transfer", "Transform_Tables", If(operation = "delete", "SB_Contents_Delete_TB_IM", "SB_Contents_Change_TB_IM_Qty")))
        If operation = "transfer" Then
            c.Parameters.AddWithValue("@Table_From", Id) : c.Parameters.AddWithValue("@Table_To", Id + 1)
        Else
            c.Parameters.AddWithValue("@Bill_T_ID", Id) : c.Parameters.AddWithValue("@T_ID", Line)
            If operation <> "delete" Then c.Parameters.AddWithValue("@Def", If(operation = "minus", -1, 1))
        End If
        Return c
    End Function
    Private Function Request(cn As SqlConnection, tx As SqlTransaction, operation As String) As AuditOperationRequest
        context.ScreenCode = If(operation = "transfer", "TablesBalance", "TB_BillIM")
        Dim cmd = Command(operation), op = AuditTables.Operation(cmd)
        Return AuditTables.Request(context, cmd, AuditTables.Capture(cn, tx, op, Id, If(operation = "transfer", Id + 1, 0)), 2D)
    End Function
    Private Sub Fixture(work As Action(Of SqlConnection, SqlTransaction))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    If candidate IsNot Nothing Then Q(cn, tx, candidate)
                    Q(cn, tx, "INSERT dbo.[Tables](TB_ID,Flate_ID,T_Name,isActive,isbusy,TB_ORDER_CODE,Start_Open) VALUES(" & Id & ",1,N'مصدر اختبار',1,1,N'Source024','20261001'),(" & (Id + 1) & ",1,N'هدف اختبار',1,0,NULL,NULL)")
                    Q(cn, tx, "INSERT dbo.Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Discount,Pure,SB_ID,NeedRepost,S_Bills_Type,Table_ID,TB_ORDER_CODE,TB_isOrderd,isPied) VALUES(" & Id & ",1,GETDATE(),1,1,0,1,130,5,125," & Id & ",0,1," & Id & ",N'Source024',1,0), (" & (Id + 1) & ",1,GETDATE(),1,1,0,1,20,0,20," & (Id + 1) & ",0,1," & Id & ",N'Source024',1,1)")
                    Q(cn, tx, "INSERT dbo.SB_Contents(T_ID,Bill_T_ID,IM_ID,U_ID,ST_ID,[Date],Cost,Price,QTY,T_Price,U_Cargo,ST_QTY,isDepended,Saler_Percent,Notes,D_Vaild,IM_Discount,Barcode,Serial_Code,Compons) VALUES(" & Line & "," & Id & ",1,2,1,GETDATE(),6.528,55,2,110,1.2345,2.47,1,0,N'table fixture',NULL,0,N'barcode',N'serial',N'components text'),(" & (Line + 1) & "," & Id & ",1,2,1,GETDATE(),NULL,20,1,20,1,1,1,0,N'untouched',N'',NULL,NULL,NULL,NULL)")
                    Q(cn, tx, "UPDATE dbo.Sys_Features SET TB_Auto_Print=0 WHERE T_ID=(SELECT T_ID FROM dbo.Sys_Model)")
                    work(cn, tx)
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
    End Sub
    Private Sub Printing(cn As SqlConnection, tx As SqlTransaction, count As Integer)
        Q(cn, tx, "UPDATE dbo.Sys_Features SET TB_Auto_Print=1 WHERE T_ID=(SELECT T_ID FROM dbo.Sys_Model); INSERT dbo.Printers(Printer_ID,Printer_Name,Printer_IP) VALUES(" & Id & ",N'Not a physical printer',N'192.0.2.24')")
        For i = 1 To count
            Q(cn, tx, "INSERT dbo.GM_Printer(GM_ID,PTR_ID) SELECT GM_ID," & Id & " FROM dbo.IM_Menu WHERE IM_ID=1")
        Next
    End Sub
    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim builder As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS Tables024 rollback", .ConnectTimeout = 5, .Pooling = False}
        If builder.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database")
        connection = builder.ConnectionString
        context = New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "024", .ScreenCode = "TablesBalance"}
        Dim definitions As String(), stored As String(), domains As String, counts As Long()
        Dim objects = {"Transform_Tables", "SB_Contents_Change_TB_IM_Qty", "SB_Contents_Delete_TB_IM", "Recalculate_Agent_Bill_Total", "Network_Audit_Request_Begin", "Network_Audit_Event_Write"}
        Dim counted = {"Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change", "Network_Edit_Tracker"}
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other clients")
            context.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid)
            Check(context.SourceId = Guid.Parse("3c520084-0214-43d4-a5fb-797a179c7a7c"), "approved source")
            For Each pair In {"Tables|TB_ID", "Agents_Balance_MV|T_ID", "SB_Contents|T_ID", "Printers|Printer_ID"}
                Dim p = pair.Split("|"c)
                Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.[" & p(0) & "] WHERE " & p(1) & " BETWEEN " & Id & " AND " & (Id + 100))) = 0, "unused fixture " & p(0))
            Next
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditTableFail024')") Is DBNull.Value, "failure trigger unused")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('Tables'),OBJECT_ID('Agents_Balance_MV'),OBJECT_ID('SB_Contents'),OBJECT_ID('TB_AutoPrint_tmp'))")) = 0, "no unknown business triggers")
            definitions = objects.Select(Function(x) CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & x & "'))"))).ToArray()
            stored = VerifySql.Select(Function(x) Finger(cn, Nothing, x)).ToArray()
            counts = counted.Select(Function(x) CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & x))).ToArray()
            domains = CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1"))
        End Using
        For Each mode In {"plus", "minus", "delete"}
            Dim current = mode
            Fixture(Sub(cn, tx)
                        Dim r = Request(cn, tx, current), stale = Request(cn, tx, current)
                        Dim stock = Finger(cn, tx, VerifySql(4)), receipts = Finger(cn, tx, VerifySql(5)), treasury = Finger(cn, tx, VerifySql(6))
                        AuditTables.ExecuteInTransaction(cn, tx, r)
                        Dim total = If(current = "delete", 20D, If(current = "minus", 75D, 185D))
                        Check(CDec(Q(cn, tx, "SELECT Total FROM dbo.Agents_Balance_MV WHERE T_ID=" & Id)) = total, "recalculated total " & current)
                        Check(CDec(Q(cn, tx, "SELECT Pure FROM dbo.Agents_Balance_MV WHERE T_ID=" & Id)) = total - 5D, "net after discount " & current)
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.SB_Contents WHERE T_ID=" & Line)) = If(current = "delete", 0, 1), "line state " & current)
                        If current <> "delete" Then Check(CDec(Q(cn, tx, "SELECT ST_QTY FROM dbo.SB_Contents WHERE T_ID=" & Line)) = If(current = "minus", 1.23D, 3.7D), "SQL cargo rounding " & current)
                        Check(Finger(cn, tx, VerifySql(4)) = stock AndAlso Finger(cn, tx, VerifySql(5)) = receipts AndAlso Finger(cn, tx, VerifySql(6)) = treasury, "existing stock and payment semantics " & current)
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "'")) = 39, "39 fields " & current)
                        Check(AuditTables.ExecuteInTransaction(cn, tx, r).Status = "Completed", "idempotent " & current)
                        Reject(Sub() AuditTables.ExecuteInTransaction(cn, tx, stale), "stale " & current, "تغير")
                        r.Inputs.Single(Function(x) x.Name = "DisplayedQuantity").Value = AuditValue.Capture("Decimal", 99D)
                        Reject(Sub() AuditTables.ExecuteInTransaction(cn, tx, r), "request identity payload mismatch " & current)
                    End Sub)
        Next
        For Each occupied In {False, True}
            Dim targetOccupied = occupied
            Fixture(Sub(cn, tx)
                        If targetOccupied Then Q(cn, tx, "UPDATE dbo.[Tables] SET TB_ORDER_CODE=N'Target024',isbusy=1,Start_Open='20260901' WHERE TB_ID=" & (Id + 1))
                        Dim r = Request(cn, tx, "transfer"), lines = Finger(cn, tx, VerifySql(2))
                        AuditTables.ExecuteInTransaction(cn, tx, r)
                        Check(CInt(Q(cn, tx, "SELECT Table_ID FROM dbo.Agents_Balance_MV WHERE T_ID=" & Id)) = Id + 1, "invoice moved " & targetOccupied)
                        Check(CInt(Q(cn, tx, "SELECT Table_ID FROM dbo.Agents_Balance_MV WHERE T_ID=" & (Id + 1))) = Id, "paid invoice stays " & targetOccupied)
                        Check(CStr(Q(cn, tx, "SELECT TB_ORDER_CODE FROM dbo.Agents_Balance_MV WHERE T_ID=" & Id)) = If(targetOccupied, "Target024", "Source024"), "destination order code " & targetOccupied)
                        Check(Not CBool(Q(cn, tx, "SELECT isbusy FROM dbo.[Tables] WHERE TB_ID=" & Id)) AndAlso CBool(Q(cn, tx, "SELECT isbusy FROM dbo.[Tables] WHERE TB_ID=" & (Id + 1))), "table busy states " & targetOccupied)
                        Check(Q(cn, tx, "SELECT Start_Open FROM dbo.[Tables] WHERE TB_ID=" & Id) Is DBNull.Value, "source open date cleared")
                        Check(Finger(cn, tx, VerifySql(2)) = lines, "transfer never rewrites item rows")
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "'")) = 33, "two table rows plus invoice")
                        Check(AuditTables.ExecuteInTransaction(cn, tx, r).Status = "Completed", "transfer replay")
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    For n = 2 To 7
                        Q(cn, tx, "INSERT dbo.Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Discount,Pure,SB_ID,NeedRepost,S_Bills_Type,Table_ID,TB_ORDER_CODE,TB_isOrderd,isPied) VALUES(" & (Id + n) & ",1,GETDATE(),1,1,0,1,0,0,0," & (Id + n) & ",0,1," & Id & ",N'Source024',1,0)")
                    Next
                    Dim r = Request(cn, tx, "transfer")
                    AuditTables.ExecuteInTransaction(cn, tx, r)
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE Table_ID=" & (Id + 1))) = 7, "all seven invoices moved")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) = 3, "invoice snapshots paged")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "'")) = 123, "all seven invoices captured")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim s = AuditTables.Capture(cn, tx, "Table.LineQuantityChanged", Id, 0), cmd = Command("plus")
                    context.ScreenCode = "TB_BillIM"
                    Reject(Sub() AuditTables.Request(context, cmd, s, 1D), "displayed quantity mismatch")
                    Q(cn, tx, "UPDATE dbo.SB_Contents SET QTY=1 WHERE T_ID=" & Line)
                    s = AuditTables.Capture(cn, tx, "Table.LineQuantityChanged", Id, 0)
                    Reject(Sub() AuditTables.Request(context, Command("minus"), s, 1D), "minimum decrement guard")
                End Sub)
        For Each mode In {"plus", "delete"}
            Dim current = mode
            Fixture(Sub(cn, tx)
                        Printing(cn, tx, 27)
                        Q(cn, tx, "INSERT dbo.TB_AutoPrint_tmp(T_ID,[DATE],Notes,Printer_IP) VALUES(0,'20260101',N'existing',N'192.0.2.24'),(0,'20260101',N'existing',N'192.0.2.24')")
                        Dim before = CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.TB_AutoPrint_tmp")), r = Request(cn, tx, current)
                        AuditTables.ExecuteInTransaction(cn, tx, r)
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.TB_AutoPrint_tmp")) = before + 27, "duplicate print rows preserved " & current)
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) = 3, "print rows paged " & current)
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND c.EntityType='TablePrintRow' AND BeforeState=1 AND AfterState IN(2,3)")) = 108, "print payload captured " & current)
                        AuditTables.ExecuteInTransaction(cn, tx, r)
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.TB_AutoPrint_tmp")) = before + 27, "retry does not requeue " & current)
                    End Sub)
        Next
        For Each testMode In {"disabled", "permission", "domain", "wrong-screen", "paid", "void", "wrong-line", "stale-line", "stale-header", "same-table", "zero-delta"}
            Dim mode = testMode
            Fixture(Sub(cn, tx)
                        Dim transfer = mode = "permission" OrElse mode = "same-table", r = Request(cn, tx, If(transfer, "transfer", "plus"))
                        Select Case mode
                            Case "disabled" : Q(cn, tx, "UPDATE dbo.Users SET is_Allow=0 WHERE User_ID=1")
                            Case "permission" : Q(cn, tx, "UPDATE dbo.Users_Validations SET Transfer_Table=0 WHERE T_ID=(SELECT Valid_ID FROM dbo.Users WHERE User_ID=1)")
                            Case "domain" : Q(cn, tx, "UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code=""Sales.Tables""]') WHERE ConfigId=1")
                            Case "wrong-screen" : r.Context.ScreenCode = "POS"
                            Case "paid" : Q(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isPied=1 WHERE T_ID=" & Id)
                            Case "void" : Q(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isVoid=1 WHERE T_ID=" & Id)
                            Case "wrong-line" : r.Inputs.Single(Function(x) x.Name = "TargetLineId").Value = AuditValue.Capture("Integer", Line + 100)
                            Case "stale-line" : Q(cn, tx, "UPDATE dbo.SB_Contents SET QTY=7 WHERE T_ID=" & (Line + 1))
                            Case "stale-header" : Q(cn, tx, "UPDATE dbo.Agents_Balance_MV SET Discount=8 WHERE T_ID=" & Id)
                            Case "same-table" : r.Inputs.Single(Function(x) x.Name = "DestinationTableId").Value = AuditValue.Capture("Integer", Id)
                            Case "zero-delta" : r.Inputs.Single(Function(x) x.Name = "QuantityDelta").Value = AuditValue.Capture("Integer", 0)
                        End Select
                        Reject(Sub() AuditTables.ExecuteInTransaction(cn, tx, r), "reject " & mode)
                    End Sub)
        Next
        For Each mode In {"transfer", "plus", "delete"}
            Dim current = mode
            Fixture(Sub(cn, tx)
                        If current <> "transfer" Then Printing(cn, tx, 1)
                        Dim r = Request(cn, tx, current)
                        Q(cn, tx, "CREATE TRIGGER dbo.AuditTableFail024 ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN IF EXISTS(SELECT 1 FROM inserted WHERE EventCode LIKE 'Table.%' AND EventOrdinal=2) THROW 51124,'forced table detail failure',1; END")
                        Reject(Sub() AuditTables.ExecuteInTransaction(cn, tx, r), "detail failure rolls back " & current, "forced table detail failure")
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, "transfer")
                    Q(cn, tx, "CREATE TRIGGER dbo.AuditTableFail024 ON dbo.Agents_Balance_MV AFTER UPDATE AS BEGIN THROW 51124,'forced transfer failure',1; END")
                    Reject(Sub() AuditTables.ExecuteInTransaction(cn, tx, r), "swallowed legacy rollback never reports success")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, "plus")
                    Using other As New SqlConnection(connection)
                        other.Open()
                        Reject(Sub() Q(other, Nothing, "SET LOCK_TIMEOUT 800; UPDATE dbo.SB_Contents SET QTY=9 WHERE T_ID=" & Line), "snapshot prevents competing edit", "Lock request time out")
                    End Using
                    AuditTables.ExecuteInTransaction(cn, tx, r)
                    Check(CDec(Q(cn, tx, "SELECT QTY FROM dbo.SB_Contents WHERE T_ID=" & Line)) = 3D, "locked quantity applied once")
                End Sub)
        Using cn As New SqlConnection(connection)
            cn.Open()
            For i = 0 To VerifySql.Length - 1
                Check(Finger(cn, Nothing, VerifySql(i)) = stored(i), "original data unchanged " & i)
            Next
            For i = 0 To objects.Length - 1
                Check(CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & objects(i) & "'))")) = definitions(i), "definition unchanged " & objects(i))
            Next
            For i = 0 To counted.Length - 1
                Check(CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & counted(i))) = counts(i), "rollback count " & counted(i))
            Next
            Check(CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = domains, "domain rollback")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.Network_Audit_Request WHERE State<>1")) = 0, "no incomplete request")
            Check(Q(cn, Nothing, "SELECT OBJECT_ID('dbo.AuditTableFail024')") Is DBNull.Value, "failure trigger removed")
        End Using
    End Sub
End Module
