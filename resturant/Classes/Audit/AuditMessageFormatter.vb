Option Strict On

Friend NotInheritable Class AuditMessageFormatter
    Friend Shared Function Format(request As AuditOperationRequest, item As AuditEventRequest) As String
        Select Case item.EventCode
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
