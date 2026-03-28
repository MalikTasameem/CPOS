Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing.Printing
Imports DevComponents.DotNetBar
Imports System.Net.NetworkInformation
Imports System.Globalization
Imports System.Management
Imports System.IO.Ports
Imports System.Runtime.InteropServices
Imports System
Imports Microsoft.Office.Interop
Imports Microsoft.Win32
Imports System.Text.RegularExpressions

Module FunModule

    Public h = My.Computer.Screen.Bounds.Size.Height
    Public w = My.Computer.Screen.Bounds.Size.Width

    Public Type_Of_E_mail As Integer = 0 ' 0:from balance , 1:from sales

    Public Sub prepare_Sys()

        'Try

        MY_Settings.Cpu_ID = CpuId()
        'MY_Settings.Hard_Serial_NUM = GetDriveSerialNumber()     
        Save_AppSetting()
        Sys_Switch()
        Test_Computer_Setting()
        Test_Product_Code()
        PC_Connect()

        'Catch ex As Exception
        '    Environment.Exit(0)
        'End Try

    End Sub

    Public Sub EXEPTION()
        MsgBox("تأكد من الإتصال بقاعدة البيانات", MsgBoxStyle.Exclamation, "")
    End Sub


    'Private Function Check_Database()
    '    Try
    '        Dim Con As New SqlConnection()
    '        If My_Settings.DB_Authentication = 0 Then
    '            My_Settings.SqlConStr = "Data Source= " & My_Settings.S_SERVER & " ;initial catalog=" & My_Settings.DataBase & ";Integrated Security=True;"
    '        Else
    '            My_Settings.SqlConStr = "Data Source= " & My_Settings.S_SERVER & " ;initial catalog=" & My_Settings.DataBase & ";User Id=" & My_Settings.DB_UName & ";Password=" & My_Settings.DB_Pass & ""
    '        End If
    '        Dim C As New C
    '        C.Str = "SELECT * FROM master.dbo.sysdatabases WHERE name = '" & My_Settings.DataBase & "'"
    '        C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
    '        C.Con.Open()
    '        C.Dr = C.Com.ExecuteReader
    '        If C.Dr.HasRows = True Then Return 1
    '        C.Con.Close()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    '    Return 0
    'End Function

    Private Sub START_REPORT()
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\START_REPORT.rpt")
        pp.LoadTables()
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        Dim p As New print
        p.CrystalReportViewer1.ReportSource = pp.rp
        pp.rp.Dispose()
        p.Show()
    End Sub

    Public Sub PC_Connect()
        Try
            Dim c As New C
            Dim sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
            Using (sqlCon)
                Dim sqlComm As New SqlClient.SqlCommand()
                sqlComm.Connection = sqlCon
                sqlComm.CommandText = "PC_Connect"
                sqlComm.CommandType = CommandType.StoredProcedure
                sqlComm.Parameters.AddWithValue("@CP_NAME", My.Computer.Name)
                sqlCon.Open()
                Try
                    sqlComm.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                sqlCon.Close()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message + " (PC_Connect)")
        End Try

    End Sub


    Public Sub Test_Product_Code()
        Try
            Dim C As New C
            C.Str = "Select ISNULL(ProductCode,'') AS ProductCode From Activation_Details WHERE CP_NAME = '" & My.Computer.Name & "'"
            'C.Str = "Select ISNULL(ProductCode,'') AS ProductCode From Activation_Details WHERE [Cpu_ID] = '" & MY_Settings.Hard_Serial_NUM & "'"
            C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
            C.Con.Open()

            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                C.Dr.Read()
                If C.Dr("ProductCode") <> "" Then
                    S_ProductCode = C.Dr("ProductCode")
                Else
                    Build_Product_Code()
                End If

            Else
                Build_Product_Code()
            End If
            C.Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message + "  (Test_Product_Code)")
        End Try

    End Sub

    'Public Function Get_HDD_S()
    '    Dim HDD_Serial As String = ""
    '    Dim MB_serial As String = ""

    '    Dim hdd As New ManagementObjectSearcher("select * from Win32_DiskDrive")

    '    For Each hd In hdd.Get
    '        HDD_Serial = hd("SerialNumber")
    '    Next


    '    Dim mboard As New ManagementObjectSearcher("select * from Win32_BaseBoard")
    '    For Each mb In mboard.Get
    '        MB_serial = mb("SerialNumber")
    '    Next

    '    Return MB_serial
    '    ' Return HDD_Serial
    'End Function



    Public Sub Sys_Switch()
        Try
            Dim C As New C
            C.Str = "Select * From Sys_Features WHERE T_ID = (SELECT T_ID FROM Sys_Model)"
            C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
            C.Con.Open()

            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                C.Dr.Read()
                S_Pr = C.Dr("Pr")
                S_Tables = C.Dr("Tables")
                S_SubPrints = C.Dr("SubPrints")
                S_Notes = C.Dr("Notes")
                S_Orders = C.Dr("Orders")
                S_Stores = C.Dr("Stores")
                S_Frm = C.Dr("Frm")
                S_In_Out_side = C.Dr("inside_Bill")
                SScreenDefault = C.Dr("SScreenDefault")
                IM_Default_Stut = C.Dr("IM_Default_Stut")
                S_Marketers = C.Dr("Marketers")
                If S_Orders = True Then SB_DefaultStatus = 3

                S_SerialCode = C.Dr("SerialCode")
                S_Out_Travel = C.Dr("Out_Travel")
                S_Allow_MinSP = C.Dr("Allow_MinSP")
                S_Exp_Pch = C.Dr("Exp_Pch")
                S_AgentCard = C.Dr("AgentCards")
                S_Phone_For_Tables = C.Dr("Phone_For_Tables")
                S_Auto_Recive_ST_Tran = C.Dr("is_Auto_Recive_ST_Tran")
                S_IM_Valid = C.Dr("IM_Valid")
                S_TB_Auto_Print = C.Dr("TB_Auto_Print")
                'is_Pch_Discount_Distribute = C.Dr("is_Pch_Discount_Distribute")
            End If


            C = New C
            C.Str = " Select  TOP 1 * From SERVERS "
            C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
            C.Con.Open()

            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                C.Dr.Read()
                S_Sub_Code = C.Dr("Sub_Code")
                Serv_Desc = C.Dr("Serv_Desc")
                START_ID = C.Dr("START_ID")
                END_ID = C.Dr("END_ID")
            End If
            C.Con.Close()


        Catch ex As Exception
            System_Start_Normal = False
            MsgBox(ex.Message + " (Sys_Switch)")
        End Try





    End Sub

    Public Sub Test_Computer_Setting()
        Try
            Dim C As New C
            C.Str = "Select CP_NAME FROM SysSetting WHERE CP_NAME ='" & My.Computer.Name & "'"
            'C.Str = "Select CP_NAME FROM SysSetting WHERE Cpu_ID ='" & MY_Settings.Hard_Serial_NUM & "'"
            C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
            C.Con.Open()

            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                Get_Computer_Setting()
            Else
                Computer_Setting_InsertDefult()
            End If
            C.Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message + " (Test_Computer_Setting)")
        End Try

    End Sub


    Private Sub Build_Product_Code()
        Dim Day As String = Date.Now.Day.ToString
        If Day.Count = 1 Then Day = "0" + Day
        Dim Month As String = Date.Now.Month.ToString
        If Month.Count = 1 Then Month = "0" + Month
        Date_Serial = Day + Month + Date.Now.Year.ToString
        Save_AppSetting()
        ''-------------------------------------------------------------------------
        Dim Tmp As String = ""
        Dim TmpSerialArray(8) As Char
        For i = 0 To 7
            Randomize()
            TmpSerialArray(i) = (CInt(Math.Ceiling(Rnd() * 9)) + 1).ToString
            Tmp = Tmp + TmpSerialArray(i)
        Next
        Random_Serial = Tmp

        Dim Serial As String = ""
        Serial = Serial + Date_Serial + MY_Settings.Cpu_ID + Random_Serial 'Get_first8Digits(MY_Settings.Hard_Serial_NUM)

        S_ProductCode = Serial
        Save_AppSetting()

        Parsing_Code(Serial)

        Dim c As New C
        Dim sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
        Using (sqlCon)
            Dim sqlComm As New SqlClient.SqlCommand()
            sqlComm.Connection = sqlCon
            sqlComm.CommandText = "Activation_Details_insert"
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.AddWithValue("@CP_NAME", My.Computer.Name)
            sqlComm.Parameters.AddWithValue("@Cpu_ID", MY_Settings.Cpu_ID) 'MY_Settings.Hard_Serial_NUM 
            sqlComm.Parameters.AddWithValue("@ProductCode", Serial)
            sqlCon.Open()
            Try
                sqlComm.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            sqlCon.Close()
        End Using
    End Sub

    Function Get_first8Digits(inputString As String)


        ' Variable to store the result
        Dim first8CharsOrNumbers As String = ""
        Dim charCount As Integer = 0

        ' Iterate through each character in the string
        For Each ch As Char In inputString
            ' Check if the character is a letter or a digit
            If Char.IsLetterOrDigit(ch) Then
                ' Add the valid character to the result
                first8CharsOrNumbers &= ch
                charCount += 1

                ' Stop if we have collected 8 valid characters
                If charCount = 8 Then
                    Exit For
                End If
            End If
        Next

        ' Output the result
        'Console.WriteLine("First 8 letters or numbers: " & first8CharsOrNumbers)
        'MsgBox(first8CharsOrNumbers)
        Return first8CharsOrNumbers
    End Function


    'Sub Main()
    '    ' Path to the file you want to check and create
    '    Dim filePath As String = "C:\Temp\myfile.txt"

    '    ' Check if the file exists
    '    If Not File.Exists(filePath) Then
    '        ' File does not exist, create it
    '        Using sw As StreamWriter = File.CreateText(filePath)
    '            ' You can write content to the file if needed
    '            sw.WriteLine("This is a new file created because it did not exist.")
    '        End Using
    '        Console.WriteLine("File created successfully: " & filePath)
    '    Else
    '        ' File exists
    '        Console.WriteLine("File already exists: " & filePath)
    '    End If
    'End Sub

    Public Sub GetLastShow()
        Try
            Dim c As New C
            Dim sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
            Using (sqlCon)
                Dim sqlComm As New SqlClient.SqlCommand()
                sqlComm.Connection = sqlCon
                sqlComm.CommandText = "GetLastShow"
                sqlComm.CommandType = CommandType.StoredProcedure
                sqlComm.Parameters.AddWithValue("@CP_NAME", My.Computer.Name)
                sqlCon.Open()
                sqlComm.ExecuteNonQuery()
                sqlCon.Close()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Public Sub Parsing_Code(ByRef Product_Code As String)
        Dim i As Integer
        Dim length As Integer
        Dim value As String

        length = Product_Code.Length
        value = Product_Code

        For i = 0 To Product_Code.Length Step 5
            If i = 0 Then
                value = value.Insert(i + 4, "-")
            Else
                value = value.Insert(i + 4, "-")
            End If
        Next
        'length - 4
        Product_Code = value
    End Sub


    '---------------------------------------------------------------------------------
    Public Sub Get_Computer_Setting()
        Dim c As New C
        Dim str As String = "Select *  From SysSetting  Where CP_NAME = '" & My.Computer.Name & "'"
        c.Com = New SqlCommand(str, c.Con)

        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            c.Dr.Read()

            If c.Dr.HasRows = True Then

                SBill_Title_1 = c.Dr("CompName")
                SBill_Title_2 = c.Dr("englishN")
                SBill_Footer = c.Dr("BillNotes")
                isBackup = c.Dr("isBackup")
                BackupPath = c.Dr("BackupPath")
                isBackupPath2 = c.Dr("isBackup2")
                BackupPath_2 = c.Dr("BackupPath_2")
                Default_Printer_80 = c.Dr("Default_Printer_80")
                Default_Printer_A4 = c.Dr("Default_Printer_A4")

                isShowPrintBtn = c.Dr("isShowPrintBtn")
                isDiscount = c.Dr("isDiscount")
                MinimumDiscountValue = c.Dr("MinDisValue")
                SB_DefaultStatus = c.Dr("SB_DefaultStatus")
                PrintTBKsh = c.Dr("PrintTBKsh")
                SB_AutoPrint = c.Dr("SB_AutoPrint")

                KshPrint = c.Dr("KshPrint")
                BackupPath_OnExit = c.Dr("BK_OnExit")
                OrderPage_ID = c.Dr("OrderPage_ID")
                ReceiptPage_ID = c.Dr("ReceiptPage_ID")
                SB_Default_AG = c.Dr("SB_Default_AG")

                ImageScannerPath = c.Dr("ScannerName")
                isCostmerScreen = c.Dr("isCostmerScreen")
                SScreenDefault = c.Dr("SScreenDefault")
                AGBillPage_ID = c.Dr("AGBillPage_ID")
                IM_Day_Valid = c.Dr("IM_Day_Valid")

                SB_ST_ID = c.Dr("SB_ST_ID")
                PCH_ST_ID = c.Dr("PCH_ST_ID")
                SB_ST_Can_change = c.Dr("SB_ST_Can_change")
                PCH_ST_Can_change = c.Dr("PCH_ST_Can_change")
                'is_Use_Order = c.Dr("Use_Order")
                CP_Bill_Screen = c.Dr("CP_Bill_Screen")

                Sales_Page_ID = c.Dr("Sales_Page_ID")
                User_AutoPrintSB = c.Dr("User_AutoPrintSB")
                is_SB_OutputScreen = c.Dr("is_SB_OutputScreen")
                ' SB_Default_ST = c.Dr("SB_Default_ST")
                POS_By_Shortcut = c.Dr("POS_By_Shortcut")
                Open_NewBill_When_OpenSale = c.Dr("Open_NewBill_When_OpenSale")
                SB_IM_Alert_When_Repetition = c.Dr("SB_IM_Alert_When_Repetition")
                Discount_Distribute = c.Dr("Discount_Distribute")
                SB_is_Check_Thankes = c.Dr("Check_With_Thankes")

                PCH_TR_ID = c.Dr("PCH_TR_ID")
                SB_TR_ID = c.Dr("SB_TR_ID")
                Rct_Tr_ID = c.Dr("Rct_Tr_ID")
                EXP_TR_ID = c.Dr("EXP_ST_ID")

                Org_PCH_TR_ID = c.Dr("PCH_TR_ID")
                Org_SB_TR_ID = c.Dr("SB_TR_ID")
                Org_Rct_Tr_ID = c.Dr("Rct_Tr_ID")
                Org_EXP_TR_ID = c.Dr("EXP_ST_ID")

                E_mail = c.Dr("E_mail")
                E_mail_Password = c.Dr("E_mail_Pass")
                E_mail_Host = c.Dr("Host")
                E_mail_Port = c.Dr("Port")

                IM_min_QTY = c.Dr("IM_min_QTY")

                Rct_Tr_Can_change = c.Dr("Rct_Tr_Can_change")

                SB_AutoOpenDrawer = c.Dr("SB_AutoOpenDrawer")

                Pr_PrinterPage_Type = c.Dr("Pr_PrinterPage_Type")
                S_is_Multi_BAR = c.Dr("is_Multi_BAR")
                Sales_ViewPage_ID = c.Dr("Sales_ViewPage_ID")
                IM_Default_Stut = c.Dr("IM_Default_Stut")
                Def_U_ID = c.Dr("Def_U_ID")

                is_Use_Total_Port = c.Dr("is_Use_Total_Port")
                Total_Port = c.Dr("Total_Port")

                Ban_Expierd_IM_MV = c.Dr("Ban_Expierd_IM_MV")
                Notif_IM_Sell_Less_Than_Cost = c.Dr("Notif_IM_Sell_Less_Than_Cost")
                Default_Barcode_Printer = c.Dr("Barcode_DefPrinter")
                Def_Befor_Print = c.Dr("Def_Print_Befor_Print")
                SB_is_Check_IM = c.Dr("SB_is_Check_IM")
                SB_is_Copy_Print = c.Dr("SB_is_Copy")
                Copy_Printer = c.Dr("Copy_Printer_Path")

                is_Screen_Font_Resize = c.Dr("is_Screen_Font_Resize")
                Font_Resize_Num = c.Dr("Font_Resize_Num")

                Use_Inside_Barcode = c.Dr("Use_Inside_Barcode_CB")
                Notif_IM_Negitane_QTY = c.Dr("Notif_IM_Negitane_QTY_CB")
                Notif_If_SB_Has_No_SB_Price = c.Dr("Notif_If_SB_Has_No_SB_Price_CB")

                Mizan_BarcodeFrom = c.Dr("Mizan_BarcodeFrom")
                Mizan_BarcodeTo = c.Dr("Mizan_BarcodeTo")
                Mizan_QtyFrom = c.Dr("Mizan_QtyFrom")
                Mizan_QtyTo = c.Dr("Mizan_QtyTo")
                isUseAsPhone_Crawler = c.Dr("isUseAsPhone_Crawler")
                POS_IM_COUNT = c.Dr("POS_IM_COUNT")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            System_Start_Normal = False
        End Try
        c.Con.Close()


        '---------------------------------------------------------------------------
        c = New C
        str = "Select AG_Bill,IN_Bill  From OrdersPageType  Where ID = '" & OrderPage_ID & "'"
        c.Com = New SqlCommand(str, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            c.Dr.Read()

            If c.Dr.HasRows = True Then
                OrderBill_AG_Bill_Track = c.Dr("AG_Bill")
                OrderBill_IN_Bill_Track = c.Dr("IN_Bill")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()

        c = New C
        str = "Select Track From ReceiptsPageType  Where ID = '" & ReceiptPage_ID & "'"
        c.Com = New SqlCommand(str, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            c.Dr.Read()

            If c.Dr.HasRows = True Then
                Receipt_Track = c.Dr("Track")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()
        '-----------------------------------------------------------------------------
        c = New C
        Dim S As String = "Select StdDis From Agents_Types Where ID = 5"
        c.Com = New SqlClient.SqlCommand(S, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                StdDisPercent = c.Dr("StdDis")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()
        '-----------------------------------------------------------------------------
        c = New C
        S = "Select Points_Sale,Point_Inc From Agents_Types Where ID = 6"
        c.Com = New SqlClient.SqlCommand(S, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Points_Sale = c.Dr("Points_Sale")
                Point_Inc = c.Dr("Point_Inc")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        '---------------------------------------------------------------------------

        c = New C
        S = "Select StdDis From Agents_Types Where ID = 7"
        c.Com = New SqlClient.SqlCommand(S, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                PrevDisPercent = c.Dr("StdDis")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()

        '--------------------------------------------------------------------------

        c = New C
        S = "Select AG_Bill From AGBillType  Where ID = '" & AGBillPage_ID & "'"
        c.Com = New SqlClient.SqlCommand(S, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                AGBillPage_Bill_Track = c.Dr("AG_Bill")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()
        '-------------------------------------------------------------------

        c = New C
        S = "Select AG_Bill from Sales_Bill_Page  Where ID = '" & Sales_Page_ID & "'"
        c.Com = New SqlClient.SqlCommand(S, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Sales_BillPage_Bill_Track = c.Dr("AG_Bill")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()

        '-------------------------------------------------------------------

        c = New C
        S = "Select AG_Bill from Sales_ViewBill_Page  Where ID = '" & Sales_ViewPage_ID & "'"
        c.Com = New SqlClient.SqlCommand(S, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Sales_ViewPage_Bill_Track = c.Dr("AG_Bill")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()

        '-------------------------------------------------------------------

        c = New C
        S = "Select TOP 1 AG_ID from AGENTS"
        c.Com = New SqlClient.SqlCommand(S, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Default_AG_ID = c.Dr("AG_ID")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c.Con.Close()

    End Sub

    Public Function SB_Discount_Distribute(T_ID As Integer, Discount_Val As Double)
        Dim c As New C
        Dim is_Execute As Boolean
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_Discount_Distribute"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID)
            If Not String.IsNullOrWhiteSpace(Discount_Val) Then
                .Parameters.AddWithValue("@Discount_Val", Discount_Val)
            End If

        End With
        is_Execute = SQL_SP_EXEC(c.Com)

        Return is_Execute
    End Function

    Public Sub Open_Cash_Drawer()
        Dim D As New Main
        D.OpenCashdrawer()
    End Sub

    Public Sub CB_CHecked(sender As Object)
        If sender.Checked = True Then
            sender.ForeColor = Color.DarkGreen
        Else
            sender.ForeColor = Color.Black
        End If
    End Sub

    Public Function SQL_SP_EXEC(sqlComm As SqlClient.SqlCommand)
        Dim isDone = True
        Dim T
        Dim sqlCon = New SqlConnection(MY_Settings.SqlConStr)
        Using (sqlCon)
            sqlComm.Connection = sqlCon
            sqlCon.Open()
            T = sqlComm.ExecuteNonQuery()
            If T = 0 Then isDone = False
            sqlCon.Close()
        End Using
        Return isDone
    End Function

    Public Sub Build_Connection()
        If My_Settings.IsAttachDB = False Then
            If My_Settings.DB_Authentication = 0 Then
                My_Settings.SqlConStr = "Data Source= " & My_Settings.S_SERVER & " ;initial catalog=" & My_Settings.DataBase & ";Integrated Security=True;"
            Else
                My_Settings.SqlConStr = "Data Source= " & My_Settings.S_SERVER & " ;initial catalog=" & My_Settings.DataBase & ";User Id=" & My_Settings.DB_UName & ";Password=" & My_Settings.DB_Pass & ""
            End If
        Else
            MY_Settings.AttachDbFilename = Application.StartupPath & "\DB\" & MY_Settings.DataBase & ".mdf"
            My_Settings.SqlConStr = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & My_Settings.AttachDbFilename & ";Integrated Security=True;"
        End If

        'Save_AppSetting()

    End Sub

    'Public Function max_group(table As String, col As String) As Integer
    '    Dim c As New C
    '    Dim counter As Integer
    '    Dim sql As String = "select max(" & col & ")from " & table & ""
    '    Dim x As New SqlClient.SqlCommand(sql, c.Con)
    '    c.Con.Open()
    '    If IsDBNull(x.ExecuteScalar()) Then
    '        counter = 1
    '    Else
    '        counter = (x.ExecuteScalar()) + 1
    '    End If
    '    c.Con.Close()
    '    Return counter
    'End Function

    Public Sub Check_Point_in_FloatNum(Sender As Object, e As EventArgs)
        If Sender.Text = "." Then Sender.Text = "0."
    End Sub

    Public Sub Select_ExpBill(T_ID As Integer)

        Dim TOTAL As Double = 0
        Dim C As New C

        Dim S As String = ""

        Select Case FormType
            Case 1
                F_Sales.T_ID = T_ID
                F_Sales.Fill_Bill_Info()
                F_Sales.SB_Contents_SELECT_Bill()
                F_Sales.SelectStateBt()
                Exit Sub
            Case 2 : S = "Select * From Pch_Balance_MV_V Where T_ID = '" & T_ID & "'"
            Case 3 : S = "Select * From IMEX_Balance_MV_V Where T_ID = '" & T_ID & "'"
            Case 4 : S = "Select * From Invoice_Balance_MV_V Where T_ID = '" & T_ID & "'"
            Case 7 : S = "Select * From ST_Trans_Balance_MV_V Where T_ID = '" & T_ID & "'"
            Case 8 : S = "Select * From EXP_Balance_MV_V Where T_ID = '" & T_ID & "'"
            Case 11 : S = "Select * From InSale_Balance_MV_V Where T_ID = '" & T_ID & "'"
            Case 13 : S = "Select * From Outsale_Balance_MV_V Where T_ID = '" & T_ID & "'"
        End Select


        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                If FormType = 2 Then
                    With F_Pch
                        .T_ID = C.Dr("T_ID")
                        .Pch_ID = C.Dr("Bill_ID")
                        .Bill_ID_Txt.Text = S_Sub_Code & (C.Dr("Bill_ID")) ' - START_ID).ToString

                        .AG_ID = C.Dr("AG_ID")
                        .GET_AG()

                        .DateTimeEx.Text = C.Dr("Date")
                        .Notes_txt.Text = C.Dr("About")
                        .EX_ReferNumTextBox.Text = C.Dr("ReferNum")
                        TOTAL = C.Dr("Cost")
                        .Disc = C.Dr("Discount")
                        .Total_txt.Text = TOTAL.ToString("N")
                        .Pure_txt.Text = TOTAL.ToString("N") - C.Dr("Discount")
                        .Discount_txt.Text = C.Dr("Discount")
                        .Switch_Dependcy(C.Dr("isDepended"))
                        .isVoid = C.Dr("isVoid")
                        .User_Name_lb.Text = C.Dr("UserName") + " - " + C.Dr("Date").ToString

                        .SelectStateBt()
                        .Pch_Contents_SELECT_Bill()
                        Select_Pch_Receipt(.T_ID)
                        .Cr_Equal_TXT.Text = C.Dr("Cr_Equal_Value")
                        .Pch_Contents_SELECT_EXP()
                        If .AGMetroGrid.Rows.Count = 0 Then .DateTimeEx.Value = Date.Now
                    End With

                ElseIf FormType = 3 Then

                    With F_IM_Execute
                        .T_ID = C.Dr("T_ID")
                        .AG_ID = C.Dr("AG_ID")
                        .AG_Cm.Set_IM_By_ID(.AG_ID)
                        '.GET_AG()
                        .Bill_ID_Txt.Text = S_Sub_Code & (C.Dr("Bill_ID")) ' - START_ID).ToString
                        .Bill_ID = C.Dr("Bill_ID")
                        .Title_txt.Text = C.Dr("Receipt_Title")
                        .DateTimeEx.Text = C.Dr("Date")
                        .Notes_txt.Text = C.Dr("About")
                        TOTAL = C.Dr("Cost")
                        .Total_TextBox.Text = TOTAL.ToString("N")
                        .Switch_Dependcy(C.Dr("isDepended"))
                        .isVoid = C.Dr("isVoid")
                        .User_Name_lb.Text = C.Dr("UserName") + " - " + C.Dr("Date").ToString
                        .SelectStateBt()
                        .SELECT_EXP_Cats(T_ID)
                        If .AGMetroGrid.Rows.Count = 0 Then .DateTimeEx.Value = Date.Now
                    End With

                ElseIf FormType = 4 Then

                    With F_Invoice
                        .T_ID = C.Dr("T_ID")
                        .Bill_ID_Txt.Text = S_Sub_Code & (C.Dr("Bill_ID")) ' - START_ID).ToString
                        .Pch_ID = C.Dr("Bill_ID")
                        .Title_txt.Text = C.Dr("Receipt_Title")
                        .DateTimeEx.Text = C.Dr("Date")
                        .Notes_txt.Text = C.Dr("About")
                        TOTAL = C.Dr("Cost")
                        .Total_TextBox.Text = TOTAL.ToString("N")
                        .Switch_Dependcy(C.Dr("isDepended"))
                        .isVoid = C.Dr("isVoid")
                        .User_Name_lb.Text = C.Dr("UserName") + " - " + C.Dr("Date").ToString
                        .Invoice_Type_cm.SelectedIndex = C.Dr("Invoice_Type")
                        .Pch_Contents_SELECT_Bill()
                        .SelectStateBt()
                        If .AGMetroGrid.Rows.Count = 0 Then .DateTimeEx.Value = Date.Now
                    End With

                ElseIf FormType = 7 Then

                    With F_Stores_ImmediateOrder
                        .T_ID = C.Dr("T_ID")
                        .Bill_ID_Txt.Text = S_Sub_Code & (C.Dr("Bill_ID")) ' - START_ID).ToString
                        .Bill_ID = C.Dr("Bill_ID")
                        .DateTimeEx.Text = C.Dr("Date")
                        .Notes_txt.Text = C.Dr("About")
                        TOTAL = C.Dr("Cost")
                        .Switch_Dependcy(C.Dr("isDepended"))
                        .isVoid = C.Dr("isVoid")
                        .User_Name_lb.Text = C.Dr("UserName") + " - " + C.Dr("Date").ToString
                        .St_Trans_Contents_SELECT_Bill()
                        .SelectStateBt()
                        If .AGMetroGrid.Rows.Count = 0 Then .DateTimeEx.Value = Date.Now
                    End With

                ElseIf FormType = 8 Then

                    With F_EXP_Details
                        .T_ID = C.Dr("T_ID")
                        .AG_ID = C.Dr("AG_ID")
                        .GET_AG()
                        .Bill_ID_Txt.Text = S_Sub_Code & (C.Dr("Bill_ID")) ' - START_ID).ToString
                        .Bill_ID = C.Dr("Bill_ID")
                        .Notes_txt.Text = C.Dr("About")
                        .Title_txt.Text = C.Dr("Receipt_Title")
                        TOTAL = C.Dr("Cost")
                        .Total_TextBox.Text = TOTAL.ToString("N")
                        .Switch_Dependcy(C.Dr("isDepended"))
                        .isVoid = C.Dr("isVoid")
                        .DateTimeEx.Text = C.Dr("Date")
                        .User_Name_lb.Text = C.Dr("UserName") + " - " + C.Dr("Date").ToString
                        .SELECT_EXP_Cats(.T_ID)
                        .SelectStateBt()
                        Select_EX_Receipt(.T_ID)
                        If .AGMetroGrid.Rows.Count = 0 Then .DateTimeEx.Value = Date.Now
                    End With


                ElseIf FormType = 11 Then

                    With F_Inside_Sales
                        .T_ID = C.Dr("T_ID")
                        .Bill_ID_Txt.Text = S_Sub_Code & (C.Dr("Bill_ID")) ' - START_ID).ToString
                        .Bill_ID = C.Dr("Bill_ID")
                        .Title_txt.Text = C.Dr("Receipt_Title")
                        .DateTimeEx.Text = C.Dr("Date")
                        .Notes_txt.Text = C.Dr("About")
                        TOTAL = C.Dr("Cost")
                        .Total_TextBox.Text = TOTAL.ToString("N")
                        .Switch_Dependcy(C.Dr("isDepended"))
                        .isVoid = C.Dr("isVoid")
                        .User_Name_lb.Text = C.Dr("UserName") + " - " + C.Dr("Date").ToString
                        .Emp_FS.Set_IM_By_ID(C.Dr("AG_ID"))
                        .SelectStateBt()
                        .SELECT_EXP_Cats(T_ID)
                        If .AGMetroGrid.Rows.Count = 0 Then .DateTimeEx.Value = Date.Now
                    End With

                ElseIf FormType = 13 Then

                    With F_Outside_Sales
                        .T_ID = C.Dr("T_ID")
                        .Bill_ID_Txt.Text = S_Sub_Code & (C.Dr("Bill_ID")) ' - START_ID).ToString
                        .Bill_ID = C.Dr("Bill_ID")
                        .Title_txt.Text = C.Dr("Receipt_Title")
                        .DateTimeEx.Text = C.Dr("Date")
                        .Notes_txt.Text = C.Dr("About")
                        TOTAL = C.Dr("Cost")
                        .Total_TextBox.Text = TOTAL.ToString("N")
                        .Switch_Dependcy(C.Dr("isDepended"))
                        .isVoid = C.Dr("isVoid")
                        .User_Name_lb.Text = C.Dr("UserName") + " - " + C.Dr("Date").ToString
                        .Emp_FS.Set_IM_By_ID(C.Dr("AG_ID"))
                        .SelectStateBt()
                        .SELECT_EXP_Cats(T_ID)
                        If .AGMetroGrid.Rows.Count = 0 Then .DateTimeEx.Value = Date.Now
                    End With




                End If

            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

    End Sub

    Public Function ConvertImage(ByVal myImage As Image) As Byte()
        Try
            Dim mstream As New MemoryStream
            myImage.Save(mstream, System.Drawing.Imaging.ImageFormat.Png)
            Dim myBytes(mstream.Length - 1) As Byte
            mstream.Position = 0
            mstream.Read(myBytes, 0, mstream.Length)
            Return myBytes
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub Kill_All_Processes()
        For Each prog As Process In Process.GetProcesses
            If prog.ProcessName = "CPOS" Then
                prog.Kill()
            End If
        Next
    End Sub


    Public Sub Call_KeyBoard()
        Shell("cmd.exe /c start osk")
    End Sub

    Public Sub Set_Ar_Language()
        Application.CurrentInputLanguage = InputLanguage.FromCulture(New CultureInfo("ar-sa"))
    End Sub

    Public Sub Set_Eng_Language()
        Application.CurrentInputLanguage = InputLanguage.FromCulture(New CultureInfo("en"))
    End Sub

    Public Function ResizeImage(SourceImage As Drawing.Image, TargetWidth As Int32, TargetHeight As Int32) As Drawing.Bitmap
        Dim bmSource = New Drawing.Bitmap(SourceImage)
        Return ResizeImage(bmSource, TargetWidth, TargetHeight)
    End Function

    Public Function ResizeImage(bmSource As Drawing.Bitmap, TargetWidth As Int32, TargetHeight As Int32) As Drawing.Bitmap
        Dim bmDest As New Drawing.Bitmap(TargetWidth, TargetHeight, Drawing.Imaging.PixelFormat.Format32bppArgb)

        Dim nSourceAspectRatio = bmSource.Width / bmSource.Height
        Dim nDestAspectRatio = bmDest.Width / bmDest.Height

        Dim NewX = 0
        Dim NewY = 0
        Dim NewWidth = bmDest.Width
        Dim NewHeight = bmDest.Height

        If nDestAspectRatio = nSourceAspectRatio Then
            'same ratio
        ElseIf nDestAspectRatio > nSourceAspectRatio Then
            'Source is taller
            NewWidth = Convert.ToInt32(Math.Floor(nSourceAspectRatio * NewHeight))
            NewX = Convert.ToInt32(Math.Floor((bmDest.Width - NewWidth) / 2))
        Else
            'Source is wider
            NewHeight = Convert.ToInt32(Math.Floor((1 / nSourceAspectRatio) * NewWidth))
            NewY = Convert.ToInt32(Math.Floor((bmDest.Height - NewHeight) / 2))
        End If

        Using grDest = Drawing.Graphics.FromImage(bmDest)
            With grDest
                .CompositingQuality = Drawing.Drawing2D.CompositingQuality.HighQuality
                .InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                .PixelOffsetMode = Drawing.Drawing2D.PixelOffsetMode.HighQuality
                .SmoothingMode = Drawing.Drawing2D.SmoothingMode.AntiAlias
                .CompositingMode = Drawing.Drawing2D.CompositingMode.SourceOver

                .DrawImage(bmSource, NewX, NewY, NewWidth, NewHeight)
            End With
        End Using

        Return bmDest
    End Function


    Public Sub Check_Only_Int(Sender As Object, e As KeyPressEventArgs)
        Select Case e.KeyChar
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", vbBack
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub

    Public Sub Check_Only_Float(Sender As Object, e As KeyPressEventArgs)
        Select Case e.KeyChar
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", vbBack
                If (e.KeyChar.ToString = ".") And (Sender.Text.Contains(e.KeyChar.ToString)) Then
                    e.Handled = True
                    Exit Sub
                End If
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub

    Public Sub Check_Only_Float_With_Negitave(Sender As Object, e As KeyPressEventArgs)
        Select Case e.KeyChar
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", "-", vbBack
                If (e.KeyChar.ToString = ".") And (Sender.Text.Contains(e.KeyChar.ToString)) Then
                    e.Handled = True
                    Exit Sub
                End If
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub

    Public Sub Computer_Setting_InsertDefult()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SysSetting_insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@CP_NAME", My.Computer.Name)
            .Parameters.AddWithValue("@logo", ConvertImage(SPLASH_FORM.PictureBox1.Image))
            .Parameters.AddWithValue("@CompName", "إسم المحل 1")
            .Parameters.AddWithValue("@englishN", "إسم المحل 2")
            If S_Orders = True Then
                .Parameters.AddWithValue("@SB_DefaultStatus", SB_DefaultStatus)
                .Parameters.AddWithValue("@Use_Order", 1)
            End If
            .Parameters.AddWithValue("@SScreenDefault", SScreenDefault)
            .Parameters.AddWithValue("@IM_Default_Stut", IM_Default_Stut)
        End With
        If SQL_SP_EXEC(c.Com) Then Get_Computer_Setting()
    End Sub

    'Public Function getMacAddress() As String
    '    Try
    '        Dim adapters As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()
    '        Dim adapter As NetworkInterface
    '        Dim myMac As String = String.Empty

    '        For Each adapter In adapters
    '            Select Case adapter.NetworkInterfaceType
    '                'Exclude Tunnels, Loopbacks and PPP
    '                Case NetworkInterfaceType.Tunnel, NetworkInterfaceType.Loopback, NetworkInterfaceType.Ppp
    '                Case Else
    '                    If Not adapter.GetPhysicalAddress.ToString = String.Empty And Not adapter.GetPhysicalAddress.ToString = "00000000000000E0" Then
    '                        myMac = adapter.GetPhysicalAddress.ToString
    '                        Exit For ' Got a mac so exit for
    '                    End If

    '            End Select
    '        Next adapter

    '        Return myMac
    '    Catch ex As Exception
    '        Return String.Empty
    '    End Try
    'End Function


    Public Function CpuId() As String
        Dim computer As String = "."
        Dim wmi As Object = GetObject("winmgmts:" & _
            "{impersonationLevel=impersonate}!\\" & _
            computer & "\root\cimv2")
        Dim processors As Object = wmi.ExecQuery("Select * from " & _
            "Win32_Processor")

        Dim cpu_ids As String = ""
        For Each cpu As Object In processors
            cpu_ids = cpu_ids & ", " & cpu.ProcessorId
        Next cpu
        If cpu_ids.Length > 0 Then cpu_ids = _
            cpu_ids.Substring(2)
        Return (cpu_ids)
    End Function


    Public Function GetDriveSerialNumber() As String

        '        Dim HDD_serial As String = ""
        '        Dim str As String = ""

        '        Dim objMan As New Management.ManagementClass("Win32_BaseBoard")
        '        Dim objCol As Management.ManagementObjectCollection = objMan.GetInstances

        '        Dim objItem As Management.ManagementObject

        '        For Each objItem In objCol
        '            Dim pi As Management.PropertyDataCollection = objItem.Properties
        '            HDD_serial = CType(pi.Item("SerialNumber").Value, String)
        '            '  Console.WriteLine("SerialNumber: " & CType(pi.Item("SerialNumber").Value, String))
        '            '  Console.WriteLine("Tag: " & CType(pi.Item("Tag").Value, String))
        '            str = HDD_serial
        '            GoTo NEXT_STEP
        '        Next

        'NEXT_STEP:
        '        Dim New_Str As String = ""
        '        For i = 0 To str.Count - 1

        '            Select Case str(i)
        '                Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" : New_Str += str(i)

        '                Case "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
        '                    Dim t As String = str(i)
        '                    New_Str += t.ToUpper()

        '                Case "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
        '                    New_Str += str(i)

        '            End Select

        '            If New_Str.Count = 4 Then GoTo FINISH
        '        Next

        '        While New_Str.Count < 4
        '            New_Str += "0"
        '        End While

        'FINISH:
        '        Return New_Str
        '--------------------------------------------------------------------------------------------------------

        Dim STR As String = ""
        Try
            ' Get the drive letter where the application is running
            Dim appPath As String = AppDomain.CurrentDomain.BaseDirectory
            Dim driveLetter As String = Path.GetPathRoot(appPath)

            ' Query WMI for the physical drives
            Dim searcher As New ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE DeviceID = '" & driveLetter.TrimEnd("\"c) & "'")
            For Each queryObj As ManagementObject In searcher.Get()
                Dim deviceId As String = queryObj("DeviceID").ToString()

                ' Query for the partition information
                Dim partitionSearcher As New ManagementObjectSearcher("ASSOCIATORS OF {Win32_LogicalDisk.DeviceID='" & deviceId & "'} WHERE AssocClass=Win32_LogicalDiskToPartition")
                For Each partitionObj As ManagementObject In partitionSearcher.Get()
                    Dim partitionDeviceId As String = partitionObj("DeviceID").ToString()

                    ' Query for the physical drive information
                    Dim diskSearcher As New ManagementObjectSearcher("ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" & partitionDeviceId & "'} WHERE AssocClass=Win32_DiskDriveToDiskPartition")
                    For Each diskObj As ManagementObject In diskSearcher.Get()
                        ' Get the hard drive serial number
                        Console.WriteLine("Hard Drive Serial Number: " & diskObj("SerialNumber").ToString())
                        STR = diskObj("SerialNumber").ToString()
                    Next
                Next
            Next
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try

        'MsgBox(STR)
        Return STR
    End Function




    Public Sub DeleteAll_Data()
        Dim c As New C
        Dim Tr As Boolean = MY_Settings.IsAttachDB

        With c.Com
            .Connection = c.Con
            .CommandText = "DeleteAll_Data"
            .CommandType = CommandType.StoredProcedure
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تـم تهيئة النظــام", MsgBoxStyle.Information)
            If S_Pr = True Then
                Pr_ID = 0
            Else
                Pr_ID = 1
            End If
            isPr_Open = False
            MY_Settings.IsAttachDB = Tr
            Save_AppSetting()
            'Computer_Setting_InsertDefult()
        End If
    End Sub

    Public Sub AG_Balance_Delete()
        Dim c As New C
        Dim Tr As Boolean = MY_Settings.IsAttachDB
        With c.Com
            .Connection = c.Con
            .CommandText = "Delete_AG_Balance"
            .CommandType = CommandType.StoredProcedure
        End With
        If SQL_SP_EXEC(c.Com) = True Then MsgBox("تـم ترحيل اصدة العملاء والخزينة", MsgBoxStyle.Information)
    End Sub

    Public Sub DeleteAllBuyAndSell()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "DeleteAll_BuysAndSales"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Delete_ALL_Qty", F_SysDelete.Delete_ALL_Qty_CB.Checked)
            If String.IsNullOrWhiteSpace(F_SysDelete.Jrd_ID_Txt.Text) = False Then .Parameters.AddWithValue("@Jrd_ID", F_SysDelete.Jrd_T_ID)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تـــم ترحيل ارصدة البيع والشــراء", MsgBoxStyle.Information)
            If S_Pr = True Then
                Pr_ID = 0
            Else
                Pr_ID = 1
            End If
            isPr_Open = False
        End If
    End Sub

    Public Sub DeleteAll_STBalance()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "[DeleteAll_STBalance]"
            .CommandType = CommandType.StoredProcedure
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تـــم تهيئة المخزن", MsgBoxStyle.Information)
        End If
    End Sub

    Public Sub Arabic_Lang()
        Application.CurrentInputLanguage = InputLanguage.FromCulture(New CultureInfo("ar-sa"))
    End Sub

    Public Sub Save_Total(T_ID As Integer, Total As Double, Disc As Double)

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Update_Total"
        sqlComm.CommandType = CommandType.StoredProcedure
        'sqlComm.Parameters.AddWithValue("@Form_Type", FormType)
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@Total", Total)
        sqlComm.Parameters.AddWithValue("@Disc", Disc)
        SQL_SP_EXEC(sqlComm)

    End Sub

    Public Sub Save_AG_Name(T_ID As Integer, AG_ID As Integer, ON_UPDATE As Boolean)
        If AG_ID > 0 Then

            Dim sqlComm As New SqlClient.SqlCommand
            sqlComm.CommandText = "AG_Balance_Update_AG"
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
            sqlComm.Parameters.AddWithValue("@AG_ID", AG_ID)
            sqlComm.Parameters.AddWithValue("@ON_UPDATE", ON_UPDATE)
            SQL_SP_EXEC(sqlComm)

        End If
    End Sub

    Public Sub Save_ReferNum(T_ID As Integer, ReferNum As String)
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Update_ReferNum"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@ReferNum", ReferNum)
        SQL_SP_EXEC(sqlComm)
    End Sub

    Public Function DependingBill(T_ID As Integer, Optional PAY_ID As Integer = 1, Optional Tr_ID As Integer = 1)

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Update_isDepended"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        If isPr_Open Then sqlComm.Parameters.AddWithValue("@Pr_ID", Pr_ID)

        If PAY_ID = 1 Then

            Select Case FormType
                Case 2, 6
                    sqlComm.Parameters.AddWithValue("@Tr_ID", PCH_TR_ID)
                Case 5
                    sqlComm.Parameters.AddWithValue("@Tr_ID", SB_TR_ID)
                Case 8
                    sqlComm.Parameters.AddWithValue("@Tr_ID", EXP_TR_ID)
            End Select

        Else

            sqlComm.Parameters.AddWithValue("@Tr_ID", Tr_ID)

        End If

        sqlComm.Parameters.AddWithValue("@Pay_ID", PAY_ID)


        If SQL_SP_EXEC(sqlComm) = True Then
            'MsgBox("تم الإعتماد ", MsgBoxStyle.Information)
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub DependingUpdatedBill(T_ID As Integer)
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_DependingUpdatedBill"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        SQL_SP_EXEC(sqlComm)
    End Sub

    Public Sub Void_Row(T_ID As Integer)
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "AG_Balance_Void_Row"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تم إلغــاء المعاملة ", MsgBoxStyle.Information)

            Network_Edit_Tracker_insert("إلغاء الإيصال", F_Receipt.ReceiptNum_Txt.Text, AG_Type, 3)

            If FormType = 1 Then Select_Sales_Receipt(F_Sales.T_ID)
            If FormType = 2 Then Select_Pch_Receipt(F_Pch.T_ID)
            If FormType = 8 Then Select_EX_Receipt(F_EXP_Details.T_ID)
            F_Receipt.Void_Lb.Visible = True
            F_Receipt.Treasury_ComboBox.Text = Show_TR_T_Balance(F_Receipt.Treasury_ComboBox.SelectedValue)
            F_Receipt.print_butt.Enabled = False
            F_Receipt.Edit_butt.Enabled = False
            F_Receipt.DeleteButton.Enabled = False

        End If

    End Sub

    Public Sub Save_Date(T_ID As Integer, DateTime As DateTimePicker)

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Update_Date"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@Date", DateTime.Value)
        sqlComm.Parameters.AddWithValue("@Month", DateTime.Value.Month)
        sqlComm.Parameters.AddWithValue("@YEAR", DateTime.Value.Year)

        SQL_SP_EXEC(sqlComm)

    End Sub

    Public Sub AG_Balance_Update_Travel(T_ID As Integer, Travel_ID As Integer, Cr_ID As Integer, Cr_Adress As String, Cr_Phone As String)
        If Cr_ID <= 0 Then Cr_ID = 1
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "[AG_Balance_Update_Travel]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@Travel_ID", Travel_ID)

        sqlComm.Parameters.AddWithValue("@Cr_ID", Cr_ID)
        sqlComm.Parameters.AddWithValue("@Cr_Adress", Cr_Adress)
        sqlComm.Parameters.AddWithValue("@Cr_Phone", Cr_Phone)

        SQL_SP_EXEC(sqlComm)
    End Sub

    Public Sub Save_Title_Name(T_ID As Integer, Title As String)

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Update_Title_Name"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@Title", Title)
        SQL_SP_EXEC(sqlComm)
    End Sub

    Public Sub AG_Balance_Update_Cr_Phone(T_ID As Integer, Notes As String)
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Update_Cr_Phone"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        If String.IsNullOrWhiteSpace(Notes) = False Then sqlComm.Parameters.AddWithValue("@Cr_Phone", Notes)
        SQL_SP_EXEC(sqlComm)
    End Sub

    Public Sub Save_About(T_ID As Integer, Notes As String)
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Update_About"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        If String.IsNullOrWhiteSpace(Notes) = False Then sqlComm.Parameters.AddWithValue("@About", Notes)
        SQL_SP_EXEC(sqlComm)
    End Sub

    Public Sub AG_Balance_Update_Marketer(T_ID As Integer, M_ID As Integer)

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Update_Marketer"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        If M_ID > 0 Then sqlComm.Parameters.AddWithValue("@M_ID", M_ID)
        SQL_SP_EXEC(sqlComm)
    End Sub

    Public Sub AG_Balance_Update_Custody(T_ID As Integer, C_ID As Integer)
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Update_Custody"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@C_ID", C_ID)
        SQL_SP_EXEC(sqlComm)
    End Sub

    Public Sub Select_Pch_Receipt(T_ID As Integer)
        Dim C = New C
        Try
            F_Pch.Receipts_DT.Clear()
            Dim S As String = " select T_ID,Receipt_Num,Type_Name,Value from Pch_Receipts_V WHERE Receipt_Tran_ID = '" & T_ID & "' AND isVoid = 0"
            ' Order By T_ID ASC "
            C.Da = New SqlClient.SqlDataAdapter(S, C.Con)
            C.Da.Fill(F_Pch.Receipts_DT)
            F_Pch.ReceiptsMetroGrid.DataSource = F_Pch.Receipts_DT
            If String.IsNullOrWhiteSpace(F_Pch.CreditTextBox.Text) Then F_Pch.CreditTextBox.Text = "0.000"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Select_Sales_Receipt(T_ID As Integer)
        Dim C = New C
        Try
            F_Sales.Receipts_DT.Clear()
            Dim S As String = " select T_ID,PAYMENT_NAME,Receipt_Num,Type_Name,Value from SB_Receipts_V WHERE Receipt_Tran_ID = '" & T_ID & "' AND isVoid = 0"
            ' Order By T_ID ASC "
            C.Da = New SqlClient.SqlDataAdapter(S, C.Con)
            C.Da.Fill(F_Sales.Receipts_DT)
            F_Sales.ReceiptsMetroGrid.DataSource = F_Sales.Receipts_DT
            If String.IsNullOrWhiteSpace(F_Sales.Piedmoney_txt.Text) Then F_Sales.Piedmoney_txt.Text = "0.000"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Select_EX_Receipt(T_ID As Integer)
        Dim C = New C
        Try
            F_EXP_Details.Receipts_DT.Clear()
            Dim S As String = " select T_ID,Receipt_Num,Type_Name,Value from EXP_Receipts_V WHERE Receipt_Tran_ID = '" & T_ID & "' AND isVoid = 0"
            ' Order By T_ID ASC "
            C.Da = New SqlClient.SqlDataAdapter(S, C.Con)
            C.Da.Fill(F_EXP_Details.Receipts_DT)
            F_EXP_Details.ReceiptsMetroGrid.DataSource = F_EXP_Details.Receipts_DT
            If String.IsNullOrWhiteSpace(F_EXP_Details.CreditTextBox.Text) Then F_EXP_Details.CreditTextBox.Text = "0.000"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function GetLastDayOfMonth(DateTimePicker_To As DateTimePicker, Month As Integer)
        'set return value to the last day of the month for any date passed in to the method create a datetime variable set to the passed in date
        DateTimePicker_To.Text = New Date(Now.Year, Month, 1)
        Dim dtTo As Date = DateTimePicker_To.Text
        'overshoot the date by a month
        dtTo = dtTo.AddMonths(1)
        'remove all of the days in the next month to get bumped down to the last day of the previous month
        dtTo = dtTo.AddDays(-(dtTo.Day))
        'return the last day of the month
        DateTimePicker_To.Text = dtTo
        Return DateTimePicker_To
    End Function

    'Get the first day of the month
    Public Function GetFirstDayOfMonth(DateTimePicker As DateTimePicker, Month As Integer)
        DateTimePicker.Text = New Date(Now.Year, Month, 1)
        Return DateTimePicker
    End Function

    Public Sub TextNumberKeypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Check_Only_Float(sender, e)
    End Sub

    Public Sub Set_Form(ByRef Ctrl As Form, Pl As Panel)
        Pl.Controls.Clear()
        Ctrl.TopLevel = False
        Ctrl.Visible = True
        Pl.Controls.Add(Ctrl)
        Ctrl.Parent = Pl
        Ctrl.Size = New System.Drawing.Size(Pl.Size.Width, Pl.Size.Height)
        Pl.Tag = Ctrl.Name
    End Sub

    Public Sub Network_Edit_Tracker_insert(Notes As String, Bill_ID As String, Screen_Type As Integer, Operation_ID As Integer)
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "Network_Edit_Tracker_insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@User_ID", USER_ID)
            .Parameters.AddWithValue("@Notes", Notes)
            .Parameters.AddWithValue("@Bill_ID", Bill_ID)
            .Parameters.AddWithValue("@Screen_Type", Screen_Type)
            .Parameters.AddWithValue("@Operation_ID", Operation_ID)
            .Parameters.AddWithValue("@CP_Name", My.Computer.Name)
            '.Parameters.AddWithValue("@DATE", Date.Now.ToString("yyyy-MM-dd HH:mm:ss.mmm"))
        End With
        SQL_SP_EXEC(c.Com)
    End Sub


    Public Function Get_Barcode_U_IM_ID()
        Dim c As New C
        Dim U_IM_ID As Integer
        Try
            Dim s As String
            s = "SELECT ISNULL(MAX(U_IM_ID),0) + 1000 AS MX FROM IM_Units WHERE   U_IM_ID   BETWEEN " & START_ID & " AND " & END_ID
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                U_IM_ID = c.Dr("MX")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return U_IM_ID
    End Function


    Public Sub Delete_Last_Empty_Bill(T_ID As Integer)
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Delete_Last_Empty_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID)
            SQL_SP_EXEC(C.Com)
        End With
    End Sub

    'Public Sub Pending_DataBase()
    '    Dim Con_Str As String = "Data Source= " + My_Settings.S_SERVER + " ;initial catalog = MASTER ;Integrated Security=True;"
    '    Dim Con = New SqlConnection(Con_Str)

    '    Dim C As New C
    '    With C.Com
    '        .Connection = Con
    '        .CommandText = "DB_Pend"
    '        .CommandType = CommandType.StoredProcedure

    '        If SQL_SP_EXEC(C.Com) = True Then MsgBox("تم إصلاح قاعدة البيانات ", MsgBoxStyle.Information)
    '    End With
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
            'MsgBox(ex.Message)
        End Try
        '-------------------------------------------------------------------------------
    End Sub

    <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
    Public Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, <MarshalAs(UnmanagedType.LPWStr)> ByVal lParam As String) As Int32
    End Function

    'Public Sub ExportButton_Setting()
    '    Try
    '        Dim path As String = Application.StartupPath & "\Settings Files\" & My.Computer.Name & "_BackUpSettings.AppSettings"
    '        Using sWriter As New StreamWriter(path)
    '            For Each setting As Configuration.SettingsPropertyValue In My.Settings.PropertyValues

    '                sWriter.WriteLine(setting.Name & ";" & setting.PropertyValue.ToString())

    '            Next

    '        End Using

    '        My.Settings.Save()
    '        ' MessageBox.Show("تم اخذ نسخة احتياطية من الاعدادات", "النظام", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    Catch ex As Exception
    '        'Logger.Log(ex, "", "", System.Reflection.MethodBase.GetCurrentMethod().Name)
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub


    Public Sub Recover_Setting()

        Try
            Dim filePath = Environment.ExpandEnvironmentVariables("%userprofile%\AppData\Local\CLASS")
            Directory.Delete(filePath, True)
            MessageBox.Show("تم تهيئة الاعدادات بنجاح", "النظام", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Application.Exit()
            'Application.ExitThread()

        Catch ex As Exception
            'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
            MsgBox(ex.Message)
        End Try

        'Try
        '    Dim path As String = Application.StartupPath & "\Settings Files\" & My.Computer.Name & "_BackUpSettings.AppSettings"
        '    If File.Exists(path) Then

        '        Using sReader As New StreamReader(path)

        '            While sReader.Peek() > 0
        '                Try
        '                    Dim input = sReader.ReadLine()

        '                    ' Split comma delimited data ( SettingName,SettingValue )  
        '                    Dim dataSplit = input.Split(CChar(";"))
        '                    'If dataSplit(1) = "True" Or dataSplit(1) = "False" Then
        '                    '    '           Setting         Value  
        '                    '    My_Settings(dataSplit(0)) = Convert.ToBoolean(dataSplit(1))
        '                    'Else
        '                    '    My_Settings(dataSplit(0)) = dataSplit(1)
        '                    'End If

        '                    Check_Setting(dataSplit(0), dataSplit(1))

        '                Catch ex As Exception

        '                End Try
        '            End While

        '        End Using

        '        Save_AppSetting()

        '        MessageBox.Show("تم تحميل الاعدادات بنجاح ... قم بالخروج من الظام تم قم بالدخول مجددا", "النظام", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        Application.Exit()
        '        Application.ExitThread()

        '    Else
        '        MessageBox.Show("لم يتم ايجاد ملف الاعدادات الاحتياطي ... فشل استعادة الاعدادات", "النظام", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show("يوجد خطأ ... فشل استعادة الاعدادات" & vbNewLine & ex.Message, "النظام", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub


    Public Sub Switch_To_DV_Show()
        Dim F As New DGV_Control
        F.TabControl1.TabPages.Remove(F.StTabPage)
        F.TabControl1.TabPages.Remove(F.ItemPriceTabPage)
        F.Show()
    End Sub


    Public Function Show_AG_T_Balance(AG_ID As Integer)
        Dim c As New C
        Dim N As Double = 0
        Try
            Dim s As String
            s = "SELECT isnull(T_Balance,0) as T_Balance FROM [AGENTS_MENU_V] WHERE AG_ID = '" & AG_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                N = c.Dr("T_Balance")
                Return N.ToString("N")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return 0
    End Function

    Public Function Show_TR_T_Balance(TR_ID As Integer)
        Dim c As New C
        Dim N As Double = 0
        Try
            Dim s As String
            s = "SELECT isnull(T_BALANCE,0) as T_Balance FROM [TR_MENU_V] WHERE TR_ID = '" & TR_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                N = c.Dr("T_Balance")
                Return N.ToString("N")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return 0
    End Function


    Public Function St_Count()
        Dim c As New C
        Try
            Dim s As String
            s = "SELECT COUNT(ST_ID) AS N FROM STORES"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Return c.Dr("N")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return 1
    End Function

    Public Function CHECK_IF_AGENT_SKIP_MAX_DEBIT(AG_ID As Integer)
        Dim c As New C
        Try
            Dim s As String
            s = "SELECT Max_Debit,(T_Balance * -1) AS T_Balance FROM AGENTS_MENU_V WHERE AG_ID = '" & AG_ID & "' AND Max_Debit > 0"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                If c.Dr("T_Balance") > c.Dr("Max_Debit") Then Return 1
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return 0
    End Function


    Public Function Ge_St_Items() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "-----كل المخازن-----"))

        Dim c1 As New C
        Dim s As String = "select ST_ID AS ID,ST_name AS name from STORES ORDER By ST_ID ASC"
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

    Public Function PrinterState(Namee As String)
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
                        Return False
                    Else
                        'MsgBox("الطابعــة متصلــة", MsgBoxStyle.Information, "")
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

    End Function


    Public Sub Bill_Perfet_Select_For_Bill(T_ID As Integer)
        Dim C As New C
        With C.Com
            .CommandText = "[Bill_Perfet_Select_For_Bill]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Value", SqlDbType.Float, 0)
            .Parameters.AddWithValue("@T_ID", T_ID)
            .Parameters("@Value").Direction = ParameterDirection.Output
        End With

        If SQL_SP_EXEC(C.Com) = True Then
            MsgBox(" ربح الفاتورة : " + C.Com.Parameters("@Value").Value.ToString(), MsgBoxStyle.Information)
        End If

    End Sub

    Public Function IM_Serach(IM_SH As String) As String

        Dim words As String() = IM_SH.Split(New Char() {" "c})
        Dim Str As String = ""
        Dim IM_Str As String = ""
        Dim S_and As String = " and item_name Like "

        If words.Length() = 1 Then
            Str = "select IM_ID,item_name,IM_NUM,isValid from IM_All_V WHERE item_name Like '%" & words(0) & "%' Order by item_name ASC"
        Else
            IM_Str = "'%" & words(0) & "%'" & S_and

            For i = 1 To words.Length - 1

                If i = words.Length - 1 Then
                    IM_Str += "'%" & words(i) & "%'"
                Else
                    IM_Str += "'%" & words(i) & "%'" & S_and
                End If
            Next
            Str = "select IM_ID,item_name,IM_NUM,isValid from IM_All_V WHERE item_name Like " & IM_Str & " Order by item_name ASC"

        End If
        Return Str
    End Function

    Public Function IM_Serach_With_QTY(IM_SH As String) As String

        Dim words As String() = IM_SH.Split(New Char() {" "c})
        Dim Str As String = ""
        Dim IM_Str As String = ""
        Dim S_and As String = " and item_name Like "

        If words.Length() = 1 Then
            Str = "select IM_ID,item_name,isValid from IM_All_V_With_QTY WHERE item_name Like '%" & words(0) & "%' Order by item_name ASC"
        Else
            IM_Str = "'%" & words(0) & "%'" & S_and

            For i = 1 To words.Length - 1

                If i = words.Length - 1 Then
                    IM_Str += "'%" & words(i) & "%'"
                Else
                    IM_Str += "'%" & words(i) & "%'" & S_and
                End If
            Next
            Str = "select IM_ID,item_name,isValid from IM_All_V_With_QTY WHERE item_name Like " & IM_Str & " Order by item_name ASC"

        End If
        Return Str

    End Function

    Public Function IM_Serach_Auto_Format(IM_SH As String) As String

        Dim words As String() = IM_SH.Split(New Char() {" "c})
        Dim Str As String = ""
        Dim IM_Str As String = ""
        Dim S_and As String = " and item_name Like "

        If words.Length() = 1 Then
            Str = "select IM_ID,item_name,isValid from IM_All_V WHERE item_name Like '%" & words(0) & "%' Order by item_name ASC"
        Else
            IM_Str = "'%" & words(0) & "%'" & S_and

            For i = 1 To words.Length - 1

                If i = words.Length - 1 Then
                    IM_Str += "'%" & words(i) & "%'"
                Else
                    IM_Str += "'%" & words(i) & "%'" & S_and
                End If
            Next
            Str = "select IM_ID,item_name,isValid from IM_All_V WHERE item_name Like " & IM_Str & " Order by item_name ASC"

        End If
        Return Str

    End Function

    Public Function IM_Serach_barcode(IM_SH As String) As String

        Dim words As String() = IM_SH.Split(New Char() {" "c})
        Dim Str As String = ""
        Dim IM_Str As String = ""
        Dim S_and As String = " and item_name Like "

        If words.Length() = 1 Then
            Str = "select U_IM_ID,IM_ID,item_name,U_Name,Barcode from IM_units_Search_V WHERE item_name Like '%" & words(0) & "%' AND Barcode <>'' Order by item_name ASC"
        Else
            IM_Str = "'%" & words(0) & "%'" & S_and

            For i = 1 To words.Length - 1

                If i = words.Length - 1 Then
                    IM_Str += "'%" & words(i) & "%'"
                Else
                    IM_Str += "'%" & words(i) & "%'" & S_and
                End If
            Next
            Str = "select U_IM_ID,IM_ID,item_name,U_Name,Barcode from IM_units_Search_V WHERE item_name Like " & IM_Str & " AND Barcode <>'' Order by item_name ASC"

        End If
        Return Str
    End Function

    Public Function AG_Serach(IM_SH As String) As String

        Dim words As String() = IM_SH.Split(New Char() {" "c})
        Dim Str As String = ""
        Dim IM_Str As String = ""
        Dim S_and As String = " and item_name Like "

        If words.Length() = 1 Then
            Str = "Select AG_ID,Ag_name,isnull(T_Balance,0) AS T_Balance from Agents WHERE Ag_name Like '%" & words(0) & "%' AND AG_ID NOT IN('" & U_AG_ID & "') Order by Ag_name ASC "
        Else
            IM_Str = "'%" & words(0) & "%'" & S_and

            For i = 1 To words.Length - 1

                If i = words.Length - 1 Then
                    IM_Str += "'%" & words(i) & "%'"
                Else
                    IM_Str += "'%" & words(i) & "%'" & S_and
                End If
            Next
            Str = "Select AG_ID,Ag_name,isnull(T_Balance,0) AS T_Balance from Agents WHERE Ag_name Like " & IM_Str & " AND AG_ID NOT IN('" & U_AG_ID & "') Order by Ag_name ASC "

        End If
        Return Str
    End Function


    Public Function Ge_Tr_Items() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "---خزينة النظام الإفتراضية---"))

        Dim c1 As New C
        Dim s As String = "select Tr_ID AS ID,Tr_Name AS name FROM TR_MENU_V ORDER By Tr_ID ASC"
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


    Public Function SELECT_BARCODE(IM_ID As Integer, U_ID As Integer)
        Dim c As New C
        Dim Barcode_IM As String = ""
        Try
            Dim s As String
            s = "select ISNULL(BARCODE,'') AS BARCODE from IM_UNITS WHERE IM_ID = '" & IM_ID & "' AND U_ID = '" & U_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Barcode_IM = c.Dr("BARCODE")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return Barcode_IM
    End Function


    Public Sub Load_IM_By_ID(ByRef IMDataGridViewX As DataGridView)
        Dim c As New C
        Dim IM_DT As New DataTable
        Try
            IM_Dt.Clear()
            c.Str = "select IM_ID,item_name,isValid from IM_All_V WHERE IM_ID = '" & GLOBAL_IM_ID & "' Order by item_name ASC"
            c.Da = New SqlClient.SqlDataAdapter(c.Str, c.Con)
            c.Da.Fill(IM_Dt)
            IMDataGridViewX.DataSource = IM_Dt
            If IM_Dt.Rows.Count > 0 Then
                IMDataGridViewX.Visible = True
                IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
            Else
                IMDataGridViewX.Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Function Check_Unit_Exist(ByRef IM_Unit_cm As ComboBox, ByVal Unit_cargo As String)
        Dim c As New C
        Try
            Dim s As String
            s = "select U_ID from Units WHERE U_Name = '" & IM_Unit_cm.Text & "' AND U_Cargo = '" & Unit_cargo & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                IM_Unit_cm.SelectedValue = c.Dr("U_ID")
                Return 1
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return 0
    End Function

    Public Function GET_Unit_Exist(ByRef IM_Unit_Str As String, ByVal Unit_cargo As String)
        Dim c As New C
        Dim U_ID As Integer
        Try
            Dim s As String
            s = "select U_ID from Units WHERE U_Name = '" & IM_Unit_Str & "' AND U_Cargo = '" & Unit_cargo & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            c.Dr.Read()
            U_ID = c.Dr("U_ID")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return U_ID
    End Function

    Public Sub Unit_Insert(ByRef IM_Unit_cm As ComboBox, Unit_cargo As String, Form_Index As Integer)
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "Unit_Insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@U_ID", 0)
            .Parameters.AddWithValue("@U_Name", IM_Unit_cm.Text)
            .Parameters("@U_ID").Direction = ParameterDirection.Output
            If String.IsNullOrWhiteSpace(Unit_cargo) = False Then
                If Convert.ToDouble(Unit_cargo) = 0 Then
                    Unit_cargo = "1"
                    .Parameters.AddWithValue("@U_Cargo", 1)
                Else
                    .Parameters.AddWithValue("@U_Cargo", Unit_cargo)
                End If
            Else
                .Parameters.AddWithValue("@U_Cargo", 1)
            End If

        End With
        If SQL_SP_EXEC(c.Com) = True Then
            Network_Edit_Tracker_insert("(من شاشة إضافة صنف) الوحدة:" & IM_Unit_cm.Text & " الحمولة:" & Unit_cargo, 0, Form_Index, 1)
            Load_Units(IM_Unit_cm)
            IM_Unit_cm.SelectedValue = c.Com.Parameters("@U_ID").Value.ToString()
        End If
    End Sub

    Public Sub Load_Units(ByRef IM_Unit_cm As ComboBox)
        Dim c As New C
        Try
            Dim sql As String = " select U_ID,U_Name from Units " ' Where U_ID <> 1
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

    Public Sub IM_Fetch_Unit_Cargo(ByRef IM_Unit_cm As ComboBox, ByRef Unit_cargo_txt As TextBox)
        Dim c As New C
        Try
            Dim s As String
            s = "select U_Cargo from Units WHERE U_ID = '" & IM_Unit_cm.SelectedValue & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Unit_cargo_txt.Text = c.Dr("U_Cargo")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Function CheckConnection(Cs As String)
        Dim bb As New SqlConnection
        Try

            bb.ConnectionString = Cs
            'MsgBox(bb.ConnectionTimeout)
            bb.Open()
            bb.Close()

            Return True
        Catch ex As Exception
            bb.Close()
            Return False
        End Try
    End Function

    Public Function query(ByVal query_Str As String)
        '  On Error Resume Next
        Dim C As New C
        C.Com = New SqlCommand(query_Str, C.Con)
        C.Con.Open()
        Try
            C.Com.ExecuteNonQuery()
            C.Con.Close()
            Return 1
        Catch ex As Exception
            MsgBox(ex.Message)
            C.Con.Close()
            Return 0
        End Try

        Return 0
    End Function

    Public Function Show_IM_Cost(Show_Msg As Boolean, IM_ID As Integer, U_ID As Integer)
        Dim c As New C
        Dim Cost As Double = 0
        Dim item_name As String = ""
        Dim U_NAME As String = ""
        Try
            Dim s As String
            s = "select ISNULL(Cost,0) AS Cost , U_Cargo , U_NAME , item_name from IM_Menu_Units_V WHERE IM_ID = '" & IM_ID & "' AND U_ID = '" & U_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Cost = c.Dr("Cost") * c.Dr("U_Cargo")
                U_NAME = c.Dr("U_NAME")
                item_name = c.Dr("item_name")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If Show_Msg = True Then MsgBox("  الوحدة : " + U_NAME + "  التكلفة : " + Cost.ToString, MsgBoxStyle.Information, " تكلفة الصنف : " + item_name)
        Return Cost
    End Function

    Public Sub Fetch_IM_Valids(ByRef Valid_Dt As DataTable, ByRef Valid_cm As ComboBox, ByVal IM_ID As Integer, ByVal ST_cm As ComboBox)
        Valid_Dt.Clear()
        Dim c As New C
        Try
            Dim s As String
            s = "select D_Vaild from ST_Balance_V WHERE IM_ID = '" & IM_ID & "' AND ST_ID = '" & ST_cm.SelectedValue & "' AND QTY <> 0 Order By D_Vaild  ASC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(Valid_Dt)
            Valid_cm.DataSource = Valid_Dt
            Valid_cm.DisplayMember = "D_Vaild"
            If Valid_Dt.Rows.Count > 0 Then Valid_cm.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub



    Public Sub IM_Fetch_QTY_OfValid(ByVal IM_ID As Integer, ByVal ST_cm As ComboBox, ByVal Valid_cm As ComboBox, ByRef Valid_QTY_txt As TextBox, ByVal U_Cargo As Double)
        Dim c As New C
        Try
            Dim s As String
            s = "select ISNULL(SUM(QTY),0) AS QTY from ST_Balance_V WHERE IM_ID = '" & IM_ID & "' AND ST_ID = '" & ST_cm.SelectedValue & "' And D_Vaild = '" & Valid_cm.Text & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Valid_QTY_txt.Text = c.Dr("QTY") / U_Cargo
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Public Sub IM_Fetch_QTY_OfValid_ST_INT(ByVal IM_ID As Integer, ByVal ST_ID As Integer, ByVal Valid_cm As ComboBox, ByRef Valid_QTY_txt As TextBox, ByVal U_Cargo As Double)
        Dim c As New C
        Try
            Dim s As String
            s = "select ISNULL(SUM(QTY),0) AS QTY from ST_Balance_V WHERE IM_ID = '" & IM_ID & "' AND ST_ID = '" & ST_ID & "' And D_Vaild = '" & Valid_cm.Text & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Valid_QTY_txt.Text = c.Dr("QTY") / U_Cargo
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Public Sub Load_IM_ALL_QTY(ByVal IM_ID As Integer, ByRef ALL_QTY As Double, ByRef ALL_QTY_txt As TextBox, ByVal U_Cargo As Double)
        Dim c As New C
        Try
            Dim s As String
            s = "select ISNULL(SUM(QTY),0) AS QTY from ST_Balance_V WHERE IM_ID = '" & IM_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                ALL_QTY = c.Dr("QTY")
                ALL_QTY_txt.Text = (ALL_QTY / U_Cargo) '.ToString("0.00")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Public Sub Load_IM_ST_QTY(ByVal IM_ID As Integer, ByVal ST_cm As ComboBox, ByRef IM_QTY As Double)
        Dim c As New C
        Try
            Dim s As String
            s = "select ISNULL(SUM(QTY),0) AS QTY from ST_Balance_V WHERE IM_ID = '" & IM_ID & "' AND ST_ID = '" & ST_cm.SelectedValue & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                IM_QTY = c.Dr("QTY")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Load_IM_ST_QTY_ST_INT(ByVal IM_ID As Integer, ByVal ST_ID As Integer, ByRef IM_QTY As Double)
        Dim c As New C
        Try
            Dim s As String
            s = "select ISNULL(SUM(QTY),0) AS QTY from ST_Balance_V WHERE IM_ID = '" & IM_ID & "' AND ST_ID = '" & ST_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                IM_QTY = c.Dr("QTY")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Public Function IsIdInRange(id As Integer)
        Try
            If id >= START_ID And id < END_ID Then
                Return True
            Else
                Return False
            End If
            Return False

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Check_For_OpenPierod()
        Dim C As New C
        C.Con.Open()
        With C.Com
            .Connection = C.Con
            .CommandText = "Check_For_OpenPierod"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@USER_ID", USER_ID)
        End With

        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            C.Dr.Read()
            isPr_Open = True

            Pr_ID = C.Dr("Pr_ID")
            U_Flate_ID = C.Dr("TB_Flate_ID")

            Return 1
        Else
            isPr_Open = False
            Pr_ID = 1
            Return 0

        End If
        C.Con.Close()


        Return 0
    End Function


    Public Sub AG_Balance_UN_Void_Row(T_ID As Integer, Bill_ID As Integer, ag_type As Integer)

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_UN_Void_Row"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)

        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تم التراجع", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" تراجع عن إلغاء فاتورة ", Bill_ID, ag_type, 3)
        End If

    End Sub


    Public Sub Fetch_Pr_Details_()
        Dim c As New C
        Dim Cost As Double = 0
        Try
            Dim s As String
            s = "SELECT ISNULL(SUM(Pure),0) AS P FROM Agents_Balance_MV_RCT_V WHERE isVoid = 0 AND isDepended = 1 AND Pr_ID = '" & Pr_ID & "' AND BsType_ID = 3"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                MsgBox(" إجمالي المقبوض للورديــة : " + c.Dr("P").ToString, MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Public Sub AG_Balance_Update_Deliver_date(Deliver_DateTimePicker1 As DateTimePicker)

        Dim sqlComm As New SqlClient.SqlCommand
            sqlComm.CommandText = "[AG_Balance_Update_Deliver_date]"
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
            sqlComm.Parameters.AddWithValue("@Date", Deliver_DateTimePicker1.Value)
        SQL_SP_EXEC(sqlComm)

    End Sub

    Public Sub AG_Balance_Update_Date_Deliver(T_ID As Integer, DateTime As DateTimePicker)

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Update_Date_Deliver"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        If DateTime.Checked = True Then sqlComm.Parameters.AddWithValue("@Date", DateTime.Value)
        SQL_SP_EXEC(sqlComm)
    End Sub

    Public Function Serach(IM_SH As String, _SearchColumn As String) As String
        Dim words As String() = IM_SH.Split(New Char() {" "c})
        Dim Str As String = ""
        Dim IM_Str As String = ""
        Dim S_and As String = " and " & _SearchColumn & " Like "

        If words.Length() = 1 Then
            Str = _SearchColumn & "  = '" & IM_SH & "'  or " & _SearchColumn & "  like '%" & words(0) & "%' or " & _SearchColumn & "  like '%" & IM_SH & "' or " & _SearchColumn & "  like '" & IM_SH & "%'"
        Else
            IM_Str = "'%" & words(0) & "%'" & S_and

            For i = 1 To words.Length - 1

                If i = words.Length - 1 Then
                    IM_Str += "'%" & words(i) & "%'"
                Else
                    IM_Str += "'%" & words(i) & "%'" & S_and
                End If
            Next


            Str = _SearchColumn & "  like " & IM_Str

        End If
        Return Str
    End Function

    Public Sub check_hide_tr(cmb As ComboBox, ByRef balance As TextBox)
        If TypeName(cmb.SelectedValue) = "Integer" Then

            If U_Hide_Unrelated_Tr = True Then
                If cmb.SelectedValue <> SB_TR_ID Then
                    balance.UseSystemPasswordChar = True
                Else
                    balance.UseSystemPasswordChar = False
                End If
            End If

        End If
    End Sub

    Public Sub EXCEL_EXPORT(gridv As DataGridView)
        Try
            Const stXL_SUBKEY As String = "\Excel.Application\CurVer"
            Dim rkVersionKey As Microsoft.Win32.RegistryKey = Nothing
            rkVersionKey = Registry.ClassesRoot.OpenSubKey(name:=stXL_SUBKEY, writable:=False)
            If rkVersionKey Is Nothing Then
                'not installed
                MessageBox.Show("الرجاء تثبيت حزمة Microsoft Office أولا", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                If gridv.RowCount > 0 Then
                    Dim xlApp As Excel.Application
                    Dim xlWorkBook As Excel.Workbook
                    Dim xlWorkSheet As Excel.Worksheet
                    Dim misValue As Object = System.Reflection.Missing.Value
                    Dim i As Integer
                    Dim j As Integer
                    ' xlApp = New Excel.Application
                    xlApp = CreateObject("Excel.Application")
                    xlWorkBook = xlApp.Workbooks.Add(misValue)
                    xlWorkSheet = xlWorkBook.Sheets(1)
                    For Each col As DataGridViewColumn In gridv.Columns
                        If col.Visible Then
                            xlWorkSheet.Cells(1, col.Index + 2) = col.HeaderText.ToString
                        End If
                    Next
                    For i = 0 To gridv.Rows.Count - 1
                        For j = 0 To gridv.ColumnCount - 1
                            If gridv.Columns(j).Visible Then
                                xlWorkSheet.Cells(i + 2, j + 2) = gridv(j, i).Value.ToString()
                            End If
                        Next
                    Next
                    xlApp.Visible = True
                    xlWorkBook.Activate()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
            'Class1.con.Close()
        End Try
    End Sub

    Public Sub Update_Discount(T_ID As Integer, Discount As Double, Optional Percent_Discount As Double = 0)

        If Percent_Discount = 0 Then
            query("Update Agents_Balance_MV SET Discount = " & Discount & " WHERE T_ID = " & T_ID)
        Else
            query("Update Agents_Balance_MV SET Discount = " & Discount & " , Debit_2 = " & Percent_Discount & " WHERE T_ID = " & T_ID)
        End If

    End Sub

    Public Sub LoadPrinters(ByRef Barcode_Cm As ComboBox)
        Dim pkInstalledPrinters As String
        ' Find all printers installed
        For Each pkInstalledPrinters In
            PrinterSettings.InstalledPrinters
            Barcode_Cm.Items.Add(pkInstalledPrinters)
        Next pkInstalledPrinters
        ' Set the combo to the first printer in the list

        'Barcode_Cm.SelectedIndex = -1

    End Sub


    Public Function GET_AG_NO_SPACES(ag_name)
        Dim c As New C

        Try
            Dim s As String
            s = "select Ag_ID from Agents WHERE Ag_name = '" & ag_name.Trim() & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then Return True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        Return False
    End Function


    Public Function CHECK_IF_IM_RSV(IM_ID)
        Dim c As New C
        Dim s As String


        s = "select ISNULL(dbo.IM_Menu.MaxQtyAlert, 0) AS MaxQtyAlert from IM_MENU WHERE IM_ID = '" & IM_ID & "'"


        c.Com = New SqlClient.SqlCommand(s, c.Con)
        c.Con.Open()
        Try
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows = True Then
                c.Dr.Read()
                If c.Dr("MaxQtyAlert") = 0 Then
                    Return 0
                Else
                    Return 1
                End If

            Else
                Return 0
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return 0
    End Function


    Public Sub Set_Default_Printer(Default_Printer)
        Dim printerName As String = Default_Printer.Trim()
        Dim args As String = $"printui.dll,PrintUIEntry /y /n ""{printerName}"""

        Dim psi As New ProcessStartInfo("rundll32.exe", args) With {
    .UseShellExecute = False,
    .CreateNoWindow = True
}

        Process.Start(psi).WaitForExit()

    End Sub


    Public Sub Load_ALL_IM()
        Dim c As New C
        Dim s As String
        IM_Dt = New DataTable
        Try
            IM_Dt.Clear()

            If SB_Sch_With_QTY = False Then
                s = "select  IM_ID as ItemID,IM_NUM AS IM_NUMBER ,item_name as ItemName,Barcode as Barcode ,isValid from IM_Menu_V  Order by item_name ASC"
            Else
                s = "select  IM_ID as ItemID,IM_NUM AS IM_NUMBER ,item_name as ItemName,Barcode as Barcode ,isValid from IM_All_V_With_QTY  Order by item_name ASC"
            End If
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(IM_Dt)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------

        IM_Dt_Barcodes = New DataTable
        Try
            IM_Dt_Barcodes.Clear()
            s = "select  IM_ID as ItemID,IM_NUM AS IM_NUMBER ,item_name as ItemName,Barcode as Barcode,isValid from IM_All_Barcodes_V  Order by item_name ASC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(IM_Dt_Barcodes)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        '-------------------------------------------------------------------------------------------------------------------------------------------------------------
        c = New C
        IM_Units_Dt = New DataTable
        Try
            IM_Units_Dt.Clear()
            s = "select U_IM_ID,IM_ID,item_name,U_Name,U_ID,U_Cargo,Price,Min_SP,Min_SP_2,Barcode,isValid from IM_Menu_Units_V Order By IM_ID,U_ID Asc"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(IM_Units_Dt)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    '----------------------------------------------------------------------------< Change Resolution >
    'Private Resolution As New ResolutionChanger
    'Private OldWidth As UInteger
    'Private OldHeight As UInteger

    'Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    '    Resolution.SetResolution(OldWidth, OldHeight)
    'End Sub

    'Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    '    OldHeight = CUInt(Screen.PrimaryScreen.Bounds.Height)
    '    OldWidth = CUInt(Screen.PrimaryScreen.Bounds.Width)
    '    Select Case Resolution.SetResolution(1024, 768)
    '        Case ResolutionChanger.ChangeResult.Success
    '            MsgBox("The Resolution was changed", MsgBoxStyle.OkOnly)
    '        Case ResolutionChanger.ChangeResult.Restart
    '            MsgBox("Restart your system to activate the new resolution setting", MsgBoxStyle.OkOnly)
    '        Case ResolutionChanger.ChangeResult.Fail
    '            MsgBox("The resolution couldn't be changed", MsgBoxStyle.OkOnly)
    '        Case ResolutionChanger.ChangeResult.ResolutionNotSupported
    '            MsgBox("The requested resolution is not supported by your system", MsgBoxStyle.OkOnly)
    '    End Select
    'End Sub
    '----------------------------------------------------------------------------< Change Resolution />
End Module
