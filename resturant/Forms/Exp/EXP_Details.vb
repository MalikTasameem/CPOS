Public Class EXP_Details : Inherits System.Windows.Forms.Form
    Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Public T_ID As Integer
    Public isDepended As Boolean
    Public isVoid As Boolean
    Dim IM_ID As Integer = 0
    Dim IM_Dt As New DataTable
    Dim IM_QTY As Double = 0
    Public TOTAL As Double = 0

    Dim Bill_DT As New DataTable
    Dim On_Update As Boolean = False
    Public Receipts_DT As New DataTable

    Public isShowingDetails As Boolean = False

    Dim AG_Dt As New DataTable
    Public AG_ID As Integer
    Public Bill_ID As Integer = 0

    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If On_Update = True Then Edit_butt_Click(sender, e)
        FormType = 0
        'F_MainForm.Fill_ALL_IM()
        Me.Dispose()
    End Sub

    Private Sub Expenses_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then If New_butt.Enabled = True Then New_butt_Click(sender, e)
        If e.KeyCode = Keys.F12 Then If Save_butt.Enabled = True Then Save_butt_Click(sender, e)
        If e.KeyCode = Keys.F2 Then If Print_btn.Enabled = True Then Print_btn_Click(sender, e)
        If e.KeyCode = Keys.F3 Then If Edit_butt.Enabled = True Then If Edit_butt.Text = EditState Then Edit_butt_Click(sender, e)
        If e.KeyCode = Keys.F4 Then If Delete_butt.Enabled = True Then Delete_butt_Click(sender, e)
        If e.KeyCode = Keys.F5 Then IM_SH_txt.Select()
    End Sub

    Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        FormType = 8

        Delete_butt.Visible = U_ExpVoid
        Edit_butt.Visible = U_ExpEdit

        EditState = Edit_butt.Text
        DefaultFormState = Me.Text
        Disable_Fields()
        Get_Last_T_ID()

        If isShowing_Trans = True Then
            Select_ExpBill(T_ID_Trans)
            SelectStateBt()
            New_butt.Enabled = False
            SearchButton.Enabled = False
        End If

    End Sub

    Public Sub Get_Last_T_ID()
        Try
            Dim C As New C
            Dim S As String = "Select Top 1 T_ID From Agents_Balance_MV Where User_ID = '" & USER_ID & "' AND BsType_ID = 2 AND isDepended = 0 AND isVoid = 0  AND T_ID BETWEEN " & START_ID & " AND " & END_ID & "  ORDER BY T_ID DESC"
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
                    'SELECT_MAX()
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

    Public Sub SELECT_MAX()
        Dim c As New C
        Try
            Dim s As String
            s = "SELECT ISNULL(MAX(IMEX_ID),0) + 1 AS N FROM Agents_Balance_MV "
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


    Private Sub Enable_Fields()
        Title_txt.Enabled = True
        DateTimeEx.Enabled = True
        Notes_txt.Enabled = True
        Ebable_CatFields()
    End Sub

    Private Sub Disable_Fields()
        Title_txt.Enabled = False
        DateTimeEx.Enabled = False
        Notes_txt.Enabled = False
        Disable_CatFields()
    End Sub

    Private Sub Disable_CatFields()
        IM_SH_txt.Enabled = False
        Show_IM_btn.Enabled = False
        QtyTextBox.Enabled = False
        ADDCatButton.Enabled = False
        RemoveCatButton.Enabled = False
        AG_Panel.Enabled = False
    End Sub

    Private Sub Ebable_CatFields()
        IM_SH_txt.Enabled = True
        Show_IM_btn.Enabled = True
        QtyTextBox.Enabled = True
        ADDCatButton.Enabled = True
        RemoveCatButton.Enabled = True
        AG_Panel.Enabled = True
    End Sub


    Public Sub Switch_Dependcy(F As Boolean)

        If F = True Then
            isDepended = 1
            AGMetroGrid.BackgroundColor = Color.LightGreen
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
            Save_butt.Enabled = False
            Print_btn.Enabled = True
        Else
            isDepended = 0
            AGMetroGrid.BackgroundColor = Color.LightYellow
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
            Save_butt.Enabled = True
            Print_btn.Enabled = False
        End If

    End Sub

    Private Sub NewStateBt()
        Enable_Fields()
        Save_butt.Enabled = True
        Edit_butt.Enabled = False
        Delete_butt.Enabled = False
        Me.Text = "فاتورة مصروفات جديـدة"
        IM_SH_txt.Select()
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
        Edit_butt.BackColor = Color.White
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
                Save_butt.Enabled = True
                Edit_butt.Enabled = False
                Print_btn.Enabled = False
                ' AGMetroGrid.BackgroundColor = Color.Gray
                ' AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.Gray
                Enable_Fields()
                Delete_butt.Enabled = False
            Else
                Edit_butt.Enabled = True
                Print_btn.Enabled = True
                Disable_Fields()
                Delete_butt.Enabled = True
            End If

            DeletedBillLabel.Visible = False
            Edit_butt.Text = EditState

        End If

        Me.Text = "عرض بيانات فاتورة"

    End Sub

    Private Sub ClearFields()
        T_ID = 0
        Notes_txt.Clear()
        Total_TextBox.Clear()
        Bill_DT.Clear()
        Title_txt.Clear()
        DateTimeEx.Text = Date.Now
        Edit_butt.Text = EditState
        DeletedBillLabel.Visible = False
        isVoid = False
        ClearCatFields()
        User_Name_lb.Text = "---"
        Me.Text = FormState
        On_Update = False
        CreditTextBox.Clear()
        Receipts_DT.Clear()
        AG_ID = Default_AG_ID
        AG_SH_txt.Clear()
    End Sub

    Private Sub New_butt_Click(sender As Object, e As EventArgs) Handles New_butt.Click
        If On_Update = True Then Edit_butt_Click(sender, e)
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
        Beep()
        If MessageBox.Show("إعتماد المعاملة ؟", "تنويه", MessageBoxButtons.OKCancel, _
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.OK Then

            Save_Title_Name(T_ID, Title_txt.Text)
            Save_AG_Name(T_ID, AG_ID, On_Update)
            Save_About(T_ID, Notes_txt.Text)
            Save_Date(T_ID, DateTimeEx)
            Update_Total()
            'DependingBill(T_ID)


            Dim F As New Pay_Main_Form
            F.MONEY_VALUE = Total_TextBox.Text
            If FormType = 5 Then
                F.Temp_Tr_ID = SB_TR_ID
            Else
                F.Temp_Tr_ID = PCH_TR_ID
            End If

            F.AG_ID = AG_ID
            F.ShowDialog()

            If F.is_OK = True Then


                Dim Tr_ID, Pay_ID As Integer
                Tr_ID = F.Tr_ID
                Pay_ID = F.Pay_ID


                If DependingBill(T_ID, Pay_ID, Tr_ID) = True Then
                    Switch_Dependcy(1)
                    SelectStateBt()
                End If


            End If











            Switch_Dependcy(1)
            SelectStateBt()
            Select_EX_Receipt(T_ID)
        End If
    End Sub


    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click

        If T_ID > 0 Then
            If On_Update = False Then

                Beep()
                If MessageBox.Show(" سيتم تعديل الفاتورة بشكل مباشر مع كل تغير ... تأكيد التعديل ؟ ", "تعديل فاتورة", MessageBoxButtons.YesNo, _
                             MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    Edit_butt.BackColor = Color.GreenYellow
                    On_Update = True
                    AGMetroGrid.BackgroundColor = Color.LightYellow
                    AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
                    RemoveCatButton.Enabled = True
                    Enable_Fields()
                    Edit_butt.Text = "إيقاف التعديل"
                    '   Network_Edit_Tracker_insert("تعديل فاتورة مصروفات  / رقم آلي : " + Bill_ID_Txt.Text + "  / المدخل :  " + User_Name_lb.Text, Total_TextBox.Text, 0, 0)
                End If

            Else
                Save_AG_Name(T_ID, AG_ID, On_Update)
                Save_Title_Name(T_ID, Title_txt.Text)
                Save_About(T_ID, Notes_txt.Text)
                Save_Date(T_ID, DateTimeEx)
                Update_Total()
                Edit_butt.Text = EditState
                Disable_Fields()
                'MsgBox("تم التعديل", MsgBoxStyle.Information)
                Switch_Dependcy(isDepended)
                SelectStateBt()
                Select_EX_Receipt(T_ID)
                On_Update = False

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
            Network_Edit_Tracker_insert("إلغاء الفاتورة", Bill_ID_Txt.Text, 2, 3)
            isVoid = True
            SelectStateBt()
        End If

    End Sub

    Private Sub TreasuryCard_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub Tr_Name_txtb_Enter(sender As Object, e As EventArgs)
        Arabic_Lang()
    End Sub

    Private Sub Tr_BankNum_TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Total_TextBox.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub BackButton_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    'Private Sub AGMetroGrid_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles AGMetroGrid.CellEndEdit
    '    Update_Cat()
    'End Sub


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
        If IM_ID = 0 Then
            MsgBox("حدد البند", MsgBoxStyle.Exclamation)
            IM_SH_txt.Select()
        Else
            If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"
            Insert_Cat()
        End If

    End Sub

    Private Sub Update_Total()
        If String.IsNullOrWhiteSpace(Total_TextBox.Text) = False Then Save_Total(T_ID, TOTAL, 0)
    End Sub

    Private Sub ClearCatFields()
        IM_SH_txt.Clear()
        QtyTextBox.Clear()
        IM_Cost_txt.Clear()
    End Sub


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
        sqlComm.Parameters.AddWithValue("@BsType_ID", 2)
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters("@EXP_ID").Direction = ParameterDirection.Output
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then
            '  Me.Bill_ID_Txt.Text = sqlComm.Parameters("@EXP_ID").Value.ToString()
            T_ID = sqlComm.Parameters("@T_ID").Value.ToString()
            Select_ExpBill(T_ID)
            'Switch_Dependcy(0)
        End If

    End Sub

    Private Sub Insert_Cat()

        If String.IsNullOrWhiteSpace(IM_Cost_txt.Text) Then IM_Cost_txt.Text = "0"
        'Dim Row_Index As Integer = 0
        'If AGMetroGrid.Rows.Count > 0 Then Row_Index = AGMetroGrid.CurrentCell.RowIndex + 1
        Dim sqlComm As New SqlClient.SqlCommand()
        sqlComm.CommandText = "EXP_Details_Insert"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@IMEX_T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@EX_ID", IM_ID)
        sqlComm.Parameters.AddWithValue("@On_Update", On_Update)
        sqlComm.Parameters.AddWithValue("@Price", IM_Cost_txt.Text)
        sqlComm.Parameters.AddWithValue("@Total", Convert.ToDouble(QtyTextBox.Text) * Convert.ToDouble(IM_Cost_txt.Text))
        If String.IsNullOrWhiteSpace(QtyTextBox.Text) = False Then sqlComm.Parameters.AddWithValue("@QYT", Convert.ToDouble(QtyTextBox.Text))
        If SQL_SP_EXEC(sqlComm) = True Then
            Network_Edit_Tracker_insert(" المصروف:" & IM_SH_txt.Text & " العدد:" & QtyTextBox.Text & " السعر:" & IM_Cost_txt.Text, Bill_ID_Txt.Text, 2, 1)
            SELECT_EXP_Cats(T_ID)
            Update_Total()
            ClearCatFields()
            'If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index).Cells("EX_Name_CL")
            IM_SH_txt.Select()
        End If

    End Sub


    Public Sub SELECT_EXP_Cats(T_ID As Integer)
        Bill_DT.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "EXP_Details_SELECT_Bill"
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
        sqlComm.CommandText = "EXP_Details_Delete"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
        sqlComm.Parameters.AddWithValue("@On_Update", On_Update)
        If SQL_SP_EXEC(sqlComm) = True Then
            Network_Edit_Tracker_insert(" المصروف:" & AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString & _
               " العدد:" & AGMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString & " السعر:" & AGMetroGrid.CurrentRow.Cells("Price_CL").Value.ToString, Bill_ID_Txt.Text, 2, 2)
            SELECT_EXP_Cats(T_ID)
            Update_Total()
            If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index - 1).Cells("EX_Name_CL")
            IM_SH_txt.Select()
        End If

    End Sub

    Private Sub IM_Cost_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_Cost_txt.KeyDown
        Select Case e.KeyCode
            Case Keys.Return : QtyTextBox.Select()
        End Select
    End Sub


    Private Sub QtyTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles QtyTextBox.KeyDown

        Select Case e.KeyCode
            Case Keys.Return : If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)
        End Select

    End Sub

    Private Sub QtyTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QtyTextBox.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub QtyTextBox_TextChanged(sender As Object, e As EventArgs) Handles QtyTextBox.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub RemoveCatButton_Click(sender As Object, e As EventArgs) Handles RemoveCatButton.Click

        If AGMetroGrid.Rows.Count > 0 Then
            If MessageBox.Show(" حذف الصنف " + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel, _
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
        FormType = 8
        PchSearch.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub

    Public Sub Load_IM()
        Dim c As New C
        Try
            IM_Dt.Clear()
            Dim s As String
            s = "select Ex_ID,Ex_Name from Expenses_Card WHERE Ex_Name Like '%" & IM_SH_txt.Text & "%'"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(IM_Dt)
            IMDataGridViewX.DataSource = IM_Dt
            If IM_Dt.Rows.Count > 0 Then
                IMDataGridViewX.Visible = True
                IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
            Else
                IMDataGridViewX.Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub IM_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_SH_txt.KeyDown

        Select Case e.KeyCode
            Case Keys.Return
                If IMDataGridViewX.Visible = True Then
                    Fetch_ItemToList()
                Else
                    IM_Cost_txt.Select()
                End If

            Case Keys.Down
                If IMDataGridViewX.Visible = True Then
                    IMDataGridViewX.Select()
                Else
                    IM_Cost_txt.Select()
                End If
            Case Keys.Delete : IM_SH_txt.Clear()

        End Select


    End Sub

    Private Sub IM_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles IM_SH_txt.TextChanged
        If IM_SH_txt.Text.Count > 0 Then
            Load_IM()
        Else
            IMDataGridViewX.Visible = False
            IM_ID = 0
            IM_Cost_txt.Clear()
        End If

        If IM_ID = 0 Then
            IM_SH_txt.BackColor = Color.LightGray
        Else
            IM_SH_txt.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub

    Private Sub IMDataGridViewX_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles IMDataGridViewX.CellClick
        Fetch_ItemToList()
    End Sub

    Private Sub IMDataGridViewX_KeyDown(sender As Object, e As KeyEventArgs) Handles IMDataGridViewX.KeyDown
        If e.KeyCode = Keys.Return Then
            Fetch_ItemToList()
        End If

        If e.KeyCode = Keys.Up Then
            If IMDataGridViewX.CurrentRow.Index = 0 Then
                IM_SH_txt.Select()
            End If
        End If
    End Sub

    Private Sub Fetch_ItemToList()
        If IMDataGridViewX.Rows.Count > 0 Then
            IM_ID = IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value
            IM_SH_txt.Text = IMDataGridViewX.CurrentRow.Cells("item_name_CL").Value
            IM_SH_txt.BackColor = Color.LightGoldenrodYellow
            IMDataGridViewX.Visible = False
            IM_Cost_txt.Select()
        End If
    End Sub



    Private Sub Show_IM_btn_Click(sender As Object, e As EventArgs) Handles Show_IM_btn.Click
        If IMDataGridViewX.Visible = True Then
            IMDataGridViewX.Visible = False
        Else
            IMDataGridViewX.Visible = True
            Load_ALL_AG()
        End If
    End Sub

    Public Sub Load_ALL_AG()
        Dim c As New C

        Try
            IM_Dt.Clear()
            Dim s As String
            s = "select Ex_ID,Ex_Name from Expenses_Card Order By Ex_Name ASC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(IM_Dt)
            IMDataGridViewX.DataSource = IM_Dt
            If IM_Dt.Rows.Count > 0 Then
                IMDataGridViewX.Visible = True
                IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
            Else
                IMDataGridViewX.Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Dim Tmp_Bill_ID As Integer
    Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
        Tmp_Bill_ID = Bill_ID
        Bill_ID_Txt.Text = Bill_ID - 1
        Get_T_ID()
    End Sub

    Public Sub Get_T_ID()
        Dim C As New C
        Dim S As String = "Select T_ID From Agents_Balance_MV Where EXP_ID = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "' AND BsType_ID = 2"
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
                ' Bill_ID_Txt.Text = Tmp_Bill_ID
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


    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        IMTranPrintData()
    End Sub

    Public Sub IMTranPrintData()

        Try
            Dim pp As New ReportConnection
            pp.rp.Load(Application.StartupPath & "\reports\EXP_Bill.rpt")
            pp.CrTables = pp.rp.Database.Tables
            For Each CrTable In pp.CrTables
                pp.crtableLogoninfo = CrTable.LogOnInfo
                pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
                CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
            Next
            With pp
                .rp.SetParameterValue(0, "  رقم الفاتورة :  " + Bill_ID_Txt.Text + vbNewLine + " تاريخ : " + DateTimeEx.Value)
                .rp.SetParameterValue(1, USER_NAME)
                .rp.SetParameterValue(2, My_Settings.Server_Desc)
                .rp.SetParameterValue(3, T_ID)
                .rp.SetParameterValue(4, Total_TextBox.Text)
                .rp.SetParameterValue(5, IM_Qty_LB.Text)
                .rp.SetParameterValue(6, Notes_txt.Text)
            End With

            'Dim p As New print
            'p.CrystalReportViewer1.ReportSource = pp.rp
            'p.Show()

            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_A4))

            pp.rp.PrintOptions.PrinterName = Default_Printer_A4
            pp.rp.PrintToPrinter(1, False, 0, 0)
            pp.rp.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

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

    Private Sub IM_Cost_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles IM_Cost_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub IM_Cost_txt_TextChanged(sender As Object, e As EventArgs) Handles IM_Cost_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Title_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Title_txt.KeyDown
        If e.KeyCode = Keys.Return Then Save_Title_Name(T_ID, Title_txt.Text)
    End Sub

    Private Sub IMDataGridViewX_VisibleChanged(sender As Object, e As EventArgs) Handles IMDataGridViewX.VisibleChanged
        If IMDataGridViewX.Visible = True Then

            Me.Controls.Add(IMDataGridViewX)
            IMDataGridViewX.BringToFront()
            IMDataGridViewX.Location = New Point(IM_Panel.Location.X, IM_Panel.Location.Y + IM_Panel.Size.Height + 1)
        Else
            IM_Panel.Controls.Add(IMDataGridViewX)
            IMDataGridViewX.Location = New Point(IM_SH_txt.Location.X, IM_SH_txt.Location.Y + IM_SH_txt.Size.Height + 1)
        End If
    End Sub

    Private Sub DeliveryingButton_Click(sender As Object, e As EventArgs) Handles DeliveryingButton.Click
        If isDepended = True Then
            FormType = 8
            AG_Type = 4
            F_Receipt = New Receipt
            Receipt_Tran_ID = T_ID

            With F_Receipt
                Rct_Tr_ID = EXP_TR_ID
                ' .ClearFields()
                .Fields_Panel.Enabled = True
                .AG_Cm.Enabled = False
                .Barcode_SH_txt.Enabled = False
                .Receipt_Title_combobox.Text = "فاتورة مصروفات : " + Bill_ID_Txt.Text
                '.AG_Cm.Set_IM_By_ID(AG_ID)
                .AG_ID = AG_ID
                .money_num_txtb.Text = TOTAL - Convert.ToDouble(CreditTextBox.Text)
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

    '-------------------------------------------------------------------------<AGENTS FILTER>
    Public Sub Load_AG()
        Dim c As New C

        Try
            AG_Dt.Clear()
            Dim s As String
            s = "select AG_ID,Ag_name,ISNULL(T_Balance,0) AS T_Balance from AG_EXP_CARD_V WHERE Ag_name Like '%" & AG_SH_txt.Text & "%' "
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
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AG_SH_txt_Enter(sender As Object, e As EventArgs) Handles AG_SH_txt.Enter
        Set_Ar_Language()
    End Sub

    Private Sub AG_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles AG_SH_txt.KeyDown
        If e.KeyCode = Keys.Down Then AG_Grid.Select()
        If e.KeyCode = Keys.Return Then If AG_Grid.Visible = True Then Fetch_ItemToList2()
        If e.KeyCode = Keys.Delete Then AG_SH_txt.Clear()
    End Sub

    Private Sub AG_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles AG_SH_txt.TextChanged
        If AG_SH_txt.Text.Count > 0 Then
            Load_AG()
        Else
            AG_Grid.Visible = False
            AG_ID = Default_AG_ID
            Save_AG_Name(T_ID, AG_ID, On_Update)
        End If
        Check_AG_Pied()

    End Sub

    Private Sub Check_AG_Pied()
        If S_Stores = False Then
            If AG_ID = Default_AG_ID Then
                AG_SH_txt.BackColor = Color.LightGray
            Else
                AG_SH_txt.BackColor = Color.LightGoldenrodYellow
            End If
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
            AG_SH_txt.BackColor = Color.LightGoldenrodYellow
            AG_Grid.Visible = False
            Save_AG_Name(T_ID, AG_ID, On_Update)
            Check_AG_Pied()
        End If
    End Sub

    Private Sub Show_IM_btn2_Click(sender As Object, e As EventArgs) Handles Show_IM_btn2.Click
        If AG_Grid.Visible = True Then
            AG_Grid.Visible = False

            'Panel1.Controls.Add(AG_Grid)
            'AG_Grid.Location = New Point(AG_SH_txt.Location.X, AG_SH_txt.Location.Y + AG_SH_txt.Size.Height + 1)

        Else
            Fill_All_AG()
            AG_Grid.Visible = True
            AG_Grid.Size = New Point(AG_Grid.Size.Width, 530)

            'Me.Controls.Add(AG_Grid)
            'AG_Grid.BringToFront()
            'AG_Grid.Location = New Point(Panel1.Location.X, Panel1.Location.Y + Panel1.Size.Height + 1)

        End If
    End Sub

    Private Sub Fill_All_AG()
        Try
            Dim C As New C
            AG_Dt.Clear()
            Dim s As String = "SELECT AG_ID,Ag_name,ISNULL(T_Balance,0) AS T_Balance from AG_EXP_CARD_V  Order by Ag_name ASC"
            C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
            C.Da.Fill(AG_Dt)
            AG_Grid.DataSource = AG_Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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

    '-------------------------------------------------------------------------</AGENTS FILTER>
End Class