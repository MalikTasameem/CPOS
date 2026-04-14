Public Class Receipt

    Dim rs As New Resizer
    Dim Reciept_T_ID As Integer
    Dim Is_ComandSuccess As Boolean = False
    Dim EditState As String = ""
    Public AG_ID As Integer = 0
    Dim IM_Dt As New DataTable
    Dim On_Update As Boolean = False

    Public Clear_Tran_ID As Boolean = True

    Dim ReceiptNum As Integer = 0
    'Dim Tr_B As Double = 0
    Private Sub new_butt_Click(sender As Object, e As EventArgs) Handles new_butt.Click
        Make_New_Receipt()
    End Sub

    Dim KEY_HANDLER As String
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Const CS_DROPSHADOW As Integer = &H20000
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property

    Private Sub Make_New_Receipt()
        AG_Cm.Enabled = True
        Fields_Panel.Enabled = True
        ClearFields()
        save_butt.Enabled = True
        print_butt.Enabled = False
        Void_Lb.Visible = False
        Edit_butt.Enabled = False
        Edit_butt.Text = "تعديـل F3"
        Edit_butt.BackColor = Color.White
        DeleteButton.Enabled = False
        Me.BackColor = SystemColors.Control
        Get_MAX_T_ID()
        AG_Cm.Focus()
    End Sub

    Private Sub After_Save_Receipt()
        Fields_Panel.Enabled = False
        save_butt.Enabled = False
        print_butt.Enabled = True
        Edit_butt.Enabled = True
        DeleteButton.Enabled = True
    End Sub


    Public Sub ClearFields()
        For Each a As Control In Fields_Panel.Controls
            If TypeOf a Is TextBox Then
                a.Text = Nothing
            End If
        Next

        AG_Cm.Textt = ""
        Current_QTY.Text = Nothing
        payment_Type_combo.SelectedIndex = 0
        Receipt_Title_combobox.Clear()
        Receipt_Title_combobox.Text = ""
        bankName_Combo.SelectedIndex = -1
        DateTimeReceipt.Text = Date.Now
        On_Update = False
        ReceiptTypeComboBox.SelectedValue = AG_Type
        Receipt_Tran_ID = 0
        Treasury_ComboBox.SelectedValue = Rct_Tr_ID
        Treasury_Balance.Text = Show_TR_T_Balance(Treasury_ComboBox.SelectedValue)
    End Sub

    Private Sub Load_Data()

        Dim C As New C
        Try
            Dim sql As String = "Select id , Type_Name from AgentBalance_Type Order By id ASC"
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            ReceiptTypeComboBox.DataSource = C.Dt
            ReceiptTypeComboBox.DisplayMember = "Type_Name"
            ReceiptTypeComboBox.ValueMember = "id"
            ReceiptTypeComboBox.SelectedValue = AG_Type
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        C = New C
        Try
            Dim sql As String = "Select Distinct Tr_ID,Tr_Name from TR_MENU_V "
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            Treasury_ComboBox.DataSource = C.Dt
            Treasury_ComboBox.DisplayMember = "Tr_Name"
            Treasury_ComboBox.ValueMember = "Tr_ID"
            Treasury_ComboBox.SelectedValue = Rct_Tr_ID
            If Rct_Tr_Can_change = False Then Treasury_ComboBox.Enabled = False
            Treasury_ComboBox.Text = Show_TR_T_Balance(Treasury_ComboBox.SelectedValue)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        C = New C
        Try
            Dim sql As String = "Select Distinct Bank_Name from Treasury_Balance_MV "
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            bankName_Combo.DataSource = C.Dt
            bankName_Combo.DisplayMember = "Bank_Name"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        C = New C
        Try
            Dim sql As String = "Select P_ID,PAYMENT_NAME AS PAYMENT_NAME from PAYMENT_METHOD "
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            If C.Dt.Rows.Count > 0 Then
                payment_Type_combo.DataSource = C.Dt
                payment_Type_combo.DisplayMember = "PAYMENT_NAME"
                payment_Type_combo.ValueMember = "P_ID"
                payment_Type_combo.SelectedIndex = 0
            Else
                sql = "Select '1' AS P_ID,'نقدا' AS PAYMENT_NAME "
                C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
                C.Da.Fill(C.Dt)
                payment_Type_combo.DataSource = C.Dt
                payment_Type_combo.DisplayMember = "PAYMENT_NAME"
                payment_Type_combo.ValueMember = "P_ID"
                payment_Type_combo.SelectedIndex = 0
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        If Application.OpenForms().OfType(Of Sales).Any Then
            If F_Sales.IS_Show_Rctp = True Then

                Select_Receipt(F_Sales.ReceiptsMetroGrid.CurrentRow.Cells("Receipt_T_ID_CL").Value)
                Exit Sub
            End If
        End If


        If FormType = 0 Then
            '--------------------------------------------------------------------------------------------
        ElseIf FormType = 1 Then
            If isOrder = True Then
                new_butt.Enabled = False
                save_butt.Enabled = True
                DeleteButton.Enabled = False
                Rct_Move_Panel.Enabled = False
            Else

                If Application.OpenForms().OfType(Of Sales).Any = True Then
                    If F_Sales.isDepended = True Then
                        With F_Sales
                            money_num_txtb.Select()
                            new_butt.Enabled = False
                            save_butt.Enabled = True
                            DeleteButton.Enabled = False
                            Rct_Move_Panel.Enabled = False
                        End With
                    End If

                Else
                    If Sales_Fast.isDepended = True Then
                        money_num_txtb.Select()
                        new_butt.Enabled = False
                        save_butt.Enabled = True
                        DeleteButton.Enabled = False
                        Rct_Move_Panel.Enabled = False
                    End If
                End If

            End If
            '--------------------------------------------------------------------------------------------
        ElseIf FormType = 2 Then
            With F_Pch
                If .isShowingDetails = True Then
                    Select_Receipt(.ReceiptsMetroGrid.CurrentRow.Cells("Receipt_T_ID_CL").Value)
                    Exit Sub
                Else
                    money_num_txtb.Select()
                    new_butt.Enabled = False
                    save_butt.Enabled = True
                    If .T_Other_Cr_TXT.Visible = True Then money_num_txtb.Clear()

                    DeleteButton.Enabled = False
                    Rct_Move_Panel.Enabled = False

                End If
            End With

        ElseIf FormType = 8 Then
            With F_EXP_Details
                If .isShowingDetails = True Then
                    Select_Receipt(.ReceiptsMetroGrid.CurrentRow.Cells("Receipt_T_ID_CL").Value)
                    Exit Sub
                Else
                    new_butt.Enabled = False
                    save_butt.Enabled = True
                    money_num_txtb.Select()
                    DeleteButton.Enabled = False
                    Rct_Move_Panel.Enabled = False

                End If
            End With

        ElseIf FormType = 12 Then
            With F_Custody
                new_butt.Enabled = False
                save_butt.Enabled = True
                money_num_txtb.Select()
                DeleteButton.Enabled = False
                Rct_Move_Panel.Enabled = False
            End With

        End If


        If isShowing_Trans = True Then Select_Receipt(T_ID_Trans)


    End Sub


    Public Sub Select_Receipt(T_ID As Integer)
        Dim C As New C
        Dim S As String = "Select * From Receipts_V Where T_ID = '" & T_ID & "'"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                With Me
                    Reciept_T_ID = T_ID
                    Receipt_Tran_ID = C.Dr("Receipt_Tran_ID")
                    ReceiptNum_Txt.Text = S_Sub_Code & (C.Dr("Receipt_Num")) ' - START_ID).ToString
                    ReceiptNum = C.Dr("Receipt_Num")
                    AG_Cm.Set_IM_By_ID(C.Dr("AG_ID"))

                    .ReceiptTypeComboBox.SelectedValue = C.Dr("BsType_ID")
                    .DateTimeReceipt.Text = C.Dr("Date")
                    .Receipt_Title_combobox.Text = C.Dr("Receipt_Title")
                    .Notes_txtb.Text = C.Dr("About")
                    '.Treasury_ComboBox.SelectedValue = C.Dr("Tr_ID")
                    '.payment_Type_combo.SelectedValue = C.Dr("Pay_ID")

                    If IsDBNull(C.Dr("Bank_Name")) = True Then
                        .payment_Type_combo.SelectedIndex = 0
                    Else
                        If String.IsNullOrWhiteSpace(C.Dr("Bank_Name")) = True Then
                            .payment_Type_combo.SelectedIndex = 0
                        Else
                            .payment_Type_combo.SelectedIndex = 1
                            .bankName_Combo.Text = C.Dr("Bank_Name")
                            .CheckNum_txtb.Text = C.Dr("CheckNum")
                        End If

                    End If



                    .money_num_txtb.Text = C.Dr("Value")
                    '/ C.Dr("Cr_Equal_Value")
                    If Convert.ToDouble(.money_num_txtb.Text) < 0 Then .money_num_txtb.Text = .money_num_txtb.Text * -1

                    payment_Type_combo.SelectedValue = C.Dr("Pay_ID")
                    Treasury_ComboBox.SelectedValue = C.Dr("Tr_ID")

                    If C.Dr("isVoid") = 1 Then
                        Void_Lb.Visible = True
                        Void_Lb.BackColor = Color.Red
                        .Fields_Panel.Enabled = False
                        .new_butt.Enabled = False
                        .save_butt.Enabled = False
                        .print_butt.Enabled = False
                        .DeleteButton.Enabled = False
                        Edit_butt.Enabled = False

                    Else
                        Void_Lb.Visible = False
                        .Fields_Panel.Enabled = False
                        .new_butt.Enabled = False
                        .save_butt.Enabled = False
                        .print_butt.Enabled = True
                        Edit_butt.Enabled = True
                        DeleteButton.Enabled = True
                    End If

                    '     Load_AG_Balance()

                End With

                ' Else
                '  MsgBox("لم يتم التعرف على رقم الإيصال", MsgBoxStyle.Exclamation)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()
    End Sub

    Private Sub money_num_txtb_KeyDown(sender As Object, e As KeyEventArgs) Handles money_num_txtb.KeyDown
        If e.KeyCode = Keys.Return Then
            Treasury_ComboBox.Select()
            Treasury_ComboBox.DroppedDown = True
        End If
    End Sub

    Private Sub money_num_txtb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles money_num_txtb.KeyPress
        Check_Only_Float(sender, e)
    End Sub


    Private Sub money_num_txtb_TextChanged(sender As Object, e As EventArgs) Handles money_num_txtb.TextChanged
        Check_Point_in_FloatNum(sender, e)
        Me.money_char_txtb.Text = HANY(Val(money_num_txtb.Text), "EGYPT")
    End Sub

    Private Sub payment_Type_combo_KeyDown(sender As Object, e As KeyEventArgs) Handles payment_Type_combo.KeyDown
        If e.KeyCode = Keys.Return Then
            If payment_Type_combo.SelectedIndex = 1 Then
                bankName_Combo.Select()
                bankName_Combo.DroppedDown = True
            Else
                Notes_txtb.Select()
            End If
        End If
    End Sub

    Private Sub payment_method_combo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles payment_Type_combo.SelectedIndexChanged
        If payment_Type_combo.SelectedIndex = 0 Then
            BankPanel.Enabled = False
            CheckNum_txtb.Clear()
            bankName_Combo.SelectedIndex = -1

        Else
            BankPanel.Enabled = True
        End If
    End Sub

    Private Sub save_butt_Click(sender As Object, e As EventArgs) Handles save_butt.Click
        If Preper_To_Insert() = 1 Then Receipt_Insert()
    End Sub

    Private Function Preper_To_Insert()

        If AG_ID = 0 Then
            MsgBox("الرجاء التأكد من صحة إسم العميل", MsgBoxStyle.Exclamation, "خطأ فالحفظ")
            AG_Cm.Focus()
            Return 0
        End If

        If String.IsNullOrWhiteSpace(money_num_txtb.Text) Then

            MsgBox("الرجاء التأكد من إدخال قيمة المعاملة", MsgBoxStyle.Critical, "خطأ فالحفظ")
            money_num_txtb.Focus()
            Return 0

        ElseIf Convert.ToDouble(money_num_txtb.Text) = 0 Then

            MsgBox("الرجاء التأكد من إدخال قيمة المعاملة", MsgBoxStyle.Critical, "خطأ فالحفظ")
            money_num_txtb.Focus()
            Return 0
            End If


        'If ReceiptTypeComboBox.SelectedValue = 4 Then

        '    If Convert.ToDouble(Treasury_Balance.Text) < Convert.ToDouble(money_num_txtb.Text) Then
        '        Beep()
        '        If MessageBox.Show("المبلغ المدخل أكبر من حساب الخزينة ... إستمرار على أي حال", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
        '                           MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
        '            Return 0
        '        End If

        '    End If
        'End If

        Return 1

    End Function


    Public Sub Receipt_Insert()

        Dim sqlComm As New SqlClient.SqlCommand()

        sqlComm.CommandText = "Agents_BalanceMV_insert_RCT"
        sqlComm.Parameters.AddWithValue("@Receipt_Num", 0)

        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", 0)

        If S_Pr = True And Pr_ID > 0 Then sqlComm.Parameters.AddWithValue("@Pr_ID", Pr_ID)
        sqlComm.Parameters.AddWithValue("@Receipt_Tran_ID", Receipt_Tran_ID)

        sqlComm.Parameters.AddWithValue("@AG_ID", Me.AG_ID)
        sqlComm.Parameters.AddWithValue("Date", Me.DateTimeReceipt.Value)
        sqlComm.Parameters.AddWithValue("@Receipt_Title", Me.Receipt_Title_combobox.Text)

        sqlComm.Parameters.AddWithValue("@Pure", Convert.ToDouble(Me.money_num_txtb.Text))

        sqlComm.Parameters.AddWithValue("@About", Notes_txtb.Text)
        sqlComm.Parameters.AddWithValue("@BsType_ID", Me.ReceiptTypeComboBox.SelectedValue)

        If payment_Type_combo.SelectedIndex = 1 Then
            sqlComm.Parameters.AddWithValue("@Bank_Name", bankName_Combo.Text)
            sqlComm.Parameters.AddWithValue("@CheckNum", CheckNum_txtb.Text)
        End If
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters.AddWithValue("@Tr_ID", Treasury_ComboBox.SelectedValue)
        sqlComm.Parameters.AddWithValue("@Pay_ID", payment_Type_combo.SelectedValue)

        sqlComm.Parameters("@Receipt_Num").Direction = ParameterDirection.Output
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then

            MsgBox("تم حفظ الإيصــال", MsgBoxStyle.Information)
            Reciept_T_ID = sqlComm.Parameters("@T_ID").Value.ToString()
            ReceiptNum = sqlComm.Parameters("@Receipt_Num").Value.ToString()

            'Reload_Balance()
            Current_QTY.Text = Show_AG_T_Balance(AG_ID)

            Is_ComandSuccess = True

            '  If On_Update = False Then
            ReceiptNum_Txt.Text = sqlComm.Parameters("@Receipt_Num").Value.ToString()
            Network_Edit_Tracker_insert(" إيصال للحساب " & AG_Cm.Textt & " بقيمة : " & money_num_txtb.Text, ReceiptNum_Txt.Text, AG_Type, 1)
            'Else
            '    ReceiptNum_Txt.Text = ReceiptNum
            '    Network_Edit_Tracker_insert(" إيصال للحساب " & AG_Cm.Textt & " بقيمة : " & money_num_txtb.Text, ReceiptNum_Txt.Text, AG_Type, 3)
            'End If

            If FormType = 12 Then If Application.OpenForms().OfType(Of Custody).Any Then F_Custody.Custody_CLOSE()
            If FormType = 1 Then If Application.OpenForms().OfType(Of Sales).Any Then Select_Sales_Receipt(F_Sales.T_ID)
            If FormType = 2 Then If Application.OpenForms().OfType(Of Pch).Any Then Select_Pch_Receipt(F_Pch.T_ID)
            If FormType = 8 Then If Application.OpenForms().OfType(Of EXP_Details).Any Then Select_EX_Receipt(F_EXP_Details.T_ID)

            Treasury_Balance.Text = Show_TR_T_Balance(Treasury_ComboBox.SelectedValue)
            After_Save_Receipt()

            'If Application.OpenForms().OfType(Of SearchAgentBill).Any Then SearchAgentBill.Load_Data()
        End If

    End Sub


    Public Sub Receipt_UPDATE()

        Dim sqlComm As New SqlClient.SqlCommand()

        sqlComm.CommandText = "[Agents_BalanceMV_Update_RCT]"
        'sqlComm.Parameters.AddWithValue("@Receipt_Num", ReceiptNum)

        sqlComm.CommandType = CommandType.StoredProcedure
        'sqlComm.Parameters.AddWithValue("@Prev_T_ID", Reciept_T_ID)
            sqlComm.Parameters.AddWithValue("@T_ID", Reciept_T_ID)

            If S_Pr = True And Pr_ID > 0 Then sqlComm.Parameters.AddWithValue("@Pr_ID", Pr_ID)
            sqlComm.Parameters.AddWithValue("@Receipt_Tran_ID", Receipt_Tran_ID)

            sqlComm.Parameters.AddWithValue("@AG_ID", Me.AG_ID)
            sqlComm.Parameters.AddWithValue("Date", Me.DateTimeReceipt.Value)
            sqlComm.Parameters.AddWithValue("@Receipt_Title", Me.Receipt_Title_combobox.Text)

            sqlComm.Parameters.AddWithValue("@Pure", Convert.ToDouble(Me.money_num_txtb.Text))

            sqlComm.Parameters.AddWithValue("@About", Notes_txtb.Text)
            sqlComm.Parameters.AddWithValue("@BsType_ID", Me.ReceiptTypeComboBox.SelectedValue)
            If payment_Type_combo.SelectedIndex = 1 Then
                sqlComm.Parameters.AddWithValue("@Bank_Name", bankName_Combo.Text)
                sqlComm.Parameters.AddWithValue("@CheckNum", CheckNum_txtb.Text)
            End If
            sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
            sqlComm.Parameters.AddWithValue("@Tr_ID", Treasury_ComboBox.SelectedValue)
        sqlComm.Parameters.AddWithValue("@Pay_ID", payment_Type_combo.SelectedValue)

        If SQL_SP_EXEC(sqlComm) = True Then

                MsgBox("تم حفظ الإيصــال", MsgBoxStyle.Information)

            Current_QTY.Text = Show_AG_T_Balance(AG_ID)
            Is_ComandSuccess = True
            ReceiptNum_Txt.Text = ReceiptNum
            Network_Edit_Tracker_insert(" إيصال للحساب " & AG_Cm.Textt & " بقيمة : " & money_num_txtb.Text, ReceiptNum_Txt.Text, AG_Type, 3)

            If FormType = 12 Then If Application.OpenForms().OfType(Of Custody).Any Then F_Custody.Custody_CLOSE()
            If FormType = 1 Then If Application.OpenForms().OfType(Of Sales).Any Then Select_Sales_Receipt(F_Sales.T_ID)
            If FormType = 2 Then If Application.OpenForms().OfType(Of Pch).Any Then Select_Pch_Receipt(F_Pch.T_ID)
            If FormType = 8 Then If Application.OpenForms().OfType(Of EXP_Details).Any Then Select_EX_Receipt(F_EXP_Details.T_ID)

            Treasury_Balance.Text = Show_TR_T_Balance(Treasury_ComboBox.SelectedValue)
        After_Save_Receipt()

        Edit_butt.Text = EditState
        Edit_butt.BackColor = Color.White
        On_Update = False

            'If Application.OpenForms().OfType(Of SearchAgentBill).Any Then SearchAgentBill.Load_Data()
        End If

    End Sub


    Private Sub print_butt_Click(sender As Object, e As EventArgs) Handles print_butt.Click
        ValidateChildren()
        Print_RECIEPT()
        '    My.Computer.Audio.Play("D:\Music & Images Disk\Music\إيطاليا.wav")
        '    ', AudioPlayMode.WaitToComplete)

    End Sub

    Public Sub Print_RECIEPT()

        Try
            Dim pp As New ReportConnection

            pp.rp.Load(Application.StartupPath & Receipt_Track)

            pp.CrTables = pp.rp.Database.Tables
            For Each CrTable In pp.CrTables
                pp.crtableLogoninfo = CrTable.LogOnInfo
                pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
                CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
            Next


            With pp
                .rp.SetParameterValue(0, Convert.ToDouble(money_num_txtb.Text))

                If String.IsNullOrWhiteSpace(CR_Phone_Txt.Text) Then
                    .rp.SetParameterValue(1, AG_Cm.Textt)
                Else
                    .rp.SetParameterValue(1, AG_Cm.Textt & " \ " & CR_Phone_Txt.Text)
                End If

                '.rp.SetParameterValue(1, AG_Cm.Textt & " \ " & CR_Phone_Txt.Text)
                .rp.SetParameterValue(2, ReceiptNum_Txt.Text)
                .rp.SetParameterValue(3, DateTimeReceipt.Value)


                .rp.SetParameterValue(4, payment_Type_combo.Text)
                If payment_Type_combo.SelectedIndex = 0 Then
                    .rp.SetParameterValue(11, "--------------------------------")
                Else
                    .rp.SetParameterValue(11, "  " + "شيك رقم  " + CheckNum_txtb.Text + " * " + "  " + " عن مصرف " + bankName_Combo.Text + " * ")
                End If

                .rp.SetParameterValue(5, Notes_txtb.Text + "  " + " * " + Receipt_Title_combobox.Text + " * ")

                .rp.SetParameterValue(6, money_char_txtb.Text)

                If AG_Show_Balance_CB.Checked = True Then
                    .rp.SetParameterValue(7, "رصيد الحساب :  " & Current_QTY.Text)
                Else
                    .rp.SetParameterValue(7, " ")
                End If


                .rp.SetParameterValue(8, USER_NAME)
                '.rp.SetParameterValue(9, ReceiptCode_Contx_Label.Text)
                .rp.SetParameterValue(10, ReceiptTypeComboBox.Text)
                '
                .rp.SetParameterValue(12, SBill_Title_1)

                If ReceiptTypeComboBox.SelectedValue = 2 Or ReceiptTypeComboBox.SelectedValue = 4 Or ReceiptTypeComboBox.SelectedValue = 5 Then
                    .rp.SetParameterValue(13, "سلمت للسيد")
                ElseIf ReceiptTypeComboBox.SelectedValue = 3 Or ReceiptTypeComboBox.SelectedValue = 9 Then
                    .rp.SetParameterValue(13, "استلمت من السيد")
                End If

                If ReceiptPage_ID = 1 Then
                    If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_80))
                    .rp.PrintOptions.PrinterName = Default_Printer_80
                Else
                    If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_A4))
                    .rp.PrintOptions.PrinterName = Default_Printer_A4
                End If
                .rp.SetParameterValue(14, 0)



                If Show_Bill_CB.Checked = True Then
                    Dim p As New print
                    p.CrystalReportViewer1.ReportSource = pp.rp
                    p.ShowDialog()
                Else
                    .rp.PrintToPrinter(1, False, 0, 0)
                    .rp.Dispose()
                End If

            End With


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub Receipt_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'F_MainForm.Fill_ALL_IM()
        Me.Dispose()
    End Sub

    Private Sub Receipt_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then If new_butt.Enabled = True Then new_butt_Click(sender, e)
        If e.KeyCode = Keys.F12 Then If save_butt.Enabled = True Then save_butt_Click(sender, e)
        If e.KeyCode = Keys.F3 Then If DeleteButton.Enabled = True Then DeleteButton_Click(sender, e)
        If e.KeyCode = Keys.Escape Then Me.Close()

        If e.KeyCode = Keys.Return Then
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
        End If
    End Sub

    Private Sub Receipt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        ThemeManager.ApplyThemeToForm(Me)
        EditState = Edit_butt.Text
        Edit_butt.Visible = U_Update_Receipt
        DeleteButton.Visible = U_Cancel_Receipt
        Get_MAX_T_ID()
      
        Load_Data()
        Title_Lb.Text = "شاشة إدراج : " + ReceiptTypeComboBox.Text
        Title_Lb.BackColor = ReceiptTypeComboBox.BackColor
        ReceiptNum = ReceiptNum_Txt.Text

        If AG_ID > 0 Then AG_Cm.Set_IM_By_ID(AG_ID)

        AG_Show_Balance_CB.Checked = MY_Settings.AG_Show_Balance_in_Receipt
    End Sub


    Public Sub Get_MAX_T_ID()

        Dim C As New C
        Dim S As String = "Select ISNULL(MAX(Receipt_Num),0) + 1 AS MX From Agents_Balance_MV_RCT"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                If FormType = 0 Then ClearFields()
                ReceiptNum_Txt.Text = C.Dr("MX")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

    End Sub


    Dim Tmp_Bill_ID As Integer
    Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
        Tmp_Bill_ID = ReceiptNum
        ReceiptNum_Txt.Text = ReceiptNum - 1
        Get_T_ID()
    End Sub

    Public Sub Get_T_ID()
            Dim C As New C
            Dim S As String = ""
        S = "Select T_ID From Agents_Balance_MV_RCT Where Receipt_Num = '" & Convert.ToInt64(ReceiptNum_Txt.Text) & "'"

            C.Com = New SqlClient.SqlCommand(S, C.Con)
            C.Con.Open()
            Try
                C.Dr = C.Com.ExecuteReader
                If C.Dr.HasRows Then
                    C.Dr.Read()
                    ClearFields()
                    Select_Receipt(C.Dr("T_ID"))
                    new_butt.Enabled = True
                Else
                    MsgBox("لم يتم التعرف على الفاتورة", MsgBoxStyle.Exclamation)
                    ReceiptNum_Txt.Text = Tmp_Bill_ID
                End If

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            C.Con.Close()
    End Sub

    Private Sub Up_Bill_btn_Click(sender As Object, e As EventArgs) Handles Up_Bill_btn.Click
        If Not String.IsNullOrWhiteSpace(ReceiptNum_Txt.Text) Then
            Tmp_Bill_ID = ReceiptNum
            ReceiptNum_Txt.Text = ReceiptNum + 1
            Get_T_ID()
        End If
    End Sub

    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    'Private Sub DateTimeReceipt_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimeReceipt.KeyDown
    '    If e.KeyCode = Keys.Return Then Receipt_Title_combobox.Select()
    'End Sub

    'Private Sub bankName_Combo_KeyDown(sender As Object, e As KeyEventArgs) Handles bankName_Combo.KeyDown
    '    If e.KeyCode = Keys.Return Then CheckNum_txtb.Select()
    'End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        Beep()
        If MessageBox.Show("سيتم حـــذف الإيصال بشكل نهائي وكل المعاملات المتعلقة به ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                       MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then Agents_Balance_MV_RCT_VOID(Reciept_T_ID)
    End Sub

    'Private Sub CheckNum_txtb_KeyDown(sender As Object, e As KeyEventArgs) Handles CheckNum_txtb.KeyDown
    '    If e.KeyCode = Keys.Return Then Notes_txtb.Select()
    'End Sub




    Public Sub Agents_Balance_MV_RCT_VOID(T_ID As Integer)
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "[Agents_Balance_MV_RCT_VOID]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تم إلغــاء المعاملة ", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert("إلغاء الإيصال", F_Receipt.ReceiptNum_Txt.Text, AG_Type, 3)

            If FormType = 1 Then Select_Sales_Receipt(F_Sales.T_ID)
            If FormType = 2 Then Select_Pch_Receipt(F_Pch.T_ID)
            If FormType = 8 Then Select_EX_Receipt(F_EXP_Details.T_ID)
            F_Receipt.Void_Lb.Visible = True
            F_Receipt.Void_Lb.BackColor = Color.Red
            F_Receipt.Treasury_ComboBox.Text = Show_TR_T_Balance(F_Receipt.Treasury_ComboBox.SelectedValue)
            F_Receipt.print_butt.Enabled = False
            F_Receipt.Edit_butt.Enabled = False
            F_Receipt.DeleteButton.Enabled = False
        End If

    End Sub
    Public Sub Fetch_ItemToList()
        AG_ID = AG_Cm.TXT_ID.Text
        Current_QTY.Text = Show_AG_T_Balance(AG_ID)
        Fetch_AG_Currency()
    End Sub


    Public Sub Fetch_AG_Currency()
        'Dim C As New C
        'Dim S As String = "Select Cr_ID,Cr_Equal From AGENTS_MENU_V Where AG_ID = '" & AG_ID & "'"
        'C.Com = New SqlClient.SqlCommand(S, C.Con)
        'C.Con.Open()
        'Try
        '    C.Dr = C.Com.ExecuteReader
        '    If C.Dr.HasRows Then
        '        C.Dr.Read()
        '        Cr_CM.SelectedValue = C.Dr("Cr_ID")
        '        Cr_Equal_TXT.Text = C.Dr("Cr_Equal")

        '        If Cr_CM.SelectedValue = 1 Then
        '            CurrencyPanel.Visible = False
        '        Else
        '            CurrencyPanel.Visible = True
        '        End If

        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        'C.Con.Close()
    End Sub

    'Private Sub Barcode_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_SH_txt.KeyDown
    '    If e.KeyCode = Keys.Return Then If String.IsNullOrWhiteSpace(Barcode_SH_txt.Text) = False Then Load_IM_Barcode()
    '    If e.KeyCode = Keys.Delete Then Barcode_SH_txt.Clear()
    'End Sub

    Public Sub Load_IM_Barcode()
        Dim c As New C
        IM_Dt.Clear()
        Try
            Dim s As String
            s = "select AG_ID,Ag_name,isnull(T_Balance,0) AS T_Balance from Agents WHERE Barcode = '" & Barcode_SH_txt.Text & "'"

            C.Com = New SqlClient.SqlCommand(S, C.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                'AG_ID = c.Dr("AG_ID")
                AG_Cm.Set_IM_By_ID(c.Dr("AG_ID"))
                'IM_SH_txt.Text = c.Dr("Ag_name")
                Current_QTY.Text = c.Dr("T_Balance")
                'IMDataGridViewX.Visible = False
                Fetch_AG_Currency()
            Else
                MsgBox("لم يتم التعرف على الرقم ", MsgBoxStyle.Exclamation)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Current_QTY_DoubleClick(sender As Object, e As EventArgs) Handles Current_QTY.DoubleClick
        Dim Num As Double = Convert.ToDouble(Current_QTY.Text)
        If Num < 0 Then Num = Num * -1
        money_num_txtb.Text = Num
    End Sub

    'Private Sub Receipt_Title_combobox_KeyDown_1(sender As Object, e As KeyEventArgs) Handles Receipt_Title_combobox.KeyDown
    '    If e.KeyCode = Keys.Return Then money_num_txtb.Select()
    'End Sub


    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        If Reciept_T_ID > 0 Then
            If On_Update = False Then

                Beep()
                If MessageBox.Show(" تعديل الإيصال ؟ ", "تعديل", MessageBoxButtons.YesNo, _
                             MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    Edit_butt.BackColor = Color.GreenYellow
                    On_Update = True
                    Fields_Panel.Enabled = True
                    'Show_IM_btn.Enabled = True

                    Edit_butt.Text = "حفظ التعديل"
                    'Network_Edit_Tracker_insert("تعديل " + ReceiptTypeComboBox.Text + " / رقم  : " + ReceiptNum_Txt.Text, money_num_txtb.Text, 0, 0)
                End If
            Else
                If Preper_To_Insert() = 1 Then Receipt_UPDATE()

            End If
        End If
    End Sub

   

    Private Sub ReceiptNum_Txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ReceiptNum_Txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub ReceiptNum_Txt_Enter(sender As Object, e As EventArgs) Handles ReceiptNum_Txt.Enter
        Tmp_Bill_ID = Convert.ToInt64(ReceiptNum_Txt.Text)
    End Sub

    Private Sub ReceiptNum_Txt_KeyDown(sender As Object, e As KeyEventArgs) Handles ReceiptNum_Txt.KeyDown
        If e.KeyCode = Keys.Return Then
            Tmp_Bill_ID = ReceiptNum
            Get_T_ID()
        End If
    End Sub

    Private Sub Show_Bill_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Show_Bill_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub


    Private Sub إضافةالإيصالإلىقائمةالعهدToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إضافةالإيصالإلىقائمةالعهدToolStripMenuItem.Click
        If ReceiptTypeComboBox.SelectedValue = 3 Then
            MsgBox("لا يمكن إجراء عهدة لإيصال قبض", MsgBoxStyle.Exclamation, "")
        Else
            Custody_SELECT()
        End If

    End Sub

    Public Sub Custody_SELECT()
        Dim c As New C
        Try
            Dim s As String
            s = "select *  FROM Custody_V WHERE T_ID = '" & ReceiptNum & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                MsgBox("تم إعتماد الواصل كهعدة", MsgBoxStyle.Information)
            Else
                Custody_INSERT()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Custody_INSERT()

        Dim C As New C
        With C.Com
            .CommandText = "Custody_INSERT"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", ReceiptNum)
            .Parameters.AddWithValue("@C_Rcpt_T_ID", ReceiptNum)
            '.Parameters("@T_ID").Direction = ParameterDirection.Output
        End With

        If SQL_SP_EXEC(C.Com) = True Then
            MsgBox("تمت إضافة العهدة", MsgBoxStyle.Information)
            'Network_Edit_Tracker_insert(" العميل:" & IM_SH_txt.Text & " الرقم:" & Barcode_txt.Text & " لهاتف:" & AG_Phone_TextBox.Text & " العنوان:" & AG_AddressTextBox.Text & " البريد الإلكتروني:" _
            '                & Email_txt.Text & " النوع:" & AG_Type_cm.Text & " العملة:" & Cr_CM.Text & " المرتب:" & SalaryTextBox.Text & " إشعار الدين:" & Max_Debit_txt.Text, 0, 27, 1)
        End If

    End Sub

    Private Sub Treasury_ComboBox_KeyDown(sender As Object, e As KeyEventArgs) Handles Treasury_ComboBox.KeyDown
        If e.KeyCode = Keys.Return Then
            payment_Type_combo.Select()
            payment_Type_combo.DroppedDown = True
        End If
    End Sub

    Private Sub AG_Cm_ID_Changed(sender As Object, e As EventArgs) Handles AG_Cm.ID_Changed
        If AG_Cm.TXT_ID.Text > 0 Then Fetch_ItemToList()
    End Sub



    Private Sub Treasury_ComboBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles Treasury_ComboBox.SelectedValueChanged
        If TypeName(Treasury_ComboBox.SelectedValue) = "Integer" Then
            Treasury_Balance.Text = Show_TR_T_Balance(Treasury_ComboBox.SelectedValue)
            check_hide_tr(Treasury_ComboBox, Treasury_Balance)
        End If
    End Sub

    'Private Sub OnKeyBoard1_UC_Button1Click(sender As Object, e As EventArgs)
    '    Dim BT As New Button
    '    BT = sender

    '    For Each a As Control In Fields_Panel.Controls
    '        If a.Name = KEY_HANDLER Then
    '            a.Focus()
    '            a.Select()

    '            If BT.Name <> "BLANG" And BT.Name <> "BCLEAR" And BT.Name <> "BSPACE" Then

    '                If TypeOf a Is FSearch_Filter Then
    '                    Dim a2 As New FSearch_Filter
    '                    a2.Textt = a.Text
    '                    'a2.Textt += BT.Text
    '                Else
    '                    SendKeys.Send("{" & BT.Text & "}")
    '                End If

    '            ElseIf BT.Name = "BCLEAR" Then
    '                If TypeOf a Is FSearch_Filter Then
    '                    Dim a2 As New FSearch_Filter
    '                    a2.Textt = ""
    '                Else
    '                    a.Text = ""
    '                End If

    '            ElseIf BT.Name = "BSPACE" Then
    '                SendKeys.Send(" ")
    '            End If


    '        End If
    '        'a.Text += BT.Text
    '    Next
    'End Sub

    Private Sub AG_Cm_Enter(sender As Object, e As EventArgs) Handles AG_Cm.Enter, money_num_txtb.Enter, Receipt_Title_combobox.Enter, Notes_txtb.Enter
        KEY_HANDLER = sender.name
    End Sub


    Private Sub AG_Show_Balance_CB_CheckedChanged(sender As Object, e As EventArgs) Handles AG_Show_Balance_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.AG_Show_Balance_in_Receipt = AG_Show_Balance_CB.Checked
        MY_Settings.Save_AppSetting()
    End Sub

    Private Sub payment_Type_combo_SelectedValueChanged(sender As Object, e As EventArgs) Handles payment_Type_combo.SelectedValueChanged
        If TypeName(payment_Type_combo.SelectedValue) = "Integer" Then PaymentMethodDefaultAccounts_SELECT()
    End Sub


    Public Sub PaymentMethodDefaultAccounts_SELECT()
        Treasury_ComboBox.Enabled = True
        Dim c As New C
        Dim Tmp_Tr_ID As Integer = payment_Type_combo.SelectedValue
        Try
            Dim s As String
            s = "select TOP 1 AccountID , isnull(is_Lock,0) as is_Lock FROM PaymentMethodDefaultAccounts WHERE PaymentMethodID = '" & payment_Type_combo.SelectedValue & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Treasury_ComboBox.SelectedValue = c.Dr("AccountID")
                If c.Dr("is_Lock") = True Then Treasury_ComboBox.Enabled = False
            Else
                payment_Type_combo.SelectedValue = Tmp_Tr_ID
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Back_Btn_Click(sender As Object, e As EventArgs) Handles Back_Btn.Click
        Me.Close()
    End Sub
End Class