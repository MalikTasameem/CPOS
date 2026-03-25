Public Class Percent_Disc
    Public TOTAL As Double = 0
    Public T_ID As Integer
    Public Result As Double = 0
    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub UpdateGBButton_Click(sender As Object, e As EventArgs) Handles UpdateGBButton.Click
        If Not String.IsNullOrWhiteSpace(Discount_txt.Text) Then
            Result = (TOTAL * (Discount_txt.Text / 100))
            Update_Discount(T_ID, (TOTAL * (Discount_txt.Text / 100)))

            'F_Sales.Discount_txt.Text = (TOTAL * (Discount_txt.Text / 100))
            'F_Sales.Discount_calc()

            Me.Close()
        End If
    End Sub

    Private Sub Percent_Disc_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Return Then UpdateGBButton_Click(sender, e)
    End Sub

    Private Sub Discount_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Discount_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Discount_txt_TextChanged(sender As Object, e As EventArgs) Handles Discount_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Discount_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Discount_txt.KeyDown
        If e.KeyCode = Keys.Return Then UpdateGBButton_Click(sender, e)
    End Sub

    Private Sub Percent_Disc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Discount_txt.Select()
        Result = 0
    End Sub
End Class