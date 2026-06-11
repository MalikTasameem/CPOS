Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms

Partial Class FrmSalesDraftActionLogMonitor

    Private ReadOnly _connectionString As String
    Private LogsDt As DataTable = Nothing
    Private ReadOnly SearchTextTimer As New Timer()
    Private IsInitializing As Boolean = True
    Private _printRows As New List(Of DataRow)()
    Private _printColumns As New List(Of DataGridViewColumn)()
    Private _printRowIndex As Integer = 0
    Private _printPageNumber As Integer = 1
    Private _printDateTime As DateTime

    Public Sub New()
        Me.New(MY_Settings.SqlConStr)
    End Sub

    Public Sub New(connectionString As String)
        InitializeComponent()

        _connectionString = connectionString

        AddHandlers()
        SetDefaultFilters()
        PrepareGrid()
        LoadLookups()
        IsInitializing = False
        LoadLogs()
    End Sub

#Region "Startup"

    Private Sub AddHandlers()
        AddHandler btnSearch.Click, AddressOf btnSearch_Click
        AddHandler btnRefresh.Click, AddressOf btnRefresh_Click
        AddHandler btnClear.Click, AddressOf btnClear_Click
        AddHandler btnClose.Click, AddressOf btnClose_Click
        AddHandler btnPrint.Click, AddressOf btnPrint_Click
        AddHandler dgvLogs.CellFormatting, AddressOf dgvLogs_CellFormatting
        AddHandler txtSearch.KeyDown, AddressOf txtSearch_KeyDown
        AddHandler txtBillNo.KeyDown, AddressOf txtSearch_KeyDown
        AddHandler txtSearch.TextChanged, AddressOf txtSearch_TextChanged
        AddHandler txtBillNo.TextChanged, AddressOf txtSearch_TextChanged
        AddHandler cmbSearchColumn.SelectedIndexChanged, AddressOf cmbSearchColumn_SelectedIndexChanged

        SearchTextTimer.Interval = 450
        AddHandler SearchTextTimer.Tick, AddressOf SearchTextTimer_Tick
    End Sub

    Private Sub SetDefaultFilters()
        dtpFromDate.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
        dtpToDate.Value = DateTime.Now.Date

        dtpFromTime.Value = DateTime.Today
        dtpToTime.Value = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59)
    End Sub

    Private Sub PrepareGrid()
        dgvLogs.EnableHeadersVisualStyles = False
        dgvLogs.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59)
        dgvLogs.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvLogs.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
        dgvLogs.DefaultCellStyle.Font = New Font("Segoe UI Semibold", 9.0!, FontStyle.Bold)
        dgvLogs.DefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42)
        dgvLogs.DefaultCellStyle.SelectionBackColor = Color.FromArgb(37, 99, 235)
        dgvLogs.DefaultCellStyle.SelectionForeColor = Color.White
        dgvLogs.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252)
        dgvLogs.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        dgvLogs.AllowUserToOrderColumns = False
        dgvLogs.RowTemplate.Height = 28
        dgvLogs.RightToLeft = Windows.Forms.RightToLeft.Yes
    End Sub

#End Region

#Region "Lookups"

    Private Sub LoadLookups()
        LoadUsers()
        LoadActionTypes()
        LoadSearchColumns()
    End Sub

    Private Sub LoadSearchColumns()
        Dim dt As New DataTable()
        dt.Columns.Add("SearchColumn", GetType(String))
        dt.Columns.Add("SearchColumnName", GetType(String))

        dt.Rows.Add("ALL", "كل الأعمدة")
        dt.Rows.Add("UserName", "المستخدم")
        dt.Rows.Add("MachineName", "الجهاز")
        dt.Rows.Add("ActionType", "نوع الحركة")
        dt.Rows.Add("ActionDescription", "الوصف")
        dt.Rows.Add("ControlName", "الأداة")
        dt.Rows.Add("OldValue", "القيمة السابقة")
        dt.Rows.Add("NewValue", "القيمة الجديدة")
        dt.Rows.Add("ItemName", "الصنف")
        dt.Rows.Add("DraftId", "رقم المسودة")
        dt.Rows.Add("DraftLogId", "رقم السجل")
        dt.Rows.Add("Final_SB_ID", "رقم الفاتورة")
        dt.Rows.Add("Final_T_ID", "رقم الحركة")
        dt.Rows.Add("Qty", "الكمية")
        dt.Rows.Add("Total", "الإجمالي")

        cmbSearchColumn.DataSource = dt
        cmbSearchColumn.ValueMember = "SearchColumn"
        cmbSearchColumn.DisplayMember = "SearchColumnName"
    End Sub

    Private Sub LoadUsers()
        Try
            Dim dt As New DataTable()
            Dim sql As String =
