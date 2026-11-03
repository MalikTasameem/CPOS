/* SQL Server 2014. Item-unit links/prices audit; all IM_Units_* business procedures remain unchanged. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51121,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51121,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51121,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51121,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.Groups"]')=1) THROW 51121,'Unexpected source or prerequisite.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51121,'Incomplete audit requests.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('ItemUnit.Created',@sql)>0 OR CHARINDEX('Group.Created',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51121,'Unexpected begin baseline or already installed.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''ItemUnit.Created'',''ItemUnit.Updated'',''ItemUnit.Deleted'',''ItemUnit.PriceChanged'',''ItemUnit.OfferPriceChanged'') AND @DomainCode=''Catalog.ItemUnits'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('ItemUnit.Created',@sql)>0 OR CHARINDEX(@old,@sql)=0 THROW 51121,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''ItemUnit.Created'',''ItemUnit.Updated'',''ItemUnit.Deleted'',''ItemUnit.PriceChanged'',''ItemUnit.OfferPriceChanged'') AND @domain=''Catalog.ItemUnits'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51121,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''ItemUnit.Created'',''ItemUnit.Updated'',''ItemUnit.Deleted'',''ItemUnit.PriceChanged'',''ItemUnit.OfferPriceChanged'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51121,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditItemUnits021: actual item context, unit siblings and recipe rows.
 IF @operation IN(''ItemUnit.Created'',''ItemUnit.Updated'',''ItemUnit.Deleted'',''ItemUnit.PriceChanged'',''ItemUnit.OfferPriceChanged'')
 BEGIN
  DECLARE @iuItem nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),
   @iuTarget nvarchar(50)=@EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)''),
   @iuScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)'');
  IF @EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)'')<>''CatalogItem'' OR ISNULL(TRY_CONVERT(int,@iuItem),0)<=0 OR ISNULL(TRY_CONVERT(int,@iuTarget),0)<=0
   OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0 THROW 51121,''Invalid item-unit identity.'',1;
  IF NOT ((@operation=''ItemUnit.Created'' AND @iuScreen IN(''ItemsMenu'',''Add_Unit''))
   OR (@operation=''ItemUnit.Deleted'' AND @iuScreen=''ItemsMenu'') OR (@operation=''ItemUnit.Updated'' AND @iuScreen=''Update_IM_Unit'')
   OR (@operation=''ItemUnit.PriceChanged'' AND @iuScreen=''IM_Change_PriceByVal'') OR (@operation=''ItemUnit.OfferPriceChanged'' AND @iuScreen=''OFFERS'')) THROW 51121,''Invalid item-unit screen.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=''CatalogItem'' AND EntityId=@iuItem) THROW 51121,''Item-unit request mismatch.'',1;
  DECLARE @iuf TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40));
  INSERT @iuf VALUES
   (''CatalogItem'',''ItemName'',''String'',''nvarchar(500)''),
   (''CatalogItem'',''ItemSalesName'',''String'',''nvarchar(500)''),
   (''ItemUnit'',''ItemId'',''Integer'',''int''),
   (''ItemUnit'',''UnitId'',''Integer'',''int''),
   (''ItemUnit'',''UnitBarcode'',''String'',''nvarchar(max)''),
   (''ItemUnit'',''UnitPrice'',''Decimal'',''numeric(18,3)''),
   (''ItemUnit'',''MinimumPrice'',''Decimal'',''numeric(18,3)''),
   (''ItemUnit'',''MinimumPrice2'',''Decimal'',''numeric(18,3)''),
   (''ItemUnit'',''DefaultFlag'',''Integer'',''int''),
   (''ItemUnit'',''UnitRowEnabled'',''Boolean'',''bit''),
   (''ItemUnit'',''UnitUserId'',''Integer'',''int''),
   (''ItemUnit'',''OfferPrice'',''Decimal'',''numeric(18,3)''),
   (''ItemUnitRecipe'',''RecipeItemId'',''Integer'',''int''),
   (''ItemUnitRecipe'',''RecipeItemUnitId'',''Integer'',''int''),
   (''ItemUnitRecipe'',''RecipeComponentId'',''Integer'',''int''),
   (''ItemUnitRecipe'',''RecipeUnitId'',''Integer'',''int''),
   (''ItemUnitRecipe'',''RecipeQuantity'',''Float'',''float(53)''),
   (''ItemUnitRecipe'',''RecipeRowEnabled'',''Boolean'',''bit''),
   (''ItemUnitRecipe'',''RecipeUserId'',''Integer'',''int'');
  IF EXISTS(SELECT 1 FROM @changes c WHERE ValueOrigin<>1 OR NOT EXISTS(SELECT 1 FROM @iuf f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType)) THROW 51121,''Invalid item-unit field.'',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>CASE EntityType WHEN ''CatalogItem'' THEN 2 WHEN ''ItemUnit'' THEN 10 ELSE 7 END
    OR MIN(CASE WHEN BeforeState=1 THEN 1 ELSE 0 END)<>MAX(CASE WHEN BeforeState=1 THEN 1 ELSE 0 END)
    OR MIN(CASE WHEN AfterState=1 THEN 1 ELSE 0 END)<>MAX(CASE WHEN AfterState=1 THEN 1 ELSE 0 END)) THROW 51121,''Incomplete or inconsistent item-unit row.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE ISNULL(TRY_CONVERT(int,EntityId),0)<=0 OR BeforeState=0 OR AfterState=0 OR (BeforeState=1 AND AfterState=1)) THROW 51121,''Uncaptured item-unit values.'',1;
  IF @EventOrdinal=1
  BEGIN
   IF (SELECT COUNT(*) FROM @changes)<>2 OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>''CatalogItem'' OR EntityId<>@iuItem OR BeforeState NOT IN(2,3) OR AfterState NOT IN(2,3)) THROW 51121,''Invalid item context.'',1;
  END
  ELSE
  BEGIN
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''CatalogItem'' OR (FieldCode=''ItemId'' AND ((BeforeState=3 AND BeforeValue<>@iuItem) OR (AfterState=3 AND AfterValue<>@iuItem)))) THROW 51121,''Invalid item-unit details.'',1;
  END;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Catalog.ItemUnits"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.ItemUnits"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
