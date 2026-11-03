ALTER PROCEDURE dbo.Network_Audit_Request_Begin
 @SourceId uniqueidentifier,@RequestId uniqueidentifier,@OperationCode varchar(64),@DomainCode varchar(64),
 @ActorKind tinyint,@ActorUserId int=NULL,@ActorServiceCode varchar(64)=NULL,@SessionId uniqueidentifier=NULL,
 @EntityType varchar(64)=NULL,@EntityId nvarchar(128)=NULL,
 @RequestFingerprint varbinary(32),@CanonicalRequest varbinary(max),@LockTimeout int=5000
AS
BEGIN
 SET NOCOUNT ON; SET XACT_ABORT ON;
 IF @@TRANCOUNT=0 OR XACT_STATE()<>1 THROW 51100,'Audit requires the caller writable transaction.',1;
 IF @SourceId IS NULL OR @RequestId IS NULL OR @RequestId='00000000-0000-0000-0000-000000000000'
    OR DATALENGTH(@RequestFingerprint)<>32 OR ISNULL(DATALENGTH(@CanonicalRequest),0) NOT BETWEEN 1 AND 1048576
    OR @LockTimeout NOT BETWEEN 0 AND 10000 THROW 51101,'Invalid audit request contract.',1;
 IF NOT ((@OperationCode='AuditTest.Changed' AND @DomainCode='AuditTest') OR (@OperationCode IN('Quote.LineAdded','Quote.LineDeleted','Quote.LineChanged','Quote.LineQuantityStepped') AND @DomainCode='Sales.QuoteLines') OR (@OperationCode IN('Document.AccountChanged','Document.DiscountChanged','Document.FastDiscountChanged') AND @DomainCode='Documents.DirectFields') OR (@OperationCode IN('Purchase.LineAdded') AND @DomainCode='Inventory.PurchaseAddition') OR (@OperationCode IN('AccountEntry.Created') AND @DomainCode='Finance.AccountEntries') OR (@OperationCode IN('Expense.LineAdded','Expense.LineDeleted') AND @DomainCode='Expenses.Lines') OR (@OperationCode IN('Return.LineAdded','Return.LineDeleted','Return.LineQuantityChanged') AND @DomainCode='Sales.ReturnLines') OR (@OperationCode IN('EmployeeValue.Created','EmployeeValue.Updated','EmployeeValue.Voided') AND @DomainCode='People.EmployeeValues') OR (@OperationCode IN('StockDocument.LineAdded','StockDocument.LineDeleted','StockDocument.LineQuantityChanged') AND @DomainCode='Inventory.DocumentLines') OR (@OperationCode IN('TreasuryMovement.Created','TreasuryMovement.Voided','TreasuryTransfer.Created','TreasuryTransfer.Voided','AccountTransfer.Created','AccountTransfer.Voided') AND @DomainCode='Finance.Transfers') OR (@OperationCode IN('Table.Transferred','Table.LineQuantityChanged','Table.LineDeleted') AND @DomainCode='Sales.Tables') OR (@OperationCode IN('Item.FastUpdated','Item.Deleted') AND @DomainCode='Catalog.Items') OR (@OperationCode IN('Offer.Created','Offer.ItemOffersDeleted','PriceRule.Created','PriceRule.Deleted') AND @DomainCode='Catalog.PricingRules') OR (@OperationCode IN('ItemUnit.Created','ItemUnit.Updated','ItemUnit.Deleted','ItemUnit.PriceChanged','ItemUnit.OfferPriceChanged') AND @DomainCode='Catalog.ItemUnits') OR (@OperationCode IN('Group.Created','Group.Updated','Group.Deleted','Group.PrinterLinked','Group.PrinterUnlinked') AND @DomainCode='Catalog.Groups') OR (@OperationCode IN('Unit.Created','Unit.Updated','Unit.Deleted') AND @DomainCode='Catalog.Units') OR (@OperationCode='Document.Voided' AND @DomainCode='Documents.Cancellation') OR (@OperationCode='Sales.LineAdded' AND @DomainCode='Sales.LineLifecycle') OR (@OperationCode='Sales.LineDeleted' AND @DomainCode='Sales.LineLifecycle') OR (@OperationCode='Sales.LineQuantityStepped' AND @DomainCode='Sales.LineQuantity') OR (@OperationCode IN('Sales.LineDetailsChanged','Sales.LinePriceChanged') AND @DomainCode='Sales.LineEditing') OR (@OperationCode='Document.EditOpened' AND @DomainCode='Documents.EditOpening') OR (@OperationCode='Sales.EditOpened' AND @DomainCode='Sales.EditOpening')
     OR (@OperationCode IN('Settlement.LineAdded','Settlement.LineDeleted','Settlement.LineUpdated','Settlement.Created','Settlement.Approved','Settlement.Voided','Settlement.EditOpened','Settlement.HeaderSaved','Settlement.EditFinished') AND @DomainCode='Inventory.Settlement')
     OR (@OperationCode IN('Security.SessionStarted','Security.LoginRejected','Security.SessionEnded') AND @DomainCode='Security'))
     THROW 51101,'Unknown operation/domain.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND SchemaVersion=1 AND SourceId=@SourceId)
     THROW 51102,'Audit source/schema mismatch.',1;
 DECLARE @resource nvarchar(255)=N'CPOS.Audit.Request.'+CONVERT(nvarchar(36),@SourceId)+N'.'+CONVERT(nvarchar(36),@RequestId),@lock int;
 EXEC @lock=sys.sp_getapplock @Resource=@resource,@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=@LockTimeout;
 IF @lock<0 THROW 51103,'Audit request is busy or lock acquisition failed; retry same identity.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId)
 BEGIN
   IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId
      AND RequestFingerprint=@RequestFingerprint AND DATALENGTH(CanonicalRequest)=DATALENGTH(@CanonicalRequest) AND CanonicalRequest=@CanonicalRequest)
      THROW 51104,'Request identity conflicts with different content.',1;
   IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND State<>1)
      THROW 51105,'An incomplete request exists. Do not execute again.',1;
   SELECT CAST('Completed' AS varchar(30)) Status,ResultCode,CONVERT(nvarchar(max),ResultXml) ResultXml
      FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId;
   RETURN;
 END;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code=sql:variable("@DomainCode")]')=1)
     THROW 51102,'Audit domain is disabled. No legacy fallback is allowed.',1;
 INSERT dbo.Network_Audit_Request(SourceId,RequestId,ContractVersion,OperationCode,ActorKind,ActorUserId,ActorServiceCode,SessionId,
 EntityType,EntityId,RequestFingerprint,CanonicalRequest,State)
 VALUES(@SourceId,@RequestId,1,@OperationCode,@ActorKind,@ActorUserId,@ActorServiceCode,@SessionId,@EntityType,@EntityId,@RequestFingerprint,@CanonicalRequest,0);
 SELECT CAST('NewPendingCommit' AS varchar(30)) Status,CAST(NULL AS tinyint) ResultCode,CAST(NULL AS nvarchar(max)) ResultXml;
END
