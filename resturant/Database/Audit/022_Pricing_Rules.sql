/* SQL Server 2014. Offers and percentage rules audit only. Business procedures/tables unchanged. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51122,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51122,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51122,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51122,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.ItemUnits"]')=1) THROW 51122,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51122,'Incomplete audit requests.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Offer.Created',@sql)>0 OR CHARINDEX('ItemUnit.Created',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51122,'Unexpected begin baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''Offer.Created'',''Offer.ItemOffersDeleted'',''PriceRule.Created'',''PriceRule.Deleted'') AND @DomainCode=''Catalog.PricingRules'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Offer.Created',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51122,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''Offer.Created'',''Offer.ItemOffersDeleted'',''PriceRule.Created'',''PriceRule.Deleted'') AND @domain=''Catalog.PricingRules'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51122,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Offer.Created'',''Offer.ItemOffersDeleted'',''PriceRule.Created'',''PriceRule.Deleted'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51122,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditPricingRules022: multiset offers (S references are request-local) and keyed percentage rules.
 IF @operation IN(''Offer.Created'',''Offer.ItemOffersDeleted'',''PriceRule.Created'',''PriceRule.Deleted'')
 BEGIN
  DECLARE @pcOffer bit=CASE WHEN @operation LIKE ''Offer.%'' THEN 1 ELSE 0 END,
   @pcOwner nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @pcTarget nvarchar(50)=@EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)''),
   @pcScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'');
  DECLARE @pcParent varchar(64)=CASE WHEN @pcOffer=1 THEN ''ItemOffers'' ELSE ''PriceRuleGroup'' END,
   @pcChild varchar(64)=CASE WHEN @pcOffer=1 THEN ''OfferSnapshotRow'' ELSE ''PriceRule'' END;
  IF @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>@pcParent OR ISNULL(TRY_CONVERT(int,@pcOwner),0)<=0 OR ISNULL(TRY_CONVERT(int,@pcTarget),0)<=0
   OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0 OR (@pcOffer=1 AND @pcTarget<>@pcOwner) THROW 51122,''Invalid pricing identity.'',1;
  IF @pcScreen<>CASE WHEN @pcOffer=1 THEN ''OFFERS'' ELSE ''IM_Change_Price_ByPercent'' END THROW 51122,''Invalid pricing screen.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@pcParent AND EntityId=@pcOwner) THROW 51122,''Pricing request mismatch.'',1;
  DECLARE @pcFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @pcFields VALUES
   (''ItemOffers'',''OfferItemName'',''String'',''nvarchar(500)''),
   (''ItemOffers'',''OfferItemSalesName'',''String'',''nvarchar(500)''),
   (''PriceRuleGroup'',''RuleGroupName'',''String'',''nvarchar(200)''),
   (''OfferSnapshotRow'',''OfferItemId'',''Integer'',''int''),
   (''OfferSnapshotRow'',''OfferCreatedAt'',''DateTime'',''datetime''),
   (''OfferSnapshotRow'',''OfferTitle'',''String'',''nvarchar(4000)''),
   (''OfferSnapshotRow'',''OfferStartDate'',''Date'',''date''),
   (''OfferSnapshotRow'',''OfferEndDate'',''Date'',''date''),
   (''OfferSnapshotRow'',''OfferStartTime'',''String'',''time(0)''),
   (''OfferSnapshotRow'',''OfferEndTime'',''String'',''time(0)''),
   (''OfferSnapshotRow'',''OfferUserId'',''Integer'',''int''),
   (''PriceRule'',''RuleCreatedAt'',''DateTime'',''datetime''),
   (''PriceRule'',''RuleGroupId'',''Integer'',''int''),
   (''PriceRule'',''RulePercent'',''Decimal'',''numeric(18,2)'');
  IF EXISTS(SELECT 1 FROM @changes c WHERE ValueOrigin<>1 OR NOT EXISTS(SELECT 1 FROM @pcFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51122,''Invalid pricing field.'',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE EntityType WHEN ''ItemOffers'' THEN 2 WHEN ''PriceRuleGroup'' THEN 1 WHEN ''OfferSnapshotRow'' THEN 8 ELSE 3 END
   OR MIN(CASE WHEN BeforeState=1 THEN 1 ELSE 0 END)<>MAX(CASE WHEN BeforeState=1 THEN 1 ELSE 0 END)
   OR MIN(CASE WHEN AfterState=1 THEN 1 ELSE 0 END)<>MAX(CASE WHEN AfterState=1 THEN 1 ELSE 0 END)) THROW 51122,''Incomplete pricing row.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE BeforeState=0 OR AfterState=0 OR (BeforeState=1 AND AfterState=1)) THROW 51122,''Uncaptured pricing values.'',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes)<>CASE WHEN @pcOffer=1 THEN 2 ELSE 1 END OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>@pcParent OR EntityId<>@pcOwner OR BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3)) THROW 51122,''Invalid pricing context.'',1;
  END
  ELSE
  BEGIN
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType<>@pcChild OR (@pcOffer=0 AND ISNULL(TRY_CONVERT(int,EntityId),0)<=0)
    OR (@pcOffer=1 AND (LEFT(EntityId,1)<>''S'' OR SUBSTRING(EntityId,2,127) LIKE ''%[^0-9]%'' OR ISNULL(TRY_CONVERT(int,SUBSTRING(EntityId,2,127)),0)<=0))
    OR (FieldCode IN(''OfferItemId'',''RuleGroupId'') AND ((BeforeState=3 AND BeforeValue<>@pcOwner) OR (AfterState=3 AND AfterValue<>@pcOwner)))
    OR (@operation IN(''Offer.Created'',''PriceRule.Created'') AND AfterState NOT IN(2,3))
    OR (@operation IN(''Offer.ItemOffersDeleted'',''PriceRule.Deleted'') AND BeforeState NOT IN(2,3))
    OR (@operation=''Offer.ItemOffersDeleted'' AND AfterState<>1)
    OR (@operation=''PriceRule.Created'' AND (BeforeState<>1 OR EntityId<>@pcTarget))
    OR (@operation=''PriceRule.Deleted'' AND EntityId=@pcTarget AND AfterState<>1)
    OR (@pcOffer=0 AND (BeforeState=2 OR AfterState=2))) THROW 51122,''Invalid pricing detail states.'',1;
  END;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Catalog.PricingRules"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.PricingRules"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;

