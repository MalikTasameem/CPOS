Imports System.Data
Imports System.Data.SqlClient

Imports System.Drawing.Printing

Public Class FrmBudgetEntries

    '1 = Spend (صرف) , 2 = Reserve (حجز)
    Public Property EntryMode As Integer = 2

    Private ReadOnly ConnStr As String = MY_Settings.SqlConStr

    Private WithEvents PDSpendVoucher As New PrintDocument
    Private PPSpendVoucher As New PrintPreviewDialog
    Private WithEvents PDOfficialSpendVoucher As New PrintDocument
    Private PPOfficialSpendVoucher As New PrintPreviewDialog
    Private SpendVoucherPrintData As DataTable
    Private IsLoadingEntriesGrid As Boolean = False
    Private dgvEntriesContextMenu As ContextMenuStrip
    Private ctxPickBeneficiary As ToolStripMenuItem
    Private ctxEditStamp As ToolStripMenuItem
    Private ctxEditStatement As ToolStripMenuItem
    Private ctxPreviewJournal As ToolStripMenuItem
    Private ctxApprove As ToolStripMenuItem
    Private ctxCancelEntry As ToolStripMenuItem
    Private ctxPrintVoucher As ToolStripMenuItem
    Private ctxPrintOfficialVoucher As ToolStripMenuItem

    Private Class JournalPreviewLine
        Public Property AccountCode As String
        Public Property AccountName As String
        Public Property Statement As String
        Public Property Debit As Decimal
        Public Property Credit As Decimal
    End Class

    Private Function NewRefNo() As String
        Return "BE-" & DateTime.Now.ToString("yyyyMMdd-HHmmss")
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
    Private Sub FrmBudgetEntries_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplyGridStyle()
            EnsureEntriesContextMenu()
            EnsureBudgetEnhancements()
            EnsureContraAccountColumn()
            EnsureStampAccountNameDisplay()
            ApplyModeUI()
            ApplyBudgetOverSpendWarning()
            InitializePrintDocument()
            LoadFiscalYears()
            LoadDoors()
            LoadCommitmentTypes()
            ClearForm(keepYear:=True)
            LoadEntriesGrid()
            SetStatus("جاهز")
            If EntryMode = 1 Then
                BtnApprove.Visible = True
                BtnCancelEntry.Visible = True
            End If
        Catch ex As Exception
            SetStatus("خطأ: " & ex.Message)
        End Try
    End Sub

    Private Sub EnsureContraAccountColumn()
        Using cn As New SqlConnection(ConnStr)
            cn.Open()
            Using cmd As New SqlCommand("
IF COL_LENGTH('dbo.Budget_Items', 'ContraAccountCode') IS NULL
BEGIN
    ALTER TABLE dbo.Budget_Items ADD ContraAccountCode NVARCHAR(40) NULL;
END", cn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub EnsureBudgetEnhancements()
        Using cn As New SqlConnection(ConnStr)
            cn.Open()

            Using cmd As New SqlCommand("
IF OBJECT_ID('dbo.Budget_CommitmentTypes', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Budget_CommitmentTypes
    (
        CommitmentTypeId TINYINT NOT NULL PRIMARY KEY,
        CommitmentTypeCode NVARCHAR(30) NOT NULL UNIQUE,
        CommitmentTypeName NVARCHAR(150) NOT NULL,
        Notes NVARCHAR(300) NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_Budget_CommitmentTypes_IsActive DEFAULT (1),
        SortOrder INT NOT NULL CONSTRAINT DF_Budget_CommitmentTypes_SortOrder DEFAULT (0)
    );
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Budget_CommitmentTypes)
BEGIN
    INSERT INTO dbo.Budget_CommitmentTypes
    (CommitmentTypeId, CommitmentTypeCode, CommitmentTypeName, Notes, IsActive, SortOrder)
    VALUES
    (1, N'PURCHASE_REQUEST', N'طلب شراء', N'حجز مبدئي ناتج عن طلب شراء', 1, 1),
    (2, N'SUPPLY_ORDER', N'أمر توريد', N'حجز ناتج عن أمر توريد أو أمر شراء معتمد', 1, 2),
    (3, N'CONTRACT', N'عقد', N'حجز ناتج عن عقد خدمات أو توريد أو صيانة', 1, 3),
    (4, N'WORK_ORDER', N'أمر عمل', N'حجز ناتج عن أمر عمل أو تكليف تنفيذي', 1, 4),
    (5, N'MANUAL_RESERVE', N'حجز يدوي', N'حجز يدوي مباشر', 1, 5),
    (6, N'RECURRING_EXPENSE', N'مصروف دوري', N'حجز لمصروفات دورية', 1, 6),
    (7, N'ADJUSTMENT', N'تسوية', N'تسوية ميزانية', 1, 7),
    (8, N'PETTY_CASH', N'عهدة / سلفة', N'حجز مرتبط بعهدة أو سلفة', 1, 8),
    (9, N'PROJECT_RESERVE', N'حجز مشروع', N'حجز مرتبط بمشروع', 1, 9);
END;

IF COL_LENGTH('dbo.Budget_Entries', 'StatusId') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD StatusId TINYINT NOT NULL CONSTRAINT DF_Budget_Entries_StatusId DEFAULT (0);

IF COL_LENGTH('dbo.Budget_Entries', 'CommitmentTypeId') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD CommitmentTypeId TINYINT NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'SourceType') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD SourceType INT NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'SourceId') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD SourceId INT NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'SourceTable') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD SourceTable NVARCHAR(50) NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'SourceRefNo') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD SourceRefNo NVARCHAR(50) NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'ApprovedAt') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD ApprovedAt DATETIME NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'ApprovedBy') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD ApprovedBy INT NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'CanceledAt') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD CanceledAt DATETIME NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'CanceledBy') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD CanceledBy INT NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'CancelReason') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD CancelReason NVARCHAR(500) NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'ReversalJournalId') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD ReversalJournalId INT NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'BeneficiaryType') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD BeneficiaryType TINYINT NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'BeneficiaryId') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD BeneficiaryId INT NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'ContraAccountCode') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD ContraAccountCode NVARCHAR(40) NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'PaymentMethodId') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD PaymentMethodId TINYINT NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'InvoiceNo') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD InvoiceNo NVARCHAR(50) NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'DocumentNo') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD DocumentNo NVARCHAR(50) NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'SpendStatement') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD SpendStatement NVARCHAR(500) NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'CostCenterId') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD CostCenterId INT NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'ProjectId') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD ProjectId INT NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'HasStamp') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD HasStamp BIT NOT NULL CONSTRAINT DF_Budget_Entries_HasStamp DEFAULT (0);

IF COL_LENGTH('dbo.Budget_Entries', 'StampPercent') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD StampPercent DECIMAL(18,3) NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'StampAccountCode') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD StampAccountCode NVARCHAR(40) NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'StampAmount') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD StampAmount DECIMAL(18,3) NULL;

IF COL_LENGTH('dbo.Budget_Entries', 'CreatedAt') IS NULL
    ALTER TABLE dbo.Budget_Entries ADD CreatedAt DATETIME NOT NULL CONSTRAINT DF_Budget_Entries_CreatedAt DEFAULT (GETDATE());

IF OBJECT_ID('dbo.Budget_BeneficiaryTypes', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Budget_BeneficiaryTypes
    (
        BeneficiaryType TINYINT NOT NULL PRIMARY KEY,
        BeneficiaryTypeName NVARCHAR(150) NOT NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_Budget_BeneficiaryTypes_IsActive DEFAULT (1),
        SortOrder INT NOT NULL CONSTRAINT DF_Budget_BeneficiaryTypes_SortOrder DEFAULT (0)
    );
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Budget_BeneficiaryTypes)
BEGIN
    INSERT INTO dbo.Budget_BeneficiaryTypes (BeneficiaryType, BeneficiaryTypeName, IsActive, SortOrder)
    VALUES
    (1, N'مورد', 1, 1),
    (2, N'موظف', 1, 2),
    (3, N'جهة حكومية', 1, 3),
    (4, N'حساب مقابل', 1, 4);
END;

IF OBJECT_ID('dbo.Budget_PaymentMethods', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Budget_PaymentMethods
    (
        PaymentMethodId TINYINT NOT NULL PRIMARY KEY,
        PaymentMethodName NVARCHAR(150) NOT NULL,
        IsActive BIT NOT NULL CONSTRAINT DF_Budget_PaymentMethods_IsActive DEFAULT (1),
        SortOrder INT NOT NULL CONSTRAINT DF_Budget_PaymentMethods_SortOrder DEFAULT (0)
    );
END;

IF NOT EXISTS (SELECT 1 FROM dbo.Budget_PaymentMethods)
BEGIN
    INSERT INTO dbo.Budget_PaymentMethods (PaymentMethodId, PaymentMethodName, IsActive, SortOrder)
    VALUES
    (1, N'نقدي', 1, 1),
    (2, N'تحويل مصرفي', 1, 2),
    (3, N'صك', 1, 3);
END;

IF NOT EXISTS (
    SELECT 1 FROM sys.foreign_keys
    WHERE name = 'FK_BudgetEntries_CommitmentTypes'
)
BEGIN
    ALTER TABLE dbo.Budget_Entries WITH NOCHECK
    ADD CONSTRAINT FK_BudgetEntries_CommitmentTypes
    FOREIGN KEY (CommitmentTypeId)
    REFERENCES dbo.Budget_CommitmentTypes(CommitmentTypeId);
END;
", cn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub LoadCommitmentTypes()
        If cmbCommitmentTypes Is Nothing Then Exit Sub

        Dim dt As New DataTable()
        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT CommitmentTypeId,
       CommitmentTypeName
FROM dbo.Budget_CommitmentTypes
WHERE IsActive = 1
ORDER BY SortOrder, CommitmentTypeId;", cn)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        cmbCommitmentTypes.DataSource = dt
        cmbCommitmentTypes.DisplayMember = "CommitmentTypeName"
        cmbCommitmentTypes.ValueMember = "CommitmentTypeId"
        cmbCommitmentTypes.SelectedIndex = -1
    End Sub

    Private Sub InitializePrintDocument()
        PPSpendVoucher.Document = PDSpendVoucher
        PPSpendVoucher.WindowState = FormWindowState.Maximized
        PDSpendVoucher.DefaultPageSettings.Landscape = False
        PDSpendVoucher.DefaultPageSettings.Margins = New Margins(35, 35, 35, 35)

        PPOfficialSpendVoucher.Document = PDOfficialSpendVoucher
        PPOfficialSpendVoucher.WindowState = FormWindowState.Maximized
        PDOfficialSpendVoucher.DefaultPageSettings.Landscape = False
        PDOfficialSpendVoucher.DefaultPageSettings.Margins = New Margins(30, 30, 30, 30)
    End Sub

    Private Sub ApplyModeUI()
        Dim isReserveMode As Boolean = (EntryMode = 2)

        If EntryMode = 1 Then
            lblMode.Text = "الوضع: صرف"
            lblMode.ForeColor = Color.LightCoral
            btnExecute.Text = "✓ تنفيذ عملية الصرف"
            'If btnPrintVoucher IsNot Nothing Then btnPrintVoucher.Visible = True
            If btnPrintOfficialVoucher IsNot Nothing Then btnPrintOfficialVoucher.Visible = True
            'If btnPreviewJournal IsNot Nothing Then btnPreviewJournal.Visible = True
            'If btnUpdateSpendStatement IsNot Nothing Then btnUpdateSpendStatement.Visible = True
            'If btnEditStamp IsNot Nothing Then btnEditStamp.Visible = True
            If txtSpendStatement IsNot Nothing Then txtSpendStatement.Visible = True
            If lblSpendStatement IsNot Nothing Then lblSpendStatement.Visible = True
        Else
            lblMode.Text = "الوضع: حجز"
            lblMode.ForeColor = Color.LightSkyBlue
            btnExecute.Text = "✓ تنفيذ عملية الحجز"
            If btnPrintVoucher IsNot Nothing Then btnPrintVoucher.Visible = False
            If btnPrintOfficialVoucher IsNot Nothing Then btnPrintOfficialVoucher.Visible = False
            If btnPreviewJournal IsNot Nothing Then btnPreviewJournal.Visible = False
            If btnUpdateSpendStatement IsNot Nothing Then btnUpdateSpendStatement.Visible = False
            If btnEditStamp IsNot Nothing Then btnEditStamp.Visible = False
            If txtSpendStatement IsNot Nothing Then txtSpendStatement.Visible = False
            If lblSpendStatement IsNot Nothing Then lblSpendStatement.Visible = False
        End If

        If cmbCommitmentTypes IsNot Nothing Then
            cmbCommitmentTypes.Visible = isReserveMode
            lblCommitmentType.Visible = isReserveMode
            txtSourceRef.Visible = isReserveMode
            lblSourceRef.Visible = isReserveMode
            txtSourceTable.Visible = isReserveMode
            lblSourceTable.Visible = isReserveMode
        End If

        SetStampControlsVisible(Not isReserveMode)
        SetBudgetItemExpenseAccountControlsVisible(Not isReserveMode)
        UpdateStampControls()
        UpdateBudgetItemExpenseAccountPreview()
    End Sub

    Private Sub ApplyGridStyle()
        dgvEntries.EnableHeadersVisualStyles = False
        dgvEntries.ColumnHeadersHeight = 30
        dgvEntries.RowTemplate.Height = 26
        dgvEntries.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0!, FontStyle.Regular)
        dgvEntries.DefaultCellStyle.Font = New Font("Segoe UI Semibold", 9.0!, FontStyle.Bold)
        dgvEntries.DefaultCellStyle.SelectionBackColor = Color.FromArgb(226, 232, 240)
        dgvEntries.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42)
        dgvEntries.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        dgvEntries.AllowUserToResizeRows = True
        dgvEntries.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvEntries.GridColor = Color.FromArgb(226, 232, 240)
        dgvEntries.RowHeadersVisible = False
        dgvEntries.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvEntries.ReadOnly = False
    End Sub

    Private Sub EnsureEntriesContextMenu()
        If dgvEntriesContextMenu IsNot Nothing Then Return

        dgvEntriesContextMenu = New ContextMenuStrip()
        dgvEntriesContextMenu.RightToLeft = RightToLeft.Yes

        ctxPickBeneficiary = New ToolStripMenuItem("اختيار حساب المستفيد")
        ctxEditStamp = New ToolStripMenuItem("تعديل الدمغة")
        ctxEditStatement = New ToolStripMenuItem("تعديل البيان")
        ctxPreviewJournal = New ToolStripMenuItem("معاينة القيد")
        ctxApprove = New ToolStripMenuItem("اعتماد الصرف")
        ctxCancelEntry = New ToolStripMenuItem("إلغاء الاعتماد")
        ctxPrintVoucher = New ToolStripMenuItem("طباعة الإذن")
        ctxPrintOfficialVoucher = New ToolStripMenuItem("طباعة رسمي")

        dgvEntriesContextMenu.Items.AddRange(New ToolStripItem() {
            ctxPickBeneficiary,
            ctxEditStamp,
            ctxEditStatement,
            New ToolStripSeparator(),
            ctxPreviewJournal,
            ctxApprove,
            ctxCancelEntry,
            New ToolStripSeparator(),
            ctxPrintVoucher,
            ctxPrintOfficialVoucher
        })

        AddHandler dgvEntriesContextMenu.Opening, AddressOf dgvEntriesContextMenu_Opening
        AddHandler ctxPickBeneficiary.Click, AddressOf ctxPickBeneficiary_Click
        AddHandler ctxEditStamp.Click, Sub() If btnEditStamp IsNot Nothing Then btnEditStamp.PerformClick()
        AddHandler ctxEditStatement.Click, Sub() If btnUpdateSpendStatement IsNot Nothing Then btnUpdateSpendStatement.PerformClick()
        AddHandler ctxPreviewJournal.Click, Sub() If btnPreviewJournal IsNot Nothing Then btnPreviewJournal.PerformClick()
        AddHandler ctxApprove.Click, Sub() If BtnApprove IsNot Nothing Then BtnApprove.PerformClick()
        AddHandler ctxCancelEntry.Click, Sub() If BtnCancelEntry IsNot Nothing Then BtnCancelEntry.PerformClick()
        AddHandler ctxPrintVoucher.Click, Sub() If btnPrintVoucher IsNot Nothing Then btnPrintVoucher.PerformClick()
        AddHandler ctxPrintOfficialVoucher.Click, Sub() If btnPrintOfficialVoucher IsNot Nothing Then btnPrintOfficialVoucher.PerformClick()

        dgvEntries.ContextMenuStrip = dgvEntriesContextMenu
    End Sub

    Private Sub dgvEntriesContextMenu_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs)
        If dgvEntries.CurrentRow Is Nothing OrElse dgvEntries.CurrentRow.IsNewRow Then
            e.Cancel = True
            Return
        End If

        Dim status As String = GridCellText(dgvEntries.CurrentRow, "EntryStatus")
        Dim isSpendMode As Boolean = (EntryMode = 1)
        If Not isSpendMode Then
            e.Cancel = True
            Return
        End If

        Dim isDraftSpend As Boolean = isSpendMode AndAlso status = "غير معتمد"
        Dim isApprovedOrCanceled As Boolean = (status = "معتمد" OrElse status = "ملغى")

        ctxPickBeneficiary.Visible = isSpendMode
        ctxEditStamp.Visible = isSpendMode
        ctxEditStatement.Visible = isSpendMode
        ctxPreviewJournal.Visible = isSpendMode
        ctxApprove.Visible = isSpendMode
        ctxCancelEntry.Visible = isSpendMode
        ctxPrintVoucher.Visible = isSpendMode
        ctxPrintOfficialVoucher.Visible = isSpendMode

        ctxPickBeneficiary.Enabled = isDraftSpend
        ctxEditStamp.Enabled = isDraftSpend
        ctxEditStatement.Enabled = isSpendMode AndAlso status <> "معتمد" AndAlso status <> "غير معتمد"
        ctxPreviewJournal.Enabled = isSpendMode
        ctxApprove.Enabled = isDraftSpend
        ctxCancelEntry.Enabled = isSpendMode AndAlso status = "معتمد"
        ctxPrintVoucher.Enabled = isSpendMode AndAlso isApprovedOrCanceled
        ctxPrintOfficialVoucher.Enabled = isSpendMode AndAlso isApprovedOrCanceled
    End Sub

    Private Sub ctxPickBeneficiary_Click(sender As Object, e As EventArgs)
        If dgvEntries.CurrentRow Is Nothing Then Return
        PickPaymentAccountForGridRow(dgvEntries.CurrentRow.Index)
    End Sub

    Private Sub SetStampControlsVisible(visible As Boolean)
        EnsureStampAccountNameDisplay()
        If chkHasStamp IsNot Nothing Then chkHasStamp.Visible = visible
        If lblStampPercent IsNot Nothing Then lblStampPercent.Visible = visible
        If txtStampPercent IsNot Nothing Then txtStampPercent.Visible = visible
        ' If lblStampAccount IsNot Nothing Then lblStampAccount.Visible = visible
        If txtStampAccountCode IsNot Nothing Then txtStampAccountCode.Visible = visible
        If txtStampAccountName IsNot Nothing Then txtStampAccountName.Visible = visible
        If btnPickStampAccount IsNot Nothing Then btnPickStampAccount.Visible = visible
    End Sub

    Private Sub SetBudgetItemExpenseAccountControlsVisible(visible As Boolean)
        If lblLinkedBudgetAccount IsNot Nothing Then lblLinkedBudgetAccount.Visible = visible
        If txtLinkedBudgetAccount IsNot Nothing Then txtLinkedBudgetAccount.Visible = visible
    End Sub

    Private Sub UpdateStampControls()
        If txtStampPercent IsNot Nothing Then txtStampPercent.Enabled = False
        If txtStampAccountCode IsNot Nothing Then txtStampAccountCode.Enabled = False
        If txtStampAccountName IsNot Nothing Then txtStampAccountName.Enabled = False
        If btnPickStampAccount IsNot Nothing Then btnPickStampAccount.Enabled = False
    End Sub

    Private Sub EnsureStampAccountNameDisplay()
        If txtStampAccountName IsNot Nothing OrElse cardForm Is Nothing Then Return

        txtStampAccountName = New TextBox With {
            .Name = "txtStampAccountName",
            .ReadOnly = True,
            .BackColor = Color.WhiteSmoke,
            .BorderStyle = BorderStyle.FixedSingle,
            .Font = New Font("Segoe UI Semibold", 9.5!, FontStyle.Bold),
            .RightToLeft = RightToLeft.Yes
        }

        cardForm.Controls.Add(txtStampAccountName)
        txtStampAccountName.SetBounds(420, 113, 235, 25)
        txtStampAccountName.BringToFront()
    End Sub

    Private Function ApplyDefaultStampSettings(Optional showWarning As Boolean = True) As Boolean
        If EntryMode <> 1 Then Return True
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
    ' Cascading Doors/Chapters/Items
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
    ' Balance Logic
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

    Private Function LoadBudgetItemExpenseAccountsForItem(budgetItemId As Integer) As DataTable
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT
    ABI.AccountId,
    LTRIM(RTRIM(CONVERT(NVARCHAR(40), A.ACC_CODE))) AS AccountCode,
    A.ACC_NAME AS AccountName,
    ISNULL(ABI.IsDefault, 0) AS IsDefault,
    LTRIM(RTRIM(CONVERT(NVARCHAR(40), A.ACC_CODE))) + N' - ' + A.ACC_NAME +
        CASE WHEN ISNULL(ABI.IsDefault, 0) = 1 THEN N' (افتراضي)' ELSE N'' END AS AccountText
