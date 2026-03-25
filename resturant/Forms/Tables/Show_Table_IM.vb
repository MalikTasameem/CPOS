Public Class Show_Table_IM

    Private Sub Show_Table_IM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TB_Info.Text = F_POS.TB_Name_Lb.Text
        SB_Contents_SELECT_TB(F_POS.TB_ID)
    End Sub


    Public Sub SB_Contents_SELECT_TB(TB_ID As Integer)
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "TB_NotPied_V_SELECT_TB"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@TB_ID", TB_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        IMGrid.DataSource = C.Dt
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Show_Table_IM_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub
End Class