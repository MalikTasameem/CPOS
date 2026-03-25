Public Class SHOW_Agents_Reciepts

    Private Sub RevMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_AG()
    End Sub


    Public Sub Load_AG()

        Try
            Dim C As New C
            With (C.Com)
                .Connection = C.Con
                .CommandText = "Reports_Fetch_AG_Reciepts"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@is_By_Pr", POS_Report.is_By_Pr)
                .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
                .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
                If POS_Report.PeriodsCmb.Text <> "" Then .Parameters.AddWithValue("@Pr_ID", POS_Report.PeriodsCmb.SelectedValue)
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