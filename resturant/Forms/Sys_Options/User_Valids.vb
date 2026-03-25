Imports System.Data.SqlClient

Public Class User_Valids
    Dim S_ID As Integer
    Dim S_Name As String
    Private Sub NewEmpButton_Click(sender As Object, e As EventArgs) Handles NewSButton.Click
        SaveSButton.Enabled = True
        EditSButton.Enabled = False
        DeleteSButton.Enabled = False
        SNameTextBox.Clear()
        SNameTextBox.Enabled = True
        SNameTextBox.Select()
        EditSButton.Text = "تعديل"
        Clear_ValidPanel()
        TabControl1.Enabled = True
    End Sub

    Private Sub SaveSButton_Click(sender As Object, e As EventArgs) Handles SaveSButton.Click
        If String.IsNullOrWhiteSpace(SNameTextBox.Text) = False Then Store_Insert()
    End Sub


    Private Sub Clear_ValidPanel()
        For Each gb As Control In TabControl1.Controls
            ' If TypeOf gb Is GroupBox And gb.Name <> "GroupBox1" Then
            For Each ch As CheckBox In gb.Controls
                '  DirectCast(ch, CheckBox).Checked = False
                ch.Checked = False
            Next
            ' End If
        Next

        SalesCB.Checked = False
        SalesDisCB.Checked = False
        SalesVoidCB.Checked = False
        Sales_EditCB.Checked = False
        SB_RtnCB.Checked = False
        End_Table_CB.Checked = False
        SB_IM_Update_CB.Checked = False
        SB_Sell_Under_Cost_CB.Checked = False
        SB_Show_Price_CB.Checked = False
        SB_Show_Cash_CB.Checked = False
        U_Sell_Under_Min_SP_CB.Checked = False
        Show_IM_Cost_CB.Checked = False
    End Sub

    Public Sub Store_Insert()
        Dim c As New C
        Dim sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
        Using (sqlCon)
            Dim sqlComm As New SqlClient.SqlCommand()
            sqlComm.Connection = sqlCon
            sqlComm.CommandText = "[User_Valid_Insert]"
            sqlComm.CommandType = CommandType.StoredProcedure

            sqlComm.Parameters.AddWithValue("@Valid_Name", SNameTextBox.Text)
            sqlComm.Parameters.AddWithValue("@Sales", SalesCB.Checked)
            sqlComm.Parameters.AddWithValue("@Sales_Dis", SalesDisCB.Checked)
            sqlComm.Parameters.AddWithValue("@SalesVoid", SalesVoidCB.Checked)
            sqlComm.Parameters.AddWithValue("@Pch", PchCB.Checked)
            sqlComm.Parameters.AddWithValue("@PchVoid", PchVoidCB.Checked)
            sqlComm.Parameters.AddWithValue("@Exp", ExpCB.Checked)
            sqlComm.Parameters.AddWithValue("@ExpVoid", ExpVoidCB.Checked)
            sqlComm.Parameters.AddWithValue("@StExplore", StExploreCB.Checked)
            sqlComm.Parameters.AddWithValue("@Add_Draw_St", Add_Draw_StCB.Checked)
            sqlComm.Parameters.AddWithValue("@Update_IM_QTY", Update_IM_QTY_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Agents", BalancesCB.Checked)
            sqlComm.Parameters.AddWithValue("@Reports", ReportsCB.Checked)
            sqlComm.Parameters.AddWithValue("@Balance", BalancesCB.Checked)
            sqlComm.Parameters.AddWithValue("@SB_Update", Sales_EditCB.Checked)
            sqlComm.Parameters.AddWithValue("@Pch_Update", Pch_EditCB.Checked)
            sqlComm.Parameters.AddWithValue("@SB_Rtn", SB_RtnCB.Checked)
            sqlComm.Parameters.AddWithValue("@Pch_Rtn", Pch_RtnCB.Checked)
            sqlComm.Parameters.AddWithValue("@End_Table", End_Table_CB.Checked)
            sqlComm.Parameters.AddWithValue("@SB_IM_Update", SB_IM_Update_CB.Checked)
            sqlComm.Parameters.AddWithValue("@SB_Sell_Under_Cost", SB_Sell_Under_Cost_CB.Checked)
            sqlComm.Parameters.AddWithValue("@SB_Show_Price_Info", SB_Show_Price_CB.Checked)
            sqlComm.Parameters.AddWithValue("@SB_Show_Cash", SB_Show_Cash_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Frm", FRM_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Sell_Under_Min_SP", U_Sell_Under_Min_SP_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Sell_Under_Min_SP_2", U_Sell_Under_Min_SP_2_CB.Checked)
            sqlComm.Parameters.AddWithValue("@SB_Show_IM_COST", Show_IM_Cost_CB.Checked)
            sqlComm.Parameters.AddWithValue("@IM", IM_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Exp_Static", Exp_StaticCB.Checked)
            sqlComm.Parameters.AddWithValue("@Frm_M", FRM_M_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Update_Receipt", Update_Receipt_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Cancel_Receipt", Cancel_Receipt_CB.Checked)
            sqlComm.Parameters.AddWithValue("@ExpEdit", ExpEditCB.Checked)
            sqlComm.Parameters.AddWithValue("@is_Can_Skip_AG_Max_Debit", is_Can_Skip_AG_Max_Debit_CB.Checked)
            sqlComm.Parameters.AddWithValue("@SB_Show_Bill_Profet", is_Show_Bill_Profet_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Transfer_Table", Transfer_Table_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Save_otherBill", Save_otherBill_CB.Checked)


            sqlComm.Parameters.AddWithValue("@Tr_Widraw", Tr_Widraw_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Tr_Deposit", Tr_Deposit_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Tr_Convert", Tr_Convert_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Hide_Unrelated_Tr", Hide_Unrelated_Tr_CB.Checked)

            sqlCon.Open()
            Try
                sqlComm.ExecuteNonQuery()
                MsgBox("تـــم إضافة الصلاحية ", MsgBoxStyle.Information)
                SNameTextBox.Clear()
                Load_StoreData()
                TabControl1.Enabled = False
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            sqlCon.Close()
        End Using
    End Sub

    Private Sub STORES_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_StoreData()
        If S_Tables = False Then
            End_Table_CB.Visible = False
            Transfer_Table_CB.Visible = False
        End If
        'If SScreenDefault <> 2 Then SB_Show_Price_CB.Visible = False
        FRM_CB.Visible = S_Frm
        FRM_M_CB.Visible = S_Frm
    End Sub

    Public Sub Load_StoreData()
        Dim c As New C
        Dim s As String = "select T_ID,Valid_Name from Users_Validations"
        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
        Dim dt As New DataTable
        c.Da.Fill(dt)
        S_listBox.DataSource = dt
        S_listBox.DisplayMember = "Valid_Name"
        S_listBox.ValueMember = "T_ID"
    End Sub

    Private Sub EditSButton_Click(sender As Object, e As EventArgs) Handles EditSButton.Click
        If EditSButton.Text = "تعديل" Then
            SNameTextBox.Enabled = True
            DeleteSButton.Enabled = False
            SaveSButton.Enabled = False
            EditSButton.Text = "تأكيد التعديل"
            If S_ID > 1 Then TabControl1.Enabled = True
        Else
            If String.IsNullOrWhiteSpace(SNameTextBox.Text) = False Then Store_Update()
        End If
    End Sub

    Public Sub Store_Update()
        Dim c As New C
        Dim sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
        Using (sqlCon)
            Dim sqlComm As New SqlClient.SqlCommand()
            sqlComm.Connection = sqlCon
            sqlComm.CommandText = "[User_Valid_UPDATE]"
            sqlComm.CommandType = CommandType.StoredProcedure

            sqlComm.Parameters.AddWithValue("@T_ID", S_ID)
            sqlComm.Parameters.AddWithValue("@Valid_Name", SNameTextBox.Text)
            sqlComm.Parameters.AddWithValue("@Sales", SalesCB.Checked)
            sqlComm.Parameters.AddWithValue("@Sales_Dis", SalesDisCB.Checked)
            sqlComm.Parameters.AddWithValue("@SalesVoid", SalesVoidCB.Checked)
            sqlComm.Parameters.AddWithValue("@Pch", PchCB.Checked)
            sqlComm.Parameters.AddWithValue("@PchVoid", PchVoidCB.Checked)
            sqlComm.Parameters.AddWithValue("@Exp", ExpCB.Checked)
            sqlComm.Parameters.AddWithValue("@ExpVoid", ExpVoidCB.Checked)
            sqlComm.Parameters.AddWithValue("@StExplore", StExploreCB.Checked)
            sqlComm.Parameters.AddWithValue("@Add_Draw_St", Add_Draw_StCB.Checked)
            sqlComm.Parameters.AddWithValue("@Update_IM_QTY", Update_IM_QTY_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Agents", BalancesCB.Checked)
            sqlComm.Parameters.AddWithValue("@Reports", ReportsCB.Checked)
            sqlComm.Parameters.AddWithValue("@Balance", BalancesCB.Checked)
            sqlComm.Parameters.AddWithValue("@SB_Update", Sales_EditCB.Checked)
            sqlComm.Parameters.AddWithValue("@Pch_Update", Pch_EditCB.Checked)
            sqlComm.Parameters.AddWithValue("@SB_Rtn", SB_RtnCB.Checked)
            sqlComm.Parameters.AddWithValue("@Pch_Rtn", Pch_RtnCB.Checked)
            sqlComm.Parameters.AddWithValue("@End_Table", End_Table_CB.Checked)
            sqlComm.Parameters.AddWithValue("@SB_IM_Update", SB_IM_Update_CB.Checked)
            sqlComm.Parameters.AddWithValue("@SB_Sell_Under_Cost", SB_Sell_Under_Cost_CB.Checked)
            sqlComm.Parameters.AddWithValue("@SB_Show_Price_Info", SB_Show_Price_CB.Checked)
            sqlComm.Parameters.AddWithValue("@SB_Show_Cash", SB_Show_Cash_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Frm", FRM_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Sell_Under_Min_SP", U_Sell_Under_Min_SP_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Sell_Under_Min_SP_2", U_Sell_Under_Min_SP_2_CB.Checked)
            sqlComm.Parameters.AddWithValue("@SB_Show_IM_COST", Show_IM_Cost_CB.Checked)
            sqlComm.Parameters.AddWithValue("@IM", IM_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Exp_Static", Exp_StaticCB.Checked)
            sqlComm.Parameters.AddWithValue("@Frm_M", FRM_M_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Update_Receipt", Update_Receipt_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Cancel_Receipt", Cancel_Receipt_CB.Checked)
            sqlComm.Parameters.AddWithValue("@ExpEdit", ExpEditCB.Checked)

            sqlComm.Parameters.AddWithValue("@is_Can_Skip_AG_Max_Debit", is_Can_Skip_AG_Max_Debit_CB.Checked)
            sqlComm.Parameters.AddWithValue("@SB_Show_Bill_Profet", is_Show_Bill_Profet_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Transfer_Table", Transfer_Table_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Save_otherBill", Save_otherBill_CB.Checked)

            sqlComm.Parameters.AddWithValue("@Tr_Widraw", Tr_Widraw_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Tr_Deposit", Tr_Deposit_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Tr_Convert", Tr_Convert_CB.Checked)
            sqlComm.Parameters.AddWithValue("@Hide_Unrelated_Tr", Hide_Unrelated_Tr_CB.Checked)


            sqlCon.Open()
            Try
                sqlComm.ExecuteNonQuery()
                MsgBox("تـــم تعديل الصلاحية ", MsgBoxStyle.Information)
                Me.Load_StoreData()
                SNameTextBox.Clear()
                SNameTextBox.Enabled = False
                EditSButton.Text = "تعديل"
                TabControl1.Enabled = False
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            sqlCon.Close()
        End Using
    End Sub

    Private Sub S_listBox_MouseClick(sender As Object, e As MouseEventArgs) Handles S_listBox.MouseClick
        Select_Store()

    End Sub

    Public Sub Select_Store()
        Dim C As New C

        C.Str = "select * from Users_Validations_V where T_ID ='" & S_listBox.SelectedValue & "'"
        C.Com = New SqlCommand(C.Str, C.Con)

        C.Con.Open()
        C.Dr = C.Com.ExecuteReader
        C.Dr.Read()
        S_ID = C.Dr("T_ID")
        SNameTextBox.Text = C.Dr("Valid_Name")
        SalesCB.Checked = C.Dr("Sales")
        SalesDisCB.Checked = C.Dr("Sales_Dis")
        SalesVoidCB.Checked = C.Dr("SalesVoid")
        PchCB.Checked = C.Dr("Pch")
        PchVoidCB.Checked = C.Dr("PchVoid")
        ExpCB.Checked = C.Dr("Exp")
        ExpVoidCB.Checked = C.Dr("ExpVoid")
        StExploreCB.Checked = C.Dr("StExplore")
        Add_Draw_StCB.Checked = C.Dr("Add_Draw_St")
        BalancesCB.Checked = C.Dr("Agents")
        ReportsCB.Checked = C.Dr("Reports")
        BalancesCB.Checked = C.Dr("Balance")
        Update_IM_QTY_CB.Checked = C.Dr("Update_IM_QTY")
        Sales_EditCB.Checked = C.Dr("SB_Update")
        Pch_EditCB.Checked = C.Dr("Pch_Update")
        SB_RtnCB.Checked = C.Dr("SB_Rtn")
        Pch_RtnCB.Checked = C.Dr("Pch_Rtn")
        End_Table_CB.Checked = C.Dr("End_Table")
        SB_IM_Update_CB.Checked = C.Dr("SB_IM_Update")
        SB_Sell_Under_Cost_CB.Checked = C.Dr("SB_Sell_Under_Cost")
        SB_Show_Price_CB.Checked = C.Dr("SB_Show_Price_Info")
        SB_Show_Cash_CB.Checked = C.Dr("SB_Show_Cash")
        FRM_CB.Checked = C.Dr("FRM")
        U_Sell_Under_Min_SP_CB.Checked = C.Dr("Sell_Under_Min_SP")
        U_Sell_Under_Min_SP_2_CB.Checked = C.Dr("Sell_Under_Min_SP_2")
        Show_IM_Cost_CB.Checked = C.Dr("SB_Show_IM_COST")
        IM_CB.Checked = C.Dr("IM")
        Exp_StaticCB.Checked = C.Dr("Exp_Static")
        FRM_M_CB.Checked = C.Dr("Frm_M")
        Update_Receipt_CB.Checked = C.Dr("Update_Receipt")
        Cancel_Receipt_CB.Checked = C.Dr("Cancel_Receipt")
        ExpEditCB.Checked = C.Dr("ExpEdit")
        is_Can_Skip_AG_Max_Debit_CB.Checked = C.Dr("is_Can_Skip_AG_Max_Debit")
        is_Show_Bill_Profet_CB.Checked = C.Dr("SB_Show_Bill_Profet")
        Transfer_Table_CB.Checked = C.Dr("Transfer_Table")
        Save_otherBill_CB.Checked = C.Dr("Save_otherBill")

        Tr_Widraw_CB.Checked = C.Dr("Tr_Widraw")
        Tr_Deposit_CB.Checked = C.Dr("Tr_Deposit")
        Tr_Convert_CB.Checked = C.Dr("Tr_Convert")
        Hide_Unrelated_Tr_CB.Checked = C.Dr("Hide_Unrelated_Tr")


        SNameTextBox.Enabled = False
        DeleteSButton.Enabled = True
        EditSButton.Enabled = True
        SaveSButton.Enabled = False
        EditSButton.Text = "تعديل"
        TabControl1.Enabled = False
        If S_ID = 1 Then
            DeleteSButton.Enabled = False
        Else
            DeleteSButton.Enabled = True
        End If

        C.Con.Close()
    End Sub

    Private Sub DeleteSButton_Click(sender As Object, e As EventArgs) Handles DeleteSButton.Click

        If MessageBox.Show(" تأكيد حذف " + S_Name, "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            S_Delete()
            Load_StoreData()
            SNameTextBox.Enabled = False
            SNameTextBox.Clear()
            DeleteSButton.Enabled = False
            SaveSButton.Enabled = False
            EditSButton.Enabled = False
        End If

    End Sub

    Public Sub S_Delete()
        Dim c As New C
        Dim sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
        Using (sqlCon)
            Dim sqlComm As New SqlClient.SqlCommand()
            c.Com = New SqlClient.SqlCommand
            sqlComm.Connection = sqlCon
            sqlComm.CommandText = "User_Valid_Delete"
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.AddWithValue("T_ID", S_ID)
            sqlCon.Open()
            Try
                sqlComm.ExecuteNonQuery()
                MsgBox("تـم حذف الصلاحية ", MsgBoxStyle.Information)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            sqlCon.Close()
        End Using
    End Sub

    Private Sub SNameTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles SNameTextBox.KeyDown
        If e.KeyCode = Keys.Return Then SaveSButton_Click(sender, e)
    End Sub

    Private Sub Saler_Percent_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Saler_Percent_txt_TextChanged(sender As Object, e As EventArgs)
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub STORES_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub



    Private Sub SalesCB_CheckedChanged(sender As Object, e As EventArgs) Handles SalesCB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SalesDisCB_CheckedChanged(sender As Object, e As EventArgs) Handles SalesDisCB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SalesVoidCB_CheckedChanged(sender As Object, e As EventArgs) Handles SalesVoidCB.CheckedChanged
        CB_CHecked(sender)
    End Sub


    Private Sub PschCB_CheckedChanged(sender As Object, e As EventArgs) Handles PchCB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub PcshVoid_CheckedChanged(sender As Object, e As EventArgs) Handles PchVoidCB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub ExpCB_CheckedChanged(sender As Object, e As EventArgs) Handles ExpCB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub ExpVoidCB_CheckedChanged(sender As Object, e As EventArgs) Handles ExpVoidCB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Add_Draw_StoresCB_CheckedChanged(sender As Object, e As EventArgs) Handles Add_Draw_StCB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub ReportsCB_CheckedChanged(sender As Object, e As EventArgs) Handles ReportsCB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub StoreExploreCB_CheckedChanged(sender As Object, e As EventArgs) Handles StExploreCB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub BalancesCB_CheckedChanged(sender As Object, e As EventArgs) Handles BalancesCB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Update_IM_QTY_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Update_IM_QTY_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Sales_EditCB_CheckedChanged(sender As Object, e As EventArgs) Handles Sales_EditCB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Pch_EditCB_CheckedChanged(sender As Object, e As EventArgs) Handles Pch_EditCB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SB_RtnCB_CheckedChanged(sender As Object, e As EventArgs) Handles SB_RtnCB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Pch_RtnCB_CheckedChanged(sender As Object, e As EventArgs) Handles Pch_RtnCB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub End_Table_CB_CheckedChanged(sender As Object, e As EventArgs) Handles End_Table_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SB_IM_Update_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SB_IM_Update_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SB_Sell_Under_Cost_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SB_Sell_Under_Cost_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub TB_Show_Price_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SB_Show_Price_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SB_Show_Cash_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SB_Show_Cash_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub FRM_CB_CheckedChanged(sender As Object, e As EventArgs) Handles FRM_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub U_Sell_Under_Min_SP_CB_CheckedChanged(sender As Object, e As EventArgs) Handles U_Sell_Under_Min_SP_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Show_IM_CostCB_CheckedChanged(sender As Object, e As EventArgs) Handles Show_IM_Cost_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub


    Private Sub IM_CB_CheckedChanged(sender As Object, e As EventArgs) Handles IM_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Exp_StaticCB_CheckedChanged(sender As Object, e As EventArgs) Handles Exp_StaticCB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub FRM_M_CB_CheckedChanged(sender As Object, e As EventArgs) Handles FRM_M_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Update_Receipt_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Update_Receipt_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Cancel_Receipt_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Cancel_Receipt_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub ExpEditCB_CheckedChanged(sender As Object, e As EventArgs) Handles ExpEditCB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub U_Sell_Under_Min_SP_2_CB_CheckedChanged(sender As Object, e As EventArgs) Handles U_Sell_Under_Min_SP_2_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub is_Can_Skip_AG_Max_Debit_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_Can_Skip_AG_Max_Debit_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub is_Show_Bill_Profet_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_Show_Bill_Profet_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Transfer_Table_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Transfer_Table_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Save_otherBill_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Save_otherBill_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Tr_Widraw_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Tr_Widraw_CB.CheckedChanged, Tr_Deposit_CB.CheckedChanged, Tr_Convert_CB.CheckedChanged, Hide_Unrelated_Tr_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub
End Class