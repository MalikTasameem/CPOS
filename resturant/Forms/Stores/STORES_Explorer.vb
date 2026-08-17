Imports System.Data.SqlClient
Imports System.Drawing.Printing
'Imports Microsoft.Win32
Public Class STORES_Explorer
    'Dim rs As New Resizer
    Dim IM_DT As New DataTable
    Dim Get_IM As Boolean = False
    Private _printRowIndex As Integer = 0
    Private _printDateTime As DateTime
    Private WithEvents PdfButton As Button

    Public is_Update_Valid_D As Boolean = False
    Public is_Update_QTY As Boolean = False
    Public is_Update_Unit_Cost As Boolean = False

    Private Sub STORES_Explorer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        '   F_MainForm.Fill_ALL_IM()
        Me.Dispose()
    End Sub
    Private Sub STORES_Explorer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. إعداد التكبير الديناميكي وإيقاف التحجيم التلقائي
        SetupAnchors()
        SetupPdfButton()

        ' 2. تطبيق الثيم الإجباري 
        Try
            ThemeManager.ApplyThemeToForm(Me)
        Catch ex As Exception
        End Try
        ' 'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        'rs.FindAllControls(Me)
        '' Me.WindowState = FormWindowState.Maximized
        fetch_ST()
        fetch_GM()
        Get_IM = True
        Fill_ALL_IM(0)
        Make_Hints()
        ' Recover_STORES_Explorer_File_Setting(CheckedListBox1)
        'Check_View_Control()
        GM_Serach.Select()
    End Sub
    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub MaxFormButton_Click(sender As Object, e As EventArgs) Handles MaxFormButton.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.MaximumSize = Screen.FromHandle(Me.Handle).WorkingArea.Size
            Me.WindowState = FormWindowState.Maximized
            MaxFormButton.Text = "❐"
        Else
            Me.WindowState = FormWindowState.Normal
            MaxFormButton.Text = "⬜"
        End If
        '   Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub MinFormButton_Click(sender As Object, e As EventArgs) Handles MinFormButton.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    ' --- حركة الفورم (السحب والإفلات) ---
    Private drag As Boolean
    Private mouseX As Integer
    Private mouseY As Integer

    Private Sub TitleBar_Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseDown, Title_LB.MouseDown
        drag = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub TitleBar_Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseMove, Title_LB.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub

    Private Sub TitleBar_Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseUp, Title_LB.MouseUp
        drag = False
    End Sub


    ' ==========================================
    ' 🌟 نظام التكبير الديناميكي (بديل الريسايزر) 🌟
    ' ==========================================
    Private Sub SetupAnchors()
        Me.AutoScaleMode = AutoScaleMode.None
        Me.DoubleBuffered = True

        ' 1. الجريدات الأساسية
        'If grid_panel IsNot Nothing Then grid_panel.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        If gridv IsNot Nothing Then gridv.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        ' 2. الأجزاء السفلية
        '  If Panel3 IsNot Nothing Then Panel3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        If textBox_total IsNot Nothing Then textBox_total.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        If Label3 IsNot Nothing Then Label3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        If TOTAL_Grid IsNot Nothing Then TOTAL_Grid.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left

        ' 3. شريط البحث والأجزاء العلوية
        'If Panel2 IsNot Nothing Then Panel2.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        '' فلاتر البحث (يمين)
        'If ST_cm IsNot Nothing Then ST_cm.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        'If GM_Serach IsNot Nothing Then GM_Serach.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        'If Print_type_Cmb IsNot Nothing Then Print_type_Cmb.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        'If IMNUM_CB IsNot Nothing Then IMNUM_CB.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        'If BarcodeSearch_CB IsNot Nothing Then BarcodeSearch_CB.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        'If Show_only_Zero_CB IsNot Nothing Then Show_only_Zero_CB.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        'If Label9 IsNot Nothing Then Label9.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        'If Label4 IsNot Nothing Then Label4.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        'If CMSearchTextBox IsNot Nothing Then CMSearchTextBox.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        'If CheckedListBox1 IsNot Nothing Then CheckedListBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        ' أزرار العمليات العلوية (يسار)
        'If EXCEL_BTN IsNot Nothing Then EXCEL_BTN.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        'If Button1 IsNot Nothing Then Button1.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        ''   If Button2 IsNot Nothing Then Button2.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        'If PrintButton IsNot Nothing Then PrintButton.Anchor = AnchorStyles.Top Or AnchorStyles.Left

        ' أزرار العمليات السفلية (يسار)
        If Recount_Cost_btn IsNot Nothing Then Recount_Cost_btn.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        If Up_Update_btn IsNot Nothing Then Up_Update_btn.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        If IM_btn IsNot Nothing Then IM_btn.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
    End Sub

    Private Sub SetupPdfButton()

        If PdfButton IsNot Nothing Then Return

        PdfButton = New Button With {
            .Anchor = PrintButton.Anchor,
            .BackColor = PrintButton.BackColor,
            .Cursor = Cursors.Hand,
            .FlatStyle = PrintButton.FlatStyle,
            .Font = PrintButton.Font,
            .ForeColor = PrintButton.ForeColor,
            .Location = New Point(PrintButton.Right + 8, PrintButton.Top),
            .Name = "PdfButton",
            .RightToLeft = RightToLeft.Yes,
            .Size = PrintButton.Size,
            .TabStop = False,
            .Tag = "PRINT",
            .Text = "PDF",
            .UseVisualStyleBackColor = False
        }

        PdfButton.FlatAppearance.BorderColor = PrintButton.FlatAppearance.BorderColor
        PdfButton.FlatAppearance.MouseDownBackColor = PrintButton.FlatAppearance.MouseDownBackColor
        PdfButton.FlatAppearance.MouseOverBackColor = PrintButton.FlatAppearance.MouseOverBackColor

        Panel2.Controls.Add(PdfButton)
        PdfButton.BringToFront()

    End Sub

    Private Sub Make_Hints()
        SendMessage(CMSearchTextBox.Handle, &H1501, 0, "إبحث عن صنف")
    End Sub

    'Private Sub Check_View_Control()
    '    Me.DataGridViewX.Columns("GM_Name_CL").Visible = My_Settings.ST_GM_Name
    '    Me.DataGridViewX.Columns("ST_Name_CL").Visible = MY_Settings.ST_STNAME

    '    If U_SB_Show_IM_COST = True Then
    '        Me.DataGridViewX.Columns("Last_Pch_Price").Visible = MY_Settings.ST_Last_Pch_Price
    '    Else
    '        Me.DataGridViewX.Columns("Last_Pch_Price").Visible = U_SB_Show_IM_COST
    '        Me.DataGridViewX.Columns("Cost_CL").Visible = U_SB_Show_IM_COST
    '        Me.DataGridViewX.Columns("T_Cost_CL").Visible = U_SB_Show_IM_COST
    '    End If


    '    If S_IM_Valid = False Then
    '        Me.DataGridViewX.Columns("D_Valid_CL").Visible = S_IM_Valid
    '    Else
    '        Me.DataGridViewX.Columns("D_Valid_CL").Visible = MY_Settings.ST_Valid
    '    End If


    '    Me.DataGridViewX.Columns("IM_Num_CL").Visible = MY_Settings.ST_IM_Num
    '    'D_Valid_Panel.Visible = MY_Settings.ST_Valid
    '    If ST_cm.Items.Count = 2 Then ST_cm.SelectedValue = SB_ST_ID
    'End Sub

    Public Sub fetch_GM()
        GM_Serach.DataSource = GetMailItems_GM()
        GM_Serach.DisplayMember = "name"
        GM_Serach.ValueMember = "ID"
        GM_Serach.SelectedIndex = 0
    End Sub

    Function GetMailItems_GM() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "------- كل التصنيفات -------"))

        Dim c1 As New C
        Dim s As String = "select GM_ID AS ID,GM_NAME AS name from General_menu ORDER By GM_ID ASC"
        c1.Com = New SqlClient.SqlCommand(s, c1.Con)
        c1.Con.Open()
        Try
            c1.Dr = c1.Com.ExecuteReader
            If c1.Dr.HasRows Then
                While c1.Dr.Read
                    mailItems.Add(New MailItem(c1.Dr("ID"), c1.Dr("name")))
                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
        Return mailItems

    End Function

    Public Sub fetch_ST()
        ST_cm.DataSource = GetMailItems()
        ST_cm.DisplayMember = "name"
        ST_cm.ValueMember = "ID"
        ST_cm.SelectedValue = SB_ST_ID
    End Sub

    Function GetMailItems() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "------- كل المخازن -------"))

        Dim c1 As New C
        Dim s As String = "select ST_ID AS ID,ST_name AS name from STORES ORDER By ST_ID ASC"
        c1.Com = New SqlClient.SqlCommand(s, c1.Con)
        c1.Con.Open()
        Try
            c1.Dr = c1.Com.ExecuteReader
            If c1.Dr.HasRows Then
                While c1.Dr.Read
                    mailItems.Add(New MailItem(c1.Dr("ID"), c1.Dr("name")))
                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
        Return mailItems

    End Function

    Public Sub Fill_ALL_IM(IM_ID As Integer)

        'IM_DT.Clear()
        'Dim C As New C
        'With C.Com
        '    .Connection = C.Con
        '    .CommandText = "Stores_IM_SELECT_ALL"
        '    .CommandType = CommandType.StoredProcedure
        '    .Parameters.AddWithValue("@St_ID", ST_cm.SelectedValue)
        '    .Parameters.AddWithValue("@GM_ID", GM_Serach.SelectedValue)
        '    If IM_ID > 0 Then .Parameters.AddWithValue("@IM_ID", IM_ID)

        '    If D_Valid_Panel.Visible = True Then
        '        .Parameters.AddWithValue("@D_F", DateRange_Flate1.D_F.Value)
        '        .Parameters.AddWithValue("@D_T", DateRange_Flate1.D_T.Value)
        '        .Parameters.AddWithValue("@is_Valid_Filter", FilterBy_D_Valid_CB.Checked)
        '    End If

        'End With
        'C.Da = New SqlClient.SqlDataAdapter(C.Com)
        'C.Da.Fill(IM_DT)
        'DataGridViewX.DataSource = IM_DT

        'Get_Total_QYT()
        '-----------------------------------------------------------------------------------------
        '-------------------------------------------------------------------------------------------------------------

        gridv.DataSource = Nothing
        IM_DT = New DataTable


        Dim Main_Query, Select_Query, Min_SP As String
        Dim Table_Name As String = "IM_STORE_View"
        Dim C As New C

        Min_SP = ""
        If S_Allow_MinSP = True Then Min_SP = ",Min_SP AS '   بيع الجملة',Min_SP_2 AS '    بيع جملة الجملة' "

        Select_Query = " select ' ' AS '    ت',IM_ID,ST_ID, ST_Name as '      المخزن',GM_Name as '      المجموعة',IM_Num as '      رقم الصنف',Barcode as '      الباركود' , " &
        "item_name as '      إسم الصنف',U_Name as '      الوحدة',D_Valid as '      الصلاحية',QTY as '      العدد',Cost as '      التكلفة',Last_Pch_Price AS '      أخر شراء', " &
         "Price AS '  سعر البيع' " & Min_SP & "  , T_Cost as '      إجمالي التكلفة' ,item_name"


        Main_Query = Select_Query & " FROM " & Table_Name

        Main_Query &= " WHERE is_Default = 1 "

        If ST_cm.SelectedIndex > 0 Then Main_Query = Main_Query & " AND ST_ID = " & ST_cm.SelectedValue
        If GM_Serach.SelectedIndex > 0 Then Main_Query = Main_Query & " AND GM_ID = " & GM_Serach.SelectedValue

        If IM_ID > 0 Then Main_Query = Main_Query & " AND IM_ID = " & IM_ID

        If Show_only_Zero_CB.Checked = False Then Main_Query = Main_Query & " AND QTY <> 0 "

        C.Da = New SqlClient.SqlDataAdapter(Main_Query, C.Con)
        C.Da.SelectCommand.CommandTimeout = 120
        C.Da.Fill(IM_DT)
        gridv.DataSource = IM_DT

        If gridv.Columns.Count > 0 Then


            ' Define a list or array of column names to check
            Dim columnNamesToCheck As String() = {"   بيع الجملة", "    بيع جملة الجملة", "      التكلفة", "      أخر شراء", "  سعر البيع", "      إجمالي التكلفة", "      العدد"}

            ' Loop through each column in the Advanced GridView
            For Each column As DataGridViewColumn In gridv.Columns
                ' Check if the column's header text matches any in the list
                If columnNamesToCheck.Contains(column.HeaderText) Then
                    'Console.WriteLine("Matched Column: " & column.HeaderText)
                    gridv.Columns(column.Name).Tag = 1
                    If column.HeaderText <> "      العدد" Then gridv.Columns(column.Name).DefaultCellStyle.Format = N_Point_Fter
                End If
            Next


            'If CheckedListBox1.Items.Count = 0 Then
            '    CheckedListBox1.Items.Clear()
            '    For i As Integer = 0 To gridv.ColumnCount - 1
            '        Dim CL = gridv.Columns(i).Name
            '        CheckedListBox1.Items.Add(CL)
            '    Next
            'End If


            'Recover_STORES_Explorer_File_Setting(CheckedListBox1)

            'For i = 0 To gridv.ColumnCount - 1
            '    gridv.Columns(i).Visible = CheckedListBox1.GetItemChecked(i)
            'Next





            If U_SB_Show_IM_COST Then


                UcGridColumnsSelector1.BindGrid(
gridv,
New List(Of String) From {""},
Me.Name.ToString
 )

            Else

                UcGridColumnsSelector1.BindGrid(
gridv,
New List(Of String) From {"      التكلفة", "      أخر شراء", "      إجمالي التكلفة"},
Me.Name.ToString
 )

            End If


            'If U_SB_Show_IM_COST = False Then
            '    gridv.Columns("      التكلفة").Visible = False
            '    CheckedListBox1.SetItemCheckState(11, False)

            '    gridv.Columns("      أخر شراء").Visible = False
            '    CheckedListBox1.SetItemCheckState(12, False)

            '    gridv.Columns("      إجمالي التكلفة").Visible = False
            '    CheckedListBox1.SetItemCheckState(14, False)
            'End If


            GET_TOTALS_GRID(gridv)

        End If

        'Get_Total_QYT()
    End Sub


    Private Sub Index_GV()
        For i = 0 To gridv.Rows.Count - 1
            gridv.Rows(i).Cells(0).Value = i + 1
        Next
        textBox_total.Text = gridv.Rows.Count.ToString
    End Sub
    'Private Sub STORES_Explorer_Resize(sender As Object, e As EventArgs) Handles Me.Resize
    '    '  rs.ResizeAllControls(Me)
    'End Sub

    Private Sub CMSearchTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CMSearchTextBox.KeyPress
        If e.KeyChar = "'" Then e.Handled = True
    End Sub

    Private Sub CMSearchTextBox_TextChanged(sender As Object, e As EventArgs) Handles CMSearchTextBox.TextChanged

        If BarcodeSearch_CB.Checked = False And IMNUM_CB.Checked = False Then
            IM_DT.DefaultView.RowFilter = " item_name LIKE '%" + EscapeRowFilterValue(sender.Text) + "%'"

            GET_TOTALS_GRID(gridv)
            'Get_Total_QYT()
        End If
    End Sub

    Private Function EscapeRowFilterValue(value As String) As String
        If value Is Nothing Then Return ""
        Return value.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]").Replace("*", "[*]")
    End Function


    Public Sub Get_Total_QYT()

        'Dim Cost As Double = 0
        'Dim Qty As Double = 0
        'For i = 0 To gridv.Rows.Count - 1
        '    Cost += gridv.Rows(i).Cells(14).Value
        '    Qty += gridv.Rows(i).Cells(10).Value
        'Next
        'TotalQYT_txt.Text = Qty.ToString("N")
        'TotalCost_txt.Text = Cost.ToString("N")
    End Sub


    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click
        If gridv.Rows.Count = 0 Then
            MsgBox("لا توجد بيانات للطباعة.", MsgBoxStyle.Exclamation, "")
            Return
        End If

        'If U_SB_Show_IM_COST = False Then
        '    gridv.Columns(11).Visible = False
        '    CheckedListBox1.SetItemCheckState(11, False)

        '    gridv.Columns(12).Visible = False
        '    CheckedListBox1.SetItemCheckState(12, False)

        '    gridv.Columns(14).Visible = False
        '    CheckedListBox1.SetItemCheckState(14, False)
        'End If

        PreviewStoresExplorerPrint()

    End Sub

    Private Sub PdfButton_Click(sender As Object, e As EventArgs) Handles PdfButton.Click

        If gridv.Rows.Count = 0 Then
            MsgBox("لا توجد بيانات للتصدير.", MsgBoxStyle.Exclamation, "")
            Return
        End If

        'If U_SB_Show_IM_COST = False Then
        '    gridv.Columns(11).Visible = False
        '    CheckedListBox1.SetItemCheckState(11, False)

        '    gridv.Columns(12).Visible = False
        '    CheckedListBox1.SetItemCheckState(12, False)

        '    gridv.Columns(14).Visible = False
        '    CheckedListBox1.SetItemCheckState(14, False)
        'End If

        ExportStoresExplorerPdf()

    End Sub

    Private Sub PreviewStoresExplorerPrint()

        Using printDocument As PrintDocument = CreateStoresExplorerPrintDocument()
            Using previewDialog As New PrintPreviewDialog()
                previewDialog.Document = printDocument
                previewDialog.WindowState = FormWindowState.Maximized
                previewDialog.Text = "معاينة كشف المخزن"
                previewDialog.ShowDialog(Me)
            End Using
        End Using

    End Sub

    Private Sub ExportStoresExplorerPdf()

        Dim pdfPrinterName As String = GetPdfPrinterName()

        If pdfPrinterName = "" Then
            MsgBox("طابعة Microsoft Print to PDF غير متوفرة على هذا الجهاز.", MsgBoxStyle.Exclamation, "")
            Return
        End If

        Using saveDialog As New SaveFileDialog()
            saveDialog.Filter = "PDF Files (*.pdf)|*.pdf"
            saveDialog.FileName = "كشف المخزن " & Date.Now.ToString("yyyyMMdd_HHmm") & ".pdf"
            saveDialog.Title = "حفظ كشف المخزن PDF"

            If saveDialog.ShowDialog(Me) <> DialogResult.OK Then Return

            Using printDocument As PrintDocument = CreateStoresExplorerPrintDocument()
                printDocument.PrinterSettings.PrinterName = pdfPrinterName
                printDocument.PrinterSettings.PrintToFile = True
                printDocument.PrinterSettings.PrintFileName = saveDialog.FileName
                printDocument.PrintController = New StandardPrintController()
                printDocument.Print()
            End Using
        End Using

    End Sub

    Private Function CreateStoresExplorerPrintDocument() As PrintDocument

        Dim printDocument As New PrintDocument()
        printDocument.DocumentName = "كشف المخزن"
        printDocument.DefaultPageSettings.Landscape = Print_type_Cmb.SelectedIndex <> 1
        printDocument.DefaultPageSettings.Margins = New Margins(30, 30, 40, 45)

        AddHandler printDocument.BeginPrint, AddressOf StoresExplorerPrintDocument_BeginPrint
        AddHandler printDocument.PrintPage, AddressOf StoresExplorerPrintDocument_PrintPage

        Return printDocument

    End Function

    Private Sub StoresExplorerPrintDocument_BeginPrint(sender As Object, e As PrintEventArgs)

        _printRowIndex = 0
        _printDateTime = Date.Now

    End Sub

    Private Sub StoresExplorerPrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)

        Dim bounds As Rectangle = e.MarginBounds
        Dim y As Integer = bounds.Top
        Dim visibleColumns As List(Of DataGridViewColumn) = GetPrintableColumns()

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

            Dim centerFormat As New StringFormat()
            centerFormat.Alignment = StringAlignment.Center
            centerFormat.LineAlignment = StringAlignment.Center
            centerFormat.Trimming = StringTrimming.EllipsisCharacter

            DrawStoresReportStoreHeader(e.Graphics, bounds, y, storeTitleFont, storeSubTitleFont, centerFormat)

            e.Graphics.DrawString(
                "كشف المخزن",
                titleFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 28),
                centerFormat
            )
            y += 30

            e.Graphics.DrawString(
                GetStoresExplorerFilterText(),
                infoFont,
                Brushes.Black,
                New Rectangle(bounds.Left, y, bounds.Width, 24),
                rtlFormat
            )
            y += 30

            Dim rowHeight As Integer = 24
            Dim widths As List(Of Integer) = CalculatePrintColumnWidths(visibleColumns, bounds.Width)
            Dim x As Integer = bounds.Right

            For i As Integer = 0 To visibleColumns.Count - 1
                x -= widths(i)
                Dim rect As New Rectangle(x, y, widths(i), rowHeight)

                Using backBrush As New SolidBrush(Color.FromArgb(45, 62, 80))
                    e.Graphics.FillRectangle(backBrush, rect)
                End Using

                e.Graphics.DrawRectangle(Pens.DarkGray, rect)
                e.Graphics.DrawString(visibleColumns(i).HeaderText.Trim(), headerFont, Brushes.White, rect, centerFormat)
            Next

            y += rowHeight

            Dim rowsPerPage As Integer = CalculatePrintableRowsPerPage(y, bounds.Bottom, rowHeight)
            Dim totalPages As Integer = CalculateTotalPrintPages(gridv.Rows.Count, rowsPerPage)
            Dim currentPage As Integer = CalculateCurrentPrintPage(_printRowIndex, rowsPerPage)

            While _printRowIndex < gridv.Rows.Count

                If y + rowHeight > bounds.Bottom - 58 Then
                    DrawStoresReportFooter(e.Graphics, bounds, currentPage, totalPages, infoFont, centerFormat)
                    e.HasMorePages = True
                    Return
                End If

                Dim row As DataGridViewRow = gridv.Rows(_printRowIndex)
                x = bounds.Right

                For i As Integer = 0 To visibleColumns.Count - 1
                    x -= widths(i)
                    Dim rect As New Rectangle(x, y, widths(i), rowHeight)

                    If _printRowIndex Mod 2 = 0 Then
                        e.Graphics.FillRectangle(Brushes.White, rect)
                    Else
                        Using altBrush As New SolidBrush(Color.FromArgb(248, 250, 252))
                            e.Graphics.FillRectangle(altBrush, rect)
                        End Using
                    End If

                    e.Graphics.DrawRectangle(Pens.LightGray, rect)
                    e.Graphics.DrawString(GetPrintableCellText(row, visibleColumns(i)), rowFont, Brushes.Black, rect, centerFormat)
                Next

                y += rowHeight
                _printRowIndex += 1

            End While

            y += 8
            DrawStoresTotals(e.Graphics, bounds, y, totalFont, centerFormat)
            DrawStoresReportFooter(e.Graphics, bounds, currentPage, totalPages, infoFont, centerFormat)

        End Using

        e.HasMorePages = False

    End Sub

    Private Function GetPrintableColumns() As List(Of DataGridViewColumn)

        Dim columns As New List(Of DataGridViewColumn)()

        For Each col As DataGridViewColumn In gridv.Columns
            If col.Visible Then columns.Add(col)
        Next

        Return columns

    End Function

    Private Function CalculatePrintColumnWidths(columns As List(Of DataGridViewColumn), availableWidth As Integer) As List(Of Integer)

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

    Private Function GetPrintableCellText(row As DataGridViewRow, column As DataGridViewColumn) As String

        If row.Cells(column.Name).Value Is Nothing OrElse row.Cells(column.Name).Value Is DBNull.Value Then Return ""

        If column.Tag IsNot Nothing AndAlso column.Tag.ToString() = "1" Then
            Dim numberValue As Decimal
            If Decimal.TryParse(row.Cells(column.Name).Value.ToString(), numberValue) Then Return numberValue.ToString(N_Point_Fter)
        End If

        Return row.Cells(column.Name).FormattedValue.ToString()

    End Function

    Private Function GetStoresExplorerFilterText() As String

        Dim searchText As String = CMSearchTextBox.Text.Trim()
        If searchText = "" Then searchText = "الكل"

        Return "المخزن: " & ST_cm.Text &
            "    المجموعة: " & GM_Serach.Text &
            "    البحث: " & searchText &
            "    عدد الأصناف: " & gridv.Rows.Count.ToString()

    End Function

    Private Sub DrawStoresReportStoreHeader(graphics As Graphics, bounds As Rectangle, ByRef y As Integer, storeTitleFont As Font, storeSubTitleFont As Font, centerFormat As StringFormat)

        If Not String.IsNullOrWhiteSpace(SBill_Title_1) Then
            graphics.DrawString(SBill_Title_1, storeTitleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 30), centerFormat)
            y += 30
        End If

        If Not String.IsNullOrWhiteSpace(SBill_Title_2) Then
            graphics.DrawString(SBill_Title_2, storeSubTitleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 24), centerFormat)
            y += 24
        End If

    End Sub

    Private Sub DrawStoresTotals(graphics As Graphics, bounds As Rectangle, y As Integer, totalFont As Font, centerFormat As StringFormat)

        If TOTAL_Grid.Columns.Count = 0 OrElse TOTAL_Grid.Rows.Count = 0 Then Return

        Dim rowHeight As Integer = 24
        Dim colWidth As Integer = Math.Max(80, bounds.Width \ TOTAL_Grid.Columns.Count)
        Dim x As Integer = bounds.Right

        For Each col As DataGridViewColumn In TOTAL_Grid.Columns
            x -= colWidth
            Dim rect As New Rectangle(x, y, colWidth, rowHeight)
            FillTotalCell(graphics, rect)
            graphics.DrawString(col.HeaderText.Trim(), totalFont, Brushes.Black, rect, centerFormat)
        Next

        y += rowHeight
        x = bounds.Right

        For i As Integer = 0 To TOTAL_Grid.Columns.Count - 1
            x -= colWidth
            Dim rect As New Rectangle(x, y, colWidth, rowHeight)
            FillTotalCell(graphics, rect)

            Dim valueText As String = ""
            Dim value As Object = TOTAL_Grid.Rows(0).Cells(i).Value

            If value IsNot Nothing AndAlso value IsNot DBNull.Value Then
                Dim numberValue As Decimal
                If Decimal.TryParse(value.ToString(), numberValue) Then
                    valueText = numberValue.ToString(N_Point_Fter)
                Else
                    valueText = value.ToString()
                End If
            End If

            graphics.DrawString(valueText, totalFont, Brushes.Black, rect, centerFormat)
        Next

    End Sub

    Private Sub FillTotalCell(graphics As Graphics, rect As Rectangle)

        Using totalBrush As New SolidBrush(Color.FromArgb(235, 240, 245))
            graphics.FillRectangle(totalBrush, rect)
        End Using

        graphics.DrawRectangle(Pens.LightGray, rect)

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

    Private Sub DrawStoresReportFooter(graphics As Graphics, bounds As Rectangle, currentPage As Integer, totalPages As Integer, footerFont As Font, centerFormat As StringFormat)

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

    Private Sub MakeBarcode_btn_Click(sender As Object, e As EventArgs)
        printbarcode.ShowDialog()
    End Sub

    Private Sub IM_btn_Click(sender As Object, e As EventArgs) Handles IM_btn.Click
        Me.Cursor = Cursors.AppStarting
        F_ItemsMenu = New ItemsMenu
        F_ItemsMenu.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '  Fetch_QTY()
        Fill_ALL_IM(0)
    End Sub

    Private Sub ST_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles ST_cm.SelectedValueChanged
        '  If Get_IM = True Then Fill_ALL_IM(0)
    End Sub

    'Private Sub BarcodeSearch_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    '    CMSearchTextBox.Select()
    '    If BarcodeSearch_CB.Checked = True Then IMNUM_CB.Checked = False
    'End Sub

    Private Sub CMSearchTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles CMSearchTextBox.KeyDown
        If e.KeyCode = Keys.Return Then Search_By_Barcode() 'And (BarcodeSearch_CB.Checked = True Or IMNUM_CB.Checked = True)
        If e.KeyCode = Keys.Delete Then CMSearchTextBox.Clear()
    End Sub

    Public Sub Search_By_Barcode()

        Dim IM_ID As Integer
        Dim c As New C
        IM_DT.Clear()
        Try
            Dim s As String = ""

            If IMNUM_CB.Checked Then s = "select IM_ID from IM_Menu WHERE IM_NUM = '" & CMSearchTextBox.Text & "'"
            If BarcodeSearch_CB.Checked Then s = "select IM_ID from IM_units_Search_V WHERE Barcode = '" & CMSearchTextBox.Text & "'"

            's = "select IM_ID from IM_units_Search_V WHERE Barcode = '" & CMSearchTextBox.Text & "'"

            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                IM_ID = c.Dr("IM_ID")
                Fill_ALL_IM(IM_ID)
            Else
                MsgBox("لم يتم التعرف على الإدخال ", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub



    Private Sub Up_Update_btn_Click(sender As Object, e As EventArgs) Handles Up_Update_btn.Click
        If U_Update_IM_QTY = True Then
            If gridv.Rows.Count > 0 Then
                is_Update_Valid_D = False
                is_Update_QTY = True
                is_Update_Unit_Cost = False
                IM_Update_Qty.ShowDialog()
            End If
        End If
    End Sub


    Private Sub GM_Serach_SelectedValueChanged(sender As Object, e As EventArgs) Handles GM_Serach.SelectedValueChanged
        '   Fetch_QTY()
        'If Get_IM = True Then Fill_ALL_IM(0)
    End Sub




    Private Sub DataGridViewX_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles gridv.MouseDoubleClick
        'If U_Update_IM_QTY = True Then
        '    If gridv.Rows.Count > 0 Then
        '        '  If SHhow_Zero_Qty_btn.BackColor = Color.WhiteSmoke Then
        '        If gridv.Rows.Count > 0 Then
        '            is_Update_Valid_D = False
        '            is_Update_QTY = True
        '            is_Update_Unit_Cost = False
        '            IM_Update_Qty.ShowDialog()
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub DGV_Control_btn_Click(sender As Object, e As EventArgs)
        Dim F As New DGV_Control
        F.TabControl1.TabPages.Remove(F.OtherTabPage)
        F.TabControl1.TabPages.Remove(F.ItemPriceTabPage)
        F.Show()
    End Sub

    Private Sub Recount_Qty_btn_Click(sender As Object, e As EventArgs)
        If ST_cm.SelectedValue = 0 Then
            MsgBox("حدد المخزن المراد تدويره", MsgBoxStyle.Information, "")
            ST_cm.DroppedDown = True
            ST_cm.Select()
        Else
            If MessageBox.Show(" تدوير كميات الأصناف ؟؟ ... قد تستغرق العملية بعض الوقت ", "تأكيد", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then RECOUNT_IM_ALL_QTY()
        End If

    End Sub

    Public Sub RECOUNT_IM_ALL_QTY()
        Me.Cursor = Cursors.AppStarting
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "RECOUNT_IM_ALL_QTY"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            Network_Edit_Tracker_insert(" تدوير كميات الأصناف بمخزن : " & ST_cm.Text & "  من شاشة كشف المخازن ", 0, 0, 0)
            Me.Cursor = Cursors.Default
            Fill_ALL_IM(0)
            Make_Hints()
            MsgBox("تم التدوير", MsgBoxStyle.Information, "")
            Network_Edit_Tracker_insert(" تدوير كمية المخزن:" & ST_cm.Text, 0, 23, 3)
        End If

    End Sub

    Private Sub Recount_Cost_btn_Click(sender As Object, e As EventArgs) Handles Recount_Cost_btn.Click

        If MessageBox.Show(" تدوير متوسط تكلفةالأصناف ؟؟ ... قد تستغرق العملية بعض الوقت ", "تأكيد", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then RECOUNT_IM_ALL_COST()

    End Sub

    Public Sub RECOUNT_IM_ALL_COST()
        Me.Cursor = Cursors.AppStarting
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "[RECOUNT_IM_ALL_MainCost]"
            .CommandType = CommandType.StoredProcedure
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            Network_Edit_Tracker_insert(" تدوير متوسط تكلفة الأصناف من شاشة كشف المخازن   : ", 0, 0, 0)
            Me.Cursor = Cursors.Default
            Fill_ALL_IM(0)
            Make_Hints()
            MsgBox("تم التدوير", MsgBoxStyle.Information, "")
            Network_Edit_Tracker_insert("تدوير تكلفة الخازن", 0, 23, 3)

        End If

    End Sub

    Private Sub EXCEL_BTN_Click(sender As Object, e As EventArgs) Handles EXCEL_BTN.Click
        EXCEL_EXPORT(gridv)
    End Sub

    Private Sub gridv_ColumnStateChanged(sender As Object, e As DataGridViewColumnStateChangedEventArgs) Handles gridv.ColumnStateChanged
        If e.StateChanged = DataGridViewElementStates.Visible Then GET_TOTALS_GRID(gridv)
    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs)
        'If CheckedListBox1.Items.Count > 0 Then
        '    ExportButton_STORES_Explorer_Setting_ToFile(CheckedListBox1)
        'End If

    End Sub


    Private Sub BarcodeSearch_CB_CheckedChanged(sender As Object, e As EventArgs) Handles BarcodeSearch_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub IMNUM_CB_CheckedChanged(sender As Object, e As EventArgs) Handles IMNUM_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub


    Public Sub GET_TOTALS_GRID(gridv)

        Index_GV()

        Dim arr_val As New List(Of Double)()
        'TOTAL_Grid.DataSource = Nothing

        For i = 0 To TOTAL_Grid.Columns.Count - 1
            TOTAL_Grid.Columns.Remove(TOTAL_Grid.Columns(0).Name)
        Next


        For i = 0 To gridv.columns.count - 1
            If gridv.columns(i).Tag = 1 And gridv.columns(i).Visible = True Then


                Dim SUM As Double = 0

                For j As Integer = 0 To gridv.Rows.Count() - 1 Step +1
                    If Not IsDBNull(gridv.Rows(j).Cells(i).Value) Then SUM = SUM + gridv.Rows(j).Cells(i).Value
                Next

                arr_val.Add(SUM)

                Dim col As New DataGridViewTextBoxColumn
                col.HeaderText = "إجمالي:- " & gridv.columns(i).HeaderText
                col.DefaultCellStyle.Format = N_Point_Fter
                TOTAL_Grid.Columns.Add(col)


            End If
        Next

        If arr_val.Count > 0 Then TOTAL_Grid.Rows.Add("")

        For i = 0 To TOTAL_Grid.Columns.Count - 1
            TOTAL_Grid.Rows(0).Cells(i).Value = arr_val(i) '.ToString("00.00")
        Next

        TOTAL_Grid.ClearSelection()

    End Sub

    Private Sub STORES_Explorer_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub Show_only_Zero_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Show_only_Zero_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class
