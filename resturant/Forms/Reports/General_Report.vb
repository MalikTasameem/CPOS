Imports System.Data.SqlClient

Imports System.Drawing.Printing

Public Class General_Report
    Dim rs As New Resizer
    Dim IM_DT As New DataTable
    Dim ST As Boolean = False
    Private _printRowIndex As Integer = 0
    Private _printPageNumber As Integer = 1
    Private _printDateTime As DateTime
    Private WithEvents PdfButton As Button

    Private Sub STORES_Explorer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
    Private Sub STORES_Explorer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.ApplyThemeToForm(Me)
        SetupPdfButton()
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        GENERAL_REPORT_SELECT()
        'GENERAL_REPORT_SELECT_2()
        'GENERAL_REPORT_SELECT_3()
    End Sub
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Const CS_DROPSHADOW As Integer = &H20000
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property
    Private drag As Boolean
    Private mouseX As Integer
    Private mouseY As Integer

    Private Sub TitleBar_Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseDown, TopTitle_LB.MouseDown
        drag = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub TitleBar_Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseMove, TopTitle_LB.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub

    Private Sub TitleBar_Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseUp, TopTitle_LB.MouseUp
        drag = False
    End Sub

    Private Sub STORES_Explorer_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub Store_R_Insert()
        Dim c As New C
        Dim sqlCon = New SqlClient.SqlConnection(MY_Settings.SqlConStr)
        Using (sqlCon)
            Dim sqlComm As New SqlClient.SqlCommand()
            c.Com = New SqlClient.SqlCommand
            sqlComm.Connection = sqlCon
            sqlComm.CommandText = "STORES_R_DELETE"
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlCon.Open()
            Try
                sqlComm.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        sqlCon.Close()
        '***********************************************************************

        For i = 0 To DataGridViewX.Rows.Count - 1
            c = New C
            sqlCon = New SqlClient.SqlConnection(MY_Settings.SqlConStr)
            Using (sqlCon)
                Dim sqlComm As New SqlClient.SqlCommand()
                c.Com = New SqlClient.SqlCommand
                sqlComm.Connection = sqlCon
                sqlComm.CommandText = "STORES_R_INSERT"
                sqlComm.CommandType = CommandType.StoredProcedure

                sqlComm.Parameters.AddWithValue("@CM_NAME", DataGridViewX.Rows(i).Cells("item_name_CL").Value)
                sqlComm.Parameters.AddWithValue("@QYT", DataGridViewX.Rows(i).Cells("QTY_CL").Value)
                sqlComm.Parameters.AddWithValue("@Unit", DataGridViewX.Rows(i).Cells("Unit_CL").Value)
                sqlComm.Parameters.AddWithValue("@Cost", DataGridViewX.Rows(i).Cells("Cost_CL").Value)
                sqlCon.Open()
                Try
                    sqlComm.ExecuteNonQuery()
                    ' MsgBox("تـــم الحذف ", MsgBoxStyle.Information)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
            sqlCon.Close()
        Next


    End Sub

    Private Sub SetupPdfButton()

        If PdfButton IsNot Nothing Then Return

        PdfButton = New Button With {
            .BackColor = PrintButton.BackColor,
            .Cursor = Cursors.Hand,
            .FlatStyle = PrintButton.FlatStyle,
            .Font = PrintButton.Font,
            .ForeColor = PrintButton.ForeColor,
            .ImageAlign = PrintButton.ImageAlign,
            .Location = New Point(3, PrintButton.Top),
            .Name = "PdfButton",
            .RightToLeft = System.Windows.Forms.RightToLeft.Yes,
            .Size = New Size(84, PrintButton.Height),
            .TabIndex = PrintButton.TabIndex + 1,
            .Tag = "PRINT",
            .Text = "PDF",
            .UseVisualStyleBackColor = False
        }

        Me.Controls.Add(PdfButton)
        PdfButton.BringToFront()

    End Sub

    Public Sub STORE_R_Print()

        If DataGridViewX.Rows.Count = 0 Then Return

        Using printDocument As PrintDocument = CreateGeneralReportPrintDocument()
            Using previewDialog As New PrintPreviewDialog()
                previewDialog.Document = printDocument
                previewDialog.WindowState = FormWindowState.Maximized
                previewDialog.Text = "معاينة التقرير العام"
                previewDialog.ShowDialog(Me)
            End Using
        End Using

    End Sub

    Private Sub ExportGeneralReportPdf()

        If DataGridViewX.Rows.Count = 0 Then
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
            saveDialog.FileName = "التقرير العام " & Date.Now.ToString("yyyyMMdd_HHmm") & ".pdf"
            saveDialog.Title = "حفظ التقرير العام PDF"

            If saveDialog.ShowDialog(Me) <> DialogResult.OK Then Return

            Using printDocument As PrintDocument = CreateGeneralReportPrintDocument()
                printDocument.PrinterSettings.PrinterName = pdfPrinterName
                printDocument.PrinterSettings.PrintToFile = True
                printDocument.PrinterSettings.PrintFileName = saveDialog.FileName
                printDocument.PrintController = New StandardPrintController()
                printDocument.Print()
            End Using
        End Using

    End Sub

    Private Function CreateGeneralReportPrintDocument() As PrintDocument

        Dim printDocument As New PrintDocument()
        printDocument.DocumentName = "التقرير العام"
        printDocument.DefaultPageSettings.Landscape = True
        printDocument.DefaultPageSettings.Margins = New Margins(30, 30, 40, 45)

        AddHandler printDocument.BeginPrint, AddressOf GeneralReportPrintDocument_BeginPrint
        AddHandler printDocument.PrintPage, AddressOf GeneralReportPrintDocument_PrintPage

        Return printDocument

    End Function

    Private Sub GeneralReportPrintDocument_BeginPrint(sender As Object, e As PrintEventArgs)

        _printRowIndex = 0
        _printPageNumber = 1
        _printDateTime = Date.Now

    End Sub

    Private Sub GeneralReportPrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)

        Dim bounds As Rectangle = e.MarginBounds
        Dim y As Integer = bounds.Top
        Dim visibleColumns As List(Of DataGridViewColumn) = GetGeneralReportPrintableColumns()

        If visibleColumns.Count = 0 Then
            e.HasMorePages = False
            Return
        End If

        Using storeTitleFont As New Font("Segoe UI", 15.0!, FontStyle.Bold),
              storeSubTitleFont As New Font("Segoe UI", 10.0!, FontStyle.Bold),
              titleFont As New Font("Segoe UI", 13.0!, FontStyle.Bold),
              infoFont As New Font("Segoe UI", 8.5!, FontStyle.Bold),
              headerFont As New Font("Segoe UI", 7.0!, FontStyle.Bold),
              rowFont As New Font("Segoe UI", 7.0!, FontStyle.Regular),
              totalFont As New Font("Segoe UI", 8.0!, FontStyle.Bold)

            Dim rtlFormat As New StringFormat()
            rtlFormat.Alignment = StringAlignment.Far
            rtlFormat.LineAlignment = StringAlignment.Center
            rtlFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft
            rtlFormat.Trimming = StringTrimming.EllipsisCharacter

            Dim descriptionFormat As New StringFormat()
            descriptionFormat.Alignment = StringAlignment.Near
            descriptionFormat.LineAlignment = StringAlignment.Center
            descriptionFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft
            descriptionFormat.Trimming = StringTrimming.Word

            Dim centerFormat As New StringFormat()
            centerFormat.Alignment = StringAlignment.Center
            centerFormat.LineAlignment = StringAlignment.Center
            centerFormat.Trimming = StringTrimming.EllipsisCharacter

            DrawGeneralReportStoreHeader(e.Graphics, bounds, y, storeTitleFont, storeSubTitleFont, centerFormat)

            e.Graphics.DrawString(
                TopTitle_LB.Text,
                titleFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 28),
                centerFormat
            )
            y += 30

            e.Graphics.DrawString(
                GetGeneralReportFilterText(),
                infoFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 24),
                rtlFormat
            )
            y += 30

            Dim rowHeight As Integer = 24
            Dim widths As List(Of Integer) = CalculateGeneralReportColumnWidths(visibleColumns, bounds.Width)
            Dim x As Integer = bounds.Right

            For i As Integer = 0 To visibleColumns.Count - 1
                x -= widths(i)
                Dim rect As New Rectangle(x, y, widths(i), rowHeight)

                Using backBrush As New SolidBrush(Color.FromArgb(45, 62, 80))
                    e.Graphics.FillRectangle(backBrush, rect)
                End Using

                e.Graphics.DrawRectangle(Pens.DarkGray, rect)
                e.Graphics.DrawString(GetGeneralReportColumnHeader(visibleColumns(i)), headerFont, Brushes.White, rect, centerFormat)
            Next

            y += rowHeight

            Dim firstRowY As Integer = y
            Dim totalPages As Integer = CalculateGeneralReportTotalPages(e.Graphics, visibleColumns, widths, rowFont, firstRowY, bounds.Bottom, rowHeight)
            Dim currentPage As Integer = _printPageNumber

            While _printRowIndex < DataGridViewX.Rows.Count

                Dim row As DataGridViewRow = DataGridViewX.Rows(_printRowIndex)
                Dim currentRowHeight As Integer = CalculateGeneralReportRowHeight(e.Graphics, row, visibleColumns, widths, rowFont, rowHeight)
                Dim maxRowHeight As Integer = Math.Max(rowHeight, bounds.Bottom - 58 - firstRowY)

                If currentRowHeight > maxRowHeight Then currentRowHeight = maxRowHeight

                If y + currentRowHeight > bounds.Bottom - 58 Then
                    DrawGeneralReportFooter(e.Graphics, bounds, currentPage, totalPages, infoFont, centerFormat)
                    _printPageNumber += 1
                    e.HasMorePages = True
                    Return
                End If

                x = bounds.Right

                For i As Integer = 0 To visibleColumns.Count - 1
                    x -= widths(i)
                    Dim rect As New Rectangle(x, y, widths(i), currentRowHeight)

                    If _printRowIndex Mod 2 = 0 Then
                        e.Graphics.FillRectangle(Brushes.White, rect)
                    Else
                        Using altBrush As New SolidBrush(Color.FromArgb(248, 250, 252))
                            e.Graphics.FillRectangle(altBrush, rect)
                        End Using
                    End If

                    e.Graphics.DrawRectangle(Pens.LightGray, rect)

                    If IsGeneralReportDescriptionColumn(visibleColumns(i)) Then
                        Dim descriptionRect As New Rectangle(rect.Left + 4, rect.Top + 2, rect.Width - 8, rect.Height - 4)
                        e.Graphics.DrawString(GetGeneralReportCellText(row, visibleColumns(i)), rowFont, Brushes.Black, descriptionRect, descriptionFormat)
                    Else
                        e.Graphics.DrawString(GetGeneralReportCellText(row, visibleColumns(i)), rowFont, Brushes.Black, rect, centerFormat)
                    End If
                Next

                y += currentRowHeight
                _printRowIndex += 1

            End While

            y += 8
            DrawGeneralReportSummary(e.Graphics, bounds, y, totalFont, centerFormat)
            DrawGeneralReportFooter(e.Graphics, bounds, currentPage, totalPages, infoFont, centerFormat)

        End Using

        e.HasMorePages = False

    End Sub

    Private Function GetGeneralReportPrintableColumns() As List(Of DataGridViewColumn)

        Dim columns As New List(Of DataGridViewColumn)()

        For Each col As DataGridViewColumn In DataGridViewX.Columns
            If col.Visible Then columns.Add(col)
        Next

        Return columns

    End Function

    Private Function CalculateGeneralReportColumnWidths(columns As List(Of DataGridViewColumn), availableWidth As Integer) As List(Of Integer)

        Dim widths As New List(Of Integer)()
        Dim totalGridWidth As Integer = 0

        For Each col As DataGridViewColumn In columns
            totalGridWidth += Math.Max(30, col.Width)
        Next

        If totalGridWidth <= 0 Then totalGridWidth = columns.Count * 80

        Dim usedWidth As Integer = 0

        For i As Integer = 0 To columns.Count - 1
            Dim width As Integer

            If i = columns.Count - 1 Then
                width = Math.Max(30, availableWidth - usedWidth)
            Else
                width = Math.Max(30, CInt(availableWidth * (Math.Max(30, columns(i).Width) / CDbl(totalGridWidth))))
            End If

            widths.Add(width)
            usedWidth += width
        Next

        Return widths

    End Function

    Private Function GetGeneralReportColumnHeader(column As DataGridViewColumn) As String

        If Not String.IsNullOrWhiteSpace(column.HeaderText) Then Return column.HeaderText.Trim()

        Return column.Name

    End Function

    Private Function GetGeneralReportCellText(row As DataGridViewRow, column As DataGridViewColumn) As String

        If row.IsNewRow Then Return ""
        If row.Cells(column.Name).Value Is Nothing OrElse row.Cells(column.Name).Value Is DBNull.Value Then Return ""

        Dim value As Object = row.Cells(column.Name).Value

        If IsGeneralReportSequenceColumn(column) Then
            Dim sequenceValue As Decimal
            If Decimal.TryParse(value.ToString(), sequenceValue) Then Return sequenceValue.ToString("0")
        End If

        If TypeOf value Is DateTime Then
            Return CDate(value).ToString("yyyy/MM/dd")
        End If

        Dim numberValue As Decimal
        If Decimal.TryParse(value.ToString(), numberValue) Then Return numberValue.ToString(N_Point_Fter)

        Return row.Cells(column.Name).FormattedValue.ToString()

    End Function

    Private Function IsGeneralReportSequenceColumn(column As DataGridViewColumn) As Boolean

        Dim headerText As String = column.HeaderText.Trim()
        Dim columnName As String = column.Name.Trim()

        If column.Index = 0 OrElse column.DisplayIndex = 0 Then Return True

        If headerText = "م" OrElse headerText = "م." OrElse headerText = "ت" OrElse headerText = "ت." OrElse headerText = "#" Then Return True
        If headerText.Equals("No", StringComparison.OrdinalIgnoreCase) OrElse headerText.Equals("No.", StringComparison.OrdinalIgnoreCase) Then Return True
        If headerText.Equals("Num", StringComparison.OrdinalIgnoreCase) OrElse headerText.Equals("Number", StringComparison.OrdinalIgnoreCase) Then Return True

        If columnName.Equals("م", StringComparison.OrdinalIgnoreCase) OrElse columnName.Equals("ت", StringComparison.OrdinalIgnoreCase) OrElse columnName.Equals("No", StringComparison.OrdinalIgnoreCase) Then Return True
        If columnName.Equals("Num", StringComparison.OrdinalIgnoreCase) OrElse columnName.Equals("Number", StringComparison.OrdinalIgnoreCase) Then Return True

        Return False

    End Function

    Private Function IsGeneralReportDescriptionColumn(column As DataGridViewColumn) As Boolean

        If column.HeaderText.Trim().Equals("Description", StringComparison.OrdinalIgnoreCase) Then Return True
        If column.Name.Trim().Equals("Description", StringComparison.OrdinalIgnoreCase) Then Return True
        If column.Name.Trim().IndexOf("Description", StringComparison.OrdinalIgnoreCase) >= 0 Then Return True

        Return False

    End Function

    Private Function CalculateGeneralReportRowHeight(graphics As Graphics, row As DataGridViewRow, columns As List(Of DataGridViewColumn), widths As List(Of Integer), rowFont As Font, baseRowHeight As Integer) As Integer

        Dim rowHeight As Integer = baseRowHeight

        Using descriptionFormat As New StringFormat()
            descriptionFormat.Alignment = StringAlignment.Near
            descriptionFormat.LineAlignment = StringAlignment.Center
            descriptionFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft
            descriptionFormat.Trimming = StringTrimming.Word

            For i As Integer = 0 To columns.Count - 1
                If Not IsGeneralReportDescriptionColumn(columns(i)) Then Continue For

                Dim descriptionText As String = GetGeneralReportCellText(row, columns(i))
                If String.IsNullOrWhiteSpace(descriptionText) Then Continue For

                Dim textSize As SizeF = graphics.MeasureString(descriptionText, rowFont, Math.Max(30, widths(i) - 8), descriptionFormat)
                rowHeight = Math.Max(rowHeight, CInt(Math.Ceiling(textSize.Height)) + 10)
            Next
        End Using

        Return rowHeight

    End Function

    Private Function CalculateGeneralReportTotalPages(graphics As Graphics, columns As List(Of DataGridViewColumn), widths As List(Of Integer), rowFont As Font, firstRowY As Integer, pageBottom As Integer, baseRowHeight As Integer) As Integer

        Dim pages As Integer = 1
        Dim y As Integer = firstRowY
        Dim printableBottom As Integer = pageBottom - 58
        Dim maxRowHeight As Integer = Math.Max(baseRowHeight, printableBottom - firstRowY)

        For Each row As DataGridViewRow In DataGridViewX.Rows
            If row.IsNewRow Then Continue For

            Dim rowHeight As Integer = CalculateGeneralReportRowHeight(graphics, row, columns, widths, rowFont, baseRowHeight)
            If rowHeight > maxRowHeight Then rowHeight = maxRowHeight

            If y + rowHeight > printableBottom AndAlso y > firstRowY Then
                pages += 1
                y = firstRowY
            End If

            y += rowHeight
        Next

        Return Math.Max(1, pages)

    End Function

    Private Function GetGeneralReportFilterText() As String

        Return "الفترة: من " & DateRange.D_F.Value.ToString("yyyy/MM/dd") &
            " إلى " & DateRange.D_T.Value.ToString("yyyy/MM/dd") &
            "    عدد السجلات: " & DataGridViewX.Rows.Count.ToString()

    End Function

    Private Sub DrawGeneralReportStoreHeader(graphics As Graphics, bounds As Rectangle, ByRef y As Integer, storeTitleFont As Font, storeSubTitleFont As Font, centerFormat As StringFormat)

        If Not String.IsNullOrWhiteSpace(SBill_Title_1) Then
            graphics.DrawString(SBill_Title_1, storeTitleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 30), centerFormat)
            y += 30
        End If

        If Not String.IsNullOrWhiteSpace(SBill_Title_2) Then
            graphics.DrawString(SBill_Title_2, storeSubTitleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 24), centerFormat)
            y += 24
        End If

    End Sub

    Private Sub DrawGeneralReportSummary(graphics As Graphics, bounds As Rectangle, y As Integer, totalFont As Font, centerFormat As StringFormat)

        If Not Panel1.Visible OrElse String.IsNullOrWhiteSpace(Total_Balance_txt.Text) Then Return

        Dim rowHeight As Integer = 26
        Dim summaryWidth As Integer = Math.Min(320, bounds.Width)
        Dim rect As New Rectangle(bounds.Right - summaryWidth, y, summaryWidth, rowHeight)

        Using totalBrush As New SolidBrush(Color.FromArgb(235, 240, 245))
            graphics.FillRectangle(totalBrush, rect)
        End Using

        graphics.DrawRectangle(Pens.LightGray, rect)
        graphics.DrawString("الإجمالي العام: " & Total_Balance_txt.Text, totalFont, Brushes.Black, rect, centerFormat)

    End Sub

    Private Function CalculatePrintableRowsPerPage(firstRowY As Integer, pageBottom As Integer, rowHeight As Integer) As Integer

        Dim availableHeight As Integer = pageBottom - 58 - firstRowY
        If availableHeight <= 0 Then Return 1

        Return Math.Max(1, CInt(Math.Floor(availableHeight / CDbl(rowHeight))))

    End Function

    Private Function CalculateTotalPrintPages(totalRows As Integer, rowsPerPage As Integer) As Integer

        If totalRows <= 0 OrElse rowsPerPage <= 0 Then Return 1

        Return Math.Max(1, CInt(Math.Ceiling(totalRows / CDbl(rowsPerPage))))

    End Function

    Private Function CalculateCurrentPrintPage(rowIndex As Integer, rowsPerPage As Integer) As Integer

        If rowsPerPage <= 0 Then Return 1

        Return Math.Max(1, (rowIndex \ rowsPerPage) + 1)

    End Function

    Private Sub DrawGeneralReportFooter(graphics As Graphics, bounds As Rectangle, currentPage As Integer, totalPages As Integer, footerFont As Font, centerFormat As StringFormat)

        If _printDateTime = DateTime.MinValue Then _printDateTime = Date.Now

        Dim footerTop As Integer = bounds.Bottom - 26
        Dim sideWidth As Integer = CInt(bounds.Width * 0.34)
        Dim centerWidth As Integer = bounds.Width - (sideWidth * 2)

        Using rightFormat As New StringFormat(),
              leftFormat As New StringFormat()

            rightFormat.Alignment = StringAlignment.Far
            rightFormat.LineAlignment = StringAlignment.Center
            rightFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft

            leftFormat.Alignment = StringAlignment.Near
            leftFormat.LineAlignment = StringAlignment.Center

            graphics.DrawLine(Pens.LightGray, bounds.Left, footerTop - 4, bounds.Right, footerTop - 4)
            graphics.DrawString("المعد: " & USER_NAME, footerFont, Brushes.Black, New Rectangle(bounds.Right - sideWidth, footerTop, sideWidth, 22), rightFormat)
            graphics.DrawString(currentPage.ToString() & "/" & totalPages.ToString(), footerFont, Brushes.Black, New Rectangle(bounds.Left + sideWidth, footerTop, centerWidth, 22), centerFormat)
            graphics.DrawString("تاريخ الطباعة: " & _printDateTime.ToString("yyyy/MM/dd HH:mm"), footerFont, Brushes.Black, New Rectangle(bounds.Left, footerTop, sideWidth, 22), leftFormat)
        End Using

    End Sub

    Private Function GetPdfPrinterName() As String

        For Each printerName As String In PrinterSettings.InstalledPrinters
            If printerName.IndexOf("Microsoft Print to PDF", StringComparison.OrdinalIgnoreCase) >= 0 Then Return printerName
        Next

        Return ""

    End Function

    Private Sub PdfButton_Click(sender As Object, e As EventArgs) Handles PdfButton.Click
        ExportGeneralReportPdf()
    End Sub

    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click
        If DataGridViewX.Rows.Count > 0 Then STORE_R_Print()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        GENERAL_REPORT_SELECT()
        'GENERAL_REPORT_SELECT_2()
        'GENERAL_REPORT_SELECT_3()
    End Sub

    Public Sub GENERAL_REPORT_SELECT()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[GENERAL_REPORT_SELECT]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", DateRange.D_F.Value)
            .Parameters.AddWithValue("@D_T", DateRange.D_T.Value)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        DataGridViewX.DataSource = C.Dt

    End Sub

    Private Sub DataGridViewX_DataBindingComplete(
    sender As Object,
    e As DataGridViewBindingCompleteEventArgs
) Handles DataGridViewX.DataBindingComplete

        If DataGridViewX.Columns.Count = 0 Then Exit Sub

        DataGridViewX.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        With DataGridViewX.Columns(0)
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 40
            .Resizable = DataGridViewTriState.False
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

        With DataGridViewX.Columns(1)
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 200
            .Resizable = DataGridViewTriState.False
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

        With DataGridViewX.Columns(2)
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 165
            .Resizable = DataGridViewTriState.False
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

        ' DataGridViewX.Columns(3).DefaultCellStyle.Font =
        'New Font(DataGridViewX.Font.FontFamily, 7, FontStyle.Regular)

    End Sub



    Public Sub GENERAL_REPORT_SELECT_2()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[GENERAL_REPORT_SELECT_2]"
            .CommandType = CommandType.StoredProcedure
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        DataGridViewX1.DataSource = C.Dt
    End Sub

    Public Sub GENERAL_REPORT_SELECT_3()

        Dim TOTAL_FIRST_TIME As Double = 0
        Dim TOTAL_LAST_TIME As Double = 0
        Dim Total_Bercent As Double = 0
        Dim TOTAL_DEBIT As Double = 0
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[GENERAL_REPORT_SELECT_3]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@TOTAL_BALANCE", 0)
            .Parameters.AddWithValue("@TOTAL_FIRST_TIME", 0)
            .Parameters.AddWithValue("@TOTAL_LAST_TIME", 0)
            .Parameters.AddWithValue("@TOTAL_DEBIT", 0)
            .Parameters("@TOTAL_BALANCE").Direction = ParameterDirection.Output
            .Parameters("@TOTAL_FIRST_TIME").Direction = ParameterDirection.Output
            .Parameters("@TOTAL_LAST_TIME").Direction = ParameterDirection.Output
            .Parameters("@TOTAL_DEBIT").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) = True Then
                Total_Balance_txt.Text = .Parameters("@TOTAL_BALANCE").Value.ToString()
                TOTAL_FIRST_TIME = .Parameters("@TOTAL_FIRST_TIME").Value.ToString()
                TOTAL_LAST_TIME = .Parameters("@TOTAL_LAST_TIME").Value.ToString()
                TOTAL_DEBIT = .Parameters("@TOTAL_DEBIT").Value.ToString()

                TOTAL_DEBIT *= -1

                Total_Balance_txt.Text = ((.Parameters("@TOTAL_BALANCE").Value) + (TOTAL_DEBIT)).ToString

                If Not String.IsNullOrWhiteSpace(Total_Balance_txt.Text) Then

                    TOTAL_FIRST_TIME = .Parameters("@TOTAL_FIRST_TIME").Value.ToString()
                    TOTAL_LAST_TIME = .Parameters("@TOTAL_LAST_TIME").Value.ToString()

                    Total_Bercent = (Convert.ToDouble(Total_Balance_txt.Text) / TOTAL_FIRST_TIME) * 100


                    'If Convert.ToDouble(Total_Balance_txt.Text) > 0 Then
                    '    Total_Balance_txt.BackColor = Color.LightGreen
                    '    Tag_lb.ForeColor = Color.DarkGreen
                    '    Tag_lb.Text = " ربــح بنسبة " + Total_Bercent.ToString("00.00") + " % "
                    '    Tag_lb.Visible = True
                    'ElseIf Convert.ToDouble(Total_Balance_txt.Text) = 0 Then
                    '    Total_Balance_txt.BackColor = SystemColors.ButtonHighlight
                    '    Tag_lb.Text = ""
                    '    Tag_lb.Visible = False
                    'Else
                    '    Total_Balance_txt.BackColor = Color.IndianRed
                    '    Tag_lb.ForeColor = Color.DarkRed
                    '    Tag_lb.Text = " خســـارة بنسبة " + Total_Bercent.ToString("00.00") + " % "
                    '    Tag_lb.Visible = True
                    'End If
                End If

            End If
        End With

    End Sub





    Private Sub Total_Balance_txt_TextChanged(sender As Object, e As EventArgs) Handles Total_Balance_txt.TextChanged
        money_char_txtb.Text = HANY(Val(Total_Balance_txt.Text), "EGYPT")
    End Sub

    Private Sub HeaderCloseBtn_Click(sender As Object, e As EventArgs) Handles HeaderCloseBtn.Click
        Me.Close()
    End Sub

    Private Sub MaxFormButton_Click(sender As Object, e As EventArgs) Handles MaxFormButton.Click
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
            MaxFormButton.Text = "⬜"
        Else
            Me.WindowState = FormWindowState.Maximized
            MaxFormButton.Text = "🗗"
        End If
    End Sub
End Class
