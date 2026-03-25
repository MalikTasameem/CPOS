Public Class Show_EXP_Rpt_Details
    Dim C As New C
    Dim rs As New Resizer
    Private Sub Show_IM_Rtn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        SB_Contents_SELECT_Bill()
        SendMessage(IM_SH_txt.Handle, &H1501, 0, "إبحث في قائمة الملاحظـــات")
    End Sub

    Public Sub SB_Contents_SELECT_Bill()

        With C.Com
            .Connection = C.Con
            .CommandText = "[EXP_SelectDetails_ONE_IM_By_Date]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@EX_ID", HOME.F_Exp_Report.EXP_GridViewX1.CurrentRow.Cells("EX_ID_CL").Value)
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
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

    Private Sub IM_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles IM_SH_txt.TextChanged
        Dim Dv As DataView
        Dv = C.Dt.AsDataView
        Dv.RowFilter = Serach(IM_SH_txt.Text, "[ABOUT]")
        BillMetroGrid.DataSource = Dv
    End Sub

    Private Sub Show_EXP_Rpt_Details_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub
End Class