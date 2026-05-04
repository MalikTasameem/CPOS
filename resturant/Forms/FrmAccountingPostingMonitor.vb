Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms

Partial Class FrmAccountingPostingMonitor

    Private ReadOnly _connectionString As String
    Private ReadOnly _currentUserId As Integer

    Public Sub New(connectionString As String, currentUserId As Integer)
        InitializeComponent()

        _connectionString = connectionString
        _currentUserId = currentUserId

        AddHandlers()
        SetDefaultDates()
        LoadLookups()
        LoadPostingMonitor()
    End Sub

#Region "Startup"

    Private Sub AddHandlers()
        AddHandler btnSearch.Click, AddressOf btnSearch_Click
        AddHandler btnRefresh.Click, AddressOf btnRefresh_Click
        AddHandler btnPostSelected.Click, AddressOf btnPostSelected_Click
        AddHandler btnPostAll.Click, AddressOf btnPostAll_Click
        AddHandler btnViewJournal.Click, AddressOf btnViewJournal_Click
        AddHandler btnClose.Click, AddressOf btnClose_Click

        AddHandler dgvPosting.CellFormatting, AddressOf dgvPosting_CellFormatting
        AddHandler dgvPosting.SelectionChanged, AddressOf dgvPosting_SelectionChanged
        AddHandler dgvPosting.CellDoubleClick, AddressOf dgvPosting_CellDoubleClick

        AddHandler txtSearch.KeyDown, AddressOf txtSearch_KeyDown
    End Sub

    Private Sub SetDefaultDates()
        dtpFrom.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
        dtpTo.Value = DateTime.Now.Date
    End Sub

#End Region

#Region "Lookups"

    Private Sub LoadLookups()
        LoadSourceCombo()
        LoadStatusCombo()
        LoadTypesCombo()
    End Sub

    Private Sub LoadSourceCombo()
        Dim dt As New DataTable()
        dt.Columns.Add("Value", GetType(String))
        dt.Columns.Add("Text", GetType(String))

        dt.Rows.Add(DBNull.Value, "الكل")
        dt.Rows.Add("MV", "الفواتير والحركات")
        dt.Rows.Add("RCT", "الإيصالات")

        cmbSource.DataSource = dt
        cmbSource.ValueMember = "Value"
        cmbSource.DisplayMember = "Text"
    End Sub

    Private Sub LoadStatusCombo()
        cmbPostingStatus.Items.Clear()
        cmbPostingStatus.Items.Add("الكل")
        cmbPostingStatus.Items.Add("غير مرحل")
        cmbPostingStatus.Items.Add("مرحل")
        cmbPostingStatus.SelectedIndex = 0
    End Sub

    Private Sub LoadTypesCombo()
        Try
            Dim dt As New DataTable()

            Dim sql As String =
"
SELECT CAST(NULL AS INT) AS id, N'الكل' AS Type_Name
UNION ALL
SELECT id, Type_Name
FROM dbo.AgentBalance_Type
WHERE Visible = 1
ORDER BY id;
"

            Using con As New SqlConnection(_connectionString)
                Using da As New SqlDataAdapter(sql, con)
                    da.Fill(dt)
                End Using
            End Using

            cmbType.DataSource = dt
            cmbType.ValueMember = "id"
            cmbType.DisplayMember = "Type_Name"

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ في تحميل أنواع الحركات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Load Posting Monitor"

    Private Sub LoadPostingMonitor()
        Try
            lblStatusMessage.Text = "جاري تحميل البيانات..."
            Application.DoEvents()

            Dim sql As String =
"
SELECT
    SourceTable,
    T_ID,
    [Date],
    BsType_ID,
    Type_Name,
    AG_NAME,
    Tr_NAME,
    Total,
    Discount,
    Pure,
    isDepended,
    isVoid,
    JournalId,
    PostedAt,
    PostedBy,
    PostingStatus,
    Receipt_Title,
    About
FROM dbo.V_AccountingPostingMonitor
WHERE [Date] >= @FromDate
  AND [Date] < DATEADD(DAY, 1, @ToDate)
  AND (@SourceTable IS NULL OR SourceTable = @SourceTable)
  AND (@BsType_ID IS NULL OR BsType_ID = @BsType_ID)
  AND (@PostingStatus IS NULL OR PostingStatus = @PostingStatus)
  AND ISNULL(isVoid, 0) = 0
  AND
  (
      @SearchText IS NULL
      OR CAST(T_ID AS NVARCHAR(50)) LIKE '%' + @SearchText + '%'
      OR ISNULL(Type_Name, '') LIKE '%' + @SearchText + '%'
      OR ISNULL(Receipt_Title, '') LIKE '%' + @SearchText + '%'
      OR ISNULL(About, '') LIKE '%' + @SearchText + '%'
      OR CAST(ISNULL(JournalId, 0) AS NVARCHAR(50)) LIKE '%' + @SearchText + '%'
  )
