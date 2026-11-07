Public Class DraftCustomerSelector

    Private CustomersView As DataView
    Private LoadedFromServer As Boolean

    Public Property DefaultCustomerId As Integer = 1
    Public Property DefaultCustomerName As String = "نقدي"
    Public Property SelectedCustomerId As Integer
    Public Property SelectedCustomerName As String
    Public Property SelectedFromLocalCache As Boolean
    Public Property CacheUpdatedAt As DateTime?
    Public Property IsConfirmed As Boolean

    Private Sub DraftCustomerSelector_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.ApplyThemeToForm(Me)
        ConfigureGridFonts()
        LoadCustomers()
        SearchTextBox.Select()
    End Sub

    Private Sub ConfigureGridFonts()
        CustomersGrid.AutoGenerateColumns = False
        CustomersGrid.DefaultCellStyle.Font = New Font("Segoe UI Semibold", 10.0!, FontStyle.Bold)
        CustomersGrid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10.0!, FontStyle.Regular)
        CustomersGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        CustomersGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215)
        CustomersGrid.DefaultCellStyle.SelectionForeColor = Color.White
    End Sub

    Private Sub LoadCustomers()
        Dim service As New DraftCustomerCacheService()
        Dim result As DraftCustomerCacheResult = service.LoadCustomers(DefaultCustomerId)

        LoadedFromServer = result.LoadedFromServer
        CacheUpdatedAt = result.CacheUpdatedAt

        Dim customers As DataTable = result.Customers
        If customers Is Nothing Then customers = CreateFallbackTable()

        EnsureCashCustomer(customers)
        CustomersView = New DataView(customers)
        CustomersView.Sort = "Ag_name ASC, AG_ID ASC"
        CustomersGrid.DataSource = CustomersView

        If LoadedFromServer Then
            SourceStatusLabel.BackColor = Color.Honeydew
            SourceStatusLabel.ForeColor = Color.DarkGreen
            SourceStatusLabel.Text = "متصل بالخادم - تم تحديث قائمة العملاء الآن."
        ElseIf customers.Rows.Count > 1 OrElse CacheUpdatedAt.HasValue Then
            SourceStatusLabel.BackColor = Color.LemonChiffon
            SourceStatusLabel.ForeColor = Color.DarkGoldenrod
            SourceStatusLabel.Text = "وضع غير متصل - يتم العرض من آخر نسخة محلية" &
                If(CacheUpdatedAt.HasValue, " بتاريخ " & CacheUpdatedAt.Value.ToString("yyyy/MM/dd HH:mm"), "") & "."
        Else
            SourceStatusLabel.BackColor = Color.MistyRose
            SourceStatusLabel.ForeColor = Color.DarkRed
            SourceStatusLabel.Text = "تعذر الاتصال ولا توجد قائمة محلية؛ يتوفر العميل النقدي فقط."
        End If

        If String.IsNullOrWhiteSpace(result.ErrorMessage) = False Then
            SourceStatusLabel.Tag = result.ErrorMessage
        End If

        SelectCurrentOrFirstRow()
    End Sub

    Private Function CreateFallbackTable() As DataTable
        Dim table As New DataTable("DraftCustomers")
        table.Columns.Add("AG_ID", GetType(Integer))
        table.Columns.Add("Ag_name", GetType(String))
        table.Columns.Add("Barcode", GetType(String))
        table.Columns.Add("Ag_phone", GetType(String))
        table.Columns.Add("Type_ID", GetType(Integer))
        table.Columns.Add("isDefaultAG", GetType(Boolean))
        Return table
    End Function

    Private Sub EnsureCashCustomer(customers As DataTable)
        If customers Is Nothing Then Return

        Dim cashRows As DataRow() = customers.Select("AG_ID = " & DefaultCustomerId.ToString())
        If cashRows.Length > 0 Then
            If String.IsNullOrWhiteSpace(DefaultCustomerName) Then
                DefaultCustomerName = Convert.ToString(cashRows(0)("Ag_name"))
            End If
            Return
        End If

        Dim row As DataRow = customers.NewRow()
        row("AG_ID") = DefaultCustomerId
        row("Ag_name") = If(String.IsNullOrWhiteSpace(DefaultCustomerName), "نقدي", DefaultCustomerName)
        row("Barcode") = ""
        row("Ag_phone") = ""
        row("Type_ID") = 1
        row("isDefaultAG") = True
        customers.Rows.Add(row)
    End Sub

    Private Sub SelectCurrentOrFirstRow()
        If CustomersGrid.Rows.Count = 0 Then Return

        For Each row As DataGridViewRow In CustomersGrid.Rows
            If Convert.ToInt32(row.Cells("CustomerIdColumn").Value) = DefaultCustomerId Then
                row.Selected = True
                CustomersGrid.CurrentCell = row.Cells("CustomerNameColumn")
                Return
            End If
        Next

        CustomersGrid.Rows(0).Selected = True
        CustomersGrid.CurrentCell = CustomersGrid.Rows(0).Cells("CustomerNameColumn")
    End Sub

    Private Sub SearchTextBox_TextChanged(sender As Object, e As EventArgs) Handles SearchTextBox.TextChanged
        If CustomersView Is Nothing Then Return

        Dim keyword As String = EscapeRowFilterValue(SearchTextBox.Text.Trim())
        If String.IsNullOrWhiteSpace(keyword) Then
            CustomersView.RowFilter = ""
        Else
            CustomersView.RowFilter =
                "Ag_name LIKE '%" & keyword & "%' OR " &
                "Ag_phone LIKE '%" & keyword & "%' OR " &
                "Barcode LIKE '%" & keyword & "%' OR " &
                "CONVERT(AG_ID, 'System.String') LIKE '%" & keyword & "%'"
        End If

        If CustomersGrid.Rows.Count > 0 Then
            CustomersGrid.Rows(0).Selected = True
            CustomersGrid.CurrentCell = CustomersGrid.Rows(0).Cells("CustomerNameColumn")
        End If
    End Sub

    Private Function EscapeRowFilterValue(value As String) As String
        Return value.Replace("'", "''").
            Replace("[", "[[]").
            Replace("%", "[%]").
            Replace("*", "[*]")
    End Function

    Private Sub SelectButton_Click(sender As Object, e As EventArgs) Handles SelectButton.Click
        ConfirmSelectedCustomer()
    End Sub

    Private Sub CustomersGrid_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles CustomersGrid.CellDoubleClick
        If e.RowIndex >= 0 Then ConfirmSelectedCustomer()
    End Sub

    Private Sub ConfirmSelectedCustomer()
        If CustomersGrid.CurrentRow Is Nothing Then
            MessageBox.Show("اختر عميلاً أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        SelectedCustomerId = Convert.ToInt32(CustomersGrid.CurrentRow.Cells("CustomerIdColumn").Value)
        SelectedCustomerName = Convert.ToString(CustomersGrid.CurrentRow.Cells("CustomerNameColumn").Value).Trim()
        SelectedFromLocalCache = Not LoadedFromServer
        IsConfirmed = True
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub CashCustomerButton_Click(sender As Object, e As EventArgs) Handles CashCustomerButton.Click
        SelectedCustomerId = DefaultCustomerId
        SelectedCustomerName = If(String.IsNullOrWhiteSpace(DefaultCustomerName), "نقدي", DefaultCustomerName)
        SelectedFromLocalCache = Not LoadedFromServer
        IsConfirmed = True
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub DraftCustomerSelector_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            DialogResult = DialogResult.Cancel
            Close()
        End If
    End Sub

    Private Sub SourceStatusLabel_DoubleClick(sender As Object, e As EventArgs) Handles SourceStatusLabel.DoubleClick
        If SourceStatusLabel.Tag Is Nothing Then Return
        MessageBox.Show(Convert.ToString(SourceStatusLabel.Tag), "تفاصيل الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

End Class
