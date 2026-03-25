'Imports System.Data.SqlClient
'Imports System.Data.Sql


''Imports System
''Imports System.ServiceProcess

'Public Class login
'    Dim aa As Integer


'    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
'        aa = 1
'        passTxt.Text = passTxt.Text + aa.ToString()
'    End Sub

'    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
'        aa = 2
'        passTxt.Text = passTxt.Text + aa.ToString()
'    End Sub

'    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
'        aa = 3
'        passTxt.Text = passTxt.Text + aa.ToString()
'    End Sub

'    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
'        aa = 4
'        passTxt.Text = passTxt.Text + aa.ToString()
'    End Sub

'    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
'        aa = 5
'        passTxt.Text = passTxt.Text + aa.ToString()
'    End Sub

'    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
'        aa = 6
'        passTxt.Text = passTxt.Text + aa.ToString()
'    End Sub

'    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
'        aa = 7
'        passTxt.Text = passTxt.Text + aa.ToString()
'    End Sub

'    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
'        aa = 8
'        passTxt.Text = passTxt.Text + aa.ToString()
'    End Sub

'    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
'        aa = 9
'        passTxt.Text = passTxt.Text + aa.ToString()
'    End Sub

'    Private Sub Button0_Click(sender As Object, e As EventArgs) Handles Button0.Click
'        aa = 0
'        passTxt.Text = passTxt.Text + aa.ToString()
'    End Sub

'    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
'        Me.Close()        
'    End Sub

'    Private Sub EnterButton_Click(sender As Object, e As EventArgs) Handles EnterButton.Click


'        If passTxt.Text = "SERAJ RESAL" Then
'            passTxt.Clear()
'            SetSystem.ShowDialog()
'            Exit Sub
'        End If


'        Try
'            If passTxt.Text = ((Convert.ToInt16(Date.Now.Day)) + (Convert.ToInt16(Date.Now.Month)) + (Convert.ToInt16(Date.Now.Year))) * ((Convert.ToInt16(Date.Now.Day)) * 2) Then
'                users.ShowDialog()
'                Exit Sub
'            End If
'        Catch ex As Exception
'        End Try



'        If passTxt.Text = "" Then
'            MsgBox("أدخل كلمة المرور", MsgBoxStyle.Exclamation, "حقل إجباري")
'            passTxt.Focus()

'        Else

'            'If passMaskedTextBox1.Text = Parse_Space(1) + Date.Now.Day().ToString + Parse_Space(3) + Date.Now.Month().ToString Then
'            '    UpdateUserPass.is_Change_PASS_Missed = True
'            '    UpdateUserPass.ShowDialog()
'            'Else
'            Me.Cursor = Cursors.AppStarting
'            CheckPass()
'            Me.Cursor = Cursors.Default
'        End If
'        '   End If
'    End Sub


'    Private Sub CheckPass()
'        Try

'            Dim C As New C
'            C.Str = "Select *  From Users Where UserPass= '" & EncryptionHelper.Encrypt(passTxt.Text) & "'" ' and user_id between " & START_ID & " and " & END_ID
'            C.Com = New SqlCommand(C.Str, C.Con)
'            C.Con.Open()

'            C.Dr = C.Com.ExecuteReader
'            C.Dr.Read()
'            If C.Dr.HasRows = True Then
'                passTxt.Clear()
'                USER_ID = C.Dr("user_id")
'                'Home.User_Lb.Text = C.Dr("UserName")
'                USER_NAME = C.Dr("UserName")

'                If C.Dr("is_Allow") = 0 Then
'                    MsgBox("المعذرة .. أنت غير مسموح لك بالدخول للنظام", MsgBoxStyle.Exclamation)
'                    Exit Sub
'                End If

'                'If Not Application.OpenForms().OfType(Of MainForm).Any Then F_MainForm = New MainForm
'                F_MainForm = New MainForm

