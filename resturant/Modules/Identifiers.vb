Module Identifiers
    'User Info :
    Public USER_ID As Integer
    Public User_isAdmin As Boolean
    Public USER_NAME As String
    Public isPhone_User As Boolean = False
    Public Default_TB_ID As Integer = 0
    Public U_Flate_ID As Integer = 0
    Public U_AG_ID As Integer = 0
    Public Str_Name As String = ""

    Public isShowing_Trans As Boolean = False
    Public T_ID_Trans As Integer
    Public Str_ As String
    '--------------------
    'Period Info
    Public Pr_ID As Integer = 1
    Public isPr_Open As Boolean
    '-------------------
    'Table Pied Info :
    Public TB_Part_Pied As Boolean = False
    '-------------------
    'Table Pied Info :
    Public POS_Search As Boolean = False
    '--------------------
    'computer Setting :
    Public SBill_Title_1 As String = ""
    Public SBill_Title_2 As String = ""
    Public SBill_Footer As String = ""


    ' Public SB_TypeOf_Inc As Integer = 0
    Public U_SalesVoid As Boolean = True
    Public U_SalesDis As Boolean = True
    Public U_Add_Draw_St As Boolean = True
    Public U_Balance As Boolean = True
    Public U_Cancel_Pch As Boolean = True
    Public U_ADD_Pch As Boolean = True
    Public U_Update_IM_QTY As Boolean = True
    Public KshPrint As Boolean = True
    Public U_StExplore As Boolean = True
    Public U_ExpVoid As Boolean = True
    ' Public U_Exp As Boolean = True
    Public U_SB_Update As Boolean = True
    Public U_Pch_Update As Boolean = True
    Public U_SB_Rtn As Boolean = True
    Public U_Pch_Rtn As Boolean = True
    Public U_End_Table As Boolean = True
    Public U_Transfer_Table As Boolean = True
    Public U_SB_IM_Update As Boolean = True
    Public U_SB_Sell_Under_Cost As Boolean = True
    Public U_Sell_Under_Min_SP As Boolean = True
    Public U_Sell_Under_Min_SP_2 As Boolean = True

    Public U_SB_Show_Price_Info As Boolean = True
    Public U_SB_Show_Cash As Boolean = True
    Public U_SB_Show_IM_COST As Boolean = True

    Public U_Update_Receipt As Boolean = True
    Public U_Cancel_Receipt As Boolean = True
    Public U_ExpEdit As Boolean = True
    Public U_AG_Skip_Max As Boolean = True
    Public U_Show_Bill_Profet As Boolean = True
    Public U_Save_otherBill As Boolean = True


    Public U_Tr_Widraw As Boolean = True
    Public U_Tr_Deposit As Boolean = True
    Public U_Tr_Convert As Boolean = True
    Public U_Hide_Unrelated_Tr As Boolean = True

    '--------------------
    'Forms :
    Public F_POS As POS
    Public F_BillsMenu As BillsMenu
    Public F_Search_Phone_Bills As Search_Phone_Bills
    Public F_SalesMenu As SalesTypes
    Public F_SysOptions As SysOptions
    Public F_GeneralMenu As GeneralMenu
    Public F_ItemsMenu As ItemsMenu
    Public F_Notes As Notes
    Public F_Periods As Periods
    Public F_MainForm As MainForm
    Public F_PeriodsNotes As PeriodsNotes
    Public F_SalesReport As POS_Report
    Public F_SB_Edit As SB_Edit
    Public F_Printers As Printers
    Public F_Backup As Backup
    Public F_users As users
    Public F_SysDelete As SysDelete
    Public F_Units As Units
    Public F_Agent As Agent
    Public F_AgentsMenu As AgentsMenu
    Public F_TablesCard As TablesCard
    Public F_AddFastAgent As AddFastAgent
    Public F_OrderOptions As OrderOptions
    Public F_OrdersMenu As OrdersMenu
    Public F_Receipt As Receipt
    Public F_SB_TablesMenu As SB_TablesMenu
    Public F_TablesBalance As TablesBalance
    Public F_Pch As Pch
    Public F_ExpencesCard As EXP_Card
    Public F_TB_BillIM As TB_BillIM
    Public F_EXP_Details As EXP_Details
    Public F_Format_Items_Auto As Format_Items_Auto
    Public F_ViewBill As ViewBill
    Public F_Inside_Sales As Inside_Sales
    Public F_Outside_Sales As Outside_Sales
    Public F_Exp_Static As Exp_Static
    Public F_Format_Items_Manual As Format_Items_Manual
    Public F_Balances As Balances
    Public F_BillNotes As BillNotes
    Public F_Stores_ImmediateOrder As Stores_ImmediateOrder
    Public F_STORES As STORES
    Public F_ChangeUnit As ChangeUnit
    Public F_TreasuryCard As Tr_Card
    Public F_Tr_Deposit_Withdraw As Tr_Deposit_Withdraw
    Public F_Tr_Transfers As Tr_Transfers
    Public F_SBChangePrice As SBChangePrice
    Public F_STORES_Explorer As STORES_Explorer
    Public F_IM_Execute As IM_Execute
    Public F_Invoice As Invoice
    Public F_Sales As Sales
    Public F_IM_Valid As IM_Valid
    Public F_Custody As Custody

    Public F_Pch_IM_card As Pch_IM_card_11
    Public F_Invoice_IM_card As Invoice_IM_card
    Public F_SB_IM_card As SB_IM_card
    Public F_IMEX_IM_card As IMEX_IM_card
    Public F_Stores_Trans_IM_card As Stores_Trans_IM_card
    Public F_Frm_manuel_IM_card As Frm_manuel_IM_card
    Public F_Frm_auto_IM_card As Frm_auto_IM_card
    '--------------------
    ' Public SqlConnection As String = ""


    Public S_ProductCode As String = ""
    Public Date_Serial As String = ""
    Public Random_Serial As String = ""
    '----------------------------------
    Public AG_Type As Integer
    Public FormType As Integer = 0 ' 1: Sales , 2:Pch , 3:IMEX , 4:Invoice , 5:Sales_Retuns , 6:Pch_Returns , 7:IM STORE CONVERT , 8:EXP_Details , 9:FORMAT ITEM , 10:View Bill , 11:Inside Sale , 12:Custody , 13:Outside Sale 
    Public TR_Type As Integer ' 1: Widraw , 2:Deposit , 3:Convert 
    Public isOrder As Boolean = False
    Public isViewOrder As Boolean = False
    Public Def_Befor_Print As Integer = 0
    Public SB_is_Check_IM As Boolean = False
    Public SB_is_Copy_Print As Boolean = False

    Public StdDisPercent As Double = 0
    Public Points_Sale As Double = 0
    Public Point_Inc As Double = 0
    Public PrevDisPercent As Double = 0

    '-----------------------------------------
    Public Sales_AG_ID As Integer
    Public Sales_AG_Name As String

    Public Home_Panel As String = ""


    Public TOTAL, Disc, Pure As Double
    Public T_ID, SB_ID As Integer

    Public System_Start_Normal As Boolean = True
    'Public System_Start_Error As String = True

End Module
