/* Phase 2A / SQL Server 2014. TEST DATABASE ONLY.
   Adds four audit tables; does NOT replace the legacy writer or change business tables.
   All domains remain disabled. The strict writer and business-path migration are a later batch.
   Retain these tables on rollback of the application; never delete audit history to downgrade.
   Confirm the selected database and Windows time-zone ID before deployment.
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;
DECLARE @ExpectedDatabase sysname = N'CPOS_ACCOUNTING';
DECLARE @TimeZoneId nvarchar(128) = N'Libya Standard Time';
IF DB_NAME() <> @ExpectedDatabase
    THROW 51000, 'Select the approved CPOS_ACCOUNTING test database. Nothing was changed.', 1;
IF OBJECT_ID(N'dbo.Network_Edit_Tracker', N'U') IS NULL
    THROW 51000, 'The expected CPOS legacy audit table is missing.', 1;

BEGIN TRY
    BEGIN TRANSACTION;
    DECLARE @LockResult int;
    EXEC @LockResult = sys.sp_getapplock @Resource=N'CPOS.NetworkAudit.Schema',
        @LockMode='Exclusive', @LockOwner='Transaction', @LockTimeout=10000;
    IF @LockResult < 0 THROW 51000, 'Could not acquire the audit schema deployment lock.', 1;

    DECLARE @Existing int;
    SELECT @Existing = COUNT(*) FROM sys.tables
    WHERE schema_id=SCHEMA_ID(N'dbo') AND name IN
        (N'Network_Audit_Source',N'Network_Audit_Request',N'Network_Audit_Event',N'Network_Audit_Change');
    IF @Existing <> 0
        THROW 51000, 'Audit V2 tables already exist (or installation is partial). Validate instead of reinstalling.', 1;

    CREATE TABLE dbo.Network_Audit_Source (
        ConfigId tinyint NOT NULL CONSTRAINT PK_Network_Audit_Source PRIMARY KEY,
        SourceId uniqueidentifier NOT NULL,
        LegacyServId int NULL,
        BranchCode nvarchar(20) NULL,
        TimeZoneId nvarchar(128) NOT NULL,
        SchemaVersion smallint NOT NULL,
        EnabledDomainsXml xml NOT NULL,
        CreatedAtUtc datetime2(3) NOT NULL CONSTRAINT DF_NAS_Created DEFAULT SYSUTCDATETIME(),
        CONSTRAINT UQ_NAS_Source UNIQUE(SourceId),
        CONSTRAINT CK_NAS_Config CHECK (ConfigId=1 AND SchemaVersion=1),
        CONSTRAINT CK_NAS_Id CHECK (SourceId<>'00000000-0000-0000-0000-000000000000')
    );

    CREATE TABLE dbo.Network_Audit_Request (
        SourceId uniqueidentifier NOT NULL,
        RequestId uniqueidentifier NOT NULL,
        ContractVersion smallint NOT NULL,
        OperationCode varchar(64) NOT NULL,
        ActorKind tinyint NOT NULL,
        ActorUserId int NULL,
        ActorServiceCode varchar(64) NULL,
        SessionId uniqueidentifier NULL,
        EntityType varchar(64) NULL,
        EntityId nvarchar(128) NULL,
        RequestFingerprint binary(32) NOT NULL,
        CanonicalRequest varbinary(max) NOT NULL,
        State tinyint NOT NULL,
        ResultCode tinyint NULL,
        ResultXml xml NULL,
        CreatedAtUtc datetime2(3) NOT NULL CONSTRAINT DF_NAR_Created DEFAULT SYSUTCDATETIME(),
        CompletedAtUtc datetime2(3) NULL,
        CONSTRAINT PK_Network_Audit_Request PRIMARY KEY(SourceId,RequestId),
        CONSTRAINT CK_NAR_Id CHECK (SourceId<>'00000000-0000-0000-0000-000000000000'
            AND RequestId<>'00000000-0000-0000-0000-000000000000'),
        CONSTRAINT CK_NAR_Contract CHECK (ContractVersion=1 AND LEN(OperationCode)>0 AND DATALENGTH(CanonicalRequest)>0),
        CONSTRAINT CK_NAR_Actor CHECK (
            (ActorKind=1 AND ActorUserId IS NOT NULL AND ActorUserId>0 AND ActorServiceCode IS NULL) OR
            (ActorKind=2 AND ActorUserId IS NULL AND ActorServiceCode IS NULL AND SessionId IS NULL) OR
            (ActorKind=3 AND ActorUserId IS NULL AND ActorServiceCode IS NOT NULL AND LEN(ActorServiceCode)>0)),
        CONSTRAINT CK_NAR_State CHECK (
            (State=0 AND ResultCode IS NULL AND ResultXml IS NULL AND CompletedAtUtc IS NULL) OR
            (State=1 AND ResultCode IS NOT NULL AND ResultCode BETWEEN 1 AND 5 AND
                ResultXml IS NOT NULL AND CompletedAtUtc IS NOT NULL AND CompletedAtUtc>=CreatedAtUtc))
    );

    CREATE TABLE dbo.Network_Audit_Event (
        EventId uniqueidentifier NOT NULL,
        LocalSequence bigint IDENTITY(1,1) NOT NULL,
        SourceId uniqueidentifier NOT NULL,
        RequestId uniqueidentifier NOT NULL,
        EventOrdinal int NOT NULL,
        CorrelationId uniqueidentifier NULL,
        ContractVersion smallint NOT NULL,
        EventCode varchar(64) NOT NULL,
        DomainCode varchar(64) NOT NULL,
        ScreenCode varchar(64) NOT NULL,
        EventKind tinyint NOT NULL,
        Outcome tinyint NOT NULL,
        ActorKind tinyint NOT NULL,
        ActorUserId int NULL,
        ActorServiceCode varchar(64) NULL,
        ActorName nvarchar(256) NULL,
        SessionId uniqueidentifier NULL,
        MachineName nvarchar(128) NOT NULL,
        ApplicationVersion nvarchar(50) NOT NULL,
        EntityType varchar(64) NULL,
        EntityId nvarchar(128) NULL,
        DocumentNumber nvarchar(50) NULL,
        OccurredAtUtc datetime2(3) NOT NULL,
        RecordedAtUtc datetime2(3) NOT NULL CONSTRAINT DF_NAE_Recorded DEFAULT SYSUTCDATETIME(),
        BusinessDate datetime2(3) NULL,
        LegacyScreenType int NULL,
        LegacyOperationId int NULL,
        ReasonCode varchar(64) NULL,
        ReasonText nvarchar(1000) NULL,
        TemplateCode varchar(64) NOT NULL,
        TemplateVersion smallint NOT NULL,
        Notes nvarchar(max) NOT NULL,
        RelatedEntitiesXml xml NULL,
        EventFingerprint binary(32) NOT NULL,
        CanonicalEvent varbinary(max) NOT NULL,
        CONSTRAINT PK_Network_Audit_Event PRIMARY KEY NONCLUSTERED(EventId),
        CONSTRAINT UQ_NAE_Sequence UNIQUE CLUSTERED(LocalSequence),
        CONSTRAINT UQ_NAE_RequestOrdinal UNIQUE(SourceId,RequestId,EventOrdinal),
        CONSTRAINT FK_NAE_Request FOREIGN KEY(SourceId,RequestId)
            REFERENCES dbo.Network_Audit_Request(SourceId,RequestId),
        CONSTRAINT CK_NAE_Id CHECK (EventId<>'00000000-0000-0000-0000-000000000000'
            AND EventOrdinal>0 AND ContractVersion=1 AND TemplateVersion>0),
        CONSTRAINT CK_NAE_Codes CHECK (LEN(EventCode)>0 AND LEN(DomainCode)>0 AND LEN(ScreenCode)>0
            AND LEN(MachineName)>0 AND LEN(ApplicationVersion)>0 AND LEN(TemplateCode)>0
            AND LEN(Notes)>0 AND DATALENGTH(CanonicalEvent)>0),
        CONSTRAINT CK_NAE_Outcome CHECK (EventKind BETWEEN 1 AND 3 AND Outcome BETWEEN 1 AND 4),
        CONSTRAINT CK_NAE_Actor CHECK (
            (ActorKind=1 AND ActorUserId IS NOT NULL AND ActorUserId>0 AND ActorServiceCode IS NULL) OR
            (ActorKind=2 AND ActorUserId IS NULL AND ActorServiceCode IS NULL AND SessionId IS NULL AND EventKind IN(2,3)) OR
            (ActorKind=3 AND ActorUserId IS NULL AND ActorServiceCode IS NOT NULL AND LEN(ActorServiceCode)>0)),
        CONSTRAINT CK_NAE_BusinessActor CHECK (EventKind<>1 OR ActorKind=3 OR
            (ActorKind=1 AND SessionId IS NOT NULL AND SessionId<>'00000000-0000-0000-0000-000000000000')),
        CONSTRAINT CK_NAE_LegacyOperation CHECK (LegacyOperationId IS NULL OR LegacyOperationId BETWEEN 1 AND 3)
    );
    CREATE INDEX IX_NAE_Recorded ON dbo.Network_Audit_Event(RecordedAtUtc,LocalSequence);
    CREATE INDEX IX_NAE_Entity ON dbo.Network_Audit_Event(SourceId,EntityType,EntityId,RecordedAtUtc);
    CREATE INDEX IX_NAE_Actor ON dbo.Network_Audit_Event(ActorUserId,RecordedAtUtc);

    CREATE TABLE dbo.Network_Audit_Change (
        EventId uniqueidentifier NOT NULL,
        ChangeNo int NOT NULL,
        EntityType varchar(64) NOT NULL,
        EntityId nvarchar(128) NOT NULL,
        FieldCode varchar(64) NOT NULL,
        ValueType varchar(20) NOT NULL,
        SourceSqlType varchar(40) NULL,
        BeforeState tinyint NOT NULL,
        BeforeValue nvarchar(max) NULL,
        AfterState tinyint NOT NULL,
        AfterValue nvarchar(max) NULL,
        ValueOrigin tinyint NOT NULL,
        CONSTRAINT PK_Network_Audit_Change PRIMARY KEY(EventId,ChangeNo),
        CONSTRAINT UQ_NAC_Field UNIQUE(EventId,EntityType,EntityId,FieldCode),
        CONSTRAINT FK_NAC_Event FOREIGN KEY(EventId) REFERENCES dbo.Network_Audit_Event(EventId),
        CONSTRAINT CK_NAC_Codes CHECK (ChangeNo>0 AND LEN(EntityType)>0 AND LEN(EntityId)>0 AND LEN(FieldCode)>0),
        CONSTRAINT CK_NAC_Type CHECK (ValueType IN('Integer','Decimal','Float','Boolean','String','Date','DateTime','Guid')),
        CONSTRAINT CK_NAC_Before CHECK ((BeforeState IN(0,1,2) AND BeforeValue IS NULL) OR (BeforeState=3 AND BeforeValue IS NOT NULL)),
        CONSTRAINT CK_NAC_After CHECK ((AfterState IN(0,1,2) AND AfterValue IS NULL) OR (AfterState=3 AND AfterValue IS NOT NULL)),
        CONSTRAINT CK_NAC_Origin CHECK (ValueOrigin BETWEEN 1 AND 3)
    );

    INSERT dbo.Network_Audit_Source(ConfigId,SourceId,TimeZoneId,SchemaVersion,EnabledDomainsXml)
    VALUES(1,NEWID(),@TimeZoneId,1,N'<Domains />');
    COMMIT;
    SELECT ConfigId,SourceId,TimeZoneId,SchemaVersion,EnabledDomainsXml FROM dbo.Network_Audit_Source;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT>0 ROLLBACK;
    THROW;
END CATCH;
