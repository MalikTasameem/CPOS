Imports System.Data
Imports System.Data.SqlClient

Partial Public Class FrmIncomeStatementDesigner

    Private _isLoading As Boolean = False
    Private _currentTemplateID As Integer = 0
    Private _currentLineID As Integer = 0
    Private _currentAccountLineID As Integer = 0
    Private _currentFormulaLineID As Integer = 0

    Private Const CurrentUserID As Integer = 1

#Region "Form Load"

    Private Sub FrmIncomeStatementDesigner_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            _isLoading = True

            InitDates()
            InitCombos()
            InitGrids()

            LoadTemplates()

            _isLoading = False

            If cboTemplates.Items.Count > 0 Then
                cboTemplates.SelectedIndex = 0
            End If

            SetStatus("تم تحميل الشاشة.")

        Catch ex As Exception
            _isLoading = False
            ShowError(ex)
        End Try
    End Sub

    Private Sub InitDates()
        dtpDateFrom.Value = New Date(Date.Today.Year, 1, 1)
        dtpDateTo.Value = Date.Today
    End Sub

    Private Sub InitCombos()
        BindSmallCombo(cboLineType, GetLineTypeTable(), "Name", "ID")
        BindSmallCombo(cboCalculationSign, GetCalculationSignTable(), "Name", "ID")
        BindSmallCombo(cboDisplaySignMode, GetDisplaySignModeTable(), "Name", "ID")
        BindSmallCombo(cboNormalBalanceSide, GetNormalBalanceSideTable(), "Name", "ID")
    End Sub

    Private Sub InitGrids()
        ConfigureGrid(dgvLines)
        ConfigureGrid(dgvAccountLines)
        ConfigureGrid(dgvLinkedAccounts)
        ConfigureGrid(dgvFormulaLines)
        ConfigureGrid(dgvFormulaDetails)
        ConfigureGrid(dgvPreview)
        ConfigureGrid(dgvValidation)
    End Sub

#End Region

#Region "Connection"

    Private Function GetConnectionString() As String
        ' عدّل هذا السطر حسب مشروعك
        ' مثال:
        ' Return My.Settings.CPOS_ACCOUNTINGConnectionString

        Return MY_Settings.SqlConStr
    End Function

    Private Function GetConnection() As SqlConnection
        Return New SqlConnection(GetConnectionString())
    End Function

#End Region

#Region "Database Helpers"

    Private Function ExecuteDataTable(queryOrProcedure As String,
                                      Optional parameters As List(Of SqlParameter) = Nothing,
                                      Optional isStoredProcedure As Boolean = True) As DataTable

        Using con As SqlConnection = GetConnection()
            Using cmd As New SqlCommand(queryOrProcedure, con)
                cmd.CommandTimeout = 120
                cmd.CommandType = If(isStoredProcedure, CommandType.StoredProcedure, CommandType.Text)

                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters.ToArray())
                End If

                Using da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    Return dt
                End Using
            End Using
        End Using
    End Function

    Private Function ExecuteScalarValue(queryOrProcedure As String,
                                        Optional parameters As List(Of SqlParameter) = Nothing,
                                        Optional isStoredProcedure As Boolean = True) As Object

        Using con As SqlConnection = GetConnection()
            Using cmd As New SqlCommand(queryOrProcedure, con)
                cmd.CommandTimeout = 120
                cmd.CommandType = If(isStoredProcedure, CommandType.StoredProcedure, CommandType.Text)

                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters.ToArray())
                End If

                con.Open()
                Return cmd.ExecuteScalar()
            End Using
        End Using
    End Function

    Private Function ExecuteProcedureWithOutput(procedureName As String,
                                                parameters As List(Of SqlParameter),
                                                outputParameterName As String) As Object

        Using con As SqlConnection = GetConnection()
            Using cmd As New SqlCommand(procedureName, con)
                cmd.CommandTimeout = 120
                cmd.CommandType = CommandType.StoredProcedure

                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters.ToArray())
                End If

                con.Open()
                cmd.ExecuteNonQuery()

                Return cmd.Parameters(outputParameterName).Value
            End Using
        End Using
    End Function

#End Region

#Region "Combo Tables"

    Private Function GetLineTypeTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("ID", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))

        dt.Rows.Add(1, "عنوان")
        dt.Rows.Add(2, "بند حسابات")
        dt.Rows.Add(3, "إجمالي تلقائي")
        dt.Rows.Add(4, "معادلة")
        dt.Rows.Add(5, "فاصل")

        Return dt
    End Function

    Private Function GetCalculationSignTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("ID", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))

        dt.Rows.Add(1, "يضاف")
        dt.Rows.Add(2, "يخصم")

        Return dt
    End Function

    Private Function GetDisplaySignModeTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("ID", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))

        dt.Rows.Add(1, "عرض موجب")
        dt.Rows.Add(2, "بين قوسين إذا كان مخصومًا")
        dt.Rows.Add(3, "حسب الرصيد الحقيقي")
        dt.Rows.Add(4, "دائمًا بين قوسين")
        dt.Rows.Add(5, "بدون إشارة خاصة")

        Return dt
    End Function

    Private Function GetNormalBalanceSideTable() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("ID", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))

        dt.Rows.Add(0, "لا يهم")
        dt.Rows.Add(1, "مدين")
        dt.Rows.Add(2, "دائن")

        Return dt
    End Function

    Private Sub BindSmallCombo(cbo As ComboBox, dt As DataTable, displayMember As String, valueMember As String)
        cbo.DataSource = dt
        cbo.DisplayMember = displayMember
        cbo.ValueMember = valueMember
    End Sub

#End Region

