Imports System.Data
Imports System.Data.SqlClient

Partial Public Class FrmIncomeStatementAccountPicker

    Public Property SelectedAccountID As Integer = 0
    Public Property SelectedAccountCode As String = ""
    Public Property SelectedAccountName As String = ""
    Public Property IncludeChildren As Boolean = True
    Public Property AccountSignMode As Integer = 1

    Public Property DateFrom As Date = New Date(Date.Today.Year, 1, 1)
    Public Property DateTo As Date = Date.Today

    Private _isLoading As Boolean = False

#Region "Load"

    Private Sub FrmIncomeStatementAccountPicker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _isLoading = True

            InitGrid()
            InitAccountSignModeCombo()

            _isLoading = False

            LoadAccounts()

        Catch ex As Exception
            _isLoading = False
            ShowError(ex)
        End Try
    End Sub

    Private Sub InitGrid()
        dgvAccounts.AllowUserToAddRows = False
        dgvAccounts.AllowUserToDeleteRows = False
        dgvAccounts.ReadOnly = True
        dgvAccounts.MultiSelect = False
        dgvAccounts.RowHeadersVisible = False
        dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvAccounts.BackgroundColor = Color.White
    End Sub

    Private Sub InitAccountSignModeCombo()
        Dim dt As New DataTable()
        dt.Columns.Add("ID", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))

        dt.Rows.Add(1, "حسب طبيعة البند")
        dt.Rows.Add(2, "عكس")
        dt.Rows.Add(3, "موجب دائمًا")
        dt.Rows.Add(4, "سالب دائمًا")
        dt.Rows.Add(5, "حسب الرصيد كما هو")

        cboAccountSignMode.DataSource = dt
        cboAccountSignMode.DisplayMember = "Name"
        cboAccountSignMode.ValueMember = "ID"
        cboAccountSignMode.SelectedValue = 1
    End Sub

#End Region

#Region "Connection"

    Private Function GetConnectionString() As String
        ' عدّل هذا السطر مثل الشاشة الرئيسية
        Return MY_Settings.SqlConStr
    End Function

    Private Function GetConnection() As SqlConnection
        Return New SqlConnection(GetConnectionString())
    End Function

#End Region

#Region "Database"

    Private Function ExecuteDataTable(procedureName As String,
                                      Optional parameters As List(Of SqlParameter) = Nothing) As DataTable

        Using con As SqlConnection = GetConnection()
            Using cmd As New SqlCommand(procedureName, con)
                cmd.CommandTimeout = 120
                cmd.CommandType = CommandType.StoredProcedure

                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters.ToArray())
                End If

                Using da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    Return dt
                End Using
            End Using
        End Using
    End Function

#End Region

#Region "Load Accounts"

    Private Sub LoadAccounts()
        Dim dt As DataTable = ExecuteDataTable(
            "dbo.ACC_IncomeStatement_SearchAccounts",
            New List(Of SqlParameter) From {
                New SqlParameter("@SearchText", If(String.IsNullOrWhiteSpace(txtSearch.Text), DBNull.Value, CType(txtSearch.Text.Trim(), Object))),
                New SqlParameter("@OnlyMovementAccounts", chkOnlyMovementAccounts.Checked),
                New SqlParameter("@DateFrom", DateFrom.Date),
                New SqlParameter("@DateTo", DateTo.Date)
            })

        dgvAccounts.DataSource = dt
        FormatAccountsGrid()

        lblStatus.Text = "عدد الحسابات: " & dt.Rows.Count.ToString()
    End Sub

    Private Sub FormatAccountsGrid()
        If dgvAccounts.DataSource Is Nothing Then Return

        HideColumnIfExists("ParentAccountID")
        HideColumnIfExists("AccountNature")
        HideColumnIfExists("ACC_NATURAL")

        SetHeader("AccountID", "رقم")
        SetHeader("AccountCode", "الكود")
        SetHeader("AccountName", "اسم الحساب")
        SetHeader("AccountNatureName", "الطبيعة")
        SetHeader("ACC_LEVEL", "المستوى")
        SetHeader("ACC_TYPE", "النوع")
        SetHeader("ACC_CAT", "التصنيف")
    End Sub

    Private Sub SetHeader(columnName As String, headerText As String)
        If dgvAccounts.Columns.Contains(columnName) Then
            dgvAccounts.Columns(columnName).HeaderText = headerText
        End If
    End Sub

    Private Sub HideColumnIfExists(columnName As String)
        If dgvAccounts.Columns.Contains(columnName) Then
            dgvAccounts.Columns(columnName).Visible = False
        End If
    End Sub

#End Region

#Region "Events"

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            LoadAccounts()
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            LoadAccounts()
        End If
    End Sub

    Private Sub chkOnlyMovementAccounts_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnlyMovementAccounts.CheckedChanged
        If _isLoading Then Return

        Try
            LoadAccounts()
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub dgvAccounts_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAccounts.CellDoubleClick
        If e.RowIndex >= 0 Then
            ConfirmSelection()
        End If
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        ConfirmSelection()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

#Region "Selection"

    Private Sub ConfirmSelection()
        Try
            If dgvAccounts.CurrentRow Is Nothing Then
                MessageBox.Show("اختر حسابًا أولًا.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim row As DataRowView = TryCast(dgvAccounts.CurrentRow.DataBoundItem, DataRowView)
            If row Is Nothing Then Return

            SelectedAccountID = SafeInt(row("AccountID"))
            SelectedAccountCode = SafeString(row("AccountCode"))
            SelectedAccountName = SafeString(row("AccountName"))

            IncludeChildren = chkIncludeChildren.Checked
            AccountSignMode = CInt(cboAccountSignMode.SelectedValue)

            If SelectedAccountID <= 0 Then
                MessageBox.Show("الحساب المحدد غير صحيح.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

#End Region

#Region "Helpers"

    Private Function SafeInt(value As Object) As Integer
        If value Is Nothing OrElse value Is DBNull.Value Then Return 0

        Dim result As Integer
        If Integer.TryParse(value.ToString(), result) Then
            Return result
        End If

        Return 0
    End Function

    Private Function SafeString(value As Object) As String
        If value Is Nothing OrElse value Is DBNull.Value Then Return ""
        Return value.ToString()
    End Function

    Private Sub ShowError(ex As Exception)
        lblStatus.Text = "خطأ: " & ex.Message
        MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

#End Region

End Class