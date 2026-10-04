SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.ACC_Journal_Reverse', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.ACC_Journal_Reverse AS RETURN 0;');
GO

ALTER PROCEDURE dbo.ACC_Journal_Reverse
    @JournalId          INT,
    @ReverseDate        DATETIME,
    @UserId             INT,
    @Reason             NVARCHAR(500),
    @ReversalJournalId  INT OUTPUT,
    @ErrorMessage       NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @ReversalJournalId = NULL;
    SET @ErrorMessage = NULL;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.ACC_BALANCE_MASTER WITH (UPDLOCK, HOLDLOCK)
            WHERE T_ID = @JournalId
              AND is_Depended = 1
        )
            RAISERROR(N'القيد الأصلي غير موجود أو غير معتمد.', 16, 1);

        IF EXISTS
        (
            SELECT 1
            FROM dbo.ACC_BALANCE_MASTER
            WHERE T_ID = @JournalId
              AND ISNULL(reverse_T_ID, 0) <> 0
        )
            RAISERROR(N'تم عكس هذا القيد مسبقا.', 16, 1);

        IF NOT EXISTS
        (
            SELECT 1 FROM dbo.ACC_BALANCE
            WHERE B_T_ID = @JournalId
              AND IS_VOID = 0
              AND is_Depended = 1
        )
            RAISERROR(N'القيد الأصلي لا يحتوي تفاصيل فعالة.', 16, 1);

        DECLARE @Rows dbo.ACC_BALANCE_BULK_TYPE;
        DECLARE @JournalNumber VARCHAR(50);
        DECLARE @SaveError NVARCHAR(500);
        DECLARE @OriginalNumber VARCHAR(50);
        DECLARE @ReversalNotes NVARCHAR(MAX);
        DECLARE @CurrencyId INT;
        DECLARE @ExchangeRate NUMERIC(18,3);
        DECLARE @ReversalSourceKey NVARCHAR(100);

        SELECT
            @OriginalNumber = JournalNumber,
            @CurrencyId = Currency_ID,
            @ExchangeRate = Currency_Equal
        FROM dbo.ACC_BALANCE_MASTER
        WHERE T_ID = @JournalId;

        SET @ReversalNotes = N'قيد عكسي للقيد '
            + CONVERT(NVARCHAR(20), @JournalId)
            + N' (' + ISNULL(@OriginalNumber, '') + N') - '
            + ISNULL(@Reason, N'');
        SET @ReversalSourceKey = CONVERT(NVARCHAR(100), @JournalId);

        INSERT INTO @Rows
        (
            RowNo, ACC_CODE, DEBIT, CREDIT, Currency,
            Notes, Bill_Num, COST_ID
        )
        SELECT
            ROW_NUMBER() OVER (ORDER BY T_ID),
            CONVERT(NVARCHAR(50), ACC_CODE),
            CREDIT,
            DEBIT,
            Currency,
            N'قيد عكسي للقيد ' + CONVERT(NVARCHAR(20), @JournalId)
                + N' - ' + ISNULL(@Reason, N''),
            Bill_Num,
            COST_ID
        FROM dbo.ACC_BALANCE
        WHERE B_T_ID = @JournalId
          AND IS_VOID = 0
          AND is_Depended = 1;

        EXEC dbo.ACC_Journal_Save
            @JournalDate = @ReverseDate,
            @UserId = @UserId,
            @Notes = @ReversalNotes,
            @CurrencyId = @CurrencyId,
            @ExchangeRate = @ExchangeRate,
            @SourceType = NULL,
            @SourceId = @JournalId,
            @SourceTable = N'REVERSAL',
            @SourceKey = @ReversalSourceKey,
            @Rows = @Rows,
            @NewJournalId = @ReversalJournalId OUTPUT,
            @JournalNumber = @JournalNumber OUTPUT,
            @ErrorMessage = @SaveError OUTPUT;

        UPDATE dbo.ACC_BALANCE_MASTER
        SET reverse_T_ID = @ReversalJournalId
        WHERE T_ID = @JournalId;

        UPDATE dbo.ACC_BALANCE_MASTER
        SET is_reverse = 1
        WHERE T_ID = @ReversalJournalId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        SET @ErrorMessage = LEFT(N'[ACC_Journal_Reverse] ' + ERROR_MESSAGE(), 500);
        SET @ReversalJournalId = NULL;
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH;
END;
GO
