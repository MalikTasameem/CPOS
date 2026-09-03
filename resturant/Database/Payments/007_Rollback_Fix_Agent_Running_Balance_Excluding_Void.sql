USE [CPOS_ACCOUNTING]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* يعيد آلية إعادة ترصيد كشف العملاء السابقة. */
ALTER PROCEDURE [dbo].[ReCount_Agent_balance]
    @AG_ID INT
AS
BEGIN
    DECLARE @Balance MONEY = 0;
    DECLARE @bal NUMERIC(18, 3) = 0;

    ;WITH q AS
    (
        SELECT TOP 10000000 *
        FROM dbo.AG_MV_Report
        WHERE AG_ID = @AG_ID
          AND isVoid = 0
        ORDER BY [Date], [T_ID]
    )
    UPDATE q
       SET @bal = Balance = @bal + (ISNULL(Debit, 0) - ISNULL(Credit, 0));
END
GO

