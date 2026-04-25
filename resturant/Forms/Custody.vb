Public Class Custody
   
    Public T_ID As Integer
    Dim is_Open As Integer
    Dim is_Void As Integer
    Dim Move_Type As Integer = 0

    Private Sub Change_IM_Details_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Const CS_DROPSHADOW As Integer = &H20000
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property
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

    Private Sub Change_IM_Details_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        ThemeManager.ApplyThemeToForm(Me)
        Dim Move_Type As Integer = 0
        Load_ALL_AG()
        Load_ALL_Titles()
        Get_MAX_T_ID()
    End Sub

    Public Sub Get_MAX_T_ID()

        Dim C As New C
        Dim S As String = "Select ISNULL(MAX(T_ID),1) AS MX From CUSTODY"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                ReceiptNum_Txt.Text = C.Dr("MX")
                Custody_SELECT()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

    End Sub


    Public Sub Custody_SELECT()
        Dim c As New C
        Try
            Dim s As String
            If Move_Type = 1 Then
                s = "select top 1 *  FROM Custody_V WHERE T_ID > '" & Tmp_Bill_ID & "'"
            ElseIf Move_Type = -1 Then
                s = "select top 1 *  FROM Custody_V WHERE T_ID < '" & Tmp_Bill_ID & "'"
            Else
                s = "select *  FROM Custody_V WHERE T_ID = '" & ReceiptNum_Txt.Text & "'"
            End If

            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()

                T_ID = c.Dr("T_ID")
                C_Agent_cm.SelectedValue = c.Dr("AG_ID")
                DateTime.Value = c.Dr("DATE")
                C_Title_txt.Text = c.Dr("C_Title")
                C_Reciept_Num_txt.Text = c.Dr("Receipt_Num")
                Notes_txt.Text = c.Dr("NOTE")
                Dim N As Double = c.Dr("Value")
                money_num_txtb.Text = N.ToString("N")

                is_Open = c.Dr("IS_Open")
                is_Void = c.Dr("isVoid")

                Load_MV_OF_Custody()
                'Check_Reciept()
                Select_Type()

            Else
                MsgBox("لم يتم التعرف على رقم العهدة", MsgBoxStyle.Exclamation)
                ReceiptNum_Txt.Text = Tmp_Bill_ID
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub Select_Type()
        If is_Void = 1 Then
            BillsGrid.BackgroundColor = Color.IndianRed
            BillsGrid.RowsDefaultCellStyle.BackColor = Color.IndianRed
            Print_btn.Enabled = False
            ADDCatButton.Enabled = False
            RemoveCatButton.Enabled = False
        Else
            If is_Open = 0 Then

                BillsGrid.BackgroundColor = Color.LightGreen
                BillsGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
                Total_Rest_txt.Text = "تم الإقفال"
                Print_btn.Enabled = True
                ADDCatButton.Enabled = False
                RemoveCatButton.Enabled = False
            Else
                BillsGrid.BackgroundColor = Color.LightYellow
                BillsGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
                Print_btn.Enabled = True
                ADDCatButton.Enabled = True
                RemoveCatButton.Enabled = True
            End If


        End If
    End Sub



    Private Sub Change_IM_Details_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        FormType = 0
        Me.Dispose()
    End Sub

    Private Sub BackButton_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub


    Private Sub money_num_txtb_KeyPress(sender As Object, e As KeyPressEventArgs) Handles money_num_txtb.KeyPress
        Check_Only_Float(sender, e)
    End Sub


    Private Sub money_num_txtb_TextChanged(sender As Object, e As EventArgs) Handles money_num_txtb.TextChanged
        Check_Point_in_FloatNum(sender, e)
        'Select Case Cr_CM.SelectedValue

        '    Case 1
        Me.money_char_txtb.Text = HANY(Val(money_num_txtb.Text), "EGYPT")
        '    Case 2
        '        Me.money_char_txtb.Text = HANY(Val(money_num_txtb.Text), "USA")
        '    Case 3
        '        Me.money_char_txtb.Text = HANY(Val(money_num_txtb.Text), "EUR")
        'End Select

        'If money_num_txtb.Text.Count = 0 Then T_Other_Cr_TXT.Clear()
        'If T_Other_Cr_TXT.Visible = True Then If money_num_txtb.Text.Count > 0 Then T_Other_Cr_TXT.Text = (Convert.ToDouble(money_num_txtb.Text) * Convert.ToDouble(Cr_Equal_TXT.Text)).ToString("n")

    End Sub

   
    'Private Sub SB_Service_Trans_DELETE()
    '    Dim C As New C
    '    With C.Com
    '        .Connection = C.Con
    '        .CommandText = "[Custody_DELETE]"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@T_ID", T_ID)
    '    End With
    '    If SQL_SP_EXEC(C.Com) = True Then
    '        MsgBox("تمت إضافة العهدة", MsgBoxStyle.Information)

    '        'Network_Edit_Tracker_insert(" العميل:" & IM_SH_txt.Text & " الرقم:" & Barcode_txt.Text & " لهاتف:" & AG_Phone_TextBox.Text & " العنوان:" & AG_AddressTextBox.Text & " البريد الإلكتروني:" _
    '        '                & Email_txt.Text & " النوع:" & AG_Type_cm.Text & " العملة:" & Cr_CM.Text & " المرتب:" & SalaryTextBox.Text & " إشعار الدين:" & Max_Debit_txt.Text, 0, 27, 1)

    '    End If
    'End Sub

    Private Sub Calc_Total()
        Dim TOTAL_Bills = 0
        For i = 0 To BillsGrid.Rows.Count - 1
            TOTAL_Bills = TOTAL_Bills + BillsGrid.Rows(i).Cells("Pure_CL").Value
        Next
        Total_Rcpt_txt.Text = TOTAL_Bills.ToString("N")
        If is_Open = 1 Then Total_Rest_txt.Text = (Convert.ToDouble(money_num_txtb.Text) - TOTAL_Bills).ToString("N")

    End Sub


    Private Sub money_num_txtb_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles money_num_txtb.Validating
        If String.IsNullOrWhiteSpace(money_num_txtb.Text) Then money_num_txtb.Text = "0"
    End Sub


    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        Custody_INSERT()
    End Sub

    Private Sub Custody_INSERT()

        Dim C As New C

        With C.Com
            .CommandText = "Custody_INSERT"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", 0)
            .Parameters.AddWithValue("@C_Rcpt_T_ID", C_Reciept_Num_txt.Text)
            .Parameters("@T_ID").Direction = ParameterDirection.Output
        End With

        If SQL_SP_EXEC(C.Com) = True Then
            MsgBox("تمت إضافة العهدة", MsgBoxStyle.Information)

            'Network_Edit_Tracker_insert(" العميل:" & IM_SH_txt.Text & " الرقم:" & Barcode_txt.Text & " لهاتف:" & AG_Phone_TextBox.Text & " العنوان:" & AG_AddressTextBox.Text & " البريد الإلكتروني:" _
            '                & Email_txt.Text & " النوع:" & AG_Type_cm.Text & " العملة:" & Cr_CM.Text & " المرتب:" & SalaryTextBox.Text & " إشعار الدين:" & Max_Debit_txt.Text, 0, 27, 1)

            T_ID = C.Com.Parameters("@T_ID").Value.ToString()

        End If

    End Sub

    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        'Custody_UPDATE()
    End Sub


    Private Sub Load_MV_OF_Custody()
        Dim c As New C
        Try
            Dim s As String
            s = "SELECT T_ID,ROW_NUMBER() OVER(ORDER BY Date) AS IDX,CONVERT(Date,Date) AS Date,Receipt_Title,Bill_Num,Pure,About FROM Custody_Details_V WHERE Custody_ID =  '" & T_ID & "'"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            BillsGrid.DataSource = c.Dt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    'Private Sub Custody_UPDATE()

    '    Dim C As New C

    '    With C.Com
    '        .CommandText = "Custody_UPDATE"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@T_ID", T_ID)
    '        .Parameters.AddWithValue("@DATE", DateTime.Value)
    '        .Parameters.AddWithValue("@C_Title", C_Title_txt.Text)
    '        .Parameters.AddWithValue("@C_Rcpt_T_ID", C_Reciept_Num_txt.Text)
    '        .Parameters.AddWithValue("@NOTES", Notes_txt.Text)
    '    End With

    '    If SQL_SP_EXEC(C.Com) = True Then
    '        MsgBox("تمت التعديل", MsgBoxStyle.Information)

    '        'Network_Edit_Tracker_insert(" العميل:" & IM_SH_txt.Text & " الرقم:" & Barcode_txt.Text & " لهاتف:" & AG_Phone_TextBox.Text & " العنوان:" & AG_AddressTextBox.Text & " البريد الإلكتروني:" _
    '        '                & Email_txt.Text & " النوع:" & AG_Type_cm.Text & " العملة:" & Cr_CM.Text & " المرتب:" & SalaryTextBox.Text & " إشعار الدين:" & Max_Debit_txt.Text, 0, 27, 1)

    '        'T_ID = C.Com.Parameters("@T_ID").Value.ToString()

    '    End If

    'End Sub

    Dim Tmp_Bill_ID
    Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
        Tmp_Bill_ID = Convert.ToInt64(ReceiptNum_Txt.Text)
        ReceiptNum_Txt.Text = Convert.ToInt64(ReceiptNum_Txt.Text) - 1
        Move_Type = -1
        Custody_SELECT()
        Move_Type = 0
    End Sub

    Private Sub ReceiptNum_Txt_KeyDown(sender As Object, e As KeyEventArgs) Handles ReceiptNum_Txt.KeyDown
        If e.KeyCode = Keys.Return Then
            T_ID = ReceiptNum_Txt.Text
            Custody_SELECT()
        End If
    End Sub

    Private Sub ADDCatButton_Click(sender As Object, e As EventArgs) Handles ADDCatButton.Click
        Custody_Details_INSERT()
    End Sub

    Private Sub Custody_Details_INSERT()
        If String.IsNullOrWhiteSpace(Details_Value_txt.Text) Then Details_Value_txt.Text = "0"
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[Agents_MV_insert_Custody_Exp]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@AG_ID", C_Agent_cm.SelectedValue)
            .Parameters.AddWithValue("@Balance", Convert.ToDouble(Details_Value_txt.Text))
            .Parameters.AddWithValue("@TITLE", Details_Name_cm.Text)
            .Parameters.AddWithValue("@ABOUT", Details_Notes_txt.Text)
            .Parameters.AddWithValue("@Date", Details_Date.Value)
            .Parameters.AddWithValue("@Custody_ID", T_ID)
            .Parameters.AddWithValue("@USER_ID", USER_ID)
            .Parameters.AddWithValue("@Cr_Adress", Details_Bill_Num_txt.Text)
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            MsgBox("تم الإضافة", MsgBoxStyle.Information, "")
            Network_Edit_Tracker_insert(" النوع:" & Details_Name_cm.Text & " العنوان:" & Details_Bill_Num_txt.Text & " بقيمة : " & Details_Value_txt.Text, 0, 34, 1)
            Load_MV_OF_Custody()
            Load_ALL_Titles()
            Clear_Add_Fields()
        End If

    End Sub

    Private Sub Clear_Add_Fields()
        For Each a As Control In Me.ExpandablePanel1.Controls
            If TypeOf a Is TextBox Then a.Text = ""
            Details_Date.Select()
        Next
    End Sub

    Private Sub BillsGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles BillsGrid.RowsAdded
        Calc_Total()
    End Sub

    Private Sub BillsGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles BillsGrid.RowsRemoved
        Calc_Total()
    End Sub

    Private Sub New_butt_Click(sender As Object, e As EventArgs) Handles New_butt.Click
        Clear_Fields()
    End Sub

    Private Sub Clear_Fields()
        For Each a As Control In Me.Controls
            If TypeOf a Is TextBox Then a.Text = ""
        Next
    End Sub

    Public Sub Load_ALL_AG()
        Dim c As New C
        Try
            Dim s As String
            s = "select AG_ID,Ag_name from Agents WHERE AG_ID <> 1"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            C_Agent_cm.DataSource = c.Dt
            C_Agent_cm.DisplayMember = "Ag_name"
            C_Agent_cm.ValueMember = "AG_ID"
            C_Agent_cm.SelectedIndex = -1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Load_ALL_Titles()
        Dim c As New C
        Try
            Dim s As String
            s = "select DISTINCT Receipt_Title from Agents_Balance_MV WHERE Custody_ID IS NOT NULL"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            Details_Name_cm.DataSource = c.Dt
            Details_Name_cm.DisplayMember = "Receipt_Title"
            Details_Name_cm.SelectedIndex = -1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub Check_Reciept_btn_Click(sender As Object, e As EventArgs) Handles Check_Reciept_btn.Click
    '    Check_Reciept()
    'End Sub

    'Private Sub Check_Reciept()
    '    Dim c As New C
    '    Try
    '        Dim s As String
    '        s = "select AG_ID,Debit,Receipt_Title,About,Date FROM Agents_Balance_MV WHERE Receipt_Num = '" & C_Reciept_Num_txt.Text & "'"
    '        c.Com = New SqlClient.SqlCommand(s, c.Con)
    '        c.Con.Open()
    '        c.Dr = c.Com.ExecuteReader
    '        If c.Dr.HasRows Then
    '            c.Dr.Read()
    '            C_Agent_cm.SelectedValue = c.Dr("AG_ID")
    '            money_num_txtb.Text = c.Dr("Debit")
    '            C_Title_txt.Text = c.Dr("Receipt_Title")
    '            Notes_txt.Text = c.Dr("About")
    '            DateTime.Value = c.Dr("Date")
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try


    'End Sub

    Private Sub RemoveCatButton_Click(sender As Object, e As EventArgs) Handles RemoveCatButton.Click
        If BillsGrid.Rows.Count > 0 Then
            'Beep()
            If MessageBox.Show("تأكيد إلغاء المعاملة نهائيا", "", MessageBoxButtons.OKCancel, _
                               MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
                Custody_Details_DELETE()
            End If
        End If
    End Sub

    Private Sub Custody_Details_DELETE()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "AG_Balance_Void_Row"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", BillsGrid.CurrentRow.Cells("T_ID_CL").Value)
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            Network_Edit_Tracker_insert(" النوع:" & BillsGrid.CurrentRow.Cells("Receipt_Title_CL").Value.ToString & " العنوان:" & BillsGrid.CurrentRow.Cells("About_CL").Value.ToString & " بقيمة : " & BillsGrid.CurrentRow.Cells("Pure_CL").Value, 0, 34, 2)
            Load_MV_OF_Custody()
        End If
    End Sub

    Public Sub Custody_CLOSE()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Custody_CLOSE"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID)
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            'Network_Edit_Tracker_insert(" النوع:" & BillsGrid.CurrentRow.Cells("Receipt_Title_CL").Value.ToString & " العنوان:" & BillsGrid.CurrentRow.Cells("About_CL").Value.ToString & " بقيمة : " & BillsGrid.CurrentRow.Cells("Credit_CL").Value, 0, 34, 2)
            Custody_SELECT()

        End If
    End Sub

    Private Sub Up_Bill_btn_Click(sender As Object, e As EventArgs) Handles Up_Bill_btn.Click
        If Not String.IsNullOrWhiteSpace(ReceiptNum_Txt.Text) Then
            Tmp_Bill_ID = Convert.ToInt64(ReceiptNum_Txt.Text)
            ReceiptNum_Txt.Text = Convert.ToInt64(ReceiptNum_Txt.Text) + 1
            Move_Type = 1
            Custody_SELECT()
            Move_Type = 0
        End If
    End Sub

    Private Sub Move_To_SB_btn_Click(sender As Object, e As EventArgs) Handles Move_To_SB_btn.Click

        If Convert.ToDouble(Total_Rest_txt.Text) > 0 Then

            If MessageBox.Show("تأكيد إقفال العهدة و القيام بإيصال قبض بقيمة : " & Total_Rest_txt.Text, "", MessageBoxButtons.OKCancel, _
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
                AG_Type = 3
                FormType = 12
                F_Receipt = New Receipt
                With F_Receipt
                    Rct_Tr_ID = SB_TR_ID
                    .ClearFields()
                    .Fields_Panel.Enabled = True
                    .AG_Cm.Enabled = False
                    .Barcode_SH_txt.Enabled = False
                    .Receipt_Title_combobox.Text = "إقفال عهدة : " + C_Title_txt.Text & " \ رقم: " & T_ID.ToString
                    .AG_Cm.Set_IM_By_ID(C_Agent_cm.SelectedValue)
                    .Current_QTY.Text = Show_AG_T_Balance(C_Agent_cm.SelectedValue)
                    .Fetch_AG_Currency()
                    .money_num_txtb.Text = Total_Rest_txt.Text
                End With
                F_Receipt.ShowDialog()
            End If

        ElseIf MessageBox.Show("تأكيد إقفال العهدة ", "", MessageBoxButtons.OKCancel, _
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
            Custody_CLOSE()
        End If

    End Sub

    Public Sub Custody_Bill()

        Try
            Dim pp As New ReportConnection

            pp.rp.Load(Application.StartupPath & "\reports\Custody_Bill.rpt")

            pp.CrTables = pp.rp.Database.Tables

            For Each CrTable In pp.CrTables
                pp.crtableLogoninfo = CrTable.LogOnInfo
                pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
                CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
            Next
            With pp
                .rp.SetParameterValue(0, " تاريخ : " + DateTime.Value)
                .rp.SetParameterValue(1, USER_NAME)
                .rp.SetParameterValue(2, MY_Settings.Server_Desc)
                .rp.SetParameterValue(3, T_ID)
                .rp.SetParameterValue(4, Total_Rcpt_txt.Text)
                .rp.SetParameterValue(5, "عهــــدة")
                .rp.SetParameterValue(6, " الرقم : " + C_Reciept_Num_txt.Text & " / " & " العنـــوان : " + C_Title_txt.Text)
                .rp.SetParameterValue(7, " الحســاب : " + C_Agent_cm.Text)
                .rp.SetParameterValue(8, " المبلغ : " + money_num_txtb.Text)
                .rp.SetParameterValue(9, Total_Rest_txt.Text)

            End With

            Dim p As New print
            p.CrystalReportViewer1.ReportSource = pp.rp
            p.Show()

            'If Def_Befor_Print = 1 Then Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n ""{0}""", Default_Printer_A4))
            'pp.rp.PrintOptions.PrinterName = Default_Printer_A4
            'pp.rp.PrintToPrinter(1, False, 0, 0)
            'pp.rp.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        Custody_Bill()
    End Sub

    Private Sub Details_Value_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Details_Value_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Details_Value_txt_TextChanged(sender As Object, e As EventArgs) Handles Details_Value_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Details_Date_KeyDown(sender As Object, e As KeyEventArgs) Handles Details_Date.KeyDown
        If e.KeyCode = Keys.Return Then
            Details_Name_cm.DroppedDown = True
            Details_Name_cm.Select()
        End If
    End Sub

    Private Sub Details_Name_cm_KeyDown(sender As Object, e As KeyEventArgs) Handles Details_Name_cm.KeyDown
        If e.KeyCode = Keys.Return Then Details_Bill_Num_txt.Select()
    End Sub

    Private Sub Details_Bill_Num_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Details_Bill_Num_txt.KeyDown
        If e.KeyCode = Keys.Return Then Details_Value_txt.Select()
    End Sub

    Private Sub Details_Value_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Details_Value_txt.KeyDown
        If e.KeyCode = Keys.Return Then Details_Notes_txt.Select()
    End Sub

    Private Sub Details_Notes_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Details_Notes_txt.KeyDown
        If e.KeyCode = Keys.Return Then If ADDCatButton.Enabled = True Then ADDCatButton_Click(sender, e)
    End Sub

    Private Sub HeaderCloseBtn_Click(sender As Object, e As EventArgs) Handles HeaderCloseBtn.Click
        Me.Close()
    End Sub
End Class