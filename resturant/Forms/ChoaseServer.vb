Public Class ChoaseServer

    Private Sub Set_Btn_Click(sender As Object, e As EventArgs) Handles Set_Btn.Click
        If ServersGrid.Rows.Count > 0 Then
            Beep()
            If MessageBox.Show(" إختيار خادم " + ServersGrid.CurrentRow.Cells(1).Value, "", MessageBoxButtons.OKCancel, _
                   MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                Set_Server(ServersGrid.CurrentRow.Cells(0).Value)
                login.Button23_Click(sender, e)
            End If
        End If

    End Sub

    Private Sub Set_Server(Serv_ID As Integer)
        Try
            My_Settings.SqlConStr = "Data Source= " + My_Settings.Server_Choese_Server + " ;initial catalog='" + My_Settings.DB_Choese_Server + "';Integrated Security=True;"


            Dim C As New C
            C.Con.Open()
            With C.Com
                .Connection = C.Con
                .CommandText = "SERVERS_SELECT_SET"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@Serv_ID", Serv_ID)
            End With
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                My_Settings.Server_Desc = C.Dr("Serv_Desc")
                My_Settings.S_SERVER = C.Dr("Serv_Name")
                My_Settings.DataBase = C.Dr("DB_Name")
                My_Settings.DB_Authentication = C.Dr("DB_Authentication")
                My_Settings.DB_UName = C.Dr("DB_UserName")
                My_Settings.DB_Pass = C.Dr("DB_Pass")
                Save_AppSetting

                Dim Tmp As String = ""
                Dim Str As String = C.Dr("Serv_Name")
                For i = 0 To Str.Count - 1
                    If Str(i) = "," Then
                        Exit For
                    Else
                        Tmp += Str(i)
                    End If
                Next
                My_Settings.SERVER_IP = Tmp

                Build_Connection()
                ServerOptions.Show_Current_Server()
                login.Text = My_Settings.Server_Desc + " - تسجيل الدخــول  "
                Me.Close()
            End If
            C.Con.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ChoaseServer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set_Server(1)
        Load_SERVERS()
    End Sub

    Public Sub Load_SERVERS()
          Try
            My_Settings.SqlConStr = "Data Source= " + My_Settings.Server_Choese_Server + " ;initial catalog='" + My_Settings.DB_Choese_Server + "';Integrated Security=True;"
            Save_AppSetting
            Dim c As New C
            Dim s As String = "select Serv_ID,Serv_Desc from SERVERS"
            c.Da = New SqlClient.SqlDataAdapter(s, My_Settings.SqlConStr)
            c.Da.Fill(c.Dt)
            ServersGrid.DataSource = c.Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Server_DELETE()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SERVERS_DELETE"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Serv_ID", ServersGrid.CurrentRow.Cells(0).Value)
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            Load_SERVERS()
        End If
    End Sub


End Class