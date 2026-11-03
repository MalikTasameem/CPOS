/* SQL Server 2014. Direct header account/discount changes only. Requires 031. No business schema/procedure changes. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51132,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51132,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51132,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51132,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Inventory.PurchaseAddition"]')=1) THROW 51132,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51132,'Incomplete audit requests.',1;
 IF EXISTS(SELECT 1 FROM sys.triggers WHERE is_disabled=0 AND parent_id=OBJECT_ID('Agents_Balance_MV')) THROW 51132,'Review business triggers before deployment.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Document.AccountChanged',@sql)>0 OR CHARINDEX('Purchase.LineAdded',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51132,'Unexpected begin baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''Document.AccountChanged'',''Document.DiscountChanged'',''Document.FastDiscountChanged'') AND @DomainCode=''Documents.DirectFields'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Document.AccountChanged',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51132,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''Document.AccountChanged'',''Document.DiscountChanged'',''Document.FastDiscountChanged'') AND @domain=''Documents.DirectFields'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51132,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Document.AccountChanged'',''Document.DiscountChanged'',''Document.FastDiscountChanged'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51132,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditDocumentFields032: closed direct account / discount edits only.
 IF @operation IN(''Document.AccountChanged'',''Document.DiscountChanged'',''Document.FastDiscountChanged'')
 BEGIN
  DECLARE @dfOwner nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @dfEntity varchar(64)=@EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)''),
   @dfScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)''),
   @dfKind int,@dfNumber int,@dfAccount int,@dfDiscount numeric(18,3),@dfPercent numeric(18,3);
  SELECT @dfKind=BsType_ID,@dfAccount=AG_ID,@dfDiscount=Discount,@dfPercent=Debit_2,
   @dfNumber=CASE BsType_ID WHEN 1 THEN SB_ID WHEN 7 THEN Pch_ID WHEN 16 THEN ViewSB_ID END
  FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@dfOwner) AND isVoid=0;
  IF @dfEntity<>''DocumentHeader'' OR ISNULL(TRY_CONVERT(int,@dfOwner),0)<=0 OR ISNULL(@dfKind,0) NOT IN(1,7,16)
   OR @EventOrdinal<>1 OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0
   OR @EventXml.value(''count(/Event/Document)'',''int'')<>1 OR @dfNumber IS NULL
   OR @EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)'')<>CONVERT(nvarchar(50),@dfNumber)
   THROW 51132,''Invalid direct header identity.'',1;
  IF NOT((@operation=''Document.AccountChanged'' AND ((@dfKind=1 AND @dfScreen IN(''Sales'',''AgentsMenu'')) OR (@dfKind=16 AND @dfScreen=''ViewBill'')))
   OR (@operation=''Document.DiscountChanged'' AND ((@dfKind=1 AND @dfScreen IN(''Sales'',''Percent_Disc'',''SBChangePrice'')) OR (@dfKind=7 AND @dfScreen IN(''Pch'',''Percent_Disc''))))
   OR (@operation=''Document.FastDiscountChanged'' AND @dfKind=1 AND @dfScreen=''Fast_SB_Discount''))
   THROW 51132,''Invalid direct header screen.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@dfEntity AND EntityId=@dfOwner)
   THROW 51132,''Direct header request mismatch.'',1;
  DECLARE @dfFields TABLE(F varchar(64),T varchar(20),S varchar(40));
  INSERT @dfFields VALUES
   (''Voided'',''Integer'',''int''),
   (''NeedRepost'',''Boolean'',''bit''),
   (''NeedCancelReverse'',''Boolean'',''bit''),
   (''VoidAt'',''DateTime'',''datetime''),
   (''VoidBy'',''Integer'',''int''),
   (''VoidReason'',''String'',''nvarchar(500)''),
   (''JournalId'',''Integer'',''int''),
   (''DocumentTotal'',''Decimal'',''numeric(18,3)''),
   (''DocumentPure'',''Decimal'',''numeric(18,3)''),
   (''Discount'',''Decimal'',''numeric(18,3)''),
   (''Debit'',''Decimal'',''numeric(18,3)''),
   (''Credit'',''Decimal'',''numeric(18,3)''),
   (''Balance'',''Decimal'',''numeric(18,3)''),
   (''AccountId'',''Integer'',''int''),
   (''Approved'',''Integer'',''int''),
   (''DocumentType'',''Integer'',''int''),
   (''DocumentNumber'',''Integer'',''int''),
   (''DocumentDate'',''DateTime'',''datetime''),
   (''SalesOpen'',''Boolean'',''bit''),
   (''Paused'',''Boolean'',''bit''),
   (''OrderDelivered'',''Boolean'',''bit''),
   (''PaidState'',''Integer'',''int''),
   (''TableId'',''Integer'',''int''),
   (''AccountName'',''String'',''nvarchar(500)''),
   (''SecondaryDebit'',''Decimal'',''numeric(18,3)''),
   (''EditOpenedAt'',''DateTime'',''datetime''),
   (''EditOpenedBy'',''Integer'',''int'');
  IF (SELECT COUNT(*) FROM @changes)<>27 OR EXISTS(SELECT 1 FROM @changes c WHERE c.EntityType<>@dfEntity OR c.EntityId<>@dfOwner OR c.ValueOrigin<>1 OR c.BeforeState NOT IN(2,3) OR c.AfterState NOT IN(2,3)
    OR NOT EXISTS(SELECT 1 FROM @dfFields f WHERE f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType))
   THROW 51132,''Invalid or incomplete direct header fields.'',1;
  IF EXISTS(SELECT FieldCode FROM @changes GROUP BY FieldCode HAVING COUNT(*)<>1)
   THROW 51132,''Duplicate direct header field.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE NOT((@operation=''Document.AccountChanged'' AND FieldCode IN(''AccountId'',''AccountName'')) OR (@operation=''Document.DiscountChanged'' AND FieldCode=''Discount'') OR (@operation=''Document.FastDiscountChanged'' AND FieldCode IN(''Discount'',''SecondaryDebit'')))
   AND (BeforeState<>AfterState OR ISNULL(BeforeValue,N'''') COLLATE Latin1_General_100_BIN2<>ISNULL(AfterValue,N'''') COLLATE Latin1_General_100_BIN2))
   THROW 51132,''Unexpected direct header side effect.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE FieldCode=''DocumentType'' AND (AfterState<>3 OR AfterValue<>CONVERT(nvarchar(20),@dfKind)))
   OR EXISTS(SELECT 1 FROM @changes WHERE FieldCode=''DocumentNumber'' AND (AfterState<>3 OR AfterValue<>CONVERT(nvarchar(20),@dfNumber)))
   OR EXISTS(SELECT 1 FROM @changes WHERE FieldCode=''AccountId'' AND (AfterState<>3 OR AfterValue<>CONVERT(nvarchar(20),@dfAccount)))
   OR EXISTS(SELECT 1 FROM @changes WHERE FieldCode=''Discount'' AND ((@dfDiscount IS NULL AND AfterState<>2) OR (@dfDiscount IS NOT NULL AND (AfterState<>3 OR TRY_CONVERT(numeric(18,3),AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),AfterValue)<>@dfDiscount))))
   THROW 51132,''Stored direct header result mismatch.'',1;
  IF @operation=''Document.FastDiscountChanged'' AND EXISTS(SELECT 1 FROM @changes WHERE FieldCode=''SecondaryDebit'' AND ((@dfPercent IS NULL AND AfterState<>2) OR (@dfPercent IS NOT NULL AND (AfterState<>3 OR TRY_CONVERT(numeric(18,3),AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),AfterValue)<>@dfPercent))))
   THROW 51132,''Stored fast discount percentage mismatch.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Documents.DirectFields"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Documents.DirectFields"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
