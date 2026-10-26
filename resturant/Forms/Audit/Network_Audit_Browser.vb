Imports System.Threading.Tasks

Public Class Network_Audit_Browser
    Private reader As AuditCompatibilityReader
    Private capabilities As AuditReadCapabilities
    Private searchBounds As AuditReadBounds
    Private appliedFilter As AuditSearchFilter
    Private pageIndex As Integer
    Private resultCount As Long
    Private busy As Boolean

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
        UserText.Clear()
        ScreenText.Clear()
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
        If UserText.Text.Trim().Length > 0 AndAlso (Not Integer.TryParse(UserText.Text.Trim(), selectedUser) OrElse selectedUser <= 0) Then
            Throw New ArgumentException("أدخل رقم مستخدم موجبًا أو اترك الحقل فارغًا لجميع المستخدمين.")
        End If
        Return New AuditSearchFilter With {
            .FromDate = FromPicker.Value.Date, .ToDate = ToPicker.Value.Date,
            .RecordKind = "V2",
            .Outcome = OutcomeCombo.SelectedIndex, .UserId = selectedUser,
            .DocumentNumber = DocumentText.Text.Trim(), .ScreenText = ScreenText.Text.Trim(), .SearchText = NotesText.Text.Trim()}
    End Function

    Private Sub SetBusy(value As Boolean)
        busy = value
        FiltersLayout.Enabled = Not value
        CloseButton.Enabled = Not value
        EventsGrid.Enabled = Not value
        PreviousButton.Enabled = Not value AndAlso pageIndex > 0
        NextButton.Enabled = Not value AndAlso (CLng(pageIndex) + 1) * AuditCompatibilityReader.PageSize < resultCount
        DetailsButton.Enabled = Not value AndAlso EventsGrid.CurrentRow IsNot Nothing
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
            SetBusy(True)
            StatusLabel.Text = "جارٍ قراءة السجل..."
            Dim result As AuditReadPage = Await Task.Run(Function()
                                                            If newSearch Then
                                                                nextCaps = nextReader.GetCapabilities()
                                                                nextBounds = nextReader.CaptureBounds(nextCaps)
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
            resultCount = result.Total
            pageIndex = requestedPage
            EventsGrid.DataSource = result.Rows
            PageLabel.Text = String.Format("الصفحة {0} / {1}  |  {2:N0} حدث", pageIndex + 1, Math.Max(1, Math.Ceiling(resultCount / CDbl(AuditCompatibilityReader.PageSize))), resultCount)
            BasisLabel.Text = "السجل الجديد بتوقيت " & capabilities.TimeZone.Id & ". يشمل المسارات المنقولة فقط؛ استكمال ربط بقية الشاشات لم ينتهِ بعد."
            StatusLabel.Text = "تمت القراءة — لا تُنشأ أحداث ولا تُعدّل بيانات من هذه الشاشة."
        Catch ex As Exception
            If Not IsDisposed Then StatusLabel.Text = "تعذر التحديث؛ النتائج السابقة باقية: " & ex.Message
        Finally
            If Not IsDisposed Then SetBusy(False)
        End Try
    End Function

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
End Class
