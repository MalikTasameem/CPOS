<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSalesPrintLayoutManager
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
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.grpTemplates = New System.Windows.Forms.GroupBox()
        Me.btnApplyTemplate = New System.Windows.Forms.Button()
        Me.pnlTemplatePreview = New System.Windows.Forms.Panel()
        Me.lstTemplates = New System.Windows.Forms.ListBox()
        Me.dgvColumns = New System.Windows.Forms.DataGridView()
        Me.dgvStyles = New System.Windows.Forms.DataGridView()
        Me.dgvSections = New System.Windows.Forms.DataGridView()
        Me.grpOptions = New System.Windows.Forms.GroupBox()
        Me.numLogoHeight = New System.Windows.Forms.NumericUpDown()
        Me.numLogoWidth = New System.Windows.Forms.NumericUpDown()
        Me.lblLogoHeight = New System.Windows.Forms.Label()
        Me.lblLogoWidth = New System.Windows.Forms.Label()
        Me.cmbFontFamily = New System.Windows.Forms.ComboBox()
        Me.lblFontFamily = New System.Windows.Forms.Label()
        Me.chkLandscape = New System.Windows.Forms.CheckBox()
        Me.numMarginBottom = New System.Windows.Forms.NumericUpDown()
        Me.numMarginTop = New System.Windows.Forms.NumericUpDown()
        Me.numMarginRight = New System.Windows.Forms.NumericUpDown()
        Me.numMarginLeft = New System.Windows.Forms.NumericUpDown()
        Me.lblMarginBottom = New System.Windows.Forms.Label()
        Me.lblMarginTop = New System.Windows.Forms.Label()
        Me.lblMarginRight = New System.Windows.Forms.Label()
        Me.lblMarginLeft = New System.Windows.Forms.Label()
        Me.cmbPrinter = New System.Windows.Forms.ComboBox()
        Me.lblPrinter = New System.Windows.Forms.Label()
        Me.cmbPaperKind = New System.Windows.Forms.ComboBox()
        Me.lblPaper = New System.Windows.Forms.Label()
        Me.txtProfileName = New System.Windows.Forms.TextBox()
        Me.lblProfileName = New System.Windows.Forms.Label()
        Me.cmbProfiles = New System.Windows.Forms.ComboBox()
        Me.lblProfiles = New System.Windows.Forms.Label()
        Me.lblSections = New System.Windows.Forms.Label()
        Me.lblStyles = New System.Windows.Forms.Label()
        Me.lblColumns = New System.Windows.Forms.Label()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnDefault = New System.Windows.Forms.Button()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.grpTemplates.SuspendLayout()
        CType(Me.dgvColumns, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvStyles, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvSections, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpOptions.SuspendLayout()
        CType(Me.numLogoHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numLogoWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMarginBottom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMarginTop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMarginRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numMarginLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1260, 42)
        Me.pnlHeader.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(870, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(390, 42)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "إدارة التقرير الديناميكي لفاتورة المبيعات"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClose
        '
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(0, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(48, 42)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "X"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.grpTemplates)
        Me.pnlMain.Controls.Add(Me.dgvColumns)
        Me.pnlMain.Controls.Add(Me.dgvStyles)
        Me.pnlMain.Controls.Add(Me.dgvSections)
        Me.pnlMain.Controls.Add(Me.grpOptions)
        Me.pnlMain.Controls.Add(Me.lblSections)
        Me.pnlMain.Controls.Add(Me.lblStyles)
        Me.pnlMain.Controls.Add(Me.lblColumns)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 42)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(12)
        Me.pnlMain.Size = New System.Drawing.Size(1260, 620)
        Me.pnlMain.TabIndex = 1
        '
        'grpTemplates
        '
        Me.grpTemplates.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpTemplates.Controls.Add(Me.btnApplyTemplate)
        Me.grpTemplates.Controls.Add(Me.pnlTemplatePreview)
        Me.grpTemplates.Controls.Add(Me.lstTemplates)
        Me.grpTemplates.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.grpTemplates.Location = New System.Drawing.Point(12, 15)
        Me.grpTemplates.Name = "grpTemplates"
        Me.grpTemplates.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.grpTemplates.Size = New System.Drawing.Size(220, 593)
        Me.grpTemplates.TabIndex = 7
        Me.grpTemplates.TabStop = False
        Me.grpTemplates.Text = "قوالب جاهزة مؤقتة"
        '
        'btnApplyTemplate
        '
        Me.btnApplyTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnApplyTemplate.BackColor = System.Drawing.Color.White
        Me.btnApplyTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnApplyTemplate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnApplyTemplate.Location = New System.Drawing.Point(12, 546)
        Me.btnApplyTemplate.Name = "btnApplyTemplate"
        Me.btnApplyTemplate.Size = New System.Drawing.Size(196, 34)
        Me.btnApplyTemplate.TabIndex = 2
        Me.btnApplyTemplate.Text = "تطبيق القالب"
        Me.btnApplyTemplate.UseVisualStyleBackColor = False
        '
        'pnlTemplatePreview
        '
        Me.pnlTemplatePreview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTemplatePreview.BackColor = System.Drawing.Color.White
        Me.pnlTemplatePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTemplatePreview.Location = New System.Drawing.Point(12, 164)
        Me.pnlTemplatePreview.Name = "pnlTemplatePreview"
        Me.pnlTemplatePreview.Size = New System.Drawing.Size(196, 369)
        Me.pnlTemplatePreview.TabIndex = 1
        '
        'lstTemplates
        '
        Me.lstTemplates.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstTemplates.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lstTemplates.FormattingEnabled = True
        Me.lstTemplates.ItemHeight = 17
        Me.lstTemplates.Location = New System.Drawing.Point(12, 27)
        Me.lstTemplates.Name = "lstTemplates"
        Me.lstTemplates.Size = New System.Drawing.Size(196, 123)
        Me.lstTemplates.TabIndex = 0
        '
        'dgvColumns
        '
        Me.dgvColumns.AllowUserToAddRows = False
        Me.dgvColumns.AllowUserToDeleteRows = False
        Me.dgvColumns.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvColumns.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvColumns.BackgroundColor = System.Drawing.Color.White
        Me.dgvColumns.ColumnHeadersHeight = 30
        Me.dgvColumns.Location = New System.Drawing.Point(244, 436)
        Me.dgvColumns.Name = "dgvColumns"
        Me.dgvColumns.RowHeadersVisible = False
        Me.dgvColumns.RowTemplate.Height = 28
        Me.dgvColumns.Size = New System.Drawing.Size(610, 172)
        Me.dgvColumns.TabIndex = 4
        '
        'dgvStyles
        '
        Me.dgvStyles.AllowUserToAddRows = False
        Me.dgvStyles.AllowUserToDeleteRows = False
        Me.dgvStyles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvStyles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvStyles.BackgroundColor = System.Drawing.Color.White
        Me.dgvStyles.ColumnHeadersHeight = 30
        Me.dgvStyles.Location = New System.Drawing.Point(244, 252)
        Me.dgvStyles.Name = "dgvStyles"
        Me.dgvStyles.RowHeadersVisible = False
        Me.dgvStyles.RowTemplate.Height = 28
        Me.dgvStyles.Size = New System.Drawing.Size(610, 150)
        Me.dgvStyles.TabIndex = 6
        '
        'dgvSections
        '
        Me.dgvSections.AllowUserToAddRows = False
        Me.dgvSections.AllowUserToDeleteRows = False
        Me.dgvSections.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvSections.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvSections.BackgroundColor = System.Drawing.Color.White
        Me.dgvSections.ColumnHeadersHeight = 30
        Me.dgvSections.Location = New System.Drawing.Point(244, 33)
        Me.dgvSections.Name = "dgvSections"
        Me.dgvSections.RowHeadersVisible = False
        Me.dgvSections.RowTemplate.Height = 28
        Me.dgvSections.Size = New System.Drawing.Size(610, 176)
        Me.dgvSections.TabIndex = 2
        '
        'grpOptions
        '
        Me.grpOptions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right) _
            Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.grpOptions.Controls.Add(Me.numLogoHeight)
        Me.grpOptions.Controls.Add(Me.numLogoWidth)
        Me.grpOptions.Controls.Add(Me.lblLogoHeight)
        Me.grpOptions.Controls.Add(Me.lblLogoWidth)
        Me.grpOptions.Controls.Add(Me.cmbFontFamily)
        Me.grpOptions.Controls.Add(Me.lblFontFamily)
        Me.grpOptions.Controls.Add(Me.chkLandscape)
        Me.grpOptions.Controls.Add(Me.numMarginBottom)
        Me.grpOptions.Controls.Add(Me.numMarginTop)
        Me.grpOptions.Controls.Add(Me.numMarginRight)
        Me.grpOptions.Controls.Add(Me.numMarginLeft)
        Me.grpOptions.Controls.Add(Me.lblMarginBottom)
        Me.grpOptions.Controls.Add(Me.lblMarginTop)
        Me.grpOptions.Controls.Add(Me.lblMarginRight)
        Me.grpOptions.Controls.Add(Me.lblMarginLeft)
        Me.grpOptions.Controls.Add(Me.cmbPrinter)
        Me.grpOptions.Controls.Add(Me.lblPrinter)
        Me.grpOptions.Controls.Add(Me.cmbPaperKind)
        Me.grpOptions.Controls.Add(Me.lblPaper)
        Me.grpOptions.Controls.Add(Me.txtProfileName)
        Me.grpOptions.Controls.Add(Me.lblProfileName)
        Me.grpOptions.Controls.Add(Me.cmbProfiles)
        Me.grpOptions.Controls.Add(Me.lblProfiles)
        Me.grpOptions.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.grpOptions.Location = New System.Drawing.Point(866, 15)
        Me.grpOptions.Name = "grpOptions"
        Me.grpOptions.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.grpOptions.Size = New System.Drawing.Size(382, 593)
        Me.grpOptions.TabIndex = 0
        Me.grpOptions.TabStop = False
        Me.grpOptions.Text = "الإعدادات العامة"
        '
        'numLogoHeight
        '
        Me.numLogoHeight.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.numLogoHeight.Location = New System.Drawing.Point(20, 218)
        Me.numLogoHeight.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.numLogoHeight.Minimum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.numLogoHeight.Name = "numLogoHeight"
        Me.numLogoHeight.Size = New System.Drawing.Size(120, 25)
        Me.numLogoHeight.TabIndex = 22
        Me.numLogoHeight.Value = New Decimal(New Integer() {72, 0, 0, 0})
        '
        'numLogoWidth
        '
        Me.numLogoWidth.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.numLogoWidth.Location = New System.Drawing.Point(20, 181)
        Me.numLogoWidth.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.numLogoWidth.Minimum = New Decimal(New Integer() {20, 0, 0, 0})
        Me.numLogoWidth.Name = "numLogoWidth"
        Me.numLogoWidth.Size = New System.Drawing.Size(120, 25)
        Me.numLogoWidth.TabIndex = 20
        Me.numLogoWidth.Value = New Decimal(New Integer() {72, 0, 0, 0})
        '
        'lblLogoHeight
        '
        Me.lblLogoHeight.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblLogoHeight.Location = New System.Drawing.Point(146, 216)
        Me.lblLogoHeight.Name = "lblLogoHeight"
        Me.lblLogoHeight.Size = New System.Drawing.Size(220, 26)
        Me.lblLogoHeight.TabIndex = 21
        Me.lblLogoHeight.Text = "طول الشعار"
        Me.lblLogoHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLogoWidth
        '
        Me.lblLogoWidth.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblLogoWidth.Location = New System.Drawing.Point(146, 179)
        Me.lblLogoWidth.Name = "lblLogoWidth"
        Me.lblLogoWidth.Size = New System.Drawing.Size(220, 26)
        Me.lblLogoWidth.TabIndex = 19
        Me.lblLogoWidth.Text = "عرض الشعار"
        Me.lblLogoWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbFontFamily
        '
        Me.cmbFontFamily.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFontFamily.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbFontFamily.FormattingEnabled = True
        Me.cmbFontFamily.Location = New System.Drawing.Point(20, 142)
        Me.cmbFontFamily.Name = "cmbFontFamily"
        Me.cmbFontFamily.Size = New System.Drawing.Size(220, 25)
        Me.cmbFontFamily.TabIndex = 18
        '
        'lblFontFamily
        '
        Me.lblFontFamily.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblFontFamily.Location = New System.Drawing.Point(246, 140)
        Me.lblFontFamily.Name = "lblFontFamily"
        Me.lblFontFamily.Size = New System.Drawing.Size(120, 26)
        Me.lblFontFamily.TabIndex = 17
        Me.lblFontFamily.Text = "نوع الخط"
        Me.lblFontFamily.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkLandscape
        '
        Me.chkLandscape.AutoSize = True
        Me.chkLandscape.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.chkLandscape.Location = New System.Drawing.Point(224, 412)
        Me.chkLandscape.Name = "chkLandscape"
        Me.chkLandscape.Size = New System.Drawing.Size(142, 23)
        Me.chkLandscape.TabIndex = 16
        Me.chkLandscape.Text = "طباعة بالعرض"
        Me.chkLandscape.UseVisualStyleBackColor = True
        '
        'numMarginBottom
        '
        Me.numMarginBottom.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.numMarginBottom.Location = New System.Drawing.Point(20, 366)
        Me.numMarginBottom.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.numMarginBottom.Name = "numMarginBottom"
        Me.numMarginBottom.Size = New System.Drawing.Size(120, 25)
        Me.numMarginBottom.TabIndex = 15
        '
        'numMarginTop
        '
        Me.numMarginTop.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.numMarginTop.Location = New System.Drawing.Point(20, 329)
        Me.numMarginTop.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.numMarginTop.Name = "numMarginTop"
        Me.numMarginTop.Size = New System.Drawing.Size(120, 25)
        Me.numMarginTop.TabIndex = 13
        '
        'numMarginRight
        '
        Me.numMarginRight.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.numMarginRight.Location = New System.Drawing.Point(20, 292)
        Me.numMarginRight.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.numMarginRight.Name = "numMarginRight"
        Me.numMarginRight.Size = New System.Drawing.Size(120, 25)
        Me.numMarginRight.TabIndex = 11
        '
        'numMarginLeft
        '
        Me.numMarginLeft.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.numMarginLeft.Location = New System.Drawing.Point(20, 255)
        Me.numMarginLeft.Maximum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.numMarginLeft.Name = "numMarginLeft"
        Me.numMarginLeft.Size = New System.Drawing.Size(120, 25)
        Me.numMarginLeft.TabIndex = 9
        '
        'lblMarginBottom
        '
        Me.lblMarginBottom.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblMarginBottom.Location = New System.Drawing.Point(146, 364)
        Me.lblMarginBottom.Name = "lblMarginBottom"
        Me.lblMarginBottom.Size = New System.Drawing.Size(220, 26)
        Me.lblMarginBottom.TabIndex = 14
        Me.lblMarginBottom.Text = "الهامش السفلي"
        Me.lblMarginBottom.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMarginTop
        '
        Me.lblMarginTop.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblMarginTop.Location = New System.Drawing.Point(146, 327)
        Me.lblMarginTop.Name = "lblMarginTop"
        Me.lblMarginTop.Size = New System.Drawing.Size(220, 26)
        Me.lblMarginTop.TabIndex = 12
        Me.lblMarginTop.Text = "الهامش العلوي"
        Me.lblMarginTop.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMarginRight
        '
        Me.lblMarginRight.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblMarginRight.Location = New System.Drawing.Point(146, 290)
        Me.lblMarginRight.Name = "lblMarginRight"
        Me.lblMarginRight.Size = New System.Drawing.Size(220, 26)
        Me.lblMarginRight.TabIndex = 10
        Me.lblMarginRight.Text = "الهامش الأيمن"
        Me.lblMarginRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblMarginLeft
        '
        Me.lblMarginLeft.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblMarginLeft.Location = New System.Drawing.Point(146, 253)
        Me.lblMarginLeft.Name = "lblMarginLeft"
        Me.lblMarginLeft.Size = New System.Drawing.Size(220, 26)
        Me.lblMarginLeft.TabIndex = 8
        Me.lblMarginLeft.Text = "الهامش الأيسر"
        Me.lblMarginLeft.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbPrinter
        '
        Me.cmbPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPrinter.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbPrinter.FormattingEnabled = True
        Me.cmbPrinter.Location = New System.Drawing.Point(20, 105)
        Me.cmbPrinter.Name = "cmbPrinter"
        Me.cmbPrinter.Size = New System.Drawing.Size(220, 25)
        Me.cmbPrinter.TabIndex = 7
        '
        'lblPrinter
        '
        Me.lblPrinter.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblPrinter.Location = New System.Drawing.Point(246, 103)
        Me.lblPrinter.Name = "lblPrinter"
        Me.lblPrinter.Size = New System.Drawing.Size(120, 26)
        Me.lblPrinter.TabIndex = 6
        Me.lblPrinter.Text = "الطابعة"
        Me.lblPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbPaperKind
        '
        Me.cmbPaperKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPaperKind.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbPaperKind.FormattingEnabled = True
        Me.cmbPaperKind.Items.AddRange(New Object() {"A4", "A5", "A6", "RECEIPT"})
        Me.cmbPaperKind.Location = New System.Drawing.Point(20, 68)
        Me.cmbPaperKind.Name = "cmbPaperKind"
        Me.cmbPaperKind.Size = New System.Drawing.Size(220, 25)
        Me.cmbPaperKind.TabIndex = 5
        '
        'lblPaper
        '
        Me.lblPaper.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblPaper.Location = New System.Drawing.Point(246, 66)
        Me.lblPaper.Name = "lblPaper"
        Me.lblPaper.Size = New System.Drawing.Size(120, 26)
        Me.lblPaper.TabIndex = 4
        Me.lblPaper.Text = "نوع الورق"
        Me.lblPaper.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtProfileName
        '
        Me.txtProfileName.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtProfileName.Location = New System.Drawing.Point(20, 31)
        Me.txtProfileName.Name = "txtProfileName"
        Me.txtProfileName.Size = New System.Drawing.Size(220, 25)
        Me.txtProfileName.TabIndex = 3
        '
        'lblProfileName
        '
        Me.lblProfileName.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblProfileName.Location = New System.Drawing.Point(246, 29)
        Me.lblProfileName.Name = "lblProfileName"
        Me.lblProfileName.Size = New System.Drawing.Size(120, 26)
        Me.lblProfileName.TabIndex = 2
        Me.lblProfileName.Text = "اسم التصميم"
        Me.lblProfileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbProfiles
        '
        Me.cmbProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProfiles.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbProfiles.FormattingEnabled = True
        Me.cmbProfiles.Location = New System.Drawing.Point(20, 454)
        Me.cmbProfiles.Name = "cmbProfiles"
        Me.cmbProfiles.Size = New System.Drawing.Size(220, 25)
        Me.cmbProfiles.TabIndex = 1
        '
        'lblProfiles
        '
        Me.lblProfiles.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblProfiles.Location = New System.Drawing.Point(246, 452)
        Me.lblProfiles.Name = "lblProfiles"
        Me.lblProfiles.Size = New System.Drawing.Size(120, 26)
        Me.lblProfiles.TabIndex = 0
        Me.lblProfiles.Text = "التصاميم"
        Me.lblProfiles.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSections
        '
        Me.lblSections.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSections.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblSections.Location = New System.Drawing.Point(554, 10)
        Me.lblSections.Name = "lblSections"
        Me.lblSections.Size = New System.Drawing.Size(300, 20)
        Me.lblSections.TabIndex = 1
        Me.lblSections.Text = "مكونات التقرير"
        Me.lblSections.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblStyles
        '
        Me.lblStyles.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStyles.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblStyles.Location = New System.Drawing.Point(554, 229)
        Me.lblStyles.Name = "lblStyles"
        Me.lblStyles.Size = New System.Drawing.Size(300, 20)
        Me.lblStyles.TabIndex = 5
        Me.lblStyles.Text = "الألوان والتنسيقات"
        Me.lblStyles.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblColumns
        '
        Me.lblColumns.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblColumns.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblColumns.Location = New System.Drawing.Point(554, 413)
        Me.lblColumns.Name = "lblColumns"
        Me.lblColumns.Size = New System.Drawing.Size(300, 20)
        Me.lblColumns.TabIndex = 3
        Me.lblColumns.Text = "أعمدة جدول الأصناف"
        Me.lblColumns.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlButtons
        '
        Me.pnlButtons.Controls.Add(Me.btnNew)
        Me.pnlButtons.Controls.Add(Me.btnSave)
        Me.pnlButtons.Controls.Add(Me.btnDefault)
        Me.pnlButtons.Controls.Add(Me.btnPreview)
        Me.pnlButtons.Controls.Add(Me.btnPrint)
        Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlButtons.Location = New System.Drawing.Point(0, 662)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Padding = New System.Windows.Forms.Padding(12, 8, 12, 8)
        Me.pnlButtons.Size = New System.Drawing.Size(1260, 58)
        Me.pnlButtons.TabIndex = 2
        '
        'btnNew
        '
        Me.btnNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNew.BackColor = System.Drawing.Color.White
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnNew.Location = New System.Drawing.Point(1052, 11)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(96, 36)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "جديد"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.Location = New System.Drawing.Point(950, 11)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(96, 36)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "حفظ"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnDefault
        '
        Me.btnDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDefault.BackColor = System.Drawing.Color.White
        Me.btnDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDefault.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnDefault.Location = New System.Drawing.Point(800, 11)
        Me.btnDefault.Name = "btnDefault"
        Me.btnDefault.Size = New System.Drawing.Size(144, 36)
        Me.btnDefault.TabIndex = 2
        Me.btnDefault.Text = "تعيين كافتراضي"
        Me.btnDefault.UseVisualStyleBackColor = False
        '
        'btnPreview
        '
        Me.btnPreview.BackColor = System.Drawing.Color.White
        Me.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnPreview.Location = New System.Drawing.Point(118, 11)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(100, 36)
        Me.btnPreview.TabIndex = 3
        Me.btnPreview.Text = "معاينة"
        Me.btnPreview.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.White
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrint.Location = New System.Drawing.Point(12, 11)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(100, 36)
        Me.btnPrint.TabIndex = 4
        Me.btnPrint.Text = "طباعة"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'FrmSalesPrintLayoutManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1260, 720)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlButtons)
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmSalesPrintLayoutManager"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إدارة التقرير الديناميكي لفاتورة المبيعات"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.grpTemplates.ResumeLayout(False)
        CType(Me.dgvColumns, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvStyles, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvSections, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpOptions.ResumeLayout(False)
        Me.grpOptions.PerformLayout()
        CType(Me.numLogoHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numLogoWidth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMarginBottom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMarginTop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMarginRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numMarginLeft, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents btnClose As Button
    Friend WithEvents pnlMain As Panel
    Friend WithEvents grpTemplates As GroupBox
    Friend WithEvents btnApplyTemplate As Button
    Friend WithEvents pnlTemplatePreview As Panel
    Friend WithEvents lstTemplates As ListBox
    Friend WithEvents dgvColumns As DataGridView
    Friend WithEvents dgvStyles As DataGridView
    Friend WithEvents dgvSections As DataGridView
    Friend WithEvents grpOptions As GroupBox
    Friend WithEvents numLogoHeight As NumericUpDown
    Friend WithEvents numLogoWidth As NumericUpDown
    Friend WithEvents lblLogoHeight As Label
    Friend WithEvents lblLogoWidth As Label
    Friend WithEvents cmbFontFamily As ComboBox
    Friend WithEvents lblFontFamily As Label
    Friend WithEvents chkLandscape As CheckBox
    Friend WithEvents numMarginBottom As NumericUpDown
    Friend WithEvents numMarginTop As NumericUpDown
    Friend WithEvents numMarginRight As NumericUpDown
    Friend WithEvents numMarginLeft As NumericUpDown
    Friend WithEvents lblMarginBottom As Label
    Friend WithEvents lblMarginTop As Label
    Friend WithEvents lblMarginRight As Label
    Friend WithEvents lblMarginLeft As Label
    Friend WithEvents cmbPrinter As ComboBox
    Friend WithEvents lblPrinter As Label
    Friend WithEvents cmbPaperKind As ComboBox
    Friend WithEvents lblPaper As Label
    Friend WithEvents txtProfileName As TextBox
    Friend WithEvents lblProfileName As Label
    Friend WithEvents cmbProfiles As ComboBox
    Friend WithEvents lblProfiles As Label
    Friend WithEvents lblSections As Label
    Friend WithEvents lblStyles As Label
    Friend WithEvents lblColumns As Label
    Friend WithEvents pnlButtons As Panel
    Friend WithEvents btnNew As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnDefault As Button
    Friend WithEvents btnPreview As Button
    Friend WithEvents btnPrint As Button
End Class
