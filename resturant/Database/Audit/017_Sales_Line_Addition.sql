/* SQL Server 2014. Addition/merge contract only; no business formulas or table changes. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51117,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51117,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51117,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51117,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.LineLifecycle"]')=1) THROW 51117,'Unexpected source or lifecycle disabled.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51117,'Incomplete audit requests.',1;
 IF ISNULL(CHARINDEX('AuditComponents015',OBJECT_DEFINITION(OBJECT_ID('dbo.SB_Contents_INSERT'))),0)=0
 OR ISNULL(CHARINDEX('AuditComponents015',OBJECT_DEFINITION(OBJECT_ID('dbo.SB_Contents_INSERT_2'))),0)=0
 OR ISNULL(CHARINDEX('AuditTarget013',OBJECT_DEFINITION(OBJECT_ID('dbo.AG_Balance_DependingUpdatedBill'))),0)=0
 OR ISNULL(CHARINDEX('AuditDetailComponents016',OBJECT_DEFINITION(OBJECT_ID('dbo.SB_IM_Unit_Change'))),0)=0 THROW 51117,'Install approved prerequisites first.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Sales.LineAdded',@sql)>0 OR CHARINDEX('Sales.LineDeleted',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51117,'Unexpected begin baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode=''Sales.LineAdded'' AND @DomainCode=''Sales.LineLifecycle'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Sales.LineAdded',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51117,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation=''Sales.LineAdded'' AND @domain=''Sales.LineLifecycle'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''Sales.LineDeleted'' AND ((EntityType';
 IF CHARINDEX(@old,@sql)=0 THROW 51117,'Unexpected lifecycle field catalogue.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Sales.LineDeleted'',''Sales.LineAdded'') AND ((EntityType');
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51117,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditAddition017: actual full rows, new or merged, plus component/stock pages.
 IF @operation=''Sales.LineAdded''
 BEGIN
  DECLARE @addedBill nvarchar(128)=(SELECT EntityId FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=''SalesInvoice''),
   @addedLine nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)'');
  IF @addedBill IS NULL OR @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>''SalesLine''
   OR @EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'') NOT IN(''POS'',''Sales_Fast'',''SB_IM_card'') THROW 51117,''Invalid addition identity or screen.'',1;
  IF @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>1 OR @EventXml.exist(''/Event/Entities/Entity[@role="Document" and @type="SalesInvoice" and @id=sql:variable("@addedBill")]'')<>1 THROW 51117,''Missing invoice relation.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE ValueOrigin<>1 OR BeforeState=0 OR AfterState=0 OR (BeforeState=1 AND AfterState=1)
   OR EntityType NOT IN(''SalesLine'',''SalesInvoice'',''SalesStructure'',''StockBucket'')
   OR (EntityType<>''SalesStructure'' AND AfterState<2) OR (EntityType IN(''SalesInvoice'',''StockBucket'') AND BeforeState<2)) THROW 51117,''Actual addition values required.'',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes)<>32 OR (SELECT COUNT(*) FROM @changes WHERE EntityType=''SalesLine'' AND EntityId=@addedLine)<>24 OR (SELECT COUNT(*) FROM @changes WHERE EntityType=''SalesInvoice'' AND EntityId=@addedBill)<>8 THROW 51117,''Complete added line and header required.'',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''SalesLine'' AND BeforeState=1) AND EXISTS(SELECT 1 FROM @changes WHERE EntityType=''SalesLine'' AND BeforeState<>1) THROW 51117,''Inconsistent new/merged line state.'',1;
  END
  ELSE
  BEGIN
   IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Event WHERE SourceId=@SourceId AND RequestId=@RequestId AND EventOrdinal=1 AND EventCode=@operation AND EntityType=''SalesLine'' AND EntityId=@addedLine) THROW 51117,''Addition detail must follow its main event.'',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType NOT IN(''SalesStructure'',''StockBucket''))
    OR EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE EntityType WHEN ''SalesStructure'' THEN 10 WHEN ''StockBucket'' THEN 4 ELSE -1 END) THROW 51117,''Addition detail rows incomplete.'',1;
  END
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
