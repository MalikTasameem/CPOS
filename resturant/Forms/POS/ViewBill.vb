
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Public Class ViewBill : Inherits System.Windows.Forms.Form

    Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Public T_ID As Integer
    Public isDepended As Boolean
    Public isVoid As Boolean
    Public isShowingDetails As Boolean = False
    Public TOTAL As Double = 0
    Public Disc As Double = 0
    Public Pure As Double = 0
    Public AG_ID As Integer = 0
    Public isNewBill As Integer
    Public Barcode As String
    Dim isPied As Integer = 0
    Dim On_Update As Boolean
    Public IS_Show_Rctp As Boolean = False
    Dim AG_Balance As Double = 0
    Dim PREV_AG_ID As Integer
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Const CS_DROPSHADOW As Integer = &H20000
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property
    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        FormType = 0
    End Sub

    Private Sub Expenses_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Keys.F1
                If New_butt.Enabled = True Then ResetNewBill()
            Case Keys.F12
                If Save_butt.Enabled = True Then Save_butt_Click(sender, e)
            Case Keys.F2
                If Print_btn.Enabled = True Then Print_btn_Click(sender, e)
            Case Keys.F3
                If Edit_butt.Enabled = True Then Edit_butt_Click(sender, e)
            Case Keys.F4
                If Delete_butt.Enabled = True Then Delete_butt_Click(sender, e)
            Case Keys.F7
                SBPauseBtn_Click(sender, e)

            Case Keys.PageUp
                Up_Bill_btn_Click(sender, e)
            Case Keys.PageDown
                Down_Bill_btn_Click(sender, e)

            Case 107 'Add
                If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
                    If AGMetroGrid.Rows.Count > 0 Then
                        Dim Def As Integer = 1
                        Change_IM_Qty(Def)
                    End If
                End If

            Case 109 'Subtrac
                If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
                    If AGMetroGrid.Rows.Count > 0 Then
                        If AGMetroGrid.CurrentRow.Cells("QTY_CL").Value > 1 Then
                            Dim Def As Integer = -1
                            Change_IM_Qty(Def)
                        End If
                    End If
                End If

        End Select
    End Sub

    Private Sub Change_IM_Qty(def As Integer)
        Dim SB_T_ID As Integer = AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value
        Dim Row_Index As Integer = AGMetroGrid.CurrentCell.RowIndex
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "ViewBill_Contents_Change_IM_Qty"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", SB_T_ID)
            .Parameters.AddWithValue("@Def", def)
            .Parameters.AddWithValue("@On_Update", On_Update)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            ViewBill_Contents_SELECT_Bill()
            AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index).Cells("EX_Name_CL")
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
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")

        If TitleBar_Panel IsNot Nothing Then TitleBar_Panel.Tag = "HEADER"
        'If Title_Label IsNot Nothing Then Title_Label.Tag = "TITLE_TRANSPARENT"
        If ExitFormButton IsNot Nothing Then ExitFormButton.Tag = "DELETE"
        'If DeletedBillLabel IsNot Nothing Then DeletedBillLabel.Tag = "DELETE"
        If MaxFormButton IsNot Nothing Then MaxFormButton.Tag = "GENERAL"
        'If DeletedBillLabel IsNot Nothing Then DeletedBillLabel.Tag = "DELETE"

        If New_butt IsNot Nothing Then New_butt.Tag = "GENERAL"
        If Save_butt IsNot Nothing Then Save_butt.Tag = "SAVE"
        If Edit_butt IsNot Nothing Then Edit_butt.Tag = "GENERAL"
        If Delete_butt IsNot Nothing Then Delete_butt.Tag = "DELETE"
        If Print_btn IsNot Nothing Then Print_btn.Tag = "PRINT"
        'If SearchButton IsNot Nothing Then SearchButton.Tag = "GENERAL"
        'If MakeBarcode_btn IsNot Nothing Then MakeBarcode_btn.Tag = "GENERAL"
        'If Aggregate_Btn IsNot Nothing Then Aggregate_Btn.Tag = "GENERAL"
        'If DeliveryingButton IsNot Nothing Then DeliveryingButton.Tag = "SAVE"

        If ADDCatButton IsNot Nothing Then ADDCatButton.Tag = "GENERAL"
        If RemoveCatButton IsNot Nothing Then RemoveCatButton.Tag = "DELETE"


        'If IM_btn IsNot Nothing Then IM_btn.Tag = "GENERAL"
        'If Show_IM_btn2 IsNot Nothing Then Show_IM_btn2.Tag = "GENERAL"
        If DGV_Control_btn IsNot Nothing Then DGV_Control_btn.Tag = "GENERAL"
        If Up_Bill_btn IsNot Nothing Then Up_Bill_btn.Tag = "GENERAL"
        If Down_Bill_btn IsNot Nothing Then Down_Bill_btn.Tag = "GENERAL"
        If Calc_Dicount_Btn IsNot Nothing Then Calc_Dicount_Btn.Tag = "GENERAL"
        'If ADD_Dist_btn IsNot Nothing Then ADD_Dist_btn.Tag = "GENERAL"
        'If Remove_Dist_btn IsNot Nothing Then Remove_Dist_btn.Tag = "DELETE"

        ' تطبيق الثيم الإجباري
        ThemeManager.ApplyThemeToForm(Me)



        FormType = 10
        Check_View_Control()
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        EditState = Edit_butt.Text
        Delete_butt.Visible = U_SalesVoid

        If U_SalesDis = True And isDiscount = True Then
            DiscountPanel.Visible = True
        Else
            DiscountPanel.Visible = False
        End If
        Edit_butt.Visible = U_SB_Update
        Get_Last_T_ID()

        If isShowing_Trans = True Then
            T_ID = T_ID_Trans
            ViewBill_Contents_SELECT_Bill()
            Fill_Bill_Info()
            SelectStateBt()
            New_butt.Enabled = False
        End If


    End Sub

    Public Sub Fill_All_AG_Proj()
        Try
            Dim C As New C
            Dim s As String = "SELECT Proj_ID,Proj_NAME from AG_Projects_V WHERE AG_ID = '" & Me.AG_ID & "' ORDER BY Proj_ID DESC"
            C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
            C.Da.Fill(C.Dt)
            Project_cm.DataSource = C.Dt
            Project_cm.ValueMember = "Proj_ID"
            Project_cm.DisplayMember = "Proj_NAME"
            ' Project_cm.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Get_Last_T_ID()

        Dim C As New C
            Dim S As String = "Select Top 1 T_ID From Agents_Balance_MV Where User_ID = '" & USER_ID & "' AND BsType_ID = 16 AND isDepended = 0 AND isVoid = 0  AND T_ID BETWEEN " & START_ID & " AND " & END_ID & " ORDER BY T_ID DESC"
            C.Com = New SqlClient.SqlCommand(S, C.Con)
            C.Con.Open()
            Try
                C.Dr = C.Com.ExecuteReader
                If C.Dr.HasRows Then
                    C.Dr.Read()
                    ClearFields()
                    T_ID = C.Dr("T_ID")
                    Fill_Bill_Info()
                    SelectStateBt()
                    ViewBill_Contents_SELECT_Bill()
                Else
                    Call_New_Bill()
                    'SELECT_MAX()
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            C.Con.Close()

    End Sub

    Public Sub SELECT_MAX()
        Dim c As New C
        Try
            Dim s As String
            s = "SELECT ISNULL(MAX(ViewSB_ID),0) + 1 AS N FROM Agents_Balance_MV "
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

    Public Sub Check_View_Control()
        Me.AGMetroGrid.Columns("Date_CL").Visible = My_Settings.S_Date_CL
        Me.AGMetroGrid.Columns("ST_Name_CL").Visible = My_Settings.S_ST_Name_CL
        Me.AGMetroGrid.Columns("D_Valid_CL").Visible = My_Settings.S_D_Valid_CL
        Me.AGMetroGrid.Columns("IMUnit_CL").Visible = My_Settings.S_IMUnit_CL
        Me.AGMetroGrid.Columns("Price_CL").Visible = My_Settings.S_Price_CL
        Me.AGMetroGrid.Columns("Total_CL").Visible = My_Settings.S_Total_CL
        Me.AGMetroGrid.Columns("Notes_CL").Visible = My_Settings.SP_Notes_CL
        Me.AGMetroGrid.Columns("IMNUM_CL").Visible = MY_Settings.S_IMNUM_CL
        'Min_SP_Panel.Visible = S_Allow_MinSP
    End Sub

    Public Sub ViewBill_Contents_SELECT_Bill()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "ViewBill_Contents_SELECT_Bill_2"
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
            .CommandText = "ViewBill_Info_V_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
        End With
        C.Con.Open()
        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            C.Dr.Read()

            AG_ID = C.Dr("AG_ID")
            AG_Cm.Set_IM_By_ID(AG_ID)

            DateTimeEx.Text = C.Dr("Date")

            If C.Dr("isPied") = 1 Then
                Save_butt.Enabled = False
                Discount_txt.Enabled = False
            Else
                Save_butt.Enabled = True
                Discount_txt.Enabled = True
            End If
            Bill_ID_Txt.Text = C.Dr("ViewSB_ID")
            If C.Dr("Discount") > 0 Then
                Discount_txt.Text = C.Dr("Discount")
                If Discount_Distribute = False Then Pure_txt.Text = C.Dr("Total") - C.Dr("Discount")
            End If
            isVoid = C.Dr("isVoid")
            isDepended = C.Dr("isDepended")

            isPied = C.Dr("isPied")
            User_Name_lb.Text = C.Dr("U_Name") + " - " + C.Dr("Date").ToString
            Notes_txt.Text = C.Dr("About")

            Fill_All_AG_Proj()
            Project_cm.SelectedValue = C.Dr("Proj_ID")

        Else
            AG_ID = Default_AG_ID
            AG_Cm.Set_IM_By_ID(AG_ID)
            VoidLb.Enabled = False
        End If
        C.Con.Close()
    End Sub


    Private Sub Enable_Fields()
        DateTimeEx.Enabled = True
        AG_Cm.Enabled = True
        Notes_txt.Enabled = True
        Ebable_CatFields()
        Project_cm.Enabled = True
        Discount_txt.Enabled = True
        Calc_Dicount_Btn.Enabled = True
    End Sub

    Private Sub Disable_Fields()
        DateTimeEx.Enabled = False
        AG_Cm.Enabled = False
        Notes_txt.Enabled = False
        Disable_CatFields()
        Project_cm.Enabled = False
        Discount_txt.Enabled = False
        Calc_Dicount_Btn.Enabled = False

    End Sub

    Private Sub Disable_CatFields()
        ADDCatButton.Enabled = False
        RemoveCatButton.Enabled = False
    End Sub

    Private Sub Ebable_CatFields()
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
        Me.Text = "فاتورة عرض جديدة"
        AG_Cm.Visible = True
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
            Move_To_SB_btn.Enabled = False
        Else

            If isDepended = False Then
                Save_butt.Enabled = True
                Edit_butt.Enabled = False
                Print_btn.Enabled = False
                Move_To_SB_btn.Enabled = False
                AGMetroGrid.BackgroundColor = Color.LightYellow
                AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
                Enable_Fields()
            Else
                Save_butt.Enabled = False
                Edit_butt.Enabled = True
                Print_btn.Enabled = True
                Move_To_SB_btn.Enabled = True
                AGMetroGrid.BackgroundColor = Color.LightGreen
                AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
                Disable_Fields()
            End If

            VoidLb.Visible = False
            Delete_butt.Enabled = True
        End If

        Me.Text = "عرض بيانات فاتورة"

    End Sub


    Private Sub ClearFields()
        T_ID = 0
        Notes_txt.Clear()
        Total_TextBox.Clear()
        Discount_txt.Clear()
        DateTimeEx.Text = Date.Now
        VoidLb.Visible = False
        isVoid = False
        isDepended = False
        Discount_txt.Clear()
        Disc = 0
        AG_Balance = 0
        Me.Text = FormState
        Edit_butt.BackColor = Color.WhiteSmoke
        Edit_butt.Text = EditState
        On_Update = False
    End Sub


    Private Sub ResetNewBill()
        Enable_Fields()
        ClearFields()
        Insert_NewBill()
        NewStateBt()
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
        sqlComm.Parameters.AddWithValue("@AG_ID", Default_AG_ID)
        sqlComm.Parameters.AddWithValue("@Date", Me.DateTimeEx.Value)
        sqlComm.Parameters.AddWithValue("@BsType_ID", 16)
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters("@ViewSB_ID").Direction = ParameterDirection.Output
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then
            Me.Bill_ID_Txt.Text = sqlComm.Parameters("@ViewSB_ID").Value.ToString()
            T_ID = sqlComm.Parameters("@T_ID").Value.ToString()
            Fill_Bill_Info()
            ViewBill_Contents_SELECT_Bill()
            Switch_Dependcy(0)

        End If
    End Sub

    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        If AGMetroGrid.Rows.Count > 0 Then
            If AG_Cm.TXT_ID.Text = 0 Then
                MsgBox("حدد إسم العميل", MsgBoxStyle.Critical, "خطأ في الإعتماد")
                AG_Cm.Focus()
            Else
                If AG_Cm.TXT_ID.Text = 0 Then
                    AG_ID = Default_AG_ID
                    AG_Cm.Set_IM_By_ID(AG_ID)
                End If

                Beep()
                Save_AG_Name(T_ID, AG_ID, False)
                Save_About(T_ID, Notes_txt.Text)
                Save_Date(T_ID, DateTimeEx)
                ConfermBill()
            End If

        End If
       
    End Sub

    Public Sub ConfermBill()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "ViewBill_ConfermBill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
            .Parameters.AddWithValue("@TOTAL", TOTAL)
            .Parameters.AddWithValue("@Discount", Disc)
            .Parameters.AddWithValue("@Pure", Pure)

        End With
        If SQL_SP_EXEC(c.Com) = True Then
            Switch_Dependcy(1)
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

    'Private Sub BackButton_Click(sender As Object, e As EventArgs)
    '    Me.Close()
    'End Sub

    Private Sub AGMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles AGMetroGrid.MouseDoubleClick
        FormType = 10
        If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And AGMetroGrid.Rows.Count > 0 Then
            If AGMetroGrid.SelectedRows.Count = 1 Then
                Change_IM_Details.ShowDialog()
            End If
        End If
    End Sub


    Private Sub AGMetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles AGMetroGrid.RowsAdded
        Calc_Total()
    End Sub

    Private Sub AGMetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles AGMetroGrid.RowsRemoved
        Calc_Total()
    End Sub

    Private Sub Calc_Total()
        TOTAL = 0
        If String.IsNullOrWhiteSpace(Discount_txt.Text) Then Disc = 0
        Dim QTY As Double = 0
        For i = 0 To AGMetroGrid.Rows.Count - 1
            TOTAL = TOTAL + AGMetroGrid.Rows(i).Cells("Total_CL").Value
            QTY += AGMetroGrid.Rows(i).Cells("QTY_CL").Value
        Next

        Total_TextBox.Text = TOTAL.ToString("N3")

        If Discount_Distribute = False Then
            Pure = (TOTAL - Disc)
        Else
            Pure = TOTAL
        End If

        Pure_txt.Text = Pure.ToString("N3")
        IM_Count_LB.Text = AGMetroGrid.Rows.Count.ToString + " : مواد "
        IM_Qty_LB.Text = QTY.ToString + " : كميات "

    End Sub

    Private Sub ViewBill_Contents_Delete_IM(T_ID_CL As Integer)
        Dim Row_Index As Integer = AGMetroGrid.CurrentCell.RowIndex
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "ViewBill_Contents_Delete_IM"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID_CL)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            Network_Edit_Tracker_insert(" الصنف:" & AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString & " الوحدة:" & AGMetroGrid.CurrentRow.Cells("IMUnit_CL").Value.ToString & _
           " العدد:" & AGMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString & " السعر:" & AGMetroGrid.CurrentRow.Cells("Price_CL").Value.ToString, Bill_ID_Txt.Text, 16, 2)
            ViewBill_Contents_SELECT_Bill()

            If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index - 1).Cells("EX_Name_CL")

            Save_Total(T_ID, TOTAL, Disc)
        End If
    End Sub

    Private Sub RemoveCatButton_Click(sender As Object, e As EventArgs) Handles RemoveCatButton.Click
        If AGMetroGrid.Rows.Count > 0 Then
            If MessageBox.Show(" حذف الصنف " + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                ViewBill_Contents_Delete_IM(AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
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
            Network_Edit_Tracker_insert(" (من شاشة فواتير العرض) الزبون:" & AG_Cm.Textt, 0, 27, 1)
            New_AG_ID = sqlComm.Parameters("@AG_ID").Value.ToString()

        End If
        Return New_AG_ID
    End Function


    Private Sub Discount_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Discount_txt.KeyDown

        If Discount_Distribute = False Then
            If Not String.IsNullOrWhiteSpace(Discount_txt.Text) Then
                Disc = Convert.ToDouble(Discount_txt.Text)
                Pure_txt.Text = TOTAL - Disc
                Pure = TOTAL - Disc
            End If
        Else
            If e.KeyCode = Keys.Return Then
                If String.IsNullOrWhiteSpace(Discount_txt.Text) Then Discount_txt.Text = "0"
                If SB_Discount_Distribute(Me.T_ID, Discount_txt.Text) = True Then
                    Disc = Discount_txt.Text
                    ViewBill_Contents_SELECT_Bill()
                End If
            End If
        End If

    End Sub


    Private Sub Discount_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Discount_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Discount_txt_KeyUp(sender As Object, e As KeyEventArgs) Handles Discount_txt.KeyUp
        If Discount_Distribute = False Then
            If String.IsNullOrWhiteSpace(Discount_txt.Text) Then
                Disc = 0
                Pure_txt.Text = TOTAL
            Else
                Disc = Convert.ToDouble(Discount_txt.Text)
                Pure_txt.Text = TOTAL - Disc
                Pure = TOTAL - Disc
            End If
        End If
    End Sub

    Private Sub Discount_txt_TextChanged(sender As Object, e As EventArgs) Handles Discount_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
        If String.IsNullOrWhiteSpace(Discount_txt.Text) Then Disc = 0
    End Sub

    Dim Tmp_Bill_ID As Integer
    Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
        Tmp_Bill_ID = Convert.ToInt64(Bill_ID_Txt.Text)
        Bill_ID_Txt.Text = Convert.ToInt64(Bill_ID_Txt.Text) - 1
        Get_T_ID()
    End Sub

    Public Sub Get_T_ID()

        Dim C As New C
            Dim S As String = ""
            If Search_By_Bar_CB.Checked = True Then
                S = "Select T_ID From Agents_Balance_MV Where Barcode = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "'"
            Else
                S = "Select T_ID From Agents_Balance_MV Where ViewSB_ID = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "'"
            End If

            C.Com = New SqlClient.SqlCommand(S, C.Con)
            C.Con.Open()
            Try
                C.Dr = C.Com.ExecuteReader
                If C.Dr.HasRows Then
                    C.Dr.Read()
                    ClearFields()
                    T_ID = C.Dr("T_ID")
                    ViewBill_Contents_SELECT_Bill()
                    Fill_Bill_Info()
                    SelectStateBt()
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
            Tmp_Bill_ID = Convert.ToInt64(Bill_ID_Txt.Text)
            Bill_ID_Txt.Text = Convert.ToInt64(Bill_ID_Txt.Text) + 1
            Get_T_ID()
        End If
    End Sub


    Private Sub Bill_ID_Txt_Enter(sender As Object, e As EventArgs) Handles Bill_ID_Txt.Enter
        Tmp_Bill_ID = Convert.ToInt64(Bill_ID_Txt.Text)
    End Sub

    Private Sub AGMetroGrid_KeyDown(sender As Object, e As KeyEventArgs) Handles AGMetroGrid.KeyDown

        If e.KeyCode = Keys.Delete Then
            If AGMetroGrid.Rows.Count > 0 And AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
                If MessageBox.Show(" حذف الصنف " + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                    ViewBill_Contents_Delete_IM(AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
                End If
            End If
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
        F_ViewBill_IM_card = New ViewBill_IM_card
        F_ViewBill_IM_card.T_ID = T_ID
        F_ViewBill_IM_card.AG_ID = AG_ID
        F_ViewBill_IM_card.ShowDialog()
        'ADD_IM()
    End Sub

    Private Sub Show_AG_Balance()
        Me.Cursor = Cursors.AppStarting
        Sales_AG_ID = AG_ID
        Sales_AG_Name = AG_Cm.Textt
        F_Balances = New Balances
        F_Balances.ShowDialog()
        Me.Cursor = Cursors.Default
        Sales_AG_ID = 0
    End Sub

    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        If AGMetroGrid.Rows.Count > 0 Then
            Me.Cursor = Cursors.AppStarting
            CashPrint(Sales_ViewPage_Bill_Track)
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Public Sub CashPrint(Sales_ViewPage_Bill_Track As String)

        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & Sales_ViewPage_Bill_Track)
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, Me.T_ID)
            .rp.SetParameterValue(1, SBill_Title_1)
            .rp.SetParameterValue(2, SBill_Title_2)
            .rp.SetParameterValue(3, SBill_Footer)
            .rp.SetParameterValue(4, IM_Qty_LB.Text)
            .rp.SetParameterValue(5, IM_Count_LB.Text)
            .rp.SetParameterValue(6, HANY(Val(Pure), "EGYPT"))
            .rp.SetParameterValue(7, "")
            .rp.SetParameterValue(8, Notes_txt.Text)

            If Project_cm.SelectedValue <= 1 Then
                .rp.SetParameterValue(10, "")
            Else
                .rp.SetParameterValue(10, " الزبون : " + Project_cm.Text)
            End If
        End With

        If Show_Bill_CB.Checked = False Then
            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_A4))
            pp.rp.PrintOptions.PrinterName = Default_Printer_A4
            pp.rp.PrintToPrinter(1, False, 0, 0)
            pp.rp.Dispose()
        Else
            Dim p As New print
            p.CrystalReportViewer1.ReportSource = pp.rp
            p.Show()
        End If


    End Sub


    Private Sub SBPauseBtn_Click(sender As Object, e As EventArgs)
        Beep()
        If MessageBox.Show(" تعليق الفاتورة " + Bill_ID_Txt.Text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) _
             = Windows.Forms.DialogResult.OK Then
            SB_PauseBill()
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
        Call_New_Bill()
    End Sub

    Private Sub Call_New_Bill()
        If T_ID > 0 Then
            If MessageBox.Show("فتح فاتورة جديدة", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information,
                               MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                ResetNewBill()
            End If
        Else
            ResetNewBill()
        End If
    End Sub

    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click


        If Edit_butt.BackColor = Color.WhiteSmoke Then
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
                Enable_Fields()
                Edit_butt.Text = "إيقاف التعديل"
            End If
        Else
            Save_Total(T_ID, TOTAL, Disc)
            Save_About(T_ID, Notes_txt.Text)
            Save_Date(T_ID, DateTimeEx)
            On_Update = False
            Edit_butt.Text = EditState
            Edit_butt.BackColor = Color.WhiteSmoke
            Notes_txt.Enabled = False
            DateTimeEx.Enabled = False
            Project_cm.Enabled = False
            Disable_Fields()
            SelectStateBt()
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles DGV_Control_btn.Click
        Switch_To_DV_Show()
    End Sub

    Private Sub تعديلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تعديلToolStripMenuItem.Click
        If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And AGMetroGrid.Rows.Count > 0 And On_Update = False Then Change_IM_Details.ShowDialog()
    End Sub

    Private Sub عرضالتكلفةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضالتكلفةToolStripMenuItem.Click
        Show_IM_Cost(True, Me.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value, Me.AGMetroGrid.CurrentRow.Cells("U_ID_CL").Value)
    End Sub

    Public Function Show_IM_Cost(Show_Msg As Boolean, IM_ID As Integer, U_ID As Integer)
        Dim c As New C
        Dim Cost As Double = 0
        Try
            Dim s As String
            s = "select ISNULL(Cost,0) AS Cost , U_Cargo  from IM_Menu_Units_V WHERE IM_ID = '" & IM_ID & "' AND U_ID = '" & U_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Cost = c.Dr("Cost") * c.Dr("U_Cargo")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        If Show_Msg = True Then MsgBox("  الوحدة : " + AGMetroGrid.CurrentRow.Cells("IMUnit_CL").Value + vbNewLine + "  التكلفة : " + Cost.ToString, MsgBoxStyle.Information, " تكلفة الصنف : " + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value)
        Return Cost
    End Function

    Private Sub Search_By_Bar_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Search_By_Bar_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub



    Private Sub إضافةكعميلجديدToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إضافةكعميلجديدToolStripMenuItem.Click
        ADD_Fast_AG()
    End Sub

    Private Sub كشفحسابالعميلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles كشفحسابالعميلToolStripMenuItem.Click
        Show_AG_Balance()
    End Sub

    Private Sub عرضرصيدالعميلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضرصيدالعميلToolStripMenuItem.Click
        MsgBox(AG_Balance.ToString("n"), MsgBoxStyle.Information, "رصيد العميل : " & AG_Cm.Textt)
    End Sub

    Private Sub إرسالالفاتورةللبريدالإلكترونيToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إرسالالفاتورةللبريدالإلكترونيToolStripMenuItem.Click
        Type_Of_E_mail = 2
        Me.Cursor = Cursors.AppStarting
        SendingOptions.ShowDialog()
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub Calc_Dicount_Btn_Click(sender As Object, e As EventArgs) Handles Calc_Dicount_Btn.Click
        If Discount_Distribute = False Then
            If Not String.IsNullOrWhiteSpace(Discount_txt.Text) Then
                Disc = Convert.ToDouble(Discount_txt.Text)
                Pure_txt.Text = TOTAL - Disc
                Pure = TOTAL - Disc
            End If
        Else
            If String.IsNullOrWhiteSpace(Discount_txt.Text) Then Discount_txt.Text = "0"
            If SB_Discount_Distribute(Me.T_ID, Discount_txt.Text) = True Then
                Disc = Discount_txt.Text
                ViewBill_Contents_SELECT_Bill()
            End If
        End If
        Update_Discount(T_ID, Discount_txt.Text)
        Network_Edit_Tracker_insert(" تخفيض للفاتورة بقيمة:" & Disc.ToString, Bill_ID_Txt.Text, 16, 3)

    End Sub


    Private Sub Show_AG_Projects_btn_Click(sender As Object, e As EventArgs) Handles Show_AG_Projects_btn.Click
        AG_Projects.ShowDialog()
    End Sub

    Private Sub Project_cm_KeyDown(sender As Object, e As KeyEventArgs) Handles Project_cm.KeyDown
        If e.KeyCode = Keys.Return Then Save_Pro()
    End Sub

    Public Sub Save_Pro()

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Update_AG_Project"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@Proj_ID", Project_cm.SelectedValue)
        SQL_SP_EXEC(sqlComm)
    End Sub

    Private Sub Show_Bill_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Show_Bill_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Move_To_SB_btn_Click(sender As Object, e As EventArgs) Handles Move_To_SB_btn.Click

        If AGMetroGrid.Rows.Count > 0 Then
            If IM_min_QTY = False Then
                If IM_Check_Neg_QTY_For_ViewBill() = 1 Then
                    'MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
                    MsgBox(" لا يمكن سحب كمية بالسالب للصنف  " & Str_Name, MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            End If
        End If

        If MessageBox.Show(" تحويل الفاتورة إلى فاتورة مبيعات نهائية ", "", MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
            F_Sales.Call_New_Bill()
            ViewBill_Move_IM_To_SB()
        End If

    End Sub

    Private Function IM_Check_Neg_QTY_For_ViewBill()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "[IM_Check_Neg_QTY_For_ViewBill]"
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

    Public Sub ViewBill_Move_IM_To_SB()
        Try
            Dim sqlComm As New SqlClient.SqlCommand
            sqlComm.CommandText = "[ViewBill_Move_IM_To_SB]"
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.AddWithValue("@SB_T_ID", F_Sales.T_ID)
            sqlComm.Parameters.AddWithValue("@View_Bill_T_ID", T_ID)
            sqlComm.Parameters.AddWithValue("@TOTAL", TOTAL)
            sqlComm.Parameters.AddWithValue("@Disc", Disc)
            SQL_SP_EXEC(sqlComm)
            MsgBox("تم تحويل الفاتورة إلى فاتورة مبيعات رقم " & F_Sales.SB_ID.ToString, MsgBoxStyle.Information, "")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            query("UPDATE ViewBill_Contents SET isDepended = 1 where Bill_T_ID = " & T_ID)
            MsgBox("تم حـــجز البضاعـــة للزبـــون", MsgBoxStyle.Information, "")

        Catch ex As Exception

        End Try

    End Sub

    Private Sub END_RSV_DATE_ValueChanged(sender As Object, e As EventArgs) Handles END_RSV_DATE.ValueChanged
        If END_RSV_DATE.Checked = True Then
            AG_Balance_Update_Deliver_date(END_RSV_DATE)
        Else
            query("UPDATE Agents_Balance_MV SET Deliver_date = NULL where T_ID = " & T_ID)
        End If

    End Sub

    Private Sub تحديدنوعالطباعـــةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تحديدنوعالطباعـــةToolStripMenuItem.Click
        Dim dialog As New SingleChoiceDialog()
        dialog.Text = "حدد نوع الطباعة"
        ' أضف الخيارات إلى ListBox
        'dialog.ListBox1.Items.AddRange(New String() {"Option 1", "Option 2", "Option 3"})

        Dim c2 As New C
        c2.Str = "select ID,Type from Sales_ViewBill_Page"
        c2.Da = New SqlClient.SqlDataAdapter(c2.Str, c2.Con)
        c2.Da.Fill(c2.Dt)
        dialog.ListBox1.DataSource = c2.Dt
        dialog.ListBox1.DisplayMember = "Type"
        dialog.ListBox1.ValueMember = "ID"
        dialog.ListBox1.SelectedIndex = 0

        ' عرض مربع الحوار
        If dialog.ShowDialog() = DialogResult.OK Then CashPrint(get_prt_path(dialog.ListBox1.SelectedValue))

    End Sub
    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    ' --- 2. زر تكبير واستعادة الفورم (ذكي وبدون تغطية شريط المهام) ---
    Private Sub MaxFormButton_Click(sender As Object, e As EventArgs) Handles MaxFormButton.Click
        If Me.WindowState = FormWindowState.Normal Then
            ' 💡 السر هنا: تحديد حجم مساحة العمل فقط (WorkingArea)
            Me.MaximumSize = Screen.FromHandle(Me.Handle).WorkingArea.Size
            Me.WindowState = FormWindowState.Maximized
            MaxFormButton.Text = "❐" ' تغيير الرمز لاستعادة
        Else
            Me.WindowState = FormWindowState.Normal
            MaxFormButton.Text = "⬜" ' تغيير الرمز لتكبير
        End If
    End Sub

    ' --- 3. زر تصغير الفورم لشريط المهام ---
    Private Sub MinFormButton_Click(sender As Object, e As EventArgs) Handles MinFormButton.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    ' --- 4. حركة الفورم (السحب والإفلات) عبر شريط العنوان ---
    Private drag As Boolean
    Private mouseX As Integer
    Private mouseY As Integer

    Private Sub TitleBar_Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseDown, Title_LB.MouseDown
        drag = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub TitleBar_Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseMove, Title_LB.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub

    Private Sub TitleBar_Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseUp, Title_LB.MouseUp
        drag = False
    End Sub

    Private Function get_prt_path(Sales_Page_ID As Integer)

        Dim C As New C
        Dim S As String = ""
        Dim result As String = ""

        S = "Select AG_Bill From Sales_ViewBill_Page Where ID = " & Sales_Page_ID

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


    Private Sub AG_Cm_ID_Changed(sender As Object, e As EventArgs) Handles AG_Cm.ID_Changed
        If AG_Cm.TXT_ID.Text > 0 Then
            Fetch_ItemToList2()
            'AG_Label.Text = "رصيد الحســاب: ( " & GET_AG_Balance().ToString & " ) "
        End If
    End Sub

    Public Sub Fetch_ItemToList2()
        If AG_Cm.TXT_ID.Text > 0 Then

            PREV_AG_ID = AG_ID
            AG_ID = AG_Cm.TXT_ID.Text

            Save_AG_Name(T_ID, AG_ID, On_Update)
                Network_Edit_Tracker_insert(" تعديل الفاتورة إلي حساب " & AG_Cm.Textt, SB_ID, 1, 3)

                Fill_All_AG_Proj()
            End If

    End Sub


End Class