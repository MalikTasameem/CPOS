Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms

Partial Class FrmAccountingPostingSettings

    Private ReadOnly _connectionString As String
    Private ReadOnly _currentUserId As Integer

    Public Sub New(connectionString As String, currentUserId As Integer)
        InitializeComponent()

        _connectionString = connectionString
        _currentUserId = currentUserId

        AddHandlers()
        ClearInputs()
        LoadSettings()
    End Sub

#Region "Handlers"

    Private Sub AddHandlers()
        AddHandler btnNew.Click, AddressOf btnNew_Click
        AddHandler btnSave.Click, AddressOf btnSave_Click
        AddHandler btnUpdate.Click, AddressOf btnUpdate_Click
        AddHandler btnSetActive.Click, AddressOf btnSetActive_Click
        AddHandler btnToggleActive.Click, AddressOf btnToggleActive_Click
        AddHandler btnRefresh.Click, AddressOf btnRefresh_Click
        AddHandler btnClose.Click, AddressOf btnClose_Click
        AddHandler dgvSettings.SelectionChanged, AddressOf dgvSettings_SelectionChanged
        AddHandler dgvSettings.CellFormatting, AddressOf dgvSettings_CellFormatting
    End Sub

#End Region

#Region "Load"

    Private Sub LoadSettings()
        Try
            lblStatus.Text = "جاري تحميل الإعدادات..."

            Dim sql As String =
"
SELECT
    Id,
    SalesAccountCode,
    SalesReturnAccountCode,
    InventoryAccountCode,
    CostOfGoodsSoldAccountCode,
    PurchaseAccountCode,
    PurchaseReturnAccountCode,
    DamageExpenseAccountCode,
    ManufacturingExpenseAccountCode,
    InventoryAdjustmentGainAccountCode,
    InventoryAdjustmentLossAccountCode,
    DiscountAllowedAccountCode,
    DiscountReceivedAccountCode,
    GeneralExpenseAccountCode,
    DefaultCurrencyId,
    DefaultCurrencyEqual,
    IsActive,
    CreatedAt
