/* TEMPLATE ONLY - DO NOT EXECUTE AS-IS. */
RAISERROR(N'قالب فقط: أكمل مصفوفة المواءمة واستبدل الرموز قبل التنفيذ.', 16, 1);
RETURN;
GO

CREATE PROCEDURE dbo.[<PROJECT>_PostSalesDocument]
    @DocumentId <DOCUMENT_ID_SQL_TYPE>,
    @UserId INT,
    @NewJournalId INT OUTPUT,
    @ErrorMessage NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @DocumentKind TINYINT;
    DECLARE @SourceKey NVARCHAR(100);
    DECLARE @SourceId BIGINT;
    DECLARE @DocumentNumber NVARCHAR(50);
    DECLARE @DocumentDate DATETIME;
    DECLARE @PartyAccountCode VARCHAR(40);
    DECLARE @GrossTotal NUMERIC(18,3);
    DECLARE @DiscountAmount NUMERIC(18,3);
    DECLARE @NetAmount NUMERIC(18,3);
    DECLARE @CurrencyId INT;
    DECLARE @ExchangeRate NUMERIC(18,3);
    DECLARE @JournalNumber VARCHAR(50);
    DECLARE @Lines dbo.ACC_SALES_LINE_TYPE;

    BEGIN TRY
        BEGIN TRANSACTION;

        /* اقرأ الرأس مع قفل، وارفض غير المعتمد والملغي والمرحّل سابقا. */
        SELECT
        @DocumentKind = CASE WHEN H.[<IS_RETURN_COLUMN>] = 1 THEN 2 ELSE 1 END,
        @SourceKey = CONVERT(NVARCHAR(100), H.[<DOCUMENT_ID_COLUMN>]),
        @SourceId = TRY_CONVERT(BIGINT, H.[<DOCUMENT_ID_COLUMN>]),
        @DocumentNumber = CONVERT(NVARCHAR(50), H.[<DOCUMENT_NUMBER_COLUMN>]),
        @DocumentDate = H.[<DOCUMENT_DATE_COLUMN>],
        @PartyAccountCode = P.Tree_Code,
        @GrossTotal = CONVERT(NUMERIC(18,3), H.[<GROSS_TOTAL_COLUMN>]),
        @DiscountAmount = CONVERT(NUMERIC(18,3), ISNULL(H.[<DISCOUNT_COLUMN>], 0)),
        @NetAmount = CONVERT(NUMERIC(18,3), H.[<NET_TOTAL_COLUMN>]),
        @CurrencyId = ISNULL(H.[<CURRENCY_ID_COLUMN>], 1),
        @ExchangeRate = CONVERT(NUMERIC(18,3), ISNULL(H.[<EXCHANGE_RATE_COLUMN>], 1))
        FROM dbo.[<SALES_HEADER_TABLE>] AS H WITH (UPDLOCK, HOLDLOCK)
    INNER JOIN dbo.[<PARTY_TABLE>] AS P
        ON P.[<PARTY_ID_COLUMN>] = H.[<HEADER_PARTY_ID_COLUMN>]
        WHERE H.[<DOCUMENT_ID_COLUMN>] = @DocumentId
      AND H.[<APPROVED_COLUMN>] = 1
      AND H.[<VOID_COLUMN>] = 0
      AND H.JournalId IS NULL;

        IF @SourceKey IS NULL
            RAISERROR(N'المستند غير موجود أو غير صالح للترحيل.', 16, 1);

    /* افصل استعلام المرتجع إذا كانت تفاصيله في جدول مستقل. */
        INSERT INTO @Lines
    (
        [LineNo], SourceLineKey, InventoryAccountCode,
        RevenueAccountCode, SalesReturnAccountCode, COGSAccountCode,
        Quantity, LineGross, UnitCost, CurrencyId, CostCenterId
    )
        SELECT
        ROW_NUMBER() OVER (ORDER BY D.[<DETAIL_ID_COLUMN>]),
        CONVERT(NVARCHAR(100), D.[<DETAIL_ID_COLUMN>]),
        S.Tree_Code, NULL, NULL, NULL,
        CONVERT(NUMERIC(18,3), D.[<QUANTITY_COLUMN>]),
        CONVERT(NUMERIC(18,3), D.[<LINE_TOTAL_COLUMN>]),
        CONVERT(NUMERIC(18,3), D.[<UNIT_COST_COLUMN>]),
        @CurrencyId,
        D.[<COST_CENTER_ID_COLUMN>]
        FROM dbo.[<SALES_DETAIL_TABLE>] AS D
    INNER JOIN dbo.[<STORE_TABLE>] AS S
        ON S.[<STORE_ID_COLUMN>] = D.[<DETAIL_STORE_ID_COLUMN>]
        WHERE D.[<DETAIL_DOCUMENT_ID_COLUMN>] = @DocumentId
      AND D.[<DETAIL_APPROVED_COLUMN>] = 1;

        EXEC dbo.ACC_PostSalesDocument
        @DocumentKind = @DocumentKind,
        @SourceTable = N'<SALES_SOURCE_CODE>',
        @SourceKey = @SourceKey,
        @SourceId = @SourceId,
        @DocumentNumber = @DocumentNumber,
        @DocumentDate = @DocumentDate,
        @PartyAccountCode = @PartyAccountCode,
        @GrossTotal = @GrossTotal,
        @DiscountAmount = @DiscountAmount,
        @NetAmount = @NetAmount,
        @CurrencyId = @CurrencyId,
        @ExchangeRate = @ExchangeRate,
        @UserId = @UserId,
        @Lines = @Lines,
        @NewJournalId = @NewJournalId OUTPUT,
        @JournalNumber = @JournalNumber OUTPUT,
        @ErrorMessage = @ErrorMessage OUTPUT;

        UPDATE dbo.[<SALES_HEADER_TABLE>]
        SET JournalId = @NewJournalId, PostedAt = GETDATE(), PostedBy = @UserId
        WHERE [<DOCUMENT_ID_COLUMN>] = @DocumentId
          AND JournalId IS NULL;

        IF @@ROWCOUNT <> 1
            RAISERROR(N'تعذر ربط القيد بالمستند بسبب تعديل متزامن.', 16, 1);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        SET @ErrorMessage = LEFT(N'[<PROJECT>_PostSalesDocument] ' + ERROR_MESSAGE(), 500);
        SET @NewJournalId = NULL;
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH;
END;
GO
