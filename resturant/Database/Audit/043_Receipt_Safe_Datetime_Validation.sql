/* SQL Server 2014. Repair receipt audit date validation without changing business data.
   The previous nested datetime2 -> datetime conversion could be evaluated by the optimizer
   against unrelated numeric change rows. Text such as 1000 was accepted as datetime2 year
   1000 and then raised an out-of-range exception when converted to SQL datetime. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51143,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51143,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@receipt nvarchar(max),@startAt int,@endAt int,
         @old nvarchar(200),@new nvarchar(200),@oldCount int,@newCount int;
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51143,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID())
  THROW 51143,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK)
               WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Finance.Receipts"]')=1)
  THROW 51143,'Receipt audit prerequisite is missing.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51143,'Incomplete audit requests.',1;

 SET @sql=OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write'));
 IF @sql IS NULL THROW 51143,'Audit writer is missing.',1;
 IF CHARINDEX(N'AuditReceipts043: safe datetime validation.',@sql)>0
 BEGIN
  COMMIT;
  RETURN;
 END;
 SET @startAt=CHARINDEX(N'-- AuditReceipts041: ordinary receipts only',@sql);
 SET @endAt=CHARINDEX(N'INSERT dbo.Network_Audit_Event(EventId',@sql,@startAt);
 IF @startAt=0 OR @endAt=0 OR @endAt<=@startAt THROW 51143,'Unexpected receipt writer baseline.',1;

 SET @old=N'CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))';
 SET @new=N'TRY_CONVERT(datetime,c.AfterValue,126)';
 SET @receipt=SUBSTRING(@sql,@startAt,@endAt-@startAt);
 SET @oldCount=(LEN(@receipt)-LEN(REPLACE(@receipt,@old,N'')))/LEN(@old);
 SET @newCount=(LEN(@receipt)-LEN(REPLACE(@receipt,@new,N'')))/LEN(@new);
 IF @oldCount=20 AND @newCount=0
  SET @receipt=REPLACE(@receipt,@old,@new);
 ELSE IF NOT (@oldCount=0 AND @newCount=20)
  THROW 51143,'Unexpected receipt datetime validation count.',1;
 SET @receipt=N'-- AuditReceipts043: safe datetime validation.'+CHAR(13)+CHAR(10)+@receipt;
 SET @sql=STUFF(@sql,@startAt,@endAt-@startAt,@receipt);
 SET @sql=REPLACE(@sql,N'CREATE PROCEDURE',N'ALTER PROCEDURE');
 EXEC sys.sp_executesql @sql;

 SET @sql=OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write'));
 SET @startAt=CHARINDEX(N'-- AuditReceipts043: safe datetime validation.',@sql);
 SET @endAt=CHARINDEX(N'INSERT dbo.Network_Audit_Event(EventId',@sql,@startAt);
 SET @receipt=SUBSTRING(@sql,@startAt,@endAt-@startAt);
 IF @startAt=0 OR @endAt=0 OR CHARINDEX(@old,@receipt)>0
    OR (LEN(@receipt)-LEN(REPLACE(@receipt,@new,N'')))/LEN(@new)<>20
  THROW 51143,'Receipt datetime repair verification failed.',1;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
