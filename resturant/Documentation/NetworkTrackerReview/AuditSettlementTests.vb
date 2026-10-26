Option Strict On
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks

Friend Module AuditSettlementTests
    Private checks As Integer
    Private source As Guid = Guid.NewGuid()
    Private itemId As Integer, storeId As Integer, unitId As Integer, barcode As String
    Private Const DocumentId As Integer = 2147483000
    Private Const OtherDocumentId As Integer = 2147483001

    Public Function Main(args As String()) As Integer
        Try
            UnitTests()
            If args.Length = 2 AndAlso args(0) = "--settings" Then LiveTests(args(1))
            If args.Length = 2 AndAlso args(0) = "--read-only-policy" Then ReadOnlyPolicyTests(args(1))
            Console.WriteLine("PASS: " & checks & " settlement checks.")
            Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.GetType().Name & ": " & ex.Message & Environment.NewLine & ex.StackTrace)
            Return 1
        End Try
    End Function

    Private Sub Check(ok As Boolean, name As String)
        If Not ok Then Throw New Exception("FAILED: " & name)
        checks += 1
        Console.WriteLine("PASS " & name)
    End Sub

    Private Sub Reject(action As Action, name As String, Optional expectedText As String = Nothing)
        Try
            action()
        Catch ex As Exception When TypeOf ex Is ArgumentException OrElse TypeOf ex Is InvalidOperationException OrElse TypeOf ex Is SqlException
            If expectedText IsNot Nothing AndAlso Not ex.Message.Contains(expectedText) Then Throw New Exception("Unexpected rejection in " & name, ex)
            Check(True, name)
            Return
        End Try
        Throw New Exception("Expected rejection: " & name)
    End Sub

    Private Function Context() As AuditEventContext
        Return New AuditEventContext With {.SourceId = source, .ActorKind = AuditActorKind.User, .ActorUserId = 2147483645,
            .ActorName = "AuditTest settlement", .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "3B", .ScreenCode = "ST_settlement"}
    End Function

    Private Function AddOperation(Optional difference As Decimal = 2D, Optional doc As Integer = DocumentId) As AuditOperationRequest
        Return AuditSettlementLines.AddRequest(Context(), doc, itemId, storeId, unitId, barcode, 10D, difference, 8R, Nothing)
    End Function

    Private Sub UnitTests()
        Check(AuditSettlementLines.ReadEnabledFlag(CObj(True)), "boxed SQL bit true enables settlement audit")
        Check(Not AuditSettlementLines.ReadEnabledFlag(CObj(False)), "boxed SQL bit false keeps settlement audit disabled")
        Reject(Sub() AuditSettlementLines.ReadEnabledFlag(Nothing), "missing activation row cannot fall back to legacy")
        Reject(Sub() AuditSettlementLines.ReadEnabledFlag(DBNull.Value), "NULL activation cannot fall back to legacy")
        Reject(Sub() AuditSettlementLines.ReadEnabledFlag(1), "unexpected numeric activation type rejected")
        Reject(Sub() AuditSettlementLines.ReadEnabledFlag("True"), "unexpected text activation type rejected")
        itemId = 1 : storeId = 1 : unitId = 1 : barcode = "test"
        Dim request = AddOperation()
        Dim bytes = AuditCanonical.RequestBytes(request)
        Check(bytes.Length > 0 AndAlso request.EntityId = DocumentId.ToString(CultureInfo.InvariantCulture), "request carries explicit document identity")
        Dim oldCulture = Thread.CurrentThread.CurrentCulture
        Try
            Thread.CurrentThread.CurrentCulture = New CultureInfo("ar-LY")
            Check(bytes.SequenceEqual(AuditCanonical.RequestBytes(request)), "settlement inputs are culture invariant")
        Finally
            Thread.CurrentThread.CurrentCulture = oldCulture
        End Try
        Reject(Sub() AuditSettlementLines.AddRequest(Context(), 0, 1, 1, 1, "", 1D, 1D, 0R, Nothing), "missing document rejected")
        Reject(Sub() AddOperation(1.1234D), "fractional precision is not silently rounded")
        Reject(Sub() AuditSettlementLines.AddRequest(Context(), 1, 1, 1, 1, New String("x"c, 51), 1D, 1D, 0R, Nothing), "oversized barcode rejected")
        Dim deleteOperation = AuditSettlementLines.DeleteRequest(Context(), DocumentId, 99)
        Check(deleteOperation.OperationCode = "Settlement.LineDeleted" AndAlso deleteOperation.Inputs.Single(Function(x) x.Name = "LineId").Value.Text = "99", "delete carries exact line and document")
        Dim changedContext = Context()
        changedContext.SessionId = Nothing
        Reject(Sub() AuditSettlementLines.DeleteRequest(changedContext, DocumentId, 99), "confirmed session required for settlement")
        Check(request.Inputs.Single(Function(x) x.Name = "ExpiryDate").Value.State = AuditValueState.SqlNull, "absent expiry remains SQL NULL input")
        Check(AuditContract.FieldType("Settlement.LineAdded", "ExpiryText") = "String" AndAlso AuditContract.FieldType("Settlement.LineAdded", "StoredQuantity") = "Float", "stored expiry and float quantity retain actual SQL types")
    End Sub

    ' Safe while the pilot is active: SELECT only, no domain toggles or business fixtures.
    Private Sub ReadOnlyPolicyTests(settings As String)
        Dim line = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim b As New SqlConnectionStringBuilder(line.Substring("SqlConStr:".Length)) With {.ApplicationName = "CPOS Audit policy read-only tests", .ConnectTimeout = 5}
        If b.InitialCatalog <> "CPOS_ACCOUNTING" Then Throw New InvalidOperationException("Read-only policy checks require CPOS_ACCOUNTING.")
        Using cn As New SqlConnection(b.ConnectionString)
            cn.Open()
            Check(CStr(Scalar(cn, Nothing, "SELECT DB_NAME()")) = "CPOS_ACCOUNTING", "read-only policy database verified")
            Dim sqlTrue = Scalar(cn, Nothing, "SELECT CAST(N'<Domains><Domain code=""Inventory.Settlement""/></Domains>' AS xml).exist('/Domains/Domain[@code=""Inventory.Settlement""]')")
            Dim sqlFalse = Scalar(cn, Nothing, "SELECT CAST(N'<Domains/>' AS xml).exist('/Domains/Domain[@code=""Inventory.Settlement""]')")
            Check(AuditSettlementLines.ReadEnabledFlag(sqlTrue), "real SQL XML enabled result decoded correctly")
            Check(Not AuditSettlementLines.ReadEnabledFlag(sqlFalse), "real SQL XML disabled result decoded correctly")
            Reject(Sub() AuditSettlementLines.ReadEnabledFlag(Scalar(cn, Nothing, "SELECT CAST(NULL AS bit)")), "real SQL NULL result rejected")
            Reject(Sub() AuditSettlementLines.ReadEnabledFlag(Scalar(cn, Nothing, "SELECT CAST(1 AS bit) WHERE 1=0")), "real SQL missing row rejected")
            Dim flag = Scalar(cn, Nothing, "SELECT EnabledDomainsXml.exist('/Domains/Domain[@code=""Inventory.Settlement""]') FROM dbo.Network_Audit_Source WHERE ConfigId=1")
            Check(TypeOf flag Is Boolean, "SQL XML exist returns a boxed Boolean")
            Check(AuditSettlementLines.IsEnabled(b.ConnectionString) = DirectCast(flag, Boolean), "application gate matches actual SQL settlement flag")
        End Using
    End Sub

    Private Function Scalar(cn As SqlConnection, tx As SqlTransaction, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn, tx)
            Return cmd.ExecuteScalar()
        End Using
    End Function

    Private Sub Fixture(cn As SqlConnection, tx As SqlTransaction, doc As Integer)
        Using cmd As New SqlCommand("IF EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV WHERE T_ID=@Id) THROW 51106,'Fixture ID already exists; do not overwrite.',1; INSERT dbo.Agents_Balance_MV(T_ID,AG_ID,[Date],BsType_ID,isDepended,isVoid,User_ID,Total,Discount,Pure,ST_Sett_ID) VALUES(@Id,(SELECT MIN(AG_ID) FROM dbo.Agents),GETDATE(),39,0,0,1,0,0,0,@Id);", cn, tx)
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = doc
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub EnableInTransaction(cn As SqlConnection, tx As SqlTransaction)
        Scalar(cn, tx, "UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml=N'<Domains><Domain code=""Inventory.Settlement""/></Domains>' WHERE ConfigId=1")
    End Sub

    Private Function LineId(result As AuditRequestResult) As Integer
        Return Integer.Parse(AuditCanonical.ParseXml(result.ResultXml).Element("Values").Elements("Value").Single(Function(x) CStr(x.Attribute("name")) = "LineId").Value, CultureInfo.InvariantCulture)
    End Function

    Private Sub LiveTests(settings As String)
        Dim line = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim b As New SqlConnectionStringBuilder(line.Substring("SqlConStr:".Length)) With {.InitialCatalog = "CPOS_ACCOUNTING", .ApplicationName = "CPOS Audit3B rollback tests", .ConnectTimeout = 5}
        Using cn As New SqlConnection(b.ConnectionString)
            cn.Open()
            Check(CStr(Scalar(cn, Nothing, "SELECT DB_NAME()")) = "CPOS_ACCOUNTING", "approved database verified")
            If CInt(Scalar(cn, Nothing, "SELECT CASE WHEN OBJECT_ID('dbo.ST_Settlement_Line_AssertWritable','P') IS NULL THEN 0 ELSE 1 END")) = 1 Then
                Throw New InvalidOperationException("After upgrade 008 use Test_Phase3D.ps1 -LiveRollbackTests. Historical 3B fixtures use a synthetic unauthorized actor and an assumed stock balance; read-only/unit 3B checks remain valid.")
            End If
            Check(CInt(Scalar(cn, Nothing, "SELECT EnabledDomainsXml.value('count(/Domains/*)','int') FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = 0, "precondition all domains disabled")
            source = CType(Scalar(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid)
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.parameters WHERE object_id=OBJECT_ID('dbo.ST_Balance_Update') AND name='@Inserted_T_ID' AND is_output=1")) = 1, "reliable inserted ID output installed")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE parent_id IN(OBJECT_ID('dbo.Agents_Balance_MV'),OBJECT_ID('dbo.ST_QTY_Deferences')) AND is_disabled=0")) = 0, "no unreviewed business triggers on fixture tables")
            Using cmd As New SqlCommand("SELECT TOP(1) v.IM_ID,v.ST_ID,u.U_ID,ISNULL(v.Barcode,N'') Barcode FROM dbo.IM_STORE_View v JOIN dbo.IM_Menu_Units_V u ON u.IM_ID=v.IM_ID AND u.is_Default=1 WHERE v.is_Default=1 AND u.U_Cargo>0 AND LEN(ISNULL(v.Barcode,N''))<=50 AND (SELECT COUNT(*) FROM dbo.IM_Menu_Units_V z WHERE z.IM_ID=v.IM_ID AND z.is_Default=1)=1 ORDER BY CASE WHEN v.Cost>0 THEN 0 ELSE 1 END,v.IM_ID,v.ST_ID", cn)
                Using reader = cmd.ExecuteReader()
                    If Not reader.Read() Then Throw New InvalidOperationException("No valid sample item/default unit; no business test executed.")
                    itemId = CInt(reader("IM_ID")) : storeId = CInt(reader("ST_ID")) : unitId = CInt(reader("U_ID")) : barcode = CStr(reader("Barcode"))
                End Using
            End Using
            Dim priorLines = CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.ST_QTY_Deferences"))
            Dim priorHeads = CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.Agents_Balance_MV"))
            Dim priorLegacy = CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.Network_Edit_Tracker"))
            Dim priorEvents = CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Event"))
            Dim priorRequests = CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Request"))
            Dim priorChanges = CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Change"))
            Using tx = cn.BeginTransaction()
                EnableInTransaction(cn, tx)
                Fixture(cn, tx, DocumentId)
                Fixture(cn, tx, OtherDocumentId)
                Dim add = AddOperation()
                Dim result = AuditSettlementLines.ExecuteInTransaction(cn, tx, add)
                Dim inserted = LineId(result)
                Check(result.Status = "NewPendingCommit" AndAlso result.ResultCode.GetValueOrDefault() = 1, "business and audit still pending caller commit")
                Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.ST_QTY_Deferences WHERE T_ID=" & inserted & " AND Bill_T_ID=" & DocumentId)) = 1, "exact returned ID points to inserted fixture line")
                Check(CDec(Scalar(cn, tx, "SELECT Total FROM dbo.Agents_Balance_MV WHERE T_ID=" & DocumentId)) = CDec(Scalar(cn, tx, "SELECT TOTAL FROM dbo.ST_QTY_Deferences WHERE T_ID=" & inserted)), "header recalculated from saved line")
                Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & add.RequestId.ToString("D") & "' AND c.EntityType='SettlementLine' AND c.BeforeState=1 AND c.AfterState=3")) = 9, "all nine saved line values captured with absent before")
                Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & add.RequestId.ToString("D") & "' AND c.ValueOrigin=2")) = 4, "display/input values labeled input not stock balance")
                Dim replay = AuditSettlementLines.ExecuteInTransaction(cn, tx, add)
                Check(replay.Status = "Completed" AndAlso LineId(replay) = inserted, "same request replays same line result within caller transaction")
                Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.ST_QTY_Deferences WHERE Bill_T_ID=" & DocumentId)) = 1, "replay does not insert a second line")
                Dim zero = AddOperation(0D)
                Check(AuditSettlementLines.ExecuteInTransaction(cn, tx, zero).ResultCode.GetValueOrDefault() = 5, "zero difference is NoChange")
                Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & zero.RequestId.ToString("D") & "'")) = 0, "NoChange has no fictitious event")
                Dim deletion = AuditSettlementLines.DeleteRequest(Context(), DocumentId, inserted)
                Check(AuditSettlementLines.ExecuteInTransaction(cn, tx, deletion).ResultCode.GetValueOrDefault() = 1, "delete and audit succeed in same transaction")
                Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.ST_QTY_Deferences WHERE T_ID=" & inserted)) = 0, "exact fixture line deleted")
                Check(CDec(Scalar(cn, tx, "SELECT Total FROM dbo.Agents_Balance_MV WHERE T_ID=" & DocumentId)) = 0D, "delete recalculates correct header instead of NULL document")
                Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & deletion.RequestId.ToString("D") & "' AND c.EntityType='SettlementLine' AND c.BeforeState=3 AND c.AfterState=1")) = 9, "deleted line snapshot retained")
                Check(AuditSettlementLines.ExecuteInTransaction(cn, tx, deletion).Status = "Completed", "delete replay does not repeat mutation")
                Check(AuditSettlementLines.ExecuteInTransaction(cn, tx, AuditSettlementLines.DeleteRequest(Context(), DocumentId, inserted)).ResultCode.GetValueOrDefault() = 5, "already absent line gives NoChange")
                tx.Rollback()
            End Using
            FailureTests(cn)
            NegativeDifferenceTest(cn)
            AllocationLockTest(cn, b.ConnectionString)
            Check(CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.ST_QTY_Deferences")) = priorLines, "all fixture line changes rolled back")
            Check(CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.Agents_Balance_MV")) = priorHeads, "all fixture headers rolled back")
            Check(CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.Network_Edit_Tracker")) = priorLegacy, "legacy tracker untouched by V2")
            Check(CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Event")) = priorEvents, "fixture audit events rolled back")
            Check(CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Request")) = priorRequests, "fixture requests rolled back")
            Check(CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo.Network_Audit_Change")) = priorChanges, "fixture field changes rolled back")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE name='AuditTest_3B_Reject'")) = 0, "forced-failure trigger rolled back")
            Check(CInt(Scalar(cn, Nothing, "SELECT EnabledDomainsXml.value('count(/Domains/*)','int') FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = 0, "activation rolled back to disabled")
        End Using
    End Sub

    Private Sub FailureTests(cn As SqlConnection)
        For Each scenario In {"WrongParent", "WrongUnit", "Void", "AuditFailure"}
            Using tx = cn.BeginTransaction()
                EnableInTransaction(cn, tx)
                Fixture(cn, tx, DocumentId)
                Fixture(cn, tx, OtherDocumentId)
                If scenario = "WrongParent" Then
                    Dim line = LineId(AuditSettlementLines.ExecuteInTransaction(cn, tx, AddOperation()))
                    Reject(Sub() AuditSettlementLines.ExecuteInTransaction(cn, tx, AuditSettlementLines.DeleteRequest(Context(), OtherDocumentId, line)), "cross-document deletion rejected")
                ElseIf scenario = "WrongUnit" Then
                    Dim request = AddOperation()
                    request.Inputs.Single(Function(x) x.Name = "UnitId").Value = AuditValue.Capture("Integer", Integer.MaxValue)
                    Reject(Sub() AuditSettlementLines.ExecuteInTransaction(cn, tx, request), "nondefault unit cannot silently use wrong conversion")
                ElseIf scenario = "Void" Then
                    Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET isVoid=1 WHERE T_ID=" & DocumentId)
                    Reject(Sub() AuditSettlementLines.ExecuteInTransaction(cn, tx, AddOperation()), "void settlement cannot receive lines")
                Else
                    ' SQL trigger on audit only, installed inside this rollback transaction to force real writer failure AFTER business insertion.
                    Scalar(cn, tx, "CREATE TRIGGER dbo.AuditTest_3B_Reject ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN THROW 51106,'Intentional audit fixture failure.',1; END")
                    Reject(Sub() AuditSettlementLines.ExecuteInTransaction(cn, tx, AddOperation()), "actual SQL audit failure prevents successful business result", "Intentional audit fixture failure.")
                End If
                If tx.Connection IsNot Nothing Then tx.Rollback()
            End Using
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID IN(" & DocumentId & "," & OtherDocumentId & ")")) = 0, "failure rollback removes fixture work " & scenario)
        Next
    End Sub

    Private Sub NegativeDifferenceTest(cn As SqlConnection)
        Using tx = cn.BeginTransaction()
            EnableInTransaction(cn, tx)
            Fixture(cn, tx, DocumentId)
            Scalar(cn, tx, "UPDATE dbo.Agents_Balance_MV SET Discount=1.250 WHERE T_ID=" & DocumentId)
            Dim request = AddOperation(-1.25D)
            request.Inputs.Single(Function(x) x.Name = "RequestedQuantity").Value = AuditValue.Capture("Decimal", 6.75D)
            request.Inputs.Single(Function(x) x.Name = "ExpiryDate").Value = AuditValue.Capture("DateTime", New DateTime(2026, 12, 31))
            Dim inserted = LineId(AuditSettlementLines.ExecuteInTransaction(cn, tx, request))
            Check(CDbl(Scalar(cn, tx, "SELECT ST_QTY FROM dbo.ST_QTY_Deferences WHERE T_ID=" & inserted)) < 0R, "negative fractional difference retains stored sign")
            Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=" & DocumentId & " AND Pure=Total-Discount AND Discount=1.250")) = 1, "recalculation preserves header discount")
            Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId JOIN dbo.ST_QTY_Deferences l ON l.T_ID=" & inserted & " WHERE e.RequestId='" & request.RequestId.ToString("D") & "' AND c.FieldCode='ExpiryText' AND c.AfterValue=l.Valid_D AND LEN(l.Valid_D)>0")) = 1, "expiry audit captures exact stored text without date reinterpretation")
            Dim notes = CStr(Scalar(cn, tx, "SELECT Notes FROM dbo.Network_Audit_Event WHERE RequestId='" & request.RequestId.ToString("D") & "'"))
            Check(notes.Contains("رقم الصنف " & itemId.ToString(CultureInfo.InvariantCulture)) AndAlso notes.Contains("التكلفة المحفوظة") AndAlso notes.Contains("إجمالي المستند من"), "Arabic event describes actual line and header values")
            tx.Rollback()
        End Using
    End Sub

    Private Sub AllocationLockTest(cn As SqlConnection, connection As String)
        Using tx = cn.BeginTransaction()
            Fixture(cn, tx, DocumentId)
            ' Legacy signature still works without receiving the new optional output.
            Using cmd As New SqlCommand("dbo.ST_Balance_Update", cn, tx)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@T_ID", 0)
                cmd.Parameters.AddWithValue("@Bill_T_ID", DocumentId)
                cmd.Parameters.AddWithValue("@IM_ID", itemId)
                cmd.Parameters.AddWithValue("@ST_ID", storeId)
                cmd.Parameters.AddWithValue("@ST_QTY", 10D)
                cmd.Parameters.AddWithValue("@Qty_Deference", 2D)
                cmd.Parameters.AddWithValue("@Tag", "+")
                cmd.Parameters.AddWithValue("@Barcode", barcode)
                cmd.ExecuteNonQuery()
            End Using
            Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.ST_QTY_Deferences WHERE Bill_T_ID=" & DocumentId)) = 1, "legacy call signature retained")
            Dim ready As New ManualResetEventSlim(False)
            Dim competing = Task.Run(Function()
                Using other As New SqlConnection(connection)
                    other.Open()
                    Using otherTx = other.BeginTransaction()
                        ready.Set()
                        Try
                            Using cmd As New SqlCommand("SET LOCK_TIMEOUT 750; SELECT TOP(1) T_ID FROM dbo.ST_QTY_Deferences WITH(TABLOCKX,HOLDLOCK)", other, otherTx)
                                cmd.ExecuteScalar()
                            End Using
                            Return False
                        Catch ex As SqlException When ex.Number = 1222
                            Return True
                        Finally
                            otherTx.Rollback()
                        End Try
                    End Using
                End Using
            End Function)
            Check(ready.Wait(3000) AndAlso competing.Wait(5000) AndAlso competing.Result, "table allocation lock excludes concurrent allocator until transaction ends")
            tx.Rollback()
        End Using
    End Sub
End Module
