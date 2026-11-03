/* SQL Server 2014. Group master and printer-link audit only; GM_* business procedures stay unchanged. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51120,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51120,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51120,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51120,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.Units"]')=1) THROW 51120,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51120,'Incomplete audit requests.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Group.Created',@sql)>0 OR CHARINDEX('Unit.Created',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51120,'Unexpected begin baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''Group.Created'',''Group.Updated'',''Group.Deleted'',''Group.PrinterLinked'',''Group.PrinterUnlinked'') AND @DomainCode=''Catalog.Groups'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Group.Created',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51120,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''Group.Created'',''Group.Updated'',''Group.Deleted'',''Group.PrinterLinked'',''Group.PrinterUnlinked'') AND @domain=''Catalog.Groups'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51120,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Group.Created'',''Group.Updated'',''Group.Deleted'',''Group.PrinterLinked'',''Group.PrinterUnlinked'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51120,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditGroups020: actual master and printer-link rows.
 IF @operation IN(''Group.Created'',''Group.Updated'',''Group.Deleted'',''Group.PrinterLinked'',''Group.PrinterUnlinked'')
 BEGIN
  DECLARE @groupId nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @groupScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'');
  IF @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>''ProductGroup'' OR ISNULL(TRY_CONVERT(int,@groupId),0)<=0
   OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0 THROW 51120,''Invalid group identity.'',1;
  IF @groupScreen NOT IN(''GeneralMenu'',''ItemsMenu'',''IM_ADD_New'') OR (@operation<>''Group.Created'' AND @groupScreen<>''GeneralMenu'') THROW 51120,''Invalid group screen.'',1;
  IF @operation=''Group.Deleted'' AND @groupId=''1'' THROW 51120,''Protected group.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=''ProductGroup''
   AND EntityId=CASE WHEN @operation=''Group.Created'' THEN ''New'' ELSE @groupId END) THROW 51120,''Group request mismatch.'',1;
  DECLARE @gf TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @gf VALUES
   (''ProductGroup'',''GroupName'',''String'',''nvarchar(200)''),(''ProductGroup'',''PrinterId'',''Integer'',''bigint''),
   (''ProductGroup'',''BackRed'',''Integer'',''int''),(''ProductGroup'',''BackGreen'',''Integer'',''int''),(''ProductGroup'',''BackBlue'',''Integer'',''int''),
   (''ProductGroup'',''ForeRed'',''Integer'',''int''),(''ProductGroup'',''ForeGreen'',''Integer'',''int''),(''ProductGroup'',''ForeBlue'',''Integer'',''int''),
   (''ProductGroup'',''ShowInPos'',''Integer'',''int''),(''ProductGroup'',''GroupRank'',''Float'',''float(53)''),
   (''ProductGroup'',''GroupStoreId'',''Integer'',''int''),(''ProductGroup'',''KitchenScreen'',''String'',''nvarchar(200)''),(''ProductGroup'',''CategoryId'',''Integer'',''int''),
   (''GroupPrinter'',''GroupId'',''Integer'',''int''),(''GroupPrinter'',''LinkedPrinterId'',''Integer'',''int'');
  IF EXISTS(SELECT 1 FROM @changes c WHERE ValueOrigin<>1 OR NOT EXISTS(SELECT 1 FROM @gf f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51120,''Invalid group field.'',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE WHEN EntityType=''ProductGroup'' THEN 13 ELSE 2 END) THROW 51120,''Incomplete group row.'',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes)<>13 OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>''ProductGroup'' OR EntityId<>@groupId
   OR (@operation=''Group.Created'' AND BeforeState<>1) OR (@operation<>''Group.Created'' AND BeforeState NOT IN(2,3))
   OR (@operation=''Group.Deleted'' AND AfterState<>1) OR (@operation<>''Group.Deleted'' AND AfterState NOT IN(2,3))) THROW 51120,''Invalid group header states.'',1;
  END
  ELSE
  BEGIN
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType<>''GroupPrinter'' OR BeforeState NOT IN(1,3) OR AfterState NOT IN(1,3) OR (BeforeState=1 AND AfterState=1)
    OR (FieldCode=''GroupId'' AND ((BeforeState=3 AND BeforeValue<>@groupId) OR (AfterState=3 AND AfterValue<>@groupId)))) THROW 51120,''Invalid printer-link row.'',1;
  END;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Catalog.Groups"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.Groups"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
