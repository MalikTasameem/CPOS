/* SQL Server 2014. CODE ONLY: install only after approval and exclusive-session checks.
   No business table/schema/formula changes. New local-temp procedures replace only audit-routed global cost recount calls. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51136,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51136,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51136,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51136,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.QuoteLines"]')=1) THROW 51136,'Unexpected source or prerequisite 034.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51136,'Incomplete audit requests.',1;
 IF OBJECT_ID('dbo.AuditInventory_ItemCost036') IS NOT NULL OR OBJECT_ID('dbo.AuditInventory_AllCosts036') IS NOT NULL THROW 51136,'Local cost procedure already exists.',1;
 EXEC sys.sp_executesql N'/* AuditInventoryLocalCost036: same reviewed arithmetic; local snapshot lives in this procedure only. */

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
       SET @ERR_STR  = ''[ReCount_IM_Main_COST] '' + ERROR_MESSAGE()
	   raiserror(@ERR_STR,16,1,''خطأ'')
ROLLBACK
END CATCH












';
 EXEC sys.sp_executesql N'/* AuditInventoryLocalCost036: all items use the isolated per-item implementation. */

CREATE PROCEDURE [dbo].[AuditInventory_AllCosts036]

AS
BEGIN TRY

BEGIN TRANSACTION 

-- The original initial global snapshot had no business side effects; each item still takes its own snapshot.




DECLARE @I INT = 0 
SELECT @I = MIN(IM_ID) FROM IM_Menu WHERE isStore IN (1,2)
WHILE @I IS NOT NULL
BEGIN

--IF (SELECT IM_ID FROM FRM_Details WHERE IM_ID = @I) IS NULL
BEGIN EXEC [AuditInventory_ItemCost036] @I END

SELECT @I = MIN(IM_ID) FROM IM_Menu WHERE IM_ID > @I AND isStore IN (1,2)
 
END


COMMIT
END TRY

BEGIN CATCH
IF @@TRANCOUNT > 0
DECLARE @ERR_STR NVARCHAR(MAX)
       SET @ERR_STR  = ''[RECOUNT_IM_ALL_MainCost] '' + ERROR_MESSAGE()
	   raiserror(@ERR_STR,16,1,''خطأ'')
ROLLBACK
END CATCH










';
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Inventory.PurchaseLineDeleted',@sql)>0 OR CHARINDEX('Quote.LineAdded',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51136,'Unexpected begin baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''Inventory.PurchaseLineDeleted'',''Inventory.PurchaseLineChanged'',''Manufacturing.AutoProductAdded'',''Manufacturing.ManualLineAdded'',''Manufacturing.ProductDeleted'',''Manufacturing.ProductChanged'',''Manufacturing.IngredientDeleted'',''Manufacturing.CostRecalculated'',''Manufacturing.PurchaseCopied'',''Manufacturing.SalesCopied'',''Inventory.QuantityRecounted'',''Inventory.ItemCostRecounted'',''Inventory.AllCostsRecounted'') AND @DomainCode=''Inventory.Operations'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Inventory.PurchaseLineDeleted',@sql)>0 OR CHARINDEX(@old,@sql)=0 OR CHARINDEX('Quote.LineAdded',@sql)=0 THROW 51136,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''Inventory.PurchaseLineDeleted'',''Inventory.PurchaseLineChanged'',''Manufacturing.AutoProductAdded'',''Manufacturing.ManualLineAdded'',''Manufacturing.ProductDeleted'',''Manufacturing.ProductChanged'',''Manufacturing.IngredientDeleted'',''Manufacturing.CostRecalculated'',''Manufacturing.PurchaseCopied'',''Manufacturing.SalesCopied'',''Inventory.QuantityRecounted'',''Inventory.ItemCostRecounted'',''Inventory.AllCostsRecounted'') AND @domain=''Inventory.Operations'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51136,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Inventory.PurchaseLineDeleted'',''Inventory.PurchaseLineChanged'',''Manufacturing.AutoProductAdded'',''Manufacturing.ManualLineAdded'',''Manufacturing.ProductDeleted'',''Manufacturing.ProductChanged'',''Manufacturing.IngredientDeleted'',''Manufacturing.CostRecalculated'',''Manufacturing.PurchaseCopied'',''Manufacturing.SalesCopied'',''Inventory.QuantityRecounted'',''Inventory.ItemCostRecounted'',''Inventory.AllCostsRecounted'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51136,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditInventoryOperations036: reviewed purchase/manufacturing/recount graph.
 IF @operation IN(''Inventory.PurchaseLineDeleted'',''Inventory.PurchaseLineChanged'',''Manufacturing.AutoProductAdded'',''Manufacturing.ManualLineAdded'',''Manufacturing.ProductDeleted'',''Manufacturing.ProductChanged'',''Manufacturing.IngredientDeleted'',''Manufacturing.CostRecalculated'',''Manufacturing.PurchaseCopied'',''Manufacturing.SalesCopied'',''Inventory.QuantityRecounted'',''Inventory.ItemCostRecounted'',''Inventory.AllCostsRecounted'')
 BEGIN
  DECLARE @iOwner nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @iScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'');
  IF @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>''InventoryOperation'' OR ISNULL(TRY_CONVERT(int,@iOwner),0)<=0
   OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0 THROW 51136,''Invalid inventory event identity.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=''InventoryOperation'' AND EntityId=@iOwner) THROW 51136,''Inventory request mismatch.'',1;
  IF NOT ((@operation=''Inventory.PurchaseLineDeleted'' AND @iScreen IN(''Pch'',''Invoice'')) OR (@operation=''Inventory.PurchaseLineChanged'' AND @iScreen IN(''Change_IM_Details'')) OR (@operation=''Manufacturing.AutoProductAdded'' AND @iScreen IN(''Frm_auto_IM_card'',''Frm_manuel_IM_card'')) OR (@operation=''Manufacturing.ManualLineAdded'' AND @iScreen IN(''Frm_manuel_IM_card'')) OR (@operation=''Manufacturing.ProductDeleted'' AND @iScreen IN(''Format_Items_Auto'',''Format_Items_Manual'')) OR (@operation=''Manufacturing.ProductChanged'' AND @iScreen IN(''Change_IM_Details'')) OR (@operation=''Manufacturing.IngredientDeleted'' AND @iScreen IN(''Format_Items_Manual'')) OR (@operation=''Manufacturing.CostRecalculated'' AND @iScreen IN(''Format_Items_Manual'')) OR (@operation=''Manufacturing.PurchaseCopied'' AND @iScreen IN(''Format_Items_Manual'')) OR (@operation=''Manufacturing.SalesCopied'' AND @iScreen IN(''Format_Items_Manual'')) OR (@operation=''Inventory.QuantityRecounted'' AND @iScreen IN(''STORES_Explorer'',''IM_CYRCLE'')) OR (@operation=''Inventory.ItemCostRecounted'' AND @iScreen IN(''Recount_IM_Cost'')) OR (@operation=''Inventory.AllCostsRecounted'' AND @iScreen IN(''STORES_Explorer'',''IM_CYRCLE''))) THROW 51136,''Invalid inventory screen.'',1;
  DECLARE @iFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40),TN sysname,KN sysname,CN sysname);
  INSERT @iFields VALUES (''InvDocument'',''InvDocument_T_ID'',''Integer'',''int'',''Agents_Balance_MV'',''T_ID'',''T_ID''),
