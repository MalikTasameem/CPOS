Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Globalization
Imports System.Linq

Friend Module AuditItemLifecycleTests
    Private checks As Integer
    Private Sub Check(ok As Boolean, message As String)
        If Not ok Then Throw New Exception("FAIL " & message)
        checks += 1
    End Sub
    Private Sub Reject(action As Action, message As String)
        Try
            action()
        Catch ex As ArgumentException
            Check(True, message) : Return
        Catch ex As InvalidOperationException
            Check(True, message) : Return
        End Try
        Throw New Exception("Expected rejection: " & message)
    End Sub
    Private Function Request(op As String) As AuditOperationRequest
        Dim cleanup = op = "Item.DisabledRowsRemoved", acquire = op = "Item.PlaceholderAcquired"
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .OperationCode = op, .EntityType = If(cleanup, "DisabledItemRows", "CatalogItemRecord"), .EntityId = If(cleanup, "1", If(acquire, "New", "77")),
            .Context = New AuditEventContext With {.SourceId = Guid.NewGuid(), .SessionId = Guid.NewGuid(), .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "اختبار", .MachineName = "Offline", .ApplicationVersion = "038", .ScreenCode = If(op = "Item.FullSaved" OrElse acquire, "ItemsMenu", "IM_ADD_New")}}
        For Each d As String In AuditContract.ItemLifecycleInputs(op)
            Dim p = d.Split("|"c), v As Object = Nothing
            If p(3) = "N" Then
                v = DBNull.Value
            Else
                Select Case p(2)
                    Case "String" : v = "اختبار"
                    Case "Integer" : v = 1
                    Case "Boolean" : v = True
                    Case "Float" : v = 6.528R
                End Select
            End If
            If p(0) = "ExpectedLifecycleSnapshot" Then v = New String("A"c, 64)
            If p(0) = "TargetItemId" Then v = If(cleanup, 0, 77)
            r.Inputs.Add(New AuditInput With {.Name = p(0), .ValueType = p(2), .Value = AuditValue.Capture(p(2), v)})
        Next
        AuditContract.Request(r) : Return r
    End Function
    Private Function Snapshot(enabled As Boolean, Optional populated As Boolean = True) As Dictionary(Of String, DataTable)
        Dim result As New Dictionary(Of String, DataTable)(StringComparer.Ordinal)
        For Each entityName As String In AuditContract.ItemLifecycleEntities()
            Dim dt As New DataTable
            dt.Columns.Add("Id", GetType(Integer))
            For Each d As String In AuditContract.ItemLifecycleFields(entityName)
                Dim p = d.Split("|"c)
                Dim t = If(p(2) = "Integer", GetType(Integer), If(p(2) = "Decimal", GetType(Decimal), If(p(2) = "Float", GetType(Double), If(p(2) = "Boolean", GetType(Boolean), If(p(2) = "DateTime", GetType(DateTime), GetType(String))))))
                dt.Columns.Add(p(1), t)
            Next
            If populated AndAlso entityName <> "ItemDifference" Then
                Dim row = dt.NewRow() : row("Id") = If(entityName = "CatalogItemRecord", 77, 80)
                For Each d As String In AuditContract.ItemLifecycleFields(entityName)
                    Dim p = d.Split("|"c), v As Object = Nothing
                    Select Case p(2)
                        Case "Integer" : v = 1
                        Case "Boolean" : v = True
                        Case "String" : v = "test"
                        Case "Decimal" : v = 2D
                        Case "Float" : v = 2R
                        Case "DateTime" : v = New DateTime(2026, 10, 27)
                    End Select
                    If p(1) = "IM_ID" Then v = 77
                    If p(1) = "User_ID" Then v = 1
                    If p(1) = "Row_Enabled" Then v = enabled
                    If p(1) = "PhotoSha256" OrElse p(1) = "PhotoBytes" Then v = DBNull.Value
                    row(p(1)) = v
                Next
                dt.Rows.Add(row)
            End If
            result.Add(entityName, dt)
        Next
        Return result
    End Function
    Private Function Events(r As AuditOperationRequest) As List(Of AuditEventRequest)
        Dim cleanup = r.OperationCode = "Item.DisabledRowsRemoved", acquire = r.OperationCode = "Item.PlaceholderAcquired"
        Dim b = Snapshot(Not cleanup AndAlso Not acquire), a = Snapshot(Not acquire)
        If acquire Then
            b = Snapshot(False, False)
            For Each e As String In AuditContract.ItemLifecycleEntities().Where(Function(n) n <> "CatalogItemRecord")
                a(e).Clear()
            Next
        ElseIf cleanup Then
            b("ItemStockRow").Clear() : a = Snapshot(False, False)
        End If
        Return AuditItemLifecycle.Events(r, b, a, New DateTime(2026, 10, 27, 12, 0, 0, DateTimeKind.Utc))
    End Function
    Public Function Main() As Integer
        Try
            For Each op In {"Item.PlaceholderAcquired", "Item.FullSaved", "Item.QuickSaved", "Item.DisabledRowsRemoved"}
                Dim r = Request(op)
                Check(AuditContract.Domain(op) = "Catalog.ItemLifecycle", "domain " & op)
                Dim allEvents = Events(r)
                Check(allEvents.Count > 0, "events " & op)
                For Each e In allEvents
                    AuditContract.EventData(r, e)
                    Check(True, "canonical event " & op)
                Next
                For Each required In r.Inputs.Select(Function(x) x.Name).ToArray()
                    Dim altered = Request(op)
                    altered.Inputs.RemoveAll(Function(x) x.Name = required)
                    Reject(Sub() AuditContract.Request(altered), "missing " & required)
                Next
                Dim bad = Request(op) : bad.Context.ScreenCode = "POS"
                Reject(Sub() AuditContract.Request(bad), "wrong screen " & op)
                bad = Request(op) : bad.EntityType = "Unit"
                Reject(Sub() AuditContract.Request(bad), "wrong entity " & op)
                bad = Request(op) : bad.Inputs.Single(Function(x) x.Name = "ExpectedLifecycleSnapshot").Value = AuditValue.Capture("String", "bad")
                Reject(Sub() AuditContract.Request(bad), "invalid fingerprint " & op)
                For Each e In allEvents
                    Dim saved = e.Changes(0).SourceSqlType
                    e.Changes(0).SourceSqlType = "money"
                    Reject(Sub() AuditContract.EventData(r, e), "wrong SQL type " & op)
                    e.Changes(0).SourceSqlType = saved
                    Dim last = e.Changes.Last() : e.Changes.RemoveAt(e.Changes.Count - 1)
                    Reject(Sub() AuditContract.EventData(r, e), "missing field " & op)
                    e.Changes.Add(last)
                Next
            Next
            Dim full = Request("Item.FullSaved")
            full.Inputs.Single(Function(x) x.Name = "ItemName").Value = AuditValue.Capture("String", New String("x"c, 201))
            Reject(Sub() AuditContract.Request(full), "SQL name length")
            full = Request("Item.FullSaved")
            full.Inputs.Single(Function(x) x.Name = "ItemPhotoSha256").Value = AuditValue.Capture("String", New String("F"c, 64))
            Reject(Sub() AuditContract.Request(full), "photo state mismatch")
            Dim cleanupRequest = Request("Item.DisabledRowsRemoved"), cleanEvents = Events(cleanupRequest)
            Dim ownerChange = cleanEvents.SelectMany(Function(e) e.Changes).First(Function(c) c.FieldCode = "UnitUserId")
            ownerChange.Before = AuditValue.Capture("Integer", 2)
            Reject(Sub()
                       For Each e In cleanEvents
                           AuditContract.EventData(cleanupRequest, e)
                       Next
                   End Sub, "cleanup foreign user")
            Dim fullRequest = Request("Item.FullSaved"), before = Snapshot(True), after = Snapshot(True)
            after("ItemStockRow").Rows(0)("ST_QTY") = 99D
            Reject(Sub() AuditItemLifecycle.Events(fullRequest, before, after, New DateTime(2026, 10, 27, 12, 0, 0, DateTimeKind.Utc)), "stock quantity must not change")
            Check(AuditItemLifecycle.Fingerprint(before) <> AuditItemLifecycle.Fingerprint(after), "snapshot notices stock changes")
            For Each op In {"Item.PlaceholderAcquired", "Item.QuickSaved", "Item.DisabledRowsRemoved"}
                Check(AuditItemLifecycle.QuickPermissionColumn("IM_ADD_New", op, "Pch_IM_card_11") = "Pch", "purchasing quick context " & op)
                Check(AuditItemLifecycle.QuickPermissionColumn("IM_ADD_New", op, "Invoice_IM_card") = "StExplore", "inventory quick context " & op)
                Check(AuditItemLifecycle.QuickPermissionColumn("IM_ADD_New", op, "Frm_manuel_IM_card") = "Frm_M", "manual production quick context " & op)
                Check(AuditItemLifecycle.QuickPermissionColumn("IM_ADD_New", op, "Sales_Fast") = "Sales", "sales quick context " & op)
                Check(AuditItemLifecycle.QuickPermissionColumn("IM_ADD_New", op, "Items_Search") = "IM", "search alone grants no sales permission " & op)
                Check(AuditItemLifecycle.QuickPermissionColumn("IM_ADD_New", op, "") = "IM", "unknown provenance fails closed " & op)
            Next
            Check(AuditItemLifecycle.QuickPermissionColumn("ItemsMenu", "Item.PlaceholderAcquired", "Pch_IM_card_11") = "IM", "full editor allocation stays IM")
            Check(AuditItemLifecycle.QuickPermissionColumn("ItemsMenu", "Item.FullSaved", "Pch_IM_card_11") = "IM", "full editor save stays IM")
            Check(AuditItemLifecycle.QuickPermissionColumn("IM_ADD_New", "Item.FullSaved", "Pch_IM_card_11") = "IM", "invalid full save cannot inherit quick permission")
            Check(AuditItemLifecycle.QuickPermissionColumn("IM_ADD_New", "Item.QuickSaved", "Stores_Trans_IM_card") = "Add_Draw_St", "transfer permission")
            Check(AuditItemLifecycle.QuickPermissionColumn("IM_ADD_New", "Item.QuickSaved", "ST_settlement_IM_card") = "Update_IM_QTY", "settlement permission")
            Check(AuditItemLifecycle.QuickPermissionColumn("IM_ADD_New", "Item.QuickSaved", "Returns.Sales") = "SB_Rtn", "sales return permission")
            Check(AuditItemLifecycle.QuickPermissionColumn("IM_ADD_New", "Item.QuickSaved", "Returns.Purchase") = "Pch_Rtn", "purchase return permission")
            Dim fixedRequest = Request("Item.QuickSaved"), prepared As New AuditItemLifecycle.Prepared("Pch_IM_card_11")
            prepared.BindRequest(fixedRequest) : prepared.VerifyRequest(fixedRequest)
            Check(prepared.QuickEntryScreen = "Pch_IM_card_11", "fixed quick provenance")
            Reject(Sub() prepared.BindRequest(fixedRequest), "provenance cannot be rebound")
            fixedRequest.Context.ActorUserId = 2
            Reject(Sub() prepared.VerifyRequest(fixedRequest), "mutated request cannot reuse provenance")
            Dim definitions As New DataTable, itemUnits As New DataTable
            definitions.Columns.Add("U_ID", GetType(Integer)) : definitions.Columns.Add("U_Cargo", GetType(Double)) : definitions.Columns.Add("ProcedureCargo", GetType(Decimal))
            definitions.Rows.Add(1, 1R, 1D) : definitions.Rows.Add(2, 1.001R, 1D) : definitions.Rows.Add(3, 0.999R, 1D) : definitions.Rows.Add(4, 1.006R, 1.01D) : definitions.Rows.Add(5, 0.994R, 0.99D)
            itemUnits.Columns.Add("U_ID", GetType(Integer)) : itemUnits.Columns.Add("is_Default", GetType(Integer))
            Check(AuditItemLifecycle.RequiredUnitCount("Item.QuickSaved", itemUnits, definitions, 1, 2) = 1, "quick uses SQL rounded cargo 1.001 to 1.00")
            Check(AuditItemLifecycle.RequiredUnitCount("Item.QuickSaved", itemUnits, definitions, 1, 3) = 1, "quick uses SQL rounded cargo 0.999 to 1.00")
            Check(AuditItemLifecycle.RequiredUnitCount("Item.QuickSaved", itemUnits, definitions, 1, 4) = 2, "quick cargo 1.01 creates two links")
            Check(AuditItemLifecycle.QuickCargoSupported(definitions.Rows(2)), "rounded cargo below one is accepted when SQL becomes one")
            Check(Not AuditItemLifecycle.QuickCargoSupported(definitions.Rows(4)), "cargo remaining below one rejected")
            itemUnits.Rows.Add(2, 0)
            Check(AuditItemLifecycle.RequiredUnitCount("Item.FullSaved", itemUnits, definitions, 1, 0) = 1, "full keeps raw view equality but rounded variable branch")
            itemUnits.Rows.Clear() : itemUnits.Rows.Add(4, 0)
            Check(AuditItemLifecycle.RequiredUnitCount("Item.FullSaved", itemUnits, definitions, 1, 0) = 1, "full adds base one link once")
            Check(AuditItemLifecycle.RequiredUnitCount("Item.FullSaved", itemUnits, definitions, 2, 0) = 2, "full raw base cargo is not exactly one")
            Console.WriteLine("PASS " & checks & " item-lifecycle offline checks; no SQL or forms opened.")
            Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString()) : Return 1
        End Try
    End Function
End Module
