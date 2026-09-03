USE [CPOS_ACCOUNTING]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
    إصلاح الرصيد المتحرك للخزينة:
    - ترتيب حتمي حسب التاريخ ثم رقم الحركة.
    - الحركة الملغية لا تغير الرصيد.
    - يبقى رصيد صف الحركة الملغية مساويًا لرصيد الصف السابق.
    متوافق مع SQL Server 2014.
*/
ALTER PROCEDURE [dbo].[ReCount_Tresuary_balance]
    @Tr_ID INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    ;WITH RunningBalances AS
    (
        SELECT
            M.T_ID,
            SUM
            (
                CONVERT
                (
                    DECIMAL(38, 3),
                    CASE
                        WHEN ISNULL(M.isVoid, 0) = 0
                            THEN ISNULL(M.Credit, 0) - ISNULL(M.Debit, 0)
                        ELSE 0
                    END
                )
            ) OVER
            (
                PARTITION BY M.Tr_ID
                ORDER BY M.[date], M.T_ID
                ROWS UNBOUNDED PRECEDING
            ) AS RunningBalance
        FROM dbo.Treasury_Balance_MV AS M
        WHERE M.Tr_ID = @Tr_ID
    )
    UPDATE M
       SET M.Balance = CONVERT(NUMERIC(18, 3), R.RunningBalance)
    FROM dbo.Treasury_Balance_MV AS M
    INNER JOIN RunningBalances AS R ON R.T_ID = M.T_ID
    WHERE M.Tr_ID = @Tr_ID;
END
GO

/* إصلاح الأرصدة المخزنة حاليًا لكل الخزائن بعد تثبيت الإجراء. */
DECLARE @TreasuryID INT;
DECLARE TreasuryCursor CURSOR LOCAL FAST_FORWARD FOR
    SELECT DISTINCT Tr_ID
    FROM dbo.Treasury_Balance_MV
    WHERE Tr_ID IS NOT NULL;

OPEN TreasuryCursor;
FETCH NEXT FROM TreasuryCursor INTO @TreasuryID;

WHILE @@FETCH_STATUS = 0
BEGIN
    EXEC dbo.ReCount_Tresuary_balance @Tr_ID = @TreasuryID;
    FETCH NEXT FROM TreasuryCursor INTO @TreasuryID;
END;

CLOSE TreasuryCursor;
DEALLOCATE TreasuryCursor;
GO

