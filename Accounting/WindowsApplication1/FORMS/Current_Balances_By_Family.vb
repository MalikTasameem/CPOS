Public Class Current_Balances_By_Family


    Dim DT As New DataTable
    Private WithEvents PD As New System.Drawing.Printing.PrintDocument
    Private PPD As New PrintPreviewDialog
    Private PrintableRows As New List(Of Integer)
    Private CurrentRow As Integer = 0
    Private PageNumber As Integer = 1
    Private TotalPages As Integer = 1
    Private CurrentPrintLandscape As Boolean = True
    Dim ACC_CODE_DT As New DataTable
    Private Sub BALANCES_REVIEW_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ACC_CODE_DT.Clear()
        ACC_CODE_DT = Accounts_Datatable

        DateRange_Flate1.FYear_Txt.Text = Identifiers.F_YEAR
        ' ACC_LEVEL_txt.SelectedItem = "1"
        DateRange_Flate1.ALLTime_CheckBox.Checked = True
            PreparePrintMenu()
            Refresh_form()
            Make_Hints()
        End Sub

    Private Sub Refresh_form()
        SELECT_Balance()
    End Sub

    Private Sub Make_Hints()
            SendMessage(Search_By_Acc_Name_txt.Handle, &H1501, 0, "إبحث عن إسم حســـاب")
            SendMessage(Search_By_Acc_Code_txt.Handle, &H1501, 0, "إبحث عن رقـــم حســاب")
        End Sub


    Private Sub Search_By_Acc_Name_txt_TextChanged(sender As Object, e As EventArgs) Handles Search_By_Acc_Name_txt.TextChanged
            Dim Dv As DataView
            Dv = DT.AsDataView
            Dv.RowFilter = IM_Serach(Search_By_Acc_Name_txt.Text, "[إسم الحساب]")
            DataGridView1.DataSource = Dv
        End Sub

        Private Sub Search_By_Acc_Code_txt_TextChanged(sender As Object, e As EventArgs) Handles Search_By_Acc_Code_txt.TextChanged
            Dim Dv As DataView
            Dv = DT.AsDataView
            Dv.RowFilter = IM_Serach(Search_By_Acc_Code_txt.Text, "[رقم الحساب]")
            DataGridView1.DataSource = Dv
        End Sub
        Public Async Sub SELECT_Balance()
            DT = New DataTable

        If ACC_INFO1.ACC_CODE_TXT.Text IsNot Nothing Then

            Dim C As New C

            With C.Com
                .Connection = C.Con
                .CommandText = "[Current_Balances_By_Family_SELECT]"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@Hide_Zeros", Hide_Zeros_CB.Checked)
                .Parameters.AddWithValue("@ACC_CODE", ACC_INFO1.ACC_CODE_TXT.Text)

                .Parameters.AddWithValue("@DATE_F", DateRange_Flate1.D_F.Value)
                .Parameters.AddWithValue("@DATE_T", DateRange_Flate1.D_T.Value)

                If Cost_Center_Control1.COST_CM.SelectedIndex > -1 Then .Parameters.AddWithValue("@COST_ID", Cost_Center_Control1.COST_CM.SelectedValue)

            End With

            CircularPanel.Visible = True
            CircularProgressControl1.Start()
            DT = Await Task(Of DataTable).Run(Function() LoadDataTable(C.Com, MY_Settings.SqlConStr))
            DataGridView1.DataSource = DT

            If DataGridView1.Rows.Count > 0 Then

                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(DataGridView1.Columns.Count - 1).Visible = False
                DataGridView1.Columns(DataGridView1.Columns.Count - 2).Visible = False
                DataGridView1.Columns(DataGridView1.Columns.Count - 3).DefaultCellStyle.Format = "N3"
                DataGridView1.Columns(DataGridView1.Columns.Count - 4).DefaultCellStyle.Format = "N3"
                DataGridView1.Columns(DataGridView1.Columns.Count - 5).DefaultCellStyle.Format = "N3"
                DataGridView1.Columns(DataGridView1.Columns.Count - 6).DefaultCellStyle.Format = "N3"
                DataGridView1.Columns(DataGridView1.Columns.Count - 2).Tag = 1
                DataGridView1.Columns(DataGridView1.Columns.Count - 3).Tag = 1
                DataGridView1.Columns(DataGridView1.Columns.Count - 4).Tag = 1
                DataGridView1.Columns(DataGridView1.Columns.Count - 5).Tag = 1

            End If
            CircularPanel.Visible = False
            CircularProgressControl1.Stop()

        End If


    End Sub

        Private Sub DataGridView1_DataSourceChanged(sender As Object, e As EventArgs) Handles DataGridView1.DataSourceChanged

        Compute_Balance(DT)
        Total_C_txt.Text = T_CREDIT
            Total_D_txt.Text = T_DEBIT

            Total_B_D_txt.Text = T_BALANCE_D.ToString()
        Total_B_C_txt.Text = T_BALANCE_C.ToString()
        Rows_txt.Text = DT.Rows.Count

        Dif_TXT.Text = T_BALANCE_D - T_BALANCE_C

    End Sub


        Dim T_BALANCE_D = 0
        Dim T_BALANCE_C = 0

        Public Sub Compute_Balance(DT As DataTable)
            Dim rows As Integer = 0
            T_DEBIT = 0
            T_CREDIT = 0

            T_BALANCE_D = 0
            T_BALANCE_C = 0
            Try

                Do Until rows = DT.Rows.Count

                    If (Not IsDBNull(DT(rows)("مدين - المجاميع"))) Then
                        Dim Tax_Withheld As Double = DT(rows)("مدين - المجاميع")
                        T_CREDIT += Tax_Withheld
                    End If

                    If (Not IsDBNull(DT(rows)("دائــن - المجاميع"))) Then
                        Dim Tax_Withheld As Double = DT(rows)("دائــن - المجاميع")
                        T_DEBIT += Tax_Withheld
                    End If


                    If (Not IsDBNull(DT(rows)("مديـن - الأرصــدة"))) Then
                        Dim Tax_Withheld As Double = DT(rows)("مديـن - الأرصــدة")
                    T_BALANCE_D += Tax_Withheld
                End If

                    If (Not IsDBNull(DT(rows)("دائـن - الأرصــدة"))) Then
                        Dim Tax_Withheld As Double = DT(rows)("دائـن - الأرصــدة")
                    T_BALANCE_C += Tax_Withheld
                End If


                    rows = rows + 1
                Loop
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try


        End Sub
        Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
            Try
                If DataGridView1.Columns(e.ColumnIndex).Name = "دائـن - الأرصــدة" Or DataGridView1.Columns(e.ColumnIndex).Name = "دائــن - المجاميع" Then
                    If Not IsDBNull(e.Value) Then

                        Select Case e.Value
                            Case 0
                                e.CellStyle.ForeColor = Drawing.Color.Lavender
                                e.CellStyle.ForeColor = Drawing.Color.Black
                            Case Else
                                e.CellStyle.BackColor = Color.FromArgb(255, 192, 192)
                                e.CellStyle.ForeColor = Drawing.Color.DarkRed
                        End Select

                    End If
                End If

                If DataGridView1.Columns(e.ColumnIndex).Name = "مديـن - الأرصــدة" Or DataGridView1.Columns(e.ColumnIndex).Name = "مدين - المجاميع" Then
                    If Not IsDBNull(e.Value) Then

                        Select Case e.Value

                            Case 0
                                e.CellStyle.ForeColor = Drawing.Color.Lavender
                                e.CellStyle.ForeColor = Drawing.Color.Black

                            Case Else
                                e.CellStyle.BackColor = Color.FromArgb(192, 255, 192)
                            e.CellStyle.ForeColor = Drawing.Color.DarkGreen

                    End Select
                    End If
                End If



        Catch ex As Exception

            End Try
        End Sub

        Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
            Me.Close()
        End Sub

    Private Sub RefreshBtn_Click(sender As Object, e As EventArgs) Handles RefreshBtn.Click
        Refresh_form()
    End Sub



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
            pp.rp.Load(Application.StartupPath & "\Reports\Current_Balances_By_Family.rpt")
            pp.LoadTables()

            With pp
                .rp.SetParameterValue("TITLE_NUM", TITLE_txt.Text & " \ " & ACC_INFO1.ACC_CODE_Cm.Text & vbNewLine & "للفترة من : " & DateRange_Flate1.D_F.Value & " إلى: " & DateRange_Flate1.D_T.Value & vbNewLine & "مركز التكلفة :" & If(Cost_Center_Control1.COST_CM.SelectedIndex = -1, "الكل", Cost_Center_Control1.COST_CM.Text))
                .rp.SetParameterValue("Title_1", MY_Settings.SBill_Title_1)
                .rp.SetParameterValue("Title_2", MY_Settings.SBill_Title_2)
                .rp.SetParameterValue("Total_C_txt", Total_C_txt.Text)
                .rp.SetParameterValue("Total_D_txt", Total_D_txt.Text)
                .rp.SetParameterValue("Total_B_D_txt", Total_B_D_txt.Text)
                .rp.SetParameterValue("Total_B_C_txt", Total_B_C_txt.Text)
                .rp.SetParameterValue("T_ROWS", Rows_txt.Text)
                .rp.SetParameterValue("USER_Input", USER_NAME)

                .rp.SetParameterValue("ACC_CODE", ACC_INFO1.ACC_CODE_TXT.Text)
                .rp.SetParameterValue("DATE_F", DateRange_Flate1.D_F.Value)
                .rp.SetParameterValue("DATE_T", DateRange_Flate1.D_T.Value)
                .rp.SetParameterValue("Hide_Zeros", Hide_Zeros_CB.Checked)
                If String.IsNullOrWhiteSpace(Dif_TXT.Text) Then Dif_TXT.Text = "0"
                .rp.SetParameterValue("Dif_TXT", Dif_TXT.Text)

            End With

            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "Excel Files|*.xls"
            saveDialog.Title = "حفظ التقرير كملف Excel"
            saveDialog.FileName = TITLE_txt.Text & ".xls"

            If saveDialog.ShowDialog() = DialogResult.OK Then
                Dim exportPath As String = saveDialog.FileName
                ExportReportToExcel(pp.rp, exportPath)
            End If
        Catch ex As Exception
            MessageBox.Show("حدث خطأ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PreparePrintMenu()
        If Print_CntxtMStrip.Items.ContainsKey("PrintLandscapeMenuItem") Then Exit Sub

        Dim printLandscapeItem As New ToolStripMenuItem("طباعة بالعرض")
        printLandscapeItem.Name = "PrintLandscapeMenuItem"

        Dim printPortraitItem As New ToolStripMenuItem("طباعة بالطول")
        printPortraitItem.Name = "PrintPortraitMenuItem"

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

    Private Sub PreparePrint()
        CurrentRow = 0
        PageNumber = 1
        TotalPages = 1
        PrintableRows.Clear()

        PD.DefaultPageSettings.Landscape = CurrentPrintLandscape
        PD.DefaultPageSettings.Margins = New System.Drawing.Printing.Margins(25, 25, 30, 30)

        BuildPrintableRows()
        TotalPages = EstimateTotalPages()
    End Sub

    Private Sub BuildPrintableRows()
        PrintableRows.Clear()

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).IsNewRow Then Continue For
            PrintableRows.Add(i)
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
        Dim headerFont As New Font("Tahoma", If(CurrentPrintLandscape, 9.0!, 8.0!), FontStyle.Bold)
        Dim bodyFont As New Font("Tahoma", If(CurrentPrintLandscape, 8.5!, 7.5!), FontStyle.Regular)
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

        Dim costName As String = If(Cost_Center_Control1.COST_CM.SelectedIndex = -1, "الكل", Cost_Center_Control1.COST_CM.Text)
        Dim familyText As String = ACC_INFO1.ACC_CODE_Cm.Text
        If String.IsNullOrWhiteSpace(familyText) Then familyText = ACC_INFO1.ACC_CODE_TXT.Text

        g.DrawString(MY_Settings.SBill_Title_1, companyFontAr, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 24), sfRight)
        y += 26
        g.DrawString(MY_Settings.SBill_Title_2, companyFontEn, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 22), sfRight)
        y += 26
        g.DrawLine(Pens.Black, marginLeft, y, marginRight, y)
        y += 8

        g.DrawString(TITLE_txt.Text, titleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 26), sfCenter)
        y += 28
        g.DrawString("الحساب: " & familyText & "     مركز التكلفة: " & costName, subTitleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 20), sfCenter)
        y += 22
        g.DrawString("من " & DateRange_Flate1.D_F.Value.ToString("dd/MM/yyyy") & " إلى " & DateRange_Flate1.D_T.Value.ToString("dd/MM/yyyy"), subTitleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 20), sfCenter)
        y += 24
        g.DrawString("صفحة " & PageNumber.ToString() & " من " & TotalPages.ToString(), bodyFont, Brushes.Black, New RectangleF(marginLeft, y - 4, pageWidth, 18), sfLeft)
        y += 18

        Dim colWidths = GetPrintColumnWidths(pageWidth)
        DrawPrintHeader(g, marginLeft, y, colWidths, headerFont, sfCenter)
        y += 32

        While CurrentRow < PrintableRows.Count
            Dim row As DataGridViewRow = DataGridView1.Rows(PrintableRows(CurrentRow))
            Dim rowHeight As Integer = EstimatePrintRowHeight(g, row, bodyFont, colWidths(1))

            If y + rowHeight > e.MarginBounds.Bottom - 110 Then
                e.HasMorePages = True
                PageNumber += 1
                Return
            End If

            DrawPrintRow(g, row, marginLeft, y, rowHeight, colWidths, bodyFont, sfCenter, sfRight)
            y += rowHeight
            CurrentRow += 1
        End While

        y += 8
        DrawTotals(g, marginLeft, y, pageWidth, totalFont, sfCenter, costName)

        e.HasMorePages = False
        CurrentRow = 0
        PageNumber = 1
    End Sub

    Private Function GetPrintColumnWidths(pageWidth As Integer) As Integer()
        Return {
            CInt(pageWidth * 0.13),
            CInt(pageWidth * 0.31),
            CInt(pageWidth * 0.13),
            CInt(pageWidth * 0.13),
            CInt(pageWidth * 0.15),
            CInt(pageWidth * 0.15)
        }
    End Function

    Private Sub DrawPrintHeader(g As Graphics, x As Integer, y As Integer, colWidths As Integer(), headerFont As Font, sfCenter As StringFormat)
        Dim headers() As String = {"رقم الحساب", "إسم الحساب", "مدين - المجاميع", "دائن - المجاميع", "مدين - الأرصدة", "دائن - الأرصدة"}
        Dim currentX As Integer = x + TotalColumnWidth(colWidths)

        For i As Integer = 0 To headers.Length - 1
            currentX -= colWidths(i)
            Dim rect As New Rectangle(currentX, y, colWidths(i), 32)
            g.FillRectangle(New SolidBrush(Color.FromArgb(225, 225, 225)), rect)
            g.DrawRectangle(New Pen(Color.FromArgb(80, 80, 80)), rect)
            g.DrawString(headers(i), headerFont, Brushes.Black, New RectangleF(rect.X, rect.Y, rect.Width, rect.Height), sfCenter)
        Next
    End Sub

    Private Sub DrawPrintRow(g As Graphics, row As DataGridViewRow, x As Integer, y As Integer, rowHeight As Integer, colWidths As Integer(), bodyFont As Font, sfCenter As StringFormat, sfRight As StringFormat)
        Dim values() As String = {
            GetCellText(row, "رقم الحساب", "ACC_CODE"),
            GetCellText(row, "إسم الحساب", "ACC_NAME"),
            GetNumberCellText(row, "مدين - المجاميع"),
            GetNumberCellText(row, "دائــن - المجاميع", "دائن - المجاميع"),
            GetNumberCellText(row, "مديـن - الأرصــدة", "مدين - الأرصدة"),
            GetNumberCellText(row, "دائـن - الأرصــدة", "دائن - الأرصدة")
        }

        Dim currentX As Integer = x + TotalColumnWidth(colWidths)

        For i As Integer = 0 To values.Length - 1
            currentX -= colWidths(i)
            Dim rect As New Rectangle(currentX, y, colWidths(i), rowHeight)
            If CurrentRow Mod 2 = 1 Then
                g.FillRectangle(New SolidBrush(Color.FromArgb(250, 250, 250)), rect)
            End If

            g.DrawRectangle(New Pen(Color.FromArgb(150, 150, 150)), rect)

            Dim useFormat As StringFormat = If(i = 1, sfRight, sfCenter)
            Dim brush As Brush = Brushes.Black
            If i = 2 OrElse i = 4 Then brush = Brushes.DarkGreen
            If i = 3 OrElse i = 5 Then brush = Brushes.DarkRed

            g.DrawString(values(i), bodyFont, brush, New RectangleF(rect.X + 4, rect.Y + 2, rect.Width - 8, rect.Height - 4), useFormat)
        Next
    End Sub

    Private Sub DrawTotals(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, totalFont As Font, sfCenter As StringFormat, costName As String)
        Dim boxHeight As Integer = 28
        Dim boxWidth As Integer = CInt(pageWidth / 3)

        g.DrawLine(Pens.Black, x, y, x + pageWidth, y)
        y += 6

        DrawSummaryBoxesRow(g, x, y, pageWidth, boxWidth, boxHeight,
                            {"إجمالي مدين المجاميع", "إجمالي دائن المجاميع", "عدد الصفوف"},
                            {Total_C_txt.Text, Total_D_txt.Text, Rows_txt.Text},
                            totalFont, sfCenter)

        y += boxHeight + 4

        DrawSummaryBoxesRow(g, x, y, pageWidth, boxWidth, boxHeight,
                            {"إجمالي مدين الأرصدة", "إجمالي دائن الأرصدة", "الفرق"},
                            {Total_B_D_txt.Text, Total_B_C_txt.Text, Dif_TXT.Text},
                            totalFont, sfCenter)

        y += boxHeight + 4

        DrawSummaryBoxesRow(g, x, y, pageWidth, boxWidth, boxHeight,
                            {"المعد", "تاريخ الطباعة", "مركز التكلفة"},
                            {USER_NAME, Date.Now.ToString("dd/MM/yyyy HH:mm"), costName},
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

    Private Function EstimatePrintRowHeight(g As Graphics, row As DataGridViewRow, bodyFont As Font, accountNameWidth As Integer) As Integer
        Dim accountName As String = GetCellText(row, "إسم الحساب", "ACC_NAME")
        Dim h As Integer = CInt(g.MeasureString(accountName, bodyFont, accountNameWidth - 8).Height) + 10
        If h < 28 Then h = 28
        Return h
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

                Dim colWidths = GetPrintColumnWidths(pageWidth)
                Dim usableHeight As Integer = pageHeight - 235
                Dim y As Integer = 0
                Dim pages As Integer = 1

                For Each rowIndex In PrintableRows
                    Dim h As Integer = EstimatePrintRowHeight(g, DataGridView1.Rows(rowIndex), bodyFont, colWidths(1))

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

    Private Function GetNumberCellText(row As DataGridViewRow, ParamArray columnNames() As String) As String
        Dim text As String = GetCellText(row, columnNames)
        Dim d As Decimal

        If Decimal.TryParse(text, d) Then
            If d = 0D Then Return ""
            Return d.ToString("N3")
        End If

        Return text
    End Function

    Private Sub Hide_Zeros_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Hide_Zeros_CB.CheckedChanged
        CB_CHecked(sender)

    End Sub

    Private Sub Print_Btn_ButtonClick(sender As Object, e As EventArgs) Handles Print_Btn.ButtonClick
        Print_B()
    End Sub

    Private Sub إستخراجالتقريرExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إستخراجالتقريرExcelToolStripMenuItem.Click
        Print_B(True)
    End Sub
End Class
