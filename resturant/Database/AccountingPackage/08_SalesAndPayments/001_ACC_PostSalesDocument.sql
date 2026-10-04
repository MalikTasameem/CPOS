SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.ACC_PostSalesDocument', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.ACC_PostSalesDocument AS RETURN 0;');
GO

ALTER PROCEDURE dbo.ACC_PostSalesDocument
    @DocumentKind      TINYINT,
    @SourceTable       SYSNAME,
    @SourceKey         NVARCHAR(100),
    @SourceId          BIGINT = NULL,
    @DocumentNumber    NVARCHAR(50),
    @DocumentDate      DATETIME,
    @PartyAccountCode  VARCHAR(40),
    @GrossTotal        NUMERIC(18,3),
    @DiscountAmount    NUMERIC(18,3) = 0,
    @NetAmount         NUMERIC(18,3),
    @CurrencyId        INT = 1,
    @ExchangeRate      NUMERIC(18,3) = 1,
    @UserId            INT,
    @Lines             dbo.ACC_SALES_LINE_TYPE READONLY,
    @NewJournalId      INT OUTPUT,
    @JournalNumber     VARCHAR(50) OUTPUT,
    @ErrorMessage      NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @NewJournalId = NULL;
    SET @JournalNumber = NULL;
    SET @ErrorMessage = NULL;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @DocumentKind NOT IN (1, 2)
            RAISERROR(N'DocumentKind must be 1 for sale or 2 for sales return.', 16, 1);

        IF @GrossTotal <= 0 OR @NetAmount < 0 OR @DiscountAmount < 0
            RAISERROR(N'قيم الفاتورة غير صالحة.', 16, 1);

        IF ROUND(@GrossTotal - @DiscountAmount, 3) <> ROUND(@NetAmount, 3)
            RAISERROR(N'الصافي لا يساوي الإجمالي ناقص الخصم.', 16, 1);

        IF @DocumentKind = 2 AND @DiscountAmount <> 0
            RAISERROR(N'خصم رأس مرتجع المبيعات غير مدعوم في الإصدار الحالي من العقد.', 16, 1);

        IF NOT EXISTS (SELECT 1 FROM @Lines)
            RAISERROR(N'لا توجد تفاصيل للمستند.', 16, 1);

        IF ROUND((SELECT SUM(LineGross) FROM @Lines), 3) <> ROUND(@GrossTotal, 3)
            RAISERROR(N'مجموع تفاصيل المستند لا يساوي الإجمالي.', 16, 1);

        IF @PartyAccountCode IS NULL OR LTRIM(RTRIM(@PartyAccountCode)) = ''
            RAISERROR(N'العميل غير مربوط بحساب محاسبي.', 16, 1);

        IF EXISTS
        (
            SELECT 1 FROM @Lines
            WHERE InventoryAccountCode IS NULL
               OR LTRIM(RTRIM(InventoryAccountCode)) = ''
        )
            RAISERROR(N'يوجد مخزن غير مربوط بحساب مخزون.', 16, 1);

        EXEC dbo.ACC_SYSTEM_ACCOUNT_VALIDATE_FOR_POSTING;

        DECLARE @SalesAccountCode VARCHAR(40);
        DECLARE @SalesReturnAccountCode VARCHAR(40);
        DECLARE @DiscountAllowedAccountCode VARCHAR(40);
        DECLARE @COGSAccountCode VARCHAR(40);

        SELECT @SalesAccountCode = A.ACC_CODE
        FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES AS T
        INNER JOIN dbo.ACC_SYSTEM_ACCOUNT_LINKS AS L
            ON L.SystemAccountTypeID = T.SystemAccountTypeID AND L.IsActive = 1
        INNER JOIN dbo.ACCOUNTS_TREE AS A ON A.T_ID = L.ACC_T_ID
        WHERE T.AccountKey = N'SALES_REVENUE' AND T.IsActive = 1;

        SELECT @SalesReturnAccountCode = A.ACC_CODE
        FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES AS T
        INNER JOIN dbo.ACC_SYSTEM_ACCOUNT_LINKS AS L
            ON L.SystemAccountTypeID = T.SystemAccountTypeID AND L.IsActive = 1
        INNER JOIN dbo.ACCOUNTS_TREE AS A ON A.T_ID = L.ACC_T_ID
        WHERE T.AccountKey = N'SALES_RETURN' AND T.IsActive = 1;

        SELECT @DiscountAllowedAccountCode = A.ACC_CODE
        FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES AS T
        INNER JOIN dbo.ACC_SYSTEM_ACCOUNT_LINKS AS L
            ON L.SystemAccountTypeID = T.SystemAccountTypeID AND L.IsActive = 1
        INNER JOIN dbo.ACCOUNTS_TREE AS A ON A.T_ID = L.ACC_T_ID
        WHERE T.AccountKey = N'DISCOUNT_ALLOWED' AND T.IsActive = 1;

        SELECT @COGSAccountCode = A.ACC_CODE
        FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES AS T
        INNER JOIN dbo.ACC_SYSTEM_ACCOUNT_LINKS AS L
            ON L.SystemAccountTypeID = T.SystemAccountTypeID AND L.IsActive = 1
        INNER JOIN dbo.ACCOUNTS_TREE AS A ON A.T_ID = L.ACC_T_ID
        WHERE T.AccountKey = N'COGS' AND T.IsActive = 1;

        DECLARE @Rows dbo.ACC_BALANCE_BULK_TYPE;
        DECLARE @TotalCost NUMERIC(18,3);
        DECLARE @JournalNotes NVARCHAR(MAX);

        SELECT @TotalCost = SUM(ROUND(Quantity * UnitCost, 3)) FROM @Lines;
        SET @JournalNotes = CASE WHEN @DocumentKind = 1
            THEN N'فاتورة مبيعات رقم '
            ELSE N'مرتجع مبيعات رقم ' END + ISNULL(@DocumentNumber, N'');

        IF @DocumentKind = 1
        BEGIN
            INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
            VALUES (1, @PartyAccountCode, NULL, @NetAmount, @CurrencyId, N'طرف العميل', @DocumentNumber, NULL);

            IF @DiscountAmount > 0
                INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
                VALUES (2, @DiscountAllowedAccountCode, NULL, @DiscountAmount, @CurrencyId, N'خصم مسموح', @DocumentNumber, NULL);

            INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
            SELECT
                100 + ROW_NUMBER() OVER (ORDER BY ISNULL(RevenueAccountCode, @SalesAccountCode)),
                ISNULL(RevenueAccountCode, @SalesAccountCode),
                SUM(LineGross), NULL, @CurrencyId, N'إيراد المبيعات', @DocumentNumber, CostCenterId
            FROM @Lines
            GROUP BY ISNULL(RevenueAccountCode, @SalesAccountCode), CostCenterId;

            IF @TotalCost > 0
            BEGIN
                INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
                SELECT
                    200 + ROW_NUMBER() OVER (ORDER BY ISNULL(COGSAccountCode, @COGSAccountCode)),
                    ISNULL(COGSAccountCode, @COGSAccountCode),
                    NULL, SUM(ROUND(Quantity * UnitCost, 3)), @CurrencyId,
                    N'تكلفة البضاعة المباعة', @DocumentNumber, CostCenterId
                FROM @Lines
                WHERE Quantity * UnitCost > 0
                GROUP BY ISNULL(COGSAccountCode, @COGSAccountCode), CostCenterId;

                INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
                SELECT
                    300 + ROW_NUMBER() OVER (ORDER BY InventoryAccountCode),
                    InventoryAccountCode,
                    SUM(ROUND(Quantity * UnitCost, 3)), NULL, @CurrencyId,
                    N'خروج مخزون بسبب البيع', @DocumentNumber, CostCenterId
                FROM @Lines
                WHERE Quantity * UnitCost > 0
                GROUP BY InventoryAccountCode, CostCenterId;
            END;
        END
        ELSE
        BEGIN
            INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
            SELECT
                100 + ROW_NUMBER() OVER (ORDER BY ISNULL(SalesReturnAccountCode, @SalesReturnAccountCode)),
                ISNULL(SalesReturnAccountCode, @SalesReturnAccountCode),
                NULL, SUM(LineGross), @CurrencyId, N'مردودات المبيعات', @DocumentNumber, CostCenterId
            FROM @Lines
            GROUP BY ISNULL(SalesReturnAccountCode, @SalesReturnAccountCode), CostCenterId;

            INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
            VALUES (1, @PartyAccountCode, @NetAmount, NULL, @CurrencyId, N'تخفيض مديونية العميل', @DocumentNumber, NULL);

            IF @TotalCost > 0
            BEGIN
                INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
                SELECT
                    200 + ROW_NUMBER() OVER (ORDER BY InventoryAccountCode),
                    InventoryAccountCode,
                    NULL, SUM(ROUND(Quantity * UnitCost, 3)), @CurrencyId,
                    N'عودة مخزون من مرتجع المبيعات', @DocumentNumber, CostCenterId
                FROM @Lines
                WHERE Quantity * UnitCost > 0
                GROUP BY InventoryAccountCode, CostCenterId;

                INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
                SELECT
                    300 + ROW_NUMBER() OVER (ORDER BY ISNULL(COGSAccountCode, @COGSAccountCode)),
                    ISNULL(COGSAccountCode, @COGSAccountCode),
                    SUM(ROUND(Quantity * UnitCost, 3)), NULL, @CurrencyId,
                    N'عكس تكلفة البضاعة المباعة', @DocumentNumber, CostCenterId
                FROM @Lines
                WHERE Quantity * UnitCost > 0
                GROUP BY ISNULL(COGSAccountCode, @COGSAccountCode), CostCenterId;
            END;
        END;

        EXEC dbo.ACC_Journal_Save
            @JournalDate = @DocumentDate,
            @UserId = @UserId,
            @Notes = @JournalNotes,
            @CurrencyId = @CurrencyId,
            @ExchangeRate = @ExchangeRate,
            @SourceType = @DocumentKind,
            @SourceId = @SourceId,
            @SourceTable = @SourceTable,
            @SourceKey = @SourceKey,
            @Rows = @Rows,
            @NewJournalId = @NewJournalId OUTPUT,
            @JournalNumber = @JournalNumber OUTPUT,
            @ErrorMessage = @ErrorMessage OUTPUT;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        SET @ErrorMessage = LEFT(N'[ACC_PostSalesDocument] ' + ERROR_MESSAGE(), 500);
        SET @NewJournalId = NULL;
        SET @JournalNumber = NULL;
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH;
END;
GO

