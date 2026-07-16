Imports System.Drawing.Printing
Imports System.Linq

Public Class ManualFormatPrintDocumentRenderer
    Private ReadOnly PrintData As ManualFormatPrintData
    Private SectionIndex As Integer = 0
    Private RowIndex As Integer = 0
    Private PageNumber As Integer = 1

    Public Sub New(data As ManualFormatPrintData)
        PrintData = data
    End Sub

    Public Function CreatePrintDocument() As PrintDocument
        Dim doc As New PrintDocument()
        doc.DocumentName = If(PrintData Is Nothing OrElse String.IsNullOrWhiteSpace(PrintData.ReportTitle), "تقرير أمر تصنيع يدوي", PrintData.ReportTitle)
        doc.DefaultPageSettings.Landscape = True
        doc.DefaultPageSettings.Margins = New Margins(35, 35, 35, 40)
        doc.DefaultPageSettings.PaperSize = New PaperSize("A4", 827, 1169)

        AddHandler doc.BeginPrint, AddressOf PrintDocument_BeginPrint
        AddHandler doc.PrintPage, AddressOf PrintDocument_PrintPage

        Return doc
    End Function

    Private Sub PrintDocument_BeginPrint(sender As Object, e As PrintEventArgs)
        SectionIndex = 0
        RowIndex = 0
        PageNumber = 1
    End Sub

    Private Sub PrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)
        Dim bounds As Rectangle = e.MarginBounds
        Dim y As Integer = bounds.Top

        Using titleFont As New Font("Segoe UI Semibold", 13.0F, FontStyle.Bold),
              subTitleFont As New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
              infoFont As New Font("Segoe UI", 8.0F, FontStyle.Regular),
              summaryFont As New Font("Segoe UI Semibold", 8.0F, FontStyle.Bold),
              sectionFont As New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold),
              headerFont As New Font("Segoe UI", 8.0F, FontStyle.Bold),
              rowFont As New Font("Segoe UI Semibold", 7.5F, FontStyle.Bold),
              footerFont As New Font("Segoe UI", 7.0F, FontStyle.Regular),
              centerFormat As StringFormat = CreateFormat(StringAlignment.Center),
              rightFormat As StringFormat = CreateFormat(StringAlignment.Near),
              leftFormat As StringFormat = CreateFormat(StringAlignment.Far)

            DrawReportHeader(e.Graphics, bounds, y, titleFont, subTitleFont, infoFont, centerFormat, rightFormat)

            Dim pageBottom As Integer = bounds.Bottom - 26
            Dim rowHeight As Integer = 24
            Dim headerHeight As Integer = 25

            While PrintData IsNot Nothing AndAlso SectionIndex < PrintData.Sections.Count
                Dim section As ManualFormatPrintSection = PrintData.Sections(SectionIndex)

                If section.Columns.Count = 0 Then
                    SectionIndex += 1
                    RowIndex = 0
                    Continue While
                End If

                If y + 58 > pageBottom Then
                    e.HasMorePages = True
                    PageNumber += 1
                    DrawFooter(e.Graphics, bounds, footerFont, centerFormat)
                    Return
                End If

                If RowIndex = 0 Then
                    DrawSectionTitle(e.Graphics, bounds, y, section.Title, sectionFont, rightFormat)
                End If

                Dim widths As List(Of Integer) = CalculateColumnWidths(section.Columns, bounds.Width)

                If RowIndex < section.Rows.Count Then
                    DrawTableHeader(e.Graphics, bounds, y, section.Columns, widths, headerHeight, headerFont, centerFormat)
                    y += headerHeight

                    While RowIndex < section.Rows.Count
                        If y + rowHeight > pageBottom Then
                            e.HasMorePages = True
                            PageNumber += 1
                            DrawFooter(e.Graphics, bounds, footerFont, centerFormat)
                            Return
                        End If

                        DrawTableRow(e.Graphics, bounds, y, section, widths, rowHeight, rowFont, centerFormat, rightFormat, leftFormat)
                        y += rowHeight
                        RowIndex += 1
                    End While
                End If

                Dim summaryHeight As Integer = GetSectionSummaryHeight(section)
                If y + summaryHeight > pageBottom Then
                    e.HasMorePages = True
                    PageNumber += 1
                    DrawFooter(e.Graphics, bounds, footerFont, centerFormat)
                    Return
                End If

                DrawSectionSummary(e.Graphics, bounds, y, section, infoFont, summaryFont, rightFormat, centerFormat)

                SectionIndex += 1
                RowIndex = 0
            End While

            DrawFooter(e.Graphics, bounds, footerFont, centerFormat)
            e.HasMorePages = False
        End Using
    End Sub

    Private Sub DrawReportHeader(g As Graphics, bounds As Rectangle, ByRef y As Integer, titleFont As Font, subTitleFont As Font, infoFont As Font, centerFormat As StringFormat, rightFormat As StringFormat)
        If String.IsNullOrWhiteSpace(SBill_Title_1) = False Then
            g.DrawString(SBill_Title_1, titleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 27), centerFormat)
            y += 27
        End If

        If String.IsNullOrWhiteSpace(SBill_Title_2) = False Then
            g.DrawString(SBill_Title_2, infoFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 20), centerFormat)
            y += 20
        End If

        Dim reportTitle As String = If(PrintData Is Nothing, "تقرير أمر تصنيع يدوي", PrintData.ReportTitle)
        g.DrawString(reportTitle, subTitleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 24), centerFormat)
        y += 30

        Dim rightText As String = "رقم الأمر: " & PrintData.OrderNumber & "    التاريخ: " & PrintData.OrderDate & "    العنوان: " & PrintData.OrderSubject
        g.DrawString(rightText, infoFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 20), rightFormat)
        y += 20

        Dim partyText As String = "الزبون: " & PrintData.CustomerName & "    فاتورة المبيعات: " & PrintData.SalesBillNumber & "    الموظف: " & PrintData.EmployeeName & "    تسليم: " & PrintData.DeliverDate
        g.DrawString(partyText, infoFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 20), rightFormat)
        y += 24

        If String.IsNullOrWhiteSpace(PrintData.Notes) = False Then
            g.DrawString("ملاحظات: " & PrintData.Notes, infoFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 20), rightFormat)
            y += 22
        End If
    End Sub

    Private Sub DrawSectionTitle(g As Graphics, bounds As Rectangle, ByRef y As Integer, title As String, sectionFont As Font, rightFormat As StringFormat)
        Using brush As New SolidBrush(Color.FromArgb(236, 240, 245))
            g.FillRectangle(brush, New Rectangle(bounds.Left, y, bounds.Width, 24))
        End Using

        Using pen As New Pen(Color.FromArgb(190, 196, 205))
            g.DrawRectangle(pen, New Rectangle(bounds.Left, y, bounds.Width, 24))
        End Using

        g.DrawString(title, sectionFont, Brushes.Black, New Rectangle(bounds.Left + 6, y, bounds.Width - 12, 24), rightFormat)
        y += 28
    End Sub

    Private Sub DrawTableHeader(g As Graphics, bounds As Rectangle, y As Integer, columns As List(Of ManualFormatPrintColumn), widths As List(Of Integer), height As Integer, headerFont As Font, centerFormat As StringFormat)
        Dim x As Integer = bounds.Right

        For i As Integer = 0 To columns.Count - 1
            x -= widths(i)
            Dim rect As New Rectangle(x, y, widths(i), height)
            Using brush As New SolidBrush(Color.FromArgb(45, 62, 80))
                g.FillRectangle(brush, rect)
            End Using

            g.DrawRectangle(Pens.White, rect)
            g.DrawString(columns(i).HeaderText, headerFont, Brushes.White, rect, centerFormat)
        Next
    End Sub

    Private Sub DrawTableRow(g As Graphics, bounds As Rectangle, y As Integer, section As ManualFormatPrintSection, widths As List(Of Integer), height As Integer, rowFont As Font, centerFormat As StringFormat, rightFormat As StringFormat, leftFormat As StringFormat)
        Dim x As Integer = bounds.Right
        Dim rowBackColor As Color = If(RowIndex Mod 2 = 0, Color.White, Color.FromArgb(247, 249, 252))

        For i As Integer = 0 To section.Columns.Count - 1
            x -= widths(i)
            Dim rect As New Rectangle(x, y, widths(i), height)

            Using brush As New SolidBrush(rowBackColor)
                g.FillRectangle(brush, rect)
            End Using

            Using pen As New Pen(Color.FromArgb(210, 215, 222))
                g.DrawRectangle(pen, rect)
            End Using

            Dim value As String = ""
            If RowIndex < section.Rows.Count AndAlso i < section.Rows(RowIndex).Count Then value = section.Rows(RowIndex)(i)

            Dim fmt As StringFormat = centerFormat
            If section.Columns(i).Alignment = "Right" Then fmt = rightFormat
            If section.Columns(i).Alignment = "Left" Then fmt = leftFormat

            g.DrawString(value, rowFont, Brushes.Black, Rectangle.Inflate(rect, -3, -1), fmt)
        Next
    End Sub

    Private Sub DrawSectionSummary(g As Graphics, bounds As Rectangle, ByRef y As Integer, section As ManualFormatPrintSection, infoFont As Font, summaryFont As Font, rightFormat As StringFormat, centerFormat As StringFormat)
        Dim items As List(Of KeyValuePair(Of String, String)) = BuildSectionSummaryItems(section)
        If items.Count = 0 Then Return

        Using brush As New SolidBrush(Color.FromArgb(248, 250, 252))
            g.FillRectangle(brush, New Rectangle(bounds.Left, y, bounds.Width, 24))
        End Using

        Using pen As New Pen(Color.FromArgb(190, 196, 205))
            g.DrawRectangle(pen, New Rectangle(bounds.Left, y, bounds.Width, 24))
        End Using

        Dim sectionTitle As String = If(section Is Nothing, "الإجماليات", "إجماليات " & section.Title)
        g.DrawString(sectionTitle, summaryFont, Brushes.Black, New Rectangle(bounds.Left + 6, y, bounds.Width - 12, 24), rightFormat)
        y += 26

        Dim itemsPerRow As Integer = 4
        Dim itemHeight As Integer = 24
        Dim itemWidth As Integer = Math.Max(145, CInt(Math.Floor(bounds.Width / CDbl(itemsPerRow))))
        Dim labelWidth As Integer = 74

        For i As Integer = 0 To items.Count - 1
            Dim rowIndex As Integer = i \ itemsPerRow
            Dim columnIndex As Integer = i Mod itemsPerRow
            Dim x As Integer = bounds.Right - ((columnIndex + 1) * itemWidth)
            Dim itemY As Integer = y + (rowIndex * itemHeight)

            If x < bounds.Left Then x = bounds.Left

            Dim rect As New Rectangle(x, itemY, itemWidth, itemHeight)
            Dim labelRect As New Rectangle(rect.Right - labelWidth, rect.Top, labelWidth, rect.Height)
            Dim valueRect As New Rectangle(rect.Left, rect.Top, rect.Width - labelWidth, rect.Height)

            Using brush As New SolidBrush(Color.White)
                g.FillRectangle(brush, rect)
            End Using

            Using pen As New Pen(Color.FromArgb(210, 215, 222))
                g.DrawRectangle(pen, rect)
                g.DrawLine(pen, labelRect.Left, labelRect.Top, labelRect.Left, labelRect.Bottom)
            End Using

            g.DrawString(items(i).Key, infoFont, Brushes.Black, Rectangle.Inflate(labelRect, -3, -1), rightFormat)
            g.DrawString(items(i).Value, summaryFont, Brushes.Black, Rectangle.Inflate(valueRect, -3, -1), centerFormat)
        Next

        y += (CInt(Math.Ceiling(items.Count / CDbl(itemsPerRow))) * itemHeight) + 10
    End Sub

    Private Function GetSectionSummaryHeight(section As ManualFormatPrintSection) As Integer
        Dim items As List(Of KeyValuePair(Of String, String)) = BuildSectionSummaryItems(section)
        If items.Count = 0 Then Return 0

        Dim itemsPerRow As Integer = 4
        Dim itemHeight As Integer = 24
        Return 26 + (CInt(Math.Ceiling(items.Count / CDbl(itemsPerRow))) * itemHeight) + 10
    End Function

    Private Function BuildSectionSummaryItems(section As ManualFormatPrintSection) As List(Of KeyValuePair(Of String, String))
        Dim items As New List(Of KeyValuePair(Of String, String))
        If section Is Nothing Then Return items

        items.Add(New KeyValuePair(Of String, String)("عدد الصفوف", section.Rows.Count.ToString("N0")))

        For Each column As ManualFormatPrintColumn In section.Columns
            If column.HasTotal AndAlso column.TotalValueCount > 0 Then
                items.Add(New KeyValuePair(Of String, String)("إجمالي " & column.HeaderText, FormatTotalValue(column.TotalValue, column.DisplayFormat)))
            End If
        Next

        Return items
    End Function

    Private Function FormatTotalValue(value As Decimal, displayFormat As String) As String
        If String.IsNullOrWhiteSpace(displayFormat) = False Then
            Try
                Return value.ToString(displayFormat)
            Catch
            End Try
        End If

        If Decimal.Truncate(value) = value Then Return value.ToString("N0")
        Return value.ToString("N3")
    End Function

    Private Sub DrawFooter(g As Graphics, bounds As Rectangle, footerFont As Font, centerFormat As StringFormat)
        Dim footerY As Integer = bounds.Bottom - 16
        Dim footerText As String = "المستخدم: " & If(PrintData Is Nothing, USER_NAME, PrintData.UserName) & "    تاريخ الطباعة: " & DateTime.Now.ToString("yyyy/MM/dd HH:mm") & "    صفحة " & PageNumber.ToString()
        g.DrawString(footerText, footerFont, Brushes.Black, New Rectangle(bounds.Left, footerY, bounds.Width, 16), centerFormat)
    End Sub

    Private Function CalculateColumnWidths(columns As List(Of ManualFormatPrintColumn), availableWidth As Integer) As List(Of Integer)
        Dim widths As New List(Of Integer)
        If columns Is Nothing OrElse columns.Count = 0 Then Return widths

        Dim totalWeight As Integer = columns.Sum(Function(c) Math.Max(35, c.WidthValue))
        Dim usedWidth As Integer = 0

        For i As Integer = 0 To columns.Count - 1
            Dim columnWidth As Integer = CInt(Math.Floor((Math.Max(35, columns(i).WidthValue) / CDbl(totalWeight)) * availableWidth))
            columnWidth = Math.Max(35, columnWidth)

            If i = columns.Count - 1 Then columnWidth = Math.Max(35, availableWidth - usedWidth)
            widths.Add(columnWidth)
            usedWidth += columnWidth
        Next

        Return widths
    End Function

    Private Shared Function CreateFormat(alignment As StringAlignment) As StringFormat
        Dim fmt As New StringFormat()
        fmt.Alignment = alignment
        fmt.LineAlignment = StringAlignment.Center
        fmt.FormatFlags = StringFormatFlags.DirectionRightToLeft
        fmt.Trimming = StringTrimming.EllipsisCharacter
        Return fmt
    End Function
End Class
