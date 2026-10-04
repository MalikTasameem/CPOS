SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.ACC_PostInventoryTransfer', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.ACC_PostInventoryTransfer AS RETURN 0;');
GO

ALTER PROCEDURE dbo.ACC_PostInventoryTransfer
    @SourceTable       SYSNAME,
    @SourceKey         NVARCHAR(100),
    @SourceId          BIGINT = NULL,
    @DocumentNumber    NVARCHAR(50),
    @DocumentDate      DATETIME,
    @CurrencyId        INT = 1,
    @ExchangeRate      NUMERIC(18,3) = 1,
    @UserId            INT,
    @Lines             dbo.ACC_INVENTORY_TRANSFER_LINE_TYPE READONLY,
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
            RAISERROR(N'لا توجد تفاصيل لتحويل المخزون.', 16, 1);

        DECLARE @Rows dbo.ACC_BALANCE_BULK_TYPE;
        DECLARE @JournalNotes NVARCHAR(MAX);
        SET @JournalNotes = N'تحويل مخزون رقم ' + ISNULL(@DocumentNumber, N'');

        INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
        SELECT 100 + ROW_NUMBER() OVER (ORDER BY ToInventoryAccountCode),
               ToInventoryAccountCode, NULL, SUM(Amount), @CurrencyId,
               N'مخزون داخل', @DocumentNumber, CostCenterId
        FROM @Lines
        GROUP BY ToInventoryAccountCode, CostCenterId;

        INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
        SELECT 200 + ROW_NUMBER() OVER (ORDER BY FromInventoryAccountCode),
               FromInventoryAccountCode, SUM(Amount), NULL, @CurrencyId,
               N'مخزون خارج', @DocumentNumber, CostCenterId
        FROM @Lines
        GROUP BY FromInventoryAccountCode, CostCenterId;

        EXEC dbo.ACC_Journal_Save
            @JournalDate = @DocumentDate, @UserId = @UserId, @Notes = @JournalNotes,
            @CurrencyId = @CurrencyId, @ExchangeRate = @ExchangeRate,
            @SourceType = 12, @SourceId = @SourceId,
            @SourceTable = @SourceTable, @SourceKey = @SourceKey,
            @Rows = @Rows, @NewJournalId = @NewJournalId OUTPUT,
            @JournalNumber = @JournalNumber OUTPUT, @ErrorMessage = @ErrorMessage OUTPUT;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        SET @ErrorMessage = LEFT(N'[ACC_PostInventoryTransfer] ' + ERROR_MESSAGE(), 500);
        SET @NewJournalId = NULL;
        SET @JournalNumber = NULL;
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH;
END;
GO

