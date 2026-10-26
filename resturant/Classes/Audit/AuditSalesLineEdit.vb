Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Xml.Linq

Friend NotInheritable Class AuditSalesLineSnapshot
    Friend Header As DataRow
    Friend Line As DataRow
End Class

Friend NotInheritable Class AuditSalesLineEdit
    Private Shared ReadOnly HeaderExtra As String() = {"T_ID", "SB_ID", "Date", "BsType_ID", "S_Bills_Type", "EditOpenedAt", "EditOpenedBy"}

    Friend Shared Sub Exact(value As Decimal, positive As Boolean)
        If Decimal.Round(value, 3) <> value OrElse value > 999999999999999.999D OrElse value < 0D OrElse (positive AndAlso value = 0D) Then Throw New ArgumentException("القيمة خارج الدقة المسموحة؛ لا تقرّب بصمت.")
    End Sub

    Private Shared Function One(cn As SqlConnection, tx As SqlTransaction, sql As String, id As Integer) As DataRow
        Using cmd As New SqlCommand(sql, cn, tx)
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id
            Dim dt As New DataTable()
            Using rd = cmd.ExecuteReader()
                dt.Load(rd)
            End Using
            If dt.Rows.Count <> 1 Then Throw New InvalidOperationException("السطر أو المرجع غير موجود أو غير فريد.")
            Return dt.Rows(0)
        End Using
    End Function

    Friend Shared Function Header(cn As SqlConnection, tx As SqlTransaction, id As Integer) As DataRow
        Dim cols = HeaderExtra.Concat(AuditContract.SalesHeaderFields().Select(Function(x) x.Split("|"c)(1))).Distinct().Select(Function(x) "[" & x & "]")
        Return One(cn, tx, "SELECT " & String.Join(",", cols) & " FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@Id", id)
    End Function

    Friend Shared Function Line(cn As SqlConnection, tx As SqlTransaction, id As Integer) As DataRow
        Dim cols = AuditContract.SalesLineFields().Where(Function(x) Not x.StartsWith("StockQuantity|")).Select(Function(x) "[" & x.Split("|"c)(1) & "]")
        Return One(cn, tx, "SELECT T_ID,Bill_T_ID," & String.Join(",", cols) & " FROM dbo.SB_Contents WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@Id", id)
    End Function

    Friend Shared Function Fingerprint(row As DataRow) As String
        Dim xml As New XElement("Row")
        For Each col As DataColumn In row.Table.Columns
            If col.ColumnName = "AuditStock" Then Continue For
            Dim v = row(col)
            Dim t = If(col.DataType Is GetType(Double), "Float", If(col.DataType Is GetType(Decimal), "Decimal", If(col.DataType Is GetType(DateTime), "DateTime", If(col.DataType Is GetType(Boolean), "Boolean", If(col.DataType Is GetType(Integer) OrElse col.DataType Is GetType(Long), "Integer", "String")))))
            Dim value = AuditValue.Capture(t, v)
            xml.Add(New XElement(col.ColumnName, New XAttribute("state", CInt(value.State)), value.Text))
        Next
        Return BitConverter.ToString(AuditCanonical.Hash(Encoding.UTF8.GetBytes(xml.ToString(SaveOptions.DisableFormatting)))).Replace("-", "")
    End Function

    Friend Shared Function AssertWritable(cn As SqlConnection, tx As SqlTransaction, headerValue As DataRow, context As AuditEventContext, onUpdate As Boolean) As DataRow
        If IsDBNull(headerValue("BsType_ID")) OrElse CInt(headerValue("BsType_ID")) <> 1 OrElse IsDBNull(headerValue("isVoid")) OrElse CInt(headerValue("isVoid")) <> 0 Then Throw New InvalidOperationException("فاتورة البيع غير صالحة أو ملغاة.")
        Dim permissions = One(cn, tx, "SELECT u.is_Allow,u.isAdmin,v.Sales,v.SB_Update,v.SB_IM_Update,v.Sales_Dis,v.SB_Sell_Under_Cost,v.Sell_Under_Min_SP FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@Id", context.ActorUserId.Value)
        If Not Flag(permissions, "is_Allow") OrElse Not Flag(permissions, "Sales") Then Throw New UnauthorizedAccessException("صلاحية المبيعات غير متاحة للمستخدم.")
        If IsDBNull(headerValue("isDepended")) OrElse Not {0, 1}.Contains(CInt(headerValue("isDepended"))) Then Throw New InvalidOperationException("حالة اعتماد الفاتورة غير معروفة.")
        If CInt(headerValue("isDepended")) = 1 OrElse onUpdate Then
            If Not onUpdate OrElse Not Flag(permissions, "SB_Update") Then Throw New UnauthorizedAccessException("يلزم فتح تعديل الفاتورة وصلاحية التعديل.")
            If IsDBNull(headerValue("EditOpenedAt")) OrElse IsDBNull(headerValue("EditOpenedBy")) OrElse CInt(headerValue("EditOpenedBy")) <> context.ActorUserId.Value Then Throw New InvalidOperationException("افتح تعديل الفاتورة بالمستخدم الحالي أولًا.")
            Using cmd As New SqlCommand("SELECT TOP(1) ActorUserId,SessionId FROM dbo.Network_Audit_Event WITH(HOLDLOCK) WHERE SourceId=@Source AND EntityType='SalesInvoice' AND EntityId=@Id AND EventCode='Sales.EditOpened' ORDER BY LocalSequence DESC", cn, tx)
                cmd.Parameters.Add("@Source", SqlDbType.UniqueIdentifier).Value = context.SourceId
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar, 128).Value = CInt(headerValue("T_ID")).ToString(CultureInfo.InvariantCulture)
                Using rd = cmd.ExecuteReader()
                    If Not rd.Read() OrElse IsDBNull(rd("ActorUserId")) OrElse IsDBNull(rd("SessionId")) OrElse CInt(rd("ActorUserId")) <> context.ActorUserId.Value OrElse CType(rd("SessionId"), Guid) <> context.SessionId.Value Then Throw New InvalidOperationException("فتح التعديل لا يتبع جلسة الدخول الحالية.")
                End Using
            End Using
        End If
        Return permissions
    End Function

    Private Shared Function Flag(row As DataRow, name As String) As Boolean
        Return Not IsDBNull(row(name)) AndAlso Convert.ToBoolean(row(name), CultureInfo.InvariantCulture)
    End Function

    Friend Shared Function ReadForEdit(connection As String, context As AuditEventContext, billId As Integer, lineId As Integer, onUpdate As Boolean) As AuditSalesLineSnapshot
        AuditContract.Context(context, "Sales.LineDetailsChanged")
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction()
                Dim result = ReadInTransaction(cn, tx, context, billId, lineId, onUpdate)
                tx.Commit()
                Return result
            End Using
        End Using
    End Function

    Friend Shared Function ReadInTransaction(cn As SqlConnection, tx As SqlTransaction, context As AuditEventContext, billId As Integer, lineId As Integer, onUpdate As Boolean) As AuditSalesLineSnapshot
        Dim h = Header(cn, tx, billId)
        AssertWritable(cn, tx, h, context, onUpdate)
        Dim l = Line(cn, tx, lineId)
        If CInt(l("Bill_T_ID")) <> billId Then Throw New InvalidOperationException("السطر لا يتبع الفاتورة المحددة.")
        Fingerprint(h) : Fingerprint(l)
        Return New AuditSalesLineSnapshot With {.Header = h, .Line = l}
    End Function

    Private Shared Sub Input(r As AuditOperationRequest, name As String, value As Object)
        Dim t = AuditContract.FieldType(r.OperationCode, name)
        r.Inputs.Add(New AuditInput With {.Name = name, .ValueType = t, .Value = AuditValue.Capture(t, value)})
    End Sub

    Friend Shared Function Request(context As AuditEventContext, snapshot As AuditSalesLineSnapshot, parent As String, price As Decimal,
                                   onUpdate As Boolean, allowNegative As Boolean, enforceMinimum As Boolean,
                                   Optional unitId As Integer? = Nothing, Optional quantity As Decimal? = Nothing,
                                   Optional discount As Double? = Nothing, Optional notes As String = Nothing) As AuditOperationRequest
        If snapshot Is Nothing Then Throw New ArgumentException("لقطة السطر السابقة مطلوبة.")
        Exact(price, False)
        Dim isDetails = unitId.HasValue
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = If(isDetails, "Sales.LineDetailsChanged", "Sales.LinePriceChanged"), .EntityType = "SalesInvoice", .EntityId = CInt(snapshot.Header("T_ID")).ToString(CultureInfo.InvariantCulture)}
        Input(r, "LineId", CLng(snapshot.Line("T_ID")))
        Input(r, "ExpectedLineSnapshot", Fingerprint(snapshot.Line)) : Input(r, "ExpectedHeaderSnapshot", Fingerprint(snapshot.Header))
        Input(r, "ParentScreen", parent) : Input(r, "OnUpdate", onUpdate) : Input(r, "AllowNegative", allowNegative) : Input(r, "EnforceMinimum", enforceMinimum)
        Input(r, "RequestedPrice", price)
        If isDetails Then
            If unitId.Value <= 0 OrElse Not quantity.HasValue OrElse Not discount.HasValue OrElse notes Is Nothing OrElse discount.Value < 0 Then Throw New ArgumentException("تفاصيل الصنف غير صالحة.")
            Exact(quantity.Value, True)
            Input(r, "UnitId", unitId.Value) : Input(r, "RequestedQuantity", quantity.Value) : Input(r, "RequestedDiscount", discount.Value) : Input(r, "LineNotes", notes)
        End If
        AuditContract.Request(r)
        Return r
    End Function

    Private Shared Function V(r As AuditOperationRequest, name As String) As String
        Return r.Inputs.Single(Function(x) x.Name = name).Value.Text
    End Function

    Friend Shared Function Stock(cn As SqlConnection, tx As SqlTransaction, l As DataRow) As Double
        Using cmd As New SqlCommand("SELECT CONVERT(float,ISNULL(SUM(QTY),0)) FROM dbo.ST_Balance_V WITH(HOLDLOCK) WHERE IM_ID=@Item AND ST_ID=@Store AND ISNULL(D_Vaild,N'')=@Expiry", cn, tx)
            cmd.Parameters.Add("@Item", SqlDbType.Int).Value = l("IM_ID")
            cmd.Parameters.Add("@Store", SqlDbType.BigInt).Value = l("ST_ID")
            cmd.Parameters.Add("@Expiry", SqlDbType.NVarChar, 50).Value = If(IsDBNull(l("D_Vaild")), "", CStr(l("D_Vaild")))
            Return CDbl(cmd.ExecuteScalar())
        End Using
    End Function

    Friend Shared Sub AddChanges(item As AuditEventRequest, before As DataRow, after As DataRow, definitions As String(), entity As String, id As String)
        For Each definition In definitions
            Dim p = definition.Split("|"c)
            item.Changes.Add(New AuditFieldChange With {.ChangeNo = item.Changes.Count + 1, .EntityType = entity, .EntityId = id, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1, .Before = AuditValue.Capture(p(2), before(p(1))), .After = AuditValue.Capture(p(2), after(p(1)))})
        Next
    End Sub

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, operation As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable Then Throw New InvalidOperationException("تعديل سطر البيع يتطلب معاملة Serializable.")
        AuditContract.Request(operation)
        If Not AuditContract.IsSalesLine(operation.OperationCode) Then Throw New ArgumentException("عملية سطر غير معتمدة.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, operation)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request
            Dim billId = Integer.Parse(r.EntityId, CultureInfo.InvariantCulture), lineId = Integer.Parse(V(r, "LineId"), CultureInfo.InvariantCulture)
            Dim h = Header(cn, tx, billId)
            Dim permissions = AssertWritable(cn, tx, h, r.Context, V(r, "OnUpdate") = "1")
            Dim l = Line(cn, tx, lineId)
            If CInt(l("Bill_T_ID")) <> billId OrElse Fingerprint(l) <> V(r, "ExpectedLineSnapshot") OrElse Fingerprint(h) <> V(r, "ExpectedHeaderSnapshot") Then Throw New InvalidOperationException("تغير السطر أو الفاتورة منذ العرض؛ أغلق النافذة وأعد فتحها قبل التأكيد.")
            Dim price = Decimal.Parse(V(r, "RequestedPrice"), CultureInfo.InvariantCulture)
            Exact(price, False)
            Dim details = r.OperationCode = "Sales.LineDetailsChanged"
            Dim unitId = If(details, Integer.Parse(V(r, "UnitId"), CultureInfo.InvariantCulture), CInt(l("U_ID")))
            Dim unit = One(cn, tx, "SELECT U_ID,U_Cargo,Cost,Price,Min_SP FROM dbo.IM_Menu_Units_V WITH(HOLDLOCK) WHERE IM_ID=" & CInt(l("IM_ID")).ToString(CultureInfo.InvariantCulture) & " AND U_ID=@Id", unitId)
            Dim menu = One(cn, tx, "SELECT isChangePrice,isStore FROM dbo.IM_Menu WITH(HOLDLOCK) WHERE IM_ID=@Id", CInt(l("IM_ID")))
            Dim stocked = Not IsDBNull(menu("isStore")) AndAlso {1, 2}.Contains(CInt(menu("isStore")))
            If IsDBNull(unit("U_Cargo")) OrElse CDbl(unit("U_Cargo")) <= 0 Then Throw New InvalidOperationException("حمولة الوحدة غير صالحة.")
            If Not Flag(permissions, "SB_Sell_Under_Cost") AndAlso price < CDec(If(IsDBNull(unit("Cost")), 0R, CDbl(unit("Cost"))) * CDbl(unit("U_Cargo"))) Then Throw New UnauthorizedAccessException("لا تملك صلاحية البيع بأقل من التكلفة الحالية.")
            If details Then
                If Not Flag(permissions, "SB_IM_Update") AndAlso price <> If(unitId = CInt(l("U_ID")), CDec(l("Price")), CDec(unit("Price"))) Then Throw New UnauthorizedAccessException("صلاحية تعديل سعر الصنف غير متاحة.")
                Dim discount = Double.Parse(V(r, "RequestedDiscount"), CultureInfo.InvariantCulture)
                If Double.IsNaN(discount) OrElse Double.IsInfinity(discount) OrElse discount < 0 Then Throw New ArgumentException("خصم الصنف غير صالح.")
                If Not Flag(permissions, "Sales_Dis") AndAlso (IsDBNull(l("IM_Discount")) OrElse discount <> CDbl(l("IM_Discount"))) Then
                    If discount <> 0R OrElse (Not IsDBNull(l("IM_Discount")) AndAlso CDbl(l("IM_Discount")) <> 0R) Then Throw New UnauthorizedAccessException("صلاحية خصم الصنف غير متاحة.")
                End If
                Exact(Decimal.Parse(V(r, "RequestedQuantity"), CultureInfo.InvariantCulture), True)
            Else
                If Not Flag(menu, "isChangePrice") Then Throw New UnauthorizedAccessException("الصنف لا يسمح بتغيير السعر.")
                If V(r, "EnforceMinimum") = "1" AndAlso Not Flag(permissions, "isAdmin") AndAlso Flag(permissions, "Sell_Under_Min_SP") AndAlso Not IsDBNull(unit("Min_SP")) AndAlso price < CDec(unit("Min_SP")) Then Throw New UnauthorizedAccessException("لا يمكنك البيع بأقل من أدنى سعر بيع.")
            End If
            l.Table.Columns.Add("AuditStock", GetType(Double)) : l("AuditStock") = Stock(cn, tx, l)
            Dim componentBefore As DataTable = Nothing
            Dim componentBuckets As SortedDictionary(Of String, DataRow) = Nothing
            Dim rebuildComponents = details AndAlso (unitId <> CInt(l("U_ID")) OrElse Decimal.Parse(V(r, "RequestedQuantity"), CultureInfo.InvariantCulture) <> CDec(l("QTY")))
            If rebuildComponents Then
                If tx.IsolationLevel <> IsolationLevel.Serializable Then Throw New InvalidOperationException("تعديل كمية/وحدة الصنف يتطلب معاملة Serializable.")
                Using version As New SqlCommand("SELECT ISNULL(CHARINDEX('AuditDetailComponents016',OBJECT_DEFINITION(OBJECT_ID('dbo.SB_IM_Unit_Change'))),0)", cn, tx)
                    If CInt(version.ExecuteScalar()) = 0 Then Throw New InvalidOperationException("إجراء تعديل مكونات الصنف غير محدث؛ طبّق الترقية 016 قبل المتابعة.")
                End Using
                componentBefore = AuditSalesQuantity.Structures(cn, tx, lineId)
                Dim recipe = AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID,IM_ID,U_IM_ID,Struct_IM_ID,U_ID,Qty,U_Cargo,IM_U_ID FROM dbo.IM_Struct_Menu_V WITH(HOLDLOCK) WHERE IM_ID=@P0 AND IM_U_ID=@P1 ORDER BY T_ID", CInt(l("IM_ID")), unitId)
                componentBuckets = AuditSalesQuantity.Buckets(New AuditSalesQuantitySnapshot With {.Sale = New AuditSalesLineSnapshot With {.Header = h, .Line = l}, .Structures = componentBefore, .Recipe = recipe})
                For Each bucket In componentBuckets.Values
                    bucket("AuditStock") = Stock(cn, tx, bucket)
                Next
            End If
            If details AndAlso stocked AndAlso V(r, "AllowNegative") = "0" Then
                Dim cargo = Decimal.Round(CDec(unit("U_Cargo")), 2, MidpointRounding.AwayFromZero)
                Dim wanted = Decimal.Round(cargo * Decimal.Parse(V(r, "RequestedQuantity"), CultureInfo.InvariantCulture), 2, MidpointRounding.AwayFromZero)
                Dim increase = wanted - CDec(l("ST_QTY"))
                If increase > 0 AndAlso CDbl(increase) > CDbl(l("AuditStock")) + 0.0000001R Then Throw New InvalidOperationException("الرصيد الحالي لا يسمح بزيادة الصرف؛ لم يُحفظ التعديل.")
            End If
            Using cmd = AuditRequestCoordinator.Command(If(details, "dbo.SB_IM_Unit_Change", "dbo.IM_Conferm_NewPrice"), cn, tx)
                cmd.Parameters.Add("@IM_T_ID", SqlDbType.Int).Value = lineId
                cmd.Parameters.Add("@On_Update", SqlDbType.Bit).Value = V(r, "OnUpdate") = "1"
                If details Then
                    cmd.Parameters.Add("@SB_T_ID", SqlDbType.Int).Value = billId
                    cmd.Parameters.Add("@U_ID", SqlDbType.Int).Value = unitId
                    Money(cmd, "@QTY", Decimal.Parse(V(r, "RequestedQuantity"), CultureInfo.InvariantCulture))
                    Money(cmd, "@Price", price)
                    cmd.Parameters.Add("@IM_Discount", SqlDbType.Float).Value = Double.Parse(V(r, "RequestedDiscount"), CultureInfo.InvariantCulture)
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, -1).Value = V(r, "LineNotes")
                Else
                    cmd.Parameters.Add("@New_Price", SqlDbType.Float).Value = CDbl(price)
                End If
                cmd.ExecuteNonQuery()
            End Using
            Dim after = Line(cn, tx, lineId), headerAfter = Header(cn, tx, billId)
            If CInt(after("Bill_T_ID")) <> billId Then Throw New InvalidOperationException("تغير مرجع السطر أثناء التنفيذ.")
            after.Table.Columns.Add("AuditStock", GetType(Double)) : after("AuditStock") = Stock(cn, tx, after)
            ' Reject only a new decrease beyond available stock, not an existing deficit being reduced.
            If stocked AndAlso V(r, "AllowNegative") = "0" AndAlso CDbl(after("AuditStock")) < -0.0000001R AndAlso CDbl(after("AuditStock")) < CDbl(l("AuditStock")) - 0.0000001R Then Throw New InvalidOperationException("الرصيد الحالي لا يسمح بزيادة الصرف؛ لم يُحفظ التعديل.")
            Dim item As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = 1, .EventCode = r.OperationCode, .Outcome = 1, .EntityType = "SalesLine", .EntityId = V(r, "LineId"), .DocumentNumber = If(IsDBNull(headerAfter("SB_ID")), Nothing, Convert.ToString(headerAfter("SB_ID"), CultureInfo.InvariantCulture)), .OccurredAtUtc = AuditRequestCoordinator.ServerUtcNow(cn, tx), .BusinessDate = If(IsDBNull(headerAfter("Date")), CType(Nothing, DateTime?), CDate(headerAfter("Date")))}
            AddChanges(item, l, after, AuditContract.SalesLineFields(), "SalesLine", item.EntityId)
            AddChanges(item, h, headerAfter, AuditContract.SalesHeaderFields(), "SalesInvoice", r.EntityId)
            item.RelatedEntities.Add(New AuditEntityReference With {.Role = "Document", .EntityType = "SalesInvoice", .EntityId = r.EntityId})
            Dim events As New List(Of AuditEventRequest) From {item}
            If rebuildComponents Then
                Dim componentAfter = AuditSalesQuantity.Structures(cn, tx, lineId)
                Dim afterScope = AuditSalesQuantity.Buckets(New AuditSalesQuantitySnapshot With {.Sale = New AuditSalesLineSnapshot With {.Header = headerAfter, .Line = after}, .Structures = componentAfter, .Recipe = New DataTable()})
                If afterScope.Keys.Any(Function(x) Not componentBuckets.ContainsKey(x)) Then Throw New InvalidOperationException("ظهر تأثير مكونات خارج نطاق المخزون المقفول.")
                Dim groups As New List(Of List(Of AuditFieldChange))()
                Dim oldRows = componentBefore.Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x("T_ID")))
                Dim newRows = componentAfter.Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x("T_ID")))
                For Each id In oldRows.Keys.Union(newRows.Keys).OrderBy(Function(x) x)
                    groups.Add(AuditSalesQuantity.StructureChanges(If(oldRows.ContainsKey(id), oldRows(id), Nothing), If(newRows.ContainsKey(id), newRows(id), Nothing), id.ToString(CultureInfo.InvariantCulture)))
                Next
                For Each bucket In componentBuckets
                    Dim latest = bucket.Value.Table.Clone().Rows.Add(bucket.Value.ItemArray)
                    latest("AuditStock") = Stock(cn, tx, latest)
                    If V(r, "AllowNegative") = "0" AndAlso CDbl(latest("AuditStock")) < -0.0000001R AndAlso CDbl(latest("AuditStock")) < CDbl(bucket.Value("AuditStock")) - 0.0000001R Then Throw New InvalidOperationException("الرصيد الحالي للصنف أو أحد مكوناته لا يسمح بالتعديل.")
                    If oldRows.Count > 0 OrElse newRows.Count > 0 Then
                        Dim part = AuditSalesQuantity.NewEvent(r, headerAfter, 2, item.OccurredAtUtc)
                        AddChanges(part, bucket.Value, latest, AuditContract.SalesQuantityFields("StockBucket"), "StockBucket", bucket.Key)
                        groups.Add(part.Changes)
                    End If
                Next
                Dim extra As AuditEventRequest = Nothing
                For Each group In groups
                    If extra Is Nothing OrElse extra.Changes.Count + group.Count > 100 Then
                        extra = AuditSalesQuantity.NewEvent(r, headerAfter, events.Count + 1, item.OccurredAtUtc) : events.Add(extra)
                    End If
                    For Each change In group
                        change.ChangeNo = extra.Changes.Count + 1 : extra.Changes.Add(change)
                    Next
                Next
            End If
            Dim changed = events.Any(Function(e) e.Changes.Any(Function(x) x.Before.State <> x.After.State OrElse x.Before.Text <> x.After.Text))
            If changed Then
                For Each part In events
                    AuditContract.EventData(r, part)
                Next
                For Each part In events
                    AuditWriter.WriteInTransaction(cn, tx, lease, part)
                Next
            End If
            Dim code As Byte = If(changed, CByte(1), CByte(5))
            Dim result = AuditRequestCoordinator.Complete(cn, tx, lease, code, New Dictionary(Of String, String) From {{"EntityId", r.EntityId}, {"LineId", item.EntityId}})
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = code, .ResultXml = result}
        Catch
            lease.Faulted = True
            Throw
        End Try
    End Function

    Private Shared Sub Money(cmd As SqlCommand, name As String, value As Decimal)
        Dim p = cmd.Parameters.Add(name, SqlDbType.Decimal) : p.Precision = 18 : p.Scale = 3 : p.Value = value
    End Sub

    Friend Shared Function Save(connection As String, request As AuditOperationRequest) As AuditRequestResult
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Dim result = ExecuteInTransaction(cn, tx, request)
                    tx.Commit() : result.Status = "Completed" : Return result
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
