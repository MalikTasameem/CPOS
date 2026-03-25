Public Class OPEN_Bills
    Dim Open_Dt As New DataTable
    Private Sub OPEN_Bills_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Fill_Data()
    End Sub

    Private Sub Fill_Data()
        Dim C = New C
        'Dim dt As New DataTable
        With (C.Com)
            .Connection = C.Con
            .CommandText = "Open_AGMV_SELECT"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@USER_ID", USER_ID)
            If U_Save_otherBill = True Then
                .Parameters.AddWithValue("@IS_ADMIN", True)
            Else
                .Parameters.AddWithValue("@IS_ADMIN", User_isAdmin)
            End If

        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(Open_Dt)
        Open_MV_DV.DataSource = Open_Dt

        Open_MV_DV.Columns(0).Visible = False
        Open_MV_DV.Columns(3).Visible = False
        Open_MV_DV.Columns(4).Visible = False
        Open_MV_DV.Columns(6).Visible = False
    End Sub

    Private Sub OPEN_Bills_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Open_MV_DV_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Open_MV_DV.MouseDoubleClick

        Me.Cursor = Cursors.AppStarting
        If Open_MV_DV.Rows.Count > 0 Then
            'If Open_MV_DV.CurrentRow.Cells("AG_MV_Type_ID_CL").Value <> 6 Then
            Select Case Open_MV_DV.CurrentRow.Cells(4).Value
                Case 3, 4
                    T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
                    isShowing_Trans = True
                    F_Receipt = New Receipt
                    F_Receipt.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 5, 6
                    Add_Prev_Balance.is_Select = True
                    Add_Prev_Balance.ShowDialog()

                Case 1
                    FormType = 1

                    Select Case SScreenDefault
                        Case 0
                            SB_is_Fast = False
                            T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
                            isShowing_Trans = True
                            F_Sales = New Sales
                            F_Sales.ShowDialog()
                            isShowing_Trans = False
                            FormType = 0
                        Case 1
                            SB_is_Fast = False
                            If Check_IF_isOrder(Open_MV_DV.CurrentRow.Cells(0).Value) = True Then POS_ENTER_AS_ORDER = True
                            F_POS = New POS
                            T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
                            isShowing_Trans = True
                            F_POS.ShowDialog()
                            isShowing_Trans = False
                        Case 2
                            SB_is_Fast = True
                            T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
                            isShowing_Trans = True
                            Sales_Fast.ShowDialog()
                            isShowing_Trans = False
                    End Select

                Case 7
                    FormType = 2
                    T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
                    isShowing_Trans = True
                    F_Pch = New Pch
                    F_Pch.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 8
                    FormType = 3
                    T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
                    isShowing_Trans = True
                    F_IM_Execute = New IM_Execute
                    F_IM_Execute.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 9
                    FormType = 4
                    T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
                    isShowing_Trans = True
                    F_Invoice = New Invoice
                    F_Invoice.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0
                Case 10
                    FormType = 5
                    T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
                    isShowing_Trans = True
                    Returns.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 11
                    FormType = 6
                    T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
                    isShowing_Trans = True
                    Returns.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 12
                    FormType = 7
                    T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
                    isShowing_Trans = True
                    F_Stores_ImmediateOrder = New Stores_ImmediateOrder
                    F_Stores_ImmediateOrder.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 2
                    FormType = 7
                    T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
                    isShowing_Trans = True
                    F_EXP_Details = New EXP_Details
                    F_EXP_Details.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 13
                    FormType = 9
                    T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
                    isShowing_Trans = True
                    F_Format_Items_Auto = New Format_Items_Auto
                    F_Format_Items_Auto.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 16
                    FormType = 10
                    T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
                    isShowing_Trans = True
                    F_ViewBill = New ViewBill
                    F_ViewBill.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 17
                    FormType = 11
                    T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
                    isShowing_Trans = True
                    F_Inside_Sales = New Inside_Sales
                    F_Inside_Sales.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0


                Case 18
                    FormType = 9
                    T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
                    isShowing_Trans = True
                    F_Format_Items_Manual = New Format_Items_Manual
                    F_Format_Items_Manual.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0

                Case 35
                    FormType = 11
                    T_ID_Trans = Open_MV_DV.CurrentRow.Cells(0).Value
                    isShowing_Trans = True
                    F_Outside_Sales = New Outside_Sales
                    F_Outside_Sales.ShowDialog()
                    isShowing_Trans = False
                    FormType = 0



            End Select
            ' End If
        End If
        Me.Cursor = Cursors.Default
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
End Class