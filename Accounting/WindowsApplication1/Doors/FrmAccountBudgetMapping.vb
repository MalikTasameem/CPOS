Imports System.Data
Imports System.Data.SqlClient

Public Class FrmAccountBudgetMapping

    Private ReadOnly ConnStr As String = MY_Settings.SqlConStr

    'مفتاح صف الربط الحالي (للتحديد من الجريد)
    Private CurrentMappingId As Integer = 0
    Private MappingDt As New DataTable()
    Private MappingBs As New BindingSource()

    Private Function NewRefNo() As String
        Return "MAP-" & DateTime.Now.ToString("yyyyMMdd-HHmmss")
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

    Private Sub FrmAccountBudgetMapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplyGridStyle()
            BudgetUiHelper.ApplyBudgetFormStyle(Me)
            ApplyAccountPickerStyle()
            ArrangeResponsiveLayout()
            LoadAccounts()
            LoadBudgetItems()
            LoadMappingGrid()
            ClearForm()
            SetStatus("جاهز")
        Catch ex As Exception
            SetStatus("خطأ: " & ex.Message)
        End Try
    End Sub

    Private Sub ApplyAccountPickerStyle()
        If btnPickAccount Is Nothing Then Exit Sub

        btnPickAccount.AutoSize = False
        btnPickAccount.FlatStyle = FlatStyle.Flat
        btnPickAccount.Font = New Font("Segoe UI Semibold", 9.0!, FontStyle.Bold)
        btnPickAccount.Text = "..."
    End Sub

    Private Sub ArrangeResponsiveLayout()
        If pnlContent Is Nothing OrElse cardForm Is Nothing OrElse cardGrid Is Nothing Then Return

        Dim margin As Integer = 15
        Dim contentW As Integer = Math.Max(0, pnlContent.ClientSize.Width - (margin * 2))
        Dim contentH As Integer = Math.Max(0, pnlContent.ClientSize.Height - (margin * 2))

        cardForm.SetBounds(margin, 4, contentW, 163)
        cardGrid.SetBounds(margin, cardForm.Bottom + 8, contentW, Math.Max(120, contentH - cardForm.Height - 8))

        ArrangeHeaderControls()
        ArrangeAccountFormControls()
        ArrangeActionButtons()
    End Sub

    Private Sub ArrangeHeaderControls()
        If pnlHeader Is Nothing Then Return

        If lblTitle IsNot Nothing Then
            lblTitle.Left = Math.Max(12, pnlHeader.ClientSize.Width - lblTitle.Width - 40)
            lblTitle.Top = 10
        End If

        If lblSubTitle IsNot Nothing Then
            lblSubTitle.Left = Math.Max(12, pnlHeader.ClientSize.Width - lblSubTitle.Width - 42)
            lblSubTitle.Top = 42
        End If
    End Sub

    Private Sub ArrangeAccountFormControls()
        If cardForm Is Nothing Then Return

        Dim margin As Integer = 16
        Dim gap As Integer = 8
        Dim labelW As Integer = 95
        Dim inputH As Integer = 25
        Dim rightEdge As Integer = cardForm.ClientSize.Width - margin
        Dim labelX As Integer = rightEdge - labelW
        Dim inputRight As Integer = labelX - 10
        Dim inputLeftLimit As Integer = Math.Max(margin, CInt(cardForm.ClientSize.Width * 0.42))
        Dim codeW As Integer = 115
        Dim pickW As Integer = 28
        Dim nameX As Integer = inputLeftLimit
        Dim codeX As Integer = inputRight - codeW
        Dim pickX As Integer = codeX - gap - pickW
        Dim nameW As Integer = Math.Max(180, pickX - gap - nameX)

        If lblAccount IsNot Nothing Then lblAccount.SetBounds(labelX, 25, labelW, 20)
        If txtAccountCode IsNot Nothing Then txtAccountCode.SetBounds(codeX, 23, codeW, inputH)
        If btnPickAccount IsNot Nothing Then btnPickAccount.SetBounds(pickX, 23, pickW, inputH)
        If txtAccountName IsNot Nothing Then txtAccountName.SetBounds(nameX, 23, nameW, inputH)

        If lblItem IsNot Nothing Then lblItem.SetBounds(labelX, 65, labelW, 20)
        If cmbItems IsNot Nothing Then cmbItems.SetBounds(nameX, 63, inputRight - nameX, inputH)
        If chkIsDefault IsNot Nothing Then chkIsDefault.SetBounds(nameX, 100, 180, 24)

        If lblSearch IsNot Nothing Then lblSearch.SetBounds(labelX, 137, labelW, 20)
        If txtSearch IsNot Nothing Then txtSearch.SetBounds(nameX, 135, inputRight - nameX, inputH)

        ArrangeStatsLabels(inputLeftLimit - 24)
    End Sub

    Private Sub ArrangeStatsLabels(maxRight As Integer)
        Dim margin As Integer = 16
        Dim gap As Integer = 10
        Dim statW As Integer = Math.Max(110, Math.Min(210, (Math.Max(360, maxRight) - margin - (gap * 2)) \ 3))
        Dim startX As Integer = margin

        If lblUnlinkedItemsStat IsNot Nothing Then lblUnlinkedItemsStat.SetBounds(startX, 14, statW, 34)
        If lblLinkedItemsStat IsNot Nothing Then lblLinkedItemsStat.SetBounds(startX + statW + gap, 14, statW, 34)
        If lblTotalItemsStat IsNot Nothing Then lblTotalItemsStat.SetBounds(startX + ((statW + gap) * 2), 14, statW, 34)
    End Sub

    Private Sub ArrangeActionButtons()
        If pnlActions Is Nothing Then Return

        Dim buttons() As Button = {btnNew, btnSave, btnDelete, btnRefresh, btnExit}
        Dim buttonW As Integer = 110
        Dim buttonH As Integer = 36
        Dim gap As Integer = 10
        Dim x As Integer = pnlActions.ClientSize.Width - 40 - buttonW
        Dim y As Integer = 10

        For Each btn As Button In buttons
            If btn Is Nothing Then Continue For
            btn.SetBounds(x, y, buttonW, buttonH)
            x -= buttonW + gap
        Next
    End Sub

    Private Sub FrmAccountBudgetMapping_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        ArrangeResponsiveLayout()
    End Sub

    Private Sub ApplyGridStyle()
        dgvMapping.EnableHeadersVisualStyles = False
        dgvMapping.ColumnHeadersHeight = 38
        dgvMapping.RowTemplate.Height = 34
        dgvMapping.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        dgvMapping.DefaultCellStyle.Font = New Font("Segoe UI", 10)
        ' dgvMapping.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 237, 255)
        dgvMapping.RowHeadersVisible = False
        dgvMapping.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    '=========================
    ' Load Data
    '=========================
    Private Sub LoadAccounts()
        Dim dt As New DataTable()

        '✅ نعرض الحسابات التحليلية فقط إن توفر عمود is_leaf
        'لو ما عندك is_leaf شغّال، احذف شرط where بكل بساطة
        Dim sql As String =
