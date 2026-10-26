/* Approved target-only correction. SQL Server 2014. No formula/payment changes. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51102,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51102,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@definition nvarchar(max),@anchor nvarchar(300);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51103,'Deployment busy.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c') THROW 51102,'Unexpected source.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51102,'Close other database clients.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51102,'Incomplete audit requests.',1;
 SET @definition=OBJECT_DEFINITION(OBJECT_ID('dbo.AG_Balance_DependingUpdatedBill'));
 SET @anchor=N'SELECT @T_ID = Bill_T_ID FROM SB_Contents WHERE T_ID = @T_ID';
 IF @definition IS NULL OR CHARINDEX(@anchor,@definition)=0 OR CHARINDEX('AuditTarget013',@definition)>0
  THROW 51102,'Unexpected baseline or correction already installed.',1;
 IF CHARINDEX(@anchor,@definition,CHARINDEX(@anchor,@definition)+LEN(@anchor))>0 THROW 51102,'Ambiguous baseline.',1;
 SET @definition=REPLACE(REPLACE(@definition,'CREATE PROCEDURE','ALTER PROCEDURE'),@anchor,
  N'-- AuditTarget013: @T_ID is the invoice ID supplied by the caller; never reinterpret it as a sales-line ID.');
 EXEC sys.sp_executesql @definition;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
