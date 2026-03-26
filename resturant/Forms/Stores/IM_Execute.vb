Public Class IM_Execute : Inherits System.Windows.Forms.Form
    Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Public T_ID As Integer
    Public isDepended As Boolean
    Public isVoid As Boolean

    Dim IM_ID As Integer = 0
    ' Dim IM_Dt As New DataTable
    Dim IM_QTY As Double = 0
    Public TOTAL As Double = 0

    ' Dim U_Dt As New DataTable
    ' Dim Get_Unit As Boolean = False
    ' Dim ALL_QTY As Double = 0
    ' Dim U_Cargo As Double = 0
    '  Dim Valid_Dt As New DataTable
    Dim Bill_DT As New DataTable
    '  Dim U_ID As Integer
    Dim On_Update As Boolean
    Dim AG_Dt As New DataTable
    Public AG_ID As Integer
    ' Public Barcode_IM As String = ""

    Public Bill_ID As Integer = 0

    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        FormType = 0
        'F_MainForm.Fill_ALL_IM()
    End Sub

    Private Sub Expenses_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then If New_butt.Enabled = True Then New_butt_Click(sender, e)
        If e.KeyCode = Keys.F12 Then If Save_butt.Enabled = True Then Save_butt_Click(sender, e)
        If e.KeyCode = Keys.F2 Then If Print_btn.Enabled = True Then Print_btn_Click(sender, e)
        If e.KeyCode = Keys.F3 Then If Edit_butt.Enabled = True Then If Edit_butt.Text = EditState Then Edit_butt_Click(sender, e)
        If e.KeyCode = Keys.F4 Then If Delete_butt.Enabled = True Then Delete_butt_Click(sender, e)
        'If e.KeyCode = Keys.F5 Then mySearchControl.txtSearch.Select()

    End Sub

    Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        ' If St_Count() = 1 Then All_St_Panel.Visible = False
        Me.WindowState = FormWindowState.Maximized
        FormType = 3
        Check_View_Control()
        EditState = Edit_butt.Text
        DefaultFormState = Me.Text
        '   Load_ST()
        Disable_Fields()
        Get_Last_T_ID()

        If isShowing_Trans = True Then
            Select_ExpBill(T_ID_Trans)
            SelectStateBt()
            New_butt.Enabled = False
            SearchButton.Enabled = False
        End If


        'Load_ALL_IM()
        '' تحميل البيانات
        'mySearchControl.ItemsTable = IM_Dt
        'mySearchControl.itemsTable_Barcode = IM_Dt_Barcodes
        'mySearchControl.MaxGridHeight = 400
        ''mySearchControl.DefaultSearchField = "اسم الصنف"
        '' إضافة الكنترول للفورم
        ''Me.Controls.Add(mySearchControl)
        '' استقبال الاختيار
        'AddHandler mySearchControl.ItemSelected, AddressOf HandleItemSelected

        'mySearchControl.txtSearch.Select()

    End Sub


    'Private Sub HandleItemSelected(itemId As Integer)

    '    IM_ID = itemId
    '    Get_Unit = False
    '    Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
    '    Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
    '    Fetch_IM_Units()
    '    QtyTextBox.Select()

    'End Sub

    Public Sub Get_Last_T_ID()

        Dim C As New C
        Dim S As String = "Select Top 1 T_ID From Agents_Balance_MV Where User_ID = '" & USER_ID & "' AND BsType_ID = 8 AND isDepended = 0 AND isVoid = 0  AND T_ID BETWEEN " & START_ID & " AND " & END_ID & " ORDER BY T_ID DESC"
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
                'SELECT_MAX()
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
        AGMetroGrid.Columns("IMNUM_CL").Visible = MY_Settings.S_IMNUM_CL
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
        AG_Cm.Enabled = True
        Ebable_CatFields()
    End Sub

    Private Sub Disable_Fields()
        Title_txt.Enabled = False
        DateTimeEx.Enabled = False
        Notes_txt.Enabled = False
        AG_Cm.Enabled = False
        Disable_CatFields()
    End Sub

    Private Sub Disable_CatFields()
        '  mySearchControl.Enabled = False
        ' QtyTextBox.Enabled = False
        ADDCatButton.Enabled = False
        RemoveCatButton.Enabled = False
    End Sub

    Private Sub Ebable_CatFields()
        '    mySearchControl.Enabled = True
        '   QtyTextBox.Enabled = True
        ADDCatButton.Enabled = True
        RemoveCatButton.Enabled = True
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
        Me.Text = "فاتورة تالف جديـدة"
        '  mySearchControl.txtSearch.Select()
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
        T_ID = 0
        Notes_txt.Clear()
        Total_TextBox.Clear()
        Bill_DT.Clear()
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
            Network_Edit_Tracker_insert("تعديل فاتورة تالف / رقم آلي : " + Bill_ID_Txt.Text + " / المدخل :  " + User_Name_lb.Text, Total_TextBox.Text, 0, 0)
            'mySearchControl.txtSearch.Select()
        Else
            On_Update = False
            Edit_butt.BackColor = Color.White
            Update_Total()
            Save_About(T_ID, Notes_txt.Text)
            Save_Date(T_ID, DateTimeEx)
            Save_Title_Name(T_ID, Title_txt.Text)
            Edit_butt.Text = EditState
            Disable_Fields()
            ' MsgBox("تم التعديل", MsgBoxStyle.Information)
            SelectStateBt()
        End If


    End Sub

    Private Sub Delete_butt_Click(sender As Object, e As EventArgs) Handles Delete_butt.Click

        Beep()
        If MessageBox.Show(" سيتم إلغاء الفاتورة رقم " + Bill_ID_Txt.Text + " وكل المعاملات الخاصة بها ... متأكد ", "إلغــاء فاتورة", MessageBoxButtons.OKCancel,
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
            Network_Edit_Tracker_insert("إلغاء الفاتورة", Bill_ID_Txt.Text, 8, 3)
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

        F_IMEX_IM_card = New IMEX_IM_card
        F_IMEX_IM_card.T_ID = T_ID
        F_IMEX_IM_card.ShowDialog()

        'If IM_ID = 0 Then
        '    MsgBox("حددالصنف", MsgBoxStyle.Exclamation)
        '    mySearchControl.txtSearch.Select()
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
        If String.IsNullOrWhiteSpace(Total_TextBox.Text) = False Then Save_Total(T_ID, TOTAL, 0)
    End Sub

    'Private Sub ClearCatFields()
    '    mySearchControl.Clear_txt()
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
        sqlComm.Parameters.AddWithValue("@BsType_ID", 8)
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters("@IMEX_ID").Direction = ParameterDirection.Output
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then
            '  Me.Bill_ID_Txt.Text = sqlComm.Parameters("@IMEX_ID").Value.ToString()
            T_ID = sqlComm.Parameters("@T_ID").Value.ToString()
            'Switch_Dependcy(0)
            Select_ExpBill(T_ID)
        End If

    End Sub

    'Private Sub Insert_Cat()

    '    If String.IsNullOrWhiteSpace(Barcode_IM) Then Barcode_IM = SELECT_BARCODE(IM_ID, U_ID)
    '    Dim sqlComm As New SqlClient.SqlCommand()
    '    sqlComm.CommandText = "IMEX_Details_Insert"
    '    sqlComm.CommandType = CommandType.StoredProcedure
    '    sqlComm.Parameters.AddWithValue("@IMEX_T_ID", T_ID)
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
    '        Network_Edit_Tracker_insert(" الصنف:" & mySearchControl.txtSearch.Text & " الوحدة:" & IM_Unit_cm.Text & " العدد:" & QtyTextBox.Text & " التكلفة:" & IM_Cost_txt.Text, Bill_ID_Txt.Text, 8, 1)
    '        Update_Total()
    '        ClearCatFields()
    '        SELECT_EXP_Cats(T_ID)
    '        mySearchControl.txtSearch.Select()
    '    End If

    'End Sub


    Public Sub SELECT_EXP_Cats(T_ID As Integer)
        Bill_DT.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "IMEX_Details_SELECT_Bill"
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
        sqlComm.CommandText = "IMEX_Details_Delete"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
        'sqlComm.Parameters.AddWithValue("@On_Update", On_Update)
        If SQL_SP_EXEC(sqlComm) = True Then
            Network_Edit_Tracker_insert(" الصنف:" & AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString & " الوحدة:" & AGMetroGrid.CurrentRow.Cells("IMUnit_CL").Value.ToString &
               " العدد:" & AGMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString & " التكلفة:" & AGMetroGrid.CurrentRow.Cells("Price_CL").Value.ToString, Bill_ID_Txt.Text, 8, 2)
            SELECT_EXP_Cats(T_ID)
            Update_Total()
            If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index - 1).Cells("EX_Name_CL")
            '  mySearchControl.txtSearch.Select()
        End If

    End Sub

    'Private Sub IM_Cost_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_Cost_txt.KeyDown
    '    Select Case e.KeyCode
    '        Case Keys.Up : mySearchControl.txtSearch.Select()
    '        Case Keys.Right : QtyTextBox.Select()
    '    End Select
    'End Sub


    'Private Sub QtyTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles QtyTextBox.KeyDown

    '    Select Case e.KeyCode
    '        Case Keys.Return : If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)
    '        Case Keys.Left : If Valid_Panel.Visible = True Then
    '                Valid_cm.Select()
    '                Valid_cm.DroppedDown = True
    '            End If
    '        Case Keys.Up : mySearchControl.txtSearch.Select()
    '        Case Keys.Right
    '            IM_Unit_cm.Select()
    '            IM_Unit_cm.DroppedDown = True
    '    End Select

    'End Sub

    'Private Sub QtyTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QtyTextBox.KeyPress
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub QtyTextBox_TextChanged(sender As Object, e As EventArgs) Handles QtyTextBox.TextChanged
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
        FormType = 3
        PchSearch.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub

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

    'Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles IM_Unit_cm.SelectedValueChanged
    '    If String.IsNullOrWhiteSpace(Current_QTY.Text) = False And Get_Unit = True Then
    '        IM_Fetch_QTY()
    '        If Valid_Panel.Visible = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
    '    End If
    'End Sub

    'Private Sub IM_Unit_cm_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_Unit_cm.KeyDown
    '    Select Case e.KeyCode
    '        Case Keys.Return, Keys.Left : QtyTextBox.Select()
    '    End Select
    'End Sub


    'Private Sub ST_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles ST_cm.SelectedValueChanged
    '    If Get_Unit = True Then
    '        Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
    '        IM_Fetch_QTY()
    '        If Valid_Panel.Visible = True Then Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
    '    End If
    'End Sub

    'Private Sub Valid_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles Valid_cm.SelectedValueChanged
    '    If Get_Unit = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
    'End Sub

    '---------------------------------------
    Dim Tmp_Bill_ID As Integer
    Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
        If Not String.IsNullOrWhiteSpace(Bill_ID_Txt.Text) Then
            Tmp_Bill_ID = Bill_ID
            Bill_ID_Txt.Text = Bill_ID - 1
            Get_T_ID()
        End If
    End Sub

    Public Sub Get_T_ID()

        Dim C As New C
        Dim S As String = "Select T_ID From Agents_Balance_MV Where IMEX_ID = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "'"
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
        FormType = 3
        Switch_To_DV_Show()
    End Sub

    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        IMTranPrintData()
    End Sub

    Public Sub IMTranPrintData()

        Try
            Dim pp As New ReportConnection

            pp.rp.Load(Application.StartupPath & "\reports\IMEX_Bill.rpt")

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

                .rp.SetParameterValue(6, "  رقم الفاتورة :  " + Bill_ID_Txt.Text)
                .rp.SetParameterValue(7, "  العنوان :  " + Title_txt.Text)
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

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If IM_ID > 0 Then
            Show_IM_Details.IM_ID = IM_ID
            Show_IM_Details.ShowDialog()
        End If
    End Sub

    Private Sub AG_Cm_ID_Changed(sender As Object, e As EventArgs) Handles AG_Cm.ID_Changed
        If AG_Cm.TXT_ID.Text > 0 Then Save_AG_Name(T_ID, AG_ID, On_Update)
    End Sub
End Class