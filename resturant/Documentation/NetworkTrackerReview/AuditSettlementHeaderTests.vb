Option Strict On
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Linq

Friend Module AuditSettlementHeaderTests
    Private checks As Integer
    Private source As Guid = Guid.NewGuid()
    Private itemId As Integer, storeId As Integer
    Private Const FirstLine As Integer = 2147482900
    Private Const Expiry As String = "2088-12-31"
    Private ReadOnly DocumentDate As New DateTime(2026, 10, 24, 10, 15, 30)

    Public Function Main(args As String()) As Integer
        Try
            Dim r = Request("Created", 0)
            Check(r.EntityId = "new:" & r.RequestId.ToString("D"), "new document has stable pre-allocation identity")
            Reject(Sub() Request("Created", 1), "existing ID cannot be recreated")
            Reject(Sub() Request("Voided", 0), "void requires document ID")
            Reject(Sub() AuditSettlementHeader.Request(Context(), "HeaderSaved", 1, DocumentDate, New String("x"c, 351), "", False), "oversized title rejected without truncation")
            Reject(Sub() AuditSettlementHeader.Request(Context(), "HeaderSaved", 1, DocumentDate, "", New String("x"c, 4097), False), "oversized notes rejected without truncation")
            Check(AuditContract.FieldType("Settlement.EditOpened", "NeedRepost") = "Boolean", "accounting edit flags are typed")
            Check(Request("Voided", 1).Inputs.Single().Name = "AllowNegative", "void freezes machine negative-stock policy in request")
            If args.Length = 2 AndAlso args(0) = "--settings" Then Live(args(1))
            Console.WriteLine("PASS: " & checks & " header/stock checks.")
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

    Private Sub Reject(action As Action, name As String, Optional expected As String = Nothing)
        Try
            action()
        Catch ex As Exception
            If expected IsNot Nothing AndAlso Not ex.Message.Contains(expected) Then Throw New Exception("Wrong failure: " & name, ex)
            Check(True, name) : Return
        End Try
        Throw New Exception("Expected rejection: " & name)
    End Sub

    Private Function Context() As AuditEventContext
        Return New AuditEventContext With {.SourceId = source, .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "AuditTest 3C",
            .SessionId = Guid.NewGuid(), .MachineName = "AuditTests", .ApplicationVersion = "3C", .ScreenCode = "ST_settlement"}
    End Function

    Private Function Request(action As String, id As Integer, Optional title As String = "اختبار رأس التسوية") As AuditOperationRequest
        Return AuditSettlementHeader.Request(Context(), action, id, DocumentDate, title, "اختبار داخل Rollback", False)
    End Function

    Private Function Scalar(cn As SqlConnection, tx As SqlTransaction, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn, tx)
            Return cmd.ExecuteScalar()
        End Using
    End Function

    Private Sub Enable(cn As SqlConnection, tx As SqlTransaction)
        Scalar(cn, tx, "UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml=N'<Domains><Domain code=""Inventory.Settlement""/></Domains>' WHERE ConfigId=1")
    End Sub

    Private Function Apply(cn As SqlConnection, tx As SqlTransaction, action As String, id As Integer) As AuditRequestResult
        Return AuditSettlementHeader.ExecuteInTransaction(cn, tx, Request(action, id))
    End Function

    Private Sub Line(cn As SqlConnection, tx As SqlTransaction, id As Integer, doc As Integer, qty As Double)
        Using cmd As New SqlCommand("IF EXISTS(SELECT 1 FROM dbo.ST_QTY_Deferences WHERE T_ID=@Id) THROW 51106,'Fixture line collision.',1; INSERT dbo.ST_QTY_Deferences(T_ID,Bill_T_ID,[DATE],IM_ID,ST_ID,Valid_D,ST_QTY,Cost,U_Cargo,Barcode,TOTAL) VALUES(@Id,@Doc,GETDATE(),@Item,@Store,@Expiry,@Qty,7,1,N'AuditTest3C',@Qty*7)", cn, tx)
            cmd.Parameters.AddWithValue("@Id", id) : cmd.Parameters.AddWithValue("@Doc", doc)
            cmd.Parameters.AddWithValue("@Item", itemId) : cmd.Parameters.AddWithValue("@Store", storeId)
            cmd.Parameters.AddWithValue("@Expiry", Expiry) : cmd.Parameters.AddWithValue("@Qty", qty)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Function Stock(cn As SqlConnection, tx As SqlTransaction) As Double
        Return CDbl(Scalar(cn, tx, "SELECT ISNULL(SUM(QTY),0) FROM dbo.ST_Balance_V WHERE IM_ID=" & itemId & " AND ST_ID=" & storeId & " AND D_Vaild='" & Expiry & "'"))
    End Function

    Private Sub Live(settings As String)
        Dim setting = File.ReadAllLines(settings).Single(Function(x) x.StartsWith("SqlConStr:", StringComparison.Ordinal))
        Dim builder As New SqlConnectionStringBuilder(setting.Substring("SqlConStr:".Length)) With {.InitialCatalog = "CPOS_ACCOUNTING", .ApplicationName = "CPOS Audit3C rollback tests"}
        Using cn As New SqlConnection(builder.ConnectionString)
            cn.Open()
            Check(CStr(Scalar(cn, Nothing, "SELECT DB_NAME()")) = "CPOS_ACCOUNTING", "approved database verified")
            Check(CInt(Scalar(cn, Nothing, "SELECT EnabledDomainsXml.value('count(/Domains/*)','int') FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = 0, "all domains initially disabled")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE parent_id IN(OBJECT_ID('dbo.Agents_Balance_MV'),OBJECT_ID('dbo.ST_QTY_Deferences')) AND is_disabled=0")) = 0, "fixture tables have no unreviewed triggers")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM dbo.Users u JOIN dbo.Users_Validations_V v ON v.User_ID=u.User_ID WHERE u.User_ID=1 AND u.is_Allow=1 AND v.Update_IM_QTY=1 AND v.PchVoid=1 AND v.Pch_Update=1")) = 1, "existing test actor authorized without changing users")
            source = CType(Scalar(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid)
            itemId = CInt(Scalar(cn, Nothing, "SELECT MIN(IM_ID) FROM dbo.IM_Menu WHERE isStore IN(1,2)"))
            storeId = CInt(Scalar(cn, Nothing, "SELECT MIN(ST_ID) FROM dbo.Stores"))
            Check(Stock(cn, Nothing) = 0R, "fixture stock key starts empty")
            Dim tables = {"Agents_Balance_MV", "ST_QTY_Deferences", "Network_Edit_Tracker", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change"}
            Dim counts = tables.Select(Function(t) CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & t))).ToArray()
            Using tx = cn.BeginTransaction()
                Enable(cn, tx)
                Dim create = Request("Created", 0)
                Dim result = AuditSettlementHeader.ExecuteInTransaction(cn, tx, create)
                Dim id = AuditSettlementHeader.ResultId(result)
                Check(result.Status = "NewPendingCommit" AndAlso id > 0, "creation returns exact pending document identity")
                Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Change c JOIN dbo.Network_Audit_Event e ON e.EventId=c.EventId WHERE e.RequestId='" & create.RequestId.ToString("D") & "' AND c.BeforeState=1 AND c.AfterState IN(2,3)")) = 24, "creation captures 24 actual header fields")
                Check(AuditSettlementHeader.ResultId(AuditSettlementHeader.ExecuteInTransaction(cn, tx, create)) = id, "replayed creation does not allocate another document")
                Line(cn, tx, FirstLine, id, 5R)
                Check(Stock(cn, tx) = 5R AndAlso CInt(Scalar(cn, tx, "SELECT isDepended FROM dbo.Agents_Balance_MV WHERE T_ID=" & id)) = 0, "line affects stock BEFORE approval")
                Apply(cn, tx, "Approved", id)
                Check(Stock(cn, tx) = 5R, "approval does not add stock twice")
                Check(CDec(Scalar(cn, tx, "SELECT Total FROM dbo.Agents_Balance_MV WHERE T_ID=" & id)) = 35D, "approval uses actual line total")
                Check(Apply(cn, tx, "Approved", id).ResultCode.GetValueOrDefault() = 5, "second approval is NoChange")
                Apply(cn, tx, "EditOpened", id)
                Check(CInt(Scalar(cn, tx, "SELECT EditOpenedBy FROM dbo.Agents_Balance_MV WHERE T_ID=" & id)) = 1 AndAlso Stock(cn, tx) = 5R, "edit-open captures actor without reversing stock")
                Dim save = Request("HeaderSaved", id, "عنوان محدث")
                AuditSettlementHeader.ExecuteInTransaction(cn, tx, save)
                Check(CStr(Scalar(cn, tx, "SELECT Receipt_Title FROM dbo.Agents_Balance_MV WHERE T_ID=" & id)) = "عنوان محدث", "header edit persists exact title")
                Check(AuditSettlementHeader.ExecuteInTransaction(cn, tx, Request("HeaderSaved", id, "عنوان محدث")).ResultCode.GetValueOrDefault() = 5, "unchanged header has no fictitious modification")
                Apply(cn, tx, "EditFinished", id)
                Dim voidResult = Apply(cn, tx, "Voided", id)
                Check(voidResult.ResultCode.GetValueOrDefault() = 1 AndAlso Stock(cn, tx) = 0R, "void removes immediate settlement effect")
                Check(CInt(Scalar(cn, tx, "SELECT VoidBy FROM dbo.Agents_Balance_MV WHERE T_ID=" & id)) = 1, "void keeps actual actor")
                Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.ST_QTY_Deferences WHERE Bill_T_ID=" & id)) = 1, "void preserves settlement detail history")
                Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.VW_InventoryTransactionFlow WHERE SourceTable='ST_QTY_Deferences' AND SourceParentId=" & id)) = 0, "inventory-flow view excludes void settlement")
                Check(Apply(cn, tx, "Voided", id).ResultCode.GetValueOrDefault() = 5, "second void is NoChange")
                tx.Rollback()
            End Using
            RemovalTests(cn)
            AdditionalStockTests(cn)
            FailureTests(cn)
            For i = 0 To tables.Length - 1
                Check(CLng(Scalar(cn, Nothing, "SELECT COUNT_BIG(*) FROM dbo." & tables(i))) = counts(i), "rollback restores " & tables(i))
            Next
            Check(Stock(cn, Nothing) = 0R, "stock fixture fully rolled back")
            Check(CInt(Scalar(cn, Nothing, "SELECT COUNT(*) FROM sys.triggers WHERE name='AuditTest_3C_Reject'")) = 0, "failure trigger removed by rollback")
            Check(CInt(Scalar(cn, Nothing, "SELECT EnabledDomainsXml.value('count(/Domains/*)','int') FROM dbo.Network_Audit_Source WHERE ConfigId=1")) = 0, "domains remain disabled")
        End Using
    End Sub

    Private Sub RemovalTests(cn As SqlConnection)
        Using tx = cn.BeginTransaction()
            Enable(cn, tx)
            Dim id = AuditSettlementHeader.ResultId(Apply(cn, tx, "Created", 0))
            Dim other = AuditSettlementHeader.ResultId(Apply(cn, tx, "Created", 0))
            Line(cn, tx, FirstLine, id, 3R) : Line(cn, tx, FirstLine + 1, id, 2R) : Line(cn, tx, FirstLine + 2, other, -1R)
            Apply(cn, tx, "Approved", id)
            Using cmd As New SqlCommand("dbo.ST_Settlement_CanRemove", cn, tx)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Bill_T_ID", id)
                Dim f = cmd.Parameters.Add("@F", SqlDbType.Int) : f.Direction = ParameterDirection.Output
                Dim info = cmd.Parameters.Add("@Str_Name", SqlDbType.NVarChar, 1500) : info.Direction = ParameterDirection.Output
                cmd.ExecuteNonQuery()
                Check(CInt(f.Value) = 1, "removal groups duplicate stock keys: 3+2 would leave -1")
            End Using
            Reject(Sub() Apply(cn, tx, "Voided", id), "void negative stock blocked atomically", "كمية سالبة")
            If tx.Connection IsNot Nothing Then tx.Rollback()
        End Using
        Using tx = cn.BeginTransaction()
            Enable(cn, tx)
            Dim id = AuditSettlementHeader.ResultId(Apply(cn, tx, "Created", 0))
            Line(cn, tx, FirstLine, id, 5R) : Line(cn, tx, FirstLine + 1, id, -4R)
            Reject(Sub() AuditSettlementLines.ExecuteInTransaction(cn, tx, AuditSettlementLines.DeleteRequest(Context(), id, FirstLine)), "line deletion checks settlement stock, not purchase ID", "كمية سالبة")
            If tx.Connection IsNot Nothing Then tx.Rollback()
        End Using
    End Sub

    Private Sub AdditionalStockTests(cn As SqlConnection)
        Using tx = cn.BeginTransaction()
            Enable(cn, tx)
            Dim id = AuditSettlementHeader.ResultId(Apply(cn, tx, "Created", 0))
            Apply(cn, tx, "HeaderSaved", id)
            Check(CInt(Scalar(cn, tx, "SELECT isDepended FROM dbo.Agents_Balance_MV WHERE T_ID=" & id)) = 0, "unapproved header save does not approve the document")
            Reject(Sub() Apply(cn, tx, "Approved", id), "empty settlement cannot be approved", "بلا أصناف")
            If tx.Connection IsNot Nothing Then tx.Rollback()
        End Using
        Using tx = cn.BeginTransaction()
            Enable(cn, tx)
            Check(CInt(Scalar(cn, tx, "SELECT COUNT(*) FROM dbo.Agents_Balance_MV WHERE T_ID=2147482800")) = 0, "orphan fixture parent is absent")
            Line(cn, tx, FirstLine, 2147482800, 8R)
            Check(Stock(cn, tx) = 8R, "historic orphan settlement effect is preserved")
            Dim id = AuditSettlementHeader.ResultId(Apply(cn, tx, "Created", 0))
            Line(cn, tx, FirstLine + 1, id, -3R)
            Check(Stock(cn, tx) = 5R, "negative line takes effect before approval")
            Apply(cn, tx, "Approved", id)
            Check(Stock(cn, tx) = 5R, "negative approval does not subtract stock twice")
            Apply(cn, tx, "Voided", id)
            Check(Stock(cn, tx) = 8R, "voiding negative settlement restores stock")
            tx.Rollback()
        End Using
        Using tx = cn.BeginTransaction()
            Enable(cn, tx)
            Dim id = AuditSettlementHeader.ResultId(Apply(cn, tx, "Created", 0))
            Dim other = AuditSettlementHeader.ResultId(Apply(cn, tx, "Created", 0))
            Line(cn, tx, FirstLine, id, 5R) : Line(cn, tx, FirstLine + 1, other, -4R)
            Apply(cn, tx, "Approved", id)
            Dim requestToAllow = AuditSettlementHeader.Request(Context(), "Voided", id, DocumentDate, "", "", True)
            AuditSettlementHeader.ExecuteInTransaction(cn, tx, requestToAllow)
            Check(Stock(cn, tx) = -4R, "explicit machine allow-negative policy permits void")
            tx.Rollback()
        End Using
        Using tx = cn.BeginTransaction()
            Enable(cn, tx)
            Dim id = AuditSettlementHeader.ResultId(Apply(cn, tx, "Created", 0))
            Line(cn, tx, FirstLine, id, 5R) : Line(cn, tx, FirstLine + 1, id, -4R)
            AuditSettlementLines.ExecuteInTransaction(cn, tx, AuditSettlementLines.DeleteRequest(Context(), id, FirstLine, True))
            Check(Stock(cn, tx) = -4R, "explicit machine allow-negative policy permits line removal")
            tx.Rollback()
        End Using
        Using tx = cn.BeginTransaction()
            Using cmd As New SqlCommand("dbo.ST_Settlement_Header_Apply", cn, tx)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@Action", "Created")
                cmd.Parameters.AddWithValue("@T_ID", 0).Direction = ParameterDirection.InputOutput
                cmd.Parameters.AddWithValue("@User_ID", -2147483647)
                cmd.Parameters.AddWithValue("@Date", DocumentDate)
                Reject(Sub() cmd.ExecuteNonQuery(), "unknown actor cannot write a settlement header", "صلاحية")
            End Using
            If tx.Connection IsNot Nothing Then tx.Rollback()
        End Using
    End Sub

    Private Sub FailureTests(cn As SqlConnection)
        For Each action In {"Created", "Approved", "Voided", "EditOpened", "HeaderSaved", "EditFinished"}
            Using tx = cn.BeginTransaction()
                Enable(cn, tx)
                Dim id = 0
                If action <> "Created" Then
                    id = AuditSettlementHeader.ResultId(Apply(cn, tx, "Created", 0))
                    Line(cn, tx, FirstLine, id, 5R)
                    If action <> "Approved" Then Apply(cn, tx, "Approved", id)
                    If action = "HeaderSaved" OrElse action = "EditFinished" Then Apply(cn, tx, "EditOpened", id)
                End If
                Scalar(cn, tx, "CREATE TRIGGER dbo.AuditTest_3C_Reject ON dbo.Network_Audit_Event AFTER INSERT AS BEGIN THROW 51106,'Intentional header audit failure.',1; END")
                Dim requestToFail = Request(action, id, "عنوان يتطلب تغييرًا")
                Reject(Sub() AuditSettlementHeader.ExecuteInTransaction(cn, tx, requestToFail), "audit failure rolls back header operation " & action, "Intentional header audit failure.")
                If tx.Connection IsNot Nothing Then tx.Rollback()
            End Using
        Next
    End Sub
End Module
