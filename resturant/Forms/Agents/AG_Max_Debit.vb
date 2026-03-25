Public Class AG_Max_Debit

    Private Sub RevMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_AG()
    End Sub



    Public Sub Load_AG()

        Try
            Dim C As New C
            With (C.Com)
                .Connection = C.Con
                .CommandText = "AG_Max_Debit_SELECT"
                .CommandType = CommandType.StoredProcedure
            End With
            C.Da = New SqlClient.SqlDataAdapter(C.Com)
            C.Da.Fill(C.Dt)
            DataGridViewX.DataSource = c.Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Back_ADD_New_IM_btn_Click(sender As Object, e As EventArgs) Handles Back_ADD_New_IM_btn.Click
        Me.Close()
    End Sub
End Class