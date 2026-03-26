Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Public Class Sales : Inherits System.Windows.Forms.Form

    Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Public T_ID As Integer
    Public Pied_T_ID As Integer
    Public isDepended As Boolean
    Public isVoid As Boolean
    Public isPause As Boolean
    Public Receipts_DT As New DataTable

    Public isCashReceipt_Success As Boolean = False
    Public isShowingDetails As Boolean = False

    Public IM_ID As Integer = 0
    Public Barcode As String = ""
    ' Public Barcode_IM As String = ""
    ' Dim IM_Dt As New DataTable
    ' Dim IM_QTY As Double = 0
    Public TOTAL As Double = 0
    Public TOTAL_Check As Double = 0
    Public Disc As Double = 0
    Public Pure As Decimal = 0
    Public AG_ID As Integer = 0
    Dim AG_Dt As New DataTable
    ' Dim U_Dt As New DataTable
    ' Public Get_Unit As Boolean = False
    ' Dim U_Cargo As Double = 0
    ' Dim ALL_QTY As Double = 0
    ' Dim Valid_Dt As New DataTable
    Public isNewBill As Integer
    Dim isPied As Integer = 0
    Dim BillUser_ID As Integer

    Public On_Update As Boolean
    Dim U_ID As Integer
    '  Dim Min_SP As Double
    '  Dim Min_SP_2 As Double
    Public IS_Show_Rctp As Boolean = False

    Public SB_ID As Integer

    Dim PREV_AG_ID As Integer
    Public is_Order_IM_Print As Boolean = False

    Dim Old_Disc As Double = 0

    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If On_Update = True Then Edit_butt_Click(sender, e)
        FormType = 0
        'Close_Sale()
        'F_MainForm.Fill_ALL_IM()
        ' If AGMetroGrid.Rows.Count = 0 And isDepended = False Then Delete_Last_Empty_Bill(T_ID)
        Me.Dispose()
    End Sub


    Private Sub Expenses_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Keys.Escape : Me.Close()
            Case Keys.F1
                If New_butt.Enabled = True Then ResetNewBill()
            Case Keys.F12
                If Save_butt.Enabled = True Then Save_butt_Click(sender, e)
            Case Keys.F2
                If Print_btn.Enabled = True Then Print_btn_Click(sender, e)
            Case Keys.F3
                If Edit_butt.Enabled = True And Edit_butt.Visible = True Then Edit_butt_Click(sender, e)
            Case Keys.F4
                If Delete_butt.Enabled = True And Delete_butt.Visible = True Then Delete_butt_Click(sender, e)
            'Case Keys.F5
            '    IM_SH_txt.Select()
            'Case Keys.F8
            '    Barcode_SH_txt.Select()
            Case Keys.F7
                SBPauseBtn_Click(sender, e)

            Case Keys.PageUp
                Up_Bill_btn_Click(sender, e)
            Case Keys.PageDown
                Down_Bill_btn_Click(sender, e)

            Case Keys.F10
                If IM_Check_Panel.Visible = True Then Clear_Check_btn_Click(sender, e)

            Case 107 'Add

                If On_Update = False Then
                    If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
                        If AGMetroGrid.Rows.Count > 0 Then
                            Dim Def As Integer = 1

                            If IM_min_QTY = False Then
                                If IM_Check_Neg_QTY_2() = 1 Then
                                    MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Exclamation, "")
                                    Exit Sub
                                Else
                                    Change_IM_Qty(Def)
                                End If
                            Else
                                Change_IM_Qty(Def)
                            End If


                        End If
                    End If
                End If


            Case 109 'Subtrac
                If On_Update = False Then
                    If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
                        If AGMetroGrid.Rows.Count > 0 Then
                            If AGMetroGrid.CurrentRow.Cells("QTY_CL").Value > 1 Then
                                Dim Def As Integer = -1
                                Change_IM_Qty(Def)
                            End If
                        End If
                    End If
                End If

            Case Keys.F9
                If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And AGMetroGrid.Rows.Count > 0 And On_Update = False Then Change_IM_Details.ShowDialog()

        End Select
    End Sub

    Private Function IM_Check_Neg_QTY_2()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_POS_2"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@T_ID", AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)

            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                F = .Parameters("@F").Value
            End If
        End With

        Return F
    End Function

    Private Sub Change_IM_Qty(def As Integer)
        Dim SB_T_ID As Integer = AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value
        Dim Row_Index As Integer = AGMetroGrid.CurrentCell.RowIndex
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_Contents_Change_IM_Qty"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", SB_T_ID)
            .Parameters.AddWithValue("@Def", def)
            .Parameters.AddWithValue("@On_Update", On_Update)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            SB_Contents_SELECT_Bill()
            AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index).Cells("EX_Name_CL")
            Network_Edit_Tracker_insert(" الصنف:" + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString + " العدد:" + AGMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString + " السعر:" + AGMetroGrid.CurrentRow.Cells("Price_CL").Value.ToString, _
             Bill_ID_Txt.Text, 1, 3)
        End If
    End Sub

    Private Sub Sales_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress

        Select Case e.KeyChar
            Case "+" 'Add
                e.Handled = True

            Case "-" 'Subtrac
                e.Handled = True
        End Select

    End Sub

    Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' If St_Count() = 1 Then All_St_Panel.Visible = False
        FormType = 1

        Check_View_Control()
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        EditState = Edit_butt.Text
        'Load_ST()

        Delete_butt.Visible = U_SalesVoid

        If U_SalesDis = True And isDiscount = True Then
            DiscountPanel.Visible = True
        Else
            DiscountPanel.Visible = False
        End If

        Edit_butt.Visible = U_SB_Update
        ' Min_SP_CB.Visible = U_Sell_Under_Min_SP
        ' Min_SP_2_CB.Visible = U_Sell_Under_Min_SP_2
        Show_Cash_btn.Visible = U_SB_Show_Cash

        'If U_SB_IM_Update = True Then
        '    PriceTextBox.ReadOnly = False
        '    Bercent_TXT.ReadOnly = False
        'Else
        '    PriceTextBox.ReadOnly = True
        '    Bercent_TXT.ReadOnly = True
        'End If

        AG_Show_Balance_CB.Checked = MY_Settings.SB_AG_Show_Balance
        Show_Bill_CB.Checked = MY_Settings.SB_Show_Bill
        Show_Bill_Rest_CB.Checked = MY_Settings.SB_Show_Bill_Rest
        Show_SumPied_CB.Checked = MY_Settings.SB_Show_SumPied


        عرضالتكلفةToolStripMenuItem.Visible = U_SB_Show_IM_COST

        Show_Cash_btn.Visible = S_Pr

        If isShowing_Trans = True Then
            T_ID = T_ID_Trans
            Fill_Bill_Info()
            SB_Contents_SELECT_Bill()
            SelectStateBt()
            New_butt.Enabled = False
            SBPauseBtn.Enabled = False
        Else
            If Open_NewBill_When_OpenSale = True Then ResetNewBill()
        End If

    End Sub

    Public Sub Check_View_Control()
        AGMetroGrid.Columns("Date_CL").Visible = My_Settings.S_Date_CL
        AGMetroGrid.Columns("ST_Name_CL").Visible = My_Settings.S_ST_Name_CL
        AGMetroGrid.Columns("D_Valid_CL").Visible = My_Settings.S_D_Valid_CL
        AGMetroGrid.Columns("IMUnit_CL").Visible = My_Settings.S_IMUnit_CL
        AGMetroGrid.Columns("Price_CL").Visible = My_Settings.S_Price_CL
        AGMetroGrid.Columns("Total_CL").Visible = My_Settings.S_Total_CL
        AGMetroGrid.Columns("Notes_CL").Visible = My_Settings.SP_Notes_CL
        AGMetroGrid.Columns("IMNUM_CL").Visible = My_Settings.S_IMNUM_CL
        AGMetroGrid.Columns("Barcode_CL").Visible = My_Settings.S_Barcode_CL
        AGMetroGrid.Columns("Serial_Code_CL").Visible = My_Settings.S_Serial_Code_CL
        AGMetroGrid.Columns("IM_NOTE_CL").Visible = MY_Settings.S_IM_NOTE_CL
        AGMetroGrid.Columns("IM_Discount_CL").Visible = MY_Settings.S_IM_Discount_CL


        '    Min_SP_Panel.Visible = S_Allow_MinSP

        ' ST_Bercent_Panel.Visible = S_Stores
        Markter_Cm.Visible = S_Marketers
        Marketer_Lb.Visible = S_Marketers
        ' Serial_Code_Panel.Visible = S_SerialCode
        IM_Check_Panel.Visible = SB_is_Check_IM
        AGMetroGrid.Columns("Check_CL").Visible = SB_is_Check_IM
        عرضربحالفاتورةToolStripMenuItem.Visible = U_Show_Bill_Profet
    End Sub

    'Public Sub Load_ST()
    '    Dim c As New C
    '    Try
    '        Dim s As String
    '        s = "select ST_ID,ST_name from STORES ORDER By ST_ID ASC"
    '        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
    '        c.Da.Fill(c.Dt)
    '        ST_cm.DataSource = c.Dt
    '        ST_cm.DisplayMember = "ST_name"
    '        ST_cm.ValueMember = "ST_ID"
    '        ST_cm.SelectedValue = SB_ST_ID
    '        If SB_ST_Can_change = False Then ST_cm.Enabled = False
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Public Sub SB_Contents_SELECT_Bill()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Contents_SELECT_Bill_2"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@SB_T_ID", Me.T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        AGMetroGrid.DataSource = C.Dt
        If AGMetroGrid.Rows.Count > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(AGMetroGrid.Rows.Count - 1).Cells("EX_Name_CL")
    End Sub

    Public Sub Fill_Bill_Info()

        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Info_V_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
        End With
        C.Con.Open()
        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            C.Dr.Read()

            AG_ID = C.Dr("AG_ID")
            AG_Cm.Set_IM_By_ID(AG_ID)
            'GET_AG()
            AG_Label.Text = "رصيد الحســاب: ( " & GET_AG_Balance().ToString & " ) "
            Fill_All_AG_Proj()
            Project_cm.SelectedValue = C.Dr("Proj_ID")

            DateTimeEx.Text = C.Dr("Date")
            BillNumTxt.Text = C.Dr("S_Bill_Pr_ID")
            Barcode = C.Dr("Barcode")

            If C.Dr("isPied") = 1 Then
                Save_butt.Enabled = False
            Else
                Save_butt.Enabled = True
            End If
            Bill_ID_Txt.Text = C.Dr("SB_ID")
            SB_ID = C.Dr("SB_ID")


            If C.Dr("Discount") > 0 Then
                Discount_txt.Text = C.Dr("Discount")
                Disc = C.Dr("Discount")
                'If Discount_Distribute = False Then Pure_txt.Text = C.Dr("Total") - C.Dr("Discount")
                Old_Disc = Disc
            End If

            isVoid = C.Dr("isVoid")
            isDepended = C.Dr("isDepended")

            isPied = C.Dr("isPied")

            User_Name_lb.Text = C.Dr("U_Name") + " - " + C.Dr("Date").ToString
            BillUser_ID = C.Dr("User_ID")
            Notes_txt.Text = C.Dr("About")

            isPause = C.Dr("isPause")
            If isPause = False Then
                SBPauseBtn.Text = "تعليق F7"
            Else
                SBPauseBtn.Text = "إلغاء التعليق"
            End If
            Markter_Cm.Set_IM_By_ID(C.Dr("Markter_ID"))
            Pure = C.Dr("Total") - C.Dr("Discount")

            'OUT_SALE_Bill_TXT.Text = C.Dr("Travel_ID")

            Select_Sales_Receipt(T_ID)
        Else
            AG_ID = Default_AG_ID
            'AG_SH_txt.Text = "نقدي"
            Fetch_ItemToList2()
            VoidLb.Enabled = False
        End If
        C.Con.Close()
    End Sub

    'Public Sub Make_Discount()
    '    DiscountBillBtn.Visible = True
    '    DiscountPanel.Visible = True
    '    DiscountPanel.BringToFront()
    '    MetroGrid.Size = New Size(AGMetroGrid.Size.Width, ImTabPanel.Size.Height)
    '    Dim l1 = PureTextBox.Font.Size
    '    PureTextBox.Font = New System.Drawing.Font("Stencil", l1, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point, CType(0, Byte))
    'End Sub

    Private Sub Enable_Fields()
        AG_Cm.Enabled = True
        ' Show_IM_btn2.Enabled = True
        DateTimeEx.Enabled = True
        Notes_txt.Enabled = True
        Project_cm.Enabled = True
        Show_AG_Projects_btn.Enabled = True
        Markter_Cm.Enabled = True
        Ebable_CatFields()
    End Sub

    Private Sub Disable_Fields()
        AG_Cm.Enabled = False
        ' Show_IM_btn2.Enabled = False
        DateTimeEx.Enabled = False
        Notes_txt.Enabled = False
        Project_cm.Enabled = False
        Show_AG_Projects_btn.Enabled = False
        Markter_Cm.Enabled = False
        Disable_CatFields()
    End Sub

    Private Sub Disable_CatFields()
        'IM_SH_txt.Enabled = False
        ' Show_IM_btn.Enabled = False
        ' Barcode_SH_txt.Enabled = False
        ' QtyTextBox.Enabled = False
        '  PriceTextBox.Enabled = False
        ADDCatButton.Enabled = False
        RemoveCatButton.Enabled = False
    End Sub

    Private Sub Ebable_CatFields()
        ' IM_SH_txt.Enabled = True
        ' Show_IM_btn.Enabled = True
        '  Barcode_SH_txt.Enabled = True
        '  QtyTextBox.Enabled = True
        '  PriceTextBox.Enabled = True
        ADDCatButton.Enabled = True
        RemoveCatButton.Enabled = True
    End Sub


    Public Sub Switch_Dependcy(F As Boolean)

        If F = True Then
            isDepended = 1

            AGMetroGrid.BackgroundColor = Color.LightGreen
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
            AG_Cm.Enabled = False
            Save_butt.Enabled = False
        Else
            isDepended = 0
            AGMetroGrid.BackgroundColor = Color.LightYellow
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
            AG_Cm.Enabled = True
            Save_butt.Enabled = True
        End If

    End Sub

    Private Sub NewStateBt()
        Enable_Fields()
        Save_butt.Enabled = True
        ' Delete_butt.Enabled = False
        Me.Text = "فاتورة مبيعات جديدة"
        'AG_Grid.Visible = False
        AG_Cm.Enabled = True
    End Sub
    Private Sub DeleteOrUpdateStateBt()
        Disable_Fields()
        Save_butt.Enabled = False
        Delete_butt.Enabled = False
        Me.Text = DefaultFormState
    End Sub

    Private Sub SavedStateBt()
        Disable_Fields()
        Save_butt.Enabled = False
        Delete_butt.Enabled = False
        Me.Text = DefaultFormState
    End Sub

    Public Sub SelectStateBt()
        Edit_butt.Text = EditState
        If isVoid = True Then
            VoidLb.Visible = True
            Disable_Fields()
            Save_butt.Enabled = False
            Edit_butt.Enabled = False
            Delete_butt.Enabled = False
            AGMetroGrid.Enabled = True
            AGMetroGrid.BackgroundColor = Color.IndianRed
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.IndianRed
            Print_btn.Enabled = False
            DiscountPanel.Enabled = False
            DeliveryingButton.Enabled = False
        Else

            If isDepended = False Then
                Save_butt.Enabled = True
                Edit_butt.Enabled = False
                Print_btn.Enabled = False
                AGMetroGrid.BackgroundColor = Color.LightYellow
                AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
                Enable_Fields()
                DiscountPanel.Enabled = True
            Else
                Save_butt.Enabled = False
                Edit_butt.Enabled = True
                Print_btn.Enabled = True
                AGMetroGrid.BackgroundColor = Color.LightGreen
                AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
                Disable_Fields()
                DiscountPanel.Enabled = False
                DeliveryingButton.Enabled = True
            End If

            VoidLb.Visible = False
            Delete_butt.Enabled = True

            If U_Save_otherBill = False And BillUser_ID <> USER_ID Then
                If Edit_butt.Enabled = True Then Edit_butt.Enabled = False
                If Save_butt.Enabled = True Then Save_butt.Enabled = False
                If Delete_butt.Enabled = True Then Delete_butt.Enabled = False
                Disable_CatFields()
                Disable_Fields()
                If AGMetroGrid.Enabled = True Then AGMetroGrid.Enabled = False
            End If

        End If

        'SELECT_IM()

        Me.Text = "فاتورة مبيعات "

        If AGMetroGrid.Rows.Count = 0 Then DateTimeEx.Value = Date.Now
        Pied_T_ID = Get_Reciept_ID()
        Get_Rtn_Count()
    End Sub

    Public Function Get_Reciept_ID()
        Dim c As New C
        Try
            Dim s As String
            s = "select T_ID from Receipts_V WHERE Receipt_Tran_ID = '" & T_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Return c.Dr("T_ID")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return 0
    End Function

    Public Sub Get_Rtn_Count()
        Dim c As New C
        Dim N As Double = 0
        Try
            Dim s As String
            s = "select ISNULL(SUM(T_PRICE),0) AS S from Rtn_Contents WHERE Orginal_Bill_T_ID = '" & T_ID & "' AND isDepended=1"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                N = c.Dr("S")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Rtn_Count_txt.Text = N
    End Sub

    Private Sub ClearFields()
        isCashReceipt_Success = False
        T_ID = 0
        Pied_T_ID = 0
        Notes_txt.Clear()
        ' PriceTextBox.Clear()
        Total_TextBox.Clear()
        Receipts_DT.Clear()
        DateTimeEx.Text = Date.Now
        VoidLb.Visible = False
        isVoid = False
        isDepended = False
        '  ClearCatFields()
        Discount_txt.Clear()
        Disc = 0
        Me.Text = FormState
        Edit_butt.BackColor = Color.WhiteSmoke
        Edit_butt.Text = EditState
        On_Update = False
        SB_ID = 0
        Project_cm.SelectedIndex = -1
        Markter_Cm.Set_IM_By_ID(1)
        ' ST_cm.SelectedValue = SB_ST_ID
    End Sub

    Private Sub ResetNewBill()
        Load_PauseBills()
        ClearFields()
        Call_New_Bill()
        NewStateBt()
    End Sub


    Public Sub Call_New_Bill()
        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "Call_New_SalesBill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", 0)
            .Parameters.AddWithValue("@AG_ID", SB_Default_AG)
            .Parameters.AddWithValue("@Bill_Num", 0)
            .Parameters.AddWithValue("@SB_ID", 0)
            .Parameters.AddWithValue("@isNew", 0)
            '.Parameters.AddWithValue("@SB_Type", SB_DefaultStatus)
            .Parameters.AddWithValue("@Pr_ID", Pr_ID)
            .Parameters.AddWithValue("@isPied", 0)
            .Parameters.AddWithValue("@User_ID", USER_ID)
            .Parameters("@T_ID").Direction = ParameterDirection.Output
            .Parameters("@Bill_Num").Direction = ParameterDirection.Output
            .Parameters("@isNew").Direction = ParameterDirection.Output
            .Parameters("@SB_ID").Direction = ParameterDirection.Output
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            Me.T_ID = C.Com.Parameters("@T_ID").Value
            Bill_ID_Txt.Text = C.Com.Parameters("@SB_ID").Value.ToString()
            BillNumTxt.Text = C.Com.Parameters("@Bill_Num").Value.ToString()
            isNewBill = C.Com.Parameters("@isNew").Value
            SB_Contents_SELECT_Bill()
            Fill_Bill_Info()
            SelectStateBt()
        End If
    End Sub

    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click

        If AGMetroGrid.Rows.Count > 0 Then

            If AG_ID = 0 Then
                Beep()
                MsgBox("حدد إسم العميل", MsgBoxStyle.Critical, "خطأ في الإعتماد")
                AG_Cm.Focus()
            Else
                If String.IsNullOrWhiteSpace(AG_Cm.Textt) Then
                    AG_ID = Default_AG_ID
                    AG_Cm.Set_IM_By_ID(AG_ID)
                End If
                Beep()

                Save_AG_Name(T_ID, AG_ID, On_Update)
                Save_About(T_ID, Notes_txt.Text)
                Save_Date(T_ID, DateTimeEx)
                Save_Pro()
                ConfermBill()
                AG_Label.Text = "رصيد الحســاب: ( " & GET_AG_Balance().ToString & " ) "
            End If

            isCashReceipt_Success = False

        End If

    End Sub

    Public Sub ConfermBill()

        Dim F As New Pay_Main_Form
        F.Temp_Tr_ID = SB_TR_ID
        F.AG_ID = AG_ID
        F.MONEY_VALUE = Pure
        F.ShowDialog()

        If F.is_OK = True Then
            Dim Tr_ID, Pay_ID As Integer
            Tr_ID = F.Tr_ID
            Pay_ID = F.Pay_ID


            Dim c As New C
            With c.Com
                .Connection = c.Con
                .CommandText = "SB_ConfermBill"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@T_ID", Me.T_ID)
                .Parameters.AddWithValue("@TOTAL", TOTAL)
                .Parameters.AddWithValue("@Discount", Disc)
                .Parameters.AddWithValue("@Pure", Pure)
                If AG_ID <> Default_AG_ID Then .Parameters.AddWithValue("@Pied", Piedmoney_txt.Text)
                .Parameters.AddWithValue("@AGType_ID", 1)
                .Parameters.AddWithValue("@Tr_ID", Tr_ID) 'SB_TR_ID
                .Parameters.AddWithValue("@Pr_ID", Pr_ID)
                .Parameters.AddWithValue("@User_ID", USER_ID)
                .Parameters.AddWithValue("@Pay_ID", Pay_ID)
            End With
            If SQL_SP_EXEC(c.Com) = True Then
                Switch_Dependcy(1)
                If SB_AutoOpenDrawer = True Then Open_Cash_Drawer()
                If SB_AutoPrint = True Then
                    Me.Cursor = Cursors.AppStarting
                    CashPrint(Sales_BillPage_Bill_Track, Sales_Page_ID)
                    Me.Cursor = Cursors.Default
                End If
                SelectStateBt()
                Select_Sales_Receipt(T_ID)
                If MY_Settings.S_OpenNextBill = True Then Call_New_Bill()
                ' SELECT_IM()
            End If


        End If

    End Sub


    Private Sub Delete_butt_Click(sender As Object, e As EventArgs) Handles Delete_butt.Click

        Beep()
        If MessageBox.Show(" سيتم إلغاء الفاتورة رقم " + Bill_ID_Txt.Text + " وكل المعاملات الخاصة بها ... متأكد ", "إلغــاء فاتورة", MessageBoxButtons.OKCancel, _
               MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
            Cancel_Bill()
        End If
    End Sub

    Private Sub Cancel_Bill()

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Void_Row"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)

        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تم إلغاء الفاتورة", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert("إلغاء الفاتورة", Bill_ID_Txt.Text, 1, 3)
            isVoid = True
            SelectStateBt()
        End If

    End Sub

    Private Sub TreasuryCard_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub


    Private Sub Tr_BankNum_TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Total_TextBox.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Public Sub Calc_Total()
        TOTAL = 0
        TOTAL_Check = 0

        If String.IsNullOrWhiteSpace(Discount_txt.Text) Then Disc = 0
        Dim QTY As Double = 0
        For i = 0 To AGMetroGrid.Rows.Count - 1
            TOTAL = TOTAL + AGMetroGrid.Rows(i).Cells("Total_CL").Value
            QTY += AGMetroGrid.Rows(i).Cells("QTY_CL").Value

            If AGMetroGrid.Rows(i).Cells("Check_CL").Value = True Then TOTAL_Check += AGMetroGrid.Rows(i).Cells("Total_CL").Value
        Next

        Total_TextBox.Text = TOTAL.ToString(N_Point_Fter)

        If Discount_Distribute = False Then
            Pure = (TOTAL - Disc)
            TOTAL_Check = (TOTAL_Check - Disc)
        Else
            Pure = TOTAL
        End If

        Pure_txt.Text = Pure.ToString(N_Point_Fter)
        IM_Count_LB.Text = AGMetroGrid.Rows.Count.ToString + " : مواد "
        IM_Qty_LB.Text = QTY.ToString + " : كميات "
        Check_Compute_txt.Text = TOTAL_Check.ToString(N_Point_Fter)

    End Sub


    'Public Sub ADD_IM()


    '    If On_Update = True Then
    '        If AG_ID <> Default_AG_ID Then
    '            If U_AG_Skip_Max = False Then
    '                If CHECK_IF_AGENT_SKIP_MAX_DEBIT(AG_ID) = 1 Then
    '                    MsgBox("عذرا ... هذا الزبون قد تخطى سقف الدين الخاص به ولا يمكنك إضافة المزيد من الأصناف عليه", MsgBoxStyle.Critical, "خطأ فالإدراج")
    '                    Exit Sub
    '                End If
    '            Else
    '                If CHECK_IF_AGENT_SKIP_MAX_DEBIT(AG_ID) = 1 Then MsgBox("هذا الزبون قد تخطى سقف الدين الخاص به", MsgBoxStyle.Exclamation, "تنويه ")
    '            End If
    '        End If
    '    End If


    '    If IM_ID = 0 Then
    '        MsgBox("حددالصنف", MsgBoxStyle.Exclamation)
    '        SELECT_IM()
    '    ElseIf String.IsNullOrWhiteSpace(PriceTextBox.Text) Then
    '        MsgBox("حدد سعر القطعة", MsgBoxStyle.Exclamation)
    '        PriceTextBox.Select()
    '    Else
    '        If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"


    '        If S_Allow_MinSP = True Then
    '            If User_isAdmin = False Then

    '                If U_Sell_Under_Min_SP = True And Min_SP_CB.Checked = True Then
    '                    If Convert.ToDouble(PriceTextBox.Text) < Min_SP And Min_SP > 0 Then
    '                        MsgBox(" ( " + Min_SP.ToString + " ) لا يمكنك البيع بأقل من  سعر الجملة", MsgBoxStyle.Exclamation)
    '                        Exit Sub
    '                    End If

    '                End If

    '                If U_Sell_Under_Min_SP_2 = True And Min_SP_2_CB.Checked = True Then
    '                    If Convert.ToDouble(PriceTextBox.Text) < Min_SP_2 And Min_SP_2 > 0 Then
    '                        MsgBox(" ( " + Min_SP_2.ToString + " ) لا يمكنك البيع بأقل من  سعر جملة الجملة", MsgBoxStyle.Exclamation)
    '                        Exit Sub
    '                    End If

    '                End If

    '            Else

    '                If Convert.ToDouble(PriceTextBox.Text) < Min_SP And Min_SP > 0 Then
    '                    If MessageBox.Show(" ( " + Min_SP.ToString + " ) سوف يتم البيع بأقل من  سعر الجملة", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, _
    '                                       MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then Exit Sub

    '                    If Convert.ToDouble(PriceTextBox.Text) < Min_SP_2 And Min_SP_2 > 0 Then
    '                        If MessageBox.Show(" ( " + Min_SP_2.ToString + " ) سوف يتم البيع بأقل من  سعر جملة الجملة", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, _
    '                                           MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then Exit Sub
    '                    End If

    '                End If
    '            End If
    '        End If


    '        If U_SB_Sell_Under_Cost = False Then
    '            If Show_IM_Cost(False, IM_ID, U_ID) > PriceTextBox.Text Then
    '                MsgBox("لا يمكنك البيع بأقل من سعر التكلفة", MsgBoxStyle.Critical)
    '                Exit Sub
    '            End If
    '        Else
    '            If Show_IM_Cost(False, IM_ID, U_ID) > PriceTextBox.Text Then
    '                If MessageBox.Show(" سوف يتم البيع بأقل من سعر التكلفة", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, _
    '                                              MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then Exit Sub
    '            End If
    '        End If


    '        If IM_Check_Neg_QTY_() = 1 Then
    '            If QTY_ALERT_SOUND = True Then My.Computer.Audio.Play(Application.StartupPath & "\QTY ALERT.wav")
    '            If IM_min_QTY = False Then
    '                MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
    '                Exit Sub
    '            End If
    '        End If

    '        If Valid_Panel.Visible = True And Valid_cm.Items.Count > 0 Then
    '            If Ban_Expierd_IM_MV = True Then

    '                If Convert.ToDateTime(Valid_cm.Text) <= Date.Now.Date Then
    '                    MsgBox("صنف منتهية صلاحيته لا يمكن بيعه", MsgBoxStyle.Critical, "خطأ")
    '                    Exit Sub
    '                End If
    '            End If
    '        End If


    '        If SB_IM_Alert_When_Repetition = True Then
    '            For i = 0 To AGMetroGrid.Rows.Count - 1
    '                If AGMetroGrid.Rows(i).Cells("Bill_IMID_CL").Value = IM_ID Then
    '                    Beep()
    '                    If MessageBox.Show(" هذا الصنف تم إدراجه بالفاتورة ... هل تريد الإستمرار ؟ ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
    '                        Exit Sub
    '                    Else
    '                        Add_ItemToBill(IM_ID)
    '                        Exit Sub
    '                    End If
    '                End If
    '            Next
    '        End If


    '        If Valid_Panel.Visible = True And Valid_cm.Items.Count > 1 Then
    '            Beep()
    '            If MessageBox.Show(" يوجد من هذا الصنف أكثر من صلاحية وانت اخترت صلاحية  " + vbNewLine + Valid_cm.Text + vbNewLine + " هل تريد الإستمرار ؟ ", "", _
    '                               MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.Cancel Then
    '                Exit Sub
    '            End If
    '        End If

    '        Add_ItemToBill(IM_ID)
    '    End If
    'End Sub

    'Private Sub SELECT_IM()
    '    If MY_Settings.S_Default = 0 Then
    '        Barcode_SH_txt.Select()
    '    Else
    '        IM_SH_txt.Select()
    '    End If
    'End Sub


    'Private Function IM_Check_Neg_QTY_()
    '    Dim C As New C
    '    Dim F As Integer = 0
    '    With C.Com
    '        .Connection = C.Con
    '        .CommandText = "IM_Check_Neg_QTY_"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@F", 0)
    '        .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
    '        .Parameters.AddWithValue("@IM_ID", IM_ID)
    '        .Parameters.AddWithValue("@D_Vaild", Valid_cm.Text)
    '        .Parameters.AddWithValue("@Enterd_Qty", QtyTextBox.Text)
    '        .Parameters.AddWithValue("@Cargo", U_Cargo)

    '        .Parameters("@F").Direction = ParameterDirection.Output
    '        If SQL_SP_EXEC(C.Com) Then F = .Parameters("@F").Value
    '    End With

    '    Return F
    'End Function



    ' Private Sub ClearCatFields()
    '  IMDataGridViewX.Visible = False
    '  IM_ID = 0
    ' IM_SH_txt.Clear()
    'Barcode_SH_txt.Clear()
    '   Current_QTY.Clear()
    '   PriceTextBox.Clear()
    '     QtyTextBox.Clear()
    'U_Dt.Clear()

    ' ALL_QTY_txt.Clear()
    ' Valid_QTY_txt.Clear()
    ' Valid_Dt.Clear()
    ' If S_Stores = False Then ST_Bercent_Panel.Visible = False
    'Barcode_IM = ""
    ' Serial_Code_txt.Clear()
    ' LAST_SELL_Lb.Text = "أخر بيع للزبون: "
    ' End Sub


    'Public Sub Add_ItemToBill(IM_ID As String)
    '    Barcode_IM = SELECT_BARCODE(IM_ID, U_ID)
    '    If Not String.IsNullOrWhiteSpace(Bercent_TXT.Text) Then PriceTextBox.Text = _
    '(Convert.ToDouble(PriceTextBox.Text) + Convert.ToDouble(PriceTextBox.Text) * (Convert.ToDouble(Bercent_TXT.Text) / 100)).ToString("00.00")

    '    Dim C As New C
    '    With C.Com
    '        .Connection = C.Con
    '        .CommandText = "SB_Contents_INSERT_2"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@SB_T_ID", Me.T_ID)
    '        .Parameters.AddWithValue("@IM_ID", IM_ID)
    '        .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
    '        If Valid_Panel.Visible = True Then
    '            .Parameters.AddWithValue("@D_Vaild", Valid_cm.Text)
    '            .Parameters.AddWithValue("@Current_QTY", Convert.ToDouble(Current_QTY.Text))
    '        End If

    '        If String.IsNullOrWhiteSpace(QtyTextBox.Text) = False Then .Parameters.AddWithValue("@QYT", Convert.ToDouble(QtyTextBox.Text))
    '        .Parameters.AddWithValue("@U_ID", U_ID)
    '        .Parameters.AddWithValue("@Price", PriceTextBox.Text)
    '        .Parameters.AddWithValue("@On_Update", On_Update)
    '        If Not String.IsNullOrWhiteSpace(Bercent_TXT.Text) Then .Parameters.AddWithValue("@Notes", Bercent_TXT.Text & " % ")

    '        .Parameters.AddWithValue("@Barcode", Barcode_IM)
    '        .Parameters.AddWithValue("@Serial_Code", Serial_Code_txt.Text)


    '        .Parameters.AddWithValue("@is_By_Min_SP", Min_SP_CB.Checked)
    '        .Parameters.AddWithValue("@is_By_Min_SP_2", Min_SP_2_CB.Checked)
    '        .Parameters.AddWithValue("@SB_IM_NEW_ROW", SB_IM_NEW_ROW)
    '    End With

    '    If SQL_SP_EXEC(C.Com) = True Then
    '        If On_Update = True Then
    '            DependingUpdatedBill(T_ID)
    '        End If

    '        Network_Edit_Tracker_insert(" الصنف:" + IM_SH_txt.Text + " الوحدة:" + IM_Unit_cm.Text + " العدد:" + QtyTextBox.Text + " السعر:" + PriceTextBox.Text, Bill_ID_Txt.Text, 1, 1)

    '        SB_Contents_SELECT_Bill()
    '        ClearCatFields()

    '        SELECT_IM()
    '    End If

    'End Sub


    Private Sub SB_Contents_Delete_IM(T_ID_CL As Integer)
        Dim Row_Index As Integer = AGMetroGrid.CurrentCell.RowIndex
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_Contents_Delete_IM"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID_CL)
            .Parameters.AddWithValue("@On_Update", On_Update)
        End With
        If SQL_SP_EXEC(c.Com) = True Then

            'If F_Sales.On_Update = True Then F_Sales.AG_Balance_Update_Pied()

            Network_Edit_Tracker_insert(" الصنف:" + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString + " الوحدة:" + AGMetroGrid.CurrentRow.Cells("IMUnit_CL").Value.ToString + " العدد:" + AGMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString _
                                        + " السعر:" + AGMetroGrid.CurrentRow.Cells("Price_CL").Value.ToString, Bill_ID_Txt.Text, 1, 2)

            SB_Contents_SELECT_Bill()
            If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index - 1).Cells("EX_Name_CL")
            'SELECT_IM()



        End If
    End Sub

    'Private Sub PriceTextBox_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Up : IM_SH_txt.Select()
    '        Case Keys.Down : If AGMetroGrid.Rows.Count > 0 = True Then AGMetroGrid.Select()
    '        Case Keys.Right
    '            IM_Unit_cm.Select()
    '            IM_Unit_cm.DroppedDown = True
    '        Case Keys.Return : QtyTextBox.Select()
    '    End Select
    'End Sub

    'Private Sub PriceTextBox_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub PriceTextBox_TextChanged(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
    'End Sub

    'Private Sub QtyTextBox_KeyDown(sender As Object, e As KeyEventArgs)

    '    Select Case e.KeyCode
    '        Case Keys.Return : ADD_IM()
    '        Case Keys.Up : Barcode_SH_txt.Select()
    '        Case Keys.Down : If AGMetroGrid.Rows.Count > 0 = True Then AGMetroGrid.Select()
    '        Case Keys.Right : PriceTextBox.Select()
    '        Case Keys.Left
    '            If Valid_Panel.Visible = True Then
    '                Valid_cm.DroppedDown = True
    '                Valid_cm.Select()
    '            End If
    '    End Select

    'End Sub

    'Private Sub QtyTextBox_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub QtyTextBox_TextChanged(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
    'End Sub

    Private Sub RemoveCatButton_Click(sender As Object, e As EventArgs) Handles RemoveCatButton.Click
        If AGMetroGrid.Rows.Count > 0 Then
            If MessageBox.Show(" حذف الصنف " + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                SB_Contents_Delete_IM(AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
            End If
        End If
    End Sub

    Private Sub ADD_Fast_AG()
        If GET_AG_NO_SPACES(AG_Cm.Textt) = True Then 'AG_Cm.TXT_ID.Text <> 0 Or
            MsgBox("هذا العميل موجود بالفعل", MsgBoxStyle.Critical, "إضافة عميل")
        ElseIf String.IsNullOrWhiteSpace(AG_Cm.Textt) Then
            MsgBox("أدخل اسم العميل الجديد", MsgBoxStyle.Exclamation)
            AG_Cm.Focus()
        Else
            Beep()
            If MessageBox.Show(" إضافة " + AG_Cm.Textt + " إلى قائمة العملاء ", " إضافة العميل ", MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                Insert_Fast_AG()
            End If
        End If
    End Sub

    Private Function Insert_Fast_AG()
        Dim New_AG_ID As Integer = 0
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "Agents_insert"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@AG_ID", 0)
        sqlComm.Parameters.AddWithValue("@Ag_name", AG_Cm.Textt)
        sqlComm.Parameters.AddWithValue("@Barcode", "")
        sqlComm.Parameters.AddWithValue("@Type_ID", Cr_Type_ID)
        sqlComm.Parameters.AddWithValue("@E_mail", "")
        sqlComm.Parameters("@AG_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تمت إضافة العميــل", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" (من شاشة المبيعات) الزبون:" & AG_Cm.Textt, 0, 27, 1)
            New_AG_ID = sqlComm.Parameters("@AG_ID").Value.ToString()

        End If
        Return New_AG_ID
    End Function


    'Public Sub Load_IM()
    '    Dim c As New C

    '    Try
    '        IM_Dt.Clear()

    '        If SB_Sch_With_QTY = False Then
    '            c.Str = IM_Serach(IM_SH_txt.Text)
    '        Else
    '            c.Str = IM_Serach_With_QTY(IM_SH_txt.Text)
    '        End If

    '        c.Da = New SqlClient.SqlDataAdapter(c.Str, c.Con)
    '        c.Da.Fill(IM_Dt)
    '        IMDataGridViewX.DataSource = IM_Dt
    '        If IM_Dt.Rows.Count > 0 Then
    '            IMDataGridViewX.Visible = True
    '            IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
    '        Else
    '            IMDataGridViewX.Visible = False
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub


    'Private Sub IM_SH_txt_KeyDown(sender As Object, e As KeyEventArgs)

    '    Select Case e.KeyCode
    '        Case Keys.Return
    '            Search_From_Grid()
    '        Case Keys.Down
    '            If IMDataGridViewX.Visible = True Then
    '                IMDataGridViewX.Select()
    '            Else
    '                QtyTextBox.Select()
    '            End If
    '        Case Keys.Left : Barcode_SH_txt.Select()
    '        Case Keys.Delete
    '            ClearCatFields()
    '    End Select

    'End Sub

    'Public Sub Search_From_Grid()
    '    If IMDataGridViewX.Visible = True Then
    '        Fetch_ItemToList()
    '    Else
    '        QtyTextBox.Select()
    '    End If
    'End Sub


    'Private Sub IM_SH_txt_TextChanged(sender As Object, e As EventArgs)
    '    If IM_SH_txt.Text.Count > 0 Then
    '        Load_IM()
    '    Else
    '        ClearCatFields()
    '    End If
    '    If IM_ID = 0 Then
    '        IM_SH_txt.BackColor = Color.LightGray
    '    Else
    '        IM_SH_txt.BackColor = Color.LightGoldenrodYellow
    '    End If

    'End Sub

    'Private Sub IMDataGridViewX_CellClick(sender As Object, e As DataGridViewCellEventArgs)
    '    Fetch_ItemToList()
    'End Sub

    'Private Sub IMDataGridViewX_KeyDown(sender As Object, e As KeyEventArgs)
    '    If e.KeyCode = Keys.Return Then Fetch_ItemToList()
    '    If e.KeyCode = Keys.Up Then If IMDataGridViewX.CurrentRow.Index = 0 Then SELECT_IM()
    'End Sub

    'Private Sub Fetch_ItemToList()

    '    If IMDataGridViewX.Rows.Count > 0 Then

    '        PriceTextBox.Clear()

    '        IM_ID = IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value
    '        IM_SH_txt.Text = IMDataGridViewX.CurrentRow.Cells("item_name_CL").Value
    '        IM_SH_txt.BackColor = Color.LightGoldenrodYellow
    '        Get_Unit = False
    '        Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
    '        Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
    '        Fetch_IM_Units()
    '        Load_IM_Change_Price()
    '        IMDataGridViewX.Visible = False
    '        QtyTextBox.Select()

    '        If IMDataGridViewX.CurrentRow.Cells("isValid_CL").Value = 1 Then
    '            Valid_Panel.Visible = True
    '            Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
    '            IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
    '        Else
    '            Valid_Panel.Visible = False
    '        End If
    '    End If
    'End Sub

    'Public Sub Load_IM_Change_Price()
    '    Dim c As New C
    '    Try
    '        Dim s As String
    '        s = "select Percent_Price from Change_Price WHERE GM_ID = (SELECT GM_ID FROM IM_Menu WHERE IM_ID = '" & IM_ID & "') "
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            ST_Bercent_Panel.Visible = True
    '            Bercent_TXT.Text = c.Dr("Percent_Price")
    '        Else
    '            ST_Bercent_Panel.Visible = False
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Public Sub Fetch_IM_Units()
    '    Get_Unit = False
    '    Dim c As New C
    '    U_Dt.Clear()
    '    Try
    '        Dim s As String
    '        s = "select U_IM_ID,U_Name from IM_Menu_Units_V  WHERE IM_ID = '" & IM_ID & "' Order By U_Cargo Asc"
    '        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
    '        c.Da.Fill(U_Dt)
    '        IM_Unit_cm.DataSource = U_Dt
    '        IM_Unit_cm.DisplayMember = "U_Name"
    '        IM_Unit_cm.ValueMember = "U_IM_ID"
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    '    Get_Unit = True
    '    IM_Fetch_QTY()
    'End Sub

    'Private Sub Show_IM_btn_Click(sender As Object, e As EventArgs)
    '    If IMDataGridViewX.Visible = True Then
    '        IMDataGridViewX.Visible = False

    '    Else
    '        Load_ALL_IM()
    '    End If
    'End Sub

    'Public Sub Load_ALL_IM()
    '    Dim c As New C

    '    Try
    '        IM_Dt.Clear()
    '        Dim s As String
    '        If SB_Sch_With_QTY = False Then
    '            s = "select TOP 100 IM_ID,item_name,isValid from IM_All_V  Order by item_name ASC"
    '        Else
    '            s = "select TOP 100 IM_ID,item_name,isValid from IM_All_V_With_QTY  Order by item_name ASC"
    '        End If

    '        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
    '        c.Da.Fill(IM_Dt)
    '        IMDataGridViewX.DataSource = IM_Dt
    '        If IM_Dt.Rows.Count > 0 Then
    '            IMDataGridViewX.Visible = True
    '            IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
    '        Else
    '            IMDataGridViewX.Visible = False
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Private Sub Barcode_SH_txt_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return : If String.IsNullOrWhiteSpace(Barcode_SH_txt.Text) = False Then Load_IM_Barcode()
    '       ' Case Keys.Down : QtyTextBox.Select()
    '        Case Keys.Delete
    '            Barcode_SH_txt.Clear()
    '            Barcode_IM = ""
    '    End Select
    'End Sub

    ' Public Sub Load_IM_Barcode()


    'If S_is_Multi_BAR = True Then
    '    If Check_IF_Multi_BAR() > 1 Then
    '        SELECT_Multi_Bar()
    '        Exit Sub
    '    End If
    'End If


    'Dim c As New C
    'IM_Dt.Clear()
    'Try
    '    Dim s As String
    '    If Sh_ByNum_CB.Checked = True Then
    '        s = "select IM_ID,item_name,isValid from IM_All_V WHERE IM_NUM = '" & Barcode_SH_txt.Text & "'"
    '    Else
    '        s = "select U_IM_ID,IM_ID,item_name,isValid from IM_units_Search_V WHERE Barcode = '" & Barcode_SH_txt.Text & "'"
    '    End If

    '    c.Com = New SqlClient.SqlCommand(s, c.Con)
    '    c.Con.Open()

    '    c.Dr = c.Com.ExecuteReader
    '    If c.Dr.HasRows Then
    '        c.Dr.Read()
    '        IM_ID = c.Dr("IM_ID")
    '        IM_SH_txt.Text = c.Dr("item_name")
    '        If Sh_ByNum_CB.Checked = False Then Barcode_IM = Barcode_SH_txt.Text
    '        Get_Unit = False
    '        Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
    '        Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
    '        Load_IM_Change_Price()
    '        IMDataGridViewX.Visible = False

    '        If c.Dr("isValid") = 1 Then
    '            Valid_Panel.Visible = True
    '            Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
    '        Else
    '            Valid_Panel.Visible = False
    '        End If

    '        QtyTextBox.Select()
    '        Fetch_IM_Units()
    '        If Sh_ByNum_CB.Checked = False Then IM_Unit_cm.SelectedValue = c.Dr("U_IM_ID")
    '        Barcode_SH_txt.Clear()
    '        If MY_Settings.S_CodeAdd_1 = True Then ADD_IM()
    '    Else
    '        If Barcode_SH_txt.Text.Count >= 8 Then
    ' Check_If_Mizan()
    '        Else
    '            MsgBox("لم يتم التعرف على الإدخال ", MsgBoxStyle.Exclamation)
    '            Barcode_SH_txt.Clear()
    '            Barcode_IM = ""
    '        End If
    '    End If

    'Catch ex As Exception
    '    MsgBox(ex.Message)
    'End Try
    '  End Sub

    'Private Function Check_IF_Multi_BAR()
    '    Dim c As New C
    '    IM_Dt.Clear()
    '    Try
    '        Dim s As String
    '        s = "select COUNT(U_IM_ID) AS C from IM_units_Search_V WHERE Barcode = '" & Barcode_SH_txt.Text & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            Return c.Dr("C")
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    '    Return 1
    'End Function

    'Private Sub SELECT_Multi_Bar()
    '    Dim c As New C
    '    Try
    '        IM_Dt.Clear()
    '        Dim s As String
    '        s = "select IM_ID,item_name,isValid from IM_All_BY_BAR_V WHERE Barcode  = '" & Barcode_SH_txt.Text & "' Order by item_name ASC"
    '        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
    '        c.Da.Fill(IM_Dt)
    '        IMDataGridViewX.DataSource = IM_Dt
    '        If IM_Dt.Rows.Count > 0 Then
    '            IMDataGridViewX.Visible = True
    '            IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
    '        Else
    '            IMDataGridViewX.Visible = False
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub


    'Private Sub Check_If_Mizan()
    '    Dim c As New C
    '    Dim New_Barcode As String = ""
    '    Dim Qty As String = ""
    '    Dim Price As Double = 0
    '    Dim Price_Dot As String = ""

    '    Dim Qty_Dot As String = ""
    '    Dim T_Price As Double = 0
    '    Dim T_Price_Dot As String = ""

    '    Try

    '        For i = Mizan_BarcodeFrom - 1 To Mizan_BarcodeTo - 1
    '            New_Barcode += Barcode_SH_txt.Text(i)
    '        Next

    '        Dim S As String = "Select U_IM_ID,IM_ID,item_name,isValid,Price from IM_units_Search_V WHERE Barcode = '" & New_Barcode & "'"
    '        c.Com = New SqlClient.SqlCommand(S, c.Con)
    '        c.Con.Open()

    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()


    '            If Second_Part_isPrice = 0 Then
    '                For i = Mizan_QtyFrom - 1 To Mizan_QtyTo - 1
    '                    Qty += Barcode_SH_txt.Text(i)
    '                Next
    '                QtyTextBox.Text = Convert.ToDouble(Qty) / 1000
    '            Else


    '                For i = Mizan_QtyFrom - 1 To Mizan_QtyTo - 1
    '                    Qty_Dot += Barcode_SH_txt.Text(i)
    '                Next
    '                Qty = Qty_Dot(0) & Qty_Dot(1) '& Qty_Dot(2)
    '                Qty_Dot = "0" & "." & Qty_Dot(2) & Qty_Dot(3) & Qty_Dot(4)
    '                Qty = Qty + Convert.ToDouble(Qty_Dot)
    '                QtyTextBox.Text = Qty


    '                For j = Mizan_BarcodeTo To Mizan_QtyFrom - 1
    '                    T_Price_Dot += Barcode_SH_txt.Text(j)
    '                Next
    '                T_Price = T_Price_Dot(0) & T_Price_Dot(1) & T_Price_Dot(2)
    '                T_Price_Dot = "0" & "." & T_Price_Dot(3) & T_Price_Dot(4)
    '                T_Price = T_Price + Convert.ToDouble(T_Price_Dot)
    '                '-------------------------------------------------------------------------------

    '                PriceTextBox.Text = Convert.ToDouble(T_Price) / Qty


    '            End If

    '            IM_ID = c.Dr("IM_ID")
    '            IM_SH_txt.Text = c.Dr("item_name")
    '            If Sh_ByNum_CB.Checked = False Then Barcode_IM = New_Barcode
    '            Get_Unit = False
    '            Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
    '            Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
    '            IMDataGridViewX.Visible = False

    '            If c.Dr("isValid") = 1 Then
    '                Valid_Panel.Visible = True
    '                Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
    '            Else
    '                Valid_Panel.Visible = False
    '            End If

    '            QtyTextBox.Select()
    '            Fetch_IM_Units()
    '            IM_Unit_cm.SelectedValue = c.Dr("U_IM_ID")
    '            Barcode_SH_txt.Clear()

    '            If MY_Settings.S_CodeAdd_1 = True Then ADD_IM()

    '        Else
    '            MsgBox("لم يتم التعرف على الإدخال ", MsgBoxStyle.Exclamation)
    '            Barcode_IM = ""
    '            Barcode_SH_txt.Clear()
    '        End If

    '    Catch ex As Exception
    '    End Try
    'End Sub


    ''-------------------------------------------------------------------------<AGENTS FILTER>
    'Public Sub Load_AG()
    '    Dim c As New C

    '    Try
    '        AG_Dt.Clear()
    '        Dim s As String
    '        s = "select AG_ID,Ag_name,ISNULL(T_Balance,0) AS T_Balance from AGENTS_MENU_V WHERE Ag_name Like '%" & AG_SH_txt.Text & "%' AND Type_ID <> '" & Suply_Type_ID & "' Order by Ag_name ASC"
    '        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
    '        c.Da.Fill(AG_Dt)
    '        AG_Grid.DataSource = AG_Dt
    '        If AG_Dt.Rows.Count > 0 Then
    '            AG_Grid.Visible = True
    '            AG_Grid.Size = New Point(AG_Grid.Size.Width, 530)
    '        Else
    '            AG_Grid.Visible = False
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Public Sub GET_AG()
    '    Dim c As New C

    '    Try
    '        AG_Dt.Clear()
    '        Dim s As String
    '        s = "select Ag_name from Agents WHERE Ag_ID = '" & AG_ID & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            AG_SH_txt.Text = c.Dr("Ag_name")
    '            AG_SH_txt.BackColor = Color.LightGoldenrodYellow
    '            AG_Grid.Visible = False
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Private Sub AG_SH_txt_Enter(sender As Object, e As EventArgs)
    '    Set_Ar_Language()
    'End Sub

    'Private Sub AG_SH_txt_KeyDown(sender As Object, e As KeyEventArgs)
    '    If e.KeyCode = Keys.Down Then AG_Grid.Select()

    '    If e.KeyCode = Keys.Return Then If AG_Grid.Visible = True Then Fetch_ItemToList2()

    '    If e.KeyCode = Keys.Delete Then AG_SH_txt.Clear()
    'End Sub

    'Private Sub AG_SH_txt_TextChanged(sender As Object, e As EventArgs)
    '    If AG_SH_txt.Text.Count > 0 Then
    '        Load_AG()
    '    Else
    '        AG_Grid.Visible = False
    '        AG_ID = SB_Default_AG
    '        Save_AG_Name(T_ID, AG_ID, On_Update)
    '    End If
    '    Check_AG_Pied()

    'End Sub

    'Private Sub Check_AG_Pied()
    '    If S_Stores = False Then
    '        If AG_ID = Default_AG_ID Then
    '            AG_SH_txt.BackColor = Color.LightGray
    '        Else
    '            AG_SH_txt.BackColor = Color.LightGoldenrodYellow
    '        End If
    '    End If
    'End Sub

    'Private Sub AG_Grid_CellClick(sender As Object, e As DataGridViewCellEventArgs)
    '    Fetch_ItemToList2()
    'End Sub

    'Private Sub AG_Grid_KeyDown(sender As Object, e As KeyEventArgs)
    '    If e.KeyCode = Keys.Return Then Fetch_ItemToList2()
    '    If e.KeyCode = Keys.Up Then If AG_Grid.CurrentRow.Index = 0 Then AG_SH_txt.Select()
    'End Sub

    Public Sub Fetch_ItemToList2()
        If AG_Cm.TXT_ID.Text > 0 Then

            PREV_AG_ID = AG_ID
            AG_ID = AG_Cm.TXT_ID.Text
            'AG_SH_txt.Text = AG_Grid.CurrentRow.Cells(1).Value
            'AG_SH_txt.BackColor = Color.LightGoldenrodYellow
            'AG_Grid.Visible = False
            'AG_Label.Text = "رصيد الحســاب: ( " & AG_Grid.CurrentRow.Cells(2).Value & " ) "
            If U_AG_Skip_Max = False Then
                If CHECK_IF_AGENT_SKIP_MAX_DEBIT(AG_ID) = 1 Then
                    MsgBox("عذرا ... هذا الزبون قد تخطى سقف الدين الخاص به ولا يمكنك فتح فاتورة جديدة له", MsgBoxStyle.Critical, "خطأ فالإدراج")
                    AG_ID = PREV_AG_ID
                    '  GET_AG()
                    'AG_SH_txt.BackColor = Color.LightGoldenrodYellow
                Else
                    Save_AG_Name(T_ID, AG_ID, On_Update)
                    Network_Edit_Tracker_insert(" تعديل الفاتورة إلي حساب " & AG_Cm.Textt, SB_ID, 1, 3)
                    '   Check_AG_Pied()
                    Fill_All_AG_Proj()
                End If
            Else
                If CHECK_IF_AGENT_SKIP_MAX_DEBIT(AG_ID) = 1 Then MsgBox("هذا الزبون قد تخطى سقف الدين الخاص به", MsgBoxStyle.Exclamation, "تنويه ")
                Save_AG_Name(T_ID, AG_ID, On_Update)
                Network_Edit_Tracker_insert(" تعديل الفاتورة إلي حساب " & AG_Cm.Textt, SB_ID, 1, 3)
                '  Check_AG_Pied()
                Fill_All_AG_Proj()
            End If


        End If
    End Sub

    'Private Sub Show_IM_btn2_Click(sender As Object, e As EventArgs)
    '    If AG_Grid.Visible = True Then
    '        AG_Grid.Visible = False

    '    Else
    '        Fill_All_AG()
    '        AG_Grid.Visible = True
    '        AG_Grid.Size = New Point(AG_Grid.Size.Width, 530)

    '    End If
    'End Sub

    'Private Sub Fill_All_AG()
    '    Try
    '        Dim C As New C
    '        AG_Dt.Clear()
    '        Dim s As String = "SELECT top 100 AG_ID,Ag_name,ISNULL(T_Balance,0) AS T_Balance from AGENTS_MENU_V WHERE Type_ID <> '" & Suply_Type_ID & "' Order by Ag_name ASC"
    '        C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
    '        C.Da.Fill(AG_Dt)
    '        AG_Grid.DataSource = AG_Dt
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    '-------------------------------------------------------------------------</AGENTS FILTER>

    Public Sub Fill_All_AG_Proj()
        Try
            Dim C As New C
            Dim s As String = "SELECT Proj_ID,Proj_NAME from AG_Projects_V WHERE AG_ID = '" & F_Sales.AG_ID & "' ORDER BY Proj_ID DESC"
            C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
            C.Da.Fill(C.Dt)
            Project_cm.DataSource = C.Dt
            Project_cm.ValueMember = "Proj_ID"
            Project_cm.DisplayMember = "Proj_NAME"
            Project_cm.SelectedIndex = -1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Public Sub AG_Balance_Update_Pied()
    'Dim sqlComm As New SqlClient.SqlCommand
    'sqlComm.CommandText = "AG_Balance_Update_Pied"
    'sqlComm.CommandType = CommandType.StoredProcedure
    'sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
    'SQL_SP_EXEC(sqlComm)
    'If Application.OpenForms().OfType(Of SearchAgentBill).Any Then SearchAgentBill.Load_Data()
    ' SearchAgentBill.Load_Data()
    ' End Sub

    Public Sub Save_Pro()
        ' If Project_cm.SelectedValue <= 0 Then Project_cm.SelectedValue = 1

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Update_AG_Project"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@Proj_ID", Project_cm.SelectedValue)
        SQL_SP_EXEC(sqlComm)
    End Sub


    'Private Sub IM_Fetch_QTY()
    '    Dim c As New C
    '    Try
    '        Dim s As String
    '        s = "select U_ID,U_Cargo,Price,Min_SP,Min_SP_2 from IM_Menu_Units_V WHERE U_IM_ID = '" & IM_Unit_cm.SelectedValue & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            U_Cargo = c.Dr("U_Cargo")
    '            Dim N As Double = (Convert.ToDouble(IM_QTY) / c.Dr("U_Cargo"))
    '            Current_QTY.Text = N.ToString("N")
    '            PriceTextBox.Text = c.Dr("Price")
    '            ALL_QTY_txt.Text = ALL_QTY / U_Cargo
    '            U_ID = c.Dr("U_ID")


    '            If Min_SP_CB.Checked = True Then
    '                PriceTextBox.Text = c.Dr("Min_SP")
    '                If c.Dr("Min_SP") = 0 Then PriceTextBox.Clear()
    '            ElseIf Min_SP_2_CB.Checked = True Then
    '                PriceTextBox.Text = c.Dr("Min_SP_2")
    '                If c.Dr("Min_SP_2") = 0 Then PriceTextBox.Clear()
    '            End If

    '            Min_SP = c.Dr("Min_SP")
    '            Min_SP_2 = c.Dr("Min_SP_2")

    '            Load_IM_LAST_SELL_AG()
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub


    'Private Sub Load_IM_LAST_SELL_AG()
    '    Dim c As New C
    '    Try
    '        Dim s As String
    '        s = "SELECT TOP 1 CONVERT(NUMERIC(18,2),Price) AS Price FROM Agent_SB_IM_MV_V WHERE AG_ID = '" & AG_ID & "' AND IM_ID = '" & IM_ID & "' ORDER BY T_ID DESC"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            LAST_SELL_Lb.Text = "أخر بيع للزبون: " & c.Dr("Price")
    '        Else
    '            LAST_SELL_Lb.Text = "أخر بيع للزبون: " & " لا يوجد "
    '        End If


    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs)
    '    If String.IsNullOrWhiteSpace(Current_QTY.Text) = False And Get_Unit = True Then
    '        IM_Fetch_QTY()
    '        If Valid_Panel.Visible = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
    '    End If
    'End Sub

    Private Sub Discount_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Discount_txt.KeyDown

        If Not String.IsNullOrWhiteSpace(Discount_txt.Text) Then
            Discount_calc()
        End If

    End Sub


    Private Sub Discount_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Discount_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Discount_txt_KeyUp(sender As Object, e As KeyEventArgs) Handles Discount_txt.KeyUp
        If String.IsNullOrWhiteSpace(Discount_txt.Text) Then
            Disc = 0
            Pure_txt.Text = TOTAL
        Else
            Discount_calc()
        End If
    End Sub

    Private Sub Discount_txt_TextChanged(sender As Object, e As EventArgs) Handles Discount_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub


    'Private Sub IM_Unit_cm_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return, Keys.Left : QtyTextBox.Select()
    '    End Select
    'End Sub


    Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
        Bill_ID_Txt.Text = Convert.ToInt64(Bill_ID_Txt.Text) - 1
        Get_T_ID()
    End Sub

    Public Sub Get_T_ID()


        Dim C As New C
        Dim S As String = ""
        If Search_By_Bar_CB.Checked = True Then
            S = "Select T_ID From Agents_Balance_MV Where Barcode = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "'"
        Else
            S = "Select T_ID From Agents_Balance_MV Where SB_ID = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "'"
        End If

        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                ClearFields()
                T_ID = C.Dr("T_ID")
                Fill_Bill_Info()
                SB_Contents_SELECT_Bill()
                SelectStateBt()
            Else
                MsgBox("لم يتم التعرف على الفاتورة", MsgBoxStyle.Exclamation)
                If Search_By_Bar_CB.Checked = True Then
                    Bill_ID_Txt.Clear()
                Else
                    Bill_ID_Txt.Text = SB_ID
                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

    End Sub

    Private Sub Up_Bill_btn_Click(sender As Object, e As EventArgs) Handles Up_Bill_btn.Click
        If Not String.IsNullOrWhiteSpace(Bill_ID_Txt.Text) Then
            '  Tmp_Bill_ID = Convert.ToInt64(Bill_ID_Txt.Text)
            Bill_ID_Txt.Text = Convert.ToInt64(Bill_ID_Txt.Text) + 1
            Get_T_ID()
        End If
    End Sub

    Private Sub Bill_ID_Txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Bill_ID_Txt.KeyDown
        If e.KeyCode = Keys.Return Then Get_T_ID()
        If e.KeyCode = Keys.Up Then Up_Bill_btn_Click(sender, e)
        If e.KeyCode = Keys.Down Then Down_Bill_btn_Click(sender, e)
    End Sub

    Private Sub Bill_ID_Txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Bill_ID_Txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub


    Private Sub ADDCatButton_Click(sender As Object, e As EventArgs) Handles ADDCatButton.Click
        'ADD_IM()

        F_SB_IM_card = New SB_IM_card
        F_SB_IM_card.T_ID = T_ID
        F_SB_IM_card.AG_ID = AG_ID
        F_SB_IM_card.ShowDialog()

    End Sub


    Private Sub Show_AG_Balance()
        F_Balances = New Balances
        With F_Balances
            .AG_ID = AG_ID
            .AG_Cm.Set_IM_By_ID(AG_ID)

            .Load_Data()
            .AllAgentsCheckBox.Enabled = False
            .AllRecieptsCheckBox.Checked = True
            .AllUsersCheckBox.Checked = True
            .AllTimeCheckBox.Checked = True
            .AG_MV_Prepare_To_Search()

            .MetroTabControl1.TabPages.Remove(.MetroTabPage2)
            .MetroTabControl1.TabPages.Remove(.MetroTabPage3)
            '.AG_Type_cm.Visible = False
            .MetroTabControl1.TabPages.Remove(.MetroTabPage4)
            .MetroTabControl1.TabPages.Remove(.MetroTabPage5)
            .MetroTabControl1.TabPages.Remove(.MetroTabPage6)
            .MenuStrip1.Visible = False
        End With
        F_Balances.ShowDialog()
    End Sub


    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        If AGMetroGrid.Rows.Count > 0 Then
            Me.Cursor = Cursors.AppStarting
            CashPrint(Sales_BillPage_Bill_Track, Sales_Page_ID)
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Public Function CashPrint(Sales_BillPage_Bill_Track As String, Sales_Page_ID As Integer)
        Dim Balance_Notes As String = ""
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & Sales_BillPage_Bill_Track)
        pp.LoadTables()
        With pp

            Select Case Sales_Page_ID
                Case 9

                    .rp.SetParameterValue(0, BillNumTxt.Text)
                    .rp.SetParameterValue(1, USER_NAME)
                    .rp.SetParameterValue(2, Pure_txt.Text)
                    .rp.SetParameterValue(3, "")
                    .rp.SetParameterValue(4, Me.T_ID)
                    .rp.SetParameterValue(5, SBill_Title_1)
                    .rp.SetParameterValue(6, SBill_Title_2)
                    .rp.SetParameterValue(7, SBill_Footer)
                    .rp.SetParameterValue(8, "*" + Barcode + "*")

                Case Else

                    .rp.SetParameterValue(0, Me.T_ID)
                    .rp.SetParameterValue(1, SBill_Title_1)
                    .rp.SetParameterValue(2, SBill_Title_2)
                    .rp.SetParameterValue(3, SBill_Footer)
                    .rp.SetParameterValue(4, IM_Qty_LB.Text)
                    .rp.SetParameterValue(5, IM_Count_LB.Text)



                    CALCE_REST_AG_BALANCE(Balance_Notes, "N")

                    If Sales_Page_ID = 2 Or Sales_Page_ID = 8 Then

                        .rp.SetParameterValue(6, "*" + Barcode + "*")
                        .rp.SetParameterValue(7, SB_ID)
                        .rp.SetParameterValue(8, Pure - Convert.ToDouble(Piedmoney_txt.Text))

                        'If Show_SumPied_CB.Checked = True Then Balance_Notes += " المدفوع : " & Piedmoney_txt.Text & vbNewLine
                        'If Show_Bill_Rest_CB.Checked = True And AG_ID <> Default_AG_ID Then Balance_Notes += " باقي للفاتورة : " & (Pure - Convert.ToDouble(Piedmoney_txt.Text)).ToString() & vbNewLine

                        'If AG_Show_Balance_CB.Checked = True And AG_ID <> Default_AG_ID Then
                        '    If Convert.ToDouble(GET_AG_Balance()) < 0 Then
                        '        Balance_Notes += " باقي عالحساب : " & (GET_AG_Balance().ToString) & vbNewLine
                        '    ElseIf Convert.ToDouble(GET_AG_Balance()) > 0 Then
                        '        Balance_Notes += " باقي للحساب : " & (GET_AG_Balance().ToString) & vbNewLine
                        '    Else
                        '        Balance_Notes += " الرصيد : " & Convert.ToDouble(GET_AG_Balance().ToString) & vbNewLine
                        '    End If
                        'End If
                        'If Show_Bill_Rest_CB.Checked = True And AG_ID <> Default_AG_ID Then Balance_Notes += " باقي للفاتورة : " & (Pure - Convert.ToDouble(Piedmoney_txt.Text)).ToString() & vbNewLine
                        .rp.SetParameterValue(9, Balance_Notes)

                    Else

                        If Sales_Page_ID = 5 Then
                            Balance_Notes = ""
                            CALCE_REST_AG_BALANCE(Balance_Notes, " , ")
                            .rp.SetParameterValue(8, Balance_Notes & vbNewLine & Notes_txt.Text)
                        Else
                            .rp.SetParameterValue(8, Notes_txt.Text)
                        End If

                        .rp.SetParameterValue(6, HANY(Val(Pure), "EGYPT"))
                        .rp.SetParameterValue(7, "*" + Barcode + "*")


                        If SB_is_Check_Thankes = True And (Convert.ToDouble(Piedmoney_txt.Text) = Convert.ToDouble(Pure_txt.Text)) Then
                            .rp.SetParameterValue(9, "خالــــص مع الشكر")
                        Else
                            .rp.SetParameterValue(9, "")
                        End If

                        If Project_cm.SelectedValue <= 1 Then
                            .rp.SetParameterValue(10, "")
                        Else
                            .rp.SetParameterValue(10, " / " + Project_cm.Text)
                        End If

                        .rp.SetParameterValue(11, Pure - Convert.ToDouble(Piedmoney_txt.Text))
                        .rp.SetParameterValue(12, Piedmoney_txt.Text)

                        If is_Order_IM_Print = False Then
                            'If Show_SumPied_CB.Checked = True Then Balance_Notes += " المدفوع : " & Piedmoney_txt.Text & vbNewLine
                            'If Show_Bill_Rest_CB.Checked = True And AG_ID <> Default_AG_ID Then Balance_Notes += " باقي للفاتورة : " & (Pure - Convert.ToDouble(Piedmoney_txt.Text)).ToString() & vbNewLine

                            'If AG_Show_Balance_CB.Checked = True And AG_ID <> Default_AG_ID Then
                            '    If Convert.ToDouble(GET_AG_Balance()) < 0 Then
                            '        Balance_Notes += " باقي عالحساب : " & Convert.ToDouble(GET_AG_Balance()) & vbNewLine
                            '    ElseIf Convert.ToDouble(GET_AG_Balance()) > 0 Then
                            '        Balance_Notes += " باقي للحساب : " & Convert.ToDouble(GET_AG_Balance()) & vbNewLine
                            '    Else
                            '        Balance_Notes += " الرصيد : " & Convert.ToDouble(GET_AG_Balance()) & vbNewLine
                            '    End If
                            'End If

                            .rp.SetParameterValue(13, Balance_Notes)
                        End If


                        'If Sales_Page_ID = 18 Then
                        '    '.rp.SetParameterValue(14, DateTimeEx.Value)
                        '    '.rp.SetParameterValue(15, AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value)
                        '    '.rp.SetParameterValue(14, OUT_SALE_Bill_TXT.Text)
                        'End If

                    End If

            End Select


        End With

        If Sales_Page_ID <> 2 Then
            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_A4))
            pp.rp.PrintOptions.PrinterName = Default_Printer_A4
        Else
            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_80))
            pp.rp.PrintOptions.PrinterName = Default_Printer_80
        End If

        If Show_Bill_CB.Checked = False Then
            pp.rp.PrintToPrinter(1, False, 0, 0)
            pp.rp.Dispose()
        Else
            Dim p As New print
            p.CrystalReportViewer1.ReportSource = pp.rp
            p.ShowDialog()
        End If

        Return pp
    End Function

    Private Sub CALCE_REST_AG_BALANCE(ByRef Balance_Notes As String, Dot As String)

        Dim BALANCE As Double = GET_AG_Balance()


        If Dot = "N" Then
            If Show_SumPied_CB.Checked = True Then Balance_Notes += " المدفوع : " & Piedmoney_txt.Text & vbNewLine
            If Show_Bill_Rest_CB.Checked = True And AG_ID <> Default_AG_ID Then Balance_Notes += " باقي للفاتورة : " & (Pure - Convert.ToDouble(Piedmoney_txt.Text)).ToString() & vbNewLine
            If AG_Show_Balance_CB.Checked = True And AG_ID <> Default_AG_ID Then
                If Convert.ToDouble(BALANCE) < 0 Then
                    Balance_Notes += " باقي للحساب : " & (BALANCE.ToString) & vbNewLine
                ElseIf Convert.ToDouble(BALANCE) > 0 Then
                    Balance_Notes += " باقي على الحساب : " & (BALANCE.ToString) & vbNewLine
                Else
                    Balance_Notes += " الرصيد : " & Convert.ToDouble(BALANCE.ToString) & vbNewLine
                End If
            End If
            'If Show_Bill_Rest_CB.Checked = True And AG_ID <> Default_AG_ID Then Balance_Notes += " باقي للفاتورة : " & (Pure - Convert.ToDouble(Piedmoney_txt.Text)).ToString() & vbNewLine
        Else

            If Show_SumPied_CB.Checked = True Then Balance_Notes += " المدفوع : " & Piedmoney_txt.Text & " , "
            If Show_Bill_Rest_CB.Checked = True And AG_ID <> Default_AG_ID Then Balance_Notes += " باقي للفاتورة : " & (Pure - Convert.ToDouble(Piedmoney_txt.Text)).ToString() & " , "
            If AG_Show_Balance_CB.Checked = True And AG_ID <> Default_AG_ID Then
                If Convert.ToDouble(BALANCE) < 0 Then
                    Balance_Notes += " باقي للحساب : " & (BALANCE.ToString) & " , "
                ElseIf Convert.ToDouble(BALANCE) > 0 Then
                    Balance_Notes += " باقي على الحساب : " & (BALANCE.ToString) & " , "
                Else
                    Balance_Notes += " الرصيد : " & Convert.ToDouble(BALANCE.ToString) & " , "
                End If
            End If
            'If Show_Bill_Rest_CB.Checked = True And AG_ID <> Default_AG_ID Then Balance_Notes += " باقي للفاتورة : " & (Pure - Convert.ToDouble(Piedmoney_txt.Text)).ToString() & vbNewLine
        End If

    End Sub

    Public Function GET_AG_Balance()
        Return Show_AG_T_Balance(AG_ID)
    End Function


    Private Sub SBPauseBtn_Click(sender As Object, e As EventArgs) Handles SBPauseBtn.Click

        If isPause = True Then
            If isDepended = True Then
                Beep()
                If MessageBox.Show(" إلغاء تعليق الفاتورة " + Bill_ID_Txt.Text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) _
                 = Windows.Forms.DialogResult.OK Then
                    SB_Cancel_PauseBill()
                End If
            Else
                MsgBox("لا يمكن إلغاء تعليق فاتورة غير محفوظة", MsgBoxStyle.Exclamation, "")
            End If
        Else
            Beep()
            If MessageBox.Show(" تعليق الفاتورة " + Bill_ID_Txt.Text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) _
                 = Windows.Forms.DialogResult.OK Then
                SB_PauseBill()
            End If
        End If

    End Sub

    Private Sub SB_Cancel_PauseBill()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_Cancel_PauseBill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            ResetNewBill()
        End If
    End Sub

    Private Sub SB_PauseBill()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_PauseBill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            ResetNewBill()
        End If
    End Sub

    Private Sub New_butt_Click(sender As Object, e As EventArgs) Handles New_butt.Click
        If On_Update = True Then Edit_butt_Click(sender, e)
        ResetNewBill()
    End Sub

    Private Sub Load_PauseBills()
        Dim C As New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "SB_PauseBill_SelectList_2"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Pr_ID", Pr_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        Me.PauseCmb.DataSource = C.Dt
        Me.PauseCmb.ValueMember = "T_ID"
        Me.PauseCmb.DisplayMember = "Bill_Num"
        PauseCmb.SelectedIndex = -1

        If C.Dt.Rows.Count > 0 Then
            MoveToBill_Btn.Enabled = True
        Else
            MoveToBill_Btn.Enabled = False
        End If

    End Sub


    Private Sub MoveToBill_Btn_Click(sender As Object, e As EventArgs) Handles MoveToBill_Btn.Click
        If PauseCmb.SelectedIndex > -1 Then
            Me.Enabled = False
            ClearFields()
            T_ID = PauseCmb.SelectedValue
            SB_Contents_SELECT_Bill()
            Fill_Bill_Info()
            SelectStateBt()
            Me.Enabled = True
        End If
    End Sub

    Private Sub DeliveryingButton_Click(sender As Object, e As EventArgs) Handles DeliveryingButton.Click
        If isDepended = True Then
            FormType = 1
            AG_Type = 3
            F_Receipt = New Receipt
            Receipt_Tran_ID = T_ID

            With F_Receipt
                Rct_Tr_ID = SB_TR_ID
                .Fields_Panel.Enabled = True
                .AG_Cm.Enabled = False
                .Barcode_SH_txt.Enabled = False
                .Receipt_Title_combobox.Text = "فاتورة مبيعات : " + Bill_ID_Txt.Text
                .AG_ID = AG_ID
                .money_num_txtb.Text = Pure - Convert.ToDouble(Piedmoney_txt.Text)
            End With

            isShowing_Trans = False
            F_Receipt.ShowDialog()
            AG_Label.Text = "رصيد الحســاب: ( " & GET_AG_Balance().ToString & " ) "
        Else
            MsgBox("يجب إعتماد الفاتورة أولا", MsgBoxStyle.Exclamation, "")
        End If

    End Sub

    'Private Sub ST_cm_SelectedValueChanged(sender As Object, e As EventArgs)
    '    If Get_Unit = True Then
    '        Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
    '        IM_Fetch_QTY()
    '        If Valid_Panel.Visible = True Then Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
    '        If Get_Unit = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
    '    End If
    'End Sub

    'Private Sub Valid_cm_SelectedValueChanged(sender As Object, e As EventArgs)
    '    If Get_Unit = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
    'End Sub


    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        If T_ID > 0 Then
            If On_Update = False Then

                Beep()
                If MessageBox.Show(" سيتم تعديل الفاتورة بشكل مباشر مع كل تغير ... تأكيد التعديل ؟ ", "تعديل فاتورة", MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    Edit_butt.BackColor = Color.GreenYellow
                    On_Update = True
                    AGMetroGrid.BackgroundColor = Color.LightYellow
                    AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
                    ADDCatButton.Enabled = True
                    RemoveCatButton.Enabled = True
                    Notes_txt.Enabled = True
                    DateTimeEx.Enabled = True
                    Project_cm.Enabled = True
                    Show_AG_Projects_btn.Enabled = True
                    DiscountPanel.Enabled = True
                    AG_Cm.Enabled = True
                    'Show_IM_btn2.Enabled = True
                    Markter_Cm.Enabled = True
                    Ebable_CatFields()
                    Edit_butt.Text = "إيقاف التعديل"
                End If

            Else
                Save_Total(T_ID, TOTAL, Disc)
                Save_About(T_ID, Notes_txt.Text)
                Save_Date(T_ID, DateTimeEx)
                Save_Pro()
                Begin_Discount()
                AG_Label.Text = "رصيد الحســاب: ( " & GET_AG_Balance().ToString & " ) "
                On_Update = False
                Edit_butt.Text = EditState
                Edit_butt.BackColor = Color.White
                Notes_txt.Enabled = False
                DateTimeEx.Enabled = False
                Project_cm.Enabled = False
                Show_AG_Projects_btn.Enabled = False
                DiscountPanel.Enabled = False

                AG_Cm.Enabled = True
                'Show_IM_btn2.Enabled = True
                Markter_Cm.Enabled = False

                SelectStateBt()
                Select_Sales_Receipt(T_ID)

            End If
        End If
    End Sub

    Sub Close_Sale()
        Update_Discount(T_ID, Discount_txt.Text)
        Network_Edit_Tracker_insert(" تخفيض للفاتورة بقيمة:" & Disc.ToString, Bill_ID_Txt.Text, 1, 3)
    End Sub



    'Private Sub Valid_cm_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return
    '            QtyTextBox.Select()
    '            ADD_IM()
    '        Case Keys.Right : QtyTextBox.Select()
    '    End Select
    'End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles DGV_Control_btn.Click
        FormType = 1
        Switch_To_DV_Show()
    End Sub

    Private Sub تعديلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تعديلToolStripMenuItem.Click
        Mov_To_Edit_IM()
    End Sub

    Private Sub عرضالتكلفةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضالتكلفةToolStripMenuItem.Click
        Show_IM_Cost(True, F_Sales.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value, F_Sales.AGMetroGrid.CurrentRow.Cells("U_ID_CL").Value)
    End Sub




    'Private Sub Sh_ByNum_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    '    IMDataGridViewX.Columns("IM_NUM_CL").Visible = Sh_ByNum_CB.Checked
    '    Barcode_SH_txt.Select()


    'End Sub

    Private Sub Search_By_Bar_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Search_By_Bar_CB.CheckedChanged
        CB_CHecked(sender)
        Bill_ID_Txt.Clear()
        Bill_ID_Txt.Select()
    End Sub

    Private Sub OpenCahDR_Btn_Click(sender As Object, e As EventArgs) Handles OpenCahDR_Btn.Click
        Open_Cash_Drawer()
    End Sub

    Private Sub Show_Cash_btn_Click(sender As Object, e As EventArgs) Handles Show_Cash_btn.Click
        Fetch_Pr_Details_()
    End Sub



    'Private Sub ALL_QTY_txt_TextChanged(sender As Object, e As EventArgs)
    '    If Not String.IsNullOrWhiteSpace(ALL_QTY_txt.Text) Then
    '        Dim N As Double = ALL_QTY_txt.Text
    '        ALL_QTY_txt.Text = N.ToString("N")
    '    End If
    'End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub


    Private Sub إضافةكعميلجديدToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إضافةكعميلجديدToolStripMenuItem.Click
        ADD_Fast_AG()
    End Sub

    Private Sub كشفحسابالعميلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles كشفحسابالعميلToolStripMenuItem.Click
        Show_AG_Balance()
    End Sub

    Private Sub عرضرصيدالعميلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضرصيدالعميلToolStripMenuItem.Click
        MsgBox(Show_AG_T_Balance(AG_ID).ToString(), MsgBoxStyle.Information, "رصيد العميل : " & AG_Cm.Textt)
    End Sub


    Private Sub إرسالالفاتورةللبريدالإلكترونيToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إرسالالفاتورةللبريدالإلكترونيToolStripMenuItem.Click
        Type_Of_E_mail = 1
        Me.Cursor = Cursors.AppStarting
        SendingOptions.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Show_IM_Rtn_btn_Click(sender As Object, e As EventArgs) Handles Show_IM_Rtn_btn.Click
        Show_IM_Rtn.ShowDialog()
    End Sub


    Private Sub Calc_Dicount_Btn_Click(sender As Object, e As EventArgs) Handles Calc_Dicount_Btn.Click
        Begin_Discount()
    End Sub


    Public Sub Discount_calc()
        Disc = Convert.ToDouble(Discount_txt.Text)
        Pure_txt.Text = TOTAL - Disc
        Pure = TOTAL - Disc
    End Sub

    Private Sub Begin_Discount()

        If String.IsNullOrWhiteSpace(Discount_txt.Text) Then Discount_txt.Text = "0"

        Discount_calc()

        Close_Sale()

    End Sub

    Private Sub طباعةورقةA5ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles طباعةورقةA5ToolStripMenuItem.Click
        CashPrint("\reports\Costmer_SB_A5.rpt", 3)
    End Sub

    Private Sub طباعةورقA4ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles طباعةورقA4ToolStripMenuItem.Click
        CashPrint("\reports\Costmer_SB_A4.rpt", 1)
    End Sub

    Private Sub طباعةورقةA4ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles طباعةورقةA4ToolStripMenuItem.Click
        CashPrint("\reports\Costmer_SB_A4_2.rpt", 4)
    End Sub

    'Private Sub Barcode_SH_txt_TextChanged(sender As Object, e As EventArgs)
    '    If Sh_ByNum_CB.Checked = True And Barcode_SH_txt.Text.Count > 0 Then
    '        Load_IMByNum()
    '    Else
    '        IMDataGridViewX.Visible = False
    '    End If
    'End Sub

    'Private Sub Barcode_SH_txt_Enter(sender As Object, e As EventArgs)
    '    If Sh_ByNum_CB.Checked = True Then IMDataGridViewX.Columns("IM_NUM_CL").Visible = True
    'End Sub

    'Public Sub Load_IMByNum()
    '    Dim c As New C

    '    Try
    '        IM_Dt.Clear()
    '        Dim s As String
    '        s = "select IM_ID,item_name,isValid,IM_NUM from IM_All_V WHERE IM_NUM Like '%" & Barcode_SH_txt.Text & "%' Order by item_name ASC"
    '        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
    '        c.Da.Fill(IM_Dt)
    '        IMDataGridViewX.DataSource = IM_Dt
    '        If IM_Dt.Rows.Count > 0 Then
    '            IMDataGridViewX.Visible = True
    '            IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
    '        Else
    '            IMDataGridViewX.Visible = False
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub ورقةA4تصميمجاهزToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ورقةA4تصميمجاهزToolStripMenuItem.Click
        CashPrint("\reports\Costmer_SB_A4_3.rpt", 5)
    End Sub


    Private Sub طباعةإذنصـــرفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles طباعةإذنصـــرفToolStripMenuItem.Click
        is_Order_IM_Print = True
        CashPrint("\reports\Costmer_SB_Resive_Order_A5.rpt", 5)
        is_Order_IM_Print = False
    End Sub

    Private Sub Project_cm_KeyDown(sender As Object, e As KeyEventArgs) Handles Project_cm.KeyDown
        If e.KeyCode = Keys.Return Then Save_Pro()
    End Sub

    Private Sub Show_AG_Projects_btn_Click(sender As Object, e As EventArgs) Handles Show_AG_Projects_btn.Click
        AG_Projects.ShowDialog()
    End Sub

    Private Sub ReceiptsMetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles ReceiptsMetroGrid.RowsAdded
        Calc_Credit()
    End Sub

    Private Sub Calc_Credit()
        Dim Sum As Double = 0
        For i = 0 To ReceiptsMetroGrid.Rows.Count - 1
            Sum = Sum + ReceiptsMetroGrid.Rows(i).Cells("Value_CL").Value
        Next
        Piedmoney_txt.Text = Sum.ToString("n")
    End Sub

    Private Sub ReceiptsMetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles ReceiptsMetroGrid.RowsRemoved
        Calc_Credit()
    End Sub

    Private Sub ReceiptsMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ReceiptsMetroGrid.MouseDoubleClick
        If ReceiptsMetroGrid.Rows.Count > 0 Then
            AG_Type = 3
            IS_Show_Rctp = True
            F_Receipt = New Receipt
            F_Receipt.ShowDialog()
            IS_Show_Rctp = False
        End If
    End Sub


    Private Sub عرضفواتيرالزبونToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضفواتيرالزبونToolStripMenuItem.Click
        PchSearch.ShowDialog()
    End Sub

    Private Sub AG_Show_Balance_CB_CheckedChanged(sender As Object, e As EventArgs) Handles AG_Show_Balance_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.SB_AG_Show_Balance = AG_Show_Balance_CB.Checked
        Save_AppSetting()
    End Sub

    'Private Sub Bercent_TXT_TextChanged(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
    'End Sub

    'Private Sub Bercent_TXT_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float_With_Negitave(sender, e)
    'End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If IM_ID > 0 Then
            Show_IM_Details.IM_ID = IM_ID
            Show_IM_Details.ShowDialog()
        End If
    End Sub

    'Private Sub ST_Bercent_Panel_VisibleChanged(sender As Object, e As EventArgs)
    '    If ST_Bercent_Panel.Visible = False Then Bercent_TXT.Clear()
    'End Sub

    Private Sub طباعةورقةA4تصميمجاهز2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles طباعةورقةA4تصميمجاهز2ToolStripMenuItem.Click
        CashPrint("\reports\Costmer_SB_A4_4.rpt", 5)
    End Sub


    Private Sub DateTimeEx_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimeEx.KeyDown
        If e.KeyCode = Keys.Return Then Save_Date(T_ID, DateTimeEx)
    End Sub

    Private Sub AG_Cm_ID_Changed(sender As Object, e As EventArgs) Handles AG_Cm.ID_Changed
        If AG_Cm.TXT_ID.Text > 0 Then
            Fetch_ItemToList2()
            AG_Label.Text = "رصيد الحســاب: ( " & GET_AG_Balance().ToString & " ) "
        End If
    End Sub


    'Private Sub AG_Grid_VisibleChanged(sender As Object, e As EventArgs)
    '    If AG_Grid.Visible = True Then

    '        Me.Controls.Add(AG_Grid)
    '        AG_Grid.BringToFront()
    '        AG_Grid.Location = New Point(AG_Panel.Location.X, AG_Panel.Location.Y + AG_Panel.Size.Height + 1)
    '    Else
    '        AG_Panel.Controls.Add(AG_Grid)
    '        AG_Grid.Location = New Point(AG_SH_txt.Location.X, AG_SH_txt.Location.Y + AG_SH_txt.Size.Height + 1)
    '    End If
    'End Sub


    'Private Sub IMDataGridViewX_VisibleChanged(sender As Object, e As EventArgs)
    '    If IMDataGridViewX.Visible = True Then

    '        Me.Controls.Add(IMDataGridViewX)
    '        IMDataGridViewX.BringToFront()
    '        IMDataGridViewX.Location = New Point(IM_Panel.Location.X, IM_Panel.Location.Y + IM_Panel.Size.Height + 1)
    '    Else
    '        IM_Panel.Controls.Add(IMDataGridViewX)
    '        IMDataGridViewX.Location = New Point(IM_SH_txt.Location.X, IM_SH_txt.Location.Y + IM_SH_txt.Size.Height + 1)
    '    End If
    'End Sub


    Private Sub Show_Bill_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Show_Bill_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.SB_Show_Bill = Show_Bill_CB.Checked
        Save_AppSetting()
    End Sub

    'Private Sub Min_SP_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    '    If Min_SP_CB.Checked = True Then Min_SP_2_CB.Checked = False
    '    Price_Text_Check_Read()
    '    IM_Fetch_QTY()
    'End Sub

    'Private Sub Min_SP_2_CBCheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    '    If Min_SP_2_CB.Checked = True Then Min_SP_CB.Checked = False
    '    Price_Text_Check_Read()
    '    IM_Fetch_QTY()
    'End Sub

    'Private Sub Price_Text_Check_Read()
    '    If Min_SP_CB.Checked = True Or Min_SP_2_CB.Checked = True Then
    '        PriceTextBox.ReadOnly = True
    '    Else
    '        If U_SB_IM_Update = True Then
    '            PriceTextBox.ReadOnly = False
    '            Bercent_TXT.ReadOnly = False
    '        Else
    '            PriceTextBox.ReadOnly = True
    '            Bercent_TXT.ReadOnly = True
    '        End If
    '    End If
    'End Sub

    Private Sub طباعةورقA4ملاحظاتالصنفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles طباعةورقA4ملاحظاتالصنفToolStripMenuItem.Click
        CashPrint("\reports\Costmer_SB_With_Notes_A4.rpt", 7)
    End Sub

    Private Sub Show_Billl_Rest_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Show_Bill_Rest_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.SB_Show_Bill_Rest = Show_Bill_Rest_CB.Checked
        Save_AppSetting()
    End Sub

    Private Sub Clear_Check_btn_Click(sender As Object, e As EventArgs) Handles Clear_Check_btn.Click
        SB_Contents_Uncheck_IM()
    End Sub


    Private Sub SB_Contents_Uncheck_IM()

        Dim C As New C

        For Each row As DataGridViewRow In AGMetroGrid.Rows
            If row.Cells("Check_CL").Value = True Then
                C = New C
                With (C.Com)
                    .Connection = C.Con
                    .CommandText = "SB_Contents_Uncheck_IM"
                    .CommandType = CommandType.StoredProcedure
                    .Parameters.AddWithValue("@T_ID", row.Cells("T_ID_CL").Value)
                End With
                SQL_SP_EXEC(C.Com)

            End If
        Next

        SB_Contents_SELECT_Bill()

    End Sub

    Private Sub AGMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles AGMetroGrid.MouseDoubleClick
        Mov_To_Edit_IM()
    End Sub

    Private Sub Mov_To_Edit_IM()
        FormType = 1
        If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And AGMetroGrid.Rows.Count > 0 Then Change_IM_Details.ShowDialog()
    End Sub

    Private Sub AGMetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles AGMetroGrid.RowsAdded
        ' 'Calc_Total()
    End Sub

    Private Sub AGMetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles AGMetroGrid.RowsRemoved
        '   'Calc_Total()
    End Sub
    Private Sub AGMetroGrid_KeyDown(sender As Object, e As KeyEventArgs) Handles AGMetroGrid.KeyDown

        If e.KeyCode = Keys.Delete Then
            If AGMetroGrid.Rows.Count > 0 And AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
                If MessageBox.Show(" حذف الصنف " + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                    SB_Contents_Delete_IM(AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
                End If
            End If
        End If

        '    If e.KeyCode = Keys.Up Then If AGMetroGrid.Rows.Count > 0 Then If AGMetroGrid.CurrentRow.Index = 0 Then QtyTextBox.Select()

    End Sub

    Private Sub عرضربحالفاتورةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضربحالفاتورةToolStripMenuItem.Click
        Bill_Perfet_Select_For_Bill(T_ID)
    End Sub

    Private Sub Show_SumPied_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Show_SumPied_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.SB_Show_SumPied = Show_SumPied_CB.Checked
        Save_AppSetting()
    End Sub

    'Private Sub IM_SH_txt_MouseDoubleClick(sender As Object, e As MouseEventArgs)
    '    Items_Search.ShowDialog()
    '    If GLOBAL_IM_ID > 0 Then
    '        Load_IM_By_ID(IMDataGridViewX)
    '        For i = 0 To IMDataGridViewX.Rows.Count - 1
    '            If IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value = GLOBAL_IM_ID Then
    '                Exit For
    '            End If
    '        Next
    '        Fetch_ItemToList()
    '    End If
    'End Sub


    Private Sub AG_SH_txt_MouseDoubleClick(sender As Object, e As MouseEventArgs)
        F_AgentsMenu = New AgentsMenu
        ' Set_Form(F_AgentsMenu, IMPanel)
        F_AgentsMenu.ShowDialog()
        F_AgentsMenu.Barcode_SH_txt.Select()
    End Sub

    Private Sub Markter_Cm_ID_Changed(sender As Object, e As EventArgs) Handles Markter_Cm.ID_Changed
        AG_Balance_Update_Marketer(T_ID, Markter_Cm.TXT_ID.Text)
    End Sub

    Private Sub تخفيضبنسبةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تخفيضبنسبةToolStripMenuItem.Click
        Dim F_Percent_Disc As New Percent_Disc
        F_Percent_Disc.T_ID = T_ID
        F_Percent_Disc.TOTAL = TOTAL
        F_Percent_Disc.ShowDialog()
        Fill_Bill_Info()
        'F_Sales.Discount_txt.Text = (TOTAL * (Discount_txt.Text / 100))
        'F_Sales.Discount_calc()
    End Sub

    Private Sub عرضأخرمبيعاتللصنفبالنسبةللزبونToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضأخرمبيعاتللصنفبالنسبةللزبونToolStripMenuItem.Click
        Show_AG_IM_SALES.ShowDialog()
    End Sub

    Private Sub VoidLb_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles VoidLb.MouseDoubleClick

        If U_SalesVoid = True Then

            Beep()
            If MessageBox.Show(" سيتم تراجع عن إلغاء الفاتورة رقم " + Bill_ID_Txt.Text + " وكل المعاملات الخاصة بها ... متأكد ", "تاكيد العملية", MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
                AG_Balance_UN_Void_Row(T_ID, SB_ID, 1)
                Get_T_ID()
            End If

        End If
    End Sub

    Private Sub Pure_txt_TextChanged(sender As Object, e As EventArgs) Handles Pure_txt.TextChanged
        If is_Use_Total_Port = True Then Show_Total_Port(Pure)
    End Sub

    Private Sub AGMetroGrid_DataSourceChanged(sender As Object, e As EventArgs) Handles AGMetroGrid.DataSourceChanged
        Calc_Total()
    End Sub

    Private Sub إيجارالصنفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إيجارالصنفToolStripMenuItem.Click
        If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And AGMetroGrid.Rows.Count > 0 Then
            Dim F As New Rsv_IM
            F.ShowDialog()
        End If
       
    End Sub

    Private Sub طباعةإذنصـــرفA4ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles طباعةإذنصـــرفA4ToolStripMenuItem.Click
        is_Order_IM_Print = True
        CashPrint("\reports\Costmer_SB_Resive_Order_A4.rpt", 5)
        is_Order_IM_Print = False
    End Sub

    'Private Sub Button1_Click_1(sender As Object, e As EventArgs)
    '    query("UPDATE Agents_Balance_MV SET Travel_ID = NULL WHERE T_ID = " & T_ID)
    '    OUT_SALE_Bill_TXT.Clear()
    'End Sub

    'Private Sub OUT_SALE_Bill_TXT_MouseDoubleClick(sender As Object, e As MouseEventArgs)
    '    SB_LINK_WITH_OUT_SALE.ShowDialog()
    'End Sub

    Private Sub طباعةالتسليموالإستلامToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles طباعةالتسليموالإستلامToolStripMenuItem.Click
        CashPrint("\reports\Costmer_SB_A5_IM_IN_OUT_QTY.rpt", 18)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        CashPrint("\reports\Costmer_SB_A5_IM_IN_OUT_QTY.rpt", 18)
    End Sub

    Private Sub تحديدنوعالطباعـــةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تحديدنوعالطباعـــةToolStripMenuItem.Click
        Dim dialog As New SingleChoiceDialog()
        dialog.Text = "حدد نوع الطباعة"
        ' أضف الخيارات إلى ListBox
        'dialog.ListBox1.Items.AddRange(New String() {"Option 1", "Option 2", "Option 3"})

        Dim c2 As New C
        c2.Str = "select ID,Type from Sales_Bill_Page"
        c2.Da = New SqlClient.SqlDataAdapter(c2.Str, c2.Con)
        c2.Da.Fill(c2.Dt)
        dialog.ListBox1.DataSource = c2.Dt
        dialog.ListBox1.DisplayMember = "Type"
        dialog.ListBox1.ValueMember = "ID"
        dialog.ListBox1.SelectedValue = Sales_Page_ID


        ' عرض مربع الحوار
        If dialog.ShowDialog() = DialogResult.OK Then
            CashPrint(get_prt_path(dialog.ListBox1.SelectedValue), dialog.ListBox1.SelectedValue)

            '    ' عرض الخيار الذي اختاره المستخدم
            '    MessageBox.Show("تم اختيار: " & dialog.SelectedItem)
            'Else
            '    MessageBox.Show("لم يتم اختيار أي خيار.")
        End If
    End Sub


    Public Function get_prt_path(Sales_Page_ID As Integer)


        Dim C As New C
        Dim S As String = ""
        Dim result As String = ""


        S = "Select AG_Bill From Sales_Bill_Page Where ID = " & Sales_Page_ID


        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                result = C.Dr("AG_Bill")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

        Return result
    End Function

    Private Sub جدولحجزالخدمةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles جدولحجزالخدمةToolStripMenuItem.Click
        CalendarForm.IM_T_ID = Me.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value
        CalendarForm.IM_NAME = Me.AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value
        CalendarForm.IM_ID = Me.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value
        CalendarForm.ShowDialog()
    End Sub

    Private Sub إدراجموظفللخدمةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إدراجموظفللخدمةToolStripMenuItem.Click
        If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And AGMetroGrid.Rows.Count > 0 Then
            Dim F As New SB_Contents_AGENTS

            F.SB_T_ID = Me.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value
            F.IM_NAME = Me.AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value

            F.ShowDialog()
        End If
    End Sub

    Private Sub PriceTextBox_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub PriceTextBox_KeyDown(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub PriceTextBox_KeyPress(sender As Object, e As KeyPressEventArgs)

    End Sub

    Private Sub QtyTextBox_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub QtyTextBox_KeyDown(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub QtyTextBox_KeyPress(sender As Object, e As KeyPressEventArgs)

    End Sub
End Class