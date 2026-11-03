Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Globalization
Imports System.Threading.Tasks

Public Class Network_Audit_Browser
    Private reader As AuditCompatibilityReader
    Private capabilities As AuditReadCapabilities
    Private searchBounds As AuditReadBounds
    Private appliedFilter As AuditSearchFilter
    Private pageIndex As Integer
    Private resultCount As Long
    Private busy As Boolean
    Private screenFilterLoaded As Boolean
    Private userFilterLoaded As Boolean
    Private printRows As DataTable
    Private printRowIndex As Integer
    Private printPageNumber As Integer
    Private printFilterDescription As String = ""

    Public Sub New()
        InitializeComponent()
        ResetFilters()
        For Each column As DataGridViewColumn In EventsGrid.Columns
            column.SortMode = DataGridViewColumnSortMode.NotSortable
        Next
    End Sub

    Private Sub ResetFilters()
        FromPicker.Value = New DateTime(Date.Today.Year, Date.Today.Month, 1)
        ToPicker.Value = Date.Today
        KindCombo.SelectedIndex = 0
        OutcomeCombo.SelectedIndex = 0
        DocumentText.Clear()
        If UserCombo.DataSource Is Nothing Then
            If UserCombo.Items.Count = 0 Then UserCombo.Items.Add("كل المستخدمين")
            UserCombo.SelectedIndex = 0
        Else
            UserCombo.SelectedValue = 0
            If UserCombo.SelectedIndex < 0 Then UserCombo.SelectedIndex = 0
        End If
        If ScreenCombo.Items.Count = 0 Then ScreenCombo.Items.Add("كل الشاشات")
        ScreenCombo.SelectedIndex = 0
        NotesText.Clear()
    End Sub

    Private Sub RequireAccess()
        If Not User_isAdmin OrElse USER_ID <= 0 Then Throw New UnauthorizedAccessException("هذه النسخة من شاشة المراقبة الجديدة مخصصة للمدير؛ صلاحيات العرض والتصدير المستقلة ستضاف في مرحلة لاحقة.")
    End Sub

    Private Async Sub Browser_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            RequireAccess()
        Catch ex As UnauthorizedAccessException
            MessageBox.Show(Me, ex.Message, "صلاحية العرض", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Close()
            Return
        End Try
        Await LoadPageAsync(True, 0)
    End Sub

    Private Function ReadFilters() As AuditSearchFilter
        Dim selectedUser As Integer
        If UserCombo.SelectedValue IsNot Nothing AndAlso UserCombo.SelectedValue IsNot DBNull.Value Then
            Integer.TryParse(Convert.ToString(UserCombo.SelectedValue, CultureInfo.InvariantCulture), selectedUser)
        End If
        Return New AuditSearchFilter With {
            .FromDate = FromPicker.Value.Date, .ToDate = ToPicker.Value.Date,
            .RecordKind = "V2",
            .Outcome = OutcomeCombo.SelectedIndex, .UserId = selectedUser,
            .DocumentNumber = DocumentText.Text.Trim(),
            .ScreenText = If(ScreenCombo.SelectedIndex <= 0, "", Convert.ToString(ScreenCombo.SelectedItem, CultureInfo.InvariantCulture).Trim()),
            .SearchText = NotesText.Text.Trim()}
    End Function

    Private Sub SetBusy(value As Boolean)
        busy = value
        FiltersLayout.Enabled = Not value
        CloseButton.Enabled = Not value
        EventsGrid.Enabled = Not value
        PreviousButton.Enabled = Not value AndAlso pageIndex > 0
        NextButton.Enabled = Not value AndAlso (CLng(pageIndex) + 1) * AuditCompatibilityReader.PageSize < resultCount
        DetailsButton.Enabled = Not value AndAlso EventsGrid.CurrentRow IsNot Nothing
        PrintButton.Enabled = Not value AndAlso EventsGrid.Rows.Count > 0
        UseWaitCursor = value
    End Sub

    Private Async Function LoadPageAsync(newSearch As Boolean, requestedPage As Integer) As Task
        If busy Then Return
        Try
            RequireAccess()
            Dim nextFilter As AuditSearchFilter = If(newSearch, ReadFilters(), appliedFilter)
            AuditCompatibilityReader.Validate(nextFilter, requestedPage)
            Dim nextReader As New AuditCompatibilityReader(MY_Settings.SqlConStr, True, False)
            Dim nextCaps As AuditReadCapabilities = capabilities
            Dim nextBounds As AuditReadBounds = searchBounds
            Dim nextScreenCodes As List(Of String) = Nothing
            Dim nextUsers As DataTable = Nothing
            SetBusy(True)
            StatusLabel.Text = "جارٍ قراءة السجل..."
            Dim result As AuditReadPage = Await Task.Run(Function()
                                                            If newSearch Then
                                                                nextCaps = nextReader.GetCapabilities()
                                                                nextBounds = nextReader.CaptureBounds(nextCaps)
                                                                If Not screenFilterLoaded Then nextScreenCodes = nextReader.ReadScreenCodes(nextCaps, nextBounds)
                                                                If Not userFilterLoaded Then nextUsers = nextReader.ReadUsers(nextCaps, nextBounds)
                                                            End If
                                                            Return nextReader.ReadPage(nextFilter, nextCaps, nextBounds, requestedPage)
                                                        End Function)
            If IsDisposed Then Return
            RequireAccess()
            ' Publish all state only after the entire read succeeds. Failed paging retains the old page.
            reader = nextReader
            capabilities = nextCaps
            searchBounds = nextBounds
            appliedFilter = nextFilter
            If nextScreenCodes IsNot Nothing Then PopulateScreenFilter(nextScreenCodes)
            If nextUsers IsNot Nothing Then PopulateUserFilter(nextUsers)
            resultCount = result.Total
            pageIndex = requestedPage
            EventsGrid.DataSource = result.Rows
            PageLabel.Text = String.Format("الصفحة {0} / {1}  |  {2:N0} حدث", pageIndex + 1, Math.Max(1, Math.Ceiling(resultCount / CDbl(AuditCompatibilityReader.PageSize))), resultCount)
            BasisLabel.Text = "السجل الجديد بتوقيت " & capabilities.TimeZone.Id & ". الترتيب من الأقدم إلى الأحدث؛ يشمل المسارات المنقولة فقط."
            StatusLabel.Text = "تمت القراءة — لا تُنشأ أحداث ولا تُعدّل بيانات من هذه الشاشة."
        Catch ex As Exception
            If Not IsDisposed Then StatusLabel.Text = "تعذر التحديث؛ النتائج السابقة باقية: " & ex.Message
        Finally
            If Not IsDisposed Then SetBusy(False)
        End Try
    End Function

    Private Sub PopulateScreenFilter(values As IEnumerable(Of String))
        Dim selected = If(ScreenCombo.SelectedIndex > 0, Convert.ToString(ScreenCombo.SelectedItem, CultureInfo.InvariantCulture), "")
        ScreenCombo.BeginUpdate()
        Try
            ScreenCombo.Items.Clear()
            ScreenCombo.Items.Add("كل الشاشات")
            For Each value In values
                ScreenCombo.Items.Add(value)
            Next
            Dim selectedIndex = If(selected.Length = 0, 0, ScreenCombo.FindStringExact(selected))
            ScreenCombo.SelectedIndex = If(selectedIndex >= 0, selectedIndex, 0)
            screenFilterLoaded = True
        Finally
            ScreenCombo.EndUpdate()
        End Try
    End Sub

    Private Sub PopulateUserFilter(values As DataTable)
        Dim selectedUser As Integer
        If UserCombo.SelectedValue IsNot Nothing AndAlso UserCombo.SelectedValue IsNot DBNull.Value Then
            Integer.TryParse(Convert.ToString(UserCombo.SelectedValue, CultureInfo.InvariantCulture), selectedUser)
        End If
        Dim items As New DataTable()
        items.Columns.Add("UserId", GetType(Integer))
        items.Columns.Add("DisplayName", GetType(String))
        items.Rows.Add(0, "كل المستخدمين")
        For Each row As DataRow In values.Rows
            items.Rows.Add(Convert.ToInt32(row("UserId"), CultureInfo.InvariantCulture), Convert.ToString(row("DisplayName"), CultureInfo.CurrentCulture))
        Next
        UserCombo.DisplayMember = "DisplayName"
        UserCombo.ValueMember = "UserId"
        UserCombo.DataSource = items
        UserCombo.SelectedValue = selectedUser
        If UserCombo.SelectedIndex < 0 Then UserCombo.SelectedIndex = 0
        userFilterLoaded = True
    End Sub

    Private Async Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        Await LoadPageAsync(True, 0)
    End Sub

    Private Sub ResetButton_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
        ResetFilters()
        StatusLabel.Text = "تم ضبط المرشحات؛ اضغط بحث لتطبيقها."
    End Sub

    Private Async Sub PreviousButton_Click(sender As Object, e As EventArgs) Handles PreviousButton.Click
        Await LoadPageAsync(False, pageIndex - 1)
    End Sub

    Private Async Sub NextButton_Click(sender As Object, e As EventArgs) Handles NextButton.Click
        Await LoadPageAsync(False, pageIndex + 1)
    End Sub

    Private Async Sub DetailsButton_Click(sender As Object, e As EventArgs) Handles DetailsButton.Click
        Await OpenDetailsAsync()
    End Sub

    Private Async Sub EventsGrid_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles EventsGrid.CellDoubleClick
        If e.RowIndex >= 0 Then Await OpenDetailsAsync()
    End Sub

    Private Sub EventsGrid_SelectionChanged(sender As Object, e As EventArgs) Handles EventsGrid.SelectionChanged
        If Not busy Then DetailsButton.Enabled = EventsGrid.CurrentRow IsNot Nothing
    End Sub

    Private Async Function OpenDetailsAsync() As Task
        If busy OrElse EventsGrid.CurrentRow Is Nothing OrElse reader Is Nothing Then Return
        Try
            RequireAccess()
            Dim row As DataRow = DirectCast(EventsGrid.CurrentRow.DataBoundItem, DataRowView).Row
            Dim kind As String = CStr(row("RecordKind"))
            Dim sequence As Long = CLng(row("SequenceId"))
            Dim eventId As Guid = If(IsDBNull(row("EventId")), Guid.Empty, CType(row("EventId"), Guid))
            SetBusy(True)
            StatusLabel.Text = "جارٍ تحميل تفاصيل الحدث..."
            Dim details As DataSet = Await Task.Run(Function() reader.ReadDetails(kind, sequence, eventId))
            If IsDisposed Then Return
            RequireAccess()
            Using dialog As New Network_Audit_Details()
                dialog.SetDetails(kind, details, capabilities)
                dialog.ShowDialog(Me)
            End Using
            StatusLabel.Text = "جاهز"
        Catch ex As Exception
            If Not IsDisposed Then StatusLabel.Text = "تعذر تحميل التفاصيل: " & ex.Message
        Finally
            If Not IsDisposed Then SetBusy(False)
        End Try
    End Function

    Private Sub Browser_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If busy AndAlso e.CloseReason = CloseReason.UserClosing Then e.Cancel = True
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        Close()
    End Sub

    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click
        If busy Then Return
        Try
            RequireAccess()
            Dim table = CurrentResultTable()
            If table Is Nothing OrElse table.Rows.Count = 0 Then
                MessageBox.Show(Me, "لا توجد نتائج معروضة للطباعة.", "معاينة الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            printRows = table.Copy()
            printFilterDescription = String.Format(CultureInfo.CurrentCulture,
                "النتائج المعروضة: الصفحة {0} من {1} — الفترة {2:yyyy/MM/dd} إلى {3:yyyy/MM/dd} — الشاشة: {4} — المستخدم: {5}",
                pageIndex + 1, Math.Max(1, Math.Ceiling(resultCount / CDbl(AuditCompatibilityReader.PageSize))),
                appliedFilter.FromDate, appliedFilter.ToDate, If(String.IsNullOrWhiteSpace(appliedFilter.ScreenText), "كل الشاشات", appliedFilter.ScreenText),
                UserFilterDescription(appliedFilter.UserId))
            ResultsPrintDocument.DefaultPageSettings.Landscape = True
            ResultsPrintDocument.DefaultPageSettings.Margins = New Margins(35, 35, 40, 40)
            StatusLabel.Text = "جارٍ إعداد معاينة طباعة الصفحة المعروضة..."
            ResultsPrintPreviewDialog.Document = ResultsPrintDocument
            ResultsPrintPreviewDialog.ShowDialog(Me)
            StatusLabel.Text = "جاهز"
        Catch ex As Exception
            StatusLabel.Text = "تعذرت معاينة الطباعة: " & ex.Message
            MessageBox.Show(Me, ex.Message, "تعذرت معاينة الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            printRows = Nothing
        End Try
    End Sub

    Private Function UserFilterDescription(userId As Integer) As String
        If userId = 0 Then Return "كل المستخدمين"
        Dim view = TryCast(UserCombo.DataSource, DataView)
        If view IsNot Nothing Then
            For Each row As DataRowView In view
                If Convert.ToInt32(row("UserId"), CultureInfo.InvariantCulture) = userId Then Return Convert.ToString(row("DisplayName"), CultureInfo.CurrentCulture)
            Next
        End If
        Dim table = TryCast(UserCombo.DataSource, DataTable)
        If table IsNot Nothing Then
            For Each row As DataRow In table.Rows
                If Convert.ToInt32(row("UserId"), CultureInfo.InvariantCulture) = userId Then Return Convert.ToString(row("DisplayName"), CultureInfo.CurrentCulture)
            Next
        End If
        Return "مستخدم رقم " & userId.ToString(CultureInfo.InvariantCulture)
    End Function

    Private Function CurrentResultTable() As DataTable
        Dim table = TryCast(EventsGrid.DataSource, DataTable)
        If table IsNot Nothing Then Return table
        Dim view = TryCast(EventsGrid.DataSource, DataView)
        Return If(view Is Nothing, Nothing, view.ToTable())
    End Function

    Private Sub ResultsPrintDocument_BeginPrint(sender As Object, e As PrintEventArgs) Handles ResultsPrintDocument.BeginPrint
        printRowIndex = 0
        printPageNumber = 0
    End Sub

    Private Sub ResultsPrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs) Handles ResultsPrintDocument.PrintPage
        If printRows Is Nothing Then
            e.HasMorePages = False
            Return
        End If
        printPageNumber += 1
        Dim bounds = e.MarginBounds
        Using titleFont As New Font("Segoe UI Semibold", 14.0!, FontStyle.Bold),
              infoFont As New Font("Segoe UI", 8.5!), headerFont As New Font("Segoe UI Semibold", 8.0!, FontStyle.Bold),
              rowFont As New Font("Segoe UI", 7.5!)
            Using centered As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center},
                  rtl As New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center,
                      .FormatFlags = StringFormatFlags.DirectionRightToLeft, .Trimming = StringTrimming.EllipsisCharacter}
                Dim y As Single = bounds.Top
                e.Graphics.DrawString("تقرير أحداث شبكة المراقبة", titleFont, Brushes.Black, New RectangleF(bounds.Left, y, bounds.Width, 30), centered)
                y += 32
                e.Graphics.DrawString(printFilterDescription, infoFont, Brushes.Black, New RectangleF(bounds.Left, y, bounds.Width, 30), rtl)
                y += 34

                Dim headers = {"وقت الحدث", "الشاشة", "العملية", "النتيجة", "المستخدم", "المستند", "وصف الحدث"}
                Dim fields = {"EventTime", "ScreenLabel", "ActionLabel", "OutcomeLabel", "ActorName", "DocumentNumber", "Notes"}
                Dim ratios = {0.16F, 0.12F, 0.15F, 0.1F, 0.12F, 0.09F, 0.26F}
                Dim widths(headers.Length - 1) As Single
                Dim used As Single = 0
                For i = 0 To widths.Length - 2
                    widths(i) = CSng(Math.Floor(bounds.Width * ratios(i)))
                    used += widths(i)
                Next
                widths(widths.Length - 1) = bounds.Width - used
                DrawPrintRow(e.Graphics, bounds.Right, y, 28, headers, widths, headerFont, rtl, True)
                y += 28
                Const rowHeight As Single = 42
                While printRowIndex < printRows.Rows.Count AndAlso y + rowHeight + 24 <= bounds.Bottom
                    Dim row = printRows.Rows(printRowIndex)
                    Dim values(fields.Length - 1) As String
                    For i = 0 To fields.Length - 1
                        values(i) = PrintValue(row, fields(i))
                    Next
                    DrawPrintRow(e.Graphics, bounds.Right, y, rowHeight, values, widths, rowFont, rtl, False)
                    y += rowHeight
                    printRowIndex += 1
                End While
                e.Graphics.DrawString("صفحة الطباعة " & printPageNumber.ToString(CultureInfo.CurrentCulture), infoFont, Brushes.Black,
                                      New RectangleF(bounds.Left, bounds.Bottom - 20, bounds.Width, 20), centered)
                e.HasMorePages = printRowIndex < printRows.Rows.Count
            End Using
        End Using
    End Sub

    Private Shared Sub DrawPrintRow(graphics As Graphics, right As Single, top As Single, height As Single,
                                    values As String(), widths As Single(), font As Font, format As StringFormat, header As Boolean)
        Dim x = right
        For i = 0 To values.Length - 1
            x -= widths(i)
            Dim cell As New RectangleF(x, top, widths(i), height)
            If header Then graphics.FillRectangle(Brushes.Gainsboro, cell)
            graphics.DrawRectangle(Pens.Gray, cell.X, cell.Y, cell.Width, cell.Height)
            cell.Inflate(-3, -2)
            graphics.DrawString(If(values(i), ""), font, Brushes.Black, cell, format)
        Next
    End Sub

    Private Shared Function PrintValue(row As DataRow, field As String) As String
        If Not row.Table.Columns.Contains(field) OrElse IsDBNull(row(field)) Then Return ""
        If field = "EventTime" Then Return CDate(row(field)).ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture)
        Return Convert.ToString(row(field), CultureInfo.CurrentCulture)
    End Function
End Class
