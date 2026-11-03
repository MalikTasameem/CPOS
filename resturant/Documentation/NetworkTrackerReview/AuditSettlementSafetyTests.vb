Option Strict On
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Threading.Tasks

Friend Module AuditSettlementSafetyTests
    Private checks As Integer
    Private connection As String
    Private context As AuditEventContext
    Private itemId As Integer, storeId As Integer, unitId As Integer
    Private Const Doc As Integer = 2147482600
    Private Const Seed As Integer = 2147482601
    Private Const CompetingSale As Long = 2147482602L
    Private ReadOnly Expiry As New DateTime(2099, 11, 27)

    Public Function Main(args As String()) As Integer
        Try
            Check(AuditSettlementLines.VerifiedDifference(12D, 10R, 10R) = 2D, "fresh positive difference")
            Check(AuditSettlementLines.VerifiedDifference(8D, 10R, 10R) = -2D, "fresh negative difference")
            Reject(Sub() AuditSettlementLines.VerifiedDifference(12D, 10R, 8R), "stale displayed balance rejected", "تغير رصيد")
            Reject(Sub() AuditSettlementLines.VerifiedDifference(1D, 0.333333R, 0.333333R), "unrepresentable difference rejected", "الدقة")
            If args.Length = 2 AndAlso args(0) = "--settings" Then Live(args(1))
            Console.WriteLine("PASS: " & checks & " safety checks.")
            Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString())
            Return 1
        End Try
    End Function

    Private Sub Check(ok As Boolean, name As String)
        If Not ok Then Throw New Exception("FAILED: " & name)
        checks += 1 : Console.WriteLine("PASS " & name)
    End Sub

    Private Sub Reject(action As Action, name As String, expected As String)
        Try
            action()
        Catch ex As Exception
            If Not ex.Message.Contains(expected) Then Throw New Exception("Unexpected failure: " & name, ex)
            Check(True, name) : Return
        End Try
        Throw New Exception("Expected rejection: " & name)
    End Sub

    Private Function Scalar(cn As SqlConnection, tx As SqlTransaction, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn, tx)
            Return cmd.ExecuteScalar()
        End Using
    End Function

    Private Function Balance(cn As SqlConnection, tx As SqlTransaction) As Double
        Return CDbl(Scalar(cn, tx, "SELECT ISNULL(SUM(QTY),0) FROM dbo.ST_Balance_V WHERE IM_ID=" & itemId & " AND ST_ID=" & storeId & " AND D_Vaild='2099-11-27'"))
    End Function

    Private Function SavedLine(cn As SqlConnection, tx As SqlTransaction, id As Integer) As DataRow
        Using cmd As New SqlCommand("SELECT * FROM dbo.ST_QTY_Deferences WHERE T_ID=" & id, cn, tx)
            Dim dt As New DataTable()
            Using rd = cmd.ExecuteReader()
                dt.Load(rd)
            End Using
            Return dt.Rows(0)
        End Using
    End Function

    Private Function Add(target As Decimal, shown As Double) As AuditOperationRequest
        Return AuditSettlementLines.AddRequest(context, Doc, itemId, storeId, unitId, "Audit3D", target, target - CDec(shown), shown, Expiry)
    End Function

    Private Function RejectedStockUnchanged(cn As SqlConnection, tx As SqlTransaction, disabledBalance As Double) As Boolean
        ' THROW with XACT_ABORT can roll back the outer fixture, including its seed.
        If CInt(Scalar(cn, tx, "SELECT XACT_STATE()")) = 0 Then
            Return CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM ST_QTY_Deferences WHERE T_ID=" & Seed)) = 0 AndAlso
                CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM Agents_Balance_MV WHERE T_ID=" & Doc)) = 0 AndAlso Balance(cn, tx) = 0R
        End If
        Return Balance(cn, tx) = disabledBalance AndAlso CDbl(SavedLine(cn, tx, Seed)("ST_QTY")) = 8R
    End Function

    Private Function Id(result As AuditRequestResult) As Integer
        Return Integer.Parse(AuditCanonical.ParseXml(result.ResultXml).Element("Values").Elements("Value").Single(Function(x) CStr(x.Attribute("name")) = "LineId").Value)
    End Function

    Private Sub Header(cn As SqlConnection, tx As SqlTransaction, action As String)
        AuditSettlementHeader.ExecuteInTransaction(cn, tx, AuditSettlementHeader.Request(context, action, Doc, New DateTime(2026, 10, 26), "Audit3D", "Rollback fixture", False))
    End Sub

    Private Sub Fixture(test As Action(Of SqlConnection, SqlTransaction))
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction()
                Try
                    Using cmd As New SqlCommand("INSERT dbo.Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Discount,Pure,ST_Sett_ID) VALUES(@Doc,1,GETDATE(),39,0,0,1,0,0,0,@Doc); INSERT dbo.ST_QTY_Deferences(T_ID,Bill_T_ID,[DATE],IM_ID,ST_ID,Valid_D,ST_QTY,Cost,U_Cargo,Barcode,TOTAL) VALUES(@Seed,@Doc,GETDATE(),@Item,@Store,N'2099-11-27',8,7,1,N'Audit3DSeed',56);", cn, tx)
                        cmd.Parameters.AddWithValue("@Doc", Doc) : cmd.Parameters.AddWithValue("@Seed", Seed)
                        cmd.Parameters.AddWithValue("@Item", itemId) : cmd.Parameters.AddWithValue("@Store", storeId)
                        cmd.ExecuteNonQuery()
                    End Using
                    test(cn, tx)
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
    End Sub

    ' A legacy sales writer does not acquire an audit application lock. Its stock write
    ' must still wait for the settlement's locked stock read. Never commit this fixture.
    Private Function CompetingSaleIsBlocked() As Boolean
        Dim competingTask = Task.Run(Function() As Boolean
                                Using cn As New SqlConnection(connection)
                                    cn.Open()
                                    Using tx = cn.BeginTransaction()
                                        Try
                                            Using cmd As New SqlCommand("SET LOCK_TIMEOUT 800; INSERT dbo.SB_Contents(T_ID,Bill_T_ID,IM_ID,U_ID,ST_ID,[Date],Price,D_Vaild,QTY,T_Price,U_Cargo,ST_QTY,isDepended) VALUES(@Id,4,@Item,@Unit,@Store,GETDATE(),1,N'2099-11-27',1,1,1,1,1);", cn, tx)
                                                cmd.CommandTimeout = 5
                                                cmd.Parameters.AddWithValue("@Id", CompetingSale)
                                                cmd.Parameters.AddWithValue("@Item", itemId)
                                                cmd.Parameters.AddWithValue("@Unit", unitId)
                                                cmd.Parameters.AddWithValue("@Store", storeId)
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
                            End Function)
        Return competingTask.GetAwaiter().GetResult()
    End Function

    Private Sub Live(settingsPath As String)
        Dim setting = File.ReadAllLines(settingsPath).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim b As New SqlConnectionStringBuilder(setting.Substring(10)) With {.ApplicationName = "CPOS Audit3D rollback tests", .ConnectTimeout = 5}
        If b.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New Exception("Unexpected database.")
        connection = b.ConnectionString
        Dim tables = {"Agents_Balance_MV", "ST_QTY_Deferences", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change", "Network_Edit_Tracker", "SB_Contents"}
        Dim counts As Long(), domains As String
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND database_id=DB_ID() AND session_id<>@@SPID")) = 0, "no other clients during rollback tests")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=" & Doc)) = 0 AndAlso CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.ST_QTY_Deferences WHERE T_ID=" & Seed)) = 0, "fixture identities do not exist")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.SB_Contents WHERE T_ID=" & CompetingSale)) = 0 AndAlso CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=4 AND BsType_ID=1")) = 1, "competing sale fixture references verified")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE parent_id IN(OBJECT_ID('dbo.Agents_Balance_MV'),OBJECT_ID('dbo.ST_QTY_Deferences'),OBJECT_ID('dbo.SB_Contents')) AND is_disabled=0")) = 0, "no unreviewed fixture triggers")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Users u JOIN dbo.Users_Validations_V v ON v.User_ID=u.User_ID WHERE u.User_ID=1 AND u.is_Allow=1 AND v.Update_IM_QTY=1 AND v.Pch_Update=1")) = 1, "existing test actor authorized")
            context = New AuditEventContext With {.SourceId = CType(Scalar(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid), .ActorKind = AuditActorKind.User, .ActorUserId = 1,
                .ActorName = "Audit3D test", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "3D", .ScreenCode = "ST_settlement"}
            Using cmd As New SqlCommand("SELECT TOP(1) v.IM_ID,v.ST_ID,u.U_ID FROM dbo.IM_STORE_View v JOIN dbo.IM_Menu_Units_V u ON u.IM_ID=v.IM_ID AND u.is_Default=1 JOIN dbo.IM_Menu i ON i.IM_ID=v.IM_ID AND i.isStore IN(1,2) WHERE u.U_Cargo=1 ORDER BY CASE WHEN v.Cost>0 THEN 0 ELSE 1 END,v.IM_ID", cn)
                Using rd = cmd.ExecuteReader()
                    If Not rd.Read() Then Throw New Exception("No default-cargo-one test item.")
                    itemId = CInt(rd(0)) : storeId = CInt(rd(1)) : unitId = CInt(rd(2))
                End Using
            End Using
            Check(Balance(cn, Nothing) = 0R, "isolated expiry stock key empty")
            counts = tables.Select(Function(t) CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & t))).ToArray()
            domains = CStr(Scalar(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1"))
        End Using
        Check(AuditSettlementLines.IsEnabled(connection), "008 gate installed and enabled")

        ' The screens no longer retain a legacy write path when the gate is disabled.
        For Each action In {"Created", "HeaderSaved", "Approved", "EditOpened", "EditFinished", "Voided"}
            Dim operation = action
            Fixture(Sub(cn, tx)
                        Dim request = AuditSettlementHeader.Request(context, operation, If(operation = "Created", 0, Doc), New DateTime(2026, 10, 26), "Audit3D", "Rollback fixture", False)
                        Scalar(cn, tx, "UPDATE Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code=""Inventory.Settlement""]') WHERE ConfigId=1")
                        Dim disabledBalance = Balance(cn, tx)
                        Reject(Sub() AuditSettlementHeader.ExecuteInTransaction(cn, tx, request), "disabled gate blocks header " & operation, "Audit domain is disabled")
                        Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM Network_Audit_Request WHERE RequestId='" & request.RequestId.ToString() & "'")) = 0, "disabled header leaves no request " & operation)
                        Check(RejectedStockUnchanged(cn, tx, disabledBalance), "disabled header leaves no stock change " & operation)
                    End Sub)
        Next
        For Each action In {"Add", "Update", "Delete"}
            Dim operation = action
            Fixture(Sub(cn, tx)
                        Dim request As AuditOperationRequest
                        Select Case operation
                            Case "Add" : request = Add(10D, 8R)
                            Case "Update" : request = AuditSettlementLines.UpdateRequest(context, Doc, Seed, 9D, AuditSettlementLines.LineSnapshot(SavedLine(cn, tx, Seed)), False)
                            Case Else : request = AuditSettlementLines.DeleteRequest(context, Doc, Seed)
                        End Select
                        Scalar(cn, tx, "UPDATE Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code=""Inventory.Settlement""]') WHERE ConfigId=1")
                        Dim disabledBalance = Balance(cn, tx)
                        Reject(Sub() AuditSettlementLines.ExecuteInTransaction(cn, tx, request), "disabled gate blocks line " & operation, "Audit domain is disabled")
                        Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM Network_Audit_Request WHERE RequestId='" & request.RequestId.ToString() & "'")) = 0, "disabled line leaves no request " & operation)
                        Check(RejectedStockUnchanged(cn, tx, disabledBalance), "disabled line leaves no stock change " & operation)
                    End Sub)
        Next
        Fixture(Sub(cn, tx)
                    Dim request = Add(10D, 8R), result = AuditSettlementLines.ExecuteInTransaction(cn, tx, request)
                    Scalar(cn, tx, "UPDATE Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code=""Inventory.Settlement""]') WHERE ConfigId=1")
                    Dim disabledBalance = Balance(cn, tx)
                    Check(Id(AuditSettlementLines.ExecuteInTransaction(cn, tx, request)) = Id(result) AndAlso Balance(cn, tx) = disabledBalance AndAlso CDbl(SavedLine(cn, tx, Id(result))("ST_QTY")) = 2R, "completed request resolves after gate disabled without duplicate stock")
                End Sub)

        Fixture(Sub(cn, tx)
                    Check(Not CompetingSaleIsBlocked(), "control: fixture alone does not block an unrelated sales writer")
                    AuditSettlementLines.ExecuteInTransaction(cn, tx, Add(10D, 8R))
                    Check(CompetingSaleIsBlocked(), "stock locks block concurrent legacy sales until settlement ends")
                End Sub)

        Fixture(Sub(cn, tx)
                    Dim request = Add(10D, 8R)
                    Dim result = AuditSettlementLines.ExecuteInTransaction(cn, tx, request)
                    Dim lineId = Id(result)
                    Check(Balance(cn, tx) = 10R, "add reaches requested stock immediately")
                    Check(Id(AuditSettlementLines.ExecuteInTransaction(cn, tx, request)) = lineId AndAlso Balance(cn, tx) = 10R, "same request never adds twice")
                    Dim old = SavedLine(cn, tx, lineId)
                    Dim update = AuditSettlementLines.UpdateRequest(context, Doc, lineId, 3D, AuditSettlementLines.LineSnapshot(old), False)
                    AuditSettlementLines.ExecuteInTransaction(cn, tx, update)
                    Check(Balance(cn, tx) = 11R, "line update applies only delta from old line")
                    Dim changed = SavedLine(cn, tx, lineId)
                    Check(CDbl(changed("ST_QTY")) = 3R AndAlso CDec(changed("Cost")) = CDec(old("Cost")) AndAlso CStr(changed("Valid_D")) = CStr(old("Valid_D")), "edit preserves cost and expiry")
                    Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON c.EventId=e.EventId WHERE e.RequestId='" & update.RequestId.ToString() & "' AND c.EntityType='SettlementLine' AND c.BeforeState IN(2,3) AND c.AfterState IN(2,3)")) = 9, "updated event captures all nine before and after fields")
                    Check(AuditSettlementLines.ExecuteInTransaction(cn, tx, update).Status = "Completed" AndAlso Balance(cn, tx) = 11R, "update replay is idempotent")
                    Check(AuditSettlementLines.ExecuteInTransaction(cn, tx, AuditSettlementLines.UpdateRequest(context, Doc, lineId, 3D, AuditSettlementLines.LineSnapshot(changed), False)).ResultCode.GetValueOrDefault() = 5, "unchanged edit produces no false change")
                    AuditSettlementLines.ExecuteInTransaction(cn, tx, AuditSettlementLines.DeleteRequest(context, Doc, lineId))
                    Check(Balance(cn, tx) = 8R, "delete removes only line effect")
                End Sub)
        Fixture(Sub(cn, tx)
                    AuditSettlementLines.ExecuteInTransaction(cn, tx, Add(6D, 8R))
                    Check(Balance(cn, tx) = 6R, "negative difference reduces stock")
                    Check(AuditSettlementLines.ExecuteInTransaction(cn, tx, Add(6D, 6R)).ResultCode.GetValueOrDefault() = 5, "unchanged current quantity is NoChange")
                End Sub)
        Fixture(Sub(cn, tx)
                    Reject(Sub() AuditSettlementLines.ExecuteInTransaction(cn, tx, Add(12D, 10R)), "live stale balance rejected", "تغير رصيد")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim r = SavedLine(cn, tx, Seed)
                    Dim request = AuditSettlementLines.UpdateRequest(context, Doc, Seed, 7D, AuditSettlementLines.LineSnapshot(r), False)
                    Scalar(cn, tx, "UPDATE dbo.ST_QTY_Deferences SET Cost=8 WHERE T_ID=" & Seed)
                    Reject(Sub() AuditSettlementLines.ExecuteInTransaction(cn, tx, request), "stale line including cost rejected", "تغير السطر")
                End Sub)
        Fixture(Sub(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.Users SET is_Allow=0 WHERE User_ID=1")
                    Reject(Sub() AuditSettlementLines.ExecuteInTransaction(cn, tx, Add(10D, 8R)), "blocked actor checked during transaction", "صلاحية")
                End Sub)
        Fixture(Sub(cn, tx)
                    Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isVoid=1 WHERE T_ID=" & Doc)
                    Reject(Sub() AuditSettlementLines.ExecuteInTransaction(cn, tx, Add(10D, 8R)), "voided document cannot accept a pending line", "ملغى")
                End Sub)
        Fixture(Sub(cn, tx)
                    Header(cn, tx, "Approved")
                    Reject(Sub() AuditSettlementLines.ExecuteInTransaction(cn, tx, Add(10D, 8R)), "approval while dialog open requires explicit edit", "افتح تعديل")
                End Sub)
        Fixture(Sub(cn, tx)
                    Header(cn, tx, "Approved") : Header(cn, tx, "EditOpened")
                    Dim lineId = Id(AuditSettlementLines.ExecuteInTransaction(cn, tx, Add(10D, 8R)))
                    Check(Balance(cn, tx) = 10R, "approved document allows active authorized editor")
                    Header(cn, tx, "EditFinished")
                    Reject(Sub() AuditSettlementLines.ExecuteInTransaction(cn, tx, AuditSettlementLines.DeleteRequest(context, Doc, lineId)), "finished edit cannot use stale accounting edit metadata", "افتح تعديل")
                End Sub)
        Fixture(Sub(cn, tx)
                    Header(cn, tx, "Approved") : Header(cn, tx, "EditOpened")
                    Dim otherSession = Add(10D, 8R)
                    otherSession.Context.SessionId = Guid.NewGuid()
                    Reject(Sub() AuditSettlementLines.ExecuteInTransaction(cn, tx, otherSession), "same user different login session cannot reuse edit", "افتح تعديل")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim old = SavedLine(cn, tx, Seed)
                    Reject(Sub() AuditSettlementLines.ExecuteInTransaction(cn, tx, AuditSettlementLines.UpdateRequest(context, Doc, Seed, -1D, AuditSettlementLines.LineSnapshot(old), False)), "negative stock edit blocked", "سالبة")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim old = SavedLine(cn, tx, Seed)
                    AuditSettlementLines.ExecuteInTransaction(cn, tx, AuditSettlementLines.UpdateRequest(context, Doc, Seed, 0D, AuditSettlementLines.LineSnapshot(old), False))
                    Check(Balance(cn, tx) = 0R AndAlso CDec(SavedLine(cn, tx, Seed)("Cost")) = 7D, "zero difference edit preserves line identity and cost")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim old = SavedLine(cn, tx, Seed)
                    AuditSettlementLines.ExecuteInTransaction(cn, tx, AuditSettlementLines.UpdateRequest(context, Doc, Seed, -1D, AuditSettlementLines.LineSnapshot(old), True))
                    Check(Balance(cn, tx) = -1R, "explicit existing negative-stock policy is respected")
                End Sub)
        Fixture(Sub(cn, tx)
                    Scalar(cn, tx, "CREATE TRIGGER dbo.AuditTest3DReject ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN THROW 51106,'Intentional Audit3D failure',1; END")
                    Reject(Sub() AuditSettlementLines.ExecuteInTransaction(cn, tx, Add(10D, 8R)), "audit failure rolls back stock with operation", "Intentional Audit3D")
                End Sub)
        Fixture(Sub(cn, tx)
                    Dim old = SavedLine(cn, tx, Seed)
                    Dim request = AuditSettlementLines.UpdateRequest(context, Doc, Seed, 9D, AuditSettlementLines.LineSnapshot(old), False)
                    Scalar(cn, tx, "CREATE TRIGGER dbo.AuditTest3DReject ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN THROW 51106,'Intentional Audit3D failure',1; END")
                    Reject(Sub() AuditSettlementLines.ExecuteInTransaction(cn, tx, request), "audit failure also rolls back line update", "Intentional Audit3D")
                End Sub)
        Using cn As New SqlConnection(connection)
            cn.Open()
            For i = 0 To tables.Length - 1
                Check(CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & tables(i))) = counts(i), "rollback restored " & tables(i))
            Next
            Check(Balance(cn, Nothing) = 0R, "fixture stock fully rolled back")
            Check(CStr(Scalar(cn, Nothing, "SELECT CONVERT(nvarchar(max),EnabledDomainsXml) FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = domains, "activation unchanged")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE name='AuditTest3DReject'")) = 0, "failure trigger rolled back")
            Check(CInt(Scalar(cn, Nothing, "SELECT is_Allow FROM dbo.Users WHERE User_ID=1")) = 1, "actor activation restored")
        End Using
    End Sub
End Module
