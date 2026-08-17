USE [CPOS_ACCOUNTING]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID(N'dbo.SB_ReconcileEditedBillPayments_V2', N'P') IS NULL
BEGIN
    EXEC(N'CREATE PROCEDURE dbo.SB_ReconcileEditedBillPayments_V2 AS RETURN 0')
END
GO

ALTER PROCEDURE [dbo].[SB_ReconcileEditedBillPayments_V2]
    @T_ID INT,
    @ExpectedOriginalPure DECIMAL(18, 3),
    @ExpectedNewPure DECIMAL(18, 3),
    @ExpectedNetPaid DECIMAL(18, 3),
    @User_ID INT,
    @Pr_ID INT,
    @Payments dbo.SalePaymentAllocationType READONLY
AS
BEGIN
    SET NOCOUNT ON
    SET XACT_ABORT ON

    DECLARE
        @AG_ID INT,
        @SB_ID NVARCHAR(25),
        @NewPure DECIMAL(18, 3),
        @CurrentNetPaid DECIMAL(18, 3),
        @FinalNetPaid DECIMAL(18, 3),
        @Adjustment DECIMAL(18, 3),
        @RequiredAmount DECIMAL(18, 3),
        @PaymentsTotal DECIMAL(18, 3),
        @ReceiptTypeID INT,
        @ReceiptTitle NVARCHAR(500),
        @PaymentMethodID INT,
        @TreasuryID INT,
        @PaymentAmount DECIMAL(18, 3),
        @ReferenceNumber NVARCHAR(100),
        @BankName NVARCHAR(150),
        @CheckNumber NVARCHAR(100),
        @Notes NVARCHAR(500),
        @CreatedReceiptID INT,
        @CreatedReceiptNumber INT,
        @Date DATETIME

    BEGIN TRY
        BEGIN TRANSACTION

        SELECT
            @AG_ID = AG_ID,
            @SB_ID = CONVERT(NVARCHAR(25), SB_ID),
            @NewPure = ISNULL(Pure, 0)
        FROM [dbo].[Agents_Balance_MV] WITH (UPDLOCK, HOLDLOCK)
        WHERE T_ID = @T_ID
          AND BsType_ID = 1
          AND ISNULL(isVoid, 0) = 0

        IF @AG_ID IS NULL
            RAISERROR(N'لم يتم العثور على فاتورة مبيعات فعالة للتسوية.', 16, 1)

        IF ISNULL((SELECT is_Auto_Pied FROM [dbo].[Agents] WHERE AG_ID = @AG_ID), 0) <> 1
            RAISERROR(N'تسوية العملاء الآجلين أو ذوي الدفع الجزئي تتطلب اختيار سياسة الرصيد من شاشة التسوية.', 16, 1)

        IF @ExpectedOriginalPure < 0 OR @ExpectedNetPaid < 0 OR @NewPure < 0
            RAISERROR(N'قيم الفاتورة أو لقطة الدفع غير صحيحة.', 16, 1)

        IF @NewPure <> @ExpectedNewPure
            RAISERROR(N'تغير صافي الفاتورة بعد فتح شاشة التسوية. أعد تحميل الفاتورة.', 16, 1)

        SELECT @CurrentNetPaid = ISNULL(SUM(
            CASE
                WHEN BsType_ID = 3 THEN ISNULL(Pure, 0)
                WHEN BsType_ID = 4 THEN -ISNULL(Pure, 0)
                ELSE 0
            END), 0)
        FROM [dbo].[Agents_Balance_MV_RCT] WITH (UPDLOCK, HOLDLOCK)
        WHERE Receipt_Tran_ID = @T_ID
          AND BsType_ID IN (3, 4)
          AND ISNULL(isVoid, 0) = 0

        IF @CurrentNetPaid <> @ExpectedNetPaid
            RAISERROR(N'تغيرت دفعات الفاتورة بعد فتحها للتعديل. أعد تحميل الفاتورة قبل التسوية.', 16, 1)

        SET @Adjustment = @NewPure - @CurrentNetPaid
        SET @RequiredAmount = ABS(@Adjustment)

        SELECT @PaymentsTotal = ISNULL(SUM(Amount), 0)
        FROM @Payments

        IF @Adjustment = 0
        BEGIN
            IF EXISTS (SELECT 1 FROM @Payments)
                RAISERROR(N'لا يجب إرسال دفعات عندما لا يوجد فرق مطلوب تسويته.', 16, 1)
        END
        ELSE
        BEGIN
            IF NOT EXISTS (SELECT 1 FROM @Payments)
                RAISERROR(N'يجب إدخال طريقة دفع واحدة على الأقل لتسوية الفرق.', 16, 1)

            IF EXISTS
            (
                SELECT 1
                FROM @Payments
                WHERE PaymentMethodID <= 0
                   OR TreasuryID <= 0
                   OR Amount <= 0
            )
                RAISERROR(N'توجد بيانات غير صحيحة في توزيعات التسوية.', 16, 1)

            IF @PaymentsTotal <> @RequiredAmount
                RAISERROR(N'مجموع توزيعات التسوية لا يساوي فرق الفاتورة المطلوب.', 16, 1)

            IF EXISTS
            (
                SELECT 1
                FROM @Payments payment
                WHERE NOT EXISTS
                (
                    SELECT 1
                    FROM [dbo].[PAYMENT_METHOD] method
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
                    FROM [dbo].[PaymentMethodDefaultAccounts] account
                    WHERE account.PaymentMethodID = payment.PaymentMethodID
                      AND ISNULL(account.IsActive, 0) = 1
                )
            )
                RAISERROR(N'توجد طريقة دفع غير مفعلة.', 16, 1)

            IF EXISTS
            (
                SELECT 1
                FROM @Payments payment
                INNER JOIN [dbo].[PaymentMethodDefaultAccounts] account
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
                    FROM [dbo].[TreasuryCard] treasury
                    WHERE treasury.Tr_ID = payment.TreasuryID
                )
            )
                RAISERROR(N'توجد خزينة غير معرفة في توزيعات التسوية.', 16, 1)

            /* لا يسمح برد مبلغ أكبر من صافي المقبوض سابقًا لنفس الطريقة والخزينة. */
            IF @Adjustment < 0 AND EXISTS
            (
                SELECT 1
                FROM
                (
                    SELECT PaymentMethodID, TreasuryID, SUM(Amount) AS RequestedRefund
                    FROM @Payments
                    GROUP BY PaymentMethodID, TreasuryID
                ) requested
                OUTER APPLY
                (
                    SELECT ISNULL(SUM(
                        CASE
                            WHEN receipt.BsType_ID = 3 THEN ISNULL(receipt.Pure, 0)
                            WHEN receipt.BsType_ID = 4 THEN -ISNULL(receipt.Pure, 0)
                            ELSE 0
                        END), 0) AS RefundableAmount
                    FROM [dbo].[Agents_Balance_MV_RCT] receipt
                    WHERE receipt.Receipt_Tran_ID = @T_ID
                      AND receipt.Pay_ID = requested.PaymentMethodID
                      AND receipt.Tr_ID = requested.TreasuryID
                      AND receipt.BsType_ID IN (3, 4)
                      AND ISNULL(receipt.isVoid, 0) = 0
                ) available
                WHERE requested.RequestedRefund > available.RefundableAmount
            )
                RAISERROR(N'قيمة الاسترداد تتجاوز الرصيد القابل للرد لطريقة دفع أو خزينة محددة.', 16, 1)

            SET @ReceiptTypeID = CASE WHEN @Adjustment > 0 THEN 3 ELSE 4 END
            SET @ReceiptTitle = CASE
                WHEN @Adjustment > 0 THEN N'تحصيل فرق تعديل فاتورة مبيعات : ' + ISNULL(@SB_ID, N'')
                ELSE N'استرداد فرق تعديل فاتورة مبيعات : ' + ISNULL(@SB_ID, N'')
            END
            SET @Date = GETDATE()

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
                SET @CreatedReceiptID = 0
                SET @CreatedReceiptNumber = 0

                EXEC [dbo].[Agents_BalanceMV_insert_RCT]
                    @T_ID = @CreatedReceiptID OUTPUT,
                    @Date = @Date,
                    @Receipt_Num = @CreatedReceiptNumber OUTPUT,
                    @AG_ID = @AG_ID,
                    @Pr_ID = @Pr_ID,
                    @Receipt_Tran_ID = @T_ID,
                    @ReferNum = @ReferenceNumber,
                    @Receipt_Title = @ReceiptTitle,
                    @Pure = @PaymentAmount,
                    @About = @Notes,
                    @BsType_ID = @ReceiptTypeID,
                    @User_ID = @User_ID,
                    @Tr_ID = @TreasuryID,
                    @Bank_Name = @BankName,
                    @CheckNum = @CheckNumber,
                    @Pay_ID = @PaymentMethodID

                IF @CreatedReceiptID <= 0
                   OR NOT EXISTS
                   (
                       SELECT 1
                       FROM [dbo].[Agents_Balance_MV_RCT]
                       WHERE T_ID = @CreatedReceiptID
                         AND Receipt_Tran_ID = @T_ID
                         AND BsType_ID = @ReceiptTypeID
                         AND Pure = @PaymentAmount
                   )
                   OR NOT EXISTS
                   (
                       SELECT 1
                       FROM [dbo].[Treasury_Balance_MV]
                       WHERE AGBalance_T_ID = @CreatedReceiptID
                         AND Tr_ID = @TreasuryID
                         AND Pure = @PaymentAmount
                         AND ISNULL(isVoid, 0) = 0
                   )
                    RAISERROR(N'فشل إنشاء سند أو حركة خزينة لتسوية الفاتورة.', 16, 1)

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

        SELECT @FinalNetPaid = ISNULL(SUM(
            CASE
                WHEN BsType_ID = 3 THEN ISNULL(Pure, 0)
                WHEN BsType_ID = 4 THEN -ISNULL(Pure, 0)
                ELSE 0
            END), 0)
        FROM [dbo].[Agents_Balance_MV_RCT]
        WHERE Receipt_Tran_ID = @T_ID
          AND BsType_ID IN (3, 4)
          AND ISNULL(isVoid, 0) = 0

        IF @FinalNetPaid <> @NewPure
            RAISERROR(N'لم ينتج عن التسوية تطابق صافي المدفوع مع صافي الفاتورة.', 16, 1)

        COMMIT TRANSACTION

        SELECT
            CAST(1 AS BIT) AS IsSuccess,
            @T_ID AS T_ID,
            @ExpectedOriginalPure AS OriginalPure,
            @NewPure AS NewPure,
            @Adjustment AS Adjustment,
            @FinalNetPaid AS FinalNetPaid
    END TRY
    BEGIN CATCH
        IF CURSOR_STATUS('local', 'PaymentCursor') >= 0
            CLOSE PaymentCursor
        IF CURSOR_STATUS('local', 'PaymentCursor') >= -1
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
