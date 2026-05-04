Public Class Invoice : Inherits System.Windows.Forms.Form
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
            If Label16 IsNot Nothing Then Label16.Tag = "TITLE_TRANSPARENT" ' عنوان الفورم
            If DeletedBillLabel IsNot Nothing Then DeletedBillLabel.Tag = "TITLE_TRANSPARENT"

            ' حاويات التنظيم الأصلية الموجودة في كودك (لجعل خلفيتها تتبع الثيم)
            If Panel1 IsNot Nothing Then Panel1.Tag = "TRANSPARENT"
            If Panel2 IsNot Nothing Then Panel2.Tag = "TRANSPARENT"
            If Panel3 IsNot Nothing Then Panel3.Tag = "TRANSPARENT"
            If Panel9 IsNot Nothing Then Panel9.Tag = "TRANSPARENT"
            If Panel13 IsNot Nothing Then Panel13.Tag = "TRANSPARENT"

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
        FormType = 4
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
        Dim S As String = "Select Top 1 T_ID From Agents_Balance_MV Where User_ID = '" & USER_ID & "' AND BsType_ID = 9 AND isDepended = 0 AND isVoid = 0  AND T_ID BETWEEN " & START_ID & " AND " & END_ID & " ORDER BY T_ID DESC"
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
        AGMetroGrid.Columns("Notes_CL").Visible = MY_Settings.SP_Notes_CL
        AGMetroGrid.Columns("IMNUM_CL").Visible = MY_Settings.S_IMNUM_CL
        AGMetroGrid.Columns("Barcode_CL").Visible = MY_Settings.S_Barcode_CL

        'Min_SP_Panel.Visible = S_Allow_MinSP
        'Min_SP_Panel_2.Visible = S_Allow_MinSP
    End Sub

    Public Sub SELECT_MAX()
        Dim c As New C
        Try
            Dim s As String
            s = "SELECT ISNULL(MAX(Jrd_ID),0) + 1 AS N FROM Agents_Balance_MV "
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
        Invoice_Type_cm.Enabled = True
        Ebable_CatFields()
    End Sub

    Private Sub Disable_Fields()
        Title_txt.Enabled = False
        DateTimeEx.Enabled = False
        Notes_txt.Enabled = False
        Invoice_Type_cm.Enabled = False
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
        Total_TextBox.Clear()
        Bill_DT.Clear()
        Receipts_DT.Clear()
        DateTimeEx.Text = Date.Now
        Edit_butt.Text = EditState
        DeletedBillLabel.Visible = False
        isVoid = False
        '  ClearCatFields()
        Me.Text = FormState
        User_Name_lb.Text = "---"
        Invoice_Type_cm.SelectedIndex = 0
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
                AG_Balance_Update_Invoice_Type()
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

    Private Sub AG_Balance_Update_Invoice_Type()
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Update_Invoice_Type"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@Type", Invoice_Type_cm.SelectedIndex)
        SQL_SP_EXEC(sqlComm)
    End Sub

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

    Private Sub Tr_BankNum_TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Total_TextBox.KeyPress
        Check_Only_Int(sender, e)
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
        Dim QTY As Double = 0
        For i = 0 To AGMetroGrid.Rows.Count - 1
            TOTAL = TOTAL + AGMetroGrid.Rows(i).Cells("Total_CL").Value
            QTY += AGMetroGrid.Rows(i).Cells("QTY_CL").Value
        Next
        Total_TextBox.Text = TOTAL.ToString("N")
        IM_Count_LB.Text = AGMetroGrid.Rows.Count.ToString + " : مواد "
        IM_Qty_LB.Text = QTY.ToString + " : كميات "
    End Sub

    Private Sub ADDCatButton_Click(sender As Object, e As EventArgs) Handles ADDCatButton.Click

        F_Invoice_IM_card = New Invoice_IM_card
        F_Invoice_IM_card.T_ID = T_ID
        F_Invoice_IM_card.ShowDialog()

        'Search_txt.Clear()
        'If IM_ID = 0 Then
        '    MsgBox("حددالصنف", MsgBoxStyle.Exclamation)
        '    IM_SH_txt.Select()
        'ElseIf String.IsNullOrWhiteSpace(PriceTextBox.Text) Then
        '    MsgBox("حدد سعر القطعة", MsgBoxStyle.Exclamation)
        '    PriceTextBox.Select()
        'Else
        '    If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"

        '    For i = 0 To AGMetroGrid.Rows.Count - 1
        '        If AGMetroGrid.Rows(i).Cells("EX_ID_CL").Value = IM_ID And AGMetroGrid.Rows(i).Cells("St_ID_CL").Value = ST_cm.SelectedValue Then

        '            If Valid_Panel.Visible = True Then
        '                If AGMetroGrid.Rows(i).Cells("D_Valid_CL").Value = D_Valid.Text Then
        '                    'MsgBox("تم إدخال الصنف", MsgBoxStyle.Critical)
        '                    'Exit Sub
        '                    If MessageBox.Show(" تم إدخال الصنف ... هل تريد الإستمرار ", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
        '                        Exit Sub
        '                    End If
        '                End If
        '            Else
        '                If MessageBox.Show(" تم إدخال الصنف ... هل تريد الإستمرار ", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
        '                    Exit Sub
        '                End If
        '                'MsgBox("تم إدخال الصنف", MsgBoxStyle.Critical)
        '                'Exit Sub
        '            End If
        '        End If
        '    Next

        '    If Not String.IsNullOrWhiteSpace(NewSalePrice_txt.Text) And Convert.ToDouble(Prev_Sale_Unit_txt.Text) > 0 Then
        '        If Convert.ToDouble(ALL_QTY_txt.Text) > 0 Then
        '            If Convert.ToDouble(NewSalePrice_txt.Text) < IM_Calc_Avg(False) Then
        '                If MessageBox.Show(" سعر البيع أقل من التكلفة .. هل تريد الإستمرار ", "تحذير", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
        '                    Exit Sub
        '                End If
        '            End If
        '        End If
        '    End If


        '    Insert_Cat()
        'End If

    End Sub


    Private Sub Update_Total()
        If String.IsNullOrWhiteSpace(Total_TextBox.Text) = False Then Save_Total(T_ID, TOTAL, 0)
    End Sub

    'Private Sub ClearCatFields()
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
        sqlComm.Parameters.AddWithValue("@BsType_ID", 9)
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters("@Jrd_ID").Direction = ParameterDirection.Output
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then

            T_ID = sqlComm.Parameters("@T_ID").Value.ToString()

            Select_ExpBill(T_ID)

            'If My_Settings.S_Default = 0 Then
            '    Barcode_SH_txt.Select()
            'Else
            '    IM_SH_txt.Select()
            'End If
        End If

    End Sub


    'Private Sub Insert_Cat()
    '    If String.IsNullOrWhiteSpace(Barcode_IM) Then Barcode_IM = SELECT_BARCODE(IM_ID, U_ID)
    '    'Dim Row_Index As Integer = 0
    '    'If AGMetroGrid.Rows.Count > 0 Then Row_Index = AGMetroGrid.CurrentCell.RowIndex + 1
    '    Dim sqlComm As New SqlClient.SqlCommand()
    '    sqlComm.CommandText = "Pch_Details_Insert"
    '    sqlComm.CommandType = CommandType.StoredProcedure
    '    sqlComm.Parameters.AddWithValue("@T_ID", 0)
    '    sqlComm.Parameters.AddWithValue("@Pch_T_ID", T_ID)
    '    sqlComm.Parameters.AddWithValue("@IM_ID", IM_ID)
    '    sqlComm.Parameters.AddWithValue("@U_ID", U_ID)
    '    sqlComm.Parameters.AddWithValue("@Price", PriceTextBox.Text)
    '    If Valid_Panel.Visible = True Then sqlComm.Parameters.AddWithValue("@D_Vaild", D_Valid.Value.Date)
    '    sqlComm.Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
    '    sqlComm.Parameters.AddWithValue("@QYT", QtyTextBox.Text)
    '    sqlComm.Parameters.AddWithValue("@Total", Convert.ToDouble(QtyTextBox.Text) * Convert.ToDouble(PriceTextBox.Text))
    '    If String.IsNullOrWhiteSpace(NewSalePrice_txt.Text) = False Then sqlComm.Parameters.AddWithValue("@NewSale", NewSalePrice_txt.Text)
    '    If String.IsNullOrWhiteSpace(NewSaleByOne.Text) = False Then sqlComm.Parameters.AddWithValue("@NewSaleByOne", NewSaleByOne.Text)

    '    If String.IsNullOrWhiteSpace(Min_SP_txt.Text) = False Then sqlComm.Parameters.AddWithValue("@Min_SP", Min_SP_txt.Text)
    '    If String.IsNullOrWhiteSpace(Min_SP_By_One_txt.Text) = False Then sqlComm.Parameters.AddWithValue("@Min_SP_ByOne", Min_SP_By_One_txt.Text)

    '    If String.IsNullOrWhiteSpace(Min_SP_2_txt.Text) = False Then sqlComm.Parameters.AddWithValue("@Min_SP_2", Convert.ToDouble(Min_SP_2_txt.Text))
    '    If String.IsNullOrWhiteSpace(Min_SP_2_By_One_txt.Text) = False Then sqlComm.Parameters.AddWithValue("@Min_SP_2_ByOne", Convert.ToDouble(Min_SP_2_By_One_txt.Text))

    '    sqlComm.Parameters.AddWithValue("@Invoice_Type", Invoice_Type_cm.SelectedIndex)

    '    sqlComm.Parameters.AddWithValue("@On_Update", On_Update)
    '    sqlComm.Parameters.AddWithValue("@Barcode", Barcode_IM)
    '    If SQL_SP_EXEC(sqlComm) = True Then

    '        Network_Edit_Tracker_insert(" الصنف:" + IM_SH_txt.Text + " الوحدة:" + IM_Unit_cm.Text + " العدد:" + QtyTextBox.Text + " السعر:" + PriceTextBox.Text + " البيع:" _
    '            + NewSalePrice_txt.Text + " بيع القطعة:" + NewSaleByOne.Text, Bill_ID_Txt.Text, 9, 1)

    '        Update_Total()
    '        ClearCatFields()

    '        'Network_Edit_Tracker_insert("إضافة صنف لفاتورة مشتريات / رقم آلي: " + Bill_ID_Txt.Text + " /الصنف: " + IM_SH_txt.Text _
    '        '                + " /العدد: " + QtyTextBox.Text + " /الوحدة: " + IM_Unit_cm.Text + " /السعر: " + PriceTextBox.Text + " /البيع:  " _
    '        '                + NewSalePrice_txt.Text + " /بيع القطعة:  " + NewSaleByOne.Text, Total_TextBox.Text, 0, 0)

    '        If On_Update = True Then DependingUpdatedBill(T_ID)
    '        Pch_Contents_SELECT_Bill()
    '        'If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index).Cells("EX_Name_CL")
    '        If MY_Settings.S_Default = 0 Then
    '            Barcode_SH_txt.Select()
    '        Else
    '            IM_SH_txt.Select()
    '        End If
    '    End If

    'End Sub


    'Public Sub SELECT_EXP_Cats(T_ID)
    '    Try
    '        AGMetroGrid.Rows.Clear()
    '        Dim C As New C
    '        Dim S As String = "Select * From Pch_Details_V Where Pch_T_ID = '" & T_ID & "'"
    '        C.Com = New SqlClient.SqlCommand(S, C.Con)
    '        C.Con.Open()
    '        Try
    '            C.Dr = C.Com.ExecuteReader
    '            If C.Dr.HasRows Then
    '                While C.Dr.Read
    '                    AGMetroGrid.Rows.Add(C.Dr("T_ID"), C.Dr("IM_ID"), "", C.Dr("item_name"), C.Dr("U_Name"), C.Dr("Price"), C.Dr("QYT"), C.Dr("NewSale"), C.Dr("Total"))
    '                End While
    '            End If

    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try
    '        C.Con.Close()
    '    Catch ex As Exception
    '        '...
    '    End Try
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
    End Sub


    Private Sub Delete_Cat()
        Dim Row_Index As Integer = AGMetroGrid.CurrentCell.RowIndex
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "Pch_Details_Delete"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
        sqlComm.Parameters.AddWithValue("@ON_UPDATE", 0)
        If SQL_SP_EXEC(sqlComm) = True Then
            Network_Edit_Tracker_insert(" الصنف:" + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString + " الوحدة:" + AGMetroGrid.CurrentRow.Cells("IMUnit_CL").Value.ToString + " العدد:" + AGMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString _
                            + " السعر:" + AGMetroGrid.CurrentRow.Cells("Price_CL").Value.ToString, Bill_ID_Txt.Text, 9, 2)

            Pch_Contents_SELECT_Bill()
            Update_Total()

            If On_Update = True Then If Not IsDBNull(AGMetroGrid.CurrentRow.Cells("NewSale_CL").Value) Then MsgBox("قم بتعديل سعر البيع من شاشة الأصناف", MsgBoxStyle.Information, "")
            If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index - 1).Cells("EX_Name_CL")
            'If My_Settings.S_Default = 0 Then
            '    Barcode_SH_txt.Select()
            'Else
            '    IM_SH_txt.Select()
            'End If
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
    '    Check_Only_Float_With_Negitave(sender, e)
    'End Sub

    'Private Sub QtyTextBox_TextChanged(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
    '    CalcAvgCost()
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

    'Private Sub CalcAvgCost()
    '    If S_Stores = True Then
    '        If String.IsNullOrWhiteSpace(Current_QTY.Text) Then Current_QTY.Text = "0"
    '        If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "0"
    '        If Current_QTY.Text.Count > 0 Then If Convert.ToDouble(Current_QTY.Text) > 0 Then IM_Set_Avg()
    '    End If
    'End Sub

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
        FormType = 4
        PchSearch.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub AGMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles AGMetroGrid.MouseDoubleClick
        FormType = 4
        If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And AGMetroGrid.Rows.Count > 0 Then Change_IM_Details.ShowDialog()
    End Sub

    '-------------------------------------------------------------------------------------------------
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
    '        IMDataGridViewX.Visible = False
    '        IM_ID = 0
    '        U_Dt.Clear()
    '        Current_QTY.Clear()
    '        ALL_QTY_txt.Clear()
    '        U_Cargo = 1
    '        Prev_Sale_Unit_txt.Clear()

    '        PriceTextBox.Clear()
    '        QtyTextBox.Clear()
    '        NewSaleByOne.Clear()
    '        CostByOne.Clear()
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
    '        Load_IM_Cost()
    '        If IMDataGridViewX.CurrentRow.Cells("isValid_CL").Value = 1 Then
    '            Valid_Panel.Visible = True
    '            D_Valid.Value = Date.Now
    '        Else
    '            Valid_Panel.Visible = False
    '        End If
    '    End If
    'End Sub

    'Public Sub Load_IM_Cost()
    '    Dim c As New C
    '    Try
    '        Dim s As String
    '        s = "select ISNULL(COST,0) AS COST from IM_MENU WHERE IM_ID = '" & IM_ID & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            PriceTextBox.Text = c.Dr("COST") * U_Cargo

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
    '        s = "select IM_ID,item_name,IM_NUM,isValid from IM_All_V  Order by item_name ASC"
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

    '    Dim C As New C
    '    IM_Dt.Clear()
    '    Try
    '        Dim s As String
    '        If Sh_ByNum_CB.Checked = True Then
    '            s = "select IM_ID,item_name,isValid from IM_All_V WHERE IM_NUM = '" & Barcode_SH_txt.Text & "' And isStore = 1"
    '        Else
    '            s = "select U_IM_ID,IM_ID,item_name,isValid from IM_units_Search_V WHERE Barcode = '" & Barcode_SH_txt.Text & "'"
    '        End If
    '        C.Com = New SqlClient.SqlCommand(s, C.Con)
    '        C.Con.Open()

    '        C.Dr = C.Com.ExecuteReader
    '        If C.Dr.HasRows Then
    '            C.Dr.Read()
    '            IM_ID = C.Dr("IM_ID")
    '            IM_SH_txt.Text = C.Dr("item_name")
    '            If Sh_ByNum_CB.Checked = False Then Barcode_IM = Barcode_SH_txt.Text
    '            Get_Unit = False
    '            Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
    '            Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
    '            Load_IM_Cost()
    '            IMDataGridViewX.Visible = False

    '            If C.Dr("isValid") = 1 Then
    '                Valid_Panel.Visible = True
    '            Else
    '                Valid_Panel.Visible = False
    '            End If

    '            QtyTextBox.Select()
    '            Fetch_IM_Units()
    '            Barcode_SH_txt.Clear()
    '            If Sh_ByNum_CB.Checked = False Then IM_Unit_cm.SelectedValue = C.Dr("U_IM_ID")
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


    'Public Function IM_Calc_Avg(isMsg As Boolean)
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
    '            Prev_Cost = c.Dr("Cost")
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
    '            Prev_Qty = c.Dr("QTY")
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    '    Dim AVG_COST As Double = ((Prev_Cost * Prev_Qty) + (Convert.ToDouble(PriceTextBox.Text) * Convert.ToDouble(QtyTextBox.Text))) / (Prev_Qty + Convert.ToDouble(QtyTextBox.Text)) * U_Cargo
    '    If isMsg = True Then MsgBox(AVG_COST.ToString("00.00"), MsgBoxStyle.Information, "إجمالي التكلفــة")


    '    Return AVG_COST

    'End Function

    'Private Sub NewSalePrice_txt_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return

    '            If NewSaleByOne.Visible = False Then
    '                If Two_Panel.Visible = False Then
    '                    If Min_SP_Panel.Visible = True Then
    '                        Min_SP_txt.Select()
    '                        Exit Sub
    '                    End If

    '                End If
    '                If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)
    '            Else
    '                NewSaleByOne.Select()
    '            End If

    '        Case Keys.Left : If NewSaleByOne.Visible = False Then NewSaleByOne.Select()
    '        Case Keys.Up : Barcode_SH_txt.Select()
    '        Case Keys.Right : PriceTextBox.Select()
    '        Case Keys.Down : Min_SP_txt.Select()
    '    End Select
    'End Sub

    'Private Sub IM_Fetch_QTY()
    '    Dim c As New C
    '    Try
    '        Dim s As String
    '        s = "select U_ID,U_Cargo,Price,Min_SP from IM_Menu_Units_V WHERE U_IM_ID = '" & IM_Unit_cm.SelectedValue & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            U_Cargo = c.Dr("U_Cargo")
    '            Prev_Sale_Unit_txt.Text = c.Dr("Price")
    '            Dim N As Double = (Convert.ToDouble(IM_QTY) / c.Dr("U_Cargo"))
    '            Current_QTY.Text = N.ToString("N")
    '            ALL_QTY_txt.Text = ALL_QTY / U_Cargo
    '            U_ID = c.Dr("U_ID")
    '            '  PrevMin_SP_txt.Text = c.Dr("Min_SP")
    '            If U_Cargo > 1 Then
    '                One_Panel.Visible = True
    '                Two_Panel.Visible = True
    '                NewSaleByOne.Visible = True
    '                CostByOne.Visible = True

    '                Min_SP_By_One_txt.Visible = True
    '                Min_SP_By_One_Lb.Visible = True

    '                Min_SP_2_By_One_txt.Visible = True
    '                Min_SP_2_By_One_Lb.Visible = True
    '            Else
    '                One_Panel.Visible = False
    '                Two_Panel.Visible = False
    '                NewSaleByOne.Visible = False
    '                CostByOne.Visible = False

    '                Min_SP_By_One_txt.Visible = False
    '                Min_SP_By_One_Lb.Visible = False

    '                Min_SP_2_By_One_txt.Visible = False
    '                Min_SP_2_By_One_Lb.Visible = False

    '            End If
    '            CalcAvgCost()
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs)
    '    If Get_Unit = True Then
    '        IM_Fetch_QTY()
    '        Load_IM_Cost()
    '    End If
    'End Sub


    Private Sub MakeBarcode_btn_Click(sender As Object, e As EventArgs) Handles MakeBarcode_btn.Click
        printbarcode.Auto_Print = True
        printbarcode.ShowDialog()
        printbarcode.Auto_Print = False
    End Sub

    'Private Sub IM_Unit_cm_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return, Keys.Left : QtyTextBox.Select()
    '    End Select
    'End Sub

    'Private Sub NewSaleByOne_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return
    '            If D_Valid.Visible = True Then
    '                D_Valid.Select()
    '                Exit Sub
    '            End If
    '            If Min_SP_Panel.Visible = True Then
    '                Min_SP_txt.Select()
    '                Exit Sub
    '            End If
    '            If ADDCatButton.Enabled = True Then
    '                ADDCatButton_Click(sender, e)
    '            End If

    '        Case Keys.Up : Barcode_SH_txt.Select()
    '        Case Keys.Right : CostByOne.Select()
    '        Case Keys.Down
    '            If Min_SP_By_One_txt.Visible = True Then Min_SP_By_One_txt.Select()
    '    End Select
    'End Sub

    'Private Sub NewSaleByOne_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub CostByOne_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return, Keys.Left : NewSaleByOne.Select()
    '        Case Keys.Up : Barcode_SH_txt.Select()
    '        Case Keys.Right : NewSalePrice_txt.Select()
    '    End Select
    'End Sub

    'Private Sub NewSaleByOne_TextChanged(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
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
        Dim S As String = "Select T_ID From Agents_Balance_MV Where Jrd_ID = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "'"
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
        FormType = 4
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
                .rp.SetParameterValue(5, Total_TextBox.Text)
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
    '        'If MessageBox.Show(" إضافة وحدة للصنف " + IM_SH_txt.Text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
    '        Add_Unit.IM_ID = IM_ID
    '        Add_Unit.ShowDialog()
    '        'End If
    '    End If
    'End Sub

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

    'Private Sub Min_SP_txt_TextChanged(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
    '    If Not String.IsNullOrWhiteSpace(Min_SP_txt.Text) And U_Cargo > 1 Then
    '        Min_SP_By_One_txt.Text = (Convert.ToDouble(Min_SP_txt.Text) / U_Cargo).ToString("N")
    '    Else
    '        Min_SP_By_One_txt.Clear()
    '    End If
    'End Sub

    'Private Sub Min_SP_txt_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Up
    '            QtyTextBox.Select()
    '        Case Keys.Return
    '            'Min_SP_By_One_txt.Select()
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

    'Private Sub Min_SP_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub Min_SP_By_One_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Min_SP_By_One_txt.KeyPress
    '    Check_Point_in_FloatNum(sender, e)
    'End Sub

    'Private Sub Min_SP_By_One_txt_KeyDown(sender As Object, e As KeyEventArgs)

    '    Select Case e.KeyCode
    '        Case Keys.Up
    '            NewSaleByOne.Select()
    '        Case Keys.Return
    '            If Min_SP_Panel_2.Visible = True Then
    '                Min_SP_2_txt.Select()
    '                Exit Sub
    '            End If

    '            If ADDCatButton.Enabled = True Then
    '                ADDCatButton_Click(sender, e)
    '                Exit Sub
    '            End If

    '    End Select

    'End Sub

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


    'Private Sub Min_SP_2_txt_TextChanged(sender As Object, e As EventArgs)
    '    If Not String.IsNullOrWhiteSpace(Min_SP_2_txt.Text) And U_Cargo > 1 Then
    '        Min_SP_2_By_One_txt.Text = (Convert.ToDouble(Min_SP_2_txt.Text) / U_Cargo).ToString("N")
    '    Else
    '        Min_SP_2_By_One_txt.Clear()
    '    End If
    'End Sub

    'Private Sub Min_SP_2_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub Min_SP_2_txt_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return
    '            If Min_SP_2_By_One_txt.Visible = True Then
    '                Min_SP_2_By_One_txt.Select()
    '                Exit Sub
    '            End If
    '            If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)
    '    End Select
    'End Sub

    'Private Sub Min_SP_2_By_One_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub Min_SP_2_By_One_txt_TextChanged(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
    'End Sub

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
                        Network_Edit_Tracker_insert("تعديل فاتورة جرد / رقم آلي : " + Bill_ID_Txt.Text + " / المدخل :  " + User_Name_lb.Text, Total_TextBox.Text, 0, 0)
                        Notes_txt.Enabled = True
                        DateTimeEx.Enabled = True
                        Invoice_Type_cm.Enabled = True
                        'If MY_Settings.S_Default = 0 Then
                        '    Barcode_SH_txt.Select()
                        'Else
                        '    IM_SH_txt.Select()
                        'End If
                    End If

                Else
                    AG_Balance_Update_Invoice_Type()
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
                'If MY_Settings.S_Default = 0 Then
                '    Barcode_SH_txt.Select()
                'Else
                '    IM_SH_txt.Select()
                'End If
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

    'Private Sub Min_SP_2_By_One_txt_KeyDown(sender As Object, e As KeyEventArgs)
    '    If e.KeyCode = Keys.Return Then If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)
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

    '    Dim AVG_COST As Double = ((Prev_Cost * Prev_Qty) + _
    '                              ((Convert.ToDouble(PriceTextBox.Text) / U_Cargo) * (Convert.ToDouble(QtyTextBox.Text) * U_Cargo))) _
    '                          / (Prev_Qty + (Convert.ToDouble(QtyTextBox.Text)))

    '    If isMsg = True Then MsgBox((AVG_COST).ToString("00.00"), MsgBoxStyle.Information, "إجمالي التكلفــة")

    '    Return AVG_COST
    'End Function

    '-----------------------------------------------------------------------------------------------------------------------------
    Private Sub تعديلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تعديلToolStripMenuItem.Click
        FormType = 4
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


End Class