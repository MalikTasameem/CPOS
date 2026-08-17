USE [CPOS_ACCOUNTING]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* النوع مشترك مع SB_ConfermBill_V2، ويُنشأ هنا فقط إذا لم يسبق نشر المرحلة الأولى. */
IF TYPE_ID(N'dbo.SalePaymentAllocationType') IS NULL
BEGIN
    EXEC(N'
        CREATE TYPE dbo.SalePaymentAllocationType AS TABLE
        (
            [LineNo]           INT             NOT NULL PRIMARY KEY,
            PaymentMethodID    INT             NOT NULL,
            TreasuryID         INT             NOT NULL,
            Amount             DECIMAL(18, 3)  NOT NULL,
            ReferenceNumber    NVARCHAR(100)   NULL,
            BankName           NVARCHAR(150)   NULL,
            CheckNumber        NVARCHAR(100)   NULL,
            Notes              NVARCHAR(500)   NULL
        )
    ')
END
GO

IF OBJECT_ID(N'dbo.PushSalesDraft_V2', N'P') IS NULL
BEGIN
    EXEC(N'CREATE PROCEDURE dbo.PushSalesDraft_V2 AS RETURN 0')
END
GO

ALTER PROCEDURE [dbo].[PushSalesDraft_V2]
(
    @AG_ID INT,
    @S_Bill_Pr_ID INT = NULL,
    @Table_ID INT = NULL,
    @Date DATETIME,
    @Discount NUMERIC(18, 3) = 0,
    @About NVARCHAR(MAX) = NULL,
    @BsType_ID INT,
    @isVoid INT = 0,
    @isPied INT = NULL,
    @User_ID INT,
    @Markter_ID INT = NULL,
    @Details dbo.SB_Contents_DraftType READONLY,
    @Payments dbo.SalePaymentAllocationType READONLY,
    @Pr_ID INT
)
AS
BEGIN
    SET NOCOUNT ON
    SET XACT_ABORT ON

    DECLARE
        @Header_T_ID INT,
        @Bill_Num INT,
        @SB_ID INT,
        @Total NUMERIC(18, 3),
        @Pure NUMERIC(18, 3),
        @NextDetailTID BIGINT,
        @Receipt_Title NVARCHAR(2500),
        @PaymentsTotal DECIMAL(18, 3),
        @PaymentMethodID INT,
        @TreasuryID INT,
        @PaymentAmount DECIMAL(18, 3),
        @ReferenceNumber NVARCHAR(100),
        @BankName NVARCHAR(150),
        @CheckNumber NVARCHAR(100),
        @Notes NVARCHAR(500),
        @IsAutoPaid BIT

    BEGIN TRY
        BEGIN TRANSACTION

        IF NOT EXISTS (SELECT 1 FROM @Details)
            RAISERROR(N'لا يمكن حفظ فاتورة بدون تفاصيل.', 16, 1)

        IF @BsType_ID IS NULL OR @User_ID IS NULL OR @AG_ID IS NULL
            RAISERROR(N'بيانات رأس الفاتورة غير مكتملة.', 16, 1)

        SELECT @Total = ISNULL(SUM(T_Price), 0)
        FROM @Details

        SET @Pure = ISNULL(@Total, 0) - ISNULL(@Discount, 0)

        IF @Pure < 0
            RAISERROR(N'صافي الفاتورة لا يمكن أن يكون سالباً.', 16, 1)

        SELECT @IsAutoPaid = ISNULL(is_Auto_Pied, 0)
        FROM Agents
        WHERE AG_ID = @AG_ID

        SELECT @PaymentsTotal = ISNULL(SUM(Amount), 0)
        FROM @Payments

        IF ISNULL(@IsAutoPaid, 0) = 1
        BEGIN
            IF NOT EXISTS (SELECT 1 FROM @Payments)
                RAISERROR(N'يجب إدخال طريقة دفع واحدة على الأقل.', 16, 1)

            IF EXISTS
            (
                SELECT 1
                FROM @Payments
                WHERE PaymentMethodID <= 0 OR TreasuryID <= 0 OR Amount <= 0
            )
                RAISERROR(N'توجد بيانات غير صحيحة في قائمة الدفعات.', 16, 1)

            IF @PaymentsTotal <> @Pure
                RAISERROR(N'مجموع الدفعات لا يساوي صافي الفاتورة.', 16, 1)

            IF EXISTS
            (
                SELECT 1
                FROM @Payments payment
                WHERE NOT EXISTS
                (
                    SELECT 1
                    FROM PaymentMethodDefaultAccounts account
                    WHERE account.PaymentMethodID = payment.PaymentMethodID
                      AND ISNULL(account.IsActive, 0) = 1
                )
            )
                RAISERROR(N'توجد طريقة دفع غير مفعلة.', 16, 1)

            IF EXISTS
            (
                SELECT 1
                FROM @Payments payment
                INNER JOIN PaymentMethodDefaultAccounts account
                    ON account.PaymentMethodID = payment.PaymentMethodID
                   AND ISNULL(account.IsActive, 0) = 1
                   AND ISNULL(account.is_Lock, 0) = 1
                WHERE account.AccountID <> payment.TreasuryID
            )
                RAISERROR(N'الخزينة المحددة لا تطابق الحساب المقفل لطريقة الدفع.', 16, 1)

            IF EXISTS
            (
                SELECT 1
                FROM @Payments payment
                WHERE NOT EXISTS
                (
                    SELECT 1
                    FROM TreasuryCard treasury
                    WHERE treasury.Tr_ID = payment.TreasuryID
                )
            )
                RAISERROR(N'توجد خزينة غير معرفة في قائمة الدفعات.', 16, 1)
        END
        ELSE IF EXISTS (SELECT 1 FROM @Payments)
        BEGIN
            RAISERROR(N'العميل المحدد لا يستخدم السداد التلقائي.', 16, 1)
        END

        EXEC @Header_T_ID = AA_GET_MAX_ID 'Agents_Balance_MV'
        EXEC @Bill_Num = AG_BalanceMV_GetMax_BillNum
        EXEC @SB_ID = AG_BalanceMV_GetMax_SB_ID

        INSERT INTO dbo.Agents_Balance_MV
        (
            T_ID, AG_ID, SB_ID, S_Bill_Pr_ID, Table_ID, [Date], Total,
            Discount, Pure, About, BsType_ID, isDepended, isVoid, isPied,
            User_ID, Markter_ID, Barcode
        )
        VALUES
        (
            @Header_T_ID, @AG_ID, @SB_ID, @S_Bill_Pr_ID, @Table_ID, @Date,
            @Total, @Discount, @Pure, @About, @BsType_ID, 1, @isVoid,
            @isPied, @User_ID, @Markter_ID, (@Header_T_ID + 100)
        )

        EXEC @NextDetailTID = AA_GET_MAX_ID 'SB_Contents'

        ;WITH D AS
        (
            SELECT
                ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS RN,
                IM_ID, U_ID, ST_ID, [Date], Compons, Cost, Price, D_Vaild,
                QTY, T_Price, U_Cargo, ST_QTY, isDepended, Barcode
            FROM @Details
        )
        INSERT INTO dbo.SB_Contents
        (
            T_ID, Bill_T_ID, IM_ID, U_ID, ST_ID, [Date], Compons, Cost,
            Price, D_Vaild, QTY, T_Price, U_Cargo, ST_QTY, isDepended, Barcode
        )
        SELECT
            @NextDetailTID + RN, @Header_T_ID, IM_ID, U_ID, ST_ID, [Date],
            Compons, Cost, Price, D_Vaild, QTY, T_Price, U_Cargo, ST_QTY, 1,
            Barcode
        FROM D

        IF ISNULL(@IsAutoPaid, 0) = 1
        BEGIN
            SET @Receipt_Title = N'فاتورة مبيعات : ' + CAST(@SB_ID AS NVARCHAR(50))

            DECLARE PaymentCursor CURSOR LOCAL FAST_FORWARD FOR
                SELECT
                    PaymentMethodID, TreasuryID, Amount, ReferenceNumber,
                    BankName, CheckNumber, Notes
                FROM @Payments
                ORDER BY [LineNo]

            OPEN PaymentCursor

            FETCH NEXT FROM PaymentCursor INTO
                @PaymentMethodID, @TreasuryID, @PaymentAmount,
                @ReferenceNumber, @BankName, @CheckNumber, @Notes

            WHILE @@FETCH_STATUS = 0
            BEGIN
                EXEC [Agents_BalanceMV_insert_RCT]
                    0, @Date, 0, @AG_ID, @Pr_ID, @Header_T_ID,
                    @ReferenceNumber, @Receipt_Title, @PaymentAmount, @Notes,
                    3, @User_ID, @TreasuryID, @BankName, @CheckNumber,
                    @PaymentMethodID

                FETCH NEXT FROM PaymentCursor INTO
                    @PaymentMethodID, @TreasuryID, @PaymentAmount,
                    @ReferenceNumber, @BankName, @CheckNumber, @Notes
            END

            CLOSE PaymentCursor
            DEALLOCATE PaymentCursor
        END

        COMMIT TRANSACTION

        SELECT
            CAST(1 AS BIT) AS IsSuccess,
            @Header_T_ID AS Header_T_ID,
            @SB_ID AS SB_ID,
            @Total AS Total,
            @Pure AS Pure
    END TRY
    BEGIN CATCH
        IF CURSOR_STATUS('local', 'PaymentCursor') >= 0
            CLOSE PaymentCursor

        IF CURSOR_STATUS('local', 'PaymentCursor') > -3
            DEALLOCATE PaymentCursor

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION

        SELECT
            CAST(0 AS BIT) AS IsSuccess,
            ERROR_NUMBER() AS ErrorNumber,
            ERROR_MESSAGE() AS ErrorMessage,
            ERROR_LINE() AS ErrorLine
    END CATCH
END
GO

