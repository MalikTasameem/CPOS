Imports System.Data.SqlClient

Imports System.Drawing.Printing

Public Class SearchAgent_Pch_Bill : Inherits System.Windows.Forms.Form
    Private Const ReportTitle As String = "كشف فواتير مشتريات"
    Dim Bills_DT As New DataTable
    Dim Dv As New DataView
    Dim BalanceType As String = ""
    Private drag As Boolean
    Private mouseX As Integer
    Private mouseY As Integer
    Private _printRowIndex As Integer = 0
    Private _printDateTime As DateTime
    Private _printPageNumber As Integer = 1
    Private _printTotalsPrinted As Boolean = False
    'Dim AG_ID As Integer

    Private Sub ExpSearch_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or &H20000
            Return cp
        End Get
    End Property

    ' 2. برمجة زر الإغلاق العلوي
    Private Sub TopCloseButton_Click(sender As Object, e As EventArgs) Handles TopCloseButton.Click
        Me.Close()
    End Sub

    ' 3. برمجة سحب الفورم من شريط العنوان
    Private Sub TitleBar_Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseDown
        drag = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub TitleBar_Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub

    Private Sub TitleBar_Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseUp
        drag = False
    End Sub

    Private Sub SearchAgent_Pch_Bill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' ==========================================
            ' 1. إسناد التاقات (Tags) المعتمدة للثيم
            ' ==========================================
            '   If TitleBar_Panel IsNot Nothing Then TitleBar_Panel.Tag = "HEADER"
            '   If Title_Label IsNot Nothing Then Title_Label.Tag = "TITLE_TRANSPARENT"
            If TopCloseButton IsNot Nothing Then TopCloseButton.Tag = "DELETE"

            ' الأزرار اللي ذكرتها
            If Print_btn IsNot Nothing Then Print_btn.Tag = "PRINT"
            If IM_Serach_btn IsNot Nothing Then IM_Serach_btn.Tag = "GENERAL"

            ' تطبيق الثيم الإجباري الخاص بالمنظومة
            ThemeManager.ApplyThemeToForm(Me)

            ' ==========================================
            ' 2. إعدادات الواجهة (Frameless)
            ' ==========================================
            ' التأكد من بقاء شريط العنوان في المقدمة باش ما يتغطاش بالفلاتر
            If TitleBar_Panel IsNot Nothing Then TitleBar_Panel.BringToFront()

            SetupModernLayout()
            ConfigureMainGrid()
            Make_Hints()

            ' ==========================================
            ' 5. تطبيق الصلاحيات والإعدادات المحفوظة
            ' ==========================================
            'Markter_Cm.Visible = S_Marketers
            'Marketer_Lb.Visible = S_Marketers

            Bill_cmb.SelectedIndex = MY_Settings.AG_SH_Bill_Type
            is_Auto_Select_CB.Checked = MY_Settings.SB_Search_Bill_Autot_Select

            ' ==========================================
            ' 6. تجهيز الفلاتر والتركيز
            ' ==========================================
            If RPT_CM.Items.Count > 0 Then
                RPT_CM.SelectedIndex = 0
            End If

            AG_Cm.Focus()

        Catch ex As Exception
            MsgBox("حدث خطأ أثناء تحميل شاشة البحث: " & vbCrLf & ex.Message, MsgBoxStyle.Critical, "خطأ التحميل")
        End Try
    End Sub

    Private Sub SetupModernLayout()

        Me.AutoScaleMode = AutoScaleMode.None
        Me.DoubleBuffered = True

        If Panel4 IsNot Nothing Then Panel4.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        If Panel1 IsNot Nothing Then Panel1.Dock = DockStyle.Bottom
        If TitleBar_Panel IsNot Nothing Then TitleBar_Panel.BringToFront()

        ArrangeModernLayout()

    End Sub

    Private Sub ArrangeModernLayout()

        If Me.ClientSize.Width <= 0 Then Return

        Dim paddingLeft As Integer = 12
        Dim rightEdge As Integer = Me.ClientSize.Width - 24
        Dim topY As Integer = If(TitleBar_Panel IsNot Nothing, TitleBar_Panel.Bottom + 10, 45)

        If AG_Cm IsNot Nothing Then AG_Cm.SetBounds(Math.Max(300, rightEdge - 455), topY, Math.Min(410, rightEdge - 320), 31)
        If Label4 IsNot Nothing Then Label4.SetBounds(rightEdge - 40, topY + 7, 55, 22)
        'If Markter_Cm IsNot Nothing Then Markter_Cm.SetBounds(Math.Max(300, rightEdge - 365), topY + 39, 315, 31)
        'If Marketer_Lb IsNot Nothing Then Marketer_Lb.SetBounds(rightEdge - 53, topY + 45, 70, 22)

        If IM_Serach_btn IsNot Nothing Then IM_Serach_btn.SetBounds(paddingLeft, topY, 105, 36)
        If Print_btn IsNot Nothing Then Print_btn.SetBounds(paddingLeft + 113, topY, 105, 36)
        If PdfButton IsNot Nothing Then PdfButton.SetBounds(paddingLeft + 226, topY, 96, 36)
        If isDeletedCheckBox IsNot Nothing Then isDeletedCheckBox.SetBounds(paddingLeft + 335, topY + 7, 95, 24)
        If Label32 IsNot Nothing Then Label32.SetBounds(paddingLeft + 555, topY + 8, 50, 22)
        If Bill_cmb IsNot Nothing Then Bill_cmb.SetBounds(paddingLeft + 425, topY + 4, 125, 29)

        If Panel2 IsNot Nothing Then Panel2.SetBounds(paddingLeft, topY + 48, Math.Min(640, Math.Max(320, Me.ClientSize.Width - 410)), 46)
        If is_Auto_Select_CB IsNot Nothing Then is_Auto_Select_CB.SetBounds(Math.Max(paddingLeft + 660, rightEdge - 160), topY + 88, 150, 24)

        If Panel3 IsNot Nothing Then Panel3.SetBounds(paddingLeft, topY + 108, Math.Min(680, Math.Max(320, Me.ClientSize.Width - 365)), 34)
        If RPT_CM IsNot Nothing Then RPT_CM.SetBounds(Math.Max(Panel3.Right + 25, rightEdge - 285), topY + 108, 270, 31)

        If Panel4 IsNot Nothing AndAlso Panel1 IsNot Nothing Then
            Dim gridTop As Integer = topY + 155
            Panel4.SetBounds(4, gridTop, Math.Max(100, Me.ClientSize.Width - 8), Math.Max(100, Panel1.Top - gridTop - 5))
        End If

        If UcGridColumnsSelector1 IsNot Nothing Then UcGridColumnsSelector1.SetBounds(paddingLeft, topY + 108, 115, 34)

    End Sub

    Private Sub ConfigureMainGrid()

        With advancedDataGridView_main
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AutoGenerateColumns = True
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .BackgroundColor = Color.White
            .BorderStyle = BorderStyle.None
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .ColumnHeadersHeight = 34
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .EnableHeadersVisualStyles = False
            .MultiSelect = False
            .ReadOnly = True
            .RightToLeft = RightToLeft.Yes
            .RowHeadersVisible = False
            .RowTemplate.Height = 31
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect

            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 62, 80)
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.25!, FontStyle.Bold)
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.BackColor = Color.White
            .DefaultCellStyle.ForeColor = Color.FromArgb(32, 39, 48)
            .DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 250)
            .DefaultCellStyle.SelectionForeColor = Color.Black
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252)
        End With

    End Sub

    Private Sub Make_Hints()

        If SearchFilterTextBox IsNot Nothing Then SendMessage(SearchFilterTextBox.Handle, &H1501, 0, "بحث برقم الفاتورة أو اسم المورد أو المورد 2")

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
            Main_Query = "SELECT Ag_name AS ' الحساب ',SUM(TOTAL) AS ' الإجمالي ',SUM(Discount) AS ' التخفيض ',SUM(Cost) AS ' الصافي ', SUM(Total_Pied) AS ' المدفوع ',SUM(Rest) AS ' الباقي '" &
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

            MarkFinancialColumns()



            If Bills_DT.Rows.Count = 0 Then
                MsgBox("لا توجد عناصر للعرض", MsgBoxStyle.Exclamation, "")
            Else
                For i As Integer = 0 To advancedDataGridView_main.ColumnCount - 1
                    advancedDataGridView_main.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Next
            End If

            AfterGridDataBound()


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

            If AG_Cm.TXT_ID.Text > 0 Then middle &= " AND AG_ID = '" & AG_Cm.TXT_ID.Text & "' "

            Select Case Bill_cmb.SelectedIndex
                Case 0
                    middle &= " and isVoid = '" & Convert.ToInt16(isDeletedCheckBox.Checked) & "' "
                Case 1
                    middle &= " and isVoid = '" & Convert.ToInt16(isDeletedCheckBox.Checked) & "' AND isPied = 1 "
                Case 2
                    middle &= " and isVoid = '" & Convert.ToInt16(isDeletedCheckBox.Checked) & "' AND isPied = 0 "
            End Select

            If ALL_time_CheckBox.Checked = False Then middle &= " AND CONVERT(DATE,DATE) BETWEEN   CONVERT(DATE,'" & DateRange_Flate.D_From.Text & "') AND CONVERT(DATE,'" & DateRange_Flate.D_To.Text & "')   "

            Main_Query &= middle & last
            c.Da = New SqlClient.SqlDataAdapter(Main_Query, c.Con)


            c.Da.SelectCommand.CommandTimeout = 120

            c.Da.Fill(Bills_DT)
            bindingSource_main.DataSource = Bills_DT
            advancedDataGridView_main.DataSource = bindingSource_main

            MarkFinancialColumns()


            If Bills_DT.Rows.Count = 0 Then
                MsgBox("لا توجد عناصر للعرض", MsgBoxStyle.Exclamation, "")
            Else
                advancedDataGridView_main.Columns(1).Visible = False
                advancedDataGridView_main.Columns(14).Visible = False
                advancedDataGridView_main.Columns(15).Visible = False

                'advancedDataGridView_main.Columns(16).Visible = False

                For i As Integer = 0 To advancedDataGridView_main.ColumnCount - 1
                    advancedDataGridView_main.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Next
            End If

            AfterGridDataBound()


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
        ArrangeModernLayout()
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
            F_Pch.BillNumPanel.Enabled = False
            F_Pch.ShowDialog()
            isShowing_Trans = False

        End If

    End Sub


    Private Sub isDeletedCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles isDeletedCheckBox.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub Bill_cmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Bill_cmb.SelectedIndexChanged
        MY_Settings.AG_SH_Bill_Type = Bill_cmb.SelectedIndex
        Save_AppSetting()
    End Sub

    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        If advancedDataGridView_main.Rows.Count = 0 Then
            MsgBox("لا توجد بيانات للطباعة.", MsgBoxStyle.Exclamation, "")
            Return
        End If

        PreviewSearchAgentPchBillPrint()
    End Sub

    Private Sub PdfButton_Click(sender As Object, e As EventArgs) Handles PdfButton.Click

        If advancedDataGridView_main.Rows.Count = 0 Then
            MsgBox("لا توجد بيانات للتصدير.", MsgBoxStyle.Exclamation, "")
            Return
        End If

        ExportSearchAgentPchBillPdf()

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
        Load_bills()
    End Sub

    Private Sub Load_bills()
        Try
            Me.Cursor = Cursors.WaitCursor
            If IM_Serach_btn IsNot Nothing Then
                IM_Serach_btn.Enabled = False
                IM_Serach_btn.Text = "جاري البحث..."
            End If

            If RPT_CM.SelectedIndex = 0 Then
                Load_Data()
            Else
                Load_Data_2()
            End If

        Finally
            If IM_Serach_btn IsNot Nothing Then
                IM_Serach_btn.Enabled = True
                IM_Serach_btn.Text = "⌕ بحث"
            End If
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub advancedDataGridView_main_Sorted(sender As Object, e As EventArgs) Handles advancedDataGridView_main.Sorted
        Index_GV()
    End Sub

    Private Sub AfterGridDataBound()

        ConfigureMainGrid()
        BindColumnsSelector()
        ApplySearchFilter()
        Index_GV()
        UpdateFinancialTotals()

    End Sub

    Private Sub BindColumnsSelector()

        If UcGridColumnsSelector1 Is Nothing OrElse advancedDataGridView_main.Columns.Count = 0 Then Return

        UcGridColumnsSelector1.BindGrid(
            advancedDataGridView_main,
            New List(Of String) From {""},
            Me.Name.ToString()
        )

    End Sub

    Private Sub SearchFilterTextBox_TextChanged(sender As Object, e As EventArgs) Handles SearchFilterTextBox.TextChanged
        ApplySearchFilter()
    End Sub

    Private Sub SearchFilterTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles SearchFilterTextBox.KeyDown
        If e.KeyCode = Keys.Delete Then SearchFilterTextBox.Clear()
    End Sub

    Private Sub ApplySearchFilter()

        If bindingSource_main Is Nothing OrElse bindingSource_main.DataSource Is Nothing OrElse Bills_DT Is Nothing Then Return

        Dim searchText As String = If(SearchFilterTextBox Is Nothing, "", SearchFilterTextBox.Text.Trim())

        If searchText = "" Then
            bindingSource_main.RemoveFilter()
            Index_GV()
            UpdateFinancialTotals()
            Return
        End If

        Dim escapedText As String = EscapeRowFilterValue(searchText)
        Dim filters As New List(Of String)()

        AddLikeFilter(filters, "رقم الفاتورة", escapedText)
        AddLikeFilter(filters, "الحساب", escapedText)
        AddLikeFilter(filters, "الزبون 2", escapedText)
        AddLikeFilter(filters, "Bill_ID", escapedText)
        AddLikeFilter(filters, "Ag_name", escapedText)
        AddLikeFilter(filters, "Proj_NAME", escapedText)

        If filters.Count = 0 Then
            bindingSource_main.RemoveFilter()
        Else
            bindingSource_main.Filter = String.Join(" OR ", filters)
        End If

        Index_GV()
        UpdateFinancialTotals()

    End Sub

    Private Sub AddLikeFilter(filters As List(Of String), caption As String, escapedText As String)

        Dim columnName As String = FindColumnName(caption)
        If columnName = "" Then Return

        filters.Add("Convert([" & columnName.Replace("]", "\]") & "], 'System.String') LIKE '%" & escapedText & "%'")

    End Sub

    Private Function FindColumnName(caption As String) As String

        If Bills_DT Is Nothing Then Return ""

        Dim normalizedCaption As String = NormalizeColumnCaption(caption)

        For Each column As DataColumn In Bills_DT.Columns
            If NormalizeColumnCaption(column.ColumnName) = normalizedCaption Then Return column.ColumnName
        Next

        Return ""

    End Function

    Private Function NormalizeColumnCaption(value As String) As String

        If value Is Nothing Then Return ""
        Return value.Replace(" ", "").Replace(vbTab, "").Trim()

    End Function

    Private Sub MarkFinancialColumns()

        If advancedDataGridView_main Is Nothing OrElse advancedDataGridView_main.Columns.Count = 0 Then Return

        Dim financialCaptions As New HashSet(Of String) From {
            NormalizeColumnCaption("الإجمالي"),
            NormalizeColumnCaption("التخفيض"),
            NormalizeColumnCaption("الصافي"),
            NormalizeColumnCaption("المدفوع"),
            NormalizeColumnCaption("الباقي")
        }

        For Each column As DataGridViewColumn In advancedDataGridView_main.Columns
            column.Tag = Nothing

            If financialCaptions.Contains(NormalizeColumnCaption(column.HeaderText)) Then
                column.Tag = 1
            End If
        Next

    End Sub

    Private Function EscapeRowFilterValue(value As String) As String

        If value Is Nothing Then Return ""
        Return value.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]").Replace("*", "[*]")

    End Function

    Private Sub Index_GV()
        If advancedDataGridView_main.Columns.Count = 0 Then Return

        For i = 0 To advancedDataGridView_main.Rows.Count - 1
            advancedDataGridView_main.Rows(i).Cells(0).Value = i + 1
        Next

    End Sub


    Private Sub bindingSource_main_ListChanged(sender As Object, e As System.ComponentModel.ListChangedEventArgs) Handles bindingSource_main.ListChanged
        UpdateFinancialTotals()
    End Sub

    Private Sub UpdateFinancialTotals()

        If TotalsGrid Is Nothing Then Return

        Dim totalsTable As New DataTable()
        totalsTable.Columns.Add("عدد الصفوف")

        Dim financialColumns As New List(Of DataGridViewColumn)()

        If advancedDataGridView_main IsNot Nothing Then
            For Each column As DataGridViewColumn In advancedDataGridView_main.Columns
                If column.Tag IsNot Nothing AndAlso column.Tag.ToString() = "1" Then
                    financialColumns.Add(column)
                    totalsTable.Columns.Add(column.HeaderText.Trim())
                End If
            Next
        End If

        Dim row As DataRow = totalsTable.NewRow()
        row("عدد الصفوف") = If(advancedDataGridView_main Is Nothing, 0, advancedDataGridView_main.Rows.Count)

        For Each column As DataGridViewColumn In financialColumns
            Dim totalValue As Decimal = 0D

            For Each gridRow As DataGridViewRow In advancedDataGridView_main.Rows
                If gridRow.IsNewRow Then Continue For

                Dim value As Object = gridRow.Cells(column.Name).Value
                If value Is Nothing OrElse value Is DBNull.Value Then Continue For

                Dim numberValue As Decimal
                If Decimal.TryParse(value.ToString(), Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, numberValue) OrElse
                    Decimal.TryParse(value.ToString(), Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, numberValue) Then
                    totalValue += numberValue
                End If
            Next

            row(column.HeaderText.Trim()) = totalValue.ToString(N_Point_Fter)
        Next

        totalsTable.Rows.Add(row)
        TotalsGrid.DataSource = totalsTable

        For Each column As DataGridViewColumn In TotalsGrid.Columns
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Next

    End Sub

    Private Sub PreviewSearchAgentPchBillPrint()

        Using printDocument As PrintDocument = CreateSearchAgentPchBillPrintDocument()
            Using previewDialog As New PrintPreviewDialog()
                previewDialog.Document = printDocument
                previewDialog.WindowState = FormWindowState.Maximized
                previewDialog.Text = "معاينة " & ReportTitle
                previewDialog.ShowDialog(Me)
            End Using
        End Using

    End Sub

    Private Sub ExportSearchAgentPchBillPdf()

        Dim pdfPrinterName As String = GetPdfPrinterName()

        If pdfPrinterName = "" Then
            MsgBox("طابعة Microsoft Print to PDF غير متوفرة على هذا الجهاز.", MsgBoxStyle.Exclamation, "")
            Return
        End If

        Using saveDialog As New SaveFileDialog()
            saveDialog.Filter = "PDF Files (*.pdf)|*.pdf"
            saveDialog.FileName = ReportTitle & " " & Date.Now.ToString("yyyyMMdd_HHmm") & ".pdf"
            saveDialog.Title = "حفظ " & ReportTitle & " PDF"

            If saveDialog.ShowDialog(Me) <> DialogResult.OK Then Return

            Using printDocument As PrintDocument = CreateSearchAgentPchBillPrintDocument()
                printDocument.PrinterSettings.PrinterName = pdfPrinterName
                printDocument.PrinterSettings.PrintToFile = True
                printDocument.PrinterSettings.PrintFileName = saveDialog.FileName
                printDocument.PrintController = New StandardPrintController()
                printDocument.Print()
            End Using
        End Using

    End Sub

    Private Function CreateSearchAgentPchBillPrintDocument() As PrintDocument

        Dim printDocument As New PrintDocument()
        printDocument.DocumentName = ReportTitle
        printDocument.DefaultPageSettings.Landscape = True
        printDocument.DefaultPageSettings.Margins = New Margins(30, 30, 40, 45)

        AddHandler printDocument.BeginPrint, AddressOf SearchAgentPchBillPrintDocument_BeginPrint
        AddHandler printDocument.PrintPage, AddressOf SearchAgentPchBillPrintDocument_PrintPage

        Return printDocument

    End Function

    Private Sub SearchAgentPchBillPrintDocument_BeginPrint(sender As Object, e As PrintEventArgs)

        _printRowIndex = 0
        _printDateTime = Date.Now
        _printPageNumber = 1
        _printTotalsPrinted = False

    End Sub

    Private Sub SearchAgentPchBillPrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)

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
              rowFont As New Font("Segoe UI", 7.0!, FontStyle.Regular)

            Dim centerFormat As New StringFormat()
            centerFormat.Alignment = StringAlignment.Center
            centerFormat.LineAlignment = StringAlignment.Center
            centerFormat.Trimming = StringTrimming.EllipsisCharacter

            Dim rtlFormat As New StringFormat()
            rtlFormat.Alignment = StringAlignment.Far
            rtlFormat.LineAlignment = StringAlignment.Center
            rtlFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft
            rtlFormat.Trimming = StringTrimming.EllipsisCharacter

            DrawReportStoreHeader(e.Graphics, bounds, y, storeTitleFont, storeSubTitleFont, centerFormat)

            e.Graphics.DrawString(ReportTitle, titleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 28), centerFormat)
            y += 30

            e.Graphics.DrawString(GetSearchAgentPchBillFilterText(), infoFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 24), rtlFormat)
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

            Dim firstDataRowY As Integer = y
            Dim rowsPerPage As Integer = CalculatePrintableRowsPerPage(firstDataRowY, bounds.Bottom, rowHeight)
            Dim totalsHeight As Integer = CalculateTotalsPrintHeight(rowHeight)
            Dim totalPages As Integer = CalculateTotalPrintPagesWithTotals(advancedDataGridView_main.Rows.Count, rowsPerPage, rowHeight, totalsHeight)
            Dim currentPage As Integer = _printPageNumber

            While _printRowIndex < advancedDataGridView_main.Rows.Count

                If y + rowHeight > bounds.Bottom - 58 Then
                    DrawReportFooter(e.Graphics, bounds, currentPage, totalPages, infoFont, centerFormat)
                    _printPageNumber += 1
                    e.HasMorePages = True
                    Return
                End If

                Dim row As DataGridViewRow = advancedDataGridView_main.Rows(_printRowIndex)
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

            If Not _printTotalsPrinted Then
                If y + totalsHeight > bounds.Bottom - 58 Then
                    DrawReportFooter(e.Graphics, bounds, currentPage, totalPages, infoFont, centerFormat)
                    _printPageNumber += 1
                    e.HasMorePages = True
                    Return
                End If

                y += 8
                DrawFinancialTotals(e.Graphics, bounds, y, headerFont, rowFont, centerFormat)
                _printTotalsPrinted = True
            End If

            DrawReportFooter(e.Graphics, bounds, currentPage, totalPages, infoFont, centerFormat)

        End Using

        e.HasMorePages = False

    End Sub

    Private Function GetPrintableColumns() As List(Of DataGridViewColumn)

        Dim columns As New List(Of DataGridViewColumn)()

        For Each col As DataGridViewColumn In advancedDataGridView_main.Columns
            If col.Visible Then columns.Add(col)
        Next

        Return columns

    End Function

    Private Function CalculatePrintColumnWidths(columns As List(Of DataGridViewColumn), availableWidth As Integer) As List(Of Integer)

        Dim widths As New List(Of Integer)()
        Dim totalGridWidth As Integer = 0

        For Each col As DataGridViewColumn In columns
            totalGridWidth += Math.Max(35, col.Width)
        Next

        If totalGridWidth <= 0 Then totalGridWidth = columns.Count * 80

        Dim usedWidth As Integer = 0

        For i As Integer = 0 To columns.Count - 1
            Dim width As Integer

            If i = columns.Count - 1 Then
                width = Math.Max(35, availableWidth - usedWidth)
            Else
                width = Math.Max(35, CInt(availableWidth * (Math.Max(35, columns(i).Width) / CDbl(totalGridWidth))))
            End If

            widths.Add(width)
            usedWidth += width
        Next

        Return widths

    End Function

    Private Function CalculateTotalsPrintHeight(rowHeight As Integer) As Integer

        If TotalsGrid Is Nothing OrElse TotalsGrid.Columns.Count = 0 OrElse TotalsGrid.Rows.Count = 0 Then Return 0

        Return 8 + (rowHeight * 2)

    End Function

    Private Sub DrawFinancialTotals(graphics As Graphics, bounds As Rectangle, ByRef y As Integer, headerFont As Font, rowFont As Font, centerFormat As StringFormat)

        If TotalsGrid Is Nothing OrElse TotalsGrid.Columns.Count = 0 OrElse TotalsGrid.Rows.Count = 0 Then Return

        Dim columns As New List(Of DataGridViewColumn)()

        For Each column As DataGridViewColumn In TotalsGrid.Columns
            If column.Visible Then columns.Add(column)
        Next

        If columns.Count = 0 Then Return

        Dim rowHeight As Integer = 24
        Dim widths As List(Of Integer) = CalculatePrintColumnWidths(columns, bounds.Width)
        Dim x As Integer = bounds.Right

        For i As Integer = 0 To columns.Count - 1
            x -= widths(i)
            Dim rect As New Rectangle(x, y, widths(i), rowHeight)

            Using backBrush As New SolidBrush(Color.FromArgb(45, 62, 80))
                graphics.FillRectangle(backBrush, rect)
            End Using

            graphics.DrawRectangle(Pens.DarkGray, rect)
            graphics.DrawString(columns(i).HeaderText.Trim(), headerFont, Brushes.White, rect, centerFormat)
        Next

        y += rowHeight
        x = bounds.Right

        Dim totalRow As DataGridViewRow = TotalsGrid.Rows(0)

        For i As Integer = 0 To columns.Count - 1
            x -= widths(i)
            Dim rect As New Rectangle(x, y, widths(i), rowHeight)

            Using totalBrush As New SolidBrush(Color.FromArgb(235, 240, 245))
                graphics.FillRectangle(totalBrush, rect)
            End Using

            graphics.DrawRectangle(Pens.LightGray, rect)
            graphics.DrawString(GetPrintableCellText(totalRow, columns(i)), rowFont, Brushes.Black, rect, centerFormat)
        Next

        y += rowHeight

    End Sub

    Private Function GetPrintableCellText(row As DataGridViewRow, column As DataGridViewColumn) As String

        If row.Cells(column.Name).Value Is Nothing OrElse row.Cells(column.Name).Value Is DBNull.Value Then Return ""

        If column.Tag IsNot Nothing AndAlso column.Tag.ToString() = "1" Then
            Dim numberValue As Decimal
            If Decimal.TryParse(row.Cells(column.Name).Value.ToString(), numberValue) Then Return numberValue.ToString(N_Point_Fter)
        End If

        Return row.Cells(column.Name).FormattedValue.ToString()

    End Function

    Private Function GetSearchAgentPchBillFilterText() As String

        Dim searchText As String = If(SearchFilterTextBox Is Nothing OrElse SearchFilterTextBox.Text.Trim() = "", "الكل", SearchFilterTextBox.Text.Trim())

        Return "المورد: " & AG_Cm.Textt &
            "    النوع: " & Bill_cmb.Text &
            "    الفترة: " & GetDateRangeText() &
            "    التقرير: " & RPT_CM.Text &
            "    البحث: " & searchText &
            "    عدد الصفوف: " & advancedDataGridView_main.Rows.Count.ToString()

    End Function

    Private Function GetDateRangeText() As String

        If ALL_time_CheckBox IsNot Nothing AndAlso ALL_time_CheckBox.Checked Then Return "كل الفترات"

        Return DateRange_Flate.D_From.Text & " - " & DateRange_Flate.D_To.Text

    End Function

    Private Sub DrawReportStoreHeader(graphics As Graphics, bounds As Rectangle, ByRef y As Integer, storeTitleFont As Font, storeSubTitleFont As Font, centerFormat As StringFormat)

        If Not String.IsNullOrWhiteSpace(SBill_Title_1) Then
            graphics.DrawString(SBill_Title_1, storeTitleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 30), centerFormat)
            y += 30
        End If

        If Not String.IsNullOrWhiteSpace(SBill_Title_2) Then
            graphics.DrawString(SBill_Title_2, storeSubTitleFont, Brushes.Black, New Rectangle(bounds.Left, y, bounds.Width, 24), centerFormat)
            y += 24
        End If

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

    Private Function CalculateTotalPrintPagesWithTotals(totalRows As Integer, rowsPerPage As Integer, rowHeight As Integer, totalsHeight As Integer) As Integer

        Dim rowPages As Integer = CalculateTotalPrintPages(totalRows, rowsPerPage)

        If totalsHeight <= 0 OrElse rowsPerPage <= 0 OrElse rowHeight <= 0 Then Return rowPages

        Dim rowsOnLastPage As Integer = totalRows Mod rowsPerPage
        If rowsOnLastPage = 0 AndAlso totalRows > 0 Then rowsOnLastPage = rowsPerPage

        Dim availableHeightAfterRows As Integer = (rowsPerPage - rowsOnLastPage) * rowHeight
        If totalsHeight > availableHeightAfterRows Then rowPages += 1

        Return Math.Max(1, rowPages)

    End Function

    Private Function CalculateCurrentPrintPage(rowIndex As Integer, rowsPerPage As Integer) As Integer

        If rowsPerPage <= 0 Then Return 1

        Return Math.Max(1, (rowIndex \ rowsPerPage) + 1)

    End Function

    Private Sub DrawReportFooter(graphics As Graphics, bounds As Rectangle, currentPage As Integer, totalPages As Integer, footerFont As Font, centerFormat As StringFormat)

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

    Private Sub is_Auto_Select_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_Auto_Select_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.SB_Search_Bill_Autot_Select = is_Auto_Select_CB.Checked
        MY_Settings.Save_AppSetting()
    End Sub

    Private Sub AG_Cm_ID_Changed(sender As Object, e As EventArgs) Handles AG_Cm.ID_Changed
        ' If AG_Cm.TXT_ID.Text > 0 Then If is_Auto_Select_CB.Checked = True Then Load_bills()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.WindowState = FormWindowState.Normal Then
            ' 1. تحديد أقصى حجم للفورم ليكون بحجم مساحة العمل فقط (لتفادي تغطية شريط المهام)
            Me.MaximumSize = Screen.FromHandle(Me.Handle).WorkingArea.Size

            ' 2. تكبير الفورم
            Me.WindowState = FormWindowState.Maximized

            ' 3. تغيير أيقونة الزر إلى (استعادة)
            Button1.Text = "❐"
        Else
            ' 1. إرجاع الفورم لحجمه الطبيعي
            Me.WindowState = FormWindowState.Normal

            ' 2. تغيير أيقونة الزر إلى (تكبير)
            Button1.Text = "⬜"
        End If
    End Sub
End Class

