Imports System.Data.SqlClient
Imports Microsoft.SqlServer.Management.Smo
Imports System.Xml.XPath
Imports System.Data.Sql

Public Class ServerOptions
    Dim DataBase As String
    Dim Connection As String = ""
    Dim con As New SqlConnection(Connection)
    Dim cmd As New SqlCommand

    Private Sub ServerOptions_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'Load Items To Server Name
            CmbSeverName.Items.Add(My.Computer.Name.ToString)
            CmbSeverName.Items.Add(My.Computer.Name.ToString & "\SQLExpress")
            'CmbSeverName.Items.Add("LocalHost")
            'CmbSeverName.Items.Add("LocalHost\SQLExpress")
            'CmbSeverName.Items.Add(MY_Settings.S_SERVER.ToString)
            CmbSeverName.Text = MY_Settings.S_SERVER.ToString

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        TrailCheckBox.Checked = MY_Settings.IsAttachDB

        If String.IsNullOrWhiteSpace(MY_Settings.DataBase) = True Then
            ServMenu_Btn.Enabled = False
            SaveEdit_Btn.Enabled = False
        End If

        Server_Choese_Server_txt.Text = MY_Settings.Server_Choese_Server
        DB_Choese_Server_txt.Text = MY_Settings.DB_Choese_Server
        isCenterSys_CB.Checked = MY_Settings.isCenterSys

        isSubSys_CB.Checked = MY_Settings.is_SubSys
        SERVER_IP_txt.Text = MY_Settings.SERVER_IP

        Serv_Desc_txt.Text = MY_Settings.Server_Desc

        Show_Current_Server()

    End Sub

    Public Sub Show_Current_Server()
        Serv_Desc_Lb.Text = MY_Settings.Server_Desc
        ServerNameLabel.Text = MY_Settings.S_SERVER

        If MY_Settings.DB_Authentication = 0 Then
            CmbAuth.SelectedIndex = 0
        Else
            CmbAuth.SelectedIndex = 1
        End If

        AuthenticationLabel.Text = CmbAuth.SelectedItem
        DataBaseNameLabel.Text = MY_Settings.DataBase
        UserNameLabel.Text = MY_Settings.DB_UName
        PassWordLabel.Text = MY_Settings.DB_Pass
        CmbDataBase.Text = MY_Settings.DataBase

        Online_Con_txt.Text = MY_Settings.Online_Con_Str
    End Sub

    Public Sub CmbDataBase_DropDown(sender As Object, e As EventArgs) Handles CmbDataBase.DropDown
        Try
            Me.Cursor = Cursors.WaitCursor
            CmbDataBase.Items.Clear()
            Dim ServerName As String = CmbSeverName.Text.ToString
            Dim ServerConnection As Microsoft.SqlServer.Management.Common.ServerConnection = New Microsoft.SqlServer.Management.Common.ServerConnection
            ServerConnection.ServerInstance = ServerName
            ServerConnection.LoginSecure = True

            If CmbAuth.SelectedIndex = 1 Then ' for login with username and password
                ServerConnection.LoginSecure = False
                ServerConnection.Login = TxtUserName.Text
                ServerConnection.Password = TxtPassWord.Text
            End If

            Dim Server As Server = New Server(ServerConnection)
            Try
                For Each DataBase As Database In Server.Databases
                    CmbDataBase.Items.Add(DataBase.Name)
                Next
            Catch ex As Exception
                Me.Cursor = Cursors.Default
            End Try

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub CmbAuth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbAuth.SelectedIndexChanged
        AuthErrorProvider.Clear()
        check_cmb()
    End Sub

    Private Sub check_cmb()
        If CmbAuth.SelectedIndex = 0 Then
            UserPassGroupBox.Enabled = False
            Connection = "Data Source =" & CmbSeverName.Text & "; initial Catalog =" & CmbDataBase.Text & ";integrated Security= true"
        Else
            UserPassGroupBox.Enabled = True
            Connection = "Data Source =" & CmbSeverName.Text & "; initial Catalog =" & CmbDataBase.Text & ";" &
            "user id=" & TxtUserName.Text & ";password=" & TxtPassWord.Text & ";integrated Security= false"
        End If
    End Sub

    Private Sub CHECK_CONNECTION_ON_REAL_TIME()
        Me.Cursor = Cursors.AppStarting
        check_cmb()
        is_CHECKED_LB.Visible = True
        If CheckConnection(Connection) Then
            is_CHECKED_LB.BackColor = Color.DarkSeaGreen
            is_CHECKED_LB.Text = "متصـــل"
        Else
            is_CHECKED_LB.BackColor = Color.LightGray
            is_CHECKED_LB.Text = "غير متصــل"
        End If
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub CmbSeverName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSeverName.SelectedIndexChanged
        ServerNameErrorProvider.Clear()
    End Sub

    Private Sub CmbSeverName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbSeverName.Validating
        If String.IsNullOrWhiteSpace(CmbSeverName.Text) Then
            ServerNameErrorProvider.SetError(CmbSeverName, "Enter Server Name")
            e.Cancel = True
        Else
            ServerNameErrorProvider.Clear()
        End If
    End Sub

    Private Sub CmbAuth_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbAuth.Validating
        If String.IsNullOrWhiteSpace(CmbAuth.Text) Then
            AuthErrorProvider.SetError(CmbAuth, "Enter Authentication Type")
            e.Cancel = True
        Else
            AuthErrorProvider.Clear()
        End If
    End Sub

    Private Sub CmbDataBase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbDataBase.SelectedIndexChanged
        DBNameErrorProvider.Clear()
    End Sub

    Private Sub CmbDataBase_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbDataBase.Validating
        If String.IsNullOrWhiteSpace(CmbDataBase.Text) Then
            DBNameErrorProvider.SetError(CmbDataBase, "Enter DataBase Name")
            e.Cancel = True
        Else
            DBNameErrorProvider.Clear()
        End If
    End Sub

    Private Sub TxtUserName_TextChanged(sender As Object, e As EventArgs) Handles TxtUserName.TextChanged
        UnameErrorProvider.Clear()
    End Sub

    Private Sub TxtUserName_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TxtUserName.Validating
        If TxtUserName.Enabled = True Then
            If String.IsNullOrWhiteSpace(TxtUserName.Text) Then
                UnameErrorProvider.SetError(TxtUserName, "Enter UserName")
                e.Cancel = True
            Else
                UnameErrorProvider.Clear()
            End If
        End If
    End Sub

    Private Sub TxtPassWord_TextChanged(sender As Object, e As EventArgs) Handles TxtPassWord.TextChanged
        UpassErrorProvider.Clear()
    End Sub

    Private Sub TxtPassWord_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TxtPassWord.Validating
        If TxtPassWord.Enabled = True Then
            If String.IsNullOrWhiteSpace(TxtPassWord.Text) Then
                UpassErrorProvider.SetError(TxtPassWord, "Enter Password")
                e.Cancel = True
            Else
                UpassErrorProvider.Clear()
            End If
        End If
    End Sub


    Private Sub TrilCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles TrailCheckBox.CheckedChanged
        CB_CHecked(sender)
        If TrailCheckBox.Checked = True Then
            MY_Settings.IsAttachDB = True
        Else
            MY_Settings.IsAttachDB = False
        End If
        Save_AppSetting()

    End Sub


    Private Sub SaveEdit_Btn_Click(sender As Object, e As EventArgs) Handles SaveEdit_Btn.Click
        If ValidateChildren() = True Then Server_INSERT()
    End Sub

    Private Sub Server_INSERT()
        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "SERVERS_INSERT"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Serv_Desc", Serv_Desc_txt.Text)
            .Parameters.AddWithValue("@Serv_Name", CmbSeverName.Text)
            .Parameters.AddWithValue("@DB_Authentication", CmbAuth.SelectedIndex)
            .Parameters.AddWithValue("@DB_Name", CmbDataBase.Text)

            If CmbAuth.SelectedIndex = 1 Then
                .Parameters.AddWithValue("@DB_UserName", TxtUserName.Text)
                .Parameters.AddWithValue("@DB_Pass", TxtPassWord.Text)
            End If

        End With

        If SQL_SP_EXEC(C.Com) Then
            MsgBox("تمت إضافة الخادم", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub ServMenu_Btn_Click(sender As Object, e As EventArgs) Handles ServMenu_Btn.Click
        ChoaseServer.ShowDialog()
    End Sub

    Private Sub UseServer_Btn_Click(sender As Object, e As EventArgs) Handles UseServer_Btn.Click
        Try
            If ValidateChildren() = True Then
                MY_Settings.Server_Desc = Serv_Desc_txt.Text
                MY_Settings.S_SERVER = CmbSeverName.Text
                MY_Settings.DataBase = CmbDataBase.Text
                'F_MainForm.DB_Name_Tool.Text = CmbDataBase.Text
                MY_Settings.DB_Authentication = CmbAuth.SelectedIndex
                MY_Settings.DB_UName = TxtUserName.Text
                MY_Settings.DB_Pass = TxtPassWord.Text
                MY_Settings.SERVER_IP = SERVER_IP_txt.Text
                MY_Settings.Online_Con_Str = Online_Con_txt.Text

                Save_AppSetting()
                Me.Cursor = Cursors.AppStarting
                Build_Connection()
                prepare_Sys()
                Show_Current_Server()
                Me.Cursor = Cursors.Default


                Dim tmpl As String = ""
                tmpl = CmbSeverName.Text + vbNewLine + CmbAuth.SelectedIndex.ToString + vbNewLine + TxtUserName.Text + vbNewLine + TxtPassWord.Text + vbNewLine + CmbDataBase.Text
                System.IO.File.WriteAllText("Connection Info.txt", "")
                Dim Writer As System.IO.StreamWriter
                Writer = New System.IO.StreamWriter("Connection Info.txt", OpenMode.Input)
                Writer.Write(tmpl)
                Writer.Close()

                MsgBox("تم حفظ بيانات الإتصال", MsgBoxStyle.Information, "")
                'Application.Restart()
                'Build_Connection()


            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Server_Choese_Server_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Server_Choese_Server_txt.KeyDown
        If e.KeyCode = Keys.Return Then
            MY_Settings.Server_Choese_Server = Server_Choese_Server_txt.Text
            Save_AppSetting()
        End If
    End Sub

    Private Sub DB_Choese_Server_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles DB_Choese_Server_txt.KeyDown
        If e.KeyCode = Keys.Return Then
            MY_Settings.DB_Choese_Server = DB_Choese_Server_txt.Text
            Save_AppSetting()
        End If
    End Sub

    Private Sub isCenterSys_CB_CheckedChanged(sender As Object, e As EventArgs) Handles isCenterSys_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.isCenterSys = isCenterSys_CB.Checked
        Save_AppSetting()
    End Sub


    Private Sub isSubSys_CB_CheckedChanged(sender As Object, e As EventArgs) Handles isSubSys_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.is_SubSys = isSubSys_CB.Checked
        Save_AppSetting()
    End Sub

    Private Sub SERVER_IP_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles SERVER_IP_txt.KeyDown
        If e.KeyCode = Keys.Return Then
            MY_Settings.SERVER_IP = SERVER_IP_txt.Text
            Save_AppSetting()
        End If
    End Sub


    Private Sub Switch_PC_Activation_btn_Click(sender As Object, e As EventArgs) Handles Switch_PC_Activation_btn.Click
        SwitchActivation.ShowDialog()
    End Sub


    Public Sub Attach_Database()
        'Try
        MY_Settings.SqlConStr = "Data Source= " + My.Computer.Name + " ;initial catalog = MASTER ;Integrated Security=True;"
        Save_AppSetting()

        Me.Cursor = Cursors.AppStarting

        Dim c As New C

        Dim strCreate As String

        strCreate = "CREATE DATABASE " & NewDBTextBox.Text & " ON PRIMARY " & _
           "(NAME =  " & NewDBTextBox.Text & " , " & _
            " FILENAME = '" & Application.StartupPath & "\DB\" & NewDBTextBox.Text & ".mdf', " & _
           " SIZE = 10MB, " & _
           " FILEGROWTH = 10%) " & _
           " LOG ON " & _
           "(NAME = MyDatabase_Log, " & _
           " FILENAME = '" & Application.StartupPath & "\DB\" & NewDBTextBox.Text & "_log.ldf', " & _
           " SIZE = 3MB, " & _
           " FILEGROWTH = 10%)"

        '-------------------------------------------------------------------------------------
        Dim commCreate As SqlCommand = New SqlCommand(strCreate, c.Con)

        Try
            c.Con.Open()
            '  connCreate.Open()
            commCreate.ExecuteNonQuery()
            MessageBox.Show("تم إنشاء القاعدة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        c.Con.Close()

        '--------------------------------------------------------------------------------------------

        Dim BackupPath = Application.StartupPath + "\DB\" & NewDBTextBox.Text + ".bak"

        Dim strAlter As String

        strAlter = "RESTORE DATABASE " & NewDBTextBox.Text & " FROM  DISK = '" & BackupPath & "' WITH  FILE = 1, " &
            " MOVE '" & NewDBTextBox.Text & "' TO '" & Application.StartupPath & "\DB\" & NewDBTextBox.Text & ".mdf', " &
            "  MOVE '" & NewDBTextBox.Text & "_log' TO '" & Application.StartupPath & "\DB\" & NewDBTextBox.Text & "_log.ldf', NOUNLOAD,  REPLACE,  STATS = 5"


        Dim commAlter As SqlCommand = New SqlCommand(strAlter, c.Con)

        Try
            c.Con.Open()
            commAlter.ExecuteNonQuery()
            MessageBox.Show("تم تكوين ملفات القاعدة بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
        c.Con.Close()


        MessageBox.Show("سيتم إعادة تشغيل النظام", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Application.Restart()


        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Attach_Database_btn_Click(sender As Object, e As EventArgs) Handles Attach_Database_btn.Click
        If MessageBox.Show(" إنشاء وتكوين قاعدة بيانات جديدة بإسم " + NewDBTextBox.Text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                               MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
            Attach_Database()
        End If

    End Sub

    ' <summary>
    ' Reads the data of specified node provided in the parameter
    ' </summary>
    ' <param name="pstrValueToRead">Node to be read</param>
    '' <returns>string containing the value</returns>
    'Private Shared Function ReadValueFromXML(ByVal pstrValueToRead As String) As String
    '    Try
    '        Dim doc As XPathDocument = New XPathDocument(Application.StartupPath + "\CPOS.exe")
    '        Dim nav As XPathNavigator = doc.CreateNavigator()
    '        Dim expr As XPathExpression
    '        expr = nav.Compile("/settings/" & pstrValueToRead)
    '        Dim iterator As XPathNodeIterator = nav.[Select](expr)

    '        While iterator.MoveNext()
    '            Return iterator.Current.Value
    '        End While

    '        Return String.Empty
    '    Catch
    '        Return String.Empty
    '    End Try
    'End Function

    Private Sub Check_Btn_Click(sender As Object, e As EventArgs) Handles Check_Btn.Click
        CHECK_CONNECTION_ON_REAL_TIME()
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        Me.Cursor = Cursors.WaitCursor
        CmbSeverName.Items.Clear()
        Dim dt As DataTable = SqlDataSourceEnumerator.Instance.GetDataSources
        For Each dr As DataRow In dt.Rows
            If Not IsDBNull(dr("InstanceName")) Then
                CmbSeverName.Items.Add(String.Concat(dr("ServerName"), "\", dr("InstanceName")))
            Else
                CmbSeverName.Items.Add(String.Concat(dr("ServerName"), "", ""))
            End If
        Next
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Refresh_btn_Click(sender As Object, e As EventArgs) Handles Refresh_btn.Click
        Online_Con_txt.Text = "Data Source= 65.21.80.196;initial catalog='';User Id='';Password=''"
    End Sub
End Class
