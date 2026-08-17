USE [CPOS_ACCOUNTING]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* يعيد حماية المرحلة 003 مع استمرار السلوك القديم لسند فعال واحد. */
ALTER PROCEDURE [dbo].[AG_Balance_Update_Total]
    @T_ID INT,
    @Total MONEY,
    @Disc MONEY = 0
AS
BEGIN
    SET NOCOUNT ON
    SET XACT_ABORT ON

    BEGIN TRY
        BEGIN TRANSACTION

        DECLARE
            @AG_ID INT,
            @Bs_Type INT,
            @Tr_MV_ID INT,
            @ActiveReceiptCount INT

        EXEC [dbo].[Recalculate_Agent_Bill_Total] @T_ID

        SELECT
            @AG_ID = AG_ID,
            @Bs_Type = BsType_ID,
            @Total = TOTAL
        FROM [dbo].[Agents_Balance_MV_V]
        WHERE T_ID = @T_ID

        IF @Bs_Type IN (1, 2, 7, 10, 11)
           AND ISNULL((SELECT is_Auto_Pied FROM [dbo].[Agents] WHERE AG_ID = @AG_ID), 0) = 1
        BEGIN
            SELECT @ActiveReceiptCount = COUNT(*)
            FROM [dbo].[Agents_Balance_MV_RCT]
            WHERE Receipt_Tran_ID = @T_ID
              AND AG_ID = @AG_ID
              AND ISNULL(isVoid, 0) = 0

            IF @ActiveReceiptCount = 1
            BEGIN
                SELECT @Tr_MV_ID = T_ID
                FROM [dbo].[Agents_Balance_MV_RCT]
                WHERE Receipt_Tran_ID = @T_ID
                  AND AG_ID = @AG_ID
                  AND ISNULL(isVoid, 0) = 0

                UPDATE [dbo].[Agents_Balance_MV_RCT]
                SET Pure = (@Total - @Disc)
                WHERE T_ID = @Tr_MV_ID

                IF @Bs_Type = 1
                BEGIN
                    UPDATE [dbo].[Treasury_Balance_MV]
                    SET Pure = (@Total - @Disc), Credit = (@Total - @Disc)
                    WHERE AGBalance_T_ID = @Tr_MV_ID
                END
                ELSE
                BEGIN
                    UPDATE [dbo].[Treasury_Balance_MV]
                    SET Pure = (@Total - @Disc), Debit = (@Total - @Disc)
                    WHERE AGBalance_T_ID = @Tr_MV_ID
                END

                EXEC [dbo].[Agents_Balance_MV_RCT_OpenForEdit]
                    @T_ID = @Tr_MV_ID,
                    @User_ID = 1
            END
        END

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION

        DECLARE @ERR_STR NVARCHAR(MAX)
        SET @ERR_STR = N'[AG_Balance_Update_Total] ' + ERROR_MESSAGE()
        RAISERROR(@ERR_STR, 16, 1)
    END CATCH
END
GO

