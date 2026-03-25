Public Class DGV_Control


    Dim ST_Case As Integer = 0 ' 1:St Explorer , 2:IM_Valid , 3 ITEMS_PRICE
    Private Sub DGV_Control_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub DGV_Control_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        LAST_PCH_CB.Visible = U_SB_Show_IM_COST

        If FormType = 0 Then
            'If F_STORES_Explorer.ParentForm = True Then

            If Application.OpenForms().OfType(Of STORES_Explorer).Any Then
                ST_Case = 1
                Handle_With_St_Tab()

                'ElseIf F_STORES_Explorer.IsHandleCreated = True Then
            ElseIf Application.OpenForms().OfType(Of IM_Valid).Any Then
                ST_Case = 2
                If LAST_PCH_CB.Visible = True Then LAST_PCH_CB.Enabled = False
                VALID_CB.Enabled = False
                GM_CB.Enabled = False
                Handle_With_St_Tab()
                'ElseIf Items_Prices.IsHandleCreated = True Then
            ElseIf Application.OpenForms().OfType(Of Items_Prices).Any Or Application.OpenForms().OfType(Of Price_Less_Than_Cost).Any Then
                ST_Case = 3
                IMPR_MINPR_CB.Visible = S_Allow_MinSP
                IMPR_MINPR_2_CB.Visible = S_Allow_MinSP
                Handle_With_ITEMS_PRICES_Tab()
            End If

        Else

            Handle_With_Other_Tab()
        End If

        Serial_Code_CB.Visible = S_SerialCode
    End Sub

    Private Sub Handle_With_ITEMS_PRICES_Tab()
        IMPR_IMNUM_CB.Checked = My_Settings.IMPR_IMNUM
        IMPR_BAR_CB.Checked = My_Settings.IMPR_BAR
        IMPR_MINPR_CB.Checked = My_Settings.IMPR_MINSP
        IMPR_MINPR_2_CB.Checked = My_Settings.IMPR_MINSP_2
    End Sub

    Private Sub Handle_With_St_Tab()
        GM_CB.Checked = My_Settings.ST_GM_Name
        ST_STName_CB.Checked = My_Settings.ST_STNAME
        If LAST_PCH_CB.Visible = True Then LAST_PCH_CB.Checked = MY_Settings.ST_Last_Pch_Price
        VALID_CB.Checked = My_Settings.ST_Valid
        SHOW_IMNUM_CB.Checked = My_Settings.ST_IM_Num
    End Sub

    Private Sub Handle_With_Other_Tab()
        Select Case FormType
            Case 1
                If SB_is_Fast = False Then
                    If F_Sales.AGMetroGrid.Columns("Date_CL").Visible = True Then Date_cb.Checked = True
                    If F_Sales.AGMetroGrid.Columns("ST_Name_CL").Visible = True Then St_cb.Checked = True
                    If F_Sales.AGMetroGrid.Columns("D_Valid_CL").Visible = True Then D_Valid_cb.Checked = True
                    If F_Sales.AGMetroGrid.Columns("IMUnit_CL").Visible = True Then Unit_cb.Checked = True
                    If F_Sales.AGMetroGrid.Columns("Price_CL").Visible = True Then Price_cb.Checked = True
                    If F_Sales.AGMetroGrid.Columns("Total_CL").Visible = True Then Total_cb.Checked = True
                    If F_Sales.AGMetroGrid.Columns("Notes_CL").Visible = True Then Notes_cb.Checked = True
                    If F_Sales.AGMetroGrid.Columns("IMNUM_CL").Visible = True Then IMNUM_cb.Checked = True
                    If F_Sales.AGMetroGrid.Columns("Barcode_CL").Visible = True Then Barcode_CB.Checked = True
                    If F_Sales.AGMetroGrid.Columns("IM_NOTE_CL").Visible = True Then IM_NOTE_CB.Checked = True
                    If F_Sales.AGMetroGrid.Columns("IM_Discount_CL").Visible = True Then IM_Discount_CB.Checked = True


                Else
                    If Sales_Fast.AGMetroGrid.Columns("Date_CL").Visible = True Then Date_cb.Checked = True
                    If Sales_Fast.AGMetroGrid.Columns("ST_Name_CL").Visible = True Then St_cb.Checked = True
                    If Sales_Fast.AGMetroGrid.Columns("D_Valid_CL").Visible = True Then D_Valid_cb.Checked = True
                    If Sales_Fast.AGMetroGrid.Columns("IMUnit_CL").Visible = True Then Unit_cb.Checked = True
                    If Sales_Fast.AGMetroGrid.Columns("Price_CL").Visible = True Then Price_cb.Checked = True
                    If Sales_Fast.AGMetroGrid.Columns("Total_CL").Visible = True Then Total_cb.Checked = True
                    If Sales_Fast.AGMetroGrid.Columns("Notes_CL").Visible = True Then Notes_cb.Checked = True
                    If Sales_Fast.AGMetroGrid.Columns("IMNUM_CL").Visible = True Then IMNUM_cb.Checked = True
                    If Sales_Fast.AGMetroGrid.Columns("Barcode_CL").Visible = True Then Barcode_CB.Checked = True
                    Serial_Code_CB.Enabled = False
                End If

                OpenNextBill_CB.Visible = True
                B_CodeAdd_1_CB.Visible = True
                'S_Default_Panel.Visible = True
                Notes_cb.Visible = True
                OpenNextBill_CB.Checked = My_Settings.S_OpenNextBill
                B_CodeAdd_1_CB.Checked = My_Settings.S_CodeAdd_1

                SB_Sch_With_QTY_CB.Checked = MY_Settings.SB_Sch_With_QTY
            Case 2
                Date_cb.Visible = False
                If F_Pch.AGMetroGrid.Columns("ST_Name_CL").Visible = True Then St_cb.Checked = True
                If F_Pch.AGMetroGrid.Columns("D_Valid_CL").Visible = True Then D_Valid_cb.Checked = True
                If F_Pch.AGMetroGrid.Columns("IMUnit_CL").Visible = True Then Unit_cb.Checked = True
                If F_Pch.AGMetroGrid.Columns("Price_CL").Visible = True Then Price_cb.Checked = True
                If F_Pch.AGMetroGrid.Columns("Total_CL").Visible = True Then Total_cb.Checked = True
                If F_Pch.AGMetroGrid.Columns("Notes_CL").Visible = True Then Notes_cb.Checked = True
                If F_Pch.AGMetroGrid.Columns("IMNUM_CL").Visible = True Then IMNUM_cb.Checked = True
                If F_Pch.AGMetroGrid.Columns("Barcode_CL").Visible = True Then Barcode_CB.Checked = True
                Notes_cb.Visible = True
            Case 3
                Date_cb.Visible = False
                If F_IM_Execute.AGMetroGrid.Columns("ST_Name_CL").Visible = True Then St_cb.Checked = True
                If F_IM_Execute.AGMetroGrid.Columns("D_Valid_CL").Visible = True Then D_Valid_cb.Checked = True
                If F_IM_Execute.AGMetroGrid.Columns("IMUnit_CL").Visible = True Then Unit_cb.Checked = True
                If F_IM_Execute.AGMetroGrid.Columns("Price_CL").Visible = True Then Price_cb.Checked = True
                If F_IM_Execute.AGMetroGrid.Columns("Total_CL").Visible = True Then Total_cb.Checked = True
                If F_IM_Execute.AGMetroGrid.Columns("IMNUM_CL").Visible = True Then IMNUM_cb.Checked = True
                SB_Sch_With_QTY_CB.Checked = MY_Settings.SB_Sch_With_QTY
            Case 4
                Date_cb.Visible = False
                If F_Invoice.AGMetroGrid.Columns("ST_Name_CL").Visible = True Then St_cb.Checked = True
                If F_Invoice.AGMetroGrid.Columns("D_Valid_CL").Visible = True Then D_Valid_cb.Checked = True
                If F_Invoice.AGMetroGrid.Columns("IMUnit_CL").Visible = True Then Unit_cb.Checked = True
                If F_Invoice.AGMetroGrid.Columns("Price_CL").Visible = True Then Price_cb.Checked = True
                If F_Invoice.AGMetroGrid.Columns("Total_CL").Visible = True Then Total_cb.Checked = True
                If F_Invoice.AGMetroGrid.Columns("IMNUM_CL").Visible = True Then IMNUM_cb.Checked = True
                If F_Invoice.AGMetroGrid.Columns("Notes_CL").Visible = True Then Notes_cb.Checked = True
                If F_Invoice.AGMetroGrid.Columns("Barcode_CL").Visible = True Then Barcode_CB.Checked = True
                Notes_cb.Visible = True
            Case 5, 6
                If Returns.MetroGrid1.Columns("DATE_CL").Visible = True Then Date_cb.Checked = True
                If Returns.BillMetroGrid.Columns("ST_Name_CL_2").Visible = True Then St_cb.Checked = True
                If Returns.BillMetroGrid.Columns("D_Valid_CL_2").Visible = True Then D_Valid_cb.Checked = True
                If Returns.BillMetroGrid.Columns("IMUnit_CL_2").Visible = True Then Unit_cb.Checked = True
                If Returns.BillMetroGrid.Columns("Price_CL_2").Visible = True Then Price_cb.Checked = True
                If Returns.BillMetroGrid.Columns("Total_CL_2").Visible = True Then Total_cb.Checked = True
                If Returns.BillMetroGrid.Columns("IMNUM_CL_2").Visible = True Then IMNUM_cb.Checked = True
                If FormType = 5 Then If Returns.MetroGrid1.Columns("Serial_Code_CL").Visible = True Then Serial_Code_CB.Checked = True
                ' If Returns.MetroGrid1.Columns("Proj_NAME_CL").Visible = True Then Proj_CB.Checked = True
            Case 7
                Date_cb.Visible = False
                St_cb.Visible = False
                If F_Stores_ImmediateOrder.AGMetroGrid.Columns("D_Valid_CL").Visible = True Then D_Valid_cb.Checked = True
                If F_Stores_ImmediateOrder.AGMetroGrid.Columns("IMUnit_CL").Visible = True Then Unit_cb.Checked = True
                If F_Stores_ImmediateOrder.AGMetroGrid.Columns("Price_CL").Visible = True Then Price_cb.Checked = True
                If F_Stores_ImmediateOrder.AGMetroGrid.Columns("Total_CL").Visible = True Then Total_cb.Checked = True
                If F_Stores_ImmediateOrder.AGMetroGrid.Columns("IMNUM_CL").Visible = True Then IMNUM_cb.Checked = True

            Case 10

                If F_ViewBill.AGMetroGrid.Columns("Date_CL").Visible = True Then Date_cb.Checked = True
                If F_ViewBill.AGMetroGrid.Columns("ST_Name_CL").Visible = True Then St_cb.Checked = True
                If F_ViewBill.AGMetroGrid.Columns("D_Valid_CL").Visible = True Then D_Valid_cb.Checked = True
                If F_ViewBill.AGMetroGrid.Columns("IMUnit_CL").Visible = True Then Unit_cb.Checked = True
                If F_ViewBill.AGMetroGrid.Columns("Price_CL").Visible = True Then Price_cb.Checked = True
                If F_ViewBill.AGMetroGrid.Columns("Total_CL").Visible = True Then Total_cb.Checked = True
                If F_ViewBill.AGMetroGrid.Columns("Notes_CL").Visible = True Then Notes_cb.Checked = True
                If F_ViewBill.AGMetroGrid.Columns("IMNUM_CL").Visible = True Then IMNUM_cb.Checked = True
                'If F_ViewBill.AGMetroGrid.Columns("Barcode_CL").Visible = True Then Barcode_CB.Checked = True

            Case 11
                Date_cb.Visible = False
                If F_Inside_Sales.AGMetroGrid.Columns("ST_Name_CL").Visible = True Then St_cb.Checked = True
                If F_Inside_Sales.AGMetroGrid.Columns("D_Valid_CL").Visible = True Then D_Valid_cb.Checked = True
                If F_Inside_Sales.AGMetroGrid.Columns("IMUnit_CL").Visible = True Then Unit_cb.Checked = True
                If F_Inside_Sales.AGMetroGrid.Columns("Price_CL").Visible = True Then Price_cb.Checked = True
                If F_Inside_Sales.AGMetroGrid.Columns("Total_CL").Visible = True Then Total_cb.Checked = True
                If F_Inside_Sales.AGMetroGrid.Columns("IMNUM_CL").Visible = True Then IMNUM_cb.Checked = True
                SB_Sch_With_QTY_CB.Checked = MY_Settings.SB_Sch_With_QTY
        End Select

        S_Deafult_cm.SelectedIndex = MY_Settings.S_Default
    End Sub

    Private Sub Back_Btn_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Select Case FormType
            Case 1
                If SB_is_Fast = False Then
                    F_Sales.Check_View_Control()
                Else
                    Sales_Fast.Check_View_Control()
                End If

            Case 2
                F_Pch.Check_View_Control()
            Case 3
                F_IM_Execute.Check_View_Control()
            Case 4
                F_Invoice.Check_View_Control()
            Case 5, 6
                Returns.Check_View_Control()
            Case 7
                F_Stores_ImmediateOrder.Check_View_Control()
            Case 10
                F_ViewBill.Check_View_Control()
            Case 11
                F_Inside_Sales.Check_View_Control()
        End Select
        Me.Close()
    End Sub

    Private Sub Date_cb_CheckedChanged(sender As Object, e As EventArgs) Handles Date_cb.CheckedChanged
        CB_CHecked(sender)
        My_Settings.S_Date_CL = sender.Checked


    End Sub

    Private Sub St_cb_CheckedChanged(sender As Object, e As EventArgs) Handles St_cb.CheckedChanged
        CB_CHecked(sender)
        My_Settings.S_ST_Name_CL = sender.Checked
        Save_AppSetting()
    End Sub

    Private Sub D_Valid_cb_CheckedChanged(sender As Object, e As EventArgs) Handles D_Valid_cb.CheckedChanged
        CB_CHecked(sender)
        My_Settings.S_D_Valid_CL = sender.Checked
        Save_AppSetting()
    End Sub

    Private Sub Unit_cb_CheckedChanged(sender As Object, e As EventArgs) Handles Unit_cb.CheckedChanged
        CB_CHecked(sender)
        My_Settings.S_IMUnit_CL = sender.Checked
        Save_AppSetting()
    End Sub

    Private Sub Price_cb_CheckedChanged(sender As Object, e As EventArgs) Handles Price_cb.CheckedChanged
        CB_CHecked(sender)
        My_Settings.S_Price_CL = sender.Checked
        Save_AppSetting()
    End Sub

    Private Sub Total_cb_CheckedChanged(sender As Object, e As EventArgs) Handles Total_cb.CheckedChanged
        CB_CHecked(sender)
        My_Settings.S_Total_CL = sender.Checked
        Save_AppSetting()
    End Sub

    Private Sub B_CodeAdd_1_CB_CheckedChanged(sender As Object, e As EventArgs) Handles B_CodeAdd_1_CB.CheckedChanged
        CB_CHecked(sender)
        My_Settings.S_CodeAdd_1 = sender.Checked
        Save_AppSetting()
    End Sub

    Private Sub OpenNextBill_CB_CheckedChanged(sender As Object, e As EventArgs) Handles OpenNextBill_CB.CheckedChanged
        CB_CHecked(sender)
        Select Case FormType
            Case 1
                My_Settings.S_OpenNextBill = sender.Checked
        End Select
        Save_AppSetting()
    End Sub

    Private Sub S_Deafult_cm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles S_Deafult_cm.SelectedIndexChanged
        'Select Case FormType
        '    Case 1
        MY_Settings.S_Default = S_Deafult_cm.SelectedIndex
        'End Select
        Save_AppSetting()
    End Sub


    Private Sub Notes_cb_CheckedChanged(sender As Object, e As EventArgs) Handles Notes_cb.CheckedChanged
        CB_CHecked(sender)
        My_Settings.SP_Notes_CL = sender.Checked
        Save_AppSetting()
    End Sub

    Private Sub IMNUM_cb_CheckedChanged(sender As Object, e As EventArgs) Handles IMNUM_cb.CheckedChanged
        CB_CHecked(sender)
        My_Settings.S_IMNUM_CL = sender.Checked
        Save_AppSetting()
    End Sub

    Private Sub Proj_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Proj_CB.CheckedChanged
        CB_CHecked(sender)
        My_Settings.S_Project_CL = sender.Checked
        Save_AppSetting()
    End Sub

    '-----------------------------------------------------------
    Private Sub GN_CB_CheckedChanged(sender As Object, e As EventArgs) Handles GM_CB.CheckedChanged
        CB_CHecked(sender)
        My_Settings.ST_GM_Name = sender.CHECKED
        Save_AppSetting()
        Check_View_Control()
    End Sub

    Private Sub SHOW_IMNUM_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SHOW_IMNUM_CB.CheckedChanged
        CB_CHecked(sender)
        My_Settings.ST_IM_Num = sender.CHECKED
        Save_AppSetting()
        Check_View_Control()
    End Sub

    Private Sub ST_STName_CB_CheckedChanged(sender As Object, e As EventArgs) Handles ST_STName_CB.CheckedChanged
        CB_CHecked(sender)
        My_Settings.ST_STNAME = sender.CHECKED
        Save_AppSetting()
        Check_View_Control()
    End Sub

    Private Sub LAST_PCH_CB_CheckedChanged(sender As Object, e As EventArgs) Handles LAST_PCH_CB.CheckedChanged
        CB_CHecked(sender)
        My_Settings.ST_Last_Pch_Price = sender.CHECKED
        Save_AppSetting()
        Check_View_Control()
    End Sub

    Private Sub VALID_CB_CheckedChanged(sender As Object, e As EventArgs) Handles VALID_CB.CheckedChanged
        CB_CHecked(sender)
        My_Settings.ST_Valid = sender.CHECKED
        Save_AppSetting()
        Check_View_Control()
    End Sub

    Private Sub Check_View_Control()

        If ST_Case = 1 Then
            'F_STORES_Explorer.DataGridViewX.Columns("GM_Name_CL").Visible = My_Settings.ST_GM_Name
            'F_STORES_Explorer.DataGridViewX.Columns("ST_Name_CL").Visible = My_Settings.ST_STNAME
            'If LAST_PCH_CB.Visible = True Then F_STORES_Explorer.DataGridViewX.Columns("Last_Pch_Price").Visible = MY_Settings.ST_Last_Pch_Price
            'F_STORES_Explorer.DataGridViewX.Columns("D_Valid_CL").Visible = My_Settings.ST_Valid
            'F_STORES_Explorer.DataGridViewX.Columns("IM_Num_CL").Visible = My_Settings.ST_IM_Num

            GM_CB.Checked = My_Settings.ST_GM_Name
            St_cb.Checked = My_Settings.ST_STNAME
            If LAST_PCH_CB.Visible = True Then LAST_PCH_CB.Checked = MY_Settings.ST_Last_Pch_Price
            VALID_CB.Checked = My_Settings.ST_Valid

            SHOW_IMNUM_CB.Checked = My_Settings.ST_IM_Num


        ElseIf ST_Case = 2 Then
            F_IM_Valid.DataGridViewX.Columns("ST_Name_CL").Visible = My_Settings.ST_STNAME
            F_IM_Valid.DataGridViewX.Columns("IM_Num_CL").Visible = My_Settings.ST_IM_Num

            GM_CB.Checked = My_Settings.ST_GM_Name
            St_cb.Checked = My_Settings.ST_STNAME
            If LAST_PCH_CB.Visible = True Then LAST_PCH_CB.Checked = MY_Settings.ST_Last_Pch_Price
            VALID_CB.Checked = My_Settings.ST_Valid
            SHOW_IMNUM_CB.Checked = My_Settings.ST_IM_Num


        ElseIf ST_Case = 3 Then
            Items_Prices.DataGridViewX.Columns("IM_Num_CL").Visible = My_Settings.IMPR_IMNUM
            Items_Prices.DataGridViewX.Columns("Barcode_CL").Visible = My_Settings.IMPR_BAR
            Items_Prices.DataGridViewX.Columns("Price_2_CL").Visible = My_Settings.IMPR_MINSP
            Items_Prices.DataGridViewX.Columns("Price_3_CL").Visible = My_Settings.IMPR_MINSP_2

            GM_CB.Checked = My_Settings.ST_GM_Name
            St_cb.Checked = My_Settings.ST_STNAME
            If LAST_PCH_CB.Visible = True Then LAST_PCH_CB.Checked = MY_Settings.ST_Last_Pch_Price
            VALID_CB.Checked = My_Settings.ST_Valid
            SHOW_IMNUM_CB.Checked = My_Settings.ST_IM_Num

        End If


    End Sub
    '-----------------------------------------------------------

    Private Sub Barcode_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Barcode_CB.CheckedChanged
        CB_CHecked(sender)
        My_Settings.S_Barcode_CL = sender.CHECKED
        Save_AppSetting
        Check_View_Control()
    End Sub

    Private Sub Serial_Code_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Serial_Code_CB.CheckedChanged
        CB_CHecked(sender)
        My_Settings.S_Serial_Code_CL = sender.CHECKED
        Save_AppSetting
        Check_View_Control()
    End Sub


    Private Sub IMPR_IMNUM_CB_CheckedChanged(sender As Object, e As EventArgs) Handles IMPR_IMNUM_CB.CheckedChanged
        CB_CHecked(sender)
        My_Settings.IMPR_IMNUM = sender.CHECKED
        Save_AppSetting
        Check_View_Control()
    End Sub

    Private Sub IMPR_BAR_CB_CheckedChanged(sender As Object, e As EventArgs) Handles IMPR_BAR_CB.CheckedChanged
        CB_CHecked(sender)
        My_Settings.IMPR_BAR = sender.CHECKED
        Save_AppSetting
        Check_View_Control()
    End Sub

    Private Sub IMPR_MINPR_CB_CheckedChanged(sender As Object, e As EventArgs) Handles IMPR_MINPR_CB.CheckedChanged
        CB_CHecked(sender)
        My_Settings.IMPR_MINSP = sender.CHECKED
        Save_AppSetting
        Check_View_Control()
    End Sub

    Private Sub IMPR_MINPR_2_CB_CheckedChanged(sender As Object, e As EventArgs) Handles IMPR_MINPR_2_CB.CheckedChanged
        CB_CHecked(sender)
        My_Settings.IMPR_MINSP_2 = sender.CHECKED
        Save_AppSetting
        Check_View_Control()
    End Sub

    Private Sub SB_Sch_With_QTY_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SB_Sch_With_QTY_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.SB_Sch_With_QTY = SB_Sch_With_QTY_CB.Checked
        Save_AppSetting()
    End Sub

    Private Sub IM_NOTE_CB_CheckedChanged(sender As Object, e As EventArgs) Handles IM_NOTE_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.S_IM_NOTE_CL = sender.CHECKED
        Save_AppSetting()
        Check_View_Control()
    End Sub

    Private Sub IM_Discount_CB_CheckedChanged(sender As Object, e As EventArgs) Handles IM_Discount_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.S_IM_Discount_CL = sender.Checked
        Save_AppSetting()
    End Sub
End Class