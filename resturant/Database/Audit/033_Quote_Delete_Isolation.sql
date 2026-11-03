/* Approved quote-only correction. SQL Server 2014. CODE ONLY: not installed.
   Removes the unrelated sales-components deletion, preserving existing quote/recalculation behavior. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51133,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51133,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@expected nvarchar(max)=N'CREATE PROCEDURE [dbo].[ViewBill_Contents_Delete_IM]
	@T_ID int
AS
BEGIN TRY
BEGIN TRANSACTION
DECLARE @ST_QTY NUMERIC(18,3),@U_Cargo FLOAT,@U_ID INT,@IM_ID INT,@ST_ID INT,@D_Vaild NVARCHAR(50),@Bill_T_ID INT

SELECT @U_Cargo = U_Cargo ,@IM_ID=IM_ID, @ST_ID=ST_ID,@D_Vaild=D_Vaild,@ST_QTY=ST_QTY , @Bill_T_ID = Bill_T_ID FROM ViewBill_Contents where T_ID = @T_ID
DELETE FROM ViewBill_Contents  where T_ID = @T_ID
DELETE FROM SB_IM_Components WHERE IM_T_ID = @T_ID

EXEC [Recalculate_Agent_Bill_Total] @Bill_T_ID
 
COMMIT
END TRY

BEGIN CATCH
IF @@TRANCOUNT > 0
raiserror(''[ViewBill_Contents_Delete_IM]'',16,1,''خطأ'')
ROLLBACK
END CATCH';
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51133,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51133,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c') THROW 51133,'Unexpected source.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.ViewBill_Contents_Delete_IM'));
 IF @sql IS NULL THROW 51133,'Missing quote delete procedure.',1;
 SET @sql=LTRIM(RTRIM(REPLACE(REPLACE(@sql,CHAR(13),N''),N'ALTER PROCEDURE',N'CREATE PROCEDURE')));
 WHILE RIGHT(@sql,1) IN(CHAR(10),CHAR(9),N' ') SET @sql=LEFT(@sql,LEN(@sql+N'#')-2);
 IF @sql COLLATE Latin1_General_100_BIN2<>@expected COLLATE Latin1_General_100_BIN2 THROW 51133,'Quote delete baseline changed; review before applying.',1;
 SET @sql=REPLACE(@sql,N'DELETE FROM SB_IM_Components WHERE IM_T_ID = @T_ID',N'-- QuoteDelete033: quote IDs are not sales-line IDs. Do not touch SB_IM_Components.');
 SET @sql=REPLACE(@sql,N'CREATE PROCEDURE',N'ALTER PROCEDURE');
 EXEC sys.sp_executesql @sql;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