"SELECT
    T_ID AS AccountId,
    ACC_CODE,
    ACC_NAME,
    ACC_CODE + N' - ' + ACC_NAME AS AccountText
FROM ACCOUNTS_TREE
--WHERE (ISNULL(is_leaf, 1) = 1)
ORDER BY ACC_CODE;"

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand(sql, cn)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        cmbAccounts.DataSource = dt
        cmbAccounts.DisplayMember = "AccountText"
        cmbAccounts.ValueMember = "AccountId"
        cmbAccounts.SelectedIndex = -1
        txtAccountCode.Text = ""
        txtAccountName.Text = ""
    End Sub

    Private Sub LoadBudgetItems()
        Dim dt As New DataTable()

        Dim sql As String =
"SELECT
    i.BudgetItemId,
    (d.DoorCode + N'/' + c.ChapterCode + N'/' + i.ItemCode) + N' - ' + i.ItemName AS ItemText
FROM Budget_Items i
JOIN Budget_Chapters c ON i.ChapterId = c.ChapterId
JOIN Budget_Doors d ON c.DoorId = d.DoorId
WHERE i.IsActive = 1 AND c.IsActive = 1 AND d.IsActive = 1
ORDER BY d.DoorCode, c.ChapterCode, i.ItemCode;"

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand(sql, cn)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        cmbItems.DataSource = dt
        cmbItems.DisplayMember = "ItemText"
        cmbItems.ValueMember = "BudgetItemId"
        cmbItems.SelectedIndex = -1
    End Sub

    Private Sub LoadMappingGrid()
        MappingDt = New DataTable()

        Dim sql As String =
