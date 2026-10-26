Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Xml.Linq

' Settlement line operations and the shared settlement-domain activation check.
Friend NotInheritable Class AuditSettlementLines
    Friend Shared Function IsEnabled(connection As String) As Boolean
        Dim policy = New SqlAuditSecurityStore().Policy(connection)
        If policy.SourceId = Guid.Empty Then Return False ' Verified legacy database only.
        Using cn As New SqlConnection(connection), cmd As New SqlCommand("SELECT EnabledDomainsXml.exist('/Domains/Domain[@code=""Inventory.Settlement""]') FROM dbo.Network_Audit_Source WHERE ConfigId=1", cn)
            cn.Open()
            Dim enabled = ReadEnabledFlag(cmd.ExecuteScalar())
            If enabled Then
                cmd.CommandText = "SELECT CASE WHEN OBJECT_ID('dbo.ST_Settlement_Line_AssertWritable','P') IS NOT NULL AND OBJECT_ID('dbo.ST_Settlement_Header_Apply','P') IS NOT NULL AND OBJECT_ID('dbo.ST_Settlement_CanRemove','P') IS NOT NULL AND EXISTS(SELECT 1 FROM sys.parameters WHERE object_id=OBJECT_ID('dbo.ST_QTY_Deferences_Details_Delete') AND name='@AllowNegative') THEN 1 ELSE 0 END"
                If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New InvalidOperationException("متطلبات حماية التسويات غير مكتملة؛ يجب تثبيت ترقية 008 قبل استخدامها.")
            End If
            Return enabled
        End Using
    End Function

    Friend Shared Function ReadEnabledFlag(value As Object) As Boolean
        ' SQL XML.exist returns bit/Boolean; VB CInt(True) is -1, not 1.
        ' Missing, NULL or unexpected values must not silently select the legacy path.
        If Not TypeOf value Is Boolean Then Throw New InvalidOperationException("تعذر قراءة حالة تفعيل مراقبة التسويات؛ إعداد التفعيل مفقود أو غير صالح.")
        Return DirectCast(value, Boolean)
    End Function

    Private Shared Sub Input(request As AuditOperationRequest, name As String, value As Object)
        Dim valueType = AuditContract.FieldType(request.OperationCode, name)
        request.Inputs.Add(New AuditInput With {.Name = name, .ValueType = valueType, .Value = AuditValue.Capture(valueType, value)})
    End Sub

    Friend Shared Function AddRequest(context As AuditEventContext, documentId As Integer, itemId As Integer, storeId As Integer, unitId As Integer,
                                      barcode As String, targetQuantity As Decimal, signedDifference As Decimal, displayedQuantity As Double,
                                      expiry As DateTime?, Optional allowNegative As Boolean = False) As AuditOperationRequest
        If documentId <= 0 OrElse itemId <= 0 OrElse storeId <= 0 OrElse unitId <= 0 Then Throw New ArgumentException("المستند والصنف والمخزن والوحدة مطلوبة.")
        For Each quantityValue As Decimal In {targetQuantity, signedDifference}
            If Decimal.Round(quantityValue, 3) <> quantityValue OrElse Math.Abs(quantityValue) > 999999999999999.999D Then Throw New ArgumentException("الكمية خارج دقة numeric(18,3)؛ لا تقرب بصمت.")
        Next
        If barcode Is Nothing OrElse barcode.Length > 50 Then Throw New ArgumentException("باركود غير صالح.")
        Dim request = NewRequest(context, documentId, "Settlement.LineAdded")
        Input(request, "ItemId", itemId)
        Input(request, "StoreId", storeId)
        Input(request, "UnitId", unitId)
        Input(request, "Barcode", barcode)
        Input(request, "RequestedQuantity", targetQuantity)
        Input(request, "QuantityDelta", signedDifference)
        Input(request, "DisplayedQuantity", displayedQuantity)
        Input(request, "ExpiryDate", If(expiry.HasValue, CType(DateTime.SpecifyKind(expiry.Value.Date, DateTimeKind.Unspecified), Object), DBNull.Value))
        Input(request, "AllowNegative", allowNegative)
        AuditContract.Request(request)
        Return request
    End Function

    Friend Shared Function UpdateRequest(context As AuditEventContext, documentId As Integer, lineId As Integer,
                                         newDifference As Decimal, expectedSnapshot As String, allowNegative As Boolean) As AuditOperationRequest
        If documentId <= 0 OrElse lineId <= 0 OrElse expectedSnapshot Is Nothing OrElse expectedSnapshot.Length <> 64 Then Throw New ArgumentException("يلزم مستند وسطر ولقطة سابقة صحيحة.")
        ExactQuantity(newDifference)
        Dim request = NewRequest(context, documentId, "Settlement.LineUpdated")
        Input(request, "LineId", lineId)
        Input(request, "QuantityDelta", newDifference)
        Input(request, "ExpectedLineSnapshot", expectedSnapshot)
        Input(request, "AllowNegative", allowNegative)
        AuditContract.Request(request)
        Return request
    End Function

    Friend Shared Function LineSnapshot(rowValue As DataRow) As String
        Dim xml As New XElement("Line")
        For Each name In {"T_ID", "Bill_T_ID", "IM_ID", "ST_ID", "ST_QTY", "Cost", "U_Cargo", "Valid_D", "Barcode", "Total", "DATE"}
            Dim v = rowValue(name)
            Dim valueType = If(TypeOf v Is Double, "Float", If(TypeOf v Is Decimal, "Decimal", If(TypeOf v Is DateTime, "DateTime", If(TypeOf v Is Integer, "Integer", "String"))))
            Dim captured = AuditValue.Capture(valueType, v)
            xml.Add(New XElement(name, New XAttribute("state", CInt(captured.State)), captured.Text))
        Next
        Return BitConverter.ToString(AuditCanonical.Hash(Encoding.UTF8.GetBytes(xml.ToString(SaveOptions.DisableFormatting)))).Replace("-", "")
    End Function

    Private Shared Sub AssertWritable(cn As SqlConnection, tx As SqlTransaction, documentId As Integer, context As AuditEventContext)
        Using cmd = AuditRequestCoordinator.Command("dbo.ST_Settlement_Line_AssertWritable", cn, tx)
            cmd.Parameters.Add("@Bill_T_ID", SqlDbType.Int).Value = documentId
            cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = context.ActorUserId.Value
            cmd.Parameters.Add("@SessionId", SqlDbType.UniqueIdentifier).Value = context.SessionId.Value
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Friend Shared Function ReadEditLine(connection As String, context As AuditEventContext, documentId As Integer, lineId As Integer) As DataRow
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction()
                AssertWritable(cn, tx, documentId, context)
                Dim saved = Line(cn, tx, lineId)
                If saved Is Nothing OrElse CInt(saved("Bill_T_ID")) <> documentId Then Throw New InvalidOperationException("السطر غير موجود في هذا المستند.")
                tx.Commit()
                Return saved
            End Using
        End Using
    End Function

    Private Shared Function DefaultUnit(cn As SqlConnection, tx As SqlTransaction, itemId As Integer, unitId As Integer) As Decimal
        Dim unitRow = Row(cn, tx, "SELECT U_ID,U_Cargo FROM dbo.IM_Menu_Units_V WITH(HOLDLOCK) WHERE IM_ID=@Id AND is_Default=1", itemId)
        If unitRow Is Nothing OrElse IsDBNull(unitRow("U_Cargo")) OrElse CDec(unitRow("U_Cargo")) <= 0D OrElse CInt(unitRow("U_ID")) <> unitId Then Throw New InvalidOperationException("اختر الوحدة الافتراضية للصنف؛ تحويل الوحدات غير معتمد لهذا المسار.")
        Dim cargo = CDec(unitRow("U_Cargo"))
        If Decimal.Round(cargo, 2) <> cargo Then Throw New InvalidOperationException("دقة معامل الوحدة غير مدعومة بإجراء التسوية الحالي.")
        Return cargo
    End Function

    ' HOLDLOCK protects the base stock rows/ranges until the business/audit transaction ends.
    Private Shared Function Stock(cn As SqlConnection, tx As SqlTransaction, itemId As Integer, storeId As Integer, expiryText As String) As Double
        Using cmd As New SqlCommand("SELECT CONVERT(float,ISNULL(SUM(QTY),0)) FROM dbo.ST_Balance_V WITH(HOLDLOCK) WHERE IM_ID=@Item AND ST_ID=@Store AND ISNULL(D_Vaild,N'')=@Expiry", cn, tx)
            cmd.Parameters.Add("@Item", SqlDbType.Int).Value = itemId
            cmd.Parameters.Add("@Store", SqlDbType.Int).Value = storeId
            cmd.Parameters.Add("@Expiry", SqlDbType.NVarChar, 50).Value = expiryText
            Return Convert.ToDouble(cmd.ExecuteScalar(), CultureInfo.InvariantCulture)
        End Using
    End Function

    Friend Shared Function ReadCurrentQuantity(connection As String, itemId As Integer, storeId As Integer, unitId As Integer, expiry As DateTime?) As Double
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction()
                Dim cargo = DefaultUnit(cn, tx, itemId, unitId)
                Dim quantity = Stock(cn, tx, itemId, storeId, If(expiry.HasValue, expiry.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), "")) / CDbl(cargo)
                tx.Commit()
                Return quantity
            End Using
        End Using
    End Function

    Private Shared Sub ExactQuantity(quantity As Decimal)
        If Decimal.Round(quantity, 3) <> quantity OrElse Math.Abs(quantity) > 999999999999999.999D Then Throw New ArgumentException("الكمية خارج دقة numeric(18,3)؛ لا تقرب بصمت.")
    End Sub

    Friend Shared Function VerifiedDifference(target As Decimal, displayed As Double, current As Double) As Decimal
        ExactQuantity(target)
        If Double.IsNaN(displayed) OrElse Double.IsInfinity(displayed) OrElse Math.Abs(displayed - current) > 0.0000001R Then Throw New InvalidOperationException("تغير رصيد الصنف منذ عرضه؛ لم تحفظ التسوية. حدّث الرصيد وأكد الكمية مجددًا.")
        Dim raw = target - CDec(current)
        Dim result = Decimal.Round(raw, 3)
        If Math.Abs(raw - result) > 0.0000001D Then Throw New InvalidOperationException("فرق التسوية لا يمكن حفظه بالدقة المتاحة دون تقريب؛ راجع الوحدة والرصيد.")
        ExactQuantity(result)
        Return result
    End Function

    Friend Shared Function DeleteRequest(context As AuditEventContext, documentId As Integer, lineId As Integer, Optional allowNegative As Boolean = False) As AuditOperationRequest
        If documentId <= 0 OrElse lineId <= 0 Then Throw New ArgumentException("معرف المستند والسطر مطلوبان.")
        Dim request = NewRequest(context, documentId, "Settlement.LineDeleted")
        Input(request, "LineId", lineId)
        Input(request, "AllowNegative", allowNegative)
        AuditContract.Request(request)
        Return request
    End Function

    Private Shared Function NewRequest(context As AuditEventContext, id As Integer, operation As String) As AuditOperationRequest
        Return New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .OperationCode = operation, .Context = context.Copy(),
            .EntityType = "Settlement", .EntityId = id.ToString(CultureInfo.InvariantCulture)}
    End Function

    Private Shared Function Value(request As AuditOperationRequest, name As String) As Object
        Dim inputValue = request.Inputs.Single(Function(x) x.Name = name).Value
        If inputValue.State = AuditValueState.SqlNull Then Return DBNull.Value
        If inputValue.State <> AuditValueState.Present Then Throw New ArgumentException("مدخل التسوية غير ملتقط: " & name)
        Select Case inputValue.ValueType
            Case "Integer" : Return Integer.Parse(inputValue.Text, CultureInfo.InvariantCulture)
            Case "Decimal" : Return Decimal.Parse(inputValue.Text, CultureInfo.InvariantCulture)
            Case "Float" : Return Double.Parse(inputValue.Text, CultureInfo.InvariantCulture)
            Case "DateTime" : Return DateTime.SpecifyKind(DateTime.Parse(inputValue.Text, CultureInfo.InvariantCulture), DateTimeKind.Unspecified)
            Case "String" : Return inputValue.Text
            Case "Boolean" : Return inputValue.Text = "1"
            Case Else : Throw New ArgumentException("نوع مدخل غير مدعوم.")
        End Select
    End Function

    Private Shared Function Row(cn As SqlConnection, tx As SqlTransaction, sql As String, id As Integer) As DataRow
        Using cmd As New SqlCommand(sql, cn, tx)
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id
            Dim table As New DataTable()
            Using reader = cmd.ExecuteReader()
                table.Load(reader)
            End Using
            If table.Rows.Count = 0 Then Return Nothing
            If table.Rows.Count <> 1 Then Throw New InvalidOperationException("مرجع التسوية غير فريد.")
            Return table.Rows(0)
        End Using
    End Function

    Private Shared Function Header(cn As SqlConnection, tx As SqlTransaction, id As Integer) As DataRow
        Dim result = Row(cn, tx, "SELECT T_ID,ST_Sett_ID,[Date],Total,Pure,isDepended,isVoid,BsType_ID FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@Id", id)
        If result Is Nothing OrElse CInt(result("BsType_ID")) <> 39 OrElse CInt(result("isVoid")) <> 0 Then Throw New InvalidOperationException("مستند التسوية غير موجود أو ملغى أو من نوع آخر.")
        Return result
    End Function

    Private Shared Function Line(cn As SqlConnection, tx As SqlTransaction, id As Integer) As DataRow
        Return Row(cn, tx, "SELECT T_ID,Bill_T_ID,IM_ID,ST_ID,ST_QTY,Cost,U_Cargo,Valid_D,Barcode,Total,[DATE] FROM dbo.ST_QTY_Deferences WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@Id", id)
    End Function

    Private Shared Sub Number(cmd As SqlCommand, name As String, value As Decimal)
        Dim p = cmd.Parameters.Add(name, SqlDbType.Decimal)
        p.Precision = 18 : p.Scale = 3 : p.Value = value
    End Sub

    ' Caller owns the exact transaction. Tests can roll back real fixture lines plus their audit together.
    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, operation As AuditOperationRequest) As AuditRequestResult
        If Not {"Settlement.LineAdded", "Settlement.LineDeleted", "Settlement.LineUpdated"}.Contains(operation.OperationCode) Then Throw New ArgumentException("عملية تسوية غير مدعومة.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, operation)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim request = lease.Request
            Dim documentId = Integer.Parse(request.EntityId, CultureInfo.InvariantCulture)
            AssertWritable(cn, tx, documentId, request.Context)
            Dim beforeHeader = Header(cn, tx, documentId)
            Dim beforeLine As DataRow = Nothing, afterLine As DataRow = Nothing
            Dim lineId As Integer
            Dim stockBefore As Double, stockAfter As Double, itemId As Integer, storeId As Integer, expiryText As String
            If request.OperationCode = "Settlement.LineAdded" Then
                itemId = CInt(Value(request, "ItemId")) : storeId = CInt(Value(request, "StoreId"))
                Dim expiryValue = Value(request, "ExpiryDate")
                expiryText = If(IsDBNull(expiryValue), "", CDate(expiryValue).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
                Dim cargo = DefaultUnit(cn, tx, itemId, CInt(Value(request, "UnitId")))
                stockBefore = Stock(cn, tx, itemId, storeId, expiryText)
                Dim target = CDec(Value(request, "RequestedQuantity"))
                If target < 0D AndAlso Not CBool(Value(request, "AllowNegative")) Then Throw New InvalidOperationException("الكمية السالبة غير مسموحة.")
                Dim difference = VerifiedDifference(target, CDbl(Value(request, "DisplayedQuantity")), stockBefore / CDbl(cargo))
                If difference <> CDec(Value(request, "QuantityDelta")) Then Throw New InvalidOperationException("فرق الشاشة لا يطابق الكمية المطلوبة والرصيد؛ حدّث البيانات.")
                If difference = 0D Then Return NoChange(cn, tx, lease)
                Using cmd As New SqlCommand("dbo.ST_Balance_Update", cn, tx)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("@Bill_T_ID", SqlDbType.Int).Value = documentId
                    cmd.Parameters.Add("@IM_ID", SqlDbType.Int).Value = Value(request, "ItemId")
                    cmd.Parameters.Add("@ST_ID", SqlDbType.Int).Value = Value(request, "StoreId")
                    cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, 50).Value = Value(request, "Barcode")
                    cmd.Parameters.Add("@D_Valid", SqlDbType.Date).Value = Value(request, "ExpiryDate")
                    Number(cmd, "@ST_QTY", CDec(Value(request, "RequestedQuantity")))
                    Number(cmd, "@Qty_Deference", Math.Abs(difference))
                    cmd.Parameters.Add("@Tag", SqlDbType.Char, 1).Value = If(difference < 0D, "-", "+")
                    Dim output = cmd.Parameters.Add("@Inserted_T_ID", SqlDbType.Int)
                    output.Direction = ParameterDirection.Output
                    cmd.ExecuteNonQuery()
                    If IsDBNull(output.Value) Then Throw New InvalidOperationException("إجراء الحفظ لم يرجع هوية السطر؛ لا يبحث عن آخر سطر.")
                    lineId = CInt(output.Value)
                End Using
                afterLine = Line(cn, tx, lineId)
                If afterLine Is Nothing OrElse CInt(afterLine("Bill_T_ID")) <> documentId Then Throw New InvalidOperationException("السطر المحفوظ لا يتبع المستند المطلوب.")
                stockAfter = Stock(cn, tx, itemId, storeId, expiryText)
                If Math.Abs(stockAfter / CDbl(cargo) - CDbl(target)) > 0.0000001R Then Throw New InvalidOperationException("الرصيد الناتج لا يطابق التسوية المطلوبة؛ تم رفض العملية.")
            Else
                lineId = CInt(Value(request, "LineId"))
                beforeLine = Line(cn, tx, lineId)
                If beforeLine Is Nothing Then
                    If request.OperationCode = "Settlement.LineUpdated" Then Throw New InvalidOperationException("حذف السطر منذ عرضه؛ أعد تحميل المستند.")
                    Return NoChange(cn, tx, lease)
                End If
                If CInt(beforeLine("Bill_T_ID")) <> documentId Then Throw New InvalidOperationException("السطر المختار يتبع مستند تسوية آخر.")
                itemId = CInt(beforeLine("IM_ID")) : storeId = CInt(beforeLine("ST_ID")) : expiryText = Convert.ToString(beforeLine("Valid_D"))
                stockBefore = Stock(cn, tx, itemId, storeId, expiryText)
                If request.OperationCode = "Settlement.LineUpdated" Then
                    If CStr(Value(request, "ExpectedLineSnapshot")) <> LineSnapshot(beforeLine) Then Throw New InvalidOperationException("تغير السطر منذ عرضه؛ أعد فتح تعديل السطر.")
                    Dim cargo = CDec(beforeLine("U_Cargo"))
                    If cargo <= 0D Then Throw New InvalidOperationException("معامل الوحدة المحفوظ غير صالح.")
                    Dim baseQuantity = CDec(Value(request, "QuantityDelta")) * cargo
                    ExactQuantity(baseQuantity)
                    Dim newQuantity = CDbl(baseQuantity)
                    Dim delta = newQuantity - CDbl(beforeLine("ST_QTY"))
                    If delta = 0R Then Return NoChange(cn, tx, lease)
                    If Not CBool(Value(request, "AllowNegative")) AndAlso stockBefore + delta < -0.0000001R Then Throw New InvalidOperationException("تعديل السطر ينتج كمية سالبة؛ لم يحفظ التعديل.")
                    Using cmd As New SqlCommand("UPDATE dbo.ST_QTY_Deferences SET ST_QTY=@Qty,Total=CONVERT(numeric(18,3),@Qty*Cost) WHERE T_ID=@Line AND Bill_T_ID=@Bill; EXEC dbo.Recalculate_Agent_Bill_Total @Bill;", cn, tx)
                        cmd.Parameters.Add("@Qty", SqlDbType.Float).Value = newQuantity
                        cmd.Parameters.Add("@Line", SqlDbType.Int).Value = lineId
                        cmd.Parameters.Add("@Bill", SqlDbType.Int).Value = documentId
                        cmd.ExecuteNonQuery()
                    End Using
                    afterLine = Line(cn, tx, lineId)
                    stockAfter = Stock(cn, tx, itemId, storeId, expiryText)
                    If Math.Abs(stockAfter - (stockBefore + delta)) > 0.0000001R Then Throw New InvalidOperationException("أثر تعديل السطر لا يطابق الرصيد الناتج؛ لم يحفظ التعديل.")
                Else
                    Using cmd As New SqlCommand("dbo.ST_QTY_Deferences_Details_Delete", cn, tx)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = lineId
                        cmd.Parameters.Add("@ExpectedBillId", SqlDbType.Int).Value = documentId
                        cmd.Parameters.Add("@AllowNegative", SqlDbType.Bit).Value = Value(request, "AllowNegative")
                        Dim deleted = cmd.Parameters.Add("@Deleted", SqlDbType.Bit)
                        deleted.Direction = ParameterDirection.Output
                        cmd.ExecuteNonQuery()
                        If IsDBNull(deleted.Value) OrElse Not CBool(deleted.Value) Then Throw New InvalidOperationException("لم يؤكد الإجراء حذف السطر المحدد.")
                    End Using
                    stockAfter = Stock(cn, tx, itemId, storeId, expiryText)
                End If
            End If
            Dim afterHeader = Header(cn, tx, documentId)
            Dim eventItem As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = 1, .EventCode = request.OperationCode,
                .Outcome = 1, .EntityType = "SettlementLine", .EntityId = lineId.ToString(CultureInfo.InvariantCulture),
                .DocumentNumber = If(IsDBNull(beforeHeader("ST_Sett_ID")), Nothing, Convert.ToString(beforeHeader("ST_Sett_ID"), CultureInfo.InvariantCulture)),
                .OccurredAtUtc = AuditRequestCoordinator.ServerUtcNow(cn, tx), .BusinessDate = CDate(beforeHeader("Date"))}
            Dim fields = {"ItemId", "StoreId", "StoredQuantity", "Cost", "UnitCargo", "ExpiryText", "Barcode", "Total", "CreatedAt"}
            Dim columns = {"IM_ID", "ST_ID", "ST_QTY", "Cost", "U_Cargo", "Valid_D", "Barcode", "Total", "DATE"}
            Dim sqlTypes = {"int", "int", "float(53)", "numeric(18,3)", "numeric(18,3)", "nvarchar(50)", "nvarchar(50)", "numeric(18,3)", "datetime"}
            For i = 0 To fields.Length - 1
                Change(eventItem, "SettlementLine", eventItem.EntityId, fields(i), sqlTypes(i),
                       Captured(request, fields(i), beforeLine, columns(i)), Captured(request, fields(i), afterLine, columns(i)), 1)
            Next
            Change(eventItem, "Settlement", request.EntityId, "DocumentTotal", "numeric(18,3)", AuditValue.Capture("Decimal", beforeHeader("Total")), AuditValue.Capture("Decimal", afterHeader("Total")), 1)
            Change(eventItem, "Settlement", request.EntityId, "DocumentPure", "numeric(18,3)", AuditValue.Capture("Decimal", beforeHeader("Pure")), AuditValue.Capture("Decimal", afterHeader("Pure")), 1)
            Change(eventItem, "SettlementStock", eventItem.EntityId, "StockQuantity", "float(53)", AuditValue.Capture("Float", stockBefore), AuditValue.Capture("Float", stockAfter), 1)
            If request.OperationCode = "Settlement.LineAdded" Then
                For Each name In {"UnitId", "QuantityDelta", "RequestedQuantity", "DisplayedQuantity"}
                    Change(eventItem, "SettlementInput", eventItem.EntityId, name, Nothing, AuditValue.Missing(AuditValueState.NotCaptured), request.Inputs.Single(Function(x) x.Name = name).Value, 2)
                Next
            End If
            Dim lineRow = If(afterLine, beforeLine)
            eventItem.RelatedEntities.Add(New AuditEntityReference With {.Role = "Document", .EntityType = "Settlement", .EntityId = request.EntityId})
            eventItem.RelatedEntities.Add(New AuditEntityReference With {.Role = "Item", .EntityType = "Item", .EntityId = Convert.ToString(lineRow("IM_ID"), CultureInfo.InvariantCulture)})
            eventItem.RelatedEntities.Add(New AuditEntityReference With {.Role = "Store", .EntityType = "Store", .EntityId = Convert.ToString(lineRow("ST_ID"), CultureInfo.InvariantCulture)})
            AuditWriter.WriteInTransaction(cn, tx, lease, eventItem)
            Dim resultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", request.EntityId}, {"LineId", eventItem.EntityId}})
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = resultXml}
        Catch
            lease.Faulted = True
            Throw ' Caller MUST roll back business changes and audit together.
        End Try
    End Function

    Private Shared Function NoChange(cn As SqlConnection, tx As SqlTransaction, lease As AuditRequestLease) As AuditRequestResult
        Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 5, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 5)}
    End Function

    Private Shared Function Captured(request As AuditOperationRequest, field As String, rowValue As DataRow, column As String) As AuditValue
        If rowValue Is Nothing Then Return AuditValue.Missing(AuditValueState.Absent)
        Return AuditValue.Capture(AuditContract.FieldType(request.OperationCode, field), rowValue(column))
    End Function

    Private Shared Sub Change(item As AuditEventRequest, entityType As String, entityId As String, field As String, sqlType As String,
                              before As AuditValue, after As AuditValue, origin As Byte)
        item.Changes.Add(New AuditFieldChange With {.ChangeNo = item.Changes.Count + 1, .EntityType = entityType, .EntityId = entityId,
            .FieldCode = field, .ValueType = AuditContract.FieldType(item.EventCode, field), .SourceSqlType = sqlType,
            .Before = before, .After = after, .ValueOrigin = origin})
    End Sub

    Friend Shared Function Save(connection As String, request As AuditOperationRequest) As AuditRequestResult
        Try
            Using cn As New SqlConnection(connection)
                cn.Open()
                Using tx = cn.BeginTransaction()
                    Try
                        Dim result = ExecuteInTransaction(cn, tx, request)
                        tx.Commit()
                        result.Status = "Completed"
                        Return result
                    Catch
                        Try
                            If tx.Connection IsNot Nothing Then tx.Rollback()
                        Catch
                            ' Keep the original failure, including an uncertain Commit result.
                        End Try
                        Throw
                    End Try
                End Using
            End Using
        Catch ex As Exception
            Try
                Dim recovered = AuditRequestCoordinator.Status(connection, request)
                If recovered.Status = "Completed" Then Return recovered
            Catch
                ' Unreachable status is NOT proof of failed business execution.
            End Try
            Throw New InvalidOperationException("لم تتأكد نتيجة التسوية. احتفظ بنفس الطلب عند إعادة المحاولة؛ مرجعه: " & request.RequestId.ToString("D") & Environment.NewLine & ex.Message, ex)
        End Try
    End Function
End Class
