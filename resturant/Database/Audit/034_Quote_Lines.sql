/* SQL Server 2014. Quote line lifecycle only. Requires revised 032 and approved 033. CODE ONLY. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51134,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51134,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51134,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51134,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Documents.DirectFields"]')=1) THROW 51134,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51134,'Incomplete audit requests.',1;
 IF CHARINDEX('QuoteDelete033:',ISNULL(OBJECT_DEFINITION(OBJECT_ID('dbo.ViewBill_Contents_Delete_IM')),N''))=0 THROW 51134,'Apply approved quote delete correction 033 first.',1;
 IF EXISTS(SELECT 1 FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('Agents_Balance_MV'),OBJECT_ID('ViewBill_Contents'))) THROW 51134,'Review business triggers before deployment.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Quote.LineAdded',@sql)>0 OR CHARINDEX('Document.FastDiscountChanged',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51134,'Unexpected begin baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''Quote.LineAdded'',''Quote.LineDeleted'',''Quote.LineChanged'',''Quote.LineQuantityStepped'') AND @DomainCode=''Sales.QuoteLines'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Quote.LineAdded',@sql)>0 OR CHARINDEX(@old,@sql)=0 OR CHARINDEX('Document.FastDiscountChanged',@sql)=0 THROW 51134,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''Quote.LineAdded'',''Quote.LineDeleted'',''Quote.LineChanged'',''Quote.LineQuantityStepped'') AND @domain=''Sales.QuoteLines'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51134,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Quote.LineAdded'',''Quote.LineDeleted'',''Quote.LineChanged'',''Quote.LineQuantityStepped'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51134,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditQuoteLines034: quote-only row mutations, after correction 033.
 IF @operation IN(''Quote.LineAdded'',''Quote.LineDeleted'',''Quote.LineChanged'',''Quote.LineQuantityStepped'')
 BEGIN
  DECLARE @qOwner nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @qScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)''),@qNumber int,@qLine nvarchar(128);
  IF @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>''QuoteDocument'' OR ISNULL(TRY_CONVERT(int,@qOwner),0)<=0 OR @EventOrdinal<>1
   OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0 OR @EventXml.value(''count(/Event/Document)'',''int'')<>1
   THROW 51134,''Invalid quote event identity.'',1;
  SELECT @qNumber=ViewSB_ID FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@qOwner) AND BsType_ID=16 AND isVoid=0;
  IF @qNumber IS NULL OR @EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)'')<>CONVERT(nvarchar(50),@qNumber) THROW 51134,''Quote number mismatch.'',1;
  IF NOT((@operation=''Quote.LineAdded'' AND @qScreen=''ViewBill_IM_card'') OR (@operation=''Quote.LineChanged'' AND @qScreen=''Change_IM_Details'') OR (@operation IN(''Quote.LineDeleted'',''Quote.LineQuantityStepped'') AND @qScreen=''ViewBill'')) THROW 51134,''Invalid quote screen.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=''QuoteDocument'' AND EntityId=@qOwner) THROW 51134,''Quote request mismatch.'',1;
  DECLARE @qFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @qFields VALUES
   (''QuoteDocument'',''Voided'',''Integer'',''int''),
   (''QuoteDocument'',''NeedRepost'',''Boolean'',''bit''),
   (''QuoteDocument'',''NeedCancelReverse'',''Boolean'',''bit''),
   (''QuoteDocument'',''VoidAt'',''DateTime'',''datetime''),
   (''QuoteDocument'',''VoidBy'',''Integer'',''int''),
   (''QuoteDocument'',''VoidReason'',''String'',''nvarchar(500)''),
   (''QuoteDocument'',''JournalId'',''Integer'',''int''),
   (''QuoteDocument'',''DocumentTotal'',''Decimal'',''numeric(18,3)''),
   (''QuoteDocument'',''DocumentPure'',''Decimal'',''numeric(18,3)''),
   (''QuoteDocument'',''Discount'',''Decimal'',''numeric(18,3)''),
   (''QuoteDocument'',''Debit'',''Decimal'',''numeric(18,3)''),
   (''QuoteDocument'',''Credit'',''Decimal'',''numeric(18,3)''),
   (''QuoteDocument'',''Balance'',''Decimal'',''numeric(18,3)''),
   (''QuoteDocument'',''AccountId'',''Integer'',''int''),
   (''QuoteDocument'',''Approved'',''Integer'',''int''),
   (''QuoteDocument'',''DocumentType'',''Integer'',''int''),
   (''QuoteDocument'',''DocumentNumber'',''Integer'',''int''),
   (''QuoteDocument'',''DocumentDate'',''DateTime'',''datetime''),
   (''QuoteDocument'',''SalesOpen'',''Boolean'',''bit''),
   (''QuoteDocument'',''Paused'',''Boolean'',''bit''),
   (''QuoteDocument'',''OrderDelivered'',''Boolean'',''bit''),
   (''QuoteDocument'',''PaidState'',''Integer'',''int''),
   (''QuoteDocument'',''TableId'',''Integer'',''int''),
   (''QuoteDocument'',''AccountName'',''String'',''nvarchar(500)''),
   (''QuoteDocument'',''SecondaryDebit'',''Decimal'',''numeric(18,3)''),
   (''QuoteDocument'',''EditOpenedAt'',''DateTime'',''datetime''),
   (''QuoteDocument'',''EditOpenedBy'',''Integer'',''int''),
   (''QuoteLine'',''QuoteOwnerId'',''Integer'',''int''),
   (''QuoteLine'',''QuoteItemId'',''Integer'',''int''),
   (''QuoteLine'',''QuoteUnitId'',''Integer'',''bigint''),
   (''QuoteLine'',''QuoteStoreId'',''Integer'',''bigint''),
   (''QuoteLine'',''QuoteLineDate'',''DateTime'',''datetime''),
   (''QuoteLine'',''QuoteComponents'',''String'',''nvarchar(max)''),
   (''QuoteLine'',''QuoteCost'',''Decimal'',''numeric(18,3)''),
   (''QuoteLine'',''QuotePrice'',''Decimal'',''numeric(18,3)''),
   (''QuoteLine'',''QuoteExpiry'',''String'',''nvarchar(50)''),
   (''QuoteLine'',''QuoteQuantity'',''Float'',''float(53)''),
   (''QuoteLine'',''QuoteLineTotal'',''Decimal'',''numeric(18,3)''),
   (''QuoteLine'',''QuoteCargo'',''Float'',''float(53)''),
   (''QuoteLine'',''QuoteStockQuantity'',''Decimal'',''numeric(18,2)''),
   (''QuoteLine'',''QuoteApproved'',''Integer'',''int''),
   (''QuoteLine'',''QuoteParentLine'',''Integer'',''int''),
   (''QuoteLine'',''QuoteSalespersonPercent'',''Decimal'',''numeric(18,2)''),
   (''QuoteLine'',''QuoteNotes'',''String'',''nvarchar(max)''),
   (''QuoteLine'',''QuoteBarcode'',''String'',''nvarchar(50)''),
   (''QuoteLine'',''QuoteItemName'',''String'',''nvarchar(500)'');
  IF (SELECT COUNT(*) FROM @changes)<>46 OR EXISTS(SELECT 1 FROM @changes c WHERE c.ValueOrigin<>1 OR NOT EXISTS(SELECT 1 FROM @qFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType))
   THROW 51134,''Invalid or incomplete quote fields.'',1;
  IF EXISTS(SELECT EntityType,FieldCode FROM @changes GROUP BY EntityType,FieldCode HAVING COUNT(*)<>1) THROW 51134,''Duplicate quote field.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''QuoteDocument'' AND (EntityId<>@qOwner OR BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3))) THROW 51134,''Invalid quote header state.'',1;
  IF (SELECT COUNT(DISTINCT EntityId) FROM @changes WHERE EntityType=''QuoteLine'')<>1 THROW 51134,''Expected one quote row.'',1;
  SELECT TOP(1) @qLine=EntityId FROM @changes WHERE EntityType=''QuoteLine'';
  IF ISNULL(TRY_CONVERT(int,@qLine),0)<=0 THROW 51134,''Invalid quote row id.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''QuoteLine'' AND
    ((@operation=''Quote.LineAdded'' AND (BeforeState<>1 OR AfterState NOT IN(2,3)))
     OR (@operation=''Quote.LineDeleted'' AND (BeforeState NOT IN(2,3) OR AfterState<>1))
     OR (@operation IN(''Quote.LineChanged'',''Quote.LineQuantityStepped'') AND (BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3))))) THROW 51134,''Invalid quote row state.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE FieldCode=''QuoteOwnerId'' AND CASE WHEN @operation=''Quote.LineDeleted'' THEN BeforeValue ELSE AfterValue END<>@qOwner)
   OR EXISTS(SELECT 1 FROM @changes WHERE FieldCode=''DocumentType'' AND (AfterState<>3 OR AfterValue<>''16''))
   OR EXISTS(SELECT 1 FROM @changes WHERE FieldCode=''DocumentNumber'' AND (AfterState<>3 OR AfterValue<>CONVERT(nvarchar(50),@qNumber))) THROW 51134,''Quote owner/type mismatch.'',1;
  IF @operation=''Quote.LineDeleted''
  BEGIN
   IF EXISTS(SELECT 1 FROM dbo.ViewBill_Contents WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@qLine)) THROW 51134,''Deleted quote row still exists.'',1;
   IF CHARINDEX(''QuoteDelete033:'',ISNULL(OBJECT_DEFINITION(OBJECT_ID(''dbo.ViewBill_Contents_Delete_IM'')),N''''))=0 THROW 51134,''Quote delete correction missing.'',1;
  END
  ELSE IF NOT EXISTS(SELECT 1 FROM dbo.ViewBill_Contents WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@qLine) AND Bill_T_ID=TRY_CONVERT(int,@qOwner)) THROW 51134,''Saved quote row missing.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''QuoteDocument'' AND NOT(@operation IN(''Quote.LineAdded'',''Quote.LineDeleted'') AND FieldCode IN(''DocumentTotal'',''DocumentPure''))
   AND (BeforeState<>AfterState OR ISNULL(BeforeValue,N'''') COLLATE Latin1_General_100_BIN2<>ISNULL(AfterValue,N'''') COLLATE Latin1_General_100_BIN2)) THROW 51134,''Unexpected quote header effect.'',1;
  IF @operation IN(''Quote.LineChanged'',''Quote.LineQuantityStepped'') AND EXISTS(SELECT 1 FROM @changes WHERE EntityType=''QuoteLine'' AND NOT
   ((@operation=''Quote.LineChanged'' AND FieldCode IN(''QuoteUnitId'',''QuoteQuantity'',''QuotePrice'',''QuoteLineTotal'',''QuoteCargo'',''QuoteStockQuantity'')) OR (@operation=''Quote.LineQuantityStepped'' AND FieldCode IN(''QuoteQuantity'',''QuoteLineTotal'',''QuoteStockQuantity'')))
   AND (BeforeState<>AfterState OR ISNULL(BeforeValue,N'''') COLLATE Latin1_General_100_BIN2<>ISNULL(AfterValue,N'''') COLLATE Latin1_General_100_BIN2)) THROW 51134,''Unexpected quote row effect.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.QuoteLines"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.QuoteLines"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;

