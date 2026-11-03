Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

Friend NotInheritable Class AuditItemUnitSnapshot
    Friend Item As DataTable
    Friend Units As DataTable
    Friend Recipes As DataTable
End Class

Friend NotInheritable Class AuditItemUnits
    Friend Shared Function Operation(command As SqlCommand) As String
        Select Case command.CommandText.Trim("["c, "]"c).ToLowerInvariant()
            Case "im_units_insert" : Return "ItemUnit.Created"
            Case "im_units_delete" : Return "ItemUnit.Deleted"
            Case "im_units_updateprice" : Return "ItemUnit.Updated"
            Case "im_units_updatepriceby_val" : Return "ItemUnit.PriceChanged"
            Case "im_units_update_offer_price" : Return "ItemUnit.OfferPriceChanged"
            Case Else : Throw New ArgumentException("مسار رابط الوحدة غير معتمد.")
        End Select
    End Function

    Friend Shared Function Parameter(command As SqlCommand, name As String, Optional fallback As Object = Nothing) As Object
        Dim p = command.Parameters.Cast(Of SqlParameter)().SingleOrDefault(Function(x) String.Equals(x.ParameterName.TrimStart("@"c), name, StringComparison.OrdinalIgnoreCase))
        If p IsNot Nothing Then
            If p.Value Is Nothing Then Throw New ArgumentException("قيمة غير محددة: " & name)
            Return p.Value
        End If
        If fallback Is Nothing Then Throw New ArgumentException("معامل مطلوب: " & name)
        Return fallback
    End Function

    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, itemId As Integer, target As Integer) As AuditItemUnitSnapshot
        Dim columns As Func(Of String, String) = Function(e) String.Join(",", AuditContract.ItemUnitFields(e).Select(Function(x) "[" & x.Split("|"c)(1) & "]"))
        Dim s As New AuditItemUnitSnapshot With {
            .Item = AuditSalesQuantity.Rows(cn, tx, "SELECT IM_ID," & columns("CatalogItem") & " FROM dbo.IM_Menu WITH(UPDLOCK,HOLDLOCK) WHERE IM_ID=@P0", itemId),
            .Units = AuditSalesQuantity.Rows(cn, tx, "SELECT U_IM_ID," & columns("ItemUnit") & " FROM dbo.IM_Units WITH(UPDLOCK,HOLDLOCK) WHERE IM_ID=@P0 ORDER BY U_IM_ID", itemId),
            .Recipes = AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID," & columns("ItemUnitRecipe") & " FROM dbo.IM_Struct_Menu WITH(UPDLOCK,HOLDLOCK) WHERE U_IM_ID=@P1 OR U_IM_ID IN(SELECT U_IM_ID FROM dbo.IM_Units WITH(HOLDLOCK) WHERE IM_ID=@P0) ORDER BY T_ID", itemId, target)}
        If s.Item.Rows.Count <> 1 Then Throw New InvalidOperationException("الصنف غير موجود أو معرفه مكرر.")
        Return s
    End Function

    Friend Shared Function Fingerprint(s As AuditItemUnitSnapshot) As String
        Return BitConverter.ToString(AuditCanonical.Hash(AuditCanonical.Encode({AuditSalesQuantity.Fingerprint(s.Item), AuditSalesQuantity.Fingerprint(s.Units), AuditSalesQuantity.Fingerprint(s.Recipes)}))).Replace("-", "")
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, context As AuditEventContext)
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) WHERE u.User_ID=@User AND u.is_Allow=1" &
            If(context.ScreenCode = "Add_Unit", "", " AND EXISTS(SELECT 1 FROM dbo.Users_Validations_V v WITH(HOLDLOCK) WHERE v.User_ID=u.User_ID AND v.IM=1)"), cn, tx)
            cmd.Parameters.Add("@User", SqlDbType.Int).Value = context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("المستخدم غير مسموح له أو لا يملك صلاحية إدارة الأصناف.")
        End Using
    End Sub

    Friend Shared Function Read(connection As String, context As AuditEventContext, command As SqlCommand) As AuditItemUnitSnapshot
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Permission(cn, tx, context)
                Dim create = Operation(command) = "ItemUnit.Created", target = If(create, 0, Convert.ToInt32(Parameter(command, "U_IM_ID")))
                Dim itemId As Integer
                If create Then
                    itemId = Convert.ToInt32(Parameter(command, "IM_ID"))
                Else
                    Dim rows = AuditSalesQuantity.Rows(cn, tx, "SELECT IM_ID FROM dbo.IM_Units WITH(UPDLOCK,HOLDLOCK) WHERE U_IM_ID=@P0", target)
                    If rows.Rows.Count <> 1 OrElse IsDBNull(rows.Rows(0)(0)) Then Throw New InvalidOperationException("رابط الوحدة غير موجود أو غير مرتبط بصنف.")
                    itemId = CInt(rows.Rows(0)(0))
                End If
                Dim s = Capture(cn, tx, itemId, target)
                tx.Commit() : Return s
            End Using
        End Using
    End Function

    Friend Shared Function Request(context As AuditEventContext, command As SqlCommand, before As AuditItemUnitSnapshot, checkBarcode As Boolean) As AuditOperationRequest
        Dim op = Operation(command), create = op = "ItemUnit.Created"
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = op,
            .EntityType = "CatalogItem", .EntityId = CInt(before.Item.Rows(0)("IM_ID")).ToString(CultureInfo.InvariantCulture)}
        For Each name In AuditContract.ItemUnitInputs(op)
            Dim value As Object
            Select Case name
                Case "ExpectedItemUnitSnapshot" : value = Fingerprint(before)
                Case "TargetItemUnitId" : value = If(create, CObj(0), Parameter(command, "U_IM_ID"))
                Case "CheckBarcode" : value = checkBarcode
                Case "MakeDefault" : value = Parameter(command, "is_Default")
                Case "UnitBarcode" : value = Parameter(command, "Barcode", DBNull.Value)
                Case "MinimumPrice" : value = Parameter(command, "Min_SP", DBNull.Value)
                Case "MinimumPrice2" : value = Parameter(command, "Min_SP_2", DBNull.Value)
                Case "UnitRowEnabled" : value = Parameter(command, "Row_Enabled", False)
                Case "UnitId" : value = Parameter(command, "U_ID")
                Case "UnitPrice", "OfferPrice" : value = Parameter(command, "Price")
                Case Else : Throw New ArgumentException("مدخل غير معتمد.")
            End Select
            Dim type = AuditContract.FieldType(op, name)
            If Not IsDBNull(value) Then
                Select Case type
                    Case "Decimal" : value = Convert.ToDecimal(value, CultureInfo.CurrentCulture)
                    Case "Integer" : value = Convert.ToInt32(value, CultureInfo.CurrentCulture)
                    Case "Boolean" : value = Convert.ToBoolean(value, CultureInfo.CurrentCulture)
                End Select
            End If
            r.Inputs.Add(New AuditInput With {.Name = name, .ValueType = type, .Value = AuditValue.Capture(type, value)})
        Next
        AuditContract.Request(r) : Return r
    End Function

    Private Shared Function Db(r As AuditOperationRequest, name As String) As Object
        Dim input = r.Inputs.Single(Function(x) x.Name = name)
        If input.Value.State = AuditValueState.SqlNull Then Return DBNull.Value
        Select Case input.ValueType
            Case "Integer" : Return Integer.Parse(input.Value.Text, CultureInfo.InvariantCulture)
            Case "Decimal" : Return Decimal.Parse(input.Value.Text, CultureInfo.InvariantCulture)
            Case "Boolean" : Return input.Value.Text = "1"
            Case Else : Return input.Value.Text
        End Select
    End Function

    Private Shared Sub LockAllocation(cn As SqlConnection, tx As SqlTransaction)
        Dim ranges = AuditSalesQuantity.Rows(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)")
        If ranges.Rows.Count <> 1 OrElse IsDBNull(ranges.Rows(0)(0)) OrElse IsDBNull(ranges.Rows(0)(1)) Then Throw New InvalidOperationException("نطاق ترقيم روابط الوحدات غير محدد بشكل منفرد.")
        Dim first = CInt(ranges.Rows(0)(0)), last = CInt(ranges.Rows(0)(1))
        Dim rows = AuditSalesQuantity.Rows(cn, tx, "SELECT ISNULL(MAX(U_IM_ID),@P0) FROM dbo.IM_Units WITH(UPDLOCK,HOLDLOCK) WHERE U_IM_ID BETWEEN @P0 AND @P1", first, last)
        If first >= last OrElse CInt(rows.Rows(0)(0)) >= last Then Throw New InvalidOperationException("نطاق روابط الوحدات ممتلئ أو غير صالح.")
        Dim nextId = CInt(rows.Rows(0)(0)) + 1
        If AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID FROM dbo.IM_Struct_Menu WITH(UPDLOCK,HOLDLOCK) WHERE U_IM_ID=@P0", nextId).Rows.Count <> 0 Then Throw New InvalidOperationException("رقم الرابط الجديد مرتبط بمكونات يتيمة؛ يلزم مراجعتها قبل الإضافة.")
    End Sub

    Private Shared Sub AddMoney(cmd As SqlCommand, name As String, value As Object)
        Dim p = cmd.Parameters.Add(name, SqlDbType.Decimal)
        p.Precision = 18 : p.Scale = 3 : p.Value = value
    End Sub

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse Not AuditContract.IsItemUnit(request.OperationCode) Then Throw New ArgumentException("يلزم عقد رابط الوحدة ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, create = r.OperationCode = "ItemUnit.Created", delete = r.OperationCode = "ItemUnit.Deleted", edit = r.OperationCode = "ItemUnit.Updated"
            Permission(cn, tx, r.Context)
            If create Then LockAllocation(cn, tx)
            Dim itemId = Integer.Parse(r.EntityId, CultureInfo.InvariantCulture), target = CInt(Db(r, "TargetItemUnitId"))
            Dim before = Capture(cn, tx, itemId, target)
            If Fingerprint(before) <> CStr(Db(r, "ExpectedItemUnitSnapshot")) Then Throw New InvalidOperationException("تغير الصنف أو وحداته أو مكوناته؛ حدّث العرض قبل المتابعة.")
            If Not create AndAlso Not before.Units.Rows.Cast(Of DataRow)().Any(Function(x) CInt(x("U_IM_ID")) = target) Then Throw New InvalidOperationException("الرابط لا يتبع الصنف المحدد.")
            If create Then
                If AuditSalesQuantity.Rows(cn, tx, "SELECT U_ID FROM dbo.Units WITH(HOLDLOCK) WHERE U_ID=@P0", CInt(Db(r, "UnitId"))).Rows.Count <> 1 Then Throw New InvalidOperationException("الوحدة الأساسية غير موجودة.")
            End If
            If (create OrElse edit) AndAlso CBool(Db(r, "CheckBarcode")) AndAlso Not IsDBNull(Db(r, "UnitBarcode")) AndAlso Not String.IsNullOrWhiteSpace(CStr(Db(r, "UnitBarcode"))) Then
                ' Preserve IM_Check_Barcode's original 30-character parameter and exact policy.
                Using check = AuditRequestCoordinator.Command("dbo.IM_Check_Barcode", cn, tx)
                    check.Parameters.Add("@F", SqlDbType.Int).Direction = ParameterDirection.Output
                    check.Parameters.Add("@Barcode", SqlDbType.NVarChar, 30).Value = Db(r, "UnitBarcode")
                    check.Parameters.Add("@IM_ID", SqlDbType.Int).Value = itemId
                    check.ExecuteNonQuery()
                    If CInt(check.Parameters("@F").Value) > 0 Then Throw New InvalidOperationException("باركود متكرر.")
                End Using
            End If
            Dim procedure = If(create, "IM_Units_insert", If(delete, "IM_Units_DELETE", If(edit, "IM_Units_UpdatePrice", If(r.OperationCode = "ItemUnit.PriceChanged", "IM_Units_UpdatePriceBy_Val", "IM_Units_Update_OFFER_Price"))))
            Using cmd = AuditRequestCoordinator.Command("dbo." & procedure, cn, tx)
                cmd.Parameters.Add(If(create, "@IM_ID", "@U_IM_ID"), SqlDbType.Int).Value = If(create, itemId, target)
                If Not delete Then AddMoney(cmd, "@Price", Db(r, If(r.OperationCode = "ItemUnit.OfferPriceChanged", "OfferPrice", "UnitPrice")))
                If create OrElse edit Then
                    AddMoney(cmd, "@Min_SP", Db(r, "MinimumPrice"))
                    AddMoney(cmd, "@Min_SP_2", Db(r, "MinimumPrice2"))
                    cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, If(create, -1, 50)).Value = Db(r, "UnitBarcode")
                End If
                If create Then
                    cmd.Parameters.Add("@U_ID", SqlDbType.Int).Value = Db(r, "UnitId")
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = r.Context.ActorUserId.Value
                    cmd.Parameters.Add("@Row_Enabled", SqlDbType.Bit).Value = Db(r, "UnitRowEnabled")
                ElseIf edit Then
                    cmd.Parameters.Add("@is_Default", SqlDbType.Bit).Value = Db(r, "MakeDefault")
                End If
                cmd.ExecuteNonQuery()
            End Using
            AuditRequestCoordinator.RequireTransaction(cn, tx)
            Dim after = Capture(cn, tx, itemId, target)
            If create Then
                Dim keys = before.Units.Rows.Cast(Of DataRow)().Select(Function(x) CInt(x("U_IM_ID"))).ToArray()
                Dim added = after.Units.Rows.Cast(Of DataRow)().Where(Function(x) Not keys.Contains(CInt(x("U_IM_ID")))).ToArray()
                If added.Length <> 1 Then Throw New InvalidOperationException("لم يؤكد الإجراء إضافة رابط وحدة واحد.")
                target = CInt(added(0)("U_IM_ID"))
            ElseIf delete Then
                If after.Units.Rows.Cast(Of DataRow)().Any(Function(x) CInt(x("U_IM_ID")) = target) OrElse after.Recipes.Rows.Cast(Of DataRow)().Any(Function(x) CInt(x("U_IM_ID")) = target) Then Throw New InvalidOperationException("لم يؤكد الإجراء حذف الرابط ومكوناته.")
            ElseIf Not after.Units.Rows.Cast(Of DataRow)().Any(Function(x) CInt(x("U_IM_ID")) = target) Then
                Throw New InvalidOperationException("اختفى رابط الوحدة أثناء التعديل.")
            End If
            Dim occurred = AuditRequestCoordinator.ServerUtcNow(cn, tx)
            Dim part As Func(Of Integer, AuditEventRequest) = Function(n) New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = n, .EventCode = r.OperationCode, .Outcome = 1,
                .EntityType = "CatalogItem", .EntityId = r.EntityId, .DocumentNumber = target.ToString(CultureInfo.InvariantCulture), .OccurredAtUtc = occurred}
            Dim events As New List(Of AuditEventRequest) From {part(1)}
            Append(events(0), "CatalogItem", r.EntityId, before.Item.Rows(0), after.Item.Rows(0))
            Dim detail As AuditEventRequest = Nothing
            Dim oldTables = {before.Units, before.Recipes}, newTables = {after.Units, after.Recipes}, entities = {"ItemUnit", "ItemUnitRecipe"}, keyColumns = {"U_IM_ID", "T_ID"}
            For n = 0 To entities.Length - 1
                Dim keyColumn = keyColumns(n)
                Dim oldRows = oldTables(n).Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x(keyColumn)))
                Dim newRows = newTables(n).Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x(keyColumn)))
                For Each key In oldRows.Keys.Union(newRows.Keys).OrderBy(Function(x) x)
                    If detail Is Nothing OrElse detail.Changes.Count + AuditContract.ItemUnitFields(entities(n)).Length > 100 Then
                        detail = part(events.Count + 1) : events.Add(detail)
                    End If
                    Append(detail, entities(n), key.ToString(CultureInfo.InvariantCulture), If(oldRows.ContainsKey(key), oldRows(key), Nothing), If(newRows.ContainsKey(key), newRows(key), Nothing))
                Next
            Next
            For Each e In events
                AuditContract.EventData(r, e)
            Next
            For Each e In events
                AuditWriter.WriteInTransaction(cn, tx, lease, e)
            Next
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1,
                New Dictionary(Of String, String) From {{"EntityId", target.ToString(CultureInfo.InvariantCulture)}})}
        Catch
            lease.Faulted = True : Throw
        End Try
    End Function

    Private Shared Sub Append(e As AuditEventRequest, entity As String, id As String, before As DataRow, after As DataRow)
        For Each definition In AuditContract.ItemUnitFields(entity)
            Dim p = definition.Split("|"c)
            e.Changes.Add(New AuditFieldChange With {.ChangeNo = e.Changes.Count + 1, .EntityType = entity, .EntityId = id, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1,
                .Before = If(before Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), before(p(1)))),
                .After = If(after Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), after(p(1))))})
        Next
    End Sub

    Friend Shared Function Save(connection As String, request As AuditOperationRequest) As AuditRequestResult
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Dim result = ExecuteInTransaction(cn, tx, request)
                    tx.Commit() : Return result
                Catch
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