#Region "Templates"

    Private Sub LoadTemplates()
        Dim sql As String =
            "SELECT TemplateID, TemplateName " &
            "FROM dbo.IncomeStatementTemplates " &
            "WHERE IsActive = 1 " &
            "ORDER BY IsDefault DESC, TemplateID DESC;"

        Dim dt As DataTable = ExecuteDataTable(sql, Nothing, False)

        cboTemplates.DataSource = dt
        cboTemplates.DisplayMember = "TemplateName"
        cboTemplates.ValueMember = "TemplateID"
    End Sub

    Private Sub cboTemplates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTemplates.SelectedIndexChanged
        If _isLoading Then Return

        Try
            If cboTemplates.SelectedValue Is Nothing OrElse Not IsNumeric(cboTemplates.SelectedValue) Then
                Return
            End If

            _currentTemplateID = CInt(cboTemplates.SelectedValue)

            LoadAllTemplateData()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnCreateDefaultTemplate_Click(sender As Object, e As EventArgs) Handles btnCreateDefaultTemplate.Click
        Try
            If MessageBox.Show("هل تريد إنشاء قالب قائمة دخل تجارية افتراضية؟",
                               "تأكيد",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            Dim outTemplateID As New SqlParameter("@TemplateID", SqlDbType.Int)
            outTemplateID.Direction = ParameterDirection.Output

            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@CreatedBy", CurrentUserID),
                outTemplateID
            }

            ExecuteProcedureWithOutput("dbo.ACC_IncomeStatement_CreateDefaultCommercialTemplate",
                                       parameters,
                                       "@TemplateID")

            LoadTemplates()

            If outTemplateID.Value IsNot DBNull.Value Then
                cboTemplates.SelectedValue = CInt(outTemplateID.Value)
            End If

            SetStatus("تم إنشاء قالب افتراضي بنجاح.")

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

#End Region

#Region "Load Data"

    Private Sub LoadAllTemplateData()
        If _currentTemplateID <= 0 Then Return

        LoadLines()
        LoadAccountLines()
        LoadFormulaLines()
        LoadPreview()
        LoadValidation()

        SetStatus("تم تحميل بيانات القالب.")
    End Sub

    Private Sub LoadLines()
        Dim dt As DataTable = ExecuteDataTable(
            "dbo.ACC_IncomeStatement_GetTemplateLines",
            New List(Of SqlParameter) From {
                New SqlParameter("@TemplateID", _currentTemplateID)
            })

        dgvLines.DataSource = dt
        FormatLinesGrid(dgvLines)

        If dt.Rows.Count > 0 Then
            dgvLines.Rows(0).Selected = True
            SelectLineFromGrid()
        Else
            ClearLineDetails()
        End If
    End Sub

    Private Sub LoadAccountLines()
        Dim dt As DataTable = ExecuteDataTable(
            "dbo.ACC_IncomeStatement_GetTemplateLines",
            New List(Of SqlParameter) From {
                New SqlParameter("@TemplateID", _currentTemplateID)
            })

        If dt.Columns.Contains("LineType") Then
            Dim view As New DataView(dt)
            view.RowFilter = "LineType = 2"
            dgvAccountLines.DataSource = view.ToTable()
        Else
            dgvAccountLines.DataSource = dt
        End If

        FormatLinesGrid(dgvAccountLines)

        If dgvAccountLines.Rows.Count > 0 Then
            dgvAccountLines.Rows(0).Selected = True
            SelectAccountLineFromGrid()
        Else
            dgvLinkedAccounts.DataSource = Nothing
        End If
    End Sub

    Private Sub LoadFormulaLines()
        Dim dt As DataTable = ExecuteDataTable(
            "dbo.ACC_IncomeStatement_GetTemplateLines",
            New List(Of SqlParameter) From {
                New SqlParameter("@TemplateID", _currentTemplateID)
            })

        If dt.Columns.Contains("LineType") Then
            Dim view As New DataView(dt)
            view.RowFilter = "LineType = 4"
            dgvFormulaLines.DataSource = view.ToTable()
        Else
            dgvFormulaLines.DataSource = dt
        End If

        FormatLinesGrid(dgvFormulaLines)

        If dgvFormulaLines.Rows.Count > 0 Then
            dgvFormulaLines.Rows(0).Selected = True
            SelectFormulaLineFromGrid()
        Else
            dgvFormulaDetails.DataSource = Nothing
        End If
    End Sub

    Private Sub LoadLinkedAccounts(lineID As Integer)
        If lineID <= 0 Then
            dgvLinkedAccounts.DataSource = Nothing
            Return
        End If

        Dim dt As DataTable = ExecuteDataTable(
            "dbo.ACC_IncomeStatement_GetLineAccounts",
            New List(Of SqlParameter) From {
                New SqlParameter("@LineID", lineID)
            })

        dgvLinkedAccounts.DataSource = dt
        FormatLinkedAccountsGrid()
    End Sub

    Private Sub LoadFormulaDetails(formulaLineID As Integer)
        If formulaLineID <= 0 Then
            dgvFormulaDetails.DataSource = Nothing
            Return
        End If

        Dim dt As DataTable = ExecuteDataTable(
            "dbo.ACC_IncomeStatement_GetFormulaDetails",
            New List(Of SqlParameter) From {
                New SqlParameter("@FormulaLineID", formulaLineID)
            })

        dgvFormulaDetails.DataSource = dt
        FormatFormulaDetailsGrid()
    End Sub

    Private Sub LoadPreview()
        If _currentTemplateID <= 0 Then Return

        Dim dt As DataTable = ExecuteDataTable(
            "dbo.ACC_IncomeStatement_Preview",
            New List(Of SqlParameter) From {
                New SqlParameter("@TemplateID", _currentTemplateID),
                New SqlParameter("@DateFrom", dtpDateFrom.Value.Date),
                New SqlParameter("@DateTo", dtpDateTo.Value.Date),
                New SqlParameter("@HideZero", chkHideZero.Checked)
            })

        ClearTitleRowsAmounts(dt)
        dgvPreview.DataSource = dt
        FormatPreviewGrid()
    End Sub

    Private Sub LoadValidation()
        If _currentTemplateID <= 0 Then Return

        Dim dt As DataTable = ExecuteDataTable(
            "dbo.ACC_IncomeStatement_Validate",
            New List(Of SqlParameter) From {
                New SqlParameter("@TemplateID", _currentTemplateID),
                New SqlParameter("@DateFrom", dtpDateFrom.Value.Date),
                New SqlParameter("@DateTo", dtpDateTo.Value.Date),
                New SqlParameter("@OnlyAccountsWithMovement", True),
                New SqlParameter("@OnlyLikelyIncomeStatementAccounts", True)
            })

        dgvValidation.DataSource = dt
        FormatValidationGrid()

        SetStatus("عدد التنبيهات: " & dt.Rows.Count.ToString())
    End Sub

