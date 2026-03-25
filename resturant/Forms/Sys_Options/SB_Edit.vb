Imports System.Drawing.Printing
Imports System.Data.SqlClient
Imports System.IO

Public Class SB_Edit
    Dim Data As Byte()
    Private Sub Ask_ToPrintSubPrinterCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles Notif_IM_Sell_Less_Than_Cost_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub isPrintAfterEndBill_CB_CheckedChanged(sender As Object, e As EventArgs) Handles isPrintAfterEndBill_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub isShowPrint_CB_CheckedChanged(sender As Object, e As EventArgs) Handles isShowPrint_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub KshPrint_CB_CheckedChanged(sender As Object, e As EventArgs) Handles KshPrint_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub isDiscountCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles isDiscountCheckBox.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub MinimumDiscountTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MinimumDiscountTextBox.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub MinimumDiscountTextBox_TextChanged(sender As Object, e As EventArgs) Handles MinimumDiscountTextBox.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub SB_Edit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        SM_LoadPrinters_ToOptions()
        GetSerialPortNames()
        Load_Bill_Types()
        Load_Pages_Types()
        Load_AG()
        Fetch_Tr()
        Fetch_Store()
        Load_Units()
        IntialForm()
        Check_Sys_Featurs()
    End Sub

    Private Sub Check_Sys_Featurs()
        PrintTBKsh_CB.Visible = S_Tables
        CP_Bill_Screen_CB.Visible = S_Tables
        If SScreenDefault_Cmb.SelectedIndex <> 1 Then
            isShowPrint_CB.Visible = False
        Else
            isShowPrint_CB.Visible = True
        End If

        Table_Panel2.Visible = S_Tables
        Pr_Panel.Visible = S_Pr
        SubPrint_Panel.Visible = S_SubPrints

        isUseAsPhone_Crawler_CB.Visible = S_Phone_For_Tables

    End Sub

    Sub GetSerialPortNames()
        ' Show all available COM ports.
        For Each sp As String In My.Computer.Ports.SerialPortNames
            Total_Port_cm.Items.Add(sp)
        Next
    End Sub

    Public Sub Load_Units()
        Dim c As New C
        Try
            Dim sql As String = " select U_ID,U_Name from Units WHERE U_Cargo = 1"
            c.Da = New SqlClient.SqlDataAdapter(sql, c.Con)
            c.Da.Fill(c.Dt)
            IM_Unit_cm.DataSource = c.Dt
            IM_Unit_cm.DisplayMember = "U_Name"
            IM_Unit_cm.ValueMember = "U_ID"
            IM_Unit_cm.SelectedValue = Def_U_ID
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Fetch_Store()
        Dim c As New C
        Dim s As String = "select ST_ID,St_Name from Stores "

        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
        Dim dt_S As New DataTable
        Dim dt_P As New DataTable
        c.Da.Fill(dt_S)
        c.Da.Fill(dt_P)

        SalesStoreComboBox.DataSource = dt_S
        PcshStoreComboBox.DataSource = dt_P

        SalesStoreComboBox.DisplayMember = "St_Name"
        SalesStoreComboBox.ValueMember = "ST_ID"

        PcshStoreComboBox.DisplayMember = "St_Name"
        PcshStoreComboBox.ValueMember = "ST_ID"

        SalesStoreComboBox.SelectedValue = SB_ST_ID
        PcshStoreComboBox.SelectedValue = PCH_ST_ID


        SBST_CanChange_cm.Checked = SB_ST_Can_change
        PCHST_CanChange_cm.Checked = PCH_ST_Can_change

    End Sub

    Private Sub Fetch_Tr()
        Dim c As New C
        Dim s As String = "select Tr_ID,Tr_Name FROM TR_MENU_V "

        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
        Dim dt_1, dt_2, dt_3, dt_4 As New DataTable

        c.Da.Fill(dt_1)
        c.Da.Fill(dt_2)
        c.Da.Fill(dt_3)
        c.Da.Fill(dt_4)

        SalesTrComboBox.DataSource = dt_1
        PcshTrComboBox.DataSource = dt_2
        Reciept_Tr_cmb.DataSource = dt_3
        ExpTrComboBox.DataSource = dt_4

        SalesTrComboBox.DisplayMember = "Tr_Name"
        SalesTrComboBox.ValueMember = "Tr_ID"

        PcshTrComboBox.DisplayMember = "Tr_Name"
        PcshTrComboBox.ValueMember = "Tr_ID"

        Reciept_Tr_cmb.DisplayMember = "Tr_Name"
        Reciept_Tr_cmb.ValueMember = "Tr_ID"

        ExpTrComboBox.DisplayMember = "Tr_Name"
        ExpTrComboBox.ValueMember = "Tr_ID"
    End Sub


    Private Sub Load_AG()

        Dim C As New C
        Dim sql As String = " select AG_ID,Ag_name from Agents WHERE Type_ID NOT IN ('" & Suply_Type_ID & "') Order by Ag_name ASC"
        C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
        C.Da.Fill(C.Dt)
        Default_AG_cm.DataSource = C.Dt
        Default_AG_cm.DisplayMember = "Ag_name"
        Default_AG_cm.ValueMember = "AG_ID"
        Default_AG_cm.SelectedValue = SB_Default_AG
    End Sub

    Public Sub Load_Bill_Types()
        Dim c As New C
        c.Str = "select B_Type_ID,B_Name from Sales_Bills_Types"
        c.Da = New SqlDataAdapter(c.Str, c.Con)
        c.Da.Fill(c.Dt)
        BillDefaultStateCm.DataSource = c.Dt
        BillDefaultStateCm.DisplayMember = "B_Name"
        BillDefaultStateCm.ValueMember = "B_Type_ID"
        BillDefaultStateCm.SelectedValue = SB_DefaultStatus

        Dim c2 As New C
        c2.Str = "select ID,Type from Sales_Bill_Page"
        c2.Da = New SqlDataAdapter(c2.Str, c2.Con)
        c2.Da.Fill(c2.Dt)
        Sales_Bill_Page_cm.DataSource = c2.Dt
        Sales_Bill_Page_cm.DisplayMember = "Type"
        Sales_Bill_Page_cm.ValueMember = "ID"
        Sales_Bill_Page_cm.SelectedValue = Sales_Page_ID

        Dim c3 As New C
        c3.Str = "select ID,Type from Sales_ViewBill_Page"
        c3.Da = New SqlDataAdapter(c3.Str, c3.Con)
        c3.Da.Fill(c3.Dt)
        Sales_ViewBill_Page_cm.DataSource = c3.Dt
        Sales_ViewBill_Page_cm.DisplayMember = "Type"
        Sales_ViewBill_Page_cm.ValueMember = "ID"
        Sales_ViewBill_Page_cm.SelectedValue = Sales_ViewPage_ID

    End Sub

    Public Sub Load_Pages_Types()
        Dim c As New C
        c.Str = "select ID,Type from OrdersPageType"
        c.Da = New SqlDataAdapter(c.Str, c.Con)
        c.Da.Fill(c.Dt)
        Orders_PageTypeCm.DataSource = c.Dt
        Orders_PageTypeCm.DisplayMember = "Type"
        Orders_PageTypeCm.ValueMember = "ID"
        Orders_PageTypeCm.SelectedValue = OrderPage_ID

        c = New C
        c.Str = "select ID,Type from ReceiptsPageType"
        c.Da = New SqlDataAdapter(c.Str, c.Con)
        c.Da.Fill(c.Dt)
        Receipts_PageTypeCm.DataSource = c.Dt
        Receipts_PageTypeCm.DisplayMember = "Type"
        Receipts_PageTypeCm.ValueMember = "ID"
        Receipts_PageTypeCm.SelectedValue = ReceiptPage_ID

        c = New C
        c.Str = "select ID,Type from AGBillType"
        c.Da = New SqlDataAdapter(c.Str, c.Con)
        c.Da.Fill(c.Dt)
        AGBillType.DataSource = c.Dt
        AGBillType.DisplayMember = "Type"
        AGBillType.ValueMember = "ID"
        AGBillType.SelectedValue = AGBillPage_ID

    End Sub

    Private Sub SM_LoadPrinters_ToOptions()
        Dim pkInstalledPrinters As String
        ' Find all printers installed
        For Each pkInstalledPrinters In _
            PrinterSettings.InstalledPrinters
            A4Printer_Cmb.Items.Add(pkInstalledPrinters)
            KashierPrinterComboBox.Items.Add(pkInstalledPrinters)
            Barcode_DefPrinter_Cm.Items.Add(pkInstalledPrinters)
            Copy_Printer_Path_Cm.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters
        ' Set the combo to the first printer in the list
        A4Printer_Cmb.SelectedIndex = -1
        KashierPrinterComboBox.SelectedIndex = -1
        Barcode_DefPrinter_Cm.SelectedIndex = -1
        Copy_Printer_Path_Cm.SelectedIndex = -1
    End Sub

    Public Sub IntialForm()

        If String.IsNullOrWhiteSpace(Default_Printer_80) = False Then KashierPrinterComboBox.SelectedItem = Default_Printer_80
        If String.IsNullOrWhiteSpace(Default_Printer_A4) = False Then A4Printer_Cmb.SelectedItem = Default_Printer_A4
        If String.IsNullOrWhiteSpace(Default_Barcode_Printer) = False Then Barcode_DefPrinter_Cm.SelectedItem = Default_Barcode_Printer
        If String.IsNullOrWhiteSpace(Copy_Printer) = False Then Copy_Printer_Path_Cm.SelectedItem = Copy_Printer


        isPrintAfterEndBill_CB.Checked = SB_AutoPrint
        isShowPrint_CB.Checked = isShowPrintBtn
        isDiscountCheckBox.Checked = isDiscount
        If isDiscountCheckBox.Checked = False Then DiscountGroupBox.Enabled = False

        MinimumDiscountTextBox.Text = MinimumDiscountValue
        BillDefaultStateCm.SelectedValue = SB_DefaultStatus
        PrintTBKsh_CB.Checked = PrintTBKsh

        KshPrint_CB.Checked = KshPrint
        SScreenDefault_Cmb.SelectedIndex = SScreenDefault
        isCostmerScreen_CB.Checked = isCostmerScreen
        IM_Day_Valid_txt.Text = IM_Day_Valid
        'Use_Order_cb.Checked = S_Orders
        CP_Bill_Screen_CB.Checked = CP_Bill_Screen
        Sales_Bill_Page_cm.SelectedValue = Sales_Page_ID
        User_AutoPrintSB_CB.Checked = User_AutoPrintSB
        is_Use_Total_Port_CB.Checked = is_SB_OutputScreen
        '  SB_Default_ST_CM.SelectedIndex = SB_Default_ST
        POS_IM_Default_CM.SelectedIndex = POS_By_Shortcut
        Notif_IM_Sell_Less_Than_Cost_CB.Checked = Notif_IM_Sell_Less_Than_Cost
        SB_IM_Alert_When_Repetition_CB.Checked = SB_IM_Alert_When_Repetition
        Discount_Distribute_CB.Checked = Discount_Distribute
        SB_is_Check_Thankes_CB.Checked = SB_is_Check_Thankes

        SalesTrComboBox.SelectedValue = Org_SB_TR_ID
        PcshTrComboBox.SelectedValue = Org_PCH_TR_ID
        Reciept_Tr_cmb.SelectedValue = Org_Rct_Tr_ID
        ExpTrComboBox.SelectedValue = Org_EXP_TR_ID

        CMPNameTextBox.Text = SBill_Title_1
        CmpEnglishNameTextBox.Text = SBill_Title_2
        NotesTextBox.Text = SBill_Footer

        SHOWphto()

        E_mail_txt.Text = E_mail
        E_mail_Pass_txt.Text = E_mail_Password
        Host_txt.Text = E_mail_Host
        Port_txt.Text = E_mail_Port
        IM_Min_Qty_cb.Checked = IM_min_QTY

        Recipt_Tr_CanChange_CB.Checked = Rct_Tr_Can_change

        'Allow_MinSP_CB.Checked = S_Allow_MinSP
        SB_AutoOpenDrawer_CB.Checked = SB_AutoOpenDrawer
        TypeOfPrint_Cmb.SelectedIndex = Pr_PrinterPage_Type
        is_Multi_BAR_CB.Checked = S_is_Multi_BAR

        Sales_ViewBill_Page_cm.SelectedValue = Sales_ViewPage_ID
        IM_Type_cm.SelectedIndex = IM_Default_Stut

        is_Use_Total_Port_CB.Checked = is_Use_Total_Port
        If String.IsNullOrWhiteSpace(Total_Port) = False Then Total_Port_cm.SelectedItem = Total_Port



        Ban_Expierd_IM_MV_cb.Checked = Ban_Expierd_IM_MV
        Notif_IM_Sell_Less_Than_Cost_CB.Checked = Notif_IM_Sell_Less_Than_Cost
        Def_Befor_Print_CB.Checked = Def_Befor_Print
        SB_is_Check_IM_CB.Checked = SB_is_Check_IM
        SB_is_Copy_CB.Checked = SB_is_Copy_Print
        is_Screen_Font_Resize_cb.Checked = is_Screen_Font_Resize
        Font_Resize_Num_Txt.Text = Font_Resize_Num

        Use_Inside_Barcode_CB.Checked = Use_Inside_Barcode
        Notif_IM_Negitane_QTY_CB.Checked = Notif_IM_Negitane_QTY
        Notif_If_SB_Has_No_SB_Price_CB.Checked = Notif_If_SB_Has_No_SB_Price

        If Total_Port_cm.Items.Count > 0 Then
            is_Use_Total_Port_CB.Enabled = True
        Else
            is_Use_Total_Port_CB.Checked = False
            is_Use_Total_Port_CB.Enabled = False
        End If

        DOT1_F_txt.Text = Mizan_BarcodeFrom
        DOT1_2_txt.Text = Mizan_BarcodeTo
        DOT2_F_txt.Text = Mizan_QtyFrom
        DOT2_T_txt.Text = Mizan_QtyTo

        isUseAsPhone_Crawler_CB.Checked = isUseAsPhone_Crawler
        POS_IM_COUNT_CM.Text = POS_IM_COUNT

        If Second_Part_isPrice = 1 Then
            Price_Rd.Checked = True
        Else
            Balance_Rd.Checked = True
        End If

        QTY_ALERT_SOUND_CB.Checked = QTY_ALERT_SOUND

        Copy_Printer_CB_2.Checked = is_POS_Copy_2
        Copy_Printer_2_cm.Text = POS_Copy_2_Path

        Select Case Thread_Time
            Case 0 : Thread_Time_CM.SelectedIndex = 0
            Case 60 : Thread_Time_CM.SelectedIndex = 1
            Case 300 : Thread_Time_CM.SelectedIndex = 2
            Case 1800 : Thread_Time_CM.SelectedIndex = 3
            Case 3600 : Thread_Time_CM.SelectedIndex = 4
            Case 7200 : Thread_Time_CM.SelectedIndex = 5
            Case 14400 : Thread_Time_CM.SelectedIndex = 6
        End Select

        SB_Remove_Dec_CB.Checked = SB_Remove_Dec

        Open_NewBill_When_OpenSale_CB.Checked = Open_NewBill_When_OpenSale

        SB_IM_NEW_ROW_CB.Checked = SB_IM_NEW_ROW

        Manual_FRM_rpt_cm.SelectedIndex = Manual_FRM_rpt

        OutSale_rpt_CM.SelectedIndex = OutSale_rpt
        is_Home_Mange_Printers_CB.Checked = is_Home_Mange_Printers


        POS_BARCODE_TYPE_CM.Text = POS_BARCODE_TYPE

        SERVER_IMG_PATH_txt.Text = MY_Settings.SERVER_IMG_PATH


        Dim colorToApply As Color

        If Not String.IsNullOrEmpty(MY_Settings.DEBIT_COLOR) Then
            colorToApply = ColorTranslator.FromHtml(MY_Settings.DEBIT_COLOR)
            DEBIT_COLOR_Panel.BackColor = colorToApply
        End If



        If Not String.IsNullOrEmpty(MY_Settings.CREDIT_COLOR) Then
            colorToApply = ColorTranslator.FromHtml(MY_Settings.CREDIT_COLOR)
            CREDIT_COLOR_Panel.BackColor = colorToApply
        End If

        N_Point_Fter_CM.Text = MY_Settings.N_Point_Fter


    End Sub

    Private Sub SHOWphto()
        'On Error Resume Next
        Dim c As New C
        Dim s As String = "SELECT LOGO FROM SysSetting"
        c.Com = New SqlCommand(s, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows = True Then
                c.Dr.Read()
                If IsDBNull(c.Dr("LOGO")) = False Then
                    Data = DirectCast(c.Dr("LOGO"), Byte())
                    Dim MS As New MemoryStream(Data)
                    IMPictureBox.Image = Image.FromStream(MS)
                Else
                    IMPictureBox.Image = My.Resources.white
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()

    End Sub

    Private Sub MinimumDiscountTextBox_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MinimumDiscountTextBox.Validating
        If String.IsNullOrWhiteSpace(MinimumDiscountTextBox.Text) Then MinimumDiscountTextBox.Text = 0
    End Sub

    Private Sub PrintTBKsh_CB_CheckedChanged(sender As Object, e As EventArgs) Handles PrintTBKsh_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        Update_SB_Info()
    End Sub

    Private Sub Update_SB_Info()
        Dim c As New C

        With c.Com
            .Connection = c.Con
            .CommandText = "Update_SB_Edit"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@CompName", CMPNameTextBox.Text)
            .Parameters.AddWithValue("@englishN", CmpEnglishNameTextBox.Text)
            .Parameters.AddWithValue("@logo", ConvertImage(IMPictureBox.Image))
            .Parameters.AddWithValue("@BillNotes", NotesTextBox.Text)
            .Parameters.AddWithValue("@CPU_ID", My.Computer.Name)
            .Parameters.AddWithValue("@Default_Printer_80", KashierPrinterComboBox.Text)
            .Parameters.AddWithValue("@Default_Printer_A4", A4Printer_Cmb.Text)
            .Parameters.AddWithValue("@SB_AutoPrint", isPrintAfterEndBill_CB.Checked)
            .Parameters.AddWithValue("@isShowPrintBtn", isShowPrint_CB.Checked)
            .Parameters.AddWithValue("@isDiscount", isDiscountCheckBox.Checked)
            .Parameters.AddWithValue("@MinDisValue", 0)
            .Parameters.AddWithValue("@SB_DefaultStatus", BillDefaultStateCm.SelectedValue)
            .Parameters.AddWithValue("@PrintTBKsh", PrintTBKsh_CB.Checked)
            .Parameters.AddWithValue("@SB_TypeOf_Inc", SScreenDefault_Cmb.SelectedIndex)
            .Parameters.AddWithValue("@KshPrint", KshPrint_CB.Checked)
            .Parameters.AddWithValue("@OrderPage_ID", Orders_PageTypeCm.SelectedValue)
            .Parameters.AddWithValue("@ReceiptPage_ID", Receipts_PageTypeCm.SelectedValue)
            .Parameters.AddWithValue("@SB_Default_AG", Default_AG_cm.SelectedValue)
            .Parameters.AddWithValue("@isCostmerScreen", isCostmerScreen_CB.Checked)
            .Parameters.AddWithValue("@SScreenDefault", SScreenDefault_Cmb.SelectedIndex)
            .Parameters.AddWithValue("@AGBillPage_ID", AGBillType.SelectedValue)
            .Parameters.AddWithValue("@IM_Day_Valid", IM_Day_Valid_txt.Text)
            .Parameters.AddWithValue("@Use_Order", Ban_Expierd_IM_MV_cb.Checked)
            .Parameters.AddWithValue("@CP_Bill_Screen", CP_Bill_Screen_CB.Checked)
            .Parameters.AddWithValue("@Sales_Page_ID", Sales_Bill_Page_cm.SelectedValue)
            .Parameters.AddWithValue("@User_AutoPrintSB", User_AutoPrintSB_CB.Checked)
            .Parameters.AddWithValue("@is_SB_OutputScreen", is_Use_Total_Port_CB.Checked)
            .Parameters.AddWithValue("@SB_Default_ST", 1)
            .Parameters.AddWithValue("@POS_By_Shortcut", POS_IM_Default_CM.SelectedIndex)
            .Parameters.AddWithValue("@Open_NewBill_When_OpenSale", Open_NewBill_When_OpenSale_CB.Checked)
            .Parameters.AddWithValue("@SB_IM_Alert_When_Repetition", SB_IM_Alert_When_Repetition_CB.Checked)
            .Parameters.AddWithValue("@Discount_Distribute", Discount_Distribute_CB.Checked)
            .Parameters.AddWithValue("@Check_With_Thankes", SB_is_Check_Thankes_CB.Checked)
            .Parameters.AddWithValue("@PCH_TR_ID", PcshTrComboBox.SelectedValue)
            .Parameters.AddWithValue("@SB_TR_ID", SalesTrComboBox.SelectedValue)

            .Parameters.AddWithValue("@E_mail", E_mail_txt.Text)
            .Parameters.AddWithValue("@E_mail_Pass", E_mail_Pass_txt.Text)
            .Parameters.AddWithValue("@Host", Host_txt.Text)
            .Parameters.AddWithValue("@Port", Port_txt.Text)

            .Parameters.AddWithValue("@IM_min_QTY", IM_Min_Qty_cb.Checked)

            .Parameters.AddWithValue("@SB_ST_ID", SalesStoreComboBox.SelectedValue)
            .Parameters.AddWithValue("@PCH_ST_ID", PcshStoreComboBox.SelectedValue)
            .Parameters.AddWithValue("@EXP_ST_ID", ExpTrComboBox.SelectedValue)

            .Parameters.AddWithValue("@SB_ST_Can_change", SBST_CanChange_cm.Checked)
            .Parameters.AddWithValue("@PCH_ST_Can_change", PCHST_CanChange_cm.Checked)

            .Parameters.AddWithValue("@Rct_Tr_ID", Reciept_Tr_cmb.SelectedValue)
            .Parameters.AddWithValue("@Rct_Tr_Can_change", Recipt_Tr_CanChange_CB.Checked)
            '.Parameters.AddWithValue("@Allow_MinSP", Allow_MinSP_CB.Checked)
            .Parameters.AddWithValue("@SB_AutoOpenDrawer", SB_AutoOpenDrawer_CB.Checked)

            .Parameters.AddWithValue("@Pr_PrinterPage_Type", TypeOfPrint_Cmb.SelectedIndex)
            .Parameters.AddWithValue("@is_Multi_BAR", is_Multi_BAR_CB.Checked)
            .Parameters.AddWithValue("@Sales_ViewPage_ID", Sales_ViewBill_Page_cm.SelectedValue)
            .Parameters.AddWithValue("@IM_Default_Stut", IM_Type_cm.SelectedIndex)
            .Parameters.AddWithValue("@Def_U_ID", IM_Unit_cm.SelectedValue)

            .Parameters.AddWithValue("@is_Use_Total_Port", is_Use_Total_Port_CB.Checked)
            .Parameters.AddWithValue("@Total_Port", Total_Port_cm.Text)

            .Parameters.AddWithValue("@Ban_Expierd_IM_MV", Ban_Expierd_IM_MV_cb.Checked)
            .Parameters.AddWithValue("@Notif_IM_Sell_Less_Than_Cost", Notif_IM_Sell_Less_Than_Cost_CB.Checked)
            .Parameters.AddWithValue("@Barcode_DefPrinter", Barcode_DefPrinter_Cm.Text)
            .Parameters.AddWithValue("@Def_Befor_Print", Def_Befor_Print_CB.Checked)
            .Parameters.AddWithValue("@SB_is_Check_IM", SB_is_Check_IM_CB.Checked)


            .Parameters.AddWithValue("@SB_is_Copy", SB_is_Copy_CB.Checked)
            .Parameters.AddWithValue("@Copy_Printer_Path", Copy_Printer_Path_Cm.Text)

            .Parameters.AddWithValue("@is_Screen_Font_Resize", is_Screen_Font_Resize_cb.Checked)
            .Parameters.AddWithValue("@Font_Resize_Num", Font_Resize_Num_Txt.Text)


            .Parameters.AddWithValue("@Use_Inside_Barcode_CB", Use_Inside_Barcode_CB.Checked)
            .Parameters.AddWithValue("@Notif_IM_Negitane_QTY_CB", Notif_IM_Negitane_QTY_CB.Checked)
            .Parameters.AddWithValue("@Notif_If_SB_Has_No_SB_Price_CB", Notif_If_SB_Has_No_SB_Price_CB.Checked)

            .Parameters.AddWithValue("@Mizan_BarcodeFrom", DOT1_F_txt.Text)
            .Parameters.AddWithValue("@Mizan_BarcodeTo", DOT1_2_txt.Text)
            .Parameters.AddWithValue("@Mizan_QtyFrom", DOT2_F_txt.Text)
            .Parameters.AddWithValue("@Mizan_QtyTo", DOT2_T_txt.Text)
            .Parameters.AddWithValue("@isUseAsPhone_Crawler", isUseAsPhone_Crawler_CB.Checked)
            .Parameters.AddWithValue("@POS_IM_COUNT", POS_IM_COUNT_CM.Text)
        End With

        If SQL_SP_EXEC(c.Com) Then


            If Balance_Rd.Checked = True Then
                Second_Part_isPrice = 0
            Else
                Second_Part_isPrice = 1
            End If

            QTY_ALERT_SOUND = QTY_ALERT_SOUND_CB.Checked

            is_POS_Copy_2 = Copy_Printer_CB_2.Checked
            POS_Copy_2_Path = Copy_Printer_2_cm.Text

            Select Case Thread_Time_CM.SelectedIndex
                Case 0 : Thread_Time = 0

                Case 1 : Thread_Time = 60
                Case 2 : Thread_Time = 300

                Case 3 : Thread_Time = 1800
                Case 4 : Thread_Time = 3600
                Case 5 : Thread_Time = 7200
                Case 6 : Thread_Time = 14400
            End Select

            SB_Remove_Dec = SB_Remove_Dec_CB.Checked
            SB_IM_NEW_ROW = SB_IM_NEW_ROW_CB.Checked


            Manual_FRM_rpt = Manual_FRM_rpt_cm.SelectedIndex
            OutSale_rpt = OutSale_rpt_CM.SelectedIndex
            is_Home_Mange_Printers = is_Home_Mange_Printers_CB.Checked

            POS_BARCODE_TYPE = POS_BARCODE_TYPE_CM.Text

            MY_Settings.SERVER_IMG_PATH = SERVER_IMG_PATH_txt.Text

            MY_Settings.N_Point_Fter = N_Point_Fter_CM.Text


            Save_AppSetting()

            Try
                Dim FileToSaveAs As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, Application.StartupPath() & "\logo\logo.jpg")
                IMPictureBox.Image.Save(FileToSaveAs, System.Drawing.Imaging.ImageFormat.Jpeg)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try


            MsgBox("تم حفظ التعديلات", MsgBoxStyle.Information)
            Get_Computer_Setting()

            Me.Close()
        End If
    End Sub


    Private Sub isCostmerScreen_CB_CheckedChanged(sender As Object, e As EventArgs) Handles isCostmerScreen_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub IM_Day_Valid_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles IM_Day_Valid_txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub Use_Order_cb_CheckedChanged(sender As Object, e As EventArgs) Handles Ban_Expierd_IM_MV_cb.CheckedChanged
        CB_CHecked(sender)
        If Ban_Expierd_IM_MV_cb.Checked = False Then
            If BillDefaultStateCm.SelectedValue = 3 Then
                BillDefaultStateCm.SelectedValue = 2
            End If
        End If
    End Sub


    Private Sub BillDefaultStateComboBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles BillDefaultStateCm.SelectedValueChanged
        'On Error Resume Next
        'If BillDefaultStateComboBox.SelectedValue = 3 And Use_Order_cb.Checked = False Then
        '    BillDefaultStateComboBox.SelectedValue = 2
        'End If
    End Sub

    Private Sub CP_Bill_Screen_CB_CheckedChanged(sender As Object, e As EventArgs) Handles CP_Bill_Screen_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Emp_AutoPrint_CB_CheckedChanged(sender As Object, e As EventArgs) Handles User_AutoPrintSB_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub



    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub


    Private Sub SB_IM_Alert_When_Repetition_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SB_IM_Alert_When_Repetition_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Discount_Distribute_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Discount_Distribute_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SB_is_Check_Thankes_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SB_is_Check_Thankes_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub ChoasePicureButton_Click(sender As Object, e As EventArgs) Handles ChoasePicureButton.Click
        On Error Resume Next
        With Me.OpenFileDialog1
            .Filter = "(Image Files)|*.jpg;*.png;*.bmp;*.gif;*.ico|Jpg, | *.jpg|Png, | *.png|Bmp, | *.bmp|Gif, | *.gif|Ico | *.ico"
            .FilterIndex = 1
            .Multiselect = False
            .Title = "حدد شعار الشركة"
            .ShowDialog()
            If Len(.FileName) > 0 Then
                IMPictureBox.Image = Image.FromFile(OpenFileDialog1.FileName)
            End If
        End With
    End Sub

    Private Sub NoPictureButton_Click(sender As Object, e As EventArgs) Handles NoPictureButton.Click
        IMPictureBox.Image = My.Resources.white
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim webAddress As String = LinkLabel1.Text
        Process.Start(webAddress)
    End Sub

    Private Sub ShowPassButton_MouseHover(sender As Object, e As EventArgs) Handles ShowPassButton.MouseHover
        E_mail_Pass_txt.UseSystemPasswordChar = False
    End Sub

    Private Sub ShowPassButton_MouseLeave(sender As Object, e As EventArgs) Handles ShowPassButton.MouseLeave
        E_mail_Pass_txt.UseSystemPasswordChar = True
    End Sub

    Private Sub E_mail_Pass_txt_TextChanged(sender As Object, e As EventArgs) Handles E_mail_Pass_txt.TextChanged
        If E_mail_Pass_txt.Text.Count > 0 Then
            ShowPassButton.Visible = True
        Else
            ShowPassButton.Visible = False
        End If
    End Sub


    Private Sub IM_Min_Qty_cb_CheckedChanged(sender As Object, e As EventArgs) Handles IM_Min_Qty_cb.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SBST_CanChange_cm_CheckedChanged(sender As Object, e As EventArgs) Handles SBST_CanChange_cm.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub PCHST_CanChange_cm_CheckedChanged(sender As Object, e As EventArgs) Handles PCHST_CanChange_cm.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Recipt_Tr_CanChange_cm_CheckedChanged(sender As Object, e As EventArgs) Handles Recipt_Tr_CanChange_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    'Private Sub SP_MinSP_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Allow_MinSP_CB.CheckedChanged
    '    CB_CHecked(sender)
    'End Sub

    Private Sub SB_AutoOpenDrawer_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SB_AutoOpenDrawer_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub


    Private Sub is_Multi_BAR_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_Multi_BAR_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub is_PriceOutputScreen_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_Use_Total_Port_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SScreenDefault_Cmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SScreenDefault_Cmb.SelectedIndexChanged
        If SScreenDefault_Cmb.SelectedIndex <> 1 Then
            Touch_StatusGB.Visible = False
            Touch_Bill_GB.Visible = False
            ' Use_Order_cb.Visible = False
            Sells_Bill_GB.Visible = True
            Copy_Print_Panel.Visible = False
        Else
            Sells_Bill_GB.Visible = False
            Touch_StatusGB.Visible = True
            Touch_Bill_GB.Visible = True
            Copy_Print_Panel.Visible = True
            ' Use_Order_cb.Visible = True
        End If

        If SScreenDefault_Cmb.SelectedIndex = 2 Then
            Notif_If_SB_Has_No_SB_Price_CB.Visible = True
        Else
            Notif_If_SB_Has_No_SB_Price_CB.Visible = False
        End If


    End Sub

    Private Sub Def_Befor_Print_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Def_Befor_Print_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SB_is_Check_IM_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SB_is_Check_IM_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SB_is_Copy_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SB_is_Copy_CB.CheckedChanged
        CB_CHecked(sender)
        If SB_is_Copy_CB.Checked = True Then
            Copy_Printer_Path_Cm.Enabled = True
        Else
            Copy_Printer_Path_Cm.Enabled = False
        End If
    End Sub



    Private Sub Resize_Txt_KeyPress(sender As Object, e As KeyPressEventArgs)
        Check_Only_Float(sender, e)
    End Sub


    Private Sub is_Screen_Resize_cb_CheckedChanged(sender As Object, e As EventArgs) Handles is_Screen_Font_Resize_cb.CheckedChanged
        CB_CHecked(sender)
        If is_Screen_Font_Resize_cb.Checked = True Then
            Font_Resize_Num_Txt.Enabled = True
        Else
            Font_Resize_Num_Txt.Enabled = False
        End If
    End Sub


    Private Sub Use_Inside_Barcode_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Use_Inside_Barcode_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Notif_IM_Negitane_QTY_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Notif_IM_Negitane_QTY_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Notif_If_SB_Has_No_SB_Name_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Notif_If_SB_Has_No_SB_Price_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Mizan_BarcodeFrom_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DOT1_F_txt.KeyPress, DOT1_2_txt.KeyPress, DOT2_F_txt.KeyPress, DOT2_T_txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub isUseAsPhone_Crawler_CB_CheckedChanged(sender As Object, e As EventArgs) Handles isUseAsPhone_Crawler_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub


    Private Sub Balance_Rd_CheckedChanged(sender As Object, e As EventArgs) Handles Balance_Rd.CheckedChanged, Price_Rd.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub QTY_ALERT_SOUNDCB_CheckedChanged(sender As Object, e As EventArgs) Handles QTY_ALERT_SOUND_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SB_Remove_Dec_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SB_Remove_Dec_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Open_NewBill_When_OpenSale_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Open_NewBill_When_OpenSale_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SB_IM_NEW_ROW_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SB_IM_NEW_ROW_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub is_Home_Mange_Printers_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_Home_Mange_Printers_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SelectColorButton_Click(sender As Object, e As EventArgs) Handles SelectColorButton_DEBIT.Click
        Using colorDialog As New ColorDialog()
            ' Optionally, set the initial color.
            'colorDialog.Color = Me.BackColor

            colorDialog.AllowFullOpen = True
            colorDialog.SolidColorOnly = False

            ' Show the color dialog and check if the user clicks OK.
            If colorDialog.ShowDialog() = DialogResult.OK Then
                ' Apply the selected color to a control (e.g., form background).
                DEBIT_COLOR_Panel.BackColor = colorDialog.Color

                MY_Settings.DEBIT_COLOR = ColorTranslator.ToHtml(colorDialog.Color)

                MessageBox.Show("Selected color: " & colorDialog.Color.ToString())
            End If
        End Using
    End Sub

    Private Sub SelectColorButton_CREDIT_Click(sender As Object, e As EventArgs) Handles SelectColorButton_CREDIT.Click
        Using colorDialog As New ColorDialog()
            ' Optionally, set the initial color.
            'colorDialog.Color = Me.BackColor

            colorDialog.AllowFullOpen = True
            colorDialog.SolidColorOnly = False

            ' Show the color dialog and check if the user clicks OK.
            If colorDialog.ShowDialog() = DialogResult.OK Then
                ' Apply the selected color to a control (e.g., form background).
                CREDIT_COLOR_Panel.BackColor = colorDialog.Color

                MY_Settings.CREDIT_COLOR = ColorTranslator.ToHtml(colorDialog.Color)

                MessageBox.Show("Selected color: " & colorDialog.Color.ToString())
            End If
        End Using
    End Sub
End Class