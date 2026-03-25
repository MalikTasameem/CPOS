Public Class Rsv_IM

    Private Sub Rsv_IM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IM_LB.Text = F_Sales.AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value
        SB_Rsv_SELECT()
    End Sub

    Private Sub ADDCatButton_Click(sender As Object, e As EventArgs) Handles ADDCatButton.Click
        SB_Rsv_Pros(F_Sales.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value, "")
        'query("INSERT INTO [dbo].[SB_Rsv]([T_ID],[DATE_F]) VALUES (" & F_Sales.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value & ",'" & Date_F.Value & "')")
    End Sub


    Private Sub SB_Rsv_Pros(ID As Integer, Process As String)
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Rsv_Pros"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", F_Sales.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
            .Parameters.AddWithValue("@SB_T_ID", F_Sales.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
            .Parameters.AddWithValue("@DATE_F", Date_F.Value)
            .Parameters.AddWithValue("@Process", Process)
            If SQL_SP_EXEC(C.Com) Then SB_Rsv_SELECT()
        End With
    End Sub


    Public Sub SB_Rsv_SELECT()

        Try
            Dim C As New C
            Dim s As String
            s = "select T_ID,DATE_F,DAYS,MONTHS,YEARS  from SB_Rsv_V  WHERE SB_T_ID = " & F_Sales.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value
            C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
            C.Da.Fill(C.Dt)
            AGMetroGrid.DataSource = C.Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub RemoveCatButton_Click(sender As Object, e As EventArgs) Handles RemoveCatButton.Click
        If AGMetroGrid.Rows.Count > 0 Then SB_Rsv_Pros(AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value, "DELETE")
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub
End Class