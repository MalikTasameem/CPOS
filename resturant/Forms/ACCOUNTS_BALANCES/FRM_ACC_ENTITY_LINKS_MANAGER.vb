Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms

Public Class FRM_ACC_ENTITY_LINKS_MANAGER

#Region "Fields"

    ' غيّر هذا حسب متغير الاتصال الموجود في مشروعك
    Private ReadOnly ConStr As String = MY_Settings.SqlConStr

    Private _dsDashboard As DataSet
    Private _isLoading As Boolean = False

#End Region

#Region "Form Events"

    Private Sub FRM_ACC_ENTITY_LINKS_MANAGER_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _isLoading = True

            InitEntityTypes()
            InitMovementFilter()
            InitGrids()

            _isLoading = False

            LoadDashboard()

        Catch ex As Exception
            _isLoading = False
            ShowError(ex)
        End Try
    End Sub

#End Region

#Region "Initialization"

    Private Function GetConnection() As SqlConnection
        Return New SqlConnection(ConStr)
    End Function

    Private Sub InitEntityTypes()
        Dim dt As New DataTable()
        dt.Columns.Add("ID", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))

        dt.Rows.Add(DBNull.Value, "الكل")
        dt.Rows.Add(1, "حسابات عامة")
        dt.Rows.Add(2, "زبائن")
        dt.Rows.Add(3, "موردين")
        dt.Rows.Add(4, "موظفين")
        dt.Rows.Add(5, "مخازن")
        dt.Rows.Add(6, "خزائن")
        dt.Rows.Add(7, "مصروفات عامة")
        dt.Rows.Add(8, "مصروفات مشتريات")
        dt.Rows.Add(9, "مصارف")

        cmbEntityType.DataSource = dt
        cmbEntityType.DisplayMember = "Name"
        cmbEntityType.ValueMember = "ID"
        cmbEntityType.SelectedIndex = 0
    End Sub

    Private Sub InitMovementFilter()
        Dim dt As New DataTable()
        dt.Columns.Add("ID", GetType(Object))
        dt.Columns.Add("Name", GetType(String))

        dt.Rows.Add(DBNull.Value, "الكل")
        dt.Rows.Add(1, "عليها حركة")
        dt.Rows.Add(0, "بدون حركة")

        cmbMovement.DataSource = dt
        cmbMovement.DisplayMember = "Name"
        cmbMovement.ValueMember = "ID"
        cmbMovement.SelectedIndex = 0
    End Sub

    Private Sub InitGrids()
        PrepareGrid(dgvDetails)
        PrepareGrid(dgvSummary)
        PrepareGrid(dgvDuplicates)
        PrepareGrid(dgvRulesIssues)
        PrepareGrid(dgvLinksIssues)
    End Sub

    Private Sub PrepareGrid(dgv As DataGridView)
        dgv.AutoGenerateColumns = True
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.ReadOnly = True
        dgv.MultiSelect = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.RowHeadersVisible = False
        dgv.BackgroundColor = Color.White
        dgv.BorderStyle = BorderStyle.None
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
        dgv.EnableHeadersVisualStyles = False
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(238, 238, 238)
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        dgv.DefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250)
    End Sub

#End Region

