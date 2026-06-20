Imports System.Data
Imports System.Data.SqlClient

Public Class FrmBudgetReleaseToSpend

    Private ReadOnly ConnStr As String = MY_Settings.SqlConStr

    '=========================
    ' Audit Context (SQL 2014)
    '=========================
    Private Sub SetUserContext(refNo As String)
        Using cn As New SqlConnection(ConnStr)
            cn.Open()
            Using cmd As New SqlCommand("
DELETE FROM dbo.User_Context WHERE SPID = @@SPID;
INSERT INTO dbo.User_Context (SPID, UserId, RefNo)
VALUES (@@SPID, @UserId, @RefNo);", cn)

                cmd.Parameters.AddWithValue("@UserId", USER_ID)
                cmd.Parameters.AddWithValue("@RefNo", refNo)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Function NewRefNo() As String
        Return "REL-" & DateTime.Now.ToString("yyyyMMdd-HHmmss")
    End Function

    '=========================
    ' Load
    '=========================
    Private Sub FrmBudgetReleaseToSpend_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplyGridStyle()
            BudgetUiHelper.ApplyBudgetFormStyle(Me)
            ApplyFinancialSummaryStyle()
            ApplyBudgetOverSpendWarning()
            EnsureAccountNameDisplays()
            LoadBeneficiaryTypes()
            LoadPaymentMethods()
            LoadFiscalYears()
            LoadDoors()
            ClearForm()
            ArrangeResizableLayout()
            SetStatus("جاهز")
        Catch ex As Exception
            SetStatus("خطأ: " & ex.Message)
        End Try
    End Sub

    Private Sub ArrangeResizableLayout()
        If cardGrid Is Nothing OrElse dgvReserves Is Nothing OrElse dgvTimeline Is Nothing Then Exit Sub

        If pnlContent IsNot Nothing AndAlso cardForm IsNot Nothing AndAlso cardSummary IsNot Nothing Then
            Dim margin As Integer = 15
            cardSummary.SetBounds(margin, 3, Math.Max(200, pnlContent.ClientSize.Width - (margin * 2)), 60)
            cardForm.SetBounds(margin, cardSummary.Bottom + 1, Math.Max(200, pnlContent.ClientSize.Width - (margin * 2)), 180)
            cardGrid.SetBounds(margin, cardForm.Bottom + 4, Math.Max(200, pnlContent.ClientSize.Width - (margin * 2)), Math.Max(220, pnlContent.ClientSize.Height - cardForm.Bottom - 8))
        End If

        ArrangeFinancialSummary()
        ArrangeEntryInputs()

        Dim padding As Integer = 3
        Dim gap As Integer = 3
        Dim gridWidth As Integer = Math.Max(200, cardGrid.ClientSize.Width - (padding * 2))
        Dim availableHeight As Integer = Math.Max(220, cardGrid.ClientSize.Height - (padding * 2) - gap)
        Dim reservesHeight As Integer = Math.Max(110, CInt(availableHeight * 0.5))
        Dim timelineHeight As Integer = Math.Max(110, availableHeight - reservesHeight)

        dgvReserves.SetBounds(padding, padding, gridWidth, reservesHeight)
        dgvTimeline.SetBounds(padding, dgvReserves.Bottom + gap, gridWidth, timelineHeight)
    End Sub

    Private Function IsBudgetOverSpendAllowed() As Boolean
        Return MY_Settings.Use_State_Budget AndAlso MY_Settings.Allow_Budget_OverSpend
    End Function

    Private Sub ApplyBudgetOverSpendWarning()
        If lblBudgetOverSpendWarning IsNot Nothing Then
            lblBudgetOverSpendWarning.Visible = IsBudgetOverSpendAllowed()
        End If
    End Sub

    Private Sub ArrangeEntryInputs()
        If cardForm Is Nothing Then Exit Sub
        EnsureAccountNameDisplays()
        RestoreCardFormDesignerSizing()
    End Sub

    Private Sub EnsureAccountNameDisplays()
        If cardForm Is Nothing Then Return

        If txtContraAccountName Is Nothing Then
            txtContraAccountName = CreateReadonlyAccountNameBox("txtContraAccountName")
            cardForm.Controls.Add(txtContraAccountName)
        End If

        If txtStampAccountName Is Nothing Then
            txtStampAccountName = CreateReadonlyAccountNameBox("txtStampAccountName")
            cardForm.Controls.Add(txtStampAccountName)
        End If

        txtContraAccountName.BringToFront()
        txtStampAccountName.BringToFront()
    End Sub

    Private Sub RestoreCardFormDesignerSizing()
        If btnPickContraAccount IsNot Nothing Then
            btnPickContraAccount.Font = New Font("Segoe UI Semibold", 9.0!, FontStyle.Bold)
            btnPickContraAccount.AutoSize = False
            btnPickContraAccount.Size = New Size(26, 24)
            btnPickContraAccount.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        End If

        If btnPickStampAccount IsNot Nothing Then
            btnPickStampAccount.Font = New Font("Segoe UI Semibold", 9.0!, FontStyle.Bold)
            btnPickStampAccount.AutoSize = False
            btnPickStampAccount.Size = New Size(26, 23)
            btnPickStampAccount.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        End If

        If txtContraAccountCode IsNot Nothing Then txtContraAccountCode.Size = New Size(208, 24)
        If txtContraAccountName IsNot Nothing Then txtContraAccountName.Size = New Size(236, 24)

        If txtStampPercent IsNot Nothing Then
            txtStampPercent.Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
            txtStampPercent.Size = New Size(43, 23)
        End If

        If txtStampAccountCode IsNot Nothing Then
            txtStampAccountCode.Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
            txtStampAccountCode.Size = New Size(91, 23)
        End If

        If txtStampAccountName IsNot Nothing Then txtStampAccountName.Size = New Size(225, 24)
        If chkHasStamp IsNot Nothing Then chkHasStamp.AutoSize = True
    End Sub

    Private Function CreateReadonlyAccountNameBox(name As String) As TextBox
        Return New TextBox With {
            .Name = name,
            .ReadOnly = True,
            .BackColor = Color.WhiteSmoke,
            .BorderStyle = BorderStyle.FixedSingle,
            .Font = New Font("Segoe UI Semibold", 9.5!, FontStyle.Bold),
            .RightToLeft = RightToLeft.Yes
        }
    End Function

    Private Sub ApplyFinancialSummaryStyle()
        If cardSummary Is Nothing Then Exit Sub

        cardSummary.BackColor = Color.FromArgb(248, 250, 252)

        For Each caption As Label In New Label() {Label4, Label3, Label2, Label1}
            If caption Is Nothing Then Continue For
            caption.AutoSize = False
            caption.Font = New Font("Segoe UI Semibold", 9.5!, FontStyle.Bold)
            caption.TextAlign = ContentAlignment.MiddleCenter
            caption.BackColor = Color.FromArgb(226, 232, 240)
            caption.ForeColor = Color.FromArgb(30, 41, 59)
            caption.BorderStyle = BorderStyle.FixedSingle
        Next

        For Each valueLabel As Label In New Label() {lblAllocated, lblSpent, lblReserved, lblAvailable}
            If valueLabel Is Nothing Then Continue For
            valueLabel.AutoSize = False
            valueLabel.Font = New Font("Segoe UI Semibold", 10.5!, FontStyle.Bold)
            valueLabel.TextAlign = ContentAlignment.MiddleCenter
            valueLabel.BackColor = Color.White
            valueLabel.BorderStyle = BorderStyle.FixedSingle
        Next

        lblAllocated.ForeColor = Color.FromArgb(21, 128, 61)
        lblSpent.ForeColor = Color.FromArgb(185, 28, 28)
        lblReserved.ForeColor = Color.FromArgb(146, 64, 14)
        lblAvailable.ForeColor = Color.FromArgb(29, 78, 216)

        ArrangeFinancialSummary()
    End Sub

    Private Sub ArrangeFinancialSummary()
        If cardSummary Is Nothing Then Exit Sub

        Dim margin As Integer = 8
        Dim gap As Integer = 8
        Dim cellWidth As Integer = Math.Max(130, CInt((cardSummary.ClientSize.Width - (margin * 2) - (gap * 3)) / 4))
        Dim titleHeight As Integer = 23
        Dim valueHeight As Integer = 27
        Dim top As Integer = 5

        Dim labels() As Label = {Label4, Label3, Label2, Label1}
        Dim values() As Label = {lblAllocated, lblSpent, lblReserved, lblAvailable}

        For i As Integer = 0 To 3
            Dim left As Integer = cardSummary.ClientSize.Width - margin - ((i + 1) * cellWidth) - (i * gap)
            If labels(i) IsNot Nothing Then labels(i).SetBounds(left, top, cellWidth, titleHeight)
            If values(i) IsNot Nothing Then values(i).SetBounds(left, top + titleHeight - 1, cellWidth, valueHeight)
        Next
    End Sub

    Private Sub ApplyGridStyle()
        dgvReserves.EnableHeadersVisualStyles = False
        dgvReserves.ColumnHeadersHeight = 38
        dgvReserves.RowTemplate.Height = 34
        dgvReserves.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        dgvReserves.DefaultCellStyle.Font = New Font("Segoe UI", 10)
        ''dgvReserves.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 237, 255)
        dgvReserves.RowHeadersVisible = False
        dgvReserves.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        '------------------------------------------------------------------------------------------------------

        dgvTimeline.EnableHeadersVisualStyles = False
        dgvTimeline.ColumnHeadersHeight = 38
        dgvTimeline.RowTemplate.Height = 34
        dgvTimeline.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        dgvTimeline.DefaultCellStyle.Font = New Font("Segoe UI", 10)
        ''dgvTimeline.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 237, 255)
        dgvTimeline.RowHeadersVisible = False
        dgvTimeline.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill



    End Sub

    Private Sub LoadBeneficiaryTypes()
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT BeneficiaryType, BeneficiaryTypeName
FROM dbo.Budget_BeneficiaryTypes
WHERE IsActive = 1
ORDER BY SortOrder, BeneficiaryType;", cn)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        cmbBeneficiaryType.DataSource = dt
        cmbBeneficiaryType.DisplayMember = "BeneficiaryTypeName"
        cmbBeneficiaryType.ValueMember = "BeneficiaryType"
        cmbBeneficiaryType.SelectedIndex = -1
    End Sub

    Private Sub LoadPaymentMethods()
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT PaymentMethodId, PaymentMethodName
FROM dbo.Budget_PaymentMethods
WHERE IsActive = 1
ORDER BY SortOrder, PaymentMethodId;", cn)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        cmbPaymentMethod.DataSource = dt
        cmbPaymentMethod.DisplayMember = "PaymentMethodName"
        cmbPaymentMethod.ValueMember = "PaymentMethodId"
        cmbPaymentMethod.SelectedIndex = -1
    End Sub

    '=========================
    ' Fiscal Years
    '=========================
    Private Sub LoadFiscalYears()
        'Dim nowY = DateTime.Now.Year
        'cmbFiscalYear.Items.Clear()
        'For y As Integer = nowY - 2 To nowY + 5
        '    cmbFiscalYear.Items.Add(y)
        'Next
        'cmbFiscalYear.SelectedItem = nowY

        cmbFiscalYear.Items.Add(Identifiers.F_YEAR)
        cmbFiscalYear.SelectedItem = Identifiers.F_YEAR
    End Sub

    Private Function SelectedYear() As Integer
        If cmbFiscalYear.SelectedItem Is Nothing Then Return 0
        Return Convert.ToInt32(cmbFiscalYear.SelectedItem)
    End Function

    '=========================
    ' Cascading Lists
    '=========================
    Private Sub LoadDoors()
        Dim dt As New DataTable()
        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT DoorId,
       DoorCode + N' - ' + DoorName AS DoorText
