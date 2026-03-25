Imports System.Globalization
Imports System.Data.SqlClient

Public Class users
    Public ID_u As Integer
    Public NewUser_id As Integer
    'Public d_dt As New DataTable
    'Public dt As New DataTable

    Dim user_dt As New DataTable
    Dim isAdmin As Boolean
    Dim Allow_Str As String = "لا"
    Dim User_Name As String

    Public Sub clear_Text()
        NameUserTextBox.Clear()
        PassUserTextBox.Clear()
        Default_AG_cm.SelectedIndex = -1
        Is_Allow_CB.Enabled = True
        Is_Allow_CB.Checked = True
        EditUserButton.Text = "تعديل"
        ID_u = 0
        EditUserButton.Enabled = False
        ChangePassButton.Enabled = False
        DeleteUserButton.Enabled = False
    End Sub


    Private Sub users_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
    Private Sub users_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then NewUserButton_Click(sender, e)
        If e.KeyCode = Keys.F2 Then If AddUserButton.Enabled = True Then AddUserButton_Click(sender, e)
        If e.KeyCode = Keys.F3 Then If EditUserButton.Enabled = True Then EditUserButton_Click(sender, e)
        If e.KeyCode = Keys.F4 Then If DeleteUserButton.Enabled = True Then DeleteUserButton_Click(sender, e)
    End Sub

    Private Sub users_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        USERS_fill_List()
        Load_AG()
        If isCostmerScreen = False Then CostmerScreen_CB.Visible = False
        Users_Validations()
        fetch_Tr()
        SendMessage(CMSearchTextBox.Handle, &H1501, 0, "إبحث عن مستخدم")
    End Sub

    Public Sub fetch_Tr()
        Treasury_ComboBox.DataSource = Ge_Tr_Items()
        Treasury_ComboBox.DisplayMember = "name"
        Treasury_ComboBox.ValueMember = "ID"
        Treasury_ComboBox.SelectedIndex = 0
    End Sub

    Public Sub Users_Validations()
        Dim c As New C
        Try
            Dim s As String
            s = "select T_ID,Valid_Name from Users_Validations ORDER By T_ID ASC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            Valid_Cm.DataSource = c.Dt
            Valid_Cm.DisplayMember = "Valid_Name"
            Valid_Cm.ValueMember = "T_ID"
            Valid_Cm.SelectedValue = 1

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Load_AG()

        Dim C As New C
        Dim sql As String = " select AG_ID,Ag_name from Agents WHERE AG_ID <> " & Default_AG_ID & " Order By AG_ID ASC"
        C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
        C.Da.Fill(C.Dt)
        Default_AG_cm.DataSource = C.Dt
        Default_AG_cm.DisplayMember = "Ag_name"
        Default_AG_cm.ValueMember = "AG_ID"
        Default_AG_cm.SelectedValue = SB_Default_AG
    End Sub


    Public Sub USERS_fill_List()
        Dim c1 As New C
        user_dt.Clear()
        Dim sql As String = "select user_id,UserName from Users ORDER BY UserName ASC"
        Dim com As New SqlDataAdapter(sql, c1.Con)
        com.Fill(user_dt)
        NameUserListBox.DataSource = user_dt
        NameUserListBox.ValueMember = "user_id"
        NameUserListBox.DisplayMember = "UserName"


    End Sub

    Public Sub User_Insert()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "User_Insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@userName", NameUserTextBox.Text)
            .Parameters.AddWithValue("@UserPass", EncryptionHelper.Encrypt(PassUserTextBox.Text))
            .Parameters.AddWithValue("@Valid_ID", Valid_Cm.SelectedValue)
            '.Parameters.AddWithValue("@isPone_Users", isPone_Users.Checked)
            'If isPone_Users.Checked = True Then .Parameters.AddWithValue("@TB_ID", Phone_Selected_TP.SelectedValue)
            .Parameters.AddWithValue("@isCostmerScreen", CostmerScreen_CB.Checked)
            If Default_AG_cm.SelectedValue > 0 Then .Parameters.AddWithValue("@AG_ID", Default_AG_cm.SelectedValue)
            .Parameters.AddWithValue("@is_Allow", Is_Allow_CB.Checked)
            .Parameters.AddWithValue("@Tr_ID", Treasury_ComboBox.SelectedValue)
            .Parameters.AddWithValue("@WEB_PASS", PassUserTextBox.Text)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تم إضافة المستخدم بنجاح", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" الإسم:" & NameUserTextBox.Text & " الصلاحية:" & Valid_Cm.Text & " الخزينة الإفتراضية:" & Treasury_ComboBox.Text & " إمكانية الدخول:" & Allow_Str, 0, 31, 1)
            clear_Text()
            'd_dt.Clear()
            USERS_fill_List()
            FieldsPanel.Enabled = False
            AddUserButton.Enabled = False
        End If

    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Function USER_check_pass()
        Dim c1 As New C
        Dim f As Boolean = True
        Dim sql As String = "select * from users where UserPass ='" & EncryptionHelper.Encrypt(PassUserTextBox.Text) & "'"
        Dim com As New SqlCommand(sql, c1.Con)
        Dim dr As SqlDataReader
        c1.Con.Open()
        Try
            dr = com.ExecuteReader
            dr.Read()
            If dr.HasRows = True Then
                f = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()

        Return f
    End Function

    Public Function USERS_check_pass_Edited()
        Dim c1 As New C
        Dim f As Boolean = False
        Dim sql As String = "select * from users where UserPass ='" & PassUserTextBox.Text & "' and user_id not in('" & ID_u & "')"
        Dim com As New SqlCommand(sql, c1.Con)
        Dim dr As SqlDataReader
        c1.Con.Open()
        Try
            dr = com.ExecuteReader
            dr.Read()

            If dr.HasRows = True Then
                f = True
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()

        Return f
    End Function

    '-----------------------------------------------------------------
    Public Sub User_Update()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "User_Update"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@user_id", ID_u)
            .Parameters.AddWithValue("@userName", NameUserTextBox.Text)
            .Parameters.AddWithValue("@Valid_ID", Valid_Cm.SelectedValue)
            '.Parameters.AddWithValue("@isPone_Users", isPone_Users.Checked)
            'If isPone_Users.Checked = True Then .Parameters.AddWithValue("@TB_ID", Phone_Selected_TP.SelectedValue)
            .Parameters.AddWithValue("@isCostmerScreen", CostmerScreen_CB.Checked)
            If Default_AG_cm.SelectedValue > 0 Then .Parameters.AddWithValue("@AG_ID", Default_AG_cm.SelectedValue)
            .Parameters.AddWithValue("@is_Allow", Is_Allow_CB.Checked)
            .Parameters.AddWithValue("@Tr_ID", Treasury_ComboBox.SelectedValue)

            .Parameters.AddWithValue("@WEB_PASS", PassUserTextBox.Text)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تم التعديل ", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" الإسم السابق:" & User_Name & " الإسم:" & NameUserTextBox.Text & " الصلاحية:" & Valid_Cm.Text & " الخزينة الإفتراضية:" & Treasury_ComboBox.Text & " إمكانية الدخول:" & Allow_Str, 0, 31, 3)
            clear_Text()
            'd_dt.Clear()
            USERS_fill_List()
            FieldsPanel.Enabled = False
            AddUserButton.Enabled = False
        End If


    End Sub

    Public Sub User_Delete()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "User_Delete"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("user_id", ID_u)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تم الحذف ", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" الإسم:" & User_Name, 0, 31, 2)
            clear_Text()
            'd_dt.Clear()
            USERS_fill_List()
            EditUserButton.Text = "تعديل"
            EditUserButton.Enabled = False
            ChangePassButton.Enabled = False
            DeleteUserButton.Enabled = False
        End If

    End Sub


    Public Sub USER_Reader_in_Text()
        Dim c As New C
        c.Str = "select * from Users where user_id ='" & NameUserListBox.SelectedValue & "'"
        c.Com = New SqlCommand(c.Str, c.Con)

        c.Con.Open()
        c.Dr = c.Com.ExecuteReader
        c.Dr.Read()
        ID_u = c.Dr("user_id")
        NameUserTextBox.Text = c.Dr("UserName")
        User_Name = c.Dr("UserName")
        Try
            PassUserTextBox.Text = EncryptionHelper.Decrypt(c.Dr("UserPass"))
        Catch ex As Exception
            PassUserTextBox.Text = c.Dr("UserPass")
        End Try


        isAdmin = c.Dr("isAdmin")
        Valid_Cm.SelectedValue = c.Dr("Valid_ID")
        CostmerScreen_CB.Checked = c.Dr("isCostmerScreen")
        If Not IsDBNull(c.Dr("AG_ID")) Then Default_AG_cm.SelectedValue = c.Dr("AG_ID")
        Is_Allow_CB.Checked = c.Dr("is_Allow")
        Treasury_ComboBox.SelectedValue = c.Dr("Tr_ID")
        c.Con.Close()

        EditUserButton.Text = "تعديل"
        DeleteUserButton.Enabled = True
        FieldsPanel.Enabled = False
        DeleteUserButton.Enabled = True
        EditUserButton.Enabled = True
        PassUserTextBox.Enabled = False
        ChangePassButton.Enabled = True
        AddUserButton.Enabled = False

    End Sub

    'Public Sub User_Fetch()
    '    Dim c As New C
    '    dt.Clear()
    '    Dim sql As String = "select * from users"
    '    Dim com As New SqlDataAdapter(sql, c.Con)
    '    com.Fill(dt)
    '    NameUserListBox.DataSource = dt
    '    NameUserListBox.ValueMember = "UserID"
    '    NameUserListBox.DisplayMember = "UserName"
    'End Sub

    Private Sub NameUserTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles NameUserTextBox.KeyDown
        If e.KeyCode = Keys.Return Then PassUserTextBox.Select()
    End Sub

    Private Sub NameUserTextBox_TextChanged(sender As Object, e As EventArgs) Handles NameUserTextBox.TextChanged
        ErrorProvideName.Clear()
    End Sub

    Private Sub NameUserTextBox_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles NameUserTextBox.Validating
        If String.IsNullOrWhiteSpace(NameUserTextBox.Text) Then
            ErrorProvideName.SetError(NameUserTextBox, " الرجاء إدخال اسم المستخدم")
            e.Cancel = True
        Else
            e.Cancel = False
            ErrorProvideName.Clear()
        End If
    End Sub

    Private Sub PassUserTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles PassUserTextBox.KeyDown
        If e.KeyCode = Keys.Return Then
            Valid_Cm.DroppedDown = True
            Valid_Cm.Select()
        End If
    End Sub

    'Private Sub PassUserTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PassUserTextBox.KeyPress
    '    Check_Only_Int(sender, e)
    'End Sub

    Private Sub PassUserTextBox_TextChanged(sender As Object, e As EventArgs) Handles PassUserTextBox.TextChanged
        ErrorProvidePass.Clear()
    End Sub

    Private Sub PassUserTextBox_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles PassUserTextBox.Validating
        If String.IsNullOrWhiteSpace(PassUserTextBox.Text) Then
            ErrorProvidePass.SetError(PassUserTextBox, "الرجاء إدخال الرقم السري ")
            e.Cancel = True
        Else
            e.Cancel = False
            ErrorProvidePass.Clear()
        End If
    End Sub

    Private Sub NewUserButton_Click(sender As Object, e As EventArgs) Handles NewUserButton.Click
        FieldsPanel.Enabled = True
        EditUserButton.Text = "تعديل"
        PassUserTextBox.Enabled = True
        DeleteUserButton.Enabled = False
        EditUserButton.Enabled = False
        ChangePassButton.Enabled = False
        AddUserButton.Enabled = True
        clear_Text()
        NameUserTextBox.Select()
        Valid_Cm.Enabled = True
        Valid_Cm.SelectedIndex = 0
        Treasury_ComboBox.SelectedValue = 0
    End Sub

    Private Sub AddUserButton_Click(sender As Object, e As EventArgs) Handles AddUserButton.Click
        If ValidateChildren() = True Then
            If USER_check_pass() = False Then
                MsgBox("هذا الرقم موجود مسبقاً ... الرجاء اختيار رقم آخر", MsgBoxStyle.Critical)
                Exit Sub
            Else
                User_Insert()
            End If

        End If
    End Sub
    Private Sub EditUserButton_Click(sender As Object, e As EventArgs) Handles EditUserButton.Click

        If EditUserButton.Text = "تعديل" Then
            EditUserButton.Text = "تأكيد التعديلات"
            FieldsPanel.Enabled = True
        Else
            If ValidateChildren() = True Then User_Update()
        End If

    End Sub

    Private Sub DeleteUserButton_Click(sender As Object, e As EventArgs) Handles DeleteUserButton.Click
        If ID_u = 1 Then
            MsgBox("هذا المستخدم هو الرئيسي للمنظومة ... لا يمكنك حذفه", MsgBoxStyle.Exclamation, "إيقاف عملية الحــذف")
        Else
            Beep()
            If MessageBox.Show(" تـأكيــد حــذف المــوظف " + NameUserTextBox.Text, "تــأكيد الحــفظ", _
          MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                User_Delete()
            End If
        End If

    End Sub

    Private Sub ComUser_KeyDown(sender As Object, e As KeyEventArgs) Handles Valid_Cm.KeyDown
        If e.KeyCode = Keys.Return Then
            AddUserButton_Click(sender, e)
        End If
    End Sub

    Private Sub ChangePassButton_Click(sender As Object, e As EventArgs) Handles ChangePassButton.Click
        If ID_u > 0 Then UpdateUserPass.ShowDialog()
    End Sub


    Private Sub ComUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Valid_Cm.SelectedIndexChanged
        If CostmerScreen_CB.Checked = True Then Valid_Cm.SelectedIndex = 0
    End Sub



    Private Sub NameUserListBox_Click(sender As Object, e As EventArgs) Handles NameUserListBox.Click
        clear_Text()
        USER_Reader_in_Text()
        If ID_u = 1 Then
            Valid_Cm.Enabled = False
            Is_Allow_CB.Enabled = False
        Else
            Valid_Cm.Enabled = True
            Is_Allow_CB.Enabled = True
        End If
    End Sub


    Private Sub CostmerScreen_CB_CheckedChanged(sender As Object, e As EventArgs) Handles CostmerScreen_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Is_Allow_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Is_Allow_CB.CheckedChanged
        CB_CHecked(sender)
        If sender.Checked = True Then
            Allow_Str = "نعم"
        Else
            Allow_Str = "لا"
        End If
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub ShowPassButton_MouseHover(sender As Object, e As EventArgs) Handles ShowPassButton.MouseHover
        PassUserTextBox.UseSystemPasswordChar = False
    End Sub

    Private Sub ShowPassButton_MouseLeave(sender As Object, e As EventArgs) Handles ShowPassButton.MouseLeave
        PassUserTextBox.UseSystemPasswordChar = True
    End Sub

    Private Sub CMSearchTextBox_TextChanged(sender As Object, e As EventArgs) Handles CMSearchTextBox.TextChanged
        Dim dv As DataView
        dv = user_dt.DefaultView
        dv.RowFilter = "UserName like '%" & CMSearchTextBox.Text & "%'"
    End Sub
End Class