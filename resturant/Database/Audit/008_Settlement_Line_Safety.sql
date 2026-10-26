/* Settlement V2 line safety. SQL Server 2014. No tables/columns/business rows changed.
   Install once, with all other clients closed. Existing audit activation is preserved. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51102,'Approved CPOS_ACCOUNTING test database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51102,'Cannot verify closed sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51103,'Deployment is busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID())
  THROW 51102,'Close other database sessions before installing 008.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND SchemaVersion=1
  AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c') THROW 51102,'Unexpected audit source.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51102,'Resolve incomplete requests first.',1;
 IF OBJECT_ID('dbo.ST_Settlement_Line_AssertWritable') IS NOT NULL THROW 51102,'008 already installed; do not overwrite.',1;

 EXEC(N'CREATE PROCEDURE dbo.ST_Settlement_Line_AssertWritable
 @Bill_T_ID int,@User_ID int,@SessionId uniqueidentifier
 AS
 BEGIN
  SET NOCOUNT ON;
  IF @@TRANCOUNT=0 OR XACT_STATE()<>1 THROW 51100,''Line validation requires the business transaction.'',1;
  DECLARE @dep int,@void int,@editBy int,@editAt datetime,@event varchar(64),@actor int,@session uniqueidentifier;
  SELECT @dep=isDepended,@void=isVoid,@editBy=EditOpenedBy,@editAt=EditOpenedAt
   FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@Bill_T_ID AND BsType_ID=39;
  IF @dep IS NULL OR ISNULL(@void,1)<>0 THROW 51106,N''مستند التسوية غير موجود أو ملغى.'',1;
  IF @dep NOT IN(0,1) THROW 51106,N''حالة اعتماد التسوية غير صالحة.'',1;
  IF @SessionId IS NULL OR NOT EXISTS(
   SELECT 1 FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID
   WHERE u.User_ID=@User_ID AND u.is_Allow=1 AND ((@dep=0 AND v.Update_IM_QTY=1) OR (@dep=1 AND v.Pch_Update=1)))
   THROW 51107,N''لا تملك صلاحية تعديل سطور التسوية في حالتها الحالية.'',1;
  IF @dep=1
  BEGIN
   -- Accounting edit metadata is historical: use the latest V2 edit lifecycle as well.
   SELECT TOP(1) @event=EventCode,@actor=ActorUserId,@session=SessionId
    FROM dbo.Network_Audit_Event WITH(HOLDLOCK)
    WHERE SourceId=(SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1)
     AND EntityType=''Settlement'' AND EntityId=CONVERT(nvarchar(128),@Bill_T_ID)
     AND EventCode IN(''Settlement.EditOpened'',''Settlement.EditFinished'',''Settlement.Voided'')
    ORDER BY LocalSequence DESC;
   IF ISNULL(@editBy,0)<>@User_ID OR @editAt IS NULL OR ISNULL(@event,'''')<>''Settlement.EditOpened''
     OR ISNULL(@actor,0)<>@User_ID OR @session IS NULL OR @session<>@SessionId
    THROW 51107,N''افتح تعديل المستند المعتمد بهذه الجلسة أولًا؛ ربما أنهى مستخدم آخر التعديل أو تغيرت حالة المستند.'',1;
  END;
 END');

 -- Preserve the existing closed catalogue and extend it for a real before/after line update.
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 IF @sql IS NULL OR CHARINDEX('Settlement.EditFinished',@sql)=0 OR CHARINDEX('Settlement.LineUpdated',@sql)>0
  THROW 51102,'Unexpected request catalogue baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),'''Settlement.LineDeleted''','''Settlement.LineDeleted'',''Settlement.LineUpdated''');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 IF @sql IS NULL OR CHARINDEX('Settlement.LineUpdated',@sql)>0 THROW 51102,'Unexpected event catalogue baseline.',1;
 SET @sql=REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE');
 -- IN lists only: keep the deletion-specific value-state check unchanged.
 SET @sql=REPLACE(@sql,'''Settlement.LineAdded'',''Settlement.LineDeleted''','''Settlement.LineAdded'',''Settlement.LineDeleted'',''Settlement.LineUpdated''');
 SET @old=N'FieldCode IN(''StoredQuantity'',''DisplayedQuantity'')';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected quantity field catalogue.',1;
 SET @sql=REPLACE(@sql,@old,N'FieldCode IN(''StoredQuantity'',''DisplayedQuantity'',''StockQuantity'')');
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51102,'Unexpected event writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'IF @operation=''Settlement.LineUpdated'' AND EXISTS(SELECT 1 FROM @changes WHERE EntityType=''SettlementLine'' AND (BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3)))
   THROW 51101,''Line update requires real before and after values.'',1;
 INSERT dbo.Network_Audit_Event(EventId');
 EXEC sys.sp_executesql @sql;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
