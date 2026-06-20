USE [CPOS_ACCOUNTING]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF COL_LENGTH('dbo.Budget_Entries', 'HasStamp') IS NULL
BEGIN
    ALTER TABLE dbo.Budget_Entries
    ADD HasStamp BIT NOT NULL
        CONSTRAINT DF_Budget_Entries_HasStamp DEFAULT (0);
END
GO

IF COL_LENGTH('dbo.Budget_Entries', 'StampPercent') IS NULL
BEGIN
    ALTER TABLE dbo.Budget_Entries
    ADD StampPercent DECIMAL(18,3) NULL;
END
GO

IF COL_LENGTH('dbo.Budget_Entries', 'StampAccountCode') IS NULL
BEGIN
    ALTER TABLE dbo.Budget_Entries
    ADD StampAccountCode NVARCHAR(40) NULL;
END
GO

IF COL_LENGTH('dbo.Budget_Entries', 'StampAmount') IS NULL
BEGIN
    ALTER TABLE dbo.Budget_Entries
    ADD StampAmount DECIMAL(18,3) NULL;
END
GO

ALTER PROCEDURE [dbo].[Budget_Approve_Entry]
(
    @BudgetEntryId INT,
    @ApprovedBy INT,
    @ExpenseAccountCode NVARCHAR(40) = NULL,
    @ContraAccountCode NVARCHAR(40) = NULL,
    @BeneficiaryType TINYINT = NULL,
    @BeneficiaryId INT = NULL,
    @PaymentMethodId TINYINT = NULL,
    @InvoiceNo NVARCHAR(50) = NULL,
    @DocumentNo NVARCHAR(50) = NULL,
    @SpendStatement NVARCHAR(500) = NULL,
    @StampPercent DECIMAL(18,3) = NULL,
    @StampAccountCode NVARCHAR(40) = NULL,
    @AccountingEntryId INT OUTPUT,
    @Msg NVARCHAR(300) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        DECLARE
            @BudgetItemId INT,
            @Amount DECIMAL(18,3),
            @EntryType TINYINT,
            @EntryDate DATETIME,
            @Notes NVARCHAR(500),
            @SavedHasStamp BIT,
            @SavedStampPercent DECIMAL(18,3),
            @SavedStampAccountCode NVARCHAR(40);

        SELECT
            @BudgetItemId = BudgetItemId,
            @Amount = Amount,
            @EntryType = EntryType,
            @EntryDate = EntryDate,
            @Notes = Notes,
            @SavedHasStamp = ISNULL(HasStamp, 0),
            @SavedStampPercent = StampPercent,
            @SavedStampAccountCode = StampAccountCode,
            @AccountingEntryId = AccountingEntryId
        FROM dbo.Budget_Entries WITH (UPDLOCK, HOLDLOCK)
        WHERE BudgetEntryId = @BudgetEntryId;

        IF @BudgetItemId IS NULL
        BEGIN
            SET @Msg = N'القيد غير موجود';
            ROLLBACK;
            RETURN;
        END;

        IF @AccountingEntryId IS NOT NULL
        BEGIN
            SET @Msg = N'تم اعتماد هذا الصرف مسبقًا';
            ROLLBACK;
            RETURN;
        END;

        IF EXISTS (
            SELECT 1
            FROM dbo.ACC_BALANCE_BUDGET_LINK
            WHERE BudgetEntryId = @BudgetEntryId
        )
        BEGIN
            SET @Msg = N'هذه الحركة مرتبطة مسبقًا بقيد محاسبي';
            ROLLBACK;
            RETURN;
        END;

        IF @EntryType <> 1
        BEGIN
            SET @Msg = N'هذا السجل ليس صرفًا فعليًا';
            ROLLBACK;
            RETURN;
        END;

        DECLARE
            @ExpenseAccount_Code NVARCHAR(40),
            @account_ID INT;

        IF NULLIF(LTRIM(RTRIM(@ExpenseAccountCode)), N'') IS NOT NULL
        BEGIN
            SELECT TOP 1
                @account_ID = ABI.AccountId,
                @ExpenseAccount_Code = LTRIM(RTRIM(CONVERT(NVARCHAR(40), A.ACC_CODE)))
            FROM dbo.Account_Budget_Items ABI
            INNER JOIN dbo.ACCOUNTS_TREE A
                ON A.T_ID = ABI.AccountId
            WHERE ABI.BudgetItemId = @BudgetItemId
              AND LTRIM(RTRIM(CONVERT(NVARCHAR(40), A.ACC_CODE))) = LTRIM(RTRIM(@ExpenseAccountCode))
            ORDER BY ISNULL(ABI.IsDefault, 0) DESC, A.ACC_CODE;

            IF @account_ID IS NULL
            BEGIN
                SET @Msg = N'حساب مصروف البند المختار غير مرتبط بهذا البند';
                ROLLBACK;
                RETURN;
            END;
        END;

        IF @account_ID IS NULL
        BEGIN
            SELECT TOP 1
                @account_ID = ABI.AccountId,
                @ExpenseAccount_Code = LTRIM(RTRIM(CONVERT(NVARCHAR(40), A.ACC_CODE)))
            FROM dbo.Account_Budget_Items ABI
            INNER JOIN dbo.ACCOUNTS_TREE A
                ON A.T_ID = ABI.AccountId
            WHERE ABI.BudgetItemId = @BudgetItemId
              AND ISNULL(ABI.IsDefault, 0) = 1
            ORDER BY A.ACC_CODE;
        END;

        IF @account_ID IS NULL
        BEGIN
            SELECT TOP 1
                @account_ID = ABI.AccountId,
                @ExpenseAccount_Code = LTRIM(RTRIM(CONVERT(NVARCHAR(40), A.ACC_CODE)))
            FROM dbo.Account_Budget_Items ABI
            INNER JOIN dbo.ACCOUNTS_TREE A
                ON A.T_ID = ABI.AccountId
            WHERE ABI.BudgetItemId = @BudgetItemId
            ORDER BY A.ACC_CODE;
        END;

        IF NULLIF(LTRIM(RTRIM(@ExpenseAccount_Code)), N'') IS NULL
        BEGIN
            SET @Msg = N'لا يوجد حساب مصروف مرتبط بهذا البند';
            ROLLBACK;
            RETURN;
        END;

        IF NULLIF(LTRIM(RTRIM(@ContraAccountCode)), N'') IS NULL
        BEGIN
            SELECT TOP 1
                @ContraAccountCode = ContraAccountCode
            FROM dbo.Budget_Items
            WHERE BudgetItemId = @BudgetItemId;
        END;

        IF NULLIF(LTRIM(RTRIM(@ContraAccountCode)), N'') IS NULL
        BEGIN
            SET @Msg = N'لا يوجد حساب دفع / مستفيد لهذا الإذن';
            ROLLBACK;
            RETURN;
        END;

        IF NOT EXISTS (
            SELECT 1
            FROM dbo.ACCOUNTS_TREE
            WHERE ACC_CODE = @ContraAccountCode
        )
        BEGIN
            SET @Msg = N'حساب الدفع / المستفيد غير موجود في شجرة الحسابات';
            ROLLBACK;
            RETURN;
        END;

        IF @SavedHasStamp = 1
        BEGIN
            IF @StampPercent IS NULL OR @StampPercent <= 0
                SET @StampPercent = @SavedStampPercent;

            IF NULLIF(LTRIM(RTRIM(@StampAccountCode)), N'') IS NULL
                SET @StampAccountCode = @SavedStampAccountCode;
        END;

        DECLARE
            @StampAmount DECIMAL(18,3) = 0,
            @NetAmount DECIMAL(18,3) = @Amount;

        IF ISNULL(@StampPercent, 0) > 0
        BEGIN
            SET @StampAmount = ROUND((@Amount * @StampPercent) / 100, 3);
            SET @NetAmount = @Amount - @StampAmount;

            IF @StampAmount <= 0 OR @StampAmount >= @Amount
            BEGIN
                SET @Msg = N'قيمة الدمغة غير صحيحة';
                ROLLBACK;
                RETURN;
            END;

            IF NULLIF(LTRIM(RTRIM(@StampAccountCode)), N'') IS NULL
            BEGIN
                SET @Msg = N'لم يتم تحديد حساب الدمغة';
                ROLLBACK;
                RETURN;
            END;

            IF NOT EXISTS (
                SELECT 1
                FROM dbo.ACCOUNTS_TREE
                WHERE ACC_CODE = @StampAccountCode
            )
            BEGIN
                SET @Msg = N'حساب الدمغة غير موجود في شجرة الحسابات';
                ROLLBACK;
                RETURN;
            END;
        END;

        DECLARE @Statement NVARCHAR(400);

        SET @Statement = N'صرف بند ميزانية - رقم حركة: '
                       + CAST(@BudgetEntryId AS NVARCHAR(20));

        DECLARE
            @Err NVARCHAR(500),
            @OpStatus INT,
            @ReceiptNum INT;

        SET @Err = N'';
        SET @OpStatus = 1;
        SET @ReceiptNum = 0;

        DECLARE @IsOnlyMaster BIT;
        SET @IsOnlyMaster = CASE WHEN @StampAmount > 0 THEN 1 ELSE 0 END;

        EXEC [dbo].[ACC_BALANCE_proc_Receipt]
            @DATE = @EntryDate,
            @ACC_CODE_FROM = @ExpenseAccount_Code,
            @ACC_CODE_TO = @ContraAccountCode,
            @DEBIT = @Amount,
            @CREDIT = @Amount,
            @USER_ID = @ApprovedBy,
            @Notes_MASTER = @Statement,
            @COST_ID = 1,
            @ERROR_MSG = @Err OUTPUT,
            @OP_Status = @OpStatus OUTPUT,
            @Receipt_Type = 4,
            @Receipt_Num = @ReceiptNum,
            @Bank_Name = N'',
            @Check_Number = N'//',
            @Cr_ID = 1,
            @Currency_Equal = 1,
            @BankTransactionNumber = NULL,
            @B_T_ID = @AccountingEntryId OUTPUT,
            @is_Only_Master = @IsOnlyMaster,
            @SourceType = 20,
            @SourceId = @BudgetEntryId,
            @SourceTable = N'Budget_Entries';

        IF ISNULL(@OpStatus, 0) = 0
        BEGIN
            SET @Msg = ISNULL(@Err, N'فشل إنشاء القيد المحاسبي');
            ROLLBACK;
            RETURN;
        END;

        IF @AccountingEntryId IS NULL OR @AccountingEntryId = 0
        BEGIN
            SET @Msg = N'فشل إنشاء القيد المحاسبي';
            ROLLBACK;
            RETURN;
        END;

        IF @StampAmount > 0
        BEGIN
            DECLARE
                @DetailOpStatus INT,
                @DetailErrorMsg NVARCHAR(500),
                @NextNumber NVARCHAR(50);

            SET @DetailOpStatus = 1;
            SET @DetailErrorMsg = N'';
            SET @NextNumber = N'';

            EXEC dbo.ACC_BALANCE_proc
                @T_ID = 0,
                @B_T_ID = @AccountingEntryId,
                @DATE = @EntryDate,
                @ACC_CODE = @ExpenseAccount_Code,
                @DEBIT = @Amount,
                @USER_ID = @ApprovedBy,
                @IS_VOID = 0,
                @Currency = 1,
                @Notes = @Statement,
                @Notes_MASTER = @Statement,
                @Process = N'',
                @Bill_Num = N'',
                @COST_ID = 1,
                @Cr_ID = 1,
                @Currency_Equal = 1,
                @NextNumber = @NextNumber OUTPUT,
                @OP_Status = @DetailOpStatus OUTPUT,
                @ERROR_MSG = @DetailErrorMsg OUTPUT;

            IF ISNULL(@DetailOpStatus, 0) = 0
            BEGIN
                SET @Msg = ISNULL(@DetailErrorMsg, N'فشل إضافة سطر مصروف البند للقيد');
                ROLLBACK;
                RETURN;
            END;

            SET @DetailOpStatus = 1;
            SET @DetailErrorMsg = N'';
            SET @NextNumber = N'';

            EXEC dbo.ACC_BALANCE_proc
                @T_ID = 0,
                @B_T_ID = @AccountingEntryId,
                @DATE = @EntryDate,
                @ACC_CODE = @ContraAccountCode,
                @CREDIT = @NetAmount,
                @USER_ID = @ApprovedBy,
                @IS_VOID = 0,
                @Currency = 1,
                @Notes = N'صافي الصرف بعد الدمغة',
                @Notes_MASTER = @Statement,
                @Process = N'',
                @Bill_Num = N'',
                @COST_ID = 1,
                @Cr_ID = 1,
                @Currency_Equal = 1,
                @NextNumber = @NextNumber OUTPUT,
                @OP_Status = @DetailOpStatus OUTPUT,
                @ERROR_MSG = @DetailErrorMsg OUTPUT;

            IF ISNULL(@DetailOpStatus, 0) = 0
            BEGIN
                SET @Msg = ISNULL(@DetailErrorMsg, N'فشل إضافة سطر حساب الدفع / المستفيد للقيد');
                ROLLBACK;
                RETURN;
            END;

            SET @DetailOpStatus = 1;
            SET @DetailErrorMsg = N'';
            SET @NextNumber = N'';

            EXEC dbo.ACC_BALANCE_proc
                @T_ID = 0,
                @B_T_ID = @AccountingEntryId,
                @DATE = @EntryDate,
                @ACC_CODE = @StampAccountCode,
                @CREDIT = @StampAmount,
                @USER_ID = @ApprovedBy,
                @IS_VOID = 0,
                @Currency = 1,
                @Notes = N'دمغة إذن صرف موازنة',
                @Notes_MASTER = @Statement,
                @Process = N'',
                @Bill_Num = N'',
                @COST_ID = 1,
                @Cr_ID = 1,
                @Currency_Equal = 1,
                @NextNumber = @NextNumber OUTPUT,
                @OP_Status = @DetailOpStatus OUTPUT,
                @ERROR_MSG = @DetailErrorMsg OUTPUT;

            IF ISNULL(@DetailOpStatus, 0) = 0
            BEGIN
                SET @Msg = ISNULL(@DetailErrorMsg, N'فشل إضافة سطر الدمغة للقيد');
                ROLLBACK;
                RETURN;
            END;
        END;

        DECLARE
            @DoorId INT,
            @ChapterId INT,
            @BudgetYear INT;

        SELECT
            @ChapterId = C.ChapterId,
            @DoorId = C.DoorId
        FROM dbo.Budget_Items I
        INNER JOIN dbo.Budget_Chapters C
            ON C.ChapterId = I.ChapterId
        WHERE I.BudgetItemId = @BudgetItemId;

        SET @BudgetYear = YEAR(@EntryDate);

        INSERT INTO dbo.ACC_BALANCE_BUDGET_LINK
        (
            B_T_ID,
            ACC_T_ID,
            BudgetEntryId,
            DoorId,
            ChapterId,
            BudgetItemId,
            BudgetYear,
            Amount,
            LinkType,
            CreatedBy
        )
        VALUES
        (
            @AccountingEntryId,
            @account_ID,
            @BudgetEntryId,
            @DoorId,
            @ChapterId,
            @BudgetItemId,
            @BudgetYear,
            @Amount,
            1,
            @ApprovedBy
        );

        UPDATE dbo.Budget_Entries
        SET AccountingEntryId = @AccountingEntryId,
            BeneficiaryType = ISNULL(@BeneficiaryType, BeneficiaryType),
            BeneficiaryId = ISNULL(@BeneficiaryId, BeneficiaryId),
            PaymentMethodId = ISNULL(@PaymentMethodId, PaymentMethodId),
            InvoiceNo = ISNULL(@InvoiceNo, InvoiceNo),
            DocumentNo = ISNULL(@DocumentNo, DocumentNo),
            SpendStatement = ISNULL(@SpendStatement, SpendStatement),
            ContraAccountCode = @ContraAccountCode,
            HasStamp = CASE WHEN @StampAmount > 0 THEN 1 ELSE ISNULL(HasStamp, 0) END,
            StampPercent = CASE WHEN @StampAmount > 0 THEN @StampPercent ELSE StampPercent END,
            StampAccountCode = CASE WHEN @StampAmount > 0 THEN @StampAccountCode ELSE StampAccountCode END,
            StampAmount = CASE WHEN @StampAmount > 0 THEN @StampAmount ELSE StampAmount END
        WHERE BudgetEntryId = @BudgetEntryId;

        COMMIT;

        SET @Msg = N'تم اعتماد الصرف وإنشاء القيد رقم '
                 + CAST(@AccountingEntryId AS NVARCHAR(20));
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;

        SET @Msg = ERROR_MESSAGE();
    END CATCH
END
GO
