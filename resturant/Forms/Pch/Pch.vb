Public Class Pch : Inherits System.Windows.Forms.Form
    Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Public T_ID As Integer
    Public isDepended As Boolean
    Public isVoid As Boolean
    Public Receipts_DT As New DataTable
    Dim Indx_ID As Integer

    Public isShowingDetails As Boolean = False

    Public IM_ID As Integer = 0
    Dim IM_Dt As New DataTable
    Dim IM_QTY As Double = 0
    Public TOTAL As Double = 0
    Public AG_ID As Integer = 0
    Dim AG_Dt As New DataTable
    Dim U_Dt As New DataTable
    Dim Get_Unit As Boolean = False
    Public Bill_DT As New DataTable
    Public Exp_DT As New DataTable
    Dim U_Cargo As Double = 1
    Dim ALL_QTY As Double = 0
    Public On_Update As Boolean
    Dim U_ID As Integer
    Public Pch_ID As Integer

    Dim AG_Balance As Double = 0

    Public Disc As Double = 0
    Public Pure As Double = 0

    Public Barcode_IM As String = ""

    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If On_Update = True Then Edit_butt_Click(sender, e)
        Me.Dispose()
        FormType = 0
    End Sub

    Private Sub Expenses_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then If New_butt.Enabled = True Then New_butt_Click(sender, e)
        If e.KeyCode = Keys.F2 Then If Print_btn.Enabled = True Then Print_btn_Click(sender, e)
        If e.KeyCode = Keys.F12 Then If Save_butt.Enabled = True Then Save_butt_Click(sender, e)
        If e.KeyCode = Keys.F3 Then If Edit_butt.Enabled = True And Edit_butt.Visible = True Then If Edit_butt.Text = EditState Then Edit_butt_Click(sender, e)
        If e.KeyCode = Keys.F4 Then If Delete_butt.Enabled = True And Delete_butt.Visible = True Then Delete_butt_Click(sender, e)
        'If e.KeyCode = Keys.F5 Then IM_SH_txt.Select()
        'If e.KeyCode = Keys.F8 Then Barcode_SH_txt.Select()
        If e.KeyCode = Keys.Escape Then Me.Close()



    End Sub

    Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If St_Count() = 1 Then All_St_Panel.Visible = False
        FormType = 2
        Check_View_Control()
        Pch_Exp_Panel.Visible = S_Exp_Pch
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized

        EditState = Edit_butt.Text
        DefaultFormState = Me.Text
        Disable_Fields()
        '  Load_ST()
        Fetch_Currency()
        Get_Last_T_ID()

        If U_Cancel_Pch = False Then Delete_butt.Visible = False
        If isShowing_Trans = True Then
            Select_ExpBill(T_ID_Trans)
            SelectStateBt()
            New_butt.Enabled = False
            SearchButton.Enabled = False
        End If

        ' SELECT_IM()


    End Sub

    Private Sub Fetch_Currency()
        Dim C As New C
        Try
            Dim sql As String = "Select Cr_ID , Cr_Name from Currency Order By Cr_ID ASC"
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            Cr_CM.DataSource = C.Dt
            Cr_CM.DisplayMember = "Cr_Name"
            Cr_CM.ValueMember = "Cr_ID"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Get_Last_T_ID()

        Dim C As New C
        Dim S As String = "Select Top 1 T_ID From Agents_Balance_MV Where User_ID = '" & USER_ID & "' AND BsType_ID = 7 AND isDepended = 0 AND isVoid = 0  AND T_ID BETWEEN " & START_ID & " AND " & END_ID & " ORDER BY T_ID DESC"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                ClearFields()
                T_ID = C.Dr("T_ID")
                Select_ExpBill(T_ID)
            Else
                Call_New_Bill()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

    End Sub

    Public Sub Check_View_Control()
        AGMetroGrid.Columns("ST_Name_CL").Visible = MY_Settings.S_ST_Name_CL
        AGMetroGrid.Columns("D_Valid_CL").Visible = MY_Settings.S_D_Valid_CL
        AGMetroGrid.Columns("IMUnit_CL").Visible = MY_Settings.S_IMUnit_CL
        AGMetroGrid.Columns("Price_CL").Visible = MY_Settings.S_Price_CL
        AGMetroGrid.Columns("Total_CL").Visible = MY_Settings.S_Total_CL
        AGMetroGrid.Columns("Notes_CL").Visible = MY_Settings.SP_Notes_CL
        AGMetroGrid.Columns("IMNUM_CL").Visible = MY_Settings.S_IMNUM_CL
        AGMetroGrid.Columns("Barcode_CL").Visible = MY_Settings.S_Barcode_CL

        'Min_SP_Panel.Visible = S_Allow_MinSP
        'Min_SP_Panel_2.Visible = S_Allow_MinSP
        Aggregate_Btn.Visible = S_Stores


    End Sub

    Public Sub SELECT_MAX()
        Dim c As New C
        Try
            Dim s As String
            s = "SELECT ISNULL(MAX(Pch_ID),0) + 1 AS N FROM Agents_Balance_MV "
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Bill_ID_Txt.Text = c.Dr("N")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
    '        ST_cm.SelectedValue = PCH_ST_ID
    '        If PCH_ST_Can_change = False Then ST_cm.Enabled = False
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub Enable_Fields()
        AG_SH_txt.Enabled = True
        Show_IM_btn2.Enabled = True
        EX_ReferNumTextBox.Enabled = True
        DateTimeEx.Enabled = True
        Notes_txt.Enabled = True
        Ebable_CatFields()
        DiscountPanel.Enabled = True
        ADD_Dist_btn.Enabled = True
        Remove_Dist_btn.Enabled = True
        Cr_Equal_TXT.Enabled = True
        Aggregate_Btn.Enabled = True
        AGMetroGrid.BackgroundColor = Color.LightYellow
        AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
    End Sub

    Private Sub Disable_Fields()
        AG_SH_txt.Enabled = False
        Show_IM_btn2.Enabled = False
        EX_ReferNumTextBox.Enabled = False
        DateTimeEx.Enabled = False
        Notes_txt.Enabled = False
        DiscountPanel.Enabled = False
        ADD_Dist_btn.Enabled = False
        Remove_Dist_btn.Enabled = False
        Cr_Equal_TXT.Enabled = False
        Disable_CatFields()
    End Sub

    Private Sub Disable_CatFields()
        'IM_SH_txt.Enabled = False
        'Show_IM_btn.Enabled = False
        'ADD_New_IM_btn.Enabled = False
        'Barcode_SH_txt.Enabled = False
        'QtyTextBox.Enabled = False
        'PriceTextBox.Enabled = False
        ADDCatButton.Enabled = False
        RemoveCatButton.Enabled = False
        ADD_Dist_btn.Enabled = False
        Remove_Dist_btn.Enabled = False
    End Sub

    Private Sub Ebable_CatFields()
        'IM_SH_txt.Enabled = True
        'Show_IM_btn.Enabled = True
        'ADD_New_IM_btn.Enabled = True
        'Barcode_SH_txt.Enabled = True
        'QtyTextBox.Enabled = True
        'PriceTextBox.Enabled = True
        ADDCatButton.Enabled = True
        RemoveCatButton.Enabled = True
        ADD_Dist_btn.Enabled = True
        Remove_Dist_btn.Enabled = True
    End Sub


    Public Sub Switch_Dependcy(F As Boolean)

        If F = True Then
            isDepended = 1

            AGMetroGrid.BackgroundColor = Color.LightGreen
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
            AG_SH_txt.Enabled = False
            AG_SH_txt.Enabled = False
            DeliveryingButton.Enabled = True
            Save_butt.Enabled = False
        Else
            isDepended = 0
            AGMetroGrid.BackgroundColor = Color.LightYellow
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
            AG_SH_txt.Enabled = True
            AG_SH_txt.Enabled = True
            DeliveryingButton.Enabled = False
            Save_butt.Enabled = True
        End If

    End Sub

    Private Sub NewStateBt()
        Enable_Fields()
        Save_butt.Enabled = True
        Edit_butt.Enabled = False
        Delete_butt.Enabled = False
        Me.Text = "فاتورة مشتريات جديدة"
        AG_Grid.Visible = False
        AG_SH_txt.Enabled = True
        '  SELECT_IM()
    End Sub
    Private Sub DeleteOrUpdateStateBt()
        Disable_Fields()
        Save_butt.Enabled = False
        Edit_butt.Enabled = False
        Delete_butt.Enabled = False
        Me.Text = DefaultFormState
    End Sub

    Private Sub SavedStateBt()
        Disable_Fields()
        Save_butt.Enabled = False
        Edit_butt.Enabled = False
        Delete_butt.Enabled = False
        Me.Text = DefaultFormState
    End Sub

    Public Sub SelectStateBt()

        If isVoid = True Then
            DeletedBillLabel.Visible = True
            Print_btn.Enabled = False
            Disable_Fields()
            Save_butt.Enabled = False
            Edit_butt.Enabled = False
            Edit_butt.Text = EditState
            Delete_butt.Enabled = False
            AGMetroGrid.Enabled = True
            AGMetroGrid.BackgroundColor = Color.IndianRed
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.IndianRed
            DiscountPanel.Enabled = False
            DeliveryingButton.Enabled = False
            Aggregate_Btn.Enabled = False
        Else
            If isDepended = False Then
                Save_butt.Enabled = True
                DiscountPanel.Enabled = True
                Print_btn.Enabled = False
                Enable_Fields()
            Else
                Print_btn.Enabled = True
                AGMetroGrid.BackgroundColor = Color.LightGreen
                AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
                DiscountPanel.Enabled = False
                Disable_Fields()
            End If

            Edit_butt.Enabled = True
            Edit_butt.Text = EditState
            DeletedBillLabel.Visible = False
            Delete_butt.Enabled = True
            DeliveryingButton.Enabled = True
            Aggregate_Btn.Enabled = False

        End If

        Me.Text = "فاتورة مشتريات "
        'SELECT_IM()

    End Sub

    Private Sub ClearFields()
        '  isCashReceipt_Success = False
        T_ID = 0
        AG_ID = Default_AG_ID
        'ST_cm.SelectedValue = PCH_ST_ID
        Notes_txt.Clear()
        EX_ReferNumTextBox.Clear()
        'PriceTextBox.Clear()
        Pure_txt.Clear()
        Bill_DT.Clear()
        Exp_DT.Clear()
        Receipts_DT.Clear()
        DateTimeEx.Text = Date.Now
        Edit_butt.Text = EditState
        DeletedBillLabel.Visible = False
        isVoid = False
        CreditTextBox.ForeColor = Color.Black
        'ClearCatFields()
        User_Name_lb.Text = "---"
        Me.Text = FormState
        On_Update = False
        Edit_butt.BackColor = Color.WhiteSmoke
        AG_SH_txt.Clear()
        AG_Balance = 0
        Discount_txt.Clear()
        Total_txt.Clear()
        Pure_txt.Text = "0"
    End Sub

    Private Sub New_butt_Click(sender As Object, e As EventArgs) Handles New_butt.Click
        If On_Update = True Then Edit_butt_Click(sender, e)
        Call_New_Bill()
    End Sub

    Private Sub Call_New_Bill()
        If T_ID > 0 Then

            If MessageBox.Show("فتح فاتورة جديدة", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information,
                               MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                ClearFields()
                Insert_NewBill()
                NewStateBt()
            End If
        Else
            ClearFields()
            Insert_NewBill()
            NewStateBt()

        End If

        '  SELECT_IM()
    End Sub

    'Private Sub SELECT_IM()
    '    If MY_Settings.S_Default = 0 Then
    '        Barcode_SH_txt.Select()
    '    Else
    '        IM_SH_txt.Select()
    '    End If
    'End Sub

    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        If AGMetroGrid.Rows.Count > 0 Then

            If String.IsNullOrWhiteSpace(AG_SH_txt.Text) = False And AG_ID = 0 Then
                MsgBox("حدد إسم العميل", MsgBoxStyle.Critical, "خطأ في الإعتماد")
                AG_SH_txt.Select()
            Else
                If String.IsNullOrWhiteSpace(AG_SH_txt.Text) Then
                    '   AG_SH_txt.Text = "نقدي"
                    Fetch_ItemToList2()
                End If
                Beep()
                If MessageBox.Show(" حفظ الفاتــورة ؟ ", "تنويه", MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.OK Then
                    Save_AG_Name(T_ID, AG_ID, On_Update)
                    Save_About(T_ID, Notes_txt.Text)
                    Save_ReferNum(T_ID, EX_ReferNumTextBox.Text)
                    Save_Date(T_ID, DateTimeEx)
                    AG_Balance_Update_Equal_Value()
                    Prepare_Discount()
                    If DependingBill(T_ID) = True Then Select_ExpBill(T_ID)

                End If

            End If

        End If
    End Sub

    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        If isDepended = True Then


            If U_Pch_Update = True Then
                If On_Update = False Then
                    Beep()
                    If MessageBox.Show(" سيتم تعديل الفاتورة بشكل مباشر مع كل تغير ... تأكيد التعديل ؟ ", "تعديل فاتورة", MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                        Edit_butt.BackColor = Color.GreenYellow
                        On_Update = True
                        AGMetroGrid.Enabled = True
                        AGMetroGrid.BackgroundColor = Color.LightYellow
                        AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
                        ADDCatButton.Enabled = True
                        RemoveCatButton.Enabled = True
                        Ebable_CatFields()
                        Edit_butt.Text = "إيقاف التعديل"
                        Notes_txt.Enabled = True
                        DateTimeEx.Enabled = True
                        EX_ReferNumTextBox.Enabled = True
                        DiscountPanel.Enabled = True
                        AG_SH_txt.Enabled = True
                        Show_IM_btn2.Enabled = True
                        Aggregate_Btn.Enabled = True
                        If Cr_CM.SelectedValue > 1 And Cr_Equal_TXT.Visible = True Then Cr_Equal_TXT.Enabled = True
                        'SELECT_IM()
                    End If

                Else
                    Save_Total(T_ID, TOTAL, Disc)
                    Save_About(T_ID, Notes_txt.Text)
                    Save_ReferNum(T_ID, EX_ReferNumTextBox.Text)
                    Save_Date(T_ID, DateTimeEx)
                    Prepare_Discount()
                    On_Update = False
                    Edit_butt.Text = EditState
                    Edit_butt.BackColor = Color.WhiteSmoke
                    SelectStateBt()
                    Notes_txt.Enabled = False
                    DiscountPanel.Enabled = False

                    AG_SH_txt.Enabled = False
                    Show_IM_btn2.Enabled = False
                    Select_Pch_Receipt(T_ID)

                End If
            Else
                MsgBox("أنت غير مخول بتعديل فاتورة تم حفظها", MsgBoxStyle.Exclamation)
            End If

        Else

            If Edit_butt.Text = EditState Then
                Edit_butt.Text = "ح التعديل"
                Enable_Fields()
                AGMetroGrid.BackgroundColor = Color.LightYellow
                AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
                'SELECT_IM()
            Else
                Save_About(T_ID, Notes_txt.Text)
                Save_Date(T_ID, DateTimeEx)
                Edit_butt.Text = EditState
                Disable_Fields()
                SelectStateBt()
            End If

        End If

    End Sub

    Private Sub Delete_butt_Click(sender As Object, e As EventArgs) Handles Delete_butt.Click

        If AGMetroGrid.Rows.Count > 0 Then
            If IM_min_QTY = False Then
                If IM_Check_Neg_QTY_For_Cancel_Pch() = 1 Then
                    MsgBox(" لا يمكن سحب كمية بالسالب للصنف  " & Str_Name, MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            End If
        End If


        Beep()
        If MessageBox.Show(" سيتم إلغاء الفاتورة رقم " + Bill_ID_Txt.Text + " وكل المعاملات الخاصة بها ... متأكد ", "إلغــاء فاتورة", MessageBoxButtons.OKCancel,
               MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
            Cancel_Bill()
        End If

    End Sub

    Private Function IM_Check_Neg_QTY_For_Cancel_Pch()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_For_Cancel_Pch"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@T_ID", T_ID)
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

    Private Sub Cancel_Bill()

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Void_Row"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)

        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تم إلغاء الفاتورة", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert("إلغاء الفاتورة", Bill_ID_Txt.Text, 7, 3)
            isVoid = True
            SelectStateBt()
        End If

    End Sub

    Private Sub TreasuryCard_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub Tr_Name_txtb_Enter(sender As Object, e As EventArgs)
        Arabic_Lang()
    End Sub

    Private Sub Tr_BankNum_TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pure_txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub


    Private Sub Calc_Total()
        TOTAL = 0
        Dim Dist_Values As Double = 0

        Dim QTY As Double = 0
        For i = 0 To AGMetroGrid.Rows.Count - 1
            TOTAL = TOTAL + AGMetroGrid.Rows(i).Cells("Total_CL").Value
            QTY += AGMetroGrid.Rows(i).Cells("QTY_CL").Value
        Next

        For j = 0 To Dist_DV.Rows.Count - 1
            If Dist_DV.Rows(j).Cells("isWithBill_CL").Value = False Then
                Dist_Values = Dist_Values + Dist_DV.Rows(j).Cells("Dist_Values_CL").Value
            End If
        Next

        Total_txt.Text = TOTAL.ToString(N_Point_Fter)
        Pure_txt.Text = (TOTAL - Disc).ToString(N_Point_Fter)
        Pure = TOTAL - Disc

        IM_Count_LB.Text = AGMetroGrid.Rows.Count.ToString + " : مواد "
        IM_Qty_LB.Text = QTY.ToString + " : كميات "

        If Cr_CM.SelectedValue > 1 Then T_Other_Cr_TXT.Text = (Pure / Convert.ToDouble(Cr_Equal_TXT.Text)).ToString(N_Point_Fter)

    End Sub

    Private Sub ADDCatButton_Click(sender As Object, e As EventArgs) Handles ADDCatButton.Click
        F_Pch_IM_card = New Pch_IM_card_11
        F_Pch_IM_card.T_ID = T_ID
        F_Pch_IM_card.ShowDialog()

        'If IM_ID = 0 Then
        '    MsgBox("حددالصنف", MsgBoxStyle.Exclamation)
        '    SELECT_IM()
        'ElseIf String.IsNullOrWhiteSpace(PriceTextBox.Text) Then
        '    MsgBox("حدد سعر القطعة", MsgBoxStyle.Exclamation)
        '    PriceTextBox.Select()
        'Else
        '    If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"
        '    For i = 0 To AGMetroGrid.Rows.Count - 1
        '        If AGMetroGrid.Rows(i).Cells("EX_ID_CL").Value = IM_ID And AGMetroGrid.Rows(i).Cells("St_ID_CL").Value = ST_cm.SelectedValue Then

        '            If Valid_Panel.Visible = True Then
        '                If AGMetroGrid.Rows(i).Cells("D_Valid_CL").Value = D_Valid.Text Then
        '                    'MsgBox("تم إدخال الصنف", MsgBoxStyle.Critical, "خطأ في الإدخال")
        '                    If MessageBox.Show(" تم إدخال الصنف ... هل تريد الإستمرار ", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
        '                        Exit Sub
        '                    End If

        '                End If
        '            Else
        '                If MessageBox.Show(" تم إدخال الصنف ... هل تريد الإستمرار ", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
        '                    Exit Sub
        '                End If

        '            End If
        '        End If
        '    Next

        '    If Not String.IsNullOrWhiteSpace(NewSalePrice_txt.Text) And Convert.ToDouble(Prev_Sale_Unit_txt.Text) > 0 Then
        '        If Convert.ToDouble(ALL_QTY_txt.Text) > 0 Then
        '            If Convert.ToDouble(NewSalePrice_txt.Text) < IM_Calc_Avg(False, IM_ID) Then
        '                If MessageBox.Show(" سعر البيع أقل من التكلفة .. هل تريد الإستمرار ", "تحذير", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
        '                    Exit Sub
        '                End If
        '            End If
        '        End If
        '    End If


        '    If Valid_Panel.Visible = True Then
        '        If Ban_Expierd_IM_MV = True Then
        '            If D_Valid.Value.Date <= Date.Now.Date Then
        '                MsgBox("صنف منتهية صلاحيته لا يمكن شرائه", MsgBoxStyle.Critical, "خطأ")
        '                Exit Sub
        '            End If
        '        End If
        '    End If

        '    Insert_Cat()
        'End If

    End Sub


    'Private Sub Update_Total()
    '    'If String.IsNullOrWhiteSpace(Pure_txt.Text) = False Then Save_Total(T_ID, TOTAL, Disc)
    'End Sub

    'Private Sub ClearCatFields()
    '    IMDataGridViewX.Visible = False
    '    IM_SH_txt.Clear()
    '    Barcode_SH_txt.Clear()
    '    Current_QTY.Clear()
    '    PriceTextBox.Clear()
    '    QtyTextBox.Clear()
    '    NewSalePrice_txt.Clear()
    '    U_Dt.Clear()
    '    Prev_Sale_Unit_txt.Clear()
    '    Min_SP_By_One_txt.Clear()
    '    Min_SP_txt.Clear()
    '    Min_SP_2_txt.Clear()
    '    Min_SP_2_By_One_txt.Clear()
    '    Barcode_IM = ""
    '    Valid_ListBox.Items.Clear()
    '    IM_ID = 0
    'End Sub


    Private Sub Insert_NewBill()

        Dim sqlComm As New SqlClient.SqlCommand()

        sqlComm.CommandText = "Agents_BalanceMV_insert"
        sqlComm.CommandType = CommandType.StoredProcedure

        sqlComm.Parameters.AddWithValue("@T_ID", 0)

        sqlComm.Parameters.AddWithValue("@Pch_ID", 0)
        sqlComm.Parameters.AddWithValue("@IMEX_ID", 0)
        sqlComm.Parameters.AddWithValue("@Jrd_ID", 0)
        sqlComm.Parameters.AddWithValue("@SRtn_ID", 0)
        sqlComm.Parameters.AddWithValue("@PRtn_ID", 0)
        sqlComm.Parameters.AddWithValue("@Receipt_Num", 0)
        sqlComm.Parameters.AddWithValue("@ST_Tran_ID", 0)
        sqlComm.Parameters.AddWithValue("@EXP_ID", 0)
        sqlComm.Parameters.AddWithValue("@Frm_ID", 0)
        sqlComm.Parameters.AddWithValue("@ViewSB_ID", 0)
        sqlComm.Parameters.AddWithValue("@InSale_ID", 0)
        sqlComm.Parameters.AddWithValue("@Outsale_ID", 0)
        sqlComm.Parameters.AddWithValue("@Frm_ID_M", 0)
        sqlComm.Parameters.AddWithValue("@AG_ID", 1)
        sqlComm.Parameters.AddWithValue("@Date", Me.DateTimeEx.Value)
        sqlComm.Parameters.AddWithValue("@BsType_ID", 7)
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters("@Pch_ID").Direction = ParameterDirection.Output
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then
            T_ID = sqlComm.Parameters("@T_ID").Value.ToString()
            Select_ExpBill(T_ID)
            '    SELECT_IM()
            Fetch_AG_Currency()
        End If

    End Sub

    'Private Sub Insert_Cat()
    '    Barcode_IM = SELECT_BARCODE(IM_ID, U_ID)

    '    If String.IsNullOrWhiteSpace(Barcode_IM) Then Barcode_IM = SELECT_BARCODE(IM_ID, U_ID)

    '    Dim sqlComm As New SqlClient.SqlCommand()
    '    sqlComm.CommandText = "Pch_Details_Insert"
    '    sqlComm.CommandType = CommandType.StoredProcedure
    '    sqlComm.Parameters.AddWithValue("@T_ID", 0)
    '    sqlComm.Parameters.AddWithValue("@Pch_T_ID", T_ID)
    '    sqlComm.Parameters.AddWithValue("@IM_ID", IM_ID)
    '    sqlComm.Parameters.AddWithValue("@U_ID", U_ID)
    '    sqlComm.Parameters.AddWithValue("@Price", Convert.ToDouble(PriceTextBox.Text) * Convert.ToDouble(Cr_Equal_TXT.Text))
    '    If Valid_Panel.Visible = True Then sqlComm.Parameters.AddWithValue("@D_Vaild", D_Valid.Value.Date)
    '    sqlComm.Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
    '    sqlComm.Parameters.AddWithValue("@QYT", QtyTextBox.Text)
    '    sqlComm.Parameters.AddWithValue("@Total", Convert.ToDouble(QtyTextBox.Text) * (Convert.ToDouble(PriceTextBox.Text) * Convert.ToDouble(Cr_Equal_TXT.Text)))
    '    If String.IsNullOrWhiteSpace(NewSalePrice_txt.Text) = False Then sqlComm.Parameters.AddWithValue("@NewSale", Convert.ToDouble(NewSalePrice_txt.Text) * Convert.ToDouble(Cr_Equal_TXT.Text))
    '    If String.IsNullOrWhiteSpace(NewSaleByOne.Text) = False Then sqlComm.Parameters.AddWithValue("@NewSaleByOne", Convert.ToDouble(NewSaleByOne.Text) * Convert.ToDouble(Cr_Equal_TXT.Text))

    '    If String.IsNullOrWhiteSpace(Min_SP_txt.Text) = False Then sqlComm.Parameters.AddWithValue("@Min_SP", Convert.ToDouble(Min_SP_txt.Text) * Convert.ToDouble(Cr_Equal_TXT.Text))
    '    If String.IsNullOrWhiteSpace(Min_SP_By_One_txt.Text) = False Then sqlComm.Parameters.AddWithValue("@Min_SP_ByOne", Convert.ToDouble(Min_SP_By_One_txt.Text) * Convert.ToDouble(Cr_Equal_TXT.Text))

    '    If String.IsNullOrWhiteSpace(Min_SP_2_txt.Text) = False Then sqlComm.Parameters.AddWithValue("@Min_SP_2", Convert.ToDouble(Min_SP_2_txt.Text) * Convert.ToDouble(Cr_Equal_TXT.Text))
    '    If String.IsNullOrWhiteSpace(Min_SP_2_By_One_txt.Text) = False Then sqlComm.Parameters.AddWithValue("@Min_SP_2_ByOne", Convert.ToDouble(Min_SP_2_By_One_txt.Text) * Convert.ToDouble(Cr_Equal_TXT.Text))

    '    sqlComm.Parameters.AddWithValue("@On_Update", On_Update)
    '    sqlComm.Parameters.AddWithValue("@Barcode", Barcode_IM)

    '    sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

    '    If SQL_SP_EXEC(sqlComm) = True Then

    '        Indx_ID = sqlComm.Parameters("@T_ID").Value
    '        Valid_Notes_Insert()
    '        Update_Total()

    '        If On_Update = True Then DependingUpdatedBill(T_ID)

    '        Network_Edit_Tracker_insert(" الصنف:" + IM_SH_txt.Text + " الوحدة:" + IM_Unit_cm.Text + " العدد:" + QtyTextBox.Text + " السعر:" + PriceTextBox.Text + " البيع:" _
    '                                    + NewSalePrice_txt.Text + " بيع القطعة:" + NewSaleByOne.Text, Bill_ID_Txt.Text, 7, 1)

    '        ClearCatFields()
    '        Pch_Contents_SELECT_Bill()

    '        SELECT_IM()

    '    End If

    'End Sub


    Public Sub Pch_Contents_SELECT_Bill()
        Bill_DT.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Pch_Details_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Bill_T_ID", Me.T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(Bill_DT)
        AGMetroGrid.DataSource = Bill_DT
        If AGMetroGrid.Rows.Count > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(AGMetroGrid.Rows.Count - 1).Cells("EX_Name_CL")
        Calc_Total()
    End Sub

    Public Sub Pch_Contents_SELECT_EXP()
        Exp_DT.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[Pch_Details_SELECT_EXP_Dist]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Bill_T_ID", Me.T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(Exp_DT)
        Dist_DV.DataSource = Exp_DT
    End Sub

    'Private Sub Valid_Notes_Insert()

    '    For i = 0 To Valid_ListBox.Items.Count - 1
    '        Valid_ListBox.SelectedIndex = i

    '        Dim sqlComm As New SqlClient.SqlCommand
    '        sqlComm.CommandText = "Valid_Notes_Insert"
    '        sqlComm.CommandType = CommandType.StoredProcedure
    '        sqlComm.Parameters.AddWithValue("@Pch_T_ID", Indx_ID)
    '        sqlComm.Parameters.AddWithValue("@IM_ID", IM_ID)
    '        sqlComm.Parameters.AddWithValue("@VALID_DATE", Valid_ListBox.SelectedItem)
    '        SQL_SP_EXEC(sqlComm)

    '    Next
    'End Sub

    Private Sub Delete_Cat()

        Dim Row_Index As Integer = AGMetroGrid.CurrentCell.RowIndex
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "Pch_Details_Delete"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
        sqlComm.Parameters.AddWithValue("@On_Update", On_Update)
        If SQL_SP_EXEC(sqlComm) = True Then


            If Not IsDBNull(AGMetroGrid.CurrentRow.Cells("NewSale_CL").Value) Then MsgBox("قم بتعديل سعر البيع من شاشة الأصناف", MsgBoxStyle.Information, "")

            Network_Edit_Tracker_insert(" الصنف:" + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString + " الوحدة:" + AGMetroGrid.CurrentRow.Cells("IMUnit_CL").Value.ToString + " العدد:" + AGMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString _
                            + " السعر:" + AGMetroGrid.CurrentRow.Cells("Price_CL").Value.ToString, Bill_ID_Txt.Text, 7, 2)

            Pch_Contents_SELECT_Bill()
            ' Update_Total()
            If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index - 1).Cells("EX_Name_CL")
            '  SELECT_IM()

        End If

    End Sub

    'Private Sub PriceTextBox_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return, Keys.Left : NewSalePrice_txt.Select()
    '        Case Keys.Up : Barcode_SH_txt.Select()
    '        Case Keys.Right : QtyTextBox.Select()
    '    End Select
    'End Sub

    'Private Sub PriceTextBox_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub PriceTextBox_TextChanged(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
    '    If Not String.IsNullOrWhiteSpace(PriceTextBox.Text) And U_Cargo > 1 Then
    '        CostByOne.Text = Convert.ToDouble(PriceTextBox.Text) / U_Cargo
    '        CalcAvgCost()
    '    Else
    '        CostByOne.Clear()
    '        CalcAvgCost()
    '    End If

    '    If S_Stores = True And Not String.IsNullOrWhiteSpace(ALL_QTY_txt.Text) Then
    '        If Convert.ToDouble(ALL_QTY_txt.Text) <= 0 Then
    '            NewSalePrice_txt.Text = PriceTextBox.Text
    '        End If
    '    End If

    'End Sub

    'Private Sub QtyTextBox_KeyDown(sender As Object, e As KeyEventArgs)

    '    Select Case e.KeyCode
    '        Case Keys.Return, Keys.Left : PriceTextBox.Select()
    '        Case Keys.Up : IM_SH_txt.Select()
    '        Case Keys.Right
    '            IM_Unit_cm.Select()
    '            IM_Unit_cm.DroppedDown = True
    '    End Select

    'End Sub

    'Private Sub QtyTextBox_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub QtyTextBox_TextChanged(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
    '    CalcAvgCost()
    'End Sub

    'Private Sub CalcAvgCost()
    '    If S_Stores = True Then
    '        If String.IsNullOrWhiteSpace(Current_QTY.Text) Then Current_QTY.Text = "0"
    '        If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "0"
    '        If Current_QTY.Text.Count > 0 Then If Convert.ToDouble(Current_QTY.Text) > 0 Then IM_Set_Avg()
    '    End If
    'End Sub

    'Public Sub IM_Set_Avg()
    '    Dim Prev_Cost As Double = 0
    '    Dim Prev_Qty As Double = 0
    '    If String.IsNullOrWhiteSpace(PriceTextBox.Text) Then PriceTextBox.Text = "0"

    '    Dim c As New C
    '    c = New C
    '    Try
    '        Dim s As String
    '        s = "select ISNULL(SUM(QTY),0) AS QTY from ST_Balance_V WHERE IM_ID = '" & IM_ID & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            Prev_Qty = c.Dr("QTY")
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    '    '------------------------------------------------------------------------------------------------

    '    c = New C
    '    Try
    '        Dim s As String
    '        s = "select Cost from IM_All_V WHERE IM_ID = '" & IM_ID & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            Prev_Cost = c.Dr("Cost")
    '            NewSalePrice_txt.Text =
    '             ((((Prev_Cost * Prev_Qty) + (Convert.ToDouble(PriceTextBox.Text) * Convert.ToDouble(QtyTextBox.Text))) / (Prev_Qty + Convert.ToDouble(QtyTextBox.Text))) * U_Cargo).ToString("0.00")
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub

    Private Sub RemoveCatButton_Click(sender As Object, e As EventArgs) Handles RemoveCatButton.Click

        If Dist_DV.Rows.Count > 0 Then
            MsgBox("لا يمكنك إضافة أو التعديل فالأصناف لوجود قيمة موزعة على البضاغة ... قم بإلغاء القيمة الموزعة أولا", MsgBoxStyle.Exclamation, "تنويه")

        Else

            If IM_min_QTY = False Then
                If IM_Check_Neg_QTY_For_Update_Pch() = 1 Then
                    MsgBox("في حالة خذف الصنف ستصبح كمية المخزون سالبة", MsgBoxStyle.Critical, "خطأ")
                    Exit Sub
                End If
            End If

            If AGMetroGrid.Rows.Count > 0 Then
                If MessageBox.Show(" حذف الصنف " + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                    Delete_Cat()
                End If
            End If
        End If

    End Sub

    Private Function IM_Check_Neg_QTY_For_Update_Pch()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_For_Update_Pch"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@Pch_T_ID", AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)

            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                F = .Parameters("@F").Value
            End If
        End With

        Return F
    End Function

    Private Sub EX_ReferNumTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles EX_ReferNumTextBox.KeyDown
        If e.KeyCode = Keys.Return Then If Edit_butt.Text = EditState Then Save_ReferNum(T_ID, EX_ReferNumTextBox.Text)
    End Sub

    Private Sub DateTimeEx_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimeEx.KeyDown
        If e.KeyCode = Keys.Return Then If Edit_butt.Text = EditState Then Save_Date(T_ID, DateTimeEx)
    End Sub


    Private Sub SerachButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        Me.Cursor = Cursors.AppStarting
        FormType = 2
        PchSearch.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub DeliveryingButton_Click(sender As Object, e As EventArgs) Handles DeliveryingButton.Click

        If isDepended = True Then
            FormType = 2
            AG_Type = 4
            F_Receipt = New Receipt
            Receipt_Tran_ID = T_ID


            With F_Receipt
                Rct_Tr_ID = PCH_TR_ID
                .Fields_Panel.Enabled = True
                .AG_Cm.Enabled = False
                .Barcode_SH_txt.Enabled = False
                .Receipt_Title_combobox.Text = "فاتورة مشتريات : " + Bill_ID_Txt.Text
                .AG_ID = AG_ID
                .money_num_txtb.Text = Pure - Convert.ToDouble(CreditTextBox.Text)
            End With

            isShowing_Trans = False
            F_Receipt.ShowDialog()

        Else
            MsgBox("يجب إعتماد الفاتورة أولا", MsgBoxStyle.Exclamation, "")
        End If

    End Sub


    Private Sub ReceiptsMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ReceiptsMetroGrid.MouseDoubleClick
        If ReceiptsMetroGrid.Rows.Count > 0 Then
            AG_Type = 4
            isShowingDetails = True
            F_Receipt = New Receipt
            F_Receipt.ShowDialog()
            isShowingDetails = False
        End If
    End Sub

    Private Sub ReceiptsMetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles ReceiptsMetroGrid.RowsAdded
        Calc_Credit()
    End Sub

    Private Sub Calc_Credit()
        Dim Sum As Double = 0
        For i = 0 To ReceiptsMetroGrid.Rows.Count - 1
            Sum = Sum + ReceiptsMetroGrid.Rows(i).Cells("Value_CL").Value
        Next
        CreditTextBox.Text = Sum.ToString("n")
    End Sub

    Private Sub ReceiptsMetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles ReceiptsMetroGrid.RowsRemoved
        Calc_Credit()
    End Sub


    Private Function Insert_Fast_AG()
        Dim New_AG_ID As Integer = 0
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "Agents_insert"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@AG_ID", 0)
        sqlComm.Parameters.AddWithValue("@Ag_name", AG_SH_txt.Text)
        sqlComm.Parameters.AddWithValue("@Barcode", "")
        sqlComm.Parameters.AddWithValue("@Type_ID", Suply_Type_ID)
        sqlComm.Parameters("@AG_ID").Direction = ParameterDirection.Output
        sqlComm.Parameters.AddWithValue("@E_mail", "")
        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تمت إضافة العميــل", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" (من شاشة المشتريات) الزبون:" & AG_SH_txt.Text, 0, 27, 1)
            New_AG_ID = sqlComm.Parameters("@AG_ID").Value.ToString()
            Load_AG()
        End If
        Return New_AG_ID
    End Function

    'Public Sub Load_IM()
    '    Dim c As New C

    '    Try
    '        IM_Dt.Clear()

    '        c.Str = IM_Serach(IM_SH_txt.Text)
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
    '            If IMDataGridViewX.Visible = True Then
    '                Fetch_ItemToList()
    '            Else
    '                QtyTextBox.Select()
    '            End If

    '        Case Keys.Down
    '            If IMDataGridViewX.Visible = True Then
    '                IMDataGridViewX.Select()
    '            Else
    '                QtyTextBox.Select()
    '            End If

    '        Case Keys.Left : Barcode_SH_txt.Select()
    '        Case Keys.Delete : IM_SH_txt.Clear()
    '    End Select


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

    'Public Sub Fetch_ItemToList()
    '    If IMDataGridViewX.Rows.Count > 0 Then
    '        IM_ID = IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value
    '        IM_SH_txt.Text = IMDataGridViewX.CurrentRow.Cells("item_name_CL").Value
    '        IM_SH_txt.BackColor = Color.LightGoldenrodYellow
    '        Get_Unit = False
    '        Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
    '        Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
    '        Fetch_IM_Units()
    '        IMDataGridViewX.Visible = False
    '        QtyTextBox.Select()
    '        If IMDataGridViewX.CurrentRow.Cells("isValid_CL").Value = 1 Then
    '            Valid_Panel.Visible = True
    '            D_Valid.Value = Date.Now
    '        Else
    '            Valid_Panel.Visible = False
    '        End If
    '    End If
    'End Sub

    'Public Sub Fetch_IM_Units()

    '    Dim c As New C
    '    U_Dt.Clear()
    '    Try
    '        Dim s As String
    '        s = "select U_IM_ID,U_Name from IM_Menu_Units_V WHERE IM_ID = '" & IM_ID & "' Order By U_Cargo Desc"
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
    '        s = "select IM_ID,item_name,isValid from IM_All_V  Order by item_name ASC"
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
    '        Case Keys.Down : PriceTextBox.Select()
    '        Case Keys.Delete
    '            Barcode_SH_txt.Clear()
    '            Barcode_IM = ""
    '    End Select
    'End Sub

    'Public Sub Load_IM_Barcode()

    '    If S_is_Multi_BAR = True Then
    '        If Check_IF_Multi_BAR() > 1 Then
    '            SELECT_Multi_Bar()
    '            Exit Sub
    '        End If
    '    End If


    '    Dim c As New C
    '    IM_Dt.Clear()
    '    Try
    '        Dim s As String
    '        If Sh_ByNum_CB.Checked = True Then
    '            s = "select IM_ID,item_name,isValid from IM_All_V WHERE IM_NUM = '" & Barcode_SH_txt.Text & "'"
    '        Else
    '            s = "select U_IM_ID,IM_ID,item_name,isValid from IM_units_Search_V WHERE Barcode = '" & Barcode_SH_txt.Text & "'"
    '        End If

    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()

    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            IM_ID = c.Dr("IM_ID")
    '            IM_SH_txt.Text = c.Dr("item_name")
    '            If Sh_ByNum_CB.Checked = False Then Barcode_IM = Barcode_SH_txt.Text

    '            Get_Unit = False
    '            Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
    '            Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
    '            IMDataGridViewX.Visible = False

    '            If c.Dr("isValid") = 1 Then
    '                Valid_Panel.Visible = True
    '            Else
    '                Valid_Panel.Visible = False
    '            End If

    '            QtyTextBox.Select()
    '            Fetch_IM_Units()
    '            If Sh_ByNum_CB.Checked = False Then IM_Unit_cm.SelectedValue = c.Dr("U_IM_ID")
    '            Barcode_SH_txt.Clear()
    '        Else
    '            If MessageBox.Show("هذا الصنف غير موجود ضمن قائمة الأصناف ... هل تريد إضافته", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
    '                IM_ADD_New.Barcode_SH_txt.Text = Barcode_SH_txt.Text
    '                IM_ADD_New.ShowDialog()
    '                If is_Add_New_IM = True Then QtyTextBox.Select()
    '            End If
    '        End If

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

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

    '-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------2

    Public Sub Load_AG()
        Dim c As New C

        Try
            AG_Dt.Clear()
            Dim s As String
            s = "select AG_ID,Ag_name,isnull(T_Balance,0) AS T_Balance from AGENTS_MENU_V WHERE Ag_name Like '%" & AG_SH_txt.Text & "%' AND Type_ID IN ('" & Suply_Type_ID & "','" & General_AG_Type_ID & "')"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(AG_Dt)
            AG_Grid.DataSource = AG_Dt
            If AG_Dt.Rows.Count > 0 Then
                AG_Grid.Visible = True
                AG_Grid.Size = New Point(AG_Grid.Size.Width, 530)
            Else
                AG_Grid.Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub GET_AG()
        Dim c As New C

        Try
            AG_Dt.Clear()
            Dim s As String
            s = "select Ag_name from Agents WHERE Ag_ID = '" & AG_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                AG_SH_txt.Text = c.Dr("Ag_name")
                AG_SH_txt.BackColor = Color.LightGoldenrodYellow
                AG_Grid.Visible = False
                Fetch_AG_Currency()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AG_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles AG_SH_txt.KeyDown
        If e.KeyCode = Keys.Down Then AG_Grid.Select()
        If e.KeyCode = Keys.Delete Then AG_SH_txt.Clear()
        If e.KeyCode = Keys.Return Then If AG_Grid.Visible = True Then Fetch_ItemToList2()
    End Sub

    Private Sub AG_SH_txt_Enter(sender As Object, e As EventArgs) Handles AG_SH_txt.Enter
        Set_Ar_Language()
    End Sub

    Private Sub AG_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles AG_SH_txt.TextChanged

        If AG_SH_txt.Text.Count > 0 Then
            Load_AG()
        Else
            AG_Grid.Visible = False
            AG_ID = Default_AG_ID
            Save_AG_Name(T_ID, AG_ID, On_Update)
            Fetch_AG_Currency()
        End If
        Check_AG_Pied()
    End Sub

    Private Sub Check_AG_Pied()
        If AG_ID = Default_AG_ID Then
            AG_SH_txt.BackColor = Color.LightGray
        Else
            AG_SH_txt.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub

    Private Sub AG_Grid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles AG_Grid.CellClick
        Fetch_ItemToList2()
    End Sub

    Private Sub AG_Grid_KeyDown(sender As Object, e As KeyEventArgs) Handles AG_Grid.KeyDown
        If e.KeyCode = Keys.Return Then Fetch_ItemToList2()
        If e.KeyCode = Keys.Up Then If AG_Grid.CurrentRow.Index = 0 Then AG_SH_txt.Select()
    End Sub

    Public Sub Fetch_ItemToList2()
        If AG_Grid.Rows.Count > 0 Then
            AG_ID = AG_Grid.CurrentRow.Cells(0).Value
            AG_SH_txt.Text = AG_Grid.CurrentRow.Cells(1).Value
            AG_Balance = AG_Grid.CurrentRow.Cells(2).Value
            AG_SH_txt.BackColor = Color.LightGoldenrodYellow
            AG_Grid.Visible = False
            Save_AG_Name(T_ID, AG_ID, On_Update)
            Network_Edit_Tracker_insert(" تعديل الفاتورة إلي حساب " & AG_SH_txt.Text, Bill_ID_Txt.Text, 7, 3)
            Fetch_AG_Currency()
            AG_Balance_Update_Equal_Value()
        End If
    End Sub

    Public Sub Fetch_AG_Currency()
        Dim C As New C
        Dim S As String = "Select Cr_ID,Cr_Equal From AGENTS_MENU_V Where AG_ID = '" & AG_ID & "'"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                Cr_CM.SelectedValue = C.Dr("Cr_ID")
                Cr_Equal_TXT.Text = C.Dr("Cr_Equal")

                If Cr_CM.SelectedValue > 1 Then
                    Pure_2_LB.Visible = True
                    T_Other_Cr_TXT.Visible = True
                    Cr_Equal_TXT.Visible = True
                Else
                    Pure_2_LB.Visible = False
                    T_Other_Cr_TXT.Visible = False
                    Cr_Equal_TXT.Visible = False
                End If



            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()
    End Sub

    Private Sub Show_IM_btn2_Click(sender As Object, e As EventArgs) Handles Show_IM_btn2.Click
        If AG_Grid.Visible = True Then
            AG_Grid.Visible = False
        Else
            Fill_All_AG()
            AG_Grid.Visible = True
            AG_Grid.Size = New Point(AG_Grid.Size.Width, 530)
        End If
    End Sub


    Private Sub Fill_All_AG()
        Try
            Dim C As New C
            AG_Dt.Clear()
            Dim s As String = "SELECT top 100 AG_ID,Ag_name,ISNULL(T_Balance,0) AS T_Balance from AGENTS_MENU_V WHERE Type_ID IN ('" & Suply_Type_ID & "','" & General_AG_Type_ID & "') Order by Ag_name ASC"
            C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
            C.Da.Fill(AG_Dt)
            AG_Grid.DataSource = AG_Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub BillPictureBox_DoubleClick(sender As Object, e As EventArgs)

    '    If BillPictureBox.Image IsNot Nothing Then
    '        PchAdd_img.BillPictureBox.Image = BillPictureBox.Image
    '        PchAdd_img.ShowDialog()
    '    Else
    '        PchAdd_img.ShowDialog()
    '    End If

    'End Sub


    'Private Sub NewSalePrice_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub NewSalePrice_txt_TextChanged(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
    '    If Not String.IsNullOrWhiteSpace(NewSalePrice_txt.Text) And U_Cargo > 1 Then
    '        NewSaleByOne.Text = (Convert.ToDouble(NewSalePrice_txt.Text) / U_Cargo).ToString("N")
    '    Else
    '        NewSaleByOne.Clear()
    '    End If
    'End Sub

    'Private Sub IM_CalcAvgCost_btn_Click(sender As Object, e As EventArgs)
    '    If ALL_QTY_txt.Text.Count > 0 Then
    '        If Convert.ToDouble(ALL_QTY_txt.Text) > 0 And PriceTextBox.Text.Count > 0 Then IM_Calc_Avg(True, IM_ID)
    '    End If
    'End Sub



    'Public Function IM_Calc_Avg(isMsg As Boolean, IM_ID As Integer)
    '    Dim c As New C
    '    Dim Prev_Cost As Double = 0
    '    Dim Prev_Qty As Double = 0

    '    Try
    '        Dim s As String
    '        s = "select Cost from IM_All_V WHERE IM_ID = '" & IM_ID & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            Prev_Cost = c.Dr("Cost") * U_Cargo
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    '    c = New C
    '    Try
    '        Dim s As String
    '        s = "select ISNULL(SUM(QTY),0) AS QTY from ST_Balance_V WHERE IM_ID = '" & IM_ID & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            Prev_Qty = c.Dr("QTY") / U_Cargo
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try


    '    Dim AVG_COST As Double = ((Prev_Cost * Prev_Qty) +
    '                              ((Convert.ToDouble(PriceTextBox.Text) / U_Cargo) * (Convert.ToDouble(QtyTextBox.Text) * U_Cargo))) _
    '                          / (Prev_Qty + (Convert.ToDouble(QtyTextBox.Text)))

    '    If isMsg = True Then MsgBox((AVG_COST).ToString("00.00"), MsgBoxStyle.Information, "إجمالي التكلفــة")

    '    Return AVG_COST
    'End Function



    'Private Sub NewSalePrice_txt_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return

    '            If Min_SP_Panel.Visible = True Then
    '                Min_SP_txt.Select()
    '                Exit Sub
    '            End If

    '            If NewSaleByOne.Visible = False Then
    '                If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)
    '            Else
    '                CostByOne.Select()
    '            End If

    '        Case Keys.Left : If NewSaleByOne.Visible = False Then NewSaleByOne.Select()
    '        Case Keys.Up : Barcode_SH_txt.Select()
    '        Case Keys.Right : PriceTextBox.Select()
    '        Case Keys.Down
    '            If Min_SP_txt.Visible = True Then Min_SP_txt.Select()
    '    End Select
    'End Sub

    'Private Sub IM_Fetch_QTY()
    '    Dim c As New C
    '    Try
    '        Dim s As String
    '        s = "select U_ID,U_Cargo,Price,Min_SP from IM_Menu_Units_V WHERE U_IM_ID = '" & IM_Unit_cm.SelectedValue & "' AND IM_ID = '" & IM_ID & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            U_Cargo = c.Dr("U_Cargo")
    '            Prev_Sale_Unit_txt.Text = c.Dr("Price")
    '            'Min_SP_2_txt.Text = c.Dr("Min_SP")
    '            Dim N As Double = (Convert.ToDouble(IM_QTY) / c.Dr("U_Cargo"))
    '            Current_QTY.Text = N.ToString("N")
    '            ALL_QTY_txt.Text = ALL_QTY / U_Cargo
    '            U_ID = c.Dr("U_ID")
    '            If U_Cargo > 1 Then
    '                One_Panel.Visible = True
    '                Two_Panel.Visible = True
    '                NewSaleByOne.Visible = True
    '                CostByOne.Visible = True
    '                Min_SP_By_One_Lb.Visible = True
    '                Min_SP_By_One_txt.Visible = True

    '                Min_SP_2_By_One_txt.Visible = True
    '                Min_SP_2_By_One_Lb.Visible = True

    '            Else
    '                One_Panel.Visible = False
    '                Two_Panel.Visible = False
    '                NewSaleByOne.Visible = False
    '                CostByOne.Visible = False
    '                Min_SP_By_One_Lb.Visible = False
    '                Min_SP_By_One_txt.Visible = False

    '                Min_SP_2_By_One_txt.Visible = False
    '                Min_SP_2_By_One_Lb.Visible = False

    '            End If
    '            CalcAvgCost()
    '            IM_Fetch_last_Pch_Price()
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Private Sub IM_Fetch_last_Pch_Price()
    '    PriceTextBox.Clear()
    '    Dim c As New C
    '    Try
    '        Dim s As String
    '        s = "select TOP 1 Price from Pch_Details WHERE IM_ID = '" & IM_ID & "' AND U_ID = '" & U_ID & "' AND isDepended = 1 ORDER BY Date DESC"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            PriceTextBox.Text = c.Dr("Price")
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub


    'Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs)
    '    If Get_Unit = True Then IM_Fetch_QTY()
    'End Sub


    Private Sub MakeBarcode_btn_Click(sender As Object, e As EventArgs) Handles MakeBarcode_btn.Click
        printbarcode.Auto_Print = True
        printbarcode.ShowDialog()
        printbarcode.Auto_Print = False
    End Sub

    Private Sub AGMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles AGMetroGrid.MouseDoubleClick
        FormType = 2
        If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And AGMetroGrid.Rows.Count > 0 Then Change_IM_Details.ShowDialog()
    End Sub


    'Private Sub IM_Unit_cm_KeyDown(sender As Object, e As KeyEventArgs)

    '    Select Case e.KeyCode
    '        Case Keys.Return, Keys.Left : QtyTextBox.Select()
    '    End Select

    'End Sub

    'Private Sub NewSaleByOne_TextChanged(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
    'End Sub

    'Private Sub CostByOne_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return : NewSaleByOne.Select()
    '        Case Keys.Up : Barcode_SH_txt.Select()
    '        Case Keys.Right : NewSalePrice_txt.Select()
    '        Case Keys.Left : NewSaleByOne.Select()
    '    End Select
    'End Sub

    'Private Sub NewSaleByOne_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return

    '            If Min_SP_Panel_2.Visible = True Then
    '                Min_SP_2_txt.Select()
    '                Exit Sub
    '            Else
    '                If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)
    '            End If

    '        Case Keys.Up : Barcode_SH_txt.Select()
    '        Case Keys.Right : CostByOne.Select()
    '        Case Keys.Down
    '            If Min_SP_By_One_txt.Visible = True Then Min_SP_By_One_txt.Select()
    '    End Select
    'End Sub


    'Private Sub ST_cm_SelectedValueChanged(sender As Object, e As EventArgs)
    '    If Get_Unit = True Then
    '        Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
    '        IM_Fetch_QTY()
    '    End If
    'End Sub


    Dim Tmp_Bill_ID As Integer
    Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
        Tmp_Bill_ID = Pch_ID
        Bill_ID_Txt.Text = Pch_ID - 1
        Get_T_ID()
    End Sub

    Public Sub Get_T_ID()

        Dim C As New C
        Dim S As String = "Select T_ID From Agents_Balance_MV Where Pch_ID = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "'"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                ClearFields()
                T_ID = C.Dr("T_ID")
                Select_ExpBill(T_ID)
            Else
                MsgBox("لم يتم التعرف على الفاتورة", MsgBoxStyle.Exclamation)
                Bill_ID_Txt.Text = Tmp_Bill_ID
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

    End Sub

    Private Sub Up_Bill_btn_Click(sender As Object, e As EventArgs) Handles Up_Bill_btn.Click
        If Not String.IsNullOrWhiteSpace(Bill_ID_Txt.Text) Then
            Tmp_Bill_ID = Pch_ID
            Bill_ID_Txt.Text = Pch_ID + 1
            Get_T_ID()
        End If
    End Sub

    Private Sub DGV_Control_btn_Click(sender As Object, e As EventArgs) Handles DGV_Control_btn.Click
        FormType = 2
        Switch_To_DV_Show()
    End Sub

    'Private Sub BarCode_txt_TextChanged(sender As Object, e As KeyPressEventArgs)
    '    If e.KeyChar = " " Then e.Handled = True
    'End Sub


    'Private Sub ADD_New_IM_btn_Click(sender As Object, e As EventArgs)
    '    IM_ADD_New.ShowDialog()
    '    If is_Add_New_IM = True Then QtyTextBox.Select()
    'End Sub

    'Private Sub Sh_ByNum_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    '    IMDataGridViewX.Columns("IM_NUM_CL").Visible = Sh_ByNum_CB.Checked
    '    Barcode_SH_txt.Select()
    'End Sub

    'Private Sub Ass_U_btn_Click(sender As Object, e As EventArgs)
    '    If IM_ID > 0 Then
    '        Beep()
    '        If MessageBox.Show(" إضافة وحدة للصنف " + IM_SH_txt.Text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
    '            Add_Unit.IM_ID = IM_ID
    '            Add_Unit.ShowDialog()
    '        End If
    '    End If
    'End Sub

    'Private Sub ALL_QTY_txt_TextChanged(sender As Object, e As EventArgs)   ', NewSaleByOne.TextChanged
    '    If Not String.IsNullOrWhiteSpace(sender.Text) Then
    '        Dim N As Double = sender.Text
    '        sender.Text = N.ToString("N")
    '    End If
    'End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub
    Private Sub Bill_ID_Txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Bill_ID_Txt.KeyDown
        If e.KeyCode = Keys.Return Then
            Get_T_ID()
        End If

        If e.KeyCode = Keys.Up Then Up_Bill_btn_Click(sender, e)
        If e.KeyCode = Keys.Down Then Down_Bill_btn_Click(sender, e)
    End Sub

    Private Sub Bill_ID_Txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Bill_ID_Txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub عرضرصيدالعميلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضرصيدالعميلToolStripMenuItem.Click
        MsgBox(Show_AG_T_Balance(AG_ID).ToString(), MsgBoxStyle.Information, "رصيد العميل : " & AG_SH_txt.Text)
    End Sub

    Private Sub كشفحسابالعميلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles كشفحسابالعميلToolStripMenuItem.Click
        Show_AG_Balance()
    End Sub

    Private Sub Show_AG_Balance()
        F_Balances = New Balances
        With F_Balances
            .AG_ID = AG_ID
            .AG_Cm.Set_IM_By_ID(AG_ID)

            .Fetch_AG_Currency()
            .Load_Data()
            .AllAgentsCheckBox.Enabled = False
            .AllRecieptsCheckBox.Checked = True
            .AllUsersCheckBox.Checked = True
            .AllTimeCheckBox.Checked = True
            .AG_MV_Prepare_To_Search()

            .MetroTabControl1.TabPages.Remove(.MetroTabPage2)
            .MetroTabControl1.TabPages.Remove(.MetroTabPage3)
            .MetroTabControl1.TabPages.Remove(.MetroTabPage4)
            .MetroTabControl1.TabPages.Remove(.MetroTabPage5)
            .MetroTabControl1.TabPages.Remove(.MetroTabPage6)
            .MenuStrip1.Visible = False
        End With
        F_Balances.ShowDialog()
    End Sub

    Private Sub إضافةكعميلجديدToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إضافةكعميلجديدToolStripMenuItem.Click
        ADD_Fast_AG()
    End Sub

    Private Sub ADD_Fast_AG()
        If AG_ID <> Default_AG_ID Or GET_AG_NO_SPACES(AG_SH_txt.Text) = True Then
            MsgBox("هذا العميل موجود بالفعل", MsgBoxStyle.Critical, "إضافة عميل")
        ElseIf String.IsNullOrWhiteSpace(AG_SH_txt.Text) Then
            MsgBox("أدخل اسم العميل الجديد", MsgBoxStyle.Exclamation)
            AG_SH_txt.Select()
        Else
            Beep()
            If MessageBox.Show(" إضافة " + AG_SH_txt.Text + " إلى قائمة العملاء ", " إضافة العميل ", MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                Insert_Fast_AG()
            End If
        End If
    End Sub

    Private Sub Calc_Dicount_Btn_Click(sender As Object, e As EventArgs) Handles Calc_Dicount_Btn.Click
        Prepare_Discount()
    End Sub

    Private Sub Prepare_Discount()

        If String.IsNullOrWhiteSpace(Discount_txt.Text) Then Discount_txt.Text = "0"
        Make_Discount()

    End Sub

    Private Sub Make_Discount()
        Disc = Convert.ToDouble(Discount_txt.Text) * Convert.ToDouble(Cr_Equal_TXT.Text)
        Discount_txt.Text = Disc

        Update_Discount(T_ID, Discount_txt.Text)
        Network_Edit_Tracker_insert(" تخفيض للفاتورة بقيمة:" & Disc.ToString, Bill_ID_Txt.Text, 7, 3)

        If Cr_CM.SelectedValue > 1 Then T_Other_Cr_TXT.Text = (Pure / Convert.ToDouble(Cr_Equal_TXT.Text)).ToString("n")

        Pure_txt.Text = (TOTAL - Disc).ToString("n")
        Pure = TOTAL - Disc

    End Sub


    Private Sub Discount_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Discount_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Discount_txt_TextChanged(sender As Object, e As EventArgs) Handles Discount_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
        If String.IsNullOrWhiteSpace(Discount_txt.Text) Then Disc = 0
    End Sub

    'Private Sub Barcode_SH_txt_TextChanged(sender As Object, e As EventArgs)
    '    If Sh_ByNum_CB.Checked = True And Barcode_SH_txt.Text.Count > 0 Then
    '        Load_IMByNum()
    '    Else
    '        IMDataGridViewX.Visible = False
    '    End If
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

    'Private Sub IM_SH_txt_Enter(sender As Object, e As EventArgs)
    '    IMDataGridViewX.Columns("IM_NUM_CL").Visible = False
    'End Sub

    'Private Sub Barcode_SH_txt_Enter(sender As Object, e As EventArgs)
    '    If Sh_ByNum_CB.Checked = True Then IMDataGridViewX.Columns("IM_NUM_CL").Visible = True
    'End Sub

    'Private Sub Min_SP_txt_TextChanged(sender As Object, e As EventArgs)
    '    'Check_Point_in_FloatNum(sender, e)
    '    If MIN_RD.Checked = True Then


    '        If Not String.IsNullOrWhiteSpace(Min_SP_txt.Text) And U_Cargo > 1 Then
    '            Min_SP_By_One_txt.Text = (Convert.ToDouble(Min_SP_txt.Text) / U_Cargo).ToString("N")
    '        Else
    '            Min_SP_By_One_txt.Clear()
    '        End If

    '    End If
    'End Sub

    'Private Sub Min_SP_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub



    'Private Sub Min_SP_txt_KeyDown(sender As Object, e As KeyEventArgs)

    '    Select Case e.KeyCode
    '        Case Keys.Up
    '            NewSalePrice_txt.Select()
    '        Case Keys.Return

    '            If Min_SP_By_One_txt.Visible = True Then
    '                Min_SP_By_One_txt.Select()
    '                Exit Sub
    '            End If

    '            If Min_SP_Panel_2.Visible = True Then
    '                Min_SP_2_txt.Select()
    '                Exit Sub
    '            End If

    '    End Select

    'End Sub

    'Private Sub Min_SP_By_One_txt_KeyDown(sender As Object, e As KeyEventArgs)

    '    Select Case e.KeyCode
    '        Case Keys.Up
    '            NewSaleByOne.Select()
    '        Case Keys.Return
    '            NewSaleByOne.Select()
    '    End Select

    'End Sub

    'Private Sub NewSaleByOne_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    Private Sub Cr_Equal_TXT_KeyDown(sender As Object, e As KeyEventArgs) Handles Cr_Equal_TXT.KeyDown
        If e.KeyCode = Keys.Return Then AG_Balance_Update_Equal_Value()
    End Sub


    Private Sub AG_Balance_Update_Equal_Value()

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "[AG_Balance_Update_Equal_Value]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@Cr_ID", Cr_CM.SelectedValue)
        If Not String.IsNullOrWhiteSpace(Cr_Equal_TXT.Text) Then sqlComm.Parameters.AddWithValue("@Cr_Equal_Value", Cr_Equal_TXT.Text)

        SQL_SP_EXEC(sqlComm)
    End Sub

    Private Sub Cr_Equal_TXT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cr_Equal_TXT.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Cr_Equal_TXT_TextChanged(sender As Object, e As EventArgs) Handles Cr_Equal_TXT.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub ADD_Dist_btn_Click(sender As Object, e As EventArgs) Handles ADD_Dist_btn.Click
        ADD_Pch_Exp.ShowDialog()
    End Sub

    Private Sub Remove_Dist_btn_Click(sender As Object, e As EventArgs) Handles Remove_Dist_btn.Click
        If Dist_DV.Rows.Count > 0 Then
            Beep()
            If MessageBox.Show("حذف الخدمة الموزعة", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then Pch_Exp_Values_DELETE()
        End If
    End Sub

    Public Sub Pch_Exp_Values_DELETE()

        Dim sqlComm As New SqlClient.SqlCommand()
        sqlComm.CommandText = "[Pch_Exp_Values_DELETE]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", Dist_DV.CurrentRow.Cells("Dist_T_ID_CL").Value)
        If SQL_SP_EXEC(sqlComm) = True Then
            Pch_Contents_SELECT_Bill()
            Pch_Contents_SELECT_EXP()
        End If

    End Sub

    Private Sub Cr_CM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cr_CM.SelectedIndexChanged
        If TypeName(Cr_CM.SelectedValue) = "Integer" Then
            If Cr_CM.SelectedValue = 1 Then
                Cr_Equal_TXT.Enabled = False
            Else
                Cr_Equal_TXT.Enabled = True
            End If
        End If
    End Sub

    Private Sub Aggregate_Btn_Click(sender As Object, e As EventArgs) Handles Aggregate_Btn.Click
        Beep()
        If MessageBox.Show("تحديد سعر صرف البضاعة بالتساوي مع متوسط التكلفة ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then Pch_Details_Make_SP_SAME_COST()
    End Sub

    Private Sub Pch_Details_Make_SP_SAME_COST()

        Dim sqlComm As New SqlClient.SqlCommand()
        sqlComm.CommandText = "[Pch_Details_Make_SP_SAME_COST]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        If SQL_SP_EXEC(sqlComm) = True Then Pch_Contents_SELECT_Bill()

    End Sub

    Private Sub AG_Grid_VisibleChanged(sender As Object, e As EventArgs) Handles AG_Grid.VisibleChanged
        If AG_Grid.Visible = True Then

            Me.Controls.Add(AG_Grid)
            AG_Grid.BringToFront()
            AG_Grid.Location = New Point(AG_Panel.Location.X, AG_Panel.Location.Y + AG_Panel.Size.Height + 1)
        Else
            AG_Panel.Controls.Add(AG_Grid)
            AG_Grid.Location = New Point(AG_SH_txt.Location.X, AG_SH_txt.Location.Y + AG_SH_txt.Size.Height + 1)
        End If
    End Sub

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

    'Private Sub Min_SP_2_txt_TextChanged(sender As Object, e As EventArgs)

    '    If MIN_RD_2.Checked = True Then
    '        If Not String.IsNullOrWhiteSpace(Min_SP_2_txt.Text) And U_Cargo > 1 Then
    '            Min_SP_2_By_One_txt.Text = (Convert.ToDouble(Min_SP_2_txt.Text) / U_Cargo).ToString("N")
    '        Else
    '            Min_SP_2_By_One_txt.Clear()
    '        End If
    '    End If

    'End Sub

    'Private Sub Min_SP_2_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub Min_SP_2_txt_KeyDown(sender As Object, e As KeyEventArgs)



    '    Select Case e.KeyCode
    '        Case Keys.Up
    '            NewSalePrice_txt.Select()
    '        Case Keys.Return

    '            If Min_SP_2_By_One_txt.Visible = True Then
    '                Min_SP_2_By_One_txt.Select()
    '                Exit Sub
    '            Else
    '                If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)
    '            End If

    '    End Select

    ' End Sub

    'Private Sub Min_SP_2_By_One_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub


    'Private Sub Min_SP_2_By_One_txt_TextChanged(sender As Object, e As EventArgs)

    '    If MIN_BY_ONE_RD_2.Checked = True Then
    '        If Not String.IsNullOrWhiteSpace(Min_SP_2_By_One_txt.Text) And U_Cargo > 1 Then
    '            Min_SP_2_txt.Text = (Convert.ToDouble(Min_SP_2_By_One_txt.Text) * U_Cargo).ToString("N")
    '        Else
    '            Min_SP_2_txt.Clear()
    '        End If
    '    End If

    'End Sub

    Public Sub IMTranPrintData()

        Try
            Dim pp As New ReportConnection

            pp.rp.Load(Application.StartupPath & "\reports\Invoice_Bill.rpt")

            pp.CrTables = pp.rp.Database.Tables

            For Each CrTable In pp.CrTables
                pp.crtableLogoninfo = CrTable.LogOnInfo
                pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
                CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
            Next
            With pp
                .rp.SetParameterValue(0, " تاريخ : " + DateTimeEx.Value)
                .rp.SetParameterValue(1, USER_NAME)
                .rp.SetParameterValue(2, SBill_Title_1 & vbNewLine & SBill_Title_2)
                .rp.SetParameterValue(3, IM_Qty_LB.Text)
                .rp.SetParameterValue(4, T_ID)
                .rp.SetParameterValue(5, Pure_txt.Text)
                .rp.SetParameterValue(6, "فاتـــورة مشتريــات")

                .rp.SetParameterValue(7, " العنوان : " + EX_ReferNumTextBox.Text)
                .rp.SetParameterValue(8, " الرقم الألي : " + Bill_ID_Txt.Text)
                .rp.SetParameterValue(9, " المـورد : " + AG_SH_txt.Text + vbNewLine)

            End With

            Dim p As New print
            p.CrystalReportViewer1.ReportSource = pp.rp
            p.Show()

            'If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_A4))
            'pp.rp.PrintOptions.PrinterName = Default_Printer_A4
            'pp.rp.PrintToPrinter(1, False, 0, 0)
            'pp.rp.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        IMTranPrintData()
    End Sub

    Private Sub Dist_DV_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles Dist_DV.RowsAdded
        Calc_Total()
    End Sub

    Private Sub Dist_DV_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles Dist_DV.RowsRemoved
        Calc_Total()
    End Sub

    Private Sub Discount_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Discount_txt.KeyDown
        If e.KeyCode = Keys.Return Then Prepare_Discount()
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

    Private Sub IM_btn_Click(sender As Object, e As EventArgs) Handles IM_btn.Click
        F_ItemsMenu = New ItemsMenu
        F_ItemsMenu.ShowDialog()
    End Sub

    'Private Sub Add_Valid_Btn_Click(sender As Object, e As EventArgs)
    '    Valid_ListBox.Items.Add(Valid_For_List_Date.Value)
    'End Sub

    'Private Sub Remove_Valid_Btn_Click(sender As Object, e As EventArgs)
    '    Valid_ListBox.Items.Remove(Valid_ListBox.SelectedItem)
    'End Sub

    Private Sub تعديلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تعديلToolStripMenuItem.Click
        FormType = 2
        If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And AGMetroGrid.Rows.Count > 0 Then Change_IM_Details.ShowDialog()
    End Sub

    Private Sub عرضالتكلفةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضالتكلفةToolStripMenuItem.Click
        Show_IM_Cost(True, F_Pch.AGMetroGrid.CurrentRow.Cells("EX_ID_CL").Value, F_Pch.AGMetroGrid.CurrentRow.Cells("U_ID_CL").Value)
    End Sub

    Private Sub تعديلصلاحياتالصنفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تعديلصلاحياتالصنفToolStripMenuItem.Click
        Mang_IM_Valid_Notes.IM_ID = AGMetroGrid.CurrentRow.Cells("EX_ID_CL").Value
        Mang_IM_Valid_Notes.Bill_T_ID = AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value
        Mang_IM_Valid_Notes.IM_NAME = AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value
        Mang_IM_Valid_Notes.ShowDialog()
    End Sub

    'Private Sub Show_IM_Note_Valid_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    '    IM_Valid_Note_Panel.Visible = Show_IM_Note_Valid_CB.Checked()
    'End Sub

    Private Sub علاضبطاقةالصنفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles علاضبطاقةالصنفToolStripMenuItem.Click
        isShowing_Trans = True
        Str_ = F_Pch.AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value
        F_ItemsMenu = New ItemsMenu
        F_ItemsMenu.ShowDialog()
        isShowing_Trans = False
    End Sub

    'Private Sub Min_SP_2_By_One_txt_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return

    '            If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)

    '        Case Keys.Up : NewSalePrice_txt.Select()
    '        Case Keys.Right : Min_SP_2_txt.Select()

    '    End Select
    'End Sub

    'Private Sub Confirm_ADD_bercent_Click(sender As Object, e As EventArgs)
    '    If SP_CB.Checked = True Then NewSalePrice_txt.Text = ((Convert.ToDouble(bercent_ADD_txt.Text) * Convert.ToDouble(PriceTextBox.Text)) / 100) + Convert.ToDouble(PriceTextBox.Text)

    '    If SP_1_CB.Checked = True Then Min_SP_txt.Text = ((Convert.ToDouble(bercent_ADD_txt.Text) * Convert.ToDouble(PriceTextBox.Text)) / 100) + Convert.ToDouble(PriceTextBox.Text)

    '    If SP_2_CB.Checked = True Then Min_SP_2_txt.Text = ((Convert.ToDouble(bercent_ADD_txt.Text) * Convert.ToDouble(PriceTextBox.Text)) / 100) + Convert.ToDouble(PriceTextBox.Text)

    'End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If IM_ID > 0 Then
            Show_IM_Details.IM_ID = IM_ID
            Show_IM_Details.ShowDialog()
        End If
    End Sub


    Private Sub DeletedBillLabel_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DeletedBillLabel.MouseDoubleClick
        If U_Cancel_Pch = True Then
            Beep()
            If MessageBox.Show(" سيتم تراجع عن إلغاء الفاتورة رقم " + Bill_ID_Txt.Text + " وكل المعاملات الخاصة بها ... متأكد ", "تاكيد العملية", MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
                AG_Balance_UN_Void_Row(T_ID, Pch_ID, 7)
                Get_T_ID()
            End If

        End If
    End Sub

    'Private Sub SP_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    'End Sub

    'Private Sub SP_1_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    'End Sub

    'Private Sub SP_2_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    'End Sub

    'Private Sub Min_SP_Panel_VisibleChanged(sender As Object, e As EventArgs)
    '    SP_1_CB.Visible = Min_SP_Panel.Visible
    'End Sub

    'Private Sub Min_SP_Panel_2_VisibleChanged(sender As Object, e As EventArgs)
    '    SP_2_CB.Visible = Min_SP_Panel_2.Visible
    'End Sub


    Private Sub تخفيضبنسبةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تخفيضبنسبةToolStripMenuItem.Click
        Dim F_Percent_Disc As New Percent_Disc
        F_Percent_Disc.T_ID = T_ID
        F_Percent_Disc.TOTAL = TOTAL
        F_Percent_Disc.ShowDialog()
        Select_ExpBill(T_ID)

    End Sub

    Private Sub ADD_New_IM_btn_Click(sender As Object, e As EventArgs)

    End Sub


    'Private Sub Min_SP_By_One_txt_TextChanged(sender As Object, e As EventArgs)

    '    If MIN_BY_ONE_RD.Checked = True Then

    '        If Not String.IsNullOrWhiteSpace(Min_SP_By_One_txt.Text) And U_Cargo > 1 Then
    '            Min_SP_txt.Text = (Convert.ToDouble(Min_SP_By_One_txt.Text) * U_Cargo).ToString("N")
    '        Else
    '            Min_SP_txt.Clear()
    '        End If

    '    End If

    'End Sub

End Class