/* SQL Server 2014. Additional audit catalogue only; original sales procedures stay unchanged. */
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
  AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Documents.EditOpening"]')=1
  AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.EditOpening"]')=1) THROW 51102,'Unexpected source or previous activation.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51102,'Resolve incomplete requests first.',1;
 IF OBJECT_ID('dbo.SB_IM_Unit_Change','P') IS NULL OR OBJECT_ID('dbo.IM_Conferm_NewPrice','P') IS NULL THROW 51102,'Sales procedures missing.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Sales.LineDetailsChanged',@sql)>0 OR CHARINDEX('Document.EditOpened',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected baseline or 011 already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''Sales.LineDetailsChanged'',''Sales.LinePriceChanged'') AND @DomainCode=''Sales.LineEditing'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Sales.LineDetailsChanged',@sql)>0 OR CHARINDEX('Document.EditOpened',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected writer catalogue.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''Sales.LineDetailsChanged'',''Sales.LinePriceChanged'') AND @domain=''Sales.LineEditing'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected field catalogue.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Sales.LineDetailsChanged'',''Sales.LinePriceChanged'') AND (
  (EntityType=''SalesLine'' AND (
   (FieldCode IN(''ItemId'',''UnitId'',''StoreId'',''LineApproved'',''MinimumPriceUsed'',''MinimumPrice2Used'') AND ValueType=''Integer'') OR
   (FieldCode IN(''LineQuantity'',''LinePrice'',''LineTotal'',''StoredQuantity'') AND ValueType=''Decimal'') OR
   (FieldCode IN(''LineCost'',''UnitCargo'',''LineDiscount'',''StockQuantity'') AND ValueType=''Float'') OR
   (FieldCode IN(''LineNotes'',''ExpiryText'') AND ValueType=''String'') OR (FieldCode=''LineDate'' AND ValueType=''DateTime''))) OR
  (EntityType=''SalesInvoice'' AND (
   (FieldCode IN(''DocumentTotal'',''DocumentPure'',''Discount'') AND ValueType=''Decimal'') OR
   (FieldCode IN(''Approved'',''Voided'',''AccountId'') AND ValueType=''Integer'') OR
   (FieldCode=''DocumentProfit'' AND ValueType=''Float'') OR (FieldCode=''NeedRepost'' AND ValueType=''Boolean''))))) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'IF @operation IN(''Sales.LineDetailsChanged'',''Sales.LinePriceChanged'')
 BEGIN
  DECLARE @saleId nvarchar(128)=(SELECT EntityId FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=''SalesInvoice''),
   @saleLine nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @saleScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'');
  IF @saleId IS NULL OR @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>''SalesLine''
   OR (@operation=''Sales.LineDetailsChanged'' AND @saleScreen<>''Change_IM_Details'')
   OR (@operation=''Sales.LinePriceChanged'' AND @saleScreen<>''SBChangePrice'')
   THROW 51101,''Sales line request identity or screen mismatch.'',1;
  IF (SELECT COUNT(*) FROM @changes)<>25 OR (SELECT COUNT(*) FROM @changes WHERE EntityType=''SalesLine'')<>17
   OR (SELECT COUNT(*) FROM @changes WHERE EntityType=''SalesInvoice'')<>8
   OR EXISTS(SELECT 1 FROM @changes WHERE ValueOrigin<>1 OR BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3)
       OR (EntityType=''SalesLine'' AND EntityId<>@saleLine) OR (EntityType=''SalesInvoice'' AND EntityId<>@saleId))
   THROW 51101,''Sales edit requires twenty-five actual fields.'',1;
  IF @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>1
   OR @EventXml.exist(''/Event/Entities/Entity[@role="Document" and @type="SalesInvoice" and @id=sql:variable("@saleId")]'')<>1
   THROW 51101,''Sales line document reference missing.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.LineEditing"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.LineEditing"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
