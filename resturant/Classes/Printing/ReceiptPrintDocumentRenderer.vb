Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Drawing.Text

Public Class ReceiptPrintDocumentRenderer
    Private ReadOnly PrintData As ReceiptPrintData
    Private ReadOnly PageTypeId As Integer
    Private ReadOnly PaperKind As String

    Public Sub New(data As ReceiptPrintData, receiptPageTypeId As Integer)
        PrintData = data
        PageTypeId = receiptPageTypeId
        PaperKind = ResolvePaperKind()
    End Sub

    Public Function CreatePrintDocument() As PrintDocument
        Dim doc As New PrintDocument()
        doc.DocumentName = If(PrintData Is Nothing OrElse String.IsNullOrWhiteSpace(PrintData.DocumentTitle), "سند إيصال", PrintData.DocumentTitle)
        doc.DefaultPageSettings.Landscape = False
        doc.DefaultPageSettings.Margins = GetMargins()
        doc.DefaultPageSettings.PaperSize = GetPaperSize()

        Return doc
    End Function

    Private Function ResolvePaperKind() As String
        Dim track As String = If(Receipt_Track, "").ToUpperInvariant()

        If PageTypeId = 1 OrElse track.Contains("ROLL") OrElse track.Contains("80") Then Return "ROLL"
        If PageTypeId = 3 OrElse track.Contains("A5") Then Return "A5"

        Return "A4"
    End Function

    Private Function GetMargins() As Margins
        Select Case PaperKind
            Case "ROLL"
                Return New Margins(8, 8, 8, 8)
            Case "A5"
                Return New Margins(25, 25, 30, 25)
            Case Else
                Return New Margins(34, 34, 38, 35)
        End Select
    End Function

    Private Function GetPaperSize() As PaperSize
        Select Case PaperKind
            Case "ROLL"
                Return New PaperSize("Receipt80", 300, 620)
            Case "A5"
                Return New PaperSize("A5", 583, 827)
            Case Else
                Return New PaperSize("A4", 827, 1169)
        End Select
    End Function

    Public Sub PrintPage(sender As Object, e As PrintPageEventArgs)
        If PrintData Is Nothing Then
            e.HasMorePages = False
            Return
        End If

        e.Graphics.SmoothingMode = SmoothingMode.HighQuality
        e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit

        DrawVoucher(e.Graphics, e.MarginBounds)
        e.HasMorePages = False
    End Sub

    Private Sub DrawVoucher(g As Graphics, bounds As Rectangle)
        Dim contentWidth As Integer = bounds.Width
        Dim scale As Single = Math.Max(0.55F, Math.Min(1.15F, contentWidth / 610.0F))
        If PaperKind = "ROLL" Then scale = 0.58F

        Dim tableWidth As Integer = If(PaperKind = "ROLL", bounds.Width, Math.Min(bounds.Width, CInt(610 * scale)))
        Dim left As Integer = bounds.Left + ((bounds.Width - tableWidth) \ 2)
        Dim y As Integer = bounds.Top + CInt(4 * scale)

        Using titleFont As New Font("Segoe UI Semibold", 10.0F * scale, FontStyle.Bold),
              subTitleFont As New Font("Segoe UI", 7.8F * scale, FontStyle.Regular),
              docTitleFont As New Font("Segoe UI Semibold", 16.0F * scale, FontStyle.Bold),
              labelFont As New Font("Segoe UI Semibold", 8.8F * scale, FontStyle.Bold),
              valueFont As New Font("Segoe UI Semibold", 9.0F * scale, FontStyle.Bold),
              amountFont As New Font("Segoe UI Semibold", 17.0F * scale, FontStyle.Bold),
              signFont As New Font("Segoe UI Semibold", 8.8F * scale, FontStyle.Bold),
              footerFont As New Font("Segoe UI", 7.2F * scale, FontStyle.Regular),
              linePen As New Pen(Color.Black, 1.0F),
              centerFormat As StringFormat = CreateFormat(StringAlignment.Center),
              rightFormat As StringFormat = CreateFormat(StringAlignment.Near)

            DrawCenteredText(g, PrintData.StoreTitle, titleFont, Brushes.Black, New Rectangle(left, y, tableWidth, CInt(18 * scale)), centerFormat)
            y += CInt(18 * scale)
            DrawCenteredText(g, PrintData.StoreSubTitle, subTitleFont, Brushes.Black, New Rectangle(left, y, tableWidth, CInt(15 * scale)), centerFormat)
            y += CInt(17 * scale)
            g.DrawLine(linePen, left, y, left + tableWidth, y)
            y += CInt(11 * scale)
            DrawCenteredText(g, PrintData.DocumentTitle, docTitleFont, Brushes.Black, New Rectangle(left, y, tableWidth, CInt(32 * scale)), centerFormat)
            y += CInt(36 * scale)

            DrawTopInfoRow(g, left, tableWidth, y, CInt(27 * scale), labelFont, valueFont, centerFormat, rightFormat)
            y += CInt(33 * scale)

            DrawFullRow(g, left, tableWidth, y, CInt(39 * scale), PrintData.PartyCaption, PrintData.PartyName, labelFont, valueFont, rightFormat)
            y += CInt(47 * scale)

            DrawFullRow(g, left, tableWidth, y, CInt(49 * scale), "البيان", PrintData.StatementText, labelFont, valueFont, rightFormat)
            y += CInt(57 * scale)

            DrawFullRow(g, left, tableWidth, y, CInt(49 * scale), "المبلغ", PrintData.AmountText, labelFont, amountFont, rightFormat)
            y += CInt(57 * scale)

            DrawFullRow(g, left, tableWidth, y, CInt(49 * scale), "فقط", PrintData.AmountInWords, labelFont, valueFont, rightFormat)
            y += CInt(56 * scale)

            DrawSplitRow(g, left, tableWidth, y, CInt(28 * scale), "طريقة الدفع", PrintData.PaymentMethod, "تفاصيل", PrintData.PaymentDetails, labelFont, valueFont, centerFormat, rightFormat)
            y += CInt(33 * scale)

            Dim treasuryText As String = If(PrintData.ShowTreasury, PrintData.TreasuryName, "")
            DrawSplitRow(g, left, tableWidth, y, CInt(28 * scale), "الخزينة", treasuryText, "العملة", PrintData.CurrencyName, labelFont, valueFont, centerFormat, rightFormat)
            y += CInt(33 * scale)

            Dim balanceText As String = If(PrintData.ShowAccountBalance, PrintData.AccountBalanceText, "")
            DrawFullRow(g, left, tableWidth, y, CInt(28 * scale), "رصيد الحساب", balanceText, labelFont, valueFont, rightFormat)
            y += CInt(53 * scale)

            g.DrawLine(linePen, left, y, left + tableWidth, y)
            y += CInt(17 * scale)

            DrawSignatures(g, left, tableWidth, y, signFont, centerFormat, scale)
            y += CInt(62 * scale)

            DrawFooter(g, left, tableWidth, y, footerFont, rightFormat, scale)
        End Using
    End Sub

    Private Shared Function CreateFormat(alignment As StringAlignment) As StringFormat
        Dim fmt As New StringFormat()
        fmt.Alignment = alignment
        fmt.LineAlignment = StringAlignment.Center
        fmt.FormatFlags = StringFormatFlags.DirectionRightToLeft
        fmt.Trimming = StringTrimming.EllipsisWord
        Return fmt
    End Function

    Private Sub DrawTopInfoRow(g As Graphics, left As Integer, width As Integer, y As Integer, height As Integer, labelFont As Font, valueFont As Font, centerFormat As StringFormat, rightFormat As StringFormat)
        Dim labelWidth As Integer = GetLabelWidth(width)
        Dim right As Integer = left + width
        Dim receiptValueWidth As Integer = CInt((width - (labelWidth * 2)) * 0.45)
        Dim x As Integer = right

        x -= labelWidth
        DrawCell(g, New Rectangle(x, y, labelWidth, height), "رقم الإيصال", labelFont, True, centerFormat)

        x -= receiptValueWidth
        DrawCell(g, New Rectangle(x, y, receiptValueWidth, height), PrintData.ReceiptNumber, valueFont, False, centerFormat)

        x -= labelWidth
        DrawCell(g, New Rectangle(x, y, labelWidth, height), "التاريخ", labelFont, True, centerFormat)

        DrawCell(g, New Rectangle(left, y, x - left, height), PrintData.ReceiptDate.ToString("dd/MM/yyyy HH:mm"), valueFont, False, centerFormat)
    End Sub

    Private Sub DrawFullRow(g As Graphics, left As Integer, width As Integer, y As Integer, height As Integer, labelText As String, valueText As String, labelFont As Font, valueFont As Font, rightFormat As StringFormat)
        Dim labelWidth As Integer = GetLabelWidth(width)
        Dim labelRect As New Rectangle(left + width - labelWidth, y, labelWidth, height)
        Dim valueRect As New Rectangle(left, y, width - labelWidth, height)

        DrawCell(g, labelRect, labelText, labelFont, True, rightFormat)
        DrawCell(g, valueRect, valueText, valueFont, False, rightFormat)
    End Sub

    Private Sub DrawSplitRow(g As Graphics, left As Integer, width As Integer, y As Integer, height As Integer, rightLabel As String, rightValue As String, midLabel As String, leftValue As String, labelFont As Font, valueFont As Font, centerFormat As StringFormat, rightFormat As StringFormat)
        Dim labelWidth As Integer = GetLabelWidth(width)
        Dim right As Integer = left + width
        Dim rightValueWidth As Integer = CInt((width - (labelWidth * 2)) * 0.45)
        Dim x As Integer = right

        x -= labelWidth
        DrawCell(g, New Rectangle(x, y, labelWidth, height), rightLabel, labelFont, True, centerFormat)

        x -= rightValueWidth
        DrawCell(g, New Rectangle(x, y, rightValueWidth, height), rightValue, valueFont, False, rightFormat)

        x -= labelWidth
        DrawCell(g, New Rectangle(x, y, labelWidth, height), midLabel, labelFont, True, centerFormat)

        DrawCell(g, New Rectangle(left, y, x - left, height), leftValue, valueFont, False, rightFormat)
    End Sub

    Private Sub DrawCell(g As Graphics, rect As Rectangle, text As String, font As Font, isLabel As Boolean, fmt As StringFormat)
        Dim backColor As Color = If(isLabel, Color.FromArgb(237, 241, 245), Color.FromArgb(251, 253, 255))

        Using backBrush As New SolidBrush(backColor)
            g.FillRectangle(backBrush, rect)
        End Using

        Using borderPen As New Pen(Color.Black, 1.0F)
            g.DrawRectangle(borderPen, rect)
        End Using

        Dim textRect As Rectangle = Rectangle.Inflate(rect, -4, -1)
        g.DrawString(If(text, ""), font, Brushes.Black, textRect, fmt)
    End Sub

    Private Sub DrawCenteredText(g As Graphics, text As String, font As Font, brush As Brush, rect As Rectangle, fmt As StringFormat)
        If String.IsNullOrWhiteSpace(text) Then Return
        g.DrawString(text, font, brush, rect, fmt)
    End Sub

    Private Sub DrawSignatures(g As Graphics, left As Integer, width As Integer, y As Integer, font As Font, centerFormat As StringFormat, scale As Single)
        Dim signWidth As Integer = CInt(width * 0.35)
        Dim firstRect As New Rectangle(left + CInt(width * 0.18), y, signWidth, CInt(20 * scale))
        Dim secondRect As New Rectangle(left + CInt(width * 0.57), y, signWidth, CInt(20 * scale))

        g.DrawString("توقيع الصراف", font, Brushes.Black, firstRect, centerFormat)
        g.DrawString("توقيع المستلم", font, Brushes.Black, secondRect, centerFormat)

        firstRect.Y += CInt(31 * scale)
        secondRect.Y += CInt(31 * scale)
        g.DrawString("....................", font, Brushes.Black, firstRect, centerFormat)
        g.DrawString("....................", font, Brushes.Black, secondRect, centerFormat)
    End Sub

    Private Sub DrawFooter(g As Graphics, left As Integer, width As Integer, y As Integer, font As Font, rightFormat As StringFormat, scale As Single)
        Dim footerRect As New Rectangle(left, y, width, CInt(18 * scale))
        g.DrawString("المعد: " & PrintData.UserName, font, Brushes.Black, footerRect, rightFormat)

        footerRect.Y += CInt(17 * scale)
        g.DrawString("تاريخ الطباعة : " & DateTime.Now.ToString("HH:mm  dd/MM/yyyy"), font, Brushes.Black, footerRect, rightFormat)
    End Sub

    Private Shared Function GetLabelWidth(width As Integer) As Integer
        Return Math.Max(48, Math.Min(88, CInt(width * 0.145)))
    End Function
End Class