FROM dbo.Account_Budget_Items ABI
INNER JOIN dbo.ACCOUNTS_TREE A ON A.T_ID = ABI.AccountId
WHERE ABI.BudgetItemId = @BudgetItemId
ORDER BY ISNULL(ABI.IsDefault, 0) DESC, A.ACC_CODE;", cn)

                cmd.Parameters.Add("@BudgetItemId", SqlDbType.Int).Value = budgetItemId
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Private Function LoadBudgetItemExpenseAccountsForEntry(budgetEntryId As Integer) As DataTable
        Dim itemId As Integer = GetBudgetItemIdForEntry(budgetEntryId)
        If itemId <= 0 Then Return New DataTable()
        Return LoadBudgetItemExpenseAccountsForItem(itemId)
    End Function

    Private Function GetBudgetItemIdForEntry(budgetEntryId As Integer) As Integer
        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT TOP 1 BudgetItemId
FROM dbo.Budget_Entries
WHERE BudgetEntryId = @BudgetEntryId;", cn)

                cmd.Parameters.Add("@BudgetEntryId", SqlDbType.Int).Value = budgetEntryId
                cn.Open()

                Dim value = cmd.ExecuteScalar()
                If value Is Nothing OrElse value Is DBNull.Value Then Return 0
                Return Convert.ToInt32(value)
            End Using
        End Using
    End Function

    Private Function BudgetItemExpenseAccountText(row As DataRow) As String
        If row Is Nothing Then Return ""

        Dim accountCode As String = CellText(row, "AccountCode")
        Dim accountName As String = CellText(row, "AccountName")

        If String.IsNullOrWhiteSpace(accountCode) Then Return accountName
        If String.IsNullOrWhiteSpace(accountName) Then Return accountCode

        Return accountCode & " - " & accountName
    End Function

    Private Function IsBudgetItemExpenseAccountDefault(row As DataRow) As Boolean
        If row Is Nothing Then Return False

        Dim isDefault As Boolean = False
        If Boolean.TryParse(CellText(row, "IsDefault"), isDefault) Then Return isDefault

        Dim n As Integer = 0
        If Integer.TryParse(CellText(row, "IsDefault"), n) Then Return n <> 0

        Return False
    End Function

    Private Function FindDefaultBudgetItemExpenseAccount(accounts As DataTable) As DataRow
        If accounts Is Nothing Then Return Nothing

        For Each row As DataRow In accounts.Rows
            If IsBudgetItemExpenseAccountDefault(row) Then Return row
        Next

        Return Nothing
    End Function

    Private Sub ResetBudgetItemExpenseAccountPreview(Optional text As String = "")
        If txtLinkedBudgetAccount Is Nothing Then Exit Sub

        txtLinkedBudgetAccount.Text = text
        txtLinkedBudgetAccount.ForeColor = Color.Blue
    End Sub

    Private Sub UpdateBudgetItemExpenseAccountPreview()
        If txtLinkedBudgetAccount Is Nothing Then Exit Sub

        If EntryMode <> 1 Then
            ResetBudgetItemExpenseAccountPreview()
            Exit Sub
        End If

        If cmbItems.SelectedIndex < 0 OrElse cmbItems.SelectedValue Is Nothing Then
            ResetBudgetItemExpenseAccountPreview("اختر بند الصرف لعرض حساب مصروف البند")
            Exit Sub
        End If

        Dim itemId As Integer = Convert.ToInt32(cmbItems.SelectedValue)
        Dim accounts As DataTable = LoadBudgetItemExpenseAccountsForItem(itemId)

        If accounts.Rows.Count = 0 Then
            txtLinkedBudgetAccount.Text = "لا يوجد حساب مصروف مرتبط بهذا البند"
            txtLinkedBudgetAccount.ForeColor = Color.DarkRed
            SetStatus("يجب ربط البند بحساب من الدليل قبل اعتماد الصرف")
        ElseIf accounts.Rows.Count = 1 Then
            txtLinkedBudgetAccount.Text = BudgetItemExpenseAccountText(accounts.Rows(0))
            txtLinkedBudgetAccount.ForeColor = Color.DarkGreen
        Else
            Dim defaultRow As DataRow = FindDefaultBudgetItemExpenseAccount(accounts)
            If defaultRow Is Nothing Then
                txtLinkedBudgetAccount.Text = "عدة حسابات مصروف مرتبطة - اختر الحساب عند الاعتماد"
            Else
                txtLinkedBudgetAccount.Text = "عدة حسابات مصروف مرتبطة - الافتراضي: " & BudgetItemExpenseAccountText(defaultRow)
            End If

            txtLinkedBudgetAccount.ForeColor = Color.DarkOrange
        End If
    End Sub


    '=========================
    ' Grid
    '=========================
    Private Function LoadPaymentAccountsForGrid() As DataTable
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT AccountCode, AccountText
FROM
(
    SELECT
        N'' AS AccountCode,
        N'-- بدون حساب دفع --' AS AccountText,
        0 AS SortOrder

    UNION ALL

    SELECT
        LTRIM(RTRIM(CONVERT(NVARCHAR(40), ACC_CODE))) AS AccountCode,
        LTRIM(RTRIM(CONVERT(NVARCHAR(40), ACC_CODE))) + N' - ' + ACC_NAME AS AccountText,
        1 AS SortOrder
    FROM dbo.ACCOUNTS_TREE
) X
ORDER BY SortOrder, AccountCode;", cn)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Private Sub ConfigurePaymentAccountGridColumn()
        If Not dgvEntries.Columns.Contains("ContraAccountCode") Then Exit Sub

        If EntryMode <> 1 Then
            dgvEntries.Columns("ContraAccountCode").Visible = False
            If dgvEntries.Columns.Contains("PickContraAccount") Then
                dgvEntries.Columns("PickContraAccount").Visible = False
            End If
            Exit Sub
        End If

        Dim accountColumn As DataGridViewColumn = dgvEntries.Columns("ContraAccountCode")
        Dim insertIndex As Integer = accountColumn.Index
        If dgvEntries.Columns.Contains("ItemName") Then
            insertIndex = dgvEntries.Columns("ItemName").Index + 1
        End If

        accountColumn.Visible = True
        accountColumn.HeaderText = "حساب الدفع / المستفيد"
        accountColumn.ReadOnly = True
        accountColumn.FillWeight = 180

        If Not dgvEntries.Columns.Contains("PickContraAccount") Then
            Dim btnCol As New DataGridViewButtonColumn()
            btnCol.Name = "PickContraAccount"
            btnCol.HeaderText = ""
            btnCol.Text = "..."
            btnCol.UseColumnTextForButtonValue = True
            btnCol.ReadOnly = True
            btnCol.Width = 34
            btnCol.MinimumWidth = 34
            btnCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            dgvEntries.Columns.Add(btnCol)
        End If

        dgvEntries.Columns("PickContraAccount").Visible = True
        dgvEntries.Columns("PickContraAccount").Width = 30
        dgvEntries.Columns("PickContraAccount").MinimumWidth = 30
        dgvEntries.Columns("PickContraAccount").AutoSizeMode = DataGridViewAutoSizeColumnMode.None

        If insertIndex < dgvEntries.Columns.Count Then
            dgvEntries.Columns("PickContraAccount").DisplayIndex = insertIndex
            accountColumn.DisplayIndex = Math.Min(insertIndex + 1, dgvEntries.Columns.Count - 1)
        End If
    End Sub

    Private Sub LoadEntriesGrid(Optional year As Integer = 0, Optional itemId As Integer = 0)
        IsLoadingEntriesGrid = True
        Try

            Dim STR_WHERE As String = " AND e.EntryType = " & EntryMode

            Dim dt As New DataTable()
            Dim sql As String = "SELECT TOP 500
    e.BudgetEntryId,
    e.FiscalYear,
    d.DoorCode,
    c.ChapterCode,
    i.ItemCode,
    i.ItemName,
    CASE e.EntryType WHEN 1 THEN N'صرف' WHEN 2 THEN N'حجز' ELSE N'-' END AS EntryTypeName,
    ct.CommitmentTypeName,
    e.Amount,
    e.EntryDate,
    e.CreatedAt,
    e.AccountingEntryId,
    e.ReversalJournalId,
    e.ReserveEntryId,
    e.SourceRefNo,
    e.SourceTable,
    e.HasStamp,
    e.StampPercent,
    e.StampAccountCode,
    e.StampAmount,
    ISNULL(SA.ACC_NAME, N'') AS StampAccountName,
    e.BeneficiaryType,
    ISNULL(BT.BeneficiaryTypeName, N'') AS BeneficiaryTypeName,
    e.BeneficiaryId,
    e.PaymentMethodId,
    ISNULL(PM.PaymentMethodName, N'') AS PaymentMethodName,
    ISNULL(e.InvoiceNo, N'') AS InvoiceNo,
    ISNULL(e.DocumentNo, N'') AS DocumentNo,
    ISNULL(e.SpendStatement, N'') AS SpendStatement,
    COALESCE(
        NULLIF(LTRIM(RTRIM(e.ContraAccountCode)), N''),
        NULLIF(LTRIM(RTRIM(i.ContraAccountCode)), N''),
        N''
    ) AS ContraAccountCode,
    ISNULL(PA.ACC_NAME, N'') AS PaymentAccountName,
    e.ApprovedAt,
    e.ApprovedBy,
    ISNULL(UInput.UserName, CONVERT(NVARCHAR(50), e.ApprovedBy)) AS ApprovedByName,
    e.CanceledAt,
    e.CanceledBy,
    e.CancelReason,

    CASE
        WHEN e.EntryType = 2 THEN
            CASE e.StatusId
                WHEN 0 THEN N'مسودة'
                WHEN 1 THEN N'محجوز'
                WHEN 2 THEN N'ملغى'
                WHEN 3 THEN N'مغلق'
                WHEN 4 THEN N'مرفوض'
                ELSE N'غير معروف'
            END
        ELSE
            CASE
                WHEN e.ReversalJournalId IS NOT NULL THEN N'ملغى'
                WHEN EXISTS (
                    SELECT 1
                    FROM dbo.ACC_BALANCE_BUDGET_LINK bl
                    WHERE bl.BudgetEntryId = e.BudgetEntryId
                      AND bl.LinkType = 4
                ) THEN N'ملغى'
                WHEN e.AccountingEntryId IS NULL THEN N'غير معتمد'
                WHEN EXISTS (
                    SELECT 1
                    FROM ACC_BALANCE_MASTER jm
                    WHERE jm.T_ID = e.AccountingEntryId
                ) THEN N'معتمد'
                ELSE N'غير معتمد'
            END
    END AS EntryStatus,

    e.Notes
FROM Budget_Entries e
JOIN Budget_Items i ON e.BudgetItemId = i.BudgetItemId
JOIN Budget_Chapters c ON i.ChapterId = c.ChapterId
JOIN Budget_Doors d ON c.DoorId = d.DoorId
LEFT JOIN dbo.Budget_CommitmentTypes ct ON ct.CommitmentTypeId = e.CommitmentTypeId
LEFT JOIN dbo.Budget_BeneficiaryTypes BT ON BT.BeneficiaryType = e.BeneficiaryType
LEFT JOIN dbo.Budget_PaymentMethods PM ON PM.PaymentMethodId = e.PaymentMethodId
LEFT JOIN dbo.ACCOUNTS_TREE SA ON SA.ACC_CODE = e.StampAccountCode
LEFT JOIN dbo.ACCOUNTS_TREE PA
    ON PA.ACC_CODE = COALESCE(
        NULLIF(LTRIM(RTRIM(e.ContraAccountCode)), N''),
        NULLIF(LTRIM(RTRIM(i.ContraAccountCode)), N'')
    )
LEFT JOIN dbo.Users UInput ON UInput.user_id = e.ApprovedBy
WHERE (@Y = 0 OR e.FiscalYear = @Y)
  AND (@ItemId = 0 OR e.BudgetItemId = @ItemId) " & STR_WHERE &
