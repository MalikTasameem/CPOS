Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing

Public Class FrmAccountPicker

    ' عدّل هذا حسب نظام الاتصال عندك
    Private ReadOnly ConStr As String = MY_Settings.SqlConStr

    Public Property SelectedAccountTID As Integer = 0
    Public Property SelectedAccountCode As String = ""
    Public Property SelectedAccountName As String = ""

    Public Property OnlyLeaf As Boolean = True
    Public Property OnlyUnlocked As Boolean = True

    Private _dtAccounts As DataTable

    Private Sub FrmAccountPicker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetupGrid()
            LoadAccounts()
            txtSearch.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadAccounts(txtSearch.Text.Trim())
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            LoadAccounts(txtSearch.Text.Trim())
        ElseIf e.KeyCode = Keys.Down Then
            If dgvAccounts.Rows.Count > 0 Then
                dgvAccounts.Focus()
            End If
        End If
    End Sub

    Private Sub dgvAccounts_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvAccounts.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SelectCurrentAccount()
        End If
    End Sub

    Private Sub dgvAccounts_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAccounts.CellDoubleClick
        If e.RowIndex >= 0 Then
            SelectCurrentAccount()
        End If
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        SelectCurrentAccount()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub SetupGrid()
        With dgvAccounts
            .AutoGenerateColumns = False
            .Columns.Clear()
            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .RowHeadersVisible = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .EnableHeadersVisualStyles = False
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 55, 72)
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .ColumnHeadersDefaultCellStyle.Font = New Font("Tahoma", 9.0!, FontStyle.Bold)
            .DefaultCellStyle.Font = New Font("Tahoma", 9.0!)
            .ColumnHeadersHeight = 36
            .RowTemplate.Height = 32
        End With

        AddTextColumn("ACC_CODE", "كود الحساب", 120)
        AddTextColumn("ACC_NAME", "اسم الحساب", 280)
        AddTextColumn("ACC_NATURAL", "الطبيعة", 70)
        AddTextColumn("ACC_LEVEL", "المستوى", 70)
        AddTextColumn("ACC_PARENT", "الأب", 120)

        AddHiddenColumn("T_ID")
        AddHiddenColumn("is_Lock_Trans")
        AddHiddenColumn("IsLeafAccount")
        AddHiddenColumn("ChildCount")
        AddHiddenColumn("AccountDisplayName")
    End Sub

    Private Sub AddTextColumn(dataPropertyName As String, headerText As String, fillWeight As Single)
        Dim col As New DataGridViewTextBoxColumn()
        col.DataPropertyName = dataPropertyName
        col.HeaderText = headerText
        col.Name = dataPropertyName
        col.FillWeight = fillWeight
        col.SortMode = DataGridViewColumnSortMode.Automatic
        dgvAccounts.Columns.Add(col)
    End Sub

    Private Sub AddHiddenColumn(dataPropertyName As String)
        Dim col As New DataGridViewTextBoxColumn()
        col.DataPropertyName = dataPropertyName
        col.Name = dataPropertyName
        col.Visible = False
        dgvAccounts.Columns.Add(col)
    End Sub

    Private Sub LoadAccounts(Optional searchText As String = "")
        Try
            lblStatus.Text = "جاري تحميل الحسابات..."

            Using cn As New SqlConnection(ConStr)
                Using cmd As New SqlCommand("dbo.ACC_TREE_SELECTABLE_LOAD", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 60

                    cmd.Parameters.Add("@OnlyLeaf", SqlDbType.Bit).Value = OnlyLeaf
                    cmd.Parameters.Add("@OnlyUnlocked", SqlDbType.Bit).Value = OnlyUnlocked

                    If String.IsNullOrWhiteSpace(searchText) Then
                        cmd.Parameters.Add("@SearchText", SqlDbType.NVarChar, 250).Value = DBNull.Value
                    Else
                        cmd.Parameters.Add("@SearchText", SqlDbType.NVarChar, 250).Value = searchText
                    End If

                    Using da As New SqlDataAdapter(cmd)
                        _dtAccounts = New DataTable()
                        da.Fill(_dtAccounts)
                    End Using
                End Using
            End Using

            dgvAccounts.DataSource = _dtAccounts
            ApplyRowsStyle()

            lblStatus.Text = "عدد الحسابات: " & _dtAccounts.Rows.Count.ToString()

            If dgvAccounts.Rows.Count > 0 Then
                dgvAccounts.ClearSelection()
                dgvAccounts.Rows(0).Selected = True
                dgvAccounts.CurrentCell = dgvAccounts.Rows(0).Cells("ACC_CODE")
            End If

        Catch ex As Exception
            lblStatus.Text = "فشل تحميل الحسابات"
            MessageBox.Show(ex.Message, "خطأ في التحميل", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ApplyRowsStyle()
        For Each gridRow As DataGridViewRow In dgvAccounts.Rows

            Dim isLeaf As Boolean = False
            Dim isLocked As Boolean = False

            If gridRow.Cells("IsLeafAccount").Value IsNot Nothing AndAlso
               gridRow.Cells("IsLeafAccount").Value IsNot DBNull.Value Then
                isLeaf = Convert.ToBoolean(gridRow.Cells("IsLeafAccount").Value)
            End If

            If gridRow.Cells("is_Lock_Trans").Value IsNot Nothing AndAlso
               gridRow.Cells("is_Lock_Trans").Value IsNot DBNull.Value Then
                isLocked = Convert.ToBoolean(gridRow.Cells("is_Lock_Trans").Value)
            End If

            If isLocked Then
                gridRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 238)
                gridRow.DefaultCellStyle.ForeColor = Color.FromArgb(120, 30, 30)
            ElseIf isLeaf Then
                gridRow.DefaultCellStyle.BackColor = Color.White
                gridRow.DefaultCellStyle.ForeColor = Color.Black
            Else
                gridRow.DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)
                gridRow.DefaultCellStyle.ForeColor = Color.DimGray
            End If

        Next
    End Sub

    Private Sub SelectCurrentAccount()
        If dgvAccounts.CurrentRow Is Nothing Then
            MessageBox.Show("اختر حسابًا أولًا.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim row As DataGridViewRow = dgvAccounts.CurrentRow

        If row.Cells("T_ID").Value Is Nothing OrElse row.Cells("T_ID").Value Is DBNull.Value Then
            MessageBox.Show("الحساب المحدد غير صحيح.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim isLocked As Boolean = False
        If row.Cells("is_Lock_Trans").Value IsNot Nothing AndAlso row.Cells("is_Lock_Trans").Value IsNot DBNull.Value Then
            isLocked = Convert.ToBoolean(row.Cells("is_Lock_Trans").Value)
        End If

        If isLocked Then
            MessageBox.Show("هذا الحساب مقفل من الترحيل ولا يمكن اختياره.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        SelectedAccountTID = Convert.ToInt32(row.Cells("T_ID").Value)
        SelectedAccountCode = Convert.ToString(row.Cells("ACC_CODE").Value)
        SelectedAccountName = Convert.ToString(row.Cells("ACC_NAME").Value)

        If SelectedAccountTID <= 0 Then
            MessageBox.Show("رقم الحساب غير صحيح.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

End Class