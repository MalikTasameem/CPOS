Imports System.Data.SqlClient
Public Class Saleslogin

    Dim aa As Integer ' login 
    Dim isUserLoggin As Boolean = False

    Private Sub LoginButton_Click(sender As Object, e As EventArgs) Handles LoginButton.Click
        Dim c1 As New C
        'Dim pass As String = ""
        'Dim count As Integer = 0
        'For i = 0 To passTxt.Text.Count - 1
        '    If count = 7 Then
        '        Exit For
        '    End If
        '    pass = pass + passTxt.Text(i)
        '    count += 1
        'Next

        If passTxt.Text = "" Then
            MsgBox("أدخل كلمة المرور", MsgBoxStyle.Exclamation, "حقل إجباري")
            passTxt.Focus()
            Exit Sub
        End If

        Dim str As String = "Select *  From Users Where UserPass= '" & EncryptionHelper.Encrypt(passTxt.Text) & "'  and user_id between " & START_ID & " and " & END_ID
        Dim com As New SqlCommand(str, c1.Con)
        Dim dr As SqlDataReader
        c1.Con.Open()
        Try
            dr = com.ExecuteReader
            dr.Read()

            If dr.HasRows = True Then

                Me.Cursor = Cursors.AppStarting
                USER_ID = dr("user_id")
                User_isAdmin = dr("isAdmin")
                USER_NAME = dr("UserName")

                If IsIdInRange(USER_ID) = False Then
                    MsgBox("هذا المستخدم لا يتبع الفرع", MsgBoxStyle.Critical, "")
                    Exit Sub
                End If

                If dr("is_Allow") = 0 Then
                    MsgBox("المعذرة .. أنت غير مسموح لك بالدخول للنظام", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

                Check_For_OpenPierod()
                If Not IsDBNull(dr("AG_ID")) Then U_AG_ID = dr("AG_ID")

                If S_Pr = True And isPr_Open = False Then
                    MsgBox("يجب عليك فتح وردية أولا لكي تستمر في شاشة البيع", MsgBoxStyle.Exclamation)
                Else

                    F_MainForm.Mange_EmpValid()
                    If User_POS = False Then
                        MsgBox("لا تتوفر صلاحية دخول لواجهة البيع لهذا المستخدم", MsgBoxStyle.Critical)
                        Exit Sub
                    End If

                    F_POS.POS_CHECK_USER_Valid()
                    F_POS.ResetNewBill()
                    F_POS.UnLockButton.BackgroundImage = My.Resources.ResourceManager.GetObject("UnLock")
                    F_POS.FormActive = True
                    F_POS.Enabled = True

                    isUserLoggin = True
                    Beep()
                    Me.Cursor = Cursors.Default
                    Me.Close()
                End If

            Else
                MsgBox("خطأ في البيانات المدخلة", MsgBoxStyle.Critical, "خطأ")
                passTxt.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()

    End Sub

    'Private Sub Enable_Form()
    '    For Each A As Control In POS.Controls
    '        A.Enabled = True
    '    Next
    'End Sub

    Private Sub ShowLetterCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles ShowLetterCheckBox.CheckedChanged
        If ShowLetterCheckBox.Checked = True Then
            passTxt.UseSystemPasswordChar = False
        Else
            passTxt.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        aa = 1
        passTxt.Text = passTxt.Text + aa.ToString()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        aa = 2
        passTxt.Text = passTxt.Text + aa.ToString()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        aa = 3
        passTxt.Text = passTxt.Text + aa.ToString()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        aa = 4
        passTxt.Text = passTxt.Text + aa.ToString()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        aa = 5
        passTxt.Text = passTxt.Text + aa.ToString()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        aa = 6
        passTxt.Text = passTxt.Text + aa.ToString()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        aa = 7
        passTxt.Text = passTxt.Text + aa.ToString()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        aa = 8
        passTxt.Text = passTxt.Text + aa.ToString()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        aa = 9
        passTxt.Text = passTxt.Text + aa.ToString()
    End Sub

    Private Sub Button0_Click(sender As Object, e As EventArgs) Handles Button0.Click
        aa = 0
        passTxt.Text = passTxt.Text + aa.ToString()
    End Sub

    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles BackButton.Click
        Dim shownum As String
        Dim mOpflag As Boolean
        If mOpflag = False And passTxt.Text <> "" Then
            shownum = passTxt.Text
            shownum = shownum.Remove(shownum.Length - 1, 1)
            passTxt.Text = shownum
        End If
    End Sub

    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        passTxt.Clear()
    End Sub

    'Private Sub Saleslogin_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

    'End Sub

    Private Sub Saleslogin_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Return Then LoginButton_Click(sender, e)
    End Sub

    Private Sub Saleslogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        passTxt.Clear()
        passTxt.Select()
        isUserLoggin = False
    End Sub

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        If isUserLoggin = False Then
            If MessageBox.Show(" سيتم الخروج من النظــام", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                Application.Exit()
            End If
        Else
            Me.Dispose()
        End If
    End Sub
End Class