USE [CPOS_ACCOUNTING]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
    طبقة دفع متعددة مستقلة عن البنية الحالية.
    لا ينشئ هذا الملف أي جدول ولا يعدل SB_ConfermBill الحالي.
*/
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

IF OBJECT_ID(N'dbo.SB_ConfermBill_V2', N'P') IS NULL
BEGIN
    EXEC(N'CREATE PROCEDURE dbo.SB_ConfermBill_V2 AS RETURN 0')
END
GO

ALTER PROCEDURE [dbo].[SB_ConfermBill_V2]
    @T_ID INT,
    @TOTAL DECIMAL(18, 3),
    @Discount DECIMAL(18, 3),
    @Pure DECIMAL(18, 3),
    @Pied DECIMAL(18, 3) = NULL,
    @AGType_ID INT,
    @Point_Inc DECIMAL(18, 3) = NULL,
    @Points_Sale DECIMAL(18, 3) = NULL,
    @Deliver_date NVARCHAR(150) = NULL,
    @Order_isDeleverd BIT = NULL,
    @isCostmerScreen BIT = NULL,
    @Pr_ID INT,
    @TB_ID INT = NULL,
    @User_ID INT = NULL,
    @Payments dbo.SalePaymentAllocationType READONLY
AS
BEGIN
    SET NOCOUNT ON
    SET XACT_ABORT ON
    SET DEADLOCK_PRIORITY NORMAL

    DECLARE
        @B_Type INT,
        @TB_ORDER_CODE NVARCHAR(100) = NULL,
        @TB_NAME NVARCHAR(100) = NULL,
        @Bill_Num INT,
        @AG_ID INT,
        @SB_ID NVARCHAR(25),
        @Receipt_Title NVARCHAR(500),
        @DATE DATETIME,
        @isPied INT = 0,
        @LockedT_ID INT,
        @PaymentsTotal DECIMAL(18, 3),
        @PaymentMethodID INT,
        @TreasuryID INT,
        @PaymentAmount DECIMAL(18, 3),
        @ReferenceNumber NVARCHAR(100),
        @BankName NVARCHAR(150),
        @CheckNumber NVARCHAR(100),
        @Notes NVARCHAR(500),
        @ErrorMessage NVARCHAR(2048)

    BEGIN TRY
        IF @T_ID IS NULL OR @T_ID <= 0
            RAISERROR(N'رقم الفاتورة غير صحيح.', 16, 1)

        IF @TOTAL < 0 OR @Discount < 0 OR @Pure < 0
            RAISERROR(N'قيم الفاتورة المالية غير صحيحة.', 16, 1)

        IF EXISTS
        (
            SELECT 1
            FROM @Payments
            WHERE PaymentMethodID <= 0
               OR TreasuryID <= 0
               OR Amount <= 0
        )
            RAISERROR(N'توجد بيانات غير صحيحة في قائمة الدفعات.', 16, 1)

        BEGIN TRANSACTION

        /* يمنع إنهاء الفاتورة نفسها بالتزامن من جهازين. */
        SELECT
            @LockedT_ID = T_ID
        FROM Agents_Balance_MV WITH (UPDLOCK, HOLDLOCK)
        WHERE T_ID = @T_ID

        IF @LockedT_ID IS NULL
            RAISERROR(N'لم يتم العثور على الفاتورة المطلوب إنهاؤها.', 16, 1)

        SELECT
            @AG_ID = AG_ID,
            @B_Type = S_Bills_Type,
            @Bill_Num = S_Bill_Pr_ID,
            @isPied = isPied,
            @SB_ID = SB_ID,
            @DATE = GETDATE()
        FROM SB_Info_V
        WHERE T_ID = @T_ID

        IF @AG_ID IS NULL
            RAISERROR(N'تعذر قراءة بيانات الفاتورة.', 16, 1)

        /* نفس قاعدة السداد التلقائي الموجودة في SB_ConfermBill الحالي. */
        IF ((SELECT is_Auto_Pied FROM Agents WHERE AG_ID = @AG_ID) = 1)
           OR (@AGType_ID IN (3, 5))
        BEGIN
            SET @Pied = @Pure
        END

        SELECT @PaymentsTotal = ISNULL(SUM(Amount), 0)
        FROM @Payments

        IF ISNULL(@Pied, 0) > 0 AND @isPied = 0
        BEGIN
            IF NOT EXISTS (SELECT 1 FROM @Payments)
                RAISERROR(N'يجب إدخال طريقة دفع واحدة على الأقل.', 16, 1)

            IF @PaymentsTotal <> @Pied
                RAISERROR(N'مجموع الدفعات لا يساوي المبلغ المقبوض.', 16, 1)

            IF EXISTS
            (
                SELECT 1
                FROM @Payments payment
                WHERE NOT EXISTS
                (
                    SELECT 1
                    FROM PAYMENT_METHOD method
                    WHERE method.P_ID = payment.PaymentMethodID
                )
            )
                RAISERROR(N'توجد طريقة دفع غير معرفة.', 16, 1)

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
            RAISERROR(N'لا يجب إرسال دفعات لفاتورة لا تستلزم إنشاء سند قبض جديد.', 16, 1)
        END

        EXEC [SB_Contents_Structers_insert] @T_ID

        IF @TB_ID IS NOT NULL
        BEGIN
            SELECT
                @TB_ORDER_CODE = TB_ORDER_CODE,
                @TB_NAME = T_NAME
            FROM Tables
            WHERE TB_ID = @TB_ID

            SET @TB_NAME = N' جزء من طاولة ' + @TB_NAME

            UPDATE Agents_Balance_MV
            SET About = @TB_NAME
            WHERE T_ID = @T_ID
        END

        UPDATE Agents_Balance_MV
        SET isDepended = 1,
            Receipt_Title = @SB_ID,
            Total = @TOTAL,
            Discount = @Discount,
            SB_isOpen = 0,
            isPause = NULL,
            Deliver_date = @Deliver_date,
            Order_isDeleverd = @Order_isDeleverd,
            TB_ORDER_CODE = @TB_ORDER_CODE,
            Table_ID = @TB_ID
        WHERE T_ID = @T_ID

        UPDATE Periods
        SET AG_Counter += 1
        WHERE Pr_ID = @Pr_ID

        IF ISNULL(@Pied, 0) > 0 AND @isPied = 0
        BEGIN
            SET @Receipt_Title = N'فاتورة مبيعات  : ' + @SB_ID

            DECLARE PaymentCursor CURSOR LOCAL FAST_FORWARD FOR
                SELECT
                    PaymentMethodID,
                    TreasuryID,
                    Amount,
                    ReferenceNumber,
                    BankName,
                    CheckNumber,
                    Notes
                FROM @Payments
                ORDER BY [LineNo]

            OPEN PaymentCursor

            FETCH NEXT FROM PaymentCursor INTO
                @PaymentMethodID,
                @TreasuryID,
                @PaymentAmount,
                @ReferenceNumber,
                @BankName,
                @CheckNumber,
                @Notes

            WHILE @@FETCH_STATUS = 0
            BEGIN
                EXEC [Agents_BalanceMV_insert_RCT]
                    0,
                    @DATE,
                    0,
                    @AG_ID,
                    @Pr_ID,
                    @T_ID,
                    @ReferenceNumber,
                    @Receipt_Title,
                    @PaymentAmount,
                    @Notes,
                    3,
                    @User_ID,
                    @TreasuryID,
                    @BankName,
                    @CheckNumber,
                    @PaymentMethodID

                FETCH NEXT FROM PaymentCursor INTO
                    @PaymentMethodID,
                    @TreasuryID,
                    @PaymentAmount,
                    @ReferenceNumber,
                    @BankName,
                    @CheckNumber,
                    @Notes
            END

            CLOSE PaymentCursor
            DEALLOCATE PaymentCursor
        END

        IF @isCostmerScreen = 1
        BEGIN
            INSERT INTO Costmer_Screen
                (T_ID, Bill_Num, isawait, isShowed, TimeStart)
            VALUES
                (@T_ID, @Bill_Num, 1, 1, GETDATE())
        END

        IF @TB_ID IS NOT NULL
        BEGIN
            UPDATE Agents_Balance_MV
            SET TB_ORDER_CODE =
                (SELECT TB_ORDER_CODE FROM Tables WHERE TB_ID = @TB_ID)
            WHERE T_ID = @T_ID
        END

        IF (SELECT TB_Auto_Print FROM Sys_Features
            WHERE T_ID = (SELECT T_ID FROM Sys_Model)) = 1
        BEGIN
            INSERT INTO TB_AutoPrint_tmp (T_ID)
            VALUES (@T_ID)
        END

        EXEC Recalculate_Agent_Bill_Total @T_ID

        COMMIT TRANSACTION

        SELECT
            CAST(1 AS BIT) AS IsSuccess,
            @T_ID AS T_ID,
            @PaymentsTotal AS PaymentsTotal,
            (SELECT COUNT(*) FROM @Payments) AS PaymentsCount
    END TRY
    BEGIN CATCH
        IF CURSOR_STATUS('local', 'PaymentCursor') >= 0
            CLOSE PaymentCursor

        IF CURSOR_STATUS('local', 'PaymentCursor') > -3
            DEALLOCATE PaymentCursor

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION

        SET @ErrorMessage = ERROR_MESSAGE()
        RAISERROR(@ErrorMessage, 16, 1)
    END CATCH
END
GO
