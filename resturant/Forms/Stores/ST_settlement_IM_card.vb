Public Class ST_settlement_IM_card : Inherits System.Windows.Forms.Form

    'Dim DefaultFormState As String = ""
    Public T_ID As Integer
    'Public Receipts_DT As New DataTable
    'Dim Indx_ID As Integer
    'Public isShowingDetails As Boolean = False
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
        'FormType = 2
        'Check_View_Control()

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
        Load_SelectedItemData()
        '  QtyTextBox.Select()

        'If isValid = 1 Then
        '    Valid_Panel.Visible = True
        '    D_Valid.Value = Date.Now
        'Else
        '    Valid_Panel.Visible = False
        'End If
    End Sub


    'Public Sub Check_View_Control()
    '    Min_SP_Panel.Visible = S_Allow_MinSP
    '    Min_SP_Panel_2.Visible = S_Allow_MinSP
    'End Sub


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


        If IM_ID > 0 Then
            Dim F As New IM_Update_Qty
            F.IM_ID = IM_ID
            F.IM_NAME = mySearchControl.txtSearch.Text
            F.Unit_NAME = IM_Unit_cm.Text
            F.ST_NAME = ST_cm.Text
            F.CURRENT_QTY = Current_QTY.Text
            F.ST_ID = ST_cm.SelectedValue
            F.Barcode = SELECT_BARCODE(IM_ID, U_ID)
            F.ShowDialog()
        End If

    End Sub


    Private Sub ClearCatFields()


        mySearchControl.Clear_txt()
        Current_QTY.Clear()
        'PriceTextBox.Clear()
        'QtyTextBox.Clear()
        'NewSalePrice_txt.Clear()
        U_Dt.Clear()
        'Prev_Sale_Unit_txt.Clear()
        'Min_SP_By_One_txt.Clear()
        'Min_SP_txt.Clear()
        'Min_SP_2_txt.Clear()
        'Min_SP_2_By_One_txt.Clear()
        Barcode_IM = ""
        '   Valid_ListBox.Items.Clear()
        IM_ID = 0
    End Sub

    Public Sub Fetch_IM_Units()

        Dim c As New C
        U_Dt.Clear()
        Try
            Dim s As String
            s = "select U_IM_ID,U_Name from IM_Menu_Units_V WHERE IM_ID = '" & IM_ID & "' Order By U_Cargo Desc"
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

    Private Sub Load_SelectedItemData()

        Get_Unit = False
        U_Dt.Clear()

        Try

            Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr)

                cn.Open()

                Using allQtyCmd As New SqlClient.SqlCommand(
                    "select ISNULL(SUM(QTY),0) AS QTY from ST_Balance_V WHERE IM_ID = @IM_ID",
                    cn
                )

                    allQtyCmd.Parameters.AddWithValue("@IM_ID", IM_ID)
                    ALL_QTY = Convert.ToDouble(allQtyCmd.ExecuteScalar())

                End Using

                Dim stId As Object = ST_cm.SelectedValue
                If stId Is Nothing OrElse stId Is DBNull.Value Then stId = 0

                Using storeQtyCmd As New SqlClient.SqlCommand(
                    "select ISNULL(SUM(QTY),0) AS QTY from ST_Balance_V WHERE IM_ID = @IM_ID AND ST_ID = @ST_ID",
                    cn
                )

                    storeQtyCmd.Parameters.AddWithValue("@IM_ID", IM_ID)
                    storeQtyCmd.Parameters.AddWithValue("@ST_ID", stId)
                    IM_QTY = Convert.ToDouble(storeQtyCmd.ExecuteScalar())

                End Using

                Using unitsCmd As New SqlClient.SqlCommand(
                    "select U_IM_ID,U_Name from IM_Menu_Units_V WHERE IM_ID = @IM_ID Order By U_Cargo Desc",
                    cn
                )

                    unitsCmd.Parameters.AddWithValue("@IM_ID", IM_ID)

                    Using da As New SqlClient.SqlDataAdapter(unitsCmd)
                        da.Fill(U_Dt)
                    End Using

                End Using

                IM_Unit_cm.DataSource = U_Dt
                IM_Unit_cm.DisplayMember = "U_Name"
                IM_Unit_cm.ValueMember = "U_IM_ID"

                Get_Unit = True
                ApplySelectedUnitData(cn)

            End Using

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub ApplySelectedUnitData(cn As SqlClient.SqlConnection)

        If IM_Unit_cm.SelectedValue Is Nothing Then
            Return
        End If

        Using unitCmd As New SqlClient.SqlCommand(
            "select U_ID,U_Cargo,Price,Min_SP from IM_Menu_Units_V WHERE U_IM_ID = @U_IM_ID AND IM_ID = @IM_ID",
            cn
        )

            unitCmd.Parameters.AddWithValue("@U_IM_ID", IM_Unit_cm.SelectedValue)
            unitCmd.Parameters.AddWithValue("@IM_ID", IM_ID)

            Using rdr As SqlClient.SqlDataReader = unitCmd.ExecuteReader()

                If rdr.Read() Then

                    U_Cargo = Convert.ToDouble(rdr("U_Cargo"))

                    If U_Cargo <> 0 Then
                        Current_QTY.Text = (Convert.ToDouble(IM_QTY) / U_Cargo).ToString("N")
                        ALL_QTY_txt.Text = ALL_QTY / U_Cargo
                    Else
                        Current_QTY.Text = "0"
                        ALL_QTY_txt.Text = "0"
                    End If

                    U_ID = Convert.ToInt32(rdr("U_ID"))

                End If

            End Using

        End Using

    End Sub


    Private Sub IM_Fetch_QTY()
        Dim c As New C
        Try
            Dim s As String
            s = "select U_ID,U_Cargo,Price,Min_SP from IM_Menu_Units_V WHERE U_IM_ID = '" & IM_Unit_cm.SelectedValue & "' AND IM_ID = '" & IM_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                U_Cargo = c.Dr("U_Cargo")
                '  Prev_Sale_Unit_txt.Text = c.Dr("Price")

                Dim N As Double = (Convert.ToDouble(IM_QTY) / c.Dr("U_Cargo"))
                Current_QTY.Text = N.ToString("N")
                ALL_QTY_txt.Text = ALL_QTY / U_Cargo
                U_ID = c.Dr("U_ID")
                'If U_Cargo > 1 Then
                '    One_Panel.Visible = True
                '    Two_Panel.Visible = True
                '    NewSaleByOne.Visible = True
                '    CostByOne.Visible = True
                '    Min_SP_By_One_Lb.Visible = True
                '    Min_SP_By_One_txt.Visible = True

                '    Min_SP_2_By_One_txt.Visible = True
                '    Min_SP_2_By_One_Lb.Visible = True

                'Else
                '    One_Panel.Visible = False
                '    Two_Panel.Visible = False
                '    NewSaleByOne.Visible = False
                '    CostByOne.Visible = False
                '    Min_SP_By_One_Lb.Visible = False
                '    Min_SP_By_One_txt.Visible = False

                '    Min_SP_2_By_One_txt.Visible = False
                '    Min_SP_2_By_One_Lb.Visible = False

                'End If
                'CalcAvgCost()
                'IM_Fetch_last_Pch_Price()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub IM_Fetch_last_Pch_Price()
    '    PriceTextBox.Clear()
    '    Dim c As New C
    '    Try
    '        Dim s As String
    '        s = "select TOP 1 Price from Pch_Details WHERE IM_ID = '" & IM_ID & "' AND U_ID = '" & U_ID & "' AND isDepended = 1 ORDER BY Date DESC"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            PriceTextBox.Text = c.Dr("Price")
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles IM_Unit_cm.SelectedValueChanged
        If Get_Unit = True Then IM_Fetch_QTY()
    End Sub


    'Private Sub IM_Unit_cm_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_Unit_cm.KeyDown

    '    Select Case e.KeyCode
    '        Case Keys.Return, Keys.Left : QtyTextBox.Select()
    '    End Select

    'End Sub

    'Private Sub NewSaleByOne_TextChanged(sender As Object, e As EventArgs)
    '    Check_Point_in_FloatNum(sender, e)
    'End Sub

    'Private Sub CostByOne_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return : NewSaleByOne.Select()
    '        Case Keys.Up : mySearchControl.txtSearch.Select()
    '        Case Keys.Right : NewSalePrice_txt.Select()
    '        Case Keys.Left : NewSaleByOne.Select()
    '    End Select
    'End Sub

    'Private Sub NewSaleByOne_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return

    '            If Min_SP_Panel_2.Visible = True Then
    '                Min_SP_2_txt.Select()
    '                Exit Sub
    '            Else
    '                If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)
    '            End If

    '        Case Keys.Up : mySearchControl.txtSearch.Select()
    '        Case Keys.Right : CostByOne.Select()
    '        Case Keys.Down
    '            If Min_SP_By_One_txt.Visible = True Then Min_SP_By_One_txt.Select()
    '    End Select
    'End Sub


    Private Sub ST_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles ST_cm.SelectedValueChanged
        If Get_Unit = True Then
            Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
            IM_Fetch_QTY()
        End If
    End Sub

    'Private Sub Ass_U_btn_Click(sender As Object, e As EventArgs)
    '    If IM_ID > 0 Then
    '        Beep()
    '        If MessageBox.Show(" إضافة وحدة للصنف " + mySearchControl.txtSearch.Text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
    '            Add_Unit.IM_ID = IM_ID
    '            Add_Unit.ShowDialog()
    '        End If
    '    End If
    'End Sub

    Private Sub ALL_QTY_txt_TextChanged(sender As Object, e As EventArgs) Handles ALL_QTY_txt.TextChanged
        If Not String.IsNullOrWhiteSpace(sender.Text) Then
            Dim N As Double = sender.Text
            sender.Text = N.ToString("N")
        End If
    End Sub

    'Private Sub Min_SP_txt_TextChanged(sender As Object, e As EventArgs)

    '    If MIN_RD.Checked = True Then
    '        If Not String.IsNullOrWhiteSpace(Min_SP_txt.Text) And U_Cargo > 1 Then
    '            Min_SP_By_One_txt.Text = (Convert.ToDouble(Min_SP_txt.Text) / U_Cargo).ToString("N")
    '        Else
    '            Min_SP_By_One_txt.Clear()
    '        End If

    '    End If
    'End Sub

    'Private Sub Min_SP_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub Min_SP_txt_KeyDown(sender As Object, e As KeyEventArgs)

    '    Select Case e.KeyCode
    '        Case Keys.Up
    '            NewSalePrice_txt.Select()
    '        Case Keys.Return

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

    'Private Sub Min_SP_By_One_txt_KeyDown(sender As Object, e As KeyEventArgs)

    '    Select Case e.KeyCode
    '        Case Keys.Up
    '            NewSaleByOne.Select()
    '        Case Keys.Return
    '            NewSaleByOne.Select()
    '    End Select

    'End Sub

    'Private Sub NewSaleByOne_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub



    'Private Sub Min_SP_2_txt_TextChanged(sender As Object, e As EventArgs)

    '    If MIN_RD_2.Checked = True Then
    '        If Not String.IsNullOrWhiteSpace(Min_SP_2_txt.Text) And U_Cargo > 1 Then
    '            Min_SP_2_By_One_txt.Text = (Convert.ToDouble(Min_SP_2_txt.Text) / U_Cargo).ToString("N")
    '        Else
    '            Min_SP_2_By_One_txt.Clear()
    '        End If
    '    End If

    'End Sub

    'Private Sub Min_SP_2_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub Min_SP_2_txt_KeyDown(sender As Object, e As KeyEventArgs)

    '    Select Case e.KeyCode
    '        Case Keys.Up
    '            NewSalePrice_txt.Select()
    '        Case Keys.Return

    '            If Min_SP_2_By_One_txt.Visible = True Then
    '                Min_SP_2_By_One_txt.Select()
    '                Exit Sub
    '            Else
    '                If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)
    '            End If

    '    End Select

    'End Sub

    'Private Sub Min_SP_2_By_One_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    Check_Only_Float(sender, e)
    'End Sub

    'Private Sub Min_SP_2_By_One_txt_TextChanged(sender As Object, e As EventArgs)

    '    If MIN_BY_ONE_RD_2.Checked = True Then
    '        If Not String.IsNullOrWhiteSpace(Min_SP_2_By_One_txt.Text) And U_Cargo > 1 Then
    '            Min_SP_2_txt.Text = (Convert.ToDouble(Min_SP_2_By_One_txt.Text) * U_Cargo).ToString("N")
    '        Else
    '            Min_SP_2_txt.Clear()
    '        End If
    '    End If

    'End Sub


    'Private Sub Add_Valid_Btn_Click(sender As Object, e As EventArgs)
    '    Valid_ListBox.Items.Add(Valid_For_List_Date.Value)
    'End Sub

    'Private Sub Remove_Valid_Btn_Click(sender As Object, e As EventArgs)
    '    Valid_ListBox.Items.Remove(Valid_ListBox.SelectedItem)
    'End Sub


    'Private Sub Show_IM_Note_Valid_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    '    IM_Valid_Note_Panel.Visible = Show_IM_Note_Valid_CB.Checked()
    'End Sub


    'Private Sub Min_SP_2_By_One_txt_KeyDown(sender As Object, e As KeyEventArgs)
    '    Select Case e.KeyCode
    '        Case Keys.Return

    '            If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)

    '        Case Keys.Up : NewSalePrice_txt.Select()
    '        Case Keys.Right : Min_SP_2_txt.Select()

    '    End Select
    'End Sub

    'Private Sub Confirm_ADD_bercent_Click(sender As Object, e As EventArgs)
    '    If SP_CB.Checked = True Then NewSalePrice_txt.Text = ((Convert.ToDouble(bercent_ADD_txt.Text) * Convert.ToDouble(PriceTextBox.Text)) / 100) + Convert.ToDouble(PriceTextBox.Text)

    '    If SP_1_CB.Checked = True Then Min_SP_txt.Text = ((Convert.ToDouble(bercent_ADD_txt.Text) * Convert.ToDouble(PriceTextBox.Text)) / 100) + Convert.ToDouble(PriceTextBox.Text)

    '    If SP_2_CB.Checked = True Then Min_SP_2_txt.Text = ((Convert.ToDouble(bercent_ADD_txt.Text) * Convert.ToDouble(PriceTextBox.Text)) / 100) + Convert.ToDouble(PriceTextBox.Text)

    'End Sub

    'Private Sub SP_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    'End Sub

    'Private Sub SP_1_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    'End Sub

    'Private Sub SP_2_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    'End Sub

    'Private Sub Min_SP_Panel_VisibleChanged(sender As Object, e As EventArgs)
    '    SP_1_CB.Visible = Min_SP_Panel.Visible
    'End Sub

    'Private Sub Min_SP_Panel_2_VisibleChanged(sender As Object, e As EventArgs)
    '    SP_2_CB.Visible = Min_SP_Panel_2.Visible
    'End Sub


    'Private Sub Min_SP_By_One_txt_TextChanged(sender As Object, e As EventArgs)

    '    If MIN_BY_ONE_RD.Checked = True Then

    '        If Not String.IsNullOrWhiteSpace(Min_SP_By_One_txt.Text) And U_Cargo > 1 Then
    '            Min_SP_txt.Text = (Convert.ToDouble(Min_SP_By_One_txt.Text) * U_Cargo).ToString("N")
    '        Else
    '            Min_SP_txt.Clear()
    '        End If

    '    End If

    'End Sub

    Private Sub Exit_Btn_Click(sender As Object, e As EventArgs) Handles Exit_Btn.Click
        Me.Close()
    End Sub


    'Private Sub ADD_New_IM_btn_Click(sender As Object, e As EventArgs)
    '    IM_ADD_New.ShowDialog()
    '    If is_Add_New_IM = True Then QtyTextBox.Select()
    'End Sub
End Class
