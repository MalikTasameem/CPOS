Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Drawing.Printing
Imports System.Collections.Generic
Imports Microsoft.Win32
Public Class IMP_Perfet_Report_1
    Dim Min_SP As Boolean = False
    Dim Min_SP_2 As Boolean = False
    'Dim is_Filter_SB_Type As Boolean = False
    Dim IM_MV_Dt As New DataTable

    Dim rs As New Resizer
    Private _itemProfitPrintRowIndex As Integer
    Private _itemProfitPrintPageNumber As Integer
    Private _itemProfitPrintTotalPages As Integer = 1
    Private _itemProfitPrintRowsPerPage As Integer
    Private _itemProfitPrintTotalRows As Integer
    Private _itemProfitPrintDateTime As Date
    Private _itemProfitTotalsPrinted As Boolean

    Private Sub Empty_F_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        ConfigureTotalsGrid()
        fetch_SB_Type()
        Min_SB_Panel.Visible = S_Allow_MinSP
        ApplyMarketerVisibility()
        'Markters_GroupBox.Visible = S_Marketers
        fetch_ST()
        fetch_GM()
    End Sub
    Public Sub fetch_GM()
        PerfetGM_Serach.DataSource = GetMailItems()
        PerfetGM_Serach.DisplayMember = "name"
        PerfetGM_Serach.ValueMember = "ID"
        PerfetGM_Serach.SelectedIndex = 0
    End Sub


    Public Sub fetch_ST()
        ST_cm.DataSource = Ge_St_Items()
        ST_cm.DisplayMember = "name"
        ST_cm.ValueMember = "ID"
        ST_cm.SelectedIndex = 0
    End Sub

    Private Sub Empty_F_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub ConfigureTotalsGrid()

        If IMPerfTotals_DGV Is Nothing Then Return
        If IMPerfTotals_DGV.Rows.Count = 0 Then IMPerfTotals_DGV.Rows.Add()

        IMPerfTotals_DGV.ClearSelection()

    End Sub

    Private Sub ApplyMarketerVisibility()

        If IMPerf_DGV.Columns.Contains("Markter_Val_CL") Then
            IMPerf_DGV.Columns("Markter_Val_CL").Visible = S_Marketers
        End If

        If IMPerfTotals_DGV.Columns.Contains("TotalsMarkter_CL") Then
            IMPerfTotals_DGV.Columns("TotalsMarkter_CL").Visible = S_Marketers
        End If

    End Sub

    Private Sub IMPerf_Serch_btn_Click(sender As Object, e As EventArgs) Handles IMPerf_Serch_btn.Click
        IM_Perfet_Select_By_Date()
    End Sub


    Private Sub IM_Perfet_Select_By_Date()
        IM_MV_Dt.Clear()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "IM_Perfet_Select"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
            .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
            .Parameters.AddWithValue("@GM_ID", PerfetGM_Serach.SelectedValue)
            .Parameters.AddWithValue("@is_By_Min_SP", Min_SP)
            .Parameters.AddWithValue("@is_By_Min_SP_2", Min_SP_2)
            .Parameters.AddWithValue("@Sales_Type", Sales_Type_Cm.SelectedValue)
        End With

        C.Da = New SqlDataAdapter(C.Com)
        C.Da.SelectCommand.CommandTimeout = 0
        C.Da.Fill(IM_MV_Dt)
        IMPerf_DGV.DataSource = IM_MV_Dt
        ApplyMarketerVisibility()
        Calc_IMPerf()
        'IMRtn_Perfet_Select()

        If IM_MV_Dt.Rows.Count > 0 Then

            'CheckedListBox1.Items.Clear()
            'For i As Integer = 0 To IMPerf_DGV.ColumnCount - 1
            '    Dim CL = IMPerf_DGV.Columns(i).Name
            '    CheckedListBox1.Items.Add(CL)

            '    If IMPerf_DGV.Columns(i).Visible = True Then
            '        CheckedListBox1.SetItemChecked(i, True)
            '    End If
            'Next
        End If
    End Sub



    'Private Sub IMRtn_Perfet_Select()
    '    Dim C = New C
    '    With (C.Com)
    '        .Connection = C.Con
    '        .CommandText = "IMRtn_Perfet_Select"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
    '        .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
    '        .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
    '        .Parameters.AddWithValue("@GM_ID", PerfetGM_Serach.SelectedValue)
    '        .Parameters.Add("@SumRtn", SqlDbType.Float, 0)
    '        .Parameters.Add("@RTN_Saler_Money", SqlDbType.Float, 0)
    '        .Parameters.Add("@Markter_Val", SqlDbType.Float, 0)
    '        .Parameters.Add("@Disc_Val", SqlDbType.Float, 0)
    '        .Parameters.Add("@SUM_COST", SqlDbType.Float, 0)
    '        .Parameters("@RTN_Saler_Money").Direction = ParameterDirection.Output
    '        .Parameters("@SumRtn").Direction = ParameterDirection.Output
    '        .Parameters("@Markter_Val").Direction = ParameterDirection.Output
    '        .Parameters("@Disc_Val").Direction = ParameterDirection.Output
    '        .Parameters("@SUM_COST").Direction = ParameterDirection.Output
    '        .Parameters.AddWithValue("@is_By_Min_SP", Min_SP)
    '        .Parameters.AddWithValue("@is_By_Min_SP_2", Min_SP_2)
    '        .Parameters.AddWithValue("@Sales_Type", Sales_Type_Cm.SelectedValue)


    '    End With
    '    If SQL_SP_EXEC(C.Com) = True Then
    '        With (C.Com)
    '            SBRtn_TotalPerfet_txt.Text = .Parameters("@SumRtn").Value
    '            Total_ALL_Perfet_txt.Text = Pure_IM_Perfet_txt.Text
    '            Pure_IM_Perfet_txt.Text = (Convert.ToDouble(Total_ALL_Perfet_txt.Text) - Convert.ToDouble(SBRtn_TotalPerfet_txt.Text)).ToString("N")


    '            Total_RtnMark_Val_txt.Text = .Parameters("@Markter_Val").Value
    '            Pure_Mark_Val_txt.Text = (Convert.ToDouble(Total_Mark_Val_txt.Text) - Convert.ToDouble(Total_RtnMark_Val_txt.Text)).ToString("N")


    '            Dim N As Double = Convert.ToDouble(Saler_M_T_txt.Text) - .Parameters("@RTN_Saler_Money").Value
    '            Pure_Saler_M_txt.Text = N.ToString("N")


    '            Dim N2 As Double = .Parameters("@Disc_Val").Value
    '            Tota_Disc_TXT.Text = N2.ToString("N")


    '            Final_Pure_Perfet_txt.Text = (Convert.ToDouble(Pure_IM_Perfet_txt.Text) - Convert.ToDouble(Pure_Mark_Val_txt.Text) - _
    '                                          Convert.ToDouble(Pure_Saler_M_txt.Text) - Convert.ToDouble(Tota_Disc_TXT.Text)).ToString("N")


    '            Total_Cost_txt.Text = .Parameters("@SUM_COST").Value
    '        End With
    '    End If
    'End Sub

    Private Sub Sales_Type_Cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles Sales_Type_Cm.SelectedValueChanged
        'If is_Filter_SB_Type = True Then

        If TypeName(Sales_Type_Cm.SelectedValue) = "Integer" Then
            If Sales_Type_Cm.SelectedValue = 0 Then
                Min_SP = True
                Min_SP_2 = True
            End If

            If Sales_Type_Cm.SelectedValue = 1 Then
                Min_SP = False
                Min_SP_2 = False
            End If

            If Sales_Type_Cm.SelectedValue = 2 Then
                Min_SP = True
                Min_SP_2 = False
            End If

            If Sales_Type_Cm.SelectedValue = 3 Then
                Min_SP_2 = True
                Min_SP = False
            End If
        End If


        'End If
    End Sub

    Public Sub fetch_SB_Type()
        Sales_Type_Cm.DataSource = Ge_SB_Type()
        Sales_Type_Cm.DisplayMember = "name"
        Sales_Type_Cm.ValueMember = "ID"
        Sales_Type_Cm.SelectedIndex = 0
    End Sub

    Function Ge_SB_Type() As List(Of MailItem)
        Dim mailItems = New List(Of MailItem)
        mailItems.Add(New MailItem(0, "---- كل المبيعات ----"))
        mailItems.Add(New MailItem(1, "القطاعي"))
        mailItems.Add(New MailItem(2, "الجملة"))
        mailItems.Add(New MailItem(3, "جملة الجملة"))
        Return mailItems

    End Function

    Private Sub IMPerf_DGV_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles IMPerf_DGV.RowsAdded
        Calc_IMPerf()
    End Sub

    Private Sub IMPerf_DGV_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles IMPerf_DGV.RowsRemoved
        Calc_IMPerf()
    End Sub

    Public Sub Calc_IMPerf()

        UpdateTotalsGrid()

    End Sub

    Private Sub UpdateTotalsGrid()

        If IMPerfTotals_DGV Is Nothing OrElse IMPerfTotals_DGV.Columns.Count = 0 Then Return
        If IMPerfTotals_DGV.Rows.Count = 0 Then IMPerfTotals_DGV.Rows.Add()

        With IMPerfTotals_DGV.Rows(0)
            .Cells("TotalsTitle_CL").Value = "الإجمالي"
            .Cells("TotalsQty_CL").Value = GetGridColumnTotal("DataGridViewTextBoxColumn25")
            .Cells("TotalsCost_CL").Value = GetGridColumnTotal("T_Cost_CL")
            .Cells("TotalsSales_CL").Value = GetGridColumnTotal("T_Price_CL")
            .Cells("TotalsProfit_CL").Value = GetGridColumnTotal("Total_Perfet_CL")
            '.Cells("TotalsSaler_CL").Value = GetGridColumnTotal("Saler_Money_CL")
            '.Cells("TotalsMarkter_CL").Value = GetGridColumnTotal("Markter_Val_CL")
        End With

        IMPerfTotals_DGV.ClearSelection()

    End Sub

    Private Sub IMMV_Search_txt_TextChanged(sender As Object, e As EventArgs) Handles IMMV_Search_txt.TextChanged
        Dim Dv As DataView = IM_MV_Dt.DefaultView

        If IM_MV_Dt.Columns.Contains("Item_Name") = False Then
            Calc_IMPerf()
            Return
        End If

        Dim filterText As String = IMMV_Search_txt.Text.Trim().Replace("'", "''")
        If filterText = "" Then
            Dv.RowFilter = ""
        Else
            Dv.RowFilter = "Convert([Item_Name], 'System.String') LIKE '%" & filterText & "%'"
        End If

        IMPerf_DGV.DataSource = Dv
        Calc_IMPerf()
    End Sub

    Private Sub B_Berfet_Print_btn_Click(sender As Object, e As EventArgs) Handles B_Berfet_Print_btn.Click
        PrintItemProfitReport()
    End Sub

    Private Sub B_Berfet_Pdf_btn_Click(sender As Object, e As EventArgs) Handles B_Berfet_Pdf_btn.Click
        ExportItemProfitReportPdf()
    End Sub

    Private Sub PrintItemProfitReport()

        If GetItemProfitPrintableRowsCount() = 0 Then
            MsgBox("لا توجد بيانات للطباعة.", MsgBoxStyle.Exclamation, "")
            Return
        End If

        Using printDocument As PrintDocument = CreateItemProfitPrintDocument()
            Using previewDialog As New PrintPreviewDialog()
                previewDialog.Document = printDocument
                previewDialog.WindowState = FormWindowState.Maximized
                previewDialog.Text = "معاينة تقرير ربح الأصناف"
                previewDialog.ShowDialog(Me)
            End Using
        End Using

    End Sub

    Private Sub ExportItemProfitReportPdf()

        If GetItemProfitPrintableRowsCount() = 0 Then
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
            saveDialog.FileName = "تقرير ربح الأصناف " & Date.Now.ToString("yyyyMMdd_HHmm") & ".pdf"
            saveDialog.Title = "حفظ تقرير ربح الأصناف PDF"

            If saveDialog.ShowDialog(Me) <> DialogResult.OK Then Return

            Using printDocument As PrintDocument = CreateItemProfitPrintDocument()
                printDocument.PrinterSettings.PrinterName = pdfPrinterName
                printDocument.PrinterSettings.PrintToFile = True
                printDocument.PrinterSettings.PrintFileName = saveDialog.FileName
                printDocument.PrintController = New StandardPrintController()
                printDocument.Print()
            End Using
        End Using

    End Sub

    Private Function CreateItemProfitPrintDocument() As PrintDocument

        Dim printDocument As New PrintDocument()
        printDocument.DocumentName = "تقرير ربح الأصناف"
        printDocument.DefaultPageSettings.Landscape = True
        printDocument.DefaultPageSettings.Margins = New Margins(30, 30, 40, 45)

        AddHandler printDocument.BeginPrint, AddressOf ItemProfitPrintDocument_BeginPrint
        AddHandler printDocument.PrintPage, AddressOf ItemProfitPrintDocument_PrintPage

        Return printDocument

    End Function

    Private Sub ItemProfitPrintDocument_BeginPrint(sender As Object, e As PrintEventArgs)

        _itemProfitPrintRowIndex = 0
        _itemProfitPrintPageNumber = 1
        _itemProfitPrintTotalPages = 1
        _itemProfitPrintRowsPerPage = 0
        _itemProfitPrintTotalRows = GetItemProfitPrintableRowsCount()
        _itemProfitPrintDateTime = Date.Now
        _itemProfitTotalsPrinted = False

    End Sub

    Private Sub ItemProfitPrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)

        Dim bounds As Rectangle = e.MarginBounds
        Dim y As Integer = bounds.Top

        Using storeTitleFont As New Font("Segoe UI Semibold", 14.0!, FontStyle.Bold),
              storeSubTitleFont As New Font("Segoe UI", 10.0!, FontStyle.Regular),
              titleFont As New Font("Segoe UI Semibold", 12.0!, FontStyle.Bold),
              infoFont As New Font("Segoe UI", 8.0!, FontStyle.Regular),
              headerFont As New Font("Segoe UI", 8.0!, FontStyle.Bold),
              rowFont As New Font("Segoe UI Semibold", 7.5!, FontStyle.Bold),
              totalFont As New Font("Segoe UI Semibold", 8.0!, FontStyle.Bold)

            Using centerFormat As New StringFormat(),
                  rightFormat As New StringFormat(),
                  leftFormat As New StringFormat()

                centerFormat.Alignment = StringAlignment.Center
                centerFormat.LineAlignment = StringAlignment.Center
                centerFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft

                rightFormat.Alignment = StringAlignment.Far
                rightFormat.LineAlignment = StringAlignment.Center
                rightFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft

                leftFormat.Alignment = StringAlignment.Near
                leftFormat.LineAlignment = StringAlignment.Center

                DrawItemProfitStoreHeader(e.Graphics, bounds, y, storeTitleFont, storeSubTitleFont, centerFormat)

                e.Graphics.DrawString("تقرير ربح الأصناف", titleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 26), centerFormat)
                y += 28
                e.Graphics.DrawString(GetItemProfitFilterText(), infoFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 22), centerFormat)
                y += 28

                Dim columns As List(Of DataGridViewColumn) = GetVisibleColumns(IMPerf_DGV)
                Dim rowHeight As Integer = 24
                Dim headerHeight As Integer = 28
                Dim totalsHeight As Integer = 58
                Dim tableBottom As Integer = bounds.Bottom - 38
                Dim availableRowsHeight As Integer = Math.Max(rowHeight, tableBottom - y - headerHeight - totalsHeight)

                If _itemProfitPrintRowsPerPage = 0 Then
                    _itemProfitPrintRowsPerPage = Math.Max(1, availableRowsHeight \ rowHeight)
                    _itemProfitPrintTotalPages = Math.Max(1, CInt(Math.Ceiling(_itemProfitPrintTotalRows / CDbl(_itemProfitPrintRowsPerPage))))
                End If

                DrawGridHeader(e.Graphics, bounds, y, headerHeight, headerFont, centerFormat, columns, Color.FromArgb(35, 48, 68))
                y += headerHeight

                Dim rowsPrintedOnPage As Integer = 0

                While _itemProfitPrintRowIndex < IMPerf_DGV.Rows.Count
                    Dim row As DataGridViewRow = IMPerf_DGV.Rows(_itemProfitPrintRowIndex)

                    If row.IsNewRow Then
                        _itemProfitPrintRowIndex += 1
                        Continue While
                    End If

                    If y + rowHeight > tableBottom Then
                        DrawItemProfitFooter(e.Graphics, bounds, infoFont, centerFormat, rightFormat, leftFormat)
                        _itemProfitPrintPageNumber += 1
                        e.HasMorePages = True
                        Return
                    End If

                    DrawGridRow(e.Graphics, bounds, y, rowHeight, rowFont, centerFormat, row, columns, rowsPrintedOnPage Mod 2 = 1)
                    y += rowHeight
                    rowsPrintedOnPage += 1
                    _itemProfitPrintRowIndex += 1
                End While

                If _itemProfitTotalsPrinted = False Then
                    If y + totalsHeight > tableBottom Then
                        _itemProfitPrintTotalPages = Math.Max(_itemProfitPrintTotalPages, _itemProfitPrintPageNumber + 1)
                        DrawItemProfitFooter(e.Graphics, bounds, infoFont, centerFormat, rightFormat, leftFormat)
                        _itemProfitPrintPageNumber += 1
                        e.HasMorePages = True
                        Return
                    End If

                    DrawTotalsGridForPrint(e.Graphics, bounds, y, headerFont, totalFont, centerFormat)
                    _itemProfitTotalsPrinted = True
                End If

                DrawItemProfitFooter(e.Graphics, bounds, infoFont, centerFormat, rightFormat, leftFormat)
                e.HasMorePages = False

            End Using
        End Using

    End Sub

    Private Sub DrawItemProfitStoreHeader(ByVal graphics As Graphics, ByVal bounds As Rectangle, ByRef y As Integer, ByVal storeTitleFont As Font, ByVal storeSubTitleFont As Font, ByVal centerFormat As StringFormat)

        If Not String.IsNullOrWhiteSpace(SBill_Title_1) Then
            graphics.DrawString(SBill_Title_1, storeTitleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 28), centerFormat)
            y += 30
        End If

        If Not String.IsNullOrWhiteSpace(SBill_Title_2) Then
            graphics.DrawString(SBill_Title_2, storeSubTitleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 22), centerFormat)
            y += 24
        End If

    End Sub

    Private Sub DrawGridHeader(ByVal graphics As Graphics, ByVal bounds As Rectangle, ByVal y As Integer, ByVal height As Integer, ByVal headerFont As Font, ByVal centerFormat As StringFormat, ByVal columns As List(Of DataGridViewColumn), ByVal headerColor As Color)

        Dim widths As Dictionary(Of String, Integer) = GetScaledColumnWidths(columns, bounds.Width)
        Dim x As Integer = bounds.Right

        Using headerBrush As New SolidBrush(headerColor)
            For Each column As DataGridViewColumn In columns
                Dim rect As New Rectangle(x - widths(column.Name), y, widths(column.Name), height)
                graphics.FillRectangle(headerBrush, rect)
                graphics.DrawRectangle(Pens.White, rect)
                graphics.DrawString(column.HeaderText, headerFont, Brushes.White, rect, centerFormat)
                x -= widths(column.Name)
            Next
        End Using

    End Sub

    Private Sub DrawGridRow(ByVal graphics As Graphics, ByVal bounds As Rectangle, ByVal y As Integer, ByVal height As Integer, ByVal rowFont As Font, ByVal centerFormat As StringFormat, ByVal row As DataGridViewRow, ByVal columns As List(Of DataGridViewColumn), ByVal isAlternate As Boolean)

        Dim widths As Dictionary(Of String, Integer) = GetScaledColumnWidths(columns, bounds.Width)
        Dim x As Integer = bounds.Right

        Using alternateBrush As New SolidBrush(Color.FromArgb(248, 250, 252))
            Dim backBrush As Brush = If(isAlternate, alternateBrush, Brushes.White)

            For Each column As DataGridViewColumn In columns
                Dim rect As New Rectangle(x - widths(column.Name), y, widths(column.Name), height)
                graphics.FillRectangle(backBrush, rect)
                graphics.DrawRectangle(Pens.LightGray, rect)
                graphics.DrawString(GetFormattedGridCellText(row, column), rowFont, Brushes.Black, rect, centerFormat)
                x -= widths(column.Name)
            Next
        End Using

    End Sub

    Private Sub DrawTotalsGridForPrint(ByVal graphics As Graphics, ByVal bounds As Rectangle, ByVal y As Integer, ByVal headerFont As Font, ByVal totalFont As Font, ByVal centerFormat As StringFormat)

        Dim columns As List(Of DataGridViewColumn) = GetVisibleColumns(IMPerfTotals_DGV)
        If columns.Count = 0 OrElse IMPerfTotals_DGV.Rows.Count = 0 Then Return

        Dim headerHeight As Integer = 26
        Dim rowHeight As Integer = 30
        Dim widths As Dictionary(Of String, Integer) = GetScaledColumnWidths(columns, bounds.Width)
        Dim x As Integer = bounds.Right

        Using headerBrush As New SolidBrush(Color.FromArgb(20, 83, 45))
            For Each column As DataGridViewColumn In columns
                Dim rect As New Rectangle(x - widths(column.Name), y, widths(column.Name), headerHeight)
                graphics.FillRectangle(headerBrush, rect)
                graphics.DrawRectangle(Pens.White, rect)
                graphics.DrawString(column.HeaderText, headerFont, Brushes.White, rect, centerFormat)
                x -= widths(column.Name)
            Next
        End Using

        y += headerHeight
        x = bounds.Right

        Using summaryBrush As New SolidBrush(Color.FromArgb(236, 253, 245))
            For Each column As DataGridViewColumn In columns
                Dim rect As New Rectangle(x - widths(column.Name), y, widths(column.Name), rowHeight)
                graphics.FillRectangle(summaryBrush, rect)
                graphics.DrawRectangle(Pens.LightGray, rect)
                graphics.DrawString(GetFormattedGridCellText(IMPerfTotals_DGV.Rows(0), column), totalFont, Brushes.Black, rect, centerFormat)
                x -= widths(column.Name)
            Next
        End Using

    End Sub

    Private Sub DrawItemProfitFooter(ByVal graphics As Graphics, ByVal bounds As Rectangle, ByVal footerFont As Font, ByVal centerFormat As StringFormat, ByVal rightFormat As StringFormat, ByVal leftFormat As StringFormat)

        Dim footerTop As Integer = bounds.Bottom - 24
        Dim sideWidth As Integer = bounds.Width \ 3
        Dim centerWidth As Integer = bounds.Width - (sideWidth * 2)

        graphics.DrawLine(Pens.LightGray, bounds.Left, footerTop - 5, bounds.Right, footerTop - 5)
        graphics.DrawString("المعد: " & USER_NAME, footerFont, Brushes.Black, New Rectangle(bounds.Right - sideWidth, footerTop, sideWidth, 22), rightFormat)
        graphics.DrawString(_itemProfitPrintPageNumber.ToString() & "/" & _itemProfitPrintTotalPages.ToString(), footerFont, Brushes.Black, New Rectangle(bounds.Left + sideWidth, footerTop, centerWidth, 22), centerFormat)
        graphics.DrawString("تاريخ الطباعة: " & _itemProfitPrintDateTime.ToString("yyyy/MM/dd HH:mm"), footerFont, Brushes.Black, New Rectangle(bounds.Left, footerTop, sideWidth, 22), leftFormat)

    End Sub

    Private Function GetVisibleColumns(ByVal grid As DataGridView) As List(Of DataGridViewColumn)

        Dim columns As New List(Of DataGridViewColumn)

        If grid Is Nothing Then Return columns

        For Each column As DataGridViewColumn In grid.Columns
            If column.Visible Then columns.Add(column)
        Next

        columns.Sort(Function(first As DataGridViewColumn, second As DataGridViewColumn) first.DisplayIndex.CompareTo(second.DisplayIndex))

        Return columns

    End Function

    Private Function GetScaledColumnWidths(ByVal columns As List(Of DataGridViewColumn), ByVal totalWidth As Integer) As Dictionary(Of String, Integer)

        Dim widths As New Dictionary(Of String, Integer)
        If columns Is Nothing OrElse columns.Count = 0 Then Return widths

        Dim sourceWidth As Integer = 0
        For Each column As DataGridViewColumn In columns
            sourceWidth += Math.Max(40, column.Width)
        Next

        If sourceWidth <= 0 Then sourceWidth = columns.Count * 80

        Dim usedWidth As Integer = 0
        For i As Integer = 0 To columns.Count - 1
            Dim column As DataGridViewColumn = columns(i)
            Dim width As Integer

            If i = columns.Count - 1 Then
                width = Math.Max(35, totalWidth - usedWidth)
            Else
                width = Math.Max(35, CInt((Math.Max(40, column.Width) / CDbl(sourceWidth)) * totalWidth))
            End If

            widths(column.Name) = width
            usedWidth += width
        Next

        Return widths

    End Function

    Private Function GetFormattedGridCellText(ByVal row As DataGridViewRow, ByVal column As DataGridViewColumn) As String

        If row Is Nothing OrElse column Is Nothing Then Return ""
        If row.Cells(column.Name).Value Is Nothing OrElse IsDBNull(row.Cells(column.Name).Value) Then Return ""

        Dim value As Object = row.Cells(column.Name).Value

        If TypeOf value Is Date Then Return CType(value, Date).ToString("yyyy/MM/dd")

        If IsNumericValue(value) Then
            Dim format As String = column.DefaultCellStyle.Format
            If String.IsNullOrWhiteSpace(format) Then format = "N2"
            Return ToDoubleValue(value).ToString(format)
        End If

        Return value.ToString()

    End Function

    Private Function IsNumericValue(ByVal value As Object) As Boolean

        Return TypeOf value Is Byte OrElse
               TypeOf value Is SByte OrElse
               TypeOf value Is Short OrElse
               TypeOf value Is UShort OrElse
               TypeOf value Is Integer OrElse
               TypeOf value Is UInteger OrElse
               TypeOf value Is Long OrElse
               TypeOf value Is ULong OrElse
               TypeOf value Is Single OrElse
               TypeOf value Is Double OrElse
               TypeOf value Is Decimal

    End Function

    Private Function GetGridCellText(ByVal row As DataGridViewRow, ByVal columnName As String) As String

        If row Is Nothing OrElse IMPerf_DGV.Columns.Contains(columnName) = False Then Return ""
        If row.Cells(columnName).Value Is Nothing OrElse IsDBNull(row.Cells(columnName).Value) Then Return ""

        Return row.Cells(columnName).Value.ToString()

    End Function

    Private Function GetGridColumnTotal(ByVal columnName As String) As Double

        Dim total As Double = 0

        If IMPerf_DGV Is Nothing OrElse IMPerf_DGV.Columns.Contains(columnName) = False Then Return total

        For Each row As DataGridViewRow In IMPerf_DGV.Rows
            If row.IsNewRow Then Continue For
            total += ToDoubleValue(row.Cells(columnName).Value)
        Next

        Return total

    End Function

    Private Function ToDoubleValue(ByVal value As Object) As Double

        If value Is Nothing OrElse IsDBNull(value) Then Return 0

        If IsNumericValue(value) Then Return Convert.ToDouble(value, CultureInfo.CurrentCulture)

        Dim number As Double
        Dim textValue As String = value.ToString()

        If Double.TryParse(textValue, NumberStyles.Any, CultureInfo.CurrentCulture, number) Then Return number
        If Double.TryParse(textValue, NumberStyles.Any, CultureInfo.InvariantCulture, number) Then Return number

        Return 0

    End Function

    Private Function GetItemProfitPrintableRowsCount() As Integer

        Dim count As Integer = 0

        For Each row As DataGridViewRow In IMPerf_DGV.Rows
            If row.IsNewRow = False Then count += 1
        Next

        Return count

    End Function

    Private Function GetItemProfitFilterText() As String

        Return "الفترة: من " & HOME.DateRange_Flate.D_F.Value.ToShortDateString & " إلى " & HOME.DateRange_Flate.D_T.Value.ToShortDateString &
               "    المخزن: " & ST_cm.Text &
               "    التصنيف: " & PerfetGM_Serach.Text &
               "    نوع المبيعات: " & Sales_Type_Cm.Text

    End Function

    Private Function GetPdfPrinterName() As String

        For Each printerName As String In PrinterSettings.InstalledPrinters
            If printerName.IndexOf("Microsoft Print to PDF", StringComparison.OrdinalIgnoreCase) >= 0 Then Return printerName
        Next

        Return ""

    End Function

    Private Sub EXCEL_BTN_Click(sender As Object, e As EventArgs) Handles EXCEL_BTN.Click
        EXCEL_EXPORT(IMPerf_DGV)
    End Sub

    Private Sub EXCEL_EXPORT(gridv As DataGridView)
        Try
            Const stXL_SUBKEY As String = "\Excel.Application\CurVer"
            Dim rkVersionKey As Microsoft.Win32.RegistryKey = Nothing
            rkVersionKey = Registry.ClassesRoot.OpenSubKey(name:=stXL_SUBKEY, writable:=False)
            If rkVersionKey Is Nothing Then
                'not installed
                MessageBox.Show("الرجاء تثبيت حزمة Microsoft Office أولا", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                If gridv.RowCount > 0 Then
                    Dim xlApp As Object
                    Dim xlWorkBook As Object
                    Dim xlWorkSheet As Object
                    Dim misValue As Object = System.Reflection.Missing.Value
                    Dim i As Integer
                    Dim j As Integer
                    ' xlApp = New Excel.Application
                    xlApp = CreateObject("Excel.Application")
                    xlWorkBook = xlApp.Workbooks.Add(misValue)
                    xlWorkSheet = xlWorkBook.Sheets(1)
                    For Each col As DataGridViewColumn In gridv.Columns
                        If col.Visible Then
                            xlWorkSheet.Cells(1, col.Index + 2) = col.HeaderText.ToString
                        End If
                    Next
                    For i = 0 To gridv.Rows.Count - 1
                        For j = 0 To gridv.ColumnCount - 1
                            If gridv.Columns(j).Visible Then
                                xlWorkSheet.Cells(i + 2, j + 2) = gridv(j, i).Value.ToString()
                            End If
                        Next
                    Next
                    xlApp.Visible = True
                    xlWorkBook.Activate()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
            'Class1.con.Close()
        End Try
    End Sub
End Class
