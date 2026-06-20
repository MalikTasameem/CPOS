USE [CPOS_ACCOUNTING]
GO

IF OBJECT_ID('dbo.Budget_SpendVoucher_Print', 'P') IS NOT NULL
    DROP PROCEDURE dbo.Budget_SpendVoucher_Print;
GO

IF OBJECT_ID('dbo.V_Budget_SpendVoucher_Print', 'V') IS NOT NULL
    DROP VIEW dbo.V_Budget_SpendVoucher_Print;
GO

CREATE VIEW dbo.V_Budget_SpendVoucher_Print
AS
SELECT
    E.BudgetEntryId,
    E.FiscalYear,
    E.EntryDate,
    E.Amount,
    ISNULL(E.Notes, N'') AS Notes,
    ISNULL(E.SpendStatement, N'') AS SpendStatement,
    ISNULL(E.InvoiceNo, N'') AS InvoiceNo,
    ISNULL(E.DocumentNo, N'') AS DocumentNo,

    E.EntryType,
    CASE E.EntryType
        WHEN 1 THEN N'إذن صرف'
        WHEN 2 THEN N'حجز'
        WHEN 3 THEN N'فك حجز'
        WHEN 4 THEN N'تسوية'
        ELSE N'غير معروف'
    END AS EntryTypeName,

    ISNULL(E.StatusId, 0) AS StatusId,
    CASE
        WHEN E.ReversalJournalId IS NOT NULL THEN N'ملغى'
        WHEN ISNULL(E.StatusId, 0) = 2 THEN N'ملغى'
        WHEN E.AccountingEntryId IS NULL THEN N'غير معتمد'
        ELSE N'معتمد'
    END AS StatusName,

    E.AccountingEntryId,
    E.ReversalJournalId,

    M.JournalNumber,
    M.[DATE] AS JournalDate,

    RevM.JournalNumber AS ReversalJournalNumber,
    RevM.[DATE] AS ReversalJournalDate,

    D.DoorId,
    D.DoorCode,
    D.DoorName,

    C.ChapterId,
    C.ChapterCode,
    C.ChapterName,

    I.BudgetItemId,
    I.ItemCode,
    I.ItemName,

    E.CostCenterId,
    CC.COST_NAME AS CostCenterName,

    E.ProjectId,
    CASE WHEN E.ProjectId IS NULL THEN N'' ELSE CONVERT(NVARCHAR(50), E.ProjectId) END AS ProjectName,

    E.ReserveEntryId,
    R.Amount AS ReserveAmount,
    R.EntryDate AS ReserveDate,
    ISNULL(R.Notes, N'') AS ReserveNotes,

    E.BeneficiaryType,
    BT.BeneficiaryTypeName,

    E.BeneficiaryId,
    E.ContraAccountCode,
    A.ACC_NAME AS ContraAccountName,

    E.PaymentMethodId,
    PM.PaymentMethodName,

    E.ApprovedAt,
    E.ApprovedBy,
    ISNULL(UInput.UserName, CONVERT(NVARCHAR(50), E.ApprovedBy)) AS ApprovedByName,

    E.CanceledAt,
    E.CanceledBy,
    E.CancelReason,

    ISNULL(ReserveCalc.ReleasedAmount, 0) AS TotalReleasedFromReserve,
    ISNULL(R.Amount, 0) - ISNULL(ReserveCalc.ReleasedAmount, 0) AS ReserveRemainingAmount

FROM dbo.Budget_Entries E
INNER JOIN dbo.Budget_Items I
    ON I.BudgetItemId = E.BudgetItemId
INNER JOIN dbo.Budget_Chapters C
    ON C.ChapterId = I.ChapterId
INNER JOIN dbo.Budget_Doors D
    ON D.DoorId = C.DoorId

LEFT JOIN dbo.ACC_BALANCE_MASTER M
    ON M.T_ID = E.AccountingEntryId
LEFT JOIN dbo.ACC_BALANCE_MASTER RevM
    ON RevM.T_ID = E.ReversalJournalId

LEFT JOIN dbo.Budget_Entries R
    ON R.BudgetEntryId = E.ReserveEntryId

LEFT JOIN dbo.Budget_BeneficiaryTypes BT
    ON BT.BeneficiaryType = E.BeneficiaryType
LEFT JOIN dbo.Budget_PaymentMethods PM
    ON PM.PaymentMethodId = E.PaymentMethodId

LEFT JOIN dbo.ACCOUNTS_TREE A
    ON A.ACC_CODE = E.ContraAccountCode

LEFT JOIN dbo.COST_CENTER CC
    ON CC.COST_ID = E.CostCenterId

LEFT JOIN dbo.Users UInput
    ON UInput.user_id = E.ApprovedBy

OUTER APPLY
(
    SELECT
        SUM(X.Amount) AS ReleasedAmount
    FROM dbo.Budget_Entries X
    WHERE X.ReserveEntryId = E.ReserveEntryId
      AND X.EntryType = 3
      AND ISNULL(X.StatusId, 1) <> 2
) ReserveCalc

WHERE E.EntryType = 1;
GO

CREATE PROCEDURE dbo.Budget_SpendVoucher_Print
(
    @BudgetEntryId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM dbo.V_Budget_SpendVoucher_Print
    WHERE BudgetEntryId = @BudgetEntryId;
END
GO