#Region "Load Dashboard"

    Private Sub LoadDashboard()
        Try
            lblStatus.Text = "جاري تحميل بيانات الربط المحاسبي..."
            Application.DoEvents()

            Using con As SqlConnection = GetConnection()
                Using cmd As New SqlCommand("dbo.ACC_ENTITY_LINKS_DASHBOARD", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 120

                    cmd.Parameters.Add("@ENTITY_TYPE", SqlDbType.TinyInt).Value = GetSelectedEntityType()
                    cmd.Parameters.Add("@ONLY_ISSUES", SqlDbType.Bit).Value = chkOnlyIssues.Checked
                    cmd.Parameters.Add("@HAS_MOVEMENT", SqlDbType.Bit).Value = GetSelectedMovement()
                    cmd.Parameters.Add("@SEARCH", SqlDbType.NVarChar, 200).Value = GetSearchValue()

                    Using da As New SqlDataAdapter(cmd)
                        _dsDashboard = New DataSet()
                        da.Fill(_dsDashboard)
                    End Using
                End Using
            End Using

            BindDashboard()
            lblStatus.Text = "تم تحميل البيانات بنجاح"

        Catch ex As Exception
            lblStatus.Text = "حدث خطأ أثناء التحميل"
            ShowError(ex)
        End Try
    End Sub

    Private Function GetSelectedEntityType() As Object
        If cmbEntityType.SelectedValue Is Nothing OrElse IsDBNull(cmbEntityType.SelectedValue) Then
            Return DBNull.Value
        End If

        Return Convert.ToByte(cmbEntityType.SelectedValue)
    End Function

    Private Function GetSelectedMovement() As Object
        If cmbMovement.SelectedValue Is Nothing OrElse IsDBNull(cmbMovement.SelectedValue) Then
            Return DBNull.Value
        End If

        Return Convert.ToBoolean(Convert.ToInt32(cmbMovement.SelectedValue))
    End Function

    Private Function GetSearchValue() As Object
        Dim s As String = txtSearch.Text.Trim()

        If String.IsNullOrWhiteSpace(s) Then
            Return DBNull.Value
        End If

        Return s
    End Function

    Private Sub BindDashboard()
        If _dsDashboard Is Nothing Then Return

        If _dsDashboard.Tables.Count > 0 Then
            dgvSummary.DataSource = _dsDashboard.Tables(0)
            FormatSummaryGrid()
        End If

        If _dsDashboard.Tables.Count > 1 Then
            BindCards(_dsDashboard.Tables(1))
        End If

        If _dsDashboard.Tables.Count > 2 Then
            dgvDetails.DataSource = _dsDashboard.Tables(2)
            FormatDetailsGrid()
        End If

        If _dsDashboard.Tables.Count > 3 Then
            dgvDuplicates.DataSource = _dsDashboard.Tables(3)
            FormatSimpleGrid(dgvDuplicates)
        End If

        If _dsDashboard.Tables.Count > 4 Then
            dgvRulesIssues.DataSource = _dsDashboard.Tables(4)
            FormatSimpleGrid(dgvRulesIssues)
        End If

        If _dsDashboard.Tables.Count > 5 Then
            dgvLinksIssues.DataSource = _dsDashboard.Tables(5)
            FormatSimpleGrid(dgvLinksIssues)
        End If


        UpdateSelectedInfo()
        UpdateActionButtonsState()

    End Sub

    Private Sub BindCards(dt As DataTable)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            lblTotalValue.Text = "0"
            lblOKValue.Text = "0"
            lblIssuesValue.Text = "0"
            lblMissingValue.Text = "0"
            lblMovementValue.Text = "0"
            Return
        End If

        Dim r As DataRow = dt.Rows(0)

        lblTotalValue.Text = ToText(r, "TotalEntities")
        lblOKValue.Text = ToText(r, "TotalOK")
        lblIssuesValue.Text = ToText(r, "TotalIssues")
        lblMissingValue.Text = ToText(r, "TotalMissingAccountCode")
        lblMovementValue.Text = ToText(r, "TotalHasMovement")
    End Sub

    Private Function ToText(row As DataRow, col As String) As String
        If row.Table.Columns.Contains(col) = False Then Return "0"
        If row(col) Is DBNull.Value Then Return "0"
        Return Convert.ToString(row(col))
    End Function

#End Region

#Region "Formatting"

    Private Sub FormatSummaryGrid()
        RenameColumn(dgvSummary, "ENTITY_TYPE", "رقم النوع")
        RenameColumn(dgvSummary, "ENTITY_NAME_AR", "النوع")
        RenameColumn(dgvSummary, "TotalEntities", "الإجمالي")
        RenameColumn(dgvSummary, "TotalWithAccountCode", "لديها حساب")
        RenameColumn(dgvSummary, "TotalWithLink", "لديها رابط")
        RenameColumn(dgvSummary, "TotalMissingAccountCode", "بدون حساب")
        RenameColumn(dgvSummary, "TotalAccountNotFoundInTree", "حساب غير موجود")
        RenameColumn(dgvSummary, "TotalMissingLink", "رابط ناقص")
        RenameColumn(dgvSummary, "TotalDifferentLinkCode", "اختلاف الرابط")
        RenameColumn(dgvSummary, "TotalHasMovement", "عليها حركة")
        RenameColumn(dgvSummary, "TotalOK", "سليم")
        RenameColumn(dgvSummary, "TotalIssues", "مشاكل")
        RenameColumn(dgvSummary, "OK_Percent", "نسبة السلامة")
        RenameColumn(dgvSummary, "SummaryStatus", "الحالة")
    End Sub

    Private Sub FormatDetailsGrid()
        RenameColumn(dgvDetails, "ENTITY_TYPE", "رقم النوع")
        RenameColumn(dgvDetails, "ENTITY_NAME_AR", "النوع")
        RenameColumn(dgvDetails, "SOURCE_TABLE", "الجدول")
        RenameColumn(dgvDetails, "SOURCE_ID", "رقم الكيان")
        RenameColumn(dgvDetails, "ENTITY_NAME", "الاسم")
        RenameColumn(dgvDetails, "SOURCE_ACC_CODE", "حساب الجدول")
        RenameColumn(dgvDetails, "LINK_ID", "رقم الرابط")
        RenameColumn(dgvDetails, "LINK_ACC_CODE", "حساب الرابط")
        RenameColumn(dgvDetails, "PARENT_ACC_CODE", "الحساب الأب")
        RenameColumn(dgvDetails, "ACC_NAME", "اسم الحساب")
        RenameColumn(dgvDetails, "ACC_PARENT", "الأب في الدليل")
        RenameColumn(dgvDetails, "ACC_LEVEL", "المستوى")
        RenameColumn(dgvDetails, "ACC_NATURAL", "الطبيعة")
        RenameColumn(dgvDetails, "IS_MISSING_ACC_CODE", "بدون حساب")
        RenameColumn(dgvDetails, "IS_ACC_NOT_FOUND_IN_TREE", "غير موجود بالدليل")
        RenameColumn(dgvDetails, "IS_MISSING_LINK", "رابط ناقص")
        RenameColumn(dgvDetails, "IS_LINK_CODE_DIFFERENT", "اختلاف الرابط")
        RenameColumn(dgvDetails, "HAS_MOVEMENT", "عليه حركة")
        RenameColumn(dgvDetails, "LINK_STATUS", "الحالة")
        RenameColumn(dgvDetails, "SuggestedAction", "الإجراء المقترح")

        ColorDetailsRows()
    End Sub

    Private Sub FormatSimpleGrid(dgv As DataGridView)
        For Each col As DataGridViewColumn In dgv.Columns
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        Next
    End Sub

    Private Sub RenameColumn(dgv As DataGridView, columnName As String, headerText As String)
        If dgv.Columns.Contains(columnName) Then
            dgv.Columns(columnName).HeaderText = headerText
        End If
    End Sub

    Private Sub ColorDetailsRows()
        For Each row As DataGridViewRow In dgvDetails.Rows
            If row.IsNewRow Then Continue For

            Dim hasIssue As Boolean =
                GetBoolCell(row, "IS_MISSING_ACC_CODE") OrElse
                GetBoolCell(row, "IS_ACC_NOT_FOUND_IN_TREE") OrElse
                GetBoolCell(row, "IS_MISSING_LINK") OrElse
                GetBoolCell(row, "IS_LINK_CODE_DIFFERENT")

            Dim hasMovement As Boolean = GetBoolCell(row, "HAS_MOVEMENT")

            If hasIssue Then
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 224)
            Else
                row.DefaultCellStyle.BackColor = Color.White
            End If

            If hasMovement Then
                row.DefaultCellStyle.ForeColor = Color.FromArgb(120, 40, 40)
            End If
        Next
    End Sub

    Private Function GetBoolCell(row As DataGridViewRow, colName As String) As Boolean
        If dgvDetails.Columns.Contains(colName) = False Then Return False
        Dim v = row.Cells(colName).Value
        If v Is Nothing OrElse v Is DBNull.Value Then Return False
        Return Convert.ToBoolean(v)
    End Function

