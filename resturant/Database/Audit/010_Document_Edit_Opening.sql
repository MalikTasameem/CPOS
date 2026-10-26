/* SQL Server 2014. Opening shared document headers only; no business schema changes. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51102,'Approved test database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51102,'Cannot verify closed sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51103,'Deployment is busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51102,'Close all other database sessions.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SchemaVersion=1
  AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Security"]')=1
  AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.EditOpening"]')=1) THROW 51102,'Unexpected source or previous activation.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51102,'Resolve incomplete requests first.',1;
 IF OBJECT_ID('dbo.Agents_Balance_MV_OpenForEdit','P') IS NULL THROW 51102,'Original document procedure missing.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Document.EditOpened',@sql)>0 OR CHARINDEX('Sales.EditOpened',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected baseline or 010 already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode=''Document.EditOpened'' AND @DomainCode=''Documents.EditOpening'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Document.EditOpened',@sql)>0 OR CHARINDEX('Sales.EditOpened',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected event catalogue.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation=''Document.EditOpened'' AND @domain=''Documents.EditOpening'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected field catalogue.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation=''Document.EditOpened'' AND (
  (FieldCode IN(''DocumentType'',''DocumentNumber'',''Approved'',''Voided'',''JournalId'',''OriginalJournalId'',''EditOpenedBy'') AND ValueType=''Integer'') OR
  (FieldCode=''NeedRepost'' AND ValueType=''Boolean'') OR
  (FieldCode=''EditOpenedAt'' AND ValueType=''DateTime'') OR
  (FieldCode IN(''DocumentTotal'',''DocumentPure'',''Discount'') AND ValueType=''Decimal''))) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'IF @operation=''Document.EditOpened''
 BEGIN
  IF (SELECT COUNT(*) FROM @changes)<>12 OR EXISTS(SELECT 1 FROM @changes WHERE ValueOrigin<>1 OR BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3))
   THROW 51101,''Document opening requires twelve actual before/after fields.'',1;
  IF (SELECT COUNT(*) FROM @changes WHERE FieldCode=''DocumentType'' AND BeforeState=3 AND AfterState=3 AND BeforeValue=AfterValue
      AND AfterValue IN(''2'',''7'',''8'',''9'',''10'',''11'',''12'',''13'',''17'',''18'',''35''))<>1
   THROW 51101,''Invalid document type.'',1;
  DECLARE @docType int=(SELECT CONVERT(int,AfterValue) FROM @changes WHERE FieldCode=''DocumentType'');
  DECLARE @docEntity varchar(64)=CASE @docType
   WHEN 2 THEN ''GeneralExpense'' WHEN 7 THEN ''PurchaseInvoice'' WHEN 8 THEN ''WasteDocument'' WHEN 9 THEN ''StockCount''
   WHEN 10 THEN ''SalesReturn'' WHEN 11 THEN ''PurchaseReturn'' WHEN 12 THEN ''StockTransfer'' WHEN 13 THEN ''AutoManufacturing''
   WHEN 17 THEN ''StockIssue'' WHEN 18 THEN ''ManualManufacturing'' WHEN 35 THEN ''StockReceipt'' END;
  DECLARE @docId nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @docScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'');
  IF @docEntity IS NULL OR @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>@docEntity
   OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>@docEntity OR EntityId<>@docId)
   OR NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@docEntity AND EntityId=@docId)
   THROW 51101,''Document identity mismatch.'',1;
  IF NOT ((@docType=2 AND @docScreen=''EXP_Details'') OR (@docType=7 AND @docScreen=''Pch'') OR (@docType=8 AND @docScreen=''IM_Execute'')
   OR (@docType=9 AND @docScreen=''Invoice'') OR (@docType IN(10,11) AND @docScreen=''Returns'')
   OR (@docType=12 AND @docScreen=''Stores_ImmediateOrder'') OR (@docType=13 AND @docScreen=''Format_Items_Auto'')
   OR (@docType=17 AND @docScreen=''Inside_Sales'') OR (@docType=18 AND @docScreen=''Format_Items_Manual'')
   OR (@docType=35 AND @docScreen IN(''Outside_Sales'',''Outside_Sales_Touch'')))
   THROW 51101,''Document screen mismatch.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Documents.EditOpening"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Documents.EditOpening"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
