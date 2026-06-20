Imports System.Data
Imports System.Data.SqlClient

Public Class FrmBudgetAllocations

    Private ReadOnly ConnStr As String = MY_Settings.SqlConStr
    'Private CurrentAllocationId As Integer = 0
    Private CurrentMovementId As Integer = 0

    Private Function NewRefNo() As String
        Return "ALC-" & DateTime.Now.ToString("yyyyMMdd-HHmmss")
    End Function

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

    '=========================
    ' Load
    '=========================
    Private Sub FrmBudgetAllocations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplyGridStyle()

            LoadFiscalYears()
            LoadAllocationTypes()
            LoadProviders()
            LoadOriginalFiscalYears()
            LoadDoors()

            ClearForm()
            LoadAllocationsGrid(SelectedYear())

            SetStatus("جاهز")
        Catch ex As Exception
            SetStatus("خطأ: " & ex.Message)
        End Try
        '------------------------------------------------------------
        'Try
        '    ApplyGridStyle()
        '    BudgetUiHelper.ApplyBudgetFormStyle(Me)
        '    LoadFiscalYears()
        '    LoadDoors()
        '    ClearForm()
        '    SetStatus("جاهز")
        'Catch ex As Exception
        '    SetStatus("خطأ: " & ex.Message)
        'End Try
    End Sub

    Private Sub LoadAllocationTypes()
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT AllocationTypeId, TypeName
FROM dbo.Budget_AllocationTypes
WHERE IsActive = 1
  AND AllocationTypeId NOT IN (4, 5)
ORDER BY AllocationTypeId;", cn)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        cmbAllocationType.DataSource = dt
        cmbAllocationType.DisplayMember = "TypeName"
        cmbAllocationType.ValueMember = "AllocationTypeId"
        cmbAllocationType.SelectedIndex = -1
    End Sub

    Private Sub LoadProviders()
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT ProviderId, ProviderName
FROM dbo.Budget_FundingProviders
WHERE IsActive = 1
ORDER BY ProviderName;", cn)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        cmbProvider.DataSource = dt
        cmbProvider.DisplayMember = "ProviderName"
        cmbProvider.ValueMember = "ProviderId"
        cmbProvider.SelectedIndex = -1
    End Sub

    Private Sub LoadOriginalFiscalYears()
        cmbOriginalFiscalYear.Items.Clear()

        Dim currentYear As Integer = Identifiers.F_YEAR

        For y As Integer = currentYear - 10 To currentYear
            cmbOriginalFiscalYear.Items.Add(y)
        Next

        cmbOriginalFiscalYear.SelectedIndex = -1
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

    Private Sub ApplyGridStyle()
        dgvAllocations.EnableHeadersVisualStyles = False
        dgvAllocations.ColumnHeadersHeight = 38
        dgvAllocations.RowTemplate.Height = 34
        dgvAllocations.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        dgvAllocations.DefaultCellStyle.Font = New Font("Segoe UI", 10)
        dgvAllocations.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 237, 255)
        dgvAllocations.RowHeadersVisible = False
        dgvAllocations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        dgvAllocations.ScrollBars = ScrollBars.Both
        dgvAllocations.DefaultCellStyle.WrapMode = DataGridViewTriState.False
    End Sub

    '=========================
    ' Data Loaders
    '=========================
    Private Sub LoadFiscalYears()
        '✅ بدون جدول: نولّد قائمة سنوات (من السنة الحالية -2 إلى +5)
        'Dim nowY = DateTime.Now.Year
        cmbFiscalYear.Items.Clear()

        'For y As Integer = nowY - 2 To nowY + 5
        '    cmbFiscalYear.Items.Add(y)
        'Next

        'cmbFiscalYear.SelectedItem = nowY

        '----------------------------------------------------------------------------------------------------------------------------------------------------

        'Dim DT As New DataTable
        'Dim C As New C
        'Dim da As New SqlClient.SqlDataAdapter(" select YEAR_ID,is_Close  from YEARS WHERE is_Close = 0 ORDER BY YEAR_ID DESC", C.Con)
        'da.Fill(DT)

        'cmbFiscalYear.DataSource = DT
        'cmbFiscalYear.DisplayMember = "YEAR_ID"
        'cmbFiscalYear.ValueMember = "is_Close"

        'If DT.Rows.Count > 0 Then
        cmbFiscalYear.Items.Add(Identifiers.F_YEAR)
        cmbFiscalYear.SelectedItem = Identifiers.F_YEAR
        'cmbFiscalYear.Text = Identifiers.F_YEAR
        'End If
    End Sub

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

    Private Sub LoadAllocationsGrid(Optional fiscalYear As Integer = 0,
                                Optional doorId As Integer = 0,
                                Optional chapterId As Integer = 0,
                                Optional itemId As Integer = 0)

        Dim dt As New DataTable()

        Dim sql As String =
