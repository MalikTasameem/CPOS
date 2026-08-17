USE [CPOS_ACCOUNTING]
GO

/* يحذف طبقة المسودات الجديدة فقط، ولا يحذف النوع المشترك أو أي بيانات. */
IF OBJECT_ID(N'dbo.PushSalesDraft_V2', N'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.PushSalesDraft_V2
END
GO

