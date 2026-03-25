Module Identifiers2



    '----------
    Public Switch_To_Cash As Boolean = False
    Public SB_is_Fast As Boolean = False
    Public Receipt_is_By_Travel_Menu As Boolean = False
    '----------------------------------------
    'CP Setting :
    Public isBackup As Boolean = False
    Public BackupPath As String = ""
    Public Default_Printer_80 As String = ""
    Public isDiscount As Boolean = False
    Public BackupPath_2 As String = ""
    Public isBackupPath2 As Boolean = False
    Public isPrintAfterCash As Boolean = True
    Public MinimumDiscountValue As Double = 0
    Public SB_DefaultStatus As Integer = 1
    Public SB_AutoPrint As Boolean = False
    Public Default_Printer_A4 As String = ""
    Public Default_Barcode_Printer As String = ""
    Public Copy_Printer As String = ""
    Public isShowPrintBtn As Boolean = False
    Public PrintTBKsh As Boolean = False
    Public BackupPath_OnExit As String = ""
    Public OrderPage_ID As Integer = 1
    Public ReceiptPage_ID As Integer = 1
    Public AGBillPage_ID As Integer = 1
    Public Sales_Page_ID As Integer = 1
    Public Sales_ViewPage_ID As Integer = 1
    Public IM_Day_Valid As Integer = 0
    Public User_AutoPrintSB As Boolean = False

    Public IM_Default_Stut As Integer = 0

    Public SB_ST_ID As Integer = 1
    Public PCH_ST_ID As Integer = 1

    Public SB_ST_Can_change As Boolean = False
    Public PCH_ST_Can_change As Integer = False

    Public OrderBill_AG_Bill_Track As String = ""
    Public OrderBill_IN_Bill_Track As String = ""
    Public AGBillPage_Bill_Track As String = ""
    Public Sales_BillPage_Bill_Track As String = ""
    Public Sales_ViewPage_Bill_Track As String = ""
    Public Receipt_Track As String = ""
    Public SB_Default_AG As Integer = 1
    Public ImageScannerPath As String = ""
    Public isCostmerScreen As Boolean = False
    Public SScreenDefault As Integer = -1
    Public is_Screen_Font_Resize As Boolean = False
    'Public is_Use_Order As Boolean = False
    Public CP_Bill_Screen As Boolean = False
    Public Font_Resize_Num As Double = False

    Public Use_Inside_Barcode As Boolean = False
    Public Notif_IM_Negitane_QTY As Boolean = False
    Public Notif_If_SB_Has_No_SB_Price As Boolean = False

    Public User_POS As Boolean = True
    Public is_SB_OutputScreen As Boolean = False
    ' Public SB_Default_ST As Integer = 0
    Public POS_By_Shortcut As Integer = 0
    Public Open_NewBill_When_OpenSale As Boolean = False
    Public SB_IM_Alert_When_Repetition As Boolean = False
    Public Discount_Distribute As Boolean = False
    Public SB_is_Check_Thankes As Boolean = False

    Public PCH_TR_ID As Integer = 1
    Public SB_TR_ID As Integer = 1
    Public Rct_Tr_ID As Integer = 1
    Public EXP_TR_ID As Integer = 1

    Public Org_PCH_TR_ID As Integer = 1
    Public Org_SB_TR_ID As Integer = 1
    Public Org_Rct_Tr_ID As Integer = 1
    Public Org_EXP_TR_ID As Integer = 1


    Public E_mail As String
    Public E_mail_Password As String
    Public E_mail_Host As String
    Public E_mail_Port As String

    Public IM_min_QTY As Boolean = False


    Public Rct_Tr_Can_change As Boolean
    Public S_Allow_MinSP As Boolean
    Public S_Exp_Pch As Boolean

    '------------------------------------------------
    Public SB_AutoOpenDrawer As Boolean = False
    Public Pr_PrinterPage_Type As Integer
    Public S_is_Multi_BAR As Boolean = False
    Public Def_U_ID As Integer = 2
    Public is_Use_Total_Port As Boolean = False
    Public Total_Port As String = ""
    Public Ban_Expierd_IM_MV As Boolean = False
    Public Notif_IM_Sell_Less_Than_Cost As Boolean = False

    Public Mizan_BarcodeFrom As Integer = 0
    Public Mizan_BarcodeTo As Integer = 6
    Public Mizan_QtyFrom As Integer = 7
    Public Mizan_QtyTo As Integer = 11

    Public isUseAsPhone_Crawler As Boolean = False
    Public POS_IM_COUNT As Integer = 36
    '-------------------------------------------------
    Public S_Tables As Boolean = False
    Public S_Pr As Boolean = False
    Public S_SubPrints As Boolean = False
    Public S_TB_Auto_Print As Boolean = False

    Public S_Notes As Boolean = False
    Public S_Orders As Boolean = False

    Public S_Stores As Boolean = False
    Public S_Frm As Boolean = False

    Public S_In_Out_side As Boolean = False
    Public S_Marketers As Boolean = False

    Public S_SerialCode As Boolean = False
    Public S_Out_Travel As Boolean = False
    Public S_AgentCard As Boolean = False

    Public POS_ENTER_AS_ORDER As Boolean = False
    Public Valid_Allow_IM As Boolean = True

    Public Cr_Type_ID As Integer = 2
    Public Suply_Type_ID As Integer = 3
    Public Emp_Type_ID As Integer = 4
    Public General_AG_Type_ID As Integer = 1
    Public Card_AG_Type_ID As Integer = 5
    'Public is_Pch_Discount_Distribute As Boolean = False

    Public is_Add_New_IM As Boolean = False

    Public S_Phone_For_Tables As Boolean = False
    Public S_Auto_Recive_ST_Tran As Boolean = False
    Public S_IM_Valid As Boolean = False

    Public GLOBAL_IM_ID As Integer = 0
    'Public SYS_ERROR As Boolean = False

    Public Receipt_Tran_ID As Integer = 0

    Public S_Sub_Code As String = ""
    Public Serv_Desc As String = ""
    Public START_ID As Double = 0
    Public END_ID As Double = 0

    Public Default_AG_ID As Integer = 1

    Public is_IM_Use_Rsv As Boolean


    Public IM_Dt_Barcodes As New DataTable
    Public IM_Dt As New DataTable
    Public IM_Units_Dt As New DataTable
    Public EXP_Dt As New DataTable

End Module
