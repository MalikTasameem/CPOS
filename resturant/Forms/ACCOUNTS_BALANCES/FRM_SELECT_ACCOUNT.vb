Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms

Public Class FRM_SELECT_ACCOUNT

#Region "Properties"

    Public Property SelectedAccCode As String = ""
    Public Property SelectedAccName As String = ""
    Public Property SelectedAccParent As String = ""
    Public Property SelectedAccLevel As Integer = 0

#End Region

#Region "Fields"

    Private ReadOnly _initialSearch As String = ""

    ' غيّر هذا حسب مشروعك، أو انسخ نفس GetConnection من الفورم السابق
    Private ReadOnly ConStr As String = MY_Settings.SqlConStr

#End Region

#Region "Constructors"

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(initialSearch As String)
        InitializeComponent()
        _initialSearch = If(initialSearch, "")
    End Sub

#End Region

#Region "Form Events"

    Private Sub FRM_SELECT_ACCOUNT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            ' تطبيق الثيم الإجباري
            ThemeManager.ApplyThemeToForm(Me)

            PrepareGrid()

            If Not String.IsNullOrWhiteSpace(_initialSearch) Then
                txtSearch.Text = _initialSearch.Trim()
            End If

            LoadAccounts()
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

#End Region

#Region "Database"

    Private Function GetConnection() As SqlConnection
        Return New SqlConnection(ConStr)
    End Function

    Private Sub LoadAccounts()
        Try
            Dim dt As New DataTable()

            Using con As SqlConnection = GetConnection()
                Using cmd As New SqlCommand()
                    cmd.Connection = con
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 120

                    cmd.CommandText =
"
SELECT TOP 500
    A.ACC_CODE,
    A.ACC_NAME,
    A.ACC_PARENT,
    A.ACC_LEVEL,
    A.ACC_NATURAL,
    A.ACC_TYPE,
    A.ACC_CAT,
    A.is_Lock_Trans,
    CASE 
        WHEN EXISTS
        (
            SELECT 1 
            FROM dbo.ACCOUNTS_TREE C 
            WHERE C.ACC_PARENT = A.ACC_CODE
        )
        THEN CAST(0 AS bit)
        ELSE CAST(1 AS bit)
    END AS IsLeaf
FROM dbo.ACCOUNTS_TREE A
WHERE
    (
        @SEARCH IS NULL
        OR A.ACC_CODE LIKE '%' + @SEARCH + '%'
        OR A.ACC_NAME LIKE N'%' + @SEARCH + N'%'
    )
    AND
    (
        @ONLY_LEAF = 0
        OR NOT EXISTS
        (
            SELECT 1 
            FROM dbo.ACCOUNTS_TREE C 
            WHERE C.ACC_PARENT = A.ACC_CODE
        )
    )
ORDER BY A.ACC_CODE;
"

                    Dim searchValue As Object = DBNull.Value
                    If Not String.IsNullOrWhiteSpace(txtSearch.Text) Then
                        searchValue = txtSearch.Text.Trim()
                    End If

                    cmd.Parameters.Add("@SEARCH", SqlDbType.NVarChar, 200).Value = searchValue
                    cmd.Parameters.Add("@ONLY_LEAF", SqlDbType.Bit).Value = chkOnlyLeaf.Checked

                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using

            dgvAccounts.DataSource = dt
            FormatGrid()
            UpdateSelectedLabel()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

#End Region

#Region "Grid"

    Private Sub PrepareGrid()
        dgvAccounts.AutoGenerateColumns = True
        dgvAccounts.AllowUserToAddRows = False
        dgvAccounts.AllowUserToDeleteRows = False
        dgvAccounts.ReadOnly = True
        dgvAccounts.MultiSelect = False
        dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvAccounts.RowHeadersVisible = False
        dgvAccounts.BackgroundColor = Color.White
        dgvAccounts.BorderStyle = BorderStyle.None
        dgvAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
        dgvAccounts.EnableHeadersVisualStyles = False
        dgvAccounts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(238, 238, 238)
        dgvAccounts.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
        dgvAccounts.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        dgvAccounts.DefaultCellStyle.Font = New Font("Segoe UI", 9.0F, FontStyle.Regular)
        dgvAccounts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250)
    End Sub

    Private Sub FormatGrid()
        RenameColumn("ACC_CODE", "رقم الحساب")
        RenameColumn("ACC_NAME", "اسم الحساب")
        RenameColumn("ACC_PARENT", "الحساب الأب")
        RenameColumn("ACC_LEVEL", "المستوى")
        RenameColumn("ACC_NATURAL", "الطبيعة")
        RenameColumn("ACC_TYPE", "النوع")
        RenameColumn("ACC_CAT", "التصنيف")
        RenameColumn("is_Lock_Trans", "مقفل")
        RenameColumn("IsLeaf", "فرعي")

        If dgvAccounts.Columns.Contains("ACC_NAME") Then
            dgvAccounts.Columns("ACC_NAME").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If
    End Sub

    Private Sub RenameColumn(columnName As String, headerText As String)
        If dgvAccounts.Columns.Contains(columnName) Then
            dgvAccounts.Columns(columnName).HeaderText = headerText
        End If
    End Sub

    Private Function HasSelectedRow() As Boolean
        Return dgvAccounts.CurrentRow IsNot Nothing AndAlso dgvAccounts.CurrentRow.IsNewRow = False
    End Function

    Private Function GetCellText(columnName As String) As String
        If Not HasSelectedRow() Then Return ""
        If Not dgvAccounts.Columns.Contains(columnName) Then Return ""

        Dim v = dgvAccounts.CurrentRow.Cells(columnName).Value

        If v Is Nothing OrElse v Is DBNull.Value Then Return ""

        Return Convert.ToString(v).Trim()
    End Function

    Private Function GetCellInt(columnName As String) As Integer
        Dim s = GetCellText(columnName)
        Dim n As Integer = 0
        Integer.TryParse(s, n)
        Return n
    End Function

    Private Sub UpdateSelectedLabel()
        If Not HasSelectedRow() Then
            lblSelected.Text = "لم يتم اختيار حساب"
            Return
        End If

        lblSelected.Text = "المحدد: " & GetCellText("ACC_CODE") & " - " & GetCellText("ACC_NAME")
    End Sub

#End Region

#Region "Actions"

    Private Sub SelectCurrentAccount()
        If Not HasSelectedRow() Then
            MessageBox.Show("يرجى اختيار حساب.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        SelectedAccCode = GetCellText("ACC_CODE")
        SelectedAccName = GetCellText("ACC_NAME")
        SelectedAccParent = GetCellText("ACC_PARENT")
        SelectedAccLevel = GetCellInt("ACC_LEVEL")

        If String.IsNullOrWhiteSpace(SelectedAccCode) Then
            MessageBox.Show("الحساب المحدد غير صالح.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

#End Region

#Region "Events"

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadAccounts()
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            LoadAccounts()
        End If
    End Sub

    Private Sub chkOnlyLeaf_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnlyLeaf.CheckedChanged
        LoadAccounts()
    End Sub

    Private Sub dgvAccounts_SelectionChanged(sender As Object, e As EventArgs) Handles dgvAccounts.SelectionChanged
        UpdateSelectedLabel()
    End Sub

    Private Sub dgvAccounts_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAccounts.CellDoubleClick
        If e.RowIndex >= 0 Then
            SelectCurrentAccount()
        End If
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        SelectCurrentAccount()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

#Region "Helpers"

    Private Sub ShowError(ex As Exception)
        MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

#End Region

End Class