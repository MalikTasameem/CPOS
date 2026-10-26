/* SQL Server 2014. Quantity +/- audit only; business procedures remain unchanged. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51102,'Approved test database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51102,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51103,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51102,'Close other database sessions.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SchemaVersion=1
  AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.LineEditing"]')=1)
  THROW 51102,'Unexpected source or previous activation.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51102,'Resolve incomplete requests first.',1;
 IF (SELECT COUNT(*) FROM sys.procedures WHERE schema_id=SCHEMA_ID('dbo') AND name IN('SB_Contents_Change_IM_Qty','SB_Contents_Structers_Recount_One','SB_Contents_Structers_Remove_ONE','SB_Contents_Structers_insert_ONE'))<>4
  THROW 51102,'Quantity or component procedures missing.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Sales.LineQuantityStepped',@sql)>0 OR CHARINDEX('Sales.LineDetailsChanged',@sql)=0 OR CHARINDEX(@old,@sql)=0
  THROW 51102,'Unexpected baseline or 012 already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode=''Sales.LineQuantityStepped'' AND @DomainCode=''Sales.LineQuantity'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Sales.LineQuantityStepped',@sql)>0 OR CHARINDEX('Sales.LineDetailsChanged',@sql)=0 OR CHARINDEX(@old,@sql)=0
  THROW 51102,'Unexpected writer catalogue.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation=''Sales.LineQuantityStepped'' AND @domain=''Sales.LineQuantity'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected field catalogue.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation=''Sales.LineQuantityStepped'' AND ((EntityType=''SalesLine'' AND ((FieldCode IN(''ItemId'',''UnitId'',''StoreId'',''LineApproved'',''MinimumPriceUsed'',''MinimumPrice2Used'') AND ValueType=''Integer'') OR (FieldCode IN(''LineQuantity'',''LinePrice'',''LineTotal'',''StoredQuantity'') AND ValueType=''Decimal'') OR (FieldCode IN(''LineCost'',''UnitCargo'',''LineDiscount'',''StockQuantity'') AND ValueType=''Float'') OR (FieldCode IN(''LineNotes'',''ExpiryText'') AND ValueType=''String'') OR (FieldCode IN(''LineDate'') AND ValueType=''DateTime''))) OR
 (EntityType=''SalesInvoice'' AND ((FieldCode IN(''DocumentTotal'',''DocumentPure'',''Discount'') AND ValueType=''Decimal'') OR (FieldCode IN(''Approved'',''Voided'',''AccountId'') AND ValueType=''Integer'') OR (FieldCode IN(''DocumentProfit'') AND ValueType=''Float'') OR (FieldCode IN(''NeedRepost'') AND ValueType=''Boolean''))) OR
 (EntityType=''SalesStructure'' AND ((FieldCode IN(''StructureLineId'',''StructureItemId'',''StructureUnitId'',''StructureStoreId'',''StructureApproved'') AND ValueType=''Integer'') OR (FieldCode IN(''StructureQuantity'',''StructureCargo'',''StructureStoredQuantity'',''StructureCost'') AND ValueType=''Decimal'') OR (FieldCode IN(''StructureDate'') AND ValueType=''DateTime''))) OR
 (EntityType=''StockBucket'' AND ((FieldCode IN(''ItemId'',''StoreId'') AND ValueType=''Integer'') OR (FieldCode IN(''ExpiryText'') AND ValueType=''String'') OR (FieldCode IN(''StockQuantity'') AND ValueType=''Float''))))) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'IF @operation=''Sales.LineQuantityStepped''
 BEGIN
  DECLARE @stepBill nvarchar(128)=(SELECT EntityId FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=''SalesInvoice''),
   @stepLine nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)'');
  IF @stepBill IS NULL OR @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>''SalesLine''
   OR @EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'') NOT IN(''Sales'',''Sales_Fast'',''POS'')
   THROW 51101,''Invalid quantity step identity or screen.'',1;
  IF @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>1 OR
   @EventXml.exist(''/Event/Entities/Entity[@role="Document" and @type="SalesInvoice" and @id=sql:variable("@stepBill")]'')<>1
   THROW 51101,''Quantity step invoice reference missing.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE ValueOrigin<>1 OR BeforeState=0 OR AfterState=0 OR (BeforeState=1 AND AfterState=1)
   OR (EntityType<>''SalesStructure'' AND (BeforeState<2 OR AfterState<2)))
   THROW 51101,''Actual captured quantity values required.'',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes)<>25 OR (SELECT COUNT(*) FROM @changes WHERE EntityType=''SalesLine'' AND EntityId=@stepLine)<>17
    OR (SELECT COUNT(*) FROM @changes WHERE EntityType=''SalesInvoice'' AND EntityId=@stepBill)<>8
    THROW 51101,''Quantity first event requires complete line and header.'',1;
  END
  ELSE
  BEGIN
   IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Event WHERE SourceId=@SourceId AND RequestId=@RequestId AND EventOrdinal=1 AND EntityType=''SalesLine'' AND EntityId=@stepLine AND EventCode=@operation)
    THROW 51101,''Quantity detail must follow its main event.'',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType NOT IN(''SalesStructure'',''StockBucket''))
    OR EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId
     HAVING COUNT(*)<>CASE WHEN EntityType=''SalesStructure'' THEN 10 ELSE 4 END)
    THROW 51101,''Quantity detail rows must be complete.'',1;
  END
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.LineQuantity"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.LineQuantity"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
