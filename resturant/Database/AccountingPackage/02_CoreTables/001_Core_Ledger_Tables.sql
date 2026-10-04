SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    IF OBJECT_ID(N'dbo.ACCOUNTS_TREE', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.ACCOUNTS_TREE
        (
            T_ID              INT             NOT NULL,
            ACC_CODE          VARCHAR(40)     NOT NULL,
            ACC_LEVEL         INT             NOT NULL,
            ACC_PARENT        VARCHAR(40)     NOT NULL,
            ACC_NAME          NVARCHAR(2500)  NOT NULL,
            ACC_TYPE          NVARCHAR(50)    NULL,
            ACC_CAT           NVARCHAR(500)   NULL,
            ACC_CLOSING       BIT             NULL,
            ACC_HINT          NVARCHAR(500)   NULL,
            DEBIT             FLOAT           NULL,
            CREDIT            FLOAT           NULL,
            ACC_NATURAL       CHAR(1)         NULL,
            is_Balance_View   BIT             NULL,
            cash_flow_ID      INT             NULL CONSTRAINT DF_ACCOUNTS_TREE_cash_flow_ID DEFAULT (0),
            SIDE              VARCHAR(500)    NULL,
            is_Lock_Trans     BIT             NULL,
            ACC_DIGIT         INT             NULL CONSTRAINT DF_ACCOUNTS_TREE_ACC_DIGIT DEFAULT (1),
            Start_DEBIT       FLOAT           NULL,
            Start_CREDIT      FLOAT           NULL,
            End_DEBIT         FLOAT           NULL,
            End_CREDIT        FLOAT           NULL,
            AccountSourceType TINYINT         NOT NULL CONSTRAINT DF_ACCOUNTS_TREE_AccountSourceType DEFAULT (0),
            CONSTRAINT PK_ACCOUNTS_TREE PRIMARY KEY (T_ID),
            CONSTRAINT UQ_ACCOUNTS_TREE_ACC_CODE UNIQUE (ACC_CODE)
        );

        CREATE INDEX IX_ACCOUNTS_TREE_ACC_PARENT
            ON dbo.ACCOUNTS_TREE(ACC_PARENT);
    END;

    IF OBJECT_ID(N'dbo.ACC_BALANCE_MASTER', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.ACC_BALANCE_MASTER
        (
            T_ID                    INT             NOT NULL,
            [DATE]                  DATETIME        NOT NULL,
            DATE_IN                 DATETIME        NOT NULL CONSTRAINT DF_ACC_BALANCE_MASTER_DATE_IN DEFAULT (GETDATE()),
            NOTES                   NVARCHAR(MAX)   NOT NULL,
            Currency_ID             INT             NOT NULL,
            Currency_Equal          NUMERIC(18,3)   NOT NULL,
            is_Depended             INT             NOT NULL CONSTRAINT DF_ACC_BALANCE_MASTER_is_Depended DEFAULT (0),
            USER_ID                 INT             NOT NULL,
            DEPEND_USER_ID          INT             NULL,
            Receipt_Type            INT             NULL,
            Receipt_Num             INT             NULL,
            Bank_Name               NVARCHAR(2550)  NULL,
            Check_Number            NVARCHAR(50)    NULL,
            IssueDate               DATE            NULL,
            DueDate                 DATE            NULL,
            ChequeStatus            NVARCHAR(100)   NULL,
            ReconciliationDate      DATE            NULL,
            BankTransactionNumber   NVARCHAR(50)    NULL,
            Cheque_Notes            NVARCHAR(400)   NULL,
            StatusId                TINYINT         NOT NULL CONSTRAINT DF_ACC_BALANCE_MASTER_StatusId DEFAULT (0),
            JournalNumber           VARCHAR(50)     NULL,
            [YEAR]                  INT             NULL,
            is_reverse              INT             NULL,
            reverse_T_ID            INT             NULL,
            SourceType              INT             NULL,
            SourceId                BIGINT          NULL,
            SourceTable             SYSNAME         NULL,
            SourceKey               NVARCHAR(100)   NULL,
            EntrySourceType         INT             NOT NULL CONSTRAINT DF_ACC_BALANCE_MASTER_EntrySourceType DEFAULT (0),
            CONSTRAINT PK_ACC_BALANCE_MASTER PRIMARY KEY (T_ID)
        );

        CREATE INDEX IX_ACC_BALANCE_MASTER_Source
            ON dbo.ACC_BALANCE_MASTER(SourceTable, SourceKey, SourceId, is_Depended);

        CREATE UNIQUE INDEX UX_ACC_BALANCE_MASTER_ActiveSource
            ON dbo.ACC_BALANCE_MASTER(SourceTable, SourceKey)
            WHERE SourceTable IS NOT NULL
              AND SourceKey IS NOT NULL
              AND reverse_T_ID IS NULL;

        CREATE INDEX IX_ACC_BALANCE_MASTER_Date
            ON dbo.ACC_BALANCE_MASTER([DATE], is_Depended);
    END;

    IF OBJECT_ID(N'dbo.ACC_BALANCE', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.ACC_BALANCE
        (
            T_ID          INT             NOT NULL,
            B_T_ID        INT             NOT NULL,
            DATE_IN       DATETIME        NOT NULL CONSTRAINT DF_ACC_BALANCE_DATE_IN DEFAULT (GETDATE()),
            ACC_CODE      VARCHAR(50)     NOT NULL,
            DEBIT         NUMERIC(18,3)   NULL,
            CREDIT        NUMERIC(18,3)   NULL,
            USER_ID       INT             NOT NULL,
            IS_VOID       INT             NOT NULL CONSTRAINT DF_ACC_BALANCE_IS_VOID DEFAULT (0),
            Currency      INT             NULL,
            Notes         NVARCHAR(MAX)   NULL,
            Bill_Num      NVARCHAR(50)    NULL,
            COST_ID       INT             NULL,
            is_Depended   INT             NOT NULL CONSTRAINT DF_ACC_BALANCE_is_Depended DEFAULT (0),
            [YEAR]        INT             NULL,
            CONSTRAINT PK_ACC_BALANCE PRIMARY KEY (T_ID),
            CONSTRAINT FK_ACC_BALANCE_MASTER FOREIGN KEY (B_T_ID)
                REFERENCES dbo.ACC_BALANCE_MASTER(T_ID),
            CONSTRAINT CK_ACC_BALANCE_OneSideOnly CHECK
            (
                (ISNULL(DEBIT, 0) > 0 AND ISNULL(CREDIT, 0) = 0)
                OR
                (ISNULL(CREDIT, 0) > 0 AND ISNULL(DEBIT, 0) = 0)
            ),
            CONSTRAINT CK_ACC_BALANCE_NoNegative CHECK
            (
                ISNULL(DEBIT, 0) >= 0 AND ISNULL(CREDIT, 0) >= 0
            )
        );

        CREATE INDEX IX_ACC_BALANCE_ACC_CODE
            ON dbo.ACC_BALANCE(ACC_CODE);

        CREATE INDEX IX_ACC_BALANCE_IncomeStatement
            ON dbo.ACC_BALANCE(ACC_CODE, DATE_IN, IS_VOID, is_Depended);

        CREATE INDEX IX_ACC_BALANCE_B_T_ID
            ON dbo.ACC_BALANCE(B_T_ID);
    END;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
