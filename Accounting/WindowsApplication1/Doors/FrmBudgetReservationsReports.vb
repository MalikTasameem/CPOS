Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing.Printing

Public Class FrmBudgetReservationsReports

    Private ReadOnly ConnStr As String = MY_Settings.SqlConStr

    '==============================
    ' Printing variables
    '==============================
    Private WithEvents PD As New Printing.PrintDocument
    Private PrintRowIndex As Integer = 0
    Private PrintPageNo As Integer = 1
    Private IsLoading As Boolean = False


    '==============================
    ' نوع التقرير (بدون Enum لتفادي أخطاء Designer/Copy)
    '==============================
    Private Const RT_ACTIVE As Integer = 1
    Private Const RT_PARTIAL As Integer = 2
    Private Const RT_COMPLETED As Integer = 3
    Private Const RT_ALL As Integer = 4


    '==============================
    ' Form Load
    '==============================
    Private Sub FrmBudgetReservationsReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            IsLoading = True
            ApplyGridStyle(dgvReserves)
            ApplyGridStyle(dgvTimeline)
            BudgetUiHelper.ApplyBudgetFormStyle(Me)
            ApplyReportsLayout()

            LoadFiscalYears()
            InitializeDateFilters()
            LoadDoors()

            rbActive.Checked = True
            IsLoading = False
            LoadReservationsReport()

            lblStatus.Text = "جاهز"
        Catch ex As Exception
            IsLoading = False
            lblStatus.Text = "خطأ: " & ex.Message
        End Try
    End Sub

    '==============================
    ' Helpers – UI
    '==============================
    Private Sub ApplyGridStyle(grid As DataGridView)
        grid.EnableHeadersVisualStyles = False
        grid.ColumnHeadersHeight = 36
        grid.RowTemplate.Height = 32
        grid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        grid.DefaultCellStyle.Font = New Font("Segoe UI", 10)
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 237, 255)
        grid.RowHeadersVisible = False
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        grid.ReadOnly = True
        grid.AllowUserToAddRows = False
        grid.AllowUserToDeleteRows = False
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grid.MultiSelect = False

        dgvReserves.DefaultCellStyle.SelectionBackColor = Color.FromArgb(180, 200, 230)
        dgvReserves.DefaultCellStyle.SelectionForeColor = Color.Black

    End Sub

    Private Sub ApplyReportsLayout()
        PanelHeader.BackColor = Color.White
        GroupBoxReportType.BackColor = Color.White
        PanelFooter.BackColor = Color.White

        btnShowTimeline.Text = "◷ عرض حركة الحجز"
        btnPrint.Text = "⎙ طباعة التقرير"
        btnExport.Text = "⇩ تصدير PDF"
        btnClose.Text = "⟵ خروج"

        lblYear.Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)
        lblDoor.Font = lblYear.Font
        lblChapter.Font = lblYear.Font
        lblItem.Font = lblYear.Font
        lblFrom.Font = lblYear.Font
        lblTo.Font = lblYear.Font
    End Sub

    '==============================
    ' Load Fiscal Years
    '==============================
    Private Sub LoadFiscalYears()
        cmbFiscalYear.Items.Clear()
        cmbFiscalYear.Items.Add(Identifiers.F_YEAR)
        cmbFiscalYear.SelectedIndex = 0
    End Sub

    Private Function SelectedYear() As Integer
        If cmbFiscalYear.SelectedItem Is Nothing Then Return 0
        Return CInt(cmbFiscalYear.SelectedItem)
    End Function

    Private Sub InitializeDateFilters()
        Dim y As Integer = SelectedYear()
        If y <= 0 Then y = DateTime.Now.Year

        dtFrom.Value = New DateTime(y, 1, 1)
        dtTo.Value = New DateTime(y, 12, 31)
    End Sub

    '==============================
    ' Cascading Lists
    '==============================
    Private Sub LoadDoors()
        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT DoorId,
       DoorCode + N' - ' + DoorName AS DoorText
FROM Budget_Doors
WHERE IsActive = 1
ORDER BY DoorCode;", cn)

                Dim dt As New DataTable()
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using

                cmbDoors.DataSource = dt
                cmbDoors.DisplayMember = "DoorText"
                cmbDoors.ValueMember = "DoorId"
                cmbDoors.SelectedIndex = -1
            End Using
        End Using

        cmbChapters.DataSource = Nothing
        cmbItems.DataSource = Nothing
    End Sub

    Private Sub LoadChapters(doorId As Integer)
        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT ChapterId,
       ChapterCode + N' - ' + ChapterName AS ChapterText
