SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @Seed TABLE
    (
        AccountKey           NVARCHAR(100) NOT NULL,
        AccountNameAr        NVARCHAR(200) NOT NULL,
        AccountNameEn        NVARCHAR(200) NULL,
        Required             BIT NOT NULL,
        AllowSameAccount     BIT NOT NULL,
        MustBeLeaf           BIT NOT NULL,
        Expected_ACC_NATURAL CHAR(1) NULL,
        Notes                NVARCHAR(500) NULL
    );

    INSERT INTO @Seed
    (
        AccountKey, AccountNameAr, AccountNameEn, Required,
        AllowSameAccount, MustBeLeaf, Expected_ACC_NATURAL, Notes
    )
    VALUES
        (N'SALES_REVENUE',              N'إيراد المبيعات',             N'Sales Revenue',              1, 0, 1, 'D', NULL),
        (N'SALES_RETURN',               N'مردودات المبيعات',           N'Sales Returns',              1, 0, 1, 'C', NULL),
        (N'PURCHASES',                  N'المشتريات',                   N'Purchases',                  1, 0, 1, 'C', NULL),
        (N'PURCHASE_RETURN',            N'مردودات المشتريات',          N'Purchase Returns',           1, 0, 1, 'D', NULL),
        (N'INVENTORY',                  N'المخزون',                     N'Inventory',                  0, 0, 1, 'C', NULL),
        (N'COGS',                       N'تكلفة البضاعة المباعة',       N'Cost of Goods Sold',         1, 0, 1, 'C', NULL),
        (N'CUSTOMER_CONTROL',           N'حساب رقابة العملاء',         N'Customer Control',           0, 0, 0, 'C', NULL),
        (N'SUPPLIER_CONTROL',           N'حساب رقابة الموردين',        N'Supplier Control',           0, 0, 0, 'D', NULL),
        (N'DISCOUNT_ALLOWED',           N'الخصم المسموح',              N'Discount Allowed',           1, 0, 1, 'C', NULL),
        (N'DISCOUNT_RECEIVED',          N'الخصم المكتسب',              N'Discount Received',          1, 0, 1, 'D', NULL),
        (N'VAT_OUTPUT',                 N'ضريبة المخرجات',             N'VAT Output',                 0, 0, 1, 'D', NULL),
        (N'VAT_INPUT',                  N'ضريبة المدخلات',             N'VAT Input',                  0, 0, 1, 'C', NULL),
        (N'ROUNDING_DIFF',              N'فروق التقريب',               N'Rounding Differences',       0, 0, 1, NULL, NULL),
        (N'PROFIT_LOSS',                N'الأرباح والخسائر',           N'Profit and Loss',            0, 0, 0, NULL, NULL),
        (N'CAPITAL',                    N'رأس المال',                  N'Capital',                    1, 0, 0, 'D', NULL),
        (N'DAMAGE_EXPENSE',             N'مصروف التلف',                N'Damage Expense',             1, 0, 1, 'C', NULL),
        (N'MANUFACTURING_EXPENSE',      N'مصروف التصنيع',              N'Manufacturing Expense',      0, 0, 1, 'C', N'يمكن استخدام COGS كبديل وفقا للسياسة الحالية.'),
        (N'INVENTORY_ADJUSTMENT_GAIN',  N'أرباح تسوية المخزون',        N'Inventory Adjustment Gain',  0, 1, 1, NULL, NULL),
        (N'INVENTORY_ADJUSTMENT_LOSS',  N'خسائر تسوية المخزون',        N'Inventory Adjustment Loss',  0, 1, 1, NULL, NULL),
        (N'GENERAL_EXPENSE',            N'المصروف العام',              N'General Expense',            0, 0, 1, 'C', NULL),
        (N'OPENING_BALANCES',           N'الأرصدة الافتتاحية',         N'Opening Balances',           1, 0, 1, NULL, NULL),
        (N'PAYROLL_WAGE_CAlCULATION',   N'احتساب الأجور',              N'Payroll Wage Calculation',   0, 0, 1, 'D', NULL);

    UPDATE T
    SET
        T.AccountNameAr = S.AccountNameAr,
        T.AccountNameEn = S.AccountNameEn,
        T.Required = S.Required,
        T.AllowSameAccount = S.AllowSameAccount,
        T.MustBeLeaf = S.MustBeLeaf,
        T.Expected_ACC_NATURAL = S.Expected_ACC_NATURAL,
        T.IsActive = 1,
        T.Notes = S.Notes
    FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES AS T
    INNER JOIN @Seed AS S ON S.AccountKey = T.AccountKey;

    INSERT INTO dbo.ACC_SYSTEM_ACCOUNT_TYPES
    (
        AccountKey, AccountNameAr, AccountNameEn, Required,
        AllowSameAccount, MustBeLeaf, Expected_ACC_NATURAL, IsActive, Notes
    )
    SELECT
        S.AccountKey, S.AccountNameAr, S.AccountNameEn, S.Required,
        S.AllowSameAccount, S.MustBeLeaf, S.Expected_ACC_NATURAL, 1, S.Notes
    FROM @Seed AS S
    WHERE NOT EXISTS
    (
        SELECT 1
        FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES AS T
        WHERE T.AccountKey = S.AccountKey
    );

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;

