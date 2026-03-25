Public Class Tr_Transfers : Inherits Windows.Forms.Form

    Dim Tran_ID As Integer
    Dim User_Name As String

    Private Sub new_butt_Click(sender As Object, e As EventArgs) Handles new_butt.Click
        Make_New_Receipt()
    End Sub

    Private Sub Make_New_Receipt()
        Fields_Panel.Enabled = True
        ClearFields()
        save_butt.Enabled = True
        print_butt.Enabled = False
        Cancel_Btn.Enabled = False
        Void_Lb.Visible = False
    End Sub

    Private Sub After_SELECT_Receipt()
        Fields_Panel.Enabled = False
        save_butt.Enabled = False
        print_butt.Enabled = True
        Cancel_Btn.Enabled = True
        Void_Lb.Visible = False
    End Sub

    Private Sub After_Save_Receipt()
        Fields_Panel.Enabled = False
        save_butt.Enabled = False
        print_butt.Enabled = True
        Cancel_Btn.Enabled = True
    End Sub

    Private Sub After_Void_Receipt()
        Fields_Panel.Enabled = False
        save_butt.Enabled = False
        print_butt.Enabled = False
        Cancel_Btn.Enabled = False
        Void_Lb.Visible = True
    End Sub



    Private Sub ClearFields()
        For Each a As Control In Fields_Panel.Controls
            If TypeOf a Is TextBox Then
                a.Text = Nothing
            End If
        Next

        F_Treasury_ComboBox.SelectedValue = 1
        T_Treasury_ComboBox.SelectedValue = 1

        DateTimeReceipt.Text = Date.Now
        Select_Tran_MAX()
    End Sub

    Private Sub Load_Data()

        Dim C As New C
        Dim C2 As New C
        Try
            Dim sql As String = "Select Distinct Tr_ID,Tr_Name FROM TR_MENU_V "
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            C.Da.Fill(C2.Dt)

            F_Treasury_ComboBox.DataSource = C.Dt
            F_Treasury_ComboBox.DisplayMember = "Tr_Name"
            F_Treasury_ComboBox.ValueMember = "Tr_ID"
            F_Treasury_Balance.Text = Show_TR_T_Balance(F_Treasury_ComboBox.SelectedValue)

            T_Treasury_ComboBox.DataSource = C2.Dt
            T_Treasury_ComboBox.DisplayMember = "Tr_Name"
            T_Treasury_ComboBox.ValueMember = "Tr_ID"
            T_Treasury_Balance.Text = Show_TR_T_Balance(T_Treasury_ComboBox.SelectedValue)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

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

        If F_Treasury_ComboBox.SelectedValue = T_Treasury_ComboBox.SelectedValue Then

            MsgBox("لا يمكن نقل قيمة إلى نفس الخزينة", MsgBoxStyle.Critical, "خطأ فالحفظ")
            Exit Sub
        ElseIf String.IsNullOrWhiteSpace(money_num_txtb.Text) Then

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

            Treasury_BalanceMV_Transfer()

        End If

    End Sub



    Public Sub Treasury_BalanceMV_Transfer()

        Dim sqlComm As New SqlClient.SqlCommand()

        sqlComm.CommandText = "Treasury_BalanceMV_Transfer"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@Tran_ID", 0)
        sqlComm.Parameters.AddWithValue("Date", Me.DateTimeReceipt.Value)
        sqlComm.Parameters.AddWithValue("@F_Tr_ID", F_Treasury_ComboBox.SelectedValue)
        sqlComm.Parameters.AddWithValue("@T_Tr_ID", T_Treasury_ComboBox.SelectedValue)
        sqlComm.Parameters.AddWithValue("@Value", Me.money_num_txtb.Text)
        sqlComm.Parameters.AddWithValue("@About", ReDescription_txtb.Text)
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters("@Tran_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تم الإضافة بنجاح", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" من حساب الخزينة:" & F_Treasury_ComboBox.Text & " إلى حساب الخزينة:" & T_Treasury_ComboBox.Text & " القيمة:" & money_num_txtb.Text, Me.ReceiptNum_Txt.Text, 26, 1)
            Me.ReceiptNum_Txt.Text = sqlComm.Parameters("@Tran_ID").Value.ToString()

            F_Treasury_Balance.Text = Show_TR_T_Balance(F_Treasury_ComboBox.SelectedValue)
            T_Treasury_Balance.Text = Show_TR_T_Balance(T_Treasury_ComboBox.SelectedValue)
            'After_Save_Receipt()
            Select_Tr_Balance_Transfer()
        End If


    End Sub

    Private Sub print_butt_Click(sender As Object, e As EventArgs) Handles print_butt.Click

        Print_RECIEPT()
    End Sub

    Public Sub Print_RECIEPT()

        Try
            Dim pp As New ReportConnection

            pp.rp.Load(Application.StartupPath & "\Reports\Treasury_Trans_A5.rpt")

            pp.CrTables = pp.rp.Database.Tables
            For Each CrTable In pp.CrTables
                pp.crtableLogoninfo = CrTable.LogOnInfo
                pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
                CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
            Next


            With pp
                .rp.SetParameterValue(0, money_num_txtb.Text)
                .rp.SetParameterValue(1, ReceiptNum_Txt.Text)
                .rp.SetParameterValue(2, DateTimeReceipt.Text)
                .rp.SetParameterValue(3, money_char_txtb.Text)
                .rp.SetParameterValue(4, User_Name)
                .rp.SetParameterValue(5, SBill_Title_1)
                .rp.SetParameterValue(6, ReDescription_txtb.Text)
                .rp.SetParameterValue(7, F_Treasury_ComboBox.Text)
                .rp.SetParameterValue(8, T_Treasury_ComboBox.Text)
                .rp.SetParameterValue(9, "إذن تحويل بين الخزائن")
                If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_A4))
                .rp.PrintOptions.PrinterName = Default_Printer_A4
                .rp.PrintToPrinter(1, False, 0, 0)
                .rp.Dispose()

            End With


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub Tr_Deposit_Withdraw_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub


    Private Sub Treasury_ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles F_Treasury_ComboBox.SelectedIndexChanged
        On Error Resume Next
        F_Treasury_Balance.Text = Show_TR_T_Balance(F_Treasury_ComboBox.SelectedValue)
    End Sub

    Private Sub Select_Tran_MAX()

        Dim C As New C
        Dim S As String = "select (isnull(max(Tran_ID),0) + 1) AS N from Treasury_Balance_MV"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                ReceiptNum_Txt.Text = C.Dr("N")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()
    End Sub


    Private Sub DateTimeReceipt_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimeReceipt.KeyDown
        If e.KeyCode = Keys.Return Then
            F_Treasury_ComboBox.DroppedDown = True
            F_Treasury_ComboBox.Select()
        End If
    End Sub

    Private Sub Tr_Deposit_Withdraw_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then If new_butt.Enabled = True Then new_butt_Click(sender, e)
        If e.KeyCode = Keys.F2 Then If save_butt.Enabled = True Then save_butt_Click(sender, e)
    End Sub

    Private Sub Tr_Deposit_Withdraw_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If U_Tr_Convert = False Then
            MsgBox("خارج صلاحياتك", MsgBoxStyle.Critical, "صلاحية المستخدم")
            Me.Close()
        End If

        Load_Data()
    End Sub

    Private Sub T_Treasury_ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles T_Treasury_ComboBox.SelectedIndexChanged
        On Error Resume Next
        T_Treasury_Balance.Text = Show_TR_T_Balance(T_Treasury_ComboBox.SelectedValue)
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub F_Treasury_ComboBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles F_Treasury_ComboBox.SelectedValueChanged
        ReDescription_txtb.Text = " تحويل من  " + F_Treasury_ComboBox.Text + " إلى " + T_Treasury_ComboBox.Text
        check_hide_tr(F_Treasury_ComboBox, F_Treasury_Balance)
    End Sub

    Private Sub T_Treasury_ComboBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles T_Treasury_ComboBox.SelectedValueChanged
        ReDescription_txtb.Text = " تحويل من  " + F_Treasury_ComboBox.Text + " إلى " + T_Treasury_ComboBox.Text
        check_hide_tr(T_Treasury_ComboBox, T_Treasury_Balance)
    End Sub




    Private Sub Select_Tr_Balance_Transfer()

        Dim C As New C
        Dim S As String = "Select * From Treasury_BalanceMV_Transfer_V Where [Tran_ID] = '" & ReceiptNum_Txt.Text & "'"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()


                Tran_ID = C.Dr("Tran_ID")
                DateTimeReceipt.Value = C.Dr("date")
                F_Treasury_ComboBox.SelectedValue = C.Dr("Tr_ID")
                money_num_txtb.Text = C.Dr("Debit")
                ReDescription_txtb.Text = C.Dr("notice_move")
                User_Name = C.Dr("UserName")

                C.Dr.Read()
                T_Treasury_ComboBox.SelectedValue = C.Dr("Tr_ID")
                Fields_Panel.Enabled = False

                If C.Dr("isVoid") = True Then
                    After_Void_Receipt()
                Else
                    After_SELECT_Receipt()
                End If

            Else
                MsgBox("لم يتم التعرف على رقم التحويل", MsgBoxStyle.Exclamation)
                ReceiptNum_Txt.Text = Tran_ID
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()


    End Sub


    Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
        ReceiptNum_Txt.Text = Convert.ToInt64(ReceiptNum_Txt.Text) - 1
        Select_Tr_Balance_Transfer()
    End Sub

    Private Sub Up_Bill_btn_Click(sender As Object, e As EventArgs) Handles Up_Bill_btn.Click
        ReceiptNum_Txt.Text = Convert.ToInt64(ReceiptNum_Txt.Text) + 1
        Select_Tr_Balance_Transfer()
    End Sub

    Private Sub ReceiptNum_Txt_KeyDown(sender As Object, e As KeyEventArgs) Handles ReceiptNum_Txt.KeyDown
        If e.KeyCode = Keys.Return Then Select_Tr_Balance_Transfer()
    End Sub

    Private Sub ReceiptNum_Txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ReceiptNum_Txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub Cancel_Btn_Click(sender As Object, e As EventArgs) Handles Cancel_Btn.Click
        If MessageBox.Show(" إلغاء التحويل بشكل نهائي ", "", MessageBoxButtons.OKCancel, _
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then Treasury_BalanceMV_isVoid()
    End Sub

    Public Sub Treasury_BalanceMV_isVoid()
        Dim sqlComm As New SqlClient.SqlCommand()
        sqlComm.CommandText = "[Treasury_BalanceMV_Trans_isVoid]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@Tran_ID", Tran_ID)

        If SQL_SP_EXEC(sqlComm) = True Then
            Network_Edit_Tracker_insert(" من حساب الخزينة:" & F_Treasury_ComboBox.Text & " إلى حساب الخزينة:" & T_Treasury_ComboBox.Text & " القيمة:" & money_num_txtb.Text, Me.ReceiptNum_Txt.Text, 26, 2)
            After_Void_Receipt()

            F_Treasury_Balance.Text = Show_TR_T_Balance(F_Treasury_ComboBox.SelectedValue)
            T_Treasury_Balance.Text = Show_TR_T_Balance(T_Treasury_ComboBox.SelectedValue)

        End If
    End Sub
End Class