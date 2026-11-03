/* SQL Server 2014. CODE ONLY: do not install while clients are connected.
   Preserves the existing calculation order; no receipt/custody/cancellation migration.
   Requires the sequential 031-036 package, including Inventory.Operations (036). */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51137,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51137,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51137,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51137,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.QuoteLines"]')=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Inventory.Operations"]')=1) THROW 51137,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51137,'Incomplete requests.',1;
 IF EXISTS(SELECT 1 FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('Agents_Balance_MV'),OBJECT_ID('ViewBill_Contents'),OBJECT_ID('Currency'))) THROW 51137,'Review business triggers first.',1;
 IF OBJECT_ID('dbo.ViewBill_Discount_Distribute_Audited') IS NOT NULL THROW 51137,'Quote distribution object already exists; review deployment state.',1;
 -- This is a separate quote-only procedure. The sales procedure is never replaced.
 EXEC sys.sp_executesql N'CREATE PROCEDURE [dbo].[ViewBill_Discount_Distribute_Audited] 
@T_ID	int,
@Discount_Val Numeric(18,2)= NULL
AS

declare @isError int , @SUM_QTY Numeric(18,2) , @j INT , @U_ID INT , @IM_ID INT , @SLAEPRICE Numeric(18,2)
IF NOT EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@T_ID AND BsType_ID=16 AND isVoid=0) THROW 51137,''Expected active quote document.'',1;
-- QuoteDistribution037: quote rows only; preserve the original distribution formula.
BEGIN TRAN

IF @Discount_Val IS NOT NULL 
BEGIN
SELECT @j = min(T_ID) From ViewBill_Contents WHERE Bill_T_ID = @T_ID
WHILE @j is Not NULL
BEGIN
SELECT @U_ID = U_ID ,@IM_ID = IM_ID FROM ViewBill_Contents WHERE T_ID = @j
SELECT @SLAEPRICE = Price FROM IM_Units WHERE IM_ID = @IM_ID and U_ID = @U_ID 
Update ViewBill_Contents SET Price =@SLAEPRICE, T_Price = (QTY * @SLAEPRICE) 
WHERE T_ID = @j 
SELECT @j = min(T_ID) From ViewBill_Contents WHERE Bill_T_ID = @T_ID AND T_ID > @j
END
-------------------------------------------------------------------------------------------
SELECT @SUM_QTY = ISNULL(SUM(QTY),0) FROM ViewBill_Contents WHERE Bill_T_ID = @T_ID
Update ViewBill_Contents SET Price = Price -  (@Discount_Val / @SUM_QTY) , T_Price = (QTY * (Price - (@Discount_Val / @SUM_QTY)))
WHERE Bill_T_ID = @T_ID 

UPDATE Agents_Balance_MV SET Discount = @Discount_Val WHERE T_ID = @T_ID

END
ELSE
BEGIN

SELECT @j = min(T_ID) From ViewBill_Contents WHERE Bill_T_ID = @T_ID
WHILE @j is Not NULL
BEGIN
SELECT @U_ID = U_ID ,@IM_ID = IM_ID FROM ViewBill_Contents WHERE T_ID = @j
SELECT @SLAEPRICE = Price FROM IM_Units WHERE IM_ID = @IM_ID and U_ID = @U_ID 
Update ViewBill_Contents SET Price =@SLAEPRICE, T_Price = (QTY * @SLAEPRICE) 
WHERE T_ID = @j
SELECT @j = min(T_ID) From ViewBill_Contents WHERE Bill_T_ID = @T_ID AND T_ID > @j

UPDATE Agents_Balance_MV SET Discount = NULL WHERE T_ID = @T_ID
END


END

	select @isError = @@ERROR 
	IF @isError <> 0 
	goto PROBLEM

		COMMIT TRAN

	PROBLEM:
	if @isError <> 0 begin
	print ''DB ERROR ! _ SB_Discount_Distribute ''
	ROLLBACK TRAN