" ORDER BY e.BudgetEntryId DESC;"

            Using cn As New SqlConnection(ConnStr)
                Using cmd As New SqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@Y", year)
                    cmd.Parameters.AddWithValue("@ItemId", itemId)

                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using

            dgvEntries.DataSource = dt
            ApplyEntriesFilter()

            If dgvEntries.Columns.Count > 0 Then
                dgvEntries.Columns("BudgetEntryId").Visible = False
                dgvEntries.Columns("ReversalJournalId").Visible = False
                dgvEntries.Columns("ReserveEntryId").Visible = False
                dgvEntries.Columns("SourceTable").Visible = False
                dgvEntries.Columns("HasStamp").Visible = False
                dgvEntries.Columns("StampPercent").Visible = False
                dgvEntries.Columns("StampAccountCode").Visible = False
                dgvEntries.Columns("StampAmount").Visible = False
                dgvEntries.Columns("StampAccountName").Visible = False
                dgvEntries.Columns("BeneficiaryType").Visible = False
                dgvEntries.Columns("BeneficiaryTypeName").Visible = False
                dgvEntries.Columns("BeneficiaryId").Visible = False
                dgvEntries.Columns("PaymentMethodId").Visible = False
                dgvEntries.Columns("PaymentMethodName").Visible = False
                dgvEntries.Columns("InvoiceNo").Visible = False
                dgvEntries.Columns("DocumentNo").Visible = False
                dgvEntries.Columns("PaymentAccountName").Visible = False
                dgvEntries.Columns("ApprovedAt").Visible = False
                dgvEntries.Columns("ApprovedBy").Visible = False
                dgvEntries.Columns("ApprovedByName").Visible = False
                dgvEntries.Columns("CanceledAt").Visible = False
                dgvEntries.Columns("CanceledBy").Visible = False
                dgvEntries.Columns("CancelReason").Visible = False

                dgvEntries.Columns("DoorCode").HeaderText = "باب"
                dgvEntries.Columns("ChapterCode").HeaderText = "فصل"
                dgvEntries.Columns("ItemCode").HeaderText = "كود"
                dgvEntries.Columns("ItemName").HeaderText = "البند"
                dgvEntries.Columns("EntryTypeName").HeaderText = "النوع"
                dgvEntries.Columns("CommitmentTypeName").HeaderText = "نوع الحجز"
                dgvEntries.Columns("Amount").HeaderText = "المبلغ"
                dgvEntries.Columns("EntryDate").HeaderText = "تاريخ المعاملة"
                dgvEntries.Columns("CreatedAt").HeaderText = "تاريخ الإنشاء"
                dgvEntries.Columns("AccountingEntryId").HeaderText = "رقم القيد"
                dgvEntries.Columns("SourceRefNo").HeaderText = "رقم المصدر"
                dgvEntries.Columns("EntryStatus").HeaderText = "الحالة"
                dgvEntries.Columns("SpendStatement").HeaderText = "بيان الصرف"
                dgvEntries.Columns("Notes").HeaderText = "ملاحظات"

                For Each col As DataGridViewColumn In dgvEntries.Columns
                    col.ReadOnly = True
                Next

                ConfigurePaymentAccountGridColumn()

                If EntryMode = 1 Then
                    dgvEntries.Columns("CommitmentTypeName").Visible = False
                    dgvEntries.Columns("SourceRefNo").Visible = False
                End If

                dgvEntries.Columns("Amount").DefaultCellStyle.Format = "N3"
                dgvEntries.Columns("EntryDate").DefaultCellStyle.Format = "yyyy-MM-dd HH:mm"
                dgvEntries.Columns("CreatedAt").DefaultCellStyle.Format = "yyyy-MM-dd HH:mm"

                dgvEntries.Columns("EntryStatus").Width = 110
                dgvEntries.Columns("EntryStatus").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                dgvEntries.Columns("SpendStatement").MinimumWidth = 180
                dgvEntries.Columns("SpendStatement").DefaultCellStyle.WrapMode = DataGridViewTriState.True


                UcGridColumnsSelector1.BindGrid(
dgvEntries,
New List(Of String) From {""},
Me.Name.ToString
)
            End If

            dgvEntries.ClearSelection()
            UpdateSelectedDetails()
            SetStatus($"تم تحميل {dt.Rows.Count} عملية")
        Finally
            IsLoadingEntriesGrid = False
        End Try
    End Sub


    Private Sub dgvEntries_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) _
    Handles dgvEntries.CellFormatting

        If e.ColumnIndex < 0 Then Return
        If e.RowIndex >= 0 Then ApplyEntryStatusCellStyle(e)

        If dgvEntries.Columns(e.ColumnIndex).Name = "ContraAccountCode" AndAlso e.RowIndex >= 0 Then
            Dim accountCode As String = Convert.ToString(e.Value).Trim()
            Dim accountName As String = GridCellText(dgvEntries.Rows(e.RowIndex), "PaymentAccountName")

            If Not String.IsNullOrWhiteSpace(accountCode) AndAlso Not String.IsNullOrWhiteSpace(accountName) Then
                e.Value = accountCode & " - " & accountName
                e.FormattingApplied = True
            End If
        End If
    End Sub

    Private Sub ApplyEntryStatusCellStyle(e As DataGridViewCellFormattingEventArgs)
        If e.RowIndex < 0 OrElse e.RowIndex >= dgvEntries.Rows.Count Then Return

        Dim row As DataGridViewRow = dgvEntries.Rows(e.RowIndex)
        Dim status As String = GridCellText(row, "EntryStatus")

        Dim backColor As Color = Color.White
        Dim foreColor As Color = Color.FromArgb(15, 23, 42)
        Dim selectionBackColor As Color = Color.FromArgb(226, 232, 240)

        Select Case status
            Case "معتمد", "محجوز"
                backColor = Color.FromArgb(232, 247, 239)
                foreColor = Color.FromArgb(22, 101, 52)
                selectionBackColor = Color.FromArgb(187, 247, 208)
            Case "غير معتمد", "مسودة"
                backColor = Color.FromArgb(255, 247, 237)
                foreColor = Color.FromArgb(154, 52, 18)
                selectionBackColor = Color.FromArgb(254, 215, 170)
            Case "ملغى", "مرفوض"
                backColor = Color.FromArgb(254, 242, 242)
                foreColor = Color.FromArgb(153, 27, 27)
                selectionBackColor = Color.FromArgb(254, 202, 202)
            Case "مغلق"
                backColor = Color.FromArgb(239, 246, 255)
                foreColor = Color.FromArgb(30, 64, 175)
                selectionBackColor = Color.FromArgb(191, 219, 254)
        End Select

        e.CellStyle.BackColor = backColor
        e.CellStyle.ForeColor = foreColor
        e.CellStyle.SelectionBackColor = selectionBackColor
        e.CellStyle.SelectionForeColor = foreColor
        e.CellStyle.Font = dgvEntries.DefaultCellStyle.Font

        If dgvEntries.Columns(e.ColumnIndex).Name = "EntryStatus" Then
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If
    End Sub

    Private Sub dgvEntries_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvEntries.CellMouseDown
        If e.Button <> MouseButtons.Right Then Return
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return

        dgvEntries.ClearSelection()
        dgvEntries.Rows(e.RowIndex).Selected = True
        dgvEntries.CurrentCell = dgvEntries.Rows(e.RowIndex).Cells(e.ColumnIndex)
    End Sub

    Private Sub dgvEntries_SelectionChanged(sender As Object, e As EventArgs) _
    Handles dgvEntries.SelectionChanged

        If dgvEntries.CurrentRow Is Nothing Then
            If btnUpdateSpendStatement IsNot Nothing Then btnUpdateSpendStatement.Enabled = False
            If btnEditStamp IsNot Nothing Then btnEditStamp.Enabled = False
            UpdateSelectedDetails()
            Exit Sub
        End If

        Dim status As String = dgvEntries.CurrentRow.Cells("EntryStatus").Value.ToString()

        If EntryMode = 1 Then
            BtnApprove.Enabled = (status = "غير معتمد")
            BtnCancelEntry.Enabled = (status = "معتمد")
            If btnPrintVoucher IsNot Nothing Then btnPrintVoucher.Enabled = (status = "معتمد" OrElse status = "ملغى")
            If btnPrintOfficialVoucher IsNot Nothing Then btnPrintOfficialVoucher.Enabled = (status = "معتمد" OrElse status = "ملغى")
            If btnUpdateSpendStatement IsNot Nothing Then btnUpdateSpendStatement.Enabled = (status <> "معتمد" AndAlso status <> "غير معتمد")
            If btnEditStamp IsNot Nothing Then btnEditStamp.Enabled = (status = "غير معتمد")
        Else
            BtnApprove.Enabled = False
            BtnCancelEntry.Enabled = False
            If btnPrintVoucher IsNot Nothing Then btnPrintVoucher.Enabled = False
            If btnPrintOfficialVoucher IsNot Nothing Then btnPrintOfficialVoucher.Enabled = False
            If btnUpdateSpendStatement IsNot Nothing Then btnUpdateSpendStatement.Enabled = False
            If btnEditStamp IsNot Nothing Then btnEditStamp.Enabled = False
        End If

        UpdateSelectedDetails()
    End Sub

    Private Sub dgvEntries_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgvEntries.CellBeginEdit
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub
        If Not dgvEntries.Columns(e.ColumnIndex).Name.Equals("ContraAccountCode", StringComparison.OrdinalIgnoreCase) Then Exit Sub

        e.Cancel = True
        SetStatus("استخدم زر الاختيار بجانب حساب الدفع / المستفيد")
    End Sub

    Private Sub dgvEntries_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEntries.CellContentClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub
        If Not dgvEntries.Columns(e.ColumnIndex).Name.Equals("PickContraAccount", StringComparison.OrdinalIgnoreCase) Then Exit Sub

        PickPaymentAccountForGridRow(e.RowIndex)
    End Sub

    Private Sub PickPaymentAccountForGridRow(rowIndex As Integer)
        If rowIndex < 0 OrElse rowIndex >= dgvEntries.Rows.Count Then Exit Sub

        Dim row As DataGridViewRow = dgvEntries.Rows(rowIndex)
        Dim status As String = GridCellText(row, "EntryStatus")
        If status <> "غير معتمد" Then
            SetStatus("يمكن تحديد حساب الدفع / المستفيد للإذن غير المعتمد فقط")
            Exit Sub
        End If

        Dim budgetEntryId As Integer = 0
        If Not Integer.TryParse(GridCellText(row, "BudgetEntryId"), budgetEntryId) Then Exit Sub

        ACC_CODE_Search = ""
        ACC_NAME_Search = ""

        Dim frm As New BALANCE_SEARCH
        frm.ShowDialog()

        If String.IsNullOrWhiteSpace(ACC_CODE_Search) Then Exit Sub

        Dim paymentAccountCode As String = ACC_CODE_Search.Trim()
        SavePaymentAccountForEntry(budgetEntryId, paymentAccountCode)

        row.Cells("ContraAccountCode").Value = paymentAccountCode
        If dgvEntries.Columns.Contains("PaymentAccountName") Then
            row.Cells("PaymentAccountName").Value = ACC_NAME_Search.Trim()
        End If

        SetStatus("تم حفظ حساب الدفع / المستفيد للإذن رقم " & budgetEntryId.ToString())
        UpdateSelectedDetails()
    End Sub

    Private Sub dgvEntries_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvEntries.CurrentCellDirtyStateChanged
        If dgvEntries.CurrentCell Is Nothing Then Exit Sub
        If Not dgvEntries.IsCurrentCellDirty Then Exit Sub
        If Not dgvEntries.Columns(dgvEntries.CurrentCell.ColumnIndex).Name.Equals("ContraAccountCode", StringComparison.OrdinalIgnoreCase) Then Exit Sub

        dgvEntries.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub dgvEntries_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEntries.CellValueChanged
        If IsLoadingEntriesGrid Then Exit Sub
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub
        If Not dgvEntries.Columns(e.ColumnIndex).Name.Equals("ContraAccountCode", StringComparison.OrdinalIgnoreCase) Then Exit Sub

        Dim row As DataGridViewRow = dgvEntries.Rows(e.RowIndex)
        Dim status As String = GridCellText(row, "EntryStatus")
        If status <> "غير معتمد" Then Exit Sub

        Dim budgetEntryId As Integer = 0
        If Not Integer.TryParse(GridCellText(row, "BudgetEntryId"), budgetEntryId) Then Exit Sub

        Dim paymentAccountCode As String = Convert.ToString(row.Cells("ContraAccountCode").Value).Trim()
        SavePaymentAccountForEntry(budgetEntryId, paymentAccountCode)

        If dgvEntries.Columns.Contains("PaymentAccountName") Then
            row.Cells("PaymentAccountName").Value = GetAccountName(paymentAccountCode)
        End If

        SetStatus("تم حفظ حساب الدفع / المستفيد للإذن رقم " & budgetEntryId.ToString())
        UpdateSelectedDetails()
    End Sub

    Private Sub dgvEntries_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvEntries.DataError
        e.ThrowException = False
        SetStatus("قيمة حساب الدفع / المستفيد غير موجودة في قائمة الحسابات")
    End Sub

    Private Sub txtEntriesFilter_TextChanged(sender As Object, e As EventArgs) Handles txtEntriesFilter.TextChanged
        ApplyEntriesFilter()
    End Sub

    Private Sub ApplyEntriesFilter()
        If dgvEntries Is Nothing OrElse dgvEntries.DataSource Is Nothing Then Return

        Dim dt As DataTable = TryCast(dgvEntries.DataSource, DataTable)
        If dt Is Nothing Then Return

        Dim filterText As String = If(txtEntriesFilter Is Nothing, "", txtEntriesFilter.Text.Trim())
        If String.IsNullOrWhiteSpace(filterText) Then
            dt.DefaultView.RowFilter = ""
            Return
        End If

        Dim safeText As String = EscapeRowFilterLikeValue(filterText)
        Dim parts As New List(Of String)

        For Each col As DataColumn In dt.Columns
            Select Case Type.GetTypeCode(col.DataType)
                Case TypeCode.String
                    parts.Add(String.Format("CONVERT([{0}], 'System.String') LIKE '%{1}%'", col.ColumnName.Replace("]", "]]"), safeText))
                Case TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.Decimal, TypeCode.Double, TypeCode.Single, TypeCode.DateTime
                    parts.Add(String.Format("CONVERT([{0}], 'System.String') LIKE '%{1}%'", col.ColumnName.Replace("]", "]]"), safeText))
            End Select
        Next

        If parts.Count = 0 Then
            dt.DefaultView.RowFilter = ""
            Return
        End If

        Try
            dt.DefaultView.RowFilter = String.Join(" OR ", parts)
        Catch
            dt.DefaultView.RowFilter = ""
        End Try
        UpdateSelectedDetails()
    End Sub

    Private Function EscapeRowFilterLikeValue(value As String) As String
        Return value.Replace("'", "''").
                     Replace("[", "[[]").
                     Replace("%", "[%]").
                     Replace("*", "[*]")
    End Function

    Private Sub UpdateSelectedDetails()
        If txtSelectedDetails Is Nothing Then Exit Sub

        If dgvEntries.CurrentRow Is Nothing OrElse dgvEntries.CurrentRow.IsNewRow Then
            txtSelectedDetails.Text = "لا يوجد صف محدد."
            Exit Sub
        End If

        Dim row As DataGridViewRow = dgvEntries.CurrentRow
        Dim stampText As String = "لا توجد"
        If GridCellDecimal(row, "StampAmount") > 0D Then
            stampText = GridCellText(row, "StampAmount") & " / " &
                        GridCellText(row, "StampPercent") & "% / " &
                        CombineGridCodeName(row, "StampAccountCode", "StampAccountName")
        End If

        Dim parts As New List(Of String)
        parts.Add("رقم العملية: " & GridCellText(row, "BudgetEntryId"))
        parts.Add("الحالة: " & GridCellText(row, "EntryStatus"))
        parts.Add("النوع: " & GridCellText(row, "EntryTypeName"))
        parts.Add("التاريخ: " & GridCellText(row, "EntryDate"))
        parts.Add("المبلغ: " & GridCellText(row, "Amount"))
        parts.Add("القيد: " & GridCellText(row, "AccountingEntryId"))
        parts.Add("الحجز المرتبط: " & GridCellText(row, "ReserveEntryId"))
        parts.Add("المستفيد: " & GridCellText(row, "BeneficiaryTypeName") & " " & GridCellText(row, "BeneficiaryId"))
        parts.Add("طريقة الدفع: " & GridCellText(row, "PaymentMethodName"))
        parts.Add("حساب الدفع / المستفيد: " & CombineGridCodeName(row, "ContraAccountCode", "PaymentAccountName"))
        parts.Add("فاتورة/مستند: " & GridCellText(row, "InvoiceNo") & " / " & GridCellText(row, "DocumentNo"))
        parts.Add("المصدر: " & GridCellText(row, "SourceRefNo") & " " & GridCellText(row, "SourceTable"))
        If EntryMode = 1 Then
            parts.Add("حساب مصروف البند: " & DescribeBudgetItemExpenseAccountsForEntry(Convert.ToInt32(row.Cells("BudgetEntryId").Value)))
        End If
        parts.Add("الدمغة: " & stampText)
        parts.Add("المعتمد: " & GridCellText(row, "ApprovedByName") & " " & GridCellText(row, "ApprovedAt"))
        parts.Add("الإلغاء: " & GridCellText(row, "CancelReason"))
        parts.Add("بيان الصرف: " & GridCellText(row, "SpendStatement"))
        parts.Add("ملاحظات: " & GridCellText(row, "Notes"))

        txtSelectedDetails.Text = String.Join("    |    ", parts.Where(Function(x) Not x.EndsWith(": ") AndAlso Not x.EndsWith(":  / ") AndAlso Not x.EndsWith(":  ")))
    End Sub

    Private Sub LoadSelectedStampInputs()
        If dgvEntries.CurrentRow Is Nothing OrElse dgvEntries.CurrentRow.IsNewRow Then Exit Sub
        If chkHasStamp Is Nothing Then Exit Sub

        Dim hasStampText As String = GridCellText(dgvEntries.CurrentRow, "HasStamp")
        Dim hasStamp As Boolean = False
        Boolean.TryParse(hasStampText, hasStamp)
        If GridCellDecimal(dgvEntries.CurrentRow, "StampAmount") > 0D OrElse GridCellDecimal(dgvEntries.CurrentRow, "StampPercent") > 0D Then
            hasStamp = True
        End If

        chkHasStamp.Checked = hasStamp
        If txtStampPercent IsNot Nothing Then txtStampPercent.Text = If(hasStamp, GridCellText(dgvEntries.CurrentRow, "StampPercent"), "")
        If txtStampAccountCode IsNot Nothing Then txtStampAccountCode.Text = If(hasStamp, GridCellText(dgvEntries.CurrentRow, "StampAccountCode"), "")
        If txtStampAccountName IsNot Nothing Then txtStampAccountName.Text = If(hasStamp, GridCellText(dgvEntries.CurrentRow, "StampAccountName"), "")
        UpdateStampControls()
    End Sub

    Private Function GridCellText(row As DataGridViewRow, columnName As String) As String
        If row Is Nothing OrElse row.DataGridView Is Nothing Then Return ""
        If Not row.DataGridView.Columns.Contains(columnName) Then Return ""
        Dim value = row.Cells(columnName).Value
        If value Is Nothing OrElse value Is DBNull.Value Then Return ""

        If TypeOf value Is Date OrElse TypeOf value Is DateTime Then
            Return Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm")
        End If

        If TypeOf value Is Decimal OrElse TypeOf value Is Double OrElse TypeOf value Is Single Then
            Return Convert.ToDecimal(value).ToString("N3")
        End If

        Return value.ToString().Trim()
    End Function

    Private Function GridCellDecimal(row As DataGridViewRow, columnName As String) As Decimal
        Dim value As Decimal
        Decimal.TryParse(GridCellText(row, columnName), value)
        Return value
    End Function

    Private Function CombineGridCodeName(row As DataGridViewRow, codeColumn As String, nameColumn As String) As String
        Dim code As String = GridCellText(row, codeColumn)
        Dim name As String = GridCellText(row, nameColumn)
        If String.IsNullOrWhiteSpace(code) Then Return name
        If String.IsNullOrWhiteSpace(name) Then Return code
        Return code & " - " & name
    End Function



    '=========================
    ' Helpers
    '=========================
    Private Sub ClearForm(Optional keepYear As Boolean = True)
        txtAmount.Text = ""
        txtNotes.Text = ""
        If txtSpendStatement IsNot Nothing Then txtSpendStatement.Text = ""
        If cmbCommitmentTypes IsNot Nothing Then cmbCommitmentTypes.SelectedIndex = -1
        If txtSourceRef IsNot Nothing Then txtSourceRef.Text = ""
        If txtSourceTable IsNot Nothing Then txtSourceTable.Text = ""
        If chkHasStamp IsNot Nothing Then chkHasStamp.Checked = False
        If txtStampPercent IsNot Nothing Then txtStampPercent.Text = ""
        If txtStampAccountCode IsNot Nothing Then txtStampAccountCode.Text = ""
        If txtStampAccountName IsNot Nothing Then txtStampAccountName.Text = ""
        If dtpEntryDate IsNot Nothing Then dtpEntryDate.Value = DateTime.Now
        ResetBudgetItemExpenseAccountPreview()
        UpdateStampControls()
        cmbDoors.SelectedIndex = -1
        cmbChapters.DataSource = Nothing
        cmbItems.DataSource = Nothing

        If Not keepYear Then
            cmbFiscalYear.SelectedItem = DateTime.Now.Year
        End If
    End Sub

    Private Sub UpdateSelectedSpendStatement(Optional newStatement As String = Nothing)
        If EntryMode <> 1 Then
            MessageBox.Show("تعديل بيان الصرف متاح في وضع الصرف فقط", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If dgvEntries.CurrentRow Is Nothing OrElse dgvEntries.CurrentRow.IsNewRow Then
            MessageBox.Show("اختر عملية صرف من القائمة أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedStatus As String = GridCellText(dgvEntries.CurrentRow, "EntryStatus")
        If selectedStatus = "معتمد" Then
            MessageBox.Show("لا يمكن تعديل بيان عملية صرف معتمدة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If selectedStatus = "غير معتمد" Then
            MessageBox.Show("لا يمكن تعديل بيان عملية صرف غير معتمدة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim budgetEntryId As Integer = Convert.ToInt32(dgvEntries.CurrentRow.Cells("BudgetEntryId").Value)
        Dim spendStatementValue As Object = DBNull.Value

        If newStatement Is Nothing Then
            newStatement = If(txtSpendStatement Is Nothing, "", txtSpendStatement.Text)
        End If

        If Not String.IsNullOrWhiteSpace(newStatement) Then
            spendStatementValue = newStatement.Trim()
        End If

        Using cn As New SqlConnection(ConnStr)
            cn.Open()
            Using cmd As New SqlCommand("
UPDATE dbo.Budget_Entries
SET SpendStatement = @SpendStatement
WHERE BudgetEntryId = @BudgetEntryId
  AND EntryType = 1;", cn)
                cmd.Parameters.Add("@SpendStatement", SqlDbType.NVarChar, 500).Value = spendStatementValue
                cmd.Parameters.AddWithValue("@BudgetEntryId", budgetEntryId)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        If dgvEntries.Columns.Contains("SpendStatement") Then
            dgvEntries.CurrentRow.Cells("SpendStatement").Value = If(spendStatementValue Is DBNull.Value, "", spendStatementValue)
        End If

        If txtSpendStatement IsNot Nothing Then txtSpendStatement.Text = If(spendStatementValue Is DBNull.Value, "", spendStatementValue.ToString())
        UpdateSelectedDetails()
        SetStatus("تم تعديل بيان الصرف")
    End Sub

    Private Function ShowSpendStatementEditDialog(row As DataGridViewRow, ByRef newStatement As String) As Boolean
        Dim currentStatement As String = GridCellText(row, "SpendStatement")
        Dim infoText As String =
            "رقم الإذن: " & GridCellText(row, "BudgetEntryId") & Environment.NewLine &
            "البند: " & GridCellText(row, "ItemCode") & " - " & GridCellText(row, "ItemName") & Environment.NewLine &
            "المبلغ: " & GridCellText(row, "Amount") & Environment.NewLine &
            "التاريخ: " & GridCellText(row, "EntryDate") & Environment.NewLine &
            "الحالة: " & GridCellText(row, "EntryStatus")

        Using dlg As New Form()
            dlg.Text = "تعديل بيان الصرف"
            dlg.StartPosition = FormStartPosition.CenterParent
            dlg.FormBorderStyle = FormBorderStyle.FixedDialog
            dlg.MinimizeBox = False
            dlg.MaximizeBox = False
            dlg.ShowInTaskbar = False
            dlg.ClientSize = New Size(560, 340)
            dlg.Font = New Font("Segoe UI", 10.0!)
            dlg.RightToLeft = RightToLeft.Yes
            dlg.RightToLeftLayout = True

            Dim lblInfo As New Label()
            lblInfo.Text = infoText
            lblInfo.Font = New Font("Segoe UI Semibold", 9.75!, FontStyle.Bold)
            lblInfo.BackColor = Color.FromArgb(248, 250, 252)
            lblInfo.BorderStyle = BorderStyle.FixedSingle
            lblInfo.TextAlign = ContentAlignment.MiddleRight
            lblInfo.SetBounds(15, 15, 530, 105)

            Dim lblStatement As New Label()
            lblStatement.Text = "بيان الصرف"
            lblStatement.Font = New Font("Segoe UI", 9.75!, FontStyle.Bold)
            lblStatement.SetBounds(445, 130, 100, 24)

            Dim txtStatement As New TextBox()
            txtStatement.Multiline = True
            txtStatement.ScrollBars = ScrollBars.Vertical
            txtStatement.Font = New Font("Segoe UI Semibold", 10.0!, FontStyle.Bold)
            txtStatement.BorderStyle = BorderStyle.FixedSingle
            txtStatement.RightToLeft = RightToLeft.Yes
            txtStatement.Text = currentStatement
            txtStatement.SetBounds(15, 155, 530, 120)

            Dim btnSave As New Button()
            btnSave.Text = "حفظ"
            btnSave.Font = New Font("Segoe UI Semibold", 9.75!, FontStyle.Bold)
            btnSave.DialogResult = DialogResult.OK
            btnSave.SetBounds(335, 292, 100, 34)

            Dim btnCancel As New Button()
            btnCancel.Text = "إلغاء"
            btnCancel.Font = New Font("Segoe UI Semibold", 9.75!, FontStyle.Bold)
            btnCancel.DialogResult = DialogResult.Cancel
            btnCancel.SetBounds(445, 292, 100, 34)

            dlg.Controls.Add(lblInfo)
            dlg.Controls.Add(lblStatement)
            dlg.Controls.Add(txtStatement)
            dlg.Controls.Add(btnSave)
            dlg.Controls.Add(btnCancel)
            dlg.AcceptButton = btnSave
            dlg.CancelButton = btnCancel

            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return False

            newStatement = txtStatement.Text
            Return True
        End Using
    End Function

    Private Function ValidateForm() As Boolean
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

        If EntryMode = 2 Then
            If cmbCommitmentTypes Is Nothing OrElse cmbCommitmentTypes.SelectedIndex < 0 Then
                MessageBox.Show("اختر نوع الحجز", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                If cmbCommitmentTypes IsNot Nothing Then cmbCommitmentTypes.Focus()
                Return False
            End If
        End If

        Dim amt As Decimal
        If Not Decimal.TryParse(txtAmount.Text.Trim(), amt) Then
            MessageBox.Show("المبلغ غير صحيح", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtAmount.Focus()
            Return False
        End If

        If amt <= 0D Then
            MessageBox.Show("المبلغ يجب أن يكون أكبر من صفر", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtAmount.Focus()
            Return False
        End If

        If EntryMode = 1 AndAlso chkHasStamp IsNot Nothing AndAlso chkHasStamp.Checked Then
            If Not ApplyDefaultStampSettings(False) Then
                MessageBox.Show("اضبط إعدادات الدمغة الافتراضية من شاشة إدارة النظام قبل استخدامها.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            Dim stampPercent As Decimal
            If txtStampPercent Is Nothing OrElse Not Decimal.TryParse(txtStampPercent.Text.Trim(), stampPercent) OrElse stampPercent <= 0D Then
                MessageBox.Show("أدخل نسبة الدمغة بشكل صحيح", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                If txtStampPercent IsNot Nothing Then txtStampPercent.Focus()
                Return False
            End If

            Dim stampAmount As Decimal = Math.Round((amt * stampPercent) / 100D, 3)
            If stampAmount <= 0D OrElse stampAmount >= amt Then
                MessageBox.Show("قيمة الدمغة يجب أن تكون أكبر من صفر وأقل من مبلغ الصرف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                If txtStampPercent IsNot Nothing Then txtStampPercent.Focus()
                Return False
            End If

            If txtStampAccountCode Is Nothing OrElse String.IsNullOrWhiteSpace(txtStampAccountCode.Text) Then
                MessageBox.Show("اختر حساب الدمغة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                If btnPickStampAccount IsNot Nothing Then btnPickStampAccount.Focus()
                Return False
            End If

            If String.IsNullOrWhiteSpace(GetAccountName(txtStampAccountCode.Text.Trim())) Then
                MessageBox.Show("حساب الدمغة غير موجود في شجرة الحسابات", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtStampAccountCode.Focus()
                Return False
            End If
        End If

        Return True
    End Function

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

    Private Function ConfirmBudgetOverSpend(operationName As String, summary As BudgetSummary, amount As Decimal) As Boolean
        Dim msg As String =
            "تنبيه: إعداد النظام يسمح بتنفيذ عمليات الموازنة عند عدم كفاية الاعتماد." & Environment.NewLine &
            "نوع العملية: " & operationName & Environment.NewLine & Environment.NewLine &
            "الاعتماد: " & summary.Allocated.ToString("N3") & Environment.NewLine &
            "المصروف: " & summary.Spent.ToString("N3") & Environment.NewLine &
            "المحجوز: " & summary.Reserved.ToString("N3") & Environment.NewLine &
            "المتاح: " & summary.Available.ToString("N3") & Environment.NewLine &
            "القيمة المطلوبة: " & amount.ToString("N3") & Environment.NewLine & Environment.NewLine &
            "هل تريد المتابعة؟"

        Return MessageBox.Show(msg, "تأكيد سماحية الموازنة", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.Yes
    End Function

    '=========================
    ' Execute Entry (Reserve/Spend)
    '=========================
    Private Sub ExecuteEntry()
        If Not ValidateForm() Then Exit Sub

        Dim year As Integer = SelectedYear()
        Dim itemId As Integer = Convert.ToInt32(cmbItems.SelectedValue)

        Dim amt As Decimal
        Decimal.TryParse(txtAmount.Text.Trim(), amt)

        Dim sum As BudgetSummary = GetItemBudgetSummary(itemId, year)
        If sum.Allocated <= 0D OrElse sum.Available < amt Then
            If IsBudgetOverSpendAllowed() Then
                Dim operationName As String = If(EntryMode = 2, "حجز", "صرف")
                If Not ConfirmBudgetOverSpend(operationName, sum, amt) Then Exit Sub
            Else
                If sum.Allocated <= 0D Then
                    MessageBox.Show("لا يوجد اعتماد لهذا البند في هذه السنة. الرجاء إدخال اعتماد أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    MessageBox.Show(
                        "لا يوجد رصيد متاح كافي." & Environment.NewLine &
                        $"الاعتماد: {sum.Allocated:N3}" & Environment.NewLine &
                        $"المصروف: {sum.Spent:N3}" & Environment.NewLine &
                        $"المحجوز: {sum.Reserved:N3}" & Environment.NewLine &
                        $"المتاح: {sum.Available:N3}",
                        "رفض العملية",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
                End If
                Exit Sub
            End If
        End If

        Dim refNo = NewRefNo()
        SetUserContext(refNo)

        Dim commitmentTypeId As Object = DBNull.Value
        If EntryMode = 2 AndAlso cmbCommitmentTypes IsNot Nothing AndAlso cmbCommitmentTypes.SelectedIndex >= 0 Then
            commitmentTypeId = Convert.ToInt32(cmbCommitmentTypes.SelectedValue)
        End If

        Dim sourceId As Object = DBNull.Value
        Dim tempSourceId As Integer
        If txtSourceRef IsNot Nothing AndAlso Integer.TryParse(txtSourceRef.Text.Trim(), tempSourceId) Then
            sourceId = tempSourceId
        End If

        Dim sourceRefNo As Object = DBNull.Value
        If txtSourceRef IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(txtSourceRef.Text) Then
            sourceRefNo = txtSourceRef.Text.Trim()
        End If

        Dim sourceTable As Object = DBNull.Value
        If txtSourceTable IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(txtSourceTable.Text) Then
            sourceTable = txtSourceTable.Text.Trim()
        End If

        Dim hasStamp As Boolean = (EntryMode = 1 AndAlso chkHasStamp IsNot Nothing AndAlso chkHasStamp.Checked)
        Dim stampPercentValue As Decimal = 0D
        Dim stampAmountValue As Decimal = 0D
        Dim stampPercent As Object = DBNull.Value
        Dim stampAccountCode As Object = DBNull.Value
        Dim stampAmount As Object = DBNull.Value

        If hasStamp Then
            Decimal.TryParse(txtStampPercent.Text.Trim(), stampPercentValue)
            stampAmountValue = Math.Round((amt * stampPercentValue) / 100D, 3)
            stampPercent = stampPercentValue
            stampAmount = stampAmountValue
            stampAccountCode = txtStampAccountCode.Text.Trim()
        End If

        Using cn As New SqlConnection(ConnStr)
            cn.Open()
            Using cmd As New SqlCommand("
INSERT INTO Budget_Entries
(
    BudgetItemId,
    FiscalYear,
    Amount,
    EntryType,
    EntryDate,
    AccountingEntryId,
    Notes,
    StatusId,
    CommitmentTypeId,
    SourceId,
    SourceRefNo,
    SourceTable,
    HasStamp,
    StampPercent,
    StampAccountCode,
    StampAmount,
    SpendStatement
)
VALUES
(
    @ItemId,
    @Y,
    @Amt,
    @Type,
    @EntryDate,
    NULL,
    @Notes,
    @StatusId,
    @CommitmentTypeId,
    @SourceId,
    @SourceRefNo,
    @SourceTable,
    @HasStamp,
    @StampPercent,
    @StampAccountCode,
    @StampAmount,
    @SpendStatement
);", cn)

                cmd.Parameters.AddWithValue("@ItemId", itemId)
                cmd.Parameters.AddWithValue("@Y", year)
                cmd.Parameters.AddWithValue("@Amt", amt)
                cmd.Parameters.AddWithValue("@Type", EntryMode)
                cmd.Parameters.Add("@EntryDate", SqlDbType.DateTime).Value = dtpEntryDate.Value
                cmd.Parameters.AddWithValue("@Notes", If(txtNotes.Text Is Nothing, "", txtNotes.Text.Trim()))
                cmd.Parameters.AddWithValue("@StatusId", If(EntryMode = 2, 1, 0))
                cmd.Parameters.Add("@CommitmentTypeId", SqlDbType.TinyInt).Value = commitmentTypeId
                cmd.Parameters.Add("@SourceId", SqlDbType.Int).Value = sourceId
                cmd.Parameters.Add("@SourceRefNo", SqlDbType.NVarChar, 50).Value = sourceRefNo
                cmd.Parameters.Add("@SourceTable", SqlDbType.NVarChar, 50).Value = sourceTable
                cmd.Parameters.Add("@HasStamp", SqlDbType.Bit).Value = hasStamp
                cmd.Parameters.Add("@StampPercent", SqlDbType.Decimal).Value = stampPercent
                cmd.Parameters("@StampPercent").Precision = 18
                cmd.Parameters("@StampPercent").Scale = 3
                cmd.Parameters.Add("@StampAccountCode", SqlDbType.NVarChar, 40).Value = stampAccountCode
                cmd.Parameters.Add("@StampAmount", SqlDbType.Decimal).Value = stampAmount
                cmd.Parameters("@StampAmount").Precision = 18
                cmd.Parameters("@StampAmount").Scale = 3
                cmd.Parameters.Add("@SpendStatement", SqlDbType.NVarChar, 500).Value =
                    If(EntryMode = 1 AndAlso txtSpendStatement IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(txtSpendStatement.Text),
                       txtSpendStatement.Text.Trim(),
                       DBNull.Value)

                cmd.ExecuteNonQuery()
            End Using
        End Using

        SetStatus("تم تنفيذ العملية بنجاح")

        LoadEntriesGrid(year, itemId)
        UpdateBudgetSummary()
        UpdateSelectedDetails()

        'تفريغ المبلغ فقط لتسريع إدخال عمليات متتالية على نفس البند
        txtAmount.Text = ""
        txtNotes.Text = ""
        If dtpEntryDate IsNot Nothing Then dtpEntryDate.Value = DateTime.Now
        If txtSpendStatement IsNot Nothing Then txtSpendStatement.Text = ""
        If txtSourceRef IsNot Nothing Then txtSourceRef.Text = ""
        If txtSourceTable IsNot Nothing Then txtSourceTable.Text = ""
        If cmbCommitmentTypes IsNot Nothing Then cmbCommitmentTypes.SelectedIndex = -1
        If chkHasStamp IsNot Nothing Then chkHasStamp.Checked = False
        If txtStampPercent IsNot Nothing Then txtStampPercent.Text = ""
        If txtStampAccountCode IsNot Nothing Then txtStampAccountCode.Text = ""
        If txtStampAccountName IsNot Nothing Then txtStampAccountName.Text = ""
        UpdateStampControls()
        txtAmount.Focus()
    End Sub

    '=========================
    ' Events
    '=========================
    Private Sub cmbDoors_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbDoors.SelectionChangeCommitted
        Try
            LoadChapters(Convert.ToInt32(cmbDoors.SelectedValue))
            dgvEntries.DataSource = Nothing
            ResetBudgetItemExpenseAccountPreview("اختر بند الصرف لعرض حساب مصروف البند")
        Catch
        End Try
    End Sub

    Private Sub cmbChapters_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbChapters.SelectionChangeCommitted
        Try
            LoadItems(Convert.ToInt32(cmbChapters.SelectedValue))
            dgvEntries.DataSource = Nothing
            ResetBudgetItemExpenseAccountPreview("اختر بند الصرف لعرض حساب مصروف البند")
        Catch
        End Try
    End Sub

    Private Sub cmbItems_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbItems.SelectionChangeCommitted
        Try
            LoadEntriesGrid(SelectedYear(), Convert.ToInt32(cmbItems.SelectedValue))
            UpdateBudgetSummary()
            UpdateBudgetItemExpenseAccountPreview()
        Catch
        End Try
    End Sub

    Private Sub cmbFiscalYear_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbFiscalYear.SelectionChangeCommitted
        Try
            LoadEntriesGrid(SelectedYear(), If(cmbItems.SelectedIndex >= 0, Convert.ToInt32(cmbItems.SelectedValue), 0))
            UpdateBudgetSummary()
            UpdateBudgetItemExpenseAccountPreview()
        Catch
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        ClearForm(keepYear:=True)
        SetStatus("جديد")
    End Sub

    Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        Try
            ExecuteEntry()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SetStatus("فشل التنفيذ")
        End Try
    End Sub

    Private Sub btnUpdateSpendStatement_Click(sender As Object, e As EventArgs) Handles btnUpdateSpendStatement.Click
        Try
            If dgvEntries.CurrentRow Is Nothing OrElse dgvEntries.CurrentRow.IsNewRow Then
                MessageBox.Show("اختر عملية صرف من القائمة أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim status As String = GridCellText(dgvEntries.CurrentRow, "EntryStatus")
            If status = "معتمد" Then
                MessageBox.Show("لا يمكن تعديل بيان عملية صرف معتمدة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            If status = "غير معتمد" Then
                MessageBox.Show("لا يمكن تعديل بيان عملية صرف غير معتمدة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim currentStatement As String = GridCellText(dgvEntries.CurrentRow, "SpendStatement")
            Dim newStatement As String = currentStatement

            If Not ShowSpendStatementEditDialog(dgvEntries.CurrentRow, newStatement) Then Return

            If String.IsNullOrWhiteSpace(newStatement) AndAlso String.IsNullOrWhiteSpace(currentStatement) Then Return
            If String.IsNullOrWhiteSpace(newStatement) AndAlso Not String.IsNullOrWhiteSpace(currentStatement) Then
                If MessageBox.Show("هل تريد تفريغ بيان الصرف؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
            End If

            UpdateSelectedSpendStatement(newStatement)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SetStatus("فشل تعديل بيان الصرف")
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            LoadEntriesGrid(SelectedYear(), If(cmbItems.SelectedIndex >= 0, Convert.ToInt32(cmbItems.SelectedValue), 0))
            UpdateBudgetSummary()
            UpdateBudgetItemExpenseAccountPreview()
            SetStatus("تم التحديث")
        Catch ex As Exception
            SetStatus("خطأ: " & ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
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
        If EntryMode <> 1 Then Exit Sub

        MessageBox.Show("حساب الدمغة يحدد من شاشة إدارة النظام فقط.", "إعدادات الدمغة", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnEditStamp_Click(sender As Object, e As EventArgs) Handles btnEditStamp.Click
        Try
            EditSelectedEntryStamp()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            SetStatus("فشل تعديل الدمغة")
        End Try
    End Sub

    Private Sub btnPreviewJournal_Click(sender As Object, e As EventArgs) Handles btnPreviewJournal.Click
        If dgvEntries.CurrentRow Is Nothing Then
            MessageBox.Show("اختر إذن صرف لمعاينة القيد", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim budgetEntryId As Integer = Convert.ToInt32(dgvEntries.CurrentRow.Cells("BudgetEntryId").Value)

        Dim budgetItemExpenseAccountCode As String = ResolveBudgetItemExpenseAccountForApproval(budgetEntryId)
        If String.IsNullOrWhiteSpace(budgetItemExpenseAccountCode) Then
            MessageBox.Show("لم يتم تحديد حساب مصروف البند. يرجى ربط البند بحساب من الدليل.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim paymentAccountCode As String = ResolvePaymentAccountForApproval(budgetEntryId)
        If String.IsNullOrWhiteSpace(paymentAccountCode) Then Exit Sub
        SavePaymentAccountForEntry(budgetEntryId, paymentAccountCode)

        Dim stampInfo As DataRow = LoadStampInfoForEntry(budgetEntryId)
        Dim stampAccountCode As String = CellText(stampInfo, "StampAccountCode")

        If IsStampEnabled(stampInfo) AndAlso String.IsNullOrWhiteSpace(stampAccountCode) Then
            stampAccountCode = PickStampAccountForApproval()
            If String.IsNullOrWhiteSpace(stampAccountCode) Then Exit Sub
        End If

        Dim lines As List(Of JournalPreviewLine) = BuildJournalPreviewLines(budgetEntryId, budgetItemExpenseAccountCode, paymentAccountCode, stampAccountCode)
        ShowJournalPreview(lines, "معاينة القيد المحاسبي", False)
    End Sub

    Private Sub btnPrintVoucher_Click(sender As Object, e As EventArgs) Handles btnPrintVoucher.Click
        If dgvEntries.CurrentRow Is Nothing Then
            MessageBox.Show("اختر إذن صرف للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If GridCellText(dgvEntries.CurrentRow, "EntryStatus") = "غير معتمد" Then
            MessageBox.Show("لا يمكن طباعة إذن صرف غير معتمد", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim budgetEntryId As Integer = Convert.ToInt32(dgvEntries.CurrentRow.Cells("BudgetEntryId").Value)
        PrintBudgetSpendVoucher(budgetEntryId)
    End Sub

    Private Sub btnPrintOfficialVoucher_Click(sender As Object, e As EventArgs) Handles btnPrintOfficialVoucher.Click
        If dgvEntries.CurrentRow Is Nothing Then
            MessageBox.Show("اختر إذن صرف للطباعة الرسمية", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If GridCellText(dgvEntries.CurrentRow, "EntryStatus") = "غير معتمد" Then
            MessageBox.Show("لا يمكن طباعة إذن صرف غير معتمد", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim budgetEntryId As Integer = Convert.ToInt32(dgvEntries.CurrentRow.Cells("BudgetEntryId").Value)
        PrintOfficialSpendVoucher(budgetEntryId)
    End Sub

    Private Sub PrintBudgetSpendVoucher(ByVal budgetEntryId As Integer)
        SpendVoucherPrintData = LoadSpendVoucherPrintData(budgetEntryId)

        If SpendVoucherPrintData.Rows.Count = 0 Then
            MessageBox.Show("لم يتم العثور على بيانات إذن الصرف للطباعة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        PPSpendVoucher.ShowDialog()
    End Sub

    Private Sub PrintOfficialSpendVoucher(ByVal budgetEntryId As Integer)
        SpendVoucherPrintData = LoadSpendVoucherPrintData(budgetEntryId)

        If SpendVoucherPrintData.Rows.Count = 0 Then
            MessageBox.Show("لم يتم العثور على بيانات إذن الصرف للطباعة الرسمية", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        PPOfficialSpendVoucher.ShowDialog()
    End Sub

    Private Function LoadSpendVoucherPrintData(ByVal budgetEntryId As Integer) As DataTable
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Using cmd As New SqlCommand("
SELECT
    E.BudgetEntryId,
    E.FiscalYear,
    E.EntryDate,
    E.Amount,
    ISNULL(E.Notes, N'') AS Notes,
    ISNULL(E.SpendStatement, N'') AS SpendStatement,
    ISNULL(E.InvoiceNo, N'') AS InvoiceNo,
    ISNULL(E.DocumentNo, N'') AS DocumentNo,
    ISNULL(E.HasStamp, 0) AS HasStamp,
    ISNULL(E.StampPercent, 0) AS StampPercent,
    ISNULL(E.StampAccountCode, N'') AS StampAccountCode,
    ISNULL(SA.ACC_NAME, N'') AS StampAccountName,
    ISNULL(E.StampAmount, 0) AS StampAmount,
    E.EntryType,
    CASE E.EntryType
        WHEN 1 THEN N'إذن صرف'
        WHEN 2 THEN N'حجز'
        WHEN 3 THEN N'فك حجز'
        WHEN 4 THEN N'تسوية'
        ELSE N'غير معروف'
    END AS EntryTypeName,
    CASE
        WHEN E.ReversalJournalId IS NOT NULL THEN N'ملغى'
        WHEN E.AccountingEntryId IS NULL THEN N'غير معتمد'
        ELSE N'معتمد'
    END AS StatusName,
    E.AccountingEntryId,
    E.ReversalJournalId,
    M.JournalNumber,
    M.[DATE] AS JournalDate,
    RevM.JournalNumber AS ReversalJournalNumber,
    RevM.[DATE] AS ReversalJournalDate,
    D.DoorCode,
    D.DoorName,
    C.ChapterCode,
    C.ChapterName,
    I.ItemCode,
    I.ItemName,
    E.CostCenterId,
    ISNULL(CC.COST_NAME, N'') AS CostCenterName,
    CASE WHEN E.ProjectId IS NULL THEN N'' ELSE CONVERT(NVARCHAR(50), E.ProjectId) END AS ProjectName,
    E.ReserveEntryId,
    R.Amount AS ReserveAmount,
    R.EntryDate AS ReserveDate,
    ISNULL(R.Notes, N'') AS ReserveNotes,
    E.BeneficiaryType,
    ISNULL(BT.BeneficiaryTypeName, N'') AS BeneficiaryTypeName,
    E.BeneficiaryId,
    ISNULL(E.ContraAccountCode, N'') AS ContraAccountCode,
    ISNULL(A.ACC_NAME, N'') AS ContraAccountName,
    E.PaymentMethodId,
    ISNULL(PM.PaymentMethodName, N'') AS PaymentMethodName,
    E.ApprovedAt,
    E.ApprovedBy,
    ISNULL(UInput.UserName, CONVERT(NVARCHAR(50), E.ApprovedBy)) AS ApprovedByName,
    E.CanceledAt,
    E.CanceledBy,
    E.CancelReason,
    ISNULL(ReserveCalc.ReleasedAmount, 0) AS TotalReleasedFromReserve,
    ISNULL(R.Amount, 0) - ISNULL(ReserveCalc.ReleasedAmount, 0) AS ReserveRemainingAmount,
    E.Amount + ISNULL(PreviousSupplierSpend.AmountTotal, 0) AS OfficialSupplierSpendToDate
FROM dbo.Budget_Entries E
INNER JOIN dbo.Budget_Items I ON I.BudgetItemId = E.BudgetItemId
INNER JOIN dbo.Budget_Chapters C ON C.ChapterId = I.ChapterId
INNER JOIN dbo.Budget_Doors D ON D.DoorId = C.DoorId
LEFT JOIN dbo.ACC_BALANCE_MASTER M ON M.T_ID = E.AccountingEntryId
LEFT JOIN dbo.ACC_BALANCE_MASTER RevM ON RevM.T_ID = E.ReversalJournalId
LEFT JOIN dbo.Budget_Entries R ON R.BudgetEntryId = E.ReserveEntryId
LEFT JOIN dbo.Budget_BeneficiaryTypes BT ON BT.BeneficiaryType = E.BeneficiaryType
LEFT JOIN dbo.Budget_PaymentMethods PM ON PM.PaymentMethodId = E.PaymentMethodId
LEFT JOIN dbo.ACCOUNTS_TREE A ON A.ACC_CODE = E.ContraAccountCode
LEFT JOIN dbo.ACCOUNTS_TREE SA ON SA.ACC_CODE = E.StampAccountCode
LEFT JOIN dbo.COST_CENTER CC ON CC.COST_ID = E.CostCenterId
LEFT JOIN dbo.Users UInput ON UInput.user_id = E.ApprovedBy
OUTER APPLY
(
    SELECT SUM(X.Amount) AS ReleasedAmount
    FROM dbo.Budget_Entries X
    WHERE X.ReserveEntryId = E.ReserveEntryId
      AND X.EntryType = 3
      AND ISNULL(X.StatusId, 1) <> 2
) ReserveCalc
OUTER APPLY
(
    SELECT SUM(X.Amount) AS AmountTotal
    FROM dbo.Budget_Entries X
    WHERE X.EntryType = 1
      AND X.BudgetEntryId <> E.BudgetEntryId
      AND X.ReserveEntryId = E.ReserveEntryId
      AND ISNULL(LTRIM(RTRIM(X.ContraAccountCode)), N'') = ISNULL(LTRIM(RTRIM(E.ContraAccountCode)), N'')
      AND X.AccountingEntryId IS NOT NULL
      AND X.ReversalJournalId IS NULL
      AND ISNULL(X.StatusId, 1) <> 2
) PreviousSupplierSpend
WHERE E.EntryType = 1
  AND E.BudgetEntryId = @BudgetEntryId;", cn)

                cmd.Parameters.Add("@BudgetEntryId", SqlDbType.Int).Value = budgetEntryId

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Private Sub PDSpendVoucher_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PDSpendVoucher.PrintPage
        If SpendVoucherPrintData Is Nothing OrElse SpendVoucherPrintData.Rows.Count = 0 Then
            e.HasMorePages = False
            Return
        End If

        Dim row As DataRow = SpendVoucherPrintData.Rows(0)
        Dim g As Graphics = e.Graphics
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        Dim x As Integer = e.MarginBounds.Left
        Dim y As Integer = e.MarginBounds.Top
        Dim pageWidth As Integer = e.MarginBounds.Width

        Using companyFont As New Font("Tahoma", 11, FontStyle.Bold),
              titleFont As New Font("Tahoma", 15, FontStyle.Bold),
              subTitleFont As New Font("Tahoma", 9, FontStyle.Bold),
              sectionFont As New Font("Tahoma", 9.5!, FontStyle.Bold),
              bodyFont As New Font("Tahoma", 8.7!, FontStyle.Regular),
              boldFont As New Font("Tahoma", 8.7!, FontStyle.Bold)

            Dim sfRight As New StringFormat With {.Alignment = StringAlignment.Far, .LineAlignment = StringAlignment.Center, .FormatFlags = StringFormatFlags.DirectionRightToLeft}
            Dim sfCenter As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center, .FormatFlags = StringFormatFlags.DirectionRightToLeft}

            g.DrawString(MY_Settings.SBill_Title_1, companyFont, Brushes.Black, New RectangleF(x, y, pageWidth, 22), sfRight)
            y += 23
            g.DrawString(MY_Settings.SBill_Title_2, companyFont, Brushes.Black, New RectangleF(x, y, pageWidth, 22), sfRight)
            y += 25
            g.DrawLine(Pens.Black, x, y, x + pageWidth, y)
            y += 8

            g.DrawString("إذن صرف موازنة", titleFont, Brushes.Black, New RectangleF(x, y, pageWidth, 28), sfCenter)
            y += 28
            g.DrawString("Budget Spending Voucher", subTitleFont, Brushes.Black, New RectangleF(x, y, pageWidth, 20), sfCenter)
            y += 26

            y = DrawVoucherSection(g, x, y, pageWidth, "بيانات الإذن", bodyFont, boldFont, sectionFont, sfRight,
                New String() {"رقم إذن الصرف", "تاريخ الإذن", "السنة المالية", "حالة الصرف"},
                New String() {CellText(row, "BudgetEntryId"), DateCellText(row, "EntryDate"), CellText(row, "FiscalYear"), CellText(row, "StatusName")})

            y = DrawVoucherSection(g, x, y, pageWidth, "بيانات الموازنة", bodyFont, boldFont, sectionFont, sfRight,
                New String() {"الباب", "الفصل", "البند", "مركز التكلفة", "المشروع"},
                New String() {
                    CombineCodeName(row, "DoorCode", "DoorName"),
                    CombineCodeName(row, "ChapterCode", "ChapterName"),
                    CombineCodeName(row, "ItemCode", "ItemName"),
                    CellText(row, "CostCenterName"),
                    CellText(row, "ProjectName")})

            y = DrawVoucherSection(g, x, y, pageWidth, "بيانات الصرف", bodyFont, boldFont, sectionFont, sfRight,
                New String() {"نوع المستفيد", "المستفيد / الحساب المقابل", "طريقة الدفع", "رقم الفاتورة", "رقم المستند"},
                New String() {
                    CellText(row, "BeneficiaryTypeName"),
                    CombineCodeName(row, "ContraAccountCode", "ContraAccountName"),
                    CellText(row, "PaymentMethodName"),
                    CellText(row, "InvoiceNo"),
                    CellText(row, "DocumentNo")})

            y = DrawVoucherSection(g, x, y, pageWidth, "بيانات الحجز", bodyFont, boldFont, sectionFont, sfRight,
                New String() {"رقم الحجز المرتبط", "تاريخ الحجز", "قيمة الحجز", "المفكوك من الحجز", "المتبقي من الحجز"},
                New String() {
                    CellText(row, "ReserveEntryId"),
                    DateCellText(row, "ReserveDate"),
                    NumberCellText(row, "ReserveAmount"),
                    NumberCellText(row, "TotalReleasedFromReserve"),
                    NumberCellText(row, "ReserveRemainingAmount")})

            Dim amountText As String = NumberCellText(row, "Amount")
            Dim amountWords As String = ""
            Dim amount As Decimal
            If Decimal.TryParse(CellText(row, "Amount"), amount) Then amountWords = HANY(amount, "LYD")

            y = DrawVoucherSection(g, x, y, pageWidth, "المبلغ والبيان", bodyFont, boldFont, sectionFont, sfRight,
                New String() {"مبلغ الصرف", "قيمة الدمغة", "حساب الدمغة", "المبلغ كتابة", "بيان الصرف", "ملاحظات"},
                New String() {
                    amountText,
                    NumberCellText(row, "StampAmount"),
                    CombineCodeName(row, "StampAccountCode", "StampAccountName"),
                    amountWords,
                    CellText(row, "SpendStatement"),
                    CellText(row, "Notes")})

            y = DrawVoucherSection(g, x, y, pageWidth, "القيد المحاسبي", bodyFont, boldFont, sectionFont, sfRight,
                New String() {"رقم القيد", "رقم اليومية", "تاريخ القيد", "رقم القيد العكسي إن وجد"},
                New String() {CellText(row, "AccountingEntryId"), CellText(row, "JournalNumber"), DateCellText(row, "JournalDate"), CellText(row, "ReversalJournalNumber")})

            y += 8
            DrawVoucherSignatures(g, x, y, pageWidth, bodyFont, boldFont, sfCenter)
        End Using

        e.HasMorePages = False
    End Sub

    Private Sub PDOfficialSpendVoucher_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PDOfficialSpendVoucher.PrintPage
        If SpendVoucherPrintData Is Nothing OrElse SpendVoucherPrintData.Rows.Count = 0 Then
            e.HasMorePages = False
            Return
        End If

        Dim row As DataRow = SpendVoucherPrintData.Rows(0)
        Dim g As Graphics = e.Graphics
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        Dim x As Integer = e.MarginBounds.Left
        Dim y As Integer = e.MarginBounds.Top
        Dim w As Integer = e.MarginBounds.Width
        Dim h As Integer = e.MarginBounds.Height
        Dim amount As Decimal = CellDecimal(row, "Amount")
        Dim stampAmount As Decimal = CellDecimal(row, "StampAmount")
        Dim netAmount As Decimal = amount - stampAmount
        If netAmount <= 0D Then netAmount = amount

        Using borderPen As New Pen(Color.Black, 1.2!),
              thinPen As New Pen(Color.Black, 0.7!),
              headerBrush As New SolidBrush(Color.FromArgb(238, 242, 247)),
              titleFont As New Font("Tahoma", 15, FontStyle.Bold),
              countryFont As New Font("Tahoma", 10.5!, FontStyle.Bold),
              headFont As New Font("Tahoma", 8.5!, FontStyle.Bold),
              bodyFont As New Font("Tahoma", 8.2!, FontStyle.Regular),
              boldFont As New Font("Tahoma", 8.2!, FontStyle.Bold),
              smallFont As New Font("Tahoma", 7.3!, FontStyle.Regular)

            Dim sfRight As New StringFormat With {.Alignment = StringAlignment.Far, .LineAlignment = StringAlignment.Center, .FormatFlags = StringFormatFlags.DirectionRightToLeft}
            Dim sfCenter As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center, .FormatFlags = StringFormatFlags.DirectionRightToLeft}
            Dim sfLeft As New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center}
            Dim sfRtlActualRight As New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center, .FormatFlags = StringFormatFlags.DirectionRightToLeft}

            g.DrawRectangle(borderPen, x, y, w, h)
            g.DrawRectangle(thinPen, x + 4, y + 4, w - 8, h - 8)

            Dim topY As Integer = y + 18
            g.DrawString("نموذج م.م", smallFont, Brushes.Black, New RectangleF(x + 18, topY, 120, 18), sfLeft)
            DrawOfficialDottedText(g, "رقم التسجيل", "", x + w - 250, topY + 4, 210, bodyFont, sfRight)
            DrawOfficialDottedText(g, "بند الصرف", CombineCodeName(row, "ItemCode", "ItemName"), x + w - 250, topY + 24, 210, bodyFont, sfRight)

            g.DrawString("التاريخ: " & DateCellText(row, "EntryDate"), bodyFont, Brushes.Black, New RectangleF(x + 55, topY + 54, 140, 20), sfRight)
            g.DrawString("الرقم: " & CellText(row, "BudgetEntryId"), bodyFont, Brushes.Black, New RectangleF(x + 55, topY + 76, 140, 20), sfRight)

            g.DrawString("دولة ليبيا", countryFont, Brushes.Black, New RectangleF(x, topY + 54, w, 22), sfCenter)
            g.DrawString("إذن صــــرف", titleFont, Brushes.Black, New RectangleF(x, topY + 88, w, 30), sfCenter)

            Dim infoY As Integer = topY + 126
            Dim infoX As Integer = x + w - 330
            DrawOfficialPlainLine(g, "الجهة: " & MY_Settings.SBill_Title_1, infoX, infoY, 285, bodyFont, sfRtlActualRight)
            DrawOfficialPlainLine(g, "الفرع: " & MY_Settings.SBill_Title_2, infoX, infoY + 18, 285, bodyFont, sfRtlActualRight)
            DrawOfficialPlainLine(g, "الغرض: صرف بند ميزانية", infoX, infoY + 36, 285, bodyFont, sfRtlActualRight)
            DrawOfficialPlainLine(g, "الباب: " & CombineCodeName(row, "DoorCode", "DoorName"), infoX, infoY + 54, 285, bodyFont, sfRtlActualRight)
            DrawOfficialPlainLine(g, "البند: " & CombineCodeName(row, "ItemCode", "ItemName"), infoX, infoY + 72, 285, bodyFont, sfRtlActualRight)
            DrawOfficialPlainLine(g, "يصرف إلى: " & CombineCodeName(row, "ContraAccountCode", "ContraAccountName"), infoX, infoY + 90, 285, bodyFont, sfRtlActualRight)

            Dim tableY As Integer = infoY + 122
            Dim tableH As Integer = 230
            Dim amountW As Integer = 130
            Dim detailsW As Integer = 230
            Dim budgetW As Integer = w - amountW - detailsW - 90
            Dim tableX As Integer = x + 55

            DrawOfficialTableHeader(g, tableX + amountW + detailsW, tableY, budgetW, 30, "بيانات الاعتماد المالي", headFont, headerBrush, thinPen, sfCenter)
            DrawOfficialTableHeader(g, tableX + amountW, tableY, detailsW, 30, "تفاصيل الصرف", headFont, headerBrush, thinPen, sfCenter)
            DrawOfficialTableHeader(g, tableX, tableY, amountW, 30, "المبلغ", headFont, headerBrush, thinPen, sfCenter)

            Dim amountSubW As Integer = amountW \ 2
            DrawOfficialCell(g, tableX, tableY + 30, amountSubW, 28, "دينار", bodyFont, thinPen, sfCenter)
            DrawOfficialCell(g, tableX + amountSubW, tableY + 30, amountSubW, 28, "درهم", bodyFont, thinPen, sfCenter)

            Dim hasStamp As Boolean = (stampAmount > 0D)
            Dim totalBottomRowsHeight As Integer = If(hasStamp, 56, 28)
            Dim detailsBodyHeight As Integer = tableH - 30 - totalBottomRowsHeight
            Dim amountBodyHeight As Integer = tableH - 58 - totalBottomRowsHeight

            DrawOfficialCell(g, tableX + amountW, tableY + 30, detailsW, detailsBodyHeight, CellText(row, "SpendStatement"), boldFont, thinPen, sfRight)
            If hasStamp Then
                DrawOfficialCell(g, tableX + amountW, tableY + tableH - 56, detailsW, 28, "قيمة الدمغة", boldFont, thinPen, sfCenter)
            End If
            DrawOfficialCell(g, tableX + amountW, tableY + tableH - 28, detailsW, 28, "المبلغ الصافي", boldFont, thinPen, sfCenter)

            Dim budgetX As Integer = tableX + amountW + detailsW
            DrawOfficialBudgetApprovalBlock(g, budgetX, tableY + 30, budgetW, tableH - 30, bodyFont, thinPen, sfCenter, sfRtlActualRight, CellDecimal(row, "OfficialSupplierSpendToDate"))

            DrawOfficialCell(g, tableX, tableY + 58, amountSubW, amountBodyHeight, OfficialDinarText(amount), boldFont, thinPen, sfCenter)
            DrawOfficialCell(g, tableX + amountSubW, tableY + 58, amountSubW, amountBodyHeight, OfficialDirhamText(amount), boldFont, thinPen, sfCenter)
            If hasStamp Then
                DrawOfficialCell(g, tableX, tableY + tableH - 56, amountSubW, 28, OfficialDinarText(stampAmount), boldFont, thinPen, sfCenter)
                DrawOfficialCell(g, tableX + amountSubW, tableY + tableH - 56, amountSubW, 28, OfficialDirhamText(stampAmount), boldFont, thinPen, sfCenter)
            End If
            DrawOfficialCell(g, tableX, tableY + tableH - 28, amountSubW, 28, OfficialDinarText(netAmount), boldFont, thinPen, sfCenter)
            DrawOfficialCell(g, tableX + amountSubW, tableY + tableH - 28, amountSubW, 28, OfficialDirhamText(netAmount), boldFont, thinPen, sfCenter)

            Dim lawY As Integer = tableY + tableH + 16
            g.DrawString("(يعد وفقا للمادتين 99،100 من اللائحة)", smallFont, Brushes.Black, New RectangleF(x, lawY, w, 18), sfCenter)

            Dim pledgeY As Integer = lawY + 30
            Dim pledgeW As Integer = 560
            Dim pledgeX As Integer = x + w - pledgeW - 45
            g.DrawString("إقرار من الجهة التي تأذن بالصرف", headFont, Brushes.Black, New RectangleF(pledgeX, pledgeY, pledgeW, 20), sfRtlActualRight)
            Dim lines() As String = {
                "1- المبلغ المذكور بالأذن مستحق الدفع للمستفيد وصرفه يتم بموجب تفويض صادر صحيح.",
                "2- المبلغ مطابق للفاتورة أو الطلب المستوفي، وأن الصرف خاص ومخول.",
                "3- المصروفات والارتباطات مقيدة في صورة الاعتمادات المالية المخصصة للغرض المبين بالإذن.",
                "4- المستندات المؤيدة للحركة محفوظة ومطابقة للقيود بسجل المصروفات."
            }
            For i As Integer = 0 To lines.Length - 1
                g.DrawString(lines(i), smallFont, Brushes.Black, New RectangleF(pledgeX, pledgeY + 25 + (i * 18), pledgeW, 18), sfRtlActualRight)
            Next

            Dim controllerSignatureX As Integer = x + 45
            DrawOfficialDottedText(g, "التوقيع", "", controllerSignatureX, pledgeY + 104, 245, bodyFont, sfRight)
            g.DrawString("المراقب المالي بالمركز", bodyFont, Brushes.Black, New RectangleF(controllerSignatureX, pledgeY + 128, 210, 18), sfRight)

            Dim receiveY As Integer = pledgeY + 168
            g.DrawLine(thinPen, x + 45, receiveY, x + w - 45, receiveY)
            receiveY += 14
            g.DrawString("تسلمت المبلغ المبين أعلاه وقدره: " & HANY(netAmount, "LYD"), boldFont, Brushes.Black, New RectangleF(x + 55, receiveY, w - 110, 20), sfRight)
            g.DrawString("وذلك بتاريخ: " & DateTime.Now.ToString("dd/MM/yyyy"), bodyFont, Brushes.Black, New RectangleF(x + w - 270, receiveY + 32, 220, 20), sfRight)
            DrawOfficialDottedText(g, "توقيع المستلم", "", x + 95, receiveY + 32, 245, bodyFont, sfRight)
            DrawOfficialDottedText(g, "في", "", x + 95, receiveY + 64, 245, bodyFont, sfRight)
            DrawOfficialDottedText(g, "صرف بصك رقم", CellText(row, "DocumentNo"), x + w - 270, receiveY + 64, 220, bodyFont, sfRight)
            DrawOfficialDottedText(g, "حساب رقم", "", x + w - 270, receiveY + 92, 220, bodyFont, sfRight)
            DrawOfficialDottedText(g, "توقيع شاهد الدفع", "", x + w - 270, receiveY + 120, 220, bodyFont, sfRight)
            g.DrawString("توقيع الصراف", bodyFont, Brushes.Black, New RectangleF(x + 55, receiveY + 126, 180, 20), sfRight)
        End Using

        e.HasMorePages = False
    End Sub

    Private Function DrawVoucherSection(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, title As String,
                                        bodyFont As Font, boldFont As Font, sectionFont As Font, sfRight As StringFormat,
                                        labels() As String, values() As String) As Integer
        Dim headerHeight As Integer = 24
        Dim rowHeight As Integer = 25
        Dim colWidth As Integer = CInt(pageWidth / 2)
        Dim valueWidth As Integer = colWidth - 115
        Dim totalRows As Integer = CInt(Math.Ceiling(labels.Length / 2.0))
        Dim rect As New Rectangle(x, y, pageWidth, headerHeight + (totalRows * rowHeight))

        g.DrawRectangle(New Pen(Color.FromArgb(120, 120, 120)), rect)
        g.FillRectangle(New SolidBrush(Color.FromArgb(230, 235, 242)), New Rectangle(x, y, pageWidth, headerHeight))
        g.DrawString(title, sectionFont, Brushes.Black, New RectangleF(x + 5, y, pageWidth - 10, headerHeight), sfRight)
        y += headerHeight

        For i As Integer = 0 To totalRows - 1
            For j As Integer = 0 To 1
                Dim index As Integer = (i * 2) + j
                If index >= labels.Length Then Continue For

                Dim cellX As Integer = x + pageWidth - ((j + 1) * colWidth)
                Dim cellRect As New Rectangle(cellX, y, colWidth, rowHeight)
                Dim labelRect As New Rectangle(cellRect.Right - 110, y, 105, rowHeight)
                Dim valueRect As New Rectangle(cellRect.Left + 5, y, valueWidth, rowHeight)

                g.DrawRectangle(New Pen(Color.FromArgb(190, 190, 190)), cellRect)
                g.DrawString(labels(index) & ":", boldFont, Brushes.Black, New RectangleF(labelRect.X, labelRect.Y, labelRect.Width, labelRect.Height), sfRight)
                g.DrawString(If(values(index), ""), bodyFont, Brushes.Black, New RectangleF(valueRect.X, valueRect.Y, valueRect.Width, valueRect.Height), sfRight)
            Next

            y += rowHeight
        Next

        Return y + 7
    End Function

    Private Sub DrawOfficialTableHeader(g As Graphics, x As Integer, y As Integer, width As Integer, height As Integer,
                                        text As String, font As Font, brush As Brush, pen As Pen, sf As StringFormat)
        g.FillRectangle(brush, New Rectangle(x, y, width, height))
        DrawOfficialCell(g, x, y, width, height, text, font, pen, sf)
    End Sub

    Private Sub DrawOfficialBudgetRow(g As Graphics, x As Integer, y As Integer, width As Integer, height As Integer,
                                      label As String, value As String, font As Font, pen As Pen, sfRight As StringFormat)
        Dim valueWidth As Integer = 90
        DrawOfficialCell(g, x, y, valueWidth \ 2, height, "", font, pen, sfRight)
        DrawOfficialCell(g, x + (valueWidth \ 2), y, valueWidth \ 2, height, value, font, pen, sfRight)
        DrawOfficialCell(g, x + valueWidth, y, width - valueWidth, height, label, font, pen, sfRight)
    End Sub

    Private Sub DrawOfficialBudgetApprovalBlock(g As Graphics, x As Integer, y As Integer, width As Integer, height As Integer,
                                                font As Font, pen As Pen, sfCenter As StringFormat, sfRight As StringFormat,
                                                supplierSpendToDate As Decimal)
        Dim amountColsWidth As Integer = 96
        Dim amountColW As Integer = amountColsWidth \ 2
        Dim labelW As Integer = width - amountColsWidth
        Dim authH As Integer = 44
        Dim headerH As Integer = 30
        Dim remainingH As Integer = height - authH - headerH
        Dim rowH As Integer = CInt(remainingH / 5)
        Dim lastRowH As Integer = remainingH - (rowH * 4)
        Dim labelX As Integer = x + amountColsWidth
        Dim smallApprovalFont As New Font(font.FontFamily, Math.Max(6.5!, font.Size - 1.0!), font.Style)

        DrawOfficialCell(g, x, y, amountColsWidth, authH, "", font, pen, sfCenter)
        DrawOfficialCell(g, labelX, y, labelW, authH, "رقم التفويض: " & Dots(28), font, pen, sfRight)

        DrawOfficialCell(g, x, y + authH, amountColW, headerH, "دينار", font, pen, sfCenter)
        DrawOfficialCell(g, x + amountColW, y + authH, amountColW, headerH, "درهم", font, pen, sfCenter)
        DrawOfficialCell(g, labelX, y + authH, labelW, headerH, "", font, pen, sfRight)

        Dim labels() As String = {
            "مجموع الاعتمادات المفوض بها",
            "مجموع المصروفات والارتباطات" & Environment.NewLine & "لتاريخه",
            "الرصيد قبل صرف هذا الإذن",
            "صفحة سجل الاعتمادات",
            "توقيع كاتب السجل"
        }

        Dim currentY As Integer = y + authH + headerH
        For i As Integer = 0 To labels.Length - 1
            Dim currentH As Integer = If(i = labels.Length - 1, lastRowH, rowH)
            Dim dinarText As String = If(i = 1 AndAlso supplierSpendToDate > 0D, OfficialDinarText(supplierSpendToDate), "")
            Dim dirhamText As String = If(i = 1 AndAlso supplierSpendToDate > 0D, OfficialDirhamText(supplierSpendToDate), "")
            DrawOfficialCell(g, x, currentY, amountColW, currentH, dinarText, font, pen, sfCenter)
            DrawOfficialCell(g, x + amountColW, currentY, amountColW, currentH, dirhamText, font, pen, sfCenter)
            DrawOfficialCell(g, labelX, currentY, labelW, currentH, labels(i), If(i = 1, smallApprovalFont, font), pen, sfRight)
            currentY += currentH
        Next

        smallApprovalFont.Dispose()
    End Sub

    Private Sub DrawOfficialCell(g As Graphics, x As Integer, y As Integer, width As Integer, height As Integer,
                                 text As String, font As Font, pen As Pen, sf As StringFormat)
        Dim rect As New Rectangle(x, y, width, height)
        g.DrawRectangle(pen, rect)
        g.DrawString(If(text, ""), font, Brushes.Black, New RectangleF(rect.X + 3, rect.Y + 2, rect.Width - 6, rect.Height - 4), sf)
    End Sub

    Private Sub DrawOfficialPlainLine(g As Graphics, text As String, x As Integer, y As Integer, width As Integer, font As Font, sf As StringFormat)
        g.DrawString(If(text, ""), font, Brushes.Black, New RectangleF(x, y, width, 18), sf)
    End Sub

    Private Sub DrawOfficialDottedText(g As Graphics, label As String, value As String, x As Integer, y As Integer,
                                       width As Integer, font As Font, sfRight As StringFormat)
        Dim text As String = label & ": " & If(String.IsNullOrWhiteSpace(value), Dots(24), value)
        g.DrawString(text, font, Brushes.Black, New RectangleF(x, y, width, 20), sfRight)
    End Sub

    Private Function Dots(count As Integer) As String
        Return New String("."c, Math.Max(0, count))
    End Function

    Private Function OfficialDinarText(amount As Decimal) As String
        Dim absAmount As Decimal = Math.Abs(Math.Round(amount, 3))
        Dim dinarPart As Decimal = Decimal.Truncate(absAmount)
        Dim dirhamPart As Integer = CInt(Math.Round((absAmount - dinarPart) * 1000D, 0))

        If dirhamPart >= 1000 Then
            dinarPart += 1D
        End If

        Return dinarPart.ToString("N0")
    End Function

    Private Function OfficialDirhamText(amount As Decimal) As String
        Dim absAmount As Decimal = Math.Abs(Math.Round(amount, 3))
        Dim dinarPart As Decimal = Decimal.Truncate(absAmount)
        Dim dirhamPart As Integer = CInt(Math.Round((absAmount - dinarPart) * 1000D, 0))

        If dirhamPart >= 1000 Then
            dirhamPart = 0
        End If

        Return dirhamPart.ToString("000")
    End Function

    Private Sub DrawVoucherSignatures(g As Graphics, x As Integer, y As Integer, pageWidth As Integer, bodyFont As Font, boldFont As Font, sfCenter As StringFormat)
        Dim titles() As String = {"إعداد", "مراجعة", "اعتماد", "استلام المستفيد"}
        Dim boxWidth As Integer = CInt(pageWidth / titles.Length)
        Dim boxHeight As Integer = 54
        Dim currentX As Integer = x + pageWidth

        For Each title As String In titles
            currentX -= boxWidth
            Dim rect As New Rectangle(currentX, y, boxWidth, boxHeight)
            g.DrawRectangle(Pens.Black, rect)
            g.DrawString(title, boldFont, Brushes.Black, New RectangleF(rect.X, rect.Y + 3, rect.Width, 20), sfCenter)
            g.DrawString("....................", bodyFont, Brushes.Black, New RectangleF(rect.X, rect.Y + 27, rect.Width, 20), sfCenter)
        Next
    End Sub

    Private Function CellText(row As DataRow, columnName As String) As String
        If row Is Nothing OrElse Not row.Table.Columns.Contains(columnName) Then Return ""
        If row(columnName) Is Nothing OrElse row(columnName) Is DBNull.Value Then Return ""
        Return row(columnName).ToString()
    End Function

    Private Function DateCellText(row As DataRow, columnName As String) As String
        Dim d As Date
        If Date.TryParse(CellText(row, columnName), d) Then Return d.ToString("dd/MM/yyyy")
        Return ""
    End Function

    Private Function NumberCellText(row As DataRow, columnName As String) As String
        Dim d As Decimal
        If Decimal.TryParse(CellText(row, columnName), d) Then Return d.ToString("N3")
        Return ""
    End Function

    Private Function CombineCodeName(row As DataRow, codeColumn As String, nameColumn As String) As String
        Dim code As String = CellText(row, codeColumn)
        Dim name As String = CellText(row, nameColumn)
        If String.IsNullOrWhiteSpace(code) Then Return name
        If String.IsNullOrWhiteSpace(name) Then Return code
        Return code & " - " & name
    End Function

    Private Function LoadStampInfoForEntry(budgetEntryId As Integer) As DataRow
        Dim dt As New DataTable()

        Using con As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
SELECT TOP 1
    ISNULL(HasStamp, 0) AS HasStamp,
    ISNULL(StampPercent, 0) AS StampPercent,
    ISNULL(StampAccountCode, N'') AS StampAccountCode,
    ISNULL(StampAmount, 0) AS StampAmount
FROM dbo.Budget_Entries
WHERE BudgetEntryId = @BudgetEntryId;", con)

                cmd.Parameters.Add("@BudgetEntryId", SqlDbType.Int).Value = budgetEntryId
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        If dt.Rows.Count = 0 Then Return Nothing
        Return dt.Rows(0)
    End Function

    Private Function IsStampEnabled(row As DataRow) As Boolean
        If row Is Nothing Then Return False
        Dim hasStamp As Boolean = False
        Boolean.TryParse(CellText(row, "HasStamp"), hasStamp)
        If hasStamp Then Return True
        Return CellDecimal(row, "StampPercent") > 0D
    End Function

    Private Function CellDecimal(row As DataRow, columnName As String) As Decimal
        Dim value As Decimal
        Decimal.TryParse(CellText(row, columnName), value)
        Return value
    End Function

    Private Function GetBudgetItemExpenseAccountCodeForEntry(budgetEntryId As Integer) As String
        Dim accounts As DataTable = LoadBudgetItemExpenseAccountsForEntry(budgetEntryId)
        If accounts.Rows.Count = 0 Then Return ""
        If accounts.Rows.Count = 1 Then Return CellText(accounts.Rows(0), "AccountCode")

        Dim defaultRow As DataRow = FindDefaultBudgetItemExpenseAccount(accounts)
        If defaultRow Is Nothing Then Return ""

        Return CellText(defaultRow, "AccountCode")
    End Function

    Private Function GetBudgetEntryAmount(budgetEntryId As Integer) As Decimal
        Using con As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
SELECT TOP 1 Amount
FROM dbo.Budget_Entries
WHERE BudgetEntryId = @BudgetEntryId;", con)

                cmd.Parameters.Add("@BudgetEntryId", SqlDbType.Int).Value = budgetEntryId
                con.Open()

                Dim value = cmd.ExecuteScalar()
                If value Is Nothing OrElse value Is DBNull.Value Then Return 0D
                Return Convert.ToDecimal(value)
            End Using
        End Using
    End Function

    Private Function DescribeBudgetItemExpenseAccountsForEntry(budgetEntryId As Integer) As String
        Dim accounts As DataTable = LoadBudgetItemExpenseAccountsForEntry(budgetEntryId)

        If accounts.Rows.Count = 0 Then Return "لا يوجد حساب مصروف مرتبط"
        If accounts.Rows.Count = 1 Then Return BudgetItemExpenseAccountText(accounts.Rows(0))

        Dim defaultRow As DataRow = FindDefaultBudgetItemExpenseAccount(accounts)
        If defaultRow IsNot Nothing Then
            Return "عدة حسابات مصروف مرتبطة، الافتراضي: " & BudgetItemExpenseAccountText(defaultRow)
        End If

        Return "عدة حسابات مصروف مرتبطة، يلزم الاختيار عند الاعتماد"
    End Function

    Private Function ResolveBudgetItemExpenseAccountForApproval(budgetEntryId As Integer) As String
        Dim accounts As DataTable = LoadBudgetItemExpenseAccountsForEntry(budgetEntryId)

        If accounts.Rows.Count = 0 Then Return ""

        If accounts.Rows.Count = 1 Then
            Dim code As String = CellText(accounts.Rows(0), "AccountCode")
            SetStatus("تم اختيار حساب البند تلقائيًا: " & code)
            Return code
        End If

        Return PickBudgetItemExpenseAccountFromLinkedAccounts(accounts)
    End Function

    Private Function PickBudgetItemExpenseAccountFromLinkedAccounts(accounts As DataTable) As String
        If accounts Is Nothing OrElse accounts.Rows.Count = 0 Then Return ""

        Using frm As New Form()
            Using lbl As New Label()
                Using cbo As New ComboBox()
                    Using btnOk As New Button()
                        Using btnCancel As New Button()
                            frm.Text = "اختيار حساب مصروف البند"
                            frm.StartPosition = FormStartPosition.CenterParent
                            frm.Size = New Size(620, 170)
                            frm.FormBorderStyle = FormBorderStyle.FixedDialog
                            frm.MaximizeBox = False
                            frm.MinimizeBox = False
                            frm.RightToLeft = RightToLeft.Yes
                            frm.RightToLeftLayout = True
                            frm.Font = New Font("Segoe UI", 9.75!, FontStyle.Regular)

                            lbl.AutoSize = False
                            lbl.TextAlign = ContentAlignment.MiddleRight
                            lbl.Location = New Point(18, 16)
                            lbl.Size = New Size(565, 28)
                            lbl.Text = "يوجد أكثر من حساب مصروف مرتبط بهذا البند. اختر الحساب الذي سيظهر مدينًا في قيد الصرف:"

                            cbo.DropDownStyle = ComboBoxStyle.DropDownList
                            cbo.Font = New Font("Segoe UI Semibold", 10.0!, FontStyle.Bold)
                            cbo.Location = New Point(18, 52)
                            cbo.Size = New Size(565, 25)
                            cbo.DataSource = accounts
                            cbo.DisplayMember = "AccountText"
                            cbo.ValueMember = "AccountCode"

                            Dim defaultRow As DataRow = FindDefaultBudgetItemExpenseAccount(accounts)
                            If defaultRow IsNot Nothing Then
                                cbo.SelectedValue = CellText(defaultRow, "AccountCode")
                            Else
                                cbo.SelectedIndex = 0
                            End If

                            btnOk.Text = "اختيار"
                            btnOk.Size = New Size(95, 34)
                            btnOk.Location = New Point(118, 88)
                            btnOk.DialogResult = DialogResult.OK

                            btnCancel.Text = "إلغاء"
                            btnCancel.Size = New Size(95, 34)
                            btnCancel.Location = New Point(18, 88)
                            btnCancel.DialogResult = DialogResult.Cancel

                            frm.Controls.Add(lbl)
                            frm.Controls.Add(cbo)
                            frm.Controls.Add(btnOk)
                            frm.Controls.Add(btnCancel)
                            frm.AcceptButton = btnOk
                            frm.CancelButton = btnCancel

                            If frm.ShowDialog(Me) <> DialogResult.OK Then Return ""
                            If cbo.SelectedValue Is Nothing Then Return ""

                            Return Convert.ToString(cbo.SelectedValue).Trim()
                        End Using
                    End Using
                End Using
            End Using
        End Using
    End Function

    Private Function BuildJournalPreviewLines(budgetEntryId As Integer, budgetItemExpenseAccountCode As String, paymentAccountCode As String, stampAccountCode As String) As List(Of JournalPreviewLine)
        Dim amount As Decimal = GetBudgetEntryAmount(budgetEntryId)
        If String.IsNullOrWhiteSpace(budgetItemExpenseAccountCode) Then
            Throw New Exception("لا يوجد حساب مصروف مرتبط بهذا البند")
        End If

        Dim stampInfo As DataRow = LoadStampInfoForEntry(budgetEntryId)
        Dim stampPercent As Decimal = CellDecimal(stampInfo, "StampPercent")
        Dim stampAmount As Decimal = If(IsStampEnabled(stampInfo), Math.Round((amount * stampPercent) / 100D, 3), 0D)

        If stampAmount > 0D AndAlso String.IsNullOrWhiteSpace(stampAccountCode) Then
            Throw New Exception("لم يتم تحديد حساب الدمغة")
        End If

        If stampAmount >= amount Then
            Throw New Exception("قيمة الدمغة يجب أن تكون أقل من مبلغ الصرف")
        End If

        Dim netAmount As Decimal = amount - stampAmount
        Dim lines As New List(Of JournalPreviewLine)

        lines.Add(New JournalPreviewLine With {
            .AccountCode = budgetItemExpenseAccountCode,
            .AccountName = GetAccountName(budgetItemExpenseAccountCode),
            .Statement = "مصروف بند موازنة",
            .Debit = amount,
            .Credit = 0D
        })

        lines.Add(New JournalPreviewLine With {
            .AccountCode = paymentAccountCode,
            .AccountName = GetAccountName(paymentAccountCode),
            .Statement = If(stampAmount > 0D, "صافي الصرف بعد الدمغة", "حساب الدفع / المستفيد"),
            .Debit = 0D,
            .Credit = netAmount
        })

        If stampAmount > 0D Then
            lines.Add(New JournalPreviewLine With {
                .AccountCode = stampAccountCode,
                .AccountName = GetAccountName(stampAccountCode),
                .Statement = "دمغة بنسبة " & stampPercent.ToString("N3") & "%",
                .Debit = 0D,
                .Credit = stampAmount
            })
        End If

        Return lines
    End Function

    Private Function ShowJournalPreview(lines As List(Of JournalPreviewLine), title As String, requireConfirm As Boolean) As Boolean
        Dim totalDebit As Decimal = lines.Sum(Function(x) x.Debit)
        Dim totalCredit As Decimal = lines.Sum(Function(x) x.Credit)

        Using frm As New Form()
            Using grid As New DataGridView()
                Using pnl As New Panel()
                    Using lblTotals As New Label()
                        Using btnOk As New Button()
                            Using btnCancel As New Button()
                                frm.Text = title
                                frm.StartPosition = FormStartPosition.CenterParent
                                frm.Size = New Size(760, 390)
                                frm.FormBorderStyle = FormBorderStyle.FixedDialog
                                frm.MaximizeBox = False
                                frm.MinimizeBox = False
                                frm.RightToLeft = RightToLeft.Yes
                                frm.RightToLeftLayout = True
                                frm.Font = New Font("Segoe UI", 9.0!, FontStyle.Regular)

                                grid.Dock = DockStyle.Top
                                grid.Height = 285
                                grid.ReadOnly = True
                                grid.AllowUserToAddRows = False
                                grid.AllowUserToDeleteRows = False
                                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                                grid.RowHeadersVisible = False
                                grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                                grid.DataSource = lines.Select(Function(x) New With {
                                    .الحساب = x.AccountCode,
                                    .اسم_الحساب = x.AccountName,
                                    .البيان = x.Statement,
                                    .مدين = x.Credit.ToString("N3"),
                                    .دائن = x.Debit.ToString("N3")
                                }).ToList()

                                pnl.Dock = DockStyle.Bottom
                                pnl.Height = 62

                                lblTotals.AutoSize = False
                                lblTotals.TextAlign = ContentAlignment.MiddleRight
                                lblTotals.Location = New Point(300, 8)
                                lblTotals.Size = New Size(430, 42)
                                lblTotals.Font = New Font("Segoe UI Semibold", 10.0!, FontStyle.Bold)
                                lblTotals.Text = "إجمالي المدين: " & totalDebit.ToString("N3") &
                                                 "    إجمالي الدائن: " & totalCredit.ToString("N3")

                                btnOk.Text = If(requireConfirm, "اعتماد", "إغلاق")
                                btnOk.Size = New Size(95, 34)
                                btnOk.Location = New Point(105, 14)
                                btnOk.DialogResult = DialogResult.OK

                                btnCancel.Text = "إلغاء"
                                btnCancel.Size = New Size(95, 34)
                                btnCancel.Location = New Point(10, 14)
                                btnCancel.DialogResult = DialogResult.Cancel
                                btnCancel.Visible = requireConfirm

                                pnl.Controls.Add(lblTotals)
                                pnl.Controls.Add(btnOk)
                                pnl.Controls.Add(btnCancel)
                                frm.Controls.Add(grid)
                                frm.Controls.Add(pnl)
                                frm.AcceptButton = btnOk
                                frm.CancelButton = If(requireConfirm, btnCancel, Nothing)

                                If totalDebit <> totalCredit Then
                                    lblTotals.ForeColor = Color.DarkRed
                                    btnOk.Enabled = Not requireConfirm
                                Else
                                    lblTotals.ForeColor = Color.DarkGreen
                                End If

                                Return frm.ShowDialog(Me) = DialogResult.OK
                            End Using
                        End Using
                    End Using
                End Using
            End Using
        End Using
    End Function


    Private Sub BtnApprove_Click(sender As Object, e As EventArgs) Handles BtnApprove.Click
        If dgvEntries.CurrentRow Is Nothing Then Exit Sub

        Dim budgetEntryId As Integer = CInt(dgvEntries.CurrentRow.Cells("BudgetEntryId").Value) ' أو من الصف المحدد
        Dim userId As Integer = USER_ID

        Dim budgetItemExpenseAccountCode As String = ResolveBudgetItemExpenseAccountForApproval(budgetEntryId)
        If String.IsNullOrWhiteSpace(budgetItemExpenseAccountCode) Then
            MessageBox.Show("لم يتم تحديد حساب مصروف البند. يرجى ربط البند بحساب من الدليل.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim paymentAccountCode As String = ResolvePaymentAccountForApproval(budgetEntryId)

        If String.IsNullOrWhiteSpace(paymentAccountCode) Then
            MessageBox.Show("لم يتم تحديد حساب الدفع / المستفيد للصرف.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        SavePaymentAccountForEntry(budgetEntryId, paymentAccountCode)

        Dim stampInfo As DataRow = LoadStampInfoForEntry(budgetEntryId)
        Dim stampAccountCode As String = CellText(stampInfo, "StampAccountCode")
        Dim stampPercent As Decimal = CellDecimal(stampInfo, "StampPercent")

        If IsStampEnabled(stampInfo) Then
            If String.IsNullOrWhiteSpace(stampAccountCode) Then
                stampAccountCode = PickStampAccountForApproval()
                If String.IsNullOrWhiteSpace(stampAccountCode) Then Exit Sub
                SaveStampAccountForEntry(budgetEntryId, stampAccountCode)
            End If

            If String.IsNullOrWhiteSpace(GetAccountName(stampAccountCode)) Then
                MessageBox.Show("حساب الدمغة غير موجود في شجرة الحسابات", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        Dim previewLines As List(Of JournalPreviewLine) = BuildJournalPreviewLines(budgetEntryId, budgetItemExpenseAccountCode, paymentAccountCode, stampAccountCode)
        If Not ShowJournalPreview(previewLines, "معاينة القيد قبل الاعتماد", True) Then Exit Sub

        Using con As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("dbo.Budget_Approve_Entry", con)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@BudgetEntryId", budgetEntryId)
                cmd.Parameters.AddWithValue("@ApprovedBy", userId)
                cmd.Parameters.Add("@ExpenseAccountCode", SqlDbType.NVarChar, 40).Value = budgetItemExpenseAccountCode
                cmd.Parameters.Add("@ContraAccountCode", SqlDbType.NVarChar, 40).Value = paymentAccountCode
                cmd.Parameters.Add("@StampPercent", SqlDbType.Decimal).Value = If(IsStampEnabled(stampInfo), CType(stampPercent, Object), DBNull.Value)
                cmd.Parameters("@StampPercent").Precision = 18
                cmd.Parameters("@StampPercent").Scale = 3
                cmd.Parameters.Add("@StampAccountCode", SqlDbType.NVarChar, 40).Value = If(IsStampEnabled(stampInfo), CType(stampAccountCode, Object), DBNull.Value)


                Dim pAccId As New SqlParameter("@AccountingEntryId", SqlDbType.Int)
                pAccId.Direction = ParameterDirection.Output
                cmd.Parameters.Add(pAccId)

                Dim pMsg As New SqlParameter("@Msg", SqlDbType.NVarChar, 300)
                pMsg.Direction = ParameterDirection.Output
                cmd.Parameters.Add(pMsg)

                con.Open()
                If Not BudgetApproveSupportsExpenseAccountParameter(con) Then
                    MessageBox.Show(
                        "الإجراء المخزن Budget_Approve_Entry لا يدعم اختيار حساب البند بعد." & Environment.NewLine &
                        "حدّث الإجراء من سكربت Budget_Approve_Entry_dynamic_contra.sql ثم أعد الاعتماد.",
                        "تحديث مطلوب",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
                    Exit Sub
                End If

                If IsStampEnabled(stampInfo) AndAlso Not BudgetApproveUsesNetStampContra(con) Then
                    MessageBox.Show(
                        "الإجراء المخزن Budget_Approve_Entry لا يدعم خصم الدمغة من حساب الدفع / المستفيد بشكل صحيح." & Environment.NewLine &
                        "حدّث الإجراء من سكربت Budget_Approve_Entry_dynamic_contra.sql ثم أعد الاعتماد.",
                        "تحديث مطلوب",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)
                    Exit Sub
                End If

                cmd.ExecuteNonQuery()

                Dim msg As String = If(pMsg.Value, "").ToString()
                Dim accId As Integer = 0
                If pAccId.Value IsNot DBNull.Value Then accId = CInt(pAccId.Value)

                MessageBox.Show(msg, "Budget Control", MessageBoxButtons.OK, MessageBoxIcon.Information)


                If accId > 0 Then
                    If MessageBox.Show("تم اعتماد إذن الصرف. هل تريد طباعة إذن الصرف؟",
                                       "طباعة",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Question,
                                       MessageBoxDefaultButton.Button1,
                                       MessageBoxOptions.RightAlign) = DialogResult.Yes Then
                        PrintBudgetSpendVoucher(budgetEntryId)
                    End If

                    ' ✅ اختيارياً: فتح شاشة القيود الجاهزة على نفس القيد

                    '-------------------------------------------------
                    'F_ACC_B = New ACC_B
                    'F_ACC_B.UP_ToolStripBtn.Enabled = False
                    'F_ACC_B.DOWN_ToolStripBtn.Enabled = False
                    'F_ACC_B.LAST_ToolStripBtn.Enabled = False
                    'F_ACC_B.First_ToolStripBtn.Enabled = False
                    'F_ACC_B.T_ID_txt_2.Enabled = False
                    'F_ACC_B.Text = " عرض القيــد ( " & accId & " )  "
                    'F_ACC_B.NEW_Btn.Enabled = False

                    'F_ACC_B.is_Select = True
                    'T_ID_Search = accId

                    'F_ACC_B.Selected_ACC_CODE = 0

                    'F_ACC_B.ShowDialog()
                    'T_ID_Search = 0
                    '-------------------------------------------------

                    btnRefresh.PerformClick()


                End If
            End Using
        End Using
    End Sub

    Private Function ResolvePaymentAccountForApproval(budgetEntryId As Integer) As String
        Dim paymentAccountCode As String = GetPaymentAccountCodeForEntry(budgetEntryId)

        If String.IsNullOrWhiteSpace(paymentAccountCode) Then
            MessageBox.Show(
                "لم يتم تحديد حساب الدفع / المستفيد لهذا الإذن." & Environment.NewLine &
                "لن يتم فتح شاشة البحث العام عن الحسابات. أدخل حساب الدفع في بيانات إذن الصرف أو الحساب المحفوظ للبند أولاً.",
                "حساب الدفع / المستفيد",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)
            Return ""
        End If

        Dim paymentAccountName As String = GetAccountName(paymentAccountCode)
        If String.IsNullOrWhiteSpace(paymentAccountName) Then
            MessageBox.Show("حساب الدفع / المستفيد غير موجود في شجرة الحسابات.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return ""
        End If

        Dim msg As String =
            "سيتم استخدام حساب الدفع / المستفيد المحفوظ:" & Environment.NewLine &
            paymentAccountCode & " - " & paymentAccountName & Environment.NewLine & Environment.NewLine &
            "هل تريد المتابعة؟"

        If MessageBox.Show(
            msg,
            "حساب الدفع / المستفيد",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.RightAlign
        ) <> DialogResult.Yes Then
            Return ""
        End If

        Return paymentAccountCode
    End Function

    Private Function PickStampAccountForApproval() As String
        Dim stampAccountCode As String = If(MY_Settings.Default_Stamp_Account_Code, "").Trim()
        If String.IsNullOrWhiteSpace(stampAccountCode) OrElse String.IsNullOrWhiteSpace(GetAccountName(stampAccountCode)) Then
            MessageBox.Show(
                "لم يتم ضبط حساب الدمغة الافتراضي في شاشة إدارة النظام.",
                "حساب الدمغة",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning)
            Return ""
        End If

        Return stampAccountCode
    End Function

    Private Function BudgetApproveSupportsExpenseAccountParameter(con As SqlConnection) As Boolean
        If con.State <> ConnectionState.Open Then con.Open()

        Using cmd As New SqlCommand("
SELECT COUNT(1)
FROM sys.parameters p
INNER JOIN sys.objects o ON o.object_id = p.object_id
INNER JOIN sys.schemas s ON s.schema_id = o.schema_id
WHERE s.name = N'dbo'
  AND o.name = N'Budget_Approve_Entry'
  AND p.name = N'@ExpenseAccountCode';", con)

            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
        End Using
    End Function

    Private Function BudgetApproveUsesNetStampContra(con As SqlConnection) As Boolean
        If con.State <> ConnectionState.Open Then con.Open()

        Using cmd As New SqlCommand("
SELECT CASE
    WHEN ISNULL(m.definition, N'') LIKE N'%@IsOnlyMaster = CASE WHEN @StampAmount > 0 THEN 1 ELSE 0 END%'
     AND ISNULL(m.definition, N'') LIKE N'%@is_Only_Master = @IsOnlyMaster%'
     AND ISNULL(m.definition, N'') LIKE N'%@ACC_CODE = @ContraAccountCode%'
     AND ISNULL(m.definition, N'') LIKE N'%@CREDIT = @NetAmount%'
     AND ISNULL(m.definition, N'') LIKE N'%@ACC_CODE = @StampAccountCode%'
     AND ISNULL(m.definition, N'') LIKE N'%@CREDIT = @StampAmount%'
    THEN 1 ELSE 0 END
FROM sys.sql_modules m
WHERE m.object_id = OBJECT_ID(N'dbo.Budget_Approve_Entry');", con)

            Dim value = cmd.ExecuteScalar()
            If value Is Nothing OrElse value Is DBNull.Value Then Return False

            Return Convert.ToInt32(value) = 1
        End Using
    End Function

    Private Sub SaveStampAccountForEntry(budgetEntryId As Integer, stampAccountCode As String)
        Using con As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
UPDATE dbo.Budget_Entries
SET StampAccountCode = @StampAccountCode
WHERE BudgetEntryId = @BudgetEntryId;", con)

                cmd.Parameters.Add("@BudgetEntryId", SqlDbType.Int).Value = budgetEntryId
                cmd.Parameters.Add("@StampAccountCode", SqlDbType.NVarChar, 40).Value = stampAccountCode
                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Function SaveSelectedEntryStampFromInputs(budgetEntryId As Integer) As Boolean
        If EntryMode <> 1 OrElse chkHasStamp Is Nothing Then Return True
        If dgvEntries.CurrentRow Is Nothing Then Return True

        Dim status As String = GridCellText(dgvEntries.CurrentRow, "EntryStatus")
        If status <> "غير معتمد" Then Return True

        Dim amount As Decimal = GetBudgetEntryAmount(budgetEntryId)
        Dim hasStamp As Boolean = chkHasStamp.Checked
        Dim stampPercentValue As Decimal = 0D
        Dim stampAmountValue As Decimal = 0D
        Dim stampPercent As Object = DBNull.Value
        Dim stampAccountCode As Object = DBNull.Value
        Dim stampAmount As Object = DBNull.Value

        If hasStamp Then
            If Not ApplyDefaultStampSettings(False) Then
                MessageBox.Show("اضبط إعدادات الدمغة الافتراضية من شاشة إدارة النظام قبل استخدامها.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If txtStampPercent Is Nothing OrElse Not Decimal.TryParse(txtStampPercent.Text.Trim(), stampPercentValue) OrElse stampPercentValue <= 0D Then
                MessageBox.Show("أدخل نسبة الدمغة بشكل صحيح", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                If txtStampPercent IsNot Nothing Then txtStampPercent.Focus()
                Return False
            End If

            stampAmountValue = Math.Round((amount * stampPercentValue) / 100D, 3)
            If stampAmountValue <= 0D OrElse stampAmountValue >= amount Then
                MessageBox.Show("قيمة الدمغة يجب أن تكون أكبر من صفر وأقل من مبلغ الصرف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                If txtStampPercent IsNot Nothing Then txtStampPercent.Focus()
                Return False
            End If

            If txtStampAccountCode Is Nothing OrElse String.IsNullOrWhiteSpace(txtStampAccountCode.Text) Then
                MessageBox.Show("اختر حساب الدمغة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                If btnPickStampAccount IsNot Nothing Then btnPickStampAccount.Focus()
                Return False
            End If

            If String.IsNullOrWhiteSpace(GetAccountName(txtStampAccountCode.Text.Trim())) Then
                MessageBox.Show("حساب الدمغة غير موجود في شجرة الحسابات", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtStampAccountCode.Focus()
                Return False
            End If

            stampPercent = stampPercentValue
            stampAmount = stampAmountValue
            stampAccountCode = txtStampAccountCode.Text.Trim()
        End If

        Using con As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
UPDATE dbo.Budget_Entries
SET HasStamp = @HasStamp,
    StampPercent = @StampPercent,
    StampAccountCode = @StampAccountCode,
    StampAmount = @StampAmount
WHERE BudgetEntryId = @BudgetEntryId
  AND AccountingEntryId IS NULL;", con)

                cmd.Parameters.Add("@BudgetEntryId", SqlDbType.Int).Value = budgetEntryId
                cmd.Parameters.Add("@HasStamp", SqlDbType.Bit).Value = hasStamp
                cmd.Parameters.Add("@StampPercent", SqlDbType.Decimal).Value = stampPercent
                cmd.Parameters("@StampPercent").Precision = 18
                cmd.Parameters("@StampPercent").Scale = 3
                cmd.Parameters.Add("@StampAccountCode", SqlDbType.NVarChar, 40).Value = stampAccountCode
                cmd.Parameters.Add("@StampAmount", SqlDbType.Decimal).Value = stampAmount
                cmd.Parameters("@StampAmount").Precision = 18
                cmd.Parameters("@StampAmount").Scale = 3

                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using

        Return True
    End Function

    Private Sub EditSelectedEntryStamp()
        If EntryMode <> 1 Then
            MessageBox.Show("تعديل الدمغة متاح في وضع الصرف فقط", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If dgvEntries.CurrentRow Is Nothing OrElse dgvEntries.CurrentRow.IsNewRow Then
            MessageBox.Show("اختر إذن صرف من القائمة أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim status As String = GridCellText(dgvEntries.CurrentRow, "EntryStatus")
        If status <> "غير معتمد" Then
            MessageBox.Show("يمكن تعديل الدمغة للإذن غير المعتمد فقط", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim budgetEntryId As Integer = Convert.ToInt32(dgvEntries.CurrentRow.Cells("BudgetEntryId").Value)
        Dim currentHasStamp As Boolean = GridCellDecimal(dgvEntries.CurrentRow, "StampAmount") > 0D OrElse
                                         GridCellDecimal(dgvEntries.CurrentRow, "StampPercent") > 0D
        Dim message As String =
            "رقم الإذن: " & budgetEntryId.ToString() & Environment.NewLine &
            "الحالة الحالية للدمغة: " & If(currentHasStamp, "مفعلة", "غير مفعلة") & Environment.NewLine & Environment.NewLine &
            "نعم: تفعيل / تحديث الدمغة من إعدادات النظام" & Environment.NewLine &
            "لا: إلغاء الدمغة من الإذن"

        Dim result As DialogResult = MessageBox.Show(message, "تعديل الدمغة", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
        If result = DialogResult.Cancel Then Return

        Dim enableStamp As Boolean = (result = DialogResult.Yes)
        If Not SaveSelectedEntryStampFromDefaults(budgetEntryId, enableStamp) Then Return

        RefreshSelectedRowStampValues(budgetEntryId)
        UpdateSelectedDetails()
        SetStatus(If(enableStamp, "تم تفعيل الدمغة للإذن رقم ", "تم إلغاء الدمغة للإذن رقم ") & budgetEntryId.ToString())
        btnRefresh.PerformClick()
    End Sub

    Private Function SaveSelectedEntryStampFromDefaults(budgetEntryId As Integer, enableStamp As Boolean) As Boolean
        Dim amount As Decimal = GetBudgetEntryAmount(budgetEntryId)
        Dim stampPercentValue As Decimal = 0D
        Dim stampAmountValue As Decimal = 0D
        Dim stampPercent As Object = DBNull.Value
        Dim stampAccountCode As Object = DBNull.Value
        Dim stampAmount As Object = DBNull.Value

        If enableStamp Then
            stampPercentValue = MY_Settings.Default_Stamp_Percent
            Dim defaultStampAccountCode As String = If(MY_Settings.Default_Stamp_Account_Code, "").Trim()

            If stampPercentValue <= 0D OrElse String.IsNullOrWhiteSpace(defaultStampAccountCode) Then
                MessageBox.Show("اضبط نسبة وحساب الدمغة الافتراضية من شاشة إدارة النظام قبل استخدامها.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If String.IsNullOrWhiteSpace(GetAccountName(defaultStampAccountCode)) Then
                MessageBox.Show("حساب الدمغة الافتراضي غير موجود في شجرة الحسابات", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            stampAmountValue = Math.Round((amount * stampPercentValue) / 100D, 3)
            If stampAmountValue <= 0D OrElse stampAmountValue >= amount Then
                MessageBox.Show("قيمة الدمغة يجب أن تكون أكبر من صفر وأقل من مبلغ الصرف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            stampPercent = stampPercentValue
            stampAccountCode = defaultStampAccountCode
            stampAmount = stampAmountValue
        End If

        Using con As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
UPDATE dbo.Budget_Entries
SET HasStamp = @HasStamp,
    StampPercent = @StampPercent,
    StampAccountCode = @StampAccountCode,
    StampAmount = @StampAmount
WHERE BudgetEntryId = @BudgetEntryId
  AND AccountingEntryId IS NULL;", con)

                cmd.Parameters.Add("@BudgetEntryId", SqlDbType.Int).Value = budgetEntryId
                cmd.Parameters.Add("@HasStamp", SqlDbType.Bit).Value = enableStamp
                cmd.Parameters.Add("@StampPercent", SqlDbType.Decimal).Value = stampPercent
                cmd.Parameters("@StampPercent").Precision = 18
                cmd.Parameters("@StampPercent").Scale = 3
                cmd.Parameters.Add("@StampAccountCode", SqlDbType.NVarChar, 40).Value = stampAccountCode
                cmd.Parameters.Add("@StampAmount", SqlDbType.Decimal).Value = stampAmount
                cmd.Parameters("@StampAmount").Precision = 18
                cmd.Parameters("@StampAmount").Scale = 3

                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using

        Return True
    End Function

    Private Sub RefreshSelectedRowStampValues(budgetEntryId As Integer)
        If dgvEntries.CurrentRow Is Nothing Then Return

        Dim stampInfo As DataRow = LoadStampInfoForEntry(budgetEntryId)
        Dim hasStamp As Boolean = IsStampEnabled(stampInfo)
        Dim stampAccountCode As String = If(hasStamp, CellText(stampInfo, "StampAccountCode"), "")
        Dim stampAccountName As String = If(hasStamp, GetAccountName(stampAccountCode), "")

        If dgvEntries.Columns.Contains("HasStamp") Then dgvEntries.CurrentRow.Cells("HasStamp").Value = hasStamp
        If dgvEntries.Columns.Contains("StampPercent") Then dgvEntries.CurrentRow.Cells("StampPercent").Value = If(hasStamp, CType(CellDecimal(stampInfo, "StampPercent"), Object), DBNull.Value)
        If dgvEntries.Columns.Contains("StampAccountCode") Then dgvEntries.CurrentRow.Cells("StampAccountCode").Value = If(hasStamp, CType(stampAccountCode, Object), DBNull.Value)
        If dgvEntries.Columns.Contains("StampAccountName") Then dgvEntries.CurrentRow.Cells("StampAccountName").Value = If(hasStamp, CType(stampAccountName, Object), DBNull.Value)
        If dgvEntries.Columns.Contains("StampAmount") Then dgvEntries.CurrentRow.Cells("StampAmount").Value = If(hasStamp, CType(CellDecimal(stampInfo, "StampAmount"), Object), DBNull.Value)
    End Sub

    Private Sub SavePaymentAccountForEntry(budgetEntryId As Integer, paymentAccountCode As String)
        Using con As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
UPDATE dbo.Budget_Entries
SET ContraAccountCode = @ContraAccountCode
WHERE BudgetEntryId = @BudgetEntryId
  AND AccountingEntryId IS NULL;", con)

                cmd.Parameters.Add("@BudgetEntryId", SqlDbType.Int).Value = budgetEntryId
                Dim accountValue As Object = DBNull.Value
                If Not String.IsNullOrWhiteSpace(paymentAccountCode) Then
                    accountValue = paymentAccountCode.Trim()
                End If
                cmd.Parameters.Add("@ContraAccountCode", SqlDbType.NVarChar, 40).Value = accountValue
                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

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

    Private Function GetPaymentAccountCodeForEntry(budgetEntryId As Integer) As String
        Using con As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
SELECT TOP 1 COALESCE(NULLIF(LTRIM(RTRIM(e.ContraAccountCode)), N''), i.ContraAccountCode)
FROM dbo.Budget_Entries e
JOIN dbo.Budget_Items i ON e.BudgetItemId = i.BudgetItemId
WHERE e.BudgetEntryId = @BudgetEntryId;", con)

                cmd.Parameters.AddWithValue("@BudgetEntryId", budgetEntryId)
                con.Open()

                Dim value = cmd.ExecuteScalar()
                If value Is Nothing OrElse value Is DBNull.Value Then Return ""

                Return value.ToString().Trim()
            End Using
        End Using
    End Function



    Private Sub BtnCancelEntry_Click(sender As Object, e As EventArgs) Handles BtnCancelEntry.Click

        ' 1) تحقق الصلاحية
        'If Not HasPermission(CurrentUser.id, "budget_cancel") Then
        '    MessageBox.Show("لا تملك صلاحية إلغاء إذن الصرف.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        ' 2) قراءة المعرفات من الصف المحدد
        Dim budgetEntryId As Integer = CInt(CInt(dgvEntries.CurrentRow.Cells("BudgetEntryId").Value)) ' أو من DataGridView
        'Dim accountingEntryId As Integer = If(IsDBNull(CurrentRowAccountingEntryId), 0, CInt(CurrentRowAccountingEntryId))

        If dgvEntries.CurrentRow Is Nothing Then Exit Sub
        Dim accountingEntryId As Integer = 0
        If Not IsDBNull(dgvEntries.CurrentRow.Cells("AccountingEntryId").Value) Then
            accountingEntryId = Convert.ToInt32(dgvEntries.CurrentRow.Cells("AccountingEntryId").Value)
        End If


        If accountingEntryId = 0 Then
            MessageBox.Show("لا يمكن الإلغاء: إذن الصرف غير معتمد.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' 3) تأكيد المستخدم
        Dim confirm = MessageBox.Show(
            "سيتم إنشاء قيد عكسي لإلغاء إذن الصرف." & vbCrLf &
            "هل أنت متأكد من المتابعة؟",
            "تأكيد الإلغاء",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        )

        If confirm <> DialogResult.Yes Then Exit Sub

        ' 4) استدعاء الإجراء
        Using con As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("dbo.Budget_Cancel_Entry", con)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@BudgetEntryId", budgetEntryId)
                cmd.Parameters.AddWithValue("@CanceledBy", USER_ID)

                Dim pRevJournalId As New SqlParameter("@ReversalJournalId", SqlDbType.Int)
                pRevJournalId.Direction = ParameterDirection.Output
                cmd.Parameters.Add(pRevJournalId)

                Dim pMsg As New SqlParameter("@Msg", SqlDbType.NVarChar, 300)
                pMsg.Direction = ParameterDirection.Output
                cmd.Parameters.Add(pMsg)

                con.Open()
                cmd.ExecuteNonQuery()

                Dim msg As String = If(pMsg.Value, "").ToString()
                Dim revJournalId As Integer = 0
                If pRevJournalId.Value IsNot DBNull.Value Then revJournalId = CInt(pRevJournalId.Value)

                MessageBox.Show(msg, "Budget Control", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' 5) (اختياري) فتح القيد العكسي
                'If revJournalId > 0 Then
                '    ' OpenJournalForm(revJournalId)
                'End If

                btnRefresh.PerformClick()
            End Using
        End Using

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

End Class




