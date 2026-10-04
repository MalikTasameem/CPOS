SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.ACC_PostPurchaseDocument', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.ACC_PostPurchaseDocument AS RETURN 0;');
GO

ALTER PROCEDURE dbo.ACC_PostPurchaseDocument
    @DocumentKind       TINYINT,
    @SourceTable        SYSNAME,
    @SourceKey          NVARCHAR(100),
    @SourceId           BIGINT = NULL,
    @DocumentNumber     NVARCHAR(50),
    @DocumentDate       DATETIME,
    @SupplierAccountCode VARCHAR(40),
    @GrossTotal         NUMERIC(18,3),
    @DiscountAmount     NUMERIC(18,3) = 0,
    @NetAmount          NUMERIC(18,3),
    @CurrencyId         INT = 1,
    @ExchangeRate       NUMERIC(18,3) = 1,
    @UserId             INT,
    @Lines              dbo.ACC_PURCHASE_LINE_TYPE READONLY,
    @NewJournalId       INT OUTPUT,
    @JournalNumber      VARCHAR(50) OUTPUT,
    @ErrorMessage       NVARCHAR(500) OUTPUT
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
            RAISERROR(N'DocumentKind must be 1 for purchase or 2 for purchase return.', 16, 1);

        IF @GrossTotal <= 0 OR @DiscountAmount < 0 OR @NetAmount < 0
            RAISERROR(N'قيم مستند المشتريات غير صالحة.', 16, 1);

        IF ROUND(@GrossTotal - @DiscountAmount, 3) <> ROUND(@NetAmount, 3)
            RAISERROR(N'صافي المشتريات لا يساوي الإجمالي ناقص الخصم.', 16, 1);

        IF @DocumentKind = 2 AND @DiscountAmount <> 0
            RAISERROR(N'خصم رأس مرتجع المشتريات غير مدعوم في الإصدار الحالي.', 16, 1);

        IF @SupplierAccountCode IS NULL OR LTRIM(RTRIM(@SupplierAccountCode)) = ''
            RAISERROR(N'المورد غير مربوط بحساب محاسبي.', 16, 1);

        IF NOT EXISTS (SELECT 1 FROM @Lines)
            RAISERROR(N'لا توجد تفاصيل للمشتريات.', 16, 1);

        IF ROUND((SELECT SUM(LineAmount) FROM @Lines), 3) <> ROUND(@GrossTotal, 3)
            RAISERROR(N'مجموع تفاصيل المشتريات لا يساوي الإجمالي.', 16, 1);

        IF EXISTS
        (
            SELECT 1 FROM @Lines
            WHERE InventoryAccountCode IS NULL
               OR LTRIM(RTRIM(InventoryAccountCode)) = ''
        )
            RAISERROR(N'يوجد مخزن مشتريات غير مربوط بحساب مخزون.', 16, 1);

        EXEC dbo.ACC_SYSTEM_ACCOUNT_VALIDATE_FOR_POSTING;

        DECLARE @PurchaseAccountCode VARCHAR(40);
        DECLARE @PurchaseReturnAccountCode VARCHAR(40);
        DECLARE @DiscountReceivedAccountCode VARCHAR(40);
        DECLARE @Rows dbo.ACC_BALANCE_BULK_TYPE;
        DECLARE @JournalNotes NVARCHAR(MAX);
        DECLARE @InventoryAccountingMode VARCHAR(10);
        DECLARE @PostingSourceType INT;

        SELECT @InventoryAccountingMode = InventoryAccountingMode
        FROM dbo.ACC_PORTABLE_SETTINGS
        WHERE SettingsId = 1;

        SET @PostingSourceType = CASE WHEN @DocumentKind = 1 THEN 7 ELSE 11 END;

        SELECT @PurchaseAccountCode = A.ACC_CODE
        FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES AS T
        INNER JOIN dbo.ACC_SYSTEM_ACCOUNT_LINKS AS L
            ON L.SystemAccountTypeID = T.SystemAccountTypeID AND L.IsActive = 1
        INNER JOIN dbo.ACCOUNTS_TREE AS A ON A.T_ID = L.ACC_T_ID
        WHERE T.AccountKey = N'PURCHASES' AND T.IsActive = 1;

        SELECT @PurchaseReturnAccountCode = A.ACC_CODE
        FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES AS T
        INNER JOIN dbo.ACC_SYSTEM_ACCOUNT_LINKS AS L
            ON L.SystemAccountTypeID = T.SystemAccountTypeID AND L.IsActive = 1
        INNER JOIN dbo.ACCOUNTS_TREE AS A ON A.T_ID = L.ACC_T_ID
        WHERE T.AccountKey = N'PURCHASE_RETURN' AND T.IsActive = 1;

        SELECT @DiscountReceivedAccountCode = A.ACC_CODE
        FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES AS T
        INNER JOIN dbo.ACC_SYSTEM_ACCOUNT_LINKS AS L
            ON L.SystemAccountTypeID = T.SystemAccountTypeID AND L.IsActive = 1
        INNER JOIN dbo.ACCOUNTS_TREE AS A ON A.T_ID = L.ACC_T_ID
        WHERE T.AccountKey = N'DISCOUNT_RECEIVED' AND T.IsActive = 1;

        SET @JournalNotes = CASE WHEN @DocumentKind = 1
            THEN N'فاتورة مشتريات رقم ' ELSE N'مرتجع مشتريات رقم ' END
            + ISNULL(@DocumentNumber, N'');

        IF @DocumentKind = 1
        BEGIN
            INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
            SELECT
                100 + ROW_NUMBER() OVER (ORDER BY InventoryAccountCode),
                CASE
                    WHEN PurchaseAccountCode IS NOT NULL THEN PurchaseAccountCode
                    WHEN @InventoryAccountingMode = 'PERIODIC' THEN @PurchaseAccountCode
                    ELSE InventoryAccountCode
                END,
                NULL, SUM(LineAmount), @CurrencyId, N'مشتريات/إدخال مخزون', @DocumentNumber, CostCenterId
            FROM @Lines
            GROUP BY CASE
                    WHEN PurchaseAccountCode IS NOT NULL THEN PurchaseAccountCode
                    WHEN @InventoryAccountingMode = 'PERIODIC' THEN @PurchaseAccountCode
                    ELSE InventoryAccountCode
                END, CostCenterId;

            INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
            VALUES (1, @SupplierAccountCode, @NetAmount, NULL, @CurrencyId, N'استحقاق المورد', @DocumentNumber, NULL);

            IF @DiscountAmount > 0
                INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
                VALUES (2, @DiscountReceivedAccountCode, @DiscountAmount, NULL, @CurrencyId, N'خصم مكتسب', @DocumentNumber, NULL);
        END
        ELSE
        BEGIN
            INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
            VALUES (1, @SupplierAccountCode, NULL, @NetAmount, @CurrencyId, N'تخفيض مستحقات المورد', @DocumentNumber, NULL);

            INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
            SELECT
                100 + ROW_NUMBER() OVER (ORDER BY InventoryAccountCode),
                CASE
                    WHEN PurchaseReturnAccountCode IS NOT NULL THEN PurchaseReturnAccountCode
                    WHEN @InventoryAccountingMode = 'PERIODIC' THEN @PurchaseReturnAccountCode
                    ELSE InventoryAccountCode
                END,
                SUM(LineAmount), NULL, @CurrencyId, N'مرتجع مشتريات/خروج مخزون', @DocumentNumber, CostCenterId
            FROM @Lines
            GROUP BY CASE
                    WHEN PurchaseReturnAccountCode IS NOT NULL THEN PurchaseReturnAccountCode
                    WHEN @InventoryAccountingMode = 'PERIODIC' THEN @PurchaseReturnAccountCode
                    ELSE InventoryAccountCode
                END, CostCenterId;
        END;

        EXEC dbo.ACC_Journal_Save
            @JournalDate = @DocumentDate,
            @UserId = @UserId,
            @Notes = @JournalNotes,
            @CurrencyId = @CurrencyId,
            @ExchangeRate = @ExchangeRate,
            @SourceType = @PostingSourceType,
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
        SET @ErrorMessage = LEFT(N'[ACC_PostPurchaseDocument] ' + ERROR_MESSAGE(), 500);
        SET @NewJournalId = NULL;
        SET @JournalNumber = NULL;
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH;
END;
GO
