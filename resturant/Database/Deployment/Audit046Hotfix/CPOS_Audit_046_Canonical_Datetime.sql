/*
 CPOS - إصلاح 046 لتوافق تاريخ المراقبة مع SQL Server 2014.
 يعالج CatalogItemRecord after-state differs عندما يرفض التحويل تاريخ الصنف الصحيح.
 يتطلب التحديث 044 أو أحدث؛ لا يغني عن 045 وصلاحية بطاقة العملاء إن لم يطبقا.

 1) أغلق CPOS على جميع الأجهزة واتصالات القاعدة الأخرى؛ اترك نافذة الاستعلام فقط.
 2) اختر قاعدة العميل من قائمة SSMS، وطابق @ExpectedDatabase أدناه مع اسمها.
 3) نفذ الملف كاملًا F5 دون تحديد جزء ودون إضافة GO، بحساب مسؤول مخول.
 4) انتظر UPGRADE SUCCESS، ثم شغّل التنفيذي المحدث واختبر إضافة صنف.

 ينشئ نسخة احتياطية COPY_ONLY/CHECKSUM ويتحقق منها قبل التعديل.
 NULL = مجلد بيانات القاعدة على الخادم؛ يمكن تحديد مجلد آخر تكتب فيه خدمة SQL.
 لا يحذف بيانات ولا يغير تاريخًا محفوظًا أو هوية المصدر، ولا يفصل أي اتصال.
 هذا إصلاح محدد وليس اعتمادًا شاملًا لكل عمليات المنظومة.
*/
DECLARE @ExpectedDatabase sysname = N'CPOS_ACCOUNTING';
DECLARE @BackupDirectory nvarchar(2000) = NULL;
IF @@TRANCOUNT<>0 OR (@@OPTIONS & 2)=2
BEGIN
 RAISERROR('Run in a new query with no open transaction and IMPLICIT_TRANSACTIONS OFF.',16,1);
 RETURN;
END;
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>@ExpectedDatabase OR DB_ID()<=4
 THROW 51246,'Select the customer database and match ExpectedDatabase first. No changes made.',1;
IF CONVERT(int,SERVERPROPERTY('ProductMajorVersion'))<12
 THROW 51246,'SQL Server 2014 or later is required.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1
 OR ISNULL(HAS_PERMS_BY_NAME(DB_NAME(),'DATABASE','CONTROL'),0)<>1
 THROW 51246,'CONTROL database and VIEW SERVER STATE required.',1;
DECLARE @lock int=-1,@release int,@source uniqueidentifier,@before nvarchar(max),@file nvarchar(4000),
 @backup nvarchar(4000),@history bit=0,@hash varchar(64)='9E4B7677EDCC3689B26D015971BA425DF24F89EB8F8DAD592C57E0BF320FAD3E';
