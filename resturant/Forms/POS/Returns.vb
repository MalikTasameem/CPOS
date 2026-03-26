Imports System.Data.SqlClient

Public Class Returns : Inherits System.Windows.Forms.Form

    Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Public T_ID As Integer
    Public isDepended As Boolean
    Public isVoid As Boolean
    'Public Receipts_DT As New DataTable

    Public isCashReceipt_Success As Boolean = False
    Public isShowingDetails As Boolean = False

    Dim IM_ID As Integer = 0
    'Dim IM_Dt As New DataTable

    Public TOTAL As Double = 0
    Public Disc As Double = 0
    Public Pure As Double = 0
    Public AG_ID As Integer = 0
    Dim AG_Dt As New DataTable
    Dim U_Dt As New DataTable
    Dim Get_Unit As Boolean = False
    Dim Cost As Double = 0


    Public Barcode As String
    Dim isPied As Integer = 0
    Dim BillUser_ID As Integer

    Dim Rtn_Dt As New DataTable
    Dim Saler_Percent As Double

    Dim U_Cargo As Double = 0
    Dim AG_Balance As Double = 0
    Public Barcode_IM As String = ""
    Public On_Update As Boolean

    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
   If On_Update = True Then Edit_butt_Click(sender, e)
        FormType = 0
        Save_Total(T_ID, TOTAL, Disc)

        Me.Dispose()
        'F_MainForm.Fill_ALL_IM()
    End Sub

    Private Sub Expenses_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Keys.Escape : Me.Close()
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

            Case 107 'Add
                If BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
                    If BillMetroGrid.Rows.Count > 0 Then
                        Dim Def As Integer = 1
                        Change_IM_Qty(Def)
                    End If
                End If

            Case 109 'Subtrac
                If BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
                    If BillMetroGrid.Rows.Count > 0 Then
                        If BillMetroGrid.CurrentRow.Cells("QTY_CL").Value > 1 Then
                            Dim Def As Integer = -1
                            Change_IM_Qty(Def)
                        End If
                    End If
                End If

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
            SB_Contents_SELECT_Bill()
            BillMetroGrid.CurrentCell = BillMetroGrid.Rows(Row_Index).Cells(3)
        End If
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
            ST_cm.SelectedIndex = 0
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized

        EditState = Edit_butt.Text
        If FormType = 5 Then
            Title_LB.Text = "فاتورة إرجاع مبيعــات"
            Search_By_Bar_CB.Visible = True
            Search_By_Bar_CB.Checked = My_Settings.Search_By_Bar_Rtn
            If Search_By_Bar_CB.Checked = True Then Bill_SH_TXT.Select()
        Else
            Title_LB.Text = "فاتورة إرجاع مشتريــات"
            Search_By_Bar_CB.Checked = False
            Serial_Code_Panel.Visible = False
        End If
        Check_View_Control()
        Load_ST()
        If U_SalesVoid = False Then
            Delete_butt.Visible = False
        Else
            Delete_butt.Visible = True
        End If
        Get_Last_T_ID()


        If isShowing_Trans = True Then
            T_ID = T_ID_Trans
            Fill_Bill_Info()
            SB_Contents_SELECT_Bill()
            SelectStateBt()
            New_butt.Enabled = False
        End If

        If Search_By_Bar_CB.Checked = False Then
            If My_Settings.S_Default = 0 Then
                'Barcode_SH_txt.Select()
            Else
                'IM_SH_txt.Select()
            End If
        Else
            Bill_SH_TXT.Select()
        End If

        ALLTime_CB.Checked = True


        If isDiscount = False Then
            MetroGrid1.Columns("Discount_CL").Visible = False
        Else
            If Discount_Distribute = True And isDiscount = True Then
                MetroGrid1.Columns("Discount_CL").Visible = True
            End If

        End If

        Min_SP_Panel.Visible = S_Allow_MinSP



        FunModule.Load_ALL_IM()
        ' تحميل البيانات
        mySearchControl.ItemsTable = IM_Dt
        mySearchControl.itemsTable_Barcode = IM_Dt_Barcodes
        mySearchControl.MaxGridHeight = 400

        'mySearchControl.DefaultSearchField = "اسم الصنف"
        ' إضافة الكنترول للفورم
        'Me.Controls.Add(mySearchControl)
        ' استقبال الاختيار
        AddHandler mySearchControl.ItemSelected, AddressOf HandleItemSelected

        mySearchControl.txtSearch.Select()

    End Sub

    Private Sub HandleItemSelected(itemId As Integer, isValid As String)
        PriceTextBox.Clear()
        IM_ID = itemId
        Get_Unit = False
        Fetch_IM_Units(IM_ID)
        'Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
        'Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
        'Fetch_IM_Units()
        'Load_IM_Change_Price()
        'QtyTextBox.Select()
        'Valid_Panel.Visible = False

    End Sub




    'Private Sub Fetch_ItemToList()
    '    If IMDataGridViewX.Rows.Count > 0 Then
    '        IM_ID = IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value
    '        IM_SH_txt.Text = IMDataGridViewX.CurrentRow.Cells("item_name_CL").Value
    '        '   Current_QTY.Text = IMDataGridViewX.CurrentRow.Cells("QtySH_CL").Value
    '        '  IM_QTY = IMDataGridViewX.CurrentRow.Cells("QtySH_CL").Value
    '        IM_SH_txt.BackColor = Color.LightGoldenrodYellow
    '        Fetch_IM_Units(IM_ID)
    '        IMDataGridViewX.Visible = False
    '        IM_SH_txt.Select()
    '        If IMDataGridViewX.CurrentRow.Cells("isValid_CL").Value = 1 Then
    '            Valid_Panel.Visible = True
    '        Else
    '            Valid_Panel.Visible = False
    '        End If
    '    End If
    'End Sub




    Public Sub Get_Last_T_ID()

        Dim C As New C
        Dim S As String = ""

        If FormType = 5 Then
            S = "Select Top 1 T_ID From Agents_Balance_MV Where User_ID = '" & USER_ID & "' AND BsType_ID = 10 AND isDepended = 0 AND isVoid = 0  AND T_ID BETWEEN " & START_ID & " AND " & END_ID & " ORDER BY T_ID DESC"
        ElseIf FormType = 6 Then
            S = "Select Top 1 T_ID From Agents_Balance_MV Where User_ID = '" & USER_ID & "' AND BsType_ID = 11 AND isDepended = 0 AND isVoid = 0  AND T_ID BETWEEN " & START_ID & " AND " & END_ID & " ORDER BY T_ID DESC"
        End If

        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                ClearFields()
                T_ID = C.Dr("T_ID")
                Fill_Bill_Info()
                SB_Contents_SELECT_Bill()
                SelectStateBt()
            Else
                Call_New_Bill()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

    End Sub

    Public Sub Check_View_Control()
        MetroGrid1.Columns("Date_CL").Visible = MY_Settings.S_Date_CL
        MetroGrid1.Columns("ST_NAME_CL").Visible = My_Settings.S_ST_Name_CL
        BillMetroGrid.Columns("ST_Name_CL_2").Visible = MY_Settings.S_ST_Name_CL
        MetroGrid1.Columns("D_Valid_CL").Visible = My_Settings.S_D_Valid_CL
        BillMetroGrid.Columns("D_Valid_CL_2").Visible = MY_Settings.S_D_Valid_CL
        MetroGrid1.Columns("U_Name_CL").Visible = My_Settings.S_IMUnit_CL
        BillMetroGrid.Columns("IMUnit_CL_2").Visible = MY_Settings.S_IMUnit_CL
        MetroGrid1.Columns("Rtn_Price_CL").Visible = My_Settings.S_Price_CL
        BillMetroGrid.Columns("Price_CL_2").Visible = MY_Settings.S_Price_CL
        MetroGrid1.Columns("T_Price_CL").Visible = My_Settings.S_Total_CL
        BillMetroGrid.Columns("Total_CL_2").Visible = MY_Settings.S_Total_CL
        MetroGrid1.Columns("IMNUM_CL").Visible = My_Settings.S_IMNUM_CL
        BillMetroGrid.Columns("IMNUM_CL_2").Visible = MY_Settings.S_IMNUM_CL
        MetroGrid1.Columns("Serial_Code_CL").Visible = MY_Settings.S_Serial_Code_CL

        Serial_Code_Panel.Visible = S_SerialCode

    End Sub

    Public Sub SELECT_MAX()
        Dim c As New C
        Try
            Dim s As String
            If FormType = 5 Then
                s = "SELECT ISNULL(MAX(SRtn_ID),0) + 1 AS N FROM Agents_Balance_MV "
            Else
                s = "SELECT ISNULL(MAX(PRtn_ID),0) + 1 AS N FROM Agents_Balance_MV "
            End If

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


    Public Sub SB_Contents_SELECT_Bill()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Rtn_Contents_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@SB_T_ID", Me.T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        BillMetroGrid.DataSource = C.Dt
    End Sub

    Public Sub Fill_Bill_Info()

        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "Rtn_Info_V_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
        End With
        C.Con.Open()
        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            C.Dr.Read()

            AG_ID = C.Dr("AG_ID")
            'GET_AG()
            AG_Cm.Set_IM_By_ID(AG_ID)

            DateTimeEx.Text = C.Dr("Date")

            If C.Dr("isPied") = 1 Then
                Save_butt.Enabled = False

            Else
                Save_butt.Enabled = True
            End If

            If FormType = 5 Then
                Bill_ID_Txt.Text = C.Dr("SRtn_ID")
            Else
                Bill_ID_Txt.Text = C.Dr("PRtn_ID")
            End If


            isVoid = C.Dr("isVoid")
            isDepended = C.Dr("isDepended")

            isPied = C.Dr("isPied")

            BillUser_ID = C.Dr("User_ID")
            User_Name_lb.Text = C.Dr("U_Name") + " - " + C.Dr("Date").ToString
            Notes_txt.Text = C.Dr("About")

        Else
            AG_ID = Default_AG_ID
            AG_Cm.Set_IM_By_ID(AG_ID)
            'Fetch_ItemToList2()
            VoidLb.Enabled = False
        End If
        C.Con.Close()
    End Sub

    Private Sub Enable_Fields()
        AG_Cm.Enabled = True
        'Show_IM_btn2.Enabled = True
        DateTimeEx.Enabled = True
        Notes_txt.Enabled = True
        Ebable_CatFields()
    End Sub

    Private Sub Disable_Fields()
        AG_Cm.Enabled = False
        'Show_IM_btn2.Enabled = False
        DateTimeEx.Enabled = False
        Notes_txt.Enabled = False
        Disable_CatFields()
    End Sub

    Private Sub Disable_CatFields()
        mySearchControl.Enabled = False
        'Show_IM_btn.Enabled = False
        'Barcode_SH_txt.Enabled = False
        QtyTextBox.Enabled = False
        PriceTextBox.Enabled = False
        ADDCatButton.Enabled = False
        RemoveCatButton.Enabled = False
        Bill_SH_TXT.Enabled = False
        Serial_Code_txt.Enabled = False
        Date_Search_Btn.Enabled = False
        Serial_Search_btn.Enabled = False
    End Sub

    Private Sub Ebable_CatFields()
        mySearchControl.Enabled = True
        'Show_IM_btn.Enabled = True
        'Barcode_SH_txt.Enabled = True
        QtyTextBox.Enabled = True
        PriceTextBox.Enabled = True
        ADDCatButton.Enabled = True
        RemoveCatButton.Enabled = True
        Bill_SH_TXT.Enabled = True
        Serial_Code_txt.Enabled = True
        Date_Search_Btn.Enabled = True
        Serial_Search_btn.Enabled = True
    End Sub


    Public Sub Switch_Dependcy(F As Boolean)

        If F = True Then
            isDepended = 1

            BillMetroGrid.BackgroundColor = Color.LightGreen
            BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
            ' AG_SH_txt.Enabled = False
            Save_butt.Enabled = False
        Else
            isDepended = 0
            BillMetroGrid.BackgroundColor = Color.LightYellow
            BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
            ' AG_SH_txt.Enabled = True
            Save_butt.Enabled = True
        End If

    End Sub

    Private Sub NewStateBt()
        Enable_Fields()
        Save_butt.Enabled = True
        Delete_butt.Enabled = False
        Me.Text = "فاتورة إرجاع جديدة"
        'AG_Grid.Visible = False
        'AG_SH_txt.Enabled = True
        If Search_By_Bar_CB.Checked = False Then
            'If MY_Settings.S_Default = 0 Then

            '    ' Barcode_SH_txt.Select()
            'Else
            '    '  IM_SH_txt.Select()
            'End If
        Else
            Bill_SH_TXT.Select()
        End If
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
            Print_btn.Enabled = False
            Save_butt.Enabled = False
            Delete_butt.Enabled = False
            BillMetroGrid.Enabled = True
            Edit_butt.Enabled = False
            BillMetroGrid.BackgroundColor = Color.IndianRed
            BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.IndianRed

        Else

            If isDepended = False Then
                Print_btn.Enabled = False
                Save_butt.Enabled = True
                Edit_butt.Enabled = False
                BillMetroGrid.BackgroundColor = Color.LightYellow
                BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
                Enable_Fields()
                Delete_butt.Enabled = False
            Else
                Print_btn.Enabled = True
                Save_butt.Enabled = False
                Edit_butt.Enabled = True
                BillMetroGrid.BackgroundColor = Color.LightGreen
                BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
                Disable_Fields()
                Delete_butt.Enabled = True
            End If
            VoidLb.Visible = False
        End If

        Me.Text = "فاتورة إرجاع"

        If Search_By_Bar_CB.Checked = False Then
            'If My_Settings.S_Default = 0 Then
            '    Barcode_SH_txt.Select()
            'Else
            '    IM_SH_txt.Select()
            'End If
        Else
            Bill_SH_TXT.Select()
        End If

    End Sub

    Private Sub ClearFields()
        isCashReceipt_Success = False
        T_ID = 0
        Notes_txt.Clear()
        PriceTextBox.Clear()
        'Receipts_DT.Clear()
        'Receipts_DT.Clear()
        DateTimeEx.Text = Date.Now
        VoidLb.Visible = False
        isVoid = False
        isDepended = False
        ClearCatFields()
        Me.Text = FormState
        Edit_butt.BackColor = Color.WhiteSmoke
        Edit_butt.Text = EditState
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
        sqlComm.Parameters.AddWithValue("@AG_ID", 1)
        sqlComm.Parameters.AddWithValue("@Date", Me.DateTimeEx.Value)
        If FormType = 5 Then
            sqlComm.Parameters.AddWithValue("@BsType_ID", 10)
        Else
            sqlComm.Parameters.AddWithValue("@BsType_ID", 11)
        End If

        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters("@SRtn_ID").Direction = ParameterDirection.Output
        sqlComm.Parameters("@PRtn_ID").Direction = ParameterDirection.Output
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then
            If FormType = 1 Then
                Me.Bill_ID_Txt.Text = sqlComm.Parameters("@SRtn_ID").Value.ToString()
            Else
                Me.Bill_ID_Txt.Text = sqlComm.Parameters("@PRtn_ID").Value.ToString()
            End If

            T_ID = sqlComm.Parameters("@T_ID").Value.ToString()

            ' BillCaseLb.Text = "فاتورة جديدة"
            ' BillCaseLb.BackColor = Color.LightSeaGreen
            SB_Contents_SELECT_Bill()
            Fill_Bill_Info()
            SelectStateBt()

        End If

    End Sub

    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        If BillMetroGrid.Rows.Count > 0 Then

            If AG_Cm.TXT_ID.Text = 0 Then
                MsgBox("حدد إسم العميل", MsgBoxStyle.Critical, "خطأ في الإعتماد")
                AG_Cm.Focus()
            Else
                'If String.IsNullOrWhiteSpace(AG_SH_txt.Text) Then
                '    '   AG_SH_txt.Text = "نقدي"
                '    Fetch_ItemToList2()
                'End If
                Beep()
                Save_AG_Name(T_ID, AG_ID, False)
                Save_About(T_ID, Notes_txt.Text)
                Save_Date(T_ID, DateTimeEx)
                Update_Total()



                Dim F As New Pay_Main_Form
                F.MONEY_VALUE = Pure_txt.Text
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

                ' ConfermBill()
            End If

            isCashReceipt_Success = False
        End If
    End Sub



    Private Sub Delete_butt_Click(sender As Object, e As EventArgs) Handles Delete_butt.Click

        If BillMetroGrid.Rows.Count > 0 Then
            If IM_min_QTY = False And FormType = 5 Then
                If IM_Check_Neg_QTY_For_Cancel_Rtn() = 1 Then
                    MsgBox(" لا يمكن سحب كمية بالسالب للصنف  " & Str_Name, MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            End If
        End If


        Beep()
        If MessageBox.Show(" سيتم إلغاء الفاتورة رقم " + Bill_ID_Txt.Text + " وكل المعاملات الخاصة بها ... متأكد ", "إلغــاء فاتورة", MessageBoxButtons.OKCancel, _
               MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
            Cancel_Bill()
        End If

    End Sub

    Private Function IM_Check_Neg_QTY_For_Cancel_Rtn()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_For_Cancel_Rtn"
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
            TOTAL = TOTAL + BillMetroGrid.Rows(i).Cells("Total_CL_2").Value
            QTY += BillMetroGrid.Rows(i).Cells("QTY_CL").Value
        Next

        IM_Count_LB.Text = BillMetroGrid.Rows.Count.ToString + " : مواد "
        IM_Qty_LB.Text = QTY.ToString + " : كميات "
        Pure = (TOTAL - Disc)
        Pure_txt.Text = Pure.ToString("N")

    End Sub

    Private Sub ADD_IM()
        If IM_ID = 0 Then
            MsgBox("حددالصنف", MsgBoxStyle.Exclamation)
            mySearchControl.txtSearch.Select()
        ElseIf String.IsNullOrWhiteSpace(PriceTextBox.Text) Then
            MsgBox("حدد سعر القطعة", MsgBoxStyle.Exclamation)
            PriceTextBox.Select()
        Else
            If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"

            If FormType = 6 Then
                If IM_min_QTY = False Then
                    If IM_Check_Neg_QTY_() = 1 Then
                        MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                End If
            End If

            'If Convert.ToDouble(QtyTextBox.Text) > MetroGrid1.CurrentRow.Cells("Rtn_QTY_CL").Value Then
            '    MsgBox("الكمية المرجعة أكبر من المحددة", MsgBoxStyle.Exclamation)
            '    Exit Sub
            'End If


            Add_ItemToBill(IM_ID)
        End If
    End Sub

    Private Function IM_Check_Neg_QTY_()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters.AddWithValue("@D_Vaild", MetroGrid1.CurrentRow.Cells("D_Valid_CL").Value)
            .Parameters.AddWithValue("@Enterd_Qty", QtyTextBox.Text)
            .Parameters.AddWithValue("@Cargo", U_Cargo)

            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                F = .Parameters("@F").Value
            End If
        End With

        Return F
    End Function

    Private Sub IM_Fetch_QTY()
        Dim c As New C
        Try
            Dim s As String
            s = "select U_Cargo,Price,Min_SP,Min_SP_2 from IM_Menu_Units_V WHERE U_ID = '" & IM_Unit_cm.SelectedValue & "' AND IM_ID = '" & IM_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                U_Cargo = c.Dr("U_Cargo")
                ' If String.IsNullOrWhiteSpace(PriceTextBox.Text) Then
                PriceTextBox.Text = Change_Price_Bercent(c.Dr("Price"))
                ' End If


                If Min_SP_CB.Checked = True Then
                    PriceTextBox.Text = c.Dr("Min_SP")
                    If c.Dr("Min_SP") = 0 Then PriceTextBox.Clear()
                ElseIf Min_SP_2_CB.Checked = True Then
                    PriceTextBox.Text = c.Dr("Min_SP_2")
                    If c.Dr("Min_SP_2") = 0 Then PriceTextBox.Clear()
                End If


            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function Change_Price_Bercent(def_price As Double)
        Dim c As New C
        Try
            Dim s As String
            s = "select  Percent_Price from Change_Price WHERE GM_ID = (SELECT GM_ID FROM IM_Menu WHERE IM_ID = '" & IM_ID & "' )"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                def_price += def_price * (c.Dr("Percent_Price") / 100)
            Else
                Return def_price
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return def_price
    End Function


    Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles IM_Unit_cm.SelectedValueChanged
        If Get_Unit = True Then IM_Fetch_QTY()
    End Sub


    Private Sub Update_Total()
        If String.IsNullOrWhiteSpace(Pure_txt.Text) = False Then
            Save_Total(T_ID, TOTAL, 0)
        End If
    End Sub

    Private Sub ClearCatFields()
        mySearchControl.btnClear.PerformClick()
        'Barcode_SH_txt.Clear()
        ' Current_QTY.Clear()
        PriceTextBox.Clear()
        QtyTextBox.Clear()
        U_Dt.Clear()
        Barcode_IM = ""
        IM_ID = 0
        DISC_Label.Text = "---"
    End Sub


    Public Sub Add_ItemToBill(IM_ID As String)
        Try

            Dim SB_ID_FROM As String = "(بدون)"
            If MetroGrid1.Rows.Count > 0 Then SB_ID_FROM = MetroGrid1.CurrentRow.Cells("Bill_ID_CL").Value.ToString

            If String.IsNullOrWhiteSpace(Barcode_IM) Then Barcode_IM = SELECT_BARCODE(IM_ID, IM_Unit_cm.SelectedValue)

            'Dim Row_Index As Integer = 0
            'If BillMetroGrid.Rows.Count > 0 Then Row_Index = BillMetroGrid.CurrentCell.RowIndex + 1
            'If String.IsNullOrWhiteSpace(Barcode_IM) Then Barcode_IM = SELECT_BARCODE(IM_ID, IM_Unit_cm.SelectedValue)
            Dim C As New C
            With C.Com
                .Connection = C.Con
                .CommandText = "Rtn_Contents_INSERT"
                .CommandType = CommandType.StoredProcedure

                If FormType = 5 Then
                    .Parameters.AddWithValue("@Rtn_Type_ID", 1)
                    .Parameters.AddWithValue("@Cost", Cost)
                    .Parameters.AddWithValue("@Saler_Percent", Saler_Percent)
                Else
                    .Parameters.AddWithValue("@Rtn_Type_ID", 7)
                End If
                .Parameters.AddWithValue("@SB_T_ID", Me.T_ID)
                .Parameters.AddWithValue("@IM_ID", IM_ID)
                .Parameters.AddWithValue("@Barcode", Barcode_IM)
                If String.IsNullOrWhiteSpace(QtyTextBox.Text) = False Then .Parameters.AddWithValue("@QYT", Convert.ToDouble(QtyTextBox.Text))
                .Parameters.AddWithValue("@U_ID", IM_Unit_cm.SelectedValue)
                .Parameters.AddWithValue("@Price", PriceTextBox.Text)
                .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
                If Valid_Panel.Visible = True Then .Parameters.AddWithValue("@D_Vaild", D_Valid.Value.Date)

                If MetroGrid1.Rows.Count > 0 Then
                    .Parameters.AddWithValue("@Orginal_Bill_T_ID", MetroGrid1.CurrentRow.Cells("B_T_ID_CL").Value)
                    .Parameters.AddWithValue("@Markter_Val", MetroGrid1.CurrentRow.Cells("Markter_Val_CL").Value)
                    .Parameters.AddWithValue("@Serial_Code", MetroGrid1.CurrentRow.Cells("Serial_Code_CL").Value)
                    .Parameters.AddWithValue("@is_By_Min_SP", MetroGrid1.CurrentRow.Cells("is_By_Min_SP_CL").Value)
                    .Parameters.AddWithValue("@is_By_Min_SP_2", MetroGrid1.CurrentRow.Cells("is_By_Min_SP_2_CL").Value)
                End If

            End With
            C.Da = New SqlClient.SqlDataAdapter(C.Com)
            C.Da.Fill(C.Dt)
            BillMetroGrid.DataSource = C.Dt

            If FormType = 5 Then
                Network_Edit_Tracker_insert(" الصنف:" & mySearchControl.txtSearch.Text & " العدد:" & QtyTextBox.Text & " السعر:" & PriceTextBox.Text & " من فاتورة:" & SB_ID_FROM, Bill_ID_Txt.Text, 10, 1)
            Else
                Network_Edit_Tracker_insert(" الصنف:" & mySearchControl.txtSearch.Text & " العدد:" & QtyTextBox.Text & " السعر:" & PriceTextBox.Text & " من فاتورة:" & SB_ID_FROM, Bill_ID_Txt.Text, 11, 1)
            End If


            ClearCatFields()
            If BillMetroGrid.Rows.Count > 0 Then BillMetroGrid.CurrentCell = BillMetroGrid.Rows(BillMetroGrid.Rows.Count - 1).Cells("EX_Name_CL")

            If Search_By_Bar_CB.Checked = False Then
                'If MY_Settings.S_Default = 0 Then
                '    Barcode_SH_txt.Select()
                'Else
                '    IM_SH_txt.Select()
                'End If
            Else
                Bill_SH_TXT.Select()
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub SB_Contents_Delete_IM(T_ID As Integer)

        Try

            Dim SB_ID_FROM As String = "(بدون)"
            If MetroGrid1.Rows.Count > 0 Then SB_ID_FROM = MetroGrid1.CurrentRow.Cells("Bill_ID_CL").Value.ToString

            Dim Row_Index As Integer = BillMetroGrid.CurrentCell.RowIndex
            Dim c As New C
            With c.Com
                .Connection = c.Con
                .CommandText = "Rtn_Contents_Delete_IM"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@T_ID", T_ID)
            End With
            If SQL_SP_EXEC(c.Com) = True Then

                If FormType = 5 Then
                    Network_Edit_Tracker_insert(" الصنف:" & BillMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString & _
            " العدد:" & BillMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString & " السعر:" & BillMetroGrid.CurrentRow.Cells("Price_CL_2").Value.ToString & " من فاتورة:" & SB_ID_FROM, Bill_ID_Txt.Text, 10, 2)
                Else
                    Network_Edit_Tracker_insert(" الصنف:" & BillMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString & _
            " العدد:" & BillMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString & " السعر:" & BillMetroGrid.CurrentRow.Cells("Price_CL_2").Value.ToString & " من فاتورة:" & SB_ID_FROM, Bill_ID_Txt.Text, 11, 2)
                End If

                SB_Contents_SELECT_Bill()
                If Row_Index > 0 Then BillMetroGrid.CurrentCell = BillMetroGrid.Rows(Row_Index - 1).Cells(3)
                If Search_By_Bar_CB.Checked = False Then
                    'If MY_Settings.S_Default = 0 Then
                    '    Barcode_SH_txt.Select()
                    'Else
                    '    IM_SH_txt.Select()
                    'End If
                Else
                    Bill_SH_TXT.Select()
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub PriceTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles PriceTextBox.KeyDown
        Select Case e.KeyCode
            Case Keys.Up : mySearchControl.txtSearch.Select()
            Case Keys.Right
                IM_Unit_cm.DroppedDown = True
                IM_Unit_cm.Select()
            Case Keys.Down : If BillMetroGrid.Rows.Count > 0 = True Then BillMetroGrid.Select()
            Case Keys.Left : QtyTextBox.Select()
            Case Keys.Return : ADDCatButton_Click(sender, e)
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
            Case Keys.Up : mySearchControl.txtSearch.Select()
            Case Keys.Down : If BillMetroGrid.Rows.Count > 0 = True Then BillMetroGrid.Select()
            Case Keys.Right : PriceTextBox.Select()
        End Select

    End Sub

    Private Sub QtyTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QtyTextBox.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub QtyTextBox_TextChanged(sender As Object, e As EventArgs) Handles QtyTextBox.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub RemoveCatButton_Click(sender As Object, e As EventArgs) Handles RemoveCatButton.Click

        If BillMetroGrid.Rows.Count > 0 Then
            If MessageBox.Show(" حذف الصنف " + BillMetroGrid.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel, _
                               MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                SB_Contents_Delete_IM(BillMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
            End If
        End If
    End Sub

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

    '        Case Keys.Left : mySearchControl.txtSearch.Select()
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
    '        ' Current_QTY.Clear()
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

    'Private Sub IMDataGridViewX_CellClick(sender As Object, e As DataGridViewCellEventArgs)
    '    Fetch_ItemToList()
    'End Sub

    'Private Sub IMDataGridViewX_KeyDown(sender As Object, e As KeyEventArgs)
    '    If e.KeyCode = Keys.Return Then Fetch_ItemToList()
    '    If e.KeyCode = Keys.Up Then If IMDataGridViewX.CurrentRow.Index = 0 Then IM_SH_txt.Select()
    'End Sub

    'Private Sub Fetch_ItemToList()
    '    If IMDataGridViewX.Rows.Count > 0 Then
    '        IM_ID = IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value
    '        IM_SH_txt.Text = IMDataGridViewX.CurrentRow.Cells("item_name_CL").Value
    '        '   Current_QTY.Text = IMDataGridViewX.CurrentRow.Cells("QtySH_CL").Value
    '        '  IM_QTY = IMDataGridViewX.CurrentRow.Cells("QtySH_CL").Value
    '        IM_SH_txt.BackColor = Color.LightGoldenrodYellow
    '        Fetch_IM_Units(IM_ID)
    '        IMDataGridViewX.Visible = False
    '        IM_SH_txt.Select()
    '        If IMDataGridViewX.CurrentRow.Cells("isValid_CL").Value = 1 Then
    '            Valid_Panel.Visible = True
    '        Else
    '            Valid_Panel.Visible = False
    '        End If
    '    End If
    'End Sub

    Private Sub Fetch_IM_Units(IM_ID As Integer)
        Get_Unit = False
        Dim c As New C
        U_Dt.Clear()
        Try
            Dim s As String
            s = "select U_ID,U_Name from IM_Menu_Units_V  WHERE IM_ID = '" & IM_ID & "' Order By U_Cargo Asc"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(U_Dt)
            IM_Unit_cm.DataSource = U_Dt
            IM_Unit_cm.DisplayMember = "U_Name"
            IM_Unit_cm.ValueMember = "U_ID"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Get_Unit = True
        IM_Fetch_QTY()
    End Sub

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
    '        Case Keys.Down : Date_Search_Btn.Select()
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
    '            s = "select U_IM_ID,IM_ID,U_ID,item_name,isValid from IM_units_Search_V WHERE Barcode = '" & Barcode_SH_txt.Text & "'"
    '        End If
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()

    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            IM_ID = c.Dr("IM_ID")
    '            IM_SH_txt.Text = c.Dr("item_name")
    '            If Sh_ByNum_CB.Checked = False Then Barcode_IM = Barcode_SH_txt.Text
    '            If c.Dr("isValid") = 1 Then
    '                Valid_Panel.Visible = True
    '            Else
    '                Valid_Panel.Visible = False
    '            End If

    '            IMDataGridViewX.Visible = False
    '            QtyTextBox.Select()
    '            Fetch_IM_Units(IM_ID)
    '            Barcode_SH_txt.Clear()
    '            If Sh_ByNum_CB.Checked = False Then IM_Unit_cm.SelectedValue = c.Dr("U_ID")
    '            IM_Fetch_QTY()
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


    '-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------2

    'Public Sub Load_AG()
    '    Dim c As New C

    '    Try
    '        AG_Dt.Clear()
    '        Dim s As String
    '        s = "select AG_ID,Ag_name,ISNULL(T_Balance,0) AS T_Balance from AGENTS_MENU_V WHERE Ag_name Like '%" & AG_SH_txt.Text & "%'"
    '        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
    '        c.Da.Fill(AG_Dt)
    '        AG_Grid.DataSource = AG_Dt
    '        If AG_Dt.Rows.Count > 0 Then
    '            AG_Grid.Visible = True
    '            AG_Grid.Size = New Point(AG_Grid.Size.Width, 530)
    '        Else
    '            AG_Grid.Visible = False
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Public Sub GET_AG()
    '    Dim c As New C

    '    Try
    '        AG_Dt.Clear()
    '        Dim s As String
    '        s = "select Ag_name,ISNULL(T_Balance,0) AS T_Balance from Agents WHERE Ag_ID = '" & AG_ID & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            AG_SH_txt.Text = c.Dr("Ag_name")
    '            AG_Balance = c.Dr("T_Balance")
    '            AG_SH_txt.BackColor = Color.LightGoldenrodYellow
    '            AG_Grid.Visible = False
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Private Sub AG_SH_txt_Enter(sender As Object, e As EventArgs)
    '    Set_Ar_Language()
    'End Sub

    'Private Sub AG_SH_txt_KeyDown(sender As Object, e As KeyEventArgs)
    '    If e.KeyCode = Keys.Down Then AG_Grid.Select()
    '    If e.KeyCode = Keys.Return Then If AG_Grid.Visible = True Then Fetch_ItemToList2()
    'End Sub

    'Private Sub AG_SH_txt_TextChanged(sender As Object, e As EventArgs)
    '    If AG_SH_txt.Text.Count > 0 Then
    '        Load_AG()
    '    Else
    '        AG_Grid.Visible = False
    '        If AG_SH_txt.Text.Count = 0 Then
    '            AG_ID = Default_AG_ID
    '            Save_AG_Name(T_ID, AG_ID, False)
    '        Else
    '            AG_ID = 0
    '        End If
    '    End If
    '    Check_AG_Pied()
    'End Sub

    'Private Sub Check_AG_Pied()
    '    If S_Stores = False Then
    '        If AG_ID = Default_AG_ID Then
    '            AG_SH_txt.BackColor = Color.LightGray
    '        Else
    '            AG_SH_txt.BackColor = Color.LightGoldenrodYellow
    '        End If
    '    End If
    'End Sub

    'Private Sub AG_Grid_CellClick(sender As Object, e As DataGridViewCellEventArgs)
    '    Fetch_ItemToList2()
    'End Sub

    'Private Sub AG_Grid_KeyDown(sender As Object, e As KeyEventArgs)
    '    If e.KeyCode = Keys.Return Then Fetch_ItemToList2()
    '    If e.KeyCode = Keys.Up Then If AG_Grid.CurrentRow.Index = 0 Then AG_SH_txt.Select()
    'End Sub

    'Public Sub Fetch_ItemToList2()
    '    If AG_Grid.Rows.Count > 0 Then
    '        AG_ID = AG_Grid.CurrentRow.Cells(0).Value
    '        AG_SH_txt.Text = AG_Grid.CurrentRow.Cells(1).Value
    '        AG_SH_txt.BackColor = Color.LightGoldenrodYellow
    '        AG_Grid.Visible = False
    '        Save_AG_Name(T_ID, AG_ID, False)
    '    End If
    'End Sub


    ' Private Sub Show_IM_btn2_Click(sender As Object, e As EventArgs)
    '    If AG_Grid.Visible = True Then
    '        AG_Grid.Visible = False
    '    Else
    '        Fill_All_AG()
    '        AG_Grid.Visible = True
    '        AG_Grid.Size = New Point(AG_Grid.Size.Width, 530)
    '    End If
    'End Sub

    'Private Sub Fill_All_AG()
    '    Try
    '        Dim C As New C
    '        AG_Dt.Clear()
    '        Dim s As String = "SELECT AG_ID,Ag_name,ISNULL(T_Balance,0) AS T_Balance from AGENTS_MENU_V Order by Ag_name ASC"
    '        C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
    '        C.Da.Fill(AG_Dt)
    '        AG_Grid.DataSource = AG_Dt
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

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
        Save_Total(T_ID, TOTAL, Disc)

        Dim C As New C
        Dim S As String = ""
        If FormType = 5 Then
            S = "Select T_ID From Agents_Balance_MV Where SRtn_ID = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "'"
        Else
            S = "Select T_ID From Agents_Balance_MV Where PRtn_ID = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "'"
        End If

        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                ClearFields()
                T_ID = C.Dr("T_ID")
                Fill_Bill_Info()
                SB_Contents_SELECT_Bill()
                SelectStateBt()
            Else
                MsgBox("لم يتم التعرف على الفاتورة", MsgBoxStyle.Exclamation)
                Bill_ID_Txt.Text = Tmp_Bill_ID
            End If

            '   BillCaseLb.Text = "فاتورة سابقة"
            '   BillCaseLb.BackColor = Color.RosyBrown

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

    Private Sub AGMetroGrid_KeyDown(sender As Object, e As KeyEventArgs) Handles BillMetroGrid.KeyDown
        If e.KeyCode = Keys.Delete Then If BillMetroGrid.Rows.Count > 0 And BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow Then SB_Contents_Delete_IM(BillMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
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
        '  If MetroGrid1.Rows.Count > 0 Then
        ADD_IM()
        'Else
        '    MsgBox(" حدد الصنف المراد ترجيعه ", MsgBoxStyle.Exclamation, "إرجاع بضاعة")
        'End If

    End Sub

    Private Sub New_butt_Click(sender As Object, e As EventArgs) Handles New_butt.Click
        If On_Update = True Then Edit_butt_Click(sender, e)
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


    Private Sub ALLTime_CB_CheckedChanged(sender As Object, e As EventArgs) Handles ALLTime_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Date_Search_Btn_Click(sender As Object, e As EventArgs) Handles Date_Search_Btn.Click
        IM_MV_V_Select_ToRtn()
    End Sub

    Private Sub IM_MV_V_Select_ToRtn()
        Rtn_Dt.Clear()
        Dim C As New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "IM_MV_V_Select_ToRtn"
            .Parameters.AddWithValue("@D_F", DateRange_Flate1.D_F.Value)
            .Parameters.AddWithValue("@D_T", DateRange_Flate1.D_T.Value)
            If FormType = 5 Then
                .Parameters.AddWithValue("@BsType_ID", 1)
            Else
                .Parameters.AddWithValue("@BsType_ID", 7)
            End If
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters.AddWithValue("@AG_ID", AG_ID)
            .Parameters.AddWithValue("@AllTime", ALLTime_CB.Checked)
            If Not String.IsNullOrWhiteSpace(Bill_SH_TXT.Text) Then .Parameters.AddWithValue("@Bill_ID", Bill_SH_TXT.Text)
            .Parameters.AddWithValue("@By_Bar", Search_By_Bar_CB.Checked)

            .CommandType = CommandType.StoredProcedure
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(Rtn_Dt)
        MetroGrid1.DataSource = Rtn_Dt
        switch_AG()
        If Rtn_Dt.Rows.Count > 0 And Search_By_Bar_CB.Checked = True Then Bill_SH_TXT.Clear()
    End Sub

    Private Sub switch_AG()
        If MetroGrid1.Rows.Count > 0 Then
            IM_ID = MetroGrid1.CurrentRow.Cells("IM_ID_SH_CL").Value
            Fetch_IM_Units(MetroGrid1.CurrentRow.Cells("IM_ID_SH_CL").Value)
            IM_Unit_cm.SelectedValue = MetroGrid1.CurrentRow.Cells("RtnU_ID_CL").Value
            If IM_Unit_cm.SelectedValue = 0 Then
                MsgBox("الوحدة التي تم البيع بها تمت إزالتها من الصنف .. قم بإرجاعها لكي تتمكن من ترجيع العنصر المحدد", MsgBoxStyle.Critical, "خطأ في الترجيع")
                ADDCatButton.Enabled = False
            Else
                AG_ID = MetroGrid1.CurrentRow.Cells("RtnAG_ID_CL").Value
                'GET_AG()
                AG_Cm.Set_IM_By_ID(AG_ID)
                QtyTextBox.Text = MetroGrid1.CurrentRow.Cells("Rtn_QTY_CL").Value
                PriceTextBox.Text = MetroGrid1.CurrentRow.Cells("Rtn_Price_CL").Value

                ST_cm.SelectedValue = MetroGrid1.CurrentRow.Cells("ST_ID_CL").Value
                Cost = MetroGrid1.CurrentRow.Cells("IMCost_CL").Value
                Saler_Percent = MetroGrid1.CurrentRow.Cells("Saler_Percent_CL").Value

                If Not String.IsNullOrWhiteSpace(MetroGrid1.CurrentRow.Cells("D_Valid_CL").Value) Then
                    Valid_Panel.Visible = True
                    D_Valid.Text = MetroGrid1.CurrentRow.Cells("D_Valid_CL").Value
                Else
                    Valid_Panel.Visible = False
                End If

                If Discount_Distribute = False And FormType = 5 Then CHECK_DISCOUNT()
                ADDCatButton.Enabled = True
            End If
        End If
    End Sub

    Public Sub CHECK_DISCOUNT()

        If MetroGrid1.CurrentRow.Cells("B_T_ID_CL").Value > 0 Then
            Dim C2 As New C
            Dim S2 As String = ""
            Dim n As Double = 0
            S2 = "Select ISNULL(SUM(QTY),0) AS SQ FROM SB_Contents Where Bill_T_ID = '" & MetroGrid1.CurrentRow.Cells("B_T_ID_CL").Value & "'"
            C2.Com = New SqlClient.SqlCommand(S2, C2.Con)
            C2.Con.Open()
            Try
                C2.Dr = C2.Com.ExecuteReader
                If C2.Dr.HasRows Then
                    C2.Dr.Read()

                    n = (Convert.ToDouble(PriceTextBox.Text) - (MetroGrid1.CurrentRow.Cells("Discount_CL").Value / C2.Dr("SQ")))
                    DISC_Label.Text = "بالتخفيض : " & n.ToString("00.00")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            C2.Con.Close()
        End If
    End Sub

    Private Sub MetroGrid1_MouseClick(sender As Object, e As MouseEventArgs) Handles MetroGrid1.MouseClick
        switch_AG()
    End Sub


    'Private Sub Sh_ByNum_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    '    IMDataGridViewX.Columns("IM_NUM_CL").Visible = Sh_ByNum_CB.Checked
    '    Barcode_SH_txt.Select()

    'End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub عرضرصيدالعميلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضرصيدالعميلToolStripMenuItem.Click
        MsgBox(AG_Balance.ToString("n"), MsgBoxStyle.Information, "رصيد العميل : " & AG_Cm.Textt)
    End Sub

    Private Sub DGV_Control_btn_Click(sender As Object, e As EventArgs) Handles DGV_Control_btn.Click
        FormType = 5
        Switch_To_DV_Show()
    End Sub

    Private Sub Bill_SH_TXT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Bill_SH_TXT.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub Bill_SH_TXT_KeyDown(sender As Object, e As KeyEventArgs) Handles Bill_SH_TXT.KeyDown
        If e.KeyCode = Keys.Return Then IM_MV_V_Select_ToRtn()
        If e.KeyCode = Keys.Delete Then Bill_SH_TXT.Clear()
    End Sub

    Private Sub Search_By_Bar_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Search_By_Bar_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.Search_By_Bar_Rtn = Search_By_Bar_CB.Checked
        Bill_SH_TXT.Select()
    End Sub


    Private Sub Serial_Search_btn_Click(sender As Object, e As EventArgs) Handles Serial_Search_btn.Click
        If Not String.IsNullOrWhiteSpace(Serial_Code_txt.Text) Then Serial_Search()
    End Sub
    Private Sub Serial_Search()
        Rtn_Dt.Clear()
        Dim C As New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "IM_MV_V_Select_By_Serial_ToRtn"
            .Parameters.AddWithValue("@Serial_Code", Serial_Code_txt.Text)
            .CommandType = CommandType.StoredProcedure
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(Rtn_Dt)
        MetroGrid1.DataSource = Rtn_Dt
        switch_AG()
        If Rtn_Dt.Rows.Count > 0 And Search_By_Bar_CB.Checked = True Then Bill_SH_TXT.Clear()
    End Sub

    Private Sub Serial_Code_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Serial_Code_txt.KeyDown
        If e.KeyCode = Keys.Return And Serial_Search_btn.Enabled = True Then Serial_Search_btn_Click(sender, e)
    End Sub

    'Private Sub AG_Grid_VisibleChanged(sender As Object, e As EventArgs)
    '    If AG_Grid.Visible = True Then

    '        Me.Controls.Add(AG_Grid)
    '        AG_Grid.BringToFront()
    '        AG_Grid.Location = New Point(AG_Panel.Location.X, AG_Panel.Location.Y + AG_Panel.Size.Height + 1)
    '    Else
    '        AG_Panel.Controls.Add(AG_Grid)
    '        AG_Grid.Location = New Point(AG_SH_txt.Location.X, AG_SH_txt.Location.Y + AG_SH_txt.Size.Height + 1)
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

    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        IMTranPrintData()
    End Sub
    Public Sub IMTranPrintData()
        Try
            Dim pp As New ReportConnection

            pp.rp.Load(Application.StartupPath & "\reports\Rtn_Bill.rpt")

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
                .rp.SetParameterValue(5, Pure_txt.Text)
                .rp.SetParameterValue(6, Title_LB.Text)

                .rp.SetParameterValue(7, "")
                .rp.SetParameterValue(8, " الرقم الألي : " + Bill_ID_Txt.Text)
                .rp.SetParameterValue(9, " الحساب : " + mySearchControl.txtSearch.Text + vbNewLine)

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

    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        If T_ID > 0 Then
            If On_Update = False Then

                Beep()
                If MessageBox.Show(" سيتم تعديل الفاتورة بشكل مباشر مع كل تغير ... تأكيد التعديل ؟ ", "تعديل فاتورة", MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    Edit_butt.BackColor = Color.GreenYellow
                    On_Update = True
                    BillMetroGrid.BackgroundColor = Color.LightYellow
                    BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
                    ADDCatButton.Enabled = True
                    RemoveCatButton.Enabled = True
                    Notes_txt.Enabled = True
                    DateTimeEx.Enabled = True
                    AG_Cm.Enabled = True

                    Ebable_CatFields()
                    Edit_butt.Text = "إيقاف التعديل"
                End If

            Else
                Save_About(T_ID, Notes_txt.Text)
                Save_Date(T_ID, DateTimeEx)
                Save_Total(T_ID, TOTAL, Disc)
                On_Update = False
                Edit_butt.Text = EditState
                Edit_butt.BackColor = Color.White
                Notes_txt.Enabled = False
                DateTimeEx.Enabled = False
                AG_Cm.Enabled = True

                SelectStateBt()

            End If
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

    Private Sub AG_Cm_ID_Changed(sender As Object, e As EventArgs) Handles AG_Cm.ID_Changed
        AG_ID = AG_Cm.TXT_ID.Text
    End Sub
End Class