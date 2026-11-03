ALTER PROCEDURE [dbo].[ViewBill_Contents_Delete_IM]
	@T_ID int
AS
BEGIN TRY
BEGIN TRANSACTION
DECLARE @ST_QTY NUMERIC(18,3),@U_Cargo FLOAT,@U_ID INT,@IM_ID INT,@ST_ID INT,@D_Vaild NVARCHAR(50),@Bill_T_ID INT

SELECT @U_Cargo = U_Cargo ,@IM_ID=IM_ID, @ST_ID=ST_ID,@D_Vaild=D_Vaild,@ST_QTY=ST_QTY , @Bill_T_ID = Bill_T_ID FROM ViewBill_Contents where T_ID = @T_ID
DELETE FROM ViewBill_Contents  where T_ID = @T_ID
-- QuoteDelete033: quote IDs are not sales-line IDs. Do not touch SB_IM_Components.

EXEC [Recalculate_Agent_Bill_Total] @Bill_T_ID
 
COMMIT
END TRY

BEGIN CATCH
IF @@TRANCOUNT > 0
raiserror('[ViewBill_Contents_Delete_IM]',16,1,'خطأ')
ROLLBACK
END CATCH




























