#End Region

#Region "Selection Helpers"

    Private Function HasSelectedDetailRow() As Boolean
        Return dgvDetails.CurrentRow IsNot Nothing AndAlso dgvDetails.CurrentRow.IsNewRow = False
    End Function

    Private Function GetSelectedEntityTypeFromGrid() As Integer
        If Not HasSelectedDetailRow() Then Throw New Exception("يرجى تحديد سجل من تفاصيل الربط.")
        Return Convert.ToInt32(dgvDetails.CurrentRow.Cells("ENTITY_TYPE").Value)
    End Function

    Private Function GetSelectedSourceIdFromGrid() As Long
        If Not HasSelectedDetailRow() Then Throw New Exception("يرجى تحديد سجل من تفاصيل الربط.")
        Return Convert.ToInt64(dgvDetails.CurrentRow.Cells("SOURCE_ID").Value)
    End Function

    Private Function GetSelectedEntityNameFromGrid() As String
        If Not HasSelectedDetailRow() Then Return ""
        If dgvDetails.Columns.Contains("ENTITY_NAME") = False Then Return ""
        Return Convert.ToString(dgvDetails.CurrentRow.Cells("ENTITY_NAME").Value)
    End Function

    Private Function GetSelectedAccCodeFromGrid() As String
        If Not HasSelectedDetailRow() Then Return ""

        If dgvDetails.Columns.Contains("SOURCE_ACC_CODE") Then
            Dim v = dgvDetails.CurrentRow.Cells("SOURCE_ACC_CODE").Value
            If v IsNot Nothing AndAlso v IsNot DBNull.Value Then
                Return Convert.ToString(v).Trim()
            End If
        End If

        Return ""
    End Function

    'Private Sub UpdateSelectedInfo()
    '    If HasSelectedDetailRow() Then
    '        lblSelectedInfo.Text = "المحدد: " & GetSelectedEntityNameFromGrid()
    '    Else
    '        lblSelectedInfo.Text = "لا يوجد تحديد"
    '    End If
    'End Sub

