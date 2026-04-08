Public Class Change_IM_Details
    Dim Get_Unit As Boolean = False
    Dim IM_ID As Integer
    Dim U_ID As Integer
    Dim QTY As Double
    Dim T_ID As Integer
    Dim D_Valid As String = ""
    Dim PrevPrice, NewPrice, Min_SP, Prev_Qty As Double
    Dim U_Cargo As Double
    Dim ST_ID As Integer
    Dim AG_ID As Integer = 1
    Dim On_Update As Boolean = False

    Dim Min_SP_2 As Double

    Private Sub Change_IM_Details_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Return Then ConfermButton_Click(sender, e)
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub Change_IM_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        ThemeManager.ApplyThemeToForm(Me)
        Select Case FormType
            Case 1
                If SB_is_Fast = False Then
                    IM_ID = F_Sales.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value
                    U_ID = F_Sales.AGMetroGrid.CurrentRow.Cells("U_ID_CL").Value
                    QTY = Convert.ToDouble(F_Sales.AGMetroGrid.CurrentRow.Cells("QTY_CL").Value)
                    QtyTextBox.Text = F_Sales.AGMetroGrid.CurrentRow.Cells("QTY_CL").Value
                    IM_LB.Text = F_Sales.AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value
                    PrevPrice = F_Sales.AGMetroGrid.CurrentRow.Cells("Price_CL").Value
                    T_ID = F_Sales.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                    D_Valid = F_Sales.AGMetroGrid.CurrentRow.Cells("D_Valid_CL").Value
                    Notes_txt.Text = F_Sales.AGMetroGrid.CurrentRow.Cells("Notes_CL").Value
                    PriceTextBox.Text = F_Sales.AGMetroGrid.CurrentRow.Cells("Price_CL").Value
                    ST_ID = F_Sales.AGMetroGrid.CurrentRow.Cells("ST_ID_CL").Value
                    IM_Disc_txt.Text = F_Sales.AGMetroGrid.CurrentRow.Cells("IM_Discount_CL").Value

                    PriceTextBox.Enabled = U_SB_IM_Update
                 
                    Note_Panel.Visible = True
                    AG_ID = F_Sales.AG_ID
                    On_Update = F_Sales.On_Update
                Else
                    IM_ID = Sales_Fast.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value
                    U_ID = Sales_Fast.AGMetroGrid.CurrentRow.Cells("U_ID_CL").Value
                    QTY = Convert.ToDouble(Sales_Fast.AGMetroGrid.CurrentRow.Cells("QTY_CL").Value)
                    QtyTextBox.Text = Sales_Fast.AGMetroGrid.CurrentRow.Cells("QTY_CL").Value
                    IM_LB.Text = Sales_Fast.AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value
                    PrevPrice = Sales_Fast.AGMetroGrid.CurrentRow.Cells("Price_CL").Value
                    T_ID = Sales_Fast.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                    D_Valid = Sales_Fast.AGMetroGrid.CurrentRow.Cells("D_Valid_CL").Value
                    Notes_txt.Text = Sales_Fast.AGMetroGrid.CurrentRow.Cells("Notes_CL").Value
                    PriceTextBox.Text = Sales_Fast.AGMetroGrid.CurrentRow.Cells("Price_CL").Value
                    ST_ID = Sales_Fast.AGMetroGrid.CurrentRow.Cells("ST_ID_CL").Value
                    IM_Disc_txt.Text = Sales_Fast.AGMetroGrid.CurrentRow.Cells("IM_Discount_CL").Value

                    'Min_SP = IM_Load_Min_SP()

                    PriceTextBox.Enabled = U_SB_IM_Update

                    Note_Panel.Visible = True

                    AG_ID = Sales_Fast.AG_ID
                    On_Update = Sales_Fast.On_Update
                End If

                IM_Load_Min_SP()

                SB_2_Btn.Visible = U_Sell_Under_Min_SP

                SB_3_Btn.Visible = U_Sell_Under_Min_SP_2

                IM_DiscountPanel.Visible = U_SalesDis


            Case 2

                If IM_min_QTY = False Then IM_Unit_cm.Enabled = False
                T_ID = F_Pch.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                IM_ID = F_Pch.AGMetroGrid.CurrentRow.Cells("EX_ID_CL").Value
                U_ID = F_Pch.AGMetroGrid.CurrentRow.Cells("U_ID_CL").Value
                QTY = Convert.ToDouble(F_Pch.AGMetroGrid.CurrentRow.Cells("QTY_CL").Value)
                QtyTextBox.Text = F_Pch.AGMetroGrid.CurrentRow.Cells("QTY_CL").Value
                IM_LB.Text = F_Pch.AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value
                PriceTextBox.Text = F_Pch.AGMetroGrid.CurrentRow.Cells("Price_CL").Value
                Notes_txt.Text = F_Pch.AGMetroGrid.CurrentRow.Cells("Notes_CL").Value
                ST_ID = F_Pch.AGMetroGrid.CurrentRow.Cells("ST_ID_CL").Value
                D_Valid = F_Pch.AGMetroGrid.CurrentRow.Cells("D_Valid_CL").Value
                If Not IsDBNull(F_Pch.AGMetroGrid.CurrentRow.Cells("NewSale_CL").Value) Then NewSalePrice_txt.Text = F_Pch.AGMetroGrid.CurrentRow.Cells("NewSale_CL").Value
                If Not IsDBNull(F_Pch.AGMetroGrid.CurrentRow.Cells("NewSaleByOne_CL").Value) Then NewSaleByOne.Text = F_Pch.AGMetroGrid.CurrentRow.Cells("NewSaleByOne_CL").Value
                PriceTextBox.Enabled = True
                PriceTextBox.ReadOnly = False
                Note_Panel.Visible = True
                Pch_Panel.Visible = True

                If F_Pch.Dist_DV.Rows.Count > 0 Then
                    MsgBox("لا يمكنك تعديل تكلفة او كمية الأصناف لوجود قيمة موزعة على البضاعة ... قم بإلغاء القيمة الموزعة أولا", MsgBoxStyle.Exclamation, "تنويه")
                    QtyTextBox.Enabled = False
                    PriceTextBox.Enabled = False
                End If

            Case 4
                If IM_min_QTY = False Then IM_Unit_cm.Enabled = False
                T_ID = F_Invoice.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value
                IM_ID = F_Invoice.AGMetroGrid.CurrentRow.Cells("EX_ID_CL").Value
                U_ID = F_Invoice.AGMetroGrid.CurrentRow.Cells("U_ID_CL").Value
                QTY = Convert.ToDouble(F_Invoice.AGMetroGrid.CurrentRow.Cells("QTY_CL").Value)
                QtyTextBox.Text = F_Invoice.AGMetroGrid.CurrentRow.Cells("QTY_CL").Value
                IM_LB.Text = F_Invoice.AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value
                PriceTextBox.Text = F_Invoice.AGMetroGrid.CurrentRow.Cells("Price_CL").Value
                Notes_txt.Text = F_Invoice.AGMetroGrid.CurrentRow.Cells("Notes_CL").Value
                ST_ID = F_Invoice.AGMetroGrid.CurrentRow.Cells("ST_ID_CL").Value
                D_Valid = F_Invoice.AGMetroGrid.CurrentRow.Cells("D_Valid_CL").Value
                If Not IsDBNull(F_Invoice.AGMetroGrid.CurrentRow.Cells("NewSale_CL").Value) Then NewSalePrice_txt.Text = F_Invoice.AGMetroGrid.CurrentRow.Cells("NewSale_CL").Value
                If Not IsDBNull(F_Invoice.AGMetroGrid.CurrentRow.Cells("NewSaleByOne_CL").Value) Then NewSaleByOne.Text = F_Invoice.AGMetroGrid.CurrentRow.Cells("NewSaleByOne_CL").Value

                PriceTextBox.Enabled = True
                PriceTextBox.ReadOnly = False
                Note_Panel.Visible = True
                Pch_Panel.Visible = True

            Case 9
                If IM_min_QTY = False Then IM_Unit_cm.Enabled = False
                IM_ID = F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("prc_IM_ID_CL").Value
                U_ID = F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("prc_U_ID_CL").Value
                QTY = Convert.ToDouble(F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("prc_QTY_CL").Value)
                QtyTextBox.Text = F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("prc_QTY_CL").Value
                IM_LB.Text = F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("FRM_IM_NAME_CL").Value
                PriceTextBox.Text = F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("Rtn_Price_CL").Value
                ST_ID = F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("ST_ID_CL").Value
                D_Valid = F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("D_Valid_CL").Value
                If Not IsDBNull(F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("SALE_Price_CL").Value) Then NewSalePrice_txt.Text = F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("SALE_Price_CL").Value
                PriceTextBox.Enabled = False
                PriceTextBox.ReadOnly = True
                Pch_Panel.Visible = True

            Case 10
                If IM_min_QTY = False Then IM_Unit_cm.Enabled = False
                IM_ID = F_ViewBill.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value
                U_ID = F_ViewBill.AGMetroGrid.CurrentRow.Cells("U_ID_CL").Value
                QTY = Convert.ToDouble(F_ViewBill.AGMetroGrid.CurrentRow.Cells("QTY_CL").Value)
                QtyTextBox.Text = F_ViewBill.AGMetroGrid.CurrentRow.Cells("QTY_CL").Value
                IM_LB.Text = F_ViewBill.AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value
                PriceTextBox.Text = F_ViewBill.AGMetroGrid.CurrentRow.Cells("Price_CL").Value
                Notes_txt.Text = F_ViewBill.AGMetroGrid.CurrentRow.Cells("Notes_CL").Value
                ST_ID = F_ViewBill.AGMetroGrid.CurrentRow.Cells("ST_ID_CL").Value



        End Select
        Fetch_IM_Units()
        IM_Fetch_QTY(False)
        SELECT_Prev_Qty()
        QtyTextBox.Select()




    End Sub

    Private Sub IM_Load_Min_SP()
        Dim c As New C
        Try
            Dim s As String
            s = "select ISNULL(Min_SP,0) AS Min_SP , ISNULL(Min_SP_2,0) AS Min_SP_2 from IM_Menu_Units_V WHERE IM_ID = '" & IM_ID & "' AND U_ID = '" & U_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Min_SP = c.Dr("Min_SP")
                Min_SP_2 = c.Dr("Min_SP_2")

                SB_2_Btn.Tag = c.Dr("Min_SP")
                SB_2_Btn.Text = SB_2_Btn.Text & " " & c.Dr("Min_SP")
                SB_3_Btn.Tag = c.Dr("Min_SP_2")
                SB_3_Btn.Text = SB_3_Btn.Text & " " & c.Dr("Min_SP_2")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Fetch_IM_Units()

        Dim c As New C
        Try
            Dim s As String
            s = "select U_ID,U_Name from IM_Menu_Units_V  WHERE IM_ID = '" & IM_ID & "' Order By U_Cargo Asc"
            's = "select U_ID,U_Name from IM_Menu_Units_V  WHERE IM_ID = '" & IM_ID & "' UNION select U_ID,U_Name from Units  WHERE U_ID = " & U_ID
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            IM_Unit_cm.DataSource = c.Dt
            IM_Unit_cm.DisplayMember = "U_Name"
            IM_Unit_cm.ValueMember = "U_ID"
            IM_Unit_cm.SelectedValue = U_ID
            Get_Unit = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub IM_Fetch_QTY(ChangePrice As Boolean)
        Dim c As New C
        Try
            Dim s As String
            s = "select U_Cargo,Price from IM_Menu_Units_V WHERE U_ID = '" & IM_Unit_cm.SelectedValue & "' AND IM_ID = '" & IM_ID & "'"
            's = "select U_Cargo,Price from IM_Menu_Units_V WHERE U_ID = '" & IM_Unit_cm.SelectedValue & "' AND IM_ID = '" & IM_ID & "' UNION select U_ID,U_Name from Units  WHERE U_ID = " & U_ID
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Dim N As Double = (QTY / c.Dr("U_Cargo"))
                If ChangePrice = True And FormType = 1 Then PriceTextBox.Text = c.Dr("Price")
                U_Cargo = c.Dr("U_Cargo")
                If U_Cargo > 1 And FormType <> 1 Then
                    Newsale_ByOne_Panel.Visible = True
                Else
                    Newsale_ByOne_Panel.Visible = False
                End If

            End If
            c.Con.Close()

            c = New C
            c.Str = "select ISNULL(SUM(QTY),0) AS QTY from ST_Balance_V WHERE IM_ID = '" & IM_ID & "'"
            c.Com = New SqlClient.SqlCommand(c.Str, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Current_QTY.Text = (c.Dr("QTY") / U_Cargo)
            End If
            c.Con.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SELECT_Prev_Qty()
        Prev_Qty = U_Cargo * Convert.ToDouble(QtyTextBox.Text)
    End Sub

    Private Sub IM_Unit_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles IM_Unit_cm.SelectedValueChanged
        If Get_Unit = True Then IM_Fetch_QTY(True)
    End Sub

    Private Sub ConfermButton_Click(sender As Object, e As EventArgs) Handles ConfermButton.Click
        If String.IsNullOrWhiteSpace(QtyTextBox.Text) Or Convert.ToDouble(QtyTextBox.Text) = 0 Then
            MsgBox("حدد الكمية", MsgBoxStyle.Exclamation)
            QtyTextBox.Select()
            Exit Sub
        End If

        Select Case FormType
            Case 1

                'If On_Update = True Then
                '    If (Convert.ToDouble(QtyTextBox.Text) > Prev_Qty) Or Convert.ToDouble(QtyTextBox.Text) > PrevPrice Then
                '        If AG_ID > 1 Then

                '            If U_AG_Skip_Max = False Then
                '                If CHECK_IF_AGENT_SKIP_MAX_DEBIT(AG_ID) = 1 Then
                '                    MsgBox("عذرا ... هذا الزبون قد تخطى سقف الدين الخاص به ولا يمكنك إضافة المزيد من الأصناف عليه", MsgBoxStyle.Critical, "خطأ فالإدراج")
                '                    Exit Sub
                '                End If
                '            Else
                '                If CHECK_IF_AGENT_SKIP_MAX_DEBIT(AG_ID) = 1 Then MsgBox("هذا الزبون قد تخطى سقف الدين الخاص به", MsgBoxStyle.Exclamation, "تنويه ")
                '            End If

                '        End If
                '    End If
                'End If

                If S_Allow_MinSP = True Then
                    'If User_isAdmin = False Then

                    '    If U_Sell_Under_Min_SP = True Then ' And IM_is_Min_SB(1) = True
                    '        If Convert.ToDouble(PriceTextBox.Text) < Min_SP And Min_SP > 0 Then
                    '            MsgBox(" ( " + Min_SP.ToString + " ) لا يمكنك البيع بأقل من سعر الجملة", MsgBoxStyle.Exclamation)
                    '            Exit Sub
                    '        End If

                    '    End If

                    '    If U_Sell_Under_Min_SP_2 = True Then 'And IM_is_Min_SB(2) = True
                    '        If Convert.ToDouble(PriceTextBox.Text) < Min_SP_2 And Min_SP_2 > 0 Then
                    '            MsgBox(" ( " + Min_SP_2.ToString + " ) لا يمكنك البيع بأقل من  سعر الجملة", MsgBoxStyle.Exclamation)
                    '            Exit Sub
                    '        End If

                    '    End If

                    'Else

                    If Convert.ToDouble(PriceTextBox.Text) < Min_SP And Min_SP > 0 Then
                            If MessageBox.Show(" ( " + Min_SP.ToString + " ) سوف يتم البيع بأقل من  سعر الجملة", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, _
                                               MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then Exit Sub

                            If Convert.ToDouble(PriceTextBox.Text) < Min_SP_2 And Min_SP_2 > 0 Then
                                If MessageBox.Show(" ( " + Min_SP_2.ToString + " ) سوف يتم البيع بأقل من  سعر جملة الجملة", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, _
                                                   MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then Exit Sub
                            End If

                        End If

                    'If Convert.ToDouble(PriceTextBox.Text) < Min_SP And Min_SP > 0 Then
                    '    If MessageBox.Show(" ( " + Min_SP.ToString + " ) سوف يتم البيع بأقل من سعر الجملة", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, _
                    '                       MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then Exit Sub
                    'End If
                    '   End If
                End If

                If SB_is_Fast = False Then
                    If U_SB_Sell_Under_Cost = False Then
                        If Show_IM_Cost(False, F_Sales.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value, IM_Unit_cm.SelectedValue) > PriceTextBox.Text Then
                            MsgBox("لا يمكنك البيع بأقل من سعر التكلفة", MsgBoxStyle.Critical)
                            Exit Sub
                        End If
                    Else
                        If Show_IM_Cost(False, F_Sales.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value, IM_Unit_cm.SelectedValue) > PriceTextBox.Text Then
                            If MessageBox.Show(" سوف يتم البيع بأقل من سعر التكلفة", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, _
                                                   MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then Exit Sub
                        End If
                    End If
                Else
                    If U_SB_Sell_Under_Cost = False Then
                        If Show_IM_Cost(False, Sales_Fast.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value, IM_Unit_cm.SelectedValue) > PriceTextBox.Text Then
                            MsgBox("لا يمكنك البيع بأقل من سعر التكلفة", MsgBoxStyle.Critical)
                            Exit Sub
                        End If
                    Else
                        If Show_IM_Cost(False, Sales_Fast.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value, IM_Unit_cm.SelectedValue) > PriceTextBox.Text Then
                            If MessageBox.Show(" سوف يتم البيع بأقل من سعر التكلفة", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, _
                                                   MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then Exit Sub
                        End If
                    End If
                End If



                If IM_min_QTY = False Then
                    If IM_Check_Neg_QTY_() = 1 Then
                        MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                End If

                If is_PercentCB.Checked = True Then
                    Notes_txt.Text = Notes_txt.Text & "/ خصم " & IM_Disc_txt.Text & " % "
                    IM_Disc_txt.Text = Convert.ToDouble(PriceTextBox.Text) * (Convert.ToDouble(IM_Disc_txt.Text) / 100)
                End If


                If SB_is_Fast = False Then
                    SB_Contents_Confirm_Unit()
                Else
                    SB_Contents_Confirm_Unit_fast()
                End If

            Case 2
                If IM_min_QTY = False Then
                    If IM_Check_Pch_Def_QTY_() = 1 Then
                        MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                End If
                Pch_Contents_Confirm_Unit()
            Case 4
                If IM_min_QTY = False Then
                    If IM_Check_Invoice_Def_QTY_() = 1 Then
                        MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
                        Exit Sub
                    End If
                End If
                Invoice_Contents_Confirm_Unit()
            Case 9
                Frm_Manual_Confirm_Unit()
            Case 10
                View_Contents_IM_Unit_Change()
        End Select

    End Sub

    'Private Function IM_is_Min_SB(TYPE As Integer)
    '    Dim c2 As New C
    '    Try
    '        Dim s As String
    '        If TYPE = 1 Then
    '            s = "SELECT is_By_Min_SP AS T FROM SB_Contents WHERE T_ID ='" & T_ID & "'"
    '        Else
    '            s = "SELECT is_By_Min_SP_2 AS T FROM SB_Contents WHERE T_ID ='" & T_ID & "'"
    '        End If

    '        c2.Com = New SqlClient.SqlCommand(s, c2.Con)
    '        c2.Con.Open()
    '        c2.Dr = c2.Com.ExecuteReader
    '        If c2.Dr.HasRows Then
    '            c2.Dr.Read()
    '            Return c2.Dr("T")
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    '    Return 0
    'End Function



    Private Sub View_Contents_IM_Unit_Change()

        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[View_Contents_IM_Unit_Change]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_T_ID", F_ViewBill.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
            .Parameters.AddWithValue("@U_ID", IM_Unit_cm.SelectedValue)
            .Parameters.AddWithValue("@QTY", QtyTextBox.Text)
            .Parameters.AddWithValue("@Price", PriceTextBox.Text)


        End With
        If SQL_SP_EXEC(C.Com) Then
            Network_Edit_Tracker_insert(" الصنف:" + IM_LB.Text + " الوحدة:" + IM_Unit_cm.Text + " العدد:" + QtyTextBox.Text + " السعر:" + PriceTextBox.Text, F_ViewBill.Bill_ID_Txt.Text, 16, 3)
            F_ViewBill.ViewBill_Contents_SELECT_Bill()
            'F_ViewBill.Update_Total()
            Me.Close()
        End If

    End Sub

    Public Function IM_Check_Pch_Def_QTY_()

        Dim F As Integer = 0

        Dim c2 As New C
        Try
            Dim s As String
            s = "SELECT isStore FROM IM_Menu WHERE IM_ID ='" & IM_ID & "'"
            c2.Com = New SqlClient.SqlCommand(s, c2.Con)
            c2.Con.Open()
            c2.Dr = c2.Com.ExecuteReader
            If c2.Dr.HasRows Then
                c2.Dr.Read()
                If c2.Dr("isStore") = 1 Or c2.Dr("isStore") = 2 Then
                    GoTo CHECK
                Else
                    Return F
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

CHECK:
        If Prev_Qty > (Convert.ToDouble(QtyTextBox.Text) * U_Cargo) Then
            Dim c As New C
            Try
                Dim s As String
                s = "select ISNULL(SUM(QTY),0) AS QTY from ST_Balance_V WHERE IM_ID = '" & IM_ID & "' AND ST_ID = '" & ST_ID & "' AND D_Valid = '" & D_Valid & "'"
                c.Com = New SqlClient.SqlCommand(s, c.Con)
                c.Con.Open()
                c.Dr = c.Com.ExecuteReader
                If c.Dr.HasRows Then
                    c.Dr.Read()
                    If c.Dr("QTY") - (Prev_Qty - (Convert.ToDouble(QtyTextBox.Text) * U_Cargo)) < 0 Then
                        F = 1
                        Return F
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If


        Return F
    End Function

    Public Function IM_Check_Invoice_Def_QTY_()
        Dim F As Integer = 0


        Dim c2 As New C
        Try
            Dim s As String
            s = "SELECT isStore FROM IM_Menu WHERE IM_ID ='" & IM_ID & "'"
            c2.Com = New SqlClient.SqlCommand(s, c2.Con)
            c2.Con.Open()
            c2.Dr = c2.Com.ExecuteReader
            If c2.Dr.HasRows Then
                c2.Dr.Read()
                If c2.Dr("isStore") = 1 Or c2.Dr("isStore") = 2 Then
                    GoTo CHECK
                Else
                    Return F
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

CHECK:


        If Prev_Qty > (Convert.ToDouble(QtyTextBox.Text) * U_Cargo) Then
            Dim c As New C

            Try
                Dim s As String
                s = "select ISNULL(SUM(QTY),0) AS QTY from ST_Balance_V WHERE IM_ID = '" & IM_ID & "' AND ST_ID = '" & ST_ID & "' AND D_Valid = '" & D_Valid & "'"
                c.Com = New SqlClient.SqlCommand(s, c.Con)
                c.Con.Open()
                c.Dr = c.Com.ExecuteReader
                If c.Dr.HasRows Then
                    c.Dr.Read()
                    If c.Dr("QTY") - (Prev_Qty - (Convert.ToDouble(QtyTextBox.Text) * U_Cargo)) < 0 Then
                        F = 1
                        Return F
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If


        Return F
    End Function

    Private Function IM_Check_Neg_QTY_()
        Dim C As New C
        Dim DEF As Double = (Convert.ToDouble(QtyTextBox.Text) * U_Cargo) - Prev_Qty
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_2"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@T_ID", T_ID)
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters.AddWithValue("@D_Vaild", D_Valid)
            .Parameters.AddWithValue("@Enterd_Qty", DEF)
            .Parameters.AddWithValue("@U_ID", IM_Unit_cm.SelectedValue)

            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                F = .Parameters("@F").Value
            End If
        End With

        Return F
    End Function

    Private Sub SB_Contents_Confirm_Unit()
        Dim Row_Index As Integer = 0
        If F_Sales.AGMetroGrid.Rows.Count > 0 Then Row_Index = F_Sales.AGMetroGrid.CurrentCell.RowIndex
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_IM_Unit_Change"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@SB_T_ID", F_Sales.T_ID)
            .Parameters.AddWithValue("@IM_T_ID", F_Sales.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
            .Parameters.AddWithValue("@U_ID", IM_Unit_cm.SelectedValue)
            .Parameters.AddWithValue("@QTY", QtyTextBox.Text)
            .Parameters.AddWithValue("@Price", PriceTextBox.Text)
            .Parameters.AddWithValue("@Notes", Notes_txt.Text)
            .Parameters.AddWithValue("@On_Update", F_Sales.On_Update)
            .Parameters.AddWithValue("@IM_Discount", IM_Disc_txt.Text)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        F_Sales.AGMetroGrid.DataSource = C.Dt
        NewPrice = PriceTextBox.Text
        'F_Sales.Calc_Total()

        'If PrevPrice <> NewPrice Then
        'Network_Edit_Tracker_insert("تعديل سعر بيع صنف '" + IM_LB.Text + "' / رقم آلي '" + F_Sales.Bill_ID_Txt.Text + " / رقم يومي : " + _
        '                 F_Sales.BillNumTxt.Text + "' من '" + PrevPrice.ToString + "' إلى '" + NewPrice.ToString + "' / المدخل :  " + F_Sales.User_Name_lb.Text, F_Sales.Pure_txt.Text, _
        '                 F_Sales.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value, 0)
        ''End If

        Network_Edit_Tracker_insert(" الصنف:" + IM_LB.Text + " الوحدة:" + IM_Unit_cm.Text + " العدد:" + QtyTextBox.Text + " السعر:" + PriceTextBox.Text + " الخصم:" + IM_Disc_txt.Text, F_Sales.Bill_ID_Txt.Text, 1, 3)


        ' If F_Sales.On_Update = True Then F_Sales.AG_Balance_Update_Pied()
        If Row_Index > 0 Then F_Sales.AGMetroGrid.CurrentCell = F_Sales.AGMetroGrid.Rows(Row_Index).Cells("EX_Name_CL")
        Me.Close()
    End Sub

    Private Sub SB_Contents_Confirm_Unit_fast()
        Dim Row_Index As Integer = 0
        If Sales_Fast.AGMetroGrid.Rows.Count > 0 Then Row_Index = Sales_Fast.AGMetroGrid.CurrentCell.RowIndex
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_IM_Unit_Change"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@SB_T_ID", Sales_Fast.T_ID)
            .Parameters.AddWithValue("@IM_T_ID", Sales_Fast.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
            .Parameters.AddWithValue("@U_ID", IM_Unit_cm.SelectedValue)
            .Parameters.AddWithValue("@QTY", QtyTextBox.Text)
            .Parameters.AddWithValue("@Price", PriceTextBox.Text)
            .Parameters.AddWithValue("@Notes", Notes_txt.Text)
            .Parameters.AddWithValue("@On_Update", Sales_Fast.On_Update)
            .Parameters.AddWithValue("@IM_Discount", IM_Disc_txt.Text)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        Sales_Fast.AGMetroGrid.DataSource = C.Dt
        NewPrice = PriceTextBox.Text
        'Sales_Fast.Calc_Total()
        'If PrevPrice <> NewPrice Then
        'Network_Edit_Tracker_insert("تعديل سعر بيع صنف '" + IM_LB.Text + "' / رقم آلي '" + Sales_Fast.Bill_ID_Txt.Text + "' من '" + PrevPrice.ToString + "' إلى '" + NewPrice.ToString + "' / المدخل :  " + Sales_Fast.User_Name_lb.Text, Sales_Fast.Pure_txt.Text, _
        '                 Sales_Fast.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value, 0)
        'End If

        Network_Edit_Tracker_insert(" الصنف:" + IM_LB.Text + " الوحدة:" + IM_Unit_cm.Text + " العدد:" + QtyTextBox.Text + " السعر:" + PriceTextBox.Text + " الخصم:" + IM_Disc_txt.Text, Sales_Fast.Bill_ID_Txt.Text, 1, 3)


        If Row_Index > 0 Then Sales_Fast.AGMetroGrid.CurrentCell = Sales_Fast.AGMetroGrid.Rows(Row_Index).Cells("EX_Name_CL")
        Me.Close()
    End Sub

    Private Sub Pch_Contents_Confirm_Unit()
        Try

            Dim Row_Index As Integer = 0
            If F_Pch.AGMetroGrid.Rows.Count > 0 Then Row_Index = F_Pch.AGMetroGrid.CurrentCell.RowIndex
            Dim C As New C
            With C.Com
                .Connection = C.Con
                .CommandText = "Pch_IM_Unit_Change"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@Pch_T_ID", F_Pch.T_ID)
                .Parameters.AddWithValue("@IM_T_ID", F_Pch.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
                .Parameters.AddWithValue("@U_ID", IM_Unit_cm.SelectedValue)
                .Parameters.AddWithValue("@QTY", QtyTextBox.Text)
                If Not String.IsNullOrWhiteSpace(PriceTextBox.Text) Then .Parameters.AddWithValue("@Price", Convert.ToDouble(PriceTextBox.Text))
                If String.IsNullOrWhiteSpace(NewSalePrice_txt.Text) = False Then .Parameters.AddWithValue("@NewSale", Convert.ToDouble(NewSalePrice_txt.Text))
                If String.IsNullOrWhiteSpace(NewSaleByOne.Text) = False Then .Parameters.AddWithValue("@NewSaleByOne", Convert.ToDouble(NewSaleByOne.Text))
                .Parameters.AddWithValue("@Notes", Notes_txt.Text)
                .Parameters.AddWithValue("@On_Update", F_Pch.On_Update)
            End With
            C.Da = New SqlClient.SqlDataAdapter(C.Com)
            C.Da.Fill(C.Dt)
            F_Pch.AGMetroGrid.DataSource = C.Dt
            If Row_Index > 0 Then F_Pch.AGMetroGrid.CurrentCell = F_Pch.AGMetroGrid.Rows(Row_Index).Cells("EX_Name_CL")


            'Network_Edit_Tracker_insert("تعديل بيانات صنف '" + IM_LB.Text + "' / رقم آلي '" + F_Pch.Bill_ID_Txt.Text + " / رقم يومي : " + _
            '     F_Pch.Bill_ID_Txt.Text + "' من '" + PrevPrice.ToString + "' إلى '" + NewPrice.ToString + "' / المدخل :  " + F_Pch.User_Name_lb.Text, F_Pch.Pure_txt.Text, _
            '     F_Pch.AGMetroGrid.CurrentRow.Cells("Bill_IMID_CL").Value, 0)

            Network_Edit_Tracker_insert(" الصنف:" + IM_LB.Text + " الوحدة:" + IM_Unit_cm.Text + " العدد:" + QtyTextBox.Text + " الشراء:" + PriceTextBox.Text + " البيع:" + NewSalePrice_txt.Text _
                                        + " بيع القطعة:" + NewSaleByOne.Text, F_Pch.Bill_ID_Txt.Text, 7, 3)
            F_Pch.Pch_Contents_SELECT_Bill()

            Me.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub Frm_Manual_Confirm_Unit()

        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[Frm_Manual_IM_Unit_Change]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_T_ID", F_Format_Items_Manual.AGMetroGrid.CurrentRow.Cells("DETAILS_T_ID_CL").Value)
            .Parameters.AddWithValue("@U_ID", IM_Unit_cm.SelectedValue)
            .Parameters.AddWithValue("@QTY", QtyTextBox.Text)
            If Not String.IsNullOrWhiteSpace(PriceTextBox.Text) Then .Parameters.AddWithValue("@Price", PriceTextBox.Text)
            If String.IsNullOrWhiteSpace(NewSalePrice_txt.Text) = False Then .Parameters.AddWithValue("@NewSale", NewSalePrice_txt.Text)
            If String.IsNullOrWhiteSpace(NewSaleByOne.Text) = False Then .Parameters.AddWithValue("@NewSaleByOne", NewSaleByOne.Text)
            ' .Parameters.AddWithValue("@Notes", Notes_txt.Text)
        End With
        If SQL_SP_EXEC(C.Com) Then

            Network_Edit_Tracker_insert(" الصنف:" + IM_LB.Text + " الوحدة:" + IM_Unit_cm.Text + " العدد:" + QtyTextBox.Text + " التكلفة:" + PriceTextBox.Text + " البيع:" + PriceTextBox.Text + " بيع القطعة:" + NewSaleByOne.Text, F_Format_Items_Manual.Bill_ID_Txt.Text, 18, 3)
            F_Format_Items_Manual.Pch_Contents_SELECT_Bill()
        End If

        Me.Close()
    End Sub


    Private Sub Invoice_Contents_Confirm_Unit()
        Try
            Dim Row_Index As Integer = 0
            If F_Invoice.AGMetroGrid.Rows.Count > 0 Then Row_Index = F_Invoice.AGMetroGrid.CurrentCell.RowIndex
            Dim C As New C
            With C.Com
                .Connection = C.Con
                .CommandText = "Pch_IM_Unit_Change"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@Pch_T_ID", F_Invoice.T_ID)
                .Parameters.AddWithValue("@IM_T_ID", F_Invoice.AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
                .Parameters.AddWithValue("@U_ID", IM_Unit_cm.SelectedValue)
                .Parameters.AddWithValue("@QTY", QtyTextBox.Text)
                If Not String.IsNullOrWhiteSpace(PriceTextBox.Text) Then .Parameters.AddWithValue("@Price", PriceTextBox.Text)
                If String.IsNullOrWhiteSpace(NewSalePrice_txt.Text) = False Then .Parameters.AddWithValue("@NewSale", NewSalePrice_txt.Text)
                If String.IsNullOrWhiteSpace(NewSaleByOne.Text) = False Then .Parameters.AddWithValue("@NewSaleByOne", NewSaleByOne.Text)
                .Parameters.AddWithValue("@Notes", Notes_txt.Text)
                .Parameters.AddWithValue("@On_Update", F_Invoice.On_Update)
            End With
            F_Invoice.Bill_DT.Clear()
            C.Da = New SqlClient.SqlDataAdapter(C.Com)
            C.Da.Fill(F_Invoice.Bill_DT)
            F_Invoice.AGMetroGrid.DataSource = F_Invoice.Bill_DT

            Network_Edit_Tracker_insert(" الصنف:" + IM_LB.Text + " الوحدة:" + IM_Unit_cm.Text + " العدد:" + QtyTextBox.Text + " الشراء:" + PriceTextBox.Text + " البيع:" + NewSalePrice_txt.Text _
                                       + " بيع القطعة:" + NewSaleByOne.Text, F_Invoice.Bill_ID_Txt.Text, 9, 3)

            If Row_Index > 0 Then F_Invoice.AGMetroGrid.CurrentCell = F_Invoice.AGMetroGrid.Rows(Row_Index).Cells("EX_Name_CL")
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
       
    End Sub

    Private Sub QtyTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles QtyTextBox.KeyDown
        Select Case e.KeyCode
            Case Keys.Right
                If PriceTextBox.Enabled = True Then
                    PriceTextBox.Select()
                Else
                    IM_Unit_cm.DroppedDown = True
                    IM_Unit_cm.Select()
                End If
        End Select
    End Sub

    Private Sub QtyTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QtyTextBox.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub QtyTextBox_TextChanged(sender As Object, e As EventArgs) Handles QtyTextBox.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub PriceTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles PriceTextBox.KeyDown
        Select Case e.KeyCode
            Case Keys.Right
                IM_Unit_cm.DroppedDown = True
                IM_Unit_cm.Select()
            Case Keys.Left : QtyTextBox.Select()
        End Select
    End Sub

    Private Sub IM_Unit_cm_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_Unit_cm.KeyDown
        Select Case e.KeyCode
            Case Keys.Left, Keys.Left
                If PriceTextBox.Enabled = True Then
                    PriceTextBox.Select()
                Else
                    QtyTextBox.Select()
                End If
        End Select
    End Sub

    Private Sub PriceTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PriceTextBox.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub PriceTextBox_TextChanged(sender As Object, e As EventArgs) Handles PriceTextBox.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Change_IM_Details_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub NewSalePrice_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles NewSalePrice_txt.KeyPress, NewSaleByOne.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub is_PercentCB_CheckedChanged(sender As Object, e As EventArgs) Handles is_PercentCB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub SB_2_Btn_Click(sender As Object, e As EventArgs) Handles SB_2_Btn.Click
        If SB_2_Btn.Tag > 0 Then PriceTextBox.Text = SB_2_Btn.Tag
    End Sub

    Private Sub SB_3_Btn_Click(sender As Object, e As EventArgs) Handles SB_3_Btn.Click
        If SB_3_Btn.Tag > 0 Then PriceTextBox.Text = SB_3_Btn.Tag
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub NewSalePrice_txt_TextChanged(sender As Object, e As EventArgs) Handles NewSalePrice_txt.TextChanged, NewSaleByOne.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub IM_CalcAvgCost_btn_Click(sender As Object, e As EventArgs) Handles IM_CalcAvgCost_btn.Click
        If Current_QTY.Text.Count > 0 Then If Convert.ToDouble(Current_QTY.Text) > 0 And PriceTextBox.Text.Count > 0 Then IM_Calc_Avg(True)
    End Sub

    Public Function IM_Calc_Avg(isMsg As Boolean)
        Dim c As New C
        Dim Prev_Cost As Double = 0
        Dim Prev_Qty As Double = 0


        Try
            Dim s As String
            '   s = "select Cost from IM_All_V WHERE IM_ID = '" & IM_ID & "'"
            s = "SELECT TOP 1 AVG_COST FROM Pch_Details WHERE IM_ID = '" & IM_ID & "' AND isDepended = 1 AND AVG_COST IS NOT NULL AND T_ID <> '" & T_ID & "' ORDER BY T_ID DESC"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Prev_Cost = c.Dr("AVG_COST") * U_Cargo
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
                Prev_Qty = (c.Dr("QTY") / U_Cargo) - (U_Cargo * QTY)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        Dim AVG_COST As Double = ((Prev_Cost * Prev_Qty) + _
                                  ((Convert.ToDouble(PriceTextBox.Text) / U_Cargo) * (Convert.ToDouble(QtyTextBox.Text) * U_Cargo))) _
                              / (Prev_Qty + (Convert.ToDouble(QtyTextBox.Text)))
        ' ----------------------------------------------------------------

        'Dim AVG_COST As Double
        'Dim c As New C
        'Try
        '    Dim s As String
        '    s = "SELECT TOP 1 AVG_COST FROM Pch_Details WHERE IM_ID = '" & IM_ID & "' AND isDepended = 1 AND AVG_COST IS NOT NULL ORDER BY T_ID DESC"
        '    C.Com = New SqlClient.SqlCommand(s, C.Con)
        '    C.Con.Open()
        '    C.Dr = C.Com.ExecuteReader
        '    If C.Dr.HasRows Then
        '        C.Dr.Read()
        '        AVG_COST = c.Dr("AVG_COST") * U_Cargo
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

        If isMsg = True Then MsgBox((AVG_COST).ToString("00.00"), MsgBoxStyle.Information, "إجمالي التكلفــة")

        Return AVG_COST
    End Function

End Class