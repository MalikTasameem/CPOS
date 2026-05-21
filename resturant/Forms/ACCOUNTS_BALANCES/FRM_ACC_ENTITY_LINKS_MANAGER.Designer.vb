<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FRM_ACC_ENTITY_LINKS_MANAGER
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSubTitle = New System.Windows.Forms.Label()
        Me.pnlFilters = New System.Windows.Forms.Panel()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.cmbMovement = New System.Windows.Forms.ComboBox()
        Me.lblMovement = New System.Windows.Forms.Label()
        Me.chkOnlyIssues = New System.Windows.Forms.CheckBox()
        Me.cmbEntityType = New System.Windows.Forms.ComboBox()
        Me.lblEntityType = New System.Windows.Forms.Label()
        Me.pnlCards = New System.Windows.Forms.Panel()
        Me.cardMovement = New System.Windows.Forms.Panel()
        Me.lblMovementValue = New System.Windows.Forms.Label()
        Me.lblMovementTitle = New System.Windows.Forms.Label()
        Me.cardMissing = New System.Windows.Forms.Panel()
        Me.lblMissingValue = New System.Windows.Forms.Label()
        Me.lblMissingTitle = New System.Windows.Forms.Label()
        Me.cardIssues = New System.Windows.Forms.Panel()
        Me.lblIssuesValue = New System.Windows.Forms.Label()
        Me.lblIssuesTitle = New System.Windows.Forms.Label()
        Me.cardOK = New System.Windows.Forms.Panel()
        Me.lblOKValue = New System.Windows.Forms.Label()
        Me.lblOKTitle = New System.Windows.Forms.Label()
        Me.cardTotal = New System.Windows.Forms.Panel()
        Me.lblTotalValue = New System.Windows.Forms.Label()
        Me.lblTotalTitle = New System.Windows.Forms.Label()
        Me.pnlActions = New System.Windows.Forms.Panel()
        Me.btnRulesSettings = New System.Windows.Forms.Button()
        Me.btnCreateAllMissingAccounts = New System.Windows.Forms.Button()
        Me.btnRepairAllLinks = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnOpenAccount = New System.Windows.Forms.Button()
        Me.btnValidateOne = New System.Windows.Forms.Button()
        Me.btnUnlockLink = New System.Windows.Forms.Button()
        Me.btnLockLink = New System.Windows.Forms.Button()
        Me.btnChangeAccount = New System.Windows.Forms.Button()
        Me.btnRepairLink = New System.Windows.Forms.Button()
        Me.btnCreateMissingAccount = New System.Windows.Forms.Button()
        Me.tabMain = New System.Windows.Forms.TabControl()
        Me.tabDetails = New System.Windows.Forms.TabPage()
        Me.dgvDetails = New System.Windows.Forms.DataGridView()
        Me.cmsDetails = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuCreateMissingAccount = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRepairLink = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuChangeAccount = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLockLink = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUnlockLink = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuValidateOne = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOpenAccount = New System.Windows.Forms.ToolStripMenuItem()
        Me.tabSummary = New System.Windows.Forms.TabPage()
        Me.dgvSummary = New System.Windows.Forms.DataGridView()
        Me.tabDuplicates = New System.Windows.Forms.TabPage()
        Me.dgvDuplicates = New System.Windows.Forms.DataGridView()
        Me.tabRulesIssues = New System.Windows.Forms.TabPage()
        Me.dgvRulesIssues = New System.Windows.Forms.DataGridView()
        Me.tabLinksIssues = New System.Windows.Forms.TabPage()
        Me.dgvLinksIssues = New System.Windows.Forms.DataGridView()
        Me.statusStripMain = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblSelectedInfo = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlHeader.SuspendLayout()
        Me.pnlFilters.SuspendLayout()
        Me.pnlCards.SuspendLayout()
        Me.cardMovement.SuspendLayout()
        Me.cardMissing.SuspendLayout()
        Me.cardIssues.SuspendLayout()
        Me.cardOK.SuspendLayout()
        Me.cardTotal.SuspendLayout()
        Me.pnlActions.SuspendLayout()
        Me.tabMain.SuspendLayout()
        Me.tabDetails.SuspendLayout()
        CType(Me.dgvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsDetails.SuspendLayout()
        Me.tabSummary.SuspendLayout()
        CType(Me.dgvSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabDuplicates.SuspendLayout()
        CType(Me.dgvDuplicates, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabRulesIssues.SuspendLayout()
        CType(Me.dgvRulesIssues, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabLinksIssues.SuspendLayout()
        CType(Me.dgvLinksIssues, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.statusStripMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.lblSubTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1280, 72)
        Me.pnlHeader.TabIndex = 4
        '
        'lblTitle
        '
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Padding = New System.Windows.Forms.Padding(0, 6, 16, 0)
        Me.lblTitle.Size = New System.Drawing.Size(1280, 38)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "إدارة الربط المحاسبي"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSubTitle
        '
        Me.lblSubTitle.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblSubTitle.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblSubTitle.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblSubTitle.Location = New System.Drawing.Point(0, 38)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Padding = New System.Windows.Forms.Padding(0, 0, 16, 8)
        Me.lblSubTitle.Size = New System.Drawing.Size(1280, 34)
        Me.lblSubTitle.TabIndex = 1
        Me.lblSubTitle.Text = "متابعة ربط الزبائن، الموردين، الموظفين، المخازن، الخزائن، المصارف، والمصروفات بال" &
    "دليل المحاسبي"
        Me.lblSubTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlFilters
        '
        Me.pnlFilters.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlFilters.Controls.Add(Me.btnRefresh)
        Me.pnlFilters.Controls.Add(Me.txtSearch)
        Me.pnlFilters.Controls.Add(Me.lblSearch)
        Me.pnlFilters.Controls.Add(Me.cmbMovement)
        Me.pnlFilters.Controls.Add(Me.lblMovement)
        Me.pnlFilters.Controls.Add(Me.chkOnlyIssues)
        Me.pnlFilters.Controls.Add(Me.cmbEntityType)
        Me.pnlFilters.Controls.Add(Me.lblEntityType)
        Me.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilters.Location = New System.Drawing.Point(0, 72)
        Me.pnlFilters.Name = "pnlFilters"
        Me.pnlFilters.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlFilters.Size = New System.Drawing.Size(1280, 50)
        Me.pnlFilters.TabIndex = 3
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(25, 15)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(140, 30)
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.Text = "تحديث"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(180, 18)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(270, 23)
        Me.txtSearch.TabIndex = 1
        '
        'lblSearch
        '
        Me.lblSearch.Location = New System.Drawing.Point(455, 20)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(65, 24)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "بحث:"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbMovement
        '
        Me.cmbMovement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMovement.Location = New System.Drawing.Point(530, 18)
        Me.cmbMovement.Name = "cmbMovement"
        Me.cmbMovement.Size = New System.Drawing.Size(135, 23)
        Me.cmbMovement.TabIndex = 3
        '
        'lblMovement
        '
        Me.lblMovement.Location = New System.Drawing.Point(670, 20)
        Me.lblMovement.Name = "lblMovement"
        Me.lblMovement.Size = New System.Drawing.Size(80, 24)
        Me.lblMovement.TabIndex = 4
        Me.lblMovement.Text = "الحركة:"
        Me.lblMovement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkOnlyIssues
        '
        Me.chkOnlyIssues.Location = New System.Drawing.Point(760, 18)
        Me.chkOnlyIssues.Name = "chkOnlyIssues"
        Me.chkOnlyIssues.Size = New System.Drawing.Size(130, 24)
        Me.chkOnlyIssues.TabIndex = 5
        Me.chkOnlyIssues.Text = "عرض المشاكل فقط"
        Me.chkOnlyIssues.UseVisualStyleBackColor = True
        '
        'cmbEntityType
        '
        Me.cmbEntityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEntityType.Location = New System.Drawing.Point(900, 18)
        Me.cmbEntityType.Name = "cmbEntityType"
        Me.cmbEntityType.Size = New System.Drawing.Size(225, 23)
        Me.cmbEntityType.TabIndex = 6
        '
        'lblEntityType
        '
        Me.lblEntityType.Location = New System.Drawing.Point(1130, 20)
        Me.lblEntityType.Name = "lblEntityType"
        Me.lblEntityType.Size = New System.Drawing.Size(80, 24)
        Me.lblEntityType.TabIndex = 7
        Me.lblEntityType.Text = "النوع:"
        Me.lblEntityType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlCards
        '
        Me.pnlCards.BackColor = System.Drawing.Color.White
        Me.pnlCards.Controls.Add(Me.cardMovement)
        Me.pnlCards.Controls.Add(Me.cardMissing)
        Me.pnlCards.Controls.Add(Me.cardIssues)
        Me.pnlCards.Controls.Add(Me.cardOK)
        Me.pnlCards.Controls.Add(Me.cardTotal)
        Me.pnlCards.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCards.Location = New System.Drawing.Point(0, 122)
        Me.pnlCards.Name = "pnlCards"
        Me.pnlCards.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlCards.Size = New System.Drawing.Size(1280, 75)
        Me.pnlCards.TabIndex = 2
        '
        'cardMovement
        '
        Me.cardMovement.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cardMovement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardMovement.Controls.Add(Me.lblMovementValue)
        Me.cardMovement.Controls.Add(Me.lblMovementTitle)
        Me.cardMovement.Location = New System.Drawing.Point(19, 3)
        Me.cardMovement.Name = "cardMovement"
        Me.cardMovement.Size = New System.Drawing.Size(220, 68)
        Me.cardMovement.TabIndex = 0
        '
        'lblMovementValue
        '
        Me.lblMovementValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMovementValue.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblMovementValue.Location = New System.Drawing.Point(0, 28)
        Me.lblMovementValue.Name = "lblMovementValue"
        Me.lblMovementValue.Size = New System.Drawing.Size(218, 38)
        Me.lblMovementValue.TabIndex = 0
        Me.lblMovementValue.Text = "0"
        Me.lblMovementValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMovementTitle
        '
        Me.lblMovementTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblMovementTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblMovementTitle.Name = "lblMovementTitle"
        Me.lblMovementTitle.Size = New System.Drawing.Size(218, 28)
        Me.lblMovementTitle.TabIndex = 1
        Me.lblMovementTitle.Text = "عليها حركة"
        Me.lblMovementTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cardMissing
        '
        Me.cardMissing.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cardMissing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardMissing.Controls.Add(Me.lblMissingValue)
        Me.cardMissing.Controls.Add(Me.lblMissingTitle)
        Me.cardMissing.Location = New System.Drawing.Point(264, 3)
        Me.cardMissing.Name = "cardMissing"
        Me.cardMissing.Size = New System.Drawing.Size(230, 68)
        Me.cardMissing.TabIndex = 1
        '
        'lblMissingValue
        '
        Me.lblMissingValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMissingValue.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblMissingValue.Location = New System.Drawing.Point(0, 28)
        Me.lblMissingValue.Name = "lblMissingValue"
        Me.lblMissingValue.Size = New System.Drawing.Size(228, 38)
        Me.lblMissingValue.TabIndex = 0
        Me.lblMissingValue.Text = "0"
        Me.lblMissingValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMissingTitle
        '
        Me.lblMissingTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblMissingTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblMissingTitle.Name = "lblMissingTitle"
        Me.lblMissingTitle.Size = New System.Drawing.Size(228, 28)
        Me.lblMissingTitle.TabIndex = 1
        Me.lblMissingTitle.Text = "بدون حساب"
        Me.lblMissingTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cardIssues
        '
        Me.cardIssues.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cardIssues.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardIssues.Controls.Add(Me.lblIssuesValue)
        Me.cardIssues.Controls.Add(Me.lblIssuesTitle)
        Me.cardIssues.Location = New System.Drawing.Point(519, 3)
        Me.cardIssues.Name = "cardIssues"
        Me.cardIssues.Size = New System.Drawing.Size(230, 68)
        Me.cardIssues.TabIndex = 2
        '
        'lblIssuesValue
        '
        Me.lblIssuesValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblIssuesValue.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblIssuesValue.Location = New System.Drawing.Point(0, 28)
        Me.lblIssuesValue.Name = "lblIssuesValue"
        Me.lblIssuesValue.Size = New System.Drawing.Size(228, 38)
        Me.lblIssuesValue.TabIndex = 0
        Me.lblIssuesValue.Text = "0"
        Me.lblIssuesValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIssuesTitle
        '
        Me.lblIssuesTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblIssuesTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblIssuesTitle.Name = "lblIssuesTitle"
        Me.lblIssuesTitle.Size = New System.Drawing.Size(228, 28)
        Me.lblIssuesTitle.TabIndex = 1
        Me.lblIssuesTitle.Text = "مشاكل"
        Me.lblIssuesTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cardOK
        '
        Me.cardOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cardOK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardOK.Controls.Add(Me.lblOKValue)
        Me.cardOK.Controls.Add(Me.lblOKTitle)
        Me.cardOK.Location = New System.Drawing.Point(774, 3)
        Me.cardOK.Name = "cardOK"
        Me.cardOK.Size = New System.Drawing.Size(230, 68)
        Me.cardOK.TabIndex = 3
        '
        'lblOKValue
        '
        Me.lblOKValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOKValue.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblOKValue.Location = New System.Drawing.Point(0, 28)
        Me.lblOKValue.Name = "lblOKValue"
        Me.lblOKValue.Size = New System.Drawing.Size(228, 38)
        Me.lblOKValue.TabIndex = 0
        Me.lblOKValue.Text = "0"
        Me.lblOKValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOKTitle
        '
        Me.lblOKTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblOKTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblOKTitle.Name = "lblOKTitle"
        Me.lblOKTitle.Size = New System.Drawing.Size(228, 28)
        Me.lblOKTitle.TabIndex = 1
        Me.lblOKTitle.Text = "سليم"
        Me.lblOKTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cardTotal
        '
        Me.cardTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cardTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardTotal.Controls.Add(Me.lblTotalValue)
        Me.cardTotal.Controls.Add(Me.lblTotalTitle)
        Me.cardTotal.Location = New System.Drawing.Point(1029, 3)
        Me.cardTotal.Name = "cardTotal"
        Me.cardTotal.Size = New System.Drawing.Size(230, 68)
        Me.cardTotal.TabIndex = 4
        '
        'lblTotalValue
        '
        Me.lblTotalValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotalValue.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalValue.Location = New System.Drawing.Point(0, 28)
        Me.lblTotalValue.Name = "lblTotalValue"
        Me.lblTotalValue.Size = New System.Drawing.Size(228, 38)
        Me.lblTotalValue.TabIndex = 0
        Me.lblTotalValue.Text = "0"
        Me.lblTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTotalTitle
        '
        Me.lblTotalTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTotalTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTotalTitle.Name = "lblTotalTitle"
        Me.lblTotalTitle.Size = New System.Drawing.Size(228, 28)
        Me.lblTotalTitle.TabIndex = 1
        Me.lblTotalTitle.Text = "إجمالي الكيانات"
        Me.lblTotalTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlActions
        '
        Me.pnlActions.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlActions.Controls.Add(Me.btnRulesSettings)
        Me.pnlActions.Controls.Add(Me.btnCreateAllMissingAccounts)
        Me.pnlActions.Controls.Add(Me.btnRepairAllLinks)
        Me.pnlActions.Controls.Add(Me.btnClose)
        Me.pnlActions.Controls.Add(Me.btnOpenAccount)
        Me.pnlActions.Controls.Add(Me.btnValidateOne)
        Me.pnlActions.Controls.Add(Me.btnUnlockLink)
        Me.pnlActions.Controls.Add(Me.btnLockLink)
        Me.pnlActions.Controls.Add(Me.btnChangeAccount)
        Me.pnlActions.Controls.Add(Me.btnRepairLink)
        Me.pnlActions.Controls.Add(Me.btnCreateMissingAccount)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlActions.Location = New System.Drawing.Point(0, 197)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlActions.Size = New System.Drawing.Size(1280, 76)
        Me.pnlActions.TabIndex = 1
        '
        'btnRulesSettings
        '
        Me.btnRulesSettings.Location = New System.Drawing.Point(189, 42)
        Me.btnRulesSettings.Name = "btnRulesSettings"
        Me.btnRulesSettings.Size = New System.Drawing.Size(120, 30)
        Me.btnRulesSettings.TabIndex = 10
        Me.btnRulesSettings.Text = "القواعد"
        Me.btnRulesSettings.UseVisualStyleBackColor = True
        '
        'btnCreateAllMissingAccounts
        '
        Me.btnCreateAllMissingAccounts.Location = New System.Drawing.Point(189, 4)
        Me.btnCreateAllMissingAccounts.Name = "btnCreateAllMissingAccounts"
        Me.btnCreateAllMissingAccounts.Size = New System.Drawing.Size(120, 30)
        Me.btnCreateAllMissingAccounts.TabIndex = 8
        Me.btnCreateAllMissingAccounts.Text = "فتح كل الناقص"
        Me.btnCreateAllMissingAccounts.UseVisualStyleBackColor = True
        '
        'btnRepairAllLinks
        '
        Me.btnRepairAllLinks.Location = New System.Drawing.Point(309, 4)
        Me.btnRepairAllLinks.Name = "btnRepairAllLinks"
        Me.btnRepairAllLinks.Size = New System.Drawing.Size(120, 30)
        Me.btnRepairAllLinks.TabIndex = 9
        Me.btnRepairAllLinks.Text = "إصلاح الكل"
        Me.btnRepairAllLinks.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(3, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 32)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "إغلاق"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnOpenAccount
        '
        Me.btnOpenAccount.Location = New System.Drawing.Point(430, 4)
        Me.btnOpenAccount.Name = "btnOpenAccount"
        Me.btnOpenAccount.Size = New System.Drawing.Size(120, 30)
        Me.btnOpenAccount.TabIndex = 1
        Me.btnOpenAccount.Text = "عرض الحساب"
        Me.btnOpenAccount.UseVisualStyleBackColor = True
        '
        'btnValidateOne
        '
        Me.btnValidateOne.Location = New System.Drawing.Point(550, 4)
        Me.btnValidateOne.Name = "btnValidateOne"
        Me.btnValidateOne.Size = New System.Drawing.Size(120, 30)
        Me.btnValidateOne.TabIndex = 2
        Me.btnValidateOne.Text = "فحص المحدد"
        Me.btnValidateOne.UseVisualStyleBackColor = True
        '
        'btnUnlockLink
        '
        Me.btnUnlockLink.Location = New System.Drawing.Point(671, 4)
        Me.btnUnlockLink.Name = "btnUnlockLink"
        Me.btnUnlockLink.Size = New System.Drawing.Size(120, 30)
        Me.btnUnlockLink.TabIndex = 3
        Me.btnUnlockLink.Text = "فك القفل"
        Me.btnUnlockLink.UseVisualStyleBackColor = True
        '
        'btnLockLink
        '
        Me.btnLockLink.Location = New System.Drawing.Point(792, 4)
        Me.btnLockLink.Name = "btnLockLink"
        Me.btnLockLink.Size = New System.Drawing.Size(120, 30)
        Me.btnLockLink.TabIndex = 4
        Me.btnLockLink.Text = "قفل الرابط"
        Me.btnLockLink.UseVisualStyleBackColor = True
        '
        'btnChangeAccount
        '
        Me.btnChangeAccount.Location = New System.Drawing.Point(913, 4)
        Me.btnChangeAccount.Name = "btnChangeAccount"
        Me.btnChangeAccount.Size = New System.Drawing.Size(120, 30)
        Me.btnChangeAccount.TabIndex = 5
        Me.btnChangeAccount.Text = "تغيير الحساب"
        Me.btnChangeAccount.UseVisualStyleBackColor = True
        '
        'btnRepairLink
        '
        Me.btnRepairLink.Location = New System.Drawing.Point(1034, 4)
        Me.btnRepairLink.Name = "btnRepairLink"
        Me.btnRepairLink.Size = New System.Drawing.Size(120, 30)
        Me.btnRepairLink.TabIndex = 6
        Me.btnRepairLink.Text = "إصلاح الرابط"
        Me.btnRepairLink.UseVisualStyleBackColor = True
        '
        'btnCreateMissingAccount
        '
        Me.btnCreateMissingAccount.Location = New System.Drawing.Point(1155, 4)
        Me.btnCreateMissingAccount.Name = "btnCreateMissingAccount"
        Me.btnCreateMissingAccount.Size = New System.Drawing.Size(120, 30)
        Me.btnCreateMissingAccount.TabIndex = 7
        Me.btnCreateMissingAccount.Text = "فتح حساب ناقص"
        Me.btnCreateMissingAccount.UseVisualStyleBackColor = True
        '
        'tabMain
        '
        Me.tabMain.Controls.Add(Me.tabDetails)
        Me.tabMain.Controls.Add(Me.tabSummary)
        Me.tabMain.Controls.Add(Me.tabDuplicates)
        Me.tabMain.Controls.Add(Me.tabRulesIssues)
        Me.tabMain.Controls.Add(Me.tabLinksIssues)
        Me.tabMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabMain.Location = New System.Drawing.Point(0, 273)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedIndex = 0
        Me.tabMain.Size = New System.Drawing.Size(1280, 465)
        Me.tabMain.TabIndex = 0
        '
        'tabDetails
        '
        Me.tabDetails.Controls.Add(Me.dgvDetails)
        Me.tabDetails.Location = New System.Drawing.Point(4, 24)
        Me.tabDetails.Name = "tabDetails"
        Me.tabDetails.Size = New System.Drawing.Size(1272, 437)
        Me.tabDetails.TabIndex = 0
        Me.tabDetails.Text = "تفاصيل الربط"
        '
        'dgvDetails
        '
        Me.dgvDetails.AllowUserToAddRows = False
        Me.dgvDetails.AllowUserToDeleteRows = False
        Me.dgvDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvDetails.BackgroundColor = System.Drawing.Color.White
        Me.dgvDetails.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvDetails.ContextMenuStrip = Me.cmsDetails
        Me.dgvDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDetails.Location = New System.Drawing.Point(0, 0)
        Me.dgvDetails.MultiSelect = False
        Me.dgvDetails.Name = "dgvDetails"
        Me.dgvDetails.ReadOnly = True
        Me.dgvDetails.RowHeadersVisible = False
        Me.dgvDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetails.Size = New System.Drawing.Size(1272, 437)
        Me.dgvDetails.TabIndex = 0
        '
        'cmsDetails
        '
        Me.cmsDetails.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cmsDetails.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCreateMissingAccount, Me.mnuRepairLink, Me.mnuChangeAccount, Me.mnuLockLink, Me.mnuUnlockLink, Me.mnuValidateOne, Me.mnuOpenAccount})
        Me.cmsDetails.Name = "cmsDetails"
        Me.cmsDetails.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cmsDetails.Size = New System.Drawing.Size(158, 158)
        '
        'mnuCreateMissingAccount
        '
        Me.mnuCreateMissingAccount.Name = "mnuCreateMissingAccount"
        Me.mnuCreateMissingAccount.Size = New System.Drawing.Size(157, 22)
        Me.mnuCreateMissingAccount.Text = "فتح حساب ناقص"
        '
        'mnuRepairLink
        '
        Me.mnuRepairLink.Name = "mnuRepairLink"
        Me.mnuRepairLink.Size = New System.Drawing.Size(157, 22)
        Me.mnuRepairLink.Text = "إصلاح الرابط"
        '
        'mnuChangeAccount
        '
        Me.mnuChangeAccount.Name = "mnuChangeAccount"
        Me.mnuChangeAccount.Size = New System.Drawing.Size(157, 22)
        Me.mnuChangeAccount.Text = "تغيير الحساب"
        '
        'mnuLockLink
        '
        Me.mnuLockLink.Name = "mnuLockLink"
        Me.mnuLockLink.Size = New System.Drawing.Size(157, 22)
        Me.mnuLockLink.Text = "قفل الرابط"
        '
        'mnuUnlockLink
        '
        Me.mnuUnlockLink.Name = "mnuUnlockLink"
        Me.mnuUnlockLink.Size = New System.Drawing.Size(157, 22)
        Me.mnuUnlockLink.Text = "فك القفل"
        '
        'mnuValidateOne
        '
        Me.mnuValidateOne.Name = "mnuValidateOne"
        Me.mnuValidateOne.Size = New System.Drawing.Size(157, 22)
        Me.mnuValidateOne.Text = "فحص المحدد"
        '
        'mnuOpenAccount
        '
        Me.mnuOpenAccount.Name = "mnuOpenAccount"
        Me.mnuOpenAccount.Size = New System.Drawing.Size(157, 22)
        Me.mnuOpenAccount.Text = "عرض الحساب"
        '
        'tabSummary
        '
        Me.tabSummary.Controls.Add(Me.dgvSummary)
        Me.tabSummary.Location = New System.Drawing.Point(4, 24)
        Me.tabSummary.Name = "tabSummary"
        Me.tabSummary.Size = New System.Drawing.Size(1272, 437)
        Me.tabSummary.TabIndex = 1
        Me.tabSummary.Text = "الملخص حسب النوع"
        '
        'dgvSummary
        '
        Me.dgvSummary.AllowUserToAddRows = False
        Me.dgvSummary.AllowUserToDeleteRows = False
        Me.dgvSummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvSummary.BackgroundColor = System.Drawing.Color.White
        Me.dgvSummary.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSummary.Location = New System.Drawing.Point(0, 0)
        Me.dgvSummary.MultiSelect = False
        Me.dgvSummary.Name = "dgvSummary"
        Me.dgvSummary.ReadOnly = True
        Me.dgvSummary.RowHeadersVisible = False
        Me.dgvSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSummary.Size = New System.Drawing.Size(1272, 437)
        Me.dgvSummary.TabIndex = 0
        '
        'tabDuplicates
        '
        Me.tabDuplicates.Controls.Add(Me.dgvDuplicates)
        Me.tabDuplicates.Location = New System.Drawing.Point(4, 24)
        Me.tabDuplicates.Name = "tabDuplicates"
        Me.tabDuplicates.Size = New System.Drawing.Size(1272, 437)
        Me.tabDuplicates.TabIndex = 2
        Me.tabDuplicates.Text = "حسابات مكررة"
        '
        'dgvDuplicates
        '
        Me.dgvDuplicates.AllowUserToAddRows = False
        Me.dgvDuplicates.AllowUserToDeleteRows = False
        Me.dgvDuplicates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvDuplicates.BackgroundColor = System.Drawing.Color.White
        Me.dgvDuplicates.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvDuplicates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDuplicates.Location = New System.Drawing.Point(0, 0)
        Me.dgvDuplicates.MultiSelect = False
        Me.dgvDuplicates.Name = "dgvDuplicates"
        Me.dgvDuplicates.ReadOnly = True
        Me.dgvDuplicates.RowHeadersVisible = False
        Me.dgvDuplicates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDuplicates.Size = New System.Drawing.Size(1272, 437)
        Me.dgvDuplicates.TabIndex = 0
        '
        'tabRulesIssues
        '
        Me.tabRulesIssues.Controls.Add(Me.dgvRulesIssues)
        Me.tabRulesIssues.Location = New System.Drawing.Point(4, 24)
        Me.tabRulesIssues.Name = "tabRulesIssues"
        Me.tabRulesIssues.Size = New System.Drawing.Size(1272, 437)
        Me.tabRulesIssues.TabIndex = 3
        Me.tabRulesIssues.Text = "مشاكل القواعد"
        '
        'dgvRulesIssues
        '
        Me.dgvRulesIssues.AllowUserToAddRows = False
        Me.dgvRulesIssues.AllowUserToDeleteRows = False
        Me.dgvRulesIssues.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvRulesIssues.BackgroundColor = System.Drawing.Color.White
        Me.dgvRulesIssues.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvRulesIssues.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvRulesIssues.Location = New System.Drawing.Point(0, 0)
        Me.dgvRulesIssues.MultiSelect = False
        Me.dgvRulesIssues.Name = "dgvRulesIssues"
        Me.dgvRulesIssues.ReadOnly = True
        Me.dgvRulesIssues.RowHeadersVisible = False
        Me.dgvRulesIssues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRulesIssues.Size = New System.Drawing.Size(1272, 437)
        Me.dgvRulesIssues.TabIndex = 0
        '
        'tabLinksIssues
        '
        Me.tabLinksIssues.Controls.Add(Me.dgvLinksIssues)
        Me.tabLinksIssues.Location = New System.Drawing.Point(4, 24)
        Me.tabLinksIssues.Name = "tabLinksIssues"
        Me.tabLinksIssues.Size = New System.Drawing.Size(1272, 437)
        Me.tabLinksIssues.TabIndex = 4
        Me.tabLinksIssues.Text = "مشاكل الروابط"
        '
        'dgvLinksIssues
        '
        Me.dgvLinksIssues.AllowUserToAddRows = False
        Me.dgvLinksIssues.AllowUserToDeleteRows = False
        Me.dgvLinksIssues.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvLinksIssues.BackgroundColor = System.Drawing.Color.White
        Me.dgvLinksIssues.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvLinksIssues.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLinksIssues.Location = New System.Drawing.Point(0, 0)
        Me.dgvLinksIssues.MultiSelect = False
        Me.dgvLinksIssues.Name = "dgvLinksIssues"
        Me.dgvLinksIssues.ReadOnly = True
        Me.dgvLinksIssues.RowHeadersVisible = False
        Me.dgvLinksIssues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLinksIssues.Size = New System.Drawing.Size(1272, 437)
        Me.dgvLinksIssues.TabIndex = 0
        '
        'statusStripMain
        '
        Me.statusStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus, Me.lblSelectedInfo})
        Me.statusStripMain.Location = New System.Drawing.Point(0, 738)
        Me.statusStripMain.Name = "statusStripMain"
        Me.statusStripMain.Size = New System.Drawing.Size(1280, 22)
        Me.statusStripMain.TabIndex = 5
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(1195, 17)
        Me.lblStatus.Spring = True
        Me.lblStatus.Text = "جاهز"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSelectedInfo
        '
        Me.lblSelectedInfo.Name = "lblSelectedInfo"
        Me.lblSelectedInfo.Size = New System.Drawing.Size(70, 17)
        Me.lblSelectedInfo.Text = "لا يوجد تحديد"
        '
        'FRM_ACC_ENTITY_LINKS_MANAGER
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1280, 760)
        Me.Controls.Add(Me.tabMain)
        Me.Controls.Add(Me.pnlActions)
        Me.Controls.Add(Me.pnlCards)
        Me.Controls.Add(Me.pnlFilters)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.statusStripMain)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MinimumSize = New System.Drawing.Size(1100, 650)
        Me.Name = "FRM_ACC_ENTITY_LINKS_MANAGER"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إدارة الربط المحاسبي"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlFilters.ResumeLayout(False)
        Me.pnlFilters.PerformLayout()
        Me.pnlCards.ResumeLayout(False)
        Me.cardMovement.ResumeLayout(False)
        Me.cardMissing.ResumeLayout(False)
        Me.cardIssues.ResumeLayout(False)
        Me.cardOK.ResumeLayout(False)
        Me.cardTotal.ResumeLayout(False)
        Me.pnlActions.ResumeLayout(False)
        Me.tabMain.ResumeLayout(False)
        Me.tabDetails.ResumeLayout(False)
        CType(Me.dgvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsDetails.ResumeLayout(False)
        Me.tabSummary.ResumeLayout(False)
        CType(Me.dgvSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabDuplicates.ResumeLayout(False)
        CType(Me.dgvDuplicates, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabRulesIssues.ResumeLayout(False)
        CType(Me.dgvRulesIssues, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabLinksIssues.ResumeLayout(False)
        CType(Me.dgvLinksIssues, System.ComponentModel.ISupportInitialize).EndInit()
        Me.statusStripMain.ResumeLayout(False)
        Me.statusStripMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubTitle As Label

    Friend WithEvents pnlFilters As Panel
    Friend WithEvents lblEntityType As Label
    Friend WithEvents cmbEntityType As ComboBox
    Friend WithEvents chkOnlyIssues As CheckBox
    Friend WithEvents lblMovement As Label
    Friend WithEvents cmbMovement As ComboBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnRefresh As Button

    Friend WithEvents pnlCards As Panel
    Friend WithEvents cardTotal As Panel
    Friend WithEvents lblTotalTitle As Label
    Friend WithEvents lblTotalValue As Label

    Friend WithEvents cardOK As Panel
    Friend WithEvents lblOKTitle As Label
    Friend WithEvents lblOKValue As Label

    Friend WithEvents cardIssues As Panel
    Friend WithEvents lblIssuesTitle As Label
    Friend WithEvents lblIssuesValue As Label

    Friend WithEvents cardMissing As Panel
    Friend WithEvents lblMissingTitle As Label
    Friend WithEvents lblMissingValue As Label

    Friend WithEvents cardMovement As Panel
    Friend WithEvents lblMovementTitle As Label
    Friend WithEvents lblMovementValue As Label

    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnCreateMissingAccount As Button
    Friend WithEvents btnRepairLink As Button
    Friend WithEvents btnChangeAccount As Button
    Friend WithEvents btnLockLink As Button
    Friend WithEvents btnUnlockLink As Button
    Friend WithEvents btnValidateOne As Button
    Friend WithEvents btnOpenAccount As Button
    Friend WithEvents btnClose As Button

    Friend WithEvents tabMain As TabControl
    Friend WithEvents tabDetails As TabPage
    Friend WithEvents dgvDetails As DataGridView

    Friend WithEvents tabSummary As TabPage
    Friend WithEvents dgvSummary As DataGridView

    Friend WithEvents tabDuplicates As TabPage
    Friend WithEvents dgvDuplicates As DataGridView

    Friend WithEvents tabRulesIssues As TabPage
    Friend WithEvents dgvRulesIssues As DataGridView

    Friend WithEvents tabLinksIssues As TabPage
    Friend WithEvents dgvLinksIssues As DataGridView
    Friend WithEvents cmsDetails As ContextMenuStrip
    Friend WithEvents mnuCreateMissingAccount As ToolStripMenuItem
    Friend WithEvents mnuRepairLink As ToolStripMenuItem
    Friend WithEvents mnuChangeAccount As ToolStripMenuItem
    Friend WithEvents mnuLockLink As ToolStripMenuItem
    Friend WithEvents mnuUnlockLink As ToolStripMenuItem
    Friend WithEvents mnuValidateOne As ToolStripMenuItem
    Friend WithEvents mnuOpenAccount As ToolStripMenuItem

    Friend WithEvents statusStripMain As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel
    Friend WithEvents lblSelectedInfo As ToolStripStatusLabel
    Friend WithEvents btnCreateAllMissingAccounts As Button
    Friend WithEvents btnRepairAllLinks As Button
    Friend WithEvents btnRulesSettings As Button
End Class
