/* Phase 3A / SQL Server 2014 / approved TEST database only.
   Run in CPOS_ACCOUNTING, after building the new client and closing all sessions.
   Set @EnableSecurity to 1 to enable the security pilot, or 0 to stop it.
   Default NULL is deliberately non-mutating. Other domains and all events are preserved.
   Do NOT use on a synchronized production database before its separate rollout checks.
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;
DECLARE @EnableSecurity bit = NULL;
IF DB_NAME() <> N'CPOS_ACCOUNTING' THROW 51102,'Select approved CPOS_ACCOUNTING test database.',1;
IF @EnableSecurity IS NULL THROW 51101,'Set @EnableSecurity explicitly to 1 or 0; no change was made.',1;
BEGIN TRY
    BEGIN TRANSACTION;
    DECLARE @LockResult int;
    EXEC @LockResult=sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=5000;
    IF @LockResult<0 THROW 51103,'Audit configuration is busy.',1;
    IF (SELECT COUNT(*) FROM sys.tables WHERE schema_id=SCHEMA_ID(N'dbo') AND name IN
        ('Network_Audit_Source','Network_Audit_Request','Network_Audit_Event','Network_Audit_Change'))<>4
        THROW 51102,'Audit tables are incomplete.',1;
    IF NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Source WHERE ConfigId=1 AND SchemaVersion=1 AND EnabledDomainsXml.exist('/Domains')=1)
        THROW 51102,'Audit source is not ready.',1;
    IF @EnableSecurity=1
    BEGIN
        IF (SELECT COUNT(*) FROM sys.procedures WHERE schema_id=SCHEMA_ID(N'dbo') AND name IN
            ('Network_Audit_Request_Begin','Network_Audit_Event_Write','Network_Audit_Request_Complete','Network_Audit_Request_Status'))<>4
            THROW 51102,'Install and validate the audit writer first.',1;
        UPDATE dbo.Network_Audit_Source
            SET EnabledDomainsXml.modify('insert <Domain code="Security"/> as last into (/Domains)[1]')
            WHERE ConfigId=1 AND EnabledDomainsXml.exist('/Domains/Domain[@code="Security"]')=0;
    END
    ELSE
        UPDATE dbo.Network_Audit_Source
            SET EnabledDomainsXml.modify('delete /Domains/Domain[@code="Security"]') WHERE ConfigId=1;
    COMMIT;
    SELECT DB_NAME() DatabaseName,SourceId,EnabledDomainsXml FROM dbo.Network_Audit_Source WHERE ConfigId=1;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT>0 ROLLBACK;
    THROW;
END CATCH;
