Imports System.IO

Module MY_Settings
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
    Public DB_Choese_Server As String = "CPOS"
    Public S_SERVER As String = My.Computer.Name
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
    Public DataBase As String = "CPOS"
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
    Public Online_Con_Str As String = "Data Source= 65.21.80.196 ;initial catalog='';User Id='';Password=''"
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
    Public Manual_FRM_rpt As Integer = 0
    Public OutSale_rpt As Integer = 0
    Public Hard_Serial_NUM As String = ""
    Public is_Home_Mange_Printers As Boolean = False
    Public POS_BARCODE_TYPE As String = "CODE_128"
    Public S_IM_Discount_CL As Boolean = False
    Public SERVER_IMG_PATH As String = ""
    Public DEBIT_COLOR As String = "#80FF80"
    Public CREDIT_COLOR As String = "#FF8080"
    Public N_Point_Fter As String = "N3"
    Public NumOfBillsTest As String = "400"
    Public SB_Search_Bill_Autot_Select As Boolean = True
    Public AG_Show_Balance_in_Receipt As Boolean = True
    Public is_Use_Multi_Pay As Boolean = False
    Public Show_AllBill_Clmns As Boolean = True
    '---------------------------------------------------------------------------------------------------------------------------------

    Public Sub ExportButton_Setting_ToFile()
        Try
            '" & My.Computer.Name & "_B
            Dim path As String = Application.StartupPath & "\Settings Files\BackUpSettings.AppSettings"
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
                sWriter.WriteLine("Manual_FRM_rpt" & ":" & Manual_FRM_rpt.ToString())
                sWriter.WriteLine("OutSale_rpt" & ":" & OutSale_rpt.ToString())
                sWriter.WriteLine("Hard_Serial_NUM" & ":" & Hard_Serial_NUM.ToString())
                sWriter.WriteLine("is_Home_Mange_Printers" & ":" & is_Home_Mange_Printers.ToString())
                sWriter.WriteLine("POS_BARCODE_TYPE" & ":" & POS_BARCODE_TYPE.ToString())
                sWriter.WriteLine("S_IM_Discount_CL" & ":" & S_IM_Discount_CL.ToString())
                sWriter.WriteLine("SERVER_IMG_PATH" & ":" & SERVER_IMG_PATH.ToString())
                sWriter.WriteLine("DEBIT_COLOR" & ":" & DEBIT_COLOR.ToString())
                sWriter.WriteLine("CREDIT_COLOR" & ":" & CREDIT_COLOR.ToString())
                sWriter.WriteLine("N_Point_Fter" & ":" & N_Point_Fter.ToString())
                sWriter.WriteLine("NumOfBillsTest" & ":" & EncryptionHelper.Encrypt(NumOfBillsTest.ToString()))
                sWriter.WriteLine("SB_Search_Bill_Autot_Select" & ":" & SB_Search_Bill_Autot_Select.ToString())
                sWriter.WriteLine("AG_Show_Balance_in_Receipt" & ":" & AG_Show_Balance_in_Receipt.ToString())
                sWriter.WriteLine("is_Use_Multi_Pay" & ":" & is_Use_Multi_Pay.ToString())
                sWriter.WriteLine("Show_AllBill_Clmns" & ":" & Show_AllBill_Clmns.ToString())




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
            Dim path As String = Application.StartupPath & "\Settings Files\BackUpSettings.AppSettings"
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


            Case "Manual_FRM_rpt"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Manual_FRM_rpt = Convert.ToBoolean(Setting_Value)
                Else
                    Manual_FRM_rpt = Setting_Value
                End If

            Case "OutSale_rpt"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    OutSale_rpt = Convert.ToBoolean(Setting_Value)
                Else
                    OutSale_rpt = Setting_Value
                End If

            Case "Hard_Serial_NUM"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Hard_Serial_NUM = Convert.ToBoolean(Setting_Value)
                Else
                    Hard_Serial_NUM = Setting_Value
                End If


            Case "is_Home_Mange_Printers"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    is_Home_Mange_Printers = Convert.ToBoolean(Setting_Value)
                Else
                    is_Home_Mange_Printers = Setting_Value
                End If

            Case "POS_BARCODE_TYPE"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    POS_BARCODE_TYPE = Convert.ToBoolean(Setting_Value)
                Else
                    POS_BARCODE_TYPE = Setting_Value
                End If

            Case "S_IM_Discount_CL"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    S_IM_Discount_CL = Convert.ToBoolean(Setting_Value)
                Else
                    S_IM_Discount_CL = Setting_Value
                End If

            Case "SERVER_IMG_PATH"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    SERVER_IMG_PATH = Convert.ToBoolean(Setting_Value)
                Else
                    SERVER_IMG_PATH = Setting_Value
                End If


            Case "DEBIT_COLOR"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    DEBIT_COLOR = Convert.ToBoolean(Setting_Value)
                Else
                    DEBIT_COLOR = Setting_Value
                End If

            Case "CREDIT_COLOR"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    CREDIT_COLOR = Convert.ToBoolean(Setting_Value)
                Else
                    CREDIT_COLOR = Setting_Value
                End If


            Case "N_Point_Fter"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    N_Point_Fter = Convert.ToBoolean(Setting_Value)
                Else
                    N_Point_Fter = Setting_Value
                End If


            Case "NumOfBillsTest"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    NumOfBillsTest = Convert.ToBoolean(Setting_Value)
                Else
                    NumOfBillsTest = EncryptionHelper.Decrypt(Setting_Value) 'Setting_Value 
                End If

            Case "SB_Search_Bill_Autot_Select"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    SB_Search_Bill_Autot_Select = Convert.ToBoolean(Setting_Value)
                Else
                    SB_Search_Bill_Autot_Select = Setting_Value
                End If


            Case "AG_Show_Balance_in_Receipt"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    AG_Show_Balance_in_Receipt = Convert.ToBoolean(Setting_Value)
                Else
                    AG_Show_Balance_in_Receipt = Setting_Value
                End If

            Case "is_Use_Multi_Pay"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    is_Use_Multi_Pay = Convert.ToBoolean(Setting_Value)
                Else
                    is_Use_Multi_Pay = Setting_Value
                End If

            Case "Show_AllBill_Clmns"
                If Setting_Value = "True" Or Setting_Value = "False" Then
                    Show_AllBill_Clmns = Convert.ToBoolean(Setting_Value)
                Else
                    Show_AllBill_Clmns = Setting_Value
                End If



        End Select
    End Sub


    Public Sub Save_AppSetting()
        ExportButton_Setting_ToFile()
    End Sub
    '------------------------------------------------------------------------------------------------------------------------------------

    Public Sub ExportButton_STORES_Explorer_Setting_ToFile(ByRef CheckedListBox1 As CheckedListBox)
        Try

            Dim path As String = Application.StartupPath & "\Settings Files\BackUpSettings_STORES_Explorer_File.AppSettings"
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
            Dim path As String = Application.StartupPath & "\Settings Files\BackUpSettings_STORES_Explorer_File.AppSettings"
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



End Module
