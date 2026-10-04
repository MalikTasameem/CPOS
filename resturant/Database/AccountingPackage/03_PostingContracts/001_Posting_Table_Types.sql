SET NOCOUNT ON;
SET XACT_ABORT ON;

IF TYPE_ID(N'dbo.ACC_BALANCE_BULK_TYPE') IS NULL
BEGIN
    EXEC(N'
        CREATE TYPE dbo.ACC_BALANCE_BULK_TYPE AS TABLE
        (
            RowNo     INT NULL,
            ACC_CODE  NVARCHAR(50) NOT NULL,
            DEBIT     NUMERIC(18,3) NULL,
            CREDIT    NUMERIC(18,3) NULL,
            Currency  INT NULL,
            Notes     NVARCHAR(MAX) NULL,
            Bill_Num  NVARCHAR(50) NULL,
            COST_ID   INT NULL
        );
    ');
END;

IF TYPE_ID(N'dbo.SalePaymentAllocationType') IS NULL
BEGIN
    EXEC(N'
        CREATE TYPE dbo.SalePaymentAllocationType AS TABLE
        (
            [LineNo]        INT NOT NULL PRIMARY KEY,
            PaymentMethodID INT NOT NULL,
            TreasuryID      INT NOT NULL,
            Amount          DECIMAL(18,3) NOT NULL,
            ReferenceNumber NVARCHAR(100) NULL,
            BankName        NVARCHAR(150) NULL,
            CheckNumber     NVARCHAR(100) NULL,
            Notes           NVARCHAR(500) NULL
        );
    ');
END;

IF TYPE_ID(N'dbo.ACC_SALES_LINE_TYPE') IS NULL
BEGIN
    EXEC(N'
        CREATE TYPE dbo.ACC_SALES_LINE_TYPE AS TABLE
        (
            [LineNo]                 INT NOT NULL PRIMARY KEY,
            SourceLineKey            NVARCHAR(100) NULL,
            InventoryAccountCode     VARCHAR(40) NOT NULL,
            RevenueAccountCode       VARCHAR(40) NULL,
            SalesReturnAccountCode   VARCHAR(40) NULL,
            COGSAccountCode          VARCHAR(40) NULL,
            Quantity                 NUMERIC(18,3) NOT NULL,
            LineGross                NUMERIC(18,3) NOT NULL,
            UnitCost                 NUMERIC(18,3) NOT NULL,
            CurrencyId               INT NULL,
            CostCenterId             INT NULL,
            CHECK (Quantity > 0),
            CHECK (LineGross >= 0),
            CHECK (UnitCost >= 0)
        );
    ');
END;

IF TYPE_ID(N'dbo.ACC_PURCHASE_LINE_TYPE') IS NULL
BEGIN
    EXEC(N'
        CREATE TYPE dbo.ACC_PURCHASE_LINE_TYPE AS TABLE
        (
            [LineNo]                 INT NOT NULL PRIMARY KEY,
            SourceLineKey            NVARCHAR(100) NULL,
            InventoryAccountCode     VARCHAR(40) NOT NULL,
            PurchaseAccountCode      VARCHAR(40) NULL,
            PurchaseReturnAccountCode VARCHAR(40) NULL,
            Quantity                 NUMERIC(18,3) NOT NULL,
            LineAmount               NUMERIC(18,3) NOT NULL,
            CurrencyId               INT NULL,
            CostCenterId             INT NULL,
            CHECK (Quantity > 0),
            CHECK (LineAmount >= 0)
        );
    ');
END;

IF TYPE_ID(N'dbo.ACC_EXPENSE_LINE_TYPE') IS NULL
BEGIN
    EXEC(N'
        CREATE TYPE dbo.ACC_EXPENSE_LINE_TYPE AS TABLE
        (
            [LineNo]              INT NOT NULL PRIMARY KEY,
            SourceLineKey         NVARCHAR(100) NULL,
            TreatmentType         TINYINT NOT NULL,
            ExpenseAccountCode    VARCHAR(40) NULL,
            InventoryAccountCode  VARCHAR(40) NULL,
            Amount                NUMERIC(18,3) NOT NULL,
            CurrencyId            INT NULL,
            CostCenterId          INT NULL,
            CHECK (TreatmentType IN (1, 2)),
            CHECK (Amount > 0)
        );
    ');
END;

IF TYPE_ID(N'dbo.ACC_INVENTORY_TRANSFER_LINE_TYPE') IS NULL
BEGIN
    EXEC(N'
        CREATE TYPE dbo.ACC_INVENTORY_TRANSFER_LINE_TYPE AS TABLE
        (
            [LineNo]                INT NOT NULL PRIMARY KEY,
            SourceLineKey           NVARCHAR(100) NULL,
            FromInventoryAccountCode VARCHAR(40) NOT NULL,
            ToInventoryAccountCode   VARCHAR(40) NOT NULL,
            Amount                  NUMERIC(18,3) NOT NULL,
            CurrencyId              INT NULL,
            CostCenterId            INT NULL,
            CHECK (Amount > 0),
            CHECK (FromInventoryAccountCode <> ToInventoryAccountCode)
        );
    ');
END;

IF TYPE_ID(N'dbo.ACC_INVENTORY_VALUE_LINE_TYPE') IS NULL
BEGIN
    EXEC(N'
        CREATE TYPE dbo.ACC_INVENTORY_VALUE_LINE_TYPE AS TABLE
        (
            [LineNo]               INT NOT NULL PRIMARY KEY,
            SourceLineKey          NVARCHAR(100) NULL,
            InventoryAccountCode   VARCHAR(40) NOT NULL,
            Amount                 NUMERIC(18,3) NOT NULL,
            CurrencyId             INT NULL,
            CostCenterId           INT NULL,
            CHECK (Amount <> 0)
        );
    ');
END;
