Imports System.Data.SqlClient

Imports System.Drawing.Printing

Public Class Frm_ItemLedger

    Private ConnectionString As String = MY_Settings.SqlConStr

    Private _IM_ID As Integer = 0
    Private _ItemName As String = ""
    Private _ST_ID As Long = 0
    Private _PrintRowIndex As Integer = 0
    Private _PrintDateTime As DateTime

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(
        ByVal imId As Integer,
        ByVal itemName As String,
        Optional ByVal stId As Long = 0
    )

        InitializeComponent()

        _IM_ID = imId
        _ItemName = itemName
        _ST_ID = stId

    End Sub

    Private Sub Frm_ItemLedger_Load(
        sender As Object,
        e As EventArgs
    ) Handles MyBase.Load

        Dtp_From.Value = New DateTime(Date.Today.Year, 1, 1)
        Dtp_To.Value = Date.Today
        'Txt_ShopName.Text = SBill_Title_1
        Txt_ItemName.Text = _ItemName

        LoadStores()

        'If _IM_ID > 0 Then
        '    Txt_IM_ID.Text = _IM_ID.ToString()
        '    Txt_ItemName.Text = _ItemName
        'End If

        If _ST_ID > 0 Then
            Cmb_Store.SelectedValue = _ST_ID
        Else
            Cmb_Store.SelectedValue = 0
        End If

        If _IM_ID > 0 Then
            LoadLedger()
        End If

    End Sub

    Private Sub LoadStores()

        Try

            Dim dt As New DataTable()
            dt.Columns.Add("ST_ID", GetType(Long))
            dt.Columns.Add("ST_Name", GetType(String))

            Dim allRow As DataRow = dt.NewRow()
            allRow("ST_ID") = 0
            allRow("ST_Name") = "كل المخازن"
            dt.Rows.Add(allRow)

            Using cn As New SqlConnection(ConnectionString)

                Dim sql As String = "
SELECT 
    CAST(ST_ID AS bigint) AS ST_ID,
    CAST(ST_Name AS nvarchar(2500)) AS ST_Name