"SELECT
    m.AllocationMovementId,
    m.FiscalYear,
    m.BudgetItemId,
    d.DoorCode,
    d.DoorName,
    c.ChapterCode,
    c.ChapterName,
    i.ItemCode,
    i.ItemName,
    m.AllocationTypeId,
    t.TypeName,
    m.Amount,
    SignedAmount =
        CASE
            WHEN t.Direction = 1 THEN m.Amount
            WHEN t.Direction = -1 THEN -m.Amount
            ELSE 0
        END,
    m.ProviderId,
    ProviderName = fp.ProviderName,
    m.MovementDate,
    m.DecisionNo,
    m.DecisionDate,
    m.OriginalFiscalYear,
    m.EmergencyReason,
    m.Reason,
    m.StatusId,
    StatusName =
        CASE m.StatusId
            WHEN 0 THEN N'مسودة'
            WHEN 1 THEN N'معتمد'
            WHEN 2 THEN N'ملغي'
            WHEN 3 THEN N'مرفوض'
            WHEN 4 THEN N'مقفل'
            WHEN 5 THEN N'مرحل / منتهي'
            ELSE N'غير معروف'
        END
FROM dbo.Budget_AllocationMovements m
JOIN dbo.Budget_AllocationTypes t ON t.AllocationTypeId = m.AllocationTypeId
JOIN dbo.Budget_Items i ON m.BudgetItemId = i.BudgetItemId
JOIN dbo.Budget_Chapters c ON i.ChapterId = c.ChapterId
JOIN dbo.Budget_Doors d ON c.DoorId = d.DoorId
LEFT JOIN dbo.Budget_FundingProviders fp ON fp.ProviderId = m.ProviderId
WHERE (@Y = 0 OR m.FiscalYear = @Y)
  AND (@DoorId = 0 OR d.DoorId = @DoorId)
  AND (@ChapterId = 0 OR c.ChapterId = @ChapterId)
  AND (@ItemId = 0 OR i.BudgetItemId = @ItemId)
  AND m.AllocationTypeId NOT IN (4,5)
