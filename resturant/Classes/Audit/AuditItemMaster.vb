Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

Friend NotInheritable Class AuditItemMaster
    Friend Shared Function Operation(command As SqlCommand) As String
        Select Case command.CommandText.Trim("["c, "]"c).ToLowerInvariant()
            Case "item_menu_delete" : Return "Item.Deleted"
            Case "item_menu_update_on_mange_form" : Return "Item.FastUpdated"
            Case Else : Throw New ArgumentException("مسار بطاقة الصنف غير معتمد.")
        End Select
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, context As AuditEventContext)
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) WHERE u.User_ID=@User AND u.is_Allow=1 AND EXISTS(SELECT 1 FROM dbo.Users_Validations_V v WITH(HOLDLOCK) WHERE v.User_ID=u.User_ID AND v.IM=1)", cn, tx)
            cmd.Parameters.Add("@User", SqlDbType.Int).Value = context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("المستخدم غير مسموح له أو لا يملك صلاحية إدارة الأصناف.")
        End Using
    End Sub

    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, id As Integer) As Dictionary(Of String, DataTable)
        Dim result As New Dictionary(Of String, DataTable)(StringComparer.Ordinal)
        Dim tables = {"IM_Menu", "IM_Units", "IM_QTY_ALERT", "IM_Formating_Menu", "ST_QTY_Deferences"}
        Dim entities = AuditContract.ItemMasterEntities()
        For i = 0 To entities.Length - 1
            Dim entity = entities(i), key = If(i = 0, "IM_ID", If(i = 1, "U_IM_ID", "T_ID"))
            Dim columns = AuditContract.ItemMasterFields(entity).Select(Function(x) x.Split("|"c)(1)).Where(Function(x) x <> "PhotoSha256" AndAlso x <> "PhotoBytes").Select(Function(x) "[" & x & "]")
            Dim rows = AuditSalesQuantity.Rows(cn, tx, "SELECT [" & key & "]," & String.Join(",", columns) & If(i = 0, ",[Photo]", "") & " FROM dbo.[" & tables(i) & "] WITH(UPDLOCK,HOLDLOCK) WHERE IM_ID=@P0 ORDER BY [" & key & "]", id)
            If i = 0 Then
                rows.Columns.Add("PhotoSha256", GetType(String)) : rows.Columns.Add("PhotoBytes", GetType(Long))
                For Each row As DataRow In rows.Rows
                    If Not IsDBNull(row("Photo")) Then
                        Dim bytes = DirectCast(row("Photo"), Byte())
                        row("PhotoSha256") = BitConverter.ToString(AuditCanonical.Hash(bytes)).Replace("-", "")
                        row("PhotoBytes") = bytes.LongLength
                    End If
                Next
                rows.Columns.Remove("Photo") ' Derived metadata only; no image bytes in requests or audit.
            End If
            result.Add(entity, rows)
        Next
        Return result
    End Function

    Friend Shared Function Fingerprint(snapshot As Dictionary(Of String, DataTable)) As String
        Return BitConverter.ToString(AuditCanonical.Hash(AuditCanonical.Encode(AuditContract.ItemMasterEntities().Select(Function(e) AuditSalesQuantity.Fingerprint(snapshot(e)))))).Replace("-", "")
    End Function

    Friend Shared Function Read(connection As String, context As AuditEventContext, command As SqlCommand) As Dictionary(Of String, DataTable)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Permission(cn, tx, context)
                Dim snapshot = Capture(cn, tx, Convert.ToInt32(AuditItemUnits.Parameter(command, "IM_ID")))
                If snapshot("CatalogItemRecord").Rows.Count <> 1 Then Throw New InvalidOperationException("الصنف غير موجود أو معرفه مكرر.")
                tx.Commit() : Return snapshot
            End Using
        End Using
    End Function

    Friend Shared Function Request(context As AuditEventContext, command As SqlCommand, before As Dictionary(Of String, DataTable)) As AuditOperationRequest
        Dim op = Operation(command)
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = op,
            .EntityType = "CatalogItemRecord", .EntityId = CInt(before("CatalogItemRecord").Rows(0)("IM_ID")).ToString(CultureInfo.InvariantCulture)}
        For Each name In AuditContract.ItemMasterInputs(op)
            Dim value As Object
            Select Case name
                Case "ExpectedItemSnapshot" : value = Fingerprint(before)
                Case "ItemName" : value = AuditItemUnits.Parameter(command, "item_name")
                Case "ItemSalesName" : value = AuditItemUnits.Parameter(command, "item_nameSales")
                Case "ItemGroupId" : value = AuditItemUnits.Parameter(command, "GM_ID")
                Case "ItemActive" : value = AuditItemUnits.Parameter(command, "isActive")
                Case "ItemNumber" : value = AuditItemUnits.Parameter(command, "IM_NUM")
                Case "DefaultUnitId" : value = AuditItemUnits.Parameter(command, "Def_U_ID")
                Case "RequestedUnitPrice" : value = AuditItemUnits.Parameter(command, "Price_ONE", DBNull.Value)
                Case Else : Throw New ArgumentException("مدخل بطاقة صنف غير معتمد.")
            End Select
            Dim type = AuditContract.FieldType(op, name)
            If Not IsDBNull(value) Then
                Select Case type
                    Case "Integer" : value = Convert.ToInt32(value, CultureInfo.CurrentCulture)
                    Case "Decimal" : value = Convert.ToDecimal(value, CultureInfo.CurrentCulture)
                    Case "Boolean" : value = Convert.ToBoolean(value, CultureInfo.CurrentCulture)
                End Select
            End If
            r.Inputs.Add(New AuditInput With {.Name = name, .ValueType = type, .Value = AuditValue.Capture(type, value)})
        Next
        If op = "Item.FastUpdated" AndAlso Not IsDBNull(AuditItemUnits.Parameter(command, "photo", DBNull.Value)) Then Throw New ArgumentException("مسار التعديل السريع لا يستقبل صورة.")
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

    Private Shared Sub LockUnitAllocation(cn As SqlConnection, tx As SqlTransaction, itemId As Integer)
        Dim ranges = AuditSalesQuantity.Rows(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)")
        If ranges.Rows.Count <> 1 OrElse IsDBNull(ranges.Rows(0)(0)) OrElse IsDBNull(ranges.Rows(0)(1)) Then Throw New InvalidOperationException("نطاق ترقيم روابط الوحدات غير محدد بشكل منفرد.")
        Dim first = CInt(ranges.Rows(0)(0)), last = CInt(ranges.Rows(0)(1))
        Dim used = AuditSalesQuantity.Rows(cn, tx, "SELECT U_IM_ID FROM dbo.IM_Units WITH(UPDLOCK,HOLDLOCK) WHERE U_IM_ID BETWEEN @P0 AND @P1 ORDER BY U_IM_ID", first, last)
        Dim links = AuditSalesQuantity.Rows(cn, tx, "SELECT u.U_IM_ID,u.is_Default,b.U_Cargo FROM dbo.IM_Units u WITH(UPDLOCK,HOLDLOCK) LEFT JOIN dbo.Units b WITH(HOLDLOCK) ON b.U_ID=u.U_ID WHERE u.IM_ID=@P0", itemId)
        Dim needsNew = links.Rows.Count = 0 OrElse (links.Rows.Count = 1 AndAlso (IsDBNull(links.Rows(0)("is_Default")) OrElse CInt(links.Rows(0)("is_Default")) <> 1) AndAlso Not IsDBNull(links.Rows(0)("U_Cargo")) AndAlso CDec(links.Rows(0)("U_Cargo")) > 1D)
        If Not needsNew Then Return
        Dim maximum = If(used.Rows.Count = 0, first, CInt(used.Rows(used.Rows.Count - 1)(0)))
        If first >= last OrElse maximum >= last Then Throw New InvalidOperationException("نطاق روابط الوحدات ممتلئ أو غير صالح.")
        If AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID FROM dbo.IM_Struct_Menu WITH(UPDLOCK,HOLDLOCK) WHERE U_IM_ID=@P0", maximum + 1).Rows.Count > 0 Then Throw New InvalidOperationException("رقم الوحدة الجديدة مرتبط بمكونات يتيمة؛ يلزم مراجعتها.")
    End Sub

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse Not AuditContract.IsItemMaster(request.OperationCode) Then Throw New ArgumentException("يلزم عقد بطاقة الصنف ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, id = Integer.Parse(r.EntityId, CultureInfo.InvariantCulture), delete = r.OperationCode = "Item.Deleted"
            Permission(cn, tx, r.Context)
            If Not delete Then LockUnitAllocation(cn, tx, id)
            Dim before = Capture(cn, tx, id)
            If before("CatalogItemRecord").Rows.Count <> 1 OrElse Fingerprint(before) <> CStr(Db(r, "ExpectedItemSnapshot")) Then Throw New InvalidOperationException("تغير الصنف أو بياناته التابعة؛ حدّث العرض قبل المتابعة.")
            Using cmd = AuditRequestCoordinator.Command(If(delete, "dbo.item_menu_delete", "dbo.item_menu_update_On_Mange_Form"), cn, tx)
                cmd.Parameters.Add("@IM_ID", SqlDbType.Int).Value = id
                If Not delete Then
                    cmd.Parameters.Add("@item_name", SqlDbType.NVarChar, 200).Value = Db(r, "ItemName")
                    cmd.Parameters.Add("@item_nameSales", SqlDbType.NVarChar, 100).Value = Db(r, "ItemSalesName")
                    cmd.Parameters.Add("@GM_ID", SqlDbType.Int).Value = Db(r, "ItemGroupId")
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = Db(r, "ItemActive")
                    cmd.Parameters.Add("@IM_NUM", SqlDbType.NVarChar, 500).Value = Db(r, "ItemNumber")
                    cmd.Parameters.Add("@Def_U_ID", SqlDbType.Int).Value = Db(r, "DefaultUnitId")
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = r.Context.ActorUserId.Value
                    cmd.Parameters.AddWithValue("@Price_ONE", Db(r, "RequestedUnitPrice"))
                End If
                cmd.ExecuteNonQuery()
            End Using
            AuditRequestCoordinator.RequireTransaction(cn, tx)
            Dim after = Capture(cn, tx, id)
            If after("CatalogItemRecord").Rows.Count <> If(delete, 0, 1) Then Throw New InvalidOperationException("لم يؤكد الإجراء نتيجة بطاقة الصنف.")
            Dim occurred = AuditRequestCoordinator.ServerUtcNow(cn, tx)
            Dim events As New List(Of AuditEventRequest)
            Dim current As AuditEventRequest = Nothing
            For Each entity In AuditContract.ItemMasterEntities()
                Dim oldRows = before(entity).Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x(0)))
                Dim newRows = after(entity).Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x(0)))
                For Each key In oldRows.Keys.Union(newRows.Keys).OrderBy(Function(x) x)
                    Dim oldRow = If(oldRows.ContainsKey(key), oldRows(key), Nothing), newRow = If(newRows.ContainsKey(key), newRows(key), Nothing)
                    If current Is Nothing OrElse current.Changes.Count + AuditContract.ItemMasterFields(entity).Length > 100 OrElse (current.EventOrdinal = 1 AndAlso entity <> "CatalogItemRecord") Then
                        current = New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = events.Count + 1, .EventCode = r.OperationCode, .Outcome = 1,
                            .EntityType = r.EntityType, .EntityId = r.EntityId, .DocumentNumber = r.EntityId, .OccurredAtUtc = occurred}
                        events.Add(current)
                    End If
                    For Each definition In AuditContract.ItemMasterFields(entity)
                        Dim p = definition.Split("|"c)
                        current.Changes.Add(New AuditFieldChange With {.ChangeNo = current.Changes.Count + 1, .EntityType = entity, .EntityId = key.ToString(CultureInfo.InvariantCulture), .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3),
                            .ValueOrigin = CByte(If(p(3) = "image", 3, 1)), .Before = If(oldRow Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), oldRow(p(1)))),
                            .After = If(newRow Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), newRow(p(1))))})
                    Next
                Next
            Next
            For Each e In events
                AuditContract.EventData(r, e)
            Next
            For Each e In events
                AuditWriter.WriteInTransaction(cn, tx, lease, e)
            Next
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", r.EntityId}})}
        Catch
            lease.Faulted = True : Throw
        End Try
    End Function

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
