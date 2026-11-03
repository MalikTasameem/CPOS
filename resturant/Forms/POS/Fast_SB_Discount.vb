Imports System.Data.SqlClient

Public Class Fast_SB_Discount

    'Public TOTAL, PURE As Double

    Public is_By_Draft As Boolean = False
    Public is_OK As Boolean = False
    Public AuditOnUpdate As Boolean = False
    Private _confirming As Boolean
    Private _billId As Integer
    Private _total As Double
    Private _initialDiscount As Double

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub


    ' Closing the dialog never writes or applies a discount. Only confirmation does.

    Private Sub Fast_SB_Discount_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub



    Private Sub UpdateGBButton_Click(sender As Object, e As EventArgs) Handles UpdateGBButton.Click
        If _confirming OrElse is_OK Then Return
        _confirming = True
        UpdateGBButton.Enabled = False
        Try
            Dim selectedTotal As Double
            Dim roundingText As String = Nothing
            If CheckBox1.Checked Then roundingText = TextBox1.Text
            If CheckBox2.Checked Then roundingText = TextBox2.Text
            If CheckBox3.Checked Then roundingText = TextBox3.Text
            If roundingText IsNot Nothing Then
                If Not Double.TryParse(roundingText, selectedTotal) OrElse Double.IsNaN(selectedTotal) OrElse Double.IsInfinity(selectedTotal) Then Throw New ArgumentException("قيمة التقريب غير صحيحة.")
                Discount_txt.Text = (_total - selectedTotal).ToString()
            End If
            Dim amount As Decimal = Convert.ToDecimal(_initialDiscount), percent As Decimal = 0D
            If Not String.IsNullOrWhiteSpace(Discount_txt.Text) AndAlso Not Decimal.TryParse(Discount_txt.Text, amount) Then Throw New ArgumentException("قيمة الخصم غير صحيحة.")
            If Not String.IsNullOrWhiteSpace(Percent_txt.Text) AndAlso Not Decimal.TryParse(Percent_txt.Text, percent) Then Throw New ArgumentException("نسبة الخصم غير صحيحة.")
            Dim resultDiscount = Convert.ToDouble(If(is_By_Draft, amount, Decimal.Round(amount, 3, MidpointRounding.AwayFromZero)))
            Dim resultPure = Convert.ToDouble(_total.ToString("00.00")) - Convert.ToDouble(resultDiscount.ToString("00.00"))
            If Not is_By_Draft Then
                If Not AuditDocumentFieldsUi.FastDiscount(Me, _billId, amount, percent, AuditOnUpdate) Then Return
            End If
            ' Publish the result only after a confirmed save (drafts remain local).
            Identifiers.Disc = resultDiscount
            Identifiers.Pure = resultPure
            is_OK = True
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "خصم الفاتورة", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            _confirming = False
            If Not IsDisposed Then UpdateGBButton.Enabled = True
        End Try
    End Sub


    Private Sub Fast_SB_Discount_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        is_OK = False
        _billId = Identifiers.T_ID
        _total = Identifiers.TOTAL
        _initialDiscount = Identifiers.Disc
        Discount_txt.Text = _initialDiscount
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
        Dim percent As Decimal
        If Decimal.TryParse(Percent_txt.Text, percent) Then Discount_txt.Text = (_total * (Convert.ToDouble(percent) / 100)).ToString()
    End Sub
End Class
