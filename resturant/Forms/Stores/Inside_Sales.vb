Public Class Inside_Sales : Inherits System.Windows.Forms.Form
    Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Public T_ID As Integer
    Public isDepended As Boolean
    Public isVoid As Boolean
    ' Public Receipts_DT As New DataTable

    Public isCashReceipt_Success As Boolean = False
    Public isShowingDetails As Boolean = False

    '  Dim IM_ID As Integer = 0
    '  Dim IM_Dt As New DataTable
    '  Dim IM_QTY As Double = 0
    Public TOTAL As Double = 0
    ' Public AG_ID As Integer = 0
    '  Dim AG_Dt As New DataTable
    ' Dim U_Dt As New DataTable
    ' Dim Get_Unit As Boolean = False
    ' Dim ALL_QTY As Double = 0
    ' Dim U_Cargo As Double = 0
    ' Dim Valid_Dt As New DataTable
    Dim Bill_DT As New DataTable
    '  Dim U_ID As Integer
    Public On_Update As Boolean
    '   Public Barcode_IM As String = ""
    Public Bill_ID As Integer = 0

    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        FormType = 0
    End Sub

    Private Sub Expenses_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then If New_butt.Enabled = True Then New_butt_Click(sender, e)
        If e.KeyCode = Keys.F12 Then If Save_butt.Enabled = True Then Save_butt_Click(sender, e)
        If e.KeyCode = Keys.F2 Then If Print_btn.Enabled = True Then Print_btn_Click(sender, e)
        If e.KeyCode = Keys.F3 Then If Edit_butt.Enabled = True Then If Edit_butt.Text = EditState Then Edit_butt_Click(sender, e)
        If e.KeyCode = Keys.F4 Then If Delete_butt.Enabled = True Then Delete_butt_Click(sender, e)
        'If e.KeyCode = Keys.F5 Then IM_SH_txt.Select()
        'If e.KeyCode = Keys.F8 Then Barcode_SH_txt.Select()
    End Sub

    Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        FormType = 11
        Check_View_Control()
        EditState = Edit_butt.Text
        DefaultFormState = Me.Text
        'Load_ST()
        Disable_Fields()
        Get_Last_T_ID()

        If isShowing_Trans = True Then
            Select_ExpBill(T_ID_Trans)
            SelectStateBt()
            New_butt.Enabled = False
            SearchButton.Enabled = False
        End If

        'If My_Settings.S_Default = 0 Then
        '    Barcode_SH_txt.Select()
        'Else
        '    IM_SH_txt.Select()
        'End If

    End Sub

    Public Sub Get_Last_T_ID()
        Try
            Dim C As New C
            Dim S As String = "Select Top 1 T_ID From Agents_Balance_MV Where User_ID = '" & USER_ID & "' AND BsType_ID = 17 AND isDepended = 0 AND isVoid = 0 AND T_ID BETWEEN " & START_ID & " AND " & END_ID & " ORDER BY T_ID DESC"
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
        Catch ex As Exception
            '...
        End Try
    End Sub

    Public Sub Check_View_Control()
        AGMetroGrid.Columns("ST_Name_CL").Visible = MY_Settings.S_ST_Name_CL
        AGMetroGrid.Columns("D_Valid_CL").Visible = My_Settings.S_D_Valid_CL
        AGMetroGrid.Columns("IMUnit_CL").Visible = My_Settings.S_IMUnit_CL
        AGMetroGrid.Columns("Price_CL").Visible = My_Settings.S_Price_CL
        AGMetroGrid.Columns("Total_CL").Visible = My_Settings.S_Total_CL
        AGMetroGrid.Columns("IMNUM_CL").Visible = My_Settings.S_IMNUM_CL

        ' IM_Cost_Panel.Visible = U_SB_Show_IM_COST
        AGMetroGrid.Columns("Price_CL").Visible = U_SB_Show_IM_COST

    End Sub

    Public Sub SELECT_MAX()
        Dim c As New C
        Try
            Dim s As String
            s = "SELECT ISNULL(MAX(InSale_ID),0) + 1 AS N FROM Agents_Balance_MV "
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
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub Enable_Fields()
        Title_txt.Enabled = True
        DateTimeEx.Enabled = True
        Notes_txt.Enabled = True
        Emp_FS.Enabled = True
        Ebable_CatFields()
    End Sub

    Private Sub Disable_Fields()
        Title_txt.Enabled = False
        DateTimeEx.Enabled = False
        Notes_txt.Enabled = False
        Disable_CatFields()
        Emp_FS.Enabled = False
    End Sub

    Private Sub Disable_CatFields()
        'IM_SH_txt.Enabled = False
        'Show_IM_btn.Enabled = False
        'Barcode_SH_txt.Enabled = False
        'QtyTextBox.Enabled = False
        ADDCatButton.Enabled = False
        RemoveCatButton.Enabled = False
        Rtn_all_btn.Enabled = False
        InSale_All_ST_Btn.Enabled = False
    End Sub

    Private Sub Ebable_CatFields()
        'IM_SH_txt.Enabled = True
        'Show_IM_btn.Enabled = True
        'Barcode_SH_txt.Enabled = True
        'QtyTextBox.Enabled = True
        ADDCatButton.Enabled = True
        RemoveCatButton.Enabled = True
        Rtn_all_btn.Enabled = True
        InSale_All_ST_Btn.Enabled = True
    End Sub


    Public Sub Switch_Dependcy(F As Boolean)

        If F = True Then
            isDepended = 1
            AGMetroGrid.BackgroundColor = Color.LightGreen
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
            Save_butt.Enabled = False
        Else
            isDepended = 0
            AGMetroGrid.BackgroundColor = Color.LightYellow
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
            Save_butt.Enabled = True
        End If

    End Sub

    Private Sub NewStateBt()
        Enable_Fields()
        Save_butt.Enabled = True
        Edit_butt.Enabled = False
        Delete_butt.Enabled = False
        Me.Text = "فاتورة داخلية جديـدة"
        'If My_Settings.S_Default = 0 Then
        '    Barcode_SH_txt.Select()
        'Else
        '    IM_SH_txt.Select()
        'End If
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
            Disable_Fields()
            Save_butt.Enabled = False
            Edit_butt.Enabled = False
            Edit_butt.Text = EditState
            Delete_butt.Enabled = False
            AGMetroGrid.Enabled = True
            AGMetroGrid.BackgroundColor = Color.IndianRed
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.IndianRed
            Print_btn.Enabled = False

        Else
            If isDepended = False Then
                Switch_Dependcy(0)
                Save_butt.Enabled = True
                Edit_butt.Enabled = False
                Print_btn.Enabled = False
                Delete_butt.Enabled = False
                Enable_Fields()
            Else
                Edit_butt.Enabled = True
                Print_btn.Enabled = True
                Delete_butt.Enabled = True
                Disable_Fields()
                Switch_Dependcy(1)
            End If

            DeletedBillLabel.Visible = False
            Edit_butt.Text = EditState

        End If

        Me.Text = "عرض بيانات فاتورة"

    End Sub

    Private Sub ClearFields()
        isCashReceipt_Success = False
        T_ID = 0
        Notes_txt.Clear()
        Total_TextBox.Clear()
        Bill_DT.Clear()
        ' Receipts_DT.Clear()
        Title_txt.Clear()
        DateTimeEx.Text = Date.Now
        Edit_butt.Text = EditState
        DeletedBillLabel.Visible = False
        isVoid = False
        'ClearCatFields()
        User_Name_lb.Text = "---"
        Me.Text = FormState
        Edit_butt.BackColor = Color.White
        On_Update = False
    End Sub

    Private Sub New_butt_Click(sender As Object, e As EventArgs) Handles New_butt.Click
        Call_New_Bill()
    End Sub

    Private Sub Call_New_Bill()
        If T_ID > 0 Then

            If MessageBox.Show("تهيئـة الحقول وفتح فاتورة جديدة", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information,
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
    End Sub

    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        If AGMetroGrid.Rows.Count > 0 Then
            Beep()
            Save_Title_Name(T_ID, Title_txt.Text)
            Save_About(T_ID, Notes_txt.Text)
            Save_Date(T_ID, DateTimeEx)
            Update_Total()
            DependingBill(T_ID)
            Switch_Dependcy(1)
            SelectStateBt()
        End If
    End Sub


    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        If Edit_butt.Text = EditState Then

            MsgBox(" سيتم تعديل الفاتورة بشكل مباشر مع كل تغير ", MsgBoxStyle.Information, "تعديل فاتورة")
            On_Update = True
            Edit_butt.Text = "إيقاف التعديل"
            Enable_Fields()
            AGMetroGrid.BackgroundColor = Color.LightYellow
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
            Edit_butt.BackColor = Color.GreenYellow
            'If MY_Settings.S_Default = 0 Then
            '    Barcode_SH_txt.Select()
            'Else
            '    IM_SH_txt.Select()
            'End If

        Else
            On_Update = False
            Save_About(T_ID, Notes_txt.Text)
            Save_Date(T_ID, DateTimeEx)
            Save_Title_Name(T_ID, Title_txt.Text)
            Update_Total()
            Edit_butt.Text = EditState
            Edit_butt.BackColor = Color.White
            Disable_Fields()
            SelectStateBt()

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
            Network_Edit_Tracker_insert("إلغاء الفاتورة", Bill_ID_Txt.Text, 17, 3)
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

    Private Sub AGMetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles AGMetroGrid.RowsAdded
        Calc_Total()
    End Sub

    Private Sub AGMetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles AGMetroGrid.RowsRemoved
        Calc_Total()
    End Sub

    Private Sub Calc_Total()
        TOTAL = 0
        Dim QTY = 0
        For i = 0 To AGMetroGrid.Rows.Count - 1
            QTY += AGMetroGrid.Rows(i).Cells("QTY_CL").Value
            TOTAL = TOTAL + AGMetroGrid.Rows(i).Cells("Total_CL").Value
        Next
        Total_TextBox.Text = TOTAL.ToString("N")
        IM_Count_LB.Text = AGMetroGrid.Rows.Count.ToString + " : مواد "
        IM_Qty_LB.Text = QTY.ToString + " : كميات "
    End Sub

    Private Sub ADDCatButton_Click(sender As Object, e As EventArgs) Handles ADDCatButton.Click
        'If IM_ID = 0 Then
        '    MsgBox("حددالصنف", MsgBoxStyle.Exclamation)
        '    IM_SH_txt.Select()
        'Else
        '    If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"
        '    If IM_min_QTY = False Then
        '        If IM_Check_Neg_QTY_() = 1 Then
        '            MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
        '            Exit Sub
        '        End If
        '    End If

        '    Insert_Cat()
        'End If

        F_Inside_Sales_IM_card = New Inside_Sales_IM_card
        F_Inside_Sales_IM_card.T_ID = T_ID
        F_Inside_Sales_IM_card.ShowDialog()
    End Sub

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
    '        If SQL_SP_EXEC(C.Com) Then
    '            F = .Parameters("@F").Value
    '        End If
    '    End With

    '    Return F
    'End Function

    Private Sub Update_Total()
        If String.IsNullOrWhiteSpace(Total_TextBox.Text) = False Then
            Save_Total(T_ID, TOTAL, 0)
        End If
    End Sub

    'Private Sub ClearCatFields()
    '    IM_SH_txt.Clear()
    '    Barcode_SH_txt.Clear()
    '    Current_QTY.Clear()
    '    QtyTextBox.Clear()
    '    IM_Cost_txt.Clear()
    '    U_Dt.Clear()
    '    Barcode_IM = ""
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
        sqlComm.Parameters.AddWithValue("@BsType_ID", 17)
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters("@InSale_ID").Direction = ParameterDirection.Output
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then
            '   Me.Bill_ID_Txt.Text = sqlComm.Parameters("@InSale_ID").Value.ToString()
            T_ID = sqlComm.Parameters("@T_ID").Value.ToString()
            'Switch_Dependcy(0)
            Select_ExpBill(T_ID)
        End If

    End Sub

    'Private Sub Insert_Cat()

    '    If String.IsNullOrWhiteSpace(Barcode_IM) Then Barcode_IM = SELECT_BARCODE(IM_ID, U_ID)
    '    Dim sqlComm As New SqlClient.SqlCommand()
    '    sqlComm.CommandText = "InSale_Details_Insert"
    '    sqlComm.CommandType = CommandType.StoredProcedure
    '    sqlComm.Parameters.AddWithValue("@InSale_T_ID", T_ID)
    '    sqlComm.Parameters.AddWithValue("@IM_ID", IM_ID)
    '    sqlComm.Parameters.AddWithValue("@U_ID", U_ID)
    '    sqlComm.Parameters.AddWithValue("@Price", IM_Cost_txt.Text)
    '    sqlComm.Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
    '    sqlComm.Parameters.AddWithValue("@Barcode", Barcode_IM)
    '    sqlComm.Parameters.AddWithValue("@Total", Convert.ToDouble(QtyTextBox.Text) * Convert.ToDouble(IM_Cost_txt.Text))
    '    If Valid_Panel.Visible = True Then sqlComm.Parameters.AddWithValue("@D_Vaild", Valid_cm.Text)
    '    If String.IsNullOrWhiteSpace(QtyTextBox.Text) = False Then sqlComm.Parameters.AddWithValue("@QYT", Convert.ToDouble(QtyTextBox.Text))
    '    sqlComm.Parameters.AddWithValue("@On_Update", On_Update)
    '    If SQL_SP_EXEC(sqlComm) = True Then
    '        Network_Edit_Tracker_insert(" الصنف:" & IM_SH_txt.Text & " الوحدة:" & IM_Unit_cm.Text & " العدد:" & QtyTextBox.Text & " السعر:" & IM_Cost_txt.Text, Bill_ID_Txt.Text, 17, 1)

    '        Update_Total()
    '        ClearCatFields()
    '        SELECT_EXP_Cats(T_ID)
    '        'If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index).Cells("EX_Name_CL")
    '        If MY_Settings.S_Default = 0 Then
    '            Barcode_SH_txt.Select()
    '        Else
    '            IM_SH_txt.Select()
    '        End If
    '    End If

    'End Sub


    Public Sub SELECT_EXP_Cats(T_ID As Integer)
        Bill_DT.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "InSale_Details_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Bill_T_ID", T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(Bill_DT)
        AGMetroGrid.DataSource = Bill_DT
        If AGMetroGrid.Rows.Count > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(AGMetroGrid.Rows.Count - 1).Cells("EX_Name_CL")
    End Sub

    Private Sub Delete_Cat()
        Dim Row_Index As Integer = AGMetroGrid.CurrentCell.RowIndex
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "InSale_Details_Delete"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
        If SQL_SP_EXEC(sqlComm) = True Then
            Network_Edit_Tracker_insert(" الصنف:" & AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString & " الوحدة:" & AGMetroGrid.CurrentRow.Cells("IMUnit_CL").Value.ToString & _
            " العدد:" & AGMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString & " السعر:" & AGMetroGrid.CurrentRow.Cells("Price_CL").Value.ToString, Bill_ID_Txt.Text, 17, 2)
            SELECT_EXP_Cats(T_ID)
            Update_Total()
            If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index - 1).Cells("EX_Name_CL")
            'If MY_Settings.S_Default = 0 Then
            '    Barcode_SH_txt.Select()
            'Else
            '    IM_SH_txt.Select()
            'End If
        End If

    End Sub

    'Private Sub IM_Cost_txt_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Up : Barcode_SH_txt.Select()
    '        Case Keys.Right : QtyTextBox.Select()
    '    End Select
    'End Sub

    'Private Sub IM_Cost_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub IM_Cost_txt_TextChanged(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
    'End Sub

    'Private Sub QtyTextBox_KeyDown(sender As Object, e As KeyEventArgs)

    '    Select Case e.KeyCode
    '        Case Keys.Return : If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)
    '        Case Keys.Left : If Valid_Panel.Visible = True Then
    '                Valid_cm.Select()
    '                Valid_cm.DroppedDown = True
    '            End If
    '        Case Keys.Up : Barcode_SH_txt.Select()
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
    'End Sub

    Private Sub RemoveCatButton_Click(sender As Object, e As EventArgs) Handles RemoveCatButton.Click

        If AGMetroGrid.Rows.Count > 0 Then
            If MessageBox.Show(" حذف الصنف " + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                Delete_Cat()
            End If
        End If
    End Sub

    Private Sub DateTimeEx_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimeEx.KeyDown
        If e.KeyCode = Keys.Return Then If Edit_butt.Text = EditState Then Save_Date(T_ID, DateTimeEx)
    End Sub


    Private Sub SerachButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        Me.Cursor = Cursors.AppStarting
        ' FormType = 11
        PchSearch.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub

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
    '        IMDataGridViewX.Visible = False
    '        IM_ID = 0
    '        U_Dt.Clear()
    '        ALL_QTY_txt.Clear()
    '        Valid_QTY_txt.Clear()
    '        Current_QTY.Clear()
    '        Valid_Dt.Clear()
    '        IM_Cost_txt.Clear()
    '    End If

    '    If IM_ID = 0 Then
    '        IM_SH_txt.BackColor = Color.LightGray
    '    Else
    '        IM_SH_txt.BackColor = Color.LightGoldenrodYellow
    '    End If
    'End Sub

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

    'Private Sub IMDataGridViewX_CellClick(sender As Object, e As DataGridViewCellEventArgs)
    '    Fetch_ItemToList()
    'End Sub

    'Private Sub IMDataGridViewX_KeyDown(sender As Object, e As KeyEventArgs)
    '    If e.KeyCode = Keys.Return Then Fetch_ItemToList()
    '    If e.KeyCode = Keys.Up Then If IMDataGridViewX.CurrentRow.Index = 0 Then IM_SH_txt.Select()
    'End Sub

    'Private Sub Fetch_ItemToList()
    '    If IMDataGridViewX.Rows.Count > 0 Then
    '        If IMDataGridViewX.Rows.Count > 0 Then
    '            IM_ID = IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value
    '            IM_SH_txt.Text = IMDataGridViewX.CurrentRow.Cells("item_name_CL").Value
    '            IM_SH_txt.BackColor = Color.LightGoldenrodYellow
    '            Get_Unit = False
    '            Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
    '            Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
    '            Fetch_IM_Units()
    '            IMDataGridViewX.Visible = False
    '            QtyTextBox.Select()

    '            If IMDataGridViewX.CurrentRow.Cells("isValid_CL").Value = 1 Then
    '                Valid_Panel.Visible = True
    '                Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
    '            Else
    '                Valid_Panel.Visible = False
    '            End If

    '        End If

    '    End If
    'End Sub

    'Private Sub Fetch_IM_Units()
    '    Get_Unit = False
    '    Dim c As New C
    '    U_Dt.Clear()
    '    Try
    '        Dim s As String
    '        s = "select U_IM_ID,U_Name from IM_Menu_Units_V  WHERE IM_ID = '" & IM_ID & "' Order By U_Cargo Desc"
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
    '            s = "select IM_ID,item_name,isValid from IM_All_V  Order by item_name ASC"
    '        Else
    '            s = "select IM_ID,item_name,isValid from IM_All_V_With_QTY  Order by item_name ASC"
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
    '        Case Keys.Down : IM_Cost_txt.Select()
    '        Case Keys.Delete : Barcode_SH_txt.Clear()
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
    '            s = "select IM_ID,item_name,isValid from IM_All_V WHERE IM_NUM = '" & Barcode_SH_txt.Text & "' And isStore = 1"
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
    '                Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
    '            Else
    '                Valid_Panel.Visible = False
    '            End If

    '            QtyTextBox.Select()
    '            Fetch_IM_Units()
    '            Barcode_SH_txt.Clear()
    '            If Sh_ByNum_CB.Checked = False Then IM_Unit_cm.SelectedValue = c.Dr("U_IM_ID")
    '        Else
    '            MsgBox("لم يتم التعرف على الإدخال ", MsgBoxStyle.Exclamation)
    '            Barcode_IM = ""
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


    'Private Sub NewSalePrice_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub NewSalePrice_txt_TextChanged(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
    'End Sub

    'Private Sub IM_CalcAvgCost_btn_Click(sender As Object, e As EventArgs)
    '    If Current_QTY.Text.Count > 0 Then
    '        If Convert.ToDouble(Current_QTY.Text) > 0 And IM_Cost_txt.Text.Count > 0 Then IM_Calc_Avg()
    '    End If
    'End Sub

    'Public Sub IM_Calc_Avg()
    '    Dim c As New C
    '    Dim U_Cargo As Double = 1
    '    Try
    '        Dim s As String
    '        s = "select U_Cargo from Units WHERE U_ID = '" & IM_Unit_cm.SelectedValue & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            U_Cargo = c.Dr("U_Cargo")
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
    '            Dim Cost As Double = c.Dr("Cost")
    '            MsgBox(((Cost + (Convert.ToDouble(IM_Cost_txt.Text) / U_Cargo)) / 2) * U_Cargo).ToString()
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub


    'Private Sub NewSalePrice_txt_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return : ADDCatButton_Click(sender, e)
    '        Case Keys.Up : Barcode_SH_txt.Select()
    '        Case Keys.Right : IM_Cost_txt.Select()
    '    End Select
    'End Sub

    'Private Sub IM_Fetch_QTY()
    '    Dim c As New C
    '    Try
    '        Dim s As String
    '        s = "select U_ID,U_Cargo,Price from IM_Menu_Units_V WHERE U_IM_ID = '" & IM_Unit_cm.SelectedValue & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            '  IM_Cost_txt.Text = c.Dr("Price")
    '            U_Cargo = c.Dr("U_Cargo")
    '            Dim N As Double = (Convert.ToDouble(IM_QTY) / c.Dr("U_Cargo"))
    '            Current_QTY.Text = N.ToString("N")
    '            ALL_QTY_txt.Text = ALL_QTY / U_Cargo
    '            U_ID = c.Dr("U_ID")
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    '    c = New C
    '    Try
    '        Dim s As String
    '        s = "select Cost from IM_All_V WHERE IM_ID = '" & IM_ID & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            IM_Cost_txt.Text = c.Dr("Cost") * U_Cargo
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub

    'Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs)
    '    If String.IsNullOrWhiteSpace(Current_QTY.Text) = False And Get_Unit = True Then
    '        'If Convert.ToDouble(Current_QTY.Text) <> 0 Then
    '        IM_Fetch_QTY()
    '        If Valid_Panel.Visible = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
    '        'End If

    '    End If
    'End Sub

    'Private Sub IM_Unit_cm_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return, Keys.Left : QtyTextBox.Select()
    '    End Select
    'End Sub


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


    '---------------------------------------
    Dim Tmp_Bill_ID As Integer
    Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
        Tmp_Bill_ID = Bill_ID
        Bill_ID_Txt.Text = Bill_ID - 1
        Get_T_ID()
    End Sub

    Public Sub Get_T_ID()

        Dim C As New C
        Dim S As String = "Select T_ID From Agents_Balance_MV Where InSale_ID = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "'"
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
            Tmp_Bill_ID = Bill_ID
            Bill_ID_Txt.Text = Bill_ID + 1
            Get_T_ID()
        End If
    End Sub

    Private Sub DGV_Control_btn_Click(sender As Object, e As EventArgs) Handles DGV_Control_btn.Click
        FormType = 11
        Switch_To_DV_Show()
    End Sub

    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        IMTranPrintData()
    End Sub

    Public Sub IMTranPrintData()

        Try
            Dim pp As New ReportConnection

            pp.rp.Load(Application.StartupPath & "\reports\InSide_Bill.rpt")

            pp.CrTables = pp.rp.Database.Tables

            For Each CrTable In pp.CrTables
                pp.crtableLogoninfo = CrTable.LogOnInfo
                pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
                CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
            Next
            With pp
                .rp.SetParameterValue(0, " تاريخ : " + DateTimeEx.Value)
                .rp.SetParameterValue(1, USER_NAME)
                .rp.SetParameterValue(2, MY_Settings.Server_Desc)
                .rp.SetParameterValue(3, IM_Qty_LB.Text)
                .rp.SetParameterValue(4, T_ID)
                .rp.SetParameterValue(5, Total_TextBox.Text)
                .rp.SetParameterValue(6, " رقم الفاتورة :  " + Bill_ID_Txt.Text)
                .rp.SetParameterValue(7, " العنوان :  " + Title_txt.Text)
                .rp.SetParameterValue(8, " الموظف :  " + Emp_FS.Textt)
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

    'Private Sub Sh_ByNum_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    '    Barcode_SH_txt.Select()
    'End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub


    Private Sub Bill_ID_Txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Bill_ID_Txt.KeyDown
        If e.KeyCode = Keys.Return Then Get_T_ID()
        If e.KeyCode = Keys.Up Then Up_Bill_btn_Click(sender, e)
        If e.KeyCode = Keys.Down Then Down_Bill_btn_Click(sender, e)
    End Sub

    Private Sub Bill_ID_Txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Bill_ID_Txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub Title_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Title_txt.KeyDown
        If e.KeyCode = Keys.Return Then Save_Title_Name(T_ID, Title_txt.Text)
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

    'Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
    '    If IM_ID > 0 Then
    '        Show_IM_Details.IM_ID = IM_ID
    '        Show_IM_Details.ShowDialog()
    '    End If
    'End Sub

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

    Private Sub OWNER_FS_ID_Changed(sender As Object, e As EventArgs) Handles Emp_FS.ID_Changed
        Save_AG_Name(T_ID, Emp_FS.TXT_ID.Text, False)
    End Sub




    Private Sub Rtn_all_btn_Click(sender As Object, e As EventArgs) Handles Rtn_all_btn.Click
        If MessageBox.Show(" إرجاع كافة البضاعة ", "تأكيد", MessageBoxButtons.OKCancel, _
                     MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.OK Then

            query("DELETE FROM InSale_Details WHERE InSale_T_ID = " & T_ID)
            SELECT_EXP_Cats(T_ID)
        End If
    End Sub

    'Private Sub InSale_ALL_ST()

    '    Dim sqlComm As New SqlClient.SqlCommand
    '    sqlComm.CommandText = "InSale_ALL_ST"
    '    sqlComm.CommandType = CommandType.StoredProcedure
    '    sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
    '    sqlComm.Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
    '    If SQL_SP_EXEC(sqlComm) = True Then F_Inside_Sales.SELECT_EXP_Cats(T_ID)

    'End Sub

    'Private Sub InSale_All_ST_Btn_Click(sender As Object, e As EventArgs) Handles InSale_All_ST_Btn.Click
    '    If MessageBox.Show(" تإذن صرف للمخزن (" + ST_cm.Text + ") كاملا ", "تأكيد", MessageBoxButtons.OKCancel,
    '                 MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.OK Then
    '        InSale_ALL_ST()
    '    End If
    'End Sub
End Class