FROM dbo.STORES
ORDER BY ST_Name
"

                Using cmd As New SqlCommand(sql, cn)

                    Dim da As New SqlDataAdapter(cmd)
                    Dim dbStores As New DataTable()

                    da.Fill(dbStores)

                    For Each r As DataRow In dbStores.Rows

                        Dim nr As DataRow = dt.NewRow()
                        nr("ST_ID") = CLng(r("ST_ID"))
                        nr("ST_Name") = r("ST_Name").ToString()
                        dt.Rows.Add(nr)

                    Next

                End Using

            End Using

            Cmb_Store.DataSource = dt
            Cmb_Store.DisplayMember = "ST_Name"
            Cmb_Store.ValueMember = "ST_ID"

        Catch ex As Exception

            MessageBox.Show(
                "خطأ في تحميل المخازن: " & ex.Message,
                "خطأ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    Private Sub Btn_Search_Click(
        sender As Object,
        e As EventArgs
    ) Handles Btn_Search.Click

        LoadLedger()

    End Sub

    Private Sub LoadLedger()

        Try

            'If Txt_IM_ID.Text.Trim() = "" Then

            '    MessageBox.Show(
            '        "أدخل رقم الصنف.",
            '        "تنبيه",
            '        MessageBoxButtons.OK,
            '        MessageBoxIcon.Warning
            '    )

            '    Return

            'End If

            'Dim imId As Integer = Convert.ToInt32(Txt_IM_ID.Text)

            Dim stId As Object = DBNull.Value

            If Cmb_Store.SelectedValue IsNot Nothing Then

                Dim selectedStore As Long = CLng(Cmb_Store.SelectedValue)

                If selectedStore > 0 Then
                    stId = selectedStore
                End If

            End If

            Using cn As New SqlConnection(ConnectionString)

                Using cmd As New SqlCommand(
                    "dbo.Inventory_ItemLedger_WithOpening",
                    cn
                )

                    cmd.CommandType = CommandType.StoredProcedure

                    cmd.Parameters.AddWithValue("@IM_ID", _IM_ID)

                    If stId Is DBNull.Value Then
                        cmd.Parameters.AddWithValue("@ST_ID", DBNull.Value)
                    Else
                        cmd.Parameters.AddWithValue("@ST_ID", stId)
                    End If

                    cmd.Parameters.AddWithValue("@FromDate", Dtp_From.Value.Date)
                    cmd.Parameters.AddWithValue("@ToDate", Dtp_To.Value.Date)

                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()

                    da.Fill(dt)

                    GridLedger.DataSource = dt

                End Using

            End Using

            FormatGrid()
            CalculateTotals()

        Catch ex As Exception

            MessageBox.Show(
                ex.Message,
                "خطأ",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )

        End Try

    End Sub

    Private Sub FormatGrid()

        If GridLedger.Columns.Count = 0 Then Return

        For Each col As DataGridViewColumn In GridLedger.Columns
            col.Visible = False
        Next

        ShowColumn("Seq", "م", 45)
        ShowColumn("Date", "التاريخ", 115)
        ShowColumn("MovementType", "الحركة", 110)
        ShowColumn("SourceParentId", "رقم الفاتورة", 95)
        ShowColumn("SourceId", "رقم السطر", 80)
        ShowColumn("ST_ID", "المخزن", 70)
        ShowColumn("U_ID", "الوحدة", 70)
        ShowColumn("U_Cargo", "تعادل", 75)
        ShowColumn("UnitQty", "كمية", 85)
        ShowColumn("QtyIn", "دخول", 85)
        ShowColumn("QtyOut", "خروج", 85)
        'ShowColumn("NetQty", "صافي", 85)
        ShowColumn("BalanceQty", "الرصيد", 95)
        ShowColumn("UnitCost", "التكلفة", 95)
        ShowColumn("TotalCost", "الإجمالي", 105)

        GridLedger.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        GridLedger.AllowUserToResizeColumns = True
        GridLedger.RowHeadersVisible = False
        GridLedger.BackgroundColor = Color.White
        GridLedger.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue
        GridLedger.RightToLeft = RightToLeft.Yes

        GridLedger.ColumnHeadersHeight = 30
        GridLedger.RowTemplate.Height = 25

        For Each col As DataGridViewColumn In GridLedger.Columns

            If col.Visible Then
                col.HeaderCell.Style.Alignment =
                DataGridViewContentAlignment.MiddleCenter

                col.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter
            End If

        Next

        FormatNumericColumn("U_Cargo")
        FormatNumericColumn("UnitQty")
        FormatNumericColumn("QtyIn")
        FormatNumericColumn("QtyOut")
        ' FormatNumericColumn("NetQty")
        FormatNumericColumn("BalanceQty")
        FormatNumericColumn("UnitCost")
        FormatNumericColumn("TotalCost")

    End Sub


    Private Sub GridLedger_CellFormatting(
    sender As Object,
    e As DataGridViewCellFormattingEventArgs
) Handles GridLedger.CellFormatting

        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        If GridLedger.Columns.Count = 0 Then Return

        Dim columnName As String =
        GridLedger.Columns(e.ColumnIndex).Name

        'إخفاء الصفر من أعمدة الحركة
        If columnName = "QtyIn" OrElse
       columnName = "QtyOut" Then 'OrElse
            '  columnName = "NetQty" Then

            If e.Value IsNot Nothing AndAlso e.Value IsNot DBNull.Value Then

                Dim val As Decimal = 0

                If Decimal.TryParse(e.Value.ToString(), val) Then

                    If val = 0D Then
                        e.Value = ""
                        e.FormattingApplied = True
                    ElseIf columnName = "QtyIn" Then
                        e.CellStyle.BackColor = Color.FromArgb(209, 250, 229)
                        e.CellStyle.ForeColor = Color.FromArgb(6, 95, 70)
                        e.CellStyle.SelectionBackColor = Color.FromArgb(16, 185, 129)
                        e.CellStyle.SelectionForeColor = Color.White
                    ElseIf columnName = "QtyOut" Then
                        e.CellStyle.BackColor = Color.FromArgb(254, 226, 226)
                        e.CellStyle.ForeColor = Color.FromArgb(153, 27, 27)
                        e.CellStyle.SelectionBackColor = Color.FromArgb(239, 68, 68)
                        e.CellStyle.SelectionForeColor = Color.White
                    End If

                End If

            End If

        End If

        'تعريب نوع الحركة
        If columnName = "MovementType" Then

            If e.Value IsNot Nothing Then
                e.Value = TranslateMovementType(e.Value.ToString())
                e.FormattingApplied = True
            End If

        End If

        'تحسين عرض رقم الفاتورة
        If columnName = "SourceParentId" Then

            Dim movementType As String = ""

            If GridLedger.Columns.Contains("MovementType") Then
                Dim movementValue As Object =
                    GridLedger.Rows(e.RowIndex).Cells("MovementType").Value

                If movementValue IsNot Nothing AndAlso movementValue IsNot DBNull.Value Then
                    movementType = movementValue.ToString()
                End If
            End If

            If e.Value IsNot Nothing AndAlso e.Value IsNot DBNull.Value Then

                Dim docNo As String = e.Value.ToString()

                If docNo <> "" Then
                    e.Value = GetDocumentPrefix(movementType) & " / " & docNo
                    e.FormattingApplied = True
                End If

            End If

        End If

    End Sub


    Private Function TranslateMovementType(movementType As String) As String

        Select Case movementType

            Case "PURCHASE"
                Return "شراء"

            Case "SALE"
                Return "بيع"

            Case "SALE_RETURN"
                Return "مردود بيع"

            Case "PCH_RETURN"
                Return "مردود شراء"

            Case "DAMAGE"
                Return "تالف"

            Case "ISSUE_IN"
                Return "صرف داخلي"

            Case "ISSUE_OUT"
                Return "صرف خارجي"

            Case "STORE_TRANS_FROM"
                Return "تحويل خروج"

            Case "STORE_TRANS_TO"
                Return "تحويل دخول"

            Case "STORE_DIFF"
                Return "تسوية مخزون"

            Case "FRM_IM"
                Return "تصنيع"

            Case "رصيد افتتاحي"
                Return "رصيد افتتاحي"

            Case Else
                Return movementType

        End Select

    End Function


    Private Function GetDocumentPrefix(movementType As String) As String

        Select Case movementType

            Case "PURCHASE"
                Return "شراء"

            Case "SALE"
                Return "بيع"

            Case "SALE_RETURN"
                Return "مردود بيع"

            Case "PCH_RETURN"
                Return "مردود شراء"

            Case "DAMAGE"
                Return "تالف"

            Case "ISSUE_IN"
                Return "صرف داخلي"

            Case "ISSUE_OUT"
                Return "صرف خارجي"

            Case "STORE_TRANS_FROM", "STORE_TRANS_TO"
                Return "تحويل"

            Case "STORE_DIFF"
                Return "تسوية"

            Case "FRM_IM"
                Return "تصنيع"

            Case Else
                Return "مستند"

        End Select

    End Function


    Private Sub ShowColumn(
        columnName As String,
        headerText As String,
        width As Integer
    )

        If GridLedger.Columns.Contains(columnName) Then

            GridLedger.Columns(columnName).Visible = True
            GridLedger.Columns(columnName).HeaderText = headerText
            GridLedger.Columns(columnName).Width = width

        End If

    End Sub

    Private Sub FormatNumericColumn(columnName As String)

        If GridLedger.Columns.Contains(columnName) Then

            GridLedger.Columns(columnName).DefaultCellStyle.Format = "N2"
            GridLedger.Columns(columnName).DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter

        End If

    End Sub

    Private Sub CalculateTotals()

        Dim totalIn As Decimal = 0
        Dim totalOut As Decimal = 0
        Dim finalBalance As Decimal = 0

        If GridLedger.DataSource Is Nothing Then
            ClearTotals()
            Return
        End If

        Dim dt As DataTable =
            TryCast(GridLedger.DataSource, DataTable)

        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            ClearTotals()
            Return
        End If

        For Each row As DataRow In dt.Rows

            If row("MovementType").ToString() <> "رصيد افتتاحي" Then

                totalIn += SafeDecimal(row("QtyIn"))
                totalOut += SafeDecimal(row("QtyOut"))

            End If

            finalBalance = SafeDecimal(row("BalanceQty"))

        Next

        Txt_TotalIn.Text = totalIn.ToString("N2")
        Txt_TotalOut.Text = totalOut.ToString("N2")
        Txt_FinalBalance.Text = finalBalance.ToString("N2")

    End Sub
    Private Sub ClearTotals()

        Txt_TotalIn.Text = "0.00"
        Txt_TotalOut.Text = "0.00"
        Txt_FinalBalance.Text = "0.00"

    End Sub

    Private Function SafeDecimal(value As Object) As Decimal

        If value Is Nothing OrElse value Is DBNull.Value Then
            Return 0
        End If

        Dim result As Decimal = 0
        Decimal.TryParse(value.ToString(), result)

        Return result

    End Function

    Private Sub Btn_Print_Click(
        sender As Object,
        e As EventArgs
    ) Handles Btn_Print.Click

        If Not HasPrintableRows() Then Return
        PrepareLedgerPrint()

        Using printDialog As New PrintDialog()

            printDialog.Document = LedgerPrintDocument
            printDialog.AllowSomePages = False
            printDialog.UseEXDialog = True

            If printDialog.ShowDialog() = DialogResult.OK Then
                LedgerPrintDocument.Print()
            End If

        End Using

    End Sub

    Private Sub Btn_Preview_Click(
        sender As Object,
        e As EventArgs
    ) Handles Btn_Preview.Click

        If Not HasPrintableRows() Then Return
        PrepareLedgerPrint()

        Using previewDialog As New PrintPreviewDialog()

            previewDialog.Document = LedgerPrintDocument
            previewDialog.WindowState = FormWindowState.Maximized
            previewDialog.Text = "معاينة كشف حركة صنف"
            previewDialog.ShowDialog(Me)

        End Using

    End Sub

    Private Sub Btn_Pdf_Click(
        sender As Object,
        e As EventArgs
    ) Handles Btn_Pdf.Click

        If Not HasPrintableRows() Then Return

        Dim pdfPrinterName As String = GetPdfPrinterName()

        If pdfPrinterName = "" Then

            MessageBox.Show(
                "طابعة Microsoft Print to PDF غير مثبتة على هذا الجهاز.",
                "تنبيه",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            )

            Return

        End If

        Using saveDialog As New SaveFileDialog()

            saveDialog.Title = "حفظ كشف الحركة كملف PDF"
            saveDialog.Filter = "PDF Files (*.pdf)|*.pdf"
            saveDialog.FileName =
                "كشف حركة صنف " & Date.Today.ToString("yyyyMMdd") & ".pdf"

            If saveDialog.ShowDialog(Me) <> DialogResult.OK Then
                Return
            End If

            Dim oldPrinterSettings As PrinterSettings =
                LedgerPrintDocument.PrinterSettings
            Dim oldPrintController As PrintController =
                LedgerPrintDocument.PrintController

            Try

                PrepareLedgerPrint()

                Dim pdfPrinterSettings As New PrinterSettings()
                pdfPrinterSettings.PrinterName = pdfPrinterName
                pdfPrinterSettings.PrintToFile = True
                pdfPrinterSettings.PrintFileName = saveDialog.FileName

                LedgerPrintDocument.PrinterSettings = pdfPrinterSettings
                LedgerPrintDocument.PrintController = New StandardPrintController()
                LedgerPrintDocument.Print()

                MessageBox.Show(
                    "تم حفظ ملف PDF بنجاح.",
                    "نجاح",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )

            Catch ex As Exception

                MessageBox.Show(
                    ex.Message,
                    "خطأ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                )

            Finally

                LedgerPrintDocument.PrinterSettings = oldPrinterSettings
                LedgerPrintDocument.PrintController = oldPrintController

            End Try

        End Using

    End Sub

    Private Function HasPrintableRows() As Boolean

        If GridLedger.Rows.Count > 0 Then
            Return True
        End If

        MessageBox.Show(
            "لا توجد بيانات للطباعة.",
            "تنبيه",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        )

        Return False

    End Function

    Private Sub PrepareLedgerPrint()

        _PrintRowIndex = 0
        _PrintDateTime = Date.Now
        LedgerPrintDocument.DefaultPageSettings.Landscape = True
        LedgerPrintDocument.DocumentName = "كشف حركة صنف"

    End Sub

    Private Function GetPdfPrinterName() As String

        For Each printerName As String In PrinterSettings.InstalledPrinters

            If printerName.IndexOf(
                "Microsoft Print to PDF",
                StringComparison.OrdinalIgnoreCase
            ) >= 0 Then

                Return printerName

            End If

        Next

        Return ""

    End Function

    Private Sub LedgerPrintDocument_BeginPrint(
        sender As Object,
        e As PrintEventArgs
    ) Handles LedgerPrintDocument.BeginPrint

        _PrintRowIndex = 0

    End Sub

    Private Sub LedgerPrintDocument_PrintPage(
        sender As Object,
        e As PrintPageEventArgs
    ) Handles LedgerPrintDocument.PrintPage

        Dim bounds As New Rectangle(
            e.PageBounds.Left + 28,
            e.MarginBounds.Top,
            e.PageBounds.Width - 56,
            e.MarginBounds.Height
        )
        Dim y As Integer = bounds.Top

        Using shopFont As New Font("Segoe UI", 15.0!, FontStyle.Bold),
              shopSubFont As New Font("Segoe UI", 10.0!, FontStyle.Bold),
              titleFont As New Font("Segoe UI", 11.0!, FontStyle.Bold),
              headerFont As New Font("Segoe UI", 7.5!, FontStyle.Bold),
              rowFont As New Font("Segoe UI Semibold", 7.5!, FontStyle.Bold),
              smallFont As New Font("Segoe UI", 8.0!)

            Dim rtlFormat As New StringFormat()
            rtlFormat.Alignment = StringAlignment.Far
            rtlFormat.LineAlignment = StringAlignment.Center
            rtlFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft

            Dim centerFormat As New StringFormat()
            centerFormat.Alignment = StringAlignment.Center
            centerFormat.LineAlignment = StringAlignment.Center

            DrawReportStoreHeader(e.Graphics, bounds, y, shopFont, shopSubFont, centerFormat)

            e.Graphics.DrawString(
                "كشف حركة صنف",
                titleFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 24),
                centerFormat
            )

            y += 28

            Dim storeName As String = "كل المخازن"

            If Cmb_Store.Text.Trim() <> "" Then
                storeName = Cmb_Store.Text.Trim()
            End If

            Dim info As String =
                "الصنف: " & Txt_ItemName.Text &
                "    المخزن: " & storeName &
                "    من: " & Dtp_From.Value.ToString("yyyy/MM/dd") &
                "    إلى: " & Dtp_To.Value.ToString("yyyy/MM/dd")

            e.Graphics.DrawString(
                info,
                smallFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 24),
                rtlFormat
            )

            y += 32

            Dim visibleColumns As New List(Of DataGridViewColumn)()

            For Each col As DataGridViewColumn In GridLedger.Columns
                If col.Visible Then
                    visibleColumns.Add(col)
                End If
            Next

            If visibleColumns.Count = 0 Then
                e.HasMorePages = False
                Return
            End If

            Dim totalGridWidth As Integer = 0

            For Each col As DataGridViewColumn In visibleColumns
                totalGridWidth += col.Width
            Next

            If totalGridWidth <= 0 Then
                e.HasMorePages = False
                Return
            End If

            Dim scale As Decimal = CDec(bounds.Width - 2) / CDec(totalGridWidth)
            Dim rowHeight As Integer = 24
            Dim x As Integer = bounds.Right

            For Each col As DataGridViewColumn In visibleColumns

                Dim colWidth As Integer = CInt(col.Width * scale)
                x -= colWidth

                Dim rect As New Rectangle(x, y, colWidth, rowHeight)

                Using backBrush As New SolidBrush(Color.FromArgb(41, 57, 85))
                    e.Graphics.FillRectangle(backBrush, rect)
                End Using

                e.Graphics.DrawRectangle(Pens.DarkGray, rect)
                e.Graphics.DrawString(
                    col.HeaderText,
                    headerFont,
                    Brushes.White,
                    rect,
                    centerFormat
                )

            Next

            y += rowHeight

            Dim rowsPerPage As Integer = CalculatePrintableRowsPerPage(y, bounds.Bottom, rowHeight)
            Dim totalPages As Integer = CalculateTotalPrintPages(GridLedger.Rows.Count, rowsPerPage)
            Dim currentPage As Integer = CalculateCurrentPrintPage(_PrintRowIndex, rowsPerPage)

            While _PrintRowIndex < GridLedger.Rows.Count

                If y + rowHeight > bounds.Bottom - 42 Then
                    DrawReportFooter(e.Graphics, bounds, currentPage, totalPages, smallFont, centerFormat)
                    e.HasMorePages = True
                    Return
                End If

                Dim row As DataGridViewRow = GridLedger.Rows(_PrintRowIndex)
                x = bounds.Right

                For Each col As DataGridViewColumn In visibleColumns

                    Dim colWidth As Integer = CInt(col.Width * scale)
                    x -= colWidth

                    Dim rect As New Rectangle(x, y, colWidth, rowHeight)
                    Dim valueText As String = ""

                    If row.Cells(col.Name).Value IsNot Nothing AndAlso
                       row.Cells(col.Name).Value IsNot DBNull.Value Then

                        valueText = row.Cells(col.Name).FormattedValue.ToString()

                    End If

                    If col.Name = "QtyIn" AndAlso valueText <> "" Then

                        Using inBrush As New SolidBrush(Color.FromArgb(209, 250, 229))
                            e.Graphics.FillRectangle(inBrush, rect)
                        End Using

                    ElseIf col.Name = "QtyOut" AndAlso valueText <> "" Then

                        Using outBrush As New SolidBrush(Color.FromArgb(254, 226, 226))
                            e.Graphics.FillRectangle(outBrush, rect)
                        End Using

                    Else

                        e.Graphics.FillRectangle(Brushes.White, rect)

                    End If

                    e.Graphics.DrawRectangle(Pens.LightGray, rect)
                    e.Graphics.DrawString(
                        valueText,
                        rowFont,
                        Brushes.Black,
                        rect,
                        centerFormat
                    )

                Next

                y += rowHeight
                _PrintRowIndex += 1

            End While

            y += 10

            Dim totals As String =
                "إجمالي الدخول: " & Txt_TotalIn.Text &
                "    إجمالي الخروج: " & Txt_TotalOut.Text &
                "    الرصيد النهائي: " & Txt_FinalBalance.Text

            e.Graphics.DrawString(
                totals,
                smallFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 26),
                rtlFormat
            )

            DrawReportFooter(e.Graphics, bounds, currentPage, totalPages, smallFont, centerFormat)

        End Using

        e.HasMorePages = False

    End Sub

    Private Sub DrawReportStoreHeader(
        graphics As Graphics,
        bounds As Rectangle,
        ByRef y As Integer,
        shopFont As Font,
        shopSubFont As Font,
        centerFormat As StringFormat
    )

        Dim mainTitle As String = SBill_Title_1
        Dim subTitle As String = SBill_Title_2

        'If String.IsNullOrWhiteSpace(mainTitle) Then
        '    mainTitle = Txt_ShopName.Text.Trim()
        'End If

        If Not String.IsNullOrWhiteSpace(mainTitle) Then
            graphics.DrawString(
                mainTitle,
                shopFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 30),
                centerFormat
            )

            y += 32
        End If

        If Not String.IsNullOrWhiteSpace(subTitle) Then
            graphics.DrawString(
                subTitle,
                shopSubFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 22),
                centerFormat
            )

            y += 24
        End If

    End Sub

    Private Function CalculatePrintableRowsPerPage(
        firstRowY As Integer,
        pageBottom As Integer,
        rowHeight As Integer
    ) As Integer

        Dim printableHeight As Integer = (pageBottom - 42) - firstRowY

        If printableHeight <= 0 OrElse rowHeight <= 0 Then
            Return 1
        End If

        Return Math.Max(1, printableHeight \ rowHeight)

    End Function

    Private Function CalculateTotalPrintPages(
        totalRows As Integer,
        rowsPerPage As Integer
    ) As Integer

        If rowsPerPage <= 0 Then
            Return 1
        End If

        Return Math.Max(1, CInt(Math.Ceiling(totalRows / CDbl(rowsPerPage))))

    End Function

    Private Function CalculateCurrentPrintPage(
        rowIndex As Integer,
        rowsPerPage As Integer
    ) As Integer

        If rowsPerPage <= 0 Then
            Return 1
        End If

        Return Math.Max(1, (rowIndex \ rowsPerPage) + 1)

    End Function

    Private Sub DrawReportFooter(
        graphics As Graphics,
        bounds As Rectangle,
        currentPage As Integer,
        totalPages As Integer,
        footerFont As Font,
        centerFormat As StringFormat
    )

        If _PrintDateTime = DateTime.MinValue Then
            _PrintDateTime = Date.Now
        End If

        Dim footerY As Integer = bounds.Bottom - 28
        Dim sideWidth As Integer = CInt(bounds.Width * 0.34)
        Dim centerWidth As Integer = bounds.Width - (sideWidth * 2)

        Using rightFormat As New StringFormat(),
              leftFormat As New StringFormat()

            rightFormat.Alignment = StringAlignment.Far
            rightFormat.LineAlignment = StringAlignment.Center
            rightFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft

            leftFormat.Alignment = StringAlignment.Near
            leftFormat.LineAlignment = StringAlignment.Center

            graphics.DrawLine(Pens.LightGray, bounds.Left, footerY - 4, bounds.Right, footerY - 4)

            graphics.DrawString(
                "المعد: " & USER_NAME,
                footerFont,
                Brushes.Black,
                New Rectangle(bounds.Right - sideWidth, footerY, sideWidth, 22),
                rightFormat
            )

            graphics.DrawString(
                currentPage.ToString() & "/" & totalPages.ToString(),
                footerFont,
                Brushes.Black,
                New Rectangle(bounds.Left + sideWidth, footerY, centerWidth, 22),
                centerFormat
            )

            graphics.DrawString(
                "تاريخ الطباعة: " & _PrintDateTime.ToString("yyyy/MM/dd HH:mm"),
                footerFont,
                Brushes.Black,
                New Rectangle(bounds.Left, footerY, sideWidth, 22),
                leftFormat
            )

        End Using

    End Sub

    Private Sub Btn_Close_Click(
        sender As Object,
        e As EventArgs
    ) Handles Btn_Close.Click

        Me.Close()

    End Sub

End Class
