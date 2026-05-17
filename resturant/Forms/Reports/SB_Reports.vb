Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Drawing.Printing

Public Class SB_Reports
    Dim rs As New Resizer
    Private B_Berfet_Pdf_btn As Button
    Private _billProfitPrintRowIndex As Integer
    Private _billProfitPrintPageNumber As Integer
    Private _billProfitPrintTotalPages As Integer = 1
    Private _billProfitPrintRowsPerPage As Integer
    Private _billProfitPrintTotalRows As Integer
    Private _billProfitPrintDateTime As Date
    Private _billProfitSummaryPrinted As Boolean

    Private Sub B_berfet_btn_Click(sender As Object, e As EventArgs) Handles B_berfet_btn.Click
        Bill_Perfet_Select_By_Date()
    End Sub

    Private Sub Bill_Perfet_Select_By_Date()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "Bill_Perfet_Select"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.SelectCommand.CommandTimeout = 0
        C.Da.Fill(C.Dt)
        Bill_Berfet_DGV.DataSource = C.Dt
        B_Berfet_Count()
    End Sub

    Private Sub B_Berfet_Count()
        Dim S As Double = 0, S2 As Double = 0

        For i = 0 To Bill_Berfet_DGV.Rows.Count - 1
            S += Bill_Berfet_DGV.Rows(i).Cells("Bill_Berfet_CL").Value
            S2 += Bill_Berfet_DGV.Rows(i).Cells("Bill_Total_CL").Value
        Next
        B_Berfet_T_txt.Text = S.ToString("N")
        B_BerfetCounter_txt.Text = Bill_Berfet_DGV.Rows.Count.ToString
        B_SB_T_txt.Text = S2.ToString("N")

    End Sub

    Private Sub B_Berfet_Print_btn_Click(sender As Object, e As EventArgs) Handles B_Berfet_Print_btn.Click
        B_Berfet_Print()
    End Sub

    Private Sub SetupPdfButton()

        If B_Berfet_Pdf_btn IsNot Nothing Then Return

        B_Berfet_Pdf_btn = New Button With {
            .BackColor = Color.White,
            .Cursor = Cursors.Hand,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI Semibold", 12.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte)),
            .Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703,
            .ImageAlign = ContentAlignment.MiddleRight,
            .Location = New Point(270, 3),
            .Name = "B_Berfet_Pdf_btn",
            .RightToLeft = RightToLeft.Yes,
            .Size = New Size(133, 38),
            .TabIndex = B_Berfet_Print_btn.TabIndex + 1,
            .Text = "PDF",
            .TextAlign = ContentAlignment.MiddleLeft,
            .UseVisualStyleBackColor = False
        }

        AddHandler B_Berfet_Pdf_btn.Click, AddressOf B_Berfet_Pdf_btn_Click
        Me.Controls.Add(B_Berfet_Pdf_btn)
        B_Berfet_Pdf_btn.BringToFront()

    End Sub

    Private Sub B_Berfet_Pdf_btn_Click(sender As Object, e As EventArgs)
        ExportBillProfitReportPdf()
    End Sub

    Private Sub B_Berfet_Print()

        If GetBillProfitPrintableRowsCount() = 0 Then
            MsgBox("لا توجد بيانات للطباعة.", MsgBoxStyle.Exclamation, "")
            Return
        End If

        Using printDocument As PrintDocument = CreateBillProfitPrintDocument()
            Using previewDialog As New PrintPreviewDialog()
                previewDialog.Document = printDocument
                previewDialog.WindowState = FormWindowState.Maximized
                previewDialog.Text = "معاينة تقرير ربح الفواتير"
                previewDialog.ShowDialog(Me)
            End Using
        End Using

    End Sub

    Private Sub ExportBillProfitReportPdf()

        If GetBillProfitPrintableRowsCount() = 0 Then
            MsgBox("لا توجد بيانات للتصدير.", MsgBoxStyle.Exclamation, "")
            Return
        End If

        Dim pdfPrinterName As String = GetPdfPrinterName()

        If pdfPrinterName = "" Then
            MsgBox("طابعة Microsoft Print to PDF غير متوفرة على هذا الجهاز.", MsgBoxStyle.Exclamation, "")
            Return
        End If

        Using saveDialog As New SaveFileDialog()
            saveDialog.Filter = "PDF Files (*.pdf)|*.pdf"
            saveDialog.FileName = "تقرير ربح الفواتير " & Date.Now.ToString("yyyyMMdd_HHmm") & ".pdf"
            saveDialog.Title = "حفظ تقرير ربح الفواتير PDF"

            If saveDialog.ShowDialog(Me) <> DialogResult.OK Then Return

            Using printDocument As PrintDocument = CreateBillProfitPrintDocument()
                printDocument.PrinterSettings.PrinterName = pdfPrinterName
                printDocument.PrinterSettings.PrintToFile = True
                printDocument.PrinterSettings.PrintFileName = saveDialog.FileName
                printDocument.PrintController = New StandardPrintController()
                printDocument.Print()
            End Using
        End Using

    End Sub

    Private Function CreateBillProfitPrintDocument() As PrintDocument

        Dim printDocument As New PrintDocument()
        printDocument.DocumentName = "تقرير ربح الفواتير"
        printDocument.DefaultPageSettings.Landscape = True
        printDocument.DefaultPageSettings.Margins = New Margins(30, 30, 40, 45)

        AddHandler printDocument.BeginPrint, AddressOf BillProfitPrintDocument_BeginPrint
        AddHandler printDocument.PrintPage, AddressOf BillProfitPrintDocument_PrintPage

        Return printDocument

    End Function

    Private Sub BillProfitPrintDocument_BeginPrint(sender As Object, e As PrintEventArgs)

        _billProfitPrintRowIndex = 0
        _billProfitPrintPageNumber = 1
        _billProfitPrintTotalPages = 1
        _billProfitPrintRowsPerPage = 0
        _billProfitPrintTotalRows = GetBillProfitPrintableRowsCount()
        _billProfitPrintDateTime = Date.Now
        _billProfitSummaryPrinted = False

    End Sub

    Private Sub BillProfitPrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)

        Dim bounds As Rectangle = e.MarginBounds
        Dim y As Integer = bounds.Top

        Using storeTitleFont As New Font("Segoe UI Semibold", 14.0!, FontStyle.Bold),
              storeSubTitleFont As New Font("Segoe UI", 10.0!, FontStyle.Regular),
              titleFont As New Font("Segoe UI Semibold", 12.0!, FontStyle.Bold),
              infoFont As New Font("Segoe UI", 8.0!, FontStyle.Regular),
              headerFont As New Font("Segoe UI", 8.0!, FontStyle.Bold),
              rowFont As New Font("Segoe UI Semibold", 8.0!, FontStyle.Bold),
              totalFont As New Font("Segoe UI Semibold", 8.5!, FontStyle.Bold)

            Using centerFormat As New StringFormat(),
                  rightFormat As New StringFormat(),
                  leftFormat As New StringFormat()

                centerFormat.Alignment = StringAlignment.Center
                centerFormat.LineAlignment = StringAlignment.Center
                centerFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft

                rightFormat.Alignment = StringAlignment.Far
                rightFormat.LineAlignment = StringAlignment.Center
                rightFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft

                leftFormat.Alignment = StringAlignment.Near
                leftFormat.LineAlignment = StringAlignment.Center

                DrawBillProfitStoreHeader(e.Graphics, bounds, y, storeTitleFont, storeSubTitleFont, centerFormat)

                e.Graphics.DrawString("تقرير ربح الفواتير", titleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 26), centerFormat)
                y += 28
                e.Graphics.DrawString(GetDateRangeText(), infoFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 22), centerFormat)
                y += 28

                Dim rowHeight As Integer = 26
                Dim tableBottom As Integer = bounds.Bottom - 38
                Dim headerHeight As Integer = 30
                Dim availableRowsHeight As Integer = Math.Max(rowHeight, tableBottom - y - headerHeight - 34)

                If _billProfitPrintRowsPerPage = 0 Then
                    _billProfitPrintRowsPerPage = Math.Max(1, availableRowsHeight \ rowHeight)
                    _billProfitPrintTotalPages = Math.Max(1, CInt(Math.Ceiling(_billProfitPrintTotalRows / CDbl(_billProfitPrintRowsPerPage))))
                End If

                DrawBillProfitTableHeader(e.Graphics, bounds, y, headerHeight, headerFont, centerFormat)
                y += headerHeight

                Dim rowsPrintedOnPage As Integer = 0

                While _billProfitPrintRowIndex < Bill_Berfet_DGV.Rows.Count
                    Dim row As DataGridViewRow = Bill_Berfet_DGV.Rows(_billProfitPrintRowIndex)

                    If row.IsNewRow Then
                        _billProfitPrintRowIndex += 1
                        Continue While
                    End If

                    If y + rowHeight > tableBottom Then
                        DrawBillProfitFooter(e.Graphics, bounds, infoFont, centerFormat, rightFormat, leftFormat)
                        _billProfitPrintPageNumber += 1
                        e.HasMorePages = True
                        Return
                    End If

                    DrawBillProfitRow(e.Graphics, bounds, y, rowHeight, rowFont, centerFormat, row, rowsPrintedOnPage Mod 2 = 1)
                    y += rowHeight
                    rowsPrintedOnPage += 1
                    _billProfitPrintRowIndex += 1
                End While

                If _billProfitSummaryPrinted = False Then
                    If y + 34 > tableBottom Then
                        _billProfitPrintTotalPages = Math.Max(_billProfitPrintTotalPages, _billProfitPrintPageNumber + 1)
                        DrawBillProfitFooter(e.Graphics, bounds, infoFont, centerFormat, rightFormat, leftFormat)
                        _billProfitPrintPageNumber += 1
                        e.HasMorePages = True
                        Return
                    End If

                    DrawBillProfitSummary(e.Graphics, bounds, y, 32, totalFont, centerFormat)
                    _billProfitSummaryPrinted = True
                End If

                DrawBillProfitFooter(e.Graphics, bounds, infoFont, centerFormat, rightFormat, leftFormat)
                e.HasMorePages = False
            End Using
        End Using

    End Sub

    Private Sub DrawBillProfitStoreHeader(ByVal graphics As Graphics, ByVal bounds As Rectangle, ByRef y As Integer, ByVal storeTitleFont As Font, ByVal storeSubTitleFont As Font, ByVal centerFormat As StringFormat)

        If Not String.IsNullOrWhiteSpace(SBill_Title_1) Then
            graphics.DrawString(SBill_Title_1, storeTitleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 28), centerFormat)
            y += 30
        End If

        If Not String.IsNullOrWhiteSpace(SBill_Title_2) Then
            graphics.DrawString(SBill_Title_2, storeSubTitleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 22), centerFormat)
            y += 24
        End If

    End Sub

    Private Sub DrawBillProfitTableHeader(ByVal graphics As Graphics, ByVal bounds As Rectangle, ByVal y As Integer, ByVal height As Integer, ByVal headerFont As Font, ByVal centerFormat As StringFormat)

        Dim headers() As String = {"التاريخ", "رقم الفاتورة", "العميل", "الإجمالي", "التخفيض", "الصافي", "ربح الفاتورة"}
        Dim widths() As Integer = GetBillProfitColumnWidths(bounds.Width)
        Dim x As Integer = bounds.Right

        Using headerBrush As New SolidBrush(Color.FromArgb(35, 48, 68))
            For i As Integer = 0 To headers.Length - 1
                Dim rect As New Rectangle(x - widths(i), y, widths(i), height)
                graphics.FillRectangle(headerBrush, rect)
                graphics.DrawRectangle(Pens.White, rect)
                graphics.DrawString(headers(i), headerFont, Brushes.White, rect, centerFormat)
                x -= widths(i)
            Next
        End Using

    End Sub

    Private Sub DrawBillProfitRow(ByVal graphics As Graphics, ByVal bounds As Rectangle, ByVal y As Integer, ByVal height As Integer, ByVal rowFont As Font, ByVal centerFormat As StringFormat, ByVal row As DataGridViewRow, ByVal isAlternate As Boolean)

        Dim values() As String = {
            FormatDateValue(GetGridCellText(row, "DataGridViewTextBoxColumn1")),
            GetGridCellText(row, "DataGridViewTextBoxColumn32"),
            GetGridCellText(row, "Column3"),
            FormatNumberValue(GetGridCellText(row, "DataGridViewTextBoxColumn34"), "N2"),
            FormatNumberValue(GetGridCellText(row, "DataGridViewTextBoxColumn35"), "N2"),
            FormatNumberValue(GetGridCellText(row, "Bill_Total_CL"), "N2"),
            FormatNumberValue(GetGridCellText(row, "Bill_Berfet_CL"), "N2")
        }

        Dim widths() As Integer = GetBillProfitColumnWidths(bounds.Width)
        Dim x As Integer = bounds.Right

        Using alternateBrush As New SolidBrush(Color.FromArgb(248, 250, 252))
            Dim backBrush As Brush = If(isAlternate, alternateBrush, Brushes.White)

            For i As Integer = 0 To values.Length - 1
                Dim rect As New Rectangle(x - widths(i), y, widths(i), height)
                graphics.FillRectangle(backBrush, rect)
                graphics.DrawRectangle(Pens.LightGray, rect)
                graphics.DrawString(values(i), rowFont, Brushes.Black, rect, centerFormat)
                x -= widths(i)
            Next
        End Using

    End Sub

    Private Sub DrawBillProfitSummary(ByVal graphics As Graphics, ByVal bounds As Rectangle, ByVal y As Integer, ByVal height As Integer, ByVal totalFont As Font, ByVal centerFormat As StringFormat)

        Dim values() As String = {
            "الإجمالي",
            GetBillProfitPrintableRowsCount().ToString("N0"),
            "",
            GetGridColumnTotal("DataGridViewTextBoxColumn34").ToString("N2"),
            GetGridColumnTotal("DataGridViewTextBoxColumn35").ToString("N2"),
            GetGridColumnTotal("Bill_Total_CL").ToString("N2"),
            GetGridColumnTotal("Bill_Berfet_CL").ToString("N2")
        }

        Dim widths() As Integer = GetBillProfitColumnWidths(bounds.Width)
        Dim x As Integer = bounds.Right

        Using summaryBrush As New SolidBrush(Color.FromArgb(236, 253, 245))
            For i As Integer = 0 To values.Length - 1
                Dim rect As New Rectangle(x - widths(i), y, widths(i), height)
                graphics.FillRectangle(summaryBrush, rect)
                graphics.DrawRectangle(Pens.LightGray, rect)
                graphics.DrawString(values(i), totalFont, Brushes.Black, rect, centerFormat)
                x -= widths(i)
            Next
        End Using

    End Sub

    Private Sub DrawBillProfitFooter(ByVal graphics As Graphics, ByVal bounds As Rectangle, ByVal footerFont As Font, ByVal centerFormat As StringFormat, ByVal rightFormat As StringFormat, ByVal leftFormat As StringFormat)

        Dim footerTop As Integer = bounds.Bottom - 24
        Dim sideWidth As Integer = bounds.Width \ 3
        Dim centerWidth As Integer = bounds.Width - (sideWidth * 2)

        graphics.DrawLine(Pens.LightGray, bounds.Left, footerTop - 5, bounds.Right, footerTop - 5)
        graphics.DrawString("المعد: " & USER_NAME, footerFont, Brushes.Black, New Rectangle(bounds.Right - sideWidth, footerTop, sideWidth, 22), rightFormat)
        graphics.DrawString(_billProfitPrintPageNumber.ToString() & "/" & _billProfitPrintTotalPages.ToString(), footerFont, Brushes.Black, New Rectangle(bounds.Left + sideWidth, footerTop, centerWidth, 22), centerFormat)
        graphics.DrawString("تاريخ الطباعة: " & _billProfitPrintDateTime.ToString("yyyy/MM/dd HH:mm"), footerFont, Brushes.Black, New Rectangle(bounds.Left, footerTop, sideWidth, 22), leftFormat)

    End Sub

    Private Function GetBillProfitColumnWidths(ByVal totalWidth As Integer) As Integer()

        Dim dateWidth As Integer = CInt(totalWidth * 0.12)
        Dim billWidth As Integer = CInt(totalWidth * 0.11)
        Dim agentWidth As Integer = CInt(totalWidth * 0.23)
        Dim totalColWidth As Integer = CInt(totalWidth * 0.13)
        Dim discountWidth As Integer = CInt(totalWidth * 0.13)
        Dim pureWidth As Integer = CInt(totalWidth * 0.13)
        Dim profitWidth As Integer = totalWidth - dateWidth - billWidth - agentWidth - totalColWidth - discountWidth - pureWidth

        Return New Integer() {dateWidth, billWidth, agentWidth, totalColWidth, discountWidth, pureWidth, profitWidth}

    End Function

    Private Function GetGridCellText(ByVal row As DataGridViewRow, ByVal columnName As String) As String

        If Not Bill_Berfet_DGV.Columns.Contains(columnName) Then Return ""
        If row.Cells(columnName).Value Is Nothing OrElse IsDBNull(row.Cells(columnName).Value) Then Return ""

        Return row.Cells(columnName).Value.ToString()

    End Function

    Private Function FormatDateValue(ByVal value As String) As String

        Dim dateValue As Date
        If Date.TryParse(value, dateValue) Then Return dateValue.ToString("yyyy/MM/dd")

        Return value

    End Function

    Private Function FormatNumberValue(ByVal value As String, ByVal format As String) As String

        Dim number As Double
        If Double.TryParse(value, number) Then Return number.ToString(format)

        Return value

    End Function

    Private Function GetBillProfitPrintableRowsCount() As Integer

        Dim count As Integer = 0

        For Each row As DataGridViewRow In Bill_Berfet_DGV.Rows
            If row.IsNewRow = False Then count += 1
        Next

        Return count

    End Function

    Private Function GetGridColumnTotal(ByVal columnName As String) As Double

        Dim total As Double = 0

        For Each row As DataGridViewRow In Bill_Berfet_DGV.Rows
            If row.IsNewRow Then Continue For

            Dim value As Double
            If Double.TryParse(GetGridCellText(row, columnName), value) Then total += value
        Next

        Return total

    End Function

    Private Function GetDateRangeText() As String

        Return "من تاريخ " & HOME.DateRange_Flate.D_F.Value.ToShortDateString & " إلى " & HOME.DateRange_Flate.D_T.Value.ToShortDateString

    End Function

    Private Function GetPdfPrinterName() As String

        For Each printerName As String In PrinterSettings.InstalledPrinters
            If printerName.IndexOf("Microsoft Print to PDF", StringComparison.OrdinalIgnoreCase) >= 0 Then Return printerName
        Next

        Return ""

    End Function

    Private Sub SB_Reports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupPdfButton()
        rs.FindAllControls(Me)
    End Sub

    Private Sub SB_Reports_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub
End Class
