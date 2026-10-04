SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.ACC_GetNextJournalNumber', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.ACC_GetNextJournalNumber AS RETURN 0;');
GO

ALTER PROCEDURE dbo.ACC_GetNextJournalNumber
    @EntryDate   DATE,
    @NextNumber VARCHAR(50) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @FiscalYear INT;
    DECLARE @PeriodNo TINYINT;
    DECLARE @CounterPeriod TINYINT;
    DECLARE @LastNumber INT;
    DECLARE @Prefix NVARCHAR(20);
    DECLARE @NumberLength INT;
    DECLARE @ResetType VARCHAR(10);

    SELECT TOP (1)
        @FiscalYear = FiscalYear,
        @PeriodNo = PeriodNo
    FROM dbo.ACC_FISCAL_PERIODS
    WHERE @EntryDate BETWEEN DateFrom AND DateTo
      AND IsClosed = 0;

    IF @FiscalYear IS NULL
    BEGIN
        RAISERROR(N'لا توجد فترة مالية مفتوحة لتاريخ القيد.', 16, 1);
        RETURN;
    END;

    SELECT
        @Prefix = JournalPrefix,
        @NumberLength = JournalNumberLength,
        @ResetType = JournalResetType
    FROM dbo.ACC_PORTABLE_SETTINGS
    WHERE SettingsId = 1;

    SET @CounterPeriod = CASE WHEN @ResetType = 'MONTH' THEN @PeriodNo ELSE 0 END;

    UPDATE dbo.ACC_JOURNAL_COUNTERS WITH (UPDLOCK, HOLDLOCK)
    SET
        @LastNumber = LastNumber = LastNumber + 1
    WHERE FiscalYear = @FiscalYear
      AND PeriodNo = @CounterPeriod;

    IF @LastNumber IS NULL
    BEGIN
        INSERT INTO dbo.ACC_JOURNAL_COUNTERS(FiscalYear, PeriodNo, LastNumber)
        VALUES (@FiscalYear, @CounterPeriod, 1);
        SET @LastNumber = 1;
    END;

    IF @ResetType = 'MONTH'
    BEGIN
        SET @NextNumber = CONVERT(VARCHAR(20), @Prefix)
            + '-' + CONVERT(VARCHAR(4), @FiscalYear)
            + RIGHT('0' + CONVERT(VARCHAR(2), @PeriodNo), 2)
            + '-' + RIGHT(REPLICATE('0', @NumberLength) + CONVERT(VARCHAR(20), @LastNumber), @NumberLength);
    END
    ELSE
    BEGIN
        SET @NextNumber = CONVERT(VARCHAR(20), @Prefix)
            + '-' + CONVERT(VARCHAR(4), @FiscalYear)
            + '-' + RIGHT(REPLICATE('0', @NumberLength) + CONVERT(VARCHAR(20), @LastNumber), @NumberLength);
    END;
END;
GO

