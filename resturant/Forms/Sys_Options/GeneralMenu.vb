Imports System.Drawing.Printing
Imports System.Data.SqlClient

Public Class GeneralMenu
    Public GM_ID As Integer
    Dim GM_NAME As String
    Dim PRINTER_DT As New DataTable

    Private Sub SubMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        Check_Sys_Featurs()
        Load_SM()
        SM_LoadPrinter()
        DisableTools()
        Load_Computers()
    End Sub


    Private Sub Check_Sys_Featurs()
        CP_Bill_Screen_GroupBox.Visible = S_Tables
        SubPrint_Panel.Visible = S_SubPrints
        If SScreenDefault <> 1 Then Touch_Screen_Panel.Visible = False
    End Sub


    Function GetMailItems() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "------- مخزن المبيعات الإفتراضي -------"))

        Dim c1 As New C
        Dim s As String = "select ST_ID AS ID,ST_name AS name from STORES ORDER By ST_ID ASC"
        c1.Com = New SqlClient.SqlCommand(s, c1.Con)
        c1.Con.Open()
        Try
            c1.Dr = c1.Com.ExecuteReader
            If c1.Dr.HasRows Then
                While c1.Dr.Read
                    mailItems.Add(New MailItem(c1.Dr("ID"), c1.Dr("name")))
                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
        Return mailItems

    End Function

    Public Sub Load_Computers()
        Dim c As New C
        Dim s As String = "select CP_NAME from SysSetting"
        c.Da = New SqlDataAdapter(s, c.Con)
        c.Da.Fill(c.Dt)
        Ksh_Screen_cmb.DataSource = c.Dt
        Ksh_Screen_cmb.DisplayMember = "CP_NAME"
        Ksh_Screen_cmb.SelectedIndex = 0
    End Sub

    Public Sub SM_LoadPrinter()
        Dim c As New C
        Dim s As String = "select Printer_ID,Printer_Name from Printers"
        c.Da = New SqlDataAdapter(s, c.Con)
        c.Da.Fill(c.Dt)
        SMPrinterComboBox.DataSource = c.Dt
        SMPrinterComboBox.DisplayMember = "Printer_Name"
        SMPrinterComboBox.ValueMember = "Printer_ID"
        SMPrinterComboBox.SelectedIndex = 0
    End Sub

    Public Sub Clear_Fields()
        SMNameTextBox.Clear()
        SMPrinterComboBox.SelectedIndex = 0
        IM_ViewerButton.Text = ""
        IM_ViewerButton.Image = Nothing
        IM_ViewerButton.BackColor = System.Drawing.SystemColors.Info
        BKNoneColoreCheckBox.Checked = True
        FKNoneColoreCheckBox.Checked = True
        is_Show_cb.Checked = True
        Rank_Num_txt.Clear()
        PRINTER_DT.Clear()
    End Sub

    Public Sub EnableTools()
        Panel1.Enabled = True
    End Sub

    Public Sub DisableTools()
        Panel1.Enabled = False
    End Sub

    Private Sub NewEmpButton_Click(sender As Object, e As EventArgs) Handles NewEmpButton.Click
        Clear_Fields()
        EnableTools()
        SaveButton.Enabled = True
        EditEmpButton.Enabled = False
        DeleteButton.Enabled = False
        SMNameTextBox.Select()
        SMPrinterComboBox.SelectedIndex = 0
    End Sub
    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click

        If String.IsNullOrWhiteSpace(SMNameTextBox.Text) Then
            SMNamerrorProvider.SetError(SMNameTextBox, "أدخل إسم القائمة")
            Exit Sub
        End If

        If SMPrinterComboBox.Visible = True Then
            If String.IsNullOrWhiteSpace(SMPrinterComboBox.Text) Then
                PrinterErrorProvider.SetError(SMPrinterComboBox, "أدخل طابعة القائمة")
                Exit Sub
            End If
        End If

        Insert_SM()
    End Sub

    Private Sub Format_Name()

        If SMNameTextBox.Text.Count < 4 Then
            Do While SMNameTextBox.Text.Count < 4
                SMNameTextBox.Text = SMNameTextBox.Text + " "
            Loop
        End If

    End Sub

    Private Sub EditEmpButton_Click(sender As Object, e As EventArgs) Handles EditEmpButton.Click
        If EditEmpButton.Text = "تعديل" Then
            EditEmpButton.Text = "تأكيد التعديل"
            EnableTools()
        Else
            If String.IsNullOrWhiteSpace(SMNameTextBox.Text) Then
                SMNamerrorProvider.SetError(SMNameTextBox, "أدخل إسم القائمة")
                Exit Sub
            End If

            If SMPrinterComboBox.Visible = True Then
                If String.IsNullOrWhiteSpace(SMPrinterComboBox.Text) Then
                    PrinterErrorProvider.SetError(SMPrinterComboBox, "أدخل طابعة القائمة")
                    Exit Sub
                End If
            End If
            Update_SM()
        End If
    End Sub

    Private Sub Insert_SM()
        Format_Name()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "GM_insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@GM_ID", 0)
            .Parameters.AddWithValue("@GM_Name", Me.SMNameTextBox.Text)
            .Parameters.AddWithValue("@Printer_ID", Me.SMPrinterComboBox.SelectedValue)

            If Me.BKNoneColoreCheckBox.Checked = False Then
                .Parameters.AddWithValue("@BK_R", Me.BKPanel.BackColor.R)
                .Parameters.AddWithValue("@BK_G", Me.BKPanel.BackColor.G)
                .Parameters.AddWithValue("@BK_B", Me.BKPanel.BackColor.B)
            End If


            If Me.FKNoneColoreCheckBox.Checked = False Then
                .Parameters.AddWithValue("@FK_R", Me.FKPanel.BackColor.R)
                .Parameters.AddWithValue("@FK_G", Me.FKPanel.BackColor.G)
                .Parameters.AddWithValue("@FK_B", Me.FKPanel.BackColor.B)
            End If
            .Parameters.AddWithValue("@POS_isShow", is_Show_cb.Checked)
            .Parameters.AddWithValue("@Ksh_Screen", Ksh_Screen_cmb.Text)
            '   .Parameters.AddWithValue("@Cat_ID", IM_CAT_SF.TXT_ID.Text)
        End With

        If SQL_SP_EXEC(C.Com) Then
            MsgBox("تمت إضافة القائمة", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" التصنيف:" & SMNameTextBox.Text & " طابعة التجهيز:" & SMPrinterComboBox.Text, 0, 21, 1)
            Clear_Fields()
            DisableTools()
            Load_SM()
        End If

    End Sub


    Public Sub Delete_SM()
        Dim c As New C

        With c.Com
            .Connection = c.Con
            .CommandText = "GM_delete"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@GM_ID", GM_ID)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تم حــذف القائمة", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" التصنيف:" & GM_NAME & " طابعة التجهيز:" & SMPrinterComboBox.Text, 0, 21, 2)

        End If

    End Sub

    Public Sub Update_SM()
        Format_Name()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "GM_update"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("GM_ID", GM_ID)
            .Parameters.AddWithValue("GM_name", SMNameTextBox.Text)
            .Parameters.AddWithValue("Printer_ID", SMPrinterComboBox.SelectedValue)

            If Me.BKNoneColoreCheckBox.Checked = False Then
                .Parameters.AddWithValue("@BK_R", Me.BKPanel.BackColor.R)
                .Parameters.AddWithValue("@BK_G", Me.BKPanel.BackColor.G)
                .Parameters.AddWithValue("@BK_B", Me.BKPanel.BackColor.B)
            End If

            If Me.FKNoneColoreCheckBox.Checked = False Then
                .Parameters.AddWithValue("@FK_R", Me.FKPanel.BackColor.R)
                .Parameters.AddWithValue("@FK_G", Me.FKPanel.BackColor.G)
                .Parameters.AddWithValue("@FK_B", Me.FKPanel.BackColor.B)
            End If

            .Parameters.AddWithValue("@POS_isShow", is_Show_cb.Checked)
            If Not String.IsNullOrWhiteSpace(Rank_Num_txt.Text) Then .Parameters.AddWithValue("@Rank_Num", Rank_Num_txt.Text)
            .Parameters.AddWithValue("@Ksh_Screen", Ksh_Screen_cmb.Text)
            ' .Parameters.AddWithValue("@Cat_ID", IM_CAT_SF.TXT_ID.Text)
        End With

        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تم التعديل", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" الإسم السابق:" & GM_NAME & " التصنيف:" & SMNameTextBox.Text & " طابعة التجهيز:" & SMPrinterComboBox.Text, 0, 21, 3)
            EditEmpButton.Text = "تعديل"
            DisableTools()
            Load_SM()
        End If

    End Sub

    Private Sub Load_SM()
        Dim C As New C

        With (C.Com)
            .Connection = C.Con
            .CommandText = "GM_NameList"
            .CommandType = CommandType.StoredProcedure
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        Me.SMDataGridViewX.DataSource = C.Dt
    End Sub

    Private Sub Featch_SM()

        Dim C As New C

        C.Con.Open()
        With C.Com
            .Connection = C.Con
            .CommandText = "GM_Select_ID"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@GM_ID", SMDataGridViewX.CurrentRow.Cells(0).Value)
        End With

        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            C.Dr.Read()

            Clear_Fields()
            DisableTools()
            EditEmpButton.Enabled = True
            DeleteButton.Enabled = True
            SaveButton.Enabled = False
            EditEmpButton.Text = "تعديل"

            GM_ID = C.Dr("GM_ID")
            GM_NAME = C.Dr("GM_name")

            'If Not IsDBNull(C.Dr("CAT_ID")) Then
            '    IM_CAT_SF.Set_IM_By_ID(C.Dr("CAT_ID"))
            'Else
            '    IM_CAT_SF.Set_IM_By_ID(1)
            'End If

            SMNameTextBox.Text = C.Dr("GM_name")
            SMPrinterComboBox.SelectedValue = C.Dr("Printer_ID")

            If IsDBNull(C.Dr("BK_R")) Then
                Me.BKPanel.BackColor = Nothing
                Me.BKNoneColoreCheckBox.Checked = True
                Me.IM_ViewerButton.BackColor = System.Drawing.SystemColors.Info
            Else
                Me.BKNoneColoreCheckBox.Checked = False
                Me.BKPanel.BackColor = Color.FromArgb(C.Dr("BK_R"), C.Dr("BK_G"), C.Dr("BK_B"))
                Me.IM_ViewerButton.BackColor = Me.BKPanel.BackColor
            End If

            If IsDBNull(C.Dr("FK_R")) Then
                Me.FKPanel.BackColor = Nothing
                Me.FKNoneColoreCheckBox.Checked = True
                Me.IM_ViewerButton.ForeColor = Color.Black
            Else
                Me.FKNoneColoreCheckBox.Checked = False
                Me.FKPanel.BackColor = Color.FromArgb(C.Dr("FK_R"), C.Dr("FK_G"), C.Dr("FK_B"))
                Me.IM_ViewerButton.ForeColor = Me.FKPanel.BackColor
            End If

            is_Show_cb.Checked = C.Dr("POS_isShow")
            Rank_Num_txt.Text = C.Dr("Rank_Num")
            If S_Tables = True Then Ksh_Screen_cmb.Text = C.Dr("Ksh_Screen")

            Load_GM_Printer()
        End If
        C.Con.Close()

    End Sub

  
    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click

        If GM_ID = 1 Then
            MsgBox("لا يمكن حذف المجموعة الرئيسية للنظام", MsgBoxStyle.Exclamation)
            Exit Sub
        Else

            If MessageBox.Show(" تـأكيــد حــذف المجمــوعة الرئيسية " + SMNameTextBox.Text, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, _
                              MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                Delete_SM()
                Clear_Fields()
                Load_SM()
            End If

        End If

    End Sub

    Private Sub SMNameTextBox_TextChanged(sender As Object, e As EventArgs) Handles SMNameTextBox.TextChanged
        SMNamerrorProvider.Clear()
        IM_ViewerButton.Text = SMNameTextBox.Text
    End Sub

    Private Sub SMPrinterComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SMPrinterComboBox.SelectedIndexChanged
        PrinterErrorProvider.Clear()
    End Sub

    Private Sub SMDataGridViewX_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles SMDataGridViewX.CellClick
        If SMDataGridViewX.Rows.Count > 0 Then Featch_SM()
    End Sub

    Private Sub BKChoaseButton_Click(sender As Object, e As EventArgs) Handles BKChoaseButton.Click
        Using dlg As New ColorDialog
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                'user selected something (and clicked ok)
                BKPanel.BackColor = dlg.Color
            End If
        End Using
    End Sub

    Private Sub FKChoaseButton_Click(sender As Object, e As EventArgs) Handles FKChoaseButton.Click
        Using dlg As New ColorDialog
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                'user selected something (and clicked ok)
                FKPanel.BackColor = dlg.Color
            End If
        End Using
    End Sub

    Private Sub BKNoneColoreCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles BKNoneColoreCheckBox.CheckedChanged
        If sender.Checked = True Then
            sender.ForeColor = Color.DarkGreen
            BKPanel.BackColor = Nothing
            BKChoaseButton.Enabled = False
            IM_ViewerButton.BackColor = System.Drawing.SystemColors.Info
        Else
            sender.ForeColor = Color.Firebrick
            BKChoaseButton.Enabled = True
        End If
    End Sub

    Private Sub FKNoneColoreCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles FKNoneColoreCheckBox.CheckedChanged
        If sender.Checked = True Then
            sender.ForeColor = Color.DarkGreen
            FKPanel.BackColor = Nothing
            FKChoaseButton.Enabled = False
            IM_ViewerButton.ForeColor = Color.Black
        Else
            sender.ForeColor = Color.Firebrick
            FKChoaseButton.Enabled = True
        End If
    End Sub

    Private Sub BKPanel_BackColorChanged(sender As Object, e As EventArgs) Handles BKPanel.BackColorChanged
        IM_ViewerButton.BackColor = BKPanel.BackColor
    End Sub

    Private Sub FKPanel_BackColorChanged(sender As Object, e As EventArgs) Handles FKPanel.BackColorChanged
        IM_ViewerButton.ForeColor = FKPanel.BackColor
    End Sub

    Private Sub is_Show_cb_CheckedChanged(sender As Object, e As EventArgs) Handles is_Show_cb.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Rank_Num_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Rank_Num_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub GeneralMenu_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub InsertU_btn_Click(sender As Object, e As EventArgs) Handles InsertU_btn.Click
        If CHECK_REPT() = 0 Then
            If GM_ID > 0 Then
                query("INSERT INTO [dbo].[GM_Printer]([GM_ID],[PTR_ID]) VALUES(" & GM_ID & "," & SMPrinterComboBox.SelectedValue & ")")
                Load_GM_Printer()
            End If
        Else
            MsgBox(" تم إدراج الطابعة من قبل ", MsgBoxStyle.Exclamation, "")
        End If
    End Sub

    Public Sub Load_GM_Printer()
        PRINTER_DT.Clear()
        Dim c As New C
        Dim s As String = "select T_ID,Printer_Name from GM_Printer_V WHERE GM_ID = " & GM_ID
        c.Da = New SqlDataAdapter(s, c.Con)
        c.Da.Fill(PRINTER_DT)
        PRINTER_GRID.DataSource = PRINTER_DT
    End Sub

    Function CHECK_REPT()
        Dim F = 0
        Dim C As New C
        Dim S As String = "Select  T_ID From GM_Printer Where GM_ID = " & GM_ID & " And PTR_ID =  " & SMPrinterComboBox.SelectedValue
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then F = 1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()


        Return F
    End Function

    Private Sub DeleteU_btn_Click(sender As Object, e As EventArgs) Handles DeleteU_btn.Click
        query("DELETE FROM [dbo].[GM_Printer] WHERE T_ID = " & PRINTER_GRID.CurrentRow.Cells("T_ID").Value)
        Load_GM_Printer()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim F As New CAT_FORM
        F.Form_Name = "IM_CAT"
        F.Form_Name_Arabic = "الفئات"
        F.F_ID = "ID"
        F.F_Name = "NAME"
        F.F_DETAILS = "IM_CAT"
        F.Checked_Table = "General_menu"
        F.Checked_Table_ID = "CAT_ID"
        F.ShowDialog()
    End Sub
End Class