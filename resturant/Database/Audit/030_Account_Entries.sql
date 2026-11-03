/* SQL Server 2014. Manual account entries only; original procedures and table schemas unchanged. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51130,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51130,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51130,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51130,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Expenses.Lines"]')=1) THROW 51130,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51130,'Incomplete audit requests.',1;
 IF EXISTS(SELECT 1 FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('Agents_Balance_MV'),OBJECT_ID('Agents_Balance_MV_RCT'),OBJECT_ID('Treasury_Balance_MV'))) THROW 51130,'Review business triggers before deployment.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('AccountEntry.Created',@sql)>0 OR CHARINDEX('Expense.LineAdded',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51130,'Unexpected begin baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''AccountEntry.Created'') AND @DomainCode=''Finance.AccountEntries'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('AccountEntry.Created',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51130,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''AccountEntry.Created'') AND @domain=''Finance.AccountEntries'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51130,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''AccountEntry.Created'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51130,'Unexpected writer body.',1;
 -- Extend only the cancellation screen/type catalogue; cancellation business SQL stays unchanged.
 SET @old=N'OR (@voidType=2 AND @voidScreen=''EXP_Details'')';
 IF CHARINDEX(@old,@sql)=0 THROW 51130,'Unexpected cancellation catalogue.',1;
 SET @sql=REPLACE(@sql,@old,N'OR (@voidType IN(5,6,37) AND @voidScreen=''Add_Prev_Balance'') '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 SET @sql=REPLACE(@sql,@old,N'-- AuditAccountEntry030: stored manual balance and salary entries.
 IF @operation=''AccountEntry.Created''
 BEGIN
  DECLARE @aeId nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @aeScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'');
  IF @aeScreen<>''Add_Prev_Balance'' OR @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>''AccountEntry'' OR ISNULL(TRY_CONVERT(int,@aeId),0)<=0
   OR @EventOrdinal<>1 OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0 OR @EventXml.value(''count(/Event/Document)'',''int'')<>1
   OR @EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)'')<>@aeId THROW 51130,''Invalid account entry identity.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=''AccountEntry'' AND EntityId=''New'') THROW 51130,''Account entry request mismatch.'',1;
  DECLARE @aeFields TABLE(F varchar(64),T varchar(20),S varchar(40));
  INSERT @aeFields VALUES
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
   (''EntryTitle'',''String'',''nvarchar(550)''),(''EntryUserId'',''Integer'',''int'');
  IF (SELECT COUNT(*) FROM @changes)<>25 OR EXISTS(SELECT 1 FROM @changes c WHERE EntityType<>''AccountEntry'' OR EntityId<>@aeId OR ValueOrigin<>1 OR BeforeState<>1 OR AfterState NOT IN(2,3) OR NOT EXISTS(SELECT 1 FROM @aeFields f WHERE f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51130,''Incomplete account entry fields.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@aeId) AND BsType_ID IN(5,6,37) AND isVoid=0 AND isDepended=1) THROW 51130,''Missing account entry.'',1;
  IF NOT EXISTS(SELECT 1 FROM @changes WHERE FieldCode=''DocumentType'' AND AfterState=3 AND AfterValue IN(''5'',''6'',''37'')) THROW 51130,''Invalid account entry type.'',1;
  IF NOT EXISTS(SELECT 1 FROM @changes WHERE FieldCode=''DocumentNumber'' AND AfterState=3 AND AfterValue=@aeId) THROW 51130,''Account entry number mismatch.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Finance.AccountEntries"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Finance.AccountEntries"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
