Imports System.Data.SqlClient

Public Class USER_VALID_ACCOUNT
    Private user_dt As New DataTable
    Private ALL_B_DT As New DataTable
    Private USER_B_DT As New DataTable
    Private IsLoadingUser As Boolean = False

    Private Sub USER_VALID_ACCOUNT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        EnsureUserAccountAccessTables()
        PrepareForm()
        USERS_fill_List()
        ACCOUNTS_TREE_SELECT_ALL()
        LoadSelectedUserAccounts()
    End Sub

    Private Sub PrepareForm()
        CircularPanel.Visible = False
        Me.RightToLeft = RightToLeft.Yes
        Me.RightToLeftLayout = True

        Button1.Text = "السماح بكل الحسابات"
        'ADD_Btn.Text = "إضافة"
        'REMOVE_BTN.Text = "حذف"

        SendMessage(CMSearchTextBox.Handle, &H1501, 0, "إبحث عن مستخدم")
        SendMessage(ALL_Search_By_Acc_Name_txt.Handle, &H1501, 0, "إبحث عن إسم حسـاب")
        SendMessage(ALL_Search_By_Acc_Code_txt.Handle, &H1501, 0, "إبحث عن رقـم حسـاب")
        SendMessage(USER_Search_By_Acc_Name_txt.Handle, &H1501, 0, "إبحث عن إسم حسـاب")
        SendMessage(USER_Search_By_Acc_Code_txt.Handle, &H1501, 0, "إبحث عن رقـم حسـاب")

        FormatGrid(ALL_DataGridView)
        FormatGrid(USER_DataGridView)
    End Sub

    Private Sub FormatGrid(grid As DataGridView)
        grid.AutoGenerateColumns = True
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        grid.BackgroundColor = Color.FromArgb(248, 250, 252)
        grid.BorderStyle = BorderStyle.FixedSingle
        grid.RowHeadersVisible = False
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grid.MultiSelect = False
        grid.ReadOnly = True
        grid.RightToLeft = RightToLeft.Yes
        grid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0!, FontStyle.Regular)
        grid.DefaultCellStyle.Font = New Font("Segoe UI Semibold", 9.0!, FontStyle.Bold)
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254)
        grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42)
        grid.RowTemplate.Height = 28
    End Sub

    Public Sub USERS_fill_List()
        user_dt.Clear()

        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            Using da As New SqlDataAdapter("SELECT user_id, UserName FROM dbo.Users ORDER BY UserName ASC", cn)
                da.Fill(user_dt)
            End Using
        End Using

        NameUserListBox.DataSource = user_dt
        NameUserListBox.ValueMember = "user_id"
        NameUserListBox.DisplayMember = "UserName"
    End Sub

    Public Sub ACCOUNTS_TREE_SELECT_ALL()
        ALL_B_DT = New DataTable
        ALL_DataGridView.DataSource = Nothing

        CircularPanel.Visible = True
        CircularProgressControl1.Start()

        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            Using da As New SqlDataAdapter("
SELECT
    T_ID,
    CONVERT(NVARCHAR(40), ACC_CODE) AS ACC_CODE,
    ACC_NAME,
    ACC_PARENT,
    ACC_LEVEL
FROM dbo.ACCOUNTS_TREE
ORDER BY ACC_CODE;", cn)
                da.Fill(ALL_B_DT)
            End Using
        End Using

        ALL_DataGridView.DataSource = ALL_B_DT
        HideAccountGridTechnicalColumns(ALL_DataGridView)

        CircularPanel.Visible = False
        CircularProgressControl1.Stop()
    End Sub

    Private Function GetSelectedUserId() As Integer
        If NameUserListBox.SelectedValue Is Nothing OrElse TypeOf NameUserListBox.SelectedValue Is DataRowView Then Return 0

        Dim userId As Integer = 0
        Integer.TryParse(NameUserListBox.SelectedValue.ToString(), userId)
        Return userId
    End Function

    Private Sub EnsureUserSetting(userId As Integer)
        If userId <= 0 Then Return

        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
IF NOT EXISTS (SELECT 1 FROM dbo.User_Account_Access_Settings WHERE UserId = @UserId)
BEGIN
    INSERT INTO dbo.User_Account_Access_Settings (UserId, AllowAllAccounts)
    VALUES (@UserId, 1)
END", cn)

                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId
                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Function LoadUserAllowAll(userId As Integer) As Boolean
        If userId <= 0 Then Return True
        EnsureUserSetting(userId)

        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
SELECT TOP 1 AllowAllAccounts
FROM dbo.User_Account_Access_Settings
WHERE UserId = @UserId;", cn)

                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId
                cn.Open()
                Dim value As Object = cmd.ExecuteScalar()
                If value Is Nothing OrElse value Is DBNull.Value Then Return True

                Return Convert.ToBoolean(value)
            End Using
        End Using
    End Function

    Private Sub SaveUserAllowAll(userId As Integer, allowAll As Boolean)
        If userId <= 0 Then Return
        EnsureUserSetting(userId)

        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
UPDATE dbo.User_Account_Access_Settings
SET AllowAllAccounts = @AllowAllAccounts,
    UpdatedAt = GETDATE()
WHERE UserId = @UserId;", cn)

                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId
                cmd.Parameters.Add("@AllowAllAccounts", SqlDbType.Bit).Value = allowAll
                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub LoadSelectedUserAccounts()
        Dim userId As Integer = GetSelectedUserId()
        If userId <= 0 Then Return

        IsLoadingUser = True
        chkAllowAllAccounts.Checked = LoadUserAllowAll(userId)
        IsLoadingUser = False

        USER_B_DT = New DataTable
        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
SELECT
    UA.AccountCode AS ACC_CODE,
    ISNULL(A.ACC_NAME, N'') AS ACC_NAME
FROM dbo.User_Account_Allowed UA
LEFT JOIN dbo.ACCOUNTS_TREE A
    ON LTRIM(RTRIM(CONVERT(NVARCHAR(40), A.ACC_CODE))) = LTRIM(RTRIM(UA.AccountCode))
WHERE UA.UserId = @UserId
ORDER BY UA.AccountCode;", cn)

                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(USER_B_DT)
                End Using
            End Using
        End Using

        USER_DataGridView.DataSource = USER_B_DT
        HideAccountGridTechnicalColumns(USER_DataGridView)
        ApplyAllowAllUiState()
    End Sub

    Private Sub HideAccountGridTechnicalColumns(grid As DataGridView)
        If grid.Columns.Contains("T_ID") Then grid.Columns("T_ID").Visible = False
        If grid.Columns.Contains("ACC_PARENT") Then grid.Columns("ACC_PARENT").Visible = False
        If grid.Columns.Contains("ACC_LEVEL") Then grid.Columns("ACC_LEVEL").Visible = False
        If grid.Columns.Contains("ACC_CODE") Then grid.Columns("ACC_CODE").HeaderText = "رقم الحساب"
        If grid.Columns.Contains("ACC_NAME") Then grid.Columns("ACC_NAME").HeaderText = "اسم الحساب"
    End Sub

    Private Function GetSelectedAccountCode(grid As DataGridView) As String
        If grid.CurrentRow Is Nothing Then Return ""
        If Not grid.Columns.Contains("ACC_CODE") Then Return ""

        Dim value As Object = grid.CurrentRow.Cells("ACC_CODE").Value
        If value Is Nothing OrElse value Is DBNull.Value Then Return ""

        Return value.ToString().Trim()
    End Function

    Private Sub AddSelectedAccount()
        Dim userId As Integer = GetSelectedUserId()
        Dim accountCode As String = GetSelectedAccountCode(ALL_DataGridView)

        If userId <= 0 Then
            MsgBox("حدد المستخدم أولاً", MsgBoxStyle.Exclamation, "صلاحية الحسابات")
            Return
        End If

        If String.IsNullOrWhiteSpace(accountCode) Then
            MsgBox("حدد الحساب المطلوب إضافته", MsgBoxStyle.Exclamation, "صلاحية الحسابات")
            Return
        End If

        SaveUserAllowAll(userId, False)
        chkAllowAllAccounts.Checked = False

        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
IF NOT EXISTS
(
    SELECT 1
    FROM dbo.User_Account_Allowed
    WHERE UserId = @UserId
      AND LTRIM(RTRIM(AccountCode)) = LTRIM(RTRIM(@AccountCode))
)
BEGIN
    INSERT INTO dbo.User_Account_Allowed (UserId, AccountCode)
    VALUES (@UserId, @AccountCode)
END", cn)

                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId
                cmd.Parameters.Add("@AccountCode", SqlDbType.NVarChar, 40).Value = accountCode
                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using

        LoadSelectedUserAccounts()
    End Sub

    Private Sub RemoveSelectedAccount()
        Dim userId As Integer = GetSelectedUserId()
        Dim accountCode As String = GetSelectedAccountCode(USER_DataGridView)

        If userId <= 0 Then
            MsgBox("حدد المستخدم أولاً", MsgBoxStyle.Exclamation, "صلاحية الحسابات")
            Return
        End If

        If String.IsNullOrWhiteSpace(accountCode) Then
            MsgBox("حدد الحساب المطلوب حذفه", MsgBoxStyle.Exclamation, "صلاحية الحسابات")
            Return
        End If

        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
DELETE FROM dbo.User_Account_Allowed
WHERE UserId = @UserId
  AND LTRIM(RTRIM(AccountCode)) = LTRIM(RTRIM(@AccountCode));", cn)

                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId
                cmd.Parameters.Add("@AccountCode", SqlDbType.NVarChar, 40).Value = accountCode
                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using

        LoadSelectedUserAccounts()
    End Sub

    Private Sub ApplyAllowAllUiState()
        Dim customMode As Boolean = Not chkAllowAllAccounts.Checked

        USER_DataGridView.Enabled = customMode
        ADD_Btn.Enabled = customMode
        REMOVE_BTN.Enabled = customMode
        USER_Search_By_Acc_Name_txt.Enabled = customMode
        USER_Search_By_Acc_Code_txt.Enabled = customMode

        If chkAllowAllAccounts.Checked Then
            Label2.Text = "المستخدم مسموح له بكل الحسابات"
        Else
            Label2.Text = "قائمة الحسابات للمستخدم"
        End If
    End Sub

    Private Sub ApplyAccountFilter(table As DataTable, grid As DataGridView, codeText As String, nameText As String)
        If table Is Nothing Then Return

        Dim filters As New List(Of String)
        If Not String.IsNullOrWhiteSpace(codeText) Then filters.Add("[ACC_CODE] LIKE '%" & EscapeRowFilterValue(codeText) & "%'")
        If Not String.IsNullOrWhiteSpace(nameText) Then filters.Add("[ACC_NAME] LIKE '%" & EscapeRowFilterValue(nameText) & "%'")

        Dim dv As DataView = table.DefaultView
        dv.RowFilter = String.Join(" AND ", filters)
        grid.DataSource = dv
    End Sub

    Private Function EscapeRowFilterValue(value As String) As String
        Return value.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]").Replace("*", "[*]")
    End Function

    Private Sub ALL_Search_TextChanged(sender As Object, e As EventArgs) Handles ALL_Search_By_Acc_Name_txt.TextChanged, ALL_Search_By_Acc_Code_txt.TextChanged
        ApplyAccountFilter(ALL_B_DT, ALL_DataGridView, ALL_Search_By_Acc_Code_txt.Text, ALL_Search_By_Acc_Name_txt.Text)
    End Sub

    Private Sub USER_Search_TextChanged(sender As Object, e As EventArgs) Handles USER_Search_By_Acc_Name_txt.TextChanged, USER_Search_By_Acc_Code_txt.TextChanged
        ApplyAccountFilter(USER_B_DT, USER_DataGridView, USER_Search_By_Acc_Code_txt.Text, USER_Search_By_Acc_Name_txt.Text)
    End Sub

    Private Sub CMSearchTextBox_TextChanged(sender As Object, e As EventArgs) Handles CMSearchTextBox.TextChanged
        Dim dv As DataView = user_dt.DefaultView
        dv.RowFilter = "UserName LIKE '%" & EscapeRowFilterValue(CMSearchTextBox.Text) & "%'"
    End Sub

    Private Sub NameUserListBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles NameUserListBox.SelectedValueChanged
        LoadSelectedUserAccounts()
    End Sub

    Private Sub ADD_Btn_Click(sender As Object, e As EventArgs) Handles ADD_Btn.Click
        AddSelectedAccount()
    End Sub

    Private Sub REMOVE_BTN_Click(sender As Object, e As EventArgs) Handles REMOVE_BTN.Click
        RemoveSelectedAccount()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim userId As Integer = GetSelectedUserId()
        If userId <= 0 Then
            MsgBox("حدد المستخدم أولاً", MsgBoxStyle.Exclamation, "صلاحية الحسابات")
            Return
        End If

        chkAllowAllAccounts.Checked = True
        SaveUserAllowAll(userId, True)
        ApplyAllowAllUiState()
    End Sub

    Private Sub chkAllowAllAccounts_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowAllAccounts.CheckedChanged
        If IsLoadingUser Then Return

        Dim userId As Integer = GetSelectedUserId()
        If userId > 0 Then SaveUserAllowAll(userId, chkAllowAllAccounts.Checked)

        ApplyAllowAllUiState()
    End Sub
End Class
