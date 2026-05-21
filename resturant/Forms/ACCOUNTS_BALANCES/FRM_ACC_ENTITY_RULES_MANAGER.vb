Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms

Public Class FRM_ACC_ENTITY_RULES_MANAGER

#Region "Fields"

    ' عدّل الاتصال حسب مشروعك كما فعلت في الفورم السابق
    Private ReadOnly ConStr As String = MY_Settings.SqlConStr

    Private _dtRules As DataTable
    Private _isLoading As Boolean = False

#End Region

#Region "Form Events"

    Private Sub FRM_ACC_ENTITY_RULES_MANAGER_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _isLoading = True

            PrepareGrid()
            ClearEditPanel()

            _isLoading = False

            LoadRules()

        Catch ex As Exception
            _isLoading = False
            ShowError(ex)
        End Try
    End Sub

#End Region

#Region "Connection"

    Private Function GetConnection() As SqlConnection
        Return New SqlConnection(ConStr)
    End Function

#End Region

#Region "Load Rules"

    Private Sub LoadRules()
        Try
            lblStatus.Text = "جاري تحميل قواعد الربط..."
            Application.DoEvents()

            _dtRules = New DataTable()

            Using con As SqlConnection = GetConnection()
                Using cmd As New SqlCommand("dbo.ACC_ENTITY_RULES_GET", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 120

                    cmd.Parameters.Add("@ONLY_ACTIVE", SqlDbType.Bit).Value = GetOnlyActiveValue()
                    cmd.Parameters.Add("@ONLY_ISSUES", SqlDbType.Bit).Value = chkOnlyIssues.Checked

                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(_dtRules)
                    End Using
                End Using
            End Using

            dgvRules.DataSource = _dtRules
            FormatGrid()

            If dgvRules.Rows.Count > 0 Then
                dgvRules.Rows(0).Selected = True
                dgvRules.CurrentCell = dgvRules.Rows(0).Cells(0)
                FillEditPanelFromCurrentRow()
            Else
                ClearEditPanel()
            End If

            lblStatus.Text = "تم تحميل القواعد بنجاح"

        Catch ex As Exception
            lblStatus.Text = "حدث خطأ أثناء تحميل القواعد"
            ShowError(ex)
        End Try
    End Sub

    Private Function GetOnlyActiveValue() As Object
        If chkOnlyActive.Checked Then
            Return True
        End If

        Return DBNull.Value
    End Function

#End Region

#Region "Grid"

    Private Sub PrepareGrid()
        dgvRules.AutoGenerateColumns = True
        dgvRules.AllowUserToAddRows = False
        dgvRules.AllowUserToDeleteRows = False
        dgvRules.ReadOnly = True
        dgvRules.MultiSelect = False
        dgvRules.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvRules.RowHeadersVisible = False
        dgvRules.BackgroundColor = Color.White
        dgvRules.BorderStyle = BorderStyle.None
        dgvRules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
        dgvRules.EnableHeadersVisualStyles = False
        dgvRules.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(238, 238, 238)
        dgvRules.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
        dgvRules.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        dgvRules.DefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
        dgvRules.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250)
    End Sub

    Private Sub FormatGrid()
        RenameColumn("RULE_ID", "رقم القاعدة")
        RenameColumn("ENTITY_TYPE", "رقم النوع")
        RenameColumn("ENTITY_NAME_AR", "النوع")
        RenameColumn("SOURCE_TABLE", "الجدول")
        RenameColumn("PARENT_ACC_CODE", "الحساب الأب")
        RenameColumn("PARENT_ACC_NAME", "اسم الحساب الأب")
        RenameColumn("PARENT_ACC_LEVEL", "مستوى الأب")
        RenameColumn("PARENT_ACC_NATURAL", "طبيعة الأب")
        RenameColumn("AUTO_CREATE_ACCOUNT", "فتح تلقائي")
        RenameColumn("ALLOW_RENAME", "مزامنة الاسم")
        RenameColumn("ALLOW_CHANGE_PARENT", "تغيير الأب")
        RenameColumn("ALLOW_DELETE_IF_NO_MOVE", "حذف بدون حركة")
        RenameColumn("IS_ACTIVE", "مفعلة")
        RenameColumn("IS_PARENT_NOT_FOUND", "الأب غير موجود")
        RenameColumn("RULE_STATUS", "الحالة")

        If dgvRules.Columns.Contains("PARENT_ACC_NAME") Then
            dgvRules.Columns("PARENT_ACC_NAME").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If

        ColorRows()
    End Sub

    Private Sub RenameColumn(columnName As String, headerText As String)
        If dgvRules.Columns.Contains(columnName) Then
            dgvRules.Columns(columnName).HeaderText = headerText
        End If
    End Sub

    Private Sub ColorRows()
        For Each row As DataGridViewRow In dgvRules.Rows
            If row.IsNewRow Then Continue For

            Dim parentNotFound As Boolean = GetBoolCell(row, "IS_PARENT_NOT_FOUND")
            Dim isActive As Boolean = GetBoolCell(row, "IS_ACTIVE")

            If parentNotFound Then
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 224)
                row.DefaultCellStyle.ForeColor = Color.FromArgb(120, 40, 40)
            ElseIf Not isActive Then
                row.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)
                row.DefaultCellStyle.ForeColor = Color.Gray
            Else
                row.DefaultCellStyle.BackColor = Color.White
                row.DefaultCellStyle.ForeColor = Color.Black
            End If
        Next
    End Sub

    Private Function GetBoolCell(row As DataGridViewRow, colName As String) As Boolean
        If Not dgvRules.Columns.Contains(colName) Then Return False

        Dim v = row.Cells(colName).Value

        If v Is Nothing OrElse v Is DBNull.Value Then Return False

        Return Convert.ToBoolean(v)
    End Function

    Private Function HasSelectedRow() As Boolean
        Return dgvRules.CurrentRow IsNot Nothing AndAlso dgvRules.CurrentRow.IsNewRow = False
    End Function

