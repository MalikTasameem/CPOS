/* SQL Server 2014. Salary batch audit; CODE ONLY, not deployed/live-tested.
   The business salary formula, loan handling, cancellation flags and accounting timing stay unchanged. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51139,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51139,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51139,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51139,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="People.Cards"]')=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.ItemLifecycle"]')=1) THROW 51139,'Unexpected source or prerequisite 038.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51139,'Incomplete audit requests.',1;
 IF EXISTS(SELECT 1 FROM sys.triggers WHERE parent_id=OBJECT_ID('dbo.Agents_Balance_MV') AND is_disabled=0) THROW 51139,'Review salary table triggers.',1;
 IF ISNULL(CONVERT(varchar(64),HASHBYTES('SHA2_256',CONVERT(varbinary(max),OBJECT_DEFINITION(OBJECT_ID('dbo.EMP_Insert_Salaries')))),2),'')<>'EF2B9B7A5340F03A7C9374A940F16B40F0337156D413C25D467681EA5EA04EE2'
  OR ISNULL(CONVERT(varchar(64),HASHBYTES('SHA2_256',CONVERT(varbinary(max),OBJECT_DEFINITION(OBJECT_ID('dbo.Agents_Balance_MV_Cancel')))),2),'')<>'3A9493E2D8EE1C2DFA27D8BB15D3EF8125483360BCFC1A2B1BBB3FD7DEAB8348' THROW 51139,'Salary/cancellation baseline changed.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Salary.BatchPosted',@sql)>0 OR CHARINDEX('Agent.Created',@sql)=0 OR CHARINDEX('Item.FullSaved',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51139,'Unexpected begin baseline; prerequisite 038 is required.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode=''Salary.BatchPosted'' AND @DomainCode=''People.Salaries'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Salary.BatchPosted',@sql)>0 OR CHARINDEX('Agent.Created',@sql)=0 OR CHARINDEX('Item.FullSaved',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51139,'Unexpected writer baseline; prerequisite 038 is required.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation=''Salary.BatchPosted'' AND @domain=''People.Salaries'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51139,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation=''Salary.BatchPosted'') OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51139,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditSalaryBatch039: preserve EMP_Insert_Salaries and cancellation flag semantics.
 IF @operation=''Salary.BatchPosted''
 BEGIN
  DECLARE @salPeriod nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),@salYear int,@salMonth int;
  SET @salYear=TRY_CONVERT(int,LEFT(@salPeriod,4)); SET @salMonth=TRY_CONVERT(int,RIGHT(@salPeriod,2));
  IF LEN(@salPeriod)<>7 OR SUBSTRING(@salPeriod,5,1)<>''-'' OR ISNULL(@salYear,0) NOT BETWEEN 1753 AND 9998 OR ISNULL(@salMonth,0) NOT BETWEEN 1 AND 12
    OR @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>''SalaryBatch'' OR @EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'')<>''Balances''
    OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0 OR @EventXml.value(''count(/Event/Document)'',''int'')<>1 OR @EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)'')<>@salPeriod THROW 51139,''Invalid salary batch identity.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=''SalaryBatch'' AND EntityId=@salPeriod) THROW 51139,''Salary request mismatch.'',1;
  DECLARE @salFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40),O tinyint);
  INSERT @salFields VALUES
   (''SalaryDocument'',''SalaryAGID'',''Integer'',''int'',1),
   (''SalaryDocument'',''SalaryDate'',''DateTime'',''datetime'',1),
   (''SalaryDocument'',''SalaryYear'',''Integer'',''int'',1),
   (''SalaryDocument'',''SalaryMonth'',''Integer'',''int'',1),
   (''SalaryDocument'',''SalaryReceiptTitle'',''String'',''nvarchar(550)'',1),
   (''SalaryDocument'',''SalaryTotal'',''Decimal'',''numeric(18,3)'',1),
   (''SalaryDocument'',''SalaryDiscount'',''Decimal'',''numeric(18,3)'',1),
   (''SalaryDocument'',''SalaryPure'',''Decimal'',''numeric(18,3)'',1),
   (''SalaryDocument'',''SalaryDebit'',''Decimal'',''numeric(18,3)'',1),
   (''SalaryDocument'',''SalaryCredit'',''Decimal'',''numeric(18,3)'',1),
   (''SalaryDocument'',''SalaryBalance'',''Decimal'',''numeric(18,3)'',1),
   (''SalaryDocument'',''SalaryUserID'',''Integer'',''int'',1),
   (''SalaryDocument'',''SalaryisVoid'',''Integer'',''int'',1),
   (''SalaryDocument'',''SalaryisDepended'',''Integer'',''int'',1),
   (''SalaryDocument'',''SalaryBsTypeID'',''Integer'',''int'',1),
   (''SalaryDocument'',''SalaryJournalId'',''Integer'',''int'',1),
   (''SalaryDocument'',''SalaryPostedAt'',''DateTime'',''datetime'',1),
   (''SalaryDocument'',''SalaryPostedBy'',''Integer'',''int'',1),
   (''SalaryDocument'',''SalaryOriginalJournalId'',''Integer'',''int'',1),
   (''SalaryDocument'',''SalaryLastReversalJournalId'',''Integer'',''int'',1),
   (''SalaryDocument'',''SalaryNeedRepost'',''Boolean'',''bit'',1),
   (''SalaryDocument'',''SalaryEditVersion'',''Integer'',''int'',1),
   (''SalaryDocument'',''SalaryEditOpenedAt'',''DateTime'',''datetime'',1),
   (''SalaryDocument'',''SalaryEditOpenedBy'',''Integer'',''int'',1),
   (''SalaryDocument'',''SalaryLastRepostAt'',''DateTime'',''datetime'',1),
   (''SalaryDocument'',''SalaryLastRepostBy'',''Integer'',''int'',1),
   (''SalaryDocument'',''SalaryVoidAt'',''DateTime'',''datetime'',1),
   (''SalaryDocument'',''SalaryVoidBy'',''Integer'',''int'',1),
   (''SalaryDocument'',''SalaryVoidReason'',''String'',''nvarchar(500)'',1),
   (''SalaryDocument'',''SalaryVoidReversalJournalId'',''Integer'',''int'',1),
   (''SalaryDocument'',''SalaryNeedCancelReverse'',''Boolean'',''bit'',1),
   (''SalaryEmployee'',''SalaryEmployeeName'',''String'',''nvarchar(500)'',1),
   (''SalaryEmployee'',''SalaryBaseAmount'',''Decimal'',''money'',1),
   (''SalaryEmployee'',''SalaryEmployeeType'',''Integer'',''int'',1),
   (''SalaryEmployee'',''SalaryEmployeePaused'',''Boolean'',''bit'',1),
   (''SalaryEmployee'',''SalaryEmployeeStarted'',''Date'',''date'',1),
   (''SalaryAdjustment'',''SalaryAdjCreatedAt'',''DateTime'',''datetime'',1),
   (''SalaryAdjustment'',''SalaryAdjBusinessDate'',''DateTime'',''datetime'',1),
   (''SalaryAdjustment'',''SalaryAdjYear'',''Integer'',''int'',1),
   (''SalaryAdjustment'',''SalaryAdjMonth'',''Integer'',''int'',1),
   (''SalaryAdjustment'',''SalaryAdjEmployeeId'',''Integer'',''int'',1),
   (''SalaryAdjustment'',''SalaryAdjType'',''Integer'',''int'',1),
   (''SalaryAdjustment'',''SalaryAdjAmount'',''Decimal'',''numeric(18,3)'',1),
   (''SalaryAdjustment'',''SalaryAdjNotes'',''String'',''nvarchar(max)'',1),
   (''SalaryAdjustment'',''SalaryAdjVoided'',''Integer'',''int'',1),
   (''SalaryAdjustment'',''SalaryAdjUserId'',''Integer'',''bigint'',1),
   (''SalaryBatch'',''SalaryYear'',''Integer'',''int'',3),
   (''SalaryBatch'',''SalaryMonth'',''Integer'',''int'',3),
   (''SalaryBatch'',''SalaryDate'',''DateTime'',''datetime'',3),
   (''SalaryBatch'',''SalaryNewCount'',''Integer'',''int'',3),
   (''SalaryBatch'',''SalaryCanceledCount'',''Integer'',''int'',3),
   (''SalaryBatch'',''SalaryTotal'',''Decimal'',''numeric(18,3)'',3);
  IF EXISTS(SELECT 1 FROM @changes c WHERE NOT EXISTS(SELECT 1 FROM @salFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType AND f.O=c.ValueOrigin)) THROW 51139,''Unexpected salary field.'',1;
  IF EXISTS(SELECT EntityType,EntityId,FieldCode FROM @changes GROUP BY EntityType,EntityId,FieldCode HAVING COUNT(*)<>1)
   OR EXISTS(SELECT c.EntityType,c.EntityId FROM @changes c GROUP BY c.EntityType,c.EntityId HAVING COUNT(*)<>(SELECT COUNT(*) FROM @salFields f WHERE f.E=c.EntityType)) THROW 51139,''Incomplete salary snapshot.'',1;
  IF @EventOrdinal=1 AND ((SELECT COUNT(*) FROM @changes)<>6 OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>''SalaryBatch'')) THROW 51139,''Salary batch summary required first.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE (EntityType=''SalaryBatch'' AND (EntityId<>@salPeriod OR @EventOrdinal<>1 OR BeforeState<>1 OR AfterState<>3))
   OR (EntityType<>''SalaryBatch'' AND (ISNULL(TRY_CONVERT(int,EntityId),0)<=0 OR AfterState NOT IN(2,3)))
   OR (EntityType=''SalaryDocument'' AND BeforeState NOT IN(1,2,3))
   OR (EntityType IN(''SalaryEmployee'',''SalaryAdjustment'') AND (BeforeState NOT IN(2,3) OR BeforeState<>AfterState OR ISNULL(DATALENGTH(BeforeValue),-1)<>ISNULL(DATALENGTH(AfterValue),-1) OR ISNULL(BeforeValue,N'''') COLLATE Latin1_General_100_BIN2<>ISNULL(AfterValue,N'''') COLLATE Latin1_General_100_BIN2))) THROW 51139,''Invalid salary states.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''SalaryBatch'' AND
   ((FieldCode=''SalaryYear'' AND TRY_CONVERT(int,AfterValue)<>@salYear) OR (FieldCode=''SalaryMonth'' AND TRY_CONVERT(int,AfterValue)<>@salMonth)
    OR (FieldCode IN(''SalaryNewCount'',''SalaryCanceledCount'') AND (TRY_CONVERT(int,AfterValue) IS NULL OR TRY_CONVERT(int,AfterValue)<0)))) THROW 51139,''Salary summary mismatch.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''SalaryDocument'' AND BeforeState<>1 AND FieldCode NOT IN(''SalaryisVoid'',''SalaryVoidAt'',''SalaryVoidBy'',''SalaryVoidReason'',''SalaryNeedCancelReverse'',''SalaryNeedRepost'') AND
    (BeforeState<>AfterState OR ISNULL(DATALENGTH(BeforeValue),-1)<>ISNULL(DATALENGTH(AfterValue),-1) OR ISNULL(BeforeValue,N'''') COLLATE Latin1_General_100_BIN2<>ISNULL(AfterValue,N'''') COLLATE Latin1_General_100_BIN2)) THROW 51139,''Unexpected previous salary mutation.'',1;
  IF EXISTS(SELECT 1 FROM @changes c JOIN dbo.Agents_Balance_MV b WITH(HOLDLOCK) ON b.T_ID=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''SalaryDocument'' AND (b.BsType_ID<>5 OR b.[Year]<>@salYear OR b.[Month]<>@salMonth)) THROW 51139,''Salary row outside period.'',1;
  IF EXISTS(SELECT EntityId FROM @changes WHERE EntityType=''SalaryDocument'' GROUP BY EntityId HAVING MIN(BeforeState)=1 AND MAX(BeforeState)<>1) THROW 51139,''Mixed new salary states.'',1;
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.EntityType=''SalaryDocument'' AND ((c.FieldCode=''SalaryisDepended'' AND c.AfterValue<>''1'') OR (c.FieldCode=''SalaryisVoid'' AND c.AfterValue<>CASE WHEN c.BeforeState=1 THEN ''0'' ELSE ''1'' END))) THROW 51139,''Invalid salary lifecycle outcome.'',1;
  IF EXISTS(SELECT 1 FROM @changes c JOIN dbo.Agents a WITH(HOLDLOCK) ON a.AG_ID=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''SalaryEmployee'' AND (a.Type_ID<>4 OR a.is_Emp_Pause<>0)) THROW 51139,''Invalid employee basis.'',1;
  IF EXISTS(SELECT 1 FROM @changes c JOIN dbo.Others_Values o WITH(HOLDLOCK) ON o.T_ID=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''SalaryAdjustment'' AND (o.[YEAR]<>@salYear OR o.[MONTH]<>@salMonth OR o.isVoid<>0)) THROW 51139,''Adjustment outside salary period.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[Agents_Balance_MV] p WITH(HOLDLOCK) ON p.[T_ID]=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''SalaryDocument'' AND (p.[T_ID] IS NULL OR (c.FieldCode=''SalaryAGID'' AND ((c.AfterState=2 AND p.[AG_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[AG_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[AG_ID])))) OR
    (c.FieldCode=''SalaryDate'' AND ((c.AfterState=2 AND p.[Date] IS NOT NULL) OR (c.AfterState=3 AND (p.[Date] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[Date])))) OR
    (c.FieldCode=''SalaryYear'' AND ((c.AfterState=2 AND p.[Year] IS NOT NULL) OR (c.AfterState=3 AND (p.[Year] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Year])))) OR
    (c.FieldCode=''SalaryMonth'' AND ((c.AfterState=2 AND p.[Month] IS NOT NULL) OR (c.AfterState=3 AND (p.[Month] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Month])))) OR
    (c.FieldCode=''SalaryReceiptTitle'' AND ((c.AfterState=2 AND p.[Receipt_Title] IS NOT NULL) OR (c.AfterState=3 AND (p.[Receipt_Title] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[Receipt_Title])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Receipt_Title]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''SalaryTotal'' AND ((c.AfterState=2 AND p.[Total] IS NOT NULL) OR (c.AfterState=3 AND (p.[Total] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Total])))) OR
    (c.FieldCode=''SalaryDiscount'' AND ((c.AfterState=2 AND p.[Discount] IS NOT NULL) OR (c.AfterState=3 AND (p.[Discount] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Discount])))) OR
    (c.FieldCode=''SalaryPure'' AND ((c.AfterState=2 AND p.[Pure] IS NOT NULL) OR (c.AfterState=3 AND (p.[Pure] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Pure])))) OR
    (c.FieldCode=''SalaryDebit'' AND ((c.AfterState=2 AND p.[Debit] IS NOT NULL) OR (c.AfterState=3 AND (p.[Debit] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Debit])))) OR
    (c.FieldCode=''SalaryCredit'' AND ((c.AfterState=2 AND p.[Credit] IS NOT NULL) OR (c.AfterState=3 AND (p.[Credit] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Credit])))) OR
    (c.FieldCode=''SalaryBalance'' AND ((c.AfterState=2 AND p.[Balance] IS NOT NULL) OR (c.AfterState=3 AND (p.[Balance] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Balance])))) OR
    (c.FieldCode=''SalaryUserID'' AND ((c.AfterState=2 AND p.[User_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[User_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[User_ID])))) OR
    (c.FieldCode=''SalaryisVoid'' AND ((c.AfterState=2 AND p.[isVoid] IS NOT NULL) OR (c.AfterState=3 AND (p.[isVoid] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isVoid])))) OR
    (c.FieldCode=''SalaryisDepended'' AND ((c.AfterState=2 AND p.[isDepended] IS NOT NULL) OR (c.AfterState=3 AND (p.[isDepended] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isDepended])))) OR
    (c.FieldCode=''SalaryBsTypeID'' AND ((c.AfterState=2 AND p.[BsType_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[BsType_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[BsType_ID])))) OR
    (c.FieldCode=''SalaryJournalId'' AND ((c.AfterState=2 AND p.[JournalId] IS NOT NULL) OR (c.AfterState=3 AND (p.[JournalId] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[JournalId])))) OR
    (c.FieldCode=''SalaryPostedAt'' AND ((c.AfterState=2 AND p.[PostedAt] IS NOT NULL) OR (c.AfterState=3 AND (p.[PostedAt] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[PostedAt])))) OR
    (c.FieldCode=''SalaryPostedBy'' AND ((c.AfterState=2 AND p.[PostedBy] IS NOT NULL) OR (c.AfterState=3 AND (p.[PostedBy] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[PostedBy])))) OR
    (c.FieldCode=''SalaryOriginalJournalId'' AND ((c.AfterState=2 AND p.[OriginalJournalId] IS NOT NULL) OR (c.AfterState=3 AND (p.[OriginalJournalId] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[OriginalJournalId])))) OR
    (c.FieldCode=''SalaryLastReversalJournalId'' AND ((c.AfterState=2 AND p.[LastReversalJournalId] IS NOT NULL) OR (c.AfterState=3 AND (p.[LastReversalJournalId] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[LastReversalJournalId])))) OR
    (c.FieldCode=''SalaryNeedRepost'' AND ((c.AfterState=2 AND p.[NeedRepost] IS NOT NULL) OR (c.AfterState=3 AND (p.[NeedRepost] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[NeedRepost])))) OR
    (c.FieldCode=''SalaryEditVersion'' AND ((c.AfterState=2 AND p.[EditVersion] IS NOT NULL) OR (c.AfterState=3 AND (p.[EditVersion] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[EditVersion])))) OR
    (c.FieldCode=''SalaryEditOpenedAt'' AND ((c.AfterState=2 AND p.[EditOpenedAt] IS NOT NULL) OR (c.AfterState=3 AND (p.[EditOpenedAt] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[EditOpenedAt])))) OR
    (c.FieldCode=''SalaryEditOpenedBy'' AND ((c.AfterState=2 AND p.[EditOpenedBy] IS NOT NULL) OR (c.AfterState=3 AND (p.[EditOpenedBy] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[EditOpenedBy])))) OR
    (c.FieldCode=''SalaryLastRepostAt'' AND ((c.AfterState=2 AND p.[LastRepostAt] IS NOT NULL) OR (c.AfterState=3 AND (p.[LastRepostAt] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[LastRepostAt])))) OR
    (c.FieldCode=''SalaryLastRepostBy'' AND ((c.AfterState=2 AND p.[LastRepostBy] IS NOT NULL) OR (c.AfterState=3 AND (p.[LastRepostBy] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[LastRepostBy])))) OR
    (c.FieldCode=''SalaryVoidAt'' AND ((c.AfterState=2 AND p.[VoidAt] IS NOT NULL) OR (c.AfterState=3 AND (p.[VoidAt] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[VoidAt])))) OR
    (c.FieldCode=''SalaryVoidBy'' AND ((c.AfterState=2 AND p.[VoidBy] IS NOT NULL) OR (c.AfterState=3 AND (p.[VoidBy] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[VoidBy])))) OR
    (c.FieldCode=''SalaryVoidReason'' AND ((c.AfterState=2 AND p.[VoidReason] IS NOT NULL) OR (c.AfterState=3 AND (p.[VoidReason] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[VoidReason])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[VoidReason]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''SalaryVoidReversalJournalId'' AND ((c.AfterState=2 AND p.[VoidReversalJournalId] IS NOT NULL) OR (c.AfterState=3 AND (p.[VoidReversalJournalId] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[VoidReversalJournalId])))) OR
    (c.FieldCode=''SalaryNeedCancelReverse'' AND ((c.AfterState=2 AND p.[NeedCancelReverse] IS NOT NULL) OR (c.AfterState=3 AND (p.[NeedCancelReverse] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[NeedCancelReverse])))))) THROW 51139,''Stored SalaryDocument snapshot mismatch.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[Agents] p WITH(HOLDLOCK) ON p.[AG_ID]=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''SalaryEmployee'' AND (p.[AG_ID] IS NULL OR (c.FieldCode=''SalaryEmployeeName'' AND ((c.AfterState=2 AND p.[Ag_name] IS NOT NULL) OR (c.AfterState=3 AND (p.[Ag_name] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[Ag_name])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Ag_name]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''SalaryBaseAmount'' AND ((c.AfterState=2 AND p.[AG_Salary] IS NOT NULL) OR (c.AfterState=3 AND (p.[AG_Salary] IS NULL OR TRY_CONVERT(money,c.AfterValue) IS NULL OR TRY_CONVERT(money,c.AfterValue)<>p.[AG_Salary])))) OR
    (c.FieldCode=''SalaryEmployeeType'' AND ((c.AfterState=2 AND p.[Type_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Type_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Type_ID])))) OR
    (c.FieldCode=''SalaryEmployeePaused'' AND ((c.AfterState=2 AND p.[is_Emp_Pause] IS NOT NULL) OR (c.AfterState=3 AND (p.[is_Emp_Pause] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[is_Emp_Pause])))) OR
    (c.FieldCode=''SalaryEmployeeStarted'' AND ((c.AfterState=2 AND p.[DATE_START] IS NOT NULL) OR (c.AfterState=3 AND (p.[DATE_START] IS NULL OR TRY_CONVERT(date,c.AfterValue) IS NULL OR TRY_CONVERT(date,c.AfterValue)<>p.[DATE_START])))))) THROW 51139,''Stored SalaryEmployee snapshot mismatch.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[Others_Values] p WITH(HOLDLOCK) ON p.[T_ID]=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''SalaryAdjustment'' AND (p.[T_ID] IS NULL OR (c.FieldCode=''SalaryAdjCreatedAt'' AND ((c.AfterState=2 AND p.[Date] IS NOT NULL) OR (c.AfterState=3 AND (p.[Date] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[Date])))) OR
    (c.FieldCode=''SalaryAdjBusinessDate'' AND ((c.AfterState=2 AND p.[DateOF_Order] IS NOT NULL) OR (c.AfterState=3 AND (p.[DateOF_Order] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[DateOF_Order])))) OR
    (c.FieldCode=''SalaryAdjYear'' AND ((c.AfterState=2 AND p.[YEAR] IS NOT NULL) OR (c.AfterState=3 AND (p.[YEAR] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[YEAR])))) OR
    (c.FieldCode=''SalaryAdjMonth'' AND ((c.AfterState=2 AND p.[MONTH] IS NOT NULL) OR (c.AfterState=3 AND (p.[MONTH] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[MONTH])))) OR
    (c.FieldCode=''SalaryAdjEmployeeId'' AND ((c.AfterState=2 AND p.[AG_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[AG_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[AG_ID])))) OR
    (c.FieldCode=''SalaryAdjType'' AND ((c.AfterState=2 AND p.[ValType] IS NOT NULL) OR (c.AfterState=3 AND (p.[ValType] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[ValType])))) OR
    (c.FieldCode=''SalaryAdjAmount'' AND ((c.AfterState=2 AND p.[Value] IS NOT NULL) OR (c.AfterState=3 AND (p.[Value] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Value])))) OR
    (c.FieldCode=''SalaryAdjNotes'' AND ((c.AfterState=2 AND p.[About] IS NOT NULL) OR (c.AfterState=3 AND (p.[About] IS NULL OR DATALENGTH(c.AfterValue)<>DATALENGTH(CONVERT(nvarchar(max),p.[About])) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[About]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''SalaryAdjVoided'' AND ((c.AfterState=2 AND p.[isVoid] IS NOT NULL) OR (c.AfterState=3 AND (p.[isVoid] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isVoid])))) OR
    (c.FieldCode=''SalaryAdjUserId'' AND ((c.AfterState=2 AND p.[User_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[User_ID] IS NULL OR TRY_CONVERT(bigint,c.AfterValue) IS NULL OR TRY_CONVERT(bigint,c.AfterValue)<>p.[User_ID])))))) THROW 51139,''Stored SalaryAdjustment snapshot mismatch.'',1;
 END;
'+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="People.Salaries"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="People.Salaries"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
