ALTER PROCEDURE dbo.Network_Audit_Event_Write
 @SourceId uniqueidentifier,@RequestId uniqueidentifier,@EventId uniqueidentifier,@EventOrdinal int,
 @EventXml xml,@ChangesXml xml,@EventFingerprint varbinary(32),@CanonicalEvent varbinary(max)
AS
BEGIN
 SET NOCOUNT ON; SET XACT_ABORT ON;
 IF @@TRANCOUNT=0 OR XACT_STATE()<>1 THROW 51100,'Audit requires the caller writable transaction.',1;
 DECLARE @resource nvarchar(255)=N'CPOS.Audit.Request.'+CONVERT(nvarchar(36),@SourceId)+N'.'+CONVERT(nvarchar(36),@RequestId);
 IF ISNULL(APPLOCK_MODE('public',@resource,'Transaction'),'NoLock')<>'Exclusive'
    THROW 51100,'Begin the request in this transaction before writing an event.',1;
 IF @EventId IS NULL OR @EventId='00000000-0000-0000-0000-000000000000' OR @EventOrdinal NOT BETWEEN 1 AND 100
   OR DATALENGTH(@EventFingerprint)<>32 OR ISNULL(DATALENGTH(@CanonicalEvent),0) NOT BETWEEN 1 AND 1048576
   OR @EventXml IS NULL OR @ChangesXml IS NULL
   OR DATALENGTH(CONVERT(nvarchar(max),@EventXml))>524288 OR DATALENGTH(CONVERT(nvarchar(max),@ChangesXml))>524288
   OR @EventXml.value('count(/Event)','int')<>1 OR @EventXml.value('count(/*)','int')<>1
   OR @EventXml.exist('/Event[@version="1"]')<>1 OR @ChangesXml.exist('/Changes[@version="1"]')<>1
   OR @ChangesXml.value('count(/Changes/Change)','int')>100
   THROW 51101,'Invalid/oversized event envelope.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND SourceId=@SourceId AND SchemaVersion=1)
    THROW 51102,'Audit source/schema mismatch.',1;
 DECLARE @operation varchar(64),@actor tinyint,@user int,@service varchar(64),@session uniqueidentifier;
 SELECT @operation=OperationCode,@actor=ActorKind,@user=ActorUserId,@service=ActorServiceCode,@session=SessionId
 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND State=0;
 IF @operation IS NULL THROW 51105,'Request is missing or already complete.',1;
 DECLARE @eventCode varchar(64)=@EventXml.value('(/Event/@code)[1]','varchar(64)'),
  @domain varchar(64)=@EventXml.value('(/Event/@domain)[1]','varchar(64)'),
  @kind tinyint=@EventXml.value('(/Event/@kind)[1]','tinyint'),
  @outcome tinyint=@EventXml.value('(/Event/@outcome)[1]','tinyint');
 IF @eventCode<>@operation OR NOT (
   (@operation='AuditTest.Changed' AND @domain='AuditTest' AND @kind=1 AND @actor IN(1,3)) OR (@operation IN('Quote.LineAdded','Quote.LineDeleted','Quote.LineChanged','Quote.LineQuantityStepped') AND @domain='Sales.QuoteLines' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation IN('Document.AccountChanged','Document.DiscountChanged','Document.FastDiscountChanged') AND @domain='Documents.DirectFields' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation IN('Purchase.LineAdded') AND @domain='Inventory.PurchaseAddition' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation IN('AccountEntry.Created') AND @domain='Finance.AccountEntries' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation IN('Expense.LineAdded','Expense.LineDeleted') AND @domain='Expenses.Lines' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation IN('Return.LineAdded','Return.LineDeleted','Return.LineQuantityChanged') AND @domain='Sales.ReturnLines' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation IN('EmployeeValue.Created','EmployeeValue.Updated','EmployeeValue.Voided') AND @domain='People.EmployeeValues' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation IN('StockDocument.LineAdded','StockDocument.LineDeleted','StockDocument.LineQuantityChanged') AND @domain='Inventory.DocumentLines' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation IN('TreasuryMovement.Created','TreasuryMovement.Voided','TreasuryTransfer.Created','TreasuryTransfer.Voided','AccountTransfer.Created','AccountTransfer.Voided') AND @domain='Finance.Transfers' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation IN('Table.Transferred','Table.LineQuantityChanged','Table.LineDeleted') AND @domain='Sales.Tables' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation IN('Item.FastUpdated','Item.Deleted') AND @domain='Catalog.Items' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation IN('Offer.Created','Offer.ItemOffersDeleted','PriceRule.Created','PriceRule.Deleted') AND @domain='Catalog.PricingRules' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation IN('ItemUnit.Created','ItemUnit.Updated','ItemUnit.Deleted','ItemUnit.PriceChanged','ItemUnit.OfferPriceChanged') AND @domain='Catalog.ItemUnits' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation IN('Group.Created','Group.Updated','Group.Deleted','Group.PrinterLinked','Group.PrinterUnlinked') AND @domain='Catalog.Groups' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation IN('Unit.Created','Unit.Updated','Unit.Deleted') AND @domain='Catalog.Units' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation='Document.Voided' AND @domain='Documents.Cancellation' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation='Sales.LineAdded' AND @domain='Sales.LineLifecycle' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation='Sales.LineDeleted' AND @domain='Sales.LineLifecycle' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation='Sales.LineQuantityStepped' AND @domain='Sales.LineQuantity' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation IN('Sales.LineDetailsChanged','Sales.LinePriceChanged') AND @domain='Sales.LineEditing' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation='Document.EditOpened' AND @domain='Documents.EditOpening' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR (@operation='Sales.EditOpened' AND @domain='Sales.EditOpening' AND @kind=1 AND @actor=1 AND @session IS NOT NULL) OR
   (@operation IN('Settlement.LineAdded','Settlement.LineDeleted','Settlement.LineUpdated','Settlement.Created','Settlement.Approved','Settlement.Voided','Settlement.EditOpened','Settlement.HeaderSaved','Settlement.EditFinished') AND @domain='Inventory.Settlement' AND @kind=1 AND @actor=1) OR
   (@operation IN('Security.SessionStarted','Security.SessionEnded') AND @domain='Security' AND @kind=2 AND @actor=1 AND @session IS NOT NULL) OR
   (@operation='Security.LoginRejected' AND @domain='Security' AND @kind=2 AND @actor IN(1,2) AND @session IS NULL))
   THROW 51101,'Event does not match its request contract.',1;
 IF (@operation='Security.LoginRejected' AND @outcome<>2) OR (@operation<>'Security.LoginRejected' AND @outcome<>1)
   THROW 51101,'Outcome does not match the event.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code=sql:variable("@domain")]')=1)
   THROW 51102,'Audit domain is disabled.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Event WHERE EventId=@EventId OR (SourceId=@SourceId AND RequestId=@RequestId AND EventOrdinal=@EventOrdinal))
 BEGIN
   IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Event WHERE EventId=@EventId AND SourceId=@SourceId AND RequestId=@RequestId AND EventOrdinal=@EventOrdinal
     AND EventFingerprint=@EventFingerprint AND DATALENGTH(CanonicalEvent)=DATALENGTH(@CanonicalEvent) AND CanonicalEvent=@CanonicalEvent)
     THROW 51104,'Event identity/ordinal conflicts with different content.',1;
   SELECT CAST(2 AS int) WriteResult;
   RETURN;
 END;
 DECLARE @changes TABLE(ChangeNo int,EntityType varchar(64),EntityId nvarchar(128),FieldCode varchar(64),ValueType varchar(20),
 SourceSqlType varchar(40),BeforeState tinyint,BeforeValue nvarchar(max),AfterState tinyint,AfterValue nvarchar(max),ValueOrigin tinyint);
 INSERT @changes
 SELECT n.value('@no','int'),n.value('@entityType','varchar(64)'),n.value('@entityId','nvarchar(128)'),n.value('@field','varchar(64)'),
 n.value('@type','varchar(20)'),n.value('(@sqlType)[1]','varchar(40)'),n.value('(Before/@state)[1]','tinyint'),
 CASE WHEN n.value('(Before/@state)[1]','tinyint')=3 THEN n.value('(Before)[1]','nvarchar(max)') END,
 n.value('(After/@state)[1]','tinyint'),CASE WHEN n.value('(After/@state)[1]','tinyint')=3 THEN n.value('(After)[1]','nvarchar(max)') END,n.value('@origin','tinyint')
 FROM @ChangesXml.nodes('/Changes/Change') x(n);
 IF (@kind=1 AND NOT EXISTS(SELECT 1 FROM @changes)) OR (@kind=2 AND EXISTS(SELECT 1 FROM @changes))
    THROW 51101,'Change details are missing or not allowed for this operation.',1;
 IF EXISTS(SELECT 1 FROM @changes WHERE NOT (
 (@operation IN('Settlement.Created','Settlement.Approved','Settlement.Voided','Settlement.EditOpened','Settlement.HeaderSaved','Settlement.EditFinished') AND (
  (FieldCode IN('DocumentNumber','OwnerUserId','AccountId','Approved','Voided','EditOpenedBy','VoidBy','JournalId','OriginalJournalId') AND ValueType='Integer') OR
  (FieldCode IN('DocumentTotal','DocumentPure','Discount','Balance') AND ValueType='Decimal') OR
  (FieldCode IN('DocumentDate','EditOpenedAt','VoidAt') AND ValueType='DateTime') OR
  (FieldCode IN('Title','About','VoidReason') AND ValueType='String') OR
  (FieldCode IN('NeedRepost','NeedCancelReverse','SalesOpen','Paused','OrderDelivered') AND ValueType='Boolean'))) OR
    (@operation='Sales.EditOpened' AND (
  (FieldCode IN('Approved','Voided','JournalId','OriginalJournalId','EditOpenedBy') AND ValueType='Integer') OR
  (FieldCode='NeedRepost' AND ValueType='Boolean') OR
  (FieldCode='EditOpenedAt' AND ValueType='DateTime') OR
  (FieldCode IN('DocumentTotal','DocumentPure','Discount') AND ValueType='Decimal'))) OR (@operation='Document.EditOpened' AND (
  (FieldCode IN('DocumentType','DocumentNumber','Approved','Voided','JournalId','OriginalJournalId','EditOpenedBy') AND ValueType='Integer') OR
  (FieldCode='NeedRepost' AND ValueType='Boolean') OR
  (FieldCode='EditOpenedAt' AND ValueType='DateTime') OR
  (FieldCode IN('DocumentTotal','DocumentPure','Discount') AND ValueType='Decimal'))) OR (@operation IN('Sales.LineDetailsChanged','Sales.LinePriceChanged') AND (
  (EntityType='SalesLine' AND (
   (FieldCode IN('ItemId','UnitId','StoreId','LineApproved','MinimumPriceUsed','MinimumPrice2Used') AND ValueType='Integer') OR
   (FieldCode IN('LineQuantity','LinePrice','LineTotal','StoredQuantity') AND ValueType='Decimal') OR
   (FieldCode IN('LineCost','UnitCargo','LineDiscount','StockQuantity') AND ValueType='Float') OR
   (FieldCode IN('LineNotes','ExpiryText') AND ValueType='String') OR (FieldCode='LineDate' AND ValueType='DateTime'))) OR
  (EntityType='SalesInvoice' AND (
   (FieldCode IN('DocumentTotal','DocumentPure','Discount') AND ValueType='Decimal') OR
   (FieldCode IN('Approved','Voided','AccountId') AND ValueType='Integer') OR
   (FieldCode='DocumentProfit' AND ValueType='Float') OR (FieldCode='NeedRepost' AND ValueType='Boolean'))))) OR (@operation IN('Sales.LineQuantityStepped','Sales.LineDetailsChanged') AND ((EntityType='SalesLine' AND ((FieldCode IN('ItemId','UnitId','StoreId','LineApproved','MinimumPriceUsed','MinimumPrice2Used') AND ValueType='Integer') OR (FieldCode IN('LineQuantity','LinePrice','LineTotal','StoredQuantity') AND ValueType='Decimal') OR (FieldCode IN('LineCost','UnitCargo','LineDiscount','StockQuantity') AND ValueType='Float') OR (FieldCode IN('LineNotes','ExpiryText') AND ValueType='String') OR (FieldCode IN('LineDate') AND ValueType='DateTime'))) OR
 (EntityType='SalesInvoice' AND ((FieldCode IN('DocumentTotal','DocumentPure','Discount') AND ValueType='Decimal') OR (FieldCode IN('Approved','Voided','AccountId') AND ValueType='Integer') OR (FieldCode IN('DocumentProfit') AND ValueType='Float') OR (FieldCode IN('NeedRepost') AND ValueType='Boolean'))) OR
 (EntityType='SalesStructure' AND ((FieldCode IN('StructureLineId','StructureItemId','StructureUnitId','StructureStoreId','StructureApproved') AND ValueType='Integer') OR (FieldCode IN('StructureQuantity','StructureCargo','StructureStoredQuantity','StructureCost') AND ValueType='Decimal') OR (FieldCode IN('StructureDate') AND ValueType='DateTime'))) OR
 (EntityType='StockBucket' AND ((FieldCode IN('ItemId','StoreId') AND ValueType='Integer') OR (FieldCode IN('ExpiryText') AND ValueType='String') OR (FieldCode IN('StockQuantity') AND ValueType='Float'))))) OR (@operation IN('Sales.LineDeleted','Sales.LineAdded') AND ((EntityType='SalesLine' AND ((FieldCode IN('ItemId','UnitId','StoreId','LineApproved','MinimumPriceUsed','MinimumPrice2Used','LineDocumentId','ParentLineId') AND ValueType='Integer') OR (FieldCode IN('LineQuantity','LinePrice','LineTotal','StoredQuantity','SalespersonPercent','MarketerValue') AND ValueType='Decimal') OR (FieldCode IN('LineCost','UnitCargo','LineDiscount') AND ValueType='Float') OR (FieldCode IN('LineNotes','ExpiryText','LineComponentsText','LineBarcode','LineSerial') AND ValueType='String') OR (FieldCode IN('LineDate') AND ValueType='DateTime') OR (FieldCode IN('LineChecked') AND ValueType='Boolean'))) OR
 (EntityType='SalesInvoice' AND ((FieldCode IN('DocumentTotal','DocumentPure','Discount') AND ValueType='Decimal') OR (FieldCode IN('Approved','Voided','AccountId') AND ValueType='Integer') OR (FieldCode IN('DocumentProfit') AND ValueType='Float') OR (FieldCode IN('NeedRepost') AND ValueType='Boolean'))) OR
 (EntityType='SalesStructure' AND ((FieldCode IN('StructureLineId','StructureItemId','StructureUnitId','StructureStoreId','StructureApproved') AND ValueType='Integer') OR (FieldCode IN('StructureQuantity','StructureCargo','StructureStoredQuantity','StructureCost') AND ValueType='Decimal') OR (FieldCode IN('StructureDate') AND ValueType='DateTime'))) OR
 (EntityType='SalesComponent' AND ((FieldCode IN('ComponentLineId','ComponentId','ComponentQuantity') AND ValueType='Integer') OR (FieldCode IN('ComponentPrice') AND ValueType='Decimal'))) OR
 (EntityType='SalesReservation' AND ((FieldCode IN('ReservationLineId') AND ValueType='Integer') OR (FieldCode IN('ReservationDateTime') AND ValueType='DateTime'))) OR
 (EntityType='SalesTimedReservation' AND ((FieldCode IN('ReservationLineId','ReservationItemId') AND ValueType='Integer') OR (FieldCode IN('ReservationDate') AND ValueType='Date') OR (FieldCode IN('ReservationTime','ReservationNotes') AND ValueType='String'))) OR
 (EntityType='SalesCommission' AND ((FieldCode IN('CommissionAccountId') AND ValueType='Integer') OR (FieldCode IN('CommissionValue') AND ValueType='Decimal'))) OR
 (EntityType='StockBucket' AND ((FieldCode IN('ItemId','StoreId') AND ValueType='Integer') OR (FieldCode IN('ExpiryText') AND ValueType='String') OR (FieldCode IN('StockQuantity') AND ValueType='Float'))))) OR (@operation='Document.Voided') OR (@operation IN('Unit.Created','Unit.Updated','Unit.Deleted')) OR (@operation IN('Group.Created','Group.Updated','Group.Deleted','Group.PrinterLinked','Group.PrinterUnlinked')) OR (@operation IN('ItemUnit.Created','ItemUnit.Updated','ItemUnit.Deleted','ItemUnit.PriceChanged','ItemUnit.OfferPriceChanged')) OR (@operation IN('Offer.Created','Offer.ItemOffersDeleted','PriceRule.Created','PriceRule.Deleted')) OR (@operation IN('Item.FastUpdated','Item.Deleted')) OR (@operation IN('Table.Transferred','Table.LineQuantityChanged','Table.LineDeleted')) OR (@operation IN('TreasuryMovement.Created','TreasuryMovement.Voided','TreasuryTransfer.Created','TreasuryTransfer.Voided','AccountTransfer.Created','AccountTransfer.Voided')) OR (@operation IN('StockDocument.LineAdded','StockDocument.LineDeleted','StockDocument.LineQuantityChanged')) OR (@operation IN('EmployeeValue.Created','EmployeeValue.Updated','EmployeeValue.Voided')) OR (@operation IN('Return.LineAdded','Return.LineDeleted','Return.LineQuantityChanged')) OR (@operation IN('Expense.LineAdded','Expense.LineDeleted')) OR (@operation IN('AccountEntry.Created')) OR (@operation IN('Purchase.LineAdded')) OR (@operation IN('Document.AccountChanged','Document.DiscountChanged','Document.FastDiscountChanged')) OR (@operation IN('Quote.LineAdded','Quote.LineDeleted','Quote.LineChanged','Quote.LineQuantityStepped')) OR (@operation='AuditTest.Changed' AND ((FieldCode IN('Quantity','Cost') AND ValueType='Decimal') OR (FieldCode='Name' AND ValueType='String') OR (FieldCode='RawQuantity' AND ValueType='Float')))
    OR (@operation IN('Settlement.LineAdded','Settlement.LineDeleted','Settlement.LineUpdated') AND (
    (FieldCode IN('ItemId','StoreId','UnitId','LineId') AND ValueType='Integer') OR
    (FieldCode IN('QuantityDelta','Cost','Total','UnitCargo','RequestedQuantity','DocumentTotal','DocumentPure') AND ValueType='Decimal') OR
    (FieldCode IN('StoredQuantity','DisplayedQuantity','StockQuantity') AND ValueType='Float') OR
    (FieldCode IN('ExpiryDate','CreatedAt') AND ValueType='DateTime') OR
    (FieldCode IN('ExpiryText','Barcode') AND ValueType='String')))
 )) THROW 51101,'Field/type not allowed by operation contract.',1;
 IF EXISTS(SELECT 1 FROM @changes WHERE LEN(BeforeValue)>4096 OR LEN(AfterValue)>4096)
    THROW 51101,'Change value too long.',1;
 IF @operation='Settlement.LineAdded' AND EXISTS(SELECT 1 FROM @changes WHERE EntityType='SettlementLine' AND (BeforeState<>1 OR AfterState NOT IN(2,3)))
    THROW 51101,'Line addition must use absent-before and captured-after.',1;
 IF @operation='Settlement.LineDeleted' AND EXISTS(SELECT 1 FROM @changes WHERE EntityType='SettlementLine' AND (BeforeState NOT IN(2,3) OR AfterState<>1))
    THROW 51101,'Line deletion must capture before and absent-after.',1;
 IF @operation='Settlement.LineUpdated' AND EXISTS(SELECT 1 FROM @changes WHERE EntityType='SettlementLine' AND (BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3)))
   THROW 51101,'Line update requires real before and after values.',1;
 IF @operation='Sales.EditOpened' AND (EXISTS(SELECT 1 FROM @changes WHERE EntityType<>'SalesInvoice' OR ValueOrigin<>1 OR BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3)) OR (SELECT COUNT(*) FROM @changes)<>10)
   THROW 51101,'Sales edit opening requires ten actual before/after header fields.',1;
 IF @operation='Document.EditOpened'
 BEGIN
  IF (SELECT COUNT(*) FROM @changes)<>12 OR EXISTS(SELECT 1 FROM @changes WHERE ValueOrigin<>1 OR BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3))
   THROW 51101,'Document opening requires twelve actual before/after fields.',1;
  IF (SELECT COUNT(*) FROM @changes WHERE FieldCode='DocumentType' AND BeforeState=3 AND AfterState=3 AND BeforeValue=AfterValue
      AND AfterValue IN('2','7','8','9','10','11','12','13','17','18','35'))<>1
   THROW 51101,'Invalid document type.',1;
  DECLARE @docType int=(SELECT CONVERT(int,AfterValue) FROM @changes WHERE FieldCode='DocumentType');
  DECLARE @docEntity varchar(64)=CASE @docType
   WHEN 2 THEN 'GeneralExpense' WHEN 7 THEN 'PurchaseInvoice' WHEN 8 THEN 'WasteDocument' WHEN 9 THEN 'StockCount'
   WHEN 10 THEN 'SalesReturn' WHEN 11 THEN 'PurchaseReturn' WHEN 12 THEN 'StockTransfer' WHEN 13 THEN 'AutoManufacturing'
   WHEN 17 THEN 'StockIssue' WHEN 18 THEN 'ManualManufacturing' WHEN 35 THEN 'StockReceipt' END;
  DECLARE @docId nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),
   @docScreen varchar(64)=@EventXml.value('(/Event/@screen)[1]','varchar(64)');
  IF @docEntity IS NULL OR @EventXml.value('(/Event/@entityType)[1]','varchar(64)')<>@docEntity
   OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>@docEntity OR EntityId<>@docId)
   OR NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@docEntity AND EntityId=@docId)
   THROW 51101,'Document identity mismatch.',1;
  IF NOT ((@docType=2 AND @docScreen='EXP_Details') OR (@docType=7 AND @docScreen='Pch') OR (@docType=8 AND @docScreen='IM_Execute')
   OR (@docType=9 AND @docScreen='Invoice') OR (@docType IN(10,11) AND @docScreen='Returns')
   OR (@docType=12 AND @docScreen='Stores_ImmediateOrder') OR (@docType=13 AND @docScreen='Format_Items_Auto')
   OR (@docType=17 AND @docScreen='Inside_Sales') OR (@docType=18 AND @docScreen='Format_Items_Manual')
   OR (@docType=35 AND @docScreen IN('Outside_Sales','Outside_Sales_Touch')))
   THROW 51101,'Document screen mismatch.',1;
 END;
 IF @operation IN('Sales.LineDetailsChanged','Sales.LinePriceChanged') AND (@EventOrdinal=1 OR @operation='Sales.LinePriceChanged')
 BEGIN
  -- AuditDetailComponents016: first part stays the original 25 fields.
  IF @operation='Sales.LinePriceChanged' AND @EventOrdinal<>1 THROW 51101,'Price-only operation has no component parts.',1;
  DECLARE @saleId nvarchar(128)=(SELECT EntityId FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType='SalesInvoice'),
   @saleLine nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),
   @saleScreen varchar(64)=@EventXml.value('(/Event/@screen)[1]','varchar(64)');
  IF @saleId IS NULL OR @EventXml.value('(/Event/@entityType)[1]','varchar(64)')<>'SalesLine'
   OR (@operation='Sales.LineDetailsChanged' AND @saleScreen<>'Change_IM_Details')
   OR (@operation='Sales.LinePriceChanged' AND @saleScreen<>'SBChangePrice')
   THROW 51101,'Sales line request identity or screen mismatch.',1;
  IF (SELECT COUNT(*) FROM @changes)<>25 OR (SELECT COUNT(*) FROM @changes WHERE EntityType='SalesLine')<>17
   OR (SELECT COUNT(*) FROM @changes WHERE EntityType='SalesInvoice')<>8
   OR EXISTS(SELECT 1 FROM @changes WHERE ValueOrigin<>1 OR BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3)
       OR (EntityType='SalesLine' AND EntityId<>@saleLine) OR (EntityType='SalesInvoice' AND EntityId<>@saleId))
   THROW 51101,'Sales edit requires twenty-five actual fields.',1;
  IF @EventXml.value('count(/Event/Entities/Entity)','int')<>1
   OR @EventXml.exist('/Event/Entities/Entity[@role="Document" and @type="SalesInvoice" and @id=sql:variable("@saleId")]')<>1
   THROW 51101,'Sales line document reference missing.',1;
 END;
 IF @operation='Sales.LineQuantityStepped' OR (@operation='Sales.LineDetailsChanged' AND @EventOrdinal>1)
 BEGIN
  DECLARE @stepBill nvarchar(128)=(SELECT EntityId FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType='SalesInvoice'),
   @stepLine nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)');
  IF @stepBill IS NULL OR @EventXml.value('(/Event/@entityType)[1]','varchar(64)')<>'SalesLine'
   OR (@operation='Sales.LineQuantityStepped' AND @EventXml.value('(/Event/@screen)[1]','varchar(64)') NOT IN('Sales','Sales_Fast','POS'))
   OR (@operation='Sales.LineDetailsChanged' AND @EventXml.value('(/Event/@screen)[1]','varchar(64)')<>'Change_IM_Details')
   THROW 51101,'Invalid quantity/detail identity or screen.',1;
  IF @EventXml.value('count(/Event/Entities/Entity)','int')<>1 OR
   @EventXml.exist('/Event/Entities/Entity[@role="Document" and @type="SalesInvoice" and @id=sql:variable("@stepBill")]')<>1
   THROW 51101,'Quantity step invoice reference missing.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE ValueOrigin<>1 OR BeforeState=0 OR AfterState=0 OR (BeforeState=1 AND AfterState=1)
   OR (EntityType<>'SalesStructure' AND (BeforeState<2 OR AfterState<2)))
   THROW 51101,'Actual captured quantity values required.',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes)<>25 OR (SELECT COUNT(*) FROM @changes WHERE EntityType='SalesLine' AND EntityId=@stepLine)<>17
    OR (SELECT COUNT(*) FROM @changes WHERE EntityType='SalesInvoice' AND EntityId=@stepBill)<>8
    THROW 51101,'Quantity first event requires complete line and header.',1;
  END
  ELSE
  BEGIN
   IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Event WHERE SourceId=@SourceId AND RequestId=@RequestId AND EventOrdinal=1 AND EntityType='SalesLine' AND EntityId=@stepLine AND EventCode=@operation)
    THROW 51101,'Quantity detail must follow its main event.',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType NOT IN('SalesStructure','StockBucket'))
    OR EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId
     HAVING COUNT(*)<>CASE WHEN EntityType='SalesStructure' THEN 10 ELSE 4 END)
    THROW 51101,'Quantity detail rows must be complete.',1;
  END
 END;
 IF @operation='Sales.LineDeleted'
 BEGIN
  DECLARE @deletedBill nvarchar(128)=(SELECT EntityId FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType='SalesInvoice'),
   @deletedLine nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)');
  IF @deletedBill IS NULL OR @EventXml.value('(/Event/@entityType)[1]','varchar(64)')<>'SalesLine'
   OR @EventXml.value('(/Event/@screen)[1]','varchar(64)') NOT IN('Sales','Sales_Fast','POS') THROW 51101,'Invalid delete identity or screen.',1;
  IF @EventXml.value('count(/Event/Entities/Entity)','int')<>1 OR @EventXml.exist('/Event/Entities/Entity[@role="Document" and @type="SalesInvoice" and @id=sql:variable("@deletedBill")]')<>1 THROW 51101,'Missing invoice relation.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE ValueOrigin<>1 OR BeforeState<2 OR AfterState=0 OR (EntityType='SalesLine' AND AfterState<>1) OR (EntityType IN('SalesInvoice','StockBucket') AND AfterState<2)) THROW 51101,'Actual delete values required.',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes)<>32 OR (SELECT COUNT(*) FROM @changes WHERE EntityType='SalesLine' AND EntityId=@deletedLine)<>24 OR (SELECT COUNT(*) FROM @changes WHERE EntityType='SalesInvoice' AND EntityId=@deletedBill)<>8 THROW 51101,'Complete deleted line and header required.',1;
  END
  ELSE
  BEGIN
   IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Event WHERE SourceId=@SourceId AND RequestId=@RequestId AND EventOrdinal=1 AND EventCode=@operation AND EntityType='SalesLine' AND EntityId=@deletedLine) THROW 51101,'Delete detail must follow its main event.',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType NOT IN('SalesStructure','SalesComponent','SalesReservation','SalesTimedReservation','SalesCommission','StockBucket'))
    OR EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE EntityType WHEN 'SalesStructure' THEN 10 WHEN 'SalesComponent' THEN 4 WHEN 'SalesReservation' THEN 2 WHEN 'SalesTimedReservation' THEN 5 WHEN 'SalesCommission' THEN 2 WHEN 'StockBucket' THEN 4 ELSE -1 END) THROW 51101,'Delete detail rows incomplete.',1;
  END
 END;
 -- AuditAddition017: actual full rows, new or merged, plus component/stock pages.
 IF @operation='Sales.LineAdded'
 BEGIN
  DECLARE @addedBill nvarchar(128)=(SELECT EntityId FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType='SalesInvoice'),
   @addedLine nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)');
  IF @addedBill IS NULL OR @EventXml.value('(/Event/@entityType)[1]','varchar(64)')<>'SalesLine'
   OR @EventXml.value('(/Event/@screen)[1]','varchar(64)') NOT IN('POS','Sales_Fast','SB_IM_card') THROW 51117,'Invalid addition identity or screen.',1;
  IF @EventXml.value('count(/Event/Entities/Entity)','int')<>1 OR @EventXml.exist('/Event/Entities/Entity[@role="Document" and @type="SalesInvoice" and @id=sql:variable("@addedBill")]')<>1 THROW 51117,'Missing invoice relation.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE ValueOrigin<>1 OR BeforeState=0 OR AfterState=0 OR (BeforeState=1 AND AfterState=1)
   OR EntityType NOT IN('SalesLine','SalesInvoice','SalesStructure','StockBucket')
   OR (EntityType<>'SalesStructure' AND AfterState<2) OR (EntityType IN('SalesInvoice','StockBucket') AND BeforeState<2)) THROW 51117,'Actual addition values required.',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes)<>32 OR (SELECT COUNT(*) FROM @changes WHERE EntityType='SalesLine' AND EntityId=@addedLine)<>24 OR (SELECT COUNT(*) FROM @changes WHERE EntityType='SalesInvoice' AND EntityId=@addedBill)<>8 THROW 51117,'Complete added line and header required.',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType='SalesLine' AND BeforeState=1) AND EXISTS(SELECT 1 FROM @changes WHERE EntityType='SalesLine' AND BeforeState<>1) THROW 51117,'Inconsistent new/merged line state.',1;
  END
  ELSE
  BEGIN
   IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Event WHERE SourceId=@SourceId AND RequestId=@RequestId AND EventOrdinal=1 AND EventCode=@operation AND EntityType='SalesLine' AND EntityId=@addedLine) THROW 51117,'Addition detail must follow its main event.',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType NOT IN('SalesStructure','StockBucket'))
    OR EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE EntityType WHEN 'SalesStructure' THEN 10 WHEN 'StockBucket' THEN 4 ELSE -1 END) THROW 51117,'Addition detail rows incomplete.',1;
  END
 END;
 -- AuditCancellation018: closed rows and types, actual values, no business correction.
 IF @operation='Document.Voided'
 BEGIN
  DECLARE @cv TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @cv SELECT e.E,f.F,f.T,f.S FROM (VALUES('BusinessDocument'),('Receipt')) e(E) CROSS JOIN (VALUES
   ('Voided','Integer','int'),('NeedRepost','Boolean','bit'),('NeedCancelReverse','Boolean','bit'),
   ('VoidAt','DateTime','datetime'),('VoidBy','Integer','int'),('VoidReason','String','nvarchar(500)'),
   ('JournalId','Integer','int'),('DocumentTotal','Decimal','numeric(18,3)'),('DocumentPure','Decimal','numeric(18,3)'),
   ('Discount','Decimal','numeric(18,3)'),('Debit','Decimal','numeric(18,3)'),('Credit','Decimal','numeric(18,3)'),
   ('Balance','Decimal','numeric(18,3)'),('AccountId','Integer','int'),('Approved','Integer','int')) f(F,T,S);
  INSERT @cv VALUES
   ('BusinessDocument','DocumentType','Integer','int'),('BusinessDocument','DocumentNumber','Integer','int'),('BusinessDocument','DocumentDate','DateTime','datetime'),
   ('BusinessDocument','SalesOpen','Boolean','bit'),('BusinessDocument','Paused','Boolean','bit'),('BusinessDocument','OrderDelivered','Boolean','bit'),('BusinessDocument','PaidState','Integer','int'),('BusinessDocument','TableId','Integer','int'),
   ('Receipt','ReceiptNumber','Integer','int'),('Receipt','ReceiptDocumentId','Integer','int'),('Receipt','PaymentId','Integer','int'),('Receipt','TreasuryId','Integer','int'),('Receipt','DocumentType','Integer','int'),
   ('TreasuryMovement','TreasuryId','Integer','int'),('TreasuryMovement','TreasuryDocumentId','Integer','int'),('TreasuryMovement','TreasuryType','Integer','int'),('TreasuryMovement','TreasuryVoided','Boolean','bit'),
   ('TreasuryMovement','DocumentPure','Decimal','numeric(18,3)'),('TreasuryMovement','Debit','Decimal','numeric(18,3)'),('TreasuryMovement','Credit','Decimal','numeric(18,3)'),('TreasuryMovement','Balance','Decimal','numeric(18,3)'),
   ('DiningTable','TableBusy','Boolean','bit'),('DiningTable','TableOrderCode','String','nvarchar(100)'),('CancellationPrintQueue','PrintQueueRows','Integer','bigint'),
   ('StockBucket','ItemId','Integer','int'),('StockBucket','StoreId','Integer','bigint'),('StockBucket','ExpiryText','String','nvarchar(50)'),('StockBucket','StockQuantity','Float','float(53)'),
   ('EXP_Details','ExpenseId','Integer','bigint'),('EXP_Details','ExpenseQuantity','Decimal','numeric(18,3)'),('EXP_Details','ExpensePrice','Decimal','numeric(18,3)'),('EXP_Details','ExpenseTotal','Decimal','numeric(18,3)'),('EXP_Details','LineApprovedBit','Boolean','bit');
  INSERT @cv SELECT e.E,f.F,f.T,f.S FROM (VALUES('SB_Contents'),('Pch_Details'),('IMEX_Details'),('Rtn_Contents'),('FRM_Details'),('FRM_Contents_Details'),('InSale_Details'),('OutSale_Details')) e(E)
   CROSS JOIN (VALUES('ItemId','Integer','int'),('ExpiryText','String','nvarchar(50)')) f(F,T,S);
  INSERT @cv SELECT e.E,f.F,f.T,f.S FROM (VALUES('SB_Contents'),('Pch_Details'),('IMEX_Details'),('Rtn_Contents'),('FRM_Details'),('FRM_Contents_Details'),('InSale_Details'),('OutSale_Details')) e(E)
   CROSS APPLY (VALUES('StoreId','Integer',CASE WHEN E='OutSale_Details' THEN 'int' ELSE 'bigint' END),
    ('StoredQuantity','Decimal',CASE WHEN E='FRM_Contents_Details' THEN 'numeric(18,3)' ELSE 'numeric(18,2)' END),
    (CASE WHEN E IN('SB_Contents','Rtn_Contents','FRM_Contents_Details') THEN 'LineApproved' ELSE 'LineApprovedBit' END,
     CASE WHEN E IN('SB_Contents','Rtn_Contents','FRM_Contents_Details') THEN 'Integer' ELSE 'Boolean' END,
     CASE WHEN E IN('SB_Contents','Rtn_Contents','FRM_Contents_Details') THEN 'int' ELSE 'bit' END)) f(F,T,S);
  INSERT @cv SELECT 'SalesStructure',f.F,f.T,f.S FROM (VALUES
   ('StructureLineId','Integer','int'),('StructureDate','DateTime','datetime'),('StructureItemId','Integer','int'),('StructureUnitId','Integer','int'),('StructureStoreId','Integer','int'),
   ('StructureQuantity','Decimal','numeric(18,3)'),('StructureCargo','Decimal','numeric(18,2)'),('StructureStoredQuantity','Decimal','numeric(18,2)'),('StructureApproved','Integer','int'),('StructureCost','Decimal','numeric(18,3)')) f(F,T,S);
  IF EXISTS(SELECT 1 FROM @changes c WHERE NOT EXISTS(SELECT 1 FROM @cv v WHERE v.E=c.EntityType AND v.F=c.FieldCode AND v.T=c.ValueType AND v.S=c.SourceSqlType)
   OR ValueOrigin<>1 OR BeforeState NOT IN(2,3) OR AfterState NOT IN(1,2,3) OR (EntityType<>'SalesStructure' AND AfterState=1)) THROW 51118,'Invalid cancellation field or state.',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>(SELECT COUNT(*) FROM @cv v WHERE v.E=EntityType)) THROW 51118,'Incomplete cancellation row.',1;
  DECLARE @voidId nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),@voidType int,
   @voidScreen varchar(64)=@EventXml.value('(/Event/@screen)[1]','varchar(64)');
  IF @EventXml.value('(/Event/@entityType)[1]','varchar(64)')<>'BusinessDocument' OR @EventXml.value('count(/Event/Entities/Entity)','int')<>0
   OR NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType='BusinessDocument' AND EntityId=@voidId) THROW 51118,'Invalid cancellation identity.',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes)<>23 OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>'BusinessDocument' OR EntityId<>@voidId) THROW 51118,'Cancellation header required first.',1;
   IF NOT EXISTS(SELECT 1 FROM @changes WHERE FieldCode='Voided' AND BeforeState=3 AND AfterState=3 AND BeforeValue='0' AND AfterValue='1') THROW 51118,'Actual cancellation transition required.',1;
   SELECT @voidType=CONVERT(int,AfterValue) FROM @changes WHERE FieldCode='DocumentType' AND BeforeState=3 AND AfterState=3 AND BeforeValue=AfterValue;
  END
  ELSE
  BEGIN
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType='BusinessDocument') THROW 51118,'Unexpected second header.',1;
   SELECT @voidType=CONVERT(int,c.AfterValue) FROM dbo.Network_Audit_Event e JOIN dbo.Network_Audit_Change c ON c.EventId=e.EventId
    WHERE e.SourceId=@SourceId AND e.RequestId=@RequestId AND e.EventOrdinal=1 AND e.EventCode=@operation AND e.EntityId=@voidId AND c.FieldCode='DocumentType';
  END;
  IF @voidType IS NULL OR NOT ((@voidType=1 AND @voidScreen IN('Sales','Sales_Fast','POS','OrdersMenu','POS_Abcent_Bills','ShowBill_IM'))
   OR (@voidType IN(5,6,37) AND @voidScreen='Add_Prev_Balance') OR (@voidType=2 AND @voidScreen='EXP_Details') OR (@voidType=7 AND @voidScreen='Pch') OR (@voidType=8 AND @voidScreen='IM_Execute') OR (@voidType=9 AND @voidScreen='Invoice')
   OR (@voidType IN(10,11) AND @voidScreen='Returns') OR (@voidType=13 AND @voidScreen='Format_Items_Auto') OR (@voidType=16 AND @voidScreen='ViewBill')
   OR (@voidType=17 AND @voidScreen='Inside_Sales') OR (@voidType=18 AND @voidScreen='Format_Items_Manual') OR (@voidType=35 AND @voidScreen IN('Outside_Sales','Outside_Sales_Touch'))) THROW 51118,'Cancellation screen/type mismatch.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType NOT IN('BusinessDocument','Receipt','TreasuryMovement','StockBucket','DiningTable') AND NOT
   ((@voidType=1 AND EntityType IN('SB_Contents','SalesStructure','CancellationPrintQueue')) OR (@voidType=2 AND EntityType='EXP_Details')
    OR (@voidType IN(7,9) AND EntityType='Pch_Details') OR (@voidType=8 AND EntityType='IMEX_Details') OR (@voidType IN(10,11) AND EntityType='Rtn_Contents')
    OR (@voidType IN(13,18) AND EntityType IN('FRM_Details','FRM_Contents_Details')) OR (@voidType=17 AND EntityType='InSale_Details') OR (@voidType=35 AND EntityType='OutSale_Details'))) THROW 51118,'Cancellation row belongs to another document type.',1;
 END;
 -- AuditUnits019: complete actual master row.
 IF @operation IN('Unit.Created','Unit.Updated','Unit.Deleted')
 BEGIN
  DECLARE @unitId nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),
   @unitScreen varchar(64)=@EventXml.value('(/Event/@screen)[1]','varchar(64)');
  IF @EventOrdinal<>1 OR @EventXml.value('(/Event/@entityType)[1]','varchar(64)')<>'Unit'
   OR ISNULL(TRY_CONVERT(int,@unitId),0)<=2 OR @EventXml.value('count(/Event/Entities/Entity)','int')<>0
   OR (SELECT COUNT(*) FROM @changes)<>3 THROW 51119,'Invalid unit event identity.',1;
  IF @unitScreen NOT IN('Units','ItemsMenu','IM_ADD_New','Add_Unit') OR (@operation<>'Unit.Created' AND @unitScreen<>'Units') THROW 51119,'Invalid unit screen.',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType='Unit'
   AND EntityId=CASE WHEN @operation='Unit.Created' THEN 'New' ELSE @unitId END) THROW 51119,'Unit request mismatch.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType<>'Unit' OR EntityId<>@unitId OR ValueOrigin<>1
   OR BeforeState<>CASE WHEN @operation='Unit.Created' THEN 1 ELSE 3 END OR AfterState<>CASE WHEN @operation='Unit.Deleted' THEN 1 ELSE 3 END
   OR NOT ((FieldCode='UnitName' AND ValueType='String' AND SourceSqlType='nvarchar(250)')
    OR (FieldCode='UnitCargo' AND ValueType='Float' AND SourceSqlType='float(53)')
    OR (FieldCode='UnitDefault' AND ValueType='Boolean' AND SourceSqlType='bit'))) THROW 51119,'Invalid unit fields or states.',1;
 END;
 -- AuditGroups020: actual master and printer-link rows.
 IF @operation IN('Group.Created','Group.Updated','Group.Deleted','Group.PrinterLinked','Group.PrinterUnlinked')
 BEGIN
  DECLARE @groupId nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),
   @groupScreen varchar(64)=@EventXml.value('(/Event/@screen)[1]','varchar(64)');
  IF @EventXml.value('(/Event/@entityType)[1]','varchar(64)')<>'ProductGroup' OR ISNULL(TRY_CONVERT(int,@groupId),0)<=0
   OR @EventXml.value('count(/Event/Entities/Entity)','int')<>0 THROW 51120,'Invalid group identity.',1;
  IF @groupScreen NOT IN('GeneralMenu','ItemsMenu','IM_ADD_New') OR (@operation<>'Group.Created' AND @groupScreen<>'GeneralMenu') THROW 51120,'Invalid group screen.',1;
  IF @operation='Group.Deleted' AND @groupId='1' THROW 51120,'Protected group.',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType='ProductGroup'
   AND EntityId=CASE WHEN @operation='Group.Created' THEN 'New' ELSE @groupId END) THROW 51120,'Group request mismatch.',1;
  DECLARE @gf TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @gf VALUES
   ('ProductGroup','GroupName','String','nvarchar(200)'),('ProductGroup','PrinterId','Integer','bigint'),
   ('ProductGroup','BackRed','Integer','int'),('ProductGroup','BackGreen','Integer','int'),('ProductGroup','BackBlue','Integer','int'),
   ('ProductGroup','ForeRed','Integer','int'),('ProductGroup','ForeGreen','Integer','int'),('ProductGroup','ForeBlue','Integer','int'),
   ('ProductGroup','ShowInPos','Integer','int'),('ProductGroup','GroupRank','Float','float(53)'),
   ('ProductGroup','GroupStoreId','Integer','int'),('ProductGroup','KitchenScreen','String','nvarchar(200)'),('ProductGroup','CategoryId','Integer','int'),
   ('GroupPrinter','GroupId','Integer','int'),('GroupPrinter','LinkedPrinterId','Integer','int');
  IF EXISTS(SELECT 1 FROM @changes c WHERE ValueOrigin<>1 OR NOT EXISTS(SELECT 1 FROM @gf f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51120,'Invalid group field.',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE WHEN EntityType='ProductGroup' THEN 13 ELSE 2 END) THROW 51120,'Incomplete group row.',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes)<>13 OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>'ProductGroup' OR EntityId<>@groupId
   OR (@operation='Group.Created' AND BeforeState<>1) OR (@operation<>'Group.Created' AND BeforeState NOT IN(2,3))
   OR (@operation='Group.Deleted' AND AfterState<>1) OR (@operation<>'Group.Deleted' AND AfterState NOT IN(2,3))) THROW 51120,'Invalid group header states.',1;
  END
  ELSE
  BEGIN
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType<>'GroupPrinter' OR BeforeState NOT IN(1,3) OR AfterState NOT IN(1,3) OR (BeforeState=1 AND AfterState=1)
    OR (FieldCode='GroupId' AND ((BeforeState=3 AND BeforeValue<>@groupId) OR (AfterState=3 AND AfterValue<>@groupId)))) THROW 51120,'Invalid printer-link row.',1;
  END;
 END;
 -- AuditItemUnits021: actual item context, unit siblings and recipe rows.
 IF @operation IN('ItemUnit.Created','ItemUnit.Updated','ItemUnit.Deleted','ItemUnit.PriceChanged','ItemUnit.OfferPriceChanged')
 BEGIN
  DECLARE @iuItem nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),
   @iuTarget nvarchar(50)=@EventXml.value('(/Event/Document/text())[1]','nvarchar(50)'),
   @iuScreen varchar(64)=@EventXml.value('(/Event/@screen)[1]','varchar(64)');
  IF @EventXml.value('(/Event/@entityType)[1]','varchar(64)')<>'CatalogItem' OR ISNULL(TRY_CONVERT(int,@iuItem),0)<=0 OR ISNULL(TRY_CONVERT(int,@iuTarget),0)<=0
   OR @EventXml.value('count(/Event/Entities/Entity)','int')<>0 THROW 51121,'Invalid item-unit identity.',1;
  IF NOT ((@operation='ItemUnit.Created' AND @iuScreen IN('ItemsMenu','Add_Unit'))
   OR (@operation='ItemUnit.Deleted' AND @iuScreen='ItemsMenu') OR (@operation='ItemUnit.Updated' AND @iuScreen='Update_IM_Unit')
   OR (@operation='ItemUnit.PriceChanged' AND @iuScreen='IM_Change_PriceByVal') OR (@operation='ItemUnit.OfferPriceChanged' AND @iuScreen='OFFERS')) THROW 51121,'Invalid item-unit screen.',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType='CatalogItem' AND EntityId=@iuItem) THROW 51121,'Item-unit request mismatch.',1;
  DECLARE @iuf TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @iuf VALUES
   ('CatalogItem','ItemName','String','nvarchar(500)'),
   ('CatalogItem','ItemSalesName','String','nvarchar(500)'),
   ('ItemUnit','ItemId','Integer','int'),
   ('ItemUnit','UnitId','Integer','int'),
   ('ItemUnit','UnitBarcode','String','nvarchar(max)'),
   ('ItemUnit','UnitPrice','Decimal','numeric(18,3)'),
   ('ItemUnit','MinimumPrice','Decimal','numeric(18,3)'),
   ('ItemUnit','MinimumPrice2','Decimal','numeric(18,3)'),
   ('ItemUnit','DefaultFlag','Integer','int'),
   ('ItemUnit','UnitRowEnabled','Boolean','bit'),
   ('ItemUnit','UnitUserId','Integer','int'),
   ('ItemUnit','OfferPrice','Decimal','numeric(18,3)'),
   ('ItemUnitRecipe','RecipeItemId','Integer','int'),
   ('ItemUnitRecipe','RecipeItemUnitId','Integer','int'),
   ('ItemUnitRecipe','RecipeComponentId','Integer','int'),
   ('ItemUnitRecipe','RecipeUnitId','Integer','int'),
   ('ItemUnitRecipe','RecipeQuantity','Float','float(53)'),
   ('ItemUnitRecipe','RecipeRowEnabled','Boolean','bit'),
   ('ItemUnitRecipe','RecipeUserId','Integer','int');
  IF EXISTS(SELECT 1 FROM @changes c WHERE ValueOrigin<>1 OR NOT EXISTS(SELECT 1 FROM @iuf f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51121,'Invalid item-unit field.',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE EntityType WHEN 'CatalogItem' THEN 2 WHEN 'ItemUnit' THEN 10 ELSE 7 END
    OR MIN(CASE WHEN BeforeState=1 THEN 1 ELSE 0 END)<>MAX(CASE WHEN BeforeState=1 THEN 1 ELSE 0 END)
    OR MIN(CASE WHEN AfterState=1 THEN 1 ELSE 0 END)<>MAX(CASE WHEN AfterState=1 THEN 1 ELSE 0 END)) THROW 51121,'Incomplete or inconsistent item-unit row.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE ISNULL(TRY_CONVERT(int,EntityId),0)<=0 OR BeforeState=0 OR AfterState=0 OR (BeforeState=1 AND AfterState=1)) THROW 51121,'Uncaptured item-unit values.',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes)<>2 OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>'CatalogItem' OR EntityId<>@iuItem OR BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3)) THROW 51121,'Invalid item context.',1;
  END
  ELSE
  BEGIN
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType='CatalogItem' OR (FieldCode='ItemId' AND ((BeforeState=3 AND BeforeValue<>@iuItem) OR (AfterState=3 AND AfterValue<>@iuItem)))) THROW 51121,'Invalid item-unit details.',1;
  END;
 END;
 -- AuditPricingRules022: multiset offers (S references are request-local) and keyed percentage rules.
 IF @operation IN('Offer.Created','Offer.ItemOffersDeleted','PriceRule.Created','PriceRule.Deleted')
 BEGIN
  DECLARE @pcOffer bit=CASE WHEN @operation LIKE 'Offer.%' THEN 1 ELSE 0 END,
   @pcOwner nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),
   @pcTarget nvarchar(50)=@EventXml.value('(/Event/Document/text())[1]','nvarchar(50)'),
   @pcScreen varchar(64)=@EventXml.value('(/Event/@screen)[1]','varchar(64)');
  DECLARE @pcParent varchar(64)=CASE WHEN @pcOffer=1 THEN 'ItemOffers' ELSE 'PriceRuleGroup' END,
   @pcChild varchar(64)=CASE WHEN @pcOffer=1 THEN 'OfferSnapshotRow' ELSE 'PriceRule' END;
  IF @EventXml.value('(/Event/@entityType)[1]','varchar(64)')<>@pcParent OR ISNULL(TRY_CONVERT(int,@pcOwner),0)<=0 OR ISNULL(TRY_CONVERT(int,@pcTarget),0)<=0
   OR @EventXml.value('count(/Event/Entities/Entity)','int')<>0 OR (@pcOffer=1 AND @pcTarget<>@pcOwner) THROW 51122,'Invalid pricing identity.',1;
  IF @pcScreen<>CASE WHEN @pcOffer=1 THEN 'OFFERS' ELSE 'IM_Change_Price_ByPercent' END THROW 51122,'Invalid pricing screen.',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@pcParent AND EntityId=@pcOwner) THROW 51122,'Pricing request mismatch.',1;
  DECLARE @pcFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @pcFields VALUES
   ('ItemOffers','OfferItemName','String','nvarchar(500)'),
   ('ItemOffers','OfferItemSalesName','String','nvarchar(500)'),
   ('PriceRuleGroup','RuleGroupName','String','nvarchar(200)'),
   ('OfferSnapshotRow','OfferItemId','Integer','int'),
   ('OfferSnapshotRow','OfferCreatedAt','DateTime','datetime'),
   ('OfferSnapshotRow','OfferTitle','String','nvarchar(4000)'),
   ('OfferSnapshotRow','OfferStartDate','Date','date'),
   ('OfferSnapshotRow','OfferEndDate','Date','date'),
   ('OfferSnapshotRow','OfferStartTime','String','time(0)'),
   ('OfferSnapshotRow','OfferEndTime','String','time(0)'),
   ('OfferSnapshotRow','OfferUserId','Integer','int'),
   ('PriceRule','RuleCreatedAt','DateTime','datetime'),
   ('PriceRule','RuleGroupId','Integer','int'),
   ('PriceRule','RulePercent','Decimal','numeric(18,2)');
  IF EXISTS(SELECT 1 FROM @changes c WHERE ValueOrigin<>1 OR NOT EXISTS(SELECT 1 FROM @pcFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51122,'Invalid pricing field.',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE EntityType WHEN 'ItemOffers' THEN 2 WHEN 'PriceRuleGroup' THEN 1 WHEN 'OfferSnapshotRow' THEN 8 ELSE 3 END
   OR MIN(CASE WHEN BeforeState=1 THEN 1 ELSE 0 END)<>MAX(CASE WHEN BeforeState=1 THEN 1 ELSE 0 END)
   OR MIN(CASE WHEN AfterState=1 THEN 1 ELSE 0 END)<>MAX(CASE WHEN AfterState=1 THEN 1 ELSE 0 END)) THROW 51122,'Incomplete pricing row.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE BeforeState=0 OR AfterState=0 OR (BeforeState=1 AND AfterState=1)) THROW 51122,'Uncaptured pricing values.',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes)<>CASE WHEN @pcOffer=1 THEN 2 ELSE 1 END OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>@pcParent OR EntityId<>@pcOwner OR BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3)) THROW 51122,'Invalid pricing context.',1;
  END
  ELSE
  BEGIN
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType<>@pcChild OR (@pcOffer=0 AND ISNULL(TRY_CONVERT(int,EntityId),0)<=0)
    OR (@pcOffer=1 AND (LEFT(EntityId,1)<>'S' OR SUBSTRING(EntityId,2,127) LIKE '%[^0-9]%' OR ISNULL(TRY_CONVERT(int,SUBSTRING(EntityId,2,127)),0)<=0))
    OR (FieldCode IN('OfferItemId','RuleGroupId') AND ((BeforeState=3 AND BeforeValue<>@pcOwner) OR (AfterState=3 AND AfterValue<>@pcOwner)))
    OR (@operation IN('Offer.Created','PriceRule.Created') AND AfterState NOT IN(2,3))
    OR (@operation IN('Offer.ItemOffersDeleted','PriceRule.Deleted') AND BeforeState NOT IN(2,3))
    OR (@operation='Offer.ItemOffersDeleted' AND AfterState<>1)
    OR (@operation='PriceRule.Created' AND (BeforeState<>1 OR EntityId<>@pcTarget))
    OR (@operation='PriceRule.Deleted' AND EntityId=@pcTarget AND AfterState<>1)
    OR (@pcOffer=0 AND (BeforeState=2 OR AfterState=2))) THROW 51122,'Invalid pricing detail states.',1;
  END;
 END;
 -- AuditItemMaster023: source values plus explicitly derived image hash/length, never image bytes.
 IF @operation IN('Item.FastUpdated','Item.Deleted')
 BEGIN
  DECLARE @imOwner nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),
   @imScreen varchar(64)=@EventXml.value('(/Event/@screen)[1]','varchar(64)');
  IF @EventXml.value('(/Event/@entityType)[1]','varchar(64)')<>'CatalogItemRecord' OR ISNULL(TRY_CONVERT(int,@imOwner),0)<=0
   OR @EventXml.value('(/Event/Document/text())[1]','nvarchar(50)')<>@imOwner OR @EventXml.value('count(/Event/Entities/Entity)','int')<>0 THROW 51123,'Invalid item identity.',1;
  IF @imScreen<>'IM_Fast_Mang' AND NOT(@operation='Item.Deleted' AND @imScreen='ItemsMenu') THROW 51123,'Invalid item screen.',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType='CatalogItemRecord' AND EntityId=@imOwner) THROW 51123,'Item request mismatch.',1;
  DECLARE @imFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40),O tinyint);
  INSERT @imFields VALUES
   ('CatalogItemRecord','ItemName','String','nvarchar(500)',1),
   ('CatalogItemRecord','ItemSalesName','String','nvarchar(500)',1),
   ('CatalogItemRecord','ItemGroupId','Integer','int',1),
   ('CatalogItemRecord','ItemPhotoSha256','String','image',3),
   ('CatalogItemRecord','ItemPhotoBytes','Integer','image',3),
   ('CatalogItemRecord','ItemActive','Boolean','bit',1),
   ('CatalogItemRecord','ItemStoreType','Integer','int',1),
   ('CatalogItemRecord','ItemUsesExpiry','Integer','int',1),
   ('CatalogItemRecord','ItemCost','Float','float(53)',1),
   ('CatalogItemRecord','ItemVariablePrice','Boolean','bit',1),
   ('CatalogItemRecord','ItemBKR','Integer','int',1),
   ('CatalogItemRecord','ItemBKG','Integer','int',1),
   ('CatalogItemRecord','ItemBKB','Integer','int',1),
   ('CatalogItemRecord','ItemFKR','Integer','int',1),
   ('CatalogItemRecord','ItemFKG','Integer','int',1),
   ('CatalogItemRecord','ItemFKB','Integer','int',1),
   ('CatalogItemRecord','ItemUserId','Integer','int',1),
   ('CatalogItemRecord','ItemEnabled','Boolean','bit',1),
   ('CatalogItemRecord','ItemLastPurchaseId','Integer','int',1),
   ('CatalogItemRecord','ItemLastPurchaseCargo','Decimal','numeric(18,3)',1),
   ('CatalogItemRecord','ItemShortcut','Integer','int',1),
   ('CatalogItemRecord','ItemNumber','String','nvarchar(100)',1),
   ('CatalogItemRecord','ItemCompanyGroup','Integer','int',1),
   ('CatalogItemRecord','ItemPhotoPath','String','nvarchar(max)',1),
   ('CatalogItemRecord','ItemNotes','String','nvarchar(max)',1),
   ('CatalogItemRecord','ItemMarketerValue','Decimal','numeric(18,3)',1),
   ('CatalogItemRecord','ItemDate','DateTime','datetime',1),
   ('CatalogItemRecord','ItemMaxAlert','Decimal','numeric(18,2)',1),
   ('CatalogItemRecord','ItemMinAlert','Decimal','numeric(18,2)',1),
   ('ItemAlert','AlertItemId','Integer','int',1),
   ('ItemAlert','AlertStoreId','Integer','bigint',1),
   ('ItemAlert','AlertMinimum','Decimal','numeric(18,3)',1),
   ('ItemAlert','AlertMaximum','Decimal','numeric(18,3)',1),
   ('ItemAlert','AlertEnabled','Boolean','bit',1),
   ('ItemAlert','AlertUserId','Integer','bigint',1),
   ('ItemFormat','FormatItemId','Integer','int',1),
   ('ItemFormat','FormatComponentId','Integer','int',1),
   ('ItemFormat','FormatUnitId','Integer','bigint',1),
   ('ItemFormat','FormatQuantity','Float','float(53)',1),
   ('ItemFormat','FormatEnabled','Boolean','bit',1),
   ('ItemFormat','FormatUserId','Integer','bigint',1),
   ('ItemDifference','DifferenceBillId','Integer','int',1),
   ('ItemDifference','DifferenceDate','DateTime','datetime',1),
   ('ItemDifference','DifferenceItemId','Integer','int',1),
   ('ItemDifference','DifferenceStoreId','Integer','int',1),
   ('ItemDifference','DifferenceExpiry','String','nvarchar(50)',1),
   ('ItemDifference','DifferenceQuantity','Float','float(53)',1),
   ('ItemDifference','DifferenceCost','Decimal','numeric(18,3)',1),
   ('ItemDifference','DifferenceCargo','Decimal','numeric(18,3)',1),
   ('ItemDifference','DifferenceBarcode','String','nvarchar(50)',1),
   ('ItemDifference','DifferenceTotal','Decimal','numeric(18,3)',1),
   ('ItemUnit','ItemId','Integer','int',1),
   ('ItemUnit','UnitId','Integer','int',1),
   ('ItemUnit','UnitBarcode','String','nvarchar(max)',1),
   ('ItemUnit','UnitPrice','Decimal','numeric(18,3)',1),
   ('ItemUnit','MinimumPrice','Decimal','numeric(18,3)',1),
   ('ItemUnit','MinimumPrice2','Decimal','numeric(18,3)',1),
   ('ItemUnit','DefaultFlag','Integer','int',1),
   ('ItemUnit','UnitRowEnabled','Boolean','bit',1),
   ('ItemUnit','UnitUserId','Integer','int',1),
   ('ItemUnit','OfferPrice','Decimal','numeric(18,3)',1);
  IF EXISTS(SELECT 1 FROM @changes c WHERE NOT EXISTS(SELECT 1 FROM @imFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType AND f.O=c.ValueOrigin)) THROW 51123,'Invalid item field.',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE EntityType WHEN 'CatalogItemRecord' THEN 29 WHEN 'ItemAlert' THEN 6 WHEN 'ItemFormat' THEN 6 WHEN 'ItemDifference' THEN 10 WHEN 'ItemUnit' THEN 10 ELSE -1 END
   OR MIN(CASE WHEN BeforeState=1 THEN 1 ELSE 0 END)<>MAX(CASE WHEN BeforeState=1 THEN 1 ELSE 0 END)
   OR MIN(CASE WHEN AfterState=1 THEN 1 ELSE 0 END)<>MAX(CASE WHEN AfterState=1 THEN 1 ELSE 0 END)) THROW 51123,'Incomplete item row.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE ISNULL(TRY_CONVERT(int,EntityId),0)<=0 OR BeforeState=0 OR AfterState=0 OR (BeforeState=1 AND AfterState=1)
   OR (@operation='Item.Deleted' AND (BeforeState NOT IN(2,3) OR AfterState<>1))
   OR (@operation='Item.FastUpdated' AND (AfterState NOT IN(2,3) OR (EntityType<>'ItemUnit' AND BeforeState NOT IN(2,3))))
   OR (FieldCode IN('ItemId','AlertItemId','FormatItemId','DifferenceItemId') AND ((BeforeState=3 AND BeforeValue<>@imOwner) OR (AfterState=3 AND AfterValue<>@imOwner)))) THROW 51123,'Invalid item states.',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes)<>29 OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>'CatalogItemRecord' OR EntityId<>@imOwner) THROW 51123,'Invalid item header.',1;
  END
  ELSE IF EXISTS(SELECT 1 FROM @changes WHERE EntityType='CatalogItemRecord') THROW 51123,'Repeated item header.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE FieldCode='ItemPhotoSha256' AND ((BeforeState=3 AND (LEN(BeforeValue)<>64 OR BeforeValue LIKE '%[^A-F0-9]%')) OR (AfterState=3 AND (LEN(AfterValue)<>64 OR AfterValue LIKE '%[^A-F0-9]%')))) THROW 51123,'Invalid photo digest.',1;
 END;
 -- AuditTables024: persisted before/after values; queue identities Qn are request-local.
 IF @operation IN('Table.Transferred','Table.LineQuantityChanged','Table.LineDeleted')
 BEGIN
  DECLARE @tbOwner nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),
   @tbEntity varchar(64)=@EventXml.value('(/Event/@entityType)[1]','varchar(64)'),
   @tbScreen varchar(64)=@EventXml.value('(/Event/@screen)[1]','varchar(64)'),
   @tbTransfer bit=CASE WHEN @operation='Table.Transferred' THEN 1 ELSE 0 END;
  IF ISNULL(TRY_CONVERT(int,@tbOwner),0)<=0 OR @tbEntity<>CASE WHEN @tbTransfer=1 THEN 'TableState' ELSE 'TableInvoice' END OR @tbScreen<>CASE WHEN @tbTransfer=1 THEN 'TablesBalance' ELSE 'TB_BillIM' END
   OR @EventXml.value('count(/Event/Entities/Entity)','int')<>0 THROW 51124,'Invalid table identity.',1;
  IF @tbTransfer=1 AND @EventXml.exist('/Event/Document')=1 THROW 51124,'Transfer does not have one invoice number.',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@tbEntity AND EntityId=@tbOwner) THROW 51124,'Table request mismatch.',1;
  DECLARE @tbFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @tbFields VALUES
   ('TableLine','ItemId','Integer','int'),
   ('TableLine','UnitId','Integer','bigint'),
   ('TableLine','StoreId','Integer','bigint'),
   ('TableLine','LineQuantity','Decimal','numeric(18,3)'),
   ('TableLine','LinePrice','Decimal','numeric(18,3)'),
   ('TableLine','LineTotal','Decimal','numeric(18,3)'),
   ('TableLine','LineCost','Float','float(53)'),
   ('TableLine','UnitCargo','Float','float(53)'),
   ('TableLine','StoredQuantity','Decimal','numeric(18,2)'),
   ('TableLine','LineDiscount','Float','float(53)'),
   ('TableLine','LineNotes','String','nvarchar(max)'),
   ('TableLine','ExpiryText','String','nvarchar(50)'),
   ('TableLine','LineApproved','Integer','int'),
   ('TableLine','LineDate','DateTime','datetime'),
   ('TableLine','MinimumPriceUsed','Integer','int'),
   ('TableLine','MinimumPrice2Used','Integer','int'),
   ('TableLine','LineDocumentId','Integer','int'),
   ('TableLine','LineComponentsText','String','nvarchar(max)'),
   ('TableLine','ParentLineId','Integer','int'),
   ('TableLine','SalespersonPercent','Decimal','numeric(18,3)'),
   ('TableLine','MarketerValue','Decimal','numeric(18,3)'),
   ('TableLine','LineBarcode','String','nvarchar(max)'),
   ('TableLine','LineSerial','String','nvarchar(100)'),
   ('TableLine','LineChecked','Boolean','bit'),
   ('TableInvoice','DocumentTotal','Decimal','numeric(18,3)'),
   ('TableInvoice','DocumentPure','Decimal','numeric(18,3)'),
   ('TableInvoice','Discount','Decimal','numeric(18,3)'),
   ('TableInvoice','DocumentProfit','Float','float(53)'),
   ('TableInvoice','Approved','Integer','int'),
   ('TableInvoice','Voided','Integer','int'),
   ('TableInvoice','NeedRepost','Boolean','bit'),
   ('TableInvoice','AccountId','Integer','int'),
   ('TableInvoice','TableId','Integer','int'),
   ('TableInvoice','InvoiceTableCode','String','nvarchar(100)'),
   ('TableInvoice','InvoiceNumber','Integer','int'),
   ('TableInvoice','InvoiceType','Integer','int'),
   ('TableInvoice','InvoicePaid','Integer','int'),
   ('TableInvoice','InvoiceOrdered','Integer','int'),
   ('TableInvoice','InvoiceOwner','Integer','int'),
   ('TableState','TableFloor','Integer','int'),
   ('TableState','TableName','String','nvarchar(100)'),
   ('TableState','TableActive','Boolean','bit'),
   ('TableState','TableReserved','Boolean','bit'),
   ('TableState','TableBusy','Boolean','bit'),
   ('TableState','TableOrderCode','String','nvarchar(100)'),
   ('TableState','TableCash','Integer','int'),
   ('TableState','TableOpenedAt','DateTime','datetime'),
   ('TableState','TablePrint','Boolean','bit'),
   ('TablePrintRow','QueueStoredId','Integer','int'),
   ('TablePrintRow','QueueDate','DateTime','datetime'),
   ('TablePrintRow','QueueNotes','String','nvarchar(max)'),
   ('TablePrintRow','QueuePrinter','String','nvarchar(100)');
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.ValueOrigin<>1 OR NOT EXISTS(SELECT 1 FROM @tbFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51124,'Invalid table field.',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE EntityType WHEN 'TableState' THEN 9 WHEN 'TableInvoice' THEN 15 WHEN 'TableLine' THEN 24 WHEN 'TablePrintRow' THEN 4 ELSE -1 END) THROW 51124,'Incomplete table row.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE
   (EntityType<>'TablePrintRow' AND ISNULL(TRY_CONVERT(int,EntityId),0)<=0)
   OR (EntityType='TablePrintRow' AND (LEFT(EntityId,1)<>'Q' OR ISNULL(TRY_CONVERT(int,SUBSTRING(EntityId,2,127)),0)<=0))
   OR (EntityType='TablePrintRow' AND BeforeState<>1)
   OR (EntityType<>'TablePrintRow' AND BeforeState NOT IN(2,3))
   OR (@operation='Table.LineDeleted' AND EntityType='TableLine' AND AfterState<>1)
   OR ((@operation<>'Table.LineDeleted' OR EntityType<>'TableLine') AND AfterState NOT IN(2,3))
   OR (@tbTransfer=1 AND EntityType NOT IN('TableState','TableInvoice'))
   OR (@tbTransfer=0 AND EntityType NOT IN('TableInvoice','TableLine','TablePrintRow'))
   OR (@tbTransfer=0 AND EntityType='TableInvoice' AND EntityId<>@tbOwner)
   OR (FieldCode='LineDocumentId' AND (BeforeValue<>@tbOwner OR (@operation<>'Table.LineDeleted' AND AfterValue<>@tbOwner)))) THROW 51124,'Invalid table row states.',1;
  IF @EventOrdinal=1
  BEGIN
   IF @tbTransfer=1 AND ((SELECT COUNT(*) FROM @changes)<>18 OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>'TableState') OR NOT EXISTS(SELECT 1 FROM @changes WHERE EntityId=@tbOwner)) THROW 51124,'Invalid transfer header.',1;
   IF @tbTransfer=0 AND ((SELECT COUNT(*) FROM @changes)<>39 OR (SELECT COUNT(*) FROM @changes WHERE EntityType='TableInvoice')<>15 OR (SELECT COUNT(*) FROM @changes WHERE EntityType='TableLine')<>24) THROW 51124,'Invalid table line header.',1;
  END
  ELSE IF EXISTS(SELECT 1 FROM @changes WHERE (@tbTransfer=1 AND EntityType<>'TableInvoice') OR (@tbTransfer=0 AND EntityType<>'TablePrintRow')) THROW 51124,'Invalid table detail part.',1;
 END;
 -- AuditFinance025: full persisted finance rows; no business procedure changes.
 IF @operation IN('TreasuryMovement.Created','TreasuryMovement.Voided','TreasuryTransfer.Created','TreasuryTransfer.Voided','AccountTransfer.Created','AccountTransfer.Voided')
 BEGIN
  DECLARE @finOwner nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),
   @finEntity varchar(64)=@EventXml.value('(/Event/@entityType)[1]','varchar(64)'),
   @finScreen varchar(64)=@EventXml.value('(/Event/@screen)[1]','varchar(64)'),
   @finCreate bit=CASE WHEN @operation LIKE '%.Created' THEN 1 ELSE 0 END,
   @finAccount bit=CASE WHEN @operation LIKE 'AccountTransfer.%' THEN 1 ELSE 0 END,
   @finTransfer bit=CASE WHEN @operation LIKE 'TreasuryTransfer.%' THEN 1 ELSE 0 END;
  IF ISNULL(TRY_CONVERT(int,@finOwner),0)<=0
   OR @finEntity<>CASE WHEN @finAccount=1 THEN 'FinanceAccountRow' WHEN @finTransfer=1 THEN 'FinanceTreasuryTransfer' ELSE 'FinanceTreasuryRow' END
   OR @finScreen<>CASE WHEN @finAccount=1 THEN 'AG_Transfers' WHEN @finTransfer=1 THEN 'Tr_Transfers' ELSE 'Tr_Deposit_Withdraw' END
   OR @EventXml.value('count(/Event/Entities/Entity)','int')<>0
   OR @EventXml.value('count(/Event/Document)','int')<>1
   OR @EventXml.value('(/Event/Document/text())[1]','nvarchar(50)')<>@finOwner THROW 51125,'Invalid financial identity.',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@finEntity AND EntityId=CASE WHEN @finCreate=1 THEN 'New' ELSE @finOwner END) THROW 51125,'Financial request mismatch.',1;
  DECLARE @finFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @finFields VALUES
   ('FinanceTreasuryRow','CashAGBalanceTID','Integer','int'),
   ('FinanceTreasuryRow','CashTranID','Integer','int'),
   ('FinanceTreasuryRow','Cashdate','DateTime','datetime'),
   ('FinanceTreasuryRow','CashTrID','Integer','int'),
   ('FinanceTreasuryRow','Cashnoticemove','String','nvarchar(max)'),
   ('FinanceTreasuryRow','CashBankName','String','nvarchar(100)'),
   ('FinanceTreasuryRow','CashCheckNum','String','nvarchar(100)'),
   ('FinanceTreasuryRow','CashPure','Decimal','numeric(18,3)'),
   ('FinanceTreasuryRow','CashDebit','Decimal','numeric(18,3)'),
   ('FinanceTreasuryRow','CashCredit','Decimal','numeric(18,3)'),
   ('FinanceTreasuryRow','CashBalance','Decimal','numeric(18,3)'),
   ('FinanceTreasuryRow','CashTrTypeID','Integer','int'),
   ('FinanceTreasuryRow','CashisTranFrom','Boolean','bit'),
   ('FinanceTreasuryRow','CashisTranTo','Boolean','bit'),
   ('FinanceTreasuryRow','CashUserID','Integer','int'),
   ('FinanceTreasuryRow','CashisVoid','Boolean','bit'),
   ('FinanceTreasuryRow','CashisFirstTime','Integer','int'),
   ('FinanceTreasuryRow','CashJournalId','Integer','int'),
   ('FinanceTreasuryRow','CashPostedAt','DateTime','datetime'),
   ('FinanceTreasuryRow','CashPostedBy','Integer','int'),
   ('FinanceTreasuryRow','CashOriginalJournalId','Integer','int'),
   ('FinanceTreasuryRow','CashLastReversalJournalId','Integer','int'),
   ('FinanceTreasuryRow','CashNeedRepost','Boolean','bit'),
   ('FinanceTreasuryRow','CashEditVersion','Integer','int'),
   ('FinanceTreasuryRow','CashEditOpenedAt','DateTime','datetime'),
   ('FinanceTreasuryRow','CashEditOpenedBy','Integer','int'),
   ('FinanceTreasuryRow','CashLastRepostAt','DateTime','datetime'),
   ('FinanceTreasuryRow','CashLastRepostBy','Integer','int'),
   ('FinanceTreasuryRow','CashVoidAt','DateTime','datetime'),
   ('FinanceTreasuryRow','CashVoidBy','Integer','int'),
   ('FinanceTreasuryRow','CashVoidReason','String','nvarchar(500)'),
   ('FinanceTreasuryRow','CashVoidReversalJournalId','Integer','int'),
   ('FinanceTreasuryRow','CashNeedCancelReverse','Boolean','bit'),
   ('FinanceAccountRow','AccountDATE','DateTime','datetime'),
   ('FinanceAccountRow','AccountAGIDFROM','Integer','int'),
   ('FinanceAccountRow','AccountAGIDTO','Integer','int'),
   ('FinanceAccountRow','AccountTITLE','String','nvarchar(2500)'),
   ('FinanceAccountRow','AccountABOUT','String','nvarchar(max)'),
   ('FinanceAccountRow','AccountUSERID','Integer','int'),
   ('FinanceAccountRow','AccountisVoid','Integer','int'),
   ('FinanceAccountRow','AccountVALUE','Decimal','numeric(18,3)'),
   ('FinanceAccountRow','AccountBsTypeID','Integer','int'),
   ('FinanceAccountRow','AccountJournalId','Integer','int'),
   ('FinanceAccountRow','AccountPostedAt','DateTime','datetime'),
   ('FinanceAccountRow','AccountPostedBy','Integer','int'),
   ('FinanceAccountRow','AccountOriginalJournalId','Integer','int'),
   ('FinanceAccountRow','AccountLastReversalJournalId','Integer','int'),
   ('FinanceAccountRow','AccountNeedRepost','Boolean','bit'),
   ('FinanceAccountRow','AccountEditVersion','Integer','int'),
   ('FinanceAccountRow','AccountEditOpenedAt','DateTime','datetime'),
   ('FinanceAccountRow','AccountEditOpenedBy','Integer','int'),
   ('FinanceAccountRow','AccountLastRepostAt','DateTime','datetime'),
   ('FinanceAccountRow','AccountLastRepostBy','Integer','int'),
   ('FinanceAccountRow','AccountVoidAt','DateTime','datetime'),
   ('FinanceAccountRow','AccountVoidBy','Integer','int'),
   ('FinanceAccountRow','AccountVoidReason','String','nvarchar(500)'),
   ('FinanceAccountRow','AccountVoidReversalJournalId','Integer','int'),
   ('FinanceAccountRow','AccountNeedCancelReverse','Boolean','bit');
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.ValueOrigin<>1 OR c.EntityType<>CASE WHEN @finAccount=1 THEN 'FinanceAccountRow' ELSE 'FinanceTreasuryRow' END OR NOT EXISTS(SELECT 1 FROM @finFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51125,'Invalid financial field.',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE WHEN @finAccount=1 THEN 25 ELSE 33 END) THROW 51125,'Incomplete financial row.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE ISNULL(TRY_CONVERT(int,EntityId),0)<=0 OR (@finCreate=1 AND BeforeState<>1) OR (@finCreate=0 AND BeforeState NOT IN(2,3)) OR AfterState NOT IN(2,3)
   OR ((@finAccount=1 OR @operation='TreasuryMovement.Created') AND EntityId<>@finOwner)
   OR (@finTransfer=1 AND FieldCode='CashTranID' AND (AfterState<>3 OR AfterValue<>@finOwner OR (@finCreate=0 AND (BeforeState<>3 OR BeforeValue<>@finOwner))))) THROW 51125,'Invalid financial states.',1;
  IF (@finAccount=1 OR @finCreate=1) AND (@EventOrdinal<>1 OR (SELECT COUNT(*) FROM @changes)<>CASE WHEN @finAccount=1 THEN 25 WHEN @finTransfer=1 THEN 66 ELSE 33 END) THROW 51125,'Incomplete financial event.',1;
  IF @operation='TreasuryMovement.Voided' AND EXISTS(SELECT 1 FROM @changes c WHERE NOT EXISTS(SELECT 1 FROM dbo.Treasury_Balance_MV m WITH(HOLDLOCK) WHERE m.T_ID=TRY_CONVERT(int,c.EntityId) AND (m.T_ID=TRY_CONVERT(int,@finOwner) OR m.Tran_ID=(SELECT Tran_ID FROM dbo.Treasury_Balance_MV WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@finOwner))))) THROW 51125,'Unrelated treasury row.',1;
 END;
 -- AuditStockLines026: complete affected line, recalculated header and derived stock.
 IF @operation IN('StockDocument.LineAdded','StockDocument.LineDeleted','StockDocument.LineQuantityChanged')
 BEGIN
  DECLARE @slOwner nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),
   @slEntity varchar(64)=@EventXml.value('(/Event/@entityType)[1]','varchar(64)'),
   @slScreen varchar(64)=@EventXml.value('(/Event/@screen)[1]','varchar(64)'),
   @slType int,@slLine varchar(64),@slCount int;
  SELECT @slType=BsType_ID FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@slOwner) AND isVoid=0;
  SET @slLine=CASE @slType WHEN 35 THEN 'StockReceiptLine' WHEN 17 THEN 'StockIssueLine' WHEN 8 THEN 'StockDamageLine' WHEN 12 THEN 'StockTransferLine' END;
  SET @slCount=CASE @slType WHEN 8 THEN 13 WHEN 12 THEN 15 ELSE 14 END;
  IF @slLine IS NULL OR @slEntity<>'StockDocument' OR ISNULL(TRY_CONVERT(int,@slOwner),0)<=0
   OR @EventXml.value('count(/Event/Entities/Entity)','int')<>0
   OR @EventXml.value('count(/Event/Document)','int')<>1 THROW 51126,'Invalid stock document identity.',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@slEntity AND EntityId=@slOwner) THROW 51126,'Stock request mismatch.',1;
  IF NOT (
    (@slType=35 AND ((@operation='StockDocument.LineAdded' AND @slScreen IN('Outside_Sales_IM_card','Outside_Sales_Touch')) OR (@operation='StockDocument.LineDeleted' AND @slScreen IN('Outside_Sales','Outside_Sales_Touch')) OR (@operation='StockDocument.LineQuantityChanged' AND @slScreen='Outside_Sales_Touch')))
    OR (@slType=17 AND ((@operation='StockDocument.LineAdded' AND @slScreen='Inside_Sales_IM_card') OR (@operation='StockDocument.LineDeleted' AND @slScreen='Inside_Sales')))
    OR (@slType=8 AND ((@operation='StockDocument.LineAdded' AND @slScreen='IMEX_IM_card') OR (@operation='StockDocument.LineDeleted' AND @slScreen='IM_Execute')))
    OR (@slType=12 AND ((@operation='StockDocument.LineAdded' AND @slScreen='Stores_Trans_IM_card') OR (@operation='StockDocument.LineDeleted' AND @slScreen='Stores_ImmediateOrder')))
  ) THROW 51126,'Invalid stock screen.',1;
  DECLARE @slFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @slFields VALUES
   ('StockDocument','Voided','Integer','int'),
   ('StockDocument','NeedRepost','Boolean','bit'),
   ('StockDocument','NeedCancelReverse','Boolean','bit'),
   ('StockDocument','VoidAt','DateTime','datetime'),
   ('StockDocument','VoidBy','Integer','int'),
   ('StockDocument','VoidReason','String','nvarchar(500)'),
   ('StockDocument','JournalId','Integer','int'),
   ('StockDocument','DocumentTotal','Decimal','numeric(18,3)'),
   ('StockDocument','DocumentPure','Decimal','numeric(18,3)'),
   ('StockDocument','Discount','Decimal','numeric(18,3)'),
   ('StockDocument','Debit','Decimal','numeric(18,3)'),
   ('StockDocument','Credit','Decimal','numeric(18,3)'),
   ('StockDocument','Balance','Decimal','numeric(18,3)'),
   ('StockDocument','AccountId','Integer','int'),
   ('StockDocument','Approved','Integer','int'),
   ('StockDocument','DocumentType','Integer','int'),
   ('StockDocument','DocumentNumber','Integer','int'),
   ('StockDocument','DocumentDate','DateTime','datetime'),
   ('StockDocument','SalesOpen','Boolean','bit'),
   ('StockDocument','Paused','Boolean','bit'),
   ('StockDocument','OrderDelivered','Boolean','bit'),
   ('StockDocument','PaidState','Integer','int'),
   ('StockDocument','TableId','Integer','int'),
   ('StockReceiptLine','SLDocumentId','Integer','int'),
   ('StockReceiptLine','SLDate','DateTime','datetime'),
   ('StockReceiptLine','SLItemId','Integer','int'),
   ('StockReceiptLine','SLUnitId','Integer','int'),
   ('StockReceiptLine','SLCost','Decimal','numeric(18,2)'),
   ('StockReceiptLine','SLExpiry','String','nvarchar(50)'),
   ('StockReceiptLine','SLQuantity','Decimal','numeric(18,2)'),
   ('StockReceiptLine','SLTotal','Decimal','numeric(18,2)'),
   ('StockReceiptLine','SLCargo','Float','float(53)'),
   ('StockReceiptLine','SLStoredQuantity','Decimal','numeric(18,2)'),
   ('StockReceiptLine','SLApproved','Boolean','bit'),
   ('StockReceiptLine','SLBarcode','String','nvarchar(max)'),
   ('StockReceiptLine','SLStoreId','Integer','int'),
   ('StockReceiptLine','SLSalePrice','Decimal','numeric(18,3)'),
   ('StockIssueLine','SLDocumentId','Integer','int'),
   ('StockIssueLine','SLDate','DateTime','datetime'),
   ('StockIssueLine','SLItemId','Integer','int'),
   ('StockIssueLine','SLUnitId','Integer','bigint'),
   ('StockIssueLine','SLCost','Decimal','numeric(18,2)'),
   ('StockIssueLine','SLExpiry','String','nvarchar(50)'),
   ('StockIssueLine','SLQuantity','Decimal','numeric(18,2)'),
   ('StockIssueLine','SLTotal','Decimal','numeric(18,2)'),
   ('StockIssueLine','SLCargo','Float','float(53)'),
   ('StockIssueLine','SLStoredQuantity','Decimal','numeric(18,2)'),
   ('StockIssueLine','SLApproved','Boolean','bit'),
   ('StockIssueLine','SLBarcode','String','nvarchar(max)'),
   ('StockIssueLine','SLStoreId','Integer','bigint'),
   ('StockIssueLine','SLSalePrice','Decimal','numeric(18,3)'),
   ('StockDamageLine','SLDocumentId','Integer','int'),
   ('StockDamageLine','SLDate','DateTime','datetime'),
   ('StockDamageLine','SLItemId','Integer','int'),
   ('StockDamageLine','SLUnitId','Integer','bigint'),
   ('StockDamageLine','SLCost','Decimal','numeric(18,2)'),
   ('StockDamageLine','SLExpiry','String','nvarchar(50)'),
   ('StockDamageLine','SLQuantity','Decimal','numeric(18,2)'),
   ('StockDamageLine','SLTotal','Decimal','numeric(18,2)'),
   ('StockDamageLine','SLCargo','Float','float(53)'),
   ('StockDamageLine','SLStoredQuantity','Decimal','numeric(18,2)'),
   ('StockDamageLine','SLApproved','Boolean','bit'),
   ('StockDamageLine','SLBarcode','String','nvarchar(50)'),
   ('StockDamageLine','SLStoreId','Integer','bigint'),
   ('StockTransferLine','SLDocumentId','Integer','int'),
   ('StockTransferLine','SLDate','DateTime','datetime'),
   ('StockTransferLine','SLItemId','Integer','int'),
   ('StockTransferLine','SLUnitId','Integer','int'),
   ('StockTransferLine','SLCost','Decimal','numeric(18,2)'),
   ('StockTransferLine','SLExpiry','String','nvarchar(50)'),
   ('StockTransferLine','SLQuantity','Decimal','numeric(18,2)'),
   ('StockTransferLine','SLTotal','Decimal','numeric(18,2)'),
   ('StockTransferLine','SLCargo','Float','float(53)'),
   ('StockTransferLine','SLStoredQuantity','Decimal','numeric(18,2)'),
   ('StockTransferLine','SLApprovedInt','Integer','int'),
   ('StockTransferLine','SLBarcode','String','nvarchar(50)'),
   ('StockTransferLine','SLFromStoreId','Integer','bigint'),
   ('StockTransferLine','SLToStoreId','Integer','bigint'),
   ('StockTransferLine','SLSalePrice','Decimal','numeric(18,2)'),
   ('StockBucket','ItemId','Integer','int'),
   ('StockBucket','StoreId','Integer','bigint'),
   ('StockBucket','ExpiryText','String','nvarchar(50)'),
   ('StockBucket','StockQuantity','Float','float(53)');
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.EntityType NOT IN('StockDocument','StockBucket',@slLine) OR c.ValueOrigin<>CASE WHEN c.EntityType='StockBucket' THEN 3 ELSE 1 END OR NOT EXISTS(SELECT 1 FROM @slFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51126,'Invalid stock field.',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>(SELECT COUNT(*) FROM @slFields f WHERE f.E=EntityType)) THROW 51126,'Incomplete stock row.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE (EntityType<>'StockBucket' AND ISNULL(TRY_CONVERT(int,EntityId),0)<=0) OR (EntityType='StockDocument' AND EntityId<>@slOwner) OR (EntityType='StockBucket' AND (LEN(EntityId)<>64 OR EntityId COLLATE Latin1_General_BIN2 LIKE '%[^A-F0-9]%'))) THROW 51126,'Invalid stock row identity.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE (EntityType IN('StockDocument','StockBucket') AND (BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3))) OR (EntityType=@slLine AND (
   (@operation='StockDocument.LineAdded' AND (BeforeState<>1 OR AfterState NOT IN(2,3))) OR
   (@operation='StockDocument.LineDeleted' AND (AfterState<>1 OR BeforeState NOT IN(2,3))) OR
   (@operation='StockDocument.LineQuantityChanged' AND (BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3)))))) THROW 51126,'Invalid stock value states.',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes WHERE EntityType='StockDocument')<>23 OR (SELECT COUNT(*) FROM @changes WHERE EntityType=@slLine)<>@slCount OR EXISTS(SELECT 1 FROM @changes WHERE EntityType='StockBucket') THROW 51126,'First stock event must contain header and line.',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType='StockDocument' AND FieldCode='DocumentType' AND (BeforeValue<>CONVERT(nvarchar(20),@slType) OR AfterValue<>CONVERT(nvarchar(20),@slType))) THROW 51126,'Stock type mismatch.',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=@slLine AND FieldCode='SLDocumentId' AND CASE WHEN @operation='StockDocument.LineDeleted' THEN BeforeValue ELSE AfterValue END<>@slOwner) THROW 51126,'Unrelated stock line.',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType='StockDocument' AND FieldCode='DocumentNumber' AND (AfterState<>3 OR AfterValue<>@EventXml.value('(/Event/Document/text())[1]','nvarchar(50)'))) THROW 51126,'Stock number mismatch.',1;
  END
  ELSE IF EXISTS(SELECT 1 FROM @changes WHERE EntityType<>'StockBucket') THROW 51126,'Additional stock events contain balances only.',1;
 END;
 -- AuditEmployeeValues027: complete persisted value.
 IF @operation IN('EmployeeValue.Created','EmployeeValue.Updated','EmployeeValue.Voided')
 BEGIN
  DECLARE @evOwner nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),
   @evCreate bit=CASE WHEN @operation='EmployeeValue.Created' THEN 1 ELSE 0 END;
  IF @EventXml.value('(/Event/@entityType)[1]','varchar(64)')<>'EmployeeValue'
   OR @EventXml.value('(/Event/@screen)[1]','varchar(64)')<>'Emp_OtherValues'
   OR @EventOrdinal<>1 OR ISNULL(TRY_CONVERT(int,@evOwner),0)<=0
   OR @EventXml.value('count(/Event/Entities/Entity)','int')<>0
   OR @EventXml.value('count(/Event/Document)','int')<>1
   OR @EventXml.value('(/Event/Document/text())[1]','nvarchar(50)')<>@evOwner
   OR (SELECT COUNT(*) FROM @changes)<>10 THROW 51127,'Invalid employee value event.',1;
  IF NOT EXISTS(SELECT 1 FROM Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType='EmployeeValue' AND EntityId=CASE WHEN @evCreate=1 THEN 'New' ELSE @evOwner END) THROW 51127,'Employee request mismatch.',1;
  DECLARE @evFields TABLE(F varchar(64),T varchar(20),S varchar(40));
  INSERT @evFields VALUES
   ('EVCreatedAt','DateTime','datetime'),('EVBusinessDate','DateTime','datetime'),('EVYear','Integer','int'),('EVMonth','Integer','int'),
   ('EVEmployeeId','Integer','int'),('EVType','Integer','int'),('EVAmount','Decimal','numeric(18,3)'),('EVNotes','String','nvarchar(max)'),
   ('EVVoided','Integer','int'),('EVOriginalUserId','Integer','bigint');
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.EntityType<>'EmployeeValue' OR c.EntityId<>@evOwner OR c.ValueOrigin<>1 OR
   NOT EXISTS(SELECT 1 FROM @evFields f WHERE f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType) OR
   (@evCreate=1 AND BeforeState<>1) OR (@evCreate=0 AND BeforeState NOT IN(2,3)) OR AfterState NOT IN(2,3)) THROW 51127,'Invalid employee value field.',1;
  IF @operation='EmployeeValue.Voided' AND NOT EXISTS(SELECT 1 FROM @changes WHERE FieldCode='EVVoided' AND AfterState=3 AND AfterValue='1') THROW 51127,'Employee cancellation not confirmed.',1;
 END;
 -- AuditReturnLines028: complete affected line, recalculated header and derived return.
 IF @operation IN('Return.LineAdded','Return.LineDeleted','Return.LineQuantityChanged')
 BEGIN
  DECLARE @rlOwner nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),
   @rlEntity varchar(64)=@EventXml.value('(/Event/@entityType)[1]','varchar(64)'),
   @rlScreen varchar(64)=@EventXml.value('(/Event/@screen)[1]','varchar(64)'),
   @rlType int,@rlLine varchar(64),@rlCount int;
  SELECT @rlType=BsType_ID FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@rlOwner) AND isVoid=0;
  SET @rlLine=CASE WHEN @rlType IN(10,11) THEN 'ReturnLine' END;
  SET @rlCount=23;
  IF @rlLine IS NULL OR @rlEntity<>'ReturnDocument' OR ISNULL(TRY_CONVERT(int,@rlOwner),0)<=0
   OR @EventXml.value('count(/Event/Entities/Entity)','int')<>0
   OR @EventXml.value('count(/Event/Document)','int')<>1 THROW 51128,'Invalid return document identity.',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@rlEntity AND EntityId=@rlOwner) THROW 51128,'Return request mismatch.',1;
  IF @rlScreen<>'Returns' THROW 51128,'Invalid return screen.',1;
  DECLARE @rlFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @rlFields VALUES
   ('ReturnDocument','Voided','Integer','int'),
   ('ReturnDocument','NeedRepost','Boolean','bit'),
   ('ReturnDocument','NeedCancelReverse','Boolean','bit'),
   ('ReturnDocument','VoidAt','DateTime','datetime'),
   ('ReturnDocument','VoidBy','Integer','int'),
   ('ReturnDocument','VoidReason','String','nvarchar(500)'),
   ('ReturnDocument','JournalId','Integer','int'),
   ('ReturnDocument','DocumentTotal','Decimal','numeric(18,3)'),
   ('ReturnDocument','DocumentPure','Decimal','numeric(18,3)'),
   ('ReturnDocument','Discount','Decimal','numeric(18,3)'),
   ('ReturnDocument','Debit','Decimal','numeric(18,3)'),
   ('ReturnDocument','Credit','Decimal','numeric(18,3)'),
   ('ReturnDocument','Balance','Decimal','numeric(18,3)'),
   ('ReturnDocument','AccountId','Integer','int'),
   ('ReturnDocument','Approved','Integer','int'),
   ('ReturnDocument','DocumentType','Integer','int'),
   ('ReturnDocument','DocumentNumber','Integer','int'),
   ('ReturnDocument','DocumentDate','DateTime','datetime'),
   ('ReturnDocument','SalesOpen','Boolean','bit'),
   ('ReturnDocument','Paused','Boolean','bit'),
   ('ReturnDocument','OrderDelivered','Boolean','bit'),
   ('ReturnDocument','PaidState','Integer','int'),
   ('ReturnDocument','TableId','Integer','int'),
   ('ReturnLine','RLDate','DateTime','datetime'),
   ('ReturnLine','RLDocumentId','Integer','int'),
   ('ReturnLine','RLReturnType','Integer','int'),
   ('ReturnLine','RLItemId','Integer','int'),
   ('ReturnLine','RLUnitId','Integer','bigint'),
   ('ReturnLine','RLStoreId','Integer','bigint'),
   ('ReturnLine','RLComponents','String','nvarchar(max)'),
   ('ReturnLine','RLCost','Decimal','numeric(18,2)'),
   ('ReturnLine','RLPrice','Decimal','numeric(18,2)'),
   ('ReturnLine','RLExpiry','String','nvarchar(50)'),
   ('ReturnLine','RLQuantity','Float','float(53)'),
   ('ReturnLine','RLTotal','Decimal','numeric(18,2)'),
   ('ReturnLine','RLCargo','Float','float(53)'),
   ('ReturnLine','RLStoredQuantity','Decimal','numeric(18,2)'),
   ('ReturnLine','RLApproved','Integer','int'),
   ('ReturnLine','RLSellerPercent','Decimal','numeric(18,2)'),
   ('ReturnLine','RLOriginalBillId','Integer','int'),
   ('ReturnLine','RLProjectId','Integer','int'),
   ('ReturnLine','RLMarketerValue','Decimal','numeric(18,3)'),
   ('ReturnLine','RLBarcode','String','nvarchar(max)'),
   ('ReturnLine','RLSerial','String','nvarchar(100)'),
   ('ReturnLine','RLMinPrice','Integer','int'),
   ('ReturnLine','RLMinPrice2','Integer','int'),
   ('StockBucket','ItemId','Integer','int'),
   ('StockBucket','StoreId','Integer','bigint'),
   ('StockBucket','ExpiryText','String','nvarchar(50)'),
   ('StockBucket','StockQuantity','Float','float(53)');
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.EntityType NOT IN('ReturnDocument','StockBucket',@rlLine) OR c.ValueOrigin<>CASE WHEN c.EntityType='StockBucket' THEN 3 ELSE 1 END OR NOT EXISTS(SELECT 1 FROM @rlFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51128,'Invalid return field.',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>(SELECT COUNT(*) FROM @rlFields f WHERE f.E=EntityType)) THROW 51128,'Incomplete return row.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE (EntityType<>'StockBucket' AND ISNULL(TRY_CONVERT(int,EntityId),0)<=0) OR (EntityType='ReturnDocument' AND EntityId<>@rlOwner) OR (EntityType='StockBucket' AND (LEN(EntityId)<>64 OR EntityId COLLATE Latin1_General_BIN2 LIKE '%[^A-F0-9]%'))) THROW 51128,'Invalid return row identity.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE (EntityType IN('ReturnDocument','StockBucket') AND (BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3))) OR (EntityType=@rlLine AND (
   (@operation='Return.LineAdded' AND (BeforeState<>1 OR AfterState NOT IN(2,3))) OR
   (@operation='Return.LineDeleted' AND (AfterState<>1 OR BeforeState NOT IN(2,3))) OR
   (@operation='Return.LineQuantityChanged' AND (BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3)))))) THROW 51128,'Invalid return value states.',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes WHERE EntityType='ReturnDocument')<>23 OR (SELECT COUNT(*) FROM @changes WHERE EntityType=@rlLine)<>@rlCount OR EXISTS(SELECT 1 FROM @changes WHERE EntityType='StockBucket') THROW 51128,'First return event must contain header and line.',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType='ReturnDocument' AND FieldCode='DocumentType' AND (BeforeValue<>CONVERT(nvarchar(20),@rlType) OR AfterValue<>CONVERT(nvarchar(20),@rlType))) THROW 51128,'Return type mismatch.',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=@rlLine AND FieldCode='RLDocumentId' AND CASE WHEN @operation='Return.LineDeleted' THEN BeforeValue ELSE AfterValue END<>@rlOwner) THROW 51128,'Unrelated return line.',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType='ReturnDocument' AND FieldCode='DocumentNumber' AND (AfterState<>3 OR AfterValue<>@EventXml.value('(/Event/Document/text())[1]','nvarchar(50)'))) THROW 51128,'Return number mismatch.',1;
  END
  ELSE IF EXISTS(SELECT 1 FROM @changes WHERE EntityType<>'StockBucket') THROW 51128,'Additional return events contain balances only.',1;
 END;
 -- AuditExpenseLines029: complete affected line and recalculated header.
 IF @operation IN('Expense.LineAdded','Expense.LineDeleted')
 BEGIN
  DECLARE @elOwner nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),
   @elEntity varchar(64)=@EventXml.value('(/Event/@entityType)[1]','varchar(64)'),
   @elScreen varchar(64)=@EventXml.value('(/Event/@screen)[1]','varchar(64)');
  IF @elEntity<>'ExpenseDocument' OR ISNULL(TRY_CONVERT(int,@elOwner),0)<=0 OR @elScreen<>'EXP_Details' OR @EventOrdinal<>1
   OR @EventXml.value('count(/Event/Entities/Entity)','int')<>0 OR @EventXml.value('count(/Event/Document)','int')<>1
   OR NOT EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@elOwner) AND BsType_ID=2 AND isVoid=0)
   THROW 51129,'Invalid expense document identity.',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@elEntity AND EntityId=@elOwner) THROW 51129,'Expense request mismatch.',1;
  DECLARE @elFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @elFields VALUES
   ('ExpenseDocument','Voided','Integer','int'),
   ('ExpenseDocument','NeedRepost','Boolean','bit'),
   ('ExpenseDocument','NeedCancelReverse','Boolean','bit'),
   ('ExpenseDocument','VoidAt','DateTime','datetime'),
   ('ExpenseDocument','VoidBy','Integer','int'),
   ('ExpenseDocument','VoidReason','String','nvarchar(500)'),
   ('ExpenseDocument','JournalId','Integer','int'),
   ('ExpenseDocument','DocumentTotal','Decimal','numeric(18,3)'),
   ('ExpenseDocument','DocumentPure','Decimal','numeric(18,3)'),
   ('ExpenseDocument','Discount','Decimal','numeric(18,3)'),
   ('ExpenseDocument','Debit','Decimal','numeric(18,3)'),
   ('ExpenseDocument','Credit','Decimal','numeric(18,3)'),
   ('ExpenseDocument','Balance','Decimal','numeric(18,3)'),
   ('ExpenseDocument','AccountId','Integer','int'),
   ('ExpenseDocument','Approved','Integer','int'),
   ('ExpenseDocument','DocumentType','Integer','int'),
   ('ExpenseDocument','DocumentNumber','Integer','int'),
   ('ExpenseDocument','DocumentDate','DateTime','datetime'),
   ('ExpenseDocument','SalesOpen','Boolean','bit'),
   ('ExpenseDocument','Paused','Boolean','bit'),
   ('ExpenseDocument','OrderDelivered','Boolean','bit'),
   ('ExpenseDocument','PaidState','Integer','int'),
   ('ExpenseDocument','TableId','Integer','int'),
   ('ExpenseLine','ExpenseDocumentId','Integer','int'),
   ('ExpenseLine','ExpenseDate','DateTime','datetime'),
   ('ExpenseLine','ExpenseId','Integer','bigint'),
   ('ExpenseLine','ExpenseQuantity','Decimal','numeric(18,3)'),
   ('ExpenseLine','ExpensePrice','Decimal','numeric(18,3)'),
   ('ExpenseLine','ExpenseTotal','Decimal','numeric(18,3)'),
   ('ExpenseLine','ExpenseApproved','Boolean','bit');
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.ValueOrigin<>1 OR NOT EXISTS(SELECT 1 FROM @elFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51129,'Invalid expense field.',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>(SELECT COUNT(*) FROM @elFields f WHERE f.E=EntityType)) THROW 51129,'Incomplete expense row.',1;
  IF (SELECT COUNT(*) FROM @changes WHERE EntityType='ExpenseDocument')<>23 OR (SELECT COUNT(*) FROM @changes WHERE EntityType='ExpenseLine')<>7 THROW 51129,'Expense requires header and one line.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE ISNULL(TRY_CONVERT(int,EntityId),0)<=0 OR (EntityType='ExpenseDocument' AND EntityId<>@elOwner)) THROW 51129,'Invalid expense row identity.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE (EntityType='ExpenseDocument' AND (BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3))) OR (EntityType='ExpenseLine' AND (
   (@operation='Expense.LineAdded' AND (BeforeState<>1 OR AfterState NOT IN(2,3))) OR
   (@operation='Expense.LineDeleted' AND (AfterState<>1 OR BeforeState NOT IN(2,3)))))) THROW 51129,'Invalid expense value states.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType='ExpenseDocument' AND FieldCode='DocumentType' AND (BeforeValue<>'2' OR AfterValue<>'2')) THROW 51129,'Expense type mismatch.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType='ExpenseLine' AND FieldCode='ExpenseDocumentId' AND CASE WHEN @operation='Expense.LineDeleted' THEN BeforeValue ELSE AfterValue END<>@elOwner) THROW 51129,'Unrelated expense line.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType='ExpenseDocument' AND FieldCode='DocumentNumber' AND (AfterState<>3 OR AfterValue<>@EventXml.value('(/Event/Document/text())[1]','nvarchar(50)'))) THROW 51129,'Expense number mismatch.',1;
 END;
 -- AuditAccountEntry030: stored manual balance and salary entries.
 IF @operation='AccountEntry.Created'
 BEGIN
  DECLARE @aeId nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),
   @aeScreen varchar(64)=@EventXml.value('(/Event/@screen)[1]','varchar(64)');
  IF @aeScreen<>'Add_Prev_Balance' OR @EventXml.value('(/Event/@entityType)[1]','varchar(64)')<>'AccountEntry' OR ISNULL(TRY_CONVERT(int,@aeId),0)<=0
   OR @EventOrdinal<>1 OR @EventXml.value('count(/Event/Entities/Entity)','int')<>0 OR @EventXml.value('count(/Event/Document)','int')<>1
   OR @EventXml.value('(/Event/Document/text())[1]','nvarchar(50)')<>@aeId THROW 51130,'Invalid account entry identity.',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType='AccountEntry' AND EntityId='New') THROW 51130,'Account entry request mismatch.',1;
  DECLARE @aeFields TABLE(F varchar(64),T varchar(20),S varchar(40));
  INSERT @aeFields VALUES
   ('Voided','Integer','int'),
   ('NeedRepost','Boolean','bit'),
   ('NeedCancelReverse','Boolean','bit'),
   ('VoidAt','DateTime','datetime'),
   ('VoidBy','Integer','int'),
   ('VoidReason','String','nvarchar(500)'),
   ('JournalId','Integer','int'),
   ('DocumentTotal','Decimal','numeric(18,3)'),
   ('DocumentPure','Decimal','numeric(18,3)'),
   ('Discount','Decimal','numeric(18,3)'),
   ('Debit','Decimal','numeric(18,3)'),
   ('Credit','Decimal','numeric(18,3)'),
   ('Balance','Decimal','numeric(18,3)'),
   ('AccountId','Integer','int'),
   ('Approved','Integer','int'),
   ('DocumentType','Integer','int'),
   ('DocumentNumber','Integer','int'),
   ('DocumentDate','DateTime','datetime'),
   ('SalesOpen','Boolean','bit'),
   ('Paused','Boolean','bit'),
   ('OrderDelivered','Boolean','bit'),
   ('PaidState','Integer','int'),
   ('TableId','Integer','int'),
   ('EntryTitle','String','nvarchar(550)'),('EntryUserId','Integer','int');
  IF (SELECT COUNT(*) FROM @changes)<>25 OR EXISTS(SELECT 1 FROM @changes c WHERE EntityType<>'AccountEntry' OR EntityId<>@aeId OR ValueOrigin<>1 OR BeforeState<>1 OR AfterState NOT IN(2,3) OR NOT EXISTS(SELECT 1 FROM @aeFields f WHERE f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51130,'Incomplete account entry fields.',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@aeId) AND BsType_ID IN(5,6,37) AND isVoid=0 AND isDepended=1) THROW 51130,'Missing account entry.',1;
  IF NOT EXISTS(SELECT 1 FROM @changes WHERE FieldCode='DocumentType' AND AfterState=3 AND AfterValue IN('5','6','37')) THROW 51130,'Invalid account entry type.',1;
  IF NOT EXISTS(SELECT 1 FROM @changes WHERE FieldCode='DocumentNumber' AND AfterState=3 AND AfterValue=@aeId) THROW 51130,'Account entry number mismatch.',1;
 END;
 -- AuditPurchaseAddition031: complete line, header, cost, prices, expiry notes and stock.
 IF @operation='Purchase.LineAdded'
 BEGIN
  DECLARE @paOwner nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),
   @paEntity varchar(64)=@EventXml.value('(/Event/@entityType)[1]','varchar(64)'),
   @paScreen varchar(64)=@EventXml.value('(/Event/@screen)[1]','varchar(64)'),@paKind int;
  SELECT @paKind=BsType_ID FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@paOwner) AND isVoid=0;
  IF @paEntity<>'PurchaseDocument' OR ISNULL(TRY_CONVERT(int,@paOwner),0)<=0 OR ISNULL(@paKind,0) NOT IN(7,9)
   OR @paScreen<>CASE WHEN @paKind=7 THEN 'Pch_IM_card_11' ELSE 'Invoice_IM_card' END
   OR @EventXml.value('count(/Event/Entities/Entity)','int')<>0 OR @EventXml.value('count(/Event/Document)','int')<>1
   THROW 51131,'Invalid purchase document identity.',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@paEntity AND EntityId=@paOwner) THROW 51131,'Purchase request mismatch.',1;
  DECLARE @paFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @paFields VALUES
   ('PurchaseDocument','Voided','Integer','int'),
   ('PurchaseDocument','NeedRepost','Boolean','bit'),
   ('PurchaseDocument','NeedCancelReverse','Boolean','bit'),
   ('PurchaseDocument','VoidAt','DateTime','datetime'),
   ('PurchaseDocument','VoidBy','Integer','int'),
   ('PurchaseDocument','VoidReason','String','nvarchar(500)'),
   ('PurchaseDocument','JournalId','Integer','int'),
   ('PurchaseDocument','DocumentTotal','Decimal','numeric(18,3)'),
   ('PurchaseDocument','DocumentPure','Decimal','numeric(18,3)'),
   ('PurchaseDocument','Discount','Decimal','numeric(18,3)'),
   ('PurchaseDocument','Debit','Decimal','numeric(18,3)'),
   ('PurchaseDocument','Credit','Decimal','numeric(18,3)'),
   ('PurchaseDocument','Balance','Decimal','numeric(18,3)'),
   ('PurchaseDocument','AccountId','Integer','int'),
   ('PurchaseDocument','Approved','Integer','int'),
   ('PurchaseDocument','DocumentType','Integer','int'),
   ('PurchaseDocument','DocumentNumber','Integer','int'),
   ('PurchaseDocument','DocumentDate','DateTime','datetime'),
   ('PurchaseDocument','SalesOpen','Boolean','bit'),
   ('PurchaseDocument','Paused','Boolean','bit'),
   ('PurchaseDocument','OrderDelivered','Boolean','bit'),
   ('PurchaseDocument','PaidState','Integer','int'),
   ('PurchaseDocument','TableId','Integer','int'),
   ('PurchaseLine','PurchaseDocumentId','Integer','int'),
   ('PurchaseLine','PurchaseDate','DateTime','datetime'),
   ('PurchaseLine','PurchaseItemId','Integer','int'),
   ('PurchaseLine','PurchaseUnitId','Integer','bigint'),
   ('PurchaseLine','PurchaseStoreId','Integer','bigint'),
   ('PurchaseLine','PurchasePrice','Float','float'),
   ('PurchaseLine','PurchaseExpiryText','String','nvarchar(50)'),
   ('PurchaseLine','PurchaseQuantity','Float','float'),
   ('PurchaseLine','PurchaseTotal','Float','float'),
   ('PurchaseLine','PurchaseNotes','String','nvarchar(max)'),
   ('PurchaseLine','PurchaseBarcode','String','nvarchar(max)'),
   ('PurchaseLine','PurchaseNewSale','Decimal','numeric(18,2)'),
   ('PurchaseLine','PurchaseNewSaleByOne','Decimal','numeric(18,2)'),
   ('PurchaseLine','PurchaseCargo','Float','float'),
   ('PurchaseLine','PurchaseStockQuantity','Decimal','numeric(18,2)'),
   ('PurchaseLine','PurchaseApproved','Boolean','bit'),
   ('PurchaseLine','PurchaseMinSale','Decimal','numeric(18,2)'),
   ('PurchaseLine','PurchaseMinSaleByOne','Decimal','numeric(18,2)'),
   ('PurchaseLine','PurchaseAverageCost','Float','float'),
   ('PurchaseLine','PurchaseMinSale2','Decimal','numeric(18,2)'),
   ('PurchaseLine','PurchaseMinSale2ByOne','Decimal','numeric(18,2)'),
   ('PurchaseLine','PurchaseCostChange','Float','float'),
   ('PurchaseLine','PurchaseStockDifference','Decimal','numeric(18,2)'),
   ('PurchaseLine','PurchaseExpenses','Float','float'),
   ('PurchaseLine','PurchaseOriginalPrice','Float','float'),
   ('PurchaseLine','PurchaseForeignPrice','Float','float'),
   ('PurchaseLine','PurchaseExchangePrice','Float','float'),
   ('PurchaseLine','PurchaseCurrencyId','Integer','int'),
   ('PurchaseItem','PurchaseItemName','String','nvarchar(500)'),
   ('PurchaseItem','PurchaseStoreMode','Integer','int'),
   ('PurchaseItem','PurchaseItemCost','Float','float'),
   ('PurchaseItem','PurchaseLastLine','Integer','int'),
   ('PurchaseItem','PurchaseLastCargoCost','Decimal','numeric(18,3)'),
   ('PurchaseUnit','PriceItemId','Integer','int'),
   ('PurchaseUnit','PriceUnitId','Integer','int'),
   ('PurchaseUnit','UnitSalePrice','Decimal','numeric(18,3)'),
   ('PurchaseUnit','UnitMinimumPrice','Decimal','numeric(18,3)'),
   ('PurchaseUnit','UnitMinimumPrice2','Decimal','numeric(18,3)'),
   ('PurchaseExpiry','ExpiryLineId','Integer','int'),
   ('PurchaseExpiry','ExpiryItemId','Integer','int'),
   ('PurchaseExpiry','ExpiryDate','DateTime','datetime'),
   ('StockBucket','ItemId','Integer','int'),
   ('StockBucket','StoreId','Integer','bigint'),
   ('StockBucket','ExpiryText','String','nvarchar(50)'),
   ('StockBucket','StockQuantity','Float','float(53)');
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.ValueOrigin<>CASE WHEN c.EntityType='StockBucket' THEN 3 ELSE 1 END OR NOT EXISTS(SELECT 1 FROM @paFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51131,'Invalid purchase field.',1;
  IF EXISTS(SELECT c.EntityType,c.EntityId FROM @changes c GROUP BY c.EntityType,c.EntityId HAVING COUNT(*)<>(SELECT COUNT(*) FROM @paFields f WHERE f.E=c.EntityType)) THROW 51131,'Incomplete purchase row.',1;
  IF @EventOrdinal=1 AND ((SELECT COUNT(*) FROM @changes)<>51 OR (SELECT COUNT(*) FROM @changes WHERE EntityType='PurchaseDocument')<>23 OR (SELECT COUNT(*) FROM @changes WHERE EntityType='PurchaseLine')<>28) THROW 51131,'Purchase requires header and added line.',1;
  IF @EventOrdinal>1 AND EXISTS(SELECT 1 FROM @changes WHERE EntityType='PurchaseDocument') THROW 51131,'Repeated purchase header.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE (EntityType<>'StockBucket' AND ISNULL(TRY_CONVERT(bigint,EntityId),0)<=0) OR (EntityType='StockBucket' AND (LEN(EntityId)<>64 OR EntityId LIKE '%[^A-F0-9]%' COLLATE Latin1_General_100_BIN2)) OR (EntityType='PurchaseDocument' AND EntityId<>@paOwner)) THROW 51131,'Invalid purchase row identity.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE AfterState NOT IN(2,3) OR
    ((EntityType='PurchaseExpiry' OR (EntityType='PurchaseLine' AND @EventOrdinal=1)) AND BeforeState<>1) OR
    (NOT(EntityType='PurchaseExpiry' OR (EntityType='PurchaseLine' AND @EventOrdinal=1)) AND BeforeState NOT IN(2,3))) THROW 51131,'Invalid purchase value states.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType='PurchaseDocument' AND FieldCode='DocumentType' AND (BeforeValue<>CONVERT(varchar(10),@paKind) OR AfterValue<>CONVERT(varchar(10),@paKind))) THROW 51131,'Purchase type mismatch.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType='PurchaseDocument' AND FieldCode='DocumentNumber' AND (AfterState<>3 OR AfterValue<>@EventXml.value('(/Event/Document/text())[1]','nvarchar(50)'))) THROW 51131,'Purchase number mismatch.',1;
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.EntityType='PurchaseLine' AND (NOT EXISTS(SELECT 1 FROM dbo.Pch_Details p WITH(HOLDLOCK) WHERE p.T_ID=TRY_CONVERT(bigint,c.EntityId) AND p.Pch_T_ID=TRY_CONVERT(int,@paOwner)) OR (c.FieldCode='PurchaseDocumentId' AND (c.AfterState<>3 OR c.AfterValue<>@paOwner)))) THROW 51131,'Unrelated purchase line.',1;
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.EntityType='PurchaseExpiry' AND NOT EXISTS(SELECT 1 FROM dbo.Valid_Notes n WITH(HOLDLOCK) JOIN dbo.Pch_Details p WITH(HOLDLOCK) ON p.T_ID=n.Pch_T_ID WHERE n.T_ID=TRY_CONVERT(int,c.EntityId) AND p.Pch_T_ID=TRY_CONVERT(int,@paOwner))) THROW 51131,'Unrelated expiry note.',1;
  IF @EventOrdinal>1 AND NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Event WHERE SourceId=@SourceId AND RequestId=@RequestId AND EventOrdinal=1 AND EventCode='Purchase.LineAdded') THROW 51131,'Missing purchase root event.',1;
 END;
 -- AuditDocumentFields032: closed direct account / discount edits only.
 IF @operation IN('Document.AccountChanged','Document.DiscountChanged','Document.FastDiscountChanged')
 BEGIN
  DECLARE @dfOwner nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),
   @dfEntity varchar(64)=@EventXml.value('(/Event/@entityType)[1]','varchar(64)'),
   @dfScreen varchar(64)=@EventXml.value('(/Event/@screen)[1]','varchar(64)'),
   @dfKind int,@dfNumber int,@dfAccount int,@dfDiscount numeric(18,3),@dfPercent numeric(18,3);
  SELECT @dfKind=BsType_ID,@dfAccount=AG_ID,@dfDiscount=Discount,@dfPercent=Debit_2,
   @dfNumber=CASE BsType_ID WHEN 1 THEN SB_ID WHEN 7 THEN Pch_ID WHEN 16 THEN ViewSB_ID END
  FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@dfOwner) AND isVoid=0;
  IF @dfEntity<>'DocumentHeader' OR ISNULL(TRY_CONVERT(int,@dfOwner),0)<=0 OR ISNULL(@dfKind,0) NOT IN(1,7,16)
   OR @EventOrdinal<>1 OR @EventXml.value('count(/Event/Entities/Entity)','int')<>0
   OR @EventXml.value('count(/Event/Document)','int')<>1 OR @dfNumber IS NULL
   OR @EventXml.value('(/Event/Document/text())[1]','nvarchar(50)')<>CONVERT(nvarchar(50),@dfNumber)
   THROW 51132,'Invalid direct header identity.',1;
  IF NOT((@operation='Document.AccountChanged' AND ((@dfKind=1 AND @dfScreen IN('Sales','AgentsMenu')) OR (@dfKind=16 AND @dfScreen='ViewBill')))
   OR (@operation='Document.DiscountChanged' AND ((@dfKind=1 AND @dfScreen IN('Sales','Percent_Disc','SBChangePrice')) OR (@dfKind=7 AND @dfScreen IN('Pch','Percent_Disc'))))
   OR (@operation='Document.FastDiscountChanged' AND @dfKind=1 AND @dfScreen='Fast_SB_Discount'))
   THROW 51132,'Invalid direct header screen.',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@dfEntity AND EntityId=@dfOwner)
   THROW 51132,'Direct header request mismatch.',1;
  DECLARE @dfFields TABLE(F varchar(64),T varchar(20),S varchar(40));
  INSERT @dfFields VALUES
   ('Voided','Integer','int'),
   ('NeedRepost','Boolean','bit'),
   ('NeedCancelReverse','Boolean','bit'),
   ('VoidAt','DateTime','datetime'),
   ('VoidBy','Integer','int'),
   ('VoidReason','String','nvarchar(500)'),
   ('JournalId','Integer','int'),
   ('DocumentTotal','Decimal','numeric(18,3)'),
   ('DocumentPure','Decimal','numeric(18,3)'),
   ('Discount','Decimal','numeric(18,3)'),
   ('Debit','Decimal','numeric(18,3)'),
   ('Credit','Decimal','numeric(18,3)'),
   ('Balance','Decimal','numeric(18,3)'),
   ('AccountId','Integer','int'),
   ('Approved','Integer','int'),
   ('DocumentType','Integer','int'),
   ('DocumentNumber','Integer','int'),
   ('DocumentDate','DateTime','datetime'),
   ('SalesOpen','Boolean','bit'),
   ('Paused','Boolean','bit'),
   ('OrderDelivered','Boolean','bit'),
   ('PaidState','Integer','int'),
   ('TableId','Integer','int'),
   ('AccountName','String','nvarchar(500)'),
   ('SecondaryDebit','Decimal','numeric(18,3)'),
   ('EditOpenedAt','DateTime','datetime'),
   ('EditOpenedBy','Integer','int');
  IF (SELECT COUNT(*) FROM @changes)<>27 OR EXISTS(SELECT 1 FROM @changes c WHERE c.EntityType<>@dfEntity OR c.EntityId<>@dfOwner OR c.ValueOrigin<>1 OR c.BeforeState NOT IN(2,3) OR c.AfterState NOT IN(2,3)
    OR NOT EXISTS(SELECT 1 FROM @dfFields f WHERE f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType))
   THROW 51132,'Invalid or incomplete direct header fields.',1;
  IF EXISTS(SELECT FieldCode FROM @changes GROUP BY FieldCode HAVING COUNT(*)<>1)
   THROW 51132,'Duplicate direct header field.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE NOT((@operation='Document.AccountChanged' AND FieldCode IN('AccountId','AccountName')) OR (@operation='Document.DiscountChanged' AND FieldCode='Discount') OR (@operation='Document.FastDiscountChanged' AND FieldCode IN('Discount','SecondaryDebit')))
   AND (BeforeState<>AfterState OR ISNULL(BeforeValue,N'') COLLATE Latin1_General_100_BIN2<>ISNULL(AfterValue,N'') COLLATE Latin1_General_100_BIN2))
   THROW 51132,'Unexpected direct header side effect.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE FieldCode='DocumentType' AND (AfterState<>3 OR AfterValue<>CONVERT(nvarchar(20),@dfKind)))
   OR EXISTS(SELECT 1 FROM @changes WHERE FieldCode='DocumentNumber' AND (AfterState<>3 OR AfterValue<>CONVERT(nvarchar(20),@dfNumber)))
   OR EXISTS(SELECT 1 FROM @changes WHERE FieldCode='AccountId' AND (AfterState<>3 OR AfterValue<>CONVERT(nvarchar(20),@dfAccount)))
   OR EXISTS(SELECT 1 FROM @changes WHERE FieldCode='Discount' AND ((@dfDiscount IS NULL AND AfterState<>2) OR (@dfDiscount IS NOT NULL AND (AfterState<>3 OR TRY_CONVERT(numeric(18,3),AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),AfterValue)<>@dfDiscount))))
   THROW 51132,'Stored direct header result mismatch.',1;
  IF @operation='Document.FastDiscountChanged' AND EXISTS(SELECT 1 FROM @changes WHERE FieldCode='SecondaryDebit' AND ((@dfPercent IS NULL AND AfterState<>2) OR (@dfPercent IS NOT NULL AND (AfterState<>3 OR TRY_CONVERT(numeric(18,3),AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),AfterValue)<>@dfPercent))))
   THROW 51132,'Stored fast discount percentage mismatch.',1;
 END;
 -- AuditQuoteLines034: quote-only row mutations, after correction 033.
 IF @operation IN('Quote.LineAdded','Quote.LineDeleted','Quote.LineChanged','Quote.LineQuantityStepped')
 BEGIN
  DECLARE @qOwner nvarchar(128)=@EventXml.value('(/Event/@entityId)[1]','nvarchar(128)'),
   @qScreen varchar(64)=@EventXml.value('(/Event/@screen)[1]','varchar(64)'),@qNumber int,@qLine nvarchar(128);
  IF @EventXml.value('(/Event/@entityType)[1]','varchar(64)')<>'QuoteDocument' OR ISNULL(TRY_CONVERT(int,@qOwner),0)<=0 OR @EventOrdinal<>1
   OR @EventXml.value('count(/Event/Entities/Entity)','int')<>0 OR @EventXml.value('count(/Event/Document)','int')<>1
   THROW 51134,'Invalid quote event identity.',1;
  SELECT @qNumber=ViewSB_ID FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@qOwner) AND BsType_ID=16 AND isVoid=0;
  IF @qNumber IS NULL OR @EventXml.value('(/Event/Document/text())[1]','nvarchar(50)')<>CONVERT(nvarchar(50),@qNumber) THROW 51134,'Quote number mismatch.',1;
  IF NOT((@operation='Quote.LineAdded' AND @qScreen='ViewBill_IM_card') OR (@operation='Quote.LineChanged' AND @qScreen='Change_IM_Details') OR (@operation IN('Quote.LineDeleted','Quote.LineQuantityStepped') AND @qScreen='ViewBill')) THROW 51134,'Invalid quote screen.',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType='QuoteDocument' AND EntityId=@qOwner) THROW 51134,'Quote request mismatch.',1;
  DECLARE @qFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @qFields VALUES
   ('QuoteDocument','Voided','Integer','int'),
   ('QuoteDocument','NeedRepost','Boolean','bit'),
   ('QuoteDocument','NeedCancelReverse','Boolean','bit'),
   ('QuoteDocument','VoidAt','DateTime','datetime'),
   ('QuoteDocument','VoidBy','Integer','int'),
   ('QuoteDocument','VoidReason','String','nvarchar(500)'),
   ('QuoteDocument','JournalId','Integer','int'),
   ('QuoteDocument','DocumentTotal','Decimal','numeric(18,3)'),
   ('QuoteDocument','DocumentPure','Decimal','numeric(18,3)'),
   ('QuoteDocument','Discount','Decimal','numeric(18,3)'),
   ('QuoteDocument','Debit','Decimal','numeric(18,3)'),
   ('QuoteDocument','Credit','Decimal','numeric(18,3)'),
   ('QuoteDocument','Balance','Decimal','numeric(18,3)'),
   ('QuoteDocument','AccountId','Integer','int'),
   ('QuoteDocument','Approved','Integer','int'),
   ('QuoteDocument','DocumentType','Integer','int'),
   ('QuoteDocument','DocumentNumber','Integer','int'),
   ('QuoteDocument','DocumentDate','DateTime','datetime'),
   ('QuoteDocument','SalesOpen','Boolean','bit'),
   ('QuoteDocument','Paused','Boolean','bit'),
   ('QuoteDocument','OrderDelivered','Boolean','bit'),
   ('QuoteDocument','PaidState','Integer','int'),
   ('QuoteDocument','TableId','Integer','int'),
   ('QuoteDocument','AccountName','String','nvarchar(500)'),
   ('QuoteDocument','SecondaryDebit','Decimal','numeric(18,3)'),
   ('QuoteDocument','EditOpenedAt','DateTime','datetime'),
   ('QuoteDocument','EditOpenedBy','Integer','int'),
   ('QuoteLine','QuoteOwnerId','Integer','int'),
   ('QuoteLine','QuoteItemId','Integer','int'),
   ('QuoteLine','QuoteUnitId','Integer','bigint'),
   ('QuoteLine','QuoteStoreId','Integer','bigint'),
   ('QuoteLine','QuoteLineDate','DateTime','datetime'),
   ('QuoteLine','QuoteComponents','String','nvarchar(max)'),
   ('QuoteLine','QuoteCost','Decimal','numeric(18,3)'),
   ('QuoteLine','QuotePrice','Decimal','numeric(18,3)'),
   ('QuoteLine','QuoteExpiry','String','nvarchar(50)'),
   ('QuoteLine','QuoteQuantity','Float','float(53)'),
   ('QuoteLine','QuoteLineTotal','Decimal','numeric(18,3)'),
   ('QuoteLine','QuoteCargo','Float','float(53)'),
   ('QuoteLine','QuoteStockQuantity','Decimal','numeric(18,2)'),
   ('QuoteLine','QuoteApproved','Integer','int'),
   ('QuoteLine','QuoteParentLine','Integer','int'),
   ('QuoteLine','QuoteSalespersonPercent','Decimal','numeric(18,2)'),
   ('QuoteLine','QuoteNotes','String','nvarchar(max)'),
   ('QuoteLine','QuoteBarcode','String','nvarchar(50)'),
   ('QuoteLine','QuoteItemName','String','nvarchar(500)');
  IF (SELECT COUNT(*) FROM @changes)<>46 OR EXISTS(SELECT 1 FROM @changes c WHERE c.ValueOrigin<>1 OR NOT EXISTS(SELECT 1 FROM @qFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType))
   THROW 51134,'Invalid or incomplete quote fields.',1;
  IF EXISTS(SELECT EntityType,FieldCode FROM @changes GROUP BY EntityType,FieldCode HAVING COUNT(*)<>1) THROW 51134,'Duplicate quote field.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType='QuoteDocument' AND (EntityId<>@qOwner OR BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3))) THROW 51134,'Invalid quote header state.',1;
  IF (SELECT COUNT(DISTINCT EntityId) FROM @changes WHERE EntityType='QuoteLine')<>1 THROW 51134,'Expected one quote row.',1;
  SELECT TOP(1) @qLine=EntityId FROM @changes WHERE EntityType='QuoteLine';
  IF ISNULL(TRY_CONVERT(int,@qLine),0)<=0 THROW 51134,'Invalid quote row id.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType='QuoteLine' AND
    ((@operation='Quote.LineAdded' AND (BeforeState<>1 OR AfterState NOT IN(2,3)))
     OR (@operation='Quote.LineDeleted' AND (BeforeState NOT IN(2,3) OR AfterState<>1))
     OR (@operation IN('Quote.LineChanged','Quote.LineQuantityStepped') AND (BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3))))) THROW 51134,'Invalid quote row state.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE FieldCode='QuoteOwnerId' AND CASE WHEN @operation='Quote.LineDeleted' THEN BeforeValue ELSE AfterValue END<>@qOwner)
   OR EXISTS(SELECT 1 FROM @changes WHERE FieldCode='DocumentType' AND (AfterState<>3 OR AfterValue<>'16'))
   OR EXISTS(SELECT 1 FROM @changes WHERE FieldCode='DocumentNumber' AND (AfterState<>3 OR AfterValue<>CONVERT(nvarchar(50),@qNumber))) THROW 51134,'Quote owner/type mismatch.',1;
  IF @operation='Quote.LineDeleted'
  BEGIN
   IF EXISTS(SELECT 1 FROM dbo.ViewBill_Contents WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@qLine)) THROW 51134,'Deleted quote row still exists.',1;
   IF CHARINDEX('QuoteDelete033:',ISNULL(OBJECT_DEFINITION(OBJECT_ID('dbo.ViewBill_Contents_Delete_IM')),N''))=0 THROW 51134,'Quote delete correction missing.',1;
  END
  ELSE IF NOT EXISTS(SELECT 1 FROM dbo.ViewBill_Contents WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@qLine) AND Bill_T_ID=TRY_CONVERT(int,@qOwner)) THROW 51134,'Saved quote row missing.',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType='QuoteDocument' AND NOT(@operation IN('Quote.LineAdded','Quote.LineDeleted') AND FieldCode IN('DocumentTotal','DocumentPure'))
   AND (BeforeState<>AfterState OR ISNULL(BeforeValue,N'') COLLATE Latin1_General_100_BIN2<>ISNULL(AfterValue,N'') COLLATE Latin1_General_100_BIN2)) THROW 51134,'Unexpected quote header effect.',1;
  IF @operation IN('Quote.LineChanged','Quote.LineQuantityStepped') AND EXISTS(SELECT 1 FROM @changes WHERE EntityType='QuoteLine' AND NOT
   ((@operation='Quote.LineChanged' AND FieldCode IN('QuoteUnitId','QuoteQuantity','QuotePrice','QuoteLineTotal','QuoteCargo','QuoteStockQuantity')) OR (@operation='Quote.LineQuantityStepped' AND FieldCode IN('QuoteQuantity','QuoteLineTotal','QuoteStockQuantity')))
   AND (BeforeState<>AfterState OR ISNULL(BeforeValue,N'') COLLATE Latin1_General_100_BIN2<>ISNULL(AfterValue,N'') COLLATE Latin1_General_100_BIN2)) THROW 51134,'Unexpected quote row effect.',1;
 END;
 INSERT dbo.Network_Audit_Event(EventId,SourceId,RequestId,EventOrdinal,CorrelationId,ContractVersion,EventCode,DomainCode,ScreenCode,EventKind,Outcome,
 ActorKind,ActorUserId,ActorServiceCode,ActorName,SessionId,MachineName,ApplicationVersion,EntityType,EntityId,DocumentNumber,OccurredAtUtc,BusinessDate,
 ReasonCode,TemplateCode,TemplateVersion,Notes,RelatedEntitiesXml,EventFingerprint,CanonicalEvent)
 SELECT @EventId,@SourceId,@RequestId,@EventOrdinal,
 CASE WHEN @EventXml.exist('/Event/@correlation')=1 THEN @EventXml.value('(/Event/@correlation)[1]','uniqueidentifier') END,
 1,@eventCode,@domain,@EventXml.value('(/Event/@screen)[1]','varchar(64)'),@kind,@outcome,@actor,@user,@service,
 CASE WHEN @EventXml.exist('/Event/ActorName')=1 THEN @EventXml.value('(/Event/ActorName)[1]','nvarchar(256)') END,@session,
 @EventXml.value('(/Event/Machine)[1]','nvarchar(128)'),@EventXml.value('(/Event/Application)[1]','nvarchar(50)'),
 CASE WHEN @EventXml.exist('/Event/@entityType')=1 THEN @EventXml.value('(/Event/@entityType)[1]','varchar(64)') END,
 CASE WHEN @EventXml.exist('/Event/@entityId')=1 THEN @EventXml.value('(/Event/@entityId)[1]','nvarchar(128)') END,
 CASE WHEN @EventXml.exist('/Event/Document')=1 THEN @EventXml.value('(/Event/Document)[1]','nvarchar(50)') END,
 @EventXml.value('(/Event/@occurred)[1]','datetime2(3)'),
 CASE WHEN @EventXml.exist('/Event/@businessDate')=1 THEN @EventXml.value('(/Event/@businessDate)[1]','datetime2(3)') END,
 CASE WHEN @EventXml.exist('/Event/@reason')=1 THEN @EventXml.value('(/Event/@reason)[1]','varchar(64)') END,
 @eventCode,1,@EventXml.value('(/Event/Notes)[1]','nvarchar(max)'),@EventXml.query('/Event/Entities'),@EventFingerprint,@CanonicalEvent;
 INSERT dbo.Network_Audit_Change(EventId,ChangeNo,EntityType,EntityId,FieldCode,ValueType,SourceSqlType,BeforeState,BeforeValue,AfterState,AfterValue,ValueOrigin)
 SELECT @EventId,ChangeNo,EntityType,EntityId,FieldCode,ValueType,SourceSqlType,BeforeState,BeforeValue,AfterState,AfterValue,ValueOrigin FROM @changes;
 SELECT CAST(1 AS int) WriteResult;
END
