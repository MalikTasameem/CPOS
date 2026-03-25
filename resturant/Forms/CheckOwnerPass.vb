Imports System.Data.SqlClient
Public Class CheckOwnerPass

    Private Sub ConfirmPassButtonX_Click(sender As Object, e As EventArgs) Handles ConfirmPassButtonX.Click

        Dim c1 As New C
        Dim str As String = "Select *  From Users Where UserPass= '" & EncryptionHelper.Encrypt(PassTextBox.Text) & "' and Valid_ID= 1 "
        Dim com As New SqlCommand(str, c1.Con)
        Dim dr As SqlDataReader
        c1.Con.Open()
        Try
            dr = com.ExecuteReader
            dr.Read()

            If dr.HasRows = True Then
                Delete_Data()
                Me.Close()
            Else
                MsgBox("خطأ في البيانات المدخلة", MsgBoxStyle.Critical, "خطأ")
                PassTextBox.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
    End Sub

    Public Sub Delete_Data()


        If F_SysDelete.Delete_ST_CB.Checked = True Then DeleteAll_STBalance()

        If F_SysDelete.DeleteAllBuyAndSellCheckBox.Checked = True Then DeleteAllBuyAndSell()

        If F_SysDelete.Delete_AGBalance_CB.Checked = True Then AG_Balance_Delete()

        If F_SysDelete.DeleteAllItemsCheckBox.Checked = True Then DeleteAll_Data()


    End Sub


    Private Sub CheckOwnerPass_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub CheckOwnerPass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PassTextBox.Select()
    End Sub

    Private Sub ShowLetterCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles ShowLetterCheckBox.CheckedChanged
        If ShowLetterCheckBox.Checked = True Then
            PassTextBox.PasswordChar = ""
            PassTextBox.Select()
        Else
            PassTextBox.PasswordChar = "#"
            PassTextBox.Select()
        End If
    End Sub

    Private Sub PassTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles PassTextBox.KeyDown
        If e.KeyCode = Keys.Return Then
            ConfirmPassButtonX_Click(sender, e)
        End If
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Call_KeyBoard()
    End Sub
End Class