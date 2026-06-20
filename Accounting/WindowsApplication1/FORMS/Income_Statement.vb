Public Class Income_Statement
    Private _currentTemplateID As Integer = 0
    Private WithEvents PD As New System.Drawing.Printing.PrintDocument
    Private PPD As New PrintPreviewDialog
    Private CurrentPrintLandscape As Boolean = True
    Private CurrentPrintRow As Integer = 0
    Private PrintPageNumber As Integer = 1
    Private PrintTotalPages As Integer = 1
    Private PrintableRows As New List(Of DataGridViewRow)

    Private Sub Search_btn_Click(sender As Object, e As EventArgs) Handles Search_btn.Click



        'GET_TREE_BALANCE(0, DateRange_Flate1.D_F.Value, DateRange_Flate1.D_T.Value, 0)

        SELECT_Balance_sheet()
    End Sub


    Private Async Sub SELECT_Balance_sheet()

        '     query("EXEC [Prepare_Income_Sheet] ")


        '     Dim DT As New DataTable
        '     Dim C As New C
        '     Dim da As New SqlClient.SqlDataAdapter("SELECT
        '    [ACC_CODE]
        '   ,[Group_Name]
        '   ,[Result_Title]
        '   ,[ACC_NAME]
        '   ,[Sign]
        '   ,[Original_Balance]
        '   ,[Total_Income]
        'FROM Tmp_IncomeReport_2 ", C.Con)
        '     da.Fill(DT)
        '     DataGridView1.DataSource = DT

        '     DataGridView1.Columns(0).Visible = False

        '     DataGridView1.Columns(5).DefaultCellStyle.Format = "N3"
        '     DataGridView1.Columns(6).DefaultCellStyle.Format = "N3"


        If _currentTemplateID <= 0 Then
            MessageBox.Show("يرجى اختيار قالب قائمة الدخل أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        CircularPanel.Visible = True
        CircularProgressControl1.Start()

        Try
            DataGridView1.DataSource = Await Task(Of DataTable).Run(Function() LoadIncomeStatementTemplateReport())
            FormatFinalReportGrid()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CircularPanel.Visible = False
            CircularProgressControl1.Stop()
        End Try



        'C.Da = New SqlClient.SqlDataAdapter(C.Com)
        'C.Da.Fill(C.Dt)
        'DataGridView1.DataSource = C.Dt

    End Sub


    Private Sub Coloring()

        Dim A = 0
        For i = 0 To DataGridView1.Rows.Count - 1



            If Not IsDBNull(DataGridView1.Rows(i).Cells("ACC_CODE_CL").Value) And Not IsDBNull(DataGridView1.Rows(i).Cells("ACC_PARENT_CL").Value) Then

                Dim ACC_CODE_VALUE As Integer = Convert.ToInt32(DataGridView1.Rows(i).Cells("ACC_CODE_CL").Value)
                Dim ACC_PARENT_VALUE As Integer = Convert.ToInt32(DataGridView1.Rows(i).Cells("ACC_PARENT_CL").Value)

                If ACC_CODE_VALUE = 0 And ACC_PARENT_VALUE = 0 Then
                    If A = 0 Then
                        Me.DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192)
                        A += 1
                    Else
                        Me.DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 192)
                    End If

                    'Me.DataGridView1.Rows(i).Cells("ACC_CODE_CL").Value = vbNull
                    'Me.DataGridView1.Rows(i).Cells("ACC_PARENT_CL").Value = vbNull

                ElseIf ACC_CODE_VALUE <> 0 And ACC_PARENT_VALUE = 0 Then
                    Me.DataGridView1.Rows(i).DefaultCellStyle.BackColor = Drawing.Color.LightGray
                End If

            End If


        Next
    End Sub


    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        ' Check if the column is "BALANCE_CL"
        If DataGridView1.Columns(e.ColumnIndex).Name = "BALANCE_CL" Then
            ' Check the value of the cell
            If e.Value IsNot Nothing Then
                Dim balanceValue As Decimal
                ' Attempt to parse the value to Decimal
                If Decimal.TryParse(e.Value.ToString(), balanceValue) Then
                    ' Set ForeColor based on the value
                    If balanceValue < 0 Then
                        e.CellStyle.ForeColor = Color.DarkRed  ' Negative balance
                    ElseIf balanceValue > 0 Then
                        e.CellStyle.ForeColor = Color.DarkGreen  ' Positive balance
                    End If
                End If
            End If
        End If
    End Sub


    Private Sub Balance_sheet_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DateRange_Flate1.FYear_Txt.Text = Identifiers.F_YEAR
        BuildTemplateSelector()
        PreparePrintMenu()
        LoadTemplates()

        DataGridView1.DefaultCellStyle.SelectionBackColor = DataGridView1.DefaultCellStyle.BackColor
        DataGridView1.DefaultCellStyle.SelectionForeColor = DataGridView1.DefaultCellStyle.ForeColor

        DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None
        DataGridView1.BorderStyle = BorderStyle.None
    End Sub

    Private Sub BuildTemplateSelector()
        Template_Lbl.BringToFront()
        Template_Cm.BringToFront()
    End Sub

    Private Sub PreparePrintMenu()
        Print_Btn.DropDownMenu = Print_CntxtMStrip

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

    Private Sub LoadTemplates()
        Dim sql As String =
            "SELECT TemplateID, TemplateName " &
            "FROM dbo.IncomeStatementTemplates " &
            "WHERE IsActive = 1 " &
            "ORDER BY IsDefault DESC, TemplateID DESC;"

        Dim dt As DataTable = ExecuteDataTable(sql, Nothing, False)

        Template_Cm.DataSource = dt
        Template_Cm.DisplayMember = "TemplateName"
        Template_Cm.ValueMember = "TemplateID"

        If Template_Cm.Items.Count > 0 Then
            Template_Cm.SelectedIndex = 0
            Template_Cm_SelectedIndexChanged(Template_Cm, EventArgs.Empty)
        End If
    End Sub

    Private Sub Template_Cm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Template_Cm.SelectedIndexChanged
        If Template_Cm Is Nothing Then Return
        If Template_Cm.SelectedValue Is Nothing OrElse Not IsNumeric(Template_Cm.SelectedValue) Then Return

        _currentTemplateID = CInt(Template_Cm.SelectedValue)
    End Sub

    Private Function LoadIncomeStatementTemplateReport() As DataTable
        Dim dt As DataTable = ExecuteDataTable(
            "dbo.ACC_IncomeStatement_Report",
            New List(Of SqlClient.SqlParameter) From {
                New SqlClient.SqlParameter("@TemplateID", _currentTemplateID),
                New SqlClient.SqlParameter("@DateFrom", DateRange_Flate1.D_F.Value.Date),
                New SqlClient.SqlParameter("@DateTo", DateRange_Flate1.D_T.Value.Date),
                New SqlClient.SqlParameter("@HideZero", Hide_Zeros_CB.Checked),
                New SqlClient.SqlParameter("@ShowHeader", True)
            })

        ClearTitleRowsAmounts(dt)
        RemoveRowsWithoutCode(dt)
        Return dt
    End Function

    Private Sub ClearTitleRowsAmounts(dt As DataTable)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return

        For Each r As DataRow In dt.Rows
            If IsTitleRow(r) Then
                ClearColumnValue(r, "Amount", DBNull.Value)
                ClearColumnValue(r, "DisplayAmount", DBNull.Value)
                ClearColumnValue(r, "DisplayAmountText", "")
            End If
        Next
    End Sub

    Private Function IsTitleRow(row As DataRow) As Boolean
        If row Is Nothing OrElse row.Table Is Nothing Then Return False

        If row.Table.Columns.Contains("LineType") AndAlso SafeInt(row("LineType")) = 1 Then
            Return True
        End If

        If row.Table.Columns.Contains("LineTypeName") AndAlso SafeString(row("LineTypeName")).Contains("عنوان") Then
            Return True
        End If

        If row.Table.Columns.Contains("IsTitle") AndAlso SafeBool(row("IsTitle")) Then
            Return True
        End If

        Return False
    End Function

    Private Sub ClearColumnValue(row As DataRow, columnName As String, value As Object)
        If row.Table.Columns.Contains(columnName) Then
            row(columnName) = value
        End If
    End Sub

    Private Function SafeInt(value As Object) As Integer
        If value Is Nothing OrElse value Is DBNull.Value Then Return 0

        Dim result As Integer
        If Integer.TryParse(value.ToString(), result) Then Return result

        Return 0
    End Function

    Private Function SafeString(value As Object) As String
        If value Is Nothing OrElse value Is DBNull.Value Then Return ""
        Return value.ToString()
    End Function

    Private Function SafeBool(value As Object) As Boolean
        If value Is Nothing OrElse value Is DBNull.Value Then Return False

        Dim result As Boolean
        If Boolean.TryParse(value.ToString(), result) Then Return result

        Dim i As Integer
        If Integer.TryParse(value.ToString(), i) Then Return i <> 0

        Return False
    End Function

    Private Sub RemoveRowsWithoutCode(dt As DataTable)
        If dt Is Nothing OrElse Not dt.Columns.Contains("LineCode") Then Return

        For i As Integer = dt.Rows.Count - 1 To 0 Step -1
            Dim code As String = CleanCodeValue(dt.Rows(i)("LineCode"))

            If String.IsNullOrWhiteSpace(code) Then
                dt.Rows.RemoveAt(i)
            End If
        Next

        If Not dt.Columns.Contains("RowNo") Then Return

        For i As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(i)("RowNo") = i + 1
        Next
    End Sub

    Private Function CleanCodeValue(value As Object) As String
        If value Is Nothing OrElse value Is DBNull.Value Then Return ""

        Return value.ToString().
                     Replace(ChrW(160), "").
                     Replace(ChrW(8203), "").
                     Replace(ChrW(65279), "").
                     Trim()
    End Function

    Private Function ExecuteDataTable(queryOrProcedure As String,
                                      Optional parameters As List(Of SqlClient.SqlParameter) = Nothing,
                                      Optional isStoredProcedure As Boolean = True) As DataTable
        Using con As New SqlClient.SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlClient.SqlCommand(queryOrProcedure, con)
                cmd.CommandTimeout = 120
                cmd.CommandType = If(isStoredProcedure, CommandType.StoredProcedure, CommandType.Text)

                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters.ToArray())
                End If

                Using da As New SqlClient.SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    Return dt
                End Using
            End Using
        End Using
    End Function

    Private Sub FormatFinalReportGrid()
        If DataGridView1.DataSource Is Nothing Then Return

        DataGridView1.ColumnHeadersVisible = True
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        HideColumnIfExists("TemplateID")
        HideColumnIfExists("ParentLineID")
        HideColumnIfExists("LineID")
        HideColumnIfExists("LineType")
        HideColumnIfExists("SortOrder")
        HideColumnIfExists("SortPath")
        HideColumnIfExists("Amount")
        HideColumnIfExists("DisplayAmount")
        HideColumnIfExists("TextAlign")
        HideColumnIfExists("AmountAlign")
        HideColumnIfExists("TemplateName")
        HideColumnIfExists("DateFrom")
        HideColumnIfExists("DateTo")
        HideColumnIfExists("IsBold")
        HideColumnIfExists("IsFormula")
        HideColumnIfExists("IsSeparator")
        HideColumnIfExists("IsNegative")
        HideColumnIfExists("IsZero")
        HideColumnIfExists("FontSize")
        HideColumnIfExists("LevelNo")
        HideColumnIfExists("IsTitle")
        HideColumnIfExists("LineTypeName")
        HideColumnIfExists("LineName")

        SetHeader("RowNo", "م")
        SetHeader("TemplateName", "القالب")
        SetHeader("DateFrom", "من تاريخ")
        SetHeader("DateTo", "إلى تاريخ")
        SetHeader("LineCode", "الكود")
        SetHeader("LineName", "البند")
        SetHeader("DisplayLineName", "البيان")
        SetHeader("LineTypeName", "النوع")
        SetHeader("LevelNo", "المستوى")
        SetHeader("DisplayAmountText", "المبلغ")
        SetHeader("IsBold", "عريض")
        SetHeader("IsTitle", "عنوان")
        SetHeader("IsFormula", "معادلة")
        SetHeader("IsSeparator", "فاصل")
        SetHeader("IsNegative", "سالب")
        SetHeader("IsZero", "صفر")
        SetHeader("FontSize", "حجم الخط")

        If DataGridView1.Columns.Contains("RowNo") Then
            DataGridView1.Columns("RowNo").Width = 45
            DataGridView1.Columns("RowNo").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End If

        If DataGridView1.Columns.Contains("DisplayLineName") Then
            DataGridView1.Columns("DisplayLineName").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If

        If DataGridView1.Columns.Contains("DisplayAmountText") Then
            DataGridView1.Columns("DisplayAmountText").Width = 140
            DataGridView1.Columns("DisplayAmountText").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            DataGridView1.Columns("DisplayAmountText").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

        ApplyFinalReportStyle()
        HideFirstDescriptionRowAndRenumber()
    End Sub

    Private Sub ApplyFinalReportStyle()
        If DataGridView1.Rows.Count = 0 Then Return

        For Each r As DataGridViewRow In DataGridView1.Rows
            If r.IsNewRow Then Continue For

            Dim isBold As Boolean = GetGridBool(r, "IsBold")
            Dim isTitle As Boolean = GetGridBool(r, "IsTitle")
            Dim isFormula As Boolean = GetGridBool(r, "IsFormula")
            Dim isSeparator As Boolean = GetGridBool(r, "IsSeparator")
            Dim isNegative As Boolean = GetGridBool(r, "IsNegative")
            Dim fontSize As Integer = GetGridInt(r, "FontSize", 9)

            Dim styleFont As FontStyle = FontStyle.Regular

            If isBold OrElse isTitle OrElse isFormula Then
                styleFont = FontStyle.Bold
            End If

            r.DefaultCellStyle.Font = New Font(DataGridView1.Font.FontFamily, fontSize, styleFont)

            If isTitle Then
                r.DefaultCellStyle.BackColor = Color.AliceBlue
            End If

            If isFormula Then
                r.DefaultCellStyle.BackColor = Color.Honeydew
            End If

            If isSeparator Then
                r.Height = 8
                r.DefaultCellStyle.BackColor = Color.LightGray
            End If

            If isNegative AndAlso DataGridView1.Columns.Contains("DisplayAmountText") Then
                r.Cells("DisplayAmountText").Style.ForeColor = Color.DarkRed
            End If
        Next
    End Sub

    Private Sub SetHeader(columnName As String, headerText As String)
        If DataGridView1.Columns.Contains(columnName) Then
            DataGridView1.Columns(columnName).HeaderText = headerText
        End If
    End Sub

    Private Sub HideColumnIfExists(columnName As String)
        If DataGridView1.Columns.Contains(columnName) Then
            DataGridView1.Columns(columnName).Visible = False
        End If
    End Sub

    Private Sub HideFirstDescriptionRowAndRenumber()
        If DataGridView1.Rows.Count = 0 Then Return

        DataGridView1.CurrentCell = Nothing

        For Each r As DataGridViewRow In DataGridView1.Rows
            If r.IsNewRow Then Continue For

            If ShouldHideDescriptionRow(r) Then
                r.Visible = False
            End If
        Next

        If Not DataGridView1.Columns.Contains("RowNo") Then Return

        Dim rowNo As Integer = 1

        For Each r As DataGridViewRow In DataGridView1.Rows
            If r.IsNewRow OrElse Not r.Visible Then Continue For

            r.Cells("RowNo").Value = rowNo
            rowNo += 1
        Next
    End Sub

    Private Function ShouldHideDescriptionRow(row As DataGridViewRow) As Boolean
        Dim lineType As String = GetCellString(row, "LineTypeName")
        Dim displayLine As String = GetCellString(row, "DisplayLineName")
        Dim lineName As String = GetCellString(row, "LineName")
        Dim lineCode As String = GetCellString(row, "LineCode")
        Dim amountText As String = GetCellString(row, "DisplayAmountText")

        If String.IsNullOrWhiteSpace(lineCode) Then Return True
        If lineType.Contains("عنوان التقرير") OrElse lineType.Contains("عنوان الفترة") Then Return True
        If Not String.IsNullOrWhiteSpace(Template_Cm.Text) AndAlso displayLine.Trim() = Template_Cm.Text.Trim() Then Return True
        If Not String.IsNullOrWhiteSpace(Template_Cm.Text) AndAlso lineName.Trim() = Template_Cm.Text.Trim() Then Return True
        If displayLine.Contains("الفترة من") OrElse lineName.Contains("الفترة من") Then Return True

        If String.IsNullOrWhiteSpace(lineCode) AndAlso String.IsNullOrWhiteSpace(amountText) Then
            If displayLine.Contains("قائمة") OrElse lineName.Contains("قائمة") Then Return True
        End If

        Return False
    End Function

    Private Function GetCellString(row As DataGridViewRow, columnName As String) As String
        If row.DataGridView Is Nothing OrElse Not row.DataGridView.Columns.Contains(columnName) Then Return ""
        If row.Cells(columnName).Value Is Nothing OrElse row.Cells(columnName).Value Is DBNull.Value Then Return ""

        Return row.Cells(columnName).Value.ToString()
    End Function

    Private Function GetGridBool(row As DataGridViewRow, columnName As String) As Boolean
        If Not row.DataGridView.Columns.Contains(columnName) Then Return False
        If row.Cells(columnName).Value Is Nothing OrElse row.Cells(columnName).Value Is DBNull.Value Then Return False

        Dim b As Boolean
        If Boolean.TryParse(row.Cells(columnName).Value.ToString(), b) Then Return b

        Dim i As Integer
        If Integer.TryParse(row.Cells(columnName).Value.ToString(), i) Then Return i <> 0

        Return False
    End Function

    Private Function GetGridInt(row As DataGridViewRow, columnName As String, defaultValue As Integer) As Integer
        If Not row.DataGridView.Columns.Contains(columnName) Then Return defaultValue
        If row.Cells(columnName).Value Is Nothing OrElse row.Cells(columnName).Value Is DBNull.Value Then Return defaultValue

        Dim i As Integer
        If Integer.TryParse(row.Cells(columnName).Value.ToString(), i) Then Return i

        Return defaultValue
    End Function


    Private Sub Print_Btn_Click(sender As Object, e As EventArgs)
        Print_B()
    End Sub


    Public Sub Print_B(Optional ByVal exportToExcel As Boolean = False)

        If Not exportToExcel Then
            If DataGridView1.Rows.Count = 0 OrElse DataGridView1.DataSource Is Nothing Then
                MessageBox.Show("لا توجد بيانات للطباعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            PreparePrint()
            PPD.Document = PD
            PPD.WindowState = FormWindowState.Maximized
            PPD.ShowDialog()
            Exit Sub
        End If

        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\Reports\Income_Statement.rpt")
        pp.LoadTables()
        With pp

            .rp.SetParameterValue("TITLE_NUM", " قائمـــــة الدخــــل " & vbNewLine & "( للفترة من " & DateRange_Flate1.D_F.Value & " إلى " & DateRange_Flate1.D_T.Value & " ) ")
            .rp.SetParameterValue("Title_1", MY_Settings.SBill_Title_1)
            .rp.SetParameterValue("Title_2", MY_Settings.SBill_Title_2)
            .rp.SetParameterValue("USER_Printer", USER_NAME)

        End With


        ' **تصدير التقرير إلى Excel بدلاً من الطباعة إذا تم اختيار التصدير**
        If exportToExcel Then
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "Excel Files|*.xls"
            saveDialog.Title = "حفظ التقرير كملف Excel"
            saveDialog.FileName = "قائمة الدخل.xls"

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

    Private Sub PreparePrint()
        CurrentPrintRow = 0
        PrintPageNumber = 1
        PrintTotalPages = 1
        PrintableRows.Clear()

        PD.DefaultPageSettings.Landscape = CurrentPrintLandscape
        PD.DefaultPageSettings.Margins = New System.Drawing.Printing.Margins(30, 30, 35, 35)

        For Each r As DataGridViewRow In DataGridView1.Rows
            If r.IsNewRow OrElse Not r.Visible Then Continue For
            PrintableRows.Add(r)
        Next

        PrintTotalPages = EstimatePrintPages()
    End Sub

    Private Sub PD_PrintPage(sender As Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles PD.PrintPage
        Dim g = e.Graphics
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        Dim marginLeft As Integer = e.MarginBounds.Left
        Dim marginRight As Integer = e.MarginBounds.Right
        Dim y As Integer = e.MarginBounds.Top
        Dim pageWidth As Integer = e.MarginBounds.Width

        Dim companyFont As New Font("Tahoma", 12, FontStyle.Bold)
        Dim titleFont As New Font("Tahoma", 15, FontStyle.Bold)
        Dim subTitleFont As New Font("Tahoma", 10, FontStyle.Bold)
        Dim headerFont As New Font("Tahoma", If(CurrentPrintLandscape, 9.5!, 8.5!), FontStyle.Bold)
        Dim bodyFont As New Font("Tahoma", If(CurrentPrintLandscape, 9.0!, 8.25!), FontStyle.Regular)
        Dim footerFont As New Font("Tahoma", 9, FontStyle.Bold)

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

        Dim sfStatement As New StringFormat With {
            .Alignment = StringAlignment.Near,
            .LineAlignment = StringAlignment.Center,
            .FormatFlags = StringFormatFlags.DirectionRightToLeft
        }

        g.DrawString(MY_Settings.SBill_Title_1, companyFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 24), sfRight)
        y += 24
        g.DrawString(MY_Settings.SBill_Title_2, companyFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 24), sfRight)
        y += 28
        g.DrawLine(Pens.Black, marginLeft, y, marginRight, y)
        y += 8

        g.DrawString("قائمــــة الدخــــل", titleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 28), sfCenter)
        y += 30
        g.DrawString("القالب: " & If(Template_Cm Is Nothing, "", Template_Cm.Text), subTitleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 22), sfCenter)
        y += 22
        g.DrawString("من " & DateRange_Flate1.D_F.Value.ToString("dd/MM/yyyy") & " إلى " & DateRange_Flate1.D_T.Value.ToString("dd/MM/yyyy"), subTitleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 22), sfCenter)
        y += 24
        g.DrawString("صفحة " & PrintPageNumber.ToString() & " من " & PrintTotalPages.ToString(), bodyFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 18), sfLeft)
        y += 24

        Dim colWidths = GetIncomePrintColumnWidths(pageWidth)
        DrawIncomePrintHeader(g, marginLeft, y, colWidths, headerFont, sfCenter)
        y += 32

        While CurrentPrintRow < PrintableRows.Count
            Dim row As DataGridViewRow = PrintableRows(CurrentPrintRow)
            Dim rowHeight As Integer = EstimateIncomeRowHeight(g, row, bodyFont, colWidths(2))

            If y + rowHeight > e.MarginBounds.Bottom - 85 Then
                e.HasMorePages = True
                PrintPageNumber += 1
                Return
            End If

            DrawIncomeRow(g, row, marginLeft, y, rowHeight, colWidths, bodyFont, sfCenter, sfStatement)
            y += rowHeight
            CurrentPrintRow += 1
        End While

        y += 8
        DrawIncomeFooter(g, marginLeft, y, pageWidth, footerFont, sfCenter)

        e.HasMorePages = False
        CurrentPrintRow = 0
        PrintPageNumber = 1
    End Sub

    Private Function GetIncomePrintColumnWidths(pageWidth As Integer) As Integer()
        Dim amountWidth As Integer = GetGridColumnWidth("DisplayAmountText")
        Dim statementWidth As Integer = GetGridColumnWidth("DisplayLineName")
        Dim codeWidth As Integer = GetGridColumnWidth("LineCode")
        Dim rowNoWidth As Integer = GetGridColumnWidth("RowNo")
        Dim totalGridWidth As Integer = amountWidth + statementWidth + codeWidth + rowNoWidth

        If totalGridWidth > 0 Then
            Return {
                CInt((amountWidth / totalGridWidth) * pageWidth),
                CInt((statementWidth / totalGridWidth) * pageWidth),
                CInt((codeWidth / totalGridWidth) * pageWidth),
                pageWidth - CInt((amountWidth / totalGridWidth) * pageWidth) - CInt((statementWidth / totalGridWidth) * pageWidth) - CInt((codeWidth / totalGridWidth) * pageWidth)
            }
        End If

        Return {
            CInt(pageWidth * 0.12),
            CInt(pageWidth * 0.55),
            CInt(pageWidth * 0.25),
            CInt(pageWidth * 0.08)
        }
    End Function

    Private Function GetGridColumnWidth(columnName As String) As Integer
        If DataGridView1.Columns.Contains(columnName) AndAlso DataGridView1.Columns(columnName).Visible Then
            Return DataGridView1.Columns(columnName).Width
        End If

        Return 0
    End Function

    Private Sub DrawIncomePrintHeader(g As Graphics, x As Integer, y As Integer, colWidths As Integer(), headerFont As Font, sfCenter As StringFormat)
        Dim headers() As String = {"المبلغ", "البيان", "الكود", "م"}
        Dim currentX As Integer = x

        For i As Integer = 0 To headers.Length - 1
            Dim rect As New Rectangle(currentX, y, colWidths(i), 32)
            g.FillRectangle(New SolidBrush(Color.FromArgb(225, 225, 225)), rect)
            g.DrawRectangle(New Pen(Color.FromArgb(80, 80, 80)), rect)
            g.DrawString(headers(i), headerFont, Brushes.Black, New RectangleF(rect.X, rect.Y, rect.Width, rect.Height), sfCenter)
            currentX += colWidths(i)
        Next
    End Sub

    Private Sub DrawIncomeRow(g As Graphics, row As DataGridViewRow, x As Integer, y As Integer, rowHeight As Integer, colWidths As Integer(), bodyFont As Font, sfCenter As StringFormat, sfRight As StringFormat)
        Dim values() As String = {
            GetCellString(row, "DisplayAmountText"),
            GetCellString(row, "DisplayLineName"),
            GetCellString(row, "LineCode"),
            GetCellString(row, "RowNo")
        }

        Dim currentX As Integer = x
        Dim bgColor As Color = Color.White

        If GetGridBool(row, "IsTitle") Then bgColor = Color.AliceBlue
        If GetGridBool(row, "IsFormula") Then bgColor = Color.Honeydew
        If CurrentPrintRow Mod 2 = 1 AndAlso bgColor = Color.White Then bgColor = Color.FromArgb(250, 250, 250)

        Dim useFontStyle As FontStyle = If(GetGridBool(row, "IsBold") OrElse GetGridBool(row, "IsFormula"), FontStyle.Bold, FontStyle.Regular)
        Dim useFont As New Font(bodyFont.FontFamily, bodyFont.Size, useFontStyle)

        For i As Integer = 0 To values.Length - 1
            Dim rect As New Rectangle(currentX, y, colWidths(i), rowHeight)
            g.FillRectangle(New SolidBrush(bgColor), rect)
            g.DrawRectangle(New Pen(Color.FromArgb(150, 150, 150)), rect)

            Dim fmt As StringFormat = If(i = 1, sfRight, sfCenter)
            Dim brush As Brush = Brushes.Black
            If i = 0 AndAlso GetGridBool(row, "IsNegative") Then brush = Brushes.DarkRed

            g.DrawString(values(i), useFont, brush, New RectangleF(rect.X + 5, rect.Y + 2, rect.Width - 10, rect.Height - 4), fmt)
            currentX += colWidths(i)
        Next
    End Sub

    Private Function EstimateIncomeRowHeight(g As Graphics, row As DataGridViewRow, bodyFont As Font, statementWidth As Integer) As Integer
        Dim text As String = GetCellString(row, "DisplayLineName")
        Dim h As Integer = CInt(g.MeasureString(text, bodyFont, statementWidth - 10).Height) + 12
        If h < 30 Then h = 30
        Return h
    End Function

    Private Sub DrawIncomeFooter(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, footerFont As Font, sfCenter As StringFormat)
        Dim boxWidth As Integer = CInt(pageWidth / 3)
        Dim boxHeight As Integer = 30

        g.DrawLine(Pens.Black, x, y, x + pageWidth, y)
        y += 6

        DrawFooterBoxesRow(g, x, y, pageWidth, boxWidth, boxHeight,
                           {"صافي الدخل", "المعد", "تاريخ الطباعة"},
                           {GetNetIncomeText(), USER_NAME, Date.Now.ToString("dd/MM/yyyy HH:mm")},
                           footerFont, sfCenter)
    End Sub

    Private Sub DrawFooterBoxesRow(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, boxWidth As Integer, boxHeight As Integer, titles() As String, values() As String, footerFont As Font, sfCenter As StringFormat)
        Dim currentX As Integer = x + pageWidth

        For i As Integer = 0 To titles.Length - 1
            currentX -= boxWidth
            Dim rect As New Rectangle(currentX, y, boxWidth, boxHeight)
            g.FillRectangle(New SolidBrush(Color.FromArgb(245, 245, 245)), rect)
            g.DrawRectangle(Pens.Black, rect)
            g.DrawString(titles(i) & ": " & values(i), footerFont, Brushes.Black, New RectangleF(rect.X + 5, rect.Y, rect.Width - 10, rect.Height), sfCenter)
        Next
    End Sub

    Private Function GetNetIncomeText() As String
        For Each row As DataGridViewRow In PrintableRows
            If String.Equals(GetCellString(row, "LineCode"), "NET_INCOME", StringComparison.OrdinalIgnoreCase) Then
                Return GetCellString(row, "DisplayAmountText")
            End If
        Next

        If PrintableRows.Count > 0 Then
            Return GetCellString(PrintableRows(PrintableRows.Count - 1), "DisplayAmountText")
        End If

        Return ""
    End Function

    Private Function EstimatePrintPages() As Integer
        Using bmp As New Bitmap(10, 10)
            Using g As Graphics = Graphics.FromImage(bmp)
                Dim bodyFont As New Font("Tahoma", If(CurrentPrintLandscape, 9.0!, 8.25!), FontStyle.Regular)
                Dim pageHeight As Integer
                Dim pageWidth As Integer

                If CurrentPrintLandscape Then
                    pageHeight = PD.DefaultPageSettings.Bounds.Width - PD.DefaultPageSettings.Margins.Top - PD.DefaultPageSettings.Margins.Bottom
                    pageWidth = PD.DefaultPageSettings.Bounds.Height - PD.DefaultPageSettings.Margins.Left - PD.DefaultPageSettings.Margins.Right
                Else
                    pageHeight = PD.DefaultPageSettings.Bounds.Height - PD.DefaultPageSettings.Margins.Top - PD.DefaultPageSettings.Margins.Bottom
                    pageWidth = PD.DefaultPageSettings.Bounds.Width - PD.DefaultPageSettings.Margins.Left - PD.DefaultPageSettings.Margins.Right
                End If

                Dim colWidths = GetIncomePrintColumnWidths(pageWidth)
                Dim usableHeight As Integer = pageHeight - 235
                Dim y As Integer = 0
                Dim pages As Integer = 1

                For Each row As DataGridViewRow In PrintableRows
                    Dim h As Integer = EstimateIncomeRowHeight(g, row, bodyFont, colWidths(2))

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

    Private Sub DataGridView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseDoubleClick
        If DataGridView1.Rows.Count > 0 Then
            Dim F As New Income_Statement_Contents
            F.ACC_CODE = DataGridView1.CurrentRow.Cells(0).Value
            F.Text = " محتويات الحساب: " & DataGridView1.CurrentRow.Cells(3).Value
            F.ShowDialog()
        End If
    End Sub

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
