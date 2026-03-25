
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
    Public Receipts_DT As New DataTable

    Public isCashReceipt_Success As Boolean = False
    Public isShowingDetails As Boolean = False

    Dim IM_ID As Integer = 0

    Dim IM_Dt As New DataTable
    Dim IM_QTY As Double = 0
    Public TOTAL As Double = 0
    Public Disc As Double = 0
    Public Pure As Double = 0
    Public AG_ID As Integer = 0
    Dim AG_Dt As New DataTable
    Dim U_Dt As New DataTable
    Dim Get_Unit As Boolean = False
    Dim U_Cargo As Double = 0
    Dim ALL_QTY As Double = 0
    Dim Valid_Dt As New DataTable
    Public isNewBill As Integer
    Public Barcode As String
    Dim isPied As Integer = 0
    Dim BillUser_ID As Integer

    Dim On_Update As Boolean
    Dim U_ID As Integer

    Public IS_Show_Rctp As Boolean = False

    Dim AG_Balance As Double = 0
    Public Barcode_IM As String = ""

    Dim Min_SP As Double
    Dim Min_SP_2 As Double
    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        'Update_Total()
        FormType = 0
        'F_MainForm.Fill_ALL_IM()
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
            Case Keys.F5
                IM_SH_txt.Select()
            Case Keys.F8
                Barcode_SH_txt.Select()
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

        'If Tm = "" Then
        '    Timer1.Start()
        '    Tick = 0
        '    ' Barcode_SH_txt.Focus()
        'End If
        'Select Case e.KeyChar
        '    Case "0" To "9"
        '        Tm = Tm + e.KeyChar
        '    Case "a" To "z"
        '        Tm = Tm + e.KeyChar
        'End Select
    End Sub

    Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        FormType = 10
        If St_Count() = 1 Then All_St_Panel.Visible = False
        Check_View_Control()
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        EditState = Edit_butt.Text
        Load_ST()


        Delete_butt.Visible = U_SalesVoid

        If U_SalesDis = True And isDiscount = True Then
            DiscountPanel.Visible = True
        Else
            DiscountPanel.Visible = False
        End If


        Edit_butt.Visible = U_SB_Update


            If U_SB_IM_Update = True Then
            PriceTextBox.ReadOnly = False
        Else
            PriceTextBox.ReadOnly = True
        End If

        Min_SP_CB.Visible = U_Sell_Under_Min_SP
        Min_SP_2_CB.Visible = U_Sell_Under_Min_SP_2

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
        Try
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
        Catch ex As Exception
            '...
        End Try
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
        Min_SP_Panel.Visible = S_Allow_MinSP
    End Sub

    Public Sub Load_ST()
        Dim c As New C
        Try
            Dim s As String
            s = "select ST_ID,ST_name from STORES ORDER By ST_ID ASC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            ST_cm.DataSource = c.Dt
            ST_cm.DisplayMember = "ST_name"
            ST_cm.ValueMember = "ST_ID"
            ST_cm.SelectedValue = SB_ST_ID
            If SB_ST_Can_change = False Then ST_cm.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
            GET_AG()

            DateTimeEx.Text = C.Dr("Date")

            '  Barcode = C.Dr("Barcode")

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
            BillUser_ID = C.Dr("User_ID")
            Notes_txt.Text = C.Dr("About")

            Fill_All_AG_Proj()
            Project_cm.SelectedValue = C.Dr("Proj_ID")

        Else
            AG_ID = Default_AG_ID
            ' AG_SH_txt.Text = "نقدي"
            Fetch_ItemToList2()
            VoidLb.Enabled = False
        End If
        C.Con.Close()
    End Sub


    Private Sub Enable_Fields()
        AG_SH_txt.Enabled = True
        DateTimeEx.Enabled = True
        AG_SH_txt.Enabled = True
        Notes_txt.Enabled = True
        Ebable_CatFields()
        Project_cm.Enabled = True
        Discount_txt.Enabled = True
        Calc_Dicount_Btn.Enabled = True
    End Sub

    Private Sub Disable_Fields()
        AG_SH_txt.Enabled = False
        DateTimeEx.Enabled = False
        ' AddFastAGButton.Enabled = False
        AG_SH_txt.Enabled = False
        Notes_txt.Enabled = False
        Disable_CatFields()
        Project_cm.Enabled = False
        Discount_txt.Enabled = False
        Calc_Dicount_Btn.Enabled = False

    End Sub

    Private Sub Disable_CatFields()
        IM_SH_txt.Enabled = False
        Show_IM_btn.Enabled = False
        Barcode_SH_txt.Enabled = False
        QtyTextBox.Enabled = False
        PriceTextBox.Enabled = False
        ADDCatButton.Enabled = False
        RemoveCatButton.Enabled = False
    End Sub

    Private Sub Ebable_CatFields()
        IM_SH_txt.Enabled = True
        Show_IM_btn.Enabled = True
        Barcode_SH_txt.Enabled = True
        QtyTextBox.Enabled = True
        PriceTextBox.Enabled = True
        ADDCatButton.Enabled = True
        RemoveCatButton.Enabled = True
    End Sub


    Public Sub Switch_Dependcy(F As Boolean)

        If F = True Then
            isDepended = 1

            AGMetroGrid.BackgroundColor = Color.LightGreen
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
            AG_SH_txt.Enabled = False
            '  AddFastAGButton.Enabled = False
            Save_butt.Enabled = False
        Else
            isDepended = 0
            AGMetroGrid.BackgroundColor = Color.LightYellow
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
            AG_SH_txt.Enabled = True
            ' AddFastAGButton.Enabled = True
            Save_butt.Enabled = True
        End If

    End Sub

    Private Sub NewStateBt()
        Enable_Fields()
        Save_butt.Enabled = True
        ' Delete_butt.Enabled = False
        Me.Text = "فاتورة عرض جديدة"
        AG_Grid.Visible = False
        AG_SH_txt.Enabled = True
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

        If My_Settings.S_Default = 0 Then
            Barcode_SH_txt.Select()
        Else
            IM_SH_txt.Select()
        End If

        Me.Text = "عرض بيانات فاتورة"

        'If BillUser_ID <> USER_ID Then
        '    If User_isAdmin = False Then
        '        AGMetroGrid.BackgroundColor = Color.Gray
        '        AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.Gray
        '        Delete_butt.Enabled = False
        '        Save_butt.Enabled = False
        '        Disable_CatFields()
        '    End If
        'End If
        Get_Rtn_Count()
    End Sub

    Public Function Get_Reciept_ID()
        Dim c As New C
        Try
            Dim s As String
            s = "select T_ID from Receipts_V WHERE Receipt_Tran_ID = '" & T_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Return c.Dr("T_ID")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return 0
    End Function

    Public Sub Get_Rtn_Count()
        Dim c As New C
        Dim N As Double = 0
        Try
            Dim s As String
            s = "select ISNULL(SUM(T_PRICE),0) AS S from Rtn_Contents WHERE Orginal_Bill_T_ID = '" & T_ID & "' AND isDepended=1"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                N = c.Dr("S")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ClearFields()
        isCashReceipt_Success = False
        T_ID = 0
        Notes_txt.Clear()
        PriceTextBox.Clear()
        Total_TextBox.Clear()
        Receipts_DT.Clear()
        Discount_txt.Clear()
        Receipts_DT.Clear()
        DateTimeEx.Text = Date.Now
        VoidLb.Visible = False
        isVoid = False
        isDepended = False
        ClearCatFields()
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
        sqlComm.Parameters.AddWithValue("@AG_ID", 1)
        sqlComm.Parameters.AddWithValue("@Date", Me.DateTimeEx.Value)
        sqlComm.Parameters.AddWithValue("@BsType_ID", 16)
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters("@ViewSB_ID").Direction = ParameterDirection.Output
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then
            Me.Bill_ID_Txt.Text = sqlComm.Parameters("@ViewSB_ID").Value.ToString()
            T_ID = sqlComm.Parameters("@T_ID").Value.ToString()
            ViewBill_Contents_SELECT_Bill()
            '  Fill_Bill_Info()
            Switch_Dependcy(0)
            If My_Settings.S_Default = 0 Then
                Barcode_SH_txt.Select()
            Else
                IM_SH_txt.Select()
            End If
        End If
    End Sub

    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        If AGMetroGrid.Rows.Count > 0 Then
            If String.IsNullOrWhiteSpace(AG_SH_txt.Text) = False And AG_ID = 0 Then
                MsgBox("حدد إسم العميل", MsgBoxStyle.Critical, "خطأ في الإعتماد")
                AG_SH_txt.Select()
            Else
                If String.IsNullOrWhiteSpace(AG_SH_txt.Text) Then
                    '   AG_SH_txt.Text = "نقدي"
                    AG_ID = Default_AG_ID
                End If
                Beep()
                Save_AG_Name(T_ID, AG_ID, False)
                Save_About(T_ID, Notes_txt.Text)
                Save_Date(T_ID, DateTimeEx)
                ConfermBill()
            End If

            isCashReceipt_Success = False
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
            'If SB_AutoPrint = True Then
            '    Me.Cursor = Cursors.AppStarting
            '    CashPrint(Sales_BillPage_Bill_Track, Sales_Page_ID)
            '    Me.Cursor = Cursors.Default
            'End If
            SelectStateBt()
            If My_Settings.S_Default = 0 Then
                Barcode_SH_txt.Select()
            Else
                IM_SH_txt.Select()
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

        Total_TextBox.Text = TOTAL.ToString("N2")

        If Discount_Distribute = False Then
            Pure = (TOTAL - Disc)
        Else
            Pure = TOTAL
        End If

        Pure_txt.Text = Pure.ToString("N2")
        IM_Count_LB.Text = AGMetroGrid.Rows.Count.ToString + " : مواد "
        IM_Qty_LB.Text = QTY.ToString + " : كميات "

    End Sub


    Public Sub ADD_IM()
        If IM_ID = 0 Then
            MsgBox("حددالصنف", MsgBoxStyle.Exclamation)
            IM_SH_txt.Select()
        ElseIf String.IsNullOrWhiteSpace(PriceTextBox.Text) Then
            MsgBox("حدد سعر القطعة", MsgBoxStyle.Exclamation)
            PriceTextBox.Select()
        Else
            If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"

            If U_SB_Sell_Under_Cost = False Then
                If Show_IM_Cost(False, IM_ID, U_ID) > PriceTextBox.Text Then
                    MsgBox("لا يمكنك البيع بأقل من سعر التكلفة", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If

            'If IM_min_QTY = False Then
            '    If IM_Check_Neg_QTY_() = 1 Then
            '        MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
            '        Exit Sub
            '    End If
            'End If


            If SB_IM_Alert_When_Repetition = True Then
                For i = 0 To AGMetroGrid.Rows.Count - 1
                    If AGMetroGrid.Rows(i).Cells("Bill_IMID_CL").Value = IM_ID Then
                        Beep()
                        If MessageBox.Show(" هذا الصنف تم إدراجه بالفاتورة ... هل تريد الإستمرار ؟ ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                            Exit Sub
                        Else
                            Add_ItemToBill(IM_ID)
                            Exit Sub
                        End If
                    End If
                Next
            End If


            If Valid_Panel.Visible = True And Valid_cm.Items.Count > 1 Then
                Beep()
                If MessageBox.Show(" يوجد من هذا الصنف أكثر من صلاحية وانت اخترت صلاحية  " + vbNewLine + Valid_cm.Text + vbNewLine + " هل تريد الإستمرار ؟ ", "", _
                                   MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If
            End If

            Add_ItemToBill(IM_ID)
        End If
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


    'Public Sub Update_Total()
    '    If String.IsNullOrWhiteSpace(Total_TextBox.Text) = False Then
    '        'Save_Total(T_ID, TOTAL, Disc)
    '    End If
    'End Sub

    Private Sub ClearCatFields()
        IM_ID = 0
        IM_SH_txt.Clear()
        Barcode_SH_txt.Clear()
        Current_QTY.Clear()
        PriceTextBox.Clear()
        QtyTextBox.Clear()
        U_Dt.Clear()
        Barcode_IM = ""
    End Sub


    Public Sub Add_ItemToBill(IM_ID As String)
        If String.IsNullOrWhiteSpace(Barcode_IM) Then Barcode_IM = SELECT_BARCODE(IM_ID, U_ID)
        'Dim Row_Index As Integer = 0
        'If AGMetroGrid.Rows.Count > 0 Then Row_Index = AGMetroGrid.CurrentCell.RowIndex + 1
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "ViewBill_Contents_INSERT_2"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@SB_T_ID", Me.T_ID)
            .Parameters.AddWithValue("@IM_ID", IM_ID)
                .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
                If Valid_Panel.Visible = True Then
                    .Parameters.AddWithValue("@D_Vaild", Valid_cm.Text)
                    .Parameters.AddWithValue("@Current_QTY", Current_QTY.Text)
                End If
            .Parameters.AddWithValue("@Barcode", Barcode_IM)
                If String.IsNullOrWhiteSpace(QtyTextBox.Text) = False Then .Parameters.AddWithValue("@QYT", Convert.ToDouble(QtyTextBox.Text))
                .Parameters.AddWithValue("@U_ID", U_ID)
                .Parameters.AddWithValue("@Price", PriceTextBox.Text)
        End With

        If SQL_SP_EXEC(C.Com) = True Then
            Network_Edit_Tracker_insert(" الصنف:" & IM_SH_txt.Text & " الوحدة:" & IM_Unit_cm.Text & " العدد:" & QtyTextBox.Text & " السعر:" & PriceTextBox.Text, Bill_ID_Txt.Text, 16, 1)

            If On_Update = True Then DependingUpdatedBill(T_ID)
            ViewBill_Contents_SELECT_Bill()
            ClearCatFields()
            Save_Total(T_ID, TOTAL, Disc)
            'If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index).Cells("EX_Name_CL")
            If MY_Settings.S_Default = 0 Then
                Barcode_SH_txt.Select()
            Else
                IM_SH_txt.Select()
            End If
        End If

    End Sub

    'Public Function Get_GM_ST_ID()
    '    Dim c As New C
    '    Try
    '        Dim s As String
    '        s = "select ISNULL(POS_ST_ID,1) AS POS_ST_ID from IM_GM_ST_ID_V WHERE IM_ID = '" & IM_ID & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            Return c.Dr("POS_ST_ID")
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    '    Return 1
    'End Function


    Private Sub ViewBill_Contents_Delete_IM(T_ID_CL As Integer)
        Dim Row_Index As Integer = AGMetroGrid.CurrentCell.RowIndex
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "ViewBill_Contents_Delete_IM"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID_CL)
            '   .Parameters.AddWithValue("@On_Update", On_Update)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            Network_Edit_Tracker_insert(" الصنف:" & AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString & " الوحدة:" & AGMetroGrid.CurrentRow.Cells("IMUnit_CL").Value.ToString & _
           " العدد:" & AGMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString & " السعر:" & AGMetroGrid.CurrentRow.Cells("Price_CL").Value.ToString, Bill_ID_Txt.Text, 16, 2)
            ViewBill_Contents_SELECT_Bill()

            If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index - 1).Cells("EX_Name_CL")
            If MY_Settings.S_Default = 0 Then
                Barcode_SH_txt.Select()
            Else
                IM_SH_txt.Select()
            End If
            'Update_Total()
            Save_Total(T_ID, TOTAL, Disc)
        End If
    End Sub

    Private Sub PriceTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles PriceTextBox.KeyDown
        Select Case e.KeyCode
            Case Keys.Up : IM_SH_txt.Select()
            Case Keys.Down : If AGMetroGrid.Rows.Count > 0 = True Then AGMetroGrid.Select()
            Case Keys.Right
                IM_Unit_cm.Select()
                IM_Unit_cm.DroppedDown = True
            Case Keys.Return : QtyTextBox.Select()
        End Select
    End Sub

    Private Sub PriceTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PriceTextBox.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub PriceTextBox_TextChanged(sender As Object, e As EventArgs) Handles PriceTextBox.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub QtyTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles QtyTextBox.KeyDown

        Select Case e.KeyCode
            Case Keys.Return : ADD_IM()
            Case Keys.Up : Barcode_SH_txt.Select()
            Case Keys.Down : If AGMetroGrid.Rows.Count > 0 = True Then AGMetroGrid.Select()
            Case Keys.Right : PriceTextBox.Select()
            Case Keys.Left
                If Valid_Panel.Visible = True Then
                    Valid_cm.DroppedDown = True
                    Valid_cm.Select()
                End If
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
                ViewBill_Contents_Delete_IM(AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
            End If
        End If
    End Sub


    'Private Sub AddFastAGButton_Click(sender As Object, e As EventArgs) Handles AddFastAGButton.Click
    '    ADD_Fast_AG()
    'End Sub

    Private Sub ADD_Fast_AG()
        If AG_ID <> Default_AG_ID Then
            MsgBox("هذا العميل موجود بالفعل", MsgBoxStyle.Critical, "إضافة عميل")
        ElseIf String.IsNullOrWhiteSpace(AG_SH_txt.Text) Then
            MsgBox("أدخل اسم العميل الجديد", MsgBoxStyle.Exclamation)
            AG_SH_txt.Select()
        Else
            Beep()
            If MessageBox.Show(" إضافة " + AG_SH_txt.Text + " إلى قائمة العملاء ", " إضافة العميل ", MessageBoxButtons.YesNo, _
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
        sqlComm.Parameters.AddWithValue("@Ag_name", AG_SH_txt.Text)
        sqlComm.Parameters.AddWithValue("@Barcode", "")
        sqlComm.Parameters.AddWithValue("@Type_ID", Cr_Type_ID)
        sqlComm.Parameters.AddWithValue("@E_mail", "")
        sqlComm.Parameters("@AG_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تمت إضافة العميــل", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" (من شاشة فاتورة عرض) الزبون:" & AG_SH_txt.Text, 0, 27, 1)
            New_AG_ID = sqlComm.Parameters("@AG_ID").Value.ToString()
            Load_AG()
        End If
        Return New_AG_ID
    End Function


    Public Sub Load_IM()
        Dim c As New C

        Try
            IM_Dt.Clear()
            c.Str = IM_Serach(IM_SH_txt.Text)
            c.Da = New SqlClient.SqlDataAdapter(c.Str, c.Con)
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
                Search_From_Grid()
            Case Keys.Down
                If IMDataGridViewX.Visible = True Then
                    IMDataGridViewX.Select()
                Else
                    QtyTextBox.Select()
                End If
            Case Keys.Left : Barcode_SH_txt.Select()
            Case Keys.Delete : IM_SH_txt.Clear()
        End Select


    End Sub

    Public Sub Search_From_Grid()
        If IMDataGridViewX.Visible = True Then
            Fetch_ItemToList()
        Else
            QtyTextBox.Select()
        End If
    End Sub


    Private Sub IM_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles IM_SH_txt.TextChanged
        If IM_SH_txt.Text.Count > 0 Then
            Load_IM()
        Else
            IMDataGridViewX.Visible = False
            IM_ID = 0
            U_Dt.Clear()
            Current_QTY.Clear()
            ALL_QTY_txt.Clear()
            PriceTextBox.Clear()
            Valid_QTY_txt.Clear()
            Valid_Dt.Clear()
            QtyTextBox.Clear()
        End If
        If IM_ID = 0 Then
            IM_SH_txt.BackColor = Color.LightGray
        Else
            IM_SH_txt.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub

    Private Sub IM_SH_txt_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles IM_SH_txt.MouseDoubleClick
        Items_Search.ShowDialog()
        If GLOBAL_IM_ID > 0 Then
            Load_IM_By_ID(IMDataGridViewX)
            For i = 0 To IMDataGridViewX.Rows.Count - 1
                If IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value = GLOBAL_IM_ID Then
                    Exit For
                End If
            Next
            Fetch_ItemToList()
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
            Get_Unit = False
            Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
            Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
            Fetch_IM_Units()
            IMDataGridViewX.Visible = False
            QtyTextBox.Select()

            If IMDataGridViewX.CurrentRow.Cells("isValid_CL").Value = 1 Then
                Valid_Panel.Visible = True
                Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
                IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
            Else
                Valid_Panel.Visible = False
            End If
        End If
    End Sub

    Private Sub Fetch_IM_Units()
        Get_Unit = False
        Dim c As New C
        U_Dt.Clear()
        Try
            Dim s As String
            s = "select U_IM_ID,U_Name from IM_Menu_Units_V  WHERE IM_ID = '" & IM_ID & "' Order By U_Cargo Asc"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(U_Dt)
            IM_Unit_cm.DataSource = U_Dt
            IM_Unit_cm.DisplayMember = "U_Name"
            IM_Unit_cm.ValueMember = "U_IM_ID"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Get_Unit = True
        IM_Fetch_QTY()
    End Sub

    Private Sub Show_IM_btn_Click(sender As Object, e As EventArgs) Handles Show_IM_btn.Click
        If IMDataGridViewX.Visible = True Then
            IMDataGridViewX.Visible = False
        Else
            Load_ALL_IM()
        End If
    End Sub

    Public Sub Load_ALL_IM()
        Dim c As New C

        Try
            IM_Dt.Clear()
            Dim s As String
            s = "select IM_ID,item_name,isValid from IM_All_V  Order by item_name ASC"
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

    Private Sub Barcode_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_SH_txt.KeyDown
        Select Case e.KeyCode
            Case Keys.Return : If String.IsNullOrWhiteSpace(Barcode_SH_txt.Text) = False Then Load_IM_Barcode()
            Case Keys.Down : QtyTextBox.Select()
            Case Keys.Delete : Barcode_SH_txt.Clear()
                Barcode_IM = ""
        End Select
    End Sub

    Public Sub Load_IM_Barcode()
        If S_is_Multi_BAR = True Then
            If Check_IF_Multi_BAR() > 1 Then
                SELECT_Multi_Bar()
                Exit Sub
            End If
        End If

        Dim c As New C
        IM_Dt.Clear()
        Try
            Dim s As String
            If Sh_ByNum_CB.Checked = True Then
                s = "select IM_ID,item_name,isValid from IM_All_V WHERE IM_NUM = '" & Barcode_SH_txt.Text & "'"
            Else
                s = "select U_IM_ID,IM_ID,item_name,isValid from IM_units_Search_V WHERE Barcode = '" & Barcode_SH_txt.Text & "'"
            End If

            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                IM_ID = c.Dr("IM_ID")
                IM_SH_txt.Text = c.Dr("item_name")
                If Sh_ByNum_CB.Checked = False Then Barcode_IM = Barcode_SH_txt.Text

                Get_Unit = False
                Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
                Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
                IMDataGridViewX.Visible = False

                If c.Dr("isValid") = 1 Then
                    Valid_Panel.Visible = True
                    Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
                Else
                    Valid_Panel.Visible = False
                End If

                QtyTextBox.Select()
                Fetch_IM_Units()
                If Sh_ByNum_CB.Checked = False Then IM_Unit_cm.SelectedValue = c.Dr("U_IM_ID")
                Barcode_SH_txt.Clear()
                If My_Settings.S_CodeAdd_1 = True Then ADD_IM()
            Else
                MsgBox("لم يتم التعرف على الإدخال ", MsgBoxStyle.Exclamation)
                Barcode_IM = ""
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function Check_IF_Multi_BAR()
        Dim c As New C
        IM_Dt.Clear()
        Try
            Dim s As String
            s = "select COUNT(U_IM_ID) AS C from IM_units_Search_V WHERE Barcode = '" & Barcode_SH_txt.Text & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Return c.Dr("C")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return 1
    End Function

    Private Sub SELECT_Multi_Bar()
        Dim c As New C
        Try
            IM_Dt.Clear()
            Dim s As String
            s = "select IM_ID,item_name,isValid from IM_All_BY_BAR_V WHERE Barcode  = '" & Barcode_SH_txt.Text & "' Order by item_name ASC"
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


    Public Sub Load_AG()
        Dim c As New C

        Try
            AG_Dt.Clear()
            Dim s As String
            s = "select AG_ID,Ag_name,ISNULL(T_Balance,0) AS T_Balance from Agents WHERE Ag_name Like '%" & AG_SH_txt.Text & "%' AND Type_ID IN ('" & Cr_Type_ID & "','" & General_AG_Type_ID & "')"
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
        If e.KeyCode = Keys.Down Then
            AG_Grid.Select()
        End If

        If e.KeyCode = Keys.Return Then
            If AG_Grid.Visible = True Then Fetch_ItemToList2()
        End If

        If e.KeyCode = Keys.Delete Then AG_SH_txt.Clear()

    End Sub

    Private Sub AG_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles AG_SH_txt.TextChanged

        If AG_SH_txt.Text.Count > 0 Then
            Load_AG()
        Else
            AG_Grid.Visible = False
            AG_ID = Default_AG_ID
            Save_AG_Name(T_ID, AG_ID, False)
        End If

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
        If e.KeyCode = Keys.Return Then
            Fetch_ItemToList2()
        End If

        If e.KeyCode = Keys.Up Then
            If AG_Grid.CurrentRow.Index = 0 Then
                AG_SH_txt.Select()
            End If
        End If
    End Sub

    Public Sub Fetch_ItemToList2()
        If AG_Grid.Rows.Count > 0 Then
            AG_ID = AG_Grid.CurrentRow.Cells(0).Value
            AG_SH_txt.Text = AG_Grid.CurrentRow.Cells(1).Value
            AG_Balance = AG_Grid.CurrentRow.Cells(2).Value
            AG_SH_txt.BackColor = Color.LightGoldenrodYellow
            AG_Grid.Visible = False
            Save_AG_Name(T_ID, AG_ID, False)
            Fill_All_AG_Proj()
        End If
    End Sub

    Private Sub Show_IM_btn2_Click(sender As Object, e As EventArgs) Handles Show_AG_ALL_btn.Click
        If AG_Grid.Visible = True Then
            AG_Grid.Visible = False
        Else
            Fill_All_AG()
            AG_Grid.Visible = True
            AG_Grid.Visible = True
            AG_Grid.Size = New Point(AG_Grid.Size.Width, 530)
        End If
    End Sub

    Private Sub Fill_All_AG()
        Try
            Dim C As New C
            AG_Dt.Clear()
            Dim s As String = "SELECT AG_ID,Ag_name,ISNULL(T_Balance,0) AS T_Balance from AGENTS_MENU_V WHERE Type_ID IN ('" & Cr_Type_ID & "','" & General_AG_Type_ID & "') Order by Ag_name ASC"
            C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
            C.Da.Fill(AG_Dt)
            AG_Grid.DataSource = AG_Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub NewSalePrice_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
        Check_Only_Float(sender, e)
    End Sub

    Private Sub NewSalePrice_txt_TextChanged(sender As Object, e As EventArgs)
        Check_Point_in_FloatNum(sender, e)
    End Sub


    Private Sub NewSalePrice_txt_KeyDown(sender As Object, e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Return : If ADDCatButton.Enabled = True Then ADD_IM()
            Case Keys.Up : Barcode_SH_txt.Select()
            Case Keys.Right : PriceTextBox.Select()
        End Select
    End Sub

    Private Sub IM_Fetch_QTY()
        Dim c As New C
        Try
            Dim s As String
            s = "select U_ID,U_Cargo,Price,Min_SP,Min_SP_2  from IM_Menu_Units_V WHERE U_IM_ID = '" & IM_Unit_cm.SelectedValue & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                U_Cargo = c.Dr("U_Cargo")
                Dim N As Double = (Convert.ToDouble(IM_QTY) / c.Dr("U_Cargo"))
                Current_QTY.Text = N.ToString("N")
                PriceTextBox.Text = c.Dr("Price")
                ALL_QTY_txt.Text = ALL_QTY / U_Cargo
                U_ID = c.Dr("U_ID")


                If Min_SP_CB.Checked = True Then
                    PriceTextBox.Text = c.Dr("Min_SP")
                    If c.Dr("Min_SP") = 0 Then PriceTextBox.Clear()
                ElseIf Min_SP_2_CB.Checked = True Then
                    PriceTextBox.Text = c.Dr("Min_SP_2")
                    If c.Dr("Min_SP_2") = 0 Then PriceTextBox.Clear()
                End If

                Min_SP = c.Dr("Min_SP")
                Min_SP_2 = c.Dr("Min_SP_2")

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles IM_Unit_cm.SelectedValueChanged
        If String.IsNullOrWhiteSpace(Current_QTY.Text) = False And Get_Unit = True Then
            IM_Fetch_QTY()
            If Valid_Panel.Visible = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
        End If
    End Sub

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


    Private Sub IM_Unit_cm_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_Unit_cm.KeyDown
        Select Case e.KeyCode
            Case Keys.Return, Keys.Left : QtyTextBox.Select()
        End Select
    End Sub

    Dim Tmp_Bill_ID As Integer
    Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
        Tmp_Bill_ID = Convert.ToInt64(Bill_ID_Txt.Text)
        Bill_ID_Txt.Text = Convert.ToInt64(Bill_ID_Txt.Text) - 1
        Get_T_ID()
    End Sub

    Public Sub Get_T_ID()
        Try
            'Update_Total()

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
        Catch ex As Exception
            '...
        End Try
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
                If MessageBox.Show(" حذف الصنف " + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel, _
                                   MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                    ViewBill_Contents_Delete_IM(AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
                End If
            End If
        End If

        If e.KeyCode = Keys.Up Then
            If AGMetroGrid.Rows.Count > 0 Then If AGMetroGrid.CurrentRow.Index = 0 Then QtyTextBox.Select()
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
        ADD_IM()
    End Sub

    Private Sub Show_AG_Balance()
        Me.Cursor = Cursors.AppStarting
        Sales_AG_ID = AG_ID
        Sales_AG_Name = AG_SH_txt.Text
        F_Balances = New Balances
        F_Balances.ShowDialog()
        Me.Cursor = Cursors.Default
        Sales_AG_ID = 0
    End Sub


    'Private Sub Piedmoney_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub Piedmoney_txt_TextChanged(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
    'End Sub


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

    Private Sub ST_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles ST_cm.SelectedValueChanged
        If Get_Unit = True Then
            Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
            IM_Fetch_QTY()
            If Valid_Panel.Visible = True Then Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
        End If
    End Sub

    Private Sub Valid_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles Valid_cm.SelectedValueChanged
        If Get_Unit = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
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
                'Ebable_CatFields()
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



    Private Sub Valid_cm_KeyDown(sender As Object, e As KeyEventArgs) Handles Valid_cm.KeyDown
        Select Case e.KeyCode
            Case Keys.Return
                QtyTextBox.Select()
                ADD_IM()
            Case Keys.Right : QtyTextBox.Select()
        End Select
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles DGV_Control_btn.Click
     Switch_To_DV_Show()
    End Sub

    Private Sub BarCode_txt_TextChanged(sender As Object, e As KeyPressEventArgs) Handles Barcode_SH_txt.KeyPress
        If e.KeyChar = " " Then e.Handled = True
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



    'Private Sub ADD_New_IM_btn_Click(sender As Object, e As EventArgs)
    '    IM_ADD_New.ShowDialog()
    'End Sub

    Private Sub Sh_ByNum_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Sh_ByNum_CB.CheckedChanged
        CB_CHecked(sender)
        IMDataGridViewX.Columns("IM_NUM_CL").Visible = Sh_ByNum_CB.Checked
        Barcode_SH_txt.Select()
    End Sub

    Private Sub Search_By_Bar_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Search_By_Bar_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    'Private Sub OpenCahDR_Btn_Click(sender As Object, e As EventArgs)
    '    Open_Cash_Drawer()
    'End Sub

    'Private Sub Show_Cash_btn_Click(sender As Object, e As EventArgs)
    '    Fetch_Pr_Details_()
    'End Sub

    'Private Sub Fetch_Pr_Details_()
    '    Dim c As New C
    '    Dim Cost As Double = 0
    '    Try
    '        Dim s As String
    '        s = "SELECT ISNULL(SUM(CREDIT),0) AS P FROM SB_V WHERE isVoid = 0 AND Pr_ID = '" & Pr_ID & "' AND BsType_ID = 3"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            MsgBox(" إجمالي المقبوض للورديــة : " + c.Dr("P").ToString, MsgBoxStyle.Information)
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub

    Private Sub ALL_QTY_txt_TextChanged(sender As Object, e As EventArgs) Handles ALL_QTY_txt.TextChanged
        If Not String.IsNullOrWhiteSpace(ALL_QTY_txt.Text) Then
            Dim N As Double = ALL_QTY_txt.Text
            ALL_QTY_txt.Text = N.ToString("N")
        End If
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub


    Private Sub إضافةكعميلجديدToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إضافةكعميلجديدToolStripMenuItem.Click
        ADD_Fast_AG()
    End Sub

    Private Sub كشفحسابالعميلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles كشفحسابالعميلToolStripMenuItem.Click
        Show_AG_Balance()
    End Sub

    Private Sub عرضرصيدالعميلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضرصيدالعميلToolStripMenuItem.Click
        MsgBox(AG_Balance.ToString("n"), MsgBoxStyle.Information, "رصيد العميل : " & AG_SH_txt.Text)
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
        'Save_Total(T_ID, TOTAL, Disc)
        'query("Update Agents_Balance_MV SET Discount = " & Discount_txt.Text & " WHERE T_ID = " & T_ID)

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
        ' If Project_cm.SelectedValue <= 0 Then Project_cm.SelectedValue = 1

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Update_AG_Project"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@Proj_ID", Project_cm.SelectedValue)
        SQL_SP_EXEC(sqlComm)
    End Sub


    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If IM_ID > 0 Then
            Show_IM_Details.IM_ID = IM_ID
            Show_IM_Details.ShowDialog()
        End If
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

    Private Sub Show_Bill_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Show_Bill_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Barcode_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles Barcode_SH_txt.TextChanged
        If Sh_ByNum_CB.Checked = True And Barcode_SH_txt.Text.Count > 0 Then
            Load_IMByNum()
        Else
            IMDataGridViewX.Visible = False
        End If
    End Sub

    Public Sub Load_IMByNum()
        Dim c As New C

        Try
            IM_Dt.Clear()
            Dim s As String
            s = "select IM_ID,item_name,isValid,IM_NUM from IM_All_V WHERE IM_NUM Like '%" & Barcode_SH_txt.Text & "%' Order by item_name ASC"
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

        If MessageBox.Show(" تحويل الفاتورة إلى فاتورة مبيعات نهائية ", "", MessageBoxButtons.OKCancel, _
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

    Private Sub Min_SP_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Min_SP_CB.CheckedChanged
        CB_CHecked(sender)
        If Min_SP_CB.Checked = True Then Min_SP_2_CB.Checked = False
        Price_Text_Check_Read()
        IM_Fetch_QTY()
    End Sub

    Private Sub Min_SP_2_CBCheckedChanged(sender As Object, e As EventArgs) Handles Min_SP_2_CB.CheckedChanged
        CB_CHecked(sender)
        If Min_SP_2_CB.Checked = True Then Min_SP_CB.Checked = False
        Price_Text_Check_Read()
        IM_Fetch_QTY()
    End Sub

    Private Sub Price_Text_Check_Read()
        If Min_SP_CB.Checked = True Or Min_SP_2_CB.Checked = True Then
            PriceTextBox.ReadOnly = True
        Else
            If U_SB_IM_Update = True Then
                PriceTextBox.ReadOnly = False
            Else
                PriceTextBox.ReadOnly = True
            End If
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
        If dialog.ShowDialog() = DialogResult.OK Then
            CashPrint(get_prt_path(dialog.ListBox1.SelectedValue))

            '    ' عرض الخيار الذي اختاره المستخدم
            '    MessageBox.Show("تم اختيار: " & dialog.SelectedItem)
            'Else
            '    MessageBox.Show("لم يتم اختيار أي خيار.")
        End If
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

End Class