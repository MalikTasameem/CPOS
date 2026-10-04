SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.ACC_PostInventoryIssue', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.ACC_PostInventoryIssue AS RETURN 0;');
GO

ALTER PROCEDURE dbo.ACC_PostInventoryIssue
    @IssueKind         TINYINT,
    @SourceTable      SYSNAME,
    @SourceKey        NVARCHAR(100),
    @SourceId         BIGINT = NULL,
    @DocumentNumber   NVARCHAR(50),
    @DocumentDate     DATETIME,
    @CurrencyId       INT = 1,
    @ExchangeRate     NUMERIC(18,3) = 1,
    @UserId           INT,
    @Lines            dbo.ACC_INVENTORY_VALUE_LINE_TYPE READONLY,
    @NewJournalId     INT OUTPUT,
    @JournalNumber    VARCHAR(50) OUTPUT,
    @ErrorMessage     NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @IssueKind NOT IN (1, 2)
            RAISERROR(N'IssueKind must be 1 for damage or 2 for manufacturing consumption.', 16, 1);

        IF NOT EXISTS (SELECT 1 FROM @Lines)
            RAISERROR(N'لا توجد تفاصيل لإخراج المخزون.', 16, 1);

        IF EXISTS (SELECT 1 FROM @Lines WHERE Amount <= 0)
            RAISERROR(N'قيم إخراج المخزون يجب أن تكون موجبة.', 16, 1);

        EXEC dbo.ACC_SYSTEM_ACCOUNT_VALIDATE_FOR_POSTING;

        DECLARE @ExpenseAccountCode VARCHAR(40);
        DECLARE @Rows dbo.ACC_BALANCE_BULK_TYPE;
        DECLARE @TotalAmount NUMERIC(18,3);
        DECLARE @JournalNotes NVARCHAR(MAX);
        DECLARE @PostingSourceType INT;

        SET @PostingSourceType = CASE WHEN @IssueKind = 1 THEN 8 ELSE 14 END;

        SELECT @ExpenseAccountCode = A.ACC_CODE
        FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES AS T
        INNER JOIN dbo.ACC_SYSTEM_ACCOUNT_LINKS AS L
            ON L.SystemAccountTypeID = T.SystemAccountTypeID AND L.IsActive = 1
        INNER JOIN dbo.ACCOUNTS_TREE AS A ON A.T_ID = L.ACC_T_ID
        WHERE T.AccountKey = CASE WHEN @IssueKind = 1
            THEN N'DAMAGE_EXPENSE' ELSE N'MANUFACTURING_EXPENSE' END
          AND T.IsActive = 1;

        IF @IssueKind = 2 AND @ExpenseAccountCode IS NULL
        BEGIN
            SELECT @ExpenseAccountCode = A.ACC_CODE
            FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES AS T
            INNER JOIN dbo.ACC_SYSTEM_ACCOUNT_LINKS AS L
                ON L.SystemAccountTypeID = T.SystemAccountTypeID AND L.IsActive = 1
            INNER JOIN dbo.ACCOUNTS_TREE AS A ON A.T_ID = L.ACC_T_ID
            WHERE T.AccountKey = N'COGS' AND T.IsActive = 1;
        END;

        IF @ExpenseAccountCode IS NULL
            RAISERROR(N'حساب مصروف إخراج المخزون غير مربوط.', 16, 1);

        SELECT @TotalAmount = SUM(Amount) FROM @Lines;

        INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
        VALUES
        (
            1, @ExpenseAccountCode, NULL, @TotalAmount, @CurrencyId,
            CASE WHEN @IssueKind = 1 THEN N'مصروف تلف' ELSE N'استهلاك تصنيع' END,
            @DocumentNumber, NULL
        );

        INSERT INTO @Rows(RowNo, ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID)
        SELECT 100 + ROW_NUMBER() OVER (ORDER BY InventoryAccountCode),
               InventoryAccountCode, SUM(Amount), NULL, @CurrencyId,
               N'خروج مخزون', @DocumentNumber, CostCenterId
        FROM @Lines
        GROUP BY InventoryAccountCode, CostCenterId;

        SET @JournalNotes = CASE WHEN @IssueKind = 1
            THEN N'تلف مخزون رقم ' ELSE N'استهلاك تصنيع رقم ' END
            + ISNULL(@DocumentNumber, N'');

        EXEC dbo.ACC_Journal_Save
            @JournalDate = @DocumentDate, @UserId = @UserId, @Notes = @JournalNotes,
            @CurrencyId = @CurrencyId, @ExchangeRate = @ExchangeRate,
            @SourceType = @PostingSourceType,
            @SourceId = @SourceId, @SourceTable = @SourceTable, @SourceKey = @SourceKey,
            @Rows = @Rows, @NewJournalId = @NewJournalId OUTPUT,
            @JournalNumber = @JournalNumber OUTPUT, @ErrorMessage = @ErrorMessage OUTPUT;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        SET @ErrorMessage = LEFT(N'[ACC_PostInventoryIssue] ' + ERROR_MESSAGE(), 500);
        SET @NewJournalId = NULL;
        SET @JournalNumber = NULL;
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH;
END;
GO
