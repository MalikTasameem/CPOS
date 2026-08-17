USE [CPOS_ACCOUNTING]
GO

/*
    يستخدم فقط قبل ربط أي شاشة بالإجراء الجديد.
    لا يحذف بيانات ولا يغير أي جدول أو إجراء قديم.
*/
IF OBJECT_ID(N'dbo.SB_ConfermBill_V2', N'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.SB_ConfermBill_V2
END
GO

IF TYPE_ID(N'dbo.SalePaymentAllocationType') IS NOT NULL
BEGIN
    DROP TYPE dbo.SalePaymentAllocationType
END
GO