'                If C.Dr("isAdmin") = True Then
'                    User_isAdmin = True
'                    F_MainForm.Sys_Setting_btn.Enabled = True
'                    U_SalesVoid = True
'                    U_SalesDis = True
'                    U_StExplore = True
'                    U_Balance = True
'                    U_Cancel_Pch = True
'                    U_Update_IM_QTY = True
'                    U_Add_Draw_St = True
'                    U_ExpVoid = True
'                    U_SB_Update = True
'                    U_Pch_Update = True
'                    U_SB_Rtn = True
'                    U_Pch_Rtn = True
'                    U_End_Table = True
'                    U_SB_IM_Update = True
'                    U_SB_Sell_Under_Cost = True
'                    U_SB_Show_Price_Info = True
'                    U_SB_Show_Cash = True
'                    U_Sell_Under_Min_SP = True
'                    U_SB_Show_IM_COST = True
'                    U_Update_Receipt = True
'                    U_Cancel_Receipt = True
'                    U_ExpEdit = True
'                    U_Sell_Under_Min_SP_2 = True
'                    U_AG_Skip_Max = True
'                    U_Show_Bill_Profet = True
'                    U_Transfer_Table = True
'                    U_Save_otherBill = True
'                    U_Tr_Widraw = True
'                    U_Tr_Deposit = True
'                    U_Tr_Convert = True
'                    U_Hide_Unrelated_Tr = False

'                Else
'                    F_MainForm.Sys_Setting_btn.Enabled = False
'                    User_isAdmin = False
'                End If

'                'If C.Dr("isPone_Users") = True Then
'                '    isPhone_User = True
'                '    Default_TB_ID = C.Dr("TB_ID")
'                'Else
'                '    isPhone_User = False
'                '    Default_TB_ID = 0
'                'End If

'                If Not IsDBNull(C.Dr("AG_ID")) Then U_AG_ID = C.Dr("AG_ID")

'                If C.Dr("Tr_ID") > 0 Then
'                    PCH_TR_ID = C.Dr("Tr_ID")
'                    SB_TR_ID = C.Dr("Tr_ID")
'                    Rct_Tr_ID = C.Dr("Tr_ID")
'                    EXP_TR_ID = C.Dr("Tr_ID")
'                Else
'                    PCH_TR_ID = Org_PCH_TR_ID
'                    SB_TR_ID = Org_SB_TR_ID
'                    Rct_Tr_ID = Org_Rct_Tr_ID
'                    EXP_TR_ID = Org_EXP_TR_ID
'                End If

'                'If C.Dr("isCostmerScreen") = True Then
'                '    'Me.Hide()
'                '    Costmer_Screen.ShowDialog()
'                'Else


'                'If IsIdInRange(USER_ID) = False Then
'                '    MsgBox("هذا المستخدم لا يتبع الفرع", MsgBoxStyle.Critical, "")
'                '    Exit Sub
'                'End If


'                Me.Hide()

'                F_MainForm = New MainForm
'                'F_MainForm.StartListening()
'                F_MainForm.ShowDialog()

'                'End If
'                C.Con.Close()

'            Else
'                MsgBox("خطأ في البيانات المدخلة", MsgBoxStyle.Critical, "خطأ")
'                passTxt.Focus()
'            End If

'            C.Con.Close()

'        Catch ex As Exception
'            '  MsgBox("تأكد من الإتصال بالشبكة و صحة إسم الخادم", MsgBoxStyle.Exclamation)
'            MsgBox(ex.Message)
'        End Try

'    End Sub


'    Private Sub ShowPassButton_MouseHover(sender As Object, e As EventArgs) Handles ShowPassButton.MouseHover
'        passTxt.UseSystemPasswordChar = False
'    End Sub

'    Private Sub ShowPassButton_MouseLeave(sender As Object, e As EventArgs) Handles ShowPassButton.MouseLeave
'        passTxt.UseSystemPasswordChar = True
'    End Sub

'    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
'        passTxt.Clear()
'        passTxt.Select()
'    End Sub

'    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles BackButton.Click
'        Dim shownum As String
'        Dim mOpflag As Boolean
'        If mOpflag = False And passTxt.Text <> "" Then
'            shownum = passTxt.Text
'            shownum = shownum.Remove(shownum.Length - 1, 1)
'            passTxt.Text = shownum
'        End If
'    End Sub

'    Private Sub login_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
'        'Application.ExitThread()
'        'Application.Exit()
'        Kill_All_Processes()
'    End Sub

