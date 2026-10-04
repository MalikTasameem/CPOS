SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    IF OBJECT_ID(N'dbo.ACC_SYSTEM_ACCOUNT_TYPES', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.ACC_SYSTEM_ACCOUNT_TYPES
        (
            SystemAccountTypeID  INT IDENTITY(1,1) NOT NULL,
            AccountKey           NVARCHAR(100) NOT NULL,
            AccountNameAr        NVARCHAR(200) NOT NULL,
            AccountNameEn        NVARCHAR(200) NULL,
            Required             BIT NOT NULL CONSTRAINT DF_ACC_SYSTEM_ACCOUNT_TYPES_Required DEFAULT (1),
            AllowSameAccount     BIT NOT NULL CONSTRAINT DF_ACC_SYSTEM_ACCOUNT_TYPES_AllowSameAccount DEFAULT (0),
            MustBeLeaf           BIT NOT NULL CONSTRAINT DF_ACC_SYSTEM_ACCOUNT_TYPES_MustBeLeaf DEFAULT (1),
            Expected_ACC_NATURAL CHAR(1) NULL,
            IsActive             BIT NOT NULL CONSTRAINT DF_ACC_SYSTEM_ACCOUNT_TYPES_IsActive DEFAULT (1),
            Notes                NVARCHAR(500) NULL,
            CONSTRAINT PK_ACC_SYSTEM_ACCOUNT_TYPES PRIMARY KEY (SystemAccountTypeID),
            CONSTRAINT UQ_ACC_SYSTEM_ACCOUNT_TYPES_AccountKey UNIQUE (AccountKey),
            CONSTRAINT CK_ACC_SYSTEM_ACCOUNT_TYPES_Natural CHECK
                (Expected_ACC_NATURAL IS NULL OR Expected_ACC_NATURAL IN ('D', 'C'))
        );
    END;

    IF OBJECT_ID(N'dbo.ACC_SYSTEM_ACCOUNT_LINKS', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.ACC_SYSTEM_ACCOUNT_LINKS
        (
            SystemAccountLinkID INT IDENTITY(1,1) NOT NULL,
            SystemAccountTypeID INT NOT NULL,
            ACC_T_ID             INT NOT NULL,
            IsActive             BIT NOT NULL CONSTRAINT DF_ACC_SYSTEM_ACCOUNT_LINKS_IsActive DEFAULT (1),
            CreatedAt            DATETIME NOT NULL CONSTRAINT DF_ACC_SYSTEM_ACCOUNT_LINKS_CreatedAt DEFAULT (GETDATE()),
            CreatedBy            INT NULL,
            UpdatedAt            DATETIME NULL,
            UpdatedBy            INT NULL,
            Notes                NVARCHAR(500) NULL,
            CONSTRAINT PK_ACC_SYSTEM_ACCOUNT_LINKS PRIMARY KEY (SystemAccountLinkID),
            CONSTRAINT FK_ACC_SYSTEM_ACCOUNT_LINKS_Type FOREIGN KEY (SystemAccountTypeID)
                REFERENCES dbo.ACC_SYSTEM_ACCOUNT_TYPES(SystemAccountTypeID),
            CONSTRAINT FK_ACC_SYSTEM_ACCOUNT_LINKS_Account FOREIGN KEY (ACC_T_ID)
                REFERENCES dbo.ACCOUNTS_TREE(T_ID)
        );

        CREATE UNIQUE INDEX UX_ACC_SYSTEM_ACCOUNT_LINKS_ActiveType
            ON dbo.ACC_SYSTEM_ACCOUNT_LINKS(SystemAccountTypeID)
            WHERE IsActive = 1;
    END;

    IF OBJECT_ID(N'dbo.ACC_SYSTEM_ACCOUNT_LINKS_LOG', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.ACC_SYSTEM_ACCOUNT_LINKS_LOG
        (
            LogID                   INT IDENTITY(1,1) NOT NULL,
            ActionType              NVARCHAR(50) NOT NULL,
            SystemAccountTypeID     INT NOT NULL,
            AccountKey              NVARCHAR(100) NULL,
            AccountNameAr           NVARCHAR(200) NULL,
            Old_ACC_T_ID            INT NULL,
            Old_ACC_CODE            VARCHAR(40) NULL,
            Old_ACC_NAME            NVARCHAR(2500) NULL,
            New_ACC_T_ID            INT NULL,
            New_ACC_CODE            VARCHAR(40) NULL,
            New_ACC_NAME            NVARCHAR(2500) NULL,
            OldRequired             BIT NULL,
            NewRequired             BIT NULL,
            OldAllowSameAccount     BIT NULL,
            NewAllowSameAccount     BIT NULL,
            OldMustBeLeaf           BIT NULL,
            NewMustBeLeaf           BIT NULL,
            OldExpected_ACC_NATURAL CHAR(1) NULL,
            NewExpected_ACC_NATURAL CHAR(1) NULL,
            OldIsActive             BIT NULL,
            NewIsActive             BIT NULL,
            OldNotes                NVARCHAR(500) NULL,
            NewNotes                NVARCHAR(500) NULL,
            ActionDate              DATETIME NOT NULL CONSTRAINT DF_ACC_SYSTEM_ACCOUNT_LINKS_LOG_ActionDate DEFAULT (GETDATE()),
            ActionBy                INT NULL,
            ActionNote              NVARCHAR(500) NULL,
            CONSTRAINT PK_ACC_SYSTEM_ACCOUNT_LINKS_LOG PRIMARY KEY (LogID)
        );
    END;

    IF OBJECT_ID(N'dbo.ACC_ENTITY_RULES', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.ACC_ENTITY_RULES
        (
            RULE_ID                 INT IDENTITY(1,1) NOT NULL,
            ENTITY_TYPE             TINYINT NOT NULL,
            ENTITY_NAME_AR          NVARCHAR(100) NOT NULL,
            SOURCE_TABLE            SYSNAME NOT NULL,
            PARENT_ACC_CODE         VARCHAR(50) NOT NULL,
            AUTO_CREATE_ACCOUNT     BIT NOT NULL CONSTRAINT DF_ACC_ENTITY_RULES_AUTO_CREATE DEFAULT (1),
            ALLOW_RENAME            BIT NOT NULL CONSTRAINT DF_ACC_ENTITY_RULES_ALLOW_RENAME DEFAULT (1),
            ALLOW_CHANGE_PARENT     BIT NOT NULL CONSTRAINT DF_ACC_ENTITY_RULES_ALLOW_CHANGE_PARENT DEFAULT (0),
            ALLOW_DELETE_IF_NO_MOVE BIT NOT NULL CONSTRAINT DF_ACC_ENTITY_RULES_ALLOW_DELETE DEFAULT (0),
            IS_ACTIVE               BIT NOT NULL CONSTRAINT DF_ACC_ENTITY_RULES_IS_ACTIVE DEFAULT (1),
            CONSTRAINT PK_ACC_ENTITY_RULES PRIMARY KEY (RULE_ID),
            CONSTRAINT UQ_ACC_ENTITY_RULES_ENTITY_TYPE UNIQUE (ENTITY_TYPE)
        );
    END;

    IF OBJECT_ID(N'dbo.ACC_ENTITY_LINKS', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.ACC_ENTITY_LINKS
        (
            LINK_ID         INT IDENTITY(1,1) NOT NULL,
            ENTITY_TYPE     TINYINT NOT NULL,
            SOURCE_TABLE    SYSNAME NOT NULL,
            SOURCE_ID       BIGINT NOT NULL,
            ACC_CODE        VARCHAR(50) NOT NULL,
            PARENT_ACC_CODE VARCHAR(50) NULL,
            ENTITY_NAME     NVARCHAR(1000) NOT NULL,
            IS_AUTO_CREATED BIT NOT NULL CONSTRAINT DF_ACC_ENTITY_LINKS_AUTO_CREATED DEFAULT (1),
            IS_ACTIVE       BIT NOT NULL CONSTRAINT DF_ACC_ENTITY_LINKS_IS_ACTIVE DEFAULT (1),
            IS_LOCKED       BIT NOT NULL CONSTRAINT DF_ACC_ENTITY_LINKS_IS_LOCKED DEFAULT (0),
            CREATED_AT      DATETIME NOT NULL CONSTRAINT DF_ACC_ENTITY_LINKS_CREATED_AT DEFAULT (GETDATE()),
            CREATED_BY      INT NULL,
            UPDATED_AT      DATETIME NULL,
            UPDATED_BY      INT NULL,
            CONSTRAINT PK_ACC_ENTITY_LINKS PRIMARY KEY (LINK_ID),
            CONSTRAINT UQ_ACC_ENTITY_LINKS_ENTITY UNIQUE (ENTITY_TYPE, SOURCE_ID),
            CONSTRAINT UQ_ACC_ENTITY_LINKS_ACC_CODE UNIQUE (ACC_CODE)
        );

        CREATE INDEX IX_ACC_ENTITY_LINKS_SOURCE_TABLE
            ON dbo.ACC_ENTITY_LINKS(SOURCE_TABLE, SOURCE_ID, IS_ACTIVE);
    END;

    IF OBJECT_ID(N'dbo.ACC_InvoiceRepostLog', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.ACC_InvoiceRepostLog
        (
            RepostLogID      INT IDENTITY(1,1) NOT NULL,
            SourceTable      SYSNAME NOT NULL,
            SourceID         BIGINT NULL,
            SourceKey        NVARCHAR(100) NOT NULL,
            BsType_ID        INT NULL,
            EditVersion      INT NOT NULL,
            OldJournalId     INT NOT NULL,
            ReversalJournalId INT NOT NULL,
            NewJournalId     INT NOT NULL,
            RepostReason     NVARCHAR(500) NULL,
            RepostAt         DATETIME NOT NULL CONSTRAINT DF_ACC_InvoiceRepostLog_RepostAt DEFAULT (GETDATE()),
            RepostBy         INT NULL,
            CONSTRAINT PK_ACC_InvoiceRepostLog PRIMARY KEY (RepostLogID)
        );

        CREATE INDEX IX_ACC_InvoiceRepostLog_Source
            ON dbo.ACC_InvoiceRepostLog(SourceTable, SourceKey, EditVersion);
    END;

    IF COL_LENGTH(N'dbo.ACC_InvoiceRepostLog', N'SourceKey') IS NULL
    BEGIN
        ALTER TABLE dbo.ACC_InvoiceRepostLog ADD SourceKey NVARCHAR(100) NULL;
        EXEC sys.sp_executesql N'
            UPDATE dbo.ACC_InvoiceRepostLog
            SET SourceKey = CONVERT(NVARCHAR(100), SourceID)
            WHERE SourceKey IS NULL;

            ALTER TABLE dbo.ACC_InvoiceRepostLog
                ALTER COLUMN SourceKey NVARCHAR(100) NOT NULL;';
    END;

    IF EXISTS
    (
        SELECT 1
        FROM sys.indexes
        WHERE object_id = OBJECT_ID(N'dbo.ACC_InvoiceRepostLog')
          AND name = N'IX_ACC_InvoiceRepostLog_Source'
    )
        DROP INDEX IX_ACC_InvoiceRepostLog_Source ON dbo.ACC_InvoiceRepostLog;

    EXEC sys.sp_executesql N'
        CREATE INDEX IX_ACC_InvoiceRepostLog_Source
            ON dbo.ACC_InvoiceRepostLog(SourceTable, SourceKey, EditVersion);';

    IF OBJECT_ID(N'dbo.ACC_InvoiceCancelLog', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.ACC_InvoiceCancelLog
        (
            CancelLogID       INT IDENTITY(1,1) NOT NULL,
            SourceTable       SYSNAME NOT NULL,
            SourceID          BIGINT NULL,
            SourceKey         NVARCHAR(100) NOT NULL,
            BsType_ID         INT NULL,
            OldJournalId      INT NULL,
            ReversalJournalId INT NULL,
            CancelReason      NVARCHAR(500) NULL,
            CancelAt          DATETIME NOT NULL CONSTRAINT DF_ACC_InvoiceCancelLog_CancelAt DEFAULT (GETDATE()),
            CancelBy          INT NULL,
            CONSTRAINT PK_ACC_InvoiceCancelLog PRIMARY KEY (CancelLogID)
        );

        CREATE INDEX IX_ACC_InvoiceCancelLog_Source
            ON dbo.ACC_InvoiceCancelLog(SourceTable, SourceKey);
    END;

    IF COL_LENGTH(N'dbo.ACC_InvoiceCancelLog', N'SourceKey') IS NULL
    BEGIN
        ALTER TABLE dbo.ACC_InvoiceCancelLog ADD SourceKey NVARCHAR(100) NULL;
        EXEC sys.sp_executesql N'
            UPDATE dbo.ACC_InvoiceCancelLog
            SET SourceKey = CONVERT(NVARCHAR(100), SourceID)
            WHERE SourceKey IS NULL;

            ALTER TABLE dbo.ACC_InvoiceCancelLog
                ALTER COLUMN SourceKey NVARCHAR(100) NOT NULL;';
    END;

    IF EXISTS
    (
        SELECT 1
        FROM sys.indexes
        WHERE object_id = OBJECT_ID(N'dbo.ACC_InvoiceCancelLog')
          AND name = N'IX_ACC_InvoiceCancelLog_Source'
    )
        DROP INDEX IX_ACC_InvoiceCancelLog_Source ON dbo.ACC_InvoiceCancelLog;

    EXEC sys.sp_executesql N'
        CREATE INDEX IX_ACC_InvoiceCancelLog_Source
            ON dbo.ACC_InvoiceCancelLog(SourceTable, SourceKey);';

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
