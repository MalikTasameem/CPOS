/* SQL Server 2014. CODE ONLY. Immediate split-table lifecycle.
   Requires the sequential 031-039 package, including People.Salaries (039).
   Preserves existing movement, stock and confirmation formulas; no receipts. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51140,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51140,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51140,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51140,'Close database clients first.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Documents.Workflows"]')=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="People.Salaries"]')=1) THROW 51140,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51140,'Incomplete requests.',1;
 IF EXISTS(SELECT 1 FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('Agents_Balance_MV'),OBJECT_ID('SB_Contents'),OBJECT_ID('Tables'),OBJECT_ID('Periods'),OBJECT_ID('ST_Balance'),OBJECT_ID('TB_AutoPrint_tmp'))) THROW 51140,'Review business triggers first.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('TableSplit.Started',@sql)>0 OR CHARINDEX('Document.EditFieldsSaved',@sql)=0 OR CHARINDEX('Salary.BatchPosted',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51140,'Unexpected begin baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''TableSplit.Started'',''TableSplit.UnitMoved'',''TableSplit.UnitReturned'',''TableSplit.Confirmed'') AND @DomainCode=''Sales.TableSplit'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('TableSplit.Started',@sql)>0 OR CHARINDEX('Document.EditFieldsSaved',@sql)=0 OR CHARINDEX('Salary.BatchPosted',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51140,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''TableSplit.Started'',''TableSplit.UnitMoved'',''TableSplit.UnitReturned'',''TableSplit.Confirmed'') AND @domain=''Sales.TableSplit'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51140,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''TableSplit.Started'',''TableSplit.UnitMoved'',''TableSplit.UnitReturned'',''TableSplit.Confirmed'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51140,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditTableSplit040: immediate start/move/return/confirmation, no receipt procedures.
 IF @operation IN(''TableSplit.Started'',''TableSplit.UnitMoved'',''TableSplit.UnitReturned'',''TableSplit.Confirmed'')
 BEGIN
  DECLARE @tsOwner nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @tsBill int,@tsNumber int=TRY_CONVERT(int,@EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)''));
  IF @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>''TableSplit'' OR @EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'')<>''Tables_Trans_IM''
   OR ISNULL(TRY_CONVERT(int,@tsOwner),0)<=0 OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0
   OR @EventXml.value(''count(/Event/Document)'',''int'')<>1 OR @tsNumber IS NULL THROW 51140,''Invalid table split identity.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=''TableSplit'' AND EntityId=@tsOwner)
   THROW 51140,''Table split request mismatch.'',1;
  IF (SELECT COUNT(*) FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE BsType_ID=1 AND SB_ID=@tsNumber AND Table_ID=TRY_CONVERT(int,@tsOwner) AND isVoid=0 AND User_ID=@user)<>1
   THROW 51140,''Table split target invoice missing or ambiguous.'',1;
  SELECT @tsBill=T_ID FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE BsType_ID=1 AND SB_ID=@tsNumber AND Table_ID=TRY_CONVERT(int,@tsOwner) AND isVoid=0 AND User_ID=@user;
  IF NOT EXISTS(SELECT 1 FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@user AND u.is_Allow=1 AND v.Sales=1 AND v.Transfer_Table=1)
   THROW 51140,''Table split permission denied.'',1;
  DECLARE @tsFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @tsFields VALUES
   (''SplitTable'',''TableFloor'',''Integer'',''int''),
   (''SplitTable'',''TableName'',''String'',''nvarchar(100)''),
   (''SplitTable'',''TableActive'',''Boolean'',''bit''),
   (''SplitTable'',''TableReserved'',''Boolean'',''bit''),
   (''SplitTable'',''TableBusy'',''Boolean'',''bit''),
   (''SplitTable'',''TableOrderCode'',''String'',''nvarchar(100)''),
   (''SplitTable'',''TableCash'',''Integer'',''int''),
   (''SplitTable'',''TableOpenedAt'',''DateTime'',''datetime''),
   (''SplitTable'',''TablePrint'',''Boolean'',''bit''),
   (''SplitHeader'',''Voided'',''Integer'',''int''),
   (''SplitHeader'',''NeedRepost'',''Boolean'',''bit''),
   (''SplitHeader'',''NeedCancelReverse'',''Boolean'',''bit''),
   (''SplitHeader'',''VoidAt'',''DateTime'',''datetime''),
   (''SplitHeader'',''VoidBy'',''Integer'',''int''),
   (''SplitHeader'',''VoidReason'',''String'',''nvarchar(500)''),
   (''SplitHeader'',''JournalId'',''Integer'',''int''),
   (''SplitHeader'',''DocumentTotal'',''Decimal'',''numeric(18,3)''),
   (''SplitHeader'',''DocumentPure'',''Decimal'',''numeric(18,3)''),
   (''SplitHeader'',''Discount'',''Decimal'',''numeric(18,3)''),
   (''SplitHeader'',''Debit'',''Decimal'',''numeric(18,3)''),
   (''SplitHeader'',''Credit'',''Decimal'',''numeric(18,3)''),
   (''SplitHeader'',''Balance'',''Decimal'',''numeric(18,3)''),
   (''SplitHeader'',''AccountId'',''Integer'',''int''),
   (''SplitHeader'',''Approved'',''Integer'',''int''),
   (''SplitHeader'',''DocumentType'',''Integer'',''int''),
   (''SplitHeader'',''DocumentNumber'',''Integer'',''int''),
   (''SplitHeader'',''DocumentDate'',''DateTime'',''datetime''),
   (''SplitHeader'',''SalesOpen'',''Boolean'',''bit''),
   (''SplitHeader'',''Paused'',''Boolean'',''bit''),
   (''SplitHeader'',''OrderDelivered'',''Boolean'',''bit''),
   (''SplitHeader'',''PaidState'',''Integer'',''int''),
   (''SplitHeader'',''TableId'',''Integer'',''int''),
   (''SplitHeader'',''AccountName'',''String'',''nvarchar(500)''),
   (''SplitHeader'',''SecondaryDebit'',''Decimal'',''numeric(18,3)''),
   (''SplitHeader'',''EditOpenedAt'',''DateTime'',''datetime''),
   (''SplitHeader'',''EditOpenedBy'',''Integer'',''int''),
   (''SplitHeader'',''WorkflowAbout'',''String'',''nvarchar(max)''),
   (''SplitHeader'',''WorkflowReference'',''String'',''nvarchar(100)''),
   (''SplitHeader'',''WorkflowProject'',''Integer'',''int''),
   (''SplitHeader'',''WorkflowExchange'',''Decimal'',''numeric(18,3)''),
   (''SplitHeader'',''WorkflowCurrency'',''Integer'',''int''),
   (''SplitHeader'',''WorkflowPeriod'',''Integer'',''int''),
   (''SplitHeader'',''WorkflowTableOrdered'',''Integer'',''int''),
   (''SplitHeader'',''SplitOrderCode'',''String'',''nvarchar(100)''),
   (''SplitHeader'',''SplitUser'',''Integer'',''int''),
   (''SplitHeader'',''SplitDailyNumber'',''Integer'',''int''),
   (''SplitHeader'',''SplitSaleType'',''Integer'',''int''),
   (''SplitHeader'',''SplitBarcode'',''String'',''nvarchar(50)''),
   (''SplitLine'',''ItemId'',''Integer'',''int''),
   (''SplitLine'',''UnitId'',''Integer'',''bigint''),
   (''SplitLine'',''StoreId'',''Integer'',''bigint''),
   (''SplitLine'',''LineQuantity'',''Decimal'',''numeric(18,3)''),
   (''SplitLine'',''LinePrice'',''Decimal'',''numeric(18,3)''),
   (''SplitLine'',''LineTotal'',''Decimal'',''numeric(18,3)''),
   (''SplitLine'',''LineCost'',''Float'',''float(53)''),
   (''SplitLine'',''UnitCargo'',''Float'',''float(53)''),
   (''SplitLine'',''StoredQuantity'',''Decimal'',''numeric(18,2)''),
   (''SplitLine'',''LineDiscount'',''Float'',''float(53)''),
   (''SplitLine'',''LineNotes'',''String'',''nvarchar(max)''),
   (''SplitLine'',''ExpiryText'',''String'',''nvarchar(50)''),
   (''SplitLine'',''LineApproved'',''Integer'',''int''),
   (''SplitLine'',''LineDate'',''DateTime'',''datetime''),
   (''SplitLine'',''MinimumPriceUsed'',''Integer'',''int''),
   (''SplitLine'',''MinimumPrice2Used'',''Integer'',''int''),
   (''SplitLine'',''LineDocumentId'',''Integer'',''int''),
   (''SplitLine'',''LineComponentsText'',''String'',''nvarchar(max)''),
   (''SplitLine'',''ParentLineId'',''Integer'',''int''),
   (''SplitLine'',''SalespersonPercent'',''Decimal'',''numeric(18,3)''),
   (''SplitLine'',''MarketerValue'',''Decimal'',''numeric(18,3)''),
   (''SplitLine'',''LineBarcode'',''String'',''nvarchar(max)''),
   (''SplitLine'',''LineSerial'',''String'',''nvarchar(100)''),
   (''SplitLine'',''LineChecked'',''Boolean'',''bit''),
   (''SplitPeriod'',''SplitCounter'',''Integer'',''int''),
   (''SplitPeriod'',''SplitPeriodOpen'',''Boolean'',''bit''),
   (''SplitPeriod'',''SplitPeriodUser'',''Integer'',''bigint''),
   (''SplitStock'',''ItemId'',''Integer'',''int''),
   (''SplitStock'',''StoreId'',''Integer'',''bigint''),
   (''SplitStock'',''ExpiryText'',''String'',''nvarchar(50)''),
   (''SplitStock'',''StockQuantity'',''Float'',''float(53)''),
   (''SplitBalance'',''SplitBalanceItem'',''Integer'',''int''),
   (''SplitBalance'',''SplitBalanceStore'',''Integer'',''int''),
   (''SplitBalance'',''SplitBalanceExpiry'',''String'',''nvarchar(50)''),
   (''SplitBalance'',''SplitBalanceQuantity'',''Decimal'',''numeric(18,3)''),
   (''SplitPrint'',''QueueStoredId'',''Integer'',''int''),
   (''SplitPrint'',''QueueDate'',''DateTime'',''datetime''),
   (''SplitPrint'',''QueueNotes'',''String'',''nvarchar(max)''),
   (''SplitPrint'',''QueuePrinter'',''String'',''nvarchar(100)'');
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.ValueOrigin<>CASE WHEN c.EntityType=''SplitStock'' THEN 3 ELSE 1 END OR c.BeforeState NOT IN(1,2,3) OR c.AfterState NOT IN(2,3)
   OR NOT EXISTS(SELECT 1 FROM @tsFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType))
   THROW 51140,''Invalid table split field/state.'',1;
  IF EXISTS(SELECT EntityType,EntityId,FieldCode FROM @changes GROUP BY EntityType,EntityId,FieldCode HAVING COUNT(*)<>1)
   THROW 51140,''Duplicate table split field.'',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId
   HAVING COUNT(*)<>CASE EntityType WHEN ''SplitTable'' THEN 9 WHEN ''SplitHeader'' THEN 39 WHEN ''SplitLine'' THEN 24 WHEN ''SplitPeriod'' THEN 3 WHEN ''SplitStock'' THEN 4 WHEN ''SplitBalance'' THEN 4 WHEN ''SplitPrint'' THEN 4 ELSE -1 END)
   THROW 51140,''Incomplete table split row.'',1;
  IF @EventOrdinal=1 AND ((SELECT COUNT(*) FROM @changes)<>18 OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>''SplitTable'') OR (SELECT COUNT(DISTINCT EntityId) FROM @changes)<>2 OR NOT EXISTS(SELECT 1 FROM @changes WHERE EntityId=@tsOwner))
   THROW 51140,''First table split event requires both tables.'',1;
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.BeforeState=1 AND NOT
    ((EntityType=''SplitHeader'' AND @operation=''TableSplit.Started'') OR (EntityType IN(''SplitLine'',''SplitBalance'') AND @operation=''TableSplit.UnitMoved'') OR (EntityType=''SplitPrint'' AND @operation=''TableSplit.Confirmed'')))
   THROW 51140,''Unexpected table split row creation.'',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING MIN(BeforeState)=1 AND MAX(BeforeState)<>1)
   THROW 51140,''Mixed previous row state.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''SplitTable'' AND NOT EXISTS(SELECT 1 FROM dbo.[Tables] t WITH(HOLDLOCK) WHERE t.TB_ID=TRY_CONVERT(int,EntityId)))
   THROW 51140,''Unknown table.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''SplitHeader'' AND NOT EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV b WITH(HOLDLOCK) WHERE b.T_ID=TRY_CONVERT(int,EntityId) AND b.BsType_ID=1))
   THROW 51140,''Unknown sales header.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''SplitHeader'' AND BeforeState=1 AND EntityId<>CONVERT(nvarchar(128),@tsBill))
   THROW 51140,''Created header is not the destination invoice.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''SplitLine'' AND NOT EXISTS(SELECT 1 FROM dbo.SB_Contents b WITH(HOLDLOCK) WHERE b.T_ID=TRY_CONVERT(int,EntityId)))
   THROW 51140,''Missing transferred sales line.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''SplitLine'' AND BeforeState=1 AND FieldCode=''LineDocumentId'' AND (AfterState<>3 OR AfterValue<>CONVERT(nvarchar(128),@tsBill)))
   THROW 51140,''Split line created outside destination.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''SplitPrint'' AND (BeforeState<>1 OR (FieldCode=''QueueStoredId'' AND (AfterState<>3 OR AfterValue<>CONVERT(nvarchar(128),@tsBill)))))
   THROW 51140,''Unexpected print queue effect.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''SplitPeriod'' AND NOT EXISTS(SELECT 1 FROM dbo.Periods p WITH(HOLDLOCK) JOIN dbo.Agents_Balance_MV b WITH(HOLDLOCK) ON b.Pr_ID=p.Pr_ID WHERE b.T_ID=@tsBill AND p.Pr_ID=TRY_CONVERT(int,EntityId)))
   THROW 51140,''Unrelated period counter.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE BeforeState<>1 AND NOT(
   (EntityType=''SplitTable'' AND @operation=''TableSplit.Confirmed'' AND FieldCode IN(''TableOrderCode'',''TableOpenedAt'',''TableBusy''))
   OR (EntityType=''SplitHeader'' AND ((@operation=''TableSplit.Started'' AND FieldCode=''TableId'')
    OR (@operation=''TableSplit.Confirmed'' AND FieldCode IN(''Approved'',''WorkflowTableOrdered'',''DocumentTotal'',''DocumentPure'',''Credit'',''SplitOrderCode''))
    OR (@operation IN(''TableSplit.UnitMoved'',''TableSplit.UnitReturned'') AND FieldCode IN(''DocumentTotal'',''DocumentPure''))))
   OR (EntityType=''SplitLine'' AND ((@operation=''TableSplit.UnitMoved'' AND FieldCode IN(''LineDocumentId'',''ParentLineId'',''LineQuantity'',''LineTotal'',''StoredQuantity''))
    OR (@operation=''TableSplit.UnitReturned'' AND FieldCode IN(''LineDocumentId'',''ParentLineId''))))
   OR (EntityType=''SplitPeriod'' AND @operation=''TableSplit.Confirmed'' AND FieldCode=''SplitCounter'')
   OR (EntityType=''SplitStock'' AND @operation<>''TableSplit.Started'' AND FieldCode=''StockQuantity'')
   OR (EntityType=''SplitBalance'' AND @operation=''TableSplit.UnitMoved'' AND FieldCode=''SplitBalanceQuantity''))
   AND (BeforeState<>AfterState OR ISNULL(DATALENGTH(BeforeValue),-1)<>ISNULL(DATALENGTH(AfterValue),-1) OR ISNULL(BeforeValue,N'''') COLLATE Latin1_General_100_BIN2<>ISNULL(AfterValue,N'''') COLLATE Latin1_General_100_BIN2))
   THROW 51140,''Unexpected table split effect.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[Tables] p WITH(HOLDLOCK) ON p.[TB_ID]=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''SplitTable'' AND (p.[TB_ID] IS NULL OR (c.FieldCode=''TableFloor'' AND ((c.AfterState=2 AND p.[Flate_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Flate_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Flate_ID])))) OR
    (c.FieldCode=''TableName'' AND ((c.AfterState=2 AND p.[T_Name] IS NOT NULL) OR (c.AfterState=3 AND (p.[T_Name] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[T_Name]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[T_Name]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''TableActive'' AND ((c.AfterState=2 AND p.[isActive] IS NOT NULL) OR (c.AfterState=3 AND (p.[isActive] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isActive])))) OR
    (c.FieldCode=''TableReserved'' AND ((c.AfterState=2 AND p.[isReserve] IS NOT NULL) OR (c.AfterState=3 AND (p.[isReserve] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isReserve])))) OR
    (c.FieldCode=''TableBusy'' AND ((c.AfterState=2 AND p.[isbusy] IS NOT NULL) OR (c.AfterState=3 AND (p.[isbusy] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isbusy])))) OR
    (c.FieldCode=''TableOrderCode'' AND ((c.AfterState=2 AND p.[TB_ORDER_CODE] IS NOT NULL) OR (c.AfterState=3 AND (p.[TB_ORDER_CODE] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[TB_ORDER_CODE]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[TB_ORDER_CODE]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''TableCash'' AND ((c.AfterState=2 AND p.[is_Cash] IS NOT NULL) OR (c.AfterState=3 AND (p.[is_Cash] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[is_Cash])))) OR
    (c.FieldCode=''TableOpenedAt'' AND ((c.AfterState=2 AND p.[Start_Open] IS NOT NULL) OR (c.AfterState=3 AND (p.[Start_Open] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[Start_Open])))) OR
    (c.FieldCode=''TablePrint'' AND ((c.AfterState=2 AND p.[is_Print] IS NOT NULL) OR (c.AfterState=3 AND (p.[is_Print] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[is_Print]))))))
   THROW 51140,''SplitTable after-state does not match storage.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[Agents_Balance_MV] p WITH(HOLDLOCK) ON p.[T_ID]=TRY_CONVERT(int,c.EntityId) LEFT JOIN dbo.Agents a WITH(HOLDLOCK) ON a.AG_ID=p.AG_ID WHERE c.EntityType=''SplitHeader'' AND (p.[T_ID] IS NULL OR (c.FieldCode=''Voided'' AND ((c.AfterState=2 AND p.[isVoid] IS NOT NULL) OR (c.AfterState=3 AND (p.[isVoid] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isVoid])))) OR
    (c.FieldCode=''NeedRepost'' AND ((c.AfterState=2 AND p.[NeedRepost] IS NOT NULL) OR (c.AfterState=3 AND (p.[NeedRepost] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[NeedRepost])))) OR
    (c.FieldCode=''NeedCancelReverse'' AND ((c.AfterState=2 AND p.[NeedCancelReverse] IS NOT NULL) OR (c.AfterState=3 AND (p.[NeedCancelReverse] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[NeedCancelReverse])))) OR
    (c.FieldCode=''VoidAt'' AND ((c.AfterState=2 AND p.[VoidAt] IS NOT NULL) OR (c.AfterState=3 AND (p.[VoidAt] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[VoidAt])))) OR
    (c.FieldCode=''VoidBy'' AND ((c.AfterState=2 AND p.[VoidBy] IS NOT NULL) OR (c.AfterState=3 AND (p.[VoidBy] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[VoidBy])))) OR
    (c.FieldCode=''VoidReason'' AND ((c.AfterState=2 AND p.[VoidReason] IS NOT NULL) OR (c.AfterState=3 AND (p.[VoidReason] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[VoidReason]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[VoidReason]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''JournalId'' AND ((c.AfterState=2 AND p.[JournalId] IS NOT NULL) OR (c.AfterState=3 AND (p.[JournalId] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[JournalId])))) OR
    (c.FieldCode=''DocumentTotal'' AND ((c.AfterState=2 AND p.[Total] IS NOT NULL) OR (c.AfterState=3 AND (p.[Total] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Total])))) OR
    (c.FieldCode=''DocumentPure'' AND ((c.AfterState=2 AND p.[Pure] IS NOT NULL) OR (c.AfterState=3 AND (p.[Pure] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Pure])))) OR
    (c.FieldCode=''Discount'' AND ((c.AfterState=2 AND p.[Discount] IS NOT NULL) OR (c.AfterState=3 AND (p.[Discount] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Discount])))) OR
    (c.FieldCode=''Debit'' AND ((c.AfterState=2 AND p.[Debit] IS NOT NULL) OR (c.AfterState=3 AND (p.[Debit] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Debit])))) OR
    (c.FieldCode=''Credit'' AND ((c.AfterState=2 AND p.[Credit] IS NOT NULL) OR (c.AfterState=3 AND (p.[Credit] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Credit])))) OR
    (c.FieldCode=''Balance'' AND ((c.AfterState=2 AND p.[Balance] IS NOT NULL) OR (c.AfterState=3 AND (p.[Balance] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Balance])))) OR
    (c.FieldCode=''AccountId'' AND ((c.AfterState=2 AND p.[AG_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[AG_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[AG_ID])))) OR
    (c.FieldCode=''Approved'' AND ((c.AfterState=2 AND p.[isDepended] IS NOT NULL) OR (c.AfterState=3 AND (p.[isDepended] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isDepended])))) OR
    (c.FieldCode=''DocumentType'' AND ((c.AfterState=2 AND p.[BsType_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[BsType_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[BsType_ID])))) OR
    (c.FieldCode=''DocumentNumber'' AND ((c.AfterState=2 AND p.SB_ID IS NOT NULL) OR (c.AfterState=3 AND (p.SB_ID IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.SB_ID)))) OR
    (c.FieldCode=''DocumentDate'' AND ((c.AfterState=2 AND p.[Date] IS NOT NULL) OR (c.AfterState=3 AND (p.[Date] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[Date])))) OR
    (c.FieldCode=''SalesOpen'' AND ((c.AfterState=2 AND p.[SB_isOpen] IS NOT NULL) OR (c.AfterState=3 AND (p.[SB_isOpen] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[SB_isOpen])))) OR
    (c.FieldCode=''Paused'' AND ((c.AfterState=2 AND p.[isPause] IS NOT NULL) OR (c.AfterState=3 AND (p.[isPause] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isPause])))) OR
    (c.FieldCode=''OrderDelivered'' AND ((c.AfterState=2 AND p.[Order_isDeleverd] IS NOT NULL) OR (c.AfterState=3 AND (p.[Order_isDeleverd] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Order_isDeleverd])))) OR
    (c.FieldCode=''PaidState'' AND ((c.AfterState=2 AND p.[isPied] IS NOT NULL) OR (c.AfterState=3 AND (p.[isPied] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isPied])))) OR
    (c.FieldCode=''TableId'' AND ((c.AfterState=2 AND p.[Table_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Table_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Table_ID])))) OR
    (c.FieldCode=''AccountName'' AND ((c.AfterState=2 AND a.Ag_name IS NOT NULL) OR (c.AfterState=3 AND (a.Ag_name IS NULL OR DATALENGTH(CONVERT(nvarchar(max),a.Ag_name))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),a.Ag_name) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''SecondaryDebit'' AND ((c.AfterState=2 AND p.[Debit_2] IS NOT NULL) OR (c.AfterState=3 AND (p.[Debit_2] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Debit_2])))) OR
    (c.FieldCode=''EditOpenedAt'' AND ((c.AfterState=2 AND p.[EditOpenedAt] IS NOT NULL) OR (c.AfterState=3 AND (p.[EditOpenedAt] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[EditOpenedAt])))) OR
    (c.FieldCode=''EditOpenedBy'' AND ((c.AfterState=2 AND p.[EditOpenedBy] IS NOT NULL) OR (c.AfterState=3 AND (p.[EditOpenedBy] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[EditOpenedBy])))) OR
    (c.FieldCode=''WorkflowAbout'' AND ((c.AfterState=2 AND p.[About] IS NOT NULL) OR (c.AfterState=3 AND (p.[About] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[About]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[About]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''WorkflowReference'' AND ((c.AfterState=2 AND p.[ReferNum] IS NOT NULL) OR (c.AfterState=3 AND (p.[ReferNum] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[ReferNum]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[ReferNum]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''WorkflowProject'' AND ((c.AfterState=2 AND p.[Proj_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Proj_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Proj_ID])))) OR
    (c.FieldCode=''WorkflowExchange'' AND ((c.AfterState=2 AND p.[Cr_Equal_Value] IS NOT NULL) OR (c.AfterState=3 AND (p.[Cr_Equal_Value] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Cr_Equal_Value])))) OR
    (c.FieldCode=''WorkflowCurrency'' AND ((c.AfterState=2 AND p.[Cr_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Cr_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Cr_ID])))) OR
    (c.FieldCode=''WorkflowPeriod'' AND ((c.AfterState=2 AND p.[Pr_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Pr_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Pr_ID])))) OR
    (c.FieldCode=''WorkflowTableOrdered'' AND ((c.AfterState=2 AND p.[TB_isOrderd] IS NOT NULL) OR (c.AfterState=3 AND (p.[TB_isOrderd] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[TB_isOrderd])))) OR
    (c.FieldCode=''SplitOrderCode'' AND ((c.AfterState=2 AND p.[TB_ORDER_CODE] IS NOT NULL) OR (c.AfterState=3 AND (p.[TB_ORDER_CODE] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[TB_ORDER_CODE]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[TB_ORDER_CODE]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''SplitUser'' AND ((c.AfterState=2 AND p.[User_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[User_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[User_ID])))) OR
    (c.FieldCode=''SplitDailyNumber'' AND ((c.AfterState=2 AND p.[S_Bill_Pr_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[S_Bill_Pr_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[S_Bill_Pr_ID])))) OR
    (c.FieldCode=''SplitSaleType'' AND ((c.AfterState=2 AND p.[S_Bills_Type] IS NOT NULL) OR (c.AfterState=3 AND (p.[S_Bills_Type] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[S_Bills_Type])))) OR
    (c.FieldCode=''SplitBarcode'' AND ((c.AfterState=2 AND p.[Barcode] IS NOT NULL) OR (c.AfterState=3 AND (p.[Barcode] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Barcode]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Barcode]) COLLATE Latin1_General_100_BIN2))))))
   THROW 51140,''SplitHeader after-state does not match storage.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[SB_Contents] p WITH(HOLDLOCK) ON p.[T_ID]=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''SplitLine'' AND (p.[T_ID] IS NULL OR (c.FieldCode=''ItemId'' AND ((c.AfterState=2 AND p.[IM_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[IM_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[IM_ID])))) OR
    (c.FieldCode=''UnitId'' AND ((c.AfterState=2 AND p.[U_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[U_ID] IS NULL OR TRY_CONVERT(bigint,c.AfterValue) IS NULL OR TRY_CONVERT(bigint,c.AfterValue)<>p.[U_ID])))) OR
    (c.FieldCode=''StoreId'' AND ((c.AfterState=2 AND p.[ST_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[ST_ID] IS NULL OR TRY_CONVERT(bigint,c.AfterValue) IS NULL OR TRY_CONVERT(bigint,c.AfterValue)<>p.[ST_ID])))) OR
    (c.FieldCode=''LineQuantity'' AND ((c.AfterState=2 AND p.[QTY] IS NOT NULL) OR (c.AfterState=3 AND (p.[QTY] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[QTY])))) OR
    (c.FieldCode=''LinePrice'' AND ((c.AfterState=2 AND p.[Price] IS NOT NULL) OR (c.AfterState=3 AND (p.[Price] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Price])))) OR
    (c.FieldCode=''LineTotal'' AND ((c.AfterState=2 AND p.[T_Price] IS NOT NULL) OR (c.AfterState=3 AND (p.[T_Price] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[T_Price])))) OR
    (c.FieldCode=''LineCost'' AND ((c.AfterState=2 AND p.[Cost] IS NOT NULL) OR (c.AfterState=3 AND (p.[Cost] IS NULL OR TRY_CONVERT(float(53),c.AfterValue) IS NULL OR TRY_CONVERT(float(53),c.AfterValue)<>p.[Cost])))) OR
    (c.FieldCode=''UnitCargo'' AND ((c.AfterState=2 AND p.[U_Cargo] IS NOT NULL) OR (c.AfterState=3 AND (p.[U_Cargo] IS NULL OR TRY_CONVERT(float(53),c.AfterValue) IS NULL OR TRY_CONVERT(float(53),c.AfterValue)<>p.[U_Cargo])))) OR
    (c.FieldCode=''StoredQuantity'' AND ((c.AfterState=2 AND p.[ST_QTY] IS NOT NULL) OR (c.AfterState=3 AND (p.[ST_QTY] IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue)<>p.[ST_QTY])))) OR
    (c.FieldCode=''LineDiscount'' AND ((c.AfterState=2 AND p.[IM_Discount] IS NOT NULL) OR (c.AfterState=3 AND (p.[IM_Discount] IS NULL OR TRY_CONVERT(float(53),c.AfterValue) IS NULL OR TRY_CONVERT(float(53),c.AfterValue)<>p.[IM_Discount])))) OR
    (c.FieldCode=''LineNotes'' AND ((c.AfterState=2 AND p.[Notes] IS NOT NULL) OR (c.AfterState=3 AND (p.[Notes] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Notes]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Notes]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''ExpiryText'' AND ((c.AfterState=2 AND p.[D_Vaild] IS NOT NULL) OR (c.AfterState=3 AND (p.[D_Vaild] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[D_Vaild]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[D_Vaild]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''LineApproved'' AND ((c.AfterState=2 AND p.[isDepended] IS NOT NULL) OR (c.AfterState=3 AND (p.[isDepended] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isDepended])))) OR
    (c.FieldCode=''LineDate'' AND ((c.AfterState=2 AND p.[Date] IS NOT NULL) OR (c.AfterState=3 AND (p.[Date] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[Date])))) OR
    (c.FieldCode=''MinimumPriceUsed'' AND ((c.AfterState=2 AND p.[is_By_Min_SP] IS NOT NULL) OR (c.AfterState=3 AND (p.[is_By_Min_SP] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[is_By_Min_SP])))) OR
    (c.FieldCode=''MinimumPrice2Used'' AND ((c.AfterState=2 AND p.[is_By_Min_SP_2] IS NOT NULL) OR (c.AfterState=3 AND (p.[is_By_Min_SP_2] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[is_By_Min_SP_2])))) OR
    (c.FieldCode=''LineDocumentId'' AND ((c.AfterState=2 AND p.[Bill_T_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Bill_T_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Bill_T_ID])))) OR
    (c.FieldCode=''LineComponentsText'' AND ((c.AfterState=2 AND p.[Compons] IS NOT NULL) OR (c.AfterState=3 AND (p.[Compons] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Compons]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Compons]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''ParentLineId'' AND ((c.AfterState=2 AND p.[Apart_From] IS NOT NULL) OR (c.AfterState=3 AND (p.[Apart_From] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Apart_From])))) OR
    (c.FieldCode=''SalespersonPercent'' AND ((c.AfterState=2 AND p.[Saler_Percent] IS NOT NULL) OR (c.AfterState=3 AND (p.[Saler_Percent] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Saler_Percent])))) OR
    (c.FieldCode=''MarketerValue'' AND ((c.AfterState=2 AND p.[Markter_Val] IS NOT NULL) OR (c.AfterState=3 AND (p.[Markter_Val] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Markter_Val])))) OR
    (c.FieldCode=''LineBarcode'' AND ((c.AfterState=2 AND p.[Barcode] IS NOT NULL) OR (c.AfterState=3 AND (p.[Barcode] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Barcode]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Barcode]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''LineSerial'' AND ((c.AfterState=2 AND p.[Serial_Code] IS NOT NULL) OR (c.AfterState=3 AND (p.[Serial_Code] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Serial_Code]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Serial_Code]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''LineChecked'' AND ((c.AfterState=2 AND p.[is_Check] IS NOT NULL) OR (c.AfterState=3 AND (p.[is_Check] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[is_Check]))))))
   THROW 51140,''SplitLine after-state does not match storage.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[Periods] p WITH(HOLDLOCK) ON p.[Pr_ID]=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''SplitPeriod'' AND (p.[Pr_ID] IS NULL OR (c.FieldCode=''SplitCounter'' AND ((c.AfterState=2 AND p.[AG_Counter] IS NOT NULL) OR (c.AfterState=3 AND (p.[AG_Counter] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[AG_Counter])))) OR
    (c.FieldCode=''SplitPeriodOpen'' AND ((c.AfterState=2 AND p.[Pr_isOpen] IS NOT NULL) OR (c.AfterState=3 AND (p.[Pr_isOpen] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Pr_isOpen])))) OR
    (c.FieldCode=''SplitPeriodUser'' AND ((c.AfterState=2 AND p.[User_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[User_ID] IS NULL OR TRY_CONVERT(bigint,c.AfterValue) IS NULL OR TRY_CONVERT(bigint,c.AfterValue)<>p.[User_ID]))))))
   THROW 51140,''SplitPeriod after-state does not match storage.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[ST_Balance] p WITH(HOLDLOCK) ON p.[T_ID]=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''SplitBalance'' AND (p.[T_ID] IS NULL OR (c.FieldCode=''SplitBalanceItem'' AND ((c.AfterState=2 AND p.[IM_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[IM_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[IM_ID])))) OR
    (c.FieldCode=''SplitBalanceStore'' AND ((c.AfterState=2 AND p.[ST_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[ST_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[ST_ID])))) OR
    (c.FieldCode=''SplitBalanceExpiry'' AND ((c.AfterState=2 AND p.[D_Valid] IS NOT NULL) OR (c.AfterState=3 AND (p.[D_Valid] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[D_Valid]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[D_Valid]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''SplitBalanceQuantity'' AND ((c.AfterState=2 AND p.[ST_QTY] IS NOT NULL) OR (c.AfterState=3 AND (p.[ST_QTY] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[ST_QTY]))))))
   THROW 51140,''SplitBalance after-state does not match storage.'',1;
  IF EXISTS(SELECT 1 FROM (SELECT EntityId,MAX(CASE WHEN FieldCode=''QueueStoredId'' THEN AfterState END) AS [QueueStoredIdState],MAX(CASE WHEN FieldCode=''QueueStoredId'' THEN AfterValue END) AS [QueueStoredIdValue],
    MAX(CASE WHEN FieldCode=''QueueDate'' THEN AfterState END) AS [QueueDateState],MAX(CASE WHEN FieldCode=''QueueDate'' THEN AfterValue END) AS [QueueDateValue],
    MAX(CASE WHEN FieldCode=''QueueNotes'' THEN AfterState END) AS [QueueNotesState],MAX(CASE WHEN FieldCode=''QueueNotes'' THEN AfterValue END) AS [QueueNotesValue],
    MAX(CASE WHEN FieldCode=''QueuePrinter'' THEN AfterState END) AS [QueuePrinterState],MAX(CASE WHEN FieldCode=''QueuePrinter'' THEN AfterValue END) AS [QueuePrinterValue] FROM @changes WHERE EntityType=''SplitPrint'' GROUP BY EntityId) q
   WHERE NOT EXISTS(SELECT 1 FROM dbo.TB_AutoPrint_tmp p WITH(HOLDLOCK) WHERE NOT(((q.[QueueStoredIdState]=2 AND p.[T_ID] IS NOT NULL) OR (q.[QueueStoredIdState]=3 AND (p.[T_ID] IS NULL OR TRY_CONVERT(int,q.[QueueStoredIdValue]) IS NULL OR TRY_CONVERT(int,q.[QueueStoredIdValue])<>p.[T_ID]))) OR ((q.[QueueDateState]=2 AND p.[DATE] IS NOT NULL) OR (q.[QueueDateState]=3 AND (p.[DATE] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),q.[QueueDateValue],126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),q.[QueueDateValue],126))<>p.[DATE]))) OR ((q.[QueueNotesState]=2 AND p.[Notes] IS NOT NULL) OR (q.[QueueNotesState]=3 AND (p.[Notes] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Notes]))<>DATALENGTH(q.[QueueNotesValue]) OR q.[QueueNotesValue] COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Notes]) COLLATE Latin1_General_100_BIN2))) OR ((q.[QueuePrinterState]=2 AND p.[Printer_IP] IS NOT NULL) OR (q.[QueuePrinterState]=3 AND (p.[Printer_IP] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Printer_IP]))<>DATALENGTH(q.[QueuePrinterValue]) OR q.[QueuePrinterValue] COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Printer_IP]) COLLATE Latin1_General_100_BIN2))))))
   THROW 51140,''Split print row does not match one actual queue row.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''SplitStock'' AND (AfterState<>3 OR BeforeState<>3 OR LEN(EntityId)<>64 OR EntityId COLLATE Latin1_General_100_BIN2 LIKE ''%[^A-F0-9]%''))
   THROW 51140,''Invalid derived stock identity or state.'',1;
  IF EXISTS(SELECT 1 FROM
   (SELECT EntityId,MAX(CASE WHEN FieldCode=''ItemId'' THEN AfterValue END) AS ItemValue,
    MAX(CASE WHEN FieldCode=''StoreId'' THEN AfterValue END) AS StoreValue,
    MAX(CASE WHEN FieldCode=''ExpiryText'' THEN AfterValue END) AS ExpiryValue,
    MAX(CASE WHEN FieldCode=''StockQuantity'' THEN AfterValue END) AS QuantityValue
    FROM @changes WHERE EntityType=''SplitStock'' GROUP BY EntityId) q
   OUTER APPLY(SELECT CONVERT(float,ISNULL(SUM(s.QTY),0)) AS QuantityValue FROM dbo.ST_Balance_V s WITH(HOLDLOCK)
    WHERE s.IM_ID=TRY_CONVERT(int,q.ItemValue) AND s.ST_ID=TRY_CONVERT(bigint,q.StoreValue) AND ISNULL(s.D_Vaild,N'''')=q.ExpiryValue) p
   WHERE ISNULL(TRY_CONVERT(int,q.ItemValue),0)<=0 OR ISNULL(TRY_CONVERT(bigint,q.StoreValue),0)<=0 OR DATALENGTH(q.ExpiryValue)>100
    OR TRY_CONVERT(float,q.QuantityValue) IS NULL OR TRY_CONVERT(float,q.QuantityValue)<>p.QuantityValue
    OR NOT EXISTS(SELECT 1 FROM dbo.SB_Contents l WITH(HOLDLOCK) WHERE l.IM_ID=TRY_CONVERT(int,q.ItemValue) AND l.ST_ID=TRY_CONVERT(bigint,q.StoreValue)
      AND DATALENGTH(ISNULL(l.D_Vaild,N''''))=DATALENGTH(q.ExpiryValue) AND ISNULL(l.D_Vaild,N'''') COLLATE Latin1_General_100_BIN2=q.ExpiryValue COLLATE Latin1_General_100_BIN2))
   THROW 51140,''Derived stock after-state does not match its item/store/expiry bucket.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.TableSplit"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.TableSplit"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF XACT_STATE()<>0 ROLLBACK;
 THROW;
END CATCH;