"
SELECT CAST(NULL AS INT) AS user_id, N'الكل' AS UserName, 0 AS SortOrder
UNION ALL
SELECT user_id, UserName, 1 AS SortOrder
FROM dbo.Users
ORDER BY SortOrder, UserName;
"

            Using con As New SqlConnection(_connectionString)
                Using da As New SqlDataAdapter(sql, con)
                    da.Fill(dt)
                End Using
            End Using

            cmbUser.DataSource = dt
            cmbUser.ValueMember = "user_id"
            cmbUser.DisplayMember = "UserName"

        Catch ex As Exception
            FillFallbackCombo(cmbUser, "user_id", "UserName")
            lblStatus.Text = "تعذر تحميل المستخدمين: " & ex.Message
        End Try
    End Sub

    Private Sub LoadActionTypes()
        Try
            Dim dt As New DataTable()
            Dim sql As String =
"
SELECT CAST(NULL AS NVARCHAR(50)) AS ActionType, N'الكل' AS ActionName, 0 AS SortOrder
UNION ALL
SELECT DISTINCT ActionType, ActionType AS ActionName, 1 AS SortOrder
FROM dbo.SalesDraft_ActionLogs
WHERE ActionType IS NOT NULL
ORDER BY SortOrder, ActionName;
"

            Using con As New SqlConnection(_connectionString)
                Using da As New SqlDataAdapter(sql, con)
                    da.Fill(dt)
                End Using
            End Using

            cmbActionType.DataSource = dt
            cmbActionType.ValueMember = "ActionType"
            cmbActionType.DisplayMember = "ActionName"

        Catch ex As Exception
            FillFallbackCombo(cmbActionType, "ActionType", "ActionName")
            lblStatus.Text = "تعذر تحميل أنواع الحركة: " & ex.Message
        End Try
    End Sub

    Private Sub FillFallbackCombo(combo As ComboBox, valueColumn As String, textColumn As String)
        Dim dt As New DataTable()
        dt.Columns.Add(valueColumn, GetType(Object))
        dt.Columns.Add(textColumn, GetType(String))
        dt.Rows.Add(DBNull.Value, "الكل")

        combo.DataSource = dt
        combo.ValueMember = valueColumn
        combo.DisplayMember = textColumn
    End Sub

#End Region

