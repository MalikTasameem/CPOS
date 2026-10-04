Imports System.Data.SqlClient

Public Class Outside_Sales_Touch_Search
    Inherits Form

    Private ReadOnly _grid As New DataGridView()
    Private ReadOnly _searchText As New TextBox()
    Private ReadOnly _fromDate As New DateTimePicker()
    Private ReadOnly _toDate As New DateTimePicker()
    Private ReadOnly _loadButton As New Button()
    Private ReadOnly _selectButton As New Button()
    Public Property SelectedTransactionId As Integer

    Public Sub New()
        Me.Text = "بحث أذونات الاستلام"
        Me.Size = New Size(950, 620)
        Me.MinimumSize = New Size(800, 520)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.RightToLeft = RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.Font = New Font("Segoe UI", 10.0!)

        Dim topPanel As New Panel() With {.Dock = DockStyle.Top, .Height = 62, .Padding = New Padding(8)}
        _searchText.Font = New Font("Segoe UI Semibold", 12.0!)
        _searchText.Location = New Point(520, 14)
        _searchText.Size = New Size(245, 30)
        _fromDate.Format = DateTimePickerFormat.Short
        _fromDate.Location = New Point(354, 16)
        _fromDate.Size = New Size(145, 27)
        _fromDate.Value = Date.Today.AddMonths(-1)
        _toDate.Format = DateTimePickerFormat.Short
        _toDate.Location = New Point(198, 16)
        _toDate.Size = New Size(145, 27)
        _loadButton.Text = "تحديث"
        _loadButton.Font = New Font("Segoe UI Semibold", 11.0!, FontStyle.Bold)
        _loadButton.Location = New Point(25, 10)
        _loadButton.Size = New Size(150, 40)
        topPanel.Controls.AddRange(New Control() {_searchText, _fromDate, _toDate, _loadButton})

        _grid.Dock = DockStyle.Fill
        _grid.ReadOnly = True
        _grid.AllowUserToAddRows = False
        _grid.AllowUserToDeleteRows = False
        _grid.RowHeadersVisible = False
        _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        _grid.MultiSelect = False
        _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        _grid.RowTemplate.Height = 40
        _grid.ColumnHeadersHeight = 42
        _grid.Font = New Font("Segoe UI Semibold", 10.5!)

        Dim bottomPanel As New Panel() With {.Dock = DockStyle.Bottom, .Height = 62}
        _selectButton.Text = "فتح الإذن المحدد"
        _selectButton.Font = New Font("Segoe UI Semibold", 12.0!, FontStyle.Bold)
        _selectButton.Size = New Size(220, 46)
        _selectButton.Location = New Point(15, 8)
        bottomPanel.Controls.Add(_selectButton)

        Me.Controls.Add(_grid)
        Me.Controls.Add(bottomPanel)
        Me.Controls.Add(topPanel)
        AddHandler Me.Load, AddressOf SearchForm_Load
        AddHandler _loadButton.Click, AddressOf LoadButton_Click
        AddHandler _selectButton.Click, AddressOf SelectButton_Click
        AddHandler _grid.CellDoubleClick, AddressOf Grid_CellDoubleClick
        AddHandler _searchText.TextChanged, AddressOf SearchText_TextChanged
    End Sub

    Private Sub SearchForm_Load(sender As Object, e As EventArgs)
        ThemeManager.ApplyThemeToForm(Me)
        LoadBills()
    End Sub

    Private Sub LoadButton_Click(sender As Object, e As EventArgs)
        LoadBills()
    End Sub

    Private Sub SearchText_TextChanged(sender As Object, e As EventArgs)
        ApplyFilter()
    End Sub

    Private Sub LoadBills()
        Try
            Dim table As New DataTable()
            Using connection As New SqlConnection(MY_Settings.SqlConStr)
                Using adapter As New SqlDataAdapter("SELECT T_ID,Bill_ID AS [رقم الإذن],Date AS [التاريخ],Receipt_Title AS [العنوان],Ag_name AS [الحساب],Cost AS [الإجمالي],UserName AS [المستخدم],isDepended AS [معتمد],isVoid AS [ملغى] FROM dbo.Outsale_Balance_MV_V WHERE CONVERT(date,Date) BETWEEN @DATE_F AND @DATE_T AND T_ID BETWEEN @START_ID AND @END_ID ORDER BY Date DESC,T_ID DESC", connection)
                    adapter.SelectCommand.Parameters.Add("@DATE_F", SqlDbType.Date).Value = _fromDate.Value.Date
                    adapter.SelectCommand.Parameters.Add("@DATE_T", SqlDbType.Date).Value = _toDate.Value.Date
                    adapter.SelectCommand.Parameters.Add("@START_ID", SqlDbType.Int).Value = START_ID
                    adapter.SelectCommand.Parameters.Add("@END_ID", SqlDbType.Int).Value = END_ID
                    adapter.Fill(table)
                End Using
            End Using
            _grid.DataSource = table
            If _grid.Columns.Contains("T_ID") Then _grid.Columns("T_ID").Visible = False
            ApplyFilter()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ApplyFilter()
        Dim table As DataTable = TryCast(_grid.DataSource, DataTable)
        If table Is Nothing Then Return
        Dim value As String = _searchText.Text.Replace("'", "''").Trim()
        table.DefaultView.RowFilter = If(value = "", "", "CONVERT([رقم الإذن], 'System.String') LIKE '%" & value & "%' OR [العنوان] LIKE '%" & value & "%' OR [الحساب] LIKE '%" & value & "%'")
    End Sub

    Private Sub SelectButton_Click(sender As Object, e As EventArgs)
        SelectCurrentRow()
    End Sub

    Private Sub Grid_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then SelectCurrentRow()
    End Sub

    Private Sub SelectCurrentRow()
        If _grid.CurrentRow Is Nothing Then Return
        SelectedTransactionId = Convert.ToInt32(_grid.CurrentRow.Cells("T_ID").Value)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class
