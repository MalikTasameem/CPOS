Public Class Tr_Deposit_Withdraw


    Dim AGBalance_T_ID As Integer = 0
    Dim T_ID As Integer = 0
    Dim Is_First_Time As String = "لا"
    Dim USER_NAME As String
    Private Sub new_butt_Click(sender As Object, e As EventArgs) Handles new_butt.Click
        Make_New_Receipt()
    End Sub

    Private Sub Make_New_Receipt()
        Fields_Panel.Enabled = True
        ClearFields()
        save_butt.Enabled = True
        print_butt.Enabled = False
        Delete_butt.Enabled = False
        Is_First_Time = "لا"
    End Sub

    Private Sub After_Save_Receipt()
        Fields_Panel.Enabled = False
        save_butt.Enabled = False
        print_butt.Enabled = True
        Delete_butt.Enabled = True
    End Sub


    Private Sub ClearFields()
        For Each a As Control In Fields_Panel.Controls
            If TypeOf a Is TextBox Then
                a.Text = Nothing
            End If
        Next

        Treasury_ComboBox.SelectedValue = 1
        DateTimeReceipt.Text = Date.Now
        ReceiptNum_Txt.Text = "0000"

        If ReceiptTypeComboBox.SelectedValue = 1 Then
            ReDescription_txtb.Text = "سحب مباشر"
        Else
            ReDescription_txtb.Text = "إيداع مباشر"
        End If
        isFirstTran_CB.Checked = False

        Treasury_Balance.Text = Show_TR_T_Balance(Treasury_ComboBox.SelectedValue)
    End Sub

    Private Sub Load_Data()

        Dim C As New C

        Try
            Dim sql As String = "Select id , Type_Name from TreasuryBalance_Type Order By id ASC"
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            ReceiptTypeComboBox.DataSource = C.Dt
            ReceiptTypeComboBox.DisplayMember = "Type_Name"
            ReceiptTypeComboBox.ValueMember = "id"
            ReceiptTypeComboBox.SelectedValue = TR_Type
            If TR_Type <> 2 Then
                isFirstTran_CB.Visible = False
            Else
                isFirstTran_CB.Visible = True
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        C = New C
        Try
            Dim sql As String = "Select Distinct Tr_ID,Tr_Name FROM TR_MENU_V "
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            Treasury_ComboBox.DataSource = C.Dt
            Treasury_ComboBox.DisplayMember = "Tr_Name"
            Treasury_ComboBox.ValueMember = "Tr_ID"
            Treasury_Balance.Text = Show_TR_T_Balance(Treasury_ComboBox.SelectedValue)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        If isShowing_Trans = True Then
            Select_Tr_MV(F_Balances.Tr_MV_MetroGrid.CurrentRow.Cells("Tr_T_ID_CL").Value)
        End If

    End Sub

    Public Sub Select_Tr_MV(T_ID As Integer)
        Dim C As New C
        Dim S As String = "Select * From Tr_MV_V Where T_ID = '" & T_ID & "'"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                With Me
                    .ReceiptTypeComboBox.SelectedValue = C.Dr("Tr_Type_ID")
                    .T_ID = C.Dr("T_ID")
                    .ReceiptNum_Txt.Text = C.Dr("T_ID")
                    .Treasury_ComboBox.SelectedValue = C.Dr("Tr_ID")
                    .DateTimeReceipt.Text = C.Dr("Date")
                    .ReDescription_txtb.Text = C.Dr("notice_move")
                    .money_num_txtb.Text = C.Dr("Value")
                    If Convert.ToDouble(.money_num_txtb.Text) < 0 Then
                        .money_num_txtb.Text = .money_num_txtb.Text * -1
                    End If
                    .Fields_Panel.Enabled = False
                    '.new_butt.Enabled = False
                    .save_butt.Enabled = False
                    .print_butt.Enabled = True
                    '  MsgBox(C.Dr("isVoid").ToString)

                    If C.Dr("isVoid") = True Then
                        Delete_butt.Enabled = False
                        Void_Lb.Visible = True
                    Else
                        Delete_butt.Enabled = True
                        Void_Lb.Visible = False
                    End If

                    isFirstTran_CB.Checked = C.Dr("isFirst_Time")
                    AGBalance_T_ID = C.Dr("AGBalance_T_ID")

                    Recipt_Num_txt.Text = C.Dr("Receipt_Num")
                    Me.USER_NAME = C.Dr("UserName")
                End With

            Else
                MsgBox("لم يتم التعرف على رقم الإيصال", MsgBoxStyle.Exclamation)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()
    End Sub

    Private Sub money_num_txtb_KeyDown(sender As Object, e As KeyEventArgs) Handles money_num_txtb.KeyDown
        If e.KeyCode = Keys.Return Then ReDescription_txtb.Select()
    End Sub

    Private Sub money_num_txtb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles money_num_txtb.KeyPress
        Check_Only_Float(sender, e)
    End Sub


    Private Sub money_num_txtb_TextChanged(sender As Object, e As EventArgs) Handles money_num_txtb.TextChanged
        If money_num_txtb.Text = "." Then
            money_num_txtb.Text = "0.0"
        End If
        Me.money_char_txtb.Text = HANY(Val(money_num_txtb.Text), "EGYPT")
    End Sub

    Private Sub save_butt_Click(sender As Object, e As EventArgs) Handles save_butt.Click

        If String.IsNullOrWhiteSpace(money_num_txtb.Text) Then

            MsgBox("الرجاء التأكد من إدخال قيمة المعاملة", MsgBoxStyle.Critical, "خطأ فالحفظ")
            ' money_num_txtb.Select()
            money_num_txtb.Focus()
            Exit Sub

        ElseIf Convert.ToDouble(money_num_txtb.Text) = 0 Then

            MsgBox("الرجاء التأكد من إدخال قيمة المعاملة", MsgBoxStyle.Critical, "خطأ فالحفظ")
            ' money_num_txtb.Select()
            money_num_txtb.Focus()
            Exit Sub

        Else

            If TR_Type = 1 Then
                If Convert.ToDouble(Treasury_Balance.Text) < Convert.ToDouble(money_num_txtb.Text) Then
                    Beep()
                    If MessageBox.Show("المبلغ المدخل أكبر من حساب الخزينة ... إستمرار على أي حال", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, _
                                       MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If

                End If
            End If

            Receipt_Insert()

        End If

    End Sub



    Public Sub Receipt_Insert()

        Dim sqlComm As New SqlClient.SqlCommand()

        sqlComm.CommandText = "Treasury_BalanceMV_insert"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", 0)
        sqlComm.Parameters.AddWithValue("Date", Me.DateTimeReceipt.Value)
        ' sqlComm.Parameters.AddWithValue("@Time", Me.DateTimeReceipt.Value.TimeOfDay)
        If ReceiptTypeComboBox.SelectedValue = 1 Then
            sqlComm.Parameters.AddWithValue("@Debit", Me.money_num_txtb.Text)
        Else
            sqlComm.Parameters.AddWithValue("@Credit", Me.money_num_txtb.Text)
        End If
        sqlComm.Parameters.AddWithValue("@Tr_Type_ID", TR_Type)
        sqlComm.Parameters.AddWithValue("@About", ReDescription_txtb.Text)
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters.AddWithValue("@Tr_ID", Treasury_ComboBox.SelectedValue)
        sqlComm.Parameters.AddWithValue("@isFirst_Time", isFirstTran_CB.Checked)
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then

            If ReceiptTypeComboBox.SelectedValue = 1 Then
                Network_Edit_Tracker_insert(" من حساب الخزينة:" & Treasury_ComboBox.Text & " القيمة:" & money_num_txtb.Text, Me.ReceiptNum_Txt.Text, 24, 1)
            Else
                Network_Edit_Tracker_insert(" إلى حساب الخزينة:" & Treasury_ComboBox.Text & " القيمة:" & money_num_txtb.Text & " أول مدة:" & Is_First_Time, Me.ReceiptNum_Txt.Text, 25, 1)
            End If

            MsgBox("تمت العملية بنجاح", MsgBoxStyle.Information)
            Me.ReceiptNum_Txt.Text = sqlComm.Parameters("@T_ID").Value.ToString()
            Treasury_Balance.Text = Show_TR_T_Balance(Treasury_ComboBox.SelectedValue)
            After_Save_Receipt()
            Select_Tr_MV(Me.ReceiptNum_Txt.Text)
        End If


    End Sub


    Private Sub Tr_Deposit_Withdraw_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Treasury_ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Treasury_ComboBox.SelectedIndexChanged
        On Error Resume Next
        Treasury_Balance.Text = Show_TR_T_Balance(Treasury_ComboBox.SelectedValue)
    End Sub

    Private Sub DateTimeReceipt_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimeReceipt.KeyDown
        If e.KeyCode = Keys.Return Then
            Treasury_ComboBox.DroppedDown = True
            Treasury_ComboBox.Select()
        End If
    End Sub



    Private Sub Tr_Deposit_Withdraw_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then If new_butt.Enabled = True Then new_butt_Click(sender, e)
        If e.KeyCode = Keys.F2 Then If save_butt.Enabled = True Then save_butt_Click(sender, e)
        If e.KeyCode = Keys.F4 Then If Delete_butt.Enabled = True Then Delete_butt_Click(sender, e)
        'If e.KeyCode = Keys.ControlKey And Keys.P Then If print_butt.Enabled = True Then print_butt_Click(sender, e)

    End Sub

    Private Sub Tr_Deposit_Withdraw_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        '  rs.FindAllControls(Me)


        If TR_Type = 1 Then
            If U_Tr_Widraw = False Then
                MsgBox("خارج صلاحياتك", MsgBoxStyle.Critical, "صلاحية المستخدم")
                Me.Close()
            End If
        End If

        If TR_Type = 2 Then
            If U_Tr_Deposit = False Then
                MsgBox("خارج صلاحياتك", MsgBoxStyle.Critical, "صلاحية المستخدم")
                Me.Close()
            End If
        End If


        Load_Data()
    End Sub
 

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub isFirstTran_CB_CheckedChanged(sender As Object, e As EventArgs) Handles isFirstTran_CB.CheckedChanged
        CB_CHecked(sender)
        If sender.checked = True Then
            Is_First_Time = "نعم"
        Else
            Is_First_Time = "لا"
        End If
    End Sub

    Private Sub Delete_butt_Click(sender As Object, e As EventArgs) Handles Delete_butt.Click
        If AGBalance_T_ID = 0 Then
            Beep()
            If MessageBox.Show(" إلغاء السند بشكل نهائي ", "", MessageBoxButtons.OKCancel, _
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then Treasury_BalanceMV_isVoid()
        Else
            MsgBox("لا يمكنك إلغاء هذه الحركة بشكل مباشر ... قم بإلغائها عن عن طريق إلغاء رقم الإيصال المرتبط بها", MsgBoxStyle.Exclamation)
        End If

    End Sub


    Public Sub Treasury_BalanceMV_isVoid()
        Dim sqlComm As New SqlClient.SqlCommand()
        sqlComm.CommandText = "[Treasury_BalanceMV_isVoid]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        If SQL_SP_EXEC(sqlComm) = True Then

            If ReceiptTypeComboBox.SelectedValue = 1 Then
                Network_Edit_Tracker_insert(" من حساب الخزينة:" & Treasury_ComboBox.Text & " القيمة:" & money_num_txtb.Text, Me.ReceiptNum_Txt.Text, 24, 2)
            Else
                Network_Edit_Tracker_insert(" إلى حساب الخزينة:" & Treasury_ComboBox.Text & " القيمة:" & money_num_txtb.Text & " أول مدة:" & Is_First_Time, Me.ReceiptNum_Txt.Text, 25, 2)
            End If
            Treasury_Balance.Text = Show_TR_T_Balance(Treasury_ComboBox.SelectedValue)
            Delete_butt.Enabled = False
            Void_Lb.Visible = True
        End If
    End Sub

    Private Sub print_butt_Click(sender As Object, e As EventArgs) Handles print_butt.Click
        Print_RECIEPT()
    End Sub

    Public Sub Print_RECIEPT()

        Try
            Dim pp As New ReportConnection

            pp.rp.Load(Application.StartupPath & "\Reports\Treasury_MV_A5.rpt")

            pp.CrTables = pp.rp.Database.Tables
            For Each CrTable In pp.CrTables
                pp.crtableLogoninfo = CrTable.LogOnInfo
                pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
                CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
            Next

            With pp
                .rp.SetParameterValue(0, money_num_txtb.Text)
                .rp.SetParameterValue(1, ReceiptNum_Txt.Text)
                .rp.SetParameterValue(2, DateTimeReceipt.Value)
                .rp.SetParameterValue(3, money_char_txtb.Text)
                .rp.SetParameterValue(4, Me.USER_NAME)
                .rp.SetParameterValue(5, SBill_Title_1)
                .rp.SetParameterValue(6, ReDescription_txtb.Text)
                .rp.SetParameterValue(7, Treasury_ComboBox.Text)
                .rp.SetParameterValue(8, ReceiptTypeComboBox.Text)

                If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_A4))
                .rp.PrintOptions.PrinterName = Default_Printer_A4
                .rp.PrintToPrinter(1, False, 0, 0)
                .rp.Dispose()

            End With


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Treasury_ComboBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles Treasury_ComboBox.SelectedValueChanged
        check_hide_tr(Treasury_ComboBox, Treasury_Balance)
    End Sub
End Class