ORDER BY [Date] DESC, T_ID DESC;
"

            Dim dt As New DataTable()

            Using con As New SqlConnection(_connectionString)
                Using da As New SqlDataAdapter(sql, con)

                    da.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = dtpFrom.Value.Date
                    da.SelectCommand.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = dtpTo.Value.Date
                    da.SelectCommand.Parameters.Add("@SourceTable", SqlDbType.NVarChar, 20).Value = GetNullableComboValue(cmbSource)
                    da.SelectCommand.Parameters.Add("@BsType_ID", SqlDbType.Int).Value = GetNullableComboValue(cmbType)

                    Dim statusValue As Object = DBNull.Value
                    If cmbPostingStatus.SelectedIndex > 0 Then
                        statusValue = cmbPostingStatus.Text
                    End If
                    da.SelectCommand.Parameters.Add("@PostingStatus", SqlDbType.NVarChar, 50).Value = statusValue

                    Dim searchValue As Object = DBNull.Value
                    If txtSearch.Text.Trim() <> "" Then
                        searchValue = txtSearch.Text.Trim()
                    End If
                    da.SelectCommand.Parameters.Add("@SearchText", SqlDbType.NVarChar, 200).Value = searchValue

                    da.Fill(dt)
                End Using
            End Using

            dgvPosting.DataSource = dt
            FormatPostingGrid()
            UpdateCounters(dt)

            dgvJournal.DataSource = Nothing
            lblStatusMessage.Text = "تم تحميل البيانات"

        Catch ex As Exception
            lblStatusMessage.Text = "حدث خطأ"
            MessageBox.Show(ex.Message, "خطأ في تحميل الحركات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetNullableComboValue(cmb As ComboBox) As Object
        If cmb Is Nothing Then Return DBNull.Value
        If cmb.SelectedIndex <= 0 Then Return DBNull.Value
        If cmb.SelectedValue Is Nothing Then Return DBNull.Value
        If cmb.SelectedValue Is DBNull.Value Then Return DBNull.Value

        Return cmb.SelectedValue
    End Function

#End Region

#Region "Grid Formatting"

    Private Sub FormatPostingGrid()
        If dgvPosting.Columns.Count = 0 Then Return

        SetHeader("SourceTable", "المصدر", 90)
        SetHeader("T_ID", "رقم الحركة", 90)
        SetHeader("Date", "التاريخ", 110)
        SetHeader("BsType_ID", "كود النوع", 80)
        SetHeader("Type_Name", "نوع الحركة", 170)
        SetHeader("AG_NAME", "الطرف", 80)
        SetHeader("Tr_NAME", "الخزينة", 80)
        SetHeader("Total", "الإجمالي", 110)
        SetHeader("Discount", "الخصم", 95)
        SetHeader("Pure", "الصافي", 110)
        SetHeader("PostingStatus", "حالة الترحيل", 115)
        SetHeader("JournalId", "رقم القيد", 90)
        SetHeader("PostedAt", "تاريخ الترحيل", 140)
        SetHeader("Receipt_Title", "البيان", 250)
        SetHeader("About", "ملاحظات", 280)

        HideColumn("isDepended")
        HideColumn("isVoid")
        HideColumn("PostedBy")

        FormatNumericColumn("Total")
        FormatNumericColumn("Discount")
        FormatNumericColumn("Pure")
    End Sub

    Private Sub SetHeader(columnName As String, headerText As String, width As Integer)
        If dgvPosting.Columns.Contains(columnName) Then
            dgvPosting.Columns(columnName).HeaderText = headerText
            dgvPosting.Columns(columnName).Width = width
        End If
    End Sub

    Private Sub HideColumn(columnName As String)
        If dgvPosting.Columns.Contains(columnName) Then
            dgvPosting.Columns(columnName).Visible = False
        End If
    End Sub

    Private Sub FormatNumericColumn(columnName As String)
        If dgvPosting.Columns.Contains(columnName) Then
            dgvPosting.Columns(columnName).DefaultCellStyle.Format = "N3"
            dgvPosting.Columns(columnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub UpdateCounters(dt As DataTable)
        Dim total As Integer = dt.Rows.Count
        Dim posted As Integer = 0
        Dim unposted As Integer = 0

        For Each row As DataRow In dt.Rows
            If row("PostingStatus").ToString() = "مرحل" Then
                posted += 1
            Else
                unposted += 1
            End If
        Next

        lblTotalCount.Text = "الإجمالي: " & total.ToString()
        lblPostedCount.Text = "المرحل: " & posted.ToString()
        lblUnpostedCount.Text = "غير المرحل: " & unposted.ToString()
    End Sub

#End Region

#Region "Buttons"

    Private Sub btnSearch_Click(sender As Object, e As EventArgs)
        LoadPostingMonitor()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        LoadPostingMonitor()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub btnPostSelected_Click(sender As Object, e As EventArgs)
        Try
            If dgvPosting.SelectedRows.Count = 0 Then
                MessageBox.Show("يرجى اختيار حركة للترحيل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim row As DataGridViewRow = dgvPosting.SelectedRows(0)

            Dim sourceTable As String = row.Cells("SourceTable").Value.ToString()
            Dim movementId As Integer = Convert.ToInt32(row.Cells("T_ID").Value)
            Dim status As String = row.Cells("PostingStatus").Value.ToString()

            If status = "مرحل" Then
                MessageBox.Show("هذه الحركة مرحلة مسبقًا", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            If MessageBox.Show("هل تريد ترحيل الحركة المحددة؟",
                               "تأكيد الترحيل",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question) = DialogResult.No Then
                Return
            End If

            PostSingleMovement(sourceTable, movementId)

            MessageBox.Show("تم الترحيل بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadPostingMonitor()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "فشل الترحيل", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPostAll_Click(sender As Object, e As EventArgs)
        Try
            If dgvPosting.Rows.Count = 0 Then
                MessageBox.Show("لا توجد حركات ظاهرة للترحيل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If MessageBox.Show("هل تريد ترحيل كل الحركات غير المرحلة الظاهرة في الشاشة؟",
                               "تأكيد الترحيل الجماعي",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question) = DialogResult.No Then
                Return
            End If

            Dim successCount As Integer = 0
            Dim failCount As Integer = 0
            Dim errors As New List(Of String)

            btnPostAll.Enabled = False
            btnPostSelected.Enabled = False
            lblStatusMessage.Text = "جاري الترحيل الجماعي..."
            Application.DoEvents()

            For Each row As DataGridViewRow In dgvPosting.Rows
                If row.IsNewRow Then Continue For

                Dim status As String = row.Cells("PostingStatus").Value.ToString()
                If status = "مرحل" Then Continue For

                Dim sourceTable As String = row.Cells("SourceTable").Value.ToString()
                Dim movementId As Integer = Convert.ToInt32(row.Cells("T_ID").Value)

                Try
                    PostSingleMovement(sourceTable, movementId)
                    successCount += 1

                Catch ex As Exception
                    failCount += 1
                    errors.Add(sourceTable & "-" & movementId.ToString() & ": " & ex.Message)
                End Try
            Next

            lblStatusMessage.Text = "انتهى الترحيل الجماعي"

            Dim msg As String =
                "تم ترحيل " & successCount.ToString() & " حركة." &
                Environment.NewLine &
                "فشل ترحيل " & failCount.ToString() & " حركة."

            MessageBox.Show(msg, "نتيجة الترحيل", MessageBoxButtons.OK, MessageBoxIcon.Information)

            If errors.Count > 0 Then
                MessageBox.Show(String.Join(Environment.NewLine, errors.Take(15)),
                                "أول أخطاء الترحيل",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning)
            End If

            LoadPostingMonitor()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnPostAll.Enabled = True
            btnPostSelected.Enabled = True
        End Try
    End Sub

    Private Sub btnViewJournal_Click(sender As Object, e As EventArgs)
        LoadSelectedJournalDetails()
    End Sub

#End Region

#Region "Posting"

    Private Sub PostSingleMovement(sourceTable As String, movementId As Integer)
        Using con As New SqlConnection(_connectionString)
            Using cmd As New SqlCommand("dbo.PostToAccounting", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 120

                cmd.Parameters.Add("@SourceTable", SqlDbType.NVarChar, 20).Value = sourceTable
                cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = movementId
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = _currentUserId

                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

#End Region

#Region "Journal Details"

    Private Sub LoadSelectedJournalDetails()
        Try
            If dgvPosting.SelectedRows.Count = 0 Then Return

            Dim row As DataGridViewRow = dgvPosting.SelectedRows(0)

            If Not dgvPosting.Columns.Contains("JournalId") Then Return

            If row.Cells("JournalId").Value Is DBNull.Value OrElse row.Cells("JournalId").Value Is Nothing Then
                dgvJournal.DataSource = Nothing
                MessageBox.Show("هذه الحركة غير مرحلة ولا يوجد لها قيد.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim journalId As Integer = Convert.ToInt32(row.Cells("JournalId").Value)

            Dim sql As String =
"
SELECT
    b.T_ID,
    b.B_T_ID AS JournalId,
    b.ACC_CODE,
    b.DEBIT,
    b.CREDIT,
    b.Notes,
    b.Bill_Num,
    b.DATE_IN,
    b.USER_ID
FROM dbo.ACC_BALANCE b
WHERE b.B_T_ID = @JournalId
ORDER BY b.T_ID;
"

            Dim dt As New DataTable()

            Using con As New SqlConnection(_connectionString)
                Using da As New SqlDataAdapter(sql, con)
                    da.SelectCommand.Parameters.Add("@JournalId", SqlDbType.Int).Value = journalId
                    da.Fill(dt)
                End Using
            End Using

            dgvJournal.DataSource = dt
            FormatJournalGrid()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ في عرض القيد", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormatJournalGrid()
        If dgvJournal.Columns.Count = 0 Then Return

        If dgvJournal.Columns.Contains("T_ID") Then
            dgvJournal.Columns("T_ID").HeaderText = "رقم السطر"
            dgvJournal.Columns("T_ID").Width = 90
        End If

        If dgvJournal.Columns.Contains("JournalId") Then
            dgvJournal.Columns("JournalId").HeaderText = "رقم القيد"
            dgvJournal.Columns("JournalId").Width = 90
        End If

        If dgvJournal.Columns.Contains("ACC_CODE") Then
            dgvJournal.Columns("ACC_CODE").HeaderText = "الحساب"
            dgvJournal.Columns("ACC_CODE").Width = 120
        End If

        If dgvJournal.Columns.Contains("DEBIT") Then
            dgvJournal.Columns("DEBIT").HeaderText = "مدين"
            dgvJournal.Columns("DEBIT").DefaultCellStyle.Format = "N3"
            dgvJournal.Columns("DEBIT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

        If dgvJournal.Columns.Contains("CREDIT") Then
            dgvJournal.Columns("CREDIT").HeaderText = "دائن"
            dgvJournal.Columns("CREDIT").DefaultCellStyle.Format = "N3"
            dgvJournal.Columns("CREDIT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

        If dgvJournal.Columns.Contains("Notes") Then
            dgvJournal.Columns("Notes").HeaderText = "البيان"
        End If

        If dgvJournal.Columns.Contains("Bill_Num") Then
            dgvJournal.Columns("Bill_Num").HeaderText = "رقم المستند"
        End If

        If dgvJournal.Columns.Contains("DATE_IN") Then
            dgvJournal.Columns("DATE_IN").HeaderText = "تاريخ الإدخال"
        End If

        If dgvJournal.Columns.Contains("USER_ID") Then
            dgvJournal.Columns("USER_ID").HeaderText = "المستخدم"
        End If
    End Sub

#End Region

#Region "Grid Events"

    Private Sub dgvPosting_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If e.RowIndex < 0 Then Return
        If dgvPosting.Rows.Count = 0 Then Return

        Dim row As DataGridViewRow = dgvPosting.Rows(e.RowIndex)

        If dgvPosting.Columns.Contains("PostingStatus") Then
            Dim status As String = Convert.ToString(row.Cells("PostingStatus").Value)

            If status = "مرحل" Then
                row.DefaultCellStyle.BackColor = Color.FromArgb(232, 248, 245)
                row.DefaultCellStyle.ForeColor = Color.FromArgb(20, 90, 50)
            Else
                row.DefaultCellStyle.BackColor = Color.FromArgb(253, 237, 236)
                row.DefaultCellStyle.ForeColor = Color.FromArgb(120, 40, 31)
            End If
        End If

        If dgvPosting.Columns(e.ColumnIndex).Name = "SourceTable" Then
            If e.Value IsNot Nothing Then
                If e.Value.ToString() = "MV" Then
                    e.Value = "فاتورة"
                    e.FormattingApplied = True
                ElseIf e.Value.ToString() = "RCT" Then
                    e.Value = "إيصال"
                    e.FormattingApplied = True
                End If
            End If
        End If
    End Sub

    Private Sub dgvPosting_SelectionChanged(sender As Object, e As EventArgs)
        dgvJournal.DataSource = Nothing
    End Sub

    Private Sub dgvPosting_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            LoadSelectedJournalDetails()
        End If
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            LoadPostingMonitor()
        End If
    End Sub

    Private Sub FrmAccountingPostingMonitor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


#End Region

End Class