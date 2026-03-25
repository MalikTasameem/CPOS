Public Class IM_OTHER_STORE

    Dim Open_Dt As New DataTable

    Private Sub OPEN_Bills_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Fill_Data()
        SendMessage(CMSearchTextBox.Handle, &H1501, 0, "إبحث عن صنف")
    End Sub

    Private Sub Fill_Data()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "IM_OTHER_STORE_V_SELECT"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@ST_ID", SB_ST_ID)

            Open_Dt.Clear()
            'Dim s As String = " select T_ID,USER_ID,St_Name,Barcode,item_name,U_Name,QTY,DATE_OF_ARRIV,DAYS,MONTHS,NOTES,USERNAME  from IM_ORDERS_COMING_V ORDER BY (DAYS*-1) ASC"
            'C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
            C.Da = New SqlClient.SqlDataAdapter(C.Com)
            C.Da.Fill(Open_Dt)
            AGMetroGrid.DataSource = Open_Dt

        End With
    End Sub

    Private Sub OPEN_Bills_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Fill_Data()
    End Sub

    Private Sub CMSearchTextBox_TextChanged(sender As Object, e As EventArgs) Handles CMSearchTextBox.TextChanged

        Dim Dv As DataView
        Dv = Open_Dt.AsDataView
        Dv.RowFilter = " item_name LIKE '%" + sender.Text + "%'"
        AGMetroGrid.DataSource = Dv

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim P As New Print_PDF
        P.PRINT_PDF(AGMetroGrid, 1, "بضاعــة في مخــازن أخــرى")
    End Sub

End Class