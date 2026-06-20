Imports System.IO

Public Module MY_Settings
    Public S_Total_CL As Boolean = True
    Public Server_Choese_Server As String = ""
    Public S_IMNUM_CL As Boolean = True
    Public AttachDbFilename As String = ""
    Public ST_GM_Name As Boolean = True
    Public is_SubSys As Boolean = False
    Public App_Suuply As String = "CLASS"
    Public DB_Authentication As String = 0
    Public SqlConStr As String = ""
    Public isCenterSys As Boolean = False
    Public S_Default As String = 1
    Public POS_Search_Type As String = 1
    Public ST_Last_Pch_Price As Boolean = True
    Public SP_Notes_CL As Boolean = True
    Public BarcodeWidth As String = 5.08
    Public IMPR_BAR As Boolean = False
    Public S_Barcode_CL As Boolean = False
    Public BarcodeHieght As String = 2
    Public S_Serial_Code_CL As Boolean = False
    Public IMPR_MINSP As Boolean = False
    Public IMPR_MINSP_2 As Boolean = False
    Public S_IMUnit_CL As Boolean = True
    Public SB_AG_Show_Balance As Boolean = False
    Public BarcodePLeft As String = 0.4
    Public DB_Choese_Server As String = "Tree"
    Public S_SERVER As String = "localhost"
    Public S_Price_CL As Boolean = True
    Public S_D_Valid_CL As Boolean = True
    Public BarcodeBUp As String = 0.4
    Public ST_STNAME As Boolean = True
    Public BarcodeNumber As String = 1
    Public BarcodePUp As String = 0.4
    Public Call_IM_After_Insert_CB As Boolean = True
    Public S_OpenNextBill As Boolean = False
    Public DB_Pass As String = ""
    Public BR_Print_IMName As Boolean = False
    Public DB_UName As String = ""
    Public Search_By_Bar_Rtn As Boolean = False
    Public Cpu_ID As String = ""
    Public ST_IM_Num As Boolean = True
    Public Server_Desc As String = ""
    Public S_Project_CL As Boolean = False
    Public BarcodeColumn As String = 1
    Public SERVER_IP As String = ""
    Public MAINFORM_BK As String = ""
    Public IsAttachDB As Boolean = False
    Public IMPR_IMNUM As Boolean = False
    Public S_ST_Name_CL As Boolean = True
    Public Order_No_Deliver_Date As Boolean = True
    Public Order_Search_Type As Boolean = False
    Public S_CodeAdd_1 As Boolean = False
    Public Pr_Printer_isShow As Boolean = False
    Public BarcodeCheckA4 As Boolean = False
    Public DataBase As String = "Tree"
    Public is_ByBarInput As Boolean = False
    Public ST_Valid As Boolean = False
    Public S_Date_CL As Boolean = True
    Public BarcodeRows As String = 1
    Public ShowCname As Boolean = False
    Public AG_SH_Bill_Type As String = 0
    Public BR_Print_IMPrice As Boolean = False
    Public SB_Sch_With_QTY As Boolean = False
    Public ShowIM_Price_On_Barcode As Boolean = True
    Public IM_Search_GM_ID As Integer = 0
    Public IM_Use_Out_KB As Boolean = False
    Public Tables_Flate_ID As Integer = 0
    Public Second_Part_isPrice As Integer = 0
    Public Print_TB_Before_End As Boolean = True
    Public QTY_ALERT_SOUND As Boolean = False
    Public SB_Show_Bill As Boolean = False
    Public SB_Show_Bill_Rest As Boolean = False
    Public SB_Show_SumPied As Boolean = False
    Public Online_Con_Str As String = ""
    Public is_POS_Copy_2 As Boolean = False
    Public POS_Copy_2_Path As String = ""
    Public S_IM_NOTE_CL As Boolean = False
    Public ShowIM_IM_NAME_On_Barcode As Boolean = True
    Public ShowIM_IM_NUM_On_Barcode As Boolean = False
    Public SALES_TYPES_CMB As Integer = 0
    Public Thread_Time As Integer = 0
    Public SB_Remove_Dec As Boolean = False
    Public SB_IM_NEW_ROW As Boolean = True
    Public GM_ID_Selected As Integer = 1
    Public SBill_Title_1 As String = ""
    Public SBill_Title_2 As String = ""

    Public ORG_ST_ACC_CODE As String = "124"
    Public Income_ST_ACC_CODE As String = "315"


    Public ACC_LEVEL_SEARCH As String = 4
    Public AG_Show_Balance_in_Receipt As Boolean = True
    Public is_Link_With_SB As Boolean = False
    Public SALES_DB As String = ""
    Public is_Dark_mode As Boolean = False
    Public Use_State_Budget As Boolean = False
    Public Allow_Budget_OverSpend As Boolean = False
    Public Default_Stamp_Percent As Decimal = 0D
    Public Default_Stamp_Account_Code As String = ""
    Public is_Print_ACC_B_Letters As Boolean = False
    Public ACC_B_printer_Type As Integer = 1
    Public YEAR_TO_MOVE_ACC As Integer = 0

    Public is_Search_By_Levels As Boolean = True

    '---------------------------------------------------------------------------------------------------------------------------------

    Public Sub ExportButton_Setting_ToFile()
        Try
            '" & My.Computer.Name & "_B
            Dim path As String = Application.StartupPath & "\Setting\Accounting\BackUpSettings.AppSettings"
            Using sWriter As New StreamWriter(path)
                'For Each setting As Configuration.SettingsPropertyValue In My_Settings.PropertyValues

                sWriter.WriteLine("S_Total_CL" & ":" & S_Total_CL.ToString())
                sWriter.WriteLine("Server_Choese_Server" & ":" & Server_Choese_Server.ToString())
                sWriter.WriteLine("S_IMNUM_CL" & ":" & S_IMNUM_CL.ToString())
                sWriter.WriteLine("AttachDbFilename" & ":" & AttachDbFilename.ToString())
                sWriter.WriteLine("ST_GM_Name" & ":" & ST_GM_Name.ToString())
                sWriter.WriteLine("is_SubSys" & ":" & is_SubSys.ToString())
                sWriter.WriteLine("App_Suuply" & ":" & App_Suuply.ToString())
                sWriter.WriteLine("DB_Authentication" & ":" & DB_Authentication.ToString())
                sWriter.WriteLine("SqlConStr" & ":" & SqlConStr.ToString())
                sWriter.WriteLine("isCenterSys" & ":" & isCenterSys.ToString())
                sWriter.WriteLine("S_Default" & ":" & S_Default.ToString())
                sWriter.WriteLine("POS_Search_Type" & ":" & POS_Search_Type.ToString())
                sWriter.WriteLine("ST_Last_Pch_Price" & ":" & ST_Last_Pch_Price.ToString())
                sWriter.WriteLine("SP_Notes_CL" & ":" & SP_Notes_CL.ToString())
                sWriter.WriteLine("BarcodeWidth" & ":" & BarcodeWidth.ToString())
                sWriter.WriteLine("IMPR_BAR" & ":" & IMPR_BAR.ToString())
                sWriter.WriteLine("S_Barcode_CL" & ":" & S_Barcode_CL.ToString())
                sWriter.WriteLine("BarcodeHieght" & ":" & BarcodeHieght.ToString())
                sWriter.WriteLine("S_Serial_Code_CL" & ":" & S_Serial_Code_CL.ToString())
                sWriter.WriteLine("IMPR_MINSP" & ":" & IMPR_MINSP.ToString())
                sWriter.WriteLine("IMPR_MINSP_2" & ":" & IMPR_MINSP_2.ToString())
                sWriter.WriteLine("S_IMUnit_CL" & ":" & S_IMUnit_CL.ToString())
                sWriter.WriteLine("SB_AG_Show_Balance" & ":" & SB_AG_Show_Balance.ToString())
                sWriter.WriteLine("BarcodePLeft" & ":" & BarcodePLeft.ToString())
                sWriter.WriteLine("DB_Choese_Server" & ":" & DB_Choese_Server.ToString())
                sWriter.WriteLine("S_SERVER" & ":" & S_SERVER.ToString())
                sWriter.WriteLine("S_Price_CL" & ":" & S_Price_CL.ToString())
                sWriter.WriteLine("S_D_Valid_CL" & ":" & S_D_Valid_CL.ToString())
                sWriter.WriteLine("BarcodeBUp" & ":" & BarcodeBUp.ToString())
                sWriter.WriteLine("ST_STNAME" & ":" & ST_STNAME.ToString())
                sWriter.WriteLine("BarcodeNumber" & ":" & BarcodeNumber.ToString())
                sWriter.WriteLine("BarcodePUp" & ":" & BarcodePUp.ToString())
                sWriter.WriteLine("Call_IM_After_Insert_CB" & ":" & Call_IM_After_Insert_CB.ToString())
                sWriter.WriteLine("S_OpenNextBill" & ":" & S_OpenNextBill.ToString())
                sWriter.WriteLine("DB_Pass" & ":" & DB_Pass.ToString())
                sWriter.WriteLine("BR_Print_IMName" & ":" & BR_Print_IMName.ToString())
                sWriter.WriteLine("DB_UName" & ":" & DB_UName.ToString())
                sWriter.WriteLine("Search_By_Bar_Rtn" & ":" & Search_By_Bar_Rtn.ToString())
                sWriter.WriteLine("Cpu_ID" & ":" & Cpu_ID.ToString())
                sWriter.WriteLine("ST_IM_Num" & ":" & ST_IM_Num.ToString())
                sWriter.WriteLine("Server_Desc" & ":" & Server_Desc.ToString())
                sWriter.WriteLine("S_Project_CL" & ":" & S_Project_CL.ToString())
                sWriter.WriteLine("BarcodeColumn" & ":" & BarcodeColumn.ToString())
                sWriter.WriteLine("SERVER_IP" & ":" & SERVER_IP.ToString())
                sWriter.WriteLine("MAINFORM_BK" & ":" & MAINFORM_BK.ToString())
                sWriter.WriteLine("IsAttachDB" & ":" & IsAttachDB.ToString())
                sWriter.WriteLine("IMPR_IMNUM" & ":" & IMPR_IMNUM.ToString())
                sWriter.WriteLine("S_ST_Name_CL" & ":" & S_ST_Name_CL.ToString())
                sWriter.WriteLine("Order_No_Deliver_Date" & ":" & Order_No_Deliver_Date.ToString())
                sWriter.WriteLine("Order_Search_Type" & ":" & Order_Search_Type.ToString())
                sWriter.WriteLine("S_CodeAdd_1" & ":" & S_CodeAdd_1.ToString())
                sWriter.WriteLine("Pr_Printer_isShow" & ":" & Pr_Printer_isShow.ToString())
                sWriter.WriteLine("BarcodeCheckA4" & ":" & BarcodeCheckA4.ToString())
                sWriter.WriteLine("DataBase" & ":" & DataBase.ToString())
                sWriter.WriteLine("is_ByBarInput" & ":" & is_ByBarInput.ToString())
                sWriter.WriteLine("ST_Valid" & ":" & ST_Valid.ToString())
                sWriter.WriteLine("S_Date_CL" & ":" & S_Date_CL.ToString())
                sWriter.WriteLine("BarcodeRows" & ":" & BarcodeRows.ToString())
                sWriter.WriteLine("ShowCname" & ":" & ShowCname.ToString())
                sWriter.WriteLine("AG_SH_Bill_Type" & ":" & AG_SH_Bill_Type.ToString())
                sWriter.WriteLine("BR_Print_IMPrice" & ":" & BR_Print_IMPrice.ToString())
                sWriter.WriteLine("SB_Sch_With_QTY" & ":" & SB_Sch_With_QTY.ToString())
                sWriter.WriteLine("ShowIM_Price_On_Barcode" & ":" & ShowIM_Price_On_Barcode.ToString())
                sWriter.WriteLine("IM_Search_GM_ID" & ":" & IM_Search_GM_ID.ToString())
                sWriter.WriteLine("IM_Use_Out_KB" & ":" & IM_Use_Out_KB.ToString())
                sWriter.WriteLine("Tables_Flate_ID" & ":" & Tables_Flate_ID.ToString())
                sWriter.WriteLine("Second_Part_isPrice" & ":" & Second_Part_isPrice.ToString())
                sWriter.WriteLine("Print_TB_Before_End" & ":" & Print_TB_Before_End.ToString())
                sWriter.WriteLine("QTY_ALERT_SOUND" & ":" & QTY_ALERT_SOUND.ToString())
                sWriter.WriteLine("SB_Show_Bill" & ":" & SB_Show_Bill.ToString())
                sWriter.WriteLine("SB_Show_Bill_Rest" & ":" & SB_Show_Bill_Rest.ToString())
                sWriter.WriteLine("SB_Show_SumPied" & ":" & SB_Show_SumPied.ToString())
                sWriter.WriteLine("Online_Con_Str" & ":" & Online_Con_Str.ToString())

                sWriter.WriteLine("is_POS_Copy_2" & ":" & is_POS_Copy_2.ToString())
                sWriter.WriteLine("POS_Copy_2_Path" & ":" & POS_Copy_2_Path.ToString())
                sWriter.WriteLine("S_IM_NOTE_CL" & ":" & S_IM_NOTE_CL.ToString())

                sWriter.WriteLine("ShowIM_IM_NAME_On_Barcode" & ":" & ShowIM_IM_NAME_On_Barcode.ToString())
                sWriter.WriteLine("ShowIM_IM_NUM_On_Barcode" & ":" & ShowIM_IM_NUM_On_Barcode.ToString())

                sWriter.WriteLine("SALES_TYPES_CMB" & ":" & SALES_TYPES_CMB.ToString())
                sWriter.WriteLine("Thread_Time" & ":" & Thread_Time.ToString())
                sWriter.WriteLine("SB_Remove_Dec" & ":" & SB_Remove_Dec.ToString())

                sWriter.WriteLine("SB_IM_NEW_ROW" & ":" & SB_IM_NEW_ROW.ToString())
                sWriter.WriteLine("GM_ID_Selected" & ":" & GM_ID_Selected.ToString())

                sWriter.WriteLine("SBill_Title_1" & ":" & SBill_Title_1.ToString())
                sWriter.WriteLine("SBill_Title_2" & ":" & SBill_Title_2.ToString())

                sWriter.WriteLine("ORG_ST_ACC_CODE" & ":" & ORG_ST_ACC_CODE.ToString())
                sWriter.WriteLine("Income_ST_ACC_CODE" & ":" & Income_ST_ACC_CODE.ToString())

                sWriter.WriteLine("Pure_Income_ACC_CODE" & ":" & Pure_Income_ACC_CODE.ToString())

                sWriter.WriteLine("ACC_LEVEL_SEARCH" & ":" & ACC_LEVEL_SEARCH.ToString())
                sWriter.WriteLine("AG_Show_Balance_in_Receipt" & ":" & AG_Show_Balance_in_Receipt.ToString())

                sWriter.WriteLine("is_Link_With_SB" & ":" & is_Link_With_SB.ToString())
                sWriter.WriteLine("SALES_DB" & ":" & SALES_DB.ToString())
                sWriter.WriteLine("is_Dark_mode" & ":" & is_Dark_mode.ToString())
                sWriter.WriteLine("Use_State_Budget" & ":" & Use_State_Budget.ToString())
                sWriter.WriteLine("Allow_Budget_OverSpend" & ":" & Allow_Budget_OverSpend.ToString())
                sWriter.WriteLine("Default_Stamp_Percent" & ":" & Default_Stamp_Percent.ToString())
                sWriter.WriteLine("Default_Stamp_Account_Code" & ":" & Default_Stamp_Account_Code.ToString())

                sWriter.WriteLine("is_Print_ACC_B_Letters" & ":" & is_Print_ACC_B_Letters.ToString())
                sWriter.WriteLine("ACC_B_printer_Type" & ":" & ACC_B_printer_Type.ToString())
                sWriter.WriteLine("YEAR_TO_MOVE_ACC" & ":" & ACC_B_printer_Type.ToString())
                sWriter.WriteLine("is_Search_By_Levels" & ":" & is_Search_By_Levels.ToString())


                'Next

            End Using

            'My_Settings.Save()
            ' MessageBox.Show("تم اخذ نسخة احتياطية من الاعدادات", "النظام", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            'Logger.Log(ex, "", "", System.Reflection.MethodBase.GetCurrentMethod().Name)
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Recover_File_Setting()
        Try
            Dim path As String = Application.StartupPath & "\Setting\Accounting\BackUpSettings.AppSettings"
            If File.Exists(path) Then

                Using sReader As New StreamReader(path)

                    While sReader.Peek() > 0
                        Try
                            Dim input = sReader.ReadLine()
                            ' Split comma delimited data ( SettingName,SettingValue )  
                            Dim dataSplit = input.Split(CChar(":"))
                            Check_Setting(dataSplit(0), dataSplit(1))
                        Catch ex As Exception
                            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "BackUpSettings")
                        End Try
                    End While
                End Using

                'MessageBox.Show("تم تحميل الاعدادات بنجاح ... قم بالخروج من الظام تم قم بالدخول مجددا", "النظام", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Application.Exit()
                'Application.ExitThread()

            Else
                ExportButton_Setting_ToFile()
                'MessageBox.Show("لم يتم ايجاد ملف الاعدادات الاحتياطي ... فشل استعادة الاعدادات", "النظام", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("يوجد خطأ ... فشل استعادة الاعدادات" & vbNewLine & ex.Message, "النظام", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Sub Check_Setting(Setting_Name As String, Setting_Value As String)
        Select Case Setting_Name

            Case "S_Total_CL"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    S_Total_CL = Convert.ToBoolean(Setting_Value)
                Else
                    S_Total_CL = Setting_Value
                End If

            Case "Server_Choese_Server"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Server_Choese_Server = Convert.ToBoolean(Setting_Value)
                Else
                    Server_Choese_Server = Setting_Value
                End If

            Case "S_IMNUM_CL"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    S_IMNUM_CL = Convert.ToBoolean(Setting_Value)
                Else
                    S_IMNUM_CL = Setting_Value
                End If

            Case "AttachDbFilename"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    AttachDbFilename = Convert.ToBoolean(Setting_Value)
                Else
                    AttachDbFilename = Setting_Value
                End If

            Case "ST_GM_Name"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    ST_GM_Name = Convert.ToBoolean(Setting_Value)
                Else
                    ST_GM_Name = Setting_Value
                End If

            Case "is_SubSys"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    is_SubSys = Convert.ToBoolean(Setting_Value)
                Else
                    is_SubSys = Setting_Value
                End If

            Case "App_Suuply"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    App_Suuply = Convert.ToBoolean(Setting_Value)
                Else
                    App_Suuply = Setting_Value
                End If

            Case "DB_Authentication"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    DB_Authentication = Convert.ToBoolean(Setting_Value)
                Else
                    DB_Authentication = Setting_Value
                End If

            Case "SqlConStr"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    SqlConStr = Convert.ToBoolean(Setting_Value)
                Else
                    SqlConStr = Setting_Value
                End If

            Case "isCenterSys"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    isCenterSys = Convert.ToBoolean(Setting_Value)
                Else
                    isCenterSys = Setting_Value
                End If

            Case "S_Default"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    S_Default = Convert.ToBoolean(Setting_Value)
                Else
                    S_Default = Setting_Value
                End If
            Case "POS_Search_Type"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    POS_Search_Type = Convert.ToBoolean(Setting_Value)
                Else
                    POS_Search_Type = Setting_Value
                End If
            Case "ST_Last_Pch_Price"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    ST_Last_Pch_Price = Convert.ToBoolean(Setting_Value)
                Else
                    ST_Last_Pch_Price = Setting_Value
                End If
            Case "SP_Notes_CL"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    SP_Notes_CL = Convert.ToBoolean(Setting_Value)
                Else
                    SP_Notes_CL = Setting_Value
                End If
            Case "BarcodeWidth"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    BarcodeWidth = Convert.ToBoolean(Setting_Value)
                Else
                    BarcodeWidth = Setting_Value
                End If
            Case "IMPR_BAR"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    IMPR_BAR = Convert.ToBoolean(Setting_Value)
                Else
                    IMPR_BAR = Setting_Value
                End If

            Case "S_Barcode_CL"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    S_Barcode_CL = Convert.ToBoolean(Setting_Value)
                Else
                    S_Barcode_CL = Setting_Value
                End If
            Case "BarcodeHieght"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    BarcodeHieght = Convert.ToBoolean(Setting_Value)
                Else
                    BarcodeHieght = Setting_Value
                End If
            Case "S_Serial_Code_CL"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    S_Serial_Code_CL = Convert.ToBoolean(Setting_Value)
                Else
                    S_Serial_Code_CL = Setting_Value
                End If
            Case "IMPR_MINSP"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    IMPR_MINSP = Convert.ToBoolean(Setting_Value)
                Else
                    IMPR_MINSP = Setting_Value
                End If
            Case "IMPR_MINSP_2"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    IMPR_MINSP_2 = Convert.ToBoolean(Setting_Value)
                Else
                    IMPR_MINSP_2 = Setting_Value
                End If
            Case "S_IMUnit_CL"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    S_IMUnit_CL = Convert.ToBoolean(Setting_Value)
                Else
                    S_IMUnit_CL = Setting_Value
                End If
            Case "SB_AG_Show_Balance"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    SB_AG_Show_Balance = Convert.ToBoolean(Setting_Value)
                Else
                    SB_AG_Show_Balance = Setting_Value
                End If
            Case "BarcodePLeft"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    BarcodePLeft = Convert.ToBoolean(Setting_Value)
                Else
                    BarcodePLeft = Setting_Value
                End If
            Case "DB_Choese_Server"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    DB_Choese_Server = Convert.ToBoolean(Setting_Value)
                Else
                    DB_Choese_Server = Setting_Value
                End If
            Case "S_SERVER"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    S_SERVER = Convert.ToBoolean(Setting_Value)
                Else
                    S_SERVER = Setting_Value
                End If
            Case "S_Price_CL"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    S_Price_CL = Convert.ToBoolean(Setting_Value)
                Else
                    S_Price_CL = Setting_Value
                End If
            Case "S_D_Valid_CL"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    S_D_Valid_CL = Convert.ToBoolean(Setting_Value)
                Else
                    S_D_Valid_CL = Setting_Value
                End If
            Case "BarcodeBUp"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    BarcodeBUp = Convert.ToBoolean(Setting_Value)
                Else
                    BarcodeBUp = Setting_Value
                End If
            Case "ST_STNAME"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    ST_STNAME = Convert.ToBoolean(Setting_Value)
                Else
                    ST_STNAME = Setting_Value
                End If
            Case "BarcodeNumber"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    BarcodeNumber = Convert.ToBoolean(Setting_Value)
                Else
                    BarcodeNumber = Setting_Value
                End If
            Case "BarcodePUp"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    BarcodePUp = Convert.ToBoolean(Setting_Value)
                Else
                    BarcodePUp = Setting_Value
                End If
            Case "Call_IM_After_Insert_CB"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Call_IM_After_Insert_CB = Convert.ToBoolean(Setting_Value)
                Else
                    Call_IM_After_Insert_CB = Setting_Value
                End If
            Case "S_OpenNextBill"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    S_OpenNextBill = Convert.ToBoolean(Setting_Value)
                Else
                    S_OpenNextBill = Setting_Value
                End If
            Case "DB_Pass"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    DB_Pass = Convert.ToBoolean(Setting_Value)
                Else
                    DB_Pass = Setting_Value
                End If
            Case "BR_Print_IMName"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    BR_Print_IMName = Convert.ToBoolean(Setting_Value)
                Else
                    BR_Print_IMName = Setting_Value
                End If
            Case "DB_UName"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    DB_UName = Convert.ToBoolean(Setting_Value)
                Else
                    DB_UName = Setting_Value
                End If
            Case "Search_By_Bar_Rtn"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Search_By_Bar_Rtn = Convert.ToBoolean(Setting_Value)
                Else
                    Search_By_Bar_Rtn = Setting_Value
                End If

            Case "Cpu_ID"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Cpu_ID = Convert.ToBoolean(Setting_Value)
                Else
                    Cpu_ID = Setting_Value
                End If
            Case "ST_IM_Num"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    ST_IM_Num = Convert.ToBoolean(Setting_Value)
                Else
                    ST_IM_Num = Setting_Value
                End If
            Case "Server_Desc"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Server_Desc = Convert.ToBoolean(Setting_Value)
                Else
                    Server_Desc = Setting_Value
                End If
            Case "S_Project_CL"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    S_Project_CL = Convert.ToBoolean(Setting_Value)
                Else
                    S_Project_CL = Setting_Value
                End If
            Case "BarcodeColumn"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    BarcodeColumn = Convert.ToBoolean(Setting_Value)
                Else
                    BarcodeColumn = Setting_Value
                End If
            Case "SERVER_IP"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    SERVER_IP = Convert.ToBoolean(Setting_Value)
                Else
                    SERVER_IP = Setting_Value
                End If
            Case "MAINFORM_BK"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    MAINFORM_BK = Convert.ToBoolean(Setting_Value)
                Else
                    MAINFORM_BK = Setting_Value
                End If
            Case "IsAttachDB"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    IsAttachDB = Convert.ToBoolean(Setting_Value)
                Else
                    IsAttachDB = Setting_Value
                End If
            Case "IMPR_IMNUM"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    IMPR_IMNUM = Convert.ToBoolean(Setting_Value)
                Else
                    IMPR_IMNUM = Setting_Value
                End If
            Case "S_ST_Name_CL"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    S_ST_Name_CL = Convert.ToBoolean(Setting_Value)
                Else
                    S_ST_Name_CL = Setting_Value
                End If
            Case "Order_No_Deliver_Date"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Order_No_Deliver_Date = Convert.ToBoolean(Setting_Value)
                Else
                    Order_No_Deliver_Date = Setting_Value
                End If
            Case "Order_Search_Type"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Order_Search_Type = Convert.ToBoolean(Setting_Value)
                Else
                    Order_Search_Type = Setting_Value
                End If
            Case "S_CodeAdd_1"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    S_CodeAdd_1 = Convert.ToBoolean(Setting_Value)
                Else
                    S_CodeAdd_1 = Setting_Value
                End If
            Case "Pr_Printer_isShow"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Pr_Printer_isShow = Convert.ToBoolean(Setting_Value)
                Else
                    Pr_Printer_isShow = Setting_Value
                End If
            Case "BarcodeCheckA4"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    BarcodeCheckA4 = Convert.ToBoolean(Setting_Value)
                Else
                    BarcodeCheckA4 = Setting_Value
                End If
            Case "DataBase"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    DataBase = Convert.ToBoolean(Setting_Value)
                Else
                    DataBase = Setting_Value
                End If
            Case "is_ByBarInput"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    is_ByBarInput = Convert.ToBoolean(Setting_Value)
                Else
                    is_ByBarInput = Setting_Value
                End If
            Case "ST_Valid"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    ST_Valid = Convert.ToBoolean(Setting_Value)
                Else
                    ST_Valid = Setting_Value
                End If
            Case "S_Date_CL"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    S_Date_CL = Convert.ToBoolean(Setting_Value)
                Else
                    S_Date_CL = Setting_Value
                End If
            Case "BarcodeRows"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    BarcodeRows = Convert.ToBoolean(Setting_Value)
                Else
                    BarcodeRows = Setting_Value
                End If

            Case "ShowCname"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    ShowCname = Convert.ToBoolean(Setting_Value)
                Else
                    ShowCname = Setting_Value
                End If
            Case "AG_SH_Bill_Type"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    AG_SH_Bill_Type = Convert.ToBoolean(Setting_Value)
                Else
                    AG_SH_Bill_Type = Setting_Value
                End If
            Case "BR_Print_IMPrice"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    BR_Print_IMPrice = Convert.ToBoolean(Setting_Value)
                Else
                    BR_Print_IMPrice = Setting_Value
                End If
            Case "SB_Sch_With_QTY"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    SB_Sch_With_QTY = Convert.ToBoolean(Setting_Value)
                Else
                    SB_Sch_With_QTY = Setting_Value
                End If
            Case "ShowIM_Price_On_Barcode"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    ShowIM_Price_On_Barcode = Convert.ToBoolean(Setting_Value)
                Else
                    ShowIM_Price_On_Barcode = Setting_Value
                End If
            Case "IM_Search_GM_ID"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    IM_Search_GM_ID = Convert.ToBoolean(Setting_Value)
                Else
                    IM_Search_GM_ID = Setting_Value
                End If

            Case "IM_Use_Out_KB"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    IM_Use_Out_KB = Convert.ToBoolean(Setting_Value)
                Else
                    IM_Use_Out_KB = Setting_Value
                End If

            Case "Tables_Flate_ID"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Tables_Flate_ID = Convert.ToBoolean(Setting_Value)
                Else
                    Tables_Flate_ID = Setting_Value
                End If

            Case "Second_Part_isPrice"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Second_Part_isPrice = Convert.ToBoolean(Setting_Value)
                Else
                    Second_Part_isPrice = Setting_Value
                End If

            Case "Print_TB_Before_End"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Print_TB_Before_End = Convert.ToBoolean(Setting_Value)
                Else
                    Print_TB_Before_End = Setting_Value
                End If

            Case "Second_Part_isPrice"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Second_Part_isPrice = Convert.ToBoolean(Setting_Value)
                Else
                    Second_Part_isPrice = Setting_Value
                End If

            Case "QTY_ALERT_SOUND"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    QTY_ALERT_SOUND = Convert.ToBoolean(Setting_Value)
                Else
                    QTY_ALERT_SOUND = Setting_Value
                End If

            Case "SB_Show_Bill"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    SB_Show_Bill = Convert.ToBoolean(Setting_Value)
                Else
                    SB_Show_Bill = Setting_Value
                End If


            Case "SB_Show_Bill_Rest"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    SB_Show_Bill_Rest = Convert.ToBoolean(Setting_Value)
                Else
                    SB_Show_Bill_Rest = Setting_Value
                End If

            Case "SB_Show_SumPied"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    SB_Show_SumPied = Convert.ToBoolean(Setting_Value)
                Else
                    SB_Show_SumPied = Setting_Value
                End If


            Case "Online_Con_Str"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Online_Con_Str = Convert.ToBoolean(Setting_Value)
                Else
                    Online_Con_Str = Setting_Value
                End If

            Case "is_POS_Copy_2"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    is_POS_Copy_2 = Convert.ToBoolean(Setting_Value)
                Else
                    is_POS_Copy_2 = Setting_Value
                End If

            Case "POS_Copy_2_Path"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    POS_Copy_2_Path = Convert.ToBoolean(Setting_Value)
                Else
                    POS_Copy_2_Path = Setting_Value
                End If

            Case "S_IM_NOTE_CL"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    S_IM_NOTE_CL = Convert.ToBoolean(Setting_Value)
                Else
                    S_IM_NOTE_CL = Setting_Value
                End If


            Case "ShowIM_IM_NAME_On_Barcode"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    ShowIM_IM_NAME_On_Barcode = Convert.ToBoolean(Setting_Value)
                Else
                    ShowIM_IM_NAME_On_Barcode = Setting_Value
                End If

            Case "ShowIM_IM_NUM_On_Barcode"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    ShowIM_IM_NUM_On_Barcode = Convert.ToBoolean(Setting_Value)
                Else
                    ShowIM_IM_NUM_On_Barcode = Setting_Value
                End If

            Case "SALES_TYPES_CMB"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    SALES_TYPES_CMB = Convert.ToBoolean(Setting_Value)
                Else
                    SALES_TYPES_CMB = Setting_Value
                End If


            Case "Thread_Time"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Thread_Time = Convert.ToBoolean(Setting_Value)
                Else
                    Thread_Time = Setting_Value
                End If

            Case "SB_Remove_Dec"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    SB_Remove_Dec = Convert.ToBoolean(Setting_Value)
                Else
                    SB_Remove_Dec = Setting_Value
                End If


            Case "SB_IM_NEW_ROW"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    SB_IM_NEW_ROW = Convert.ToBoolean(Setting_Value)
                Else
                    SB_IM_NEW_ROW = Setting_Value
                End If


            Case "GM_ID_Selected"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    GM_ID_Selected = Convert.ToBoolean(Setting_Value)
                Else
                    GM_ID_Selected = Setting_Value
                End If


            Case "SBill_Title_1"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    SBill_Title_1 = Convert.ToBoolean(Setting_Value)
                Else
                    SBill_Title_1 = Setting_Value
                End If

            Case "SBill_Title_2"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    SBill_Title_2 = Convert.ToBoolean(Setting_Value)
                Else
                    SBill_Title_2 = Setting_Value
                End If


            Case "ORG_ST_ACC_CODE"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    ORG_ST_ACC_CODE = Convert.ToBoolean(Setting_Value)
                Else
                    ORG_ST_ACC_CODE = Setting_Value
                End If

            Case "Income_ST_ACC_CODE"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Income_ST_ACC_CODE = Convert.ToBoolean(Setting_Value)
                Else
                    Income_ST_ACC_CODE = Setting_Value
                End If

            Case "Pure_Income_ACC_CODE"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Pure_Income_ACC_CODE = Convert.ToBoolean(Setting_Value)
                Else
                    Pure_Income_ACC_CODE = Setting_Value
                End If

            Case "ACC_LEVEL_SEARCH"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    ACC_LEVEL_SEARCH = Convert.ToBoolean(Setting_Value)
                Else
                    ACC_LEVEL_SEARCH = Setting_Value
                End If

            Case "AG_Show_Balance_in_Receipt"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    AG_Show_Balance_in_Receipt = Convert.ToBoolean(Setting_Value)
                Else
                    AG_Show_Balance_in_Receipt = Setting_Value
                End If


            Case "is_Link_With_SB"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    is_Link_With_SB = Convert.ToBoolean(Setting_Value)
                Else
                    is_Link_With_SB = Setting_Value
                End If

            Case "SALES_DB"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    SALES_DB = Convert.ToBoolean(Setting_Value)
                Else
                    SALES_DB = Setting_Value
                End If

            Case "is_Dark_mode"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    is_Dark_mode = Convert.ToBoolean(Setting_Value)
                Else
                    is_Dark_mode = Setting_Value
                End If

            Case "Use_State_Budget"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Use_State_Budget = Convert.ToBoolean(Setting_Value)
                Else
                    Use_State_Budget = Setting_Value
                End If

            Case "Allow_Budget_OverSpend"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Allow_Budget_OverSpend = Convert.ToBoolean(Setting_Value)
                Else
                    Allow_Budget_OverSpend = Setting_Value
                End If

            Case "Default_Stamp_Percent"
                Dim stampPercent As Decimal
                If Decimal.TryParse(Setting_Value, stampPercent) Then
                    Default_Stamp_Percent = stampPercent
                Else
                    Default_Stamp_Percent = 0D
                End If

            Case "Default_Stamp_Account_Code"
                Default_Stamp_Account_Code = Setting_Value

            Case "is_Print_ACC_B_Letters"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    is_Print_ACC_B_Letters = Convert.ToBoolean(Setting_Value)
                Else
                    is_Print_ACC_B_Letters = Setting_Value
                End If

            Case "ACC_B_printer_Type"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    ACC_B_printer_Type = Convert.ToBoolean(Setting_Value)
                Else
                    ACC_B_printer_Type = Setting_Value
                End If


            Case "YEAR_TO_MOVE_ACC"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    YEAR_TO_MOVE_ACC = Convert.ToBoolean(Setting_Value)
                Else
                    YEAR_TO_MOVE_ACC = Setting_Value
                End If

            Case "is_Search_By_Levels"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    is_Search_By_Levels = Convert.ToBoolean(Setting_Value)
                Else
                    is_Search_By_Levels = Setting_Value
                End If




        End Select
    End Sub


    Public Sub Save_AppSetting()
        ExportButton_Setting_ToFile()
    End Sub
    '------------------------------------------------------------------------------------------------------------------------------------

    Public Sub ExportButton_STORES_Explorer_Setting_ToFile(ByRef CheckedListBox1 As CheckedListBox)
        Try

            Dim path As String = Application.StartupPath & "\Setting\Accounting\BackUpSettings_STORES_Explorer_File.AppSettings"
            Using sWriter As New StreamWriter(path)
                Dim F As Boolean = False

                For i = 0 To CheckedListBox1.Items.Count - 1
                    CheckedListBox1.SelectedIndex = i
                    F = CheckedListBox1.GetItemChecked(i)
                    sWriter.WriteLine(i & ":" & F.ToString)
                    'sWriter.WriteLine(i & ":" & "False")
                Next

            End Using

            'My_Settings.Save()
            ' MessageBox.Show("تم اخذ نسخة احتياطية من الاعدادات", "النظام", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            'Logger.Log(ex, "", "", System.Reflection.MethodBase.GetCurrentMethod().Name)
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Recover_STORES_Explorer_File_Setting(ByRef CheckedListBox1 As CheckedListBox)
        Try
            Dim path As String = Application.StartupPath & "\Setting\Accounting\BackUpSettings_STORES_Explorer_File.AppSettings"
            If File.Exists(path) Then

                Using sReader As New StreamReader(path)

                    While sReader.Peek() > 0
                        Try
                            Dim input = sReader.ReadLine()
                            ' Split comma delimited data ( SettingName,SettingValue )  
                            Dim dataSplit = input.Split(CChar(":"))

                            For i = 0 To CheckedListBox1.Items.Count - 1
                                If i = dataSplit(0) Then CheckedListBox1.SetItemChecked(i, dataSplit(1))
                            Next


                            ' Check_STORES_Explorer_Setting(dataSplit(0), dataSplit(1))
                        Catch ex As Exception
                            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "BackUpSettings_STORES_Explorer_File")
                        End Try
                    End While
                End Using

                'MessageBox.Show("تم تحميل الاعدادات بنجاح ... قم بالخروج من الظام تم قم بالدخول مجددا", "النظام", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Application.Exit()
                'Application.ExitThread()

            Else
                ExportButton_STORES_Explorer_Setting_ToFile(CheckedListBox1)
                'MessageBox.Show("لم يتم ايجاد ملف الاعدادات الاحتياطي ... فشل استعادة الاعدادات", "النظام", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("يوجد خطأ ... فشل استعادة الاعدادات" & vbNewLine & ex.Message, "النظام", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    'Public Sub Check_STORES_Explorer_Setting(Setting_Name As String, Setting_Value As String)
    '    Select Case Setting_Name

    '        Case "S_Total_CL"
    '            S_Total_CL = Setting_Value

    '        Case "Server_Choese_Server"
    '            Server_Choese_Server = Setting_Value

    '        Case "S_IMNUM_CL"
    '            S_IMNUM_CL = Setting_Value

    '        Case "AttachDbFilename"
    '            AttachDbFilename = Setting_Value

    '        Case "ST_GM_Name"
    '            ST_GM_Name = Setting_Value

    '        Case "is_SubSys"
    '            is_SubSys = Setting_Value

    '        Case "App_Suuply"
    '            App_Suuply = Setting_Value

    '        Case "DB_Authentication"
    '            DB_Authentication = Setting_Value

    '        Case "SqlConStr"
    '            SqlConStr = Setting_Value


    '    End Select
    'End Sub


    'Public Sub Save_STORES_Explorer_AppSetting()
    '    ExportButton_Setting_ToFile()
    'End Sub

End Module
