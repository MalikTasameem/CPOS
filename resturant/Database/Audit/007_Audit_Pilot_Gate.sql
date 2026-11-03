/* Limited pilot: Security + Inventory.Settlement, Sales.EditOpening after 009,
   Documents.EditOpening after 010, Sales.LineEditing after 011, Sales.LineQuantity after 012, Sales.LineLifecycle after 014, Documents.Cancellation after 018, Catalog.Units after 019, Catalog.Groups after 020, Catalog.ItemUnits after 021, Catalog.PricingRules after 022, Catalog.Items after 023, Sales.Tables after 024, Finance.Transfers after 025, Inventory.DocumentLines after 026, People.EmployeeValues after 027, Sales.ReturnLines after 028, Expenses.Lines after 029, Finance.AccountEntries after 030, Inventory.PurchaseAddition after 031, Documents.DirectFields after revised 032, Sales.QuoteLines after 034, People.Cards after 035, Inventory.Operations after 036, Documents.Workflows after 037, Catalog.ItemLifecycle after 038, People.Salaries after 039, Sales.TableSplit after 040, Finance.Receipts after 041. SQL Server 2014.
   Configuration only: no business/event deletion and no schema changes.
   Default NULL performs no change. Build the full client and close all sessions first.
   This is database-wide, NOT restricted to the operator's machine. Old clients must stay closed.
   Disable removes only these pilot domains and keeps all history and other domains.
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;
DECLARE @Enable bit = NULL;
DECLARE @ExpectedSource uniqueidentifier = NULL;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51102,'Approved test database only.',1;
IF @Enable IS NULL OR @ExpectedSource IS NULL THROW 51101,'Explicit action and expected source are required; nothing changed.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1
 THROW 51102,'Cannot verify other sessions; VIEW SERVER STATE is required.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int;
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51103,'Audit configuration is busy.',1;
 IF (SELECT COUNT(*) FROM sys.tables WHERE schema_id=SCHEMA_ID('dbo') AND name IN
 ('Network_Audit_Source','Network_Audit_Request','Network_Audit_Event','Network_Audit_Change'))<>4
  THROW 51102,'Audit tables incomplete.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK)
  WHERE ConfigId=1 AND SourceId=@ExpectedSource AND SchemaVersion=1 AND EnabledDomainsXml.exist('/Domains')=1)
  THROW 51102,'Unexpected audit source or version.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID())
  THROW 51102,'Close all other sessions to the test database before changing the pilot.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1)
  THROW 51102,'An incomplete audit request requires review before changing the pilot.',1;
 IF @Enable=1
 BEGIN
  DECLARE @lineCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Sales.LineDetailsChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Sales.LinePriceChanged%' THEN 1 ELSE 0 END),0)
   FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @lineCatalogue NOT IN(0,4) THROW 51102,'Sales line catalogue is partially installed.',1;
  DECLARE @quantityCatalogue int=(SELECT COUNT(*) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')) AND definition LIKE '%Sales.LineQuantityStepped%');
  IF @quantityCatalogue NOT IN(0,2) OR (@quantityCatalogue=2 AND @lineCatalogue<>4) THROW 51102,'Quantity catalogue is partially installed or missing prerequisites.',1;
  DECLARE @deleteCatalogue int=(SELECT COUNT(*) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')) AND definition LIKE '%Sales.LineDeleted%');
  IF @deleteCatalogue NOT IN(0,2) OR (@deleteCatalogue=2 AND @quantityCatalogue<>2) THROW 51102,'Deletion catalogue is partially installed or missing prerequisites.',1;
  DECLARE @voidCatalogue int=(SELECT COUNT(*) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')) AND definition LIKE '%Document.Voided%');
  IF @voidCatalogue NOT IN(0,2) OR (@voidCatalogue=2 AND @deleteCatalogue<>2) THROW 51102,'Cancellation catalogue is partially installed or missing prerequisites.',1;
  DECLARE @unitCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Unit.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Unit.Updated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Unit.Deleted%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @unitCatalogue NOT IN(0,6) OR (@unitCatalogue=6 AND @voidCatalogue<>2) THROW 51102,'Unit catalogue is partially installed or missing prerequisites.',1;
  DECLARE @groupCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Group.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Group.Updated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Group.Deleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Group.PrinterLinked%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Group.PrinterUnlinked%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @groupCatalogue NOT IN(0,10) OR (@groupCatalogue=10 AND @unitCatalogue<>6) THROW 51102,'Group catalogue is partially installed or missing prerequisites.',1;
  DECLARE @itemUnitCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%ItemUnit.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%ItemUnit.Updated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%ItemUnit.Deleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%ItemUnit.PriceChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%ItemUnit.OfferPriceChanged%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @itemUnitCatalogue NOT IN(0,10) OR (@itemUnitCatalogue=10 AND @groupCatalogue<>10) THROW 51102,'Item-unit catalogue is partially installed or missing prerequisites.',1;
  DECLARE @pricingCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Offer.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Offer.ItemOffersDeleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%PriceRule.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%PriceRule.Deleted%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @pricingCatalogue NOT IN(0,8) OR (@pricingCatalogue=8 AND @itemUnitCatalogue<>10) THROW 51102,'Pricing catalogue is partially installed or missing prerequisites.',1;
  DECLARE @itemMasterCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Item.FastUpdated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Item.Deleted%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @itemMasterCatalogue NOT IN(0,4) OR (@itemMasterCatalogue=4 AND @pricingCatalogue<>8) THROW 51102,'Item catalogue is partially installed or missing prerequisites.',1;
  DECLARE @tablesCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Table.Transferred%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Table.LineQuantityChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Table.LineDeleted%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @tablesCatalogue NOT IN(0,6) OR (@tablesCatalogue=6 AND @itemMasterCatalogue<>4) THROW 51102,'Table catalogue is partially installed or missing prerequisites.',1;
  DECLARE @financeCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%TreasuryMovement.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%TreasuryMovement.Voided%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%TreasuryTransfer.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%TreasuryTransfer.Voided%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%AccountTransfer.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%AccountTransfer.Voided%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @financeCatalogue NOT IN(0,12) OR (@financeCatalogue=12 AND @tablesCatalogue<>6) THROW 51102,'Finance catalogue is partially installed or missing prerequisites.',1;
  DECLARE @stockLinesCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%StockDocument.LineAdded%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%StockDocument.LineDeleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%StockDocument.LineQuantityChanged%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @stockLinesCatalogue NOT IN(0,6) OR (@stockLinesCatalogue=6 AND @financeCatalogue<>12) THROW 51102,'Stock line catalogue is partially installed or missing prerequisites.',1;
  DECLARE @employeeValuesCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%EmployeeValue.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%EmployeeValue.Updated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%EmployeeValue.Voided%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @employeeValuesCatalogue NOT IN(0,6) OR (@employeeValuesCatalogue=6 AND @stockLinesCatalogue<>6) THROW 51102,'Employee value catalogue is partially installed or missing prerequisites.',1;
  DECLARE @returnLinesCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Return.LineAdded%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Return.LineDeleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Return.LineQuantityChanged%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @returnLinesCatalogue NOT IN(0,6) OR (@returnLinesCatalogue=6 AND @employeeValuesCatalogue<>6) THROW 51102,'Return lines catalogue is partially installed or missing prerequisites.',1;
  DECLARE @expenseLinesCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Expense.LineAdded%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Expense.LineDeleted%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @expenseLinesCatalogue NOT IN(0,4) OR (@expenseLinesCatalogue=4 AND @returnLinesCatalogue<>6) THROW 51102,'Expense line catalogue is partially installed or missing prerequisites.',1;
  DECLARE @accountEntryCatalogue int=(SELECT COUNT(*) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')) AND definition LIKE '%AccountEntry.Created%');
  IF @accountEntryCatalogue NOT IN(0,2) OR (@accountEntryCatalogue=2 AND @expenseLinesCatalogue<>4) THROW 51102,'Account entry catalogue is partially installed or missing prerequisites.',1;
  DECLARE @purchaseAdditionCatalogue int=(SELECT COUNT(*) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')) AND definition LIKE '%Purchase.LineAdded%');
  IF @purchaseAdditionCatalogue NOT IN(0,2) OR (@purchaseAdditionCatalogue=2 AND @accountEntryCatalogue<>2) THROW 51102,'Purchase addition catalogue is partially installed or missing prerequisites.',1;
  DECLARE @directFieldsCatalogue int=(SELECT SUM(CASE WHEN definition LIKE '%Document.AccountChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Document.DiscountChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Document.FastDiscountChanged%' THEN 1 ELSE 0 END) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF ISNULL(@directFieldsCatalogue,0) NOT IN(0,6) OR (@directFieldsCatalogue=6 AND @purchaseAdditionCatalogue<>2) THROW 51102,'Direct header catalogue is partially installed or missing prerequisites.',1;
  DECLARE @quoteCatalogue int=(SELECT SUM(CASE WHEN definition LIKE '%Quote.LineAdded%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Quote.LineDeleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Quote.LineChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Quote.LineQuantityStepped%' THEN 1 ELSE 0 END) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF ISNULL(@quoteCatalogue,0) NOT IN(0,8) OR (@quoteCatalogue=8 AND (@directFieldsCatalogue<>6 OR CHARINDEX('QuoteDelete033:',ISNULL(OBJECT_DEFINITION(OBJECT_ID('dbo.ViewBill_Contents_Delete_IM')),N''))=0)) THROW 51102,'Quote catalogue is partially installed or missing prerequisites.',1;
  DECLARE @peopleCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Agent.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Agent.Updated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Agent.Deleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%User.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%User.Updated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%User.Deleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%User.CredentialsChanged%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @peopleCatalogue NOT IN(0,14) OR (@peopleCatalogue=14 AND @quoteCatalogue<>8) THROW 51102,'Stage 035 catalogue partially installed or missing prerequisites.',1;
  DECLARE @inventoryCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Inventory.PurchaseLineDeleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Inventory.PurchaseLineChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Manufacturing.AutoProductAdded%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Manufacturing.ManualLineAdded%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Manufacturing.ProductDeleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Manufacturing.ProductChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Manufacturing.IngredientDeleted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Manufacturing.CostRecalculated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Manufacturing.PurchaseCopied%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Manufacturing.SalesCopied%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Inventory.QuantityRecounted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Inventory.ItemCostRecounted%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Inventory.AllCostsRecounted%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @inventoryCatalogue NOT IN(0,26) OR (@inventoryCatalogue=26 AND @peopleCatalogue<>14) THROW 51102,'Stage 036 catalogue partially installed or missing prerequisites.',1;
  DECLARE @workflowsCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Document.EditFieldsSaved%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Document.ProjectChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Purchase.AccountCurrencyChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Purchase.CurrencyChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Quote.Confirmed%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Quote.DiscountChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Quote.DiscountDistributed%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Document.MetadataSaved%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Quote.Reserved%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Quote.EditFieldsSaved%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @workflowsCatalogue NOT IN(0,20) OR (@workflowsCatalogue=20 AND @inventoryCatalogue<>26) THROW 51102,'Stage 037 catalogue partially installed or missing prerequisites.',1;
  DECLARE @itemLifeCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Item.PlaceholderAcquired%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Item.FullSaved%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Item.QuickSaved%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Item.DisabledRowsRemoved%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @itemLifeCatalogue NOT IN(0,8) OR (@itemLifeCatalogue=8 AND @workflowsCatalogue<>20) THROW 51102,'Stage 038 catalogue partially installed or missing prerequisites.',1;
  DECLARE @salaryCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Salary.BatchPosted%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @salaryCatalogue NOT IN(0,2) OR (@salaryCatalogue=2 AND @itemLifeCatalogue<>8) THROW 51102,'Stage 039 catalogue partially installed or missing prerequisites.',1;
  DECLARE @tableSplitCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%TableSplit.Started%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%TableSplit.UnitMoved%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%TableSplit.UnitReturned%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%TableSplit.Confirmed%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @tableSplitCatalogue NOT IN(0,8) OR (@tableSplitCatalogue=8 AND @salaryCatalogue<>2) THROW 51102,'Stage 040 catalogue partially installed or missing prerequisites.',1;
  DECLARE @receiptsCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Receipt.Created%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Receipt.EditOpened%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Receipt.Updated%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Receipt.Voided%' THEN 1 ELSE 0 END),0) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @receiptsCatalogue NOT IN(0,8) OR (@receiptsCatalogue=8 AND @tableSplitCatalogue<>8) THROW 51102,'Stage 041 catalogue partially installed or missing prerequisites.',1;
  IF @receiptsCatalogue=8 AND OBJECT_ID('dbo.Agents_BalanceMV_insert_RCT_Audited','P') IS NULL THROW 51102,'Stage 041 isolated receipt procedure is missing.',1;
  IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND
    EnabledDomainsXml.exist('/Domains/Domain[@code!="Security" and @code!="Inventory.Settlement" and @code!="Sales.EditOpening" and @code!="Documents.EditOpening" and @code!="Sales.LineEditing" and @code!="Sales.LineQuantity" and @code!="Sales.LineLifecycle" and @code!="Documents.Cancellation" and @code!="Catalog.Units" and @code!="Catalog.Groups" and @code!="Catalog.ItemUnits" and @code!="Catalog.PricingRules" and @code!="Catalog.Items" and @code!="Sales.Tables" and @code!="Finance.Transfers" and @code!="Inventory.DocumentLines" and @code!="People.EmployeeValues" and @code!="Sales.ReturnLines" and @code!="Expenses.Lines" and @code!="Finance.AccountEntries" and @code!="Inventory.PurchaseAddition" and @code!="Documents.DirectFields" and @code!="Sales.QuoteLines" and @code!="People.Cards" and @code!="Inventory.Operations" and @code!="Documents.Workflows" and @code!="Catalog.ItemLifecycle" and @code!="People.Salaries" and @code!="Sales.TableSplit" and @code!="Finance.Receipts"]')=1)
   THROW 51102,'Other domains are enabled; this pilot must remain limited.',1;
  IF (SELECT COUNT(*) FROM sys.procedures WHERE schema_id=SCHEMA_ID('dbo') AND name IN
   ('Network_Audit_Request_Begin','Network_Audit_Event_Write','Network_Audit_Request_Complete','Network_Audit_Request_Status',
    'ST_Settlement_Header_Apply','ST_Settlement_CanRemove','ST_Balance_Update','ST_QTY_Deferences_Details_Delete'))<>8
   THROW 51102,'Required writer or settlement procedures are missing.',1;
  IF NOT EXISTS(SELECT 1 FROM sys.parameters WHERE object_id=OBJECT_ID('dbo.ST_Balance_Update') AND name='@Inserted_T_ID' AND is_output=1)
   OR NOT EXISTS(SELECT 1 FROM sys.parameters WHERE object_id=OBJECT_ID('dbo.ST_QTY_Deferences_Details_Delete') AND name='@ExpectedBillId')
   OR NOT EXISTS(SELECT 1 FROM sys.parameters WHERE object_id=OBJECT_ID('dbo.ST_QTY_Deferences_Details_Delete') AND name='@AllowNegative')
   THROW 51102,'Settlement procedure signatures are not upgraded.',1;
  IF (SELECT COUNT(*) FROM sys.sql_modules WHERE object_id IN
   (OBJECT_ID('dbo.ST_Balance_TMP1_Plus_V'),OBJECT_ID('dbo.ST_Balance_TMP1_Negitave_V'),OBJECT_ID('dbo.ST_Balance_TMP_Plus_Negitave_V'),
    OBJECT_ID('dbo.IM_full_movement_tmp_V'),OBJECT_ID('dbo.VW_InventoryTransactionFlow')) AND definition LIKE '%/*Audit3C*/%')<>5
   THROW 51102,'Void settlement filters are incomplete.',1;
  IF CHARINDEX('Settlement.EditFinished',ISNULL(OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin')),''))=0
   OR CHARINDEX('Settlement.EditFinished',ISNULL(OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write')),''))=0
   THROW 51102,'Audit writer does not recognize the full header lifecycle.',1;
  UPDATE dbo.Network_Audit_Source
   SET EnabledDomainsXml.modify('insert <Domain code="Security"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Security"]')=0;
  UPDATE dbo.Network_Audit_Source
   SET EnabledDomainsXml.modify('insert <Domain code="Inventory.Settlement"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Inventory.Settlement"]')=0;
  IF CHARINDEX('Sales.EditOpened',ISNULL(OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write')),''))>0
   AND CHARINDEX('Sales.EditOpened',ISNULL(OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin')),''))>0
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.EditOpening"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.EditOpening"]')=0;
  IF CHARINDEX('Document.EditOpened',ISNULL(OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write')),''))>0
   AND CHARINDEX('Document.EditOpened',ISNULL(OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin')),''))>0
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Documents.EditOpening"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Documents.EditOpening"]')=0;
  IF @lineCatalogue=4
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.LineEditing"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.LineEditing"]')=0;
  IF @quantityCatalogue=2
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.LineQuantity"/> as last into (/Domains)[1]')
  WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.LineQuantity"]')=0;
  IF @deleteCatalogue=2
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.LineLifecycle"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.LineLifecycle"]')=0;
  IF @voidCatalogue=2
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Documents.Cancellation"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Documents.Cancellation"]')=0;
  IF @unitCatalogue=6
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Catalog.Units"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.Units"]')=0;
  IF @groupCatalogue=10
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Catalog.Groups"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.Groups"]')=0;
  IF @itemUnitCatalogue=10
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Catalog.ItemUnits"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.ItemUnits"]')=0;
  IF @pricingCatalogue=8
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Catalog.PricingRules"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.PricingRules"]')=0;
  IF @itemMasterCatalogue=4
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Catalog.Items"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.Items"]')=0;
  IF @tablesCatalogue=6
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.Tables"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.Tables"]')=0;
  IF @financeCatalogue=12
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Finance.Transfers"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Finance.Transfers"]')=0;
  IF @stockLinesCatalogue=6
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Inventory.DocumentLines"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Inventory.DocumentLines"]')=0;
  IF @employeeValuesCatalogue=6
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="People.EmployeeValues"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="People.EmployeeValues"]')=0;
  IF @returnLinesCatalogue=6
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.ReturnLines"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.ReturnLines"]')=0;
  IF @peopleCatalogue=14
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="People.Cards"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="People.Cards"]')=0;
  IF @inventoryCatalogue=26
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Inventory.Operations"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Inventory.Operations"]')=0;
  IF @workflowsCatalogue=20
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Documents.Workflows"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Documents.Workflows"]')=0;
  IF @itemLifeCatalogue=8
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Catalog.ItemLifecycle"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.ItemLifecycle"]')=0;
  IF @salaryCatalogue=2
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="People.Salaries"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="People.Salaries"]')=0;
  IF @receiptsCatalogue=8
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Finance.Receipts"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Finance.Receipts"]')=0;
  IF @tableSplitCatalogue=8
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.TableSplit"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.TableSplit"]')=0;
  IF @quoteCatalogue=8
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.QuoteLines"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.QuoteLines"]')=0;
  IF @directFieldsCatalogue=6
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Documents.DirectFields"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Documents.DirectFields"]')=0;
  IF @purchaseAdditionCatalogue=2
  BEGIN
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Inventory.PurchaseAddition"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Inventory.PurchaseAddition"]')=0;
  END;
  IF @accountEntryCatalogue=2
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Finance.AccountEntries"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Finance.AccountEntries"]')=0;
  IF @expenseLinesCatalogue=4
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Expenses.Lines"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Expenses.Lines"]')=0;
 END
 ELSE
 BEGIN
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Finance.Receipts"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Sales.TableSplit"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="People.Salaries"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Catalog.ItemLifecycle"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Documents.Workflows"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Inventory.Operations"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="People.Cards"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Sales.ReturnLines"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Expenses.Lines"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Sales.QuoteLines"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Documents.DirectFields"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Inventory.PurchaseAddition"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Finance.AccountEntries"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="People.EmployeeValues"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Inventory.DocumentLines"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Finance.Transfers"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Sales.Tables"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Catalog.Items"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Catalog.PricingRules"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Catalog.ItemUnits"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Catalog.Groups"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Catalog.Units"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Documents.Cancellation"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Sales.LineLifecycle"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Sales.LineQuantity"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Sales.LineEditing"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Documents.EditOpening"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Sales.EditOpening"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source
   SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Inventory.Settlement"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source
   SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Security"]') WHERE ConfigId=1;
 END;
 COMMIT;
 SELECT DB_NAME() AS DatabaseName,SourceId,EnabledDomainsXml FROM dbo.Network_Audit_Source WHERE ConfigId=1;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
