USE [CPOS_ACCOUNTING]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
    إصلاح الرصيد المتحرك لكشف العملاء:
    - ترتيب حتمي حسب العميل والتاريخ ورقم الحركة.
    - الحركة الملغية لا تغير الرصيد.
    - @AG_ID = 0 يعيد ترصيد كل العملاء الموجودين في جدول التقرير.
    متوافق مع SQL Server 2014.
*/
ALTER PROCEDURE [dbo].[ReCount_Agent_balance]
    @AG_ID INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    ;WITH RunningBalances AS
    (
        SELECT
            R.IDX_ID,
            R.AG_ID,
            R.Type_ID,
            R.T_ID,
            R.[Date],
            SUM
            (
                CONVERT
                (
                    DECIMAL(38, 3),
                    CASE
                        WHEN ISNULL(R.isVoid, 0) = 0
                            THEN ISNULL(R.Debit, 0) - ISNULL(R.Credit, 0)
                        ELSE 0
                    END
                )
            ) OVER
            (
                PARTITION BY R.AG_ID
                ORDER BY R.[Date], R.Type_ID, R.T_ID, R.IDX_ID
                ROWS UNBOUNDED PRECEDING
            ) AS RunningBalance
        FROM dbo.AG_MV_Report AS R
        WHERE @AG_ID = 0 OR R.AG_ID = @AG_ID
    )
    UPDATE R
       SET R.Balance = CONVERT(NUMERIC(18, 3), B.RunningBalance)
    FROM dbo.AG_MV_Report AS R
    INNER JOIN RunningBalances AS B ON B.IDX_ID = R.IDX_ID
    WHERE @AG_ID = 0 OR R.AG_ID = @AG_ID;
END
GO
