SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.ACC_Source_CancelPosting', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.ACC_Source_CancelPosting AS RETURN 0;');
GO

ALTER PROCEDURE dbo.ACC_Source_CancelPosting
    @SourceTable       SYSNAME,
    @SourceKey         NVARCHAR(100),
    @SourceId          BIGINT = NULL,
    @BsTypeId          INT = NULL,
    @OldJournalId      INT,
    @CancellationDate  DATETIME,
    @UserId            INT,
    @Reason            NVARCHAR(500),
    @ReversalJournalId INT OUTPUT,
    @ErrorMessage      NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @ReversalJournalId = NULL;
    SET @ErrorMessage = NULL;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @SourceTable IS NULL OR @SourceKey IS NULL
            RAISERROR(N'يجب تحديد مصدر المعاملة ومفتاحها.', 16, 1);

        IF @Reason IS NULL OR LTRIM(RTRIM(@Reason)) = N''
            RAISERROR(N'سبب الإلغاء إلزامي.', 16, 1);

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
            FROM dbo.ACC_InvoiceCancelLog
            WHERE SourceTable = @SourceTable
              AND SourceKey = @SourceKey
              AND OldJournalId = @OldJournalId
        )
            RAISERROR(N'تم تنفيذ عكس الإلغاء لهذا القيد مسبقا.', 16, 1);

        EXEC dbo.ACC_Journal_Reverse
            @JournalId = @OldJournalId,
            @ReverseDate = @CancellationDate,
            @UserId = @UserId,
            @Reason = @Reason,
            @ReversalJournalId = @ReversalJournalId OUTPUT,
            @ErrorMessage = @ErrorMessage OUTPUT;

        INSERT INTO dbo.ACC_InvoiceCancelLog
        (
            SourceTable, SourceID, SourceKey, BsType_ID,
            OldJournalId, ReversalJournalId,
            CancelReason, CancelBy
        )
        VALUES
        (
            @SourceTable, @SourceId, @SourceKey, @BsTypeId,
            @OldJournalId, @ReversalJournalId,
            @Reason, @UserId
        );

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        SET @ErrorMessage = LEFT(N'[ACC_Source_CancelPosting] ' + ERROR_MESSAGE(), 500);
        SET @ReversalJournalId = NULL;
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH;
END;
GO

