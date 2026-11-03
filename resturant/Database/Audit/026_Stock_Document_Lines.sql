/* SQL Server 2014. Reviewed receipt/issue/waste/transfer lines; business SQL unchanged. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51126,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51126,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51126,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51126,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Finance.Transfers"]')=1) THROW 51126,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51126,'Incomplete audit requests.',1;
 IF EXISTS(SELECT 1 FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('Outsale_Details'),OBJECT_ID('InSale_Details'),OBJECT_ID('IMEX_Details'),OBJECT_ID('Store_Trans_Details'),OBJECT_ID('Agents_Balance_MV'))) THROW 51126,'Review business triggers before deployment.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('StockDocument.LineAdded',@sql)>0 OR CHARINDEX('TreasuryMovement.Created',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51126,'Unexpected begin baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''StockDocument.LineAdded'',''StockDocument.LineDeleted'',''StockDocument.LineQuantityChanged'') AND @DomainCode=''Inventory.DocumentLines'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('StockDocument.LineAdded',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51126,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''StockDocument.LineAdded'',''StockDocument.LineDeleted'',''StockDocument.LineQuantityChanged'') AND @domain=''Inventory.DocumentLines'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51126,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''StockDocument.LineAdded'',''StockDocument.LineDeleted'',''StockDocument.LineQuantityChanged'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51126,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditStockLines026: complete affected line, recalculated header and derived stock.
 IF @operation IN(''StockDocument.LineAdded'',''StockDocument.LineDeleted'',''StockDocument.LineQuantityChanged'')
 BEGIN
  DECLARE @slOwner nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @slEntity varchar(64)=@EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)''),
   @slScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)''),
   @slType int,@slLine varchar(64),@slCount int;
  SELECT @slType=BsType_ID FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@slOwner) AND isVoid=0;
  SET @slLine=CASE @slType WHEN 35 THEN ''StockReceiptLine'' WHEN 17 THEN ''StockIssueLine'' WHEN 8 THEN ''StockDamageLine'' WHEN 12 THEN ''StockTransferLine'' END;
  SET @slCount=CASE @slType WHEN 8 THEN 13 WHEN 12 THEN 15 ELSE 14 END;
  IF @slLine IS NULL OR @slEntity<>''StockDocument'' OR ISNULL(TRY_CONVERT(int,@slOwner),0)<=0
   OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0
   OR @EventXml.value(''count(/Event/Document)'',''int'')<>1 THROW 51126,''Invalid stock document identity.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@slEntity AND EntityId=@slOwner) THROW 51126,''Stock request mismatch.'',1;
  IF NOT (
    (@slType=35 AND ((@operation=''StockDocument.LineAdded'' AND @slScreen IN(''Outside_Sales_IM_card'',''Outside_Sales_Touch'')) OR (@operation=''StockDocument.LineDeleted'' AND @slScreen IN(''Outside_Sales'',''Outside_Sales_Touch'')) OR (@operation=''StockDocument.LineQuantityChanged'' AND @slScreen=''Outside_Sales_Touch'')))
    OR (@slType=17 AND ((@operation=''StockDocument.LineAdded'' AND @slScreen=''Inside_Sales_IM_card'') OR (@operation=''StockDocument.LineDeleted'' AND @slScreen=''Inside_Sales'')))
    OR (@slType=8 AND ((@operation=''StockDocument.LineAdded'' AND @slScreen=''IMEX_IM_card'') OR (@operation=''StockDocument.LineDeleted'' AND @slScreen=''IM_Execute'')))
    OR (@slType=12 AND ((@operation=''StockDocument.LineAdded'' AND @slScreen=''Stores_Trans_IM_card'') OR (@operation=''StockDocument.LineDeleted'' AND @slScreen=''Stores_ImmediateOrder'')))
  ) THROW 51126,''Invalid stock screen.'',1;
  DECLARE @slFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @slFields VALUES
   (''StockDocument'',''Voided'',''Integer'',''int''),
   (''StockDocument'',''NeedRepost'',''Boolean'',''bit''),
   (''StockDocument'',''NeedCancelReverse'',''Boolean'',''bit''),
   (''StockDocument'',''VoidAt'',''DateTime'',''datetime''),
   (''StockDocument'',''VoidBy'',''Integer'',''int''),
   (''StockDocument'',''VoidReason'',''String'',''nvarchar(500)''),
   (''StockDocument'',''JournalId'',''Integer'',''int''),
   (''StockDocument'',''DocumentTotal'',''Decimal'',''numeric(18,3)''),
   (''StockDocument'',''DocumentPure'',''Decimal'',''numeric(18,3)''),
   (''StockDocument'',''Discount'',''Decimal'',''numeric(18,3)''),
   (''StockDocument'',''Debit'',''Decimal'',''numeric(18,3)''),
   (''StockDocument'',''Credit'',''Decimal'',''numeric(18,3)''),
   (''StockDocument'',''Balance'',''Decimal'',''numeric(18,3)''),
   (''StockDocument'',''AccountId'',''Integer'',''int''),
   (''StockDocument'',''Approved'',''Integer'',''int''),
   (''StockDocument'',''DocumentType'',''Integer'',''int''),
   (''StockDocument'',''DocumentNumber'',''Integer'',''int''),
   (''StockDocument'',''DocumentDate'',''DateTime'',''datetime''),
   (''StockDocument'',''SalesOpen'',''Boolean'',''bit''),
   (''StockDocument'',''Paused'',''Boolean'',''bit''),
   (''StockDocument'',''OrderDelivered'',''Boolean'',''bit''),
   (''StockDocument'',''PaidState'',''Integer'',''int''),
   (''StockDocument'',''TableId'',''Integer'',''int''),
   (''StockReceiptLine'',''SLDocumentId'',''Integer'',''int''),
   (''StockReceiptLine'',''SLDate'',''DateTime'',''datetime''),
   (''StockReceiptLine'',''SLItemId'',''Integer'',''int''),
   (''StockReceiptLine'',''SLUnitId'',''Integer'',''int''),
   (''StockReceiptLine'',''SLCost'',''Decimal'',''numeric(18,2)''),
   (''StockReceiptLine'',''SLExpiry'',''String'',''nvarchar(50)''),
   (''StockReceiptLine'',''SLQuantity'',''Decimal'',''numeric(18,2)''),
   (''StockReceiptLine'',''SLTotal'',''Decimal'',''numeric(18,2)''),
   (''StockReceiptLine'',''SLCargo'',''Float'',''float(53)''),
   (''StockReceiptLine'',''SLStoredQuantity'',''Decimal'',''numeric(18,2)''),
   (''StockReceiptLine'',''SLApproved'',''Boolean'',''bit''),
   (''StockReceiptLine'',''SLBarcode'',''String'',''nvarchar(max)''),
   (''StockReceiptLine'',''SLStoreId'',''Integer'',''int''),
   (''StockReceiptLine'',''SLSalePrice'',''Decimal'',''numeric(18,3)''),
   (''StockIssueLine'',''SLDocumentId'',''Integer'',''int''),
   (''StockIssueLine'',''SLDate'',''DateTime'',''datetime''),
   (''StockIssueLine'',''SLItemId'',''Integer'',''int''),
   (''StockIssueLine'',''SLUnitId'',''Integer'',''bigint''),
   (''StockIssueLine'',''SLCost'',''Decimal'',''numeric(18,2)''),
   (''StockIssueLine'',''SLExpiry'',''String'',''nvarchar(50)''),
   (''StockIssueLine'',''SLQuantity'',''Decimal'',''numeric(18,2)''),
   (''StockIssueLine'',''SLTotal'',''Decimal'',''numeric(18,2)''),
   (''StockIssueLine'',''SLCargo'',''Float'',''float(53)''),
   (''StockIssueLine'',''SLStoredQuantity'',''Decimal'',''numeric(18,2)''),
   (''StockIssueLine'',''SLApproved'',''Boolean'',''bit''),
   (''StockIssueLine'',''SLBarcode'',''String'',''nvarchar(max)''),
   (''StockIssueLine'',''SLStoreId'',''Integer'',''bigint''),
   (''StockIssueLine'',''SLSalePrice'',''Decimal'',''numeric(18,3)''),
   (''StockDamageLine'',''SLDocumentId'',''Integer'',''int''),
   (''StockDamageLine'',''SLDate'',''DateTime'',''datetime''),
   (''StockDamageLine'',''SLItemId'',''Integer'',''int''),
   (''StockDamageLine'',''SLUnitId'',''Integer'',''bigint''),
   (''StockDamageLine'',''SLCost'',''Decimal'',''numeric(18,2)''),
   (''StockDamageLine'',''SLExpiry'',''String'',''nvarchar(50)''),
   (''StockDamageLine'',''SLQuantity'',''Decimal'',''numeric(18,2)''),
   (''StockDamageLine'',''SLTotal'',''Decimal'',''numeric(18,2)''),
   (''StockDamageLine'',''SLCargo'',''Float'',''float(53)''),
   (''StockDamageLine'',''SLStoredQuantity'',''Decimal'',''numeric(18,2)''),
   (''StockDamageLine'',''SLApproved'',''Boolean'',''bit''),
   (''StockDamageLine'',''SLBarcode'',''String'',''nvarchar(50)''),
   (''StockDamageLine'',''SLStoreId'',''Integer'',''bigint''),
   (''StockTransferLine'',''SLDocumentId'',''Integer'',''int''),
   (''StockTransferLine'',''SLDate'',''DateTime'',''datetime''),
   (''StockTransferLine'',''SLItemId'',''Integer'',''int''),
   (''StockTransferLine'',''SLUnitId'',''Integer'',''int''),
   (''StockTransferLine'',''SLCost'',''Decimal'',''numeric(18,2)''),
   (''StockTransferLine'',''SLExpiry'',''String'',''nvarchar(50)''),
   (''StockTransferLine'',''SLQuantity'',''Decimal'',''numeric(18,2)''),
   (''StockTransferLine'',''SLTotal'',''Decimal'',''numeric(18,2)''),
   (''StockTransferLine'',''SLCargo'',''Float'',''float(53)''),
   (''StockTransferLine'',''SLStoredQuantity'',''Decimal'',''numeric(18,2)''),
   (''StockTransferLine'',''SLApprovedInt'',''Integer'',''int''),
   (''StockTransferLine'',''SLBarcode'',''String'',''nvarchar(50)''),
   (''StockTransferLine'',''SLFromStoreId'',''Integer'',''bigint''),
   (''StockTransferLine'',''SLToStoreId'',''Integer'',''bigint''),
   (''StockTransferLine'',''SLSalePrice'',''Decimal'',''numeric(18,2)''),
   (''StockBucket'',''ItemId'',''Integer'',''int''),
   (''StockBucket'',''StoreId'',''Integer'',''bigint''),
   (''StockBucket'',''ExpiryText'',''String'',''nvarchar(50)''),
   (''StockBucket'',''StockQuantity'',''Float'',''float(53)'');
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.EntityType NOT IN(''StockDocument'',''StockBucket'',@slLine) OR c.ValueOrigin<>CASE WHEN c.EntityType=''StockBucket'' THEN 3 ELSE 1 END OR NOT EXISTS(SELECT 1 FROM @slFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51126,''Invalid stock field.'',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>(SELECT COUNT(*) FROM @slFields f WHERE f.E=EntityType)) THROW 51126,''Incomplete stock row.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE (EntityType<>''StockBucket'' AND ISNULL(TRY_CONVERT(int,EntityId),0)<=0) OR (EntityType=''StockDocument'' AND EntityId<>@slOwner) OR (EntityType=''StockBucket'' AND (LEN(EntityId)<>64 OR EntityId COLLATE Latin1_General_BIN2 LIKE ''%[^A-F0-9]%''))) THROW 51126,''Invalid stock row identity.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE (EntityType IN(''StockDocument'',''StockBucket'') AND (BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3))) OR (EntityType=@slLine AND (
   (@operation=''StockDocument.LineAdded'' AND (BeforeState<>1 OR AfterState NOT IN(2,3))) OR
   (@operation=''StockDocument.LineDeleted'' AND (AfterState<>1 OR BeforeState NOT IN(2,3))) OR
   (@operation=''StockDocument.LineQuantityChanged'' AND (BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3)))))) THROW 51126,''Invalid stock value states.'',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes WHERE EntityType=''StockDocument'')<>23 OR (SELECT COUNT(*) FROM @changes WHERE EntityType=@slLine)<>@slCount OR EXISTS(SELECT 1 FROM @changes WHERE EntityType=''StockBucket'') THROW 51126,''First stock event must contain header and line.'',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''StockDocument'' AND FieldCode=''DocumentType'' AND (BeforeValue<>CONVERT(nvarchar(20),@slType) OR AfterValue<>CONVERT(nvarchar(20),@slType))) THROW 51126,''Stock type mismatch.'',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=@slLine AND FieldCode=''SLDocumentId'' AND CASE WHEN @operation=''StockDocument.LineDeleted'' THEN BeforeValue ELSE AfterValue END<>@slOwner) THROW 51126,''Unrelated stock line.'',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''StockDocument'' AND FieldCode=''DocumentNumber'' AND (AfterState<>3 OR AfterValue<>@EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)''))) THROW 51126,''Stock number mismatch.'',1;
  END
  ELSE IF EXISTS(SELECT 1 FROM @changes WHERE EntityType<>''StockBucket'') THROW 51126,''Additional stock events contain balances only.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Inventory.DocumentLines"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Inventory.DocumentLines"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
