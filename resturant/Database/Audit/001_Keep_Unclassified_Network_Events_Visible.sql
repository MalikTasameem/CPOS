/*
  Phase 1, SQL Server 2014. Select the intended application database in SSMS.
  Changes the VIEW ONLY; no table/schema columns, business data, or historical rows are changed.
  The updated Network_Tracker screen also reads with LEFT JOIN independently of this deployment.
  Review Phase0_Live_Metadata.json and retain the previous view definition before applying.
  This file is prepared for deployment; the coding review does not automatically execute it.
*/
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;

IF OBJECT_ID(N'dbo.Network_Edit_Tracker_V', N'V') IS NULL
BEGIN
    RAISERROR(N'Network_Edit_Tracker_V does not exist in the selected database. No change applied.',16,1);
    RETURN;
END;

BEGIN TRY
    BEGIN TRANSACTION;
    EXEC sys.sp_executesql N'
ALTER VIEW dbo.Network_Edit_Tracker_V
AS
SELECT n.T_ID,n.CP_Name,
       COALESCE(NULLIF(u.UserName,N''''),N''مستخدم غير معرّف (''+CONVERT(nvarchar(20),n.User_ID)+N'')'') AS UserName,
       n.[Date],
       COALESCE(NULLIF(s.Type_Name,N''''),N''تصنيف غير معرّف (''+ISNULL(CONVERT(nvarchar(20),n.Screen_Type),N''NULL'')+N'')'') AS Type_Name,
       COALESCE(NULLIF(o.Operation_Name,N''''),N''عملية غير معرّفة (''+ISNULL(CONVERT(nvarchar(20),n.Operation_ID),N''NULL'')+N'')'') AS Operation_Name,
       n.Notes,n.Bill_ID,n.Screen_Type,n.Operation_ID,n.User_ID
FROM dbo.Network_Edit_Tracker n
LEFT JOIN dbo.Users u ON u.User_ID=n.User_ID
LEFT JOIN dbo.AgentBalance_Type s ON s.id=n.Screen_Type
LEFT JOIN dbo.Operation_Types o ON o.T_ID=n.Operation_ID;';
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
