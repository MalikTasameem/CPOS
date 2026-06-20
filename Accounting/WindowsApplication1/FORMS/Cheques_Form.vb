Public Class Cheques_Form
    Private WithEvents ChequesPD As New System.Drawing.Printing.PrintDocument
    Private ChequesPPD As New PrintPreviewDialog
    Private ChequesPrintRows As New List(Of Integer)
    Private ChequesPrintColumns As New List(Of DataGridViewColumn)
    Private ChequesPrintColumnWidths As New List(Of Integer)
    Private ChequesCurrentRow As Integer = 0
    Private ChequesPageNumber As Integer = 1
    Private ChequesTotalPages As Integer = 1
    Private CurrentChequesPrintLandscape As Boolean = True

    Private Sub Cheques_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateRange_Flate1.FYear_Txt.Text = Identifiers.F_YEAR
        'DataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 224, 224)
        'DataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black
        PreparePrintMenu()
        Load_Balances()
    End Sub
    Public ChequeStatuses_DT As New DataTable

    Public Sub Load_Balances()

        'Dim Con = New SqlConnection("Data Source= localhost ;initial catalog= Tree ;Integrated Security=True;")
        Dim C As New C
        Dim da As New SqlClient.SqlDataAdapter("  SELECT -1 AS StatusId,'---الكل---' AS NameAr UNION ALL select StatusId , NameAr  from ChequeStatuses ", C.Con)
        da.Fill(ChequeStatuses_DT)

        Cheque_Type_CM.DataSource = ChequeStatuses_DT
        Cheque_Type_CM.DisplayMember = "NameAr"
        Cheque_Type_CM.ValueMember = "StatusId"

        DATE_TYPE_CM.SelectedIndex = 0
    End Sub


    Dim DT As New DataTable
    Dim Row_Index As Integer = 0

    Public Async Sub Cheque_SELECT()
        DT = New DataTable

        DataB.Dispose()
        DataB = New BindingSource
        DataGridView1.DataSource = Nothing

        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[Cheque_SELECT]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Cheque_Type", Cheque_Type_CM.SelectedValue)
            .Parameters.AddWithValue("@DATE_TYPE", DATE_TYPE_CM.Text)
            .Parameters.AddWithValue("@DATE_F", DateRange_Flate1.D_F.Value)
            .Parameters.AddWithValue("@DATE_T", DateRange_Flate1.D_T.Value)

        End With

        CircularPanel.Visible = True
        CircularProgressControl1.Start()
        DT = Await Task(Of DataTable).Run(Function() LoadDataTable(C.Com, MY_Settings.SqlConStr))

        DataB.DataSource = DT
        DataGridView1.DataSource = DataB

        With DataGridView1.Columns("قيمة الشيك")
            .ValueType = GetType(Decimal)                       ' يضمن التعامل كرقم
            .DefaultCellStyle.Format = "N3"                     ' 3 منازل عشرية + فاصل آلاف حسب اللغة
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        CircularPanel.Visible = False
        CircularProgressControl1.Stop()

        DataGridView1.Columns("T_ID").Visible = False
        DataGridView1.Columns("StatusId").Visible = False
        DataGridView1.Columns("IsFinal").Visible = False
        DataGridView1.Columns("BackColorHex").Visible = False
        DataGridView1.Columns("ForeColorHex").Visible = False
        DataGridView1.Columns("تاريخ الاستحقاق").Visible = False

        If Row_Index > 0 And DataGridView1.Rows.Count > 0 Then DataGridView1.CurrentCell = DataGridView1.Rows(Row_Index).Cells("قيمة الشيك")

        'advancedDataGridViewSearchToolBar_main.SetColumns(DataGridView1.Columns)

        SEARCH_CM.Items.Clear()
        For i = 0 To DataGridView1.Columns.Count - 1
            If DataGridView1.Columns(i).Visible = True Then
                SEARCH_CM.Items.Add(DataGridView1.Columns(i).Name)
            End If
        Next
    End Sub

    Private Sub Search_btn_Click(sender As Object, e As EventArgs) Handles Search_btn.Click
        Cheque_SELECT()
    End Sub



    'Private Const StatusTextColAr As String = "حالة الشيك"
    'Private Const StatusTextColEn As String = "StatusName"
    'Private Const BackHexCol As String = "BackColorHex"
    'Private Const ForeHexCol As String = "ForeColorHex"

    'Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
    '    If e.RowIndex < 0 Then Exit Sub

    '    ' حدّد عمود نص الحالة
    '    Dim statusCol As DataGridViewColumn = Nothing
    '    If DataGridView1.Columns.Contains(StatusTextColAr) Then
    '        statusCol = DataGridView1.Columns(StatusTextColAr)
    '    ElseIf DataGridView1.Columns.Contains(StatusTextColEn) Then
    '        statusCol = DataGridView1.Columns(StatusTextColEn)
    '    Else
    '        Exit Sub
    '    End If

    '    ' لو الخلية ليست في عمود الحالة، اخرج
    '    If e.ColumnIndex <> statusCol.Index Then Exit Sub

    '    ' خُذ HEX من نتيجة الاستعلام
    '    Dim backHex As String = Nothing
    '    Dim foreHex As String = Nothing

    '    If DataGridView1.Columns.Contains(BackHexCol) Then
    '        backHex = TryCast(DataGridView1.Rows(e.RowIndex).Cells("BackColorHex").Value, String)
    '    End If
    '    'If DataGridView1.Columns.Contains(ForeHexCol) Then
    '    '    foreHex = TryCast(DataGridView1.Rows(e.RowIndex).Cells("ForeColorHex").Value, String)
    '    'End If

    '    ' طبّق اللون إن وُجد
    '    If Not String.IsNullOrWhiteSpace(backHex) Then
    '        e.CellStyle.BackColor = ColorTranslator.FromHtml(backHex.Trim())
    '        e.CellStyle.SelectionBackColor = ControlPaint.Dark(e.CellStyle.BackColor)
    '    End If
    '    If Not String.IsNullOrWhiteSpace(foreHex) Then
    '        e.CellStyle.ForeColor = ColorTranslator.FromHtml(foreHex.Trim())
    '        e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor
    '    End If

    '    e.FormattingApplied = True

    'End Sub


    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete
        ColorizeRows()
    End Sub

    ' بعد: gridv.DataSource = dataTable
    Private Sub ColorizeRows()
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.IsNewRow Then Continue For
            Dim backHex As String = TryCast(row.Cells("BackColorHex").Value, String)
            Dim foreHex As String = TryCast(row.Cells("ForeColorHex").Value, String)



            If Not String.IsNullOrEmpty(backHex) Then
                Dim backColor As Color = System.Drawing.ColorTranslator.FromHtml(backHex)
                'row.DefaultCellStyle.BackColor = backColor
            End If

            If Not String.IsNullOrEmpty(foreHex) Then
                Dim foreColor As Color = System.Drawing.ColorTranslator.FromHtml(foreHex)
                row.DefaultCellStyle.ForeColor = foreColor
            End If

            ' اختياري: تحسين ألوان الاختيار كي تبقى القراءة واضحة مع التمييز
            If row.DefaultCellStyle.BackColor <> Color.Empty Then
                row.DefaultCellStyle.SelectionBackColor = ControlPaint.Dark(row.DefaultCellStyle.BackColor)
            End If
            If row.DefaultCellStyle.ForeColor <> Color.Empty Then
                row.DefaultCellStyle.SelectionForeColor = row.DefaultCellStyle.ForeColor
            End If

            row.DefaultCellStyle.Font = New Font(DataGridView1.Font, FontStyle.Bold)
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub


    Private Sub DataGridView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseDoubleClick
        If DataGridView1.Rows.Count > 0 Then
            Dim f As New Cheques_Mang
            Row_Index = DataGridView1.CurrentCell.RowIndex
            f.T_ID = DataGridView1.CurrentRow.Cells("T_ID").Value
            f.ShowDialog()

        End If
    End Sub


    Public Sub Print_B(Optional ByVal exportToExcel As Boolean = False)
        Try

            Dim Rpt_Title As String = " تقرير حالة الصكــوك " '& vbNewLine

            If DATE_TYPE_CM.SelectedIndex > 0 Then Rpt_Title &= " لتاريخ ( " & DATE_TYPE_CM.Text & " ) للفترة من  " & DateRange_Flate1.D_F.Text & " إلى " & DateRange_Flate1.D_T.Text & vbNewLine


            If Cheque_Type_CM.SelectedIndex > 0 Then Rpt_Title &= " حسب ( " & Cheque_Type_CM.Text & " ) "

            If Not exportToExcel Then
                PrintChequesDocument()
                Exit Sub
            End If

            ExportChequesGridToExcel(Rpt_Title)
            Exit Sub

            Dim pp As New ReportConnection
            pp.rp.Load(Application.StartupPath & "\Reports\Cheques.rpt")
            pp.LoadTables()

            With pp
                .rp.SetParameterValue("TITLE_NUM", Rpt_Title) '
                .rp.SetParameterValue("Title_1", MY_Settings.SBill_Title_1)
                .rp.SetParameterValue("Title_2", MY_Settings.SBill_Title_2)
                .rp.SetParameterValue("Cheque_Type", Cheque_Type_CM.SelectedValue) '
                '.rp.SetParameterValue("DATE_F", DateRange_Flate1.D_F.Value) '
                '.rp.SetParameterValue("DATE_T", DateRange_Flate1.D_T.Value) '
                .rp.SetParameterValue("DATE_TYPE", DATE_TYPE_CM.Text) 'DATE_TYPE_CM.Text
                .rp.SetParameterValue("USER_Input", USER_NAME)
            End With

            ' **تصدير التقرير إلى Excel بدلاً من الطباعة إذا تم اختيار التصدير**
            If exportToExcel Then
                Dim saveDialog As New SaveFileDialog()
                saveDialog.Filter = "Excel Files|*.xls"
                saveDialog.Title = "حفظ التقرير كملف Excel"
                saveDialog.FileName = Rpt_Title & ".xls"

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

    Private Sub Search_By_Acc_Name_txt_TextChanged(sender As Object, e As EventArgs) Handles Search_By_Acc_Name_txt.TextChanged

        If Search_By_Acc_Name_txt.Text.Count > 0 Then

            Dim columnType As String = GetColumnType(DataGridView1, SEARCH_CM.Text)
            Dim Dv As DataView
            Dv = DT.AsDataView
            If columnType = "حرفي" Then
                Dv.RowFilter = IM_Serach(Search_By_Acc_Name_txt.Text, "[" & SEARCH_CM.Text & "]")
            ElseIf columnType = "رقمي" Then
                Dv.RowFilter = "[" & SEARCH_CM.Text & "] = '" & Search_By_Acc_Name_txt.Text & "' "
            End If
            DataGridView1.DataSource = Dv

        Else
            DataGridView1.DataSource = DT
        End If

    End Sub

    ' دالة لتحديد نوع البيانات في أول خلية غير فارغة بالعمود
    Private Function GetColumnType(ByVal dgv As DataGridView, ByVal columnName As String) As String
        If Not dgv.Columns.Contains(columnName) Then
            MessageBox.Show("اسم العمود غير موجود!")
            Return "غير معروف"
        End If

        For Each row As DataGridViewRow In dgv.Rows
            ' تجاهل صفوف جديدة
            If Not row.IsNewRow Then
                Dim cellValue As Object = row.Cells(columnName).Value
                If cellValue IsNot Nothing AndAlso cellValue.ToString().Trim() <> "" Then
                    ' استخدم الدالة المساعدة لتحديد النوع
                    Return GetCellType(cellValue)
                End If
            End If
        Next

        ' إذا لم نجد أي خلية غير فارغة
        Return "فارغ"
    End Function

    ' الدالة المساعدة لتحديد نوع البيانات
    Private Function GetCellType(ByVal cellValue As Object) As String
        Dim number As Double
        If Double.TryParse(cellValue.ToString(), number) Then
            Return "رقمي"
        Else
            Return "حرفي"
        End If
    End Function

    Private Sub SEARCH_CM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SEARCH_CM.SelectedIndexChanged
        Make_Hints()
    End Sub

    Private Sub Make_Hints()
        SendMessage(Search_By_Acc_Name_txt.Handle, &H1501, 0, " إبحث عن " & SEARCH_CM.Text)
    End Sub

    Private Sub Print_Btn_ButtonClick(sender As Object, e As EventArgs) Handles Print_Btn.ButtonClick
        CurrentChequesPrintLandscape = True
        PrintChequesDocument()
    End Sub

    Private Sub إستخراجالتقريرExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إستخراجالتقريرExcelToolStripMenuItem.Click
        Print_B(True)
    End Sub

    Private Sub ExportChequesGridToExcel(reportTitle As String)
        If DataGridView1.Rows.Count = 0 Then
            MsgBox("لا توجد بيانات للتصدير", MsgBoxStyle.Exclamation, "تصدير Excel")
            Exit Sub
        End If

        PrepareChequesPrint()

        Using saveDialog As New SaveFileDialog()
            saveDialog.Filter = "Excel Files|*.xls"
            saveDialog.Title = "حفظ التقرير كملف Excel"
            saveDialog.FileName = CleanChequesFileName("تقرير حالة الصكوك") & ".xls"

            If saveDialog.ShowDialog() <> DialogResult.OK Then Exit Sub

            Try
                Using writer As New System.IO.StreamWriter(saveDialog.FileName, False, System.Text.Encoding.UTF8)
                    writer.WriteLine("<html dir='rtl'>")
                    writer.WriteLine("<head>")
                    writer.WriteLine("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />")
                    writer.WriteLine("<style>")
                    writer.WriteLine("body{font-family:Tahoma;direction:rtl;text-align:right;}")
                    writer.WriteLine("table{border-collapse:collapse;width:100%;}")
                    writer.WriteLine("th{background:#e1e7ee;font-weight:bold;border:1px solid #666;padding:6px;text-align:center;}")
                    writer.WriteLine("td{border:1px solid #999;padding:5px;mso-number-format:'\@';}")
                    writer.WriteLine(".title{font-size:18px;font-weight:bold;text-align:center;margin-bottom:8px;}")
                    writer.WriteLine(".sub{font-size:12px;font-weight:bold;text-align:center;margin-bottom:12px;}")
                    writer.WriteLine("</style>")
                    writer.WriteLine("</head>")
                    writer.WriteLine("<body>")
                    writer.WriteLine("<div class='title'>" & HtmlChequesEncode("تقرير حالة الصكوك") & "</div>")
                    writer.WriteLine("<div class='sub'>" & HtmlChequesEncode(GetChequesReportFilterText()) & "</div>")
                    writer.WriteLine("<table>")
                    writer.WriteLine("<tr>")

                    For Each col As DataGridViewColumn In DataGridView1.Columns
                        If col.Visible Then writer.WriteLine("<th>" & HtmlChequesEncode(col.HeaderText) & "</th>")
                    Next

                    writer.WriteLine("</tr>")

                    For Each row As DataGridViewRow In DataGridView1.Rows
                        If row.IsNewRow Then Continue For
                        writer.WriteLine("<tr>")

                        For Each col As DataGridViewColumn In DataGridView1.Columns
                            If col.Visible Then
                                writer.WriteLine("<td>" & HtmlChequesEncode(GetChequesCellText(row, col)) & "</td>")
                            End If
                        Next

                        writer.WriteLine("</tr>")
                    Next

                    writer.WriteLine("</table>")
                    writer.WriteLine("<br />")
                    writer.WriteLine("<table>")
                    writer.WriteLine("<tr><th>عدد الصكوك</th><td>" & ChequesPrintRows.Count.ToString() & "</td></tr>")
                    writer.WriteLine("<tr><th>إجمالي قيمة الصكوك</th><td>" & HtmlChequesEncode(GetChequesTotalAmount()) & "</td></tr>")
                    writer.WriteLine("<tr><th>المعد</th><td>" & HtmlChequesEncode(USER_NAME) & "</td></tr>")
                    writer.WriteLine("<tr><th>تاريخ التصدير</th><td>" & Date.Now.ToString("dd/MM/yyyy HH:mm") & "</td></tr>")
                    writer.WriteLine("</table>")
                    writer.WriteLine("</body>")
                    writer.WriteLine("</html>")
                End Using

                MsgBox("تم استخراج التقرير بنجاح", MsgBoxStyle.Information, "تصدير Excel")
            Catch ex As Exception
                MessageBox.Show("حدث خطأ أثناء التصدير: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Function HtmlChequesEncode(value As String) As String
        If value Is Nothing Then Return ""

        Return value.Replace("&", "&amp;").
                     Replace("<", "&lt;").
                     Replace(">", "&gt;").
                     Replace("""", "&quot;")
    End Function

    Private Function CleanChequesFileName(value As String) As String
        If String.IsNullOrWhiteSpace(value) Then Return "تقرير حالة الصكوك"

        Dim invalidChars() As Char = System.IO.Path.GetInvalidFileNameChars()
        For Each ch As Char In invalidChars
            value = value.Replace(ch, " "c)
        Next

        Return value.Trim()
    End Function

    Private Sub PreparePrintMenu()
        Print_CntxtMStrip.Items.Clear()

        Dim printLandscapeItem As New ToolStripMenuItem("طباعة بالعرض")
        Dim printPortraitItem As New ToolStripMenuItem("طباعة بالطول")
        Dim exportExcelItem As New ToolStripMenuItem("إستخراج التقرير Excel")

        AddHandler printLandscapeItem.Click,
            Sub()
                CurrentChequesPrintLandscape = True
                PrintChequesDocument()
            End Sub

        AddHandler printPortraitItem.Click,
            Sub()
                CurrentChequesPrintLandscape = False
                PrintChequesDocument()
            End Sub

        AddHandler exportExcelItem.Click,
            Sub()
                Print_B(True)
            End Sub

        Print_CntxtMStrip.Items.Add(printLandscapeItem)
        Print_CntxtMStrip.Items.Add(printPortraitItem)
        Print_CntxtMStrip.Items.Add(New ToolStripSeparator())
        Print_CntxtMStrip.Items.Add(exportExcelItem)
        Print_Btn.DropDownMenu = Print_CntxtMStrip
        Print_Btn.ContextMenuStrip = Print_CntxtMStrip
    End Sub

    Private Sub PrintChequesDocument()
        If DataGridView1.Rows.Count = 0 Then
            MsgBox("لا توجد بيانات للطباعة", MsgBoxStyle.Exclamation, "طباعة الصكوك")
            Exit Sub
        End If

        PrepareChequesPrint()
        ChequesPPD.Document = ChequesPD
        ChequesPPD.WindowState = FormWindowState.Maximized
        ChequesPPD.ShowDialog()
    End Sub

    Private Sub PrepareChequesPrint()
        ChequesCurrentRow = 0
        ChequesPageNumber = 1
        ChequesTotalPages = 1
        ChequesPrintRows.Clear()
        ChequesPrintColumns.Clear()
        ChequesPrintColumnWidths.Clear()

        ChequesPD.DefaultPageSettings.Landscape = CurrentChequesPrintLandscape
        ChequesPD.DefaultPageSettings.Margins = New System.Drawing.Printing.Margins(35, 35, 40, 40)
        ChequesPD.DefaultPageSettings.PaperSize = GetChequesA4PaperSize()

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If Not DataGridView1.Rows(i).IsNewRow Then ChequesPrintRows.Add(i)
        Next

        For Each col As DataGridViewColumn In DataGridView1.Columns
            If col.Visible Then ChequesPrintColumns.Add(col)
        Next

        ChequesTotalPages = EstimateChequesTotalPages()
        If ChequesTotalPages <= 0 Then ChequesTotalPages = 1
    End Sub

    Private Function GetChequesA4PaperSize() As System.Drawing.Printing.PaperSize
        For Each paper As System.Drawing.Printing.PaperSize In ChequesPD.PrinterSettings.PaperSizes
            If paper.Kind = System.Drawing.Printing.PaperKind.A4 Then Return paper
        Next

        Return New System.Drawing.Printing.PaperSize("A4", 827, 1169)
    End Function

    Private Sub ChequesPD_PrintPage(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles ChequesPD.PrintPage
        Dim g = e.Graphics
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        Dim marginLeft As Integer = e.MarginBounds.Left
        Dim marginRight As Integer = e.MarginBounds.Right
        Dim y As Integer = e.MarginBounds.Top
        Dim pageWidth As Integer = e.MarginBounds.Width

        Dim companyFontAr As New Font("Tahoma", 12, FontStyle.Bold)
        Dim companyFontEn As New Font("Tahoma", 11, FontStyle.Bold)
        Dim titleFont As New Font("Tahoma", 15, FontStyle.Bold)
        Dim subTitleFont As New Font("Tahoma", 9.5!, FontStyle.Bold)
        Dim headerFont As New Font("Tahoma", If(CurrentChequesPrintLandscape, 8.5!, 7.5!), FontStyle.Bold)
        Dim bodyFont As New Font("Tahoma", If(CurrentChequesPrintLandscape, 8.0!, 7.0!), FontStyle.Regular)
        Dim footerFont As New Font("Tahoma", 8.5!, FontStyle.Bold)

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
        y += 25
        g.DrawString(MY_Settings.SBill_Title_2, companyFontEn, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 22), sfRight)
        y += 25
        g.DrawLine(Pens.Black, marginLeft, y, marginRight, y)
        y += 8

        g.DrawString("تقرير حالة الصكوك", titleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 28), sfCenter)
        y += 30
        g.DrawString(GetChequesReportFilterText(), subTitleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 22), sfCenter)
        y += 24
        g.DrawString("صفحة " & ChequesPageNumber.ToString() & " من " & ChequesTotalPages.ToString(), bodyFont, Brushes.Black, New RectangleF(marginLeft, y - 4, pageWidth, 18), sfLeft)
        y += 18

        CalculateChequesColumnWidths(pageWidth)
        DrawChequesHeader(g, marginLeft, y, headerFont, sfCenter)
        y += 30

        While ChequesCurrentRow < ChequesPrintRows.Count
            Dim row As DataGridViewRow = DataGridView1.Rows(ChequesPrintRows(ChequesCurrentRow))
            Dim rowHeight As Integer = EstimateChequesRowHeight(g, row, bodyFont)

            If y + rowHeight > e.MarginBounds.Bottom - 76 Then
                DrawChequesFooter(g, e.MarginBounds, footerFont, sfRight, sfCenter, sfLeft)
                e.HasMorePages = True
                ChequesPageNumber += 1
                Return
            End If

            DrawChequesRow(g, row, marginLeft, y, rowHeight, bodyFont, sfCenter, sfRight)
            y += rowHeight
            ChequesCurrentRow += 1
        End While

        y += 8
        DrawChequesTotals(g, marginLeft, y, pageWidth, footerFont, sfCenter)
        DrawChequesFooter(g, e.MarginBounds, footerFont, sfRight, sfCenter, sfLeft)

        e.HasMorePages = False
        ChequesCurrentRow = 0
        ChequesPageNumber = 1
    End Sub

    Private Function GetChequesReportFilterText() As String
        Dim text As String = ""

        If DATE_TYPE_CM.SelectedIndex > 0 Then
            text &= "التاريخ حسب: " & DATE_TYPE_CM.Text & "     من " & DateRange_Flate1.D_F.Value.ToString("dd/MM/yyyy") & " إلى " & DateRange_Flate1.D_T.Value.ToString("dd/MM/yyyy")
        Else
            text &= "كل التواريخ"
        End If

        If Cheque_Type_CM.SelectedIndex > 0 Then text &= "     حالة الصك: " & Cheque_Type_CM.Text
        If SEARCH_CM.Text.Trim() <> "" AndAlso Search_By_Acc_Name_txt.Text.Trim() <> "" Then text &= "     بحث: " & SEARCH_CM.Text & " = " & Search_By_Acc_Name_txt.Text.Trim()

        Return text
    End Function

    Private Sub CalculateChequesColumnWidths(pageWidth As Integer)
        ChequesPrintColumnWidths.Clear()

        Dim totalGridWidth As Integer = 0
        For Each col As DataGridViewColumn In ChequesPrintColumns
            totalGridWidth += Math.Max(col.Width, 45)
        Next

        If totalGridWidth <= 0 Then Exit Sub

        For Each col As DataGridViewColumn In ChequesPrintColumns
            Dim width As Integer = CInt((Math.Max(col.Width, 45) / totalGridWidth) * pageWidth)
            If width < 48 Then width = 48
            ChequesPrintColumnWidths.Add(width)
        Next

        Dim diff As Integer = pageWidth - TotalChequesColumnWidth()
        If ChequesPrintColumnWidths.Count > 0 Then ChequesPrintColumnWidths(ChequesPrintColumnWidths.Count - 1) += diff
    End Sub

    Private Sub DrawChequesHeader(g As Graphics, x As Integer, y As Integer, headerFont As Font, sfCenter As StringFormat)
        Dim currentX As Integer = x + TotalChequesColumnWidth()

        For i As Integer = 0 To ChequesPrintColumns.Count - 1
            currentX -= ChequesPrintColumnWidths(i)
            Dim rect As New Rectangle(currentX, y, ChequesPrintColumnWidths(i), 30)
            g.FillRectangle(New SolidBrush(Color.FromArgb(225, 231, 238)), rect)
            g.DrawRectangle(New Pen(Color.FromArgb(95, 105, 115)), rect)
            g.DrawString(ChequesPrintColumns(i).HeaderText, headerFont, Brushes.Black, New RectangleF(rect.X + 3, rect.Y, rect.Width - 6, rect.Height), sfCenter)
        Next
    End Sub

    Private Sub DrawChequesRow(g As Graphics, row As DataGridViewRow, x As Integer, y As Integer, rowHeight As Integer, bodyFont As Font, sfCenter As StringFormat, sfRight As StringFormat)
        Dim currentX As Integer = x + TotalChequesColumnWidth()

        For i As Integer = 0 To ChequesPrintColumns.Count - 1
            currentX -= ChequesPrintColumnWidths(i)
            Dim rect As New Rectangle(currentX, y, ChequesPrintColumnWidths(i), rowHeight)

            If ChequesCurrentRow Mod 2 = 1 Then
                g.FillRectangle(New SolidBrush(Color.FromArgb(250, 250, 250)), rect)
            End If

            g.DrawRectangle(New Pen(Color.FromArgb(160, 160, 160)), rect)

            Dim col As DataGridViewColumn = ChequesPrintColumns(i)
            Dim text As String = GetChequesCellText(row, col)
            Dim format As StringFormat = If(IsChequesNumberColumn(col), sfCenter, sfRight)
            Dim brush As Brush = If(col.HeaderText.Contains("قيمة") OrElse col.Name.Contains("قيمة"), Brushes.DarkGreen, Brushes.Black)

            g.DrawString(text, bodyFont, brush, New RectangleF(rect.X + 4, rect.Y + 2, rect.Width - 8, rect.Height - 4), format)
        Next
    End Sub

    Private Sub DrawChequesTotals(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, footerFont As Font, sfCenter As StringFormat)
        Dim boxHeight As Integer = 28
        Dim boxWidth As Integer = CInt(pageWidth / 3)

        g.DrawLine(Pens.Black, x, y, x + pageWidth, y)
        y += 6

        DrawChequesSummaryBox(g, New Rectangle(x + pageWidth - boxWidth, y, boxWidth, boxHeight), "عدد الصكوك: " & ChequesPrintRows.Count.ToString(), footerFont, sfCenter)
        DrawChequesSummaryBox(g, New Rectangle(x + pageWidth - (boxWidth * 2), y, boxWidth, boxHeight), "إجمالي قيمة الصكوك: " & GetChequesTotalAmount(), footerFont, sfCenter)
        DrawChequesSummaryBox(g, New Rectangle(x + pageWidth - (boxWidth * 3), y, boxWidth, boxHeight), "حالة الصك: " & If(Cheque_Type_CM.SelectedIndex > 0, Cheque_Type_CM.Text, "الكل"), footerFont, sfCenter)
    End Sub

    Private Sub DrawChequesSummaryBox(g As Graphics, rect As Rectangle, text As String, footerFont As Font, sfCenter As StringFormat)
        g.FillRectangle(New SolidBrush(Color.FromArgb(245, 245, 245)), rect)
        g.DrawRectangle(Pens.Black, rect)
        g.DrawString(text, footerFont, Brushes.Black, New RectangleF(rect.X + 4, rect.Y, rect.Width - 8, rect.Height), sfCenter)
    End Sub

    Private Sub DrawChequesFooter(g As Graphics, marginBounds As Rectangle, footerFont As Font, sfRight As StringFormat, sfCenter As StringFormat, sfLeft As StringFormat)
        Dim y As Integer = marginBounds.Bottom + 10
        Dim partWidth As Single = marginBounds.Width / 3.0F

        g.DrawLine(Pens.Black, marginBounds.Left, y, marginBounds.Right, y)
        y += 8
        g.DrawString("المعد: " & USER_NAME, footerFont, Brushes.Black, New RectangleF(marginBounds.Right - partWidth, y, partWidth, 20), sfRight)
        g.DrawString("صفحة " & ChequesPageNumber.ToString() & " من " & ChequesTotalPages.ToString(), footerFont, Brushes.Black, New RectangleF(marginBounds.Left + partWidth, y, partWidth, 20), sfCenter)
        g.DrawString("تاريخ الطباعة: " & Date.Now.ToString("dd/MM/yyyy HH:mm"), footerFont, Brushes.Black, New RectangleF(marginBounds.Left, y, partWidth, 20), sfLeft)
    End Sub

    Private Function EstimateChequesRowHeight(g As Graphics, row As DataGridViewRow, bodyFont As Font) As Integer
        Dim maxHeight As Integer = 26

        For i As Integer = 0 To ChequesPrintColumns.Count - 1
            Dim text As String = GetChequesCellText(row, ChequesPrintColumns(i))
            Dim h As Integer = CInt(g.MeasureString(text, bodyFont, ChequesPrintColumnWidths(i) - 8).Height) + 8
            If h > maxHeight Then maxHeight = h
        Next

        Return maxHeight
    End Function

    Private Function EstimateChequesTotalPages() As Integer
        Using bmp As New Bitmap(10, 10)
            Using g As Graphics = Graphics.FromImage(bmp)
                Dim pageWidth As Integer = If(CurrentChequesPrintLandscape, 1169, 827) - 70
                Dim pageHeight As Integer = If(CurrentChequesPrintLandscape, 827, 1169) - 80
                Dim availableHeight As Integer = pageHeight - 160
                Dim bodyFont As New Font("Tahoma", If(CurrentChequesPrintLandscape, 8.0!, 7.0!), FontStyle.Regular)
                Dim pages As Integer = 1
                Dim usedHeight As Integer = 0

                CalculateChequesColumnWidths(pageWidth)

                For Each rowIndex As Integer In ChequesPrintRows
                    Dim rowHeight As Integer = EstimateChequesRowHeight(g, DataGridView1.Rows(rowIndex), bodyFont)
                    If usedHeight + rowHeight > availableHeight Then
                        pages += 1
                        usedHeight = 0
                    End If
                    usedHeight += rowHeight
                Next

                Return pages
            End Using
        End Using
    End Function

    Private Function TotalChequesColumnWidth() As Integer
        Dim total As Integer = 0
        For Each width As Integer In ChequesPrintColumnWidths
            total += width
        Next
        Return total
    End Function

    Private Function GetChequesCellText(row As DataGridViewRow, col As DataGridViewColumn) As String
        If row Is Nothing OrElse col Is Nothing Then Return ""
        If row.DataGridView Is Nothing OrElse Not row.DataGridView.Columns.Contains(col.Name) Then Return ""

        Dim value As Object = row.Cells(col.Name).Value
        If value Is Nothing OrElse IsDBNull(value) Then Return ""

        If TypeOf value Is Date OrElse TypeOf value Is DateTime Then Return CDate(value).ToString("dd/MM/yyyy")
        If IsChequesNumberColumn(col) Then
            Dim n As Decimal
            If Decimal.TryParse(value.ToString(), n) Then Return n.ToString("N3")
        End If

        Return value.ToString()
    End Function

    Private Function IsChequesNumberColumn(col As DataGridViewColumn) As Boolean
        If col Is Nothing Then Return False
        If col.ValueType IsNot Nothing AndAlso (col.ValueType Is GetType(Decimal) OrElse col.ValueType Is GetType(Double) OrElse col.ValueType Is GetType(Integer) OrElse col.ValueType Is GetType(Long)) Then Return True
        Return col.HeaderText.Contains("قيمة") OrElse col.HeaderText.Contains("مبلغ") OrElse col.Name.Contains("قيمة") OrElse col.Name.Contains("مبلغ")
    End Function

    Private Function GetChequesTotalAmount() As String
        Dim total As Decimal = 0D
        Dim amountColumn As DataGridViewColumn = Nothing

        If DataGridView1.Columns.Contains("قيمة الشيك") Then
            amountColumn = DataGridView1.Columns("قيمة الشيك")
        Else
            For Each col As DataGridViewColumn In DataGridView1.Columns
                If col.Visible AndAlso (col.HeaderText.Contains("قيمة") OrElse col.Name.Contains("قيمة")) Then
                    amountColumn = col
                    Exit For
                End If
            Next
        End If

        If amountColumn Is Nothing Then Return "0.000"

        For Each rowIndex As Integer In ChequesPrintRows
            Dim value As Object = DataGridView1.Rows(rowIndex).Cells(amountColumn.Name).Value
            Dim n As Decimal
            If value IsNot Nothing AndAlso Not IsDBNull(value) AndAlso Decimal.TryParse(value.ToString(), n) Then total += n
        Next

        Return total.ToString("N3")
    End Function
End Class
