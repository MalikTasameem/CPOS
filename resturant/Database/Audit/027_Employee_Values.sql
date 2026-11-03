/* SQL Server 2014. Employee additions and deductions; payroll SQL unchanged. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51127,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51127,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51127,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51127,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Inventory.DocumentLines"]')=1) THROW 51127,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51127,'Incomplete audit requests.',1;
 IF EXISTS(SELECT 1 FROM sys.triggers WHERE is_disabled=0 AND parent_id=OBJECT_ID('Others_Values')) THROW 51127,'Review employee triggers before deployment.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('EmployeeValue.Created',@sql)>0 OR CHARINDEX('StockDocument.LineAdded',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51127,'Unexpected begin baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''EmployeeValue.Created'',''EmployeeValue.Updated'',''EmployeeValue.Voided'') AND @DomainCode=''People.EmployeeValues'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('EmployeeValue.Created',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51127,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''EmployeeValue.Created'',''EmployeeValue.Updated'',''EmployeeValue.Voided'') AND @domain=''People.EmployeeValues'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51127,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''EmployeeValue.Created'',''EmployeeValue.Updated'',''EmployeeValue.Voided'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51127,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditEmployeeValues027: complete persisted value.
 IF @operation IN(''EmployeeValue.Created'',''EmployeeValue.Updated'',''EmployeeValue.Voided'')
 BEGIN
  DECLARE @evOwner nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @evCreate bit=CASE WHEN @operation=''EmployeeValue.Created'' THEN 1 ELSE 0 END;
  IF @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>''EmployeeValue''
   OR @EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'')<>''Emp_OtherValues''
   OR @EventOrdinal<>1 OR ISNULL(TRY_CONVERT(int,@evOwner),0)<=0
   OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0
   OR @EventXml.value(''count(/Event/Document)'',''int'')<>1
   OR @EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)'')<>@evOwner
   OR (SELECT COUNT(*) FROM @changes)<>10 THROW 51127,''Invalid employee value event.'',1;
  IF NOT EXISTS(SELECT 1 FROM Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=''EmployeeValue'' AND EntityId=CASE WHEN @evCreate=1 THEN ''New'' ELSE @evOwner END) THROW 51127,''Employee request mismatch.'',1;
  DECLARE @evFields TABLE(F varchar(64),T varchar(20),S varchar(40));
  INSERT @evFields VALUES
   (''EVCreatedAt'',''DateTime'',''datetime''),(''EVBusinessDate'',''DateTime'',''datetime''),(''EVYear'',''Integer'',''int''),(''EVMonth'',''Integer'',''int''),
   (''EVEmployeeId'',''Integer'',''int''),(''EVType'',''Integer'',''int''),(''EVAmount'',''Decimal'',''numeric(18,3)''),(''EVNotes'',''String'',''nvarchar(max)''),
   (''EVVoided'',''Integer'',''int''),(''EVOriginalUserId'',''Integer'',''bigint'');
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.EntityType<>''EmployeeValue'' OR c.EntityId<>@evOwner OR c.ValueOrigin<>1 OR
   NOT EXISTS(SELECT 1 FROM @evFields f WHERE f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType) OR
   (@evCreate=1 AND BeforeState<>1) OR (@evCreate=0 AND BeforeState NOT IN(2,3)) OR AfterState NOT IN(2,3)) THROW 51127,''Invalid employee value field.'',1;
  IF @operation=''EmployeeValue.Voided'' AND NOT EXISTS(SELECT 1 FROM @changes WHERE FieldCode=''EVVoided'' AND AfterState=3 AND AfterValue=''1'') THROW 51127,''Employee cancellation not confirmed.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="People.EmployeeValues"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="People.EmployeeValues"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
