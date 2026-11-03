/* SQL Server 2014. Item deletion/fast management audit; original business procedures unchanged. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51123,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51123,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51123,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51123,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.PricingRules"]')=1) THROW 51123,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51123,'Incomplete audit requests.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Item.FastUpdated',@sql)>0 OR CHARINDEX('Offer.Created',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51123,'Unexpected begin baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''Item.FastUpdated'',''Item.Deleted'') AND @DomainCode=''Catalog.Items'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Item.FastUpdated',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51123,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''Item.FastUpdated'',''Item.Deleted'') AND @domain=''Catalog.Items'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51123,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Item.FastUpdated'',''Item.Deleted'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51123,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditItemMaster023: source values plus explicitly derived image hash/length, never image bytes.
 IF @operation IN(''Item.FastUpdated'',''Item.Deleted'')
 BEGIN
  DECLARE @imOwner nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @imScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'');
  IF @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>''CatalogItemRecord'' OR ISNULL(TRY_CONVERT(int,@imOwner),0)<=0
   OR @EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)'')<>@imOwner OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0 THROW 51123,''Invalid item identity.'',1;
  IF @imScreen<>''IM_Fast_Mang'' AND NOT(@operation=''Item.Deleted'' AND @imScreen=''ItemsMenu'') THROW 51123,''Invalid item screen.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=''CatalogItemRecord'' AND EntityId=@imOwner) THROW 51123,''Item request mismatch.'',1;
  DECLARE @imFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40),O tinyint);
  INSERT @imFields VALUES
   (''CatalogItemRecord'',''ItemName'',''String'',''nvarchar(500)'',1),
   (''CatalogItemRecord'',''ItemSalesName'',''String'',''nvarchar(500)'',1),
   (''CatalogItemRecord'',''ItemGroupId'',''Integer'',''int'',1),
   (''CatalogItemRecord'',''ItemPhotoSha256'',''String'',''image'',3),
   (''CatalogItemRecord'',''ItemPhotoBytes'',''Integer'',''image'',3),
   (''CatalogItemRecord'',''ItemActive'',''Boolean'',''bit'',1),
   (''CatalogItemRecord'',''ItemStoreType'',''Integer'',''int'',1),
   (''CatalogItemRecord'',''ItemUsesExpiry'',''Integer'',''int'',1),
   (''CatalogItemRecord'',''ItemCost'',''Float'',''float(53)'',1),
   (''CatalogItemRecord'',''ItemVariablePrice'',''Boolean'',''bit'',1),
   (''CatalogItemRecord'',''ItemBKR'',''Integer'',''int'',1),
   (''CatalogItemRecord'',''ItemBKG'',''Integer'',''int'',1),
   (''CatalogItemRecord'',''ItemBKB'',''Integer'',''int'',1),
   (''CatalogItemRecord'',''ItemFKR'',''Integer'',''int'',1),
   (''CatalogItemRecord'',''ItemFKG'',''Integer'',''int'',1),
   (''CatalogItemRecord'',''ItemFKB'',''Integer'',''int'',1),
   (''CatalogItemRecord'',''ItemUserId'',''Integer'',''int'',1),
   (''CatalogItemRecord'',''ItemEnabled'',''Boolean'',''bit'',1),
   (''CatalogItemRecord'',''ItemLastPurchaseId'',''Integer'',''int'',1),
   (''CatalogItemRecord'',''ItemLastPurchaseCargo'',''Decimal'',''numeric(18,3)'',1),
   (''CatalogItemRecord'',''ItemShortcut'',''Integer'',''int'',1),
   (''CatalogItemRecord'',''ItemNumber'',''String'',''nvarchar(100)'',1),
   (''CatalogItemRecord'',''ItemCompanyGroup'',''Integer'',''int'',1),
   (''CatalogItemRecord'',''ItemPhotoPath'',''String'',''nvarchar(max)'',1),
   (''CatalogItemRecord'',''ItemNotes'',''String'',''nvarchar(max)'',1),
   (''CatalogItemRecord'',''ItemMarketerValue'',''Decimal'',''numeric(18,3)'',1),
   (''CatalogItemRecord'',''ItemDate'',''DateTime'',''datetime'',1),
   (''CatalogItemRecord'',''ItemMaxAlert'',''Decimal'',''numeric(18,2)'',1),
   (''CatalogItemRecord'',''ItemMinAlert'',''Decimal'',''numeric(18,2)'',1),
   (''ItemAlert'',''AlertItemId'',''Integer'',''int'',1),
   (''ItemAlert'',''AlertStoreId'',''Integer'',''bigint'',1),
   (''ItemAlert'',''AlertMinimum'',''Decimal'',''numeric(18,3)'',1),
   (''ItemAlert'',''AlertMaximum'',''Decimal'',''numeric(18,3)'',1),
   (''ItemAlert'',''AlertEnabled'',''Boolean'',''bit'',1),
   (''ItemAlert'',''AlertUserId'',''Integer'',''bigint'',1),
   (''ItemFormat'',''FormatItemId'',''Integer'',''int'',1),
   (''ItemFormat'',''FormatComponentId'',''Integer'',''int'',1),
   (''ItemFormat'',''FormatUnitId'',''Integer'',''bigint'',1),
   (''ItemFormat'',''FormatQuantity'',''Float'',''float(53)'',1),
   (''ItemFormat'',''FormatEnabled'',''Boolean'',''bit'',1),
   (''ItemFormat'',''FormatUserId'',''Integer'',''bigint'',1),
   (''ItemDifference'',''DifferenceBillId'',''Integer'',''int'',1),
   (''ItemDifference'',''DifferenceDate'',''DateTime'',''datetime'',1),
   (''ItemDifference'',''DifferenceItemId'',''Integer'',''int'',1),
   (''ItemDifference'',''DifferenceStoreId'',''Integer'',''int'',1),
   (''ItemDifference'',''DifferenceExpiry'',''String'',''nvarchar(50)'',1),
   (''ItemDifference'',''DifferenceQuantity'',''Float'',''float(53)'',1),
   (''ItemDifference'',''DifferenceCost'',''Decimal'',''numeric(18,3)'',1),
   (''ItemDifference'',''DifferenceCargo'',''Decimal'',''numeric(18,3)'',1),
   (''ItemDifference'',''DifferenceBarcode'',''String'',''nvarchar(50)'',1),
   (''ItemDifference'',''DifferenceTotal'',''Decimal'',''numeric(18,3)'',1),
   (''ItemUnit'',''ItemId'',''Integer'',''int'',1),
   (''ItemUnit'',''UnitId'',''Integer'',''int'',1),
   (''ItemUnit'',''UnitBarcode'',''String'',''nvarchar(max)'',1),
   (''ItemUnit'',''UnitPrice'',''Decimal'',''numeric(18,3)'',1),
   (''ItemUnit'',''MinimumPrice'',''Decimal'',''numeric(18,3)'',1),
   (''ItemUnit'',''MinimumPrice2'',''Decimal'',''numeric(18,3)'',1),
   (''ItemUnit'',''DefaultFlag'',''Integer'',''int'',1),
   (''ItemUnit'',''UnitRowEnabled'',''Boolean'',''bit'',1),
   (''ItemUnit'',''UnitUserId'',''Integer'',''int'',1),
   (''ItemUnit'',''OfferPrice'',''Decimal'',''numeric(18,3)'',1);
  IF EXISTS(SELECT 1 FROM @changes c WHERE NOT EXISTS(SELECT 1 FROM @imFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType AND f.O=c.ValueOrigin)) THROW 51123,''Invalid item field.'',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE EntityType WHEN ''CatalogItemRecord'' THEN 29 WHEN ''ItemAlert'' THEN 6 WHEN ''ItemFormat'' THEN 6 WHEN ''ItemDifference'' THEN 10 WHEN ''ItemUnit'' THEN 10 ELSE -1 END
   OR MIN(CASE WHEN BeforeState=1 THEN 1 ELSE 0 END)<>MAX(CASE WHEN BeforeState=1 THEN 1 ELSE 0 END)
   OR MIN(CASE WHEN AfterState=1 THEN 1 ELSE 0 END)<>MAX(CASE WHEN AfterState=1 THEN 1 ELSE 0 END)) THROW 51123,''Incomplete item row.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE ISNULL(TRY_CONVERT(int,EntityId),0)<=0 OR BeforeState=0 OR AfterState=0 OR (BeforeState=1 AND AfterState=1)
   OR (@operation=''Item.Deleted'' AND (BeforeState NOT IN(2,3) OR AfterState<>1))
   OR (@operation=''Item.FastUpdated'' AND (AfterState NOT IN(2,3) OR (EntityType<>''ItemUnit'' AND BeforeState NOT IN(2,3))))
   OR (FieldCode IN(''ItemId'',''AlertItemId'',''FormatItemId'',''DifferenceItemId'') AND ((BeforeState=3 AND BeforeValue<>@imOwner) OR (AfterState=3 AND AfterValue<>@imOwner)))) THROW 51123,''Invalid item states.'',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes)<>29 OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>''CatalogItemRecord'' OR EntityId<>@imOwner) THROW 51123,''Invalid item header.'',1;
  END
  ELSE IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''CatalogItemRecord'') THROW 51123,''Repeated item header.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE FieldCode=''ItemPhotoSha256'' AND ((BeforeState=3 AND (LEN(BeforeValue)<>64 OR BeforeValue LIKE ''%[^A-F0-9]%'')) OR (AfterState=3 AND (LEN(AfterValue)<>64 OR AfterValue LIKE ''%[^A-F0-9]%'')))) THROW 51123,''Invalid photo digest.'',1;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Catalog.Items"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.Items"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
