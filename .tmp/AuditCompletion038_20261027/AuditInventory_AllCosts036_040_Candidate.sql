/* AuditInventoryLocalCost036: all items use the isolated per-item implementation. */

CREATE PROCEDURE [dbo].[AuditInventory_AllCosts036]

AS
BEGIN TRY

BEGIN TRANSACTION 

-- The original initial global snapshot had no business side effects; each item still takes its own snapshot.




DECLARE @I INT = 0 
SELECT @I = MIN(IM_ID) FROM IM_Menu WHERE isStore IN (1,2)
WHILE @I IS NOT NULL
BEGIN

--IF (SELECT IM_ID FROM FRM_Details WHERE IM_ID = @I) IS NULL
BEGIN EXEC [AuditInventory_ItemCost036] @I END

SELECT @I = MIN(IM_ID) FROM IM_Menu WHERE IM_ID > @I AND isStore IN (1,2)
 
END


COMMIT
END TRY

BEGIN CATCH
IF @@TRANCOUNT > 0
DECLARE @ERR_STR NVARCHAR(MAX)
       SET @ERR_STR  = '[RECOUNT_IM_ALL_MainCost] ' + ERROR_MESSAGE()
	   raiserror(@ERR_STR,16,1,'خطأ')
ROLLBACK
END CATCH