FROM Budget_Doors
WHERE IsActive = 1
ORDER BY DoorCode;", cn)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        cmbDoors.DataSource = dt
        cmbDoors.DisplayMember = "DoorText"
        cmbDoors.ValueMember = "DoorId"
        cmbDoors.SelectedIndex = -1

        cmbChapters.DataSource = Nothing
        cmbItems.DataSource = Nothing
    End Sub

    Private Sub LoadChapters(doorId As Integer)
        Dim dt As New DataTable()
        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT ChapterId,
       ChapterCode + N' - ' + ChapterName AS ChapterText
FROM Budget_Chapters
WHERE DoorId = @DoorId AND IsActive = 1
ORDER BY ChapterCode;", cn)

                cmd.Parameters.AddWithValue("@DoorId", doorId)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        cmbChapters.DataSource = dt
        cmbChapters.DisplayMember = "ChapterText"
        cmbChapters.ValueMember = "ChapterId"
        cmbChapters.SelectedIndex = -1

        cmbItems.DataSource = Nothing
    End Sub

    Private Sub LoadItems(chapterId As Integer)
        Dim dt As New DataTable()
        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT BudgetItemId,
       ItemCode + N' - ' + ItemName AS ItemText
FROM Budget_Items
WHERE ChapterId = @ChapterId AND IsActive = 1
ORDER BY ItemCode;", cn)

                cmd.Parameters.AddWithValue("@ChapterId", chapterId)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        cmbItems.DataSource = dt
        cmbItems.DisplayMember = "ItemText"
        cmbItems.ValueMember = "BudgetItemId"
        cmbItems.SelectedIndex = -1
    End Sub

    '=========================
    ' Budget Summary
    '=========================
    Private Sub UpdateBudgetSummary()
        If cmbItems.SelectedIndex < 0 Or cmbFiscalYear.SelectedItem Is Nothing Then
            lblAllocated.Text = "0.000 دينار"
            lblSpent.Text = "0.000 دينار"
            lblReserved.Text = "0.000 دينار"
            lblAvailable.Text = "0.000 دينار"
            Exit Sub
        End If

        Dim itemId As Integer = Convert.ToInt32(cmbItems.SelectedValue)
        Dim year As Integer = SelectedYear()

        Dim sum As BudgetSummary = GetItemBudgetSummary(itemId, year)

        lblAllocated.Text = sum.Allocated.ToString("N3") & " دينار"
        lblSpent.Text = sum.Spent.ToString("N3") & " دينار"
        lblReserved.Text = sum.Reserved.ToString("N3") & " دينار"
        lblAvailable.Text = sum.Available.ToString("N3") & " دينار"
    End Sub

    '=========================
    ' Load Reserves Grid
    '=========================
    Private Sub LoadReservesGrid(itemId As Integer, year As Integer)

        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT
    ReserveEntryId,
    ReserveDate,
    ReservedAmount,
    ReleasedAmount,
    RemainingAmount,
    Notes
