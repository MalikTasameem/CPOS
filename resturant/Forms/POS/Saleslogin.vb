Imports System.Data.SqlClient
Public Class Saleslogin

    Dim aa As Integer ' login 
    Dim isUserLoggin As Boolean = False
    Private loginInProgress As Boolean

    Private Sub LoginButton_Click(sender As Object, e As EventArgs) Handles LoginButton.Click
        If loginInProgress Then Return
        If passTxt.Text = "" Then
            MsgBox("أدخل كلمة المرور", MsgBoxStyle.Exclamation, "حقل إجباري")
            passTxt.Focus()
            Exit Sub
        End If

        loginInProgress = True
        LoginButton.Enabled = False
        Me.Cursor = Cursors.AppStarting
        Try
            Dim connection As String = MY_Settings.SqlConStr
            Dim usersTable As New DataTable
            Dim periods As New DataTable
            Dim canSell As Boolean = False
            Using cn As New SqlConnection(connection)
                cn.Open()
                Using cmd As New SqlCommand("SELECT user_id,UserName,isAdmin,is_Allow,AG_ID FROM Users WHERE UserPass=@Password AND user_id BETWEEN @FirstId AND @LastId", cn)
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar, -1).Value = EncryptionHelper.Encrypt(passTxt.Text)
                    cmd.Parameters.Add("@FirstId", SqlDbType.Int).Value = START_ID
                    cmd.Parameters.Add("@LastId", SqlDbType.Int).Value = END_ID
                    Using reader = cmd.ExecuteReader()
                        usersTable.Load(reader)
                    End Using
                End Using
                If usersTable.Rows.Count > 0 AndAlso Convert.ToBoolean(usersTable.Rows(0)("is_Allow")) Then
                    Dim candidateId As Integer = Convert.ToInt32(usersTable.Rows(0)("user_id"))
                    Using cmd As New SqlCommand("Check_For_OpenPierod", cn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.Add("@USER_ID", SqlDbType.Int).Value = candidateId
                        Using reader = cmd.ExecuteReader()
                            periods.Load(reader)
                        End Using
                    End Using
                    Using cmd As New SqlCommand("SELECT Sales FROM Users_Validations_V WHERE User_id=@UserId", cn)
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = candidateId
                        Dim permission = cmd.ExecuteScalar()
                        canSell = permission IsNot Nothing AndAlso Not IsDBNull(permission) AndAlso Convert.ToBoolean(permission)
                    End Using
                End If
            End Using

            If usersTable.Rows.Count > 0 Then
                Dim candidate As DataRow = usersTable.Rows(0)
                Dim candidateId As Integer = Convert.ToInt32(candidate("user_id"))
                Dim candidateName As String = Convert.ToString(candidate("UserName"))
                passTxt.Clear()
                If Not Convert.ToBoolean(candidate("is_Allow")) OrElse Not IsIdInRange(candidateId) Then
                    ShowAuditWarning(AuditSecuritySession.Current.RejectLogin(connection, candidateId, candidateName, "Saleslogin", Environment.MachineName, Application.ProductVersion))
                    MsgBox("المعذرة .. أنت غير مسموح لك بالدخول للنظام", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

                If S_Pr AndAlso periods.Rows.Count = 0 Then
                    ShowAuditWarning(AuditSecuritySession.Current.RejectLogin(connection, candidateId, candidateName, "Saleslogin", Environment.MachineName, Application.ProductVersion, "NoOpenPeriod"))
                    MsgBox("يجب عليك فتح وردية أولا لكي تستمر في شاشة البيع", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
                If Not canSell Then
                    ShowAuditWarning(AuditSecuritySession.Current.RejectLogin(connection, candidateId, candidateName, "Saleslogin", Environment.MachineName, Application.ProductVersion, "SalesPermissionDenied"))
                    MsgBox("لا تتوفر صلاحية دخول لواجهة البيع لهذا المستخدم", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                ' All rejection checks use the candidate, not the globals of the previous user.
                Dim auditWarning As String = AuditSecuritySession.Current.Start(connection, candidateId, candidateName, "Saleslogin", Environment.MachineName, Application.ProductVersion)
                USER_ID = candidateId
                USER_NAME = candidateName
                User_isAdmin = Convert.ToBoolean(candidate("isAdmin"))
                U_AG_ID = If(IsDBNull(candidate("AG_ID")), 0, Convert.ToInt32(candidate("AG_ID")))
                isPr_Open = periods.Rows.Count > 0
                Pr_ID = If(isPr_Open, Convert.ToInt32(periods.Rows(0)("Pr_ID")), 1)
                If isPr_Open Then U_Flate_ID = Convert.ToInt32(periods.Rows(0)("TB_Flate_ID"))
                ShowAuditWarning(auditWarning)
                F_MainForm.Mange_EmpValid()
                F_POS.POS_CHECK_USER_Valid()
                F_POS.ResetNewBill()
                F_POS.UnLockButton.BackgroundImage = My.Resources.ResourceManager.GetObject("UnLock")
                F_POS.FormActive = True
                F_POS.Enabled = True
                isUserLoggin = True
                Beep()
                Me.Close()
            Else
                ShowAuditWarning(AuditSecuritySession.Current.RejectLogin(connection, Nothing, Nothing, "Saleslogin", Environment.MachineName, Application.ProductVersion))
                MsgBox("خطأ في البيانات المدخلة", MsgBoxStyle.Critical, "خطأ")
                passTxt.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            loginInProgress = False
            If Not Me.IsDisposed Then LoginButton.Enabled = True
        End Try
    End Sub

    Private Sub ShowAuditWarning(message As String)
        If Not String.IsNullOrWhiteSpace(message) Then MsgBox(message, MsgBoxStyle.Exclamation, "شبكة المراقبة")
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
