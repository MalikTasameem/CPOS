Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms

Partial Class FrmAccountingRepostMonitor

    Private ReadOnly _connectionString As String
    Private ReadOnly _currentUserId As Integer

    Public Sub New(connectionString As String, currentUserId As Integer)
        InitializeComponent()

        _connectionString = connectionString
        _currentUserId = currentUserId

        AddHandlers()
        SetDefaultDates()
        LoadLookups()
        LoadRepostMonitor()
    End Sub

#Region "Startup"

    Private Sub AddHandlers()
        AddHandler btnSearch.Click, AddressOf btnSearch_Click
        AddHandler btnRefresh.Click, AddressOf btnRefresh_Click
        AddHandler btnViewOldJournal.Click, AddressOf btnViewOldJournal_Click
        AddHandler btnViewReversalJournal.Click, AddressOf btnViewReversalJournal_Click
        AddHandler btnViewNewJournal.Click, AddressOf btnViewNewJournal_Click
        AddHandler btnClose.Click, AddressOf btnClose_Click

        AddHandler dgvRepost.CellFormatting, AddressOf dgvRepost_CellFormatting
        AddHandler dgvRepost.SelectionChanged, AddressOf dgvRepost_SelectionChanged
        AddHandler dgvRepost.CellDoubleClick, AddressOf dgvRepost_CellDoubleClick

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
        LoadUsersCombo()
        LoadTypesCombo()
    End Sub

    Private Sub LoadSourceCombo()
        Dim dt As New DataTable()
        dt.Columns.Add("Value", GetType(String))
        dt.Columns.Add("Text", GetType(String))

        dt.Rows.Add(DBNull.Value, "الكل")
        dt.Rows.Add("Agents_Balance_MV", "الفواتير والحركات")
        dt.Rows.Add("Agents_Balance_MV_RCT", "الإيصالات")

        cmbSource.DataSource = dt
        cmbSource.ValueMember = "Value"
        cmbSource.DisplayMember = "Text"
    End Sub

    Private Sub LoadUsersCombo()
        Try
            Dim dt As New DataTable()

            Dim sql As String =
"
SELECT CAST(NULL AS INT) AS USER_ID, N'الكل' AS USERNAME
UNION ALL
SELECT USER_ID, USERNAME
FROM dbo.USERS
ORDER BY USERNAME;
"

            Using con As New SqlConnection(_connectionString)
                Using da As New SqlDataAdapter(sql, con)
                    da.Fill(dt)
                End Using
            End Using

            cmbUser.DataSource = dt
            cmbUser.ValueMember = "USER_ID"
            cmbUser.DisplayMember = "USERNAME"

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ في تحميل المستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

