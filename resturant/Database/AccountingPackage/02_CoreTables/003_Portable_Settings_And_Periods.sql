SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    IF OBJECT_ID(N'dbo.ACC_PORTABLE_SETTINGS', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.ACC_PORTABLE_SETTINGS
        (
            SettingsId          TINYINT NOT NULL,
            JournalPrefix       NVARCHAR(20) NOT NULL CONSTRAINT DF_ACC_PORTABLE_SETTINGS_Prefix DEFAULT (N'JV'),
            JournalNumberLength INT NOT NULL CONSTRAINT DF_ACC_PORTABLE_SETTINGS_Length DEFAULT (5),
            JournalResetType    VARCHAR(10) NOT NULL CONSTRAINT DF_ACC_PORTABLE_SETTINGS_Reset DEFAULT ('YEAR'),
            DefaultCurrencyId   INT NOT NULL CONSTRAINT DF_ACC_PORTABLE_SETTINGS_Currency DEFAULT (1),
            DefaultCostCenterId INT NULL,
            InventoryAccountingMode VARCHAR(10) NOT NULL
                CONSTRAINT DF_ACC_PORTABLE_SETTINGS_InventoryMode DEFAULT ('PERPETUAL'),
            CONSTRAINT PK_ACC_PORTABLE_SETTINGS PRIMARY KEY (SettingsId),
            CONSTRAINT CK_ACC_PORTABLE_SETTINGS_SingleRow CHECK (SettingsId = 1),
            CONSTRAINT CK_ACC_PORTABLE_SETTINGS_Length CHECK (JournalNumberLength BETWEEN 3 AND 12),
            CONSTRAINT CK_ACC_PORTABLE_SETTINGS_Reset CHECK (JournalResetType IN ('YEAR', 'MONTH')),
            CONSTRAINT CK_ACC_PORTABLE_SETTINGS_InventoryMode CHECK
                (InventoryAccountingMode IN ('PERPETUAL', 'PERIODIC'))
        );

        INSERT INTO dbo.ACC_PORTABLE_SETTINGS(SettingsId)
        VALUES (1);
    END;

    IF COL_LENGTH(N'dbo.ACC_PORTABLE_SETTINGS', N'InventoryAccountingMode') IS NULL
    BEGIN
        ALTER TABLE dbo.ACC_PORTABLE_SETTINGS
        ADD InventoryAccountingMode VARCHAR(10) NOT NULL
            CONSTRAINT DF_ACC_PORTABLE_SETTINGS_InventoryMode DEFAULT ('PERPETUAL');

        ALTER TABLE dbo.ACC_PORTABLE_SETTINGS
        ADD CONSTRAINT CK_ACC_PORTABLE_SETTINGS_InventoryMode
            CHECK (InventoryAccountingMode IN ('PERPETUAL', 'PERIODIC'));
    END;

    IF OBJECT_ID(N'dbo.ACC_FISCAL_PERIODS', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.ACC_FISCAL_PERIODS
        (
            PeriodId       INT IDENTITY(1,1) NOT NULL,
            FiscalYear     INT NOT NULL,
            PeriodNo       TINYINT NOT NULL,
            PeriodName     NVARCHAR(100) NOT NULL,
            DateFrom       DATE NOT NULL,
            DateTo         DATE NOT NULL,
            IsClosed       BIT NOT NULL CONSTRAINT DF_ACC_FISCAL_PERIODS_IsClosed DEFAULT (0),
            ClosedAt       DATETIME NULL,
            ClosedBy       INT NULL,
            CONSTRAINT PK_ACC_FISCAL_PERIODS PRIMARY KEY (PeriodId),
            CONSTRAINT UQ_ACC_FISCAL_PERIODS_YearPeriod UNIQUE (FiscalYear, PeriodNo),
            CONSTRAINT CK_ACC_FISCAL_PERIODS_PeriodNo CHECK (PeriodNo BETWEEN 1 AND 13),
            CONSTRAINT CK_ACC_FISCAL_PERIODS_Dates CHECK (DateFrom <= DateTo)
        );

        CREATE UNIQUE INDEX UX_ACC_FISCAL_PERIODS_DateFromDateTo
            ON dbo.ACC_FISCAL_PERIODS(DateFrom, DateTo);
    END;

    IF OBJECT_ID(N'dbo.ACC_INTERNAL_SEQUENCES', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.ACC_INTERNAL_SEQUENCES
        (
            SequenceName SYSNAME NOT NULL,
            NextValue    INT NOT NULL,
            CONSTRAINT PK_ACC_INTERNAL_SEQUENCES PRIMARY KEY (SequenceName),
            CONSTRAINT CK_ACC_INTERNAL_SEQUENCES_Positive CHECK (NextValue > 0)
        );

        INSERT INTO dbo.ACC_INTERNAL_SEQUENCES(SequenceName, NextValue)
        VALUES
            (N'ACC_BALANCE_MASTER', 1),
            (N'ACC_BALANCE', 1);
    END;

    IF OBJECT_ID(N'dbo.ACC_JOURNAL_COUNTERS', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.ACC_JOURNAL_COUNTERS
        (
            FiscalYear INT NOT NULL,
            PeriodNo   TINYINT NOT NULL,
            LastNumber INT NOT NULL CONSTRAINT DF_ACC_JOURNAL_COUNTERS_LastNumber DEFAULT (0),
            CONSTRAINT PK_ACC_JOURNAL_COUNTERS PRIMARY KEY (FiscalYear, PeriodNo),
            CONSTRAINT CK_ACC_JOURNAL_COUNTERS_PeriodNo CHECK (PeriodNo BETWEEN 0 AND 13),
            CONSTRAINT CK_ACC_JOURNAL_COUNTERS_LastNumber CHECK (LastNumber >= 0)
        );
    END;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
