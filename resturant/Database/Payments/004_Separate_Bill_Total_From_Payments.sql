USE [CPOS_ACCOUNTING]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
    المرحلة النهائية لفصل مسؤولية حساب الفاتورة عن تسوية الدفع.

    هذا الإجراء لا يعدل:
      dbo.Agents_Balance_MV_RCT
      dbo.Treasury_Balance_MV

    تبقى السندات مجمدة أثناء تعديل الأصناف، وتعالج الفروق لاحقًا بواسطة
    إجراء تسوية مستقل يسجل تحصيلًا أو استردادًا جديدًا.
*/
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

        IF NOT EXISTS
        (
            SELECT 1
            FROM [dbo].[Agents_Balance_MV]
            WHERE T_ID = @T_ID
        )
            RAISERROR(N'لم يتم العثور على المعاملة المطلوب إعادة حسابها.', 16, 1)

        EXEC [dbo].[Recalculate_Agent_Bill_Total] @T_ID

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

