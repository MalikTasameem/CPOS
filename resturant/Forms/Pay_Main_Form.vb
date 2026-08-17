Public Class Pay_Main_Form

    Public Tr_ID, Pay_ID, Temp_Tr_ID, AG_ID As Integer
    Public is_OK As Boolean = False
    Public MONEY_VALUE As Decimal = 0
    Public Is_Force_Pay As Boolean = False
    Public PaymentName As String = "نقدا"
    Public EnableMultiplePayments As Boolean = False
    Private ReadOnly PaymentAllocationsValue As New List(Of SalePaymentAllocation)

    Public ReadOnly Property Payments As List(Of SalePaymentAllocation)
        Get
            Return New List(Of SalePaymentAllocation)(PaymentAllocationsValue)
        End Get
    End Property


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles OK_Btn.Click
        If PaymentAllocationsValue.Count = 0 Then AddCurrentPayment(MONEY_VALUE)
        If Not ValidatePaymentsTotal() Then Exit Sub

        Tr_ID = Pay_Method1.TR_ID
        Pay_ID = Pay_Method1.Pay_ID
        If PaymentAllocationsValue.Count = 1 Then
            Tr_ID = PaymentAllocationsValue(0).TreasuryID
            Pay_ID = PaymentAllocationsValue(0).PaymentMethodID
            PaymentName = PaymentAllocationsValue(0).PaymentName
        Else
            PaymentName = String.Join(" + ", PaymentAllocationsValue.Select(Function(payment) payment.PaymentName).Distinct())
        End If
        is_OK = True
        Me.Close()
    End Sub

    Private Sub Pay_Main_Form_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F12 Then Button1_Click(sender, e)
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub



    Private Sub Pay_Main_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigureMultiplePaymentsView()
        Pay_Method1.Set_Tr_Form()
        Pay_Method1.Load_Tr()
        MONEY_VALUE_Txt.Text = MONEY_VALUE
        PaymentAmountTxt.Text = MONEY_VALUE.ToString("N3")
        RefreshPaymentsSummary()

        If (Pay_Method1.PaymentMethodsCount = 1 Or AG_ID <> Default_AG_ID) Then

            If Is_Force_Pay = False Then
                Tr_ID = Temp_Tr_ID
                Pay_ID = 1
                PaymentName = Pay_Method1.SelectedPaymentName
                AddCurrentPayment(MONEY_VALUE)
                is_OK = True
                Me.Close()
            End If

        End If

        'If Pay_Method1.lbPayTypes.Items.Count = 1 Or AG_ID <> Default_AG_ID Then
        '    Tr_ID = Temp_Tr_ID
        '    Pay_ID = 1
        '    is_OK = True
        '    Me.Close()
        'End If
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        is_OK = False
        Me.Close()
    End Sub

    Private Sub ConfigureMultiplePaymentsView()
        PaymentsPanel.Visible = EnableMultiplePayments

        If EnableMultiplePayments Then
            Me.ClientSize = New Size(1012, 680)
            OK_Btn.Width = 1008
            ExitFormButton.Width = 1008
        Else
            Me.ClientSize = New Size(632, 680)
            OK_Btn.Width = 628
            ExitFormButton.Width = 628
        End If
    End Sub

    Private Sub AddPaymentBtn_Click(sender As Object, e As EventArgs) Handles AddPaymentBtn.Click
        Dim amount As Decimal
        If Not Decimal.TryParse(PaymentAmountTxt.Text, amount) OrElse amount <= 0D Then
            MessageBox.Show("أدخل مبلغ دفع صحيح أكبر من صفر.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            PaymentAmountTxt.Focus()
            Return
        End If

        Dim remaining As Decimal = MONEY_VALUE - PaymentAllocationsValue.Sum(Function(payment) payment.Amount)
        If amount > remaining Then
            MessageBox.Show("مبلغ الدفعة أكبر من المبلغ المتبقي.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        AddCurrentPayment(amount)
        RefreshPaymentsGrid()
    End Sub

    Private Sub RemovePaymentBtn_Click(sender As Object, e As EventArgs) Handles RemovePaymentBtn.Click
        If PaymentsGrid.CurrentRow Is Nothing Then Return

        Dim rowIndex As Integer = PaymentsGrid.CurrentRow.Index
        If rowIndex < 0 OrElse rowIndex >= PaymentAllocationsValue.Count Then Return

        PaymentAllocationsValue.RemoveAt(rowIndex)
        RefreshPaymentsGrid()
    End Sub

    Private Sub AddCurrentPayment(amount As Decimal)
        Dim existingPayment As SalePaymentAllocation = PaymentAllocationsValue.FirstOrDefault(
            Function(payment) payment.PaymentMethodID = Pay_Method1.Pay_ID AndAlso
                payment.TreasuryID = Pay_Method1.TR_ID)

        If existingPayment IsNot Nothing Then
            existingPayment.Amount += amount
            Return
        End If

        PaymentAllocationsValue.Add(New SalePaymentAllocation With {
            .PaymentMethodID = Pay_Method1.Pay_ID,
            .TreasuryID = Pay_Method1.TR_ID,
            .Amount = amount,
            .PaymentName = Pay_Method1.SelectedPaymentName,
            .TreasuryName = Pay_Method1.SelectedTreasuryName
        })
    End Sub

    Private Sub RefreshPaymentsGrid()
        PaymentsGrid.Rows.Clear()

        For Each payment As SalePaymentAllocation In PaymentAllocationsValue
            PaymentsGrid.Rows.Add(payment.PaymentName, payment.TreasuryName, payment.Amount.ToString("N3"))
        Next

        RefreshPaymentsSummary()
    End Sub

    Private Sub RefreshPaymentsSummary()
        Dim paid As Decimal = PaymentAllocationsValue.Sum(Function(payment) payment.Amount)
        Dim remaining As Decimal = MONEY_VALUE - paid

        PaidValueLbl.Text = paid.ToString("N3")
        RemainingValueLbl.Text = remaining.ToString("N3")
        RemainingValueLbl.ForeColor = If(remaining = 0D, Color.ForestGreen, Color.Firebrick)

        If remaining > 0D Then PaymentAmountTxt.Text = remaining.ToString("N3")
    End Sub

    Private Function ValidatePaymentsTotal() As Boolean
        Dim paid As Decimal = PaymentAllocationsValue.Sum(Function(payment) payment.Amount)
        If paid <> MONEY_VALUE Then
            MessageBox.Show("يجب أن يساوي مجموع الدفعات قيمة الفاتورة." & vbCrLf &
                            "المتبقي: " & (MONEY_VALUE - paid).ToString("N3"),
                            "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function
End Class