FROM dbo.Vw_BudgetReserveBalance
WHERE BudgetItemId = @ItemId
  AND FiscalYear = @Y
  AND RemainingAmount > 0
ORDER BY ReserveDate;", cn)

                cmd.Parameters.AddWithValue("@ItemId", itemId)
                cmd.Parameters.AddWithValue("@Y", year)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        dgvReserves.DataSource = dt

        If dgvReserves.Columns.Count > 0 Then
            dgvReserves.Columns("ReserveEntryId").Visible = False

            dgvReserves.Columns("ReserveDate").HeaderText = "تاريخ الحجز"
            dgvReserves.Columns("ReservedAmount").HeaderText = "المبلغ المحجوز"
            dgvReserves.Columns("ReleasedAmount").HeaderText = "المبلغ المفكوك"
            dgvReserves.Columns("RemainingAmount").HeaderText = "المتبقي للحجز"
            dgvReserves.Columns("Notes").HeaderText = "ملاحظات"

            dgvReserves.Columns("ReservedAmount").DefaultCellStyle.Format = "N3"
            dgvReserves.Columns("ReleasedAmount").DefaultCellStyle.Format = "N3"
            dgvReserves.Columns("RemainingAmount").DefaultCellStyle.Format = "N3"

            dgvReserves.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgvReserves.MultiSelect = False
        End If
    End Sub

    Private Sub LoadTimelineGrid(itemId As Integer, year As Integer)

        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT
    EntryDate,
    EntryTypeName,
    SignedAmount,
    ReservedBalanceAfter,
    Notes
FROM dbo.Vw_BudgetReserveTimeline
WHERE BudgetItemId = @ItemId
  AND FiscalYear = @Y
ORDER BY EntryDate DESC;", cn)

                cmd.Parameters.AddWithValue("@ItemId", itemId)
                cmd.Parameters.AddWithValue("@Y", year)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        dgvTimeline.DataSource = dt

        If dgvTimeline.Columns.Count > 0 Then
            dgvTimeline.Columns("EntryDate").HeaderText = "التاريخ"
            dgvTimeline.Columns("EntryTypeName").HeaderText = "الحركة"
            dgvTimeline.Columns("SignedAmount").HeaderText = "قيمة الحركة"
            dgvTimeline.Columns("ReservedBalanceAfter").HeaderText = "الرصيد بعد الحركة"
            dgvTimeline.Columns("Notes").HeaderText = "ملاحظات"

            dgvTimeline.Columns("SignedAmount").DefaultCellStyle.Format = "N3"
            dgvTimeline.Columns("ReservedBalanceAfter").DefaultCellStyle.Format = "N3"
        End If
    End Sub


    Private Sub LoadTimelineForReserve(reserveEntryId As Integer)

        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT
    EntryDate,
    EntryTypeName,
    SignedAmount,
    ReservedBalanceAfter,
    ISNULL(ReserveNotes, Notes) AS DisplayNotes
FROM dbo.Vw_BudgetReserveTimeline
WHERE BudgetEntryId = @ReserveEntryId
   OR ReserveEntryId = @ReserveEntryId
