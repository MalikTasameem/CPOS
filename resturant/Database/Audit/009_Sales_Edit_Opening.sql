/* SQL Server 2014. Narrow pilot extension: sales edit OPENING only.
   No business table/column changes, no old history migration or deletion. */
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
  AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Security"]')=1) THROW 51102,'Unexpected source or Security is not enabled.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51102,'Resolve incomplete requests first.',1;
 IF OBJECT_ID('dbo.ST_Settlement_Line_AssertWritable','P') IS NULL OR OBJECT_ID('dbo.Agents_Balance_MV_OpenForEdit','P') IS NULL THROW 51102,'Install previous prerequisites first.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Sales.EditOpened',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected baseline or 009 already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode=''Sales.EditOpened'' AND @DomainCode=''Sales.EditOpening'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Sales.EditOpened',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected event catalogue.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation=''Sales.EditOpened'' AND @domain=''Sales.EditOpening'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected field catalogue.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation=''Sales.EditOpened'' AND (
  (FieldCode IN(''Approved'',''Voided'',''JournalId'',''OriginalJournalId'',''EditOpenedBy'') AND ValueType=''Integer'') OR
  (FieldCode=''NeedRepost'' AND ValueType=''Boolean'') OR
  (FieldCode=''EditOpenedAt'' AND ValueType=''DateTime'') OR
  (FieldCode IN(''DocumentTotal'',''DocumentPure'',''Discount'') AND ValueType=''Decimal''))) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'IF @operation=''Sales.EditOpened'' AND (EXISTS(SELECT 1 FROM @changes WHERE EntityType<>''SalesInvoice'' OR ValueOrigin<>1 OR BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3)) OR (SELECT COUNT(*) FROM @changes)<>10)
   THROW 51101,''Sales edit opening requires ten actual before/after header fields.'',1;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.EditOpening"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.EditOpening"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
