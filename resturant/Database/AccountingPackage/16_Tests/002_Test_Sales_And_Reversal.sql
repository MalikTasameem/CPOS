SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    IF EXISTS (SELECT 1 FROM dbo.ACC_SYSTEM_ACCOUNT_LINKS WHERE IsActive = 1)
        RAISERROR(N'هذا الاختبار مخصص لقاعدة اختبار جديدة بلا روابط حسابات نظامية.', 16, 1);

    DECLARE @TestYear INT = 2098;
    DECLARE @TestDate DATETIME = '20980115';

    INSERT INTO dbo.ACC_FISCAL_PERIODS
    (
        FiscalYear, PeriodNo, PeriodName, DateFrom, DateTo, IsClosed
    )
    VALUES
    (
        @TestYear, 1, N'فترة اختبار', '20980101', '20980131', 0
    );

    INSERT INTO dbo.ACCOUNTS_TREE
    (
        T_ID, ACC_CODE, ACC_LEVEL, ACC_PARENT, ACC_NAME,
        ACC_NATURAL, is_Lock_Trans, AccountSourceType
    )
    VALUES
    (1900000000, 'ZZT', 1, '', N'جذر اختبار مؤقت', NULL, 0, 0),
    (1900000001, 'ZZT-CUSTOMER', 2, 'ZZT', N'عميل اختبار', 'C', 0, 0),
    (1900000002, 'ZZT-INVENTORY', 2, 'ZZT', N'مخزون اختبار', 'C', 0, 0);

    DECLARE @RequiredAccounts TABLE
    (
        RowNo INT IDENTITY(1,1),
        SystemAccountTypeID INT,
        ExpectedNatural CHAR(1)
    );

    INSERT INTO @RequiredAccounts(SystemAccountTypeID, ExpectedNatural)
    SELECT SystemAccountTypeID, Expected_ACC_NATURAL
    FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES
    WHERE IsActive = 1
      AND Required = 1;

    INSERT INTO dbo.ACCOUNTS_TREE
    (
        T_ID, ACC_CODE, ACC_LEVEL, ACC_PARENT, ACC_NAME,
        ACC_NATURAL, is_Lock_Trans, AccountSourceType
    )
    SELECT
        1900000100 + RowNo,
        'ZZT-SYS-' + RIGHT('00' + CONVERT(VARCHAR(2), RowNo), 2),
        2,
        'ZZT',
        N'حساب نظامي اختباري ' + CONVERT(NVARCHAR(10), RowNo),
        ExpectedNatural,
        0,
        0
    FROM @RequiredAccounts;

    INSERT INTO dbo.ACC_SYSTEM_ACCOUNT_LINKS
    (
        SystemAccountTypeID, ACC_T_ID, IsActive, CreatedBy
    )
    SELECT
        SystemAccountTypeID,
        1900000100 + RowNo,
        1,
        1
    FROM @RequiredAccounts;

    UPDATE dbo.ACC_INTERNAL_SEQUENCES
    SET NextValue = ISNULL((SELECT MAX(T_ID) FROM dbo.ACC_BALANCE_MASTER), 0) + 1
    WHERE SequenceName = N'ACC_BALANCE_MASTER';

    UPDATE dbo.ACC_INTERNAL_SEQUENCES
    SET NextValue = ISNULL((SELECT MAX(T_ID) FROM dbo.ACC_BALANCE), 0) + 1
    WHERE SequenceName = N'ACC_BALANCE';

    DECLARE @Lines dbo.ACC_SALES_LINE_TYPE;
    INSERT INTO @Lines
    (
        [LineNo], SourceLineKey, InventoryAccountCode,
        Quantity, LineGross, UnitCost, CurrencyId
    )
    VALUES
    (
        1, N'LINE-1', 'ZZT-INVENTORY', 2, 100, 30, 1
    );

    DECLARE @JournalId INT;
    DECLARE @JournalNumber VARCHAR(50);
    DECLARE @ErrorMessage NVARCHAR(500);

    EXEC dbo.ACC_PostSalesDocument
        @DocumentKind = 1,
        @SourceTable = N'TEST_SALES',
        @SourceKey = N'SALE-1',
        @SourceId = 1,
        @DocumentNumber = N'TEST-1',
        @DocumentDate = @TestDate,
        @PartyAccountCode = 'ZZT-CUSTOMER',
        @GrossTotal = 100,
        @DiscountAmount = 10,
        @NetAmount = 90,
        @CurrencyId = 1,
        @ExchangeRate = 1,
        @UserId = 1,
        @Lines = @Lines,
        @NewJournalId = @JournalId OUTPUT,
        @JournalNumber = @JournalNumber OUTPUT,
        @ErrorMessage = @ErrorMessage OUTPUT;

    IF @JournalId IS NULL
        RAISERROR(N'لم يتم إنشاء قيد البيع الاختباري.', 16, 1);

    IF
    (
        SELECT ROUND(SUM(ISNULL(DEBIT, 0)), 3)
        FROM dbo.ACC_BALANCE WHERE B_T_ID = @JournalId
    ) <> 160
        RAISERROR(N'إجمالي طرف DEBIT لقيد البيع لا يساوي 160.', 16, 1);

    IF
    (
        SELECT ROUND(SUM(ISNULL(CREDIT, 0)), 3)
        FROM dbo.ACC_BALANCE WHERE B_T_ID = @JournalId
    ) <> 160
        RAISERROR(N'إجمالي طرف CREDIT لقيد البيع لا يساوي 160.', 16, 1);

    DECLARE @ReversalJournalId INT;

    EXEC dbo.ACC_Journal_Reverse
        @JournalId = @JournalId,
        @ReverseDate = @TestDate,
        @UserId = 1,
        @Reason = N'اختبار العكس',
        @ReversalJournalId = @ReversalJournalId OUTPUT,
        @ErrorMessage = @ErrorMessage OUTPUT;

    IF @ReversalJournalId IS NULL
        RAISERROR(N'لم يتم إنشاء القيد العكسي الاختباري.', 16, 1);

    IF EXISTS
    (
        SELECT ACC_CODE, DEBIT, CREDIT
        FROM dbo.ACC_BALANCE
        WHERE B_T_ID = @JournalId
        EXCEPT
        SELECT ACC_CODE, CREDIT, DEBIT
        FROM dbo.ACC_BALANCE
        WHERE B_T_ID = @ReversalJournalId
    )
        RAISERROR(N'القيد العكسي لا يطابق أطراف القيد الأصلي.', 16, 1);

    SELECT
        N'PASS' AS TestStatus,
        @JournalId AS SaleJournalId,
        @ReversalJournalId AS ReversalJournalId,
        @JournalNumber AS SaleJournalNumber;

    ROLLBACK TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
