Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Drawing.Printing

Public Class Costmers_Bills_Report

    Dim rs As New Resizer
    Private Agents_Pdf_Btn As Button
    Private _agentsPrintRowIndex As Integer
    Private _agentsPrintPageNumber As Integer
    Private _agentsPrintTotalPages As Integer = 1
    Private _agentsPrintRowsPerPage As Integer
    Private _agentsPrintTotalRows As Integer
    Private _agentsPrintDateTime As Date
    Private _agentsSummaryPrinted As Boolean

    Private Sub Empty_F_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupPdfButton()
        rs.FindAllControls(Me)
    End Sub

    Private Sub Empty_F_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub


    Private Sub Agent_Bills_Btn_Click(sender As Object, e As EventArgs) Handles Agent_Bills_Btn.Click
        Agents_Bill_Num_Report_Insert()
    End Sub

    Private Sub Agents_Bill_Num_Report_Insert()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "Agents_Bill_Num_Report_Insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        Costmers_DG.DataSource = C.Dt
    End Sub

    Private Sub Agents_Print_Btn_Click(sender As Object, e As EventArgs) Handles Agents_Print_Btn.Click
        AGENTS_Bills_Num_Print()
    End Sub

    Private Sub SetupPdfButton()

        If Agents_Pdf_Btn IsNot Nothing Then Return

        Agents_Pdf_Btn = New Button With {
            .BackColor = Color.White,
            .Cursor = Cursors.Hand,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI Semibold", 12.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte)),
            .Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703,
            .ImageAlign = ContentAlignment.MiddleRight,
            .Location = New Point(492, 1),
            .Name = "Agents_Pdf_Btn",
            .RightToLeft = RightToLeft.Yes,
            .Size = New Size(133, 38),
            .TabIndex = Agents_Print_Btn.TabIndex + 1,
            .Text = "PDF",
            .TextAlign = ContentAlignment.MiddleLeft,
            .UseVisualStyleBackColor = False
        }

        AddHandler Agents_Pdf_Btn.Click, AddressOf Agents_Pdf_Btn_Click
        Me.Controls.Add(Agents_Pdf_Btn)
        Agents_Pdf_Btn.BringToFront()

    End Sub

    Private Sub Agents_Pdf_Btn_Click(sender As Object, e As EventArgs)
        ExportAgentsBillsReportPdf()
    End Sub


    Private Sub AGENTS_Bills_Num_Print()

        If GetPrintableRowsCount() = 0 Then
            MsgBox("لا توجد بيانات للطباعة.", MsgBoxStyle.Exclamation, "")
            Return
        End If

        Using printDocument As PrintDocument = CreateAgentsBillsPrintDocument()
            Using previewDialog As New PrintPreviewDialog()
                previewDialog.Document = printDocument
                previewDialog.WindowState = FormWindowState.Maximized
                previewDialog.Text = "معاينة تقرير فواتير الزبائن"
                previewDialog.ShowDialog(Me)
            End Using
        End Using

    End Sub

    Private Sub ExportAgentsBillsReportPdf()

        If GetPrintableRowsCount() = 0 Then
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
            saveDialog.FileName = "تقرير فواتير الزبائن " & Date.Now.ToString("yyyyMMdd_HHmm") & ".pdf"
            saveDialog.Title = "حفظ تقرير فواتير الزبائن PDF"

            If saveDialog.ShowDialog(Me) <> DialogResult.OK Then Return

            Using printDocument As PrintDocument = CreateAgentsBillsPrintDocument()
                printDocument.PrinterSettings.PrinterName = pdfPrinterName
                printDocument.PrinterSettings.PrintToFile = True
                printDocument.PrinterSettings.PrintFileName = saveDialog.FileName
                printDocument.PrintController = New StandardPrintController()
                printDocument.Print()
            End Using
        End Using

    End Sub

    Private Function CreateAgentsBillsPrintDocument() As PrintDocument

        Dim printDocument As New PrintDocument()
        printDocument.DocumentName = "تقرير فواتير الزبائن"
        printDocument.DefaultPageSettings.Landscape = False
        printDocument.DefaultPageSettings.Margins = New Margins(35, 35, 40, 45)

        AddHandler printDocument.BeginPrint, AddressOf AgentsBillsPrintDocument_BeginPrint
        AddHandler printDocument.PrintPage, AddressOf AgentsBillsPrintDocument_PrintPage

        Return printDocument

    End Function

    Private Sub AgentsBillsPrintDocument_BeginPrint(sender As Object, e As PrintEventArgs)

        _agentsPrintRowIndex = 0
        _agentsPrintPageNumber = 1
        _agentsPrintTotalPages = 1
        _agentsPrintRowsPerPage = 0
        _agentsPrintTotalRows = GetPrintableRowsCount()
        _agentsPrintDateTime = Date.Now
        _agentsSummaryPrinted = False

    End Sub

    Private Sub AgentsBillsPrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)

        Dim bounds As Rectangle = e.MarginBounds
        Dim y As Integer = bounds.Top

        Using storeTitleFont As New Font("Segoe UI Semibold", 14.0!, FontStyle.Bold),
              storeSubTitleFont As New Font("Segoe UI", 10.0!, FontStyle.Regular),
              titleFont As New Font("Segoe UI Semibold", 12.0!, FontStyle.Bold),
              infoFont As New Font("Segoe UI", 8.5!, FontStyle.Regular),
              headerFont As New Font("Segoe UI", 8.5!, FontStyle.Bold),
              rowFont As New Font("Segoe UI Semibold", 8.5!, FontStyle.Bold),
              totalFont As New Font("Segoe UI Semibold", 9.0!, FontStyle.Bold)

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

                DrawAgentsReportStoreHeader(e.Graphics, bounds, y, storeTitleFont, storeSubTitleFont, centerFormat)

                e.Graphics.DrawString("تقرير فواتير الزبائن", titleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 26), centerFormat)
                y += 28
                e.Graphics.DrawString(GetDateRangeText(), infoFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 22), centerFormat)
                y += 28

                Dim rowHeight As Integer = 28
                Dim tableBottom As Integer = bounds.Bottom - 38
                Dim headerHeight As Integer = 30
                Dim availableRowsHeight As Integer = Math.Max(rowHeight, tableBottom - y - headerHeight - 34)

                If _agentsPrintRowsPerPage = 0 Then
                    _agentsPrintRowsPerPage = Math.Max(1, availableRowsHeight \ rowHeight)
                    _agentsPrintTotalPages = Math.Max(1, CInt(Math.Ceiling(_agentsPrintTotalRows / CDbl(_agentsPrintRowsPerPage))))
                End If

                DrawAgentsBillsTableHeader(e.Graphics, bounds, y, headerHeight, headerFont, centerFormat)
                y += headerHeight

                Dim rowsPrintedOnPage As Integer = 0

                While _agentsPrintRowIndex < Costmers_DG.Rows.Count
                    Dim row As DataGridViewRow = Costmers_DG.Rows(_agentsPrintRowIndex)

                    If row.IsNewRow Then
                        _agentsPrintRowIndex += 1
                        Continue While
                    End If

                    If y + rowHeight > tableBottom Then
                        DrawAgentsReportFooter(e.Graphics, bounds, infoFont, centerFormat, rightFormat, leftFormat)
                        _agentsPrintPageNumber += 1
                        e.HasMorePages = True
                        Return
                    End If

                    DrawAgentsBillsRow(e.Graphics, bounds, y, rowHeight, rowFont, centerFormat, row, rowsPrintedOnPage Mod 2 = 1)
                    y += rowHeight
                    rowsPrintedOnPage += 1
                    _agentsPrintRowIndex += 1
                End While

                If _agentsPrintTotalRows = 0 Then
                    e.Graphics.DrawString("لا توجد بيانات", rowFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, rowHeight), centerFormat)
                    y += rowHeight
                End If

                If _agentsSummaryPrinted = False Then
                    If y + 34 > tableBottom Then
                        _agentsPrintTotalPages = Math.Max(_agentsPrintTotalPages, _agentsPrintPageNumber + 1)
                        DrawAgentsReportFooter(e.Graphics, bounds, infoFont, centerFormat, rightFormat, leftFormat)
                        _agentsPrintPageNumber += 1
                        e.HasMorePages = True
                        Return
                    End If

                    DrawAgentsBillsSummary(e.Graphics, bounds, y, 32, totalFont, centerFormat)
                    _agentsSummaryPrinted = True
                End If

                DrawAgentsReportFooter(e.Graphics, bounds, infoFont, centerFormat, rightFormat, leftFormat)
                e.HasMorePages = False
            End Using
        End Using

    End Sub

    Private Sub DrawAgentsReportStoreHeader(ByVal graphics As Graphics, ByVal bounds As Rectangle, ByRef y As Integer, ByVal storeTitleFont As Font, ByVal storeSubTitleFont As Font, ByVal centerFormat As StringFormat)

        If Not String.IsNullOrWhiteSpace(SBill_Title_1) Then
            graphics.DrawString(SBill_Title_1, storeTitleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 28), centerFormat)
            y += 30
        End If

        If Not String.IsNullOrWhiteSpace(SBill_Title_2) Then
            graphics.DrawString(SBill_Title_2, storeSubTitleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 22), centerFormat)
            y += 24
        End If

    End Sub

    Private Sub DrawAgentsBillsTableHeader(ByVal graphics As Graphics, ByVal bounds As Rectangle, ByVal y As Integer, ByVal height As Integer, ByVal headerFont As Font, ByVal centerFormat As StringFormat)

        Dim headers() As String = {"الزبون", "عدد الفواتير", "الإجمالي"}
        Dim widths() As Integer = GetAgentsBillsColumnWidths(bounds.Width)
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

    Private Sub DrawAgentsBillsRow(ByVal graphics As Graphics, ByVal bounds As Rectangle, ByVal y As Integer, ByVal height As Integer, ByVal rowFont As Font, ByVal centerFormat As StringFormat, ByVal row As DataGridViewRow, ByVal isAlternate As Boolean)

        Dim values() As String = {
            GetCellText(row, "AGENT_NAME"),
            FormatNumberValue(GetCellText(row, "Bills_QTY"), "N0"),
            FormatNumberValue(GetCellText(row, "DataGridViewTextBoxColumn49"), "N2")
        }

        Dim widths() As Integer = GetAgentsBillsColumnWidths(bounds.Width)
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

    Private Sub DrawAgentsBillsSummary(ByVal graphics As Graphics, ByVal bounds As Rectangle, ByVal y As Integer, ByVal height As Integer, ByVal totalFont As Font, ByVal centerFormat As StringFormat)

        Dim values() As String = {
            "الإجمالي",
            GetBillsQtyTotal().ToString("N0"),
            GetBillsValueTotal().ToString("N2")
        }

        Dim widths() As Integer = GetAgentsBillsColumnWidths(bounds.Width)
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

    Private Sub DrawAgentsReportFooter(ByVal graphics As Graphics, ByVal bounds As Rectangle, ByVal footerFont As Font, ByVal centerFormat As StringFormat, ByVal rightFormat As StringFormat, ByVal leftFormat As StringFormat)

        Dim footerTop As Integer = bounds.Bottom - 24
        Dim sideWidth As Integer = bounds.Width \ 3
        Dim centerWidth As Integer = bounds.Width - (sideWidth * 2)

        graphics.DrawLine(Pens.LightGray, bounds.Left, footerTop - 5, bounds.Right, footerTop - 5)
        graphics.DrawString("المعد: " & USER_NAME, footerFont, Brushes.Black, New Rectangle(bounds.Right - sideWidth, footerTop, sideWidth, 22), rightFormat)
        graphics.DrawString(_agentsPrintPageNumber.ToString() & "/" & _agentsPrintTotalPages.ToString(), footerFont, Brushes.Black, New Rectangle(bounds.Left + sideWidth, footerTop, centerWidth, 22), centerFormat)
        graphics.DrawString("تاريخ الطباعة: " & _agentsPrintDateTime.ToString("yyyy/MM/dd HH:mm"), footerFont, Brushes.Black, New Rectangle(bounds.Left, footerTop, sideWidth, 22), leftFormat)

    End Sub

    Private Function GetAgentsBillsColumnWidths(ByVal totalWidth As Integer) As Integer()

        Dim agentWidth As Integer = CInt(totalWidth * 0.5)
        Dim countWidth As Integer = CInt(totalWidth * 0.2)
        Dim totalValueWidth As Integer = totalWidth - agentWidth - countWidth

        Return New Integer() {agentWidth, countWidth, totalValueWidth}

    End Function

    Private Function GetCellText(ByVal row As DataGridViewRow, ByVal columnName As String) As String

        If Not Costmers_DG.Columns.Contains(columnName) Then Return ""
        If row.Cells(columnName).Value Is Nothing OrElse IsDBNull(row.Cells(columnName).Value) Then Return ""

        Return row.Cells(columnName).Value.ToString()

    End Function

    Private Function FormatNumberValue(ByVal value As String, ByVal format As String) As String

        Dim number As Double
        If Double.TryParse(value, number) Then Return number.ToString(format)

        Return value

    End Function

    Private Function GetPrintableRowsCount() As Integer

        Dim count As Integer = 0

        For Each row As DataGridViewRow In Costmers_DG.Rows
            If row.IsNewRow = False Then count += 1
        Next

        Return count

    End Function

    Private Function GetBillsQtyTotal() As Double

        Dim total As Double = 0

        For Each row As DataGridViewRow In Costmers_DG.Rows
            If row.IsNewRow Then Continue For

            Dim value As Double
            If Double.TryParse(GetCellText(row, "Bills_QTY"), value) Then total += value
        Next

        Return total

    End Function

    Private Function GetBillsValueTotal() As Double

        Dim total As Double = 0

        For Each row As DataGridViewRow In Costmers_DG.Rows
            If row.IsNewRow Then Continue For

            Dim value As Double
            If Double.TryParse(GetCellText(row, "DataGridViewTextBoxColumn49"), value) Then total += value
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

End Class