(''InvDocument'',''InvDocument_AG_ID'',''Integer'',''int'',''Agents_Balance_MV'',''T_ID'',''AG_ID''),
(''InvDocument'',''InvDocument_Pch_ID'',''Integer'',''int'',''Agents_Balance_MV'',''T_ID'',''Pch_ID''),
(''InvDocument'',''InvDocument_Jrd_ID'',''Integer'',''int'',''Agents_Balance_MV'',''T_ID'',''Jrd_ID''),
(''InvDocument'',''InvDocument_Frm_ID'',''Integer'',''int'',''Agents_Balance_MV'',''T_ID'',''Frm_ID''),
(''InvDocument'',''InvDocument_Frm_ID_M'',''Integer'',''int'',''Agents_Balance_MV'',''T_ID'',''Frm_ID_M''),
(''InvDocument'',''InvDocument_Date'',''DateTime'',''datetime'',''Agents_Balance_MV'',''T_ID'',''Date''),
(''InvDocument'',''InvDocument_Total'',''Decimal'',''numeric(18,3)'',''Agents_Balance_MV'',''T_ID'',''Total''),
(''InvDocument'',''InvDocument_Discount'',''Decimal'',''numeric(18,3)'',''Agents_Balance_MV'',''T_ID'',''Discount''),
(''InvDocument'',''InvDocument_Pure'',''Decimal'',''numeric(18,3)'',''Agents_Balance_MV'',''T_ID'',''Pure''),
(''InvDocument'',''InvDocument_Debit'',''Decimal'',''numeric(18,3)'',''Agents_Balance_MV'',''T_ID'',''Debit''),
(''InvDocument'',''InvDocument_Credit'',''Decimal'',''numeric(18,3)'',''Agents_Balance_MV'',''T_ID'',''Credit''),
(''InvDocument'',''InvDocument_Balance'',''Decimal'',''numeric(18,3)'',''Agents_Balance_MV'',''T_ID'',''Balance''),
(''InvDocument'',''InvDocument_BsType_ID'',''Integer'',''int'',''Agents_Balance_MV'',''T_ID'',''BsType_ID''),
(''InvDocument'',''InvDocument_isDepended'',''Integer'',''int'',''Agents_Balance_MV'',''T_ID'',''isDepended''),
(''InvDocument'',''InvDocument_isVoid'',''Integer'',''int'',''Agents_Balance_MV'',''T_ID'',''isVoid''),
(''InvDocument'',''InvDocument_User_ID'',''Integer'',''int'',''Agents_Balance_MV'',''T_ID'',''User_ID''),
(''InvDocument'',''InvDocument_JournalId'',''Integer'',''int'',''Agents_Balance_MV'',''T_ID'',''JournalId''),
(''InvDocument'',''InvDocument_NeedRepost'',''Boolean'',''bit'',''Agents_Balance_MV'',''T_ID'',''NeedRepost''),
(''InvDocument'',''InvDocument_EditOpenedAt'',''DateTime'',''datetime'',''Agents_Balance_MV'',''T_ID'',''EditOpenedAt''),
(''InvDocument'',''InvDocument_EditOpenedBy'',''Integer'',''int'',''Agents_Balance_MV'',''T_ID'',''EditOpenedBy''),
(''InvDocument'',''InvDocument_NeedCancelReverse'',''Boolean'',''bit'',''Agents_Balance_MV'',''T_ID'',''NeedCancelReverse''),
(''InvPurchase'',''InvPurchase_T_ID'',''Integer'',''bigint'',''Pch_Details'',''T_ID'',''T_ID''),
(''InvPurchase'',''InvPurchase_Date'',''DateTime'',''datetime'',''Pch_Details'',''T_ID'',''Date''),
(''InvPurchase'',''InvPurchase_Pch_T_ID'',''Integer'',''int'',''Pch_Details'',''T_ID'',''Pch_T_ID''),
(''InvPurchase'',''InvPurchase_IM_ID'',''Integer'',''int'',''Pch_Details'',''T_ID'',''IM_ID''),
(''InvPurchase'',''InvPurchase_U_ID'',''Integer'',''bigint'',''Pch_Details'',''T_ID'',''U_ID''),
(''InvPurchase'',''InvPurchase_ST_ID'',''Integer'',''bigint'',''Pch_Details'',''T_ID'',''ST_ID''),
(''InvPurchase'',''InvPurchase_Price'',''Float'',''float'',''Pch_Details'',''T_ID'',''Price''),
(''InvPurchase'',''InvPurchase_D_Vaild'',''String'',''nvarchar(50)'',''Pch_Details'',''T_ID'',''D_Vaild''),
(''InvPurchase'',''InvPurchase_QYT'',''Float'',''float'',''Pch_Details'',''T_ID'',''QYT''),
(''InvPurchase'',''InvPurchase_Total'',''Float'',''float'',''Pch_Details'',''T_ID'',''Total''),
(''InvPurchase'',''InvPurchase_Notes'',''String'',''nvarchar(max)'',''Pch_Details'',''T_ID'',''Notes''),
(''InvPurchase'',''InvPurchase_Barcode'',''String'',''nvarchar(max)'',''Pch_Details'',''T_ID'',''Barcode''),
(''InvPurchase'',''InvPurchase_NewSale'',''Decimal'',''numeric(18,2)'',''Pch_Details'',''T_ID'',''NewSale''),
(''InvPurchase'',''InvPurchase_NewSaleByOne'',''Decimal'',''numeric(18,2)'',''Pch_Details'',''T_ID'',''NewSaleByOne''),
(''InvPurchase'',''InvPurchase_U_Cargo'',''Float'',''float'',''Pch_Details'',''T_ID'',''U_Cargo''),
(''InvPurchase'',''InvPurchase_ST_QTY'',''Decimal'',''numeric(18,2)'',''Pch_Details'',''T_ID'',''ST_QTY''),
(''InvPurchase'',''InvPurchase_isDepended'',''Boolean'',''bit'',''Pch_Details'',''T_ID'',''isDepended''),
(''InvPurchase'',''InvPurchase_Min_SP'',''Decimal'',''numeric(18,2)'',''Pch_Details'',''T_ID'',''Min_SP''),
(''InvPurchase'',''InvPurchase_Min_SP_ByOne'',''Decimal'',''numeric(18,2)'',''Pch_Details'',''T_ID'',''Min_SP_ByOne''),
(''InvPurchase'',''InvPurchase_AVG_Cost'',''Float'',''float'',''Pch_Details'',''T_ID'',''AVG_Cost''),
(''InvPurchase'',''InvPurchase_Min_SP_2'',''Decimal'',''numeric(18,2)'',''Pch_Details'',''T_ID'',''Min_SP_2''),
(''InvPurchase'',''InvPurchase_Min_SP_2_ByOne'',''Decimal'',''numeric(18,2)'',''Pch_Details'',''T_ID'',''Min_SP_2_ByOne''),
(''InvPurchase'',''InvPurchase_AVG_COSTChange'',''Float'',''float'',''Pch_Details'',''T_ID'',''AVG_COSTChange''),
(''InvPurchase'',''InvPurchase_ST_DIF_QTY'',''Decimal'',''numeric(18,2)'',''Pch_Details'',''T_ID'',''ST_DIF_QTY''),
(''InvPurchase'',''InvPurchase_EXP_VALUE'',''Float'',''float'',''Pch_Details'',''T_ID'',''EXP_VALUE''),
(''InvPurchase'',''InvPurchase_Org_Price'',''Float'',''float'',''Pch_Details'',''T_ID'',''Org_Price''),
(''InvPurchase'',''InvPurchase_Price_By_Foriegn_Cr'',''Float'',''float'',''Pch_Details'',''T_ID'',''Price_By_Foriegn_Cr''),
(''InvPurchase'',''InvPurchase_Price_By_Equal'',''Float'',''float'',''Pch_Details'',''T_ID'',''Price_By_Equal''),
(''InvPurchase'',''InvPurchase_Cr_ID'',''Integer'',''int'',''Pch_Details'',''T_ID'',''Cr_ID''),
(''InvProduct'',''InvProduct_T_ID'',''Integer'',''int'',''FRM_Details'',''T_ID'',''T_ID''),
(''InvProduct'',''InvProduct_Date'',''DateTime'',''datetime'',''FRM_Details'',''T_ID'',''Date''),
(''InvProduct'',''InvProduct_FRM_T_ID'',''Integer'',''int'',''FRM_Details'',''T_ID'',''FRM_T_ID''),
(''InvProduct'',''InvProduct_IM_ID'',''Integer'',''int'',''FRM_Details'',''T_ID'',''IM_ID''),
(''InvProduct'',''InvProduct_U_ID'',''Integer'',''bigint'',''FRM_Details'',''T_ID'',''U_ID''),
(''InvProduct'',''InvProduct_ST_ID'',''Integer'',''bigint'',''FRM_Details'',''T_ID'',''ST_ID''),
(''InvProduct'',''InvProduct_Price'',''Float'',''float'',''FRM_Details'',''T_ID'',''Price''),
(''InvProduct'',''InvProduct_D_Vaild'',''String'',''nvarchar(50)'',''FRM_Details'',''T_ID'',''D_Vaild''),
(''InvProduct'',''InvProduct_QYT'',''Decimal'',''numeric(18,2)'',''FRM_Details'',''T_ID'',''QYT''),
(''InvProduct'',''InvProduct_Total'',''Decimal'',''numeric(18,2)'',''FRM_Details'',''T_ID'',''Total''),
(''InvProduct'',''InvProduct_NewSale'',''Decimal'',''numeric(18,2)'',''FRM_Details'',''T_ID'',''NewSale''),
(''InvProduct'',''InvProduct_NewSaleByOne'',''Decimal'',''numeric(18,2)'',''FRM_Details'',''T_ID'',''NewSaleByOne''),
(''InvProduct'',''InvProduct_U_Cargo'',''Float'',''float'',''FRM_Details'',''T_ID'',''U_Cargo''),
(''InvProduct'',''InvProduct_ST_QTY'',''Decimal'',''numeric(18,2)'',''FRM_Details'',''T_ID'',''ST_QTY''),
(''InvProduct'',''InvProduct_isDepended'',''Boolean'',''bit'',''FRM_Details'',''T_ID'',''isDepended''),
(''InvProduct'',''InvProduct_Min_SP'',''Decimal'',''numeric(18,2)'',''FRM_Details'',''T_ID'',''Min_SP''),
(''InvProduct'',''InvProduct_Min_SP_ByOne'',''Decimal'',''numeric(18,2)'',''FRM_Details'',''T_ID'',''Min_SP_ByOne''),
(''InvProduct'',''InvProduct_Barcode'',''String'',''nvarchar(50)'',''FRM_Details'',''T_ID'',''Barcode''),
(''InvIngredient'',''InvIngredient_T_ID'',''Integer'',''int'',''FRM_Contents_Details'',''T_ID'',''T_ID''),
(''InvIngredient'',''InvIngredient_DATE'',''DateTime'',''datetime'',''FRM_Contents_Details'',''T_ID'',''DATE''),
(''InvIngredient'',''InvIngredient_Bill_T_ID'',''Integer'',''int'',''FRM_Contents_Details'',''T_ID'',''Bill_T_ID''),
(''InvIngredient'',''InvIngredient_FRM_IM_T_ID'',''Integer'',''int'',''FRM_Contents_Details'',''T_ID'',''FRM_IM_T_ID''),
(''InvIngredient'',''InvIngredient_IM_ID'',''Integer'',''int'',''FRM_Contents_Details'',''T_ID'',''IM_ID''),
(''InvIngredient'',''InvIngredient_U_ID'',''Integer'',''bigint'',''FRM_Contents_Details'',''T_ID'',''U_ID''),
(''InvIngredient'',''InvIngredient_ST_ID'',''Integer'',''bigint'',''FRM_Contents_Details'',''T_ID'',''ST_ID''),
(''InvIngredient'',''InvIngredient_D_Vaild'',''String'',''nvarchar(50)'',''FRM_Contents_Details'',''T_ID'',''D_Vaild''),
(''InvIngredient'',''InvIngredient_Cost'',''Float'',''float'',''FRM_Contents_Details'',''T_ID'',''Cost''),
(''InvIngredient'',''InvIngredient_QYT'',''Float'',''float'',''FRM_Contents_Details'',''T_ID'',''QYT''),
(''InvIngredient'',''InvIngredient_U_Cargo'',''Float'',''float'',''FRM_Contents_Details'',''T_ID'',''U_Cargo''),
(''InvIngredient'',''InvIngredient_ST_QTY'',''Decimal'',''numeric(18,3)'',''FRM_Contents_Details'',''T_ID'',''ST_QTY''),
(''InvIngredient'',''InvIngredient_isDepended'',''Integer'',''int'',''FRM_Contents_Details'',''T_ID'',''isDepended''),
(''InvIngredient'',''InvIngredient_Barcode'',''String'',''nvarchar(50)'',''FRM_Contents_Details'',''T_ID'',''Barcode''),
(''InvIngredient'',''InvIngredient_is_Not_Qty'',''Integer'',''int'',''FRM_Contents_Details'',''T_ID'',''is_Not_Qty''),
(''InvItem'',''InvItem_IM_ID'',''Integer'',''int'',''IM_Menu'',''IM_ID'',''IM_ID''),
(''InvItem'',''InvItem_item_name'',''String'',''nvarchar(500)'',''IM_Menu'',''IM_ID'',''item_name''),
(''InvItem'',''InvItem_item_nameSales'',''String'',''nvarchar(500)'',''IM_Menu'',''IM_ID'',''item_nameSales''),
(''InvItem'',''InvItem_isStore'',''Integer'',''int'',''IM_Menu'',''IM_ID'',''isStore''),
(''InvItem'',''InvItem_isValid'',''Integer'',''int'',''IM_Menu'',''IM_ID'',''isValid''),
(''InvItem'',''InvItem_Cost'',''Float'',''float'',''IM_Menu'',''IM_ID'',''Cost''),
(''InvItem'',''InvItem_Markter_Val'',''Decimal'',''numeric(18,3)'',''IM_Menu'',''IM_ID'',''Markter_Val''),
(''InvUnit'',''InvUnit_U_IM_ID'',''Integer'',''int'',''IM_Units'',''U_IM_ID'',''U_IM_ID''),
(''InvUnit'',''InvUnit_IM_ID'',''Integer'',''int'',''IM_Units'',''U_IM_ID'',''IM_ID''),
(''InvUnit'',''InvUnit_U_ID'',''Integer'',''int'',''IM_Units'',''U_IM_ID'',''U_ID''),
(''InvUnit'',''InvUnit_Barcode'',''String'',''nvarchar(max)'',''IM_Units'',''U_IM_ID'',''Barcode''),
(''InvUnit'',''InvUnit_Price'',''Decimal'',''numeric(18,3)'',''IM_Units'',''U_IM_ID'',''Price''),
(''InvUnit'',''InvUnit_Min_SP'',''Decimal'',''numeric(18,3)'',''IM_Units'',''U_IM_ID'',''Min_SP''),
(''InvUnit'',''InvUnit_Min_SP_2'',''Decimal'',''numeric(18,3)'',''IM_Units'',''U_IM_ID'',''Min_SP_2''),
(''InvUnit'',''InvUnit_is_Default'',''Integer'',''int'',''IM_Units'',''U_IM_ID'',''is_Default''),
(''InvUnit'',''InvUnit_Row_Enabled'',''Boolean'',''bit'',''IM_Units'',''U_IM_ID'',''Row_Enabled''),
(''InvUnit'',''InvUnit_User_ID'',''Integer'',''int'',''IM_Units'',''U_IM_ID'',''User_ID''),
(''InvUnit'',''InvUnit_OFFER_PRICE'',''Decimal'',''numeric(18,3)'',''IM_Units'',''U_IM_ID'',''OFFER_PRICE''),
(''InvPhysicalStock'',''InvPhysicalStock_T_ID'',''Integer'',''int'',''ST_Balance'',''T_ID'',''T_ID''),
(''InvPhysicalStock'',''InvPhysicalStock_ST_ID'',''Integer'',''int'',''ST_Balance'',''T_ID'',''ST_ID''),
(''InvPhysicalStock'',''InvPhysicalStock_IM_ID'',''Integer'',''int'',''ST_Balance'',''T_ID'',''IM_ID''),
(''InvPhysicalStock'',''InvPhysicalStock_D_Valid'',''String'',''nvarchar(50)'',''ST_Balance'',''T_ID'',''D_Valid''),
(''InvPhysicalStock'',''InvPhysicalStock_ST_QTY'',''Decimal'',''numeric(18,3)'',''ST_Balance'',''T_ID'',''ST_QTY''),
(''InvExpiry'',''InvExpiry_T_ID'',''Integer'',''int'',''Valid_Notes'',''T_ID'',''T_ID''),
(''InvExpiry'',''InvExpiry_Pch_T_ID'',''Integer'',''int'',''Valid_Notes'',''T_ID'',''Pch_T_ID''),
(''InvExpiry'',''InvExpiry_IM_ID'',''Integer'',''int'',''Valid_Notes'',''T_ID'',''IM_ID''),
(''InvExpiry'',''InvExpiry_VALID_DATE'',''DateTime'',''datetime'',''Valid_Notes'',''T_ID'',''VALID_DATE''),
(''InvCostBatch'',''InvCostBatch_BatchId'',''Guid'',''uniqueidentifier'',''InventoryCostRecountBatch'',''BatchId'',''BatchId''),
(''InvCostBatch'',''InvCostBatch_IM_ID'',''Integer'',''int'',''InventoryCostRecountBatch'',''BatchId'',''IM_ID''),
(''InvCostBatch'',''InvCostBatch_PurchaseT_ID'',''Integer'',''int'',''InventoryCostRecountBatch'',''BatchId'',''PurchaseT_ID''),
(''InvCostBatch'',''InvCostBatch_PurchaseDetailId'',''Integer'',''bigint'',''InventoryCostRecountBatch'',''BatchId'',''PurchaseDetailId''),
(''InvCostBatch'',''InvCostBatch_RecountDate'',''DateTime'',''datetime'',''InventoryCostRecountBatch'',''BatchId'',''RecountDate''),
(''InvCostBatch'',''InvCostBatch_FromDate'',''DateTime'',''datetime'',''InventoryCostRecountBatch'',''BatchId'',''FromDate''),
(''InvCostBatch'',''InvCostBatch_OldPurchaseCost'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountBatch'',''BatchId'',''OldPurchaseCost''),
(''InvCostBatch'',''InvCostBatch_NewPurchaseCost'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountBatch'',''BatchId'',''NewPurchaseCost''),
(''InvCostBatch'',''InvCostBatch_CostDiff'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountBatch'',''BatchId'',''CostDiff''),
(''InvCostBatch'',''InvCostBatch_OldItemCost'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountBatch'',''BatchId'',''OldItemCost''),
(''InvCostBatch'',''InvCostBatch_NewItemCost'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountBatch'',''BatchId'',''NewItemCost''),
(''InvCostBatch'',''InvCostBatch_TotalImpact'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountBatch'',''BatchId'',''TotalImpact''),
(''InvCostBatch'',''InvCostBatch_InventoryImpact'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountBatch'',''BatchId'',''InventoryImpact''),
(''InvCostBatch'',''InvCostBatch_COGSImpact'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountBatch'',''BatchId'',''COGSImpact''),
(''InvCostBatch'',''InvCostBatch_ExpenseImpact'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountBatch'',''BatchId'',''ExpenseImpact''),
(''InvCostBatch'',''InvCostBatch_Status'',''String'',''nvarchar(20)'',''InventoryCostRecountBatch'',''BatchId'',''Status''),
(''InvCostBatch'',''InvCostBatch_JournalId'',''Integer'',''int'',''InventoryCostRecountBatch'',''BatchId'',''JournalId''),
(''InvCostBatch'',''InvCostBatch_CreatedBy'',''Integer'',''int'',''InventoryCostRecountBatch'',''BatchId'',''CreatedBy''),
(''InvCostBatch'',''InvCostBatch_CreatedAt'',''DateTime'',''datetime'',''InventoryCostRecountBatch'',''BatchId'',''CreatedAt''),
(''InvCostBatch'',''InvCostBatch_PostedBy'',''Integer'',''int'',''InventoryCostRecountBatch'',''BatchId'',''PostedBy''),
(''InvCostBatch'',''InvCostBatch_PostedAt'',''DateTime'',''datetime'',''InventoryCostRecountBatch'',''BatchId'',''PostedAt''),
(''InvCostBatch'',''InvCostBatch_Notes'',''String'',''nvarchar(500)'',''InventoryCostRecountBatch'',''BatchId'',''Notes''),
(''InvCostImpact'',''InvCostImpact_ImpactId'',''Integer'',''bigint'',''InventoryCostRecountImpact'',''ImpactId'',''ImpactId''),
(''InvCostImpact'',''InvCostImpact_BatchId'',''Guid'',''uniqueidentifier'',''InventoryCostRecountImpact'',''ImpactId'',''BatchId''),
(''InvCostImpact'',''InvCostImpact_IM_ID'',''Integer'',''int'',''InventoryCostRecountImpact'',''ImpactId'',''IM_ID''),
(''InvCostImpact'',''InvCostImpact_SourceTable'',''String'',''nvarchar(100)'',''InventoryCostRecountImpact'',''ImpactId'',''SourceTable''),
(''InvCostImpact'',''InvCostImpact_SourceId'',''Integer'',''int'',''InventoryCostRecountImpact'',''ImpactId'',''SourceId''),
(''InvCostImpact'',''InvCostImpact_SourceParentId'',''Integer'',''int'',''InventoryCostRecountImpact'',''ImpactId'',''SourceParentId''),
(''InvCostImpact'',''InvCostImpact_SourceDate'',''DateTime'',''datetime'',''InventoryCostRecountImpact'',''ImpactId'',''SourceDate''),
(''InvCostImpact'',''InvCostImpact_MovementType'',''String'',''nvarchar(50)'',''InventoryCostRecountImpact'',''ImpactId'',''MovementType''),
(''InvCostImpact'',''InvCostImpact_StoreId'',''Integer'',''int'',''InventoryCostRecountImpact'',''ImpactId'',''StoreId''),
(''InvCostImpact'',''InvCostImpact_Qty'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountImpact'',''ImpactId'',''Qty''),
(''InvCostImpact'',''InvCostImpact_U_Cargo'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountImpact'',''ImpactId'',''U_Cargo''),
(''InvCostImpact'',''InvCostImpact_OldCost'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountImpact'',''ImpactId'',''OldCost''),
(''InvCostImpact'',''InvCostImpact_NewCost'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountImpact'',''ImpactId'',''NewCost''),
(''InvCostImpact'',''InvCostImpact_OldTotalCost'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountImpact'',''ImpactId'',''OldTotalCost''),
(''InvCostImpact'',''InvCostImpact_NewTotalCost'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountImpact'',''ImpactId'',''NewTotalCost''),
(''InvCostImpact'',''InvCostImpact_DiffAmount'',''Decimal'',''decimal(19,6)'',''InventoryCostRecountImpact'',''ImpactId'',''DiffAmount''),
(''InvCostImpact'',''InvCostImpact_PurchaseT_ID'',''Integer'',''int'',''InventoryCostRecountImpact'',''ImpactId'',''PurchaseT_ID''),
(''InvCostImpact'',''InvCostImpact_PurchaseDate'',''DateTime'',''datetime'',''InventoryCostRecountImpact'',''ImpactId'',''PurchaseDate''),
(''InvCostImpact'',''InvCostImpact_CreatedAt'',''DateTime'',''datetime'',''InventoryCostRecountImpact'',''ImpactId'',''CreatedAt''),
(''InvCostImpact'',''InvCostImpact_CreatedBy'',''Integer'',''int'',''InventoryCostRecountImpact'',''ImpactId'',''CreatedBy''),
(''InvCostImpact'',''InvCostImpact_IsPosted'',''Boolean'',''bit'',''InventoryCostRecountImpact'',''ImpactId'',''IsPosted''),
(''InvCostImpact'',''InvCostImpact_JournalId'',''Integer'',''int'',''InventoryCostRecountImpact'',''ImpactId'',''JournalId''),
(''InvCostImpact'',''InvCostImpact_OldAvgCost'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountImpact'',''ImpactId'',''OldAvgCost''),
(''InvCostImpact'',''InvCostImpact_NewAvgCost'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountImpact'',''ImpactId'',''NewAvgCost''),
(''InvCostImpact'',''InvCostImpact_OldStockQty'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountImpact'',''ImpactId'',''OldStockQty''),
(''InvCostImpact'',''InvCostImpact_NewStockQty'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountImpact'',''ImpactId'',''NewStockQty''),
(''InvCostImpact'',''InvCostImpact_OldStockValue'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountImpact'',''ImpactId'',''OldStockValue''),
(''InvCostImpact'',''InvCostImpact_NewStockValue'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountImpact'',''ImpactId'',''NewStockValue''),
(''InvCostImpact'',''InvCostImpact_IsCurrentStockEffect'',''Boolean'',''bit'',''InventoryCostRecountImpact'',''ImpactId'',''IsCurrentStockEffect''),
(''InvCostImpact'',''InvCostImpact_AccountId'',''Integer'',''int'',''InventoryCostRecountImpact'',''ImpactId'',''AccountId''),
(''InvCostImpact'',''InvCostImpact_CostCenterId'',''Integer'',''int'',''InventoryCostRecountImpact'',''ImpactId'',''CostCenterId''),
(''InvCostImpact'',''InvCostImpact_Notes'',''String'',''nvarchar(500)'',''InventoryCostRecountImpact'',''ImpactId'',''Notes''),
(''InvCostImpact'',''InvCostImpact_IsReversed'',''Boolean'',''bit'',''InventoryCostRecountImpact'',''ImpactId'',''IsReversed''),
(''InvCostJournal'',''InvCostJournal_DraftId'',''Integer'',''bigint'',''InventoryCostRecountJournalDraft'',''DraftId'',''DraftId''),
(''InvCostJournal'',''InvCostJournal_BatchId'',''Guid'',''uniqueidentifier'',''InventoryCostRecountJournalDraft'',''DraftId'',''BatchId''),
(''InvCostJournal'',''InvCostJournal_IM_ID'',''Integer'',''int'',''InventoryCostRecountJournalDraft'',''DraftId'',''IM_ID''),
(''InvCostJournal'',''InvCostJournal_AccountEffect'',''String'',''nvarchar(100)'',''InventoryCostRecountJournalDraft'',''DraftId'',''AccountEffect''),
(''InvCostJournal'',''InvCostJournal_Debit'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountJournalDraft'',''DraftId'',''Debit''),
(''InvCostJournal'',''InvCostJournal_Credit'',''Decimal'',''decimal(18,6)'',''InventoryCostRecountJournalDraft'',''DraftId'',''Credit''),
(''InvCostJournal'',''InvCostJournal_SourceNote'',''String'',''nvarchar(500)'',''InventoryCostRecountJournalDraft'',''DraftId'',''SourceNote''),
(''InvCostJournal'',''InvCostJournal_CreatedAt'',''DateTime'',''datetime'',''InventoryCostRecountJournalDraft'',''DraftId'',''CreatedAt''),
(''InvCostJournal'',''InvCostJournal_CreatedBy'',''Integer'',''int'',''InventoryCostRecountJournalDraft'',''DraftId'',''CreatedBy''),
(''InvCostJournal'',''InvCostJournal_IsPosted'',''Boolean'',''bit'',''InventoryCostRecountJournalDraft'',''DraftId'',''IsPosted''),
(''InvCostJournal'',''InvCostJournal_JournalId'',''Integer'',''int'',''InventoryCostRecountJournalDraft'',''DraftId'',''JournalId''),
(''InvCostJournal'',''InvCostJournal_AccountId'',''Integer'',''int'',''InventoryCostRecountJournalDraft'',''DraftId'',''AccountId''),
(''InvCostJournal'',''InvCostJournal_CostCenterId'',''Integer'',''int'',''InventoryCostRecountJournalDraft'',''DraftId'',''CostCenterId''),
(''InvCostJournal'',''InvCostJournal_MovementType'',''String'',''nvarchar(50)'',''InventoryCostRecountJournalDraft'',''DraftId'',''MovementType''),
(''InvCostJournal'',''InvCostJournal_SourceTable'',''String'',''nvarchar(100)'',''InventoryCostRecountJournalDraft'',''DraftId'',''SourceTable''),
(''InvCostJournal'',''InvCostJournal_SourceId'',''Integer'',''int'',''InventoryCostRecountJournalDraft'',''DraftId'',''SourceId''),
(''InvCostJournal'',''InvCostJournal_LineOrder'',''Integer'',''int'',''InventoryCostRecountJournalDraft'',''DraftId'',''LineOrder''),
(''InvCostJournal'',''InvCostJournal_LineNote'',''String'',''nvarchar(500)'',''InventoryCostRecountJournalDraft'',''DraftId'',''LineNote''),
(''InvSaleCost'',''InvSaleCost_T_ID'',''Integer'',''bigint'',''SB_Contents'',''T_ID'',''T_ID''),
(''InvSaleCost'',''InvSaleCost_Bill_T_ID'',''Integer'',''int'',''SB_Contents'',''T_ID'',''Bill_T_ID''),
(''InvSaleCost'',''InvSaleCost_IM_ID'',''Integer'',''int'',''SB_Contents'',''T_ID'',''IM_ID''),
(''InvSaleCost'',''InvSaleCost_Date'',''DateTime'',''datetime'',''SB_Contents'',''T_ID'',''Date''),
(''InvSaleCost'',''InvSaleCost_Cost'',''Float'',''float'',''SB_Contents'',''T_ID'',''Cost''),
(''InvSaleCost'',''InvSaleCost_U_Cargo'',''Float'',''float'',''SB_Contents'',''T_ID'',''U_Cargo''),
(''InvSaleCost'',''InvSaleCost_Markter_Val'',''Decimal'',''numeric(18,3)'',''SB_Contents'',''T_ID'',''Markter_Val''),
(''InvReturnCost'',''InvReturnCost_T_ID'',''Integer'',''int'',''Rtn_Contents'',''T_ID'',''T_ID''),
(''InvReturnCost'',''InvReturnCost_Date'',''DateTime'',''datetime'',''Rtn_Contents'',''T_ID'',''Date''),
(''InvReturnCost'',''InvReturnCost_Bill_T_ID'',''Integer'',''int'',''Rtn_Contents'',''T_ID'',''Bill_T_ID''),
(''InvReturnCost'',''InvReturnCost_Rtn_Type_ID'',''Integer'',''int'',''Rtn_Contents'',''T_ID'',''Rtn_Type_ID''),
(''InvReturnCost'',''InvReturnCost_IM_ID'',''Integer'',''int'',''Rtn_Contents'',''T_ID'',''IM_ID''),
(''InvReturnCost'',''InvReturnCost_Cost'',''Decimal'',''numeric(18,2)'',''Rtn_Contents'',''T_ID'',''Cost''),
(''InvReturnCost'',''InvReturnCost_U_Cargo'',''Float'',''float'',''Rtn_Contents'',''T_ID'',''U_Cargo''),
(''InvIssueCost'',''InvIssueCost_T_ID'',''Integer'',''int'',''InSale_Details'',''T_ID'',''T_ID''),
(''InvIssueCost'',''InvIssueCost_Date'',''DateTime'',''datetime'',''InSale_Details'',''T_ID'',''Date''),
(''InvIssueCost'',''InvIssueCost_InSale_T_ID'',''Integer'',''int'',''InSale_Details'',''T_ID'',''InSale_T_ID''),
(''InvIssueCost'',''InvIssueCost_IM_ID'',''Integer'',''int'',''InSale_Details'',''T_ID'',''IM_ID''),
(''InvIssueCost'',''InvIssueCost_Cost'',''Decimal'',''numeric(18,2)'',''InSale_Details'',''T_ID'',''Cost''),
(''InvIssueCost'',''InvIssueCost_U_Cargo'',''Float'',''float'',''InSale_Details'',''T_ID'',''U_Cargo''),
(''InvReceiptCost'',''InvReceiptCost_T_ID'',''Integer'',''int'',''Outsale_Details'',''T_ID'',''T_ID''),
(''InvReceiptCost'',''InvReceiptCost_Date'',''DateTime'',''datetime'',''Outsale_Details'',''T_ID'',''Date''),
(''InvReceiptCost'',''InvReceiptCost_Outsale_T_ID'',''Integer'',''int'',''Outsale_Details'',''T_ID'',''Outsale_T_ID''),
(''InvReceiptCost'',''InvReceiptCost_IM_ID'',''Integer'',''int'',''Outsale_Details'',''T_ID'',''IM_ID''),
(''InvReceiptCost'',''InvReceiptCost_Cost'',''Decimal'',''numeric(18,2)'',''Outsale_Details'',''T_ID'',''Cost''),
(''InvReceiptCost'',''InvReceiptCost_U_Cargo'',''Float'',''float'',''Outsale_Details'',''T_ID'',''U_Cargo''),
(''InvTransferCost'',''InvTransferCost_T_ID'',''Integer'',''int'',''Store_Trans_Details'',''T_ID'',''T_ID''),
(''InvTransferCost'',''InvTransferCost_B_T_ID'',''Integer'',''int'',''Store_Trans_Details'',''T_ID'',''B_T_ID''),
(''InvTransferCost'',''InvTransferCost_Date'',''DateTime'',''datetime'',''Store_Trans_Details'',''T_ID'',''Date''),
(''InvTransferCost'',''InvTransferCost_IM_ID'',''Integer'',''int'',''Store_Trans_Details'',''T_ID'',''IM_ID''),
(''InvTransferCost'',''InvTransferCost_Price'',''Decimal'',''numeric(18,2)'',''Store_Trans_Details'',''T_ID'',''Price''),
(''InvTransferCost'',''InvTransferCost_U_Cargo'',''Float'',''float'',''Store_Trans_Details'',''T_ID'',''U_Cargo''),
(''InvStore'',''InvStore_ST_ID'',''Integer'',''bigint'',''Stores'',''ST_ID'',''ST_ID''),
(''InvStore'',''InvStore_St_Name'',''String'',''nvarchar(350)'',''Stores'',''ST_ID'',''St_Name''),
(''InvStockBucket'',''InvStockBucket_IM_ID'',''Integer'',''int'','''','''',''IM_ID''),
(''InvStockBucket'',''InvStockBucket_ST_ID'',''Integer'',''bigint'','''','''',''ST_ID''),
(''InvStockBucket'',''InvStockBucket_D_Vaild'',''String'',''nvarchar(50)'','''','''',''D_Vaild''),
(''InvStockBucket'',''InvStockBucket_Quantity'',''Float'',''float'','''','''',''Quantity'');
  DECLARE @iAllowed TABLE(O varchar(64),E varchar(64));
  INSERT @iAllowed VALUES (''Inventory.PurchaseLineDeleted'',''InvDocument''),(''Inventory.PurchaseLineDeleted'',''InvPurchase''),(''Inventory.PurchaseLineDeleted'',''InvExpiry''),(''Inventory.PurchaseLineDeleted'',''InvItem''),(''Inventory.PurchaseLineDeleted'',''InvUnit''),(''Inventory.PurchaseLineDeleted'',''InvCostBatch''),(''Inventory.PurchaseLineDeleted'',''InvCostImpact''),(''Inventory.PurchaseLineDeleted'',''InvCostJournal''),(''Inventory.PurchaseLineDeleted'',''InvStockBucket''),(''Inventory.PurchaseLineChanged'',''InvDocument''),(''Inventory.PurchaseLineChanged'',''InvPurchase''),(''Inventory.PurchaseLineChanged'',''InvExpiry''),(''Inventory.PurchaseLineChanged'',''InvItem''),(''Inventory.PurchaseLineChanged'',''InvUnit''),(''Inventory.PurchaseLineChanged'',''InvCostBatch''),(''Inventory.PurchaseLineChanged'',''InvCostImpact''),(''Inventory.PurchaseLineChanged'',''InvCostJournal''),(''Inventory.PurchaseLineChanged'',''InvStockBucket''),(''Manufacturing.AutoProductAdded'',''InvDocument''),(''Manufacturing.AutoProductAdded'',''InvProduct''),(''Manufacturing.AutoProductAdded'',''InvIngredient''),(''Manufacturing.AutoProductAdded'',''InvStockBucket''),(''Manufacturing.ManualLineAdded'',''InvDocument''),(''Manufacturing.ManualLineAdded'',''InvProduct''),(''Manufacturing.ManualLineAdded'',''InvIngredient''),(''Manufacturing.ManualLineAdded'',''InvStockBucket''),(''Manufacturing.ProductDeleted'',''InvDocument''),(''Manufacturing.ProductDeleted'',''InvProduct''),(''Manufacturing.ProductDeleted'',''InvIngredient''),(''Manufacturing.ProductDeleted'',''InvStockBucket''),(''Manufacturing.ProductChanged'',''InvDocument''),(''Manufacturing.ProductChanged'',''InvProduct''),(''Manufacturing.ProductChanged'',''InvIngredient''),(''Manufacturing.ProductChanged'',''InvStockBucket''),(''Manufacturing.IngredientDeleted'',''InvDocument''),(''Manufacturing.IngredientDeleted'',''InvProduct''),(''Manufacturing.IngredientDeleted'',''InvIngredient''),(''Manufacturing.IngredientDeleted'',''InvStockBucket''),(''Manufacturing.CostRecalculated'',''InvDocument''),(''Manufacturing.CostRecalculated'',''InvProduct''),(''Manufacturing.CostRecalculated'',''InvIngredient''),(''Manufacturing.CostRecalculated'',''InvStockBucket''),(''Manufacturing.PurchaseCopied'',''InvDocument''),(''Manufacturing.PurchaseCopied'',''InvProduct''),(''Manufacturing.PurchaseCopied'',''InvIngredient''),(''Manufacturing.PurchaseCopied'',''InvStockBucket''),(''Manufacturing.SalesCopied'',''InvDocument''),(''Manufacturing.SalesCopied'',''InvProduct''),(''Manufacturing.SalesCopied'',''InvIngredient''),(''Manufacturing.SalesCopied'',''InvStockBucket''),(''Inventory.QuantityRecounted'',''InvStore''),(''Inventory.QuantityRecounted'',''InvItem''),(''Inventory.QuantityRecounted'',''InvUnit''),(''Inventory.QuantityRecounted'',''InvPhysicalStock''),(''Inventory.QuantityRecounted'',''InvStockBucket''),(''Inventory.ItemCostRecounted'',''InvItem''),(''Inventory.ItemCostRecounted'',''InvSaleCost''),(''Inventory.ItemCostRecounted'',''InvReturnCost''),(''Inventory.ItemCostRecounted'',''InvIssueCost''),(''Inventory.ItemCostRecounted'',''InvReceiptCost''),(''Inventory.ItemCostRecounted'',''InvTransferCost''),(''Inventory.AllCostsRecounted'',''InvItem''),(''Inventory.AllCostsRecounted'',''InvPurchase'');
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN @iFields f ON f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType
   WHERE f.F IS NULL OR NOT EXISTS(SELECT 1 FROM @iAllowed a WHERE a.O=@operation AND a.E=c.EntityType)
   OR c.ValueOrigin<>CASE WHEN c.EntityType=''InvStockBucket'' THEN 3 ELSE 1 END
   OR c.BeforeState NOT IN(1,2,3) OR c.AfterState NOT IN(1,2,3) OR (c.BeforeState=1 AND c.AfterState=1)) THROW 51136,''Invalid inventory field/source/state.'',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>(SELECT COUNT(*) FROM @iFields f WHERE f.E=EntityType)
   OR MIN(CASE WHEN BeforeState=1 THEN 1 ELSE 0 END)<>MAX(CASE WHEN BeforeState=1 THEN 1 ELSE 0 END)
   OR MIN(CASE WHEN AfterState=1 THEN 1 ELSE 0 END)<>MAX(CASE WHEN AfterState=1 THEN 1 ELSE 0 END)) THROW 51136,''Incomplete inventory row.'',1;
  -- The current stored row must match every recorded after-value. Identifiers only come from the closed table above.
  DECLARE @iTable sysname,@iKey sysname,@iColumn sysname,@iType varchar(40),@iId nvarchar(128),@iState tinyint,@iValue nvarchar(max),@iQuery nvarchar(max),@iFound int;
  DECLARE i036 CURSOR LOCAL FAST_FORWARD FOR
   SELECT f.TN,f.KN,f.CN,f.S,c.EntityId,c.AfterState,c.AfterValue FROM @changes c JOIN @iFields f ON f.E=c.EntityType AND f.F=c.FieldCode WHERE c.EntityType<>''InvStockBucket'';
  OPEN i036;
  FETCH NEXT FROM i036 INTO @iTable,@iKey,@iColumn,@iType,@iId,@iState,@iValue;
  WHILE @@FETCH_STATUS=0
  BEGIN
   SET @iQuery=N''SELECT @found=COUNT(*) FROM dbo.''+QUOTENAME(@iTable)+N'' WITH(HOLDLOCK) WHERE CONVERT(nvarchar(128),''+QUOTENAME(@iKey)+N'')=@id'';
   IF @iState=2 SET @iQuery+=N'' AND ''+QUOTENAME(@iColumn)+N'' IS NULL'';
   IF @iState=3
   BEGIN
    IF @iType LIKE ''%char%'' SET @iQuery+=N'' AND DATALENGTH(CONVERT(nvarchar(max),''+QUOTENAME(@iColumn)+N''))=DATALENGTH(@value) AND CONVERT(nvarchar(max),''+QUOTENAME(@iColumn)+N'') COLLATE Latin1_General_100_BIN2=@value COLLATE Latin1_General_100_BIN2'';
    ELSE IF @iType IN(''datetime'',''smalldatetime'') SET @iQuery+=N'' AND ''+QUOTENAME(@iColumn)+N''=CONVERT(datetime,TRY_CONVERT(datetime2(7),@value,126))'';
    ELSE SET @iQuery+=N'' AND ''+QUOTENAME(@iColumn)+N''=TRY_CONVERT(''+@iType+N'',@value)'';
   END;
   SET @iFound=0;
   EXEC sys.sp_executesql @iQuery,N''@id nvarchar(128),@value nvarchar(max),@found int OUTPUT'',@id=@iId,@value=@iValue,@found=@iFound OUTPUT;
   IF (@iState=1 AND @iFound<>0) OR (@iState<>1 AND @iFound<>1) THROW 51136,''Inventory after-state does not match stored data.'',1;
   FETCH NEXT FROM i036 INTO @iTable,@iKey,@iColumn,@iType,@iId,@iState,@iValue;
  END;
  CLOSE i036; DEALLOCATE i036;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Inventory.Operations"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Inventory.Operations"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