#End Region

#Region "Line Selection"

    Private Sub dgvLines_SelectionChanged(sender As Object, e As EventArgs) Handles dgvLines.SelectionChanged
        If _isLoading Then Return
        SelectLineFromGrid()
    End Sub

    Private Sub SelectLineFromGrid()
        Try
            If dgvLines.CurrentRow Is Nothing Then Return
            If dgvLines.CurrentRow.DataBoundItem Is Nothing Then Return

            Dim row As DataRowView = TryCast(dgvLines.CurrentRow.DataBoundItem, DataRowView)
            If row Is Nothing Then Return

            _currentLineID = SafeInt(row("LineID"))

            txtLineCode.Text = SafeString(row("LineCode"))
            txtLineName.Text = SafeString(row("LineName"))
            txtSortOrder.Text = SafeString(row("SortOrder"))
            txtFormulaText.Text = SafeString(row("FormulaText"))

            SetComboValue(cboLineType, SafeInt(row("LineType")))
            SetComboValue(cboCalculationSign, SafeInt(row("CalculationSign")))
            SetComboValue(cboDisplaySignMode, SafeInt(row("DisplaySignMode")))

            Dim normalSide As Integer = 0
            If Not IsDBNull(row("NormalBalanceSide")) Then
                normalSide = SafeInt(row("NormalBalanceSide"))
            End If
            SetComboValue(cboNormalBalanceSide, normalSide)

            chkIsBold.Checked = SafeBool(row("IsBold"))
            chkIsVisible.Checked = SafeBool(row("IsVisible"))
            chkShowWhenZero.Checked = SafeBool(row("ShowWhenZero"))

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub dgvAccountLines_SelectionChanged(sender As Object, e As EventArgs) Handles dgvAccountLines.SelectionChanged
        If _isLoading Then Return
        SelectAccountLineFromGrid()
    End Sub

    Private Sub SelectAccountLineFromGrid()
        Try
            If dgvAccountLines.CurrentRow Is Nothing Then Return
            If dgvAccountLines.CurrentRow.DataBoundItem Is Nothing Then Return

            Dim row As DataRowView = TryCast(dgvAccountLines.CurrentRow.DataBoundItem, DataRowView)
            If row Is Nothing Then Return

            _currentAccountLineID = SafeInt(row("LineID"))
            LoadLinkedAccounts(_currentAccountLineID)

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub dgvFormulaLines_SelectionChanged(sender As Object, e As EventArgs) Handles dgvFormulaLines.SelectionChanged
        If _isLoading Then Return
        SelectFormulaLineFromGrid()
    End Sub

    Private Sub SelectFormulaLineFromGrid()
        Try
            If dgvFormulaLines.CurrentRow Is Nothing Then Return
            If dgvFormulaLines.CurrentRow.DataBoundItem Is Nothing Then Return

            Dim row As DataRowView = TryCast(dgvFormulaLines.CurrentRow.DataBoundItem, DataRowView)
            If row Is Nothing Then Return

            _currentFormulaLineID = SafeInt(row("LineID"))
            LoadFormulaDetails(_currentFormulaLineID)

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

#End Region

