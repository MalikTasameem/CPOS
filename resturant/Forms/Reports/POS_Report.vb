Imports System.Data.SqlClient
Imports System.Drawing.Printing

Imports System.Threading.Tasks

Public Class POS_Report

    Public Pr_Auto_Print As Boolean
    Public is_By_Pr As Boolean = False
    Dim rs As New Resizer
    Private Const ReportCommandTimeout As Integer = 180
    Private IsReportLoading As Boolean = False

    Private Class POSReportRequest
        Public Property IsAutoPrint As Boolean
        Public Property PrID As Integer
        Public Property HasPeriod As Boolean
        Public Property SelectedPrID As Integer
        Public Property GMID As Integer
        Public Property DateFrom As Date
        Public Property DateTo As Date
    End Class

    Private Class POSReportResult
        Public Property FinancialTable As DataTable = New DataTable()
        Public Property PayTable As DataTable = New DataTable()
        Public Property IMDetailsTable As DataTable = New DataTable()
        Public Property UserName As String = " الكــل "
        Public Property TimeText As String = ""
        Public Property StartNotes As String = "--"
        Public Property EndNotes As String = "--"
    End Class
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

    Private Async Function Fetch_Pr_Details_() As Task

        If IsReportLoading Then Return

        Dim Request = Build_POS_Report_Request()

        Try
            SetReportLoading(True, "جاري تحميل التقرير ...")

            Dim Result = Await Task.Run(Function() Load_POS_Report_Data(Request))
            Apply_POS_Report_Data(Result)

            If Request.IsAutoPrint Then
                LoadingLabel.Text = "جاري تجهيز الطباعة ..."
                Await Task.Delay(50)

                If Pr_PrinterPage_Type = 0 Then
                    PrintSalesAll()
                Else
                    PrintSales_Small()
                End If
            End If

        Catch ex As SqlException When ex.Number = -2
            MsgBox("انتهت مهلة جلب التقرير، حاول تقليل نطاق التاريخ أو جلب فترة محددة.", MsgBoxStyle.Exclamation, "تنبيه")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            SetReportLoading(False)
        End Try

    End Function

    Private Function Build_POS_Report_Request() As POSReportRequest

        Dim Request As New POSReportRequest
        Request.IsAutoPrint = Pr_Auto_Print
        Request.PrID = Pr_ID
        Request.DateFrom = HOME.DateRange_Flate.D_F.Value
        Request.DateTo = HOME.DateRange_Flate.D_T.Value
        Request.GMID = GetComboIntValue(GM_cmb, 0)

        If PeriodsCmb.SelectedIndex > -1 Then
            Request.HasPeriod = True
            Request.SelectedPrID = GetComboIntValue(PeriodsCmb, 0)
        End If

        Return Request

    End Function

    Private Function Load_POS_Report_Data(ByVal Request As POSReportRequest) As POSReportResult

        Dim Result As New POSReportResult
        Dim DS As New DataSet()

        Using sqlCon As New SqlConnection(MY_Settings.SqlConStr)
            sqlCon.Open()

            Using sqlComm As New SqlCommand()
                sqlComm.Connection = sqlCon
                sqlComm.CommandType = CommandType.StoredProcedure
                sqlComm.CommandTimeout = ReportCommandTimeout

                If Request.IsAutoPrint Then
                    sqlComm.CommandText = "Count_Total_Balance_By_Periods"
                    sqlComm.Parameters.AddWithValue("@Pr_ID", Request.PrID)
                Else
                    If Request.HasPeriod = False Then
                        sqlComm.CommandText = "Count_Total_Balance_By_Date"
                        sqlComm.Parameters.AddWithValue("@D_F", Request.DateFrom)
                        sqlComm.Parameters.AddWithValue("@D_T", Request.DateTo)
                    Else
                        sqlComm.CommandText = "Count_Total_Balance_By_Periods"
                        sqlComm.Parameters.AddWithValue("@Pr_ID", Request.SelectedPrID)
                    End If
                End If

                Using Da As New SqlDataAdapter(sqlComm)
                    Da.Fill(DS)
                End Using
            End Using

            If DS.Tables.Count > 0 Then Result.FinancialTable = DS.Tables(0)
            If DS.Tables.Count > 1 Then Result.PayTable = DS.Tables(1)

            If Request.IsAutoPrint Then
                Load_POS_Period_Details(sqlCon, Request.PrID, Result)
                Result.IMDetailsTable = Load_POS_IM_Details(sqlCon, True, Request.PrID, Request.GMID, Request.DateFrom, Request.DateTo)
            Else
                If Request.HasPeriod Then
                    Load_POS_Period_Details(sqlCon, Request.SelectedPrID, Result)
                    Result.IMDetailsTable = Load_POS_IM_Details(sqlCon, True, Request.SelectedPrID, Request.GMID, Request.DateFrom, Request.DateTo)
                Else
                    Result.UserName = " الكــل "
                    Result.TimeText = " من تاريخ " + Request.DateFrom.ToShortDateString + " إلى " + Request.DateTo.ToShortDateString
                    Result.IMDetailsTable = Load_POS_IM_Details(sqlCon, False, 0, Request.GMID, Request.DateFrom, Request.DateTo)
                End If
            End If
        End Using

        Return Result

    End Function

    Private Sub Load_POS_Period_Details(ByVal sqlCon As SqlConnection, ByVal PeriodID As Integer, ByVal Result As POSReportResult)

        Using sqlComm As New SqlCommand("Pr_SelectDetails", sqlCon)
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandTimeout = ReportCommandTimeout
            sqlComm.Parameters.AddWithValue("@Pr_ID", PeriodID)

            Using Dr = sqlComm.ExecuteReader()
                If Dr.HasRows Then
                    Dr.Read()
                    Result.UserName = Dr("UserName").ToString()
                    Result.TimeText = Dr("Time").ToString()
                    Result.StartNotes = Dr("NotesOn_Start").ToString()
                    Result.EndNotes = Dr("NotesOn_End").ToString()
                End If
            End Using
        End Using

    End Sub

    Private Function Load_POS_IM_Details(ByVal sqlCon As SqlConnection, ByVal ByPeriod As Boolean, ByVal PeriodID As Integer, ByVal GMID As Integer, ByVal DateFrom As Date, ByVal DateTo As Date) As DataTable

        Dim Dt As New DataTable()

        Using sqlComm As New SqlCommand()
            sqlComm.Connection = sqlCon
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.CommandTimeout = ReportCommandTimeout

            If ByPeriod Then
                sqlComm.CommandText = "SelectDetails_IM_By_Periods"
                sqlComm.Parameters.AddWithValue("@Pr_ID", PeriodID)
                sqlComm.Parameters.AddWithValue("GM_ID", GMID)
            Else
                sqlComm.CommandText = "SelectDetails_IM_By_Date"
                sqlComm.Parameters.AddWithValue("@D_F", DateFrom)
                sqlComm.Parameters.AddWithValue("@D_T", DateTo)
                sqlComm.Parameters.AddWithValue("GM_ID", GMID)
            End If

            Using Da As New SqlDataAdapter(sqlComm)
                Da.Fill(Dt)
            End Using
        End Using

        Return Dt

    End Function

    Private Sub Apply_POS_Report_Data(ByVal Result As POSReportResult)

        Finc_DT = Result.FinancialTable
        DataGridViewX.DataSource = Finc_DT
        Pay_Grid.DataSource = Result.PayTable
        IM_Details_GV.DataSource = Result.IMDetailsTable

        Finencial_T_txt.Text = GetFinancialNetValue(Finc_DT).ToString()

        Pr_UserName_txt.Text = Result.UserName
        Pr_Time_txt.Text = Result.TimeText
        StartNotes_txt.Text = Result.StartNotes
        EndNotes_txt.Text = Result.EndNotes

        TabControl1.SelectedTab = TabPage1

    End Sub

    Private Function GetFinancialNetValue(ByVal Dt As DataTable) As Double

        If Dt Is Nothing OrElse Dt.Rows.Count = 0 OrElse Dt.Columns.Count <= 2 Then Return 0

        Dim FirstValue As Double = GetTableDoubleValue(Dt, 0, 2)
        Dim LastValue As Double = GetTableDoubleValue(Dt, Dt.Rows.Count - 1, 2)

        Return FirstValue - LastValue

    End Function

    Private Function GetTableDoubleValue(ByVal Dt As DataTable, ByVal RowIndex As Integer, ByVal ColumnIndex As Integer) As Double

        If Dt Is Nothing OrElse Dt.Rows.Count <= RowIndex OrElse Dt.Columns.Count <= ColumnIndex Then Return 0
        If IsDBNull(Dt.Rows(RowIndex)(ColumnIndex)) Then Return 0

        Return Convert.ToDouble(Dt.Rows(RowIndex)(ColumnIndex))

    End Function

    Private Function GetComboIntValue(ByVal combo As ComboBox, ByVal defaultValue As Integer) As Integer

        If combo Is Nothing OrElse combo.SelectedValue Is Nothing Then Return defaultValue

        Dim Value As Integer = defaultValue
        If Integer.TryParse(combo.SelectedValue.ToString(), Value) Then Return Value

        Return defaultValue

    End Function

    Private Sub SetReportLoading(ByVal Loading As Boolean, Optional ByVal Message As String = "جاري تحميل التقرير ...")

        IsReportLoading = Loading
        Me.UseWaitCursor = Loading
        Me.Cursor = If(Loading, Cursors.WaitCursor, Cursors.Hand)

        If LoadingLabel IsNot Nothing Then LoadingLabel.Text = Message
        If LoadingProgress IsNot Nothing Then LoadingProgress.MarqueeAnimationSpeed = If(Loading, 30, 0)

        If LoadingPanel IsNot Nothing Then
            LoadingPanel.Visible = Loading
            If Loading Then LoadingPanel.BringToFront()
        End If

        Fetch_Pr_Details.Enabled = Not Loading
        Date_Search_Btn.Enabled = Not Loading
        Fetch_Pr_Btn.Enabled = Not Loading
        SalesPrintButton.Enabled = Not Loading
        GM_cmb.Enabled = Not Loading
        PeriodsCmb.Enabled = Not Loading

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


    Private Async Sub Fetch_Pr_Details_Click(sender As Object, e As EventArgs) Handles Fetch_Pr_Details.Click
        If PeriodsCmb.Items.Count > 0 Then Await Fetch_Pr_Details_()
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

    Private Async Sub Date_Search_Btn_Click(sender As Object, e As EventArgs) Handles Date_Search_Btn.Click
        is_By_Pr = False
        PeriodsCmb.DataSource = Nothing
        Await Fetch_Pr_Details_()
    End Sub


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

    Private Async Sub POS_Report_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Pr_Auto_Print = True Then
            Await Fetch_Pr_Details_()
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

    Private Async Sub GM_cmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GM_cmb.SelectedIndexChanged
        If GM_cmb.SelectedIndex > -1 Then Await Fetch_Pr_Details_()
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