#End Region

#Region "Edit Panel"

    Private Sub ClearEditPanel()
        txtRuleID.Text = ""
        txtEntityType.Text = ""
        txtEntityName.Text = ""
        txtSourceTable.Text = ""
        txtParentAccCode.Text = ""
        txtParentAccName.Text = ""

        chkAutoCreate.Checked = False
        chkAllowRename.Checked = False
        chkAllowChangeParent.Checked = False
        chkAllowDeleteIfNoMove.Checked = False
        chkIsActive.Checked = False

        btnSave.Enabled = False
        btnSelectParent.Enabled = False
    End Sub

    Private Sub FillEditPanelFromCurrentRow()
        If Not HasSelectedRow() Then
            ClearEditPanel()
            Return
        End If

        txtRuleID.Text = GetCellText("RULE_ID")
        txtEntityType.Text = GetCellText("ENTITY_TYPE")
        txtEntityName.Text = GetCellText("ENTITY_NAME_AR")
        txtSourceTable.Text = GetCellText("SOURCE_TABLE")
        txtParentAccCode.Text = GetCellText("PARENT_ACC_CODE")
        txtParentAccName.Text = GetCellText("PARENT_ACC_NAME")

        chkAutoCreate.Checked = GetCurrentBool("AUTO_CREATE_ACCOUNT")
        chkAllowRename.Checked = GetCurrentBool("ALLOW_RENAME")
        chkAllowChangeParent.Checked = GetCurrentBool("ALLOW_CHANGE_PARENT")
        chkAllowDeleteIfNoMove.Checked = GetCurrentBool("ALLOW_DELETE_IF_NO_MOVE")
        chkIsActive.Checked = GetCurrentBool("IS_ACTIVE")

        btnSave.Enabled = True
        btnSelectParent.Enabled = True
    End Sub

    Private Function GetCellText(columnName As String) As String
        If Not HasSelectedRow() Then Return ""
        If Not dgvRules.Columns.Contains(columnName) Then Return ""

        Dim v = dgvRules.CurrentRow.Cells(columnName).Value

        If v Is Nothing OrElse v Is DBNull.Value Then Return ""

        Return Convert.ToString(v).Trim()
    End Function

    Private Function GetCurrentBool(columnName As String) As Boolean
        If Not HasSelectedRow() Then Return False
        If Not dgvRules.Columns.Contains(columnName) Then Return False

        Dim v = dgvRules.CurrentRow.Cells(columnName).Value

        If v Is Nothing OrElse v Is DBNull.Value Then Return False

        Return Convert.ToBoolean(v)
    End Function

    Private Function GetRuleId() As Integer
        Dim id As Integer = 0
        Integer.TryParse(txtRuleID.Text.Trim(), id)
        Return id
    End Function

