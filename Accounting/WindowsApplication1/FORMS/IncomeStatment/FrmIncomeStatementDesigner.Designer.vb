<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmIncomeStatementDesigner
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.btnFinalReport = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnValidate = New System.Windows.Forms.Button()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.dtpDateTo = New System.Windows.Forms.DateTimePicker()
        Me.lblDateTo = New System.Windows.Forms.Label()
        Me.dtpDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblDateFrom = New System.Windows.Forms.Label()
        Me.btnCreateDefaultTemplate = New System.Windows.Forms.Button()
        Me.cboTemplates = New System.Windows.Forms.ComboBox()
        Me.lblTemplate = New System.Windows.Forms.Label()
        Me.tabMain = New System.Windows.Forms.TabControl()
        Me.tabLines = New System.Windows.Forms.TabPage()
        Me.splitLines = New System.Windows.Forms.SplitContainer()
        Me.grpLines = New System.Windows.Forms.GroupBox()
        Me.dgvLines = New System.Windows.Forms.DataGridView()
        Me.pnlLineButtons = New System.Windows.Forms.Panel()
        Me.btnLineDown = New System.Windows.Forms.Button()
        Me.btnLineUp = New System.Windows.Forms.Button()
        Me.btnDisableLine = New System.Windows.Forms.Button()
        Me.btnEditLine = New System.Windows.Forms.Button()
        Me.btnAddLine = New System.Windows.Forms.Button()
        Me.grpLineDetails = New System.Windows.Forms.GroupBox()
        Me.chkShowWhenZero = New System.Windows.Forms.CheckBox()
        Me.chkIsVisible = New System.Windows.Forms.CheckBox()
        Me.chkIsBold = New System.Windows.Forms.CheckBox()
        Me.txtFormulaText = New System.Windows.Forms.TextBox()
        Me.lblFormulaText = New System.Windows.Forms.Label()
        Me.cboNormalBalanceSide = New System.Windows.Forms.ComboBox()
        Me.lblNormalBalanceSide = New System.Windows.Forms.Label()
        Me.cboDisplaySignMode = New System.Windows.Forms.ComboBox()
        Me.lblDisplaySignMode = New System.Windows.Forms.Label()
        Me.cboCalculationSign = New System.Windows.Forms.ComboBox()
        Me.lblCalculationSign = New System.Windows.Forms.Label()
        Me.cboLineType = New System.Windows.Forms.ComboBox()
        Me.lblLineType = New System.Windows.Forms.Label()
        Me.txtSortOrder = New System.Windows.Forms.TextBox()
        Me.lblSortOrder = New System.Windows.Forms.Label()
        Me.txtLineName = New System.Windows.Forms.TextBox()
        Me.lblLineName = New System.Windows.Forms.Label()
        Me.txtLineCode = New System.Windows.Forms.TextBox()
        Me.lblLineCode = New System.Windows.Forms.Label()
        Me.tabAccounts = New System.Windows.Forms.TabPage()
        Me.splitAccounts = New System.Windows.Forms.SplitContainer()
        Me.grpAccountLines = New System.Windows.Forms.GroupBox()
        Me.dgvAccountLines = New System.Windows.Forms.DataGridView()
        Me.grpLinkedAccounts = New System.Windows.Forms.GroupBox()
        Me.dgvLinkedAccounts = New System.Windows.Forms.DataGridView()
        Me.pnlAccountButtons = New System.Windows.Forms.Panel()
        Me.btnUpdateAccountLink = New System.Windows.Forms.Button()
        Me.btnUnlinkAccount = New System.Windows.Forms.Button()
        Me.btnLinkAccount = New System.Windows.Forms.Button()
        Me.tabFormula = New System.Windows.Forms.TabPage()
        Me.splitFormula = New System.Windows.Forms.SplitContainer()
        Me.grpFormulaLines = New System.Windows.Forms.GroupBox()
        Me.dgvFormulaLines = New System.Windows.Forms.DataGridView()
        Me.grpFormulaDetails = New System.Windows.Forms.GroupBox()
        Me.dgvFormulaDetails = New System.Windows.Forms.DataGridView()
        Me.pnlFormulaButtons = New System.Windows.Forms.Panel()
        Me.btnRebuildFormula = New System.Windows.Forms.Button()
        Me.btnRefreshFormula = New System.Windows.Forms.Button()
        Me.tabPreview = New System.Windows.Forms.TabPage()
        Me.grpPreview = New System.Windows.Forms.GroupBox()
        Me.dgvPreview = New System.Windows.Forms.DataGridView()
        Me.pnlPreviewButtons = New System.Windows.Forms.Panel()
        Me.chkHideZero = New System.Windows.Forms.CheckBox()
        Me.btnLoadPreview = New System.Windows.Forms.Button()
        Me.tabValidation = New System.Windows.Forms.TabPage()
        Me.grpValidation = New System.Windows.Forms.GroupBox()
        Me.dgvValidation = New System.Windows.Forms.DataGridView()
        Me.pnlValidationButtons = New System.Windows.Forms.Panel()
        Me.btnLoadValidation = New System.Windows.Forms.Button()
        Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlTop.SuspendLayout()
        Me.tabMain.SuspendLayout()
        Me.tabLines.SuspendLayout()
        CType(Me.splitLines, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitLines.Panel1.SuspendLayout()
        Me.splitLines.Panel2.SuspendLayout()
        Me.splitLines.SuspendLayout()
        Me.grpLines.SuspendLayout()
        CType(Me.dgvLines, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLineButtons.SuspendLayout()
        Me.grpLineDetails.SuspendLayout()
        Me.tabAccounts.SuspendLayout()
        CType(Me.splitAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitAccounts.Panel1.SuspendLayout()
        Me.splitAccounts.Panel2.SuspendLayout()
        Me.splitAccounts.SuspendLayout()
        Me.grpAccountLines.SuspendLayout()
        CType(Me.dgvAccountLines, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpLinkedAccounts.SuspendLayout()
        CType(Me.dgvLinkedAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAccountButtons.SuspendLayout()
        Me.tabFormula.SuspendLayout()
        CType(Me.splitFormula, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitFormula.Panel1.SuspendLayout()
        Me.splitFormula.Panel2.SuspendLayout()
        Me.splitFormula.SuspendLayout()
        Me.grpFormulaLines.SuspendLayout()
        CType(Me.dgvFormulaLines, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFormulaDetails.SuspendLayout()
        CType(Me.dgvFormulaDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFormulaButtons.SuspendLayout()
        Me.tabPreview.SuspendLayout()
        Me.grpPreview.SuspendLayout()
        CType(Me.dgvPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPreviewButtons.SuspendLayout()
        Me.tabValidation.SuspendLayout()
        Me.grpValidation.SuspendLayout()
        CType(Me.dgvValidation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlValidationButtons.SuspendLayout()
        Me.statusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.btnFinalReport)
        Me.pnlTop.Controls.Add(Me.btnClose)
        Me.pnlTop.Controls.Add(Me.btnRefresh)
        Me.pnlTop.Controls.Add(Me.btnValidate)
        Me.pnlTop.Controls.Add(Me.btnPreview)
        Me.pnlTop.Controls.Add(Me.dtpDateTo)
        Me.pnlTop.Controls.Add(Me.lblDateTo)
        Me.pnlTop.Controls.Add(Me.dtpDateFrom)
        Me.pnlTop.Controls.Add(Me.lblDateFrom)
        Me.pnlTop.Controls.Add(Me.btnCreateDefaultTemplate)
        Me.pnlTop.Controls.Add(Me.cboTemplates)
        Me.pnlTop.Controls.Add(Me.lblTemplate)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(8)
        Me.pnlTop.Size = New System.Drawing.Size(1220, 58)
        Me.pnlTop.TabIndex = 0
        '
        'btnFinalReport
        '
        Me.btnFinalReport.Location = New System.Drawing.Point(172, 13)
        Me.btnFinalReport.Name = "btnFinalReport"
        Me.btnFinalReport.Size = New System.Drawing.Size(80, 32)
        Me.btnFinalReport.TabIndex = 11
        Me.btnFinalReport.Text = "تقرير"
        Me.btnFinalReport.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(4, 13)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 32)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "إغلاق"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(88, 13)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(80, 32)
        Me.btnRefresh.TabIndex = 9
        Me.btnRefresh.Text = "تحديث"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnValidate
        '
        Me.btnValidate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnValidate.Location = New System.Drawing.Point(256, 13)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(80, 32)
        Me.btnValidate.TabIndex = 8
        Me.btnValidate.Text = "فحص"
        Me.btnValidate.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.Location = New System.Drawing.Point(340, 13)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(80, 32)
        Me.btnPreview.TabIndex = 7
        Me.btnPreview.Text = "معاينة"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'dtpDateTo
        '
        Me.dtpDateTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateTo.Location = New System.Drawing.Point(424, 18)
        Me.dtpDateTo.Name = "dtpDateTo"
        Me.dtpDateTo.Size = New System.Drawing.Size(104, 22)
        Me.dtpDateTo.TabIndex = 6
        '
        'lblDateTo
        '
        Me.lblDateTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDateTo.AutoSize = True
        Me.lblDateTo.Location = New System.Drawing.Point(532, 22)
        Me.lblDateTo.Name = "lblDateTo"
        Me.lblDateTo.Size = New System.Drawing.Size(51, 14)
        Me.lblDateTo.TabIndex = 5
        Me.lblDateTo.Text = "إلى تاريخ"
        '
        'dtpDateFrom
        '
        Me.dtpDateFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateFrom.Location = New System.Drawing.Point(587, 18)
        Me.dtpDateFrom.Name = "dtpDateFrom"
        Me.dtpDateFrom.Size = New System.Drawing.Size(105, 22)
        Me.dtpDateFrom.TabIndex = 4
        '
        'lblDateFrom
        '
        Me.lblDateFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDateFrom.AutoSize = True
        Me.lblDateFrom.Location = New System.Drawing.Point(696, 22)
        Me.lblDateFrom.Name = "lblDateFrom"
        Me.lblDateFrom.Size = New System.Drawing.Size(48, 14)
        Me.lblDateFrom.TabIndex = 3
        Me.lblDateFrom.Text = "من تاريخ"
        '
        'btnCreateDefaultTemplate
        '
        Me.btnCreateDefaultTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCreateDefaultTemplate.Location = New System.Drawing.Point(752, 13)
        Me.btnCreateDefaultTemplate.Name = "btnCreateDefaultTemplate"
        Me.btnCreateDefaultTemplate.Size = New System.Drawing.Size(128, 32)
        Me.btnCreateDefaultTemplate.TabIndex = 2
        Me.btnCreateDefaultTemplate.Text = "إنشاء قالب افتراضي"
        Me.btnCreateDefaultTemplate.UseVisualStyleBackColor = True
        '
        'cboTemplates
        '
        Me.cboTemplates.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTemplates.FormattingEnabled = True
        Me.cboTemplates.Location = New System.Drawing.Point(884, 18)
        Me.cboTemplates.Name = "cboTemplates"
        Me.cboTemplates.Size = New System.Drawing.Size(260, 22)
        Me.cboTemplates.TabIndex = 1
        '
        'lblTemplate
        '
        Me.lblTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTemplate.AutoSize = True
        Me.lblTemplate.Location = New System.Drawing.Point(1148, 21)
        Me.lblTemplate.Name = "lblTemplate"
        Me.lblTemplate.Size = New System.Drawing.Size(67, 14)
        Me.lblTemplate.TabIndex = 0
        Me.lblTemplate.Text = "قالب القائمة"
        '
        'tabMain
        '
        Me.tabMain.Controls.Add(Me.tabLines)
        Me.tabMain.Controls.Add(Me.tabAccounts)
        Me.tabMain.Controls.Add(Me.tabFormula)
        Me.tabMain.Controls.Add(Me.tabPreview)
        Me.tabMain.Controls.Add(Me.tabValidation)
        Me.tabMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabMain.Location = New System.Drawing.Point(0, 58)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.RightToLeftLayout = True
        Me.tabMain.SelectedIndex = 0
        Me.tabMain.Size = New System.Drawing.Size(1220, 620)
        Me.tabMain.TabIndex = 1
        '
        'tabLines
        '
        Me.tabLines.Controls.Add(Me.splitLines)
        Me.tabLines.Location = New System.Drawing.Point(4, 23)
        Me.tabLines.Name = "tabLines"
        Me.tabLines.Padding = New System.Windows.Forms.Padding(6)
        Me.tabLines.Size = New System.Drawing.Size(1272, 614)
        Me.tabLines.TabIndex = 0
        Me.tabLines.Text = "هيكل التقرير"
        Me.tabLines.UseVisualStyleBackColor = True
        '
        'splitLines
        '
        Me.splitLines.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitLines.Location = New System.Drawing.Point(6, 6)
        Me.splitLines.Name = "splitLines"
        '
        'splitLines.Panel1
        '
        Me.splitLines.Panel1.Controls.Add(Me.grpLines)
        Me.splitLines.Panel1.Controls.Add(Me.pnlLineButtons)
        Me.splitLines.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        '
        'splitLines.Panel2
        '
        Me.splitLines.Panel2.Controls.Add(Me.grpLineDetails)
        Me.splitLines.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.splitLines.Size = New System.Drawing.Size(1260, 602)
        Me.splitLines.SplitterDistance = 820
        Me.splitLines.TabIndex = 0
        '
        'grpLines
        '
        Me.grpLines.Controls.Add(Me.dgvLines)
        Me.grpLines.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpLines.Location = New System.Drawing.Point(0, 0)
        Me.grpLines.Name = "grpLines"
        Me.grpLines.Padding = New System.Windows.Forms.Padding(8)
        Me.grpLines.Size = New System.Drawing.Size(820, 554)
        Me.grpLines.TabIndex = 1
        Me.grpLines.TabStop = False
        Me.grpLines.Text = "بنود قائمة الدخل"
        '
        'dgvLines
        '
        Me.dgvLines.AllowUserToAddRows = False
        Me.dgvLines.AllowUserToDeleteRows = False
        Me.dgvLines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLines.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLines.Location = New System.Drawing.Point(8, 23)
        Me.dgvLines.MultiSelect = False
        Me.dgvLines.Name = "dgvLines"
        Me.dgvLines.ReadOnly = True
        Me.dgvLines.RowHeadersVisible = False
        Me.dgvLines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLines.Size = New System.Drawing.Size(804, 523)
        Me.dgvLines.TabIndex = 0
        '
        'pnlLineButtons
        '
        Me.pnlLineButtons.Controls.Add(Me.btnLineDown)
        Me.pnlLineButtons.Controls.Add(Me.btnLineUp)
        Me.pnlLineButtons.Controls.Add(Me.btnDisableLine)
        Me.pnlLineButtons.Controls.Add(Me.btnEditLine)
        Me.pnlLineButtons.Controls.Add(Me.btnAddLine)
        Me.pnlLineButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlLineButtons.Location = New System.Drawing.Point(0, 554)
        Me.pnlLineButtons.Name = "pnlLineButtons"
        Me.pnlLineButtons.Padding = New System.Windows.Forms.Padding(6)
        Me.pnlLineButtons.Size = New System.Drawing.Size(820, 48)
        Me.pnlLineButtons.TabIndex = 0
        '
        'btnLineDown
        '
        Me.btnLineDown.Location = New System.Drawing.Point(9, 9)
        Me.btnLineDown.Name = "btnLineDown"
        Me.btnLineDown.Size = New System.Drawing.Size(95, 30)
        Me.btnLineDown.TabIndex = 4
        Me.btnLineDown.Text = "لأسفل"
        Me.btnLineDown.UseVisualStyleBackColor = True
        '
        'btnLineUp
        '
        Me.btnLineUp.Location = New System.Drawing.Point(110, 9)
        Me.btnLineUp.Name = "btnLineUp"
        Me.btnLineUp.Size = New System.Drawing.Size(95, 30)
        Me.btnLineUp.TabIndex = 3
        Me.btnLineUp.Text = "لأعلى"
        Me.btnLineUp.UseVisualStyleBackColor = True
        '
        'btnDisableLine
        '
        Me.btnDisableLine.Location = New System.Drawing.Point(211, 9)
        Me.btnDisableLine.Name = "btnDisableLine"
        Me.btnDisableLine.Size = New System.Drawing.Size(95, 30)
        Me.btnDisableLine.TabIndex = 2
        Me.btnDisableLine.Text = "تعطيل"
        Me.btnDisableLine.UseVisualStyleBackColor = True
        '
        'btnEditLine
        '
        Me.btnEditLine.Location = New System.Drawing.Point(312, 9)
        Me.btnEditLine.Name = "btnEditLine"
        Me.btnEditLine.Size = New System.Drawing.Size(95, 30)
        Me.btnEditLine.TabIndex = 1
        Me.btnEditLine.Text = "تعديل"
        Me.btnEditLine.UseVisualStyleBackColor = True
        '
        'btnAddLine
        '
        Me.btnAddLine.Location = New System.Drawing.Point(413, 9)
        Me.btnAddLine.Name = "btnAddLine"
        Me.btnAddLine.Size = New System.Drawing.Size(95, 30)
        Me.btnAddLine.TabIndex = 0
        Me.btnAddLine.Text = "إضافة"
        Me.btnAddLine.UseVisualStyleBackColor = True
        '
        'grpLineDetails
        '
        Me.grpLineDetails.Controls.Add(Me.chkShowWhenZero)
        Me.grpLineDetails.Controls.Add(Me.chkIsVisible)
        Me.grpLineDetails.Controls.Add(Me.chkIsBold)
        Me.grpLineDetails.Controls.Add(Me.txtFormulaText)
        Me.grpLineDetails.Controls.Add(Me.lblFormulaText)
        Me.grpLineDetails.Controls.Add(Me.cboNormalBalanceSide)
        Me.grpLineDetails.Controls.Add(Me.lblNormalBalanceSide)
        Me.grpLineDetails.Controls.Add(Me.cboDisplaySignMode)
        Me.grpLineDetails.Controls.Add(Me.lblDisplaySignMode)
        Me.grpLineDetails.Controls.Add(Me.cboCalculationSign)
        Me.grpLineDetails.Controls.Add(Me.lblCalculationSign)
        Me.grpLineDetails.Controls.Add(Me.cboLineType)
        Me.grpLineDetails.Controls.Add(Me.lblLineType)
        Me.grpLineDetails.Controls.Add(Me.txtSortOrder)
        Me.grpLineDetails.Controls.Add(Me.lblSortOrder)
        Me.grpLineDetails.Controls.Add(Me.txtLineName)
        Me.grpLineDetails.Controls.Add(Me.lblLineName)
        Me.grpLineDetails.Controls.Add(Me.txtLineCode)
        Me.grpLineDetails.Controls.Add(Me.lblLineCode)
        Me.grpLineDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpLineDetails.Location = New System.Drawing.Point(0, 0)
        Me.grpLineDetails.Name = "grpLineDetails"
        Me.grpLineDetails.Padding = New System.Windows.Forms.Padding(10)
        Me.grpLineDetails.Size = New System.Drawing.Size(436, 602)
        Me.grpLineDetails.TabIndex = 0
        Me.grpLineDetails.TabStop = False
        Me.grpLineDetails.Text = "خصائص البند"
        '
        'chkShowWhenZero
        '
        Me.chkShowWhenZero.AutoSize = True
        Me.chkShowWhenZero.Location = New System.Drawing.Point(94, 387)
        Me.chkShowWhenZero.Name = "chkShowWhenZero"
        Me.chkShowWhenZero.Size = New System.Drawing.Size(114, 18)
        Me.chkShowWhenZero.TabIndex = 18
        Me.chkShowWhenZero.Text = "إظهار إذا كان صفر"
        Me.chkShowWhenZero.UseVisualStyleBackColor = True
        '
        'chkIsVisible
        '
        Me.chkIsVisible.AutoSize = True
        Me.chkIsVisible.Checked = True
        Me.chkIsVisible.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIsVisible.Location = New System.Drawing.Point(224, 387)
        Me.chkIsVisible.Name = "chkIsVisible"
        Me.chkIsVisible.Size = New System.Drawing.Size(51, 18)
        Me.chkIsVisible.TabIndex = 17
        Me.chkIsVisible.Text = "ظاهر"
        Me.chkIsVisible.UseVisualStyleBackColor = True
        '
        'chkIsBold
        '
        Me.chkIsBold.AutoSize = True
        Me.chkIsBold.Location = New System.Drawing.Point(1, 387)
        Me.chkIsBold.Name = "chkIsBold"
        Me.chkIsBold.Size = New System.Drawing.Size(75, 18)
        Me.chkIsBold.TabIndex = 16
        Me.chkIsBold.Text = "خط عريض"
        Me.chkIsBold.UseVisualStyleBackColor = True
        '
        'txtFormulaText
        '
        Me.txtFormulaText.Location = New System.Drawing.Point(4, 335)
        Me.txtFormulaText.Name = "txtFormulaText"
        Me.txtFormulaText.Size = New System.Drawing.Size(297, 22)
        Me.txtFormulaText.TabIndex = 15
        '
        'lblFormulaText
        '
        Me.lblFormulaText.AutoSize = True
        Me.lblFormulaText.Location = New System.Drawing.Point(307, 338)
        Me.lblFormulaText.Name = "lblFormulaText"
        Me.lblFormulaText.Size = New System.Drawing.Size(67, 14)
        Me.lblFormulaText.TabIndex = 14
        Me.lblFormulaText.Text = "نص المعادلة"
        '
        'cboNormalBalanceSide
        '
        Me.cboNormalBalanceSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNormalBalanceSide.FormattingEnabled = True
        Me.cboNormalBalanceSide.Location = New System.Drawing.Point(4, 294)
        Me.cboNormalBalanceSide.Name = "cboNormalBalanceSide"
        Me.cboNormalBalanceSide.Size = New System.Drawing.Size(297, 22)
        Me.cboNormalBalanceSide.TabIndex = 13
        '
        'lblNormalBalanceSide
        '
        Me.lblNormalBalanceSide.AutoSize = True
        Me.lblNormalBalanceSide.Location = New System.Drawing.Point(307, 297)
        Me.lblNormalBalanceSide.Name = "lblNormalBalanceSide"
        Me.lblNormalBalanceSide.Size = New System.Drawing.Size(81, 14)
        Me.lblNormalBalanceSide.TabIndex = 12
        Me.lblNormalBalanceSide.Text = "طبيعة الحساب"
        '
        'cboDisplaySignMode
        '
        Me.cboDisplaySignMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDisplaySignMode.FormattingEnabled = True
        Me.cboDisplaySignMode.Location = New System.Drawing.Point(4, 253)
        Me.cboDisplaySignMode.Name = "cboDisplaySignMode"
        Me.cboDisplaySignMode.Size = New System.Drawing.Size(297, 22)
        Me.cboDisplaySignMode.TabIndex = 11
        '
        'lblDisplaySignMode
        '
        Me.lblDisplaySignMode.AutoSize = True
        Me.lblDisplaySignMode.Location = New System.Drawing.Point(307, 256)
        Me.lblDisplaySignMode.Name = "lblDisplaySignMode"
        Me.lblDisplaySignMode.Size = New System.Drawing.Size(71, 14)
        Me.lblDisplaySignMode.TabIndex = 10
        Me.lblDisplaySignMode.Text = "طريقة العرض"
        '
        'cboCalculationSign
        '
        Me.cboCalculationSign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCalculationSign.FormattingEnabled = True
        Me.cboCalculationSign.Location = New System.Drawing.Point(4, 212)
        Me.cboCalculationSign.Name = "cboCalculationSign"
        Me.cboCalculationSign.Size = New System.Drawing.Size(297, 22)
        Me.cboCalculationSign.TabIndex = 9
        '
        'lblCalculationSign
        '
        Me.lblCalculationSign.AutoSize = True
        Me.lblCalculationSign.Location = New System.Drawing.Point(307, 215)
        Me.lblCalculationSign.Name = "lblCalculationSign"
        Me.lblCalculationSign.Size = New System.Drawing.Size(81, 14)
        Me.lblCalculationSign.TabIndex = 8
        Me.lblCalculationSign.Text = "إشارة الحساب"
        '
        'cboLineType
        '
        Me.cboLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLineType.FormattingEnabled = True
        Me.cboLineType.Location = New System.Drawing.Point(4, 171)
        Me.cboLineType.Name = "cboLineType"
        Me.cboLineType.Size = New System.Drawing.Size(297, 22)
        Me.cboLineType.TabIndex = 7
        '
        'lblLineType
        '
        Me.lblLineType.AutoSize = True
        Me.lblLineType.Location = New System.Drawing.Point(307, 174)
        Me.lblLineType.Name = "lblLineType"
        Me.lblLineType.Size = New System.Drawing.Size(47, 14)
        Me.lblLineType.TabIndex = 6
        Me.lblLineType.Text = "نوع البند"
        '
        'txtSortOrder
        '
        Me.txtSortOrder.Location = New System.Drawing.Point(4, 130)
        Me.txtSortOrder.Name = "txtSortOrder"
        Me.txtSortOrder.Size = New System.Drawing.Size(297, 22)
        Me.txtSortOrder.TabIndex = 5
        '
        'lblSortOrder
        '
        Me.lblSortOrder.AutoSize = True
        Me.lblSortOrder.Location = New System.Drawing.Point(307, 133)
        Me.lblSortOrder.Name = "lblSortOrder"
        Me.lblSortOrder.Size = New System.Drawing.Size(40, 14)
        Me.lblSortOrder.TabIndex = 4
        Me.lblSortOrder.Text = "الترتيب"
        '
        'txtLineName
        '
        Me.txtLineName.Location = New System.Drawing.Point(4, 89)
        Me.txtLineName.Name = "txtLineName"
        Me.txtLineName.Size = New System.Drawing.Size(297, 22)
        Me.txtLineName.TabIndex = 3
        '
        'lblLineName
        '
        Me.lblLineName.AutoSize = True
        Me.lblLineName.Location = New System.Drawing.Point(307, 92)
        Me.lblLineName.Name = "lblLineName"
        Me.lblLineName.Size = New System.Drawing.Size(55, 14)
        Me.lblLineName.TabIndex = 2
        Me.lblLineName.Text = "اسم البند"
        '
        'txtLineCode
        '
        Me.txtLineCode.Location = New System.Drawing.Point(4, 48)
        Me.txtLineCode.Name = "txtLineCode"
        Me.txtLineCode.Size = New System.Drawing.Size(297, 22)
        Me.txtLineCode.TabIndex = 1
        '
        'lblLineCode
        '
        Me.lblLineCode.AutoSize = True
        Me.lblLineCode.Location = New System.Drawing.Point(307, 51)
        Me.lblLineCode.Name = "lblLineCode"
        Me.lblLineCode.Size = New System.Drawing.Size(49, 14)
        Me.lblLineCode.TabIndex = 0
        Me.lblLineCode.Text = "كود البند"
        '
        'tabAccounts
        '
        Me.tabAccounts.Controls.Add(Me.splitAccounts)
        Me.tabAccounts.Location = New System.Drawing.Point(4, 23)
        Me.tabAccounts.Name = "tabAccounts"
        Me.tabAccounts.Padding = New System.Windows.Forms.Padding(6)
        Me.tabAccounts.Size = New System.Drawing.Size(1272, 614)
        Me.tabAccounts.TabIndex = 1
        Me.tabAccounts.Text = "ربط الحسابات"
        Me.tabAccounts.UseVisualStyleBackColor = True
        '
        'splitAccounts
        '
        Me.splitAccounts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitAccounts.Location = New System.Drawing.Point(6, 6)
        Me.splitAccounts.Name = "splitAccounts"
        '
        'splitAccounts.Panel1
        '
        Me.splitAccounts.Panel1.Controls.Add(Me.grpAccountLines)
        Me.splitAccounts.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        '
        'splitAccounts.Panel2
        '
        Me.splitAccounts.Panel2.Controls.Add(Me.grpLinkedAccounts)
        Me.splitAccounts.Panel2.Controls.Add(Me.pnlAccountButtons)
        Me.splitAccounts.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.splitAccounts.Size = New System.Drawing.Size(1260, 602)
        Me.splitAccounts.SplitterDistance = 520
        Me.splitAccounts.TabIndex = 0
        '
        'grpAccountLines
        '
        Me.grpAccountLines.Controls.Add(Me.dgvAccountLines)
        Me.grpAccountLines.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpAccountLines.Location = New System.Drawing.Point(0, 0)
        Me.grpAccountLines.Name = "grpAccountLines"
        Me.grpAccountLines.Padding = New System.Windows.Forms.Padding(8)
        Me.grpAccountLines.Size = New System.Drawing.Size(520, 602)
        Me.grpAccountLines.TabIndex = 0
        Me.grpAccountLines.TabStop = False
        Me.grpAccountLines.Text = "بنود الحسابات"
        '
        'dgvAccountLines
        '
        Me.dgvAccountLines.AllowUserToAddRows = False
        Me.dgvAccountLines.AllowUserToDeleteRows = False
        Me.dgvAccountLines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvAccountLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAccountLines.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAccountLines.Location = New System.Drawing.Point(8, 23)
        Me.dgvAccountLines.MultiSelect = False
        Me.dgvAccountLines.Name = "dgvAccountLines"
        Me.dgvAccountLines.ReadOnly = True
        Me.dgvAccountLines.RowHeadersVisible = False
        Me.dgvAccountLines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvAccountLines.Size = New System.Drawing.Size(504, 571)
        Me.dgvAccountLines.TabIndex = 0
        '
        'grpLinkedAccounts
        '
        Me.grpLinkedAccounts.Controls.Add(Me.dgvLinkedAccounts)
        Me.grpLinkedAccounts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpLinkedAccounts.Location = New System.Drawing.Point(0, 0)
        Me.grpLinkedAccounts.Name = "grpLinkedAccounts"
        Me.grpLinkedAccounts.Padding = New System.Windows.Forms.Padding(8)
        Me.grpLinkedAccounts.Size = New System.Drawing.Size(736, 554)
        Me.grpLinkedAccounts.TabIndex = 1
        Me.grpLinkedAccounts.TabStop = False
        Me.grpLinkedAccounts.Text = "الحسابات المرتبطة"
        '
        'dgvLinkedAccounts
        '
        Me.dgvLinkedAccounts.AllowUserToAddRows = False
        Me.dgvLinkedAccounts.AllowUserToDeleteRows = False
        Me.dgvLinkedAccounts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvLinkedAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLinkedAccounts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLinkedAccounts.Location = New System.Drawing.Point(8, 23)
        Me.dgvLinkedAccounts.MultiSelect = False
        Me.dgvLinkedAccounts.Name = "dgvLinkedAccounts"
        Me.dgvLinkedAccounts.ReadOnly = True
        Me.dgvLinkedAccounts.RowHeadersVisible = False
        Me.dgvLinkedAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLinkedAccounts.Size = New System.Drawing.Size(720, 523)
        Me.dgvLinkedAccounts.TabIndex = 0
        '
        'pnlAccountButtons
        '
        Me.pnlAccountButtons.Controls.Add(Me.btnUpdateAccountLink)
        Me.pnlAccountButtons.Controls.Add(Me.btnUnlinkAccount)
        Me.pnlAccountButtons.Controls.Add(Me.btnLinkAccount)
        Me.pnlAccountButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlAccountButtons.Location = New System.Drawing.Point(0, 554)
        Me.pnlAccountButtons.Name = "pnlAccountButtons"
        Me.pnlAccountButtons.Padding = New System.Windows.Forms.Padding(6)
        Me.pnlAccountButtons.Size = New System.Drawing.Size(736, 48)
        Me.pnlAccountButtons.TabIndex = 0
        '
        'btnUpdateAccountLink
        '
        Me.btnUpdateAccountLink.Location = New System.Drawing.Point(9, 9)
        Me.btnUpdateAccountLink.Name = "btnUpdateAccountLink"
        Me.btnUpdateAccountLink.Size = New System.Drawing.Size(110, 30)
        Me.btnUpdateAccountLink.TabIndex = 2
        Me.btnUpdateAccountLink.Text = "تعديل الربط"
        Me.btnUpdateAccountLink.UseVisualStyleBackColor = True
        '
        'btnUnlinkAccount
        '
        Me.btnUnlinkAccount.Location = New System.Drawing.Point(125, 9)
        Me.btnUnlinkAccount.Name = "btnUnlinkAccount"
        Me.btnUnlinkAccount.Size = New System.Drawing.Size(110, 30)
        Me.btnUnlinkAccount.TabIndex = 1
        Me.btnUnlinkAccount.Text = "إلغاء الربط"
        Me.btnUnlinkAccount.UseVisualStyleBackColor = True
        '
        'btnLinkAccount
        '
        Me.btnLinkAccount.Location = New System.Drawing.Point(241, 9)
        Me.btnLinkAccount.Name = "btnLinkAccount"
        Me.btnLinkAccount.Size = New System.Drawing.Size(110, 30)
        Me.btnLinkAccount.TabIndex = 0
        Me.btnLinkAccount.Text = "ربط حساب"
        Me.btnLinkAccount.UseVisualStyleBackColor = True
        '
        'tabFormula
        '
        Me.tabFormula.Controls.Add(Me.splitFormula)
        Me.tabFormula.Location = New System.Drawing.Point(4, 23)
        Me.tabFormula.Name = "tabFormula"
        Me.tabFormula.Padding = New System.Windows.Forms.Padding(6)
        Me.tabFormula.Size = New System.Drawing.Size(1272, 614)
        Me.tabFormula.TabIndex = 2
        Me.tabFormula.Text = "المعادلات"
        Me.tabFormula.UseVisualStyleBackColor = True
        '
        'splitFormula
        '
        Me.splitFormula.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitFormula.Location = New System.Drawing.Point(6, 6)
        Me.splitFormula.Name = "splitFormula"
        '
        'splitFormula.Panel1
        '
        Me.splitFormula.Panel1.Controls.Add(Me.grpFormulaLines)
        Me.splitFormula.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        '
        'splitFormula.Panel2
        '
        Me.splitFormula.Panel2.Controls.Add(Me.grpFormulaDetails)
        Me.splitFormula.Panel2.Controls.Add(Me.pnlFormulaButtons)
        Me.splitFormula.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.splitFormula.Size = New System.Drawing.Size(1260, 602)
        Me.splitFormula.SplitterDistance = 520
        Me.splitFormula.TabIndex = 0
        '
        'grpFormulaLines
        '
        Me.grpFormulaLines.Controls.Add(Me.dgvFormulaLines)
        Me.grpFormulaLines.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpFormulaLines.Location = New System.Drawing.Point(0, 0)
        Me.grpFormulaLines.Name = "grpFormulaLines"
        Me.grpFormulaLines.Padding = New System.Windows.Forms.Padding(8)
        Me.grpFormulaLines.Size = New System.Drawing.Size(520, 602)
        Me.grpFormulaLines.TabIndex = 0
        Me.grpFormulaLines.TabStop = False
        Me.grpFormulaLines.Text = "بنود المعادلات"
        '
        'dgvFormulaLines
        '
        Me.dgvFormulaLines.AllowUserToAddRows = False
        Me.dgvFormulaLines.AllowUserToDeleteRows = False
        Me.dgvFormulaLines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvFormulaLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFormulaLines.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvFormulaLines.Location = New System.Drawing.Point(8, 23)
        Me.dgvFormulaLines.MultiSelect = False
        Me.dgvFormulaLines.Name = "dgvFormulaLines"
        Me.dgvFormulaLines.ReadOnly = True
        Me.dgvFormulaLines.RowHeadersVisible = False
        Me.dgvFormulaLines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvFormulaLines.Size = New System.Drawing.Size(504, 571)
        Me.dgvFormulaLines.TabIndex = 0
        '
        'grpFormulaDetails
        '
        Me.grpFormulaDetails.Controls.Add(Me.dgvFormulaDetails)
        Me.grpFormulaDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpFormulaDetails.Location = New System.Drawing.Point(0, 0)
        Me.grpFormulaDetails.Name = "grpFormulaDetails"
        Me.grpFormulaDetails.Padding = New System.Windows.Forms.Padding(8)
        Me.grpFormulaDetails.Size = New System.Drawing.Size(736, 554)
        Me.grpFormulaDetails.TabIndex = 1
        Me.grpFormulaDetails.TabStop = False
        Me.grpFormulaDetails.Text = "تفاصيل المعادلة"
        '
        'dgvFormulaDetails
        '
        Me.dgvFormulaDetails.AllowUserToAddRows = False
        Me.dgvFormulaDetails.AllowUserToDeleteRows = False
        Me.dgvFormulaDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvFormulaDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFormulaDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvFormulaDetails.Location = New System.Drawing.Point(8, 23)
        Me.dgvFormulaDetails.MultiSelect = False
        Me.dgvFormulaDetails.Name = "dgvFormulaDetails"
        Me.dgvFormulaDetails.ReadOnly = True
        Me.dgvFormulaDetails.RowHeadersVisible = False
        Me.dgvFormulaDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvFormulaDetails.Size = New System.Drawing.Size(720, 523)
        Me.dgvFormulaDetails.TabIndex = 0
        '
        'pnlFormulaButtons
        '
        Me.pnlFormulaButtons.Controls.Add(Me.btnRebuildFormula)
        Me.pnlFormulaButtons.Controls.Add(Me.btnRefreshFormula)
        Me.pnlFormulaButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFormulaButtons.Location = New System.Drawing.Point(0, 554)
        Me.pnlFormulaButtons.Name = "pnlFormulaButtons"
        Me.pnlFormulaButtons.Padding = New System.Windows.Forms.Padding(6)
        Me.pnlFormulaButtons.Size = New System.Drawing.Size(736, 48)
        Me.pnlFormulaButtons.TabIndex = 0
        '
        'btnRebuildFormula
        '
        Me.btnRebuildFormula.Location = New System.Drawing.Point(9, 9)
        Me.btnRebuildFormula.Name = "btnRebuildFormula"
        Me.btnRebuildFormula.Size = New System.Drawing.Size(130, 30)
        Me.btnRebuildFormula.TabIndex = 1
        Me.btnRebuildFormula.Text = "تعديل المعادلة"
        Me.btnRebuildFormula.UseVisualStyleBackColor = True
        '
        'btnRefreshFormula
        '
        Me.btnRefreshFormula.Location = New System.Drawing.Point(145, 9)
        Me.btnRefreshFormula.Name = "btnRefreshFormula"
        Me.btnRefreshFormula.Size = New System.Drawing.Size(110, 30)
        Me.btnRefreshFormula.TabIndex = 0
        Me.btnRefreshFormula.Text = "تحديث"
        Me.btnRefreshFormula.UseVisualStyleBackColor = True
        '
        'tabPreview
        '
        Me.tabPreview.Controls.Add(Me.grpPreview)
        Me.tabPreview.Controls.Add(Me.pnlPreviewButtons)
        Me.tabPreview.Location = New System.Drawing.Point(4, 23)
        Me.tabPreview.Name = "tabPreview"
        Me.tabPreview.Padding = New System.Windows.Forms.Padding(6)
        Me.tabPreview.Size = New System.Drawing.Size(1272, 614)
        Me.tabPreview.TabIndex = 3
        Me.tabPreview.Text = "المعاينة"
        Me.tabPreview.UseVisualStyleBackColor = True
        '
        'grpPreview
        '
        Me.grpPreview.Controls.Add(Me.dgvPreview)
        Me.grpPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpPreview.Location = New System.Drawing.Point(6, 6)
        Me.grpPreview.Name = "grpPreview"
        Me.grpPreview.Padding = New System.Windows.Forms.Padding(8)
        Me.grpPreview.Size = New System.Drawing.Size(1260, 554)
        Me.grpPreview.TabIndex = 1
        Me.grpPreview.TabStop = False
        Me.grpPreview.Text = "معاينة قائمة الدخل"
        '
        'dgvPreview
        '
        Me.dgvPreview.AllowUserToAddRows = False
        Me.dgvPreview.AllowUserToDeleteRows = False
        Me.dgvPreview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPreview.Location = New System.Drawing.Point(8, 23)
        Me.dgvPreview.MultiSelect = False
        Me.dgvPreview.Name = "dgvPreview"
        Me.dgvPreview.ReadOnly = True
        Me.dgvPreview.RowHeadersVisible = False
        Me.dgvPreview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPreview.Size = New System.Drawing.Size(1244, 523)
        Me.dgvPreview.TabIndex = 0
        '
        'pnlPreviewButtons
        '
        Me.pnlPreviewButtons.Controls.Add(Me.chkHideZero)
        Me.pnlPreviewButtons.Controls.Add(Me.btnLoadPreview)
        Me.pnlPreviewButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlPreviewButtons.Location = New System.Drawing.Point(6, 560)
        Me.pnlPreviewButtons.Name = "pnlPreviewButtons"
        Me.pnlPreviewButtons.Padding = New System.Windows.Forms.Padding(6)
        Me.pnlPreviewButtons.Size = New System.Drawing.Size(1260, 48)
        Me.pnlPreviewButtons.TabIndex = 0
        '
        'chkHideZero
        '
        Me.chkHideZero.AutoSize = True
        Me.chkHideZero.Location = New System.Drawing.Point(126, 15)
        Me.chkHideZero.Name = "chkHideZero"
        Me.chkHideZero.Size = New System.Drawing.Size(89, 18)
        Me.chkHideZero.TabIndex = 1
        Me.chkHideZero.Text = "إخفاء الأصفار"
        Me.chkHideZero.UseVisualStyleBackColor = True
        '
        'btnLoadPreview
        '
        Me.btnLoadPreview.Location = New System.Drawing.Point(9, 9)
        Me.btnLoadPreview.Name = "btnLoadPreview"
        Me.btnLoadPreview.Size = New System.Drawing.Size(110, 30)
        Me.btnLoadPreview.TabIndex = 0
        Me.btnLoadPreview.Text = "تحميل المعاينة"
        Me.btnLoadPreview.UseVisualStyleBackColor = True
        '
        'tabValidation
        '
        Me.tabValidation.Controls.Add(Me.grpValidation)
        Me.tabValidation.Controls.Add(Me.pnlValidationButtons)
        Me.tabValidation.Location = New System.Drawing.Point(4, 23)
        Me.tabValidation.Name = "tabValidation"
        Me.tabValidation.Padding = New System.Windows.Forms.Padding(6)
        Me.tabValidation.Size = New System.Drawing.Size(1272, 614)
        Me.tabValidation.TabIndex = 4
        Me.tabValidation.Text = "الفحص والتنبيهات"
        Me.tabValidation.UseVisualStyleBackColor = True
        '
        'grpValidation
        '
        Me.grpValidation.Controls.Add(Me.dgvValidation)
        Me.grpValidation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpValidation.Location = New System.Drawing.Point(6, 6)
        Me.grpValidation.Name = "grpValidation"
        Me.grpValidation.Padding = New System.Windows.Forms.Padding(8)
        Me.grpValidation.Size = New System.Drawing.Size(1260, 554)
        Me.grpValidation.TabIndex = 1
        Me.grpValidation.TabStop = False
        Me.grpValidation.Text = "نتائج الفحص"
        '
        'dgvValidation
        '
        Me.dgvValidation.AllowUserToAddRows = False
        Me.dgvValidation.AllowUserToDeleteRows = False
        Me.dgvValidation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvValidation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvValidation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvValidation.Location = New System.Drawing.Point(8, 23)
        Me.dgvValidation.MultiSelect = False
        Me.dgvValidation.Name = "dgvValidation"
        Me.dgvValidation.ReadOnly = True
        Me.dgvValidation.RowHeadersVisible = False
        Me.dgvValidation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvValidation.Size = New System.Drawing.Size(1244, 523)
        Me.dgvValidation.TabIndex = 0
        '
        'pnlValidationButtons
        '
        Me.pnlValidationButtons.Controls.Add(Me.btnLoadValidation)
        Me.pnlValidationButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlValidationButtons.Location = New System.Drawing.Point(6, 560)
        Me.pnlValidationButtons.Name = "pnlValidationButtons"
        Me.pnlValidationButtons.Padding = New System.Windows.Forms.Padding(6)
        Me.pnlValidationButtons.Size = New System.Drawing.Size(1260, 48)
        Me.pnlValidationButtons.TabIndex = 0
        '
        'btnLoadValidation
        '
        Me.btnLoadValidation.Location = New System.Drawing.Point(9, 9)
        Me.btnLoadValidation.Name = "btnLoadValidation"
        Me.btnLoadValidation.Size = New System.Drawing.Size(110, 30)
        Me.btnLoadValidation.TabIndex = 0
        Me.btnLoadValidation.Text = "تشغيل الفحص"
        Me.btnLoadValidation.UseVisualStyleBackColor = True
        '
        'statusStrip1
        '
        Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.statusStrip1.Location = New System.Drawing.Point(0, 699)
        Me.statusStrip1.Name = "statusStrip1"
        Me.statusStrip1.Size = New System.Drawing.Size(1220, 22)
        Me.statusStrip1.TabIndex = 2
        Me.statusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(30, 17)
        Me.lblStatus.Text = "جاهز"
        '
        'FrmIncomeStatementDesigner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1220, 700)
        Me.Controls.Add(Me.tabMain)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.statusStrip1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.MinimumSize = New System.Drawing.Size(1160, 660)
        Me.Name = "FrmIncomeStatementDesigner"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "مصمم قائمة الدخل"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tabMain.ResumeLayout(False)
        Me.tabLines.ResumeLayout(False)
        Me.splitLines.Panel1.ResumeLayout(False)
        Me.splitLines.Panel2.ResumeLayout(False)
        CType(Me.splitLines, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitLines.ResumeLayout(False)
        Me.grpLines.ResumeLayout(False)
        CType(Me.dgvLines, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLineButtons.ResumeLayout(False)
        Me.grpLineDetails.ResumeLayout(False)
        Me.grpLineDetails.PerformLayout()
        Me.tabAccounts.ResumeLayout(False)
        Me.splitAccounts.Panel1.ResumeLayout(False)
        Me.splitAccounts.Panel2.ResumeLayout(False)
        CType(Me.splitAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitAccounts.ResumeLayout(False)
        Me.grpAccountLines.ResumeLayout(False)
        CType(Me.dgvAccountLines, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpLinkedAccounts.ResumeLayout(False)
        CType(Me.dgvLinkedAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAccountButtons.ResumeLayout(False)
        Me.tabFormula.ResumeLayout(False)
        Me.splitFormula.Panel1.ResumeLayout(False)
        Me.splitFormula.Panel2.ResumeLayout(False)
        CType(Me.splitFormula, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitFormula.ResumeLayout(False)
        Me.grpFormulaLines.ResumeLayout(False)
        CType(Me.dgvFormulaLines, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFormulaDetails.ResumeLayout(False)
        CType(Me.dgvFormulaDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFormulaButtons.ResumeLayout(False)
        Me.tabPreview.ResumeLayout(False)
        Me.grpPreview.ResumeLayout(False)
        CType(Me.dgvPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPreviewButtons.ResumeLayout(False)
        Me.pnlPreviewButtons.PerformLayout()
        Me.tabValidation.ResumeLayout(False)
        Me.grpValidation.ResumeLayout(False)
        CType(Me.dgvValidation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlValidationButtons.ResumeLayout(False)
        Me.statusStrip1.ResumeLayout(False)
        Me.statusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlTop As Panel
    Friend WithEvents btnClose As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnValidate As Button
    Friend WithEvents btnPreview As Button
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents lblDateTo As Label
    Friend WithEvents dtpDateFrom As DateTimePicker
    Friend WithEvents lblDateFrom As Label
    Friend WithEvents btnCreateDefaultTemplate As Button
    Friend WithEvents cboTemplates As ComboBox
    Friend WithEvents lblTemplate As Label
    Friend WithEvents tabMain As TabControl
    Friend WithEvents tabLines As TabPage
    Friend WithEvents splitLines As SplitContainer
    Friend WithEvents grpLines As GroupBox
    Friend WithEvents dgvLines As DataGridView
    Friend WithEvents pnlLineButtons As Panel
    Friend WithEvents btnLineDown As Button
    Friend WithEvents btnLineUp As Button
    Friend WithEvents btnDisableLine As Button
    Friend WithEvents btnEditLine As Button
    Friend WithEvents btnAddLine As Button
    Friend WithEvents grpLineDetails As GroupBox
    Friend WithEvents chkShowWhenZero As CheckBox
    Friend WithEvents chkIsVisible As CheckBox
    Friend WithEvents chkIsBold As CheckBox
    Friend WithEvents txtFormulaText As TextBox
    Friend WithEvents lblFormulaText As Label
    Friend WithEvents cboNormalBalanceSide As ComboBox
    Friend WithEvents lblNormalBalanceSide As Label
    Friend WithEvents cboDisplaySignMode As ComboBox
    Friend WithEvents lblDisplaySignMode As Label
    Friend WithEvents cboCalculationSign As ComboBox
    Friend WithEvents lblCalculationSign As Label
    Friend WithEvents cboLineType As ComboBox
    Friend WithEvents lblLineType As Label
    Friend WithEvents txtSortOrder As TextBox
    Friend WithEvents lblSortOrder As Label
    Friend WithEvents txtLineName As TextBox
    Friend WithEvents lblLineName As Label
    Friend WithEvents txtLineCode As TextBox
    Friend WithEvents lblLineCode As Label
    Friend WithEvents tabAccounts As TabPage
    Friend WithEvents splitAccounts As SplitContainer
    Friend WithEvents grpAccountLines As GroupBox
    Friend WithEvents dgvAccountLines As DataGridView
    Friend WithEvents grpLinkedAccounts As GroupBox
    Friend WithEvents dgvLinkedAccounts As DataGridView
    Friend WithEvents pnlAccountButtons As Panel
    Friend WithEvents btnUpdateAccountLink As Button
    Friend WithEvents btnUnlinkAccount As Button
    Friend WithEvents btnLinkAccount As Button
    Friend WithEvents tabFormula As TabPage
    Friend WithEvents splitFormula As SplitContainer
    Friend WithEvents grpFormulaLines As GroupBox
    Friend WithEvents dgvFormulaLines As DataGridView
    Friend WithEvents grpFormulaDetails As GroupBox
    Friend WithEvents dgvFormulaDetails As DataGridView
    Friend WithEvents pnlFormulaButtons As Panel
    Friend WithEvents btnRebuildFormula As Button
    Friend WithEvents btnRefreshFormula As Button
    Friend WithEvents tabPreview As TabPage
    Friend WithEvents grpPreview As GroupBox
    Friend WithEvents dgvPreview As DataGridView
    Friend WithEvents pnlPreviewButtons As Panel
    Friend WithEvents chkHideZero As CheckBox
    Friend WithEvents btnLoadPreview As Button
    Friend WithEvents tabValidation As TabPage
    Friend WithEvents grpValidation As GroupBox
    Friend WithEvents dgvValidation As DataGridView
    Friend WithEvents pnlValidationButtons As Panel
    Friend WithEvents btnLoadValidation As Button
    Friend WithEvents statusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel
    Friend WithEvents btnFinalReport As Button
End Class
