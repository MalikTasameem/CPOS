/*
    Purpose:
      Keep operational entity maintenance independent from the accounting portal.
      Accounts/links are created or synchronized only when the feature is enabled
      for the active system model (Sys_Model.T_ID).

    Scope:
      Agents, Stores, TreasuryCard and Expenses_Card insert/update procedures.

    Compatibility: SQL Server 2014+
*/
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

ALTER PROCEDURE [dbo].[Agents_insert]
    @AG_ID int OUTPUT,
    @Ag_name nvarchar(500),
    @Barcode nvarchar(50) = '',
    @Ag_phone nvarchar(60) = NULL,
    @Address nvarchar(MAX) = NULL,
    @isDefaultAG bit = NULL,
    @isMonthEmp bit = NULL,
    @AG_Salary money = NULL,
    @Type_ID int,
    @AG_img image = NULL,
    @Max_Debit numeric(18, 2) = 0,
    @E_mail nvarchar(500),
    @is_Emp_Pause bit = 0,
    @Cr_ID INT = 1,
    @DATE_START DATE = NULL,
    @USER_ID INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE
        @ACC_CODE VARCHAR(50),
        @LINK_ID INT,
        @USE_ACCOUNTING_PORTAL BIT = 0;

    SELECT TOP (1)
        @USE_ACCOUNTING_PORTAL = CASE WHEN ISNULL(F.Use_AccountingPortal, 0) <> 0 THEN 1 ELSE 0 END
    FROM dbo.Sys_Model M
    INNER JOIN dbo.Sys_Features F ON F.T_ID = M.T_ID;

    IF NULLIF(LTRIM(RTRIM(@Ag_name)), N'') IS NULL
    BEGIN
        RAISERROR(N'يجب إدخال اسم الحساب/العميل/المورد/الموظف.', 16, 1);
        RETURN;
    END

    IF ISNULL(@Type_ID, 0) NOT IN (1, 2, 3, 4)
    BEGIN
        RAISERROR(N'نوع الحساب في Agents غير صحيح. الأنواع المسموحة هي 1 حساب عام، 2 زبون، 3 مورد، 4 موظف.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        EXEC @AG_ID = AA_GET_MAX_ID 'Agents';

        INSERT INTO dbo.Agents
        (
            AG_ID, Ag_name, Barcode, Ag_phone, Address, isDefaultAG,
            isMonthEmp, AG_Salary, Type_ID, Max_Debit, E_mail,
            is_Emp_Pause, Cr_ID, T_Balance, DATE_START
        )
        VALUES
        (
            @AG_ID, @Ag_name, @Barcode, @Ag_phone, @Address, @isDefaultAG,
            @isMonthEmp, @AG_Salary, @Type_ID, @Max_Debit, @E_mail,
            @is_Emp_Pause, @Cr_ID, 0, @DATE_START
        );

        IF @USE_ACCOUNTING_PORTAL = 1
        BEGIN
            EXEC dbo.ACC_ENTITY_CREATE_OR_SYNC_ACCOUNT
                 @ENTITY_TYPE = @Type_ID,
                 @SOURCE_ID = @AG_ID,
                 @USER_ID = @USER_ID,
                 @SYNC_ACCOUNT_NAME = 1,
                 @CHILD_DIGITS = 3,
                 @OUT_ACC_CODE = @ACC_CODE OUTPUT,
                 @OUT_LINK_ID = @LINK_ID OUTPUT;
        END

        COMMIT TRANSACTION;

        SELECT
            @AG_ID AS AG_ID,
            @Type_ID AS Type_ID,
            @ACC_CODE AS Tree_Code,
            @LINK_ID AS ACC_LINK_ID,
            CASE WHEN @USE_ACCOUNTING_PORTAL = 1
                 THEN N'تم حفظ الحساب وربطه بالدليل المحاسبي بنجاح.'
                 ELSE N'تم حفظ السجل بنجاح دون ربط محاسبي لأن البوابة المحاسبية غير مفعلة.'
            END AS [Message];
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END
GO

ALTER PROCEDURE [dbo].[Agents_update]
    @AG_ID int,
    @Ag_name nvarchar(350),
    @Barcode nvarchar(50),
    @Ag_phone nvarchar(200) = NULL,
    @Address nvarchar(250) = NULL,
    @isDefaultAG bit = NULL,
    @isMonthEmp bit = NULL,
    @AG_Salary money = NULL,
    @Type_ID INT,
    @AG_img image = NULL,
    @Max_Debit numeric(18, 2) = 0,
    @E_mail nvarchar(500),
    @is_Emp_Pause bit,
    @Cr_ID INT = 1,
    @DATE_START DATE,
    @USER_ID INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE
        @ACC_CODE VARCHAR(50),
        @LINK_ID INT,
        @USE_ACCOUNTING_PORTAL BIT = 0;

    SELECT TOP (1)
        @USE_ACCOUNTING_PORTAL = CASE WHEN ISNULL(F.Use_AccountingPortal, 0) <> 0 THEN 1 ELSE 0 END
    FROM dbo.Sys_Model M
    INNER JOIN dbo.Sys_Features F ON F.T_ID = M.T_ID;

    IF NULLIF(LTRIM(RTRIM(@Ag_name)), N'') IS NULL
    BEGIN
        RAISERROR(N'يجب إدخال اسم الحساب/العميل/المورد/الموظف.', 16, 1);
        RETURN;
    END

    IF ISNULL(@Type_ID, 0) NOT IN (1, 2, 3, 4)
    BEGIN
        RAISERROR(N'نوع الحساب في Agents غير صحيح. الأنواع المسموحة هي 1 حساب عام، 2 زبون، 3 مورد، 4 موظف.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM dbo.Agents WHERE AG_ID = @AG_ID)
    BEGIN
        RAISERROR(N'السجل غير موجود في Agents.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE dbo.Agents
        SET Ag_name = @Ag_name,
            Barcode = @Barcode,
            Ag_phone = @Ag_phone,
            Agents.Address = @Address,
            isDefaultAG = @isDefaultAG,
            isMonthEmp = @isMonthEmp,
            AG_Salary = @AG_Salary,
            Type_ID = @Type_ID,
            AG_img = @AG_img,
            Max_Debit = @Max_Debit,
            E_mail = @E_mail,
            is_Emp_Pause = @is_Emp_Pause,
            Cr_ID = @Cr_ID,
            DATE_START = @DATE_START
        WHERE AG_ID = @AG_ID;

        IF @USE_ACCOUNTING_PORTAL = 1
        BEGIN
            EXEC dbo.ACC_ENTITY_CREATE_OR_SYNC_ACCOUNT
                 @ENTITY_TYPE = @Type_ID,
                 @SOURCE_ID = @AG_ID,
                 @USER_ID = @USER_ID,
                 @SYNC_ACCOUNT_NAME = 1,
                 @CHILD_DIGITS = 3,
                 @OUT_ACC_CODE = @ACC_CODE OUTPUT,
                 @OUT_LINK_ID = @LINK_ID OUTPUT;
        END

        COMMIT TRANSACTION;

        SELECT @AG_ID AS AG_ID,
               @Type_ID AS Type_ID,
               @ACC_CODE AS Tree_Code,
               @LINK_ID AS ACC_LINK_ID,
               CASE WHEN @USE_ACCOUNTING_PORTAL = 1
                    THEN N'تم تعديل السجل ومزامنة الحساب المحاسبي بنجاح.'
                    ELSE N'تم تعديل السجل بنجاح دون مزامنة محاسبية لأن البوابة المحاسبية غير مفعلة.'
               END AS [Message];
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END
GO

ALTER PROCEDURE [dbo].[Store_insert]
    @St_Name nvarchar(2500),
    @Saler_Percent numeric(18, 2) = 0,
    @USER_ID INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE
        @ST_ID INT,
        @ACC_CODE VARCHAR(50),
        @LINK_ID INT,
        @USE_ACCOUNTING_PORTAL BIT = 0;

    SELECT TOP (1)
        @USE_ACCOUNTING_PORTAL = CASE WHEN ISNULL(F.Use_AccountingPortal, 0) <> 0 THEN 1 ELSE 0 END
    FROM dbo.Sys_Model M
    INNER JOIN dbo.Sys_Features F ON F.T_ID = M.T_ID;

    IF NULLIF(LTRIM(RTRIM(@St_Name)), N'') IS NULL
    BEGIN
        RAISERROR(N'يجب إدخال اسم المخزن.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
        EXEC @ST_ID = AA_GET_MAX_ID 'Stores';

        INSERT INTO dbo.Stores (ST_ID, St_Name, Saler_Percent)
        VALUES (@ST_ID, @St_Name, @Saler_Percent);

        IF @USE_ACCOUNTING_PORTAL = 1
        BEGIN
            EXEC dbo.ACC_ENTITY_CREATE_OR_SYNC_ACCOUNT
                 @ENTITY_TYPE = 5,
                 @SOURCE_ID = @ST_ID,
                 @USER_ID = @USER_ID,
                 @SYNC_ACCOUNT_NAME = 1,
                 @CHILD_DIGITS = 3,
                 @OUT_ACC_CODE = @ACC_CODE OUTPUT,
                 @OUT_LINK_ID = @LINK_ID OUTPUT;
        END

        COMMIT TRANSACTION;

        SELECT @ST_ID AS ST_ID,
               @ACC_CODE AS TREE_CODE,
               @LINK_ID AS ACC_LINK_ID,
               CASE WHEN @USE_ACCOUNTING_PORTAL = 1
                    THEN N'تم إضافة المخزن وربطه بالدليل المحاسبي بنجاح.'
                    ELSE N'تم إضافة المخزن بنجاح دون ربط محاسبي لأن البوابة المحاسبية غير مفعلة.'
               END AS [Message];
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END
GO

ALTER PROCEDURE [dbo].[Store_Update]
    @ST_ID INT,
    @St_Name nvarchar(2500),
    @Saler_Percent numeric(18, 2) = 0,
    @USER_ID INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE
        @ACC_CODE VARCHAR(50),
        @LINK_ID INT,
        @USE_ACCOUNTING_PORTAL BIT = 0;

    SELECT TOP (1)
        @USE_ACCOUNTING_PORTAL = CASE WHEN ISNULL(F.Use_AccountingPortal, 0) <> 0 THEN 1 ELSE 0 END
    FROM dbo.Sys_Model M
    INNER JOIN dbo.Sys_Features F ON F.T_ID = M.T_ID;

    IF NULLIF(LTRIM(RTRIM(@St_Name)), N'') IS NULL
    BEGIN
        RAISERROR(N'يجب إدخال اسم المخزن.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM dbo.Stores WHERE ST_ID = @ST_ID)
    BEGIN
        RAISERROR(N'المخزن غير موجود.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE dbo.Stores
        SET St_Name = @St_Name,
            Saler_Percent = @Saler_Percent
        WHERE ST_ID = @ST_ID;

        IF @USE_ACCOUNTING_PORTAL = 1
        BEGIN
            EXEC dbo.ACC_ENTITY_CREATE_OR_SYNC_ACCOUNT
                 @ENTITY_TYPE = 5,
                 @SOURCE_ID = @ST_ID,
                 @USER_ID = @USER_ID,
                 @SYNC_ACCOUNT_NAME = 1,
                 @CHILD_DIGITS = 3,
                 @OUT_ACC_CODE = @ACC_CODE OUTPUT,
                 @OUT_LINK_ID = @LINK_ID OUTPUT;
        END

        COMMIT TRANSACTION;

        SELECT @ST_ID AS ST_ID,
               @ACC_CODE AS TREE_CODE,
               @LINK_ID AS ACC_LINK_ID,
               CASE WHEN @USE_ACCOUNTING_PORTAL = 1
                    THEN N'تم تعديل المخزن ومزامنة الحساب المحاسبي بنجاح.'
                    ELSE N'تم تعديل المخزن بنجاح دون مزامنة محاسبية لأن البوابة المحاسبية غير مفعلة.'
               END AS [Message];
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END
GO

ALTER PROCEDURE [dbo].[TreasuryCard_Insert]
    @Tr_Name nvarchar(200),
    @Tr_AccountNumber int,
    @IS_BANK BIT,
    @USER_ID INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE
        @Tr_ID INT,
        @ACC_CODE VARCHAR(50),
        @LINK_ID INT,
        @ENTITY_TYPE TINYINT,
        @USE_ACCOUNTING_PORTAL BIT = 0;

    SELECT TOP (1)
        @USE_ACCOUNTING_PORTAL = CASE WHEN ISNULL(F.Use_AccountingPortal, 0) <> 0 THEN 1 ELSE 0 END
    FROM dbo.Sys_Model M
    INNER JOIN dbo.Sys_Features F ON F.T_ID = M.T_ID;

    IF NULLIF(LTRIM(RTRIM(@Tr_Name)), N'') IS NULL
    BEGIN
        RAISERROR(N'يجب إدخال اسم الخزينة/المصرف.', 16, 1);
        RETURN;
    END

    SET @ENTITY_TYPE = CASE WHEN ISNULL(@IS_BANK, 0) = 1 THEN 9 ELSE 6 END;

    BEGIN TRY
        BEGIN TRANSACTION;
        EXEC @Tr_ID = AA_GET_MAX_ID 'TreasuryCard';

        INSERT INTO dbo.TreasuryCard (Tr_ID, Tr_Name, Tr_AccountNumber, T_Balance, is_BANK)
        VALUES (@Tr_ID, @Tr_Name, @Tr_AccountNumber, 0, @IS_BANK);

        IF @USE_ACCOUNTING_PORTAL = 1
        BEGIN
            EXEC dbo.ACC_ENTITY_CREATE_OR_SYNC_ACCOUNT
                 @ENTITY_TYPE = @ENTITY_TYPE,
                 @SOURCE_ID = @Tr_ID,
                 @USER_ID = @USER_ID,
                 @SYNC_ACCOUNT_NAME = 1,
                 @CHILD_DIGITS = 3,
                 @OUT_ACC_CODE = @ACC_CODE OUTPUT,
                 @OUT_LINK_ID = @LINK_ID OUTPUT;
        END

        COMMIT TRANSACTION;

        SELECT @Tr_ID AS Tr_ID,
               @ENTITY_TYPE AS ENTITY_TYPE,
               @ACC_CODE AS Tree_Code,
               @LINK_ID AS ACC_LINK_ID,
               CASE WHEN @USE_ACCOUNTING_PORTAL = 1
                    THEN N'تم إضافة الخزينة/المصرف وربطه بالدليل المحاسبي بنجاح.'
                    ELSE N'تم إضافة الخزينة/المصرف بنجاح دون ربط محاسبي لأن البوابة المحاسبية غير مفعلة.'
               END AS [Message];
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END
GO

ALTER PROCEDURE [dbo].[TreasuryCard_Update]
    @Tr_ID int,
    @Tr_Name nvarchar(200),
    @Tr_AccountNumber int,
    @USER_ID INT,
    @IS_BANK BIT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE
        @ACC_CODE VARCHAR(50),
        @LINK_ID INT,
        @ENTITY_TYPE TINYINT,
        @USE_ACCOUNTING_PORTAL BIT = 0;

    SELECT TOP (1)
        @USE_ACCOUNTING_PORTAL = CASE WHEN ISNULL(F.Use_AccountingPortal, 0) <> 0 THEN 1 ELSE 0 END
    FROM dbo.Sys_Model M
    INNER JOIN dbo.Sys_Features F ON F.T_ID = M.T_ID;

    IF NULLIF(LTRIM(RTRIM(@Tr_Name)), N'') IS NULL
    BEGIN
        RAISERROR(N'يجب إدخال اسم الخزينة/المصرف.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM dbo.TreasuryCard WHERE Tr_ID = @Tr_ID)
    BEGIN
        RAISERROR(N'الخزينة/المصرف غير موجود.', 16, 1);
        RETURN;
    END

    SET @ENTITY_TYPE = CASE WHEN ISNULL(@IS_BANK, 0) = 1 THEN 9 ELSE 6 END;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE dbo.TreasuryCard
        SET Tr_Name = @Tr_Name,
            Tr_AccountNumber = @Tr_AccountNumber,
            IS_BANK = @IS_BANK
        WHERE Tr_ID = @Tr_ID;

        IF @USE_ACCOUNTING_PORTAL = 1
        BEGIN
            EXEC dbo.ACC_ENTITY_CREATE_OR_SYNC_ACCOUNT
                 @ENTITY_TYPE = @ENTITY_TYPE,
                 @SOURCE_ID = @Tr_ID,
                 @USER_ID = @USER_ID,
                 @SYNC_ACCOUNT_NAME = 1,
                 @CHILD_DIGITS = 3,
                 @OUT_ACC_CODE = @ACC_CODE OUTPUT,
                 @OUT_LINK_ID = @LINK_ID OUTPUT;
        END

        COMMIT TRANSACTION;

        SELECT @Tr_ID AS Tr_ID,
               @ENTITY_TYPE AS ENTITY_TYPE,
               @ACC_CODE AS Tree_Code,
               @LINK_ID AS ACC_LINK_ID,
               CASE WHEN @USE_ACCOUNTING_PORTAL = 1
                    THEN N'تم تعديل الخزينة/المصرف ومزامنة الحساب المحاسبي بنجاح.'
                    ELSE N'تم تعديل الخزينة/المصرف بنجاح دون مزامنة محاسبية لأن البوابة المحاسبية غير مفعلة.'
               END AS [Message];
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END
GO

ALTER PROCEDURE [dbo].[ExpCard_Insert]
    @Ex_ID int OUTPUT,
    @Ex_Name nvarchar(500),
    @USER_ID INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE
        @ACC_CODE VARCHAR(50),
        @LINK_ID INT,
        @USE_ACCOUNTING_PORTAL BIT = 0;

    SELECT TOP (1)
        @USE_ACCOUNTING_PORTAL = CASE WHEN ISNULL(F.Use_AccountingPortal, 0) <> 0 THEN 1 ELSE 0 END
    FROM dbo.Sys_Model M
    INNER JOIN dbo.Sys_Features F ON F.T_ID = M.T_ID;

    IF NULLIF(LTRIM(RTRIM(@Ex_Name)), N'') IS NULL
    BEGIN
        RAISERROR(N'يجب إدخال اسم بند المصروف.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
        EXEC @Ex_ID = AA_GET_MAX_ID 'Expenses_Card';

        INSERT INTO dbo.Expenses_Card (Ex_ID, Ex_Name)
        VALUES (@Ex_ID, @Ex_Name);

        IF @USE_ACCOUNTING_PORTAL = 1
        BEGIN
            EXEC dbo.ACC_ENTITY_CREATE_OR_SYNC_ACCOUNT
                 @ENTITY_TYPE = 7,
                 @SOURCE_ID = @Ex_ID,
                 @USER_ID = @USER_ID,
                 @SYNC_ACCOUNT_NAME = 1,
                 @CHILD_DIGITS = 3,
                 @OUT_ACC_CODE = @ACC_CODE OUTPUT,
                 @OUT_LINK_ID = @LINK_ID OUTPUT;
        END

        COMMIT TRANSACTION;

        SELECT @Ex_ID AS Ex_ID,
               @ACC_CODE AS Tree_Code,
               @LINK_ID AS ACC_LINK_ID,
               CASE WHEN @USE_ACCOUNTING_PORTAL = 1
                    THEN N'تم إضافة بند المصروف وربطه بالدليل المحاسبي بنجاح.'
                    ELSE N'تم إضافة بند المصروف بنجاح دون ربط محاسبي لأن البوابة المحاسبية غير مفعلة.'
               END AS [Message];
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END
GO

ALTER PROCEDURE [dbo].[ExpCard_Update]
    @Ex_ID int,
    @Ex_Name nvarchar(350) = NULL,
    @USER_ID INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE
        @ACC_CODE VARCHAR(50),
        @LINK_ID INT,
        @USE_ACCOUNTING_PORTAL BIT = 0;

    SELECT TOP (1)
        @USE_ACCOUNTING_PORTAL = CASE WHEN ISNULL(F.Use_AccountingPortal, 0) <> 0 THEN 1 ELSE 0 END
    FROM dbo.Sys_Model M
    INNER JOIN dbo.Sys_Features F ON F.T_ID = M.T_ID;

    IF NULLIF(LTRIM(RTRIM(@Ex_Name)), N'') IS NULL
    BEGIN
        RAISERROR(N'يجب إدخال اسم بند المصروف.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM dbo.Expenses_Card WHERE Ex_ID = @Ex_ID)
    BEGIN
        RAISERROR(N'بند المصروف غير موجود.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE dbo.Expenses_Card
        SET Ex_Name = @Ex_Name
        WHERE Ex_ID = @Ex_ID;

        IF @USE_ACCOUNTING_PORTAL = 1
        BEGIN
            EXEC dbo.ACC_ENTITY_CREATE_OR_SYNC_ACCOUNT
                 @ENTITY_TYPE = 7,
                 @SOURCE_ID = @Ex_ID,
                 @USER_ID = @USER_ID,
                 @SYNC_ACCOUNT_NAME = 1,
                 @CHILD_DIGITS = 3,
                 @OUT_ACC_CODE = @ACC_CODE OUTPUT,
                 @OUT_LINK_ID = @LINK_ID OUTPUT;
        END

        COMMIT TRANSACTION;

        SELECT @Ex_ID AS Ex_ID,
               @ACC_CODE AS Tree_Code,
               @LINK_ID AS ACC_LINK_ID,
               CASE WHEN @USE_ACCOUNTING_PORTAL = 1
                    THEN N'تم تعديل بند المصروف ومزامنة الحساب المحاسبي بنجاح.'
                    ELSE N'تم تعديل بند المصروف بنجاح دون مزامنة محاسبية لأن البوابة المحاسبية غير مفعلة.'
               END AS [Message];
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END
GO
