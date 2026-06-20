Imports System.Drawing.Printing
Imports ClosedXML.Excel
Imports System.IO

Public Class Balance_sheet_01

    Private WithEvents PD As New PrintDocument
    Private PPD As New PrintPreviewDialog

    Private CurrentRow As Integer = 0
    Private PageNumber As Integer = 1
    Private ColumnWidths As New List(Of Integer)
    Private RowTopMargin As Integer
    Private PrintHeaderHeight As Integer = 40

    Private Enum BalancePrintMode
        Detailed
        Official
    End Enum

    Private CurrentPrintMode As BalancePrintMode = BalancePrintMode.Detailed
    Private TotalPages As Integer = 1
    Private PrintableRows As New List(Of Integer)

    Private Sub Search_btn_Click(sender As Object, e As EventArgs) Handles Search_btn.Click
        SELECT_Balance_sheet()
    End Sub


    Private Async Sub SELECT_Balance_sheet()

        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "[BalanceSheet_ERP]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@HideZeros", Hide_Zeros_CB.Checked)
            .Parameters.AddWithValue("@DATE_F", DateRange_Flate1.D_F.Value)
            .Parameters.AddWithValue("@DATE_T", DateRange_Flate1.D_T.Value)
            .Parameters.AddWithValue("@ShowAbnormalMark", ShowAbnormalMark_CB.Checked)
        End With

        CircularPanel.Visible = True
        CircularProgressControl1.Start()
        DataGridView.DataSource = Await Task(Of DataTable).Run(Function() LoadDataTable(C.Com, MY_Settings.SqlConStr))
        Coloring()
        CircularPanel.Visible = False
        CircularProgressControl1.Stop()

    End Sub

    Private Sub Coloring()

        For i = 0 To DataGridView.Rows.Count - 1

            If Not IsDBNull(DataGridView.Rows(i).Cells("ACC_CODE_CL").Value) And Not IsDBNull(DataGridView.Rows(i).Cells("ACC_PARENT_CL").Value) Then

                Dim ACC_CODE_VALUE As Integer = Convert.ToInt32(DataGridView.Rows(i).Cells("ACC_CODE_CL").Value)
                Dim ACC_PARENT_VALUE As Integer = Convert.ToInt32(DataGridView.Rows(i).Cells("ACC_PARENT_CL").Value)

                Dim Side As String = DataGridView.Rows(i).Cells("SIDE_CL").Value
                Dim LEVEL As String = Convert.ToInt32(DataGridView.Rows(i).Cells("ACC_LEVEL_CL").Value)


                If LEVEL <> 1 Then
                    If Side = "assets" Then

                        If LEVEL = 2 Then
                            Me.DataGridView.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(100, 230, 100)
                            Me.DataGridView.Rows(i).DefaultCellStyle.Font = New Font("Arial", 14, FontStyle.Bold)
                        Else
                            Me.DataGridView.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192)
                            Me.DataGridView.Rows(i).DefaultCellStyle.Font = New Font("Arial", 12, FontStyle.Regular)
                        End If

                    ElseIf Side = "opponents" Then

                        If LEVEL = 2 Then
                            Me.DataGridView.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(255, 128, 128)
                            Me.DataGridView.Rows(i).DefaultCellStyle.Font = New Font("Arial", 14, FontStyle.Bold)
                        Else
                            Me.DataGridView.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 192)
                            Me.DataGridView.Rows(i).DefaultCellStyle.Font = New Font("Arial", 12, FontStyle.Regular)
                        End If

                    ElseIf Side = "equity" Then

                        If LEVEL = 2 Then
                            Me.DataGridView.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(220, 220, 255)
                            Me.DataGridView.Rows(i).DefaultCellStyle.Font = New Font("Arial", 14, FontStyle.Bold)
                        Else
                            Me.DataGridView.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 255)
                            Me.DataGridView.Rows(i).DefaultCellStyle.Font = New Font("Arial", 12, FontStyle.Regular)
                        End If


                    End If

                End If

            End If
        Next

    End Sub

    Private Sub Balance_sheet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Rs.FindAllControls(Me)
        DataGridView.DefaultCellStyle.SelectionBackColor = DataGridView.DefaultCellStyle.BackColor
        DataGridView.DefaultCellStyle.SelectionForeColor = DataGridView.DefaultCellStyle.ForeColor
        DataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None
        DataGridView.BorderStyle = BorderStyle.None

        DateRange_Flate1.FYear_Txt.Text = Identifiers.F_YEAR
        DateRange_Flate1.ALLTime_CheckBox.Enabled = False
        DateRange_Flate1.MonthCmbo.SelectedIndex = 0
        DateRange_Flate1.MonthCmbo.Enabled = False
        DateRange_Flate1.SetDateRange(Date.Today, False, True)

    End Sub

    Public Sub Print_B(Optional ByVal exportToExcel As Boolean = False)

        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\Reports\Balance_sheet.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue("TITLE_NUM", " قائمـــــة المركــز المالـــي " & vbNewLine & " فـــي " & DateRange_Flate1.D_T.Value)
            .rp.SetParameterValue("Title_1", MY_Settings.SBill_Title_1)
            .rp.SetParameterValue("Title_2", MY_Settings.SBill_Title_2)
            .rp.SetParameterValue("USER_Printer", USER_NAME)
        End With


        ' **تصدير التقرير إلى Excel بدلاً من الطباعة إذا تم اختيار التصدير**
        If exportToExcel Then
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "Excel Files|*.xls"
            saveDialog.Title = "حفظ التقرير كملف Excel"
            saveDialog.FileName = "قائمة المركز المالي.xls"

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


    End Sub

    Private Sub Hide_Zeros_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Hide_Zeros_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub CLOSE_B_Btn_Click(sender As Object, e As EventArgs) Handles CLOSE_B_Btn.Click

        Dim inp = InputBox("أدخل السنة الماليــة التي تريد ترحيل الميزانية لها", "فتح سنة")
        If inp <> "" Then

            Dim numericValue As Integer
            If Not Integer.TryParse(inp, numericValue) Then
                MsgBox("خطأ فالإدخال", MsgBoxStyle.Exclamation, "Invalid Input")
                Exit Sub
            End If

            If check_FOUND_YEAR(inp) = True Then
                Dim START_DATE As String = GET_FIRST_DAY_OF_YEAR(inp).ToString
                If START_DATE <> "0" Then
                    If MessageBox.Show("سيتم إدراج قيد إفتتاحي للسنة الماليــة " & vbNewLine & " معلومات القيد : " & vbNewLine & " 1.ترحيل قائمة المركز المالي من تاريخ " & DateRange_Flate1.D_F.Value & " إلى تاريخ " & DateRange_Flate1.D_T.Value & vbNewLine &
                                       "2. إدراج قيد إفتتاحي للسنة ( " & inp & " ) بتاريخ " & START_DATE, " تاكيــد العملية 1", MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) = DialogResult.OK Then

                        If MessageBox.Show(" تأكيد العملية للمرة الثانية  ", " تاكيــد العملية 2", MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.OK Then

                            MOVE_BALANCE_SHEET_TO_OPEN_NEW_YEAR(DateRange_Flate1.FYear_Txt.Text, inp)

                        End If
                    End If

                Else
                    Dim notification2 As New NotificationForm("خطأ", " لم يتم تحديد جدول للسنة المالية " & inp, "bottom", True)
                    notification2.ShowNotification()
                End If

            Else

                Dim notification3 As New NotificationForm("خطأ", " لم يتم التعرف على السنة او أنها غير معرفة فالنظام ", "bottom", True)
                notification3.ShowNotification()
            End If

        End If
    End Sub

    Public Sub MOVE_BALANCE_SHEET_TO_OPEN_NEW_YEAR(YEAR_FROM As Integer, YEAR_TO As Integer)

        Dim C As New C


        With C.Com
            .Connection = C.Con
            .CommandText = "[MOVE_BALANCE_SHEET_TO_OPEN_NEW_YEAR]"
            .CommandType = CommandType.StoredProcedure

            .Parameters.AddWithValue("@YEAR_FROM", YEAR_FROM)
            .Parameters.AddWithValue("@YEAR_TO", YEAR_TO)
            .Parameters.AddWithValue("@DATE_F", DateRange_Flate1.D_F.Value)
            .Parameters.AddWithValue("@DATE_T", DateRange_Flate1.D_T.Value)
            .Parameters.AddWithValue("@USER_ID", USER_ID)
        End With

        If SQL_SP_EXEC(C.Com) Then
            Dim notification3 As New NotificationForm("تنويه", " تم إضافة قيد إفتتاحي للسنة  " & YEAR_TO.ToString, "bottom")
            notification3.ShowNotification()
        End If
    End Sub

    Private Sub SplitButton1_ButtonClick(sender As Object, e As EventArgs) Handles Print_Btn.ButtonClick

        CurrentPrintMode = BalancePrintMode.Detailed
        PreparePrint()
        PPD.Document = PD
        PPD.WindowState = FormWindowState.Maximized
        PPD.ShowDialog()
    End Sub

    Private Sub إستخراجالتقريرExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إستخراجالتقريرExcelToolStripMenuItem.Click
        'Print_B(True)
        ExportBalanceSheetToExcel(BalancePrintMode.Detailed)
    End Sub

    Private Sub CalculateColumnWidths(dgv As DataGridView, availableWidth As Integer)
        Dim totalVisibleWidth As Integer = 0

        For Each col As DataGridViewColumn In dgv.Columns
            If col.Visible Then
                totalVisibleWidth += col.Width
            End If
        Next

        If totalVisibleWidth = 0 Then Exit Sub

        For Each col As DataGridViewColumn In dgv.Columns
            If col.Visible Then
                Dim w As Integer = CInt((col.Width / totalVisibleWidth) * availableWidth)
                ColumnWidths.Add(w)
            End If
        Next
    End Sub

    Private Sub PD_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PD.PrintPage
        If CurrentPrintMode = BalancePrintMode.Detailed Then
            PrintDetailedReport(e)
        Else
            PrintOfficialReport(e)
        End If
    End Sub


    Private Sub PrintDetailedReport(e As PrintPageEventArgs)
        Dim g = e.Graphics
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        Dim marginLeft As Integer = e.MarginBounds.Left
        Dim marginRight As Integer = e.MarginBounds.Right
        Dim y As Integer = e.MarginBounds.Top
        Dim pageWidth As Integer = e.MarginBounds.Width

        Dim companyFontAr As New Font("Tahoma", 13, FontStyle.Bold)
        Dim companyFontEn As New Font("Tahoma", 13, FontStyle.Bold)
        Dim titleFont As New Font("Tahoma", 15, FontStyle.Bold)
        Dim subTitleFont As New Font("Tahoma", 13, FontStyle.Bold)
        Dim bodyFont As New Font("Tahoma", 10, FontStyle.Regular)
        Dim boldFont As New Font("Tahoma", 10, FontStyle.Bold)
        Dim finalFont As New Font("Tahoma", 11, FontStyle.Bold)

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

        Dim sfNear As New StringFormat With {
        .Alignment = StringAlignment.Near,
        .LineAlignment = StringAlignment.Center,
        .FormatFlags = StringFormatFlags.DirectionRightToLeft
    }

        ' ---------------- رأس التقرير ----------------
        g.DrawString(MY_Settings.SBill_Title_1, companyFontAr, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 25), sfNear)
        y += 28

        g.DrawString(MY_Settings.SBill_Title_2, companyFontEn, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 25), sfRight)
        y += 30

        g.DrawLine(Pens.Black, marginLeft, y, marginRight, y)
        y += 10

        g.DrawString("قائمــة المركــز المـالـي", titleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 28), sfCenter)
        y += 30

        g.DrawString(" من " & DateRange_Flate1.D_F.Value.ToString("dd/MM/yyyy") & " إلى " & DateRange_Flate1.D_T.Value.ToString("dd/MM/yyyy"), subTitleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 25), sfCenter)
        y += 35

        g.DrawLine(Pens.Black, marginLeft, y, marginRight, y)
        y += 15

        g.DrawString("صفحة " & PageNumber.ToString() & " من " & TotalPages.ToString(), bodyFont, Brushes.Black, New RectangleF(marginLeft, y - 12, pageWidth, 20), sfNear)
        Dim pageInfo As String = "صفحة " & PageNumber.ToString() & " من " & TotalPages.ToString()
        'g.DrawString(pageInfo, bodyFont, Brushes.Black, New RectangleF(marginLeft, y - 12, pageWidth - 10, 20), sfRight)

        y += 15

        ' ---------------- جسم التقرير ----------------
        Dim titleAreaWidth As Integer = CInt(pageWidth * 0.72)
        Dim balanceAreaWidth As Integer = pageWidth - titleAreaWidth

        While CurrentRow < PrintableRows.Count
            Dim row As DataGridViewRow = DataGridView.Rows(PrintableRows(CurrentRow))
            Dim rowType As String = GetRowType(row)
            Dim level As Integer = GetLevel(row)
            Dim side As String = GetSide(row)
            Dim title As String = GetRowTitle(row)
            Dim balanceText As String = GetBalanceText(row)

            Dim balanceCode As String = GetCode(row)

            Dim rowHeight As Integer = EstimateRowHeight(g, row, bodyFont, boldFont, titleAreaWidth)

            If y + rowHeight > e.MarginBounds.Bottom Then
                e.HasMorePages = True
                PageNumber += 1
                Return
            End If

            If rowType = "SPACE" Then
                y += 12
                CurrentRow += 1
                Continue While
            End If

            Dim bgBrush As Brush = Brushes.White
            Dim txtBrush As Brush = Brushes.Black
            Dim useFont As Font = bodyFont

            If rowType = "FINAL_TOTAL" Then
                bgBrush = New SolidBrush(Color.FromArgb(220, 220, 220))
                useFont = finalFont
            ElseIf rowType = "FINAL_DIFF" Then
                bgBrush = New SolidBrush(Color.FromArgb(245, 245, 245))
                useFont = finalFont
            ElseIf rowType = "TOTAL" Then
                bgBrush = New SolidBrush(Color.FromArgb(235, 235, 235))
                useFont = boldFont
            ElseIf level = 1 Then
                bgBrush = New SolidBrush(Color.FromArgb(210, 210, 210))
                useFont = New Font("Tahoma", 14, FontStyle.Bold)
            ElseIf level = 2 Then
                If side = "assets" Then
                    bgBrush = New SolidBrush(Color.FromArgb(220, 245, 220))
                ElseIf side = "opponents" Then
                    bgBrush = New SolidBrush(Color.FromArgb(245, 220, 220))
                ElseIf side = "equity" Then
                    bgBrush = New SolidBrush(Color.FromArgb(230, 230, 250))
                End If
                useFont = New Font("Tahoma", 12, FontStyle.Bold)
            Else
                If side = "assets" Then
                    bgBrush = New SolidBrush(Color.FromArgb(245, 255, 245))
                ElseIf side = "opponents" Then
                    bgBrush = New SolidBrush(Color.FromArgb(255, 245, 245))
                ElseIf side = "equity" Then
                    bgBrush = New SolidBrush(Color.FromArgb(248, 248, 255))
                End If
                useFont = bodyFont
            End If

            Dim rowRect As New Rectangle(marginLeft, y, pageWidth, rowHeight)
            g.FillRectangle(bgBrush, rowRect)

            If rowType = "TOTAL" OrElse rowType = "FINAL_TOTAL" OrElse rowType = "FINAL_DIFF" OrElse level <= 2 Then
                g.DrawRectangle(Pens.Gray, rowRect)
            End If

            Dim indent As Integer = GetIndent(level)

            ' مثل الأصل:
            ' الرصيد في العمود الأيسر
            Dim balanceRect As New Rectangle(marginLeft + 10, y, balanceAreaWidth - 20, rowHeight)

            '' البيان في العمود الأيمن
            Dim titleRect As New Rectangle(marginLeft + balanceAreaWidth, y, titleAreaWidth - 15, rowHeight)

            ' إزاحة البيان من اليمين للداخل فعلياً
            Dim titleTextRect As New Rectangle(titleRect.X + indent, titleRect.Y, titleRect.Width - indent - 5, titleRect.Height)


            Dim sfBalance As New StringFormat()
            sfBalance.Alignment = StringAlignment.Center
            sfBalance.LineAlignment = StringAlignment.Center


            Dim sfTitle As New StringFormat()
            sfTitle.Alignment = StringAlignment.Near
            sfTitle.LineAlignment = StringAlignment.Center
            sfTitle.FormatFlags = StringFormatFlags.DirectionRightToLeft


            g.DrawString(balanceCode, useFont, txtBrush, 'رسم كود الحساب
             New RectangleF(titleTextRect.X, titleTextRect.Y, titleTextRect.Width, titleTextRect.Height), sfTitle)


            g.DrawString(title, useFont, txtBrush,
             New RectangleF(titleTextRect.X - 100, titleTextRect.Y, titleTextRect.Width, titleTextRect.Height), sfTitle)

            g.DrawString(balanceText, useFont, txtBrush,
             New RectangleF(balanceRect.X, balanceRect.Y, balanceRect.Width, balanceRect.Height), sfBalance)


            '-----------------------------------------------------------------------------------------------------------------------------------------------------

            If rowType = "FINAL_DIFF" Then
                Dim bal As Decimal = 0D
                Decimal.TryParse(If(row.Cells("BALANCE_CL").Value, "0").ToString(), bal)
                If bal <> 0D Then
                    g.DrawRectangle(New Pen(Color.Red, 1.5F), rowRect)
                End If
            End If

            y += rowHeight
            CurrentRow += 1
        End While

        e.HasMorePages = False
        CurrentRow = 0
        PageNumber = 1
    End Sub

    Private Sub PreparePrint()
        CurrentRow = 0
        PageNumber = 1
        TotalPages = 1
        PrintableRows.Clear()

        PD.DefaultPageSettings.Landscape = False
        PD.DefaultPageSettings.Margins = New Margins(35, 35, 35, 35)

        BuildPrintableRows()

        ' حساب تقريبي أولي لعدد الصفحات
        TotalPages = EstimateTotalPages()
    End Sub

    Private Sub BuildPrintableRows()
        PrintableRows.Clear()

        For i As Integer = 0 To DataGridView.Rows.Count - 1
            If DataGridView.Rows(i).IsNewRow Then Continue For

            Dim row = DataGridView.Rows(i)
            Dim rowType As String = GetRowType(row)
            Dim lvl As Integer = GetLevel(row)

            If CurrentPrintMode = BalancePrintMode.Detailed Then
                PrintableRows.Add(i)
            Else
                ' التقرير الرسمي
                If rowType = "SPACE" Then Continue For

                If rowType = "FINAL_TOTAL" OrElse rowType = "FINAL_DIFF" Then
                    PrintableRows.Add(i)
                ElseIf rowType = "TOTAL" Then
                    If lvl <= TOTAL_UpDown.Value Then PrintableRows.Add(i)
                ElseIf rowType = "ACCOUNT" Then
                    If lvl <= ACCOUNT_UpDown.Value Then PrintableRows.Add(i)
                End If
            End If
        Next
    End Sub


    Private Function GetRowTitle(row As DataGridViewRow) As String
        Dim v1 As String = If(IsDBNull(row.Cells("DataGridViewTextBoxColumn4").Value), "", row.Cells("DataGridViewTextBoxColumn4").Value.ToString())
        Dim v2 As String = If(IsDBNull(row.Cells("DataGridViewTextBoxColumn5").Value), "", row.Cells("DataGridViewTextBoxColumn5").Value.ToString())
        Dim v3 As String = If(IsDBNull(row.Cells("DataGridViewTextBoxColumn6").Value), "", row.Cells("DataGridViewTextBoxColumn6").Value.ToString())

        If v1 <> "" Then Return v1
        If v2 <> "" Then Return v2
        If v3 <> "" Then Return v3

        Return ""
    End Function

    Private Function GetRowType(row As DataGridViewRow) As String
        If DataGridView.Columns.Contains("ROWTYPE_CL") Then
            If row.Cells("ROWTYPE_CL").Value IsNot Nothing AndAlso Not IsDBNull(row.Cells("ROWTYPE_CL").Value) Then
                Return row.Cells("ROWTYPE_CL").Value.ToString().Trim()
            End If
        End If

        Return "ACCOUNT"
    End Function

    Private Function GetLevel(row As DataGridViewRow) As Integer
        If row.Cells("ACC_LEVEL_CL").Value Is Nothing OrElse IsDBNull(row.Cells("ACC_LEVEL_CL").Value) Then
            Return 0
        End If

        Dim lvl As Integer = 0
        Integer.TryParse(row.Cells("ACC_LEVEL_CL").Value.ToString(), lvl)
        Return lvl
    End Function

    Private Function GetSide(row As DataGridViewRow) As String
        If row.Cells("SIDE_CL").Value Is Nothing OrElse IsDBNull(row.Cells("SIDE_CL").Value) Then
            Return ""
        End If

        Return row.Cells("SIDE_CL").Value.ToString().Trim().ToLower()
    End Function

    Private Function GetBalanceText(row As DataGridViewRow) As String
        If row.Cells("BALANCE_CL").Value Is Nothing OrElse IsDBNull(row.Cells("BALANCE_CL").Value) Then
            Return ""
        End If

        Dim d As Decimal = 0D
        Decimal.TryParse(row.Cells("BALANCE_CL").Value.ToString(), d)

        If d = 0D AndAlso Hide_Zeros_CB.Checked Then Return ""

        Return d.ToString("N3")
    End Function

    Private Function GetCode(row As DataGridViewRow) As String
        If row.Cells("ACC_CODE_CL").Value Is Nothing OrElse IsDBNull(row.Cells("ACC_CODE_CL").Value) Then
            Return ""
        End If

        Return row.Cells("ACC_CODE_CL").Value.ToString().Trim().ToLower()
    End Function

    Private Function GetIndent(level As Integer) As Integer
        If level <= 1 Then Return 0
        If level = 2 Then Return 20
        If level = 3 Then Return 40
        Return 40 + ((level - 3) * 15)
    End Function

    Private Function EstimateRowHeight(g As Graphics, row As DataGridViewRow, bodyFont As Font, boldFont As Font, width As Integer) As Integer
        Dim rowType As String = GetRowType(row)
        Dim title As String = GetRowTitle(row)

        If rowType = "SPACE" Then Return 14

        Dim f As Font = bodyFont
        If rowType = "TOTAL" OrElse rowType = "FINAL_TOTAL" OrElse rowType = "FINAL_DIFF" OrElse GetLevel(row) <= 2 Then
            f = boldFont
        End If

        Dim sz = g.MeasureString(title, f, width)
        Dim h As Integer = CInt(sz.Height) + 10
        If h < 28 Then h = 28
        Return h
    End Function

    Private Function EstimateTotalPages() As Integer
        Dim bmp As New Bitmap(10, 10)
        Dim g As Graphics = Graphics.FromImage(bmp)

        Dim bodyFont As New Font("Tahoma", 10, FontStyle.Regular)
        Dim boldFont As New Font("Tahoma", 10, FontStyle.Bold)

        Dim pageHeight As Integer = PD.DefaultPageSettings.Bounds.Height - PD.DefaultPageSettings.Margins.Top - PD.DefaultPageSettings.Margins.Bottom
        Dim usableHeight As Integer = pageHeight - 140

        Dim y As Integer = 0
        Dim pages As Integer = 1

        For Each rowIndex In PrintableRows
            Dim row = DataGridView.Rows(rowIndex)
            Dim h As Integer = EstimateRowHeight(g, row, bodyFont, boldFont, 500)

            If y + h > usableHeight Then
                pages += 1
                y = 0
            End If

            y += h
        Next

        g.Dispose()
        bmp.Dispose()

        Return pages
    End Function
    '--------------------------------------------------------------------------------------------------


    Private Sub PrintOfficial_Btn_ButtonClick(sender As Object, e As EventArgs) Handles PrintOfficial_Btn.ButtonClick
        CurrentPrintMode = BalancePrintMode.Official
        PreparePrint()
        PPD.Document = PD
        PPD.WindowState = FormWindowState.Maximized
        PPD.ShowDialog()
    End Sub


    Private Sub PrintOfficialReport(e As PrintPageEventArgs)
        Dim g = e.Graphics
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        Dim marginLeft As Integer = e.MarginBounds.Left
        Dim marginRight As Integer = e.MarginBounds.Right
        Dim y As Integer = e.MarginBounds.Top
        Dim pageWidth As Integer = e.MarginBounds.Width

        Dim companyFontAr As New Font("Tahoma", 13, FontStyle.Bold)
        Dim companyFontEn As New Font("Tahoma", 12, FontStyle.Bold)
        Dim titleFont As New Font("Tahoma", 16, FontStyle.Bold)
        Dim subTitleFont As New Font("Tahoma", 12, FontStyle.Bold)
        Dim bodyFont As New Font("Tahoma", 10, FontStyle.Regular)
        Dim boldFont As New Font("Tahoma", 10, FontStyle.Bold)
        Dim level1Font As New Font("Tahoma", 13, FontStyle.Bold)
        Dim level2Font As New Font("Tahoma", 11, FontStyle.Bold)
        Dim totalFont As New Font("Tahoma", 11, FontStyle.Bold)
        Dim finalFont As New Font("Tahoma", 12, FontStyle.Bold)

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

        Dim sfTitle As New StringFormat With {
            .Alignment = StringAlignment.Near,
            .LineAlignment = StringAlignment.Center,
            .FormatFlags = StringFormatFlags.DirectionRightToLeft
        }

        Dim sfAmount As New StringFormat With {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Center
        }

        Dim sfNear As New StringFormat With {
        .Alignment = StringAlignment.Near,
        .LineAlignment = StringAlignment.Center,
        .FormatFlags = StringFormatFlags.DirectionRightToLeft
    }


        ' ---------------- رأس التقرير ----------------
        g.DrawString(MY_Settings.SBill_Title_1, companyFontAr, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 25), sfNear)
        y += 28

        g.DrawString(MY_Settings.SBill_Title_2, companyFontEn, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 22), sfRight)
        y += 26

        g.DrawLine(Pens.Black, marginLeft, y, marginRight, y)
        y += 12

        g.DrawString("قائمــة المركــز المـالـي", titleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 30), sfCenter)
        y += 32

        g.DrawString("في " & DateRange_Flate1.D_T.Value.ToString("dd/MM/yyyy"), subTitleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 24), sfCenter)
        y += 28

        'Dim pageInfo As String = "صفحة " & PageNumber.ToString() & " من " & TotalPages.ToString()
        'g.DrawString(pageInfo, bodyFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth - 5, 20), sfRight)

        g.DrawString("صفحة " & PageNumber.ToString() & " من " & TotalPages.ToString(), bodyFont, Brushes.Black, New RectangleF(marginLeft, y - 12, pageWidth, 20), sfNear)
        Dim pageInfo As String = "صفحة " & PageNumber.ToString() & " من " & TotalPages.ToString()

        y += 20
        g.DrawLine(Pens.Black, marginLeft, y, marginRight, y)
        y += 14

        ' ---------------- جسم التقرير ----------------
        Dim titleAreaWidth As Integer = CInt(pageWidth * 0.74)
        Dim amountAreaWidth As Integer = pageWidth - titleAreaWidth

        While CurrentRow < PrintableRows.Count
            Dim row As DataGridViewRow = DataGridView.Rows(PrintableRows(CurrentRow))
            Dim rowType As String = GetRowType(row)
            Dim level As Integer = GetLevel(row)
            Dim side As String = GetSide(row)
            Dim title As String = GetRowTitle(row)
            Dim amountText As String = GetBalanceText(row)

            Dim rowHeight As Integer = 28
            If rowType = "FINAL_TOTAL" OrElse rowType = "FINAL_DIFF" Then rowHeight = 32
            If level = 1 Then rowHeight = 32

            ' مساحة للتوقيعات في آخر الصفحة
            Dim signatureReserved As Integer = 120
            Dim isLastPageArea As Boolean = (CurrentRow >= PrintableRows.Count - 4)

            If y + rowHeight > e.MarginBounds.Bottom - If(isLastPageArea, signatureReserved, 0) Then
                e.HasMorePages = True
                PageNumber += 1
                Return
            End If

            Dim bgBrush As Brush = Brushes.White
            Dim useFont As Font = bodyFont
            Dim borderPen As Pen = Pens.Gray

            If rowType = "FINAL_TOTAL" Then
                bgBrush = New SolidBrush(Color.FromArgb(230, 230, 230))
                useFont = finalFont
                borderPen = Pens.Black
            ElseIf rowType = "FINAL_DIFF" Then
                bgBrush = New SolidBrush(Color.FromArgb(245, 245, 245))
                useFont = finalFont
                borderPen = Pens.Black
            ElseIf rowType = "TOTAL" Then
                bgBrush = New SolidBrush(Color.FromArgb(238, 238, 238))
                useFont = totalFont
            ElseIf level = 1 Then
                bgBrush = New SolidBrush(Color.FromArgb(220, 220, 220))
                useFont = level1Font
                borderPen = Pens.Black
            ElseIf level = 2 Then
                bgBrush = New SolidBrush(Color.FromArgb(245, 245, 245))
                useFont = level2Font
            Else
                bgBrush = Brushes.White
                useFont = bodyFont
            End If

            Dim rowRect As New Rectangle(marginLeft, y, pageWidth, rowHeight)
            g.FillRectangle(bgBrush, rowRect)
            g.DrawRectangle(borderPen, rowRect)

            Dim indent As Integer = 0
            If level = 2 Then indent = 18
            If level = 3 Then indent = 38

            Dim amountRect As New Rectangle(marginLeft + 10, y, amountAreaWidth - 20, rowHeight)
            Dim titleRect As New Rectangle(marginLeft + amountAreaWidth, y, titleAreaWidth - 15, rowHeight)
            Dim titleTextRect As New Rectangle(titleRect.X + 5, titleRect.Y, titleRect.Width - 10 - indent, titleRect.Height)

            g.DrawString(amountText, useFont, Brushes.Black,
                         New RectangleF(amountRect.X, amountRect.Y, amountRect.Width, amountRect.Height), sfAmount)

            g.DrawString(title, useFont, Brushes.Black,
                         New RectangleF(titleTextRect.X, titleTextRect.Y, titleTextRect.Width, titleTextRect.Height), sfTitle)

            If rowType = "FINAL_DIFF" Then
                Dim bal As Decimal = 0D
                Decimal.TryParse(If(row.Cells("BALANCE_CL").Value, "0").ToString(), bal)
                If bal <> 0D Then
                    g.DrawRectangle(New Pen(Color.Red, 1.5F), rowRect)
                End If
            End If

            y += rowHeight + 2
            CurrentRow += 1
        End While

        ' ---------------- التوقيعات والختم ----------------
        Dim signTop As Integer = e.MarginBounds.Bottom - 95

        g.DrawLine(Pens.Black, marginLeft + 40, signTop, marginLeft + 240, signTop)
        g.DrawString("توقيع المدير المالي", bodyFont, Brushes.Black,
                     New RectangleF(marginLeft + 40, signTop + 5, 200, 22), sfCenter)

        g.DrawLine(Pens.Black, marginLeft + 300, signTop, marginLeft + 500, signTop)
        g.DrawString("توقيع المراجع", bodyFont, Brushes.Black,
                     New RectangleF(marginLeft + 300, signTop + 5, 200, 22), sfCenter)

        g.DrawRectangle(Pens.Black, marginRight - 170, signTop - 20, 130, 80)
        g.DrawString("ختم / اعتماد", bodyFont, Brushes.Black,
                     New RectangleF(marginRight - 170, signTop + 85, 130, 22), sfCenter)

        e.HasMorePages = False
        CurrentRow = 0
        PageNumber = 1
    End Sub
    '--------------------------------------------------------------------------------------------------------------------------------------------------------EXCEL : 

    Private Function GetExcelRowTitle(row As DataGridViewRow) As String
        Dim v1 As String = If(IsDBNull(row.Cells("DataGridViewTextBoxColumn4").Value), "", row.Cells("DataGridViewTextBoxColumn4").Value.ToString())
        Dim v2 As String = If(IsDBNull(row.Cells("DataGridViewTextBoxColumn5").Value), "", row.Cells("DataGridViewTextBoxColumn5").Value.ToString())
        Dim v3 As String = If(IsDBNull(row.Cells("DataGridViewTextBoxColumn6").Value), "", row.Cells("DataGridViewTextBoxColumn6").Value.ToString())

        If v1 <> "" Then Return v1
        If v2 <> "" Then Return v2
        If v3 <> "" Then Return v3

        Return ""
    End Function

    Private Function GetExcelRowType(row As DataGridViewRow) As String
        If DataGridView.Columns.Contains("ROWTYPE_CL") Then
            If row.Cells("ROWTYPE_CL").Value IsNot Nothing AndAlso Not IsDBNull(row.Cells("ROWTYPE_CL").Value) Then
                Return row.Cells("ROWTYPE_CL").Value.ToString().Trim()
            End If
        End If

        Return "ACCOUNT"
    End Function

    Private Function GetExcelLevel(row As DataGridViewRow) As Integer
        If row.Cells("ACC_LEVEL_CL").Value Is Nothing OrElse IsDBNull(row.Cells("ACC_LEVEL_CL").Value) Then Return 0

        Dim lvl As Integer = 0
        Integer.TryParse(row.Cells("ACC_LEVEL_CL").Value.ToString(), lvl)
        Return lvl
    End Function

    Private Function GetExcelSide(row As DataGridViewRow) As String
        If row.Cells("SIDE_CL").Value Is Nothing OrElse IsDBNull(row.Cells("SIDE_CL").Value) Then Return ""
        Return row.Cells("SIDE_CL").Value.ToString().Trim().ToLower()
    End Function

    Private Function GetExcelBalance(row As DataGridViewRow) As Decimal?
        If row.Cells("BALANCE_CL").Value Is Nothing OrElse IsDBNull(row.Cells("BALANCE_CL").Value) Then Return Nothing

        Dim d As Decimal = 0D
        If Decimal.TryParse(row.Cells("BALANCE_CL").Value.ToString(), d) Then
            Return d
        End If

        Return Nothing
    End Function

    Private Function GetExcelCode(row As DataGridViewRow) As String
        If row.Cells("ACC_CODE_CL").Value Is Nothing OrElse IsDBNull(row.Cells("ACC_CODE_CL").Value) Then Return ""
        Return row.Cells("ACC_CODE_CL").Value.ToString().Trim().ToLower()
    End Function

    Private Function ShouldExportRow(row As DataGridViewRow, mode As BalancePrintMode) As Boolean
        Dim rowType As String = GetExcelRowType(row)
        Dim lvl As Integer = GetExcelLevel(row)

        If mode = BalancePrintMode.Detailed Then
            Return Not row.IsNewRow
        End If

        ' الرسمي
        If row.IsNewRow Then Return False
        If rowType = "SPACE" Then Return False

        If rowType = "FINAL_TOTAL" OrElse rowType = "FINAL_DIFF" Then Return True
        If rowType = "TOTAL" AndAlso lvl <= TOTAL_UpDown.Value Then Return True
        If rowType = "ACCOUNT" AndAlso lvl <= ACCOUNT_UpDown.Value Then Return True

        Return False
    End Function


    Private Sub ExportBalanceSheetToExcel(mode As BalancePrintMode)
        If DataGridView.Rows.Count = 0 Then
            MessageBox.Show("لا توجد بيانات للتصدير.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim saveDialog As New SaveFileDialog()
        saveDialog.Filter = "Excel Workbook|*.xlsx"
        saveDialog.Title = "حفظ التقرير كملف Excel"
        saveDialog.FileName = If(mode = BalancePrintMode.Detailed,
                                 "قائمة المركز المالي - تفصيلي.xlsx",
                                 "قائمة المركز المالي - رسمي.xlsx")

        If saveDialog.ShowDialog() <> DialogResult.OK Then Exit Sub

        Dim filePath As String = saveDialog.FileName

        Using wb As New XLWorkbook()
            Dim ws = wb.Worksheets.Add("Balance Sheet")

            ' اتجاه الصفحة
            ws.RightToLeft = True
            ws.PageSetup.PageOrientation = XLPageOrientation.Landscape
            ws.PageSetup.PaperSize = XLPaperSize.A4Paper

            'ws.PageSetup.SetMargins(0.4, 0.4, 0.5, 0.5)
            ws.PageSetup.Margins.Left = 0.4
            ws.PageSetup.Margins.Right = 0.4
            ws.PageSetup.Margins.Top = 0.5
            ws.PageSetup.Margins.Bottom = 0.5

            ws.PageSetup.CenterHorizontally = True

            ' عرض الأعمدة
            ws.Column("A").Width = 8     ' ت
            ws.Column("B").Width = 15     ' الكود
            ws.Column("C").Width = 55    ' البيان
            ws.Column("D").Width = 20    ' الرصيد

            Dim currentRow As Integer = 1

            ' رأس التقرير
            ws.Range(currentRow, 1, currentRow, 3).Merge()
            ws.Cell(currentRow, 1).Value = MY_Settings.SBill_Title_1
            ws.Cell(currentRow, 1).Style.Font.Bold = True
            ws.Cell(currentRow, 1).Style.Font.FontSize = 14
            ws.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
            currentRow += 1

            ws.Range(currentRow, 1, currentRow, 3).Merge()
            ws.Cell(currentRow, 1).Value = MY_Settings.SBill_Title_2
            ws.Cell(currentRow, 1).Style.Font.Bold = True
            ws.Cell(currentRow, 1).Style.Font.FontSize = 12
            ws.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
            currentRow += 2

            ws.Range(currentRow, 1, currentRow, 3).Merge()
            ws.Cell(currentRow, 1).Value = "قائمــة المركــز المـالـي"
            ws.Cell(currentRow, 1).Style.Font.Bold = True
            ws.Cell(currentRow, 1).Style.Font.FontSize = 16
            ws.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            currentRow += 1

            ws.Range(currentRow, 1, currentRow, 3).Merge()
            ws.Cell(currentRow, 1).Value = "في " & DateRange_Flate1.D_T.Value.ToString("dd/MM/yyyy")
            ws.Cell(currentRow, 1).Style.Font.Bold = True
            ws.Cell(currentRow, 1).Style.Font.FontSize = 12
            ws.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            currentRow += 2

            ' عناوين الأعمدة
            ws.Cell(currentRow, 1).Value = "ت"
            ws.Cell(currentRow, 2).Value = "رقم الحســاب"
            ws.Cell(currentRow, 3).Value = "البيـــان"
            ws.Cell(currentRow, 4).Value = "الرصيـــد"

            With ws.Range(currentRow, 1, currentRow, 4)
                .Style.Font.Bold = True
                .Style.Fill.BackgroundColor = XLColor.LightGray
                .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                .Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                .Style.Border.InsideBorder = XLBorderStyleValues.Thin
            End With

            currentRow += 1

            Dim serial As Integer = 1

            For Each dgRow As DataGridViewRow In DataGridView.Rows
                If Not ShouldExportRow(dgRow, mode) Then Continue For

                Dim rowType As String = GetExcelRowType(dgRow)
                Dim lvl As Integer = GetExcelLevel(dgRow)
                Dim side As String = GetExcelSide(dgRow)
                Dim title As String = GetExcelRowTitle(dgRow)
                Dim balance As Decimal? = GetExcelBalance(dgRow)
                Dim acc_code As String = GetExcelCode(dgRow)

                If rowType = "SPACE" Then
                    currentRow += 1
                    Continue For
                End If

                ws.Cell(currentRow, 1).Value = serial
                ws.Cell(currentRow, 2).Value = acc_code
                ws.Cell(currentRow, 3).Value = title

                If balance.HasValue Then
                    ws.Cell(currentRow, 4).Value = balance.Value
                    ws.Cell(currentRow, 4).Style.NumberFormat.Format = "#,##0.000"
                Else
                    ws.Cell(currentRow, 4).Value = ""
                End If

                ' محاذاة
                ws.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                ws.Cell(currentRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                ws.Cell(currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                ws.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                ' الإزاحة حسب المستوى
                Dim indent As Integer = 0
                If lvl = 2 Then indent = 1
                If lvl = 3 Then indent = 2
                If lvl > 3 Then indent = 3
                ws.Cell(currentRow, 2).Style.Alignment.Indent = indent

                ' حدود
                ws.Range(currentRow, 1, currentRow, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin
                ws.Range(currentRow, 1, currentRow, 4).Style.Border.InsideBorder = XLBorderStyleValues.Thin

                ' التنسيق حسب النوع
                If rowType = "FINAL_TOTAL" Then
                    ws.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = True
                    ws.Range(currentRow, 1, currentRow, 4).Style.Fill.BackgroundColor = XLColor.LightGray
                    ws.Range(currentRow, 1, currentRow, 4).Style.Font.FontSize = 12
                ElseIf rowType = "FINAL_DIFF" Then
                    ws.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = True
                    ws.Range(currentRow, 1, currentRow, 4).Style.Fill.BackgroundColor = XLColor.LightGray
                    ws.Range(currentRow, 1, currentRow, 4).Style.Font.FontSize = 12

                    If balance.HasValue AndAlso balance.Value <> 0D Then
                        ws.Range(currentRow, 1, currentRow, 3).Style.Font.FontColor = XLColor.Red
                    End If
                ElseIf rowType = "TOTAL" Then
                    ws.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = True
                    ws.Range(currentRow, 1, currentRow, 4).Style.Fill.BackgroundColor = XLColor.LightGray
                ElseIf lvl = 1 Then
                    ws.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = True
                    ws.Range(currentRow, 1, currentRow, 4).Style.Fill.BackgroundColor = XLColor.LightGray
                    ws.Range(currentRow, 1, currentRow, 4).Style.Font.FontSize = 13
                ElseIf lvl = 2 Then
                    ws.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = True
                    ws.Range(currentRow, 1, currentRow, 4).Style.Font.FontSize = 11

                    If side = "assets" Then
                        ws.Range(currentRow, 1, currentRow, 4).Style.Fill.BackgroundColor = XLColor.LightGreen
                    ElseIf side = "opponents" Then
                        ws.Range(currentRow, 1, currentRow, 4).Style.Fill.BackgroundColor = XLColor.LightPink
                    ElseIf side = "equity" Then
                        ws.Range(currentRow, 1, currentRow, 4).Style.Fill.BackgroundColor = XLColor.LightBlue
                    End If
                Else
                    If side = "assets" Then
                        ws.Range(currentRow, 1, currentRow, 4).Style.Fill.BackgroundColor = XLColor.Honeydew
                    ElseIf side = "opponents" Then
                        ws.Range(currentRow, 1, currentRow, 4).Style.Fill.BackgroundColor = XLColor.MistyRose
                    ElseIf side = "equity" Then
                        ws.Range(currentRow, 1, currentRow, 4).Style.Fill.BackgroundColor = XLColor.AliceBlue
                    End If
                End If

                currentRow += 1
                serial += 1
            Next

            ' التوقيعات في التقرير الرسمي فقط
            If mode = BalancePrintMode.Official Then
                currentRow += 3

                ws.Range(currentRow, 1, currentRow, 1).Merge()
                ws.Range(currentRow, 2, currentRow, 2).Merge()
                ws.Range(currentRow, 3, currentRow, 3).Merge()

                ws.Cell(currentRow, 1).Value = "توقيع المدير المالي"
                ws.Cell(currentRow, 2).Value = "توقيع المراجع"
                ws.Cell(currentRow, 3).Value = "ختم / اعتماد"

                ws.Range(currentRow, 1, currentRow, 3).Style.Font.Bold = True
                ws.Range(currentRow, 1, currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

                currentRow += 2

                ws.Cell(currentRow, 1).Value = "........................"
                ws.Cell(currentRow, 2).Value = "........................"
                ws.Cell(currentRow, 3).Value = "........................"

                ws.Range(currentRow, 1, currentRow, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            End If

            ' تحسين عام
            ws.Style.Font.FontName = "Tahoma"
            ws.Style.Font.FontSize = 10
            ws.Rows().AdjustToContents()

            wb.SaveAs(filePath)
        End Using

        MessageBox.Show("تم تصدير التقرير بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        ExportBalanceSheetToExcel(BalancePrintMode.Official)
    End Sub

    Private Sub ShowAbnormalMark_CB_CheckedChanged(sender As Object, e As EventArgs) Handles ShowAbnormalMark_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub
End Class