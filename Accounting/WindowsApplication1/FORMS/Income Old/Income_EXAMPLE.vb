Public Class Income_EXAMPLE
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Income_EXAMPLE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        query("EXEC [Prepare_Income_Sheet] ")
        LOAD_BALANCES()
    End Sub


    Private Sub LOAD_BALANCES()

        Dim DT As New DataTable
        Dim C As New C
        Dim da As New SqlClient.SqlDataAdapter("SELECT [ID]

      ,[Group_Name]
      ,[Result_Title]
      ,[ACC_CODE]
      ,[ACC_NAME]
      ,[Sign]
      ,[Original_Balance]
      ,[Total_Income]
	  FROM Tmp_IncomeReport_2 ", C.Con)
        da.Fill(DT)
        DataGridView_Master.DataSource = DT
    End Sub
End Class