# CPOS Portable Accounting Package

حزمة لنقل النواة المحاسبية إلى قاعدة بيانات واحدة تضم مشروع مبيعات آخر، من دون نسخ جداول مبيعات CPOS.

## الحالة الحالية

**الإصدار الجاري:** `0.4.0-lifecycle-monitoring`  
**التوافق:** SQL Server 2014 / Compatibility Level 120 أو أحدث  
**الحالة:** النواة والترحيل ودورة التعديل والإلغاء ومراقبة القيود مكتملة أوليا — لا تعتمد إنتاجيا قبل اكتمال التقارير والـAdapter الخاص بمخطط المطور واختبارات القبول.

## القرارات الأساسية

- قاعدة بيانات واحدة.
- جداول المبيعات والعملاء والمخزون تبقى جداول مشروع المطور.
- جداول دفتر الأستاذ والربط والتدقيق تنقل كنواة مستقلة.
- حقول الحساب تضاف مباشرة إلى جداول الكيانات.
- حقول دورة حياة الترحيل تضاف مباشرة إلى رؤوس المعاملات.
- لا كتابة مباشرة من المبيعات في `ACC_BALANCE_MASTER` أو `ACC_BALANCE`.
- لا تنسخ إجراءات CPOS المرتبطة بجداوله التشغيلية؛ تستبدل بعقد ترحيل محايد.

## ترتيب ملفات الإصدار التأسيسي

1. `01_Prerequisites/001_Preflight.sql`
2. `02_CoreTables/001_Core_Ledger_Tables.sql`
3. `02_CoreTables/002_Accounting_Link_And_Audit_Tables.sql`
4. `03_PostingContracts/001_Posting_Table_Types.sql`
5. `04_SeedData/001_System_Account_Types.sql`
6. `05_SystemAccounts/001_System_Account_Validation.sql`
7. `02_CoreTables/003_Portable_Settings_And_Periods.sql`
8. `06_JournalEngine/001_ACC_GetNextIdBlock.sql`
9. `06_JournalEngine/002_ACC_GetNextJournalNumber.sql`
10. `06_JournalEngine/003_ACC_Journal_Save.sql`
11. `06_JournalEngine/004_ACC_Journal_Reverse.sql`
12. `08_SalesAndPayments/001_ACC_PostSalesDocument.sql`
13. `08_SalesAndPayments/002_ACC_PostReceipt.sql`
14. `09_PurchasesAndExpenses/001_ACC_PostPurchaseDocument.sql`
15. `09_PurchasesAndExpenses/002_ACC_PostExpenseDocument.sql`
16. `10_Inventory/001_ACC_PostInventoryTransfer.sql`
17. `10_Inventory/002_ACC_PostInventoryAdjustment.sql`
18. `10_Inventory/003_ACC_PostInventoryIssue.sql`
19. `11_EditCancelReverse/001_ACC_Source_Repost.sql`
20. `11_EditCancelReverse/002_ACC_Source_CancelPosting.sql`
21. `12_Monitoring/001_ACC_Journal_Source_Status.sql`
22. `16_Tests/001_Validate_Foundation.sql`

بعد نجاح فحص الأساس وعلى **قاعدة اختبار جديدة فقط**:

23. `16_Tests/002_Test_Sales_And_Reversal.sql`

الاختبار الثاني يعمل داخل معاملة وينفذ `ROLLBACK` في النهاية، ويرفض التشغيل إذا وجد روابط حسابات نظامية فعالة مسبقا.

كل ملف يستخدم `SET XACT_ABORT ON` ومعاملة عند وجود كتابة. لا تشغل الملفات على قاعدة إنتاج دون نسخة احتياطية واختبار استعادة.

## ما تمثله هذه المرحلة

- مخطط دفتر اليومية الأساسي.
- الحسابات النظامية.
- ربط الكيانات بالدليل.
- سجلات إعادة التقييد والإلغاء.
- عقود TVP الأساسية لبناء القيود والمدفوعات.
- إعادة ترحيل المصدر بعد التعديل بطريقة «عكس القيد القديم ثم إنشاء قيد جديد».
- إلغاء المصدر محاسبيا بواسطة قيد عكسي، دون حذف تاريخ الحركة.
- View عام لمتابعة القيد الفعال والقيد المعكوس.
- قالب Adapter لدورة المصدر ومراقبة الفواتير غير المرحلة أو غير المتزامنة.
- اختبار سلامة التثبيت.

## ما لم يكتمل بعد

- واجهات إدارة الفترات المالية وإعدادات الترقيم.
- اختبارات تشغيلية للمشتريات والمصروفات وبقية المخزون.
- قائمة الدخل والمركز المالي وميزان المراجعة المحمول.
- Adapter خاص بمخطط المطور.
- Rollback واختبارات السيناريوهات المحاسبية.

وجود هذا المجلد لا يعني أن الحزمة أصبحت جاهزة للتسليم النهائي. يوضح `PACKAGE_MANIFEST.md` حالة كل مكون.

## التوثيق المرجعي

- `../../Documentation/Sales_Accounting_Integration.md`
- `../../Documentation/Accounting_Integration_Field_Dictionary.md`
- `00_Documentation/POSTING_RULES.md`
- `00_Documentation/SOURCE_LIFECYCLE.md`
