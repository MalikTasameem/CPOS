/* SQL Server 2014. Harden the three raw DocumentType integer conversions.
   Keep the existing document identity/screen validation and fail closed on invalid types. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51145,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51145,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@marker nvarchar(200)=N'-- Audit045: safe document type validation.',
  @anchor nvarchar(200)=N'-- Audit044: safe datetime validation across audit domains.';
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51145,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID())
  THROW 51145,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SchemaVersion=1)
  THROW 51145,'Audit source prerequisite is missing.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51145,'Incomplete audit requests.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write'));
 IF @sql IS NULL OR CHARINDEX(@anchor,@sql)=0 THROW 51145,'Apply stage 044 first.',1;

 DECLARE @replacements TABLE(OldText nvarchar(600),NewText nvarchar(600));
 INSERT @replacements VALUES
 (N'DECLARE @docType int=(SELECT CONVERT(int,AfterValue) FROM @changes WHERE FieldCode=''DocumentType'');',
  N'DECLARE @docType int=(SELECT TRY_CONVERT(int,AfterValue) FROM @changes WHERE FieldCode=''DocumentType'');
  IF @docType IS NULL THROW 51101,''Invalid document type.'',1;'),
 (N'SELECT @voidType=CONVERT(int,AfterValue) FROM @changes',
  N'SELECT @voidType=TRY_CONVERT(int,AfterValue) FROM @changes'),
 (N'SELECT @voidType=CONVERT(int,c.AfterValue) FROM dbo.Network_Audit_Event',
  N'SELECT @voidType=TRY_CONVERT(int,c.AfterValue) FROM dbo.Network_Audit_Event');
 IF CHARINDEX(N'IF @voidType IS NULL OR NOT (',@sql)=0 THROW 51145,'Cancellation validation guard is missing.',1;
 IF CHARINDEX(@marker,@sql)>0
 BEGIN
  IF EXISTS(SELECT 1 FROM @replacements WHERE CHARINDEX(OldText,@sql)>0 OR
   (LEN(@sql)-LEN(REPLACE(@sql,NewText,N'')))/LEN(NewText)<>1)
   THROW 51145,'Document type marker exists but validation differs.',1;
  COMMIT;
  RETURN;
 END;
 IF EXISTS(SELECT 1 FROM @replacements WHERE (LEN(@sql)-LEN(REPLACE(@sql,OldText,N'')))/LEN(OldText)<>1 OR CHARINDEX(NewText,@sql)>0)
  THROW 51145,'Unexpected document type conversion anchors.',1;
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
 SET @sql=REPLACE(@sql,N'CREATE PROCEDURE',N'ALTER PROCEDURE');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write'));
 IF @sql IS NULL OR CHARINDEX(@marker,@sql)=0 OR CHARINDEX(@anchor,@sql)=0 OR
  EXISTS(SELECT 1 FROM @replacements WHERE CHARINDEX(OldText,@sql)>0 OR (LEN(@sql)-LEN(REPLACE(@sql,NewText,N'')))/LEN(NewText)<>1)
  THROW 51145,'Document type repair verification failed.',1;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
