/*
 CPOS - ترقية تكميلية من 044 إلى 045 + صلاحية فتح بطاقة العملاء والموردين
 SQL Server 2014 أو أحدث. ليس تثبيتًا أوليًا ولا اعتمادًا شاملًا لبقية عمليات المنظومة.

 طريقة التشغيل في SQL Server Management Studio:
 1) أغلق CPOS على جميع الأجهزة واتصالات القاعدة الأخرى، واترك نافذة الاستعلام هذه فقط.
 2) اختر قاعدة العميل من قائمة قواعد البيانات أعلى النافذة، وليس master.
 3) تأكد أن @ExpectedDatabase أدناه يطابق اسم القاعدة المختارة.
 4) نفّذ الملف كاملًا مرة واحدة بواسطة F5، دون تحديد جزء منه ودون إضافة GO.
 5) لا تستبدل التنفيذي ولا تفتح CPOS إلا بعد ظهور UPGRADE SUCCESS في النتيجة الأخيرة.

 النسخة الاحتياطية تلقائية COPY_ONLY + CHECKSUM ثم RESTORE VERIFYONLY قبل أي تعديل.
 المسار على جهاز SQL Server وليس على جهاز المشغّل. NULL = مجلد ملف بيانات القاعدة.
 يمكن تخصيص مجلد موجود مسبقًا ومسموح لخدمة SQL الكتابة فيه، مثل N'D:\SQLBackups\'.
 لا تُغلق هذه النافذة أثناء التنفيذ. عند أي خطأ احتفظ برسالة الخطأ وراجع الدعم.
 لا يحذف الملف بيانات الأعمال أو المراقبة، ولا يفصل اتصالًا، ولا يعيد هوية القاعدة.
 يسمح بالبطاقة افتراضيًا؛ إعادة التنفيذ لا تلغي أي منع صلاحية حُفظ سابقًا.
 ملف ترقية قاعدة البيانات فقط؛ يلزم التنفيذي المطابق لإظهار خيار الصلاحية وتطبيقه.
*/
DECLARE @ExpectedDatabase sysname = N'CPOS_ACCOUNTING';
DECLARE @BackupDirectory nvarchar(2000) = NULL;

-- Leave the remainder unchanged. Run as a single batch.
IF @@TRANCOUNT<>0 OR (@@OPTIONS & 2)=2
BEGIN
 RAISERROR('Run in a new query with no open transaction and IMPLICIT_TRANSACTIONS OFF.',16,1);
 RETURN;
END;
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>@ExpectedDatabase OR DB_ID()<=4
 THROW 51245,'Select the customer database and match ExpectedDatabase first. No changes made.',1;
IF CONVERT(int,SERVERPROPERTY('ProductMajorVersion'))<12
 THROW 51245,'SQL Server 2014 or later is required.',1;
IF (SELECT compatibility_level FROM sys.databases WHERE database_id=DB_ID())<110
 THROW 51245,'Database compatibility level 110 or later is required. Review with support.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1
 OR ISNULL(HAS_PERMS_BY_NAME(DB_NAME(),'DATABASE','CONTROL'),0)<>1
 THROW 51245,'Use an authorized database administrator: CONTROL database and VIEW SERVER STATE required.',1;

DECLARE @auditLock int=-1,@securityLock int=-1,@release int,@source uniqueidentifier,
 @writer nvarchar(max),@before nvarchar(max),@backup nvarchar(4000),@file nvarchar(4000),
 @sql nvarchar(max),@history bit=0,@auditHash varchar(64)='0685BB67BC3CBB6058D5E3C2ADAD8C1DD27203953DC6CB77A67C5D39AEDA4FD7',
 @securityHash varchar(64)='C39A77D2B1380D797BC05244E056F80BD643062B955564BBF37228CD414D1CB2',@baseHash varchar(64)='86B0F0C187D58775C15979E127E63DD9712068A991ED5A48C4C94C73B82E1717';