'    Private Sub Check_Cp_Name()
'        Try
'            Dim C As New C
'            C.Str = "SELECT COUNT(*) as N ,CP_NAME FROM Activation_Details GROUP BY CP_NAME HAVING COUNT(*)>1"
'            C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
'            C.Con.Open()

'            C.Dr = C.Com.ExecuteReader
'            If C.Dr.HasRows = True Then
'                C.Dr.Read()
'                MessageBox.Show("يوجد تكرار في اسماء الأجهزة المسجلة على قاعدة البيانات ... قم بتعديل اسم أحد الأجهزة", "خروج في النظام", MessageBoxButtons.OK, _
'                                MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)

'                MessageBox.Show(" الإسم المتكرر : " + C.Dr("CP_NAME") + "  ,  " + " التكرار : " + C.Dr("N").ToString, "خروج في النظام", MessageBoxButtons.OK, _
'                             MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
'                'EnterButton.Enabled = False
'            End If
'            C.Con.Close()
'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try
'    End Sub

'    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load


'        If MY_Settings.App_Suuply = "RESAL" Then
'            Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
'            IM_PHONE_1_LB.Text = "091 - 430 00 70"
'            IM_PHONE_2_LB.Text = "092 - 430 00 70"
'            IM_PHONE_3_LB.Visible = False
'            SYS_DEVELOPER_LB.Visible = False
'        End If


'        Check_Cp_Name()

'        If MY_Settings.isCenterSys = True Then ChoaseServer.ShowDialog()
'        passTxt.Select()
'        Me.Text = MY_Settings.Server_Desc + " - تسجيل الدخــول  "


'        If MY_Settings.is_SubSys = True Then
'            Try
'                If My.Computer.Network.Ping(MY_Settings.SERVER_IP) Then
'                    ServeConnect_LB.Visible = True
'                    ServeConnect_LB.BackColor = Color.DarkGreen
'                    ServeConnect_LB.Text = " متصل بالخادم الرئيسي "
'                Else
'                    ServeConnect_LB.Visible = True
'                    ServeConnect_LB.BackColor = Color.DarkRed
'                    ServeConnect_LB.Text = " لا يوجد إتصال بالخادم الرئيسي "
'                End If
'            Catch ex As Exception
'                ServeConnect_LB.Visible = True
'                ServeConnect_LB.BackColor = Color.Gray
'                ServeConnect_LB.Text = " غير متصل بالشبكة "
'                MessageBox.Show("يوجد مشكلة بالتوصيل بالخادم الرئيسي ... تأكد من توصيل كابل الشبكة وصحة كتابة IP الخادم الرئيسي", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
'            End Try
'        Else
'            ServeConnect_LB.Visible = False
'            Button23.Visible = False
'        End If

'    End Sub

'    Public Sub Client_Check_Connection()
'        Try
'            If My.Computer.Network.Ping(MY_Settings.SERVER_IP) Then
'                ServeConnect_LB.Visible = True
'                ServeConnect_LB.BackColor = Color.DarkGreen
'                ServeConnect_LB.Text = " متصل بالخادم الرئيسي "
'            Else
'                ServeConnect_LB.Visible = True
'                ServeConnect_LB.BackColor = Color.DarkRed
'                ServeConnect_LB.Text = " لا يوجد إتصال بالخادم الرئيسي "
'            End If
'        Catch ex As Exception
'            ServeConnect_LB.Visible = True
'            ServeConnect_LB.BackColor = Color.Gray
'            ServeConnect_LB.Text = " غير متصل بالشبكة "
'            MessageBox.Show("يوجد مشكلة بالتوصيل بالخادم الرئيسي ... تأكد من توصيل كابل الشبكة وصحة كتابة IP الخادم الرئيسي", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
'        End Try
'    End Sub

'    Private Sub passMaskedTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles passTxt.KeyDown
'        If e.KeyCode = Keys.Return Then If EnterButton.Enabled = True Then EnterButton_Click(sender, e)
'    End Sub


'    Private Sub RestoreButton_Click(sender As Object, e As EventArgs) Handles RestoreButton.Click
'        Me.Cursor = Cursors.WaitCursor
'        Backup.Restore_DataBase()
'        Me.Cursor = Cursors.Default
'    End Sub

