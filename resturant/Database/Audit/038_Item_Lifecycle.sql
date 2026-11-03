/* SQL Server 2014. Item lifecycle. CODE ONLY: apply only after isolated database tests. */
SET NOCOUNT ON;
SET XACT_ABORT ON;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51138,'Approved database only.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1 THROW 51138,'Cannot verify sessions.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int,@sql nvarchar(max),@old nvarchar(max);
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51138,'Deployment busy.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()) THROW 51138,'Close other database clients.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK) WHERE ConfigId=1 AND SourceId='3c520084-0214-43d4-a5fb-797a179c7a7c' AND EnabledDomainsXml.exist('/Domains/Domain[@code="Documents.Workflows"]')=1) THROW 51138,'Apply stage 037 first; source mismatch.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1) THROW 51138,'Incomplete audit requests.',1;
 IF EXISTS(SELECT 1 FROM sys.triggers WHERE is_disabled=0 AND parent_id IN(OBJECT_ID('IM_Menu'),OBJECT_ID('IM_Units'),OBJECT_ID('IM_QTY_ALERT'),OBJECT_ID('IM_Formating_Menu'),OBJECT_ID('ST_QTY_Deferences'),OBJECT_ID('ST_Balance'))) THROW 51138,'Review item table triggers.',1;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin'));
 SET @old=N'(@OperationCode=''AuditTest.Changed'' AND @DomainCode=''AuditTest'')';
 IF @sql IS NULL OR CHARINDEX('Item.FullSaved',@sql)>0 OR CHARINDEX('Quote.Confirmed',@sql)=0 OR CHARINDEX('Quote.EditFieldsSaved',@sql)=0 OR CHARINDEX('Document.MetadataSaved',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51138,'Unexpected begin baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@OperationCode IN(''Item.PlaceholderAcquired'',''Item.FullSaved'',''Item.QuickSaved'',''Item.DisabledRowsRemoved'') AND @DomainCode=''Catalog.ItemLifecycle'')');
 EXEC sys.sp_executesql @sql;
 SET @sql=OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'));
 SET @old=N'(@operation=''AuditTest.Changed'' AND @domain=''AuditTest'' AND @kind=1 AND @actor IN(1,3))';
 IF @sql IS NULL OR CHARINDEX('Item.FullSaved',@sql)>0 OR CHARINDEX('Quote.Confirmed',@sql)=0 OR CHARINDEX('Quote.EditFieldsSaved',@sql)=0 OR CHARINDEX('Document.MetadataSaved',@sql)=0 OR CHARINDEX(@old,@sql)=0 THROW 51138,'Unexpected writer baseline.',1;
 SET @sql=REPLACE(REPLACE(@sql,'CREATE PROCEDURE','ALTER PROCEDURE'),@old,@old+N' OR (@operation IN(''Item.PlaceholderAcquired'',''Item.FullSaved'',''Item.QuickSaved'',''Item.DisabledRowsRemoved'') AND @domain=''Catalog.ItemLifecycle'' AND @kind=1 AND @actor=1 AND @session IS NOT NULL)');
 SET @old=N'(@operation=''AuditTest.Changed'' AND ((FieldCode';
 IF CHARINDEX(@old,@sql)=0 THROW 51138,'Unexpected field baseline.',1;
 SET @sql=REPLACE(@sql,@old,N'(@operation IN(''Item.PlaceholderAcquired'',''Item.FullSaved'',''Item.QuickSaved'',''Item.DisabledRowsRemoved'')) OR '+@old);
 SET @old=N'INSERT dbo.Network_Audit_Event(EventId';
 IF CHARINDEX(@old,@sql)=0 THROW 51138,'Unexpected writer body.',1;
 SET @sql=REPLACE(@sql,@old,N'-- AuditItemLifecycle038: full item card and actual dependent rows, no binary image payload.
 IF @operation IN(''Item.PlaceholderAcquired'',''Item.FullSaved'',''Item.QuickSaved'',''Item.DisabledRowsRemoved'')
 BEGIN
  DECLARE @ilId nvarchar(128)=@EventXml.value(''(/Event/@entityId)[1]'',''nvarchar(128)''),@ilEntity varchar(64)=@EventXml.value(''(/Event/@entityType)[1]'',''varchar(64)''),@ilScreen varchar(64)=@EventXml.value(''(/Event/@screen)[1]'',''varchar(64)''),
   @ilCleanup bit=CASE WHEN @operation=''Item.DisabledRowsRemoved'' THEN 1 ELSE 0 END,@ilAcquire bit=CASE WHEN @operation=''Item.PlaceholderAcquired'' THEN 1 ELSE 0 END;
  IF ISNULL(TRY_CONVERT(int,@ilId),0)<=0 OR @ilEntity<>CASE WHEN @ilCleanup=1 THEN ''DisabledItemRows'' ELSE ''CatalogItemRecord'' END
   OR @EventXml.value(''count(/Event/Entities/Entity)'',''int'')<>0 OR @EventXml.value(''count(/Event/Document)'',''int'')<>1 OR @EventXml.value(''(/Event/Document/text())[1]'',''nvarchar(50)'')<>@ilId THROW 51138,''Invalid item lifecycle identity.'',1;
  IF NOT((@operation=''Item.PlaceholderAcquired'' AND @ilScreen IN(''ItemsMenu'',''IM_ADD_New'')) OR (@operation=''Item.FullSaved'' AND @ilScreen=''ItemsMenu'') OR (@operation IN(''Item.QuickSaved'',''Item.DisabledRowsRemoved'') AND @ilScreen=''IM_ADD_New'')) THROW 51138,''Unexpected item screen.'',1;
  IF @ilCleanup=1 AND TRY_CONVERT(int,@ilId)<>@user THROW 51138,''Cleanup belongs to the current user.'',1;
  IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE SourceId=@SourceId AND RequestId=@RequestId AND EntityType=@ilEntity AND EntityId=CASE WHEN @ilAcquire=1 THEN ''New'' ELSE @ilId END) THROW 51138,''Item lifecycle request mismatch.'',1;
  DECLARE @ilFields TABLE(E varchar(64),F varchar(64),T varchar(20),S varchar(40),O tinyint);
  INSERT @ilFields VALUES
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
   (''ItemUnit'',''ItemId'',''Integer'',''int'',1),
   (''ItemUnit'',''UnitId'',''Integer'',''int'',1),
   (''ItemUnit'',''UnitBarcode'',''String'',''nvarchar(max)'',1),
   (''ItemUnit'',''UnitPrice'',''Decimal'',''numeric(18,3)'',1),
   (''ItemUnit'',''MinimumPrice'',''Decimal'',''numeric(18,3)'',1),
   (''ItemUnit'',''MinimumPrice2'',''Decimal'',''numeric(18,3)'',1),
   (''ItemUnit'',''DefaultFlag'',''Integer'',''int'',1),
   (''ItemUnit'',''UnitRowEnabled'',''Boolean'',''bit'',1),
   (''ItemUnit'',''UnitUserId'',''Integer'',''int'',1),
   (''ItemUnit'',''OfferPrice'',''Decimal'',''numeric(18,3)'',1),
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
   (''ItemStockRow'',''StockItemId'',''Integer'',''int'',1),
   (''ItemStockRow'',''StockStoreId'',''Integer'',''int'',1),
   (''ItemStockRow'',''StockExpiry'',''String'',''nvarchar(50)'',1),
   (''ItemStockRow'',''StockQuantity'',''Decimal'',''numeric(18,3)'',1);
  IF EXISTS(SELECT 1 FROM @changes c WHERE NOT EXISTS(SELECT 1 FROM @ilFields f WHERE f.E=c.EntityType AND f.F=c.FieldCode AND f.T=c.ValueType AND f.S=c.SourceSqlType AND f.O=c.ValueOrigin) OR ISNULL(TRY_CONVERT(int,c.EntityId),0)<=0 OR c.BeforeState=0 OR c.AfterState=0 OR (c.BeforeState=1 AND c.AfterState=1)) THROW 51138,''Invalid item lifecycle field/state.'',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING COUNT(*)<>(SELECT COUNT(*) FROM @ilFields f WHERE f.E=EntityType)) THROW 51138,''Incomplete item lifecycle row.'',1;
  IF EXISTS(SELECT EntityType,EntityId FROM @changes GROUP BY EntityType,EntityId HAVING MIN(CASE WHEN BeforeState=1 THEN 1 ELSE 0 END)<>MAX(CASE WHEN BeforeState=1 THEN 1 ELSE 0 END) OR MIN(CASE WHEN AfterState=1 THEN 1 ELSE 0 END)<>MAX(CASE WHEN AfterState=1 THEN 1 ELSE 0 END)) THROW 51138,''Inconsistent item row existence.'',1;
  IF EXISTS(SELECT 1 FROM @changes WHERE FieldCode=''ItemPhotoSha256'' AND ((BeforeState=3 AND (DATALENGTH(BeforeValue)<>128 OR BeforeValue COLLATE Latin1_General_100_BIN2 LIKE ''%[^A-F0-9]%'')) OR (AfterState=3 AND (DATALENGTH(AfterValue)<>128 OR AfterValue COLLATE Latin1_General_100_BIN2 LIKE ''%[^A-F0-9]%'')))) THROW 51138,''Invalid SHA256 image metadata.'',1;
  IF @ilCleanup=1
  BEGIN
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType NOT IN(''CatalogItemRecord'',''ItemUnit'',''ItemAlert'',''ItemFormat'') OR BeforeState NOT IN(2,3) OR AfterState<>1) THROW 51138,''Cleanup must describe deleted rows.'',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE FieldCode IN(''ItemEnabled'',''UnitRowEnabled'',''AlertEnabled'',''FormatEnabled'') AND (BeforeState<>3 OR BeforeValue<>''0'')) OR EXISTS(SELECT 1 FROM @changes WHERE FieldCode IN(''ItemUserId'',''UnitUserId'',''AlertUserId'',''FormatUserId'') AND (BeforeState<>3 OR BeforeValue<>@ilId)) THROW 51138,''Cleanup of enabled/foreign row rejected.'',1;
   IF EXISTS(SELECT 1 FROM @changes c JOIN dbo.IM_Menu m WITH(HOLDLOCK) ON m.IM_ID=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''CatalogItemRecord'')
    OR EXISTS(SELECT 1 FROM @changes c JOIN dbo.IM_Units m WITH(HOLDLOCK) ON m.U_IM_ID=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''ItemUnit'')
    OR EXISTS(SELECT 1 FROM @changes c JOIN dbo.IM_QTY_ALERT m WITH(HOLDLOCK) ON m.T_ID=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''ItemAlert'')
    OR EXISTS(SELECT 1 FROM @changes c JOIN dbo.IM_Formating_Menu m WITH(HOLDLOCK) ON m.T_ID=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''ItemFormat'') THROW 51138,''Deleted item row still exists.'',1;
  END
  ELSE
  BEGIN
   IF NOT EXISTS(SELECT 1 FROM dbo.IM_Menu WITH(HOLDLOCK) WHERE IM_ID=TRY_CONVERT(int,@ilId) AND Row_Enabled=CASE WHEN @ilAcquire=1 THEN 0 ELSE 1 END) THROW 51138,''Saved item missing or not in expected state.'',1;
   IF @EventOrdinal=1 AND ((SELECT COUNT(*) FROM @changes)<>29 OR EXISTS(SELECT 1 FROM @changes WHERE EntityType<>''CatalogItemRecord'' OR EntityId<>@ilId)) THROW 51138,''Expected complete item card first.'',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE AfterState NOT IN(2,3) OR (@ilAcquire=0 AND EntityType<>''ItemUnit'' AND BeforeState NOT IN(2,3))) THROW 51138,''Unexpected deleted/created item row.'',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''CatalogItemRecord'' AND EntityId<>@ilId) OR EXISTS(SELECT 1 FROM @changes WHERE FieldCode IN(''ItemId'',''AlertItemId'',''FormatItemId'',''DifferenceItemId'',''StockItemId'') AND ((BeforeState=3 AND BeforeValue<>@ilId) OR (AfterState=3 AND AfterValue<>@ilId))) THROW 51138,''Foreign item row.'',1;
   IF EXISTS(SELECT 1 FROM @changes WHERE EntityType=''ItemStockRow'' AND FieldCode<>''StockExpiry'' AND (BeforeState<>AfterState OR ISNULL(BeforeValue,N'''') COLLATE Latin1_General_100_BIN2<>ISNULL(AfterValue,N'''') COLLATE Latin1_General_100_BIN2)) THROW 51138,''Item card cannot change stock quantities.'',1;
   -- Compare every stored after-value using fixed, closed column mappings.
   IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[IM_Menu] p WITH(HOLDLOCK) ON p.[IM_ID]=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''CatalogItemRecord'' AND (p.[IM_ID] IS NULL OR
    (c.FieldCode=''ItemName'' AND ((c.AfterState=2 AND p.[item_name] IS NOT NULL) OR (c.AfterState=3 AND (p.[item_name] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[item_name]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[item_name]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''ItemSalesName'' AND ((c.AfterState=2 AND p.[item_nameSales] IS NOT NULL) OR (c.AfterState=3 AND (p.[item_nameSales] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[item_nameSales]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[item_nameSales]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''ItemGroupId'' AND ((c.AfterState=2 AND p.[GM_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[GM_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[GM_ID])))) OR
    (c.FieldCode=''ItemPhotoSha256'' AND ((c.AfterState=2 AND p.[Photo] IS NOT NULL) OR (c.AfterState=3 AND (p.[Photo] IS NULL OR (DATALENGTH(p.[Photo])<=8000 AND c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(varchar(64),HASHBYTES(''SHA2_256'',CONVERT(varbinary(8000),p.[Photo])),2) COLLATE Latin1_General_100_BIN2))))) OR
    (c.FieldCode=''ItemPhotoBytes'' AND ((c.AfterState=2 AND DATALENGTH(p.[Photo]) IS NOT NULL) OR (c.AfterState=3 AND (DATALENGTH(p.[Photo]) IS NULL OR TRY_CONVERT(bigint,c.AfterValue) IS NULL OR TRY_CONVERT(bigint,c.AfterValue)<>DATALENGTH(p.[Photo]))))) OR
    (c.FieldCode=''ItemActive'' AND ((c.AfterState=2 AND p.[isActive] IS NOT NULL) OR (c.AfterState=3 AND (p.[isActive] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[isActive])))) OR
    (c.FieldCode=''ItemStoreType'' AND ((c.AfterState=2 AND p.[isStore] IS NOT NULL) OR (c.AfterState=3 AND (p.[isStore] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isStore])))) OR
    (c.FieldCode=''ItemUsesExpiry'' AND ((c.AfterState=2 AND p.[isValid] IS NOT NULL) OR (c.AfterState=3 AND (p.[isValid] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[isValid])))) OR
    (c.FieldCode=''ItemCost'' AND ((c.AfterState=2 AND p.[Cost] IS NOT NULL) OR (c.AfterState=3 AND (p.[Cost] IS NULL OR TRY_CONVERT(float(53),c.AfterValue) IS NULL OR TRY_CONVERT(float(53),c.AfterValue)<>p.[Cost])))) OR
    (c.FieldCode=''ItemVariablePrice'' AND ((c.AfterState=2 AND p.[isChangePrice] IS NOT NULL) OR (c.AfterState=3 AND (p.[isChangePrice] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[isChangePrice])))) OR
    (c.FieldCode=''ItemBKR'' AND ((c.AfterState=2 AND p.[BK_R] IS NOT NULL) OR (c.AfterState=3 AND (p.[BK_R] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[BK_R])))) OR
    (c.FieldCode=''ItemBKG'' AND ((c.AfterState=2 AND p.[BK_G] IS NOT NULL) OR (c.AfterState=3 AND (p.[BK_G] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[BK_G])))) OR
    (c.FieldCode=''ItemBKB'' AND ((c.AfterState=2 AND p.[BK_B] IS NOT NULL) OR (c.AfterState=3 AND (p.[BK_B] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[BK_B])))) OR
    (c.FieldCode=''ItemFKR'' AND ((c.AfterState=2 AND p.[FK_R] IS NOT NULL) OR (c.AfterState=3 AND (p.[FK_R] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[FK_R])))) OR
    (c.FieldCode=''ItemFKG'' AND ((c.AfterState=2 AND p.[FK_G] IS NOT NULL) OR (c.AfterState=3 AND (p.[FK_G] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[FK_G])))) OR
    (c.FieldCode=''ItemFKB'' AND ((c.AfterState=2 AND p.[FK_B] IS NOT NULL) OR (c.AfterState=3 AND (p.[FK_B] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[FK_B])))) OR
    (c.FieldCode=''ItemUserId'' AND ((c.AfterState=2 AND p.[User_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[User_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[User_ID])))) OR
    (c.FieldCode=''ItemEnabled'' AND ((c.AfterState=2 AND p.[Row_Enabled] IS NOT NULL) OR (c.AfterState=3 AND (p.[Row_Enabled] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[Row_Enabled])))) OR
    (c.FieldCode=''ItemLastPurchaseId'' AND ((c.AfterState=2 AND p.[Last_PchT_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Last_PchT_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Last_PchT_ID])))) OR
    (c.FieldCode=''ItemLastPurchaseCargo'' AND ((c.AfterState=2 AND p.[Last_Pch_Cargo] IS NOT NULL) OR (c.AfterState=3 AND (p.[Last_Pch_Cargo] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Last_Pch_Cargo])))) OR
    (c.FieldCode=''ItemShortcut'' AND ((c.AfterState=2 AND p.[is_Shortcut] IS NOT NULL) OR (c.AfterState=3 AND (p.[is_Shortcut] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[is_Shortcut])))) OR
    (c.FieldCode=''ItemNumber'' AND ((c.AfterState=2 AND p.[IM_Num] IS NOT NULL) OR (c.AfterState=3 AND (p.[IM_Num] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[IM_Num]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[IM_Num]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''ItemCompanyGroup'' AND ((c.AfterState=2 AND p.[Grp_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Grp_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Grp_ID])))) OR
    (c.FieldCode=''ItemPhotoPath'' AND ((c.AfterState=2 AND p.[IM_Full_Photo] IS NOT NULL) OR (c.AfterState=3 AND (p.[IM_Full_Photo] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[IM_Full_Photo]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[IM_Full_Photo]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''ItemNotes'' AND ((c.AfterState=2 AND p.[Notes] IS NOT NULL) OR (c.AfterState=3 AND (p.[Notes] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Notes]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Notes]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''ItemMarketerValue'' AND ((c.AfterState=2 AND p.[Markter_Val] IS NOT NULL) OR (c.AfterState=3 AND (p.[Markter_Val] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Markter_Val])))) OR
    (c.FieldCode=''ItemDate'' AND ((c.AfterState=2 AND p.[Date] IS NOT NULL) OR (c.AfterState=3 AND (p.[Date] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[Date])))) OR
    (c.FieldCode=''ItemMaxAlert'' AND ((c.AfterState=2 AND p.[MaxQtyAlert] IS NOT NULL) OR (c.AfterState=3 AND (p.[MaxQtyAlert] IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue)<>p.[MaxQtyAlert])))) OR
    (c.FieldCode=''ItemMinAlert'' AND ((c.AfterState=2 AND p.[MinQtyAlert] IS NOT NULL) OR (c.AfterState=3 AND (p.[MinQtyAlert] IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,2),c.AfterValue)<>p.[MinQtyAlert])))))) THROW 51138,''CatalogItemRecord after-state differs from stored row.'',1;
   IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[IM_Units] p WITH(HOLDLOCK) ON p.[U_IM_ID]=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''ItemUnit'' AND (p.[U_IM_ID] IS NULL OR
    (c.FieldCode=''ItemId'' AND ((c.AfterState=2 AND p.[IM_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[IM_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[IM_ID])))) OR
    (c.FieldCode=''UnitId'' AND ((c.AfterState=2 AND p.[U_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[U_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[U_ID])))) OR
    (c.FieldCode=''UnitBarcode'' AND ((c.AfterState=2 AND p.[Barcode] IS NOT NULL) OR (c.AfterState=3 AND (p.[Barcode] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Barcode]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Barcode]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''UnitPrice'' AND ((c.AfterState=2 AND p.[Price] IS NOT NULL) OR (c.AfterState=3 AND (p.[Price] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Price])))) OR
    (c.FieldCode=''MinimumPrice'' AND ((c.AfterState=2 AND p.[Min_SP] IS NOT NULL) OR (c.AfterState=3 AND (p.[Min_SP] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Min_SP])))) OR
    (c.FieldCode=''MinimumPrice2'' AND ((c.AfterState=2 AND p.[Min_SP_2] IS NOT NULL) OR (c.AfterState=3 AND (p.[Min_SP_2] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Min_SP_2])))) OR
    (c.FieldCode=''DefaultFlag'' AND ((c.AfterState=2 AND p.[is_Default] IS NOT NULL) OR (c.AfterState=3 AND (p.[is_Default] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[is_Default])))) OR
    (c.FieldCode=''UnitRowEnabled'' AND ((c.AfterState=2 AND p.[Row_Enabled] IS NOT NULL) OR (c.AfterState=3 AND (p.[Row_Enabled] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[Row_Enabled])))) OR
    (c.FieldCode=''UnitUserId'' AND ((c.AfterState=2 AND p.[User_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[User_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[User_ID])))) OR
    (c.FieldCode=''OfferPrice'' AND ((c.AfterState=2 AND p.[OFFER_PRICE] IS NOT NULL) OR (c.AfterState=3 AND (p.[OFFER_PRICE] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[OFFER_PRICE])))))) THROW 51138,''ItemUnit after-state differs from stored row.'',1;
   IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[IM_QTY_ALERT] p WITH(HOLDLOCK) ON p.[T_ID]=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''ItemAlert'' AND (p.[T_ID] IS NULL OR
    (c.FieldCode=''AlertItemId'' AND ((c.AfterState=2 AND p.[IM_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[IM_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[IM_ID])))) OR
    (c.FieldCode=''AlertStoreId'' AND ((c.AfterState=2 AND p.[ST_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[ST_ID] IS NULL OR TRY_CONVERT(bigint,c.AfterValue) IS NULL OR TRY_CONVERT(bigint,c.AfterValue)<>p.[ST_ID])))) OR
    (c.FieldCode=''AlertMinimum'' AND ((c.AfterState=2 AND p.[MIN_QTY] IS NOT NULL) OR (c.AfterState=3 AND (p.[MIN_QTY] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[MIN_QTY])))) OR
    (c.FieldCode=''AlertMaximum'' AND ((c.AfterState=2 AND p.[MAX_QTY] IS NOT NULL) OR (c.AfterState=3 AND (p.[MAX_QTY] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[MAX_QTY])))) OR
    (c.FieldCode=''AlertEnabled'' AND ((c.AfterState=2 AND p.[Row_Enabled] IS NOT NULL) OR (c.AfterState=3 AND (p.[Row_Enabled] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[Row_Enabled])))) OR
    (c.FieldCode=''AlertUserId'' AND ((c.AfterState=2 AND p.[User_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[User_ID] IS NULL OR TRY_CONVERT(bigint,c.AfterValue) IS NULL OR TRY_CONVERT(bigint,c.AfterValue)<>p.[User_ID])))))) THROW 51138,''ItemAlert after-state differs from stored row.'',1;
   IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[IM_Formating_Menu] p WITH(HOLDLOCK) ON p.[T_ID]=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''ItemFormat'' AND (p.[T_ID] IS NULL OR
    (c.FieldCode=''FormatItemId'' AND ((c.AfterState=2 AND p.[IM_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[IM_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[IM_ID])))) OR
    (c.FieldCode=''FormatComponentId'' AND ((c.AfterState=2 AND p.[FRM_IM_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[FRM_IM_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[FRM_IM_ID])))) OR
    (c.FieldCode=''FormatUnitId'' AND ((c.AfterState=2 AND p.[U_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[U_ID] IS NULL OR TRY_CONVERT(bigint,c.AfterValue) IS NULL OR TRY_CONVERT(bigint,c.AfterValue)<>p.[U_ID])))) OR
    (c.FieldCode=''FormatQuantity'' AND ((c.AfterState=2 AND p.[Qty] IS NOT NULL) OR (c.AfterState=3 AND (p.[Qty] IS NULL OR TRY_CONVERT(float(53),c.AfterValue) IS NULL OR TRY_CONVERT(float(53),c.AfterValue)<>p.[Qty])))) OR
    (c.FieldCode=''FormatEnabled'' AND ((c.AfterState=2 AND p.[Row_Enabled] IS NOT NULL) OR (c.AfterState=3 AND (p.[Row_Enabled] IS NULL OR TRY_CONVERT(bit,c.AfterValue) IS NULL OR TRY_CONVERT(bit,c.AfterValue)<>p.[Row_Enabled])))) OR
    (c.FieldCode=''FormatUserId'' AND ((c.AfterState=2 AND p.[User_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[User_ID] IS NULL OR TRY_CONVERT(bigint,c.AfterValue) IS NULL OR TRY_CONVERT(bigint,c.AfterValue)<>p.[User_ID])))))) THROW 51138,''ItemFormat after-state differs from stored row.'',1;
   IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[ST_QTY_Deferences] p WITH(HOLDLOCK) ON p.[T_ID]=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''ItemDifference'' AND (p.[T_ID] IS NULL OR
    (c.FieldCode=''DifferenceBillId'' AND ((c.AfterState=2 AND p.[Bill_T_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[Bill_T_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[Bill_T_ID])))) OR
    (c.FieldCode=''DifferenceDate'' AND ((c.AfterState=2 AND p.[DATE] IS NOT NULL) OR (c.AfterState=3 AND (p.[DATE] IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126)) IS NULL OR CONVERT(datetime,TRY_CONVERT(datetime2(7),c.AfterValue,126))<>p.[DATE])))) OR
    (c.FieldCode=''DifferenceItemId'' AND ((c.AfterState=2 AND p.[IM_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[IM_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[IM_ID])))) OR
    (c.FieldCode=''DifferenceStoreId'' AND ((c.AfterState=2 AND p.[ST_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[ST_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[ST_ID])))) OR
    (c.FieldCode=''DifferenceExpiry'' AND ((c.AfterState=2 AND p.[Valid_D] IS NOT NULL) OR (c.AfterState=3 AND (p.[Valid_D] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Valid_D]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Valid_D]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''DifferenceQuantity'' AND ((c.AfterState=2 AND p.[ST_QTY] IS NOT NULL) OR (c.AfterState=3 AND (p.[ST_QTY] IS NULL OR TRY_CONVERT(float(53),c.AfterValue) IS NULL OR TRY_CONVERT(float(53),c.AfterValue)<>p.[ST_QTY])))) OR
    (c.FieldCode=''DifferenceCost'' AND ((c.AfterState=2 AND p.[Cost] IS NOT NULL) OR (c.AfterState=3 AND (p.[Cost] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[Cost])))) OR
    (c.FieldCode=''DifferenceCargo'' AND ((c.AfterState=2 AND p.[U_Cargo] IS NOT NULL) OR (c.AfterState=3 AND (p.[U_Cargo] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[U_Cargo])))) OR
    (c.FieldCode=''DifferenceBarcode'' AND ((c.AfterState=2 AND p.[Barcode] IS NOT NULL) OR (c.AfterState=3 AND (p.[Barcode] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[Barcode]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[Barcode]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''DifferenceTotal'' AND ((c.AfterState=2 AND p.[TOTAL] IS NOT NULL) OR (c.AfterState=3 AND (p.[TOTAL] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[TOTAL])))))) THROW 51138,''ItemDifference after-state differs from stored row.'',1;
   IF EXISTS(SELECT 1 FROM @changes c LEFT JOIN dbo.[ST_Balance] p WITH(HOLDLOCK) ON p.[T_ID]=TRY_CONVERT(int,c.EntityId) WHERE c.EntityType=''ItemStockRow'' AND (p.[T_ID] IS NULL OR
    (c.FieldCode=''StockItemId'' AND ((c.AfterState=2 AND p.[IM_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[IM_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[IM_ID])))) OR
    (c.FieldCode=''StockStoreId'' AND ((c.AfterState=2 AND p.[ST_ID] IS NOT NULL) OR (c.AfterState=3 AND (p.[ST_ID] IS NULL OR TRY_CONVERT(int,c.AfterValue) IS NULL OR TRY_CONVERT(int,c.AfterValue)<>p.[ST_ID])))) OR
    (c.FieldCode=''StockExpiry'' AND ((c.AfterState=2 AND p.[D_Valid] IS NOT NULL) OR (c.AfterState=3 AND (p.[D_Valid] IS NULL OR DATALENGTH(CONVERT(nvarchar(max),p.[D_Valid]))<>DATALENGTH(c.AfterValue) OR c.AfterValue COLLATE Latin1_General_100_BIN2<>CONVERT(nvarchar(max),p.[D_Valid]) COLLATE Latin1_General_100_BIN2)))) OR
    (c.FieldCode=''StockQuantity'' AND ((c.AfterState=2 AND p.[ST_QTY] IS NOT NULL) OR (c.AfterState=3 AND (p.[ST_QTY] IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue) IS NULL OR TRY_CONVERT(numeric(18,3),c.AfterValue)<>p.[ST_QTY])))))) THROW 51138,''ItemStockRow after-state differs from stored row.'',1;
   -- Large image SHA256 is computed from captured bytes by the client; SQL2014 verifies length, and hashes up to 8000 bytes.
  END;
 END;
 '+@old);
 EXEC sys.sp_executesql @sql;
 UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Catalog.ItemLifecycle"/> as last into (/Domains)[1]')
 WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Catalog.ItemLifecycle"]')=0;
 COMMIT;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
