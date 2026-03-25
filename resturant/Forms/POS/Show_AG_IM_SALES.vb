Public Class Show_AG_IM_SALES

    Private Sub Show_IM_Rtn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_IM_AG()
    End Sub

    Public Sub Load_IM_AG()
        Dim c As New C
        Try
            Dim s As String
            s = "SELECT TOP 10 DATE,Ag_Name,item_name,U_NAME,QTY,Price FROM Agent_SB_IM_MV_V WHERE  IM_ID = '" & F_Sales.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value & "' ORDER BY T_ID DESC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            BillMetroGrid.DataSource = c.Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Show_IM_Rtn_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub
End Class