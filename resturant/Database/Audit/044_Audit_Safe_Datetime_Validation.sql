/* SQL Server 2014. Repair audit date validation across all migrated domains.
   Nested datetime2 -> datetime conversion can be evaluated by the optimizer against
   unrelated numeric change rows. Values accepted as datetime2 before 1753 then raise
   an out-of-range exception when converted to SQL datetime and abort the business save. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51144,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51144,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@anchor nvarchar(200),@marker nvarchar(200),
         @oldChange nvarchar(200),@newChange nvarchar(200),
         @oldQueue nvarchar(200),@newQueue nvarchar(200),
         @oldDynamic nvarchar(200),@newDynamic nvarchar(200),
         @oldChangeCount int,@newChangeCount int,@oldQueueCount int,@newQueueCount int,
         @oldDynamicCount int,@newDynamicCount int;
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51144,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID())
  THROW 51144,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK)
               WHERE ConfigId=1 AND SchemaVersion=1)
  THROW 51144,'Audit source prerequisite is missing.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51144,'Incomplete audit requests.',1;

 SET @sql=OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write'));
 IF @sql IS NULL THROW 51144,'Audit writer is missing.',1;
 SET @marker=N'-- Audit044: safe datetime validation across audit domains.';
 SET @anchor=N'-- AuditReceipts043: safe datetime validation.';
 SET @oldChange=N'CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))';
 SET @newChange=N'TRY_CONVERT(datetime,c.AfterValue,126)';
 SET @oldQueue=N'CONVERT(datetime,TRY_CONVERT(datetime2(7),q.[QueueDateValue],126))';
 SET @newQueue=N'TRY_CONVERT(datetime,q.[QueueDateValue],126)';
 SET @oldDynamic=N'CONVERT(datetime,TRY_CONVERT(datetime2(7),@value,126))';
 SET @newDynamic=N'TRY_CONVERT(datetime,@value,126)';

 SET @oldChangeCount=(LEN(@sql)-LEN(REPLACE(@sql,@oldChange,N'')))/LEN(@oldChange);
 SET @newChangeCount=(LEN(@sql)-LEN(REPLACE(@sql,@newChange,N'')))/LEN(@newChange);
 SET @oldQueueCount=(LEN(@sql)-LEN(REPLACE(@sql,@oldQueue,N'')))/LEN(@oldQueue);
 SET @newQueueCount=(LEN(@sql)-LEN(REPLACE(@sql,@newQueue,N'')))/LEN(@newQueue);
 SET @oldDynamicCount=(LEN(@sql)-LEN(REPLACE(@sql,@oldDynamic,N'')))/LEN(@oldDynamic);
 SET @newDynamicCount=(LEN(@sql)-LEN(REPLACE(@sql,@newDynamic,N'')))/LEN(@newDynamic);

 IF CHARINDEX(@marker,@sql)>0
 BEGIN
  IF @oldChangeCount<>0 OR @newChangeCount<>60 OR @oldQueueCount<>0 OR @newQueueCount<>2
     OR @oldDynamicCount<>0 OR @newDynamicCount<>1
   THROW 51144,'Audit datetime marker exists but validation is not safe.',1;
  COMMIT;
  RETURN;
 END;
 IF CHARINDEX(@anchor,@sql)=0 THROW 51144,'Apply stage 043 first.',1;
 IF NOT((@oldChangeCount=40 AND @newChangeCount=20) OR (@oldChangeCount=0 AND @newChangeCount=60))
  THROW 51144,'Unexpected audit change datetime validation count.',1;
 IF NOT((@oldQueueCount=2 AND @newQueueCount=0) OR (@oldQueueCount=0 AND @newQueueCount=2))
  THROW 51144,'Unexpected audit queue datetime validation count.',1;
 IF NOT((@oldDynamicCount=1 AND @newDynamicCount=0) OR (@oldDynamicCount=0 AND @newDynamicCount=1))
  THROW 51144,'Unexpected audit dynamic datetime validation count.',1;

 SET @sql=REPLACE(@sql,@oldChange,@newChange);
 SET @sql=REPLACE(@sql,@oldQueue,@newQueue);
 SET @sql=REPLACE(@sql,@oldDynamic,@newDynamic);
 SET @sql=STUFF(@sql,CHARINDEX(@anchor,@sql),0,@marker+CHAR(13)+CHAR(10));
 SET @sql=REPLACE(@sql,N'CREATE PROCEDURE',N'ALTER PROCEDURE');
 EXEC sys.sp_executesql @sql;

 SET @sql=OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write'));
 IF @sql IS NULL OR CHARINDEX(@marker,@sql)=0 OR CHARINDEX(@anchor,@sql)=0
    OR CHARINDEX(@oldChange,@sql)>0 OR CHARINDEX(@oldQueue,@sql)>0 OR CHARINDEX(@oldDynamic,@sql)>0
    OR (LEN(@sql)-LEN(REPLACE(@sql,@newChange,N'')))/LEN(@newChange)<>60
    OR (LEN(@sql)-LEN(REPLACE(@sql,@newQueue,N'')))/LEN(@newQueue)<>2
    OR (LEN(@sql)-LEN(REPLACE(@sql,@newDynamic,N'')))/LEN(@newDynamic)<>1
  THROW 51144,'Audit datetime repair verification failed.',1;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
