/* Limited pilot: Security + Inventory.Settlement, Sales.EditOpening after 009,
   Documents.EditOpening after 010, Sales.LineEditing after 011, Sales.LineQuantity after 012, Sales.LineLifecycle after 014. SQL Server 2014.
   Configuration only: no business/event deletion and no schema changes.
   Default NULL performs no change. Build the full client and close all sessions first.
   This is database-wide, NOT restricted to the operator's machine. Old clients must stay closed.
   Disable removes only these pilot domains and keeps all history and other domains.
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;
DECLARE @Enable bit = NULL;
DECLARE @ExpectedSource uniqueidentifier = NULL;
IF DB_NAME()<>N'CPOS_ACCOUNTING' THROW 51102,'Approved test database only.',1;
IF @Enable IS NULL OR @ExpectedSource IS NULL THROW 51101,'Explicit action and expected source are required; nothing changed.',1;
IF ISNULL(HAS_PERMS_BY_NAME(NULL,NULL,'VIEW SERVER STATE'),0)<>1
 THROW 51102,'Cannot verify other sessions; VIEW SERVER STATE is required.',1;
BEGIN TRY
 BEGIN TRANSACTION;
 DECLARE @lock int;
 EXEC @lock=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
 IF @lock<0 THROW 51103,'Audit configuration is busy.',1;
 IF (SELECT COUNT(*) FROM sys.tables WHERE schema_id=SCHEMA_ID('dbo') AND name IN
 ('Network_Audit_Source','Network_Audit_Request','Network_Audit_Event','Network_Audit_Change'))<>4
  THROW 51102,'Audit tables incomplete.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WITH(UPDLOCK,HOLDLOCK)
  WHERE ConfigId=1 AND SourceId=@ExpectedSource AND SchemaVersion=1 AND EnabledDomainsXml.exist('/Domains')=1)
  THROW 51102,'Unexpected audit source or version.',1;
 IF EXISTS(SELECT 1 FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID())
  THROW 51102,'Close all other sessions to the test database before changing the pilot.',1;
 IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Request WHERE State<>1)
  THROW 51102,'An incomplete audit request requires review before changing the pilot.',1;
 IF @Enable=1
 BEGIN
  DECLARE @lineCatalogue int=(SELECT ISNULL(SUM(CASE WHEN definition LIKE '%Sales.LineDetailsChanged%' THEN 1 ELSE 0 END + CASE WHEN definition LIKE '%Sales.LinePriceChanged%' THEN 1 ELSE 0 END),0)
   FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')));
  IF @lineCatalogue NOT IN(0,4) THROW 51102,'Sales line catalogue is partially installed.',1;
  DECLARE @quantityCatalogue int=(SELECT COUNT(*) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')) AND definition LIKE '%Sales.LineQuantityStepped%');
  IF @quantityCatalogue NOT IN(0,2) OR (@quantityCatalogue=2 AND @lineCatalogue<>4) THROW 51102,'Quantity catalogue is partially installed or missing prerequisites.',1;
  DECLARE @deleteCatalogue int=(SELECT COUNT(*) FROM sys.sql_modules WHERE object_id IN(OBJECT_ID('dbo.Network_Audit_Request_Begin'),OBJECT_ID('dbo.Network_Audit_Event_Write')) AND definition LIKE '%Sales.LineDeleted%');
  IF @deleteCatalogue NOT IN(0,2) OR (@deleteCatalogue=2 AND @quantityCatalogue<>2) THROW 51102,'Deletion catalogue is partially installed or missing prerequisites.',1;
  IF EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND
    EnabledDomainsXml.exist('/Domains/Domain[@code!="Security" and @code!="Inventory.Settlement" and @code!="Sales.EditOpening" and @code!="Documents.EditOpening" and @code!="Sales.LineEditing" and @code!="Sales.LineQuantity" and @code!="Sales.LineLifecycle"]')=1)
   THROW 51102,'Other domains are enabled; this pilot must remain limited.',1;
  IF (SELECT COUNT(*) FROM sys.procedures WHERE schema_id=SCHEMA_ID('dbo') AND name IN
   ('Network_Audit_Request_Begin','Network_Audit_Event_Write','Network_Audit_Request_Complete','Network_Audit_Request_Status',
    'ST_Settlement_Header_Apply','ST_Settlement_CanRemove','ST_Balance_Update','ST_QTY_Deferences_Details_Delete'))<>8
   THROW 51102,'Required writer or settlement procedures are missing.',1;
  IF NOT EXISTS(SELECT 1 FROM sys.parameters WHERE object_id=OBJECT_ID('dbo.ST_Balance_Update') AND name='@Inserted_T_ID' AND is_output=1)
   OR NOT EXISTS(SELECT 1 FROM sys.parameters WHERE object_id=OBJECT_ID('dbo.ST_QTY_Deferences_Details_Delete') AND name='@ExpectedBillId')
   OR NOT EXISTS(SELECT 1 FROM sys.parameters WHERE object_id=OBJECT_ID('dbo.ST_QTY_Deferences_Details_Delete') AND name='@AllowNegative')
   THROW 51102,'Settlement procedure signatures are not upgraded.',1;
  IF (SELECT COUNT(*) FROM sys.sql_modules WHERE object_id IN
   (OBJECT_ID('dbo.ST_Balance_TMP1_Plus_V'),OBJECT_ID('dbo.ST_Balance_TMP1_Negitave_V'),OBJECT_ID('dbo.ST_Balance_TMP_Plus_Negitave_V'),
    OBJECT_ID('dbo.IM_full_movement_tmp_V'),OBJECT_ID('dbo.VW_InventoryTransactionFlow')) AND definition LIKE '%/*Audit3C*/%')<>5
   THROW 51102,'Void settlement filters are incomplete.',1;
  IF CHARINDEX('Settlement.EditFinished',ISNULL(OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin')),''))=0
   OR CHARINDEX('Settlement.EditFinished',ISNULL(OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write')),''))=0
   THROW 51102,'Audit writer does not recognize the full header lifecycle.',1;
  UPDATE dbo.Network_Audit_Source
   SET EnabledDomainsXml.modify('insert <Domain code="Security"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Security"]')=0;
  UPDATE dbo.Network_Audit_Source
   SET EnabledDomainsXml.modify('insert <Domain code="Inventory.Settlement"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Inventory.Settlement"]')=0;
  IF CHARINDEX('Sales.EditOpened',ISNULL(OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write')),''))>0
   AND CHARINDEX('Sales.EditOpened',ISNULL(OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin')),''))>0
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.EditOpening"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.EditOpening"]')=0;
  IF CHARINDEX('Document.EditOpened',ISNULL(OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write')),''))>0
   AND CHARINDEX('Document.EditOpened',ISNULL(OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Request_Begin')),''))>0
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Documents.EditOpening"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Documents.EditOpening"]')=0;
  IF @lineCatalogue=4
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.LineEditing"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.LineEditing"]')=0;
  IF @quantityCatalogue=2
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.LineQuantity"/> as last into (/Domains)[1]')
  WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.LineQuantity"]')=0;
  IF @deleteCatalogue=2
   UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('insert <Domain code="Sales.LineLifecycle"/> as last into (/Domains)[1]')
   WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Sales.LineLifecycle"]')=0;
 END
 ELSE
 BEGIN
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Sales.LineLifecycle"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Sales.LineQuantity"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Sales.LineEditing"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Documents.EditOpening"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Sales.EditOpening"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source
   SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Inventory.Settlement"]') WHERE ConfigId=1;
  UPDATE dbo.Network_Audit_Source
   SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Security"]') WHERE ConfigId=1;
 END;
 COMMIT;
 SELECT DB_NAME() AS DatabaseName,SourceId,EnabledDomainsXml FROM dbo.Network_Audit_Source WHERE ConfigId=1;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;
