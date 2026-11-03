/* SQL Server 2014. Unit master audit only; Unit_* business procedures stay unchanged. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51119,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51119,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51119,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51119,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Documents.Cancellation"]')=1) THROW 51119,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51119,'Incomplete audit requests.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Unit.Created',@sql)>0 OR CHARINDEX('Document.Voided',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51119,'Unexpected begin baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''Unit.Created'',''Unit.Updated'',''Unit.Deleted'') AND @DomainCode=''Catalog.Units'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Unit.Created',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51119,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''Unit.Created'',''Unit.Updated'',''Unit.Deleted'') AND @domain=''Catalog.Units'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51119,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Unit.Created'',''Unit.Updated'',''Unit.Deleted'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51119,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditUnits019: complete actual master row.
 IF @operation IN(''Unit.Created'',''Unit.Updated'',''Unit.Deleted'')
 BEGIN
  DECLARE @unitId nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @unitScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'');
  IF @EventOrdinal<>1 OR @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>''Unit''
   OR ISNULL(TRY_CONVERT(int,@unitId),0)<=2 OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0
   OR (SELECT COUNT(*) FROM @changes)<>3 THROW 51119,''Invalid unit event identity.'',1;
  IF @unitScreen NOT IN(''Units'',''ItemsMenu'',''IM_ADD_New'',''Add_Unit'') OR (@operation<>''Unit.Created'' AND @unitScreen<>''Units'') THROW 51119,''Invalid unit screen.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=''Unit''
   AND EntityId=CASE WHEN @operation=''Unit.Created'' THEN ''New'' ELSE @unitId END) THROW 51119,''Unit request mismatch.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType<>''Unit'' OR EntityId<>@unitId OR ValueOrigin<>1
   OR BeforeState<>CASE WHEN @operation=''Unit.Created'' THEN 1 ELSE 3 END OR AfterState<>CASE WHEN @operation=''Unit.Deleted'' THEN 1 ELSE 3 END
   OR NOT ((FieldCode=''UnitName'' AND ValueType=''String'' AND SourceSqlType=''nvarchar(250)'')
    OR (FieldCode=''UnitCargo'' AND ValueType=''Float'' AND SourceSqlType=''float(53)'')
    OR (FieldCode=''UnitDefault'' AND ValueType=''Boolean'' AND SourceSqlType=''bit''))) THROW 51119,''Invalid unit fields or states.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Catalog.Units"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.Units"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