FROM Budget_Chapters
WHERE DoorId = @DoorId AND IsActive = 1
ORDER BY ChapterCode;", cn)

                cmd.Parameters.AddWithValue("@DoorId", doorId)

                Dim dt As New DataTable()
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using

                cmbChapters.DataSource = dt
                cmbChapters.DisplayMember = "ChapterText"
                cmbChapters.ValueMember = "ChapterId"
                cmbChapters.SelectedIndex = -1
            End Using
        End Using

        cmbItems.DataSource = Nothing
    End Sub

    Private Sub LoadItems(chapterId As Integer)
        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT BudgetItemId,
       ItemCode + N' - ' + ItemName AS ItemText
FROM Budget_Items
WHERE ChapterId = @ChapterId AND IsActive = 1
ORDER BY ItemCode;", cn)

                cmd.Parameters.AddWithValue("@ChapterId", chapterId)

                Dim dt As New DataTable()
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using

                cmbItems.DataSource = dt
                cmbItems.DisplayMember = "ItemText"
                cmbItems.ValueMember = "BudgetItemId"
                cmbItems.SelectedIndex = -1
            End Using
        End Using
    End Sub

    '==============================
    ' تحديد نوع التقرير
    '==============================
    Private Function GetSelectedReportType() As Integer
        If rbActive.Checked Then Return RT_ACTIVE
        If rbPartial.Checked Then Return RT_PARTIAL
        If rbCompleted.Checked Then Return RT_COMPLETED
        Return RT_ALL
    End Function


    '==============================
    ' Load Main Report Grid
    '==============================
    Private Sub LoadReservationsReport()
        If IsLoading Then Exit Sub
        If SelectedYear() = 0 Then Exit Sub

        Dim whereClause As String = ""

        Select Case GetSelectedReportType()
            Case RT_ACTIVE
                whereClause = " AND ReleasedAmount = 0 "

            Case RT_PARTIAL
                whereClause = " AND ReleasedAmount > 0 AND RemainingAmount > 0 "

            Case RT_COMPLETED
                whereClause = " AND RemainingAmount = 0 "

            Case RT_ALL
                whereClause = ""
        End Select

        If dtFrom.Value.Date <= dtTo.Value.Date Then
            whereClause &= " AND ReserveDate >= @FromDate AND ReserveDate < DATEADD(day, 1, @ToDate) "
        End If

        If cmbDoors.SelectedIndex >= 0 Then
            whereClause &= " AND DoorCode = @DoorCode "
        End If

        If cmbChapters.SelectedIndex >= 0 Then
            whereClause &= " AND ChapterCode = @ChapterCode "
        End If

        If cmbItems.SelectedIndex >= 0 Then
            whereClause &= " AND ItemCode = @ItemCode "
        End If


        Dim sql As String =
$"
SELECT
    DoorCode,
    ChapterCode,
    ItemCode,
    ItemName,
    ReserveEntryId,
    ReserveDate,
    ReservedAmount,
    ReleasedAmount,
    RemainingAmount,
    Notes
