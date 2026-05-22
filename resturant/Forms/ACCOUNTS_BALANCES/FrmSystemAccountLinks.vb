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
            LoadLinks()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadLinks()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
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

        AddTextColumn("AccountNameAr", "الحساب الأساسي", 230)
        AddTextColumn("LinkedAccountDisplay", "الحساب المرتبط", 280)
        AddTextColumn("Expected_ACC_NATURAL", "الطبيعة", 70)
        AddTextColumn("RequiredText", "إلزامي", 70)
        AddTextColumn("LeafText", "نوع الربط", 90)
        AddTextColumn("ValidationMessage", "حالة التحقق", 260)

        AddHiddenColumn("AccountKey")
        AddHiddenColumn("ACC_T_ID")
        AddHiddenColumn("IsValid")
        AddHiddenColumn("Required")
        AddHiddenColumn("MustBeLeaf")
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

            dgvLinks.DataSource = _dtLinks
            ApplyRowsStyle()

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

            If gridRow.Cells("IsValid").Value IsNot Nothing AndAlso
               gridRow.Cells("IsValid").Value IsNot DBNull.Value Then
                isValid = Convert.ToBoolean(gridRow.Cells("IsValid").Value)
            End If

            If isValid Then
                gridRow.DefaultCellStyle.BackColor = Color.FromArgb(232, 245, 233)
                gridRow.DefaultCellStyle.ForeColor = Color.FromArgb(30, 80, 40)
            Else
                gridRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 238)
                gridRow.DefaultCellStyle.ForeColor = Color.FromArgb(120, 30, 30)
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


    'Private Sub ValidateAllLinks()
    '    Try
    '        Using cn As New SqlConnection(ConStr)
    '            Using cmd As New SqlCommand("dbo.ACC_SYSTEM_ACCOUNT_VALIDATE_ALL", cn)
    '                cmd.CommandType = CommandType.StoredProcedure
    '                cmd.CommandTimeout = 60

    '                cn.Open()

    '                Dim dt As New DataTable()
    '                Using da As New SqlDataAdapter(cmd)
    '                    da.Fill(dt)
    '                End Using

    '                If dt.Rows.Count > 0 AndAlso dt.Columns.Contains("Success") Then
    '                    MessageBox.Show(
    '                        Convert.ToString(dt.Rows(0)("Message")),
    '                        "فحص الربط",
    '                        MessageBoxButtons.OK,
    '                        MessageBoxIcon.Information
    '                    )
    '                Else
    '                    MessageBox.Show(
    '                        "تم تنفيذ الفحص.",
    '                        "فحص الربط",
    '                        MessageBoxButtons.OK,
    '                        MessageBoxIcon.Information
    '                    )
    '                End If
    '            End Using
    '        End Using

    '        LoadLinks()

    '    Catch ex As SqlException
    '        LoadLinks()

    '        MessageBox.Show(
    '            ex.Message,
    '            "نتيجة الفحص",
    '            MessageBoxButtons.OK,
    '            MessageBoxIcon.Warning
    '        )

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

End Class