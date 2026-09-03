USE [CPOS_ACCOUNTING]
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @TrDefinition NVARCHAR(MAX) = OBJECT_DEFINITION(OBJECT_ID(N'dbo.Search_Tr_MV'));
    DECLARE @TrOldExpression NVARCHAR(500) =
        N'SUM(ISNULL(Credit, 0) - ISNULL(Debit, 0)) OVER';
    DECLARE @TrNewExpression NVARCHAR(1000) =
        N'SUM(CASE WHEN ISNULL(isVoid, 0) = 1 THEN 0 ' +
        N'ELSE ISNULL(Credit, 0) - ISNULL(Debit, 0) END) OVER';

    IF @TrDefinition IS NULL
        RAISERROR(N'الإجراء dbo.Search_Tr_MV غير موجود.', 16, 1);

    IF CHARINDEX(@TrNewExpression, @TrDefinition) = 0
    BEGIN
        IF CHARINDEX(@TrOldExpression, @TrDefinition) = 0
            RAISERROR(N'تعذر العثور على معادلة الرصيد المتحرك المتوقعة داخل Search_Tr_MV.', 16, 1);

        SET @TrDefinition = REPLACE(@TrDefinition, @TrOldExpression, @TrNewExpression);
    END;

    SET @TrDefinition = REPLACE(
        @TrDefinition,
        N'CREATE PROCEDURE [dbo].[Search_Tr_MV]',
        N'ALTER PROCEDURE [dbo].[Search_Tr_MV]');

    EXEC sys.sp_executesql @TrDefinition;

    DECLARE @AgDefinition NVARCHAR(MAX) = OBJECT_DEFINITION(OBJECT_ID(N'dbo.Search_AG_MV'));
    DECLARE @AgOldOpeningFilter NVARCHAR(1000) =
        N'AND @AllRecieptsCheckBox = 1' + CHAR(10) +
        N'            AND @AllTimeCheckBox = 0';
    DECLARE @AgVoidOnlyOpeningFilter NVARCHAR(1200) =
        N'AND @AllRecieptsCheckBox = 1' + CHAR(10) +
        N'            AND @AllTimeCheckBox = 0' + CHAR(10) +
        N'            AND ISNULL(isVoid, 0) = 0';
    DECLARE @AgNewOpeningFilter NVARCHAR(2000) =
        @AgVoidOnlyOpeningFilter + CHAR(10) +
        N'            AND [Date] = (SELECT MAX(WB.[Date]) FROM WithBalance AS WB' + CHAR(10) +
        N'                          WHERE WB.AG_ID = @AG_ID' + CHAR(10) +
        N'                            AND CONVERT(DATE, WB.[Date]) < @DateTimePicker_From' + CHAR(10) +
        N'                            AND ISNULL(WB.isVoid, 0) = 0)';

    IF @AgDefinition IS NULL
        RAISERROR(N'الإجراء dbo.Search_AG_MV غير موجود.', 16, 1);

    SET @AgDefinition = REPLACE(@AgDefinition, CHAR(13), N'');

    IF CHARINDEX(@AgNewOpeningFilter, @AgDefinition) = 0
    BEGIN
        IF CHARINDEX(@AgVoidOnlyOpeningFilter, @AgDefinition) > 0
            SET @AgDefinition = REPLACE(@AgDefinition, @AgVoidOnlyOpeningFilter, @AgNewOpeningFilter);
        ELSE IF CHARINDEX(@AgOldOpeningFilter, @AgDefinition) > 0
            SET @AgDefinition = REPLACE(@AgDefinition, @AgOldOpeningFilter, @AgNewOpeningFilter);
        ELSE
            RAISERROR(N'تعذر العثور على شروط الرصيد الافتتاحي المتوقعة داخل Search_AG_MV.', 16, 1);
    END;

    SET @AgDefinition = REPLACE(
        @AgDefinition,
        N'CREATE PROCEDURE [dbo].[Search_AG_MV]',
        N'ALTER PROCEDURE [dbo].[Search_AG_MV]');

    EXEC sys.sp_executesql @AgDefinition;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO
