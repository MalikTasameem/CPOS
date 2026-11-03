Option Strict On
Imports System.Linq

Friend NotInheritable Class AuditMessageFormatter
    Friend Shared Function Format(request As AuditOperationRequest, item As AuditEventRequest) As String
        Select Case item.EventCode
            Case "Receipt.Created", "Receipt.EditOpened", "Receipt.Updated", "Receipt.Voided"
                Dim action = If(item.EventCode = "Receipt.Created", "إنشاء", If(item.EventCode = "Receipt.EditOpened", "فتح تعديل", If(item.EventCode = "Receipt.Updated", "حفظ تعديل", "إلغاء")))
                Dim details = If(item.EventOrdinal = 1,
                    "؛ النوع: " & If(FieldText(item, "ReceiptHeader", "Rct_BsType_ID", False) = "3", "قبض", "صرف") &
                    "؛ الحساب: " & FieldText(item, "ReceiptHeader", "Rct_AG_ID", False) &
                    "؛ خزينة الرأس: " & FieldText(item, "ReceiptHeader", "Rct_Tr_ID", False) &
                    "؛ طريقة الدفع: " & FieldText(item, "ReceiptHeader", "Rct_Pay_ID", False) &
                    "؛ القيمة من " & FieldText(item, "ReceiptHeader", "Rct_Pure", True) & " إلى " & FieldText(item, "ReceiptHeader", "Rct_Pure", False) &
                    "؛ الخصم من " & FieldText(item, "ReceiptHeader", "Rct_Discount", True) & " إلى " & FieldText(item, "ReceiptHeader", "Rct_Discount", False),
                    "؛ تفاصيل حركات الخزينة المرتبطة قبل/بعد")
                Return action & " الإيصال رقم " & item.DocumentNumber & "؛ الحركة " & item.EntityId & "؛ الجزء " & item.EventOrdinal.ToString() & details & ". حُفظت العملية والمراقبة في معاملة واحدة. لا يعني الحدث إعادة ترحيل قيد محاسبي أو إقفال عهدة."
            Case "TableSplit.Started", "TableSplit.UnitMoved", "TableSplit.UnitReturned", "TableSplit.Confirmed"
                Dim label = If(item.EventCode = "TableSplit.Started", "فتح نقل أصناف إلى طاولة أخرى", If(item.EventCode = "TableSplit.UnitMoved", "نقل كمية صنف بين الطاولتين", If(item.EventCode = "TableSplit.UnitReturned", "إرجاع كمية صنف للطاولة المصدر", "تأكيد نقل أصناف الطاولة")))
                Return label & "؛ المرجع " & item.EntityId & "؛ الجزء " & item.EventOrdinal.ToString() & ". حُفظ الأثر الفوري والرقابة في معاملة واحدة؛ رؤوس الفواتير والأصناف والمخزون وحالة الطاولة وطابور الطباعة المتأثرة مرفقة قبل/بعد."
            Case "Agent.Created", "Agent.Updated", "Agent.Deleted", "User.Created", "User.Updated", "User.Deleted", "User.CredentialsChanged"
                Dim isAgent = item.EventCode.StartsWith("Agent.", StringComparison.Ordinal), removed = item.EventCode.EndsWith(".Deleted", StringComparison.Ordinal)
                Dim action = If(item.EventCode.EndsWith(".Created", StringComparison.Ordinal), "إنشاء", If(removed, "حذف", If(item.EventCode = "User.CredentialsChanged", "تغيير كلمة مرور", "تعديل")))
                Return action & If(isAgent, " بطاقة حساب/عميل ", " مستخدم ") & item.EntityId & If(item.EventOrdinal = 1, "؛ الاسم: " & FieldText(item, If(isAgent, "PeopleAgent", "PeopleUser"), If(isAgent, "AgentName", "UserName"), removed), "؛ آثار الربط/الدليل أو تأكيد إجراء بيانات الاعتماد") & "؛ الجزء " & item.EventOrdinal.ToString() & ". الحفظ والمراقبة في معاملة واحدة؛ كلمات المرور والرموز السرية وبصماتها غير مسجلة."
            Case "Item.PlaceholderAcquired", "Item.FullSaved", "Item.QuickSaved", "Item.DisabledRowsRemoved"
                Dim cleanup = item.EventCode = "Item.DisabledRowsRemoved"
                Return If(cleanup, "تنظيف صفوف أصناف غير معتمدة للمستخدم ", If(item.EventCode = "Item.PlaceholderAcquired", "تخصيص/استعادة صف صنف غير معتمد ", If(item.EventCode = "Item.QuickSaved", "اعتماد إضافة صنف سريعة ", "حفظ بطاقة الصنف "))) & item.EntityId & "؛ الجزء " & item.EventOrdinal.ToString() & If(Not cleanup AndAlso item.EventOrdinal = 1, "؛ الاسم: " & FieldText(item, "CatalogItemRecord", "ItemName", False), "") & ". القيم الفعلية قبل/بعد للصنف ووحداته وتنبيهاته وتواريخ صلاحيته مرفقة؛ الصورة مسجلة ببصمتها وحجمها دون بياناتها الثنائية."
            Case "Salary.BatchPosted"
                Return "احتساب مرتبات شهر " & request.Inputs.Single(Function(x) x.Name = "SalaryMonth").Value.Text & " لسنة " & request.Inputs.Single(Function(x) x.Name = "SalaryYear").Value.Text & "؛ الجزء " & item.EventOrdinal.ToString() & ". يتضمن المرتبات السابقة الملغاة والجديدة ومراجع الموظفين والإضافات والخصومات كما طبقها الإجراء؛ لا يعني إنشاء إيصال دفع أو تنفيذ قيد عكسي."
            Case "Document.EditFieldsSaved", "Document.ProjectChanged", "Purchase.AccountCurrencyChanged", "Purchase.CurrencyChanged", "Quote.Confirmed", "Quote.DiscountChanged", "Quote.DiscountDistributed", "Document.MetadataSaved", "Quote.Reserved", "Quote.EditFieldsSaved"
                Dim label As String
                Select Case item.EventCode
                    Case "Document.EditFieldsSaved" : label = "حفظ تعديل بيانات الفاتورة"
                    Case "Document.MetadataSaved" : label = "حفظ بيانات رأس الفاتورة قبل الدفع"
                    Case "Document.ProjectChanged" : label = "تغيير مشروع الفاتورة"
                    Case "Purchase.AccountCurrencyChanged", "Purchase.CurrencyChanged" : label = "تغيير حساب/عملة المشتريات"
                    Case "Quote.Confirmed" : label = "حفظ واعتماد عرض السعر"
                    Case "Quote.Reserved" : label = "تغيير حالة حجز عرض السعر"
                    Case "Quote.EditFieldsSaved" : label = "إنهاء تعديل بيانات عرض السعر"
                    Case "Quote.DiscountDistributed" : label = "توزيع خصم عرض السعر على أصنافه"
                    Case Else : label = "تغيير خصم عرض السعر"
                End Select
                Return label & "؛ الحركة " & item.EntityId & "؛ الجزء " & item.EventOrdinal.ToString() & ". الرأس والصفوف المتأثرة محفوظة قبل/بعد؛ لا يدل هذا الحدث على اعتماد دفع أو إنشاء إيصال خزينة."
            Case "Inventory.PurchaseLineDeleted", "Inventory.PurchaseLineChanged", "Manufacturing.AutoProductAdded", "Manufacturing.ManualLineAdded", "Manufacturing.ProductDeleted", "Manufacturing.ProductChanged", "Manufacturing.IngredientDeleted", "Manufacturing.CostRecalculated", "Manufacturing.PurchaseCopied", "Manufacturing.SalesCopied", "Inventory.QuantityRecounted", "Inventory.ItemCostRecounted", "Inventory.AllCostsRecounted"
                Dim label As String
                Select Case item.EventCode
                    Case "Inventory.PurchaseLineDeleted" : label = "حذف صنف مشتريات/جرد"
                    Case "Inventory.PurchaseLineChanged" : label = "تعديل وحدة/كمية/تكلفة صنف مشتريات/جرد"
                    Case "Manufacturing.AutoProductAdded" : label = "إضافة منتج بتصنيع آلي"
                    Case "Manufacturing.ManualLineAdded" : label = "إضافة منتج/مادة بتصنيع يدوي"
                    Case "Manufacturing.ProductDeleted" : label = "حذف منتج تصنيع ومكوناته"
                    Case "Manufacturing.ProductChanged" : label = "تعديل منتج تصنيع"
                    Case "Manufacturing.IngredientDeleted" : label = "حذف مادة من التصنيع"
                    Case "Manufacturing.CostRecalculated" : label = "إعادة توزيع تكلفة التصنيع"
                    Case "Manufacturing.PurchaseCopied" : label = "نقل أصناف مشتريات إلى مواد التصنيع"
                    Case "Manufacturing.SalesCopied" : label = "نقل أصناف بيع إلى منتجات التصنيع"
                    Case "Inventory.QuantityRecounted" : label = "إعادة احتساب كميات المخزون"
                    Case "Inventory.ItemCostRecounted" : label = "إعادة احتساب تكلفة صنف"
                    Case Else : label = "إعادة احتساب متوسط تكلفة الأصناف"
                End Select
                Return label & "؛ المرجع " & item.EntityId & "؛ الجزء " & item.EventOrdinal.ToString() & ". تفاصيل الآثار الفعلية على المستندات والتكلفة والمخزون قبل/بعد مرفقة. " & If(item.EventCode = "Inventory.QuantityRecounted", "نطاق تواريخ صلاحية المخزون يتبع الإجراء الحالي وقد يشمل مخازن أخرى؛ لم يُصحح سلوكه المالي ضمن نقل المراقبة.", "لم تُغيّر معادلات الأعمال ضمن نقل المراقبة.")

            Case "Quote.LineAdded", "Quote.LineDeleted", "Quote.LineChanged", "Quote.LineQuantityStepped"
                Dim deleted = item.EventCode = "Quote.LineDeleted"
                Return If(deleted, "حذف", If(item.EventCode = "Quote.LineAdded", "إضافة", "تعديل")) & " صنف عرض السعر " & item.DocumentNumber & ": " & FieldText(item, "QuoteLine", "QuoteItemName", deleted) & "؛ الصنف " & FieldText(item, "QuoteLine", "QuoteItemId", deleted) & "؛ الوحدة من " & FieldText(item, "QuoteLine", "QuoteUnitId", True) & " إلى " & FieldText(item, "QuoteLine", "QuoteUnitId", False) & "؛ الكمية من " & FieldText(item, "QuoteLine", "QuoteQuantity", True) & " إلى " & FieldText(item, "QuoteLine", "QuoteQuantity", False) & "؛ السعر من " & FieldText(item, "QuoteLine", "QuotePrice", True) & " إلى " & FieldText(item, "QuoteLine", "QuotePrice", False) & ". حفظ الصنف والمراقبة في معاملة واحدة؛ ليس بيعًا أو حركة خزينة."
            Case "Document.FastDiscountChanged"
                Return "تأكيد خصم الفاتورة " & item.DocumentNumber & "؛ الخصم من " & FieldText(item, "DocumentHeader", "Discount", True) & " إلى " & FieldText(item, "DocumentHeader", "Discount", False) & "؛ حقل النسبة Debit_2 من " & FieldText(item, "DocumentHeader", "SecondaryDebit", True) & " إلى " & FieldText(item, "DocumentHeader", "SecondaryDebit", False) & ". تغيير مباشر محفوظ مع المراقبة؛ لا يعني اعتماد الفاتورة أو تسوية مدفوعاتها."
            Case "Document.AccountChanged", "Document.DiscountChanged"
                Return If(item.EventCode = "Document.AccountChanged", "تغيير حساب الفاتورة؛ من " & FieldText(item, "DocumentHeader", "AccountId", True) & " — " & FieldText(item, "DocumentHeader", "AccountName", True) & " إلى " & FieldText(item, "DocumentHeader", "AccountId", False) & " — " & FieldText(item, "DocumentHeader", "AccountName", False), "تغيير خصم الفاتورة؛ من " & FieldText(item, "DocumentHeader", "Discount", True) & " إلى " & FieldText(item, "DocumentHeader", "Discount", False)) & "؛ الفاتورة " & item.DocumentNumber & "؛ الحركة " & item.EntityId & ". تغيير مباشر محفوظ مع المراقبة في معاملة واحدة؛ لا يعني اعتماد الفاتورة أو تسوية مدفوعاتها أو إعادة حساب الصافي."

            Case "Purchase.LineAdded"
                Return If(item.EventOrdinal = 1, "إضافة صنف إلى فاتورة مشتريات/جرد", "آثار إضافة صنف على التكلفة والأسعار والمخزون") &
                    "؛ الفاتورة " & item.DocumentNumber & "؛ الحركة " & item.EntityId &
                    If(item.EventOrdinal = 1, "؛ الصنف " & FieldText(item, "PurchaseLine", "PurchaseItemId", False) &
                    "؛ الوحدة " & FieldText(item, "PurchaseLine", "PurchaseUnitId", False) &
                    "؛ الكمية " & FieldText(item, "PurchaseLine", "PurchaseQuantity", False) &
                    "؛ التكلفة " & FieldText(item, "PurchaseLine", "PurchasePrice", False), "") & "؛ الجزء " & item.EventOrdinal.ToString()
            Case "AccountEntry.Created"
                Dim kind = FieldText(item, "AccountEntry", "DocumentType", False)
                Return If(kind = "5", "إضافة مرتب يدوي", If(kind = "6", "إضافة رصيد سابق مدين (له)", "إضافة رصيد سابق دائن (عليه)")) &
                    "؛ الحركة " & item.EntityId & "؛ الحساب " & FieldText(item, "AccountEntry", "AccountId", False) &
                    "؛ القيمة " & FieldText(item, "AccountEntry", "DocumentPure", False) &
                    "؛ البيان " & FieldText(item, "AccountEntry", "EntryTitle", False) &
                    ". التاريخ والقيم المحفوظة في طرفي الحركة ضمن التفاصيل؛ لا يعني احتساب رواتب جماعيًا أو إنشاء إيصال خزينة."
            Case "Expense.LineAdded", "Expense.LineDeleted"
                Dim removed = item.EventCode = "Expense.LineDeleted"
                Return If(removed, "حذف بند مصروف", "إضافة بند مصروف") & "؛ الفاتورة " & item.DocumentNumber & "؛ الحركة " & item.EntityId &
                    "؛ بطاقة المصروف " & FieldText(item, "ExpenseLine", "ExpenseId", removed) &
                    "؛ الكمية " & FieldText(item, "ExpenseLine", "ExpenseQuantity", removed) &
                    "؛ السعر " & FieldText(item, "ExpenseLine", "ExpensePrice", removed) &
                    "؛ قيمة البند " & FieldText(item, "ExpenseLine", "ExpenseTotal", removed) &
                    "؛ الصافي " & FieldText(item, "ExpenseDocument", "DocumentPure", True) & " ← " & FieldText(item, "ExpenseDocument", "DocumentPure", False) &
                    ". حفظ البند وإعادة حساب رأس الفاتورة والمراقبة في معاملة واحدة؛ لا يعني اعتماد الفاتورة أو تغيير إيصالات الدفع."
            Case "EmployeeValue.Created", "EmployeeValue.Updated", "EmployeeValue.Voided"
                Return If(item.EventCode = "EmployeeValue.Created", "إضافة", If(item.EventCode = "EmployeeValue.Updated", "تعديل", "إلغاء")) & " قيمة موظف؛ الحركة " & item.EntityId &
                    "؛ الموظف " & FieldText(item, "EmployeeValue", "EVEmployeeId", True) & " ← " & FieldText(item, "EmployeeValue", "EVEmployeeId", False) &
                    "؛ النوع " & FieldText(item, "EmployeeValue", "EVType", False) & " (0 إضافة، 1 خصم)؛ القيمة " & FieldText(item, "EmployeeValue", "EVAmount", True) & " ← " & FieldText(item, "EmployeeValue", "EVAmount", False) &
                    ". تاريخ الاستحقاق والشهر والسنة والملاحظات وحالة الإلغاء محفوظة. لا يعني هذا الحدث إعادة احتساب مرتب سبق إنشاؤه أو ترحيله."
            Case "Return.LineAdded", "Return.LineDeleted", "Return.LineQuantityChanged"
                If item.EventOrdinal > 1 Then Return "أرصدة مخزون المرتجع " & item.DocumentNumber & "؛ جزء " & item.EventOrdinal.ToString() & " من الطلب نفسه؛ أرصدة مشتقة قبل/بعد."
                Dim removed = item.EventCode = "Return.LineDeleted"
                Return If(removed, "حذف صنف", If(item.EventCode = "Return.LineAdded", "إضافة صنف", "تعديل كمية صنف")) &
                    If(request.Inputs.Single(Function(x) x.Name = "DocumentType").Value.Text = "10", " — مرتجع مبيعات ", " — مرتجع مشتريات ") & item.DocumentNumber &
                    "؛ الحركة " & item.EntityId & "؛ الصنف " & FieldText(item, "ReturnLine", "RLItemId", removed) &
                    "؛ الكمية " & FieldText(item, "ReturnLine", "RLQuantity", True) & " ← " & FieldText(item, "ReturnLine", "RLQuantity", False) &
                    "؛ سعر الوحدة " & FieldText(item, "ReturnLine", "RLPrice", removed) &
                    "؛ الصافي " & FieldText(item, "ReturnDocument", "DocumentPure", True) & " ← " & FieldText(item, "ReturnDocument", "DocumentPure", False) &
                    ". حفظ السطر ورأس المرتجع والأرصدة في معاملة واحدة؛ لا يعني اعتماد الفاتورة أو تعديل مدفوعاتها."
            Case "StockDocument.LineAdded", "StockDocument.LineDeleted", "StockDocument.LineQuantityChanged"
                If item.EventOrdinal > 1 Then Return "تفاصيل أرصدة مخزون الإذن " & item.DocumentNumber & "؛ الحركة " & item.EntityId & "؛ الجزء " & item.EventOrdinal.ToString() & ". أرصدة مشتقة من كشف المخزون قبل/بعد؛ جزء من الطلب نفسه وليس عملية إضافية."
                Dim kind = Integer.Parse(request.Inputs.Single(Function(x) x.Name = "DocumentType").Value.Text, Globalization.CultureInfo.InvariantCulture)
                Dim entity = AuditContract.StockDocumentSpec(kind)(2), removed = item.EventCode = "StockDocument.LineDeleted"
                Dim label = If(kind = 35, "إذن استلام", If(kind = 17, "إذن صرف", If(kind = 8, "مستند تالف", "تحويل مخازن")))
                Return If(removed, "حذف صنف", If(item.EventCode = "StockDocument.LineAdded", "إضافة صنف", "تعديل كمية صنف")) & " — " & label & " " & item.DocumentNumber & "؛ الحركة " & item.EntityId &
                    "؛ الصنف " & FieldText(item, entity, "SLItemId", removed) & "؛ الكمية " & FieldText(item, entity, "SLQuantity", True) & " ← " & FieldText(item, entity, "SLQuantity", False) &
                    "؛ التكلفة المحفوظة " & FieldText(item, entity, "SLCost", removed) & "؛ الصافي " & FieldText(item, "StockDocument", "DocumentPure", True) & " ← " & FieldText(item, "StockDocument", "DocumentPure", False) &
                    ". السطر والمخزن والوحدة والصلاحية والأثر الفعلي محفوظة؛ الحدث لا يعني اعتماد الرأس أو ترحيل قيد محاسبي."
            Case "TreasuryMovement.Created", "TreasuryMovement.Voided", "TreasuryTransfer.Created", "TreasuryTransfer.Voided", "AccountTransfer.Created", "AccountTransfer.Voided"
                Dim action = If(item.EventCode.EndsWith(".Created", StringComparison.Ordinal), "إنشاء", "إلغاء")
                Dim label = If(item.EventCode.StartsWith("AccountTransfer.", StringComparison.Ordinal), "تحويل بين الحسابات", If(item.EventCode.StartsWith("TreasuryTransfer.", StringComparison.Ordinal), "تحويل بين الخزائن", "حركة إيداع/سحب خزينة"))
                Dim detail As String
                If item.EventCode.StartsWith("AccountTransfer.", StringComparison.Ordinal) Then
                    detail = "؛ من الحساب " & FieldText(item, "FinanceAccountRow", "AccountAGIDFROM", False) & " إلى " & FieldText(item, "FinanceAccountRow", "AccountAGIDTO", False) & "؛ القيمة " & FieldText(item, "FinanceAccountRow", "AccountVALUE", False)
                Else
                    detail = "؛ تفاصيل كل طرف تشمل رقم الخزينة والمدين والدائن وحالة الإلغاء قبل/بعد"
                End If
                Return action & " " & label & "؛ الرقم الفعلي " & item.EntityId & detail & "؛ الجزء " & item.EventOrdinal.ToString() & ". حفظ الحركة والمراقبة في معاملة واحدة. لا يعني الحدث إنشاء أو عكس قيد محاسبي؛ حقول الترحيل تعكس الإجراء الحالي."
            Case "Table.Transferred"
                Return "نقل فواتير الطاولة " & request.EntityId & " إلى الطاولة " & request.Inputs.Single(Function(x) x.Name = "DestinationTableId").Value.Text &
                    "؛ الجزء " & item.EventOrdinal.ToString() & ". يشمل الفواتير غير المدفوعة وفق الإجراء الحالي؛ أسماء الطاولات وحالتها وربط الفواتير قبل/بعد مرفقة."
            Case "Table.LineQuantityChanged", "Table.LineDeleted"
                Return If(item.EventCode = "Table.LineDeleted", "حذف صنف من فاتورة الطاولة", "تعديل كمية صنف الطاولة") & "؛ الفاتورة " & If(item.DocumentNumber, "[دون رقم عرض]") &
                    "؛ T_ID=" & request.EntityId & "؛ السطر " & request.Inputs.Single(Function(x) x.Name = "TargetLineId").Value.Text & "؛ الجزء " & item.EventOrdinal.ToString() &
                    ". بيانات الصنف والإجماليات قبل/بعد مرفقة؛ تفاصيل طابور الطباعة تعني إدراج الأمر ولا تثبت تنفيذ الطباعة."
            Case "Item.FastUpdated", "Item.Deleted"
                If item.EventOrdinal > 1 Then Return "تفاصيل وحدات الصنف والتنبيهات وقوائم التصنيع وفروق الجرد؛ الصنف " & item.EntityId & "؛ الجزء " & item.EventOrdinal.ToString() & ". القيم الفعلية قبل/بعد ضمن الطلب نفسه."
                Return If(item.EventCode = "Item.Deleted", "حذف الصنف ", "تعديل سريع للصنف ") & item.EntityId & "؛ الاسم " & FieldText(item, "CatalogItemRecord", "ItemName", True) & " ← " & FieldText(item, "CatalogItemRecord", "ItemName", False) & ". الآثار الفعلية بالتفاصيل. الصورة ممثلة ببصمة SHA256 وحجم بايتات مشتقين، وليست نسخة قابلة للاستعادة. لا يغير النقل قواعد الإجراء القديم."
            Case "Offer.Created", "Offer.ItemOffersDeleted"
                If item.EventOrdinal > 1 Then Return "تفاصيل عروض الصنف " & item.EntityId & "؛ الجزء " & item.EventOrdinal.ToString() & ". S1 وما يليها مراجع صفوف داخل هذا الطلب فقط؛ جدول العروض بلا معرف صف مستقل. القيم والمكررات قبل/بعد محفوظة، وليست عمليات إضافية."
                Return If(item.EventCode = "Offer.Created", "إضافة فترة عرض للصنف ", "حذف جميع فترات عروض الصنف ") & item.EntityId & " — " & FieldText(item, "ItemOffers", "OfferItemName", False) & ". التواريخ والأوقات والعناوين الفعلية بالتفاصيل؛ لا يغيّر هذا الإجراء أسعار الوحدات المخزنة."
            Case "PriceRule.Created", "PriceRule.Deleted"
                If item.EventOrdinal > 1 Then Return "تفاصيل نسب تسعير التصنيف " & item.EntityId & "؛ القاعدة المستهدفة " & item.DocumentNumber & "؛ الجزء " & item.EventOrdinal.ToString() & ". النسبة والقيم الفعلية قبل/بعد ضمن الطلب نفسه."
                Return If(item.EventCode = "PriceRule.Created", "تعريف نسبة تسعير للتصنيف ", "حذف نسبة تسعير للتصنيف ") & item.EntityId & " — " & FieldText(item, "PriceRuleGroup", "RuleGroupName", False) & "؛ رقم القاعدة " & item.DocumentNumber & ". هذه قاعدة تستخدمها مسارات التسعير؛ ليست تحديثًا جماعيًا لأسعار IM_Units أو إعادة تسعير فواتير سابقة."
            Case "ItemUnit.Created", "ItemUnit.Updated", "ItemUnit.Deleted", "ItemUnit.PriceChanged", "ItemUnit.OfferPriceChanged"
                If item.EventOrdinal > 1 Then Return "تفاصيل وحدات الصنف " & item.EntityId & " ومكوناتها؛ الرابط المستهدف " & item.DocumentNumber & "؛ الجزء " & item.EventOrdinal.ToString() & ". القيم الفعلية قبل/بعد ضمن الطلب نفسه وليست عملية أخرى."
                Dim action = If(item.EventCode = "ItemUnit.Created", "إضافة رابط وحدة للصنف", If(item.EventCode = "ItemUnit.Deleted", "حذف رابط وحدة الصنف ومكوناته التابعة", If(item.EventCode = "ItemUnit.Updated", "تعديل بيانات رابط الوحدة", If(item.EventCode = "ItemUnit.PriceChanged", "تغيير سعر بيع الوحدة", "تغيير سعر عرض الوحدة"))))
                Return action & "؛ الصنف " & item.EntityId & " — " & FieldText(item, "CatalogItem", "ItemName", False) & "؛ رابط الوحدة " & item.DocumentNumber & ". الأسعار والباركود وحالة الوحدة الافتراضية والمكونات الفعلية قبل/بعد محفوظة في التفاصيل."
            Case "Group.Created", "Group.Updated", "Group.Deleted", "Group.PrinterLinked", "Group.PrinterUnlinked"
                If item.EventOrdinal > 1 Then Return "تفاصيل روابط طابعات التصنيف " & item.EntityId & "؛ الجزء " & item.EventOrdinal.ToString() & "؛ القيم قبل/بعد محفوظة ضمن الطلب نفسه، وليست عملية أخرى."
                Dim action = If(item.EventCode = "Group.Created", "إنشاء تصنيف", If(item.EventCode = "Group.Deleted", "حذف تصنيف", If(item.EventCode = "Group.Updated", "تعديل تصنيف", If(item.EventCode = "Group.PrinterLinked", "ربط طابعة إضافية بالتصنيف", "فك ربط طابعة إضافية بالتصنيف"))))
                Return action & "؛ الرقم " & item.EntityId & "؛ الاسم " & FieldText(item, "ProductGroup", "GroupName", True) & " ← " & FieldText(item, "ProductGroup", "GroupName", False) & ". ألوان العرض والترتيب وطابعة التجهيز والشاشة والمخزن والفئة وروابط الطابعات الفعلية محفوظة في التفاصيل."
            Case "Unit.Created", "Unit.Updated", "Unit.Deleted"
                Dim action = If(item.EventCode = "Unit.Created", "إنشاء وحدة", If(item.EventCode = "Unit.Deleted", "حذف وحدة", "تعديل وحدة"))
                Return action & "؛ الرقم " & item.EntityId & "؛ الاسم: " & FieldText(item, "Unit", "UnitName", True) & " ← " & FieldText(item, "Unit", "UnitName", False) &
                    "؛ الحمولة: " & FieldText(item, "Unit", "UnitCargo", True) & " ← " & FieldText(item, "Unit", "UnitCargo", False) & ". القيم الفعلية وحالة الوحدة الافتراضية محفوظة؛ هذا الحدث لا يعني إضافة ارتباط وحدة بصنف."
            Case "Document.Voided"
                If item.EventOrdinal > 1 Then Return "تفاصيل إلغاء المستند " & item.DocumentNumber & "؛ الحركة " & item.EntityId & "؛ الجزء " & item.EventOrdinal.ToString() & ". حالات السطور والإيصالات والخزينة والمكونات والأرصدة الفعلية؛ ليست عملية إلغاء إضافية."
                Return "إلغاء المستند " & item.DocumentNumber & "؛ نوع المعاملة " & FieldText(item, "BusinessDocument", "DocumentType", False) & "؛ الحركة " & item.EntityId &
                    "؛ حالة الإلغاء " & FieldText(item, "BusinessDocument", "Voided", True) & " ← " & FieldText(item, "BusinessDocument", "Voided", False) &
                    "؛ المستخدم المحفوظ في حقل VoidBy القديم: " & FieldText(item, "BusinessDocument", "VoidBy", False) & ". المنفذ الفعلي محفوظ بهوية حدث المراقبة؛ الإجراء القديم لم يُصحح. راجع تفاصيل الأثر الفعلي ولا تفترض إزالة كل المكونات أو ترحيل قيد عكسي."
            Case "Sales.LineAdded"
                If item.EventOrdinal > 1 Then Return "تفاصيل مكونات وأرصدة تابعة لإضافة/دمج الصنف في فاتورة البيع " & item.DocumentNumber & "؛ السطر " & item.EntityId & "؛ الجزء " & item.EventOrdinal.ToString() & "؛ ليست إضافة أخرى."
                Return "إضافة صنف أو دمج كميته في فاتورة البيع " & item.DocumentNumber & "؛ السطر الفعلي " & item.EntityId & "؛ الصنف " & FieldText(item, "SalesLine", "ItemId", False) &
                    "؛ الكمية " & FieldText(item, "SalesLine", "LineQuantity", True) & " ← " & FieldText(item, "SalesLine", "LineQuantity", False) &
                    "؛ السعر المحفوظ " & FieldText(item, "SalesLine", "LinePrice", False) & "؛ صافي الفاتورة " & FieldText(item, "SalesInvoice", "DocumentPure", True) & " ← " & FieldText(item, "SalesInvoice", "DocumentPure", False) & ". الحفظ والمكونات والمراقبة في معاملة واحدة."
            Case "Sales.LineDeleted"
                If item.EventOrdinal > 1 Then Return "تفاصيل تابعة لحذف سطر البيع " & item.EntityId & " من الفاتورة " & item.DocumentNumber & "؛ الجزء " & item.EventOrdinal.ToString() & ". المكونات والحجوزات والقيم التابعة والأرصدة الفعلية؛ ليست عملية حذف إضافية."
                Return "حذف صنف من فاتورة البيع " & item.DocumentNumber & "؛ السطر " & item.EntityId & "؛ الصنف " & FieldText(item, "SalesLine", "ItemId", True) &
                    "؛ الكمية " & FieldText(item, "SalesLine", "LineQuantity", True) & "؛ القيمة " & FieldText(item, "SalesLine", "LineTotal", True) &
                    "؛ صافي الفاتورة المخزن " & FieldText(item, "SalesInvoice", "DocumentPure", True) & " ← " & FieldText(item, "SalesInvoice", "DocumentPure", False) & ". القيم الفعلية قبل/بعد والتوابع محفوظة تحت الطلب نفسه."
            Case "Sales.LineQuantityStepped"
                If item.EventOrdinal > 1 Then Return "تفاصيل تابعة لتعديل كمية سطر البيع " & item.EntityId & " في الفاتورة " & item.DocumentNumber & "؛ الجزء " & item.EventOrdinal.ToString() & ". صفوف المكونات وأرصدة المخزون قبل/بعد؛ جزء من الطلب نفسه وليست زيادة مستقلة."
                Return "تعديل كمية صنف في فاتورة البيع " & item.DocumentNumber & "؛ السطر " & item.EntityId & "؛ الكمية السابقة " & FieldText(item, "SalesLine", "LineQuantity", True) &
                    "؛ الكمية الجديدة " & FieldText(item, "SalesLine", "LineQuantity", False) & "؛ إجمالي السطر السابق " & FieldText(item, "SalesLine", "LineTotal", True) &
                    "؛ الجديد " & FieldText(item, "SalesLine", "LineTotal", False) & ". أثر المكونات والمخزون في تفاصيل الطلب؛ إيصالات الدفع لم تُعدّل."
            Case "Sales.LineDetailsChanged", "Sales.LinePriceChanged"
                If item.EventOrdinal > 1 Then Return "تفاصيل مكونات وأرصدة تابعة لتعديل الصنف في فاتورة البيع " & item.DocumentNumber & "؛ السطر " & item.EntityId & "؛ الجزء " & item.EventOrdinal.ToString() & "؛ ليست عملية تعديل إضافية."
                Return If(item.EventCode = "Sales.LinePriceChanged", "تغيير سعر صنف", "تعديل تفاصيل صنف") & " في فاتورة البيع " & item.DocumentNumber & "؛ السطر " & item.EntityId &
                    "؛ الصنف " & FieldText(item, "SalesLine", "ItemId", False) & "؛ الكمية " & FieldText(item, "SalesLine", "LineQuantity", True) & " ← " & FieldText(item, "SalesLine", "LineQuantity", False) &
                    "؛ السعر " & FieldText(item, "SalesLine", "LinePrice", True) & " ← " & FieldText(item, "SalesLine", "LinePrice", False) &
                    "؛ خصم السطر " & FieldText(item, "SalesLine", "LineDiscount", True) & " ← " & FieldText(item, "SalesLine", "LineDiscount", False) &
                    "؛ صافي الفاتورة " & FieldText(item, "SalesInvoice", "DocumentPure", True) & " ← " & FieldText(item, "SalesInvoice", "DocumentPure", False) &
                    ". القيم الفعلية قبل/بعد محفوظة؛ لم تُعدّل إيصالات الدفع."
            Case "Document.EditOpened"
                Return "فتح تعديل " & DocumentLabel(item.EntityType) & "؛ رقم المستند " & If(item.DocumentNumber, "[دون رقم عرض]") & "؛ رقم الحركة " & item.EntityId &
                    ". سُجل المستخدم ووقت فتح التعديل والقيم المحاسبية الفعلية قبل/بعد؛ هذا الحدث لا يعني حفظ بنود المستند أو دفعاته أو إنهاء التعديل."
            Case "Sales.EditOpened"
                Return "فتح فاتورة المبيعات للتعديل؛ الفاتورة " & item.DocumentNumber & "؛ رقم الحركة " & item.EntityId &
                    ". سُجل المستخدم ووقت فتح التعديل والحقول المحاسبية قبل/بعد؛ هذا الحدث لا يعني حفظ تعديلات الأصناف أو تسوية المدفوعات."
            Case "AuditTest.Changed"
                Return "حدث اختبار معزول للمراقبة؛ الكيان " & item.EntityId & "؛ القيم السابقة واللاحقة محفوظة في التفاصيل."
            Case "Settlement.LineAdded"
                Return SettlementDescription(request, item, False)
            Case "Settlement.LineDeleted"
                Return SettlementDescription(request, item, True)
            Case "Settlement.LineUpdated"
                Return "تعديل فرق سطر التسوية؛ المستند " & item.DocumentNumber & "؛ السطر " & item.EntityId &
                    "؛ الكمية المخزنة من " & FieldText(item, "SettlementLine", "StoredQuantity", True) & " إلى " & FieldText(item, "SettlementLine", "StoredQuantity", False) &
                    "؛ رصيد المخزن من " & FieldText(item, "SettlementStock", "StockQuantity", True) & " إلى " & FieldText(item, "SettlementStock", "StockQuantity", False) &
                    ". هوية الصنف والمخزن والوحدة والتكلفة والصلاحية محفوظة دون تغيير؛ التفاصيل قبل/بعد مرفقة."
            Case "Settlement.Created", "Settlement.Approved", "Settlement.Voided", "Settlement.EditOpened", "Settlement.HeaderSaved", "Settlement.EditFinished"
                Dim action As String
                Select Case item.EventCode
                    Case "Settlement.Created" : action = "إنشاء مستند تسوية"
                    Case "Settlement.Approved" : action = "اعتماد مستند تسوية؛ لا تضاف كمياته مرة ثانية"
                    Case "Settlement.Voided" : action = "إلغاء مستند تسوية واستبعاد سطوره من الرصيد الفعلي"
                    Case "Settlement.EditOpened" : action = "فتح تعديل تسوية مع بقاء تأثير سطورها الفوري"
                    Case "Settlement.HeaderSaved" : action = "حفظ بيانات رأس التسوية"
                    Case Else : action = "إنهاء تعديل التسوية وحفظ بيانات رأسها"
                End Select
                Return action & "؛ رقم المستند " & item.DocumentNumber & "؛ T_ID=" & item.EntityId &
                    "؛ الاعتماد: " & FieldText(item, "Settlement", "Approved", True) & " ← " & FieldText(item, "Settlement", "Approved", False) &
                    "؛ الإلغاء: " & FieldText(item, "Settlement", "Voided", True) & " ← " & FieldText(item, "Settlement", "Voided", False) &
                    "؛ الإجمالي المحفوظ: " & FieldText(item, "Settlement", "DocumentTotal", False) & ". القيم والحقول المحاسبية قبل/بعد محفوظة في التفاصيل."
            Case "Security.SessionStarted"
                Return "نجح التحقق من المستخدم وتم تسجيل بدء الجلسة " & request.Context.SessionId.Value.ToString("D") & "."
            Case "Security.SessionEnded"
                Return "طلب المستخدم الخروج الصريح من الجلسة " & request.Context.SessionId.Value.ToString("D") & "."
            Case "Security.LoginRejected"
                Select Case item.ReasonCode
                    Case "InvalidCredential"
                        Return "رُفض الدخول: بيانات الاعتماد لم تطابق مستخدمًا؛ الهوية مجهولة ولم تُحفظ بيانات الاعتماد."
                    Case "UserNotAllowed"
                        Return "رُفض الدخول: المستخدم المحدد غير مسموح له بالدخول؛ لم تبدأ جلسة."
                    Case "NoOpenPeriod"
                        Return "رُفض فتح واجهة البيع: المستخدم المحدد لا يملك وردية مفتوحة والوردية مطلوبة؛ لم تتغير الجلسة السابقة."
                    Case "SalesPermissionDenied"
                        Return "رُفض فتح واجهة البيع: المستخدم المحدد لا يملك صلاحية المبيعات؛ لم تتغير الجلسة السابقة."
                End Select
                Throw New ArgumentException("سبب رفض الدخول غير معتمد.")
            Case Else : Throw New ArgumentException("قالب مراقبة غير معتمد.")
        End Select
    End Function

    Private Shared Function DocumentLabel(entity As String) As String
        Select Case entity
            Case "GeneralExpense" : Return "مستند مصروفات عامة"
            Case "PurchaseInvoice" : Return "فاتورة مشتريات"
            Case "WasteDocument" : Return "إذن تالف"
            Case "StockCount" : Return "فاتورة جرد"
            Case "SalesReturn" : Return "إرجاع مبيعات"
            Case "PurchaseReturn" : Return "إرجاع مشتريات"
            Case "StockTransfer" : Return "إذن تحويل أصناف"
            Case "AutoManufacturing" : Return "أمر تصنيع آلي"
            Case "StockIssue" : Return "إذن صرف مخزون"
            Case "ManualManufacturing" : Return "أمر تصنيع يدوي"
            Case "StockReceipt" : Return "إذن استلام مخزون"
            Case Else : Throw New ArgumentException("نوع مستند غير معتمد للقالب.")
        End Select
    End Function

    Private Shared Function SettlementDescription(request As AuditOperationRequest, item As AuditEventRequest, deleted As Boolean) As String
        Return If(deleted, "حذف", "إضافة") & " سطر فرق تسوية؛ المستند " & If(item.DocumentNumber, "[دون رقم عرض]") &
            " (T_ID=" & request.EntityId & ")؛ السطر " & item.EntityId &
            "؛ رقم الصنف " & FieldText(item, "SettlementLine", "ItemId", deleted) &
            "؛ رقم المخزن " & FieldText(item, "SettlementLine", "StoreId", deleted) &
            "؛ كمية الفرق المخزنة " & FieldText(item, "SettlementLine", "StoredQuantity", deleted) &
            "؛ الحمولة " & FieldText(item, "SettlementLine", "UnitCargo", deleted) &
            "؛ التكلفة المحفوظة " & FieldText(item, "SettlementLine", "Cost", deleted) &
            "؛ إجمالي السطر " & FieldText(item, "SettlementLine", "Total", deleted) &
            "؛ إجمالي المستند من " & FieldText(item, "Settlement", "DocumentTotal", True) &
            " إلى " & FieldText(item, "Settlement", "DocumentTotal", False) &
            ". بيانات السطر " & If(deleted, "قبل الحذف", "بعد الإضافة") & " محفوظة بالتفاصيل؛ فرق التسوية ليس رصيد المخزن النهائي."
    End Function

    Private Shared Function FieldText(item As AuditEventRequest, entityType As String, code As String, before As Boolean) As String
        For Each change In item.Changes
            If change.EntityType = entityType AndAlso change.FieldCode = code Then
                Dim value = If(before, change.Before, change.After)
                Select Case value.State
                    Case AuditValueState.Present : Return value.Text
                    Case AuditValueState.SqlNull : Return "NULL"
                    Case AuditValueState.Absent : Return "غير موجود"
                End Select
            End If
        Next
        Return "غير ملتقط"
    End Function
End Class
