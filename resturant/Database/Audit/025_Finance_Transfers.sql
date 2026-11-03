/* SQL Server 2014. Treasury movements and treasury/account transfers, atomic audit. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51125,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51125,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51125,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51125,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.Tables"]')=1) THROW 51125,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51125,'Incomplete audit requests.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('TreasuryMovement.Created',@sql)>0 OR CHARINDEX('Table.Transferred',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51125,'Unexpected begin baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''TreasuryMovement.Created'',''TreasuryMovement.Voided'',''TreasuryTransfer.Created'',''TreasuryTransfer.Voided'',''AccountTransfer.Created'',''AccountTransfer.Voided'') AND @DomainCode=''Finance.Transfers'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('TreasuryMovement.Created',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51125,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''TreasuryMovement.Created'',''TreasuryMovement.Voided'',''TreasuryTransfer.Created'',''TreasuryTransfer.Voided'',''AccountTransfer.Created'',''AccountTransfer.Voided'') AND @domain=''Finance.Transfers'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51125,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''TreasuryMovement.Created'',''TreasuryMovement.Voided'',''TreasuryTransfer.Created'',''TreasuryTransfer.Voided'',''AccountTransfer.Created'',''AccountTransfer.Voided'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51125,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditFinance025: full persisted finance rows; no business procedure changes.
 IF @operation IN(''TreasuryMovement.Created'',''TreasuryMovement.Voided'',''TreasuryTransfer.Created'',''TreasuryTransfer.Voided'',''AccountTransfer.Created'',''AccountTransfer.Voided'')
 BEGIN
  DECLARE @finOwner nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @finEntity varchar(64)=@EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)''),
   @finScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)''),
   @finCreate bit=CASE WHEN @operation LIKE ''%.Created'' THEN 1 ELSE 0 END,
   @finAccount bit=CASE WHEN @operation LIKE ''AccountTransfer.%'' THEN 1 ELSE 0 END,
   @finTransfer bit=CASE WHEN @operation LIKE ''TreasuryTransfer.%'' THEN 1 ELSE 0 END;
  IF ISNULL(TRY_CONVERT(int,@finOwner),0)<=0
   OR @finEntity<>CASE WHEN @finAccount=1 THEN ''FinanceAccountRow'' WHEN @finTransfer=1 THEN ''FinanceTreasuryTransfer'' ELSE ''FinanceTreasuryRow'' END
   OR @finScreen<>CASE WHEN @finAccount=1 THEN ''AG_Transfers'' WHEN @finTransfer=1 THEN ''Tr_Transfers'' ELSE ''Tr_Deposit_Withdraw'' END
   OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0
   OR @EventXml.value(''count(/Event/Document)'',''int'')<>1
   OR @EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)'')<>@finOwner THROW 51125,''Invalid financial identity.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@finEntity AND EntityId=CASE WHEN @finCreate=1 THEN ''New'' ELSE @finOwner END) THROW 51125,''Financial request mismatch.'',1;
  DECLARE @finFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @finFields VALUES
   (''FinanceTreasuryRow'',''CashAGBalanceTID'',''Integer'',''int''),
   (''FinanceTreasuryRow'',''CashTranID'',''Integer'',''int''),
   (''FinanceTreasuryRow'',''Cashdate'',''DateTime'',''datetime''),
   (''FinanceTreasuryRow'',''CashTrID'',''Integer'',''int''),
   (''FinanceTreasuryRow'',''Cashnoticemove'',''String'',''nvarchar(max)''),
   (''FinanceTreasuryRow'',''CashBankName'',''String'',''nvarchar(100)''),
   (''FinanceTreasuryRow'',''CashCheckNum'',''String'',''nvarchar(100)''),
   (''FinanceTreasuryRow'',''CashPure'',''Decimal'',''numeric(18,3)''),
   (''FinanceTreasuryRow'',''CashDebit'',''Decimal'',''numeric(18,3)''),
   (''FinanceTreasuryRow'',''CashCredit'',''Decimal'',''numeric(18,3)''),
   (''FinanceTreasuryRow'',''CashBalance'',''Decimal'',''numeric(18,3)''),
   (''FinanceTreasuryRow'',''CashTrTypeID'',''Integer'',''int''),
   (''FinanceTreasuryRow'',''CashisTranFrom'',''Boolean'',''bit''),
   (''FinanceTreasuryRow'',''CashisTranTo'',''Boolean'',''bit''),
   (''FinanceTreasuryRow'',''CashUserID'',''Integer'',''int''),
   (''FinanceTreasuryRow'',''CashisVoid'',''Boolean'',''bit''),
   (''FinanceTreasuryRow'',''CashisFirstTime'',''Integer'',''int''),
   (''FinanceTreasuryRow'',''CashJournalId'',''Integer'',''int''),
   (''FinanceTreasuryRow'',''CashPostedAt'',''DateTime'',''datetime''),
   (''FinanceTreasuryRow'',''CashPostedBy'',''Integer'',''int''),
   (''FinanceTreasuryRow'',''CashOriginalJournalId'',''Integer'',''int''),
   (''FinanceTreasuryRow'',''CashLastReversalJournalId'',''Integer'',''int''),
   (''FinanceTreasuryRow'',''CashNeedRepost'',''Boolean'',''bit''),
   (''FinanceTreasuryRow'',''CashEditVersion'',''Integer'',''int''),
   (''FinanceTreasuryRow'',''CashEditOpenedAt'',''DateTime'',''datetime''),
   (''FinanceTreasuryRow'',''CashEditOpenedBy'',''Integer'',''int''),
   (''FinanceTreasuryRow'',''CashLastRepostAt'',''DateTime'',''datetime''),
   (''FinanceTreasuryRow'',''CashLastRepostBy'',''Integer'',''int''),
   (''FinanceTreasuryRow'',''CashVoidAt'',''DateTime'',''datetime''),
   (''FinanceTreasuryRow'',''CashVoidBy'',''Integer'',''int''),
   (''FinanceTreasuryRow'',''CashVoidReason'',''String'',''nvarchar(500)''),
   (''FinanceTreasuryRow'',''CashVoidReversalJournalId'',''Integer'',''int''),
   (''FinanceTreasuryRow'',''CashNeedCancelReverse'',''Boolean'',''bit''),
   (''FinanceAccountRow'',''AccountDATE'',''DateTime'',''datetime''),
   (''FinanceAccountRow'',''AccountAGIDFROM'',''Integer'',''int''),
   (''FinanceAccountRow'',''AccountAGIDTO'',''Integer'',''int''),
   (''FinanceAccountRow'',''AccountTITLE'',''String'',''nvarchar(2500)''),
   (''FinanceAccountRow'',''AccountABOUT'',''String'',''nvarchar(max)''),
   (''FinanceAccountRow'',''AccountUSERID'',''Integer'',''int''),
   (''FinanceAccountRow'',''AccountisVoid'',''Integer'',''int''),
   (''FinanceAccountRow'',''AccountVALUE'',''Decimal'',''numeric(18,3)''),
   (''FinanceAccountRow'',''AccountBsTypeID'',''Integer'',''int''),
   (''FinanceAccountRow'',''AccountJournalId'',''Integer'',''int''),
   (''FinanceAccountRow'',''AccountPostedAt'',''DateTime'',''datetime''),
   (''FinanceAccountRow'',''AccountPostedBy'',''Integer'',''int''),
   (''FinanceAccountRow'',''AccountOriginalJournalId'',''Integer'',''int''),
   (''FinanceAccountRow'',''AccountLastReversalJournalId'',''Integer'',''int''),
   (''FinanceAccountRow'',''AccountNeedRepost'',''Boolean'',''bit''),
   (''FinanceAccountRow'',''AccountEditVersion'',''Integer'',''int''),
   (''FinanceAccountRow'',''AccountEditOpenedAt'',''DateTime'',''datetime''),
   (''FinanceAccountRow'',''AccountEditOpenedBy'',''Integer'',''int''),
   (''FinanceAccountRow'',''AccountLastRepostAt'',''DateTime'',''datetime''),
   (''FinanceAccountRow'',''AccountLastRepostBy'',''Integer'',''int''),
   (''FinanceAccountRow'',''AccountVoidAt'',''DateTime'',''datetime''),
   (''FinanceAccountRow'',''AccountVoidBy'',''Integer'',''int''),
   (''FinanceAccountRow'',''AccountVoidReason'',''String'',''nvarchar(500)''),
   (''FinanceAccountRow'',''AccountVoidReversalJournalId'',''Integer'',''int''),
   (''FinanceAccountRow'',''AccountNeedCancelReverse'',''Boolean'',''bit'');
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.ValueOrigin<>1 OR c.EntityType<>CASE WHEN @finAccount=1 THEN ''FinanceAccountRow'' ELSE ''FinanceTreasuryRow'' END OR NOT EXISTS(SELECT 1 FROM @finFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51125,''Invalid financial field.'',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE WHEN @finAccount=1 THEN 25 ELSE 33 END) THROW 51125,''Incomplete financial row.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE ISNULL(TRY_CONVERT(int,EntityId),0)<=0 OR (@finCreate=1 AND BeforeState<>1) OR (@finCreate=0 AND BeforeState NOT IN(2,3)) OR AfterState NOT IN(2,3)
   OR ((@finAccount=1 OR @operation=''TreasuryMovement.Created'') AND EntityId<>@finOwner)
   OR (@finTransfer=1 AND FieldCode=''CashTranID'' AND (AfterState<>3 OR AfterValue<>@finOwner OR (@finCreate=0 AND (BeforeState<>3 OR BeforeValue<>@finOwner))))) THROW 51125,''Invalid financial states.'',1;
  IF (@finAccount=1 OR @finCreate=1) AND (@EventOrdinal<>1 OR (SELECT COUNT(*) FROM @changes)<>CASE WHEN @finAccount=1 THEN 25 WHEN @finTransfer=1 THEN 66 ELSE 33 END) THROW 51125,''Incomplete financial event.'',1;
  IF @operation=''TreasuryMovement.Voided'' AND EXISTS(SELECT 1 FROM @changes c WHERE NOT EXISTS(SELECT 1 FROM dbo.Treasury_Balance_MV m WITH(HOLDLOCK) WHERE m.T_ID=TRY_CONVERT(int,c.EntityId) AND (m.T_ID=TRY_CONVERT(int,@finOwner) OR m.Tran_ID=(SELECT Tran_ID FROM dbo.Treasury_Balance_MV WITH(HOLDLOCK) WHERE T_ID=TRY_CONVERT(int,@finOwner))))) THROW 51125,''Unrelated treasury row.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Finance.Transfers"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Finance.Transfers"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