ORDER BY m.FiscalYear DESC, m.MovementDate DESC, m.AllocationMovementId DESC;"

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@Y", fiscalYear)
                cmd.Parameters.AddWithValue("@DoorId", doorId)
                cmd.Parameters.AddWithValue("@ChapterId", chapterId)
                cmd.Parameters.AddWithValue("@ItemId", itemId)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        dgvAllocations.DataSource = dt
        FormatAllocationsGrid()

        dgvAllocations.ClearSelection()
        SetStatus($"تم تحميل {dt.Rows.Count} حركة اعتماد")
    End Sub

    Private Sub FormatAllocationsGrid()
        If dgvAllocations.Columns.Count = 0 Then Return

        HideColumn("AllocationMovementId")
        HideColumn("BudgetItemId")
        HideColumn("AllocationTypeId")
        HideColumn("ProviderId")
        HideColumn("StatusId")

        SetHeader("FiscalYear", "السنة")
        SetHeader("DoorCode", "كود الباب")
        SetHeader("DoorName", "الباب")
        SetHeader("ChapterCode", "كود الفصل")
        SetHeader("ChapterName", "الفصل")
        SetHeader("ItemCode", "كود البند")
        SetHeader("ItemName", "البند")
        SetHeader("TypeName", "نوع الاعتماد")
        SetHeader("Amount", "القيمة")
        SetHeader("SignedAmount", "الأثر")
        SetHeader("ProviderName", "جهة الاعتماد")
        SetHeader("MovementDate", "تاريخ الحركة")
        SetHeader("DecisionNo", "رقم القرار")
        SetHeader("DecisionDate", "تاريخ القرار")
        SetHeader("OriginalFiscalYear", "السنة الأصلية")
        SetHeader("EmergencyReason", "سبب الطارئ")
        SetHeader("Reason", "البيان")
        SetHeader("StatusName", "الحالة")

        MoneyColumn("Amount")
        MoneyColumn("SignedAmount")

        If dgvAllocations.Columns.Contains("MovementDate") Then
            dgvAllocations.Columns("MovementDate").DefaultCellStyle.Format = "yyyy-MM-dd"
        End If

        If dgvAllocations.Columns.Contains("DecisionDate") Then
            dgvAllocations.Columns("DecisionDate").DefaultCellStyle.Format = "yyyy-MM-dd"
        End If

        SetColumnWidth("FiscalYear", 75)
        SetColumnWidth("DoorCode", 85)
        SetColumnWidth("DoorName", 160)
        SetColumnWidth("ChapterCode", 85)
        SetColumnWidth("ChapterName", 160)
        SetColumnWidth("ItemCode", 95)
        SetColumnWidth("ItemName", 230)
        SetColumnWidth("TypeName", 140)
        SetColumnWidth("Amount", 120)
        SetColumnWidth("SignedAmount", 120)
        SetColumnWidth("ProviderName", 160)
        SetColumnWidth("MovementDate", 110)
        SetColumnWidth("DecisionNo", 120)
        SetColumnWidth("DecisionDate", 110)
        SetColumnWidth("OriginalFiscalYear", 110)
        SetColumnWidth("EmergencyReason", 220)
        SetColumnWidth("Reason", 260)
        SetColumnWidth("StatusName", 110)
    End Sub

    Private Sub HideColumn(columnName As String)
        If dgvAllocations.Columns.Contains(columnName) Then
            dgvAllocations.Columns(columnName).Visible = False
        End If
    End Sub

    Private Sub SetHeader(columnName As String, headerText As String)
        If dgvAllocations.Columns.Contains(columnName) Then
            dgvAllocations.Columns(columnName).HeaderText = headerText
        End If
    End Sub

    Private Sub MoneyColumn(columnName As String)
        If dgvAllocations.Columns.Contains(columnName) Then
            dgvAllocations.Columns(columnName).DefaultCellStyle.Format = "N3"
            dgvAllocations.Columns(columnName).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub SetColumnWidth(columnName As String, width As Integer)
        If dgvAllocations.Columns.Contains(columnName) Then
            dgvAllocations.Columns(columnName).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvAllocations.Columns(columnName).Width = width
        End If
    End Sub

    '    Private Sub LoadAllocationsGrid(Optional fiscalYear As Integer = 0, Optional doorId As Integer = 0, Optional chapterId As Integer = 0, Optional itemId As Integer = 0)
    '        Dim dt As New DataTable()

    '        Dim sql As String =
    '"SELECT
    '    a.AllocationId,
    '    a.FiscalYear,
    '    a.BudgetItemId,
    '    d.DoorCode,
    '    d.DoorName,
    '    c.ChapterCode,
    '    c.ChapterName,
    '    i.ItemCode,
    '    i.ItemName,
    '    a.AllocatedAmount
    'FROM Budget_Allocations a
    'JOIN Budget_Items i ON a.BudgetItemId = i.BudgetItemId
    'JOIN Budget_Chapters c ON i.ChapterId = c.ChapterId
    'JOIN Budget_Doors d ON c.DoorId = d.DoorId
    'WHERE (@Y = 0 OR a.FiscalYear = @Y)
    '  AND (@DoorId = 0 OR d.DoorId = @DoorId)
    '  AND (@ChapterId = 0 OR c.ChapterId = @ChapterId)
    '  AND (@ItemId = 0 OR i.BudgetItemId = @ItemId)
    'ORDER BY a.FiscalYear DESC, d.DoorCode, c.ChapterCode, i.ItemCode;"

    '        Using cn As New SqlConnection(ConnStr)
    '            Using cmd As New SqlCommand(sql, cn)
    '                cmd.Parameters.AddWithValue("@Y", fiscalYear)
    '                cmd.Parameters.AddWithValue("@DoorId", doorId)
    '                cmd.Parameters.AddWithValue("@ChapterId", chapterId)
    '                cmd.Parameters.AddWithValue("@ItemId", itemId)

    '                Using da As New SqlDataAdapter(cmd)
    '                    da.Fill(dt)
    '                End Using
    '            End Using
    '        End Using

    '        dgvAllocations.DataSource = dt

    '        If dgvAllocations.Columns.Count > 0 Then
    '            dgvAllocations.Columns("AllocationId").Visible = False
    '            dgvAllocations.Columns("BudgetItemId").Visible = False

    '            dgvAllocations.Columns("FiscalYear").HeaderText = "السنة"
    '            dgvAllocations.Columns("DoorCode").HeaderText = "كود الباب"
    '            dgvAllocations.Columns("DoorName").HeaderText = "اسم الباب"
    '            dgvAllocations.Columns("ChapterCode").HeaderText = "كود الفصل"
    '            dgvAllocations.Columns("ChapterName").HeaderText = "اسم الفصل"
    '            dgvAllocations.Columns("ItemCode").HeaderText = "كود البند"
    '            dgvAllocations.Columns("ItemName").HeaderText = "اسم البند"
    '            dgvAllocations.Columns("AllocatedAmount").HeaderText = "الاعتماد"

    '            dgvAllocations.Columns("AllocatedAmount").DefaultCellStyle.Format = "N3"
    '        End If

    '        dgvAllocations.ClearSelection()
    '        SetStatus($"تم تحميل {dt.Rows.Count} اعتماد")
    '    End Sub

    '=========================
    ' Helpers
    '=========================
    'Private Sub ClearForm()
    '    CurrentAllocationId = 0
    '    txtAmount.Text = ""
    '    'لا نمسح السنة إلا إذا تريد
    '    'cmbFiscalYear.SelectedItem = DateTime.Now.Year
    '    cmbDoors.SelectedIndex = -1
    '    cmbChapters.DataSource = Nothing
    '    cmbItems.DataSource = Nothing
    'End Sub

    Private Sub ClearForm()
        CurrentMovementId = 0

        txtAmount.Text = ""
        txtAmountWords.Text = ""
        txtDecisionNo.Text = ""
        txtReason.Text = ""
        txtEmergencyReason.Text = ""

        cmbAllocationType.SelectedIndex = -1
        cmbProvider.SelectedIndex = -1

        cmbDoors.SelectedIndex = -1
        cmbChapters.DataSource = Nothing
        cmbItems.DataSource = Nothing

        cmbOriginalFiscalYear.SelectedIndex = -1

        dtpMovementDate.Value = DateTime.Now
        dtpDecisionDate.Value = DateTime.Now

        chkAutoApprove.Checked = True

        UpdateTypeFieldsVisibility()
    End Sub

    Private Sub UpdateTypeFieldsVisibility()
        Dim typeId As Integer = SelectedAllocationTypeId()

        Dim isCarried As Boolean = (typeId = 6)
        Dim isEmergency As Boolean = (typeId = 7)

        lblOriginalFiscalYear.Visible = isCarried
        cmbOriginalFiscalYear.Visible = isCarried

        lblEmergencyReason.Visible = isEmergency
        txtEmergencyReason.Visible = isEmergency
    End Sub

    Private Sub cmbAllocationType_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbAllocationType.SelectionChangeCommitted
        UpdateTypeFieldsVisibility()
    End Sub

    Private Function SelectedAllocationTypeId() As Integer
        If cmbAllocationType.SelectedIndex < 0 OrElse cmbAllocationType.SelectedValue Is Nothing Then Return 0
        Return Convert.ToInt32(cmbAllocationType.SelectedValue)
    End Function

    Private Function SelectedYear() As Integer
        If cmbFiscalYear.SelectedItem Is Nothing Then Return 0
        Return Convert.ToInt32(cmbFiscalYear.SelectedItem)
    End Function

    Private Function ValidateForm() As Boolean
        If cmbAllocationType.SelectedIndex < 0 Then
            MessageBox.Show("اختر نوع الاعتماد", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbAllocationType.Focus()
            Return False
        End If

        If cmbFiscalYear.SelectedItem Is Nothing Then
            MessageBox.Show("اختر السنة المالية", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbFiscalYear.Focus()
            Return False
        End If

        If cmbDoors.SelectedIndex < 0 Then
            MessageBox.Show("اختر الباب", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbDoors.Focus()
            Return False
        End If

        If cmbChapters.SelectedIndex < 0 Then
            MessageBox.Show("اختر الفصل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbChapters.Focus()
            Return False
        End If

        If cmbItems.SelectedIndex < 0 Then
            MessageBox.Show("اختر البند", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbItems.Focus()
            Return False
        End If

        Dim amt As Decimal
        If Not BudgetUiHelper.TryParseMoneyText(txtAmount.Text.Trim(), amt) Then
            MessageBox.Show("قيمة الاعتماد غير صحيحة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtAmount.Focus()
            Return False
        End If

        If amt <= 0D Then
            MessageBox.Show("قيمة الاعتماد يجب أن تكون أكبر من صفر", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtAmount.Focus()
            Return False
        End If

        Dim typeId As Integer = SelectedAllocationTypeId()

        If typeId = 6 AndAlso cmbOriginalFiscalYear.SelectedIndex < 0 Then
            MessageBox.Show("الاعتماد المرحل يحتاج تحديد السنة الأصلية", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbOriginalFiscalYear.Focus()
            Return False
        End If

        If typeId = 7 AndAlso String.IsNullOrWhiteSpace(txtEmergencyReason.Text) Then
            MessageBox.Show("الاعتماد الطارئ يحتاج سبب الطارئ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtEmergencyReason.Focus()
            Return False
        End If

        Return True
    End Function

    'Private Function ValidateForm() As Boolean
    '    If cmbFiscalYear.SelectedItem Is Nothing Then
    '        MessageBox.Show("اختر السنة المالية", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        cmbFiscalYear.Focus()
    '        Return False
    '    End If

    '    If cmbDoors.SelectedIndex < 0 Then
    '        MessageBox.Show("اختر الباب", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        cmbDoors.Focus()
    '        Return False
    '    End If

    '    If cmbChapters.SelectedIndex < 0 Then
    '        MessageBox.Show("اختر الفصل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        cmbChapters.Focus()
    '        Return False
    '    End If

    '    If cmbItems.SelectedIndex < 0 Then
    '        MessageBox.Show("اختر البند", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        cmbItems.Focus()
    '        Return False
    '    End If

    '    Dim amt As Decimal
    '    If Not Decimal.TryParse(txtAmount.Text.Trim(), amt) Then
    '        MessageBox.Show("قيمة الاعتماد غير صحيحة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        txtAmount.Focus()
    '        Return False
    '    End If

    '    If amt < 0D Then
    '        MessageBox.Show("قيمة الاعتماد يجب أن تكون أكبر أو تساوي صفر", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        txtAmount.Focus()
    '        Return False
    '    End If

    '    Return True
    'End Function

    Private Sub SetStatus(msg As String)
        lblStatus.Text = msg
    End Sub

    '=========================
    ' Save (UPSERT)
    '=========================

    Private Sub SaveAllocation()
        If Not ValidateForm() Then Exit Sub

        Dim year As Integer = SelectedYear()
        Dim itemId As Integer = Convert.ToInt32(cmbItems.SelectedValue)
        Dim typeId As Integer = SelectedAllocationTypeId()

        Dim amt As Decimal
        BudgetUiHelper.TryParseMoneyText(txtAmount.Text.Trim(), amt)

        Dim providerId As Object = DBNull.Value
        If cmbProvider.SelectedIndex >= 0 Then
            providerId = Convert.ToInt32(cmbProvider.SelectedValue)
        End If

        Dim originalYear As Object = DBNull.Value
        If typeId = 6 AndAlso cmbOriginalFiscalYear.SelectedIndex >= 0 Then
            originalYear = Convert.ToInt32(cmbOriginalFiscalYear.SelectedItem)
        End If

        Dim emergencyReason As Object = DBNull.Value
        If typeId = 7 Then
            emergencyReason = txtEmergencyReason.Text.Trim()
        End If

        Dim refNo = NewRefNo()
        SetUserContext(refNo)

        Dim newId As Integer = 0

        Using cn As New SqlConnection(ConnStr)
            cn.Open()

            Using cmd As New SqlCommand("dbo.Budget_AllocationMovement_Insert", cn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@FiscalYear", year)
                cmd.Parameters.AddWithValue("@BudgetItemId", itemId)
                cmd.Parameters.AddWithValue("@AllocationTypeId", typeId)
                cmd.Parameters.AddWithValue("@Amount", amt)

                cmd.Parameters.AddWithValue("@ProviderId", providerId)

                cmd.Parameters.AddWithValue("@MovementDate", dtpMovementDate.Value)
                cmd.Parameters.AddWithValue("@DecisionNo", NullIfEmpty(txtDecisionNo.Text))
                cmd.Parameters.AddWithValue("@DecisionDate", dtpDecisionDate.Value.Date)
                cmd.Parameters.AddWithValue("@DocumentNo", DBNull.Value)
                cmd.Parameters.AddWithValue("@SourceRefNo", refNo)

                cmd.Parameters.AddWithValue("@Reason", NullIfEmpty(txtReason.Text))
                cmd.Parameters.AddWithValue("@Notes", DBNull.Value)

                cmd.Parameters.AddWithValue("@OriginalFiscalYear", originalYear)
                cmd.Parameters.AddWithValue("@CarriedFromBudgetItemId", DBNull.Value)

                cmd.Parameters.AddWithValue("@EmergencyReason", emergencyReason)
                cmd.Parameters.AddWithValue("@IsFromReserve", False)
                cmd.Parameters.AddWithValue("@ReserveBudgetItemId", DBNull.Value)

                cmd.Parameters.AddWithValue("@CostCenterId", DBNull.Value)
                cmd.Parameters.AddWithValue("@ProjectId", DBNull.Value)

                cmd.Parameters.AddWithValue("@CreatedBy", USER_ID)
                cmd.Parameters.AddWithValue("@AutoApprove", chkAutoApprove.Checked)

                Dim outParam As New SqlParameter("@NewMovementId", SqlDbType.Int)
                outParam.Direction = ParameterDirection.Output
                cmd.Parameters.Add(outParam)

                Using da As New SqlDataAdapter(cmd)
                    Dim resultDt As New DataTable()
                    da.Fill(resultDt)

                    If resultDt.Rows.Count > 0 Then
                        Dim result As Integer = Convert.ToInt32(resultDt.Rows(0)("Result"))
                        Dim msg As String = Convert.ToString(resultDt.Rows(0)("MessageText"))

                        If result = 0 Then
                            MessageBox.Show(msg, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            SetStatus("فشل الحفظ")
                            Return
                        Else
                            MessageBox.Show(msg, "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                End Using

                If outParam.Value IsNot DBNull.Value Then
                    newId = Convert.ToInt32(outParam.Value)
                End If
            End Using
        End Using

        CurrentMovementId = newId

        SetStatus("تم حفظ حركة الاعتماد")

        Dim doorId As Integer = If(cmbDoors.SelectedIndex >= 0, Convert.ToInt32(cmbDoors.SelectedValue), 0)
        Dim chapterId As Integer = If(cmbChapters.SelectedIndex >= 0, Convert.ToInt32(cmbChapters.SelectedValue), 0)

        LoadAllocationsGrid(year, doorId, chapterId, itemId)

        txtAmount.Text = ""
        txtAmount.Focus()
    End Sub

    Private Function NullIfEmpty(value As String) As Object
        If String.IsNullOrWhiteSpace(value) Then
            Return DBNull.Value
        End If

        Return value.Trim()
    End Function






    '    Private Sub SaveAllocation()
    '        If Not ValidateForm() Then Exit Sub

    '        Dim year As Integer = SelectedYear()
    '        Dim itemId As Integer = Convert.ToInt32(cmbItems.SelectedValue)

    '        Dim amt As Decimal
    '        Decimal.TryParse(txtAmount.Text.Trim(), amt)

    '        Dim refNo = NewRefNo()
    '        SetUserContext(refNo)

    '        Using cn As New SqlConnection(ConnStr)
    '            cn.Open()

    '            '✅ Upsert حسب (BudgetItemId, FiscalYear)
    '            Using cmd As New SqlCommand("
    'IF EXISTS (SELECT 1 FROM Budget_Allocations WHERE BudgetItemId = @ItemId AND FiscalYear = @Y)
    'BEGIN
    '    UPDATE Budget_Allocations
    '    SET AllocatedAmount = @Amt
    '    WHERE BudgetItemId = @ItemId AND FiscalYear = @Y;
    'END
    'ELSE
    'BEGIN
    '    INSERT INTO Budget_Allocations (BudgetItemId, FiscalYear, AllocatedAmount)
    '    VALUES (@ItemId, @Y, @Amt);
    'END
    '", cn)

    '                cmd.Parameters.AddWithValue("@ItemId", itemId)
    '                cmd.Parameters.AddWithValue("@Y", year)
    '                cmd.Parameters.AddWithValue("@Amt", amt)

    '                cmd.ExecuteNonQuery()
    '            End Using
    '        End Using

    '        SetStatus("تم حفظ الاعتماد")

    '        'تحديث الجريد بناءً على الفلاتر الحالية
    '        Dim doorId As Integer = If(cmbDoors.SelectedIndex >= 0, Convert.ToInt32(cmbDoors.SelectedValue), 0)
    '        Dim chapterId As Integer = If(cmbChapters.SelectedIndex >= 0, Convert.ToInt32(cmbChapters.SelectedValue), 0)
    '        LoadAllocationsGrid(year, doorId, chapterId, itemId)

    '        'لا نمسح الباب/الفصل لتسهيل إدخال عدة بنود داخل نفس الفصل
    '        CurrentAllocationId = 0
    '        txtAmount.Text = ""
    '        txtAmount.Focus()
    '    End Sub

    '=========================
    ' Grid Selection
    '=========================


    Private Sub FillFromGrid()
        If dgvAllocations.CurrentRow Is Nothing Then Exit Sub

        CurrentMovementId = Convert.ToInt32(dgvAllocations.CurrentRow.Cells("AllocationMovementId").Value)

        Dim year As Integer = Convert.ToInt32(dgvAllocations.CurrentRow.Cells("FiscalYear").Value)
        Dim itemId As Integer = Convert.ToInt32(dgvAllocations.CurrentRow.Cells("BudgetItemId").Value)
        Dim typeId As Integer = Convert.ToInt32(dgvAllocations.CurrentRow.Cells("AllocationTypeId").Value)
        Dim amt As Decimal = Convert.ToDecimal(dgvAllocations.CurrentRow.Cells("Amount").Value)

        cmbFiscalYear.SelectedItem = year
        cmbAllocationType.SelectedValue = typeId
        txtAmount.Text = amt.ToString("0.###")

        If dgvAllocations.CurrentRow.Cells("ProviderId").Value IsNot DBNull.Value Then
            cmbProvider.SelectedValue = Convert.ToInt32(dgvAllocations.CurrentRow.Cells("ProviderId").Value)
        Else
            cmbProvider.SelectedIndex = -1
        End If

        If dgvAllocations.CurrentRow.Cells("DecisionNo").Value IsNot DBNull.Value Then
            txtDecisionNo.Text = Convert.ToString(dgvAllocations.CurrentRow.Cells("DecisionNo").Value)
        Else
            txtDecisionNo.Text = ""
        End If

        If dgvAllocations.CurrentRow.Cells("Reason").Value IsNot DBNull.Value Then
            txtReason.Text = Convert.ToString(dgvAllocations.CurrentRow.Cells("Reason").Value)
        Else
            txtReason.Text = ""
        End If

        If dgvAllocations.CurrentRow.Cells("EmergencyReason").Value IsNot DBNull.Value Then
            txtEmergencyReason.Text = Convert.ToString(dgvAllocations.CurrentRow.Cells("EmergencyReason").Value)
        Else
            txtEmergencyReason.Text = ""
        End If

        If dgvAllocations.CurrentRow.Cells("OriginalFiscalYear").Value IsNot DBNull.Value Then
            cmbOriginalFiscalYear.SelectedItem = Convert.ToInt32(dgvAllocations.CurrentRow.Cells("OriginalFiscalYear").Value)
        Else
            cmbOriginalFiscalYear.SelectedIndex = -1
        End If

        If dgvAllocations.CurrentRow.Cells("MovementDate").Value IsNot DBNull.Value Then
            dtpMovementDate.Value = Convert.ToDateTime(dgvAllocations.CurrentRow.Cells("MovementDate").Value)
        End If

        If dgvAllocations.CurrentRow.Cells("DecisionDate").Value IsNot DBNull.Value Then
            dtpDecisionDate.Value = Convert.ToDateTime(dgvAllocations.CurrentRow.Cells("DecisionDate").Value)
        End If

        Using cn As New SqlConnection(ConnStr)
            cn.Open()
            Using cmd As New SqlCommand("
SELECT d.DoorId, c.ChapterId, i.BudgetItemId
FROM Budget_Items i
JOIN Budget_Chapters c ON i.ChapterId = c.ChapterId
JOIN Budget_Doors d ON c.DoorId = d.DoorId
WHERE i.BudgetItemId = @ItemId;", cn)

                cmd.Parameters.AddWithValue("@ItemId", itemId)

                Using rd = cmd.ExecuteReader()
                    If rd.Read() Then
                        Dim doorId As Integer = Convert.ToInt32(rd("DoorId"))
                        Dim chapterId As Integer = Convert.ToInt32(rd("ChapterId"))

                        cmbDoors.SelectedValue = doorId
                        LoadChapters(doorId)

                        cmbChapters.SelectedValue = chapterId
                        LoadItems(chapterId)

                        cmbItems.SelectedValue = itemId
                    End If
                End Using
            End Using
        End Using

        UpdateTypeFieldsVisibility()
        SetStatus("وضع عرض / متابعة الحركة")
    End Sub



    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        If CurrentMovementId <= 0 Then
            MessageBox.Show("اختر حركة اعتماد أولًا", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("هل تريد اعتماد هذه الحركة؟", "تأكيد الاعتماد",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then
            Return
        End If

        Try
            Using cn As New SqlConnection(ConnStr)
                cn.Open()

                Using cmd As New SqlCommand("dbo.Budget_AllocationMovement_Approve", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@AllocationMovementId", CurrentMovementId)
                    cmd.Parameters.AddWithValue("@ApprovedBy", USER_ID)

                    Using da As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)

                        If dt.Rows.Count > 0 Then
                            Dim result As Integer = Convert.ToInt32(dt.Rows(0)("Result"))
                            Dim msg As String = Convert.ToString(dt.Rows(0)("MessageText"))

                            MessageBox.Show(msg,
                                        If(result = 1, "تم", "خطأ"),
                                        MessageBoxButtons.OK,
                                        If(result = 1, MessageBoxIcon.Information, MessageBoxIcon.Error))
                        End If
                    End Using
                End Using
            End Using

            LoadAllocationsGrid(SelectedYear())
            SetStatus("تم تنفيذ الاعتماد")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SetStatus("فشل الاعتماد")
        End Try
    End Sub


    Private Sub btnCancelMovement_Click(sender As Object, e As EventArgs) Handles btnCancelMovement.Click
        If CurrentMovementId <= 0 Then
            MessageBox.Show("اختر حركة اعتماد أولًا", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim reason As String = InputBox("اكتب سبب إلغاء حركة الاعتماد:", "سبب الإلغاء")

        If String.IsNullOrWhiteSpace(reason) Then
            MessageBox.Show("يجب إدخال سبب الإلغاء", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("هل تريد إلغاء هذه الحركة؟", "تأكيد الإلغاء",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Warning,
                       MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then
            Return
        End If

        Try
            Using cn As New SqlConnection(ConnStr)
                cn.Open()

                Using cmd As New SqlCommand("dbo.Budget_AllocationMovement_Cancel", cn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@AllocationMovementId", CurrentMovementId)
                    cmd.Parameters.AddWithValue("@CanceledBy", USER_ID)
                    cmd.Parameters.AddWithValue("@CancelReason", reason)

                    Using da As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable()
                        da.Fill(dt)

                        If dt.Rows.Count > 0 Then
                            Dim result As Integer = Convert.ToInt32(dt.Rows(0)("Result"))
                            Dim msg As String = Convert.ToString(dt.Rows(0)("MessageText"))

                            MessageBox.Show(msg,
                                        If(result = 1, "تم", "خطأ"),
                                        MessageBoxButtons.OK,
                                        If(result = 1, MessageBoxIcon.Information, MessageBoxIcon.Error))
                        End If
                    End Using
                End Using
            End Using

            LoadAllocationsGrid(SelectedYear())
            SetStatus("تم إلغاء الحركة")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SetStatus("فشل الإلغاء")
        End Try
    End Sub


    '    Private Sub FillFromGrid()
    '        If dgvAllocations.CurrentRow Is Nothing Then Exit Sub

    '        CurrentAllocationId = Convert.ToInt32(dgvAllocations.CurrentRow.Cells("AllocationId").Value)

    '        Dim year As Integer = Convert.ToInt32(dgvAllocations.CurrentRow.Cells("FiscalYear").Value)
    '        Dim itemId As Integer = Convert.ToInt32(dgvAllocations.CurrentRow.Cells("BudgetItemId").Value)
    '        Dim amt As Decimal = Convert.ToDecimal(dgvAllocations.CurrentRow.Cells("AllocatedAmount").Value)

    '        cmbFiscalYear.SelectedItem = year
    '        txtAmount.Text = amt.ToString("0.###")

    '        '🔸 لتحديد Door/Chapter/Item بدقة نحتاج لاستعلام مساعد
    '        Using cn As New SqlConnection(ConnStr)
    '            cn.Open()
    '            Using cmd As New SqlCommand("
    'SELECT d.DoorId, c.ChapterId, i.BudgetItemId
    'FROM Budget_Items i
    'JOIN Budget_Chapters c ON i.ChapterId = c.ChapterId
    'JOIN Budget_Doors d ON c.DoorId = d.DoorId
    'WHERE i.BudgetItemId = @ItemId;", cn)

    '                cmd.Parameters.AddWithValue("@ItemId", itemId)

    '                Using rd = cmd.ExecuteReader()
    '                    If rd.Read() Then
    '                        Dim doorId As Integer = Convert.ToInt32(rd("DoorId"))
    '                        Dim chapterId As Integer = Convert.ToInt32(rd("ChapterId"))

    '                        cmbDoors.SelectedValue = doorId
    '                        LoadChapters(doorId)

    '                        cmbChapters.SelectedValue = chapterId
    '                        LoadItems(chapterId)

    '                        cmbItems.SelectedValue = itemId
    '                    End If
    '                End Using
    '            End Using
    '        End Using

    '        SetStatus("وضع التعديل")
    '    End Sub

    '=========================
    ' Events
    '=========================
    Private Sub cmbDoors_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbDoors.SelectionChangeCommitted
        Try
            LoadChapters(Convert.ToInt32(cmbDoors.SelectedValue))
            dgvAllocations.DataSource = Nothing
        Catch
        End Try
    End Sub

    Private Sub cmbChapters_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbChapters.SelectionChangeCommitted
        Try
            LoadItems(Convert.ToInt32(cmbChapters.SelectedValue))
            dgvAllocations.DataSource = Nothing
        Catch
        End Try
    End Sub

    Private Sub cmbItems_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbItems.SelectionChangeCommitted
        Try
            'عند اختيار بند نعرض اعتماده في السنة الحالية إن وجد
            LoadAllocationsGrid(SelectedYear(),
                                If(cmbDoors.SelectedIndex >= 0, Convert.ToInt32(cmbDoors.SelectedValue), 0),
                                If(cmbChapters.SelectedIndex >= 0, Convert.ToInt32(cmbChapters.SelectedValue), 0),
                                Convert.ToInt32(cmbItems.SelectedValue))
        Catch
        End Try
    End Sub

    Private Sub cmbFiscalYear_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbFiscalYear.SelectionChangeCommitted
        Try
            'فلترة بالعام فقط
            LoadAllocationsGrid(SelectedYear())
        Catch
        End Try
    End Sub

    Private Sub dgvAllocations_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAllocations.CellClick
        FillFromGrid()
    End Sub

    Private Sub dgvAllocations_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAllocations.CellDoubleClick
        FillFromGrid()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        ClearForm()
        SetStatus("جديد")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            SaveAllocation()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SetStatus("فشل الحفظ")
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            LoadAllocationsGrid(SelectedYear())
            SetStatus("تم التحديث")
        Catch ex As Exception
            SetStatus("خطأ: " & ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    'اختياري: تنسيق عند مغادرة حقل المبلغ
    Private Sub txtAmount_Leave(sender As Object, e As EventArgs) Handles txtAmount.Leave
        Dim amt As Decimal
        If Decimal.TryParse(txtAmount.Text.Trim(), amt) Then
            txtAmount.Text = amt.ToString("N3")
        End If
    End Sub

    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged
        UpdateAmountWords()
    End Sub
End Class
