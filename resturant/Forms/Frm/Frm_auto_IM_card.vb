Public Class Frm_auto_IM_card : Inherits System.Windows.Forms.Form

    ' Dim DefaultFormState As String = ""
    Public T_ID As Integer
    '  Public Receipts_DT As New DataTable
    Dim Indx_ID As Integer
    '  Public isShowingDetails As Boolean = False
    Public IM_ID As Integer = 0
    Dim IM_QTY As Double = 0
    Dim U_Dt As New DataTable
    Dim Get_Unit As Boolean = False
    Dim U_Cargo As Double = 1
    Dim ALL_QTY As Double = 0
    Dim U_ID As Integer
    Public Barcode_IM As String = ""

    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()

    End Sub


    Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If St_Count() = 1 Then All_St_Panel.Visible = False
        '  FormType = 2
        Check_View_Control()

        'DefaultFormState = Me.Text

        Load_ST()

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
        IM_ID = itemId
        Get_Unit = False
        Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
        Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
        Fetch_IM_Units()
        QtyTextBox.Select()

        PriceTextBox.Text = Calc_Frm_Cost()
    End Sub


    Public Sub Check_View_Control()
        Min_SP_Panel.Visible = S_Allow_MinSP
        Min_SP_Panel_2.Visible = S_Allow_MinSP
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
                ST_cm.SelectedValue = PCH_ST_ID
                If PCH_ST_Can_change = False Then ST_cm.Enabled = False
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
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


    Private Sub ADDCatButton_Click(sender As Object, e As EventArgs) Handles ADDCatButton.Click


        If IM_ID = 0 Then
            MsgBox("حددالصنف", MsgBoxStyle.Exclamation)
            mySearchControl.txtSearch.Select()
        Else
            If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"

            If IM_min_QTY = False Then
                If IM_Check_Neg_QTY_() = 1 Then
                    MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
                    Exit Sub
                End If
            End If

            If Valid_Panel.Visible = True Then
                If D_Valid.Value.Date <= Date.Now.Date Then
                    MsgBox("صنف منتهية صلاحيته لا يمكن إدراجه", MsgBoxStyle.Critical, "خطأ")
                    Exit Sub
                End If
            End If

            Insert_Cat()

        End If

    End Sub

    Private Function IM_Check_Neg_QTY_()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_For_Format_IM"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters.AddWithValue("@Enterd_Qty", Convert.ToDouble(QtyTextBox.Text) * U_Cargo)
            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then F = .Parameters("@F").Value
        End With

        Return F
    End Function


    Private Sub ClearCatFields()

        mySearchControl.Clear_txt()
        Current_QTY.Clear()
        PriceTextBox.Clear()
        QtyTextBox.Clear()
        NewSalePrice_txt.Clear()
        U_Dt.Clear()
        Prev_Sale_Unit_txt.Clear()
        Min_SP_By_One_txt.Clear()
        Min_SP_txt.Clear()
        Min_SP_2_txt.Clear()
        Min_SP_2_By_One_txt.Clear()
        Barcode_IM = ""
        Valid_ListBox.Items.Clear()
        IM_ID = 0
    End Sub


    Private Sub Insert_Cat()
        If String.IsNullOrWhiteSpace(Barcode_IM) Then Barcode_IM = SELECT_BARCODE(IM_ID, IM_Unit_cm.SelectedValue)
        Dim Row_Index As Integer = 0
        If F_Format_Items_Auto.AGMetroGrid.Rows.Count > 0 Then Row_Index = F_Format_Items_Auto.AGMetroGrid.CurrentCell.RowIndex + 1
        Dim sqlComm As New SqlClient.SqlCommand()
        sqlComm.CommandText = "FRM_Details_Insert"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@Pch_T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@IM_ID", IM_ID)
        sqlComm.Parameters.AddWithValue("@U_ID", IM_Unit_cm.SelectedValue)
        sqlComm.Parameters.AddWithValue("@Price", 0)
        If Valid_Panel.Visible = True Then sqlComm.Parameters.AddWithValue("@D_Vaild", D_Valid.Value.Date)
        sqlComm.Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
        sqlComm.Parameters.AddWithValue("@QYT", QtyTextBox.Text)
        sqlComm.Parameters.AddWithValue("@Total", 0)
        sqlComm.Parameters.AddWithValue("@Barcode", Barcode_IM)

        If String.IsNullOrWhiteSpace(NewSalePrice_txt.Text) = False Then sqlComm.Parameters.AddWithValue("@NewSale", NewSalePrice_txt.Text)
        If String.IsNullOrWhiteSpace(NewSaleByOne.Text) = False Then sqlComm.Parameters.AddWithValue("@NewSaleByOne", NewSaleByOne.Text)

        If String.IsNullOrWhiteSpace(Min_SP_txt.Text) = False Then sqlComm.Parameters.AddWithValue("@Min_SP", Convert.ToDouble(Min_SP_txt.Text))
        If String.IsNullOrWhiteSpace(Min_SP_By_One_txt.Text) = False Then sqlComm.Parameters.AddWithValue("@Min_SP_ByOne", Convert.ToDouble(Min_SP_By_One_txt.Text))


        If SQL_SP_EXEC(sqlComm) = True Then

            Network_Edit_Tracker_insert(" الصنف:" + mySearchControl.txtSearch.Text + " الوحدة:" + IM_Unit_cm.Text + " العدد:" + QtyTextBox.Text + " السعر:" + PriceTextBox.Text + " البيع:" _
                                       + NewSalePrice_txt.Text + " بيع القطعة:" + NewSaleByOne.Text, F_Format_Items_Auto.Bill_ID_Txt.Text, 13, 1)

            'Update_Total()
            ClearCatFields()
            F_Format_Items_Auto.Pch_Contents_SELECT_Bill()
            If Row_Index > 0 Then F_Format_Items_Auto.AGMetroGrid.CurrentCell = F_Format_Items_Auto.AGMetroGrid.Rows(Row_Index).Cells("FRM_IM_NAME_CL")
            'If MY_Settings.S_Default = 0 Then
            '    Barcode_SH_txt.Select()
            'Else
            '    IM_SH_txt.Select()
            'End If

            Me.Close()
        End If

    End Sub

    Private Sub Valid_Notes_Insert()

            For i = 0 To Valid_ListBox.Items.Count - 1
                Valid_ListBox.SelectedIndex = i

                Dim sqlComm As New SqlClient.SqlCommand
                sqlComm.CommandText = "Valid_Notes_Insert"
                sqlComm.CommandType = CommandType.StoredProcedure
                sqlComm.Parameters.AddWithValue("@Pch_T_ID", Indx_ID)
                sqlComm.Parameters.AddWithValue("@IM_ID", IM_ID)
                sqlComm.Parameters.AddWithValue("@VALID_DATE", Valid_ListBox.SelectedItem)
                SQL_SP_EXEC(sqlComm)

            Next
        End Sub


    Private Sub PriceTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles PriceTextBox.KeyDown
            Select Case e.KeyCode
                Case Keys.Return, Keys.Left : NewSalePrice_txt.Select()
            Case Keys.Up : mySearchControl.txtSearch.Select()
            Case Keys.Right : QtyTextBox.Select()
            End Select
        End Sub

        Private Sub PriceTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PriceTextBox.KeyPress
            Check_Only_Float(sender, e)
        End Sub

        Private Sub PriceTextBox_TextChanged(sender As Object, e As EventArgs) Handles PriceTextBox.TextChanged
            Check_Point_in_FloatNum(sender, e)
            If Not String.IsNullOrWhiteSpace(PriceTextBox.Text) And U_Cargo > 1 Then
                CostByOne.Text = Convert.ToDouble(PriceTextBox.Text) / U_Cargo
                CalcAvgCost()
            Else
                CostByOne.Clear()
                CalcAvgCost()
            End If

            If S_Stores = True And Not String.IsNullOrWhiteSpace(ALL_QTY_txt.Text) Then
                If Convert.ToDouble(ALL_QTY_txt.Text) <= 0 Then
                    NewSalePrice_txt.Text = PriceTextBox.Text
                End If
            End If

        End Sub

        Private Sub QtyTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles QtyTextBox.KeyDown

            Select Case e.KeyCode
                Case Keys.Return, Keys.Left : PriceTextBox.Select()
            Case Keys.Up : mySearchControl.txtSearch.Select()
            Case Keys.Right
                    IM_Unit_cm.Select()
                    IM_Unit_cm.DroppedDown = True
            End Select

        End Sub

        Private Sub QtyTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QtyTextBox.KeyPress
            Check_Only_Float(sender, e)
        End Sub

        Private Sub QtyTextBox_TextChanged(sender As Object, e As EventArgs) Handles QtyTextBox.TextChanged
            Check_Point_in_FloatNum(sender, e)
            CalcAvgCost()
        End Sub

        Private Sub CalcAvgCost()
            If S_Stores = True Then
                If String.IsNullOrWhiteSpace(Current_QTY.Text) Then Current_QTY.Text = "0"
                If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "0"
                If Current_QTY.Text.Count > 0 Then If Convert.ToDouble(Current_QTY.Text) > 0 Then IM_Set_Avg()
            End If
        End Sub

        Public Sub IM_Set_Avg()
            Dim Prev_Cost As Double = 0
            Dim Prev_Qty As Double = 0
            If String.IsNullOrWhiteSpace(PriceTextBox.Text) Then PriceTextBox.Text = "0"

            Dim c As New C
            c = New C
            Try
                Dim s As String
                s = "select ISNULL(SUM(QTY),0) AS QTY from ST_Balance_V WHERE IM_ID = '" & IM_ID & "'"
                c.Com = New SqlClient.SqlCommand(s, c.Con)
                c.Con.Open()
                c.Dr = c.Com.ExecuteReader
                If c.Dr.HasRows Then
                    c.Dr.Read()
                    Prev_Qty = c.Dr("QTY")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            '------------------------------------------------------------------------------------------------

            c = New C
            Try
                Dim s As String
                s = "select Cost from IM_All_V WHERE IM_ID = '" & IM_ID & "'"
                c.Com = New SqlClient.SqlCommand(s, c.Con)
                c.Con.Open()
                c.Dr = c.Com.ExecuteReader
                If c.Dr.HasRows Then
                    c.Dr.Read()
                    Prev_Cost = c.Dr("Cost")
                    NewSalePrice_txt.Text =
                 ((((Prev_Cost * Prev_Qty) + (Convert.ToDouble(PriceTextBox.Text) * Convert.ToDouble(QtyTextBox.Text))) / (Prev_Qty + Convert.ToDouble(QtyTextBox.Text))) * U_Cargo).ToString("0.00")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Sub

    Public Sub Fetch_IM_Units()

        Dim c As New C
        U_Dt.Clear()
        Try
            Dim s As String
            s = "select U_ID,U_Name from IM_Menu_Units_V WHERE IM_ID = '" & IM_ID & "' Order By U_Cargo Desc"
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

    Private Sub NewSalePrice_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NewSalePrice_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub NewSalePrice_txt_TextChanged(sender As Object, e As EventArgs) Handles NewSalePrice_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
        If Not String.IsNullOrWhiteSpace(NewSalePrice_txt.Text) And U_Cargo > 1 Then
            NewSaleByOne.Text = (Convert.ToDouble(NewSalePrice_txt.Text) / U_Cargo).ToString("N")
        Else
            NewSaleByOne.Clear()
        End If
    End Sub

    Private Sub IM_CalcAvgCost_btn_Click(sender As Object, e As EventArgs) Handles IM_CalcAvgCost_btn.Click
        If ALL_QTY_txt.Text.Count > 0 Then
            If Convert.ToDouble(ALL_QTY_txt.Text) > 0 And PriceTextBox.Text.Count > 0 Then IM_Calc_Avg(True, IM_ID)
        End If
    End Sub

    Public Function IM_Calc_Avg(isMsg As Boolean, IM_ID As Integer)
        Dim c As New C
        Dim Prev_Cost As Double = 0
        Dim Prev_Qty As Double = 0

        Try
            Dim s As String
            s = "select Cost from IM_All_V WHERE IM_ID = '" & IM_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Prev_Cost = c.Dr("Cost") * U_Cargo
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        c = New C
        Try
            Dim s As String
            s = "select ISNULL(SUM(QTY),0) AS QTY from ST_Balance_V WHERE IM_ID = '" & IM_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Prev_Qty = c.Dr("QTY") / U_Cargo
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        Dim AVG_COST As Double = ((Prev_Cost * Prev_Qty) +
                                  ((Convert.ToDouble(PriceTextBox.Text) / U_Cargo) * (Convert.ToDouble(QtyTextBox.Text) * U_Cargo))) _
                              / (Prev_Qty + (Convert.ToDouble(QtyTextBox.Text)))

        If isMsg = True Then MsgBox((AVG_COST).ToString("00.00"), MsgBoxStyle.Information, "إجمالي التكلفــة")

        Return AVG_COST
    End Function


    Private Sub NewSalePrice_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles NewSalePrice_txt.KeyDown
        Select Case e.KeyCode
            Case Keys.Return

                If Min_SP_Panel.Visible = True Then
                    Min_SP_txt.Select()
                    Exit Sub
                End If

                If NewSaleByOne.Visible = False Then
                    If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)
                Else
                    CostByOne.Select()
                End If

            Case Keys.Left : If NewSaleByOne.Visible = False Then NewSaleByOne.Select()
            Case Keys.Up : mySearchControl.txtSearch.Select()
            Case Keys.Right : PriceTextBox.Select()
            Case Keys.Down
                If Min_SP_txt.Visible = True Then Min_SP_txt.Select()
        End Select
    End Sub

    Private Sub IM_Fetch_QTY()
        Dim c As New C
        Try
            Dim s As String
            s = "select U_ID,U_Cargo,Price from IM_Menu_Units_V WHERE U_ID = '" & IM_Unit_cm.SelectedValue & "' AND IM_ID = '" & IM_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                U_Cargo = c.Dr("U_Cargo")
                Prev_Sale_Unit_txt.Text = c.Dr("Price")
                Dim N As Double = (Convert.ToDouble(IM_QTY) / c.Dr("U_Cargo"))
                Current_QTY.Text = N.ToString("N")
                ALL_QTY_txt.Text = ALL_QTY / U_Cargo
                ' U_ID = c.Dr("U_ID")
                If U_Cargo > 1 Then
                    One_Panel.Visible = True
                    Two_Panel.Visible = True
                    NewSaleByOne.Visible = True
                    CostByOne.Visible = True

                    Min_SP_By_One_Lb.Visible = True
                    Min_SP_By_One_txt.Visible = True

                    'Min_SP_2_By_One_txt.Visible = True
                    'Min_SP_2_By_One_Lb.Visible = True

                    CostByOne.Text = Calc_Frm_Cost()
                Else
                    One_Panel.Visible = False
                    Two_Panel.Visible = False
                    NewSaleByOne.Visible = False
                    CostByOne.Visible = False

                    Min_SP_By_One_Lb.Visible = False
                    Min_SP_By_One_txt.Visible = False

                    'Min_SP_2_By_One_txt.Visible = False
                    'Min_SP_2_By_One_Lb.Visible = False

                    CostByOne.Clear()
                End If
                CalcAvgCost()
                PriceTextBox.Text = Calc_Frm_Cost() * U_Cargo

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function Calc_Frm_Cost()
        Dim NewCost As Double = 0
        Dim C = New C
        Try
            Dim s As String
            s = "SELECT ISNULL(SUM(CostVQty),0) AS NewCost FROM IM_Formating_Menu_V WHERE IM_ID = '" & IM_ID & "'"
            C.Com = New SqlClient.SqlCommand(s, C.Con)
            C.Con.Open()
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                NewCost = C.Dr("NewCost")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return NewCost
    End Function

    Private Sub IM_Fetch_last_Pch_Price()
        PriceTextBox.Clear()
        Dim c As New C
        Try
            Dim s As String
            s = "select TOP 1 Price from Pch_Details WHERE IM_ID = '" & IM_ID & "' AND U_ID = '" & U_ID & "' AND isDepended = 1 ORDER BY Date DESC"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                PriceTextBox.Text = c.Dr("Price")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles IM_Unit_cm.SelectedValueChanged
        If Get_Unit = True Then IM_Fetch_QTY()
    End Sub


    Private Sub IM_Unit_cm_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_Unit_cm.KeyDown

        Select Case e.KeyCode
            Case Keys.Return, Keys.Left : QtyTextBox.Select()
        End Select

    End Sub

    Private Sub NewSaleByOne_TextChanged(sender As Object, e As EventArgs) Handles NewSaleByOne.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub CostByOne_KeyDown(sender As Object, e As KeyEventArgs) Handles CostByOne.KeyDown
        Select Case e.KeyCode
            Case Keys.Return : NewSaleByOne.Select()
            Case Keys.Up : mySearchControl.txtSearch.Select()
            Case Keys.Right : NewSalePrice_txt.Select()
            Case Keys.Left : NewSaleByOne.Select()
        End Select
    End Sub

    Private Sub NewSaleByOne_KeyDown(sender As Object, e As KeyEventArgs) Handles NewSaleByOne.KeyDown
        Select Case e.KeyCode
            Case Keys.Return

                If Min_SP_Panel_2.Visible = True Then
                    Min_SP_2_txt.Select()
                    Exit Sub
                Else
                    If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)
                End If

            Case Keys.Up : mySearchControl.txtSearch.Select()
            Case Keys.Right : CostByOne.Select()
            Case Keys.Down
                If Min_SP_By_One_txt.Visible = True Then Min_SP_By_One_txt.Select()
        End Select
    End Sub


    Private Sub ST_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles ST_cm.SelectedValueChanged
        If Get_Unit = True Then
            Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
            IM_Fetch_QTY()
        End If
    End Sub

    Private Sub Ass_U_btn_Click(sender As Object, e As EventArgs) Handles Ass_U_btn.Click
        If IM_ID > 0 Then
            Beep()
            If MessageBox.Show(" إضافة وحدة للصنف " + mySearchControl.txtSearch.Text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                Add_Unit.IM_ID = IM_ID
                Add_Unit.ShowDialog()
            End If
        End If
    End Sub

    Private Sub ALL_QTY_txt_TextChanged(sender As Object, e As EventArgs) Handles CostByOne.TextChanged, ALL_QTY_txt.TextChanged  ', NewSaleByOne.TextChanged
        If Not String.IsNullOrWhiteSpace(sender.Text) Then
            Dim N As Double = sender.Text
            sender.Text = N.ToString("N")
        End If
    End Sub

    Private Sub Min_SP_txt_TextChanged(sender As Object, e As EventArgs) Handles Min_SP_txt.TextChanged

        If MIN_RD.Checked = True Then
            If Not String.IsNullOrWhiteSpace(Min_SP_txt.Text) And U_Cargo > 1 Then
                Min_SP_By_One_txt.Text = (Convert.ToDouble(Min_SP_txt.Text) / U_Cargo).ToString("N")
            Else
                Min_SP_By_One_txt.Clear()
            End If

        End If
    End Sub

    Private Sub Min_SP_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Min_SP_txt.KeyPress, Min_SP_By_One_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Min_SP_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Min_SP_txt.KeyDown

        Select Case e.KeyCode
            Case Keys.Up
                NewSalePrice_txt.Select()
            Case Keys.Return

                If Min_SP_By_One_txt.Visible = True Then
                    Min_SP_By_One_txt.Select()
                    Exit Sub
                End If

                If Min_SP_Panel_2.Visible = True Then
                    Min_SP_2_txt.Select()
                    Exit Sub
                End If

        End Select

    End Sub

    Private Sub Min_SP_By_One_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Min_SP_By_One_txt.KeyDown

        Select Case e.KeyCode
            Case Keys.Up
                NewSaleByOne.Select()
            Case Keys.Return
                NewSaleByOne.Select()
        End Select

    End Sub

    Private Sub NewSaleByOne_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NewSaleByOne.KeyPress
        Check_Only_Float(sender, e)
    End Sub



    Private Sub Min_SP_2_txt_TextChanged(sender As Object, e As EventArgs) Handles Min_SP_2_txt.TextChanged

        If MIN_RD_2.Checked = True Then
            If Not String.IsNullOrWhiteSpace(Min_SP_2_txt.Text) And U_Cargo > 1 Then
                Min_SP_2_By_One_txt.Text = (Convert.ToDouble(Min_SP_2_txt.Text) / U_Cargo).ToString("N")
            Else
                Min_SP_2_By_One_txt.Clear()
            End If
        End If

    End Sub

    Private Sub Min_SP_2_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Min_SP_2_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Min_SP_2_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Min_SP_2_txt.KeyDown

        Select Case e.KeyCode
            Case Keys.Up
                NewSalePrice_txt.Select()
            Case Keys.Return

                If Min_SP_2_By_One_txt.Visible = True Then
                    Min_SP_2_By_One_txt.Select()
                    Exit Sub
                Else
                    If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)
                End If

        End Select

    End Sub

    Private Sub Min_SP_2_By_One_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Min_SP_2_By_One_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Min_SP_2_By_One_txt_TextChanged(sender As Object, e As EventArgs) Handles Min_SP_2_By_One_txt.TextChanged

        If MIN_BY_ONE_RD_2.Checked = True Then
            If Not String.IsNullOrWhiteSpace(Min_SP_2_By_One_txt.Text) And U_Cargo > 1 Then
                Min_SP_2_txt.Text = (Convert.ToDouble(Min_SP_2_By_One_txt.Text) * U_Cargo).ToString("N")
            Else
                Min_SP_2_txt.Clear()
            End If
        End If

    End Sub


    Private Sub Add_Valid_Btn_Click(sender As Object, e As EventArgs) Handles Add_Valid_Btn.Click
            Valid_ListBox.Items.Add(Valid_For_List_Date.Value)
        End Sub

        Private Sub Remove_Valid_Btn_Click(sender As Object, e As EventArgs) Handles Remove_Valid_Btn.Click
            Valid_ListBox.Items.Remove(Valid_ListBox.SelectedItem)
        End Sub


    Private Sub Show_IM_Note_Valid_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Show_IM_Note_Valid_CB.CheckedChanged
            CB_CHecked(sender)
            IM_Valid_Note_Panel.Visible = Show_IM_Note_Valid_CB.Checked()
        End Sub


    Private Sub Min_SP_2_By_One_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Min_SP_2_By_One_txt.KeyDown
            Select Case e.KeyCode
                Case Keys.Return

                    If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)

                Case Keys.Up : NewSalePrice_txt.Select()
                Case Keys.Right : Min_SP_2_txt.Select()

            End Select
        End Sub

        Private Sub Confirm_ADD_bercent_Click(sender As Object, e As EventArgs) Handles Confirm_ADD_bercent.Click
            If SP_CB.Checked = True Then NewSalePrice_txt.Text = ((Convert.ToDouble(bercent_ADD_txt.Text) * Convert.ToDouble(PriceTextBox.Text)) / 100) + Convert.ToDouble(PriceTextBox.Text)

            If SP_1_CB.Checked = True Then Min_SP_txt.Text = ((Convert.ToDouble(bercent_ADD_txt.Text) * Convert.ToDouble(PriceTextBox.Text)) / 100) + Convert.ToDouble(PriceTextBox.Text)

            If SP_2_CB.Checked = True Then Min_SP_2_txt.Text = ((Convert.ToDouble(bercent_ADD_txt.Text) * Convert.ToDouble(PriceTextBox.Text)) / 100) + Convert.ToDouble(PriceTextBox.Text)

        End Sub

    Private Sub SP_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SP_CB.CheckedChanged
            CB_CHecked(sender)
        End Sub

        Private Sub SP_1_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SP_1_CB.CheckedChanged
            CB_CHecked(sender)
        End Sub

        Private Sub SP_2_CB_CheckedChanged(sender As Object, e As EventArgs) Handles SP_2_CB.CheckedChanged
            CB_CHecked(sender)
        End Sub

        Private Sub Min_SP_Panel_VisibleChanged(sender As Object, e As EventArgs) Handles Min_SP_Panel.VisibleChanged
            SP_1_CB.Visible = Min_SP_Panel.Visible
        End Sub

        Private Sub Min_SP_Panel_2_VisibleChanged(sender As Object, e As EventArgs) Handles Min_SP_Panel_2.VisibleChanged
            SP_2_CB.Visible = Min_SP_Panel_2.Visible
        End Sub


    Private Sub Min_SP_By_One_txt_TextChanged(sender As Object, e As EventArgs) Handles Min_SP_By_One_txt.TextChanged

        If MIN_BY_ONE_RD.Checked = True Then

                If Not String.IsNullOrWhiteSpace(Min_SP_By_One_txt.Text) And U_Cargo > 1 Then
                    Min_SP_txt.Text = (Convert.ToDouble(Min_SP_By_One_txt.Text) * U_Cargo).ToString("N")
                Else
                    Min_SP_txt.Clear()
                End If

            End If

        End Sub

    Private Sub Exit_Btn_Click(sender As Object, e As EventArgs) Handles Exit_Btn.Click
        Me.Close()
    End Sub


    'Private Sub ADD_New_IM_btn_Click(sender As Object, e As EventArgs) Handles ADD_New_IM_btn.Click
    '    IM_ADD_New.ShowDialog()
    '    If is_Add_New_IM = True Then QtyTextBox.Select()
    'End Sub
End Class