SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.VW_ACC_SYSTEM_ACCOUNT_LINKS_STATUS', N'V') IS NULL
    EXEC(N'CREATE VIEW dbo.VW_ACC_SYSTEM_ACCOUNT_LINKS_STATUS AS SELECT 1 AS Placeholder;');
GO

ALTER VIEW dbo.VW_ACC_SYSTEM_ACCOUNT_LINKS_STATUS
AS
    SELECT
        T.SystemAccountTypeID,
        T.AccountKey,
        T.AccountNameAr,
        T.Required,
        T.AllowSameAccount,
        T.MustBeLeaf,
        T.Expected_ACC_NATURAL,
        T.IsActive AS TypeIsActive,
        L.SystemAccountLinkID,
        L.ACC_T_ID,
        A.ACC_CODE,
        A.ACC_NAME,
        A.ACC_NATURAL,
        L.IsActive AS LinkIsActive,
        CONVERT(BIT, CASE WHEN A.T_ID IS NOT NULL THEN 1 ELSE 0 END) AS AccountExists,
        CONVERT(BIT, CASE WHEN ISNULL(A.is_Lock_Trans, 0) = 1 THEN 1 ELSE 0 END) AS AccountIsLocked,
        CONVERT(BIT, CASE WHEN EXISTS
        (
            SELECT 1 FROM dbo.ACCOUNTS_TREE AS C
            WHERE C.ACC_PARENT = A.ACC_CODE
        ) THEN 0 ELSE 1 END) AS AccountIsLeaf,
        CONVERT(BIT, CASE
            WHEN T.IsActive = 0 THEN 1
            WHEN T.Required = 1 AND (L.SystemAccountLinkID IS NULL OR ISNULL(L.IsActive, 0) = 0) THEN 0
            WHEN L.SystemAccountLinkID IS NOT NULL AND A.T_ID IS NULL THEN 0
            WHEN L.SystemAccountLinkID IS NOT NULL AND ISNULL(A.is_Lock_Trans, 0) = 1 THEN 0
            WHEN L.SystemAccountLinkID IS NOT NULL AND T.MustBeLeaf = 1 AND EXISTS
            (
                SELECT 1 FROM dbo.ACCOUNTS_TREE AS C
                WHERE C.ACC_PARENT = A.ACC_CODE
            ) THEN 0
            WHEN L.SystemAccountLinkID IS NOT NULL
                 AND T.Expected_ACC_NATURAL IS NOT NULL
                 AND ISNULL(A.ACC_NATURAL, '') <> T.Expected_ACC_NATURAL THEN 0
            ELSE 1
        END) AS IsValid
    FROM dbo.ACC_SYSTEM_ACCOUNT_TYPES AS T
    LEFT JOIN dbo.ACC_SYSTEM_ACCOUNT_LINKS AS L
        ON L.SystemAccountTypeID = T.SystemAccountTypeID
       AND L.IsActive = 1
    LEFT JOIN dbo.ACCOUNTS_TREE AS A
        ON A.T_ID = L.ACC_T_ID;
GO

IF OBJECT_ID(N'dbo.VW_ACC_SYSTEM_ACCOUNT_DUPLICATES', N'V') IS NULL
    EXEC(N'CREATE VIEW dbo.VW_ACC_SYSTEM_ACCOUNT_DUPLICATES AS SELECT 1 AS Placeholder;');
GO

ALTER VIEW dbo.VW_ACC_SYSTEM_ACCOUNT_DUPLICATES
AS
    SELECT
        L.ACC_T_ID,
        A.ACC_CODE,
        COUNT(*) AS LinkCount
    FROM dbo.ACC_SYSTEM_ACCOUNT_LINKS AS L
    INNER JOIN dbo.ACC_SYSTEM_ACCOUNT_TYPES AS T
        ON T.SystemAccountTypeID = L.SystemAccountTypeID
       AND T.IsActive = 1
    INNER JOIN dbo.ACCOUNTS_TREE AS A
        ON A.T_ID = L.ACC_T_ID
    WHERE L.IsActive = 1
    GROUP BY L.ACC_T_ID, A.ACC_CODE
    HAVING COUNT(*) > 1
       AND SUM(CASE WHEN T.AllowSameAccount = 0 THEN 1 ELSE 0 END) > 0;
GO

IF OBJECT_ID(N'dbo.ACC_SYSTEM_ACCOUNT_VALIDATE_FOR_POSTING', N'P') IS NULL
    EXEC(N'CREATE PROCEDURE dbo.ACC_SYSTEM_ACCOUNT_VALIDATE_FOR_POSTING AS RETURN 0;');
GO

ALTER PROCEDURE dbo.ACC_SYSTEM_ACCOUNT_VALIDATE_FOR_POSTING
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM dbo.VW_ACC_SYSTEM_ACCOUNT_LINKS_STATUS
        WHERE TypeIsActive = 1
          AND IsValid = 0
    )
    BEGIN
        DECLARE @InvalidAccounts NVARCHAR(1000);

        SELECT @InvalidAccounts = STUFF
        (
            (
                SELECT N'، ' + AccountNameAr
                FROM dbo.VW_ACC_SYSTEM_ACCOUNT_LINKS_STATUS
                WHERE TypeIsActive = 1
                  AND IsValid = 0
                ORDER BY SystemAccountTypeID
                FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)'), 1, 2, N''
        );

        RAISERROR(N'ربط الحسابات النظامية غير صالح: %s', 16, 1, @InvalidAccounts);
        RETURN;
    END;

    IF EXISTS (SELECT 1 FROM dbo.VW_ACC_SYSTEM_ACCOUNT_DUPLICATES)
    BEGIN
        RAISERROR(N'يوجد حساب مستخدم لأكثر من نوع نظامي لا يسمح بالمشاركة.', 16, 1);
        RETURN;
    END;
END;
GO
