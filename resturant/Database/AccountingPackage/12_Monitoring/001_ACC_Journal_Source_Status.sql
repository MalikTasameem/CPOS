SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.ACC_VW_JOURNAL_SOURCE_STATUS', N'V') IS NULL
    EXEC(N'CREATE VIEW dbo.ACC_VW_JOURNAL_SOURCE_STATUS AS SELECT 1 AS Placeholder;');
GO

ALTER VIEW dbo.ACC_VW_JOURNAL_SOURCE_STATUS
AS
    SELECT
        J.SourceTable,
        J.SourceKey,
        J.SourceId,
        J.SourceType,
        J.T_ID AS JournalId,
        J.JournalNumber,
        J.[DATE] AS JournalDate,
        J.DATE_IN AS PostedAt,
        J.USER_ID AS PostedBy,
        J.is_Depended AS IsApproved,
        J.reverse_T_ID AS ReversalJournalId,
        R.JournalNumber AS ReversalJournalNumber,
        R.[DATE] AS ReversalDate,
        CONVERT(BIT, CASE WHEN J.reverse_T_ID IS NULL THEN 0 ELSE 1 END) AS IsReversed,
        CASE
            WHEN J.is_Depended <> 1 THEN N'قيد غير معتمد'
            WHEN J.reverse_T_ID IS NOT NULL THEN N'تم عكس القيد'
            ELSE N'قيد فعال'
        END AS JournalStatus
    FROM dbo.ACC_BALANCE_MASTER AS J
    LEFT JOIN dbo.ACC_BALANCE_MASTER AS R
        ON R.T_ID = J.reverse_T_ID
    WHERE J.SourceTable IS NOT NULL
      AND J.SourceKey IS NOT NULL
      AND ISNULL(J.is_reverse, 0) = 0;
GO

