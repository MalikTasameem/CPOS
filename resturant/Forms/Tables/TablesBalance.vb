Imports System.IO.Ports

Public Class TablesBalance
    Dim rs As New Resizer
    Public tran_F, tran_T
    Public Bill_T_ID, AG_ID, TB_Num As Integer
    Public Pied_Money As Double
    Public IS_SHOW_NUMBER As Boolean = False
    Public TB_ID As Integer

    'Dim RightPanelWidth As Integer
    'Public IsPanelHidden As Boolean = False

    Private Sub Refrech_btn_Click(sender As Object, e As EventArgs) Handles Refrech_btn.Click
        '   F_TablesMenu = New TablesMenu
        '    Set_Form(F_TablesMenu, F_Panel)
        loadtables()
        ' TablePiedApart.TB_Num = TB_Num
        SB_Contents_SELECT_TB(TB_Num)
        TB_NotPied_V_SELECT_Bill(TB_Num)
        Me.TB_Info.Text = " الطاولة : " + TB_Num.ToString
        Me.Items_btn_Click(sender, e)
    End Sub

    Public Sub loadtables()
        F_Panel.Controls.Clear()

        Dim c As New C
        Dim x = 0
        Dim y = 0
        Dim counter = 1
        ' Dim FontSize, StandarMeasure As Double
        Dim b As Integer = 0
        Dim s As String = ""
        Dim IMName As String = ""
        If U_Flate_ID = 0 Then
            s = "select * from Tables_Balances_V ORDER BY TB_ID ASC "
        Else
            s = "select * from Tables_Balances_V WHERE Flate_ID = '" & U_Flate_ID & "'  ORDER BY TB_ID ASC  "
        End If

        c.Com = New SqlClient.SqlCommand(s, c.Con)

        c.Con.Open()
        c.Dr = c.Com.ExecuteReader
        If c.Dr.HasRows Then
            While c.Dr.Read()
                Dim IMbtn As New Button
                IMbtn.Name = ("T_Name" + c.Dr("TB_ID").ToString)
                IMbtn.AutoSize = False
                IMbtn.Cursor = Cursors.Hand
                IMbtn.FlatStyle = FlatStyle.Popup
                IMbtn.Location = New System.Drawing.Point(x, y)
                IMbtn.Size = New System.Drawing.Size(F_Panel.Size.Width / 6.2, F_Panel.Size.Height / 5.25)
                IMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
                IMName = c.Dr("T_Name")
                IMbtn.Font = New System.Drawing.Font("Segoe UI", (h + w) / 125, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
                IMbtn.Text = c.Dr("T_Name")
                IMbtn.Tag = c.Dr("TB_ID")
                If c.Dr("isbusy") = True Then
                    IMbtn.BackColor = Color.IndianRed
                Else
                    IMbtn.BackColor = Color.WhiteSmoke
                End If
                Controls.Add(IMbtn)
                IMbtn.Parent = F_Panel
                AddHandler IMbtn.Click, AddressOf bt_Click

                If counter = 6 Then
                    counter = 1
                    x = 0
                    y += F_Panel.Size.Height / 5.25
                Else
                    counter += 1
                    x += F_Panel.Size.Width / 6.2
                End If
                rs.Find_One(IMbtn)

            End While
        End If

        c.Con.Close()

    End Sub

    Public Sub bt_Click(ByVal sender As Object, ByVal e As EventArgs)
        TB_Num = sender.tag

        If Me.TB_Types_CMB.SelectedIndex <> 0 Then

            If Me.tran_F = 0 Then
                Me.tran_F = sender.tag
                Me.TB_F_txt.Text = sender.Text
                Exit Sub
            End If

            If Me.tran_T = 0 Then
                Me.tran_T = sender.tag
                Me.TB_T_txt.Text = sender.Text
            End If

        Else
            ' TablePiedApart.TB_Num = TB_Num
            SB_Contents_SELECT_TB(TB_Num)
            TB_NotPied_V_SELECT_Bill(TB_Num)
            Me.TB_Info.Text = " الطاولة : " + sender.Text
            Me.Items_btn_Click(sender, e)


        End If
    End Sub




    Public Sub SB_Contents_SELECT_TB(TB_ID As Integer)
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "TB_NotPied_V_SELECT_TB"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@TB_ID", TB_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        Me.IMGrid.DataSource = C.Dt

        Dim QTY = 0
        For i = 0 To Me.IMGrid.Rows.Count - 1
            QTY = QTY + IMGrid.Rows(i).Cells("QTY_CL").Value
        Next

        TB_Info_LB.Text = " عدد المواد : " & IMGrid.RowCount.ToString & "  /    عدد الكميات :  " & QTY.ToString


    End Sub

    Public Sub TB_NotPied_V_SELECT_Bill(TB_ID As Integer)
        Dim C As New C

        Dim s As String
        With C.Com
            .Connection = C.Con
            .CommandText = "TB_NotPied_V_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@TB_ID", TB_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        Me.BillsMetroGrid.DataSource = C.Dt


        C = New C
        Try
            s = "SELECT CONVERT(INT,ISNULL(SUM(QTY),0)) AS C  from TABLES_PREV_APARTS_V WHERE TB_ORDER_CODE =  ( SELECT TB_ORDER_CODE FROM TABLES WHERE TB_ID = '" & Me.TB_Num & "' )"
            C.Com = New SqlClient.SqlCommand(s, C.Con)
            C.Con.Open()
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                Apart_List_btn.Text = "(" & C.Dr("C") & ")"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        Time_Table_Label.Text = ""
        C = New C
        s = "SELECT ISNULL(Start_Open,'') AS Start_Open,ISNULL([dbo].[get_TimeDuration](Start_Open),'') AS Time_Duration from Tables_Bills_NotPied_V where Table_ID = '" & Me.TB_Num & "'"
        C.Com = New SqlClient.SqlCommand(s, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                Time_Table_Label.Text = " بداية الدخول:- " & C.Dr("Start_Open") & vbNewLine & " الفترة:- " & C.Dr("Time_Duration")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()


    End Sub

    Private Sub TablesBalance_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Home_Panel = "POS"
        Me.Dispose()
    End Sub

    Private Sub TablesBalance_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then If SaveButton.Enabled = True Then SaveButton_Click(sender, e)
    End Sub

    Private Sub TablesBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        Home_Panel = ""

        'RightPanelWidth = PanelRight.Width

        If IS_SHOW_NUMBER = False Then
            loadtables()
        Else
            SB_Contents_SELECT_TB(TB_ID)
            TB_NotPied_V_SELECT_Bill(TB_ID)
            TB_Info.Text = " الطاولة : " + TB_ID.ToString
            Items_btn.PerformClick()
        End If



        BillsMetroGrid.Columns("Edit_TB_IMQty_CL").Visible = U_SB_Update
        Tables_Option_GB.Visible = U_Transfer_Table
        TB_Transfer_Panel.Visible = U_Transfer_Table

        SaveButton.Visible = U_End_Table
        PiedApart_btn.Visible = U_End_Table
        Debit_Table_btn.Visible = U_End_Table

        TB_Types_CMB.SelectedIndex = 0
        isPrintBeforeEndBill_CB.Checked = Print_TB_Before_End

        Show_AllBill_Clmns_CB.Checked = Show_AllBill_Clmns
        show_bill_tb_columns()

        'If IsPanelHidden = False Then

        '    ' اخفاء الجزء
        '    PanelRight.Visible = False

        '    ' تصغير عرض الفورم
        '    Me.Width = Me.Width - RightPanelWidth

        '    IsPanelHidden = True

        'End If


    End Sub


    'Public Sub ToggleRightPanel()

    '    If PanelRight.Visible = True Then
    '        PanelRight.Visible = False
    '    Else
    '        PanelRight.Visible = True
    '    End If

    'End Sub

    Private Sub TablesBalance_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls_POS(Me)
    End Sub

    Private Sub MetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles BillsMetroGrid.RowsAdded
        Calc_Bill()
    End Sub

    Private Sub MetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles BillsMetroGrid.RowsRemoved
        Calc_Bill()
    End Sub

    Public Sub Calc_Bill()
        Dim pure As Double = 0
        Dim discount As Double = 0
        Dim total As Double = 0

        For i = 0 To Me.BillsMetroGrid.Rows.Count - 1
            total = total + BillsMetroGrid.Rows(i).Cells("Total_TB_CL").Value
            discount = discount + BillsMetroGrid.Rows(i).Cells("Discount_CL").Value
            pure = pure + BillsMetroGrid.Rows(i).Cells("Pure_CL").Value
        Next
        Total_Label.Text = " الإجمالي: " & total.ToString & " \ التخفيض: " & discount.ToString
        Me.PureTextBox.Text = pure
        If is_Use_Total_Port = True Then Show_Total_Port(pure)
    End Sub

    'Private Sub Show_Total_Port(Pure As Double)
    '    Dim sp As SerialPort = New SerialPort(Total_Port, 9600, Parity.None, 8, StopBits.One)
    '    sp.Open()
    '    sp.Write(Convert.ToString(ChrW(12)))
    '    sp.WriteLine(" TABLE : " & TB_Num.ToString)
    '    sp.WriteLine(ChrW(13) & "TOTAL : " & Pure.ToString)
    '    sp.Close()
    '    sp.Dispose()
    '    sp = Nothing
    'End Sub

    Public Sub Show_Total_Port(Pure As Double)
        Try
            Dim sp As SerialPort = New SerialPort(Total_Port, 2400, Parity.None, 8, StopBits.One)
            sp.Open()
            sp.Write(Convert.ToString(ChrW(12)))
            sp.WriteLine(Pure)
            sp.Close()
            sp.Dispose()
            sp = Nothing
        Catch ex As Exception

        End Try

        '-------------------------------------------------------------------------------
    End Sub

    Public Sub Items_btn_Click(sender As Object, e As EventArgs) Handles Items_btn.Click
        BillsMetroGrid.Visible = False
        IMGrid.Visible = True
    End Sub

    Private Sub Bills_btn_Click(sender As Object, e As EventArgs) Handles Bills_btn.Click
        BillsMetroGrid.Visible = True
        IMGrid.Visible = False
    End Sub


    Private Sub BillsMetroGrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles BillsMetroGrid.CellContentClick

        If e.ColumnIndex = 0 Then
            Me.Cursor = Cursors.AppStarting
            'F_POS.SMPanel.Controls.Clear()
            F_POS.Reset_Fields()
            F_POS.isNewBill = 0
            F_POS.T_ID = BillsMetroGrid.CurrentRow.Cells("Bills_T_ID_CL").Value
            F_POS.BillNumTxt.Text = BillsMetroGrid.CurrentRow.Cells("B_Pr_ID_CL").Value
            Switch_To_Cash = True
            F_POS.Fill_Bill_Info()
            F_POS.SB_Contents_SELECT_Bill()
            F_POS.BillTypeCmb.SelectedValue = BillsMetroGrid.CurrentRow.Cells("B_Type_CL").Value
            F_SalesMenu.Hide()
            Me.Cursor = Cursors.Default
        End If

        If e.ColumnIndex = 1 Then
            If BillsMetroGrid.Rows.Count > 0 Then
                Me.Cursor = Cursors.AppStarting
                F_TB_BillIM = New TB_BillIM
                F_TB_BillIM.SB_ID = BillsMetroGrid.CurrentRow.Cells("B_D_ID_CL").Value
                F_TB_BillIM.SB_Bill_DayNum = BillsMetroGrid.CurrentRow.Cells("B_Pr_ID_CL").Value
                F_TB_BillIM.Pure = BillsMetroGrid.CurrentRow.Cells("Pure_CL").Value
                Set_Form(F_TB_BillIM, F_Panel)
                Bill_T_ID = BillsMetroGrid.CurrentRow.Cells("Bills_T_ID_CL").Value
                F_TB_BillIM.Select_IM()
                Me.Cursor = Cursors.Default
            End If
        End If

    End Sub


    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        Pied_Money = 0
        If BillsMetroGrid.Rows.Count > 0 Then
            If Check_AG() = 0 Then
                MsgBox("يجب توحيد الزبون على الطاولة", MsgBoxStyle.Critical, "")
            Else
                Beep()
                If MessageBox.Show(" إغلاق حساب " + TB_Info.Text.ToString, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    If AG_ID = Default_AG_ID Then
                        If String.IsNullOrWhiteSpace(PureTextBox.Text) = False Then Pied_Money = PureTextBox.Text
                        If SB_AutoOpenDrawer = True Then Open_Cash_Drawer()
                        If isPrintBeforeEndBill_CB.Checked = True Then Print_Table_Bill()
                        PiedUp_Table(0)
                    Else
                        TablesFindingRest.ShowDialog()
                        If TablesFindingRest.is_Back = False Then
                            If SB_AutoOpenDrawer = True Then Open_Cash_Drawer()
                            If isPrintBeforeEndBill_CB.Checked = True Then Print_Table_Bill()
                            PiedUp_Table(0)
                        End If

                    End If

                End If
            End If
        End If
    End Sub

    Private Function Check_AG()
        AG_ID = BillsMetroGrid.Rows(0).Cells("AG_ID_CL").Value
        For i = 1 To BillsMetroGrid.Rows.Count - 1

            If AG_ID <> BillsMetroGrid.Rows(i).Cells("AG_ID_CL").Value Then
                Return 0
            End If
        Next
        Return 1
    End Function


    Private Sub PiedUp_Table(is_Debit As Boolean)
        Dim F As New Pay_Main_Form
        F.MONEY_VALUE = Pied_Money
        F.Temp_Tr_ID = SB_TR_ID
        F.AG_ID = AG_ID
        F.ShowDialog()

        If F.is_OK = True Then
            Dim Tr_ID, Pay_ID As Integer
            Tr_ID = F.Tr_ID
            Pay_ID = F.Pay_ID

            Dim C As New C
            With C.Com
                .Connection = C.Con
                .CommandText = "PiedUp_Table"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@Pr_ID", Pr_ID)
                .Parameters.AddWithValue("@TB_ID", Me.TB_Num)
                If is_Debit = True Then
                    .Parameters.AddWithValue("@AG_ID", U_AG_ID)
                Else
                    .Parameters.AddWithValue("@AG_ID", AG_ID)
                End If
                .Parameters.AddWithValue("@Tr_ID", Tr_ID)
                .Parameters.AddWithValue("@USER_ID", USER_ID)
                .Parameters.AddWithValue("@Total", Pied_Money)
                .Parameters.AddWithValue("@TB_Info", TB_Info.Text)
                .Parameters.AddWithValue("@is_Debit", is_Debit)
                .Parameters.AddWithValue("@Pay_ID", Pay_ID)

            End With
            If SQL_SP_EXEC(C.Com) Then

                'If SB_AutoOpenDrawer = True Then Open_Cash_Drawer()
                'If isPrintBeforeEndBill_CB.Checked = True Then Print_Table_Bill()

                Me.SB_Contents_SELECT_TB(Me.TB_Num)
                Me.TB_NotPied_V_SELECT_Bill(Me.TB_Num)
                Refresh_Table()

                If IS_SHOW_NUMBER = True Then Me.Close()
            End If


        End If
    End Sub

    Public Sub Refresh_Table()
        For Each A As Control In F_Panel.Controls
            If TypeOf A Is Button Then
                If A.Tag = TB_Num Then
                    A.BackColor = Color.WhiteSmoke
                    Exit Sub
                End If
            End If
        Next
    End Sub


    Private Sub TB_ButOnAG_btn_Click(sender As Object, e As EventArgs) Handles TB_ButOnAG_btn.Click
        F_AgentsMenu = New AgentsMenu
        F_AgentsMenu.New_AG_Btn.Visible = False
        F_AgentsMenu.ShowDialog()
    End Sub

    Private Sub PrintBillButton_Click(sender As Object, e As EventArgs) Handles PrintBillButton.Click
        Print_Table_Bill()
    End Sub

    Private Sub Print_Table_Bill()
        If IMGrid.Rows.Count > 0 Then
            Me.Cursor = Cursors.AppStarting
            TBPrint()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Public Sub TBPrint()
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\Table_SB.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, TB_Info.Text)
            .rp.SetParameterValue(1, USER_NAME)
            .rp.SetParameterValue(2, PureTextBox.Text)
            .rp.SetParameterValue(3, Me.TB_Num)
            .rp.SetParameterValue(4, SBill_Title_1)
            .rp.SetParameterValue(5, SBill_Title_2)
            .rp.SetParameterValue(6, SBill_Footer)
        End With

        If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_80))
        pp.rp.PrintOptions.PrinterName = Default_Printer_80
        pp.rp.PrintToPrinter(1, False, 0, 0)
        pp.rp.Dispose()

        'Dim p As New print
        'p.CrystalReportViewer1.ReportSource = pp.rp
        'p.Show()
    End Sub
    Private Sub TranConfirm_btn_Click(sender As Object, e As EventArgs) Handles TranConfirm_btn.Click
        If TB_Types_CMB.SelectedIndex = 1 Then
            If MessageBox.Show(" نقل كل الأصناف من طاولة " + TB_F_txt.Text + " إلى  " + TB_T_txt.Text, "نقل", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then Transform_Tables()
        ElseIf TB_Types_CMB.SelectedIndex = 2 Then
            Tables_Trans_IM.TB_Num = Me.tran_F
            Tables_Trans_IM.TB_TO_Num = Me.tran_T
            Tables_Trans_IM.ShowDialog()
        End If
        Clear_TB_Types_Fields()
    End Sub

    Private Sub Transform_Tables()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Transform_Tables"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Table_From", tran_F)
            .Parameters.AddWithValue("@Table_To", tran_T)
        End With
        If SQL_SP_EXEC(C.Com) Then
            Network_Edit_Tracker_insert("نقل من طاولة  : " + TB_F_txt.Text + " إلى :  " + TB_T_txt.Text, 0, 28, 3)
            'TB_Tran_CB.Checked = False
            loadtables()
            'TB_F_txt.Clear()
            'TB_T_txt.Clear()
            Clear_TB_Types_Fields()
        End If
    End Sub

    'Private Sub TB_Tran_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    'If TB_Tran_CB.Checked = True Then
    '    '    TB_Tran_CB.ForeColor = Color.DarkGreen
    '    'Else
    '    '    TB_Tran_CB.ForeColor = Color.Black
    '    'End If
    '    tran_F = 0
    '    tran_T = 0
    '    TB_F_txt.Clear()
    '    TB_T_txt.Clear()
    'End Sub


    Public Sub Refresh_TB_Balance()
        Me.SB_Contents_SELECT_TB(Me.TB_Num)
        Me.TB_NotPied_V_SELECT_Bill(Me.TB_Num)
        F_TB_BillIM.Select_IM()
    End Sub

    Private Sub PiedApart_btn_Click(sender As Object, e As EventArgs) Handles PiedApart_btn.Click
        TB_Part_Pied = True
        TablePiedApart.TB_Num = Me.TB_Num
        TablePiedApart.ShowDialog()
        TB_Part_Pied = False
    End Sub

    Private Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        Dim newLeft As Integer = -F_Panel.AutoScrollPosition.X
        Dim newTop As Integer = -F_Panel.AutoScrollPosition.Y
        newTop = -F_Panel.AutoScrollPosition.Y - 450
        F_Panel.AutoScrollPosition = New Point(newLeft, newTop)
    End Sub

    Private Sub Button51_Click(sender As Object, e As EventArgs) Handles Button51.Click
        Dim newLeft As Integer = -F_Panel.AutoScrollPosition.X
        Dim newTop As Integer = -F_Panel.AutoScrollPosition.Y
        newTop = -F_Panel.AutoScrollPosition.Y + 450
        F_Panel.AutoScrollPosition = New Point(newLeft, newTop)
    End Sub


    Private Sub Debit_Table_btn_Click(sender As Object, e As EventArgs) Handles Debit_Table_btn.Click
        If U_AG_ID = 0 Then
            MsgBox("تأكد من بيانات المستخدم", MsgBoxStyle.Critical, " خطأ ")
        Else
            Beep()
            If MessageBox.Show(" تحويل حساب  " + TB_Info.Text.ToString + " كمعاملة دين ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                                   MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                Pied_Money = PureTextBox.Text
                PiedUp_Table(1)
            End If
        End If


    End Sub

    Private Sub Apart_List_btn_Click(sender As Object, e As EventArgs) Handles Apart_List_btn.Click
        Show_Tables_Aparts.ShowDialog()
    End Sub

    Private Sub Show_AllBill_Clmns_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Show_AllBill_Clmns_CB.CheckedChanged
        CB_CHecked(sender)
        Show_AllBill_Clmns = Show_AllBill_Clmns_CB.Checked

        show_bill_tb_columns()
        MY_Settings.Save_AppSetting()
    End Sub

    Private Sub show_bill_tb_columns()
        BillsMetroGrid.Columns("Date_CL").Visible = Show_AllBill_Clmns
        BillsMetroGrid.Columns("Total_TB_CL").Visible = Show_AllBill_Clmns
        BillsMetroGrid.Columns("Discount_CL").Visible = Show_AllBill_Clmns
        BillsMetroGrid.Columns("User_Name_CL").Visible = Show_AllBill_Clmns
    End Sub

    Private Sub ClearNumBtn_Click(sender As Object, e As EventArgs) Handles ClearNumBtn.Click
        Clear_TB_Types_Fields()
    End Sub

    Private Sub Clear_TB_Types_Fields()
        TB_F_txt.Clear()
        TB_T_txt.Clear()
        tran_F = 0
        tran_T = 0
        TB_Types_CMB.SelectedIndex = 0
    End Sub

    Private Sub isPrintBeforeEndBill_CB_CheckedChanged(sender As Object, e As EventArgs) Handles isPrintBeforeEndBill_CB.CheckedChanged
        CB_CHecked(sender)
        Print_TB_Before_End = isPrintBeforeEndBill_CB.Checked
        MY_Settings.Save_AppSetting()
    End Sub
End Class