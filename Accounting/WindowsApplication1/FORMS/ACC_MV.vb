Imports System.Data.SqlClient
Imports System.Drawing.Printing
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class ACC_MV
    'Public ACC_NATURAL As Char
    Public ACC_Code As String
    Public ACC_Name As String
    Public COST_ID As Integer = 0
    Public COST_NAME As String = ""

    Private WithEvents PD As New PrintDocument
    Private PPD As New PrintPreviewDialog
    Private CurrentRow As Integer = 0
    Private PageNumber As Integer = 1
    Private TotalPages As Integer = 1
    Private PrintableRows As New List(Of Integer)
    Private CurrentPrintLandscape As Boolean = True

    Private Sub ACC_MV_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateRange_Flate1.FYear_Txt.Text = Identifiers.F_YEAR
        TITLE_txt.Text = " كشــــف أستــاذ \ " & vbNewLine & ACC_Name & " : " & ACC_Code 'Balances_Form.DataGridView1.CurrentRow.Cells(2).Value
        'ACC_NATURAL = Balances_Form.DataGridView1.CurrentRow.Cells("ACC_NATURAL").Value
        PreparePrintMenu()
        SELECT_Balance()
    End Sub

    Public Async Sub SELECT_Balance()

        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "[ACC_MV]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@ACC_CODE", ACC_Code)
            .Parameters.AddWithValue("@DATE_F", DateRange_Flate1.D_F.Value)
            .Parameters.AddWithValue("@DATE_T", DateRange_Flate1.D_T.Value)
            .Parameters.Add("@DEBIT", SqlDbType.Float, (18.3), "0")
            .Parameters.Add("@CREDIT", SqlDbType.Float, (18.3), "0")
            .Parameters("@DEBIT").Direction = ParameterDirection.Output
            .Parameters("@CREDIT").Direction = ParameterDirection.Output
            If COST_ID <> 0 Then .Parameters.AddWithValue("@COST_ID", COST_ID)

        End With


        CircularPanel.Visible = True
        CircularProgressControl1.Start()
        DataGridView1.DataSource = Await Task(Of DataTable).Run(Function() LoadDataTable(C.Com, MY_Settings.SqlConStr))
        '-----------------------------------------

        Total_C_txt.Text = C.Com.Parameters("@CREDIT").Value
        Total_D_txt.Text = C.Com.Parameters("@DEBIT").Value

        Rows_txt.Text = DataGridView1.Rows.Count
        DataGridView1.Columns(DataGridView1.Columns.Count - 1).Visible = False
        DataGridView1.Columns(DataGridView1.Columns.Count - 2).Visible = False
        DataGridView1.Columns("مدين").DefaultCellStyle.Format = "N3"
        DataGridView1.Columns("دائن").DefaultCellStyle.Format = "N3"
        DataGridView1.Columns("الرصيد").DefaultCellStyle.Format = "N3"
        DataGridView1.Columns("CREDIT").DefaultCellStyle.Format = "N3"
        DataGridView1.Columns("DEBIT").DefaultCellStyle.Format = "N3"
        DataGridView1.Columns("مدين").Tag = 1
        DataGridView1.Columns("دائن").Tag = 1



        Module1.TOTAL_C_N = 0
        Module1.TOTAL_D_N = 0

        ' Loop through all columns in the DataGridView
        For Each column As DataGridViewColumn In DataGridView1.Columns
            column.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        For i = 1 To DataGridView1.Rows.Count - 1

            If IsDBNull(DataGridView1.Rows(i).Cells(1).Value) Then
                Continue For
            End If
            If IsDBNull(DataGridView1.Rows(i).Cells("مدين").Value) Then
                Module1.TOTAL_D_N += 1
            ElseIf DataGridView1.Rows(i).Cells("مدين").Value = 0 Then
                If IsDBNull(DataGridView1.Rows(i).Cells("دائن").Value) Then
                    Module1.TOTAL_C_N += 1
                ElseIf DataGridView1.Rows(i).Cells("دائن").Value = 0 Then
                    Continue For
                Else
                    Module1.TOTAL_C_N += 1
                End If

            Else
                Module1.TOTAL_C_N += 1

            End If
        Next

        TOTAL_C_N.Text = Module1.TOTAL_C_N
        TOTAL_D_N.Text = Module1.TOTAL_D_N

        If DataGridView1.Rows.Count > 0 Then
            Total_B_txt.Text = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells("الرصيد").Value
        End If

        '-----------------------------------------
        CircularPanel.Visible = False
        CircularProgressControl1.Stop()



    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseDoubleClick

        If DataGridView1.CurrentRow.Cells("قيد ألي").Value = False Then

            If DataGridView1.CurrentRow.Cells("رقم القيــد").Value <> 0 Then

                '-------------------------------------------------
                F_ACC_B = New ACC_B
                F_ACC_B.UP_ToolStripBtn.Enabled = False
                F_ACC_B.DOWN_ToolStripBtn.Enabled = False
                F_ACC_B.LAST_ToolStripBtn.Enabled = False
                F_ACC_B.First_ToolStripBtn.Enabled = False
                F_ACC_B.T_ID_txt_2.Enabled = False
                F_ACC_B.Text = " عرض القيــد ( " & DataGridView1.CurrentRow.Cells("رقم القيــد").Value & " )  "
                F_ACC_B.NEW_Btn.Enabled = False

                F_ACC_B.is_Select = True
                T_ID_Search = DataGridView1.CurrentRow.Cells("رقم القيــد").Value

                F_ACC_B.Selected_ACC_CODE = ACC_Code

                F_ACC_B.ShowDialog()
                T_ID_Search = 0
                '-------------------------------------------------

            End If

        Else

            Dim F As New Auto_Balance_info
            F.T_ID = DataGridView1.CurrentRow.Cells("رقم القيــد").Value
            F.ShowDialog()

        End If


    End Sub


    Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
        Try
            If DataGridView1.Columns(e.ColumnIndex).Name = "دائن" Then
                If Not IsDBNull(e.Value) Then
                    e.CellStyle.BackColor = Color.FromArgb(255, 192, 192)
                    e.CellStyle.ForeColor = Drawing.Color.DarkRed

                End If
            End If

            If DataGridView1.Columns(e.ColumnIndex).Name = "مدين" Then
                If Not IsDBNull(e.Value) Then
                    e.CellStyle.BackColor = Color.FromArgb(192, 255, 192)
                    e.CellStyle.ForeColor = Drawing.Color.DarkGreen


                End If
            End If

            If DataGridView1.Columns(e.ColumnIndex).Name = "الرصيد" Then
                If Not IsDBNull(e.Value) Then
                    Select Case e.Value
                        Case 0
                            e.CellStyle.ForeColor = Drawing.Color.Lavender
                            e.CellStyle.ForeColor = Drawing.Color.Black

                        Case Is < 0
                            e.CellStyle.BackColor = Color.FromArgb(255, 192, 192)
                            e.CellStyle.ForeColor = Drawing.Color.DarkRed
                        Case Is > 0
                            e.CellStyle.BackColor = Color.FromArgb(192, 255, 192)
                            e.CellStyle.ForeColor = Drawing.Color.DarkGreen

                    End Select



                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Search_btn_Click(sender As Object, e As EventArgs) Handles Search_btn.Click
        SELECT_Balance()
    End Sub



    'Private Sub Print_Btn_Click(sender As Object, e As EventArgs)
    '    If DataGridView1.Rows.Count > 0 Then Print_B()
    'End Sub



    Public Sub Print_B(Optional ByVal exportToExcel As Boolean = False)
        Try
            If DataGridView1.Rows.Count = 0 Then
                MessageBox.Show("لا توجد بيانات للطباعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Not exportToExcel Then
                PreparePrint()
                PPD.Document = PD
                PPD.WindowState = FormWindowState.Maximized
                PPD.ShowDialog()
                Exit Sub
            End If

            Dim pp As New ReportConnection
            pp.rp.Load(Application.StartupPath & "\Reports\ACC_MV.rpt")
            pp.LoadTables()

            With pp
                .rp.SetParameterValue("TITLE_NUM", TITLE_txt.Text & vbNewLine & "للفترة من : " & DateRange_Flate1.D_F.Value & " إلى: " & DateRange_Flate1.D_T.Value & vbNewLine & "مركز التكلفة :" & If(COST_ID = 0, "الكل", COST_NAME))
                '.rp.SetParameterValue("DATE", Date_.Text)
                .rp.SetParameterValue("Title_1", MY_Settings.SBill_Title_1)
                .rp.SetParameterValue("Title_2", MY_Settings.SBill_Title_2)
                '.rp.SetParameterValue("Bill_ID", T_ID_txt_2.Text)
                .rp.SetParameterValue("T_CREDIT", Total_C_txt.Text)
                .rp.SetParameterValue("T_DEBIT", Total_D_txt.Text)
                .rp.SetParameterValue("TOTAL_D_N", TOTAL_D_N.Text)
                .rp.SetParameterValue("TOTAL_C_N", TOTAL_C_N.Text)
                .rp.SetParameterValue("T_ROWS", Rows_txt.Text)
                '.rp.SetParameterValue("TITLE_Bill", M_Notes_txt.Text)
                .rp.SetParameterValue("USER_Input", USER_NAME)
                '.rp.SetParameterValue("User_Depended", Depended_User_Txt.Text)
                .rp.SetParameterValue("Money_char", HANY(Total_B_txt.Text, "LYD"))
                .rp.SetParameterValue("T_BALANCE", Total_B_txt.Text)
                .rp.SetParameterValue("ACC_TYPE", ACC_TYPE_Txt.Text)
            End With

            ' **تصدير التقرير إلى Excel بدلاً من الطباعة إذا تم اختيار التصدير**
            If exportToExcel Then
                Dim saveDialog As New SaveFileDialog()
                saveDialog.Filter = "Excel Files|*.xls"
                saveDialog.Title = "حفظ التقرير كملف Excel"
                saveDialog.FileName = TITLE_txt.Text & ".xls"

                If saveDialog.ShowDialog() = DialogResult.OK Then
                    Dim exportPath As String = saveDialog.FileName
                    ExportReportToExcel(pp.rp, exportPath)
                End If
            Else
                ' **عرض التقرير للطباعة**
                Dim p As New print
                p.CrystalReportViewer1.ReportSource = pp.rp
                p.ShowDialog()
            End If
        Catch ex As Exception
            MessageBox.Show("حدث خطأ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub PreparePrint()
        CurrentRow = 0
        PageNumber = 1
        TotalPages = 1
        PrintableRows.Clear()

        PD.DefaultPageSettings.Landscape = CurrentPrintLandscape
        PD.DefaultPageSettings.Margins = New Margins(25, 25, 30, 30)

        BuildPrintableRows()
        TotalPages = EstimateTotalPages()
    End Sub

    Private Sub PreparePrintMenu()
        Dim printLandscapeItem As New ToolStripMenuItem("طباعة بالعرض")
        Dim printPortraitItem As New ToolStripMenuItem("طباعة بالطول")

        AddHandler printLandscapeItem.Click,
            Sub()
                CurrentPrintLandscape = True
                Print_B()
            End Sub

        AddHandler printPortraitItem.Click,
            Sub()
                CurrentPrintLandscape = False
                Print_B()
            End Sub

        Print_CntxtMStrip.Items.Insert(0, printPortraitItem)
        Print_CntxtMStrip.Items.Insert(0, printLandscapeItem)
        Print_CntxtMStrip.Items.Insert(2, New ToolStripSeparator())
    End Sub

    Private Sub BuildPrintableRows()
        PrintableRows.Clear()

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).IsNewRow Then Continue For
            PrintableRows.Add(i)
        Next
    End Sub

    Private Sub PD_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PD.PrintPage
        Dim g = e.Graphics
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        Dim marginLeft As Integer = e.MarginBounds.Left
        Dim marginRight As Integer = e.MarginBounds.Right
        Dim y As Integer = e.MarginBounds.Top
        Dim pageWidth As Integer = e.MarginBounds.Width

        Dim companyFontAr As New Font("Tahoma", 12, FontStyle.Bold)
        Dim companyFontEn As New Font("Tahoma", 11, FontStyle.Bold)
        Dim titleFont As New Font("Tahoma", 14, FontStyle.Bold)
        Dim subTitleFont As New Font("Tahoma", 10, FontStyle.Bold)
        Dim headerFont As New Font("Tahoma", If(CurrentPrintLandscape, 9.5!, 8.5!), FontStyle.Bold)
        Dim bodyFont As New Font("Tahoma", If(CurrentPrintLandscape, 9.0!, 8.25!), FontStyle.Regular)
        Dim totalFont As New Font("Tahoma", 9, FontStyle.Bold)

        Dim sfRight As New StringFormat With {
            .Alignment = StringAlignment.Far,
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

        g.DrawString("كشــــف أستــاذ", titleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 26), sfCenter)
        y += 28
        g.DrawString(ACC_Name & " : " & ACC_Code, subTitleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 22), sfCenter)
        y += 24
        g.DrawString("من " & DateRange_Flate1.D_F.Value.ToString("dd/MM/yyyy") & " إلى " & DateRange_Flate1.D_T.Value.ToString("dd/MM/yyyy") & "     مركز التكلفة: " & If(COST_ID = 0, "الكل", COST_NAME), subTitleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 22), sfCenter)
        y += 26
        g.DrawString("صفحة " & PageNumber.ToString() & " من " & TotalPages.ToString(), bodyFont, Brushes.Black, New RectangleF(marginLeft, y - 4, pageWidth, 18), sfLeft)
        y += 18

        Dim colWidths = GetPrintColumnWidths(pageWidth)
        DrawPrintHeader(g, marginLeft, y, colWidths, headerFont, sfCenter)
        y += 32

        While CurrentRow < PrintableRows.Count
            Dim row As DataGridViewRow = DataGridView1.Rows(PrintableRows(CurrentRow))
            Dim rowHeight As Integer = EstimateLedgerRowHeight(g, row, bodyFont, colWidths(3))

            If y + rowHeight > e.MarginBounds.Bottom - 125 Then
                e.HasMorePages = True
                PageNumber += 1
                Return
            End If

            DrawLedgerRow(g, row, marginLeft, y, rowHeight, colWidths, bodyFont, sfCenter, sfRight)
            y += rowHeight
            CurrentRow += 1
        End While

        y += 8
        DrawTotals(g, marginLeft, y, pageWidth, totalFont, sfRight, sfCenter)

        e.HasMorePages = False
        CurrentRow = 0
        PageNumber = 1
    End Sub

    Private Function GetPrintColumnWidths(pageWidth As Integer) As Integer()
        If CurrentPrintLandscape Then
            Return {
                CInt(pageWidth * 0.05),
                CInt(pageWidth * 0.09),
                CInt(pageWidth * 0.11),
                CInt(pageWidth * 0.39),
                CInt(pageWidth * 0.12),
                CInt(pageWidth * 0.12),
                CInt(pageWidth * 0.12)
            }
        End If

        Return {
            CInt(pageWidth * 0.06),
            CInt(pageWidth * 0.11),
            CInt(pageWidth * 0.13),
            CInt(pageWidth * 0.34),
            CInt(pageWidth * 0.12),
            CInt(pageWidth * 0.12),
            CInt(pageWidth * 0.12)
        }
    End Function

    Private Sub DrawPrintHeader(g As Graphics, x As Integer, y As Integer, colWidths As Integer(), headerFont As Font, sfCenter As StringFormat)
        Dim headers() As String = {"ت", "رقم القيد", "التاريخ", "البيان", "مدين", "دائن", "الرصيد"}
        Dim currentX As Integer = x + TotalColumnWidth(colWidths)

        For i As Integer = 0 To headers.Length - 1
            currentX -= colWidths(i)
            Dim rect As New Rectangle(currentX, y, colWidths(i), 32)
            g.FillRectangle(New SolidBrush(Color.FromArgb(225, 225, 225)), rect)
            g.DrawRectangle(New Pen(Color.FromArgb(80, 80, 80)), rect)
            g.DrawString(headers(i), headerFont, Brushes.Black, New RectangleF(rect.X, rect.Y, rect.Width, rect.Height), sfCenter)
        Next
    End Sub

    Private Sub DrawLedgerRow(g As Graphics, row As DataGridViewRow, x As Integer, y As Integer, rowHeight As Integer, colWidths As Integer(), bodyFont As Font, sfCenter As StringFormat, sfRight As StringFormat)
        Dim values() As String = {
            (CurrentRow + 1).ToString(),
            GetCellText(row, "رقم القيــد"),
            GetDateCellText(row, "التاريخ", "تاريخ", "DATE", "DATE_CL"),
            GetCellText(row, "البيان", "الشرح", "ملاحظة", "ملاحظات", "Notes", "Notes_CL", "MASTER_NOTES", "MASTER_NOTES_CL"),
            GetNumberCellText(row, "مدين"),
            GetNumberCellText(row, "دائن"),
            GetNumberCellText(row, "الرصيد")
        }

        Dim currentX As Integer = x + TotalColumnWidth(colWidths)

        For i As Integer = 0 To values.Length - 1
            currentX -= colWidths(i)
            Dim rect As New Rectangle(currentX, y, colWidths(i), rowHeight)
            If CurrentRow Mod 2 = 1 Then
                g.FillRectangle(New SolidBrush(Color.FromArgb(250, 250, 250)), rect)
            End If

            g.DrawRectangle(New Pen(Color.FromArgb(150, 150, 150)), rect)

            Dim useFormat As StringFormat = If(i = 3, sfRight, sfCenter)
            Dim brush As Brush = Brushes.Black

            If i = 4 Then brush = Brushes.DarkGreen
            If i = 5 Then brush = Brushes.DarkRed
            If i = 6 Then brush = GetBalanceBrush(values(i))

            g.DrawString(values(i), bodyFont, brush, New RectangleF(rect.X + 5, rect.Y + 2, rect.Width - 10, rect.Height - 4), useFormat)
        Next
    End Sub

    Private Sub DrawTotals(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, totalFont As Font, sfRight As StringFormat, sfCenter As StringFormat)
        Dim boxHeight As Integer = 30
        Dim boxWidth As Integer = CInt(pageWidth / 3)

        g.DrawLine(Pens.Black, x, y, x + pageWidth, y)
        y += 6

        DrawSummaryBoxesRow(g, x, y, pageWidth, boxWidth, boxHeight,
                            {"إجمالي المدين", "إجمالي الدائن", "الرصيد"},
                            {Total_D_txt.Text, Total_C_txt.Text, Total_B_txt.Text & " " & ACC_TYPE_Txt.Text},
                            totalFont, sfCenter)

        y += boxHeight + 4

        DrawSummaryBoxesRow(g, x, y, pageWidth, boxWidth, boxHeight,
                            {"عدد الصفوف", "المعد", "تاريخ الطباعة"},
                            {Rows_txt.Text, USER_NAME, Date.Now.ToString("dd/MM/yyyy HH:mm")},
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
        g.DrawString(title & ": " & value, totalFont, Brushes.Black, New RectangleF(rect.X + 5, rect.Y, rect.Width - 10, rect.Height), sfCenter)
    End Sub

    Private Function EstimateLedgerRowHeight(g As Graphics, row As DataGridViewRow, bodyFont As Font, notesWidth As Integer) As Integer
        Dim notes As String = GetCellText(row, "البيان", "الشرح", "ملاحظة", "ملاحظات", "Notes", "Notes_CL", "MASTER_NOTES", "MASTER_NOTES_CL")
        Dim h As Integer = CInt(g.MeasureString(notes, bodyFont, notesWidth - 10).Height) + 12
        If h < 30 Then h = 30
        Return h
    End Function

    Private Function EstimateTotalPages() As Integer
        Using bmp As New Bitmap(10, 10)
            Using g As Graphics = Graphics.FromImage(bmp)
                Dim bodyFont As New Font("Tahoma", 8.5!, FontStyle.Regular)
                Dim pageHeight As Integer
                Dim pageWidth As Integer

                If CurrentPrintLandscape Then
                    pageHeight = PD.DefaultPageSettings.Bounds.Width - PD.DefaultPageSettings.Margins.Top - PD.DefaultPageSettings.Margins.Bottom
                    pageWidth = PD.DefaultPageSettings.Bounds.Height - PD.DefaultPageSettings.Margins.Left - PD.DefaultPageSettings.Margins.Right
                Else
                    pageHeight = PD.DefaultPageSettings.Bounds.Height - PD.DefaultPageSettings.Margins.Top - PD.DefaultPageSettings.Margins.Bottom
                    pageWidth = PD.DefaultPageSettings.Bounds.Width - PD.DefaultPageSettings.Margins.Left - PD.DefaultPageSettings.Margins.Right
                End If

                Dim colWidths = GetPrintColumnWidths(pageWidth)
                Dim usableHeight As Integer = pageHeight - 235
                Dim y As Integer = 0
                Dim pages As Integer = 1

                For Each rowIndex In PrintableRows
                    Dim h As Integer = EstimateLedgerRowHeight(g, DataGridView1.Rows(rowIndex), bodyFont, colWidths(3))

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

    Private Function TotalColumnWidth(colWidths As Integer()) As Integer
        Dim total As Integer = 0

        For Each w As Integer In colWidths
            total += w
        Next

        Return total
    End Function

    Private Function GetCellText(row As DataGridViewRow, ParamArray columnNames() As String) As String
        For Each columnName As String In columnNames
            Dim columnIndex As Integer = FindColumnIndex(columnName)

            If columnIndex >= 0 AndAlso columnIndex < row.Cells.Count Then
                Dim value = row.Cells(columnIndex).Value
                If value IsNot Nothing AndAlso Not IsDBNull(value) Then Return value.ToString()
            End If
        Next

        Return ""
    End Function

    Private Function FindColumnIndex(columnName As String) As Integer
        Dim target As String = NormalizeColumnName(columnName)

        For Each col As DataGridViewColumn In DataGridView1.Columns
            Dim nameText As String = NormalizeColumnName(col.Name)
            Dim headerText As String = NormalizeColumnName(col.HeaderText)
            Dim propertyText As String = NormalizeColumnName(col.DataPropertyName)

            If String.Equals(nameText, target, StringComparison.OrdinalIgnoreCase) OrElse
               String.Equals(headerText, target, StringComparison.OrdinalIgnoreCase) OrElse
               String.Equals(propertyText, target, StringComparison.OrdinalIgnoreCase) Then
                Return col.Index
            End If
        Next

        Return -1
    End Function

    Private Function NormalizeColumnName(value As String) As String
        If value Is Nothing Then Return ""

        Return value.Replace("ـ", "").
                     Replace(" ", "").
                     Replace("_", "").
                     Replace("-", "").
                     Trim()
    End Function

    Private Function GetDateCellText(row As DataGridViewRow, ParamArray columnNames() As String) As String
        Dim text As String = GetCellText(row, columnNames)
        Dim d As Date

        If Date.TryParse(text, d) Then Return d.ToString("dd/MM/yyyy")
        Return text
    End Function

    Private Function GetNumberCellText(row As DataGridViewRow, ParamArray columnNames() As String) As String
        Dim text As String = GetCellText(row, columnNames)
        Dim d As Decimal

        If Decimal.TryParse(text, d) Then
            If d = 0D Then Return ""
            Return d.ToString("N3")
        End If

        Return text
    End Function

    Private Function GetBalanceBrush(balanceText As String) As Brush
        Dim d As Decimal

        If Decimal.TryParse(balanceText, d) Then
            If d < 0D Then Return Brushes.DarkRed
            If d > 0D Then Return Brushes.DarkGreen
        End If

        Return Brushes.Black
    End Function



    Private Sub Total_B_txt_TextChanged(sender As Object, e As EventArgs) Handles Total_B_txt.TextChanged
        If String.IsNullOrWhiteSpace(Total_B_txt.Text) Then
            ACC_TYPE_Txt.Clear()
        Else
            If Convert.ToDecimal(Total_B_txt.Text) < 0 Then
                ACC_TYPE_Txt.Text = "(دائـن)"
            Else
                ACC_TYPE_Txt.Text = "مديـن"
            End If
        End If
    End Sub


    Private Sub Print_Btn_ButtonClick(sender As Object, e As EventArgs) Handles Print_Btn.ButtonClick
        Print_B()
    End Sub

    Private Sub إستخراجالتقريرExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إستخراجالتقريرExcelToolStripMenuItem.Click
        Print_B(True)
    End Sub


End Class