END';
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Document.EditFieldsSaved',@sql)>0 OR CHARINDEX('Quote.LineAdded',@sql)=0 OR CHARINDEX('Inventory.PurchaseLineDeleted',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51137,'Unexpected begin baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''Document.EditFieldsSaved'',''Document.ProjectChanged'',''Purchase.AccountCurrencyChanged'',''Purchase.CurrencyChanged'',''Quote.Confirmed'',''Quote.DiscountChanged'',''Quote.DiscountDistributed'',''Document.MetadataSaved'',''Quote.Reserved'',''Quote.EditFieldsSaved'') AND @DomainCode=''Documents.Workflows'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Document.EditFieldsSaved',@sql)>0 OR CHARINDEX('Quote.LineAdded',@sql)=0 OR CHARINDEX('Inventory.PurchaseLineDeleted',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51137,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''Document.EditFieldsSaved'',''Document.ProjectChanged'',''Purchase.AccountCurrencyChanged'',''Purchase.CurrencyChanged'',''Quote.Confirmed'',''Quote.DiscountChanged'',''Quote.DiscountDistributed'',''Document.MetadataSaved'',''Quote.Reserved'',''Quote.EditFieldsSaved'') AND @domain=''Documents.Workflows'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51137,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Document.EditFieldsSaved'',''Document.ProjectChanged'',''Purchase.AccountCurrencyChanged'',''Purchase.CurrencyChanged'',''Quote.Confirmed'',''Quote.DiscountChanged'',''Quote.DiscountDistributed'',''Document.MetadataSaved'',''Quote.Reserved'',''Quote.EditFieldsSaved'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51137,'Unexpected writer insertion anchor.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditDocumentWorkflows037: closed non-receipt header workflows.
 IF @operation IN(''Document.EditFieldsSaved'',''Document.ProjectChanged'',''Purchase.AccountCurrencyChanged'',''Purchase.CurrencyChanged'',''Quote.Confirmed'',''Quote.DiscountChanged'',''Quote.DiscountDistributed'',''Document.MetadataSaved'',''Quote.Reserved'',''Quote.EditFieldsSaved'')
 BEGIN
  DECLARE @wfOwner nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @wfScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)''),@wfKind int,@wfNumber int;
  IF @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>''WorkflowHeader'' OR ISNULL(TRY_CONVERT(int,@wfOwner),0)<=0
   OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0 OR @EventXml.value(''count(/Event/Document)'',''int'')<>1
   THROW 51137,''Invalid workflow identity.'',1;
  SELECT @wfKind=BsType_ID,@wfNumber=CASE BsType_ID WHEN 1 THEN SB_ID WHEN 7 THEN Pch_ID WHEN 16 THEN ViewSB_ID END
   FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@wfOwner) AND isVoid=0;
  IF @wfNumber IS NULL OR @EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)'')<>CONVERT(nvarchar(50),@wfNumber)
   THROW 51137,''Workflow number/type mismatch.'',1;
  IF NOT((@operation=''Document.EditFieldsSaved'' AND ((@wfKind=1 AND @wfScreen=''Sales'') OR (@wfKind=7 AND @wfScreen=''Pch'')))
    OR (@operation=''Document.ProjectChanged'' AND ((@wfKind=1 AND @wfScreen=''Sales'') OR (@wfKind=16 AND @wfScreen=''ViewBill'')))
    OR (@operation IN(''Purchase.AccountCurrencyChanged'',''Purchase.CurrencyChanged'') AND @wfKind=7 AND @wfScreen=''Pch'')
    OR (@operation=''Document.MetadataSaved'' AND @wfKind=1 AND @wfScreen=''Sales'')
    OR (@operation IN(''Quote.Confirmed'',''Quote.DiscountChanged'',''Quote.DiscountDistributed'',''Quote.Reserved'',''Quote.EditFieldsSaved'') AND @wfKind=16 AND @wfScreen=''ViewBill''))
   THROW 51137,''Workflow screen mismatch.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=''WorkflowHeader'' AND EntityId=@wfOwner)
   THROW 51137,''Workflow request mismatch.'',1;
  DECLARE @wfFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @wfFields VALUES
   (''WorkflowHeader'',''Voided'',''Integer'',''int''),
   (''WorkflowHeader'',''NeedRepost'',''Boolean'',''bit''),
   (''WorkflowHeader'',''NeedCancelReverse'',''Boolean'',''bit''),
   (''WorkflowHeader'',''VoidAt'',''DateTime'',''datetime''),
   (''WorkflowHeader'',''VoidBy'',''Integer'',''int''),
   (''WorkflowHeader'',''VoidReason'',''String'',''nvarchar(500)''),
   (''WorkflowHeader'',''JournalId'',''Integer'',''int''),
   (''WorkflowHeader'',''DocumentTotal'',''Decimal'',''numeric(18,3)''),
   (''WorkflowHeader'',''DocumentPure'',''Decimal'',''numeric(18,3)''),
   (''WorkflowHeader'',''Discount'',''Decimal'',''numeric(18,3)''),
   (''WorkflowHeader'',''Debit'',''Decimal'',''numeric(18,3)''),
   (''WorkflowHeader'',''Credit'',''Decimal'',''numeric(18,3)''),
   (''WorkflowHeader'',''Balance'',''Decimal'',''numeric(18,3)''),
   (''WorkflowHeader'',''AccountId'',''Integer'',''int''),
   (''WorkflowHeader'',''Approved'',''Integer'',''int''),
   (''WorkflowHeader'',''DocumentType'',''Integer'',''int''),
   (''WorkflowHeader'',''DocumentNumber'',''Integer'',''int''),
   (''WorkflowHeader'',''DocumentDate'',''DateTime'',''datetime''),
   (''WorkflowHeader'',''SalesOpen'',''Boolean'',''bit''),
   (''WorkflowHeader'',''Paused'',''Boolean'',''bit''),
   (''WorkflowHeader'',''OrderDelivered'',''Boolean'',''bit''),
   (''WorkflowHeader'',''PaidState'',''Integer'',''int''),
   (''WorkflowHeader'',''TableId'',''Integer'',''int''),
   (''WorkflowHeader'',''AccountName'',''String'',''nvarchar(500)''),
   (''WorkflowHeader'',''SecondaryDebit'',''Decimal'',''numeric(18,3)''),
   (''WorkflowHeader'',''EditOpenedAt'',''DateTime'',''datetime''),
   (''WorkflowHeader'',''EditOpenedBy'',''Integer'',''int''),
   (''WorkflowQuoteLine'',''QuoteOwnerId'',''Integer'',''int''),
   (''WorkflowQuoteLine'',''QuoteItemId'',''Integer'',''int''),
   (''WorkflowQuoteLine'',''QuoteUnitId'',''Integer'',''bigint''),
   (''WorkflowQuoteLine'',''QuoteStoreId'',''Integer'',''bigint''),
   (''WorkflowQuoteLine'',''QuoteLineDate'',''DateTime'',''datetime''),
   (''WorkflowQuoteLine'',''QuoteComponents'',''String'',''nvarchar(max)''),
   (''WorkflowQuoteLine'',''QuoteCost'',''Decimal'',''numeric(18,3)''),
   (''WorkflowQuoteLine'',''QuotePrice'',''Decimal'',''numeric(18,3)''),
   (''WorkflowQuoteLine'',''QuoteExpiry'',''String'',''nvarchar(50)''),
   (''WorkflowQuoteLine'',''QuoteQuantity'',''Float'',''float(53)''),
   (''WorkflowQuoteLine'',''QuoteLineTotal'',''Decimal'',''numeric(18,3)''),
   (''WorkflowQuoteLine'',''QuoteCargo'',''Float'',''float(53)''),
   (''WorkflowQuoteLine'',''QuoteStockQuantity'',''Decimal'',''numeric(18,2)''),
   (''WorkflowQuoteLine'',''QuoteApproved'',''Integer'',''int''),
   (''WorkflowQuoteLine'',''QuoteParentLine'',''Integer'',''int''),
   (''WorkflowQuoteLine'',''QuoteSalespersonPercent'',''Decimal'',''numeric(18,2)''),
   (''WorkflowQuoteLine'',''QuoteNotes'',''String'',''nvarchar(max)''),
   (''WorkflowQuoteLine'',''QuoteBarcode'',''String'',''nvarchar(50)''),
   (''WorkflowQuoteLine'',''QuoteItemName'',''String'',''nvarchar(500)''),
   (''WorkflowHeader'',''WorkflowAbout'',''String'',''nvarchar(max)''),
   (''WorkflowHeader'',''WorkflowReference'',''String'',''nvarchar(100)''),
   (''WorkflowHeader'',''WorkflowProject'',''Integer'',''int''),
   (''WorkflowHeader'',''WorkflowExchange'',''Decimal'',''numeric(18,3)''),
   (''WorkflowHeader'',''WorkflowCurrency'',''Integer'',''int''),
   (''WorkflowHeader'',''WorkflowPeriod'',''Integer'',''int''),
   (''WorkflowHeader'',''WorkflowTableOrdered'',''Integer'',''int''),
   (''WorkflowReference'',''LinkedReference'',''String'',''nvarchar(100)''),
   (''WorkflowReference'',''LinkedType'',''Integer'',''int''),
   (''WorkflowReference'',''LinkedPeriod'',''Integer'',''int''),
   (''WorkflowCurrency'',''CurrencyName'',''String'',''nvarchar(550)''),
   (''WorkflowCurrency'',''CurrencyRate'',''Decimal'',''numeric(18,2)'');
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.ValueOrigin<>1 OR c.BeforeState NOT IN(2,3) OR c.AfterState NOT IN(2,3)
    OR NOT EXISTS(SELECT 1 FROM @wfFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType))
   THROW 51137,''Invalid workflow field/state.'',1;
  IF EXISTS(SELECT EntityType,EntityId,FieldCode FROM @changes GROUP BY EntityType,EntityId,FieldCode HAVING COUNT(*)<>1)
   THROW 51137,''Duplicate workflow field.'',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes)<>34 OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>''WorkflowHeader'' OR EntityId<>@wfOwner)
    THROW 51137,''First workflow event must capture the complete header.'',1;
  END
  ELSE IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''WorkflowHeader'') THROW 51137,''Header only belongs in first event.'',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE EntityType WHEN ''WorkflowHeader'' THEN 34 WHEN ''WorkflowReference'' THEN 3 WHEN ''WorkflowCurrency'' THEN 2 WHEN ''WorkflowQuoteLine'' THEN 19 ELSE -1 END)
   THROW 51137,''Incomplete workflow row.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''WorkflowHeader'' AND
    ((FieldCode=''DocumentType'' AND (AfterState<>3 OR AfterValue<>CONVERT(nvarchar(50),@wfKind)))
    OR (FieldCode=''DocumentNumber'' AND (AfterState<>3 OR AfterValue<>CONVERT(nvarchar(50),@wfNumber)))))
   THROW 51137,''Header values do not identify the workflow.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''WorkflowReference'' AND
   (@operation<>''Document.EditFieldsSaved'' OR @wfKind<>7 OR NOT EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV b WITH(HOLDLOCK) WHERE b.T_ID=TRY_CONVERT(int,EntityId) AND b.Pr_ID=TRY_CONVERT(int,@wfOwner) AND b.BsType_ID IN(6,9) AND b.T_ID<>TRY_CONVERT(int,@wfOwner))))
   THROW 51137,''Unrelated linked reference.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''WorkflowCurrency'' AND
   (@operation NOT IN(''Purchase.AccountCurrencyChanged'',''Purchase.CurrencyChanged'') OR NOT EXISTS(SELECT 1 FROM dbo.Currency b WITH(HOLDLOCK) WHERE b.Cr_ID=TRY_CONVERT(int,EntityId))))
   THROW 51137,''Unrelated currency.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''WorkflowQuoteLine'' AND
   (@operation NOT IN(''Quote.Confirmed'',''Quote.Reserved'',''Quote.DiscountDistributed'') OR NOT EXISTS(SELECT 1 FROM dbo.ViewBill_Contents b WITH(HOLDLOCK) WHERE b.T_ID=TRY_CONVERT(int,EntityId) AND b.Bill_T_ID=TRY_CONVERT(int,@wfOwner))))
   THROW 51137,''Unrelated quote line.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE NOT(
   (EntityType=''WorkflowHeader'' AND (
    (@operation=''Document.EditFieldsSaved'' AND (FieldCode IN(''DocumentTotal'',''DocumentPure'',''Discount'',''DocumentDate'',''WorkflowAbout'') OR (@wfKind=1 AND FieldCode=''WorkflowProject'') OR (@wfKind=7 AND FieldCode=''WorkflowReference'')))
    OR (@operation=''Document.ProjectChanged'' AND FieldCode=''WorkflowProject'')
    OR (@operation=''Document.MetadataSaved'' AND FieldCode IN(''WorkflowAbout'',''DocumentDate'',''WorkflowProject''))
    OR (@operation=''Quote.EditFieldsSaved'' AND FieldCode IN(''WorkflowAbout'',''DocumentDate'',''DocumentTotal'',''DocumentPure''))
    OR (@operation=''Purchase.AccountCurrencyChanged'' AND FieldCode IN(''AccountId'',''AccountName'',''WorkflowExchange''))
    OR (@operation=''Purchase.CurrencyChanged'' AND FieldCode=''WorkflowExchange'')
    OR (@operation IN(''Quote.DiscountChanged'',''Quote.DiscountDistributed'') AND FieldCode=''Discount'')
    OR (@operation=''Quote.Confirmed'' AND FieldCode IN(''AccountId'',''AccountName'',''WorkflowAbout'',''DocumentDate'',''DocumentTotal'',''Discount'',''DocumentPure'',''Approved'',''SalesOpen'',''PaidState'',''WorkflowTableOrdered''))))
   OR (EntityType=''WorkflowReference'' AND FieldCode=''LinkedReference'' AND @operation=''Document.EditFieldsSaved'' AND @wfKind=7)
   OR (EntityType=''WorkflowCurrency'' AND FieldCode=''CurrencyRate'' AND @operation IN(''Purchase.AccountCurrencyChanged'',''Purchase.CurrencyChanged''))
   OR (EntityType=''WorkflowQuoteLine'' AND ((FieldCode=''QuoteApproved'' AND @operation IN(''Quote.Confirmed'',''Quote.Reserved'')) OR (FieldCode IN(''QuotePrice'',''QuoteLineTotal'') AND @operation=''Quote.DiscountDistributed''))))
   AND (BeforeState<>AfterState OR ISNULL(DATALENGTH(BeforeValue),-1)<>ISNULL(DATALENGTH(AfterValue),-1) OR ISNULL(BeforeValue,N'''') COLLATE Latin1_General_100_BIN2<>ISNULL(AfterValue,N'''') COLLATE Latin1_General_100_BIN2))
   THROW 51137,''Unexpected workflow effect.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.Agents_Balance_MV p WITH(HOLDLOCK) ON p.T_ID=TRY_CONVERT(int,c.EntityId) LEFT JOIN dbo.Agents a WITH(HOLDLOCK) ON a.AG_ID=p.AG_ID WHERE c.EntityType=''WorkflowHeader'' AND (p.T_ID IS NULL OR (c.FieldCode=''Voided'' AND ((c.AfterState=2 AND p.[isVoid] IS NOT NULL) OR (c.AfterState=3 AND (p.[isVoid] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isVoid])))) OR
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
    (c.FieldCode=''DocumentNumber'' AND ((c.AfterState=2 AND (CASE p.BsType_ID WHEN 1 THEN p.SB_ID WHEN 7 THEN p.Pch_ID WHEN 16 THEN p.ViewSB_ID END) IS NOT NULL) OR (c.AfterState=3 AND ((CASE p.BsType_ID WHEN 1 THEN p.SB_ID WHEN 7 THEN p.Pch_ID WHEN 16 THEN p.ViewSB_ID END) IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>(CASE p.BsType_ID WHEN 1 THEN p.SB_ID WHEN 7 THEN p.Pch_ID WHEN 16 THEN p.ViewSB_ID END))))) OR
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
    (c.FieldCode=''WorkflowTableOrdered'' AND ((c.AfterState=2 AND p.[TB_isOrderd] IS NOT NULL) OR (c.AfterState=3 AND (p.[TB_isOrderd] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[TB_isOrderd]))))))
   THROW 51137,''WorkflowHeader after-state does not match storage.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.Agents_Balance_MV p WITH(HOLDLOCK) ON p.T_ID=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''WorkflowReference'' AND (p.T_ID IS NULL OR (c.FieldCode=''LinkedReference'' AND ((c.AfterState=2 AND p.[ReferNum] IS NOT NULL) OR (c.AfterState=3 AND (p.[ReferNum] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[ReferNum]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[ReferNum]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''LinkedType'' AND ((c.AfterState=2 AND p.[BsType_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[BsType_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[BsType_ID])))) OR
    (c.FieldCode=''LinkedPeriod'' AND ((c.AfterState=2 AND p.[Pr_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Pr_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Pr_ID]))))))
   THROW 51137,''WorkflowReference after-state does not match storage.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.Currency p WITH(HOLDLOCK) ON p.Cr_ID=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''WorkflowCurrency'' AND (p.Cr_ID IS NULL OR (c.FieldCode=''CurrencyName'' AND ((c.AfterState=2 AND p.[Cr_Name] IS NOT NULL) OR (c.AfterState=3 AND (p.[Cr_Name] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Cr_Name]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Cr_Name]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''CurrencyRate'' AND ((c.AfterState=2 AND p.[Cr_Equal] IS NOT NULL) OR (c.AfterState=3 AND (p.[Cr_Equal] IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue)<>p.[Cr_Equal]))))))
   THROW 51137,''WorkflowCurrency after-state does not match storage.'',1;
  IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.ViewBill_Contents p WITH(HOLDLOCK) ON p.T_ID=TRY_CONVERT(int,c.EntityId) LEFT JOIN dbo.IM_Menu m WITH(HOLDLOCK) ON m.IM_ID=p.IM_ID WHERE c.EntityType=''WorkflowQuoteLine'' AND (p.T_ID IS NULL OR (c.FieldCode=''QuoteOwnerId'' AND ((c.AfterState=2 AND p.[Bill_T_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Bill_T_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Bill_T_ID])))) OR
    (c.FieldCode=''QuoteItemId'' AND ((c.AfterState=2 AND p.[IM_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[IM_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[IM_ID])))) OR
    (c.FieldCode=''QuoteUnitId'' AND ((c.AfterState=2 AND p.[U_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[U_ID] IS NULL OR TRY_CONVERT(bigint,c.AfterValue) IS NULL OR TRY_CONVERT(bigint,c.AfterValue)<>p.[U_ID])))) OR
    (c.FieldCode=''QuoteStoreId'' AND ((c.AfterState=2 AND p.[ST_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[ST_ID] IS NULL OR TRY_CONVERT(bigint,c.AfterValue) IS NULL OR TRY_CONVERT(bigint,c.AfterValue)<>p.[ST_ID])))) OR
    (c.FieldCode=''QuoteLineDate'' AND ((c.AfterState=2 AND p.[Date] IS NOT NULL) OR (c.AfterState=3 AND (p.[Date] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[Date])))) OR
    (c.FieldCode=''QuoteComponents'' AND ((c.AfterState=2 AND p.[Compons] IS NOT NULL) OR (c.AfterState=3 AND (p.[Compons] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Compons]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Compons]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''QuoteCost'' AND ((c.AfterState=2 AND p.[Cost] IS NOT NULL) OR (c.AfterState=3 AND (p.[Cost] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Cost])))) OR
    (c.FieldCode=''QuotePrice'' AND ((c.AfterState=2 AND p.[Price] IS NOT NULL) OR (c.AfterState=3 AND (p.[Price] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Price])))) OR
    (c.FieldCode=''QuoteExpiry'' AND ((c.AfterState=2 AND p.[D_Vaild] IS NOT NULL) OR (c.AfterState=3 AND (p.[D_Vaild] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[D_Vaild]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[D_Vaild]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''QuoteQuantity'' AND ((c.AfterState=2 AND p.[QTY] IS NOT NULL) OR (c.AfterState=3 AND (p.[QTY] IS NULL OR TRY_CONVERT(float(53),c.AfterValue) IS NULL OR TRY_CONVERT(float(53),c.AfterValue)<>p.[QTY])))) OR
    (c.FieldCode=''QuoteLineTotal'' AND ((c.AfterState=2 AND p.[T_Price] IS NOT NULL) OR (c.AfterState=3 AND (p.[T_Price] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[T_Price])))) OR
    (c.FieldCode=''QuoteCargo'' AND ((c.AfterState=2 AND p.[U_Cargo] IS NOT NULL) OR (c.AfterState=3 AND (p.[U_Cargo] IS NULL OR TRY_CONVERT(float(53),c.AfterValue) IS NULL OR TRY_CONVERT(float(53),c.AfterValue)<>p.[U_Cargo])))) OR
    (c.FieldCode=''QuoteStockQuantity'' AND ((c.AfterState=2 AND p.[ST_QTY] IS NOT NULL) OR (c.AfterState=3 AND (p.[ST_QTY] IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue)<>p.[ST_QTY])))) OR
    (c.FieldCode=''QuoteApproved'' AND ((c.AfterState=2 AND p.[isDepended] IS NOT NULL) OR (c.AfterState=3 AND (p.[isDepended] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isDepended])))) OR
    (c.FieldCode=''QuoteParentLine'' AND ((c.AfterState=2 AND p.[Apart_From] IS NOT NULL) OR (c.AfterState=3 AND (p.[Apart_From] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Apart_From])))) OR
    (c.FieldCode=''QuoteSalespersonPercent'' AND ((c.AfterState=2 AND p.[Saler_Percent] IS NOT NULL) OR (c.AfterState=3 AND (p.[Saler_Percent] IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue)<>p.[Saler_Percent])))) OR
    (c.FieldCode=''QuoteNotes'' AND ((c.AfterState=2 AND p.[Notes] IS NOT NULL) OR (c.AfterState=3 AND (p.[Notes] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Notes]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Notes]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''QuoteBarcode'' AND ((c.AfterState=2 AND p.[Barcode] IS NOT NULL) OR (c.AfterState=3 AND (p.[Barcode] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Barcode]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Barcode]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''QuoteItemName'' AND ((c.AfterState=2 AND m.item_name IS NOT NULL) OR (c.AfterState=3 AND (m.item_name IS NULL OR DATALENGTH(CONVERT(nvarchar(max),m.item_name))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),m.item_name) COLLATE Latin1_General_100_BIN2))))))
   THROW 51137,''WorkflowQuoteLine after-state does not match storage.'',1;
  IF @operation=''Quote.DiscountDistributed'' AND CHARINDEX(''QuoteDistribution037:'',ISNULL(OBJECT_DEFINITION(OBJECT_ID(''dbo.ViewBill_Discount_Distribute_Audited'')),N''''))=0
   THROW 51137,''Isolated quote distribution procedure missing.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Documents.Workflows"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Documents.Workflows"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF XACT_STATE()<>0 ROLLBACK;
 THROW;
END CATCH;
