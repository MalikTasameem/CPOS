SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.ACC_PostReceipt', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.ACC_PostReceipt AS RETURN 0;');
GO

ALTER PROCEDURE dbo.ACC_PostReceipt
    @ReceiptKind         TINYINT,
    @SourceTable         SYSNAME,
    @SourceKey           NVARCHAR(100),
    @SourceId            BIGINT = NULL,
    @ReceiptNumber       NVARCHAR(50),
    @ReceiptDate         DATETIME,
    @PartyAccountCode    VARCHAR(40),
    @TreasuryAccountCode VARCHAR(40),
    @PureAmount          NUMERIC(18,3),
    @DiscountAmount      NUMERIC(18,3) = 0,
    @CurrencyId          INT = 1,
    @ExchangeRate        NUMERIC(18,3) = 1,
    @CostCenterId        INT = NULL,
    @UserId              INT,
    @NewJournalId        INT OUTPUT,
    @JournalNumber       VARCHAR(50) OUTPUT,
    @ErrorMessage        NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @NewJournalId = NULL;
    SET @JournalNumber = NULL;
    SET @ErrorMessage = NULL;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @ReceiptKind NOT IN (3, 4)
            RAISERROR(N'ReceiptKind must be 3 for receipt or 4 for payment.', 16, 1);

        IF @PureAmount <= 0 OR @DiscountAmount < 0
            RAISERROR(N'قيمة الإيصال أو الخصم غير صالحة.', 16, 1);

        IF @PartyAccountCode IS NULL OR LTRIM(RTRIM(@PartyAccountCode)) = ''
            RAISERROR(N'الطرف غير مربوط بحساب محاسبي.', 16, 1);

        IF @TreasuryAccountCode IS NULL OR LTRIM(RTRIM(@TreasuryAccountCode)) = ''
            RAISERROR(N'الخزينة غير مربوطة بحساب محاسبي.', 16, 1);

        EXEC dbo.ACC_SYSTEM_ACCOUNT_VALIDATE_FOR_POSTING;

        DECLARE @DiscountAccountCode VARCHAR(40);
        DECLARE @ReceiptTotal NUMERIC(18,3);
        DECLARE @Rows dbo.ACC_BALANCE_BULK_TYPE;
        DECLARE @JournalNotes NVARCHAR(MAX);

        SET @ReceiptTotal = @PureAmount + @DiscountAmount;
        SET @JournalNotes = CASE WHEN @ReceiptKind = 3
            THEN N'سند قبض رقم ' ELSE N'سند صرف رقم ' END
            + ISNULL(@ReceiptNumber, N'');

        IF @DiscountAmount > 0
        BEGIN
            SELECT @DiscountAccountCode = A.ACC_CODE
            FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES AS T
            INNER JOIN dbo.ACC_SYSTEM_ACCOUNT_LINKS AS L
                ON L.SystemAccountTypeID = T.SystemAccountTypeID AND L.IsActive = 1
            INNER JOIN dbo.ACCOUNTS_TREE AS A ON A.T_ID = L.ACC_T_ID
            WHERE T.AccountKey = CASE WHEN @ReceiptKind = 3
                THEN N'DISCOUNT_ALLOWED' ELSE N'DISCOUNT_RECEIVED' END
              AND T.IsActive = 1;

            IF @DiscountAccountCode IS NULL
                RAISERROR(N'حساب الخصم المطلوب للإيصال غير مربوط.', 16, 1);
        END;

        IF @ReceiptKind = 3
        BEGIN
            INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
            VALUES
                (1, @TreasuryAccountCode, NULL, @PureAmount, @CurrencyId, N'دخول إلى الخزينة', @ReceiptNumber, @CostCenterId),
                (3, @PartyAccountCode, @ReceiptTotal, NULL, @CurrencyId, N'تخفيض مديونية الطرف', @ReceiptNumber, @CostCenterId);

            IF @DiscountAmount > 0
                INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
                VALUES (2, @DiscountAccountCode, NULL, @DiscountAmount, @CurrencyId, N'خصم مسموح', @ReceiptNumber, @CostCenterId);
        END
        ELSE
        BEGIN
            INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
            VALUES
                (1, @PartyAccountCode, NULL, @ReceiptTotal, @CurrencyId, N'تسوية مستحقات الطرف', @ReceiptNumber, @CostCenterId),
                (2, @TreasuryAccountCode, @PureAmount, NULL, @CurrencyId, N'خروج من الخزينة', @ReceiptNumber, @CostCenterId);

            IF @DiscountAmount > 0
                INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
                VALUES (3, @DiscountAccountCode, @DiscountAmount, NULL, @CurrencyId, N'خصم مكتسب', @ReceiptNumber, @CostCenterId);
        END;

        EXEC dbo.ACC_Journal_Save
            @JournalDate = @ReceiptDate,
            @UserId = @UserId,
            @Notes = @JournalNotes,
            @CurrencyId = @CurrencyId,
            @ExchangeRate = @ExchangeRate,
            @SourceType = @ReceiptKind,
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
        SET @ErrorMessage = LEFT(N'[ACC_PostReceipt] ' + ERROR_MESSAGE(), 500);
        SET @NewJournalId = NULL;
        SET @JournalNumber = NULL;
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH;
END;
GO

