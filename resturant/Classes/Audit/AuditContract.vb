Option Strict On
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Xml.Linq

' Closed catalogue. Adding a business path requires a reviewed contract, NOT arbitrary field names.
Friend NotInheritable Class AuditContract
    Friend Const MaxPayload As Integer = 1048576
    Friend Const MaxXmlChars As Integer = 262144

    Friend Shared Function Domain(operation As String) As String
        Select Case operation
            Case "Sales.LineDeleted", "Sales.LineAdded" : Return "Sales.LineLifecycle"
            Case "Sales.LineQuantityStepped" : Return "Sales.LineQuantity"
            Case "Sales.LineDetailsChanged", "Sales.LinePriceChanged" : Return "Sales.LineEditing"
            Case "Document.EditOpened" : Return "Documents.EditOpening"
            Case "Sales.EditOpened" : Return "Sales.EditOpening"
            Case "AuditTest.Changed" : Return "AuditTest"
            Case "Settlement.LineAdded", "Settlement.LineDeleted", "Settlement.LineUpdated", "Settlement.Created", "Settlement.Approved", "Settlement.Voided", "Settlement.EditOpened", "Settlement.HeaderSaved", "Settlement.EditFinished" : Return "Inventory.Settlement"
            Case "Security.SessionStarted", "Security.LoginRejected", "Security.SessionEnded" : Return "Security"
            Case Else : Throw New ArgumentException("رمز عملية المراقبة غير معتمد.")
        End Select
    End Function

    Friend Shared Function Kind(operation As String) As Byte
        Return If(Domain(operation) = "Security", CByte(2), CByte(1))
    End Function

    Friend Shared Sub Text(value As String, max As Integer, name As String, Optional required As Boolean = False, Optional code As Boolean = False)
        If value Is Nothing Then
            If required Then Throw New ArgumentException("قيمة مطلوبة: " & name)
            Return
        End If
        If value.Length > max OrElse (required AndAlso String.IsNullOrWhiteSpace(value)) Then Throw New ArgumentException("طول/قيمة غير صالحة: " & name)
        XmlConvert.VerifyXmlChars(value)
        If code AndAlso Not Regex.IsMatch(value, "\A[A-Za-z][A-Za-z0-9_.]*\z") Then Throw New ArgumentException("رمز غير صالح: " & name)
    End Sub

    Friend Shared Sub Context(context As AuditEventContext, operation As String)
        If context Is Nothing OrElse context.SourceId = Guid.Empty Then Throw New ArgumentException("هوية المصدر مطلوبة.")
        Text(context.MachineName, 128, "MachineName", True)
        Text(context.ApplicationVersion, 50, "ApplicationVersion", True)
        Text(context.ScreenCode, 64, "ScreenCode", True, True)
        Text(context.ActorName, 256, "ActorName")
        If context.SessionId.HasValue AndAlso context.SessionId.Value = Guid.Empty Then Throw New ArgumentException("جلسة فارغة.")
        Select Case context.ActorKind
            Case AuditActorKind.User
                If Not context.ActorUserId.HasValue OrElse context.ActorUserId.Value <= 0 OrElse context.ActorServiceCode IsNot Nothing Then Throw New ArgumentException("هوية مستخدم غير صالحة.")
                Text(context.ActorName, 256, "ActorName", True)
                If operation <> "Security.LoginRejected" AndAlso Not context.SessionId.HasValue Then Throw New ArgumentException("جلسة مؤكدة مطلوبة.")
            Case AuditActorKind.Unknown
                If operation <> "Security.LoginRejected" OrElse context.ActorUserId.HasValue OrElse context.ActorServiceCode IsNot Nothing OrElse context.SessionId.HasValue OrElse context.ActorName IsNot Nothing Then Throw New ArgumentException("لا تجوز هوية مجهولة أو مستعارة لهذا الحدث.")
            Case AuditActorKind.Service
                If operation <> "AuditTest.Changed" OrElse context.ActorServiceCode <> "AuditTests" OrElse context.ActorUserId.HasValue OrElse context.SessionId.HasValue Then Throw New ArgumentException("الخدمة غير معتمدة لهذا المسار.")
            Case Else : Throw New ArgumentException("نوع فاعل غير صالح.")
        End Select
        If operation = "Security.LoginRejected" AndAlso context.SessionId.HasValue Then Throw New ArgumentException("رفض الدخول لا يحمل جلسة سابقة.")
    End Sub

    Friend Shared Sub Entity(entityType As String, entityId As String, required As Boolean)
        Text(entityType, 64, "EntityType", required, True)
        Text(entityId, 128, "EntityId", required)
        If entityId IsNot Nothing AndAlso entityType Is Nothing Then Throw New ArgumentException("معرف دون نوع كيان.")
    End Sub

    Friend Shared Function FieldType(operation As String, field As String) As String
        If operation = "Sales.LineAdded" Then
            For Each definition In SalesAddInputs()
                Dim p = definition.Split("|"c)
                If p(0) = field Then Return p(1)
            Next
            For Each entityName In {"SalesLine", "SalesInvoice", "SalesStructure", "StockBucket"}
                For Each definition In SalesDeleteFields(entityName)
                    Dim p = definition.Split("|"c)
                    If p(0) = field Then Return p(2)
                Next
            Next
        ElseIf operation = "Sales.LineDeleted" Then
            For Each entityName As String In SalesDeleteEntities()
                For Each definition In SalesDeleteFields(entityName)
                    Dim p = definition.Split("|"c)
                    If p(0) = field Then Return p(2)
                Next
            Next
            Select Case field
                Case "LineId" : Return "Integer"
                Case "ExpectedGraphSnapshot" : Return "String"
                Case "OnUpdate" : Return "Boolean"
            End Select
        ElseIf operation = "Sales.LineQuantityStepped" Then
            For Each entityName As String In {"SalesLine", "SalesInvoice", "SalesStructure", "StockBucket"}
                For Each definition In SalesQuantityFields(entityName)
                    Dim parts = definition.Split("|"c)
                    If parts(0) = field Then Return parts(2)
                Next
            Next
            Select Case field
                Case "LineId", "QuantityDelta" : Return "Integer"
                Case "DisplayedQuantity" : Return "Decimal"
                Case "ExpectedLineSnapshot", "ExpectedHeaderSnapshot", "ExpectedStructureSnapshot", "ExpectedRecipeSnapshot" : Return "String"
                Case "OnUpdate", "AllowNegative" : Return "Boolean"
            End Select
        ElseIf IsSalesLine(operation) Then
            If operation = "Sales.LineDetailsChanged" Then
                For Each definition In SalesQuantityFields("SalesStructure").Concat(SalesQuantityFields("StockBucket"))
                    Dim parts = definition.Split("|"c)
                    If parts(0) = field Then Return parts(2)
                Next
            End If
            For Each definition In SalesLineFields().Concat(SalesHeaderFields())
                Dim parts = definition.Split("|"c)
                If parts(0) = field Then Return parts(2)
            Next
            Select Case field
                Case "LineId" : Return "Integer"
                Case "RequestedQuantity", "RequestedPrice" : Return "Decimal"
                Case "RequestedDiscount" : Return "Float"
                Case "ExpectedLineSnapshot", "ExpectedHeaderSnapshot", "ParentScreen" : Return "String"
                Case "OnUpdate", "AllowNegative", "EnforceMinimum" : Return "Boolean"
            End Select
        ElseIf operation = "AuditTest.Changed" Then
            Select Case field
                Case "Quantity", "Cost" : Return "Decimal"
                Case "Name" : Return "String"
                Case "RawQuantity" : Return "Float"
            End Select
        ElseIf operation = "Settlement.LineAdded" OrElse operation = "Settlement.LineDeleted" OrElse operation = "Settlement.LineUpdated" Then
            Select Case field
                Case "AllowNegative" : Return "Boolean"
                Case "ItemId", "StoreId", "UnitId", "LineId" : Return "Integer"
                Case "QuantityDelta", "Cost", "Total", "UnitCargo", "RequestedQuantity", "DocumentTotal", "DocumentPure" : Return "Decimal"
                Case "StoredQuantity", "DisplayedQuantity", "StockQuantity" : Return "Float"
                Case "ExpiryDate", "CreatedAt" : Return "DateTime"
                Case "ExpiryText", "Barcode", "ExpectedLineSnapshot" : Return "String"
            End Select
        ElseIf IsSettlementHeader(operation) OrElse operation = "Sales.EditOpened" OrElse operation = "Document.EditOpened" Then
            Select Case field
                Case "DocumentType"
                    If operation = "Document.EditOpened" Then Return "Integer"
                Case "DocumentNumber", "OwnerUserId", "AccountId", "Approved", "Voided", "EditOpenedBy", "VoidBy", "JournalId", "OriginalJournalId" : Return "Integer"
                Case "DocumentTotal", "DocumentPure", "Discount", "Balance" : Return "Decimal"
                Case "DocumentDate", "EditOpenedAt", "VoidAt" : Return "DateTime"
                Case "Title", "About", "VoidReason" : Return "String"
                Case "AllowNegative", "NeedRepost", "NeedCancelReverse", "SalesOpen", "Paused", "OrderDelivered" : Return "Boolean"
            End Select
        End If
        Throw New ArgumentException("الحقل غير مسموح لهذا الفعل: " & field)
    End Function

    Friend Shared Function IsSalesLine(operation As String) As Boolean
        Return operation = "Sales.LineDetailsChanged" OrElse operation = "Sales.LinePriceChanged"
    End Function

    ' Code | actual column | captured type | SQL source type. Shared by validation and capture.
    Friend Shared Function SalesLineFields() As String()
        Return {"ItemId|IM_ID|Integer|int", "UnitId|U_ID|Integer|bigint", "StoreId|ST_ID|Integer|bigint",
            "LineQuantity|QTY|Decimal|numeric(18,3)", "LinePrice|Price|Decimal|numeric(18,3)", "LineTotal|T_Price|Decimal|numeric(18,3)",
            "LineCost|Cost|Float|float(53)", "UnitCargo|U_Cargo|Float|float(53)", "StoredQuantity|ST_QTY|Decimal|numeric(18,2)",
            "LineDiscount|IM_Discount|Float|float(53)", "LineNotes|Notes|String|nvarchar(max)", "ExpiryText|D_Vaild|String|nvarchar(50)",
            "LineApproved|isDepended|Integer|int", "LineDate|Date|DateTime|datetime", "MinimumPriceUsed|is_By_Min_SP|Integer|int",
            "MinimumPrice2Used|is_By_Min_SP_2|Integer|int", "StockQuantity|AuditStock|Float|float(53)"}
    End Function

    Friend Shared Function SalesHeaderFields() As String()
        Return {"DocumentTotal|Total|Decimal|numeric(18,3)", "DocumentPure|Pure|Decimal|numeric(18,3)", "Discount|Discount|Decimal|numeric(18,3)",
            "DocumentProfit|Bill_Berfet|Float|float(53)", "Approved|isDepended|Integer|int", "Voided|isVoid|Integer|int",
            "NeedRepost|NeedRepost|Boolean|bit", "AccountId|AG_ID|Integer|int"}
    End Function

    Friend Shared Function IsSettlementHeader(operation As String) As Boolean
        Return {"Settlement.Created", "Settlement.Approved", "Settlement.Voided", "Settlement.EditOpened", "Settlement.HeaderSaved", "Settlement.EditFinished"}.Contains(operation, StringComparer.Ordinal)
    End Function

    Friend Shared Function SalesQuantityFields(entity As String) As String()
        Select Case entity
            Case "SalesLine" : Return SalesLineFields()
            Case "SalesInvoice" : Return SalesHeaderFields()
            Case "SalesStructure"
                Return {"StructureLineId|SB_Cont_T_ID|Integer|int", "StructureDate|DATE|DateTime|datetime", "StructureItemId|Struct_ID|Integer|int",
                    "StructureUnitId|U_ID|Integer|int", "StructureStoreId|ST_ID|Integer|int", "StructureQuantity|QTY|Decimal|numeric(18,3)",
                    "StructureCargo|U_Cargo|Decimal|numeric(18,2)", "StructureStoredQuantity|ST_QTY|Decimal|numeric(18,2)",
                    "StructureApproved|isDepended|Integer|int", "StructureCost|Cost|Decimal|numeric(18,3)"}
            Case "StockBucket"
                Return {"ItemId|IM_ID|Integer|int", "StoreId|ST_ID|Integer|bigint", "ExpiryText|D_Vaild|String|nvarchar(50)", "StockQuantity|AuditStock|Float|float(53)"}
            Case Else : Throw New ArgumentException("كيان تفاصيل الكمية غير معتمد.")
        End Select
    End Function

    Friend Shared Sub Value(value As AuditValue, valueType As String)
        If value Is Nothing Then Throw New ArgumentException("حالة القيمة مطلوبة.")
        If Not {"Integer", "Decimal", "Float", "Boolean", "String", "Date", "DateTime", "Guid"}.Contains(valueType, StringComparer.Ordinal) Then Throw New ArgumentException("نوع قيمة غير صالح.")
        Text(value.Text, 4096, "Value")
        If value.State = AuditValueState.Present AndAlso value.ValueType <> valueType Then Throw New ArgumentException("النوع الملتقط لا يطابق نوع الحقل.")
    End Sub

    Friend Shared Function SalesDeleteEntities() As String()
        Return {"SalesLine", "SalesInvoice", "SalesStructure", "SalesComponent", "SalesReservation", "SalesTimedReservation", "SalesCommission", "StockBucket"}
    End Function

    Friend Shared Function SalesAddInputs() As String()
        Return {"InsertPath|String", "ItemId|Integer", "StoreId|Integer", "UnitId|Integer", "ItemUnitId|Integer",
            "RequestedQuantity|Float", "RequestedPrice|Float", "ExpiryDate|Date", "OnUpdate|Boolean", "Barcode|String",
            "LineNotes|String", "CurrentQuantity|Float", "SerialCode|String", "MinimumPriceUsed|Integer", "MinimumPrice2Used|Integer",
            "NewRow|Boolean", "SalesType|Integer", "AllowNegative|Boolean", "ExpectedHeaderSnapshot|String"}
    End Function

    Friend Shared Function SalesDeleteFields(entityName As String) As String()
        Select Case entityName
            Case "SalesLine"
                Return SalesLineFields().Where(Function(x) Not x.StartsWith("StockQuantity|")).Concat({
                    "LineDocumentId|Bill_T_ID|Integer|int", "LineComponentsText|Compons|String|nvarchar(max)", "ParentLineId|Apart_From|Integer|int",
                    "SalespersonPercent|Saler_Percent|Decimal|numeric(18,3)", "MarketerValue|Markter_Val|Decimal|numeric(18,3)",
                    "LineBarcode|Barcode|String|nvarchar(max)", "LineSerial|Serial_Code|String|nvarchar(100)", "LineChecked|is_Check|Boolean|bit"}).ToArray()
            Case "SalesComponent"
                Return {"ComponentLineId|IM_T_ID|Integer|int", "ComponentId|Com_ID|Integer|int", "ComponentQuantity|QTY|Integer|int", "ComponentPrice|Price|Decimal|numeric(18,2)"}
            Case "SalesReservation"
                Return {"ReservationLineId|SB_T_ID|Integer|int", "ReservationDateTime|DATE_F|DateTime|datetime"}
            Case "SalesTimedReservation"
                Return {"ReservationLineId|SB_T_ID|Integer|int", "ReservationItemId|IM_ID|Integer|int", "ReservationDate|DATE_F|Date|date", "ReservationTime|TIME|String|time(0)", "ReservationNotes|NOTES|String|nvarchar(2500)"}
            Case "SalesCommission"
                Return {"CommissionAccountId|AG_ID|Integer|int", "CommissionValue|VALUE_Bercent|Decimal|numeric(18,2)"}
            Case Else : Return SalesQuantityFields(entityName)
        End Select
    End Function

    Friend Shared Sub Request(request As AuditOperationRequest)
        If request Is Nothing OrElse request.RequestId = Guid.Empty Then Throw New ArgumentException("هوية طلب ثابتة مطلوبة.")
        Domain(request.OperationCode)
        Context(request.Context, request.OperationCode)
        Entity(request.EntityType, request.EntityId, Kind(request.OperationCode) = 1)
        If request.Inputs.Count > 32 Then Throw New ArgumentException("عدد مدخلات الطلب يتجاوز الحد.")
        Dim names As New HashSet(Of String)(StringComparer.Ordinal)
        For Each input As AuditInput In request.Inputs
            If input Is Nothing OrElse Not names.Add(input.Name) Then Throw New ArgumentException("مدخل طلب مكرر/فارغ.")
            Dim expected As String
            If Kind(request.OperationCode) = 2 Then
                If input.Name <> "ReasonCode" Then Throw New ArgumentException("المدخل الأمني غير مسموح؛ لا تحفظ بيانات الاعتماد أو بصمتها.")
                expected = "String"
            Else
                expected = FieldType(request.OperationCode, input.Name)
            End If
            If input.ValueType <> expected Then Throw New ArgumentException("نوع مدخل غير متوافق مع العقد.")
            Value(input.Value, input.ValueType)
            If input.Name = "ReasonCode" Then Reason(request.OperationCode, input.Value.Text)
        Next
        If request.OperationCode = "Document.EditOpened" AndAlso (request.Inputs.Count <> 1 OrElse request.Inputs(0).Name <> "DocumentType" OrElse request.Inputs(0).Value.State <> AuditValueState.Present) Then Throw New ArgumentException("يلزم نوع المستند فقط لطلب فتح التعديل.")
        If IsSalesLine(request.OperationCode) Then
            Dim required = New List(Of String) From {"LineId", "ExpectedLineSnapshot", "ExpectedHeaderSnapshot", "ParentScreen", "OnUpdate", "RequestedPrice", "AllowNegative", "EnforceMinimum"}
            If request.OperationCode = "Sales.LineDetailsChanged" Then required.AddRange({"UnitId", "RequestedQuantity", "RequestedDiscount", "LineNotes"})
            If request.EntityType <> "SalesInvoice" OrElse names.Count <> required.Count OrElse required.Any(Function(x) Not names.Contains(x)) OrElse request.Inputs.Any(Function(x) x.Value.State <> AuditValueState.Present) Then Throw New ArgumentException("مدخلات تعديل سطر البيع غير مكتملة أو زائدة.")
            Dim parent = request.Inputs.Single(Function(x) x.Name = "ParentScreen").Value.Text
            If request.OperationCode = "Sales.LinePriceChanged" Then
                If request.Context.ScreenCode <> "SBChangePrice" OrElse parent <> "POS" Then Throw New ArgumentException("شاشة تغيير السعر غير معتمدة.")
            ElseIf request.Context.ScreenCode <> "Change_IM_Details" OrElse Not {"Sales", "Sales_Fast"}.Contains(parent) Then
                Throw New ArgumentException("شاشة تعديل التفاصيل غير معتمدة.")
            End If
        End If
        If request.OperationCode = "Sales.LineQuantityStepped" Then
            Dim required = {"LineId", "QuantityDelta", "DisplayedQuantity", "ExpectedLineSnapshot", "ExpectedHeaderSnapshot", "ExpectedStructureSnapshot", "ExpectedRecipeSnapshot", "OnUpdate", "AllowNegative"}
            If request.EntityType <> "SalesInvoice" OrElse Not {"Sales", "Sales_Fast", "POS"}.Contains(request.Context.ScreenCode) OrElse names.Count <> required.Length OrElse required.Any(Function(x) Not names.Contains(x)) OrElse request.Inputs.Any(Function(x) x.Value.State <> AuditValueState.Present) Then Throw New ArgumentException("طلب زيادة/نقصان الكمية غير مكتمل أو الشاشة غير معتمدة.")
            If Not {"1", "-1"}.Contains(request.Inputs.Single(Function(x) x.Name = "QuantityDelta").Value.Text) Then Throw New ArgumentException("خطوة الكمية يجب أن تكون +1 أو -1.")
        End If
        If request.OperationCode = "Sales.LineDeleted" Then
            Dim required = {"LineId", "ExpectedGraphSnapshot", "OnUpdate"}
            If request.EntityType <> "SalesInvoice" OrElse Not {"Sales", "Sales_Fast", "POS"}.Contains(request.Context.ScreenCode) OrElse names.Count <> required.Length OrElse required.Any(Function(x) Not names.Contains(x)) OrElse request.Inputs.Any(Function(x) x.Value.State <> AuditValueState.Present) Then Throw New ArgumentException("طلب حذف صنف البيع غير مكتمل أو الشاشة غير معتمدة.")
        End If
        If request.OperationCode = "Sales.LineAdded" Then
            Dim required = SalesAddInputs().Select(Function(x) x.Split("|"c)(0)).ToArray()
            Dim nullable = {"RequestedPrice", "ExpiryDate", "Barcode", "LineNotes", "CurrentQuantity"}
            If request.EntityType <> "SalesInvoice" OrElse Not {"POS", "Sales_Fast", "SB_IM_card"}.Contains(request.Context.ScreenCode) OrElse names.Count <> required.Length OrElse required.Any(Function(x) Not names.Contains(x)) Then Throw New ArgumentException("طلب إضافة صنف البيع أو شاشته غير مكتمل.")
            For Each input As AuditInput In request.Inputs
                If input.Value.State <> AuditValueState.Present AndAlso Not (nullable.Contains(input.Name) AndAlso input.Value.State = AuditValueState.SqlNull) Then Throw New ArgumentException("مدخل إضافة غير ملتقط.")
            Next
            Dim path = request.Inputs.Single(Function(x) x.Name = "InsertPath").Value.Text
            If path <> If(request.Context.ScreenCode = "POS", "POS", "Standard") Then Throw New ArgumentException("مسار الإضافة لا يطابق الشاشة.")
        End If
    End Sub

    Friend Shared Sub Reason(operation As String, reasonCode As String)
        If operation = "Security.LoginRejected" Then
            If Not {"InvalidCredential", "UserNotAllowed", "NoOpenPeriod", "SalesPermissionDenied"}.Contains(reasonCode, StringComparer.Ordinal) Then Throw New ArgumentException("سبب رفض الدخول غير معتمد.")
        ElseIf reasonCode IsNot Nothing Then
            Throw New ArgumentException("لا يقبل هذا القالب سببًا حرًا.")
        End If
    End Sub

    Friend Shared Sub EventData(request As AuditOperationRequest, item As AuditEventRequest)
        If item Is Nothing OrElse item.EventId = Guid.Empty OrElse item.EventOrdinal <= 0 OrElse item.EventOrdinal > 100 Then Throw New ArgumentException("هوية/ترتيب حدث غير صالح.")
        If item.EventCode <> request.OperationCode Then Throw New ArgumentException("الحدث لا يتبع عقد الطلب.")
        If item.CorrelationId.HasValue AndAlso item.CorrelationId.Value = Guid.Empty Then Throw New ArgumentException("هوية ارتباط فارغة.")
        If item.Outcome < 1 OrElse item.Outcome > 4 Then Throw New ArgumentException("نتيجة غير صالحة.")
        If item.EventCode = "Security.LoginRejected" Then
            If item.Outcome <> 2 Then Throw New ArgumentException("رفض الدخول ليس نجاحًا.")
        ElseIf item.Outcome <> 1 Then
            Throw New ArgumentException("القالب الحالي يسجل نجاح هذا الفعل فقط.")
        End If
        Reason(item.EventCode, item.ReasonCode)
        If item.EventCode = "Security.LoginRejected" Then
            Dim reasonInput = request.Inputs.SingleOrDefault(Function(x) x.Name = "ReasonCode")
            If reasonInput Is Nothing OrElse reasonInput.Value.State <> AuditValueState.Present OrElse reasonInput.Value.Text <> item.ReasonCode Then Throw New ArgumentException("سبب الحدث يجب أن يطابق الطلب.")
            If (item.ReasonCode = "InvalidCredential") <> (request.Context.ActorKind = AuditActorKind.Unknown) Then Throw New ArgumentException("هوية الرفض لا تطابق سببه.")
        End If
        Entity(item.EntityType, item.EntityId, Kind(item.EventCode) = 1)
        Text(item.DocumentNumber, 50, "DocumentNumber")
        If item.OccurredAtUtc.Kind <> DateTimeKind.Utc OrElse item.OccurredAtUtc.Ticks Mod TimeSpan.TicksPerMillisecond <> 0 Then Throw New ArgumentException("وقت الحدث يلزم UTC بدقة مللي ثانية من سياق الخادم.")
        If item.BusinessDate.HasValue AndAlso (item.BusinessDate.Value.Kind <> DateTimeKind.Unspecified OrElse item.BusinessDate.Value.Ticks Mod TimeSpan.TicksPerMillisecond <> 0) Then Throw New ArgumentException("تاريخ المستند يلزم Unspecified بدقة مللي ثانية دون تحويل.")
        If item.Changes.Count > 100 OrElse item.RelatedEntities.Count > 32 Then Throw New ArgumentException("تجاوز حجم التفاصيل؛ لا تُسقط الصفوف.")
        If Kind(item.EventCode) = 1 AndAlso item.Changes.Count = 0 Then Throw New ArgumentException("حدث العمل يتطلب تفاصيل فعلية.")
        Dim keys As New HashSet(Of String)(StringComparer.Ordinal)
        Dim numbers As New HashSet(Of Integer)()
        For Each change In item.Changes
            If change Is Nothing OrElse change.ChangeNo <= 0 OrElse Not numbers.Add(change.ChangeNo) Then Throw New ArgumentException("ترتيب تغيير مكرر/غير صالح.")
            Entity(change.EntityType, change.EntityId, True)
            If Not keys.Add(change.EntityType & ChrW(0) & change.EntityId & ChrW(0) & change.FieldCode) Then Throw New ArgumentException("حقل مكرر.")
            If change.ValueType <> FieldType(item.EventCode, change.FieldCode) Then Throw New ArgumentException("نوع حقل غير متوافق.")
            Text(change.SourceSqlType, 40, "SourceSqlType")
            Value(change.Before, change.ValueType)
            Value(change.After, change.ValueType)
            If item.EventCode = "Sales.EditOpened" AndAlso (change.EntityType <> "SalesInvoice" OrElse change.EntityId <> item.EntityId OrElse change.ValueOrigin <> 1 OrElse change.Before.State < AuditValueState.SqlNull OrElse change.After.State < AuditValueState.SqlNull) Then Throw New ArgumentException("فتح تعديل البيع يتطلب قيم الرأس الفعلية قبل وبعد.")
            If item.EventCode = "Document.EditOpened" AndAlso (change.EntityType <> request.EntityType OrElse item.EntityType <> request.EntityType OrElse change.EntityId <> request.EntityId OrElse item.EntityId <> request.EntityId OrElse change.ValueOrigin <> 1 OrElse change.Before.State < AuditValueState.SqlNull OrElse change.After.State < AuditValueState.SqlNull) Then Throw New ArgumentException("فتح تعديل المستند يتطلب قيم الرأس الفعلية قبل وبعد.")
            If IsSettlementHeader(item.EventCode) Then
                If change.EntityType <> "Settlement" OrElse change.EntityId <> item.EntityId OrElse change.ValueOrigin <> 1 OrElse change.After.State < AuditValueState.SqlNull Then Throw New ArgumentException("قيم رأس التسوية يجب التقاطها من الصف الفعلي.")
                If item.EventCode = "Settlement.Created" Then
                    If change.Before.State <> AuditValueState.Absent Then Throw New ArgumentException("الرأس الجديد لم يكن موجودًا قبل الإنشاء.")
                ElseIf change.Before.State < AuditValueState.SqlNull Then
                    Throw New ArgumentException("يلزم التقاط قيم الرأس السابقة.")
                End If
            End If
            If change.ValueOrigin < 1 OrElse change.ValueOrigin > 3 Then Throw New ArgumentException("مصدر قيمة غير صالح.")
            If change.EntityType = "SettlementLine" Then
                If item.EventCode = "Settlement.LineAdded" AndAlso (change.Before.State <> AuditValueState.Absent OrElse change.After.State = AuditValueState.Absent OrElse change.After.State = AuditValueState.NotCaptured) Then Throw New ArgumentException("إضافة السطر تتطلب قبل=غير موجود وقيمة لاحقة ملتقطة.")
                If item.EventCode = "Settlement.LineDeleted" AndAlso (change.After.State <> AuditValueState.Absent OrElse change.Before.State = AuditValueState.Absent OrElse change.Before.State = AuditValueState.NotCaptured) Then Throw New ArgumentException("حذف السطر يتطلب قيمة سابقة ملتقطة وبعد=غير موجود.")
                If item.EventCode = "Settlement.LineUpdated" AndAlso (change.Before.State < AuditValueState.SqlNull OrElse change.After.State < AuditValueState.SqlNull) Then Throw New ArgumentException("تعديل السطر يتطلب قيمًا فعلية قبل وبعد.")
            End If
        Next
        If item.EventCode = "Document.EditOpened" Then
            Dim expected = {"DocumentType", "DocumentNumber", "Approved", "Voided", "NeedRepost", "JournalId", "OriginalJournalId", "EditOpenedAt", "EditOpenedBy", "DocumentTotal", "DocumentPure", "Discount"}
            If item.Changes.Count <> expected.Length OrElse item.Changes.Any(Function(x) Not expected.Contains(x.FieldCode, StringComparer.Ordinal)) Then Throw New ArgumentException("يلزم التقاط حقول فتح التعديل الاثني عشر كاملة.")
            Dim kind = item.Changes.Single(Function(x) x.FieldCode = "DocumentType")
            If kind.Before.State <> AuditValueState.Present OrElse kind.After.State <> AuditValueState.Present OrElse kind.Before.Text <> request.Inputs(0).Value.Text OrElse kind.After.Text <> request.Inputs(0).Value.Text Then Throw New ArgumentException("نوع المستند الفعلي لا يطابق الطلب.")
        End If
        If IsSalesLine(item.EventCode) AndAlso (item.EventOrdinal = 1 OrElse item.EventCode = "Sales.LinePriceChanged") Then
            Dim lineId = request.Inputs.Single(Function(x) x.Name = "LineId").Value.Text
            If item.EventCode = "Sales.LinePriceChanged" AndAlso item.EventOrdinal <> 1 Then Throw New ArgumentException("تغيير السعر لا يملك أجزاء مكونات.")
            If item.EntityType <> "SalesLine" OrElse item.EntityId <> lineId OrElse item.Changes.Count <> 25 Then Throw New ArgumentException("يلزم سطر البيع وحقوله الفعلية الخمسة والعشرون.")
            For Each change In item.Changes
                Dim expected = If(change.EntityType = "SalesLine", SalesLineFields(), SalesHeaderFields()).Select(Function(x) x.Split("|"c)(0))
                If Not {"SalesLine", "SalesInvoice"}.Contains(change.EntityType) OrElse change.EntityId <> If(change.EntityType = "SalesLine", lineId, request.EntityId) OrElse Not expected.Contains(change.FieldCode) OrElse change.ValueOrigin <> 1 OrElse change.Before.State < AuditValueState.SqlNull OrElse change.After.State < AuditValueState.SqlNull Then Throw New ArgumentException("هوية/حقول سطر البيع أو الرأس غير متوافقة.")
            Next
            If item.RelatedEntities.Count <> 1 OrElse item.RelatedEntities(0).Role <> "Document" OrElse item.RelatedEntities(0).EntityType <> request.EntityType OrElse item.RelatedEntities(0).EntityId <> request.EntityId Then Throw New ArgumentException("مرجع فاتورة السطر مطلوب.")
        End If
        If item.EventCode = "Sales.LineQuantityStepped" OrElse (item.EventCode = "Sales.LineDetailsChanged" AndAlso item.EventOrdinal > 1) Then
            Dim lineId = request.Inputs.Single(Function(x) x.Name = "LineId").Value.Text
            If item.EntityType <> "SalesLine" OrElse item.EntityId <> lineId Then Throw New ArgumentException("هوية سطر خطوة الكمية مختلفة.")
            If item.RelatedEntities.Count <> 1 OrElse item.RelatedEntities(0).Role <> "Document" OrElse item.RelatedEntities(0).EntityType <> "SalesInvoice" OrElse item.RelatedEntities(0).EntityId <> request.EntityId Then Throw New ArgumentException("مرجع الفاتورة مطلوب.")
            If item.EventOrdinal = 1 AndAlso (item.Changes.Count <> 25 OrElse item.Changes.Where(Function(x) x.EntityType = "SalesLine").Count() <> 17 OrElse item.Changes.Where(Function(x) x.EntityType = "SalesInvoice").Count() <> 8) Then Throw New ArgumentException("الحدث الأول يتطلب 25 قيمة للسطر والرأس.")
            For Each group In item.Changes.GroupBy(Function(x) x.EntityType & ChrW(0) & x.EntityId)
                Dim first = group.First()
                Dim definitions = SalesQuantityFields(first.EntityType)
                If group.Count() <> definitions.Length Then Throw New ArgumentException("يجب التقاط جميع حقول الصف دون إسقاط.")
                If (item.EventOrdinal = 1) <> {"SalesLine", "SalesInvoice"}.Contains(first.EntityType) Then Throw New ArgumentException("جزء تفاصيل الكمية غير صحيح.")
                If first.EntityType = "SalesLine" AndAlso first.EntityId <> lineId Then Throw New ArgumentException("سطر مختلف.")
                If first.EntityType = "SalesInvoice" AndAlso first.EntityId <> request.EntityId Then Throw New ArgumentException("رأس مختلف.")
                For Each change In group
                    If Not definitions.Any(Function(x) x.Split("|"c)(0) = change.FieldCode) OrElse change.ValueOrigin <> 1 Then Throw New ArgumentException("حقل خطوة الكمية غير معتمد.")
                    If first.EntityType = "SalesStructure" Then
                        If change.Before.State = AuditValueState.NotCaptured OrElse change.After.State = AuditValueState.NotCaptured OrElse (change.Before.State = AuditValueState.Absent AndAlso change.After.State = AuditValueState.Absent) Then Throw New ArgumentException("صف المكون يجب أن يكون ملتقطًا أو غير موجود فعلًا.")
                    ElseIf change.Before.State < AuditValueState.SqlNull OrElse change.After.State < AuditValueState.SqlNull Then
                        Throw New ArgumentException("يجب التقاط القيم قبل وبعد.")
                    End If
                Next
            Next
        End If
        If item.EventCode = "Sales.LineAdded" Then
            If item.EntityType <> "SalesLine" OrElse item.RelatedEntities.Count <> 1 OrElse item.RelatedEntities(0).Role <> "Document" OrElse item.RelatedEntities(0).EntityType <> "SalesInvoice" OrElse item.RelatedEntities(0).EntityId <> request.EntityId Then Throw New ArgumentException("مرجع إضافة الصنف غير صحيح.")
            If item.EventOrdinal = 1 AndAlso (item.Changes.Count <> 32 OrElse item.Changes.Where(Function(x) x.EntityType = "SalesLine").Count() <> 24 OrElse item.Changes.Where(Function(x) x.EntityType = "SalesInvoice").Count() <> 8) Then Throw New ArgumentException("إضافة الصنف تتطلب السطر الكامل والرأس.")
            For Each group In item.Changes.GroupBy(Function(x) x.EntityType & ChrW(0) & x.EntityId)
                Dim first = group.First()
                If Not {"SalesLine", "SalesInvoice", "SalesStructure", "StockBucket"}.Contains(first.EntityType) Then Throw New ArgumentException("كيان إضافة غير معتمد.")
                Dim definitions = SalesDeleteFields(first.EntityType)
                If group.Count() <> definitions.Length OrElse (item.EventOrdinal = 1) <> {"SalesLine", "SalesInvoice"}.Contains(first.EntityType) Then Throw New ArgumentException("صف إضافة غير مكتمل أو في جزء غير صحيح.")
                If first.EntityType = "SalesLine" AndAlso first.EntityId <> item.EntityId Then Throw New ArgumentException("سطر مختلف.")
                If first.EntityType = "SalesInvoice" AndAlso first.EntityId <> request.EntityId Then Throw New ArgumentException("رأس مختلف.")
                If first.EntityType = "SalesLine" AndAlso group.Any(Function(x) (x.Before.State = AuditValueState.Absent) <> (first.Before.State = AuditValueState.Absent)) Then Throw New ArgumentException("حالة السطر قبل الإضافة غير متناسقة.")
                For Each change In group
                    If Not definitions.Any(Function(x) x.Split("|"c)(0) = change.FieldCode AndAlso x.Split("|"c)(3) = change.SourceSqlType) OrElse change.ValueOrigin <> 1 OrElse change.Before.State = AuditValueState.NotCaptured OrElse change.After.State = AuditValueState.NotCaptured Then Throw New ArgumentException("قيم الإضافة الفعلية غير مكتملة.")
                    If first.EntityType <> "SalesStructure" AndAlso change.After.State < AuditValueState.SqlNull Then Throw New ArgumentException("القيمة اللاحقة مطلوبة.")
                    If {"SalesInvoice", "StockBucket"}.Contains(first.EntityType) AndAlso change.Before.State < AuditValueState.SqlNull Then Throw New ArgumentException("القيمة السابقة مطلوبة.")
                    If change.Before.State = AuditValueState.Absent AndAlso change.After.State = AuditValueState.Absent Then Throw New ArgumentException("صف غير موجود قبل وبعد.")
                Next
            Next
        End If
        If item.EventCode = "Sales.LineDeleted" Then
            Dim lineId = request.Inputs.Single(Function(x) x.Name = "LineId").Value.Text
            If item.EntityType <> "SalesLine" OrElse item.EntityId <> lineId Then Throw New ArgumentException("هوية السطر المحذوف مختلفة.")
            If item.RelatedEntities.Count <> 1 OrElse item.RelatedEntities(0).Role <> "Document" OrElse item.RelatedEntities(0).EntityType <> "SalesInvoice" OrElse item.RelatedEntities(0).EntityId <> request.EntityId Then Throw New ArgumentException("مرجع الفاتورة مطلوب.")
            If item.EventOrdinal = 1 AndAlso (item.Changes.Count <> 32 OrElse item.Changes.Where(Function(x) x.EntityType = "SalesLine").Count() <> 24 OrElse item.Changes.Where(Function(x) x.EntityType = "SalesInvoice").Count() <> 8) Then Throw New ArgumentException("حدث الحذف الرئيسي يتطلب السطر الكامل والرأس.")
            For Each group In item.Changes.GroupBy(Function(x) x.EntityType & ChrW(0) & x.EntityId)
                Dim first = group.First(), definitions = SalesDeleteFields(first.EntityType)
                If group.Count() <> definitions.Length OrElse (item.EventOrdinal = 1) <> {"SalesLine", "SalesInvoice"}.Contains(first.EntityType) Then Throw New ArgumentException("تفاصيل الحذف يجب أن تكون صفوفًا كاملة في الجزء الصحيح.")
                If first.EntityType = "SalesLine" AndAlso first.EntityId <> lineId Then Throw New ArgumentException("سطر مختلف.")
                If first.EntityType = "SalesInvoice" AndAlso first.EntityId <> request.EntityId Then Throw New ArgumentException("رأس مختلف.")
                For Each change In group
                    If Not definitions.Any(Function(x) x.Split("|"c)(0) = change.FieldCode AndAlso x.Split("|"c)(3) = change.SourceSqlType) OrElse change.ValueOrigin <> 1 OrElse change.Before.State < AuditValueState.SqlNull OrElse change.After.State = AuditValueState.NotCaptured Then Throw New ArgumentException("قيم الحذف الفعلية غير مكتملة.")
                    If first.EntityType = "SalesLine" AndAlso change.After.State <> AuditValueState.Absent Then Throw New ArgumentException("السطر يجب أن يكون غير موجود بعد الحذف.")
                    If {"SalesInvoice", "StockBucket"}.Contains(first.EntityType) AndAlso change.After.State < AuditValueState.SqlNull Then Throw New ArgumentException("الرأس والرصيد يجب التقاطهما بعد الحذف.")
                Next
            Next
        End If
        For Each related In item.RelatedEntities
            Text(related.Role, 64, "Role", True, True)
            If Not {"Document", "Line", "Item", "Store", "Unit"}.Contains(related.Role, StringComparer.Ordinal) Then Throw New ArgumentException("علاقة غير معتمدة.")
            Entity(related.EntityType, related.EntityId, True)
        Next
    End Sub
End Class