#Region "Line Buttons"

    Private Sub btnAddLine_Click(sender As Object, e As EventArgs) Handles btnAddLine.Click
        Try
            If _currentTemplateID <= 0 Then
                MessageBox.Show("اختر قالبًا أولًا.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If txtLineName.Text.Trim() = "" Then
                MessageBox.Show("أدخل اسم البند.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim outLineID As New SqlParameter("@NewLineID", SqlDbType.Int)
            outLineID.Direction = ParameterDirection.Output

            Dim parameters As New List(Of SqlParameter) From {
                New SqlParameter("@TemplateID", _currentTemplateID),
                New SqlParameter("@ParentLineID", DBNull.Value),
                New SqlParameter("@LineCode", DbNullIfEmpty(txtLineCode.Text)),
                New SqlParameter("@LineName", txtLineName.Text.Trim()),
                New SqlParameter("@LineType", CInt(cboLineType.SelectedValue)),
                New SqlParameter("@SortOrder", DbNullIfEmptyInt(txtSortOrder.Text)),
                New SqlParameter("@CalculationSign", CInt(cboCalculationSign.SelectedValue)),
                New SqlParameter("@DisplaySignMode", CInt(cboDisplaySignMode.SelectedValue)),
                New SqlParameter("@NormalBalanceSide", DbNullIfZero(CInt(cboNormalBalanceSide.SelectedValue))),
                New SqlParameter("@FormulaText", DbNullIfEmpty(txtFormulaText.Text)),
                New SqlParameter("@IsBold", chkIsBold.Checked),
                New SqlParameter("@IsVisible", chkIsVisible.Checked),
                New SqlParameter("@ShowWhenZero", chkShowWhenZero.Checked),
                New SqlParameter("@UserID", CurrentUserID),
                outLineID
            }

            ExecuteProcedureWithOutput("dbo.ACC_IncomeStatement_AddLine", parameters, "@NewLineID")

            LoadAllTemplateData()

            SetStatus("تمت إضافة البند.")

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnEditLine_Click(sender As Object, e As EventArgs) Handles btnEditLine.Click
        Try
            If _currentLineID <= 0 Then
                MessageBox.Show("اختر بندًا للتعديل.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If txtLineName.Text.Trim() = "" Then
                MessageBox.Show("أدخل اسم البند.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim sortOrder As Integer
            If Not Integer.TryParse(txtSortOrder.Text.Trim(), sortOrder) Then
                MessageBox.Show("الترتيب يجب أن يكون رقمًا.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim dt As DataTable = ExecuteDataTable(
                "dbo.ACC_IncomeStatement_UpdateLine",
                New List(Of SqlParameter) From {
                    New SqlParameter("@LineID", _currentLineID),
                    New SqlParameter("@ParentLineID", DBNull.Value),
                    New SqlParameter("@LineCode", DbNullIfEmpty(txtLineCode.Text)),
                    New SqlParameter("@LineName", txtLineName.Text.Trim()),
                    New SqlParameter("@LineType", CInt(cboLineType.SelectedValue)),
                    New SqlParameter("@SortOrder", sortOrder),
                    New SqlParameter("@CalculationSign", CInt(cboCalculationSign.SelectedValue)),
                    New SqlParameter("@DisplaySignMode", CInt(cboDisplaySignMode.SelectedValue)),
                    New SqlParameter("@NormalBalanceSide", DbNullIfZero(CInt(cboNormalBalanceSide.SelectedValue))),
                    New SqlParameter("@FormulaText", DbNullIfEmpty(txtFormulaText.Text)),
                    New SqlParameter("@IsBold", chkIsBold.Checked),
                    New SqlParameter("@IsVisible", chkIsVisible.Checked),
                    New SqlParameter("@ShowWhenZero", chkShowWhenZero.Checked),
                    New SqlParameter("@UserID", CurrentUserID)
                })

            ShowProcedureMessage(dt)

            LoadAllTemplateData()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnDisableLine_Click(sender As Object, e As EventArgs) Handles btnDisableLine.Click
        Try
            If _currentLineID <= 0 Then
                MessageBox.Show("اختر بندًا للتعطيل.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If MessageBox.Show("هل تريد تعطيل هذا البند؟",
                               "تأكيد",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            Dim dt As DataTable = ExecuteDataTable(
                "dbo.ACC_IncomeStatement_DisableLine",
                New List(Of SqlParameter) From {
                    New SqlParameter("@LineID", _currentLineID),
                    New SqlParameter("@UserID", CurrentUserID)
                })

            ShowProcedureMessage(dt)

            LoadAllTemplateData()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnLineUp_Click(sender As Object, e As EventArgs) Handles btnLineUp.Click
        MoveCurrentLine(-10)
    End Sub

    Private Sub btnLineDown_Click(sender As Object, e As EventArgs) Handles btnLineDown.Click
        MoveCurrentLine(10)
    End Sub

    Private Sub MoveCurrentLine(delta As Integer)
        Try
            If _currentLineID <= 0 Then Return

            Dim currentSort As Integer
            If Not Integer.TryParse(txtSortOrder.Text.Trim(), currentSort) Then Return

            Dim newSort As Integer = currentSort + delta
            If newSort < 0 Then newSort = 0

            Dim dt As DataTable = ExecuteDataTable(
                "dbo.ACC_IncomeStatement_UpdateLineSort",
                New List(Of SqlParameter) From {
                    New SqlParameter("@LineID", _currentLineID),
                    New SqlParameter("@NewSortOrder", newSort),
                    New SqlParameter("@UserID", CurrentUserID)
                })

            ShowProcedureMessage(dt)

            LoadAllTemplateData()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

#End Region

#Region "Account Buttons"

    Private Sub btnLinkAccount_Click(sender As Object, e As EventArgs) Handles btnLinkAccount.Click
        Try
            If _currentAccountLineID <= 0 Then
                MessageBox.Show("اختر بند حسابات أولًا.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Using frm As New FrmIncomeStatementAccountPicker()
                frm.DateFrom = dtpDateFrom.Value.Date
                frm.DateTo = dtpDateTo.Value.Date

                If frm.ShowDialog(Me) = DialogResult.OK Then

                    Dim dt As DataTable = ExecuteDataTable(
                    "dbo.ACC_IncomeStatement_LinkAccount",
                    New List(Of SqlParameter) From {
                        New SqlParameter("@LineID", _currentAccountLineID),
                        New SqlParameter("@AccountID", frm.SelectedAccountID),
                        New SqlParameter("@IncludeChildren", frm.IncludeChildren),
                        New SqlParameter("@AccountSignMode", frm.AccountSignMode),
                        New SqlParameter("@AllowDuplicateInTemplate", False),
                        New SqlParameter("@UserID", CurrentUserID)
                    })

                    ShowProcedureMessage(dt)

                    LoadLinkedAccounts(_currentAccountLineID)
                    LoadPreview()
                    LoadValidation()

                    SetStatus("تم ربط الحساب: " & frm.SelectedAccountCode & " - " & frm.SelectedAccountName)
                End If
            End Using

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnUnlinkAccount_Click(sender As Object, e As EventArgs) Handles btnUnlinkAccount.Click
        Try
            If dgvLinkedAccounts.CurrentRow Is Nothing Then
                MessageBox.Show("اختر حسابًا مرتبطًا أولًا.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim row As DataRowView = TryCast(dgvLinkedAccounts.CurrentRow.DataBoundItem, DataRowView)
            If row Is Nothing Then Return

            Dim linkID As Integer = SafeInt(row("LinkID"))

            If MessageBox.Show("هل تريد إلغاء ربط الحساب؟",
                               "تأكيد",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            Dim dt As DataTable = ExecuteDataTable(
                "dbo.ACC_IncomeStatement_UnlinkAccount",
                New List(Of SqlParameter) From {
                    New SqlParameter("@LinkID", linkID),
                    New SqlParameter("@UserID", CurrentUserID)
                })

            ShowProcedureMessage(dt)

            LoadLinkedAccounts(_currentAccountLineID)
            LoadPreview()
            LoadValidation()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnUpdateAccountLink_Click(sender As Object, e As EventArgs) Handles btnUpdateAccountLink.Click
        Try
            If dgvLinkedAccounts.CurrentRow Is Nothing Then
                MessageBox.Show("اختر حسابًا مرتبطًا أولًا.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim row As DataRowView = TryCast(dgvLinkedAccounts.CurrentRow.DataBoundItem, DataRowView)
            If row Is Nothing Then Return

            Dim linkID As Integer = SafeInt(row("LinkID"))

            Using frm As New FrmIncomeStatementAccountLinkEdit()
                frm.AccountCode = SafeString(row("AccountCode"))
                frm.AccountName = SafeString(row("AccountName"))
                frm.IncludeChildren = SafeBool(row("IncludeChildren"))
                frm.AccountSignMode = SafeInt(row("AccountSignMode"))

                If frm.ShowDialog(Me) = DialogResult.OK Then

                    Dim dt As DataTable = ExecuteDataTable(
                    "dbo.ACC_IncomeStatement_UpdateAccountLink",
                    New List(Of SqlParameter) From {
                        New SqlParameter("@LinkID", linkID),
                        New SqlParameter("@IncludeChildren", frm.IncludeChildren),
                        New SqlParameter("@AccountSignMode", frm.AccountSignMode),
                        New SqlParameter("@UserID", CurrentUserID)
                    })

                    ShowProcedureMessage(dt)

                    LoadLinkedAccounts(_currentAccountLineID)
                    LoadPreview()
                    LoadValidation()

                    SetStatus("تم تعديل خصائص الربط.")
                End If
            End Using

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

#End Region

#Region "Formula Buttons"

    Private Sub btnRefreshFormula_Click(sender As Object, e As EventArgs) Handles btnRefreshFormula.Click
        Try
            LoadFormulaLines()
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnRebuildFormula_Click(sender As Object, e As EventArgs) Handles btnRebuildFormula.Click
        Try
            If dgvFormulaLines.CurrentRow Is Nothing Then
                MessageBox.Show("اختر بند معادلة أولًا.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim row As DataRowView = TryCast(dgvFormulaLines.CurrentRow.DataBoundItem, DataRowView)
            If row Is Nothing Then Return

            Dim formulaLineID As Integer = SafeInt(row("LineID"))
            Dim formulaLineCode As String = SafeString(row("LineCode"))
            Dim formulaLineName As String = SafeString(row("LineName"))

            If formulaLineID <= 0 Then
                MessageBox.Show("بند المعادلة غير صحيح.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Using frm As New FrmIncomeStatementFormulaBuilder()
                frm.TemplateID = _currentTemplateID
                frm.FormulaLineID = formulaLineID
                frm.FormulaLineCode = formulaLineCode
                frm.FormulaLineName = formulaLineName
                frm.CurrentUserID = CurrentUserID

                If frm.ShowDialog(Me) = DialogResult.OK Then
                    LoadFormulaLines()
                    LoadFormulaDetails(formulaLineID)
                    LoadPreview()
                    LoadValidation()

                    SetStatus("تم تحديث المعادلة.")
                End If
            End Using

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

#End Region

#Region "Preview And Validation"

    Private Sub btnLoadPreview_Click(sender As Object, e As EventArgs) Handles btnLoadPreview.Click
        Try
            LoadPreview()
            tabMain.SelectedTab = tabPreview
            SetStatus("تم تحميل المعاينة.")
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Try
            LoadPreview()
            tabMain.SelectedTab = tabPreview
            SetStatus("تم تحميل المعاينة.")
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnLoadValidation_Click(sender As Object, e As EventArgs) Handles btnLoadValidation.Click
        Try
            LoadValidation()
            tabMain.SelectedTab = tabValidation
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click

        Try
            LoadFinalTest()
        Catch ex As Exception
            ShowError(ex)
        End Try

        'Try
        '    LoadValidation()
        '    tabMain.SelectedTab = tabValidation
        'Catch ex As Exception
        '    ShowError(ex)
        'End Try
    End Sub

    Private Sub chkHideZero_CheckedChanged(sender As Object, e As EventArgs) Handles chkHideZero.CheckedChanged
        If _isLoading Then Return

        Try
            LoadPreview()
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

#End Region

#Region "Top Buttons"

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            LoadTemplates()

            If _currentTemplateID > 0 Then
                cboTemplates.SelectedValue = _currentTemplateID
            End If

            LoadAllTemplateData()

        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Grid Formatting"

    Private Sub ConfigureGrid(dgv As DataGridView)
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.ReadOnly = True
        dgv.MultiSelect = False
        dgv.RowHeadersVisible = False
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.BackgroundColor = Color.White
        dgv.BorderStyle = BorderStyle.FixedSingle
    End Sub

    Private Sub FormatLinesGrid(dgv As DataGridView)
        If dgv.DataSource Is Nothing Then Return

        HideColumnIfExists(dgv, "TemplateID")
        HideColumnIfExists(dgv, "ParentLineID")
        HideColumnIfExists(dgv, "SortPath")
        HideColumnIfExists(dgv, "FormulaText")
        HideColumnIfExists(dgv, "IsActive")
        HideColumnIfExists(dgv, "IsSystem")
        HideColumnIfExists(dgv, "ShowWhenZero")
        HideColumnIfExists(dgv, "IsVisible")
        HideColumnIfExists(dgv, "IsBold")

        SetHeader(dgv, "LineID", "رقم")
        SetHeader(dgv, "LineCode", "الكود")
        SetHeader(dgv, "LineName", "اسم البند")
        SetHeader(dgv, "LineTypeName", "النوع")
        SetHeader(dgv, "SortOrder", "الترتيب")
        SetHeader(dgv, "CalculationSignName", "الإشارة")
        SetHeader(dgv, "DisplaySignModeName", "العرض")
        SetHeader(dgv, "NormalBalanceSideName", "الطبيعة")
        SetHeader(dgv, "LevelNo", "المستوى")

        ApplyBoldRows(dgv)
    End Sub

    Private Sub FormatLinkedAccountsGrid()
        If dgvLinkedAccounts.DataSource Is Nothing Then Return

        HideColumnIfExists(dgvLinkedAccounts, "LineID")
        HideColumnIfExists(dgvLinkedAccounts, "ParentAccountID")
        HideColumnIfExists(dgvLinkedAccounts, "AccountNature")
        HideColumnIfExists(dgvLinkedAccounts, "CreatedBy")
        HideColumnIfExists(dgvLinkedAccounts, "UpdatedBy")

        SetHeader(dgvLinkedAccounts, "LinkID", "رقم الربط")
        SetHeader(dgvLinkedAccounts, "LineName", "البند")
        SetHeader(dgvLinkedAccounts, "AccountID", "رقم الحساب")
        SetHeader(dgvLinkedAccounts, "AccountCode", "كود الحساب")
        SetHeader(dgvLinkedAccounts, "AccountName", "اسم الحساب")
        SetHeader(dgvLinkedAccounts, "AccountNatureName", "الطبيعة")
        SetHeader(dgvLinkedAccounts, "IncludeChildrenName", "يشمل الأبناء")
        SetHeader(dgvLinkedAccounts, "AccountSignModeName", "طريقة الإشارة")

        HideColumnIfExists(dgvLinkedAccounts, "IncludeChildren")
        HideColumnIfExists(dgvLinkedAccounts, "AccountSignMode")

    End Sub

    Private Sub FormatFormulaDetailsGrid()
        If dgvFormulaDetails.DataSource Is Nothing Then Return

        HideColumnIfExists(dgvFormulaDetails, "FormulaDetailID")
        HideColumnIfExists(dgvFormulaDetails, "FormulaLineID")
        HideColumnIfExists(dgvFormulaDetails, "SourceLineID")
        HideColumnIfExists(dgvFormulaDetails, "OperationType")
        HideColumnIfExists(dgvFormulaDetails, "IsActive")

        SetHeader(dgvFormulaDetails, "FormulaLineName", "بند المعادلة")
        SetHeader(dgvFormulaDetails, "SourceLineCode", "كود المصدر")
        SetHeader(dgvFormulaDetails, "SourceLineName", "بند المصدر")
        SetHeader(dgvFormulaDetails, "OperationTypeName", "العملية")
        SetHeader(dgvFormulaDetails, "SortOrder", "الترتيب")
    End Sub

    Private Sub FormatPreviewGrid()
        If dgvPreview.DataSource Is Nothing Then Return

        HideColumnIfExists(dgvPreview, "TemplateID")
        HideColumnIfExists(dgvPreview, "ParentLineID")
        HideColumnIfExists(dgvPreview, "LineType")
        HideColumnIfExists(dgvPreview, "SortPath")
        HideColumnIfExists(dgvPreview, "CalculationSign")
        HideColumnIfExists(dgvPreview, "DisplaySignMode")
        HideColumnIfExists(dgvPreview, "NormalBalanceSide")
        HideColumnIfExists(dgvPreview, "IsVisible")
        HideColumnIfExists(dgvPreview, "ShowWhenZero")
        HideColumnIfExists(dgvPreview, "IsCalculated")

        SetHeader(dgvPreview, "LineID", "رقم")
        SetHeader(dgvPreview, "LineCode", "الكود")
        SetHeader(dgvPreview, "LineName", "البند")
        SetHeader(dgvPreview, "LineTypeName", "النوع")
        SetHeader(dgvPreview, "LevelNo", "المستوى")
        SetHeader(dgvPreview, "DisplayLineName", "العرض")
        SetHeader(dgvPreview, "CalculationSignName", "الإشارة")
        SetHeader(dgvPreview, "Amount", "المبلغ")
        SetHeader(dgvPreview, "DisplayAmount", "مبلغ العرض")
        SetHeader(dgvPreview, "DisplayAmountText", "النص")

        FormatAmountColumn(dgvPreview, "Amount")
        FormatAmountColumn(dgvPreview, "DisplayAmount")

        ApplyBoldRows(dgvPreview)
    End Sub

    Private Sub FormatValidationGrid()
        If dgvValidation.DataSource Is Nothing Then Return

        SetHeader(dgvValidation, "IssueID", "رقم")
        SetHeader(dgvValidation, "IssueType", "نوع المشكلة")
        SetHeader(dgvValidation, "IssueLevelName", "الخطورة")
        SetHeader(dgvValidation, "IssueMessage", "الرسالة")
        SetHeader(dgvValidation, "LineID", "رقم البند")
        SetHeader(dgvValidation, "LineName", "البند")
        SetHeader(dgvValidation, "AccountID", "رقم الحساب")
        SetHeader(dgvValidation, "AccountCode", "كود الحساب")
        SetHeader(dgvValidation, "AccountName", "اسم الحساب")
        SetHeader(dgvValidation, "SuggestedAction", "الإجراء المقترح")

        HideColumnIfExists(dgvValidation, "IssueLevel")

        For Each r As DataGridViewRow In dgvValidation.Rows
            If r.IsNewRow Then Continue For

            Dim levelName As String = ""
            If r.Cells("IssueLevelName") IsNot Nothing AndAlso r.Cells("IssueLevelName").Value IsNot Nothing Then
                levelName = r.Cells("IssueLevelName").Value.ToString()
            End If

            If levelName = "خطأ" Then
                r.DefaultCellStyle.BackColor = Color.MistyRose
            ElseIf levelName = "تحذير" Then
                r.DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next
    End Sub

    Private Sub ApplyBoldRows(dgv As DataGridView)
        If dgv.Columns.Contains("IsBold") = False Then Return

        For Each r As DataGridViewRow In dgv.Rows
            If r.IsNewRow Then Continue For

            Dim isBold As Boolean = False
            If r.Cells("IsBold").Value IsNot Nothing AndAlso r.Cells("IsBold").Value IsNot DBNull.Value Then
                Boolean.TryParse(r.Cells("IsBold").Value.ToString(), isBold)
            End If

            If isBold Then
                r.DefaultCellStyle.Font = New Font(dgv.Font, FontStyle.Bold)
                r.DefaultCellStyle.BackColor = Color.AliceBlue
            End If
        Next
    End Sub

    Private Sub SetHeader(dgv As DataGridView, columnName As String, headerText As String)
        If dgv.Columns.Contains(columnName) Then
            dgv.Columns(columnName).HeaderText = headerText
        End If
    End Sub

    Private Sub HideColumnIfExists(dgv As DataGridView, columnName As String)
        If dgv.Columns.Contains(columnName) Then
            dgv.Columns(columnName).Visible = False
        End If
    End Sub

    Private Sub FormatAmountColumn(dgv As DataGridView, columnName As String)
        If dgv.Columns.Contains(columnName) Then
            dgv.Columns(columnName).DefaultCellStyle.Format = "N3"
            dgv.Columns(columnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

#End Region

#Region "Helpers"

    Private Sub ClearLineDetails()
        _currentLineID = 0

        txtLineCode.Clear()
        txtLineName.Clear()
        txtSortOrder.Clear()
        txtFormulaText.Clear()

        If cboLineType.Items.Count > 0 Then cboLineType.SelectedValue = 2
        If cboCalculationSign.Items.Count > 0 Then cboCalculationSign.SelectedValue = 1
        If cboDisplaySignMode.Items.Count > 0 Then cboDisplaySignMode.SelectedValue = 1
        If cboNormalBalanceSide.Items.Count > 0 Then cboNormalBalanceSide.SelectedValue = 0

        chkIsBold.Checked = False
        chkIsVisible.Checked = True
        chkShowWhenZero.Checked = False
    End Sub

    Private Sub SetComboValue(cbo As ComboBox, value As Integer)
        Try
            cbo.SelectedValue = value
        Catch
        End Try
    End Sub

    Private Function SafeInt(value As Object) As Integer
        If value Is Nothing OrElse value Is DBNull.Value Then Return 0

        Dim result As Integer
        If Integer.TryParse(value.ToString(), result) Then
            Return result
        End If

        Return 0
    End Function

    Private Function SafeString(value As Object) As String
        If value Is Nothing OrElse value Is DBNull.Value Then Return ""
        Return value.ToString()
    End Function

    Private Function SafeBool(value As Object) As Boolean
        If value Is Nothing OrElse value Is DBNull.Value Then Return False

        Dim result As Boolean
        If Boolean.TryParse(value.ToString(), result) Then
            Return result
        End If

        Dim i As Integer
        If Integer.TryParse(value.ToString(), i) Then
            Return i <> 0
        End If

        Return False
    End Function

    Private Function DbNullIfEmpty(text As String) As Object
        If String.IsNullOrWhiteSpace(text) Then Return DBNull.Value
        Return text.Trim()
    End Function

    Private Function DbNullIfEmptyInt(text As String) As Object
        If String.IsNullOrWhiteSpace(text) Then Return DBNull.Value

        Dim result As Integer
        If Integer.TryParse(text.Trim(), result) Then
            Return result
        End If

        Return DBNull.Value
    End Function

    Private Function DbNullIfZero(value As Integer) As Object
        If value = 0 Then Return DBNull.Value
        Return value
    End Function

    Private Sub ShowProcedureMessage(dt As DataTable)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return

        If dt.Columns.Contains("MessageText") Then
            Dim msg As String = SafeString(dt.Rows(0)("MessageText"))
            SetStatus(msg)
            MessageBox.Show(msg, "النظام", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub ShowError(ex As Exception)
        SetStatus("خطأ: " & ex.Message)
        MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub SetStatus(text As String)
        lblStatus.Text = text
    End Sub


    Private Sub LoadFinalTest()
        If _currentTemplateID <= 0 Then Return

        Using con As SqlConnection = GetConnection()
            Using cmd As New SqlCommand("dbo.ACC_IncomeStatement_FinalTest", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 120

                cmd.Parameters.AddWithValue("@TemplateID", _currentTemplateID)
                cmd.Parameters.AddWithValue("@DateFrom", dtpDateFrom.Value.Date)
                cmd.Parameters.AddWithValue("@DateTo", dtpDateTo.Value.Date)
                cmd.Parameters.AddWithValue("@OnlyAccountsWithMovement", True)
                cmd.Parameters.AddWithValue("@OnlyLikelyIncomeStatementAccounts", True)

                Using da As New SqlDataAdapter(cmd)
                    Dim ds As New DataSet()
                    da.Fill(ds)

                    If ds.Tables.Count > 0 Then
                        Dim summary As DataTable = ds.Tables(0)

                        If summary.Rows.Count > 0 Then
                            Dim r As DataRow = summary.Rows(0)

                            Dim isReady As Boolean = False
                            Boolean.TryParse(r("IsReady").ToString(), isReady)

                            Dim msg As String = r("FinalMessage").ToString()
                            Dim errorCount As Integer = CInt(r("ErrorCount"))
                            Dim warningCount As Integer = CInt(r("WarningCount"))
                            Dim netIncome As Decimal = CDec(r("NET_INCOME"))

                            Dim finalMsg As String =
                                msg & Environment.NewLine & Environment.NewLine &
                                "عدد الأخطاء: " & errorCount.ToString() & Environment.NewLine &
                                "عدد التحذيرات: " & warningCount.ToString() & Environment.NewLine &
                                "صافي الدخل: " & netIncome.ToString("N3")

                            MessageBox.Show(finalMsg,
                                            "الاختبار النهائي لقائمة الدخل",
                                            MessageBoxButtons.OK,
                                            If(isReady, MessageBoxIcon.Information, MessageBoxIcon.Warning))
                        End If
                    End If

                    If ds.Tables.Count > 1 Then
                        ClearTitleRowsAmounts(ds.Tables(1))
                        dgvValidation.DataSource = ds.Tables(1)
                        FormatValidationGrid()
                        tabMain.SelectedTab = tabValidation
                    End If
                End Using
            End Using
        End Using
    End Sub


    Private Sub LoadFinalReport()
        If _currentTemplateID <= 0 Then Return

        Dim dt As DataTable = ExecuteDataTable(
            "dbo.ACC_IncomeStatement_Report",
            New List(Of SqlParameter) From {
                New SqlParameter("@TemplateID", _currentTemplateID),
                New SqlParameter("@DateFrom", dtpDateFrom.Value.Date),
                New SqlParameter("@DateTo", dtpDateTo.Value.Date),
                New SqlParameter("@HideZero", chkHideZero.Checked),
                New SqlParameter("@ShowHeader", True)
            })

        ClearTitleRowsAmounts(dt)
        dgvPreview.DataSource = dt
        FormatFinalReportGrid()

        tabMain.SelectedTab = tabPreview
        SetStatus("تم تحميل التقرير النهائي.")
    End Sub


    Private Sub ClearTitleRowsAmounts(dt As DataTable)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return

        For Each r As DataRow In dt.Rows
            If IsTitleRow(r) Then
                ClearColumnValue(r, "Amount", DBNull.Value)
                ClearColumnValue(r, "DisplayAmount", DBNull.Value)
                ClearColumnValue(r, "DisplayAmountText", "")
            End If
        Next
    End Sub


    Private Function IsTitleRow(row As DataRow) As Boolean
        If row Is Nothing OrElse row.Table Is Nothing Then Return False

        If row.Table.Columns.Contains("LineType") AndAlso SafeInt(row("LineType")) = 1 Then
            Return True
        End If

        If row.Table.Columns.Contains("LineTypeName") AndAlso SafeString(row("LineTypeName")).Trim() = "عنوان" Then
            Return True
        End If

        If row.Table.Columns.Contains("IsTitle") AndAlso SafeBool(row("IsTitle")) Then
            Return True
        End If

        Return False
    End Function


    Private Sub ClearColumnValue(row As DataRow, columnName As String, value As Object)
        If row.Table.Columns.Contains(columnName) Then
            row(columnName) = value
        End If
    End Sub


    Private Sub FormatFinalReportGrid()
        If dgvPreview.DataSource Is Nothing Then Return

        HideColumnIfExists(dgvPreview, "TemplateID")
        HideColumnIfExists(dgvPreview, "ParentLineID")
        HideColumnIfExists(dgvPreview, "LineID")
        HideColumnIfExists(dgvPreview, "LineType")
        HideColumnIfExists(dgvPreview, "SortOrder")
        HideColumnIfExists(dgvPreview, "SortPath")
        HideColumnIfExists(dgvPreview, "Amount")
        HideColumnIfExists(dgvPreview, "DisplayAmount")
        HideColumnIfExists(dgvPreview, "TextAlign")
        HideColumnIfExists(dgvPreview, "AmountAlign")

        SetHeader(dgvPreview, "RowNo", "م")
        SetHeader(dgvPreview, "TemplateName", "القالب")
        SetHeader(dgvPreview, "DateFrom", "من تاريخ")
        SetHeader(dgvPreview, "DateTo", "إلى تاريخ")
        SetHeader(dgvPreview, "LineCode", "الكود")
        SetHeader(dgvPreview, "LineName", "البند")
        SetHeader(dgvPreview, "DisplayLineName", "البيان")
        SetHeader(dgvPreview, "LineTypeName", "النوع")
        SetHeader(dgvPreview, "LevelNo", "المستوى")
        SetHeader(dgvPreview, "DisplayAmountText", "المبلغ")
        SetHeader(dgvPreview, "IsBold", "عريض")
        SetHeader(dgvPreview, "IsTitle", "عنوان")
        SetHeader(dgvPreview, "IsFormula", "معادلة")
        SetHeader(dgvPreview, "IsSeparator", "فاصل")
        SetHeader(dgvPreview, "IsNegative", "سالب")
        SetHeader(dgvPreview, "IsZero", "صفر")
        SetHeader(dgvPreview, "FontSize", "حجم الخط")

        If dgvPreview.Columns.Contains("RowNo") Then
            dgvPreview.Columns("RowNo").Width = 45
            dgvPreview.Columns("RowNo").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End If

        If dgvPreview.Columns.Contains("DisplayLineName") Then
            dgvPreview.Columns("DisplayLineName").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If

        If dgvPreview.Columns.Contains("DisplayAmountText") Then
            dgvPreview.Columns("DisplayAmountText").Width = 140
            dgvPreview.Columns("DisplayAmountText").AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvPreview.Columns("DisplayAmountText").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

        ApplyFinalReportStyle()
    End Sub


    Private Sub ApplyFinalReportStyle()
        If dgvPreview.Rows.Count = 0 Then Return

        For Each r As DataGridViewRow In dgvPreview.Rows
            If r.IsNewRow Then Continue For

            Dim isBold As Boolean = GetGridBool(r, "IsBold")
            Dim isTitle As Boolean = GetGridBool(r, "IsTitle")
            Dim isFormula As Boolean = GetGridBool(r, "IsFormula")
            Dim isSeparator As Boolean = GetGridBool(r, "IsSeparator")
            Dim isNegative As Boolean = GetGridBool(r, "IsNegative")
            Dim fontSize As Integer = GetGridInt(r, "FontSize", 9)

            Dim styleFont As FontStyle = FontStyle.Regular

            If isBold OrElse isTitle OrElse isFormula Then
                styleFont = FontStyle.Bold
            End If

            r.DefaultCellStyle.Font = New Font(dgvPreview.Font.FontFamily, fontSize, styleFont)

            If isTitle Then
                r.DefaultCellStyle.BackColor = Color.AliceBlue
            End If

            If isFormula Then
                r.DefaultCellStyle.BackColor = Color.Honeydew
            End If

            If isSeparator Then
                r.Height = 8
                r.DefaultCellStyle.BackColor = Color.LightGray
            End If

            If isNegative AndAlso dgvPreview.Columns.Contains("DisplayAmountText") Then
                r.Cells("DisplayAmountText").Style.ForeColor = Color.DarkRed
            End If
        Next
    End Sub


    Private Function GetGridBool(row As DataGridViewRow, columnName As String) As Boolean
        If Not row.DataGridView.Columns.Contains(columnName) Then Return False
        If row.Cells(columnName).Value Is Nothing OrElse row.Cells(columnName).Value Is DBNull.Value Then Return False

        Dim b As Boolean
        If Boolean.TryParse(row.Cells(columnName).Value.ToString(), b) Then Return b

        Dim i As Integer
        If Integer.TryParse(row.Cells(columnName).Value.ToString(), i) Then Return i <> 0

        Return False
    End Function

    Private Function GetGridInt(row As DataGridViewRow, columnName As String, defaultValue As Integer) As Integer
        If Not row.DataGridView.Columns.Contains(columnName) Then Return defaultValue
        If row.Cells(columnName).Value Is Nothing OrElse row.Cells(columnName).Value Is DBNull.Value Then Return defaultValue

        Dim i As Integer
        If Integer.TryParse(row.Cells(columnName).Value.ToString(), i) Then Return i

        Return defaultValue
    End Function

    Private Sub btnFinalReport_Click(sender As Object, e As EventArgs) Handles btnFinalReport.Click
        Try
            LoadFinalReport()
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub


#End Region

End Class
