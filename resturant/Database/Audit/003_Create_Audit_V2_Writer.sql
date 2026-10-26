/* Phase 2B. Approved test DB only. SQL Server 2014.
Creates procedures only; no business writes, no legacy changes, no domain activation.
Existing procedures are never overwritten. All four are installed atomically.
Errors 51100 transaction, 51101 contract, 51102 source/domain, 51103 busy,
51104 identity conflict, 51105 incomplete/already-completed request.
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51102,'Select approved CPOS_ACCOUNTING test database.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int;
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=10000;
 IF @lock<0 THROW 51103,'Audit deployment is busy.',1;
 IF (SELECT COUNT(*) FROM sys.tables WHERE schema_id=SCHEMA_ID(N'dbo') AND name IN
 ('Network_Audit_Source','Network_Audit_Request','Network_Audit_Event','Network_Audit_Change'))<>4
 THROW 51102,'Install and verify Phase 2A first.',1;
 IF EXISTS(SELECT 1 FROM sys.objects WHERE schema_id=SCHEMA_ID(N'dbo') AND name IN
 ('Network_Audit_Request_Begin','Network_Audit_Event_Write','Network_Audit_Request_Complete','Network_Audit_Request_Status'))
 THROW 51102,'Audit procedures already exist. Validate; do not overwrite.',1;
 EXEC(N'CREATE PROCEDURE dbo.Network_Audit_Request_Begin
 @SourceId uniqueidentifier,@RequestId uniqueidentifier,@OperationCode varchar(64),@DomainCode varchar(64),
 @ActorKind tinyint,@ActorUserId int=NULL,@ActorServiceCode varchar(64)=NULL,@SessionId uniqueidentifier=NULL,
 @EntityType varchar(64)=NULL,@EntityId nvarchar(128)=NULL,
 @RequestFingerprint varbinary(32),@CanonicalRequest varbinary(max),@LockTimeout int=5000
AS
BEGIN
 SET NOCOUNT ON; SET XACT_ABORT ON;
 IF @@TRANCOUNT=0 OR XACT_STATE()<>1 THROW 51100,''Audit requires the caller writable transaction.'',1;
 IF @SourceId IS NULL OR @RequestId IS NULL OR @RequestId=''00000000-0000-0000-0000-000000000000''
    OR DATALENGTH(@RequestFingerprint)<>32 OR ISNULL(DATALENGTH(@CanonicalRequest),0) NOT BETWEEN 1 AND 1048576
    OR @LockTimeout NOT BETWEEN 0 AND 10000 THROW 51101,''Invalid audit request contract.'',1;
 IF NOT ((@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')
     OR (@OperationCode=''Settlement.LineAdded'' AND @DomainCode=''Inventory.Settlement'')
     OR (@OperationCode IN(''Security.SessionStarted'',''Security.LoginRejected'',''Security.SessionEnded'') AND @DomainCode=''Security''))
     THROW 51101,''Unknown operation/domain.'',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND SchemaVersion=1 AND SourceId=@SourceId)
     THROW 51102,''Audit source/schema mismatch.'',1;
 DECLARE @resource nvarchar(255)=N''CPOS.Audit.Request.''+CONVERT(nvarchar(36),@SourceId)+N''.''+CONVERT(nvarchar(36),@RequestId),@lock int;
 EXEC @lock=sys.sp_getapplock @Resource=@resource,@LockMode=''Exclusive'',@LockOwner=''Transaction'',@LockTimeout=@LockTimeout;
 IF @lock<0 THROW 51103,''Audit request is busy or lock acquisition failed; retry same identity.'',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId)
 BEGIN
   IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId
      AND RequestFingerprint=@RequestFingerprint AND DATALENGTH(CanonicalRequest)=DATALENGTH(@CanonicalRequest) AND CanonicalRequest=@CanonicalRequest)
      THROW 51104,''Request identity conflicts with different content.'',1;
   IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND State<>1)
      THROW 51105,''An incomplete request exists. Do not execute again.'',1;
   SELECT CAST(''Completed'' AS varchar(30)) Status,ResultCode,CONVERT(nvarchar(max),ResultXml) ResultXml
      FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId;
   RETURN;
 END;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND EnabledDomainsXml.exist(''/Domains/Domain[@code=sql:variable("@DomainCode")]'')=1)
     THROW 51102,''Audit domain is disabled. No legacy fallback is allowed.'',1;
 INSERT dbo.Network_Audit_Request(SourceId,RequestId,ContractVersion,OperationCode,ActorKind,ActorUserId,ActorServiceCode,SessionId,
 EntityType,EntityId,RequestFingerprint,CanonicalRequest,State)
 VALUES(@SourceId,@RequestId,1,@OperationCode,@ActorKind,@ActorUserId,@ActorServiceCode,@SessionId,@EntityType,@EntityId,@RequestFingerprint,@CanonicalRequest,0);
 SELECT CAST(''NewPendingCommit'' AS varchar(30)) Status,CAST(NULL AS tinyint) ResultCode,CAST(NULL AS nvarchar(max)) ResultXml;
END');

 EXEC(N'CREATE PROCEDURE dbo.Network_Audit_Event_Write
 @SourceId uniqueidentifier,@RequestId uniqueidentifier,@EventId uniqueidentifier,@EventOrdinal int,
 @EventXml xml,@ChangesXml xml,@EventFingerprint varbinary(32),@CanonicalEvent varbinary(max)
AS
BEGIN
 SET NOCOUNT ON; SET XACT_ABORT ON;
 IF @@TRANCOUNT=0 OR XACT_STATE()<>1 THROW 51100,''Audit requires the caller writable transaction.'',1;
 DECLARE @resource nvarchar(255)=N''CPOS.Audit.Request.''+CONVERT(nvarchar(36),@SourceId)+N''.''+CONVERT(nvarchar(36),@RequestId);
 IF ISNULL(APPLOCK_MODE(''public'',@resource,''Transaction''),''NoLock'')<>''Exclusive''
    THROW 51100,''Begin the request in this transaction before writing an event.'',1;
 IF @EventId IS NULL OR @EventId=''00000000-0000-0000-0000-000000000000'' OR @EventOrdinal NOT BETWEEN 1 AND 100
   OR DATALENGTH(@EventFingerprint)<>32 OR ISNULL(DATALENGTH(@CanonicalEvent),0) NOT BETWEEN 1 AND 1048576
   OR @EventXml IS NULL OR @ChangesXml IS NULL
   OR DATALENGTH(CONVERT(nvarchar(max),@EventXml))>524288 OR DATALENGTH(CONVERT(nvarchar(max),@ChangesXml))>524288
   OR @EventXml.value(''count(/Event)'',''int'')<>1 OR @EventXml.value(''count(/*)'',''int'')<>1
   OR @EventXml.exist(''/Event[@version="1"]'')<>1 OR @ChangesXml.exist(''/Changes[@version="1"]'')<>1
   OR @ChangesXml.value(''count(/Changes/Change)'',''int'')>100
   THROW 51101,''Invalid/oversized event envelope.'',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND SourceId=@SourceId AND SchemaVersion=1)
    THROW 51102,''Audit source/schema mismatch.'',1;
 DECLARE @operation varchar(64),@actor tinyint,@user int,@service varchar(64),@session uniqueidentifier;
 SELECT @operation=OperationCode,@actor=ActorKind,@user=ActorUserId,@service=ActorServiceCode,@session=SessionId
 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND State=0;
 IF @operation IS NULL THROW 51105,''Request is missing or already complete.'',1;
 DECLARE @eventCode varchar(64)=@EventXml.value(''(/Event/@code)[1]'',''varchar(64)''),
  @domain varchar(64)=@EventXml.value(''(/Event/@domain)[1]'',''varchar(64)''),
  @kind tinyint=@EventXml.value(''(/Event/@kind)[1]'',''tinyint''),
  @outcome tinyint=@EventXml.value(''(/Event/@outcome)[1]'',''tinyint'');
 IF @eventCode<>@operation OR NOT (
   (@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3)) OR
   (@operation=''Settlement.LineAdded'' AND @domain=''Inventory.Settlement'' AND @kind=1 AND @actor=1) OR
   (@operation IN(''Security.SessionStarted'',''Security.SessionEnded'') AND @domain=''Security'' AND @kind=2 AND @actor=1 AND @session IS NOT NULL) OR
   (@operation=''Security.LoginRejected'' AND @domain=''Security'' AND @kind=2 AND @actor IN(1,2) AND @session IS NULL))
   THROW 51101,''Event does not match its request contract.'',1;
 IF (@operation=''Security.LoginRejected'' AND @outcome<>2) OR (@operation<>''Security.LoginRejected'' AND @outcome<>1)
   THROW 51101,''Outcome does not match the event.'',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND EnabledDomainsXml.exist(''/Domains/Domain[@code=sql:variable("@domain")]'')=1)
   THROW 51102,''Audit domain is disabled.'',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Event WHERE EventId=@EventId OR (SourceId=@SourceId AND RequestId=@RequestId AND EventOrdinal=@EventOrdinal))
 BEGIN
   IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Event WHERE EventId=@EventId AND SourceId=@SourceId AND RequestId=@RequestId AND EventOrdinal=@EventOrdinal
     AND EventFingerprint=@EventFingerprint AND DATALENGTH(CanonicalEvent)=DATALENGTH(@CanonicalEvent) AND CanonicalEvent=@CanonicalEvent)
     THROW 51104,''Event identity/ordinal conflicts with different content.'',1;
   SELECT CAST(2 AS int) WriteResult;
   RETURN;
 END;
 DECLARE @changes TABLE(ChangeNo int,EntityType varchar(64),EntityId nvarchar(128),FieldCode varchar(64),ValueType varchar(20),
 SourceSqlType varchar(40),BeforeState tinyint,BeforeValue nvarchar(max),AfterState tinyint,AfterValue nvarchar(max),ValueOrigin tinyint);
 INSERT @changes
 SELECT n.value(''@no'',''int''),n.value(''@entityType'',''varchar(64)''),n.value(''@entityId'',''nvarchar(128)''),n.value(''@field'',''varchar(64)''),
 n.value(''@type'',''varchar(20)''),n.value(''(@sqlType)[1]'',''varchar(40)''),n.value(''(Before/@state)[1]'',''tinyint''),
 CASE WHEN n.value(''(Before/@state)[1]'',''tinyint'')=3 THEN n.value(''(Before)[1]'',''nvarchar(max)'') END,
 n.value(''(After/@state)[1]'',''tinyint''),CASE WHEN n.value(''(After/@state)[1]'',''tinyint'')=3 THEN n.value(''(After)[1]'',''nvarchar(max)'') END,n.value(''@origin'',''tinyint'')
 FROM @ChangesXml.nodes(''/Changes/Change'') x(n);
 IF (@kind=1 AND NOT EXISTS(SELECT 1 FROM @changes)) OR (@kind=2 AND EXISTS(SELECT 1 FROM @changes))
    THROW 51101,''Change details are missing or not allowed for this operation.'',1;
 IF EXISTS(SELECT 1 FROM @changes WHERE NOT (
    (@operation=''AuditTest.Changed'' AND ((FieldCode IN(''Quantity'',''Cost'') AND ValueType=''Decimal'') OR (FieldCode=''Name'' AND ValueType=''String'') OR (FieldCode=''RawQuantity'' AND ValueType=''Float'')))
    OR (@operation=''Settlement.LineAdded'' AND ((FieldCode IN(''ItemId'',''StoreId'',''UnitId'') AND ValueType=''Integer'') OR
    (FieldCode IN(''QuantityDelta'',''Cost'',''Total'') AND ValueType=''Decimal'') OR (FieldCode=''StoredQuantity'' AND ValueType=''Float'') OR (FieldCode=''ExpiryDate'' AND ValueType=''DateTime'')))
 )) THROW 51101,''Field/type not allowed by operation contract.'',1;
 IF EXISTS(SELECT 1 FROM @changes WHERE LEN(BeforeValue)>4096 OR LEN(AfterValue)>4096)
    THROW 51101,''Change value too long.'',1;
 IF @operation=''Settlement.LineAdded'' AND EXISTS(SELECT 1 FROM @changes WHERE BeforeState<>1 OR AfterState NOT IN(2,3))
    THROW 51101,''Line addition must use absent-before and captured-after.'',1;
 INSERT dbo.Network_Audit_Event(EventId,SourceId,RequestId,EventOrdinal,CorrelationId,ContractVersion,EventCode,DomainCode,ScreenCode,EventKind,Outcome,
 ActorKind,ActorUserId,ActorServiceCode,ActorName,SessionId,MachineName,ApplicationVersion,EntityType,EntityId,DocumentNumber,OccurredAtUtc,BusinessDate,
 ReasonCode,TemplateCode,TemplateVersion,Notes,RelatedEntitiesXml,EventFingerprint,CanonicalEvent)
 SELECT @EventId,@SourceId,@RequestId,@EventOrdinal,
 CASE WHEN @EventXml.exist(''/Event/@correlation'')=1 THEN @EventXml.value(''(/Event/@correlation)[1]'',''uniqueidentifier'') END,
 1,@eventCode,@domain,@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)''),@kind,@outcome,@actor,@user,@service,
 CASE WHEN @EventXml.exist(''/Event/ActorName'')=1 THEN @EventXml.value(''(/Event/ActorName)[1]'',''nvarchar(256)'') END,@session,
 @EventXml.value(''(/Event/Machine)[1]'',''nvarchar(128)''),@EventXml.value(''(/Event/Application)[1]'',''nvarchar(50)''),
 CASE WHEN @EventXml.exist(''/Event/@entityType'')=1 THEN @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'') END,
 CASE WHEN @EventXml.exist(''/Event/@entityId'')=1 THEN @EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)'') END,
 CASE WHEN @EventXml.exist(''/Event/Document'')=1 THEN @EventXml.value(''(/Event/Document)[1]'',''nvarchar(50)'') END,
 @EventXml.value(''(/Event/@occurred)[1]'',''datetime2(3)''),
 CASE WHEN @EventXml.exist(''/Event/@businessDate'')=1 THEN @EventXml.value(''(/Event/@businessDate)[1]'',''datetime2(3)'') END,
 CASE WHEN @EventXml.exist(''/Event/@reason'')=1 THEN @EventXml.value(''(/Event/@reason)[1]'',''varchar(64)'') END,
 @eventCode,1,@EventXml.value(''(/Event/Notes)[1]'',''nvarchar(max)''),@EventXml.query(''/Event/Entities''),@EventFingerprint,@CanonicalEvent;
 INSERT dbo.Network_Audit_Change(EventId,ChangeNo,EntityType,EntityId,FieldCode,ValueType,SourceSqlType,BeforeState,BeforeValue,AfterState,AfterValue,ValueOrigin)
 SELECT @EventId,ChangeNo,EntityType,EntityId,FieldCode,ValueType,SourceSqlType,BeforeState,BeforeValue,AfterState,AfterValue,ValueOrigin FROM @changes;
 SELECT CAST(1 AS int) WriteResult;
END');

 EXEC(N'CREATE PROCEDURE dbo.Network_Audit_Request_Complete
 @SourceId uniqueidentifier,@RequestId uniqueidentifier,@ResultCode tinyint,@ResultXml xml
AS
BEGIN
 SET NOCOUNT ON; SET XACT_ABORT ON;
 IF @@TRANCOUNT=0 OR XACT_STATE()<>1 THROW 51100,''Audit requires the caller writable transaction.'',1;
 DECLARE @resource nvarchar(255)=N''CPOS.Audit.Request.''+CONVERT(nvarchar(36),@SourceId)+N''.''+CONVERT(nvarchar(36),@RequestId);
 IF ISNULL(APPLOCK_MODE(''public'',@resource,''Transaction''),''NoLock'')<>''Exclusive'' THROW 51100,''Request lock not owned.'',1;
 IF @ResultCode IS NULL OR @ResultCode NOT BETWEEN 1 AND 5 OR @ResultXml IS NULL OR DATALENGTH(CONVERT(nvarchar(max),@ResultXml))>524288
 OR @ResultXml.exist(''/Result[@version="1"]'')<>1 THROW 51101,''Invalid result.'',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND State=0)
   THROW 51105,''Request missing or already complete.'',1;
 DECLARE @count int=(SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE SourceId=@SourceId AND RequestId=@RequestId);
 IF (@ResultCode=5 AND @count<>0) OR (@ResultCode<>5 AND @count=0)
   THROW 51101,''NoChange cannot contain mutation events; other results require an event.'',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Event WHERE SourceId=@SourceId AND RequestId=@RequestId AND Outcome<>@ResultCode)
   THROW 51101,''Result outcome differs from its events.'',1;
 IF @ResultXml.value(''count(/Result/Events/Event)'',''int'')<>@count OR
 EXISTS(SELECT EventId FROM dbo.Network_Audit_Event WHERE SourceId=@SourceId AND RequestId=@RequestId
 EXCEPT SELECT n.value(''@id'',''uniqueidentifier'') FROM @ResultXml.nodes(''/Result/Events/Event'') x(n))
   THROW 51101,''Result event identities do not match stored events.'',1;
 UPDATE dbo.Network_Audit_Request SET State=1,ResultCode=@ResultCode,ResultXml=@ResultXml,CompletedAtUtc=SYSUTCDATETIME()
 WHERE SourceId=@SourceId AND RequestId=@RequestId;
END');

 EXEC(N'CREATE PROCEDURE dbo.Network_Audit_Request_Status
 @SourceId uniqueidentifier,@RequestId uniqueidentifier,@RequestFingerprint varbinary(32),@CanonicalRequest varbinary(max),@LockTimeout int=3000
AS
BEGIN
 SET NOCOUNT ON;
 IF @@TRANCOUNT<>0 THROW 51100,''Status must run on a new connection without a caller transaction.'',1;
 IF @LockTimeout NOT BETWEEN 0 AND 10000 OR DATALENGTH(@RequestFingerprint)<>32 OR ISNULL(DATALENGTH(@CanonicalRequest),0) NOT BETWEEN 1 AND 1048576
   THROW 51101,''Invalid status request.'',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND SourceId=@SourceId AND SchemaVersion=1)
   THROW 51102,''Audit source/schema mismatch.'',1;
 BEGIN TRY
   BEGIN TRANSACTION;
   DECLARE @resource nvarchar(255)=N''CPOS.Audit.Request.''+CONVERT(nvarchar(36),@SourceId)+N''.''+CONVERT(nvarchar(36),@RequestId),@lock int;
   EXEC @lock=sys.sp_getapplock @Resource=@resource,@LockMode=''Shared'',@LockOwner=''Transaction'',@LockTimeout=@LockTimeout;
   IF @lock<0
   BEGIN
     ROLLBACK;
     SELECT CAST(''InProgressOrUnknown'' AS varchar(30)) Status,CAST(NULL AS tinyint) ResultCode,CAST(NULL AS nvarchar(max)) ResultXml;
     RETURN;
   END;
   IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId)
   BEGIN
     IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND RequestFingerprint=@RequestFingerprint
       AND DATALENGTH(CanonicalRequest)=DATALENGTH(@CanonicalRequest) AND CanonicalRequest=@CanonicalRequest)
       THROW 51104,''Request identity conflicts with different content.'',1;
     SELECT CAST(CASE WHEN State=1 THEN ''Completed'' ELSE ''IncompleteCommitted'' END AS varchar(30)) Status,
       ResultCode,CONVERT(nvarchar(max),ResultXml) ResultXml FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId;
   END
   ELSE SELECT CAST(''NotFound'' AS varchar(30)) Status,CAST(NULL AS tinyint) ResultCode,CAST(NULL AS nvarchar(max)) ResultXml;
   COMMIT;
 END TRY
 BEGIN CATCH
   IF @@TRANCOUNT>0 ROLLBACK;
   THROW;
 END CATCH;
END');
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;