'    Private Sub ServerButton_Click(sender As Object, e As EventArgs) Handles ServerButton.Click
'        If passTxt.Text = "0000" Then
'            passTxt.Clear()
'            ServerOptions.ShowDialog()
'        End If

'    End Sub

'    Private Sub passMaskedTextBox1_TextChanged(sender As Object, e As EventArgs) Handles passTxt.TextChanged
'        If passTxt.Text.Count > 0 Then
'            ShowPassButton.Visible = True
'        Else
'            ShowPassButton.Visible = False
'        End If
'    End Sub


'    Private Sub ServersMenuBtn_Click(sender As Object, e As EventArgs) Handles ServersMenuBtn.Click
'        ChoaseServer.ShowDialog()
'    End Sub


'    Public Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
'        Me.Cursor = Cursors.AppStarting
'        Try
'            If My.Computer.Network.Ping(MY_Settings.SERVER_IP) Then
'                ServeConnect_LB.Visible = True
'                ServeConnect_LB.BackColor = Color.DarkGreen
'                ServeConnect_LB.Text = " متصل بالخادم الرئيسي "
'            Else
'                ServeConnect_LB.Visible = True
'                ServeConnect_LB.BackColor = Color.DarkRed
'                ServeConnect_LB.Text = " لا يوجد إتصال بالخادم الرئيسي "
'            End If
'        Catch ex As Exception
'            ServeConnect_LB.Visible = True
'            ServeConnect_LB.BackColor = Color.Gray
'            ServeConnect_LB.Text = " غير متصل بالشبكة "
'            MessageBox.Show("يوجد مشكلة بالتوصيل بالخادم الرئيسي ... تأكد من توصيل كابل الشبكة وصحة كتابة IP الخادم الرئيسي", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
'        End Try
'        Me.Cursor = Cursors.Default
'    End Sub

'    '   Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
'    'Dim myServiceName As String = "MSSQL$SQLEXPRESS" 'service name of SQL Server Express
'    'Dim status As String  'service status (For example, Running or Stopped)
'    'Dim mySC As ServiceController

'    'Console.WriteLine("Service: " & myServiceName)

'    ''display service status: For example, Running, Stopped, or Paused
'    'mySC = New ServiceController(myServiceName)
'    'Try
'    '    status = mySC.Status.ToString
'    'Catch ex As Exception
'    '    Console.WriteLine("Service not found. It is probably not installed. [exception=" & ex.Message & "]")
'    '    Console.ReadLine()
'    '    End
'    'End Try
'    'Console.WriteLine("Service status : " & status)

'    ''if service is Stopped or StopPending, you can run it with the following code.
'    'If mySC.Status.Equals(ServiceControllerStatus.Stopped) Or mySC.Status.Equals(ServiceControllerStatus.StopPending) Then
'    '    Try
'    '        Console.WriteLine("Starting the service...")
'    '        mySC.Start()
'    '        mySC.WaitForStatus(ServiceControllerStatus.Running)
'    '        Console.WriteLine("The service is now " & mySC.Status.ToString)

'    '    Catch ex As Exception
'    '        Console.WriteLine("Error in starting the service: " & ex.Message)
'    '    End Try
'    'End If

'    'Console.WriteLine("Press a key to end the application...")
'    'Console.ReadLine()
'    'End
'    'End Sub


'    'Private Sub StartStop()

'    '    Dim service As ServiceController = New ServiceController("MyServiceName")

'    '    If ((service.Status.Equals(ServiceControllerStatus.Stopped)) Or

'    '        (service.Status.Equals(ServiceControllerStatus.StopPending))) Then

'    '        service.Start()

'    '    Else

'    '        service.Stop()

'    '    End If

'    'End Sub

'    Private Sub Sys_Maintains_btn_Click(sender As Object, e As EventArgs) Handles Sys_Maintains_btn.Click
'        'Dim notification3 As New NotificationForm("تنبيه!", "تم إرسال الرسالة", "bottom")
'        'notification3.ShowNotification()

'        'RabbitMQConsumer.StartConsumer()


'        'RabbitMQProducer.SendNotification("طلب جديد تم إنشاؤه!")

'        If passTxt.Text = "0000" Then
'            passTxt.Clear()
'            MaintenanceForm.ShowDialog()
'        End If
'    End Sub


