Public Class Rsv_IM_VIEW
    Dim Open_Dt As New DataTable
    Private Sub OPEN_Bills_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Fill_Data()
    End Sub

    Private Sub Fill_Data()
        Dim C = New C
        'Dim dt As New DataTable
        'With (C.Com)
        '.Connection = C.Con
        '.CommandText = "Open_AGMV_SELECT"
        '.CommandType = CommandType.StoredProcedure
        '.Parameters.AddWithValue("@USER_ID", USER_ID)
        'If U_Save_otherBill = True Then
        '    .Parameters.AddWithValue("@IS_ADMIN", True)
        'Else
        '    .Parameters.AddWithValue("@IS_ADMIN", User_isAdmin)
        'End If

        Dim s As String = " select T_ID,item_name,Ag_name,SB_ID,DATE_F,DAYS,MONTHS,YEARS  from SB_Rsv_V ORDER BY DAYS DESC"
        C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
        C.Da.Fill(Open_Dt)
        AGMetroGrid.DataSource = Open_Dt

        ' End With


        'Open_MV_DV.Columns(0).Visible = False
        'Open_MV_DV.Columns(3).Visible = False
        'Open_MV_DV.Columns(4).Visible = False
        'Open_MV_DV.Columns(6).Visible = False
    End Sub

    Private Sub OPEN_Bills_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Function Check_IF_isOrder(T_ID As Integer) As Boolean
        Dim c As New C
        Dim s As String = "SELECT S_Bills_Type FROM Agents_Balance_MV WHERE T_ID = '" & T_ID & "'"
        Dim com As New SqlClient.SqlCommand(s, c.Con)
        Dim dr As SqlClient.SqlDataReader
        c.Con.Open()
        Try
            dr = com.ExecuteReader
            dr.Read()
            If dr.HasRows = True Then If dr("S_Bills_Type") = 3 Then Return True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return False
    End Function

    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        Dim F As New Print_PDF
        F.PRINT_PDF(AGMetroGrid, 1, "قائمة الأصناف المستأجرة")
    End Sub
End Class