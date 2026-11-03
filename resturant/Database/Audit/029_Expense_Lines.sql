/* SQL Server 2014. Expense lines only; original procedures and table schemas unchanged. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51129,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51129,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51129,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51129,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.ReturnLines"]')=1) THROW 51129,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51129,'Incomplete audit requests.',1;
 IF EXISTS(SELECT 1 FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('EXP_Details'),OBJECT_ID('Agents_Balance_MV'))) THROW 51129,'Review business triggers before deployment.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Expense.LineAdded',@sql)>0 OR CHARINDEX('Return.LineAdded',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51129,'Unexpected begin baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''Expense.LineAdded'',''Expense.LineDeleted'') AND @DomainCode=''Expenses.Lines'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Expense.LineAdded',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51129,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''Expense.LineAdded'',''Expense.LineDeleted'') AND @domain=''Expenses.Lines'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51129,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Expense.LineAdded'',''Expense.LineDeleted'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51129,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditExpenseLines029: complete affected line and recalculated header.
 IF @operation IN(''Expense.LineAdded'',''Expense.LineDeleted'')
 BEGIN
  DECLARE @elOwner nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @elEntity varchar(64)=@EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)''),
   @elScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'');
  IF @elEntity<>''ExpenseDocument'' OR ISNULL(TRY_CONVERT(int,@elOwner),0)<=0 OR @elScreen<>''EXP_Details'' OR @EventOrdinal<>1
   OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0 OR @EventXml.value(''count(/Event/Document)'',''int'')<>1
   OR NOT EXISTS(SELECT 1 FROM dbo.Agents_Balance_MV WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@elOwner) AND BsType_ID=2 AND isVoid=0)
   THROW 51129,''Invalid expense document identity.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@elEntity AND EntityId=@elOwner) THROW 51129,''Expense request mismatch.'',1;
  DECLARE @elFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @elFields VALUES
   (''ExpenseDocument'',''Voided'',''Integer'',''int''),
   (''ExpenseDocument'',''NeedRepost'',''Boolean'',''bit''),
   (''ExpenseDocument'',''NeedCancelReverse'',''Boolean'',''bit''),
   (''ExpenseDocument'',''VoidAt'',''DateTime'',''datetime''),
   (''ExpenseDocument'',''VoidBy'',''Integer'',''int''),
   (''ExpenseDocument'',''VoidReason'',''String'',''nvarchar(500)''),
   (''ExpenseDocument'',''JournalId'',''Integer'',''int''),
   (''ExpenseDocument'',''DocumentTotal'',''Decimal'',''numeric(18,3)''),
   (''ExpenseDocument'',''DocumentPure'',''Decimal'',''numeric(18,3)''),
   (''ExpenseDocument'',''Discount'',''Decimal'',''numeric(18,3)''),
   (''ExpenseDocument'',''Debit'',''Decimal'',''numeric(18,3)''),
   (''ExpenseDocument'',''Credit'',''Decimal'',''numeric(18,3)''),
   (''ExpenseDocument'',''Balance'',''Decimal'',''numeric(18,3)''),
   (''ExpenseDocument'',''AccountId'',''Integer'',''int''),
   (''ExpenseDocument'',''Approved'',''Integer'',''int''),
   (''ExpenseDocument'',''DocumentType'',''Integer'',''int''),
   (''ExpenseDocument'',''DocumentNumber'',''Integer'',''int''),
   (''ExpenseDocument'',''DocumentDate'',''DateTime'',''datetime''),
   (''ExpenseDocument'',''SalesOpen'',''Boolean'',''bit''),
   (''ExpenseDocument'',''Paused'',''Boolean'',''bit''),
   (''ExpenseDocument'',''OrderDelivered'',''Boolean'',''bit''),
   (''ExpenseDocument'',''PaidState'',''Integer'',''int''),
   (''ExpenseDocument'',''TableId'',''Integer'',''int''),
   (''ExpenseLine'',''ExpenseDocumentId'',''Integer'',''int''),
   (''ExpenseLine'',''ExpenseDate'',''DateTime'',''datetime''),
   (''ExpenseLine'',''ExpenseId'',''Integer'',''bigint''),
   (''ExpenseLine'',''ExpenseQuantity'',''Decimal'',''numeric(18,3)''),
   (''ExpenseLine'',''ExpensePrice'',''Decimal'',''numeric(18,3)''),
   (''ExpenseLine'',''ExpenseTotal'',''Decimal'',''numeric(18,3)''),
   (''ExpenseLine'',''ExpenseApproved'',''Boolean'',''bit'');
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.ValueOrigin<>1 OR NOT EXISTS(SELECT 1 FROM @elFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51129,''Invalid expense field.'',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>(SELECT COUNT(*) FROM @elFields f WHERE f.E=EntityType)) THROW 51129,''Incomplete expense row.'',1;
  IF (SELECT COUNT(*) FROM @changes WHERE EntityType=''ExpenseDocument'')<>23 OR (SELECT COUNT(*) FROM @changes WHERE EntityType=''ExpenseLine'')<>7 THROW 51129,''Expense requires header and one line.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE ISNULL(TRY_CONVERT(int,EntityId),0)<=0 OR (EntityType=''ExpenseDocument'' AND EntityId<>@elOwner)) THROW 51129,''Invalid expense row identity.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE (EntityType=''ExpenseDocument'' AND (BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3))) OR (EntityType=''ExpenseLine'' AND (
   (@operation=''Expense.LineAdded'' AND (BeforeState<>1 OR AfterState NOT IN(2,3))) OR
   (@operation=''Expense.LineDeleted'' AND (AfterState<>1 OR BeforeState NOT IN(2,3)))))) THROW 51129,''Invalid expense value states.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''ExpenseDocument'' AND FieldCode=''DocumentType'' AND (BeforeValue<>''2'' OR AfterValue<>''2'')) THROW 51129,''Expense type mismatch.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''ExpenseLine'' AND FieldCode=''ExpenseDocumentId'' AND CASE WHEN @operation=''Expense.LineDeleted'' THEN BeforeValue ELSE AfterValue END<>@elOwner) THROW 51129,''Unrelated expense line.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''ExpenseDocument'' AND FieldCode=''DocumentNumber'' AND (AfterState<>3 OR AfterValue<>@EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)''))) THROW 51129,''Expense number mismatch.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Expenses.Lines"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Expenses.Lines"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
