Public Class Receipt


    Dim ReceiptNum As Integer = 0
    Public AG_Type As Integer = 3

    'Dim Tr_B As Double = 0
    Private Sub new_butt_Click(sender As Object, e As EventArgs) Handles new_butt.Click
        Make_New_Receipt()
    End Sub

    Dim KEY_HANDLER As String

    Private Sub Make_New_Receipt()
        Fields_Panel.Enabled = True
        ClearFields()
        save_butt.Enabled = True
        print_butt.Enabled = False
        Me.BackColor = SystemColors.Control
        Get_MAX_T_ID()
        Currency_Equal_txt.Enabled = True
        Prepare_to_add()
        payment_Type_combo.SelectedIndex = -1
    End Sub

    Private Sub After_Save_Receipt()
        Fields_Panel.Enabled = False
        save_butt.Enabled = False
        print_butt.Enabled = True
    End Sub


    Public Sub ClearFields()
        For Each a As Control In Fields_Panel.Controls
            If TypeOf a Is TextBox Then
                a.Text = Nothing
            End If
        Next

        AG_Cm.SelectedIndex = -1
        Current_QTY.Text = Nothing
        payment_Type_combo.SelectedIndex = 0
        Receipt_Title_txt.Clear()
        Receipt_Title_txt.Text = ""
        bankName_Combo.SelectedIndex = -1
        DateTimeReceipt.Text = Date.Now
        'On_Update = False
        ReceiptTypeComboBox.SelectedValue = AG_Type
        Receipt_Tran_ID = 0
        Treasury_ComboBox.SelectedIndex = -1
        Treasury_Balance.Clear()
    End Sub

    Private Sub Load_Data()

        Dim C As New C
        Try
            Dim sql As String = " Select 3 AS id ,'سنـــد قبض' AS Type_Name UNION ALL Select 4 AS id ,'سنـــد صرف' AS Type_Name  "
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            ReceiptTypeComboBox.DataSource = C.Dt
            ReceiptTypeComboBox.DisplayMember = "Type_Name"
            ReceiptTypeComboBox.ValueMember = "id"
            ReceiptTypeComboBox.SelectedValue = AG_Type
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        AG_Cm.DataSource = Agents_Datatable
        AG_Cm.DisplayMember = "ACC_NAME"
        AG_Cm.ValueMember = "ACC_CODE"

        'C = New C
        'Try
        '    Dim sql As String = " SELECT [ACC_CODE],[ACC_NAME] FROM  ACCOUNTS_TREE WHERE ACC_PARENT IN (  SELECT [ACC_CODE]  FROM [dbo].[Rct_Mang_V] WHERE  ACC_Type = 1 ) "
        '    C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
        '    C.Da.Fill(C.Dt)
        '    AG_Cm.DataSource = C.Dt
        '    AG_Cm.DisplayMember = "ACC_NAME"
        '    AG_Cm.ValueMember = "ACC_CODE"
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try


        'C = New C
        'Try
        '    Dim sql As String = "  SELECT [ACC_CODE],[ACC_NAME] FROM  ACCOUNTS_TREE WHERE ACC_PARENT IN (  SELECT [ACC_CODE]  FROM [dbo].[Rct_Mang_V] WHERE  ACC_Type = 2 )  "
        '    C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
        '    C.Da.Fill(C.Dt)
        '    Treasury_ComboBox.DataSource = C.Dt
        '    Treasury_ComboBox.DisplayMember = "ACC_NAME"
        '    Treasury_ComboBox.ValueMember = "ACC_CODE"
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

        Treasury_ComboBox.DataSource = Treasury_Datatable
        Treasury_ComboBox.DisplayMember = "ACC_NAME"
        Treasury_ComboBox.ValueMember = "ACC_CODE"

        C = New C
        Try
            Dim sql As String = "Select Distinct Bank_Name from ACC_BALANCE_MASTER "
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            bankName_Combo.DataSource = C.Dt
            bankName_Combo.DisplayMember = "Bank_Name"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        COST_CM.DataSource = CostCenter_Datatable
        COST_CM.DisplayMember = "COST_NAME"
        COST_CM.ValueMember = "COST_ID"



        Currency_Cm.DataSource = Currencies_Datatable
        Currency_Cm.DisplayMember = "Cr_Name"
        Currency_Cm.ValueMember = "Cr_ID"

        '  If isShowing_Trans = True Then Select_Receipt(T_ID_Trans)


    End Sub

    Private Sub Currency_Cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles Currency_Cm.SelectedValueChanged
        If Currency_Cm.SelectedValue IsNot Nothing AndAlso Not TypeOf Currency_Cm.SelectedValue Is System.Data.DataRowView Then
            Currency_Equal_txt.Text = Get_Equal(Currency_Cm.SelectedValue, DateTimeReceipt, ReceiptTypeComboBox.SelectedValue)
        End If
    End Sub


    ' Public Sub Select_Receipt(T_ID As Integer)
    'Dim C As New C
    'Dim S As String = "Select * From Receipts_V Where T_ID = '" & T_ID & "'"
    'C.Com = New SqlClient.SqlCommand(S, C.Con)
    'C.Con.Open()
    'Try
    '    C.Dr = C.Com.ExecuteReader
    '    If C.Dr.HasRows Then
    '        C.Dr.Read()
    '        With Me
    '            Reciept_T_ID = T_ID
    '            Receipt_Tran_ID = C.Dr("Receipt_Tran_ID")
    '            ReceiptNum_Txt.Text = S_Sub_Code & (C.Dr("Receipt_Num")) ' - START_ID).ToString
    '            ReceiptNum = C.Dr("Receipt_Num")
    '            AG_Cm.Set_IM_By_ID(C.Dr("AG_ID"))

    '            .ReceiptTypeComboBox.SelectedValue = C.Dr("BsType_ID")
    '            .DateTimeReceipt.Text = C.Dr("Date")
    '            .Receipt_Title_combobox.Text = C.Dr("Receipt_Title")
    '            .Notes_txtb.Text = C.Dr("About")
    '            .Treasury_ComboBox.SelectedValue = C.Dr("Tr_ID")
    '            '.payment_Type_combo.SelectedValue = C.Dr("Pay_ID")

    '            If IsDBNull(C.Dr("Bank_Name")) = True Then
    '                .payment_Type_combo.SelectedIndex = 0
    '            Else
    '                If String.IsNullOrWhiteSpace(C.Dr("Bank_Name")) = True Then
    '                    .payment_Type_combo.SelectedIndex = 0
    '                Else
    '                    .payment_Type_combo.SelectedIndex = 1
    '                    .bankName_Combo.Text = C.Dr("Bank_Name")
    '                    .CheckNum_txtb.Text = C.Dr("CheckNum")
    '                End If

    '            End If



    '            .money_num_txtb.Text = C.Dr("Value")
    '            '/ C.Dr("Cr_Equal_Value")
    '            If Convert.ToDouble(.money_num_txtb.Text) < 0 Then .money_num_txtb.Text = .money_num_txtb.Text * -1

    '            If C.Dr("isVoid") = 1 Then
    '                Void_Lb.Visible = True
    '                .Fields_Panel.Enabled = False
    '                .new_butt.Enabled = False
    '                .save_butt.Enabled = False
    '                .print_butt.Enabled = False
    '                .DeleteButton.Enabled = False
    '                Edit_butt.Enabled = False

    '            Else
    '                Void_Lb.Visible = False
    '                .Fields_Panel.Enabled = False
    '                .new_butt.Enabled = False
    '                .save_butt.Enabled = False
    '                .print_butt.Enabled = True
    '                Edit_butt.Enabled = True
    '                DeleteButton.Enabled = True
    '            End If

    '            '     Load_AG_Balance()

    '        End With

    '        ' Else
    '        '  MsgBox("لم يتم التعرف على رقم الإيصال", MsgBoxStyle.Exclamation)
    '    End If

    'Catch ex As Exception
    '    MsgBox(ex.Message)
    'End Try
    'C.Con.Close()
    '  End Sub

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
        Me.money_char_txtb.Text = HANY(Val(money_num_txtb.Text), Get_Currency_Tag(Currency_Cm.SelectedValue))
    End Sub

    Private Sub payment_Type_combo_KeyDown(sender As Object, e As KeyEventArgs) Handles payment_Type_combo.KeyDown
        If e.KeyCode = Keys.Return Then
            If payment_Type_combo.SelectedIndex = 1 Then
                bankName_Combo.Select()
                bankName_Combo.DroppedDown = True
            Else
                save_butt.Select()
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

        If AG_Cm.SelectedValue <= 0 Then
            MsgBox("الرجاء التأكد من صحة إسم الحساب", MsgBoxStyle.Exclamation, "خطأ فالحفظ")
            AG_Cm.Focus()
            Return 0
        End If

        If Treasury_ComboBox.SelectedValue <= 0 Then
            MsgBox("الرجاء التأكد من صحة إسم الخزينة", MsgBoxStyle.Exclamation, "خطأ فالحفظ")
            Treasury_ComboBox.Focus()
            Return 0
        End If

        If Not ValidateManualJournalAccount(AG_Cm.SelectedValue, "حساب الطرف") Then
            AG_Cm.Focus()
            Return 0
        End If

        If Not ValidateManualJournalAccount(Treasury_ComboBox.SelectedValue, "حساب الخزينة/المصرف") Then
            Treasury_ComboBox.Focus()
            Return 0
        End If

        If Not ValidateUserJournalAccountPermission(AG_Cm.SelectedValue, "حساب الطرف") Then
            AG_Cm.Focus()
            Return 0
        End If

        If Not ValidateUserJournalAccountPermission(Treasury_ComboBox.SelectedValue, "حساب الخزينة/المصرف") Then
            Treasury_ComboBox.Focus()
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

        If payment_Type_combo.SelectedIndex = -1 Then
            MsgBox("الرجاء إدخال طريقة الدفع", MsgBoxStyle.Critical, "خطأ فالحفظ")
            payment_Type_combo.Select()
            payment_Type_combo.DroppedDown = True
            Return 0
        End If

        If CheckNum_txtb.Enabled = True Then

            If String.IsNullOrWhiteSpace(CheckNum_txtb.Text) Then
                MsgBox("الرجاء إدخال رقم الشيك", MsgBoxStyle.Critical, "خطأ فالحفظ")
                CheckNum_txtb.Select()
                Return 0
            End If
        End If




        Return 1

    End Function


    Public Sub Receipt_Insert()

        Dim C As New C


        With C.Com
            .Connection = C.Con
            .CommandText = "[ACC_BALANCE_proc_Receipt]"
            .CommandType = CommandType.StoredProcedure

            .Parameters.AddWithValue("@DATE", DateTimeReceipt.Value)
            .Parameters.AddWithValue("@ACC_CODE_FROM", AG_Cm.SelectedValue)
            .Parameters.AddWithValue("@ACC_CODE_TO", Treasury_ComboBox.SelectedValue)

            .Parameters.AddWithValue("@DEBIT", Convert.ToDouble(money_num_txtb.Text) * Convert.ToDouble(Currency_Equal_txt.Text))
            .Parameters.AddWithValue("@CREDIT", Convert.ToDouble(money_num_txtb.Text) * Convert.ToDouble(Currency_Equal_txt.Text))
            .Parameters.AddWithValue("@USER_ID", USER_ID)
            .Parameters.AddWithValue("@Notes_MASTER",
    If(String.IsNullOrWhiteSpace(CheckNum_txtb.Text),
       Receipt_Title_txt.Text,
       Receipt_Title_txt.Text & "/" & Label_check_num.Text & " : " & CheckNum_txtb.Text))
            '.Parameters.AddWithValue("@Notes_MASTER", Receipt_Title_txt.Text & "/" & CheckNum_txtb.Text)
            .Parameters.AddWithValue("@COST_ID", COST_CM.SelectedValue)
            .Parameters.Add("@ERROR_MSG", SqlDbType.NVarChar, 500)
            .Parameters.AddWithValue("@OP_Status", 1)

            .Parameters.AddWithValue("@Receipt_Type", ReceiptTypeComboBox.SelectedValue)
            .Parameters.AddWithValue("@Receipt_Num", ReceiptNum_Txt.Text)
            .Parameters.AddWithValue("@Bank_Name", bankName_Combo.Text)

            If Not String.IsNullOrWhiteSpace(CheckNum_txtb.Text) Then .Parameters.AddWithValue("@Check_Number", CheckNum_txtb.Text)

            .Parameters.AddWithValue("@B_T_ID", 0)

            '.Parameters.AddWithValue("@IS_VOID", 0)
            '.Parameters.AddWithValue("@Currency", 1)
            '.Parameters.AddWithValue("@Notes", Notes_txt.Text)

            '.Parameters.AddWithValue("@Process", Process)
            '.Parameters.AddWithValue("@Bill_Num", Bill_Num_txt.Text)

            .Parameters.AddWithValue("@Cr_ID", Currency_Cm.SelectedValue)
            .Parameters.AddWithValue("@Currency_Equal", Currency_Equal_txt.Text)


            .Parameters("@OP_Status").Direction = ParameterDirection.Output
            .Parameters("@ERROR_MSG").Direction = ParameterDirection.Output

            C.Con.Open()
            ReceiptNum_Txt.Text = C.Com.ExecuteScalar()
            C.Con.Close()


            If C.Com.Parameters("@OP_Status").Value.ToString() = "0" Then
                MsgBox(C.Com.Parameters("@ERROR_MSG").Value.ToString(), MsgBoxStyle.Critical, "خطــأ")
            Else
                MsgBox(" تم حفظ الإيصال بنجاح ", MsgBoxStyle.Information, "")
                print_butt.Enabled = True
                Fields_Panel.Enabled = False

                'Current_QTY.Text = Show_Balance(AG_Cm.SelectedValue)
                'Treasury_Balance.Text = Show_Balance(Treasury_ComboBox.SelectedValue)

            End If

        End With


        '---------------------------------------------------------------------------------------------------
        'Dim sqlComm As New SqlClient.SqlCommand()

        'sqlComm.CommandText = "Agents_BalanceMV_insert_RCT"
        'sqlComm.Parameters.AddWithValue("@Receipt_Num", 0)

        'sqlComm.CommandType = CommandType.StoredProcedure
        'sqlComm.Parameters.AddWithValue("@T_ID", 0)

        'If S_Pr = True And Pr_ID > 0 Then sqlComm.Parameters.AddWithValue("@Pr_ID", Pr_ID)
        'sqlComm.Parameters.AddWithValue("@Receipt_Tran_ID", Receipt_Tran_ID)

        'sqlComm.Parameters.AddWithValue("@AG_ID", Me.AG_ID)
        'sqlComm.Parameters.AddWithValue("Date", Me.DateTimeReceipt.Value)
        'sqlComm.Parameters.AddWithValue("@Receipt_Title", Me.Receipt_Title_combobox.Text)

        'sqlComm.Parameters.AddWithValue("@Pure", Convert.ToDouble(Me.money_num_txtb.Text))

        'sqlComm.Parameters.AddWithValue("@About", Notes_txtb.Text)
        'sqlComm.Parameters.AddWithValue("@BsType_ID", Me.ReceiptTypeComboBox.SelectedValue)

        'If payment_Type_combo.SelectedIndex = 1 Then
        '    sqlComm.Parameters.AddWithValue("@Bank_Name", bankName_Combo.Text)
        '    sqlComm.Parameters.AddWithValue("@CheckNum", CheckNum_txtb.Text)
        'End If
        'sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        'sqlComm.Parameters.AddWithValue("@Tr_ID", Treasury_ComboBox.SelectedValue)

        'sqlComm.Parameters("@Receipt_Num").Direction = ParameterDirection.Output
        'sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

        'If SQL_SP_EXEC(sqlComm) = True Then

        '    MsgBox("تم حفظ الإيصــال", MsgBoxStyle.Information)
        '    Reciept_T_ID = sqlComm.Parameters("@T_ID").Value.ToString()
        '    ReceiptNum = sqlComm.Parameters("@Receipt_Num").Value.ToString()


        '    Current_QTY.Text = Show_AG_T_Balance(AG_ID)
        '    Is_ComandSuccess = True

        '    ReceiptNum_Txt.Text = sqlComm.Parameters("@Receipt_Num").Value.ToString()



        '    Treasury_Balance.Text = Show_TR_T_Balance(Treasury_ComboBox.SelectedValue)
        '    After_Save_Receipt()


        'End If

    End Sub


    'Public Sub Receipt_UPDATE()

    '    Dim sqlComm As New SqlClient.SqlCommand()

    '    sqlComm.CommandText = "[Agents_BalanceMV_Update_RCT]"
    '    'sqlComm.Parameters.AddWithValue("@Receipt_Num", ReceiptNum)

    '    sqlComm.CommandType = CommandType.StoredProcedure
    '    'sqlComm.Parameters.AddWithValue("@Prev_T_ID", Reciept_T_ID)
    '    sqlComm.Parameters.AddWithValue("@T_ID", Reciept_T_ID)

    '    If S_Pr = True And Pr_ID > 0 Then sqlComm.Parameters.AddWithValue("@Pr_ID", Pr_ID)
    '    sqlComm.Parameters.AddWithValue("@Receipt_Tran_ID", Receipt_Tran_ID)

    '    sqlComm.Parameters.AddWithValue("@AG_ID", Me.AG_ID)
    '    sqlComm.Parameters.AddWithValue("Date", Me.DateTimeReceipt.Value)
    '    sqlComm.Parameters.AddWithValue("@Receipt_Title", Me.Receipt_Title_combobox.Text)

    '    sqlComm.Parameters.AddWithValue("@Pure", Convert.ToDouble(Me.money_num_txtb.Text))

    '    sqlComm.Parameters.AddWithValue("@About", Notes_txtb.Text)
    '    sqlComm.Parameters.AddWithValue("@BsType_ID", Me.ReceiptTypeComboBox.SelectedValue)
    '    If payment_Type_combo.SelectedIndex = 1 Then
    '        sqlComm.Parameters.AddWithValue("@Bank_Name", bankName_Combo.Text)
    '        sqlComm.Parameters.AddWithValue("@CheckNum", CheckNum_txtb.Text)
    '    End If
    '    sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
    '    sqlComm.Parameters.AddWithValue("@Tr_ID", Treasury_ComboBox.SelectedValue)
    '    '  sqlComm.Parameters.AddWithValue("@Pay_ID", payment_Type_combo.SelectedValue)

    '    If SQL_SP_EXEC(sqlComm) = True Then

    '        MsgBox("تم حفظ الإيصــال", MsgBoxStyle.Information)

    '        Current_QTY.Text = Show_AG_T_Balance(AG_ID)
    '        Is_ComandSuccess = True
    '        ReceiptNum_Txt.Text = ReceiptNum
    '        Network_Edit_Tracker_insert(" إيصال للحساب " & AG_Cm.Textt & " بقيمة : " & money_num_txtb.Text, ReceiptNum_Txt.Text, AG_Type, 3)

    '        If FormType = 12 Then If Application.OpenForms().OfType(Of Custody).Any Then F_Custody.Custody_CLOSE()
    '        If FormType = 1 Then If Application.OpenForms().OfType(Of Sales).Any Then Select_Sales_Receipt(F_Sales.T_ID)
    '        If FormType = 2 Then If Application.OpenForms().OfType(Of Pch).Any Then Select_Pch_Receipt(F_Pch.T_ID)
    '        If FormType = 8 Then If Application.OpenForms().OfType(Of EXP_Details).Any Then Select_EX_Receipt(F_EXP_Details.T_ID)

    '        Treasury_Balance.Text = Show_TR_T_Balance(Treasury_ComboBox.SelectedValue)
    '        After_Save_Receipt()

    '        Edit_butt.Text = EditState
    '        Edit_butt.BackColor = Color.White
    '        On_Update = False

    '        'If Application.OpenForms().OfType(Of SearchAgentBill).Any Then SearchAgentBill.Load_Data()
    '    End If

    'End Sub


    Private Sub print_butt_Click(sender As Object, e As EventArgs) Handles print_butt.Click
        ValidateChildren()
        Print_RECIEPT()

    End Sub

    Public Sub Print_RECIEPT()

        Try
            Dim doc As System.Drawing.Printing.PrintDocument = BuildReceiptPrintDocument()

            If Show_Bill_CB.Checked = True Then
                Using preview As New PrintPreviewDialog()
                    preview.Document = doc
                    preview.WindowState = FormWindowState.Maximized
                    preview.ShowDialog()
                End Using
            Else
                doc.Print()
                doc.Dispose()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Function BuildReceiptPrintDocument() As System.Drawing.Printing.PrintDocument
        Dim doc As New System.Drawing.Printing.PrintDocument()
        Dim isThermal As Boolean = False
        Dim printerName As String = Default_Printer_A4

        doc.DocumentName = "إيصال رقم " & ReceiptNum_Txt.Text

        If Not String.IsNullOrWhiteSpace(printerName) Then
            If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", printerName))
            doc.PrinterSettings.PrinterName = printerName
        End If

        doc.DefaultPageSettings.PaperSize = GetA4PaperSize(doc)
        doc.DefaultPageSettings.Margins = New System.Drawing.Printing.Margins(45, 45, 45, 45)
        doc.DefaultPageSettings.Landscape = False

        AddHandler doc.PrintPage,
            Sub(sender, e)
                DrawReceiptPrintDocument(e, isThermal)
            End Sub

        Return doc
    End Function

    Private Function GetA4PaperSize(doc As System.Drawing.Printing.PrintDocument) As System.Drawing.Printing.PaperSize
        For Each paper As System.Drawing.Printing.PaperSize In doc.PrinterSettings.PaperSizes
            If paper.Kind = System.Drawing.Printing.PaperKind.A4 Then Return paper
        Next

        Return New System.Drawing.Printing.PaperSize("A4", 827, 1169)
    End Function

    Private Sub DrawReceiptPrintDocument(e As System.Drawing.Printing.PrintPageEventArgs, isThermal As Boolean)
        Dim g As Graphics = e.Graphics
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        Dim bounds As Rectangle = e.MarginBounds
        Dim y As Integer = bounds.Top
        Dim pageW As Integer = bounds.Width

        Using orgFont As New Font("Segoe UI", If(isThermal, 9.0!, 13.0!), FontStyle.Bold),
              titleFont As New Font("Segoe UI", If(isThermal, 11.0!, 18.0!), FontStyle.Bold),
              headFont As New Font("Segoe UI", If(isThermal, 8.0!, 10.5!), FontStyle.Bold),
              bodyFont As New Font("Segoe UI", If(isThermal, 8.0!, 10.5!), FontStyle.Regular),
              amountFont As New Font("Segoe UI", If(isThermal, 11.0!, 18.0!), FontStyle.Bold),
              smallFont As New Font("Segoe UI", If(isThermal, 7.5!, 9.0!), FontStyle.Regular)

            Dim titleText As String = ReceiptTypeComboBox.Text
            Dim receiverLabel As String = GetReceiptPartyLabel()
            Dim checkText As String = If(payment_Type_combo.SelectedIndex = 0,
                                         "--------------------------------",
                                         "شيك رقم : " & CheckNum_txtb.Text)

            DrawCenterText(g, MY_Settings.SBill_Title_1, orgFont, bounds.Left, y, pageW, If(isThermal, 20, 26))
            y += If(isThermal, 20, 26)

            If Not String.IsNullOrWhiteSpace(MY_Settings.SBill_Title_2) Then
                DrawCenterText(g, MY_Settings.SBill_Title_2, smallFont, bounds.Left, y, pageW, If(isThermal, 18, 22))
                y += If(isThermal, 18, 22)
            End If

            g.DrawLine(Pens.Black, bounds.Left, y, bounds.Right, y)
            y += If(isThermal, 8, 14)

            DrawCenterText(g, titleText, titleFont, bounds.Left, y, pageW, If(isThermal, 28, 42))
            y += If(isThermal, 32, 48)

            DrawReceiptInfoRow(g, "رقم الإيصال", ReceiptNum_Txt.Text, "التاريخ", DateTimeReceipt.Value.ToString("dd/MM/yyyy HH:mm"), bounds, y, headFont, bodyFont, isThermal)
            y += If(isThermal, 30, 38)

            DrawReceiptBox(g, receiverLabel, AG_Cm.Text, bounds, y, headFont, bodyFont, If(isThermal, 40, 48))
            y += If(isThermal, 46, 56)

            DrawReceiptBox(g, "البيان", Receipt_Title_txt.Text, bounds, y, headFont, bodyFont, If(isThermal, 46, 60))
            y += If(isThermal, 52, 68)

            DrawReceiptBox(g, "المبلغ", FormatReceiptAmount(), bounds, y, headFont, amountFont, If(isThermal, 40, 56))
            y += If(isThermal, 46, 64)

            DrawReceiptBox(g, "فقط", money_char_txtb.Text, bounds, y, headFont, bodyFont, If(isThermal, 46, 58))
            y += If(isThermal, 52, 66)

            DrawReceiptInfoRow(g, "طريقة الدفع", payment_Type_combo.Text, "تفاصيل", checkText, bounds, y, headFont, bodyFont, isThermal)
            y += If(isThermal, 30, 38)

            DrawReceiptInfoRow(g, "الخزينة", Treasury_ComboBox.Text, "العملة", Currency_Cm.Text, bounds, y, headFont, bodyFont, isThermal)
            y += If(isThermal, 30, 38)

            If AG_Show_Balance_CB.Checked Then
                DrawReceiptBox(g, "رصيد الحساب", Current_QTY.Text, bounds, y, headFont, bodyFont, If(isThermal, 30, 36))
                y += If(isThermal, 36, 44)
            End If

            y += If(isThermal, 8, 18)
            g.DrawLine(Pens.Black, bounds.Left, y, bounds.Right, y)
            y += If(isThermal, 10, 18)

            DrawReceiptSignatures(g, bounds, y, headFont, isThermal)
            y += If(isThermal, 55, 70)

            DrawRightText(g, "المعد: " & USER_NAME, smallFont, bounds.Left, y, pageW, If(isThermal, 18, 22))
            y += If(isThermal, 18, 22)
            DrawRightText(g, "تاريخ الطباعة: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm"), smallFont, bounds.Left, y, pageW, If(isThermal, 18, 22))
        End Using

        e.HasMorePages = False
    End Sub

    Private Function GetReceiptPartyLabel() As String
        If ReceiptTypeComboBox.SelectedValue IsNot Nothing Then
            Dim receiptType As Integer
            If Integer.TryParse(ReceiptTypeComboBox.SelectedValue.ToString(), receiptType) Then
                If receiptType = 2 OrElse receiptType = 4 OrElse receiptType = 5 Then Return "سلمت للسيد"
                If receiptType = 3 OrElse receiptType = 9 Then Return "استلمت من السيد"
            End If
        End If

        Return "الحساب"
    End Function

    Private Function FormatReceiptAmount() As String
        Dim amount As Decimal
        If Decimal.TryParse(money_num_txtb.Text, amount) Then Return amount.ToString("N3")
        Return money_num_txtb.Text
    End Function

    Private Sub DrawReceiptInfoRow(g As Graphics,
                                   rightLabel As String,
                                   rightValue As String,
                                   leftLabel As String,
                                   leftValue As String,
                                   bounds As Rectangle,
                                   y As Integer,
                                   headFont As Font,
                                   bodyFont As Font,
                                   isThermal As Boolean)
        If isThermal Then
            DrawReceiptBox(g, rightLabel, rightValue, bounds, y, headFont, bodyFont, 26)
            Return
        End If

        Dim gap As Integer = 8
        Dim halfW As Integer = CInt((bounds.Width - gap) / 2)
        Dim rightRect As New Rectangle(bounds.Right - halfW, y, halfW, 32)
        Dim leftRect As New Rectangle(bounds.Left, y, halfW, 32)

        DrawReceiptBox(g, rightLabel, rightValue, rightRect, headFont, bodyFont)
        DrawReceiptBox(g, leftLabel, leftValue, leftRect, headFont, bodyFont)
    End Sub

    Private Sub DrawReceiptBox(g As Graphics,
                               labelText As String,
                               valueText As String,
                               bounds As Rectangle,
                               y As Integer,
                               headFont As Font,
                               bodyFont As Font,
                               height As Integer)
        DrawReceiptBox(g, labelText, valueText, New Rectangle(bounds.Left, y, bounds.Width, height), headFont, bodyFont)
    End Sub

    Private Sub DrawReceiptBox(g As Graphics,
                               labelText As String,
                               valueText As String,
                               rect As Rectangle,
                               headFont As Font,
                               bodyFont As Font)
        g.FillRectangle(New SolidBrush(Color.FromArgb(248, 250, 252)), rect)
        g.DrawRectangle(Pens.Black, rect)

        Dim labelW As Integer = Math.Min(105, CInt(rect.Width * 0.32))
        Dim labelRect As New Rectangle(rect.Right - labelW, rect.Top, labelW, rect.Height)
        Dim valueRect As New Rectangle(rect.Left + 4, rect.Top, rect.Width - labelW - 8, rect.Height)

        g.FillRectangle(New SolidBrush(Color.FromArgb(232, 238, 245)), labelRect)
        g.DrawRectangle(Pens.Black, labelRect)
        DrawCenterText(g, labelText, headFont, labelRect.X, labelRect.Y, labelRect.Width, labelRect.Height)
        DrawRightText(g, valueText, bodyFont, valueRect.X, valueRect.Y, valueRect.Width, valueRect.Height)
    End Sub

    Private Sub DrawReceiptSignatures(g As Graphics, bounds As Rectangle, y As Integer, font As Font, isThermal As Boolean)
        If isThermal Then
            DrawRightText(g, "توقيع المستلم: ........................", font, bounds.Left, y, bounds.Width, 24)
            y += 26
            DrawRightText(g, "توقيع الصراف: ........................", font, bounds.Left, y, bounds.Width, 24)
            Return
        End If

        Dim colW As Integer = CInt(bounds.Width / 2)
        DrawCenterText(g, "توقيع المستلم", font, bounds.Left + colW, y, colW, 24)
        DrawCenterText(g, "توقيع الصراف", font, bounds.Left, y, colW, 24)
        y += 34
        DrawCenterText(g, "........................", font, bounds.Left + colW, y, colW, 24)
        DrawCenterText(g, "........................", font, bounds.Left, y, colW, 24)
    End Sub

    Private Sub DrawRightText(g As Graphics, text As String, font As Font, x As Integer, y As Integer, w As Integer, h As Integer)
        Using sf As New StringFormat()
            sf.Alignment = StringAlignment.Far
            sf.LineAlignment = StringAlignment.Center
            sf.Trimming = StringTrimming.EllipsisCharacter
            g.DrawString(If(text, ""), font, Brushes.Black, New RectangleF(x, y, w, h), sf)
        End Using
    End Sub

    Private Sub DrawCenterText(g As Graphics, text As String, font As Font, x As Integer, y As Integer, w As Integer, h As Integer)
        Using sf As New StringFormat()
            sf.Alignment = StringAlignment.Center
            sf.LineAlignment = StringAlignment.Center
            sf.Trimming = StringTrimming.EllipsisCharacter
            g.DrawString(If(text, ""), font, Brushes.Black, New RectangleF(x, y, w, h), sf)
        End Using
    End Sub


    Private Sub Receipt_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'F_MainForm.Fill_ALL_IM()
        Me.Dispose()
    End Sub

    Private Sub Receipt_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then If new_butt.Enabled = True Then new_butt_Click(sender, e)
        If e.KeyCode = Keys.F12 Then If save_butt.Enabled = True Then save_butt_Click(sender, e)
        If e.KeyCode = Keys.Escape Then Me.Close()

        If e.KeyCode = Keys.Return Then
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
        End If
    End Sub

    Private Sub Receipt_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Load_Data()
        Get_MAX_T_ID()
        Title_Lb.Text = "شاشة إدراج : " + ReceiptTypeComboBox.Text
        Title_Lb.BackColor = ReceiptTypeComboBox.BackColor
        ReceiptNum = ReceiptNum_Txt.Text
        AG_Show_Balance_CB.Checked = MY_Settings.AG_Show_Balance_in_Receipt
        edit_labels()
    End Sub

    Private Sub Prepare_to_add()
        If Not String.IsNullOrWhiteSpace(Currency_Equal_txt.Text) Then

            If Convert.ToDouble(Currency_Equal_txt.Text) = 0 Then Currency_Equal_txt.Text = "1"

        Else
            Currency_Equal_txt.Text = "1"
        End If
    End Sub


    Private Sub edit_labels()

        If ReceiptTypeComboBox.SelectedValue = 3 Then
            from_Label.Text = " إلى حساب "
            to_Label.Text = " من خزينة "
        Else
            from_Label.Text = " من حساب "
            to_Label.Text = " إلى خزينة "

            AG_Panel.Location = New Point(5, 160)
            Tr_Panel.Location = New Point(5, 230)
        End If
    End Sub


    Public Sub Get_MAX_T_ID()

        Dim C As New C
        Dim S As String = "Select ISNULL(MAX(Receipt_Num),0) + 1 AS MX From ACC_BALANCE_MASTER"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                If FormType = 0 Then ClearFields()
                ReceiptNum_Txt.Text = C.Dr("MX")
                ReceiptNum = C.Dr("MX")
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
        S = "Select B_T_ID From ACC_BALANCE_V Where Receipt_Num = '" & Convert.ToInt64(ReceiptNum_Txt.Text) & "'"

        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                ClearFields()
                'Select_Receipt(C.Dr("T_ID"))
                B_T_ID_txt.Text = C.Dr("B_T_ID")
                SELECT_Balance(C.Dr("B_T_ID"))
                new_butt.Enabled = True
            Else
                MsgBox("لم يتم التعرف على الإيصال", MsgBoxStyle.Exclamation)
                ReceiptNum_Txt.Text = Tmp_Bill_ID
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()
    End Sub

    Public Sub SELECT_Balance(B_T_ID As Integer)
        'If Not String.IsNullOrWhiteSpace(T_ID_txt_2.Text) Then

        Dim DT As New DataTable

        'Enable_Fields(True)
        'Clear_Fields()


        Dim C As New C

        Dim da As New SqlClient.SqlDataAdapter("SELECT [T_ID],[DATE_IN] ,CONVERT(DATE,DATE) as DATE ,[COST_NAME],[Cr_Name],[Currency_Equal],[MASTER_NOTES],[ACC_CODE] ,[ACC_NAME], " &
                                                " [Bill_Num],[CREDIT],[DEBIT],[Notes],ACC_Depend_Status,is_Depended,COST_ID,Currency_ID,UserName,USER_DEPENDED,Receipt_Num,Receipt_Type,Bank_Name,Check_Number FROM [dbo].[ACC_BALANCE_V] WHERE B_T_ID = " & B_T_ID & " ORDER BY DEBIT asc ", C.Con)
        da.Fill(DT)

        If DT.Rows.Count > 0 Then

            'DataGridView1.DataSource = DT
            'Depended_Label_2.Visible = True

            Receipt_Title_txt.Text = DT(0)("MASTER_NOTES")
            DateTimeReceipt.Text = DT(0)("DATE")
            ReceiptTypeComboBox.SelectedValue = DT(0)("Receipt_Type")
            COST_CM.SelectedValue = DT(0)("COST_ID")

            Currency_Cm.SelectedValue = DT(0)("Currency_ID")
            Currency_Equal_txt.Text = DT(0)("Currency_Equal")

            If Not IsDBNull(DT(0)("Check_Number")) Then
                bankName_Combo.Text = DT(0)("Bank_Name")
                payment_Type_combo.SelectedIndex = 1
                CheckNum_txtb.Text = DT(0)("Check_Number")
            End If

            If DT(0)("Receipt_Type") = 3 Then
                Treasury_ComboBox.SelectedValue = DT(0)("ACC_CODE")

                If DT.Rows.Count > 1 Then
                    money_num_txtb.Text = DT(1)("DEBIT") / DT(0)("Currency_Equal")
                    AG_Cm.SelectedValue = DT(1)("ACC_CODE")
                End If


            ElseIf DT(0)("Receipt_Type") = 4 Then
                If DT.Rows.Count > 1 Then
                    Treasury_ComboBox.SelectedValue = DT(1)("ACC_CODE")
                End If
                money_num_txtb.Text = DT(0)("CREDIT") / DT(0)("Currency_Equal")
                AG_Cm.SelectedValue = DT(0)("ACC_CODE")
            End If

            ReceiptNum_Txt.Text = DT(0)("Receipt_Num")
            ReceiptNum = DT(0)("Receipt_Num")




            Fields_Panel.Enabled = False
            print_butt.Enabled = True

        End If

    End Sub

    Private Sub Up_Bill_btn_Click(sender As Object, e As EventArgs) Handles Up_Bill_btn.Click
        If Not String.IsNullOrWhiteSpace(ReceiptNum_Txt.Text) Then
            Tmp_Bill_ID = ReceiptNum
            ReceiptNum_Txt.Text = ReceiptNum + 1
            Get_T_ID()
        End If
    End Sub


    Public Sub Agents_Balance_MV_RCT_VOID(T_ID As Integer)
        'Dim c As New C
        'With c.Com
        '    .Connection = c.Con
        '    .CommandText = "[Agents_Balance_MV_RCT_VOID]"
        '    .CommandType = CommandType.StoredProcedure
        '    .Parameters.AddWithValue("@T_ID", T_ID)
        'End With
        'If SQL_SP_EXEC(c.Com) = True Then
        '    MsgBox("تم إلغــاء المعاملة ", MsgBoxStyle.Information)
        'End If

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

    'Private Sub Notes_txtb_KeyDown(sender As Object, e As KeyEventArgs) Handles Notes_txtb.KeyDown
    '    If e.KeyCode = Keys.Return Then If save_butt.Enabled = True Then save_butt_Click(sender, e)
    'End Sub

    Private Sub Treasury_ComboBox_KeyDown(sender As Object, e As KeyEventArgs) Handles Treasury_ComboBox.KeyDown
        If e.KeyCode = Keys.Return Then
            payment_Type_combo.Select()
            payment_Type_combo.DroppedDown = True
        End If
    End Sub

    Private Sub Treasury_ComboBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles Treasury_ComboBox.SelectedValueChanged
        'If TypeName(Treasury_ComboBox.SelectedValue) = "String" Then
        On Error Resume Next
        Treasury_Balance.Text = Show_Balance(Treasury_ComboBox.SelectedValue)
        'End If

    End Sub


    Private Sub AG_Cm_Enter(sender As Object, e As EventArgs) Handles money_num_txtb.Enter, Receipt_Title_txt.Enter
        KEY_HANDLER = sender.name
    End Sub

    Private Sub AG_Show_Balance_CB_CheckedChanged(sender As Object, e As EventArgs) Handles AG_Show_Balance_CB.CheckedChanged
        CB_CHecked(sender)
        MY_Settings.AG_Show_Balance_in_Receipt = AG_Show_Balance_CB.Checked
        MY_Settings.Save_AppSetting()
    End Sub

    Private Sub AG_Cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles AG_Cm.SelectedValueChanged

        ' If TypeName(AG_Cm.SelectedValue) = String Then
        On Error Resume Next
        Current_QTY.Text = Show_Balance(AG_Cm.SelectedValue)
        '  End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub DateTimeReceipt_ValueChanged(sender As Object, e As EventArgs) Handles DateTimeReceipt.ValueChanged
        Currency_Equal_txt.Text = Get_Equal(Currency_Cm.SelectedValue, DateTimeReceipt, ReceiptTypeComboBox.SelectedValue)
    End Sub

    Private Sub Get_Tr_Btn_Click(sender As Object, e As EventArgs) Handles Get_Tr_Btn.Click
        Dim F As New ReceiptB_search
        F.DT = Treasury_Datatable.Copy()
        F.ShowDialog()
        If ACC_CODE_Search <> "" Then Treasury_ComboBox.SelectedValue = ACC_CODE_Search
    End Sub

    Private Sub Get_Ag_Btn_Click(sender As Object, e As EventArgs) Handles Get_Ag_Btn.Click
        Dim F As New ReceiptB_search
        F.DT = Agents_Datatable.Copy()
        F.ShowDialog()
        If ACC_CODE_Search <> "" Then AG_Cm.SelectedValue = ACC_CODE_Search
    End Sub
End Class