FROM dbo.AccountingPostingSettings
ORDER BY IsActive DESC, Id DESC;
"

            Dim dt As New DataTable()

            Using con As New SqlConnection(_connectionString)
                Using da As New SqlDataAdapter(sql, con)
                    da.Fill(dt)
                End Using
            End Using

            dgvSettings.DataSource = dt
            FormatGrid()

            lblStatus.Text = "تم تحميل الإعدادات. العدد: " & dt.Rows.Count.ToString()

        Catch ex As Exception
            lblStatus.Text = "حدث خطأ"
            MessageBox.Show(ex.Message, "خطأ في تحميل الإعدادات", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormatGrid()
        If dgvSettings.Columns.Count = 0 Then Return

        SetHeader("Id", "ID", 60)
        SetHeader("SalesAccountCode", "المبيعات", 100)
        SetHeader("SalesReturnAccountCode", "مردودات المبيعات", 130)
        SetHeader("InventoryAccountCode", "المخزون", 100)
        SetHeader("CostOfGoodsSoldAccountCode", "تكلفة المبيعات", 130)
        SetHeader("PurchaseAccountCode", "المشتريات", 100)
        SetHeader("PurchaseReturnAccountCode", "مردودات المشتريات", 140)
        SetHeader("DamageExpenseAccountCode", "مصروف التالف", 120)
        SetHeader("ManufacturingExpenseAccountCode", "تكلفة التصنيع", 130)
        SetHeader("InventoryAdjustmentGainAccountCode", "أرباح التسوية", 130)
        SetHeader("InventoryAdjustmentLossAccountCode", "خسائر التسوية", 130)
        SetHeader("DiscountAllowedAccountCode", "خصم مسموح", 120)
        SetHeader("DiscountReceivedAccountCode", "خصم مكتسب", 120)
        SetHeader("GeneralExpenseAccountCode", "مصروف عام", 110)
        SetHeader("DefaultCurrencyId", "العملة", 70)
        SetHeader("DefaultCurrencyEqual", "المعادل", 90)
        SetHeader("IsActive", "فعال", 70)
        SetHeader("CreatedAt", "تاريخ الإنشاء", 140)

        If dgvSettings.Columns.Contains("DefaultCurrencyEqual") Then
            dgvSettings.Columns("DefaultCurrencyEqual").DefaultCellStyle.Format = "N3"
        End If
    End Sub

    Private Sub SetHeader(columnName As String, headerText As String, width As Integer)
        If dgvSettings.Columns.Contains(columnName) Then
            dgvSettings.Columns(columnName).HeaderText = headerText
            dgvSettings.Columns(columnName).Width = width
        End If
    End Sub

#End Region

#Region "Buttons"

    Private Sub btnNew_Click(sender As Object, e As EventArgs)
        ClearInputs()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        LoadSettings()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If Not ValidateInputs() Then Return

            If chkIsActive.Checked Then
                If MessageBox.Show("سيتم جعل هذا الإعداد هو الإعداد الفعال وتعطيل باقي الإعدادات. هل تريد المتابعة؟",
                                   "تأكيد",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question) = DialogResult.No Then
                    Return
                End If
            End If

            Using con As New SqlConnection(_connectionString)
                con.Open()

                Using tran As SqlTransaction = con.BeginTransaction()
                    Try
                        If chkIsActive.Checked Then
                            Using cmdDisable As New SqlCommand("UPDATE dbo.AccountingPostingSettings SET IsActive = 0", con, tran)
                                cmdDisable.ExecuteNonQuery()
                            End Using
                        End If

                        Dim sql As String =
"
INSERT INTO dbo.AccountingPostingSettings
(
    SalesAccountCode,
    SalesReturnAccountCode,
    InventoryAccountCode,
    CostOfGoodsSoldAccountCode,
    PurchaseAccountCode,
    PurchaseReturnAccountCode,
    DamageExpenseAccountCode,
    ManufacturingExpenseAccountCode,
    InventoryAdjustmentGainAccountCode,
    InventoryAdjustmentLossAccountCode,
    DiscountAllowedAccountCode,
    DiscountReceivedAccountCode,
    GeneralExpenseAccountCode,
    DefaultCurrencyId,
    DefaultCurrencyEqual,
    IsActive
)
VALUES
(
    @SalesAccountCode,
    @SalesReturnAccountCode,
    @InventoryAccountCode,
    @CostOfGoodsSoldAccountCode,
    @PurchaseAccountCode,
    @PurchaseReturnAccountCode,
    @DamageExpenseAccountCode,
    @ManufacturingExpenseAccountCode,
    @InventoryAdjustmentGainAccountCode,
    @InventoryAdjustmentLossAccountCode,
    @DiscountAllowedAccountCode,
    @DiscountReceivedAccountCode,
    @GeneralExpenseAccountCode,
    @DefaultCurrencyId,
    @DefaultCurrencyEqual,
    @IsActive
);
"

                        Using cmd As New SqlCommand(sql, con, tran)
                            FillCommandParameters(cmd)
                            cmd.ExecuteNonQuery()
                        End Using

                        tran.Commit()

                    Catch
                        tran.Rollback()
                        Throw
                    End Try
                End Using
            End Using

            MessageBox.Show("تم حفظ الإعدادات بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearInputs()
            LoadSettings()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ في الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs)
        Try
            If txtId.Text.Trim() = "" Then
                MessageBox.Show("يرجى اختيار إعداد من الجدول أولًا", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If Not ValidateInputs() Then Return

            Dim id As Integer = Convert.ToInt32(txtId.Text)

            If chkIsActive.Checked Then
                If MessageBox.Show("سيتم جعل هذا الإعداد هو الإعداد الفعال وتعطيل باقي الإعدادات. هل تريد المتابعة؟",
                                   "تأكيد",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question) = DialogResult.No Then
                    Return
                End If
            End If

            Using con As New SqlConnection(_connectionString)
                con.Open()

                Using tran As SqlTransaction = con.BeginTransaction()
                    Try
                        If chkIsActive.Checked Then
                            Using cmdDisable As New SqlCommand("UPDATE dbo.AccountingPostingSettings SET IsActive = 0 WHERE Id <> @Id", con, tran)
                                cmdDisable.Parameters.Add("@Id", SqlDbType.Int).Value = id
                                cmdDisable.ExecuteNonQuery()
                            End Using
                        End If

                        Dim sql As String =
"
UPDATE dbo.AccountingPostingSettings
SET
    SalesAccountCode = @SalesAccountCode,
    SalesReturnAccountCode = @SalesReturnAccountCode,
    InventoryAccountCode = @InventoryAccountCode,
    CostOfGoodsSoldAccountCode = @CostOfGoodsSoldAccountCode,
    PurchaseAccountCode = @PurchaseAccountCode,
    PurchaseReturnAccountCode = @PurchaseReturnAccountCode,
    DamageExpenseAccountCode = @DamageExpenseAccountCode,
    ManufacturingExpenseAccountCode = @ManufacturingExpenseAccountCode,
    InventoryAdjustmentGainAccountCode = @InventoryAdjustmentGainAccountCode,
    InventoryAdjustmentLossAccountCode = @InventoryAdjustmentLossAccountCode,
    DiscountAllowedAccountCode = @DiscountAllowedAccountCode,
    DiscountReceivedAccountCode = @DiscountReceivedAccountCode,
    GeneralExpenseAccountCode = @GeneralExpenseAccountCode,
    DefaultCurrencyId = @DefaultCurrencyId,
    DefaultCurrencyEqual = @DefaultCurrencyEqual,
    IsActive = @IsActive
WHERE Id = @Id;
"

                        Using cmd As New SqlCommand(sql, con, tran)
                            FillCommandParameters(cmd)
                            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id
                            cmd.ExecuteNonQuery()
                        End Using

                        tran.Commit()

                    Catch
                        tran.Rollback()
                        Throw
                    End Try
                End Using
            End Using

            MessageBox.Show("تم تعديل الإعدادات بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadSettings()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ في التعديل", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSetActive_Click(sender As Object, e As EventArgs)
        Try
            If txtId.Text.Trim() = "" Then
                MessageBox.Show("يرجى اختيار إعداد من الجدول", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim id As Integer = Convert.ToInt32(txtId.Text)

            If MessageBox.Show("هل تريد اعتماد هذا الإعداد كإعداد فعال؟",
                               "تأكيد",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question) = DialogResult.No Then
                Return
            End If

            Using con As New SqlConnection(_connectionString)
                con.Open()

                Using tran As SqlTransaction = con.BeginTransaction()
                    Try
                        Using cmd1 As New SqlCommand("UPDATE dbo.AccountingPostingSettings SET IsActive = 0", con, tran)
                            cmd1.ExecuteNonQuery()
                        End Using

                        Using cmd2 As New SqlCommand("UPDATE dbo.AccountingPostingSettings SET IsActive = 1 WHERE Id = @Id", con, tran)
                            cmd2.Parameters.Add("@Id", SqlDbType.Int).Value = id
                            cmd2.ExecuteNonQuery()
                        End Using

                        tran.Commit()

                    Catch
                        tran.Rollback()
                        Throw
                    End Try
                End Using
            End Using

            MessageBox.Show("تم اعتماد الإعداد بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadSettings()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnToggleActive_Click(sender As Object, e As EventArgs)
        Try
            If txtId.Text.Trim() = "" Then
                MessageBox.Show("يرجى اختيار إعداد من الجدول", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim id As Integer = Convert.ToInt32(txtId.Text)
            Dim newValue As Boolean = Not chkIsActive.Checked

            Using con As New SqlConnection(_connectionString)
                Dim sql As String =
"
UPDATE dbo.AccountingPostingSettings
SET IsActive = @IsActive
WHERE Id = @Id;
"
                Using cmd As New SqlCommand(sql, con)
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = newValue
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id

                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("تم تغيير حالة الإعداد", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadSettings()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Helpers"

    Private Sub FillCommandParameters(cmd As SqlCommand)
        cmd.Parameters.Add("@SalesAccountCode", SqlDbType.VarChar, 50).Value = txtSalesAccountCode.Text.Trim()
        cmd.Parameters.Add("@SalesReturnAccountCode", SqlDbType.VarChar, 50).Value = txtSalesReturnAccountCode.Text.Trim()
        cmd.Parameters.Add("@InventoryAccountCode", SqlDbType.VarChar, 50).Value = txtInventoryAccountCode.Text.Trim()
        cmd.Parameters.Add("@CostOfGoodsSoldAccountCode", SqlDbType.VarChar, 50).Value = txtCostOfGoodsSoldAccountCode.Text.Trim()

        cmd.Parameters.Add("@PurchaseAccountCode", SqlDbType.VarChar, 50).Value = GetNullableText(txtPurchaseAccountCode)
        cmd.Parameters.Add("@PurchaseReturnAccountCode", SqlDbType.VarChar, 50).Value = GetNullableText(txtPurchaseReturnAccountCode)
        cmd.Parameters.Add("@DamageExpenseAccountCode", SqlDbType.VarChar, 50).Value = txtDamageExpenseAccountCode.Text.Trim()
        cmd.Parameters.Add("@ManufacturingExpenseAccountCode", SqlDbType.VarChar, 50).Value = GetNullableText(txtManufacturingExpenseAccountCode)
        cmd.Parameters.Add("@InventoryAdjustmentGainAccountCode", SqlDbType.VarChar, 50).Value = GetNullableText(txtInventoryAdjustmentGainAccountCode)
        cmd.Parameters.Add("@InventoryAdjustmentLossAccountCode", SqlDbType.VarChar, 50).Value = GetNullableText(txtInventoryAdjustmentLossAccountCode)
        cmd.Parameters.Add("@DiscountAllowedAccountCode", SqlDbType.VarChar, 50).Value = GetNullableText(txtDiscountAllowedAccountCode)
        cmd.Parameters.Add("@DiscountReceivedAccountCode", SqlDbType.VarChar, 50).Value = GetNullableText(txtDiscountReceivedAccountCode)
        cmd.Parameters.Add("@GeneralExpenseAccountCode", SqlDbType.VarChar, 50).Value = GetNullableText(txtGeneralExpenseAccountCode)

        cmd.Parameters.Add("@DefaultCurrencyId", SqlDbType.Int).Value = Convert.ToInt32(txtDefaultCurrencyId.Text.Trim())
        cmd.Parameters.Add("@DefaultCurrencyEqual", SqlDbType.Decimal).Value = Convert.ToDecimal(txtDefaultCurrencyEqual.Text.Trim())
        cmd.Parameters("@DefaultCurrencyEqual").Precision = 18
        cmd.Parameters("@DefaultCurrencyEqual").Scale = 3

        cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = chkIsActive.Checked
    End Sub

    Private Function GetNullableText(txt As TextBox) As Object
        If txt.Text.Trim() = "" Then
            Return DBNull.Value
        End If

        Return txt.Text.Trim()
    End Function

    Private Function ValidateInputs() As Boolean
        If txtSalesAccountCode.Text.Trim() = "" Then
            MessageBox.Show("يرجى إدخال حساب المبيعات", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSalesAccountCode.Focus()
            Return False
        End If

        If txtSalesReturnAccountCode.Text.Trim() = "" Then
            MessageBox.Show("يرجى إدخال حساب مردودات المبيعات", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSalesReturnAccountCode.Focus()
            Return False
        End If

        If txtInventoryAccountCode.Text.Trim() = "" Then
            MessageBox.Show("يرجى إدخال حساب المخزون", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtInventoryAccountCode.Focus()
            Return False
        End If

        If txtCostOfGoodsSoldAccountCode.Text.Trim() = "" Then
            MessageBox.Show("يرجى إدخال حساب تكلفة المبيعات", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCostOfGoodsSoldAccountCode.Focus()
            Return False
        End If

        If txtDamageExpenseAccountCode.Text.Trim() = "" Then
            MessageBox.Show("يرجى إدخال حساب مصروف التالف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtDamageExpenseAccountCode.Focus()
            Return False
        End If

        Dim currencyId As Integer
        If Not Integer.TryParse(txtDefaultCurrencyId.Text.Trim(), currencyId) Then
            MessageBox.Show("رقم العملة يجب أن يكون رقمًا صحيحًا", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtDefaultCurrencyId.Focus()
            Return False
        End If

        Dim currencyEqual As Decimal
        If Not Decimal.TryParse(txtDefaultCurrencyEqual.Text.Trim(), currencyEqual) Then
            MessageBox.Show("معادل العملة يجب أن يكون رقمًا", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtDefaultCurrencyEqual.Focus()
            Return False
        End If

        If currencyEqual <= 0 Then
            MessageBox.Show("معادل العملة يجب أن يكون أكبر من صفر", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtDefaultCurrencyEqual.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub ClearInputs()
        txtId.Clear()

        txtSalesAccountCode.Clear()
        txtSalesReturnAccountCode.Clear()
        txtInventoryAccountCode.Clear()
        txtCostOfGoodsSoldAccountCode.Clear()
        txtPurchaseAccountCode.Clear()
        txtPurchaseReturnAccountCode.Clear()
        txtDamageExpenseAccountCode.Clear()
        txtManufacturingExpenseAccountCode.Clear()
        txtInventoryAdjustmentGainAccountCode.Clear()
        txtInventoryAdjustmentLossAccountCode.Clear()
        txtDiscountAllowedAccountCode.Clear()
        txtDiscountReceivedAccountCode.Clear()
        txtGeneralExpenseAccountCode.Clear()

        txtDefaultCurrencyId.Text = "1"
        txtDefaultCurrencyEqual.Text = "1.000"
        chkIsActive.Checked = True

        txtSalesAccountCode.Focus()
    End Sub

    Private Sub LoadSelectedRowToInputs()
        If dgvSettings.SelectedRows.Count = 0 Then Return

        Dim row As DataGridViewRow = dgvSettings.SelectedRows(0)

        txtId.Text = GetCellText(row, "Id")
        txtSalesAccountCode.Text = GetCellText(row, "SalesAccountCode")
        txtSalesReturnAccountCode.Text = GetCellText(row, "SalesReturnAccountCode")
        txtInventoryAccountCode.Text = GetCellText(row, "InventoryAccountCode")
        txtCostOfGoodsSoldAccountCode.Text = GetCellText(row, "CostOfGoodsSoldAccountCode")
        txtPurchaseAccountCode.Text = GetCellText(row, "PurchaseAccountCode")
        txtPurchaseReturnAccountCode.Text = GetCellText(row, "PurchaseReturnAccountCode")
        txtDamageExpenseAccountCode.Text = GetCellText(row, "DamageExpenseAccountCode")
        txtManufacturingExpenseAccountCode.Text = GetCellText(row, "ManufacturingExpenseAccountCode")
        txtInventoryAdjustmentGainAccountCode.Text = GetCellText(row, "InventoryAdjustmentGainAccountCode")
        txtInventoryAdjustmentLossAccountCode.Text = GetCellText(row, "InventoryAdjustmentLossAccountCode")
        txtDiscountAllowedAccountCode.Text = GetCellText(row, "DiscountAllowedAccountCode")
        txtDiscountReceivedAccountCode.Text = GetCellText(row, "DiscountReceivedAccountCode")
        txtGeneralExpenseAccountCode.Text = GetCellText(row, "GeneralExpenseAccountCode")
        txtDefaultCurrencyId.Text = GetCellText(row, "DefaultCurrencyId")
        txtDefaultCurrencyEqual.Text = GetCellText(row, "DefaultCurrencyEqual")

        If row.Cells("IsActive").Value IsNot DBNull.Value Then
            chkIsActive.Checked = Convert.ToBoolean(row.Cells("IsActive").Value)
        Else
            chkIsActive.Checked = False
        End If
    End Sub

    Private Function GetCellText(row As DataGridViewRow, columnName As String) As String
        If Not dgvSettings.Columns.Contains(columnName) Then Return ""

        If row.Cells(columnName).Value Is Nothing OrElse row.Cells(columnName).Value Is DBNull.Value Then
            Return ""
        End If

        Return row.Cells(columnName).Value.ToString()
    End Function

#End Region

#Region "Grid Events"

    Private Sub dgvSettings_SelectionChanged(sender As Object, e As EventArgs)
        LoadSelectedRowToInputs()
    End Sub

    Private Sub dgvSettings_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If e.RowIndex < 0 Then Return

        If dgvSettings.Columns.Contains("IsActive") Then
            Dim row As DataGridViewRow = dgvSettings.Rows(e.RowIndex)

            If row.Cells("IsActive").Value IsNot DBNull.Value AndAlso Convert.ToBoolean(row.Cells("IsActive").Value) Then
                row.DefaultCellStyle.BackColor = Color.FromArgb(232, 248, 245)
                row.DefaultCellStyle.ForeColor = Color.FromArgb(20, 90, 50)
            Else
                row.DefaultCellStyle.BackColor = Color.White
                row.DefaultCellStyle.ForeColor = Color.FromArgb(40, 55, 71)
            End If
        End If
    End Sub

#End Region

End Class