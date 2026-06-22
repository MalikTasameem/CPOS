Imports System.Data
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Linq
Imports ZXing

Public Class SalesPrintDocumentRenderer

    Private ReadOnly PrintData As SalesPrintData
    Private ReadOnly Profile As SalesPrintProfile
    Private PrintRowIndex As Integer = 0
    Private PageNumber As Integer = 1
    Private PrintDateTime As DateTime = DateTime.MinValue

    Public Sub New(data As SalesPrintData, profile As SalesPrintProfile)
        Me.PrintData = data
        Me.Profile = profile
    End Sub

    Public Function CreatePrintDocument() As PrintDocument
        Dim doc As New PrintDocument()
        doc.DocumentName = If(String.IsNullOrWhiteSpace(Profile.ProfileName), "تقرير مبيعات ديناميكي", Profile.ProfileName)
        doc.DefaultPageSettings.Landscape = Profile.Landscape
        doc.DefaultPageSettings.Margins = New Margins(Profile.MarginLeft, Profile.MarginRight, Profile.MarginTop, Profile.MarginBottom)
        doc.DefaultPageSettings.PaperSize = GetPaperSize()

        If String.IsNullOrWhiteSpace(Profile.PrinterName) = False Then doc.PrinterSettings.PrinterName = Profile.PrinterName

        AddHandler doc.BeginPrint, AddressOf PrintDocument_BeginPrint
        AddHandler doc.PrintPage, AddressOf PrintDocument_PrintPage

        Return doc
    End Function

    Private Function GetPaperSize() As PaperSize
        Select Case Profile.PaperKind.ToUpperInvariant()
            Case "A5"
                Return New PaperSize("A5", 583, 827)
            Case "A6"
                Return New PaperSize("A6", 413, 583)
            Case "RECEIPT"
                Dim rowsHeight As Integer = If(PrintData Is Nothing OrElse PrintData.Items Is Nothing, 0, PrintData.Items.Rows.Count * 32)
                Return New PaperSize("Receipt80", 280, Math.Max(650, 450 + rowsHeight))
            Case Else
                Return New PaperSize("A4", 827, 1169)
        End Select
    End Function

    Private Sub PrintDocument_BeginPrint(sender As Object, e As PrintEventArgs)
        PrintRowIndex = 0
        PageNumber = 1
        PrintDateTime = Date.Now
    End Sub

    Private Sub PrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)
        Dim bounds As Rectangle = e.MarginBounds
        Dim y As Integer = bounds.Top
        Dim isReceipt As Boolean = Profile.PaperKind.ToUpperInvariant() = "RECEIPT"

        Using titleFont As Font = CreateProfileFont(Profile.TitleFontSize, FontStyle.Bold),
              subTitleFont As Font = CreateProfileFont(Profile.SubTitleFontSize, FontStyle.Bold),
              infoFont As Font = CreateProfileFont(Profile.InfoFontSize, FontStyle.Bold),
              headerFont As Font = CreateProfileFont(Profile.HeaderFontSize, FontStyle.Bold),
              rowFont As Font = CreateProfileFont(Profile.RowFontSize, FontStyle.Regular),
              totalFont As Font = CreateProfileFont(Profile.TotalFontSize, FontStyle.Bold),
              footerFont As Font = CreateProfileFont(Profile.FooterFontSize, FontStyle.Regular)

            Using centerFormat As StringFormat = CreateFormat("Center"),
                  rightFormat As StringFormat = CreateFormat("Right"),
                  leftFormat As StringFormat = CreateFormat("Left")

                DrawHeader(e.Graphics, bounds, y, titleFont, subTitleFont, infoFont, centerFormat, rightFormat)

                Dim columns As List(Of SalesPrintComponent) = GetVisibleColumns()
                If IsSectionVisible("ItemsTable") AndAlso columns.Count > 0 Then
                    DrawItemsTable(e, bounds, y, columns, headerFont, rowFont, centerFormat, rightFormat)
                    If e.HasMorePages Then
                        PageNumber += 1
                        Return
                    End If
                End If

                DrawBottomSections(e.Graphics, bounds, y, totalFont, footerFont, centerFormat, rightFormat, leftFormat)
                DrawFooterLine(e.Graphics, bounds, footerFont, centerFormat)
            End Using
        End Using

        e.HasMorePages = False
    End Sub

    Private Sub DrawHeader(g As Graphics, bounds As Rectangle, ByRef y As Integer, titleFont As Font, subTitleFont As Font, infoFont As Font, centerFormat As StringFormat, rightFormat As StringFormat)
        If IsSectionVisible("Logo") AndAlso PrintData.LogoImage IsNot Nothing Then
            Dim defaultLogoSize As Integer = If(Profile.PaperKind.ToUpperInvariant() = "RECEIPT", 48, 72)
            Dim logoWidth As Integer = ClampLogoDimension(Profile.LogoWidth, defaultLogoSize)
            Dim logoHeight As Integer = ClampLogoDimension(Profile.LogoHeight, defaultLogoSize)
            Dim logoX As Integer = bounds.Left + ((bounds.Width - logoWidth) \ 2)
            g.DrawImage(PrintData.LogoImage, logoX, y, logoWidth, logoHeight)
            y += logoHeight + 4
        End If

        If IsSectionVisible("StoreTitle") AndAlso String.IsNullOrWhiteSpace(PrintData.StoreTitle) = False Then
            Using titleBrush As New SolidBrush(GetProfileColor(Profile.TitleForeColorArgb))
                g.DrawString(PrintData.StoreTitle, titleFont, titleBrush, New Rectangle(bounds.Left, y, bounds.Width, 30), centerFormat)
            End Using
            y += 32
        End If

        If IsSectionVisible("StoreAddress") AndAlso String.IsNullOrWhiteSpace(PrintData.StoreAddress) = False Then
            Using textBrush As New SolidBrush(GetProfileColor(Profile.TextForeColorArgb))
                g.DrawString(PrintData.StoreAddress, subTitleFont, textBrush, New Rectangle(bounds.Left, y, bounds.Width, 24), centerFormat)
            End Using
            y += 25
        End If

        If IsSectionVisible("BillInfo") Then
            Dim billText As String = "التاريخ: " & PrintData.BillDate
            If IsSectionVisible("BillNoDaily") = False AndAlso String.IsNullOrWhiteSpace(PrintData.BillNo) = False Then billText = "رقم الفاتورة: " & PrintData.BillNo & "    " & billText
            If IsPosUsageProfile() AndAlso IsSectionVisible("BillNoAuto") = False AndAlso String.IsNullOrWhiteSpace(PrintData.BillID) = False AndAlso PrintData.BillID <> PrintData.BillNo Then billText = "رقم آلي: " & PrintData.BillID & "    " & billText
            DrawInfoLine(g, bounds, y, billText, infoFont, rightFormat)
        End If

        If IsSectionVisible("BillNoDaily") AndAlso String.IsNullOrWhiteSpace(PrintData.BillNo) = False Then
            DrawInfoLine(g, bounds, y, "رقم يومي: " & PrintData.BillNo, infoFont, rightFormat)
        End If

        If IsSectionVisible("BillNoAuto") AndAlso String.IsNullOrWhiteSpace(PrintData.BillID) = False Then
            DrawInfoLine(g, bounds, y, "رقم آلي: " & PrintData.BillID, infoFont, rightFormat)
        End If

        If IsSectionVisible("Customer") AndAlso String.IsNullOrWhiteSpace(PrintData.CustomerName) = False Then
            Using textBrush As New SolidBrush(GetProfileColor(Profile.TextForeColorArgb))
                g.DrawString("العميل: " & PrintData.CustomerName, infoFont, textBrush, New Rectangle(bounds.Left, y, bounds.Width, 22), rightFormat)
            End Using
            y += 22
        End If

        If IsSectionVisible("Project") AndAlso String.IsNullOrWhiteSpace(PrintData.ProjectName) = False Then
            Using textBrush As New SolidBrush(GetProfileColor(Profile.TextForeColorArgb))
                g.DrawString("المشروع: " & PrintData.ProjectName, infoFont, textBrush, New Rectangle(bounds.Left, y, bounds.Width, 22), rightFormat)
            End Using
            y += 22
        End If

        If IsSectionVisible("UserName") AndAlso String.IsNullOrWhiteSpace(PrintData.UserName) = False Then
            Using textBrush As New SolidBrush(GetProfileColor(Profile.TextForeColorArgb))
                g.DrawString("المستخدم: " & PrintData.UserName, infoFont, textBrush, New Rectangle(bounds.Left, y, bounds.Width, 22), rightFormat)
            End Using
            y += 22
        End If

        y += 6
    End Sub

    Private Sub DrawInfoLine(g As Graphics, bounds As Rectangle, ByRef y As Integer, text As String, infoFont As Font, rightFormat As StringFormat)
        Using textBrush As New SolidBrush(GetProfileColor(Profile.TextForeColorArgb))
            g.DrawString(text, infoFont, textBrush, New Rectangle(bounds.Left, y, bounds.Width, 24), rightFormat)
        End Using
        y += 24
    End Sub

    Private Sub DrawItemsTable(e As PrintPageEventArgs, bounds As Rectangle, ByRef y As Integer, columns As List(Of SalesPrintComponent), headerFont As Font, rowFont As Font, centerFormat As StringFormat, rightFormat As StringFormat)
        Dim g As Graphics = e.Graphics
        Dim pageBottom As Integer = bounds.Bottom - 95
        Dim headerHeight As Integer = 26
        Dim widths As List(Of Integer) = CalculateColumnWidths(columns, bounds.Width)

        DrawTableHeader(g, bounds, y, columns, widths, headerFont, centerFormat)
        y += headerHeight

        If PrintData.Items Is Nothing Then Return

        While PrintRowIndex < PrintData.Items.Rows.Count
            Dim row As DataRow = PrintData.Items.Rows(PrintRowIndex)
            Dim rowHeight As Integer = CalculateRowHeight(g, row, columns, widths, rowFont)

            If y + rowHeight > pageBottom AndAlso y > bounds.Top Then
                e.HasMorePages = True
                Return
            End If

            DrawTableRow(g, bounds, y, row, columns, widths, rowHeight, rowFont, centerFormat, rightFormat)
            y += rowHeight
            PrintRowIndex += 1
        End While

        y += 8
    End Sub

    Private Sub DrawTableHeader(g As Graphics, bounds As Rectangle, y As Integer, columns As List(Of SalesPrintComponent), widths As List(Of Integer), headerFont As Font, centerFormat As StringFormat)
        Dim x As Integer = bounds.Right

        For i As Integer = 0 To columns.Count - 1
            x -= widths(i)
            Dim rect As New Rectangle(x, y, widths(i), 26)
            Using backBrush As New SolidBrush(GetProfileColor(Profile.HeaderBackColorArgb))
                g.FillRectangle(backBrush, rect)
            End Using
            DrawCellBorder(g, rect)
            Using foreBrush As New SolidBrush(GetProfileColor(Profile.HeaderForeColorArgb))
                g.DrawString(columns(i).DisplayName, headerFont, foreBrush, rect, centerFormat)
            End Using
        Next
    End Sub

    Private Sub DrawTableRow(g As Graphics, bounds As Rectangle, y As Integer, row As DataRow, columns As List(Of SalesPrintComponent), widths As List(Of Integer), rowHeight As Integer, rowFont As Font, centerFormat As StringFormat, rightFormat As StringFormat)
        Dim x As Integer = bounds.Right
        Dim rowBackColor As Color = GetProfileColor(Profile.RowBackColorArgb)
        If Profile.UseAlternatingRows AndAlso (PrintRowIndex Mod 2) <> 0 Then rowBackColor = GetProfileColor(Profile.AlternateRowBackColorArgb)

        For i As Integer = 0 To columns.Count - 1
            x -= widths(i)
            Dim rect As New Rectangle(x, y, widths(i), rowHeight)
            Using backBrush As New SolidBrush(rowBackColor)
                g.FillRectangle(backBrush, rect)
            End Using

            DrawCellBorder(g, rect)
            Dim value As String = GetRowValue(row, columns(i).ComponentCode)
            Dim fmt As StringFormat = If(columns(i).AlignmentValue = "Right", rightFormat, centerFormat)
            Using textBrush As New SolidBrush(GetProfileColor(Profile.TextForeColorArgb))
                g.DrawString(value, rowFont, textBrush, New Rectangle(rect.Left + 3, rect.Top + 2, rect.Width - 6, rect.Height - 4), fmt)
            End Using
        Next
    End Sub

    Private Function CalculateRowHeight(g As Graphics, row As DataRow, columns As List(Of SalesPrintComponent), widths As List(Of Integer), rowFont As Font) As Integer
        Dim h As Integer = 24
        Using rightFormat As StringFormat = CreateFormat("Right")
            For i As Integer = 0 To columns.Count - 1
                If columns(i).AlignmentValue <> "Right" Then Continue For
                Dim text As String = GetRowValue(row, columns(i).ComponentCode)
                If String.IsNullOrWhiteSpace(text) Then Continue For

                Dim size As SizeF = g.MeasureString(text, rowFont, Math.Max(20, widths(i) - 6), rightFormat)
                h = Math.Max(h, CInt(Math.Ceiling(size.Height)) + 8)
            Next
        End Using
        Return Math.Min(90, h)
    End Function

    Private Function CalculateColumnWidths(columns As List(Of SalesPrintComponent), totalWidth As Integer) As List(Of Integer)
        Dim widths As New List(Of Integer)()
        Dim totalWeight As Integer = Math.Max(1, columns.Sum(Function(c) Math.Max(10, c.WidthValue)))
        Dim used As Integer = 0

        For i As Integer = 0 To columns.Count - 1
            Dim w As Integer
            If i = columns.Count - 1 Then
                w = Math.Max(25, totalWidth - used)
            Else
                w = Math.Max(25, CInt(Math.Floor(totalWidth * (Math.Max(10, columns(i).WidthValue) / CDbl(totalWeight)))))
                used += w
            End If
            widths.Add(w)
        Next

        Return widths
    End Function

    Private Sub DrawBottomSections(g As Graphics, bounds As Rectangle, ByRef y As Integer, totalFont As Font, footerFont As Font, centerFormat As StringFormat, rightFormat As StringFormat, leftFormat As StringFormat)
        If IsSectionVisible("Totals") Then
            Dim summaryWidth As Integer = If(Profile.PaperKind.ToUpperInvariant() = "RECEIPT", bounds.Width, Math.Min(330, bounds.Width))
            Dim summaryLeft As Integer = bounds.Right - summaryWidth

            DrawSummaryRow(g, "الإجمالي", PrintData.TotalText, summaryLeft, y, summaryWidth, totalFont, rightFormat, leftFormat)
            y += 24
            DrawSummaryRow(g, "الخصم", PrintData.DiscountText, summaryLeft, y, summaryWidth, totalFont, rightFormat, leftFormat)
            y += 24
            DrawSummaryRow(g, "الصافي", PrintData.PureText, summaryLeft, y, summaryWidth, totalFont, rightFormat, leftFormat)
            y += 24
            If String.IsNullOrWhiteSpace(PrintData.PaymentName) = False Then
                DrawSummaryRow(g, "طريقة الدفع", PrintData.PaymentName, summaryLeft, y, summaryWidth, totalFont, rightFormat, leftFormat)
                y += 24
            End If
            DrawSummaryRow(g, "المدفوع", PrintData.PaidText, summaryLeft, y, summaryWidth, totalFont, rightFormat, leftFormat)
            y += 24
            DrawSummaryRow(g, "المتبقي", PrintData.RestText, summaryLeft, y, summaryWidth, totalFont, rightFormat, leftFormat)
            y += 30
        End If

        If IsSectionVisible("Notes") AndAlso String.IsNullOrWhiteSpace(PrintData.Notes) = False Then
            Using textBrush As New SolidBrush(GetProfileColor(Profile.TextForeColorArgb))
                g.DrawString("ملاحظات: " & PrintData.Notes, footerFont, textBrush, New Rectangle(bounds.Left, y, bounds.Width, 42), rightFormat)
            End Using
            y += 44
        End If

        If IsSectionVisible("Barcode") AndAlso String.IsNullOrWhiteSpace(PrintData.Barcode) = False Then
            DrawBarcode(g, bounds, y)
            y += 56
        End If

        If IsSectionVisible("Footer") AndAlso String.IsNullOrWhiteSpace(PrintData.Footer) = False Then
            Using footerBrush As New SolidBrush(GetProfileColor(Profile.FooterForeColorArgb))
                g.DrawString(PrintData.Footer, footerFont, footerBrush, New Rectangle(bounds.Left, y, bounds.Width, 38), centerFormat)
            End Using
            y += 40
        End If
    End Sub

    Private Sub DrawSummaryRow(g As Graphics, label As String, value As String, x As Integer, y As Integer, width As Integer, font As Font, rightFormat As StringFormat, leftFormat As StringFormat)
        Dim rect As New Rectangle(x, y, width, 24)
        Using backBrush As New SolidBrush(GetProfileColor(Profile.TotalBackColorArgb))
            g.FillRectangle(backBrush, rect)
        End Using
        DrawCellBorder(g, rect)
        Using totalBrush As New SolidBrush(GetProfileColor(Profile.TotalForeColorArgb))
            g.DrawString(label, font, totalBrush, New Rectangle(rect.Left + (width \ 2), rect.Top, width \ 2, rect.Height), rightFormat)
            g.DrawString(value, font, totalBrush, New Rectangle(rect.Left, rect.Top, width \ 2, rect.Height), leftFormat)
        End Using
    End Sub

    Private Sub DrawBarcode(g As Graphics, bounds As Rectangle, y As Integer)
        Try
            Dim writer As New BarcodeWriter()
            writer.Format = BarcodeFormat.CODE_128
            Using barcodeImage As Image = writer.Write(PrintData.Barcode)
                Dim barcodeWidth As Integer = Math.Min(220, bounds.Width)
                Dim x As Integer = bounds.Left + ((bounds.Width - barcodeWidth) \ 2)
                g.DrawImage(barcodeImage, x, y, barcodeWidth, 45)
            End Using
        Catch
            Using centerFormat As StringFormat = CreateFormat("Center")
                Using barcodeFont As Font = CreateProfileFont(Profile.FooterFontSize, FontStyle.Bold),
                      textBrush As New SolidBrush(GetProfileColor(Profile.TextForeColorArgb))
                    g.DrawString(PrintData.Barcode, barcodeFont, textBrush, New Rectangle(bounds.Left, y, bounds.Width, 24), centerFormat)
                End Using
            End Using
        End Try
    End Sub

    Private Sub DrawFooterLine(g As Graphics, bounds As Rectangle, footerFont As Font, centerFormat As StringFormat)
        Dim footerTop As Integer = bounds.Bottom - 24
        Dim footerText As String = "صفحة " & PageNumber.ToString() & "    " & PrintDateTime.ToString("yyyy/MM/dd HH:mm")
        Using footerBrush As New SolidBrush(GetProfileColor(Profile.FooterForeColorArgb))
            g.DrawString(footerText, footerFont, footerBrush, New Rectangle(bounds.Left, footerTop, bounds.Width, 22), centerFormat)
        End Using
    End Sub

    Private Function GetVisibleColumns() As List(Of SalesPrintComponent)
        Return Profile.Components.
            Where(Function(c) c.ComponentScope = "COLUMN" AndAlso c.IsVisible).
            OrderBy(Function(c) c.SortOrder).
            ToList()
    End Function

    Private Function IsSectionVisible(code As String) As Boolean
        Dim section As SalesPrintComponent = Profile.Components.FirstOrDefault(Function(c) c.ComponentScope = "SECTION" AndAlso c.ComponentCode = code)
        Return section IsNot Nothing AndAlso section.IsVisible
    End Function

    Private Function IsPosUsageProfile() As Boolean
        If Profile Is Nothing OrElse String.IsNullOrWhiteSpace(Profile.UsageKey) Then Return False
        Dim usageKey As String = Profile.UsageKey.Trim().ToUpperInvariant()
        Return usageKey = SalesPrintRepository.UsagePos OrElse usageKey = SalesPrintRepository.UsagePosOrder
    End Function

    Private Function GetRowValue(row As DataRow, columnName As String) As String
        If row Is Nothing OrElse row.Table Is Nothing Then Return ""
        If row.Table.Columns.Contains(columnName) = False Then Return ""
        If row(columnName) Is Nothing OrElse row(columnName) Is DBNull.Value Then Return ""
        Return row(columnName).ToString()
    End Function

    Private Function CreateFormat(alignment As String) As StringFormat
        Dim fmt As New StringFormat()
        fmt.LineAlignment = StringAlignment.Center
        fmt.Trimming = StringTrimming.Word

        Select Case alignment
            Case "Right"
                fmt.Alignment = StringAlignment.Far
                fmt.FormatFlags = StringFormatFlags.DirectionRightToLeft
            Case "Left"
                fmt.Alignment = StringAlignment.Near
            Case Else
                fmt.Alignment = StringAlignment.Center
        End Select

        Return fmt
    End Function

    Private Function CreateProfileFont(size As Decimal, style As FontStyle) As Font
        Dim fontSize As Single = CSng(Math.Max(5D, Math.Min(32D, size)))
        Dim family As String = If(String.IsNullOrWhiteSpace(Profile.FontFamily), "Segoe UI", Profile.FontFamily)

        Try
            Return New Font(family, fontSize, style)
        Catch
            Return New Font("Segoe UI", fontSize, style)
        End Try
    End Function

    Private Function GetProfileColor(argb As Integer) As Color
        Try
            Return Color.FromArgb(argb)
        Catch
            Return Color.Black
        End Try
    End Function

    Private Function ClampLogoDimension(value As Integer, defaultValue As Integer) As Integer
        If value <= 0 Then Return defaultValue
        If value < 20 Then Return 20
        If value > 300 Then Return 300
        Return value
    End Function

    Private Sub DrawCellBorder(g As Graphics, rect As Rectangle)
        If Profile.DrawGridLines = False Then Return

        Using borderPen As New Pen(GetProfileColor(Profile.BorderColorArgb))
            g.DrawRectangle(borderPen, rect)
        End Using
    End Sub

End Class
