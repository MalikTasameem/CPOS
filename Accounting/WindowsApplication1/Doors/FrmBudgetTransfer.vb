Imports System.Data
Imports System.Data.SqlClient

Public Class FrmBudgetTransfer

    Private ReadOnly ConnStr As String = MY_Settings.SqlConStr
    Private CurrentTransferGroupId As Guid = Guid.Empty

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
        Return "TRF-" & DateTime.Now.ToString("yyyyMMdd-HHmmss")
    End Function

    '=========================
    ' Load
    '=========================
    Private Sub FrmBudgetTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplyGridStyle()
            BudgetUiHelper.ApplyBudgetFormStyle(Me)
            ApplyBudgetOverSpendWarning()

            LoadFiscalYears()
            LoadDoorsFrom()
            LoadDoorsTo()

            ClearForm(keepYears:=True)

            rbTransferAllocation.Checked = True

            ' تحويل الحجز لاحقًا لأنه يحتاج آلية مستقلة
            rbTransferReserve.Text = "تحويل حجز قائم - لاحقًا"
            rbTransferReserve.Enabled = False

            ' المناقلة حاليًا داخل نفس السنة
            cmbYearTo.Enabled = False
            cmbYearTo.SelectedItem = cmbYearFrom.SelectedItem

            LoadTransfersGrid()

            SetStatus("جاهز")
        Catch ex As Exception
            SetStatus("خطأ: " & ex.Message)
        End Try
    End Sub

    Private Sub ApplyGridStyle()
        dgvTransfers.EnableHeadersVisualStyles = False
        dgvTransfers.ColumnHeadersHeight = 38
        dgvTransfers.RowTemplate.Height = 34
        dgvTransfers.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        dgvTransfers.DefaultCellStyle.Font = New Font("Segoe UI", 10)
        dgvTransfers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 237, 255)
        dgvTransfers.RowHeadersVisible = False
        dgvTransfers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    '=========================
    ' Fiscal Years
    '=========================

    Private Sub LoadFiscalYears()
        '✅ بدون جدول: نولّد قائمة سنوات (من السنة الحالية -2 إلى +5)
        'Dim nowY = DateTime.Now.Year
        cmbYearFrom.Items.Clear()
        cmbYearTo.Items.Clear()

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
        cmbYearFrom.Items.Add(Identifiers.F_YEAR)
        cmbYearFrom.SelectedItem = Identifiers.F_YEAR

        cmbYearTo.Items.Add(Identifiers.F_YEAR)
        cmbYearTo.SelectedItem = Identifiers.F_YEAR

        'cmbFiscalYear.Text = Identifiers.F_YEAR
        'End If
    End Sub

    'Private Sub LoadFiscalYears()
    '    Dim nowY = DateTime.Now.Year

    '    cmbYearFrom.Items.Clear()
    '    cmbYearTo.Items.Clear()

    '    For y As Integer = nowY - 2 To nowY + 5
    '        cmbYearFrom.Items.Add(y)
    '        cmbYearTo.Items.Add(y)
    '    Next

    '    cmbYearFrom.SelectedItem = nowY
    '    cmbYearTo.SelectedItem = nowY
    'End Sub

    Private Function SelectedYearFrom() As Integer
        If cmbYearFrom.SelectedItem Is Nothing Then Return 0
        Return Convert.ToInt32(cmbYearFrom.SelectedItem)
    End Function

    Private Function SelectedYearTo() As Integer
        If cmbYearTo.SelectedItem Is Nothing Then Return 0
        Return Convert.ToInt32(cmbYearTo.SelectedItem)
    End Function

    '=========================
    ' Cascading From
    '=========================
    Private Sub LoadDoorsFrom()
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

        cmbDoorFrom.DataSource = dt
        cmbDoorFrom.DisplayMember = "DoorText"
        cmbDoorFrom.ValueMember = "DoorId"
        cmbDoorFrom.SelectedIndex = -1

        cmbChapterFrom.DataSource = Nothing
        cmbItemFrom.DataSource = Nothing
    End Sub

    Private Sub LoadChaptersFrom(doorId As Integer)
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

        cmbChapterFrom.DataSource = dt
        cmbChapterFrom.DisplayMember = "ChapterText"
        cmbChapterFrom.ValueMember = "ChapterId"
        cmbChapterFrom.SelectedIndex = -1

        cmbItemFrom.DataSource = Nothing
    End Sub

    Private Sub LoadItemsFrom(chapterId As Integer)
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

        cmbItemFrom.DataSource = dt
        cmbItemFrom.DisplayMember = "ItemText"
        cmbItemFrom.ValueMember = "BudgetItemId"
        cmbItemFrom.SelectedIndex = -1
    End Sub

    '=========================
    ' Cascading To
    '=========================
    Private Sub LoadDoorsTo()
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

        cmbDoorTo.DataSource = dt
        cmbDoorTo.DisplayMember = "DoorText"
        cmbDoorTo.ValueMember = "DoorId"
        cmbDoorTo.SelectedIndex = -1

        cmbChapterTo.DataSource = Nothing
        cmbItemTo.DataSource = Nothing
    End Sub

    Private Sub LoadChaptersTo(doorId As Integer)
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

        cmbChapterTo.DataSource = dt
        cmbChapterTo.DisplayMember = "ChapterText"
        cmbChapterTo.ValueMember = "ChapterId"
        cmbChapterTo.SelectedIndex = -1

        cmbItemTo.DataSource = Nothing
    End Sub

    Private Sub LoadItemsTo(chapterId As Integer)
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

        cmbItemTo.DataSource = dt
        cmbItemTo.DisplayMember = "ItemText"
        cmbItemTo.ValueMember = "BudgetItemId"
        cmbItemTo.SelectedIndex = -1
    End Sub

    '=========================
    ' Summary (From)
    '=========================
    Private Sub UpdateFromSummary()
        If cmbItemFrom.SelectedIndex < 0 Or cmbYearFrom.SelectedItem Is Nothing Then
            lblFromAllocated.Text = "0.000 دينار"
            lblFromSpent.Text = "0.000 دينار"
            lblFromReserved.Text = "0.000 دينار"
            lblFromAvailable.Text = "0.000 دينار"
            lblFromAfter.Text = "0.000 دينار"
            Exit Sub
        End If

        Dim itemId As Integer = Convert.ToInt32(cmbItemFrom.SelectedValue)
        Dim year As Integer = SelectedYearFrom()

        Dim sum As BudgetSummary = GetItemBudgetSummary(itemId, year)

        lblFromAllocated.Text = sum.Allocated.ToString("N3") & " دينار"
        lblFromSpent.Text = sum.Spent.ToString("N3") & " دينار"
        lblFromReserved.Text = sum.Reserved.ToString("N3") & " دينار"
        lblFromAvailable.Text = sum.Available.ToString("N3") & " دينار"

        CalculateTransferImpact()
    End Sub

    Private Sub UpdateToSummary()
        If cmbItemTo.SelectedIndex < 0 OrElse cmbYearTo.SelectedItem Is Nothing Then
            lblToAllocated.Text = "0.000 دينار"
            lblToSpent.Text = "0.000 دينار"
            lblToReserved.Text = "0.000 دينار"
            lblToAvailable.Text = "0.000 دينار"
            lblToAfter.Text = "0.000 دينار"
            Exit Sub
        End If

        Dim itemId As Integer = Convert.ToInt32(cmbItemTo.SelectedValue)
        Dim year As Integer = SelectedYearTo()

        Dim sum As BudgetSummary = GetItemBudgetSummary(itemId, year)

        lblToAllocated.Text = sum.Allocated.ToString("N3") & " دينار"
        lblToSpent.Text = sum.Spent.ToString("N3") & " دينار"
        lblToReserved.Text = sum.Reserved.ToString("N3") & " دينار"
        lblToAvailable.Text = sum.Available.ToString("N3") & " دينار"
    End Sub

    '=========================
    ' Get Available Budget for Item/Year
    '=========================
    Private Function GetAvailableBudget(itemId As Integer, year As Integer) As Decimal
        Using cn As New SqlConnection(ConnStr)
            cn.Open()
            Using cmd As New SqlCommand("
SELECT ISNULL(Available, 0)
FROM dbo.Vw_BudgetItemSummary
WHERE BudgetItemId = @ItemId
  AND FiscalYear = @Y;", cn)

                cmd.Parameters.AddWithValue("@ItemId", itemId)
                cmd.Parameters.AddWithValue("@Y", year)

                Dim obj = cmd.ExecuteScalar()
                If obj Is Nothing OrElse obj Is DBNull.Value Then Return 0D

                Return Convert.ToDecimal(obj)
            End Using
        End Using
    End Function

    '=========================
    ' Grid (History)
    '=========================
    Private Sub LoadTransfersGrid(Optional topN As Integer = 300)
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand($"
SELECT TOP ({topN})
    TransferGroupId,
    FiscalYear,
    SourceItemCode,
    SourceItemName,
    TargetItemCode,
    TargetItemName,
    Amount,
    MovementDate,
    DecisionNo,
    DecisionDate,
    Reason,
    StatusName
FROM dbo.Vw_BudgetAllocationTransfers
ORDER BY MovementDate DESC, TransferGroupId DESC;", cn)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        dgvTransfers.DataSource = dt
        FormatTransfersGrid()
        dgvTransfers.ClearSelection()
    End Sub

    Private Sub FormatTransfersGrid()
        If dgvTransfers.Columns.Count = 0 Then Return

        If dgvTransfers.Columns.Contains("TransferGroupId") Then
            dgvTransfers.Columns("TransferGroupId").Visible = False
        End If

        If dgvTransfers.Columns.Contains("FiscalYear") Then dgvTransfers.Columns("FiscalYear").HeaderText = "السنة"
        If dgvTransfers.Columns.Contains("SourceItemCode") Then dgvTransfers.Columns("SourceItemCode").HeaderText = "كود المصدر"
        If dgvTransfers.Columns.Contains("SourceItemName") Then dgvTransfers.Columns("SourceItemName").HeaderText = "البند المصدر"
        If dgvTransfers.Columns.Contains("TargetItemCode") Then dgvTransfers.Columns("TargetItemCode").HeaderText = "كود المستفيد"
        If dgvTransfers.Columns.Contains("TargetItemName") Then dgvTransfers.Columns("TargetItemName").HeaderText = "البند المستفيد"
        If dgvTransfers.Columns.Contains("Amount") Then dgvTransfers.Columns("Amount").HeaderText = "قيمة المناقلة"
        If dgvTransfers.Columns.Contains("MovementDate") Then dgvTransfers.Columns("MovementDate").HeaderText = "تاريخ الحركة"
        If dgvTransfers.Columns.Contains("DecisionNo") Then dgvTransfers.Columns("DecisionNo").HeaderText = "رقم القرار"
        If dgvTransfers.Columns.Contains("DecisionDate") Then dgvTransfers.Columns("DecisionDate").HeaderText = "تاريخ القرار"
        If dgvTransfers.Columns.Contains("Reason") Then dgvTransfers.Columns("Reason").HeaderText = "السبب / البيان"
        If dgvTransfers.Columns.Contains("StatusName") Then dgvTransfers.Columns("StatusName").HeaderText = "الحالة"

        If dgvTransfers.Columns.Contains("Amount") Then
            dgvTransfers.Columns("Amount").DefaultCellStyle.Format = "N3"
            dgvTransfers.Columns("Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If

        If dgvTransfers.Columns.Contains("MovementDate") Then
            dgvTransfers.Columns("MovementDate").DefaultCellStyle.Format = "yyyy-MM-dd HH:mm"
        End If

        If dgvTransfers.Columns.Contains("DecisionDate") Then
            dgvTransfers.Columns("DecisionDate").DefaultCellStyle.Format = "yyyy-MM-dd"
        End If

        If dgvTransfers.Columns.Contains("SourceItemName") Then dgvTransfers.Columns("SourceItemName").FillWeight = 170
        If dgvTransfers.Columns.Contains("TargetItemName") Then dgvTransfers.Columns("TargetItemName").FillWeight = 170
        If dgvTransfers.Columns.Contains("Reason") Then dgvTransfers.Columns("Reason").FillWeight = 220
    End Sub

    '=========================
    ' Validate
    '=========================
    Private Function ValidateTransfer() As Boolean
        If cmbYearFrom.SelectedItem Is Nothing OrElse cmbYearTo.SelectedItem Is Nothing Then
            MessageBox.Show("اختر السنة المالية.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If SelectedYearFrom() <> SelectedYearTo() Then
            MessageBox.Show("المناقلة الحالية تكون داخل نفس السنة المالية فقط.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If Not rbTransferAllocation.Checked Then
            MessageBox.Show("تحويل الحجز سيتم تنفيذه في مرحلة مستقلة لاحقًا.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If cmbItemFrom.SelectedIndex < 0 Then
            MessageBox.Show("اختر البند المصدر.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbItemFrom.Focus()
            Return False
        End If

        If cmbItemTo.SelectedIndex < 0 Then
            MessageBox.Show("اختر البند المستفيد.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbItemTo.Focus()
            Return False
        End If

        Dim fromItem As Integer = Convert.ToInt32(cmbItemFrom.SelectedValue)
        Dim toItem As Integer = Convert.ToInt32(cmbItemTo.SelectedValue)

        If fromItem = toItem Then
            MessageBox.Show("لا يمكن نقل الاعتماد إلى نفس البند.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Dim amt As Decimal
        If Not BudgetUiHelper.TryReadMoneyInput(txtAmount, "قيمة المناقلة", amt) Then Return False

        If amt <= 0D Then
            MessageBox.Show("قيمة المناقلة يجب أن تكون أكبر من صفر.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtAmount.Focus()
            Return False
        End If

        Dim sum As BudgetSummary = GetItemBudgetSummary(fromItem, SelectedYearFrom())

        If amt > sum.Available Then
            If IsBudgetOverSpendAllowed() Then
                If Not ConfirmBudgetOverSpendTransfer(sum, amt) Then Return False
            Else
                MessageBox.Show(
                    "لا يمكن تنفيذ المناقلة لأن القيمة أكبر من المتاح في البند المصدر." & Environment.NewLine &
                    "المتاح الحالي: " & sum.Available.ToString("N3") & " دينار",
                    "رفض المناقلة",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                )
                Return False
            End If
        End If

        If String.IsNullOrWhiteSpace(txtDecisionNo.Text) Then
            MessageBox.Show("أدخل رقم قرار المناقلة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtDecisionNo.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(txtNotes.Text) Then
            MessageBox.Show("اكتب سبب / بيان المناقلة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNotes.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function ConfirmTransfer() As Boolean
        Dim fromText As String = ComboText(cmbItemFrom)
        Dim toText As String = ComboText(cmbItemTo)

        Dim amount As Decimal = 0D
        BudgetUiHelper.TryParseMoneyText(txtAmount.Text, amount)

        Dim fromSummary As BudgetSummary = GetItemBudgetSummary(CInt(cmbItemFrom.SelectedValue), SelectedYearFrom())
        Dim toSummary As BudgetSummary = GetItemBudgetSummary(CInt(cmbItemTo.SelectedValue), SelectedYearTo())

        Dim fromAfter As Decimal = fromSummary.Available - amount
        Dim toAfter As Decimal = toSummary.Available + amount

        Dim msg As String =
            "سيتم تنفيذ مناقلة اعتماد معتمدة مباشرة." & Environment.NewLine &
            "هذه العملية ستؤثر على موقف الموازنة فورًا." & Environment.NewLine & Environment.NewLine &
            "السنة المالية: " & SelectedYearFrom().ToString() & Environment.NewLine &
            "رقم القرار: " & txtDecisionNo.Text.Trim() & Environment.NewLine &
            "تاريخ القرار: " & dtpDecisionDate.Value.ToString("yyyy-MM-dd") & Environment.NewLine & Environment.NewLine &
            "من البند:" & Environment.NewLine &
            fromText & Environment.NewLine &
            "المتاح قبل المناقلة: " & fromSummary.Available.ToString("N3") & " دينار" & Environment.NewLine &
            "المتاح بعد المناقلة: " & fromAfter.ToString("N3") & " دينار" & Environment.NewLine & Environment.NewLine &
            "إلى البند:" & Environment.NewLine &
            toText & Environment.NewLine &
            "المتاح قبل المناقلة: " & toSummary.Available.ToString("N3") & " دينار" & Environment.NewLine &
            "المتاح بعد المناقلة: " & toAfter.ToString("N3") & " دينار" & Environment.NewLine & Environment.NewLine &
            "قيمة المناقلة: " & amount.ToString("N3") & " دينار" & Environment.NewLine & Environment.NewLine &
            "هل تريد تأكيد تنفيذ المناقلة؟"

        Return MessageBox.Show(
            msg,
            "تأكيد مناقلة الاعتماد",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2
        ) = DialogResult.Yes
    End Function

    Private Function ComboText(combo As ComboBox) As String
        If combo Is Nothing OrElse combo.SelectedIndex < 0 Then Return "-"
        Return Convert.ToString(combo.Text)
    End Function

    Private Sub CalculateTransferImpact()
        If lblFromAfter Is Nothing OrElse lblToAfter Is Nothing Then Exit Sub

        Dim amount As Decimal = 0D
        BudgetUiHelper.TryParseMoneyText(txtAmount.Text, amount)

        Dim fromAvailable As Decimal = GetMoneyFromLabel(lblFromAvailable.Text)
        Dim toAvailable As Decimal = GetMoneyFromLabel(lblToAvailable.Text)

        Dim fromAfter As Decimal = fromAvailable - amount
        Dim toAfter As Decimal = toAvailable + amount

        lblFromAfter.Text = fromAfter.ToString("N3") & " دينار"
        lblToAfter.Text = toAfter.ToString("N3") & " دينار"

        If amount <= 0D Then
            lblFromAfter.ForeColor = Color.Blue
            lblToAfter.ForeColor = Color.Blue
            btnTransfer.Enabled = True
            Exit Sub
        End If

        If fromAfter < 0D Then
            lblFromAfter.ForeColor = Color.DarkRed
            lblToAfter.ForeColor = Color.DarkGreen
            btnTransfer.Enabled = IsBudgetOverSpendAllowed()
            If IsBudgetOverSpendAllowed() Then
                SetStatus("تنبيه: قيمة المناقلة أكبر من المتاح وسيتم التنفيذ بسماحية النظام")
            Else
                SetStatus("قيمة المناقلة أكبر من المتاح في البند المصدر")
            End If
        Else
            lblFromAfter.ForeColor = Color.DarkGreen
            lblToAfter.ForeColor = Color.DarkGreen
            btnTransfer.Enabled = True
            SetStatus("جاهز")
        End If
    End Sub

    Private Function GetMoneyFromLabel(text As String) As Decimal
        Dim v As Decimal = 0D

        If String.IsNullOrWhiteSpace(text) Then Return 0D

        Decimal.TryParse(text.Replace("دينار", "").Replace(",", "").Trim(), v)

        Return v
    End Function

    Private Function NullIfEmpty(value As String) As Object
        If String.IsNullOrWhiteSpace(value) Then Return DBNull.Value
        Return value.Trim()
    End Function

    '=========================
    ' Execute Transfer
    '=========================
    Private Sub ExecuteTransfer()
        If Not ValidateTransfer() Then Exit Sub

        If Not ConfirmTransfer() Then
            SetStatus("تم إلغاء المناقلة قبل التنفيذ")
            Exit Sub
        End If

        Dim fromItem As Integer = Convert.ToInt32(cmbItemFrom.SelectedValue)
        Dim toItem As Integer = Convert.ToInt32(cmbItemTo.SelectedValue)
        Dim y As Integer = SelectedYearFrom()

        Dim amt As Decimal
        If Not BudgetUiHelper.TryReadMoneyInput(txtAmount, "قيمة المناقلة", amt) Then Exit Sub

        Dim refNo = NewRefNo()
        SetUserContext(refNo)

        Dim note As String = If(txtNotes.Text, "")
        note = note.Trim()

        Using cn As New SqlConnection(ConnStr)
            cn.Open()

            Using cmd As New SqlCommand("dbo.Budget_AllocationTransfer_Insert", cn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@FiscalYear", y)
                cmd.Parameters.AddWithValue("@SourceBudgetItemId", fromItem)
                cmd.Parameters.AddWithValue("@TargetBudgetItemId", toItem)
                cmd.Parameters.AddWithValue("@Amount", amt)

                cmd.Parameters.AddWithValue("@ProviderId", DBNull.Value)
                cmd.Parameters.AddWithValue("@MovementDate", DateTime.Now)
                cmd.Parameters.AddWithValue("@DecisionNo", NullIfEmpty(txtDecisionNo.Text))
                cmd.Parameters.AddWithValue("@DecisionDate", dtpDecisionDate.Value.Date)
                cmd.Parameters.AddWithValue("@DocumentNo", DBNull.Value)
                cmd.Parameters.AddWithValue("@SourceRefNo", refNo)

                cmd.Parameters.AddWithValue("@Reason", note)
                cmd.Parameters.AddWithValue("@Notes", note)

                cmd.Parameters.AddWithValue("@CostCenterId", DBNull.Value)
                cmd.Parameters.AddWithValue("@ProjectId", DBNull.Value)

                cmd.Parameters.AddWithValue("@CreatedBy", USER_ID)

                ' حاليًا نعتمد المناقلة مباشرة بعد التأكيد
                cmd.Parameters.AddWithValue("@AutoApprove", True)

                Dim outGroup As New SqlParameter("@TransferGroupId", SqlDbType.UniqueIdentifier)
                outGroup.Direction = ParameterDirection.Output
                cmd.Parameters.Add(outGroup)

                Using da As New SqlDataAdapter(cmd)
                    Dim resultDt As New DataTable()
                    da.Fill(resultDt)

                    If resultDt.Rows.Count > 0 Then
                        Dim result As Integer = Convert.ToInt32(resultDt.Rows(0)("Result"))
                        Dim msg As String = Convert.ToString(resultDt.Rows(0)("MessageText"))

                        MessageBox.Show(
                            msg,
                            If(result = 1, "تم", "خطأ"),
                            MessageBoxButtons.OK,
                            If(result = 1, MessageBoxIcon.Information, MessageBoxIcon.Error)
                        )

                        If result = 0 Then
                            SetStatus("فشل تنفيذ المناقلة")
                            Return
                        End If
                    End If
                End Using

            End Using
        End Using

        SetStatus("تم تنفيذ مناقلة الاعتماد بنجاح")

        UpdateFromSummary()
        UpdateToSummary()
        LoadTransfersGrid()

        txtAmount.Text = ""
        txtNotes.Text = ""
        txtDecisionNo.Text = ""

        If dtpDecisionDate IsNot Nothing Then
            dtpDecisionDate.Value = DateTime.Now
        End If

        UpdateAmountWords()
        CalculateTransferImpact()
        txtAmount.Focus()
    End Sub

    '=========================
    ' Helpers UI
    '=========================
    Private Sub ClearForm(Optional keepYears As Boolean = True)
        txtAmount.Text = ""
        txtNotes.Text = ""
        txtDecisionNo.Text = ""
        If dtpDecisionDate IsNot Nothing Then
            dtpDecisionDate.Value = DateTime.Now
        End If
        UpdateAmountWords()

        cmbDoorFrom.SelectedIndex = -1
        cmbChapterFrom.DataSource = Nothing
        cmbItemFrom.DataSource = Nothing

        cmbDoorTo.SelectedIndex = -1
        cmbChapterTo.DataSource = Nothing
        cmbItemTo.DataSource = Nothing

        If Not keepYears Then
            cmbYearFrom.SelectedItem = Identifiers.F_YEAR
            cmbYearTo.SelectedItem = Identifiers.F_YEAR
        End If

        lblFromAllocated.Text = "0.000 دينار"
        lblFromSpent.Text = "0.000 دينار"
        lblFromReserved.Text = "0.000 دينار"
        lblFromAvailable.Text = "0.000 دينار"
        lblFromAfter.Text = "0.000 دينار"

        lblToAllocated.Text = "0.000 دينار"
        lblToSpent.Text = "0.000 دينار"
        lblToReserved.Text = "0.000 دينار"
        lblToAvailable.Text = "0.000 دينار"
        lblToAfter.Text = "0.000 دينار"

        CurrentTransferGroupId = Guid.Empty
        btnTransfer.Enabled = True
    End Sub

    Private Sub SetStatus(msg As String)
        lblStatus.Text = msg
    End Sub

    Private Function IsBudgetOverSpendAllowed() As Boolean
        Return MY_Settings.Use_State_Budget AndAlso MY_Settings.Allow_Budget_OverSpend
    End Function

    Private Sub ApplyBudgetOverSpendWarning()
        If lblBudgetOverSpendWarning IsNot Nothing Then
            lblBudgetOverSpendWarning.Visible = IsBudgetOverSpendAllowed()
        End If
    End Sub

    Private Function ConfirmBudgetOverSpendTransfer(summary As BudgetSummary, amount As Decimal) As Boolean
        Dim msg As String =
            "تنبيه: إعداد النظام يسمح بتنفيذ المناقلة رغم عدم كفاية المتاح في البند المصدر." & Environment.NewLine & Environment.NewLine &
            "الاعتماد: " & summary.Allocated.ToString("N3") & Environment.NewLine &
            "المصروف: " & summary.Spent.ToString("N3") & Environment.NewLine &
            "المحجوز: " & summary.Reserved.ToString("N3") & Environment.NewLine &
            "المتاح: " & summary.Available.ToString("N3") & Environment.NewLine &
            "قيمة المناقلة: " & amount.ToString("N3") & Environment.NewLine &
            "المتاح بعد المناقلة: " & (summary.Available - amount).ToString("N3") & Environment.NewLine & Environment.NewLine &
            "هل تريد المتابعة؟"

        Return MessageBox.Show(msg, "تأكيد سماحية الموازنة", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.Yes
    End Function

    Private Sub UpdateAmountWords()
        If txtAmountWords Is Nothing Then Exit Sub

        Dim amount As Decimal = 0D
        If BudgetUiHelper.TryParseMoneyText(txtAmount.Text, amount) AndAlso amount > 0D Then
            txtAmountWords.Text = HANY(amount, "LYD")
        Else
            txtAmountWords.Text = ""
        End If
    End Sub

    '=========================
    ' Events
    '=========================
    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged
        UpdateAmountWords()
        CalculateTransferImpact()
    End Sub

    Private Sub cmbDoorFrom_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbDoorFrom.SelectionChangeCommitted
        If cmbDoorFrom.SelectedIndex >= 0 Then
            LoadChaptersFrom(Convert.ToInt32(cmbDoorFrom.SelectedValue))
            UpdateFromSummary()
            CalculateTransferImpact()
        End If
    End Sub

    Private Sub cmbChapterFrom_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbChapterFrom.SelectionChangeCommitted
        If cmbChapterFrom.SelectedIndex >= 0 Then
            LoadItemsFrom(Convert.ToInt32(cmbChapterFrom.SelectedValue))
            UpdateFromSummary()
            CalculateTransferImpact()
        End If
    End Sub

    Private Sub cmbItemFrom_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbItemFrom.SelectionChangeCommitted
        UpdateFromSummary()
        CalculateTransferImpact()
    End Sub

    Private Sub cmbDoorTo_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbDoorTo.SelectionChangeCommitted
        If cmbDoorTo.SelectedIndex >= 0 Then
            LoadChaptersTo(Convert.ToInt32(cmbDoorTo.SelectedValue))
            UpdateToSummary()
            CalculateTransferImpact()
        End If
    End Sub

    Private Sub cmbChapterTo_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbChapterTo.SelectionChangeCommitted
        If cmbChapterTo.SelectedIndex >= 0 Then
            LoadItemsTo(Convert.ToInt32(cmbChapterTo.SelectedValue))
            UpdateToSummary()
            CalculateTransferImpact()
        End If
    End Sub

    Private Sub cmbItemTo_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbItemTo.SelectionChangeCommitted
        UpdateToSummary()
        CalculateTransferImpact()
    End Sub

    Private Sub cmbYearFrom_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbYearFrom.SelectionChangeCommitted
        cmbYearTo.SelectedItem = cmbYearFrom.SelectedItem
        UpdateFromSummary()
        UpdateToSummary()
        CalculateTransferImpact()
    End Sub

    Private Sub cmbYearTo_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbYearTo.SelectionChangeCommitted
        UpdateToSummary()
        CalculateTransferImpact()
    End Sub

    Private Sub rbTransferAllocation_CheckedChanged(sender As Object, e As EventArgs) Handles rbTransferAllocation.CheckedChanged
        'يمكن لاحقًا إظهار/إخفاء قيود/تنبيهات هنا
    End Sub

    Private Sub rbTransferReserve_CheckedChanged(sender As Object, e As EventArgs) Handles rbTransferReserve.CheckedChanged
        'يمكن لاحقًا إظهار/إخفاء قيود/تنبيهات هنا
    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        Try
            ExecuteTransfer()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SetStatus("فشل المناقلة")
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            UpdateFromSummary()
            UpdateToSummary()
            CalculateTransferImpact()
            LoadTransfersGrid()
            SetStatus("تم التحديث")
        Catch ex As Exception
            SetStatus("خطأ: " & ex.Message)
        End Try
    End Sub

    Private Sub dgvTransfers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTransfers.CellClick
        If dgvTransfers.CurrentRow Is Nothing Then Return

        If dgvTransfers.Columns.Contains("TransferGroupId") Then
            Dim s As String = Convert.ToString(dgvTransfers.CurrentRow.Cells("TransferGroupId").Value)
            If Not String.IsNullOrWhiteSpace(s) Then
                CurrentTransferGroupId = Guid.Parse(s)
            End If
        End If

        SetStatus("تم تحديد مناقلة من السجل")
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class
