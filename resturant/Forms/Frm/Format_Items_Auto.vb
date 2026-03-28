Imports System.Data.SqlClient

Public Class Format_Items_Auto : Inherits System.Windows.Forms.Form

    Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Public T_ID As Integer
    Public isDepended As Boolean
    Public isVoid As Boolean

    Public isCashReceipt_Success As Boolean = False
    Public isShowingDetails As Boolean = False

    Public TOTAL As Double = 0

    Public AG_ID As Integer = 0

    Public Barcode As String
    Dim BillUser_ID As Integer
    Dim Bill_DT As New DataTable
    Dim Bill_DT_2 As New DataTable

    Public On_Update As Boolean = False

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
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        FormType = 9
        EditState = Edit_butt.Text
        Get_Last_T_ID()

        If isShowing_Trans = True Then
            T_ID = T_ID_Trans
            Fill_Bill_Info()
            Pch_Contents_SELECT_Bill()
            SelectStateBt()
            New_butt.Enabled = False
        End If

        'If MY_Settings.S_Default = 0 Then
        '    Barcode_SH_txt.Select()
        'Else
        '    IM_SH_txt.Select()
        'End If

    End Sub

    Public Sub Get_Last_T_ID()

        Dim C As New C
        Dim S As String = ""

        S = "Select Top 1 T_ID From Agents_Balance_MV Where User_ID = '" & USER_ID & "' AND BsType_ID = 13 AND isDepended = 0 AND isVoid = 0 ORDER BY T_ID DESC"

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
            s = "SELECT ISNULL(MAX(Frm_ID),0) + 1 AS N FROM Agents_Balance_MV "
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

            If C.Dr("isPied") = 1 Then
                Save_butt.Enabled = False

            Else
                Save_butt.Enabled = True
            End If

            Bill_ID_Txt.Text = S_Sub_Code & (C.Dr("Frm_ID")) ' - START_ID).ToString
            '  Bill_ID = C.Dr("Frm_ID")
            isVoid = C.Dr("isVoid")
            isDepended = C.Dr("isDepended")
            'isPied = C.Dr("isPied")
            BillUser_ID = C.Dr("User_ID")
            User_Name_lb.Text = C.Dr("U_Name") + " - " + C.Dr("Date").ToString
            Notes_txt.Text = C.Dr("About")

        Else
            AG_ID = Default_AG_ID
            VoidLb.Enabled = False
        End If
        C.Con.Close()
    End Sub

    Private Sub Enable_Fields()
        DateTimeEx.Enabled = True
        Notes_txt.Enabled = True
        Title_txt.Enabled = True
        Ebable_CatFields()
    End Sub

    Private Sub Disable_Fields()
        DateTimeEx.Enabled = False
        Notes_txt.Enabled = False
        Title_txt.Enabled = False
        Disable_CatFields()
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
            BillMetroGrid.BackgroundColor = Color.LightGreen
            BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
            Save_butt.Enabled = False
        Else
            isDepended = 0
            BillMetroGrid.BackgroundColor = Color.LightYellow
            BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
            Save_butt.Enabled = True
        End If

    End Sub

    Private Sub NewStateBt()
        Enable_Fields()
        Save_butt.Enabled = True
        Delete_butt.Enabled = False
        Edit_butt.Enabled = False
        Me.Text = "فاتورة تصنيع جديدة"
        'If MY_Settings.S_Default = 0 Then
        '    Barcode_SH_txt.Select()
        'Else
        '    IM_SH_txt.Select()
        'End If
    End Sub
    Private Sub DeleteOrUpdateStateBt()
        Disable_Fields()
        Save_butt.Enabled = False
        Delete_butt.Enabled = False
        Edit_butt.Enabled = True
        Me.Text = DefaultFormState
    End Sub

    Private Sub SavedStateBt()
        Disable_Fields()
        Save_butt.Enabled = False
        Delete_butt.Enabled = False
        Edit_butt.Enabled = False
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
            Edit_butt.Enabled = False
            Edit_butt.Text = EditState
        Else

            If isDepended = False Then
                Save_butt.Enabled = True
                BillMetroGrid.BackgroundColor = Color.LightYellow
                BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
                Enable_Fields()
                Delete_butt.Enabled = False
                Edit_butt.Enabled = False
            Else
                Save_butt.Enabled = False
                BillMetroGrid.BackgroundColor = Color.LightGreen
                BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
                Disable_Fields()
                Delete_butt.Enabled = True
                Edit_butt.Enabled = True
            End If
            VoidLb.Visible = False

            Edit_butt.Text = EditState
        End If

        Me.Text = "عرض بيانات فاتورة"

        'If MY_Settings.S_Default = 0 Then
        '    Barcode_SH_txt.Select()
        'Else
        '    IM_SH_txt.Select()
        'End If

    End Sub

    Private Sub ClearFields()
        isCashReceipt_Success = False
        T_ID = 0
        Notes_txt.Clear()
        'PriceTextBox.Clear()
        Bill_DT.Clear()
        Bill_DT_2.Clear()
        DateTimeEx.Text = Date.Now
        VoidLb.Visible = False
        isVoid = False
        isDepended = False
        'ClearCatFields()
        Me.Text = FormState
        Title_txt.Clear()
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

        sqlComm.Parameters.AddWithValue("@BsType_ID", 13)

        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters("@Frm_ID").Direction = ParameterDirection.Output
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then
            Me.Bill_ID_Txt.Text = sqlComm.Parameters("@Frm_ID").Value.ToString()
            T_ID = sqlComm.Parameters("@T_ID").Value.ToString()

            Pch_Contents_SELECT_Bill()
            Fill_Bill_Info()
            SelectStateBt()

        End If

    End Sub

    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        Beep()
        If MessageBox.Show("إعتماد أمر التصنيع بشكل نهائي", "حفظ", MessageBoxButtons.OKCancel,
              MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
            Save_About(T_ID, Notes_txt.Text)
            Save_Date(T_ID, DateTimeEx)
            Save_Title_Name(T_ID, Title_txt.Text)
            Update_Total()
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
            TOTAL = TOTAL + AGMetroGrid.Rows(i).Cells("T_Price_CL").Value
            QTY += AGMetroGrid.Rows(i).Cells("Rtn_QTY_CL").Value
        Next

        IM_Count_LB.Text = AGMetroGrid.Rows.Count.ToString + " : مواد "
        IM_Qty_LB.Text = QTY.ToString + " : كميات "
        Pure_txt.Text = TOTAL.ToString("N")

    End Sub

    Private Sub Update_Total()
        If String.IsNullOrWhiteSpace(Pure_txt.Text) = False Then
            Save_Total(T_ID, TOTAL, 0)
        End If
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
            .CommandText = "FRM_Contents_V_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Bill_T_ID", Me.T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(Bill_DT_2)
        BillMetroGrid.DataSource = Bill_DT_2

        For i = 0 To BillMetroGrid.Rows.Count - 1
            BillMetroGrid.Rows(i).Cells("INDX_CL").Value = i + 1
        Next

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
             AGMetroGrid.CurrentRow.Cells("U_Name_CL").Value.ToString + " العدد:" + AGMetroGrid.CurrentRow.Cells("Rtn_QTY_CL").Value.ToString _
            + " التكلفة:" + AGMetroGrid.CurrentRow.Cells("Rtn_Price_CL").Value.ToString + " البيع:" + AGMetroGrid.CurrentRow.Cells("SALE_Price_CL").Value.ToString, Bill_ID_Txt.Text, 13, 2)

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


            If IM_min_QTY = False Then
                If IM_Check_Neg_QTY_For_Update_FR_IM() = 1 Then
                    MsgBox("في حالة خذف الصنف ستصبح كمية المخزون سالبة", MsgBoxStyle.Critical, "خطأ")
                    Exit Sub
                End If
            End If


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

        S = "Select T_ID From Agents_Balance_MV Where FRM_ID = '" & Convert.ToInt64(Bill_ID_Txt.Text) & "'"

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
        If e.KeyCode = Keys.Return Then
            Get_T_ID()
        End If

        If e.KeyCode = Keys.Up Then Up_Bill_btn_Click(sender, e)
        If e.KeyCode = Keys.Down Then Down_Bill_btn_Click(sender, e)
    End Sub

    Private Sub Bill_ID_Txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Bill_ID_Txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub ADDCatButton_Click(sender As Object, e As EventArgs) Handles ADDCatButton.Click
        'ADD_IM()
        F_Frm_auto_IM_card = New Frm_auto_IM_card
        F_Frm_auto_IM_card.T_ID = T_ID
        F_Frm_auto_IM_card.ShowDialog()
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

    Private Sub DGV_Control_btn_Click(sender As Object, e As EventArgs) Handles DGV_Control_btn.Click
        Switch_To_DV_Show()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Title_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Title_txt.KeyDown
        If e.KeyCode = Keys.Return Then Save_Title_Name(T_ID, Title_txt.Text)
    End Sub


    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        If On_Update = False Then
            Beep()
            If MessageBox.Show(" سيتم تعديل الفاتورة بشكل مباشر مع كل تغير ... تأكيد التعديل ؟ ", "تعديل فاتورة", MessageBoxButtons.YesNo, _
                         MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                Edit_butt.BackColor = Color.GreenYellow
                On_Update = True
                BillMetroGrid.Enabled = True
                BillMetroGrid.BackgroundColor = Color.LightYellow
                BillMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow

                ADDCatButton.Enabled = True
                RemoveCatButton.Enabled = True
                Ebable_CatFields()
                Edit_butt.Text = "إيقاف التعديل"
                Notes_txt.Enabled = True
                DateTimeEx.Enabled = True
            End If

        Else
            Save_About(T_ID, Notes_txt.Text)
            Save_Date(T_ID, DateTimeEx)
            On_Update = False
            Edit_butt.Text = EditState
            Edit_butt.BackColor = Color.WhiteSmoke
            SelectStateBt()
            Notes_txt.Enabled = False
            Select_Pch_Receipt(T_ID)

        End If
    End Sub
End Class