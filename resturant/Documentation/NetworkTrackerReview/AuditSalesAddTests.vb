Option Strict On
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq

Friend Module AuditSalesAddTests
    Private checks As Integer
    Private connection As String
    Private candidate As String
    Private context As AuditEventContext
    Private Const Bill As Integer = 2147482500
    Private Const Recipe As Integer = 2147482510

    Public Function Main(args As String()) As Integer
        Try
            Check(AuditContract.Domain(AuditSalesAdd.Operation) = "Sales.LineLifecycle", "closed addition domain")
            Check(AuditContract.SalesAddInputs().Length = 19, "explicit input catalogue")
            Reject(Sub() AuditContract.FieldType(AuditSalesAdd.Operation, "Anything"), "unknown field", "الحقل")
            If args.Length >= 2 Then
                If args.Length = 4 Then candidate = File.ReadAllText(args(3))
                Live(args(1))
            End If
            Console.WriteLine("PASS: " & checks & " sales addition checks.") : Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString()) : Return 1
        End Try
    End Function

    Private Sub Check(value As Boolean, label As String)
        If Not value Then Throw New Exception("FAILED: " & label)
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
        Using cmd As New SqlCommand(sql, cn, tx) With {.CommandTimeout = 20}
            Return cmd.ExecuteScalar()
        End Using
    End Function

    Private Function Financial(cn As SqlConnection, tx As SqlTransaction) As String
        Return CStr(Q(cn, tx, "SELECT CONVERT(nvarchar(max),(SELECT (SELECT * FROM dbo.Agents_Balance_MV_RCT ORDER BY T_ID FOR XML PATH('Receipt'),TYPE),(SELECT * FROM dbo.Treasury_Balance_MV ORDER BY T_ID FOR XML PATH('Movement'),TYPE) FOR XML PATH('State'),TYPE))"))
    End Function

    Private Sub Fixture(work As Action(Of SqlConnection, SqlTransaction))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    If candidate IsNot Nothing Then Q(cn, tx, candidate)
                    Q(cn, tx, "INSERT dbo.Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Discount,Pure,SB_ID,NeedRepost,S_Bills_Type) VALUES(" & Bill & ",1,GETDATE(),1,0,0,1,0,0,0," & Bill & ",0,1)")
                    context.ScreenCode = "Sales_Fast"
                    work(cn, tx)
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
    End Sub

    Private Function Command(Optional quantity As Double = 2R, Optional merge As Boolean = False, Optional edit As Boolean = False) As SqlCommand
        Dim pos = context.ScreenCode = "POS"
        Dim cmd As New SqlCommand(If(pos, "SB_Contents_INSERT", "SB_Contents_INSERT_2"))
        cmd.Parameters.AddWithValue("@SB_T_ID", Bill) : cmd.Parameters.AddWithValue("@IM_ID", 1)
        cmd.Parameters.AddWithValue("@ST_ID", 1) : cmd.Parameters.AddWithValue("@On_Update", edit)
        cmd.Parameters.AddWithValue("@QYT", quantity) : cmd.Parameters.AddWithValue("@Barcode", "Audit4F")
        If pos Then
            cmd.Parameters.AddWithValue("@U_IM_ID", 1) : cmd.Parameters.AddWithValue("@IM_Price", 55R)
        Else
            cmd.Parameters.AddWithValue("@U_ID", 2) : cmd.Parameters.AddWithValue("@Price", 55R)
            cmd.Parameters.AddWithValue("@SB_IM_NEW_ROW", Not merge)
        End If
        Return cmd
    End Function

    Private Function Request(cn As SqlConnection, tx As SqlTransaction, Optional quantity As Double = 2R, Optional merge As Boolean = False, Optional edit As Boolean = False, Optional allowNegative As Boolean = True) As AuditOperationRequest
        Using cmd = Command(quantity, merge, edit)
            Return AuditSalesAdd.Request(context, AuditSalesLineEdit.Header(cn, tx, Bill), cmd, allowNegative)
        End Using
    End Function

    Private Function LineId(result As AuditRequestResult) As Integer
        Return Integer.Parse(AuditCanonical.ParseXml(result.ResultXml).Element("Values").Elements("Value").Single(Function(x) CStr(x.Attribute("name")) = "LineId").Value)
    End Function

    Private Function Count(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, Optional filter As String = "") As Integer
        Return CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' " & filter))
    End Function

    Private Sub AddRecipe(cn As SqlConnection, tx As SqlTransaction, Optional number As Integer = 1)
        For i = 0 To number - 1
            Q(cn, tx, "INSERT dbo.IM_Struct_Menu(T_ID,IM_ID,U_IM_ID,Struct_IM_ID,U_ID,Qty,Row_Enabled,User_ID) VALUES(" & (Recipe + i) & ",1,1,2,2,2,1,1)")
        Next
    End Sub

    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim b As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS Audit4F rollback", .ConnectTimeout = 5, .Pooling = False}
        If b.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database")
        connection = b.ConnectionString
        context = New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "4F", .ScreenCode = "Sales_Fast"}
        Dim tables = {"Agents_Balance_MV", "SB_Contents", "SB_Contents_Structers", "IM_Struct_Menu", "SB_IM_Components", "SB_Rsv", "SB_Rsv_2", "SB_VALUE_Bercent_Details", "Agents_Balance_MV_RCT", "Treasury_Balance_MV", "Network_Edit_Tracker", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change"}
        Dim procs = {"Network_Audit_Request_Begin", "Network_Audit_Event_Write", "SB_Contents_INSERT", "SB_Contents_INSERT_2", "SB_Contents_Structers_insert_ONE", "AG_Balance_DependingUpdatedBill", "AG_Balance_Update_Total"}
        Dim counts As Long(), definitions As String(), financialState As String
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other clients")
            context.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid)
            Check(context.SourceId = Guid.Parse("3c520084-0214-43d4-a5fb-797a179c7a7c"), "approved source")
            Check(CInt(Q(cn, Nothing, "SELECT (SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=" & Bill & ")+(SELECT COUNT(*) FROM dbo.IM_Struct_Menu WHERE T_ID BETWEEN " & Recipe & " AND " & (Recipe + 30) & ")")) = 0, "fixture identities unused")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(" & String.Join(",", tables.Select(Function(x) "OBJECT_ID('dbo." & x & "')")) & ")")) = 0, "no unreviewed triggers")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.IM_Struct_Menu_V WHERE IM_ID=1 AND IM_U_ID=2")) = 0, "isolated recipe baseline")
            counts = tables.Select(Function(x) CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & x))).ToArray()
            definitions = procs.Select(Function(x) CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & x & "'))"))).ToArray()
            financialState = Financial(cn, Nothing)
        End Using
        For Each screen In {"POS", "Sales_Fast", "SB_IM_card"}
            Fixture(Sub(cn, tx)
                        context.ScreenCode = screen : AddRecipe(cn, tx)
                        Dim r = Request(cn, tx), saved = AuditSalesAdd.ExecuteInTransaction(cn, tx, r), line = LineId(saved)
                        Check(saved.Status = "NewPendingCommit", "atomic add " & screen)
                        Check(CDec(Q(cn, tx, "SELECT QTY FROM dbo.SB_Contents WHERE T_ID=" & line)) = 2D, "actual line " & screen)
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=" & line & " AND QTY=4")) = 1, "component persisted " & screen)
                        Check(Count(cn, tx, r, "AND c.EntityType='SalesLine' AND c.BeforeState=1 AND c.AfterState IN(2,3)") = 24, "full new row states " & screen)
                        Check(Count(cn, tx, r) = 50, "line header component stock captured " & screen)
                        Check(LineId(AuditSalesAdd.ExecuteInTransaction(cn, tx, r)) = line AndAlso Count(cn, tx, r) = 50, "idempotent replay " & screen)
                        Check(Financial(cn, tx) = financialState, "receipts unchanged " & screen)
                        If screen <> "POS" Then
                            Dim merge = Request(cn, tx, 3, True), merged = AuditSalesAdd.ExecuteInTransaction(cn, tx, merge)
                            Check(LineId(merged) = line, "merge returns actual existing line " & screen)
                            Check(CDec(Q(cn, tx, "SELECT QTY FROM dbo.SB_Contents WHERE T_ID=" & line)) = 5D, "merge quantity " & screen)
                            Check(Count(cn, tx, merge, "AND c.EntityType='SalesLine' AND c.BeforeState IN(2,3)") = 24, "merge previous values " & screen)
                            Check(Count(cn, tx, merge, "AND c.EntityType='SalesStructure'") = 20, "replaced components before after " & screen)
                        End If
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    context.ScreenCode = "Sales"
                    Q(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isDepended=1 WHERE T_ID=" & Bill)
                    AuditSalesEdit.ExecuteInTransaction(cn, tx, AuditSalesEdit.Request(context, Bill))
                    context.ScreenCode = "SB_IM_card"
                    Dim r = Request(cn, tx, 2, False, True)
                    AuditSalesAdd.ExecuteInTransaction(cn, tx, r)
                    Check(CDec(Q(cn, tx, "SELECT Pure FROM dbo.Agents_Balance_MV WHERE T_ID=" & Bill)) = 110D, "edit recalculates correct header in same transaction")
                    Check(Financial(cn, tx) = financialState, "edit preserves payment behavior")
                End Sub)
        Fixture(Sub(cn, tx)
                    AddRecipe(cn, tx)
                    Q(cn, tx, "UPDATE dbo.Agents_Balance_MV SET S_Bills_Type=3,Order_isDeleverd=0 WHERE T_ID=" & Bill)
                    Dim r = Request(cn, tx, 2, False, False, False), line = LineId(AuditSalesAdd.ExecuteInTransaction(cn, tx, r))
                    Check(CInt(Q(cn, tx, "SELECT isDepended FROM dbo.SB_Contents WHERE T_ID=" & line)) = 0, "undelivered order stays inactive")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=" & line)) = 0, "undelivered order consumes no components")
                End Sub)
        Fixture(Sub(cn, tx)
                    AddRecipe(cn, tx, 11)
                    Dim r = Request(cn, tx) : AuditSalesAdd.ExecuteInTransaction(cn, tx, r)
                    Check(Count(cn, tx, r) = 150, "large recipe fully captured")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) = 3, "large recipe split without truncation")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx), line = LineId(AuditSalesAdd.ExecuteInTransaction(cn, tx, r))
                    Dim index = r.Inputs.FindIndex(Function(x) x.Name = "RequestedQuantity")
                    r.Inputs(index) = New AuditInput With {.Name = "RequestedQuantity", .ValueType = "Float", .Value = AuditValue.Capture("Float", 3R)}
                    Reject(Sub() AuditSalesAdd.ExecuteInTransaction(cn, tx, r), "same identity different quantity", "conflicts with different content")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim line = LineId(AuditSalesAdd.ExecuteInTransaction(cn, tx, Request(cn, tx, 0)))
                    Check(CDec(Q(cn, tx, "SELECT QTY FROM dbo.SB_Contents WHERE T_ID=" & line)) = 1D AndAlso CDec(Q(cn, tx, "SELECT ST_QTY FROM dbo.SB_Contents WHERE T_ID=" & line)) = 0D, "legacy zero-quantity normalization not silently changed")
                End Sub)
        Fixture(Sub(cn, tx)
                    Using cmd = Command(1.25R)
                        cmd.Parameters.AddWithValue("@is_By_Min_SP", True)
                        cmd.Parameters.AddWithValue("@Notes", "test notes")
                        cmd.Parameters.AddWithValue("@Serial_Code", "serial-4F")
                        Dim r = AuditSalesAdd.Request(context, AuditSalesLineEdit.Header(cn, tx, Bill), cmd, True), line = LineId(AuditSalesAdd.ExecuteInTransaction(cn, tx, r))
                        Check(CDec(Q(cn, tx, "SELECT QTY FROM dbo.SB_Contents WHERE T_ID=" & line)) = 1.25D, "fractional input retained")
                        Check(CInt(Q(cn, tx, "SELECT is_By_Min_SP FROM dbo.SB_Contents WHERE T_ID=" & line)) = 1, "checkbox maps to SQL one not minus one")
                        Check(CStr(Q(cn, tx, "SELECT Serial_Code FROM dbo.SB_Contents WHERE T_ID=" & line)) = "serial-4F", "serial preserved")
                    End Using
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "INSERT dbo.SB_Contents(T_ID,Bill_T_ID,IM_ID,U_ID,ST_ID,[Date],Cost,Price,QTY,T_Price,U_Cargo,ST_QTY,isDepended,D_Vaild,Barcode) VALUES(" & (Bill + 1) & "," & Bill & ",1,2,1,GETDATE(),50,55,1000000,55000000,1,1000000,1,N'2099-12-27',N'fallback fixture')")
                    Using cmd = Command()
                        cmd.Parameters.AddWithValue("@D_Vaild", New DateTime(2098, 1, 1))
                        Dim r = AuditSalesAdd.Request(context, AuditSalesLineEdit.Header(cn, tx, Bill), cmd, True), line = LineId(AuditSalesAdd.ExecuteInTransaction(cn, tx, r))
                        Check(CStr(Q(cn, tx, "SELECT D_Vaild FROM dbo.SB_Contents WHERE T_ID=" & line)) = "2099-12-27", "actual substituted expiry captured")
                        Check(Count(cn, tx, r, "AND c.EntityType='StockBucket' AND c.FieldCode='ExpiryText' AND c.AfterValue=N'2099-12-27'") = 1, "stock audit uses actual expiry")
                    End Using
                End Sub)
        Fixture(Sub(cn, tx)
                    AuditSalesAdd.ExecuteInTransaction(cn, tx, Request(cn, tx))
                    Using other As New SqlConnection(connection)
                        other.Open()
                        Reject(Sub() Q(other, Nothing, "SET LOCK_TIMEOUT 250; DECLARE @s int,@e int; SELECT @s=START_ID,@e=END_ID FROM dbo.SERVERS; SELECT MAX(T_ID) FROM dbo.SB_Contents WITH(UPDLOCK,HOLDLOCK) WHERE T_ID BETWEEN @s AND @e;"), "concurrent allocator cannot reuse pending identity", "Lock request time out")
                    End Using
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "UPDATE dbo.Users SET is_Allow=0 WHERE User_ID=1")
                    Reject(Sub() AuditSalesAdd.ExecuteInTransaction(cn, tx, Request(cn, tx)), "disabled actor", "صلاحية المبيعات")
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isDepended=1 WHERE T_ID=" & Bill)
                    Reject(Sub() AuditSalesAdd.ExecuteInTransaction(cn, tx, Request(cn, tx)), "approved invoice must be opened for editing", "فتح تعديل")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx)
                    Q(cn, tx, "UPDATE dbo.Agents_Balance_MV SET Discount=1 WHERE T_ID=" & Bill)
                    Reject(Sub() AuditSalesAdd.ExecuteInTransaction(cn, tx, r), "stale header", "تغيرت الفاتورة")
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isVoid=1 WHERE T_ID=" & Bill)
                    Reject(Sub() AuditSalesAdd.ExecuteInTransaction(cn, tx, Request(cn, tx)), "void invoice", "ملغاة")
                End Sub)
        Fixture(Sub(cn, tx)
                    Using cmd = Command()
                        cmd.Parameters("@U_ID").Value = 2147483000
                        Dim r = AuditSalesAdd.Request(context, AuditSalesLineEdit.Header(cn, tx, Bill), cmd, True)
                        Reject(Sub() AuditSalesAdd.ExecuteInTransaction(cn, tx, r), "unit belongs to item", "الوحدة")
                    End Using
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx, 1000000R, False, False, False)
                    Reject(Sub() AuditSalesAdd.ExecuteInTransaction(cn, tx, r), "negative stock rolls back", "الرصيد الحالي")
                End Sub)
        Fixture(Sub(cn, tx)
                    Q(cn, tx, "CREATE TRIGGER dbo.Audit4F_Fail ON dbo.Network_Audit_Event AFTER INSERT AS IF EXISTS(SELECT 1 FROM inserted WHERE EventCode='Sales.LineAdded' AND EventOrdinal=2) THROW 51117,'Injected addition audit failure',1;")
                    Reject(Sub() AuditSalesAdd.ExecuteInTransaction(cn, tx, Request(cn, tx)), "second event failure is not success", "Injected addition audit failure")
                End Sub)
        Using cn As New SqlConnection(connection)
            cn.Open()
            For i = 0 To tables.Length - 1
                Check(CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & tables(i))) = counts(i), "restored " & tables(i))
            Next
            For i = 0 To procs.Length - 1
                Check(CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & procs(i) & "'))")) = definitions(i), "definition preserved " & procs(i))
            Next
            Check(Financial(cn, Nothing) = financialState, "financial contents restored")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE name='Audit4F_Fail'")) = 0, "failure trigger rolled back")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.Network_Audit_Request WHERE State<>1")) = 0, "no incomplete requests")
        End Using
    End Sub
End Module
