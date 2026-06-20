Public Class Income_Statement_Contents
    Public ACC_CODE As String
    Dim DT As New DataTable

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Income_Statement_Contents_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SendMessage(Search_By_Acc_Name_txt.Handle, &H1501, 0, "إبحث عن إسم حســـاب")
        GET_SUB_BALANCES()
    End Sub

    Private Sub GET_SUB_BALANCES()

                Dim C As New C
        Dim da As New SqlClient.SqlDataAdapter("SELECT
       [ACC_NAME]
      ,CASE WHEN [BALANCE] < 0 THEN [BALANCE] * -1 ELSE [BALANCE] END AS BALANCE
	  FROM ACCOUNTS_TREE_V WHERE [ACC_PARENT] = " & ACC_CODE, C.Con)
        da.Fill(DT)
        DataGridView1.DataSource = DT

        'DataGridView1.Columns(0).Visible = False

        DataGridView1.Columns(1).DefaultCellStyle.Format = "N3"
        'DataGridView1.Columns(5).DefaultCellStyle.Format = "N3"

    End Sub

    Private Sub Search_By_Acc_Name_txt_TextChanged(sender As Object, e As EventArgs) Handles Search_By_Acc_Name_txt.TextChanged
        Dim Dv As DataView
        Dv = DT.AsDataView
        Dv.RowFilter = IM_Serach(Search_By_Acc_Name_txt.Text, "[ACC_NAME]")
        DataGridView1.DataSource = Dv
    End Sub
End Class