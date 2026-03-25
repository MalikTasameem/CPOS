Public Class Show_IM_Rtn

    Private Sub Show_IM_Rtn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SB_Contents_SELECT_Bill()
    End Sub

    Public Sub SB_Contents_SELECT_Bill()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Rtn_Contents_SELECT_SB_Rtn"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", F_Sales.T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        BillMetroGrid.DataSource = C.Dt
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Show_IM_Rtn_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub
End Class