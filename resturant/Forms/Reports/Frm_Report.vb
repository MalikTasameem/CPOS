Imports System.Data.SqlClient
Imports System.Globalization
Public Class Frm_Report

    Dim rs As New Resizer
    Dim IM_DT_1, IM_DT_2 As New DataTable

    Private Sub Empty_F_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        Zuby.ADGV.AdvancedDataGridView.SetTranslations(Zuby.ADGV.AdvancedDataGridView.LoadTranslationsFromFile(Application.StartupPath & "\" & "lang.json"))
        'Zuby.ADGV.AdvancedDataGridViewSearchToolBar.SetTranslations(Zuby.ADGV.AdvancedDataGridViewSearchToolBar.LoadTranslationsFromFile(Application.StartupPath & "\" & "lang.json"))
        fetch_ST()
    End Sub

    Private Sub Empty_F_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub


    Public Sub fetch_ST()
        Frm_ST_cm.DataSource = Ge_St_Items()
        Frm_ST_cm.DisplayMember = "name"
        Frm_ST_cm.ValueMember = "ID"
        Frm_ST_cm.SelectedIndex = 0

    End Sub
    Private Sub Frm_Search_Btn_Click(sender As Object, e As EventArgs) Handles Frm_Search_Btn.Click
        Frm_SelectDetails_By_Date()
    End Sub

    Private Sub Frm_SelectDetails_By_Date()
        'Dim C = New C
        'With (C.Com)
        '    .Connection = C.Con
        '    .CommandText = "[FRM_Details_SelectDetails_By_Date]"
        '    .CommandType = CommandType.StoredProcedure
        '    .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
        '    .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
        '    .Parameters.AddWithValue("@ST_ID", Frm_ST_cm.SelectedValue)
        'End With
        'C.Da = New SqlDataAdapter(C.Com)
        'C.Da.Fill(C.Dt)
        'Frm_Dt_DGV.DataSource = C.Dt
        '--------------------------------------------------------------------------------------------
        'Dim C2 = New C
        'With (C2.Com)
        '    .Connection = C2.Con
        '    .CommandText = "[FRM_Contents_SelectDetails_By_Date]"
        '    .CommandType = CommandType.StoredProcedure
        '    .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
        '    .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
        '    .Parameters.AddWithValue("@ST_ID", Frm_ST_cm.SelectedValue)
        'End With
        'C2.Da = New SqlDataAdapter(C2.Com)
        'C2.Da.Fill(C2.Dt)
        'Frm_Cn_DGV.DataSource = C2.Dt
        '-----------------------------------------------------------------------------------------------

        fun_1()
        fun_2()





    End Sub

    Private Sub fun_1()
        DataB_1.Dispose()
        DataB_1 = New BindingSource
        Frm_Dt_DGV_2.DataSource = Nothing
        IM_DT_1 = New DataTable



        Dim Main_Query As String
        Dim C As New C

        Main_Query = "SELECT '' as 'ت',CONVERT(Date,Date) AS 'التاريخ',St_Name  as 'المخزن',Emp_Name AS '  الموظــف ',Ag_name as ' الزبون  ',SB_ID as 'رقم فاتورة المبيعات',Item_name  as 'الصنــف', " &
            "U_Name as 'الوحدة',QYT AS 'العدد',NewSale as ' سعر البيع ', (QYT * NewSale) AS ' إجمالي السعر '  FROM FRM_Details_V "
        Main_Query = Main_Query & " WHERE isDepended = 1 "
        If Frm_ST_cm.SelectedIndex > 0 Then Main_Query = Main_Query & " AND ST_ID = " & Frm_ST_cm.SelectedValue
        If AG_Cm.TXT_ID.Text > 0 Then Main_Query = Main_Query & " AND Cr_ID = " & AG_Cm.TXT_ID.Text

        Main_Query = Main_Query & " and CONVERT(Date,Date) BETWEEN '" & HOME.DateRange_Flate.D_F.Text & "' AND '" & HOME.DateRange_Flate.D_T.Text & "' "

        '    .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
        '    .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)

        Main_Query = Main_Query & " ORDER BY DATE ASC "

        C.Da = New SqlClient.SqlDataAdapter(Main_Query, C.Con)
        C.Da.SelectCommand.CommandTimeout = 120
        C.Da.Fill(IM_DT_1)
        DataB_1.DataSource = IM_DT_1
        Frm_Dt_DGV_2.DataSource = DataB_1




        If IM_DT_1.Rows.Count > 0 Then


            Frm_Dt_DGV_2.Columns(8).Tag = 1
            Frm_Dt_DGV_2.Columns(9).Tag = 1
            Frm_Dt_DGV_2.Columns(10).Tag = 1
            'Frm_Dt_DGV_2.Columns(13).Tag = 1
            'Frm_Dt_DGV_2.Columns(14).Tag = 1


            Frm_Dt_DGV_2.Columns(9).DefaultCellStyle.Format = "N3"
            Frm_Dt_DGV_2.Columns(10).DefaultCellStyle.Format = "N3"
            'Frm_Dt_DGV_2.Columns(14).DefaultCellStyle.Format = "N3"

            Index_GV_1()


        End If



    End Sub

    Private Sub Frm_Dt_DGV_2_FilterStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.FilterEventArgs) Handles Frm_Dt_DGV_2.FilterStringChanged
        DataB_1.Filter = Frm_Dt_DGV_2.FilterString
        Index_GV_1()
        'GET_TOTALS_GRID(gridv)
    End Sub

    Private Sub Frm_Dt_DGV_2_SortStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.SortEventArgs) Handles Frm_Dt_DGV_2.SortStringChanged
        DataB_1.Sort = Frm_Dt_DGV_2.SortString
        Index_GV_1()
        'GET_TOTALS_GRID(gridv)
    End Sub

    Private Sub Index_GV_1()
        For i = 0 To Frm_Dt_DGV_2.Rows.Count - 1
            Frm_Dt_DGV_2.Rows(i).Cells(0).Value = i + 1
        Next
        'textBox_total.Text = Frm_Dt_DGV_2.Rows.Count.ToString
    End Sub

    '*********************************************************************************************************************************************************************
    Private Sub fun_2()
        DataB_2.Dispose()
        DataB_2 = New BindingSource
        Frm_Cn_DGV_2.DataSource = Nothing
        IM_DT_2 = New DataTable


        Dim Main_Query As String
        Dim C As New C

        Main_Query = "SELECT '' as 'ت',CONVERT(Date,Date) AS 'التاريخ',St_Name  as 'المخزن',Emp_Name AS '  الموظــف ',Ag_name as ' الزبون  ',SB_ID as 'رقم فاتورة المبيعات',Item_name  as 'الصنــف',U_Name as 'الوحدة',QYT AS 'العدد'  FROM FRM_Contents_Manuel_V "
        Main_Query = Main_Query & " WHERE isDepended = 1 "

        If Frm_ST_cm.SelectedIndex > 0 Then Main_Query = Main_Query & " AND ST_ID = " & Frm_ST_cm.SelectedValue
        If AG_Cm.TXT_ID.Text > 0 Then Main_Query = Main_Query & " AND Cr_ID = " & AG_Cm.TXT_ID.Text
        Main_Query = Main_Query & " and CONVERT(Date,Date) BETWEEN '" & HOME.DateRange_Flate.D_F.Text & "' AND '" & HOME.DateRange_Flate.D_T.Text & "' "

        Main_Query = Main_Query & " ORDER BY DATE ASC "

        C.Da = New SqlClient.SqlDataAdapter(Main_Query, C.Con)
        C.Da.SelectCommand.CommandTimeout = 120
        C.Da.Fill(IM_DT_2)
        DataB_2.DataSource = IM_DT_2
        Frm_Cn_DGV_2.DataSource = DataB_2




        If IM_DT_2.Rows.Count > 0 Then


            Frm_Cn_DGV_2.Columns(8).Tag = 1
            'Frm_Cn_DGV_2.Columns(9).Tag = 1
            'Frm_Cn_DGV_2.Columns(12).Tag = 1
            'Frm_Cn_DGV_2.Columns(13).Tag = 1
            'Frm_Cn_DGV_2.Columns(14).Tag = 1


            Frm_Cn_DGV_2.Columns(8).DefaultCellStyle.Format = "N3"
            'Frm_Cn_DGV_2.Columns(13).DefaultCellStyle.Format = "N3"
            'Frm_Cn_DGV_2.Columns(14).DefaultCellStyle.Format = "N3"

            Index_GV_2()


        End If

    End Sub

    Private Sub fun_3()
        DataB_2.Dispose()
        DataB_2 = New BindingSource
        Frm_Cn_DGV_2.DataSource = Nothing
        IM_DT_2 = New DataTable


        Dim Main_Query As String
        Dim C As New C

        Main_Query = "SELECT '' as 'ت',CONVERT(Date,Date) AS 'التاريخ',St_Name  as 'المخزن',Emp_Name AS '  الموظــف ',Ag_name as ' الزبون  ',SB_ID as 'رقم فاتورة المبيعات',Item_name  as 'الصنــف',U_Name as 'الوحدة',QYT AS 'العدد'  FROM FRM_Contents_Manuel_V "
        Main_Query = Main_Query & " WHERE isDepended = 1 "

        If Frm_ST_cm.SelectedIndex > 0 Then Main_Query = Main_Query & " AND ST_ID = " & Frm_ST_cm.SelectedValue
        If AG_Cm.TXT_ID.Text > 0 Then Main_Query = Main_Query & " AND Cr_ID = " & AG_Cm.TXT_ID.Text
        Main_Query = Main_Query & " and CONVERT(Date,Date) BETWEEN '" & HOME.DateRange_Flate.D_F.Value & "' AND '" & HOME.DateRange_Flate.D_T.Value & "' "

        Main_Query = Main_Query & " ORDER BY DATE ASC "

        C.Da = New SqlClient.SqlDataAdapter(Main_Query, C.Con)
        C.Da.SelectCommand.CommandTimeout = 120
        C.Da.Fill(IM_DT_2)
        DataB_2.DataSource = IM_DT_2
        Frm_Cn_DGV_2.DataSource = DataB_2




        If IM_DT_2.Rows.Count > 0 Then


            Frm_Cn_DGV_2.Columns(8).Tag = 1
            'Frm_Cn_DGV_2.Columns(9).Tag = 1
            'Frm_Cn_DGV_2.Columns(12).Tag = 1
            'Frm_Cn_DGV_2.Columns(13).Tag = 1
            'Frm_Cn_DGV_2.Columns(14).Tag = 1


            Frm_Cn_DGV_2.Columns(8).DefaultCellStyle.Format = "N3"
            'Frm_Cn_DGV_2.Columns(13).DefaultCellStyle.Format = "N3"
            'Frm_Cn_DGV_2.Columns(14).DefaultCellStyle.Format = "N3"

            Index_GV_2()


        End If

    End Sub

    Private Sub Frm_Cn_DGV_2_FilterStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.FilterEventArgs) Handles Frm_Cn_DGV_2.FilterStringChanged
        DataB_2.Filter = Frm_Cn_DGV_2.FilterString
        Index_GV_2()
        'GET_TOTALS_GRID(gridv)
    End Sub

    Private Sub Frm_Cn_DGV_2_SortStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.SortEventArgs) Handles Frm_Cn_DGV_2.SortStringChanged
        DataB_2.Sort = Frm_Cn_DGV_2.SortString
        Index_GV_2()
        'GET_TOTALS_GRID(gridv)
    End Sub

    Private Sub Index_GV_2()
        For i = 0 To Frm_Cn_DGV_2.Rows.Count - 1
            Frm_Cn_DGV_2.Rows(i).Cells(0).Value = i + 1
        Next
        'textBox_total.Text = Frm_Dt_DGV_2.Rows.Count.ToString
    End Sub




    Private Sub Print_Frm_Dt_Btn_Click(sender As Object, e As EventArgs) Handles Print_Frm_Dt_Btn.Click
        'Print_Frm(1)
        Dim p As New Print_PDF
        p.PRINT_PDF(Frm_Dt_DGV_2, 1, "( كشف حركة المنتجات النهائية ) / " & " من تاريخ " & HOME.DateRange_Flate.D_F.Value.ToShortDateString & " إلى " & HOME.DateRange_Flate.D_T.Value.ToShortDateString)
    End Sub

    Private Sub Print_Frm_Cn_Btn_Click(sender As Object, e As EventArgs) Handles Print_Frm_Cn_Btn.Click
        'Print_Frm(2)
        Dim p As New Print_PDF
        p.PRINT_PDF(Frm_Cn_DGV_2, 1, "( كشف حركة المواد الإستهلاكية ) / " & " من تاريخ " & HOME.DateRange_Flate.D_F.Value.ToShortDateString & " إلى " & HOME.DateRange_Flate.D_T.Value.ToShortDateString)
    End Sub

    Private Sub Print_Frm(prt_Type As Integer)
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\FRM_Details_A4.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, " من تاريخ " + HOME.DateRange_Flate.D_F.Value.ToShortDateString + " إلى " + HOME.DateRange_Flate.D_T.Value.ToShortDateString)
            .rp.SetParameterValue(1, USER_NAME)
            .rp.SetParameterValue(2, My_Settings.Server_Desc)
            .rp.SetParameterValue(3, HOME.DateRange_Flate.D_F.Value)
            .rp.SetParameterValue(4, HOME.DateRange_Flate.D_T.Value)
            .rp.SetParameterValue(5, Frm_ST_cm.SelectedValue)
            .rp.SetParameterValue(6, Frm_ST_cm.Text)
            .rp.SetParameterValue(7, prt_Type)

            If prt_Type = 1 Then
                .rp.SetParameterValue(8, "كشف حركة المواد المصنعه")
            Else
                .rp.SetParameterValue(8, "كشف حركة المواد الإستهلاكية")
            End If


        End With

        Dim p As New print
        p.CrystalReportViewer1.ReportSource = pp.rp
        p.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_80
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()

    End Sub
End Class