'    Private Sub AnyDesk_Btn_Click(sender As Object, e As EventArgs) Handles AnyDesk_Btn.Click
'        Try
'            Shell("AnyDesk.exe")
'        Catch ex As Exception
'            Dim proc As New System.Diagnostics.Process()
'            proc = Process.Start(Application.StartupPath & "\Support\AnyDesk.exe", "")
'        End Try
'    End Sub
'    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
'        Call_KeyBoard()
'    End Sub

'End Class
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.Threading.Tasks

Public Class login
    Dim aa As Integer

    ' --- 1. الحركات التفاعلية (Animations) ---

    ' حركة اهتزاز الشاشة عند الخطأ
    Private Async Sub ShakeForm()
        Dim originalLocation As Point = Me.Location
        Dim rnd As New Random()
        For i As Integer = 0 To 8
            Me.Location = New Point(originalLocation.X + rnd.Next(-10, 10), originalLocation.Y)
            Await Task.Delay(30)
        Next
        Me.Location = originalLocation
    End Sub

    ' رفض الدخول (تغيير لون مربع النص لثوانٍ مع الاهتزاز)
    Private Async Sub RejectLogin()
        passTxt.BackColor = Color.FromArgb(255, 204, 204)
        passTxt.ForeColor = Color.DarkRed
        ShakeForm()
        Await Task.Delay(600)
        passTxt.BackColor = Color.White
        passTxt.ForeColor = Color.Black
        passTxt.Clear()
        passTxt.Focus()
    End Sub

    ' --- 2. أزرار الأرقام (تم اختصارها في حدث واحد ذكي) ---
    Private Sub Numpad_Click(sender As Object, e As EventArgs) Handles Button0.Click, Button1.Click, Button2.Click, Button3.Click, Button4.Click, Button5.Click, Button6.Click, Button7.Click, Button8.Click, Button9.Click
        Dim btn As Button = CType(sender, Button)
        passTxt.Text += btn.Text
    End Sub

    ' --- 3. أزرار التحكم ---
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub



    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        passTxt.Clear()
        passTxt.Select()
    End Sub

    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles BackButton.Click
        Dim shownum As String
        Dim mOpflag As Boolean
        If mOpflag = False And passTxt.Text <> "" Then
            shownum = passTxt.Text
            shownum = shownum.Remove(shownum.Length - 1, 1)
            passTxt.Text = shownum
        End If
    End Sub

    Private Sub EnterButton_Click(sender As Object, e As EventArgs) Handles EnterButton.Click
        If passTxt.Text = "SERAJ RESAL" Then
            passTxt.Clear()
            SetSystem.ShowDialog()
            Exit Sub
        End If

        Try
            If passTxt.Text = ((Convert.ToInt16(Date.Now.Day)) + (Convert.ToInt16(Date.Now.Month)) + (Convert.ToInt16(Date.Now.Year))) * ((Convert.ToInt16(Date.Now.Day)) * 2) Then
                users.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
        End Try

        If passTxt.Text = "" Then
            MsgBox("أدخل كلمة المرور", MsgBoxStyle.Exclamation, "حقل إجباري")
            passTxt.Focus()
        Else
            Me.Cursor = Cursors.AppStarting
            CheckPass()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    ' --- 4. التحقق من قاعدة البيانات ---
    Private Sub CheckPass()
        Try
            Dim C As New C
            C.Str = "Select * From Users Where UserPass= '" & EncryptionHelper.Encrypt(passTxt.Text) & "'"
            C.Com = New SqlCommand(C.Str, C.Con)
            C.Con.Open()

            C.Dr = C.Com.ExecuteReader
            C.Dr.Read()

            If C.Dr.HasRows = True Then
                passTxt.Clear()
                USER_ID = C.Dr("user_id")
                USER_NAME = C.Dr("UserName")

                If C.Dr("is_Allow") = 0 Then
                    MsgBox("المعذرة .. أنت غير مسموح لك بالدخول للنظام", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If

                F_MainForm = New MainForm

                If C.Dr("isAdmin") = True Then
                    User_isAdmin = True
                    F_MainForm.Sys_Setting_btn.Enabled = True
                    U_SalesVoid = True
                    U_SalesDis = True
                    U_StExplore = True
                    U_Balance = True
                    U_Cancel_Pch = True
                    U_Update_IM_QTY = True
                    U_Add_Draw_St = True
                    U_ExpVoid = True
                    U_SB_Update = True
                    U_Pch_Update = True
                    U_SB_Rtn = True
                    U_Pch_Rtn = True
                    U_End_Table = True
                    U_SB_IM_Update = True
                    U_SB_Sell_Under_Cost = True
                    U_SB_Show_Price_Info = True
                    U_SB_Show_Cash = True
                    U_Sell_Under_Min_SP = True
                    U_SB_Show_IM_COST = True
                    U_Update_Receipt = True
                    U_Cancel_Receipt = True
                    U_ExpEdit = True
                    U_Sell_Under_Min_SP_2 = True
                    U_AG_Skip_Max = True
                    U_Show_Bill_Profet = True
                    U_Transfer_Table = True
                    U_Save_otherBill = True
                    U_Tr_Widraw = True
                    U_Tr_Deposit = True
                    U_Tr_Convert = True
                    U_Hide_Unrelated_Tr = False
                Else
                    F_MainForm.Sys_Setting_btn.Enabled = False
                    User_isAdmin = False
                End If

                If Not IsDBNull(C.Dr("AG_ID")) Then U_AG_ID = C.Dr("AG_ID")

                If C.Dr("Tr_ID") > 0 Then
                    PCH_TR_ID = C.Dr("Tr_ID")
                    SB_TR_ID = C.Dr("Tr_ID")
                    Rct_Tr_ID = C.Dr("Tr_ID")
                    EXP_TR_ID = C.Dr("Tr_ID")
                Else
                    PCH_TR_ID = Org_PCH_TR_ID
                    SB_TR_ID = Org_SB_TR_ID
                    Rct_Tr_ID = Org_Rct_Tr_ID
                    EXP_TR_ID = Org_EXP_TR_ID
                End If

                Me.Hide()
                F_MainForm = New MainForm
                F_MainForm.ShowDialog()
                C.Con.Close()

            Else
                ' تفعيل الحركات التفاعلية عند رفض الدخول بدلاً من MsgBox الكلاسيكي
                RejectLogin()
            End If
            C.Con.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    ' --- 5. إعدادات الفورم والشبكة والأزرار الأخرى ---
    Private Sub ShowPassButton_MouseHover(sender As Object, e As EventArgs) Handles ShowPassButton.MouseHover
        passTxt.UseSystemPasswordChar = False
    End Sub

    Private Sub ShowPassButton_MouseLeave(sender As Object, e As EventArgs) Handles ShowPassButton.MouseLeave
        passTxt.UseSystemPasswordChar = True
    End Sub

    Private Sub login_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Kill_All_Processes()
    End Sub

    Private Sub Check_Cp_Name()
        Try
            Dim C As New C
            C.Str = "SELECT COUNT(*) as N ,CP_NAME FROM Activation_Details GROUP BY CP_NAME HAVING COUNT(*)>1"
            C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
            C.Con.Open()

            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                C.Dr.Read()
                MessageBox.Show("يوجد تكرار في اسماء الأجهزة المسجلة على قاعدة البيانات ... قم بتعديل اسم أحد الأجهزة", "خروج في النظام", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
                MessageBox.Show(" الإسم المتكرر : " + C.Dr("CP_NAME") + "  ,  " + " التكرار : " + C.Dr("N").ToString, "خروج في النظام", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
            End If
            C.Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        EnterButton.Tag = "Save"          ' ليأخذ لون الحفظ (أخضر/أزرق)
        ExitButton.Tag = "Delete"         ' ليأخذ لون الحذف (أحمر)
        ClearButton.Tag = "Delete"        ' زر حرف C

        ' أزرار الأرقام (تأخذ اللون العام للثيم)
        Button1.Tag = "General" : Button2.Tag = "General" : Button3.Tag = "General"
        Button4.Tag = "General" : Button5.Tag = "General" : Button6.Tag = "General"
        Button7.Tag = "General" : Button8.Tag = "General" : Button9.Tag = "General"
        Button0.Tag = "General" : ShowPassButton.Tag = "General"

        ' الأزرار السفلية
        ServersMenuBtn.Tag = "General" : ServerButton.Tag = "General"
        RestoreButton.Tag = "General" : Sys_Maintains_btn.Tag = "General"

        ' النصوص (لكي لا تختفي في الوضع المظلم)
        Label1.Tag = "Primary"
        ServeConnect_LB.Tag = "Secondary"
        SYS_DEVELOPER_LB.Tag = "Secondary"


        ThemeManager.LoadDefaultSystemTheme()
        ThemeManager.ApplyThemeToForm(Me)

        If MY_Settings.App_Suuply = "RESAL" Then
            Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
            IM_PHONE_1_LB.Text = "091 - 430 00 70"
            IM_PHONE_2_LB.Text = "092 - 430 00 70"
            IM_PHONE_3_LB.Visible = False
            SYS_DEVELOPER_LB.Visible = False
        End If

        Check_Cp_Name()


        If MY_Settings.isCenterSys = True Then ChoaseServer.ShowDialog()
        passTxt.Select()
        Me.Text = MY_Settings.Server_Desc + " - تسجيل الدخــول  "

        If MY_Settings.is_SubSys = True Then
            Client_Check_Connection()
        Else
            ServeConnect_LB.Visible = False
            Button23.Visible = False
        End If
    End Sub


    Public Sub Client_Check_Connection()
        Try
            If My.Computer.Network.Ping(MY_Settings.SERVER_IP) Then
                ServeConnect_LB.Visible = True
                ServeConnect_LB.BackColor = Color.DarkGreen
                ServeConnect_LB.Text = " متصل بالخادم الرئيسي "
            Else
                ServeConnect_LB.Visible = True
                ServeConnect_LB.BackColor = Color.DarkRed
                ServeConnect_LB.Text = " لا يوجد إتصال بالخادم الرئيسي "
            End If
        Catch ex As Exception
            ServeConnect_LB.Visible = True
            ServeConnect_LB.BackColor = Color.Gray
            ServeConnect_LB.Text = " غير متصل بالشبكة "
            MessageBox.Show("يوجد مشكلة بالتوصيل بالخادم الرئيسي ... تأكد من توصيل كابل الشبكة وصحة كتابة IP الخادم الرئيسي", "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading)
        End Try
    End Sub

    Private Sub passMaskedTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles passTxt.KeyDown
        If e.KeyCode = Keys.Return Then If EnterButton.Enabled = True Then EnterButton_Click(sender, e)
    End Sub

    Private Sub RestoreButton_Click(sender As Object, e As EventArgs) Handles RestoreButton.Click
        Me.Cursor = Cursors.WaitCursor
        Backup.Restore_DataBase()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ServerButton_Click(sender As Object, e As EventArgs) Handles ServerButton.Click
        If passTxt.Text = "0000" Then
            passTxt.Clear()
            ServerOptions.ShowDialog()
        End If
    End Sub

    Private Sub passMaskedTextBox1_TextChanged(sender As Object, e As EventArgs) Handles passTxt.TextChanged
        If passTxt.Text.Count > 0 Then
            ShowPassButton.Visible = True
        Else
            ShowPassButton.Visible = False
        End If
    End Sub

    Private Sub ServersMenuBtn_Click(sender As Object, e As EventArgs) Handles ServersMenuBtn.Click
        ChoaseServer.ShowDialog()
    End Sub

    Public Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        Me.Cursor = Cursors.AppStarting
        Client_Check_Connection()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Sys_Maintains_btn_Click(sender As Object, e As EventArgs) Handles Sys_Maintains_btn.Click
        If passTxt.Text = "0000" Then
            passTxt.Clear()
            MaintenanceForm.ShowDialog()
        End If
    End Sub

    Private Sub AnyDesk_Btn_Click(sender As Object, e As EventArgs) Handles AnyDesk_Btn.Click
        Try
            Shell("AnyDesk.exe")
        Catch ex As Exception
            Dim proc As New System.Diagnostics.Process()
            proc = Process.Start(Application.StartupPath & "\Support\AnyDesk.exe", "")
        End Try
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Call_KeyBoard()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim frm As New Frm_ThemeSettings
        frm.ShowDialog()

    End Sub
End Class