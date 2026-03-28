Public Class Outside_Sales_IM_card : Inherits System.Windows.Forms.Form

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

    Public Sub Check_View_Control()
        IM_Cost_Panel.Visible = U_SB_Show_IM_COST

    End Sub

    Private Sub HandleItemSelected(itemId As Integer, isValid As String)
        IM_ID = itemId
        Get_Unit = False
        Load_IM_ST_QTY(IM_ID, ST_cm, IM_QTY)
        Load_IM_ALL_QTY(IM_ID, ALL_QTY, ALL_QTY_txt, U_Cargo)
        Fetch_IM_Units()
        QtyTextBox.Select()

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


        If IM_ID = 0 Then
            MsgBox("حددالصنف", MsgBoxStyle.Exclamation)
            '  SELECT_IM()
        ElseIf String.IsNullOrWhiteSpace(PriceTextBox.Text) Then
            MsgBox("حدد سعر القطعة", MsgBoxStyle.Exclamation)
            PriceTextBox.Select()
        Else
            If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"
            For i = 0 To F_Outside_Sales.AGMetroGrid.Rows.Count - 1
                If F_Outside_Sales.AGMetroGrid.Rows(i).Cells("EX_ID_CL").Value = IM_ID And F_Outside_Sales.AGMetroGrid.Rows(i).Cells("St_ID_CL").Value = ST_cm.SelectedValue Then

                    If Valid_Panel.Visible = True Then
                        If F_Outside_Sales.AGMetroGrid.Rows(i).Cells("D_Valid_CL").Value = D_Valid.Text Then
                            If MessageBox.Show(" تم إدخال الصنف ... هل تريد الإستمرار ", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
                                Exit Sub
                            End If

                        End If
                    Else
                        If MessageBox.Show(" تم إدخال الصنف ... هل تريد الإستمرار ", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
                            Exit Sub
                        End If

                    End If
                End If
            Next

            'If Not String.IsNullOrWhiteSpace(NewSalePrice_txt.Text) And Convert.ToDouble(Prev_Sale_Unit_txt.Text) > 0 Then
            '    If Convert.ToDouble(ALL_QTY_txt.Text) > 0 Then
            '        If Convert.ToDouble(NewSalePrice_txt.Text) < IM_Calc_Avg(False, IM_ID) Then
            '            If MessageBox.Show(" سعر البيع أقل من التكلفة .. هل تريد الإستمرار ", "تحذير", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
            '                Exit Sub
            '            End If
            '        End If
            '    End If
            'End If


            If Valid_Panel.Visible = True Then
                If Ban_Expierd_IM_MV = True Then
                    If D_Valid.Value.Date <= Date.Now.Date Then
                        MsgBox("صنف منتهية صلاحيته لا يمكن شرائه", MsgBoxStyle.Critical, "خطأ")
                        Exit Sub
                    End If
                End If
            End If

            Insert_Cat()
        End If

    End Sub


    Private Sub ClearCatFields()
        mySearchControl.Clear_txt()
        Current_QTY.Clear()
        PriceTextBox.Clear()
        QtyTextBox.Clear()
        U_Dt.Clear()
        Barcode_IM = ""
        IM_ID = 0
    End Sub


    Private Sub Insert_Cat()

        If String.IsNullOrWhiteSpace(Barcode_IM) Then Barcode_IM = SELECT_BARCODE(IM_ID, U_ID)
        'Dim Row_Index As Integer = 0
        'If AGMetroGrid.Rows.Count > 0 Then Row_Index = AGMetroGrid.CurrentCell.RowIndex + 1
        Dim sqlComm As New SqlClient.SqlCommand()
        sqlComm.CommandText = "[Outsale_Details_Insert]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@Outsale_T_ID", T_ID)
        sqlComm.Parameters.AddWithValue("@IM_ID", IM_ID)
        sqlComm.Parameters.AddWithValue("@U_ID", U_ID)
        sqlComm.Parameters.AddWithValue("@Price", PriceTextBox.Text)
        sqlComm.Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
        sqlComm.Parameters.AddWithValue("@Barcode", Barcode_IM)
        sqlComm.Parameters.AddWithValue("@Total", Convert.ToDouble(QtyTextBox.Text) * Convert.ToDouble(PriceTextBox.Text))
        If Valid_Panel.Visible = True Then sqlComm.Parameters.AddWithValue("@D_Vaild", D_Valid.Text)
        If String.IsNullOrWhiteSpace(QtyTextBox.Text) = False Then sqlComm.Parameters.AddWithValue("@QYT", Convert.ToDouble(QtyTextBox.Text))
        sqlComm.Parameters.AddWithValue("@On_Update", True)

        If String.IsNullOrWhiteSpace(PriceTextBox.Text) = False Then sqlComm.Parameters.AddWithValue("@SPRICE", Convert.ToDouble(PriceTextBox.Text))

        If SQL_SP_EXEC(sqlComm) = True Then
            Network_Edit_Tracker_insert(" الصنف:" & mySearchControl.txtSearch.Text & " الوحدة:" & IM_Unit_cm.Text & " العدد:" & QtyTextBox.Text & " السعر:" & PriceTextBox.Text, F_Outside_Sales.Bill_ID_Txt.Text, 35, 1)

            'Update_Total()
            ClearCatFields()
            F_Outside_Sales.SELECT_EXP_Cats(T_ID)

            Me.Close()
        End If
    End Sub


    Private Sub PriceTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles PriceTextBox.KeyDown
        Select Case e.KeyCode
            'Case Keys.Return, Keys.Left : NewSalePrice_txt.Select()
            Case Keys.Up : mySearchControl.txtSearch.Select()
            Case Keys.Right : QtyTextBox.Select()
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
                Dim N As Double = (Convert.ToDouble(IM_QTY) / c.Dr("U_Cargo"))
                Current_QTY.Text = N.ToString("N")
                ALL_QTY_txt.Text = ALL_QTY / U_Cargo
                U_ID = c.Dr("U_ID")
                IM_Fetch_last_Pch_Price()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

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

    Private Sub ALL_QTY_txt_TextChanged(sender As Object, e As EventArgs) Handles ALL_QTY_txt.TextChanged  ', NewSaleByOne.TextChanged
        If Not String.IsNullOrWhiteSpace(sender.Text) Then
            Dim N As Double = sender.Text
            sender.Text = N.ToString("N")
        End If
    End Sub


    Private Sub Exit_Btn_Click(sender As Object, e As EventArgs) Handles Exit_Btn.Click
        Me.Close()
    End Sub

End Class