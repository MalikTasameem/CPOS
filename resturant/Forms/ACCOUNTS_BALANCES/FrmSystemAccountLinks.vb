Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing

Public Class FrmSystemAccountLinks

    ' عدّل هذا حسب نظام الاتصال عندك
    Private ReadOnly ConStr As String = MY_Settings.SqlConStr

    Private _dtLinks As DataTable

    Private Sub FrmSystemAccountLinks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetupGrid()
            SetupFilter()
            LoadLinks()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetupFilter()
        cboFilter.Items.Clear()
        cboFilter.Items.Add("الكل")
        cboFilter.Items.Add("الأخطاء فقط")
        cboFilter.Items.Add("غير مربوط")
        cboFilter.Items.Add("الإجباري فقط")
        cboFilter.Items.Add("الاختياري فقط")
        cboFilter.Items.Add("الموقوف")
        cboFilter.SelectedIndex = 0
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadLinks()
    End Sub



    Private Sub btnChangeAccount_Click(sender As Object, e As EventArgs) Handles btnChangeAccount.Click
        ChangeSelectedAccount()
    End Sub

    Private Sub dgvLinks_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvLinks.CellDoubleClick
        If e.RowIndex >= 0 Then
            ChangeSelectedAccount()
        End If
    End Sub

    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
        ValidateAllLinks()
    End Sub

    Private Sub SetupGrid()
        With dgvLinks
            .AutoGenerateColumns = False
            .Columns.Clear()
            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .RowHeadersVisible = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .EnableHeadersVisualStyles = False
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 55, 72)
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .ColumnHeadersDefaultCellStyle.Font = New Font("Tahoma", 9.0!, FontStyle.Bold)
            .DefaultCellStyle.Font = New Font("Tahoma", 9.0!)
            .ColumnHeadersHeight = 36
            .RowTemplate.Height = 32
        End With



        AddTextColumn("AccountNameAr", "الحساب الأساسي", 220)
        AddTextColumn("LinkedAccountDisplay", "الحساب المرتبط", 260)
        AddTextColumn("RequiredStatusText", "الإلزام", 80)
        AddTextColumn("NaturalStatusText", "الطبيعة", 80)
        AddTextColumn("LeafStatusText", "نوع الحساب", 110)
        AddTextColumn("DuplicateStatusText", "التكرار", 90)
        AddTextColumn("ActiveStatusText", "الحالة", 80)
        AddTextColumn("ValidationMessage", "حالة التحقق", 260)

        AddHiddenColumn("SystemAccountTypeID")
        AddHiddenColumn("AccountKey")
        AddHiddenColumn("ACC_T_ID")
        AddHiddenColumn("IsValid")
        AddHiddenColumn("Required")
        AddHiddenColumn("MustBeLeaf")
        AddHiddenColumn("AllowSameAccount")
        AddHiddenColumn("Expected_ACC_NATURAL")
        AddHiddenColumn("TypeIsActive")
        AddHiddenColumn("TypeNotes")


        'AddTextColumn("AccountNameAr", "الحساب الأساسي", 220)
        'AddTextColumn("LinkedAccountDisplay", "الحساب المرتبط", 260)
        'AddTextColumn("RequiredStatusText", "الإلزام", 80)
        'AddTextColumn("NaturalStatusText", "الطبيعة", 80)
        'AddTextColumn("LeafStatusText", "نوع الحساب", 110)
        'AddTextColumn("DuplicateStatusText", "تكرار الحساب", 100)
        'AddTextColumn("ActiveStatusText", "الحالة", 80)
        'AddTextColumn("ValidationMessage", "حالة التحقق", 260)

        'AddHiddenColumn("SystemAccountTypeID")
        'AddHiddenColumn("AccountKey")
        'AddHiddenColumn("ACC_T_ID")
        'AddHiddenColumn("IsValid")
        'AddHiddenColumn("Required")
        'AddHiddenColumn("MustBeLeaf")
        'AddHiddenColumn("AllowSameAccount")
        'AddHiddenColumn("Expected_ACC_NATURAL")
        'AddHiddenColumn("TypeIsActive")


    End Sub

    Private Sub AddTextColumn(dataPropertyName As String, headerText As String, fillWeight As Single)
        Dim col As New DataGridViewTextBoxColumn()
        col.DataPropertyName = dataPropertyName
        col.HeaderText = headerText
        col.Name = dataPropertyName
        col.FillWeight = fillWeight
        col.SortMode = DataGridViewColumnSortMode.Automatic
        dgvLinks.Columns.Add(col)
    End Sub

    Private Sub AddHiddenColumn(dataPropertyName As String)
        Dim col As New DataGridViewTextBoxColumn()
        col.DataPropertyName = dataPropertyName
        col.Name = dataPropertyName
        col.Visible = False
        dgvLinks.Columns.Add(col)
    End Sub

    Private Sub LoadLinks()
        Try
            lblStatus.Text = "جاري تحميل الحسابات الأساسية..."

            Using cn As New SqlConnection(ConStr)
                Using cmd As New SqlCommand("dbo.ACC_SYSTEM_ACCOUNT_LINKS_LOAD", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 60

                    Using da As New SqlDataAdapter(cmd)
                        _dtLinks = New DataTable()
                        da.Fill(_dtLinks)
                    End Using
                End Using
            End Using

            PrepareDisplayColumns(_dtLinks)


            ApplyFilter()

            'dgvLinks.DataSource = _dtLinks
            'ApplyRowsStyle()

            'lblStatus.Text = "تم تحميل " & _dtLinks.Rows.Count.ToString() & " حساب أساسي."


            Dim invalidCount As Integer = 0

            If _dtLinks IsNot Nothing AndAlso _dtLinks.Columns.Contains("IsValid") Then
                For Each r As DataRow In _dtLinks.Rows
                    If r("IsValid") IsNot DBNull.Value AndAlso Convert.ToBoolean(r("IsValid")) = False Then
                        invalidCount += 1
                    End If
                Next
            End If

            If invalidCount = 0 Then
                lblStatus.Text = "تم تحميل " & _dtLinks.Rows.Count.ToString() & " حساب أساسي - كل الربط سليم."
            Else
                lblStatus.Text = "تم تحميل " & _dtLinks.Rows.Count.ToString() & " حساب أساسي - عدد المشاكل: " & invalidCount.ToString()
            End If



        Catch ex As Exception
            lblStatus.Text = "فشل تحميل البيانات"
            MessageBox.Show(ex.Message, "خطأ في التحميل", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub




    Private Sub ApplyFilter()
        If _dtLinks Is Nothing Then Return

        Dim dv As New DataView(_dtLinks)
        Dim filterText As String = ""

        If cboFilter IsNot Nothing AndAlso cboFilter.SelectedItem IsNot Nothing Then
            filterText = cboFilter.SelectedItem.ToString()
        End If

        Select Case filterText
            Case "الأخطاء فقط"
                dv.RowFilter = "IsValid = False"

            Case "غير مربوط"
                dv.RowFilter = "ACC_T_ID IS NULL"

            Case "الإجباري فقط"
                dv.RowFilter = "Required = True"

            Case "الاختياري فقط"
                dv.RowFilter = "Required = False"

            Case "الموقوف"
                dv.RowFilter = "TypeIsActive = False"

            Case Else
                dv.RowFilter = ""
        End Select

        dgvLinks.DataSource = dv
        ApplyRowsStyle()
        UpdateStatusText()
    End Sub



    Private Sub UpdateStatusText()
        If _dtLinks Is Nothing Then
            lblStatus.Text = "جاهز"
            Return
        End If

        Dim totalCount As Integer = _dtLinks.Rows.Count
        Dim invalidCount As Integer = 0
        Dim unlinkedCount As Integer = 0
        Dim requiredCount As Integer = 0

        For Each r As DataRow In _dtLinks.Rows
            If _dtLinks.Columns.Contains("IsValid") AndAlso
           r("IsValid") IsNot DBNull.Value AndAlso
           Convert.ToBoolean(r("IsValid")) = False Then
                invalidCount += 1
            End If

            If _dtLinks.Columns.Contains("ACC_T_ID") AndAlso r("ACC_T_ID") Is DBNull.Value Then
                unlinkedCount += 1
            End If

            If _dtLinks.Columns.Contains("Required") AndAlso
           r("Required") IsNot DBNull.Value AndAlso
           Convert.ToBoolean(r("Required")) = True Then
                requiredCount += 1
            End If
        Next

        lblStatus.Text =
        "الإجمالي: " & totalCount.ToString() &
        " | الإجباري: " & requiredCount.ToString() &
        " | غير مربوط: " & unlinkedCount.ToString() &
        " | مشاكل: " & invalidCount.ToString()
    End Sub

    Private Sub btnCancelLink_Click(sender As Object, e As EventArgs) Handles btnCancelLink.Click
        CancelSelectedLink()
    End Sub


    Private Sub CancelSelectedLink()
        If dgvLinks.CurrentRow Is Nothing Then
            MessageBox.Show("اختر حسابًا أساسيًا أولًا.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim row As DataGridViewRow = dgvLinks.CurrentRow

        Dim typeId As Integer = 0
        Dim accTId As Object = Nothing
        Dim accountName As String = Convert.ToString(row.Cells("AccountNameAr").Value)
        Dim linkedAccount As String = Convert.ToString(row.Cells("LinkedAccountDisplay").Value)
        Dim required As Boolean = GetBooleanCell(row, "Required")

        If row.Cells("SystemAccountTypeID").Value Is Nothing OrElse
       row.Cells("SystemAccountTypeID").Value Is DBNull.Value Then
            MessageBox.Show("لم يتم العثور على رقم نوع الحساب الأساسي.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        typeId = Convert.ToInt32(row.Cells("SystemAccountTypeID").Value)
        accTId = row.Cells("ACC_T_ID").Value

        If accTId Is Nothing OrElse accTId Is DBNull.Value Then
            MessageBox.Show("هذا الحساب الأساسي غير مربوط أصلًا.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim warnMsg As String =
        "هل تريد إلغاء ربط الحساب الأساسي التالي؟" & Environment.NewLine & Environment.NewLine &
        "الحساب الأساسي: " & accountName & Environment.NewLine &
        "الحساب المرتبط: " & linkedAccount

        If required Then
            warnMsg &= Environment.NewLine & Environment.NewLine &
                   "تنبيه: هذا الحساب إجباري، وإلغاء ربطه سيجعل الترحيل يفشل حتى يتم ربطه من جديد."
        End If

        Dim result = MessageBox.Show(
        warnMsg,
        "تأكيد إلغاء الربط",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning,
        MessageBoxDefaultButton.Button2
    )

        If result <> DialogResult.Yes Then Return

        Try
            Using cn As New SqlConnection(ConStr)
                Using cmd As New SqlCommand("dbo.ACC_SYSTEM_ACCOUNT_LINK_CANCEL", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 60

                    cmd.Parameters.Add("@SystemAccountTypeID", SqlDbType.Int).Value = typeId
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = DBNull.Value
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value =
                    "تم إلغاء الربط من شاشة الحسابات الأساسية"

                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("تم إلغاء الربط بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadLinks()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ في إلغاء الربط", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub






    Private Sub PrepareDisplayColumns(dt As DataTable)
        If dt Is Nothing Then Exit Sub

        If Not dt.Columns.Contains("RequiredText") Then
            dt.Columns.Add("RequiredText", GetType(String))
        End If

        If Not dt.Columns.Contains("LeafText") Then
            dt.Columns.Add("LeafText", GetType(String))
        End If

        For Each row As DataRow In dt.Rows
            Dim required As Boolean = False
            Dim mustBeLeaf As Boolean = False

            If Not IsDBNull(row("Required")) Then
                required = Convert.ToBoolean(row("Required"))
            End If

            If Not IsDBNull(row("MustBeLeaf")) Then
                mustBeLeaf = Convert.ToBoolean(row("MustBeLeaf"))
            End If

            row("RequiredText") = If(required, "نعم", "لا")
            row("LeafText") = If(mustBeLeaf, "فرعي", "رئيسي/فرعي")
        Next
    End Sub

    Private Sub ApplyRowsStyle()
        For Each gridRow As DataGridViewRow In dgvLinks.Rows

            Dim isValid As Boolean = False
            Dim required As Boolean = False
            Dim active As Boolean = True

            If dgvLinks.Columns.Contains("IsValid") AndAlso
           gridRow.Cells("IsValid").Value IsNot Nothing AndAlso
           gridRow.Cells("IsValid").Value IsNot DBNull.Value Then
                isValid = Convert.ToBoolean(gridRow.Cells("IsValid").Value)
            End If

            If dgvLinks.Columns.Contains("Required") AndAlso
           gridRow.Cells("Required").Value IsNot Nothing AndAlso
           gridRow.Cells("Required").Value IsNot DBNull.Value Then
                required = Convert.ToBoolean(gridRow.Cells("Required").Value)
            End If

            If dgvLinks.Columns.Contains("TypeIsActive") AndAlso
           gridRow.Cells("TypeIsActive").Value IsNot Nothing AndAlso
           gridRow.Cells("TypeIsActive").Value IsNot DBNull.Value Then
                active = Convert.ToBoolean(gridRow.Cells("TypeIsActive").Value)
            End If

            If Not active Then
                gridRow.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
                gridRow.DefaultCellStyle.ForeColor = Color.Gray

            ElseIf isValid Then
                gridRow.DefaultCellStyle.BackColor = Color.FromArgb(232, 245, 233)
                gridRow.DefaultCellStyle.ForeColor = Color.FromArgb(30, 80, 40)

            ElseIf required Then
                gridRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 238)
                gridRow.DefaultCellStyle.ForeColor = Color.FromArgb(120, 30, 30)

            Else
                gridRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 225)
                gridRow.DefaultCellStyle.ForeColor = Color.FromArgb(120, 90, 20)
            End If

        Next
    End Sub



    Private Sub ChangeSelectedAccount()
        If dgvLinks.CurrentRow Is Nothing Then
            MessageBox.Show("اختر حسابًا أساسيًا أولًا.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim accountKey As String = Convert.ToString(dgvLinks.CurrentRow.Cells("AccountKey").Value)
        Dim accountName As String = Convert.ToString(dgvLinks.CurrentRow.Cells("AccountNameAr").Value)

        If String.IsNullOrWhiteSpace(accountKey) Then
            MessageBox.Show("لم يتم العثور على مفتاح الحساب الأساسي.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        'Using frm As New FrmAccountPicker()
        '    frm.Text = "اختيار حساب لـ " & accountName

        Using frm As New FrmAccountPicker()
            frm.Text = "اختيار حساب لـ " & accountName

            Dim mustBeLeaf As Boolean = True

            If dgvLinks.CurrentRow.Cells("MustBeLeaf").Value IsNot Nothing AndAlso
               dgvLinks.CurrentRow.Cells("MustBeLeaf").Value IsNot DBNull.Value Then
                mustBeLeaf = Convert.ToBoolean(dgvLinks.CurrentRow.Cells("MustBeLeaf").Value)
            End If

            frm.OnlyLeaf = mustBeLeaf
            frm.OnlyUnlocked = True

            If frm.ShowDialog(Me) = DialogResult.OK Then

                If frm.SelectedAccountTID <= 0 Then
                    MessageBox.Show("لم يتم اختيار حساب صحيح.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                SaveAccountLink(accountKey, frm.SelectedAccountTID)
                LoadLinks()
            End If
        End Using
    End Sub

    Private Sub SaveAccountLink(accountKey As String, accTID As Integer)
        Try
            Using cn As New SqlConnection(ConStr)
                Using cmd As New SqlCommand("dbo.ACC_SYSTEM_ACCOUNT_LINK_SAVE", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 60

                    cmd.Parameters.Add("@AccountKey", SqlDbType.NVarChar, 100).Value = accountKey
                    cmd.Parameters.Add("@ACC_T_ID", SqlDbType.Int).Value = accTID

                    ' عدّل UserID حسب نظامك
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = DBNull.Value
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = "تم الربط من شاشة الحسابات الأساسية"

                    cn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("تم حفظ ربط الحساب بنجاح.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As SqlException
            MessageBox.Show(ex.Message, "خطأ في حفظ الربط", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub ValidateAllLinks()
        Try
            Dim ds As New DataSet()

            Using cn As New SqlConnection(ConStr)
                Using cmd As New SqlCommand("dbo.ACC_SYSTEM_ACCOUNT_VALIDATE_ALL", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandTimeout = 60

                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(ds)
                    End Using
                End Using
            End Using

            If ds.Tables.Count = 0 Then
                MessageBox.Show("لم يرجع إجراء الفحص أي نتيجة.", "فحص الربط", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim resultTable As DataTable = ds.Tables(0)
            Dim errorsTable As DataTable = Nothing

            If ds.Tables.Count > 1 Then
                errorsTable = ds.Tables(1)
            End If

            Dim success As Boolean = False
            Dim msg As String = "تم تنفيذ الفحص."

            If resultTable.Rows.Count > 0 Then
                If resultTable.Columns.Contains("Success") AndAlso resultTable.Rows(0)("Success") IsNot DBNull.Value Then
                    success = Convert.ToBoolean(resultTable.Rows(0)("Success"))
                End If

                If resultTable.Columns.Contains("Message") AndAlso resultTable.Rows(0)("Message") IsNot DBNull.Value Then
                    msg = Convert.ToString(resultTable.Rows(0)("Message"))
                End If
            End If

            If success Then
                MessageBox.Show(msg, "فحص الربط", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If errorsTable IsNot Nothing AndAlso errorsTable.Rows.Count > 0 Then
                    ShowValidationErrors(errorsTable)
                End If

                MessageBox.Show(msg, "فحص الربط", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            LoadLinks()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ في فحص الربط", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub ShowValidationErrors(errorsTable As DataTable)
        If errorsTable Is Nothing Then Exit Sub

        Dim frm As New Form()
        frm.Text = "أخطاء ربط الحسابات الأساسية"
        frm.StartPosition = FormStartPosition.CenterParent
        frm.Size = New Size(950, 500)
        frm.Font = New Font("Tahoma", 9.0!)
        frm.RightToLeft = RightToLeft.Yes
        frm.RightToLeftLayout = True

        Dim dgv As New DataGridView()
        dgv.Dock = DockStyle.Fill
        dgv.ReadOnly = True
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.RowHeadersVisible = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.DataSource = errorsTable

        frm.Controls.Add(dgv)

        AddHandler frm.Load,
            Sub()
                If dgv.Columns.Contains("ErrorType") Then
                    dgv.Columns("ErrorType").HeaderText = "نوع الخطأ"
                End If

                If dgv.Columns.Contains("AccountKey") Then
                    dgv.Columns("AccountKey").HeaderText = "المفتاح"
                End If

                If dgv.Columns.Contains("AccountNameAr") Then
                    dgv.Columns("AccountNameAr").HeaderText = "الحساب الأساسي"
                End If

                If dgv.Columns.Contains("ACC_T_ID") Then
                    dgv.Columns("ACC_T_ID").HeaderText = "رقم الحساب"
                End If

                If dgv.Columns.Contains("ACC_CODE") Then
                    dgv.Columns("ACC_CODE").HeaderText = "كود الحساب"
                End If

                If dgv.Columns.Contains("ACC_NAME") Then
                    dgv.Columns("ACC_NAME").HeaderText = "اسم الحساب"
                End If

                If dgv.Columns.Contains("ACC_NATURAL") Then
                    dgv.Columns("ACC_NATURAL").HeaderText = "الطبيعة الحالية"
                End If

                If dgv.Columns.Contains("Expected_ACC_NATURAL") Then
                    dgv.Columns("Expected_ACC_NATURAL").HeaderText = "الطبيعة المطلوبة"
                End If

                If dgv.Columns.Contains("ValidationMessage") Then
                    dgv.Columns("ValidationMessage").HeaderText = "رسالة التحقق"
                End If
            End Sub

        frm.ShowDialog(Me)
    End Sub

    Private Sub btnEditType_Click(sender As Object, e As EventArgs) Handles btnEditType.Click
        EditSelectedType()
    End Sub

    Private Sub EditSelectedType()
        If dgvLinks.CurrentRow Is Nothing Then
            MessageBox.Show("اختر حسابًا أساسيًا أولًا.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim row As DataGridViewRow = dgvLinks.CurrentRow

        If row.Cells("SystemAccountTypeID").Value Is Nothing OrElse
       row.Cells("SystemAccountTypeID").Value Is DBNull.Value Then
            MessageBox.Show("لم يتم العثور على رقم نوع الحساب الأساسي.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using frm As New FrmSystemAccountTypeEdit()
            frm.SystemAccountTypeID = Convert.ToInt32(row.Cells("SystemAccountTypeID").Value)
            frm.AccountNameAr = Convert.ToString(row.Cells("AccountNameAr").Value)

            frm.RequiredValue = GetBooleanCell(row, "Required")
            frm.AllowSameAccountValue = GetBooleanCell(row, "AllowSameAccount")
            frm.MustBeLeafValue = GetBooleanCell(row, "MustBeLeaf")
            frm.IsActiveValue = GetBooleanCell(row, "TypeIsActive")

            If row.Cells("Expected_ACC_NATURAL").Value Is Nothing OrElse
           row.Cells("Expected_ACC_NATURAL").Value Is DBNull.Value Then
                frm.ExpectedNaturalValue = ""
            Else
                frm.ExpectedNaturalValue = Convert.ToString(row.Cells("Expected_ACC_NATURAL").Value)
            End If

            If dgvLinks.Columns.Contains("TypeNotes") AndAlso
           row.Cells("TypeNotes").Value IsNot Nothing AndAlso
           row.Cells("TypeNotes").Value IsNot DBNull.Value Then
                frm.NotesValue = Convert.ToString(row.Cells("TypeNotes").Value)
            Else
                frm.NotesValue = ""
            End If

            If frm.ShowDialog(Me) = DialogResult.OK Then
                LoadLinks()
            End If
        End Using
    End Sub


    Private Sub btnDetails_Click(sender As Object, e As EventArgs) Handles btnDetails.Click
        ShowSelectedDetails()
    End Sub



    Private Sub ShowSelectedDetails()
        If dgvLinks.CurrentRow Is Nothing Then
            MessageBox.Show("اختر صفًا أولًا.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim row As DataGridViewRow = dgvLinks.CurrentRow

        Dim msg As String =
        "الحساب الأساسي: " & Convert.ToString(row.Cells("AccountNameAr").Value) & Environment.NewLine &
        "المفتاح: " & Convert.ToString(row.Cells("AccountKey").Value) & Environment.NewLine &
        "الحساب المرتبط: " & Convert.ToString(row.Cells("LinkedAccountDisplay").Value) & Environment.NewLine &
        "إجباري: " & Convert.ToString(row.Cells("RequiredStatusText").Value) & Environment.NewLine &
        "الطبيعة: " & Convert.ToString(row.Cells("NaturalStatusText").Value) & Environment.NewLine &
        "نوع الحساب: " & Convert.ToString(row.Cells("LeafStatusText").Value) & Environment.NewLine &
        "التكرار: " & Convert.ToString(row.Cells("DuplicateStatusText").Value) & Environment.NewLine &
        "الحالة: " & Convert.ToString(row.Cells("ActiveStatusText").Value) & Environment.NewLine &
        "رسالة التحقق: " & Convert.ToString(row.Cells("ValidationMessage").Value)

        If dgvLinks.Columns.Contains("TypeNotes") Then
            msg &= Environment.NewLine &
               "ملاحظات: " & Convert.ToString(row.Cells("TypeNotes").Value)
        End If

        MessageBox.Show(msg, "تفاصيل الحساب الأساسي", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub


    Private Function GetBooleanCell(row As DataGridViewRow, columnName As String) As Boolean
        If row Is Nothing Then Return False
        If Not dgvLinks.Columns.Contains(columnName) Then Return False

        Dim v = row.Cells(columnName).Value

        If v Is Nothing OrElse v Is DBNull.Value Then
            Return False
        End If

        Return Convert.ToBoolean(v)
    End Function


    Private Sub btnShowLog_Click(sender As Object, e As EventArgs) Handles btnShowLog.Click
        ShowSelectedLog()
    End Sub

    Private Sub ShowSelectedLog()
        If dgvLinks.CurrentRow Is Nothing Then
            MessageBox.Show("اختر حسابًا أساسيًا أولًا.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If dgvLinks.CurrentRow.Cells("SystemAccountTypeID").Value Is Nothing OrElse
           dgvLinks.CurrentRow.Cells("SystemAccountTypeID").Value Is DBNull.Value Then
            MessageBox.Show("لم يتم العثور على رقم نوع الحساب الأساسي.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim typeId As Integer = Convert.ToInt32(dgvLinks.CurrentRow.Cells("SystemAccountTypeID").Value)
        Dim accountName As String = Convert.ToString(dgvLinks.CurrentRow.Cells("AccountNameAr").Value)

        Using frm As New FrmSystemAccountLinksLog()
            frm.SystemAccountTypeID = typeId
            frm.AccountNameAr = accountName
            frm.ShowDialog(Me)
        End Using
    End Sub

End Class