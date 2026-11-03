Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

Friend NotInheritable Class AuditItemLifecycle
    Friend NotInheritable Class Prepared
        Friend Request As AuditOperationRequest
        Friend Photo As Byte()
        Friend ReadOnly QuickEntryScreen As String
        Private requestDigest As String

        Friend Sub New(Optional entryScreen As String = "")
            QuickEntryScreen = entryScreen
        End Sub

        Friend Sub BindRequest(r As AuditOperationRequest)
            If requestDigest IsNot Nothing Then Throw New InvalidOperationException("تم تثبيت سياق طلب الصنف بالفعل.")
            Request = r
            requestDigest = Digest(AuditCanonical.RequestBytes(r))
        End Sub

        Friend Sub VerifyRequest(r As AuditOperationRequest)
            If Request IsNot r OrElse requestDigest Is Nothing OrElse requestDigest <> Digest(AuditCanonical.RequestBytes(r)) Then Throw New InvalidOperationException("تغير طلب الصنف بعد تثبيت مصدر صلاحيته.")
        End Sub
    End Class

    Friend Shared Function Operation(command As SqlCommand) As String
        Select Case command.CommandText.Trim("["c, "]"c).ToLowerInvariant()
            Case "item_menu_insert" : Return "Item.PlaceholderAcquired"
            Case "item_menu_update" : Return "Item.FullSaved"
            Case "add_im_not_exisesit" : Return "Item.QuickSaved"
            Case "item_menu_delete_disablerows" : Return "Item.DisabledRowsRemoved"
            Case Else : Throw New ArgumentException("إجراء دورة الصنف غير معتمد.")
        End Select
    End Function

    Private Shared Sub Guard(cn As SqlConnection, tx As SqlTransaction)
        Dim definitions As New Dictionary(Of String, String) From {
            {"AA_GET_MAX_IM_MENU", "B3AA6E10EFF192872FC2FA3D8C46DAF7E3BC0E165E40BCE9F39A989C6E0BBB0F"},
            {"AA_GET_MAX_IM_UNITS", "7AECD15A7A4FA6A6EFF555FC9B3EAD6961D9226D7FD600AC739024BBE1DEB86F"},
            {"ADD_IM_Not_Exisesit", "0FFD8853B58476DBAA6F6194CF4201C0510DA6ED1C2CD410742D71CFBE38B50B"},
            {"AG_BalanceMV_GetSerial_Barcode", "59C0B613142BF4C0323710F2E94EC9A9A52E5F5851874EEF1E5D2C0F207FEA3D"},
            {"IM_Menu_Units_V", "7ED14E35A4A15EF471E81FB84D94B5940A683FAA5A1DC54C40C076C4278A37CF"},
            {"item_menu_Delete_DisableRows", "CD15FAD78BC42F8A1518B2A823F11899D877A3A8A6C1EF440D0792DFC9632717"},
            {"item_menu_insert", "93807D9F2C89F4554B938B206F106DC974BC7D386D4DCDEAED6A54728B094CB7"},
            {"item_menu_update", "BAAD503D84AB226E7EA2DA6520CFA444F94C4F1A6E89704F97FC3C3FB1F4479F"},
            {"VW_InventoryTransactionFlow", "8DE740DD7F60D35C7C87D947F227C7C9CB898DBBDFFE003C434BD0CEE8F479BE"}
        }
        For Each pair In definitions
            Using command As New SqlCommand("SELECT OBJECT_DEFINITION(OBJECT_ID(@Name))", cn, tx)
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 256).Value = "dbo." & pair.Key
                Dim definition = Convert.ToString(command.ExecuteScalar(), CultureInfo.InvariantCulture)
                If Digest(System.Text.Encoding.Unicode.GetBytes(definition)) <> pair.Value Then Throw New InvalidOperationException("تغير إجراء دورة الصنف؛ يلزم مراجعة العقد: " & pair.Key)
            End Using
        Next
        Dim triggers = AuditSalesQuantity.Rows(cn, tx, "SELECT name FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('dbo.IM_Menu'),OBJECT_ID('dbo.IM_Units'),OBJECT_ID('dbo.IM_QTY_ALERT'),OBJECT_ID('dbo.IM_Formating_Menu'),OBJECT_ID('dbo.ST_QTY_Deferences'),OBJECT_ID('dbo.ST_Balance'))")
        If triggers.Rows.Count <> 0 Then Throw New InvalidOperationException("يوجد Trigger غير مراجع على بيانات الصنف.")
    End Sub

    Friend Shared Function QuickPermissionColumn(screen As String, operationCode As String, entryScreen As String) As String
        ' Full item management never inherits the permissions of a purchasing/sales screen.
        If screen <> "IM_ADD_New" OrElse Not {"Item.PlaceholderAcquired", "Item.QuickSaved", "Item.DisabledRowsRemoved"}.Contains(operationCode) Then Return "IM"
        Select Case entryScreen
            Case "Pch", "Pch_IM_card_11" : Return "Pch"
            Case "Invoice", "Invoice_IM_card", "Inside_Sales", "Inside_Sales_IM_card", "Outside_Sales", "Outside_Sales_IM_card", "Outside_Sales_Touch", "IM_Execute", "IMEX_IM_card", "STORES_Explorer" : Return "StExplore"
            Case "Stores_ImmediateOrder", "Stores_Trans_IM_card" : Return "Add_Draw_St"
            Case "ST_settlement", "ST_settlement_IM_card" : Return "Update_IM_QTY"
            Case "Format_Items_Manual", "Frm_manuel_IM_card" : Return "Frm_M"
            Case "Format_Items_Auto", "Frm_auto_IM_card" : Return "FRM"
            Case "Sales", "SB_IM_card", "ViewBill", "ViewBill_IM_card", "Sales_Fast", "Sales_Fast_Draft", "POS" : Return "Sales"
            Case "Returns.Sales" : Return "SB_Rtn"
            Case "Returns.Purchase" : Return "Pch_Rtn"
            Case Else : Return "IM" ' No provenance, unknown screen, or item administration: fail closed.
        End Select
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, context As AuditEventContext, operationCode As String, entryScreen As String)
        Dim flag = QuickPermissionColumn(context.ScreenCode, operationCode, entryScreen)
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) WHERE u.User_ID=@U AND u.is_Allow=1 AND EXISTS(SELECT 1 FROM dbo.Users_Validations_V v WITH(HOLDLOCK) WHERE v.User_ID=u.User_ID AND (v.IM=1 OR v.[" & flag & "]=1))", cn, tx)
            cmd.Parameters.Add("@U", SqlDbType.Int).Value = context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("صلاحية إدارة الأصناف أو صلاحية الشاشة التي فتحت الإضافة السريعة مطلوبة.")
        End Using
    End Sub

    Private Shared Function Snapshot(cn As SqlConnection, tx As SqlTransaction, operationCode As String, id As Integer, userId As Integer) As Dictionary(Of String, DataTable)
        Dim result As Dictionary(Of String, DataTable)
        If operationCode = "Item.DisabledRowsRemoved" Then
            result = New Dictionary(Of String, DataTable)(StringComparer.Ordinal)
            Dim entities = AuditContract.ItemMasterEntities(), tables = {"IM_Menu", "IM_Units", "IM_QTY_ALERT", "IM_Formating_Menu", "ST_QTY_Deferences"}
            For index = 0 To entities.Length - 1
                Dim entityName = entities(index), key = If(index = 0, "IM_ID", If(index = 1, "U_IM_ID", "T_ID"))
                Dim columns = AuditContract.ItemMasterFields(entityName).Select(Function(d) d.Split("|"c)(1)).Where(Function(n) n <> "PhotoSha256" AndAlso n <> "PhotoBytes")
                Dim rows = AuditSalesQuantity.Rows(cn, tx, "SELECT [" & key & "]," & String.Join(",", columns.Select(Function(n) "[" & n & "]")) & If(index = 0, ",[Photo]", "") & " FROM dbo.[" & tables(index) & "] WITH(UPDLOCK,HOLDLOCK) WHERE " & If(index = 4, "1=0", "User_ID=@P0 AND Row_Enabled=0") & " ORDER BY [" & key & "]", userId)
                If index = 0 Then PhotoMetadata(rows)
                result.Add(entityName, rows)
            Next
        Else
            result = AuditItemMaster.Capture(cn, tx, id)
        End If
        result.Add("ItemStockRow", AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID,IM_ID,ST_ID,D_Valid,ST_QTY FROM dbo.ST_Balance WITH(UPDLOCK,HOLDLOCK) WHERE IM_ID=@P0 ORDER BY T_ID", If(operationCode = "Item.DisabledRowsRemoved", 0, id)))
        ' The procedures assign U_Cargo (float) to NUMERIC(18,2), while their view predicates use the raw float.
        result.Add("References.Units", AuditSalesQuantity.Rows(cn, tx, "SELECT U_ID,U_Cargo,TRY_CONVERT(numeric(18,2),U_Cargo) AS ProcedureCargo FROM dbo.Units WITH(HOLDLOCK) ORDER BY U_ID"))
        result.Add("References.Groups", AuditSalesQuantity.Rows(cn, tx, "SELECT GM_ID FROM dbo.General_Menu WITH(HOLDLOCK) ORDER BY GM_ID"))
        result.Add("References.Companies", AuditSalesQuantity.Rows(cn, tx, "SELECT Grp_ID FROM dbo.Comp_Groups WITH(HOLDLOCK) ORDER BY Grp_ID"))
        Return result
    End Function

    Private Shared Sub PhotoMetadata(rows As DataTable)
        rows.Columns.Add("PhotoSha256", GetType(String)) : rows.Columns.Add("PhotoBytes", GetType(Long))
        For Each row As DataRow In rows.Rows
            If Not IsDBNull(row("Photo")) Then
                Dim bytes = DirectCast(row("Photo"), Byte())
                row("PhotoSha256") = Digest(bytes) : row("PhotoBytes") = bytes.LongLength
            End If
        Next
        rows.Columns.Remove("Photo")
    End Sub

    Friend Shared Function Digest(bytes As Byte()) As String
        Return BitConverter.ToString(AuditCanonical.Hash(bytes)).Replace("-", "")
    End Function

    Friend Shared Function Fingerprint(snapshotData As Dictionary(Of String, DataTable)) As String
        Return Digest(AuditCanonical.Encode(snapshotData.Keys.OrderBy(Function(n) n, StringComparer.Ordinal).Select(Function(n) n & ":" & AuditSalesQuantity.Fingerprint(snapshotData(n)))))
    End Function

    Private Shared Function PlannedItemId(cn As SqlConnection, tx As SqlTransaction, userId As Integer) As Integer
        Dim ranges = AuditSalesQuantity.Rows(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)")
        If ranges.Rows.Count <> 1 Then Throw New InvalidOperationException("نطاق ترقيم الأصناف غير منفرد.")
        Dim firstId = CInt(ranges.Rows(0)(0)), lastId = CInt(ranges.Rows(0)(1))
        Dim ids = AuditSalesQuantity.Rows(cn, tx, "SELECT IM_ID,Row_Enabled,User_ID FROM dbo.IM_Menu WITH(UPDLOCK,HOLDLOCK) WHERE IM_ID BETWEEN @P0 AND @P1 ORDER BY IM_ID", firstId, lastId)
        Dim disabled = ids.Rows.Cast(Of DataRow)().Where(Function(row) Not IsDBNull(row("Row_Enabled")) AndAlso Not CBool(row("Row_Enabled")) AndAlso Not IsDBNull(row("User_ID")) AndAlso CInt(row("User_ID")) = userId).ToList()
        If disabled.Count > 1 Then Throw New InvalidOperationException("يوجد أكثر من صنف غير معتمد للمستخدم؛ يلزم مراجعتها قبل تخصيص رقم.")
        If disabled.Count = 1 Then Return CInt(disabled(0)("IM_ID"))
        Dim maximum = If(ids.Rows.Count = 0, firstId, CInt(ids.Rows(ids.Rows.Count - 1)(0)))
        If firstId >= lastId OrElse maximum >= lastId OrElse maximum < 0 Then Throw New InvalidOperationException("نطاق ترقيم الأصناف ممتلئ أو غير صالح.")
        Return maximum + 1
    End Function

    Private Shared Function InputObject(r As AuditOperationRequest, name As String) As Object
        Dim entry = r.Inputs.Single(Function(x) x.Name = name)
        If entry.Value.State = AuditValueState.SqlNull Then Return DBNull.Value
        Select Case entry.ValueType
            Case "Integer" : Return Long.Parse(entry.Value.Text, CultureInfo.InvariantCulture)
            Case "Float" : Return Double.Parse(entry.Value.Text, CultureInfo.InvariantCulture)
            Case "Boolean" : Return entry.Value.Text = "1"
            Case Else : Return entry.Value.Text
        End Select
    End Function

    Friend Shared Function Prepare(connection As String, context As AuditEventContext, command As SqlCommand, Optional entryScreen As String = "") As Prepared
        Dim operationCode = Operation(command), prepared As New Prepared(entryScreen)
        Dim suppliedPhoto = AuditItemUnits.Parameter(command, "photo", DBNull.Value)
        If Not IsDBNull(suppliedPhoto) Then prepared.Photo = DirectCast(DirectCast(suppliedPhoto, Byte()).Clone(), Byte())
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Guard(cn, tx) : Permission(cn, tx, context, operationCode, prepared.QuickEntryScreen)
                Dim id = If(operationCode = "Item.PlaceholderAcquired", PlannedItemId(cn, tx, context.ActorUserId.Value), If(operationCode = "Item.DisabledRowsRemoved", 0, Convert.ToInt32(AuditItemUnits.Parameter(command, "IM_ID", 0))))
                Dim before = Snapshot(cn, tx, operationCode, id, context.ActorUserId.Value)
                If operationCode = "Item.DisabledRowsRemoved" AndAlso AuditContract.ItemMasterEntities().All(Function(n) before(n).Rows.Count = 0) Then
                    tx.Commit() : Return Nothing ' No database change, hence no fabricated business event.
                End If
                Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = operationCode,
                    .EntityType = If(operationCode = "Item.DisabledRowsRemoved", "DisabledItemRows", "CatalogItemRecord"),
                    .EntityId = If(operationCode = "Item.PlaceholderAcquired", "New", If(id = 0, context.ActorUserId.Value, id).ToString(CultureInfo.InvariantCulture))}
                For Each definition As String In AuditContract.ItemLifecycleInputs(operationCode)
                    Dim p = definition.Split("|"c), inputValue As Object
                    Select Case p(0)
                        Case "ExpectedLifecycleSnapshot" : inputValue = Fingerprint(before)
                        Case "TargetItemId" : inputValue = id
                        Case "ItemPhotoSha256" : inputValue = If(prepared.Photo Is Nothing, CType(DBNull.Value, Object), Digest(prepared.Photo))
                        Case "ItemPhotoBytes" : inputValue = If(prepared.Photo Is Nothing, CType(DBNull.Value, Object), prepared.Photo.LongLength)
                        Case "ItemCost" : inputValue = AuditItemUnits.Parameter(command, p(1), 0R)
                        Case Else : inputValue = AuditItemUnits.Parameter(command, p(1), DBNull.Value)
                    End Select
                    If Not IsDBNull(inputValue) Then
                        Select Case p(2)
                            Case "Integer" : inputValue = Convert.ToInt64(inputValue, CultureInfo.CurrentCulture)
                            Case "Boolean" : inputValue = Convert.ToBoolean(inputValue, CultureInfo.CurrentCulture)
                            Case "Float" : inputValue = Convert.ToDouble(inputValue, CultureInfo.CurrentCulture)
                            Case "String" : inputValue = Convert.ToString(inputValue, CultureInfo.CurrentCulture)
                        End Select
                    End If
                    r.Inputs.Add(New AuditInput With {.Name = p(0), .ValueType = p(2), .Value = AuditValue.Capture(p(2), inputValue)})
                Next
                AuditContract.Request(r)
                ValidateBefore(cn, tx, r, before)
                prepared.BindRequest(r) : tx.Commit() : Return prepared
            End Using
        End Using
    End Function

    Private Shared Sub ValidateBefore(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, before As Dictionary(Of String, DataTable))
        If r.OperationCode = "Item.DisabledRowsRemoved" Then Return
        Dim itemRows = before("CatalogItemRecord")
        If r.OperationCode = "Item.PlaceholderAcquired" Then
            If itemRows.Rows.Count > 1 Then Throw New InvalidOperationException("رقم صنف غير منفرد.")
            If itemRows.Rows.Count = 1 AndAlso (CBool(itemRows.Rows(0)("Row_Enabled")) OrElse CInt(itemRows.Rows(0)("User_ID")) <> r.Context.ActorUserId.Value) Then Throw New InvalidOperationException("الصف غير المعتمد لا يتبع المستخدم الحالي.")
            If itemRows.Rows.Count = 0 AndAlso AuditContract.ItemLifecycleEntities().Where(Function(n) n <> "CatalogItemRecord").Any(Function(n) before(n).Rows.Count > 0) Then Throw New InvalidOperationException("الرقم الجديد له بيانات تابعة يتيمة.")
        Else
            If itemRows.Rows.Count <> 1 Then Throw New InvalidOperationException("الصنف غير موجود.")
            If Not CBool(itemRows.Rows(0)("Row_Enabled")) AndAlso CInt(itemRows.Rows(0)("User_ID")) <> r.Context.ActorUserId.Value Then Throw New InvalidOperationException("لا يمكن اعتماد صنف غير معتمد لمستخدم آخر.")
            If r.OperationCode = "Item.QuickSaved" AndAlso (CBool(itemRows.Rows(0)("Row_Enabled")) OrElse before("ItemUnit").Rows.Count > 0) Then Throw New InvalidOperationException("الإضافة السريعة تتطلب صفًا جديدًا بلا وحدات؛ حدّث الشاشة.")
            Dim oldType = CInt(itemRows.Rows(0)("isStore")), newType = CInt(InputObject(r, "ItemStoreType"))
            If (oldType = 0) <> (newType = 0) AndAlso AuditSalesQuantity.Rows(cn, tx, "SELECT TOP 1 IM_ID FROM dbo.VW_InventoryTransactionFlow WITH(HOLDLOCK) WHERE IM_ID=@P0", CInt(r.EntityId)).Rows.Count > 0 Then Throw New InvalidOperationException("لا يمكن تغيير نوع الصنف بين الخدمة والمخزون بعد وجود حركة.")
        End If
        For Each entry As AuditInput In r.Inputs.Where(Function(x) {"ItemGroupId", "ItemCompanyGroup", "DefaultUnitId", "SelectedUnitId"}.Contains(x.Name) AndAlso x.Value.State = AuditValueState.Present)
            Dim table = If(entry.Name = "ItemGroupId", "References.Groups", If(entry.Name = "ItemCompanyGroup", "References.Companies", "References.Units"))
            If before(table).Rows.Cast(Of DataRow)().Count(Function(row) Convert.ToInt64(row(0)) = CLng(InputObject(r, entry.Name))) <> 1 Then Throw New InvalidOperationException("مرجع الصنف غير موجود: " & entry.Name)
        Next
        If before("References.Groups").Rows.Count = 0 OrElse before("References.Companies").Rows.Count = 0 Then Throw New InvalidOperationException("تصنيفات الأصناف/الشركة غير معرفة.")
        If r.OperationCode = "Item.QuickSaved" Then
            Dim unitRow = before("References.Units").Rows.Cast(Of DataRow)().Single(Function(row) CInt(row("U_ID")) = CInt(InputObject(r, "SelectedUnitId")))
            If Not QuickCargoSupported(unitRow) Then Throw New InvalidOperationException("الإضافة السريعة تتطلب عبوة لا تقل عن الواحد بعد تحويلها بدقة الإجراء.")
        End If
    End Sub

    Friend Shared Function QuickCargoSupported(unitRow As DataRow) As Boolean
        Return Not IsDBNull(unitRow("ProcedureCargo")) AndAlso CDec(unitRow("ProcedureCargo")) >= 1D
    End Function

    Friend Shared Function RequiredUnitCount(operationCode As String, itemUnits As DataTable, definitions As DataTable, defaultUnitId As Integer, selectedUnitId As Integer) As Integer
        Dim units = itemUnits.Rows.Cast(Of DataRow)().ToList(), needed As Integer
        Dim definition As Func(Of Integer, DataRow) = Function(uid) definitions.Rows.Cast(Of DataRow)().Single(Function(row) CInt(row("U_ID")) = uid)
        If operationCode = "Item.QuickSaved" Then Return If(CDec(definition(selectedUnitId)("ProcedureCargo")) > 1D, 2, 1)
        Dim hasDefault = units.Any(Function(row) Not IsDBNull(row("is_Default")) AndAlso CInt(row("is_Default")) = 1)
        If Not hasDefault AndAlso units.Count = 1 AndAlso CDec(definition(CInt(units(0)("U_ID")))("ProcedureCargo")) > 1D Then needed += 1
        Dim hasOne = units.Any(Function(row) CDbl(definition(CInt(row("U_ID")))("U_Cargo")) = 1R) OrElse (needed > 0 AndAlso CDbl(definition(defaultUnitId)("U_Cargo")) = 1R)
        If units.Count + needed = 0 OrElse Not hasOne Then needed += 1
        Return needed
    End Function

    Private Shared Sub LockUnits(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, before As Dictionary(Of String, DataTable))
        If Not {"Item.FullSaved", "Item.QuickSaved"}.Contains(r.OperationCode) Then Return
        Dim rangeRows = AuditSalesQuantity.Rows(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)")
        If rangeRows.Rows.Count <> 1 Then Throw New InvalidOperationException("نطاق ترقيم الوحدات غير منفرد.")
        Dim firstId = CInt(rangeRows.Rows(0)(0)), lastId = CInt(rangeRows.Rows(0)(1))
        Dim used = AuditSalesQuantity.Rows(cn, tx, "SELECT U_IM_ID FROM dbo.IM_Units WITH(UPDLOCK,HOLDLOCK) WHERE U_IM_ID BETWEEN @P0 AND @P1 ORDER BY U_IM_ID", firstId, lastId)
        Dim maximum = If(used.Rows.Count = 0, firstId, CInt(used.Rows(used.Rows.Count - 1)(0)))
        Dim needed = RequiredUnitCount(r.OperationCode, before("ItemUnit"), before("References.Units"), CInt(InputObject(r, "DefaultUnitId")), If(r.OperationCode = "Item.QuickSaved", CInt(InputObject(r, "SelectedUnitId")), 0))
        If firstId >= lastId OrElse CLng(maximum) + needed > lastId OrElse CLng(maximum) + 1000 > Integer.MaxValue Then Throw New InvalidOperationException("نطاق روابط وحدات الصنف/باركودها ممتلئ.")
        For index = 1 To needed
            If AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID FROM dbo.IM_Struct_Menu WITH(UPDLOCK,HOLDLOCK) WHERE U_IM_ID=@P0", maximum + index).Rows.Count > 0 Then Throw New InvalidOperationException("رقم رابط الوحدة له مكونات يتيمة.")
        Next
    End Sub

    Private Shared Sub ExecuteCommand(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, photo As Byte())
        Dim procedure = If(r.OperationCode = "Item.PlaceholderAcquired", "item_menu_insert", If(r.OperationCode = "Item.FullSaved", "item_menu_update", If(r.OperationCode = "Item.QuickSaved", "ADD_IM_Not_Exisesit", "item_menu_Delete_DisableRows")))
        Using command = AuditRequestCoordinator.Command("dbo." & procedure, cn, tx)
            command.Parameters.Add("@User_ID", SqlDbType.Int).Value = r.Context.ActorUserId.Value
            If r.OperationCode <> "Item.DisabledRowsRemoved" Then
                command.Parameters.Add("@IM_ID", SqlDbType.Int).Value = CInt(InputObject(r, "TargetItemId"))
                If r.OperationCode = "Item.PlaceholderAcquired" Then command.Parameters("@IM_ID").Direction = ParameterDirection.InputOutput
            End If
            For Each definition As String In AuditContract.ItemLifecycleInputs(r.OperationCode)
                Dim p = definition.Split("|"c)
                If p(1).Length = 0 Then Continue For
                Dim parameter = command.Parameters.Add("@" & p(1), If(p(2) = "Integer", SqlDbType.Int, If(p(2) = "Float", SqlDbType.Float, If(p(2) = "Boolean", SqlDbType.Bit, SqlDbType.NVarChar))))
                If p(2) = "String" Then parameter.Size = -1
                parameter.Value = InputObject(r, p(0))
            Next
            If r.OperationCode = "Item.FullSaved" Then command.Parameters.Add("@photo", SqlDbType.Image).Value = If(photo Is Nothing, CType(DBNull.Value, Object), photo)
            command.ExecuteNonQuery()
            If r.OperationCode = "Item.PlaceholderAcquired" AndAlso CInt(command.Parameters("@IM_ID").Value) <> CInt(InputObject(r, "TargetItemId")) Then Throw New InvalidOperationException("تغير رقم الصنف المخصص.")
        End Using
    End Sub

    Friend Shared Function Events(r As AuditOperationRequest, before As Dictionary(Of String, DataTable), after As Dictionary(Of String, DataTable), occurred As DateTime) As List(Of AuditEventRequest)
        Dim result As New List(Of AuditEventRequest), current As AuditEventRequest = Nothing
        Dim cleanup = r.OperationCode = "Item.DisabledRowsRemoved", eventId = If(cleanup, r.EntityId, Convert.ToString(InputObject(r, "TargetItemId"), CultureInfo.InvariantCulture))
        For Each entityName As String In AuditContract.ItemLifecycleEntities()
            Dim oldRows = before(entityName).Rows.Cast(Of DataRow)().ToDictionary(Function(row) CInt(row(0)))
            Dim newRows = after(entityName).Rows.Cast(Of DataRow)().ToDictionary(Function(row) CInt(row(0)))
            For Each rowId In oldRows.Keys.Union(newRows.Keys).OrderBy(Function(x) x)
                Dim oldRow = If(oldRows.ContainsKey(rowId), oldRows(rowId), Nothing), newRow = If(newRows.ContainsKey(rowId), newRows(rowId), Nothing)
                If cleanup AndAlso newRow IsNot Nothing Then Throw New InvalidOperationException("لم يحذف الإجراء كل الصفوف غير المعتمدة المحددة.")
                Dim fields = AuditContract.ItemLifecycleFields(entityName)
                If current Is Nothing OrElse current.Changes.Count + fields.Length > 100 OrElse (Not cleanup AndAlso current.EventOrdinal = 1 AndAlso entityName <> "CatalogItemRecord") Then
                    current = New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = result.Count + 1, .EventCode = r.OperationCode, .Outcome = 1, .EntityType = r.EntityType, .EntityId = eventId, .DocumentNumber = eventId, .OccurredAtUtc = occurred}
                    result.Add(current)
                End If
                For Each definition As String In fields
                    Dim p = definition.Split("|"c)
                    current.Changes.Add(New AuditFieldChange With {.ChangeNo = current.Changes.Count + 1, .EntityType = entityName, .EntityId = rowId.ToString(CultureInfo.InvariantCulture), .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = CByte(If(p(3) = "image", 3, 1)),
                        .Before = If(oldRow Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), oldRow(p(1)))),
                        .After = If(newRow Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), newRow(p(1))))})
                Next
            Next
        Next
        For Each auditEvent In result
            AuditContract.EventData(r, auditEvent)
        Next
        Return result
    End Function

    Private Shared Sub VerifyResult(r As AuditOperationRequest, before As Dictionary(Of String, DataTable), after As Dictionary(Of String, DataTable))
        If r.OperationCode = "Item.DisabledRowsRemoved" Then
            If AuditContract.ItemLifecycleEntities().Any(Function(n) after(n).Rows.Count > 0) Then Throw New InvalidOperationException("بقيت صفوف غير معتمدة بعد التنظيف.")
            Return
        End If
        If after("CatalogItemRecord").Rows.Count <> 1 Then Throw New InvalidOperationException("لم يؤكد الإجراء وجود الصنف.")
        Dim row = after("CatalogItemRecord").Rows(0), acquiring = r.OperationCode = "Item.PlaceholderAcquired"
        If CBool(row("Row_Enabled")) <> (Not acquiring) Then Throw New InvalidOperationException("حالة اعتماد الصنف لا تطابق العملية.")
        If Not acquiring Then
            If CStr(row("item_name")) <> CStr(InputObject(r, "ItemName")) OrElse CInt(row("GM_ID")) <> CInt(InputObject(r, "ItemGroupId")) OrElse CInt(row("isStore")) <> CInt(InputObject(r, "ItemStoreType")) OrElse CInt(row("isValid")) <> CInt(InputObject(r, "ItemUsesExpiry")) Then Throw New InvalidOperationException("بيانات الصنف المحفوظة لا تطابق الطلب.")
        End If
        If r.OperationCode = "Item.FullSaved" Then
            For Each name In {"ItemPhotoSha256", "ItemPhotoBytes"}
                Dim columnName = If(name = "ItemPhotoSha256", "PhotoSha256", "PhotoBytes")
                Dim expected = r.Inputs.Single(Function(x) x.Name = name), actual = AuditValue.Capture(expected.ValueType, row(columnName))
                If expected.Value.State <> actual.State OrElse expected.Value.Text <> actual.Text Then Throw New InvalidOperationException("الصورة المحفوظة لا تطابق الصورة المؤكدة.")
            Next
        End If
        For Each refName As String In before.Keys.Where(Function(n) n.StartsWith("References.", StringComparison.Ordinal))
            If AuditSalesQuantity.Fingerprint(before(refName)) <> AuditSalesQuantity.Fingerprint(after(refName)) Then Throw New InvalidOperationException("إجراء الصنف غيّر جدولًا مرجعيًا.")
        Next
    End Sub

    Friend Shared Function Save(connection As String, prepared As Prepared, request As AuditOperationRequest) As AuditRequestResult
        AuditContract.Request(request)
        If prepared Is Nothing OrElse prepared.Request IsNot request Then Throw New ArgumentException("طلب الصنف لا يتبع الصورة المؤكدة.")
        prepared.VerifyRequest(request)
        If request.OperationCode = "Item.FullSaved" Then
            Dim hash = request.Inputs.Single(Function(x) x.Name = "ItemPhotoSha256").Value
            If If(prepared.Photo Is Nothing, hash.State <> AuditValueState.SqlNull, hash.State <> AuditValueState.Present OrElse hash.Text <> Digest(prepared.Photo) OrElse CLng(InputObject(request, "ItemPhotoBytes")) <> prepared.Photo.LongLength) Then Throw New InvalidOperationException("تغيرت الصورة بعد تأكيد الطلب.")
        End If
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Dim lease As AuditRequestLease = Nothing
                Try
                    lease = AuditRequestCoordinator.Begin(cn, tx, request)
                    If Not lease.MustExecute Then
                        tx.Commit() : Return lease.Result
                    End If
                    Dim r = lease.Request, id = CInt(InputObject(r, "TargetItemId"))
                    Guard(cn, tx) : Permission(cn, tx, r.Context, r.OperationCode, prepared.QuickEntryScreen)
                    If r.OperationCode = "Item.PlaceholderAcquired" AndAlso PlannedItemId(cn, tx, r.Context.ActorUserId.Value) <> id Then Throw New InvalidOperationException("تغير تخصيص رقم الصنف؛ أعد فتح الإضافة.")
                    Dim before = Snapshot(cn, tx, r.OperationCode, id, r.Context.ActorUserId.Value)
                    If Fingerprint(before) <> CStr(InputObject(r, "ExpectedLifecycleSnapshot")) Then Throw New InvalidOperationException("تغيرت بطاقة الصنف/صفوفها؛ حدّث الشاشة قبل الحفظ.")
                    ValidateBefore(cn, tx, r, before) : LockUnits(cn, tx, r, before)
                    ExecuteCommand(cn, tx, r, prepared.Photo)
                    AuditRequestCoordinator.RequireTransaction(cn, tx)
                    Dim after = Snapshot(cn, tx, r.OperationCode, id, r.Context.ActorUserId.Value)
                    VerifyResult(r, before, after)
                    Dim allEvents = Events(r, before, after, AuditRequestCoordinator.ServerUtcNow(cn, tx))
                    If allEvents.Count = 0 Then Throw New InvalidOperationException("عملية بلا تفاصيل مراقبة.")
                    For Each auditEvent In allEvents
                        AuditWriter.WriteInTransaction(cn, tx, lease, auditEvent)
                    Next
                    Dim result As New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1,
                        .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", If(id = 0, r.EntityId, id.ToString(CultureInfo.InvariantCulture))}})}
                    tx.Commit() : Return result
                Catch
                    If lease IsNot Nothing Then lease.Faulted = True
                    Try
                        If tx.Connection IsNot Nothing Then tx.Rollback()
                    Catch
                    End Try
                    Throw
                End Try
            End Using
        End Using
    End Function
End Class
