Imports System.Data.SqlClient
Imports System.Globalization


Imports System.IO
Imports System.Drawing.Imaging
Imports System.Drawing.Printing

Public Class POS_Report

    Public Pr_Auto_Print As Boolean
    Public is_By_Pr As Boolean = False
    Dim rs As New Resizer
    Private Sub SalesPrintButton_Click(sender As Object, e As EventArgs) Handles SalesPrintButton.Click

        Me.Cursor = Cursors.AppStarting
        If Pr_PrinterPage_Type = 0 Then

            If SB_PrintTotal_CB.Checked = True Then PrintSalesAll()
            If SB_PrintIM_CB.Checked = True Then IM_Sales_Tmp_R_INSERT()

        ElseIf Pr_PrinterPage_Type = 1 Then

            If SB_PrintTotal_CB.Checked = True Then PrintSales_Small()
            If SB_PrintIM_CB.Checked = True Then IM_Sales_Tmp_R_INSERT()

        ElseIf Pr_PrinterPage_Type = 2 Then

            PrintReceipt()
            If SB_PrintIM_CB.Checked = True Then IM_Sales_Tmp_R_INSERT()
        End If
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub PrintReceipt()
        Dim printDoc As New PrintDocument()

        ' اضبط اسم الطابعة الحرارية
        printDoc.PrinterSettings.PrinterName = Default_Printer_80
        printDoc.DefaultPageSettings.PaperSize = New PaperSize(Default_Printer_80, 280, 600)


        AddHandler printDoc.PrintPage, AddressOf PrintPageHandler

        printDoc.Print()
    End Sub

    Private Sub PrintPageHandler(sender As Object, e As PrintPageEventArgs)
        Dim fmarket_Title As New Font("Arial", 14, FontStyle.Bold)
        Dim fTitle As New Font("Arial", 11, FontStyle.Bold)
        Dim fBody As New Font("Arial", 10)
        Dim y As Integer = 0


        ' إسم المحل
        e.Graphics.DrawString(SBill_Title_1, fmarket_Title, Brushes.Black, 10, y)
        y += 30

        ' إسم المحل
        e.Graphics.DrawString(SBill_Title_2, fmarket_Title, Brushes.Black, 10, y)
        y += 30

        ' عنوان التقرير
        e.Graphics.DrawString("تقرير المبيعات اليومي", fTitle, Brushes.Black, 10, y)
        y += 30


        e.Graphics.DrawString(Pr_Time_txt.Text, fTitle, Brushes.Black, 10, y)
        y += 20

        e.Graphics.DrawString(Pr_UserName_txt.Text, fTitle, Brushes.Black, 10, y)
        y += 20

        ' خط فاصل
        e.Graphics.DrawString(New String("-"c, 40), fBody, Brushes.Black, 0, y)
        y += 20

        ' طباعة محتوى DataGridView
        For Each row As DataGridViewRow In DataGridViewX.Rows
            If Not row.IsNewRow Then
                Dim itemName As String = row.Cells(1).Value.ToString()
                Dim itemValue As String = row.Cells(2).Value.ToString()
                e.Graphics.DrawString(itemName, fBody, Brushes.Black, 5, y)
                e.Graphics.DrawString(itemValue.PadLeft(15), fBody, Brushes.Black, 140, y)
                y += 20
            End If
        Next

        ' خط فاصل
        e.Graphics.DrawString(New String("-"c, 40), fBody, Brushes.Black, 0, y)
        y += 30

        ' الخلاصة صافي المقبوض من المصروف
        e.Graphics.DrawString("الصافي (المقبوض - المصروف)" & " : " & Finencial_T_txt.Text, fTitle, Brushes.Black, 10, y)
        y += 30
        ' تذييل بسيط
        e.Graphics.DrawString("تمت الطباعة في: " & DateTime.Now.ToString("yyyy/MM/dd HH:mm") & " - " & USER_NAME, fBody, Brushes.Black, 0, y)
    End Sub



    Public Sub fetch_GM()
        GM_cmb.DataSource = GetMailItems()
        GM_cmb.DisplayMember = "name"
        GM_cmb.ValueMember = "ID"
        GM_cmb.SelectedIndex = 0
    End Sub


    Public Sub IM_Sales_Tmp_R_INSERT()
        IM_Sales_Tmp_R_DELETE()
        Dim C As New C
        For i = 0 To IM_Details_GV.Rows.Count - 1
            C = New C
            With C.Com
                .Connection = C.Con
                .CommandText = "IM_Sales_Tmp_R_INSERT"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@U_Name", IM_Details_GV.Rows(i).Cells(0).Value)
                .Parameters.AddWithValue("@item_name", IM_Details_GV.Rows(i).Cells(1).Value)
                .Parameters.AddWithValue("@S_QTY", IM_Details_GV.Rows(i).Cells(2).Value)
                .Parameters.AddWithValue("@S_T_Price", IM_Details_GV.Rows(i).Cells(3).Value)
            End With
            SQL_SP_EXEC(C.Com)
        Next

        If Pr_PrinterPage_Type = 0 Then
            Pr_IM_Moves_A4()
        Else
            Pr_IM_Moves_Small()
        End If

    End Sub

    Public Sub IM_Sales_Tmp_R_DELETE()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Sales_Tmp_R_DELETE"
            .CommandType = CommandType.StoredProcedure
        End With
        SQL_SP_EXEC(C.Com)
    End Sub

    Private Sub PrintSalesAll()
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\Print_Sales_List.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, Pr_UserName_txt.Text)
            .rp.SetParameterValue(1, Pr_Time_txt.Text)
            .rp.SetParameterValue(2, USER_NAME)

            .rp.SetParameterValue(3, StartNotes_txt.Text)
            .rp.SetParameterValue(4, EndNotes_txt.Text)

            .rp.SetParameterValue(5, HOME.DateRange_Flate.D_F.Value)
            .rp.SetParameterValue(6, HOME.DateRange_Flate.D_T.Value)

            If TypeName(PeriodsCmb.SelectedValue) = "Integer" Then
                .rp.SetParameterValue(7, PeriodsCmb.SelectedValue)
            Else
                .rp.SetParameterValue(7, 0)
            End If


            '.rp.SetParameterValue(5, DataGridViewX.Rows(2).Cells(2).Value)
            '.rp.SetParameterValue(6, DataGridViewX.Rows(3).Cells(2).Value)

            '' .rp.SetParameterValue(7, SB_T_D_txt.Text)

            '.rp.SetParameterValue(8, DataGridViewX.Rows(6).Cells(2).Value)
            '.rp.SetParameterValue(9, DataGridViewX.Rows(7).Cells(2).Value)


            '.rp.SetParameterValue(10, DataGridViewX.Rows(15).Cells(2).Value)

            '.rp.SetParameterValue(11, DataGridViewX.Rows(0).Cells(2).Value)
            '.rp.SetParameterValue(12, HANY(DataGridViewX.Rows(0).Cells(2).Value, "EGYPT"))
            '.rp.SetParameterValue(13, DataGridViewX.Rows(8).Cells(2).Value)
            '.rp.SetParameterValue(14, DataGridViewX.Rows(9).Cells(2).Value)



            '.rp.SetParameterValue(18, DataGridViewX.Rows(10).Cells(2).Value)
            '.rp.SetParameterValue(19, DataGridViewX.Rows(11).Cells(2).Value)

            '.rp.SetParameterValue(20, DataGridViewX.Rows(14).Cells(2).Value)

            '.rp.SetParameterValue(21, MY_Settings.Server_Desc)

            '.rp.SetParameterValue(22, DataGridViewX.Rows(4).Cells(2).Value)
            '.rp.SetParameterValue(23, DataGridViewX.Rows(5).Cells(2).Value)

            '.rp.SetParameterValue(24, DataGridViewX.Rows(1).Cells(2).Value)

            '.rp.SetParameterValue(25, DataGridViewX.Rows(12).Cells(2).Value)

            '.rp.SetParameterValue(26, DataGridViewX.Rows(13).Cells(2).Value)

            '.rp.SetParameterValue(27, Finencial_T_txt.Text)
        End With

        If MY_Settings.Pr_Printer_isShow = True Then
            Dim p As New print
            p.CrystalReportViewer1.ReportSource = pp.rp
            p.Show()
        Else
            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_A4))
            pp.rp.PrintOptions.PrinterName = Default_Printer_A4
            pp.rp.PrintToPrinter(1, False, 0, 0)
            pp.rp.Dispose()
        End If

    End Sub

    Private Sub PrintSales_Small()
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\SB_Small.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue("usr", USER_NAME)
            .rp.SetParameterValue("date from", Pr_Time_txt.Text)
            .rp.SetParameterValue("nameU", Pr_UserName_txt.Text)
            .rp.SetParameterValue("txtCharFinalTotalSales", txtCharFinalTotalSales.Text)
            .rp.SetParameterValue("txtFinalTotalSales", Finencial_T_txt.Text)
            .rp.SetParameterValue("txtTotalSales", DataGridViewX.Rows(3).Cells(2).Value)
            .rp.SetParameterValue("txtMoneyTotalSales", DataGridViewX.Rows(2).Cells(2).Value)
            .rp.SetParameterValue("txtDiscountSales", DataGridViewX.Rows(11).Cells(2).Value)
            .rp.SetParameterValue("txtMonDiscountSales", DataGridViewX.Rows(10).Cells(2).Value)
            .rp.SetParameterValue("txtReturnSales", DataGridViewX.Rows(11).Cells(2).Value)
            .rp.SetParameterValue("txtMonReturnSales", DataGridViewX.Rows(12).Cells(2).Value)
            .rp.SetParameterValue("CompN", MY_Settings.Server_Desc)

            .rp.SetParameterValue("SB_Returns_M", DataGridViewX.Rows(15).Cells(2).Value)

        End With


        If MY_Settings.Pr_Printer_isShow = True Then
            Dim p As New print
            p.CrystalReportViewer1.ReportSource = pp.rp
            p.Show()
        Else
            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_80))
            pp.rp.PrintOptions.PrinterName = Default_Printer_80
            pp.rp.PrintToPrinter(1, False, 0, 0)
            pp.rp.Dispose()
        End If

    End Sub

    Private Sub Load_Date_Periods()
        Dim C As New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "Pr_SelectClosePr"
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
            .CommandType = CommandType.StoredProcedure
        End With

        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        PeriodsCmb.DataSource = C.Dt
        PeriodsCmb.DisplayMember = "Notes"
        PeriodsCmb.ValueMember = "Pr_ID"

    End Sub

    Dim Finc_DT As New DataTable
    'Private Sub Fetch_Pr_Details_()
    '    Dim C As New C
    '    Finc_DT = New DataTable
    '    With C.Com
    '        .Connection = C.Con
    '        If Pr_Auto_Print = True Then
    '            .CommandText = "Count_Total_Balance_By_Periods"
    '            .Parameters.AddWithValue("@Pr_ID", Pr_ID)
    '        Else

    '            If PeriodsCmb.SelectedIndex = -1 Then
    '                .CommandText = "Count_Total_Balance_By_Date"
    '                .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
    '                .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
    '            Else
    '                .CommandText = "Count_Total_Balance_By_Periods"
    '                .Parameters.AddWithValue("@Pr_ID", PeriodsCmb.SelectedValue)
    '            End If
    '        End If
    '        .CommandType = CommandType.StoredProcedure
    '    End With

    '    C.Da = New SqlClient.SqlDataAdapter(C.Com)
    '    C.Da.Fill(Finc_DT)
    '    DataGridViewX.DataSource = Finc_DT

    '    Finencial_T_txt.Text = DataGridViewX.Rows(0).Cells(2).Value - DataGridViewX.Rows(DataGridViewX.Rows.Count - 1).Cells(2).Value

    '    If Pr_Auto_Print = False Then
    '        Select_Other_Details()
    '    Else
    '        Select_Auto_Details()
    '        If Pr_PrinterPage_Type = 0 Then
    '            PrintSalesAll()
    '        Else
    '            PrintSales_Small()
    '        End If
    '    End If


    'End Sub

    Private Sub Fetch_Pr_Details_()

        Dim C As New C
        Dim DS As New DataSet()

        With C.Com
            .Connection = C.Con
            .Parameters.Clear()

            If Pr_Auto_Print = True Then
                .CommandText = "Count_Total_Balance_By_Periods"
                .Parameters.AddWithValue("@Pr_ID", Pr_ID)
            Else
                If PeriodsCmb.SelectedIndex = -1 Then
                    .CommandText = "Count_Total_Balance_By_Date"
                    .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
                    .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
                Else
                    .CommandText = "Count_Total_Balance_By_Periods"
                    .Parameters.AddWithValue("@Pr_ID", PeriodsCmb.SelectedValue)
                End If
            End If

            .CommandType = CommandType.StoredProcedure
        End With

        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(DS)

        ' ==========================
        ' Result Set الأول (القديم)
        ' ==========================
        DataGridViewX.DataSource = DS.Tables(0)

        Finencial_T_txt.Text =
        DataGridViewX.Rows(0).Cells(2).Value -
        DataGridViewX.Rows(DataGridViewX.Rows.Count - 1).Cells(2).Value

        ' ==========================
        ' Result Set الثاني (طرق الدفع)
        ' ==========================
        If DS.Tables.Count > 1 Then
            Pay_Grid.DataSource = DS.Tables(1)
        End If

        ' ==========================
        ' بقية المنطق كما هو
        ' ==========================
        If Pr_Auto_Print = False Then
            Select_Other_Details()
        Else
            Select_Auto_Details()
            If Pr_PrinterPage_Type = 0 Then
                PrintSalesAll()
            Else
                PrintSales_Small()
            End If
        End If

        TabControl1.SelectedTab = TabPage1

    End Sub


    Private Sub Select_Auto_Details()
        Dim C = New C
        C.Con.Open()
        With C.Com
            .Connection = C.Con
            .CommandText = "Pr_SelectDetails"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Pr_ID", Pr_ID)
        End With

        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            C.Dr.Read()
            Pr_UserName_txt.Text = C.Dr("UserName")
            Pr_Time_txt.Text = C.Dr("Time")
            StartNotes_txt.Text = C.Dr("NotesOn_Start")
            EndNotes_txt.Text = C.Dr("NotesOn_End")
        End If
        C.Con.Close()

        C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "SelectDetails_IM_By_Periods"
            .Parameters.AddWithValue("@Pr_ID", Pr_ID)
            .Parameters.AddWithValue("GM_ID", 0)
            .CommandType = CommandType.StoredProcedure
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        IM_Details_GV.DataSource = C.Dt
    End Sub

    Private Sub Select_Other_Details()
        Dim C = New C
        If PeriodsCmb.SelectedIndex > -1 Then
            C.Con.Open()
            With C.Com
                .Connection = C.Con
                .CommandText = "Pr_SelectDetails"
                .CommandType = CommandType.StoredProcedure

                If Pr_Auto_Print = False Then
                    .Parameters.AddWithValue("@Pr_ID", PeriodsCmb.SelectedValue)
                Else
                    .Parameters.AddWithValue("@Pr_ID", Pr_ID)
                End If

            End With

            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                Pr_UserName_txt.Text = C.Dr("UserName")
                Pr_Time_txt.Text = C.Dr("Time")
                StartNotes_txt.Text = C.Dr("NotesOn_Start")
                EndNotes_txt.Text = C.Dr("NotesOn_End")
            End If
            C.Con.Close()
        Else
            Pr_UserName_txt.Text = " الكــل "
            Pr_Time_txt.Text = " من تاريخ " + HOME.DateRange_Flate.D_F.Value.ToShortDateString + " إلى " + HOME.DateRange_Flate.D_T.Value.ToShortDateString
        End If

        C = New C
        With (C.Com)
            .Connection = C.Con
            If PeriodsCmb.SelectedIndex > -1 Then
                .CommandText = "SelectDetails_IM_By_Periods"
                .Parameters.AddWithValue("@Pr_ID", PeriodsCmb.SelectedValue)
                .Parameters.AddWithValue("GM_ID", GM_cmb.SelectedValue)
            Else
                .CommandText = "SelectDetails_IM_By_Date"
                .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
                .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
                .Parameters.AddWithValue("GM_ID", GM_cmb.SelectedValue)
            End If
            .CommandType = CommandType.StoredProcedure
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        IM_Details_GV.DataSource = C.Dt
    End Sub


    Private Sub Fetch_Pr_Details_Click(sender As Object, e As EventArgs) Handles Fetch_Pr_Details.Click
        If PeriodsCmb.Items.Count.ToString > 0 Then Fetch_Pr_Details_()
    End Sub


    Private Sub SB_PrintIM_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SB_PrintIM_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SB_PrintTotal_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SB_PrintTotal_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Fetch_Pr_Btn_Click(sender As Object, e As EventArgs) Handles Fetch_Pr_Btn.Click
        Me.Cursor = Cursors.AppStarting
        is_By_Pr = True
        Load_Date_Periods()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Date_Search_Btn_Click(sender As Object, e As EventArgs) Handles Date_Search_Btn.Click
        is_By_Pr = False
        PeriodsCmb.DataSource = Nothing
        Fetch_Pr_Details_()
    End Sub

    'Private Sub ButtPrintItems_Click(sender As Object, e As EventArgs) Handles ButtPrintItems.Click
    '    PrintIM()
    'End Sub

    'Private Sub PrintIM()
    '    Dim pp As New ReportConnection
    '    If IM_Print_Typecmb.SelectedIndex = 0 Then
    '        pp.rp.Load(Application.StartupPath & "\reports\IM_A4.rpt")
    '    ElseIf IM_Print_Typecmb.SelectedIndex = 1 Then
    '        pp.rp.Load(Application.StartupPath & "\reports\IM.rpt")
    '    Else
    '        MsgBox("حدد نوع الطباعة", MsgBoxStyle.Exclamation)
    '    End If

    '    pp.LoadTables()
    '    With pp
    '        .rp.SetParameterValue(0, USER_NAME)
    '        .rp.SetParameterValue(1, My_Settings.Server_Desc)
    '    End With

    '    Dim p As New print
    '    p.CrystalReportViewer1.ReportSource = pp.rp
    '    p.Show()

    '    'pp.rp.PrintOptions.PrinterName = Default_Printer_80
    '    'pp.rp.PrintToPrinter(1, False, 0, 0)
    '    'pp.rp.Dispose()

    'End Sub


    Function GetMailItems() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "-----كل التصنيفات-----"))

        Dim c1 As New C
        Dim s As String = "select GM_Name as 'name' ,GM_ID as 'ID' from General_Menu "
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


    Private Sub txtFinalTotalSales_TextChanged(sender As Object, e As EventArgs)
        On Error Resume Next
        Me.txtCharFinalTotalSales.Text = HANY(Val(Finencial_T_txt.Text), "EGYPT")
    End Sub


    Private Sub Pr_IM_Moves_Small()


        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\SB_IM_Small.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, Pr_UserName_txt.Text)
            .rp.SetParameterValue(1, Pr_Time_txt.Text)
            .rp.SetParameterValue(2, USER_NAME)
            .rp.SetParameterValue(3, MY_Settings.Server_Desc)
            .rp.SetParameterValue(4, GM_cmb.Text)

        End With

        If MY_Settings.Pr_Printer_isShow = True Then
            Dim p As New print
            p.CrystalReportViewer1.ReportSource = pp.rp
            p.Show()
        Else
            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_80))
            pp.rp.PrintOptions.PrinterName = Default_Printer_80
            pp.rp.PrintToPrinter(1, False, 0, 0)
            pp.rp.Dispose()
        End If

    End Sub

    Private Sub Pr_IM_Moves_A4()


        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\Pr_IM_Moves_A4.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, USER_NAME)
            .rp.SetParameterValue(1, Pr_UserName_txt.Text + " / " + Pr_Time_txt.Text)
            .rp.SetParameterValue(2, MY_Settings.Server_Desc)
            .rp.SetParameterValue(3, GM_cmb.Text)
        End With


        If MY_Settings.Pr_Printer_isShow = True Then
            Dim p As New print
            p.CrystalReportViewer1.ReportSource = pp.rp
            p.Show()
        Else
            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_A4))
            pp.rp.PrintOptions.PrinterName = Default_Printer_A4
            pp.rp.PrintToPrinter(1, False, 0, 0)
            pp.rp.Dispose()
        End If

    End Sub

    Private Sub POS_Report_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Pr_Auto_Print = True Then
            Fetch_Pr_Details_()
            Pr_Auto_Print = False
            Me.Close()
        Else
            Pr_Panel.Visible = S_Pr
            Fetch_Pr_Btn.Visible = S_Pr

            rs.FindAllControls(Me)
            Print_isShow_CB.Checked = MY_Settings.Pr_Printer_isShow
            fetch_GM()

        End If
    End Sub

    Private Sub GM_cmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GM_cmb.SelectedIndexChanged
        On Error Resume Next
        Fetch_Pr_Details_()
    End Sub

    Private Sub POS_Report_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub CASH_T_M_txt_TextChanged(sender As Object, e As EventArgs) Handles Finencial_T_txt.TextChanged
        Me.txtCharFinalTotalSales.Text = HANY(Val(Finencial_T_txt.Text), "EGYPT")
    End Sub

    Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        SHOW_Agents_Reciepts.ShowDialog()
    End Sub


    Private Sub Printer_isShow_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Print_isShow_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.Pr_Printer_isShow = Print_isShow_CB.Checked
        Save_AppSetting()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SHOW_Agents_SALES.ShowDialog()
    End Sub
End Class