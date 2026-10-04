/* TEMPLATE ONLY - DO NOT EXECUTE AS-IS. */
RAISERROR(N'قالب فقط: استبدل أسماء الجداول والقيود قبل التنفيذ.', 16, 1);
RETURN;

SET NOCOUNT ON;
SET XACT_ABORT ON;

/* كرر المجموعة للمورد والموظف والخزينة والمصرف والمخزن والمصروف. */
ALTER TABLE dbo.[<CUSTOMER_TABLE>]
ADD Tree_Code   VARCHAR(40) NULL,
    ACC_LINK_ID INT NULL;

CREATE INDEX IX_<CUSTOMER_TABLE>_Tree_Code
ON dbo.[<CUSTOMER_TABLE>](Tree_Code);

ALTER TABLE dbo.[<CUSTOMER_TABLE>] WITH CHECK
ADD CONSTRAINT FK_<CUSTOMER_TABLE>_Tree_Code
FOREIGN KEY (Tree_Code) REFERENCES dbo.ACCOUNTS_TREE(ACC_CODE);

ALTER TABLE dbo.[<CUSTOMER_TABLE>] WITH CHECK
ADD CONSTRAINT FK_<CUSTOMER_TABLE>_ACC_LINK_ID
FOREIGN KEY (ACC_LINK_ID) REFERENCES dbo.ACC_ENTITY_LINKS(LINK_ID);

/* كرر المجموعة لكل رأس معاملة ينتج قيدا مستقلا. */
ALTER TABLE dbo.[<SALES_HEADER_TABLE>]
ADD JournalId                INT NULL,
    PostedAt                 DATETIME NULL,
    PostedBy                 INT NULL,
    OriginalJournalId        INT NULL,
    LastReversalJournalId    INT NULL,
    NeedRepost               BIT NOT NULL
        CONSTRAINT DF_<SALES_HEADER_TABLE>_NeedRepost DEFAULT (0),
    EditVersion              INT NOT NULL
        CONSTRAINT DF_<SALES_HEADER_TABLE>_EditVersion DEFAULT (0),
    EditOpenedAt             DATETIME NULL,
    EditOpenedBy             INT NULL,
    LastRepostAt             DATETIME NULL,
    LastRepostBy             INT NULL,
    VoidAt                   DATETIME NULL,
    VoidBy                   INT NULL,
    VoidReason               NVARCHAR(500) NULL,
    VoidReversalJournalId    INT NULL,
    NeedCancelReverse        BIT NOT NULL
        CONSTRAINT DF_<SALES_HEADER_TABLE>_NeedCancelReverse DEFAULT (0);

ALTER TABLE dbo.[<SALES_HEADER_TABLE>] WITH CHECK
ADD CONSTRAINT FK_<SALES_HEADER_TABLE>_JournalId
FOREIGN KEY (JournalId) REFERENCES dbo.ACC_BALANCE_MASTER(T_ID);

CREATE INDEX IX_<SALES_HEADER_TABLE>_AccountingStatus
ON dbo.[<SALES_HEADER_TABLE>]
(
    <VOID_COLUMN>, NeedRepost, NeedCancelReverse, JournalId
);

