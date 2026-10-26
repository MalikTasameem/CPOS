Public Class ST_settlement : Inherits System.Windows.Forms.Form
    Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Public T_ID As Integer
    Public isDepended As Boolean
    Public isVoid As Boolean
    Public Receipts_DT As New DataTable

    Public isCashReceipt_Success As Boolean = False
    Public isShowingDetails As Boolean = False

    Public IM_ID As Integer = 0
    Dim IM_Dt As New DataTable
    Dim IM_QTY As Double = 0
    Dim IM_Cost As Double = 0
    Dim U_Cargo As Double = 1
    Public TOTAL As Double = 0
    '   Public AG_ID As Integer = 0
    Dim AG_Dt As New DataTable
    Dim U_Dt As New DataTable
    Dim Get_Unit As Boolean = False

    Public Bill_DT As New DataTable
    Dim ALL_QTY As Double = 0
    Dim U_ID As Integer
    Public On_Update As Boolean
    Public Barcode_IM As String = ""
    Public Pch_ID As Integer = 0
    Private pendingDeleteAudit As AuditOperationRequest
    Private pendingDeleteConnection As String
    Private deletingLine As Boolean
    Private ReadOnly pendingControlStates As New Dictionary(Of Control, Boolean)
    Private pendingHeaderAudit As AuditOperationRequest
    Private pendingHeaderConnection As String
    Private headerBusy As Boolean
    Private pendingHeaderResult As AuditRequestResult
    Private headerRetryButton As Button
    Private headerRetryText As String
    Private ReadOnly headerControlStates As New Dictionary(Of Control, Boolean)

    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        ' Writes must finish in FormClosing, where a failure can still cancel closing.
        FormType = 0
        Me.Dispose()
    End Sub

    Private Sub Expenses_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
        If e.KeyCode = Keys.F1 Then If New_butt.Enabled = True Then New_butt_Click(sender, e)
        If e.KeyCode = Keys.F12 Then If Save_butt.Enabled = True Then Save_butt_Click(sender, e)
        If e.KeyCode = Keys.F2 Then If Print_btn.Enabled = True Then Print_btn_Click(sender, e)
        If e.KeyCode = Keys.F3 Then If Edit_butt.Enabled = True And Edit_butt.Visible = True Then If Edit_butt.Text = EditState Then Edit_butt_Click(sender, e)
        If e.KeyCode = Keys.F4 Then If Delete_butt.Enabled = True And Delete_butt.Visible = True Then Delete_butt_Click(sender, e)
        'If e.KeyCode = Keys.F5 Then IM_SH_txt.Select()
        'If e.KeyCode = Keys.F8 Then Barcode_SH_txt.Select()
    End Sub

    'Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    '  If St_Count() = 1 Then All_St_Panel.Visible = False
    '    FormType = 4
    '    Check_View_Control()
    '    rs.FindAllControls(Me)
    '    Me.WindowState = FormWindowState.Maximized

    '    EditState = Edit_butt.Text
    '    DefaultFormState = Me.Text
    '    Disable_Fields()
    '    'Load_ST()

    '    Get_Last_T_ID()

    '    If isShowing_Trans = True Then
    '        Select_ExpBill(T_ID_Trans)
    '        SelectStateBt()
    '        New_butt.Enabled = False
    '        SearchButton.Enabled = False
    '    End If

    '    'If My_Settings.S_Default = 0 Then
    '    '    Barcode_SH_txt.Select()
    '    'Else
    '    '    IM_SH_txt.Select()
    '    'End If
    '    Make_Hints()
    'End Sub
    Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' =========================================================
            ' 🌟 1. تعيين التاجات للأدوات الأصلية الموجودة في الفورم فقط
            ' =========================================================
            If ExitFormButton IsNot Nothing Then ExitFormButton.Tag = "DELETE"
            'If Label16 IsNot Nothing Then Label16.Tag = "TITLE_TRANSPARENT" ' عنوان الفورم
            If DeletedBillLabel IsNot Nothing Then DeletedBillLabel.Tag = "TITLE_TRANSPARENT"

            ' حاويات التنظيم الأصلية الموجودة في كودك (لجعل خلفيتها تتبع الثيم)
            If Panel1 IsNot Nothing Then Panel1.Tag = "TRANSPARENT"
            If Panel2 IsNot Nothing Then Panel2.Tag = "TRANSPARENT"
            If Panel3 IsNot Nothing Then Panel3.Tag = "TRANSPARENT"
            If Panel9 IsNot Nothing Then Panel9.Tag = "TRANSPARENT"
            'If Panel13 IsNot Nothing Then Panel13.Tag = "TRANSPARENT"

            ' أزرار الإجراءات
            If New_butt IsNot Nothing Then New_butt.Tag = "GENERAL"
            If Save_butt IsNot Nothing Then Save_butt.Tag = "SAVE"
            If Edit_butt IsNot Nothing Then Edit_butt.Tag = "GENERAL"
            If Delete_butt IsNot Nothing Then Delete_butt.Tag = "DELETE"
            If Print_btn IsNot Nothing Then Print_btn.Tag = "PRINT"
            If SearchButton IsNot Nothing Then SearchButton.Tag = "GENERAL"

            ' أزرار البحث والتنقل
            If Up_Bill_btn IsNot Nothing Then Up_Bill_btn.Tag = "GENERAL"
            If Down_Bill_btn IsNot Nothing Then Down_Bill_btn.Tag = "GENERAL"
            If ClearSearch_btn IsNot Nothing Then ClearSearch_btn.Tag = "GENERAL"

            ' أزرار الأدوات الجانبية
            If ADDCatButton IsNot Nothing Then ADDCatButton.Tag = "GENERAL"
            If RemoveCatButton IsNot Nothing Then RemoveCatButton.Tag = "DELETE"
            If IM_btn IsNot Nothing Then IM_btn.Tag = "GENERAL"
            If MakeBarcode_btn IsNot Nothing Then MakeBarcode_btn.Tag = "GENERAL"
            If DGV_Control_btn IsNot Nothing Then DGV_Control_btn.Tag = "GENERAL"

            ' =========================================================
            ' 🌟 2. تطبيق الثيم
            ' =========================================================
            ThemeManager.ApplyThemeToForm(Me)

        Catch ex As Exception
        End Try

        ' =========================================================
        ' 🌟 3. الكود الأصلي للتحميل الخاص بك
        ' =========================================================
        '  If St_Count() = 1 Then All_St_Panel.Visible = False
        FormType = 14
        Check_View_Control()
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized

        EditState = Edit_butt.Text
        DefaultFormState = Me.Text
        Disable_Fields()
        'Load_ST()

        If isShowing_Trans = True Then
            LoadSettlement(T_ID_Trans)
            SelectStateBt()
            New_butt.Enabled = False
            SearchButton.Enabled = False
        Else
            Get_Last_T_ID()
        End If

        'If My_Settings.S_Default = 0 Then
        '    Barcode_SH_txt.Select()
        'Else
        '    IM_SH_txt.Select()
        'End If
        Make_Hints()
    End Sub


    Private Sub Make_Hints()
        SendMessage(Search_txt.Handle, &H1501, 0, "إبحث عن إسم صنف بالفاتورة")
        SendMessage(Barcode_Search_txt.Handle, &H1501, 0, "إبحث عن باركود صنف بالفاتورة")
    End Sub



    Public Sub Get_Last_T_ID()
        Try
            Dim id As Object
            Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr), cmd As New SqlClient.SqlCommand(
                "SELECT TOP 1 T_ID FROM dbo.Agents_Balance_MV WHERE User_ID=@User AND BsType_ID=39 AND isDepended=0 AND isVoid=0 AND T_ID BETWEEN @First AND @Last ORDER BY T_ID DESC", cn)
                cmd.Parameters.Add("@User", SqlDbType.Int).Value = USER_ID
                cmd.Parameters.Add("@First", SqlDbType.Int).Value = START_ID
                cmd.Parameters.Add("@Last", SqlDbType.Int).Value = END_ID
                cn.Open()
                id = cmd.ExecuteScalar()
            End Using
            ' Release the reader/connection before creation acquires the allocation lock.
            If id IsNot Nothing AndAlso Not IsDBNull(id) Then
                LoadSettlement(Convert.ToInt32(id))
            Else
                Call_New_Bill()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadSettlement(id As Integer, Optional connection As String = Nothing)
        Dim header As New DataTable(), lines As New DataTable()
        Using cn As New SqlClient.SqlConnection(If(connection, MY_Settings.SqlConStr))
            cn.Open()
            Using cmd As New SqlClient.SqlCommand("SELECT T_ID,Bill_ID,Receipt_Title,[Date],About,Cost,isDepended,isVoid,UserName FROM dbo.St_Settlement_Balance_MV_V WHERE T_ID=@Id", cn)
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id
                Using reader = cmd.ExecuteReader()
                    header.Load(reader)
                End Using
            End Using
            If header.Rows.Count <> 1 Then Throw New InvalidOperationException("لم يعثر على مستند التسوية المحدد.")
            Using cmd As New SqlClient.SqlCommand("dbo.ST_QTY_Deferences_Details_V_SELECT_Bill", cn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@Bill_T_ID", SqlDbType.Int).Value = id
                Using reader = cmd.ExecuteReader()
                    lines.Load(reader)
                End Using
            End Using
        End Using
        ' Apply only a fully loaded header and detail set to this actual form instance.
        Dim row = header.Rows(0)
        Dim documentDate = Convert.ToDateTime(row("Date"))
        T_ID = id
        Pch_ID = Convert.ToInt32(row("Bill_ID"))
        Bill_ID_Txt.Text = S_Sub_Code & Pch_ID.ToString()
        Title_txt.Text = Convert.ToString(row("Receipt_Title"))
        Notes_txt.Text = Convert.ToString(row("About"))
        DateTimeEx.Value = documentDate
        TOTAL = If(IsDBNull(row("Cost")), 0R, Convert.ToDouble(row("Cost")))
        Switch_Dependcy(Not IsDBNull(row("isDepended")) AndAlso Convert.ToBoolean(row("isDepended")))
        isVoid = Not IsDBNull(row("isVoid")) AndAlso Convert.ToBoolean(row("isVoid"))
        DeletedBillLabel.Visible = isVoid
        User_Name_lb.Text = Convert.ToString(row("UserName")) & " - " & documentDate.ToString()
        DisplaySettlementLines(lines)
        SelectStateBt()
    End Sub

    ' True means the V2 path handled the attempt (including failure); never fall back after a failed write.
    Private Function TryAuditHeader(action As String) As Boolean
        If headerBusy Then Return True
        Try
            If pendingHeaderAudit Is Nothing Then
                If Not AuditSettlementLines.IsEnabled(MY_Settings.SqlConStr) Then Return False
                pendingHeaderConnection = MY_Settings.SqlConStr
                pendingHeaderAudit = AuditSettlementHeader.Request(
                    AuditSecuritySession.Current.ContextFor(pendingHeaderConnection, USER_ID, "ST_settlement"), action,
                    If(action = "Created", 0, Me.T_ID), If(action = "Created", Date.Now, DateTimeEx.Value),
                    If(action = "Created", "", Title_txt.Text), If(action = "Created", "", Notes_txt.Text), IM_min_QTY)
                Select Case action
                    Case "Created" : headerRetryButton = New_butt
                    Case "Approved", "HeaderSaved" : headerRetryButton = Save_butt
                    Case "Voided" : headerRetryButton = Delete_butt
                    Case Else : headerRetryButton = Edit_butt
                End Select
                headerRetryText = headerRetryButton.Text
                headerControlStates.Add(headerRetryButton, headerRetryButton.Enabled)
                headerRetryButton.Enabled = True
                headerRetryButton.Text = "إعادة المحاولة"
            End If
            headerBusy = True
            FreezeHeaderControls(Me)
            If pendingHeaderResult Is Nothing Then pendingHeaderResult = AuditSettlementHeader.Save(pendingHeaderConnection, pendingHeaderAudit)
            CompleteHeaderDisplay(pendingHeaderAudit.OperationCode, pendingHeaderResult)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            If pendingHeaderAudit IsNot Nothing AndAlso pendingHeaderResult Is Nothing Then
                Try
                    If AuditRequestCoordinator.Status(pendingHeaderConnection, pendingHeaderAudit).Status = "NotFound" Then
                        pendingHeaderAudit = Nothing
                        RestoreHeaderControls()
                    End If
                Catch
                    ' Unknown result: retain the exact immutable request and freeze all other writes.
                End Try
            End If
        Finally
            headerBusy = False
        End Try
        Return True
    End Function

    Private Sub FreezeHeaderControls(parent As Control)
        For Each child As Control In parent.Controls
            If child Is headerRetryButton OrElse child Is ExitFormButton Then Continue For
            If child.Contains(headerRetryButton) OrElse child.Contains(ExitFormButton) Then
                FreezeHeaderControls(child)
            Else
                If Not headerControlStates.ContainsKey(child) Then headerControlStates.Add(child, child.Enabled)
                child.Enabled = False
            End If
        Next
    End Sub

    Private Sub RestoreHeaderControls()
        For Each entry In headerControlStates
            If Not entry.Key.IsDisposed Then entry.Key.Enabled = entry.Value
        Next
        headerControlStates.Clear()
        If headerRetryButton IsNot Nothing Then headerRetryButton.Text = headerRetryText
        headerRetryButton = Nothing
        pendingHeaderConnection = Nothing
    End Sub

    Private Sub CompleteHeaderDisplay(operationCode As String, result As AuditRequestResult)
        pendingHeaderResult = result
        Dim id = AuditSettlementHeader.ResultId(result)
        Dim keepEditing = operationCode = "Settlement.EditOpened" OrElse (operationCode = "Settlement.HeaderSaved" AndAlso On_Update)
        Try
            LoadSettlement(id, pendingHeaderConnection)
        Catch ex As Exception
            ' This is a display failure after confirmed commit, NOT a reason to execute the command again.
            If headerRetryButton IsNot Nothing Then headerRetryButton.Text = "تحديث العرض"
            FreezeHeaderControls(Me)
            MsgBox("تم حفظ العملية وتأكدت نتيجتها، لكن تعذر تحديث العرض. اضغط تحديث العرض دون تكرار العملية." & Environment.NewLine & ex.Message, MsgBoxStyle.Exclamation)
            Return
        End Try
        pendingHeaderAudit = Nothing
        pendingHeaderResult = Nothing
        RestoreHeaderControls()
        On_Update = keepEditing
        SelectStateBt()
        Edit_butt.BackColor = Color.WhiteSmoke
        If On_Update Then
            Enable_Fields()
            AGMetroGrid.Enabled = True
            AGMetroGrid.BackgroundColor = Color.LightYellow
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
            Edit_butt.Text = "إيقاف التعديل"
            Edit_butt.BackColor = Color.GreenYellow
        End If
        If operationCode = "Settlement.Voided" Then MsgBox("تم إلغاء التسوية واستبعاد أثرها من المخزون.", MsgBoxStyle.Information)
    End Sub

    Public Sub Check_View_Control()
        AGMetroGrid.Columns("ST_Name_CL").Visible = MY_Settings.S_ST_Name_CL
        AGMetroGrid.Columns("D_Valid_CL").Visible = MY_Settings.S_D_Valid_CL
        AGMetroGrid.Columns("IMUnit_CL").Visible = MY_Settings.S_IMUnit_CL
        AGMetroGrid.Columns("Price_CL").Visible = MY_Settings.S_Price_CL
        AGMetroGrid.Columns("Total_CL").Visible = MY_Settings.S_Total_CL
        '  AGMetroGrid.Columns("Notes_CL").Visible = MY_Settings.SP_Notes_CL
        AGMetroGrid.Columns("IMNUM_CL").Visible = MY_Settings.S_IMNUM_CL
        AGMetroGrid.Columns("Barcode_CL").Visible = MY_Settings.S_Barcode_CL

        'Min_SP_Panel.Visible = S_Allow_MinSP
        'Min_SP_Panel_2.Visible = S_Allow_MinSP
    End Sub

    Public Sub SELECT_MAX()
        Dim c As New C
        Try
            Dim s As String
            s = "SELECT ISNULL(MAX(ST_Sett_ID),0) + 1 AS N FROM Agents_Balance_MV "
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                Bill_ID_Txt.Text = c.Dr("N")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Public Sub Load_ST()
    '    Dim c As New C
    '    Try
    '        Dim s As String
    '        s = "select ST_ID,ST_name from STORES ORDER By ST_ID ASC"
    '        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
    '        c.Da.Fill(c.Dt)
    '        ST_cm.DataSource = c.Dt
    '        ST_cm.DisplayMember = "ST_name"
    '        ST_cm.ValueMember = "ST_ID"
    '        ST_cm.SelectedValue = PCH_ST_ID
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub Enable_Fields()
        Title_txt.Enabled = True
        DateTimeEx.Enabled = True
        Notes_txt.Enabled = True
        'Invoice_Type_cm.Enabled = True
        Ebable_CatFields()
    End Sub

    Private Sub Disable_Fields()
        Title_txt.Enabled = False
        DateTimeEx.Enabled = False
        Notes_txt.Enabled = False
        'Invoice_Type_cm.Enabled = False
        Disable_CatFields()
    End Sub

    Private Sub Disable_CatFields()
        'IM_SH_txt.Enabled = False
        'Show_IM_btn.Enabled = False
        'Barcode_SH_txt.Enabled = False
        'QtyTextBox.Enabled = False
        'PriceTextBox.Enabled = False
        ADDCatButton.Enabled = False
        RemoveCatButton.Enabled = False
    End Sub

    Private Sub Ebable_CatFields()
        'IM_SH_txt.Enabled = True
        'Show_IM_btn.Enabled = True
        'Barcode_SH_txt.Enabled = True
        'QtyTextBox.Enabled = True
        'PriceTextBox.Enabled = True
        ADDCatButton.Enabled = True
        RemoveCatButton.Enabled = True
    End Sub


    Public Sub Switch_Dependcy(F As Boolean)

        If F = True Then
            isDepended = 1

            AGMetroGrid.BackgroundColor = Color.LightGreen
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
            Save_butt.Enabled = False
        Else
            isDepended = 0
            AGMetroGrid.BackgroundColor = Color.LightYellow
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
            Save_butt.Enabled = True
        End If

    End Sub

    Private Sub NewStateBt()
        Enable_Fields()
        Save_butt.Enabled = True
        Edit_butt.Enabled = False
        Delete_butt.Enabled = False
        Me.Text = "فاتورة جرد جديدة"
        'If My_Settings.S_Default = 0 Then
        '    Barcode_SH_txt.Select()
        'Else
        '    IM_SH_txt.Select()
        'End If
    End Sub
    'Private Sub DeleteOrUpdateStateBt()
    '    Disable_Fields()
    '    Save_butt.Enabled = False
    '    Edit_butt.Enabled = True
    '    Delete_butt.Enabled = False
    '    Me.Text = DefaultFormState
    'End Sub

    Private Sub SavedStateBt()
        Disable_Fields()
        Save_butt.Enabled = False
        Edit_butt.Enabled = False
        Delete_butt.Enabled = False
        Me.Text = DefaultFormState
    End Sub

    Public Sub SelectStateBt()

        If isVoid = True Then
            DeletedBillLabel.Visible = True
            Save_butt.Enabled = False
            Edit_butt.Enabled = False
            Edit_butt.Text = EditState
            Delete_butt.Enabled = False
            AGMetroGrid.Enabled = True
            AGMetroGrid.BackgroundColor = Color.IndianRed
            AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.IndianRed
            Print_btn.Enabled = False
            Disable_Fields()
        Else
            If isDepended = False Then
                Save_butt.Enabled = True
                Edit_butt.Enabled = False
                Print_btn.Enabled = False
                ' AGMetroGrid.BackgroundColor = Color.Gray
                ' AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.Gray
                Delete_butt.Enabled = False
                Enable_Fields()
            Else
                Save_butt.Enabled = False
                Edit_butt.Enabled = True
                Print_btn.Enabled = True
                Delete_butt.Enabled = True
                DeletedBillLabel.Visible = False
                Edit_butt.Text = EditState
                Disable_Fields()
                AGMetroGrid.BackgroundColor = Color.LightGreen
                AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightGreen
            End If

        End If
        Me.Text = "عرض بيانات فاتورة"

    End Sub

    Private Sub ClearFields()
        isCashReceipt_Success = False
        T_ID = 0
        Title_txt.Clear()
        Notes_txt.Clear()
        ' PriceTextBox.Clear()
        lblQtyPlus.Clear()
        Bill_DT.Clear()
        Receipts_DT.Clear()
        DateTimeEx.Text = Date.Now
        Edit_butt.Text = EditState
        DeletedBillLabel.Visible = False
        isVoid = False
        '  ClearCatFields()
        Me.Text = FormState
        User_Name_lb.Text = "---"
        'Invoice_Type_cm.SelectedIndex = 0
        On_Update = False
        Edit_butt.BackColor = Color.WhiteSmoke
    End Sub

    Private Sub New_butt_Click(sender As Object, e As EventArgs) Handles New_butt.Click
        Call_New_Bill()
    End Sub

    Private Sub Call_New_Bill()
        If pendingHeaderAudit IsNot Nothing Then
            TryAuditHeader("Created")
            Return
        End If
        If On_Update Then
            MsgBox("أنه تعديل المستند الحالي قبل فتح تسوية جديدة.", MsgBoxStyle.Exclamation)
            Return
        End If
        If T_ID > 0 Then
            If MessageBox.Show("فتح فاتورة جرد جديدة", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information,
                               MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                If TryAuditHeader("Created") Then Return
                ClearFields()
                Insert_NewBill()
                NewStateBt()
            End If

        Else
            If TryAuditHeader("Created") Then Return
            ClearFields()
            Insert_NewBill()
            NewStateBt()
        End If
    End Sub

    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        If pendingHeaderAudit IsNot Nothing Then
            TryAuditHeader("Approved")
            Return
        End If
        If AGMetroGrid.Rows.Count > 0 Then
            Beep()
            If MessageBox.Show("حفظ فاتورة الجرد ؟", "تنويه", MessageBoxButtons.OKCancel,
                               MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.OK Then
                'AG_Balance_Update_Invoice_Type()
                If TryAuditHeader("Approved") Then Return
                Update_Total()
                DependingBill(T_ID)
                Save_Date(T_ID, DateTimeEx)
                Save_Title_Name(T_ID, Title_txt.Text)
                Save_About(T_ID, Notes_txt.Text)
                Switch_Dependcy(1)
                SelectStateBt()
            End If
        End If
    End Sub

    'Private Sub AG_Balance_Update_Invoice_Type()
    '    Dim sqlComm As New SqlClient.SqlCommand
    '    sqlComm.CommandText = "AG_Balance_Update_Invoice_Type"
    '    sqlComm.CommandType = CommandType.StoredProcedure
    '    sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
    '    sqlComm.Parameters.AddWithValue("@Type", Invoice_Type_cm.SelectedIndex)
    '    SQL_SP_EXEC(sqlComm)
    'End Sub

    'Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
    '    If Edit_butt.Text = EditState Then
    '        Edit_butt.Text = "ح التعديل"
    '        Enable_Fields()
    '        AGMetroGrid.BackgroundColor = Color.LightYellow
    '        AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
    '        If My_Settings.S_Default = 0 Then
    '            Barcode_SH_txt.Select()
    '        Else
    '            IM_SH_txt.Select()
    '        End If
    '    Else
    '        Save_About(T_ID, Notes_txt.Text)
    '        Save_Date(T_ID, DateTimeEx)
    '        Edit_butt.Text = EditState
    '        Disable_Fields()
    '        SelectStateBt()
    '    End If

    'End Sub

    Private Sub Delete_butt_Click(sender As Object, e As EventArgs) Handles Delete_butt.Click
        If pendingHeaderAudit IsNot Nothing Then
            TryAuditHeader("Voided")
            Return
        End If
        If Not U_Cancel_Pch Then
            MsgBox("أنت غير مخول بإلغاء التسوية.", MsgBoxStyle.Exclamation)
            Return
        End If

        If AGMetroGrid.Rows.Count > 0 Then
            If IM_min_QTY = False Then
                If IM_Check_Neg_QTY_For_Cancel_Pch() = 1 Then
                    'MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
                    MsgBox(" لا يمكن سحب كمية بالسالب للصنف  " & Str_Name, MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            End If
        End If

        Beep()
        If MessageBox.Show(" سيتم إلغاء الفاتورة رقم " + Bill_ID_Txt.Text + " وكل المعاملات الخاصة بها ... متأكد ", "إلغــاء فاتورة", MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.OK Then
            Cancel_Bill()
        End If

    End Sub

    Private Function IM_Check_Neg_QTY_For_Cancel_Pch()
        Dim C As New C
        Dim F As Integer = 1
        With C.Com
            .Connection = C.Con
            .CommandText = "ST_Settlement_CanRemove"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@Bill_T_ID", T_ID)
            .Parameters.Add("@Str_Name", SqlDbType.NVarChar, 1500)
            .Parameters("@F").Direction = ParameterDirection.Output
            .Parameters("@Str_Name").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                F = .Parameters("@F").Value
                Str_Name = .Parameters("@Str_Name").Value
            End If
        End With
        Return F
    End Function

    Private Sub Cancel_Bill()
        If TryAuditHeader("Voided") Then Return

        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "ST_Settlement_Header_Apply"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", T_ID)
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.InputOutput
        sqlComm.Parameters.AddWithValue("@Action", "Voided")
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters.AddWithValue("@AllowNegative", IM_min_QTY)

        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تم إلغاء الفاتورة", MsgBoxStyle.Information)
            Network_Edit_Tracker_insert("إلغاء مستند تسوية المخزون؛ T_ID=" & T_ID.ToString(), Bill_ID_Txt.Text, 39, 3)
            isVoid = True
            SelectStateBt()
        End If

    End Sub

    Private Sub TreasuryCard_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub Tr_Name_txtb_Enter(sender As Object, e As EventArgs)
        Arabic_Lang()
    End Sub

    Private Sub Tr_BankNum_TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles lblQtyPlus.KeyPress
        Check_Only_Int(sender, e)
    End Sub


    'Private Sub AGMetroGrid_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles AGMetroGrid.CellEndEdit
    '    Update_Cat()
    'End Sub

    Private Sub AGMetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles AGMetroGrid.RowsAdded
        'Calc_Total()
    End Sub

    Private Sub AGMetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles AGMetroGrid.RowsRemoved
        'Calc_Total()
    End Sub

    Private Sub Calc_Total()

        Dim TotalQtyPlus As Decimal = 0D
        Dim TotalQtyMinus As Decimal = 0D

        Dim TotalCostPlus As Decimal = 0D
        Dim TotalCostMinus As Decimal = 0D

        For Each row As DataGridViewRow In AGMetroGrid.Rows

            If row.IsNewRow Then Continue For

            Dim qty As Decimal = 0D
            Dim cost As Decimal = 0D

            Decimal.TryParse(row.Cells("QTY_CL").Value?.ToString(), qty)
            Decimal.TryParse(row.Cells("Price_CL").Value?.ToString(), cost)

            If qty > 0 Then
                TotalQtyPlus += qty
                TotalCostPlus += qty * cost

            ElseIf qty < 0 Then
                TotalQtyMinus += qty
                TotalCostMinus += qty * cost
            End If

        Next

        Dim NetQty As Decimal = TotalQtyPlus + TotalQtyMinus
        Dim NetTotal As Decimal = TotalCostPlus + TotalCostMinus

        lblQtyPlus.Text = TotalQtyPlus.ToString("N3")
        lblQtyMinus.Text = TotalQtyMinus.ToString("N3")

        lblCostPlus.Text = TotalCostPlus.ToString("N3")
        lblCostMinus.Text = TotalCostMinus.ToString("N3")

        lblNetQty.Text = NetQty.ToString("N3")
        lblNetTotal.Text = NetTotal.ToString("N3")


        Select Case NetQty
            Case < 0 : lblNetQty.ForeColor = Color.DarkRed
            Case > 0 : lblNetQty.ForeColor = Color.DarkGreen
            Case 0 : lblNetQty.ForeColor = Color.Black
        End Select

        Select Case NetTotal
            Case < 0 : lblNetTotal.ForeColor = Color.DarkRed
            Case > 0 : lblNetTotal.ForeColor = Color.DarkGreen
            Case 0 : lblNetTotal.ForeColor = Color.Black
        End Select



        'TOTAL = 0
        'Dim QTY As Double = 0
        'For i = 0 To AGMetroGrid.Rows.Count - 1
        '    TOTAL = TOTAL + AGMetroGrid.Rows(i).Cells("Total_CL").Value
        '    QTY += AGMetroGrid.Rows(i).Cells("QTY_CL").Value
        'Next
        'IM_Count_LB.Text = AGMetroGrid.Rows.Count.ToString + " : مواد "
        'IM_Qty_LB.Text = QTY.ToString + " : كميات "
    End Sub

    Private Sub ADDCatButton_Click(sender As Object, e As EventArgs) Handles ADDCatButton.Click

        F_ST_settlement_IM_card = New ST_settlement_IM_card
        F_ST_settlement_IM_card.T_ID = T_ID
        If F_ST_settlement_IM_card.ShowDialog(Me) = DialogResult.OK Then
            Try
                Pch_Contents_SELECT_Bill()
            Catch ex As Exception
                MsgBox("تم تنفيذ العملية، لكن تعذر تحديث عرض السطور. لا تكرر الإضافة قبل تحديث العرض." & Environment.NewLine & ex.Message, MsgBoxStyle.Exclamation)
            End Try
        End If

    End Sub


    Private Sub Update_Total()
        If String.IsNullOrWhiteSpace(lblQtyPlus.Text) = False Then Save_Total(T_ID, TOTAL, 0)
    End Sub


    Private Sub Insert_NewBill()

        Dim sqlComm As New SqlClient.SqlCommand()

        sqlComm.CommandText = "Agents_BalanceMV_insert"
        sqlComm.CommandType = CommandType.StoredProcedure

        sqlComm.Parameters.AddWithValue("@T_ID", 0)
        sqlComm.Parameters.AddWithValue("@Pch_ID", 0)
        sqlComm.Parameters.AddWithValue("@IMEX_ID", 0)
        sqlComm.Parameters.AddWithValue("@Jrd_ID", 0)
        sqlComm.Parameters.AddWithValue("@SRtn_ID", 0)
        sqlComm.Parameters.AddWithValue("@PRtn_ID", 0)
        sqlComm.Parameters.AddWithValue("@Receipt_Num", 0)
        sqlComm.Parameters.AddWithValue("@ST_Tran_ID", 0)
        sqlComm.Parameters.AddWithValue("@EXP_ID", 0)
        sqlComm.Parameters.AddWithValue("@Frm_ID", 0)
        sqlComm.Parameters.AddWithValue("@ViewSB_ID", 0)
        sqlComm.Parameters.AddWithValue("@InSale_ID", 0)
        sqlComm.Parameters.AddWithValue("@Outsale_ID", 0)
        sqlComm.Parameters.AddWithValue("@Frm_ID_M", 0)
        sqlComm.Parameters.AddWithValue("@ST_Sett_ID", 0)
        sqlComm.Parameters.AddWithValue("@AG_ID", 1)
        sqlComm.Parameters.AddWithValue("@Date", Me.DateTimeEx.Value)
        sqlComm.Parameters.AddWithValue("@BsType_ID", 39)
        sqlComm.Parameters.AddWithValue("@User_ID", USER_ID)
        sqlComm.Parameters("@ST_Sett_ID").Direction = ParameterDirection.Output
        sqlComm.Parameters("@T_ID").Direction = ParameterDirection.Output

        If SQL_SP_EXEC(sqlComm) = True Then

            T_ID = sqlComm.Parameters("@T_ID").Value.ToString()

            LoadSettlement(T_ID)
        End If

    End Sub


    Public Sub Pch_Contents_SELECT_Bill()
        Dim lines As New DataTable()
        Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr), cmd As New SqlClient.SqlCommand("dbo.ST_QTY_Deferences_Details_V_SELECT_Bill", cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@Bill_T_ID", SqlDbType.Int).Value = Me.T_ID
            Using adapter As New SqlClient.SqlDataAdapter(cmd)
                adapter.Fill(lines)
            End Using
        End Using
        DisplaySettlementLines(lines)
    End Sub

    Private Sub DisplaySettlementLines(lines As DataTable)
        Bill_DT = lines
        AGMetroGrid.DataSource = Bill_DT
        If AGMetroGrid.Rows.Count > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(AGMetroGrid.Rows.Count - 1).Cells("EX_Name_CL")
        Calc_Total()
    End Sub


    Private Sub Delete_Cat()
        If pendingDeleteAudit IsNot Nothing OrElse AuditSettlementLines.IsEnabled(MY_Settings.SqlConStr) Then
            If pendingDeleteAudit Is Nothing Then
                If AGMetroGrid.CurrentRow Is Nothing OrElse Not AGMetroGrid.Columns.Contains("T_ID_CL") Then Throw New InvalidOperationException("اختر سطر التسوية أولًا.")
                pendingDeleteConnection = MY_Settings.SqlConStr
                pendingDeleteAudit = AuditSettlementLines.DeleteRequest(AuditSecuritySession.Current.ContextFor(pendingDeleteConnection, USER_ID, "ST_settlement"),
                    Me.T_ID, Convert.ToInt32(AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value), IM_min_QTY)
            End If
            FreezePendingDelete(Me)
            Dim result = AuditSettlementLines.Save(pendingDeleteConnection, pendingDeleteAudit)
            pendingDeleteAudit = Nothing
            RestorePendingDelete()
            Try
                Pch_Contents_SELECT_Bill()
            Catch ex As Exception
                MsgBox("تأكدت نتيجة طلب الحذف، لكن تعذر تحديث عرض السطور. حدّث العرض قبل أي عملية أخرى." & Environment.NewLine & ex.Message, MsgBoxStyle.Exclamation)
                Return
            End Try
            If result.ResultCode.GetValueOrDefault() = 5 Then MsgBox("السطر غير موجود؛ لم تسجل عملية حذف وهمية.", MsgBoxStyle.Information)
            Return
        End If
        Dim Row_Index As Integer = AGMetroGrid.CurrentCell.RowIndex
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "[ST_QTY_Deferences_Details_Delete]"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@T_ID", AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
        sqlComm.Parameters.AddWithValue("@ExpectedBillId", Me.T_ID)
        sqlComm.Parameters.AddWithValue("@AllowNegative", IM_min_QTY)
        If SQL_SP_EXEC(sqlComm) = True Then
            Network_Edit_Tracker_insert(" الصنف:" + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value.ToString + " كمية التسوية:" + AGMetroGrid.CurrentRow.Cells("QTY_CL").Value.ToString, Bill_ID_Txt.Text, 39, 2)

            Pch_Contents_SELECT_Bill()
            'Update_Total()

            'If On_Update = True Then If Not IsDBNull(AGMetroGrid.CurrentRow.Cells("NewSale_CL").Value) Then MsgBox("قم بتعديل سعر البيع من شاشة الأصناف", MsgBoxStyle.Information, "")
            If Row_Index > 0 AndAlso AGMetroGrid.Rows.Count > 0 Then AGMetroGrid.CurrentCell = AGMetroGrid.Rows(Math.Min(Row_Index - 1, AGMetroGrid.Rows.Count - 1)).Cells("EX_Name_CL")

        End If

    End Sub


    Private Sub RemoveCatButton_Click(sender As Object, e As EventArgs) Handles RemoveCatButton.Click
        If deletingLine Then Return
        Try
            deletingLine = True
            If pendingDeleteAudit IsNot Nothing Then
                Delete_Cat()
                Return
            End If
            If AGMetroGrid.CurrentRow IsNot Nothing AndAlso Not AGMetroGrid.CurrentRow.IsNewRow Then
                If MessageBox.Show(" حذف الصنف " + AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                    If IM_min_QTY = False Then
                        If IM_Check_Neg_QTY_For_Update_Pch() = 1 Then
                            MsgBox("في حالة خذف الصنف ستصبح كمية المخزون سالبة", MsgBoxStyle.Critical, "خطأ")
                            Exit Sub
                        End If
                    End If
                    Delete_Cat()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            If pendingDeleteAudit IsNot Nothing Then
                Try
                    If AuditRequestCoordinator.Status(pendingDeleteConnection, pendingDeleteAudit).Status = "NotFound" Then
                        pendingDeleteAudit = Nothing
                        RestorePendingDelete()
                    End If
                Catch
                End Try
            End If
        Finally
            deletingLine = False
        End Try
    End Sub

    Private Sub FreezePendingDelete(parent As Control)
        For Each child As Control In parent.Controls
            If child Is RemoveCatButton OrElse child Is ExitFormButton Then Continue For
            If child.Contains(RemoveCatButton) OrElse child.Contains(ExitFormButton) Then
                FreezePendingDelete(child)
            Else
                If Not pendingControlStates.ContainsKey(child) Then pendingControlStates.Add(child, child.Enabled)
                child.Enabled = False
            End If
        Next
    End Sub

    Private Sub RestorePendingDelete()
        For Each entry In pendingControlStates
            If Not entry.Key.IsDisposed Then entry.Key.Enabled = entry.Value
        Next
        pendingControlStates.Clear()
    End Sub

    Private Sub Settlement_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If headerBusy OrElse deletingLine Then
            e.Cancel = True
            Return
        End If
        If pendingHeaderAudit IsNot Nothing Then
            Try
                Dim status = If(pendingHeaderResult, AuditRequestCoordinator.Status(pendingHeaderConnection, pendingHeaderAudit))
                If status.Status = "Completed" Then
                    CompleteHeaderDisplay(pendingHeaderAudit.OperationCode, status)
                ElseIf status.Status = "NotFound" Then
                    pendingHeaderAudit = Nothing
                    pendingHeaderResult = Nothing
                    RestoreHeaderControls()
                End If
            Catch
            End Try
            If pendingHeaderAudit IsNot Nothing Then
                e.Cancel = True
                MsgBox(If(pendingHeaderResult Is Nothing, "نتيجة عملية المستند غير محسومة. استخدم زر إعادة المحاولة؛ المرجع: ", "العملية محفوظة، لكن تحديث العرض لم يكتمل. استخدم زر تحديث العرض؛ المرجع: ") & pendingHeaderAudit.RequestId.ToString("D"), MsgBoxStyle.Exclamation)
                Return
            End If
        End If
        If pendingDeleteAudit Is Nothing Then
            If On_Update Then
                Edit_butt_Click(sender, e)
                If On_Update Then e.Cancel = True
            End If
            Return
        End If
        Try
            Dim status = AuditRequestCoordinator.Status(pendingDeleteConnection, pendingDeleteAudit)
            If status.Status = "Completed" OrElse status.Status = "NotFound" Then
                pendingDeleteAudit = Nothing
                RestorePendingDelete()
                If On_Update Then
                    Edit_butt_Click(sender, e)
                    If On_Update Then e.Cancel = True
                End If
                Return
            End If
        Catch
        End Try
        e.Cancel = True
        MsgBox("نتيجة الحذف غير محسومة. أعد المحاولة بزر حذف الصنف. مرجع الطلب: " & pendingDeleteAudit.RequestId.ToString("D"), MsgBoxStyle.Exclamation)
    End Sub

    Private Function IM_Check_Neg_QTY_For_Update_Pch()
        Dim C As New C
        Dim F As Integer = 1
        With C.Com
            .Connection = C.Con
            .CommandText = "ST_Settlement_CanRemove"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@Bill_T_ID", Me.T_ID)
            .Parameters.AddWithValue("@LineId", AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value)
            .Parameters.Add("@Str_Name", SqlDbType.NVarChar, 1500).Direction = ParameterDirection.Output

            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then
                F = .Parameters("@F").Value
            End If
        End With

        Return F
    End Function


    Private Sub DateTimeEx_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimeEx.KeyDown
        If e.KeyCode = Keys.Return Then
            If TryAuditHeader("HeaderSaved") Then Return
            Save_Date(T_ID, DateTimeEx)
        End If
    End Sub


    Private Sub SerachButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        If On_Update Then
            MsgBox("أنه تعديل المستند قبل البحث عن مستند آخر.", MsgBoxStyle.Exclamation)
            Return
        End If
        Me.Cursor = Cursors.AppStarting
        FormType = 14
        Dim previous = F_ST_settlement
        Try
            F_ST_settlement = Me
            PchSearch.ShowDialog(Me)
        Finally
            F_ST_settlement = previous
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub AGMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles AGMetroGrid.MouseDoubleClick
        'FormType = 14
        'If AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow And AGMetroGrid.Rows.Count > 0 Then Change_IM_Details.ShowDialog()
    End Sub

    Private Sub MakeBarcode_btn_Click(sender As Object, e As EventArgs) Handles MakeBarcode_btn.Click
        printbarcode.Auto_Print = True
        printbarcode.ShowDialog()
        printbarcode.Auto_Print = False
    End Sub

    Dim Tmp_Bill_ID As Integer
    Private Sub Down_Bill_btn_Click(sender As Object, e As EventArgs) Handles Down_Bill_btn.Click
        If On_Update OrElse pendingHeaderAudit IsNot Nothing OrElse pendingDeleteAudit IsNot Nothing Then Return
        Tmp_Bill_ID = Pch_ID
        Bill_ID_Txt.Text = Pch_ID - 1
        Get_T_ID()
    End Sub

    Public Sub Get_T_ID()
        If On_Update OrElse pendingHeaderAudit IsNot Nothing OrElse pendingDeleteAudit IsNot Nothing Then
            Bill_ID_Txt.Text = S_Sub_Code & Pch_ID.ToString()
            MsgBox("أكمل العملية الحالية قبل الانتقال إلى مستند آخر.", MsgBoxStyle.Exclamation)
            Return
        End If
        Try
            Dim number As Integer
            If Not Integer.TryParse(Bill_ID_Txt.Text, number) Then Throw New InvalidOperationException("رقم التسوية غير صالح.")
            Dim id As Object
            Using cn As New SqlClient.SqlConnection(MY_Settings.SqlConStr), cmd As New SqlClient.SqlCommand(
                "SELECT T_ID FROM dbo.Agents_Balance_MV WHERE ST_Sett_ID=@Number AND BsType_ID=39", cn)
                cmd.Parameters.Add("@Number", SqlDbType.Int).Value = number
                cn.Open()
                id = cmd.ExecuteScalar()
            End Using
            If id IsNot Nothing AndAlso Not IsDBNull(id) Then
                LoadSettlement(Convert.ToInt32(id))
            Else
                MsgBox("لم يتم التعرف على الفاتورة", MsgBoxStyle.Exclamation)
                Bill_ID_Txt.Text = S_Sub_Code & Pch_ID.ToString()
            End If
        Catch ex As Exception
            Bill_ID_Txt.Text = S_Sub_Code & Pch_ID.ToString()
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Up_Bill_btn_Click(sender As Object, e As EventArgs) Handles Up_Bill_btn.Click
        If On_Update OrElse pendingHeaderAudit IsNot Nothing OrElse pendingDeleteAudit IsNot Nothing Then Return
        If Not String.IsNullOrWhiteSpace(Bill_ID_Txt.Text) Then
            Tmp_Bill_ID = Pch_ID
            Bill_ID_Txt.Text = Pch_ID + 1
            Get_T_ID()
        End If
    End Sub


    Private Sub DGV_Control_btn_Click(sender As Object, e As EventArgs) Handles DGV_Control_btn.Click
        FormType = 14
        Switch_To_DV_Show()
    End Sub

    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        IMTranPrintData()
    End Sub

    Public Sub IMTranPrintData()

        Try
            Dim pp As New ReportConnection

            pp.rp.Load(Application.StartupPath & "\reports\Invoice_Bill.rpt")

            pp.CrTables = pp.rp.Database.Tables

            For Each CrTable In pp.CrTables
                pp.crtableLogoninfo = CrTable.LogOnInfo
                pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
                CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
            Next
            With pp
                .rp.SetParameterValue(0, " تاريخ : " + DateTimeEx.Value)
                .rp.SetParameterValue(1, USER_NAME)
                .rp.SetParameterValue(2, MY_Settings.Server_Desc)
                .rp.SetParameterValue(3, IM_Qty_LB.Text)
                .rp.SetParameterValue(4, T_ID)
                .rp.SetParameterValue(5, lblQtyPlus.Text)
                .rp.SetParameterValue(6, "فاتـــورة جـــرد")

                .rp.SetParameterValue(7, " العنوان : " + Title_txt.Text)
                .rp.SetParameterValue(8, " الرقم الألي : " + Bill_ID_Txt.Text)
                .rp.SetParameterValue(9, "")

            End With

            Dim p As New print
            p.CrystalReportViewer1.ReportSource = pp.rp
            p.Show()

            'pp.rp.PrintOptions.PrinterName = Default_Printer_A4
            'pp.rp.PrintToPrinter(1, False, 0, 0)
            'pp.rp.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub IM_btn_Click(sender As Object, e As EventArgs) Handles IM_btn.Click
        F_ItemsMenu = New ItemsMenu
        F_ItemsMenu.ShowDialog()
    End Sub


    Private Sub Search_txt_TextChanged(sender As Object, e As EventArgs) Handles Search_txt.TextChanged
        Dim dv As DataView
        dv = Bill_DT.DefaultView
        dv.RowFilter = "Item_Name like '%" & Search_txt.Text & "%'"
    End Sub

    Private Sub Search_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Search_txt.KeyDown
        If e.KeyCode = Keys.Delete Then Search_txt.Clear()
    End Sub

    Private Sub ClearSearch_btn_Click(sender As Object, e As EventArgs) Handles ClearSearch_btn.Click
        Search_txt.Clear()
        Barcode_Search_txt.Clear()
    End Sub

    Private Sub ALL_QTY_txt_TextChanged(sender As Object, e As EventArgs)  ', NewSaleByOne.TextChanged
        If Not String.IsNullOrWhiteSpace(sender.Text) Then
            Dim N As Double = sender.Text
            sender.Text = N.ToString("N")
        End If
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Bill_ID_Txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Bill_ID_Txt.KeyDown
        If e.KeyCode = Keys.Return Then Get_T_ID()
        If e.KeyCode = Keys.Up Then Up_Bill_btn_Click(sender, e)
        If e.KeyCode = Keys.Down Then Down_Bill_btn_Click(sender, e)
    End Sub


    Private Sub Bill_ID_Txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Bill_ID_Txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub Title_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Title_txt.KeyDown
        If e.KeyCode = Keys.Return Then
            If TryAuditHeader("HeaderSaved") Then Return
            Save_Title_Name(T_ID, Title_txt.Text)
        End If
    End Sub


    Private Sub Edit_butt_Click(sender As Object, e As EventArgs) Handles Edit_butt.Click
        If pendingHeaderAudit IsNot Nothing Then
            TryAuditHeader("EditOpened")
            Return
        End If
        If isDepended = True Then


            If U_Pch_Update = True Then
                If On_Update = False Then
                    Beep()
                    If MessageBox.Show(" سيتم تعديل الفاتورة بشكل مباشر مع كل تغير ... تأكيد التعديل ؟ ", "تعديل فاتورة", MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                        If TryAuditHeader("EditOpened") Then Return
                        If Open_Agents_Balance_MV_For_Edit(T_ID) = False Then Exit Sub

                        Edit_butt.BackColor = Color.GreenYellow
                        On_Update = True
                        AGMetroGrid.Enabled = True
                        AGMetroGrid.BackgroundColor = Color.LightYellow
                        AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
                        ADDCatButton.Enabled = True
                        RemoveCatButton.Enabled = True
                        Title_txt.Enabled = True
                        Ebable_CatFields()
                        Edit_butt.Text = "إيقاف التعديل"
                        Network_Edit_Tracker_insert("فتح تعديل مستند تسوية المخزون؛ T_ID=" & T_ID.ToString() & "؛ المدخل: " & User_Name_lb.Text, Bill_ID_Txt.Text, 39, 3)
                        Notes_txt.Enabled = True
                        DateTimeEx.Enabled = True
                        'Invoice_Type_cm.Enabled = True

                    End If

                Else
                    'AG_Balance_Update_Invoice_Type()
                    If TryAuditHeader("EditFinished") Then Return
                    Save_Title_Name(T_ID, Title_txt.Text)
                    Save_About(T_ID, Notes_txt.Text)

                    Save_Date(T_ID, DateTimeEx)
                    Save_Total(T_ID, TOTAL, 0)

                    On_Update = False
                    Edit_butt.Text = EditState
                    Edit_butt.BackColor = Color.WhiteSmoke
                    SelectStateBt()
                    Notes_txt.Enabled = False

                End If
            Else

                MsgBox("أنت غير مخول بتعديل فاتورة تم حفظها", MsgBoxStyle.Exclamation)
            End If

        Else

            If Edit_butt.Text = EditState Then
                Edit_butt.Text = "ح التعديل"
                Enable_Fields()
                AGMetroGrid.BackgroundColor = Color.LightYellow
                AGMetroGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow
            Else
                Save_About(T_ID, Notes_txt.Text)
                Save_Date(T_ID, DateTimeEx)
                Edit_butt.Text = EditState
                Disable_Fields()
                SelectStateBt()
            End If

        End If

    End Sub

    Private Sub Barcode_Search_txt_TextChanged(sender As Object, e As EventArgs) Handles Barcode_Search_txt.TextChanged
        Dim dv As DataView
        dv = Bill_DT.DefaultView
        dv.RowFilter = "Barcode like '%" & Barcode_Search_txt.Text & "%'"
    End Sub

    Private Sub Barcode_Search_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_Search_txt.KeyDown
        If e.KeyCode = Keys.Delete Then Barcode_Search_txt.Clear()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If IM_ID > 0 Then
            Show_IM_Details.IM_ID = IM_ID
            Show_IM_Details.ShowDialog()
        End If
    End Sub

    Private Sub تعديلToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تعديلToolStripMenuItem.Click
        If headerBusy OrElse deletingLine OrElse pendingHeaderAudit IsNot Nothing OrElse pendingDeleteAudit IsNot Nothing Then Return
        If isVoid OrElse (isDepended AndAlso Not On_Update) Then
            MsgBox("افتح تعديل المستند المعتمد أولًا؛ لا يمكن تعديل مستند ملغى.", MsgBoxStyle.Exclamation)
            Return
        End If
        Dim selected = AGMetroGrid.CurrentRow
        If selected Is Nothing OrElse selected.IsNewRow Then Return
        If Not AGMetroGrid.Columns.Contains("T_ID_CL") OrElse IsDBNull(selected.Cells("T_ID_CL").Value) Then Return
        Try
            If Not AuditSettlementLines.IsEnabled(MY_Settings.SqlConStr) Then Throw New InvalidOperationException("تعديل فرق السطر متاح مع المراقبة الجديدة فقط؛ لا تستخدم نافذة تعديل الأصناف العامة للتسويات.")
            Using editor As New IM_Update_Qty
                editor.Bill_T_ID = Me.T_ID
                editor.EditLineId = Convert.ToInt32(selected.Cells("T_ID_CL").Value)
                If AGMetroGrid.Columns.Contains("EX_Name_CL") Then editor.IM_NAME = Convert.ToString(selected.Cells("EX_Name_CL").Value)
                If AGMetroGrid.Columns.Contains("ST_Name_CL") Then editor.ST_NAME = Convert.ToString(selected.Cells("ST_Name_CL").Value)
                If editor.ShowDialog(Me) = DialogResult.OK Then Pch_Contents_SELECT_Bill()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & Environment.NewLine & "حدّث عرض المستند قبل تكرار أي تعديل.", MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub عرضالتكلفةToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles عرضالتكلفةToolStripMenuItem.Click
        Show_IM_Cost(True, F_Invoice.AGMetroGrid.CurrentRow.Cells("EX_ID_CL").Value, F_Invoice.AGMetroGrid.CurrentRow.Cells("U_ID_CL").Value)
    End Sub

    Private Sub تعديلصلاحياتالصنفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles تعديلصلاحياتالصنفToolStripMenuItem.Click
        Mang_IM_Valid_Notes.IM_ID = AGMetroGrid.CurrentRow.Cells("EX_ID_CL").Value
        Mang_IM_Valid_Notes.Bill_T_ID = AGMetroGrid.CurrentRow.Cells("T_ID_CL").Value
        Mang_IM_Valid_Notes.IM_NAME = AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value

        Mang_IM_Valid_Notes.ShowDialog()
    End Sub

    Private Sub علاضبطاقةالصنفToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles علاضبطاقةالصنفToolStripMenuItem.Click
        isShowing_Trans = True
        Str_ = F_Invoice.AGMetroGrid.CurrentRow.Cells("EX_Name_CL").Value
        F_ItemsMenu = New ItemsMenu
        F_ItemsMenu.ShowDialog()
    End Sub

    Private Sub DeletedBillLabel_Click(sender As Object, e As EventArgs) Handles DeletedBillLabel.Click
        MsgBox("التراجع عن إلغاء التسوية غير مفعّل حاليًا؛ لم يتم تغيير المستند.", MsgBoxStyle.Information)
    End Sub

    'Private Sub AGMetroGrid_DataSourceChanged(sender As Object, e As EventArgs) Handles AGMetroGrid.DataSourceChanged

    'End Sub
End Class
