/* SQL Server 2014. Purchase/stock-count additions. Original business procedures and schemas unchanged. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51131,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51131,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51131,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51131,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Finance.AccountEntries"]')=1) THROW 51131,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51131,'Incomplete audit requests.',1;
 IF EXISTS(SELECT 1 FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('Pch_Details'),OBJECT_ID('Agents_Balance_MV'),OBJECT_ID('IM_Menu'),OBJECT_ID('IM_Units'),OBJECT_ID('Valid_Notes'))) THROW 51131,'Review business triggers before deployment.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Purchase.LineAdded',@sql)>0 OR CHARINDEX('AccountEntry.Created',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51131,'Unexpected begin baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''Purchase.LineAdded'') AND @DomainCode=''Inventory.PurchaseAddition'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Purchase.LineAdded',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51131,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''Purchase.LineAdded'') AND @domain=''Inventory.PurchaseAddition'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51131,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Purchase.LineAdded'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51131,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditPurchaseAddition031: complete line, header, cost, prices, expiry notes and stock.
 IF @operation=''Purchase.LineAdded''
 BEGIN
  DECLARE @paOwner nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @paEntity varchar(64)=@EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)''),
   @paScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)''),@paKind int;
  SELECT @paKind=BsType_ID FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@paOwner) AND isVoid=0;
  IF @paEntity<>''PurchaseDocument'' OR ISNULL(TRY_CONVERT(int,@paOwner),0)<=0 OR ISNULL(@paKind,0) NOT IN(7,9)
   OR @paScreen<>CASE WHEN @paKind=7 THEN ''Pch_IM_card_11'' ELSE ''Invoice_IM_card'' END
   OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0 OR @EventXml.value(''count(/Event/Document)'',''int'')<>1
   THROW 51131,''Invalid purchase document identity.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@paEntity AND EntityId=@paOwner) THROW 51131,''Purchase request mismatch.'',1;
  DECLARE @paFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @paFields VALUES
   (''PurchaseDocument'',''Voided'',''Integer'',''int''),
   (''PurchaseDocument'',''NeedRepost'',''Boolean'',''bit''),
   (''PurchaseDocument'',''NeedCancelReverse'',''Boolean'',''bit''),
   (''PurchaseDocument'',''VoidAt'',''DateTime'',''datetime''),
   (''PurchaseDocument'',''VoidBy'',''Integer'',''int''),
   (''PurchaseDocument'',''VoidReason'',''String'',''nvarchar(500)''),
   (''PurchaseDocument'',''JournalId'',''Integer'',''int''),
   (''PurchaseDocument'',''DocumentTotal'',''Decimal'',''numeric(18,3)''),
   (''PurchaseDocument'',''DocumentPure'',''Decimal'',''numeric(18,3)''),
   (''PurchaseDocument'',''Discount'',''Decimal'',''numeric(18,3)''),
   (''PurchaseDocument'',''Debit'',''Decimal'',''numeric(18,3)''),
   (''PurchaseDocument'',''Credit'',''Decimal'',''numeric(18,3)''),
   (''PurchaseDocument'',''Balance'',''Decimal'',''numeric(18,3)''),
   (''PurchaseDocument'',''AccountId'',''Integer'',''int''),
   (''PurchaseDocument'',''Approved'',''Integer'',''int''),
   (''PurchaseDocument'',''DocumentType'',''Integer'',''int''),
   (''PurchaseDocument'',''DocumentNumber'',''Integer'',''int''),
   (''PurchaseDocument'',''DocumentDate'',''DateTime'',''datetime''),
   (''PurchaseDocument'',''SalesOpen'',''Boolean'',''bit''),
   (''PurchaseDocument'',''Paused'',''Boolean'',''bit''),
   (''PurchaseDocument'',''OrderDelivered'',''Boolean'',''bit''),
   (''PurchaseDocument'',''PaidState'',''Integer'',''int''),
   (''PurchaseDocument'',''TableId'',''Integer'',''int''),
   (''PurchaseLine'',''PurchaseDocumentId'',''Integer'',''int''),
   (''PurchaseLine'',''PurchaseDate'',''DateTime'',''datetime''),
   (''PurchaseLine'',''PurchaseItemId'',''Integer'',''int''),
   (''PurchaseLine'',''PurchaseUnitId'',''Integer'',''bigint''),
   (''PurchaseLine'',''PurchaseStoreId'',''Integer'',''bigint''),
   (''PurchaseLine'',''PurchasePrice'',''Float'',''float''),
   (''PurchaseLine'',''PurchaseExpiryText'',''String'',''nvarchar(50)''),
   (''PurchaseLine'',''PurchaseQuantity'',''Float'',''float''),
   (''PurchaseLine'',''PurchaseTotal'',''Float'',''float''),
   (''PurchaseLine'',''PurchaseNotes'',''String'',''nvarchar(max)''),
   (''PurchaseLine'',''PurchaseBarcode'',''String'',''nvarchar(max)''),
   (''PurchaseLine'',''PurchaseNewSale'',''Decimal'',''numeric(18,2)''),
   (''PurchaseLine'',''PurchaseNewSaleByOne'',''Decimal'',''numeric(18,2)''),
   (''PurchaseLine'',''PurchaseCargo'',''Float'',''float''),
   (''PurchaseLine'',''PurchaseStockQuantity'',''Decimal'',''numeric(18,2)''),
   (''PurchaseLine'',''PurchaseApproved'',''Boolean'',''bit''),
   (''PurchaseLine'',''PurchaseMinSale'',''Decimal'',''numeric(18,2)''),
   (''PurchaseLine'',''PurchaseMinSaleByOne'',''Decimal'',''numeric(18,2)''),
   (''PurchaseLine'',''PurchaseAverageCost'',''Float'',''float''),
   (''PurchaseLine'',''PurchaseMinSale2'',''Decimal'',''numeric(18,2)''),
   (''PurchaseLine'',''PurchaseMinSale2ByOne'',''Decimal'',''numeric(18,2)''),
   (''PurchaseLine'',''PurchaseCostChange'',''Float'',''float''),
   (''PurchaseLine'',''PurchaseStockDifference'',''Decimal'',''numeric(18,2)''),
   (''PurchaseLine'',''PurchaseExpenses'',''Float'',''float''),
   (''PurchaseLine'',''PurchaseOriginalPrice'',''Float'',''float''),
   (''PurchaseLine'',''PurchaseForeignPrice'',''Float'',''float''),
   (''PurchaseLine'',''PurchaseExchangePrice'',''Float'',''float''),
   (''PurchaseLine'',''PurchaseCurrencyId'',''Integer'',''int''),
   (''PurchaseItem'',''PurchaseItemName'',''String'',''nvarchar(500)''),
   (''PurchaseItem'',''PurchaseStoreMode'',''Integer'',''int''),
   (''PurchaseItem'',''PurchaseItemCost'',''Float'',''float''),
   (''PurchaseItem'',''PurchaseLastLine'',''Integer'',''int''),
   (''PurchaseItem'',''PurchaseLastCargoCost'',''Decimal'',''numeric(18,3)''),
   (''PurchaseUnit'',''PriceItemId'',''Integer'',''int''),
   (''PurchaseUnit'',''PriceUnitId'',''Integer'',''int''),
   (''PurchaseUnit'',''UnitSalePrice'',''Decimal'',''numeric(18,3)''),
   (''PurchaseUnit'',''UnitMinimumPrice'',''Decimal'',''numeric(18,3)''),
   (''PurchaseUnit'',''UnitMinimumPrice2'',''Decimal'',''numeric(18,3)''),
   (''PurchaseExpiry'',''ExpiryLineId'',''Integer'',''int''),
   (''PurchaseExpiry'',''ExpiryItemId'',''Integer'',''int''),
   (''PurchaseExpiry'',''ExpiryDate'',''DateTime'',''datetime''),
   (''StockBucket'',''ItemId'',''Integer'',''int''),
   (''StockBucket'',''StoreId'',''Integer'',''bigint''),
   (''StockBucket'',''ExpiryText'',''String'',''nvarchar(50)''),
   (''StockBucket'',''StockQuantity'',''Float'',''float(53)'');
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.ValueOrigin<>CASE WHEN c.EntityType=''StockBucket'' THEN 3 ELSE 1 END OR NOT EXISTS(SELECT 1 FROM @paFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51131,''Invalid purchase field.'',1;
  IF EXISTS(SELECT c.EntityType,c.EntityId FROM @changes c GROUP BY c.EntityType,c.EntityId HAVING COUNT(*)<>(SELECT COUNT(*) FROM @paFields f WHERE f.E=c.EntityType)) THROW 51131,''Incomplete purchase row.'',1;
  IF @EventOrdinal=1 AND ((SELECT COUNT(*) FROM @changes)<>51 OR (SELECT COUNT(*) FROM @changes WHERE EntityType=''PurchaseDocument'')<>23 OR (SELECT COUNT(*) FROM @changes WHERE EntityType=''PurchaseLine'')<>28) THROW 51131,''Purchase requires header and added line.'',1;
  IF @EventOrdinal>1 AND EXISTS(SELECT 1 FROM @changes WHERE EntityType=''PurchaseDocument'') THROW 51131,''Repeated purchase header.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE (EntityType<>''StockBucket'' AND ISNULL(TRY_CONVERT(bigint,EntityId),0)<=0) OR (EntityType=''StockBucket'' AND (LEN(EntityId)<>64 OR EntityId LIKE ''%[^A-F0-9]%'' COLLATE Latin1_General_100_BIN2)) OR (EntityType=''PurchaseDocument'' AND EntityId<>@paOwner)) THROW 51131,''Invalid purchase row identity.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE AfterState NOT IN(2,3) OR
    ((EntityType=''PurchaseExpiry'' OR (EntityType=''PurchaseLine'' AND @EventOrdinal=1)) AND BeforeState<>1) OR
    (NOT(EntityType=''PurchaseExpiry'' OR (EntityType=''PurchaseLine'' AND @EventOrdinal=1)) AND BeforeState NOT IN(2,3))) THROW 51131,''Invalid purchase value states.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''PurchaseDocument'' AND FieldCode=''DocumentType'' AND (BeforeValue<>CONVERT(varchar(10),@paKind) OR AfterValue<>CONVERT(varchar(10),@paKind))) THROW 51131,''Purchase type mismatch.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''PurchaseDocument'' AND FieldCode=''DocumentNumber'' AND (AfterState<>3 OR AfterValue<>@EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)''))) THROW 51131,''Purchase number mismatch.'',1;
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.EntityType=''PurchaseLine'' AND (NOT EXISTS(SELECT 1 FROM dbo.Pch_Details p WITH(HOLDLOCK) WHERE p.T_ID=TRY_CONVERT(bigint,c.EntityId) AND p.Pch_T_ID=TRY_CONVERT(int,@paOwner)) OR (c.FieldCode=''PurchaseDocumentId'' AND (c.AfterState<>3 OR c.AfterValue<>@paOwner)))) THROW 51131,''Unrelated purchase line.'',1;
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.EntityType=''PurchaseExpiry'' AND NOT EXISTS(SELECT 1 FROM dbo.Valid_Notes n WITH(HOLDLOCK) JOIN dbo.Pch_Details p WITH(HOLDLOCK) ON p.T_ID=n.Pch_T_ID WHERE n.T_ID=TRY_CONVERT(int,c.EntityId) AND p.Pch_T_ID=TRY_CONVERT(int,@paOwner))) THROW 51131,''Unrelated expiry note.'',1;
  IF @EventOrdinal>1 AND NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Event WHERE SourceId=@SourceId AND RequestId=@RequestId AND EventOrdinal=1 AND EventCode=''Purchase.LineAdded'') THROW 51131,''Missing purchase root event.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Inventory.PurchaseAddition"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Inventory.PurchaseAddition"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
