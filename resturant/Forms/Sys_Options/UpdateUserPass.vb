Imports System.Data.SqlClient

Public Class UpdateUserPass

    '  Public is_Change_PASS_Missed As Boolean = False
    Public ID_u As Integer
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub UpdateGBButton_Click(sender As Object, e As EventArgs) Handles UpdateGBButton.Click

        If String.IsNullOrWhiteSpace(OldPassTextBox.Text) Then
            MsgBox("أدخل كلمة المرور السابقه", MsgBoxStyle.Exclamation, "خطأ في التعديل")
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(NewPassTextBox.Text) Then
            MsgBox("أدخل كلمة المرور الجديده", MsgBoxStyle.Exclamation, "خطأ في التعديل")
            Exit Sub
        End If

        If F_users.PassUserTextBox.Text = OldPassTextBox.Text Then
            '----------------------------------------------------------
            If USER_check_pass() = True Then
                MsgBox("كلمة المرور مسجلة من قبل ... الرجاء اختيار كلمة أخرى", MsgBoxStyle.Critical)
                Exit Sub
            Else
                User_Update_Pass()
            End If
            '----------------------------------------------------------
            Me.Close()
        Else
            MsgBox("كلمة المرور السابقة خاطئة", MsgBoxStyle.Critical, "خطأ في التعديل")
        End If


    End Sub

    Public Function USER_check_pass()

        Dim c1 As New C
        Dim f As Boolean = False
        Dim sql As String = "select * from users where UserPass = '" & EncryptionHelper.Encrypt(NewPassTextBox.Text) & "' and user_id not in('" & ID_u & "')"
        Dim com As New SqlCommand(sql, c1.Con)
        Dim dr As SqlDataReader
        c1.Con.Open()
        Try
            dr = com.ExecuteReader
            dr.Read()
            If dr.HasRows = True Then f = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()

        Return f
    End Function

    Public Sub User_Update_Pass()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "User_UpdatePass"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@user_id", ID_u)
            .Parameters.AddWithValue("@NewPass", EncryptionHelper.Encrypt(NewPassTextBox.Text))
            .Parameters.AddWithValue("@WEB_PASS", NewPassTextBox.Text)
        End With
        If SQL_SP_EXEC(c.Com) Then MsgBox("تم تغيير كلمة المرور", MsgBoxStyle.Information)

    End Sub

    Private Sub UpdateGB_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub UpdateGB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        OldPassTextBox.Select()

        'If is_Change_PASS_Missed = True Then
        '    USER_Reader_in_Text()
        '    ID_u = 1
        'Else
        ID_u = F_users.ID_u
        ' End If


    End Sub

    Private Sub OldPassTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles OldPassTextBox.KeyDown
        If e.KeyCode = Keys.Return Then
            NewPassTextBox.Select()
        End If
    End Sub
    Private Sub ShowPassButton_MouseHover(sender As Object, e As EventArgs) Handles ShowPassButton.MouseHover
        OldPassTextBox.UseSystemPasswordChar = False
        NewPassTextBox.UseSystemPasswordChar = False
    End Sub

    Private Sub ShowPassButton_MouseLeave(sender As Object, e As EventArgs) Handles ShowPassButton.MouseLeave
        OldPassTextBox.UseSystemPasswordChar = True
        NewPassTextBox.UseSystemPasswordChar = True
    End Sub


    Private Sub USER_Reader_in_Text()
        Dim c As New C
        c.Str = "select UserPass from Users where user_id =  1 "
        c.Com = New SqlCommand(c.Str, c.Con)

        c.Con.Open()
        c.Dr = c.Com.ExecuteReader
        c.Dr.Read()
        OldPassTextBox.Text = c.Dr("UserPass")
        c.Con.Close()
    End Sub


End Class