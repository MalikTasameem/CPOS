Public Class FrmExchangeApprovePreview

    Public Property Confirmed As Boolean = False

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Confirmed = True
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Confirmed = False
        Me.Close()
    End Sub

End Class