BEGIN TRY
 EXEC @auditLock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Session',@LockTimeout=5000;
 IF @auditLock<0 THROW 51245,'Another audit deployment is running.',1;
 EXEC @securityLock=sys.sp_getapplock @Resource=N'CPOS.Security.Schema',@LockMode='Exclusive',@LockOwner='Session',@LockTimeout=5000;
 IF @securityLock<0 THROW 51245,'Another security deployment is running.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID())
 BEGIN
  SELECT session_id AS OtherSession,host_name AS Computer,program_name AS Program
  FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID();
  THROW 51245,'Close other CPOS/SSMS connections to this database. No connection was terminated.',1;
 END;
 IF OBJECT_ID(N'dbo.Network_Audit_Source',N'U') IS NULL
 OR OBJECT_ID(N'dbo.Network_Audit_Request',N'U') IS NULL
 OR OBJECT_ID(N'dbo.Users_Validations',N'U') IS NULL
  THROW 51245,'This is not a compatible CPOS audit database. Install/verify version 044 first.',1;
 SELECT @source=SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND SchemaVersion=1;
 IF @source IS NULL THROW 51245,'Valid audit source identity is required.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1)
  THROW 51245,'Incomplete audit requests exist. Review with support before upgrading.',1;
 SET @writer=OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write'));
 SET @before=@writer;
 IF @writer IS NULL OR CHARINDEX(N'-- Audit044: safe datetime validation across audit domains.',@writer)=0
  THROW 51245,'This incremental query requires audit version 044. No changes made.',1;

 -- A marker alone is not sufficient: verify the repaired datetime guards as well.
 IF CHARINDEX(N'CONVERT(datetime,TRY_CONVERT(datetime2(7),',@writer)>0
 OR (LEN(@writer)-LEN(REPLACE(@writer,N'TRY_CONVERT(datetime,c.AfterValue,126)',N'')))/LEN(N'TRY_CONVERT(datetime,c.AfterValue,126)')<>60
 OR (LEN(@writer)-LEN(REPLACE(@writer,N'TRY_CONVERT(datetime,q.[QueueDateValue],126)',N'')))/LEN(N'TRY_CONVERT(datetime,q.[QueueDateValue],126)')<>2
 OR (LEN(@writer)-LEN(REPLACE(@writer,N'TRY_CONVERT(datetime,@value,126)',N'')))/LEN(N'TRY_CONVERT(datetime,@value,126)')<>1
  THROW 51245,'Version 044 datetime validation differs. Review with support.',1;

 IF OBJECT_ID(N'dbo.CPOS_Database_Migration',N'U') IS NOT NULL
 BEGIN
  SET @history=1;
  -- Dynamic SQL keeps legacy databases without a history table supported.
  EXEC sys.sp_executesql N'
   IF EXISTS(SELECT 1 FROM dbo.CPOS_Database_Migration WHERE Component=N''NetworkAuditV2'' AND TRY_CONVERT(int,MigrationId)>45)
    THROW 51245,''A newer audit upgrade is recorded. Do not downgrade.'',1;
   IF EXISTS(SELECT 1 FROM dbo.CPOS_Database_Migration WHERE Component=N''NetworkAuditV2'' AND MigrationId=''044'' AND Sha256<>@BaseHash)
    THROW 51245,''Existing 044 history hash differs.'',1;
   IF EXISTS(SELECT 1 FROM dbo.CPOS_Database_Migration WHERE Component=N''NetworkAuditV2'' AND MigrationId=''045'' AND Sha256<>@AuditHash)
    THROW 51245,''Existing 045 history hash differs.'',1;
   IF EXISTS(SELECT 1 FROM dbo.CPOS_Database_Migration WHERE Component=N''Security.AgentCard'' AND MigrationId=''002'' AND Sha256<>@SecurityHash)
    THROW 51245,''Existing Agent card permission history hash differs.'',1;',
   N'@BaseHash varchar(64),@AuditHash varchar(64),@SecurityHash varchar(64)',@baseHash,@auditHash,@securityHash;
 END;

 -- Server-side backup; a unique name avoids overwriting an existing backup.
 IF @BackupDirectory IS NULL
 BEGIN
  SELECT TOP(1) @file=physical_name FROM sys.database_files WHERE type=0 ORDER BY file_id;
  IF CHARINDEX(N'\',REVERSE(@file))=0 THROW 51245,'Set a valid SQL Server backup directory.',1;
  SET @BackupDirectory=LEFT(@file,LEN(@file)-CHARINDEX(N'\',REVERSE(@file))+1);
 END;
 IF NULLIF(LTRIM(RTRIM(@BackupDirectory)),N'') IS NULL THROW 51245,'Backup directory must not be empty.',1;
 IF RIGHT(@BackupDirectory,1)<>N'\' SET @BackupDirectory=@BackupDirectory+N'\';
 SET @backup=@BackupDirectory+N'CPOS_Before_Audit045_AgentCard_'+CONVERT(char(8),GETDATE(),112)+N'_'+
  REPLACE(CONVERT(char(8),GETDATE(),108),N':',N'')+N'_'+REPLACE(CONVERT(nvarchar(36),NEWID()),N'-',N'')+N'.bak';
 IF LEN(@backup)>259 THROW 51245,'Backup path is too long. Choose a shorter directory.',1;
 SELECT CONVERT(nvarchar(128),SERVERPROPERTY('ServerName')) AS TargetServer,DB_NAME() AS TargetDatabase,
  @source AS AuditSourceId,@backup AS BackupFile;
 PRINT N'Backing up and verifying the customer database. Please wait...';
 BACKUP DATABASE @ExpectedDatabase TO DISK=@backup WITH COPY_ONLY,CHECKSUM;
 RESTORE VERIFYONLY FROM DISK=@backup WITH CHECKSUM;
 PRINT N'Backup verified. Applying both changes in one transaction...';

 BEGIN TRANSACTION;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID())
  THROW 51245,'A new database client connected. Close clients and rerun; no upgrade changes committed.',1;
 IF CONVERT(varbinary(max),OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write')))<>CONVERT(varbinary(max),@before)
  THROW 51245,'Audit writer changed during backup. Review before retrying.',1;

 -- Canonical migration 045: only its database guard is parameterized.
 EXEC sys.sp_executesql N'/* SQL Server 2014. Harden the three raw DocumentType integer conversions.
   Keep the existing document identity/screen validation and fail closed on invalid types. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>@ExpectedDatabase THROW 51145,''Approved database only.'',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,''VIEW SERVER STATE''),0)<>1 THROW 51145,''Cannot verify sessions.'',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@marker nvarchar(200)=N''-- Audit045: safe document type validation.'',
  @anchor nvarchar(200)=N''-- Audit044: safe datetime validation across audit domains.'';
 EXEC @lock=sys.sp_getapplock @Resource=N''CPOS.NetworkAudit.Schema'',@LockMode=''Exclusive'',@LockOwner=''Transaction'',@LockTimeout=5000;
 IF @lock<0 THROW 51145,''Deployment busy.'',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID())
  THROW 51145,''Close other database clients.'',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SchemaVersion=1)
  THROW 51145,''Audit source prerequisite is missing.'',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51145,''Incomplete audit requests.'',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID(N''dbo.Network_Audit_Event_Write''));
 IF @sql IS NULL OR CHARINDEX(@anchor,@sql)=0 THROW 51145,''Apply stage 044 first.'',1;

 DECLARE @replacements TABLE(OldText nvarchar(600),NewText nvarchar(600));
 INSERT @replacements VALUES
 (N''DECLARE @docType int=(SELECT CONVERT(int,AfterValue) FROM @changes WHERE FieldCode=''''DocumentType'''');'',
  N''DECLARE @docType int=(SELECT TRY_CONVERT(int,AfterValue) FROM @changes WHERE FieldCode=''''DocumentType'''');
  IF @docType IS NULL THROW 51101,''''Invalid document type.'''',1;''),
 (N''SELECT @voidType=CONVERT(int,AfterValue) FROM @changes'',
  N''SELECT @voidType=TRY_CONVERT(int,AfterValue) FROM @changes''),
 (N''SELECT @voidType=CONVERT(int,c.AfterValue) FROM dbo.Network_Audit_Event'',
  N''SELECT @voidType=TRY_CONVERT(int,c.AfterValue) FROM dbo.Network_Audit_Event'');
 IF CHARINDEX(N''IF @voidType IS NULL OR NOT ('',@sql)=0 THROW 51145,''Cancellation validation guard is missing.'',1;
 IF CHARINDEX(@marker,@sql)>0
 BEGIN
  IF EXISTS(SELECT 1 FROM @replacements WHERE CHARINDEX(OldText,@sql)>0 OR
   (LEN(@sql)-LEN(REPLACE(@sql,NewText,N'''')))/LEN(NewText)<>1)
   THROW 51145,''Document type marker exists but validation differs.'',1;
  COMMIT;
  RETURN;
 END;
 IF EXISTS(SELECT 1 FROM @replacements WHERE (LEN(@sql)-LEN(REPLACE(@sql,OldText,N'''')))/LEN(OldText)<>1 OR CHARINDEX(NewText,@sql)>0)
  THROW 51145,''Unexpected document type conversion anchors.'',1;
 DECLARE @old nvarchar(600),@new nvarchar(600);
 DECLARE fixes CURSOR LOCAL FAST_FORWARD FOR SELECT OldText,NewText FROM @replacements;
 OPEN fixes;
 FETCH NEXT FROM fixes INTO @old,@new;
 WHILE @@FETCH_STATUS=0
 BEGIN
  SET @sql=REPLACE(@sql,@old,@new);
  FETCH NEXT FROM fixes INTO @old,@new;
 END;
 CLOSE fixes;
 DEALLOCATE fixes;
 SET @sql=STUFF(@sql,CHARINDEX(@anchor,@sql),0,@marker+CHAR(13)+CHAR(10));
 SET @sql=REPLACE(@sql,N''CREATE PROCEDURE'',N''ALTER PROCEDURE'');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID(N''dbo.Network_Audit_Event_Write''));
 IF @sql IS NULL OR CHARINDEX(@marker,@sql)=0 OR CHARINDEX(@anchor,@sql)=0 OR
  EXISTS(SELECT 1 FROM @replacements WHERE CHARINDEX(OldText,@sql)>0 OR (LEN(@sql)-LEN(REPLACE(@sql,NewText,N'''')))/LEN(NewText)<>1)
  THROW 51145,''Document type repair verification failed.'',1;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
',N'@ExpectedDatabase sysname',@ExpectedDatabase;
 -- Independent Agent card permission: keep existing restrictions on rerun.
 EXEC sys.sp_executesql N'/* Independent Agent screen access; existing roles keep their current access.
   The existing Agents/Balance permissions and their procedures/views are unchanged. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>@ExpectedDatabase THROW 51202,''Approved database only.'',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,''VIEW SERVER STATE''),0)<>1 THROW 51202,''Cannot verify sessions.'',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int;
 EXEC @lock=sys.sp_getapplock @Resource=N''CPOS.Security.Schema'',@LockMode=''Exclusive'',@LockOwner=''Transaction'',@LockTimeout=5000;
 IF @lock<0 THROW 51202,''Deployment busy.'',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID())
  THROW 51202,''Close other database clients.'',1;
 IF OBJECT_ID(N''dbo.Users_Validations'',N''U'') IS NULL THROW 51202,''User validations table is missing.'',1;
 IF COL_LENGTH(N''dbo.Users_Validations'',N''Open_Agent_Card'') IS NULL
  ALTER TABLE dbo.Users_Validations ADD Open_Agent_Card bit NOT NULL
   CONSTRAINT DF_Users_Validations_Open_Agent_Card DEFAULT(1) WITH VALUES;
 IF NOT EXISTS(SELECT 1 FROM sys.columns c JOIN sys.default_constraints d ON d.object_id=c.default_object_id
  WHERE c.object_id=OBJECT_ID(N''dbo.Users_Validations'') AND c.name=N''Open_Agent_Card''
   AND c.system_type_id=104 AND c.is_nullable=0 AND REPLACE(REPLACE(d.definition,N''('',N''''),N'')'',N'''')=N''1'')
  THROW 51202,''Agent card permission schema differs from the expected default-allow bit.'',1;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
',N'@ExpectedDatabase sysname',@ExpectedDatabase;

 IF @history=1
 BEGIN
  EXEC sys.sp_executesql N'
   IF NOT EXISTS(SELECT 1 FROM dbo.CPOS_Database_Migration WITH(UPDLOCK,HOLDLOCK) WHERE Component=N''NetworkAuditV2'' AND MigrationId=''045'')
    INSERT dbo.CPOS_Database_Migration(MigrationId,Component,MigrationName,Sha256,ToolVersion,SourceId,AppliedBy,HostName)
    VALUES(''045'',N''NetworkAuditV2'',N''045_Audit_Safe_Document_Type.sql'',@AuditHash,''045-AgentCard-Query'',@Source,SUSER_SNAME(),ISNULL(HOST_NAME(),N''''));
   IF NOT EXISTS(SELECT 1 FROM dbo.CPOS_Database_Migration WITH(UPDLOCK,HOLDLOCK) WHERE Component=N''Security.AgentCard'' AND MigrationId=''002'')
    INSERT dbo.CPOS_Database_Migration(MigrationId,Component,MigrationName,Sha256,ToolVersion,SourceId,AppliedBy,HostName)
    VALUES(''002'',N''Security.AgentCard'',N''002_User_Validation_Agent_Card.sql'',@SecurityHash,''045-AgentCard-Query'',@Source,SUSER_SNAME(),ISNULL(HOST_NAME(),N''''));',
    N'@AuditHash varchar(64),@SecurityHash varchar(64),@Source uniqueidentifier',@auditHash,@securityHash,@source;
 END;
 IF @@TRANCOUNT<>1 THROW 51245,'Unexpected upgrade transaction state.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND SourceId=@source AND SchemaVersion=1)
  THROW 51245,'Audit source identity changed unexpectedly.',1;
 IF CHARINDEX(N'-- Audit045: safe document type validation.',OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write')))=0
 OR COL_LENGTH(N'dbo.Users_Validations',N'Open_Agent_Card') IS NULL
  THROW 51245,'Final upgrade verification failed.',1;
 COMMIT;
 EXEC @release=sys.sp_releaseapplock @Resource=N'CPOS.Security.Schema',@LockOwner='Session';
 SET @securityLock=-1;
 EXEC @release=sys.sp_releaseapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockOwner='Session';
 SET @auditLock=-1;
 SELECT N'UPGRADE SUCCESS' AS UpgradeStatus,DB_NAME() AS DatabaseName,N'045' AS AuditVersion,
  N'Installed (existing restrictions preserved)' AS AgentCardPermission,@backup AS VerifiedBackup,@source AS AuditSourceId,
  @history AS MigrationHistoryAvailable;
 PRINT N'UPGRADE SUCCESS - install the matching CPOS executable, then test with an ordinary user.';
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 IF @securityLock>=0 EXEC @release=sys.sp_releaseapplock @Resource=N'CPOS.Security.Schema',@LockOwner='Session';
 IF @auditLock>=0 EXEC @release=sys.sp_releaseapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockOwner='Session';
 PRINT N'UPGRADE FAILED - read the error below. Do not replace the executable or ignore the error.';
 THROW;
END CATCH;
