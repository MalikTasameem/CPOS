CREATE PROCEDURE [dbo].[ViewBill_Discount_Distribute_Audited] 
@T_ID	int,
@Discount_Val Numeric(18,2)= NULL
AS

declare @isError int , @SUM_QTY Numeric(18,2) , @j INT , @U_ID INT , @IM_ID INT , @SLAEPRICE Numeric(18,2)
IF NOT EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@T_ID AND BsType_ID=16 AND isVoid=0) THROW 51137,'Expected active quote document.',1;
-- QuoteDistribution037: quote rows only; preserve the original distribution formula.
BEGIN TRAN

IF @Discount_Val IS NOT NULL 
BEGIN
SELECT @j = min(T_ID) From ViewBill_Contents WHERE Bill_T_ID = @T_ID
WHILE @j is Not NULL
BEGIN
SELECT @U_ID = U_ID ,@IM_ID = IM_ID FROM ViewBill_Contents WHERE T_ID = @j
SELECT @SLAEPRICE = Price FROM IM_Units WHERE IM_ID = @IM_ID and U_ID = @U_ID 
Update ViewBill_Contents SET Price =@SLAEPRICE, T_Price = (QTY * @SLAEPRICE) 
WHERE T_ID = @j 
SELECT @j = min(T_ID) From ViewBill_Contents WHERE Bill_T_ID = @T_ID AND T_ID > @j
END
-------------------------------------------------------------------------------------------
SELECT @SUM_QTY = ISNULL(SUM(QTY),0) FROM ViewBill_Contents WHERE Bill_T_ID = @T_ID
Update ViewBill_Contents SET Price = Price -  (@Discount_Val / @SUM_QTY) , T_Price = (QTY * (Price - (@Discount_Val / @SUM_QTY)))
WHERE Bill_T_ID = @T_ID 

UPDATE Agents_Balance_MV SET Discount = @Discount_Val WHERE T_ID = @T_ID

END
ELSE
BEGIN

SELECT @j = min(T_ID) From ViewBill_Contents WHERE Bill_T_ID = @T_ID
WHILE @j is Not NULL
BEGIN
SELECT @U_ID = U_ID ,@IM_ID = IM_ID FROM ViewBill_Contents WHERE T_ID = @j
SELECT @SLAEPRICE = Price FROM IM_Units WHERE IM_ID = @IM_ID and U_ID = @U_ID 
Update ViewBill_Contents SET Price =@SLAEPRICE, T_Price = (QTY * @SLAEPRICE) 
WHERE T_ID = @j
SELECT @j = min(T_ID) From ViewBill_Contents WHERE Bill_T_ID = @T_ID AND T_ID > @j

UPDATE Agents_Balance_MV SET Discount = NULL WHERE T_ID = @T_ID
END


END

	select @isError = @@ERROR 
	IF @isError <> 0 
	goto PROBLEM

		COMMIT TRAN

	PROBLEM:
	if @isError <> 0 begin
	print 'DB ERROR ! _ SB_Discount_Distribute '
	ROLLBACK TRAN

END