ORDER BY EntryDate;
", cn)

                cmd.Parameters.AddWithValue("@ReserveEntryId", reserveEntryId)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        dgvTimeline.DataSource = dt

        If dgvTimeline.Columns.Count > 0 Then
            dgvTimeline.Columns("EntryDate").HeaderText = "التاريخ"
            dgvTimeline.Columns("EntryTypeName").HeaderText = "الحركة"
            dgvTimeline.Columns("SignedAmount").HeaderText = "قيمة الحركة"
            dgvTimeline.Columns("ReservedBalanceAfter").HeaderText = "الرصيد بعد الحركة"
            'dgvTimeline.Columns("Notes").HeaderText = "ملاحظات"

            dgvTimeline.Columns("SignedAmount").DefaultCellStyle.Format = "N3"
            dgvTimeline.Columns("ReservedBalanceAfter").DefaultCellStyle.Format = "N3"
            dgvTimeline.Columns("EntryTypeName").DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleCenter
            dgvTimeline.Columns("DisplayNotes").HeaderText = "البيان / الحجز الأصلي"

        End If
    End Sub


    Private Sub dgvReserves_SelectionChanged(sender As Object, e As EventArgs) _
    Handles dgvReserves.SelectionChanged

        If dgvReserves.CurrentRow Is Nothing Then Exit Sub

        Dim reserveEntryId As Integer =
        Convert.ToInt32(dgvReserves.CurrentRow.Cells("ReserveEntryId").Value)

        LoadTimelineForReserve(reserveEntryId)
    End Sub




    Private Sub dgvReserves_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) _
    Handles dgvReserves.CellFormatting

        If dgvReserves.Columns(e.ColumnIndex).Name = "EntryTypeName" AndAlso e.Value IsNot Nothing Then

            Select Case e.Value.ToString()
                Case "حجز"
                    e.CellStyle.BackColor = Color.SteelBlue
                    e.CellStyle.ForeColor = Color.White
                    e.CellStyle.Font = New Font(dgvReserves.Font, FontStyle.Bold)

                Case "فك حجز"
                    e.CellStyle.BackColor = Color.SeaGreen
                    e.CellStyle.ForeColor = Color.White
                    e.CellStyle.Font = New Font(dgvReserves.Font, FontStyle.Bold)
            End Select

            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If
    End Sub



    '=========================
    ' Helpers
    '=========================
    Private Sub ClearForm()
        txtAmount.Text = ""
        txtNotes.Text = ""
        If cmbBeneficiaryType IsNot Nothing Then cmbBeneficiaryType.SelectedIndex = -1
        If cmbPaymentMethod IsNot Nothing Then cmbPaymentMethod.SelectedIndex = -1
        If txtContraAccountCode IsNot Nothing Then txtContraAccountCode.Text = ""
        If txtContraAccountName IsNot Nothing Then txtContraAccountName.Text = ""
        If chkHasStamp IsNot Nothing Then chkHasStamp.Checked = False
        If txtStampPercent IsNot Nothing Then txtStampPercent.Text = ""
        If txtStampAccountCode IsNot Nothing Then txtStampAccountCode.Text = ""
        If txtStampAccountName IsNot Nothing Then txtStampAccountName.Text = ""
        UpdateStampControls()
        If txtInvoiceNo IsNot Nothing Then txtInvoiceNo.Text = ""
        If txtDocumentNo IsNot Nothing Then txtDocumentNo.Text = ""
        If txtSpendStatement IsNot Nothing Then txtSpendStatement.Text = ""
        cmbDoors.SelectedIndex = -1
        cmbChapters.DataSource = Nothing
        cmbItems.DataSource = Nothing
        dgvReserves.DataSource = Nothing
        UpdateBudgetSummary()
    End Sub

    Private Sub SetStatus(msg As String)
        lblStatus.Text = msg
    End Sub

    Private Sub UpdateStampControls()
        If txtStampPercent IsNot Nothing Then txtStampPercent.Enabled = False
        If txtStampAccountCode IsNot Nothing Then txtStampAccountCode.Enabled = False
        If txtStampAccountName IsNot Nothing Then txtStampAccountName.Enabled = False
        If btnPickStampAccount IsNot Nothing Then btnPickStampAccount.Enabled = False
    End Sub

    Private Function ApplyDefaultStampSettings(Optional showWarning As Boolean = True) As Boolean
        If chkHasStamp Is Nothing OrElse Not chkHasStamp.Checked Then Return True

        Dim stampPercent As Decimal = MY_Settings.Default_Stamp_Percent
        Dim stampAccountCode As String = If(MY_Settings.Default_Stamp_Account_Code, "").Trim()
        Dim stampAccountName As String = GetAccountName(stampAccountCode)

        If stampPercent <= 0D OrElse String.IsNullOrWhiteSpace(stampAccountCode) OrElse String.IsNullOrWhiteSpace(stampAccountName) Then
            If showWarning Then
                MessageBox.Show(
                    "لم يتم ضبط إعدادات الدمغة الافتراضية." & Environment.NewLine &
                    "يرجى ضبط نسبة الدمغة وحساب الدمغة من شاشة إدارة النظام.",
                    "إعدادات الدمغة",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning)
            End If
            Return False
        End If

        If txtStampPercent IsNot Nothing Then txtStampPercent.Text = stampPercent.ToString("0.###")
        If txtStampAccountCode IsNot Nothing Then txtStampAccountCode.Text = stampAccountCode
        If txtStampAccountName IsNot Nothing Then txtStampAccountName.Text = stampAccountName
        SetStatus("تم تطبيق الدمغة الافتراضية: " & stampPercent.ToString("0.###") & "% / " & stampAccountCode & " - " & stampAccountName)
        Return True
    End Function

    Private Function PickAccountFromBalanceSearch() As String
        ACC_CODE_Search = ""
        ACC_NAME_Search = ""

        BALANCE_SEARCH.ShowDialog()

        If String.IsNullOrWhiteSpace(ACC_CODE_Search) Then Return ""
        Return ACC_CODE_Search.Trim()
    End Function

    Private Function GetAccountName(accCode As String) As String
        If String.IsNullOrWhiteSpace(accCode) Then Return ""

        Using con As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
