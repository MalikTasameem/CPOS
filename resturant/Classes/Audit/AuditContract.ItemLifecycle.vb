Option Strict On
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text.RegularExpressions

Partial Friend NotInheritable Class AuditContract
    Friend Shared Function IsItemLifecycle(operation As String) As Boolean
        Return {"Item.PlaceholderAcquired", "Item.FullSaved", "Item.QuickSaved", "Item.DisabledRowsRemoved"}.Contains(operation)
    End Function

    Friend Shared Function ItemLifecycleEntities() As String()
        Return ItemMasterEntities().Concat({"ItemStockRow"}).ToArray()
    End Function

    Friend Shared Function ItemLifecycleFields(entityName As String) As String()
        If entityName = "ItemStockRow" Then Return {"StockItemId|IM_ID|Integer|int", "StockStoreId|ST_ID|Integer|int", "StockExpiry|D_Valid|String|nvarchar(50)", "StockQuantity|ST_QTY|Decimal|numeric(18,3)"}
        Return ItemMasterFields(entityName)
    End Function

    ' Input code | original procedure parameter | value type | required/nullable.
    Friend Shared Function ItemLifecycleInputs(operation As String) As String()
        Dim result As New List(Of String) From {"ExpectedLifecycleSnapshot||String|R", "TargetItemId||Integer|R"}
        Select Case operation
            Case "Item.PlaceholderAcquired"
                result.AddRange({"ItemStoreType|isStore|Integer|R", "ItemGroupId|GM_ID|Integer|N"})
            Case "Item.DisabledRowsRemoved"
            Case "Item.QuickSaved"
                result.AddRange({"ItemName|item_name|String|R", "ItemGroupId|GM_ID|Integer|R", "ItemUsesExpiry|isValid|Integer|R", "ItemNumber|IM_Num|String|R", "DefaultUnitId|Def_U_ID|Integer|R", "SelectedUnitId|U_ID|Integer|R", "RequestedBarcode|Barcode|String|N", "RequestedSingleBarcode|Barcode_By_One|String|N", "ItemStoreType|is_Store|Integer|R"})
            Case "Item.FullSaved"
                result.AddRange({"ItemName|item_name|String|R", "ItemSalesName|item_nameSales|String|N", "ItemGroupId|GM_ID|Integer|R", "ItemActive|isActive|Boolean|R", "ItemStoreType|isStore|Integer|R", "ItemUsesExpiry|isValid|Integer|R", "ItemVariablePrice|isChangePrice|Boolean|R", "ItemCost|Cost|Float|R", "ItemBKR|BK_R|Integer|N", "ItemBKG|BK_G|Integer|N", "ItemBKB|BK_B|Integer|N", "ItemFKR|FK_R|Integer|N", "ItemFKG|FK_G|Integer|N", "ItemFKB|FK_B|Integer|N", "ItemShortcut|is_Shortcut|Integer|R", "ItemNumber|IM_Num|String|R", "ItemCompanyGroup|Grp_ID|Integer|R", "ItemPhotoPath|IM_Full_Photo|String|N", "ItemNotes|Notes|String|R", "DefaultUnitId|Def_U_ID|Integer|R", "ItemPhotoSha256||String|N", "ItemPhotoBytes||Integer|N"})
            Case Else : Throw New ArgumentException("عملية دورة الصنف غير معتمدة.")
        End Select
        Return result.ToArray()
    End Function

    Friend Shared Function ItemLifecycleFieldType(operation As String, fieldName As String) As String
        For Each definition As String In ItemLifecycleInputs(operation).Concat(ItemLifecycleEntities().SelectMany(Function(n) ItemLifecycleFields(n)))
            Dim parts = definition.Split("|"c)
            If parts(0) = fieldName Then Return parts(2)
        Next
        Throw New ArgumentException("حقل دورة الصنف غير معتمد.")
    End Function

    Friend Shared Sub ValidateItemLifecycleRequest(r As AuditOperationRequest)
        Dim acquire = r.OperationCode = "Item.PlaceholderAcquired", cleanup = r.OperationCode = "Item.DisabledRowsRemoved", id As Integer
        If Not {"ItemsMenu", "IM_ADD_New"}.Contains(r.Context.ScreenCode) OrElse (r.OperationCode = "Item.FullSaved" AndAlso r.Context.ScreenCode <> "ItemsMenu") OrElse ((cleanup OrElse r.OperationCode = "Item.QuickSaved") AndAlso r.Context.ScreenCode <> "IM_ADD_New") Then Throw New ArgumentException("شاشة دورة الصنف غير معتمدة.")
        If r.EntityType <> If(cleanup, "DisabledItemRows", "CatalogItemRecord") OrElse If(acquire, r.EntityId <> "New", Not Integer.TryParse(r.EntityId, id) OrElse id <= 0) Then Throw New ArgumentException("هوية دورة الصنف غير صالحة.")
        If cleanup AndAlso r.EntityId <> r.Context.ActorUserId.Value.ToString(CultureInfo.InvariantCulture) Then Throw New ArgumentException("تنظيف الصفوف يخص المستخدم الحالي فقط.")
        Dim expected = ItemLifecycleInputs(r.OperationCode)
        If expected.Length <> r.Inputs.Count Then Throw New ArgumentException("مدخلات دورة الصنف غير مكتملة.")
        For Each definition As String In expected
            Dim p = definition.Split("|"c), entry = r.Inputs.SingleOrDefault(Function(x) x.Name = p(0))
            If entry Is Nothing OrElse If(p(3) = "R", entry.Value.State <> AuditValueState.Present, entry.Value.State < AuditValueState.SqlNull) Then Throw New ArgumentException("مدخل الصنف غير ملتقط: " & p(0))
        Next
        Dim v As Func(Of String, AuditValue) = Function(n) r.Inputs.Single(Function(x) x.Name = n).Value
        If Not Regex.IsMatch(v("ExpectedLifecycleSnapshot").Text, "\A[A-F0-9]{64}\z") OrElse Not Integer.TryParse(v("TargetItemId").Text, id) OrElse If(cleanup, id <> 0, id <= 0) Then Throw New ArgumentException("بصمة/هدف الصنف غير صالح.")
        If Not acquire AndAlso Not cleanup AndAlso v("TargetItemId").Text <> r.EntityId Then Throw New ArgumentException("الصنف المستهدف لا يطابق الطلب.")
        For Each entry As AuditInput In r.Inputs.Where(Function(x) x.Value.State = AuditValueState.Present)
            Select Case entry.Name
                Case "ItemName" : Text(entry.Value.Text, If(r.OperationCode = "Item.QuickSaved", 500, 200), entry.Name, True)
                Case "ItemSalesName" : Text(entry.Value.Text, 100, entry.Name)
                Case "ItemNumber" : Text(entry.Value.Text, 100, entry.Name)
                Case "RequestedBarcode", "RequestedSingleBarcode" : Text(entry.Value.Text, 50, entry.Name)
                Case "ItemStoreType" : If Not {"0", "1", "2"}.Contains(entry.Value.Text) Then Throw New ArgumentException("نوع صنف غير صالح.")
                Case "ItemUsesExpiry", "ItemShortcut" : If Not {"0", "1"}.Contains(entry.Value.Text) Then Throw New ArgumentException("حالة صنف غير صالحة.")
                Case "ItemGroupId", "ItemCompanyGroup", "DefaultUnitId", "SelectedUnitId"
                    If Not Integer.TryParse(entry.Value.Text, id) OrElse id <= 0 Then Throw New ArgumentException("مرجع الصنف غير صالح.")
                Case "ItemBKR", "ItemBKG", "ItemBKB", "ItemFKR", "ItemFKG", "ItemFKB"
                    If Not Integer.TryParse(entry.Value.Text, id) OrElse id < 0 OrElse id > 255 Then Throw New ArgumentException("لون غير صالح.")
            End Select
        Next
        If r.OperationCode = "Item.FullSaved" Then
            Dim hash = v("ItemPhotoSha256"), bytes = v("ItemPhotoBytes"), size As Long
            If hash.State <> bytes.State OrElse (hash.State = AuditValueState.Present AndAlso (Not Regex.IsMatch(hash.Text, "\A[A-F0-9]{64}\z") OrElse Not Long.TryParse(bytes.Text, size) OrElse size <= 0)) Then Throw New ArgumentException("بيانات الصورة لا تتطابق.")
        End If
    End Sub

    Friend Shared Sub ValidateItemLifecycleEvent(r As AuditOperationRequest, e As AuditEventRequest)
        Dim cleanup = r.OperationCode = "Item.DisabledRowsRemoved", acquire = r.OperationCode = "Item.PlaceholderAcquired", id As Integer
        If e.EntityType <> r.EntityType OrElse Not Integer.TryParse(e.EntityId, id) OrElse id <= 0 OrElse (Not acquire AndAlso e.EntityId <> r.EntityId) OrElse e.DocumentNumber <> e.EntityId OrElse e.RelatedEntities.Count <> 0 Then Throw New ArgumentException("مرجع حدث الصنف غير صالح.")
        If acquire AndAlso e.EntityId <> r.Inputs.Single(Function(x) x.Name = "TargetItemId").Value.Text Then Throw New ArgumentException("رقم الصنف المكتسب لا يطابق الطلب.")
        If Not cleanup AndAlso e.EventOrdinal = 1 AndAlso (e.Changes.Count <> ItemLifecycleFields("CatalogItemRecord").Length OrElse e.Changes.Any(Function(x) x.EntityType <> "CatalogItemRecord" OrElse x.EntityId <> e.EntityId)) Then Throw New ArgumentException("صف الصنف الأول يجب التقاطه كاملًا.")
        For Each rowGroup In e.Changes.GroupBy(Function(x) x.EntityType & ":" & x.EntityId)
            Dim first = rowGroup.First()
            If Not ItemLifecycleEntities().Contains(first.EntityType) OrElse Not Integer.TryParse(first.EntityId, id) OrElse id <= 0 Then Throw New ArgumentException("كيان فرعي غير صالح.")
            Dim fields = ItemLifecycleFields(first.EntityType)
            If rowGroup.Count() <> fields.Length Then Throw New ArgumentException("صف تابع للصنف غير مكتمل.")
            For Each definition As String In fields
                Dim p = definition.Split("|"c), change = rowGroup.SingleOrDefault(Function(x) x.FieldCode = p(0))
                If change Is Nothing OrElse change.SourceSqlType <> p(3) OrElse change.ValueOrigin <> If(p(3) = "image", 3, 1) OrElse change.Before.State = AuditValueState.NotCaptured OrElse change.After.State = AuditValueState.NotCaptured OrElse (change.Before.State = AuditValueState.Absent AndAlso change.After.State = AuditValueState.Absent) Then Throw New ArgumentException("مصدر/حالة قيمة الصنف غير صالحة.")
                If cleanup Then
                    If Not {"CatalogItemRecord", "ItemUnit", "ItemAlert", "ItemFormat"}.Contains(first.EntityType) OrElse change.Before.State < AuditValueState.SqlNull OrElse change.After.State <> AuditValueState.Absent Then Throw New ArgumentException("التنظيف يلتقط الصفوف المحذوفة فقط.")
                Else
                    If change.After.State < AuditValueState.SqlNull OrElse (Not acquire AndAlso first.EntityType <> "ItemUnit" AndAlso change.Before.State < AuditValueState.SqlNull) Then Throw New ArgumentException("حفظ الصنف لا يحذف الصفوف.")
                    If {"ItemId", "AlertItemId", "FormatItemId", "DifferenceItemId", "StockItemId"}.Contains(change.FieldCode) AndAlso {change.Before, change.After}.Any(Function(x) x.State = AuditValueState.Present AndAlso x.Text <> e.EntityId) Then Throw New ArgumentException("صف يتبع صنفًا آخر.")
                    If first.EntityType = "CatalogItemRecord" AndAlso change.EntityId <> e.EntityId Then Throw New ArgumentException("صف صنف غير الهدف.")
                    If first.EntityType = "ItemStockRow" AndAlso change.FieldCode <> "StockExpiry" AndAlso (change.Before.State <> change.After.State OrElse change.Before.Text <> change.After.Text) Then Throw New ArgumentException("بطاقة الصنف لا تغير كمية/هوية المخزون.")
                End If
            Next
            If rowGroup.Select(Function(x) x.Before.State = AuditValueState.Absent).Distinct().Count() <> 1 OrElse rowGroup.Select(Function(x) x.After.State = AuditValueState.Absent).Distinct().Count() <> 1 Then Throw New ArgumentException("حالة وجود الصف غير متجانسة.")
            If cleanup Then
                Dim enabledCode = If(first.EntityType = "CatalogItemRecord", "ItemEnabled", If(first.EntityType = "ItemUnit", "UnitRowEnabled", If(first.EntityType = "ItemAlert", "AlertEnabled", "FormatEnabled")))
                Dim userCode = If(first.EntityType = "CatalogItemRecord", "ItemUserId", If(first.EntityType = "ItemUnit", "UnitUserId", If(first.EntityType = "ItemAlert", "AlertUserId", "FormatUserId")))
                Dim enabled = rowGroup.Single(Function(x) x.FieldCode = enabledCode), owner = rowGroup.Single(Function(x) x.FieldCode = userCode)
                If enabled.Before.Text <> "0" OrElse owner.Before.Text <> r.EntityId Then Throw New ArgumentException("لا يحذف التنظيف صفًا مفعلًا أو تابعًا لمستخدم آخر.")
            End If
        Next
    End Sub
End Class
