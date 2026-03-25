Imports System.Data.SqlClient
Imports System.IO


Public Class MaintenanceForm

    Private Sub M_1_btn_Click(sender As Object, e As EventArgs) Handles M_1_btn.Click
        Me.Cursor = Cursors.AppStarting
        DB_Shrink()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub DB_Shrink()
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
        '-----------------------------------------------------------------
        Try

            Dim c As New C

            Dim s As String = "ALTER DATABASE " & My_Settings.DataBase & " SET RECOVERY SIMPLE " & _
            "DBCC SHRINKFILE (" & My_Settings.DataBase & ", 10) " & _
            "DBCC SHRINKDATABASE (" & My_Settings.DataBase & ", 10)" & _
            "ALTER DATABASE " & My_Settings.DataBase & " SET RECOVERY BULK_LOGGED "

            c.Com = New SqlClient.SqlCommand(s, c.Con)


            If SQL_SP_EXEC(c.Com) = True Then
                MsgBox("تـمت  الصيانة ", MsgBoxStyle.Information)
                Application.Restart()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub M_2_btn_Click(sender As Object, e As EventArgs) Handles M_2_btn.Click
        Me.Cursor = Cursors.AppStarting
        Pending_DB()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Pending_DB()

        Dim c As New C
        Dim Con As New SqlConnection("Data Source= " + MY_Settings.S_SERVER + " ;initial catalog= MASTER ;Integrated Security=True;")

        Dim s As String = "ALTER DATABASE [" & My_Settings.DataBase & "] SET EMERGENCY " & _
        "ALTER DATABASE [" & My_Settings.DataBase & "]set single_user " & _
        "DBCC CHECKDB ([" & My_Settings.DataBase & "], REPAIR_ALLOW_DATA_LOSS) WITH ALL_ERRORMSGS; " & _
        "ALTER DATABASE [" & My_Settings.DataBase & "] set multi_user "

        c.Com = New SqlCommand(s, Con)

        Con.Open()

        Try
            c.Com.ExecuteNonQuery()
            MsgBox("تـمت  الصيانة ", MsgBoxStyle.Information)
            Con.Close()
            Application.Restart()
        Catch ex As Exception
            Con.Close()
        End Try

    End Sub

    Private Sub M_3_btn_Click(sender As Object, e As EventArgs) Handles M_3_btn.Click
        Recover_Setting()
    End Sub

    'Public Sub ExportButton_Click()
    '    Try
    '        Dim path As String = GetStartupPath() & "\BackUpSettings.AppSettings"
    '        Using sWriter As New StreamWriter(path)
    '            For Each setting As Configuration.SettingsPropertyValue In My_Settings.PropertyValues

    '                sWriter.WriteLine(setting.Name & "," & setting.PropertyValue.ToString())

    '            Next

    '        End Using

    '        Save_AppSetting
    '        MessageBox.Show("تم اخذ نسخة احتياطية من الاعدادات", "النظام", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    Catch ex As Exception
    '        'Logger.Log(ex, "", "", System.Reflection.MethodBase.GetCurrentMethod().Name)
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub







    'Public Function GetStartupPath()
    '    Try
    '        Dim filePath = Environment.ExpandEnvironmentVariables("%userprofile%\AppData\Local\resturant")
    '        Directory.Delete(filePath, True)
    '        MessageBox.Show("تم تهيئة  الاعدادات بنجاح", "النظام", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Application.Restart()
    '    Catch ex As Exception
    '        'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
    '        MsgBox(ex.Message)
    '    End Try
    'End Function


    'المسار كلمة ssm غيرها باسم مشروعك

    'دالة الاستعادة


    'Private Sub CALL_Fun()

    '    Try
    '        Dim filePath = Environment.ExpandEnvironmentVariables("%userprofile%\AppData\Local\CLASS")
    '        Directory.Delete(filePath, True)
    '        MessageBox.Show("تم تهيئة  الاعدادات بنجاح", "النظام", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Application.Restart()
    '    Catch ex As Exception
    '        'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
    '        MsgBox(ex.Message)
    '    End Try

    '    Try
    '        Dim path As String = Application.StartupPath & "\BackUpSettings.AppSettings"
    '        If File.Exists(path) Then

    '            Using sReader As New StreamReader(path)

    '                While sReader.Peek() > 0
    '                    Try
    '                        Dim input = sReader.ReadLine()

    '                        ' Split comma delimited data ( SettingName,SettingValue )  
    '                        Dim dataSplit = input.Split(CChar(","))
    '                        If dataSplit(1) = "True" Or dataSplit(1) = "False" Then
    '                            '           Setting         Value  
    '                            My_Settings(dataSplit(0)) = Convert.ToBoolean(dataSplit(1))
    '                        Else
    '                            My_Settings(dataSplit(0)) = dataSplit(1)
    '                        End If

    '                    Catch ex As Exception

    '                    End Try
    '                End While

    '            End Using

    '            Save_AppSetting

    '            MessageBox.Show("تم تحميل الاعدادات بنجاح", "النظام", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Else
    '            MessageBox.Show("لم يتم ايجاد ملف الاعدادات الاحتياطي ... فشل استعادة الاعدادات", "النظام", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("يوجد خطأ ... فشل استعادة الاعدادات" & vbNewLine & ex.Message, "النظام", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub M_4_btn_Click(sender As Object, e As EventArgs) Handles M_4_btn.Click
        MyUtilities.RunCommandCom("net start MSSQLSERVER", "/W", True)
    End Sub


    Public Class MyUtilities
        Shared Sub RunCommandCom(command As String, arguments As String, permanent As Boolean)
            Dim p As Process = New Process()
            Dim pi As ProcessStartInfo = New ProcessStartInfo()
            pi.Verb = "runas" ' aka run as administrator
            pi.Arguments = " " + If(permanent = True, "/K", "/C") + " " + command + " " + arguments
            pi.FileName = "cmd.exe"
            p.StartInfo = pi
            p.Start()
        End Sub
    End Class


    Private Sub MaintenanceForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select_DBs_Info()
        'Current_BD_Label.Text = " قاعدة البيانات الحالية : " & My_Settings.DataBase
    End Sub


    Private Sub Select_DBs_Info()
        Try
            Dim c As New C
            Dim Con As New SqlConnection("Data Source= " + MY_Settings.S_SERVER + " ;initial catalog= MASTER ;Integrated Security=True;")
            Dim s As String = "SELECT NAME,state,state_desc,user_access_desc,user_access FROM sys.databases WHERE NAME = '" & My_Settings.DataBase & "'"
            c.Da = New SqlDataAdapter(s, Con)
            c.Da.Fill(c.Dt)
            DataGridViewX.DataSource = c.Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Query_Form.ShowDialog()
    End Sub
End Class