"SELECT
    abi.Id,
    a.T_ID AS AccountId,
    a.ACC_CODE,
    a.ACC_NAME,
    abi.BudgetItemId,
    d.DoorName,
    c.ChapterName,
    i.ItemCode,
    i.ItemName,
    ISNULL(abi.IsDefault, 0) AS IsDefault,
    N'مرتبط' AS LinkStatus
FROM Account_Budget_Items abi
JOIN ACCOUNTS_TREE a ON a.T_ID = abi.AccountId
JOIN Budget_Items i ON i.BudgetItemId = abi.BudgetItemId
JOIN Budget_Chapters c ON c.ChapterId = i.ChapterId
JOIN Budget_Doors d ON d.DoorId = c.DoorId
ORDER BY d.DoorCode, c.ChapterCode, i.ItemCode, a.ACC_CODE;"

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand(sql, cn)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(MappingDt)
                End Using
            End Using
        End Using

        MappingBs.DataSource = MappingDt
        dgvMapping.DataSource = MappingBs
        ApplyMappingFilter()

        If dgvMapping.Columns.Count > 0 Then
            dgvMapping.Columns("Id").Visible = False
            dgvMapping.Columns("AccountId").Visible = False
            dgvMapping.Columns("BudgetItemId").Visible = False

            dgvMapping.Columns("ACC_CODE").HeaderText = "كود الحساب"
            dgvMapping.Columns("ACC_NAME").HeaderText = "اسم الحساب"
            dgvMapping.Columns("DoorName").HeaderText = "الباب"
            dgvMapping.Columns("ChapterName").HeaderText = "الفصل"
            dgvMapping.Columns("ItemCode").HeaderText = "كود البند"
            dgvMapping.Columns("ItemName").HeaderText = "اسم البند"
            dgvMapping.Columns("IsDefault").HeaderText = "افتراضي"
            dgvMapping.Columns("LinkStatus").HeaderText = "حالة الربط"
        End If

        LoadBudgetItemsStats()
        dgvMapping.ClearSelection()
        SetStatus($"تم تحميل {MappingDt.Rows.Count} ربط")
    End Sub

    Private Sub LoadBudgetItemsStats()
        Dim totalItems As Integer = 0
        Dim linkedItems As Integer = 0

        Using cn As New SqlConnection(ConnStr)
            cn.Open()

            Using cmd As New SqlCommand("
SELECT COUNT(1)
FROM Budget_Items i
JOIN Budget_Chapters c ON i.ChapterId = c.ChapterId
JOIN Budget_Doors d ON c.DoorId = d.DoorId
WHERE i.IsActive = 1 AND c.IsActive = 1 AND d.IsActive = 1;", cn)
                totalItems = Convert.ToInt32(cmd.ExecuteScalar())
            End Using

            Using cmd As New SqlCommand("
SELECT COUNT(DISTINCT abi.BudgetItemId)
FROM Account_Budget_Items abi
JOIN Budget_Items i ON i.BudgetItemId = abi.BudgetItemId
JOIN Budget_Chapters c ON i.ChapterId = c.ChapterId
JOIN Budget_Doors d ON c.DoorId = d.DoorId
WHERE i.IsActive = 1 AND c.IsActive = 1 AND d.IsActive = 1;", cn)
                linkedItems = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using

        Dim unlinkedItems As Integer = Math.Max(0, totalItems - linkedItems)
        lblTotalItemsStat.Text = "إجمالي البنود: " & totalItems.ToString()
        lblLinkedItemsStat.Text = "بنود مرتبطة: " & linkedItems.ToString()
        lblUnlinkedItemsStat.Text = "بنود غير مرتبطة: " & unlinkedItems.ToString()
    End Sub

    Private Sub ApplyMappingFilter()
        If MappingBs Is Nothing OrElse MappingDt Is Nothing Then Exit Sub

        Dim text As String = If(txtSearch Is Nothing, "", txtSearch.Text.Trim())
        If String.IsNullOrWhiteSpace(text) Then
            MappingBs.Filter = ""
            Exit Sub
        End If

        Dim safeText As String = text.Replace("'", "''")
        MappingBs.Filter =
            "ACC_CODE LIKE '%" & safeText & "%' OR " &
            "ACC_NAME LIKE '%" & safeText & "%' OR " &
            "DoorName LIKE '%" & safeText & "%' OR " &
            "ChapterName LIKE '%" & safeText & "%' OR " &
            "ItemCode LIKE '%" & safeText & "%' OR " &
            "ItemName LIKE '%" & safeText & "%' OR " &
            "LinkStatus LIKE '%" & safeText & "%'"
    End Sub

    '=========================
    ' Helpers
    '=========================
    Private Sub ClearForm()
        CurrentMappingId = 0
        cmbAccounts.SelectedIndex = -1
        txtAccountCode.Text = ""
        txtAccountName.Text = ""
        cmbItems.SelectedIndex = -1
        chkIsDefault.Checked = False
    End Sub

    Private Function PickAccountFromBalanceSearch() As String
        ACC_CODE_Search = ""
        ACC_NAME_Search = ""

        BALANCE_SEARCH.ShowDialog()

        If String.IsNullOrWhiteSpace(ACC_CODE_Search) Then Return ""
        Return ACC_CODE_Search.Trim()
    End Function

    Private Function SetSelectedAccountByCode(accCode As String, Optional showMessage As Boolean = False) As Boolean
        If String.IsNullOrWhiteSpace(accCode) Then
            cmbAccounts.SelectedIndex = -1
            txtAccountName.Text = ""
            Return False
        End If

        Dim dt As DataTable = TryCast(cmbAccounts.DataSource, DataTable)
        If dt Is Nothing Then Return False

        Dim safeCode As String = accCode.Trim().Replace("'", "''")
        Dim rows As DataRow() = dt.Select("ACC_CODE = '" & safeCode & "'")

        If rows.Length = 0 Then
            cmbAccounts.SelectedIndex = -1
            txtAccountName.Text = ""
            If showMessage Then MessageBox.Show("الحساب غير موجود في شجرة الحسابات", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        cmbAccounts.SelectedValue = Convert.ToInt32(rows(0)("AccountId"))
        txtAccountCode.Text = rows(0)("ACC_CODE").ToString()
        txtAccountName.Text = rows(0)("ACC_NAME").ToString()
        Return True
    End Function

    Private Sub SetSelectedAccountById(accountId As Integer)
        cmbAccounts.SelectedValue = accountId

        Dim rowView As DataRowView = TryCast(cmbAccounts.SelectedItem, DataRowView)
        If rowView Is Nothing Then
            txtAccountCode.Text = ""
            txtAccountName.Text = ""
            Return
        End If

        txtAccountCode.Text = rowView("ACC_CODE").ToString()
        txtAccountName.Text = rowView("ACC_NAME").ToString()
    End Sub

    Private Function ResolveAccountIdByCode(accountCode As String, Optional updateDisplay As Boolean = True) As Integer
        If String.IsNullOrWhiteSpace(accountCode) Then Return 0

        Using cn As New SqlConnection(ConnStr)
            cn.Open()
            Using cmd As New SqlCommand("
SELECT TOP 1 T_ID, ACC_CODE, ACC_NAME
FROM dbo.ACCOUNTS_TREE
WHERE LTRIM(RTRIM(CONVERT(NVARCHAR(40), ACC_CODE))) = LTRIM(RTRIM(@ACC_CODE));", cn)

                cmd.Parameters.Add("@ACC_CODE", SqlDbType.NVarChar, 40).Value = accountCode.Trim()

                Using rd As SqlDataReader = cmd.ExecuteReader()
                    If Not rd.Read() Then Return 0

                    Dim accountId As Integer = Convert.ToInt32(rd("T_ID"))
                    If updateDisplay Then
                        txtAccountCode.Text = rd("ACC_CODE").ToString()
                        txtAccountName.Text = rd("ACC_NAME").ToString()
                        If cmbAccounts IsNot Nothing AndAlso cmbAccounts.DataSource IsNot Nothing Then
                            cmbAccounts.SelectedValue = accountId
                        End If
                    End If

                    Return accountId
                End Using
            End Using
        End Using
    End Function

    Private Function GetSelectedAccountId() As Integer
        Dim resolvedAccountId As Integer = ResolveAccountIdByCode(txtAccountCode.Text)
        If resolvedAccountId > 0 Then Return resolvedAccountId

        If cmbAccounts.SelectedIndex < 0 OrElse cmbAccounts.SelectedValue Is Nothing Then Return 0

        Dim accountId As Integer = 0
        Integer.TryParse(cmbAccounts.SelectedValue.ToString(), accountId)
        Return accountId
    End Function

    Private Function ValidateForm() As Boolean
        If ResolveAccountIdByCode(txtAccountCode.Text) <= 0 Then
            MessageBox.Show("اختر الحساب أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtAccountCode.Focus()
            Return False
        End If

        If cmbItems.SelectedIndex < 0 Then
            MessageBox.Show("اختر بند الموازنة أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbItems.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function MappingExists(accountId As Integer, budgetItemId As Integer, excludeId As Integer) As Boolean
        Using cn As New SqlConnection(ConnStr)
            cn.Open()
            Using cmd As New SqlCommand("
SELECT COUNT(1)
FROM Account_Budget_Items
WHERE AccountId = @AccountId
  AND BudgetItemId = @BudgetItemId
  AND Id <> @ExcludeId;", cn)

                cmd.Parameters.AddWithValue("@AccountId", accountId)
                cmd.Parameters.AddWithValue("@BudgetItemId", budgetItemId)
                cmd.Parameters.AddWithValue("@ExcludeId", excludeId)

                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    '=========================
    ' Save / Delete
    '=========================
    Private Sub SaveMapping()
        If Not ValidateForm() Then Exit Sub

        Dim accountId As Integer = GetSelectedAccountId()
        Dim budgetItemId As Integer = Convert.ToInt32(cmbItems.SelectedValue)
        Dim isDef As Integer = If(chkIsDefault.Checked, 1, 0)

        If MappingExists(accountId, budgetItemId, CurrentMappingId) Then
            MessageBox.Show("هذا الربط موجود مسبقًا.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim refNo = NewRefNo()
        SetUserContext(refNo)

        Using cn As New SqlConnection(ConnStr)
            cn.Open()

            '✅ قاعدة: Default واحد فقط لكل حساب
            If isDef = 1 Then
                Using cmdReset As New SqlCommand("
UPDATE Account_Budget_Items
SET IsDefault = 0
WHERE AccountId = @AccountId;", cn)
                    cmdReset.Parameters.AddWithValue("@AccountId", accountId)
                    cmdReset.ExecuteNonQuery()
                End Using
            End If

            If CurrentMappingId = 0 Then
                Using cmd As New SqlCommand("
INSERT INTO Account_Budget_Items (AccountId, BudgetItemId, IsDefault)
VALUES (@AccountId, @BudgetItemId, @IsDefault);", cn)

                    cmd.Parameters.AddWithValue("@AccountId", accountId)
                    cmd.Parameters.AddWithValue("@BudgetItemId", budgetItemId)
                    cmd.Parameters.AddWithValue("@IsDefault", isDef)
                    cmd.ExecuteNonQuery()
                End Using

                SetStatus("تم حفظ الربط")
            Else
                Using cmd As New SqlCommand("
UPDATE Account_Budget_Items
SET AccountId = @AccountId,
    BudgetItemId = @BudgetItemId,
    IsDefault = @IsDefault
WHERE Id = @Id;", cn)

                    cmd.Parameters.AddWithValue("@AccountId", accountId)
                    cmd.Parameters.AddWithValue("@BudgetItemId", budgetItemId)
                    cmd.Parameters.AddWithValue("@IsDefault", isDef)
                    cmd.Parameters.AddWithValue("@Id", CurrentMappingId)
                    cmd.ExecuteNonQuery()
                End Using

                SetStatus("تم تحديث الربط")
            End If
        End Using

        LoadMappingGrid()
        ClearForm()
    End Sub

    Private Sub DeleteMapping()
        If CurrentMappingId = 0 Then
            MessageBox.Show("اختر صفًا من القائمة أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("هل تريد حذف هذا الربط؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Dim refNo = NewRefNo()
        SetUserContext(refNo)

        Using cn As New SqlConnection(ConnStr)
            cn.Open()
            Using cmd As New SqlCommand("DELETE FROM Account_Budget_Items WHERE Id = @Id;", cn)
                cmd.Parameters.AddWithValue("@Id", CurrentMappingId)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        SetStatus("تم حذف الربط")
        LoadMappingGrid()
        ClearForm()
    End Sub

    '=========================
    ' UI Helpers
    '=========================
    Private Sub SetStatus(msg As String)
        lblStatus.Text = msg
    End Sub

    Private Sub FillFromGrid()
        If dgvMapping.CurrentRow Is Nothing Then Exit Sub

        Dim accountId As Integer = Convert.ToInt32(dgvMapping.CurrentRow.Cells("AccountId").Value)
        Dim idValue = dgvMapping.CurrentRow.Cells("Id").Value
        Dim itemValue = dgvMapping.CurrentRow.Cells("BudgetItemId").Value

        If idValue Is Nothing OrElse IsDBNull(idValue) Then
            CurrentMappingId = 0
        Else
            CurrentMappingId = Convert.ToInt32(idValue)
        End If

        SetSelectedAccountById(accountId)

        Dim itemId As Integer = Convert.ToInt32(itemValue)
        cmbItems.SelectedValue = itemId

        chkIsDefault.Checked = Convert.ToBoolean(dgvMapping.CurrentRow.Cells("IsDefault").Value)

        SetStatus("وضع التعديل")
    End Sub

    '=========================
    ' Events
    '=========================
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        ClearForm()
        SetStatus("جديد")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            SaveMapping()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SetStatus("فشل الحفظ")
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            DeleteMapping()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SetStatus("فشل الحذف")
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            LoadAccounts()
            LoadBudgetItems()
            LoadMappingGrid()
            SetStatus("تم التحديث")
        Catch ex As Exception
            SetStatus("خطأ: " & ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub dgvMapping_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMapping.CellClick
        FillFromGrid()
    End Sub

    Private Sub dgvMapping_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMapping.CellDoubleClick
        FillFromGrid()
    End Sub

    'فلترة اختيارية: عند اختيار حساب نعرض روابطه فقط
    Private Sub cmbAccounts_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbAccounts.SelectionChangeCommitted
        Try
            Dim accId As Integer = GetSelectedAccountId()
            If accId > 0 Then SetSelectedAccountById(accId)
        Catch
        End Try
    End Sub

    Private Sub btnPickAccount_Click(sender As Object, e As EventArgs) Handles btnPickAccount.Click
        Dim accountCode As String = PickAccountFromBalanceSearch()
        If Not String.IsNullOrWhiteSpace(accountCode) Then
            SetSelectedAccountByCode(accountCode, True)
        End If
    End Sub

    Private Sub txtAccountCode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAccountCode.KeyDown
        If e.KeyCode = Keys.Enter Then
            SetSelectedAccountByCode(txtAccountCode.Text, True)
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtAccountCode_Leave(sender As Object, e As EventArgs) Handles txtAccountCode.Leave
        If String.IsNullOrWhiteSpace(txtAccountCode.Text) Then
            cmbAccounts.SelectedIndex = -1
            txtAccountName.Text = ""
            Exit Sub
        End If

        SetSelectedAccountByCode(txtAccountCode.Text, False)
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        ApplyMappingFilter()
        SetStatus("نتائج البحث: " & MappingBs.Count.ToString())
    End Sub

End Class