#Region "Load Logs"

    Private Sub LoadLogs()
        Try
            lblStatus.Text = "جاري تحميل حركات المبيعات..."
            Application.DoEvents()

            Dim fromDateTime As DateTime =
                dtpFromDate.Value.Date.Add(dtpFromTime.Value.TimeOfDay)
            Dim toDateTime As DateTime =
                dtpToDate.Value.Date.Add(dtpToTime.Value.TimeOfDay)

            If toDateTime < fromDateTime Then
                MessageBox.Show("نطاق التاريخ والوقت غير صحيح.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim sql As String =
"
SELECT TOP (9999)
    LogDB_ID,
    DraftLogId,
    DraftId,
    Final_T_ID,
    Final_SB_ID,
    User_ID,
    UserName,
    MachineName,
    ActionDateTime,
    ScreenName,
    ActionType,
    ActionDescription,
    ControlName,
    OldValue,
    NewValue,
    ItemName,
    IM_ID,
    Qty,
    Total,
    CreatedAt
FROM dbo.SalesDraft_ActionLogs
WHERE ActionDateTime >= @FromDateTime
  AND ActionDateTime <= @ToDateTime
  AND (@User_ID IS NULL OR User_ID = @User_ID)
  AND (@ActionType IS NULL OR ActionType = @ActionType)
  AND
  (
      @BillNo IS NULL
      OR Final_SB_ID = @BillNo
      OR Final_T_ID = @BillNo
      OR DraftLogId = @BillNo
  )
  AND
  (
      @SearchText IS NULL
      OR
      (
          @SearchColumn = N'ALL'
          AND
          (
              ISNULL(UserName, '') LIKE '%' + @SearchText + '%'
              OR ISNULL(MachineName, '') LIKE '%' + @SearchText + '%'
              OR ISNULL(ActionType, '') LIKE '%' + @SearchText + '%'
              OR ISNULL(ActionDescription, '') LIKE '%' + @SearchText + '%'
              OR ISNULL(ControlName, '') LIKE '%' + @SearchText + '%'
              OR ISNULL(OldValue, '') LIKE '%' + @SearchText + '%'
              OR ISNULL(NewValue, '') LIKE '%' + @SearchText + '%'
              OR ISNULL(ItemName, '') LIKE '%' + @SearchText + '%'
              OR ISNULL(DraftId, '') LIKE '%' + @SearchText + '%'
              OR CAST(ISNULL(DraftLogId, 0) AS NVARCHAR(50)) LIKE '%' + @SearchText + '%'
              OR CAST(ISNULL(Final_SB_ID, 0) AS NVARCHAR(50)) LIKE '%' + @SearchText + '%'
              OR CAST(ISNULL(Final_T_ID, 0) AS NVARCHAR(50)) LIKE '%' + @SearchText + '%'
              OR CAST(ISNULL(Qty, 0) AS NVARCHAR(50)) LIKE '%' + @SearchText + '%'
              OR CAST(ISNULL(Total, 0) AS NVARCHAR(50)) LIKE '%' + @SearchText + '%'
          )
      )
      OR (@SearchColumn = N'UserName' AND ISNULL(UserName, '') LIKE '%' + @SearchText + '%')
      OR (@SearchColumn = N'MachineName' AND ISNULL(MachineName, '') LIKE '%' + @SearchText + '%')
      OR (@SearchColumn = N'ActionType' AND ISNULL(ActionType, '') LIKE '%' + @SearchText + '%')
      OR (@SearchColumn = N'ActionDescription' AND ISNULL(ActionDescription, '') LIKE '%' + @SearchText + '%')
      OR (@SearchColumn = N'ControlName' AND ISNULL(ControlName, '') LIKE '%' + @SearchText + '%')
      OR (@SearchColumn = N'OldValue' AND ISNULL(OldValue, '') LIKE '%' + @SearchText + '%')
      OR (@SearchColumn = N'NewValue' AND ISNULL(NewValue, '') LIKE '%' + @SearchText + '%')
      OR (@SearchColumn = N'ItemName' AND ISNULL(ItemName, '') LIKE '%' + @SearchText + '%')
      OR (@SearchColumn = N'DraftId' AND ISNULL(DraftId, '') LIKE '%' + @SearchText + '%')
      OR (@SearchColumn = N'DraftLogId' AND CAST(ISNULL(DraftLogId, 0) AS NVARCHAR(50)) LIKE '%' + @SearchText + '%')
      OR (@SearchColumn = N'Final_SB_ID' AND CAST(ISNULL(Final_SB_ID, 0) AS NVARCHAR(50)) LIKE '%' + @SearchText + '%')
      OR (@SearchColumn = N'Final_T_ID' AND CAST(ISNULL(Final_T_ID, 0) AS NVARCHAR(50)) LIKE '%' + @SearchText + '%')
      OR (@SearchColumn = N'Qty' AND CAST(ISNULL(Qty, 0) AS NVARCHAR(50)) LIKE '%' + @SearchText + '%')
      OR (@SearchColumn = N'Total' AND CAST(ISNULL(Total, 0) AS NVARCHAR(50)) LIKE '%' + @SearchText + '%')
  )
ORDER BY ActionDateTime ASC, LogDB_ID ASC;
"

            Dim dt As New DataTable()

            Using con As New SqlConnection(_connectionString)
                Using da As New SqlDataAdapter(sql, con)
                    da.SelectCommand.Parameters.Add("@FromDateTime", SqlDbType.DateTime).Value = fromDateTime
                    da.SelectCommand.Parameters.Add("@ToDateTime", SqlDbType.DateTime).Value = toDateTime
                    da.SelectCommand.Parameters.Add("@User_ID", SqlDbType.Int).Value = GetNullableIntegerComboValue(cmbUser)
                    da.SelectCommand.Parameters.Add("@ActionType", SqlDbType.NVarChar, 50).Value = GetNullableStringComboValue(cmbActionType)
                    da.SelectCommand.Parameters.Add("@BillNo", SqlDbType.Int).Value = GetNullableIntegerTextValue(txtBillNo)
                    da.SelectCommand.Parameters.Add("@SearchColumn", SqlDbType.NVarChar, 50).Value = GetSearchColumnValue()
                    da.SelectCommand.Parameters.Add("@SearchText", SqlDbType.NVarChar, 200).Value = GetNullableTextValue(txtSearch)

                    da.Fill(dt)
                End Using
            End Using

            LogsDt = dt
            dgvLogs.DataSource = LogsDt

            FormatLogsGrid()
            UpdateSummary(LogsDt)

            lblStatus.Text = "تم تحميل البيانات"

        Catch ex As Exception
            lblStatus.Text = "حدث خطأ أثناء تحميل السجل"
            MessageBox.Show(ex.Message, "خطأ في تحميل حركات المبيعات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetNullableIntegerComboValue(combo As ComboBox) As Object
        If combo Is Nothing OrElse combo.SelectedValue Is Nothing Then Return DBNull.Value
        If combo.SelectedValue Is DBNull.Value Then Return DBNull.Value

        Dim id As Integer
        If Integer.TryParse(combo.SelectedValue.ToString(), id) Then Return id

        Return DBNull.Value
    End Function

    Private Function GetNullableStringComboValue(combo As ComboBox) As Object
        If combo Is Nothing OrElse combo.SelectedValue Is Nothing Then Return DBNull.Value
        If combo.SelectedValue Is DBNull.Value Then Return DBNull.Value

        Dim value As String = combo.SelectedValue.ToString().Trim()
        If value = "" Then Return DBNull.Value

        Return value
    End Function

    Private Function GetNullableIntegerTextValue(textBox As TextBox) As Object
        If textBox Is Nothing OrElse textBox.Text.Trim() = "" Then Return DBNull.Value

        Dim id As Integer
        If Integer.TryParse(textBox.Text.Trim(), id) Then Return id

        Return DBNull.Value
    End Function

    Private Function GetNullableTextValue(textBox As TextBox) As Object
        If textBox Is Nothing OrElse textBox.Text.Trim() = "" Then Return DBNull.Value
        Return textBox.Text.Trim()
    End Function

    Private Function GetSearchColumnValue() As String
        If cmbSearchColumn Is Nothing OrElse cmbSearchColumn.SelectedValue Is Nothing Then Return "ALL"
        If cmbSearchColumn.SelectedValue Is DBNull.Value Then Return "ALL"

        Dim value As String = cmbSearchColumn.SelectedValue.ToString().Trim()
        If value = "" Then Return "ALL"

        Return value
    End Function

#End Region

#Region "Formatting"

    Private Sub FormatLogsGrid()
        If dgvLogs.DataSource Is Nothing Then Exit Sub

        For Each column As DataGridViewColumn In dgvLogs.Columns
            column.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        SetGridHeader("DraftLogId", "رقم السجل", 80)
        SetGridHeader("Final_SB_ID", "رقم الفاتورة", 95)
        SetGridHeader("UserName", "المستخدم", 135)
        SetGridHeader("MachineName", "الجهاز", 120)
        SetGridHeader("ActionDateTime", "التاريخ والوقت", 150)
        SetGridHeader("ActionType", "نوع الحركة", 100)
        SetGridHeader("ActionDescription", "الوصف", 320)
        SetGridHeader("ControlName", "الأداة", 120)
        SetGridHeader("OldValue", "القيمة السابقة", 145)
        SetGridHeader("NewValue", "القيمة الجديدة", 145)
        SetGridHeader("ItemName", "الصنف", 160)
        SetGridHeader("Qty", "الكمية", 85)
        SetGridHeader("Total", "الإجمالي", 95)
        SetGridHeader("CreatedAt", "وقت الترحيل", 150)

        HideGridColumn("LogDB_ID")
        HideGridColumn("DraftId")
        HideGridColumn("Final_T_ID")
        HideGridColumn("User_ID")
        HideGridColumn("ScreenName")
        HideGridColumn("IM_ID")

        If dgvLogs.Columns.Contains("ActionDateTime") Then
            dgvLogs.Columns("ActionDateTime").DefaultCellStyle.Format = "yyyy/MM/dd HH:mm:ss"
        End If

        If dgvLogs.Columns.Contains("CreatedAt") Then
            dgvLogs.Columns("CreatedAt").DefaultCellStyle.Format = "yyyy/MM/dd HH:mm:ss"
        End If

        If dgvLogs.Columns.Contains("Qty") Then
            dgvLogs.Columns("Qty").DefaultCellStyle.Format = "N3"
            dgvLogs.Columns("Qty").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If

        If dgvLogs.Columns.Contains("Total") Then
            dgvLogs.Columns("Total").DefaultCellStyle.Format = "N3"
            dgvLogs.Columns("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If

        If dgvLogs.Columns.Contains("ActionDescription") Then
            dgvLogs.Columns("ActionDescription").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            dgvLogs.Columns("ActionDescription").MinimumWidth = 260
        End If
    End Sub

    Private Sub SetGridHeader(columnName As String, headerText As String, width As Integer)
        If dgvLogs.Columns.Contains(columnName) = False Then Exit Sub

        dgvLogs.Columns(columnName).HeaderText = headerText
        dgvLogs.Columns(columnName).Width = width
        dgvLogs.Columns(columnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    End Sub

    Private Sub HideGridColumn(columnName As String)
        If dgvLogs.Columns.Contains(columnName) Then dgvLogs.Columns(columnName).Visible = False
    End Sub

    Private Sub dgvLogs_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If e.RowIndex < 0 OrElse dgvLogs.Columns.Contains("ActionType") = False Then Exit Sub

        Dim actionType As String = Convert.ToString(dgvLogs.Rows(e.RowIndex).Cells("ActionType").Value)
        Dim rowBackColor As Color = Color.White
        Dim rowForeColor As Color = Color.FromArgb(15, 23, 42)

        Select Case actionType
            Case "إضافة"
                rowBackColor = Color.FromArgb(220, 252, 231)
                rowForeColor = Color.FromArgb(22, 101, 52)
            Case "حذف"
                rowBackColor = Color.FromArgb(254, 226, 226)
                rowForeColor = Color.FromArgb(153, 27, 27)
            Case "تعديل", "كمية", "وحدة", "خصم", "عميل"
                rowBackColor = Color.FromArgb(219, 234, 254)
                rowForeColor = Color.FromArgb(30, 64, 175)
            Case "حفظ", "طباعة"
                rowBackColor = Color.FromArgb(254, 249, 195)
                rowForeColor = Color.FromArgb(133, 77, 14)
            Case "فتح", "زر"
                rowBackColor = Color.FromArgb(241, 245, 249)
                rowForeColor = Color.FromArgb(51, 65, 85)
            Case "إغلاق"
                rowBackColor = Color.FromArgb(243, 232, 255)
                rowForeColor = Color.FromArgb(88, 28, 135)
        End Select

        Dim selectionBackColor As Color = ControlPaint.Dark(rowBackColor)

        dgvLogs.Rows(e.RowIndex).DefaultCellStyle.BackColor = rowBackColor
        dgvLogs.Rows(e.RowIndex).DefaultCellStyle.ForeColor = rowForeColor
        dgvLogs.Rows(e.RowIndex).DefaultCellStyle.SelectionBackColor = selectionBackColor
        dgvLogs.Rows(e.RowIndex).DefaultCellStyle.SelectionForeColor = GetReadableSelectionForeColor(selectionBackColor)
    End Sub

    Private Function GetReadableSelectionForeColor(backColor As Color) As Color
        Dim brightness As Double = (backColor.R * 0.299) + (backColor.G * 0.587) + (backColor.B * 0.114)

        If brightness < 145 Then Return Color.White

        Return Color.FromArgb(15, 23, 42)
    End Function

#End Region

#Region "Summary"

    Private Sub UpdateSummary(dt As DataTable)
        If dt Is Nothing Then
            lblCountValue.Text = "0"
            lblUsersValue.Text = "0"
            lblPeriodValue.Text = "-"
            lblActionSummaryValue.Text = "-"
            Exit Sub
        End If

        Dim users As New HashSet(Of Integer)()
        Dim actionCounts As New Dictionary(Of String, Integer)()
        Dim firstDate As DateTime = DateTime.MaxValue
        Dim lastDate As DateTime = DateTime.MinValue

        For Each row As DataRow In dt.Rows
            If row.Table.Columns.Contains("User_ID") AndAlso row("User_ID") IsNot DBNull.Value Then
                users.Add(Convert.ToInt32(row("User_ID")))
            End If

            Dim actionType As String = ""
            If row.Table.Columns.Contains("ActionType") AndAlso row("ActionType") IsNot DBNull.Value Then
                actionType = row("ActionType").ToString()
            End If

            If actionType <> "" Then
                If actionCounts.ContainsKey(actionType) = False Then actionCounts.Add(actionType, 0)
                actionCounts(actionType) += 1
            End If

            If row.Table.Columns.Contains("ActionDateTime") AndAlso row("ActionDateTime") IsNot DBNull.Value Then
                Dim currentDate As DateTime = Convert.ToDateTime(row("ActionDateTime"))
                If currentDate < firstDate Then firstDate = currentDate
                If currentDate > lastDate Then lastDate = currentDate
            End If
        Next

        lblCountValue.Text = dt.Rows.Count.ToString("N0")
        lblUsersValue.Text = users.Count.ToString("N0")

        If dt.Rows.Count > 0 Then
            lblPeriodValue.Text = firstDate.ToString("yyyy/MM/dd HH:mm") & " - " & lastDate.ToString("yyyy/MM/dd HH:mm")
        Else
            lblPeriodValue.Text = "-"
        End If

        lblActionSummaryValue.Text = BuildActionSummary(actionCounts)
    End Sub

    Private Function BuildActionSummary(actionCounts As Dictionary(Of String, Integer)) As String
        If actionCounts Is Nothing OrElse actionCounts.Count = 0 Then Return "-"

        Dim parts As New List(Of String)()

        For Each actionItem As KeyValuePair(Of String, Integer) In actionCounts.OrderByDescending(Function(x) x.Value).Take(5)
            parts.Add(actionItem.Key & ": " & actionItem.Value.ToString("N0"))
        Next

        Return String.Join("   |   ", parts)
    End Function

#End Region

#Region "Printing"

    Private Sub PrintCurrentLogs()
        If LogsDt Is Nothing OrElse LogsDt.Rows.Count = 0 Then
            MessageBox.Show("لا توجد بيانات للطباعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        _printColumns = GetPrintableLogColumns()
        If _printColumns.Count = 0 Then
            MessageBox.Show("لا توجد أعمدة ظاهرة للطباعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If LogsDt.Columns.Contains("ActionDateTime") AndAlso LogsDt.Columns.Contains("LogDB_ID") Then
            _printRows = LogsDt.Select("", "ActionDateTime ASC, LogDB_ID ASC").ToList()
        Else
            _printRows = LogsDt.Select().ToList()
        End If

        Using printDocument As PrintDocument = CreateLogsPrintDocument()
            Using previewDialog As New PrintPreviewDialog()
                previewDialog.Document = printDocument
                previewDialog.WindowState = FormWindowState.Maximized
                previewDialog.Text = "معاينة حركات المبيعات"
                previewDialog.ShowDialog(Me)
            End Using
        End Using
    End Sub

    Private Function CreateLogsPrintDocument() As PrintDocument
        Dim printDocument As New PrintDocument()
        printDocument.DocumentName = "مراقبة حركات المبيعات"
        printDocument.DefaultPageSettings.Landscape = True
        printDocument.DefaultPageSettings.Margins = New Margins(30, 30, 40, 45)

        AddHandler printDocument.BeginPrint, AddressOf LogsPrintDocument_BeginPrint
        AddHandler printDocument.PrintPage, AddressOf LogsPrintDocument_PrintPage

        Return printDocument
    End Function

    Private Sub LogsPrintDocument_BeginPrint(sender As Object, e As PrintEventArgs)
        _printRowIndex = 0
        _printPageNumber = 1
        _printDateTime = Date.Now
    End Sub

    Private Sub LogsPrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)
        Dim bounds As Rectangle = e.MarginBounds
        Dim y As Integer = bounds.Top

        Using storeTitleFont As New Font("Segoe UI", 14.0!, FontStyle.Bold),
              storeSubTitleFont As New Font("Segoe UI", 9.5!, FontStyle.Bold),
              titleFont As New Font("Segoe UI", 12.0!, FontStyle.Bold),
              infoFont As New Font("Segoe UI", 8.0!, FontStyle.Bold),
              headerFont As New Font("Segoe UI", 6.8!, FontStyle.Bold),
              rowFont As New Font("Segoe UI Semibold", 6.7!, FontStyle.Bold)

            Using rtlFormat As New StringFormat(),
                  centerFormat As New StringFormat(),
                  wrapFormat As New StringFormat()

                rtlFormat.Alignment = StringAlignment.Far
                rtlFormat.LineAlignment = StringAlignment.Center
                rtlFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft
                rtlFormat.Trimming = StringTrimming.EllipsisCharacter

                centerFormat.Alignment = StringAlignment.Center
                centerFormat.LineAlignment = StringAlignment.Center
                centerFormat.Trimming = StringTrimming.EllipsisCharacter

                wrapFormat.Alignment = StringAlignment.Far
                wrapFormat.LineAlignment = StringAlignment.Center
                wrapFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft
                wrapFormat.Trimming = StringTrimming.Word

                DrawLogsPrintStoreHeader(e.Graphics, bounds, y, storeTitleFont, storeSubTitleFont, centerFormat)

                e.Graphics.DrawString("تقرير مراقبة حركات المبيعات المسودة", titleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 28), centerFormat)
                y += 30

                e.Graphics.DrawString(GetLogsPrintFilterText(), infoFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 24), rtlFormat)
                y += 30

                Dim rowHeight As Integer = 24
                Dim widths As List(Of Integer) = CalculateLogColumnWidths(_printColumns, bounds.Width)
                Dim x As Integer = bounds.Right

                For i As Integer = 0 To _printColumns.Count - 1
                    x -= widths(i)
                    Dim rect As New Rectangle(x, y, widths(i), rowHeight)

                    Using backBrush As New SolidBrush(Color.FromArgb(30, 41, 59))
                        e.Graphics.FillRectangle(backBrush, rect)
                    End Using

                    e.Graphics.DrawRectangle(Pens.DarkGray, rect)
                    e.Graphics.DrawString(GetLogColumnHeader(_printColumns(i)), headerFont, Brushes.White, rect, centerFormat)
                Next

                y += rowHeight

                Dim firstRowY As Integer = y
                Dim totalPages As Integer = CalculateLogPrintTotalPages(e.Graphics, _printColumns, widths, rowFont, firstRowY, bounds.Bottom, rowHeight)
                Dim currentPage As Integer = _printPageNumber

                While _printRowIndex < _printRows.Count
                    Dim row As DataRow = _printRows(_printRowIndex)
                    Dim currentRowHeight As Integer = CalculateLogPrintRowHeight(e.Graphics, row, _printColumns, widths, rowFont, rowHeight)
                    Dim maxRowHeight As Integer = Math.Max(rowHeight, bounds.Bottom - 58 - firstRowY)

                    If currentRowHeight > maxRowHeight Then currentRowHeight = maxRowHeight

                    If y + currentRowHeight > bounds.Bottom - 58 AndAlso y > firstRowY Then
                        DrawLogsPrintFooter(e.Graphics, bounds, currentPage, totalPages, infoFont, centerFormat)
                        _printPageNumber += 1
                        e.HasMorePages = True
                        Return
                    End If

                    x = bounds.Right

                    For i As Integer = 0 To _printColumns.Count - 1
                        x -= widths(i)
                        Dim rect As New Rectangle(x, y, widths(i), currentRowHeight)

                        If _printRowIndex Mod 2 = 0 Then
                            e.Graphics.FillRectangle(Brushes.White, rect)
                        Else
                            Using altBrush As New SolidBrush(Color.FromArgb(248, 250, 252))
                                e.Graphics.FillRectangle(altBrush, rect)
                            End Using
                        End If

                        e.Graphics.DrawRectangle(Pens.LightGray, rect)

                        Dim cellRect As New Rectangle(rect.Left + 3, rect.Top + 2, rect.Width - 6, rect.Height - 4)
                        If IsLogWrappingColumn(_printColumns(i)) Then
                            e.Graphics.DrawString(GetLogPrintCellText(row, _printColumns(i)), rowFont, Brushes.Black, cellRect, wrapFormat)
                        ElseIf IsLogNumericColumn(_printColumns(i)) Then
                            e.Graphics.DrawString(GetLogPrintCellText(row, _printColumns(i)), rowFont, Brushes.Black, cellRect, centerFormat)
                        Else
                            e.Graphics.DrawString(GetLogPrintCellText(row, _printColumns(i)), rowFont, Brushes.Black, cellRect, rtlFormat)
                        End If
                    Next

                    y += currentRowHeight
                    _printRowIndex += 1
                End While

                DrawLogsPrintFooter(e.Graphics, bounds, currentPage, totalPages, infoFont, centerFormat)
            End Using
        End Using

        e.HasMorePages = False
    End Sub

    Private Function GetPrintableLogColumns() As List(Of DataGridViewColumn)
        Dim columns As New List(Of DataGridViewColumn)()

        For Each col As DataGridViewColumn In dgvLogs.Columns
            If col.Visible Then columns.Add(col)
        Next

        Return columns.OrderBy(Function(col) col.DisplayIndex).ToList()
    End Function

    Private Function CalculateLogColumnWidths(columns As List(Of DataGridViewColumn), availableWidth As Integer) As List(Of Integer)
        Dim widths As New List(Of Integer)()
        Dim totalWeight As Integer = 0

        For Each col As DataGridViewColumn In columns
            totalWeight += GetLogPrintColumnWeight(col)
        Next

        If totalWeight <= 0 Then totalWeight = columns.Count * 70

        Dim usedWidth As Integer = 0

        For i As Integer = 0 To columns.Count - 1
            Dim width As Integer

            If i = columns.Count - 1 Then
                width = Math.Max(28, availableWidth - usedWidth)
            Else
                width = Math.Max(28, CInt(availableWidth * (GetLogPrintColumnWeight(columns(i)) / CDbl(totalWeight))))
            End If

            widths.Add(width)
            usedWidth += width
        Next

        Return widths
    End Function

    Private Function GetLogPrintColumnWeight(column As DataGridViewColumn) As Integer
        Dim columnName As String = GetLogDataColumnName(column)

        Select Case columnName
            Case "ActionDescription"
                Return 260
            Case "OldValue", "NewValue", "ItemName"
                Return 125
            Case "ActionDateTime", "CreatedAt"
                Return 120
            Case "UserName", "MachineName", "ControlName"
                Return 95
            Case "ActionType", "Final_SB_ID"
                Return 75
            Case "DraftLogId", "Qty", "Total"
                Return 65
        End Select

        Return Math.Max(50, column.Width)
    End Function

    Private Function GetLogColumnHeader(column As DataGridViewColumn) As String
        If Not String.IsNullOrWhiteSpace(column.HeaderText) Then Return column.HeaderText.Trim()
        Return column.Name
    End Function

    Private Function GetLogPrintCellText(row As DataRow, column As DataGridViewColumn) As String
        Dim columnName As String = GetLogDataColumnName(column)
        If row.Table.Columns.Contains(columnName) = False Then Return ""
        If row(columnName) Is DBNull.Value Then Return ""

        Dim value As Object = row(columnName)

        If columnName = "ActionDateTime" OrElse columnName = "CreatedAt" Then
            Dim dateValue As DateTime
            If DateTime.TryParse(value.ToString(), dateValue) Then Return dateValue.ToString("yyyy/MM/dd HH:mm:ss")
        End If

        If columnName = "Qty" OrElse columnName = "Total" Then
            Dim numberValue As Decimal
            If Decimal.TryParse(value.ToString(), numberValue) Then Return numberValue.ToString("N3")
        End If

        Return value.ToString()
    End Function

    Private Function GetLogDataColumnName(column As DataGridViewColumn) As String
        If column Is Nothing Then Return ""
        If Not String.IsNullOrWhiteSpace(column.DataPropertyName) Then Return column.DataPropertyName.Trim()
        Return column.Name.Trim()
    End Function

    Private Function IsLogWrappingColumn(column As DataGridViewColumn) As Boolean
        Dim columnName As String = GetLogDataColumnName(column)
        Return columnName = "ActionDescription" OrElse columnName = "OldValue" OrElse columnName = "NewValue" OrElse columnName = "ItemName"
    End Function

    Private Function IsLogNumericColumn(column As DataGridViewColumn) As Boolean
        Dim columnName As String = GetLogDataColumnName(column)
        Return columnName = "DraftLogId" OrElse columnName = "Final_SB_ID" OrElse columnName = "Qty" OrElse columnName = "Total"
    End Function

    Private Function CalculateLogPrintRowHeight(graphics As Graphics, row As DataRow, columns As List(Of DataGridViewColumn), widths As List(Of Integer), rowFont As Font, baseRowHeight As Integer) As Integer
        Dim rowHeight As Integer = baseRowHeight

        Using wrapFormat As New StringFormat()
            wrapFormat.Alignment = StringAlignment.Far
            wrapFormat.LineAlignment = StringAlignment.Center
            wrapFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft
            wrapFormat.Trimming = StringTrimming.Word

            For i As Integer = 0 To columns.Count - 1
                If IsLogWrappingColumn(columns(i)) = False Then Continue For

                Dim text As String = GetLogPrintCellText(row, columns(i))
                If String.IsNullOrWhiteSpace(text) Then Continue For

                Dim textSize As SizeF = graphics.MeasureString(text, rowFont, Math.Max(28, widths(i) - 6), wrapFormat)
                rowHeight = Math.Max(rowHeight, CInt(Math.Ceiling(textSize.Height)) + 8)
            Next
        End Using

        Return Math.Min(rowHeight, 76)
    End Function

    Private Function CalculateLogPrintTotalPages(graphics As Graphics, columns As List(Of DataGridViewColumn), widths As List(Of Integer), rowFont As Font, firstRowY As Integer, pageBottom As Integer, baseRowHeight As Integer) As Integer
        Dim pages As Integer = 1
        Dim y As Integer = firstRowY
        Dim printableBottom As Integer = pageBottom - 58

        For Each row As DataRow In _printRows
            Dim rowHeight As Integer = CalculateLogPrintRowHeight(graphics, row, columns, widths, rowFont, baseRowHeight)

            If y + rowHeight > printableBottom AndAlso y > firstRowY Then
                pages += 1
                y = firstRowY
            End If

            y += rowHeight
        Next

        Return Math.Max(1, pages)
    End Function

    Private Sub DrawLogsPrintStoreHeader(graphics As Graphics, bounds As Rectangle, ByRef y As Integer, storeTitleFont As Font, storeSubTitleFont As Font, centerFormat As StringFormat)
        If Not String.IsNullOrWhiteSpace(SBill_Title_1) Then
            graphics.DrawString(SBill_Title_1, storeTitleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 28), centerFormat)
            y += 28
        End If

        If Not String.IsNullOrWhiteSpace(SBill_Title_2) Then
            graphics.DrawString(SBill_Title_2, storeSubTitleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 22), centerFormat)
            y += 22
        End If
    End Sub

    Private Function GetLogsPrintFilterText() As String
        Dim fromDateTime As DateTime = dtpFromDate.Value.Date.Add(dtpFromTime.Value.TimeOfDay)
        Dim toDateTime As DateTime = dtpToDate.Value.Date.Add(dtpToTime.Value.TimeOfDay)

        Return "الفترة: من " & fromDateTime.ToString("yyyy/MM/dd HH:mm:ss") &
            " إلى " & toDateTime.ToString("yyyy/MM/dd HH:mm:ss") &
            "    المستخدم: " & GetComboDisplayText(cmbUser) &
            "    نوع الحركة: " & GetComboDisplayText(cmbActionType) &
            "    عدد السجلات: " & _printRows.Count.ToString("N0")
    End Function

    Private Function GetComboDisplayText(combo As ComboBox) As String
        If combo Is Nothing OrElse combo.SelectedIndex < 0 Then Return "الكل"

        Dim text As String = combo.Text.Trim()
        If text = "" Then Return "الكل"

        Return text
    End Function

    Private Sub DrawLogsPrintFooter(graphics As Graphics, bounds As Rectangle, currentPage As Integer, totalPages As Integer, footerFont As Font, centerFormat As StringFormat)
        If _printDateTime = DateTime.MinValue Then _printDateTime = Date.Now

        Dim footerTop As Integer = bounds.Bottom - 26
        Dim sideWidth As Integer = CInt(bounds.Width * 0.34)
        Dim centerWidth As Integer = bounds.Width - (sideWidth * 2)

        Using rightFormat As New StringFormat(),
              leftFormat As New StringFormat()

            rightFormat.Alignment = StringAlignment.Far
            rightFormat.LineAlignment = StringAlignment.Center
            rightFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft

            leftFormat.Alignment = StringAlignment.Near
            leftFormat.LineAlignment = StringAlignment.Center

            graphics.DrawLine(Pens.LightGray, bounds.Left, footerTop - 4, bounds.Right, footerTop - 4)
            graphics.DrawString("المعد: " & USER_NAME, footerFont, Brushes.Black, New Rectangle(bounds.Right - sideWidth, footerTop, sideWidth, 22), rightFormat)
            graphics.DrawString(currentPage.ToString() & "/" & totalPages.ToString(), footerFont, Brushes.Black, New Rectangle(bounds.Left + sideWidth, footerTop, centerWidth, 22), centerFormat)
            graphics.DrawString("تاريخ الطباعة: " & _printDateTime.ToString("yyyy/MM/dd HH:mm"), footerFont, Brushes.Black, New Rectangle(bounds.Left, footerTop, sideWidth, 22), leftFormat)
        End Using
    End Sub

#End Region

#Region "Events"

    Private Sub btnSearch_Click(sender As Object, e As EventArgs)
        LoadLogs()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        LoadActionTypes()
        LoadLogs()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs)
        SearchTextTimer.Stop()
        SetDefaultFilters()

        If cmbUser.Items.Count > 0 Then cmbUser.SelectedIndex = 0
        If cmbActionType.Items.Count > 0 Then cmbActionType.SelectedIndex = 0
        If cmbSearchColumn.Items.Count > 0 Then cmbSearchColumn.SelectedIndex = 0

        txtSearch.Clear()
        txtBillNo.Clear()

        SearchTextTimer.Stop()
        LoadLogs()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
        PrintCurrentLogs()
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SearchTextTimer.Stop()
            LoadLogs()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs)
        If IsInitializing Then Exit Sub

        SearchTextTimer.Stop()
        SearchTextTimer.Start()
    End Sub

    Private Sub cmbSearchColumn_SelectedIndexChanged(sender As Object, e As EventArgs)
        If IsInitializing Then Exit Sub
        If txtSearch.Text.Trim() = "" Then Exit Sub

        SearchTextTimer.Stop()
        SearchTextTimer.Start()
    End Sub

    Private Sub SearchTextTimer_Tick(sender As Object, e As EventArgs)
        SearchTextTimer.Stop()
        LoadLogs()
    End Sub

    Private Sub FrmSalesDraftActionLogMonitor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


#End Region

End Class
