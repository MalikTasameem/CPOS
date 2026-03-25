Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO

Public Class Agent


    Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Public AG_ID As Integer
    Public AG_Name As String = ""
    Dim IM_Dt As New DataTable

    Private Sub AgentCard_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub AgentCard_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then If New_butt.Enabled = True Then new_butt_Click(sender, e)
        If e.KeyCode = Keys.F12 Then If Save_butt.Enabled = True Then Save_butt_Click(sender, e)
        If e.KeyCode = Keys.F3 Then If Edit_butt.Enabled = True Then If Edit_butt.Text = EditState Then Edit_butt_Click(sender, e)
        If e.KeyCode = Keys.F4 Then If Delete_butt.Enabled = True Then Delete_butt_Click(sender, e)
    End Sub
    Public Sub Agent_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        EditState = Edit_butt.Text
        DefaultFormState = Me.Text
        ' Load_AG()
        Fetch_Agents_Type()
        Fetch_Currency()
        NewStateBt()
        New_butt.Select()
        'IM_SH_txt.Select()

        Make_Hints()
        Load_ALL_AG()
        AG_Type_cm.SelectedIndex = 0

    End Sub



    Private Sub Make_Hints()
        SendMessage(Search_By_Acc_Name_txt.Handle, &H1501, 0, "إبحث عن إسم عميــــل")
        SendMessage(Search_By_Acc_Code_txt.Handle, &H1501, 0, "إبحث عن رقـــم عميــــل")
    End Sub

    Private Sub Fetch_Agents_Type()
        Dim C As New C
        Try
            Dim sql As String = "Select id,Type_Name from Agents_Types WHERE Visible = 1 Order By Rank_Num ASC"
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            AG_Type_cm.DataSource = C.Dt
            AG_Type_cm.DisplayMember = "Type_Name"
            AG_Type_cm.ValueMember = "id"
            AG_Type_cm.SelectedValue = 1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Fetch_Currency()
        Dim C As New C
        Try
            Dim sql As String = "Select Cr_ID , Cr_Name from Currency Order By Cr_ID ASC"
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            Cr_CM.DataSource = C.Dt
            Cr_CM.DisplayMember = "Cr_Name"
            Cr_CM.ValueMember = "Cr_ID"
            ' Cr_CM.SelectedValue = 1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub NewStateBt()
        FieldsPanel.Enabled = True
        Save_butt.Enabled = True
        Edit_butt.Enabled = False
        Edit_butt.Text = "تعديـل F3"
        Delete_butt.Enabled = False
        Add_Prev_Balance_btn.Enabled = False
        Me.Text = "عميل جديــد"
        IM_SH_txt.Select()
    End Sub
    Private Sub DeleteOrUpdateStateBt()
        FieldsPanel.Enabled = False
        Save_butt.Enabled = False
        Edit_butt.Enabled = False
        Delete_butt.Enabled = False
        Add_Prev_Balance_btn.Enabled = False
        Me.Text = DefaultFormState
        Load_ALL_AG()
    End Sub

    Private Sub SavedStateBt()
        FieldsPanel.Enabled = False
        Save_butt.Enabled = False
        Edit_butt.Enabled = False
        Delete_butt.Enabled = False
        Add_Prev_Balance_btn.Enabled = False
        Me.Text = DefaultFormState
        Load_ALL_AG()
    End Sub

    Private Sub SelectStateBt()
        FieldsPanel.Enabled = False
        Save_butt.Enabled = False
        Edit_butt.Enabled = True
        Delete_butt.Enabled = True
        Add_Prev_Balance_btn.Enabled = True
        Me.Text = "بيانات العميل : " + AG_Name
        Edit_butt.Text = EditState
        'IMDataGridViewX.Visible = False
    End Sub

    Private Sub ClearFields()
        IM_SH_txt.Clear()
        AG_Phone_TextBox.Clear()
        AG_AddressTextBox.Clear()
        SalaryTextBox.Text = "0"
        AG_BalanceTextBox.Text = "0.00"
        Edit_butt.Text = EditState
        Me.Text = FormState
        Barcode_txt.Clear()
        BillPictureBox.Image = My.Resources.person
        'AG_Type_cm.SelectedIndex = 0
        Max_Debit_txt.Clear()
        Email_txt.Clear()
        is_Emp_Pause_CB.Checked = False
        IM_Name_ToolStrip.Text = "(عميل جديد)"
        ID_ToolStripLabel.Text = "---"
        ACC_CODE_TXT.Clear()
        Show_ag_menuCB.Checked = False
    End Sub


    Private Sub SalaryTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SalaryTextBox.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub new_butt_Click(sender As Object, e As EventArgs) Handles New_butt.Click
        ClearFields()
        NewStateBt()
    End Sub


    Public Sub Fetch_ItemToList(Search_AG_ID As Integer)
        If IMDataGridViewX.Rows.Count > 0 Then
            'IMDataGridViewX.Visible = False

            ClearFields()

            Dim C As New C
            Dim Data As Byte()
            Dim S As String = "Select * From AGENTS_MENU_V Where AG_ID = '" & Search_AG_ID & "'"
            C.Com = New SqlClient.SqlCommand(S, C.Con)
            C.Con.Open()
            Try
                C.Dr = C.Com.ExecuteReader
                If C.Dr.HasRows Then
                    C.Dr.Read()
                    AG_ID = C.Dr("AG_ID")
                    IM_SH_txt.Text = C.Dr("Ag_name")

                    IM_Name_ToolStrip.Text = C.Dr("Ag_name")
                    ID_ToolStripLabel.Text = C.Dr("AG_ID")

                    AG_Name = C.Dr("Ag_name")
                    AG_Phone_TextBox.Text = C.Dr("Ag_phone")
                    AG_AddressTextBox.Text = C.Dr("Address")

                    Dim Num As Double = C.Dr("T_Balance")
                    'AG_BalanceTextBox.Text = C.Dr("T_Balance")
                    AG_BalanceTextBox.Text = Num.ToString(MY_Settings.N_Point_Fter)


                    Barcode_txt.Text = C.Dr("Barcode")
                    AG_Type_cm.SelectedValue = C.Dr("Type_ID")


                    Num = C.Dr("AG_Salary")
                    SalaryTextBox.Text = Num.ToString("00")

                    Date_Start.Value = C.Dr("DATE_START")

                    If IsDBNull(C.Dr("AG_img")) = False Then
                        Data = DirectCast(C.Dr("AG_img"), Byte())
                        Dim MS As New MemoryStream(Data)
                        BillPictureBox.Image = Image.FromStream(MS)
                    Else
                        BillPictureBox.Image = My.Resources.person
                    End If

                    Max_Debit_txt.Text = C.Dr("Max_Debit")
                    Email_txt.Text = C.Dr("E_mail")
                    is_Emp_Pause_CB.Checked = C.Dr("is_Emp_Pause")

                    Cr_CM.SelectedValue = C.Dr("Cr_ID")
                    ACC_CODE_TXT.Text = C.Dr("Tree_Code")

                    If Not IsDBNull(C.Dr("isDefaultAG")) Then Show_ag_menuCB.Checked = C.Dr("isDefaultAG")


                    SelectStateBt()
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            C.Con.Close()
        End If

    End Sub


    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        If ValidateChildren() = True Then
            If String.IsNullOrWhiteSpace(Barcode_txt.Text) = False Then
                AG_Check_Barcode()
            Else
                Insert_New_AG()
            End If
        End If
    End Sub

    Private Sub AG_Check_Barcode()
        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "AG_Check_Barcode"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@Barcode", Barcode_txt.Text)
            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                If .Parameters("@F").Value > 0 Then
                    BarError.SetError(Barcode_txt, "باركود متكرر")
                    Barcode_txt.Select()
                    Barcode_txt.Focus()
                Else
                    Insert_New_AG()
                End If

            End If
        End With
    End Sub

    Private Sub IM_Check_UpdateBarcode()
        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "AG_Check_UpdateBarcode"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@Barcode", Barcode_txt.Text)
            .Parameters.AddWithValue("@AG_ID", AG_ID)
            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                If .Parameters("@F").Value > 0 Then
                    BarError.SetError(Barcode_txt, "باركود متكرر")
                    Barcode_txt.Select()
                    Barcode_txt.Focus()
                Else
                    Update_AG()
                End If

            End If
        End With
    End Sub

    Private Sub Insert_New_AG()

        Dim C As New C

        With C.Com
            .CommandText = "Agents_insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Ag_ID", 0)
            .Parameters.AddWithValue("@Ag_name", IM_SH_txt.Text)
            .Parameters.AddWithValue("@Barcode", Barcode_txt.Text)
            .Parameters.AddWithValue("@Ag_phone", AG_Phone_TextBox.Text)
            .Parameters.AddWithValue("@Address", AG_AddressTextBox.Text)

            .Parameters.AddWithValue("@AG_Salary", SalaryTextBox.Text)
            .Parameters.AddWithValue("@Type_ID", AG_Type_cm.SelectedValue)

            If BillPictureBox.Image IsNot Nothing Then .Parameters.AddWithValue("@AG_Img", ConvertImage(BillPictureBox.Image))
            If String.IsNullOrWhiteSpace(Max_Debit_txt.Text) = False Then .Parameters.AddWithValue("@Max_Debit", Max_Debit_txt.Text)
            .Parameters.AddWithValue("@E_mail", Email_txt.Text)
            .Parameters.AddWithValue("@is_Emp_Pause", is_Emp_Pause_CB.Checked)
            .Parameters.AddWithValue("@Cr_ID", Cr_CM.SelectedValue)
            .Parameters.AddWithValue("@Date_Start", Date_Start.Value)

            .Parameters.AddWithValue("@isDefaultAG", Show_ag_menuCB.Checked)

            .Parameters("@Ag_ID").Direction = ParameterDirection.Output
        End With

        If SQL_SP_EXEC(C.Com) = True Then
            MsgBox("تمت إضافة العميـل", MsgBoxStyle.Information)

            Network_Edit_Tracker_insert(" العميل:" & IM_SH_txt.Text & " الرقم:" & Barcode_txt.Text & " لهاتف:" & AG_Phone_TextBox.Text & " العنوان:" & AG_AddressTextBox.Text & " البريد الإلكتروني:" _
                            & Email_txt.Text & " النوع:" & AG_Type_cm.Text & " العملة:" & Cr_CM.Text & " المرتب:" & SalaryTextBox.Text & " إشعار الدين:" & Max_Debit_txt.Text & " مباشرة العمل: " & Date_Start.Text, 0, 27, 1)

            AG_ID = C.Com.Parameters("@AG_ID").Value.ToString()
            '  Load_AG()
            ' ClearFields()
            SavedStateBt()
        End If

    End Sub

    Private Sub Update_AG()
        Dim C As New C

        With C.Com
            .CommandText = "Agents_Update"
            .CommandType = CommandType.StoredProcedure

            .Parameters.AddWithValue("@AG_ID", AG_ID)
            .Parameters.AddWithValue("@Ag_name", IM_SH_txt.Text)
            .Parameters.AddWithValue("@Barcode", Barcode_txt.Text)
            .Parameters.AddWithValue("@Ag_phone", AG_Phone_TextBox.Text)
            .Parameters.AddWithValue("@Address", AG_AddressTextBox.Text)

            .Parameters.AddWithValue("@AG_Salary", SalaryTextBox.Text)
            .Parameters.AddWithValue("@Type_ID", AG_Type_cm.SelectedValue)
            If BillPictureBox.Image IsNot Nothing Then .Parameters.AddWithValue("@AG_Img", ConvertImage(BillPictureBox.Image))
            If String.IsNullOrWhiteSpace(Max_Debit_txt.Text) = False Then .Parameters.AddWithValue("@Max_Debit", Max_Debit_txt.Text)
            .Parameters.AddWithValue("@E_mail", Email_txt.Text)
            .Parameters.AddWithValue("@is_Emp_Pause", is_Emp_Pause_CB.Checked)
            .Parameters.AddWithValue("@Cr_ID", Cr_CM.SelectedValue)
            .Parameters.AddWithValue("@Date_Start", Date_Start.Value)
            .Parameters.AddWithValue("@Tree_Code", ACC_CODE_TXT.Text)
            .Parameters.AddWithValue("@isDefaultAG", Show_ag_menuCB.Checked)
        End With


        If SQL_SP_EXEC(C.Com) = True Then
            MsgBox("تم تعديل بيانات العميــــل", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" الإسم السابق:" & AG_Name & " العميل:" & IM_SH_txt.Text & " الرقم:" & Barcode_txt.Text & " لهاتف:" & AG_Phone_TextBox.Text & " العنوان:" & AG_AddressTextBox.Text & " البريد الإلكتروني:" _
                                       & Email_txt.Text & " النوع:" & AG_Type_cm.Text & " العملة:" & Cr_CM.Text & " المرتب:" & SalaryTextBox.Text & " إشعار الدين:" & Max_Debit_txt.Text & " مباشرة العمل: " & Date_Start.Text, 0, 27, 3)
            Edit_butt.Text = EditState
            ClearFields()
            DeleteOrUpdateStateBt()
            ' Load_AG()
        End If

    End Sub

    Private Sub Delete_AG()

        Dim C As New C

        With C.Com
            .CommandText = "Agents_Delete"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@AG_ID", AG_ID)
        End With

        If SQL_SP_EXEC(C.Com) = True Then
            MsgBox("تم حـــذف بيانات العميـل", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert(" العميل:" & AG_Name, 0, 27, 2)

            ClearFields()
            DeleteOrUpdateStateBt()
            ' Load_AG()
        End If
    End Sub

    'Private Sub Load_AG()
    '    Try
    '        Dim C As New C
    '        AG_DT.Clear()
    '        Dim sql As String = " select AG_ID,Ag_name from Agents Where AG_ID > 1 Order By AG_ID ASC"
    '        C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
    '        C.Da.Fill(AG_DT)
    '        AGMetroGrid.DataSource = AG_DT
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub

    Private Sub AG_Name_txtb_Enter(sender As Object, e As EventArgs) Handles IM_SH_txt.Enter
        Arabic_Lang()
    End Sub

    Private Sub AG_Name_txtb_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_SH_txt.KeyDown
        If e.KeyCode = Keys.Return Or e.KeyCode = Keys.Down Then Barcode_txt.Select()
    End Sub

    Private Sub AG_Name_txtb_TextChanged(sender As Object, e As EventArgs)
        AG_NAMEErrorProvider.Clear()
    End Sub

    Private Sub AG_Name_txtb_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles IM_SH_txt.Validating
        If String.IsNullOrWhiteSpace(IM_SH_txt.Text) = True Then
            AG_NAMEErrorProvider.SetError(IM_SH_txt, " حدد إسم العميل ")
            e.Cancel = True
        Else
            e.Cancel = False
            AG_NAMEErrorProvider.Clear()
        End If

    End Sub

    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        If Edit_butt.Text = EditState Then
            Edit_butt.Text = "تأكيد التعديلات"
            FieldsPanel.Enabled = True
        Else
            If ValidateChildren() = True Then

                If String.IsNullOrWhiteSpace(Barcode_txt.Text) = False Then
                    IM_Check_UpdateBarcode()
                Else
                    Update_AG()
                End If

            End If

        End If
    End Sub

    Private Sub Delete_butt_Click(sender As Object, e As EventArgs) Handles Delete_butt.Click

        If MessageBox.Show(" سيتم حذف العميل " + AG_Name + " نهائيا من النظام ... متأكد ", "", MessageBoxButtons.OKCancel, _
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
            Delete_AG()
        End If

    End Sub


    Private Sub AG_Phone_TextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles AG_Phone_TextBox.KeyDown
        If e.KeyCode = Keys.Return Or e.KeyCode = Keys.Down Then AG_AddressTextBox.Select()
        If e.KeyCode = Keys.Up Then IM_SH_txt.Select()
    End Sub


    Private Sub AG_AddressTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles AG_AddressTextBox.KeyDown
        If e.KeyCode = Keys.Return Then Email_txt.Select()
    End Sub


    Private Sub isSalaryMRadio_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Left Or e.KeyCode = Keys.Return Then SalaryTextBox.Select()
    End Sub


    Private Sub isRegularAGMRadio_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Up Then AG_AddressTextBox.Select()
    End Sub

    Private Sub AG_BalanceTextBox_TextChanged(sender As Object, e As EventArgs) Handles AG_BalanceTextBox.TextChanged

        Dim AG_Balance As String = Convert.ToDouble(AG_BalanceTextBox.Text)

        If AG_Balance < 0 Then
            AG_BalanceTextBox.ForeColor = Color.DarkRed
        ElseIf AG_Balance = 0 Then
            AG_BalanceTextBox.ForeColor = Color.Black
        Else
            AG_BalanceTextBox.ForeColor = Color.DarkGreen
        End If
    End Sub

    Private Sub Barcode_txt_TextChanged(sender As Object, e As EventArgs) Handles Barcode_txt.TextChanged
        BarError.Clear()
    End Sub

    Private Sub Barcode_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_txt.KeyDown
        If e.KeyCode = Keys.Return Then AG_Phone_TextBox.Select()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        On Error Resume Next
        With Me.OpenFileDialog1
            .Filter = "(Image Files)|*.jpg;*.png;*.bmp;*.gif;*.ico|Jpg, | *.jpg|Png, | *.png|Bmp, | *.bmp|Gif, | *.gif|Ico | *.ico"
            .FilterIndex = 1
            .Multiselect = False
            .Title = "إختر صورة مرفقة للفاتورة"
            .ShowDialog()
            If Len(.FileName) > 0 Then
                BillPictureBox.Image = Image.FromFile(OpenFileDialog1.FileName)
            End If
        End With
    End Sub

    Private Sub None_Img_btn_Click(sender As Object, e As EventArgs) Handles None_Img_btn.Click
        If BillPictureBox.Image IsNot Nothing Then
            Beep()
            If MessageBox.Show(" حذف الصورة ؟ ", "", MessageBoxButtons.YesNo, _
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
                BillPictureBox.Image = My.Resources.person
            End If
        End If
    End Sub

    Private Sub AG_Type_cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles AG_Type_cm.SelectedValueChanged
        'On Error Resume Next
        If TypeName(AG_Type_cm.SelectedValue) = "Integer" Then
            If AG_Type_cm.SelectedValue = 4 Then
                Salary_GP.Visible = True
            Else
                Salary_GP.Visible = False
            End If
        End If

    End Sub

    'Public Sub Load_IM()
    '    Dim c As New C

    '    Try
    '        IM_Dt.Clear()
    '        Dim s As String
    '        s = "select AG_ID,Ag_name,isnull(T_Balance,0) AS T_Balance from Agents WHERE Ag_name Like '%" & IM_SH_txt.Text & "%'"
    '        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
    '        c.Da.Fill(IM_Dt)
    '        IMDataGridViewX.DataSource = IM_Dt
    '        If IM_Dt.Rows.Count > 0 Then
    '            IMDataGridViewX.Visible = True
    '            IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
    '        Else
    '            IMDataGridViewX.Visible = False
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub IM_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles IM_SH_txt.KeyDown

        'If e.KeyCode = Keys.Down Then If IMDataGridViewX.Visible = True Then IMDataGridViewX.Select()
        'If e.KeyCode = Keys.Return Then If IMDataGridViewX.Visible = True Then Fetch_ItemToList(IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value)

        If e.KeyCode = Keys.Return Then Barcode_txt.Select()
    End Sub

    Private Sub IM_SH_txt_Enter(sender As Object, e As EventArgs) Handles IM_SH_txt.Enter
        Set_Ar_Language()
    End Sub

    Private Sub IM_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles IM_SH_txt.TextChanged
        'If IM_SH_txt.Text.Count > 0 Then
        '    Load_IM()
        'Else
        '    IMDataGridViewX.Visible = False
        '    AG_ID = 0
        '    AG_Type_cm.SelectedIndex = 0
        'End If
        AG_NAMEErrorProvider.Clear()
       
    End Sub

    Private Sub IMDataGridViewX_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles IMDataGridViewX.CellClick
        Fetch_ItemToList(IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value)
    End Sub

    Private Sub IMDataGridViewX_KeyDown(sender As Object, e As KeyEventArgs) Handles IMDataGridViewX.KeyDown
        If e.KeyCode = Keys.Return Then
            Fetch_ItemToList(IMDataGridViewX.CurrentRow.Cells("IM_ID_CL").Value)
            IM_SH_txt.Select()
        End If

        If e.KeyCode = Keys.Up Then If IMDataGridViewX.CurrentRow.Index = 0 Then IM_SH_txt.Select()
    End Sub


    Private Sub Show_IM_btn_Click(sender As Object, e As EventArgs)
        'If IMDataGridViewX.Visible = True Then
        '    IMDataGridViewX.Visible = False
        'Else
        '    IMDataGridViewX.Visible = True
        '    Load_ALL_AG()
        'End If
    End Sub

    Public Sub Load_ALL_AG()
        Dim c As New C

        Try
            IM_Dt.Clear()
            Dim s As String
            s = "select AG_ID,Ag_name,Barcode from Agents where AG_ID NOT IN (SELECT TOP 1 AG_ID FROM Agents)"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(IM_Dt)
            IMDataGridViewX.DataSource = IM_Dt
            'If IM_Dt.Rows.Count > 0 Then
            '    IMDataGridViewX.Visible = True
            '    IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
            'Else
            '    IMDataGridViewX.Visible = False
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Max_Debit_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Max_Debit_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Max_Debit_txt_TextChanged(sender As Object, e As EventArgs) Handles Max_Debit_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Back_ADD_New_IM_btn_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub is_Emp_Pause_CB_CheckedChanged(sender As Object, e As EventArgs) Handles is_Emp_Pause_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Add_Prev_Balance_btn_Click(sender As Object, e As EventArgs) Handles Add_Prev_Balance_btn.Click

        ' If AG_ID > 1 Then
        Add_Prev_Balance.AG_NAME = AG_Name
            Add_Prev_Balance.Text = Add_Prev_Balance.Text + " : " + AG_Name
            Add_Prev_Balance.ShowDialog()
        '  End If

    End Sub

    Private Sub Refreg_Balance_btn_Click(sender As Object, e As EventArgs) Handles Refreg_Balance_btn.Click
        If AG_ID <> Default_AG_ID Then Refreg_Balance_AG()
    End Sub

    Private Sub Refreg_Balance_AG()
        Dim C As New C

        With C.Com
            .CommandText = "[SET_Agent_balance]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@AG_ID", AG_ID)
        End With


        If SQL_SP_EXEC(C.Com) = True Then
            Fetch_ItemToList(AG_ID)
            MsgBox("تم التحديث", MsgBoxStyle.Information)
        End If

    End Sub

    Private Sub Search_By_Acc_Name_txt_TextChanged(sender As Object, e As EventArgs) Handles Search_By_Acc_Name_txt.TextChanged
        Dim Dv As DataView
        Dv = IM_Dt.AsDataView
        Dv.RowFilter = IM_Serach_Filter(Search_By_Acc_Name_txt.Text, "[Ag_name]")
        IMDataGridViewX.DataSource = Dv
    End Sub

    Public Function IM_Serach_Filter(IM_SH As String, _SearchColumn As String) As String
        Dim words As String() = IM_SH.Split(New Char() {" "c})
        Dim Str As String = ""
        Dim IM_Str As String = ""
        Dim S_and As String = " and " & _SearchColumn & " Like "

        If words.Length() = 1 Then
            Str = _SearchColumn & "  = '" & IM_SH & "'  or " & _SearchColumn & "  like '%" & words(0) & "%' or " & _SearchColumn & "  like '%" & IM_SH & "' or " & _SearchColumn & "  like '" & IM_SH & "%'"
        Else
            IM_Str = "'%" & words(0) & "%'" & S_and

            For i = 1 To words.Length - 1

                If i = words.Length - 1 Then
                    IM_Str += "'%" & words(i) & "%'"
                Else
                    IM_Str += "'%" & words(i) & "%'" & S_and
                End If
            Next


            Str = _SearchColumn & "  like " & IM_Str

        End If
        Return Str
    End Function

    Private Sub Search_By_Acc_Code_txt_TextChanged(sender As Object, e As EventArgs) Handles Search_By_Acc_Code_txt.TextChanged
        Dim Dv As DataView
        Dv = IM_Dt.AsDataView
        Dv.RowFilter = IM_Serach_Filter(Search_By_Acc_Code_txt.Text, "[Barcode]")
        IMDataGridViewX.DataSource = Dv
    End Sub

    Private Sub Show_ag_menuCB_CheckedChanged(sender As Object, e As EventArgs) Handles Show_ag_menuCB.CheckedChanged
        CB_CHecked(sender)
    End Sub
End Class