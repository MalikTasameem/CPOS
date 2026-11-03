/* SQL Server 2014. Audit catalogue only: AG_Balance_Void_Row and its business callees remain unchanged. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51118,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51118,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51118,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51118,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.LineLifecycle"]')=1) THROW 51118,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51118,'Incomplete audit requests.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Document.Voided',@sql)>0 OR CHARINDEX('Sales.LineAdded',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51118,'Unexpected begin baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode=''Document.Voided'' AND @DomainCode=''Documents.Cancellation'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Document.Voided',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51118,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation=''Document.Voided'' AND @domain=''Documents.Cancellation'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51118,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation=''Document.Voided'') OR '+@old); -- exact catalogue checked below
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51118,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditCancellation018: closed rows and types, actual values, no business correction.
 IF @operation=''Document.Voided''
 BEGIN
  DECLARE @cv TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @cv SELECT e.E,f.F,f.T,f.S FROM (VALUES(''BusinessDocument''),(''Receipt'')) e(E) CROSS JOIN (VALUES
   (''Voided'',''Integer'',''int''),(''NeedRepost'',''Boolean'',''bit''),(''NeedCancelReverse'',''Boolean'',''bit''),
   (''VoidAt'',''DateTime'',''datetime''),(''VoidBy'',''Integer'',''int''),(''VoidReason'',''String'',''nvarchar(500)''),
   (''JournalId'',''Integer'',''int''),(''DocumentTotal'',''Decimal'',''numeric(18,3)''),(''DocumentPure'',''Decimal'',''numeric(18,3)''),
   (''Discount'',''Decimal'',''numeric(18,3)''),(''Debit'',''Decimal'',''numeric(18,3)''),(''Credit'',''Decimal'',''numeric(18,3)''),
   (''Balance'',''Decimal'',''numeric(18,3)''),(''AccountId'',''Integer'',''int''),(''Approved'',''Integer'',''int'')) f(F,T,S);
  INSERT @cv VALUES
   (''BusinessDocument'',''DocumentType'',''Integer'',''int''),(''BusinessDocument'',''DocumentNumber'',''Integer'',''int''),(''BusinessDocument'',''DocumentDate'',''DateTime'',''datetime''),
   (''BusinessDocument'',''SalesOpen'',''Boolean'',''bit''),(''BusinessDocument'',''Paused'',''Boolean'',''bit''),(''BusinessDocument'',''OrderDelivered'',''Boolean'',''bit''),(''BusinessDocument'',''PaidState'',''Integer'',''int''),(''BusinessDocument'',''TableId'',''Integer'',''int''),
   (''Receipt'',''ReceiptNumber'',''Integer'',''int''),(''Receipt'',''ReceiptDocumentId'',''Integer'',''int''),(''Receipt'',''PaymentId'',''Integer'',''int''),(''Receipt'',''TreasuryId'',''Integer'',''int''),(''Receipt'',''DocumentType'',''Integer'',''int''),
   (''TreasuryMovement'',''TreasuryId'',''Integer'',''int''),(''TreasuryMovement'',''TreasuryDocumentId'',''Integer'',''int''),(''TreasuryMovement'',''TreasuryType'',''Integer'',''int''),(''TreasuryMovement'',''TreasuryVoided'',''Boolean'',''bit''),
   (''TreasuryMovement'',''DocumentPure'',''Decimal'',''numeric(18,3)''),(''TreasuryMovement'',''Debit'',''Decimal'',''numeric(18,3)''),(''TreasuryMovement'',''Credit'',''Decimal'',''numeric(18,3)''),(''TreasuryMovement'',''Balance'',''Decimal'',''numeric(18,3)''),
   (''DiningTable'',''TableBusy'',''Boolean'',''bit''),(''DiningTable'',''TableOrderCode'',''String'',''nvarchar(100)''),(''CancellationPrintQueue'',''PrintQueueRows'',''Integer'',''bigint''),
   (''StockBucket'',''ItemId'',''Integer'',''int''),(''StockBucket'',''StoreId'',''Integer'',''bigint''),(''StockBucket'',''ExpiryText'',''String'',''nvarchar(50)''),(''StockBucket'',''StockQuantity'',''Float'',''float(53)''),
   (''EXP_Details'',''ExpenseId'',''Integer'',''bigint''),(''EXP_Details'',''ExpenseQuantity'',''Decimal'',''numeric(18,3)''),(''EXP_Details'',''ExpensePrice'',''Decimal'',''numeric(18,3)''),(''EXP_Details'',''ExpenseTotal'',''Decimal'',''numeric(18,3)''),(''EXP_Details'',''LineApprovedBit'',''Boolean'',''bit'');
  INSERT @cv SELECT e.E,f.F,f.T,f.S FROM (VALUES(''SB_Contents''),(''Pch_Details''),(''IMEX_Details''),(''Rtn_Contents''),(''FRM_Details''),(''FRM_Contents_Details''),(''InSale_Details''),(''OutSale_Details'')) e(E)
   CROSS JOIN (VALUES(''ItemId'',''Integer'',''int''),(''ExpiryText'',''String'',''nvarchar(50)'')) f(F,T,S);
  INSERT @cv SELECT e.E,f.F,f.T,f.S FROM (VALUES(''SB_Contents''),(''Pch_Details''),(''IMEX_Details''),(''Rtn_Contents''),(''FRM_Details''),(''FRM_Contents_Details''),(''InSale_Details''),(''OutSale_Details'')) e(E)
   CROSS APPLY (VALUES(''StoreId'',''Integer'',CASE WHEN E=''OutSale_Details'' THEN ''int'' ELSE ''bigint'' END),
    (''StoredQuantity'',''Decimal'',CASE WHEN E=''FRM_Contents_Details'' THEN ''numeric(18,3)'' ELSE ''numeric(18,2)'' END),
    (CASE WHEN E IN(''SB_Contents'',''Rtn_Contents'',''FRM_Contents_Details'') THEN ''LineApproved'' ELSE ''LineApprovedBit'' END,
     CASE WHEN E IN(''SB_Contents'',''Rtn_Contents'',''FRM_Contents_Details'') THEN ''Integer'' ELSE ''Boolean'' END,
     CASE WHEN E IN(''SB_Contents'',''Rtn_Contents'',''FRM_Contents_Details'') THEN ''int'' ELSE ''bit'' END)) f(F,T,S);
  INSERT @cv SELECT ''SalesStructure'',f.F,f.T,f.S FROM (VALUES
   (''StructureLineId'',''Integer'',''int''),(''StructureDate'',''DateTime'',''datetime''),(''StructureItemId'',''Integer'',''int''),(''StructureUnitId'',''Integer'',''int''),(''StructureStoreId'',''Integer'',''int''),
   (''StructureQuantity'',''Decimal'',''numeric(18,3)''),(''StructureCargo'',''Decimal'',''numeric(18,2)''),(''StructureStoredQuantity'',''Decimal'',''numeric(18,2)''),(''StructureApproved'',''Integer'',''int''),(''StructureCost'',''Decimal'',''numeric(18,3)'')) f(F,T,S);
  IF EXISTS(SELECT 1 FROM @changes c WHERE NOT EXISTS(SELECT 1 FROM @cv v WHERE v.E=c.EntityType AND v.F=c.FieldCode AND v.T=c.ValueType AND v.S=c.SourceSqlType)
   OR ValueOrigin<>1 OR BeforeState NOT IN(2,3) OR AfterState NOT IN(1,2,3) OR (EntityType<>''SalesStructure'' AND AfterState=1)) THROW 51118,''Invalid cancellation field or state.'',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>(SELECT COUNT(*) FROM @cv v WHERE v.E=EntityType)) THROW 51118,''Incomplete cancellation row.'',1;
  DECLARE @voidId nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),@voidType int,
   @voidScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'');
  IF @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>''BusinessDocument'' OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0
   OR NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=''BusinessDocument'' AND EntityId=@voidId) THROW 51118,''Invalid cancellation identity.'',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes)<>23 OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>''BusinessDocument'' OR EntityId<>@voidId) THROW 51118,''Cancellation header required first.'',1;
   IF NOT EXISTS(SELECT 1 FROM @changes WHERE FieldCode=''Voided'' AND BeforeState=3 AND AfterState=3 AND BeforeValue=''0'' AND AfterValue=''1'') THROW 51118,''Actual cancellation transition required.'',1;
   SELECT @voidType=CONVERT(int,AfterValue) FROM @changes WHERE FieldCode=''DocumentType'' AND BeforeState=3 AND AfterState=3 AND BeforeValue=AfterValue;
  END
  ELSE
  BEGIN
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''BusinessDocument'') THROW 51118,''Unexpected second header.'',1;
   SELECT @voidType=CONVERT(int,c.AfterValue) FROM dbo.Network_Audit_Event e JOIN dbo.Network_Audit_Change c ON c.EventId=e.EventId
    WHERE e.SourceId=@SourceId AND e.RequestId=@RequestId AND e.EventOrdinal=1 AND e.EventCode=@operation AND e.EntityId=@voidId AND c.FieldCode=''DocumentType'';
  END;
  IF @voidType IS NULL OR NOT ((@voidType=1 AND @voidScreen IN(''Sales'',''Sales_Fast'',''POS'',''OrdersMenu'',''POS_Abcent_Bills'',''ShowBill_IM''))
   OR (@voidType=2 AND @voidScreen=''EXP_Details'') OR (@voidType=7 AND @voidScreen=''Pch'') OR (@voidType=8 AND @voidScreen=''IM_Execute'') OR (@voidType=9 AND @voidScreen=''Invoice'')
   OR (@voidType IN(10,11) AND @voidScreen=''Returns'') OR (@voidType=13 AND @voidScreen=''Format_Items_Auto'') OR (@voidType=16 AND @voidScreen=''ViewBill'')
   OR (@voidType=17 AND @voidScreen=''Inside_Sales'') OR (@voidType=18 AND @voidScreen=''Format_Items_Manual'') OR (@voidType=35 AND @voidScreen IN(''Outside_Sales'',''Outside_Sales_Touch''))) THROW 51118,''Cancellation screen/type mismatch.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType NOT IN(''BusinessDocument'',''Receipt'',''TreasuryMovement'',''StockBucket'',''DiningTable'') AND NOT
   ((@voidType=1 AND EntityType IN(''SB_Contents'',''SalesStructure'',''CancellationPrintQueue'')) OR (@voidType=2 AND EntityType=''EXP_Details'')
    OR (@voidType IN(7,9) AND EntityType=''Pch_Details'') OR (@voidType=8 AND EntityType=''IMEX_Details'') OR (@voidType IN(10,11) AND EntityType=''Rtn_Contents'')
    OR (@voidType IN(13,18) AND EntityType IN(''FRM_Details'',''FRM_Contents_Details'')) OR (@voidType=17 AND EntityType=''InSale_Details'') OR (@voidType=35 AND EntityType=''OutSale_Details''))) THROW 51118,''Cancellation row belongs to another document type.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Documents.Cancellation"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Documents.Cancellation"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
