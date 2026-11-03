Option Strict On
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Xml.Linq

' Closed catalogue. Adding a business path requires a reviewed contract, NOT arbitrary field names.
Partial Friend NotInheritable Class AuditContract
    Friend Const MaxPayload As Integer = 1048576
    Friend Const MaxXmlChars As Integer = 262144

    Friend Shared Function Domain(operation As String) As String
        If IsReceipt(operation) Then Return "Finance.Receipts"
        If IsTableSplit(operation) Then Return "Sales.TableSplit"
        If operation = "Salary.BatchPosted" Then Return "People.Salaries"
        If IsItemLifecycle(operation) Then Return "Catalog.ItemLifecycle"
        If IsWorkflow(operation) Then Return "Documents.Workflows"
        If IsInventoryOperation(operation) Then Return "Inventory.Operations"
        If IsPeopleCard(operation) Then Return "People.Cards"
        Select Case operation
            Case "Quote.LineAdded", "Quote.LineDeleted", "Quote.LineChanged", "Quote.LineQuantityStepped" : Return "Sales.QuoteLines"
            Case "Document.AccountChanged", "Document.DiscountChanged", "Document.FastDiscountChanged" : Return "Documents.DirectFields"
            Case "Purchase.LineAdded" : Return "Inventory.PurchaseAddition"
            Case "AccountEntry.Created" : Return "Finance.AccountEntries"
            Case "Expense.LineAdded", "Expense.LineDeleted" : Return "Expenses.Lines"
            Case "Return.LineAdded", "Return.LineDeleted", "Return.LineQuantityChanged" : Return "Sales.ReturnLines"
            Case "EmployeeValue.Created", "EmployeeValue.Updated", "EmployeeValue.Voided" : Return "People.EmployeeValues"
            Case "StockDocument.LineAdded", "StockDocument.LineDeleted", "StockDocument.LineQuantityChanged" : Return "Inventory.DocumentLines"
            Case "TreasuryMovement.Created", "TreasuryMovement.Voided", "TreasuryTransfer.Created", "TreasuryTransfer.Voided", "AccountTransfer.Created", "AccountTransfer.Voided" : Return "Finance.Transfers"
            Case "Table.Transferred", "Table.LineQuantityChanged", "Table.LineDeleted" : Return "Sales.Tables"
            Case "Item.FastUpdated", "Item.Deleted" : Return "Catalog.Items"
            Case "Offer.Created", "Offer.ItemOffersDeleted", "PriceRule.Created", "PriceRule.Deleted" : Return "Catalog.PricingRules"
            Case "ItemUnit.Created", "ItemUnit.Updated", "ItemUnit.Deleted", "ItemUnit.PriceChanged", "ItemUnit.OfferPriceChanged" : Return "Catalog.ItemUnits"
            Case "Group.Created", "Group.Updated", "Group.Deleted", "Group.PrinterLinked", "Group.PrinterUnlinked" : Return "Catalog.Groups"
            Case "Unit.Created", "Unit.Updated", "Unit.Deleted" : Return "Catalog.Units"
            Case "Document.Voided" : Return "Documents.Cancellation"
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
        If IsReceipt(operation) Then Return ReceiptFieldType(operation, field)
        If IsTableSplit(operation) Then Return TableSplitFieldType(operation, field)
        If operation = "Salary.BatchPosted" Then Return SalaryFieldType(field)
        If IsItemLifecycle(operation) Then Return ItemLifecycleFieldType(operation, field)
        If IsWorkflow(operation) Then Return WorkflowFieldType(operation, field)
        If IsInventoryOperation(operation) Then Return InventoryFieldType(operation, field)
        If IsPeopleCard(operation) Then Return PeopleFieldType(operation, field)
        If IsQuoteLine(operation) Then
            For Each definition In QuoteInputs(operation).Concat(QuoteFields("QuoteDocument")).Concat(QuoteFields("QuoteLine"))
                Dim p = definition.Split("|"c)
                If p(0) = field Then Return p(2)
            Next
            Throw New ArgumentException("حقل عرض سعر غير معتمد.")
        End If
        If IsDocumentField(operation) Then
            If operation = "Document.FastDiscountChanged" AndAlso field = "DiscountPercent" Then Return "Decimal"
            If field = "ExpectedDocumentFields" Then Return "String"
            If field = "OnUpdate" Then Return "Boolean"
            For Each definition In DocumentHeaderFields()
                Dim p = definition.Split("|"c)
                If p(0) = field Then Return p(2)
            Next
            Throw New ArgumentException("حقل رأس فاتورة غير معتمد.")
        End If
        If operation = "Purchase.LineAdded" Then
            If {"ExpectedPurchaseSnapshot", "AdditionalExpiries"}.Contains(field) Then Return "String"
            If field = "DocumentType" Then Return "Integer"
            For Each definition In PurchaseInputs().Concat(PurchaseEntities().SelectMany(Function(x) PurchaseFields(x)))
                Dim p = definition.Split("|"c)
                If p(0) = field Then Return p(2)
            Next
            Throw New ArgumentException("حقل إضافة مشتريات/جرد غير معتمد.")
        End If
        If operation = "AccountEntry.Created" Then
            For Each definition In AccountEntryFields()
                Dim p = definition.Split("|"c)
                If p(0) = field Then Return p(2)
            Next
            Throw New ArgumentException("حقل حركة حساب غير معتمد.")
        End If
        If IsExpenseLine(operation) Then
            If field = "ExpectedExpenseSnapshot" Then Return "String"
            If {"TargetLineId", "OnUpdate"}.Contains(field) Then Return "Integer"
            For Each entityName In {"ExpenseDocument", "ExpenseLine"}
                For Each definition In ExpenseFields(entityName)
                    Dim p = definition.Split("|"c)
                    If p(0) = field Then Return p(2)
                Next
            Next
            Throw New ArgumentException("حقل بند مصروف غير معتمد.")
        End If
        If IsReturnLine(operation) Then
            If field = "AllowNegative" Then Return "Boolean"
            If {"ExpectedReturnSnapshot", "RequestedBarcode", "RequestedExpiry"}.Contains(field) Then Return "String"
            If {"RequestedQuantity", "RequestedCost", "RequestedPrice", "DisplayedQuantity"}.Contains(field) Then Return "Float"
            If {"DocumentType", "TargetLineId", "SelectedItemId", "SelectedUnitId", "SelectedStoreId", "OnUpdate", "QuantityDelta"}.Contains(field) Then Return "Integer"
            For Each entityName In {"ReturnDocument", "ReturnLine", "StockBucket"}
                For Each definition In ReturnFields(entityName)
                    Dim p = definition.Split("|"c)
                    If p(0) = field Then Return p(2)
                Next
            Next
            Throw New ArgumentException("حقل مرتجع غير معتمد.")
        End If
        If IsEmployeeValue(operation) Then
            If field = "ExpectedEmployeeValueSnapshot" Then Return "String"
            For Each definition In EmployeeValueFields()
                Dim p = definition.Split("|"c)
                If p(0) = field Then Return p(2)
            Next
            Throw New ArgumentException("حقل قيمة موظف غير معتمد.")
        End If
        If IsStockDocumentLine(operation) Then
            If {"ExpectedStockDocumentSnapshot", "RequestedBarcode", "RequestedExpiry"}.Contains(field) Then Return "String"
            If {"RequestedQuantity", "RequestedCost", "RequestedTotal", "DisplayedQuantity"}.Contains(field) Then Return "Decimal"
            If field = "RequestedSalePrice" Then Return "Float"
            If {"DocumentType", "TargetLineId", "SelectedItemId", "SelectedUnitId", "SelectedStoreId", "DestinationStoreId", "OnUpdate", "QuantityDelta"}.Contains(field) Then Return "Integer"
            For Each entityName In {"StockDocument", "StockReceiptLine", "StockIssueLine", "StockDamageLine", "StockTransferLine", "StockBucket"}
                For Each definition In StockDocumentFields(entityName)
                    Dim p = definition.Split("|"c)
                    If p(0) = field Then Return p(2)
                Next
            Next
            Throw New ArgumentException("حقل إذن مخزني غير معتمد.")
        End If
        If IsFinance(operation) Then
            If {"ExpectedFinanceSnapshot", "Description", "Title"}.Contains(field) Then Return "String"
            If field = "RequestedDate" Then Return "DateTime"
            If field = "RequestedAmount" Then Return "Decimal"
            If {"FromId", "ToId", "TreasuryId", "MovementType", "OpeningFlag"}.Contains(field) Then Return "Integer"
            For Each definition In FinanceFields(If(operation.StartsWith("AccountTransfer.", StringComparison.Ordinal), "FinanceAccountRow", "FinanceTreasuryRow"))
                Dim p = definition.Split("|"c)
                If p(0) = field Then Return p(2)
            Next
            Throw New ArgumentException("حقل مالي غير معتمد.")
        End If
        If IsTableOperation(operation) Then
            If field = "ExpectedTableSnapshot" Then Return "String"
            If field = "DisplayedQuantity" Then Return "Decimal"
            If {"DestinationTableId", "TargetLineId", "QuantityDelta"}.Contains(field) Then Return "Integer"
            For Each tableEntity As String In TableEntities(operation)
                For Each definition In TableFields(tableEntity)
                    Dim p = definition.Split("|"c)
                    If p(0) = field Then Return p(2)
                Next
            Next
            Throw New ArgumentException("حقل طاولة غير معتمد.")
        End If
        If IsItemMaster(operation) Then
            If field = "ExpectedItemSnapshot" Then Return "String"
            If field = "DefaultUnitId" Then Return "Integer"
            If field = "RequestedUnitPrice" Then Return "Decimal"
            For Each entityName In ItemMasterEntities()
                For Each definition In ItemMasterFields(entityName)
                    Dim p = definition.Split("|"c)
                    If p(0) = field Then Return p(2)
                Next
            Next
        ElseIf IsPricingRule(operation) Then
            If field = "RequestedPercent" Then Return "Float"
            If field = "ExpectedPricingSnapshot" Then Return "String"
            If field = "TargetPriceRuleId" Then Return "Integer"
            If {"RequestedStartClock", "RequestedEndClock"}.Contains(field) Then Return "DateTime"
            For Each entityName In {"ItemOffers", "PriceRuleGroup", "OfferSnapshotRow", "PriceRule"}
                For Each definition In PricingFields(entityName)
                    Dim p = definition.Split("|"c)
                    If p(0) = field Then Return p(2)
                Next
            Next
        ElseIf IsItemUnit(operation) Then
            If field = "ExpectedItemUnitSnapshot" Then Return "String"
            If field = "TargetItemUnitId" Then Return "Integer"
            If {"CheckBarcode", "MakeDefault"}.Contains(field) Then Return "Boolean"
            For Each entityName In {"CatalogItem", "ItemUnit", "ItemUnitRecipe"}
                For Each definition In ItemUnitFields(entityName)
                    Dim p = definition.Split("|"c)
                    If p(0) = field Then Return p(2)
                Next
            Next
        ElseIf IsGroup(operation) Then
            If field = "ExpectedGroupSnapshot" Then Return "String"
            If {"RequestedRank", "PrinterLinkId"}.Contains(field) Then Return "Integer"
            For Each entityName In {"ProductGroup", "GroupPrinter"}
                For Each definition In GroupFields(entityName)
                    Dim p = definition.Split("|"c)
                    If p(0) = field Then Return p(2)
                Next
            Next
        ElseIf IsUnit(operation) Then
            If field = "ExpectedUnitSnapshot" Then Return "String"
            For Each definition In UnitFields()
                Dim p = definition.Split("|"c)
                If p(0) = field Then Return p(2)
            Next
        ElseIf operation = "Document.Voided" Then
            If field = "ExpectedGraphSnapshot" Then Return "String"
            If field = "DocumentType" Then Return "Integer"
            For Each entityName In VoidEntities()
                For Each definition In VoidFields(entityName)
                    Dim p = definition.Split("|"c)
                    If p(0) = field Then Return p(2)
                Next
            Next
        ElseIf operation = "Sales.LineAdded" Then
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

    Friend Shared Function IsEmployeeValue(operation As String) As Boolean
        Return {"EmployeeValue.Created", "EmployeeValue.Updated", "EmployeeValue.Voided"}.Contains(operation, StringComparer.Ordinal)
    End Function

    Friend Shared Function EmployeeValueFields() As String()
        Return {"EVCreatedAt|Date|DateTime|datetime", "EVBusinessDate|DateOF_Order|DateTime|datetime", "EVYear|YEAR|Integer|int", "EVMonth|MONTH|Integer|int",
            "EVEmployeeId|AG_ID|Integer|int", "EVType|ValType|Integer|int", "EVAmount|Value|Decimal|numeric(18,3)", "EVNotes|About|String|nvarchar(max)",
            "EVVoided|isVoid|Integer|int", "EVOriginalUserId|User_ID|Integer|bigint"}
    End Function

    Friend Shared Function EmployeeValueInputs(operation As String) As String()
        If Not IsEmployeeValue(operation) Then Throw New ArgumentException("عملية قيمة موظف غير معتمدة.")
        Dim names As New List(Of String)
        If operation <> "EmployeeValue.Created" Then names.Add("ExpectedEmployeeValueSnapshot")
        If operation <> "EmployeeValue.Voided" Then names.AddRange({"EVBusinessDate", "EVYear", "EVMonth", "EVEmployeeId", "EVType", "EVAmount", "EVNotes"})
        Return names.ToArray()
    End Function



    Friend Shared Function IsDocumentField(operation As String) As Boolean
        Return {"Document.AccountChanged", "Document.DiscountChanged", "Document.FastDiscountChanged"}.Contains(operation, StringComparer.Ordinal)
    End Function

    Friend Shared Function IsQuoteLine(operation As String) As Boolean
        Return {"Quote.LineAdded", "Quote.LineDeleted", "Quote.LineChanged", "Quote.LineQuantityStepped"}.Contains(operation)
    End Function

    Friend Shared Function QuoteInputs(operation As String) As String()
        Dim common = {"DocumentType||Integer|R", "OnUpdate||Boolean|R", "TargetLineId||Integer|R", "ExpectedQuoteSnapshot||String|R"}
        Select Case operation
            Case "Quote.LineAdded"
                Return common.Concat({"SelectedItemId|IM_ID|Integer|R", "SelectedUnitId|U_ID|Integer|R", "SelectedStoreId|ST_ID|Integer|R", "RequestedQuantity|QYT|Float|R", "RequestedPrice|Price|Float|R", "RequestedExpiry|D_Vaild|Date|O", "CurrentQuantity|Current_QTY|Float|O", "RequestedBarcode|Barcode|String|R"}).ToArray()
            Case "Quote.LineChanged"
                Return common.Concat({"SelectedUnitId|U_ID|Integer|R", "RequestedQuantity|QTY|Decimal|R", "RequestedPrice|Price|Float|R"}).ToArray()
            Case "Quote.LineQuantityStepped"
                Return common.Concat({"QuantityDelta|Def|Integer|R", "DisplayedQuantity||Float|R"}).ToArray()
            Case "Quote.LineDeleted" : Return common
            Case Else : Throw New ArgumentException("عملية صنف عرض سعر غير معتمدة.")
        End Select
    End Function

    Friend Shared Function QuoteScreen(operation As String) As String
        Select Case operation
            Case "Quote.LineAdded" : Return "ViewBill_IM_card"
            Case "Quote.LineChanged" : Return "Change_IM_Details"
            Case "Quote.LineDeleted", "Quote.LineQuantityStepped" : Return "ViewBill"
            Case Else : Throw New ArgumentException("عملية عرض غير معتمدة.")
        End Select
    End Function

    Friend Shared Function QuoteFields(entity As String) As String()
        If entity = "QuoteDocument" Then Return DocumentHeaderFields()
        If entity <> "QuoteLine" Then Throw New ArgumentException("كيان عرض سعر غير معتمد.")
        Return {"QuoteOwnerId|Bill_T_ID|Integer|int", "QuoteItemId|IM_ID|Integer|int", "QuoteUnitId|U_ID|Integer|bigint", "QuoteStoreId|ST_ID|Integer|bigint", "QuoteLineDate|Date|DateTime|datetime", "QuoteComponents|Compons|String|nvarchar(max)", "QuoteCost|Cost|Decimal|numeric(18,3)", "QuotePrice|Price|Decimal|numeric(18,3)", "QuoteExpiry|D_Vaild|String|nvarchar(50)", "QuoteQuantity|QTY|Float|float(53)", "QuoteLineTotal|T_Price|Decimal|numeric(18,3)", "QuoteCargo|U_Cargo|Float|float(53)", "QuoteStockQuantity|ST_QTY|Decimal|numeric(18,2)", "QuoteApproved|isDepended|Integer|int", "QuoteParentLine|Apart_From|Integer|int", "QuoteSalespersonPercent|Saler_Percent|Decimal|numeric(18,2)", "QuoteNotes|Notes|String|nvarchar(max)", "QuoteBarcode|Barcode|String|nvarchar(50)", "QuoteItemName|AuditItemName|String|nvarchar(500)"}
    End Function

    Friend Shared Function DocumentFieldSpec(kind As Integer) As String()
        Select Case kind
            Case 1 : Return {"SB_ID", "Sales", "SB_Update"}
            Case 7 : Return {"Pch_ID", "Pch", "Pch_Update"}
            Case 16 : Return {"ViewSB_ID", "Sales", "SB_Update"}
            Case Else : Throw New ArgumentException("نوع فاتورة غير معتمد لتغيير الحقول المباشر.")
        End Select
    End Function

    Friend Shared Function DocumentFieldScreens(operation As String, kind As Integer) As String()
        DocumentFieldSpec(kind)
        If operation = "Document.FastDiscountChanged" Then Return If(kind = 1, New String() {"Fast_SB_Discount"}, New String() {})
        If operation = "Document.AccountChanged" Then
            If kind = 1 Then Return {"Sales", "AgentsMenu"}
            If kind = 16 Then Return {"ViewBill"}
            Return New String() {}
        End If
        If operation = "Document.DiscountChanged" Then
            If kind = 1 Then Return {"Sales", "Percent_Disc", "SBChangePrice"}
            If kind = 7 Then Return {"Pch", "Percent_Disc"}
            Return New String() {}
        End If
        Throw New ArgumentException("عملية حقول فاتورة غير معتمدة.")
    End Function

    Friend Shared Function DocumentFieldInputs(operation As String) As String()
        If Not IsDocumentField(operation) Then Throw New ArgumentException("عملية رأس فاتورة غير معتمدة.")
        If operation = "Document.FastDiscountChanged" Then Return {"DocumentType", "OnUpdate", "ExpectedDocumentFields", "Discount", "DiscountPercent"}
        Return {"DocumentType", "OnUpdate", "ExpectedDocumentFields", If(operation = "Document.AccountChanged", "AccountId", "Discount")}
    End Function

    Friend Shared Function DocumentFieldAllowed(r As AuditOperationRequest, field As String) As Boolean
        If r.OperationCode = "Document.AccountChanged" Then Return field = "AccountId" OrElse field = "AccountName"
        If field = "Discount" Then Return True
        ' Legacy fast discount leaves Debit_2 untouched when the requested percentage is zero.
        Return r.OperationCode = "Document.FastDiscountChanged" AndAlso field = "SecondaryDebit" AndAlso Decimal.Parse(r.Inputs.Single(Function(x) x.Name = "DiscountPercent").Value.Text, CultureInfo.InvariantCulture) <> 0D
    End Function

    Friend Shared Function DocumentHeaderFields() As String()
        Return VoidFields("BusinessDocument").Concat({"AccountName|AuditAccountName|String|nvarchar(500)",
            "SecondaryDebit|Debit_2|Decimal|numeric(18,3)", "EditOpenedAt|EditOpenedAt|DateTime|datetime",
            "EditOpenedBy|EditOpenedBy|Integer|int"}).ToArray()
    End Function

    Friend Shared Function PurchaseInputs() As String()
        Return {"SelectedItemId|IM_ID|Integer|int|R",
            "SelectedUnitId|U_ID|Integer|int|R",
            "SelectedStoreId|ST_ID|Integer|int|R",
            "RequestedPrice|Price|Float|float|R",
            "RequestedQuantity|QYT|Decimal|numeric(18,2)|R",
            "RequestedTotal|Total|Float|float|R",
            "RequestedExpiry|D_Vaild|String|date|O",
            "RequestedSale|NewSale|Decimal|numeric(18,2)|O",
            "RequestedSaleByOne|NewSaleByOne|Decimal|numeric(18,2)|O",
            "RequestedMinimum|Min_SP|Decimal|numeric(18,2)|O",
            "RequestedMinimumByOne|Min_SP_ByOne|Decimal|numeric(18,2)|O",
            "RequestedMinimum2|Min_SP_2|Decimal|numeric(18,2)|O",
            "RequestedMinimum2ByOne|Min_SP_2_ByOne|Decimal|numeric(18,2)|O",
            "OnUpdate|On_Update|Boolean|bit|R",
            "RequestedInvoiceType|Invoice_Type|Integer|int|O",
            "RequestedBarcode|Barcode|String|nvarchar(100)|O",
            "RequestedForeignPrice|Price_By_Foriegn_Cr|Float|float|O",
            "RequestedExchangePrice|Price_By_Equal|Float|float|O",
            "RequestedCurrencyId|Cr_ID|Integer|int|O"}
    End Function

    Friend Shared Function PurchaseEntities() As String()
        Return {"PurchaseDocument", "PurchaseLine", "PurchaseItem", "PurchaseUnit", "PurchaseExpiry", "StockBucket"}
    End Function

    Friend Shared Function PurchaseFields(entity As String) As String()
        Select Case entity
            Case "PurchaseDocument" : Return VoidFields("BusinessDocument")
            Case "StockBucket" : Return ReturnFields("StockBucket")
            Case "PurchaseLine" : Return {"PurchaseDocumentId|Pch_T_ID|Integer|int",
                "PurchaseDate|Date|DateTime|datetime",
                "PurchaseItemId|IM_ID|Integer|int",
                "PurchaseUnitId|U_ID|Integer|bigint",
                "PurchaseStoreId|ST_ID|Integer|bigint",
                "PurchasePrice|Price|Float|float",
                "PurchaseExpiryText|D_Vaild|String|nvarchar(50)",
                "PurchaseQuantity|QYT|Float|float",
                "PurchaseTotal|Total|Float|float",
                "PurchaseNotes|Notes|String|nvarchar(max)",
                "PurchaseBarcode|Barcode|String|nvarchar(max)",
                "PurchaseNewSale|NewSale|Decimal|numeric(18,2)",
                "PurchaseNewSaleByOne|NewSaleByOne|Decimal|numeric(18,2)",
                "PurchaseCargo|U_Cargo|Float|float",
                "PurchaseStockQuantity|ST_QTY|Decimal|numeric(18,2)",
                "PurchaseApproved|isDepended|Boolean|bit",
                "PurchaseMinSale|Min_SP|Decimal|numeric(18,2)",
                "PurchaseMinSaleByOne|Min_SP_ByOne|Decimal|numeric(18,2)",
                "PurchaseAverageCost|AVG_Cost|Float|float",
                "PurchaseMinSale2|Min_SP_2|Decimal|numeric(18,2)",
                "PurchaseMinSale2ByOne|Min_SP_2_ByOne|Decimal|numeric(18,2)",
                "PurchaseCostChange|AVG_COSTChange|Float|float",
                "PurchaseStockDifference|ST_DIF_QTY|Decimal|numeric(18,2)",
                "PurchaseExpenses|EXP_VALUE|Float|float",
                "PurchaseOriginalPrice|Org_Price|Float|float",
                "PurchaseForeignPrice|Price_By_Foriegn_Cr|Float|float",
                "PurchaseExchangePrice|Price_By_Equal|Float|float",
                "PurchaseCurrencyId|Cr_ID|Integer|int"}
            Case "PurchaseItem" : Return {"PurchaseItemName|item_name|String|nvarchar(500)",
                "PurchaseStoreMode|isStore|Integer|int",
                "PurchaseItemCost|Cost|Float|float",
                "PurchaseLastLine|Last_PchT_ID|Integer|int",
                "PurchaseLastCargoCost|Last_Pch_Cargo|Decimal|numeric(18,3)"}
            Case "PurchaseUnit" : Return {"PriceItemId|IM_ID|Integer|int",
                "PriceUnitId|U_ID|Integer|int",
                "UnitSalePrice|Price|Decimal|numeric(18,3)",
                "UnitMinimumPrice|Min_SP|Decimal|numeric(18,3)",
                "UnitMinimumPrice2|Min_SP_2|Decimal|numeric(18,3)"}
            Case "PurchaseExpiry" : Return {"ExpiryLineId|Pch_T_ID|Integer|int",
                "ExpiryItemId|IM_ID|Integer|int",
                "ExpiryDate|VALID_DATE|DateTime|datetime"}
            Case Else : Throw New ArgumentException("كيان إضافة مشتريات/جرد غير معتمد.")
        End Select
    End Function

    Friend Shared Function IsReturnLine(operation As String) As Boolean
        Return {"Return.LineAdded", "Return.LineDeleted", "Return.LineQuantityChanged"}.Contains(operation, StringComparer.Ordinal)
    End Function

    Friend Shared Function IsExpenseLine(operation As String) As Boolean
        Return {"Expense.LineAdded", "Expense.LineDeleted"}.Contains(operation, StringComparer.Ordinal)
    End Function

    Friend Shared Function AccountEntryFields() As String()
        Return VoidFields("BusinessDocument").Concat({"EntryTitle|Receipt_Title|String|nvarchar(550)", "EntryUserId|User_ID|Integer|int"}).ToArray()
    End Function

    Friend Shared Function ExpenseInputs(operation As String) As String()
        If Not IsExpenseLine(operation) Then Throw New ArgumentException("عملية مصروف غير معتمدة.")
        Dim values As New List(Of String) From {"TargetLineId", "OnUpdate", "ExpectedExpenseSnapshot"}
        If operation = "Expense.LineAdded" Then values.AddRange({"ExpenseId", "ExpenseQuantity", "ExpensePrice", "ExpenseTotal"})
        Return values.ToArray()
    End Function

    Friend Shared Function ExpenseFields(entity As String) As String()
        If entity = "ExpenseDocument" Then Return VoidFields("BusinessDocument")
        If entity <> "ExpenseLine" Then Throw New ArgumentException("كيان بند مصروف غير معتمد.")
        Return {"ExpenseDocumentId|IMEX_T_ID|Integer|int", "ExpenseDate|Date|DateTime|datetime",
            "ExpenseId|EX_ID|Integer|bigint", "ExpenseQuantity|QYT|Decimal|numeric(18,3)",
            "ExpensePrice|Price|Decimal|numeric(18,3)", "ExpenseTotal|Total|Decimal|numeric(18,3)",
            "ExpenseApproved|isDepended|Boolean|bit"}
    End Function

    Friend Shared Function ReturnSpec(kind As Integer) As String()
        Select Case kind
            Case 10 : Return {"Rtn_Contents", "Bill_T_ID", "ReturnLine", "SRtn_ID", "SB_Rtn"}
            Case 11 : Return {"Rtn_Contents", "Bill_T_ID", "ReturnLine", "PRtn_ID", "Pch_Rtn"}
            Case Else : Throw New ArgumentException("نوع المرتجع غير معتمد.")
        End Select
    End Function

    Friend Shared Function ReturnOptionalInputs() As String()
        Return {"RLSellerPercent", "RLOriginalBillId", "RLMarketerValue", "RLSerial", "RLMinPrice", "RLMinPrice2"}
    End Function

    Friend Shared Function ReturnParameter(name As String) As String
        If Not ReturnOptionalInputs().Contains(name) Then Throw New ArgumentException("معامل المرتجع غير معتمد.")
        Return ReturnFields("ReturnLine").Single(Function(x) x.Split("|"c)(0) = name).Split("|"c)(1)
    End Function

    Friend Shared Function ReturnInputs(operation As String) As String()
        Dim names As New List(Of String) From {"DocumentType", "TargetLineId", "ExpectedReturnSnapshot", "OnUpdate", "AllowNegative"}
        Select Case operation
            Case "Return.LineAdded"
                names.AddRange({"SelectedItemId", "SelectedUnitId", "SelectedStoreId", "RequestedQuantity", "RequestedCost", "RequestedPrice", "RequestedBarcode", "RequestedExpiry"})
                names.AddRange(ReturnOptionalInputs())
            Case "Return.LineQuantityChanged" : names.AddRange({"DisplayedQuantity", "QuantityDelta"})
            Case "Return.LineDeleted"
            Case Else : Throw New ArgumentException("عملية مرتجع غير معتمدة.")
        End Select
        Return names.ToArray()
    End Function

    Friend Shared Function ReturnFields(entity As String) As String()
        If entity = "ReturnDocument" Then Return VoidFields("BusinessDocument")
        If entity = "StockBucket" Then Return SalesQuantityFields(entity)
        If entity <> "ReturnLine" Then Throw New ArgumentException("كيان مرتجع غير معتمد.")
        Return {
            "RLDate|Date|DateTime|datetime",
            "RLDocumentId|Bill_T_ID|Integer|int",
            "RLReturnType|Rtn_Type_ID|Integer|int",
            "RLItemId|IM_ID|Integer|int",
            "RLUnitId|U_ID|Integer|bigint",
            "RLStoreId|ST_ID|Integer|bigint",
            "RLComponents|Compons|String|nvarchar(max)",
            "RLCost|Cost|Decimal|numeric(18,2)",
            "RLPrice|Price|Decimal|numeric(18,2)",
            "RLExpiry|D_Vaild|String|nvarchar(50)",
            "RLQuantity|QTY|Float|float(53)",
            "RLTotal|T_Price|Decimal|numeric(18,2)",
            "RLCargo|U_Cargo|Float|float(53)",
            "RLStoredQuantity|ST_QTY|Decimal|numeric(18,2)",
            "RLApproved|isDepended|Integer|int",
            "RLSellerPercent|Saler_Percent|Decimal|numeric(18,2)",
            "RLOriginalBillId|Orginal_Bill_T_ID|Integer|int",
            "RLProjectId|Proj_ID|Integer|int",
            "RLMarketerValue|Markter_Val|Decimal|numeric(18,3)",
            "RLBarcode|Barcode|String|nvarchar(max)",
            "RLSerial|Serial_Code|String|nvarchar(100)",
            "RLMinPrice|is_By_Min_SP|Integer|int",
            "RLMinPrice2|is_By_Min_SP_2|Integer|int"}
    End Function

    Friend Shared Function IsStockDocumentLine(operation As String) As Boolean
        Return {"StockDocument.LineAdded", "StockDocument.LineDeleted", "StockDocument.LineQuantityChanged"}.Contains(operation, StringComparer.Ordinal)
    End Function

    ' Table | parent | entity | document number | permission | insert screen | delete screen.
    Friend Shared Function StockDocumentSpec(kind As Integer) As String()
        Select Case kind
            Case 35 : Return {"Outsale_Details", "Outsale_T_ID", "StockReceiptLine", "OutSale_ID", "StExplore", "Outside_Sales_IM_card,Outside_Sales_Touch", "Outside_Sales,Outside_Sales_Touch"}
            Case 17 : Return {"InSale_Details", "InSale_T_ID", "StockIssueLine", "InSale_ID", "StExplore", "Inside_Sales_IM_card", "Inside_Sales"}
            Case 8 : Return {"IMEX_Details", "IMEX_T_ID", "StockDamageLine", "IMEX_ID", "StExplore", "IMEX_IM_card", "IM_Execute"}
            Case 12 : Return {"Store_Trans_Details", "B_T_ID", "StockTransferLine", "ST_Tran_ID", "Add_Draw_St", "Stores_Trans_IM_card", "Stores_ImmediateOrder"}
            Case Else : Throw New ArgumentException("نوع الإذن المخزني غير معتمد.")
        End Select
    End Function

    Friend Shared Function StockDocumentInputs(operation As String, kind As Integer) As String()
        StockDocumentSpec(kind)
        Dim names As New List(Of String) From {"DocumentType", "TargetLineId", "ExpectedStockDocumentSnapshot"}
        Select Case operation
            Case "StockDocument.LineAdded"
                names.AddRange({"SelectedItemId", "SelectedUnitId", "SelectedStoreId", "RequestedQuantity", "RequestedCost", "RequestedBarcode", "RequestedExpiry", "OnUpdate"})
                If kind = 12 Then names.Add("DestinationStoreId") Else names.Add("RequestedTotal")
                If kind = 35 Then names.Add("RequestedSalePrice")
            Case "StockDocument.LineQuantityChanged"
                If kind <> 35 Then Throw New ArgumentException("تعديل الكمية اللمسي خاص بإذن الاستلام.")
                names.AddRange({"DisplayedQuantity", "QuantityDelta"})
            Case "StockDocument.LineDeleted"
            Case Else : Throw New ArgumentException("عملية إذن مخزني غير معتمدة.")
        End Select
        Return names.ToArray()
    End Function

    Friend Shared Function StockDocumentFields(entity As String) As String()
        If entity = "StockDocument" Then Return VoidFields("BusinessDocument")
        If entity = "StockBucket" Then Return SalesQuantityFields(entity)
        If Not {"StockReceiptLine", "StockIssueLine", "StockDamageLine", "StockTransferLine"}.Contains(entity) Then Throw New ArgumentException("كيان إذن مخزني غير معتمد.")
        Dim transfer = entity = "StockTransferLine", small = entity = "StockReceiptLine" OrElse transfer
        Dim parent = If(transfer, "B_T_ID", If(entity = "StockReceiptLine", "Outsale_T_ID", If(entity = "StockIssueLine", "InSale_T_ID", "IMEX_T_ID")))
        Dim result As New List(Of String) From {
            "SLDocumentId|" & parent & "|Integer|int", "SLDate|Date|DateTime|datetime", "SLItemId|IM_ID|Integer|int",
            "SLUnitId|U_ID|Integer|" & If(small, "int", "bigint"), "SLCost|" & If(transfer, "Price", "Cost") & "|Decimal|numeric(18,2)",
            "SLExpiry|D_Vaild|String|nvarchar(50)", "SLQuantity|" & If(transfer, "QTY", "QYT") & "|Decimal|numeric(18,2)",
            "SLTotal|Total|Decimal|numeric(18,2)", "SLCargo|U_Cargo|Float|float(53)", "SLStoredQuantity|ST_QTY|Decimal|numeric(18,2)",
            If(transfer, "SLApprovedInt|isDepended|Integer|int", "SLApproved|isDepended|Boolean|bit"),
            "SLBarcode|Barcode|String|" & If(transfer OrElse entity = "StockDamageLine", "nvarchar(50)", "nvarchar(max)")}
        If transfer Then
            result.AddRange({"SLFromStoreId|ST_F_ID|Integer|bigint", "SLToStoreId|ST_To_ID|Integer|bigint", "SLSalePrice|SPrice|Decimal|numeric(18,2)"})
        Else
            result.Add("SLStoreId|ST_ID|Integer|" & If(small, "int", "bigint"))
            If entity <> "StockDamageLine" Then result.Add("SLSalePrice|Price|Decimal|numeric(18,3)")
        End If
        Return result.ToArray()
    End Function

    Friend Shared Function IsFinance(operation As String) As Boolean
        Return {"TreasuryMovement.Created", "TreasuryMovement.Voided", "TreasuryTransfer.Created", "TreasuryTransfer.Voided", "AccountTransfer.Created", "AccountTransfer.Voided"}.Contains(operation)
    End Function

    Friend Shared Function FinanceEntity(operation As String) As String
        If operation.StartsWith("AccountTransfer.", StringComparison.Ordinal) Then Return "FinanceAccountRow"
        If operation.StartsWith("TreasuryTransfer.", StringComparison.Ordinal) Then Return "FinanceTreasuryTransfer"
        Return "FinanceTreasuryRow"
    End Function

    Friend Shared Function FinanceScreen(operation As String) As String
        If operation.StartsWith("AccountTransfer.", StringComparison.Ordinal) Then Return "AG_Transfers"
        If operation.StartsWith("TreasuryTransfer.", StringComparison.Ordinal) Then Return "Tr_Transfers"
        Return "Tr_Deposit_Withdraw"
    End Function

    Friend Shared Function FinanceInputs(operation As String) As String()
        If operation.EndsWith(".Voided", StringComparison.Ordinal) Then Return {"ExpectedFinanceSnapshot"}
        Select Case operation
            Case "TreasuryMovement.Created" : Return {"RequestedDate", "RequestedAmount", "MovementType", "TreasuryId", "OpeningFlag", "Description"}
            Case "TreasuryTransfer.Created" : Return {"RequestedDate", "RequestedAmount", "FromId", "ToId", "Description"}
            Case "AccountTransfer.Created" : Return {"RequestedDate", "RequestedAmount", "FromId", "ToId", "Description", "Title"}
            Case Else : Throw New ArgumentException("عملية مالية غير معتمدة.")
        End Select
    End Function

    Friend Shared Function FinanceFields(entity As String) As String()
        Select Case entity
            Case "FinanceTreasuryRow" : Return {"CashAGBalanceTID|AGBalance_T_ID|Integer|int", "CashTranID|Tran_ID|Integer|int", "Cashdate|date|DateTime|datetime", "CashTrID|Tr_ID|Integer|int", "Cashnoticemove|notice_move|String|nvarchar(max)", "CashBankName|Bank_Name|String|nvarchar(100)", "CashCheckNum|CheckNum|String|nvarchar(100)", "CashPure|Pure|Decimal|numeric(18,3)", "CashDebit|Debit|Decimal|numeric(18,3)", "CashCredit|Credit|Decimal|numeric(18,3)", "CashBalance|Balance|Decimal|numeric(18,3)", "CashTrTypeID|Tr_Type_ID|Integer|int", "CashisTranFrom|isTran_From|Boolean|bit", "CashisTranTo|isTran_To|Boolean|bit", "CashUserID|User_ID|Integer|int", "CashisVoid|isVoid|Boolean|bit", "CashisFirstTime|isFirst_Time|Integer|int", "CashJournalId|JournalId|Integer|int", "CashPostedAt|PostedAt|DateTime|datetime", "CashPostedBy|PostedBy|Integer|int", "CashOriginalJournalId|OriginalJournalId|Integer|int", "CashLastReversalJournalId|LastReversalJournalId|Integer|int", "CashNeedRepost|NeedRepost|Boolean|bit", "CashEditVersion|EditVersion|Integer|int", "CashEditOpenedAt|EditOpenedAt|DateTime|datetime", "CashEditOpenedBy|EditOpenedBy|Integer|int", "CashLastRepostAt|LastRepostAt|DateTime|datetime", "CashLastRepostBy|LastRepostBy|Integer|int", "CashVoidAt|VoidAt|DateTime|datetime", "CashVoidBy|VoidBy|Integer|int", "CashVoidReason|VoidReason|String|nvarchar(500)", "CashVoidReversalJournalId|VoidReversalJournalId|Integer|int", "CashNeedCancelReverse|NeedCancelReverse|Boolean|bit"}
            Case "FinanceAccountRow" : Return {"AccountDATE|DATE|DateTime|datetime", "AccountAGIDFROM|AG_ID_FROM|Integer|int", "AccountAGIDTO|AG_ID_TO|Integer|int", "AccountTITLE|TITLE|String|nvarchar(2500)", "AccountABOUT|ABOUT|String|nvarchar(max)", "AccountUSERID|USER_ID|Integer|int", "AccountisVoid|is_Void|Integer|int", "AccountVALUE|VALUE|Decimal|numeric(18,3)", "AccountBsTypeID|BsType_ID|Integer|int", "AccountJournalId|JournalId|Integer|int", "AccountPostedAt|PostedAt|DateTime|datetime", "AccountPostedBy|PostedBy|Integer|int", "AccountOriginalJournalId|OriginalJournalId|Integer|int", "AccountLastReversalJournalId|LastReversalJournalId|Integer|int", "AccountNeedRepost|NeedRepost|Boolean|bit", "AccountEditVersion|EditVersion|Integer|int", "AccountEditOpenedAt|EditOpenedAt|DateTime|datetime", "AccountEditOpenedBy|EditOpenedBy|Integer|int", "AccountLastRepostAt|LastRepostAt|DateTime|datetime", "AccountLastRepostBy|LastRepostBy|Integer|int", "AccountVoidAt|VoidAt|DateTime|datetime", "AccountVoidBy|VoidBy|Integer|int", "AccountVoidReason|VoidReason|String|nvarchar(500)", "AccountVoidReversalJournalId|VoidReversalJournalId|Integer|int", "AccountNeedCancelReverse|NeedCancelReverse|Boolean|bit"}
            Case Else : Throw New ArgumentException("كيان مالي غير معتمد.")
        End Select
    End Function

    Friend Shared Function IsTableOperation(operation As String) As Boolean
        Return {"Table.Transferred", "Table.LineQuantityChanged", "Table.LineDeleted"}.Contains(operation)
    End Function

    Friend Shared Function TableEntities(operation As String) As String()
        If operation = "Table.Transferred" Then Return {"TableState", "TableInvoice"}
        If {"Table.LineQuantityChanged", "Table.LineDeleted"}.Contains(operation) Then Return {"TableInvoice", "TableLine", "TablePrintRow"}
        Throw New ArgumentException("عملية طاولة غير معتمدة.")
    End Function

    Friend Shared Function TableInputs(operation As String) As String()
        Select Case operation
            Case "Table.Transferred" : Return {"ExpectedTableSnapshot", "DestinationTableId"}
            Case "Table.LineQuantityChanged" : Return {"ExpectedTableSnapshot", "TargetLineId", "DisplayedQuantity", "QuantityDelta"}
            Case "Table.LineDeleted" : Return {"ExpectedTableSnapshot", "TargetLineId", "DisplayedQuantity"}
            Case Else : Throw New ArgumentException("عملية طاولة غير معتمدة.")
        End Select
    End Function

    Friend Shared Function TableFields(entity As String) As String()
        Select Case entity
            Case "TableState"
                Return {"TableFloor|Flate_ID|Integer|int", "TableName|T_Name|String|nvarchar(100)", "TableActive|isActive|Boolean|bit",
                    "TableReserved|isReserve|Boolean|bit", "TableBusy|isbusy|Boolean|bit", "TableOrderCode|TB_ORDER_CODE|String|nvarchar(100)",
                    "TableCash|is_Cash|Integer|int", "TableOpenedAt|Start_Open|DateTime|datetime", "TablePrint|is_Print|Boolean|bit"}
            Case "TableInvoice"
                Return SalesHeaderFields().Concat({"TableId|Table_ID|Integer|int", "InvoiceTableCode|TB_ORDER_CODE|String|nvarchar(100)",
                    "InvoiceNumber|SB_ID|Integer|int", "InvoiceType|BsType_ID|Integer|int", "InvoicePaid|isPied|Integer|int",
                    "InvoiceOrdered|TB_isOrderd|Integer|int", "InvoiceOwner|User_ID|Integer|int"}).ToArray()
            Case "TableLine" : Return SalesDeleteFields("SalesLine")
            Case "TablePrintRow"
                Return {"QueueStoredId|T_ID|Integer|int", "QueueDate|DATE|DateTime|datetime", "QueueNotes|Notes|String|nvarchar(max)", "QueuePrinter|Printer_IP|String|nvarchar(100)"}
            Case Else : Throw New ArgumentException("كيان طاولة غير معتمد.")
        End Select
    End Function

    Friend Shared Function IsUnit(operation As String) As Boolean
        Return {"Unit.Created", "Unit.Updated", "Unit.Deleted"}.Contains(operation)
    End Function

    Friend Shared Function IsItemMaster(operation As String) As Boolean
        Return {"Item.FastUpdated", "Item.Deleted"}.Contains(operation)
    End Function

    Friend Shared Function ItemMasterEntities() As String()
        Return {"CatalogItemRecord", "ItemUnit", "ItemAlert", "ItemFormat", "ItemDifference"}
    End Function

    Friend Shared Function ItemMasterFields(entity As String) As String()
        Select Case entity
            Case "CatalogItemRecord" : Return {"ItemName|item_name|String|nvarchar(500)", "ItemSalesName|item_nameSales|String|nvarchar(500)", "ItemGroupId|GM_ID|Integer|int", "ItemPhotoSha256|PhotoSha256|String|image", "ItemPhotoBytes|PhotoBytes|Integer|image", "ItemActive|isActive|Boolean|bit", "ItemStoreType|isStore|Integer|int", "ItemUsesExpiry|isValid|Integer|int", "ItemCost|Cost|Float|float(53)", "ItemVariablePrice|isChangePrice|Boolean|bit", "ItemBKR|BK_R|Integer|int", "ItemBKG|BK_G|Integer|int", "ItemBKB|BK_B|Integer|int", "ItemFKR|FK_R|Integer|int", "ItemFKG|FK_G|Integer|int", "ItemFKB|FK_B|Integer|int", "ItemUserId|User_ID|Integer|int", "ItemEnabled|Row_Enabled|Boolean|bit", "ItemLastPurchaseId|Last_PchT_ID|Integer|int", "ItemLastPurchaseCargo|Last_Pch_Cargo|Decimal|numeric(18,3)", "ItemShortcut|is_Shortcut|Integer|int", "ItemNumber|IM_Num|String|nvarchar(100)", "ItemCompanyGroup|Grp_ID|Integer|int", "ItemPhotoPath|IM_Full_Photo|String|nvarchar(max)", "ItemNotes|Notes|String|nvarchar(max)", "ItemMarketerValue|Markter_Val|Decimal|numeric(18,3)", "ItemDate|Date|DateTime|datetime", "ItemMaxAlert|MaxQtyAlert|Decimal|numeric(18,2)", "ItemMinAlert|MinQtyAlert|Decimal|numeric(18,2)"}
            Case "ItemAlert" : Return {"AlertItemId|IM_ID|Integer|int", "AlertStoreId|ST_ID|Integer|bigint", "AlertMinimum|MIN_QTY|Decimal|numeric(18,3)", "AlertMaximum|MAX_QTY|Decimal|numeric(18,3)", "AlertEnabled|Row_Enabled|Boolean|bit", "AlertUserId|User_ID|Integer|bigint"}
            Case "ItemFormat" : Return {"FormatItemId|IM_ID|Integer|int", "FormatComponentId|FRM_IM_ID|Integer|int", "FormatUnitId|U_ID|Integer|bigint", "FormatQuantity|Qty|Float|float(53)", "FormatEnabled|Row_Enabled|Boolean|bit", "FormatUserId|User_ID|Integer|bigint"}
            Case "ItemDifference" : Return {"DifferenceBillId|Bill_T_ID|Integer|int", "DifferenceDate|DATE|DateTime|datetime", "DifferenceItemId|IM_ID|Integer|int", "DifferenceStoreId|ST_ID|Integer|int", "DifferenceExpiry|Valid_D|String|nvarchar(50)", "DifferenceQuantity|ST_QTY|Float|float(53)", "DifferenceCost|Cost|Decimal|numeric(18,3)", "DifferenceCargo|U_Cargo|Decimal|numeric(18,3)", "DifferenceBarcode|Barcode|String|nvarchar(50)", "DifferenceTotal|TOTAL|Decimal|numeric(18,3)"}
            Case "ItemUnit" : Return ItemUnitFields(entity)
            Case Else : Throw New ArgumentException("كيان بطاقة الصنف غير معتمد.")
        End Select
    End Function

    Friend Shared Function ItemMasterInputs(operation As String) As String()
        Select Case operation
            Case "Item.Deleted" : Return {"ExpectedItemSnapshot"}
            Case "Item.FastUpdated" : Return {"ExpectedItemSnapshot", "ItemName", "ItemSalesName", "ItemGroupId", "ItemActive", "ItemNumber", "DefaultUnitId", "RequestedUnitPrice"}
            Case Else : Throw New ArgumentException("عملية بطاقة الصنف غير معتمدة.")
        End Select
    End Function

    Friend Shared Function IsPricingRule(operation As String) As Boolean
        Return {"Offer.Created", "Offer.ItemOffersDeleted", "PriceRule.Created", "PriceRule.Deleted"}.Contains(operation)
    End Function

    Friend Shared Function PricingFields(entity As String) As String()
        Select Case entity
            Case "ItemOffers" : Return {"OfferItemName|item_name|String|nvarchar(500)", "OfferItemSalesName|item_nameSales|String|nvarchar(500)"}
            Case "PriceRuleGroup" : Return {"RuleGroupName|GM_Name|String|nvarchar(200)"}
            Case "OfferSnapshotRow" : Return {"OfferItemId|IM_ID|Integer|int", "OfferCreatedAt|DATE|DateTime|datetime", "OfferTitle|TITLE|String|nvarchar(4000)", "OfferStartDate|DATE_F|Date|date", "OfferEndDate|DATE_T|Date|date", "OfferStartTime|TIME_F|String|time(0)", "OfferEndTime|TIME_T|String|time(0)", "OfferUserId|USER_ID|Integer|int"}
            Case "PriceRule" : Return {"RuleCreatedAt|DATE|DateTime|datetime", "RuleGroupId|GM_ID|Integer|int", "RulePercent|Percent_Price|Decimal|numeric(18,2)"}
            Case Else : Throw New ArgumentException("كيان قاعدة تسعير غير معتمد.")
        End Select
    End Function

    Friend Shared Function PricingInputs(operation As String) As String()
        Select Case operation
            Case "Offer.Created" : Return {"ExpectedPricingSnapshot", "OfferTitle", "OfferStartDate", "OfferEndDate", "RequestedStartClock", "RequestedEndClock"}
            Case "Offer.ItemOffersDeleted" : Return {"ExpectedPricingSnapshot"}
            Case "PriceRule.Created" : Return {"ExpectedPricingSnapshot", "RequestedPercent"}
            Case "PriceRule.Deleted" : Return {"ExpectedPricingSnapshot", "TargetPriceRuleId"}
            Case Else : Throw New ArgumentException("عملية قاعدة تسعير غير معتمدة.")
        End Select
    End Function

    Friend Shared Function IsItemUnit(operation As String) As Boolean
        Return {"ItemUnit.Created", "ItemUnit.Updated", "ItemUnit.Deleted", "ItemUnit.PriceChanged", "ItemUnit.OfferPriceChanged"}.Contains(operation)
    End Function

    Friend Shared Function ItemUnitFields(entity As String) As String()
        Select Case entity
            Case "CatalogItem" : Return {"ItemName|item_name|String|nvarchar(500)", "ItemSalesName|item_nameSales|String|nvarchar(500)"}
            Case "ItemUnit" : Return {"ItemId|IM_ID|Integer|int", "UnitId|U_ID|Integer|int", "UnitBarcode|Barcode|String|nvarchar(max)", "UnitPrice|Price|Decimal|numeric(18,3)", "MinimumPrice|Min_SP|Decimal|numeric(18,3)", "MinimumPrice2|Min_SP_2|Decimal|numeric(18,3)", "DefaultFlag|is_Default|Integer|int", "UnitRowEnabled|Row_Enabled|Boolean|bit", "UnitUserId|User_ID|Integer|int", "OfferPrice|OFFER_PRICE|Decimal|numeric(18,3)"}
            Case "ItemUnitRecipe" : Return {"RecipeItemId|IM_ID|Integer|int", "RecipeItemUnitId|U_IM_ID|Integer|int", "RecipeComponentId|Struct_IM_ID|Integer|int", "RecipeUnitId|U_ID|Integer|int", "RecipeQuantity|Qty|Float|float(53)", "RecipeRowEnabled|Row_Enabled|Boolean|bit", "RecipeUserId|User_ID|Integer|int"}
            Case Else : Throw New ArgumentException("كيان رابط الوحدة غير معتمد.")
        End Select
    End Function

    Friend Shared Function ItemUnitInputs(operation As String) As String()
        Dim common = {"TargetItemUnitId", "ExpectedItemUnitSnapshot"}
        Select Case operation
            Case "ItemUnit.Created" : Return common.Concat({"UnitId", "UnitBarcode", "UnitPrice", "MinimumPrice", "MinimumPrice2", "UnitRowEnabled", "CheckBarcode"}).ToArray()
            Case "ItemUnit.Updated" : Return common.Concat({"UnitBarcode", "UnitPrice", "MinimumPrice", "MinimumPrice2", "MakeDefault", "CheckBarcode"}).ToArray()
            Case "ItemUnit.Deleted" : Return common
            Case "ItemUnit.PriceChanged" : Return common.Concat({"UnitPrice"}).ToArray()
            Case "ItemUnit.OfferPriceChanged" : Return common.Concat({"OfferPrice"}).ToArray()
            Case Else : Throw New ArgumentException("عملية رابط الوحدة غير معتمدة.")
        End Select
    End Function

    Friend Shared Function ItemUnitScreens(operation As String) As String()
        Select Case operation
            Case "ItemUnit.Created" : Return {"ItemsMenu", "Add_Unit"}
            Case "ItemUnit.Deleted" : Return {"ItemsMenu"}
            Case "ItemUnit.Updated" : Return {"Update_IM_Unit"}
            Case "ItemUnit.PriceChanged" : Return {"IM_Change_PriceByVal"}
            Case "ItemUnit.OfferPriceChanged" : Return {"OFFERS"}
            Case Else : Throw New ArgumentException("عملية رابط الوحدة غير معتمدة.")
        End Select
    End Function

    Friend Shared Function UnitFields() As String()
        Return {"UnitName|U_Name|String|nvarchar(250)", "UnitCargo|U_Cargo|Float|float(53)", "UnitDefault|isDefault|Boolean|bit"}
    End Function

    Friend Shared Function IsGroup(operation As String) As Boolean
        Return {"Group.Created", "Group.Updated", "Group.Deleted", "Group.PrinterLinked", "Group.PrinterUnlinked"}.Contains(operation)
    End Function

    Friend Shared Function GroupFields(entity As String) As String()
        If entity = "GroupPrinter" Then Return {"GroupId|GM_ID|Integer|int", "LinkedPrinterId|PTR_ID|Integer|int"}
        If entity <> "ProductGroup" Then Throw New ArgumentException("كيان تصنيف غير معتمد.")
        Return {"GroupName|GM_Name|String|nvarchar(200)", "PrinterId|Printer_ID|Integer|bigint",
            "BackRed|BK_R|Integer|int", "BackGreen|BK_G|Integer|int", "BackBlue|BK_B|Integer|int",
            "ForeRed|FK_R|Integer|int", "ForeGreen|FK_G|Integer|int", "ForeBlue|FK_B|Integer|int",
            "ShowInPos|POS_isShow|Integer|int", "GroupRank|Rank_Num|Float|float(53)", "GroupStoreId|POS_ST_ID|Integer|int",
            "KitchenScreen|Ksh_Screen|String|nvarchar(200)", "CategoryId|Cat_ID|Integer|int"}
    End Function

    Friend Shared Function GroupInputs(operation As String) As String()
        Select Case operation
            Case "Group.Created" : Return {"GroupName", "PrinterId", "BackRed", "BackGreen", "BackBlue", "ForeRed", "ForeGreen", "ForeBlue", "ShowInPos", "KitchenScreen"}
            Case "Group.Updated" : Return GroupInputs("Group.Created").Concat({"RequestedRank", "ExpectedGroupSnapshot"}).ToArray()
            Case "Group.Deleted" : Return {"ExpectedGroupSnapshot"}
            Case "Group.PrinterLinked" : Return {"ExpectedGroupSnapshot", "LinkedPrinterId"}
            Case "Group.PrinterUnlinked" : Return {"ExpectedGroupSnapshot", "PrinterLinkId"}
            Case Else : Throw New ArgumentException("عملية تصنيف غير معتمدة.")
        End Select
    End Function

    ' Closed cancellation graph; no SQL identifiers come from UI values.
    ' type -> number column | permission | allowed screens | line table | parent column
    Friend Shared Function VoidSpec(kind As Integer) As String()
        Select Case kind
            Case 1 : Return {"SB_ID", "SalesVoid", "Sales,Sales_Fast,POS,OrdersMenu,POS_Abcent_Bills,ShowBill_IM", "SB_Contents", "Bill_T_ID"}
            Case 5, 6, 37 : Return {"T_ID", "Balance", "Add_Prev_Balance", "", ""}
            Case 2 : Return {"EXP_ID", "ExpVoid", "EXP_Details", "EXP_Details", "IMEX_T_ID"}
            Case 7 : Return {"Pch_ID", "PchVoid", "Pch", "Pch_Details", "Pch_T_ID"}
            Case 8 : Return {"IMEX_ID", "StExplore", "IM_Execute", "IMEX_Details", "IMEX_T_ID"}
            Case 9 : Return {"Jrd_ID", "PchVoid", "Invoice", "Pch_Details", "Pch_T_ID"}
            Case 10 : Return {"SRtn_ID", "SalesVoid", "Returns", "Rtn_Contents", "Bill_T_ID"}
            Case 11 : Return {"PRtn_ID", "SalesVoid", "Returns", "Rtn_Contents", "Bill_T_ID"}
            Case 13 : Return {"Frm_ID", "FRM", "Format_Items_Auto", "FRM_Details", "FRM_T_ID"}
            Case 16 : Return {"ViewSB_ID", "SalesVoid", "ViewBill", "", ""}
            Case 17 : Return {"InSale_ID", "StExplore", "Inside_Sales", "InSale_Details", "InSale_T_ID"}
            Case 18 : Return {"Frm_ID_M", "FRM_M", "Format_Items_Manual", "FRM_Details", "FRM_T_ID"}
            Case 35 : Return {"OutSale_ID", "StExplore", "Outside_Sales,Outside_Sales_Touch", "OutSale_Details", "OutSale_T_ID"}
            Case Else : Throw New ArgumentException("نوع المستند غير معتمد لمسار الإلغاء الجديد.")
        End Select
    End Function

    Friend Shared Function VoidEntities() As String()
        Return {"BusinessDocument", "Receipt", "TreasuryMovement", "SB_Contents", "EXP_Details", "Pch_Details", "IMEX_Details", "Rtn_Contents", "FRM_Details", "FRM_Contents_Details", "InSale_Details", "OutSale_Details", "SalesStructure", "StockBucket", "DiningTable", "CancellationPrintQueue"}
    End Function

    Friend Shared Function VoidAllowedEntities(kind As Integer) As String()
        Dim values As New List(Of String) From {"BusinessDocument", "Receipt", "TreasuryMovement", "StockBucket", "DiningTable"}
        Dim spec = VoidSpec(kind)
        If spec(3) <> "" Then values.Add(spec(3))
        If kind = 1 Then values.AddRange({"SalesStructure", "CancellationPrintQueue"})
        If kind = 13 OrElse kind = 18 Then values.Add("FRM_Contents_Details")
        Return values.ToArray()
    End Function

    Friend Shared Function VoidFields(entity As String) As String()
        Dim accounting = {"Voided|isVoid|Integer|int", "NeedRepost|NeedRepost|Boolean|bit", "NeedCancelReverse|NeedCancelReverse|Boolean|bit",
            "VoidAt|VoidAt|DateTime|datetime", "VoidBy|VoidBy|Integer|int", "VoidReason|VoidReason|String|nvarchar(500)",
            "JournalId|JournalId|Integer|int", "DocumentTotal|Total|Decimal|numeric(18,3)", "DocumentPure|Pure|Decimal|numeric(18,3)",
            "Discount|Discount|Decimal|numeric(18,3)", "Debit|Debit|Decimal|numeric(18,3)", "Credit|Credit|Decimal|numeric(18,3)",
            "Balance|Balance|Decimal|numeric(18,3)", "AccountId|AG_ID|Integer|int", "Approved|isDepended|Integer|int"}
        Select Case entity
            Case "BusinessDocument"
                Return accounting.Concat({"DocumentType|BsType_ID|Integer|int", "DocumentNumber|AuditDocumentNumber|Integer|int", "DocumentDate|Date|DateTime|datetime",
                    "SalesOpen|SB_isOpen|Boolean|bit", "Paused|isPause|Boolean|bit", "OrderDelivered|Order_isDeleverd|Boolean|bit", "PaidState|isPied|Integer|int", "TableId|Table_ID|Integer|int"}).ToArray()
            Case "Receipt"
                Return accounting.Concat({"ReceiptNumber|Receipt_Num|Integer|int", "ReceiptDocumentId|Receipt_Tran_ID|Integer|int", "PaymentId|Pay_ID|Integer|int", "TreasuryId|Tr_ID|Integer|int", "DocumentType|BsType_ID|Integer|int"}).ToArray()
            Case "TreasuryMovement"
                Return {"TreasuryId|Tr_ID|Integer|int", "TreasuryDocumentId|AGBalance_T_ID|Integer|int", "TreasuryType|Tr_Type_ID|Integer|int", "TreasuryVoided|isVoid|Boolean|bit",
                    "DocumentPure|Pure|Decimal|numeric(18,3)", "Debit|Debit|Decimal|numeric(18,3)", "Credit|Credit|Decimal|numeric(18,3)", "Balance|Balance|Decimal|numeric(18,3)"}
            Case "SalesStructure" : Return SalesQuantityFields(entity)
            Case "StockBucket" : Return SalesQuantityFields(entity)
            Case "DiningTable" : Return {"TableBusy|isbusy|Boolean|bit", "TableOrderCode|TB_ORDER_CODE|String|nvarchar(100)"}
            Case "CancellationPrintQueue" : Return {"PrintQueueRows|RowCount|Integer|bigint"}
            Case "EXP_Details"
                Return {"ExpenseId|EX_ID|Integer|bigint", "ExpenseQuantity|QYT|Decimal|numeric(18,3)", "ExpensePrice|Price|Decimal|numeric(18,3)", "ExpenseTotal|Total|Decimal|numeric(18,3)", "LineApprovedBit|isDepended|Boolean|bit"}
            Case "SB_Contents", "Pch_Details", "IMEX_Details", "Rtn_Contents", "FRM_Details", "FRM_Contents_Details", "InSale_Details", "OutSale_Details"
                Dim storeType = If(entity = "OutSale_Details", "int", "bigint")
                Dim qtyType = If(entity = "FRM_Contents_Details", "numeric(18,3)", "numeric(18,2)")
                Dim approved = If({"SB_Contents", "Rtn_Contents", "FRM_Contents_Details"}.Contains(entity), "LineApproved|isDepended|Integer|int", "LineApprovedBit|isDepended|Boolean|bit")
                Return {"ItemId|IM_ID|Integer|int", "StoreId|ST_ID|Integer|" & storeType, "ExpiryText|D_Vaild|String|nvarchar(50)", "StoredQuantity|ST_QTY|Decimal|" & qtyType, approved}
            Case Else : Throw New ArgumentException("كيان غير معتمد في تفاصيل الإلغاء.")
        End Select
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
        If IsPeopleCard(request.OperationCode) Then ValidatePeopleRequest(request)
        If IsTableSplit(request.OperationCode) Then ValidateTableSplitRequest(request)
        If IsReceipt(request.OperationCode) Then ValidateReceiptRequest(request)
        If request.OperationCode = "Salary.BatchPosted" Then ValidateSalaryRequest(request)
        If IsItemLifecycle(request.OperationCode) Then ValidateItemLifecycleRequest(request)
        If IsWorkflow(request.OperationCode) Then ValidateWorkflowRequest(request)
        If IsInventoryOperation(request.OperationCode) Then ValidateInventoryRequest(request)

        If IsQuoteLine(request.OperationCode) Then
            Dim required = QuoteInputs(request.OperationCode), id As Integer, create = request.OperationCode = "Quote.LineAdded"
            If request.EntityType <> "QuoteDocument" OrElse Not Integer.TryParse(request.EntityId, id) OrElse id <= 0 OrElse request.Context.ScreenCode <> QuoteScreen(request.OperationCode) OrElse names.Count <> required.Length Then Throw New ArgumentException("هوية طلب عرض السعر غير صالحة.")
            For Each d In required
                Dim p = d.Split("|"c), input = request.Inputs.SingleOrDefault(Function(x) x.Name = p(0))
                If input Is Nothing OrElse If(p(3) = "R", input.Value.State <> AuditValueState.Present, input.Value.State < AuditValueState.SqlNull) Then Throw New ArgumentException("مدخل عرض السعر غير مكتمل: " & p(0))
            Next
            Dim v As Func(Of String, String) = Function(name) request.Inputs.Single(Function(x) x.Name = name).Value.Text
            If v("DocumentType") <> "16" OrElse Not {"0", "1"}.Contains(v("OnUpdate")) OrElse Not Regex.IsMatch(v("ExpectedQuoteSnapshot"), "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("نوع/حالة/بصمة العرض غير صالحة.")
            If Not Integer.TryParse(v("TargetLineId"), id) OrElse If(create, id <> 0, id <= 0) Then Throw New ArgumentException("سطر العرض غير صالح.")
            For Each selectedInput As AuditInput In request.Inputs.Where(Function(x) {"SelectedItemId", "SelectedUnitId", "SelectedStoreId"}.Contains(x.Name))
                If Not Integer.TryParse(selectedInput.Value.Text, id) OrElse id <= 0 Then Throw New ArgumentException("مرجع الصنف/الوحدة/المخزن غير صالح.")
            Next
            If create OrElse request.OperationCode = "Quote.LineChanged" Then
                If Double.Parse(v("RequestedQuantity"), CultureInfo.InvariantCulture) <= 0R Then Throw New ArgumentException("الكمية يجب أن تكون موجبة.")
                If Not create AndAlso (Decimal.Round(Decimal.Parse(v("RequestedQuantity"), CultureInfo.InvariantCulture), 2, MidpointRounding.AwayFromZero) <= 0D OrElse Decimal.Round(Decimal.Parse(v("RequestedQuantity"), CultureInfo.InvariantCulture), 2, MidpointRounding.AwayFromZero) >= 10000000000000000D) Then Throw New ArgumentException("كمية التعديل تتجاوز نوع SQL.")
            End If
            If create Then Text(v("RequestedBarcode"), 50, "Barcode")
            If request.OperationCode = "Quote.LineQuantityStepped" AndAlso (Not {"-1", "1"}.Contains(v("QuantityDelta")) OrElse Double.Parse(v("DisplayedQuantity"), CultureInfo.InvariantCulture) + CInt(v("QuantityDelta")) <= 0R) Then Throw New ArgumentException("فرق كمية العرض غير صالح.")
        End If

        If IsDocumentField(request.OperationCode) Then
            Dim id As Integer, required = DocumentFieldInputs(request.OperationCode)
            If request.EntityType <> "DocumentHeader" OrElse Not Integer.TryParse(request.EntityId, id) OrElse id <= 0 OrElse names.Count <> required.Length OrElse required.Any(Function(x) Not names.Contains(x)) OrElse request.Inputs.Any(Function(x) x.Value.State <> AuditValueState.Present) Then Throw New ArgumentException("طلب رأس الفاتورة غير مكتمل.")
            Dim v As Func(Of String, String) = Function(name) request.Inputs.Single(Function(x) x.Name = name).Value.Text
            If Not Integer.TryParse(v("DocumentType"), id) OrElse Not DocumentFieldScreens(request.OperationCode, id).Contains(request.Context.ScreenCode) Then Throw New ArgumentException("نوع الفاتورة لا يتبع الشاشة.")
            If Not {"0", "1"}.Contains(v("OnUpdate")) OrElse Not Regex.IsMatch(v("ExpectedDocumentFields"), "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("حالة التعديل أو بصمة الفاتورة غير صالحة.")
            If request.OperationCode = "Document.AccountChanged" Then
                If Not Integer.TryParse(v("AccountId"), id) OrElse id <= 0 Then Throw New ArgumentException("الحساب الجديد غير صالح.")
            Else
                Dim amount As Decimal
                If Not Decimal.TryParse(v("Discount"), NumberStyles.Number, CultureInfo.InvariantCulture, amount) OrElse Math.Abs(Decimal.Round(amount, 3, MidpointRounding.AwayFromZero)) >= 1000000000000000D Then Throw New ArgumentException("قيمة الخصم تتجاوز نوع قاعدة البيانات.")
                If request.OperationCode = "Document.FastDiscountChanged" AndAlso (Not Decimal.TryParse(v("DiscountPercent"), NumberStyles.Number, CultureInfo.InvariantCulture, amount) OrElse Math.Abs(Decimal.Round(amount, 3, MidpointRounding.AwayFromZero)) >= 1000000000000000D) Then Throw New ArgumentException("نسبة الخصم تتجاوز نوع قاعدة البيانات.")
            End If
        End If

        If IsUnit(request.OperationCode) Then
            Dim create = request.OperationCode = "Unit.Created", delete = request.OperationCode = "Unit.Deleted"
            Dim required As New List(Of String)
            If Not create Then required.Add("ExpectedUnitSnapshot")
            If Not delete Then required.AddRange({"UnitName", "UnitCargo"})
            If request.EntityType <> "Unit" OrElse names.Count <> required.Count OrElse required.Any(Function(x) Not names.Contains(x)) OrElse request.Inputs.Any(Function(x) x.Value.State <> AuditValueState.Present) Then Throw New ArgumentException("طلب الوحدة غير مكتمل.")
            If Not {"Units", "ItemsMenu", "IM_ADD_New", "Add_Unit"}.Contains(request.Context.ScreenCode) OrElse (Not create AndAlso request.Context.ScreenCode <> "Units") Then Throw New ArgumentException("شاشة الوحدة غير معتمدة.")
            Dim id As Integer
            If If(create, request.EntityId <> "New", Not Integer.TryParse(request.EntityId, id) OrElse id <= 2) Then Throw New ArgumentException("معرف الوحدة غير صالح أو محمي.")
            If Not create AndAlso Not Regex.IsMatch(request.Inputs.Single(Function(x) x.Name = "ExpectedUnitSnapshot").Value.Text, "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("بصمة الوحدة غير صالحة.")
        End If
        If IsGroup(request.OperationCode) Then
            Dim required = GroupInputs(request.OperationCode), create = request.OperationCode = "Group.Created", id As Integer
            If request.EntityType <> "ProductGroup" OrElse required.Length <> names.Count OrElse required.Any(Function(x) Not names.Contains(x)) Then Throw New ArgumentException("طلب التصنيف غير مكتمل.")
            If Not {"GeneralMenu", "ItemsMenu", "IM_ADD_New"}.Contains(request.Context.ScreenCode) OrElse (Not create AndAlso request.Context.ScreenCode <> "GeneralMenu") Then Throw New ArgumentException("شاشة التصنيف غير معتمدة.")
            If If(create, request.EntityId <> "New", Not Integer.TryParse(request.EntityId, id) OrElse id <= 0 OrElse (request.OperationCode = "Group.Deleted" AndAlso id = 1)) Then Throw New ArgumentException("معرف تصنيف غير صالح أو محمي.")
            For Each input As AuditInput In request.Inputs
                Dim nullable = {"BackRed", "BackGreen", "BackBlue", "ForeRed", "ForeGreen", "ForeBlue", "RequestedRank"}.Contains(input.Name)
                If input.Value.State <> AuditValueState.Present AndAlso Not (nullable AndAlso input.Value.State = AuditValueState.SqlNull) Then Throw New ArgumentException("مدخل تصنيف غير ملتقط.")
            Next
            If Not create AndAlso Not Regex.IsMatch(request.Inputs.Single(Function(x) x.Name = "ExpectedGroupSnapshot").Value.Text, "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("بصمة التصنيف غير صالحة.")
        End If
        If IsItemUnit(request.OperationCode) Then
            Dim required = ItemUnitInputs(request.OperationCode), id As Integer
            If request.EntityType <> "CatalogItem" OrElse Not Integer.TryParse(request.EntityId, id) OrElse id <= 0 OrElse required.Length <> names.Count OrElse required.Any(Function(x) Not names.Contains(x)) Then Throw New ArgumentException("طلب رابط الوحدة غير مكتمل.")
            If Not ItemUnitScreens(request.OperationCode).Contains(request.Context.ScreenCode) Then Throw New ArgumentException("شاشة رابط الوحدة غير معتمدة.")
            For Each input As AuditInput In request.Inputs
                Dim nullable = {"UnitBarcode", "MinimumPrice", "MinimumPrice2"}.Contains(input.Name)
                If input.Value.State <> AuditValueState.Present AndAlso Not (nullable AndAlso input.Value.State = AuditValueState.SqlNull) Then Throw New ArgumentException("مدخل رابط الوحدة غير ملتقط.")
            Next
            Dim target = request.Inputs.Single(Function(x) x.Name = "TargetItemUnitId").Value.Text
            If Not Integer.TryParse(target, id) OrElse If(request.OperationCode = "ItemUnit.Created", id <> 0, id <= 0) Then Throw New ArgumentException("معرف رابط الوحدة غير صالح.")
            If Not Regex.IsMatch(request.Inputs.Single(Function(x) x.Name = "ExpectedItemUnitSnapshot").Value.Text, "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("بصمة روابط الصنف غير صالحة.")
        End If
        If IsItemMaster(request.OperationCode) Then
            Dim required = ItemMasterInputs(request.OperationCode), id As Integer
            If request.EntityType <> "CatalogItemRecord" OrElse Not Integer.TryParse(request.EntityId, id) OrElse id <= 0 OrElse required.Length <> names.Count OrElse required.Any(Function(x) Not names.Contains(x)) Then Throw New ArgumentException("طلب بطاقة الصنف غير مكتمل.")
            If request.Context.ScreenCode <> "IM_Fast_Mang" AndAlso Not (request.OperationCode = "Item.Deleted" AndAlso request.Context.ScreenCode = "ItemsMenu") Then Throw New ArgumentException("شاشة بطاقة الصنف غير معتمدة.")
            For Each itemInput As AuditInput In request.Inputs
                If itemInput.Value.State <> AuditValueState.Present AndAlso Not ({"ItemActive", "ItemNumber", "RequestedUnitPrice"}.Contains(itemInput.Name) AndAlso itemInput.Value.State = AuditValueState.SqlNull) Then Throw New ArgumentException("مدخل بطاقة الصنف غير ملتقط.")
            Next
            If Not Regex.IsMatch(request.Inputs.Single(Function(x) x.Name = "ExpectedItemSnapshot").Value.Text, "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("بصمة بطاقة الصنف غير صالحة.")
        End If
        If IsEmployeeValue(request.OperationCode) Then
            Dim create = request.OperationCode = "EmployeeValue.Created", id As Integer, required = EmployeeValueInputs(request.OperationCode)
            If request.EntityType <> "EmployeeValue" OrElse request.Context.ScreenCode <> "Emp_OtherValues" OrElse required.Length <> names.Count OrElse required.Any(Function(x) Not names.Contains(x)) OrElse request.Inputs.Any(Function(x) x.Value.State <> AuditValueState.Present) Then Throw New ArgumentException("طلب قيمة موظف غير مكتمل أو شاشة غير معتمدة.")
            If If(create, request.EntityId <> "New", Not Integer.TryParse(request.EntityId, id) OrElse id <= 0) Then Throw New ArgumentException("معرف قيمة الموظف غير صالح.")
            If Not create AndAlso Not Regex.IsMatch(request.Inputs.Single(Function(x) x.Name = "ExpectedEmployeeValueSnapshot").Value.Text, "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("بصمة قيمة الموظف غير صالحة.")
            If request.OperationCode <> "EmployeeValue.Voided" Then
                If Not Integer.TryParse(request.Inputs.Single(Function(x) x.Name = "EVEmployeeId").Value.Text, id) OrElse id <= 0 OrElse Not {"0", "1"}.Contains(request.Inputs.Single(Function(x) x.Name = "EVType").Value.Text) Then Throw New ArgumentException("الموظف أو نوع القيمة غير صالح.")
                Dim businessDate = DateTime.Parse(request.Inputs.Single(Function(x) x.Name = "EVBusinessDate").Value.Text, CultureInfo.InvariantCulture)
                If businessDate.Year <> Integer.Parse(request.Inputs.Single(Function(x) x.Name = "EVYear").Value.Text, CultureInfo.InvariantCulture) OrElse businessDate.Month <> Integer.Parse(request.Inputs.Single(Function(x) x.Name = "EVMonth").Value.Text, CultureInfo.InvariantCulture) Then Throw New ArgumentException("الشهر والسنة لا يتفقان مع تاريخ القيمة.")
            End If
        End If
        If request.OperationCode = "Purchase.LineAdded" Then
            Dim id As Integer, required = PurchaseInputs().Select(Function(x) x.Split("|"c)(0)).Concat({"DocumentType", "AdditionalExpiries", "ExpectedPurchaseSnapshot"}).ToArray()
            If request.EntityType <> "PurchaseDocument" OrElse Not Integer.TryParse(request.EntityId, id) OrElse id <= 0 OrElse names.Count <> required.Length OrElse required.Any(Function(x) Not names.Contains(x)) Then Throw New ArgumentException("طلب إضافة مشتريات/جرد غير مكتمل.")
            Dim value As Func(Of String, AuditValue) = Function(n) request.Inputs.Single(Function(x) x.Name = n).Value
            Dim kind = value("DocumentType").Text
            If value("DocumentType").State <> AuditValueState.Present OrElse Not {"7", "9"}.Contains(kind) OrElse request.Context.ScreenCode <> If(kind = "7", "Pch_IM_card_11", "Invoice_IM_card") Then Throw New ArgumentException("نوع الفاتورة أو الشاشة غير مطابق.")
            For Each definition In PurchaseInputs()
                Dim p = definition.Split("|"c), v = value(p(0))
                If v.State <> AuditValueState.Present AndAlso (p(4) = "R" OrElse v.State <> AuditValueState.SqlNull) Then Throw New ArgumentException("قيمة إضافة الصنف غير مكتملة.")
            Next
            For Each name In {"SelectedItemId", "SelectedUnitId", "SelectedStoreId"}
                If Not Integer.TryParse(value(name).Text, id) OrElse id <= 0 Then Throw New ArgumentException("مرجع الصنف/الوحدة/المخزن غير صالح.")
            Next
            If Decimal.Round(Decimal.Parse(value("RequestedQuantity").Text, CultureInfo.InvariantCulture), 2, MidpointRounding.AwayFromZero) <= 0D Then Throw New ArgumentException("الكمية يجب أن تكون موجبة بعد تقريب قاعدة البيانات.")
            If value("ExpectedPurchaseSnapshot").State <> AuditValueState.Present OrElse Not Regex.IsMatch(value("ExpectedPurchaseSnapshot").Text, "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("بصمة إضافة الصنف غير صالحة.")
            Text(value("RequestedBarcode").Text, 100, "RequestedBarcode")
            If value("RequestedExpiry").State = AuditValueState.Present Then DateTime.ParseExact(value("RequestedExpiry").Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
            If value("AdditionalExpiries").State <> AuditValueState.Present Then Throw New ArgumentException("قائمة الصلاحية مطلوبة ولو فارغة.")
            Dim dates = value("AdditionalExpiries").Text.Split(New Char() {";"c}, StringSplitOptions.RemoveEmptyEntries)
            If dates.Length > 100 Then Throw New ArgumentException("تواريخ الصلاحية تتجاوز سعة العملية.")
            For Each d In dates
                DateTime.ParseExact(d, "yyyy-MM-ddTHH:mm:ss.fffffff", CultureInfo.InvariantCulture)
            Next
        End If
        If request.OperationCode = "AccountEntry.Created" Then
            Dim required = {"AccountId", "DocumentType", "DocumentPure", "DocumentDate", "EntryTitle"}, id As Integer
            If request.EntityType <> "AccountEntry" OrElse request.EntityId <> "New" OrElse request.Context.ScreenCode <> "Add_Prev_Balance" OrElse names.Count <> required.Length OrElse required.Any(Function(x) Not names.Contains(x)) OrElse request.Inputs.Any(Function(x) x.Value.State <> AuditValueState.Present) Then Throw New ArgumentException("طلب حركة الحساب غير مكتمل.")
            If Not Integer.TryParse(request.Inputs.Single(Function(x) x.Name = "AccountId").Value.Text, id) OrElse id <= 0 OrElse Not {"5", "6", "37"}.Contains(request.Inputs.Single(Function(x) x.Name = "DocumentType").Value.Text) Then Throw New ArgumentException("الحساب أو نوع الحركة غير صالح.")
            If Decimal.Parse(request.Inputs.Single(Function(x) x.Name = "DocumentPure").Value.Text, CultureInfo.InvariantCulture) <= 0D Then Throw New ArgumentException("قيمة الحركة يجب أن تكون موجبة.")
            Text(request.Inputs.Single(Function(x) x.Name = "EntryTitle").Value.Text, 550, "EntryTitle")
        End If
        If IsExpenseLine(request.OperationCode) Then
            Dim id As Integer, required = ExpenseInputs(request.OperationCode), create = request.OperationCode = "Expense.LineAdded"
            If request.EntityType <> "ExpenseDocument" OrElse request.Context.ScreenCode <> "EXP_Details" OrElse Not Integer.TryParse(request.EntityId, id) OrElse id <= 0 Then Throw New ArgumentException("هوية فاتورة المصروف أو شاشتها غير صالحة.")
            If names.Count <> required.Length OrElse required.Any(Function(x) Not names.Contains(x)) OrElse request.Inputs.Any(Function(x) x.Value.State <> AuditValueState.Present) Then Throw New ArgumentException("مدخلات بند المصروف غير مكتملة.")
            If Not Integer.TryParse(request.Inputs.Single(Function(x) x.Name = "TargetLineId").Value.Text, id) OrElse If(create, id <> 0, id <= 0) Then Throw New ArgumentException("مرجع بند المصروف غير صالح.")
            If Not {"0", "1"}.Contains(request.Inputs.Single(Function(x) x.Name = "OnUpdate").Value.Text) OrElse Not Regex.IsMatch(request.Inputs.Single(Function(x) x.Name = "ExpectedExpenseSnapshot").Value.Text, "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("حالة تعديل أو بصمة المصروف غير صالحة.")
            If create Then
                If Not Integer.TryParse(request.Inputs.Single(Function(x) x.Name = "ExpenseId").Value.Text, id) OrElse id <= 0 Then Throw New ArgumentException("مرجع بطاقة المصروف غير صالح.")
                If Decimal.Parse(request.Inputs.Single(Function(x) x.Name = "ExpenseQuantity").Value.Text, CultureInfo.InvariantCulture) <= 0D Then Throw New ArgumentException("كمية المصروف يجب أن تكون موجبة.")
            End If
        End If
        If IsReturnLine(request.OperationCode) Then
            Dim id As Integer, kind As Integer, required = ReturnInputs(request.OperationCode), create = request.OperationCode = "Return.LineAdded"
            If request.EntityType <> "ReturnDocument" OrElse request.Context.ScreenCode <> "Returns" OrElse Not Integer.TryParse(request.EntityId, id) OrElse id <= 0 Then Throw New ArgumentException("هوية أو شاشة المرتجع غير صالحة.")
            If names.Count <> required.Length OrElse required.Any(Function(x) Not names.Contains(x)) Then Throw New ArgumentException("مدخلات المرتجع غير مكتملة.")
            If Not Integer.TryParse(request.Inputs.Single(Function(x) x.Name = "DocumentType").Value.Text, kind) Then Throw New ArgumentException("نوع المرتجع مطلوب.")
            ReturnSpec(kind)
            For Each input As AuditInput In request.Inputs
                If input.Value.State <> AuditValueState.Present AndAlso Not ((ReturnOptionalInputs().Contains(input.Name) OrElse input.Name = "RequestedExpiry") AndAlso input.Value.State = AuditValueState.SqlNull) Then Throw New ArgumentException("مدخل مرتجع غير ملتقط.")
            Next
            If Not Integer.TryParse(request.Inputs.Single(Function(x) x.Name = "TargetLineId").Value.Text, id) OrElse If(create, id <> 0, id <= 0) Then Throw New ArgumentException("سطر المرتجع غير صالح.")
            If Not {"0", "1"}.Contains(request.Inputs.Single(Function(x) x.Name = "OnUpdate").Value.Text) Then Throw New ArgumentException("حالة تعديل المرتجع غير صالحة.")
            If Not Regex.IsMatch(request.Inputs.Single(Function(x) x.Name = "ExpectedReturnSnapshot").Value.Text, "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("بصمة المرتجع غير صالحة.")
            If create Then
                For Each input As AuditInput In request.Inputs.Where(Function(x) {"SelectedItemId", "SelectedUnitId", "SelectedStoreId"}.Contains(x.Name))
                    If Not Integer.TryParse(input.Value.Text, id) OrElse id <= 0 Then Throw New ArgumentException("مرجع الصنف أو المخزن غير صالح.")
                Next
                Text(request.Inputs.Single(Function(x) x.Name = "RequestedBarcode").Value.Text, 50, "RequestedBarcode")
                Text(request.Inputs.Single(Function(x) x.Name = "RLSerial").Value.Text, 100, "RLSerial")
                Dim expiry = request.Inputs.Single(Function(x) x.Name = "RequestedExpiry").Value
                If expiry.State = AuditValueState.Present Then DateTime.ParseExact(expiry.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                If Double.Parse(request.Inputs.Single(Function(x) x.Name = "RequestedQuantity").Value.Text, CultureInfo.InvariantCulture) <= 0R Then Throw New ArgumentException("كمية المرتجع يجب أن تكون موجبة.")
            ElseIf request.OperationCode = "Return.LineQuantityChanged" AndAlso Not {"-1", "1"}.Contains(request.Inputs.Single(Function(x) x.Name = "QuantityDelta").Value.Text) Then
                Throw New ArgumentException("خطوة الكمية غير معتمدة.")
            End If
        End If
        If IsStockDocumentLine(request.OperationCode) Then
            Dim id As Integer, kind As Integer
            If request.EntityType <> "StockDocument" OrElse Not Integer.TryParse(request.EntityId, id) OrElse id <= 0 Then Throw New ArgumentException("مرجع الإذن غير صالح.")
            If Not names.Contains("DocumentType") OrElse Not Integer.TryParse(request.Inputs.Single(Function(x) x.Name = "DocumentType").Value.Text, kind) Then Throw New ArgumentException("نوع الإذن مطلوب.")
            Dim spec = StockDocumentSpec(kind), required = StockDocumentInputs(request.OperationCode, kind), create = request.OperationCode = "StockDocument.LineAdded"
            If names.Count <> required.Length OrElse required.Any(Function(x) Not names.Contains(x)) Then Throw New ArgumentException("مدخلات الإذن غير مكتملة.")
            If Not spec(If(create, 5, 6)).Split(","c).Contains(request.Context.ScreenCode) OrElse (request.OperationCode = "StockDocument.LineQuantityChanged" AndAlso request.Context.ScreenCode <> "Outside_Sales_Touch") Then Throw New ArgumentException("شاشة الإذن غير معتمدة.")
            For Each input As AuditInput In request.Inputs
                If input.Value.State <> AuditValueState.Present AndAlso Not (input.Name = "RequestedExpiry" AndAlso kind <> 12 AndAlso input.Value.State = AuditValueState.SqlNull) Then Throw New ArgumentException("مدخل الإذن غير ملتقط.")
            Next
            If Not Integer.TryParse(request.Inputs.Single(Function(x) x.Name = "TargetLineId").Value.Text, id) OrElse If(create, id <> 0, id <= 0) Then Throw New ArgumentException("سطر الإذن غير صالح.")
            If Not Regex.IsMatch(request.Inputs.Single(Function(x) x.Name = "ExpectedStockDocumentSnapshot").Value.Text, "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("بصمة الإذن غير صالحة.")
            If create Then
                For Each input As AuditInput In request.Inputs.Where(Function(x) {"SelectedItemId", "SelectedUnitId", "SelectedStoreId", "DestinationStoreId"}.Contains(x.Name))
                    If Not Integer.TryParse(input.Value.Text, id) OrElse id <= 0 Then Throw New ArgumentException("مرجع الصنف أو المخزن غير صالح.")
                Next
                If Not {"0", "1"}.Contains(request.Inputs.Single(Function(x) x.Name = "OnUpdate").Value.Text) Then Throw New ArgumentException("حالة تعديل الإذن غير صالحة.")
                Text(request.Inputs.Single(Function(x) x.Name = "RequestedBarcode").Value.Text, 50, "RequestedBarcode")
                Text(request.Inputs.Single(Function(x) x.Name = "RequestedExpiry").Value.Text, 50, "RequestedExpiry")
                If Decimal.Parse(request.Inputs.Single(Function(x) x.Name = "RequestedQuantity").Value.Text, CultureInfo.InvariantCulture) <= 0D Then Throw New ArgumentException("كمية الإذن يجب أن تكون موجبة.")
            ElseIf request.OperationCode = "StockDocument.LineQuantityChanged" AndAlso Not {"-1", "1"}.Contains(request.Inputs.Single(Function(x) x.Name = "QuantityDelta").Value.Text) Then
                Throw New ArgumentException("خطوة الكمية غير معتمدة.")
            End If
        End If
        If IsFinance(request.OperationCode) Then
            Dim create = request.OperationCode.EndsWith(".Created", StringComparison.Ordinal), id As Integer, required = FinanceInputs(request.OperationCode)
            If request.EntityType <> FinanceEntity(request.OperationCode) OrElse request.Context.ScreenCode <> FinanceScreen(request.OperationCode) OrElse required.Length <> names.Count OrElse required.Any(Function(x) Not names.Contains(x)) OrElse request.Inputs.Any(Function(x) x.Value.State <> AuditValueState.Present) Then Throw New ArgumentException("طلب مالي غير مكتمل أو شاشة غير معتمدة.")
            If If(create, request.EntityId <> "New", Not Integer.TryParse(request.EntityId, id) OrElse id <= 0) Then Throw New ArgumentException("معرف الحركة المالية غير صالح.")
            If create Then
                For Each financeInput As AuditInput In request.Inputs.Where(Function(x) {"FromId", "ToId", "TreasuryId"}.Contains(x.Name))
                    If Not Integer.TryParse(financeInput.Value.Text, id) OrElse id <= 0 Then Throw New ArgumentException("حساب الحركة غير صالح.")
                Next
                If Decimal.Parse(request.Inputs.Single(Function(x) x.Name = "RequestedAmount").Value.Text, CultureInfo.InvariantCulture) = 0D Then Throw New ArgumentException("القيمة المدخلة صفر.")
                If request.OperationCode = "TreasuryMovement.Created" Then
                    If Not {"1", "2"}.Contains(request.Inputs.Single(Function(x) x.Name = "MovementType").Value.Text) OrElse Not {"0", "1"}.Contains(request.Inputs.Single(Function(x) x.Name = "OpeningFlag").Value.Text) Then Throw New ArgumentException("نوع حركة الخزينة غير صالح.")
                ElseIf request.Inputs.Single(Function(x) x.Name = "FromId").Value.Text = request.Inputs.Single(Function(x) x.Name = "ToId").Value.Text Then
                    Throw New ArgumentException("لا يمكن التحويل إلى الحساب نفسه.")
                End If
                Dim title = request.Inputs.SingleOrDefault(Function(x) x.Name = "Title")
                If title IsNot Nothing AndAlso title.Value.Text.Length > 2500 Then Throw New ArgumentException("عنوان التحويل أطول من الحقل.")
            ElseIf Not Regex.IsMatch(request.Inputs(0).Value.Text, "\A[A-F0-9]{64}\z") Then
                Throw New ArgumentException("بصمة الحركة المالية غير صالحة.")
            End If
        End If
        If IsTableOperation(request.OperationCode) Then
            Dim transfer = request.OperationCode = "Table.Transferred", id As Integer, required = TableInputs(request.OperationCode)
            If request.EntityType <> If(transfer, "TableState", "TableInvoice") OrElse Not Integer.TryParse(request.EntityId, id) OrElse id <= 0 OrElse required.Length <> names.Count OrElse required.Any(Function(x) Not names.Contains(x)) OrElse request.Inputs.Any(Function(x) x.Value.State <> AuditValueState.Present) Then Throw New ArgumentException("طلب الطاولة غير مكتمل.")
            If request.Context.ScreenCode <> If(transfer, "TablesBalance", "TB_BillIM") Then Throw New ArgumentException("شاشة الطاولة غير معتمدة.")
            If Not Regex.IsMatch(request.Inputs.Single(Function(x) x.Name = "ExpectedTableSnapshot").Value.Text, "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("بصمة الطاولة غير صالحة.")
            Dim target = request.Inputs.Single(Function(x) x.Name = If(transfer, "DestinationTableId", "TargetLineId")).Value.Text
            If Not Integer.TryParse(target, id) OrElse id <= 0 OrElse (transfer AndAlso target = request.EntityId) Then Throw New ArgumentException("هدف الطاولة غير صالح.")
            If request.OperationCode = "Table.LineQuantityChanged" AndAlso Not {"-1", "1"}.Contains(request.Inputs.Single(Function(x) x.Name = "QuantityDelta").Value.Text) Then Throw New ArgumentException("تغيير الكمية يجب أن يكون وحدة واحدة.")
        End If
        If IsPricingRule(request.OperationCode) Then
            Dim offer = request.OperationCode.StartsWith("Offer.", StringComparison.Ordinal), id As Integer
            Dim required = PricingInputs(request.OperationCode)
            If request.EntityType <> If(offer, "ItemOffers", "PriceRuleGroup") OrElse Not Integer.TryParse(request.EntityId, id) OrElse id <= 0 OrElse required.Length <> names.Count OrElse required.Any(Function(x) Not names.Contains(x)) OrElse request.Inputs.Any(Function(x) x.Value.State <> AuditValueState.Present) Then Throw New ArgumentException("طلب قاعدة التسعير غير مكتمل.")
            If request.Context.ScreenCode <> If(offer, "OFFERS", "IM_Change_Price_ByPercent") Then Throw New ArgumentException("شاشة قاعدة التسعير غير معتمدة.")
            If Not Regex.IsMatch(request.Inputs.Single(Function(x) x.Name = "ExpectedPricingSnapshot").Value.Text, "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("بصمة قاعدة التسعير غير صالحة.")
            If request.OperationCode = "PriceRule.Deleted" AndAlso (Not Integer.TryParse(request.Inputs.Single(Function(x) x.Name = "TargetPriceRuleId").Value.Text, id) OrElse id <= 0) Then Throw New ArgumentException("معرف قاعدة التسعير غير صالح.")
        End If
        If request.OperationCode = "Document.Voided" Then
            If request.EntityType <> "BusinessDocument" OrElse names.Count <> 2 OrElse Not names.Contains("DocumentType") OrElse Not names.Contains("ExpectedGraphSnapshot") OrElse request.Inputs.Any(Function(x) x.Value.State <> AuditValueState.Present) Then Throw New ArgumentException("عقد الإلغاء غير مكتمل.")
            Dim kind = Integer.Parse(request.Inputs.Single(Function(x) x.Name = "DocumentType").Value.Text, CultureInfo.InvariantCulture)
            If Not VoidSpec(kind)(2).Split(","c).Contains(request.Context.ScreenCode) Then Throw New ArgumentException("شاشة الإلغاء لا تطابق نوع المستند.")
        End If
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
            Dim quantityDelta As Integer
            If Not Integer.TryParse(request.Inputs.Single(Function(x) x.Name = "QuantityDelta").Value.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, quantityDelta) OrElse quantityDelta = 0 Then Throw New ArgumentException("فرق الكمية يجب أن يكون عددًا صحيحًا غير صفري.")
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
        If IsPeopleCard(item.EventCode) Then ValidatePeopleEvent(request, item)
        If IsTableSplit(item.EventCode) Then ValidateTableSplitEvent(request, item)
        If IsReceipt(item.EventCode) Then ValidateReceiptEvent(request, item)
        If item.EventCode = "Salary.BatchPosted" Then ValidateSalaryEvent(request, item)
        If IsItemLifecycle(item.EventCode) Then ValidateItemLifecycleEvent(request, item)
        If IsWorkflow(item.EventCode) Then ValidateWorkflowEvent(request, item)
        If IsInventoryOperation(item.EventCode) Then ValidateInventoryEvent(request, item)
        If item.EventCode = "Document.EditOpened" Then
            Dim expected = {"DocumentType", "DocumentNumber", "Approved", "Voided", "NeedRepost", "JournalId", "OriginalJournalId", "EditOpenedAt", "EditOpenedBy", "DocumentTotal", "DocumentPure", "Discount"}
            If item.Changes.Count <> expected.Length OrElse item.Changes.Any(Function(x) Not expected.Contains(x.FieldCode, StringComparer.Ordinal)) Then Throw New ArgumentException("يلزم التقاط حقول فتح التعديل الاثني عشر كاملة.")
            Dim kind = item.Changes.Single(Function(x) x.FieldCode = "DocumentType")
            If kind.Before.State <> AuditValueState.Present OrElse kind.After.State <> AuditValueState.Present OrElse kind.Before.Text <> request.Inputs(0).Value.Text OrElse kind.After.Text <> request.Inputs(0).Value.Text Then Throw New ArgumentException("نوع المستند الفعلي لا يطابق الطلب.")
        End If
        If IsEmployeeValue(item.EventCode) Then
            Dim id As Integer, create = item.EventCode = "EmployeeValue.Created", fields = EmployeeValueFields()
            If item.EntityType <> request.EntityType OrElse Not Integer.TryParse(item.EntityId, id) OrElse id <= 0 OrElse (Not create AndAlso item.EntityId <> request.EntityId) OrElse item.DocumentNumber <> item.EntityId OrElse item.EventOrdinal <> 1 OrElse item.RelatedEntities.Count <> 0 OrElse item.Changes.Count <> fields.Length Then Throw New ArgumentException("حدث قيمة موظف غير مكتمل.")
            For Each change In item.Changes
                If change.EntityType <> item.EntityType OrElse change.EntityId <> item.EntityId OrElse change.ValueOrigin <> 1 OrElse Not fields.Any(Function(x) x.Split("|"c)(0) = change.FieldCode AndAlso x.Split("|"c)(3) = change.SourceSqlType) Then Throw New ArgumentException("مصدر قيمة الموظف غير معتمد.")
                If If(create, change.Before.State <> AuditValueState.Absent, change.Before.State < AuditValueState.SqlNull) OrElse change.After.State < AuditValueState.SqlNull Then Throw New ArgumentException("حالة قيمة الموظف غير مكتملة.")
            Next
        End If

        If IsDocumentField(item.EventCode) Then
            AuditContract.Request(request)
            Dim fields = DocumentHeaderFields()
            If item.EntityType <> request.EntityType OrElse item.EntityId <> request.EntityId OrElse item.EventOrdinal <> 1 OrElse item.RelatedEntities.Count <> 0 OrElse String.IsNullOrWhiteSpace(item.DocumentNumber) OrElse item.Changes.Count <> fields.Length Then Throw New ArgumentException("حدث رأس الفاتورة غير مكتمل.")
            For Each c In item.Changes
                If c.EntityType <> item.EntityType OrElse c.EntityId <> item.EntityId OrElse c.ValueOrigin <> 1 OrElse c.Before.State < AuditValueState.SqlNull OrElse c.After.State < AuditValueState.SqlNull OrElse Not fields.Any(Function(x) x.Split("|"c)(0) = c.FieldCode AndAlso x.Split("|"c)(3) = c.SourceSqlType) Then Throw New ArgumentException("مصدر/حالة حقل رأس الفاتورة غير صالح.")
                Dim allowed = DocumentFieldAllowed(request, c.FieldCode)
                If Not allowed AndAlso (c.Before.State <> c.After.State OrElse c.Before.Text <> c.After.Text) Then Throw New ArgumentException("تغيير خارج نطاق العملية المباشرة.")
            Next
            Dim kind = request.Inputs.Single(Function(x) x.Name = "DocumentType").Value.Text
            If item.Changes.Single(Function(x) x.FieldCode = "DocumentType").After.Text <> kind OrElse item.Changes.Single(Function(x) x.FieldCode = "DocumentNumber").After.Text <> item.DocumentNumber Then Throw New ArgumentException("رقم/نوع الفاتورة غير مطابق.")
            Dim field = If(item.EventCode = "Document.AccountChanged", "AccountId", "Discount")
            Dim actual = item.Changes.Single(Function(x) x.FieldCode = field).After
            Dim requested = request.Inputs.Single(Function(x) x.Name = field).Value.Text
            If actual.State <> AuditValueState.Present OrElse If(field = "AccountId", actual.Text <> requested, Decimal.Parse(actual.Text, CultureInfo.InvariantCulture) <> Decimal.Round(Decimal.Parse(requested, CultureInfo.InvariantCulture), 3, MidpointRounding.AwayFromZero)) Then Throw New ArgumentException("النتيجة لا تطابق الطلب.")
            If DocumentFieldAllowed(request, "SecondaryDebit") Then
                Dim percent = item.Changes.Single(Function(x) x.FieldCode = "SecondaryDebit").After
                If percent.State <> AuditValueState.Present OrElse Decimal.Parse(percent.Text, CultureInfo.InvariantCulture) <> Decimal.Round(Decimal.Parse(request.Inputs.Single(Function(x) x.Name = "DiscountPercent").Value.Text, CultureInfo.InvariantCulture), 3, MidpointRounding.AwayFromZero) Then Throw New ArgumentException("نسبة الخصم المحفوظة لا تطابق الطلب.")
            End If
        End If

        If IsQuoteLine(item.EventCode) Then
            AuditContract.Request(request)
            If item.EntityType <> request.EntityType OrElse item.EntityId <> request.EntityId OrElse item.EventOrdinal <> 1 OrElse item.RelatedEntities.Count <> 0 OrElse String.IsNullOrWhiteSpace(item.DocumentNumber) OrElse item.Changes.Count <> QuoteFields("QuoteDocument").Length + QuoteFields("QuoteLine").Length Then Throw New ArgumentException("حدث صنف العرض غير مكتمل.")
            Dim create = item.EventCode = "Quote.LineAdded", delete = item.EventCode = "Quote.LineDeleted", groups = item.Changes.GroupBy(Function(x) x.EntityType & "|" & x.EntityId).ToArray()
            If groups.Length <> 2 Then Throw New ArgumentException("يجب تسجيل رأس العرض والسطر فقط.")
            For Each g In groups
                Dim first = g.First(), fields = QuoteFields(first.EntityType), line = first.EntityType = "QuoteLine", id As Integer
                If Not Integer.TryParse(first.EntityId, id) OrElse id <= 0 OrElse g.Count() <> fields.Length OrElse (Not line AndAlso first.EntityId <> request.EntityId) OrElse (line AndAlso Not create AndAlso first.EntityId <> request.Inputs.Single(Function(x) x.Name = "TargetLineId").Value.Text) Then Throw New ArgumentException("هوية/حقول سطر العرض لا تطابق الطلب.")
                For Each c In g
                    If c.ValueOrigin <> 1 OrElse Not fields.Any(Function(x) x.Split("|"c)(0) = c.FieldCode AndAlso x.Split("|"c)(3) = c.SourceSqlType) OrElse If(line AndAlso create, c.Before.State <> AuditValueState.Absent, c.Before.State < AuditValueState.SqlNull) OrElse If(line AndAlso delete, c.After.State <> AuditValueState.Absent, c.After.State < AuditValueState.SqlNull) Then Throw New ArgumentException("مصدر أو حالة حقل عرض السعر غير صالحة.")
                Next
            Next
            If item.Changes.Single(Function(x) x.FieldCode = "DocumentType").After.Text <> "16" OrElse item.Changes.Single(Function(x) x.FieldCode = "DocumentNumber").After.Text <> item.DocumentNumber Then Throw New ArgumentException("رقم/نوع العرض لا يطابق الحدث.")
            Dim owner = item.Changes.Single(Function(x) x.FieldCode = "QuoteOwnerId")
            If If(delete, owner.Before.Text, owner.After.Text) <> request.EntityId Then Throw New ArgumentException("السطر لا يتبع العرض.")
        End If

        If item.EventCode = "Purchase.LineAdded" Then
            AuditContract.Request(request)
            If item.EntityType <> request.EntityType OrElse item.EntityId <> request.EntityId OrElse item.RelatedEntities.Count <> 0 OrElse String.IsNullOrWhiteSpace(item.DocumentNumber) Then Throw New ArgumentException("هوية حدث إضافة الصنف غير مطابقة.")
            For Each group In item.Changes.GroupBy(Function(x) x.EntityType & ChrW(0) & x.EntityId)
                Dim first = group.First(), fields = PurchaseFields(first.EntityType), id As Long
                If group.Count() <> fields.Length Then Throw New ArgumentException("صف مراقبة إضافة الصنف غير كامل.")
                If first.EntityType = "StockBucket" Then
                    If Not Regex.IsMatch(first.EntityId, "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("مرجع رصيد غير صالح.")
                ElseIf Not Long.TryParse(first.EntityId, id) OrElse id <= 0 Then
                    Throw New ArgumentException("مرجع صف إضافة الصنف غير صالح.")
                End If
                For Each c In group
                    If c.ValueOrigin <> If(c.EntityType = "StockBucket", 3, 1) OrElse Not fields.Any(Function(x) x.Split("|"c)(0) = c.FieldCode AndAlso x.Split("|"c)(3) = c.SourceSqlType) OrElse c.After.State < AuditValueState.SqlNull Then Throw New ArgumentException("مصدر أو حالة حقل إضافة الصنف غير صالح.")
                    If c.EntityType = "PurchaseExpiry" OrElse (item.EventOrdinal = 1 AndAlso c.EntityType = "PurchaseLine") Then
                        If c.Before.State <> AuditValueState.Absent Then Throw New ArgumentException("يلزم صف جديد لم يكن موجودًا.")
                    ElseIf c.Before.State < AuditValueState.SqlNull Then
                        Throw New ArgumentException("يلزم التقاط القيمة السابقة.")
                    End If
                Next
                If first.EntityType = "PurchaseDocument" AndAlso (first.EntityId <> request.EntityId OrElse item.EventOrdinal <> 1) Then Throw New ArgumentException("رأس فاتورة غير مرتبط.")
                If first.EntityType = "PurchaseLine" AndAlso group.Single(Function(x) x.FieldCode = "PurchaseDocumentId").After.Text <> request.EntityId Then Throw New ArgumentException("سطر غير تابع للفاتورة.")
                If first.EntityType = "PurchaseItem" AndAlso first.EntityId <> request.Inputs.Single(Function(x) x.Name = "SelectedItemId").Value.Text Then Throw New ArgumentException("صنف غير مرتبط.")
                If first.EntityType = "PurchaseUnit" AndAlso group.Single(Function(x) x.FieldCode = "PriceItemId").After.Text <> request.Inputs.Single(Function(x) x.Name = "SelectedItemId").Value.Text Then Throw New ArgumentException("وحدة صنف غير مرتبطة.")
                If first.EntityType = "PurchaseExpiry" AndAlso group.Single(Function(x) x.FieldCode = "ExpiryItemId").After.Text <> request.Inputs.Single(Function(x) x.Name = "SelectedItemId").Value.Text Then Throw New ArgumentException("صلاحية صنف غير مرتبطة.")
            Next
            If item.EventOrdinal = 1 Then
                If item.Changes.Count <> 51 OrElse item.Changes.Where(Function(x) x.EntityType = "PurchaseDocument").Count() <> 23 OrElse item.Changes.Where(Function(x) x.EntityType = "PurchaseLine").Count() <> 28 Then Throw New ArgumentException("يلزم الرأس والسطر الجديد كاملين.")
                If item.Changes.Single(Function(x) x.FieldCode = "DocumentType").After.Text <> request.Inputs.Single(Function(x) x.Name = "DocumentType").Value.Text OrElse item.Changes.Single(Function(x) x.FieldCode = "DocumentNumber").After.Text <> item.DocumentNumber Then Throw New ArgumentException("نوع/رقم الفاتورة غير مطابق.")
            ElseIf item.Changes.Any(Function(x) x.EntityType = "PurchaseDocument") Then
                Throw New ArgumentException("رأس الفاتورة محصور بالحدث الأول.")
            End If
        End If
        If item.EventCode = "AccountEntry.Created" Then
            Dim id As Integer, fields = AccountEntryFields()
            If item.EntityType <> "AccountEntry" OrElse Not Integer.TryParse(item.EntityId, id) OrElse id <= 0 OrElse item.DocumentNumber <> item.EntityId OrElse item.EventOrdinal <> 1 OrElse item.RelatedEntities.Count <> 0 OrElse item.Changes.Count <> fields.Length Then Throw New ArgumentException("حدث حركة الحساب غير مكتمل.")
            For Each change In item.Changes
                If change.EntityType <> item.EntityType OrElse change.EntityId <> item.EntityId OrElse change.ValueOrigin <> 1 OrElse Not fields.Any(Function(x) x.Split("|"c)(0) = change.FieldCode AndAlso x.Split("|"c)(3) = change.SourceSqlType) OrElse change.Before.State <> AuditValueState.Absent OrElse change.After.State < AuditValueState.SqlNull Then Throw New ArgumentException("حقل حركة حساب غير مطابق.")
            Next
            For Each name In {"AccountId", "DocumentType", "EntryTitle"}
                If item.Changes.Single(Function(x) x.FieldCode = name).After.Text <> request.Inputs.Single(Function(x) x.Name = name).Value.Text Then Throw New ArgumentException("الحركة المحفوظة لا تطابق الطلب.")
            Next
        End If
        If IsExpenseLine(item.EventCode) Then
            If item.EntityType <> request.EntityType OrElse item.EntityId <> request.EntityId OrElse item.EventOrdinal <> 1 OrElse item.RelatedEntities.Count <> 0 OrElse String.IsNullOrWhiteSpace(item.DocumentNumber) OrElse item.Changes.Count <> 30 Then Throw New ArgumentException("حدث بند المصروف غير مكتمل.")
            Dim groups = item.Changes.GroupBy(Function(x) x.EntityType & ChrW(0) & x.EntityId).ToArray(), key As Integer
            If groups.Length <> 2 OrElse groups.Count(Function(x) x.First().EntityType = "ExpenseDocument") <> 1 OrElse groups.Count(Function(x) x.First().EntityType = "ExpenseLine") <> 1 Then Throw New ArgumentException("يلزم رأس المصروف وبند واحد.")
            For Each group In groups
                Dim first = group.First(), fields = ExpenseFields(first.EntityType), line = first.EntityType = "ExpenseLine"
                If Not Integer.TryParse(first.EntityId, key) OrElse key <= 0 OrElse group.Count() <> fields.Length OrElse (Not line AndAlso first.EntityId <> request.EntityId) Then Throw New ArgumentException("مرجع صف المصروف غير مطابق.")
                For Each change In group
                    If change.ValueOrigin <> 1 OrElse Not fields.Any(Function(x) x.Split("|"c)(0) = change.FieldCode AndAlso x.Split("|"c)(3) = change.SourceSqlType) Then Throw New ArgumentException("مصدر حقل المصروف غير معتمد.")
                    If line Then
                        If If(item.EventCode = "Expense.LineAdded", change.Before.State <> AuditValueState.Absent OrElse change.After.State < AuditValueState.SqlNull, change.Before.State < AuditValueState.SqlNull OrElse change.After.State <> AuditValueState.Absent) Then Throw New ArgumentException("حالة بند المصروف غير مطابقة.")
                    ElseIf change.Before.State < AuditValueState.SqlNull OrElse change.After.State < AuditValueState.SqlNull Then
                        Throw New ArgumentException("يلزم التقاط رأس المصروف قبل وبعد.")
                    End If
                Next
            Next
            Dim parent = item.Changes.Single(Function(x) x.FieldCode = "ExpenseDocumentId")
            If If(item.EventCode = "Expense.LineDeleted", parent.Before.Text, parent.After.Text) <> request.EntityId Then Throw New ArgumentException("البند لا يتبع المصروف.")
        End If
        If IsReturnLine(item.EventCode) Then
            Dim kind = Integer.Parse(request.Inputs.Single(Function(x) x.Name = "DocumentType").Value.Text, CultureInfo.InvariantCulture), lineEntity = ReturnSpec(kind)(2), key As Integer
            If item.EntityType <> request.EntityType OrElse item.EntityId <> request.EntityId OrElse item.RelatedEntities.Count <> 0 OrElse String.IsNullOrWhiteSpace(item.DocumentNumber) Then Throw New ArgumentException("مرجع حدث المرتجع غير مطابق.")
            For Each group In item.Changes.GroupBy(Function(x) x.EntityType & ChrW(0) & x.EntityId)
                Dim first = group.First(), fields = ReturnFields(first.EntityType)
                If Not {"ReturnDocument", lineEntity, "StockBucket"}.Contains(first.EntityType) OrElse group.Count() <> fields.Length Then Throw New ArgumentException("صف مراقبة مرتجع غير مكتمل.")
                If first.EntityType = "ReturnDocument" AndAlso first.EntityId <> request.EntityId Then Throw New ArgumentException("رأس مرتجع آخر.")
                If first.EntityType <> "StockBucket" AndAlso (Not Integer.TryParse(first.EntityId, key) OrElse key <= 0) Then Throw New ArgumentException("مرجع صف غير صالح.")
                For Each change In group
                    If change.ValueOrigin <> If(first.EntityType = "StockBucket", 3, 1) OrElse Not fields.Any(Function(x) x.Split("|"c)(0) = change.FieldCode AndAlso x.Split("|"c)(3) = change.SourceSqlType) Then Throw New ArgumentException("مصدر حقل مرتجع غير مطابق.")
                    If first.EntityType = lineEntity Then
                        If If(item.EventCode = "Return.LineAdded", change.Before.State <> AuditValueState.Absent OrElse change.After.State < AuditValueState.SqlNull, If(item.EventCode = "Return.LineDeleted", change.Before.State < AuditValueState.SqlNull OrElse change.After.State <> AuditValueState.Absent, change.Before.State < AuditValueState.SqlNull OrElse change.After.State < AuditValueState.SqlNull)) Then Throw New ArgumentException("حالة سطر مرتجع غير مطابقة.")
                    ElseIf change.Before.State < AuditValueState.SqlNull OrElse change.After.State < AuditValueState.SqlNull Then
                        Throw New ArgumentException("يلزم التقاط رأس المرتجع والرصيد قبل وبعد.")
                    End If
                Next
            Next
            If item.EventOrdinal = 1 AndAlso (item.Changes.Count <> ReturnFields("ReturnDocument").Length + ReturnFields(lineEntity).Length OrElse item.Changes.Any(Function(x) x.EntityType = "StockBucket")) Then Throw New ArgumentException("يلزم رأس المرتجع والسطر كاملين في الحدث الأول.")
            If item.EventOrdinal > 1 AndAlso item.Changes.Any(Function(x) x.EntityType <> "StockBucket") Then Throw New ArgumentException("الأجزاء الإضافية لأرصدة المرتجع فقط.")
        End If
        If IsStockDocumentLine(item.EventCode) Then
            Dim kind = Integer.Parse(request.Inputs.Single(Function(x) x.Name = "DocumentType").Value.Text, CultureInfo.InvariantCulture), lineEntity = StockDocumentSpec(kind)(2), key As Integer
            If item.EntityType <> request.EntityType OrElse item.EntityId <> request.EntityId OrElse item.RelatedEntities.Count <> 0 OrElse String.IsNullOrWhiteSpace(item.DocumentNumber) Then Throw New ArgumentException("مرجع حدث الإذن غير مطابق.")
            For Each group In item.Changes.GroupBy(Function(x) x.EntityType & ChrW(0) & x.EntityId)
                Dim first = group.First(), fields = StockDocumentFields(first.EntityType)
                If Not {"StockDocument", lineEntity, "StockBucket"}.Contains(first.EntityType) OrElse group.Count() <> fields.Length Then Throw New ArgumentException("صف مراقبة إذن غير مكتمل.")
                If first.EntityType = "StockDocument" AndAlso first.EntityId <> request.EntityId Then Throw New ArgumentException("رأس إذن آخر.")
                If first.EntityType <> "StockBucket" AndAlso (Not Integer.TryParse(first.EntityId, key) OrElse key <= 0) Then Throw New ArgumentException("مرجع صف غير صالح.")
                For Each change In group
                    If change.ValueOrigin <> If(first.EntityType = "StockBucket", 3, 1) OrElse Not fields.Any(Function(x) x.Split("|"c)(0) = change.FieldCode AndAlso x.Split("|"c)(3) = change.SourceSqlType) Then Throw New ArgumentException("مصدر حقل إذن غير مطابق.")
                    If first.EntityType = lineEntity Then
                        If If(item.EventCode = "StockDocument.LineAdded", change.Before.State <> AuditValueState.Absent OrElse change.After.State < AuditValueState.SqlNull, If(item.EventCode = "StockDocument.LineDeleted", change.Before.State < AuditValueState.SqlNull OrElse change.After.State <> AuditValueState.Absent, change.Before.State < AuditValueState.SqlNull OrElse change.After.State < AuditValueState.SqlNull)) Then Throw New ArgumentException("حالة سطر إذن غير مطابقة.")
                    ElseIf change.Before.State < AuditValueState.SqlNull OrElse change.After.State < AuditValueState.SqlNull Then
                        Throw New ArgumentException("يلزم التقاط رأس الإذن والرصيد قبل وبعد.")
                    End If
                Next
            Next
            If item.EventOrdinal = 1 AndAlso (item.Changes.Count <> StockDocumentFields("StockDocument").Length + StockDocumentFields(lineEntity).Length OrElse item.Changes.Any(Function(x) x.EntityType = "StockBucket")) Then Throw New ArgumentException("يلزم رأس الإذن والسطر كاملين في الحدث الأول.")
            If item.EventOrdinal > 1 AndAlso item.Changes.Any(Function(x) x.EntityType <> "StockBucket") Then Throw New ArgumentException("الأجزاء الإضافية لأرصدة الإذن فقط.")
        End If
        If IsFinance(item.EventCode) Then
            Dim create = item.EventCode.EndsWith(".Created", StringComparison.Ordinal), account = item.EventCode.StartsWith("AccountTransfer.", StringComparison.Ordinal), key As Integer
            Dim entityName = If(account, "FinanceAccountRow", "FinanceTreasuryRow"), fields = FinanceFields(entityName)
            If item.EntityType <> request.EntityType OrElse Not Integer.TryParse(item.EntityId, key) OrElse key <= 0 OrElse (Not create AndAlso item.EntityId <> request.EntityId) OrElse item.DocumentNumber <> item.EntityId OrElse item.RelatedEntities.Count <> 0 Then Throw New ArgumentException("مرجع الحركة المالية غير صالح.")
            If account AndAlso (item.EventOrdinal <> 1 OrElse item.Changes.Count <> fields.Length) Then Throw New ArgumentException("تحويل الحسابات يتطلب صفًا كاملًا واحدًا.")
            If item.EventCode = "TreasuryTransfer.Created" AndAlso (item.EventOrdinal <> 1 OrElse item.Changes.Count <> fields.Length * 2) Then Throw New ArgumentException("التحويل يتطلب الطرفين.")
            If item.EventCode = "TreasuryMovement.Created" AndAlso (item.EventOrdinal <> 1 OrElse item.Changes.Count <> fields.Length) Then Throw New ArgumentException("حركة الخزينة تتطلب صفًا واحدًا.")
            For Each group In item.Changes.GroupBy(Function(x) x.EntityId)
                If group.Count() <> fields.Length OrElse Not Integer.TryParse(group.Key, key) OrElse key <= 0 Then Throw New ArgumentException("صف مالي غير مكتمل.")
                If (account OrElse item.EventCode = "TreasuryMovement.Created") AndAlso group.Key <> item.EntityId Then Throw New ArgumentException("صف مالي خارج الطلب.")
                For Each change In group
                    If change.EntityType <> entityName OrElse change.ValueOrigin <> 1 OrElse Not fields.Any(Function(x) x.Split("|"c)(0) = change.FieldCode AndAlso x.Split("|"c)(3) = change.SourceSqlType) Then Throw New ArgumentException("مصدر حقل مالي غير معتمد.")
                    If If(create, change.Before.State <> AuditValueState.Absent, change.Before.State < AuditValueState.SqlNull) OrElse change.After.State < AuditValueState.SqlNull Then Throw New ArgumentException("حالة مالية غير صحيحة.")
                    If item.EventCode.StartsWith("TreasuryTransfer.", StringComparison.Ordinal) AndAlso change.FieldCode = "CashTranID" AndAlso (change.After.Text <> item.EntityId OrElse (Not create AndAlso change.Before.Text <> item.EntityId)) Then Throw New ArgumentException("طرف يتبع تحويلاً آخر.")
                Next
            Next
        End If
        If IsTableOperation(item.EventCode) Then
            Dim transfer = item.EventCode = "Table.Transferred", key As Integer
            If item.EntityType <> request.EntityType OrElse item.EntityId <> request.EntityId OrElse item.RelatedEntities.Count <> 0 Then Throw New ArgumentException("مرجع الطاولة لا يطابق الطلب.")
            If transfer AndAlso item.DocumentNumber IsNot Nothing Then Throw New ArgumentException("نقل الطاولة قد يشمل عدة فواتير ولا يحمل رقم فاتورة مفردًا.")
            If item.EventOrdinal = 1 Then
                If transfer Then
                    If item.Changes.Count <> 18 OrElse item.Changes.Any(Function(x) x.EntityType <> "TableState") Then Throw New ArgumentException("يلزم التقاط الطاولتين كاملتين.")
                ElseIf item.Changes.Count <> 39 OrElse item.Changes.Where(Function(x) x.EntityType = "TableInvoice").Count() <> 15 OrElse item.Changes.Where(Function(x) x.EntityType = "TableLine").Count() <> 24 Then
                    Throw New ArgumentException("يلزم رأس فاتورة الطاولة وسطرها كاملين.")
                End If
            End If
            For Each group In item.Changes.GroupBy(Function(x) x.EntityType & ChrW(0) & x.EntityId)
                Dim first = group.First(), fields = TableFields(first.EntityType)
                If Not TableEntities(item.EventCode).Contains(first.EntityType) OrElse group.Count() <> fields.Length Then Throw New ArgumentException("صف طاولة غير مكتمل.")
                If first.EntityType = "TablePrintRow" Then
                    If item.EventOrdinal = 1 OrElse Not Regex.IsMatch(first.EntityId, "\AQ[1-9][0-9]*\z") Then Throw New ArgumentException("هوية طابور الطباعة محلية للطلب.")
                ElseIf Not Integer.TryParse(first.EntityId, key) OrElse key <= 0 Then
                    Throw New ArgumentException("معرف صف الطاولة غير صالح.")
                End If
                If transfer Then
                    If (item.EventOrdinal = 1) <> (first.EntityType = "TableState") Then Throw New ArgumentException("جزء النقل غير صحيح.")
                    If first.EntityType = "TableState" AndAlso Not {request.EntityId, request.Inputs.Single(Function(x) x.Name = "DestinationTableId").Value.Text}.Contains(first.EntityId) Then Throw New ArgumentException("طاولة خارج الطلب.")
                Else
                    If (item.EventOrdinal = 1) = (first.EntityType = "TablePrintRow") Then Throw New ArgumentException("جزء صنف الطاولة غير صحيح.")
                    If first.EntityType = "TableInvoice" AndAlso first.EntityId <> request.EntityId Then Throw New ArgumentException("رأس فاتورة مختلف.")
                    If first.EntityType = "TableLine" AndAlso first.EntityId <> request.Inputs.Single(Function(x) x.Name = "TargetLineId").Value.Text Then Throw New ArgumentException("سطر مختلف.")
                End If
                For Each change In group
                    If Not fields.Any(Function(x) x.Split("|"c)(0) = change.FieldCode AndAlso x.Split("|"c)(3) = change.SourceSqlType) OrElse change.ValueOrigin <> 1 Then Throw New ArgumentException("مصدر حقل الطاولة غير معتمد.")
                    Dim beforeAbsent = first.EntityType = "TablePrintRow", afterAbsent = first.EntityType = "TableLine" AndAlso item.EventCode = "Table.LineDeleted"
                    If If(beforeAbsent, change.Before.State <> AuditValueState.Absent, change.Before.State < AuditValueState.SqlNull) OrElse If(afterAbsent, change.After.State <> AuditValueState.Absent, change.After.State < AuditValueState.SqlNull) Then Throw New ArgumentException("حالة صف الطاولة غير صحيحة.")
                    If change.FieldCode = "LineDocumentId" AndAlso (change.Before.Text <> request.EntityId OrElse (Not afterAbsent AndAlso change.After.Text <> request.EntityId)) Then Throw New ArgumentException("السطر يتبع فاتورة أخرى.")
                Next
            Next
        End If
        If IsItemMaster(item.EventCode) Then
            If item.EntityType <> request.EntityType OrElse item.EntityId <> request.EntityId OrElse item.DocumentNumber <> request.EntityId OrElse item.RelatedEntities.Count <> 0 Then Throw New ArgumentException("مرجع بطاقة الصنف غير صالح.")
            If item.EventOrdinal = 1 AndAlso (item.Changes.Count <> ItemMasterFields("CatalogItemRecord").Length OrElse item.Changes.Any(Function(x) x.EntityType <> "CatalogItemRecord" OrElse x.EntityId <> item.EntityId)) Then Throw New ArgumentException("يلزم صف الصنف كاملًا.")
            For Each group In item.Changes.GroupBy(Function(x) x.EntityType & ChrW(0) & x.EntityId)
                Dim first = group.First(), fields = ItemMasterFields(first.EntityType), key As Integer
                If group.Count() <> fields.Length OrElse Not Integer.TryParse(first.EntityId, key) OrElse key <= 0 OrElse (item.EventOrdinal = 1) <> (first.EntityType = "CatalogItemRecord") Then Throw New ArgumentException("صف تابع للصنف غير مكتمل.")
                For Each change In group
                    Dim photo = {"ItemPhotoSha256", "ItemPhotoBytes"}.Contains(change.FieldCode)
                    If Not fields.Any(Function(x) x.Split("|"c)(0) = change.FieldCode AndAlso x.Split("|"c)(3) = change.SourceSqlType) OrElse change.ValueOrigin <> If(photo, 3, 1) OrElse change.Before.State = AuditValueState.NotCaptured OrElse change.After.State = AuditValueState.NotCaptured OrElse (change.Before.State = AuditValueState.Absent AndAlso change.After.State = AuditValueState.Absent) Then Throw New ArgumentException("قيمة بطاقة صنف غير معتمدة.")
                    If item.EventCode = "Item.Deleted" AndAlso (change.Before.State < AuditValueState.SqlNull OrElse change.After.State <> AuditValueState.Absent) Then Throw New ArgumentException("الحذف يتطلب الصف المحذوف فعليًا.")
                    If item.EventCode = "Item.FastUpdated" AndAlso (change.After.State < AuditValueState.SqlNull OrElse (first.EntityType <> "ItemUnit" AndAlso change.Before.State < AuditValueState.SqlNull)) Then Throw New ArgumentException("حالة التعديل السريع غير صالحة.")
                    If {"ItemId", "AlertItemId", "FormatItemId", "DifferenceItemId"}.Contains(change.FieldCode) AndAlso ((change.Before.State = AuditValueState.Present AndAlso change.Before.Text <> item.EntityId) OrElse (change.After.State = AuditValueState.Present AndAlso change.After.Text <> item.EntityId)) Then Throw New ArgumentException("صف يتبع صنفًا آخر.")
                    If change.FieldCode = "ItemPhotoSha256" AndAlso {change.Before, change.After}.Any(Function(v) v.State = AuditValueState.Present AndAlso Not Regex.IsMatch(v.Text, "\A[A-F0-9]{64}\z")) Then Throw New ArgumentException("بصمة الصورة غير صالحة.")
                Next
                If group.Select(Function(x) x.Before.State = AuditValueState.Absent).Distinct().Count() <> 1 OrElse group.Select(Function(x) x.After.State = AuditValueState.Absent).Distinct().Count() <> 1 Then Throw New ArgumentException("حالة وجود صف الصنف غير متجانسة.")
            Next
        End If
        If IsPricingRule(item.EventCode) Then
            Dim offer = item.EventCode.StartsWith("Offer.", StringComparison.Ordinal), parentType = If(offer, "ItemOffers", "PriceRuleGroup"), rowType = If(offer, "OfferSnapshotRow", "PriceRule")
            Dim id As Integer
            If item.EntityType <> request.EntityType OrElse item.EntityId <> request.EntityId OrElse item.RelatedEntities.Count <> 0 OrElse Not Integer.TryParse(item.DocumentNumber, id) OrElse id <= 0 Then Throw New ArgumentException("مرجع حدث قاعدة التسعير غير صالح.")
            If offer AndAlso item.DocumentNumber <> item.EntityId Then Throw New ArgumentException("مرجع عروض الصنف لا يطابق الطلب.")
            If item.EventCode = "PriceRule.Deleted" AndAlso item.DocumentNumber <> request.Inputs.Single(Function(x) x.Name = "TargetPriceRuleId").Value.Text Then Throw New ArgumentException("معرف القاعدة المحذوفة لا يطابق الطلب.")
            If item.EventOrdinal = 1 AndAlso (item.Changes.Count <> PricingFields(parentType).Length OrElse item.Changes.Any(Function(x) x.EntityType <> parentType OrElse x.EntityId <> item.EntityId)) Then Throw New ArgumentException("يلزم سياق قاعدة التسعير كاملًا.")
            For Each group In item.Changes.GroupBy(Function(x) x.EntityType & ChrW(0) & x.EntityId)
                Dim first = group.First(), entity = If(item.EventOrdinal = 1, parentType, rowType), fields = PricingFields(entity)
                If first.EntityType <> entity OrElse group.Count() <> fields.Length Then Throw New ArgumentException("صف قاعدة تسعير غير مكتمل.")
                If item.EventOrdinal > 1 AndAlso If(offer, Not Regex.IsMatch(first.EntityId, "\AS[1-9][0-9]*\z"), Not Integer.TryParse(first.EntityId, id) OrElse id <= 0) Then Throw New ArgumentException("معرف تفاصيل قاعدة التسعير غير صالح.")
                For Each change In group
                    If Not fields.Any(Function(x) x.Split("|"c)(0) = change.FieldCode AndAlso x.Split("|"c)(3) = change.SourceSqlType) OrElse change.ValueOrigin <> 1 OrElse change.Before.State = AuditValueState.NotCaptured OrElse change.After.State = AuditValueState.NotCaptured OrElse (change.Before.State = AuditValueState.Absent AndAlso change.After.State = AuditValueState.Absent) Then Throw New ArgumentException("قيم قاعدة التسعير غير فعلية.")
                    If item.EventOrdinal = 1 AndAlso (change.Before.State < AuditValueState.SqlNull OrElse change.After.State < AuditValueState.SqlNull) Then Throw New ArgumentException("سياق قاعدة التسعير يجب أن يبقى موجودًا.")
                    If {"OfferItemId", "RuleGroupId"}.Contains(change.FieldCode) AndAlso ((change.Before.State = AuditValueState.Present AndAlso change.Before.Text <> item.EntityId) OrElse (change.After.State = AuditValueState.Present AndAlso change.After.Text <> item.EntityId)) Then Throw New ArgumentException("صف تسعير يتبع كيانًا آخر.")
                    If item.EventOrdinal > 1 AndAlso item.EventCode.EndsWith("Created", StringComparison.Ordinal) AndAlso change.After.State < AuditValueState.SqlNull Then Throw New ArgumentException("الإنشاء لا يحذف صفوف تسعير.")
                    If item.EventOrdinal > 1 AndAlso item.EventCode.EndsWith("Deleted", StringComparison.Ordinal) AndAlso change.Before.State < AuditValueState.SqlNull Then Throw New ArgumentException("الحذف لا ينشئ صفوف تسعير.")
                    If item.EventOrdinal > 1 AndAlso item.EventCode = "Offer.ItemOffersDeleted" AndAlso change.After.State <> AuditValueState.Absent Then Throw New ArgumentException("الحذف يشمل جميع عروض الصنف.")
                Next
                If group.Select(Function(x) x.Before.State = AuditValueState.Absent).Distinct().Count() <> 1 OrElse group.Select(Function(x) x.After.State = AuditValueState.Absent).Distinct().Count() <> 1 Then Throw New ArgumentException("حالة وجود صف التسعير غير متجانسة.")
            Next
        End If
        If IsItemUnit(item.EventCode) Then
            Dim target As Integer
            If item.EntityType <> "CatalogItem" OrElse item.EntityId <> request.EntityId OrElse item.RelatedEntities.Count <> 0 OrElse Not Integer.TryParse(item.DocumentNumber, target) OrElse target <= 0 Then Throw New ArgumentException("مرجع حدث رابط الوحدة غير صالح.")
            If item.EventCode <> "ItemUnit.Created" AndAlso item.DocumentNumber <> request.Inputs.Single(Function(x) x.Name = "TargetItemUnitId").Value.Text Then Throw New ArgumentException("الرابط المستهدف لا يطابق الطلب.")
            If item.EventOrdinal = 1 AndAlso (item.Changes.Count <> 2 OrElse item.Changes.Any(Function(x) x.EntityType <> "CatalogItem" OrElse x.EntityId <> item.EntityId)) Then Throw New ArgumentException("يلزم سياق الصنف كاملًا.")
            For Each group In item.Changes.GroupBy(Function(x) x.EntityType & ChrW(0) & x.EntityId)
                Dim first = group.First(), fields = ItemUnitFields(first.EntityType)
                If group.Count() <> fields.Length OrElse (item.EventOrdinal = 1) <> (first.EntityType = "CatalogItem") Then Throw New ArgumentException("صف رابط وحدة غير مكتمل.")
                For Each change In group
                    If Not fields.Any(Function(x) x.Split("|"c)(0) = change.FieldCode AndAlso x.Split("|"c)(3) = change.SourceSqlType) OrElse change.ValueOrigin <> 1 OrElse change.Before.State = AuditValueState.NotCaptured OrElse change.After.State = AuditValueState.NotCaptured OrElse (change.Before.State = AuditValueState.Absent AndAlso change.After.State = AuditValueState.Absent) Then Throw New ArgumentException("قيم رابط الوحدة غير فعلية.")
                    If first.EntityType = "CatalogItem" AndAlso (change.Before.State < AuditValueState.SqlNull OrElse change.After.State < AuditValueState.SqlNull) Then Throw New ArgumentException("سياق الصنف يجب أن يبقى موجودًا.")
                    If change.FieldCode = "ItemId" AndAlso ((change.Before.State = AuditValueState.Present AndAlso change.Before.Text <> item.EntityId) OrElse (change.After.State = AuditValueState.Present AndAlso change.After.Text <> item.EntityId)) Then Throw New ArgumentException("رابط وحدة يتبع صنفًا آخر.")
                Next
                If group.Select(Function(x) x.Before.State = AuditValueState.Absent).Distinct().Count() <> 1 OrElse group.Select(Function(x) x.After.State = AuditValueState.Absent).Distinct().Count() <> 1 Then Throw New ArgumentException("حالة وجود الصف غير متجانسة.")
            Next
        End If
        If IsGroup(item.EventCode) Then
            Dim create = item.EventCode = "Group.Created", delete = item.EventCode = "Group.Deleted", id As Integer
            If item.EntityType <> "ProductGroup" OrElse Not Integer.TryParse(item.EntityId, id) OrElse id <= 0 OrElse (Not create AndAlso item.EntityId <> request.EntityId) OrElse item.RelatedEntities.Count <> 0 Then Throw New ArgumentException("مرجع حدث التصنيف غير صالح.")
            If item.EventOrdinal = 1 AndAlso (item.Changes.Count <> 13 OrElse item.Changes.Any(Function(x) x.EntityType <> "ProductGroup")) Then Throw New ArgumentException("الجزء الأول يتطلب رأس التصنيف كاملًا.")
            For Each group In item.Changes.GroupBy(Function(x) x.EntityType & ChrW(0) & x.EntityId)
                Dim first = group.First(), definitions = GroupFields(first.EntityType)
                If group.Count() <> definitions.Length OrElse (item.EventOrdinal = 1) <> (first.EntityType = "ProductGroup") Then Throw New ArgumentException("صف تصنيف غير مكتمل أو في جزء غير صحيح.")
                For Each change In group
                    Dim definition = definitions.SingleOrDefault(Function(x) x.Split("|"c)(0) = change.FieldCode)
                    If definition Is Nothing OrElse change.SourceSqlType <> definition.Split("|"c)(3) OrElse change.ValueOrigin <> 1 Then Throw New ArgumentException("حقل التصنيف غير معتمد.")
                    If first.EntityType = "ProductGroup" Then
                        If change.EntityId <> item.EntityId OrElse If(create, change.Before.State <> AuditValueState.Absent, change.Before.State < AuditValueState.SqlNull) OrElse If(delete, change.After.State <> AuditValueState.Absent, change.After.State < AuditValueState.SqlNull) Then Throw New ArgumentException("حالة رأس التصنيف غير صحيحة.")
                    Else
                        If change.Before.State <> AuditValueState.Present AndAlso change.Before.State <> AuditValueState.Absent Then Throw New ArgumentException("رابط طابعة غير ملتقط.")
                        If change.After.State <> AuditValueState.Present AndAlso change.After.State <> AuditValueState.Absent Then Throw New ArgumentException("رابط طابعة غير ملتقط.")
                        If change.Before.State = AuditValueState.Absent AndAlso change.After.State = AuditValueState.Absent Then Throw New ArgumentException("رابط غير موجود في الحالتين.")
                        If change.FieldCode = "GroupId" AndAlso ((change.Before.State = AuditValueState.Present AndAlso change.Before.Text <> item.EntityId) OrElse (change.After.State = AuditValueState.Present AndAlso change.After.Text <> item.EntityId)) Then Throw New ArgumentException("الرابط يتبع تصنيفًا آخر.")
                    End If
                Next
            Next
        End If
        If IsUnit(item.EventCode) Then
            Dim create = item.EventCode = "Unit.Created", delete = item.EventCode = "Unit.Deleted", id As Integer
            If item.EventOrdinal <> 1 OrElse item.EntityType <> "Unit" OrElse Not Integer.TryParse(item.EntityId, id) OrElse id <= 2 OrElse (Not create AndAlso item.EntityId <> request.EntityId) OrElse item.RelatedEntities.Count <> 0 OrElse item.Changes.Count <> 3 Then Throw New ArgumentException("مرجع حدث الوحدة غير صالح.")
            For Each change In item.Changes
                Dim definition = UnitFields().SingleOrDefault(Function(x) x.Split("|"c)(0) = change.FieldCode)
                If definition Is Nothing OrElse change.EntityType <> "Unit" OrElse change.EntityId <> item.EntityId OrElse change.SourceSqlType <> definition.Split("|"c)(3) OrElse change.ValueOrigin <> 1 OrElse change.Before.State <> If(create, AuditValueState.Absent, AuditValueState.Present) OrElse change.After.State <> If(delete, AuditValueState.Absent, AuditValueState.Present) Then Throw New ArgumentException("يلزم صف الوحدة الفعلي كاملًا قبل وبعد.")
            Next
        End If
        If item.EventCode = "Document.Voided" Then
            Dim voidKind = Integer.Parse(request.Inputs.Single(Function(x) x.Name = "DocumentType").Value.Text, CultureInfo.InvariantCulture)
            If item.EntityType <> "BusinessDocument" OrElse item.EntityId <> request.EntityId OrElse item.RelatedEntities.Count <> 0 Then Throw New ArgumentException("مرجع الإلغاء مختلف.")
            If item.EventOrdinal = 1 AndAlso (item.Changes.Count <> VoidFields("BusinessDocument").Length OrElse item.Changes.Any(Function(x) x.EntityType <> "BusinessDocument" OrElse x.EntityId <> request.EntityId)) Then Throw New ArgumentException("الحدث الأول للإلغاء يتطلب الرأس الكامل.")
            For Each group In item.Changes.GroupBy(Function(x) x.EntityType & ChrW(0) & x.EntityId)
                Dim first = group.First(), definitions = VoidFields(first.EntityType)
                If Not VoidAllowedEntities(voidKind).Contains(first.EntityType) Then Throw New ArgumentException("تفاصيل الإلغاء لا تنتمي إلى نوع المستند.")
                If group.Count() <> definitions.Length OrElse (item.EventOrdinal = 1) <> (first.EntityType = "BusinessDocument") Then Throw New ArgumentException("صف إلغاء غير مكتمل أو في الجزء غير الصحيح.")
                For Each change In group
                    If Not definitions.Any(Function(x) x.Split("|"c)(0) = change.FieldCode AndAlso x.Split("|"c)(3) = change.SourceSqlType) OrElse change.ValueOrigin <> 1 OrElse change.Before.State < AuditValueState.SqlNull OrElse change.After.State = AuditValueState.NotCaptured Then Throw New ArgumentException("تفاصيل الإلغاء يجب أن تكون قيمًا فعلية.")
                    If first.EntityType <> "SalesStructure" AndAlso change.After.State < AuditValueState.SqlNull Then Throw New ArgumentException("بعد الإلغاء يبقى الصف موجودًا.")
                Next
            Next
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
