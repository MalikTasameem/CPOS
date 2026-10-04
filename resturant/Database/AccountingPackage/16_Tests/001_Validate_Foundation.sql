SET NOCOUNT ON;

DECLARE @Errors TABLE
(
    ErrorCode VARCHAR(50) NOT NULL,
    ErrorMessage NVARCHAR(500) NOT NULL
);

IF OBJECT_ID(N'dbo.ACCOUNTS_TREE', N'U') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TABLE', N'الجدول ACCOUNTS_TREE غير موجود.');

IF OBJECT_ID(N'dbo.ACC_BALANCE_MASTER', N'U') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TABLE', N'الجدول ACC_BALANCE_MASTER غير موجود.');

IF OBJECT_ID(N'dbo.ACC_BALANCE', N'U') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TABLE', N'الجدول ACC_BALANCE غير موجود.');

IF OBJECT_ID(N'dbo.ACC_SYSTEM_ACCOUNT_TYPES', N'U') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TABLE', N'الجدول ACC_SYSTEM_ACCOUNT_TYPES غير موجود.');

IF OBJECT_ID(N'dbo.ACC_SYSTEM_ACCOUNT_LINKS', N'U') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TABLE', N'الجدول ACC_SYSTEM_ACCOUNT_LINKS غير موجود.');

IF OBJECT_ID(N'dbo.ACC_ENTITY_RULES', N'U') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TABLE', N'الجدول ACC_ENTITY_RULES غير موجود.');

IF OBJECT_ID(N'dbo.ACC_ENTITY_LINKS', N'U') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TABLE', N'الجدول ACC_ENTITY_LINKS غير موجود.');

IF TYPE_ID(N'dbo.ACC_BALANCE_BULK_TYPE') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TYPE', N'النوع ACC_BALANCE_BULK_TYPE غير موجود.');

IF TYPE_ID(N'dbo.SalePaymentAllocationType') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TYPE', N'النوع SalePaymentAllocationType غير موجود.');

IF TYPE_ID(N'dbo.ACC_SALES_LINE_TYPE') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TYPE', N'النوع ACC_SALES_LINE_TYPE غير موجود.');

IF TYPE_ID(N'dbo.ACC_PURCHASE_LINE_TYPE') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TYPE', N'النوع ACC_PURCHASE_LINE_TYPE غير موجود.');

IF TYPE_ID(N'dbo.ACC_EXPENSE_LINE_TYPE') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TYPE', N'النوع ACC_EXPENSE_LINE_TYPE غير موجود.');

IF TYPE_ID(N'dbo.ACC_INVENTORY_TRANSFER_LINE_TYPE') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TYPE', N'النوع ACC_INVENTORY_TRANSFER_LINE_TYPE غير موجود.');

IF TYPE_ID(N'dbo.ACC_INVENTORY_VALUE_LINE_TYPE') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TYPE', N'النوع ACC_INVENTORY_VALUE_LINE_TYPE غير موجود.');

IF OBJECT_ID(N'dbo.ACC_PORTABLE_SETTINGS', N'U') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TABLE', N'الجدول ACC_PORTABLE_SETTINGS غير موجود.');

IF OBJECT_ID(N'dbo.ACC_FISCAL_PERIODS', N'U') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TABLE', N'الجدول ACC_FISCAL_PERIODS غير موجود.');

IF OBJECT_ID(N'dbo.ACC_INTERNAL_SEQUENCES', N'U') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TABLE', N'الجدول ACC_INTERNAL_SEQUENCES غير موجود.');

IF OBJECT_ID(N'dbo.ACC_JOURNAL_COUNTERS', N'U') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TABLE', N'الجدول ACC_JOURNAL_COUNTERS غير موجود.');

IF OBJECT_ID(N'dbo.ACC_GetNextIdBlock', N'P') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_PROCEDURE', N'الإجراء ACC_GetNextIdBlock غير موجود.');

IF OBJECT_ID(N'dbo.ACC_GetNextJournalNumber', N'P') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_PROCEDURE', N'الإجراء ACC_GetNextJournalNumber غير موجود.');

IF OBJECT_ID(N'dbo.ACC_Journal_Save', N'P') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_PROCEDURE', N'الإجراء ACC_Journal_Save غير موجود.');

IF OBJECT_ID(N'dbo.ACC_Journal_Reverse', N'P') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_PROCEDURE', N'الإجراء ACC_Journal_Reverse غير موجود.');

