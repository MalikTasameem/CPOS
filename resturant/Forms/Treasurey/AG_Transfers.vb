Public Class AG_Transfers : Inherits Windows.Forms.Form

    Dim T_ID As Integer
    Dim User_Name As String
    Public is_Select As Boolean = False

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
    Private drag As Boolean
    Private mouseX As Integer
    Private mouseY As Integer

    Private Sub TitleBar_Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseDown, TopTitle_LB.MouseDown
        drag = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub TitleBar_Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseMove, TopTitle_LB.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub

    Private Sub TitleBar_Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseUp, TopTitle_LB.MouseUp
        drag = False
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

        F_Treasury_ComboBox.Textt = ""
        T_Treasury_ComboBox.Textt = ""

        DateTimeReceipt.Text = Date.Now
        Select_Tran_MAX()
    End Sub

    'Private Sub Load_Data()

    '    Dim C As New C
    '    Dim C2 As New C
    '    Try
    '        Dim sql As String = "Select AG_ID,Ag_name FROM AGENTS_MENU_V "
    '        C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
    '        C.Da.Fill(C.Dt)
    '        C.Da.Fill(C2.Dt)

    '        F_Treasury_ComboBox.DataSource = C.Dt
    '        F_Treasury_ComboBox.DisplayMember = "Ag_name"
    '        F_Treasury_ComboBox.ValueMember = "AG_ID"
    '        F_Treasury_Balance.Text = Show_AG_T_Balance(F_Treasury_ComboBox.SelectedValue)

    '        T_Treasury_ComboBox.DataSource = C2.Dt
    '        T_Treasury_ComboBox.DisplayMember = "Ag_name"
    '        T_Treasury_ComboBox.ValueMember = "AG_ID"
    '        T_Treasury_Balance.Text = Show_AG_T_Balance(T_Treasury_ComboBox.SelectedValue)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub


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

        If F_Treasury_ComboBox.TXT_ID.Text = T_Treasury_ComboBox.TXT_ID.Text Then

            MsgBox("لا يمكن نقل قيمة إلى نفس الحساب", MsgBoxStyle.Critical, "خطأ فالحفظ")
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

        sqlComm.CommandText = "[AG_MV_TRANSACTION_pros]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", 0)
        sqlComm.Parameters.AddWithValue("Date", Me.DateTimeReceipt.Value)
        sqlComm.Parameters.AddWithValue("@AG_ID_FROM", F_Treasury_ComboBox.TXT_ID.Text)
        sqlComm.Parameters.AddWithValue("@AG_ID_TO", T_Treasury_ComboBox.TXT_ID.Text)
        sqlComm.Parameters.AddWithValue("@Value", Me.money_num_txtb.Text)
        sqlComm.Parameters.AddWithValue("@TITLE", ReDescription_txtb.Text)
        sqlComm.Parameters.AddWithValue("@ABOUT ", "")
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters.AddWithValue("@Process", "")

        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تم الإضافة بنجاح", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" من حساب :" & F_Treasury_ComboBox.Textt & " إلى حساب :" & T_Treasury_ComboBox.Textt & " القيمة:" & money_num_txtb.Text, Me.ReceiptNum_Txt.Text, 40, 1)
            Me.ReceiptNum_Txt.Text = sqlComm.Parameters("@T_ID").Value.ToString()

            F_Treasury_Balance.Text = Show_AG_T_Balance(F_Treasury_ComboBox.TXT_ID.Text)
            T_Treasury_Balance.Text = Show_AG_T_Balance(T_Treasury_ComboBox.TXT_ID.Text)
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
                .rp.SetParameterValue(7, F_Treasury_ComboBox.Textt)
                .rp.SetParameterValue(8, T_Treasury_ComboBox.Textt)
                .rp.SetParameterValue(9, "إذن تحويل بين الحسابات")


                If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_A4))
                .rp.PrintOptions.PrinterName = Default_Printer_A4
                .rp.PrintToPrinter(1, False, 0, 0)
                .rp.Dispose()

                'Dim p As New print
                'p.CrystalReportViewer1.ReportSource = pp.rp
                'p.Show()

            End With


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub Tr_Deposit_Withdraw_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub




    Private Sub Select_Tran_MAX()

        Dim C As New C
        Dim S As String = "select (isnull(max(T_ID),0) + 1) AS N from AG_MV_TRANSACTION"
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


    'Private Sub DateTimeReceipt_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimeReceipt.KeyDown
    '    If e.KeyCode = Keys.Return Then
    '        F_Treasury_ComboBox.DroppedDown = True
    '        F_Treasury_ComboBox.Select()
    '    End If
    'End Sub

    Private Sub Tr_Deposit_Withdraw_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then If new_butt.Enabled = True Then new_butt_Click(sender, e)
        If e.KeyCode = Keys.F2 Then If save_butt.Enabled = True Then save_butt_Click(sender, e)
    End Sub

    Private Sub Tr_Deposit_Withdraw_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If U_Tr_Convert = False Then
            MsgBox("خارج صلاحياتك", MsgBoxStyle.Critical, "صلاحية المستخدم")
            Me.Close()
        End If

        'Load_Data()

        If is_Select = True Then
            ReceiptNum_Txt.Text = F_Balances.AGMVMetroGrid.CurrentRow.Cells("T_ID_CL").Value
            Select_Tr_Balance_Transfer()
        End If

    End Sub



    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub






    Private Sub Select_Tr_Balance_Transfer()

        Dim C As New C
        Dim S As String = "Select * From AG_MV_TRANSACTION_V Where [T_ID] = '" & ReceiptNum_Txt.Text & "'"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()


                T_ID = C.Dr("T_ID")
                DateTimeReceipt.Value = C.Dr("date")
                F_Treasury_ComboBox.Set_IM_By_ID(C.Dr("AG_ID_FROM"))
                T_Treasury_ComboBox.Set_IM_By_ID(C.Dr("AG_ID_TO"))
                money_num_txtb.Text = C.Dr("VALUE")
                ReDescription_txtb.Text = C.Dr("TITLE")
                User_Name = C.Dr("UserName")

                Fields_Panel.Enabled = False

                If C.Dr("is_Void") = 1 Then
                    After_Void_Receipt()
                Else
                    After_SELECT_Receipt()
                End If

            Else
                MsgBox("لم يتم التعرف على رقم التحويل", MsgBoxStyle.Exclamation)
                ReceiptNum_Txt.Text = T_ID
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
        sqlComm.CommandText = "[AG_MV_TRANSACTION_pros]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)

        sqlComm.Parameters.AddWithValue("Date", Me.DateTimeReceipt.Value)
        sqlComm.Parameters.AddWithValue("@AG_ID_FROM", F_Treasury_ComboBox.TXT_ID.Text)
        sqlComm.Parameters.AddWithValue("@AG_ID_TO", T_Treasury_ComboBox.TXT_ID.Text)
        sqlComm.Parameters.AddWithValue("@Value", Me.money_num_txtb.Text)
        sqlComm.Parameters.AddWithValue("@TITLE", ReDescription_txtb.Text)
        sqlComm.Parameters.AddWithValue("@ABOUT ", "")
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)


        sqlComm.Parameters.AddWithValue("@Process", "DELETE")
        If SQL_SP_EXEC(sqlComm) = True Then
            Network_Edit_Tracker_insert(" من حساب :" & F_Treasury_ComboBox.Textt & " إلى حساب :" & T_Treasury_ComboBox.Textt & " القيمة:" & money_num_txtb.Text, Me.ReceiptNum_Txt.Text, 40, 2)
            After_Void_Receipt()

            F_Treasury_Balance.Text = Show_AG_T_Balance(F_Treasury_ComboBox.TXT_ID.Text)
            T_Treasury_Balance.Text = Show_AG_T_Balance(T_Treasury_ComboBox.TXT_ID.Text)

        End If
    End Sub


    'Private Sub F_Treasury_ComboBox_SelectedValueChanged(sender As Object, e As EventArgs)

    '    '   check_hide_tr(F_Treasury_ComboBox, F_Treasury_Balance)
    'End Sub

    'Private Sub T_Treasury_ComboBox_SelectedValueChanged(sender As Object, e As EventArgs)

    '    ' check_hide_tr(T_Treasury_ComboBox, T_Treasury_Balance)
    'End Sub

    'Private Sub Treasury_ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    On Error Resume Next

    'End Sub

    'Private Sub T_Treasury_ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    On Error Resume Next

    'End Sub

    Private Sub F_Treasury_ComboBox_ID_Changed(sender As Object, e As EventArgs) Handles F_Treasury_ComboBox.ID_Changed
        If F_Treasury_ComboBox.TXT_ID.Text > 0 Then
            ReDescription_txtb.Text = " تحويل من حساب  " + F_Treasury_ComboBox.Textt + " إلى " + T_Treasury_ComboBox.Textt
            F_Treasury_Balance.Text = Show_AG_T_Balance(F_Treasury_ComboBox.TXT_ID.Text)
        Else
            F_Treasury_Balance.Clear()
        End If

    End Sub

    Private Sub T_Treasury_ComboBox_ID_Changed(sender As Object, e As EventArgs) Handles T_Treasury_ComboBox.ID_Changed
        If T_Treasury_ComboBox.TXT_ID.Text > 0 Then
            ReDescription_txtb.Text = " تحويل من حساب  " + F_Treasury_ComboBox.Textt + " إلى " + T_Treasury_ComboBox.Textt
            T_Treasury_Balance.Text = Show_AG_T_Balance(T_Treasury_ComboBox.TXT_ID.Text)
        Else
            T_Treasury_Balance.Clear()
        End If

    End Sub

    Private Sub HeaderCloseBtn_Click(sender As Object, e As EventArgs) Handles HeaderCloseBtn.Click
        Me.Close()
    End Sub
End Class