SELECT TOP 1 ACC_NAME
FROM dbo.ACCOUNTS_TREE
WHERE ACC_CODE = @ACC_CODE;", con)

                cmd.Parameters.Add("@ACC_CODE", SqlDbType.NVarChar, 40).Value = accCode.Trim()
                con.Open()

                Dim value = cmd.ExecuteScalar()
                If value Is Nothing OrElse value Is DBNull.Value Then Return ""
                Return value.ToString().Trim()
            End Using
        End Using
    End Function

    Private Function GridCellText(row As DataGridViewRow, columnName As String) As String
        If row Is Nothing OrElse Not row.DataGridView.Columns.Contains(columnName) Then Return ""
        Dim value = row.Cells(columnName).Value
        If value Is Nothing OrElse IsDBNull(value) Then Return ""
        Return value.ToString().Trim()
    End Function

    Private Function ComboText(cmb As ComboBox) As String
        If cmb Is Nothing OrElse cmb.SelectedIndex < 0 Then Return ""
        Return cmb.Text.Trim()
    End Function

    Private Function ShowReserveToSpendConfirmation(reserveEntryId As Integer, remaining As Decimal, amount As Decimal) As Boolean
        Dim selectedRow As DataGridViewRow = dgvReserves.CurrentRow
        Dim spendStatement As String = If(txtSpendStatement Is Nothing, "", txtSpendStatement.Text.Trim())
        Dim notes As String = If(txtNotes Is Nothing, "", txtNotes.Text.Trim())
        Dim contraAccountCode As String = If(txtContraAccountCode Is Nothing, "", txtContraAccountCode.Text.Trim())
        Dim contraAccountName As String = GetAccountName(contraAccountCode)

        Dim infoText As String =
            "رقم الحجز: " & reserveEntryId.ToString() & Environment.NewLine &
            "تاريخ الحجز: " & GridCellText(selectedRow, "ReserveDate") & Environment.NewLine &
            "المبلغ المحجوز: " & GridCellText(selectedRow, "ReservedAmount") & Environment.NewLine &
            "المبلغ المفكوك سابقا: " & GridCellText(selectedRow, "ReleasedAmount") & Environment.NewLine &
            "المتبقي من الحجز: " & remaining.ToString("N3") & Environment.NewLine &
            "مبلغ التحويل إلى صرف: " & amount.ToString("N3") & Environment.NewLine &
            "الرصيد المتبقي بعد التحويل: " & (remaining - amount).ToString("N3") & Environment.NewLine &
            "طريقة الدفع: " & ComboText(cmbPaymentMethod) & Environment.NewLine &
            "نوع المستفيد: " & ComboText(cmbBeneficiaryType) & Environment.NewLine &
            "الحساب المقابل: " & contraAccountCode & If(String.IsNullOrWhiteSpace(contraAccountName), "", " - " & contraAccountName) & Environment.NewLine &
            "رقم الفاتورة: " & If(txtInvoiceNo Is Nothing, "", txtInvoiceNo.Text.Trim()) & Environment.NewLine &
            "رقم المستند: " & If(txtDocumentNo Is Nothing, "", txtDocumentNo.Text.Trim())

        Using dlg As New Form()
            dlg.Text = "تأكيد تحويل الحجز إلى صرف"
            dlg.StartPosition = FormStartPosition.CenterParent
            dlg.FormBorderStyle = FormBorderStyle.FixedDialog
            dlg.MinimizeBox = False
            dlg.MaximizeBox = False
            dlg.ShowInTaskbar = False
            dlg.ClientSize = New Size(650, 510)
            dlg.Font = New Font("Segoe UI", 10.0!)
            dlg.RightToLeft = RightToLeft.Yes
            dlg.RightToLeftLayout = True

            Dim lblInfoTitle As New Label()
            lblInfoTitle.Text = "تفاصيل التحويل قبل التنفيذ"
            lblInfoTitle.Font = New Font("Segoe UI Semibold", 11.0!, FontStyle.Bold)
            lblInfoTitle.ForeColor = Color.FromArgb(30, 41, 59)
            lblInfoTitle.SetBounds(20, 15, 610, 28)

            Dim txtInfo As New TextBox()
            txtInfo.Multiline = True
            txtInfo.ReadOnly = True
            txtInfo.ScrollBars = ScrollBars.Vertical
            txtInfo.BackColor = Color.FromArgb(248, 250, 252)
            txtInfo.BorderStyle = BorderStyle.FixedSingle
            txtInfo.Font = New Font("Segoe UI Semibold", 9.75!, FontStyle.Bold)
            txtInfo.Text = infoText
            txtInfo.SetBounds(20, 48, 610, 210)

            Dim lblStatement As New Label()
            lblStatement.Text = "بيان الصرف الذي سيتم حفظه"
            lblStatement.Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)
            lblStatement.SetBounds(20, 270, 610, 24)

            Dim txtStatementPreview As New TextBox()
            txtStatementPreview.Multiline = True
            txtStatementPreview.ReadOnly = True
            txtStatementPreview.ScrollBars = ScrollBars.Vertical
            txtStatementPreview.BorderStyle = BorderStyle.FixedSingle
            txtStatementPreview.BackColor = Color.White
            txtStatementPreview.Font = New Font("Segoe UI Semibold", 10.0!, FontStyle.Bold)
            txtStatementPreview.Text = If(String.IsNullOrWhiteSpace(spendStatement), "بدون بيان صرف", spendStatement)
            txtStatementPreview.SetBounds(20, 298, 610, 82)

            Dim lblNotesPreview As New Label()
            lblNotesPreview.Text = "الملاحظات: " & If(String.IsNullOrWhiteSpace(notes), "بدون ملاحظات", notes)
            lblNotesPreview.AutoEllipsis = True
            lblNotesPreview.BorderStyle = BorderStyle.FixedSingle
            lblNotesPreview.BackColor = Color.FromArgb(255, 251, 235)
            lblNotesPreview.Font = New Font("Segoe UI", 9.75!, FontStyle.Bold)
            lblNotesPreview.TextAlign = ContentAlignment.MiddleRight
            lblNotesPreview.SetBounds(20, 390, 610, 42)

            Dim btnConfirm As New Button()
            btnConfirm.Text = "تأكيد التحويل"
            btnConfirm.Font = New Font("Segoe UI Semibold", 10.0!, FontStyle.Bold)
            btnConfirm.DialogResult = DialogResult.OK
            btnConfirm.SetBounds(410, 452, 120, 36)

            Dim btnCancel As New Button()
            btnCancel.Text = "إلغاء"
            btnCancel.Font = New Font("Segoe UI Semibold", 10.0!, FontStyle.Bold)
            btnCancel.DialogResult = DialogResult.Cancel
            btnCancel.SetBounds(540, 452, 90, 36)

            dlg.Controls.Add(lblInfoTitle)
            dlg.Controls.Add(txtInfo)
            dlg.Controls.Add(lblStatement)
            dlg.Controls.Add(txtStatementPreview)
            dlg.Controls.Add(lblNotesPreview)
            dlg.Controls.Add(btnConfirm)
            dlg.Controls.Add(btnCancel)
            dlg.AcceptButton = btnConfirm
            dlg.CancelButton = btnCancel

            Return dlg.ShowDialog(Me) = DialogResult.OK
        End Using
    End Function

    Private Sub SaveSpendEntryStampInfo(spendEntryId As Integer, amount As Decimal)
        Dim hasStamp As Boolean = (chkHasStamp IsNot Nothing AndAlso chkHasStamp.Checked)
        Dim stampPercentValue As Decimal = 0D
        Dim stampAmountValue As Decimal = 0D
        Dim stampPercent As Object = DBNull.Value
        Dim stampAccountCode As Object = DBNull.Value
        Dim stampAmount As Object = DBNull.Value

        If hasStamp Then
            If Not ApplyDefaultStampSettings(False) Then
                Throw New Exception("اضبط إعدادات الدمغة الافتراضية من شاشة إدارة النظام قبل استخدامها.")
            End If

            Decimal.TryParse(txtStampPercent.Text.Trim(), stampPercentValue)
            stampAmountValue = Math.Round((amount * stampPercentValue) / 100D, 3)
            stampPercent = stampPercentValue
            stampAmount = stampAmountValue
            stampAccountCode = txtStampAccountCode.Text.Trim()
        End If

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
UPDATE dbo.Budget_Entries
SET HasStamp = @HasStamp,
    StampPercent = @StampPercent,
    StampAccountCode = @StampAccountCode,
    StampAmount = @StampAmount
