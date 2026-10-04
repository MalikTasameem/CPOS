SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.ACC_Source_Repost', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.ACC_Source_Repost AS RETURN 0;');
GO

ALTER PROCEDURE dbo.ACC_Source_Repost
    @SourceTable       SYSNAME,
    @SourceKey         NVARCHAR(100),
    @SourceId          BIGINT = NULL,
    @SourceType        INT = NULL,
    @BsTypeId          INT = NULL,
    @OldJournalId      INT,
    @EditVersion       INT,
    @DocumentDate      DATETIME,
    @ReversalDate      DATETIME,
    @UserId            INT,
    @Reason            NVARCHAR(500),
    @CurrencyId        INT = 1,
    @ExchangeRate      NUMERIC(18,3) = 1,
    @Rows              dbo.ACC_BALANCE_BULK_TYPE READONLY,
    @ReversalJournalId INT OUTPUT,
    @NewJournalId      INT OUTPUT,
    @JournalNumber     VARCHAR(50) OUTPUT,
    @ErrorMessage      NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @ReversalJournalId = NULL;
    SET @NewJournalId = NULL;
    SET @JournalNumber = NULL;
    SET @ErrorMessage = NULL;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @SourceTable IS NULL OR @SourceKey IS NULL
            RAISERROR(N'يجب تحديد مصدر المعاملة ومفتاحها.', 16, 1);

        IF @EditVersion IS NULL OR @EditVersion <= 0
            RAISERROR(N'رقم نسخة التعديل يجب أن يكون أكبر من صفر.', 16, 1);

        IF NOT EXISTS
        (
            SELECT 1
            FROM dbo.ACC_BALANCE_MASTER WITH (UPDLOCK, HOLDLOCK)
            WHERE T_ID = @OldJournalId
              AND SourceTable = @SourceTable
              AND SourceKey = @SourceKey
              AND is_Depended = 1
              AND ISNULL(is_reverse, 0) = 0
              AND reverse_T_ID IS NULL
        )
            RAISERROR(N'القيد القديم لا يطابق المصدر أو سبق عكسه.', 16, 1);

        IF EXISTS
        (
            SELECT 1
            FROM dbo.ACC_InvoiceRepostLog
            WHERE SourceTable = @SourceTable
              AND SourceKey = @SourceKey
              AND EditVersion = @EditVersion
        )
            RAISERROR(N'تم تنفيذ إعادة التقييد لهذه النسخة مسبقا.', 16, 1);

        EXEC dbo.ACC_Journal_Reverse
            @JournalId = @OldJournalId,
            @ReverseDate = @ReversalDate,
            @UserId = @UserId,
            @Reason = @Reason,
            @ReversalJournalId = @ReversalJournalId OUTPUT,
            @ErrorMessage = @ErrorMessage OUTPUT;

        EXEC dbo.ACC_Journal_Save
            @JournalDate = @DocumentDate,
            @UserId = @UserId,
            @Notes = @Reason,
            @CurrencyId = @CurrencyId,
            @ExchangeRate = @ExchangeRate,
            @SourceType = @SourceType,
            @SourceId = @SourceId,
            @SourceTable = @SourceTable,
            @SourceKey = @SourceKey,
            @Rows = @Rows,
            @NewJournalId = @NewJournalId OUTPUT,
            @JournalNumber = @JournalNumber OUTPUT,
            @ErrorMessage = @ErrorMessage OUTPUT;

        INSERT INTO dbo.ACC_InvoiceRepostLog
        (
            SourceTable, SourceID, SourceKey, BsType_ID, EditVersion,
            OldJournalId, ReversalJournalId, NewJournalId,
            RepostReason, RepostBy
        )
        VALUES
        (
            @SourceTable, @SourceId, @SourceKey, @BsTypeId, @EditVersion,
            @OldJournalId, @ReversalJournalId, @NewJournalId,
            @Reason, @UserId
        );

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        SET @ErrorMessage = LEFT(N'[ACC_Source_Repost] ' + ERROR_MESSAGE(), 500);
        SET @ReversalJournalId = NULL;
        SET @NewJournalId = NULL;
        SET @JournalNumber = NULL;
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH;
END;
GO