#End Region

#Region "Actions"

    Private Sub SaveRule()
        Dim ruleId As Integer = GetRuleId()

        If ruleId <= 0 Then
            MessageBox.Show("يرجى اختيار قاعدة صحيحة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim parentCode As String = txtParentAccCode.Text.Trim()

        If String.IsNullOrWhiteSpace(parentCode) Then
            MessageBox.Show("يرجى اختيار الحساب الأب.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If MessageBox.Show("هل تريد حفظ تعديل قاعدة الربط؟",
                           "تأكيد",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question,
                           MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then
            Return
        End If

        Using con As SqlConnection = GetConnection()
            Using cmd As New SqlCommand("dbo.ACC_ENTITY_RULES_UPDATE", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 120

                cmd.Parameters.Add("@RULE_ID", SqlDbType.Int).Value = ruleId
                cmd.Parameters.Add("@PARENT_ACC_CODE", SqlDbType.VarChar, 50).Value = parentCode
                cmd.Parameters.Add("@AUTO_CREATE_ACCOUNT", SqlDbType.Bit).Value = chkAutoCreate.Checked
                cmd.Parameters.Add("@ALLOW_RENAME", SqlDbType.Bit).Value = chkAllowRename.Checked
                cmd.Parameters.Add("@ALLOW_CHANGE_PARENT", SqlDbType.Bit).Value = chkAllowChangeParent.Checked
                cmd.Parameters.Add("@ALLOW_DELETE_IF_NO_MOVE", SqlDbType.Bit).Value = chkAllowDeleteIfNoMove.Checked
                cmd.Parameters.Add("@IS_ACTIVE", SqlDbType.Bit).Value = chkIsActive.Checked

                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("تم حفظ قاعدة الربط بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
        LoadRules()
    End Sub

    Private Sub SelectParentAccount()
        Using frm As New FRM_SELECT_ACCOUNT(txtParentAccCode.Text.Trim())
            frm.chkOnlyLeaf.Checked = False

            If frm.ShowDialog(Me) <> DialogResult.OK Then
                Return
            End If

            txtParentAccCode.Text = frm.SelectedAccCode
            txtParentAccName.Text = frm.SelectedAccName
        End Using
    End Sub

    Private Sub ValidateRules()
        Dim dt As New DataTable()

        Using con As SqlConnection = GetConnection()
            Using cmd As New SqlCommand("dbo.ACC_ENTITY_RULES_VALIDATE", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 120

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        If dt.Rows.Count = 0 Then
            MessageBox.Show("كل قواعد الربط سليمة.", "فحص القواعد", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("توجد مشاكل في قواعد الربط. سيتم عرض المشاكل فقط.", "فحص القواعد", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            chkOnlyIssues.Checked = True
            LoadRules()
        End If
    End Sub

#End Region

#Region "Events"

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadRules()
    End Sub

    Private Sub chkOnlyActive_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnlyActive.CheckedChanged
        If _isLoading Then Return
        LoadRules()
    End Sub

    Private Sub chkOnlyIssues_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnlyIssues.CheckedChanged
        If _isLoading Then Return
        LoadRules()
    End Sub

    Private Sub dgvRules_SelectionChanged(sender As Object, e As EventArgs) Handles dgvRules.SelectionChanged
        FillEditPanelFromCurrentRow()
    End Sub

    Private Sub btnSelectParent_Click(sender As Object, e As EventArgs) Handles btnSelectParent.Click
        Try
            SelectParentAccount()
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            SaveRule()
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
        Try
            ValidateRules()
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Helpers"

    Private Sub ShowError(ex As Exception)
        MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

#End Region

End Class