WHERE BudgetEntryId = @BudgetEntryId
  AND AccountingEntryId IS NULL;", cn)

                cmd.Parameters.Add("@BudgetEntryId", SqlDbType.Int).Value = spendEntryId
                cmd.Parameters.Add("@HasStamp", SqlDbType.Bit).Value = hasStamp
                cmd.Parameters.Add("@StampPercent", SqlDbType.Decimal).Value = stampPercent
                cmd.Parameters("@StampPercent").Precision = 18
                cmd.Parameters("@StampPercent").Scale = 3
                cmd.Parameters.Add("@StampAccountCode", SqlDbType.NVarChar, 40).Value = stampAccountCode
                cmd.Parameters.Add("@StampAmount", SqlDbType.Decimal).Value = stampAmount
                cmd.Parameters("@StampAmount").Precision = 18
                cmd.Parameters("@StampAmount").Scale = 3

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Function ValidateForm() As Boolean

        If dgvReserves.CurrentRow Is Nothing Then
            MessageBox.Show("اختر حجزًا أولًا", "تنبيه")
            Return False
        End If

        If cmbFiscalYear.SelectedItem Is Nothing Then
            MessageBox.Show("اختر السنة المالية", "تنبيه")
            Return False
        End If

        If cmbItems.SelectedIndex < 0 Then
            MessageBox.Show("اختر بند الموازنة", "تنبيه")
            Return False
        End If

        Dim amt As Decimal
        If Not Decimal.TryParse(txtAmount.Text.Trim(), amt) OrElse amt <= 0D Then
            MessageBox.Show("مبلغ التحويل غير صحيح", "تنبيه")
            Return False
        End If

        Dim sum As BudgetSummary = GetItemBudgetSummary(Convert.ToInt32(cmbItems.SelectedValue), SelectedYear())
        If sum.Reserved < amt Then
            MessageBox.Show("المبلغ أكبر من الرصيد المحجوز", "رفض العملية")
            Return False
        End If


        Dim remaining As Decimal = 0D

        If dgvReserves.CurrentRow.Cells("RemainingAmount").Value Is Nothing OrElse
   IsDBNull(dgvReserves.CurrentRow.Cells("RemainingAmount").Value) OrElse
   Not Decimal.TryParse(dgvReserves.CurrentRow.Cells("RemainingAmount").Value.ToString(), remaining) Then

            MessageBox.Show("لا يمكن قراءة المتبقي من الحجز المحدد", "تنبيه")
            Return False
        End If

        If amt > remaining Then
            MessageBox.Show("المبلغ أكبر من المتبقي في هذا الحجز", "رفض العملية")
            Return False
        End If

        If cmbBeneficiaryType.SelectedIndex < 0 Then
            MessageBox.Show("اختر نوع المستفيد", "تنبيه")
            cmbBeneficiaryType.Focus()
            Return False
        End If

        If cmbPaymentMethod.SelectedIndex < 0 Then
            MessageBox.Show("اختر طريقة الدفع", "تنبيه")
            cmbPaymentMethod.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtContraAccountCode.Text) Then
            MessageBox.Show("حدد الحساب المقابل", "تنبيه")
            txtContraAccountCode.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(GetAccountName(txtContraAccountCode.Text.Trim())) Then
            MessageBox.Show("الحساب المقابل غير موجود في شجرة الحسابات", "تنبيه")
            txtContraAccountCode.Focus()
            Return False
        End If

        If chkHasStamp IsNot Nothing AndAlso chkHasStamp.Checked Then
            If Not ApplyDefaultStampSettings(False) Then
                MessageBox.Show("اضبط إعدادات الدمغة الافتراضية من شاشة إدارة النظام قبل استخدامها.", "تنبيه")
                Return False
            End If

            Dim stampPercent As Decimal
            If txtStampPercent Is Nothing OrElse Not Decimal.TryParse(txtStampPercent.Text.Trim(), stampPercent) OrElse stampPercent <= 0D Then
                MessageBox.Show("أدخل نسبة الدمغة بشكل صحيح", "تنبيه")
                If txtStampPercent IsNot Nothing Then txtStampPercent.Focus()
                Return False
            End If

            Dim stampAmount As Decimal = Math.Round((amt * stampPercent) / 100D, 3)
            If stampAmount <= 0D OrElse stampAmount >= amt Then
                MessageBox.Show("قيمة الدمغة يجب أن تكون أكبر من صفر وأقل من مبلغ الصرف", "تنبيه")
                If txtStampPercent IsNot Nothing Then txtStampPercent.Focus()
                Return False
            End If

            If txtStampAccountCode Is Nothing OrElse String.IsNullOrWhiteSpace(txtStampAccountCode.Text) Then
                MessageBox.Show("حدد حساب الدمغة", "تنبيه")
                If btnPickStampAccount IsNot Nothing Then btnPickStampAccount.Focus()
                Return False
            End If

            If String.IsNullOrWhiteSpace(GetAccountName(txtStampAccountCode.Text.Trim())) Then
                MessageBox.Show("حساب الدمغة غير موجود في شجرة الحسابات", "تنبيه")
                txtStampAccountCode.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    '=========================
    ' Convert Reserve → Spend
    '=========================

    Private Sub ConvertReserveToSpend()
        If Not ValidateForm() Then Exit Sub

        Dim reserveEntryId As Integer =
        Convert.ToInt32(dgvReserves.CurrentRow.Cells("ReserveEntryId").Value)

        Dim remaining As Decimal =
        Convert.ToDecimal(dgvReserves.CurrentRow.Cells("RemainingAmount").Value)

        Dim amt As Decimal
        If Not Decimal.TryParse(txtAmount.Text.Trim(), amt) OrElse amt <= 0D Then
            MessageBox.Show("مبلغ التحويل غير صحيح", "تنبيه")
            Exit Sub
        End If

        If amt > remaining Then
            MessageBox.Show("المبلغ أكبر من المتبقي في هذا الحجز", "رفض العملية")
            Exit Sub
        End If

        If Not ShowReserveToSpendConfirmation(reserveEntryId, remaining, amt) Then
            SetStatus("تم إلغاء التحويل قبل التنفيذ")
            Exit Sub
        End If

        Dim refNo = NewRefNo()
        SetUserContext(refNo)

        Dim releaseEntryId As Integer = 0
        Dim spendEntryId As Integer = 0
        Dim msg As String = ""

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("dbo.Budget_Reserve_ConvertToSpend", cn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@ReserveEntryId", reserveEntryId)
                cmd.Parameters.AddWithValue("@Amount", amt)
                cmd.Parameters.AddWithValue("@SpendNotes", If(txtNotes.Text Is Nothing, "", txtNotes.Text.Trim()))

                cmd.Parameters.Add("@BeneficiaryType", SqlDbType.TinyInt).Value =
                    If(cmbBeneficiaryType.SelectedIndex >= 0, cmbBeneficiaryType.SelectedValue, DBNull.Value)

                cmd.Parameters.Add("@BeneficiaryId", SqlDbType.Int).Value = DBNull.Value

                cmd.Parameters.Add("@ContraAccountCode", SqlDbType.NVarChar, 40).Value =
                    If(String.IsNullOrWhiteSpace(txtContraAccountCode.Text), DBNull.Value, txtContraAccountCode.Text.Trim())

                cmd.Parameters.Add("@PaymentMethodId", SqlDbType.TinyInt).Value =
                    If(cmbPaymentMethod.SelectedIndex >= 0, cmbPaymentMethod.SelectedValue, DBNull.Value)

                cmd.Parameters.Add("@InvoiceNo", SqlDbType.NVarChar, 50).Value =
                    If(String.IsNullOrWhiteSpace(txtInvoiceNo.Text), DBNull.Value, txtInvoiceNo.Text.Trim())

                cmd.Parameters.Add("@DocumentNo", SqlDbType.NVarChar, 50).Value =
                    If(String.IsNullOrWhiteSpace(txtDocumentNo.Text), DBNull.Value, txtDocumentNo.Text.Trim())

                cmd.Parameters.Add("@SpendStatement", SqlDbType.NVarChar, 500).Value =
                    If(String.IsNullOrWhiteSpace(txtSpendStatement.Text), DBNull.Value, txtSpendStatement.Text.Trim())

                cmd.Parameters.AddWithValue("@CreatedBy", USER_ID)

                Dim pRelease As New SqlParameter("@ReleaseEntryId", SqlDbType.Int)
                pRelease.Direction = ParameterDirection.Output
                cmd.Parameters.Add(pRelease)

                Dim pSpend As New SqlParameter("@SpendEntryId", SqlDbType.Int)
                pSpend.Direction = ParameterDirection.Output
                cmd.Parameters.Add(pSpend)

                Dim pMsg As New SqlParameter("@Msg", SqlDbType.NVarChar, 300)
                pMsg.Direction = ParameterDirection.Output
                cmd.Parameters.Add(pMsg)

                cn.Open()
                cmd.ExecuteNonQuery()

                If pRelease.Value IsNot DBNull.Value Then releaseEntryId = Convert.ToInt32(pRelease.Value)
                If pSpend.Value IsNot DBNull.Value Then spendEntryId = Convert.ToInt32(pSpend.Value)
                msg = If(pMsg.Value Is Nothing OrElse pMsg.Value Is DBNull.Value, "", pMsg.Value.ToString())
            End Using
        End Using

        If spendEntryId > 0 Then
            SaveSpendEntryStampInfo(spendEntryId, amt)
        End If

        MessageBox.Show(msg, "تحويل الحجز", MessageBoxButtons.OK, MessageBoxIcon.Information)

        If cmbItems.SelectedIndex >= 0 Then
            Dim itemId As Integer = Convert.ToInt32(cmbItems.SelectedValue)
            Dim year As Integer = SelectedYear()

            LoadReservesGrid(itemId, year)
            UpdateBudgetSummary()

            If reserveEntryId > 0 Then
                LoadTimelineForReserve(reserveEntryId)
            End If
        End If

        txtAmount.Text = ""
        txtNotes.Text = ""
        txtAmount.Focus()

        If spendEntryId > 0 Then
            SetStatus("تم التحويل إلى صرف رقم " & spendEntryId)
        Else
            SetStatus("لم يتم التحويل")
        End If
    End Sub



    '    Private Sub ConvertReserveToSpend()
    '        If Not ValidateForm() Then Exit Sub


    '        Dim reserveEntryId As Integer =
    '        Convert.ToInt32(dgvReserves.CurrentRow.Cells("ReserveEntryId").Value)

    '        Dim remaining As Decimal =
    '        Convert.ToDecimal(dgvReserves.CurrentRow.Cells("RemainingAmount").Value)

    '        Dim amt As Decimal
    '        If Not Decimal.TryParse(txtAmount.Text.Trim(), amt) OrElse amt <= 0D Then
    '            MessageBox.Show("مبلغ التحويل غير صحيح", "تنبيه")
    '            Exit Sub
    '        End If

    '        If amt > remaining Then
    '            MessageBox.Show("المبلغ أكبر من المتبقي في هذا الحجز", "رفض العملية")
    '            Exit Sub
    '        End If


    '        Dim itemId As Integer = Convert.ToInt32(cmbItems.SelectedValue)
    '        Dim year As Integer = SelectedYear()


    '        Decimal.TryParse(txtAmount.Text.Trim(), amt)

    '        Dim refNo = NewRefNo()
    '        SetUserContext(refNo)

    '        Using cn As New SqlConnection(ConnStr)
    '            cn.Open()

    '            Using tr = cn.BeginTransaction()
    '                Try
    '                    '1️⃣ فك حجز (مرتبط بالحجز الأصلي)
    '                    Using cmdRelease As New SqlCommand("
    'INSERT INTO Budget_Entries
    '(BudgetItemId, FiscalYear, Amount, EntryType, ReserveEntryId, EntryDate, Notes)
    'VALUES
    '(@ItemId, @Y, @Amt, 3, @ReserveEntryId, GETDATE(), @Notes);", cn, tr)

    '                        cmdRelease.Parameters.AddWithValue("@ItemId", itemId)
    '                        cmdRelease.Parameters.AddWithValue("@Y", year)
    '                        cmdRelease.Parameters.AddWithValue("@Amt", amt)
    '                        cmdRelease.Parameters.AddWithValue("@ReserveEntryId", reserveEntryId)
    '                        cmdRelease.Parameters.AddWithValue("@Notes",
    '                        "فك حجز من الحجز رقم " & reserveEntryId & " - " & txtNotes.Text)

    '                        cmdRelease.ExecuteNonQuery()
    '                    End Using

    '                    '2️⃣ صرف مقابل الحجز
    '                    Using cmdSpend As New SqlCommand("
    'INSERT INTO Budget_Entries
    '(BudgetItemId, FiscalYear, Amount, EntryType, EntryDate, Notes)
    'VALUES
    '(@ItemId, @Y, @Amt, 1, GETDATE(), @Notes);", cn, tr)

    '                        cmdSpend.Parameters.AddWithValue("@ItemId", itemId)
    '                        cmdSpend.Parameters.AddWithValue("@Y", year)
    '                        cmdSpend.Parameters.AddWithValue("@Amt", amt)
    '                        cmdSpend.Parameters.AddWithValue("@Notes",
    '                        "صرف مقابل الحجز رقم " & reserveEntryId & " - " & txtNotes.Text)

    '                        cmdSpend.ExecuteNonQuery()
    '                    End Using

    '                    tr.Commit()

    '                Catch
    '                    tr.Rollback()
    '                    Throw
    '                End Try
    '            End Using
    '        End Using

    '        '🔄 تحديث الواجهات
    '        LoadReservesGrid(itemId, year)
    '        UpdateBudgetSummary()

    '        If dgvReserves.CurrentRow IsNot Nothing Then
    '            Dim newReserveId =
    '            Convert.ToInt32(dgvReserves.CurrentRow.Cells("ReserveEntryId").Value)
    '            LoadTimelineForReserve(newReserveId)
    '        End If

    '        txtAmount.Text = ""
    '        txtNotes.Text = ""

    '        SetStatus("تم تحويل الحجز إلى صرف بنجاح")
    '    End Sub

    '=========================
    ' Events
    '=========================
    Private Sub cmbDoors_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbDoors.SelectionChangeCommitted
        LoadChapters(Convert.ToInt32(cmbDoors.SelectedValue))
    End Sub

    Private Sub cmbChapters_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbChapters.SelectionChangeCommitted
        LoadItems(Convert.ToInt32(cmbChapters.SelectedValue))
    End Sub

    Private Sub cmbItems_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbItems.SelectionChangeCommitted
        LoadReservesGrid(Convert.ToInt32(cmbItems.SelectedValue), SelectedYear())
        UpdateBudgetSummary()
    End Sub

    Private Sub cmbFiscalYear_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbFiscalYear.SelectionChangeCommitted
        If cmbItems.SelectedIndex >= 0 Then
            LoadReservesGrid(Convert.ToInt32(cmbItems.SelectedValue), SelectedYear())
            UpdateBudgetSummary()
        End If
    End Sub

    Private Sub btnConvert_Click(sender As Object, e As EventArgs) Handles btnConvert.Click
        Try
            ConvertReserveToSpend()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ")
            SetStatus("فشل التحويل")
        End Try
    End Sub

    Private Sub btnPickContraAccount_Click(sender As Object, e As EventArgs) Handles btnPickContraAccount.Click
        Dim accountCode As String = PickAccountFromBalanceSearch()
        If Not String.IsNullOrWhiteSpace(accountCode) Then
            txtContraAccountCode.Text = accountCode
            If txtContraAccountName IsNot Nothing Then txtContraAccountName.Text = GetAccountName(accountCode)
        End If
    End Sub

    Private Sub chkHasStamp_CheckedChanged(sender As Object, e As EventArgs) Handles chkHasStamp.CheckedChanged
        If chkHasStamp.Checked Then
            If Not ApplyDefaultStampSettings(True) Then
                chkHasStamp.Checked = False
                Return
            End If
        Else
            If txtStampPercent IsNot Nothing Then txtStampPercent.Text = ""
            If txtStampAccountCode IsNot Nothing Then txtStampAccountCode.Text = ""
            If txtStampAccountName IsNot Nothing Then txtStampAccountName.Text = ""
        End If
        UpdateStampControls()
    End Sub

    Private Sub btnPickStampAccount_Click(sender As Object, e As EventArgs) Handles btnPickStampAccount.Click
        MessageBox.Show("حساب الدمغة يحدد من شاشة إدارة النظام فقط.", "إعدادات الدمغة", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub txtContraAccountCode_Leave(sender As Object, e As EventArgs) Handles txtContraAccountCode.Leave
        If txtContraAccountName IsNot Nothing Then txtContraAccountName.Text = GetAccountName(txtContraAccountCode.Text)
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        If cmbItems.SelectedIndex >= 0 Then
            LoadReservesGrid(Convert.ToInt32(cmbItems.SelectedValue), SelectedYear())
            UpdateBudgetSummary()
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub FrmBudgetReleaseToSpend_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ArrangeResizableLayout()
    End Sub



    Private Sub txtAmount_Leave(sender As Object, e As EventArgs) Handles txtAmount.Leave
        Dim amt As Decimal
        If Decimal.TryParse(txtAmount.Text.Trim(), amt) Then
            txtAmount.Text = amt.ToString("N3")
        End If
    End Sub

    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged
        UpdateAmountWords()
    End Sub

    Private Sub UpdateAmountWords()
        If txtAmountWords Is Nothing Then Exit Sub

        Dim amount As Decimal = 0D
        If BudgetUiHelper.TryParseMoneyText(txtAmount.Text, amount) AndAlso amount > 0D Then
            txtAmountWords.Text = HANY(amount, "LYD")
        Else
            txtAmountWords.Text = ""
        End If
    End Sub

    Private Sub cardForm_Paint(sender As Object, e As PaintEventArgs) Handles cardForm.Paint

    End Sub
End Class
