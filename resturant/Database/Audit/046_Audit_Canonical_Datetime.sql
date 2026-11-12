/* SQL Server 2014. AuditValue emits ISO timestamps with seven fractional digits.
   Parse as datetime2(7), then safely round/range-check against the stored datetime.
   BOTH conversions must be TRY_CONVERT: malformed and pre-1753 values must not throw.
   Incremental after 044 (also compatible with 045); no business data changes. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51146,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51146,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@marker nvarchar(200)=N'-- Audit046: canonical seven-digit datetime validation.',
  @anchor nvarchar(200)=N'-- Audit044: safe datetime validation across audit domains.';
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51146,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID())
  THROW 51146,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SchemaVersion=1)
  THROW 51146,'Audit source prerequisite is missing.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51146,'Incomplete audit requests.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write'));
 IF @sql IS NULL OR CHARINDEX(@anchor,@sql)=0 THROW 51146,'Apply stage 044 first.',1;
 DECLARE @fixes TABLE(OldText nvarchar(200),NewText nvarchar(600),ExpectedCount int);
 INSERT @fixes VALUES
 (N'TRY_CONVERT(datetime,c.AfterValue,126)',N'(CASE WHEN TRY_CONVERT(datetime2(7),c.AfterValue,126)<=TRY_CONVERT(datetime2(7),''9999-12-31T23:59:59.9970000'',126) THEN TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) END)',60),
 (N'TRY_CONVERT(datetime,q.[QueueDateValue],126)',N'(CASE WHEN TRY_CONVERT(datetime2(7),q.[QueueDateValue],126)<=TRY_CONVERT(datetime2(7),''9999-12-31T23:59:59.9970000'',126) THEN TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),q.[QueueDateValue],126)) END)',2),
 -- @value occurs inside a dynamic SQL string in the writer, so its literal is escaped twice.
 (N'TRY_CONVERT(datetime,@value,126)',N'(CASE WHEN TRY_CONVERT(datetime2(7),@value,126)<=TRY_CONVERT(datetime2(7),''''9999-12-31T23:59:59.9970000'''',126) THEN TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),@value,126)) END)',1);
 IF CHARINDEX(@marker,@sql)>0
 BEGIN
  IF EXISTS(SELECT 1 FROM @fixes WHERE CHARINDEX(OldText,@sql)>0 OR
    (LEN(@sql)-LEN(REPLACE(@sql,NewText,N'')))/LEN(NewText)<>ExpectedCount)
   THROW 51146,'Canonical datetime marker exists but validation differs.',1;
  COMMIT;
  RETURN;
 END;
 IF EXISTS(SELECT 1 FROM @fixes WHERE (LEN(@sql)-LEN(REPLACE(@sql,OldText,N'')))/LEN(OldText)<>ExpectedCount OR CHARINDEX(NewText,@sql)>0)
  THROW 51146,'Unexpected canonical datetime conversion anchors.',1;
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
 SET @sql=REPLACE(@sql,N'CREATE PROCEDURE',N'ALTER PROCEDURE');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write'));
 IF @sql IS NULL OR CHARINDEX(@marker,@sql)=0 OR CHARINDEX(@anchor,@sql)=0 OR
  EXISTS(SELECT 1 FROM @fixes WHERE CHARINDEX(OldText,@sql)>0 OR
    (LEN(@sql)-LEN(REPLACE(@sql,NewText,N'')))/LEN(NewText)<>ExpectedCount)
  THROW 51146,'Canonical datetime repair verification failed.',1;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
