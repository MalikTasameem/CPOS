Public Class Percent_Disc
    Public TOTAL As Double = 0
    Public T_ID As Integer
    Public Result As Double = 0
    Public Confirmed As Boolean
    Private _saving As Boolean
    Public AuditOnUpdate As Boolean
    Public AuditDocumentType As Integer
    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub UpdateGBButton_Click(sender As Object, e As EventArgs) Handles UpdateGBButton.Click
        If _saving OrElse Confirmed OrElse String.IsNullOrWhiteSpace(Discount_txt.Text) Then Return
        _saving = True
        UpdateGBButton.Enabled = False
        Try
            Dim percent As Double
            If Not Double.TryParse(Discount_txt.Text, percent) OrElse Double.IsNaN(percent) OrElse Double.IsInfinity(percent) Then Throw New ArgumentException("نسبة الخصم غير صحيحة.")
            Dim requested As Double = TOTAL * (percent / 100)
            Dim savedValue = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(requested), 3, MidpointRounding.AwayFromZero))
            If Not AuditDocumentFieldsUi.Discount(Me, T_ID, AuditDocumentType, Convert.ToDecimal(requested), AuditOnUpdate) Then Return
            Result = savedValue
            Confirmed = True
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "خصم الفاتورة", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            _saving = False
            If Not IsDisposed Then UpdateGBButton.Enabled = True
        End Try
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
        Confirmed = False
    End Sub
End Class