BEGIN TRY
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Session',@LockTimeout=5000;
 IF @lock<0 THROW 51246,'Another audit deployment is running.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID())
 BEGIN
  SELECT session_id,host_name,program_name FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID();
  THROW 51246,'Close other CPOS/SSMS connections. No connection was terminated.',1;
 END;
 SET @before=OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write'));
 IF @before IS NULL OR CHARINDEX(N'-- Audit044: safe datetime validation across audit domains.',@before)=0
  THROW 51246,'This correction requires audit version 044. No changes made.',1;
 SELECT @source=SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND SchemaVersion=1;
 IF @source IS NULL THROW 51246,'Audit source identity missing.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1)
  THROW 51246,'Incomplete audit requests. Review with support first.',1;
 IF OBJECT_ID(N'dbo.CPOS_Database_Migration',N'U') IS NOT NULL
 BEGIN
  SET @history=1;
  EXEC sys.sp_executesql N'
   IF EXISTS(SELECT 1 FROM dbo.CPOS_Database_Migration WHERE Component=N''NetworkAuditV2'' AND TRY_CONVERT(int,MigrationId)>46)
    THROW 51246,''Newer audit version is recorded; do not downgrade.'',1;
   IF EXISTS(SELECT 1 FROM dbo.CPOS_Database_Migration WHERE Component=N''NetworkAuditV2'' AND MigrationId=''046'' AND Sha256<>@Hash)
    THROW 51246,''Existing 046 hash differs.'',1;',N'@Hash varchar(64)',@hash;
 END;
 IF @BackupDirectory IS NULL
 BEGIN
  SELECT TOP(1) @file=physical_name FROM sys.database_files WHERE type=0 ORDER BY file_id;
  IF CHARINDEX(N'\',REVERSE(@file))=0 THROW 51246,'Set a server-side backup directory.',1;
  SET @BackupDirectory=LEFT(@file,LEN(@file)-CHARINDEX(N'\',REVERSE(@file))+1);
 END;
 IF NULLIF(LTRIM(RTRIM(@BackupDirectory)),N'') IS NULL THROW 51246,'Backup directory must not be empty.',1;
 IF RIGHT(@BackupDirectory,1)<>N'\' SET @BackupDirectory=@BackupDirectory+N'\';
 SET @backup=@BackupDirectory+N'CPOS_Before_Audit046_'+CONVERT(char(8),GETDATE(),112)+N'_'+
  REPLACE(CONVERT(char(8),GETDATE(),108),N':',N'')+N'_'+REPLACE(CONVERT(nvarchar(36),NEWID()),N'-',N'')+N'.bak';
 IF LEN(@backup)>259 THROW 51246,'Backup path too long.',1;
 SELECT CONVERT(nvarchar(128),SERVERPROPERTY('ServerName')) AS TargetServer,DB_NAME() AS TargetDatabase,@source AS AuditSourceId,@backup AS BackupFile;
 BACKUP DATABASE @ExpectedDatabase TO DISK=@backup WITH COPY_ONLY,CHECKSUM;
 RESTORE VERIFYONLY FROM DISK=@backup WITH CHECKSUM;
 BEGIN TRANSACTION;
 IF CONVERT(varbinary(max),OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write')))<>CONVERT(varbinary(max),@before)
  THROW 51246,'Audit writer changed during backup. No upgrade committed.',1;
 EXEC sys.sp_executesql N'/* SQL Server 2014. AuditValue emits ISO timestamps with seven fractional digits.
   Parse as datetime2(7), then safely round/range-check against the stored datetime.
   BOTH conversions must be TRY_CONVERT: malformed and pre-1753 values must not throw.
   Incremental after 044 (also compatible with 045); no business data changes. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>@ExpectedDatabase THROW 51146,''Approved database only.'',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,''VIEW SERVER STATE''),0)<>1 THROW 51146,''Cannot verify sessions.'',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@marker nvarchar(200)=N''-- Audit046: canonical seven-digit datetime validation.'',
  @anchor nvarchar(200)=N''-- Audit044: safe datetime validation across audit domains.'';
 EXEC @lock=sys.sp_getapplock @Resource=N''CPOS.NetworkAudit.Schema'',@LockMode=''Exclusive'',@LockOwner=''Transaction'',@LockTimeout=5000;
 IF @lock<0 THROW 51146,''Deployment busy.'',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID())
  THROW 51146,''Close other database clients.'',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SchemaVersion=1)
  THROW 51146,''Audit source prerequisite is missing.'',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51146,''Incomplete audit requests.'',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID(N''dbo.Network_Audit_Event_Write''));
 IF @sql IS NULL OR CHARINDEX(@anchor,@sql)=0 THROW 51146,''Apply stage 044 first.'',1;
 DECLARE @fixes TABLE(OldText nvarchar(200),NewText nvarchar(600),ExpectedCount int);
 INSERT @fixes VALUES
 (N''TRY_CONVERT(datetime,c.AfterValue,126)'',N''(CASE WHEN TRY_CONVERT(datetime2(7),c.AfterValue,126)<=TRY_CONVERT(datetime2(7),''''9999-12-31T23:59:59.9970000'''',126) THEN TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) END)'',60),
 (N''TRY_CONVERT(datetime,q.[QueueDateValue],126)'',N''(CASE WHEN TRY_CONVERT(datetime2(7),q.[QueueDateValue],126)<=TRY_CONVERT(datetime2(7),''''9999-12-31T23:59:59.9970000'''',126) THEN TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),q.[QueueDateValue],126)) END)'',2),
 -- @value occurs inside a dynamic SQL string in the writer, so its literal is escaped twice.
 (N''TRY_CONVERT(datetime,@value,126)'',N''(CASE WHEN TRY_CONVERT(datetime2(7),@value,126)<=TRY_CONVERT(datetime2(7),''''''''9999-12-31T23:59:59.9970000'''''''',126) THEN TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),@value,126)) END)'',1);
 IF CHARINDEX(@marker,@sql)>0
 BEGIN
  IF EXISTS(SELECT 1 FROM @fixes WHERE CHARINDEX(OldText,@sql)>0 OR
    (LEN(@sql)-LEN(REPLACE(@sql,NewText,N'''')))/LEN(NewText)<>ExpectedCount)
   THROW 51146,''Canonical datetime marker exists but validation differs.'',1;
  COMMIT;
  RETURN;
 END;
 IF EXISTS(SELECT 1 FROM @fixes WHERE (LEN(@sql)-LEN(REPLACE(@sql,OldText,N'''')))/LEN(OldText)<>ExpectedCount OR CHARINDEX(NewText,@sql)>0)
  THROW 51146,''Unexpected canonical datetime conversion anchors.'',1;
 DECLARE @old nvarchar(200),@new nvarchar(600);
 DECLARE fixes CURSOR LOCAL FAST_FORWARD FOR SELECT OldText,NewText FROM @fixes;
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
  EXISTS(SELECT 1 FROM @fixes WHERE CHARINDEX(OldText,@sql)>0 OR
    (LEN(@sql)-LEN(REPLACE(@sql,NewText,N'''')))/LEN(NewText)<>ExpectedCount)
  THROW 51146,''Canonical datetime repair verification failed.'',1;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
',N'@ExpectedDatabase sysname',@ExpectedDatabase;
 IF @history=1
  EXEC sys.sp_executesql N'
   IF NOT EXISTS(SELECT 1 FROM dbo.CPOS_Database_Migration WITH(UPDLOCK,HOLDLOCK) WHERE Component=N''NetworkAuditV2'' AND MigrationId=''046'')
    INSERT dbo.CPOS_Database_Migration(MigrationId,Component,MigrationName,Sha256,ToolVersion,SourceId,AppliedBy,HostName)
    VALUES(''046'',N''NetworkAuditV2'',N''046_Audit_Canonical_Datetime.sql'',@Hash,''046-Date-Query'',@Source,SUSER_SNAME(),ISNULL(HOST_NAME(),N''''));',
    N'@Hash varchar(64),@Source uniqueidentifier',@hash,@source;
 IF @@TRANCOUNT<>1 THROW 51246,'Unexpected transaction state.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND SourceId=@source AND SchemaVersion=1)
  THROW 51246,'Source identity changed unexpectedly.',1;
 COMMIT;
 EXEC @release=sys.sp_releaseapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockOwner='Session';
 SET @lock=-1;
 SELECT N'UPGRADE SUCCESS' AS UpgradeStatus,DB_NAME() AS DatabaseName,N'046' AS AuditCorrection,@backup AS VerifiedBackup,@source AS AuditSourceId;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 IF @lock>=0 EXEC @release=sys.sp_releaseapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockOwner='Session';
 PRINT N'UPGRADE FAILED - retain the error and contact support. Do not ignore it.';
 THROW;
END CATCH;
