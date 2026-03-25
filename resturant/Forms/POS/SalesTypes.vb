Public Class SalesTypes
    Dim rs As New Resizer
    Public TabNum As Integer = 0
    Private Sub TB_Btn_Click(sender As Object, e As EventArgs) Handles TB_Btn.Click
        Me.Cursor = Cursors.AppStarting
        F_TablesBalance = New TablesBalance
        Set_Form(F_TablesBalance, F_Panel)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub AG_Btn_Click(sender As Object, e As EventArgs) Handles Orders_Btn.Click
        F_OrdersMenu = New OrdersMenu
        Set_Form(F_OrdersMenu, F_Panel)
        F_OrdersMenu.Search_txt.Select()
    End Sub

    Private Sub SB_Btn_Click(sender As Object, e As EventArgs) Handles SB_Btn.Click
        F_BillsMenu = New BillsMenu
        Set_Form(F_BillsMenu, F_Panel)
    End Sub

    Private Sub SalesTypes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Test_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        If S_Tables = False Then
            TB_Btn.Visible = False
            'Else

            '    If U_End_Table = False And U_Transfer_Table = False Then
            '        TB_Btn.Visible = False
            '    End If

        End If

        Orders_Btn.Visible = S_Orders

        OrdersWaitingNum()
    End Sub

    Public Sub OrdersWaitingNum()
        Dim C As New C
        C.Str = "SELECT COUNT(T_ID) AS NUM from OrdersMenu_V WHERE Order_isDeleverd = 0"
        C.Com = New SqlClient.SqlCommand(C.Str, C.Con)
        C.Con.Open()
        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows = True Then
            C.Dr.Read()
            Orders_Btn.Text = " الطلبيات " + "( " + C.Dr("Num").ToString + " )"
        End If
        C.Con.Close()
    End Sub

    Private Sub Test_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls_POS(Me)
    End Sub

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Home_Panel = "POS"
        F_POS.ResetNewBill()
        ' Set_Form(F_POS, Home.Home_Panel)
        Me.Close()
    End Sub

    Private Sub By_Phone_Unpied_btn_Click(sender As Object, e As EventArgs) Handles By_Phone_Unpied_btn.Click
        F_Search_Phone_Bills = New Search_Phone_Bills
        Set_Form(F_Search_Phone_Bills, F_Panel)
    End Sub
End Class