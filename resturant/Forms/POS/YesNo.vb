Public Class YesNo
    Public Result As Boolean = False
    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Result = False
        Me.Close()
    End Sub


    Private Sub No_Btn_Click(sender As Object, e As EventArgs) Handles No_Btn.Click
        Result = False
        Me.Close()
    End Sub

    Private Sub Yes_Btn_Click(sender As Object, e As EventArgs) Handles Yes_Btn.Click
        Result = True
        Me.Close()
    End Sub

    Private Sub YesNo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class