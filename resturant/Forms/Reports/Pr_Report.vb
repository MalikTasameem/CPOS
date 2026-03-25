Imports System.Data.SqlClient
Imports System.Globalization
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class Pr_Report

    Dim rs As New Resizer
    Dim ag_Dt As New DataTable
    Private Sub Empty_F_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
    End Sub


    Private Sub Empty_F_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
        Zuby.ADGV.AdvancedDataGridView.SetTranslations(Zuby.ADGV.AdvancedDataGridView.LoadTranslationsFromFile(Application.StartupPath & "\" & "lang.json"))
    End Sub

    Private Sub InSale_Search_btn_Click(sender As Object, e As EventArgs) Handles InSale_Search_btn.Click
        DataB.Dispose()
        DataB = New BindingSource
        gridv.DataSource = Nothing
        ag_Dt = New DataTable
        InSale_SelectDetails_By_Date()
    End Sub

    Private Sub InSale_SelectDetails_By_Date()
 

        Dim Main_Query, TABLE_NAME, WHERE_STR As String

        TABLE_NAME = " [Periods_ALL_V] "

        WHERE_STR = " WHERE 1=1  "
        Dim C As New C

        Main_Query = "SELECT '' AS ' ت ',UserName AS '     المستخدم',Pr_Start AS '     البداية',Pr_End AS '     النهاية',CASH_IN AS '    المقبوض',CASH_OUT as '   المصروف ',WORK_TIME as '   الفترة ',NotesOn_Start AS '     ملاحظة عند البدء', " & _
            " NotesOn_End AS '     ملاحظة عند الإنهاء',AG_Counter AS '     المعملات' "
        If S_Tables = True Then
            Main_Query = Main_Query & "  ,Debit AS '   المديونية  ',Flate_Name  AS '   المكان '"
        End If

        Main_Query = Main_Query & " FROM " & TABLE_NAME


        WHERE_STR = WHERE_STR & " AND CONVERT(DATE,Pr_Start) BETWEEN   CONVERT(DATE,'" & HOME.DateRange_Flate.D_From.Text & "') AND CONVERT(DATE,'" & HOME.DateRange_Flate.D_To.Text & "')   "

        'If InSale_ST_cm.SelectedValue > 0 Then WHERE_STR = WHERE_STR & " AND ST_ID = " & InSale_ST_cm.SelectedValue

        'If Emp_FS.TXT_ID.Text > 0 Then WHERE_STR = WHERE_STR & " AND AG_ID = " & Emp_FS.TXT_ID.Text

        'If U_CAT_Str.Count > 0 Then
        '    WHERE_STR = WHERE_STR & " AND IM_CAT_ID IN ( " & U_CAT_Str & ") "
        'Else
        '    MsgBox("لم يتم تحديد فئة للصلاحية", MsgBoxStyle.Critical, "صلاحية الأصناف")
        '    Exit Sub
        'End If

        'If SHOW_ALL_SYS = False Then WHERE_STR = WHERE_STR & " AND T_ID BETWEEN " & START_ID & " AND " & END_ID

        'WHERE_STR = WHERE_STR & " Group By Item_name,U_Name,St_Name,AG_NAME,IM_CAT,IM_COMPANY "

        Main_Query = Main_Query & WHERE_STR

        C.Da = New SqlClient.SqlDataAdapter(Main_Query, C.Con)
        C.Da.SelectCommand.CommandTimeout = 120
        C.Da.Fill(ag_Dt)
        DataB.DataSource = ag_Dt
        gridv.DataSource = DataB
        Index_GV()

        gridv.Columns(4).Tag = 1
        gridv.Columns(5).Tag = 1
        gridv.Columns(9).Tag = 1

        If ag_Dt.Rows.Count = 0 Then
            MsgBox("لا توجد عناصر للعرض", MsgBoxStyle.Exclamation, "")
        Else
            gridv.Columns(0).Visible = False

            CheckedListBox1.Items.Clear()
            For i As Integer = 0 To gridv.ColumnCount - 1
                Dim CL = gridv.Columns(i).Name
                CheckedListBox1.Items.Add(CL)
                gridv.Columns(i).Width = 150
                gridv.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next
        End If
    End Sub

    Private Sub gridv_FilterStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.FilterEventArgs) Handles gridv.FilterStringChanged
        DataB.Filter = gridv.FilterString
        Index_GV()
    End Sub

    Private Sub gridv_SortStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.SortEventArgs) Handles gridv.SortStringChanged
        DataB.Sort = gridv.SortString
        Index_GV()
    End Sub

    Private Sub Index_GV()
        For i = 0 To gridv.Rows.Count - 1
            gridv.Rows(i).Cells(0).Value = i + 1
        Next
    End Sub


    Private Sub InSale_print_btn_Click(sender As Object, e As EventArgs) Handles InSale_print_btn.Click
        Dim P As New Print_PDF
        P.PRINT_PDF_List(gridv, CheckedListBox1, " تقرير ورديات العمل من تاريخ " & HOME.DateRange_Flate.D_From.Text & " إلى " & HOME.DateRange_Flate.D_To.Text, 1)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PrintSales_Small()
    End Sub

    Private Sub PrintSales_Small()
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\Pr_Report_80.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, USER_NAME)
            .rp.SetParameterValue(1, SBill_Title_1)
            .rp.SetParameterValue(2, SBill_Title_2)
            .rp.SetParameterValue(3, HOME.DateRange_Flate.D_From.Text)
            .rp.SetParameterValue(4, HOME.DateRange_Flate.D_To.Text)


            .rp.SetParameterValue(5, " تقرير ورديات العمل في الفترة  " & vbNewLine & " من " & HOME.DateRange_Flate.D_From.Text & " إلى " & HOME.DateRange_Flate.D_To.Text)

        End With


        'If My_Settings.Pr_Printer_isShow = True Then
        '    Dim p As New print
        '    p.CrystalReportViewer1.ReportSource = pp.rp
        '    p.Show()
        'Else
        If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_80))
        pp.rp.PrintOptions.PrinterName = Default_Printer_80
        pp.rp.PrintToPrinter(1, False, 0, 0)
        pp.rp.Dispose()
        ' End If

    End Sub
End Class