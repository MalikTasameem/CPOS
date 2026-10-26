Option Strict On
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks

Friend Module AuditSalesLineEditTests
    Private checks As Integer
    Private connection As String
    Private context As AuditEventContext
    Private candidateSql As String
    Private Const Bill As Integer = 2147482300
    Private Const Line As Integer = 2147482301
    Private Const CompetingLine As Integer = 2147482303

    Public Function Main(args As String()) As Integer
        Try
            Check(AuditContract.Domain("Sales.LinePriceChanged") = "Sales.LineEditing", "separate partial line-edit domain")
            Check(AuditContract.SalesLineFields().Length = 17 AndAlso AuditContract.SalesHeaderFields().Length = 8, "25 closed actual fields")
            Reject(Sub() AuditSalesLineEdit.Exact(1.2345D, False), "excess precision rejected", "الدقة")
            Reject(Sub() AuditSalesLineEdit.Exact(-1D, False), "negative price rejected", "الدقة")
            Reject(Sub() AuditSalesLineEdit.Exact(0D, True), "zero quantity rejected", "الدقة")
            AuditSalesLineEdit.Exact(0D, False) : Check(True, "zero price remains valid")
            Reject(Sub() AuditContract.FieldType("Sales.LinePriceChanged", "Password"), "unknown fields rejected", "الحقل")
            If args.Length = 4 AndAlso args(2) = "--candidate" Then candidateSql = File.ReadAllText(args(3))
            If args.Length >= 2 AndAlso args(0) = "--settings" Then Live(args(1))
            Console.WriteLine("PASS: " & checks & " sales line edit checks.")
            Return 0
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

    Private Function CompetingSaleIsBlocked() As Boolean
        Return Task.Run(Function() As Boolean
                            Using cn As New SqlConnection(connection)
                                cn.Open()
                                Using tx = cn.BeginTransaction()
                                    Try
                                        Using cmd As New SqlCommand("SET LOCK_TIMEOUT 800; INSERT dbo.SB_Contents(T_ID,Bill_T_ID,IM_ID,U_ID,ST_ID,[Date],Price,D_Vaild,QTY,T_Price,U_Cargo,ST_QTY,isDepended) VALUES(@Id,4,1,2,1,GETDATE(),1,N'AUDIT4C',1,1,1,1,1)", cn, tx)
                                            cmd.CommandTimeout = 5
                                            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = CompetingLine
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

    Private Sub Fixture(work As Action(Of SqlConnection, SqlTransaction))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    If candidateSql IsNot Nothing Then Scalar(cn, tx, candidateSql)
                    Scalar(cn, tx, "INSERT dbo.Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Discount,Pure,SB_ID,NeedRepost,S_Bills_Type) VALUES(" & Bill & ",1,GETDATE(),1,0,0,1,110,5,105," & Bill & ",0,1)")
                    Scalar(cn, tx, "INSERT dbo.SB_Contents(T_ID,Bill_T_ID,IM_ID,U_ID,ST_ID,[Date],Cost,Price,QTY,T_Price,U_Cargo,ST_QTY,isDepended,Saler_Percent,Notes,D_Vaild,IM_Discount) VALUES(" & Line & "," & Bill & ",1,2,1,GETDATE(),50,55,2,110,1,2,0,0,N'before',N'AUDIT4C',0)")
                    work(cn, tx)
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
    End Sub

    Private Function Snapshot(cn As SqlConnection, tx As SqlTransaction, Optional edit As Boolean = False) As AuditSalesLineSnapshot
        Return AuditSalesLineEdit.ReadInTransaction(cn, tx, context, Bill, Line, edit)
    End Function

    Private Function Details(cn As SqlConnection, tx As SqlTransaction, Optional edit As Boolean = False, Optional negative As Boolean = True) As AuditOperationRequest
        context.ScreenCode = "Change_IM_Details"
        Return AuditSalesLineEdit.Request(context, Snapshot(cn, tx, edit), "Sales", 60D, edit, negative, False, 2, 3D, 2R, "after")
    End Function

    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim b As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS Audit4C rollback", .ConnectTimeout = 5, .Pooling = False}
        If b.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database.")
        connection = b.ConnectionString
        context = New AuditEventContext With {.SourceId = Guid.NewGuid(), .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "4C", .ScreenCode = "Change_IM_Details"}
        Dim tables = {"Agents_Balance_MV", "SB_Contents", "SB_Contents_Structers", "IM_Struct_Menu", "Agents_Balance_MV_RCT", "Treasury_Balance_MV", "Network_Edit_Tracker", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change"}
        Dim before As Long(), definitions As String(), domains As String, menuFlag As Object, financialBefore As String
        Dim procedures = {"SB_IM_Unit_Change", "IM_Conferm_NewPrice", "AG_Balance_Update_Total", "AG_Balance_Update_Pied_On_Update", "Recalculate_Agent_Bill_Total", "Check_IM_OFFER", "Network_Audit_Event_Write"}
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND database_id=DB_ID() AND session_id<>@@SPID")) = 0, "no other clients")
            Check(CInt(Scalar(cn, Nothing, "SELECT (SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=" & Bill & ")+(SELECT COUNT(*) FROM dbo.SB_Contents WHERE T_ID=" & Line & ")")) = 0, "fixture keys unused")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE parent_id IN(OBJECT_ID('dbo.Agents_Balance_MV'),OBJECT_ID('dbo.SB_Contents'),OBJECT_ID('dbo.IM_Menu'),OBJECT_ID('dbo.Users'),OBJECT_ID('dbo.Users_Validations'),OBJECT_ID('dbo.Network_Audit_Event')) AND is_disabled=0")) = 0, "no unreviewed triggers")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Users u JOIN dbo.Users_Validations_V v ON v.User_ID=u.User_ID WHERE u.User_ID=1 AND u.Valid_ID=1 AND u.is_Allow=1 AND v.Sales=1 AND v.SB_Update=1 AND v.SB_IM_Update=1 AND v.Sales_Dis=1 AND v.SB_Sell_Under_Cost=1")) = 1, "actual actor and permissions verified")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.IM_Menu_Units_V WHERE IM_ID=1 AND U_ID=2 AND U_Cargo=1 AND Cost=50 AND Price=55")) = 1, "known item unit values")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.OFFERS_MASTER WHERE IM_ID=1")) = 0, "fixture has no offer overrides")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.IM_Struct_Menu WHERE IM_ID=1 OR T_ID BETWEEN 2147482450 AND 2147482465")) = 0, "component recipe fixture unused")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.SB_Contents WHERE T_ID=" & CompetingLine)) = 0 AndAlso CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=4 AND BsType_ID=1 AND isVoid=0")) = 1, "competing sale references verified")
            context.SourceId = CType(Scalar(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid)
            domains = CStr(Scalar(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1"))
            menuFlag = Scalar(cn, Nothing, "SELECT isChangePrice FROM dbo.IM_Menu WHERE IM_ID=1")
            financialBefore = FinancialState(cn, Nothing)
            definitions = procedures.Select(Function(p) CStr(Scalar(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & p & "'))"))).ToArray()
            before = tables.Select(Function(t) CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & t))).ToArray()
        End Using
        Fixture(Sub(cn, tx)
                    Check(Not CompetingSaleIsBlocked(), "control fixture alone does not block other sales")
                    AuditSalesLineEdit.ExecuteInTransaction(cn, tx, Details(cn, tx))
                    Check(CompetingSaleIsBlocked(), "locked stock read blocks competing legacy sales until transaction ends")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Details(cn, tx)
                    Check(AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r).Status = "NewPendingCommit", "details and event share transaction")
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.SB_Contents WHERE T_ID=" & Line & " AND QTY=3 AND Price=60 AND T_Price=178 AND IM_Discount=2 AND Notes=N'after'")) = 1, "actual details saved with original calculation")
                    Check(CDec(Scalar(cn, tx, "SELECT Pure FROM dbo.Agents_Balance_MV WHERE T_ID=" & Bill)) = 173D, "header pure recalculated with existing discount")
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "'")) = 25, "25 real before/after fields")
                    Check(AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r).Status = "Completed", "retry returns original result")
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) = 1, "retry creates one event")
                    Dim again = AuditSalesLineEdit.Request(context, Snapshot(cn, tx), "Sales_Fast", 60D, False, True, False, 2, 3D, 2R, "after")
                    Check(AuditSalesLineEdit.ExecuteInTransaction(cn, tx, again).ResultCode.GetValueOrDefault() = 5, "same actual values return NoChange")
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & again.RequestId.ToString() & "'")) = 0, "NoChange creates no false mutation event")
                    Check(FinancialState(cn, tx) = financialBefore, "details do not change receipt or treasury values")
                End Sub)
        Fixture(Sub(cn, tx)
                    context.ScreenCode = "Change_IM_Details"
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.IM_Menu_Units_V WHERE IM_ID=1 AND U_ID=5 AND U_Cargo=1")) = 1, "alternate actual unit verified")
                    Dim r = AuditSalesLineEdit.Request(context, Snapshot(cn, tx), "Sales_Fast", 60D, False, True, False, 5, 4D, 2R, "alternate unit")
                    AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r)
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.SB_Contents WHERE T_ID=" & Line & " AND U_ID=5 AND U_Cargo=1 AND ST_QTY=4 AND QTY=4 AND T_Price=238")) = 1, "unit and quantity use original procedure conversion")
                End Sub)
        Fixture(Sub(cn, tx)
                    context.ScreenCode = "SBChangePrice"
                    Scalar(cn, tx, "UPDATE dbo.IM_Menu SET isChangePrice=1 WHERE IM_ID=1")
                    Dim r = AuditSalesLineEdit.Request(context, Snapshot(cn, tx), "POS", 65D, False, True, False)
                    AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r)
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.SB_Contents WHERE T_ID=" & Line & " AND Price=65 AND QTY=2 AND T_Price=130 AND IM_Discount=0")) = 1, "POS price operation preserves quantity and discount")
                    Check(CDec(Scalar(cn, tx, "SELECT Pure FROM dbo.Agents_Balance_MV WHERE T_ID=" & Bill)) = 125D, "POS price updates actual header")
                    Check(FinancialState(cn, tx) = financialBefore, "POS price does not change receipt or treasury values")
                End Sub)
        Fixture(Sub(cn, tx)
                    context.ScreenCode = "SBChangePrice"
                    Scalar(cn, tx, "UPDATE dbo.IM_Menu SET isChangePrice=0 WHERE IM_ID=1")
                    Dim r = AuditSalesLineEdit.Request(context, Snapshot(cn, tx), "POS", 65D, False, True, False)
                    Reject(Sub() AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r), "item price-change policy enforced", "لا يسمح")
                End Sub)
        Fixture(Sub(cn, tx)
                    context.ScreenCode = "Change_IM_Details"
                    Dim r = AuditSalesLineEdit.Request(context, Snapshot(cn, tx), "Sales", 49D, False, True, False, 2, 2D, 0R, "below cost")
                    Scalar(cn, tx, "UPDATE dbo.Users_Validations SET SB_Sell_Under_Cost=0 WHERE T_ID=1")
                    Reject(Sub() AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r), "actual current cost permission enforced", "أقل من التكلفة")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Details(cn, tx)
                    r.Inputs.Single(Function(x) x.Name = "UnitId").Value = AuditValue.Capture("Integer", 2147482302)
                    Reject(Sub() AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r), "unit outside item rejected", "غير موجود")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Details(cn, tx)
                    r.Inputs.Single(Function(x) x.Name = "LineId").Value = AuditValue.Capture("Integer", 2147482302)
                    Reject(Sub() AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r), "missing line rejected", "غير موجود")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Details(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.SB_Contents SET Bill_T_ID=0 WHERE T_ID=" & Line)
                    Reject(Sub() AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r), "line moved outside invoice rejected", "تغير السطر")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Details(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.SB_Contents SET Price=56 WHERE T_ID=" & Line)
                    Reject(Sub() AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r), "stale line rejected", "تغير السطر")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Details(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET Discount=6 WHERE T_ID=" & Bill)
                    Reject(Sub() AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r), "stale invoice rejected", "تغير السطر")
                End Sub)
        For Each permission In {"Sales", "SB_IM_Update", "Sales_Dis"}
            Fixture(Sub(cn, tx)
                        Dim r = Details(cn, tx)
                        Scalar(cn, tx, "UPDATE dbo.Users_Validations SET [" & permission & "]=0 WHERE T_ID=1")
                        Reject(Sub() AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r), "permission checked during save " & permission, "صلاحية")
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Dim r = Details(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isVoid=1 WHERE T_ID=" & Bill)
                    Reject(Sub() AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r), "void invoice rejected", "ملغاة")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Details(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isDepended=1 WHERE T_ID=" & Bill)
                    Reject(Sub() AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r), "approval while dialog open rejected", "فتح تعديل")
                End Sub)
        Fixture(Sub(cn, tx)
                    context.ScreenCode = "Sales"
                    Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isDepended=1 WHERE T_ID=" & Bill)
                    AuditSalesEdit.ExecuteInTransaction(cn, tx, AuditSalesEdit.Request(context, Bill))
                    Dim r = Details(cn, tx, True)
                    AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r)
                    Check(True, "approved invoice permits recorded current edit session")
                    Check(FinancialState(cn, tx) = financialBefore, "approved edit preserves all receipt and treasury values")
                    Dim originalSession = context.SessionId
                    context.SessionId = Guid.NewGuid()
                    Reject(Sub() Snapshot(cn, tx, True), "different session cannot reuse edit opening", "جلسة")
                    context.SessionId = originalSession
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Details(cn, tx, False, False)
                    Reject(Sub() AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r), "fresh insufficient stock rejected", "الرصيد الحالي")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Details(cn, tx)
                    Scalar(cn, tx, "CREATE TRIGGER dbo.AuditTest4CReject ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN THROW 51106,'Intentional Audit4C failure',1; END")
                    Reject(Sub() AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r), "audit failure prevents saving details", "Intentional Audit4C")
                    Check(CInt(Scalar(cn, tx, "SELECT XACT_STATE()")) <= 0, "no committable partial operation after writer failure")
                End Sub)
        ComponentCases(financialBefore)
        Using cn As New SqlConnection(connection)
            cn.Open()
            For i = 0 To tables.Length - 1
                Check(CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & tables(i))) = before(i), "rollback restored " & tables(i))
            Next
            For i = 0 To procedures.Length - 1
                Check(CStr(Scalar(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & procedures(i) & "'))")) = definitions(i), "business definition unchanged " & procedures(i))
            Next
            Check(CInt(Scalar(cn, Nothing, "SELECT (SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=" & Bill & ")+(SELECT COUNT(*) FROM dbo.SB_Contents WHERE T_ID=" & Line & ")")) = 0, "no fixtures retained")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE name IN('AuditTest4CReject','AuditTest016Reject')")) = 0, "failure triggers rolled back")
            Check(Object.Equals(Scalar(cn, Nothing, "SELECT isChangePrice FROM dbo.IM_Menu WHERE IM_ID=1"), menuFlag), "item policy restored")
            Check(CStr(Scalar(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = domains, "activation unchanged")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Users_Validations WHERE T_ID=1 AND Sales=1 AND SB_IM_Update=1 AND Sales_Dis=1")) = 1, "permission flags restored")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Users_Validations WHERE T_ID=1 AND SB_Sell_Under_Cost=1")) = 1, "cost permission restored")
            Check(FinancialState(cn, Nothing) = financialBefore, "all receipt and treasury values restored after tests")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.SB_Contents WHERE T_ID=" & CompetingLine)) = 0, "competing sale fully rolled back")
        End Using
    End Sub

    Private Sub SeedComponents(cn As SqlConnection, tx As SqlTransaction, Optional count As Integer = 1)
        Scalar(cn, tx, "UPDATE dbo.SB_Contents SET isDepended=1 WHERE T_ID=" & Line)
        For i = 0 To count - 1
            Scalar(cn, tx, "INSERT dbo.IM_Struct_Menu(T_ID,IM_ID,U_IM_ID,Struct_IM_ID,U_ID,Qty,Row_Enabled,User_ID) VALUES(" & (2147482450 + i) & ",1,1,2,2,2,1,1)")
        Next
        Scalar(cn, tx, "EXEC dbo.SB_Contents_Structers_insert_ONE " & Line)
    End Sub

    Private Function ComponentStock(cn As SqlConnection, tx As SqlTransaction) As Decimal
        Return CDec(Scalar(cn, tx, "SELECT ISNULL(SUM(QTY),0) FROM dbo.ST_Balance_V WHERE IM_ID=2 AND ST_ID=1 AND D_Vaild=N''"))
    End Function

    Private Sub ComponentCases(financialBefore As String)
        Fixture(Sub(cn, tx)
                    SeedComponents(cn, tx)
                    Dim stock = ComponentStock(cn, tx), r = Details(cn, tx)
                    AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r)
                    Check(CDec(Scalar(cn, tx, "SELECT SUM(ST_QTY) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=" & Line)) = 6D, "direct quantity edit rebuilds components")
                    Check(ComponentStock(cn, tx) = stock - 2D, "direct quantity edit changes component stock by delta only")
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "'")) = 53, "line/header plus removed/created recipe and both stock buckets captured")
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) = 2, "components are a second part of the same operation")
                    AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r)
                    Check(ComponentStock(cn, tx) = stock - 2D, "replay does not rebuild or consume components again")
                    Dim again = Details(cn, tx)
                    Check(AuditSalesLineEdit.ExecuteInTransaction(cn, tx, again).ResultCode.GetValueOrDefault() = 5, "unchanged detail save keeps NoChange with recipes")
                    Check(FinancialState(cn, tx) = financialBefore, "component detail edit preserves payments")
                End Sub)
        Fixture(Sub(cn, tx)
                    SeedComponents(cn, tx)
                    Scalar(cn, tx, "INSERT dbo.IM_Struct_Menu(T_ID,IM_ID,U_IM_ID,Struct_IM_ID,U_ID,Qty,Row_Enabled,User_ID) VALUES(2147482465,1,362,2,2,3,1,1)")
                    context.ScreenCode = "Change_IM_Details"
                    Dim r = AuditSalesLineEdit.Request(context, Snapshot(cn, tx), "Sales_Fast", 60D, False, True, False, 5, 4D, 2R, "new recipe unit")
                    AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r)
                    Check(CDec(Scalar(cn, tx, "SELECT SUM(ST_QTY) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=" & Line)) = 12D, "new unit uses its own recipe instead of old unit recipe")
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=" & Line)) = 1, "unit change replaces rather than appends structures")
                End Sub)
        Fixture(Sub(cn, tx)
                    SeedComponents(cn, tx, 11)
                    Dim r = Details(cn, tx)
                    AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r)
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) = 4, "large recipe detail audit splits complete groups")
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "'")) = 253, "all 253 values survive pagination")
                End Sub)
        Fixture(Sub(cn, tx)
                    SeedComponents(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.SB_Contents SET isDepended=0 WHERE T_ID=" & Line)
                    Dim r = Details(cn, tx)
                    AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r)
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=" & Line)) = 0, "inactive detail edit clears stale active components")
                End Sub)
        Fixture(Sub(cn, tx)
                    SeedComponents(cn, tx)
                    ' Main stock does not change; only the new-unit recipe consumes more component stock.
                    Scalar(cn, tx, "INSERT dbo.IM_Struct_Menu(T_ID,IM_ID,U_IM_ID,Struct_IM_ID,U_ID,Qty,Row_Enabled,User_ID) VALUES(2147482465,1,362,2,2,999999,1,1)")
                    Dim r = AuditSalesLineEdit.Request(context, Snapshot(cn, tx), "Sales", 60D, False, False, False, 5, 2D, 0R, "component shortage")
                    Reject(Sub() AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r), "component-only shortage rejects whole detail edit", "أحد مكوناته")
                End Sub)
        Fixture(Sub(cn, tx)
                    SeedComponents(cn, tx)
                    Dim r = Details(cn, tx)
                    Scalar(cn, tx, "CREATE TRIGGER dbo.AuditTest016Reject ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN IF EXISTS(SELECT 1 FROM inserted WHERE EventOrdinal=2) THROW 51106,'Intentional component audit failure',1; END")
                    Reject(Sub() AuditSalesLineEdit.ExecuteInTransaction(cn, tx, r), "failure in component audit part prevents partial save", "Intentional component audit")
                    Check(CInt(Scalar(cn, tx, "SELECT XACT_STATE()")) <= 0, "component audit failure leaves no committable partial change")
                End Sub)
    End Sub
End Module
