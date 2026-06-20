Imports System.Data
Imports System.Data.SqlClient

Public Class FrmBudgetItems

    Private ReadOnly ConnStr As String = MY_Settings.SqlConStr
    Private CurrentItemId As Integer = 0

    Private Function NewRefNo() As String
        Return "BI-" & DateTime.Now.ToString("yyyyMMdd-HHmmss")
    End Function

    '=========================
    ' Audit Context (SQL 2014)
    '=========================
    Private Sub SetUserContext(refNo As String)
        Using cn As New SqlConnection(ConnStr)
            cn.Open()
            Using cmd As New SqlCommand("
DELETE FROM dbo.User_Context WHERE SPID = @@SPID;
INSERT INTO dbo.User_Context (SPID, UserId, RefNo)
VALUES (@@SPID, @UserId, @RefNo);", cn)

                cmd.Parameters.AddWithValue("@UserId", USER_ID)
                cmd.Parameters.AddWithValue("@RefNo", refNo)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    '=========================
    ' Load
    '=========================
    Private Sub FrmBudgetItems_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplyGridStyle()
            BudgetUiHelper.ApplyBudgetFormStyle(Me)
            EnsureContraAccountColumn()
            LoadDoors()
            'LoadContraAccounts()
            ClearForm()
            SetStatus("جاهز")
        Catch ex As Exception
            SetStatus("خطأ: " & ex.Message)
        End Try
    End Sub

    Private Sub EnsureContraAccountColumn()
        Using cn As New SqlConnection(ConnStr)
            cn.Open()
            Using cmd As New SqlCommand("
IF COL_LENGTH('dbo.Budget_Items', 'ContraAccountCode') IS NULL
BEGIN
    ALTER TABLE dbo.Budget_Items ADD ContraAccountCode NVARCHAR(40) NULL;
END", cn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    '    Private Sub LoadContraAccounts()
    '        Dim dt As New DataTable()
    '        Using cn As New SqlConnection(ConnStr)
    '            Using cmd As New SqlCommand("
    'SELECT ACC_CODE, AccountText
    'FROM (
    '    SELECT CAST(NULL AS NVARCHAR(40)) AS ACC_CODE,
    '           N'-- اختر الحساب المقابل --' AS AccountText,
    '           0 AS SortNo
    '    UNION ALL
    '    SELECT CONVERT(NVARCHAR(40), ACC_CODE) AS ACC_CODE,
    '           CONVERT(NVARCHAR(40), ACC_CODE) + N' - ' + ACC_NAME + N' (' +
    '           CASE ACC_Type WHEN 1 THEN N'مصرف' WHEN 2 THEN N'صندوق' ELSE N'حساب' END + N')' AS AccountText,
    '           1 AS SortNo
    '    FROM dbo.Rct_Mang_V
    '    WHERE ACC_Type IN (1, 2)
    ') x
    'ORDER BY SortNo, AccountText;", cn)

    '                Using da As New SqlDataAdapter(cmd)
    '                    da.Fill(dt)
    '                End Using
    '            End Using
    '        End Using

    '        cmbContraAccount.DataSource = dt
    '        cmbContraAccount.DisplayMember = "AccountText"
    '        cmbContraAccount.ValueMember = "ACC_CODE"
    '        cmbContraAccount.SelectedIndex = 0
    '    End Sub

    Private Sub ApplyGridStyle()
        dgvItems.EnableHeadersVisualStyles = False
        dgvItems.ColumnHeadersHeight = 38
        dgvItems.RowTemplate.Height = 34
        dgvItems.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        dgvItems.DefaultCellStyle.Font = New Font("Segoe UI", 10)
        dgvItems.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 237, 255)
        dgvItems.RowHeadersVisible = False
    End Sub

    Private Sub LoadDoors()
        Dim dt As New DataTable()
        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT DoorId,
       DoorCode + N' - ' + DoorName AS DoorText
FROM Budget_Doors
WHERE IsActive = 1
ORDER BY DoorCode;", cn)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        cmbDoors.DataSource = dt
        cmbDoors.DisplayMember = "DoorText"
        cmbDoors.ValueMember = "DoorId"
        cmbDoors.SelectedIndex = -1
    End Sub

    Private Sub LoadChapters(doorId As Integer)
        Dim dt As New DataTable()
        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT ChapterId,
       ChapterCode + N' - ' + ChapterName AS ChapterText
FROM Budget_Chapters
WHERE DoorId = @DoorId AND IsActive = 1
ORDER BY ChapterCode;", cn)

                cmd.Parameters.AddWithValue("@DoorId", doorId)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        cmbChapters.DataSource = dt
        cmbChapters.DisplayMember = "ChapterText"
        cmbChapters.ValueMember = "ChapterId"
        cmbChapters.SelectedIndex = -1
    End Sub

    Private Sub LoadItems(Optional chapterId As Integer = 0)
        Dim dt As New DataTable()
        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT
    i.BudgetItemId,
    d.DoorName,
    c.ChapterName,
    i.ItemCode,
    i.ItemName,
    i.ContraAccountCode,
    ISNULL(i.ContraAccountCode + N' - ' + a.ACC_NAME, N'') AS ContraAccountName,
    i.IsActive
FROM Budget_Items i
JOIN Budget_Chapters c ON i.ChapterId = c.ChapterId
JOIN Budget_Doors d ON c.DoorId = d.DoorId
LEFT JOIN ACCOUNTS_TREE a ON CONVERT(NVARCHAR(40), a.ACC_CODE) = i.ContraAccountCode
WHERE (@ChapterId = 0 OR i.ChapterId = @ChapterId)
ORDER BY d.DoorCode, c.ChapterCode, i.ItemCode;", cn)

                cmd.Parameters.AddWithValue("@ChapterId", chapterId)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        dgvItems.DataSource = dt
        dgvItems.Columns("BudgetItemId").Visible = False
        dgvItems.Columns("DoorName").HeaderText = "الباب"
        dgvItems.Columns("ChapterName").HeaderText = "الفصل"
        dgvItems.Columns("ItemCode").HeaderText = "كود البند"
        dgvItems.Columns("ItemName").HeaderText = "اسم البند"
        dgvItems.Columns("ContraAccountCode").Visible = False
        dgvItems.Columns("ContraAccountName").HeaderText = "الحساب المقابل"
        dgvItems.Columns("IsActive").HeaderText = "نشط"

        dgvItems.ClearSelection()
        SetStatus($"تم تحميل {dt.Rows.Count} بند")
    End Sub

    '=========================
    ' Helpers
    '=========================
    Private Sub ClearForm()
        CurrentItemId = 0
        cmbDoors.SelectedIndex = -1
        cmbChapters.DataSource = Nothing
        txtItemCode.Text = ""
        txtItemName.Text = ""
        '  If cmbContraAccount.DataSource IsNot Nothing Then cmbContraAccount.SelectedIndex = 0
        chkIsActive.Checked = True
    End Sub

    Private Function ValidateForm() As Boolean
        If cmbDoors.SelectedIndex < 0 Then
            MsgBox("اختر الباب أولاً", MsgBoxStyle.Exclamation)
            Return False
        End If
        If cmbChapters.SelectedIndex < 0 Then
            MsgBox("اختر الفصل أولاً", MsgBoxStyle.Exclamation)
            Return False
        End If
        If txtItemCode.Text.Trim = "" Then
            MsgBox("أدخل كود البند", MsgBoxStyle.Exclamation)
            Return False
        End If
        If txtItemName.Text.Trim = "" Then
            MsgBox("أدخل اسم البند", MsgBoxStyle.Exclamation)
            Return False
        End If
        'If cmbContraAccount.SelectedIndex < 0 OrElse cmbContraAccount.SelectedValue Is Nothing OrElse cmbContraAccount.SelectedValue Is DBNull.Value Then
        '    MsgBox("اختر الحساب المقابل للبند من حسابات المصرف أو الصندوق", MsgBoxStyle.Exclamation)
        '    Return False
        'End If
        Return True
    End Function

    Private Function ItemCodeExists(chapterId As Integer, code As String, excludeId As Integer) As Boolean
        Using cn As New SqlConnection(ConnStr)
            cn.Open()
            Using cmd As New SqlCommand("
SELECT COUNT(1)
FROM Budget_Items
WHERE ChapterId = @ChapterId
  AND ItemCode = @Code
  AND BudgetItemId <> @ExcludeId;", cn)

                cmd.Parameters.AddWithValue("@ChapterId", chapterId)
                cmd.Parameters.AddWithValue("@Code", code)
                cmd.Parameters.AddWithValue("@ExcludeId", excludeId)
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    '=========================
    ' Save / Delete
    '=========================
    Private Sub SaveItem()
        If Not ValidateForm() Then Exit Sub

        Dim chapterId As Integer = cmbChapters.SelectedValue
        Dim code = txtItemCode.Text.Trim()
        Dim name = txtItemName.Text.Trim()
        '  Dim contraAccountCode As String = cmbContraAccount.SelectedValue.ToString()
        Dim active = If(chkIsActive.Checked, 1, 0)

        If ItemCodeExists(chapterId, code, CurrentItemId) Then
            MsgBox("كود البند موجود مسبقًا داخل نفس الفصل", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        SetUserContext(NewRefNo())

        Using cn As New SqlConnection(ConnStr)
            cn.Open()

            If CurrentItemId = 0 Then
                Using cmd As New SqlCommand("
INSERT INTO Budget_Items (ChapterId, ItemCode, ItemName, IsActive)
                  VALUES (@ChapterId, @Code, @Name, @IsActive);", cn)

                    cmd.Parameters.AddWithValue("@ChapterId", chapterId)
                    cmd.Parameters.AddWithValue("@Code", code)
                    cmd.Parameters.AddWithValue("@Name", name)
                    cmd.Parameters.AddWithValue("@IsActive", active)
                    cmd.ExecuteNonQuery()
                End Using
                SetStatus("تمت إضافة البند")
            Else
                Using cmd As New SqlCommand("
UPDATE Budget_Items
SET ChapterId = @ChapterId,
    ItemCode = @Code,
    ItemName = @Name,
    IsActive = @IsActive
WHERE BudgetItemId = @Id;", cn)

                    cmd.Parameters.AddWithValue("@ChapterId", chapterId)
                    cmd.Parameters.AddWithValue("@Code", code)
                    cmd.Parameters.AddWithValue("@Name", name)
                    cmd.Parameters.AddWithValue("@IsActive", active)
                    cmd.Parameters.AddWithValue("@Id", CurrentItemId)
                    cmd.ExecuteNonQuery()
                End Using
                SetStatus("تم تعديل البند")
            End If
        End Using

        LoadItems(chapterId)
        ClearForm()
    End Sub

    Private Sub SoftDeleteItem()
        If CurrentItemId = 0 Then Exit Sub

        If MsgBox("هل تريد تعطيل هذا البند؟", MsgBoxStyle.Question + MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then Exit Sub

        SetUserContext(NewRefNo())

        Using cn As New SqlConnection(ConnStr)
            cn.Open()
            Using cmd As New SqlCommand("
UPDATE Budget_Items
SET IsActive = 0
WHERE BudgetItemId = @Id;", cn)

                cmd.Parameters.AddWithValue("@Id", CurrentItemId)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        SetStatus("تم تعطيل البند")
        LoadItems(cmbChapters.SelectedValue)
        ClearForm()
    End Sub

    '=========================
    ' Events
    '=========================
    Private Sub cmbDoors_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbDoors.SelectionChangeCommitted
        LoadChapters(cmbDoors.SelectedValue)
        dgvItems.DataSource = Nothing
    End Sub

    Private Sub cmbChapters_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbChapters.SelectionChangeCommitted
        LoadItems(cmbChapters.SelectedValue)
    End Sub

    Private Sub dgvItems_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvItems.CellClick
        If dgvItems.CurrentRow Is Nothing Then Exit Sub
        CurrentItemId = dgvItems.CurrentRow.Cells("BudgetItemId").Value
        txtItemCode.Text = dgvItems.CurrentRow.Cells("ItemCode").Value.ToString()
        txtItemName.Text = dgvItems.CurrentRow.Cells("ItemName").Value.ToString()
        'If dgvItems.CurrentRow.Cells("ContraAccountCode").Value IsNot Nothing AndAlso dgvItems.CurrentRow.Cells("ContraAccountCode").Value IsNot DBNull.Value Then
        '    cmbContraAccount.SelectedValue = dgvItems.CurrentRow.Cells("ContraAccountCode").Value.ToString()
        'ElseIf cmbContraAccount.DataSource IsNot Nothing Then
        '    cmbContraAccount.SelectedIndex = 0
        'End If
        chkIsActive.Checked = Convert.ToBoolean(dgvItems.CurrentRow.Cells("IsActive").Value)
        SetStatus("وضع التعديل")
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        ClearForm()
        SetStatus("جديد")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveItem()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        SoftDeleteItem()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        If cmbChapters.SelectedIndex >= 0 Then
            LoadItems(cmbChapters.SelectedValue)
        End If
        SetStatus("تم التحديث")
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub SetStatus(msg As String)
        lblStatus.Text = msg
    End Sub

End Class