IF OBJECT_ID(N'dbo.ACC_SYSTEM_ACCOUNT_VALIDATE_FOR_POSTING', N'P') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_PROCEDURE', N'الإجراء ACC_SYSTEM_ACCOUNT_VALIDATE_FOR_POSTING غير موجود.');

IF OBJECT_ID(N'dbo.ACC_PostSalesDocument', N'P') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_PROCEDURE', N'الإجراء ACC_PostSalesDocument غير موجود.');

IF OBJECT_ID(N'dbo.ACC_PostReceipt', N'P') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_PROCEDURE', N'الإجراء ACC_PostReceipt غير موجود.');

IF OBJECT_ID(N'dbo.ACC_PostPurchaseDocument', N'P') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_PROCEDURE', N'الإجراء ACC_PostPurchaseDocument غير موجود.');

IF OBJECT_ID(N'dbo.ACC_PostExpenseDocument', N'P') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_PROCEDURE', N'الإجراء ACC_PostExpenseDocument غير موجود.');

IF OBJECT_ID(N'dbo.ACC_PostInventoryTransfer', N'P') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_PROCEDURE', N'الإجراء ACC_PostInventoryTransfer غير موجود.');

IF OBJECT_ID(N'dbo.ACC_PostInventoryAdjustment', N'P') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_PROCEDURE', N'الإجراء ACC_PostInventoryAdjustment غير موجود.');

IF OBJECT_ID(N'dbo.ACC_PostInventoryIssue', N'P') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_PROCEDURE', N'الإجراء ACC_PostInventoryIssue غير موجود.');

IF OBJECT_ID(N'dbo.ACC_Source_Repost', N'P') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_PROCEDURE', N'الإجراء ACC_Source_Repost غير موجود.');

IF OBJECT_ID(N'dbo.ACC_Source_CancelPosting', N'P') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_PROCEDURE', N'الإجراء ACC_Source_CancelPosting غير موجود.');

IF OBJECT_ID(N'dbo.VW_ACC_SYSTEM_ACCOUNT_LINKS_STATUS', N'V') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_VIEW', N'العرض VW_ACC_SYSTEM_ACCOUNT_LINKS_STATUS غير موجود.');

IF OBJECT_ID(N'dbo.VW_ACC_SYSTEM_ACCOUNT_DUPLICATES', N'V') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_VIEW', N'العرض VW_ACC_SYSTEM_ACCOUNT_DUPLICATES غير موجود.');

IF OBJECT_ID(N'dbo.ACC_VW_JOURNAL_SOURCE_STATUS', N'V') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_VIEW', N'العرض ACC_VW_JOURNAL_SOURCE_STATUS غير موجود.');

IF OBJECT_ID(N'dbo.ACC_InvoiceRepostLog', N'U') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TABLE', N'الجدول ACC_InvoiceRepostLog غير موجود.');

IF OBJECT_ID(N'dbo.ACC_InvoiceCancelLog', N'U') IS NULL
    INSERT INTO @Errors VALUES ('MISSING_TABLE', N'الجدول ACC_InvoiceCancelLog غير موجود.');

IF OBJECT_ID(N'dbo.ACC_SYSTEM_ACCOUNT_TYPES', N'U') IS NOT NULL
AND (SELECT COUNT(*) FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES WHERE IsActive = 1) < 22
    INSERT INTO @Errors VALUES ('SEED_INCOMPLETE', N'أنواع الحسابات النظامية الفعالة أقل من 22.');

IF OBJECT_ID(N'dbo.ACCOUNTS_TREE', N'U') IS NOT NULL
AND EXISTS
(
    SELECT ACC_CODE
    FROM dbo.ACCOUNTS_TREE
    GROUP BY ACC_CODE
    HAVING COUNT(*) > 1
)
    INSERT INTO @Errors VALUES ('DUPLICATE_ACCOUNT_CODE', N'يوجد كود حساب مكرر في الدليل.');

IF EXISTS (SELECT 1 FROM @Errors)
BEGIN
    SELECT ErrorCode, ErrorMessage FROM @Errors ORDER BY ErrorCode, ErrorMessage;
    RAISERROR(N'فشل فحص تأسيس الحزمة المحاسبية. راجع قائمة الأخطاء.', 16, 1);
    RETURN;
END;

SELECT
    N'PASS' AS ValidationStatus,
    DB_NAME() AS DatabaseName,
    (SELECT COUNT(*) FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES WHERE IsActive = 1) AS ActiveSystemAccountTypes;
