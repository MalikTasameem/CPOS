Public Class SwitchActivation

    Private Sub Set_Btn_Click(sender As Object, e As EventArgs) Handles Set_Btn.Click
        If ServersGrid.Rows.Count > 0 Then
            Beep()
            If MessageBox.Show(" إختيار خادم " + ServersGrid.CurrentRow.Cells(1).Value, "", MessageBoxButtons.OKCancel, _
                   MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                Set_SwitchActivation()
            End If
        End If

    End Sub


    Public Sub Load_SERVERS()
        Try
            Dim c As New C
            c.Str = "select T_ID,CP_NAME,LastShow from Activation_Details WHERE isConnected <> 1  OR isConnected IS NULL AND Cpu_ID = '" & My_Settings.Cpu_ID & "'"
            c.Da = New SqlClient.SqlDataAdapter(c.Str, c.Con)
            c.Da.Fill(c.Dt)
            ServersGrid.DataSource = c.Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Set_SwitchActivation()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Set_SwitchActivation"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", ServersGrid.CurrentRow.Cells("T_ID_CL").Value)
            .Parameters.AddWithValue("@CP_NAME", My.Computer.Name)
        End With
        If SQL_SP_EXEC(C.Com) = True Then MsgBox("تم التطبيق ... سيتم إعادة تشغيل النظام", MsgBoxStyle.Information, "")
    End Sub

    Private Sub SwitchActivation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_SERVERS()
    End Sub
End Class