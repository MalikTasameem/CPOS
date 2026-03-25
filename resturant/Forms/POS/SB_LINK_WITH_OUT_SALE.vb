Public Class SB_LINK_WITH_OUT_SALE
    Private Sub POS_D_Valid_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub POS_D_Valid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Valid_Allow_IM = True
        Load_Valid()
    End Sub

    Private Sub Load_Valid()

        Dim c As New C
        Try
            Dim s As String
            s = "select OutSale_ID from Agents_Balance_MV  WHERE AG_ID = '" & F_Sales.AG_ID & "' AND BsType_ID = 35 AND OutSale_ID  NOT IN (SELECT Travel_ID FROM Agents_Balance_MV WHERE AG_ID = '2' AND BsType_ID = 1 AND Travel_ID IS NOT NULL ) " &
                " ORDER BY DATE DESC "
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            DataGridViewX1.DataSource = c.Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub ConfermButton_Click(sender As Object, e As EventArgs) Handles ConfermButton.Click
        If DataGridViewX1.Rows.Count > 0 Then

            query("UPDATE Agents_Balance_MV SET Travel_ID = " & DataGridViewX1.CurrentRow.Cells("OutSale_ID_CL").Value & " WHERE T_ID = " & F_Sales.T_ID)
            'F_Sales.OUT_SALE_Bill_TXT.Text = DataGridViewX1.CurrentRow.Cells("OutSale_ID_CL").Value
            Me.Close()

        End If
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click

        Me.Close()
    End Sub
End Class