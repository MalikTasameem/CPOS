Public Class SalesPaymentReconciliationForm

    Private ReadOnly SnapshotValue As SalesBillPaymentSnapshot
    Private ReadOnly NewPureValue As Decimal
    Private ReadOnly PaymentAllocations As New List(Of SalePaymentAllocation)

    Public ReadOnly Property IsApproved As Boolean
    Public ReadOnly Property Payments As IEnumerable(Of SalePaymentAllocation)
        Get
            Return PaymentAllocations
        End Get
    End Property

    Public Sub New(snapshot As SalesBillPaymentSnapshot, newPure As Decimal)
        If snapshot Is Nothing Then Throw New ArgumentNullException("snapshot")
        InitializeComponent()
        SnapshotValue = snapshot
        NewPureValue = newPure
    End Sub

    Private Sub SalesPaymentReconciliationForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BillNumberValueLabel.Text = If(String.IsNullOrWhiteSpace(SnapshotValue.BillNumber), "---", SnapshotValue.BillNumber)
        OriginalPureValueLabel.Text = SnapshotValue.OriginalPure.ToString(N_Point_Fter)
        NewPureValueLabel.Text = NewPureValue.ToString(N_Point_Fter)
        PaidValueLabel.Text = SnapshotValue.NetPaidTotal.ToString(N_Point_Fter)

        Dim adjustment As Decimal = NewPureValue - SnapshotValue.NetPaidTotal
        AdjustmentValueLabel.Text = Math.Abs(adjustment).ToString(N_Point_Fter)
        AdjustmentTitleLabel.Text = If(adjustment > 0D, "المطلوب تحصيله", If(adjustment < 0D, "المطلوب استرداده", "لا يوجد فرق"))
        AdjustmentValueLabel.ForeColor = If(adjustment < 0D, Color.FromArgb(185, 28, 28), Color.FromArgb(21, 128, 61))

        OriginalPaymentsGrid.DataSource = SnapshotValue.Payments.Select(
            Function(payment) New With {
                Key .PaymentName = payment.PaymentName,
                Key .TreasuryName = payment.TreasuryName,
                Key .Amount = payment.SignedAmount
            }).ToList()

        SelectPaymentsButton.Enabled = adjustment <> 0D
        ConfirmButton.Enabled = adjustment = 0D
        If adjustment = 0D Then SelectionStatusLabel.Text = "الدفعات متطابقة ولا تحتاج إلى حركة جديدة."
    End Sub

    Private Sub SelectPaymentsButton_Click(sender As Object, e As EventArgs) Handles SelectPaymentsButton.Click
        Dim adjustment As Decimal = NewPureValue - SnapshotValue.NetPaidTotal
        If adjustment = 0D Then Exit Sub

        Using paymentForm As New Pay_Main_Form()
            paymentForm.MONEY_VALUE = Math.Abs(adjustment)
            paymentForm.Temp_Tr_ID = SB_TR_ID
            paymentForm.AG_ID = SnapshotValue.AgentID
            paymentForm.EnableMultiplePayments = True
            paymentForm.Is_Force_Pay = True
            paymentForm.Text = If(adjustment > 0D, "تحصيل فرق تعديل الفاتورة", "تحديد طرق استرداد فرق الفاتورة")
            paymentForm.ShowDialog(Me)

            If Not paymentForm.is_OK Then Exit Sub

            PaymentAllocations.Clear()
            For Each payment As SalePaymentAllocation In paymentForm.Payments
                PaymentAllocations.Add(New SalePaymentAllocation With {
                    .PaymentMethodID = payment.PaymentMethodID,
                    .TreasuryID = payment.TreasuryID,
                    .Amount = payment.Amount,
                    .PaymentName = payment.PaymentName,
                    .TreasuryName = payment.TreasuryName,
                    .ReferenceNumber = payment.ReferenceNumber,
                    .Notes = payment.Notes
                })
            Next

            SelectedPaymentsGrid.DataSource = PaymentAllocations.Select(
                Function(payment) New With {
                    Key .PaymentName = payment.PaymentName,
                    Key .TreasuryName = payment.TreasuryName,
                    Key .Amount = payment.Amount
                }).ToList()
            SelectionStatusLabel.Text = If(adjustment > 0D, "تم تحديد طرق تحصيل الفرق.", "تم تحديد طرق استرداد الفرق.")
            ConfirmButton.Enabled = PaymentAllocations.Count > 0
        End Using
    End Sub

    Private Sub ConfirmButton_Click(sender As Object, e As EventArgs) Handles ConfirmButton.Click
        _IsApproved = True
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub CancelButtonControl_Click(sender As Object, e As EventArgs) Handles CancelButtonControl.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

End Class
