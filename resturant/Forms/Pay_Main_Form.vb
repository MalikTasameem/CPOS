Public Class Pay_Main_Form

    Public Tr_ID, Pay_ID, Temp_Tr_ID, AG_ID As Integer
    Public is_OK As Boolean = False
    Public MONEY_VALUE As Decimal = 0


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles OK_Btn.Click
        Tr_ID = Pay_Method1.TR_ID
        Pay_ID = Pay_Method1.Pay_ID
        is_OK = True
        Me.Close()
    End Sub

    Private Sub Pay_Main_Form_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F12 Then Button1_Click(sender, e)
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub Pay_Main_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Pay_Method1.Set_Tr_Form()
        Pay_Method1.Load_Tr()
        MONEY_VALUE_Txt.Text = MONEY_VALUE
        If Pay_Method1.PaymentMethodsCount = 1 Or AG_ID <> Default_AG_ID Then
            Tr_ID = Temp_Tr_ID
            Pay_ID = 1
            is_OK = True
            Me.Close()
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
End Class