FROM Vw_BudgetReservations_Report
WHERE FiscalYear = @Y
{whereClause}
ORDER BY DoorCode, ChapterCode, ReserveDate;
"

        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@Y", SelectedYear())
                If dtFrom.Value.Date <= dtTo.Value.Date Then
                    cmd.Parameters.AddWithValue("@FromDate", dtFrom.Value.Date)
                    cmd.Parameters.AddWithValue("@ToDate", dtTo.Value.Date)
                End If
                If cmbDoors.SelectedIndex >= 0 Then cmd.Parameters.AddWithValue("@DoorCode", ComboCode(cmbDoors))
                If cmbChapters.SelectedIndex >= 0 Then cmd.Parameters.AddWithValue("@ChapterCode", ComboCode(cmbChapters))
                If cmbItems.SelectedIndex >= 0 Then cmd.Parameters.AddWithValue("@ItemCode", ComboCode(cmbItems))

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        dgvReserves.DataSource = dt
        ApplyReservesGridStyle()
        dgvTimeline.DataSource = Nothing
        ColorizeReserveRows()

        lblStatus.Text = $"عدد الحجوزات: {dt.Rows.Count}"
    End Sub

    Private Function ComboCode(combo As ComboBox) As String
        If combo Is Nothing OrElse combo.SelectedIndex < 0 Then Return ""
        Dim text As String = Convert.ToString(combo.Text)
        Dim p As Integer = text.IndexOf(" - ")
        If p > 0 Then Return text.Substring(0, p).Trim()
        Return text.Trim()
    End Function

    '==============================
    ' Grid Columns Style
    '==============================
    Private Sub ApplyReservesGridStyle()
        If dgvReserves.Columns.Count = 0 Then Exit Sub

        dgvReserves.Columns("DoorCode").HeaderText = "الباب"
        dgvReserves.Columns("ChapterCode").HeaderText = "الفصل"
        dgvReserves.Columns("ItemCode").HeaderText = "كود البند"
        dgvReserves.Columns("ItemName").HeaderText = "اسم البند"
        dgvReserves.Columns("ReserveEntryId").HeaderText = "رقم الحجز"
        dgvReserves.Columns("ReserveDate").HeaderText = "تاريخ الحجز"
        dgvReserves.Columns("ReservedAmount").HeaderText = "المبلغ المحجوز"
        dgvReserves.Columns("ReleasedAmount").HeaderText = "المفكوك"
        dgvReserves.Columns("RemainingAmount").HeaderText = "المتبقي"
        dgvReserves.Columns("Notes").HeaderText = "البيان"

        dgvReserves.Columns("ReservedAmount").DefaultCellStyle.Format = "N3"
        dgvReserves.Columns("ReleasedAmount").DefaultCellStyle.Format = "N3"
        dgvReserves.Columns("RemainingAmount").DefaultCellStyle.Format = "N3"
        dgvReserves.Columns("ReserveDate").DefaultCellStyle.Format = "yyyy-MM-dd"

        dgvReserves.Columns("ItemName").FillWeight = 180
        dgvReserves.Columns("Notes").FillWeight = 220
        dgvReserves.Columns("DoorCode").FillWeight = 70
        dgvReserves.Columns("ChapterCode").FillWeight = 70
        dgvReserves.Columns("ItemCode").FillWeight = 80
        dgvReserves.Columns("ReservedAmount").FillWeight = 90
        dgvReserves.Columns("ReleasedAmount").FillWeight = 90
        dgvReserves.Columns("RemainingAmount").FillWeight = 90

        dgvReserves.ClearSelection()
    End Sub

    '==============================
    ' Events – Filters
    '==============================
    Private Sub cmbDoors_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbDoors.SelectionChangeCommitted
        If cmbDoors.SelectedIndex >= 0 Then
            LoadChapters(CInt(cmbDoors.SelectedValue))
            LoadReservationsReport()
        End If
    End Sub

    Private Sub cmbChapters_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbChapters.SelectionChangeCommitted
        If cmbChapters.SelectedIndex >= 0 Then
            LoadItems(CInt(cmbChapters.SelectedValue))
            LoadReservationsReport()
        End If
    End Sub

    Private Sub cmbItems_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbItems.SelectionChangeCommitted
        LoadReservationsReport()
    End Sub

    Private Sub cmbFiscalYear_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbFiscalYear.SelectionChangeCommitted
        InitializeDateFilters()
        LoadReservationsReport()
    End Sub

    Private Sub dtFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtFrom.ValueChanged
        LoadReservationsReport()
    End Sub

    Private Sub dtTo_ValueChanged(sender As Object, e As EventArgs) Handles dtTo.ValueChanged
        LoadReservationsReport()
    End Sub

    Private Sub ReportType_CheckedChanged(sender As Object, e As EventArgs) _
        Handles rbActive.CheckedChanged,
                rbPartial.CheckedChanged,
                rbCompleted.CheckedChanged,
                rbAll.CheckedChanged

        If DirectCast(sender, RadioButton).Checked Then
            LoadReservationsReport()
        End If
    End Sub

    '==============================
    ' Footer Buttons
    '==============================
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgvReserves_SelectionChanged(sender As Object, e As EventArgs) _
    Handles dgvReserves.SelectionChanged

        If dgvReserves.CurrentRow Is Nothing Then Exit Sub
        If dgvReserves.CurrentRow.Index < 0 Then Exit Sub

        Dim reserveEntryId As Integer =
        CInt(dgvReserves.CurrentRow.Cells("ReserveEntryId").Value)

        LoadTimelineForReserve(reserveEntryId)

    End Sub


    Private Sub LoadTimelineForReserve(reserveEntryId As Integer)

        Dim dt As New DataTable()

        Dim sql As String =
