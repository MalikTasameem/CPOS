
Public Class SPLASH_FORM

    Private Sub SPLASH_FORM_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = My_Settings.App_Suuply
        'If My_Settings.App_Suuply = "RESAL" Then
        '    Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        '    Me.Size = New Size(400, 400)
        '    PictureBox1.Image = My.Resources.RESAL_SOFT1
        'End If

    End Sub

End Class