#End Region

#Region "Button Events"

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadDashboard()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnCreateMissingAccount_Click(sender As Object, e As EventArgs) Handles btnCreateMissingAccount.Click
        Try
            Dim entityType = GetSelectedEntityTypeFromGrid()
            Dim sourceId = GetSelectedSourceIdFromGrid()

            If MessageBox.Show("هل تريد فتح/مزامنة الحساب المحاسبي للسجل المحدد؟",
                               "تأكيد",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question,
                               MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then
                Return
            End If

            CreateOrSyncAccount(entityType, sourceId)
            LoadDashboard()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnRepairLink_Click(sender As Object, e As EventArgs) Handles btnRepairLink.Click
        Try
            Dim entityType = GetSelectedEntityTypeFromGrid()
            Dim sourceId = GetSelectedSourceIdFromGrid()

            If MessageBox.Show("هل تريد إصلاح الرابط المحاسبي للسجل المحدد؟",
                               "تأكيد",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question,
                               MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then
                Return
            End If

            RepairLink(entityType, sourceId)
            LoadDashboard()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnChangeAccount_Click(sender As Object, e As EventArgs) Handles btnChangeAccount.Click
        Try
            Dim entityType = GetSelectedEntityTypeFromGrid()
            Dim sourceId = GetSelectedSourceIdFromGrid()



            Using frm As New FRM_SELECT_ACCOUNT(GetSelectedAccCodeFromGrid())
                If frm.ShowDialog(Me) <> DialogResult.OK Then
                    Return
                End If

                Dim newAccCode As String = frm.SelectedAccCode
                Dim newAccName As String = frm.SelectedAccName

                If String.IsNullOrWhiteSpace(newAccCode) Then Return

                If MessageBox.Show("هل تريد تغيير الحساب المرتبط إلى:" &
                                   Environment.NewLine &
                                   newAccCode & " - " & newAccName,
                                   "تأكيد تغيير الحساب",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Warning,
                                   MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then
                    Return
                End If

                ChangeAccount(entityType, sourceId, newAccCode)
                LoadDashboard()
            End Using


        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnLockLink_Click(sender As Object, e As EventArgs) Handles btnLockLink.Click
        Try
            Dim entityType = GetSelectedEntityTypeFromGrid()
            Dim sourceId = GetSelectedSourceIdFromGrid()

            LockLink(entityType, sourceId)
            LoadDashboard()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnUnlockLink_Click(sender As Object, e As EventArgs) Handles btnUnlockLink.Click
        Try
            Dim entityType = GetSelectedEntityTypeFromGrid()
            Dim sourceId = GetSelectedSourceIdFromGrid()

            UnlockLink(entityType, sourceId)
            LoadDashboard()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnValidateOne_Click(sender As Object, e As EventArgs) Handles btnValidateOne.Click
        Try
            Dim entityType = GetSelectedEntityTypeFromGrid()
            Dim sourceId = GetSelectedSourceIdFromGrid()

            ValidateOne(entityType, sourceId)

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnOpenAccount_Click(sender As Object, e As EventArgs) Handles btnOpenAccount.Click
        Try
            Dim accCode As String = GetSelectedAccCodeFromGrid()

            If String.IsNullOrWhiteSpace(accCode) Then
                MessageBox.Show("السجل المحدد لا يحتوي على رقم حساب.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            MessageBox.Show("رقم الحساب: " & accCode,
                            "عرض الحساب",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)

            ' لاحقًا يمكن فتح شاشة الدليل هنا:
            ' Dim frm As New FRM_ACCOUNTS_TREE(accCode)
            ' frm.ShowDialog()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

#End Region

#Region "Filter Events"

    Private Sub cmbEntityType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEntityType.SelectedIndexChanged
        If _isLoading Then Return
        LoadDashboard()
    End Sub

    Private Sub cmbMovement_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMovement.SelectedIndexChanged
        If _isLoading Then Return
        LoadDashboard()
    End Sub

    Private Sub chkOnlyIssues_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnlyIssues.CheckedChanged
        If _isLoading Then Return
        LoadDashboard()
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            LoadDashboard()
        End If
    End Sub

    Private Sub dgvDetails_SelectionChanged(sender As Object, e As EventArgs) Handles dgvDetails.SelectionChanged
        UpdateSelectedInfo()
    End Sub

#End Region

#Region "Details Context Menu"

    Private Sub dgvDetails_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvDetails.CellMouseDown
        If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            dgvDetails.ClearSelection()
            dgvDetails.Rows(e.RowIndex).Selected = True
            dgvDetails.CurrentCell = dgvDetails.Rows(e.RowIndex).Cells(e.ColumnIndex)
            UpdateSelectedInfo()
        End If
    End Sub

    Private Sub cmsDetails_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsDetails.Opening
        UpdateContextMenuState()

        If Not HasSelectedDetailRow() Then
            e.Cancel = True
        End If
    End Sub

    Private Sub mnuCreateMissingAccount_Click(sender As Object, e As EventArgs) Handles mnuCreateMissingAccount.Click
        btnCreateMissingAccount.PerformClick()
    End Sub

    Private Sub mnuRepairLink_Click(sender As Object, e As EventArgs) Handles mnuRepairLink.Click
        btnRepairLink.PerformClick()
    End Sub

    Private Sub mnuChangeAccount_Click(sender As Object, e As EventArgs) Handles mnuChangeAccount.Click
        btnChangeAccount.PerformClick()
    End Sub

    Private Sub mnuLockLink_Click(sender As Object, e As EventArgs) Handles mnuLockLink.Click
        btnLockLink.PerformClick()
    End Sub

    Private Sub mnuUnlockLink_Click(sender As Object, e As EventArgs) Handles mnuUnlockLink.Click
        btnUnlockLink.PerformClick()
    End Sub

    Private Sub mnuValidateOne_Click(sender As Object, e As EventArgs) Handles mnuValidateOne.Click
        btnValidateOne.PerformClick()
    End Sub

    Private Sub mnuOpenAccount_Click(sender As Object, e As EventArgs) Handles mnuOpenAccount.Click
        btnOpenAccount.PerformClick()
    End Sub

#End Region

#Region "Database Actions"

    Private Sub CreateOrSyncAccount(entityType As Integer, sourceId As Long)
        Using con As SqlConnection = GetConnection()
            Using cmd As New SqlCommand("dbo.ACC_ENTITY_CREATE_OR_SYNC_ACCOUNT", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 120

                cmd.Parameters.Add("@ENTITY_TYPE", SqlDbType.TinyInt).Value = entityType
                cmd.Parameters.Add("@SOURCE_ID", SqlDbType.BigInt).Value = sourceId
                cmd.Parameters.Add("@USER_ID", SqlDbType.Int).Value = GetCurrentUserId()
                cmd.Parameters.Add("@SYNC_ACCOUNT_NAME", SqlDbType.Bit).Value = True
                cmd.Parameters.Add("@CHILD_DIGITS", SqlDbType.Int).Value = 3

                Dim pAcc = cmd.Parameters.Add("@OUT_ACC_CODE", SqlDbType.VarChar, 50)
                pAcc.Direction = ParameterDirection.Output

                Dim pLink = cmd.Parameters.Add("@OUT_LINK_ID", SqlDbType.Int)
                pLink.Direction = ParameterDirection.Output

                con.Open()
                cmd.ExecuteNonQuery()

                MessageBox.Show("تم فتح/مزامنة الحساب بنجاح." & Environment.NewLine &
                                "رقم الحساب: " & Convert.ToString(pAcc.Value),
                                "تم",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
            End Using
        End Using
    End Sub

    Private Sub RepairLink(entityType As Integer, sourceId As Long)
        Using con As SqlConnection = GetConnection()
            Using cmd As New SqlCommand("dbo.ACC_ENTITY_REPAIR_LINK", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 120

                cmd.Parameters.Add("@ENTITY_TYPE", SqlDbType.TinyInt).Value = entityType
                cmd.Parameters.Add("@SOURCE_ID", SqlDbType.BigInt).Value = sourceId
                cmd.Parameters.Add("@USER_ID", SqlDbType.Int).Value = GetCurrentUserId()

                Dim pLink = cmd.Parameters.Add("@OUT_LINK_ID", SqlDbType.Int)
                pLink.Direction = ParameterDirection.Output

                con.Open()
                cmd.ExecuteNonQuery()

                MessageBox.Show("تم إصلاح الرابط بنجاح." & Environment.NewLine &
                                "رقم الرابط: " & Convert.ToString(pLink.Value),
                                "تم",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
            End Using
        End Using
    End Sub

    Private Sub ChangeAccount(entityType As Integer, sourceId As Long, newAccCode As String)
        Using con As SqlConnection = GetConnection()
            Using cmd As New SqlCommand("dbo.ACC_ENTITY_CHANGE_ACCOUNT", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 120

                cmd.Parameters.Add("@ENTITY_TYPE", SqlDbType.TinyInt).Value = entityType
                cmd.Parameters.Add("@SOURCE_ID", SqlDbType.BigInt).Value = sourceId
                cmd.Parameters.Add("@NEW_ACC_CODE", SqlDbType.VarChar, 50).Value = newAccCode
                cmd.Parameters.Add("@USER_ID", SqlDbType.Int).Value = GetCurrentUserId()
                cmd.Parameters.Add("@FORCE_IF_NO_MOVE", SqlDbType.Bit).Value = False

                con.Open()
                cmd.ExecuteNonQuery()

                MessageBox.Show("تم تغيير الحساب المرتبط بنجاح.",
                                "تم",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
            End Using
        End Using
    End Sub

    Private Sub LockLink(entityType As Integer, sourceId As Long)
        ExecuteSimpleEntityProcedure("dbo.ACC_ENTITY_LOCK_LINK", entityType, sourceId, "تم قفل الرابط بنجاح.")
    End Sub

    Private Sub UnlockLink(entityType As Integer, sourceId As Long)
        ExecuteSimpleEntityProcedure("dbo.ACC_ENTITY_UNLOCK_LINK", entityType, sourceId, "تم فك قفل الرابط بنجاح.")
    End Sub

    Private Sub ExecuteSimpleEntityProcedure(procName As String, entityType As Integer, sourceId As Long, successMessage As String)
        Using con As SqlConnection = GetConnection()
            Using cmd As New SqlCommand(procName, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 120

                cmd.Parameters.Add("@ENTITY_TYPE", SqlDbType.TinyInt).Value = entityType
                cmd.Parameters.Add("@SOURCE_ID", SqlDbType.BigInt).Value = sourceId
                cmd.Parameters.Add("@USER_ID", SqlDbType.Int).Value = GetCurrentUserId()

                con.Open()
                cmd.ExecuteNonQuery()

                MessageBox.Show(successMessage, "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        End Using
    End Sub

    Private Sub ValidateOne(entityType As Integer, sourceId As Long)
        Dim dt As New DataTable()

        Using con As SqlConnection = GetConnection()
            Using cmd As New SqlCommand("dbo.ACC_ENTITY_VALIDATE_ONE", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 120

                cmd.Parameters.Add("@ENTITY_TYPE", SqlDbType.TinyInt).Value = entityType
                cmd.Parameters.Add("@SOURCE_ID", SqlDbType.BigInt).Value = sourceId

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        If dt.Rows.Count = 0 Then
            MessageBox.Show("لم يتم العثور على بيانات فحص لهذا السجل.", "فحص", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim r = dt.Rows(0)
        Dim msg As String =
            "النوع: " & Convert.ToString(r("ENTITY_NAME_AR")) & Environment.NewLine &
            "الاسم: " & Convert.ToString(r("ENTITY_NAME")) & Environment.NewLine &
            "حساب الجدول: " & Convert.ToString(r("SOURCE_ACC_CODE")) & Environment.NewLine &
            "حساب الرابط: " & Convert.ToString(r("LINK_ACC_CODE")) & Environment.NewLine &
            "الحالة: " & Convert.ToString(r("LINK_STATUS")) & Environment.NewLine &
            "عليه حركة: " & Convert.ToString(r("HAS_MOVEMENT"))

        MessageBox.Show(msg, "نتيجة الفحص", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub


    Private Sub RepairAllLinks()
        Using con As SqlConnection = GetConnection()
            Using cmd As New SqlCommand("dbo.ACC_ENTITY_REPAIR_ALL_LINKS", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 300

                cmd.Parameters.Add("@USER_ID", SqlDbType.Int).Value = GetCurrentUserId()

                con.Open()
                cmd.ExecuteNonQuery()

                MessageBox.Show("تم تنفيذ إصلاح الروابط الناقصة بنجاح.",
                                "تم",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
            End Using
        End Using
    End Sub

#End Region

#Region "Helpers"

    Private Function GetCurrentUserId() As Integer
        ' عدّلها حسب نظام المستخدمين عندك
        ' مثلًا: Return CurrentUser.ID
        Return USER_ID
    End Function

    Private Sub ShowError(ex As Exception)
        MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

#End Region


    Private Sub UpdateContextMenuState()
        Dim hasRow As Boolean = HasSelectedDetailRow()

        mnuCreateMissingAccount.Enabled = False
        mnuRepairLink.Enabled = False
        mnuChangeAccount.Enabled = False
        mnuLockLink.Enabled = False
        mnuUnlockLink.Enabled = False
        mnuValidateOne.Enabled = hasRow
        mnuOpenAccount.Enabled = hasRow

        If Not hasRow Then Return

        Dim isMissingAccCode As Boolean = GetBoolCellByName("IS_MISSING_ACC_CODE")
        Dim isMissingLink As Boolean = GetBoolCellByName("IS_MISSING_LINK")
        Dim isAccNotFound As Boolean = GetBoolCellByName("IS_ACC_NOT_FOUND_IN_TREE")
        Dim isDifferentLink As Boolean = GetBoolCellByName("IS_LINK_CODE_DIFFERENT")
        Dim hasMovement As Boolean = GetBoolCellByName("HAS_MOVEMENT")

        Dim hasAccCode As Boolean = Not String.IsNullOrWhiteSpace(GetSelectedAccCodeFromGrid())

        mnuCreateMissingAccount.Enabled = isMissingAccCode
        mnuRepairLink.Enabled = hasAccCode AndAlso Not isAccNotFound AndAlso (isMissingLink OrElse isDifferentLink)
        mnuChangeAccount.Enabled = hasRow AndAlso Not hasMovement
        mnuLockLink.Enabled = hasRow AndAlso Not isMissingLink
        mnuUnlockLink.Enabled = hasRow AndAlso Not isMissingLink
        mnuOpenAccount.Enabled = hasAccCode
    End Sub


    Private Sub UpdateActionButtonsState()
        Dim hasRow As Boolean = HasSelectedDetailRow()

        btnCreateMissingAccount.Enabled = False
        btnRepairLink.Enabled = False
        btnChangeAccount.Enabled = False
        btnLockLink.Enabled = False
        btnUnlockLink.Enabled = False
        btnValidateOne.Enabled = hasRow
        btnOpenAccount.Enabled = hasRow

        btnRepairAllLinks.Enabled = True
        btnCreateAllMissingAccounts.Enabled = True

        If Not hasRow Then
            UpdateContextMenuState()
            Return
        End If

        Dim isMissingAccCode As Boolean = GetBoolCellByName("IS_MISSING_ACC_CODE")
        Dim isMissingLink As Boolean = GetBoolCellByName("IS_MISSING_LINK")
        Dim isAccNotFound As Boolean = GetBoolCellByName("IS_ACC_NOT_FOUND_IN_TREE")
        Dim isDifferentLink As Boolean = GetBoolCellByName("IS_LINK_CODE_DIFFERENT")
        Dim hasMovement As Boolean = GetBoolCellByName("HAS_MOVEMENT")

        Dim hasAccCode As Boolean = Not String.IsNullOrWhiteSpace(GetSelectedAccCodeFromGrid())

        ' فتح حساب ناقص فقط إذا لا يوجد Tree_Code
        btnCreateMissingAccount.Enabled = isMissingAccCode

        ' إصلاح الرابط إذا يوجد حساب في الجدول، والحساب موجود بالدليل، لكن الرابط ناقص أو مختلف
        btnRepairLink.Enabled = hasAccCode AndAlso Not isAccNotFound AndAlso (isMissingLink OrElse isDifferentLink)

        ' تغيير الحساب متاح إذا يوجد سجل، لكن SQL سيمنع إذا الحساب القديم عليه حركة
        btnChangeAccount.Enabled = hasRow AndAlso Not hasMovement

        ' القفل وفك القفل يحتاجان رابط موجود
        btnLockLink.Enabled = hasRow AndAlso Not isMissingLink
        btnUnlockLink.Enabled = hasRow AndAlso Not isMissingLink

        ' عرض الحساب فقط إذا عنده رقم حساب
        btnOpenAccount.Enabled = hasAccCode

        UpdateContextMenuState()
    End Sub

    Private Function GetBoolCellByName(colName As String) As Boolean
        If Not HasSelectedDetailRow() Then Return False
        If Not dgvDetails.Columns.Contains(colName) Then Return False

        Dim v = dgvDetails.CurrentRow.Cells(colName).Value

        If v Is Nothing OrElse v Is DBNull.Value Then Return False

        Return Convert.ToBoolean(v)
    End Function


    Private Sub UpdateSelectedInfo()
        If HasSelectedDetailRow() Then
            lblSelectedInfo.Text = "المحدد: " & GetSelectedEntityNameFromGrid()
        Else
            lblSelectedInfo.Text = "لا يوجد تحديد"
        End If

        UpdateActionButtonsState()
    End Sub


    Private Sub btnRepairAllLinks_Click(sender As Object, e As EventArgs) Handles btnRepairAllLinks.Click
        Try
            If MessageBox.Show("سيتم إصلاح كل الروابط الناقصة التي لديها Tree_Code صحيح." &
                               Environment.NewLine &
                               "هل تريد المتابعة؟",
                               "تأكيد إصلاح الكل",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question,
                               MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then
                Return
            End If

            RepairAllLinks()
            LoadDashboard()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub


    Private Sub btnCreateAllMissingAccounts_Click(sender As Object, e As EventArgs) Handles btnCreateAllMissingAccounts.Click
        Try
            If MessageBox.Show("سيتم فتح حسابات محاسبية لكل الكيانات التي لا تملك Tree_Code." &
                               Environment.NewLine &
                               "هذه العملية قد تضيف عدة حسابات في الدليل المحاسبي." &
                               Environment.NewLine &
                               "هل تريد المتابعة؟",
                               "تأكيد فتح كل الحسابات الناقصة",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Warning,
                               MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then
                Return
            End If

            CreateAllMissingAccounts()
            LoadDashboard()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub



    Private Sub CreateAllMissingAccounts()
        Using con As SqlConnection = GetConnection()
            Using cmd As New SqlCommand("dbo.ACC_ENTITY_CREATE_ALL_MISSING", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 300

                cmd.Parameters.Add("@USER_ID", SqlDbType.Int).Value = GetCurrentUserId()

                con.Open()
                cmd.ExecuteNonQuery()

                MessageBox.Show("تم تنفيذ فتح الحسابات الناقصة بنجاح.",
                            "تم",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
            End Using
        End Using
    End Sub


    Private Sub btnRulesSettings_Click(sender As Object, e As EventArgs) Handles btnRulesSettings.Click
        Try
            Using frm As New FRM_ACC_ENTITY_RULES_MANAGER()
                frm.ShowDialog(Me)
            End Using

            LoadDashboard()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub



End Class
