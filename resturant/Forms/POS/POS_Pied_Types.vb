Public Class POS_Pied_Types
    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckConnection(MY_Settings.Online_Con_Str) = False Then
            MsgBox("تحقق من إعدادات الإتصال بخادم NFC", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim F As New PAY_BY_NFC
        F.Total = F_POS.PureTextBox.Text
        F.Bill_num = F_POS.SB_ID_Txt.Text
        ' F_POS.Pay_ID = 4
        F.ShowDialog()

        Me.Close()

    End Sub

    Private Sub Pay_Btn_Click(sender As Object, e As EventArgs) Handles Pay_Btn.Click

        Beep()
        Dim MSG As New YesNo
        MSG.Msg_Lb.Text = " دفع الفاتــورة نقدي ؟  "
        MSG.ShowDialog()
        If MSG.Result = True Then
            F_POS.PIED_OK = True
            ' F_POS.Pay_ID = 1
            Me.Close()
        End If



    End Sub

    Private Sub Credit_Pay_Btn_Click(sender As Object, e As EventArgs) Handles Credit_Pay_Btn.Click
        Beep()
        Dim MSG As New YesNo
        MSG.Msg_Lb.Text = " دفع بالبطاقة المصرفية ؟  "
        MSG.ShowDialog()
        If MSG.Result = True Then
            F_POS.PIED_OK = True
            ' F_POS.Pay_ID = 3
            Me.Close()
        End If
    End Sub
End Class