"
SELECT
    EntryDate,
    EntryTypeName,
    SignedAmount,
    ReservedBalanceAfter,
    ISNULL(ReserveNotes, Notes) AS DisplayNotes
FROM Vw_BudgetReserveTimeline
WHERE BudgetEntryId = @ReserveId
   OR ReserveEntryId = @ReserveId
ORDER BY EntryDate;
"

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@ReserveId", reserveEntryId)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        dgvTimeline.DataSource = dt
        ApplyTimelineGridStyle()

    End Sub

    Private Sub ApplyTimelineGridStyle()
        If dgvTimeline.Columns.Count = 0 Then Exit Sub

        dgvTimeline.Columns("EntryDate").HeaderText = "التاريخ"
        dgvTimeline.Columns("EntryTypeName").HeaderText = "النوع"
        dgvTimeline.Columns("SignedAmount").HeaderText = "القيمة"
        dgvTimeline.Columns("ReservedBalanceAfter").HeaderText = "الرصيد بعد الحركة"
        dgvTimeline.Columns("DisplayNotes").HeaderText = "البيان"

        dgvTimeline.Columns("SignedAmount").DefaultCellStyle.Format = "N3"
        dgvTimeline.Columns("ReservedBalanceAfter").DefaultCellStyle.Format = "N3"
        dgvTimeline.Columns("EntryDate").DefaultCellStyle.Format = "yyyy-MM-dd HH:mm"
        dgvTimeline.Columns("DisplayNotes").FillWeight = 240
        dgvTimeline.Columns("ReservedBalanceAfter").FillWeight = 120

        dgvTimeline.ClearSelection()
    End Sub


    Private Sub ColorizeReserveRows()

        For Each row As DataGridViewRow In dgvReserves.Rows

            If row.IsNewRow Then Continue For

            Dim released As Decimal = 0D
            Dim remaining As Decimal = 0D

            Decimal.TryParse(row.Cells("ReleasedAmount").Value.ToString(), released)
            Decimal.TryParse(row.Cells("RemainingAmount").Value.ToString(), remaining)

            ' 🟦 نشط
            If released = 0D Then
                row.DefaultCellStyle.BackColor = Color.FromArgb(221, 235, 247) ' أزرق فاتح

                ' 🟨 جزئي
            ElseIf released > 0D AndAlso remaining > 0D Then
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 204) ' أصفر فاتح

                ' 🟩 مكتمل
            ElseIf remaining = 0D Then
                row.DefaultCellStyle.BackColor = Color.FromArgb(226, 239, 218) ' أخضر فاتح
            End If

        Next

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        If dgvReserves.Rows.Count = 0 Then
            MessageBox.Show("لا توجد بيانات للطباعة", "تنبيه")
            Exit Sub
        End If

        Dim doc = CreateReservationsStatusReport()

        Dim preview As New PrintPreviewDialog()
        preview.Document = doc
        preview.WindowState = FormWindowState.Maximized
        preview.ShowDialog()

        'If dgvReserves.Rows.Count = 0 Then
        '    MessageBox.Show("لا توجد بيانات للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Sub
        'End If

        'PrintRowIndex = 0
        'PrintPageNo = 1

        'Dim dlg As New PrintPreviewDialog()
        'dlg.Document = PD
        'dlg.WindowState = FormWindowState.Maximized
        'dlg.ShowDialog()
    End Sub

    Public Function CreateReservationsStatusReport() As Printing.PrintDocument

        Dim doc As New Printing.PrintDocument()

        ' A4 Landscape
        doc.DefaultPageSettings.Landscape = False
        doc.DefaultPageSettings.Margins = New Margins(30, 30, 25, 25)

        AddHandler doc.PrintPage,
        Sub(sender, e)
            PrintReservationsStatus(e)
        End Sub

        Return doc
    End Function

    Private Function GetPeriodText() As String
        Return $"الفترة: من {dtFrom.Value:yyyy-MM-dd} إلى {dtTo.Value:yyyy-MM-dd}"
    End Function

    Private Sub PrintReservationsStatus(e As Printing.PrintPageEventArgs)

        Dim g As Graphics = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

        Dim fontTitle As New Font("Segoe UI", 14, FontStyle.Bold)
        Dim fontHeader As New Font("Segoe UI", 10, FontStyle.Bold)
        Dim fontBody As New Font("Segoe UI", 10)
        Dim fontSmall As New Font("Segoe UI", 9)

        Dim leftX As Integer = e.MarginBounds.Left
        Dim topY As Integer = e.MarginBounds.Top
        Dim pageW As Integer = e.MarginBounds.Width

        Dim yPos As Integer = topY

        ' =======================
        ' Header (نفس الأسلوب)
        ' =======================
        Dim title As String = "تقرير موقف الحجوزات"

        yPos = DrawReportHeader(g, e, title, SelectedYear())

        g.DrawString(
        $"نوع التقرير: {GetSelectedReportTypeName()}    {GetPeriodText()}",
        fontSmall,
        Brushes.DimGray,
        leftX,
        yPos
    )
        yPos += 25

        g.DrawString(
        "تاريخ الطباعة: " & DateTime.Now.ToString("yyyy-MM-dd HH:mm") &
        "    المستخدم: " & USER_ID.ToString(),
        fontSmall,
        Brushes.DimGray,
        leftX,
        yPos
    )
        yPos += 25

        ' =======================
        ' Table settings
        ' =======================
        Dim rowH As Integer = 28
        Dim headerH As Integer = 30

        Dim colDoor As Integer = 80
        Dim colChapter As Integer = 80
        Dim colItemCode As Integer = 100
        Dim colItemName As Integer = 280
        Dim colMoney As Integer = 100

        Dim tableW As Integer =
        colDoor + colChapter + colItemCode + colItemName + (colMoney * 3)

        If tableW > pageW Then
            colItemName = Math.Max(200, pageW - (colDoor + colChapter + colItemCode + colMoney * 3))
            tableW = colDoor + colChapter + colItemCode + colItemName + (colMoney * 3)
        End If

        'Dim x0 As Integer = leftX
        Dim x0 As Integer = e.MarginBounds.Right - tableW


        ' =======================
        ' Header row
        ' =======================
        Dim headerRect As New Rectangle(x0, yPos, tableW, headerH)
        g.FillRectangle(New SolidBrush(Color.FromArgb(240, 242, 245)), headerRect)
        g.DrawRectangle(Pens.DarkGray, headerRect)

        Dim headers() As String = {
        "الباب", "الفصل", "كود البند", "اسم البند",
        "المحجوز", "المفكوك", "المتبقي"
    }

        Dim widths() As Integer = {
        colDoor, colChapter, colItemCode, colItemName,
        colMoney, colMoney, colMoney
    }

        Dim x As Integer = x0
        'For i = 0 To headers.Length - 1
        For i As Integer = headers.Length - 1 To 0 Step -1
            Dim r As New Rectangle(x, yPos, widths(i), headerH)
            DrawCellText(g, headers(i), fontHeader, r, HorizontalAlignment.Center)
            g.DrawRectangle(Pens.DarkGray, r)
            x += widths(i)
        Next

        yPos += headerH

        ' =======================
        ' Rows from dgvReserves
        ' =======================
        Dim totRes As Decimal = 0D
        Dim totRel As Decimal = 0D
        Dim totRem As Decimal = 0D

        For Each row As DataGridViewRow In dgvReserves.Rows

            If row.IsNewRow Then Continue For
            If yPos + rowH > e.MarginBounds.Bottom - 40 Then
                e.HasMorePages = True
                Return
            End If

            Dim res As Decimal = CDec(row.Cells("ReservedAmount").Value)
            Dim rel As Decimal = CDec(row.Cells("ReleasedAmount").Value)
            Dim rem1 As Decimal = CDec(row.Cells("RemainingAmount").Value)

            totRes += res : totRel += rel : totRem += rem1

            Dim cells() As String = {
            row.Cells("DoorCode").Value.ToString(),
            row.Cells("ChapterCode").Value.ToString(),
            row.Cells("ItemCode").Value.ToString(),
            row.Cells("ItemName").Value.ToString(),
            res.ToString("N3"),
            rel.ToString("N3"),
            rem1.ToString("N3")
        }

            x = x0
            'For i = 0 To cells.Length - 1
            For i As Integer = cells.Length - 1 To 0 Step -1
                Dim r As New Rectangle(x, yPos, widths(i), rowH)
                DrawCellText(
                g,
                cells(i),
                fontBody,
                r,
                If(i >= 4, HorizontalAlignment.Right, HorizontalAlignment.Center)
            )
                g.DrawRectangle(Pens.Gainsboro, r)
                x += widths(i)
            Next

            yPos += rowH
        Next

        ' =======================
        ' Totals row
        ' =======================
        yPos += 5
        Dim totRect As New Rectangle(x0, yPos, tableW, rowH)
        g.FillRectangle(New SolidBrush(Color.FromArgb(250, 250, 250)), totRect)
        g.DrawRectangle(Pens.DarkGray, totRect)

        Dim totCells() As String = {
        "", "", "", "الإجمالي",
        totRes.ToString("N3"),
        totRel.ToString("N3"),
        totRem.ToString("N3")
    }

        x = x0
        'For i = 0 To totCells.Length - 1
        For i As Integer = totCells.Length - 1 To 0 Step -1
            Dim r As New Rectangle(x, yPos, widths(i), rowH)
            DrawCellText(
            g,
            totCells(i),
            fontHeader,
            r,
            If(i >= 4, HorizontalAlignment.Right, HorizontalAlignment.Center)
        )
            g.DrawRectangle(Pens.DarkGray, r)
            x += widths(i)
        Next

        ' =======================
        ' Footer
        ' =======================
        Dim footerY As Integer = e.MarginBounds.Bottom + 5
        g.DrawString(
        "توقيع المختص: ____________________      اعتماد: ____________________",
        fontSmall,
        Brushes.Black,
        leftX,
        footerY
    )

        e.HasMorePages = False
    End Sub

    Private Function GetSelectedReportTypeName() As String
        If rbActive.Checked Then Return "الحجوزات النشطة"
        If rbPartial.Checked Then Return "الحجوزات المفكوكة جزئيًا"
        If rbCompleted.Checked Then Return "الحجوزات المفكوكة بالكامل"
        Return "جميع الحجوزات"
    End Function

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click

        Dim fiscalYear As Integer = cmbFiscalYear.SelectedItem
        Dim doc As PrintDocument = CreateReservationsStatusReport()

        'ExportToPdf(doc, $"تقرير_الحجوزات_{fiscalYear}")

        Dim STR_NAME As String = $"تقرير_الحجوزات_{fiscalYear}"

        Try
            Dim sfd As New SaveFileDialog()
            sfd.Filter = "PDF Files (*.pdf)|*.pdf"
            sfd.FileName = STR_NAME & ".pdf"

            If sfd.ShowDialog() <> DialogResult.OK Then Exit Sub

            ExportToPdf(doc, sfd.FileName)

            lblStatus.Text = "تم تصدير الملف بنجاح"
            'MessageBox.Show("تم إنشاء ملف PDF بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("خطأ أثناء التصدير: " & ex.Message)
        End Try


    End Sub


    'Private Sub ExportToPdf(doc As PrintDocument, defaultFileName As String)

    '    Using dlg As New PrintDialog()
    '        dlg.AllowSomePages = False
    '        dlg.UseEXDialog = True
    '        dlg.Document = doc

    '        ' اختيار طابعة PDF
    '        For Each printer As String In PrinterSettings.InstalledPrinters
    '            If printer.ToLower().Contains("pdf") Then
    '                doc.PrinterSettings.PrinterName = printer
    '                Exit For
    '            End If
    '        Next

    '        ' اقتراح اسم الملف
    '        doc.PrinterSettings.PrintToFile = True
    '        doc.PrinterSettings.PrintFileName =
    '        IO.Path.Combine(
    '            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
    '            defaultFileName & ".pdf"
    '        )

    '        Try
    '            doc.Print()
    '            MessageBox.Show("تم إنشاء ملف PDF بنجاح", "PDF", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Catch ex As Exception
    '            MessageBox.Show("فشل إنشاء PDF: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try
    '    End Using

    'End Sub


End Class
