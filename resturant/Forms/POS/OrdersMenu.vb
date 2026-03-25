Public Class OrdersMenu
    Dim Orders_Dt As New DataTable
    Dim OrdersBill_Dt As New DataTable

    Dim dv As DataView
    Dim RS As New Resizer
    Public Order_T_ID As Integer
    Dim BarcodN As String
    Dim ALL As Boolean = False
    Public SB_ID As Integer
    Public AG_ID As Integer
    'Public TB_isOrderd As Integer = 0


    'Private Sub OrdersMenu_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
    '    If e.KeyCode = Keys.ControlKey Then
    '        If Search_txt.Visible = True Then
    '            Search_txt.Select()
    '        Else
    '            BarcodeSearch_txt.Select()
    '        End If
    '    End If
    'End Sub

    Private Sub OrdersMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RS.FindAllControls(Me)
        If U_SalesVoid = False Then VoidBill_Btn.Visible = False
        Edit_Date_btn.Visible = U_SB_Update
        Deliver_Date.Enabled = U_SB_Update
        TypeCmb.SelectedIndex = 0
        Bar_CB.Checked = MY_Settings.Order_Search_Type
        Zuby.ADGV.AdvancedDataGridView.SetTranslations(Zuby.ADGV.AdvancedDataGridView.LoadTranslationsFromFile(Application.StartupPath & "\" & "lang.json"))
    End Sub

    Private Sub Load_Order_Bills()
        'Me.Cursor = Cursors.AppStarting
        'Orders_Dt.Clear()
        'Dim C = New C
        'With (C.Com)
        '    .Connection = C.Con
        '    .CommandText = "SB_SELECT_Orders"
        '    .CommandType = CommandType.StoredProcedure
        '    .Parameters.AddWithValue("@Type", TypeCmb.SelectedIndex)
        '    .Parameters.AddWithValue("@Bar_CB", Bar_CB.Checked)
        '    .Parameters.AddWithValue("@SH", Search_txt.Text)
        '    .Parameters.AddWithValue("@ALL", ALL)
        'End With
        'C.Da = New SqlClient.SqlDataAdapter(C.Com)
        'C.Da.Fill(Orders_Dt)
        'OrdersGrid.DataSource = Orders_Dt
        'Me.Cursor = Cursors.Default
        'If Orders_Dt.Rows.Count > 0 Then
        '    OrdersGrid.Visible = True
        '    OrdersGrid.Size = New Size(Me.Size.Width, Me.Size.Height - 50)
        '    If Bar_CB.Checked = True Then Begin_Fetch()
        'Else
        '    OrdersGrid.Visible = False
        '    'If Bar_CB.Checked = True Then MsgBox("لم يتم التعرف على الباركود", MsgBoxStyle.Exclamation, "")
        'End If
        ''If Bar_CB.Checked = True Then Search_txt.Clear()
        If Only_IM_View_CB.Checked = True Then
            Load_Order_For_IM()
        Else
            Load_Order_For_Bills()
        End If
    End Sub
    Private Sub Load_Order_For_Bills()

        Refresh_Order_grid()
        Dim c As New C

        Try
            Orders_Dt.Clear()
            Dim Main_Query As String
            Main_Query = "SELECT T_ID,SB_ID AS 'ت.الفاتورة',Barcode AS 'باركود الطلبية',CONVERT(Date,Date) AS 'تاريخ',CONVERT(DATE,Deliver_date) AS 'تاريخ الإستلام',Ag_name AS 'الزبون',AG_Barcode AS 'باركود الزبون' , Cr_Phone AS ' الإسم ' from OrdersMenu_V "
            Dim middle As String = " where 1=1 "
            Dim last As String = " ORDER By Deliver_date ASC "

            If Bar_CB.Checked = True Then
                middle = middle & " AND Barcode = '" & Search_txt.Text & "' "
            ElseIf Not String.IsNullOrWhiteSpace(Search_txt.Text) Then
                middle = middle & " And (Ag_name LIKE '%'+ '" & Search_txt.Text & "' + '%' OR AG_Barcode Like '%'+ '" & Search_txt.Text & "' + '%' ) "
            End If

            Select Case TypeCmb.SelectedIndex
                Case 0
                    middle = middle & " and Order_isDeleverd = 0 "
                Case 1
                    middle = middle & " and Order_isDeleverd = 1 "
            End Select

            Main_Query = Main_Query & middle & last

            c.Da = New SqlClient.SqlDataAdapter(Main_Query, c.Con)
            c.Da.SelectCommand.CommandTimeout = 120
            c.Da.Fill(Orders_Dt)
            '  OrdersGrid.DataSource = Orders_Dt
            DataB.DataSource = Orders_Dt
            OrdersGrid.DataSource = DataB

            If Orders_Dt.Rows.Count > 0 Then
                OrdersGrid.Visible = True
                OrdersGrid.Size = New Size(Me.Size.Width, 750)
                If Bar_CB.Checked = True Then Begin_Fetch()
            Else
                OrdersGrid.Visible = False
            End If

            OrdersGrid.Columns("T_ID").Visible = False
            AddColBT()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Load_Order_For_IM()

        Refresh_Order_grid()
        Dim c As New C

        Try
            Orders_Dt.Clear()
            Dim Main_Query As String
            Main_Query = "SELECT Barcode AS 'باركود الطلبية',CONVERT(Date,Date) AS 'تاريخ',CONVERT(DATE,Deliver_date) AS 'تاريخ الإستلام',Ag_name AS 'الزيون',IM_Name AS 'الصنف',QTY AS 'العدد' from Order_Menu_IM_V "
            Dim middle As String = " where 1=1 "
            Dim last As String = " ORDER By Deliver_date ASC "

            If Bar_CB.Checked = True > 0 Then
                middle = middle & " AND Barcode = '" & Search_txt.Text & "' "
            ElseIf Not String.IsNullOrWhiteSpace(Search_txt.Text) Then
                middle = middle & " And (Ag_name LIKE '%'+ '" & Search_txt.Text & "' + '%' ) "
            End If

            Select Case TypeCmb.SelectedIndex
                Case 0
                    middle = middle & " and Order_isDeleverd = 0 "
                Case 1
                    middle = middle & " and Order_isDeleverd = 1 "
            End Select

            Main_Query = Main_Query & middle & last

            c.Da = New SqlClient.SqlDataAdapter(Main_Query, c.Con)
            c.Da.SelectCommand.CommandTimeout = 120
            c.Da.Fill(Orders_Dt)
            '  OrdersGrid.DataSource = Orders_Dt
            DataB.DataSource = Orders_Dt
            OrdersGrid.DataSource = DataB

            If Orders_Dt.Rows.Count > 0 Then
                OrdersGrid.Visible = True
                OrdersGrid.Size = New Size(Me.Size.Width, 750)
                If Bar_CB.Checked = True Then Begin_Fetch()
            Else
                OrdersGrid.Visible = False
            End If

            '   OrdersGrid.Columns("T_ID").Visible = False
            '    AddColBT()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AddColBT()
        Dim bt As New DataGridViewButtonColumn
        bt.UseColumnTextForButtonValue = True
        bt.Text = "عــرض"
        OrdersGrid.Columns.Add(bt)
    End Sub

    Private Sub Refresh_Order_grid()
        DataB.Dispose()
        DataB = New BindingSource
        OrdersGrid.DataSource = Nothing
        OrdersGrid.CleanFilter()
        OrdersGrid.Columns.Clear()
        Orders_Dt = New DataTable
    End Sub
    '---------------------------------------------------------------------------------------------------------------------

    Private Sub OrdersMenu_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        RS.ResizeAllControls_POS(Me)
    End Sub


    Private Sub TypeCmb_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TypeCmb.SelectedIndexChanged
        Search_txt_TextChanged(sender, e)
    End Sub

    Private Sub Search_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Search_txt.KeyDown

        If Bar_CB.Checked = False Then
            If e.KeyCode = Keys.Down Then If OrdersGrid.Visible = True Then OrdersGrid.Select()
            If e.KeyCode = Keys.Return Then
                If OrdersGrid.Visible = False Then
                    Load_Order_Bills()
                Else
                    Begin_Fetch()
                End If
            End If
        Else
            If e.KeyCode = Keys.Return Then Load_Order_Bills()
        End If

        If e.KeyCode = Keys.Delete Then Search_txt.Clear()

    End Sub

    Private Sub Search_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Search_txt.KeyPress
        Select Case e.KeyChar
            Case "'", ")", "("
                e.Handled = True
        End Select
    End Sub

    Private Sub Search_txt_TextChanged(sender As Object, e As EventArgs) Handles Search_txt.TextChanged
        OrdersGrid.Visible = False

        If Bar_CB.Checked = False Then
            If Search_txt.Text.Count > 0 Then
                ALL = False
                Load_Order_Bills()
            Else
                ALL = True
                Orders_Dt.Clear()
                OrdersGrid.Visible = False
            End If
        End If

    End Sub



    Private Sub Begin_Fetch()
        If OrdersGrid.Rows.Count > 0 Then
            OrdersGrid.Visible = False
            Order_T_ID = OrdersGrid.CurrentRow.Cells("T_ID").Value
            BarcodN = OrdersGrid.CurrentRow.Cells("باركود الطلبية").Value
            Load_Details()
        End If
    End Sub


    Private Sub SB_Contents_SELECT_Bill()
        OrdersBill_Dt.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Contents_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@SB_T_ID", Order_T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(OrdersBill_Dt)
        MetroGrid.DataSource = OrdersBill_Dt
    End Sub

    Private Sub Clear_Details()
        UserName_txt.Clear()
        OrdersBill_Dt.Clear()
        'Deliver_Date_txt.Clear()
        Pied_txt.Clear()
        Rest_txt.Clear()
        Notes_txt.Clear()
        Bill_Date_txt.Clear()
        Bill_Date_txt.Clear()
        AG_Name_txt.Clear()
        Barcode_txt.Clear()
        Pure_txt.Clear()
        isVoid_LB.Visible = False
        Ag_phone_txt.Clear()
        SB_day_Bill_txt.Clear()
        TotalTextBox.Clear()
        Cr_Phone_Txt.Clear()
        'TB_isOrderd = 0
    End Sub

    Private Sub Load_Details()
        Me.Cursor = Cursors.AppStarting
        Orders_Dt.Clear()
        Dim C = New C
        Dim S As String = " SELECT SB_ID,S_Bill_Pr_ID,AG_Name,Barcode,DATE,ISNULL(pure,0) AS pure,First_Pied,ISNULL(About,'') as About,ISNULL(Rest,'0.00') as Rest,Deliver_date as Deliver_date, " &
            " isnull(Order_isDeleverd,0) as Order_isDeleverd,UserName,isVoid,Ag_phone,AG_ID,TB_isOrderd,Total,Discount,isnull(Cr_Phone,'') as Cr_Phone from OrdersMenu_V WHERE T_ID = '" & Order_T_ID & "'"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            C.Dr.Read()

            Search_txt.Clear()
            Clear_Details()

            AG_ID = C.Dr("AG_ID")
            SB_ID = C.Dr("SB_ID")
            SB_day_Bill_txt.Text = C.Dr("S_Bill_Pr_ID")
            Bill_Date_txt.Text = C.Dr("Date")
            AG_Name_txt.Text = C.Dr("Ag_Name")
            Barcode_txt.Text = C.Dr("Barcode")
            UserName_txt.Text = C.Dr("UserName")
            Ag_phone_txt.Text = C.Dr("Ag_phone")

            TotalTextBox.Text = C.Dr("Total")
            DiscountTextBox.Text = C.Dr("Discount")

            Pure_txt.Text = C.Dr("pure")

            'If IsDBNull(C.Dr("Deliver_date")) Then
            '    Deliver_Date_txt.Text = "غير محدد"
            'Else
            '    Deliver_Date_txt.Text = C.Dr("Deliver_date")
            'End If

            If IsDBNull(C.Dr("Deliver_date")) Then
                Deliver_LB.Visible = True
                Deliver_LB.Text = "غير محدد"
                Deliver_Date.Checked = False
            Else
                Deliver_LB.Visible = False
                Deliver_Date.Text = C.Dr("Deliver_date")
                Deliver_Date.Checked = True
            End If

            Pied_txt.Text = Select_Sales_Receipt(Order_T_ID)

            Rest_txt.Text = Convert.ToDouble(Pure_txt.Text) - Convert.ToDouble(Pied_txt.Text)

            'If C.Dr("Rest") = 0 Then
            '    Cash_btn.Enabled = False
            'Else
            '    Cash_btn.Enabled = True
            'End If

            Notes_txt.Text = C.Dr("About")
            If C.Dr("Order_isDeleverd") = True Then
                OrderDeliver_btn.Enabled = False
                Cancel_Recive_btn.Enabled = True
            Else
                OrderDeliver_btn.Enabled = True
                Cancel_Recive_btn.Enabled = False
            End If

            If C.Dr("isVoid") = 1 Then
                OrderDeliver_btn.Enabled = False
                Cash_btn.Enabled = False
                VoidBill_Btn.Enabled = False
                isVoid_LB.Visible = True
                Print_Costmer_Bill_btn.Enabled = False
                Print_IN_btn.Enabled = False
                Cancel_Recive_btn.Enabled = False
            Else
                Cash_btn.Enabled = True
                VoidBill_Btn.Enabled = True
                isVoid_LB.Visible = False
                Print_IN_btn.Enabled = True
                Cancel_Recive_btn.Enabled = True
            End If

            Cr_Phone_Txt.Text = C.Dr("Cr_Phone")

            SB_Contents_SELECT_Bill()
        End If
        C.Con.Close()
        Me.Cursor = Cursors.Default
    End Sub


    Private Function Select_Sales_Receipt(T_ID As Integer)
        Dim C = New C
        Dim Value As Double = 0
        Try
            Dim S As String = "SELECT ISNULL(SUM(Value),0) AS PIED from SB_Receipts_V WHERE Receipt_Tran_ID = '" & T_ID & "' AND isVoid = 0"
            C.Com = New SqlClient.SqlCommand(S, C.Con)
            C.Con.Open()
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                Value = C.Dr("PIED")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return Value
    End Function

    Private Sub Deliver_Order(Order_isDeleverd As Boolean)
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "Deliver_Order"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Order_T_ID)
            .Parameters.AddWithValue("@Order_isDeleverd", Order_isDeleverd)
            .Parameters.AddWithValue("@User_ID", USER_ID)
            .Parameters.AddWithValue("@Tr_ID", SB_TR_ID)
            .Parameters.AddWithValue("@Pr_ID", Pr_ID)
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            F_SalesMenu.OrdersWaitingNum()
            Beep()
            If Convert.ToDouble(Rest_txt.Text) > 0 Then
                If Order_isDeleverd = True Then
                    If MessageBox.Show("إستلام مبلغ ... إيصـال قبـض", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                  MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.Yes Then
                        Switch_To_Reciept()
                    End If
                End If
            End If
            OrderDeliver_btn.Enabled = False
            Clear_Details()
        End If
    End Sub

    Private Sub Bar_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Bar_CB.CheckedChanged

        CB_CHecked(sender)
        My_Settings.Order_Search_Type = Bar_CB.Checked
        Save_AppSetting()
    End Sub

    Private Sub Cash_btn_Click(sender As Object, e As EventArgs) Handles Cash_btn.Click

        Switch_To_Reciept()
        Load_Details()
        Pied_txt.Text = Select_Sales_Receipt(Order_T_ID)
    End Sub


    Private Sub Switch_To_Reciept()
        isOrder = True
        FormType = 1
        AG_Type = 3
        F_Receipt = New Receipt
        Receipt_Tran_ID = Order_T_ID

        With F_Receipt
            Rct_Tr_ID = SB_TR_ID
            ' .ClearFields()
            .Fields_Panel.Enabled = True
            .AG_Cm.Set_IM_By_ID(AG_ID)
            .AG_Cm.Enabled = False
            .Barcode_SH_txt.Enabled = False
            .Receipt_Title_combobox.Text = "فاتورة مبيعات : " + SB_day_Bill_txt.Text + "  (طلبية)  \ باركود : " + Barcode_txt.Text
            .Current_QTY.Text = Show_AG_T_Balance(AG_ID)
            .Fetch_AG_Currency()
            .money_num_txtb.Text = Convert.ToDouble(Rest_txt.Text)

            .CR_Phone_Txt.Text = Cr_Phone_Txt.Text
        End With

        F_Receipt.ShowDialog()
        isOrder = False
    End Sub


    Private Sub OrderDeliver_btn_Click(sender As Object, e As EventArgs) Handles OrderDeliver_btn.Click

        If MetroGrid.Rows.Count > 0 Then
            If IM_min_QTY = False Then
                If IM_Check_Neg_QTY_For_Deliver_Order() = 1 Then

                    Beep()
                    Dim MSG_1 As New OK
                    MSG_1.Msg_Lb.Text = " لا يمكن سحب كمية بالسالب للصنف  " & Str_Name
                    MSG_1.ShowDialog()

                    'MsgBox(" لا يمكن سحب كمية بالسالب للصنف  " & Str_Name, MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            End If
        End If

        Beep()
        Dim MSG As New YesNo
        MSG.Msg_Lb.Text = " تسليم الطلبية "
        MSG.ShowDialog()
        If MSG.Result = True Then Deliver_Order(True)

    End Sub

    Private Function IM_Check_Neg_QTY_For_Deliver_Order()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_For_Deliver_Order"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@T_ID", Order_T_ID)
            .Parameters.Add("@Str_Name", SqlDbType.Char, 1500)
            .Parameters("@F").Direction = ParameterDirection.Output
            .Parameters("@Str_Name").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                F = .Parameters("@F").Value
                Str_Name = .Parameters("@Str_Name").Value
            End If
        End With
        Return F
    End Function

    Private Sub Print_IN_btn_Click(sender As Object, e As EventArgs) Handles Print_IN_btn.Click
        Me.Cursor = Cursors.AppStarting
        Order_SB_Ksh()
        Me.Cursor = Cursors.Default
    End Sub

    Public Sub OrderCashPrint()

        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & OrderBill_AG_Bill_Track)
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, SB_day_Bill_txt.Text)
            .rp.SetParameterValue(1, UserName_txt.Text)
            .rp.SetParameterValue(2, Pure_txt.Text)
            .rp.SetParameterValue(3, "طلبية")
            .rp.SetParameterValue(4, Order_T_ID)
            .rp.SetParameterValue(5, "*" + Barcode_txt.Text + "*")
            .rp.SetParameterValue(6, SBill_Title_1)
            .rp.SetParameterValue(7, SBill_Footer)
            .rp.SetParameterValue(8, Barcode_txt.Text)
            .rp.SetParameterValue(9, "0")
            .rp.SetParameterValue(10, "0")
            'If OrderPage_ID = 1 Then
            '    .rp.SetParameterValue(9, Pied_txt.Text)
            '    .rp.SetParameterValue(10, Rest_txt.Text)
            'End If
        End With

        If OrderPage_ID = 1 Then
            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_80))
            pp.rp.PrintOptions.PrinterName = Default_Printer_80
        Else
            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_A4))
            pp.rp.PrintOptions.PrinterName = Default_Printer_A4
        End If
        pp.rp.PrintToPrinter(1, False, 0, 0)
        pp.rp.Dispose()

        'Dim p As New print
        'p.CrystalReportViewer1.ReportSource = pp.rp
        'p.Show()
    End Sub

    Public Sub Order_SB_Ksh()

        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & OrderBill_IN_Bill_Track)
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, Order_T_ID)
            .rp.SetParameterValue(1, "*" + Barcode_txt.Text + "*")
        End With

        If OrderPage_ID = 1 Then
            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_80))
            pp.rp.PrintOptions.PrinterName = Default_Printer_80
        Else
            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_A4))
            pp.rp.PrintOptions.PrinterName = Default_Printer_A4
        End If
        pp.rp.PrintToPrinter(1, False, 0, 0)
        pp.rp.Dispose()

        'Dim p As New print
        'p.CrystalReportViewer1.ReportSource = pp.rp
        'p.Show()

    End Sub

    Private Sub VoidBillBtn_Click(sender As Object, e As EventArgs) Handles VoidBill_Btn.Click
        Beep()
        Dim MSG As New YesNo
        MSG.Msg_Lb.Text = " إلغـاء الفاتورة بشكل نهائي "
        MSG.ShowDialog()
        If MSG.Result = True Then SB_VoidBill()
    End Sub

    Private Sub SB_VoidBill()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "AG_Balance_Void_Row"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Order_T_ID)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            Network_Edit_Tracker_insert("إلغاء الفاتورة (باركود " & Barcode_txt.Text & ")", SB_ID, 1, 3)
            Load_Details()
        End If


    End Sub

    Private Sub OrdersGrid_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Return Then Begin_Fetch()
        If e.KeyCode = Keys.Up Then If OrdersGrid.CurrentRow.Index = 0 Then Search_txt.Select()
    End Sub

    Private Sub Print_Costmer_Bill_btn_Click(sender As Object, e As EventArgs) Handles Print_Costmer_Bill_btn.Click
        Print_Order()
    End Sub

    Private Sub Print_Order()
        Try
            Print_Costmer_Bill_btn.Enabled = False
            OrderCashPrint()
            Print_Costmer_Bill_btn.Enabled = True
        Catch ex As Exception
            Print_Costmer_Bill_btn.Enabled = True
        End Try

    End Sub


    Private Sub Show_IM_btn_Click(sender As Object, e As EventArgs) Handles Show_IM_btn.Click
        If OrdersGrid.Visible = True Then
            OrdersGrid.Visible = False
        Else
            OrdersGrid.Visible = True
            Load_Order_Bills()
            OrdersGrid.Size = New Size(OrdersGrid.Size.Width, 750) 'Me.Size.Height - 20
        End If
    End Sub


    Private Sub Cancel_Recive_btn_Click(sender As Object, e As EventArgs) Handles Cancel_Recive_btn.Click
        Beep()
        If MessageBox.Show(" إستعادة حالة الطلبية لوضع طلبية معلقة ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, _
            MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
            Network_Edit_Tracker_insert(" إستعادة وضع الطلبية باركود : " + Barcode_txt.Text, Pure_txt.Text, 0, 0)
            Deliver_Order(False)
        End If
    End Sub

    Private Sub OrdersMenu_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress

        ''If Bar_CB.Checked = True Then

        'Select Case e.KeyChar
        '    Case "+", "-"
        '        e.Handled = True
        '        Exit Sub
        'End Select


        'If Me.Search_txt.Focused = False Then
        '    Search_txt.Focus()
        '    If Me.Search_txt.Enabled = True Then
        '        Search_txt.Text = e.KeyChar.ToString
        '        Search_txt.SelectionStart = Search_txt.Text.Length
        '        e.Handled = True
        '    End If
        '    '  If e.KeyChar = "+" Or e.KeyChar = "-" Then Search_txt.Clear()
        'End If
        ''End If

    End Sub

    Private Sub OrdersMenu_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F12 Then If OrderDeliver_btn.Enabled = True Then OrderDeliver_btn_Click(sender, e)
    End Sub

    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Open_butt.Click
        Me.Cursor = Cursors.AppStarting
        F_POS.Reset_Fields()
        F_POS.isNewBill = 0
        F_POS.T_ID = Order_T_ID
        F_POS.BillNumTxt.Text = SB_day_Bill_txt.Text
        Switch_To_Cash = True
        F_POS.Fill_Bill_Info()
        F_POS.SB_Contents_SELECT_Bill()
        F_POS.BillTypeCmb.SelectedValue = 3
        F_SalesMenu.Hide()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub OpenCahDR_Btn_Click(sender As Object, e As EventArgs) Handles OpenCahDR_Btn.Click
        Open_Cash_Drawer()
    End Sub

    Private Sub Only_IM_View_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Only_IM_View_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Edit_Date_btn_Click(sender As Object, e As EventArgs) Handles Edit_Date_btn.Click
        AG_Balance_Update_Date_Deliver(Order_T_ID, Deliver_Date)
    End Sub




    Private Sub Deliver_Date_ValueChanged(sender As Object, e As EventArgs) Handles Deliver_Date.ValueChanged
        Deliver_LB.Visible = Not Deliver_Date.Checked
    End Sub

    Private Sub OrdersGrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles OrdersGrid.CellContentClick
        '  If e.ColumnIndex = 0 Then Begin_Fetch()

        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then
            Begin_Fetch()
        End If
    End Sub
End Class