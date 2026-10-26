/* SQL Server 2014. Full sales-line deletion graph. No business procedure changes. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51102,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51102,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51103,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51102,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.LineQuantity"]')=1) THROW 51102,'Unexpected source or prerequisites.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51102,'Incomplete requests.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Sales.LineDeleted',@sql)>0 OR CHARINDEX('Sales.LineQuantityStepped',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode=''Sales.LineDeleted'' AND @DomainCode=''Sales.LineLifecycle'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Sales.LineDeleted',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation=''Sales.LineDeleted'' AND @domain=''Sales.LineLifecycle'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected field catalogue.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation=''Sales.LineDeleted'' AND ((EntityType=''SalesLine'' AND ((FieldCode IN(''ItemId'',''UnitId'',''StoreId'',''LineApproved'',''MinimumPriceUsed'',''MinimumPrice2Used'',''LineDocumentId'',''ParentLineId'') AND ValueType=''Integer'') OR (FieldCode IN(''LineQuantity'',''LinePrice'',''LineTotal'',''StoredQuantity'',''SalespersonPercent'',''MarketerValue'') AND ValueType=''Decimal'') OR (FieldCode IN(''LineCost'',''UnitCargo'',''LineDiscount'') AND ValueType=''Float'') OR (FieldCode IN(''LineNotes'',''ExpiryText'',''LineComponentsText'',''LineBarcode'',''LineSerial'') AND ValueType=''String'') OR (FieldCode IN(''LineDate'') AND ValueType=''DateTime'') OR (FieldCode IN(''LineChecked'') AND ValueType=''Boolean''))) OR
 (EntityType=''SalesInvoice'' AND ((FieldCode IN(''DocumentTotal'',''DocumentPure'',''Discount'') AND ValueType=''Decimal'') OR (FieldCode IN(''Approved'',''Voided'',''AccountId'') AND ValueType=''Integer'') OR (FieldCode IN(''DocumentProfit'') AND ValueType=''Float'') OR (FieldCode IN(''NeedRepost'') AND ValueType=''Boolean''))) OR
 (EntityType=''SalesStructure'' AND ((FieldCode IN(''StructureLineId'',''StructureItemId'',''StructureUnitId'',''StructureStoreId'',''StructureApproved'') AND ValueType=''Integer'') OR (FieldCode IN(''StructureQuantity'',''StructureCargo'',''StructureStoredQuantity'',''StructureCost'') AND ValueType=''Decimal'') OR (FieldCode IN(''StructureDate'') AND ValueType=''DateTime''))) OR
 (EntityType=''SalesComponent'' AND ((FieldCode IN(''ComponentLineId'',''ComponentId'',''ComponentQuantity'') AND ValueType=''Integer'') OR (FieldCode IN(''ComponentPrice'') AND ValueType=''Decimal''))) OR
 (EntityType=''SalesReservation'' AND ((FieldCode IN(''ReservationLineId'') AND ValueType=''Integer'') OR (FieldCode IN(''ReservationDateTime'') AND ValueType=''DateTime''))) OR
 (EntityType=''SalesTimedReservation'' AND ((FieldCode IN(''ReservationLineId'',''ReservationItemId'') AND ValueType=''Integer'') OR (FieldCode IN(''ReservationDate'') AND ValueType=''Date'') OR (FieldCode IN(''ReservationTime'',''ReservationNotes'') AND ValueType=''String''))) OR
 (EntityType=''SalesCommission'' AND ((FieldCode IN(''CommissionAccountId'') AND ValueType=''Integer'') OR (FieldCode IN(''CommissionValue'') AND ValueType=''Decimal''))) OR
 (EntityType=''StockBucket'' AND ((FieldCode IN(''ItemId'',''StoreId'') AND ValueType=''Integer'') OR (FieldCode IN(''ExpiryText'') AND ValueType=''String'') OR (FieldCode IN(''StockQuantity'') AND ValueType=''Float''))))) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected event body.',1;
 SET @sql=REPLACE(@sql,@old,N'IF @operation=''Sales.LineDeleted''
 BEGIN
  DECLARE @deletedBill nvarchar(128)=(SELECT EntityId FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=''SalesInvoice''),
   @deletedLine nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)'');
  IF @deletedBill IS NULL OR @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>''SalesLine''
   OR @EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'') NOT IN(''Sales'',''Sales_Fast'',''POS'') THROW 51101,''Invalid delete identity or screen.'',1;
  IF @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>1 OR @EventXml.exist(''/Event/Entities/Entity[@role="Document" and @type="SalesInvoice" and @id=sql:variable("@deletedBill")]'')<>1 THROW 51101,''Missing invoice relation.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE ValueOrigin<>1 OR BeforeState<2 OR AfterState=0 OR (EntityType=''SalesLine'' AND AfterState<>1) OR (EntityType IN(''SalesInvoice'',''StockBucket'') AND AfterState<2)) THROW 51101,''Actual delete values required.'',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes)<>32 OR (SELECT COUNT(*) FROM @changes WHERE EntityType=''SalesLine'' AND EntityId=@deletedLine)<>24 OR (SELECT COUNT(*) FROM @changes WHERE EntityType=''SalesInvoice'' AND EntityId=@deletedBill)<>8 THROW 51101,''Complete deleted line and header required.'',1;
  END
  ELSE
  BEGIN
   IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Event WHERE SourceId=@SourceId AND RequestId=@RequestId AND EventOrdinal=1 AND EventCode=@operation AND EntityType=''SalesLine'' AND EntityId=@deletedLine) THROW 51101,''Delete detail must follow its main event.'',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType NOT IN(''SalesStructure'',''SalesComponent'',''SalesReservation'',''SalesTimedReservation'',''SalesCommission'',''StockBucket''))
    OR EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE EntityType WHEN ''SalesStructure'' THEN 10 WHEN ''SalesComponent'' THEN 4 WHEN ''SalesReservation'' THEN 2 WHEN ''SalesTimedReservation'' THEN 5 WHEN ''SalesCommission'' THEN 2 WHEN ''StockBucket'' THEN 4 ELSE -1 END) THROW 51101,''Delete detail rows incomplete.'',1;
  END
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.LineLifecycle"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.LineLifecycle"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;

