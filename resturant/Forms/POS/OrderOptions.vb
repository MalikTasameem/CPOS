Public Class OrderOptions

    Dim rs As New Resizer

    Private Sub OrderOptions_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        F_POS.Fetch_IM()
        Me.Dispose()
    End Sub

    Private Sub DeleverOption_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)

        If F_POS.AG_ID = Default_AG_ID Or (F_POS.AG_ID <> Default_AG_ID And F_POS.AG_TypeID = 5) Then
            Piedmoney_txt.Text = F_POS.PureTextBox.Text
            Piedmoney_txt.Enabled = False
            If F_POS.AG_TypeID = 5 Then
                Point_Panel.Visible = True
                If F_POS.Point < 0 Then
                    Point_Panel.Enabled = True
                End If
                Point_LB.Text = " النقاط  : " + (F_POS.Point * -1).ToString
            End If
        Else
            Piedmoney_txt.Select()
            Piedmoney_txt.Focus()
        End If

        If F_POS.isDierct_Reseve = True Then
            NoneDate_CB.Checked = True
            DeliverDate_Panel.Visible = False
        Else
            Barcode_txt.Text = F_POS.Barcode
            DeliverDate_Panel.Visible = True
            NoneDate_CB.Checked = My_Settings.Order_No_Deliver_Date
        End If


    End Sub

    Private Sub DeleverOption_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub Up_Btn_Click(sender As Object, e As EventArgs) Handles Up_Btn.Click
        Delever_Date.Text = Delever_Date.Value.AddDays(1)
    End Sub

    Private Sub Down_Btn_Click(sender As Object, e As EventArgs) Handles Down_Btn.Click
        Delever_Date.Text = Delever_Date.Value.AddDays(-1)
    End Sub


    Private Sub Bar_CB_CheckedChanged(sender As Object, e As EventArgs) Handles NoneDate_CB.CheckedChanged
        CB_CHecked(sender)
        My_Settings.Order_No_Deliver_Date = NoneDate_CB.Checked
        Save_AppSetting()
    End Sub

    Private Sub Piedmoney_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Piedmoney_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Piedmoney_txt_TextChanged(sender As Object, e As EventArgs) Handles Piedmoney_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
        If Piedmoney_txt.Text.Count = 0 Then
            Rest_txt.Text = F_POS.PureTextBox.Text
        Else
            Rest_txt.Text = (Convert.ToDouble(F_POS.PureTextBox.Text) - Convert.ToDouble(Piedmoney_txt.Text)).ToString("n")
        End If
    End Sub

    Private Sub Back_Btn_Click(sender As Object, e As EventArgs) Handles Back_Btn.Click
        Me.Close()
    End Sub

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        If String.IsNullOrWhiteSpace(Piedmoney_txt.Text) Then Piedmoney_txt.Text = "0"

        If Convert.ToDouble(Rest_txt.Text) < 0 Then
            ' MsgBox("المبلغ المدفوع أكبر من تكلفة الفاتورة", MsgBoxStyle.Critical, "خطأ")
            Beep()
            Dim MSG As New OK
            MSG.Msg_Lb.Text = "المبلغ المدفوع أكبر من تكلفة الفاتورة"
            MSG.ShowDialog()
            Exit Sub
        End If

        If F_POS.MetroGrid.Rows.Count > 0 Then
            Me.Cursor = Cursors.AppStarting

            If Not String.IsNullOrWhiteSpace(Piedmoney_txt.Text) Then F_POS.isDierct_Reseve = False

            F_POS.ConfermBill()
            'If SB_AutoPrint = True Then F_POS.Print_Bill()
            F_POS.ResetNewBill()
                Me.Close()
                Me.Cursor = Cursors.Default
            End If

    End Sub

    Private Sub Barcode_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Barcode_CB.CheckedChanged
        Barcode_Check()
    End Sub

    Private Sub Barcode_Check()
        If Barcode_CB.Checked = True And isPhone_User = False Then
            Barcode_txt.Visible = True
            Barcode_CB.Checked = True
            UsePoint_btn.BackColor = Color.Goldenrod
            Barcode_txt.Select()
            Dim Point = (F_POS.Point * -1)
            If Point >= Convert.ToDouble(F_POS.PureTextBox.Text) Then
                Piedmoney_txt.Text = "0"
            Else
                Piedmoney_txt.Text = Convert.ToDouble(F_POS.PureTextBox.Text) - Point
            End If

        Else
            Piedmoney_txt.Text = F_POS.PureTextBox.Text
            Barcode_txt.Visible = False
            Barcode_CB.Checked = False
            UsePoint_btn.BackColor = Color.White
        End If
    End Sub

    Private Sub Barcode_btn_Click(sender As Object, e As EventArgs) Handles UsePoint_btn.Click
        If Barcode_CB.Checked = True Then
            Barcode_CB.Checked = False
        Else
            Barcode_CB.Checked = True
        End If
        Barcode_Check()
    End Sub


    Private Sub B1_Click(sender As Object, e As EventArgs) Handles B1.Click
        Test_Num(sender)
    End Sub

    Private Sub Test_Num(Btn As Button)
        If Piedmoney_txt.Enabled = True Then Piedmoney_txt.Text += Btn.Tag
    End Sub

    Private Sub B2_Click(sender As Object, e As EventArgs) Handles B2.Click
        Test_Num(sender)
    End Sub

    Private Sub B3_Click(sender As Object, e As EventArgs) Handles B3.Click
        Test_Num(sender)
    End Sub

    Private Sub B4_Click(sender As Object, e As EventArgs) Handles B4.Click
        Test_Num(sender)
    End Sub

    Private Sub B5_Click(sender As Object, e As EventArgs) Handles B5.Click
        Test_Num(sender)
    End Sub

    Private Sub B6_Click(sender As Object, e As EventArgs) Handles B6.Click
        Test_Num(sender)
    End Sub

    Private Sub B7_Click(sender As Object, e As EventArgs) Handles B7.Click
        Test_Num(sender)
    End Sub

    Private Sub B8_Click(sender As Object, e As EventArgs) Handles B8.Click
        Test_Num(sender)
    End Sub

    Private Sub B9_Click(sender As Object, e As EventArgs) Handles B9.Click
        Test_Num(sender)
    End Sub

    Private Sub B0_Click(sender As Object, e As EventArgs) Handles B0.Click
        Test_Num(sender)
    End Sub

    Private Sub BDot_Click(sender As Object, e As EventArgs) Handles BDot.Click

        If F_POS.AG_ID <> Default_AG_ID Then
            If Piedmoney_txt.Text.Contains(".") = False Then Piedmoney_txt.Text += "."
        End If


    End Sub

    Private Sub ClearNumBtn_Click(sender As Object, e As EventArgs) Handles ClearNumBtn.Click
        Piedmoney_txt.Clear()
    End Sub
End Class