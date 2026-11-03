/* AuditInventoryLocalCost036: same reviewed arithmetic; local snapshot lives in this procedure only. */

CREATE PROCEDURE [dbo].[AuditInventory_ItemCost036]

    @IM_ID INT

AS
BEGIN TRY
BEGIN TRANSACTION 




DECLARE @I INT , @PREV_QTY INT = 0 , @PREV_COST FLOAT = 0 , @NEW_QTY INT = 0 , @NEW_COST FLOAT = 0 , @DATE DATETIME , @Loop_DATE DATETIME
,@AVG_RUNTIME_COST FLOAT = 0 , @AVG_COSTChange FLOAT = 0

UPDATE Pch_Details SET AVG_Cost = 0 , AVG_COSTChange = 0 WHERE IM_ID = @IM_ID

CREATE TABLE [#AuditPchCost036](
	[T_ID] [bigint] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Pch_T_ID] [int] NOT NULL,
	[IM_ID] [int] NOT NULL,
	[U_ID] [bigint] NOT NULL,
	[ST_ID] [bigint] NOT NULL,
	[Price] [float] NOT NULL,
	[D_Vaild] [nvarchar](50) NOT NULL,
	[QYT] [float] NOT NULL,
	[Total] [float] NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[Barcode] [nvarchar](100) NULL,
	[NewSale] [numeric](18, 2) NULL,
	[NewSaleByOne] [numeric](18, 2) NULL,
	[U_Cargo] [float] NOT NULL,
	[ST_QTY] [numeric](18, 2) NOT NULL,
	[isDepended] [bit] NOT NULL,
	[Min_SP] [numeric](18, 2) NULL,
	[Min_SP_ByOne] [numeric](18, 2) NULL,
	[AVG_Cost] [float] NULL,
	[Min_SP_2] [numeric](18, 2) NULL,
	[Min_SP_2_ByOne] [numeric](18, 2) NULL,
	[AVG_COSTChange] [float] NULL,
	[ST_DIF_QTY] [numeric](18, 2) NULL,
)


INSERT INTO #AuditPchCost036 SELECT 
       [T_ID]
      ,[Date]
      ,[Pch_T_ID]
      ,[IM_ID]
      ,[U_ID]
      ,[ST_ID]
      ,[Price]
      ,[D_Vaild]
      ,[QYT]
      ,[Total]
      ,[Notes]
      ,[Barcode]
      ,[NewSale]
      ,[NewSaleByOne]
      ,[U_Cargo]
      ,[ST_QTY]
      ,[isDepended]
      ,[Min_SP]
      ,[Min_SP_ByOne]
      ,[AVG_Cost]
      ,[Min_SP_2]
      ,[Min_SP_2_ByOne]
      ,[AVG_COSTChange]
      ,[ST_DIF_QTY]
  FROM [dbo].[Pch_Details] ORDER BY DATE ASC




SELECT @I = MIN(T_ID) FROM #AuditPchCost036 WHERE IM_ID = @IM_ID AND isDepended = 1
WHILE (@I) IS NOT NULL
BEGIN

select @Loop_DATE = date from #AuditPchCost036 where T_ID = @I

SELECT @NEW_QTY = ST_QTY , @NEW_COST = (Price / U_Cargo) , @DATE = DATE FROM #AuditPchCost036 WHERE DATE = @Loop_DATE  AND IM_ID = @IM_ID AND T_ID = @I

EXEC @PREV_QTY = [CALC_IM_QTY_IN_specific_Range_Date] @IM_ID,@DATE
IF @PREV_QTY < 0 BEGIN SET @PREV_QTY = 0 END

IF ( @PREV_QTY + @NEW_QTY) > 0
BEGIN

SELECT @AVG_RUNTIME_COST = ((@NEW_QTY * @NEW_COST ) + (@PREV_QTY * @PREV_COST)) / ( @PREV_QTY + @NEW_QTY)
SELECT @AVG_COSTChange = @AVG_COSTChange + @AVG_RUNTIME_COST 



UPDATE Pch_Details SET AVG_Cost = @AVG_RUNTIME_COST , AVG_COSTChange = (@AVG_RUNTIME_COST - @PREV_COST) WHERE DATE = @Loop_DATE AND IM_ID = @IM_ID AND T_ID = @I


--UPDATE SB_Contents SET Cost = @AVG_RUNTIME_COST * U_Cargo WHERE IM_ID = @IM_ID AND DATE >= @DATE
--UPDATE Rtn_Contents SET Cost = @AVG_RUNTIME_COST * U_Cargo WHERE IM_ID = @IM_ID AND DATE >= @DATE
--UPDATE InSale_Details SET Cost = @AVG_RUNTIME_COST * U_Cargo WHERE IM_ID = @IM_ID AND DATE >= @DATE
--UPDATE Outsale_Details SET Cost = @AVG_RUNTIME_COST * U_Cargo WHERE IM_ID = @IM_ID AND DATE >= @DATE
--UPDATE FRM_Contents_Details SET Cost = @AVG_RUNTIME_COST * U_Cargo WHERE IM_ID = @IM_ID AND DATE >= @DATE
--UPDATE Store_Trans_Details SET Price = @AVG_RUNTIME_COST * U_Cargo  WHERE IM_ID = @IM_ID AND  DATE >= @DATE
--UPDATE IMEX_Details SET Cost = @AVG_RUNTIME_COST * U_Cargo  WHERE IM_ID = @IM_ID AND  DATE >= @DATE

--UPDATE ST_QTY_Deferences SET  Cost = @AVG_RUNTIME_COST * U_Cargo  WHERE IM_ID = @IM_ID AND  DATE >= @DATE


SELECT  @PREV_COST = @AVG_RUNTIME_COST

END


SELECT @I = MIN(T_ID) FROM #AuditPchCost036 WHERE IM_ID = @IM_ID AND isDepended = 1 AND T_ID > @I
END

UPDATE IM_Menu SET Cost = 
(SELECT ISNULL(SUM(AVG_COSTChange),0) FROM Pch_Details WHERE isDepended = 1 AND IM_ID = @IM_ID )
WHERE IM_ID = @IM_ID



--UPDATE IM_Menu SET Cost = @NEW_COST WHERE IM_ID = @IM_ID


COMMIT
END TRY

BEGIN CATCH
IF @@TRANCOUNT > 0
DECLARE @ERR_STR NVARCHAR(MAX)
       SET @ERR_STR  = '[ReCount_IM_Main_COST] ' + ERROR_MESSAGE()
	   raiserror(@ERR_STR,16,1,'خطأ')
ROLLBACK
END CATCH













