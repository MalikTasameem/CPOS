/* SQL Server 2014. Align V2 receipt authorization with the existing CPOS administrator override. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51142,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51142,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@markerAt int,@startAt int,@finishAt int,
         @needle nvarchar(max),@ending nvarchar(max),@replacement nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51142,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID())
  THROW 51142,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK)
               WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c'
                 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Finance.Receipts"]')=1)
  THROW 51142,'Receipt audit prerequisite/source mismatch.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51142,'Incomplete audit requests.',1;

 SET @sql=OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write'));
 IF @sql IS NULL THROW 51142,'Audit writer is missing.',1;
 IF CHARINDEX(N'AuditReceipts042: administrator permission override.',@sql)>0
 BEGIN
  COMMIT;
  RETURN;
 END;
 SET @markerAt=CHARINDEX(N'-- AuditReceipts041: ordinary receipts only',@sql);
 SET @needle=N'  IF NOT EXISTS(SELECT 1 FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID';
 SET @startAt=CHARINDEX(@needle,@sql,@markerAt);
 SET @ending=N'   THROW 51141,''Receipt permission denied.'',1;';
 SET @finishAt=CHARINDEX(@ending,@sql,@startAt);
 IF @markerAt=0 OR @startAt=0 OR @finishAt=0 OR CHARINDEX(@needle,@sql,@startAt+1)>0
  THROW 51142,'Unexpected receipt permission baseline.',1;

 SET @replacement=N'  -- AuditReceipts042: administrator permission override.
  IF NOT EXISTS(SELECT 1 FROM dbo.Users u WITH(HOLDLOCK) LEFT JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID
   WHERE u.User_ID=@user AND u.is_Allow=1 AND (u.isAdmin=1 OR
    ((@operation IN(''Receipt.EditOpened'',''Receipt.Updated'') AND v.Update_Receipt=1)
     OR (@operation=''Receipt.Voided'' AND v.Cancel_Receipt=1)
     OR (@operation=''Receipt.Created'' AND
       ((ISNULL(@rcLinked,0)=0 AND ((@rcType=3 AND v.Sales=1) OR (@rcType=4 AND v.Pch=1)))
        OR (@rcLinkedType=1 AND v.Sales=1) OR (@rcLinkedType=7 AND v.Pch=1) OR (@rcLinkedType=2 AND v.Exp=1)
        OR (@rcLinkedType=10 AND v.SB_Rtn=1) OR (@rcLinkedType=11 AND v.Pch_Rtn=1))))))
   THROW 51141,''Receipt permission denied.'',1;';
 SET @sql=STUFF(@sql,@startAt,@finishAt+LEN(@ending)-@startAt,@replacement);
 SET @sql=REPLACE(@sql,N'CREATE PROCEDURE',N'ALTER PROCEDURE');
 EXEC sys.sp_executesql @sql;

 SET @sql=OBJECT_DEFINITION(OBJECT_ID(N'dbo.Network_Audit_Event_Write'));
 IF CHARINDEX(N'AuditReceipts042: administrator permission override.',@sql)=0
    OR CHARINDEX(N'LEFT JOIN dbo.Users_Validations_V',@sql)=0
    OR CHARINDEX(N'u.isAdmin=1 OR',@sql)=0
  THROW 51142,'Receipt administrator permission verification failed.',1;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
