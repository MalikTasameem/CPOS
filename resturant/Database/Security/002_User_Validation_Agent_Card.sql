/* Independent Agent screen access; existing roles keep their current access.
   The existing Agents/Balance permissions and their procedures/views are unchanged. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51202,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51202,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int;
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.Security.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51202,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID())
  THROW 51202,'Close other database clients.',1;
 IF OBJECT_ID(N'dbo.Users_Validations',N'U') IS NULL THROW 51202,'User validations table is missing.',1;
 IF COL_LENGTH(N'dbo.Users_Validations',N'Open_Agent_Card') IS NULL
  ALTER TABLE dbo.Users_Validations ADD Open_Agent_Card bit NOT NULL
   CONSTRAINT DF_Users_Validations_Open_Agent_Card DEFAULT(1) WITH VALUES;
 IF NOT EXISTS(SELECT 1 FROM sys.columns c JOIN sys.default_constraints d ON d.object_id=c.default_object_id
  WHERE c.object_id=OBJECT_ID(N'dbo.Users_Validations') AND c.name=N'Open_Agent_Card'
   AND c.system_type_id=104 AND c.is_nullable=0 AND REPLACE(REPLACE(d.definition,N'(',N''),N')',N'')=N'1')
  THROW 51202,'Agent card permission schema differs from the expected default-allow bit.',1;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
