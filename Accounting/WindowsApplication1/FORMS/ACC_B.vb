Imports System.Data.SqlClient
Imports System.Drawing.Printing


Public Class ACC_B
    Dim DT As New DataTable
    Dim ACC_CODE_DT As New DataTable
    Public is_Select As Boolean = False
    Dim b_balanced_str = "القيـــد مـــوزون"
    Dim b_balanced_not_str = "القيـــد غير موزون"

    Dim b_Depend_str = "القيـــد معتمد"
    Dim b_Depend_not_str = "القيـــد غير معتمد"

    Public Selected_ACC_CODE As String = ""

    '=========================================================
    ' Budget Journal UI/Lock State
    '=========================================================
    Private CurrentJournalIsBudgetJournal As Boolean = False
    Private CurrentJournalIsReverseJournal As Boolean = False
    Private CurrentBudgetEntryId As Integer = 0
    Private CurrentBudgetInfoText As String = ""
    Private CurrentReverseInfoText As String = ""
    Private CurrentJournalTypeText As String = "قيد عادي"
    Private BudgetJournalToolLabel As ToolStripLabel = Nothing
    Private CurrentAccBPermissions As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
    Private IsApplyingAccBPermissions As Boolean = False

    Private Const AccBApprovePermission As String = "ACC_B.APPROVE"
    Private Const AccBPrintPermission As String = "ACC_B.PRINT"
    Private Const AccBEditApprovalPermission As String = "ACC_B.EDIT_APPROVAL"
    Private Const AccBReversePermission As String = "ACC_B.REVERSE"


    Dim clb As New CheckedListBox()
    Dim print_cmb As New ComboBox()

    Private WithEvents PD As New PrintDocument
    Private PPD As New PrintPreviewDialog
    Private CurrentRow As Integer = 0
    Private PageNumber As Integer = 1
    Private TotalPages As Integer = 1
    Private PrintableRows As New List(Of Integer)
    Private PrintableColumns As New List(Of PrintColumnInfo)
    Private CurrentPrintLandscape As Boolean = True
    Private Const PrintLandscapeMenuItemName As String = "PrintLandscapeMenuItem"
    Private Const PrintPortraitMenuItemName As String = "PrintPortraitMenuItem"
    Private Const PrintOrientationSeparatorName As String = "PrintOrientationSeparator"

    Private Class PrintColumnInfo
        Public Property ColumnName As String
        Public Property HeaderText As String
        Public Property SourceWidth As Integer
        Public Property IsSerial As Boolean
    End Class

    Private Sub ADD_Btn_Click(sender As Object, e As EventArgs) Handles ADD_Btn.Click

        If BlockBudgetJournalAction("إضافة أو تعديل تفاصيل القيد") Then Exit Sub

        SELECT_ACC_NATURAL()
        If B_Name_Cm.Tag = 1 Then Exit Sub


        If ValidateChildren() = True Then
            If Not ValidateManualJournalAccount(B_NUM_txt.Text, "حساب القيد") Then Exit Sub
            If Not ValidateUserJournalAccountPermission(B_NUM_txt.Text, "حساب القيد") Then Exit Sub

            If String.IsNullOrWhiteSpace(DEBIT_txt.Text) And String.IsNullOrWhiteSpace(CREDIT_txt.Text) Then
                'MsgBox("حدد قيمة القيد", MsgBoxStyle.Critical, "")
                Dim notification3 As New NotificationForm("خطأ", " حدد قيمة  الحساب ", "bottom", True)
                notification3.ShowNotification()
                Exit Sub
            End If
            Prepare_to_add()
            ACC_BALANCE_proc("")
            B_NUM_txt.Select()
        End If




    End Sub

    Private Sub B_Name_Cm_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles B_Name_Cm.Validating
        If B_Name_Cm.SelectedIndex = -1 Then
            ACC_CODE_ErrorProvider.SetError(B_Name_Cm, " أدخل حساب للقيــد ")
            B_Name_Cm.Select()
            e.Cancel = True
        Else
            e.Cancel = False
            ACC_CODE_ErrorProvider.Clear()
        End If

    End Sub

    Private Sub B_NUM_txt_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles B_NUM_txt.Validating

        If String.IsNullOrWhiteSpace(B_NUM_txt.Text) = True Then
            ACC_CODE_NUM_ErrorProvider.SetError(B_NUM_txt, " أدخل رقم الحساب ")
            B_NUM_txt.Select()
            e.Cancel = True
        ElseIf B_NUM_txt.Text <> B_Name_Cm.SelectedValue Then

            ACC_CODE_NUM_ErrorProvider.SetError(B_NUM_txt, " تحقق من صحة إدخال رقم الحساب ")
            B_NUM_txt.Select()
        Else
            e.Cancel = False
            ACC_CODE_NUM_ErrorProvider.Clear()
        End If

    End Sub

    Private Sub Prepare_to_add()
        If Not String.IsNullOrWhiteSpace(Currency_Equal_txt.Text) Then

            If Convert.ToDouble(Currency_Equal_txt.Text) = 0 Then Currency_Equal_txt.Text = "1"

        Else
            Currency_Equal_txt.Text = "1"
        End If
    End Sub

    Private Sub ACC_BALANCE_proc(Process As String)
        Dim C As New C


        With C.Com
            .Connection = C.Con
            .CommandText = "[ACC_BALANCE_proc]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID_Details_txt.Text)
            .Parameters.AddWithValue("@B_T_ID", T_ID_txt_2.Text)
            .Parameters.AddWithValue("@DATE", Date_.Value)
            .Parameters.AddWithValue("@ACC_CODE", B_NUM_txt.Text)
            If Not String.IsNullOrWhiteSpace(DEBIT_txt.Text) Then .Parameters.AddWithValue("@DEBIT", Convert.ToDouble(DEBIT_txt.Text) * Convert.ToDouble(Currency_Equal_txt.Text))
            If Not String.IsNullOrWhiteSpace(CREDIT_txt.Text) Then .Parameters.AddWithValue("@CREDIT", Convert.ToDouble(CREDIT_txt.Text) * Convert.ToDouble(Currency_Equal_txt.Text))
            .Parameters.AddWithValue("@USER_ID", USER_ID)
            .Parameters.AddWithValue("@IS_VOID", 0)
            .Parameters.AddWithValue("@Currency", 1)
            .Parameters.AddWithValue("@Notes", Notes_txt.Text)
            .Parameters.AddWithValue("@Notes_MASTER", M_Notes_txt.Text)
            .Parameters.AddWithValue("@Process", Process)
            .Parameters.AddWithValue("@Bill_Num", Bill_Num_txt.Text)
            .Parameters.AddWithValue("@COST_ID", COST_CM.SelectedValue)
            .Parameters.AddWithValue("@Cr_ID", Currency_Cm.SelectedValue)
            .Parameters.AddWithValue("@Currency_Equal", Currency_Equal_txt.Text)

            .Parameters.AddWithValue("@NextNumber", "")

            .Parameters.AddWithValue("@OP_Status", 1)
            .Parameters.Add("@ERROR_MSG", SqlDbType.NVarChar, 500)
            .Parameters("@OP_Status").Direction = ParameterDirection.Output
            .Parameters("@ERROR_MSG").Direction = ParameterDirection.Output
            .Parameters("@NextNumber").Direction = ParameterDirection.Output

            C.Con.Open()
            T_ID_txt_2.Text = C.Com.ExecuteScalar()
            C.Con.Close()


            If C.Com.Parameters("@OP_Status").Value.ToString() = "0" Then
                ' MsgBox(C.Com.Parameters("@ERROR_MSG").Value.ToString(), MsgBoxStyle.Critical, "خطــأ")
                Dim notification3 As New NotificationForm("خطأ", C.Com.Parameters("@ERROR_MSG").Value.ToString(), "bottom", True)
                notification3.ShowNotification()
            Else
                Clear_Fields()
                SELECT_Balance()
            End If

            'If Process = "DEPEND" Then MsgBox("تم إعتمــاد القيــد", MsgBoxStyle.Information, "") End

        End With
    End Sub

    Private Sub ACC_B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If SELECT_ARCHIVE_COUNTER(Identifiers.F_YEAR) > 0 Then
            Dim notification3 As New NotificationForm("خطأ", "يوجد أرشيف لهذه السنة ... لا يمكن إجراء قيود إلا بعد استرجاعها", "bottom", True)
            notification3.ShowNotification()
            Me.BeginInvoke(New MethodInvoker(AddressOf Me.Close))
        End If

        FYear_Txt_Tool.Text = Identifiers.F_YEAR
        Load_Balances()
        If is_Select = True Then
            T_ID_txt_2.Text = T_ID_Search
            SELECT_Balance()

            If Not String.IsNullOrWhiteSpace(Selected_ACC_CODE) Then

                For Each row As DataGridViewRow In DataGridView1.Rows
                    If row.Cells("ACC_CODE_CL").Value IsNot Nothing AndAlso row.Cells("ACC_CODE_CL").Value.ToString() = Selected_ACC_CODE Then
                        row.Selected = True
                        Exit For
                    End If
                Next

            End If

        Else
            T_ID_txt_2.Text = Load_Balances_MAX_ID()
        End If

        COST_CM.Select()
        PreparePrintMenu()


        '------------------------------------------------------

        'clb.Items.Add("عرض القيمة بالحروف فالطباعة", MY_Settings.is_Print_ACC_B_Letters)
        'clb.Height = 60

        '' ✅ إنشاء ComboBox
        ''Dim cmb As New ComboBox()
        'print_cmb.FlatStyle = FlatStyle.Flat
        'print_cmb.Items.AddRange({"طباعة أفقية", " طباعة عمودية"})
        'print_cmb.DropDownStyle = ComboBoxStyle.DropDownList
        'print_cmb.SelectedIndex = 0
        'print_cmb.Width = 120

        '' ✅ استضافة الأدوات داخل ToolStripControlHost
        'Dim hostCombo As New ToolStripControlHost(print_cmb)
        'Dim hostClb As New ToolStripControlHost(clb)

        '' ✅ إنشاء القائمة المنسدلة ToolStripDropDown
        'Dim dropDown As New ToolStripDropDown()
        'dropDown.Items.Add(hostCombo)
        'dropDown.Items.Add(New ToolStripSeparator()) ' خط فاصل اختياري
        'dropDown.Items.Add(hostClb)

        '' ✅ زر في الـ ToolStrip لفتح القائمة
        'Dim btn As New ToolStripDropDownButton("خيارات")
        'btn.DropDown = dropDown
        'ToolStrip1.Items.Add(btn)
        'print_cmb.SelectedIndex = MY_Settings.ACC_B_printer_Type
        'CurrentPrintLandscape = (print_cmb.SelectedIndex = 0)
        'PreparePrintMenu()
        'InitializeBudgetJournalUi()
        'LoadAccBPermissions()
        'ApplyAccBPermissions()



        '' ✅ مثال: التقاط حدث تغيير اختيار ComboBox
        'AddHandler print_cmb.SelectedIndexChanged,
        '    Sub()
        '        MY_Settings.ACC_B_printer_Type = print_cmb.SelectedIndex
        '        CurrentPrintLandscape = (print_cmb.SelectedIndex = 0)
        '        MY_Settings.Save_AppSetting()
        '        'MessageBox.Show("تم اختيار: " & print_cmb.SelectedItem.ToString())
        '    End Sub


        '' ✅ مثال: التقاط تغيير CheckBox
        'AddHandler clb.ItemCheck,
        '    Sub(s, eArgs)
        '        Dim itemText = clb.Items(eArgs.Index).ToString()
        '        'If eArgs.NewValue = CheckState.Checked Then
        '        '    MessageBox.Show("تم تحديد: " & itemText)
        '        'Else
        '        '    MessageBox.Show("تم إلغاء التحديد: " & itemText)

        '        'MsgBox(clb.GetItemChecked(0).ToString)
        '        MY_Settings.is_Print_ACC_B_Letters = Not clb.GetItemChecked(0)
        '        MY_Settings.Save_AppSetting()
        '        'End If
        '    End Sub

    End Sub

    Private Function Load_Balances_MAX_ID()
        Dim C = New C
        Try
            Dim S As String = "SELECT ISNULL(MAX(T_ID),0) + 1 AS MX FROM ACC_BALANCE_MASTER "
            C.Com = New SqlClient.SqlCommand(S, C.Con)
            C.Con.Open()
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                Return C.Dr("MX")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return 0
    End Function


    Public Sub SELECT_Balance()
        If Not String.IsNullOrWhiteSpace(T_ID_txt_2.Text) Then


            Enable_Fields(True)
            Clear_Fields()

            DT = New DataTable
            Dim C As New C

            Dim da As New SqlClient.SqlDataAdapter("SELECT [T_ID],[DATE_IN] ,CONVERT(DATE,DATE) as DATE ,[COST_NAME],[Cr_Name],[Currency_Equal],[MASTER_NOTES],[ACC_CODE] ,[ACC_NAME], " &
                                                " [Bill_Num],[CREDIT],[DEBIT],[Notes],ACC_Depend_Status,is_Depended,COST_ID,Currency_ID,UserName,USER_DEPENDED,Receipt_Num,JournalNumber FROM " &
                                                " [dbo].[ACC_BALANCE_V] WHERE B_T_ID = " & T_ID_txt_2.Text & " ORDER BY Debit ASC ", C.Con)
            da.Fill(DT)

            If DT.Rows.Count > 0 Then

                DataGridView1.DataSource = DT

                Depended_Label_2.Visible = True

                M_Notes_txt.Text = DT(0)("MASTER_NOTES")
                Date_.Text = DT(0)("DATE")

                Depended_Label_2.Text = DT(0)("ACC_Depend_Status")
                COST_CM.SelectedValue = DT(0)("COST_ID")

                Input_User_Txt.Text = DT(0)("UserName")
                Depended_User_Txt.Text = DT(0)("USER_DEPENDED")

                If DT(0)("is_Depended") = 0 Then
                    Depended_Label_2.Text = b_Depend_not_str
                    Depended_Label_2.ForeColor = Color.DarkRed

                    Enable_Fields(True)
                Else
                    Depended_Label_2.Text = b_Depend_str
                    Depended_Label_2.ForeColor = Color.DarkGreen
                    Enable_Fields(False)
                End If

                If Not IsDBNull(DT(0)("JournalNumber")) Then NextNumber_TextBox.Text = DT(0)("JournalNumber")

                If DataGridView1.Rows.Count > 0 Then
                    DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells("ACC_CODE_CL")
                    DataGridView1.Columns("Receipt_Num").Visible = False
                    DataGridView1.Columns("JournalNumber").Visible = False
                    For Each col As DataGridViewColumn In DataGridView1.Columns
                        col.SortMode = DataGridViewColumnSortMode.NotSortable
                    Next

                End If

                ReceiptNum_Txt.Text = DT(0)("Receipt_Num")

                LoadBudgetJournalInfo(Convert.ToInt32(T_ID_txt_2.Text))

                UcGridColumnsSelector1.BindGrid(
DataGridView1,
New List(Of String) From {"T_ID_CL", "ACC_Depend_Status_CL", "is_Depended_CL", "Currency_ID_CL", "COST_ID_CL", "UserName_CL", "USER_DEPENDED_CL", "JournalNumber", "Receipt_Num"},
Me.Name.ToString
)
            Else
                ResetBudgetJournalState()
                EDIT_Btn.Enabled = False
                Depended_Label_2.Visible = False
                T_ID_txt_2.Focus()
                Currency_Cm.Enabled = True
            End If

        End If
    End Sub


    Private Sub Enable_Fields(f As Boolean)
        M_Notes_txt.Enabled = f
        Fields_Panel.Enabled = f
        'Grid_GroupBox.Enabled = f
        ADD_Btn.Enabled = f
        REMOVE_BTN.Enabled = f
        Depend_Btn.Enabled = f
        Edit_title_date_Btn.Enabled = f
        EDIT_Btn.Enabled = Not f
        'Currency_Cm.Enabled = f
        'Currency_Equal_txt.Enabled = f

        ApplyBudgetJournalUiState()
        ApplyAccBPermissions()
    End Sub

    Private Sub LOAD_ALL_BALANCES()

        ACC_CODE_DT.Clear()
        ACC_CODE_DT = Accounts_Datatable

    End Sub

    Public Sub Load_Balances()


        COST_CM.DataSource = CostCenter_Datatable
        COST_CM.DisplayMember = "COST_NAME"
        COST_CM.ValueMember = "COST_ID"

        Currency_Cm.DataSource = Currencies_Datatable
        Currency_Cm.DisplayMember = "Cr_Name"
        Currency_Cm.ValueMember = "Cr_ID"

        LOAD_ALL_BALANCES()
    End Sub


    Private Sub B_Name_Cm_KeyDown(sender As Object, e As KeyEventArgs) Handles B_Name_Cm.KeyDown
        If e.KeyCode = Keys.Return Then
            If TypeName(B_Name_Cm.SelectedValue) = "String" Then
                B_NUM_txt.Text = B_Name_Cm.SelectedValue
                SELECT_ACC_NATURAL()

                If B_Name_Cm.SelectedIndex = -1 Then
                    Credit_Label.Visible = False
                    Debit_Label.Visible = False
                End If

                B_Name_Cm.DroppedDown = False

            End If
        End If

        If e.KeyCode = Keys.Right Then If B_Name_Cm.SelectionStart = 0 Then B_NUM_txt.Select()


    End Sub


    Private Sub B_NUM_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles B_NUM_txt.KeyDown
        If e.KeyCode = Keys.Return Then If ACC_CODE_DT.Rows.Count > 0 Then B_Name_Cm.SelectedValue = B_NUM_txt.Text
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles NEW_Btn.Click
        ResetBudgetJournalState()
        Enable_Fields(True)
        Clear_Fields()
        ResetBudgetJournalState()
        T_ID_txt_2.Text = Load_Balances_MAX_ID()
        Currency_Cm.Enabled = True
        Currency_Equal_txt.Enabled = True
    End Sub

    Private Sub Clear_Input_Fields()
        For Each a As Control In Fields_Panel.Controls
            If TypeOf a Is TextBox Then
                a.Text = ""
            End If
        Next

        '  B_Name_Cm.BackColor = System.Drawing.Color.Gainsboro
        B_Name_Cm.Tag = 0
    End Sub


    Private Sub Clear_Fields()

        Clear_Input_Fields()

        Rows_txt.Clear()
        Total_B_txt.Text = 0
        Total_C_txt.Text = 0
        Total_D_txt.Text = 0

        TOTAL_C_N.Text = 0
        TOTAL_D_N.Text = 0

        Date_.Value = Date.Now
        B_Name_Cm.SelectedIndex = -1

        DT.Clear()
        DT = New DataTable

        T_ID_Details_txt.Text = 0
        M_Notes_txt.Clear()
        ReceiptNum_Txt.Clear()

        Depended_Label_2.Visible = False
        b_status_Label_2.Visible = False

        NextNumber_TextBox.Clear()

        'SELECT_Balance()
    End Sub

    Dim tmp_ID

    Private Sub Copy_btn_Click(sender As Object, e As EventArgs) Handles Copy_btn.Click
        Notes_txt.Text = M_Notes_txt.Text
    End Sub

    Private Sub DataGridView1_DataSourceChanged(sender As Object, e As EventArgs) Handles DataGridView1.DataSourceChanged


        If DT.Rows.Count > 0 Then
            b_status_Label_2.Visible = True
            Compute_Balance(DT)
            Total_C_txt.Text = T_CREDIT.ToString()
            Total_D_txt.Text = T_DEBIT.ToString()
            TOTAL_C_N.Text = Module1.TOTAL_C_N
            TOTAL_D_N.Text = Module1.TOTAL_D_N

            Rows_txt.Text = DT.Rows.Count

            Total_B_txt.Text = Convert.ToDouble(Total_D_txt.Text) - Convert.ToDouble(Total_C_txt.Text)
        Else
            b_status_Label_2.Visible = False
            Total_C_txt.Text = 0
            Total_D_txt.Text = 0
            Total_B_txt.Text = 0
            Rows_txt.Text = 0
            TOTAL_C_N.Text = 0
            TOTAL_D_N.Text = 0
        End If

        If Total_C_txt.Text = Total_D_txt.Text Then
            b_status_Label_2.Text = b_balanced_str
            'b_status_Label_2.BackColor = Drawing.Color.PaleGreen
            b_status_Label_2.ForeColor = Color.DarkGreen
        Else
            b_status_Label_2.Text = b_balanced_not_str
            'b_status_Label_2.BackColor = Drawing.Color.LightCoral
            b_status_Label_2.ForeColor = Color.DarkRed
        End If

        If DataGridView1.ColumnCount > 0 Then

            If DataGridView1.Rows.Count > 0 Then
                Currency_Cm.SelectedValue = DataGridView1.Rows(0).Cells("Currency_ID_CL").Value
                Currency_Equal_txt.Text = DataGridView1.Rows(0).Cells("Currency_Equal_CL").Value
                'Currency_Cm.Enabled = False
                Currency_Equal_txt.Enabled = False
            Else
                'Currency_Cm.Enabled = True
                Currency_Equal_txt.Enabled = True
            End If

        Else
            'Currency_Cm.Enabled = True
            Currency_Equal_txt.Enabled = True

        End If

    End Sub

    Private Sub REMOVE_BTN_Click(sender As Object, e As EventArgs) Handles REMOVE_BTN.Click
        If BlockBudgetJournalAction("حذف تفاصيل القيد") Then Exit Sub

        If DataGridView1.Rows.Count > 0 Then
            If MessageBox.Show(" تأكيد حذف السجـــل ... " & vbNewLine & DataGridView1.CurrentRow.Cells("ACC_NAME_CL").Value, "تاكيــد العملية", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.OK Then

                ACC_BALANCE_proc_DELETE(DataGridView1.CurrentRow.Cells("ACC_NAME_CL").Value)
            End If
        End If
    End Sub

    Private Sub ACC_BALANCE_proc_DELETE(ACC_NAME As String)
        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "[ACC_BALANCE_proc]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", DataGridView1.CurrentRow.Cells("T_ID_CL").Value)
            .Parameters.AddWithValue("@B_T_ID", T_ID_txt_2.Text)
            .Parameters.AddWithValue("@Process", "DELETE")
            .Parameters.AddWithValue("@OP_Status", 1)
            .Parameters.Add("@ERROR_MSG", SqlDbType.NVarChar, 500)
            .Parameters.AddWithValue("@NextNumber", "")

            .Parameters("@OP_Status").Direction = ParameterDirection.Output
            .Parameters("@ERROR_MSG").Direction = ParameterDirection.Output
            .Parameters("@NextNumber").Direction = ParameterDirection.Output

            C.Con.Open()
            C.Com.ExecuteScalar()
            C.Con.Close()

            If C.Com.Parameters("@OP_Status").Value.ToString() = "0" Then
                MsgBox(C.Com.Parameters("@ERROR_MSG").Value.ToString(), MsgBoxStyle.Critical, "خطــأ")
            Else
                Dim notification3 As New NotificationForm("تنويه", " تم حذف السجل " & ACC_NAME, "bottom")
                notification3.ShowNotification()

            End If

            SELECT_Balance()

        End With
    End Sub

    Private Sub SEARCH_ACC_BTN_Click(sender As Object, e As EventArgs) Handles SEARCH_ACC_BTN.Click
        'ACC_CODE_Search = ""
        MOVE_TO_ACCOUNTS_MENU()
    End Sub


    Private Sub MOVE_TO_ACCOUNTS_MENU()
        BALANCE_SEARCH.ShowDialog()
        If ACC_CODE_Search <> "" Then B_NUM_txt.Text = ACC_CODE_Search
    End Sub



    Private Sub B_NUM_txt_TextChanged(sender As Object, e As EventArgs) Handles B_NUM_txt.TextChanged

        If B_NUM_txt.Text.Count > 0 Then
            Filter_B()
        Else
            LOAD_ALL_BALANCES()
        End If
        ACC_CODE_NUM_ErrorProvider.Clear()
    End Sub

    Private Sub Filter_B()

        ACC_CODE_DT = GetAccountTreeDataTable(Accounts_Datatable, B_NUM_txt.Text)

        B_Name_Cm.DataSource = ACC_CODE_DT
        B_Name_Cm.DisplayMember = "ACC_NAME"
        B_Name_Cm.ValueMember = "ACC_CODE"
        B_Name_Cm.DroppedDown = True
        If ACC_CODE_DT.Rows.Count = 0 Then B_Name_Cm.Text = ""

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
        Try
            If DataGridView1.Columns(e.ColumnIndex).Name = "دائن" Then
                If Not IsDBNull(e.Value) Then
                    e.CellStyle.BackColor = Color.FromArgb(255, 192, 192)
                    e.CellStyle.ForeColor = Drawing.Color.DarkRed
                    'ElseIf e.Value = "تم اعداد التقرير النهائي" Then
                    '    e.CellStyle.BackColor = Drawing.Color.DarkGreen
                    '    e.CellStyle.ForeColor = Drawing.Color.White

                End If
            End If

            If DataGridView1.Columns(e.ColumnIndex).Name = "مدين" Then
                If Not IsDBNull(e.Value) Then
                    e.CellStyle.BackColor = Color.FromArgb(192, 255, 192)
                    e.CellStyle.ForeColor = Drawing.Color.DarkGreen
                    'ElseIf e.Value = "تم اعداد التقرير النهائي" Then
                    '    e.CellStyle.BackColor = Drawing.Color.DarkGreen
                    '    e.CellStyle.ForeColor = Drawing.Color.White

                End If
            End If


        Catch ex As Exception

        End Try
    End Sub


    Private Sub Depend_Btn_Click(sender As Object, e As EventArgs) Handles Depend_Btn.Click
        If BlockAccBPermission(AccBApprovePermission, "اعتماد قيد") Then Exit Sub
        If BlockBudgetJournalAction("اعتماد قيد صرف الميزانية من شاشة القيود") Then Exit Sub

        If Total_B_txt.Text <> 0 Then

            Dim notification3 As New NotificationForm("خطــأ فالإعتمــاد", " القيــد غير مــوزون ", "bottom", True)
            notification3.ShowNotification()

            'MsgBox("القيــد غير مــوزون", MsgBoxStyle.Critical, "خطــأ فالإعتمــاد")
            Exit Sub
        End If
        If MessageBox.Show("سيتم إعتماد القيــد رقم ( " & T_ID_txt_2.Text & " ) ولن يتم التعديل فيه بعد الأن .. هل أنت متاكد ", "تاكيــد العملية", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.OK Then
            ACC_BALANCE_DEPEND(True)
        End If

    End Sub




    Private Sub ACC_BALANCE_DEPEND(Depended As Boolean)
        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "[ACC_BALANCE_DEPEND]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", T_ID_txt_2.Text)
            .Parameters.AddWithValue("@Depended", Depended)
            .Parameters.AddWithValue("@USER_ID", USER_ID)
            .Parameters.AddWithValue("@OP_Status", 1)
            .Parameters.Add("@ERROR_MSG", SqlDbType.NVarChar, 500)
            .Parameters.Add("@NextNumber", SqlDbType.NVarChar, 500)

            .Parameters("@OP_Status").Direction = ParameterDirection.Output
            .Parameters("@ERROR_MSG").Direction = ParameterDirection.Output
            .Parameters("@NextNumber").Direction = ParameterDirection.Output

            SQL_SP_EXEC(C.Com)

            If C.Com.Parameters("@OP_Status").Value.ToString() = "0" Then
                Dim notification3 As New NotificationForm("خطأ فالتحرير", C.Com.Parameters("@ERROR_MSG").Value.ToString(), "bottom", True)
                notification3.ShowNotification()
            Else
                If Depended = True Then
                    Dim notification3 As New NotificationForm("إشعار", " تم إعتمــاد القيــد " & T_ID_txt_2.Text, "bottom")
                    notification3.ShowNotification()
                Else
                    Dim notification3 As New NotificationForm("إشعار", " تم تحريــر القيــد " & T_ID_txt_2.Text, "bottom")
                    notification3.ShowNotification()
                    Enable_Fields(True)
                End If
                Clear_Fields()
                SELECT_Balance()
            End If


        End With
    End Sub

    Private Sub DataGridView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseDoubleClick

        If BlockBudgetJournalAction("تعديل سطر من قيد صرف الميزانية") Then Exit Sub

        If DataGridView1.Rows.Count > 0 Then

            If Not String.IsNullOrWhiteSpace(T_ID_Details_txt.Text) Then
                If Convert.ToInt16(T_ID_Details_txt.Text) > 0 Then
                    Dim notification3 As New NotificationForm("خطــأ فالتعديل", " يوجد سجــل قيد التعديـــل ... قم بإدراته اولا ", "bottom", True)
                    notification3.ShowNotification()
                    '  MsgBox("يوجد سجــل قيد التعديـــل ... قم بإدراته اولا", MsgBoxStyle.Critical, "خطــأ فالتعديل")
                    Exit Sub
                End If
            End If


            If DataGridView1.CurrentRow.Cells("is_Depended_CL").Value = 0 Then

                'Clear_Fields()
                T_ID_Details_txt.Text = DataGridView1.CurrentRow.Cells("T_ID_CL").Value
                COST_CM.SelectedValue = DataGridView1.CurrentRow.Cells("COST_ID_CL").Value
                Bill_Num_txt.Text = DataGridView1.CurrentRow.Cells("Bill_Num_CL").Value
                B_NUM_txt.Text = DataGridView1.CurrentRow.Cells("ACC_CODE_CL").Value
                'B_Name_Cm.SelectedValue = DataGridView1.CurrentRow.Cells("ACC_CODE_CL").Value

                Currency_Cm.SelectedValue = DataGridView1.CurrentRow.Cells("Currency_ID_CL").Value
                Currency_Equal_txt.Text = DataGridView1.CurrentRow.Cells("Currency_Equal_CL").Value

                If Not IsDBNull(DataGridView1.CurrentRow.Cells("CREDIT_CL").Value) Then
                    CREDIT_txt.Text = Math.Round(DataGridView1.CurrentRow.Cells("CREDIT_CL").Value / Currency_Equal_txt.Text, 3)
                End If

                If Not IsDBNull(DataGridView1.CurrentRow.Cells("DEBIT_CL").Value) Then
                    DEBIT_txt.Text = Math.Round(DataGridView1.CurrentRow.Cells("DEBIT_CL").Value / Currency_Equal_txt.Text, 3)
                End If

                Notes_txt.Text = DataGridView1.CurrentRow.Cells("Notes_CL").Value
                DataGridView1.Rows.Remove(DataGridView1.CurrentRow)
                B_Name_Cm.DroppedDown = False

            End If


        End If
    End Sub

    Private Sub T_ID_Details_txt_TextChanged(sender As Object, e As EventArgs) Handles T_ID_Details_txt.TextChanged
        If T_ID_Details_txt.Text.Count > 0 Then
            If T_ID_Details_txt.Text > 0 Then
                Entry_Label.Text = "(تعديــل الإدخــال)"
                Entry_Label.BackColor = Color.LightGray
            Else

                Entry_Label.Text = "(إدخــال جديــد)"
                Entry_Label.BackColor = Color.LightYellow
            End If
        End If

    End Sub

    Private Sub Refresh_Btn_Click(sender As Object, e As EventArgs) Handles Refresh_Btn.Click
        'Clear_Fields()
        Clear_Input_Fields()
        SELECT_Balance()
    End Sub



    Private Sub Edit_title_date_Btn_Click(sender As Object, e As EventArgs) Handles Edit_title_date_Btn.Click
        If BlockBudgetJournalAction("تعديل تاريخ أو شرح قيد صرف الميزانية") Then Exit Sub

        ACC_BALANCE_proc("UPDATE_MASTER")
    End Sub

    Private Sub Cancel_Btn_Click(sender As Object, e As EventArgs) Handles EDIT_Btn.Click
        If BlockAccBPermission(AccBEditApprovalPermission, "تحرير قيد") Then Exit Sub
        If BlockBudgetJournalAction("تحرير قيد صرف الميزانية") Then Exit Sub

        If MessageBox.Show(" تأكيد التحرير ... سيتم إلغاء الإعتماد للقيد  ", "تاكيــد العملية", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.OK Then
            ACC_BALANCE_DEPEND(False)
        End If
    End Sub

    Private Sub DEBIT_txt_TextChanged(sender As Object, e As EventArgs) Handles DEBIT_txt.TextChanged
        If DEBIT_txt.Text.Count > 0 Then CREDIT_txt.Clear()
    End Sub

    Private Sub CREDIT_txt_TextChanged(sender As Object, e As EventArgs) Handles CREDIT_txt.TextChanged
        If CREDIT_txt.Text.Count > 0 Then DEBIT_txt.Clear()
    End Sub

    Private Sub Notes_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Notes_txt.KeyDown
        If e.KeyCode = Keys.Return Then
            If ADD_Btn.Enabled = True Then ADD_Btn_Click(sender, e)
        End If
    End Sub


    Private Sub B_Name_Cm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles B_Name_Cm.SelectedIndexChanged
        ACC_CODE_ErrorProvider.Clear()
    End Sub


    Private Sub SELECT_ACC_NATURAL()


        Dim C = New C
        Try
            Dim S As String = "SELECT ACC_NATURAL,is_Lock_Trans FROM [ACCOUNTS_TREE_V] WHERE ACC_CODE = " & B_Name_Cm.SelectedValue
            C.Com = New SqlClient.SqlCommand(S, C.Con)
            C.Con.Open()
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                If C.Dr("ACC_NATURAL") = "C" Then
                    Debit_Label.Visible = False
                    Credit_Label.Visible = True
                Else
                    Debit_Label.Visible = True
                    Credit_Label.Visible = False
                End If

                If C.Dr("is_Lock_Trans") = 1 Then
                    ' B_Name_Cm.BackColor = System.Drawing.Color.IndianRed
                    MsgBox("لا يمكن إضافة قيود لهذا الحساب", MsgBoxStyle.Critical, "القيد مقفل")
                    ADD_Btn.Enabled = False
                    B_Name_Cm.Tag = 1
                Else
                    '  B_Name_Cm.BackColor = System.Drawing.Color.Gainsboro
                    ADD_Btn.Enabled = True
                    B_Name_Cm.Tag = 0
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub B_NUM_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles B_NUM_txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub Currency_Cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles Currency_Cm.SelectedValueChanged

        If Currency_Cm.SelectedValue IsNot Nothing AndAlso Not TypeOf Currency_Cm.SelectedValue Is System.Data.DataRowView Then
            Currency_Equal_txt.Text = Get_Equal(Currency_Cm.SelectedValue, Date_, 0)
        End If
    End Sub

    Public Sub Print_B(Optional ByVal exportToExcel As Boolean = False)
        Try
            If DataGridView1.Rows.Count = 0 Then
                MessageBox.Show("لا توجد بيانات للطباعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Not exportToExcel Then
                PreparePrint()
                PPD.Document = PD
                PPD.WindowState = FormWindowState.Maximized
                PPD.ShowDialog()
                Exit Sub
            End If

            Dim pp As New ReportConnection
            pp.rp.Load(Application.StartupPath & "\Reports\ACC_B_" & print_cmb.SelectedIndex.ToString & ".rpt")
            pp.LoadTables()

            With pp
                .rp.SetParameterValue("TITLE_NUM", " قيــد يومية ")
                .rp.SetParameterValue("DATE", Date_.Text)
                .rp.SetParameterValue("Title_1", MY_Settings.SBill_Title_1)
                .rp.SetParameterValue("Title_2", MY_Settings.SBill_Title_2)
                .rp.SetParameterValue("Bill_ID", T_ID_txt_2.Text)
                .rp.SetParameterValue("T_CREDIT", Total_C_txt.Text)
                .rp.SetParameterValue("T_DEBIT", Total_D_txt.Text)
                .rp.SetParameterValue("TOTAL_D_N", TOTAL_D_N.Text)
                .rp.SetParameterValue("TOTAL_C_N", TOTAL_C_N.Text)
                .rp.SetParameterValue("T_ROWS", Rows_txt.Text)
                .rp.SetParameterValue("TITLE_Bill", M_Notes_txt.Text)
                .rp.SetParameterValue("USER_Input", Input_User_Txt.Text)
                .rp.SetParameterValue("User_Depended", Depended_User_Txt.Text)



                ' ✅ التحقق من أول CheckBox
                If IsPrintMoneyLettersEnabled() Then
                    ' مثلاً: إضافة باراميتر لو أول خيار محدد
                    .rp.SetParameterValue("Money_char", HANY(T_CREDIT, "LYD"))
                Else
                    .rp.SetParameterValue("Money_char", "")
                End If

                'If Money_Char_CB.Checked = True Then
                '    .rp.SetParameterValue("Money_char", HANY(T_CREDIT, "LYD")) 'Get_Currency_Tag(DataGridView1.CurrentRow.Cells("Currency_ID_CL").Value))
                'Else
                '    .rp.SetParameterValue("Money_char", "")
                'End If
            End With

            ' **تصدير التقرير إلى Excel بدلاً من الطباعة إذا تم اختيار التصدير**
            If exportToExcel Then
                Dim saveDialog As New SaveFileDialog()
                saveDialog.Filter = "Excel Files|*.xls"
                saveDialog.Title = "حفظ التقرير كملف Excel"
                saveDialog.FileName = "قيد رقم (" & T_ID_txt_2.Text & ").xls"

                If saveDialog.ShowDialog() = DialogResult.OK Then
                    Dim exportPath As String = saveDialog.FileName
                    ExportReportToExcel(pp.rp, exportPath)
                End If
            Else
                ' **عرض التقرير للطباعة**
                Dim p As New print
                p.CrystalReportViewer1.ReportSource = pp.rp
                p.ShowDialog()
            End If
        Catch ex As Exception
            MessageBox.Show("حدث خطأ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub PreparePrint()
        CurrentRow = 0
        PageNumber = 1
        TotalPages = 1
        PrintableRows.Clear()

        PD.DefaultPageSettings.Landscape = CurrentPrintLandscape
        PD.DefaultPageSettings.Margins = New Margins(25, 25, 30, 30)

        BuildPrintableRows()
        BuildPrintableColumns()
        TotalPages = EstimateTotalPages()
    End Sub


    Private Sub PreparePrintMenu()
        If Print_CntxtMStrip.Items.ContainsKey(PrintLandscapeMenuItemName) Then Return

        If print_cmb.Items.Count = 0 Then
            print_cmb.Items.AddRange({"طباعة أفقية", "طباعة عمودية"})
        End If

        If MY_Settings.ACC_B_printer_Type >= 0 AndAlso MY_Settings.ACC_B_printer_Type <= 1 Then
            print_cmb.SelectedIndex = MY_Settings.ACC_B_printer_Type
        Else
            print_cmb.SelectedIndex = 0
        End If
        CurrentPrintLandscape = (print_cmb.SelectedIndex = 0)

        Dim printLandscapeItem As New ToolStripMenuItem("طباعة بالعرض") With {
            .Name = PrintLandscapeMenuItemName
        }
        Dim printPortraitItem As New ToolStripMenuItem("طباعة بالطول") With {
            .Name = PrintPortraitMenuItemName
        }
        Dim orientationSeparator As New ToolStripSeparator With {
            .Name = PrintOrientationSeparatorName
        }

        AddHandler printLandscapeItem.Click,
            Sub()
                SetAccBPrintOrientation(True)
                PRINT_ACC_B()
            End Sub

        AddHandler printPortraitItem.Click,
            Sub()
                SetAccBPrintOrientation(False)
                PRINT_ACC_B()
            End Sub

        Print_CntxtMStrip.Items.Insert(0, printPortraitItem)
        Print_CntxtMStrip.Items.Insert(0, printLandscapeItem)
        Print_CntxtMStrip.Items.Insert(2, orientationSeparator)
    End Sub


    Private Sub SetAccBPrintOrientation(ByVal landscape As Boolean)
        CurrentPrintLandscape = landscape

        If print_cmb.Items.Count = 0 Then
            print_cmb.Items.AddRange({"طباعة أفقية", "طباعة عمودية"})
        End If

        print_cmb.SelectedIndex = If(landscape, 0, 1)
        MY_Settings.ACC_B_printer_Type = print_cmb.SelectedIndex
        MY_Settings.Save_AppSetting()
    End Sub


    Private Sub BuildPrintableRows()
        PrintableRows.Clear()

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).IsNewRow Then Continue For
            PrintableRows.Add(i)
        Next
    End Sub


    Private Sub BuildPrintableColumns()
        PrintableColumns.Clear()

        PrintableColumns.Add(New PrintColumnInfo With {
            .ColumnName = "",
            .HeaderText = "م",
            .SourceWidth = 45,
            .IsSerial = True
        })

        Dim visibleColumns = DataGridView1.Columns.Cast(Of DataGridViewColumn)().
            Where(Function(c) c.Visible).
            OrderBy(Function(c) c.DisplayIndex)

        For Each col As DataGridViewColumn In visibleColumns
            PrintableColumns.Add(New PrintColumnInfo With {
                .ColumnName = col.Name,
                .HeaderText = If(String.IsNullOrWhiteSpace(col.HeaderText), col.Name, col.HeaderText),
                .SourceWidth = Math.Max(col.Width, 60),
                .IsSerial = False
            })
        Next
    End Sub


    Private Sub PD_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PD.PrintPage
        Dim g = e.Graphics
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        Dim marginLeft As Integer = e.MarginBounds.Left
        Dim marginRight As Integer = e.MarginBounds.Right
        Dim y As Integer = e.MarginBounds.Top
        Dim pageWidth As Integer = e.MarginBounds.Width

        Dim companyFontAr As New Font("Tahoma", 12, FontStyle.Bold)
        Dim companyFontEn As New Font("Tahoma", 11, FontStyle.Bold)
        Dim titleFont As New Font("Tahoma", 14, FontStyle.Bold)
        Dim subTitleFont As New Font("Tahoma", 9.5!, FontStyle.Bold)
        Dim headerFont As New Font("Tahoma", If(CurrentPrintLandscape, 9.0!, 8.0!), FontStyle.Bold)
        Dim bodyFont As New Font("Tahoma", If(CurrentPrintLandscape, 8.5!, 7.75!), FontStyle.Regular)
        Dim totalFont As New Font("Tahoma", 9, FontStyle.Bold)

        Dim sfRight As New StringFormat With {
            .Alignment = StringAlignment.Far,
            .LineAlignment = StringAlignment.Center,
            .FormatFlags = StringFormatFlags.DirectionRightToLeft
        }

        Dim sfCenter As New StringFormat With {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Center,
            .FormatFlags = StringFormatFlags.DirectionRightToLeft
        }

        Dim sfLeft As New StringFormat With {
            .Alignment = StringAlignment.Near,
            .LineAlignment = StringAlignment.Center
        }

        g.DrawString(MY_Settings.SBill_Title_1, companyFontAr, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 24), sfRight)
        y += 26
        g.DrawString(MY_Settings.SBill_Title_2, companyFontEn, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 22), sfRight)
        y += 26
        g.DrawLine(Pens.Black, marginLeft, y, marginRight, y)
        y += 8

        g.DrawString("قيــــد يوميــــة", titleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 26), sfCenter)
        y += 28
        g.DrawString("صفحة " & PageNumber.ToString() & " من " & TotalPages.ToString(), bodyFont, Brushes.Black, New RectangleF(marginLeft, y - 4, pageWidth, 18), sfLeft)
        y += 18

        DrawEntryInfo(g, marginLeft, y, pageWidth, totalFont, sfCenter)
        y += 96

        If CurrentJournalIsBudgetJournal AndAlso Not CurrentJournalIsReverseJournal AndAlso Not String.IsNullOrWhiteSpace(CurrentBudgetInfoText) Then
            Dim budgetInfoHeight As Integer = DrawBudgetJournalPrintInfo(g, marginLeft, y, pageWidth, bodyFont)
            y += budgetInfoHeight + 8
        End If

        If CurrentJournalIsReverseJournal AndAlso Not String.IsNullOrWhiteSpace(CurrentReverseInfoText) Then
            Dim reverseInfoHeight As Integer = DrawReverseJournalPrintInfo(g, marginLeft, y, pageWidth, bodyFont)
            y += reverseInfoHeight + 8
        End If

        Dim notesHeight As Integer = DrawMasterNotes(g, marginLeft, y, pageWidth, bodyFont, sfRight)
        y += notesHeight + 8

        Dim colWidths = GetPrintColumnWidths(pageWidth)
        DrawPrintHeader(g, marginLeft, y, colWidths, headerFont, sfCenter)
        y += 30

        While CurrentRow < PrintableRows.Count
            Dim row As DataGridViewRow = DataGridView1.Rows(PrintableRows(CurrentRow))
            Dim rowHeight As Integer = EstimateEntryRowHeight(g, row, bodyFont, colWidths)

            If y + rowHeight > e.MarginBounds.Bottom - 130 Then
                e.HasMorePages = True
                PageNumber += 1
                Return
            End If

            DrawEntryRow(g, row, marginLeft, y, rowHeight, colWidths, bodyFont, sfCenter, sfRight)
            y += rowHeight
            CurrentRow += 1
        End While

        y += 8
        DrawTotals(g, marginLeft, y, pageWidth, totalFont, sfCenter)

        e.HasMorePages = False
        CurrentRow = 0
        PageNumber = 1
    End Sub


    Private Sub DrawEntryInfo(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, font As Font, sfCenter As StringFormat)
        Dim boxHeight As Integer = 28
        Dim boxWidth As Integer = CInt(pageWidth / 4)

        DrawSummaryBoxesRow(g, x, y, pageWidth, boxWidth, boxHeight,
                            {"رقم القيد", "الرقم الإشاري", "رقم الإيصال", "حالة القيد"},
                            {T_ID_txt_2.Text, NextNumber_TextBox.Text, ReceiptNum_Txt.Text, GetEntryStatusText()},
                            font, sfCenter)

        y += boxHeight + 4

        DrawSummaryBoxesRow(g, x, y, pageWidth, CInt(pageWidth / 3), boxHeight,
                            {"تاريخ القيد", "معد القيد", "مراجع القيد"},
                            {Date_.Value.ToString("dd/MM/yyyy"), Input_User_Txt.Text, Depended_User_Txt.Text},
                            font, sfCenter)
    End Sub


    Private Function DrawBudgetJournalPrintInfo(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, font As Font) As Integer
        Dim noteFormat As New StringFormat With {
            .Alignment = StringAlignment.Near,
            .LineAlignment = StringAlignment.Center,
            .FormatFlags = StringFormatFlags.DirectionRightToLeft
        }

        Dim text As String = "بيانات قيد الميزانية: " & CurrentBudgetInfoText
        Dim height As Integer = CInt(g.MeasureString(text, font, pageWidth - 12, noteFormat).Height) + 14
        If height < 34 Then height = 34

        Dim rect As New Rectangle(x, y, pageWidth, height)
        g.FillRectangle(New SolidBrush(Color.FromArgb(226, 239, 253)), rect)
        g.DrawRectangle(New Pen(Color.FromArgb(79, 129, 189)), rect)
        g.DrawString(text, font, New SolidBrush(Color.FromArgb(20, 74, 119)), New RectangleF(rect.X + 6, rect.Y + 2, rect.Width - 12, rect.Height - 4), noteFormat)

        Return height
    End Function


    Private Function DrawReverseJournalPrintInfo(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, font As Font) As Integer
        Dim noteFormat As New StringFormat With {
            .Alignment = StringAlignment.Near,
            .LineAlignment = StringAlignment.Center,
            .FormatFlags = StringFormatFlags.DirectionRightToLeft
        }

        Dim text As String = GetReverseJournalDisplayText()
        Dim height As Integer = CInt(g.MeasureString(text, font, pageWidth - 12, noteFormat).Height) + 14
        If height < 34 Then height = 34

        Dim rect As New Rectangle(x, y, pageWidth, height)
        g.FillRectangle(New SolidBrush(Color.FromArgb(238, 232, 246)), rect)
        g.DrawRectangle(New Pen(Color.FromArgb(91, 56, 137)), rect)
        g.DrawString(text, font, New SolidBrush(Color.FromArgb(91, 56, 137)), New RectangleF(rect.X + 6, rect.Y + 2, rect.Width - 12, rect.Height - 4), noteFormat)

        Return height
    End Function


    Private Function GetReverseJournalDisplayText() As String
        Return "تنبيه: هذا قيد عكسي - " & CurrentReverseInfoText
    End Function


    Private Function DrawMasterNotes(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, font As Font, sfRight As StringFormat) As Integer
        Dim notes As String = M_Notes_txt.Text
        Dim noteFormat As New StringFormat With {
            .Alignment = StringAlignment.Near,
            .LineAlignment = StringAlignment.Center,
            .FormatFlags = StringFormatFlags.DirectionRightToLeft
        }
        Dim height As Integer = CInt(g.MeasureString("شرح القيد: " & notes, font, pageWidth - 12, noteFormat).Height) + 12
        If height < 30 Then height = 30

        Dim rect As New Rectangle(x, y, pageWidth, height)
        g.FillRectangle(New SolidBrush(Color.FromArgb(248, 248, 248)), rect)
        g.DrawRectangle(Pens.Black, rect)
        g.DrawString("شرح القيد: " & notes, font, Brushes.Black, New RectangleF(rect.X + 6, rect.Y + 2, rect.Width - 12, rect.Height - 4), noteFormat)

        Return height
    End Function


    Private Function GetPrintColumnWidths(pageWidth As Integer) As Integer()
        If PrintableColumns.Count = 0 Then BuildPrintableColumns()

        Dim widths As New List(Of Integer)
        Dim totalSourceWidth As Integer = 0

        For Each col As PrintColumnInfo In PrintableColumns
            totalSourceWidth += col.SourceWidth
        Next

        If totalSourceWidth <= 0 Then totalSourceWidth = 1

        Dim usedWidth As Integer = 0
        For i As Integer = 0 To PrintableColumns.Count - 1
            Dim w As Integer

            If i = PrintableColumns.Count - 1 Then
                w = pageWidth - usedWidth
            Else
                w = CInt((PrintableColumns(i).SourceWidth / totalSourceWidth) * pageWidth)
                If PrintableColumns(i).IsSerial AndAlso w < 38 Then w = 38
                If Not PrintableColumns(i).IsSerial AndAlso w < 55 Then w = 55
            End If

            widths.Add(w)
            usedWidth += w
        Next

        Return widths.ToArray()
    End Function


    Private Sub DrawPrintHeader(g As Graphics, x As Integer, y As Integer, colWidths As Integer(), headerFont As Font, sfCenter As StringFormat)
        Dim currentX As Integer = x + TotalColumnWidth(colWidths)

        For i As Integer = 0 To PrintableColumns.Count - 1
            currentX -= colWidths(i)
            Dim rect As New Rectangle(currentX, y, colWidths(i), 30)
            g.FillRectangle(New SolidBrush(Color.FromArgb(225, 225, 225)), rect)
            g.DrawRectangle(New Pen(Color.FromArgb(80, 80, 80)), rect)
            g.DrawString(PrintableColumns(i).HeaderText, headerFont, Brushes.Black, New RectangleF(rect.X, rect.Y, rect.Width, rect.Height), sfCenter)
        Next
    End Sub


    Private Sub DrawEntryRow(g As Graphics, row As DataGridViewRow, x As Integer, y As Integer, rowHeight As Integer, colWidths As Integer(), bodyFont As Font, sfCenter As StringFormat, sfRight As StringFormat)
        Dim currentX As Integer = x + TotalColumnWidth(colWidths)

        For i As Integer = 0 To PrintableColumns.Count - 1
            currentX -= colWidths(i)
            Dim rect As New Rectangle(currentX, y, colWidths(i), rowHeight)
            If CurrentRow Mod 2 = 1 Then
                g.FillRectangle(New SolidBrush(Color.FromArgb(250, 250, 250)), rect)
            End If

            g.DrawRectangle(New Pen(Color.FromArgb(150, 150, 150)), rect)

            Dim value As String = GetPrintColumnValue(row, PrintableColumns(i))
            Dim useFormat As StringFormat = If(IsTextColumn(PrintableColumns(i)), sfRight, sfCenter)
            Dim brush As Brush = Brushes.Black

            If IsDebitColumn(PrintableColumns(i)) Then brush = Brushes.DarkGreen
            If IsCreditColumn(PrintableColumns(i)) Then brush = Brushes.DarkRed

            g.DrawString(value, bodyFont, brush, New RectangleF(rect.X + 4, rect.Y + 2, rect.Width - 8, rect.Height - 4), useFormat)
        Next
    End Sub


    Private Sub DrawTotals(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, totalFont As Font, sfCenter As StringFormat)
        Dim boxHeight As Integer = 30
        Dim boxWidth As Integer = CInt(pageWidth / 4)

        g.DrawLine(Pens.Black, x, y, x + pageWidth, y)
        y += 6

        DrawSummaryBoxesRow(g, x, y, pageWidth, boxWidth, boxHeight,
                            {"إجمالي المدين", "إجمالي الدائن", "الفرق", "عدد الصفوف"},
                            {Total_C_txt.Text, Total_D_txt.Text, Total_B_txt.Text, Rows_txt.Text},
                            totalFont, sfCenter)

        y += boxHeight + 4

        DrawSummaryBoxesRow(g, x, y, pageWidth, boxWidth, boxHeight,
                            {"عدد المدين", "عدد الدائن", "معد التقرير", "تاريخ الطباعة"},
                            {GetPositiveDebitRowsCount().ToString(), GetPositiveCreditRowsCount().ToString(), Input_User_Txt.Text, Date.Now.ToString("dd/MM/yyyy HH:mm")},
                            totalFont, sfCenter)

        If IsPrintMoneyLettersEnabled() Then
            y += boxHeight + 6
            Dim moneyRect As New Rectangle(x, y, pageWidth, 30)
            g.FillRectangle(New SolidBrush(Color.FromArgb(248, 248, 248)), moneyRect)
            g.DrawRectangle(Pens.Black, moneyRect)
            g.DrawString("القيمة بالحروف: " & HANY(Total_C_txt.Text, "LYD"), totalFont, Brushes.Black, New RectangleF(moneyRect.X + 5, moneyRect.Y, moneyRect.Width - 10, moneyRect.Height), sfCenter)
        End If
    End Sub


    Private Function IsPrintMoneyLettersEnabled() As Boolean
        If clb IsNot Nothing AndAlso clb.Items.Count > 0 Then
            Return clb.GetItemChecked(0)
        End If

        Return MY_Settings.is_Print_ACC_B_Letters
    End Function


    Private Sub DrawSummaryBoxesRow(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, boxWidth As Integer, boxHeight As Integer, titles() As String, values() As String, totalFont As Font, sfCenter As StringFormat)
        Dim currentX As Integer = x + pageWidth

        For i As Integer = 0 To titles.Length - 1
            currentX -= boxWidth
            Dim rect As New Rectangle(currentX, y, boxWidth, boxHeight)
            DrawSummaryBox(g, rect, titles(i), values(i), totalFont, sfCenter)
        Next
    End Sub


    Private Sub DrawSummaryBox(g As Graphics, rect As Rectangle, title As String, value As String, totalFont As Font, sfCenter As StringFormat)
        g.FillRectangle(New SolidBrush(Color.FromArgb(245, 245, 245)), rect)
        g.DrawRectangle(Pens.Black, rect)
        g.DrawString(title & ": " & value, totalFont, Brushes.Black, New RectangleF(rect.X + 5, rect.Y, rect.Width - 10, rect.Height), sfCenter)
    End Sub


    Private Function EstimateEntryRowHeight(g As Graphics, row As DataGridViewRow, bodyFont As Font, colWidths As Integer()) As Integer
        Dim h As Integer = 30

        For i As Integer = 0 To PrintableColumns.Count - 1
            Dim value As String = GetPrintColumnValue(row, PrintableColumns(i))
            Dim measuredHeight As Integer = CInt(g.MeasureString(value, bodyFont, Math.Max(colWidths(i) - 8, 20)).Height) + 12
            If measuredHeight > h Then h = measuredHeight
        Next

        If h < 30 Then h = 30
        Return h
    End Function


    Private Function EstimateTotalPages() As Integer
        Using bmp As New Bitmap(10, 10)
            Using g As Graphics = Graphics.FromImage(bmp)
                Dim bodyFont As New Font("Tahoma", 8.0!, FontStyle.Regular)
                Dim pageHeight As Integer
                Dim pageWidth As Integer

                If CurrentPrintLandscape Then
                    pageHeight = PD.DefaultPageSettings.Bounds.Width - PD.DefaultPageSettings.Margins.Top - PD.DefaultPageSettings.Margins.Bottom
                    pageWidth = PD.DefaultPageSettings.Bounds.Height - PD.DefaultPageSettings.Margins.Left - PD.DefaultPageSettings.Margins.Right
                Else
                    pageHeight = PD.DefaultPageSettings.Bounds.Height - PD.DefaultPageSettings.Margins.Top - PD.DefaultPageSettings.Margins.Bottom
                    pageWidth = PD.DefaultPageSettings.Bounds.Width - PD.DefaultPageSettings.Margins.Left - PD.DefaultPageSettings.Margins.Right
                End If

                Dim colWidths = GetPrintColumnWidths(pageWidth)
                Dim usableHeight As Integer = pageHeight - 300
                Dim y As Integer = 0
                Dim pages As Integer = 1

                For Each rowIndex In PrintableRows
                    Dim h As Integer = EstimateEntryRowHeight(g, DataGridView1.Rows(rowIndex), bodyFont, colWidths)

                    If y + h > usableHeight Then
                        pages += 1
                        y = 0
                    End If

                    y += h
                Next

                Return pages
            End Using
        End Using
    End Function


    Private Function TotalColumnWidth(colWidths As Integer()) As Integer
        Dim total As Integer = 0

        For Each w As Integer In colWidths
            total += w
        Next

        Return total
    End Function


    Private Function GetPrintColumnValue(row As DataGridViewRow, col As PrintColumnInfo) As String
        If col Is Nothing Then Return ""
        If col.IsSerial Then Return (CurrentRow + 1).ToString()

        Dim value As String = GetCellText(row, col.ColumnName)

        If IsAmountColumn(col) Then
            Dim d As Decimal
            If Decimal.TryParse(value, d) Then
                If d = 0D Then Return ""
                Return d.ToString("N3")
            End If
        End If

        Dim dateValue As Date
        If IsDateColumn(col) AndAlso Date.TryParse(value, dateValue) Then
            Return dateValue.ToString("dd/MM/yyyy")
        End If

        Return value
    End Function


    Private Function IsTextColumn(col As PrintColumnInfo) As Boolean
        If col Is Nothing OrElse col.IsSerial Then Return False

        Dim key As String = NormalizeColumnName(col.ColumnName & col.HeaderText)
        Return key.Contains("NAME") OrElse key.Contains("NOTES") OrElse key.Contains("شرح") OrElse
               key.Contains("ملاحظة") OrElse key.Contains("اسمالحساب") OrElse key.Contains("إسمالحساب") OrElse
               key.Contains("مركزالتكلفة") OrElse key.Contains("العملة")
    End Function


    Private Function IsAmountColumn(col As PrintColumnInfo) As Boolean
        If col Is Nothing Then Return False

        Dim key As String = NormalizeColumnName(col.ColumnName & col.HeaderText)
        Return key.Contains("CREDIT") OrElse key.Contains("DEBIT") OrElse key.Contains("مدين") OrElse key.Contains("دائن") OrElse key.Contains("سعرالصرف")
    End Function


    Private Function IsDebitColumn(col As PrintColumnInfo) As Boolean
        If col Is Nothing Then Return False

        Dim key As String = NormalizeColumnName(col.ColumnName & col.HeaderText)
        Return key.Contains("CREDIT") OrElse key.Contains("مدين")
    End Function


    Private Function IsCreditColumn(col As PrintColumnInfo) As Boolean
        If col Is Nothing Then Return False

        Dim key As String = NormalizeColumnName(col.ColumnName & col.HeaderText)
        Return key.Contains("DEBIT") OrElse key.Contains("دائن")
    End Function


    Private Function IsDateColumn(col As PrintColumnInfo) As Boolean
        If col Is Nothing Then Return False

        Dim key As String = NormalizeColumnName(col.ColumnName & col.HeaderText)
        Return key.Contains("DATE") OrElse key.Contains("تاريخ") OrElse key.Contains("الإدخال")
    End Function


    Private Function GetPositiveDebitRowsCount() As Integer
        Return GetPositiveRowsCount(True)
    End Function


    Private Function GetPositiveCreditRowsCount() As Integer
        Return GetPositiveRowsCount(False)
    End Function


    Private Function GetPositiveRowsCount(isDebit As Boolean) As Integer
        Dim count As Integer = 0

        For Each rowIndex As Integer In PrintableRows
            Dim row As DataGridViewRow = DataGridView1.Rows(rowIndex)
            Dim amountText As String

            If isDebit Then
                amountText = GetCellText(row, "CREDIT_CL", "CREDIT", "مدين")
            Else
                amountText = GetCellText(row, "DEBIT_CL", "DEBIT", "دائن")
            End If

            Dim amount As Decimal
            If Decimal.TryParse(amountText, amount) AndAlso amount > 0D Then
                count += 1
            End If
        Next

        Return count
    End Function


    Private Function GetCellText(row As DataGridViewRow, ParamArray columnNames() As String) As String
        For Each columnName As String In columnNames
            Dim columnIndex As Integer = FindColumnIndex(columnName)

            If columnIndex >= 0 AndAlso columnIndex < row.Cells.Count Then
                Dim value = row.Cells(columnIndex).Value
                If value IsNot Nothing AndAlso Not IsDBNull(value) Then Return value.ToString()
            End If
        Next

        Return ""
    End Function


    Private Function FindColumnIndex(columnName As String) As Integer
        Dim target As String = NormalizeColumnName(columnName)

        For Each col As DataGridViewColumn In DataGridView1.Columns
            Dim nameText As String = NormalizeColumnName(col.Name)
            Dim headerText As String = NormalizeColumnName(col.HeaderText)
            Dim propertyText As String = NormalizeColumnName(col.DataPropertyName)

            If String.Equals(nameText, target, StringComparison.OrdinalIgnoreCase) OrElse
               String.Equals(headerText, target, StringComparison.OrdinalIgnoreCase) OrElse
               String.Equals(propertyText, target, StringComparison.OrdinalIgnoreCase) Then
                Return col.Index
            End If
        Next

        Return -1
    End Function


    Private Function NormalizeColumnName(value As String) As String
        If value Is Nothing Then Return ""

        Return value.Replace("ـ", "").
                     Replace(" ", "").
                     Replace("_", "").
                     Replace("-", "").
                     Trim()
    End Function


    Private Function GetNumberCellText(row As DataGridViewRow, ParamArray columnNames() As String) As String
        Dim text As String = GetCellText(row, columnNames)
        Dim d As Decimal

        If Decimal.TryParse(text, d) Then
            If d = 0D Then Return ""
            Return d.ToString("N3")
        End If

        Return text
    End Function


    Private Function GetEntryStatusText() As String
        If DataGridView1.Rows.Count = 0 Then Return ""

        Dim depended As String = GetCellText(DataGridView1.Rows(0), "ACC_Depend_Status_CL", "ACC_Depend_Status")
        If Not String.IsNullOrWhiteSpace(depended) Then Return depended

        Dim isDepended As String = GetCellText(DataGridView1.Rows(0), "is_Depended_CL", "is_Depended")
        If isDepended = "1" OrElse isDepended.ToLower() = "true" Then Return b_Depend_str

        Return b_Depend_not_str
    End Function


    Private Sub Date__ValueChanged(sender As Object, e As EventArgs) Handles Date_.ValueChanged
        Currency_Equal_txt.Text = Get_Equal(Currency_Cm.SelectedValue, Date_, 0)
    End Sub



    Private Sub T_ID_txt_2_KeyDown(sender As Object, e As KeyEventArgs) Handles T_ID_txt_2.KeyDown
        If T_ID_txt_2.Text.Count > 0 Then If e.KeyCode = Keys.Return Then SELECT_Balance()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles UP_ToolStripBtn.Click
        tmp_ID = T_ID_txt_2.Text
        T_ID_txt_2.Text = T_ID_txt_2.Text + 1
        SELECT_Balance()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles DOWN_ToolStripBtn.Click
        tmp_ID = T_ID_txt_2.Text
        If tmp_ID = 0 Then
            Exit Sub
        End If
        T_ID_txt_2.Text = T_ID_txt_2.Text - 1
        SELECT_Balance()
    End Sub

    Private Sub T_ID_txt_2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles T_ID_txt_2.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub ACC_B_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F8 Then If SEARCH_ACC_BTN.Enabled = True Then MOVE_TO_ACCOUNTS_MENU()
    End Sub


    Private Function SELECT_First_Last(TYPE As String)


        Dim C = New C
        Try
            Dim S As String = ""

            If TYPE = "FIRST" Then
                S = "SELECT TOP 1 T_ID FROM [ACC_BALANCE_MASTER] "
            ElseIf TYPE = "LAST" Then
                S = "SELECT TOP 1 T_ID FROM [ACC_BALANCE_MASTER] ORDER BY T_ID DESC "
            End If


            C.Com = New SqlClient.SqlCommand(S, C.Con)
            C.Con.Open()
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                Return C.Dr("T_ID")
            Else
                Dim notification3 As New NotificationForm("تنويه", " لا يوجد بيانات للعرض ", "bottom")
                notification3.ShowNotification()
                '  MsgBox("لا يوجد بيانات للعرض", MsgBoxStyle.Information, "")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        Return 0
    End Function

    Private Sub LAST_ToolStripBtn_Click(sender As Object, e As EventArgs) Handles LAST_ToolStripBtn.Click
        tmp_ID = SELECT_First_Last("LAST")
        If tmp_ID <> 0 Then
            T_ID_txt_2.Text = tmp_ID
            SELECT_Balance()
        End If

    End Sub

    Private Sub First_ToolStripBtn_Click(sender As Object, e As EventArgs) Handles First_ToolStripBtn.Click
        tmp_ID = SELECT_First_Last("FIRST")
        If tmp_ID <> 0 Then
            T_ID_txt_2.Text = tmp_ID
            SELECT_Balance()
        End If
    End Sub

    Private Sub reverse_Btn_Click(sender As Object, e As EventArgs) Handles reverse_Btn.Click
        If BlockAccBPermission(AccBReversePermission, "إنشاء قيد عكسي") Then Exit Sub
        If BlockBudgetJournalAction("إنشاء قيد عكسي لقيد صرف الميزانية من شاشة القيود") Then Exit Sub

        If MessageBox.Show(" ... سيتم توليد قيد عكسي للقيد من أجل تسويته  ", "تاكيــد العملية", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.OK Then
            ACC_BALANCE_Insert_reverse(False)
        End If
    End Sub


    Private Sub ACC_BALANCE_Insert_reverse(Depended As Boolean)
        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "[ACC_BALANCE_Insert_reverse]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@B_T_ID", T_ID_txt_2.Text)
            .Parameters.AddWithValue("@USER_ID", USER_ID)
            .Parameters.AddWithValue("@OP_Status", 1)
            .Parameters.AddWithValue("@B_T_ID_NEW", 0)
            .Parameters.Add("@ERROR_MSG", SqlDbType.NVarChar, 500)
            .Parameters("@OP_Status").Direction = ParameterDirection.Output
            .Parameters("@ERROR_MSG").Direction = ParameterDirection.Output

            SQL_SP_EXEC(C.Com)

            If C.Com.Parameters("@OP_Status").Value.ToString() = "0" Then
                Dim notification3 As New NotificationForm("خطأ فالتحرير", C.Com.Parameters("@ERROR_MSG").Value.ToString(), "bottom", True)
                notification3.ShowNotification()
            Else
                If Depended = True Then
                    Dim notification3 As New NotificationForm("إشعار", " تم إجراء عملية عكسية للقيــد " & T_ID_txt_2.Text, "bottom")
                    notification3.ShowNotification()
                Else
                    Dim notification3 As New NotificationForm("إشعار", " تم عملية عكسية للقيــد " & T_ID_txt_2.Text, "bottom")
                    notification3.ShowNotification()
                    Enable_Fields(True)
                End If
                Clear_Fields()
                SELECT_Balance()
            End If


        End With
    End Sub

    Private Sub Depend_Btn_EnabledChanged(sender As Object, e As EventArgs) Handles Depend_Btn.EnabledChanged
        If CurrentJournalIsReverseJournal OrElse CurrentJournalIsBudgetJournal Then
            reverse_Btn.Enabled = False
        Else
            reverse_Btn.Enabled = Not Depend_Btn.Enabled
        End If

        ApplyAccBPermissions()
    End Sub


    Private Sub PRINT_ACC_B()
        If BlockAccBPermission(AccBPrintPermission, "طباعة قيد") Then Exit Sub

        If DataGridView1.Rows.Count > 0 Then

            If Total_B_txt.Text <> 0 Then
                Dim notification3 As New NotificationForm("خطــأ فالإعتمــاد", " القيــد غير مــوزون ", "bottom", True)
                notification3.ShowNotification()
                '  MsgBox("القيــد غير مــوزون", MsgBoxStyle.Critical, "خطــأ فالطباعة")
                Exit Sub
            End If

            If DataGridView1.Rows.Count > 0 Then
                If DT(0)("is_Depended") = 0 Then

                    If MessageBox.Show(" القيد غير معتمد ... هل تريد الإستمرار فالطباعة ", "تاكيــد العملية", MessageBoxButtons.OKCancel,
                                       MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.Cancel Then
                        Exit Sub
                    End If

                    'MsgBox("القيــد غير معتمد", MsgBoxStyle.Critical, "خطــأ فالطباعة")
                    'Exit Sub
                End If
            End If

            Print_B()

        End If
    End Sub

    Private Sub إستخراجالتقريرExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إستخراجالتقريرExcelToolStripMenuItem.Click
        If BlockAccBPermission(AccBPrintPermission, "تصدير/طباعة قيد") Then Exit Sub
        Print_B(True)
    End Sub

    Private Sub Print_Btn_ButtonClick(sender As Object, e As EventArgs) Handles Print_Btn.ButtonClick
        PRINT_ACC_B()
    End Sub

    Private Sub LoadAccBPermissions()
        CurrentAccBPermissions = TreeMainPermissions.LoadAllowedPermissions(USER_ID, User_isAdmin)
    End Sub

    Private Function HasAccBPermission(permissionKey As String) As Boolean
        Return User_isAdmin OrElse CurrentAccBPermissions.Contains(permissionKey)
    End Function

    Private Function BlockAccBPermission(permissionKey As String, actionName As String) As Boolean
        If HasAccBPermission(permissionKey) Then Return False

        MessageBox.Show("ليس لديك صلاحية: " & actionName, "صلاحيات المستخدم", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
        Return True
    End Function

    Private Sub ApplyAccBPermissions()
        If IsApplyingAccBPermissions Then Exit Sub

        IsApplyingAccBPermissions = True
        Try
            Dim canApprove As Boolean = HasAccBPermission(AccBApprovePermission)
            Dim canPrint As Boolean = HasAccBPermission(AccBPrintPermission)
            Dim canEditApproval As Boolean = HasAccBPermission(AccBEditApprovalPermission)
            Dim canReverse As Boolean = HasAccBPermission(AccBReversePermission)

            Depend_Btn.Visible = canApprove
            If Not canApprove Then Depend_Btn.Enabled = False

            Print_Btn.Visible = canPrint
            إستخراجالتقريرExcelToolStripMenuItem.Visible = canPrint
            If Not canPrint Then Print_Btn.Enabled = False

            EDIT_Btn.Visible = canEditApproval
            If Not canEditApproval Then EDIT_Btn.Enabled = False

            reverse_Btn.Visible = canReverse
            If Not canReverse Then reverse_Btn.Enabled = False
        Finally
            IsApplyingAccBPermissions = False
        End Try
    End Sub



    '=========================================================
    ' Budget Journal helpers
    '=========================================================
    Private Sub InitializeBudgetJournalUi()
        If BudgetJournalToolLabel Is Nothing Then
            BudgetJournalToolLabel = New ToolStripLabel()
            BudgetJournalToolLabel.Name = "BudgetJournalToolLabel"
            BudgetJournalToolLabel.Font = New Font("Arial", 10.25!, FontStyle.Bold)
            BudgetJournalToolLabel.Text = "نوع القيد: قيد عادي"
            BudgetJournalToolLabel.Visible = True

            ToolStrip1.Items.Add(New ToolStripSeparator())
            ToolStrip1.Items.Add(BudgetJournalToolLabel)
        End If

        ResetBudgetJournalState()
    End Sub


    Private Sub ResetBudgetJournalState()
        CurrentJournalIsBudgetJournal = False
        CurrentJournalIsReverseJournal = False
        CurrentBudgetEntryId = 0
        CurrentBudgetInfoText = ""
        CurrentReverseInfoText = ""
        CurrentJournalTypeText = "قيد عادي"

        If BudgetJournalToolLabel IsNot Nothing Then
            BudgetJournalToolLabel.Text = "نوع القيد: قيد عادي"
            BudgetJournalToolLabel.ForeColor = Color.DarkGreen
        End If

        If Entry_Label IsNot Nothing Then
            Entry_Label.Text = "(إدخــال جديــد)"
            Entry_Label.BackColor = Color.LightYellow
            Entry_Label.ForeColor = Color.Black
        End If

        If BudgetJournalInfo_Label IsNot Nothing Then
            BudgetJournalInfo_Label.Text = ""
            BudgetJournalInfo_Label.Visible = False
        End If
    End Sub


    Private Sub LoadBudgetJournalInfo(ByVal journalId As Integer)
        CurrentJournalIsBudgetJournal = False
        CurrentJournalIsReverseJournal = False
        CurrentBudgetEntryId = 0
        CurrentBudgetInfoText = ""
        CurrentReverseInfoText = ""
        CurrentJournalTypeText = "قيد عادي"

        Dim C As New C

        Try
            Dim sql As String =
                "SELECT TOP 1 " &
                "IsBudgetJournal, " &
                "ISNULL(BudgetEntryId, 0) AS BudgetEntryId, " &
                "ISNULL(DoorName, N'') AS DoorName, " &
                "ISNULL(ChapterName, N'') AS ChapterName, " &
                "ISNULL(BudgetItemName, N'') AS BudgetItemName, " &
                "ISNULL(BudgetAmount, 0) AS BudgetAmount, " &
                "SourceType, SourceId, SourceTable " &
                "FROM dbo.V_ACC_BALANCE_MASTER_WITH_BUDGET " &
                "WHERE T_ID = @T_ID;"

            Using cmd As New SqlCommand(sql, C.Con)

                cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = journalId

                If C.Con.State <> ConnectionState.Open Then C.Con.Open()

                Using dr As SqlDataReader = cmd.ExecuteReader()
                    If dr.Read() Then
                        CurrentJournalIsBudgetJournal = Convert.ToBoolean(dr("IsBudgetJournal"))

                        If CurrentJournalIsBudgetJournal Then
                            CurrentBudgetEntryId = Convert.ToInt32(dr("BudgetEntryId"))
                            CurrentJournalTypeText = "قيد صرف ميزانية"

                            Dim amountText As String = Convert.ToDecimal(dr("BudgetAmount")).ToString("N3")
                            CurrentBudgetInfoText =
                                "قيد صرف ميزانية | حركة رقم: " & CurrentBudgetEntryId.ToString() &
                                " | الباب: " & dr("DoorName").ToString() &
                                " | الفصل: " & dr("ChapterName").ToString() &
                                " | البند: " & dr("BudgetItemName").ToString() &
                                " | المبلغ: " & amountText
                        ElseIf Not IsDBNull(dr("SourceType")) Then
                            CurrentJournalTypeText = "قيد آلي"
                            CurrentBudgetInfoText = "قيد آلي | المصدر: " & dr("SourceTable").ToString() &
                                                    " | رقم المصدر: " & dr("SourceId").ToString()
                        End If
                    End If
                End Using
            End Using

        Catch ex As Exception
            CurrentJournalIsBudgetJournal = False
            CurrentJournalIsReverseJournal = False
            CurrentBudgetInfoText = ""
            CurrentReverseInfoText = ""
            CurrentJournalTypeText = "قيد عادي"

            Dim notification3 As New NotificationForm("تنبيه", "تعذر قراءة بيانات نوع القيد: " & ex.Message, "bottom", True)
            notification3.ShowNotification()
        Finally
            If C.Con.State = ConnectionState.Open Then C.Con.Close()
        End Try

        ApplyReverseJournalState()
        ApplyBudgetJournalUiState()
    End Sub


    Private Sub ApplyReverseJournalState()
        CurrentJournalIsReverseJournal =
            IsReverseJournalText(M_Notes_txt.Text) OrElse
            IsReverseJournalText(CurrentBudgetInfoText) OrElse
            IsReverseJournalText(CurrentJournalTypeText)

        If CurrentJournalIsReverseJournal Then
            CurrentReverseInfoText = "قيد عكسي | " & If(String.IsNullOrWhiteSpace(M_Notes_txt.Text), "تم إنشاء هذا القيد لعكس أو إلغاء قيد سابق", M_Notes_txt.Text)

            If CurrentJournalIsBudgetJournal Then
                CurrentJournalTypeText = "قيد صرف ميزانية عكسي"
            Else
                CurrentJournalTypeText = "قيد عكسي"
            End If
        End If
    End Sub


    Private Function IsReverseJournalText(value As String) As Boolean
        If String.IsNullOrWhiteSpace(value) Then Return False

        Dim text As String = value.Trim().Replace("أ", "ا").Replace("إ", "ا").Replace("آ", "ا")
        Return text.Contains("عكس") OrElse
               text.Contains("عكسي") OrElse
               text.Contains("الغاء")
    End Function


    Private Sub ApplyBudgetJournalUiState()
        If BudgetJournalToolLabel IsNot Nothing Then
            BudgetJournalToolLabel.Text = "نوع القيد: " & CurrentJournalTypeText
            If CurrentJournalIsReverseJournal Then
                BudgetJournalToolLabel.ForeColor = Color.FromArgb(91, 56, 137)
            Else
                BudgetJournalToolLabel.ForeColor = If(CurrentJournalIsBudgetJournal, Color.DarkOrange, Color.DarkGreen)
            End If
        End If

        If CurrentJournalIsBudgetJournal Then
            Entry_Label.Text = If(CurrentJournalIsReverseJournal, "(قيد ميزانية عكسي)", "(قيد ميزانية)")
            Entry_Label.BackColor = Color.LightYellow
            Entry_Label.ForeColor = Color.Black

            If CurrentJournalIsReverseJournal Then
                BudgetJournalInfo_Label.Text = GetReverseJournalDisplayText()
                BudgetJournalInfo_Label.BackColor = Color.FromArgb(238, 232, 246)
                BudgetJournalInfo_Label.ForeColor = Color.FromArgb(91, 56, 137)
                BudgetJournalInfo_Label.Visible = True
                BudgetJournalInfo_Label.BringToFront()
            Else
                BudgetJournalInfo_Label.Text = CurrentBudgetInfoText
                BudgetJournalInfo_Label.BackColor = Color.FromArgb(226, 239, 253)
                BudgetJournalInfo_Label.ForeColor = Color.FromArgb(20, 74, 119)
                BudgetJournalInfo_Label.Visible = True
                BudgetJournalInfo_Label.BringToFront()
            End If

            Fields_Panel.Enabled = False
            M_Notes_txt.Enabled = False
            Date_.Enabled = False

            ADD_Btn.Enabled = False
            REMOVE_BTN.Enabled = False
            Depend_Btn.Enabled = False
            EDIT_Btn.Enabled = False
            Edit_title_date_Btn.Enabled = False
            reverse_Btn.Enabled = False
        Else
            If BudgetJournalInfo_Label IsNot Nothing Then
                BudgetJournalInfo_Label.Text = ""
                BudgetJournalInfo_Label.Visible = False
            End If

            If CurrentJournalIsReverseJournal Then
                Entry_Label.Text = "(قيد عكسي)"
                Entry_Label.BackColor = Color.FromArgb(238, 232, 246)
                Entry_Label.ForeColor = Color.FromArgb(91, 56, 137)

                If BudgetJournalInfo_Label IsNot Nothing Then
                    BudgetJournalInfo_Label.Text = GetReverseJournalDisplayText()
                    BudgetJournalInfo_Label.BackColor = Color.FromArgb(238, 232, 246)
                    BudgetJournalInfo_Label.ForeColor = Color.FromArgb(91, 56, 137)
                    BudgetJournalInfo_Label.Visible = True
                    BudgetJournalInfo_Label.BringToFront()
                End If

                Fields_Panel.Enabled = False
                M_Notes_txt.Enabled = False
                Date_.Enabled = False

                ADD_Btn.Enabled = False
                REMOVE_BTN.Enabled = False
                Depend_Btn.Enabled = False
                EDIT_Btn.Enabled = False
                Edit_title_date_Btn.Enabled = False
                reverse_Btn.Enabled = False
            End If

            If Not CurrentJournalIsReverseJournal Then
                Date_.Enabled = True
            End If
        End If
    End Sub


    Private Function CurrentUserCanEditBudgetJournal() As Boolean
        'عدّل هذه الدالة لاحقًا عند إضافة صلاحية خاصة مثل:
        'Return USER_HAS_PERMISSION("EDIT_BUDGET_JOURNAL")
        Return False
    End Function


    Private Function BlockBudgetJournalAction(ByVal actionName As String) As Boolean
        If CurrentJournalIsReverseJournal Then
            Dim reverseMsg As String =
                "لا يمكن " & actionName & " لأن هذا القيد عكسي." & vbCrLf &
                "القيد العكسي للعرض فقط ولا يسمح بتعديل بياناته أو تحريره أو إضافة/حذف بنود منه."

            Dim reverseNotification As New NotificationForm("قيد عكسي مقفل", reverseMsg, "bottom", True)
            reverseNotification.ShowNotification()

            Return True
        End If

        If Not CurrentJournalIsBudgetJournal Then Return False
        If CurrentUserCanEditBudgetJournal() Then Return False

        Dim msg As String =
            "لا يمكن " & actionName & " من شاشة القيود العادية." & vbCrLf &
            "هذا القيد مرتبط بصرف ميزانية رقم: " & CurrentBudgetEntryId.ToString() & vbCrLf &
            "يجب الرجوع إلى شاشة الميزانية أو إنشاء إجراء إلغاء/عكس مخصص للميزانية."

        Dim notification3 As New NotificationForm("قيد ميزانية مقفل", msg, "bottom", True)
        notification3.ShowNotification()

        Return True
    End Function


    Public Class ClosingEntryResult
        Public Property Success As Boolean
        Public Property EntryId As Integer
        Public Property Message As String
        Public Property JournalNo As String
    End Class


    Public Function GenerateClosingEntry(
        ByVal ParentAccCode As String,
        ByVal TargetAccCode As String,
        ByVal ClosingDate As DateTime,
        ByVal UserId As Integer,
        ByVal ClosingType As String,
        ByVal Notes As String,
        Optional ByVal CurrencyId As Integer = 1,
        Optional ByVal CurrencyEqual As Decimal = 1D,
        Optional ByVal CostId As Integer = 1
    ) As ClosingEntryResult

        Dim result As New ClosingEntryResult()
        Dim C As New C

        Try

            Using cmd As New SqlCommand("ACC_GenerateClosingEntry", C.Con)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@ParentAccCode", ParentAccCode)
                cmd.Parameters.AddWithValue("@TargetAccCode", TargetAccCode)
                cmd.Parameters.AddWithValue("@ClosingDate", ClosingDate)
                cmd.Parameters.AddWithValue("@UserId", UserId)
                cmd.Parameters.AddWithValue("@ClosingType", ClosingType)
                cmd.Parameters.AddWithValue("@NotesMaster", Notes)
                cmd.Parameters.AddWithValue("@CurrencyId", CurrencyId)
                cmd.Parameters.AddWithValue("@CurrencyEqual", CurrencyEqual)
                cmd.Parameters.AddWithValue("@CostId", CostId)
                cmd.Parameters.AddWithValue("@YEAR", F_YEAR)


                cmd.Parameters.AddWithValue("@B_T_ID", T_ID_txt_2.Text)

                Dim p_Status As New SqlParameter("@OP_Status", SqlDbType.Int)
                p_Status.Direction = ParameterDirection.Output
                cmd.Parameters.Add(p_Status)

                Dim p_Error As New SqlParameter("@ERROR_MSG", SqlDbType.NVarChar, 500)
                p_Error.Direction = ParameterDirection.Output
                cmd.Parameters.Add(p_Error)

                Dim p_Journal As New SqlParameter("@NextNumber", SqlDbType.VarChar, 50)
                p_Journal.Direction = ParameterDirection.Output
                cmd.Parameters.Add(p_Journal)

                If C.Con.State <> ConnectionState.Open Then
                    C.Con.Open()
                End If

                cmd.ExecuteNonQuery()

                Dim status As Integer = 0
                Dim entryId As Integer = 0
                Dim errorMsg As String = ""
                Dim journalNo As String = ""

                If Not IsDBNull(p_Status.Value) Then
                    status = Convert.ToInt32(p_Status.Value)
                End If


                If p_Error.Value IsNot Nothing AndAlso Not IsDBNull(p_Error.Value) Then
                    errorMsg = p_Error.Value.ToString()
                End If

                If p_Journal.Value IsNot Nothing AndAlso Not IsDBNull(p_Journal.Value) Then
                    journalNo = p_Journal.Value.ToString()
                End If

                SELECT_Balance()

                result.Success = (status = 1)
                result.EntryId = entryId
                result.JournalNo = journalNo

                If status = 1 Then
                    result.Message = "تم إنشاء القيد بنجاح"
                Else
                    result.Message = errorMsg
                End If
            End Using

        Catch ex As Exception
            result.Success = False
            result.EntryId = 0
            result.Message = ex.Message
            result.JournalNo = ""
        Finally
            If C.Con.State = ConnectionState.Open Then
                C.Con.Close()
            End If
        End Try

        Return result
    End Function


    Private Sub قيدإقفالإيراداتToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles قيدإقفالإيراداتToolStripMenuItem.Click

        Dim inp = InputBox("أدخل رقم الحساب اللرئيسي الخاص بالإيرادات", "فتح سنة")
        If inp <> "" Then


            Dim result As ClosingEntryResult = GenerateClosingEntry(
    inp,
    Pure_Income_ACC_CODE,
    New DateTime(F_YEAR, 12, 31),
    USER_ID,
    "REVENUE",
   " قيد إقفال الإيرادات لسنة  " & F_YEAR.ToString
)

            If result.Success Then
                MessageBox.Show("تم الإقفال بنجاح" & vbCrLf &
                                "رقم القيد: " & result.EntryId & vbCrLf &
                                "رقم اليومية: " & result.JournalNo)
            Else
                MessageBox.Show("خطأ: " & result.Message)
            End If


        End If

    End Sub

    Private Sub قيدإقفالمصروفاتToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles قيدإقفالمصروفاتToolStripMenuItem.Click

        Dim inp = InputBox("أدخل رقم الحساب اللرئيسي الخاص بالمصروفات", "فتح سنة")
        If inp <> "" Then


            Dim result As ClosingEntryResult = GenerateClosingEntry(
inp,
Pure_Income_ACC_CODE,
New DateTime(F_YEAR, 12, 31),
USER_ID,
"EXPENSE",
" قيد إقفال المصروفات لسنة  " & F_YEAR.ToString
)
            If result.Success Then
                MessageBox.Show("تم الإقفال بنجاح" & vbCrLf &
                                "رقم القيد: " & result.EntryId & vbCrLf &
                                "رقم اليومية: " & result.JournalNo)
            Else
                MessageBox.Show("خطأ: " & result.Message)
            End If

        End If


    End Sub
End Class













'Imports System.Data.SqlClient
'Imports System.Drawing.Printing


'Public Class ACC_B
'    Dim DT As New DataTable
'    Dim ACC_CODE_DT As New DataTable
'    Public is_Select As Boolean = False
'    Dim b_balanced_str = "القيـــد مـــوزون"
'    Dim b_balanced_not_str = "القيـــد غير موزون"

'    Dim b_Depend_str = "القيـــد معتمد"
'    Dim b_Depend_not_str = "القيـــد غير معتمد"

'    Public Selected_ACC_CODE As String = ""

'    Dim clb As New CheckedListBox()
'    Dim print_cmb As New ComboBox()

'    Private WithEvents PD As New PrintDocument
'    Private PPD As New PrintPreviewDialog
'    Private CurrentRow As Integer = 0
'    Private PageNumber As Integer = 1
'    Private TotalPages As Integer = 1
'    Private PrintableRows As New List(Of Integer)
'    Private PrintableColumns As New List(Of PrintColumnInfo)
'    Private CurrentPrintLandscape As Boolean = True

'    Private Class PrintColumnInfo
'        Public Property ColumnName As String
'        Public Property HeaderText As String
'        Public Property SourceWidth As Integer
'        Public Property IsSerial As Boolean
'    End Class

'    Private Sub ADD_Btn_Click(sender As Object, e As EventArgs) Handles ADD_Btn.Click

'        SELECT_ACC_NATURAL()
'        If B_Name_Cm.Tag = 1 Then Exit Sub


'        If ValidateChildren() = True Then
'            If String.IsNullOrWhiteSpace(DEBIT_txt.Text) And String.IsNullOrWhiteSpace(CREDIT_txt.Text) Then
'                'MsgBox("حدد قيمة القيد", MsgBoxStyle.Critical, "")
'                Dim notification3 As New NotificationForm("خطأ", " حدد قيمة  الحساب ", "bottom", True)
'                notification3.ShowNotification()
'                Exit Sub
'            End If
'            Prepare_to_add()
'            ACC_BALANCE_proc("")
'            B_NUM_txt.Select()
'        End If




'    End Sub

'    Private Sub B_Name_Cm_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles B_Name_Cm.Validating
'        If B_Name_Cm.SelectedIndex = -1 Then
'            ACC_CODE_ErrorProvider.SetError(B_Name_Cm, " أدخل حساب للقيــد ")
'            B_Name_Cm.Select()
'            e.Cancel = True
'        Else
'            e.Cancel = False
'            ACC_CODE_ErrorProvider.Clear()
'        End If

'    End Sub

'    Private Sub B_NUM_txt_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles B_NUM_txt.Validating

'        If String.IsNullOrWhiteSpace(B_NUM_txt.Text) = True Then
'            ACC_CODE_NUM_ErrorProvider.SetError(B_NUM_txt, " أدخل رقم الحساب ")
'            B_NUM_txt.Select()
'            e.Cancel = True
'        ElseIf B_NUM_txt.Text <> B_Name_Cm.SelectedValue Then

'            ACC_CODE_NUM_ErrorProvider.SetError(B_NUM_txt, " تحقق من صحة إدخال رقم الحساب ")
'            B_NUM_txt.Select()
'        Else
'            e.Cancel = False
'            ACC_CODE_NUM_ErrorProvider.Clear()
'        End If

'    End Sub

'    Private Sub Prepare_to_add()
'        If Not String.IsNullOrWhiteSpace(Currency_Equal_txt.Text) Then

'            If Convert.ToDouble(Currency_Equal_txt.Text) = 0 Then Currency_Equal_txt.Text = "1"

'        Else
'            Currency_Equal_txt.Text = "1"
'        End If
'    End Sub

'    Private Sub ACC_BALANCE_proc(Process As String)
'        Dim C As New C


'        With C.Com
'            .Connection = C.Con
'            .CommandText = "[ACC_BALANCE_proc]"
'            .CommandType = CommandType.StoredProcedure
'            .Parameters.AddWithValue("@T_ID", T_ID_Details_txt.Text)
'            .Parameters.AddWithValue("@B_T_ID", T_ID_txt_2.Text)
'            .Parameters.AddWithValue("@DATE", Date_.Value)
'            .Parameters.AddWithValue("@ACC_CODE", B_NUM_txt.Text)
'            If Not String.IsNullOrWhiteSpace(DEBIT_txt.Text) Then .Parameters.AddWithValue("@DEBIT", Convert.ToDouble(DEBIT_txt.Text) * Convert.ToDouble(Currency_Equal_txt.Text))
'            If Not String.IsNullOrWhiteSpace(CREDIT_txt.Text) Then .Parameters.AddWithValue("@CREDIT", Convert.ToDouble(CREDIT_txt.Text) * Convert.ToDouble(Currency_Equal_txt.Text))
'            .Parameters.AddWithValue("@USER_ID", USER_ID)
'            .Parameters.AddWithValue("@IS_VOID", 0)
'            .Parameters.AddWithValue("@Currency", 1)
'            .Parameters.AddWithValue("@Notes", Notes_txt.Text)
'            .Parameters.AddWithValue("@Notes_MASTER", M_Notes_txt.Text)
'            .Parameters.AddWithValue("@Process", Process)
'            .Parameters.AddWithValue("@Bill_Num", Bill_Num_txt.Text)
'            .Parameters.AddWithValue("@COST_ID", COST_CM.SelectedValue)
'            .Parameters.AddWithValue("@Cr_ID", Currency_Cm.SelectedValue)
'            .Parameters.AddWithValue("@Currency_Equal", Currency_Equal_txt.Text)

'            .Parameters.AddWithValue("@NextNumber", "")

'            .Parameters.AddWithValue("@OP_Status", 1)
'            .Parameters.Add("@ERROR_MSG", SqlDbType.NVarChar, 500)
'            .Parameters("@OP_Status").Direction = ParameterDirection.Output
'            .Parameters("@ERROR_MSG").Direction = ParameterDirection.Output
'            .Parameters("@NextNumber").Direction = ParameterDirection.Output

'            C.Con.Open()
'            T_ID_txt_2.Text = C.Com.ExecuteScalar()
'            C.Con.Close()


'            If C.Com.Parameters("@OP_Status").Value.ToString() = "0" Then
'                ' MsgBox(C.Com.Parameters("@ERROR_MSG").Value.ToString(), MsgBoxStyle.Critical, "خطــأ")
'                Dim notification3 As New NotificationForm("خطأ", C.Com.Parameters("@ERROR_MSG").Value.ToString(), "bottom", True)
'                notification3.ShowNotification()
'            Else
'                Clear_Fields()
'                SELECT_Balance()
'            End If

'            'If Process = "DEPEND" Then MsgBox("تم إعتمــاد القيــد", MsgBoxStyle.Information, "") End

'        End With
'    End Sub

'    Private Sub ACC_B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

'        If SELECT_ARCHIVE_COUNTER(Identifiers.F_YEAR) > 0 Then
'            Dim notification3 As New NotificationForm("خطأ", "يوجد أرشيف لهذه السنة ... لا يمكن إجراء قيود إلا بعد استرجاعها", "bottom", True)
'            notification3.ShowNotification()
'            Me.BeginInvoke(New MethodInvoker(AddressOf Me.Close))
'        End If

'        FYear_Txt_Tool.Text = Identifiers.F_YEAR
'        Load_Balances()
'        If is_Select = True Then
'            T_ID_txt_2.Text = T_ID_Search
'            SELECT_Balance()

'            If Not String.IsNullOrWhiteSpace(Selected_ACC_CODE) Then

'                For Each row As DataGridViewRow In DataGridView1.Rows
'                    If row.Cells("ACC_CODE_CL").Value IsNot Nothing AndAlso row.Cells("ACC_CODE_CL").Value.ToString() = Selected_ACC_CODE Then
'                        row.Selected = True
'                        Exit For
'                    End If
'                Next

'            End If

'        Else
'            T_ID_txt_2.Text = Load_Balances_MAX_ID()
'        End If

'        COST_CM.Select()


'        '------------------------------------------------------

'        clb.Items.Add("عرض القيمة بالحروف فالطباعة", MY_Settings.is_Print_ACC_B_Letters)
'        clb.Height = 60

'        ' ✅ إنشاء ComboBox
'        'Dim cmb As New ComboBox()
'        print_cmb.FlatStyle = FlatStyle.Flat
'        print_cmb.Items.AddRange({"طباعة أفقية", " طباعة عمودية"})
'        print_cmb.DropDownStyle = ComboBoxStyle.DropDownList
'        print_cmb.SelectedIndex = 0
'        print_cmb.Width = 120

'        ' ✅ استضافة الأدوات داخل ToolStripControlHost
'        Dim hostCombo As New ToolStripControlHost(print_cmb)
'        Dim hostClb As New ToolStripControlHost(clb)

'        ' ✅ إنشاء القائمة المنسدلة ToolStripDropDown
'        Dim dropDown As New ToolStripDropDown()
'        dropDown.Items.Add(hostCombo)
'        dropDown.Items.Add(New ToolStripSeparator()) ' خط فاصل اختياري
'        dropDown.Items.Add(hostClb)

'        ' ✅ زر في الـ ToolStrip لفتح القائمة
'        Dim btn As New ToolStripDropDownButton("خيارات")
'        btn.DropDown = dropDown
'        ToolStrip1.Items.Add(btn)
'        print_cmb.SelectedIndex = MY_Settings.ACC_B_printer_Type
'        CurrentPrintLandscape = (print_cmb.SelectedIndex = 0)
'        PreparePrintMenu()



'        ' ✅ مثال: التقاط حدث تغيير اختيار ComboBox
'        AddHandler print_cmb.SelectedIndexChanged,
'            Sub()
'                MY_Settings.ACC_B_printer_Type = print_cmb.SelectedIndex
'                CurrentPrintLandscape = (print_cmb.SelectedIndex = 0)
'                MY_Settings.Save_AppSetting()
'                'MessageBox.Show("تم اختيار: " & print_cmb.SelectedItem.ToString())
'            End Sub


'        ' ✅ مثال: التقاط تغيير CheckBox
'        AddHandler clb.ItemCheck,
'            Sub(s, eArgs)
'                Dim itemText = clb.Items(eArgs.Index).ToString()
'                'If eArgs.NewValue = CheckState.Checked Then
'                '    MessageBox.Show("تم تحديد: " & itemText)
'                'Else
'                '    MessageBox.Show("تم إلغاء التحديد: " & itemText)

'                'MsgBox(clb.GetItemChecked(0).ToString)
'                MY_Settings.is_Print_ACC_B_Letters = Not clb.GetItemChecked(0)
'                MY_Settings.Save_AppSetting()
'                'End If
'            End Sub

'    End Sub

'    Private Function Load_Balances_MAX_ID()
'        Dim C = New C
'        Try
'            Dim S As String = "SELECT ISNULL(MAX(T_ID),0) + 1 AS MX FROM ACC_BALANCE_MASTER "
'            C.Com = New SqlClient.SqlCommand(S, C.Con)
'            C.Con.Open()
'            C.Dr = C.Com.ExecuteReader
'            If C.Dr.HasRows Then
'                C.Dr.Read()
'                Return C.Dr("MX")
'            End If
'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try

'        Return 0
'    End Function


'    Public Sub SELECT_Balance()
'        If Not String.IsNullOrWhiteSpace(T_ID_txt_2.Text) Then


'            Enable_Fields(True)
'            Clear_Fields()

'            DT = New DataTable
'            Dim C As New C

'            Dim da As New SqlClient.SqlDataAdapter("SELECT [T_ID],[DATE_IN] ,CONVERT(DATE,DATE) as DATE ,[COST_NAME],[Cr_Name],[Currency_Equal],[MASTER_NOTES],[ACC_CODE] ,[ACC_NAME], " &
'                                                " [Bill_Num],[CREDIT],[DEBIT],[Notes],ACC_Depend_Status,is_Depended,COST_ID,Currency_ID,UserName,USER_DEPENDED,Receipt_Num,JournalNumber FROM [dbo].[ACC_BALANCE_V] WHERE B_T_ID = " & T_ID_txt_2.Text & " ORDER BY Debit ASC ", C.Con)
'            da.Fill(DT)

'            If DT.Rows.Count > 0 Then

'                DataGridView1.DataSource = DT

'                Depended_Label_2.Visible = True

'                M_Notes_txt.Text = DT(0)("MASTER_NOTES")
'                Date_.Text = DT(0)("DATE")

'                Depended_Label_2.Text = DT(0)("ACC_Depend_Status")
'                COST_CM.SelectedValue = DT(0)("COST_ID")

'                Input_User_Txt.Text = DT(0)("UserName")
'                Depended_User_Txt.Text = DT(0)("USER_DEPENDED")

'                If DT(0)("is_Depended") = 0 Then
'                    Depended_Label_2.Text = b_Depend_not_str
'                    Depended_Label_2.ForeColor = Color.DarkRed

'                    Enable_Fields(True)
'                Else
'                    Depended_Label_2.Text = b_Depend_str
'                    Depended_Label_2.ForeColor = Color.DarkGreen
'                    Enable_Fields(False)
'                End If

'                If Not IsDBNull(DT(0)("JournalNumber")) Then NextNumber_TextBox.Text = DT(0)("JournalNumber")

'                If DataGridView1.Rows.Count > 0 Then
'                    DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells("ACC_CODE_CL")
'                    DataGridView1.Columns("Receipt_Num").Visible = False
'                    DataGridView1.Columns("JournalNumber").Visible = False
'                    For Each col As DataGridViewColumn In DataGridView1.Columns
'                        col.SortMode = DataGridViewColumnSortMode.NotSortable
'                    Next

'                End If

'                ReceiptNum_Txt.Text = DT(0)("Receipt_Num")


'                UcGridColumnsSelector1.BindGrid(
'DataGridView1,
'New List(Of String) From {"T_ID_CL", "ACC_Depend_Status_CL", "is_Depended_CL", "Currency_ID_CL", "COST_ID_CL", "UserName_CL", "USER_DEPENDED_CL", "JournalNumber", "Receipt_Num"},
'Me.Name.ToString
')
'            Else
'                EDIT_Btn.Enabled = False
'                Depended_Label_2.Visible = False
'                T_ID_txt_2.Focus()
'                Currency_Cm.Enabled = True
'            End If

'        End If
'    End Sub


'    Private Sub Enable_Fields(f As Boolean)
'        M_Notes_txt.Enabled = f
'        Fields_Panel.Enabled = f
'        'Grid_GroupBox.Enabled = f
'        ADD_Btn.Enabled = f
'        REMOVE_BTN.Enabled = f
'        Depend_Btn.Enabled = f
'        Edit_title_date_Btn.Enabled = f
'        EDIT_Btn.Enabled = Not f
'        'Currency_Cm.Enabled = f
'        'Currency_Equal_txt.Enabled = f
'    End Sub

'    Private Sub LOAD_ALL_BALANCES()

'        ACC_CODE_DT.Clear()
'        ACC_CODE_DT = Accounts_Datatable

'    End Sub

'    Public Sub Load_Balances()


'        COST_CM.DataSource = CostCenter_Datatable
'        COST_CM.DisplayMember = "COST_NAME"
'        COST_CM.ValueMember = "COST_ID"

'        Currency_Cm.DataSource = Currencies_Datatable
'        Currency_Cm.DisplayMember = "Cr_Name"
'        Currency_Cm.ValueMember = "Cr_ID"

'        LOAD_ALL_BALANCES()
'    End Sub


'    Private Sub B_Name_Cm_KeyDown(sender As Object, e As KeyEventArgs) Handles B_Name_Cm.KeyDown
'        If e.KeyCode = Keys.Return Then
'            If TypeName(B_Name_Cm.SelectedValue) = "String" Then
'                B_NUM_txt.Text = B_Name_Cm.SelectedValue
'                SELECT_ACC_NATURAL()

'                If B_Name_Cm.SelectedIndex = -1 Then
'                    Credit_Label.Visible = False
'                    Debit_Label.Visible = False
'                End If

'                B_Name_Cm.DroppedDown = False

'            End If
'        End If

'        If e.KeyCode = Keys.Right Then If B_Name_Cm.SelectionStart = 0 Then B_NUM_txt.Select()


'    End Sub


'    Private Sub B_NUM_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles B_NUM_txt.KeyDown
'        If e.KeyCode = Keys.Return Then If ACC_CODE_DT.Rows.Count > 0 Then B_Name_Cm.SelectedValue = B_NUM_txt.Text
'    End Sub

'    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles NEW_Btn.Click
'        Enable_Fields(True)
'        Clear_Fields()
'        T_ID_txt_2.Text = Load_Balances_MAX_ID()
'        Currency_Cm.Enabled = True
'        Currency_Equal_txt.Enabled = True
'    End Sub

'    Private Sub Clear_Input_Fields()
'        For Each a As Control In Fields_Panel.Controls
'            If TypeOf a Is TextBox Then
'                a.Text = ""
'            End If
'        Next

'        '  B_Name_Cm.BackColor = System.Drawing.Color.Gainsboro
'        B_Name_Cm.Tag = 0
'    End Sub


'    Private Sub Clear_Fields()

'        Clear_Input_Fields()

'        Rows_txt.Clear()
'        Total_B_txt.Text = 0
'        Total_C_txt.Text = 0
'        Total_D_txt.Text = 0


'        Date_.Value = Date.Now
'        B_Name_Cm.SelectedIndex = -1

'        DT.Clear()
'        DT = New DataTable

'        T_ID_Details_txt.Text = 0
'        M_Notes_txt.Clear()
'        ReceiptNum_Txt.Clear()

'        Depended_Label_2.Visible = False
'        b_status_Label_2.Visible = False

'        NextNumber_TextBox.Clear()

'        'SELECT_Balance()
'    End Sub

'    Dim tmp_ID

'    Private Sub Copy_btn_Click(sender As Object, e As EventArgs) Handles Copy_btn.Click
'        Notes_txt.Text = M_Notes_txt.Text
'    End Sub

'    Private Sub DataGridView1_DataSourceChanged(sender As Object, e As EventArgs) Handles DataGridView1.DataSourceChanged


'        If DT.Rows.Count > 0 Then
'            b_status_Label_2.Visible = True
'            Compute_Balance(DT)
'            Total_C_txt.Text = T_CREDIT.ToString()
'            Total_D_txt.Text = T_DEBIT.ToString()
'            TOTAL_C_N.Text = Module1.TOTAL_C_N
'            TOTAL_D_N.Text = Module1.TOTAL_D_N

'            Rows_txt.Text = DT.Rows.Count

'            Total_B_txt.Text = Convert.ToDouble(Total_D_txt.Text) - Convert.ToDouble(Total_C_txt.Text)
'        Else
'            b_status_Label_2.Visible = False
'            Total_C_txt.Text = 0
'            Total_D_txt.Text = 0
'            Total_B_txt.Text = 0
'            Rows_txt.Text = 0
'            TOTAL_C_N.Text = 0
'            TOTAL_D_N.Text = 0
'        End If

'        If Total_C_txt.Text = Total_D_txt.Text Then
'            b_status_Label_2.Text = b_balanced_str
'            'b_status_Label_2.BackColor = Drawing.Color.PaleGreen
'            b_status_Label_2.ForeColor = Color.DarkGreen
'        Else
'            b_status_Label_2.Text = b_balanced_not_str
'            'b_status_Label_2.BackColor = Drawing.Color.LightCoral
'            b_status_Label_2.ForeColor = Color.DarkRed
'        End If

'        If DataGridView1.ColumnCount > 0 Then

'            If DataGridView1.Rows.Count > 0 Then
'                Currency_Cm.SelectedValue = DataGridView1.Rows(0).Cells("Currency_ID_CL").Value
'                Currency_Equal_txt.Text = DataGridView1.Rows(0).Cells("Currency_Equal_CL").Value
'                'Currency_Cm.Enabled = False
'                Currency_Equal_txt.Enabled = False
'            Else
'                'Currency_Cm.Enabled = True
'                Currency_Equal_txt.Enabled = True
'            End If

'        Else
'            'Currency_Cm.Enabled = True
'            Currency_Equal_txt.Enabled = True

'        End If

'    End Sub

'    Private Sub REMOVE_BTN_Click(sender As Object, e As EventArgs) Handles REMOVE_BTN.Click
'        If DataGridView1.Rows.Count > 0 Then
'            If MessageBox.Show(" تأكيد حذف السجـــل ... " & vbNewLine & DataGridView1.CurrentRow.Cells("ACC_NAME_CL").Value, "تاكيــد العملية", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.OK Then

'                ACC_BALANCE_proc_DELETE(DataGridView1.CurrentRow.Cells("ACC_NAME_CL").Value)
'            End If
'        End If
'    End Sub

'    Private Sub ACC_BALANCE_proc_DELETE(ACC_NAME As String)
'        Dim C As New C

'        With C.Com
'            .Connection = C.Con
'            .CommandText = "[ACC_BALANCE_proc]"
'            .CommandType = CommandType.StoredProcedure
'            .Parameters.AddWithValue("@T_ID", DataGridView1.CurrentRow.Cells("T_ID_CL").Value)
'            .Parameters.AddWithValue("@B_T_ID", T_ID_txt_2.Text)
'            .Parameters.AddWithValue("@Process", "DELETE")
'            .Parameters.AddWithValue("@OP_Status", 1)
'            .Parameters.Add("@ERROR_MSG", SqlDbType.NVarChar, 500)
'            .Parameters.AddWithValue("@NextNumber", "")

'            .Parameters("@OP_Status").Direction = ParameterDirection.Output
'            .Parameters("@ERROR_MSG").Direction = ParameterDirection.Output
'            .Parameters("@NextNumber").Direction = ParameterDirection.Output

'            C.Con.Open()
'            C.Com.ExecuteScalar()
'            C.Con.Close()

'            If C.Com.Parameters("@OP_Status").Value.ToString() = "0" Then
'                MsgBox(C.Com.Parameters("@ERROR_MSG").Value.ToString(), MsgBoxStyle.Critical, "خطــأ")
'            Else
'                Dim notification3 As New NotificationForm("تنويه", " تم حذف السجل " & ACC_NAME, "bottom")
'                notification3.ShowNotification()

'            End If

'            SELECT_Balance()

'        End With
'    End Sub

'    Private Sub SEARCH_ACC_BTN_Click(sender As Object, e As EventArgs) Handles SEARCH_ACC_BTN.Click
'        'ACC_CODE_Search = ""
'        MOVE_TO_ACCOUNTS_MENU()
'    End Sub


'    Private Sub MOVE_TO_ACCOUNTS_MENU()
'        BALANCE_SEARCH.ShowDialog()
'        If ACC_CODE_Search <> "" Then B_NUM_txt.Text = ACC_CODE_Search
'    End Sub



'    Private Sub B_NUM_txt_TextChanged(sender As Object, e As EventArgs) Handles B_NUM_txt.TextChanged

'        If B_NUM_txt.Text.Count > 0 Then
'            Filter_B()
'        Else
'            LOAD_ALL_BALANCES()
'        End If
'        ACC_CODE_NUM_ErrorProvider.Clear()
'    End Sub

'    Private Sub Filter_B()

'        ACC_CODE_DT = GetAccountTreeDataTable(Accounts_Datatable, B_NUM_txt.Text)

'        B_Name_Cm.DataSource = ACC_CODE_DT
'        B_Name_Cm.DisplayMember = "ACC_NAME"
'        B_Name_Cm.ValueMember = "ACC_CODE"
'        B_Name_Cm.DroppedDown = True
'        If ACC_CODE_DT.Rows.Count = 0 Then B_Name_Cm.Text = ""

'    End Sub

'    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
'        Me.Close()
'    End Sub

'    Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
'        Try
'            If DataGridView1.Columns(e.ColumnIndex).Name = "دائن" Then
'                If Not IsDBNull(e.Value) Then
'                    e.CellStyle.BackColor = Color.FromArgb(255, 192, 192)
'                    e.CellStyle.ForeColor = Drawing.Color.DarkRed
'                    'ElseIf e.Value = "تم اعداد التقرير النهائي" Then
'                    '    e.CellStyle.BackColor = Drawing.Color.DarkGreen
'                    '    e.CellStyle.ForeColor = Drawing.Color.White

'                End If
'            End If

'            If DataGridView1.Columns(e.ColumnIndex).Name = "مدين" Then
'                If Not IsDBNull(e.Value) Then
'                    e.CellStyle.BackColor = Color.FromArgb(192, 255, 192)
'                    e.CellStyle.ForeColor = Drawing.Color.DarkGreen
'                    'ElseIf e.Value = "تم اعداد التقرير النهائي" Then
'                    '    e.CellStyle.BackColor = Drawing.Color.DarkGreen
'                    '    e.CellStyle.ForeColor = Drawing.Color.White

'                End If
'            End If


'        Catch ex As Exception

'        End Try
'    End Sub


'    Private Sub Depend_Btn_Click(sender As Object, e As EventArgs) Handles Depend_Btn.Click
'        If Total_B_txt.Text <> 0 Then

'            Dim notification3 As New NotificationForm("خطــأ فالإعتمــاد", " القيــد غير مــوزون ", "bottom", True)
'            notification3.ShowNotification()

'            'MsgBox("القيــد غير مــوزون", MsgBoxStyle.Critical, "خطــأ فالإعتمــاد")
'            Exit Sub
'        End If
'        If MessageBox.Show("سيتم إعتماد القيــد رقم ( " & T_ID_txt_2.Text & " ) ولن يتم التعديل فيه بعد الأن .. هل أنت متاكد ", "تاكيــد العملية", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.OK Then
'            ACC_BALANCE_DEPEND(True)
'        End If

'    End Sub




'    Private Sub ACC_BALANCE_DEPEND(Depended As Boolean)
'        Dim C As New C

'        With C.Com
'            .Connection = C.Con
'            .CommandText = "[ACC_BALANCE_DEPEND]"
'            .CommandType = CommandType.StoredProcedure
'            .Parameters.AddWithValue("@T_ID", T_ID_txt_2.Text)
'            .Parameters.AddWithValue("@Depended", Depended)
'            .Parameters.AddWithValue("@USER_ID", USER_ID)
'            .Parameters.AddWithValue("@OP_Status", 1)
'            .Parameters.Add("@ERROR_MSG", SqlDbType.NVarChar, 500)
'            .Parameters.Add("@NextNumber", SqlDbType.NVarChar, 500)

'            .Parameters("@OP_Status").Direction = ParameterDirection.Output
'            .Parameters("@ERROR_MSG").Direction = ParameterDirection.Output
'            .Parameters("@NextNumber").Direction = ParameterDirection.Output

'            SQL_SP_EXEC(C.Com)

'            If C.Com.Parameters("@OP_Status").Value.ToString() = "0" Then
'                Dim notification3 As New NotificationForm("خطأ فالتحرير", C.Com.Parameters("@ERROR_MSG").Value.ToString(), "bottom", True)
'                notification3.ShowNotification()
'            Else
'                If Depended = True Then
'                    Dim notification3 As New NotificationForm("إشعار", " تم إعتمــاد القيــد " & T_ID_txt_2.Text, "bottom")
'                    notification3.ShowNotification()
'                Else
'                    Dim notification3 As New NotificationForm("إشعار", " تم تحريــر القيــد " & T_ID_txt_2.Text, "bottom")
'                    notification3.ShowNotification()
'                    Enable_Fields(True)
'                End If
'                Clear_Fields()
'                SELECT_Balance()
'            End If


'        End With
'    End Sub

'    Private Sub DataGridView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseDoubleClick

'        If DataGridView1.Rows.Count > 0 Then

'            If Not String.IsNullOrWhiteSpace(T_ID_Details_txt.Text) Then
'                If Convert.ToInt16(T_ID_Details_txt.Text) > 0 Then
'                    Dim notification3 As New NotificationForm("خطــأ فالتعديل", " يوجد سجــل قيد التعديـــل ... قم بإدراته اولا ", "bottom", True)
'                    notification3.ShowNotification()
'                    '  MsgBox("يوجد سجــل قيد التعديـــل ... قم بإدراته اولا", MsgBoxStyle.Critical, "خطــأ فالتعديل")
'                    Exit Sub
'                End If
'            End If


'            If DataGridView1.CurrentRow.Cells("is_Depended_CL").Value = 0 Then

'                'Clear_Fields()
'                T_ID_Details_txt.Text = DataGridView1.CurrentRow.Cells("T_ID_CL").Value
'                COST_CM.SelectedValue = DataGridView1.CurrentRow.Cells("COST_ID_CL").Value
'                Bill_Num_txt.Text = DataGridView1.CurrentRow.Cells("Bill_Num_CL").Value
'                B_NUM_txt.Text = DataGridView1.CurrentRow.Cells("ACC_CODE_CL").Value
'                'B_Name_Cm.SelectedValue = DataGridView1.CurrentRow.Cells("ACC_CODE_CL").Value

'                Currency_Cm.SelectedValue = DataGridView1.CurrentRow.Cells("Currency_ID_CL").Value
'                Currency_Equal_txt.Text = DataGridView1.CurrentRow.Cells("Currency_Equal_CL").Value

'                If Not IsDBNull(DataGridView1.CurrentRow.Cells("CREDIT_CL").Value) Then
'                    CREDIT_txt.Text = Math.Round(DataGridView1.CurrentRow.Cells("CREDIT_CL").Value / Currency_Equal_txt.Text, 3)
'                End If

'                If Not IsDBNull(DataGridView1.CurrentRow.Cells("DEBIT_CL").Value) Then
'                    DEBIT_txt.Text = Math.Round(DataGridView1.CurrentRow.Cells("DEBIT_CL").Value / Currency_Equal_txt.Text, 3)
'                End If

'                Notes_txt.Text = DataGridView1.CurrentRow.Cells("Notes_CL").Value
'                DataGridView1.Rows.Remove(DataGridView1.CurrentRow)
'                B_Name_Cm.DroppedDown = False

'            End If


'        End If
'    End Sub

'    Private Sub T_ID_Details_txt_TextChanged(sender As Object, e As EventArgs) Handles T_ID_Details_txt.TextChanged
'        If T_ID_Details_txt.Text.Count > 0 Then
'            If T_ID_Details_txt.Text > 0 Then
'                Entry_Label.Text = "(تعديــل الإدخــال)"
'                Entry_Label.BackColor = Color.LightGray
'            Else

'                Entry_Label.Text = "(إدخــال جديــد)"
'                Entry_Label.BackColor = Color.LightYellow
'            End If
'        End If

'    End Sub

'    Private Sub Refresh_Btn_Click(sender As Object, e As EventArgs) Handles Refresh_Btn.Click
'        'Clear_Fields()
'        Clear_Input_Fields()
'        SELECT_Balance()
'    End Sub



'    Private Sub Edit_title_date_Btn_Click(sender As Object, e As EventArgs) Handles Edit_title_date_Btn.Click
'        ACC_BALANCE_proc("UPDATE_MASTER")
'    End Sub

'    Private Sub Cancel_Btn_Click(sender As Object, e As EventArgs) Handles EDIT_Btn.Click
'        If MessageBox.Show(" تأكيد التحرير ... سيتم إلغاء الإعتماد للقيد  ", "تاكيــد العملية", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.OK Then
'            ACC_BALANCE_DEPEND(False)
'        End If
'    End Sub

'    Private Sub DEBIT_txt_TextChanged(sender As Object, e As EventArgs) Handles DEBIT_txt.TextChanged
'        If DEBIT_txt.Text.Count > 0 Then CREDIT_txt.Clear()
'    End Sub

'    Private Sub CREDIT_txt_TextChanged(sender As Object, e As EventArgs) Handles CREDIT_txt.TextChanged
'        If CREDIT_txt.Text.Count > 0 Then DEBIT_txt.Clear()
'    End Sub

'    Private Sub Notes_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Notes_txt.KeyDown
'        If e.KeyCode = Keys.Return Then
'            If ADD_Btn.Enabled = True Then ADD_Btn_Click(sender, e)
'        End If
'    End Sub


'    Private Sub B_Name_Cm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles B_Name_Cm.SelectedIndexChanged
'        ACC_CODE_ErrorProvider.Clear()
'    End Sub


'    Private Sub SELECT_ACC_NATURAL()


'        Dim C = New C
'        Try
'            Dim S As String = "SELECT ACC_NATURAL,is_Lock_Trans FROM [ACCOUNTS_TREE_V] WHERE ACC_CODE = " & B_Name_Cm.SelectedValue
'            C.Com = New SqlClient.SqlCommand(S, C.Con)
'            C.Con.Open()
'            C.Dr = C.Com.ExecuteReader
'            If C.Dr.HasRows Then
'                C.Dr.Read()
'                If C.Dr("ACC_NATURAL") = "C" Then
'                    Debit_Label.Visible = False
'                    Credit_Label.Visible = True
'                Else
'                    Debit_Label.Visible = True
'                    Credit_Label.Visible = False
'                End If

'                If C.Dr("is_Lock_Trans") = 1 Then
'                    ' B_Name_Cm.BackColor = System.Drawing.Color.IndianRed
'                    MsgBox("لا يمكن إضافة قيود لهذا الحساب", MsgBoxStyle.Critical, "القيد مقفل")
'                    ADD_Btn.Enabled = False
'                    B_Name_Cm.Tag = 1
'                Else
'                    '  B_Name_Cm.BackColor = System.Drawing.Color.Gainsboro
'                    ADD_Btn.Enabled = True
'                    B_Name_Cm.Tag = 0
'                End If

'            End If
'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try

'    End Sub

'    Private Sub B_NUM_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles B_NUM_txt.KeyPress
'        Check_Only_Int(sender, e)
'    End Sub

'    Private Sub Currency_Cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles Currency_Cm.SelectedValueChanged

'        If Currency_Cm.SelectedValue IsNot Nothing AndAlso Not TypeOf Currency_Cm.SelectedValue Is System.Data.DataRowView Then
'            Currency_Equal_txt.Text = Get_Equal(Currency_Cm.SelectedValue, Date_, 0)
'        End If
'    End Sub

'    Public Sub Print_B(Optional ByVal exportToExcel As Boolean = False)
'        Try
'            If DataGridView1.Rows.Count = 0 Then
'                MessageBox.Show("لا توجد بيانات للطباعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
'                Exit Sub
'            End If

'            If Not exportToExcel Then
'                PreparePrint()
'                PPD.Document = PD
'                PPD.WindowState = FormWindowState.Maximized
'                PPD.ShowDialog()
'                Exit Sub
'            End If

'            Dim pp As New ReportConnection
'            pp.rp.Load(Application.StartupPath & "\Reports\ACC_B_" & print_cmb.SelectedIndex.ToString & ".rpt")
'            pp.LoadTables()

'            With pp
'                .rp.SetParameterValue("TITLE_NUM", " قيــد يومية ")
'                .rp.SetParameterValue("DATE", Date_.Text)
'                .rp.SetParameterValue("Title_1", MY_Settings.SBill_Title_1)
'                .rp.SetParameterValue("Title_2", MY_Settings.SBill_Title_2)
'                .rp.SetParameterValue("Bill_ID", T_ID_txt_2.Text)
'                .rp.SetParameterValue("T_CREDIT", Total_C_txt.Text)
'                .rp.SetParameterValue("T_DEBIT", Total_D_txt.Text)
'                .rp.SetParameterValue("TOTAL_D_N", TOTAL_D_N.Text)
'                .rp.SetParameterValue("TOTAL_C_N", TOTAL_C_N.Text)
'                .rp.SetParameterValue("T_ROWS", Rows_txt.Text)
'                .rp.SetParameterValue("TITLE_Bill", M_Notes_txt.Text)
'                .rp.SetParameterValue("USER_Input", Input_User_Txt.Text)
'                .rp.SetParameterValue("User_Depended", Depended_User_Txt.Text)



'                ' ✅ التحقق من أول CheckBox
'                If clb.GetItemChecked(0) Then
'                    ' مثلاً: إضافة باراميتر لو أول خيار محدد
'                    .rp.SetParameterValue("Money_char", HANY(T_CREDIT, "LYD"))
'                Else
'                    .rp.SetParameterValue("Money_char", "")
'                End If

'                'If Money_Char_CB.Checked = True Then
'                '    .rp.SetParameterValue("Money_char", HANY(T_CREDIT, "LYD")) 'Get_Currency_Tag(DataGridView1.CurrentRow.Cells("Currency_ID_CL").Value))
'                'Else
'                '    .rp.SetParameterValue("Money_char", "")
'                'End If
'            End With

'            ' **تصدير التقرير إلى Excel بدلاً من الطباعة إذا تم اختيار التصدير**
'            If exportToExcel Then
'                Dim saveDialog As New SaveFileDialog()
'                saveDialog.Filter = "Excel Files|*.xls"
'                saveDialog.Title = "حفظ التقرير كملف Excel"
'                saveDialog.FileName = "قيد رقم (" & T_ID_txt_2.Text & ").xls"

'                If saveDialog.ShowDialog() = DialogResult.OK Then
'                    Dim exportPath As String = saveDialog.FileName
'                    ExportReportToExcel(pp.rp, exportPath)
'                End If
'            Else
'                ' **عرض التقرير للطباعة**
'                Dim p As New print
'                p.CrystalReportViewer1.ReportSource = pp.rp
'                p.ShowDialog()
'            End If
'        Catch ex As Exception
'            MessageBox.Show("حدث خطأ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        End Try
'    End Sub


'    Private Sub PreparePrint()
'        CurrentRow = 0
'        PageNumber = 1
'        TotalPages = 1
'        PrintableRows.Clear()

'        PD.DefaultPageSettings.Landscape = CurrentPrintLandscape
'        PD.DefaultPageSettings.Margins = New Margins(25, 25, 30, 30)

'        BuildPrintableRows()
'        BuildPrintableColumns()
'        TotalPages = EstimateTotalPages()
'    End Sub


'    Private Sub PreparePrintMenu()
'        Dim printLandscapeItem As New ToolStripMenuItem("طباعة بالعرض")
'        Dim printPortraitItem As New ToolStripMenuItem("طباعة بالطول")

'        AddHandler printLandscapeItem.Click,
'            Sub()
'                CurrentPrintLandscape = True
'                print_cmb.SelectedIndex = 0
'                PRINT_ACC_B()
'            End Sub

'        AddHandler printPortraitItem.Click,
'            Sub()
'                CurrentPrintLandscape = False
'                print_cmb.SelectedIndex = 1
'                PRINT_ACC_B()
'            End Sub

'        Print_CntxtMStrip.Items.Insert(0, printPortraitItem)
'        Print_CntxtMStrip.Items.Insert(0, printLandscapeItem)
'        Print_CntxtMStrip.Items.Insert(2, New ToolStripSeparator())
'    End Sub


'    Private Sub BuildPrintableRows()
'        PrintableRows.Clear()

'        For i As Integer = 0 To DataGridView1.Rows.Count - 1
'            If DataGridView1.Rows(i).IsNewRow Then Continue For
'            PrintableRows.Add(i)
'        Next
'    End Sub


'    Private Sub BuildPrintableColumns()
'        PrintableColumns.Clear()

'        PrintableColumns.Add(New PrintColumnInfo With {
'            .ColumnName = "",
'            .HeaderText = "م",
'            .SourceWidth = 45,
'            .IsSerial = True
'        })

'        Dim visibleColumns = DataGridView1.Columns.Cast(Of DataGridViewColumn)().
'            Where(Function(c) c.Visible).
'            OrderBy(Function(c) c.DisplayIndex)

'        For Each col As DataGridViewColumn In visibleColumns
'            PrintableColumns.Add(New PrintColumnInfo With {
'                .ColumnName = col.Name,
'                .HeaderText = If(String.IsNullOrWhiteSpace(col.HeaderText), col.Name, col.HeaderText),
'                .SourceWidth = Math.Max(col.Width, 60),
'                .IsSerial = False
'            })
'        Next
'    End Sub


'    Private Sub PD_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PD.PrintPage
'        Dim g = e.Graphics
'        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

'        Dim marginLeft As Integer = e.MarginBounds.Left
'        Dim marginRight As Integer = e.MarginBounds.Right
'        Dim y As Integer = e.MarginBounds.Top
'        Dim pageWidth As Integer = e.MarginBounds.Width

'        Dim companyFontAr As New Font("Tahoma", 12, FontStyle.Bold)
'        Dim companyFontEn As New Font("Tahoma", 11, FontStyle.Bold)
'        Dim titleFont As New Font("Tahoma", 14, FontStyle.Bold)
'        Dim subTitleFont As New Font("Tahoma", 9.5!, FontStyle.Bold)
'        Dim headerFont As New Font("Tahoma", If(CurrentPrintLandscape, 9.0!, 8.0!), FontStyle.Bold)
'        Dim bodyFont As New Font("Tahoma", If(CurrentPrintLandscape, 8.5!, 7.75!), FontStyle.Regular)
'        Dim totalFont As New Font("Tahoma", 9, FontStyle.Bold)

'        Dim sfRight As New StringFormat With {
'            .Alignment = StringAlignment.Far,
'            .LineAlignment = StringAlignment.Center,
'            .FormatFlags = StringFormatFlags.DirectionRightToLeft
'        }

'        Dim sfCenter As New StringFormat With {
'            .Alignment = StringAlignment.Center,
'            .LineAlignment = StringAlignment.Center,
'            .FormatFlags = StringFormatFlags.DirectionRightToLeft
'        }

'        Dim sfLeft As New StringFormat With {
'            .Alignment = StringAlignment.Near,
'            .LineAlignment = StringAlignment.Center
'        }

'        g.DrawString(MY_Settings.SBill_Title_1, companyFontAr, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 24), sfRight)
'        y += 26
'        g.DrawString(MY_Settings.SBill_Title_2, companyFontEn, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 22), sfRight)
'        y += 26
'        g.DrawLine(Pens.Black, marginLeft, y, marginRight, y)
'        y += 8

'        g.DrawString("قيــــد يوميــــة", titleFont, Brushes.Black, New RectangleF(marginLeft, y, pageWidth, 26), sfCenter)
'        y += 28
'        g.DrawString("صفحة " & PageNumber.ToString() & " من " & TotalPages.ToString(), bodyFont, Brushes.Black, New RectangleF(marginLeft, y - 4, pageWidth, 18), sfLeft)
'        y += 18

'        DrawEntryInfo(g, marginLeft, y, pageWidth, totalFont, sfCenter)
'        y += 96

'        Dim notesHeight As Integer = DrawMasterNotes(g, marginLeft, y, pageWidth, bodyFont, sfRight)
'        y += notesHeight + 8

'        Dim colWidths = GetPrintColumnWidths(pageWidth)
'        DrawPrintHeader(g, marginLeft, y, colWidths, headerFont, sfCenter)
'        y += 30

'        While CurrentRow < PrintableRows.Count
'            Dim row As DataGridViewRow = DataGridView1.Rows(PrintableRows(CurrentRow))
'            Dim rowHeight As Integer = EstimateEntryRowHeight(g, row, bodyFont, colWidths)

'            If y + rowHeight > e.MarginBounds.Bottom - 130 Then
'                e.HasMorePages = True
'                PageNumber += 1
'                Return
'            End If

'            DrawEntryRow(g, row, marginLeft, y, rowHeight, colWidths, bodyFont, sfCenter, sfRight)
'            y += rowHeight
'            CurrentRow += 1
'        End While

'        y += 8
'        DrawTotals(g, marginLeft, y, pageWidth, totalFont, sfCenter)

'        e.HasMorePages = False
'        CurrentRow = 0
'        PageNumber = 1
'    End Sub


'    Private Sub DrawEntryInfo(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, font As Font, sfCenter As StringFormat)
'        Dim boxHeight As Integer = 28
'        Dim boxWidth As Integer = CInt(pageWidth / 4)

'        DrawSummaryBoxesRow(g, x, y, pageWidth, boxWidth, boxHeight,
'                            {"رقم القيد", "الرقم الإشاري", "رقم الإيصال", "حالة القيد"},
'                            {T_ID_txt_2.Text, NextNumber_TextBox.Text, ReceiptNum_Txt.Text, GetEntryStatusText()},
'                            font, sfCenter)

'        y += boxHeight + 4

'        DrawSummaryBoxesRow(g, x, y, pageWidth, CInt(pageWidth / 3), boxHeight,
'                            {"تاريخ القيد", "معد القيد", "مراجع القيد"},
'                            {Date_.Value.ToString("dd/MM/yyyy"), Input_User_Txt.Text, Depended_User_Txt.Text},
'                            font, sfCenter)
'    End Sub


'    Private Function DrawMasterNotes(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, font As Font, sfRight As StringFormat) As Integer
'        Dim notes As String = M_Notes_txt.Text
'        Dim noteFormat As New StringFormat With {
'            .Alignment = StringAlignment.Near,
'            .LineAlignment = StringAlignment.Center,
'            .FormatFlags = StringFormatFlags.DirectionRightToLeft
'        }
'        Dim height As Integer = CInt(g.MeasureString("شرح القيد: " & notes, font, pageWidth - 12, noteFormat).Height) + 12
'        If height < 30 Then height = 30

'        Dim rect As New Rectangle(x, y, pageWidth, height)
'        g.FillRectangle(New SolidBrush(Color.FromArgb(248, 248, 248)), rect)
'        g.DrawRectangle(Pens.Black, rect)
'        g.DrawString("شرح القيد: " & notes, font, Brushes.Black, New RectangleF(rect.X + 6, rect.Y + 2, rect.Width - 12, rect.Height - 4), noteFormat)

'        Return height
'    End Function


'    Private Function GetPrintColumnWidths(pageWidth As Integer) As Integer()
'        If PrintableColumns.Count = 0 Then BuildPrintableColumns()

'        Dim widths As New List(Of Integer)
'        Dim totalSourceWidth As Integer = 0

'        For Each col As PrintColumnInfo In PrintableColumns
'            totalSourceWidth += col.SourceWidth
'        Next

'        If totalSourceWidth <= 0 Then totalSourceWidth = 1

'        Dim usedWidth As Integer = 0
'        For i As Integer = 0 To PrintableColumns.Count - 1
'            Dim w As Integer

'            If i = PrintableColumns.Count - 1 Then
'                w = pageWidth - usedWidth
'            Else
'                w = CInt((PrintableColumns(i).SourceWidth / totalSourceWidth) * pageWidth)
'                If PrintableColumns(i).IsSerial AndAlso w < 38 Then w = 38
'                If Not PrintableColumns(i).IsSerial AndAlso w < 55 Then w = 55
'            End If

'            widths.Add(w)
'            usedWidth += w
'        Next

'        Return widths.ToArray()
'    End Function


'    Private Sub DrawPrintHeader(g As Graphics, x As Integer, y As Integer, colWidths As Integer(), headerFont As Font, sfCenter As StringFormat)
'        Dim currentX As Integer = x + TotalColumnWidth(colWidths)

'        For i As Integer = 0 To PrintableColumns.Count - 1
'            currentX -= colWidths(i)
'            Dim rect As New Rectangle(currentX, y, colWidths(i), 30)
'            g.FillRectangle(New SolidBrush(Color.FromArgb(225, 225, 225)), rect)
'            g.DrawRectangle(New Pen(Color.FromArgb(80, 80, 80)), rect)
'            g.DrawString(PrintableColumns(i).HeaderText, headerFont, Brushes.Black, New RectangleF(rect.X, rect.Y, rect.Width, rect.Height), sfCenter)
'        Next
'    End Sub


'    Private Sub DrawEntryRow(g As Graphics, row As DataGridViewRow, x As Integer, y As Integer, rowHeight As Integer, colWidths As Integer(), bodyFont As Font, sfCenter As StringFormat, sfRight As StringFormat)
'        Dim currentX As Integer = x + TotalColumnWidth(colWidths)

'        For i As Integer = 0 To PrintableColumns.Count - 1
'            currentX -= colWidths(i)
'            Dim rect As New Rectangle(currentX, y, colWidths(i), rowHeight)
'            If CurrentRow Mod 2 = 1 Then
'                g.FillRectangle(New SolidBrush(Color.FromArgb(250, 250, 250)), rect)
'            End If

'            g.DrawRectangle(New Pen(Color.FromArgb(150, 150, 150)), rect)

'            Dim value As String = GetPrintColumnValue(row, PrintableColumns(i))
'            Dim useFormat As StringFormat = If(IsTextColumn(PrintableColumns(i)), sfRight, sfCenter)
'            Dim brush As Brush = Brushes.Black

'            If IsDebitColumn(PrintableColumns(i)) Then brush = Brushes.DarkGreen
'            If IsCreditColumn(PrintableColumns(i)) Then brush = Brushes.DarkRed

'            g.DrawString(value, bodyFont, brush, New RectangleF(rect.X + 4, rect.Y + 2, rect.Width - 8, rect.Height - 4), useFormat)
'        Next
'    End Sub


'    Private Sub DrawTotals(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, totalFont As Font, sfCenter As StringFormat)
'        Dim boxHeight As Integer = 30
'        Dim boxWidth As Integer = CInt(pageWidth / 4)

'        g.DrawLine(Pens.Black, x, y, x + pageWidth, y)
'        y += 6

'        DrawSummaryBoxesRow(g, x, y, pageWidth, boxWidth, boxHeight,
'                            {"إجمالي المدين", "إجمالي الدائن", "الفرق", "عدد الصفوف"},
'                            {Total_C_txt.Text, Total_D_txt.Text, Total_B_txt.Text, Rows_txt.Text},
'                            totalFont, sfCenter)

'        y += boxHeight + 4

'        DrawSummaryBoxesRow(g, x, y, pageWidth, boxWidth, boxHeight,
'                            {"عدد المدين", "عدد الدائن", "معد التقرير", "تاريخ الطباعة"},
'                            {GetPositiveDebitRowsCount().ToString(), GetPositiveCreditRowsCount().ToString(), Input_User_Txt.Text, Date.Now.ToString("dd/MM/yyyy HH:mm")},
'                            totalFont, sfCenter)

'        If clb.GetItemChecked(0) Then
'            y += boxHeight + 6
'            Dim moneyRect As New Rectangle(x, y, pageWidth, 30)
'            g.FillRectangle(New SolidBrush(Color.FromArgb(248, 248, 248)), moneyRect)
'            g.DrawRectangle(Pens.Black, moneyRect)
'            g.DrawString("القيمة بالحروف: " & HANY(Total_C_txt.Text, "LYD"), totalFont, Brushes.Black, New RectangleF(moneyRect.X + 5, moneyRect.Y, moneyRect.Width - 10, moneyRect.Height), sfCenter)
'        End If
'    End Sub


'    Private Sub DrawSummaryBoxesRow(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, boxWidth As Integer, boxHeight As Integer, titles() As String, values() As String, totalFont As Font, sfCenter As StringFormat)
'        Dim currentX As Integer = x + pageWidth

'        For i As Integer = 0 To titles.Length - 1
'            currentX -= boxWidth
'            Dim rect As New Rectangle(currentX, y, boxWidth, boxHeight)
'            DrawSummaryBox(g, rect, titles(i), values(i), totalFont, sfCenter)
'        Next
'    End Sub


'    Private Sub DrawSummaryBox(g As Graphics, rect As Rectangle, title As String, value As String, totalFont As Font, sfCenter As StringFormat)
'        g.FillRectangle(New SolidBrush(Color.FromArgb(245, 245, 245)), rect)
'        g.DrawRectangle(Pens.Black, rect)
'        g.DrawString(title & ": " & value, totalFont, Brushes.Black, New RectangleF(rect.X + 5, rect.Y, rect.Width - 10, rect.Height), sfCenter)
'    End Sub


'    Private Function EstimateEntryRowHeight(g As Graphics, row As DataGridViewRow, bodyFont As Font, colWidths As Integer()) As Integer
'        Dim h As Integer = 30

'        For i As Integer = 0 To PrintableColumns.Count - 1
'            Dim value As String = GetPrintColumnValue(row, PrintableColumns(i))
'            Dim measuredHeight As Integer = CInt(g.MeasureString(value, bodyFont, Math.Max(colWidths(i) - 8, 20)).Height) + 12
'            If measuredHeight > h Then h = measuredHeight
'        Next

'        If h < 30 Then h = 30
'        Return h
'    End Function


'    Private Function EstimateTotalPages() As Integer
'        Using bmp As New Bitmap(10, 10)
'            Using g As Graphics = Graphics.FromImage(bmp)
'                Dim bodyFont As New Font("Tahoma", 8.0!, FontStyle.Regular)
'                Dim pageHeight As Integer
'                Dim pageWidth As Integer

'                If CurrentPrintLandscape Then
'                    pageHeight = PD.DefaultPageSettings.Bounds.Width - PD.DefaultPageSettings.Margins.Top - PD.DefaultPageSettings.Margins.Bottom
'                    pageWidth = PD.DefaultPageSettings.Bounds.Height - PD.DefaultPageSettings.Margins.Left - PD.DefaultPageSettings.Margins.Right
'                Else
'                    pageHeight = PD.DefaultPageSettings.Bounds.Height - PD.DefaultPageSettings.Margins.Top - PD.DefaultPageSettings.Margins.Bottom
'                    pageWidth = PD.DefaultPageSettings.Bounds.Width - PD.DefaultPageSettings.Margins.Left - PD.DefaultPageSettings.Margins.Right
'                End If

'                Dim colWidths = GetPrintColumnWidths(pageWidth)
'                Dim usableHeight As Integer = pageHeight - 300
'                Dim y As Integer = 0
'                Dim pages As Integer = 1

'                For Each rowIndex In PrintableRows
'                    Dim h As Integer = EstimateEntryRowHeight(g, DataGridView1.Rows(rowIndex), bodyFont, colWidths)

'                    If y + h > usableHeight Then
'                        pages += 1
'                        y = 0
'                    End If

'                    y += h
'                Next

'                Return pages
'            End Using
'        End Using
'    End Function


'    Private Function TotalColumnWidth(colWidths As Integer()) As Integer
'        Dim total As Integer = 0

'        For Each w As Integer In colWidths
'            total += w
'        Next

'        Return total
'    End Function


'    Private Function GetPrintColumnValue(row As DataGridViewRow, col As PrintColumnInfo) As String
'        If col Is Nothing Then Return ""
'        If col.IsSerial Then Return (CurrentRow + 1).ToString()

'        Dim value As String = GetCellText(row, col.ColumnName)

'        If IsAmountColumn(col) Then
'            Dim d As Decimal
'            If Decimal.TryParse(value, d) Then
'                If d = 0D Then Return ""
'                Return d.ToString("N3")
'            End If
'        End If

'        Dim dateValue As Date
'        If IsDateColumn(col) AndAlso Date.TryParse(value, dateValue) Then
'            Return dateValue.ToString("dd/MM/yyyy")
'        End If

'        Return value
'    End Function


'    Private Function IsTextColumn(col As PrintColumnInfo) As Boolean
'        If col Is Nothing OrElse col.IsSerial Then Return False

'        Dim key As String = NormalizeColumnName(col.ColumnName & col.HeaderText)
'        Return key.Contains("NAME") OrElse key.Contains("NOTES") OrElse key.Contains("شرح") OrElse
'               key.Contains("ملاحظة") OrElse key.Contains("اسمالحساب") OrElse key.Contains("إسمالحساب") OrElse
'               key.Contains("مركزالتكلفة") OrElse key.Contains("العملة")
'    End Function


'    Private Function IsAmountColumn(col As PrintColumnInfo) As Boolean
'        If col Is Nothing Then Return False

'        Dim key As String = NormalizeColumnName(col.ColumnName & col.HeaderText)
'        Return key.Contains("CREDIT") OrElse key.Contains("DEBIT") OrElse key.Contains("مدين") OrElse key.Contains("دائن") OrElse key.Contains("سعرالصرف")
'    End Function


'    Private Function IsDebitColumn(col As PrintColumnInfo) As Boolean
'        If col Is Nothing Then Return False

'        Dim key As String = NormalizeColumnName(col.ColumnName & col.HeaderText)
'        Return key.Contains("CREDIT") OrElse key.Contains("مدين")
'    End Function


'    Private Function IsCreditColumn(col As PrintColumnInfo) As Boolean
'        If col Is Nothing Then Return False

'        Dim key As String = NormalizeColumnName(col.ColumnName & col.HeaderText)
'        Return key.Contains("DEBIT") OrElse key.Contains("دائن")
'    End Function


'    Private Function IsDateColumn(col As PrintColumnInfo) As Boolean
'        If col Is Nothing Then Return False

'        Dim key As String = NormalizeColumnName(col.ColumnName & col.HeaderText)
'        Return key.Contains("DATE") OrElse key.Contains("تاريخ") OrElse key.Contains("الإدخال")
'    End Function


'    Private Function GetPositiveDebitRowsCount() As Integer
'        Return GetPositiveRowsCount(True)
'    End Function


'    Private Function GetPositiveCreditRowsCount() As Integer
'        Return GetPositiveRowsCount(False)
'    End Function


'    Private Function GetPositiveRowsCount(isDebit As Boolean) As Integer
'        Dim count As Integer = 0

'        For Each rowIndex As Integer In PrintableRows
'            Dim row As DataGridViewRow = DataGridView1.Rows(rowIndex)
'            Dim amountText As String

'            If isDebit Then
'                amountText = GetCellText(row, "CREDIT_CL", "CREDIT", "مدين")
'            Else
'                amountText = GetCellText(row, "DEBIT_CL", "DEBIT", "دائن")
'            End If

'            Dim amount As Decimal
'            If Decimal.TryParse(amountText, amount) AndAlso amount > 0D Then
'                count += 1
'            End If
'        Next

'        Return count
'    End Function


'    Private Function GetCellText(row As DataGridViewRow, ParamArray columnNames() As String) As String
'        For Each columnName As String In columnNames
'            Dim columnIndex As Integer = FindColumnIndex(columnName)

'            If columnIndex >= 0 AndAlso columnIndex < row.Cells.Count Then
'                Dim value = row.Cells(columnIndex).Value
'                If value IsNot Nothing AndAlso Not IsDBNull(value) Then Return value.ToString()
'            End If
'        Next

'        Return ""
'    End Function


'    Private Function FindColumnIndex(columnName As String) As Integer
'        Dim target As String = NormalizeColumnName(columnName)

'        For Each col As DataGridViewColumn In DataGridView1.Columns
'            Dim nameText As String = NormalizeColumnName(col.Name)
'            Dim headerText As String = NormalizeColumnName(col.HeaderText)
'            Dim propertyText As String = NormalizeColumnName(col.DataPropertyName)

'            If String.Equals(nameText, target, StringComparison.OrdinalIgnoreCase) OrElse
'               String.Equals(headerText, target, StringComparison.OrdinalIgnoreCase) OrElse
'               String.Equals(propertyText, target, StringComparison.OrdinalIgnoreCase) Then
'                Return col.Index
'            End If
'        Next

'        Return -1
'    End Function


'    Private Function NormalizeColumnName(value As String) As String
'        If value Is Nothing Then Return ""

'        Return value.Replace("ـ", "").
'                     Replace(" ", "").
'                     Replace("_", "").
'                     Replace("-", "").
'                     Trim()
'    End Function


'    Private Function GetNumberCellText(row As DataGridViewRow, ParamArray columnNames() As String) As String
'        Dim text As String = GetCellText(row, columnNames)
'        Dim d As Decimal

'        If Decimal.TryParse(text, d) Then
'            If d = 0D Then Return ""
'            Return d.ToString("N3")
'        End If

'        Return text
'    End Function


'    Private Function GetEntryStatusText() As String
'        If DataGridView1.Rows.Count = 0 Then Return ""

'        Dim depended As String = GetCellText(DataGridView1.Rows(0), "ACC_Depend_Status_CL", "ACC_Depend_Status")
'        If Not String.IsNullOrWhiteSpace(depended) Then Return depended

'        Dim isDepended As String = GetCellText(DataGridView1.Rows(0), "is_Depended_CL", "is_Depended")
'        If isDepended = "1" OrElse isDepended.ToLower() = "true" Then Return b_Depend_str

'        Return b_Depend_not_str
'    End Function


'    Private Sub Date__ValueChanged(sender As Object, e As EventArgs) Handles Date_.ValueChanged
'        Currency_Equal_txt.Text = Get_Equal(Currency_Cm.SelectedValue, Date_, 0)
'    End Sub



'    Private Sub T_ID_txt_2_KeyDown(sender As Object, e As KeyEventArgs) Handles T_ID_txt_2.KeyDown
'        If T_ID_txt_2.Text.Count > 0 Then If e.KeyCode = Keys.Return Then SELECT_Balance()
'    End Sub

'    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles UP_ToolStripBtn.Click
'        tmp_ID = T_ID_txt_2.Text
'        T_ID_txt_2.Text = T_ID_txt_2.Text + 1
'        SELECT_Balance()
'    End Sub

'    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles DOWN_ToolStripBtn.Click
'        tmp_ID = T_ID_txt_2.Text
'        If tmp_ID = 0 Then
'            Exit Sub
'        End If
'        T_ID_txt_2.Text = T_ID_txt_2.Text - 1
'        SELECT_Balance()
'    End Sub

'    Private Sub T_ID_txt_2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles T_ID_txt_2.KeyPress
'        Check_Only_Int(sender, e)
'    End Sub

'    Private Sub ACC_B_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
'        If e.KeyCode = Keys.F8 Then If SEARCH_ACC_BTN.Enabled = True Then MOVE_TO_ACCOUNTS_MENU()
'    End Sub


'    Private Function SELECT_First_Last(TYPE As String)


'        Dim C = New C
'        Try
'            Dim S As String = ""

'            If TYPE = "FIRST" Then
'                S = "SELECT TOP 1 T_ID FROM [ACC_BALANCE_MASTER] "
'            ElseIf TYPE = "LAST" Then
'                S = "SELECT TOP 1 T_ID FROM [ACC_BALANCE_MASTER] ORDER BY T_ID DESC "
'            End If


'            C.Com = New SqlClient.SqlCommand(S, C.Con)
'            C.Con.Open()
'            C.Dr = C.Com.ExecuteReader
'            If C.Dr.HasRows Then
'                C.Dr.Read()
'                Return C.Dr("T_ID")
'            Else
'                Dim notification3 As New NotificationForm("تنويه", " لا يوجد بيانات للعرض ", "bottom")
'                notification3.ShowNotification()
'                '  MsgBox("لا يوجد بيانات للعرض", MsgBoxStyle.Information, "")
'            End If
'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try


'        Return 0
'    End Function

'    Private Sub LAST_ToolStripBtn_Click(sender As Object, e As EventArgs) Handles LAST_ToolStripBtn.Click
'        tmp_ID = SELECT_First_Last("LAST")
'        If tmp_ID <> 0 Then
'            T_ID_txt_2.Text = tmp_ID
'            SELECT_Balance()
'        End If

'    End Sub

'    Private Sub First_ToolStripBtn_Click(sender As Object, e As EventArgs) Handles First_ToolStripBtn.Click
'        tmp_ID = SELECT_First_Last("FIRST")
'        If tmp_ID <> 0 Then
'            T_ID_txt_2.Text = tmp_ID
'            SELECT_Balance()
'        End If
'    End Sub

'    Private Sub reverse_Btn_Click(sender As Object, e As EventArgs) Handles reverse_Btn.Click
'        If MessageBox.Show(" ... سيتم توليد قيد عكسي للقيد من أجل تسويته  ", "تاكيــد العملية", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.OK Then
'            ACC_BALANCE_Insert_reverse(False)
'        End If
'    End Sub


'    Private Sub ACC_BALANCE_Insert_reverse(Depended As Boolean)
'        Dim C As New C

'        With C.Com
'            .Connection = C.Con
'            .CommandText = "[ACC_BALANCE_Insert_reverse]"
'            .CommandType = CommandType.StoredProcedure
'            .Parameters.AddWithValue("@B_T_ID", T_ID_txt_2.Text)
'            .Parameters.AddWithValue("@USER_ID", USER_ID)
'            .Parameters.AddWithValue("@OP_Status", 1)
'            .Parameters.AddWithValue("@B_T_ID_NEW", 0)
'            .Parameters.Add("@ERROR_MSG", SqlDbType.NVarChar, 500)
'            .Parameters("@OP_Status").Direction = ParameterDirection.Output
'            .Parameters("@ERROR_MSG").Direction = ParameterDirection.Output

'            SQL_SP_EXEC(C.Com)

'            If C.Com.Parameters("@OP_Status").Value.ToString() = "0" Then
'                Dim notification3 As New NotificationForm("خطأ فالتحرير", C.Com.Parameters("@ERROR_MSG").Value.ToString(), "bottom", True)
'                notification3.ShowNotification()
'            Else
'                If Depended = True Then
'                    Dim notification3 As New NotificationForm("إشعار", " تم إجراء عملية عكسية للقيــد " & T_ID_txt_2.Text, "bottom")
'                    notification3.ShowNotification()
'                Else
'                    Dim notification3 As New NotificationForm("إشعار", " تم عملية عكسية للقيــد " & T_ID_txt_2.Text, "bottom")
'                    notification3.ShowNotification()
'                    Enable_Fields(True)
'                End If
'                Clear_Fields()
'                SELECT_Balance()
'            End If


'        End With
'    End Sub

'    Private Sub Depend_Btn_EnabledChanged(sender As Object, e As EventArgs) Handles Depend_Btn.EnabledChanged
'        reverse_Btn.Enabled = Not Depend_Btn.Enabled
'    End Sub


'    Private Sub PRINT_ACC_B()
'        If DataGridView1.Rows.Count > 0 Then

'            If Total_B_txt.Text <> 0 Then
'                Dim notification3 As New NotificationForm("خطــأ فالإعتمــاد", " القيــد غير مــوزون ", "bottom", True)
'                notification3.ShowNotification()
'                '  MsgBox("القيــد غير مــوزون", MsgBoxStyle.Critical, "خطــأ فالطباعة")
'                Exit Sub
'            End If

'            If DataGridView1.Rows.Count > 0 Then
'                If DT(0)("is_Depended") = 0 Then

'                    If MessageBox.Show(" القيد غير معتمد ... هل تريد الإستمرار فالطباعة ", "تاكيــد العملية", MessageBoxButtons.OKCancel,
'                                       MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.Cancel Then
'                        Exit Sub
'                    End If

'                    'MsgBox("القيــد غير معتمد", MsgBoxStyle.Critical, "خطــأ فالطباعة")
'                    'Exit Sub
'                End If
'            End If

'            Print_B()

'        End If
'    End Sub

'    Private Sub إستخراجالتقريرExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إستخراجالتقريرExcelToolStripMenuItem.Click
'        Print_B(True)
'    End Sub

'    Private Sub Print_Btn_ButtonClick(sender As Object, e As EventArgs) Handles Print_Btn.ButtonClick
'        PRINT_ACC_B()
'    End Sub


'    Public Class ClosingEntryResult
'        Public Property Success As Boolean
'        Public Property EntryId As Integer
'        Public Property Message As String
'        Public Property JournalNo As String
'    End Class


'    Public Function GenerateClosingEntry(
'        ByVal ParentAccCode As String,
'        ByVal TargetAccCode As String,
'        ByVal ClosingDate As DateTime,
'        ByVal UserId As Integer,
'        ByVal ClosingType As String,
'        ByVal Notes As String,
'        Optional ByVal CurrencyId As Integer = 1,
'        Optional ByVal CurrencyEqual As Decimal = 1D,
'        Optional ByVal CostId As Integer = 1
'    ) As ClosingEntryResult

'        Dim result As New ClosingEntryResult()
'        Dim C As New C

'        Try

'            Using cmd As New SqlCommand("ACC_GenerateClosingEntry", C.Con)
'                cmd.CommandType = CommandType.StoredProcedure

'                cmd.Parameters.AddWithValue("@ParentAccCode", ParentAccCode)
'                cmd.Parameters.AddWithValue("@TargetAccCode", TargetAccCode)
'                cmd.Parameters.AddWithValue("@ClosingDate", ClosingDate)
'                cmd.Parameters.AddWithValue("@UserId", UserId)
'                cmd.Parameters.AddWithValue("@ClosingType", ClosingType)
'                cmd.Parameters.AddWithValue("@NotesMaster", Notes)
'                cmd.Parameters.AddWithValue("@CurrencyId", CurrencyId)
'                cmd.Parameters.AddWithValue("@CurrencyEqual", CurrencyEqual)
'                cmd.Parameters.AddWithValue("@CostId", CostId)
'                cmd.Parameters.AddWithValue("@YEAR", F_YEAR)


'                cmd.Parameters.AddWithValue("@B_T_ID", T_ID_txt_2.Text)

'                Dim p_Status As New SqlParameter("@OP_Status", SqlDbType.Int)
'                p_Status.Direction = ParameterDirection.Output
'                cmd.Parameters.Add(p_Status)

'                Dim p_Error As New SqlParameter("@ERROR_MSG", SqlDbType.NVarChar, 500)
'                p_Error.Direction = ParameterDirection.Output
'                cmd.Parameters.Add(p_Error)

'                Dim p_Journal As New SqlParameter("@NextNumber", SqlDbType.VarChar, 50)
'                p_Journal.Direction = ParameterDirection.Output
'                cmd.Parameters.Add(p_Journal)

'                If C.Con.State <> ConnectionState.Open Then
'                    C.Con.Open()
'                End If

'                cmd.ExecuteNonQuery()

'                Dim status As Integer = 0
'                Dim entryId As Integer = 0
'                Dim errorMsg As String = ""
'                Dim journalNo As String = ""

'                If Not IsDBNull(p_Status.Value) Then
'                    status = Convert.ToInt32(p_Status.Value)
'                End If


'                If p_Error.Value IsNot Nothing AndAlso Not IsDBNull(p_Error.Value) Then
'                    errorMsg = p_Error.Value.ToString()
'                End If

'                If p_Journal.Value IsNot Nothing AndAlso Not IsDBNull(p_Journal.Value) Then
'                    journalNo = p_Journal.Value.ToString()
'                End If

'                SELECT_Balance()

'                result.Success = (status = 1)
'                result.EntryId = entryId
'                result.JournalNo = journalNo

'                If status = 1 Then
'                    result.Message = "تم إنشاء القيد بنجاح"
'                Else
'                    result.Message = errorMsg
'                End If
'            End Using

'        Catch ex As Exception
'            result.Success = False
'            result.EntryId = 0
'            result.Message = ex.Message
'            result.JournalNo = ""
'        Finally
'            If C.Con.State = ConnectionState.Open Then
'                C.Con.Close()
'            End If
'        End Try

'        Return result
'    End Function


'    Private Sub قيدإقفالإيراداتToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles قيدإقفالإيراداتToolStripMenuItem.Click

'        Dim inp = InputBox("أدخل رقم الحساب اللرئيسي الخاص بالإيرادات", "فتح سنة")
'        If inp <> "" Then


'            Dim result As ClosingEntryResult = GenerateClosingEntry(
'    inp,
'    Pure_Income_ACC_CODE,
'    New DateTime(F_YEAR, 12, 31),
'    USER_ID,
'    "REVENUE",
'   " قيد إقفال الإيرادات لسنة  " & F_YEAR.ToString
')

'            If result.Success Then
'                MessageBox.Show("تم الإقفال بنجاح" & vbCrLf &
'                                "رقم القيد: " & result.EntryId & vbCrLf &
'                                "رقم اليومية: " & result.JournalNo)
'            Else
'                MessageBox.Show("خطأ: " & result.Message)
'            End If


'        End If

'    End Sub

'    Private Sub قيدإقفالمصروفاتToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles قيدإقفالمصروفاتToolStripMenuItem.Click

'        Dim inp = InputBox("أدخل رقم الحساب اللرئيسي الخاص بالمصروفات", "فتح سنة")
'        If inp <> "" Then


'            Dim result As ClosingEntryResult = GenerateClosingEntry(
'inp,
'Pure_Income_ACC_CODE,
'New DateTime(F_YEAR, 12, 31),
'USER_ID,
'"EXPENSE",
'" قيد إقفال المصروفات لسنة  " & F_YEAR.ToString
')
'            If result.Success Then
'                MessageBox.Show("تم الإقفال بنجاح" & vbCrLf &
'                                "رقم القيد: " & result.EntryId & vbCrLf &
'                                "رقم اليومية: " & result.JournalNo)
'            Else
'                MessageBox.Show("خطأ: " & result.Message)
'            End If

'        End If


'    End Sub
'End Class
