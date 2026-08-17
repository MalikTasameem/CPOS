USE [CPOS_ACCOUNTING]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* يعيد نسخة الإجراء التي كانت مستخدمة قبل تطبيق حماية تعدد الدفعات. */
ALTER PROCEDURE [dbo].[AG_Balance_Update_Total]
    @T_ID INT,
    @Total MONEY,
    @Disc MONEY = 0
AS
BEGIN TRY
BEGIN TRANSACTION

DECLARE @Cr_Equal_Value NUMERIC(18,3), @AG_ID INT, @Bs_Type INT,
        @Tr_MV_ID INT, @Tr_ID INT

EXEC Recalculate_Agent_Bill_Total @T_ID

SELECT @AG_ID = AG_ID, @Bs_Type = BsType_ID, @Total = TOTAL
FROM Agents_Balance_MV_V
WHERE T_ID = @T_ID

IF @Bs_Type = 1
BEGIN
    IF (SELECT is_Auto_Pied FROM Agents WHERE AG_ID = @AG_ID) = 1
    BEGIN
        SELECT @Tr_MV_ID = T_ID
        FROM Agents_Balance_MV_RCT
        WHERE Receipt_Tran_ID = @T_ID

        UPDATE Agents_Balance_MV_RCT
        SET Pure = (@Total - @Disc)
        WHERE Receipt_Tran_ID = @T_ID AND AG_ID = @AG_ID

        UPDATE Treasury_Balance_MV
        SET Pure = (@Total - @Disc), Credit = (@Total - @Disc)
        WHERE AGBalance_T_ID = @Tr_MV_ID

        EXEC [dbo].[Agents_Balance_MV_RCT_OpenForEdit]
            @T_ID = @Tr_MV_ID,
            @User_ID = 1
    END
END

IF @Bs_Type = 2 OR @Bs_Type = 7 OR @Bs_Type = 10 OR @Bs_Type = 11
BEGIN
    IF (SELECT is_Auto_Pied FROM Agents WHERE AG_ID = @AG_ID) = 1
    BEGIN
        SELECT @Tr_MV_ID = T_ID
        FROM Agents_Balance_MV_RCT
        WHERE Receipt_Tran_ID = @T_ID

        UPDATE Agents_Balance_MV_RCT
        SET Pure = (@Total - @Disc)
        WHERE Receipt_Tran_ID = @T_ID AND AG_ID = @AG_ID

        UPDATE Treasury_Balance_MV
        SET Pure = (@Total - @Disc), Debit = (@Total - @Disc)
        WHERE AGBalance_T_ID = @Tr_MV_ID

        EXEC [dbo].[Agents_Balance_MV_RCT_OpenForEdit]
            @T_ID = @Tr_MV_ID,
            @User_ID = 1
    END
END

COMMIT
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
    BEGIN
        DECLARE @ERR_STR NVARCHAR(MAX)
        SET @ERR_STR = '[AG_Balance_Update_Total] ' + ERROR_MESSAGE()
        RAISERROR(@ERR_STR, 16, 1, 'خطأ')
        ROLLBACK
    END
END CATCH
GO

