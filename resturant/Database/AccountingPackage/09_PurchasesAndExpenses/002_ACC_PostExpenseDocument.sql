SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.ACC_PostExpenseDocument', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.ACC_PostExpenseDocument AS RETURN 0;');
GO

ALTER PROCEDURE dbo.ACC_PostExpenseDocument
    @SourceTable           SYSNAME,
    @SourceKey             NVARCHAR(100),
    @SourceId              BIGINT = NULL,
    @DocumentNumber        NVARCHAR(50),
    @DocumentDate          DATETIME,
    @CounterpartyAccountCode VARCHAR(40),
    @CurrencyId            INT = 1,
    @ExchangeRate          NUMERIC(18,3) = 1,
    @UserId                INT,
    @Lines                 dbo.ACC_EXPENSE_LINE_TYPE READONLY,
    @NewJournalId          INT OUTPUT,
    @JournalNumber         VARCHAR(50) OUTPUT,
    @ErrorMessage          NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @NewJournalId = NULL;
    SET @JournalNumber = NULL;
    SET @ErrorMessage = NULL;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @CounterpartyAccountCode IS NULL OR LTRIM(RTRIM(@CounterpartyAccountCode)) = ''
            RAISERROR(N'الطرف أو الخزينة المقابلة للمصروف غير مربوطة بحساب.', 16, 1);

        IF NOT EXISTS (SELECT 1 FROM @Lines)
            RAISERROR(N'لا توجد تفاصيل للمصروف.', 16, 1);

        IF EXISTS
        (
            SELECT 1 FROM @Lines
            WHERE (TreatmentType = 1 AND (ExpenseAccountCode IS NULL OR LTRIM(RTRIM(ExpenseAccountCode)) = ''))
               OR (TreatmentType = 2 AND (InventoryAccountCode IS NULL OR LTRIM(RTRIM(InventoryAccountCode)) = ''))
        )
            RAISERROR(N'يوجد بند مصروف بلا حساب مطابق لطريقة معالجته.', 16, 1);

        DECLARE @Rows dbo.ACC_BALANCE_BULK_TYPE;
        DECLARE @TotalAmount NUMERIC(18,3);
        DECLARE @JournalNotes NVARCHAR(MAX);

        SELECT @TotalAmount = SUM(Amount) FROM @Lines;
        SET @JournalNotes = N'مستند مصروف رقم ' + ISNULL(@DocumentNumber, N'');

        INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
        SELECT
            100 + ROW_NUMBER() OVER
            (
                ORDER BY CASE WHEN TreatmentType = 1 THEN ExpenseAccountCode ELSE InventoryAccountCode END
            ),
            CASE WHEN TreatmentType = 1 THEN ExpenseAccountCode ELSE InventoryAccountCode END,
            NULL,
            SUM(Amount),
            @CurrencyId,
            CASE WHEN TreatmentType = 1 THEN N'مصروف فترة' ELSE N'مصروف محمل على المخزون' END,
            @DocumentNumber,
            CostCenterId
        FROM @Lines
        GROUP BY
            TreatmentType,
            CASE WHEN TreatmentType = 1 THEN ExpenseAccountCode ELSE InventoryAccountCode END,
            CostCenterId;

        INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
        VALUES
        (
            1, @CounterpartyAccountCode, @TotalAmount, NULL,
            @CurrencyId, N'الطرف المقابل للمصروف', @DocumentNumber, NULL
        );

        EXEC dbo.ACC_Journal_Save
            @JournalDate = @DocumentDate,
            @UserId = @UserId,
            @Notes = @JournalNotes,
            @CurrencyId = @CurrencyId,
            @ExchangeRate = @ExchangeRate,
            @SourceType = 2,
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
        SET @ErrorMessage = LEFT(N'[ACC_PostExpenseDocument] ' + ERROR_MESSAGE(), 500);
        SET @NewJournalId = NULL;
        SET @JournalNumber = NULL;
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH;
END;
GO

