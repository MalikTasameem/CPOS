Imports System.Data.SqlClient

Public Class Daily_B_Form
    Dim DT As New DataTable
    Dim rs As New Resizer
    Private WithEvents PD As New System.Drawing.Printing.PrintDocument
    Private PPD As New PrintPreviewDialog
    Private PrintableRows As New List(Of Integer)
    Private PrintColumns As New List(Of DataGridViewColumn)
    Private PrintColumnWidths As New List(Of Integer)
    Private CurrentRow As Integer = 0
    Private PageNumber As Integer = 1
    Private TotalPages As Integer = 1
    Private CurrentPrintLandscape As Boolean = True

    Private Sub Daily_B_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        DateRange_Flate1.FYear_Txt.Text = Identifiers.F_YEAR
        B_TYPE_CM.SelectedIndex = 0
        Balanced_Cm.SelectedIndex = 1
        Depended_Cm.SelectedIndex = 0
        PreparePrintMenu()
        SELECT_Balance()
    End Sub

    Public Async Sub SELECT_Balance()
        DT = New DataTable
        'DataGridView1.DataSource = Nothing

        DataB.Dispose()
        DataB = New BindingSource
        DataGridView1.DataSource = Nothing



        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[Daily_Balance_Select]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@DATE_F", DateRange_Flate1.D_F.Value)
            .Parameters.AddWithValue("@DATE_T", DateRange_Flate1.D_T.Value)
            .Parameters.AddWithValue("@TYPE", B_TYPE_CM.SelectedIndex)

            .Parameters.AddWithValue("@Depended", Depended_Cm.SelectedIndex)
            .Parameters.AddWithValue("@Balanced", Balanced_Cm.SelectedIndex)
        End With

        CircularPanel.Visible = True
        CircularProgressControl1.Start()
        DT = Await Task(Of DataTable).Run(Function() LoadDataTable(C.Com, MY_Settings.SqlConStr))

        DataB.DataSource = DT
        DataGridView1.DataSource = DataB

        CircularPanel.Visible = False
        CircularProgressControl1.Stop()

        'C.Da = New SqlClient.SqlDataAdapter(C.Com)
        'C.Da.Fill(DT)
        'DataGridView1.DataSource = DT

        DataGridView1.Columns(DataGridView1.Columns.Count - 1).Visible = False
        DataGridView1.Columns(DataGridView1.Columns.Count - 2).Visible = False
        PopulateFilterColumns()

        If B_TYPE_CM.SelectedIndex = 0 Then
            DataGridView1.Columns("مدين").DefaultCellStyle.Format = "N3"
            DataGridView1.Columns("مدين").Tag = 1

            DataGridView1.Columns("دائن").DefaultCellStyle.Format = "N3"
            DataGridView1.Columns("دائن").Tag = 1

        Else
            DataGridView1.Columns("مدين").DefaultCellStyle.Format = "N3"
            DataGridView1.Columns("مدين").Tag = 1

            DataGridView1.Columns("دائن").DefaultCellStyle.Format = "N3"
            DataGridView1.Columns("دائن").Tag = 1
        End If


        'DataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect

    End Sub


    Private Sub DataGridView1_DataSourceChanged(sender As Object, e As EventArgs) Handles DataGridView1.DataSourceChanged

        UpdateTotalsFromCurrentView()

    End Sub


    Private Sub DataGridView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseDoubleClick

        F_ACC_B = New ACC_B
        F_ACC_B.UP_ToolStripBtn.Enabled = False
        F_ACC_B.DOWN_ToolStripBtn.Enabled = False
        F_ACC_B.T_ID_txt_2.Enabled = False
        F_ACC_B.Text = " عرض القيــد ( " & DataGridView1.CurrentRow.Cells(0).Value.ToString & " )  "
        F_ACC_B.NEW_Btn.Enabled = False

        F_ACC_B.is_Select = True
        T_ID_Search = DataGridView1.CurrentRow.Cells(0).Value
        F_ACC_B.ShowDialog()
        T_ID_Search = 0


        'Dim F_ACC_B As New ACC_B
        'T_ID_Search = DataGridView1.CurrentRow.Cells(0).Value
        'F_ACC_B.ShowDialog()
        'T_ID_Search = 0

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
        Try
            If DataGridView1.Columns(e.ColumnIndex).Name = "دائن" Then
                If Not IsDBNull(e.Value) Then
                    e.CellStyle.BackColor = Color.FromArgb(255, 192, 192)
                    e.CellStyle.ForeColor = Drawing.Color.DarkRed
                    'ElseIf e.Value = "تم اعداد التقرير النهائي" Then
                    '    e.CellStyle.BackColor = Drawing.Color.DarkGreen
                    '    e.CellStyle.ForeColor = Drawing.Color.White

                End If
            End If

            If DataGridView1.Columns(e.ColumnIndex).Name = "مدين" Then
                If Not IsDBNull(e.Value) Then
                    e.CellStyle.BackColor = Color.FromArgb(192, 255, 192)
                    e.CellStyle.ForeColor = Drawing.Color.DarkGreen
                    'ElseIf e.Value = "تم اعداد التقرير النهائي" Then
                    '    e.CellStyle.BackColor = Drawing.Color.DarkGreen
                    '    e.CellStyle.ForeColor = Drawing.Color.White

                End If
            End If


            If B_TYPE_CM.SelectedIndex = 0 Then
                If DataGridView1.Columns(e.ColumnIndex).Name = "\" Then
                    If e.Value = "القيـــد مـــوزون" Then
                        e.CellStyle.BackColor = Drawing.Color.LightGreen
                        e.CellStyle.ForeColor = Drawing.Color.Black
                    Else
                        e.CellStyle.BackColor = Drawing.Color.IndianRed
                        e.CellStyle.ForeColor = Drawing.Color.Black

                    End If
                End If

                If DataGridView1.Columns(e.ColumnIndex).Name = "\ " Then
                    If e.Value = "القيـــد مـعتمد" Then
                        e.CellStyle.BackColor = Drawing.Color.LightGreen
                        e.CellStyle.ForeColor = Drawing.Color.Black
                    Else
                        e.CellStyle.BackColor = Drawing.Color.IndianRed
                        e.CellStyle.ForeColor = Drawing.Color.Black

                    End If
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RefreshBtn_Click(sender As Object, e As EventArgs) Handles RefreshBtn.Click
        SELECT_Balance()
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        If B_TYPE_CM.SelectedIndex = 1 Then
            ' Get the current row
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            ' Specify the column index that contains the number
            Dim columnIndex As Integer = 0 ' Change this to the column index with the number

            ' Check if the cell contains a valid number
            Dim cellValue As Object = row.Cells(columnIndex).Value
            If cellValue IsNot Nothing AndAlso IsNumeric(cellValue) Then
                Dim number As Integer = Convert.ToInt32(cellValue)

                ' Apply color based on the number (e.g., even vs odd)
                If number Mod 2 = 0 Then
                    row.DefaultCellStyle.BackColor = Color.LightGray  ' Even number
                Else
                    row.DefaultCellStyle.BackColor = Color.White  ' Odd number
                End If
            End If

        End If
    End Sub

    'Private Sub All_TimeCB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    '    GroupBox1.Enabled = Not All_TimeCB.Checked
    'End Sub

    Private Sub Print_Btn_Click(sender As Object, e As EventArgs) Handles Print_Btn.Click
        If DataGridView1.Rows.Count = 0 Then
            MessageBox.Show("لا توجد بيانات للطباعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        PreparePrint()
        PPD.Document = PD
        PPD.WindowState = FormWindowState.Maximized
        PPD.ShowDialog()
    End Sub

    Private Sub Daily_B_Form_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub B_TYPE_CM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles B_TYPE_CM.SelectedIndexChanged
        If B_TYPE_CM.SelectedIndex = 0 Then
            Panel1.Enabled = True
        Else
            Panel1.Enabled = False
        End If
    End Sub


    Private Sub FilterColumn_Cm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FilterColumn_Cm.SelectedIndexChanged
        ApplyGridFilter()
    End Sub

    Private Sub Filter_Txt_TextChanged(sender As Object, e As EventArgs) Handles Filter_Txt.TextChanged
        ApplyGridFilter()
    End Sub

    Private Sub ApplyGridFilter()
        If DataB Is Nothing Then Exit Sub

        Dim filterText As String = If(Filter_Txt.Text, "").Trim()
        If String.IsNullOrWhiteSpace(filterText) OrElse FilterColumn_Cm.SelectedValue Is Nothing Then
            DataB.Filter = ""
            UpdateTotalsFromCurrentView()
            Exit Sub
        End If

        Dim columnName As String = FilterColumn_Cm.SelectedValue.ToString()
        DataB.Filter = BuildContainsFilter(columnName, filterText)
        UpdateTotalsFromCurrentView()
    End Sub

    Private Function BuildContainsFilter(columnName As String, filterText As String) As String
        Dim safeColumn As String = columnName.Replace("]", "]]")
        Dim safeText As String = filterText.Replace("'", "''").
                                        Replace("[", "[[]").
                                        Replace("%", "[%]").
                                        Replace("*", "[*]")

        Return "CONVERT([" & safeColumn & "], 'System.String') LIKE '%" & safeText & "%'"
    End Function

    Private Sub PopulateFilterColumns()
        If FilterColumn_Cm Is Nothing OrElse DataGridView1.Columns.Count = 0 Then Exit Sub

        Dim currentValue As String = ""
        If FilterColumn_Cm.SelectedValue IsNot Nothing Then currentValue = FilterColumn_Cm.SelectedValue.ToString()

        Dim dtColumns As New DataTable()
        dtColumns.Columns.Add("ColumnName", GetType(String))
        dtColumns.Columns.Add("HeaderText", GetType(String))

        For Each col As DataGridViewColumn In DataGridView1.Columns
            If col.Visible Then
                Dim columnName As String = col.DataPropertyName
                If String.IsNullOrWhiteSpace(columnName) Then columnName = col.Name

                If Not String.IsNullOrWhiteSpace(columnName) Then
                    dtColumns.Rows.Add(columnName, col.HeaderText)
                End If
            End If
        Next

        FilterColumn_Cm.DataSource = dtColumns
        FilterColumn_Cm.DisplayMember = "HeaderText"
        FilterColumn_Cm.ValueMember = "ColumnName"

        If Not String.IsNullOrWhiteSpace(currentValue) AndAlso dtColumns.Select("ColumnName = '" & currentValue.Replace("'", "''") & "'").Length > 0 Then
            FilterColumn_Cm.SelectedValue = currentValue
        ElseIf FilterColumn_Cm.Items.Count > 0 Then
            FilterColumn_Cm.SelectedIndex = 0
        End If
    End Sub

    Private Sub UpdateTotalsFromCurrentView()
        If DataB Is Nothing Then Exit Sub

        T_DEBIT = 0
        T_CREDIT = 0

        For Each item As Object In DataB.List
            Dim drv As DataRowView = TryCast(item, DataRowView)
            If drv Is Nothing Then Continue For

            T_CREDIT += GetRowNumber(drv, "مدين")
            T_DEBIT += GetRowNumber(drv, "دائن")
        Next

        Total_C_txt.Text = T_CREDIT.ToString()
        Total_D_txt.Text = T_DEBIT.ToString()
        Rows_txt.Text = DataB.Count.ToString()
        Total_B_txt.Text = (T_DEBIT - T_CREDIT).ToString()
    End Sub

    Private Function GetRowNumber(row As DataRowView, columnName As String) As Double
        If row Is Nothing OrElse Not row.Row.Table.Columns.Contains(columnName) Then Return 0
        If row(columnName) Is Nothing OrElse row(columnName) Is DBNull.Value Then Return 0

        Dim value As Double = 0
        Double.TryParse(row(columnName).ToString(), value)
        Return value
    End Function

    Private Sub PreparePrintMenu()
        PrintMenu.Items.Clear()

        Dim printLandscapeItem As New ToolStripMenuItem("طباعة بالعرض")
        Dim printPortraitItem As New ToolStripMenuItem("طباعة بالطول")

        AddHandler printLandscapeItem.Click,
            Sub()
                CurrentPrintLandscape = True
                Print_Btn.PerformClick()
            End Sub

        AddHandler printPortraitItem.Click,
            Sub()
                CurrentPrintLandscape = False
                Print_Btn.PerformClick()
            End Sub

        PrintMenu.Items.Add(printLandscapeItem)
        PrintMenu.Items.Add(printPortraitItem)
        Print_Btn.ContextMenuStrip = PrintMenu
    End Sub

    Private Sub PreparePrint()
        CurrentRow = 0
        PageNumber = 1
        TotalPages = 1
        PrintableRows.Clear()
        PrintColumns.Clear()
        PrintColumnWidths.Clear()

        PD.DefaultPageSettings.Landscape = CurrentPrintLandscape
        PD.DefaultPageSettings.Margins = New System.Drawing.Printing.Margins(25, 25, 30, 30)

        BuildPrintableRows()
        BuildPrintColumns()
        TotalPages = EstimateTotalPages()
    End Sub

    Private Sub BuildPrintableRows()
        PrintableRows.Clear()

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).IsNewRow Then Continue For
            PrintableRows.Add(i)
        Next
    End Sub

    Private Sub BuildPrintColumns()
        PrintColumns.Clear()

        For Each col As DataGridViewColumn In DataGridView1.Columns
            If col.Visible Then PrintColumns.Add(col)
        Next
    End Sub

    Private Sub PD_PrintPage(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles PD.PrintPage
        Dim g = e.Graphics
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        Dim marginLeft As Integer = e.MarginBounds.Left
        Dim marginRight As Integer = e.MarginBounds.Right
        Dim y As Integer = e.MarginBounds.Top
        Dim pageWidth As Integer = e.MarginBounds.Width

        Dim companyFontAr As New Font("Tahoma", 12, FontStyle.Bold)
        Dim companyFontEn As New Font("Tahoma", 11, FontStyle.Bold)
        Dim titleFont As New Font("Tahoma", 14, FontStyle.Bold)
        Dim subTitleFont As New Font("Tahoma", 9.5!, FontStyle.Bold)
        Dim headerFont As New Font("Tahoma", If(CurrentPrintLandscape, 8.5!, 7.5!), FontStyle.Bold)
        Dim bodyFont As New Font("Tahoma", If(CurrentPrintLandscape, 8.0!, 7.0!), FontStyle.Regular)
        Dim totalFont As New Font("Tahoma", 8.5!, FontStyle.Bold)

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

        g.DrawString(MY_Settings.SBill_Title_1, companyFontAr, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 24), sfRight)
        y += 26
        g.DrawString(MY_Settings.SBill_Title_2, companyFontEn, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 22), sfRight)
        y += 26
        g.DrawLine(Pens.Black, marginLeft, y, marginRight, y)
        y += 8

        g.DrawString(TITLE_txt.Text, titleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 26), sfCenter)
        y += 28
        g.DrawString("نوع العرض: " & B_TYPE_CM.Text & "     الاعتماد: " & Depended_Cm.Text & "     التوازن: " & Balanced_Cm.Text, subTitleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 20), sfCenter)
        y += 22
        g.DrawString("من " & DateRange_Flate1.D_F.Value.ToString("dd/MM/yyyy") & " إلى " & DateRange_Flate1.D_T.Value.ToString("dd/MM/yyyy"), subTitleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 20), sfCenter)
        y += 24
        g.DrawString("صفحة " & PageNumber.ToString() & " من " & TotalPages.ToString(), bodyFont, Brushes.Black, New RectangleF(marginLeft, y - 4, pageWidth, 18), sfLeft)
        y += 18

        CalculatePrintColumnWidths(pageWidth)
        DrawPrintHeader(g, marginLeft, y, headerFont, sfCenter)
        y += 32

        While CurrentRow < PrintableRows.Count
            Dim row As DataGridViewRow = DataGridView1.Rows(PrintableRows(CurrentRow))
            Dim rowHeight As Integer = EstimatePrintRowHeight(g, row, bodyFont)

            If y + rowHeight > e.MarginBounds.Bottom - 92 Then
                e.HasMorePages = True
                PageNumber += 1
                Return
            End If

            DrawPrintRow(g, row, marginLeft, y, rowHeight, bodyFont, sfCenter, sfRight)
            y += rowHeight
            CurrentRow += 1
        End While

        y += 8
        DrawTotals(g, marginLeft, y, pageWidth, totalFont, sfCenter)

        e.HasMorePages = False
        CurrentRow = 0
        PageNumber = 1
    End Sub

    Private Sub CalculatePrintColumnWidths(pageWidth As Integer)
        PrintColumnWidths.Clear()

        Dim totalGridWidth As Integer = 0
        For Each col As DataGridViewColumn In PrintColumns
            totalGridWidth += Math.Max(col.Width, 40)
        Next

        If totalGridWidth <= 0 Then Exit Sub

        For Each col As DataGridViewColumn In PrintColumns
            Dim width As Integer = CInt((Math.Max(col.Width, 40) / totalGridWidth) * pageWidth)
            If width < 45 Then width = 45
            PrintColumnWidths.Add(width)
        Next

        Dim diff As Integer = pageWidth - TotalColumnWidth(PrintColumnWidths)
        If PrintColumnWidths.Count > 0 Then PrintColumnWidths(PrintColumnWidths.Count - 1) += diff
    End Sub

    Private Sub DrawPrintHeader(g As Graphics, x As Integer, y As Integer, headerFont As Font, sfCenter As StringFormat)
        Dim currentX As Integer = x + TotalColumnWidth(PrintColumnWidths)

        For i As Integer = 0 To PrintColumns.Count - 1
            currentX -= PrintColumnWidths(i)
            Dim rect As New Rectangle(currentX, y, PrintColumnWidths(i), 32)
            g.FillRectangle(New SolidBrush(Color.FromArgb(225, 225, 225)), rect)
            g.DrawRectangle(New Pen(Color.FromArgb(80, 80, 80)), rect)
            g.DrawString(PrintColumns(i).HeaderText, headerFont, Brushes.Black, New RectangleF(rect.X, rect.Y, rect.Width, rect.Height), sfCenter)
        Next
    End Sub

    Private Sub DrawPrintRow(g As Graphics, row As DataGridViewRow, x As Integer, y As Integer, rowHeight As Integer, bodyFont As Font, sfCenter As StringFormat, sfRight As StringFormat)
        Dim currentX As Integer = x + TotalColumnWidth(PrintColumnWidths)

        For i As Integer = 0 To PrintColumns.Count - 1
            currentX -= PrintColumnWidths(i)
            Dim rect As New Rectangle(currentX, y, PrintColumnWidths(i), rowHeight)
            If CurrentRow Mod 2 = 1 Then
                g.FillRectangle(New SolidBrush(Color.FromArgb(250, 250, 250)), rect)
            End If

            g.DrawRectangle(New Pen(Color.FromArgb(150, 150, 150)), rect)

            Dim text As String = GetGridCellText(row, PrintColumns(i).Name)
            Dim useFormat As StringFormat = If(IsNumberColumn(PrintColumns(i)), sfCenter, sfRight)
            Dim brush As Brush = Brushes.Black
            If PrintColumns(i).Name = "مدين" Then brush = Brushes.DarkGreen
            If PrintColumns(i).Name = "دائن" Then brush = Brushes.DarkRed

            g.DrawString(text, bodyFont, brush, New RectangleF(rect.X + 4, rect.Y + 2, rect.Width - 8, rect.Height - 4), useFormat)
        Next
    End Sub

    Private Sub DrawTotals(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, totalFont As Font, sfCenter As StringFormat)
        Dim boxHeight As Integer = 28
        Dim boxWidth As Integer = CInt(pageWidth / 4)

        g.DrawLine(Pens.Black, x, y, x + pageWidth, y)
        y += 6

        DrawSummaryBoxesRow(g, x, y, pageWidth, boxWidth, boxHeight,
                            {"إجمالي المدين", "إجمالي الدائن", "الرصيد", "عدد الصفوف"},
                            {Total_C_txt.Text, Total_D_txt.Text, Total_B_txt.Text, Rows_txt.Text},
                            totalFont, sfCenter)

        y += boxHeight + 4

        DrawSummaryBoxesRow(g, x, y, pageWidth, boxWidth, boxHeight,
                            {"المعد", "تاريخ الطباعة", "نوع العرض", "الفترة"},
                            {USER_NAME, Date.Now.ToString("dd/MM/yyyy HH:mm"), B_TYPE_CM.Text, DateRange_Flate1.D_F.Value.ToString("dd/MM/yyyy") & " - " & DateRange_Flate1.D_T.Value.ToString("dd/MM/yyyy")},
                            totalFont, sfCenter)
    End Sub

    Private Sub DrawSummaryBoxesRow(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, boxWidth As Integer, boxHeight As Integer, titles() As String, values() As String, totalFont As Font, sfCenter As StringFormat)
        Dim currentX As Integer = x + pageWidth

        For i As Integer = 0 To titles.Length - 1
            currentX -= boxWidth
            Dim rect As New Rectangle(currentX, y, boxWidth, boxHeight)
            DrawSummaryBox(g, rect, titles(i), values(i), totalFont, sfCenter)
        Next
    End Sub

    Private Sub DrawSummaryBox(g As Graphics, rect As Rectangle, title As String, value As String, totalFont As Font, sfCenter As StringFormat)
        g.FillRectangle(New SolidBrush(Color.FromArgb(245, 245, 245)), rect)
        g.DrawRectangle(Pens.Black, rect)
        g.DrawString(title & ": " & value, totalFont, Brushes.Black, New RectangleF(rect.X + 4, rect.Y, rect.Width - 8, rect.Height), sfCenter)
    End Sub

    Private Function EstimatePrintRowHeight(g As Graphics, row As DataGridViewRow, bodyFont As Font) As Integer
        Dim maxHeight As Integer = 28

        For i As Integer = 0 To PrintColumns.Count - 1
            Dim text As String = GetGridCellText(row, PrintColumns(i).Name)
            Dim h As Integer = CInt(g.MeasureString(text, bodyFont, PrintColumnWidths(i) - 8).Height) + 10
            If h > maxHeight Then maxHeight = h
        Next

        Return maxHeight
    End Function

    Private Function EstimateTotalPages() As Integer
        Using bmp As New Bitmap(10, 10)
            Using g As Graphics = Graphics.FromImage(bmp)
                Dim bodyFont As New Font("Tahoma", 8.0!, FontStyle.Regular)
                Dim pageHeight As Integer
                Dim pageWidth As Integer

                If CurrentPrintLandscape Then
                    pageHeight = PD.DefaultPageSettings.Bounds.Width - PD.DefaultPageSettings.Margins.Top - PD.DefaultPageSettings.Margins.Bottom
                    pageWidth = PD.DefaultPageSettings.Bounds.Height - PD.DefaultPageSettings.Margins.Left - PD.DefaultPageSettings.Margins.Right
                Else
                    pageHeight = PD.DefaultPageSettings.Bounds.Height - PD.DefaultPageSettings.Margins.Top - PD.DefaultPageSettings.Margins.Bottom
                    pageWidth = PD.DefaultPageSettings.Bounds.Width - PD.DefaultPageSettings.Margins.Left - PD.DefaultPageSettings.Margins.Right
                End If

                CalculatePrintColumnWidths(pageWidth)
                Dim usableHeight As Integer = pageHeight - 230
                Dim y As Integer = 0
                Dim pages As Integer = 1

                For Each rowIndex In PrintableRows
                    Dim h As Integer = EstimatePrintRowHeight(g, DataGridView1.Rows(rowIndex), bodyFont)

                    If y + h > usableHeight Then
                        pages += 1
                        y = 0
                    End If

                    y += h
                Next

                Return pages
            End Using
        End Using
    End Function

    Private Function TotalColumnWidth(widths As List(Of Integer)) As Integer
        Dim total As Integer = 0

        For Each w As Integer In widths
            total += w
        Next

        Return total
    End Function

    Private Function GetGridCellText(row As DataGridViewRow, columnName As String) As String
        If row Is Nothing OrElse Not DataGridView1.Columns.Contains(columnName) Then Return ""

        Dim value = row.Cells(columnName).Value
        If value Is Nothing OrElse value Is DBNull.Value Then Return ""

        Dim d As Decimal
        If Decimal.TryParse(value.ToString(), d) AndAlso IsNumberColumn(DataGridView1.Columns(columnName)) Then
            If d = 0D Then Return ""
            Return d.ToString("N3")
        End If

        Return value.ToString()
    End Function

    Private Function IsNumberColumn(col As DataGridViewColumn) As Boolean
        If col Is Nothing Then Return False
        If col.Tag IsNot Nothing AndAlso col.Tag.ToString() = "1" Then Return True
        Return col.Name = "مدين" OrElse col.Name = "دائن"
    End Function

End Class
