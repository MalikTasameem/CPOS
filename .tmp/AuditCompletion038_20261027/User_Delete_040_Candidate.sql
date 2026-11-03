-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[User_Delete] 
	@user_id int	

AS 
BEGIN

IF ( SELECT COUNT(user_id) FROM IM_Menu WHERE user_id = @user_id )  = 0
AND ( SELECT COUNT(user_id) FROM IM_Units WHERE user_id = @user_id )  = 0
-- PeopleCards035: immutable V2 activity prevents deleting its actor.
AND NOT EXISTS(SELECT 1 FROM dbo.Network_Audit_Event WITH(HOLDLOCK) WHERE ActorUserId=@user_id)
AND ( SELECT COUNT(user_id) FROM IM_Formating_Menu WHERE user_id = @user_id )  = 0
AND ( SELECT COUNT(user_id) FROM IM_QTY_ALERT WHERE user_id = @user_id )  = 0
AND ( SELECT COUNT(user_id) FROM Periods WHERE user_id = @user_id )  = 0
AND ( SELECT COUNT(user_id) FROM Others_Values WHERE user_id = @user_id )  = 0
AND ( SELECT COUNT(user_id) FROM Agents_Balance_MV WHERE user_id = @user_id )  = 0
BEGIN
delete from Users where user_id = @user_id	
END
ELSE
BEGIN
raiserror('لايمكن حذف مستخدم عليه حركة',16,1,'خطأ')
END


END
















--AS
--declare @isError int
--BEGIN TRY

--BEGIN TRANSACTION
--delete from Users where user_id = @user_id	
--COMMIT
--END TRY

--BEGIN CATCH
--IF @@TRANCOUNT > 0
--raiserror('لايمكن حذف مستخدم عليه حركة',16,1,'خطأ')
--ROLLBACK
--END CATCH
-----------------------------------------------------------------



































































