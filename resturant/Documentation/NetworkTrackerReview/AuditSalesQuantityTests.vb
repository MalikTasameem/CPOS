Option Strict On
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks

Friend Module AuditSalesQuantityTests
    Private checks As Integer
    Private connection As String
    Private context As AuditEventContext
    Private Const Bill As Integer = 2147482100
    Private Const Line As Integer = 2147482101
    Private Const Recipe As Integer = 2147482102
    Private Const Seed As Integer = 2147482130
    Private Const Competitor As Integer = 2147482131

    Public Function Main(args As String()) As Integer
        Try
            Check(AuditContract.Domain(AuditSalesQuantity.Operation) = "Sales.LineQuantity", "separate quantity-only domain")
            Check(AuditContract.SalesQuantityFields("SalesStructure").Length = 10, "complete component row contract")
            Check(AuditContract.SalesQuantityFields("StockBucket").Length = 4, "stock key and quantity contract")
            Reject(Sub() AuditContract.FieldType(AuditSalesQuantity.Operation, "UnknownField"), "closed fields", "الحقل")
            If args.Length = 2 AndAlso args(0) = "--settings" Then Live(args(1))
            Console.WriteLine("PASS: " & checks & " quantity step checks.") : Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString()) : Return 1
        End Try
    End Function

    Private Sub Check(value As Boolean, name As String)
        If Not value Then Throw New Exception("FAILED: " & name)
        checks += 1 : Console.WriteLine("PASS " & name)
    End Sub

    Private Sub Reject(work As Action, name As String, expected As String)
        Try
            work()
        Catch ex As Exception
            If Not ex.Message.Contains(expected) Then Throw New Exception("Unexpected rejection: " & name, ex)
            Check(True, name) : Return
        End Try
        Throw New Exception("Expected rejection: " & name)
    End Sub

    Private Function Scalar(cn As SqlConnection, tx As SqlTransaction, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn, tx)
            Return cmd.ExecuteScalar()
        End Using
    End Function

    Private Function FinancialState(cn As SqlConnection, tx As SqlTransaction) As String
        Return CStr(Scalar(cn, tx, "SELECT CONVERT(nvarchar(max),(SELECT (SELECT * FROM dbo.Agents_Balance_MV_RCT ORDER BY T_ID FOR XML PATH('Receipt'),TYPE),(SELECT * FROM dbo.Treasury_Balance_MV ORDER BY T_ID FOR XML PATH('Movement'),TYPE) FOR XML PATH('FinancialState'),TYPE))"))
    End Function

    Private Sub Fixture(work As Action(Of SqlConnection, SqlTransaction))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Scalar(cn, tx, "INSERT dbo.Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Discount,Pure,SB_ID,NeedRepost,S_Bills_Type) VALUES(" & Bill & ",1,GETDATE(),1,0,0,1,110,5,105," & Bill & ",0,1)")
                    Scalar(cn, tx, "INSERT dbo.SB_Contents(T_ID,Bill_T_ID,IM_ID,U_ID,ST_ID,[Date],Cost,Price,QTY,T_Price,U_Cargo,ST_QTY,isDepended,Saler_Percent,Notes,D_Vaild,IM_Discount) VALUES(" & Line & "," & Bill & ",1,2,1,GETDATE(),50,55,2,110,1,2,1,0,N'quantity fixture',N'2099-12-27',0)")
                    context.ScreenCode = "Sales"
                    work(cn, tx)
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
    End Sub

    Private Function Snapshot(cn As SqlConnection, tx As SqlTransaction, Optional edit As Boolean = False) As AuditSalesQuantitySnapshot
        Return AuditSalesQuantity.ReadInTransaction(cn, tx, context, Bill, Line, edit)
    End Function

    Private Function StepRequest(cn As SqlConnection, tx As SqlTransaction, delta As Integer, Optional edit As Boolean = False, Optional negative As Boolean = True) As AuditOperationRequest
        Dim old = Snapshot(cn, tx, edit)
        Return AuditSalesQuantity.Request(context, old, delta, CDec(old.Sale.Line("QTY")), edit, negative)
    End Function

    Private Sub OpenEdit(cn As SqlConnection, tx As SqlTransaction)
        Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isDepended=1 WHERE T_ID=" & Bill)
        AuditSalesEdit.ExecuteInTransaction(cn, tx, AuditSalesEdit.Request(context, Bill))
    End Sub

    Private Sub AddRecipe(cn As SqlConnection, tx As SqlTransaction, Optional count As Integer = 1, Optional quantity As Integer = 2)
        For i = 0 To count - 1
            Scalar(cn, tx, "INSERT dbo.IM_Struct_Menu(T_ID,IM_ID,U_IM_ID,Struct_IM_ID,U_ID,Qty,Row_Enabled,User_ID) VALUES(" & (Recipe + i) & ",1,1,2,2," & quantity & ",1,1)")
        Next
    End Sub

    Private Function Events(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As Integer
        Return CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & request.RequestId.ToString() & "'"))
    End Function

    Private Function CompetingSaleIsBlocked(item As Integer, expiry As String) As Boolean
        Return Task.Run(Function() As Boolean
                            Using cn As New SqlConnection(connection)
                                cn.Open()
                                Using tx = cn.BeginTransaction()
                                    Try
                                        Using cmd As New SqlCommand("SET LOCK_TIMEOUT 800; INSERT dbo.SB_Contents(T_ID,Bill_T_ID,IM_ID,U_ID,ST_ID,[Date],Price,D_Vaild,QTY,T_Price,U_Cargo,ST_QTY,isDepended) VALUES(@Id,4,@Item,2,1,GETDATE(),1,@Expiry,1,1,1,1,1)", cn, tx)
                                            cmd.CommandTimeout = 5
                                            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Competitor
                                            cmd.Parameters.Add("@Item", SqlDbType.Int).Value = item
                                            cmd.Parameters.Add("@Expiry", SqlDbType.NVarChar, 50).Value = expiry
                                            cmd.ExecuteNonQuery()
                                        End Using
                                        Return False
                                    Catch ex As SqlException When ex.Number = 1222
                                        Return True
                                    Finally
                                        If tx.Connection IsNot Nothing Then tx.Rollback()
                                    End Try
                                End Using
                            End Using
                        End Function).GetAwaiter().GetResult()
    End Function

    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim b As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS Audit4D rollback", .ConnectTimeout = 5}
        If b.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database.")
        connection = b.ConnectionString
        context = New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "4D", .ScreenCode = "Sales"}
        Dim tables = {"Agents_Balance_MV", "SB_Contents", "SB_Contents_Structers", "IM_Struct_Menu", "ST_QTY_Deferences", "Agents_Balance_MV_RCT", "Treasury_Balance_MV", "Network_Edit_Tracker", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change"}
        Dim procedures = {"SB_Contents_Change_IM_Qty", "SB_Contents_Structers_Recount_One", "SB_Contents_Structers_Remove_ONE", "SB_Contents_Structers_insert_ONE", "AG_Balance_Update_Total", "AG_Balance_Update_Pied_On_Update", "Recalculate_Agent_Bill_Total", "Inc_IM_QTY", "Dec_IM_QTY"}
        Dim before As Long(), definitions As String(), domains As String, financialBefore As String
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND database_id=DB_ID() AND session_id<>@@SPID")) = 0, "no other database clients")
            Check(CInt(Scalar(cn, Nothing, "SELECT (SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=" & Bill & ")+(SELECT COUNT(*) FROM dbo.SB_Contents WHERE T_ID IN(" & Line & "," & Competitor & "))+(SELECT COUNT(*) FROM dbo.IM_Struct_Menu WHERE T_ID BETWEEN " & Recipe & " AND " & (Recipe + 11) & ")+(SELECT COUNT(*) FROM dbo.ST_QTY_Deferences WHERE T_ID=" & Seed & ")")) = 0, "fixture identifiers unused")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('dbo.Agents_Balance_MV'),OBJECT_ID('dbo.SB_Contents'),OBJECT_ID('dbo.SB_Contents_Structers'),OBJECT_ID('dbo.IM_Struct_Menu'),OBJECT_ID('dbo.ST_QTY_Deferences'),OBJECT_ID('dbo.Users'),OBJECT_ID('dbo.Users_Validations'),OBJECT_ID('dbo.Network_Audit_Event'))")) = 0, "no unreviewed test triggers")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Users u JOIN dbo.Users_Validations_V v ON v.User_ID=u.User_ID WHERE u.User_ID=1 AND u.Valid_ID=1 AND u.is_Allow=1 AND v.Sales=1 AND v.SB_Update=1")) = 1, "actor and actual permissions verified")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.IM_Menu_Units_V WHERE IM_ID=1 AND U_ID=2 AND U_IM_ID=1 AND U_Cargo=1")) = 1 AndAlso CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.IM_Menu WHERE IM_ID IN(1,2) AND isStore=1")) = 2, "stock items and unit references verified")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.IM_Struct_Menu WHERE IM_ID=1")) = 0, "fixture recipe initially empty")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=4 AND BsType_ID=1 AND isVoid=0")) = 1, "competitor parent verified")
            context.SourceId = CType(Scalar(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid)
            domains = CStr(Scalar(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1"))
            before = tables.Select(Function(t) CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & t))).ToArray()
            definitions = procedures.Select(Function(p) CStr(Scalar(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & p & "'))"))).ToArray()
            financialBefore = FinancialState(cn, Nothing)
        End Using
        For Each screen In {"Sales", "Sales_Fast", "POS"}
            Fixture(Sub(cn, tx)
                        context.ScreenCode = screen
                        Dim r = StepRequest(cn, tx, 1)
                        Check(AuditSalesQuantity.ExecuteInTransaction(cn, tx, r).Status = "NewPendingCommit", "atomic step " & screen)
                        Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.SB_Contents WHERE T_ID=" & Line & " AND QTY=3 AND ST_QTY=3 AND T_Price=165")) = 1, "original quantity calculation " & screen)
                        Check(CDec(Scalar(cn, tx, "SELECT Pure FROM dbo.Agents_Balance_MV WHERE T_ID=" & Bill)) = 105D, "unapproved stored header behavior preserved " & screen)
                        Check(Events(cn, tx, r) = 2, "main event and stock detail part " & screen)
                        Check(AuditSalesQuantity.ExecuteInTransaction(cn, tx, r).Status = "Completed" AndAlso Events(cn, tx, r) = 2, "same request never increments twice " & screen)
                        Check(FinancialState(cn, tx) = financialBefore, "receipts and treasury unchanged " & screen)
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    AuditSalesQuantity.ExecuteInTransaction(cn, tx, StepRequest(cn, tx, -1))
                    Check(CDec(Scalar(cn, tx, "SELECT QTY FROM dbo.SB_Contents WHERE T_ID=" & Line)) = 1D, "minus one")
                    Dim r = StepRequest(cn, tx, -1)
                    Check(AuditSalesQuantity.ExecuteInTransaction(cn, tx, r).ResultCode.GetValueOrDefault() = 5 AndAlso Events(cn, tx, r) = 0, "boundary NoChange without event")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim old = Snapshot(cn, tx)
                    Reject(Sub() AuditSalesQuantity.Request(context, old, 1, 1D, False, True), "stale displayed quantity rejected", "تغيرت الكمية")
                    Reject(Sub() AuditSalesQuantity.Request(context, old, 0, 2D, False, True), "zero quantity difference rejected", "غير صفري")
                    context.ScreenCode = "TB_BillIM"
                    Reject(Sub() AuditSalesQuantity.Request(context, old, 1, 2D, False, True), "table route cannot use wrong procedure", "الشاشة")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = StepRequest(cn, tx, 3)
                    AuditSalesQuantity.ExecuteInTransaction(cn, tx, r)
                    Check(CDec(Scalar(cn, tx, "SELECT QTY FROM dbo.SB_Contents WHERE T_ID=" & Line)) = 5D, "multi-unit increase is accepted atomically")
                    Check(Events(cn, tx, r) = 2, "multi-unit increase is fully audited")
                End Sub)
        Fixture(Sub(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.SB_Contents SET QTY=5,ST_QTY=5,T_Price=275 WHERE T_ID=" & Line)
                    Dim r = StepRequest(cn, tx, -3)
                    AuditSalesQuantity.ExecuteInTransaction(cn, tx, r)
                    Check(CDec(Scalar(cn, tx, "SELECT QTY FROM dbo.SB_Contents WHERE T_ID=" & Line)) = 2D, "multi-unit decrease is accepted atomically")
                    Check(Events(cn, tx, r) = 2, "multi-unit decrease is fully audited")
                End Sub)
        Fixture(Sub(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.SB_Contents SET QTY=1.5,ST_QTY=1.5,T_Price=82.5 WHERE T_ID=" & Line)
                    Dim r = StepRequest(cn, tx, -1)
                    Check(AuditSalesQuantity.ExecuteInTransaction(cn, tx, r).ResultCode.GetValueOrDefault() = 5 AndAlso CDec(Scalar(cn, tx, "SELECT QTY FROM dbo.SB_Contents WHERE T_ID=" & Line)) = 1.5D, "fractional lower boundary stays unchanged")
                    Check(AuditSalesQuantity.ExecuteInTransaction(cn, tx, r).Status = "Completed" AndAlso Events(cn, tx, r) = 0, "NoChange replay creates no event")
                End Sub)
        Fixture(Sub(cn, tx)
                    AddRecipe(cn, tx) : OpenEdit(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.SB_Contents SET QTY=1,ST_QTY=1,T_Price=55 WHERE T_ID=" & Line)
                    Scalar(cn, tx, "EXEC dbo.SB_Contents_Structers_insert_ONE " & Line)
                    Dim old = Snapshot(cn, tx, True), r = StepRequest(cn, tx, -1, True)
                    AuditSalesQuantity.ExecuteInTransaction(cn, tx, r)
                    Check(AuditSalesQuantity.Fingerprint(Snapshot(cn, tx, True).Structures) = AuditSalesQuantity.Fingerprint(old.Structures) AndAlso Events(cn, tx, r) = 0, "NoChange does not rebuild component identities")
                End Sub)
        Fixture(Sub(cn, tx)
                    AuditSalesQuantity.ExecuteInTransaction(cn, tx, StepRequest(cn, tx, -1, False, False))
                    Check(CDec(Scalar(cn, tx, "SELECT QTY FROM dbo.SB_Contents WHERE T_ID=" & Line)) = 1D, "reducing an existing stock deficit is allowed")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = StepRequest(cn, tx, 1)
                    AuditSalesQuantity.ExecuteInTransaction(cn, tx, r)
                    r.Inputs.Single(Function(x) x.Name = "QuantityDelta").Value = AuditValue.Capture("Integer", -1)
                    Reject(Sub() AuditSalesQuantity.ExecuteInTransaction(cn, tx, r), "same identity with different step rejected", "conflicts")
                End Sub)
        Fixture(Sub(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.SB_Contents SET IM_Discount=3,T_Price=107 WHERE T_ID=" & Line)
                    AuditSalesQuantity.ExecuteInTransaction(cn, tx, StepRequest(cn, tx, 1))
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.SB_Contents WHERE T_ID=" & Line & " AND IM_Discount=3 AND T_Price=165")) = 1, "legacy step discount calculation is unchanged, not silently repaired")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = StepRequest(cn, tx, 1)
                    Scalar(cn, tx, "UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code=""Sales.LineQuantity""]') WHERE ConfigId=1")
                    Reject(Sub() AuditSalesQuantity.ExecuteInTransaction(cn, tx, r), "disabled domain cannot fall back to legacy", "disabled")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = StepRequest(cn, tx, 1)
                    Reject(Sub() AuditContract.EventData(r, New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = 101}), "oversized event-part count rejected without truncation", "ترتيب")
                End Sub)
        For Each update In {"UPDATE dbo.SB_Contents SET Price=56 WHERE T_ID=" & Line, "UPDATE dbo.Agents_Balance_MV SET Discount=6 WHERE T_ID=" & Bill}
            Fixture(Sub(cn, tx)
                        Dim r = StepRequest(cn, tx, 1)
                        Scalar(cn, tx, update)
                        Reject(Sub() AuditSalesQuantity.ExecuteInTransaction(cn, tx, r), "stale line or header rejected", "تغير السطر")
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    AddRecipe(cn, tx)
                    OpenEdit(cn, tx)
                    Scalar(cn, tx, "EXEC dbo.SB_Contents_Structers_insert_ONE " & Line)
                    Dim oldId = CInt(Scalar(cn, tx, "SELECT T_ID FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=" & Line))
                    Dim r = StepRequest(cn, tx, 1, True)
                    AuditSalesQuantity.ExecuteInTransaction(cn, tx, r)
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=" & Line & " AND T_ID<>" & oldId & " AND QTY=6 AND ST_QTY=6")) = 1, "original component remove and rebuild captured")
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND c.EntityType='SalesStructure' AND (c.BeforeState=1 OR c.AfterState=1)")) = 20, "old and new component identities have ten fields each")
                    Check(CDec(Scalar(cn, tx, "SELECT Pure FROM dbo.Agents_Balance_MV WHERE T_ID=" & Bill)) = 160D, "approved invoice original total recalculation")
                    Check(FinancialState(cn, tx) = financialBefore, "approved step leaves payments unchanged")
                End Sub)
        Fixture(Sub(cn, tx)
                    AddRecipe(cn, tx, 12) : OpenEdit(cn, tx)
                    Scalar(cn, tx, "EXEC dbo.SB_Contents_Structers_insert_ONE " & Line)
                    Dim r = StepRequest(cn, tx, 1, True)
                    AuditSalesQuantity.ExecuteInTransaction(cn, tx, r)
                    Check(Events(cn, tx, r) = 4, "large component detail split into bounded parts")
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "'")) = 273, "all 273 values retained without truncation")
                End Sub)
        For Each changeRecipe In {True, False}
            Fixture(Sub(cn, tx)
                        AddRecipe(cn, tx) : OpenEdit(cn, tx)
                        Scalar(cn, tx, "EXEC dbo.SB_Contents_Structers_insert_ONE " & Line)
                        Dim r = StepRequest(cn, tx, 1, True)
                        Scalar(cn, tx, If(changeRecipe, "UPDATE dbo.IM_Struct_Menu SET Qty=3 WHERE T_ID=" & Recipe, "UPDATE dbo.SB_Contents_Structers SET Cost=12 WHERE SB_Cont_T_ID=" & Line))
                        Reject(Sub() AuditSalesQuantity.ExecuteInTransaction(cn, tx, r), "stale recipe or component rejected", "تغير السطر")
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    AddRecipe(cn, tx) : OpenEdit(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET S_Bills_Type=3 WHERE T_ID=" & Bill & "; UPDATE dbo.SB_Contents SET isDepended=0 WHERE T_ID=" & Line)
                    AuditSalesQuantity.ExecuteInTransaction(cn, tx, StepRequest(cn, tx, 1, True, False))
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=" & Line)) = 0, "order retains no component consumption")
                End Sub)
        Fixture(Sub(cn, tx)
                    Reject(Sub() AuditSalesQuantity.ExecuteInTransaction(cn, tx, StepRequest(cn, tx, 1, False, False)), "new main item negative stock rejected", "الرصيد الحالي")
                End Sub)
        Fixture(Sub(cn, tx)
                    AddRecipe(cn, tx, 1, 1000000) : OpenEdit(cn, tx)
                    Scalar(cn, tx, "INSERT dbo.ST_QTY_Deferences(T_ID,Bill_T_ID,[DATE],IM_ID,ST_ID,Valid_D,ST_QTY,Cost,U_Cargo,Barcode,TOTAL) VALUES(" & Seed & "," & Bill & ",GETDATE(),1,1,N'2099-12-27',100,1,1,N'Audit4D',100)")
                    Reject(Sub() AuditSalesQuantity.ExecuteInTransaction(cn, tx, StepRequest(cn, tx, 1, True, False)), "component negative stock rejected even when main stock sufficient", "الرصيد الحالي")
                End Sub)
        Fixture(Sub(cn, tx)
                    AddRecipe(cn, tx)
                    Check(Not CompetingSaleIsBlocked(2, ""), "control allows competing component sale")
                    AuditSalesQuantity.ExecuteInTransaction(cn, tx, StepRequest(cn, tx, 1))
                    Check(CompetingSaleIsBlocked(1, "2099-12-27"), "main stock locks block concurrent legacy sale")
                    Check(CompetingSaleIsBlocked(2, ""), "component stock locks block concurrent legacy sale")
                End Sub)
        For Each permission In {"Sales", "SB_Update"}
            Fixture(Sub(cn, tx)
                        OpenEdit(cn, tx)
                        Dim r = StepRequest(cn, tx, 1, True)
                        Scalar(cn, tx, "UPDATE dbo.Users_Validations SET " & permission & "=0 WHERE T_ID=1")
                        Reject(Sub() AuditSalesQuantity.ExecuteInTransaction(cn, tx, r), "current permission enforced " & permission, "صلاحية")
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Dim r = StepRequest(cn, tx, 1)
                    Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isVoid=1 WHERE T_ID=" & Bill)
                    Reject(Sub() AuditSalesQuantity.ExecuteInTransaction(cn, tx, r), "void document rejected", "ملغاة")
                End Sub)
        Fixture(Sub(cn, tx)
                    OpenEdit(cn, tx)
                    Dim session = context.SessionId
                    context.SessionId = Guid.NewGuid()
                    Reject(Sub() Snapshot(cn, tx, True), "another login cannot reuse edit opening", "جلسة")
                    context.SessionId = session
                End Sub)
        For Each ordinal In {1, 2}
            Fixture(Sub(cn, tx)
                        AddRecipe(cn, tx) : OpenEdit(cn, tx)
                        Dim r = StepRequest(cn, tx, 1, True)
                        Scalar(cn, tx, "CREATE TRIGGER dbo.AuditTest4DReject ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN IF EXISTS(SELECT 1 FROM inserted WHERE EventCode='Sales.LineQuantityStepped' AND EventOrdinal=" & ordinal & ") THROW 51106,'Intentional Audit4D failure',1; END")
                        Reject(Sub() AuditSalesQuantity.ExecuteInTransaction(cn, tx, r), "writer failure in part " & ordinal & " rolls back quantity and components", "Intentional Audit4D")
                        Check(CInt(Scalar(cn, tx, "SELECT XACT_STATE()")) <= 0, "no committable partial operation")
                    End Sub)
        Next
        Using cn As New SqlConnection(connection)
            cn.Open()
            For i = 0 To tables.Length - 1
                Check(CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & tables(i))) = before(i), "rollback restored " & tables(i))
            Next
            For i = 0 To procedures.Length - 1
                Check(CStr(Scalar(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & procedures(i) & "'))")) = definitions(i), "business definition unchanged " & procedures(i))
            Next
            Check(FinancialState(cn, Nothing) = financialBefore, "all receipt and treasury values restored")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE name='AuditTest4DReject'")) = 0, "no failure trigger retained")
            Check(CStr(Scalar(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = domains, "domain activation unchanged by tests")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Users_Validations WHERE T_ID=1 AND Sales=1 AND SB_Update=1")) = 1, "permissions restored")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.SB_Contents WHERE T_ID=" & Competitor)) = 0, "competing sale fully rolled back")
        End Using
    End Sub
End Module
