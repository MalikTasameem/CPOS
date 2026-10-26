Option Strict On
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq

Friend Module AuditSalesDeleteTests
    Private checks As Integer
    Private connection As String
    Private context As AuditEventContext
    Private Const Bill As Integer = 2147482300
    Private Const Line As Integer = 2147482301
    Private Const Other As Integer = 2147482302
    Private Const Child As Integer = 2147482310

    Public Function Main(args As String()) As Integer
        Try
            Check(AuditContract.Domain(AuditSalesDelete.Operation) = "Sales.LineLifecycle", "closed deletion domain")
            Check(AuditContract.SalesDeleteFields("SalesLine").Length = 24, "all non-key sales columns captured")
            Check(AuditContract.SalesDeleteFields("SalesTimedReservation").Single(Function(x) x.StartsWith("ReservationTime|")).EndsWith("String|time(0)"), "SQL time preserved as explicit hh:mm:ss")
            Reject(Sub() AuditContract.FieldType(AuditSalesDelete.Operation, "Anything"), "unknown field rejected", "الحقل")
            If args.Length = 2 AndAlso args(0) = "--settings" Then Live(args(1))
            Console.WriteLine("PASS: " & checks & " sales deletion checks.") : Return 0
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
        Using cmd As New SqlCommand(sql, cn, tx)
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
                    Q(cn, tx, "INSERT dbo.Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Discount,Pure,SB_ID,NeedRepost,S_Bills_Type) VALUES(" & Bill & ",1,GETDATE(),1,0,0,1,130,5,125," & Bill & ",0,1)")
                    Q(cn, tx, "INSERT dbo.SB_Contents(T_ID,Bill_T_ID,IM_ID,U_ID,ST_ID,[Date],Cost,Price,QTY,T_Price,U_Cargo,ST_QTY,isDepended,Saler_Percent,Notes,D_Vaild,IM_Discount,Barcode,Serial_Code,Compons) VALUES(" & Line & "," & Bill & ",1,2,1,GETDATE(),50,55,2,110,1,2,1,0,N'delete fixture',N'2099-12-27',0,N'barcode',N'serial',N'components text'),(" & Other & "," & Bill & ",1,2,1,GETDATE(),2,20,1,20,1,1,1,0,N'untouched',N'2099-12-27',0,NULL,NULL,NULL)")
                    context.ScreenCode = "Sales"
                    work(cn, tx)
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
    End Sub

    Private Function Request(cn As SqlConnection, tx As SqlTransaction, Optional edit As Boolean = False) As AuditOperationRequest
        Return AuditSalesDelete.Request(context, AuditSalesDelete.ReadInTransaction(cn, tx, context, Bill, Line, edit), edit)
    End Function

    Private Sub OpenEdit(cn As SqlConnection, tx As SqlTransaction)
        Q(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isDepended=1 WHERE T_ID=" & Bill)
        AuditSalesEdit.ExecuteInTransaction(cn, tx, AuditSalesEdit.Request(context, Bill))
    End Sub

    Private Sub Children(cn As SqlConnection, tx As SqlTransaction, Optional components As Integer = 1)
        Q(cn, tx, "INSERT dbo.SB_Contents_Structers(SB_Cont_T_ID,[DATE],Struct_ID,U_ID,ST_ID,QTY,U_Cargo,ST_QTY,isDepended,Cost) VALUES(" & Line & ",GETDATE(),2,2,1,4,1,4,1,NULL)")
        For i = 0 To components - 1
            Q(cn, tx, "INSERT dbo.SB_IM_Components(T_ID,IM_T_ID,Com_ID,QTY,Price) VALUES(" & (Child + i) & "," & Line & ",1,2,3)")
        Next
        Q(cn, tx, "INSERT dbo.SB_Rsv(T_ID,SB_T_ID,DATE_F) VALUES(" & Child & "," & Line & ",'2099-12-27T11:12:13')")
        Q(cn, tx, "INSERT dbo.SB_Rsv_2(T_ID,SB_T_ID,IM_ID,DATE_F,[TIME],NOTES) VALUES(" & Child & "," & Line & ",1,'2099-12-27','11:12:13',N'timed reservation')")
        Q(cn, tx, "INSERT dbo.SB_VALUE_Bercent_Details(SB_T_ID,AG_ID,VALUE_Bercent) VALUES(" & Line & ",1,7)")
    End Sub

    Private Function EventCount(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest) As Integer
        Return CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'"))
    End Function

    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim b As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS Audit4E rollback", .ConnectTimeout = 5}
        If b.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database.")
        connection = b.ConnectionString
        context = New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "4E", .ScreenCode = "Sales"}
        Dim tables = {"Agents_Balance_MV", "SB_Contents", "SB_Contents_Structers", "SB_IM_Components", "SB_Rsv", "SB_Rsv_2", "SB_VALUE_Bercent_Details", "Agents_Balance_MV_RCT", "Treasury_Balance_MV", "Network_Edit_Tracker", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change"}
        Dim procs = {"SB_Contents_Delete_IM", "SB_Contents_Structers_Remove_ONE", "AG_Balance_Update_Pied_On_Update", "AG_Balance_Update_Total", "Recalculate_Agent_Bill_Total", "Inc_IM_QTY"}
        Dim counts As Long(), definitions As String(), domains As String, financialState As String
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other clients")
            Check(CInt(Q(cn, Nothing, "SELECT (SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=" & Bill & ")+(SELECT COUNT(*) FROM dbo.SB_Contents WHERE T_ID BETWEEN " & Line & " AND " & Other & ")+(SELECT COUNT(*) FROM dbo.SB_IM_Components WHERE T_ID BETWEEN " & Child & " AND " & (Child + 30) & ")+(SELECT COUNT(*) FROM dbo.SB_Rsv WHERE T_ID=" & Child & ")+(SELECT COUNT(*) FROM dbo.SB_Rsv_2 WHERE T_ID=" & Child & ")")) = 0, "fixture identities unused")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(" & String.Join(",", tables.Select(Function(x) "OBJECT_ID('dbo." & x & "')")) & ",OBJECT_ID('dbo.Users'),OBJECT_ID('dbo.Users_Validations'))")) = 0, "no unreviewed test triggers")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.Users u JOIN dbo.Users_Validations_V v ON v.User_ID=u.User_ID WHERE u.User_ID=1 AND u.Valid_ID=1 AND u.is_Allow=1 AND v.Sales=1 AND v.SB_Update=1")) = 1, "actual user permissions")
            context.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid)
            Check(context.SourceId = Guid.Parse("3c520084-0214-43d4-a5fb-797a179c7a7c"), "approved source")
            counts = tables.Select(Function(x) CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & x))).ToArray()
            definitions = procs.Select(Function(x) CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & x & "'))"))).ToArray()
            domains = CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1"))
            financialState = Financial(cn, Nothing)
        End Using
        For Each screen In {"Sales", "Sales_Fast", "POS"}
            Fixture(Sub(cn, tx)
                        context.ScreenCode = screen
                        Dim r = Request(cn, tx)
                        Check(AuditSalesDelete.ExecuteInTransaction(cn, tx, r).Status = "NewPendingCommit", "atomic delete " & screen)
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.SB_Contents WHERE T_ID=" & Line)) = 0, "requested line deleted " & screen)
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.SB_Contents WHERE T_ID=" & Other & " AND QTY=1 AND T_Price=20")) = 1, "other line unchanged " & screen)
                        Check(EventCount(cn, tx, r) = 2, "main and stock parts " & screen)
                        Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND c.EntityType='SalesLine' AND c.BeforeState IN(2,3) AND c.AfterState=1")) = 24, "all deleted columns captured " & screen)
                        Check(AuditSalesDelete.ExecuteInTransaction(cn, tx, r).Status = "Completed" AndAlso EventCount(cn, tx, r) = 2, "replay does not delete again " & screen)
                        Check(CDec(Q(cn, tx, "SELECT Pure FROM dbo.Agents_Balance_MV WHERE T_ID=" & Bill)) = 125D, "unapproved header behavior preserved " & screen)
                        Check(Financial(cn, tx) = financialState, "payments unchanged " & screen)
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Children(cn, tx) : OpenEdit(cn, tx)
                    Dim r = Request(cn, tx, True)
                    AuditSalesDelete.ExecuteInTransaction(cn, tx, r)
                    Check(CDec(Q(cn, tx, "SELECT Pure FROM dbo.Agents_Balance_MV WHERE T_ID=" & Bill)) = 15D, "approved header recalculated by original procedure")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND c.EntityType IN('SalesStructure','SalesComponent','SalesReservation','SalesTimedReservation','SalesCommission') AND c.AfterState=1")) = 23, "all five dependent entities captured as deleted")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND c.FieldCode='ReservationTime' AND c.BeforeValue=N'11:12:13'")) = 1, "reservation time captured exactly")
                    Check(Financial(cn, tx) = financialState, "approved delete payments unchanged")
                End Sub)
        Fixture(Sub(cn, tx)
                    Children(cn, tx, 30) : OpenEdit(cn, tx)
                    Dim r = Request(cn, tx, True)
                    AuditSalesDelete.ExecuteInTransaction(cn, tx, r)
                    Check(EventCount(cn, tx, r) = 3, "large dependent rows split without truncation")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "'")) = 179, "all 179 actual values recorded")
                End Sub)
        Fixture(Sub(cn, tx)
                    Children(cn, tx)
                    Dim r = Request(cn, tx)
                    AuditSalesDelete.ExecuteInTransaction(cn, tx, r)
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=" & Line)) = 0, "approved correction removes components even before invoice approval")
                    Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND c.EntityType='SalesStructure' AND c.AfterState=1")) = 10, "all removed component values recorded as absent")
                End Sub)
        For Each mutationSql As String In {"UPDATE dbo.SB_Contents SET Notes=N'changed' WHERE T_ID=" & Line, "UPDATE dbo.Agents_Balance_MV SET Discount=6 WHERE T_ID=" & Bill, "UPDATE dbo.SB_Rsv_2 SET [TIME]='12:00:00' WHERE T_ID=" & Child}
            Fixture(Sub(cn, tx)
                        Children(cn, tx)
                        Dim r = Request(cn, tx)
                        Q(cn, tx, mutationSql)
                        Reject(Sub() AuditSalesDelete.ExecuteInTransaction(cn, tx, r), "stale line/header/dependent rejected", "تغير السطر")
                    End Sub)
        Next
        For Each permission In {"Sales", "SB_Update"}
            Fixture(Sub(cn, tx)
                        OpenEdit(cn, tx)
                        Dim r = Request(cn, tx, True)
                        Q(cn, tx, "UPDATE dbo.Users_Validations SET " & permission & "=0 WHERE T_ID=1")
                        Reject(Sub() AuditSalesDelete.ExecuteInTransaction(cn, tx, r), "permission revoked " & permission, "صلاحية")
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx)
                    Q(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isVoid=1 WHERE T_ID=" & Bill)
                    Reject(Sub() AuditSalesDelete.ExecuteInTransaction(cn, tx, r), "void document rejected", "ملغاة")
                End Sub)
        Fixture(Sub(cn, tx)
                    OpenEdit(cn, tx)
                    Dim r = Request(cn, tx, True) : r.Context.SessionId = Guid.NewGuid()
                    Reject(Sub() AuditSalesDelete.ExecuteInTransaction(cn, tx, r), "different login rejected", "جلسة")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = Request(cn, tx)
                    Q(cn, tx, "UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code=""Sales.LineLifecycle""]') WHERE ConfigId=1")
                    Reject(Sub() AuditSalesDelete.ExecuteInTransaction(cn, tx, r), "disabled domain fails closed", "disabled")
                End Sub)
        For Each part In {1, 2}
            Fixture(Sub(cn, tx)
                        Children(cn, tx) : OpenEdit(cn, tx)
                        Dim r = Request(cn, tx, True)
                        Q(cn, tx, "CREATE TRIGGER dbo.AuditTest4EReject ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN IF EXISTS(SELECT 1 FROM inserted WHERE EventCode='Sales.LineDeleted' AND EventOrdinal=" & part & ") THROW 51106,'Intentional delete audit failure',1; END")
                        Reject(Sub() AuditSalesDelete.ExecuteInTransaction(cn, tx, r), "writer failure part " & part, "Intentional")
                        Check(CInt(Q(cn, If(tx.Connection Is Nothing, Nothing, tx), "SELECT XACT_STATE()")) <= 0, "no partial committable operation")
                    End Sub)
        Next
        Using cn As New SqlConnection(connection)
            cn.Open()
            For i = 0 To tables.Length - 1
                Check(CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & tables(i))) = counts(i), "rollback restored " & tables(i))
            Next
            For i = 0 To procs.Length - 1
                Check(CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & procs(i) & "'))")) = definitions(i), "business definition unchanged " & procs(i))
            Next
            Check(Financial(cn, Nothing) = financialState, "all payments restored")
            Check(CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = domains, "domains restored")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE name='AuditTest4EReject'")) = 0, "failure trigger rolled back")
        End Using
    End Sub
End Module
