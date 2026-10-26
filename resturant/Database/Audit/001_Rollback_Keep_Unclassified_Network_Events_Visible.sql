/*
  Restores the INNER JOIN definition verified in Phase 0 (2026-10-13).
  Run only when reversing migration 001 on a database with that baseline.
  WARNING: unclassified legacy events will again be hidden from view consumers.
  The rows themselves are not changed. The new screen has its own LEFT JOIN search.
*/
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;
IF OBJECT_ID(N'dbo.Network_Edit_Tracker_V',N'V') IS NULL
BEGIN
    RAISERROR(N'Network_Edit_Tracker_V does not exist. No change applied.',16,1);
    RETURN;
END;
BEGIN TRY
    BEGIN TRANSACTION;
    EXEC sys.sp_executesql N'
ALTER VIEW dbo.Network_Edit_Tracker_V
AS
SELECT n.T_ID,n.CP_Name,u.UserName,n.[Date],s.Type_Name,o.Operation_Name,
       n.Notes,n.Bill_ID,n.Screen_Type,n.Operation_ID,n.User_ID
FROM dbo.Network_Edit_Tracker n
INNER JOIN dbo.Users u ON u.User_ID=n.User_ID
INNER JOIN dbo.AgentBalance_Type s ON s.id=n.Screen_Type
INNER JOIN dbo.Operation_Types o ON o.T_ID=n.Operation_ID;';
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
