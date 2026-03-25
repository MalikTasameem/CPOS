Imports System.Data.SqlClient

Public Class SearchAgent_Pch_Bill : Inherits System.Windows.Forms.Form
    Dim rs As New Resizer
    Dim Bills_DT As New DataTable
    Dim Dv As New DataView
    Dim BalanceType As String = ""
    'Dim AG_ID As Integer

    Private Sub ExpSearch_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub


    Private Sub ExpSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)

        Zuby.ADGV.AdvancedDataGridView.SetTranslations(Zuby.ADGV.AdvancedDataGridView.LoadTranslationsFromFile(Application.StartupPath & "\" & "lang.json"))
        Zuby.ADGV.AdvancedDataGridViewSearchToolBar.SetTranslations(Zuby.ADGV.AdvancedDataGridViewSearchToolBar.LoadTranslationsFromFile(Application.StartupPath & "\" & "lang.json"))

        Markter_Cm.Visible = S_Marketers
        Marketer_Lb.Visible = S_Marketers

        'For i = 0 To AGMetroGrid.Columns.Count - 1
        '    AGMetroGrid.Columns.Item(i).SortMode = DataGridViewColumnSortMode.NotSortable
        'Next i

        Bill_cmb.SelectedIndex = MY_Settings.AG_SH_Bill_Type

        '  Me.WindowState = FormWindowState.Maximized

        RPT_CM.SelectedIndex = 0
        AG_Cm.Focus()
    End Sub

    Public Sub Load_Data_2()

        Dim c As New C

        bindingSource_main.Dispose()
        bindingSource_main = New BindingSource
        advancedDataGridView_main.DataSource = Nothing
        Bills_DT = New DataTable

        Try
            Bills_DT.Clear()
            Dim Main_Query As String
            Main_Query = "SELECT Ag_name AS ' الحساب ',SUM(TOTAL) AS ' الإجمالي ',SUM(Discount) AS ' التخفيض ',SUM(Cost) AS ' الصافي ', SUM(Total_Pied) AS ' المدفوع ',SUM(Rest) AS ' الباقي '" & _
             " from Pch_Balance_MV_V "
            Dim middle As String = " where 1=1 "
            middle = middle & " GROUP BY Ag_name "
            Dim last As String = " order by Ag_name ASC "
            'If AG_Cm.TXT_ID.Text > 0 Then middle = middle & " AND AG_ID = '" & AG_Cm.TXT_ID.Text & "' "
            'Select Case Bill_cmb.SelectedIndex
            '    Case 0
            '        middle = middle & " and isVoid = '" & Convert.ToInt16(isDeletedCheckBox.Checked) & "' "
            '    Case 1
            '        middle = middle & " and isVoid = '" & Convert.ToInt16(isDeletedCheckBox.Checked) & "' AND isPied = 1 "
            '    Case 2
            '        middle = middle & " and isVoid = '" & Convert.ToInt16(isDeletedCheckBox.Checked) & "' AND isPied = 0 "
            'End Select

            'If Markter_Cm.TXT_ID.Text > 1 Then middle = middle & " and Markter_ID = " & Markter_Cm.TXT_ID.Text
            'If ALL_time_CheckBox.Checked = False Then middle = middle & " AND CONVERT(DATE,DATE) BETWEEN   CONVERT(DATE,'" & DateRange_Flate.D_From.Text & "') AND CONVERT(DATE,'" & DateRange_Flate.D_To.Text & "')   "

            Main_Query = Main_Query & middle & last
            c.Da = New SqlClient.SqlDataAdapter(Main_Query, c.Con)



            c.Da.SelectCommand.CommandTimeout = 120

            c.Da.Fill(Bills_DT)
            bindingSource_main.DataSource = Bills_DT
            advancedDataGridView_main.DataSource = bindingSource_main

            advancedDataGridView_main.Columns(1).Tag = 1
            advancedDataGridView_main.Columns(2).Tag = 1
            advancedDataGridView_main.Columns(3).Tag = 1
            advancedDataGridView_main.Columns(4).Tag = 1
            advancedDataGridView_main.Columns(5).Tag = 1



            If Bills_DT.Rows.Count = 0 Then
                MsgBox("لا توجد عناصر للعرض", MsgBoxStyle.Exclamation, "")
            Else

                CheckedListBox1.Items.Clear()
                For i As Integer = 0 To advancedDataGridView_main.ColumnCount - 1
                    Dim CL = advancedDataGridView_main.Columns(i).HeaderText
                    CheckedListBox1.Items.Add(CL)

                    advancedDataGridView_main.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Next
            End If


            advancedDataGridViewSearchToolBar_main.SetColumns(advancedDataGridView_main.Columns)


            'Coloring()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Load_Data()

        'Bills_DT.Clear()
        'Dim C As New C
        'With (C.Com)
        '    .Connection = C.Con
        '    .CommandText = "[Balance_MV_V_SELECT_AG_Bills]"
        '    .CommandType = CommandType.StoredProcedure
        '    'If Unpied_CB.Checked = True Then
        '    '    .Parameters.AddWithValue("@isPied", 0)
        '    'Else
        '    .Parameters.AddWithValue("@Type", Bill_cmb.SelectedIndex)
        '    'End If

        '    .Parameters.AddWithValue("@isVoid", isDeletedCheckBox.Checked)
        '    .Parameters.AddWithValue("@AG_ID", AG_ID)
        'End With
        'C.Da = New SqlClient.SqlDataAdapter(C.Com)
        'C.Da.Fill(Bills_DT)
        'AGMetroGrid.DataSource = Bills_DT
        'Coloring()
        '--------------------------------------------------------------------------------
        Dim c As New C

        bindingSource_main.Dispose()
        bindingSource_main = New BindingSource
        advancedDataGridView_main.DataSource = Nothing
        Bills_DT = New DataTable

        Try
            Bills_DT.Clear()
            Dim Main_Query As String
            Main_Query = "SELECT ROW_NUMBER() OVER(ORDER BY Date DESC) AS ' ت ',T_ID,Date AS ' التاريخ ',UserName AS '  المستخدم ',Bill_ID AS ' رقم الفاتورة ',B_Name AS ' النوع ',Ag_name AS ' الحساب ',Proj_NAME AS ' الزبون 2 ',TOTAL AS ' الإجمالي ',Discount AS ' التخفيض ',Cost AS ' الصافي ', " &
                "Total_Pied AS ' المدفوع ',Rest AS ' الباقي ',Name_ AS '  ',isVoid,isPied,PIED_Name AS ' الخلاص' " &
            "from Pch_Balance_MV_V "
            Dim middle As String = " where 1=1 "
            Dim last As String = " order by Date DESC "
            If AG_Cm.TXT_ID.Text > 0 Then middle = middle & " AND AG_ID = '" & AG_Cm.TXT_ID.Text & "' "
            Select Case Bill_cmb.SelectedIndex
                Case 0
                    middle = middle & " and isVoid = '" & Convert.ToInt16(isDeletedCheckBox.Checked) & "' "
                Case 1
                    middle = middle & " and isVoid = '" & Convert.ToInt16(isDeletedCheckBox.Checked) & "' AND isPied = 1 "
                Case 2
                    middle = middle & " and isVoid = '" & Convert.ToInt16(isDeletedCheckBox.Checked) & "' AND isPied = 0 "
            End Select

            If Markter_Cm.TXT_ID.Text > 1 Then middle = middle & " and Markter_ID = " & Markter_Cm.TXT_ID.Text
            If ALL_time_CheckBox.Checked = False Then middle = middle & " AND CONVERT(DATE,DATE) BETWEEN   CONVERT(DATE,'" & DateRange_Flate.D_From.Text & "') AND CONVERT(DATE,'" & DateRange_Flate.D_To.Text & "')   "

            Main_Query = Main_Query & middle & last
            c.Da = New SqlClient.SqlDataAdapter(Main_Query, c.Con)


            '---------------------------------------------------------------------------------------------------------------------------------------------
            'Dim C As New C

            'With (C.Com)
            '    .Connection = C.Con
            '    .CommandText = "[Search_AG_Bill_AAAAAAAAAAA]"
            '    .CommandType = CommandType.StoredProcedure
            '    .Parameters.AddWithValue("@AllTimeCheckBox", ALL_time_CheckBox.Checked)
            '    '.Parameters.AddWithValue("@isVoid", isDeletedCheckBox.Checked)

            '    .Parameters.AddWithValue("@DateTimePicker_From", DateRange_Flate.D_From.Text)
            '    .Parameters.AddWithValue("@DateTimePicker_To", DateRange_Flate.D_To.Text)
            '    .Parameters.AddWithValue("@AG_ID", AG_Cm.TXT_ID.Text)
            '    .Parameters.AddWithValue("@Markter_ID", AG_Cm.TXT_ID.Text)
            '    .Parameters.AddWithValue("@Bill_cmb", Bill_cmb.SelectedIndex)
            '    .Parameters.AddWithValue("@isDeletedCheckBox", isDeletedCheckBox.Checked)

            'End With

            'c.Da = New SqlClient.SqlDataAdapter(c.Com)
            '---------------------------------------------------------------------------------------------------------------------------------------------


            c.Da.SelectCommand.CommandTimeout = 120

            c.Da.Fill(Bills_DT)
            bindingSource_main.DataSource = Bills_DT
            advancedDataGridView_main.DataSource = bindingSource_main

            advancedDataGridView_main.Columns(8).Tag = 1
            advancedDataGridView_main.Columns(9).Tag = 1
            advancedDataGridView_main.Columns(10).Tag = 1
            advancedDataGridView_main.Columns(11).Tag = 1
            advancedDataGridView_main.Columns(12).Tag = 1
            advancedDataGridView_main.Columns(13).Tag = 1


            If Bills_DT.Rows.Count = 0 Then
                MsgBox("لا توجد عناصر للعرض", MsgBoxStyle.Exclamation, "")
            Else
                advancedDataGridView_main.Columns(1).Visible = False
                advancedDataGridView_main.Columns(14).Visible = False
                advancedDataGridView_main.Columns(15).Visible = False

                'advancedDataGridView_main.Columns(16).Visible = False

                CheckedListBox1.Items.Clear()
                For i As Integer = 0 To advancedDataGridView_main.ColumnCount - 1
                    Dim CL = advancedDataGridView_main.Columns(i).HeaderText
                    CheckedListBox1.Items.Add(CL)

                    advancedDataGridView_main.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Next
            End If


            advancedDataGridViewSearchToolBar_main.SetColumns(advancedDataGridView_main.Columns)


            'Coloring()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    'Private Sub Coloring()
    '    For i = 0 To AGMetroGrid.Rows.Count - 1

    '        If AGMetroGrid.Rows(i).Cells("isDeleted_CL").Value = 0 Then

    '            Select Case AGMetroGrid.Rows(i).Cells("isPied_CL").Value
    '                Case 0
    '                    AGMetroGrid.Rows(i).Cells(0).Style.BackColor = Color.LightGray
    '                    AGMetroGrid.Rows(i).Cells(1).Style.BackColor = Color.LightGray
    '                    AGMetroGrid.Rows(i).Cells(2).Style.BackColor = Color.LightGray
    '                    AGMetroGrid.Rows(i).Cells(3).Style.BackColor = Color.LightGray
    '                    AGMetroGrid.Rows(i).Cells(4).Style.BackColor = Color.LightGray
    '                    AGMetroGrid.Rows(i).Cells(5).Style.BackColor = Color.LightGray
    '                    AGMetroGrid.Rows(i).Cells(6).Style.BackColor = Color.LightGray
    '                    AGMetroGrid.Rows(i).Cells(7).Style.BackColor = Color.LightGray
    '                    AGMetroGrid.Rows(i).Cells(8).Style.BackColor = Color.LightGray
    '                    AGMetroGrid.Rows(i).Cells(9).Style.BackColor = Color.LightGray
    '                    AGMetroGrid.Rows(i).Cells(10).Style.BackColor = Color.LightGray
    '                Case 1
    '                    AGMetroGrid.Rows(i).Cells(0).Style.BackColor = Color.LightGreen
    '                    AGMetroGrid.Rows(i).Cells(1).Style.BackColor = Color.LightGreen
    '                    AGMetroGrid.Rows(i).Cells(2).Style.BackColor = Color.LightGreen
    '                    AGMetroGrid.Rows(i).Cells(3).Style.BackColor = Color.LightGreen
    '                    AGMetroGrid.Rows(i).Cells(4).Style.BackColor = Color.LightGreen
    '                    AGMetroGrid.Rows(i).Cells(5).Style.BackColor = Color.LightGreen
    '                    AGMetroGrid.Rows(i).Cells(6).Style.BackColor = Color.LightGreen
    '                    AGMetroGrid.Rows(i).Cells(7).Style.BackColor = Color.LightGreen
    '                    AGMetroGrid.Rows(i).Cells(8).Style.BackColor = Color.LightGreen
    '                    AGMetroGrid.Rows(i).Cells(9).Style.BackColor = Color.LightGreen
    '                    AGMetroGrid.Rows(i).Cells(10).Style.BackColor = Color.LightGreen
    '            End Select


    '        Else
    '            AGMetroGrid.Rows(i).Cells(0).Style.BackColor = Color.IndianRed
    '            AGMetroGrid.Rows(i).Cells(1).Style.BackColor = Color.IndianRed
    '            AGMetroGrid.Rows(i).Cells(2).Style.BackColor = Color.IndianRed
    '            AGMetroGrid.Rows(i).Cells(3).Style.BackColor = Color.IndianRed
    '            AGMetroGrid.Rows(i).Cells(4).Style.BackColor = Color.IndianRed
    '            AGMetroGrid.Rows(i).Cells(5).Style.BackColor = Color.IndianRed
    '            AGMetroGrid.Rows(i).Cells(6).Style.BackColor = Color.IndianRed
    '            AGMetroGrid.Rows(i).Cells(7).Style.BackColor = Color.IndianRed
    '            AGMetroGrid.Rows(i).Cells(8).Style.BackColor = Color.IndianRed
    '            AGMetroGrid.Rows(i).Cells(9).Style.BackColor = Color.IndianRed
    '            AGMetroGrid.Rows(i).Cells(10).Style.BackColor = Color.IndianRed
    '        End If

    '    Next
    'End Sub


    Private Sub ExpSearch_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub AGMetroGrid_KeyDown(sender As Object, e As KeyEventArgs) Handles advancedDataGridView_main.KeyDown
        If e.KeyCode = Keys.Return Then Move_To_Select()
    End Sub

    Private Sub AGMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles advancedDataGridView_main.MouseDoubleClick
        Move_To_Select()
    End Sub

    Private Sub Move_To_Select()
        If advancedDataGridView_main.Rows.Count > 0 Then
            isShowing_Trans = True
            F_Pch = New Pch
            T_ID_Trans = advancedDataGridView_main.CurrentRow.Cells(1).Value
            'F_Pch.عرضفواتيرالزبونToolStripMenuItem.Visible = False
            F_Pch.BillNumPanel.Enabled = False
            F_Pch.ShowDialog()
            isShowing_Trans = False

        End If

    End Sub


    Private Sub isDeletedCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles isDeletedCheckBox.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Bill_cmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Bill_cmb.SelectedIndexChanged
        MY_Settings.AG_SH_Bill_Type = Bill_cmb.SelectedIndex
        Save_AppSetting()
    End Sub

    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        'Print()
        '-------------------------
        'AG_MV_print_Reset()
        'AG_MV_print()
        'AG_MV_print_Reset()

        Dim P As New Print_PDF
        P.PRINT_PDF_List(advancedDataGridView_main, CheckedListBox1, TITLE_TXT.Text, 1)
    End Sub

    'Public Sub AG_MV_print_Reset()
    '    Dim c As New C
    '    c.Com = New SqlCommand(" DELETE FROM [dbo].[AG_Bills_RPT] ", c.Con)
    '        SQL_SP_EXEC(c.Com)
    'End Sub

    'Private Sub AG_MV_print()
    '    Dim c As New C
    '    For i = 0 To AGMetroGrid.Rows.Count - 1

    '        c.Com = New SqlCommand("INSERT INTO [dbo].[AG_Bills_RPT] ([DATE],[BILL_NO],[PROJ_NAME],[PURE],[BILL_CASE],[IS_VOID],[PIED_NAME]) VALUES " &
    '         " ('" & AGMetroGrid.Rows(i).Cells(2).Value & "' " &
    '         " ,'" & AGMetroGrid.Rows(i).Cells(3).Value & "' " &
    '         " ,'" & AGMetroGrid.Rows(i).Cells(4).Value & "' " &
    '         " ,'" & AGMetroGrid.Rows(i).Cells(6).Value & "' " &
    '         " ,'" & AGMetroGrid.Rows(i).Cells(7).Value & "' " &
    '         " ,'" & AGMetroGrid.Rows(i).Cells("isDeleted_CL").Value & "' " &
    '         " ,'" & AGMetroGrid.Rows(i).Cells(9).Value & "') ", c.Con)
    '        SQL_SP_EXEC(c.Com)

    '    Next

    '    Try
    '        Dim p As New print
    '        Dim pp As New ReportConnection

    '        pp.rp.Load(Application.StartupPath & "\reports\AG_Bills.rpt")


    '        pp.CrTables = pp.rp.Database.Tables
    '        For Each CrTable In pp.CrTables
    '            pp.crtableLogoninfo = CrTable.LogOnInfo
    '            pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
    '            CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
    '        Next


    '        With pp
    '            .rp.SetParameterValue(0, USER_NAME)
    '            .rp.SetParameterValue(1, MY_Settings.Server_Desc)
    '            .rp.SetParameterValue(2, AG_Cm.Textt)
    '            '.rp.SetParameterValue(3, Bill_cmb.SelectedIndex)
    '            '.rp.SetParameterValue(4, isDeletedCheckBox.Checked)
    '            '.rp.SetParameterValue(5, AG_ID)
    '            If Markter_Cm.TXT_ID.Text > 0 Then
    '                .rp.SetParameterValue(3, "النوع: " & Bill_cmb.Text & "  / المسوق: " & Markter_Cm.Textt)
    '            Else
    '                .rp.SetParameterValue(3, "النوع: " & Bill_cmb.Text)
    '            End If


    '            '.rp.PrintOptions.PrinterName = Default_Printer_A4
    '            '.rp.PrintToPrinter(1, False, 0, 0)
    '            '.rp.Dispose()


    '            p.CrystalReportViewer1.ReportSource = pp.rp
    '            p.ShowDialog()


    '        End With

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub

    'Private Sub Print()
    '    Dim pp As New ReportConnection
    '    pp.rp.Load(Application.StartupPath & "\reports\AG_Bills.rpt")
    '    pp.LoadTables()
    '    With pp
    '        .rp.SetParameterValue(0, USER_NAME)
    '        .rp.SetParameterValue(1, My_Settings.Server_Desc)
    '        .rp.SetParameterValue(2, IM_SH_txt.Text)
    '        .rp.SetParameterValue(3, Bill_cmb.SelectedIndex)
    '        .rp.SetParameterValue(4, isDeletedCheckBox.Checked)
    '        .rp.SetParameterValue(5, AG_ID)
    '        .rp.SetParameterValue(6, Bill_cmb.Text)
    '    End With

    '    Dim p As New print
    '    p.CrystalReportViewer1.ReportSource = pp.rp
    '    p.Show()

    '    'pp.rp.PrintOptions.PrinterName = Default_Printer_80
    '    'pp.rp.PrintToPrinter(1, False, 0, 0)
    '    'pp.rp.Dispose()

    'End Sub

    Private Sub IM_Serach_btn_Click(sender As Object, e As EventArgs) Handles IM_Serach_btn.Click
        Me.Cursor = Cursors.WaitCursor
        If RPT_CM.SelectedIndex = 0 Then
            Load_Data()
        Else
            Load_Data_2()
        End If



        Me.Cursor = Cursors.Default
    End Sub

    Private Sub gridv_FilterStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.FilterEventArgs) Handles advancedDataGridView_main.FilterStringChanged
        bindingSource_main.Filter = advancedDataGridView_main.FilterString
        Index_GV()
    End Sub

    Private Sub gridv_SortStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.SortEventArgs) Handles advancedDataGridView_main.SortStringChanged
        bindingSource_main.Sort = advancedDataGridView_main.SortString
        Index_GV()
    End Sub

    Private Sub Index_GV()
        For i = 0 To advancedDataGridView_main.Rows.Count - 1
            advancedDataGridView_main.Rows(i).Cells(0).Value = i + 1
        Next

    End Sub


    Private Sub bindingSource_main_ListChanged(sender As Object, e As System.ComponentModel.ListChangedEventArgs) Handles bindingSource_main.ListChanged
        textBox_total.Text = bindingSource_main.List.Count.ToString()
    End Sub

    Private Sub advancedDataGridViewSearchToolBar_main_Search(sender As Object, e As Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventArgs) Handles advancedDataGridViewSearchToolBar_main.Search
        Dim restartsearch = True
        Dim startColumn = 0
        Dim startRow = 0
        If Not e.FromBegin Then
            Dim endcol As Boolean = advancedDataGridView_main.CurrentCell.ColumnIndex + 1 >= advancedDataGridView_main.ColumnCount
            Dim endrow As Boolean = advancedDataGridView_main.CurrentCell.RowIndex + 1 >= advancedDataGridView_main.RowCount

            If endcol AndAlso endrow Then
                startColumn = advancedDataGridView_main.CurrentCell.ColumnIndex
                startRow = advancedDataGridView_main.CurrentCell.RowIndex
            Else
                startColumn = If(endcol, 0, advancedDataGridView_main.CurrentCell.ColumnIndex + 1)
                startRow = advancedDataGridView_main.CurrentCell.RowIndex + If(endcol, 1, 0)
            End If
        End If
        Dim c As DataGridViewCell = advancedDataGridView_main.FindCell(e.ValueToSearch, If(e.ColumnToSearch IsNot Nothing, e.ColumnToSearch.Name, Nothing), startRow, startColumn, e.WholeWord, e.CaseSensitive)
        If c Is Nothing AndAlso restartsearch Then c = advancedDataGridView_main.FindCell(e.ValueToSearch, If(e.ColumnToSearch IsNot Nothing, e.ColumnToSearch.Name, Nothing), 0, 0, e.WholeWord, e.CaseSensitive)
        If c IsNot Nothing Then advancedDataGridView_main.CurrentCell = c
    End Sub


End Class