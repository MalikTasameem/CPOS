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
 @sql nvarchar(max),@history bit=0,@auditHash varchar(64)='__AUDIT_SHA__',
 @securityHash varchar(64)='__SECURITY_SHA__',@baseHash varchar(64)='__BASE044_SHA__';
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
 EXEC sys.sp_executesql N'__AUDIT_SQL__',N'@ExpectedDatabase sysname',@ExpectedDatabase;
 -- Independent Agent card permission: keep existing restrictions on rerun.
 EXEC sys.sp_executesql N'__SECURITY_SQL__',N'@ExpectedDatabase sysname',@ExpectedDatabase;

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
