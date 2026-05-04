Public Class ST_settlement : Inherits System.Windows.Forms.Form
    Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Public T_ID As Integer
    Public isDepended As Boolean
    Public isVoid As Boolean
    Public Receipts_DT As New DataTable

    Public isCashReceipt_Success As Boolean = False
    Public isShowingDetails As Boolean = False

    Public IM_ID As Integer = 0
    Dim IM_Dt As New DataTable
    Dim IM_QTY As Double = 0
    Dim IM_Cost As Double = 0
    Dim U_Cargo As Double = 1
    Public TOTAL As Double = 0
    '   Public AG_ID As Integer = 0
    Dim AG_Dt As New DataTable
    Dim U_Dt As New DataTable
    Dim Get_Unit As Boolean = False

    Public Bill_DT As New DataTable
    Dim ALL_QTY As Double = 0
    Dim U_ID As Integer
    Public On_Update As Boolean
    Public Barcode_IM As String = ""
    Public Pch_ID As Integer = 0

    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If On_Update = True Then Edit_butt_Click(sender, e)
        FormType = 0
        Me.Dispose()
    End Sub

    Private Sub Expenses_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
        If e.KeyCode = Keys.F1 Then If New_butt.Enabled = True Then New_butt_Click(sender, e)
        If e.KeyCode = Keys.F12 Then If Save_butt.Enabled = True Then Save_butt_Click(sender, e)
        If e.KeyCode = Keys.F2 Then If Print_btn.Enabled = True Then Print_btn_Click(sender, e)
        If e.KeyCode = Keys.F3 Then If Edit_butt.Enabled = True And Edit_butt.Visible = True Then If Edit_butt.Text = EditState Then Edit_butt_Click(sender, e)
        If e.KeyCode = Keys.F4 Then If Delete_butt.Enabled = True And Delete_butt.Visible = True Then Delete_butt_Click(sender, e)
        'If e.KeyCode = Keys.F5 Then IM_SH_txt.Select()
        'If e.KeyCode = Keys.F8 Then Barcode_SH_txt.Select()
    End Sub

    'Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    '  If St_Count() = 1 Then All_St_Panel.Visible = False
    '    FormType = 4
    '    Check_View_Control()
    '    rs.FindAllControls(Me)
    '    Me.WindowState = FormWindowState.Maximized

    '    EditState = Edit_butt.Text
    '    DefaultFormState = Me.Text
    '    Disable_Fields()
    '    'Load_ST()

    '    Get_Last_T_ID()

    '    If isShowing_Trans = True Then
    '        Select_ExpBill(T_ID_Trans)
    '        SelectStateBt()
    '        New_butt.Enabled = False
    '        SearchButton.Enabled = False
    '    End If

    '    'If My_Settings.S_Default = 0 Then
    '    '    Barcode_SH_txt.Select()
    '    'Else
    '    '    IM_SH_txt.Select()
    '    'End If
    '    Make_Hints()
    'End Sub
    Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' =========================================================
            ' 🌟 1. تعيين التاجات للأدوات الأصلية الموجودة في الفورم فقط
            ' =========================================================
            If ExitFormButton IsNot Nothing Then ExitFormButton.Tag = "DELETE"
            'If Label16 IsNot Nothing Then Label16.Tag = "TITLE_TRANSPARENT" ' عنوان الفورم
            If DeletedBillLabel IsNot Nothing Then DeletedBillLabel.Tag = "TITLE_TRANSPARENT"

            ' حاويات التنظيم الأصلية الموجودة في كودك (لجعل خلفيتها تتبع الثيم)
            If Panel1 IsNot Nothing Then Panel1.Tag = "TRANSPARENT"
            If Panel2 IsNot Nothing Then Panel2.Tag = "TRANSPARENT"
            If Panel3 IsNot Nothing Then Panel3.Tag = "TRANSPARENT"
            If Panel9 IsNot Nothing Then Panel9.Tag = "TRANSPARENT"
            'If Panel13 IsNot Nothing Then Panel13.Tag = "TRANSPARENT"

            ' أزرار الإجراءات
            If New_butt IsNot Nothing Then New_butt.Tag = "GENERAL"
            If Save_butt IsNot Nothing Then Save_butt.Tag = "SAVE"
            If Edit_butt IsNot Nothing Then Edit_butt.Tag = "GENERAL"
            If Delete_butt IsNot Nothing Then Delete_butt.Tag = "DELETE"
            If Print_btn IsNot Nothing Then Print_btn.Tag = "PRINT"
            If SearchButton IsNot Nothing Then SearchButton.Tag = "GENERAL"

            ' أزرار البحث والتنقل
            If Up_Bill_btn IsNot Nothing Then Up_Bill_btn.Tag = "GENERAL"
            If Down_Bill_btn IsNot Nothing Then Down_Bill_btn.Tag = "GENERAL"
            If ClearSearch_btn IsNot Nothing Then ClearSearch_btn.Tag = "GENERAL"

            ' أزرار الأدوات الجانبية
            If ADDCatButton IsNot Nothing Then ADDCatButton.Tag = "GENERAL"
            If RemoveCatButton IsNot Nothing Then RemoveCatButton.Tag = "DELETE"
            If IM_btn IsNot Nothing Then IM_btn.Tag = "GENERAL"
            If MakeBarcode_btn IsNot Nothing Then MakeBarcode_btn.Tag = "GENERAL"
            If DGV_Control_btn IsNot Nothing Then DGV_Control_btn.Tag = "GENERAL"

            ' =========================================================
            ' 🌟 2. تطبيق الثيم
            ' =========================================================
            ThemeManager.ApplyThemeToForm(Me)

        Catch ex As Exception
        End Try

        ' =========================================================
        ' 🌟 3. الكود الأصلي للتحميل الخاص بك
        ' =========================================================
        '  If St_Count() = 1 Then All_St_Panel.Visible = False
        FormType = 14
        Check_View_Control()
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized

        EditState = Edit_butt.Text
        DefaultFormState = Me.Text
        Disable_Fields()
        'Load_ST()

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
        Make_Hints()
    End Sub


    Private Sub Make_Hints()
        SendMessage(Search_txt.Handle, &H1501, 0, "إبحث عن إسم صنف بالفاتورة")
        SendMessage(Barcode_Search_txt.Handle, &H1501, 0, "إبحث عن باركود صنف بالفاتورة")
    End Sub



    Public Sub Get_Last_T_ID()
        Dim C As New C
        Dim S As String = "Select Top 1 T_ID From Agents_Balance_MV Where User_ID = '" & USER_ID & "' AND BsType_ID = 39 AND isDepended = 0 AND isVoid = 0  AND T_ID BETWEEN " & START_ID & " AND " & END_ID & " ORDER BY T_ID DESC"
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
        '  AGMetroGrid.Columns("Notes_CL").Visible = MY_Settings.SP_Notes_CL
        AGMetroGrid.Columns("IMNUM_CL").Visible = MY_Settings.S_IMNUM_CL
        AGMetroGrid.Columns("Barcode_CL").Visible = MY_Settings.S_Barcode_CL

        'Min_SP_Panel.Visible = S_Allow_MinSP
        'Min_SP_Panel_2.Visible = S_Allow_MinSP
    End Sub

    Public Sub SELECT_MAX()
        Dim c As New C
        Try
            Dim s As String
            s = "SELECT ISNULL(MAX(ST_Sett_ID),0) + 1 AS N FROM Agents_Balance_MV "
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
        'Invoice_Type_cm.Enabled = True
        Ebable_CatFields()
    End Sub

    Private Sub Disable_Fields()
        Title_txt.Enabled = False
        DateTimeEx.Enabled = False
        Notes_txt.Enabled = False
        'Invoice_Type_cm.Enabled = False
        Disable_CatFields()
    End Sub

    Private Sub Disable_CatFields()
        'IM_SH_txt.Enabled = False
        'Show_IM_btn.Enabled = False
        'Barcode_SH_txt.Enabled = False
        'QtyTextBox.Enabled = False
        'PriceTextBox.Enabled = False
        ADDCatButton.Enabled = False
        RemoveCatButton.Enabled = False
    End Sub

    Private Sub Ebable_CatFields()
        'IM_SH_txt.Enabled = True
        'Show_IM_btn.Enabled = True
        'Barcode_SH_txt.Enabled = True
        'QtyTextBox.Enabled = True
        'PriceTextBox.Enabled = True
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
        Me.Text = "فاتورة جرد جديدة"
        'If My_Settings.S_Default = 0 Then
        '    Barcode_SH_txt.Select()
        'Else
        '    IM_SH_txt.Select()
        'End If
    End Sub
    'Private Sub DeleteOrUpdateStateBt()
    '    Disable_Fields()
    '    Save_butt.Enabled = False
    '    Edit_butt.Enabled = True
    '    Delete_butt.Enabled = False
    '    Me.Text = DefaultFormState
    'End Sub

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
            Save_butt.Enabled = False
            Edit_butt.Enabled = False
            Edit_butt.Text = EditState
            Delete_butt.Enabled = False
            AGMetroGrid.Enabled = True
            AGMetroGrid.BackgroundColor = Color.IndianRed
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.IndianRed
            Print_btn.Enabled = False
            Disable_Fields()
        Else
            If isDepended = False Then
                Save_butt.Enabled = True
                Edit_butt.Enabled = False
                Print_btn.Enabled = False
                ' AGMetroGrid.BackgroundColor = Color.Gray
                ' AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.Gray
                Delete_butt.Enabled = False
                Enable_Fields()
            Else
                Edit_butt.Enabled = True
                Print_btn.Enabled = True
                Delete_butt.Enabled = True
                DeletedBillLabel.Visible = False
                Edit_butt.Text = EditState
                Disable_Fields()
                AGMetroGrid.BackgroundColor = Color.LightGreen
                AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
            End If

        End If
        Me.Text = "عرض بيانات فاتورة"

    End Sub

    Private Sub ClearFields()
        isCashReceipt_Success = False
        T_ID = 0
        Title_txt.Clear()
        Notes_txt.Clear()
        ' PriceTextBox.Clear()
        lblQtyPlus.Clear()
        Bill_DT.Clear()
        Receipts_DT.Clear()
        DateTimeEx.Text = Date.Now
        Edit_butt.Text = EditState
        DeletedBillLabel.Visible = False
        isVoid = False
        '  ClearCatFields()
        Me.Text = FormState
        User_Name_lb.Text = "---"
        'Invoice_Type_cm.SelectedIndex = 0
        On_Update = False
        Edit_butt.BackColor = Color.WhiteSmoke
    End Sub

    Private Sub New_butt_Click(sender As Object, e As EventArgs) Handles New_butt.Click
        Call_New_Bill()
    End Sub

    Private Sub Call_New_Bill()
        If T_ID > 0 Then
            If MessageBox.Show("فتح فاتورة جرد جديدة", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information,
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
            If MessageBox.Show("حفظ فاتورة الجرد ؟", "تنويه", MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.OK Then
                'AG_Balance_Update_Invoice_Type()
                Update_Total()
                DependingBill(T_ID)
                Save_Date(T_ID, DateTimeEx)
                Save_Title_Name(T_ID, Title_txt.Text)
                Save_About(T_ID, Notes_txt.Text)
                Switch_Dependcy(1)
                SelectStateBt()
            End If
        End If
    End Sub

    'Private Sub AG_Balance_Update_Invoice_Type()
    '    Dim sqlComm As New SqlClient.SqlCommand
    '    sqlComm.CommandText = "AG_Balance_Update_Invoice_Type"
    '    sqlComm.CommandType = CommandType.StoredProcedure
    '    sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
    '    sqlComm.Parameters.AddWithValue("@Type", Invoice_Type_cm.SelectedIndex)
    '    SQL_SP_EXEC(sqlComm)
    'End Sub

    'Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
    '    If Edit_butt.Text = EditState Then
    '        Edit_butt.Text = "ح التعديل"
    '        Enable_Fields()
    '        AGMetroGrid.BackgroundColor = Color.LightYellow
    '        AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
    '        If My_Settings.S_Default = 0 Then
    '            Barcode_SH_txt.Select()
    '        Else
    '            IM_SH_txt.Select()
    '        End If
    '    Else
    '        Save_About(T_ID, Notes_txt.Text)
    '        Save_Date(T_ID, DateTimeEx)
    '        Edit_butt.Text = EditState
    '        Disable_Fields()
    '        SelectStateBt()
    '    End If

    'End Sub

    Private Sub Delete_butt_Click(sender As Object, e As EventArgs) Handles Delete_butt.Click

        If AGMetroGrid.Rows.Count > 0 Then
            If IM_min_QTY = False Then
                If IM_Check_Neg_QTY_For_Cancel_Pch() = 1 Then
                    'MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
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
            Network_Edit_Tracker_insert("إلغاء الفاتورة", Bill_ID_Txt.Text, 9, 3)
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

    Private Sub Tr_BankNum_TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles lblQtyPlus.KeyPress
        Check_Only_Int(sender, e)
    End Sub


    'Private Sub AGMetroGrid_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles AGMetroGrid.CellEndEdit
    '    Update_Cat()
    'End Sub

    Private Sub AGMetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles AGMetroGrid.RowsAdded
        'Calc_Total()
    End Sub

    Private Sub AGMetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles AGMetroGrid.RowsRemoved
        'Calc_Total()
    End Sub

    Private Sub Calc_Total()

        Dim TotalQtyPlus As Decimal = 0D
        Dim TotalQtyMinus As Decimal = 0D

        Dim TotalCostPlus As Decimal = 0D
        Dim TotalCostMinus As Decimal = 0D

        For Each row As DataGridViewRow In AGMetroGrid.Rows

            If row.IsNewRow Then Continue For

            Dim qty As Decimal = 0D
            Dim cost As Decimal = 0D

            Decimal.TryParse(row.Cells("QTY_CL").Value?.ToString(), qty)
            Decimal.TryParse(row.Cells("Price_CL").Value?.ToString(), cost)

            If qty > 0 Then
                TotalQtyPlus += qty
                TotalCostPlus += qty * cost

            ElseIf qty < 0 Then
                TotalQtyMinus += qty
                TotalCostMinus += qty * cost
            End If

        Next

        Dim NetQty As Decimal = TotalQtyPlus + TotalQtyMinus
        Dim NetTotal As Decimal = TotalCostPlus + TotalCostMinus

        lblQtyPlus.Text = TotalQtyPlus.ToString("N3")
        lblQtyMinus.Text = TotalQtyMinus.ToString("N3")

        lblCostPlus.Text = TotalCostPlus.ToString("N3")
        lblCostMinus.Text = TotalCostMinus.ToString("N3")

        lblNetQty.Text = NetQty.ToString("N3")
        lblNetTotal.Text = NetTotal.ToString("N3")


        Select Case NetQty
            Case < 0 : lblNetQty.ForeColor = Color.DarkRed
            Case > 0 : lblNetQty.ForeColor = Color.DarkGreen
            Case 0 : lblNetQty.ForeColor = Color.Black
        End Select

        Select Case NetTotal
            Case < 0 : lblNetTotal.ForeColor = Color.DarkRed
            Case > 0 : lblNetTotal.ForeColor = Color.DarkGreen
            Case 0 : lblNetTotal.ForeColor = Color.Black
        End Select



        'TOTAL = 0
        'Dim QTY As Double = 0
        'For i = 0 To AGMetroGrid.Rows.Count - 1
        '    TOTAL = TOTAL + AGMetroGrid.Rows(i).Cells("Total_CL").Value
        '    QTY += AGMetroGrid.Rows(i).Cells("QTY_CL").Value
        'Next
        'IM_Count_LB.Text = AGMetroGrid.Rows.Count.ToString + " : مواد "
        'IM_Qty_LB.Text = QTY.ToString + " : كميات "
    End Sub

    Private Sub ADDCatButton_Click(sender As Object, e As EventArgs) Handles ADDCatButton.Click

        F_ST_settlement_IM_card = New ST_settlement_IM_card
        F_ST_settlement_IM_card.T_ID = T_ID
        F_ST_settlement_IM_card.ShowDialog()

    End Sub


    Private Sub Update_Total()
        If String.IsNullOrWhiteSpace(lblQtyPlus.Text) = False Then Save_Total(T_ID, TOTAL, 0)
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
        sqlComm.Parameters.AddWithValue("@ST_Sett_ID", 0)
        sqlComm.Parameters.AddWithValue("@AG_ID", 1)
        sqlComm.Parameters.AddWithValue("@Date", Me.DateTimeEx.Value)
        sqlComm.Parameters.AddWithValue("@BsType_ID", 39)
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters("@ST_Sett_ID").Direction = ParameterDirection.Output
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then

            T_ID = sqlComm.Parameters("@T_ID").Value.ToString()

            Select_ExpBill(T_ID)
        End If

    End Sub


    Public Sub Pch_Contents_SELECT_Bill()
        Bill_DT.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[ST_QTY_Deferences_Details_V_SELECT_Bill]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Bill_T_ID", Me.T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(Bill_DT)
        AGMetroGrid.DataSource = Bill_DT
        If AGMetroGrid.Rows.Count > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(AGMetroGrid.Rows.Count - 1).Cells("EX_Name_CL")
        Calc_Total()
    End Sub


    Private Sub Delete_Cat()
        Dim Row_Index As Integer = AGMetroGrid.CurrentCell.RowIndex
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "[ST_QTY_Deferences_Details_Delete]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
        If SQL_SP_EXEC(sqlComm) = True Then
            Network_Edit_Tracker_insert(" الصنف:" + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString + " كمية التسوية:" + AGMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString, Bill_ID_Txt.Text, 39, 2)

            Pch_Contents_SELECT_Bill()
            'Update_Total()

            'If On_Update = True Then If Not IsDBNull(AGMetroGrid.CurrentRow.Cells("NewSale_CL").Value) Then MsgBox("قم بتعديل سعر البيع من شاشة الأصناف", MsgBoxStyle.Information, "")
            If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index - 1).Cells("EX_Name_CL")

        End If

    End Sub


    Private Sub RemoveCatButton_Click(sender As Object, e As EventArgs) Handles RemoveCatButton.Click

        If AGMetroGrid.Rows.Count > 0 Then
            If MessageBox.Show(" حذف الصنف " + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                If IM_min_QTY = False Then
                    If IM_Check_Neg_QTY_For_Update_Pch() = 1 Then
                        MsgBox("في حالة خذف الصنف ستصبح كمية المخزون سالبة", MsgBoxStyle.Critical, "خطأ")
                        Exit Sub
                    End If
                End If

                Delete_Cat()
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


    Private Sub DateTimeEx_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimeEx.KeyDown
        If e.KeyCode = Keys.Return Then Save_Date(T_ID, DateTimeEx)
    End Sub


    Private Sub SerachButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        Me.Cursor = Cursors.AppStarting
        FormType = 14
        PchSearch.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub AGMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles AGMetroGrid.MouseDoubleClick
        'FormType = 14
        'If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And AGMetroGrid.Rows.Count > 0 Then Change_IM_Details.ShowDialog()
    End Sub

    Private Sub MakeBarcode_btn_Click(sender As Object, e As EventArgs) Handles MakeBarcode_btn.Click
        printbarcode.Auto_Print = True
        printbarcode.ShowDialog()
        printbarcode.Auto_Print = False
    End Sub

    Dim Tmp_Bill_ID As Integer
    Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
        Tmp_Bill_ID = Pch_ID
        Bill_ID_Txt.Text = Pch_ID - 1
        Get_T_ID()
    End Sub

    Public Sub Get_T_ID()
        Dim C As New C
        Dim S As String = "Select T_ID From Agents_Balance_MV Where ST_Sett_ID = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "'"
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
        FormType = 14
        Switch_To_DV_Show()
    End Sub

    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        IMTranPrintData()
    End Sub

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
                .rp.SetParameterValue(2, MY_Settings.Server_Desc)
                .rp.SetParameterValue(3, IM_Qty_LB.Text)
                .rp.SetParameterValue(4, T_ID)
                .rp.SetParameterValue(5, lblQtyPlus.Text)
                .rp.SetParameterValue(6, "فاتـــورة جـــرد")

                .rp.SetParameterValue(7, " العنوان : " + Title_txt.Text)
                .rp.SetParameterValue(8, " الرقم الألي : " + Bill_ID_Txt.Text)
                .rp.SetParameterValue(9, "")

            End With

            Dim p As New print
            p.CrystalReportViewer1.ReportSource = pp.rp
            p.Show()

            'pp.rp.PrintOptions.PrinterName = Default_Printer_A4
            'pp.rp.PrintToPrinter(1, False, 0, 0)
            'pp.rp.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub IM_btn_Click(sender As Object, e As EventArgs) Handles IM_btn.Click
        F_ItemsMenu = New ItemsMenu
        F_ItemsMenu.ShowDialog()
    End Sub


    Private Sub Search_txt_TextChanged(sender As Object, e As EventArgs) Handles Search_txt.TextChanged
        Dim dv As DataView
        dv = Bill_DT.DefaultView
        dv.RowFilter = "Item_Name like '%" & Search_txt.Text & "%'"
    End Sub

    Private Sub Search_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Search_txt.KeyDown
        If e.KeyCode = Keys.Delete Then Search_txt.Clear()
    End Sub

    Private Sub ClearSearch_btn_Click(sender As Object, e As EventArgs) Handles ClearSearch_btn.Click
        Search_txt.Clear()
        Barcode_Search_txt.Clear()
    End Sub

    Private Sub ALL_QTY_txt_TextChanged(sender As Object, e As EventArgs)  ', NewSaleByOne.TextChanged
        If Not String.IsNullOrWhiteSpace(sender.Text) Then
            Dim N As Double = sender.Text
            sender.Text = N.ToString("N")
        End If
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

    Private Sub Title_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Title_txt.KeyDown
        If e.KeyCode = Keys.Return Then Save_Title_Name(T_ID, Title_txt.Text)
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
                        Title_txt.Enabled = True
                        Ebable_CatFields()
                        Edit_butt.Text = "إيقاف التعديل"
                        Network_Edit_Tracker_insert("تعديل فاتورة جرد / رقم آلي : " + Bill_ID_Txt.Text + " / المدخل :  " + User_Name_lb.Text, lblQtyPlus.Text, 0, 0)
                        Notes_txt.Enabled = True
                        DateTimeEx.Enabled = True
                        'Invoice_Type_cm.Enabled = True

                    End If

                Else
                    'AG_Balance_Update_Invoice_Type()
                    Save_Title_Name(T_ID, Title_txt.Text)
                    Save_About(T_ID, Notes_txt.Text)

                    Save_Date(T_ID, DateTimeEx)
                    Save_Total(T_ID, TOTAL, 0)

                    On_Update = False
                    Edit_butt.Text = EditState
                    Edit_butt.BackColor = Color.WhiteSmoke
                    SelectStateBt()
                    Notes_txt.Enabled = False

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
            Else
                Save_About(T_ID, Notes_txt.Text)
                Save_Date(T_ID, DateTimeEx)
                Edit_butt.Text = EditState
                Disable_Fields()
                SelectStateBt()
            End If

        End If

    End Sub

    Private Sub Barcode_Search_txt_TextChanged(sender As Object, e As EventArgs) Handles Barcode_Search_txt.TextChanged
        Dim dv As DataView
        dv = Bill_DT.DefaultView
        dv.RowFilter = "Barcode like '%" & Barcode_Search_txt.Text & "%'"
    End Sub

    Private Sub Barcode_Search_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_Search_txt.KeyDown
        If e.KeyCode = Keys.Delete Then Barcode_Search_txt.Clear()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If IM_ID > 0 Then
            Show_IM_Details.IM_ID = IM_ID
            Show_IM_Details.ShowDialog()
        End If
    End Sub

    Private Sub تعديلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تعديلToolStripMenuItem.Click
        FormType = 14
        If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And AGMetroGrid.Rows.Count > 0 Then Change_IM_Details.ShowDialog()
    End Sub

    Private Sub عرضالتكلفةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضالتكلفةToolStripMenuItem.Click
        Show_IM_Cost(True, F_Invoice.AGMetroGrid.CurrentRow.Cells("EX_ID_CL").Value, F_Invoice.AGMetroGrid.CurrentRow.Cells("U_ID_CL").Value)
    End Sub

    Private Sub تعديلصلاحياتالصنفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تعديلصلاحياتالصنفToolStripMenuItem.Click
        Mang_IM_Valid_Notes.IM_ID = AGMetroGrid.CurrentRow.Cells("EX_ID_CL").Value
        Mang_IM_Valid_Notes.Bill_T_ID = AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value
        Mang_IM_Valid_Notes.IM_NAME = AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value

        Mang_IM_Valid_Notes.ShowDialog()
    End Sub

    Private Sub علاضبطاقةالصنفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles علاضبطاقةالصنفToolStripMenuItem.Click
        isShowing_Trans = True
        Str_ = F_Invoice.AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value
        F_ItemsMenu = New ItemsMenu
        F_ItemsMenu.ShowDialog()
    End Sub

    Private Sub DeletedBillLabel_Click(sender As Object, e As EventArgs) Handles DeletedBillLabel.Click
        If U_Cancel_Pch = True Then
            Beep()
            If MessageBox.Show(" سيتم تراجع عن إلغاء الفاتورة رقم " + Bill_ID_Txt.Text + " وكل المعاملات الخاصة بها ... متأكد ", "تاكيد العملية", MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
                AG_Balance_UN_Void_Row(T_ID, Pch_ID, 9)
                Get_T_ID()
            End If

        End If
    End Sub

    'Private Sub AGMetroGrid_DataSourceChanged(sender As Object, e As EventArgs) Handles AGMetroGrid.DataSourceChanged

    'End Sub
End Class