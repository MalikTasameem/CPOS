USE [CPOS_ACCOUNTING]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* يعيد آلية إعادة ترصيد الخزينة السابقة. */
ALTER PROCEDURE [dbo].[ReCount_Tresuary_balance]
    @Tr_ID INT
AS
BEGIN
    DECLARE @ela_id NVARCHAR(10) = @Tr_ID;
    DECLARE @bal NUMERIC(18, 3) = 0;

    ;WITH q AS
    (
        SELECT TOP 10000000 *
        FROM Treasury_Balance_MV
        WHERE Tr_ID = @ela_id
          AND isVoid = 0
        ORDER BY [date], [T_ID]
    )
    UPDATE q
       SET @bal = Balance = @bal + (ISNULL(Credit, 0) - ISNULL(Debit, 0));
END
GO

