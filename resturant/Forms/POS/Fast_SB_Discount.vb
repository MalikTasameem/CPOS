Imports System.Data.SqlClient

Public Class Fast_SB_Discount

    'Public TOTAL, PURE As Double


    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub


    Private Sub UpdateGB_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        If Application.OpenForms().OfType(Of Sales_Fast).Any Then
            Sales_Fast.Total_TextBox.Text = Identifiers.TOTAL
            Sales_Fast.Discount_txt.Text = Identifiers.Disc
            Sales_Fast.Pure_txt.Text = Identifiers.Pure

            Sales_Fast.TOTAL = Identifiers.TOTAL
            Sales_Fast.Disc = Identifiers.Disc
            Sales_Fast.Pure = Identifiers.Pure

        End If

        If Application.OpenForms().OfType(Of POS).Any Then
            F_POS.TotalTextBox.Text = Identifiers.TOTAL
            F_POS.DiscountTextBox.Text = Identifiers.Disc
            F_POS.PureTextBox.Text = Identifiers.Pure

            F_POS.TOTAL = Identifiers.TOTAL
            F_POS.DISCOUNT = Identifiers.Disc
            F_POS.Pure = Identifiers.Pure


            F_SBChangePrice.NumTextBox.Text = Identifiers.Disc
            UpdateGBButton_Click(sender, e)
        End If

        Me.Dispose()
    End Sub

    Private Sub Fast_SB_Discount_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub



    Private Sub UpdateGBButton_Click(sender As Object, e As EventArgs) Handles UpdateGBButton.Click


        'If CheckBox1.Checked = True Then Discount_txt.Text = Sales_Fast.TOTAL - Convert.ToDouble(TextBox1.Text)
        'If CheckBox2.Checked = True Then Discount_txt.Text = Sales_Fast.TOTAL - Convert.ToDouble(TextBox2.Text)
        'If CheckBox3.Checked = True Then Discount_txt.Text = Sales_Fast.TOTAL - Convert.ToDouble(TextBox3.Text)


        'If Not String.IsNullOrWhiteSpace(Discount_txt.Text) Then
        '    Sales_Fast.Disc = Convert.ToDouble(Discount_txt.Text)
        '    Sales_Fast.Pure_txt.Text = (Sales_Fast.TOTAL).ToString("00.00") - (Sales_Fast.Disc).ToString("00.00")
        '    Sales_Fast.Pure = Sales_Fast.Pure_txt.Text
        'End If


        'Network_Edit_Tracker_insert(" تخفيض للفاتورة بقيمة:" & Discount_txt.Text, Sales_Fast.SB_ID, 1, 3)
        'Save_Total(Sales_Fast.T_ID, Sales_Fast.TOTAL, Sales_Fast.Disc)
        'Sales_Fast.Discount_txt.Text = Discount_txt.Text
        'Me.Close()
        '------------------------------------------------------------------------------------------------------------------------
        If CheckBox1.Checked = True Then Discount_txt.Text = Identifiers.TOTAL - Convert.ToDouble(TextBox1.Text)
        If CheckBox2.Checked = True Then Discount_txt.Text = Identifiers.TOTAL - Convert.ToDouble(TextBox2.Text)
        If CheckBox3.Checked = True Then Discount_txt.Text = Identifiers.TOTAL - Convert.ToDouble(TextBox3.Text)


        If Not String.IsNullOrWhiteSpace(Discount_txt.Text) Then
            Identifiers.Disc = Convert.ToDouble(Discount_txt.Text)
            Identifiers.Pure = (Identifiers.TOTAL).ToString("00.00") - (Identifiers.Disc).ToString("00.00")
            'Sales_Fast.Pure = Pure
        End If

        If String.IsNullOrWhiteSpace(Percent_txt.Text) Then Percent_txt.Text = 0

        Update_Discount(Identifiers.T_ID, Identifiers.Disc, Percent_txt.Text)
        Network_Edit_Tracker_insert(" تخفيض للفاتورة بقيمة:" & Discount_txt.Text, Identifiers.SB_ID, 1, 3)

        'Save_Total(Identifiers.T_ID, Identifiers.TOTAL, Identifiers.Disc)
        ' Discount_txt.Text = Discount_txt.Text
        Me.Close()
    End Sub


    Private Sub Fast_SB_Discount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Discount_txt.Text = Sales_Fast.Disc
        'Discount_txt.Focus()
        'Discount_txt.Select()


        'Select Case (Convert.ToDouble(Sales_Fast.Total_TextBox.Text) - Math.Floor(Convert.ToDouble(Sales_Fast.Total_TextBox.Text))).ToString("N")
        '    Case 0.01 To 0.24
        '        TextBox1.Text = Math.Floor(Convert.ToDouble(Sales_Fast.Total_TextBox.Text))
        '        TextBox2.Text = Math.Floor(Convert.ToDouble(Sales_Fast.Total_TextBox.Text)) & ".25"

        '    Case 0.26 To 0.49
        '        TextBox2.Text = Math.Floor(Convert.ToDouble(Sales_Fast.Total_TextBox.Text)) & ".25"
        '        TextBox1.Text = Math.Floor(Convert.ToDouble(Sales_Fast.Total_TextBox.Text)) & ".50"

        '    Case 0.51 To 0.74
        '        TextBox1.Text = Math.Floor(Convert.ToDouble(Sales_Fast.Total_TextBox.Text)) & ".50"
        '        TextBox2.Text = Math.Floor(Convert.ToDouble(Sales_Fast.Total_TextBox.Text)) & ".75"

        '    Case 0.76 To 0.99
        '        TextBox1.Text = Math.Floor(Convert.ToDouble(Sales_Fast.Total_TextBox.Text)) & ".75"
        '        TextBox2.Text = Math.Floor(Convert.ToDouble(Sales_Fast.Total_TextBox.Text)) + 1

        'End Select

        'TextBox3.Text = Math.Floor(Convert.ToDouble(Sales_Fast.Total_TextBox.Text))

        'CHECK_FIELDS()
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------

        Discount_txt.Text = Identifiers.Disc
        Discount_txt.Focus()
        Discount_txt.Select()


        Select Case (Convert.ToDouble(Identifiers.TOTAL) - Math.Floor(Convert.ToDouble(Identifiers.TOTAL))).ToString("N")
            Case 0.01 To 0.24
                TextBox1.Text = Math.Floor(Convert.ToDouble(Identifiers.TOTAL))
                TextBox2.Text = Math.Floor(Convert.ToDouble(Identifiers.TOTAL)) & ".25"

            Case 0.26 To 0.49
                TextBox2.Text = Math.Floor(Convert.ToDouble(Identifiers.TOTAL)) & ".25"
                TextBox1.Text = Math.Floor(Convert.ToDouble(Identifiers.TOTAL)) & ".50"

            Case 0.51 To 0.74
                TextBox1.Text = Math.Floor(Convert.ToDouble(Identifiers.TOTAL)) & ".50"
                TextBox2.Text = Math.Floor(Convert.ToDouble(Identifiers.TOTAL)) & ".75"

            Case 0.76 To 0.99
                TextBox1.Text = Math.Floor(Convert.ToDouble(Identifiers.TOTAL)) & ".75"
                TextBox2.Text = Math.Floor(Convert.ToDouble(Identifiers.TOTAL)) + 1

        End Select

        TextBox3.Text = Math.Floor(Convert.ToDouble(Identifiers.TOTAL))

        CHECK_FIELDS()
    End Sub

    Private Sub Discount_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Discount_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Discount_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Discount_txt.KeyDown
        If e.KeyCode = Keys.Return Then UpdateGBButton_Click(sender, e)
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox1.Checked = False
            CheckBox3.Checked = False
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Checked = False
            CheckBox3.Checked = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox1.Checked = True Then
            CheckBox1.Checked = False
        Else
            CheckBox1.Checked = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If CheckBox2.Checked = True Then
            CheckBox2.Checked = False
        Else
            CheckBox2.Checked = True
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox1.Checked = False
            CheckBox2.Checked = False
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If CheckBox3.Checked = True Then
            CheckBox3.Checked = False
        Else
            CheckBox3.Checked = True
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        CHECK_FIELDS()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        CHECK_FIELDS()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        CHECK_FIELDS()
    End Sub

    Private Sub CHECK_FIELDS()


        If TextBox1.Text.Count = 0 Then
            CheckBox1.Enabled = False
            Button1.Enabled = False
        Else
            CheckBox1.Enabled = True
            Button1.Enabled = True
        End If
        '------------------------------------
        If TextBox2.Text.Count = 0 Then
            CheckBox2.Enabled = False
            Button2.Enabled = False
        Else
            CheckBox2.Enabled = True
            Button2.Enabled = True
        End If
        '------------------------------------
        If TextBox3.Text.Count = 0 Then
            CheckBox3.Enabled = False
            Button3.Enabled = False
        Else
            CheckBox3.Enabled = True
            Button3.Enabled = True
        End If
    End Sub


    Private Sub Percent_txt_TextChanged(sender As Object, e As EventArgs) Handles Percent_txt.TextChanged
        If Not String.IsNullOrWhiteSpace(Percent_txt.Text) Then Discount_txt.Text = (TOTAL * (Percent_txt.Text / 100))
    End Sub
End Class