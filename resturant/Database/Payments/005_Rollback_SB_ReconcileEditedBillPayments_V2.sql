USE [CPOS_ACCOUNTING]
GO

IF OBJECT_ID(N'dbo.SB_ReconcileEditedBillPayments_V2', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SB_ReconcileEditedBillPayments_V2]
GO