#Region "Load Repost Monitor"

    Private Sub LoadRepostMonitor()
        Try
            lblStatusMessage.Text = "جاري تحميل سجل التعديلات..."
            Application.DoEvents()

            'Dim sql As String =
            '          "SELECT [SourceTable]
            '    ,[T_ID]
            '    ,[Date]
            '    ,[BsType_ID]
            '    ,[Type_Name]
            '    ,[AG_NAME]
            '    ,[Tr_Name]
            '    ,[Total]
            '    ,[Discount]
            '    ,[Pure]
            '    ,[isDepended]
            '    ,[isVoid]
            '    ,[JournalId]
            '    ,[PostedAt]
            '    ,[PostedBy]
            '    ,[NeedRepost]
            '    ,[OriginalJournalId]
            '    ,[LastReversalJournalId]
            '    ,[EditVersion]
            '    ,[NeedCancelReverse]
            '    ,[VoidReversalJournalId]
            '    ,[VoidAt]
            '    ,[VoidBy]
            '    ,[VoidReason]
            '    ,[PostingStatus]
            '    ,[PostingAction]
            '    ,[NeedsAccountingAction]
            '    ,[Receipt_Title]
            '    ,[About]
            'FROM [dbo].[V_AccountingPostingMonitor]"

            '"   WHERE RepostAt >= @FromDate
            '     AND RepostAt < DATEADD(DAY, 1, @ToDate)
            '     AND (@SourceTable IS NULL OR SourceTable = @SourceTable)
            '     AND (@BsType_ID IS NULL OR BsType_ID = @BsType_ID)
            '     AND (@User_ID IS NULL OR RepostBy = @User_ID)
            '     AND
            '     (
            '         @SearchText IS NULL
            '         OR CAST(T_ID AS NVARCHAR(50)) LIKE '%' + @SearchText + '%'
            '         OR CAST(OldJournalId AS NVARCHAR(50)) LIKE '%' + @SearchText + '%'
            '         OR CAST(ReversalJournalId AS NVARCHAR(50)) LIKE '%' + @SearchText + '%'
            '         OR CAST(NewJournalId AS NVARCHAR(50)) LIKE '%' + @SearchText + '%'
            '         OR ISNULL(Type_Name, '') LIKE '%' + @SearchText + '%'
            '         OR ISNULL(AG_NAME, '') LIKE '%' + @SearchText + '%'
            '         OR ISNULL(Tr_NAME, '') LIKE '%' + @SearchText + '%'
            '         OR ISNULL(Receipt_Title, '') LIKE '%' + @SearchText + '%'
            '         OR ISNULL(About, '') LIKE '%' + @SearchText + '%'
            '         OR ISNULL(RepostReason, '') LIKE '%' + @SearchText + '%'
            '         OR ISNULL(RepostUserName, '') LIKE '%' + @SearchText + '%'
            '     )
            '   ORDER BY RepostAt DESC, RepostLogID DESC;"



            Dim sql As String =
            "
            SELECT
                OperationType,
                OperationName,
                LogID,
                SourceTable,
                SourceName,
                T_ID,
                DocDate,
                BsType_ID,
                Type_Name,
                AG_NAME,
                Tr_NAME,
                Total,
                Discount,
                Pure,
                Receipt_Title,
                About,
                EditVersion,

                OldJournalId,
                OldJournalNumber,
                OldJournalDate,

                ReversalJournalId,
                ReversalJournalNumber,
                ReversalJournalDate,

                NewJournalId,
                NewJournalNumber,
                NewJournalDate,

                ReasonText,
                OperationAt,
                OperationBy,
                OperationUserName
            FROM dbo.V_AccountingAdjustmentMonitor
            WHERE OperationAt >= @FromDate
              AND OperationAt < DATEADD(DAY, 1, @ToDate)
              AND (@SourceTable IS NULL OR SourceTable = @SourceTable)
              AND (@BsType_ID IS NULL OR BsType_ID = @BsType_ID)
              AND (@User_ID IS NULL OR OperationBy = @User_ID)
              AND
              (
                  @SearchText IS NULL
                  OR CAST(T_ID AS NVARCHAR(50)) LIKE '%' + @SearchText + '%'
                  OR CAST(OldJournalId AS NVARCHAR(50)) LIKE '%' + @SearchText + '%'
                  OR CAST(ReversalJournalId AS NVARCHAR(50)) LIKE '%' + @SearchText + '%'
                  OR CAST(ISNULL(NewJournalId, 0) AS NVARCHAR(50)) LIKE '%' + @SearchText + '%'
                  OR ISNULL(Type_Name, '') LIKE '%' + @SearchText + '%'
                  OR ISNULL(AG_NAME, '') LIKE '%' + @SearchText + '%'
                  OR ISNULL(Tr_NAME, '') LIKE '%' + @SearchText + '%'
                  OR ISNULL(Receipt_Title, '') LIKE '%' + @SearchText + '%'
                  OR ISNULL(About, '') LIKE '%' + @SearchText + '%'
                  OR ISNULL(ReasonText, '') LIKE '%' + @SearchText + '%'
                  OR ISNULL(OperationUserName, '') LIKE '%' + @SearchText + '%'
                  OR ISNULL(OperationName, '') LIKE '%' + @SearchText + '%'
              )
            ORDER BY OperationAt DESC, LogID DESC;
            "

            Dim dt As New DataTable()

            Using con As New SqlConnection(_connectionString)
                Using da As New SqlDataAdapter(sql, con)

                    da.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = dtpFrom.Value.Date
                    da.SelectCommand.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = dtpTo.Value.Date
                    da.SelectCommand.Parameters.Add("@SourceTable", SqlDbType.NVarChar, 100).Value = GetNullableComboValue(cmbSource)
                    da.SelectCommand.Parameters.Add("@BsType_ID", SqlDbType.Int).Value = GetNullableComboValue(cmbType)
                    da.SelectCommand.Parameters.Add("@User_ID", SqlDbType.Int).Value = GetNullableComboValue(cmbUser)

                    Dim searchValue As Object = DBNull.Value
                    If txtSearch.Text.Trim() <> "" Then
                        searchValue = txtSearch.Text.Trim()
                    End If

                    da.SelectCommand.Parameters.Add("@SearchText", SqlDbType.NVarChar, 200).Value = searchValue

                    da.Fill(dt)
                End Using
            End Using

            dgvRepost.DataSource = dt
            FormatRepostGrid()
            UpdateCounters(dt)

            dgvJournal.DataSource = Nothing
            lblStatusMessage.Text = "تم تحميل سجل التعديلات"

        Catch ex As Exception
            lblStatusMessage.Text = "حدث خطأ"
            MessageBox.Show(ex.Message, "خطأ في تحميل سجل التعديلات", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub FormatRepostGrid()
        If dgvRepost.Columns.Count = 0 Then Return

        'SetHeader("RepostLogID", "رقم السجل", 80)
        SetHeader("SourceName", "المصدر", 120)
        SetHeader("T_ID", "رقم الحركة", 90)
        SetHeader("DocDate", "تاريخ المستند", 110)
        SetHeader("Type_Name", "نوع الحركة", 160)
        SetHeader("AG_NAME", "الطرف", 140)
        SetHeader("Tr_NAME", "الخزينة", 120)
        SetHeader("Total", "الإجمالي", 110)
        SetHeader("Discount", "الخصم", 95)
        SetHeader("Pure", "الصافي", 110)
        SetHeader("Receipt_Title", "البيان", 230)
        SetHeader("EditVersion", "نسخة التعديل", 90)

        SetHeader("OldJournalId", "القيد القديم", 90)
        SetHeader("OldJournalNumber", "رقم القديم", 110)

        SetHeader("ReversalJournalId", "القيد العكسي", 95)
        SetHeader("ReversalJournalNumber", "رقم العكسي", 110)

        SetHeader("NewJournalId", "القيد الجديد", 90)
        SetHeader("NewJournalNumber", "رقم الجديد", 110)

        'SetHeader("RepostReason", "سبب التعديل", 220)
        'SetHeader("RepostAt", "تاريخ إعادة التقييد", 150)
        'SetHeader("RepostUserName", "المستخدم", 130)


        SetHeader("OperationName", "نوع العملية", 150)
        SetHeader("LogID", "رقم السجل", 80)
        SetHeader("ReasonText", "السبب", 220)
        SetHeader("OperationAt", "تاريخ العملية", 150)
        SetHeader("OperationUserName", "المستخدم", 130)



        HideColumn("SourceTable")
        HideColumn("BsType_ID")
        'HideColumn("RepostBy")

        HideColumn("OperationType")
        HideColumn("OperationBy")

        HideColumn("OldJournalDate")
        HideColumn("ReversalJournalDate")
        HideColumn("NewJournalDate")
        HideColumn("About")

        FormatNumericColumn("Total")
        FormatNumericColumn("Discount")
        FormatNumericColumn("Pure")

        If dgvRepost.Columns.Contains("DocDate") Then
            dgvRepost.Columns("DocDate").DefaultCellStyle.Format = "yyyy/MM/dd"
        End If

        'If dgvRepost.Columns.Contains("RepostAt") Then
        '    dgvRepost.Columns("RepostAt").DefaultCellStyle.Format = "yyyy/MM/dd HH:mm"
        'End If

        If dgvRepost.Columns.Contains("OperationAt") Then
            dgvRepost.Columns("OperationAt").DefaultCellStyle.Format = "yyyy/MM/dd HH:mm"
        End If

    End Sub

    Private Sub SetHeader(columnName As String, headerText As String, width As Integer)
        If dgvRepost.Columns.Contains(columnName) Then
            dgvRepost.Columns(columnName).HeaderText = headerText
            dgvRepost.Columns(columnName).Width = width
        End If
    End Sub

    Private Sub HideColumn(columnName As String)
        If dgvRepost.Columns.Contains(columnName) Then
            dgvRepost.Columns(columnName).Visible = False
        End If
    End Sub

    Private Sub FormatNumericColumn(columnName As String)
        If dgvRepost.Columns.Contains(columnName) Then
            dgvRepost.Columns(columnName).DefaultCellStyle.Format = "N3"
            dgvRepost.Columns(columnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub UpdateCounters(dt As DataTable)
        Dim total As Integer = dt.Rows.Count
        Dim mvCount As Integer = 0
        Dim rctCount As Integer = 0

        For Each row As DataRow In dt.Rows
            Dim sourceTable As String = Convert.ToString(row("SourceTable"))

            If sourceTable = "Agents_Balance_MV" Then
                mvCount += 1
            ElseIf sourceTable = "Agents_Balance_MV_RCT" Then
                rctCount += 1
            End If
        Next

        lblTotalCount.Text = "الإجمالي: " & total.ToString()
        lblMvCount.Text = "الفواتير: " & mvCount.ToString()
        lblRctCount.Text = "الإيصالات: " & rctCount.ToString()
    End Sub

#End Region

#Region "Buttons"

    Private Sub btnSearch_Click(sender As Object, e As EventArgs)
        LoadRepostMonitor()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        LoadRepostMonitor()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub btnViewOldJournal_Click(sender As Object, e As EventArgs)
        LoadSelectedJournalDetails("OldJournalId")
    End Sub

    Private Sub btnViewReversalJournal_Click(sender As Object, e As EventArgs)
        LoadSelectedJournalDetails("ReversalJournalId")
    End Sub

    Private Sub btnViewNewJournal_Click(sender As Object, e As EventArgs)
        'LoadSelectedJournalDetails("NewJournalId")

        If dgvRepost.SelectedRows.Count = 0 Then Return

        Dim row As DataGridViewRow = dgvRepost.SelectedRows(0)
        Dim operationType As String = ""

        If dgvRepost.Columns.Contains("OperationType") Then
            operationType = Convert.ToString(row.Cells("OperationType").Value)
        End If

        If operationType = "CANCEL_REVERSE" Then
            MessageBox.Show("عملية الإلغاء لا تحتوي على قيد جديد، يوجد فقط القيد القديم والقيد العكسي.",
                            "تنبيه",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
            Return
        End If

        LoadSelectedJournalDetails("NewJournalId")

    End Sub

#End Region

#Region "Journal Details"

    Private Sub LoadSelectedJournalDetails(journalColumnName As String)
        Try
            If dgvRepost.SelectedRows.Count = 0 Then Return
            If Not dgvRepost.Columns.Contains(journalColumnName) Then Return

            Dim row As DataGridViewRow = dgvRepost.SelectedRows(0)

            If row.Cells(journalColumnName).Value Is DBNull.Value OrElse row.Cells(journalColumnName).Value Is Nothing Then
                dgvJournal.DataSource = Nothing
                MessageBox.Show("لا يوجد رقم قيد في هذا الحقل.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim journalId As Integer = Convert.ToInt32(row.Cells(journalColumnName).Value)

            Dim sql As String =
"
SELECT
    b.T_ID,
    b.B_T_ID AS JournalId,
    b.ACC_CODE,
    at.ACC_NAME,
    b.DEBIT,
    b.CREDIT,
    b.Notes,
    b.Bill_Num,
    b.DATE_IN,
    b.USER_ID
FROM dbo.ACC_BALANCE b
LEFT JOIN dbo.ACCOUNTS_TREE at
    ON at.ACC_CODE = b.ACC_CODE
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
            dgvJournal.Columns("T_ID").Width = 80
        End If

        If dgvJournal.Columns.Contains("JournalId") Then
            dgvJournal.Columns("JournalId").HeaderText = "رقم القيد"
            dgvJournal.Columns("JournalId").Width = 90
        End If

        If dgvJournal.Columns.Contains("ACC_CODE") Then
            dgvJournal.Columns("ACC_CODE").HeaderText = "كود الحساب"
            dgvJournal.Columns("ACC_CODE").Width = 120
        End If

        If dgvJournal.Columns.Contains("ACC_NAME") Then
            dgvJournal.Columns("ACC_NAME").HeaderText = "اسم الحساب"
            dgvJournal.Columns("ACC_NAME").Width = 180
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
            dgvJournal.Columns("Notes").Width = 250
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

    Private Sub dgvRepost_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If e.RowIndex < 0 Then Return
        If dgvRepost.Rows.Count = 0 Then Return

        Dim row As DataGridViewRow = dgvRepost.Rows(e.RowIndex)

        If dgvRepost.Columns.Contains("SourceTable") Then
            Dim sourceTable As String = Convert.ToString(row.Cells("SourceTable").Value)

            If sourceTable = "Agents_Balance_MV" Then
                row.DefaultCellStyle.BackColor = Color.FromArgb(232, 248, 245)
                row.DefaultCellStyle.ForeColor = Color.FromArgb(20, 90, 50)

            ElseIf sourceTable = "Agents_Balance_MV_RCT" Then
                row.DefaultCellStyle.BackColor = Color.FromArgb(235, 245, 255)
                row.DefaultCellStyle.ForeColor = Color.FromArgb(20, 70, 120)
            End If
        End If

        If dgvRepost.Columns(e.ColumnIndex).Name = "SourceTable" Then
            If e.Value IsNot Nothing Then
                If e.Value.ToString() = "Agents_Balance_MV" Then
                    e.Value = "فاتورة"
                    e.FormattingApplied = True
                ElseIf e.Value.ToString() = "Agents_Balance_MV_RCT" Then
                    e.Value = "إيصال"
                    e.FormattingApplied = True
                End If
            End If
        End If


        If dgvRepost.Columns.Contains("OperationType") Then
            Dim operationType As String = Convert.ToString(row.Cells("OperationType").Value)

            Select Case operationType
                Case "REPOST"
                    row.DefaultCellStyle.BackColor = Color.FromArgb(232, 248, 245)
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(20, 90, 50)

                Case "CANCEL_REVERSE"
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 243, 224)
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(120, 70, 0)
            End Select
        End If

    End Sub

    Private Sub dgvRepost_SelectionChanged(sender As Object, e As EventArgs)
        dgvJournal.DataSource = Nothing
    End Sub

    Private Sub dgvRepost_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            LoadSelectedJournalDetails("NewJournalId")
        End If
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            LoadRepostMonitor()
        End If
    End Sub

    Private Sub FrmAccountingRepostMonitor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

#End Region

End Class