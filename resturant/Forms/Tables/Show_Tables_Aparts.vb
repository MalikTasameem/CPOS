Public Class Show_Tables_Aparts

    Private Sub Show_IM_Rtn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SB_Contents_SELECT_Bill()
    End Sub

    Public Sub SB_Contents_SELECT_Bill()
        Try
            Dim C As New C
            Dim s As String = "SELECT item_name,SUM(QTY) AS QTY,SUM(Price) AS Price,SUM(T_Price) AS T_Price from TABLES_PREV_APARTS_V WHERE TB_ORDER_CODE =  ( SELECT TB_ORDER_CODE FROM TABLES WHERE TB_ID = '" & F_TablesBalance.TB_Num & "' ) GROUP BY item_name"
            C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
            C.Da.Fill(C.Dt)
            BillMetroGrid.DataSource = C.Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        Dim sum As Double = 0
        For i = 0 To Me.BillMetroGrid.Rows.Count - 1
            sum = sum + BillMetroGrid.Rows(i).Cells("Total_CL_2").Value
        Next
        Me.PureTextBox.Text = sum
        If is_Use_Total_Port = True Then Show_Total_Port(sum)


    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Show_IM_Rtn_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub
End Class