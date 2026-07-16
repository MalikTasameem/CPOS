Imports System.Data.SqlClient
Imports System.Drawing.Printing

Public Class Format_Items_Manual : Inherits System.Windows.Forms.Form

    Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Public T_ID As Integer
    Public isDepended As Boolean
    Public isVoid As Boolean

    Dim Valid_Dt As New DataTable

    Public isCashReceipt_Success As Boolean = False
    Public isShowingDetails As Boolean = False

    '  Dim IM_ID As Integer = 0
    ' Dim IM_Dt As New DataTable
    ' Dim IM_QTY As Double = 0
    Public TOTAL As Double = 0

    Public AG_ID As Integer = 0
    Dim AG_Dt As New DataTable
    ' Dim U_Dt As New DataTable
    Dim Get_Unit As Boolean = False
    'Dim Cost As Double = 0

    Public Barcode As String
    Dim isPied As Integer = 0
    Dim BillUser_ID As Integer

    Dim Bill_DT As New DataTable
    Dim Bill_DT_2 As New DataTable
    Dim Bill_DT_3 As New DataTable

    'Dim U_Cargo As Double = 1
    'Dim ALL_QTY As Double = 0
    'Dim isStore As Integer
    'Public Barcode_IM As String = ""
    Public Bill_ID As Integer

    Public On_Update As Boolean

    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        FormType = 0
        Me.Dispose()
    End Sub

    Private Sub Expenses_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Keys.F1
                If New_butt.Enabled = True Then ResetNewBill()
            Case Keys.F12
                If Save_butt.Enabled = True Then Save_butt_Click(sender, e)

            Case Keys.F4
                If Delete_butt.Enabled = True Then Delete_butt_Click(sender, e)

                'Case Keys.F5
                '    IM_SH_txt.Select()
                'Case Keys.F8
                '    Barcode_SH_txt.Select()


        End Select
    End Sub

    Private Sub Change_IM_Qty(def As Integer)
        Dim SB_T_ID As Integer = BillMetroGrid.CurrentRow.Cells("T_ID_CL").Value
        Dim Row_Index As Integer = BillMetroGrid.CurrentCell.RowIndex
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "SB_Contents_Change_IM_Qty"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", SB_T_ID)
            .Parameters.AddWithValue("@Def", def)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            Pch_Contents_SELECT_Bill()
            BillMetroGrid.CurrentCell = BillMetroGrid.Rows(Row_Index).Cells(3)
        End If
    End Sub


    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        'If IM_ID > 0 Then
        '    Show_IM_Details.IM_ID = IM_ID
        '    Show_IM_Details.ShowDialog()
        'End If
    End Sub

    Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '   If St_Count() = 1 Then All_St_Panel.Visible = False
        ThemeManager.ApplyThemeToForm(Me)
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized

        FormType = 9
        ' Load_ST()
        Get_Last_T_ID()

        EditState = Edit_butt.Text

        If isShowing_Trans = True Then
            T_ID = T_ID_Trans
            Fill_Bill_Info()
            Pch_Contents_SELECT_Bill()
            SelectStateBt()
            New_butt.Enabled = False
        End If

        'If My_Settings.S_Default = 0 Then
        '    Barcode_SH_txt.Select()
        'Else
        '    IM_SH_txt.Select()
        'End If


    End Sub

    Public Sub Check_View_Control()
        AGMetroGrid.Columns("ST_NAME_CL").Visible = MY_Settings.S_ST_Name_CL
        AGMetroGrid.Columns("D_Valid_CL").Visible = MY_Settings.S_D_Valid_CL
        AGMetroGrid.Columns("U_Name_CL").Visible = MY_Settings.S_IMUnit_CL

        AGMetroGrid.Columns("Rtn_Price_CL").Visible = MY_Settings.S_Price_CL
        AGMetroGrid.Columns("T_Price_CL").Visible = MY_Settings.S_Total_CL

        'Min_SP_Panel.Visible = S_Allow_MinSP
        'Min_SP_Panel_2.Visible = S_Allow_MinSP
    End Sub

    Public Sub Get_Last_T_ID()
        Dim C As New C
        Dim S As String = ""

        S = "Select Top 1 T_ID From Agents_Balance_MV Where User_ID = '" & USER_ID & "' AND BsType_ID = 18 AND isDepended = 0 AND isVoid = 0 AND T_ID BETWEEN " & START_ID & " AND " & END_ID & " ORDER BY T_ID DESC "

        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                ClearFields()
                T_ID = C.Dr("T_ID")
                Fill_Bill_Info()
                Pch_Contents_SELECT_Bill()
                SelectStateBt()
            Else
                Call_New_Bill()
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
            s = "SELECT ISNULL(MAX(Frm_ID_M),0) + 1 AS N FROM Agents_Balance_MV "
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


    Public Sub Fill_Bill_Info()

        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "FRM_Info_V_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
        End With
        C.Con.Open()
        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            C.Dr.Read()

            AG_ID = C.Dr("AG_ID")

            DateTimeEx.Text = C.Dr("Date")
            Title_txt.Text = C.Dr("Receipt_Title")
            'Barcode = C.Dr("Barcode")

            If C.Dr("isPied") = 1 Then
                Save_butt.Enabled = False

            Else
                Save_butt.Enabled = True
            End If

            Bill_ID_Txt.Text = S_Sub_Code & (C.Dr("Frm_ID_M")) ' - START_ID).ToString
            Bill_ID = C.Dr("Frm_ID_M")

            isVoid = C.Dr("isVoid")
            isDepended = C.Dr("isDepended")

            isPied = C.Dr("isPied")

            BillUser_ID = C.Dr("User_ID")
            User_Name_lb.Text = C.Dr("U_Name") + " - " + C.Dr("Date").ToString
            Notes_txt.Text = C.Dr("About")

            EMP_FS.Set_IM_By_ID(C.Dr("Cr_ID"))
            SB_BILL_FS.Set_IM_By_ID(C.Dr("Travel_ID"))

            SELECT_Deliver_Date()
        Else
            AG_ID = Default_AG_ID
            VoidLb.Enabled = False
        End If
        C.Con.Close()
    End Sub


    Public Sub SELECT_Deliver_Date()
        Dim c As New C
        Try
            Dim s As String
            s = "SELECT  Deliver_date FROM Agents_Balance_MV  WHERE T_ID = " & T_ID
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()

                If IsDBNull(c.Dr("Deliver_date")) Then
                    Deliver_DateTimePicker1.Checked = False
                Else
                    Deliver_DateTimePicker1.Checked = True
                    Deliver_DateTimePicker1.Text = c.Dr("Deliver_date")
                End If


            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Enable_Fields()
        DateTimeEx.Enabled = True
        Notes_txt.Enabled = True
        Title_txt.Enabled = True
        SB_BILL_FS.Enabled = True
        EMP_FS.Enabled = True
        Deliver_DateTimePicker1.Enabled = True
        Ebable_CatFields()
    End Sub

    Private Sub Disable_Fields()
        DateTimeEx.Enabled = False
        Notes_txt.Enabled = False
        Title_txt.Enabled = False
        SB_BILL_FS.Enabled = False
        EMP_FS.Enabled = False
        Deliver_DateTimePicker1.Enabled = False
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
        RemoveCatButton_2.Enabled = False
    End Sub

    Private Sub Ebable_CatFields()
        'IM_SH_txt.Enabled = True
        'Show_IM_btn.Enabled = True
        'Barcode_SH_txt.Enabled = True
        'QtyTextBox.Enabled = True
        'PriceTextBox.Enabled = True
        ADDCatButton.Enabled = True
        RemoveCatButton.Enabled = True
        RemoveCatButton_2.Enabled = True
    End Sub


    Public Sub Switch_Dependcy(F As Boolean)

        If F = True Then
            isDepended = 1
            BillMetroGrid.BackgroundColor = Color.LightGreen
            BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
            Save_butt.Enabled = False
            Print_btn.Enabled = True
            Print_btn_2.Enabled = True
        Else
            isDepended = 0
            BillMetroGrid.BackgroundColor = Color.LightYellow
            BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
            Save_butt.Enabled = True
            Print_btn.Enabled = False
            Print_btn_2.Enabled = False
        End If

    End Sub

    Private Sub NewStateBt()
        Enable_Fields()
        Save_butt.Enabled = True
        Delete_butt.Enabled = False
        Me.Text = "فاتورة تصنيع جديدة"
        'If My_Settings.S_Default = 0 Then
        '    Barcode_SH_txt.Select()
        'Else
        '    IM_SH_txt.Select()
        'End If
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

        If isVoid = True Then
            VoidLb.Visible = True
            Disable_Fields()
            Save_butt.Enabled = False
            Delete_butt.Enabled = False
            BillMetroGrid.Enabled = True
            BillMetroGrid.BackgroundColor = Color.IndianRed
            BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.IndianRed


            AGMetroGrid.Enabled = True
            AGMetroGrid.BackgroundColor = Color.IndianRed
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.IndianRed

            ServicesGrid.Enabled = True
            ServicesGrid.BackgroundColor = Color.IndianRed
            ServicesGrid.RowsDefaultCellStyle.BackColor = Color.IndianRed


            Print_btn.Enabled = False
            Print_btn_2.Enabled = False
            Aggregate_Btn.Enabled = False
        Else

            If isDepended = False Then
                Save_butt.Enabled = True
                BillMetroGrid.BackgroundColor = Color.LightYellow
                BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow


                AGMetroGrid.BackgroundColor = Color.LightYellow
                AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow

                ServicesGrid.BackgroundColor = Color.LightYellow
                ServicesGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow

                Enable_Fields()
                Delete_butt.Enabled = False
                Print_btn.Enabled = False
                Print_btn_2.Enabled = False
                Aggregate_Btn.Enabled = True
            Else
                Save_butt.Enabled = False
                BillMetroGrid.BackgroundColor = Color.LightGreen
                BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen

                AGMetroGrid.BackgroundColor = Color.LightGreen
                AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen

                ServicesGrid.BackgroundColor = Color.LightGreen
                ServicesGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen

                Disable_Fields()
                Delete_butt.Enabled = True
                Print_btn.Enabled = True
                Print_btn_2.Enabled = True
                Aggregate_Btn.Enabled = False
            End If
            VoidLb.Visible = False
        End If

        Me.Text = "عرض بيانات فاتورة"

        'If My_Settings.S_Default = 0 Then
        '    Barcode_SH_txt.Select()
        'Else
        '    IM_SH_txt.Select()
        'End If

    End Sub

    Private Sub ClearFields()
        isCashReceipt_Success = False
        T_ID = 0
        Notes_txt.Clear()
        '  PriceTextBox.Clear()
        Bill_DT.Clear()
        Bill_DT_2.Clear()
        DateTimeEx.Text = Date.Now
        VoidLb.Visible = False
        isVoid = False
        isDepended = False
        '  ClearCatFields()
        Me.Text = FormState
        Title_txt.Clear()
        EMP_FS.Textt = ""
        SB_BILL_FS.Textt = ""
        SB_AG_NAME_TXT.Clear()
    End Sub

    Private Sub ResetNewBill()
        ClearFields()
        Insert_NewBill()
        NewStateBt()
    End Sub


    Private Sub Insert_NewBill()

        Dim sqlComm As New SqlClient.SqlCommand()

        sqlComm.CommandText = "Agents_BalanceMV_insert"
        sqlComm.CommandType = CommandType.StoredProcedure

        sqlComm.Parameters.AddWithValue("@T_ID", 0)
        If isPr_Open = True Then sqlComm.Parameters.AddWithValue("@Pr_ID", Pr_ID)
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

        sqlComm.Parameters.AddWithValue("@BsType_ID", 18)

        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters("@Frm_ID_M").Direction = ParameterDirection.Output
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then
            Me.Bill_ID_Txt.Text = sqlComm.Parameters("@Frm_ID_M").Value.ToString()
            T_ID = sqlComm.Parameters("@T_ID").Value.ToString()

            ' BillCaseLb.Text = "فاتورة جديدة"
            ' BillCaseLb.BackColor = Color.LightSeaGreen
            Pch_Contents_SELECT_Bill()
            Fill_Bill_Info()
            SelectStateBt()

        End If

    End Sub

    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        Beep()
        If MessageBox.Show("إعتماد أمر التصنيع بشكل نهائي ؟", "حفظ", MessageBoxButtons.OKCancel, _
              MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
            If AGMetroGrid.Rows.Count > 0 Then FRM_Manual_Set_Cost()
            Save_About(T_ID, Notes_txt.Text)
            Save_Date(T_ID, DateTimeEx)
            Save_Title_Name(T_ID, Title_txt.Text)
            Update_Total()
            AG_Balance_Update_Date_Deliver(T_ID, Deliver_DateTimePicker1)
            If DependingBill(T_ID) = True Then
                Switch_Dependcy(1)
                SelectStateBt()
            End If
            isCashReceipt_Success = False
        End If
    End Sub


    Private Sub Delete_butt_Click(sender As Object, e As EventArgs) Handles Delete_butt.Click

        If AGMetroGrid.Rows.Count > 0 Then
            If IM_min_QTY = False Then
                If IM_Check_Neg_QTY_For_Cancel_FRM_Details() = 1 Then
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

    Private Function IM_Check_Neg_QTY_For_Cancel_FRM_Details()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_For_Cancel_FRM_Details"
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
            isVoid = True
            SelectStateBt()
        End If

    End Sub

    Private Sub TreasuryCard_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub AGMetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles BillMetroGrid.RowsAdded
        Calc_Total()
    End Sub

    Private Sub AGMetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles BillMetroGrid.RowsRemoved
        Calc_Total()
    End Sub

    Private Sub Calc_Total()
        TOTAL = 0
        Dim QTY = 0

        For i = 0 To BillMetroGrid.Rows.Count - 1
            TOTAL = TOTAL + BillMetroGrid.Rows(i).Cells("الإجمالي").Value
            QTY += BillMetroGrid.Rows(i).Cells("الكمية").Value
        Next

        If BillMetroGrid.RowCount > 0 Then
            For i = 0 To BillMetroGrid.Rows.Count - 1
                BillMetroGrid.Rows(i).Cells("ت").Value = i + 1
            Next
        End If


        IM_Count_LB.Text = BillMetroGrid.Rows.Count.ToString + " : مواد "
        IM_Qty_LB.Text = QTY.ToString + " : كميات "
        Pure_txt.Text = TOTAL.ToString("N")
    End Sub

    Private Sub Calc_Total_2()
        Dim TOTAL_2 = 0
        For i = 0 To ServicesGrid.Rows.Count - 1
            TOTAL_2 = TOTAL_2 + ServicesGrid.Rows(i).Cells(5).Value
        Next

        T_Service_Txt.Text = TOTAL_2
    End Sub


    Private Sub Update_Total()
        If String.IsNullOrWhiteSpace(Pure_txt.Text) = False Then Save_Total(T_ID, TOTAL, 0)
    End Sub



    Public Sub Pch_Contents_SELECT_Bill()

        Bill_DT.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "FRM_Details_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Bill_T_ID", Me.T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(Bill_DT)
        AGMetroGrid.DataSource = Bill_DT



        Bill_DT_2.Clear()
        C = New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[FRM_Contents_Manuel_V_SELECT_Bill]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Bill_T_ID", Me.T_ID)
            .Parameters.AddWithValue("@is_Store", 1)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(Bill_DT_2)
        'BillMetroGrid.DataSource = Bill_DT_2
        DataB.DataSource = Bill_DT_2
        BillMetroGrid.DataSource = DataB


        '-----------------------------------------------------------------------
        Bill_DT_3.Clear()
        C = New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[FRM_Contents_Manuel_V_SELECT_Bill]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Bill_T_ID", Me.T_ID)
            .Parameters.AddWithValue("@is_Store", 0)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(Bill_DT_3)
        'BillMetroGrid.DataSource = Bill_DT_2
        ServicesGrid.DataSource = Bill_DT_3
        'BillMetroGrid.DataSource = DataB
        ServicesGrid.Columns("T_ID_2_CL").Visible = False
        '----------------------------------------------------------------------


        ServicesGrid_UcGridColumnsSelector.BindGrid(
ServicesGrid,
New List(Of String) From {""},
Me.Name.ToString
 )


        BillMetro_UcGridColumnsSelector1.BindGrid(
BillMetroGrid,
New List(Of String) From {""},
Me.Name.ToString
 )


        'CheckedListBox1.Items.Clear()
        'For i As Integer = 0 To BillMetroGrid.ColumnCount - 1
        '    Dim CL = BillMetroGrid.Columns(i).Name
        '    CheckedListBox1.Items.Add(CL)
        'Next

        'CheckedListBox2.Items.Clear()
        'For i As Integer = 0 To ServicesGrid.ColumnCount - 1
        '    Dim CL = ServicesGrid.Columns(i).HeaderText
        '    CheckedListBox2.Items.Add(CL)
        'Next

        BillMetroGrid.Columns(7).Tag = 1
        BillMetroGrid.Columns(8).Tag = 1
        BillMetroGrid.Columns(9).Tag = 1
        BillMetroGrid.Columns("T_ID_CL").Visible = False

    End Sub


    Private Sub SB_Contents_Delete_IM()
        Dim Row_Index As Integer = AGMetroGrid.CurrentCell.RowIndex
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "FRM_Details_DELETE"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", AGMetroGrid.CurrentRow.Cells("DETAILS_T_ID_CL").Value)
        End With
        If SQL_SP_EXEC(c.Com) = True Then

            Network_Edit_Tracker_insert(" الصنف:" + AGMetroGrid.CurrentRow.Cells("FRM_IM_NAME_CL").Value.ToString + " الوحدة:" +
            AGMetroGrid.CurrentRow.Cells("U_Name_CL").Value.ToString + " العدد:" + AGMetroGrid.CurrentRow.Cells("prc_QTY_CL").Value.ToString _
            + " التكلفة:" + AGMetroGrid.CurrentRow.Cells("Rtn_Price_CL").Value.ToString + " البيع:" + AGMetroGrid.CurrentRow.Cells("SALE_Price_CL").Value.ToString, Bill_ID_Txt.Text, 13, 2)

            'FRM_Manual_Set_Cost()
            Pch_Contents_SELECT_Bill()
            If Row_Index > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Row_Index - 1).Cells("FRM_IM_NAME_CL")
            'If MY_Settings.S_Default = 0 Then
            '    Barcode_SH_txt.Select()
            'Else
            '    IM_SH_txt.Select()
            'End If
        End If
    End Sub


    Private Sub RemoveCatButton_Click(sender As Object, e As EventArgs) Handles RemoveCatButton.Click

        If AGMetroGrid.Rows.Count > 0 Then

            If MessageBox.Show(" حذف الصنف " + AGMetroGrid.CurrentRow.Cells("FRM_IM_NAME_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                SB_Contents_Delete_IM()
            End If
        End If
    End Sub

    Private Function IM_Check_Neg_QTY_For_Update_FR_IM()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_For_Update_FR_IM"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@FRM_T_ID", AGMetroGrid.CurrentRow.Cells("DETAILS_T_ID_CL").Value)

            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                F = .Parameters("@F").Value
            End If
        End With

        Return F
    End Function


    Dim Tmp_Bill_ID As Integer
    Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
        Tmp_Bill_ID = Convert.ToInt64(Bill_ID_Txt.Text)
        Bill_ID_Txt.Text = Convert.ToInt64(Bill_ID_Txt.Text) - 1
        Get_T_ID()
    End Sub

    Public Sub Get_T_ID()
        Dim C As New C
        Dim S As String = ""

        S = "Select T_ID From Agents_Balance_MV Where FRM_ID_M = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "'"

        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                ClearFields()
                T_ID = C.Dr("T_ID")
                Fill_Bill_Info()
                Pch_Contents_SELECT_Bill()
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


    Private Sub Bill_ID_Txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Bill_ID_Txt.KeyDown
        If e.KeyCode = Keys.Return Then Get_T_ID()

        If e.KeyCode = Keys.Up Then Up_Bill_btn_Click(sender, e)
        If e.KeyCode = Keys.Down Then Down_Bill_btn_Click(sender, e)
    End Sub

    Private Sub Bill_ID_Txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Bill_ID_Txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub ADDCatButton_Click(sender As Object, e As EventArgs) Handles ADDCatButton.Click
        'ADD_IM()
        F_Frm_manuel_IM_card = New Frm_manuel_IM_card
        F_Frm_manuel_IM_card.T_ID = T_ID
        F_Frm_manuel_IM_card.ShowDialog()
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


    'Private Sub Sh_ByNum_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    '    IMDataGridViewX.Columns("IM_NUM_CL").Visible = Sh_ByNum_CB.Checked
    '    Barcode_SH_txt.Select()
    'End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    'Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs)
    '    If Get_Unit = True Then IM_Fetch_QTY()
    '    If Valid_Panel_2.Visible = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
    'End Sub

    'Private Sub IM_Fetch_QTY()
    '    Dim c As New C
    '    Try
    '        Dim s As String
    '        s = "select U_ID,U_Cargo,Price from IM_Menu_Units_V WHERE U_ID = '" & IM_Unit_cm.SelectedValue & "' AND IM_ID = '" & IM_ID & "'"
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
    '            If U_Cargo > 1 Then
    '                One_Panel.Visible = True
    '                Two_Panel.Visible = True
    '                NewSaleByOne.Visible = True
    '                Min_SP_By_One_Lb.Visible = True
    '                Min_SP_By_One_txt.Visible = True

    '            Else
    '                One_Panel.Visible = False
    '                Two_Panel.Visible = False
    '                NewSaleByOne.Visible = False

    '                Min_SP_By_One_Lb.Visible = False
    '                Min_SP_By_One_txt.Visible = False
    '            End If

    '            CalcAvgCost()
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    '    c = New C
    '    Try
    '        Dim s As String
    '        s = "select CONVERT(Numeric(18,3),Cost) AS Cost from IM_All_V WHERE IM_ID = '" & IM_ID & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            PriceTextBox.Text = c.Dr("Cost") * U_Cargo
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

    'Private Sub IM_CalcAvgCost_btn_Click(sender As Object, e As EventArgs)
    '    If IM_ID > 0 Then
    '        If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"
    '        IM_Calc_Avg(True)
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

    'Private Sub QtyTextBox_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub


    'Private Sub QtyTextBox_TextChanged(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
    'End Sub


    'Private Sub ALL_QTY_txt_TextChanged(sender As Object, e As EventArgs)
    '    If Not String.IsNullOrWhiteSpace(ALL_QTY_txt.Text) Then
    '        Dim N As Double = ALL_QTY_txt.Text
    '        ALL_QTY_txt.Text = N.ToString("N")
    '    End If
    'End Sub

    'Private Sub is_Prudoct_RD_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    '    is_Not_Qty_CB.Visible = is_Row_RD.Checked
    'End Sub

    Private Sub RemoveCatButton_2_Click(sender As Object, e As EventArgs) Handles RemoveCatButton_2.Click

        If TabControl1.SelectedTab Is TabPage2 Then
            If BillMetroGrid.Rows.Count > 0 Then
                If MessageBox.Show(" حذف المادة الخام " + BillMetroGrid.CurrentRow.Cells(5).Value, "تأكيد", MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                    Frm_Contetnts_Manual_Delete_IM()
                End If
            End If
        Else
            If ServicesGrid.Rows.Count > 0 Then
                If MessageBox.Show(" حذف الخدمــة " + ServicesGrid.CurrentRow.Cells("Serv_IM_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                    Frm_Contetnts_Manual_Delete_IM()
                End If
            End If
        End If

    End Sub


    Private Sub Frm_Contetnts_Manual_Delete_IM()
        ' Dim Row_Index As Integer = BillMetroGrid.CurrentCell.RowIndex
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "Frm_Contetnts_Manual_Delete_IM"
            .CommandType = CommandType.StoredProcedure

            If TabControl1.SelectedTab Is TabPage2 Then
                .Parameters.AddWithValue("@T_ID", BillMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
            ElseIf TabControl1.SelectedTab Is TabPage3 Then
                .Parameters.AddWithValue("@T_ID", ServicesGrid.CurrentRow.Cells("T_ID_2_CL").Value)
            Else
                Exit Sub
            End If

        End With
        If SQL_SP_EXEC(c.Com) = True Then


            If TabControl1.SelectedTab Is TabPage2 Then
                Network_Edit_Tracker_insert(" الصنف:" + BillMetroGrid.CurrentRow.Cells(5).Value.ToString + " الوحدة:" +
                BillMetroGrid.CurrentRow.Cells(6).Value.ToString + " العدد:" + BillMetroGrid.CurrentRow.Cells(7).Value.ToString _
                + " التكلفة:" + BillMetroGrid.CurrentRow.Cells(8).Value.ToString, Bill_ID_Txt.Text, 14, 2)

            Else
                Network_Edit_Tracker_insert(" الإسم:" + ServicesGrid.CurrentRow.Cells(2).Value.ToString + " العدد:" + ServicesGrid.CurrentRow.Cells(4).Value.ToString _
               + " التكلفة:" + ServicesGrid.CurrentRow.Cells(3).Value.ToString, Bill_ID_Txt.Text, 14, 2)
            End If

            'FRM_Manual_Set_Cost()
            Pch_Contents_SELECT_Bill()
            'If MY_Settings.S_Default = 0 Then
            '    Barcode_SH_txt.Select()
            'Else
            '    IM_SH_txt.Select()
            'End If
        End If


    End Sub


    Private Sub Aggregate_Btn_Click(sender As Object, e As EventArgs) Handles Aggregate_Btn.Click
        If AGMetroGrid.Rows.Count > 0 Then
            Beep()
            If MessageBox.Show("تحديد سعر تكلفة المنتجات ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then FRM_Manual_Set_Cost()
        End If
    End Sub

    Public Sub FRM_Manual_Set_Cost()
        Dim sqlComm As New SqlClient.SqlCommand()
        sqlComm.CommandText = "[FRM_Manual_Set_Cost]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        If SQL_SP_EXEC(sqlComm) = True Then Pch_Contents_SELECT_Bill()
    End Sub

    'Private Sub Valid_cm_SelectedValueChanged(sender As Object, e As EventArgs)
    '    If Get_Unit = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
    'End Sub

    'Private Sub ST_cm_SelectedValueChanged(sender As Object, e As EventArgs)
    '    If Get_Unit = True Then
    '        Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
    '        IM_Fetch_QTY()
    '        If Valid_Panel_2.Visible = True Then Fetch_IM_Valids(Valid_Dt, Valid_cm, IM_ID, ST_cm)
    '        If Get_Unit = True Then IM_Fetch_QTY_OfValid(IM_ID, ST_cm, Valid_cm, Valid_QTY_txt, U_Cargo)
    '    End If
    'End Sub

    Private Sub AGMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles AGMetroGrid.MouseDoubleClick
        FormType = 9
        If isDepended = False Then Change_IM_Details.ShowDialog()
    End Sub


    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        IMTranPrintData()
    End Sub

    Public Sub IMTranPrintData()

        Try
            ShowManualFormatPrintPreview(ManualFormatPrintMode.RawMaterials)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Print_btn_2_Click(sender As Object, e As EventArgs) Handles Print_btn_2.Click

        IMTranPrintData_2()
    End Sub


    Public Sub IMTranPrintData_2()

        Try
            ShowManualFormatPrintPreview(ManualFormatPrintMode.Products)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ShowManualFormatPrintPreview(mode As ManualFormatPrintMode)
        Dim data As ManualFormatPrintData = ManualFormatPrintData.FromManualForm(Me, mode)
        Dim renderer As New ManualFormatPrintDocumentRenderer(data)

        Using printDocument As PrintDocument = renderer.CreatePrintDocument()
            If String.IsNullOrWhiteSpace(Default_Printer_A4) = False Then printDocument.PrinterSettings.PrinterName = Default_Printer_A4

            Using previewDialog As New PrintPreviewDialog()
                previewDialog.Document = printDocument
                previewDialog.WindowState = FormWindowState.Maximized
                previewDialog.Text = data.ReportTitle
                previewDialog.ShowDialog(Me)
            End Using
        End Using
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

    'Private Sub NewSalePrice_txt_TextChanged_1(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
    '    If Not String.IsNullOrWhiteSpace(NewSalePrice_txt.Text) And U_Cargo > 1 Then
    '        NewSaleByOne.Text = (Convert.ToDouble(NewSalePrice_txt.Text) / U_Cargo).ToString("N")
    '    Else
    '        NewSaleByOne.Clear()
    '    End If
    'End Sub

    'Private Sub Min_SP_txt_TextChanged(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
    '    If Not String.IsNullOrWhiteSpace(Min_SP_txt.Text) And U_Cargo > 1 Then
    '        Min_SP_By_One_txt.Text = (Convert.ToDouble(Min_SP_txt.Text) / U_Cargo).ToString("N")
    '    Else
    '        Min_SP_By_One_txt.Clear()
    '    End If
    'End Sub

    ''Private Sub Min_SP_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
    ''    Check_Only_Float(sender, e)
    ''End Sub

    'Private Sub Min_SP_By_One_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Min_SP_By_One_txt.KeyPress
    '    Check_Point_in_FloatNum(sender, e)
    'End Sub


    'Private Sub Min_SP_txt_KeyDown(sender As Object, e As KeyEventArgs)

    '    Select Case e.KeyCode
    '        Case Keys.Up
    '            NewSalePrice_txt.Select()
    '        Case Keys.Return
    '            Min_SP_By_One_txt.Select()
    '    End Select

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

    Private Sub MakeBarcode_btn_Click(sender As Object, e As EventArgs) Handles MakeBarcode_btn.Click
        printbarcode.Auto_Print = True
        printbarcode.ShowDialog()
        printbarcode.Auto_Print = False
    End Sub

    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        If On_Update = False Then
            Beep()
            If MessageBox.Show(" سيتم تعديل الفاتورة بشكل مباشر مع كل تغير ... تأكيد التعديل ؟ ", "تعديل فاتورة", MessageBoxButtons.YesNo,
                         MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                If Open_Agents_Balance_MV_For_Edit(T_ID) = False Then Exit Sub

                Edit_butt.BackColor = Color.GreenYellow
                On_Update = True
                AGMetroGrid.Enabled = True
                AGMetroGrid.BackgroundColor = Color.LightYellow
                AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow

                BillMetroGrid.Enabled = True
                BillMetroGrid.BackgroundColor = Color.LightYellow
                BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow

                ServicesGrid.Enabled = True
                ServicesGrid.BackgroundColor = Color.LightYellow
                ServicesGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow

                Ebable_CatFields()
                Edit_butt.Text = "إيقاف التعديل"
                Notes_txt.Enabled = True
                DateTimeEx.Enabled = True
                Aggregate_Btn.Enabled = True
                Title_txt.Enabled = True
                Enable_Fields()
            End If
        Else
            Save_About(T_ID, Notes_txt.Text)
            Save_Date(T_ID, DateTimeEx)
            AG_Balance_Update_Date_Deliver(T_ID, Deliver_DateTimePicker1)
            On_Update = False
            Edit_butt.Text = EditState
            Edit_butt.BackColor = Color.WhiteSmoke
            SelectStateBt()
            Notes_txt.Enabled = False

            DateTimeEx.Enabled = False
            Aggregate_Btn.Enabled = False
            Title_txt.Enabled = False

            Disable_Fields()
        End If


    End Sub

    Private Sub FSearch_Filter2_ID_Changed(sender As Object, e As EventArgs) Handles SB_BILL_FS.ID_Changed
        query("UPDATE Agents_Balance_MV SET Travel_ID = " & SB_BILL_FS.TXT_ID.Text & " WHERE T_ID = " & T_ID)
        Load_SB_AG_NAME()
    End Sub


    Public Sub Load_SB_AG_NAME()
        Dim c As New C
        SB_AG_NAME_TXT.Clear()
        Try
            Dim s As String
            s = "select AG_NAME from [SB_Info_V] WHERE T_ID = " & SB_BILL_FS.TXT_ID.Text
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                SB_AG_NAME_TXT.Text = c.Dr("AG_NAME")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub EMP_FS_ID_Changed(sender As Object, e As EventArgs) Handles EMP_FS.ID_Changed
        query("UPDATE Agents_Balance_MV SET Cr_ID = " & EMP_FS.TXT_ID.Text & " WHERE T_ID = " & T_ID)
    End Sub

    Private Sub Open_SB_Bill_btn_Click(sender As Object, e As EventArgs) Handles Open_SB_Bill_btn.Click
        If SB_BILL_FS.TXT_ID.Text > 0 Then
            isShowing_Trans = True
            F_Sales = New Sales
            T_ID_Trans = SB_BILL_FS.TXT_ID.Text
            F_Sales.عرضفواتيرالزبونToolStripMenuItem.Visible = False
            F_Sales.BillNumPanel.Enabled = False
            F_Sales.ShowDialog()
            isShowing_Trans = False
        End If
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        ShowManualFormatPrintPreview(ManualFormatPrintMode.RawMaterials)
    End Sub

    Private Sub Deliver_DateTimePicker1_KeyDown(sender As Object, e As KeyEventArgs) Handles Deliver_DateTimePicker1.KeyDown
        If e.KeyCode = Keys.Return Then AG_Balance_Update_Date_Deliver(T_ID, Deliver_DateTimePicker1)
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

    Private Sub Format_Items_Manual_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Check_View_Control()
    End Sub

    'Private Sub is_Not_Qty_CB_CheckedChanged(sender As Object, e As EventArgs)
    ''    CB_CHecked(sender)
    ''End Sub

    'Private Sub BillMetroGrid_BackgroundColorChanged(sender As Object, e As EventArgs) Handles BillMetroGrid.BackgroundColorChanged

    '    'AGMetroGrid.BackgroundColor = BillMetroGrid.BackgroundColor
    '    'AGMetroGrid.RowsDefaultCellStyle.BackColor = BillMetroGrid.RowsDefaultCellStyle.BackColor

    '    'ServicesGrid.BackgroundColor = BillMetroGrid.BackgroundColor
    '    'ServicesGrid.RowsDefaultCellStyle.BackColor = BillMetroGrid.RowsDefaultCellStyle.BackColor
    'End Sub


    Private Sub ServicesGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles ServicesGrid.RowsRemoved
        Calc_Total_2()
    End Sub

    Private Sub ServicesGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles ServicesGrid.RowsAdded
        Calc_Total_2()
    End Sub

    Private Sub MOVE_From_pch_Click(sender As Object, e As EventArgs) Handles MOVE_From_pch.Click
        Dim inp = InputBox("ادخل رقم الفاتورة", "نقل أصناف من فاتورة مشتريات إلى فاتورة التصنيع")
        If inp <> "" Then check_pch_num(inp)
    End Sub

    Public Sub FRM_Manual_MOVE_FROM_PCH(PCH_T_ID)

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "[FRM_Manual_MOVE_FROM _PCH]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@PCH_T_ID", PCH_T_ID)
        sqlComm.Parameters.AddWithValue("@FRM_T_ID", T_ID)
        If SQL_SP_EXEC(sqlComm) = True Then Pch_Contents_SELECT_Bill()
    End Sub

    Public Sub check_pch_num(num)
        Dim C As New C
        Dim S As String = ""
        Dim PCH_T_ID As Integer
        S = "Select T_ID From Agents_Balance_MV Where pch_id = '" & num & "'"

        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                PCH_T_ID = C.Dr("T_ID")
                If MessageBox.Show(" سيتم نقل الفاتورة رقم " + num.ToString + " وكل الأصناف الخاصة بها ... متأكد ", "نقل فاتورة", MessageBoxButtons.OKCancel,
               MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                    FRM_Manual_MOVE_FROM_PCH(PCH_T_ID)
                End If

            Else
                MsgBox("لم يتم التعرف على الفاتورة", MsgBoxStyle.Exclamation)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        'If is_Not_Qty_CB.Checked = True Then
        '    query("UPDATE [FRM_Contents_Details] SET [is_Not_Qty] = 1 WHERE T_ID = " & BillMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
        '    Pch_Contents_SELECT_Bill()
        'Else
        '    query("UPDATE [FRM_Contents_Details] SET [is_Not_Qty] = 0 WHERE T_ID = " & BillMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
        '    Pch_Contents_SELECT_Bill()
        'End If
    End Sub




    Public Sub FRM_Manual_MOVE_FROM_SB(PCH_T_ID)
        Dim TYPE As String = ""
        'If is_Row_RD.Checked = True Then
        '    TYPE = "FRM_Contents_Details"
        'Else
        TYPE = "FRM_Details"
        'End If

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "[FRM_Manual_MOVE_FROM_SB]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@SB_T_ID", PCH_T_ID)
        sqlComm.Parameters.AddWithValue("@FRM_T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@is_Not_Qty", False) 'is_Not_Qty_CB.Checked
        sqlComm.Parameters.AddWithValue("@TYPE", TYPE)

        If SQL_SP_EXEC(sqlComm) = True Then Pch_Contents_SELECT_Bill()
    End Sub

    Private Sub MOVE_From_SB_Click(sender As Object, e As EventArgs) Handles MOVE_From_SB.Click
        Dim inp = InputBox("ادخل رقم الفاتورة", "نقل أصناف من فاتورة مبيعات إلى فاتورة التصنيع")
        If inp <> "" Then check_sb_num(inp)

    End Sub


    Public Sub check_sb_num(num)
        Dim C As New C
        Dim S As String = ""
        Dim PCH_T_ID As Integer
        S = "Select T_ID From Agents_Balance_MV Where SB_id = '" & num & "'"

        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                PCH_T_ID = C.Dr("T_ID")
                If MessageBox.Show(" سيتم نقل الفاتورة رقم " + num.ToString + " وكل الأصناف الخاصة بها ... متأكد ", "نقل فاتورة", MessageBoxButtons.OKCancel,
               MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                    FRM_Manual_MOVE_FROM_SB(PCH_T_ID)
                End If

            Else
                MsgBox("لم يتم التعرف على الفاتورة", MsgBoxStyle.Exclamation)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

    End Sub

    Private Sub طباعــةمفتوحــةموادالخـــامToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles طباعــةمفتوحــةموادالخـــامToolStripMenuItem.Click
        ShowManualFormatPrintPreview(ManualFormatPrintMode.Services)

    End Sub

    Private Sub FullPrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FullPrintToolStripMenuItem.Click
        ShowManualFormatPrintPreview(ManualFormatPrintMode.Full)
    End Sub


End Class
