Imports System.Data.SqlClient

Imports System.Drawing
Imports System.Drawing.Printing

Public Class Cash_Flow_Report
    Private WithEvents PD As New PrintDocument
    Private PPD As New PrintPreviewDialog
    Private CashFlowPrintItems As New List(Of CashFlowPrintItem)
    Private CurrentPrintItem As Integer = 0
    Private PageNumber As Integer = 1
    Private TotalPages As Integer = 1

    Private Sub Cash_Flow_Report_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        YEAR_Txt.Text = Identifiers.F_YEAR



    End Sub

    Private Sub Search_btn_Click(sender As Object, e As EventArgs) Handles Search_btn.Click
        CASH_FLOW_balance_SELECT()
    End Sub

    Private Sub Print_Btn_ButtonClick(sender As Object, e As EventArgs) Handles Print_Btn.ButtonClick
        Print_B()
    End Sub

    Private Sub إستخراجالتقريرExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إستخراجالتقريرExcelToolStripMenuItem.Click
        Print_B()
    End Sub

    Private Sub Print_B()
        Try
            PreparePrint()

            If CashFlowPrintItems.Count = 0 Then
                MessageBox.Show("لا توجد بيانات للطباعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            PPD.Document = PD
            PPD.WindowState = FormWindowState.Maximized
            PPD.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء طباعة تقرير التدفقات النقدية: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Async Sub CASH_FLOW_balance_SELECT()

        Dim C As New C

        With C.Com
            .CommandText = "[CASH_FLOW_balance_SELECT]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@CASH_FLOW_ID", 0)
            .Parameters.AddWithValue("@YEAR", YEAR_Txt.Text)
        End With

        Panel1.Visible = True
        CircularProgressControl1.Start()

        Dim resultTables As List(Of DataTable) =
        Await Task.Run(Function() LoadMultipleTablesByDataSet(C.Com, MY_Settings.SqlConStr))

        CircularProgressControl1.Stop()
        Panel1.Visible = False

        '  MessageBox.Show("عدد الجداول المسترجعة: " & resultTables.Count)

        If resultTables.Count >= 1 Then Grid1.DataSource = resultTables(0)
        If resultTables.Count >= 2 Then Grid2.DataSource = resultTables(1)
        If resultTables.Count >= 3 Then Grid3.DataSource = resultTables(2)

        TOTAL_Loop_cash()
    End Sub

    Private Sub TOTAL_Loop_cash()
        Dim grid_1_total As Decimal = 0
        For i = 0 To Grid1.Rows.Count - 1
            grid_1_total += Grid1.Rows(i).Cells("NET_CASH_FLOW_CL").Value
        Next
        TextBox1.Text = grid_1_total.ToString("N")


        Dim grid_2_total As Decimal = 0
        For i = 0 To Grid2.Rows.Count - 1
            grid_2_total += Grid2.Rows(i).Cells("NET_CASH_FLOW_CL_2").Value
        Next
        TextBox2.Text = grid_2_total.ToString("N")


        Dim grid_3_total As Decimal = 0
        For i = 0 To Grid3.Rows.Count - 1
            grid_3_total += Grid3.Rows(i).Cells("NET_CASH_FLOW_CL_3").Value
        Next
        TextBox3.Text = grid_3_total.ToString("N")


        TextBox4.Text = (grid_1_total + grid_2_total + grid_3_total).ToString("N")

    End Sub

    Private Sub PreparePrint()
        CurrentPrintItem = 0
        PageNumber = 1
        TotalPages = 1
        CashFlowPrintItems.Clear()

        PD.DefaultPageSettings.Landscape = False
        PD.DefaultPageSettings.Margins = New Margins(25, 25, 30, 35)

        BuildCashFlowPrintItems()
        TotalPages = EstimateTotalPages()
    End Sub

    Private Sub BuildCashFlowPrintItems()
        AddCashFlowSection("التدفقات النقدية من الأنشطة التشغيلية", Grid1, TextBox1.Text)
        AddCashFlowSection("التدفقات النقدية من الأنشطة الاستثمارية", Grid2, TextBox2.Text)
        AddCashFlowSection("التدفقات النقدية من الأنشطة التمويلية", Grid3, TextBox3.Text)

        If CashFlowPrintItems.Count > 0 Then
            CashFlowPrintItems.Add(New CashFlowPrintItem With {
                .ItemType = CashFlowPrintItemType.GrandTotal,
                .SectionTitle = "إجمالي التدفقات النقدية",
                .Values = New String() {"", "", "", TextBox4.Text}
            })
        End If
    End Sub

    Private Sub AddCashFlowSection(sectionTitle As String, grid As DataGridView, totalText As String)
        If grid Is Nothing OrElse grid.Rows.Count = 0 Then Exit Sub

        Dim hasRows As Boolean = False
        For Each row As DataGridViewRow In grid.Rows
            If Not row.IsNewRow Then
                hasRows = True
                Exit For
            End If
        Next

        If Not hasRows Then Exit Sub

        CashFlowPrintItems.Add(New CashFlowPrintItem With {
            .ItemType = CashFlowPrintItemType.SectionHeader,
            .SectionTitle = sectionTitle
        })

        For Each row As DataGridViewRow In grid.Rows
            If row.IsNewRow Then Continue For

            CashFlowPrintItems.Add(New CashFlowPrintItem With {
                .ItemType = CashFlowPrintItemType.Row,
                .SectionTitle = sectionTitle,
                .Values = New String() {
                    GetGridCellText(row, "ACC_NAME", "ACC_NAME_2", "Column1"),
                    GetNumberGridCellText(row, "START_B_CL", "START_B_CL_2", "Column2"),
                    GetNumberGridCellText(row, "END_B_CL", "END_B_CL_2", "Column3"),
                    GetNumberGridCellText(row, "NET_CASH_FLOW_CL", "NET_CASH_FLOW_CL_2", "NET_CASH_FLOW_CL_3")
                }
            })
        Next

        CashFlowPrintItems.Add(New CashFlowPrintItem With {
            .ItemType = CashFlowPrintItemType.SectionTotal,
            .SectionTitle = "صافي " & sectionTitle,
            .Values = New String() {"", "", "", totalText}
        })
    End Sub

    Private Sub PD_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PD.PrintPage
        Dim g = e.Graphics
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        Dim marginLeft As Integer = e.MarginBounds.Left
        Dim marginRight As Integer = e.MarginBounds.Right
        Dim y As Integer = e.MarginBounds.Top
        Dim pageWidth As Integer = e.MarginBounds.Width
        Dim footerReserve As Integer = 48

        Dim companyFontAr As New Font("Tahoma", 12, FontStyle.Bold)
        Dim companyFontEn As New Font("Tahoma", 11, FontStyle.Bold)
        Dim titleFont As New Font("Tahoma", 14, FontStyle.Bold)
        Dim subTitleFont As New Font("Tahoma", 9.5!, FontStyle.Bold)
        Dim sectionFont As New Font("Tahoma", 10.0!, FontStyle.Bold)
        Dim headerFont As New Font("Tahoma", 8.6!, FontStyle.Bold)
        Dim bodyFont As New Font("Tahoma", 8.4!, FontStyle.Regular)
        Dim totalFont As New Font("Tahoma", 8.7!, FontStyle.Bold)
        Dim footerFont As New Font("Tahoma", 8.0!, FontStyle.Regular)

        Dim sfRight As New StringFormat With {
            .Alignment = StringAlignment.Near,
            .LineAlignment = StringAlignment.Center,
            .FormatFlags = StringFormatFlags.DirectionRightToLeft
        }

        Dim sfCenter As New StringFormat With {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Center,
            .FormatFlags = StringFormatFlags.DirectionRightToLeft
        }

        Dim sfLeft As New StringFormat With {
            .Alignment = StringAlignment.Near,
            .LineAlignment = StringAlignment.Center
        }

        DrawReportHeader(g, marginLeft, marginRight, y, pageWidth, companyFontAr, companyFontEn, titleFont, subTitleFont, sfRight, sfCenter, sfLeft)
        y += HeaderHeight()

        Dim colWidths = GetPrintColumnWidths(pageWidth)

        While CurrentPrintItem < CashFlowPrintItems.Count
            Dim item As CashFlowPrintItem = CashFlowPrintItems(CurrentPrintItem)
            Dim itemHeight As Integer = GetPrintItemHeight(item)

            If item.ItemType = CashFlowPrintItemType.SectionHeader Then
                itemHeight += 28
            End If

            If y + itemHeight > e.MarginBounds.Bottom - footerReserve Then
                DrawReportFooter(g, e.MarginBounds, footerFont, sfRight, sfCenter, sfLeft)
                e.HasMorePages = True
                PageNumber += 1
                Return
            End If

            Select Case item.ItemType
                Case CashFlowPrintItemType.SectionHeader
                    DrawSectionTitle(g, marginLeft, y, pageWidth, item.SectionTitle, sectionFont, sfCenter)
                    y += 28
                    DrawTableHeader(g, marginLeft, y, colWidths, headerFont, sfCenter)
                    y += 28
                Case CashFlowPrintItemType.Row
                    DrawCashFlowRow(g, item, marginLeft, y, GetPrintItemHeight(item), colWidths, bodyFont, sfRight, sfCenter, False)
                    y += GetPrintItemHeight(item)
                Case CashFlowPrintItemType.SectionTotal
                    DrawCashFlowRow(g, item, marginLeft, y, GetPrintItemHeight(item), colWidths, totalFont, sfRight, sfCenter, True)
                    y += GetPrintItemHeight(item) + 8
                Case CashFlowPrintItemType.GrandTotal
                    y += 6
                    DrawGrandTotal(g, marginLeft, y, pageWidth, item, totalFont, sfCenter)
                    y += GetPrintItemHeight(item)
            End Select

            CurrentPrintItem += 1
        End While

        DrawReportFooter(g, e.MarginBounds, footerFont, sfRight, sfCenter, sfLeft)

        e.HasMorePages = False
        CurrentPrintItem = 0
        PageNumber = 1
    End Sub

    Private Sub DrawReportHeader(g As Graphics, marginLeft As Integer, marginRight As Integer, y As Integer, pageWidth As Integer, companyFontAr As Font, companyFontEn As Font, titleFont As Font, subTitleFont As Font, sfRight As StringFormat, sfCenter As StringFormat, sfLeft As StringFormat)
        g.DrawString(MY_Settings.SBill_Title_1, companyFontAr, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 24), sfRight)
        y += 26
        g.DrawString(MY_Settings.SBill_Title_2, companyFontEn, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 22), sfRight)
        y += 26
        g.DrawLine(Pens.Black, marginLeft, y, marginRight, y)
        y += 8

        g.DrawString("قائمة التدفقات النقدية", titleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 28), sfCenter)
        y += 30
        g.DrawString("السنة المالية: " & YEAR_Txt.Text & "     تاريخ الطباعة: " & Date.Now.ToString("dd/MM/yyyy HH:mm"), subTitleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 22), sfCenter)
        y += 24
        g.DrawString("صفحة " & PageNumber.ToString() & " من " & TotalPages.ToString(), subTitleFont, Brushes.Black, New RectangleF(marginLeft, y - 2, pageWidth, 18), sfLeft)
    End Sub

    Private Function HeaderHeight() As Integer
        Return 134
    End Function

    Private Function GetPrintColumnWidths(pageWidth As Integer) As Integer()
        Return {
            CInt(pageWidth * 0.37),
            CInt(pageWidth * 0.21),
            CInt(pageWidth * 0.21),
            pageWidth - CInt(pageWidth * 0.37) - CInt(pageWidth * 0.21) - CInt(pageWidth * 0.21)
        }
    End Function

    Private Sub DrawSectionTitle(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, title As String, sectionFont As Font, sfCenter As StringFormat)
        Dim rect As New Rectangle(x, y, pageWidth, 28)
        g.FillRectangle(New SolidBrush(Color.FromArgb(226, 239, 234)), rect)
        g.DrawRectangle(New Pen(Color.FromArgb(80, 110, 95)), rect)
        g.DrawString(title, sectionFont, New SolidBrush(Color.FromArgb(22, 78, 62)), New RectangleF(rect.X + 4, rect.Y, rect.Width - 8, rect.Height), sfCenter)
    End Sub

    Private Sub DrawTableHeader(g As Graphics, x As Integer, y As Integer, colWidths As Integer(), headerFont As Font, sfCenter As StringFormat)
        Dim headers() As String = {"الحساب", "رصيد البداية", "رصيد النهاية", "صافي التدفق"}
        Dim currentX As Integer = x + TotalColumnWidth(colWidths)

        For i As Integer = 0 To headers.Length - 1
            currentX -= colWidths(i)
            Dim rect As New Rectangle(currentX, y, colWidths(i), 28)
            g.FillRectangle(New SolidBrush(Color.FromArgb(234, 234, 234)), rect)
            g.DrawRectangle(New Pen(Color.FromArgb(95, 95, 95)), rect)
            g.DrawString(headers(i), headerFont, Brushes.Black, New RectangleF(rect.X + 4, rect.Y, rect.Width - 8, rect.Height), sfCenter)
        Next
    End Sub

    Private Sub DrawCashFlowRow(g As Graphics, item As CashFlowPrintItem, x As Integer, y As Integer, rowHeight As Integer, colWidths As Integer(), rowFont As Font, sfRight As StringFormat, sfCenter As StringFormat, isTotal As Boolean)
        Dim currentX As Integer = x + TotalColumnWidth(colWidths)
        Dim fillColor As Color = If(isTotal, Color.FromArgb(245, 247, 246), If(CurrentPrintItem Mod 2 = 1, Color.FromArgb(252, 252, 252), Color.White))

        For i As Integer = 0 To colWidths.Length - 1
            currentX -= colWidths(i)
            Dim rect As New Rectangle(currentX, y, colWidths(i), rowHeight)
            g.FillRectangle(New SolidBrush(fillColor), rect)
            g.DrawRectangle(New Pen(Color.FromArgb(165, 165, 165)), rect)

            Dim text As String = ""
            If item.Values IsNot Nothing AndAlso i < item.Values.Length Then text = item.Values(i)
            If isTotal AndAlso i = 0 Then text = item.SectionTitle

            Dim brush As Brush = Brushes.Black
            If i = 3 Then
                Dim amount As Decimal
                If Decimal.TryParse(text, amount) Then
                    If amount < 0D Then brush = Brushes.DarkRed Else brush = Brushes.DarkGreen
                ElseIf text.Contains("-") Then
                    brush = Brushes.DarkRed
                Else
                    brush = Brushes.DarkGreen
                End If
            End If

            Dim fmt As StringFormat = If(i = 0, sfRight, sfCenter)
            g.DrawString(text, rowFont, brush, New RectangleF(rect.X + 4, rect.Y + 2, rect.Width - 8, rect.Height - 4), fmt)
        Next
    End Sub

    Private Sub DrawGrandTotal(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, item As CashFlowPrintItem, totalFont As Font, sfCenter As StringFormat)
        Dim rect As New Rectangle(x, y, pageWidth, 34)
        g.FillRectangle(New SolidBrush(Color.FromArgb(215, 232, 224)), rect)
        g.DrawRectangle(New Pen(Color.FromArgb(45, 90, 70), 1.5!), rect)

        Dim totalText As String = item.Values(3)
        Dim brush As Brush = Brushes.DarkGreen
        Dim amount As Decimal
        If Decimal.TryParse(totalText, amount) AndAlso amount < 0D Then brush = Brushes.DarkRed

        g.DrawString(item.SectionTitle & " : " & totalText, totalFont, brush, New RectangleF(rect.X + 6, rect.Y, rect.Width - 12, rect.Height), sfCenter)
    End Sub

    Private Sub DrawReportFooter(g As Graphics, marginBounds As Rectangle, footerFont As Font, sfRight As StringFormat, sfCenter As StringFormat, sfLeft As StringFormat)
        Dim footerY As Integer = marginBounds.Bottom - 34
        Dim pageWidth As Integer = marginBounds.Width
        Dim boxWidth As Integer = CInt(pageWidth / 4)
        Dim values() As String = {
            "إعداد التقرير: " & USER_NAME,
            "السنة المالية: " & YEAR_Txt.Text,
            "Page " & PageNumber.ToString() & " of " & TotalPages.ToString(),
            Date.Now.ToString("dd/MM/yyyy HH:mm")
        }

        g.DrawLine(New Pen(Color.FromArgb(120, 120, 120)), marginBounds.Left, footerY - 6, marginBounds.Right, footerY - 6)

        For i As Integer = 0 To values.Length - 1
            Dim rect As New Rectangle(marginBounds.Left + (i * boxWidth), footerY, boxWidth, 26)
            Dim fmt As StringFormat = If(i = 0, sfRight, If(i = 2, sfCenter, sfLeft))
            g.DrawString(values(i), footerFont, Brushes.DimGray, New RectangleF(rect.X + 3, rect.Y, rect.Width - 6, rect.Height), fmt)
        Next
    End Sub

    Private Function EstimateTotalPages() As Integer
        Dim pageHeight As Integer = PD.DefaultPageSettings.Bounds.Height - PD.DefaultPageSettings.Margins.Top - PD.DefaultPageSettings.Margins.Bottom
        Dim usableHeight As Integer = pageHeight - HeaderHeight() - 48
        Dim y As Integer = 0
        Dim pages As Integer = 1

        For Each item As CashFlowPrintItem In CashFlowPrintItems
            Dim h As Integer = GetPrintItemHeight(item)
            If item.ItemType = CashFlowPrintItemType.SectionHeader Then h += 28

            If y + h > usableHeight Then
                pages += 1
                y = 0
            End If

            y += h
        Next

        Return Math.Max(pages, 1)
    End Function

    Private Function GetPrintItemHeight(item As CashFlowPrintItem) As Integer
        Select Case item.ItemType
            Case CashFlowPrintItemType.SectionHeader
                Return 28
            Case CashFlowPrintItemType.SectionTotal
                Return 30
            Case CashFlowPrintItemType.GrandTotal
                Return 34
            Case Else
                Return 25
        End Select
    End Function

    Private Function TotalColumnWidth(colWidths As Integer()) As Integer
        Dim total As Integer = 0

        For Each w As Integer In colWidths
            total += w
        Next

        Return total
    End Function

    Private Function GetGridCellText(row As DataGridViewRow, ParamArray columnNames() As String) As String
        For Each columnName As String In columnNames
            If row.DataGridView.Columns.Contains(columnName) Then
                Dim value = row.Cells(columnName).Value
                If value IsNot Nothing AndAlso Not IsDBNull(value) Then Return value.ToString()
            End If
        Next

        Return ""
    End Function

    Private Function GetNumberGridCellText(row As DataGridViewRow, ParamArray columnNames() As String) As String
        Dim text As String = GetGridCellText(row, columnNames)
        Dim d As Decimal

        If Decimal.TryParse(text, d) Then Return d.ToString("N3")

        Return text
    End Function

    Private Enum CashFlowPrintItemType
        SectionHeader = 0
        Row = 1
        SectionTotal = 2
        GrandTotal = 3
    End Enum

    Private Class CashFlowPrintItem
        Public Property ItemType As CashFlowPrintItemType
        Public Property SectionTitle As String
        Public Property Values As String()
    End Class


    Public Function LoadMultipleTablesByDataSet(cmd As SqlCommand, connStr As String) As List(Of DataTable)
        Dim result As New List(Of DataTable)()
        Using con As New SqlConnection(connStr)
            cmd.Connection = con
            Using da As New SqlDataAdapter(cmd)
                Dim ds As New DataSet()
                da.Fill(ds)
                For Each table As DataTable In ds.Tables
                    result.Add(table)
                Next
            End Using
        End Using
        Return result
    End Function




    'Public Function LoadMultipleTables(cmd As SqlCommand, connStr As String) As List(Of DataTable)
    '    Dim result As New List(Of DataTable)()

    '    Using con As New SqlConnection(connStr)
    '        con.Open()
    '        cmd.Connection = con

    '        Using reader As SqlDataReader = cmd.ExecuteReader()
    '            Do
    '                Dim dt As New DataTable()
    '                dt.Load(reader)
    '                result.Add(dt)
    '            Loop While Not reader.IsClosed AndAlso reader.NextResult()
    '        End Using
    '    End Using

    '    Return result
    'End Function




    'Private Async Sub CASH_FLOW_balance_SELECT()

    '    Dim C As New C

    '    With C.Com
    '        .Connection = C.Con
    '        .CommandText = "[CASH_FLOW_balance_SELECT]"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@CASH_FLOW_ID", 0)
    '        .Parameters.AddWithValue("@YEAR", YEAR_Txt.Text)
    '    End With

    '    Panel1.Visible = True
    '    CircularProgressControl1.Start()
    '    Await Task(Of DataTable).Run(Function() LoadDataTable(C.Com, MY_Settings.SqlConStr))
    '    Panel1.Visible = False
    '    CircularProgressControl1.Stop()


    'End Sub
End Class
