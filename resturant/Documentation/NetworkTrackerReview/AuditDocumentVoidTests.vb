Option Strict On
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq

Friend Module AuditDocumentVoidTests
    Private checks As Integer
    Private connection As String
    Private candidate As String
    Private context As AuditEventContext
    Private Const Bill As Integer = 2147482600
    Private Const Line As Integer = 2147482601
    Private Const Receipt As Integer = 2147482602
    Private ReadOnly Kinds As Integer() = {1, 2, 7, 8, 9, 10, 11, 13, 16, 17, 18, 35}
    Private ReadOnly Tables As String() = {"Agents_Balance_MV", "SB_Contents", "SB_Contents_Structers", "EXP_Details", "Pch_Details", "IMEX_Details", "Rtn_Contents", "FRM_Details", "FRM_Contents_Details", "InSale_Details", "OutSale_Details", "Agents_Balance_MV_RCT", "Treasury_Balance_MV", "TB_AutoPrint_tmp", "Network_Edit_Tracker", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change"}

    Public Function Main(args As String()) As Integer
        Try
            Check(AuditContract.Domain(AuditDocumentVoid.Operation) = "Documents.Cancellation", "closed cancellation domain")
            Check(AuditContract.VoidFields("BusinessDocument").Length = 23, "23 actual header values")
            Check(AuditContract.VoidFields("Receipt").Length = 20, "20 actual receipt values")
            Reject(Sub() AuditContract.VoidSpec(19), "unknown type denied", "غير معتمد")
            Reject(Sub() AuditContract.VoidFields("Users"), "sensitive table denied", "غير معتمد")
            If args.Length >= 2 Then
                If args.Length = 4 Then candidate = File.ReadAllText(args(3))
                Live(args(1))
            End If
            Console.WriteLine("PASS " & checks & " cancellation checks.") : Return 0
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
            If Not ex.Message.Contains(expected) Then Throw New Exception("Unexpected rejection: " & label, ex)
            Check(True, label) : Return
        End Try
        Throw New Exception("Expected rejection: " & label)
    End Sub

    Private Function Q(cn As SqlConnection, tx As SqlTransaction, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn, tx) With {.CommandTimeout = 30}
            Return cmd.ExecuteScalar()
        End Using
    End Function

    Private Sub Fixture(kind As Integer, work As Action(Of SqlConnection, SqlTransaction), Optional approved As Integer = 1)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    If candidate IsNot Nothing Then Q(cn, tx, candidate)
                    Dim spec = AuditContract.VoidSpec(kind)
                    Q(cn, tx, "INSERT dbo.Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Discount,Pure,[" & spec(0) & "],NeedRepost,S_Bills_Type) VALUES(" & Bill & ",1,GETDATE()," & kind & "," & approved & ",0,1,100,5,95," & Bill & ",1,1)")
                    Q(cn, tx, "INSERT dbo.Agents_Balance_MV_RCT(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Discount,Pure,Receipt_Tran_ID,Receipt_Num,Pay_ID,Tr_ID,NeedRepost) VALUES(" & Receipt & ",1,GETDATE(),3,1,0,1,50,0,50," & Bill & "," & Receipt & ",1,1,1)")
                    Q(cn, tx, "INSERT dbo.Treasury_Balance_MV(T_ID,Tr_ID,Tr_Type_ID,User_ID,AGBalance_T_ID,Pure,Debit,Credit,Balance,isVoid) VALUES(" & Receipt & ",1,3,1," & Receipt & ",50,50,0,50,0)")
                    Select Case kind
                        Case 1
                            Q(cn, tx, "INSERT dbo.SB_Contents(T_ID,Bill_T_ID,IM_ID,U_ID,ST_ID,[Date],Price,QTY,T_Price,U_Cargo,ST_QTY,isDepended,D_Vaild) VALUES(" & Line & "," & Bill & ",1,2,1,GETDATE(),10,2,20,1,2,1,'')")
                            Q(cn, tx, "INSERT dbo.SB_Contents_Structers(SB_Cont_T_ID,[DATE],Struct_ID,U_ID,ST_ID,QTY,U_Cargo,ST_QTY,isDepended,Cost) VALUES(" & Line & ",GETDATE(),2,2,1,4,1,4,1,0)")
                        Case 2
                            Q(cn, tx, "INSERT dbo.EXP_Details(T_ID,IMEX_T_ID,[Date],EX_ID,QYT,Price,Total,isDepended) VALUES(" & Line & "," & Bill & ",GETDATE(),1,2,10,20,1)")
                        Case 7, 9
                            Q(cn, tx, "INSERT dbo.Pch_Details(T_ID,Pch_T_ID,[Date],IM_ID,U_ID,ST_ID,Price,QYT,Total,U_Cargo,ST_QTY,isDepended,D_Vaild) VALUES(" & Line & "," & Bill & ",GETDATE(),1,2,1,10,2,20,1,2,1,'')")
                        Case 10, 11
                            Q(cn, tx, "INSERT dbo.Rtn_Contents(T_ID,Bill_T_ID,[Date],Rtn_Type_ID,IM_ID,U_ID,ST_ID,Price,QTY,T_Price,U_Cargo,ST_QTY,isDepended,D_Vaild) VALUES(" & Line & "," & Bill & ",GETDATE()," & If(kind = 10, 1, 7) & ",1,2,1,10,2,20,1,2,1,'')")
                        Case 13, 18
                            Q(cn, tx, "INSERT dbo.FRM_Details(T_ID,FRM_T_ID,[Date],IM_ID,U_ID,ST_ID,Price,QYT,Total,U_Cargo,ST_QTY,isDepended,D_Vaild) VALUES(" & Line & "," & Bill & ",GETDATE(),1,2,1,10,2,20,1,2,1,'')")
                            Q(cn, tx, "INSERT dbo.FRM_Contents_Details(Bill_T_ID,[DATE],IM_ID,U_ID,ST_ID,Cost,QYT,U_Cargo,ST_QTY,isDepended,D_Vaild) VALUES(" & Bill & ",GETDATE(),2,2,1,10,4,1,4,1,'')")
                        Case 8, 17, 35
                            Q(cn, tx, "INSERT dbo.[" & spec(3) & "](T_ID,[" & spec(4) & "],[Date],IM_ID,U_ID,ST_ID,QYT,Total,U_Cargo,ST_QTY,isDepended,D_Vaild) VALUES(" & Line & "," & Bill & ",GETDATE(),1,2,1,2,20,1,2,1,'')")
                    End Select
                    work(cn, tx)
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
    End Sub

    Private Function Request(cn As SqlConnection, tx As SqlTransaction, kind As Integer) As AuditOperationRequest
        Return AuditDocumentVoid.Request(context, AuditDocumentVoid.Capture(cn, tx, Bill, kind))
    End Function

    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim builder As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS Cancellation018 rollback", .ConnectTimeout = 5, .Pooling = False}
        If builder.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database.")
        connection = builder.ConnectionString
        context = New AuditEventContext With {.ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTests", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "018", .ScreenCode = "Sales"}
        Dim counts As Long(), definitions As String(), domains As String
        Dim procedures = {"AG_Balance_Void_Row", "Agents_Balance_MV_Cancel", "Agents_Balance_MV_RCT_VOID", "Agents_Balance_MV_RCT_Cancel", "SB_Contents_Structers_Remove", "Network_Audit_Request_Begin", "Network_Audit_Event_Write"}
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other clients")
            context.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid)
            Check(context.SourceId = Guid.Parse("3c520084-0214-43d4-a5fb-797a179c7a7c"), "approved source")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(" & String.Join(",", Tables.Select(Function(x) "OBJECT_ID('dbo." & x & "')")) & ")")) = 0, "no unreviewed triggers")
            For Each table In Tables.Where(Function(x) Not x.StartsWith("Network_") AndAlso Not {"SB_Contents_Structers", "FRM_Contents_Details", "TB_AutoPrint_tmp"}.Contains(x))
                Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM dbo.[" & table & "] WHERE T_ID BETWEEN " & Bill & " AND " & Receipt)) = 0, "unused fixture keys " & table)
            Next
            counts = Tables.Select(Function(x) CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.[" & x & "]"))).ToArray()
            definitions = procedures.Select(Function(x) CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & x & "'))"))).ToArray()
            domains = CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1"))
        End Using
        For Each kind In Kinds
            For Each screen In AuditContract.VoidSpec(kind)(2).Split(","c)
                context.ScreenCode = screen
                Fixture(kind, Sub(cn, tx)
                                  Dim r = Request(cn, tx, kind)
                                  Check(AuditDocumentVoid.ExecuteInTransaction(cn, tx, r).Status = "NewPendingCommit", "atomic cancellation " & screen & "/" & kind)
                                  Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=" & Bill & " AND isVoid=1 AND VoidBy=1 AND Total=100 AND Discount=5 AND Pure=95 AND NeedRepost=0")) = 1, "unchanged formula and legacy VoidBy " & screen)
                                  Check(CInt(Q(cn, tx, "SELECT isVoid FROM dbo.Agents_Balance_MV_RCT WHERE T_ID=" & Receipt)) = 1, "receipt cancelled " & screen)
                                  Check(CBool(Q(cn, tx, "SELECT isVoid FROM dbo.Treasury_Balance_MV WHERE T_ID=" & Receipt)), "treasury cancelled " & screen)
                                  Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND c.EntityType='Receipt'")) = 20, "complete receipt evidence " & screen)
                                  Check(AuditDocumentVoid.ExecuteInTransaction(cn, tx, r).Status = "Completed", "replay does not re-cancel " & screen)
                                  If kind = 1 Then Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=" & Line)) = 0, "approved sale removes components")
                              End Sub)
            Next
        Next
        context.ScreenCode = "Sales"
        Fixture(1, Sub(cn, tx)
                       Dim r = Request(cn, tx, 1)
                       AuditDocumentVoid.ExecuteInTransaction(cn, tx, r)
                       Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.SB_Contents_Structers WHERE SB_Cont_T_ID=" & Line)) = 1, "deferred pre-approval component behavior preserved")
                       Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND c.EntityType='SalesStructure' AND c.AfterState IN(2,3)")) = 10, "remaining component truthfully captured")
                   End Sub, 0)
        Fixture(1, Sub(cn, tx)
                       Dim r = Request(cn, tx, 1)
                       Q(cn, tx, "UPDATE dbo.Agents_Balance_MV_RCT SET Pure=51 WHERE T_ID=" & Receipt)
                       Reject(Sub() AuditDocumentVoid.ExecuteInTransaction(cn, tx, r), "stale receipt graph rejected", "تغير المستند")
                   End Sub)
        Fixture(1, Sub(cn, tx)
                       Dim r = Request(cn, tx, 1)
                       Q(cn, tx, "UPDATE dbo.Users SET is_Allow=0 WHERE User_ID=1")
                       Reject(Sub() AuditDocumentVoid.ExecuteInTransaction(cn, tx, r), "disabled user rejected", "صلاحية")
                   End Sub)
        Fixture(1, Sub(cn, tx)
                       Q(cn, tx, "UPDATE dbo.Users SET Valid_ID=1,is_Allow=1 WHERE User_ID=1001")
                       context.ActorUserId = 1001
                       Try
                           Dim r = Request(cn, tx, 1)
                           AuditDocumentVoid.ExecuteInTransaction(cn, tx, r)
                           Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "' AND ActorUserId=1001")) >= 2, "actual actor differs from legacy fixed actor")
                           Check(CInt(Q(cn, tx, "SELECT VoidBy FROM dbo.Agents_Balance_MV WHERE T_ID=" & Bill)) = 1, "business actor correction explicitly deferred")
                       Finally
                           context.ActorUserId = 1
                       End Try
                   End Sub)
        Fixture(1, Sub(cn, tx)
                       Q(cn, tx, "INSERT dbo.SB_Contents_Structers(SB_Cont_T_ID,[DATE],Struct_ID,U_ID,ST_ID,QTY,U_Cargo,ST_QTY,isDepended,Cost) SELECT " & Line & ",GETDATE(),2,2,1,4,1,4,1,0 FROM (VALUES(1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11)) n(v)")
                       Dim r = Request(cn, tx, 1)
                       AuditDocumentVoid.ExecuteInTransaction(cn, tx, r)
                       Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) >= 3, "large graph paged")
                       Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & r.RequestId.ToString() & "' AND c.EntityType='SalesStructure' AND c.AfterState=1")) = 120, "all removed components captured without truncation")
                   End Sub)
        Fixture(1, Sub(cn, tx)
                       Dim r = Request(cn, tx, 1)
                       Q(cn, tx, "UPDATE dbo.Users_Validations SET SalesVoid=0 WHERE T_ID=1")
                       Reject(Sub() AuditDocumentVoid.ExecuteInTransaction(cn, tx, r), "cancellation permission checked again", "صلاحية")
                   End Sub)
        Fixture(1, Sub(cn, tx)
                       Dim r = Request(cn, tx, 1)
                       r.Context.ScreenCode = "Pch"
                       Reject(Sub() AuditDocumentVoid.ExecuteInTransaction(cn, tx, r), "wrong screen cannot cancel sale", "شاشة الإلغاء")
                   End Sub)
        Fixture(1, Sub(cn, tx)
                       Dim r = Request(cn, tx, 1)
                       Q(cn, tx, "UPDATE dbo.Agents_Balance_MV_RCT SET isVoid=1 WHERE T_ID=" & Receipt)
                       r = Request(cn, tx, 1)
                       Reject(Sub() AuditDocumentVoid.ExecuteInTransaction(cn, tx, r), "existing receipt cancellation failure preserved", "ROLLBACK TRANSACTION")
                   End Sub)
        Fixture(1, Sub(cn, tx)
                       Dim r = Request(cn, tx, 1)
                       Q(cn, tx, "CREATE TRIGGER dbo.Audit018_TestFail ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN IF EXISTS(SELECT 1 FROM inserted WHERE EventCode='Document.Voided' AND EventOrdinal=2) THROW 51118,'Audit018 injected failure',1; END")
                       Reject(Sub() AuditDocumentVoid.ExecuteInTransaction(cn, tx, r), "second event failure rolls back business", "Audit018 injected failure")
                   End Sub)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(Tables.Select(Function(x) CLng(Q(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.[" & x & "]"))).SequenceEqual(counts), "all row counts restored")
            Check(procedures.Select(Function(x) CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo." & x & "'))"))).SequenceEqual(definitions), "business and audit definitions restored")
            Check(CStr(Q(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = domains, "domain baseline restored")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE name='Audit018_TestFail'")) = 0, "no fault trigger left")
        End Using
    End Sub
End Module
