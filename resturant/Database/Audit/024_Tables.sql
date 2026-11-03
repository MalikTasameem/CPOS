/* SQL Server 2014. Atomic table operations audit; business procedures are not changed. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51124,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51124,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51124,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51124,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.Items"]')=1) THROW 51124,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51124,'Incomplete audit requests.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Table.Transferred',@sql)>0 OR CHARINDEX('Item.FastUpdated',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51124,'Unexpected begin baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''Table.Transferred'',''Table.LineQuantityChanged'',''Table.LineDeleted'') AND @DomainCode=''Sales.Tables'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Table.Transferred',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51124,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''Table.Transferred'',''Table.LineQuantityChanged'',''Table.LineDeleted'') AND @domain=''Sales.Tables'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51124,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Table.Transferred'',''Table.LineQuantityChanged'',''Table.LineDeleted'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51124,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditTables024: persisted before/after values; queue identities Qn are request-local.
 IF @operation IN(''Table.Transferred'',''Table.LineQuantityChanged'',''Table.LineDeleted'')
 BEGIN
  DECLARE @tbOwner nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @tbEntity varchar(64)=@EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)''),
   @tbScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)''),
   @tbTransfer bit=CASE WHEN @operation=''Table.Transferred'' THEN 1 ELSE 0 END;
  IF ISNULL(TRY_CONVERT(int,@tbOwner),0)<=0 OR @tbEntity<>CASE WHEN @tbTransfer=1 THEN ''TableState'' ELSE ''TableInvoice'' END OR @tbScreen<>CASE WHEN @tbTransfer=1 THEN ''TablesBalance'' ELSE ''TB_BillIM'' END
   OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0 THROW 51124,''Invalid table identity.'',1;
  IF @tbTransfer=1 AND @EventXml.exist(''/Event/Document'')=1 THROW 51124,''Transfer does not have one invoice number.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@tbEntity AND EntityId=@tbOwner) THROW 51124,''Table request mismatch.'',1;
  DECLARE @tbFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @tbFields VALUES
   (''TableLine'',''ItemId'',''Integer'',''int''),
   (''TableLine'',''UnitId'',''Integer'',''bigint''),
   (''TableLine'',''StoreId'',''Integer'',''bigint''),
   (''TableLine'',''LineQuantity'',''Decimal'',''numeric(18,3)''),
   (''TableLine'',''LinePrice'',''Decimal'',''numeric(18,3)''),
   (''TableLine'',''LineTotal'',''Decimal'',''numeric(18,3)''),
   (''TableLine'',''LineCost'',''Float'',''float(53)''),
   (''TableLine'',''UnitCargo'',''Float'',''float(53)''),
   (''TableLine'',''StoredQuantity'',''Decimal'',''numeric(18,2)''),
   (''TableLine'',''LineDiscount'',''Float'',''float(53)''),
   (''TableLine'',''LineNotes'',''String'',''nvarchar(max)''),
   (''TableLine'',''ExpiryText'',''String'',''nvarchar(50)''),
   (''TableLine'',''LineApproved'',''Integer'',''int''),
   (''TableLine'',''LineDate'',''DateTime'',''datetime''),
   (''TableLine'',''MinimumPriceUsed'',''Integer'',''int''),
   (''TableLine'',''MinimumPrice2Used'',''Integer'',''int''),
   (''TableLine'',''LineDocumentId'',''Integer'',''int''),
   (''TableLine'',''LineComponentsText'',''String'',''nvarchar(max)''),
   (''TableLine'',''ParentLineId'',''Integer'',''int''),
   (''TableLine'',''SalespersonPercent'',''Decimal'',''numeric(18,3)''),
   (''TableLine'',''MarketerValue'',''Decimal'',''numeric(18,3)''),
   (''TableLine'',''LineBarcode'',''String'',''nvarchar(max)''),
   (''TableLine'',''LineSerial'',''String'',''nvarchar(100)''),
   (''TableLine'',''LineChecked'',''Boolean'',''bit''),
   (''TableInvoice'',''DocumentTotal'',''Decimal'',''numeric(18,3)''),
   (''TableInvoice'',''DocumentPure'',''Decimal'',''numeric(18,3)''),
   (''TableInvoice'',''Discount'',''Decimal'',''numeric(18,3)''),
   (''TableInvoice'',''DocumentProfit'',''Float'',''float(53)''),
   (''TableInvoice'',''Approved'',''Integer'',''int''),
   (''TableInvoice'',''Voided'',''Integer'',''int''),
   (''TableInvoice'',''NeedRepost'',''Boolean'',''bit''),
   (''TableInvoice'',''AccountId'',''Integer'',''int''),
   (''TableInvoice'',''TableId'',''Integer'',''int''),
   (''TableInvoice'',''InvoiceTableCode'',''String'',''nvarchar(100)''),
   (''TableInvoice'',''InvoiceNumber'',''Integer'',''int''),
   (''TableInvoice'',''InvoiceType'',''Integer'',''int''),
   (''TableInvoice'',''InvoicePaid'',''Integer'',''int''),
   (''TableInvoice'',''InvoiceOrdered'',''Integer'',''int''),
   (''TableInvoice'',''InvoiceOwner'',''Integer'',''int''),
   (''TableState'',''TableFloor'',''Integer'',''int''),
   (''TableState'',''TableName'',''String'',''nvarchar(100)''),
   (''TableState'',''TableActive'',''Boolean'',''bit''),
   (''TableState'',''TableReserved'',''Boolean'',''bit''),
   (''TableState'',''TableBusy'',''Boolean'',''bit''),
   (''TableState'',''TableOrderCode'',''String'',''nvarchar(100)''),
   (''TableState'',''TableCash'',''Integer'',''int''),
   (''TableState'',''TableOpenedAt'',''DateTime'',''datetime''),
   (''TableState'',''TablePrint'',''Boolean'',''bit''),
   (''TablePrintRow'',''QueueStoredId'',''Integer'',''int''),
   (''TablePrintRow'',''QueueDate'',''DateTime'',''datetime''),
   (''TablePrintRow'',''QueueNotes'',''String'',''nvarchar(max)''),
   (''TablePrintRow'',''QueuePrinter'',''String'',''nvarchar(100)'');
  IF EXISTS(SELECT 1 FROM @changes c WHERE c.ValueOrigin<>1 OR NOT EXISTS(SELECT 1 FROM @tbFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51124,''Invalid table field.'',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE EntityType WHEN ''TableState'' THEN 9 WHEN ''TableInvoice'' THEN 15 WHEN ''TableLine'' THEN 24 WHEN ''TablePrintRow'' THEN 4 ELSE -1 END) THROW 51124,''Incomplete table row.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE
   (EntityType<>''TablePrintRow'' AND ISNULL(TRY_CONVERT(int,EntityId),0)<=0)
   OR (EntityType=''TablePrintRow'' AND (LEFT(EntityId,1)<>''Q'' OR ISNULL(TRY_CONVERT(int,SUBSTRING(EntityId,2,127)),0)<=0))
   OR (EntityType=''TablePrintRow'' AND BeforeState<>1)
   OR (EntityType<>''TablePrintRow'' AND BeforeState NOT IN(2,3))
   OR (@operation=''Table.LineDeleted'' AND EntityType=''TableLine'' AND AfterState<>1)
   OR ((@operation<>''Table.LineDeleted'' OR EntityType<>''TableLine'') AND AfterState NOT IN(2,3))
   OR (@tbTransfer=1 AND EntityType NOT IN(''TableState'',''TableInvoice''))
   OR (@tbTransfer=0 AND EntityType NOT IN(''TableInvoice'',''TableLine'',''TablePrintRow''))
   OR (@tbTransfer=0 AND EntityType=''TableInvoice'' AND EntityId<>@tbOwner)
   OR (FieldCode=''LineDocumentId'' AND (BeforeValue<>@tbOwner OR (@operation<>''Table.LineDeleted'' AND AfterValue<>@tbOwner)))) THROW 51124,''Invalid table row states.'',1;
  IF @EventOrdinal=1
  BEGIN
   IF @tbTransfer=1 AND ((SELECT COUNT(*) FROM @changes)<>18 OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>''TableState'') OR NOT EXISTS(SELECT 1 FROM @changes WHERE EntityId=@tbOwner)) THROW 51124,''Invalid transfer header.'',1;
   IF @tbTransfer=0 AND ((SELECT COUNT(*) FROM @changes)<>39 OR (SELECT COUNT(*) FROM @changes WHERE EntityType=''TableInvoice'')<>15 OR (SELECT COUNT(*) FROM @changes WHERE EntityType=''TableLine'')<>24) THROW 51124,''Invalid table line header.'',1;
  END
  ELSE IF EXISTS(SELECT 1 FROM @changes WHERE (@tbTransfer=1 AND EntityType<>''TableInvoice'') OR (@tbTransfer=0 AND EntityType<>''TablePrintRow'')) THROW 51124,''Invalid table detail part.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.Tables"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.Tables"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
