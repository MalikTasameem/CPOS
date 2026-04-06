Imports System.Data.SqlClient
Imports System.IO
Imports System.Management



Public Class MainForm



    Dim rs As New Resizer

    Dim TimeConuter As Integer = 0
    Dim TimeConuter2 As Integer = 0

    Dim IM_Valid As String = ""
    Dim IM_Min As String = ""
    Dim IM_Max As String = ""
    Dim AG_B As String = ""
    Dim Open_AGMV As String = ""
    Dim IM_SELL_LESS_THAN_COST As String = ""
    Dim IM_Neg_QTY As String = ""
    Dim IM_Notes_Valid As String = ""
    Dim RCT_NOT_WITH_SB As String = "" 'إيصالات لا تساوي الفواتير
    Dim RSV_IM As String = ""
    Dim ORDER_IM As String = ""
    Dim ST_OTHER As String = ""
    Dim RCT_NOT_WITH_SB_DT As New DataTable
    Dim is_Con As Boolean = False
    Dim Valid_N = 0
    'Dim Open_Bills As String = ""
    Dim drag As Boolean
    Dim mouseX As Integer
    Dim mouseY As Integer
    Private Sub TitleBar_Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseDown, Title_Label.MouseDown
        drag = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub TitleBar_Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseMove, Title_Label.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub

    Private Sub TitleBar_Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseUp, Title_Label.MouseUp
        drag = False
    End Sub

    Private Sub CloseBtn_Click(sender As Object, e As EventArgs) Handles CloseBtn.Click
        Application.Exit()
    End Sub

    Private Sub MaxBtn_Click(sender As Object, e As EventArgs) Handles MaxBtn.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub MinBtn_Click(sender As Object, e As EventArgs) Handles MinBtn.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Public Sub Mange_EmpValid()

        Dim c As New C
        Dim str As String = "Select * From Users_Validations_V Where User_id = '" & USER_ID & "'"
        c.Com = New SqlClient.SqlCommand(str, c.Con)

        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            c.Dr.Read()

            If c.Dr.HasRows = True Then

                If User_isAdmin = False Then
                    Sys_Setting_btn.Enabled = False
                    'Open_MV_DV.Columns("USER_NAME_CL").Visible = False
                End If

                If c.Dr("Sales") = True Or c.Dr("Pch") = True Then Agents_btn.Enabled = True

                If c.Dr("Sales") = False Then
                    Me.Sales_Btn.Enabled = False
                    CashLB.Enabled = False
                    User_POS = False
                    عرضفواتيرزبونToolStripMenuItem.Visible = False
                Else
                    Me.Sales_Btn.Enabled = True
                    CashLB.Enabled = True
                    User_POS = True
                    عرضفواتيرزبونToolStripMenuItem.Visible = True
                End If

                If c.Dr("Pch") = False Then
                    Me.Pch_btn.Enabled = False
                    Deliver_LB.Enabled = False
                    U_ADD_Pch = False
                Else
                    Me.Pch_btn.Enabled = True
                    Deliver_LB.Enabled = True
                    U_ADD_Pch = True
                End If

                If c.Dr("Exp") = False Then
                    Me.Exp_LB.Enabled = False
                Else
                    Me.Exp_LB.Enabled = True
                End If

                If c.Dr("PchVoid") = False Then
                    U_Cancel_Pch = False
                    ' U_Exp = False
                Else
                    U_Cancel_Pch = True
                    '  U_Exp = True
                End If

                If c.Dr("ExpVoid") = False Then
                    U_ExpVoid = False
                Else
                    U_ExpVoid = True
                End If


                U_Balance = c.Dr("Balance")
                'If c.Dr("Balance") = False Then
                '    Me.Balances_btn.Enabled = False
                '    Agents_btn.Enabled = False
                '    U_Balance = False
                'Else
                '    Me.Balances_btn.Enabled = True
                '    Agents_btn.Enabled = True
                '    U_Balance = True
                'End If

                If c.Dr("Reports") = False Then
                    Me.Reports_btn.Enabled = False
                Else
                    Me.Reports_btn.Enabled = True
                End If

                If c.Dr("SalesVoid") = False Then
                    U_SalesVoid = False
                Else
                    U_SalesVoid = True
                End If

                If c.Dr("Sales_Dis") = True And isDiscount = True Then
                    U_SalesDis = True
                Else
                    U_SalesDis = False
                End If

                If c.Dr("Update_IM_QTY") = False Then
                    U_Update_IM_QTY = False
                Else
                    U_Update_IM_QTY = True
                End If

                If c.Dr("Add_Draw_St") = False Then
                    U_Add_Draw_St = False
                    STTRANC_LB.Enabled = False
                Else
                    U_Add_Draw_St = False
                    STTRANC_LB.Enabled = True
                    U_Add_Draw_St = True
                End If

                If c.Dr("StExplore") = False Then
                    U_StExplore = False
                    ST_Explore_LB.Enabled = False
                    Invoice_LB.Enabled = False
                    IMEX_LB.Enabled = False
                    Prepare_Invoice_LB.Enabled = False
                    ST_Btn.Enabled = False
                Else
                    U_StExplore = True
                    ST_Explore_LB.Enabled = True
                    Invoice_LB.Enabled = True
                    IMEX_LB.Enabled = True
                    Prepare_Invoice_LB.Enabled = True
                    ST_Btn.Enabled = True
                End If

                If c.Dr("SB_Update") = False Then
                    U_SB_Update = False
                Else
                    U_SB_Update = True
                End If

                If c.Dr("Pch_Update") = False Then
                    U_Pch_Update = False
                Else
                    U_Pch_Update = True
                End If

                If c.Dr("SB_Rtn") = False Then
                    U_SB_Rtn = False
                    SBRtn_LB.Enabled = False
                Else
                    U_SB_Rtn = True
                    SBRtn_LB.Enabled = True
                End If

                If c.Dr("Pch_Rtn") = False Then
                    U_Pch_Rtn = False
                    Pch_Rtn_LB.Enabled = False
                Else
                    U_Pch_Rtn = True
                    Pch_Rtn_LB.Enabled = True
                End If

                If c.Dr("End_Table") = False Then
                    U_End_Table = False
                Else
                    U_End_Table = True
                End If

                If c.Dr("SB_IM_Update") = False Then
                    U_SB_IM_Update = False
                Else
                    U_SB_IM_Update = True
                End If

                If c.Dr("SB_Sell_Under_Cost") = False Then
                    U_SB_Sell_Under_Cost = False
                Else
                    U_SB_Sell_Under_Cost = True
                End If

                If c.Dr("SB_Show_Price_Info") = False Then
                    U_SB_Show_Price_Info = False
                Else
                    U_SB_Show_Price_Info = True
                End If

                If c.Dr("SB_Show_Cash") = False Then
                    U_SB_Show_Cash = False
                Else
                    U_SB_Show_Cash = True
                End If

                If c.Dr("Frm") = False Then
                    Me.FRM_Auto_LB.Enabled = False
                Else
                    Me.FRM_Auto_LB.Enabled = True
                End If

                If c.Dr("Sell_Under_Min_SP") = False Then
                    U_Sell_Under_Min_SP = False
                Else
                    U_Sell_Under_Min_SP = True
                End If

                If c.Dr("Sell_Under_Min_SP_2") = False Then
                    U_Sell_Under_Min_SP_2 = False
                Else
                    U_Sell_Under_Min_SP_2 = True
                End If


                If c.Dr("SB_Show_IM_COST") = False Then
                    U_SB_Show_IM_COST = False
                Else
                    U_SB_Show_IM_COST = True
                End If

                If c.Dr("IM") = False Then
                    Me.ITEMS_btn.Enabled = False
                Else
                    Me.ITEMS_btn.Enabled = True
                End If

                If c.Dr("Exp_Static") = False Then
                    Me.Exp_Static_LB.Enabled = False
                Else
                    Me.Exp_Static_LB.Enabled = True
                End If


                If c.Dr("Frm_M") = False Then
                    Me.FRM_M_LB.Enabled = False
                Else
                    Me.FRM_M_LB.Enabled = True
                End If



                U_Update_Receipt = c.Dr("Update_Receipt")
                U_Cancel_Receipt = c.Dr("Cancel_Receipt")

                U_ExpEdit = c.Dr("ExpEdit")

                U_AG_Skip_Max = c.Dr("is_Can_Skip_AG_Max_Debit")
                U_Show_Bill_Profet = c.Dr("SB_Show_Bill_Profet")
                U_Transfer_Table = c.Dr("Transfer_Table")
                U_Save_otherBill = c.Dr("Save_otherBill")


                U_Tr_Widraw = c.Dr("Tr_Widraw")
                U_Tr_Deposit = c.Dr("Tr_Deposit")
                U_Tr_Convert = c.Dr("Tr_Convert")
                U_Hide_Unrelated_Tr = c.Dr("Hide_Unrelated_Tr")



            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()

    End Sub

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Const CS_DROPSHADOW As Integer = &H20000
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property



    'Private Sub MainForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
    '    rs.ResizeAllControls_POS(Me)
    'End Sub

    Public Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModernLoader.ShowLoader()



        ' =====================================================================
        ' 🌟 هندلة الواجهة الذكية (إسقاط الشامخ) 🌟
        ' =====================================================================

        ' 1. البارات العلوية والسفلية 
        If ToolStrip IsNot Nothing Then ToolStrip.Tag = "HEADER"
        If ToolStrip IsNot Nothing Then ToolStrip.Tag = "HEADER"
        If StatusStrip1 IsNot Nothing Then StatusStrip1.Tag = "FOOTER"

        TitleBar_Panel.Tag = "HEADER"

        ' 💡 استخدام التاج السحري الجديد للشفافية
        Title_Label.Tag = "TITLE_TRANSPARENT"
        If Serv_Label IsNot Nothing Then Serv_Label.Tag = "SECONDARY_TRANSPARENT"
        If Serv_Label IsNot Nothing Then Serv_Label.Tag = "TITLE_TRANSPARENT"
        ' 2. أزرار الكنترول العلوية (تندمج مع الهيدر)
        ExitFormButton.Tag = "DELETE"
        ActiveLinkLa.Tag = "DELETE"
        ' 3. الخط البرتقالي التزييني 
        If Panel3 IsNot Nothing Then Panel3.Tag = "ACCENT"

        ' 4. القائمة الجانبية والكروت (الداشبورد)
        If TableLayoutPanel1 IsNot Nothing Then TableLayoutPanel1.Tag = "SIDEBAR"
        If Panel_Dash1 IsNot Nothing Then Panel_Dash1.Tag = "CARD"
        If Panel_Dash2 IsNot Nothing Then Panel_Dash2.Tag = "CARD"

        ' 5. الإحصائيات والأرقام داخل الكروت
        If Lb_Title1 IsNot Nothing Then Lb_Title1.Tag = "SECONDARY_TRANSPARENT"
        If Lb_Title2 IsNot Nothing Then Lb_Title2.Tag = "SECONDARY_TRANSPARENT"
        If Lb_Value1 IsNot Nothing Then Lb_Value1.Tag = "ACCENT_TRANSPARENT"
        If Lb_Value2 IsNot Nothing Then Lb_Value2.Tag = "ACCENT_TRANSPARENT"

        '' 6. الأزرار في المنتصف
        'Btn_QuickSales.Tag = "SAVE"
        'Btn_QuickPch.Tag = "PRIMARY"
        'Btn_QuickOffers.Tag = "GENERAL"
        'Table_Bill_Screen_btn.Tag = "GENERAL"
        Load_Dashboard_Buttons()

        ' =====================================================================
        ' 🚀 تطبيق الثيم
        ' =====================================================================
        Try
            ThemeManager.LoadDefaultSystemTheme()
            ThemeManager.ApplyThemeToForm(Me)
        Catch ex As Exception
        End Try


        Load_Form()

        If MY_Settings.Thread_Time = 0 Then
            ValidTimer.Stop()
        Else
            ValidTimer.Start()
        End If

        IM_Valid = "أصناف صلاحية"
        IM_Min = "أصناف ستنتهي قريبا"
        IM_Max = "أصناف وصلت أعلى كمية"
        AG_B = "ديون عملاء"
        Open_AGMV = "فواتير بها بضاعة وغير محفوظة"
        IM_SELL_LESS_THAN_COST = "مبيعات أقل من التكلفة"
        IM_Neg_QTY = "أصناف بكميات سالبة"
        IM_Notes_Valid = "قائمة صلاحية الأصناف"
        RCT_NOT_WITH_SB = "فواتير مبيعات متعارضه مع الإيصالات"
        RSV_IM = "أصناف مستأجرة"
        ORDER_IM = "بضاعــة قادمــة"
        ST_OTHER = "بضاعــة متوفرة في المخازن الأخرى"

        'Open_Bills = "فواتير مفتوحة"

        ALERT_DT.Columns.Add("FirstName", GetType(String))
        ALERT_DT.Columns.Add("LastName", GetType(String))

        For i = 0 To ALERT_DGV.Rows.Count - 1
            ALERT_DGV.Rows.Item(i).Visible = False
        Next
        Check_For_OpenPierod()

        Fill_ALL_ALERT()
        Set_Data_Alert()
        ModernLoader.CloseLoader()
        'If System_Start_Normal = False Then
        '    system_ALert.Text = " خطأ في تشغيل النظام ... الرجاء الخروج من النظام بالكامل وإعادة الدخول "
        '    system_ALert.Visible = True
        'End If

        'StartListening()

    End Sub

    Private Sub Build_Mini_Dashboard()
        ' إنشاء حاوية أفقية لضم المربعين
        Dim CardsContainer As New TableLayoutPanel()
        CardsContainer.ColumnCount = 2
        CardsContainer.RowCount = 1
        CardsContainer.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0!))
        CardsContainer.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0!))
        CardsContainer.Dock = DockStyle.Bottom
        CardsContainer.Height = 100 ' ارتفاع البطاقات
        CardsContainer.Padding = New Padding(5)
        CardsContainer.BackColor = Color.White

        ' --- البطاقة الأولى (مثال: مبيعات اليوم) ---
        Dim Card1 As New Panel()
        Card1.Dock = DockStyle.Fill
        Card1.Margin = New Padding(5)
        Card1.BackColor = Color.FromArgb(41, 128, 185) ' أزرق أنيق

        Dim Lb_Title1 As New Label()
        Lb_Title1.Text = "مبيعات اليوم"
        Lb_Title1.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Lb_Title1.ForeColor = Color.White
        Lb_Title1.Dock = DockStyle.Top
        Lb_Title1.TextAlign = ContentAlignment.MiddleCenter
        Lb_Title1.Height = 30

        Dim Lb_Value1 As New Label()
        Lb_Value1.Text = "0.00" ' هنا ستضع كود جلب المبيعات من الداتا بيز لاحقاً
        Lb_Value1.Font = New Font("Segoe UI", 20, FontStyle.Bold)
        Lb_Value1.ForeColor = Color.White
        Lb_Value1.Dock = DockStyle.Fill
        Lb_Value1.TextAlign = ContentAlignment.MiddleCenter

        Card1.Controls.Add(Lb_Value1)
        Card1.Controls.Add(Lb_Title1)

        ' --- البطاقة الثانية (مثال: عدد الفواتير) ---
        Dim Card2 As New Panel()
        Card2.Dock = DockStyle.Fill
        Card2.Margin = New Padding(5)
        Card2.BackColor = Color.FromArgb(39, 174, 96) ' أخضر أنيق

        Dim Lb_Title2 As New Label()
        Lb_Title2.Text = "عدد فواتير اليوم"
        Lb_Title2.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Lb_Title2.ForeColor = Color.White
        Lb_Title2.Dock = DockStyle.Top
        Lb_Title2.TextAlign = ContentAlignment.MiddleCenter
        Lb_Title2.Height = 30

        Dim Lb_Value2 As New Label()
        Lb_Value2.Text = "0" ' هنا ستضع كود جلب عدد الفواتير
        Lb_Value2.Font = New Font("Segoe UI", 20, FontStyle.Bold)
        Lb_Value2.ForeColor = Color.White
        Lb_Value2.Dock = DockStyle.Fill
        Lb_Value2.TextAlign = ContentAlignment.MiddleCenter

        Card2.Controls.Add(Lb_Value2)
        Card2.Controls.Add(Lb_Title2)

        ' إضافة البطاقات للحاوية
        CardsContainer.Controls.Add(Card1, 0, 0)
        CardsContainer.Controls.Add(Card2, 1, 0)

        ' إضافة الحاوية إلى اللوحة الوسطى (تحت الاختصارات وفوق شريط الأزرار)
        '     Me.Panel_Shortcuts_Main.Controls.Add(CardsContainer)
        CardsContainer.BringToFront() ' لضمان ظهورها فوق الأزرار السفلية
    End Sub


    Public Sub Load_Form()

        'Activation.isFor_Check_Sys_Active = True
        'Activation.ShowDialog()
        'Activation.isFor_Check_Sys_Active = False

        'If Activation.isActive = False Then
        '    Count_AG_BalanceRows()
        '    ActiveLinkLabel.Visible = True
        'Else
        '    ActiveLinkLabel.Visible = False
        'End If
        ''-----------------------------------------------------------------------------------------------



        'DB_Name_Tool.Text = MY_Settings.DataBase
        'Serv_Desc_lb.Text = MY_Settings.Server_Desc
        'U_Name_Tool.Text = USER_NAME

        'If MY_Settings.is_SubSys = True Then
        '    Button23.Visible = True
        '    is_Con = True
        'End If


        'Check_Sys_Featurs()

        'rs.FindAllControls(Me)
        'Me.WindowState = FormWindowState.Maximized

        'TimeTimer.Start()


        'If MY_Settings.is_SubSys = True Then
        '    Con_Timer.Start()
        '    ServeConnect_LB.Visible = True
        'Else
        '    Con_Timer.Stop()
        '    ServeConnect_LB.Visible = False
        'End If


        'LoadPrinters(KashierPrinterComboBox)
        'If Not String.IsNullOrWhiteSpace(Default_Printer_80) Then KashierPrinterComboBox.SelectedItem = Default_Printer_80

        'LoadPrinters(A4Printer_Cmb)
        'If Not String.IsNullOrWhiteSpace(Default_Printer_A4) Then A4Printer_Cmb.SelectedItem = Default_Printer_A4


        'LoadPrinters(Barcode_DefPrinter_Cm)
        'If Not String.IsNullOrWhiteSpace(Default_Barcode_Printer) Then Barcode_DefPrinter_Cm.SelectedItem = Default_Barcode_Printer


        'Setting_GroupBox.Enabled = is_Home_Mange_Printers
        Activation.isFor_Check_Sys_Active = True
        Dim hiddenHandle As IntPtr = Activation.Handle
        Activation.isFor_Check_Sys_Active = False

        If Activation.isActive = False Then
            Count_AG_BalanceRows()
            ActiveLinkLa.Visible = True
        Else
            ActiveLinkLa.Visible = False
        End If
        ' =====================================================================

        DB_Name_Tool.Text = MY_Settings.DataBase
        Serv_Label.Text = MY_Settings.Server_Desc
        U_Name_Tool.Text = USER_NAME

        If MY_Settings.is_SubSys = True Then
            Button23.Visible = True
            is_Con = True
        End If

        Check_Sys_Featurs()

        ' rs.FindAllControls(Me) ' تأكدنا من إيقافها لسرعة الصاروخ
        ' Me.WindowState = FormWindowState.Maximized ' تم نقلها لخصائص الديزاينر فليس لها داعٍ هنا

        TimeTimer.Start()

        If MY_Settings.is_SubSys = True Then
            Con_Timer.Start()
            ServeConnect_LB.Visible = True
        Else
            Con_Timer.Stop()
            ServeConnect_LB.Visible = False
        End If

        LoadPrinters(KashierPrinterComboBox)
        If Not String.IsNullOrWhiteSpace(Default_Printer_80) Then KashierPrinterComboBox.SelectedItem = Default_Printer_80

        LoadPrinters(A4Printer_Cmb)
        If Not String.IsNullOrWhiteSpace(Default_Printer_A4) Then A4Printer_Cmb.SelectedItem = Default_Printer_A4


        LoadPrinters(Barcode_DefPrinter_Cm)
        If Not String.IsNullOrWhiteSpace(Default_Barcode_Printer) Then Barcode_DefPrinter_Cm.SelectedItem = Default_Barcode_Printer


        Setting_GroupBox.Enabled = is_Home_Mange_Printers
    End Sub
    Public Sub Check_Sys_Featurs()
        IM_Frm_Btn.Visible = S_Frm
        ToolStripSeparator5_Frm.Visible = S_Frm
        أوامرمحزنToolStripMenuItem.Visible = S_In_Out_side
        NOTES_LB.Visible = S_Notes
        Table_Bill_Screen_btn.Visible = S_Tables
        Prd_LB.Visible = S_Pr

        If User_isAdmin = False Then
            Sys_Setting_btn.Enabled = False
            Agents_btn.Enabled = False
            Mange_EmpValid()
            'Open_MV_DV.Columns("USER_NAME_CL").Visible = False
        End If

        طلبياتخارجيةToolStripMenuItem.Visible = S_Out_Travel
        'فاتورةطلبيةToolStripMenuItem.Visible = S_Orders

        'If SScreenDefault = 0 Then
        '    عرضفواتيرزبونToolStripMenuItem.Visible = True
        'Else
        '    عرضفواتيرزبونToolStripMenuItem.Visible = False
        'End If
    End Sub





    '----------------------------------------------------------------------------------------------------------------------------------------------------------

    'Private Sub Tracking_TB_Print_Log()
    '    Try
    '        Dim c As New C
    '        Using c.Con
    '            c.Con.Open()
    '            Dim cmd As New SqlCommand("SELECT [T_ID],[ERROR_TITLE],[ERROR_LOG] FROM [dbo].[TB_AutoPrint_Log]  ", c.Con)
    '            Dim dependencyATT As New SqlDependency(cmd)
    '            AddHandler dependencyATT.OnChange, AddressOf OnDBChangeATT
    '            cmd.ExecuteReader()
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    'Private Sub OnDBChangeATT(ByVal sender As Object, ByVal e As SqlNotificationEventArgs)
    '    If e.Type = SqlNotificationType.Change Then Select_TB_Print_Log()
    'End Sub

    'Public Sub StartListening()
    '    Dim c As New C
    '    SqlDependency.Stop(MY_Settings.SqlConStr)
    '    SqlDependency.Start(MY_Settings.SqlConStr)
    '    Tracking_TB_Print_Log()
    'End Sub


    'Public Sub Select_TB_Print_Log()

    '    Dim c As New C
    '    Dim str As String = "Select TOP 1 [T_ID],[ERROR_TITLE],[ERROR_LOG] FROM [dbo].[TB_AutoPrint_Log] ORDER BY T_ID DESC "
    '    c.Com = New SqlClient.SqlCommand(str, c.Con)

    '    c.Con.Open()
    '    Try
    '        c.Dr = c.Com.ExecuteReader
    '        c.Dr.Read()

    '        If c.Dr.HasRows = True Then
    '            Dim notification3 As New NotificationForm(c.Dr("ERROR_TITLE"), c.Dr("ERROR_LOG"), "bottom")
    '            notification3.ShowNotification()
    '        End If


    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    '    c.Con.Close()

    'End Sub
    '----------------------------------------------------------------------------------------------------------------------------------------------------------


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        If S_Pr = True Then
            If isPr_Open = True Then
                Beep()
                Dim result As Integer = MessageBox.Show(" لم يتم إنهاء وردية العمل .. هل تريد إنهائها الأن ؟", "", MessageBoxButtons.YesNoCancel,
                                   MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If result = Windows.Forms.DialogResult.Yes Then
                    F_Periods = New Periods
                    F_Periods.ShowDialog()
                ElseIf result = Windows.Forms.DialogResult.No Then
                    Me.Close()
                    login.Show()
                Else
                    Exit Sub
                End If
            Else
                Me.Close()
                login.Show()
            End If

        Else
            Me.Close()
            login.Show()
        End If
    End Sub


    Private Sub Table_Bill_Screen_btn_Click(sender As Object, e As EventArgs) Handles Table_Bill_Screen_btn.Click
        Table_Bill_Screen.ShowDialog()
    End Sub
    Private Sub MainForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' rs.ResizeAllControls_POS(Me)  ' <-- أوقف هذا الكود ليصبح الفورم خفيفاً جداً
    End Sub
    Dim Notif_Tick = 0
    Private Sub TimeTimer_Tick(sender As Object, e As EventArgs) Handles TimeTimer.Tick
        ' شريط الحالة السفلي (قديم)
        Date_Tool.Text = Date.Now.Date
        Time_Tool.Text = Date.Now.ToString("h:mm:ss tt")

        ' البانر العلوي (الجديد)
        If ClockLabel IsNot Nothing Then
            ClockLabel.Text = Date.Now.ToString("HH:mm:ss")
            DateLabel.Text = Date.Now.ToString("dddd, dd MMMM yyyy", New System.Globalization.CultureInfo("ar-LY"))
        End If
    End Sub
    Private Sub Btn_QuickSales_Click(sender As Object, e As EventArgs) Handles Btn_QuickSales.Click
        ' افتح فورم فاتورة المبيعات
        SB_LB.PerformClick() ' ينفذ ضغطة زر المبيعات من القائمة
    End Sub
    Private Sub Btn_QuickPch_Click(sender As Object, e As EventArgs) Handles Btn_QuickPch.Click
        ' افتح فورم المشتريات
        Pch_LB.PerformClick()
    End Sub
    Public Sub Select_TB_Print_Log()

        Dim c As New C
        Dim str As String = "Select TOP 1 [ID],[ERROR_TITLE],[ERROR_LOG] FROM [dbo].[TB_AutoPrint_Notifications] WHERE DeviceID = '" & My.Computer.Name & "' AND isRead = 0 ORDER BY ID DESC "
        c.Com = New SqlClient.SqlCommand(str, c.Con)

        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            c.Dr.Read()

            If c.Dr.HasRows = True Then
                Dim notification3 As New NotificationForm(c.Dr("ERROR_TITLE"), c.Dr("ERROR_LOG"), "bottom")
                notification3.ShowNotification()
                query(" UPDATE TB_AutoPrint_Notifications SET isRead = 1 WHERE ID = " & c.Dr("ID"))
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()

    End Sub

    Private Sub Check_Server_Con()

        Try
            If My.Computer.Network.Ping(MY_Settings.SERVER_IP) Then
                'ServeConnect_LB.Visible = True


                'ServeConnect_LB.BackColor = Color.DarkGreen
                'ServeConnect_LB.Text = "متصل بالرئيسي"
                is_Con = True
            Else
                'ServeConnect_LB.Visible = True


                'ServeConnect_LB.BackColor = Color.DarkRed
                'ServeConnect_LB.Text = "غير متصل بالرئيسي"
                is_Con = False
                ' MessageBox.Show("يوجد مشكلة بالتوصيل بالخادم الرئيسي ... تأكد من توصيل كابل الشبكة وصحة كتابة IP الخادم الرئيسي", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            End If
        Catch ex As Exception
            is_Con = False
            'ServerConnecting_Timer.Enabled = False
            'ServeConnect_LB.Visible = True


            'ServeConnect_LB.BackColor = Color.Gray
            'ServeConnect_LB.Text = "غير متصل بالرئيسي"
            MessageBox.Show("يوجد مشكلة بالتوصيل بالخادم الرئيسي ... تأكد من توصيل كابل الشبكة وصحة كتابة IP الخادم الرئيسي", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
        End Try

    End Sub



    Private Sub Home_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        GetLastShow()
        login.Show()
    End Sub

    Private Sub Home_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'Beep()
        'If MessageBox.Show("إجراء عملية صيانة سريعة للمنظومة ؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
        '                    MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
        '    Me.Cursor = Cursors.AppStarting
        ' REDUCE_NETWORK_TRACKER()
        DB_Shrink()

        'Me.Cursor = Cursors.Default
        'End If




        If MY_Settings.is_SubSys = False Then
            Backup_Data_Auto()

            If String.IsNullOrWhiteSpace(BackupPath_OnExit) = False Then
                Beep()
                If MessageBox.Show("أخد نسخة إحتياطية لقاعدة البيانات ؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    Me.Cursor = Cursors.AppStarting
                    Backup_Data_OnClose()
                    Me.Cursor = Cursors.Default
                End If
            Else
                MsgBox("يجب عليك تحديد مسار النسخة الإحتياطية في هارد ديسك خارجي أو فلاش لضمان إستعادتها في حالة حدوث عطل فالهارد ديسك الخاص بالكمبيوتر و حماية بياناتك من الحذف", MsgBoxStyle.Exclamation, "تحذيــر مهم")
            End If

        End If


        Clear_PDF_Files()
    End Sub

    Private Sub Clear_PDF_Files()
        Dim files() As String
        files = Directory.GetFileSystemEntries(Application.StartupPath & "\Previews")
        For Each element As String In files
            If (Not Directory.Exists(element)) Then
                File.Delete(Path.Combine(Application.StartupPath & "\Previews", Path.GetFileName(element)))
            End If
        Next
    End Sub

    Private Sub DB_Shrink()
        Try

            Dim c As New C

            Dim s As String = "ALTER DATABASE " & MY_Settings.DataBase & " SET RECOVERY SIMPLE " &
            "DBCC SHRINKFILE (" & MY_Settings.DataBase & ", 10) " &
            "DBCC SHRINKDATABASE (" & MY_Settings.DataBase & ", 10)" &
            "ALTER DATABASE " & MY_Settings.DataBase & " SET RECOVERY BULK_LOGGED "
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            SQL_SP_EXEC(c.Com)
            'If SQL_SP_EXEC(c.Com) = True Then MsgBox("تـمت  الصيانة ", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        '----------------------------------------------------------------------------------------------------------
        'Dim c As New C
        'Try
        '    c.Con.Open()
        '    Dim sqlCon = New SqlConnection(My_Settings.SqlConStr)
        '    Using (sqlCon)

        '        Dim sqlComm As New SqlCommand()
        '        sqlComm.Connection = sqlCon
        '        sqlComm.CommandText = "[dbo].[DB_Shrink]"
        '        sqlComm.CommandType = CommandType.StoredProcedure
        '        sqlCon.Open()
        '        Try
        '            sqlComm.ExecuteNonQuery()
        '            MsgBox("تمت الصيانة", MsgBoxStyle.Information)
        '        Catch ex As Exception
        '            MsgBox(ex.Message)
        '        End Try
        '        sqlCon.Close()
        '    End Using
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

    End Sub

    Private Sub REDUCE_NETWORK_TRACKER()
        Dim c As New C
        Try
            c.Con.Open()
            Dim sqlCon = New SqlConnection(MY_Settings.SqlConStr)
            Using (sqlCon)

                Dim sqlComm As New SqlCommand()
                sqlComm.Connection = sqlCon
                sqlComm.CommandText = "[dbo].[REDUCE_NETWORK_TRACKER]"
                sqlComm.CommandType = CommandType.StoredProcedure
                sqlCon.Open()
                Try
                    sqlComm.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                sqlCon.Close()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub Backup_Data_OnClose()

        Dim c As New C
        Try
            c.Con.Open()

            Dim sqlCon = New SqlConnection(MY_Settings.SqlConStr)
            Using (sqlCon)

                Dim sqlComm As New SqlCommand()
                sqlComm.Connection = sqlCon
                sqlComm.CommandText = "[dbo].[backup_data]"
                sqlComm.CommandType = CommandType.StoredProcedure
                sqlComm.Parameters.AddWithValue("@bath", BackupPath_OnExit)
                sqlComm.Parameters.AddWithValue("@data_name", MY_Settings.DataBase)
                sqlComm.Parameters.AddWithValue("@Counter", "")
                sqlCon.Open()
                Try
                    sqlComm.ExecuteNonQuery()
                    MsgBox("تم أخد النسخة", MsgBoxStyle.Information)

                    Dim f As FileInfo = New FileInfo(BackupPath_OnExit)
                    Dim drive = Path.GetPathRoot(f.FullName)
                    If IsProjectOnExternalDisk(drive) = False Then
                        MsgBox("يجب أن يكون مسار النسخة الإحتياطية في هارد ديسك خارجي أو فلاش لضمان إستعادتها في حالة حدوث عطل فالهارد ديسك الخاص بالكمبيوتر و حماية بياناتك من الحذف", MsgBoxStyle.Exclamation, "تحذيــر مهم")
                    End If

                Catch ex As Exception
                    MsgBox("خطأ في أخد النسخة الإحتياطية .. تأكد من صحة مسار النسخة أو أن الخادم في نفس الجهاز", MsgBoxStyle.Critical, "مسار النسخة الإحتياطية 1")
                    MsgBox(ex.Message)
                End Try
                sqlCon.Close()
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    'Public Sub F_Load()

    '    'If isBackup = True Then
    '    '    BackupTimer.Enabled = True
    '    'Else
    '    '    BackupTimer.Enabled = False
    '    'End If

    '    'If isBackupPath2 = True Then
    '    '    Backup2Timer.Enabled = True
    '    'Else
    '    '    Backup2Timer.Enabled = False
    '    'End If

    '    'If S_Phone_For_Tables = True Then
    '    '    PhoneAppTimer.Enabled = isUseAsPhone_Crawler
    '    '    'Phone_PicBox.Visible = isUseAsPhone_Crawler
    '    'End If

    'End Sub


    Private Sub Count_AG_BalanceRows()
        Dim C As New C
        C.Str = "Select COUNT(T_ID) AS Num From Agents_Balance_MV"
        C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
        C.Con.Open()

        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows = True Then
            C.Dr.Read()
            If C.Dr("Num") >= MY_Settings.NumOfBillsTest Then
                MsgBox("انتهت صلاحية النسخة التجريبية  للنظام .... يرجى تفعيل النظام", MsgBoxStyle.Exclamation)
                ToolStrip.Enabled = False
            Else
                ToolStrip.Enabled = True
                Bill_Num_LB.Visible = True
                Bill_Num_LB.Text = " لديك " & (MY_Settings.NumOfBillsTest - C.Dr("Num")).ToString & " فاتورة من أصل " & MY_Settings.NumOfBillsTest
            End If
        End If
        C.Con.Close()
    End Sub

    Private Sub ActiveLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ActiveLinkLa.LinkClicked
        Activation.ShowDialog()
    End Sub

    Public Sub Backup_Data()

        Dim c As New C
        TimeConuter = 0

        Try
            If String.IsNullOrWhiteSpace(BackupPath) Then
                MsgBox(" لم يتم تحديـــد مســار النسخة الإحتياطية لقاعدة بيانات النظام", MsgBoxStyle.Exclamation, " النسخة الإحتياطية ")
                Exit Sub
            End If

            Dim counter = My.Computer.FileSystem.GetFiles(BackupPath)

            If counter.Count >= 29 Then

                Dim files() As String
                files = Directory.GetFileSystemEntries(BackupPath)
                For Each element As String In files
                    If (Not Directory.Exists(element)) Then
                        File.Delete(Path.Combine(BackupPath, Path.GetFileName(element)))
                    End If
                Next

                counter = My.Computer.FileSystem.GetFiles(BackupPath)
            End If

            Dim NumOfFile As Integer = counter.Count + 1

            c.Con.Open()

            Dim sqlCon = New SqlConnection(MY_Settings.SqlConStr)
            Using (sqlCon)

                Dim sqlComm As New SqlCommand()
                sqlComm.Connection = sqlCon
                sqlComm.CommandText = "[dbo].[backup_data]"
                sqlComm.CommandType = CommandType.StoredProcedure
                sqlComm.Parameters.AddWithValue("@bath", BackupPath.ToString)
                sqlComm.Parameters.AddWithValue("@data_name", MY_Settings.DataBase)
                sqlComm.Parameters.AddWithValue("@Counter", NumOfFile)
                sqlCon.Open()

                Try
                    sqlComm.ExecuteNonQuery()
                    sqlCon.Close()
                Catch ex As Exception
                    MsgBox("خطأ في أخد النسخة الإحتياطية .. تأكد من صحة مسار النسخة أو أن الخادم في نفس الجهاز", MsgBoxStyle.Critical, "النسخة الإحتياطية")
                    MsgBox(ex.Message)
                End Try
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub Backup_DataPath2()
        ' On Error Resume Next
        Dim c As New C
        TimeConuter2 = 0

        Try
            If String.IsNullOrWhiteSpace(BackupPath_2) Then
                MsgBox(" لم يتم تحديـــد مســار النسخة الإحتياطية الثانية لقاعدة بيانات النظام", MsgBoxStyle.Exclamation, "مسار النسخة الإحتياطية الثانية")
                Exit Sub
            End If

            Dim counter = My.Computer.FileSystem.GetFiles(BackupPath_2)

            If counter.Count >= 29 Then
                Dim files() As String
                files = Directory.GetFileSystemEntries(BackupPath_2)
                For Each element As String In files
                    If (Not Directory.Exists(element)) Then
                        File.Delete(Path.Combine(BackupPath_2, Path.GetFileName(element)))
                    End If
                Next

                counter = My.Computer.FileSystem.GetFiles(BackupPath_2)
            End If

            Dim NumOfFile As Integer = counter.Count + 1

            c.Con.Open()

            Dim sqlCon = New SqlConnection(MY_Settings.SqlConStr)
            Using (sqlCon)

                Dim sqlComm As New SqlCommand()
                sqlComm.Connection = sqlCon
                sqlComm.CommandText = "[dbo].[backup_data]"
                sqlComm.CommandType = CommandType.StoredProcedure
                sqlComm.Parameters.AddWithValue("@bath", BackupPath_2.ToString)
                sqlComm.Parameters.AddWithValue("@data_name", MY_Settings.DataBase)
                sqlComm.Parameters.AddWithValue("@Counter", NumOfFile)
                sqlCon.Open()

                Try
                    sqlComm.ExecuteNonQuery()
                    sqlCon.Close()
                Catch ex As Exception
                    MsgBox("خطأ في أخد النسخة الإحتياطية .. تأكد من صحة مسار النسخة أو أن الخادم في نفس الجهاز", MsgBoxStyle.Critical, "مسار النسخة الإحتياطية الثانية")
                    MsgBox(ex.Message)
                End Try
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Public Sub Backup_Data_Auto()

        Dim c As New C
        Try

            Dim counter = My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Backup")
            'If counter.Count >= 50 Then

            '  Dim files() As String
            ' files = Directory.GetFileSystemEntries(Application.StartupPath & "\Backup")
            'For Each element As String In files
            '    If (Not Directory.Exists(element)) Then
            '        File.Delete(Path.Combine(Application.StartupPath & "\Backup", Path.GetFileName(element)))
            '    End If
            'Next

            Dim directory As New IO.DirectoryInfo(Application.StartupPath & "\Backup")
            For Each file As IO.FileInfo In directory.GetFiles
                If (Now - file.CreationTime).Days > 7 Then file.Delete()
            Next


            counter = My.Computer.FileSystem.GetFiles(Application.StartupPath & "\Backup")
            'End If

            Dim NumOfFile As Integer = counter.Count + 1

            c.Con.Open()

            Dim sqlCon = New SqlConnection(MY_Settings.SqlConStr)
            Using (sqlCon)

                Dim sqlComm As New SqlCommand()
                sqlComm.Connection = sqlCon
                sqlComm.CommandText = "[dbo].[backup_data]"
                sqlComm.CommandType = CommandType.StoredProcedure

                '  sqlComm.Parameters.AddWithValue("@bath", "C:\Users\SERAJ\Desktop\CPOS (تزامن) 11-03\CPOS(1.2)\resturant\bin\Debug\Backup")
                sqlComm.Parameters.AddWithValue("@bath", Application.StartupPath & "\Backup")
                sqlComm.Parameters.AddWithValue("@data_name", MY_Settings.DataBase)
                sqlComm.Parameters.AddWithValue("@Counter", NumOfFile)
                sqlCon.Open()

                Try
                    sqlComm.ExecuteNonQuery()
                    sqlCon.Close()
                Catch ex As Exception
                    MsgBox("خطأ في أخد النسخة الإحتياطية .. تأكد من صحة مسار النسخة أو أن الخادم في نفس الجهاز", MsgBoxStyle.Critical, "النسخة الإحتياطية")
                    MsgBox(ex.Message)
                End Try
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    'Private Sub BackupTimer_Tick(sender As Object, e As EventArgs)
    '    TimeConuter = TimeConuter + 1
    '    If TimeConuter = 600 Then Backup_Data()
    'End Sub

    'Private Sub Backup2Timer_Tick(sender As Object, e As EventArgs)
    '    TimeConuter2 = TimeConuter2 + 1
    '    If TimeConuter2 = 3600 Then Backup_DataPath2()
    'End Sub



    Public Sub IM_FirstTime()

        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\IM_FirstTime.rpt")
        pp.LoadTables()
        '-------------------------
        pp.rp.SetParameterValue(0, USER_NAME)
        pp.rp.SetParameterValue(1, MY_Settings.Server_Desc)


        print.CrystalReportViewer1.ReportSource = pp.rp
        print.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_A4
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()

    End Sub

    Private Sub فتحدرجالنقوذToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles فتحدرجالنقوذToolStripMenuItem.Click
        Open_Cash_Drawer()
    End Sub

    Private Sub لوحةمفاتيحToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles لوحةمفاتيحToolStripMenuItem.Click
        Dim O As New OSK_Class
        O.OSK_OPEN()
    End Sub

    Private Sub ToolStripStatusLabel5_Click(sender As Object, e As EventArgs) Handles ToolStripStatusLabel5.Click
        ' If MY_Settings.App_Suuply = "CLASS" Then
        About_CLASS.ShowDialog()
        'Else
        '    About_RESAL.ShowDialog()
        'End If

    End Sub

    Private Sub SB_LB_Click(sender As Object, e As EventArgs) Handles SB_LB.Click
        POS_ENTER_AS_ORDER = False
        Enter_POS()
    End Sub


    Private Sub Enter_POS()
        If S_Pr = True Then
            Select Case SScreenDefault
                Case 0
                    Me.Cursor = Cursors.AppStarting
                    If isPr_Open = True Then

                        SB_is_Fast = False
                        F_Sales = New Sales
                        F_Sales.Show()

                    Else
                        MsgBox("يجب عليك فتــح وردية عمل أولا", MsgBoxStyle.Exclamation)
                    End If
                    Me.Cursor = Cursors.Default
                Case 1
                    'Dim c As New C
                    'Dim s As String = "SELECT COUNT(GM_ID) AS 'COUNTER' FROM General_menu WHERE Printer_ID IS NULL"
                    'Dim com As New SqlClient.SqlCommand(s, c.Con)
                    'Dim dr As SqlClient.SqlDataReader
                    'c.Con.Open()
                    'Try
                    '    dr = com.ExecuteReader
                    '    dr.Read()
                    '    If dr.HasRows = True Then
                    '        If dr("COUNTER") > 0 Then
                    '            MsgBox(" توجد  " + dr("COUNTER").ToString + "  مجمــوعة رئيسية لم يتم تخصيص طابعــة فرعية لها ... قم بتأكد منها اولا  " _
                    '                       , MsgBoxStyle.Critical, "طابعات فرعية")
                    '            Exit Sub
                    '        End If
                    '    End If
                    'Catch ex As Exception
                    '    MsgBox(ex.Message)
                    'End Try
                    'c.Con.Close()

                    Me.Cursor = Cursors.AppStarting
                    If isPr_Open = True Then
                        SB_is_Fast = False
                        F_POS = New POS
                        F_POS.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                        Home_Panel = "POS"
                        F_POS.Show()
                    Else
                        MsgBox("يجب عليك فتــح وردية عمل أولا", MsgBoxStyle.Exclamation)
                    End If
                    Me.Cursor = Cursors.Default

                Case 2
                    If isPr_Open = True Then
                        SB_is_Fast = True
                        Sales_Fast.Show()
                    Else
                        MsgBox("يجب عليك فتــح وردية عمل أولا", MsgBoxStyle.Exclamation)
                    End If

            End Select

        Else

            Select Case SScreenDefault
                Case 0
                    SB_is_Fast = False
                    F_Sales = New Sales
                    F_Sales.Show()
                Case 1
                    Check_Printers()
                    SB_is_Fast = False
                    Me.Cursor = Cursors.AppStarting
                    F_POS = New POS
                    F_POS.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                    Home_Panel = "POS"
                    F_POS.Show()
                    Me.Cursor = Cursors.Default

                Case 2
                    SB_is_Fast = True
                    Sales_Fast.Show()

            End Select

        End If
    End Sub

    Private Sub Check_Printers()
        'Dim c As New C
        'Dim s As String = "SELECT COUNT(GM_ID) AS 'COUNTER' FROM General_menu WHERE Printer_ID IS NULL"
        'Dim com As New SqlClient.SqlCommand(s, c.Con)
        'Dim dr As SqlClient.SqlDataReader
        'c.Con.Open()
        'Try
        '    dr = com.ExecuteReader
        '    dr.Read()
        '    If dr.HasRows = True Then
        '        If dr("COUNTER") > 0 Then
        '            MsgBox(" توجد  " + dr("COUNTER").ToString + "  مجمــوعة رئيسية لم يتم تخصيص طابعــة فرعية لها ... قم بتأكد منها اولا  " _
        '                       , MsgBoxStyle.Critical, "طابعات فرعية")
        '            Exit Sub
        '        End If
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        'c.Con.Close()
    End Sub

    'Private Function Check_IF_isOrder(T_ID As Integer) As Boolean
    '    Dim c As New C
    '    Dim s As String = "SELECT S_Bills_Type FROM Agents_Balance_MV WHERE T_ID = '" & T_ID & "'"
    '    Dim com As New SqlClient.SqlCommand(s, c.Con)
    '    Dim dr As SqlClient.SqlDataReader
    '    c.Con.Open()
    '    Try
    '        dr = com.ExecuteReader
    '        dr.Read()
    '        If dr.HasRows = True Then If dr("S_Bills_Type") = 3 Then Return True
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    '    Return False
    'End Function


    Private Sub Prd_LB_Click(sender As Object, e As EventArgs) Handles Prd_LB.Click
        F_Periods = New Periods
        F_Periods.ShowDialog()
    End Sub

    Private Sub LinkLabel2_Click(sender As Object, e As EventArgs) Handles LinkLabel2.Click
        F_ViewBill = New ViewBill
        F_ViewBill.Show()
    End Sub


    Private Sub CashLB_Click(sender As Object, e As EventArgs) Handles CashLB.Click
        If Application.OpenForms().OfType(Of Sales).Any Or Application.OpenForms().OfType(Of Sales_Fast).Any Or Application.OpenForms().OfType(Of POS).Any Then
            MsgBox("يجب عليك إغلاق كل شاشات البيع المفتوحة أولا", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If S_Pr = True Then
            If isPr_Open = True Then
                FormType = 0
                AG_Type = 3
                F_Receipt = New Receipt
                Receipt_Tran_ID = 0
                F_Receipt.Show()
            Else
                MsgBox("يجب عليك فتــح وردية عمل أولا", MsgBoxStyle.Exclamation)
            End If
        Else
            FormType = 0
            AG_Type = 3
            F_Receipt = New Receipt
            Receipt_Tran_ID = 0
            F_Receipt.Show()
        End If
    End Sub

    Private Sub SBRtn_LB_Click(sender As Object, e As EventArgs) Handles SBRtn_LB.Click
        Me.Cursor = Cursors.AppStarting
        FormType = 5
        Returns.Show()
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub Pch_LB_Click(sender As Object, e As EventArgs) Handles Pch_LB.Click
        Me.Cursor = Cursors.AppStarting
        F_Pch = New Pch
        FormType = 2
        F_Pch.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Barcode_Btn_Click(sender As Object, e As EventArgs) Handles Barcode_Btn.Click
        printbarcode.Show()
    End Sub

    Private Sub Deliver_LB_Click(sender As Object, e As EventArgs) Handles Deliver_LB.Click
        If Application.OpenForms().OfType(Of Pch).Any Then
            MsgBox("يجب عليك إغلاق كل شاشات المشتريات المفتوحة أولا", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        FormType = 0
        AG_Type = 4
        F_Receipt = New Receipt
        Receipt_Tran_ID = 0
        F_Receipt.Show()
    End Sub

    Private Sub Pch_Rtn_LB_Click(sender As Object, e As EventArgs) Handles Pch_Rtn_LB.Click
        Me.Cursor = Cursors.AppStarting
        FormType = 6
        Returns.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ST_Explore_LB_Click(sender As Object, e As EventArgs) Handles ST_Explore_LB.Click
        Me.Cursor = Cursors.AppStarting
        F_STORES_Explorer = New STORES_Explorer
        F_STORES_Explorer.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Invoice_LB_Click(sender As Object, e As EventArgs) Handles Invoice_LB.Click
        F_Invoice = New Invoice
        F_Invoice.Show()
    End Sub

    Private Sub IMEX_LB_Click(sender As Object, e As EventArgs) Handles IMEX_LB.Click
        F_IM_Execute = New IM_Execute
        F_IM_Execute.Show()
    End Sub

    Private Sub STTRANC_LB_Click(sender As Object, e As EventArgs) Handles STTRANC_LB.Click
        F_Stores_ImmediateOrder = New Stores_ImmediateOrder
        F_Stores_ImmediateOrder.Show()
    End Sub

    Private Sub Prepare_Invoice_LB_Click(sender As Object, e As EventArgs) Handles Prepare_Invoice_LB.Click
        SELECT_GM_FIRST_TIME.ShowDialog()
    End Sub

    Private Sub GM_LB_Click(sender As Object, e As EventArgs) Handles GM_LB.Click
        F_GeneralMenu = New GeneralMenu
        F_GeneralMenu.Show()
    End Sub

    Private Sub IM_LB_Click(sender As Object, e As EventArgs) Handles IM_LB.Click
        F_ItemsMenu = New ItemsMenu
        F_ItemsMenu.Show()
    End Sub

    Private Sub UNITS_LB_Click(sender As Object, e As EventArgs) Handles UNITS_LB.Click
        F_Units = New Units
        F_Units.ShowDialog()
    End Sub

    Private Sub الملاحظاتToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NOTES_LB.Click
        F_Notes = New Notes
        F_Notes.ShowDialog()
    End Sub


    Private Sub Exp_LB_Click(sender As Object, e As EventArgs) Handles Exp_LB.Click
        F_EXP_Details = New EXP_Details
        F_EXP_Details.Show()
    End Sub

    Private Sub Exp_Static_LBToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles Exp_Static_LB.Click
        F_Exp_Static = New Exp_Static
        F_Exp_Static.Show()
    End Sub

    Private Sub ALL_Report_LB_Click(sender As Object, e As EventArgs) Handles ALL_Report_LB.Click
        Me.Cursor = Cursors.AppStarting
        'F_SalesReport = New Reports
        HOME.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub General_Report_LB_Click(sender As Object, e As EventArgs) Handles General_Report_LB.Click
        General_Report.Show()
    End Sub

    Private Sub FRM_Auto_LB_Click(sender As Object, e As EventArgs) Handles FRM_Auto_LB.Click
        F_Format_Items_Auto = New Format_Items_Auto
        F_Format_Items_Auto.Show()
    End Sub

    Private Sub FRM_M_LB_Click(sender As Object, e As EventArgs) Handles FRM_M_LB.Click
        F_Format_Items_Manual = New Format_Items_Manual
        F_Format_Items_Manual.Show()
    End Sub


    Private Sub Balances_btn_Click_1(sender As Object, e As EventArgs) Handles Balances_btn.Click
        'Me.Cursor = Cursors.AppStarting
        'F_Balances = New Balances
        'F_Balances.Show()
        'Me.Cursor = Cursors.Default
    End Sub


    Private Sub Sys_Setting_btn_Click_1(sender As Object, e As EventArgs) Handles Sys_Setting_btn.Click
        F_SysOptions = New SysOptions
        F_SysOptions.ShowDialog()
    End Sub

    Private Sub Serv_Desc_lb_KeyDown(sender As Object, e As KeyEventArgs) Handles Serv_Label.KeyDown
        If e.KeyData = Keys.Return Then
            MY_Settings.Server_Desc = Serv_Label.Text
            Save_AppSetting()
            '     Serv_Desc_lb.ReadOnly = True
        End If
    End Sub

    Private Sub Serv_Desc_lb_DoubleClick(sender As Object, e As EventArgs) Handles Serv_Label.DoubleClick
        '   If User_isAdmin = True Then Serv_Desc_lb.ReadOnly = False
    End Sub

    Private Sub ToolStripSplitButton1_ButtonClick(sender As Object, e As EventArgs) Handles Button23.ButtonClick
        is_Con = True
    End Sub

    Private Sub لوائحالأسعـــارToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles لوائحالأسعـــارToolStripMenuItem.Click
        Items_Prices.Show()
    End Sub

    Private Sub بطاقةالعملاءToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles بطاقةالعملاءToolStripMenuItem.Click
        F_Agent = New Agent
        F_Agent.ShowDialog()
    End Sub

    'Private Sub Open_MV_DV_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Open_MV_DV.MouseDoubleClick

    '    Me.Cursor = Cursors.AppStarting
    '    If Open_MV_DV.Rows.Count > 0 Then
    '        'If Open_MV_DV.CurrentRow.Cells("AG_MV_Type_ID_CL").Value <> 6 Then
    '        Select Case Open_MV_DV.CurrentRow.Cells(4).Value
    '            Case 3, 4
    '                T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
    '                isShowing_Trans = True
    '                F_Receipt = New Receipt
    '                F_Receipt.ShowDialog()
    '                isShowing_Trans = False
    '                FormType = 0

    '            Case 5, 6
    '                Add_Prev_Balance.is_Select = True
    '                Add_Prev_Balance.ShowDialog()

    '            Case 1
    '                FormType = 1

    '                Select Case SScreenDefault
    '                    Case 0
    '                        SB_is_Fast = False
    '                        T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
    '                        isShowing_Trans = True
    '                        F_Sales = New Sales
    '                        F_Sales.ShowDialog()
    '                        isShowing_Trans = False
    '                        FormType = 0
    '                    Case 1
    '                        SB_is_Fast = False
    '                        If Check_IF_isOrder(Open_MV_DV.CurrentRow.Cells(0).Value) = True Then POS_ENTER_AS_ORDER = True
    '                        F_POS = New POS
    '                        T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
    '                        isShowing_Trans = True
    '                        F_POS.ShowDialog()
    '                        isShowing_Trans = False
    '                    Case 2
    '                        SB_is_Fast = True
    '                        T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
    '                        isShowing_Trans = True
    '                        Sales_Fast.ShowDialog()
    '                        isShowing_Trans = False
    '                End Select

    '            Case 7
    '                FormType = 2
    '                T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
    '                isShowing_Trans = True
    '                F_Pch = New Pch
    '                F_Pch.ShowDialog()
    '                isShowing_Trans = False
    '                FormType = 0

    '            Case 8
    '                FormType = 3
    '                T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
    '                isShowing_Trans = True
    '                F_IM_Execute = New IM_Execute
    '                F_IM_Execute.ShowDialog()
    '                isShowing_Trans = False
    '                FormType = 0

    '            Case 9
    '                FormType = 4
    '                T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
    '                isShowing_Trans = True
    '                F_Invoice = New Invoice
    '                F_Invoice.ShowDialog()
    '                isShowing_Trans = False
    '                FormType = 0
    '            Case 10
    '                FormType = 5
    '                T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
    '                isShowing_Trans = True
    '                Returns.ShowDialog()
    '                isShowing_Trans = False
    '                FormType = 0

    '            Case 11
    '                FormType = 6
    '                T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
    '                isShowing_Trans = True
    '                Returns.ShowDialog()
    '                isShowing_Trans = False
    '                FormType = 0

    '            Case 12
    '                FormType = 7
    '                T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
    '                isShowing_Trans = True
    '                F_Stores_ImmediateOrder = New Stores_ImmediateOrder
    '                F_Stores_ImmediateOrder.ShowDialog()
    '                isShowing_Trans = False
    '                FormType = 0

    '            Case 2
    '                FormType = 7
    '                T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
    '                isShowing_Trans = True
    '                F_EXP_Details = New EXP_Details
    '                F_EXP_Details.ShowDialog()
    '                isShowing_Trans = False
    '                FormType = 0

    '            Case 13
    '                FormType = 9
    '                T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
    '                isShowing_Trans = True
    '                F_Format_Items_Auto = New Format_Items_Auto
    '                F_Format_Items_Auto.ShowDialog()
    '                isShowing_Trans = False
    '                FormType = 0

    '            Case 16
    '                FormType = 10
    '                T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
    '                isShowing_Trans = True
    '                F_ViewBill = New ViewBill
    '                F_ViewBill.ShowDialog()
    '                isShowing_Trans = False
    '                FormType = 0

    '            Case 17
    '                FormType = 11
    '                T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
    '                isShowing_Trans = True
    '                F_Inside_Sales = New Inside_Sales
    '                F_Inside_Sales.ShowDialog()
    '                isShowing_Trans = False
    '                FormType = 0


    '            Case 18
    '                FormType = 9
    '                T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
    '                isShowing_Trans = True
    '                F_Format_Items_Manual = New Format_Items_Manual
    '                F_Format_Items_Manual.ShowDialog()
    '                isShowing_Trans = False
    '                FormType = 0

    '            Case 35
    '                FormType = 11
    '                T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
    '                isShowing_Trans = True
    '                F_Outside_Sales = New Outside_Sales
    '                F_Outside_Sales.ShowDialog()
    '                isShowing_Trans = False
    '                FormType = 0



    '        End Select
    '        ' End If
    '    End If
    '    Me.Cursor = Cursors.Default
    'End Sub



    Private Sub تغييربنسبةمئويةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تغييربنسبةمئويةToolStripMenuItem.Click
        IM_Change_Price_ByPercent.ShowDialog()
    End Sub

    Private Sub تغيربقيمةمعينةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تغيربقيمةمعينةToolStripMenuItem.Click
        IM_Change_PriceByVal.ShowDialog()
    End Sub


    Private Sub عرضفواتيرزبونToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضفواتيرزبونToolStripMenuItem.Click
        SearchAgentBill.ShowDialog()
    End Sub


    'Dim IM_Dt As New DataTable
    'Dim Bill_Date_Str, SB_ID, BillNumTxt, BillTypeCmb, Bill_Note, Cr_Phone, TB_Name_Lb, Barcode As String
    'Public Sub Fetch_Smart_Phone_Awiting()

    '    Dim C As New C
    '    Dim T_ID As Integer = 0
    '    'Dim T_NAME As String = ""
    '    With C.Com
    '        .Connection = C.Con
    '        .CommandText = "[Perform_Add_To_AG_MV]"
    '        .Parameters.AddWithValue("@Bill_T_ID", 0)
    '        .Parameters.Add("@T_NAME", SqlDbType.NVarChar, 200, "")
    '        .Parameters("@Bill_T_ID").Direction = ParameterDirection.Output
    '        '.Parameters("@T_NAME").Direction = ParameterDirection.Output
    '        .CommandType = CommandType.StoredProcedure
    '        If SQL_SP_EXEC(C.Com) = True Then
    '            If Not String.IsNullOrWhiteSpace(.Parameters("@Bill_T_ID").Value().ToString) Then
    '                T_ID = .Parameters("@Bill_T_ID").Value()
    '                'T_NAME = .Parameters("@T_NAME").Value()
    '                Fill_Bill_Info(T_ID)
    '                SB_Contents_SELECT_Bill(T_ID)
    '            End If
    '        End If

    '    End With

    'End Sub


    'Public Sub SB_Contents_SELECT_Bill(T_ID As Integer)
    '    Dim C As New C
    '    IM_Dt.Clear()
    '    With C.Com
    '        .Connection = C.Con
    '        .CommandText = "SB_Contents_SELECT_Bill"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@SB_T_ID", T_ID)
    '    End With
    '    C.Da = New SqlClient.SqlDataAdapter(C.Com)
    '    C.Da.Fill(IM_Dt)
    'End Sub

    'Public Sub Fill_Bill_Info(T_ID As Integer)


    '    'AG_Desc_lb.Text = " الزبون : "
    '    TB_Name_Lb = "الطاولة : "
    '    Dim C As New C

    '    With C.Com
    '        .Connection = C.Con
    '        .CommandText = "SB_Info_V_SELECT_Bill"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@T_ID", T_ID)
    '    End With
    '    C.Con.Open()
    '    C.Dr = C.Com.ExecuteReader
    '    If C.Dr.HasRows Then
    '        C.Dr.Read()


    '        BillNumTxt = C.Dr("S_Bill_Pr_ID")

    '        If C.Dr("TB_ID") = 0 Then
    '            TB_Name_Lb = ""
    '        Else
    '            TB_Name_Lb = TB_Name_Lb + C.Dr("T_Name").ToString
    '        End If

    '        Barcode = C.Dr("Barcode")
    '        'TB_isOrderd = C.Dr("TB_isOrderd")

    '        Bill_Date_Str = C.Dr("date")
    '        Cr_Phone = C.Dr("Cr_Phone")

    '        BillTypeCmb = C.Dr("B_Name")


    '        SB_ID = C.Dr("SB_ID")


    '        USER_NAME = C.Dr("U_Name")




    '        If PrintTBKsh = True Then CashPrint_SB_Ksh(T_ID, TB_Name_Lb)
    '        If CP_Bill_Screen = True Then Send_To_Bill_Screen(T_ID)
    '    End If
    '    C.Con.Close()
    'End Sub


    'Public Sub CashPrint_SB_Ksh(T_ID As Integer, T_NAME As String)
    '    If AGBillPage_ID <> 7 Then


    '        Dim C As New C
    '        Dim Prt_Path As String
    '    Dim Prt_ID As Integer
    '    C.Con.Open()
    '    With C.Com
    '        .Connection = C.Con
    '        .CommandText = "SB_C_By_Printers_SELECT"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@B_T_ID", T_ID)
    '    End With
    '    C.Dr = C.Com.ExecuteReader
    '    If C.Dr.HasRows Then
    '        While C.Dr.Read()
    '            Prt_ID = C.Dr("Prt_ID")
    '            Prt_Path = C.Dr("Prt_Path")
    '            Dim pp As New ReportConnection
    '            pp.rp.Load(Application.StartupPath & "\reports\SB_Ksh.rpt")
    '            pp.LoadTables()
    '            With pp

    '                .rp.SetParameterValue(0, " الطاولة : " + T_NAME)
    '                .rp.SetParameterValue(1, T_ID)
    '                .rp.SetParameterValue(2, T_ID)
    '                .rp.SetParameterValue(3, Prt_ID)
    '            End With

    '            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", C.Dr("Prt_Path")))
    '            pp.rp.PrintOptions.PrinterName = C.Dr("Prt_Path")
    '            pp.rp.PrintToPrinter(1, False, 0, 0)
    '            pp.rp.Dispose()

    '            End While
    '    End If
    '    C.Con.Close()

    '    Else
    '        FAST_REPORT_SB_Ksh(T_ID)
    '    End If
    'End Sub


    'Private Sub FAST_REPORT_SB_Ksh(T_ID As Integer)


    '    Dim C, C2 As New C
    '    Dim Tmp_Dt As New DataTable
    '    Dim ItemRow As DataRow

    '    With Tmp_Dt.Columns
    '        .Add("IM_Name", Type.GetType("System.String"))
    '        .Add("qty", Type.GetType("System.String"))
    '    End With

    '    Data_Load(Tmp_Dt, Bill_Date_Str, SB_ID, BillNumTxt, BillTypeCmb, Bill_Note, Cr_Phone, 0, TB_Name_Lb, Barcode)

    '    '---------------------------------------------------------------------------------

    '    C.Str = " Select DISTINCT Printer_ID,Printer_Path  From GM_PRINTER_IM_V WHERE B_T_ID = " & T_ID
    '    C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
    '    C.Con.Open()

    '    C.Dr = C.Com.ExecuteReader
    '    If C.Dr.HasRows = True Then
    '        While C.Dr.Read()
    '            Tmp_Dt.Clear()

    '            '-------------------------------------------------------------------
    '            C2.Str = " Select IM_Name,QTY  From GM_PRINTER_IM_V WHERE B_T_ID = " & T_ID & " AND Printer_ID = " & C.Dr("Printer_ID")
    '            C2.Com = New SqlClient.SqlCommand(C2.Str, C2.Con)
    '            C2.Con.Open()
    '            C2.Dr = C2.Com.ExecuteReader
    '            If C2.Dr.HasRows = True Then
    '                While C2.Dr.Read()

    '                    ItemRow = Tmp_Dt.NewRow()
    '                    ItemRow("IM_Name") = C2.Dr("IM_Name")
    '                    ItemRow("qty") = C2.Dr("QTY")
    '                    Tmp_Dt.Rows.Add(ItemRow)

    '                End While
    '            End If
    '            C2.Con.Close()

    '            Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", C.Dr("Printer_Path")))
    '            PRINT_REPORT_KSH()
    '        End While

    '    End If
    '    C.Con.Close()
    '    ' ----------------------------------------------------------------------------------

    'End Sub

    Public Sub Send_To_Bill_Screen(T_ID As Integer)
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "Send_To_Bill_Screen"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Bill_T_ID", T_ID)
            .Parameters.AddWithValue("@CP_Name", My.Computer.Name)
        End With
        SQL_SP_EXEC(c.Com)
    End Sub

    Private Sub إدارةالأصنافToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إدارةالأصنافToolStripMenuItem.Click
        IM_Fast_Mang.ShowDialog()
    End Sub

    ''-----------------------------------------------------------------------------------------

    'Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles PhoneAppBWorker.DoWork
    '    PhoneAppTimer.Enabled = False
    '    Fetch_Smart_Phone_Awiting()
    '    'PhoneAppTimer.Enabled = True
    'End Sub

    'Dim Second = 0
    'Private Sub PhoneAppTimer_Tick(sender As Object, e As EventArgs) Handles PhoneAppTimer.Tick
    '    Second += 1
    '    If Second = 5 Then
    '        Try
    '            If Not PhoneAppBWorker.IsBusy Then
    '                PhoneAppBWorker.RunWorkerAsync()
    '            End If
    '            Second = 0
    '        Catch ex As Exception
    '            Second = 0
    '        End Try
    '    End If
    'End Sub


    'Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles PhoneAppBWorker.RunWorkerCompleted
    '    PhoneAppTimer.Enabled = True
    'End Sub


    'Public Sub Load_ST_Tran_To_My_STORE(T_ID As Integer)
    '    Dim c As New C
    '    With c.Com
    '        .Connection = c.Con
    '        .CommandText = "[ST_Trans_finish_Transfer]"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@T_ID", T_ID)
    '    End With
    '    If SQL_SP_EXEC(c.Com) = True Then
    '        If S_Auto_Recive_ST_Tran = False Then MsgBox("تم تحويل البضاعة للمخزن الخاص بك", MsgBoxStyle.Information, "")

    '    End If
    'End Sub

    'Private Sub Awaiting_ST_Trans_GV_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles SyncHist.MouseDoubleClick
    '    If SyncHist.RowCount > 0 Then
    '        If MessageBox.Show(" تحويل البضاعة إلى مخزن المحل ؟", "", MessageBoxButtons.YesNo, _
    '                 MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
    '            Load_ST_Tran_To_My_STORE(SyncHist.CurrentRow.Cells("ST_TRAN_T_ID_CL").Value)
    '            Load_Awaiting_ST_Trans()
    '        End If
    '    End If
    'End Sub


    Private Sub إذنصرفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إذنصرفToolStripMenuItem.Click
        F_Inside_Sales = New Inside_Sales
        F_Inside_Sales.Show()
    End Sub

    Private Sub إذنإستلامToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إذنإستلامToolStripMenuItem.Click
        F_Outside_Sales = New Outside_Sales
        F_Outside_Sales.Show()
    End Sub

    Private Sub عروضToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عروضToolStripMenuItem.Click
        OFFERS.ShowDialog()
    End Sub


    Private Sub ALERT_DGV_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ALERT_DGV.MouseDoubleClick
        Select Case ALERT_DGV.CurrentRow.Cells(0).Value
            Case "Check_For_OpenPierod"
                F_Periods = New Periods
                F_Periods.ShowDialog()
            Case "AG_Max_Debit_SELECT"
                AG_Max_Debit.ShowDialog()
            Case "IM_Valid_V_SELECT"
                F_IM_Valid = New IM_Valid
                F_IM_Valid.ShowDialog()
            Case "IM_MinAlert_V_SELECT"
                Dim F_IM_Qty_Alert As New IM_Qty_Alert
                F_IM_Qty_Alert.Is_Min = True
                F_IM_Qty_Alert.ShowDialog()
            Case "IM_MaxAlert_V_SELECT"
                Dim F_IM_Qty_Alert As New IM_Qty_Alert
                F_IM_Qty_Alert.Is_Min = False
                F_IM_Qty_Alert.ShowDialog()
            Case "Items_Prices_V_SELECT_ALL_Less_Then_Cost"
                Price_Less_Than_Cost.ShowDialog()
            Case "IM_Notes_Valid_V_SELECT"
                IM_Notes_Valid_Form.ShowDialog()
            Case "Items_Prices_V_SELECT_ALL_Negitave_QTY"
                IM_Negitave_QTY.ShowDialog()

            Case "RSV_IM_V_SELECT"
                Dim F As New Rsv_IM_VIEW
                F.ShowDialog()

            Case "ORDER_IM_V_SELECT"
                Dim F As New IM_ORDERS_COMING
                F.ShowDialog()

                'Case "RCT_NOT_WITH_SB"
                '    Dim STR As String = ""
                '    For i = 0 To RCT_NOT_WITH_SB_DT.Rows.Count - 1
                '        STR += " فاتورة رقم: " & RCT_NOT_WITH_SB_DT.Rows(i)(0).ToString & "  -  "
                '    Next
                '    MsgBox(STR, MsgBoxStyle.Exclamation, "أرقام الفواتير")
            Case "Open_Bills"
                OPEN_Bills.ShowDialog()

            Case "IM_OTHER_STORE_V_SELECT"
                Dim F As New IM_OTHER_STORE
                F.ShowDialog()

        End Select
    End Sub

    Private Sub قائمةصلاخيةالأصنافToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles قائمةصلاخيةالأصنافToolStripMenuItem.Click
        IM_Notes_Valid_Form.ShowDialog()
    End Sub


    Dim Second_2 = 0
    ' Private Sub SyncTimer_Tick(sender As Object, e As EventArgs) Handles SyncTimer.Tick
    '    Second_2 += 1
    '    If Second_2 = 60 Then
    '        Try
    '            SyncTimer.Enabled = False
    '            RefreshSuncHist()
    '            SyncTimer.Enabled = True
    '            'If Not Sync_BackgroundWorker.IsBusy Then
    '            '    Sync_BackgroundWorker.RunWorkerAsync()
    '            'End If
    '            Second_2 = 0
    '        Catch ex As Exception
    '            Second_2 = 0
    '        End Try
    '    End If
    'End Sub


    'Private Sub Sync_BackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Sync_BackgroundWorker.DoWork
    '    SyncTimer.Enabled = False
    '    RefreshSuncHist()
    'End Sub

    'Private Sub Sync_BackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Sync_BackgroundWorker.RunWorkerCompleted
    '    SyncTimer.Enabled = True
    'End Sub

    Private Sub رفعملــفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles رفعملــفToolStripMenuItem.Click
        On Error Resume Next
        With Me.OpenFileDialog1
            '  .Filter = "(Image Files)|*.jpg;*.png;*.bmp;*.gif;*.ico|Jpg, | *.jpg|Png, | *.png|Bmp, | *.bmp|Gif, | *.gif|Ico | *.ico"
            .FilterIndex = 1
            .Multiselect = False
            .Title = "حدد ملف"
            .ShowDialog()
            If Len(.FileName) > 0 Then
                UPpload_File()
            End If
        End With
    End Sub



    Private Sub UPpload_File()
        Try


            Dim FILE_TITLE As String = ""
            Dim inp = InputBox("ادخل عنوان الملف", "عنوان الملف")
            If inp = "" Then
                FILE_TITLE = "(ملف بدون عنوان بتاريخ " & Date.Now.ToString & ")"
            Else
                FILE_TITLE = inp
            End If


            Dim filePath As String = OpenFileDialog1.FileName
            'Server.MapPath("APP_DATA/Testxls.xlsx")
            Dim filename As String = Path.GetFileName(filePath)
            Dim fs As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)
            Dim br As BinaryReader = New BinaryReader(fs)
            Dim bytes As Byte() = br.ReadBytes(Convert.ToInt32(fs.Length))
            br.Close()
            fs.Close()

            Dim fi As New IO.FileInfo(filename)
            Dim extn As String = fi.Extension

            Dim C As New Online_C
            C.Str = "INSERT INTO Upload_Files ([DOC_NAME],[DOC_DATA],[DOC_EXTENSION]) values(@DOC_NAME, @DOC_DATA, @DOC_EXTENSION)"
            'Initialize SqlCommand object for insert.
            C.Com = New SqlCommand(C.Str, C.Con)
            'We are passing File Name and Image byte data as sql parameters.
            C.Com.Parameters.Add(New SqlParameter("@DOC_NAME", FILE_TITLE))
            C.Com.Parameters.Add(New SqlParameter("@DOC_DATA", bytes))
            C.Com.Parameters.Add(New SqlParameter("@DOC_EXTENSION", extn))

            C.Con.Open()
            C.Com.ExecuteNonQuery()
            C.Con.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub عرضأخرالملفاتالمرفوعةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضأخرالملفاتالمرفوعةToolStripMenuItem.Click
        Show_Uploaded_Files.ShowDialog()
    End Sub


    Private Sub ValidWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles ValidWorker.DoWork
        Fill_ALL_ALERT()
    End Sub

    Dim Second_3 = 0
    Private Sub TS_Tick(sender As Object, e As EventArgs) Handles ValidTimer.Tick

        Second_3 += 1

        If Second_3 = Thread_Time Then
            Try
                ValidTimer.Stop()



                If Not ValidWorker.IsBusy Then ValidWorker.RunWorkerAsync()
                Second_3 = 0
            Catch ex As Exception
                Second_3 = 0
            End Try
        End If
    End Sub

    Private Sub ValidWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles ValidWorker.RunWorkerCompleted

        Set_Data_Alert()
        ValidTimer.Start()
    End Sub

    Public Sub Set_Data_Alert()
        DataB.DataSource = ALERT_DT
        ALERT_DGV.DataSource = DataB
        'DataB_2.DataSource = Open_Dt
        'Open_MV_DV.DataSource = DataB_2
        ALERT_DGV.Columns(0).Visible = False

        'Open_MV_DV.Columns(0).Visible = False
        'Open_MV_DV.Columns(3).Visible = False
        'Open_MV_DV.Columns(4).Visible = False
        'Open_MV_DV.Columns(6).Visible = False
    End Sub


    Dim Open_Dt As New DataTable
    Dim ALERT_DT As New DataTable
    Public Sub Fill_ALL_ALERT()
        DataB.Dispose()
        DataB = New BindingSource
        ALERT_DT.Clear()
        Open_Dt.Clear()

        Dim N As Integer = 0
        Dim db As New C()

        Try
            ' 💡 نفتح اتصال واحد فقط لكل الإشعارات (هذا سيسرع العملية 10 أضعاف)
            db.Con.Open()
            Dim cmd As New SqlClient.SqlCommand("", db.Con)

            ' ==========================================================
            ' 1. الوردية المفتوحة (استخدام ExecuteScalar لأنه أسرع لجلب رقم واحد)
            ' ==========================================================
            cmd.CommandType = CommandType.Text
            If User_isAdmin = True Then
                cmd.CommandText = "SELECT COUNT(*) FROM Open_Periods_V"
            Else
                cmd.CommandText = "SELECT COUNT(*) FROM Open_Periods_V WHERE USER_ID = " & USER_ID
            End If
            N = Convert.ToInt32(cmd.ExecuteScalar())
            If N > 0 Then ALERT_DT.Rows.Add("Check_For_OpenPierod", " وردية مفتوحة  (" & N & ")")

            ' ==========================================================
            ' دالة مساعدة لعد الصفوف بسرعة فائقة (بدون Using نهائياً 🚫)
            ' ==========================================================
            Dim CountRowsFast = Function(command As SqlClient.SqlCommand) As Integer
                                    Dim count As Integer = 0
                                    Dim dr As SqlClient.SqlDataReader = command.ExecuteReader()
                                    While dr.Read()
                                        count += 1
                                    End While
                                    dr.Close() ' 💡 إغلاق القارئ يدوياً وبأمان للحفاظ على الاتصال
                                    Return count
                                End Function

            cmd.CommandType = CommandType.StoredProcedure

            ' 2. ديون العملاء
            cmd.CommandText = "AG_Max_Debit_SELECT"
            cmd.Parameters.Clear()
            N = CountRowsFast(cmd)
            If N > 0 Then ALERT_DT.Rows.Add("AG_Max_Debit_SELECT", AG_B & " (" & N & ")")

            ' 3. صلاحية الأصناف
            cmd.CommandText = "IM_Valid_V_SELECT"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@END_Date_Valid", Date.Now.Date.AddDays(IM_Day_Valid))
            N = CountRowsFast(cmd)
            If N > 0 Then ALERT_DT.Rows.Add("IM_Valid_V_SELECT", IM_Valid & " (" & N & ")")

            ' 4. أصناف ستنتهي قريباً
            cmd.CommandText = "IM_MinAlert_V_SELECT"
            cmd.Parameters.Clear()
            N = CountRowsFast(cmd)
            If N > 0 Then ALERT_DT.Rows.Add("IM_MinAlert_V_SELECT", IM_Min & " (" & N & ")")

            ' 5. أصناف وصلت أعلى كمية
            cmd.CommandText = "IM_MaxAlert_V_SELECT"
            cmd.Parameters.Clear()
            N = CountRowsFast(cmd)
            If N > 0 Then ALERT_DT.Rows.Add("IM_MaxAlert_V_SELECT", IM_Max & " (" & N & ")")

            ' 6. فواتير مفتوحة
            cmd.CommandText = "Open_AGMV_SELECT"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@USER_ID", USER_ID)
            cmd.Parameters.AddWithValue("@IS_ADMIN", If(U_Save_otherBill, True, User_isAdmin))
            N = CountRowsFast(cmd)
            If N > 0 Then ALERT_DT.Rows.Add("Open_Bills", Open_AGMV & " (" & N & ")")

            ' 7. مبيعات أقل من التكلفة
            If Notif_IM_Sell_Less_Than_Cost = True And U_SB_Show_Cash = True Then
                cmd.CommandText = "[Items_Prices_V_SELECT_ALL_Less_Then_Cost]"
                cmd.Parameters.Clear()
                N = CountRowsFast(cmd)
                If N > 0 Then ALERT_DT.Rows.Add("Items_Prices_V_SELECT_ALL_Less_Then_Cost", IM_SELL_LESS_THAN_COST & " (" & N & ")")
            End If

            ' 8. أصناف بكميات سالبة
            If Notif_IM_Negitane_QTY = True Then
                cmd.CommandText = "[Items_Prices_V_SELECT_ALL_Negitave_QTY]"
                cmd.Parameters.Clear()
                N = CountRowsFast(cmd)
                If N > 0 Then ALERT_DT.Rows.Add("Items_Prices_V_SELECT_ALL_Negitave_QTY", IM_Neg_QTY & " (" & N & ")")
            End If

            ' 9. قائمة ملاحظات صلاحية الأصناف
            cmd.CommandText = "[IM_Notes_Valid_V_SELECT]"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@END_Date_Valid", IM_Day_Valid)
            N = CountRowsFast(cmd)
            If N > 0 Then ALERT_DT.Rows.Add("IM_Notes_Valid_V_SELECT", IM_Notes_Valid & " (" & N & ")")

            ' 10. أصناف مستأجرة
            Try
                cmd.CommandText = "RSV_IM_V_SELECT"
                cmd.Parameters.Clear()
                N = CountRowsFast(cmd)
                If N > 0 Then ALERT_DT.Rows.Add("RSV_IM_V_SELECT", RSV_IM & " (" & N & ")")
            Catch : End Try

            ' 11. بضاعة قادمة
            Try
                cmd.CommandText = "ORDER_IM_V_SELECT"
                cmd.Parameters.Clear()
                N = CountRowsFast(cmd)
                If N > 0 Then ALERT_DT.Rows.Add("ORDER_IM_V_SELECT", ORDER_IM & " (" & N & ")")
            Catch : End Try

            ' 12. أخر تزامن (قراءة يدوية بدون Using نهائياً 🚫)
            Try
                cmd.CommandText = "SELECT top 1 [SyncEnd],[SyncState] FROM [dbo].[SynchroLog] order by id desc"
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Clear()

                Dim drSync As SqlClient.SqlDataReader = cmd.ExecuteReader()
                If drSync.HasRows Then
                    drSync.Read()
                    ALERT_DT.Rows.Add("", " أخر تزامن:  (" & drSync("SyncEnd").ToString() & ")  الحالة : " & drSync("SyncState").ToString())
                End If
                drSync.Close() ' إغلاق القارئ يدوياً
            Catch : End Try

            ' 13. بضاعة المخازن الأخرى
            Try
                cmd.CommandText = "IM_OTHER_STORE_V_SELECT"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@ST_ID", SB_ST_ID)
                N = CountRowsFast(cmd)
                If N > 0 Then ALERT_DT.Rows.Add("IM_OTHER_STORE_V_SELECT", ST_OTHER & " (" & N & ")")
            Catch : End Try

            db.Con.Close()
        Catch ex As Exception
            If db.Con.State = ConnectionState.Open Then db.Con.Close()
        End Try
    End Sub




    'Public Sub Fill_ALL_ALERT()

    '    DataB.Dispose()
    '    DataB = New BindingSource
    '    'ALERT_DGV.DataSource = Nothing

    '    Dim C As New C
    '    Dim N
    '    ALERT_DT.Clear()
    '    Open_Dt.Clear()
    '    Dim WHERE_STR = ""
    '    Dim s = ""

    '    '--------------------------------------------------------------------------------------------------1


    '    If S_Pr = True Then
    '        Check_For_OpenPierod()


    '        If User_isAdmin = True Then
    '            C.Str = "select count(*) as N from Open_Periods_V "
    '        Else
    '            C.Str = "select count(*) as N  from Open_Periods_V WHERE Open_Periods_V.USER_ID  = " & USER_ID
    '        End If
    '        C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
    '        C.Con.Open()

    '        C.Dr = C.Com.ExecuteReader
    '        If C.Dr.HasRows = True Then
    '            C.Dr.Read()

    '            If C.Dr("N") > 0 Then
    '                N = C.Dr("N")
    '                ALERT_DT.Rows.Add("Check_For_OpenPierod", " وردية مفتوحة " & " (" & N & ")")
    '            End If

    '        End If
    '        C.Con.Close()



    '    End If

    '    '--------------------------------------------------------------------------------------------------2
    '    C = New C
    '    With (C.Com)
    '        .Connection = C.Con
    '        .CommandText = "AG_Max_Debit_SELECT"
    '        .CommandType = CommandType.StoredProcedure
    '    End With
    '    C.Da = New SqlClient.SqlDataAdapter(C.Com)
    '    C.Da.Fill(C.Dt)
    '    If C.Dt.Rows.Count > 0 Then

    '        N = C.Dt.Rows.Count.ToString
    '        ALERT_DT.Rows.Add("AG_Max_Debit_SELECT", AG_B & " (" & N & ")")
    '    End If

    '    ''--------------------------------------------------------------------------------------------------3-------------------------

    '    C = New C
    '    With (C.Com)
    '        .Connection = C.Con
    '        .CommandText = "IM_Valid_V_SELECT"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@END_Date_Valid", Date.Now.Date.AddDays(IM_Day_Valid))
    '    End With
    '    C.Da = New SqlClient.SqlDataAdapter(C.Com)
    '    C.Da.Fill(C.Dt)

    '    If C.Dt.Rows.Count > 0 Then
    '        N = C.Dt.Rows.Count.ToString
    '        ALERT_DT.Rows.Add("IM_Valid_V_SELECT", IM_Valid & " (" & N & ")")
    '    End If
    '    '--------------------------------------------------------------------------------------------------4

    '    C = New C
    '    With (C.Com)
    '        .Connection = C.Con
    '        .CommandText = "IM_MinAlert_V_SELECT"
    '        .CommandType = CommandType.StoredProcedure
    '    End With
    '    C.Da = New SqlClient.SqlDataAdapter(C.Com)
    '    C.Da.Fill(C.Dt)
    '    If C.Dt.Rows.Count > 0 Then
    '        N = C.Dt.Rows.Count.ToString
    '        ALERT_DT.Rows.Add("IM_MinAlert_V_SELECT", IM_Min & " (" & N & ")")
    '    End If
    '    '--------------------------------------------------------------------------------------------------5

    '    C = New C
    '    With (C.Com)
    '        .Connection = C.Con
    '        .CommandText = "IM_MaxAlert_V_SELECT"
    '        .CommandType = CommandType.StoredProcedure
    '    End With
    '    C.Da = New SqlClient.SqlDataAdapter(C.Com)
    '    C.Da.Fill(C.Dt)

    '    If C.Dt.Rows.Count > 0 Then
    '        N = C.Dt.Rows.Count.ToString
    '        ALERT_DT.Rows.Add("IM_MaxAlert_V_SELECT", IM_Max & " (" & N & ")")
    '    End If

    '    '--------------------------------------------------------------------------------------------------6

    '    C = New C
    '    With (C.Com)
    '        .Connection = C.Con
    '        .CommandText = "Open_AGMV_SELECT"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@USER_ID", USER_ID)
    '        If U_Save_otherBill = True Then
    '            .Parameters.AddWithValue("@IS_ADMIN", True)
    '        Else
    '            .Parameters.AddWithValue("@IS_ADMIN", User_isAdmin)
    '        End If
    '    End With
    '    C.Da = New SqlClient.SqlDataAdapter(C.Com)
    '    C.Da.Fill(C.Dt)

    '    If C.Dt.Rows.Count > 0 Then
    '        N = C.Dt.Rows.Count.ToString
    '        ALERT_DT.Rows.Add("Open_Bills", Open_AGMV & " (" & N & ")")
    '    End If

    '    '--------------------------------------------------------------------------------------------------7

    '    If Notif_IM_Sell_Less_Than_Cost = True And U_SB_Show_Cash = True Then
    '        C = New C
    '        With C.Com
    '            .Connection = C.Con
    '            .CommandText = "[Items_Prices_V_SELECT_ALL_Less_Then_Cost]"
    '            .CommandType = CommandType.StoredProcedure
    '        End With
    '        C.Da = New SqlClient.SqlDataAdapter(C.Com)
    '        C.Da.Fill(C.Dt)
    '        If C.Dt.Rows.Count > 0 Then
    '            N = C.Dt.Rows.Count.ToString
    '            ALERT_DT.Rows.Add("Items_Prices_V_SELECT_ALL_Less_Then_Cost", IM_SELL_LESS_THAN_COST & " (" & N & ")")
    '        End If
    '    End If

    '    ''--------------------------------------------------------------------------------------------------8


    '    If Notif_IM_Negitane_QTY = True Then
    '        C = New C
    '        With C.Com
    '            .Connection = C.Con
    '            .CommandText = "[Items_Prices_V_SELECT_ALL_Negitave_QTY]"
    '            .CommandType = CommandType.StoredProcedure
    '        End With
    '        C.Da = New SqlClient.SqlDataAdapter(C.Com)
    '        C.Da.Fill(C.Dt)

    '        If C.Dt.Rows.Count > 0 Then

    '            N = C.Dt.Rows.Count.ToString
    '            ALERT_DT.Rows.Add("Items_Prices_V_SELECT_ALL_Negitave_QTY", IM_Neg_QTY & " (" & N & ")")
    '        End If
    '    End If

    '    '--------------------------------------------------------------------------------------------------9


    '    C = New C
    '    With C.Com
    '        .Connection = C.Con
    '        .CommandText = "[IM_Notes_Valid_V_SELECT]"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@END_Date_Valid", IM_Day_Valid)
    '    End With
    '    C.Da = New SqlClient.SqlDataAdapter(C.Com)
    '    C.Da.Fill(C.Dt)
    '    If C.Dt.Rows.Count > 0 Then
    '        N = C.Dt.Rows.Count.ToString
    '        ALERT_DT.Rows.Add("IM_Notes_Valid_V_SELECT", IM_Notes_Valid & " (" & N & ")")
    '    End If
    '    ''--------------------------------------------------------------------------------------------------10
    '    'RCT_NOT_WITH_SB_DT.Clear()
    '    'C = New C
    '    'C.Str = "Select SB_ID from RCT_NOT_WITH_SB_V "
    '    'C.Da = New SqlClient.SqlDataAdapter(C.Str, C.Con)
    '    'C.Da.Fill(RCT_NOT_WITH_SB_DT)

    '    'If RCT_NOT_WITH_SB_DT.Rows.Count > 0 Then
    '    '    N = RCT_NOT_WITH_SB_DT.Rows.Count.ToString
    '    '    ALERT_DT.Rows.Add("RCT_NOT_WITH_SB", RCT_NOT_WITH_SB & " (" & N & ")")
    '    'End If

    '    '--------------------------------------------------------------------------------------------------11


    '    Try

    '        C = New C
    '        With (C.Com)
    '            .Connection = C.Con
    '            .CommandText = "RSV_IM_V_SELECT"
    '            .CommandType = CommandType.StoredProcedure
    '        End With
    '        C.Da = New SqlClient.SqlDataAdapter(C.Com)
    '        C.Da.Fill(C.Dt)

    '        If C.Dt.Rows.Count > 0 Then
    '            N = C.Dt.Rows.Count.ToString
    '            ALERT_DT.Rows.Add("RSV_IM_V_SELECT", RSV_IM & " (" & N & ")")
    '        End If
    '    Catch ex As Exception

    '    End Try




    '    '--------------------------------------------------------------------------------------------------12

    '    Try

    '        C = New C
    '        With (C.Com)
    '            .Connection = C.Con
    '            .CommandText = "ORDER_IM_V_SELECT"
    '            .CommandType = CommandType.StoredProcedure
    '        End With
    '        C.Da = New SqlClient.SqlDataAdapter(C.Com)
    '        C.Da.Fill(C.Dt)

    '        If C.Dt.Rows.Count > 0 Then
    '            N = C.Dt.Rows.Count.ToString
    '            ALERT_DT.Rows.Add("ORDER_IM_V_SELECT", ORDER_IM & " (" & N & ")")
    '        End If
    '    Catch ex As Exception

    '    End Try

    '    '--------------------------------------------------------------------------------------------------13

    '    C = New C
    '    C.Str = "SELECT top 1 [SyncEnd],[SyncState] FROM [dbo].[SynchroLog] order by id desc"

    '    C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
    '    C.Con.Open()

    '    C.Dr = C.Com.ExecuteReader
    '    If C.Dr.HasRows = True Then
    '        C.Dr.Read()
    '        ALERT_DT.Rows.Add("", " أخر تزامن: " & " (" & C.Dr("SyncEnd") & ")  الحالة : " & C.Dr("SyncState"))
    '    End If
    '    C.Con.Close()

    '    '--------------------------------------------------------------------------------------------------14
    '    Try

    '        C = New C
    '        With (C.Com)
    '            .Connection = C.Con
    '            .CommandText = "IM_OTHER_STORE_V_SELECT"
    '            .CommandType = CommandType.StoredProcedure
    '            .Parameters.AddWithValue("@ST_ID", SB_ST_ID)
    '        End With
    '        C.Da = New SqlClient.SqlDataAdapter(C.Com)
    '        C.Da.Fill(C.Dt)

    '        If C.Dt.Rows.Count > 0 Then
    '            N = C.Dt.Rows.Count.ToString
    '            ALERT_DT.Rows.Add("IM_OTHER_STORE_V_SELECT", ST_OTHER & " (" & N & ")")
    '        End If
    '    Catch ex As Exception

    '    End Try


    'End Sub



    Private Sub ALERT_DGV_MouseHover(sender As Object, e As EventArgs) Handles ALERT_DGV.MouseHover
        ValidTimer.Stop()
    End Sub

    Private Sub ALERT_DGV_MouseLeave(sender As Object, e As EventArgs) Handles ALERT_DGV.MouseLeave
        ValidTimer.Start()
    End Sub



    Dim Second_4
    Private Sub Con_Timer_Tick(sender As Object, e As EventArgs) Handles Con_Timer.Tick

        Second_4 += 1
        If Second_4 = 6 Then
            Try
                Con_Timer.Stop()
                If Not Con_Worker.IsBusy Then Con_Worker.RunWorkerAsync()
                Second_4 = 0

                If is_Con = True Then
                    ServeConnect_LB.BackColor = Color.DarkGreen
                    ServeConnect_LB.Text = "متصل بالرئيسي"
                Else
                    ServeConnect_LB.BackColor = Color.DarkRed
                    ServeConnect_LB.Text = "غير متصل بالرئيسي"
                End If

            Catch ex As Exception
                Second_4 = 0
            End Try
        End If
    End Sub


    Private Sub Con_Worker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Con_Worker.DoWork
        Check_Server_Con()
    End Sub

    Private Sub Con_Worker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Con_Worker.RunWorkerCompleted
        Con_Timer.Start()
    End Sub


    Public Function IsProjectOnExternalDisk(ByVal driveLetter As String) As Boolean
        Dim retVal = False
        driveLetter = driveLetter.TrimEnd("\"c)

        ' browse all USB WMI physical disks
        For Each drive As ManagementObject In New ManagementObjectSearcher("select DeviceID, MediaType,InterfaceType from Win32_DiskDrive").[Get]()
            ' associate physical disks with partitions
            Dim partitionCollection As ManagementObjectCollection = New ManagementObjectSearcher(String.Format(CStr("associators of {{Win32_DiskDrive.DeviceID='{0}'}} " & "where AssocClass = Win32_DiskDriveToDiskPartition"), drive(CStr("DeviceID")))).[Get]()

            For Each partition As ManagementObject In partitionCollection
                If partition IsNot Nothing Then
                    ' associate partitions with logical disks (drive letter volumes)
                    Dim logicalCollection As ManagementObjectCollection = New ManagementObjectSearcher(String.Format(CStr("associators of {{Win32_DiskPartition.DeviceID='{0}'}} " & "where AssocClass= Win32_LogicalDiskToPartition"), partition(CStr("DeviceID")))).[Get]()

                    For Each logical As ManagementObject In logicalCollection
                        If logical IsNot Nothing Then
                            ' finally find the logical disk entry
                            Dim volumeEnumerator As ManagementObjectCollection.ManagementObjectEnumerator = New ManagementObjectSearcher(String.Format(CStr("select DeviceID from Win32_LogicalDisk " & "where Name='{0}'"), logical(CStr("Name")))).[Get]().GetEnumerator()

                            volumeEnumerator.MoveNext()

                            Dim volume As ManagementObject = CType(volumeEnumerator.Current, ManagementObject)

                            If driveLetter.ToLowerInvariant().Equals(volume(CStr("DeviceID")).ToString().ToLowerInvariant()) AndAlso (drive("MediaType").ToString().ToLowerInvariant().Contains("external") OrElse drive("InterfaceType").ToString().ToLowerInvariant().Contains("usb")) Then
                                retVal = True
                                Exit For
                            End If
                        End If
                    Next
                End If
            Next
        Next

        Return retVal
    End Function

    Private Sub فواتيرمبيعاتمتعارضهمعالإيصالاتToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles فواتيرمبيعاتمتعارضهمعالإيصالاتToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor
        Get_Rct_Not_With_Bills()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Get_Rct_Not_With_Bills()
        RCT_NOT_WITH_SB_DT.Clear()
        Dim C = New C
        C.Str = "Select SB_ID from RCT_NOT_WITH_SB_V "
        C.Da = New SqlClient.SqlDataAdapter(C.Str, C.Con)
        C.Da.Fill(RCT_NOT_WITH_SB_DT)

        'If RCT_NOT_WITH_SB_DT.Rows.Count > 0 Then
        '    ' N = RCT_NOT_WITH_SB_DT.Rows.Count.ToString
        '    ALERT_DT.Rows.Add("RCT_NOT_WITH_SB", RCT_NOT_WITH_SB & " (" & N & ")")
        'End If

        Dim STR As String = ""
        For i = 0 To RCT_NOT_WITH_SB_DT.Rows.Count - 1
            STR += " فاتورة رقم: " & RCT_NOT_WITH_SB_DT.Rows(i)(0).ToString & "  -  "
        Next

        If Not String.IsNullOrWhiteSpace(STR) Then
            MsgBox(STR, MsgBoxStyle.Exclamation, "أرقام الفواتير")
        Else
            MsgBox(" لا يوجد فواتير", MsgBoxStyle.Information, "أرقام الفواتير")
        End If

    End Sub

    Private Sub عرضفواتيرموردToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضفواتيرموردToolStripMenuItem.Click
        SearchAgent_Pch_Bill.ShowDialog()
    End Sub

    Private Sub system_ALert_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        Application.Exit()
    End Sub

    Private Sub الطلبياتالقادمـــةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles الطلبياتالقادمـــةToolStripMenuItem.Click
        Dim F As New IM_ORDERS_COMING
        F.ShowDialog()
    End Sub

    Private Sub شاشةالحساباتToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles شاشةالحساباتToolStripMenuItem.Click
        Me.Cursor = Cursors.AppStarting
        F_Balances = New Balances
        F_Balances.Show()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub سحــبToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles سحــبToolStripMenuItem.Click
        F_Tr_Deposit_Withdraw = New Tr_Deposit_Withdraw
        TR_Type = 1
        F_Tr_Deposit_Withdraw.ShowDialog()
    End Sub

    Private Sub إيداعToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إيداعToolStripMenuItem.Click
        F_Tr_Deposit_Withdraw = New Tr_Deposit_Withdraw
        TR_Type = 2
        F_Tr_Deposit_Withdraw.ShowDialog()
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        F_Tr_Transfers = New Tr_Transfers
        TR_Type = 3
        F_Tr_Transfers.ShowDialog()
    End Sub

    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        query("UPDATE SysSetting SET Default_Printer_80 = '" & KashierPrinterComboBox.Text & "' ,  Default_Printer_A4 = '" & A4Printer_Cmb.Text & "' , Barcode_DefPrinter = '" & Barcode_DefPrinter_Cm.Text & "' WHERE CP_NAME = '" & My.Computer.Name & "' ")
        Get_Computer_Setting()
    End Sub


    Private Function PrinterState(Namee As String)
        Try
            ' Set management scope
            Dim scope As New ManagementScope("\root\cimv2")
            scope.Connect()

            ' Select Printers from WMI Object Collections
            Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_Printer")

            Dim printerName As String = ""
            For Each printer As ManagementObject In searcher.[Get]()
                printerName = printer("Name").ToString().ToLower()
                If String.Compare(printerName, Namee, StringComparison.InvariantCultureIgnoreCase) = 0 Then
                    If printer("WorkOffline").ToString().ToLower().Equals("true") Then
                        ' printer is offline by user
                        MsgBox("الطابعــة غير متصلــة", MsgBoxStyle.Critical, "")
                        Return False
                    Else
                        MsgBox("الطابعــة متصلــة", MsgBoxStyle.Information, "")
                        ' printer is not offline
                        Return True
                    End If
                End If
            Next
            MessageBox.Show("الطابعة ***" & Namee & "***  غير موجودة بطابعات هذا الجاهز... او انها قد تم حذفها")
            Return False
        Catch ex As Exception
            Return False
        End Try
        '-------------------------------------------------------------------------------------------------------------------------------
        '' Loop through all installed printers
        'For Each installedPrinter As String In PrinterSettings.InstalledPrinters
        '    ' Check if the installed printer matches the one we're looking for
        '    If installedPrinter.Equals(printerName, StringComparison.OrdinalIgnoreCase) Then
        '        MsgBox("الطابعــة متصلــة", MsgBoxStyle.Information, "")
        '        Return True ' Printer is connected
        '    End If
        'Next
        'MsgBox("الطابعــة غير متصلــة", MsgBoxStyle.Critical, "")
        '' Printer is not found in the installed printers list
        'Return False
    End Function

    Private Sub check_print_Btn1_Click(sender As Object, e As EventArgs) Handles check_print_Btn1.Click
        PrinterState(KashierPrinterComboBox.Text)

    End Sub


    Private Sub check_print_Btn2_Click(sender As Object, e As EventArgs) Handles check_print_Btn2.Click
        PrinterState(A4Printer_Cmb.Text)
    End Sub

    Private Sub check_print_Btn3_Click(sender As Object, e As EventArgs) Handles check_print_Btn3.Click
        PrinterState(Barcode_DefPrinter_Cm.Text)
    End Sub

    Private Sub تحويلبينالحساباتToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تحويلبينالحساباتToolStripMenuItem.Click
        Dim f As New AG_Transfers
        f.ShowDialog()
    End Sub

    Private Sub شاشةتعديلالأسعارToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles شاشةتعديلالأسعارToolStripMenuItem.Click
        Dim F = New FrmBulkPriceUpdate
        F.Show()
    End Sub

    Private Sub تقريرتعديلالأسعارToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تقريرتعديلالأسعارToolStripMenuItem.Click
        Dim F = New FrmPriceHistoryViewer
        F.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Frm As New Frm_DashboardButtons
        Frm.ShowDialog()
        Load_Dashboard_Buttons()
    End Sub

    ' =====================================================================
    ' 🚀 نظام توليد أزرار الداشبورد الديناميكية 🚀
    ' =====================================================================
    ' =====================================================================
    ' 🚀 نظام توليد أزرار الداشبورد الديناميكية 🚀
    ' =====================================================================
    Private Sub Load_Dashboard_Buttons()
        ' 1. تنظيف البانل الخاص بك
        Panel_QuickButtons.Controls.Clear()

        ' 💡 الخدعة السحرية: إنشاء حاوية ذكية برمجياً لترتيب الأزرار ومنع تكدسها فوق بعض!
        Dim FLP As New FlowLayoutPanel()
        FLP.Dock = DockStyle.Fill
        FLP.AutoScroll = True
        FLP.BackColor = Color.Transparent
        Panel_QuickButtons.Controls.Add(FLP)

        Dim db As New C()
        Try
            ' 2. جلب الأزرار
            db.Str = "SELECT * FROM Sys_Dashboard_Buttons_List WHERE Is_Global_Active = 1 ORDER BY Default_Sort_Order ASC"
            Dim dt As New DataTable()
            db.Da = New SqlClient.SqlDataAdapter(db.Str, db.Con)
            db.Da.Fill(dt)

            ' 3. رسم الأزرار
            For Each row As DataRow In dt.Rows
                Try
                    Dim btn As New Button()
                    btn.Name = row("Action_Key").ToString()
                    btn.Text = row("Symbol_Char").ToString() & vbCrLf & row("Button_Title").ToString()
                    btn.Size = New Size(160, 110)
                    btn.FlatStyle = FlatStyle.Flat
                    btn.FlatAppearance.BorderSize = 0
                    btn.Font = New Font("Segoe UI", 14, FontStyle.Bold)
                    btn.Cursor = Cursors.Hand
                    btn.Margin = New Padding(8)

                    ' 💡 منع الثيم من الكتابة فوق ألواننا المخصصة
                    btn.Tag = "IGNORE"

                    ' قراءة اللون من الداتابيز
                    Dim bgStr As String = row("Button_BackColor").ToString()
                    Dim bgColor As Color = Color.FromArgb(59, 130, 246) ' أزرق افتراضي 
                    If Not String.IsNullOrWhiteSpace(bgStr) Then
                        Dim pts() As String = bgStr.Split(","c)
                        If pts.Length = 3 Then
                            bgColor = Color.FromArgb(CInt(pts(0).Trim()), CInt(pts(1).Trim()), CInt(pts(2).Trim()))
                        End If
                    End If

                    ' تلوين الزر وتلوين النص بشكل متعاكس ليكون واضح
                    btn.BackColor = bgColor
                    btn.ForeColor = ThemeManager.GetContrastColor(bgColor)

                    ' 💡 [الحل القاطع لمشكلة اختفاء الزر]
                    ' حساب لون صلب أغمق قليلاً من لون الزر ليكون تأثير الماوس واضحاً ولا يندمج مع الخلفية البيضاء
                    Dim hoverR As Integer = Math.Max(0, bgColor.R - 30)
                    Dim hoverG As Integer = Math.Max(0, bgColor.G - 30)
                    Dim hoverB As Integer = Math.Max(0, bgColor.B - 30)
                    ' لو كان الزر داكناً جداً، نقوم بتفتيحه بدلاً من تغميقه
                    If bgColor.R < 50 AndAlso bgColor.G < 50 AndAlso bgColor.B < 50 Then
                        hoverR = Math.Min(255, bgColor.R + 40)
                        hoverG = Math.Min(255, bgColor.G + 40)
                        hoverB = Math.Min(255, bgColor.B + 40)
                    End If
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(hoverR, hoverG, hoverB)

                    ' ربط الزر بحدث الضغط
                    AddHandler btn.Click, AddressOf DashboardButton_Click

                    ' 💡 إضافة الزر للحاوية الذكية التي أنشأناها برمجياً
                    FLP.Controls.Add(btn)

                Catch ex_inner As Exception
                    ' صمت لتجاوز أي زر به بيانات خاطئة
                End Try
            Next

        Catch ex As Exception
            MsgBox("خطأ عام في جلب بيانات الداشبورد: " & ex.Message)
        End Try
    End Sub

    ' =====================================================================
    ' 🚀 كود توجيه الأزرار (الراوتينق) وصلاحياتها 🚀
    ' =====================================================================
    Private Sub DashboardButton_Click(sender As Object, e As EventArgs)
        Dim clickedBtn As Button = DirectCast(sender, Button)
        Dim ActionKey As String = clickedBtn.Name

        ' توجيه المستخدم حسب مفتاح الإجراء (مع فحص الصلاحيات برمجياً)
        Select Case ActionKey
            Case "FRM_SALES"
                If Sales_Btn.Enabled = False Then MsgBox("ليس لديك صلاحية لفتح فاتورة المبيعات!", MsgBoxStyle.Critical) : Exit Sub
                SB_LB.PerformClick()

            Case "FRM_PCH"
                If Pch_btn.Enabled = False Then MsgBox("ليس لديك صلاحية المشتريات!", MsgBoxStyle.Critical) : Exit Sub
                Pch_LB.PerformClick()

            Case "FRM_ITEMS"
                If ITEMS_btn.Enabled = False Then MsgBox("ليس لديك صلاحية لإدارة الأصناف!", MsgBoxStyle.Critical) : Exit Sub
                إدارةالأصنافToolStripMenuItem.PerformClick()

            Case "FRM_TABLES"
                ' Table_Bill_Screen_btn.PerformClick() (أنت حذفته من الديزاينر فاستخدم الحدث المباشر:)
                Table_Bill_Screen.ShowDialog()

            Case "FRM_RECEIPT"
                If CashLB.Enabled = False Then MsgBox("ليس لديك صلاحية لإيصالات القبض والصرف!", MsgBoxStyle.Critical) : Exit Sub
                CashLB.PerformClick()

            Case "FRM_BALANCES"
                شاشةالحساباتToolStripMenuItem.PerformClick()

            Case "FRM_RTN_SALES"
                SBRtn_LB.PerformClick()

            Case "FRM_RTN_PCH"
                Pch_Rtn_LB.PerformClick()

            Case "FRM_STORES"
                ST_Explore_LB.PerformClick()

            Case "FRM_REPORTS"
                If Reports_btn.Enabled = False Then MsgBox("ليس لديك صلاحية التقارير!", MsgBoxStyle.Critical) : Exit Sub
                ALL_Report_LB.PerformClick()

            Case "FRM_SETTINGS"
                If Sys_Setting_btn.Enabled = False Then MsgBox("ليس لديك صلاحية الإعدادات!", MsgBoxStyle.Critical) : Exit Sub
                Sys_Setting_btn.PerformClick()

            Case "FRM_AGENTS"
                بطاقةالعملاءToolStripMenuItem.PerformClick()

            Case Else
                MsgBox("هذا الإجراء غير مبرمج بعد: " & ActionKey, MsgBoxStyle.Information)
        End Select
    End Sub
End Class