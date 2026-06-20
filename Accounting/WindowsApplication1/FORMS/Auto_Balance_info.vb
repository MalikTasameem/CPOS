Public Class Auto_Balance_info
    Public T_ID As String
    Dim DT As New DataTable
    Private Sub Auto_Balance_info_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        T_ID_txt.Text = T_ID
        SELECT_Balance()
    End Sub

    Public Sub SELECT_Balance()
        If Not String.IsNullOrWhiteSpace(T_ID_txt.Text) Then

            Dim C As New C
            Dim da As New SqlClient.SqlDataAdapter("SELECT DATE,[ACC_CODE],[ACC_NAME], [Bill_Num],[CREDIT],[DEBIT] FROM [dbo].[SALES_SYSTEM_BALANCES_V] WHERE TRAN_ID = '" & T_ID_txt.Text & "' ORDER BY TRAN_ID,DEBIT ASC ", C.Con)
            da.Fill(DT)

            If DT.Rows.Count > 0 Then
                DataGridView1.DataSource = DT
            End If

        End If
    End Sub

    Private Sub DataGridView1_DataSourceChanged(sender As Object, e As EventArgs) Handles DataGridView1.DataSourceChanged


        If DT.Rows.Count > 0 Then
            Compute_Balance(DT)
            Total_C_txt.Text = T_CREDIT.ToString()
            Total_D_txt.Text = T_DEBIT.ToString()
            TOTAL_C_N.Text = Module1.TOTAL_C_N
            TOTAL_D_N.Text = Module1.TOTAL_D_N

            Rows_txt.Text = DT.Rows.Count

            Total_B_txt.Text = Convert.ToDouble(Total_D_txt.Text) - Convert.ToDouble(Total_C_txt.Text)
        Else
            Total_C_txt.Text = 0
            Total_D_txt.Text = 0
            Total_B_txt.Text = 0
            Rows_txt.Text = 0
            TOTAL_C_N.Text = 0
            TOTAL_D_N.Text = 0
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
End Class