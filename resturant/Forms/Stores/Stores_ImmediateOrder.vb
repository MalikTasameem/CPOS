Imports System.Data.SqlClient

Public Class Stores_ImmediateOrder

    Dim rs As New Resizer
    '  Dim IM_Dt As New DataTable
    Public FormActive As Boolean = True
    '  Dim IM_ID As Integer = 0
    '  Dim U_Dt As New DataTable
    '  Dim Get_Unit As Boolean = False
    ' Dim IM_QTY As Double = 0

    '   Dim ALL_QTY As Double = 0
    '  Dim U_Cargo As Double = 0
    '  Dim Valid_Dt As New DataTable
    Dim Bill_DT As New DataTable

    Public T_ID As Integer
    Public isDepended As Boolean
    Public isVoid As Boolean

    Dim EditState As String = ""
    '  Dim U_ID As Integer
    Dim On_Update As Boolean

    ' Public Barcode_IM As String = ""
    Public Bill_ID As Integer = 0

    Private Sub Stores_ImmediateOrder_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        FormType = 0
        Me.Dispose()
    End Sub

    Private Sub Stores_ImmediateOrder_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                If NewButton.Enabled = True Then NewButton_Click(sender, e)
            Case Keys.F12
                If Save_butt.Enabled = True Then Save_butt_Click(sender, e)
            Case Keys.F3
                If Print_btn.Enabled = True Then Print_btn_Click(sender, e)
            Case Keys.F4
                If Delete_butt.Enabled = True Then Delete_butt_Click(sender, e)
            'Case Keys.F5
            '    IM_SH_txt.Select()
            'Case Keys.F8
            '    Barcode_SH_txt.Select()
            Case Keys.PageUp
                Up_Bill_btn_Click(sender, e)
            Case Keys.PageDown
                Down_Bill_btn_Click(sender, e)
        End Select
    End Sub

    Private Sub Stores_ImmediateOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '   If St_Count() = 1 Then All_St_Panel.Visible = False
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        FormType = 7
        EditState = Edit_butt.Text
        Check_View_Control()
        Disable_Tools()
        Load_ST()
        Get_Last_T_ID()
        If isShowing_Trans = True Then
            Select_ExpBill(T_ID_Trans)
            SelectStateBt()
            NewButton.Enabled = False
            SearchButton.Enabled = False
        End If

        'If My_Settings.S_Default = 0 Then
        '    Barcode_SH_txt.Select()
        'Else
        '    IM_SH_txt.Select()
        'End If
    End Sub

    Public Sub Get_Last_T_ID()

        Dim C As New C
        Dim S As String = "Select Top 1 T_ID From Agents_Balance_MV_St_Trans Where User_ID = '" & USER_ID & "' AND BsType_ID = 12 AND isDepended = 0 AND isVoid = 0 AND T_ID BETWEEN " & START_ID & " AND " & END_ID & " ORDER BY T_ID DESC"
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

    Private Sub Disable_CatFields()
        'IM_SH_txt.Enabled = False
        'Show_IM_btn.Enabled = False
        'Barcode_SH_txt.Enabled = False
        'QTY_txt.Enabled = False
        'PriceTextBox.Enabled = False
        ADDCatButton.Enabled = False
        RemoveCatButton.Enabled = False
    End Sub

    Private Sub Enable_CatFields()
        'IM_SH_txt.Enabled = True
        'Show_IM_btn.Enabled = True
        'Barcode_SH_txt.Enabled = True
        'QTY_txt.Enabled = True
        'PriceTextBox.Enabled = True
        ADDCatButton.Enabled = True
        RemoveCatButton.Enabled = True
    End Sub

    Private Sub ClearFields()
        T_ID = 0
        ST_cm.SelectedIndex = 0
        ST_To_cm.SelectedIndex = 0
        Notes_txt.Clear()
        'PriceTextBox.Clear()
        Bill_DT.Clear()
        DateTimeEx.Text = Date.Now
        Edit_butt.Text = EditState
        isVoid = False
        User_Name_lb.Text = "---"
        Edit_butt.BackColor = Color.White
    End Sub

    Public Sub Check_View_Control()
        AGMetroGrid.Columns("D_Valid_CL").Visible = My_Settings.S_D_Valid_CL
        AGMetroGrid.Columns("IMUnit_CL").Visible = My_Settings.S_IMUnit_CL
        AGMetroGrid.Columns("Price_CL").Visible = My_Settings.S_Price_CL
        AGMetroGrid.Columns("Total_CL").Visible = My_Settings.S_Total_CL
        AGMetroGrid.Columns("IMNUM_CL").Visible = MY_Settings.S_IMNUM_CL


        AGMetroGrid.Columns("Price_CL").Visible = U_SB_Show_IM_COST
        AGMetroGrid.Columns("Total_CL").Visible = U_SB_Show_IM_COST
        ' COST_Panel.Visible = U_SB_Show_IM_COST
        Pure_txt.Visible = U_SB_Show_IM_COST
    End Sub


    Public Sub SELECT_MAX()
        Dim c As New C
        Try
            Dim s As String
            s = "SELECT ISNULL(MAX(ST_Tran_ID),0) + 1 AS N FROM Agents_Balance_MV "
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

    Public Sub SelectStateBt()
        Disable_Tools()
        If isVoid = True Then
            VoidLb.Visible = True
            Save_butt.Enabled = False
            Edit_butt.Enabled = False
            Edit_butt.Text = EditState
            Delete_butt.Enabled = False
            AGMetroGrid.Enabled = True
            AGMetroGrid.BackgroundColor = Color.IndianRed
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.IndianRed
            Print_btn.Enabled = False
            'is_Recived_LB.Visible = False

        Else
            If isDepended = False Then
                Save_butt.Enabled = True
                Edit_butt.Enabled = True
                Print_btn.Enabled = False
                Enable_Tools()
                Switch_Dependcy(0)
                Delete_butt.Enabled = True
                '  AGMetroGrid.BackgroundColor = Color.Gray
                '  AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.Gray
            Else
                Switch_Dependcy(1)
                Edit_butt.Enabled = True
                Print_btn.Enabled = True
                Delete_butt.Enabled = True
                'is_Recived_LB.Visible = True
                'If Check_IS_Done() = 1 Then
                '    is_Recived_LB.BackColor = Color.DarkSeaGreen
                '    is_Recived_LB.Text = "تم إستلام البضاعة"
                '    Edit_butt.Enabled = False
                '    Delete_butt.Enabled = False
                'Else
                '    is_Recived_LB.BackColor = Color.LightYellow
                '    is_Recived_LB.Text = "في إنتظار الإستلام"
                '    Delete_butt.Enabled = True
                'End If

            End If

            VoidLb.Visible = False
            Edit_butt.Text = EditState

        End If

        Me.Text = "عرض بيانات فاتورة"

    End Sub

    Private Function Check_IS_Done()
        Dim C As New C
        Dim S As String = "Select TRAN_ID From ST_TRAN_DONE Where TRAN_ID = '" & T_ID & "'"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                Return 1
            Else
                Return 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

        Return 0
    End Function


    Public Sub Load_ST()
        Dim c As New C
        Dim DT_2 As New DataTable
        Try
            Dim s As String
            s = "select ST_ID,ST_name from STORES ORDER By ST_ID ASC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            c.Da.Fill(DT_2)
            ST_cm.DataSource = c.Dt
            ST_cm.DisplayMember = "ST_name"
            ST_cm.ValueMember = "ST_ID"
            ST_cm.SelectedValue = PCH_ST_ID

            ST_To_cm.DataSource = DT_2
            ST_To_cm.DisplayMember = "ST_name"
            ST_To_cm.ValueMember = "ST_ID"
            ST_To_cm.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Enable_Tools()
        DateTimeEx.Enabled = True
        Save_butt.Enabled = True
        'Barcode_SH_txt.Enabled = True
        'IM_SH_txt.Enabled = True
        Notes_txt.Enabled = True
        Enable_CatFields()
    End Sub

    Private Sub Disable_Tools()
        DateTimeEx.Enabled = False
        'Barcode_SH_txt.Enabled = False
        'IM_SH_txt.Enabled = False
        Disable_CatFields()
        Notes_txt.Enabled = False
    End Sub


    'Private Sub Fetch_ItemToList()
    '    If IMDataGridViewX.Rows.Count > 0 Then
    '        IM_ID = IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value
    '        IM_SH_txt.Text = IMDataGridViewX.CurrentRow.Cells("item_name_CL").Value
    '        IM_SH_txt.BackColor = Color.LightGoldenrodYellow
    '        Get_Unit = False
    '        Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
    '        Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
    '        Fetch_IM_Units()
    '        IMDataGridViewX.Visible = False
    '        QTY_txt.Select()

    '        If IMDataGridViewX.CurrentRow.Cells("isValid_CL").Value = 1 Then
    '            Valid_Panel.Visible = True
    '            Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
    '        Else
    '            Valid_Panel.Visible = False
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

    'Private Sub IM_SH_txt_KeyDown(sender As Object, e As KeyEventArgs)
    '    If e.KeyCode = Keys.Down Then
    '        If IMDataGridViewX.Visible = True Then
    '            IMDataGridViewX.Select()
    '        Else
    '            QTY_txt.Select()
    '        End If
    '    End If

    '    If e.KeyCode = Keys.Return Then
    '        If IMDataGridViewX.Visible = True Then
    '            Fetch_ItemToList()
    '        Else
    '            QTY_txt.Select()
    '        End If
    '    End If
    '    If e.KeyCode = Keys.Delete Then IM_SH_txt.Clear()
    'End Sub

    'Private Sub IM_SH_txt_TextChanged(sender As Object, e As EventArgs)
    '    If IM_SH_txt.Text.Count > 0 Then
    '        Load_IM()
    '    Else
    '        IMDataGridViewX.Visible = False
    '        IM_ID = 0
    '        Current_QTY.Clear()
    '        U_Dt.Clear()
    '        Valid_Dt.Clear()
    '        ALL_QTY_txt.Clear()
    '        PriceTextBox.Clear()
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

    '            QTY_txt.Select()
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

    Private Sub NewButton_Click(sender As Object, e As EventArgs) Handles NewButton.Click
        Call_New_Bill()
    End Sub

    Private Sub Call_New_Bill()
        If T_ID > 0 Then
            If MessageBox.Show("فتح فاتورة جديدة", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information,
                   MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                Start_New_Bill()
            End If

        Else
            Start_New_Bill()
        End If
    End Sub

    Private Sub Start_New_Bill()
        Bill_DT.Clear()
        ClearFields()
        ' Clear_IM_Fields()
        Insert_NewBill()
        Enable_Tools()
        'IM_SH_txt.Select()
        'If MY_Settings.S_Default = 0 Then
        '    Barcode_SH_txt.Select()
        'Else
        '    IM_SH_txt.Select()
        'End If
        On_Update = False
    End Sub


    Private Sub Insert_NewBill()

        Dim sqlComm As New SqlClient.SqlCommand()

        sqlComm.CommandText = "[Agents_Balance_MV_St_Trans_insert]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", 0)
        sqlComm.Parameters.AddWithValue("@Date", Me.DateTimeEx.Value)
        sqlComm.Parameters.AddWithValue("@BsType_ID", 12)
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output
        If SQL_SP_EXEC(sqlComm) = True Then
            T_ID = sqlComm.Parameters("@T_ID").Value.ToString()
            Select_ExpBill(T_ID)
        End If

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


    'Private Sub INSERT_IM()

    '    If String.IsNullOrWhiteSpace(QTY_txt.Text) Then QTY_txt.Text = "1"
    '    If ST_cm.SelectedValue = ST_To_cm.SelectedValue Then
    '        MsgBox("لا يمكن نقل في نفس المخزن", MsgBoxStyle.Critical, "خطــأ")
    '    Else
    '        If String.IsNullOrWhiteSpace(Barcode_IM) Then Barcode_IM = SELECT_BARCODE(IM_ID, U_ID)
    '        'Dim Row_Index As Integer = 0
    '        'If AGMetroGrid.Rows.Count > 0 Then Row_Index = AGMetroGrid.CurrentCell.RowIndex + 1
    '        Dim c As New C
    '        With (c.Com)
    '            .Connection = c.Con
    '            .CommandText = "ST_Trans_Details_insert"
    '            .CommandType = CommandType.StoredProcedure

    '            .Parameters.AddWithValue("@B_T_ID", T_ID)
    '            .Parameters.AddWithValue("@IM_ID", IM_ID)
    '            .Parameters.AddWithValue("@U_ID", U_ID)
    '            .Parameters.AddWithValue("@D_Vaild", Valid_cm.Text)
    '            .Parameters.AddWithValue("@QTY", QTY_txt.Text)
    '            .Parameters.AddWithValue("@Price", PriceTextBox.Text)
    '            .Parameters.AddWithValue("@ST_F_ID", ST_cm.SelectedValue)
    '            .Parameters.AddWithValue("@ST_To_ID", ST_To_cm.SelectedValue)
    '            .Parameters.AddWithValue("@On_Update", On_Update)
    '            .Parameters.AddWithValue("@Barcode", Barcode_IM)
    '        End With

    '        If SQL_SP_EXEC(c.Com) = True Then
    '            Network_Edit_Tracker_insert(" الصنف:" & IM_SH_txt.Text & " الوحدة:" & IM_Unit_cm.Text & " العدد:" & QTY_txt.Text & " السعر:" & PriceTextBox.Text & " من مخزن:" & ST_cm.Text & " إلى مخزن:" & ST_To_cm.Text, Bill_ID_Txt.Text, 12, 1)
    '            Clear_IM_Fields()
    '            St_Trans_Contents_SELECT_Bill()
    '            'If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index).Cells("EX_Name_CL")
    '            If MY_Settings.S_Default = 0 Then
    '                Barcode_SH_txt.Select()
    '            Else
    '                IM_SH_txt.Select()
    '            End If
    '        End If

    '    End If

    'End Sub

    Public Sub St_Trans_Contents_SELECT_Bill()
        Bill_DT.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "St_Trans_Details_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Bill_T_ID", Me.T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(Bill_DT)
        AGMetroGrid.DataSource = Bill_DT
        If AGMetroGrid.Rows.Count > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(AGMetroGrid.Rows.Count - 1).Cells("EX_Name_CL")
    End Sub

    'Private Sub Clear_IM_Fields()
    '    U_Dt.Clear()
    '    IM_SH_txt.Clear()
    '    QTY_txt.Clear()
    '    Current_QTY.Clear()
    '    Barcode_SH_txt.Clear()
    '    Barcode_IM = ""
    'End Sub

    'Private Sub IMDataGridViewX_CellClick(sender As Object, e As DataGridViewCellEventArgs)
    '    Fetch_ItemToList()
    'End Sub


    'Private Sub Barcode_SH_txt_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return : If String.IsNullOrWhiteSpace(Barcode_SH_txt.Text) = False Then Load_IM_Barcode()
    '        Case Keys.Down : QTY_txt.Select()
    '        Case Keys.Delete
    '            Barcode_SH_txt.Clear()
    '            Barcode_IM = ""
    '    End Select
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
    '        s = "select IM_ID,item_name,isValid,IM_NUM from IM_All_V  Order by item_name ASC"
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

    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click

        If AGMetroGrid.Rows.Count > 0 Then
            Save_About_ST_TRAN(T_ID, Notes_txt.Text)
            DependingBill(T_ID)
            Save_Date_ST_TRAN(T_ID, DateTimeEx)
            Switch_Dependcy(1)
            SelectStateBt()
        End If

    End Sub

    Private Sub Save_Date_ST_TRAN(T_ID As Integer, DateTime As DateTimePicker)

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "[AG_Balance_St_Trans_Update_Date]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@Date", DateTime.Value)
        SQL_SP_EXEC(sqlComm)

    End Sub


    Private Sub DependingBill(T_ID As Integer)
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "[AG_Balance_St_Trans_Update_isDepended]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        SQL_SP_EXEC(sqlComm)
    End Sub

    'Private Sub QTY_txt_KeyDown(sender As Object, e As KeyEventArgs)
    '    If e.KeyCode = Keys.Return Then ADDCatButton_Click(sender, e)
    '    If e.KeyCode = Keys.Right Then
    '        IM_Unit_cm.DroppedDown = True
    '        IM_Unit_cm.Select()
    '    End If
    '    If e.KeyCode = Keys.Left Then
    '        If Valid_Panel.Visible = True Then
    '            Valid_cm.DroppedDown = True
    '            Valid_cm.Select()
    '        End If
    '    End If
    '    If e.KeyCode = Keys.Up Then IM_SH_txt.Select()
    'End Sub

    'Private Sub QTY_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub QTY_txt_TextChanged(sender As Object, e As EventArgs)
    '    QTY_Error.Clear()
    '    Check_Point_in_FloatNum(sender, e)
    'End Sub

    'Private Sub IMDataGridViewX_KeyDown(sender As Object, e As KeyEventArgs)
    '    If e.KeyCode = Keys.Return Then Fetch_ItemToList()
    '    If e.KeyCode = Keys.Up Then If IMDataGridViewX.CurrentRow.Index = 0 Then IM_SH_txt.Select()
    'End Sub

    'Private Sub QTY_txt_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
    '    If String.IsNullOrWhiteSpace(QTY_txt.Text) Then
    '        QTY_Error.SetError(QTY_txt, " حددالكمية ")
    '        e.Cancel = True
    '    Else
    '        QTY_Error.Clear()
    '        e.Cancel = False
    '    End If
    'End Sub

    'Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs)
    '    If String.IsNullOrWhiteSpace(Current_QTY.Text) = False And Get_Unit = True Then
    '        IM_Fetch_QTY()
    '        If Valid_Panel.Visible = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
    '    End If
    'End Sub

    'Private Sub ST_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles ST_cm.SelectedValueChanged
    '    If Get_Unit = True Then
    '        Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
    '        IM_Fetch_QTY()
    '        If Valid_Panel.Visible = True Then Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
    '        Valid_Dt.Clear()
    '        If Get_Unit = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
    '    End If
    'End Sub

    'Private Sub Valid_cm_SelectedValueChanged(sender As Object, e As EventArgs)
    '    If Get_Unit = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
    'End Sub

    'Private Sub IM_Fetch_QTY()
    '    Dim c As New C
    '    Try
    '        Dim s As String
    '        s = "select U_ID,U_Cargo,Price,Cost from IM_Menu_Units_V WHERE U_IM_ID = '" & IM_Unit_cm.SelectedValue & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            U_Cargo = c.Dr("U_Cargo")
    '            Dim N As Double = (Convert.ToDouble(IM_QTY) / c.Dr("U_Cargo"))
    '            Current_QTY.Text = N.ToString("N")
    '            PriceTextBox.Text = c.Dr("Cost") * U_Cargo
    '            ALL_QTY_txt.Text = ALL_QTY / U_Cargo
    '            U_ID = c.Dr("U_ID")
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub


    Private Sub Void_Bill()
        Dim c As New C
        With (c.Com)
            .Connection = c.Con
            .CommandText = "[AG_Balance_St_Trans_Update_isVoid]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            isVoid = True
            SelectStateBt()
        End If

    End Sub

    Private Sub ADDCatButton_Click(sender As Object, e As EventArgs) Handles ADDCatButton.Click

        F_Stores_Trans_IM_card = New Stores_Trans_IM_card
        F_Stores_Trans_IM_card.T_ID = T_ID
        F_Stores_Trans_IM_card.ShowDialog()

        'If IM_min_QTY = False Then
        '    If IM_Check_Neg_QTY_() = 1 Then
        '        MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
        '        Exit Sub
        '    End If
        'End If

        'If AGMetroGrid.RowCount > 0 Then
        '    If AGMetroGrid.CurrentRow.Cells("ST_F_ID_CL").Value = ST_cm.SelectedValue And AGMetroGrid.CurrentRow.Cells("ST_To_ID_CL").Value = ST_To_cm.SelectedValue Then
        '        INSERT_IM()
        '    Else
        '        MsgBox("يوجد إختلاف في المخازن التي تم اختيارها للتحويل", MsgBoxStyle.Critical, "خطأ فالإدخال")
        '    End If
        'Else
        '    INSERT_IM()
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
    '        .Parameters.AddWithValue("@Enterd_Qty", QTY_txt.Text)
    '        .Parameters.AddWithValue("@Cargo", U_Cargo)

    '        .Parameters("@F").Direction = ParameterDirection.Output
    '        If SQL_SP_EXEC(C.Com) Then
    '            F = .Parameters("@F").Value
    '        End If
    '    End With

    '    Return F
    'End Function

    Private Sub RemoveCatButton_Click(sender As Object, e As EventArgs) Handles RemoveCatButton.Click
        If AGMetroGrid.Rows.Count > 0 Then
            If MessageBox.Show(" حذف الصنف " + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                Delete_Cat()
            End If
        End If
    End Sub

    Private Sub Delete_Cat()
        Try

            Dim Row_Index As Integer = AGMetroGrid.CurrentCell.RowIndex
            Dim sqlComm As New SqlClient.SqlCommand
            sqlComm.CommandText = "ST_Trans_Details_Delete"
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.AddWithValue("@T_ID", AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
            If SQL_SP_EXEC(sqlComm) = True Then
                Network_Edit_Tracker_insert(" الصنف:" & AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString & " الوحدة:" & AGMetroGrid.CurrentRow.Cells("IMUnit_CL").Value.ToString & " العدد:" & AGMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString _
                                            & " السعر:" & AGMetroGrid.CurrentRow.Cells("Price_CL").Value.ToString & " من مخزن:" & AGMetroGrid.CurrentRow.Cells("ST_Name_CL").Value.ToString _
                                            & " إلى مخزن:" & AGMetroGrid.CurrentRow.Cells("ST_To_CL").Value.ToString, Bill_ID_Txt.Text, 12, 2)
                St_Trans_Contents_SELECT_Bill()
                If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index - 1).Cells("EX_Name_CL")
                'If MY_Settings.S_Default = 0 Then
                '    Barcode_SH_txt.Select()
                'Else
                '    IM_SH_txt.Select()
                'End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        Me.Cursor = Cursors.AppStarting
        FormType = 7
        PchSearch.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Delete_butt_Click(sender As Object, e As EventArgs) Handles Delete_butt.Click
        If AGMetroGrid.Rows.Count > 0 Then
            If IM_min_QTY = False Then
                If Check_IS_Done() = 1 Then
                    If IM_Check_Neg_QTY_For_Cancel_ST_CONVERT() = 1 Then
                        MsgBox(" لا يمكن سحب كمية بالسالب للصنف  " & Str_Name, MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                End If
            End If
        End If


        Beep()
        If MessageBox.Show(" سيتم إلغاء الفاتورة رقم " + Bill_ID_Txt.Text + " وكل المعاملات الخاصة بها ... متأكد ", "إلغــاء فاتورة", MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
            Void_Bill()
        End If

    End Sub

    Private Function IM_Check_Neg_QTY_For_Cancel_ST_CONVERT()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_For_Cancel_ST_CONVERT"
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
        Dim S As String = "Select T_ID From Agents_Balance_MV_St_Trans Where St_Tran_ID = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "'"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                ' Clear_IM_Fields()
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

    Private Sub AGMetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles AGMetroGrid.RowsAdded
        Calc_Total()
    End Sub

    Private Sub AGMetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles AGMetroGrid.RowsRemoved
        Calc_Total()
    End Sub

    Private Sub Calc_Total()
        Dim TOTAL As Double = 0
        Dim TOTAL_SP As Double = 0
        Dim QTY As Double = 0
        For i = 0 To AGMetroGrid.Rows.Count - 1
            TOTAL = TOTAL + AGMetroGrid.Rows(i).Cells("Total_CL").Value
            TOTAL_SP = TOTAL_SP + (AGMetroGrid.Rows(i).Cells("SPrice_CL").Value * AGMetroGrid.Rows(i).Cells("QTY_CL").Value)
            QTY += AGMetroGrid.Rows(i).Cells("QTY_CL").Value
        Next

        Pure_txt.Text = TOTAL.ToString("N")
        Pure_SP_txt.Text = TOTAL_SP.ToString("N")

        IM_Count_LB.Text = AGMetroGrid.Rows.Count.ToString + " : مواد "
        IM_Qty_LB.Text = QTY.ToString + " : كميات "
    End Sub

    Private Sub DGV_Control_btn_Click(sender As Object, e As EventArgs) Handles DGV_Control_btn.Click
        FormType = 7
        Switch_To_DV_Show()
    End Sub

    'Private Sub Valid_cm_KeyDown(sender As Object, e As KeyEventArgs) Handles Valid_cm.KeyDown
    '    Select Case e.KeyCode
    '        Case Keys.Return : INSERT_IM()
    '        Case Keys.Right : QTY_txt.Select()
    '    End Select
    'End Sub

    'Private Sub IM_Unit_cm_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return, Keys.Left : QTY_txt.Select()
    '    End Select
    'End Sub

    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        If Edit_butt.Text = EditState Then
            MsgBox(" سيتم تعديل الفاتورة بشكل مباشر مع كل تغير ", MsgBoxStyle.Information, "تعديل فاتورة")
            On_Update = True
            Edit_butt.Text = "إيقاف التعديل"
            Enable_CatFields()
            AGMetroGrid.BackgroundColor = Color.LightYellow
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
            Notes_txt.Enabled = True
            DateTimeEx.Enabled = True
            Edit_butt.BackColor = Color.GreenYellow
            'If MY_Settings.S_Default = 0 Then
            '    Barcode_SH_txt.Select()
            'Else
            '    IM_SH_txt.Select()
            'End If
        Else
            On_Update = False
            Save_About_ST_TRAN(T_ID, Notes_txt.Text)
            Save_Date_ST_TRAN(T_ID, DateTimeEx)
            Edit_butt.Text = EditState
            Disable_CatFields()
            Edit_butt.BackColor = Color.White
            SelectStateBt()
        End If
    End Sub

    Private Sub Save_About_ST_TRAN(T_ID As Integer, Notes As String)
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "[AG_Balance_St_Trans_Update_About]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        If String.IsNullOrWhiteSpace(Notes) = False Then sqlComm.Parameters.AddWithValue("@About", Notes)
        SQL_SP_EXEC(sqlComm)
    End Sub

    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        IMTranPrintData()
    End Sub

    Public Sub IMTranPrintData()

        Try
            Dim pp As New ReportConnection

            If U_SB_Show_IM_COST = True Then
                pp.rp.Load(Application.StartupPath & "\reports\TranStore_Bill.rpt")
            Else
                pp.rp.Load(Application.StartupPath & "\reports\TranStore_Bill_NO_Cost.rpt")

            End If
            '  pp.rp.Load(Application.StartupPath & "\reports\TranStore_Bill.rpt")

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
                .rp.SetParameterValue(5, "  رقم الفاتورة :  " + Bill_ID_Txt.Text)
                .rp.SetParameterValue(6, Pure_txt.Text)
                .rp.SetParameterValue(7, Pure_SP_txt.Text)

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
    '    IMDataGridViewX.Columns("IM_NUM_CL").Visible = Sh_ByNum_CB.Checked
    '    Barcode_SH_txt.Select()
    'End Sub

    Private Sub Stores_ImmediateOrder_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

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

    'Private Sub ALL_QTY_txt_TextChanged(sender As Object, e As EventArgs)
    '    If Not String.IsNullOrWhiteSpace(ALL_QTY_txt.Text) Then
    '        Dim N As Double = ALL_QTY_txt.Text
    '        ALL_QTY_txt.Text = N.ToString("N")
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

End Class