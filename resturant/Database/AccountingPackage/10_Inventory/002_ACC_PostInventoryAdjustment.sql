SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.ACC_PostInventoryAdjustment', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.ACC_PostInventoryAdjustment AS RETURN 0;');
GO

ALTER PROCEDURE dbo.ACC_PostInventoryAdjustment
    @SourceTable       SYSNAME,
    @SourceKey         NVARCHAR(100),
    @SourceId          BIGINT = NULL,
    @DocumentNumber    NVARCHAR(50),
    @DocumentDate      DATETIME,
    @CurrencyId        INT = 1,
    @ExchangeRate      NUMERIC(18,3) = 1,
    @UserId            INT,
    @Lines             dbo.ACC_INVENTORY_VALUE_LINE_TYPE READONLY,
    @NewJournalId      INT OUTPUT,
    @JournalNumber     VARCHAR(50) OUTPUT,
    @ErrorMessage      NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM @Lines)
            RAISERROR(N'لا توجد فروقات مخزون للترحيل.', 16, 1);

        EXEC dbo.ACC_SYSTEM_ACCOUNT_VALIDATE_FOR_POSTING;

        DECLARE @GainAccountCode VARCHAR(40);
        DECLARE @LossAccountCode VARCHAR(40);
        DECLARE @Rows dbo.ACC_BALANCE_BULK_TYPE;
        DECLARE @GainTotal NUMERIC(18,3);
        DECLARE @LossTotal NUMERIC(18,3);
        DECLARE @JournalNotes NVARCHAR(MAX);

        SELECT @GainAccountCode = A.ACC_CODE
        FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES AS T
        INNER JOIN dbo.ACC_SYSTEM_ACCOUNT_LINKS AS L
            ON L.SystemAccountTypeID = T.SystemAccountTypeID AND L.IsActive = 1
        INNER JOIN dbo.ACCOUNTS_TREE AS A ON A.T_ID = L.ACC_T_ID
        WHERE T.AccountKey = N'INVENTORY_ADJUSTMENT_GAIN' AND T.IsActive = 1;

        SELECT @LossAccountCode = A.ACC_CODE
        FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES AS T
        INNER JOIN dbo.ACC_SYSTEM_ACCOUNT_LINKS AS L
            ON L.SystemAccountTypeID = T.SystemAccountTypeID AND L.IsActive = 1
        INNER JOIN dbo.ACCOUNTS_TREE AS A ON A.T_ID = L.ACC_T_ID
        WHERE T.AccountKey = N'INVENTORY_ADJUSTMENT_LOSS' AND T.IsActive = 1;

        IF EXISTS (SELECT 1 FROM @Lines WHERE Amount > 0) AND @GainAccountCode IS NULL
            RAISERROR(N'حساب أرباح تسوية المخزون غير مربوط.', 16, 1);

        IF EXISTS (SELECT 1 FROM @Lines WHERE Amount < 0) AND @LossAccountCode IS NULL
            RAISERROR(N'حساب خسائر تسوية المخزون غير مربوط.', 16, 1);

        SELECT
            @GainTotal = SUM(CASE WHEN Amount > 0 THEN Amount ELSE 0 END),
            @LossTotal = SUM(CASE WHEN Amount < 0 THEN ABS(Amount) ELSE 0 END)
        FROM @Lines;

        IF ISNULL(@GainTotal, 0) > 0
        BEGIN
            INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
            SELECT 100 + ROW_NUMBER() OVER (ORDER BY InventoryAccountCode),
                   InventoryAccountCode, NULL, SUM(Amount), @CurrencyId,
                   N'زيادة مخزون', @DocumentNumber, CostCenterId
            FROM @Lines WHERE Amount > 0
            GROUP BY InventoryAccountCode, CostCenterId;

            INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
            VALUES (1, @GainAccountCode, @GainTotal, NULL, @CurrencyId, N'أرباح تسوية مخزون', @DocumentNumber, NULL);
        END;

        IF ISNULL(@LossTotal, 0) > 0
        BEGIN
            INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
            VALUES (2, @LossAccountCode, NULL, @LossTotal, @CurrencyId, N'خسائر تسوية مخزون', @DocumentNumber, NULL);

            INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
            SELECT 200 + ROW_NUMBER() OVER (ORDER BY InventoryAccountCode),
                   InventoryAccountCode, SUM(ABS(Amount)), NULL, @CurrencyId,
                   N'عجز مخزون', @DocumentNumber, CostCenterId
            FROM @Lines WHERE Amount < 0
            GROUP BY InventoryAccountCode, CostCenterId;
        END;

        SET @JournalNotes = N'تسوية مخزون رقم ' + ISNULL(@DocumentNumber, N'');

        EXEC dbo.ACC_Journal_Save
            @JournalDate = @DocumentDate, @UserId = @UserId, @Notes = @JournalNotes,
            @CurrencyId = @CurrencyId, @ExchangeRate = @ExchangeRate,
            @SourceType = 39, @SourceId = @SourceId,
            @SourceTable = @SourceTable, @SourceKey = @SourceKey,
            @Rows = @Rows, @NewJournalId = @NewJournalId OUTPUT,
            @JournalNumber = @JournalNumber OUTPUT, @ErrorMessage = @ErrorMessage OUTPUT;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        SET @ErrorMessage = LEFT(N'[ACC_PostInventoryAdjustment] ' + ERROR_MESSAGE(), 500);
        SET @NewJournalId = NULL;
        SET @JournalNumber = NULL;
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH;
END;
GO

