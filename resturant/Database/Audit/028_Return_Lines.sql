/* SQL Server 2014. Return lines only; original add/delete procedures and table schemas unchanged. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51128,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51128,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51128,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51128,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="People.EmployeeValues"]')=1) THROW 51128,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51128,'Incomplete audit requests.',1;
 IF EXISTS(SELECT 1 FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('Rtn_Contents'),OBJECT_ID('Agents_Balance_MV'))) THROW 51128,'Review business triggers before deployment.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Return.LineAdded',@sql)>0 OR CHARINDEX('EmployeeValue.Created',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51128,'Unexpected begin baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''Return.LineAdded'',''Return.LineDeleted'',''Return.LineQuantityChanged'') AND @DomainCode=''Sales.ReturnLines'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Return.LineAdded',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51128,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''Return.LineAdded'',''Return.LineDeleted'',''Return.LineQuantityChanged'') AND @domain=''Sales.ReturnLines'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51128,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Return.LineAdded'',''Return.LineDeleted'',''Return.LineQuantityChanged'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51128,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditReturnLines028: complete affected line, recalculated header and derived return.
 IF @operation IN(''Return.LineAdded'',''Return.LineDeleted'',''Return.LineQuantityChanged'')
 BEGIN
  DECLARE @rlOwner nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @rlEntity varchar(64)=@EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)''),
   @rlScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)''),
   @rlType int,@rlLine varchar(64),@rlCount int;
  SELECT @rlType=BsType_ID FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@rlOwner) AND isVoid=0;
  SET @rlLine=CASE WHEN @rlType IN(10,11) THEN ''ReturnLine'' END;
  SET @rlCount=23;
  IF @rlLine IS NULL OR @rlEntity<>''ReturnDocument'' OR ISNULL(TRY_CONVERT(int,@rlOwner),0)<=0
   OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0
   OR @EventXml.value(''count(/Event/Document)'',''int'')<>1 THROW 51128,''Invalid return document identity.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@rlEntity AND EntityId=@rlOwner) THROW 51128,''Return request mismatch.'',1;
  IF @rlScreen<>''Returns'' THROW 51128,''Invalid return screen.'',1;
  DECLARE @rlFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @rlFields VALUES
   (''ReturnDocument'',''Voided'',''Integer'',''int''),
   (''ReturnDocument'',''NeedRepost'',''Boolean'',''bit''),
   (''ReturnDocument'',''NeedCancelReverse'',''Boolean'',''bit''),
   (''ReturnDocument'',''VoidAt'',''DateTime'',''datetime''),
   (''ReturnDocument'',''VoidBy'',''Integer'',''int''),
   (''ReturnDocument'',''VoidReason'',''String'',''nvarchar(500)''),
   (''ReturnDocument'',''JournalId'',''Integer'',''int''),
   (''ReturnDocument'',''DocumentTotal'',''Decimal'',''numeric(18,3)''),
   (''ReturnDocument'',''DocumentPure'',''Decimal'',''numeric(18,3)''),
   (''ReturnDocument'',''Discount'',''Decimal'',''numeric(18,3)''),
   (''ReturnDocument'',''Debit'',''Decimal'',''numeric(18,3)''),
   (''ReturnDocument'',''Credit'',''Decimal'',''numeric(18,3)''),
   (''ReturnDocument'',''Balance'',''Decimal'',''numeric(18,3)''),
   (''ReturnDocument'',''AccountId'',''Integer'',''int''),
   (''ReturnDocument'',''Approved'',''Integer'',''int''),
   (''ReturnDocument'',''DocumentType'',''Integer'',''int''),
   (''ReturnDocument'',''DocumentNumber'',''Integer'',''int''),
   (''ReturnDocument'',''DocumentDate'',''DateTime'',''datetime''),
   (''ReturnDocument'',''SalesOpen'',''Boolean'',''bit''),
   (''ReturnDocument'',''Paused'',''Boolean'',''bit''),
   (''ReturnDocument'',''OrderDelivered'',''Boolean'',''bit''),
   (''ReturnDocument'',''PaidState'',''Integer'',''int''),
   (''ReturnDocument'',''TableId'',''Integer'',''int''),
   (''ReturnLine'',''RLDate'',''DateTime'',''datetime''),
   (''ReturnLine'',''RLDocumentId'',''Integer'',''int''),
   (''ReturnLine'',''RLReturnType'',''Integer'',''int''),
   (''ReturnLine'',''RLItemId'',''Integer'',''int''),
   (''ReturnLine'',''RLUnitId'',''Integer'',''bigint''),
   (''ReturnLine'',''RLStoreId'',''Integer'',''bigint''),
   (''ReturnLine'',''RLComponents'',''String'',''nvarchar(max)''),
   (''ReturnLine'',''RLCost'',''Decimal'',''numeric(18,2)''),
   (''ReturnLine'',''RLPrice'',''Decimal'',''numeric(18,2)''),
   (''ReturnLine'',''RLExpiry'',''String'',''nvarchar(50)''),
   (''ReturnLine'',''RLQuantity'',''Float'',''float(53)''),
   (''ReturnLine'',''RLTotal'',''Decimal'',''numeric(18,2)''),
   (''ReturnLine'',''RLCargo'',''Float'',''float(53)''),
   (''ReturnLine'',''RLStoredQuantity'',''Decimal'',''numeric(18,2)''),
   (''ReturnLine'',''RLApproved'',''Integer'',''int''),
   (''ReturnLine'',''RLSellerPercent'',''Decimal'',''numeric(18,2)''),
   (''ReturnLine'',''RLOriginalBillId'',''Integer'',''int''),
   (''ReturnLine'',''RLProjectId'',''Integer'',''int''),
   (''ReturnLine'',''RLMarketerValue'',''Decimal'',''numeric(18,3)''),
   (''ReturnLine'',''RLBarcode'',''String'',''nvarchar(max)''),
   (''ReturnLine'',''RLSerial'',''String'',''nvarchar(100)''),
   (''ReturnLine'',''RLMinPrice'',''Integer'',''int''),
   (''ReturnLine'',''RLMinPrice2'',''Integer'',''int''),
   (''StockBucket'',''ItemId'',''Integer'',''int''),
   (''StockBucket'',''StoreId'',''Integer'',''bigint''),
   (''StockBucket'',''ExpiryText'',''String'',''nvarchar(50)''),
   (''StockBucket'',''StockQuantity'',''Float'',''float(53)'');
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.EntityType NOT IN(''ReturnDocument'',''StockBucket'',@rlLine) OR c.ValueOrigin<>CASE WHEN c.EntityType=''StockBucket'' THEN 3 ELSE 1 END OR NOT EXISTS(SELECT 1 FROM @rlFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51128,''Invalid return field.'',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>(SELECT COUNT(*) FROM @rlFields f WHERE f.E=EntityType)) THROW 51128,''Incomplete return row.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE (EntityType<>''StockBucket'' AND ISNULL(TRY_CONVERT(int,EntityId),0)<=0) OR (EntityType=''ReturnDocument'' AND EntityId<>@rlOwner) OR (EntityType=''StockBucket'' AND (LEN(EntityId)<>64 OR EntityId COLLATE Latin1_General_BIN2 LIKE ''%[^A-F0-9]%''))) THROW 51128,''Invalid return row identity.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE (EntityType IN(''ReturnDocument'',''StockBucket'') AND (BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3))) OR (EntityType=@rlLine AND (
   (@operation=''Return.LineAdded'' AND (BeforeState<>1 OR AfterState NOT IN(2,3))) OR
   (@operation=''Return.LineDeleted'' AND (AfterState<>1 OR BeforeState NOT IN(2,3))) OR
   (@operation=''Return.LineQuantityChanged'' AND (BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3)))))) THROW 51128,''Invalid return value states.'',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes WHERE EntityType=''ReturnDocument'')<>23 OR (SELECT COUNT(*) FROM @changes WHERE EntityType=@rlLine)<>@rlCount OR EXISTS(SELECT 1 FROM @changes WHERE EntityType=''StockBucket'') THROW 51128,''First return event must contain header and line.'',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''ReturnDocument'' AND FieldCode=''DocumentType'' AND (BeforeValue<>CONVERT(nvarchar(20),@rlType) OR AfterValue<>CONVERT(nvarchar(20),@rlType))) THROW 51128,''Return type mismatch.'',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=@rlLine AND FieldCode=''RLDocumentId'' AND CASE WHEN @operation=''Return.LineDeleted'' THEN BeforeValue ELSE AfterValue END<>@rlOwner) THROW 51128,''Unrelated return line.'',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''ReturnDocument'' AND FieldCode=''DocumentNumber'' AND (AfterState<>3 OR AfterValue<>@EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)''))) THROW 51128,''Return number mismatch.'',1;
  END
  ELSE IF EXISTS(SELECT 1 FROM @changes WHERE EntityType<>''StockBucket'') THROW 51128,''Additional return events contain balances only.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.ReturnLines"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.ReturnLines"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
