SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.ACC_Journal_Save', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.ACC_Journal_Save AS RETURN 0;');
GO

ALTER PROCEDURE dbo.ACC_Journal_Save
    @JournalDate    DATETIME,
    @UserId         INT,
    @Notes          NVARCHAR(MAX),
    @CurrencyId     INT = 1,
    @ExchangeRate   NUMERIC(18,3) = 1,
    @SourceType     INT = NULL,
    @SourceId       BIGINT = NULL,
    @SourceTable    SYSNAME = NULL,
    @SourceKey      NVARCHAR(100) = NULL,
    @Rows           dbo.ACC_BALANCE_BULK_TYPE READONLY,
    @NewJournalId   INT OUTPUT,
    @JournalNumber  VARCHAR(50) OUTPUT,
    @ErrorMessage   NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SET @NewJournalId = NULL;
    SET @JournalNumber = NULL;
    SET @ErrorMessage = NULL;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @JournalDate IS NULL
            RAISERROR(N'يجب تحديد تاريخ القيد.', 16, 1);

        IF @UserId IS NULL OR @UserId <= 0
            RAISERROR(N'يجب تحديد المستخدم المنفذ.', 16, 1);

        IF @ExchangeRate IS NULL OR @ExchangeRate <= 0
            RAISERROR(N'سعر الصرف يجب أن يكون أكبر من صفر.', 16, 1);

        IF @SourceTable IS NOT NULL
           AND (@SourceKey IS NULL OR LTRIM(RTRIM(@SourceKey)) = N'')
            RAISERROR(N'يجب تحديد SourceKey لكل حركة مصدرية.', 16, 1);

        IF NOT EXISTS (SELECT 1 FROM @Rows)
            RAISERROR(N'لا توجد تفاصيل للقيد.', 16, 1);

        IF EXISTS
        (
            SELECT 1 FROM @Rows
            WHERE ACC_CODE IS NULL
               OR LTRIM(RTRIM(ACC_CODE)) = N''
               OR LEN(ACC_CODE) > 40
        )
            RAISERROR(N'يوجد حساب فارغ أو يتجاوز 40 حرفا.', 16, 1);

        IF EXISTS
        (
            SELECT 1 FROM @Rows
            WHERE (ISNULL(DEBIT, 0) = 0 AND ISNULL(CREDIT, 0) = 0)
               OR (ISNULL(DEBIT, 0) > 0 AND ISNULL(CREDIT, 0) > 0)
               OR ISNULL(DEBIT, 0) < 0
               OR ISNULL(CREDIT, 0) < 0
        )
            RAISERROR(N'يوجد سطر بقيم مدينة/دائنة غير صالحة.', 16, 1);

        IF ROUND((SELECT SUM(ISNULL(DEBIT, 0)) FROM @Rows), 3)
           <> ROUND((SELECT SUM(ISNULL(CREDIT, 0)) FROM @Rows), 3)
            RAISERROR(N'القيد غير متوازن.', 16, 1);

        IF EXISTS
        (
            SELECT 1
            FROM @Rows AS R
            LEFT JOIN dbo.ACCOUNTS_TREE AS A
                ON A.ACC_CODE = CONVERT(VARCHAR(40), R.ACC_CODE)
            WHERE A.T_ID IS NULL
        )
            RAISERROR(N'يوجد حساب غير موجود في دليل الحسابات.', 16, 1);

        IF EXISTS
        (
            SELECT 1
            FROM @Rows AS R
            INNER JOIN dbo.ACCOUNTS_TREE AS A
                ON A.ACC_CODE = CONVERT(VARCHAR(40), R.ACC_CODE)
            WHERE ISNULL(A.is_Lock_Trans, 0) = 1
               OR EXISTS
                  (
                      SELECT 1
                      FROM dbo.ACCOUNTS_TREE AS C
                      WHERE C.ACC_PARENT = A.ACC_CODE
                  )
        )
            RAISERROR(N'يوجد حساب مقفل أو حساب أب لا يقبل الحركة.', 16, 1);

        EXEC dbo.ACC_GetNextIdBlock
            @SequenceName = N'ACC_BALANCE_MASTER',
            @BlockSize = 1,
            @FirstValue = @NewJournalId OUTPUT;

        DECLARE @JournalDateOnly DATE;
        SET @JournalDateOnly = CONVERT(DATE, @JournalDate);

        EXEC dbo.ACC_GetNextJournalNumber
            @EntryDate = @JournalDateOnly,
            @NextNumber = @JournalNumber OUTPUT;

        INSERT INTO dbo.ACC_BALANCE_MASTER
        (
            T_ID, [DATE], NOTES, Currency_ID, Currency_Equal,
            is_Depended, USER_ID, DEPEND_USER_ID, StatusId,
            JournalNumber, [YEAR], SourceType, SourceId, SourceTable, SourceKey,
            EntrySourceType
        )
        VALUES
        (
            @NewJournalId, @JournalDate, ISNULL(@Notes, N''), @CurrencyId, @ExchangeRate,
            1, @UserId, @UserId, 1,
            @JournalNumber, YEAR(@JournalDate), @SourceType, @SourceId, @SourceTable, @SourceKey,
            1
        );

        DECLARE @LineCount INT;
        DECLARE @FirstLineId INT;
        SELECT @LineCount = COUNT(*) FROM @Rows;

        EXEC dbo.ACC_GetNextIdBlock
            @SequenceName = N'ACC_BALANCE',
            @BlockSize = @LineCount,
            @FirstValue = @FirstLineId OUTPUT;

        ;WITH OrderedRows AS
        (
            SELECT
                ROW_NUMBER() OVER
                (
                    ORDER BY ISNULL(RowNo, 2147483647), ACC_CODE
                ) AS RN,
                ACC_CODE, DEBIT, CREDIT, Currency, Notes, Bill_Num, COST_ID
            FROM @Rows
        )
        INSERT INTO dbo.ACC_BALANCE
        (
            T_ID, B_T_ID, ACC_CODE, DEBIT, CREDIT, USER_ID,
            IS_VOID, Currency, Notes, Bill_Num, COST_ID,
            is_Depended, [YEAR]
        )
        SELECT
            @FirstLineId + RN - 1,
            @NewJournalId,
            CONVERT(VARCHAR(40), ACC_CODE),
            ISNULL(DEBIT, 0),
            ISNULL(CREDIT, 0),
            @UserId,
            0,
            ISNULL(Currency, @CurrencyId),
            Notes,
            Bill_Num,
            COST_ID,
            1,
            YEAR(@JournalDate)
        FROM OrderedRows;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        SET @ErrorMessage = LEFT(N'[ACC_Journal_Save] ' + ERROR_MESSAGE(), 500);
        SET @NewJournalId = NULL;
        SET @JournalNumber = NULL;
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH;
END;
GO
