<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSalesDraftActionLogMonitor
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
        Me.headerPanel = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.filtersPanel = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.lblBillNo = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.cmbSearchColumn = New System.Windows.Forms.ComboBox()
        Me.lblSearchColumn = New System.Windows.Forms.Label()
        Me.dtpToTime = New System.Windows.Forms.DateTimePicker()
        Me.lblToTime = New System.Windows.Forms.Label()
        Me.dtpFromTime = New System.Windows.Forms.DateTimePicker()
        Me.lblFromTime = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.cmbActionType = New System.Windows.Forms.ComboBox()
        Me.lblActionType = New System.Windows.Forms.Label()
        Me.cmbUser = New System.Windows.Forms.ComboBox()
        Me.lblUser = New System.Windows.Forms.Label()
        Me.summaryPanel = New System.Windows.Forms.Panel()
        Me.lblActionSummaryValue = New System.Windows.Forms.Label()
        Me.lblActionSummaryTitle = New System.Windows.Forms.Label()
        Me.lblPeriodValue = New System.Windows.Forms.Label()
        Me.lblPeriodTitle = New System.Windows.Forms.Label()
        Me.lblUsersValue = New System.Windows.Forms.Label()
        Me.lblUsersTitle = New System.Windows.Forms.Label()
        Me.lblCountValue = New System.Windows.Forms.Label()
        Me.lblCountTitle = New System.Windows.Forms.Label()
        Me.dgvLogs = New System.Windows.Forms.DataGridView()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.headerPanel.SuspendLayout()
        Me.filtersPanel.SuspendLayout()
        Me.summaryPanel.SuspendLayout()
        CType(Me.dgvLogs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'headerPanel
        '
        Me.headerPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.headerPanel.Controls.Add(Me.lblTitle)
        Me.headerPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.headerPanel.Location = New System.Drawing.Point(0, 0)
        Me.headerPanel.Name = "headerPanel"
        Me.headerPanel.Size = New System.Drawing.Size(1184, 62)
        Me.headerPanel.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Padding = New System.Windows.Forms.Padding(0, 0, 18, 0)
        Me.lblTitle.Size = New System.Drawing.Size(1184, 62)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "مراقبة وتتبع حركات المبيعات المسودة"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'filtersPanel
        '
        Me.filtersPanel.BackColor = System.Drawing.Color.White
        Me.filtersPanel.Controls.Add(Me.btnPrint)
        Me.filtersPanel.Controls.Add(Me.btnClose)
        Me.filtersPanel.Controls.Add(Me.btnClear)
        Me.filtersPanel.Controls.Add(Me.btnRefresh)
        Me.filtersPanel.Controls.Add(Me.btnSearch)
        Me.filtersPanel.Controls.Add(Me.txtBillNo)
        Me.filtersPanel.Controls.Add(Me.lblBillNo)
        Me.filtersPanel.Controls.Add(Me.txtSearch)
        Me.filtersPanel.Controls.Add(Me.lblSearch)
        Me.filtersPanel.Controls.Add(Me.cmbSearchColumn)
        Me.filtersPanel.Controls.Add(Me.lblSearchColumn)
        Me.filtersPanel.Controls.Add(Me.dtpToTime)
        Me.filtersPanel.Controls.Add(Me.lblToTime)
        Me.filtersPanel.Controls.Add(Me.dtpFromTime)
        Me.filtersPanel.Controls.Add(Me.lblFromTime)
        Me.filtersPanel.Controls.Add(Me.dtpToDate)
        Me.filtersPanel.Controls.Add(Me.lblToDate)
        Me.filtersPanel.Controls.Add(Me.dtpFromDate)
        Me.filtersPanel.Controls.Add(Me.lblFromDate)
        Me.filtersPanel.Controls.Add(Me.cmbActionType)
        Me.filtersPanel.Controls.Add(Me.lblActionType)
        Me.filtersPanel.Controls.Add(Me.cmbUser)
        Me.filtersPanel.Controls.Add(Me.lblUser)
        Me.filtersPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.filtersPanel.Location = New System.Drawing.Point(0, 62)
        Me.filtersPanel.Name = "filtersPanel"
        Me.filtersPanel.Size = New System.Drawing.Size(1184, 116)
        Me.filtersPanel.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(185, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(3, 62)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(102, 36)
        Me.btnClose.TabIndex = 9
        Me.btnClose.Text = "✕ إغلاق"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPrint.FlatAppearance.BorderSize = 0
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Location = New System.Drawing.Point(3, 20)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(102, 36)
        Me.btnPrint.TabIndex = 13
        Me.btnPrint.Text = "🖨 طباعة"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Location = New System.Drawing.Point(107, 62)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(102, 36)
        Me.btnClear.TabIndex = 8
        Me.btnClear.Text = "↺ تفريغ"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(14, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(144, Byte), Integer))
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(211, 62)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(102, 36)
        Me.btnRefresh.TabIndex = 7
        Me.btnRefresh.Text = "⟳ تحديث"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSearch.FlatAppearance.BorderSize = 0
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(316, 62)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(102, 36)
        Me.btnSearch.TabIndex = 6
        Me.btnSearch.Text = "🔎 بحث"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'txtBillNo
        '
        Me.txtBillNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtBillNo.Location = New System.Drawing.Point(423, 66)
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.Size = New System.Drawing.Size(172, 25)
        Me.txtBillNo.TabIndex = 5
        Me.txtBillNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblBillNo
        '
        Me.lblBillNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBillNo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblBillNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblBillNo.Location = New System.Drawing.Point(601, 66)
        Me.lblBillNo.Name = "lblBillNo"
        Me.lblBillNo.Size = New System.Drawing.Size(74, 24)
        Me.lblBillNo.TabIndex = 17
        Me.lblBillNo.Text = "رقم الفاتورة"
        Me.lblBillNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtSearch.Location = New System.Drawing.Point(423, 23)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(172, 25)
        Me.txtSearch.TabIndex = 2
        '
        'lblSearch
        '
        Me.lblSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSearch.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(601, 23)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(70, 24)
        Me.lblSearch.TabIndex = 15
        Me.lblSearch.Text = "بحث نصي"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbSearchColumn
        '
        Me.cmbSearchColumn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbSearchColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearchColumn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbSearchColumn.FormattingEnabled = True
        Me.cmbSearchColumn.Location = New System.Drawing.Point(228, 23)
        Me.cmbSearchColumn.Name = "cmbSearchColumn"
        Me.cmbSearchColumn.Size = New System.Drawing.Size(125, 25)
        Me.cmbSearchColumn.TabIndex = 12
        '
        'lblSearchColumn
        '
        Me.lblSearchColumn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSearchColumn.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblSearchColumn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblSearchColumn.Location = New System.Drawing.Point(359, 23)
        Me.lblSearchColumn.Name = "lblSearchColumn"
        Me.lblSearchColumn.Size = New System.Drawing.Size(64, 24)
        Me.lblSearchColumn.TabIndex = 18
        Me.lblSearchColumn.Text = "في عمود"
        Me.lblSearchColumn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpToTime
        '
        Me.dtpToTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpToTime.CustomFormat = "HH:mm:ss"
        Me.dtpToTime.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.dtpToTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToTime.Location = New System.Drawing.Point(687, 66)
        Me.dtpToTime.Name = "dtpToTime"
        Me.dtpToTime.ShowUpDown = True
        Me.dtpToTime.Size = New System.Drawing.Size(91, 25)
        Me.dtpToTime.TabIndex = 4
        '
        'lblToTime
        '
        Me.lblToTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblToTime.AutoSize = True
        Me.lblToTime.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblToTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblToTime.Location = New System.Drawing.Point(781, 70)
        Me.lblToTime.Name = "lblToTime"
        Me.lblToTime.Size = New System.Drawing.Size(56, 17)
        Me.lblToTime.TabIndex = 13
        Me.lblToTime.Text = "إلى وقت"
        Me.lblToTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpFromTime
        '
        Me.dtpFromTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpFromTime.CustomFormat = "HH:mm:ss"
        Me.dtpFromTime.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.dtpFromTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromTime.Location = New System.Drawing.Point(843, 67)
        Me.dtpFromTime.Name = "dtpFromTime"
        Me.dtpFromTime.ShowUpDown = True
        Me.dtpFromTime.Size = New System.Drawing.Size(91, 25)
        Me.dtpFromTime.TabIndex = 3
        '
        'lblFromTime
        '
        Me.lblFromTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFromTime.AutoSize = True
        Me.lblFromTime.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblFromTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblFromTime.Location = New System.Drawing.Point(937, 73)
        Me.lblFromTime.Name = "lblFromTime"
        Me.lblFromTime.Size = New System.Drawing.Size(55, 17)
        Me.lblFromTime.TabIndex = 11
        Me.lblFromTime.Text = "من وقت"
        Me.lblFromTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpToDate
        '
        Me.dtpToDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpToDate.CustomFormat = "yyyy/MM/dd"
        Me.dtpToDate.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(687, 23)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(91, 25)
        Me.dtpToDate.TabIndex = 1
        '
        'lblToDate
        '
        Me.lblToDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblToDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblToDate.Location = New System.Drawing.Point(782, 28)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(26, 17)
        Me.lblToDate.TabIndex = 9
        Me.lblToDate.Text = "إلى"
        Me.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpFromDate
        '
        Me.dtpFromDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpFromDate.CustomFormat = "yyyy/MM/dd"
        Me.dtpFromDate.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(840, 25)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(91, 25)
        Me.dtpFromDate.TabIndex = 0
        '
        'lblFromDate
        '
        Me.lblFromDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblFromDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblFromDate.Location = New System.Drawing.Point(934, 29)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(25, 17)
        Me.lblFromDate.TabIndex = 7
        Me.lblFromDate.Text = "من"
        Me.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbActionType
        '
        Me.cmbActionType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbActionType.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbActionType.FormattingEnabled = True
        Me.cmbActionType.Location = New System.Drawing.Point(992, 73)
        Me.cmbActionType.Name = "cmbActionType"
        Me.cmbActionType.Size = New System.Drawing.Size(188, 25)
        Me.cmbActionType.TabIndex = 11
        '
        'lblActionType
        '
        Me.lblActionType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblActionType.AutoSize = True
        Me.lblActionType.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblActionType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblActionType.Location = New System.Drawing.Point(1113, 51)
        Me.lblActionType.Name = "lblActionType"
        Me.lblActionType.Size = New System.Drawing.Size(64, 17)
        Me.lblActionType.TabIndex = 5
        Me.lblActionType.Text = "نوع الحركة"
        Me.lblActionType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbUser
        '
        Me.cmbUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUser.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbUser.FormattingEnabled = True
        Me.cmbUser.Location = New System.Drawing.Point(992, 23)
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.Size = New System.Drawing.Size(187, 25)
        Me.cmbUser.TabIndex = 10
        '
        'lblUser
        '
        Me.lblUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUser.AutoSize = True
        Me.lblUser.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblUser.Location = New System.Drawing.Point(1115, 3)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(61, 17)
        Me.lblUser.TabIndex = 3
        Me.lblUser.Text = "المستخدم"
        Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'summaryPanel
        '
        Me.summaryPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.summaryPanel.Controls.Add(Me.lblActionSummaryValue)
        Me.summaryPanel.Controls.Add(Me.lblActionSummaryTitle)
        Me.summaryPanel.Controls.Add(Me.lblPeriodValue)
        Me.summaryPanel.Controls.Add(Me.lblPeriodTitle)
        Me.summaryPanel.Controls.Add(Me.lblUsersValue)
        Me.summaryPanel.Controls.Add(Me.lblUsersTitle)
        Me.summaryPanel.Controls.Add(Me.lblCountValue)
        Me.summaryPanel.Controls.Add(Me.lblCountTitle)
        Me.summaryPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.summaryPanel.Location = New System.Drawing.Point(0, 178)
        Me.summaryPanel.Name = "summaryPanel"
        Me.summaryPanel.Size = New System.Drawing.Size(1184, 74)
        Me.summaryPanel.TabIndex = 2
        '
        'lblActionSummaryValue
        '
        Me.lblActionSummaryValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblActionSummaryValue.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblActionSummaryValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblActionSummaryValue.Location = New System.Drawing.Point(12, 33)
        Me.lblActionSummaryValue.Name = "lblActionSummaryValue"
        Me.lblActionSummaryValue.Size = New System.Drawing.Size(480, 26)
        Me.lblActionSummaryValue.TabIndex = 7
        Me.lblActionSummaryValue.Text = "-"
        Me.lblActionSummaryValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblActionSummaryTitle
        '
        Me.lblActionSummaryTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblActionSummaryTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblActionSummaryTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.lblActionSummaryTitle.Location = New System.Drawing.Point(12, 10)
        Me.lblActionSummaryTitle.Name = "lblActionSummaryTitle"
        Me.lblActionSummaryTitle.Size = New System.Drawing.Size(480, 20)
        Me.lblActionSummaryTitle.TabIndex = 6
        Me.lblActionSummaryTitle.Text = "ملخص نوع الحركة"
        Me.lblActionSummaryTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPeriodValue
        '
        Me.lblPeriodValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPeriodValue.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblPeriodValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblPeriodValue.Location = New System.Drawing.Point(498, 33)
        Me.lblPeriodValue.Name = "lblPeriodValue"
        Me.lblPeriodValue.Size = New System.Drawing.Size(300, 26)
        Me.lblPeriodValue.TabIndex = 5
        Me.lblPeriodValue.Text = "-"
        Me.lblPeriodValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPeriodTitle
        '
        Me.lblPeriodTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPeriodTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblPeriodTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.lblPeriodTitle.Location = New System.Drawing.Point(498, 10)
        Me.lblPeriodTitle.Name = "lblPeriodTitle"
        Me.lblPeriodTitle.Size = New System.Drawing.Size(300, 20)
        Me.lblPeriodTitle.TabIndex = 4
        Me.lblPeriodTitle.Text = "نطاق الوقت الظاهر"
        Me.lblPeriodTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblUsersValue
        '
        Me.lblUsersValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUsersValue.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblUsersValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblUsersValue.Location = New System.Drawing.Point(804, 28)
        Me.lblUsersValue.Name = "lblUsersValue"
        Me.lblUsersValue.Size = New System.Drawing.Size(160, 31)
        Me.lblUsersValue.TabIndex = 3
        Me.lblUsersValue.Text = "0"
        Me.lblUsersValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblUsersTitle
        '
        Me.lblUsersTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUsersTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblUsersTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.lblUsersTitle.Location = New System.Drawing.Point(804, 10)
        Me.lblUsersTitle.Name = "lblUsersTitle"
        Me.lblUsersTitle.Size = New System.Drawing.Size(160, 20)
        Me.lblUsersTitle.TabIndex = 2
        Me.lblUsersTitle.Text = "عدد المستخدمين"
        Me.lblUsersTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCountValue
        '
        Me.lblCountValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCountValue.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblCountValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(21, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(61, Byte), Integer))
        Me.lblCountValue.Location = New System.Drawing.Point(970, 28)
        Me.lblCountValue.Name = "lblCountValue"
        Me.lblCountValue.Size = New System.Drawing.Size(194, 31)
        Me.lblCountValue.TabIndex = 1
        Me.lblCountValue.Text = "0"
        Me.lblCountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCountTitle
        '
        Me.lblCountTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCountTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblCountTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.lblCountTitle.Location = New System.Drawing.Point(970, 10)
        Me.lblCountTitle.Name = "lblCountTitle"
        Me.lblCountTitle.Size = New System.Drawing.Size(194, 20)
        Me.lblCountTitle.TabIndex = 0
        Me.lblCountTitle.Text = "عدد الحركات"
        Me.lblCountTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvLogs
        '
        Me.dgvLogs.AllowUserToAddRows = False
        Me.dgvLogs.AllowUserToDeleteRows = False
        Me.dgvLogs.BackgroundColor = System.Drawing.Color.White
        Me.dgvLogs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLogs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLogs.Location = New System.Drawing.Point(0, 252)
        Me.dgvLogs.MultiSelect = False
        Me.dgvLogs.Name = "dgvLogs"
        Me.dgvLogs.ReadOnly = True
        Me.dgvLogs.RowHeadersVisible = False
        Me.dgvLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLogs.Size = New System.Drawing.Size(1184, 487)
        Me.dgvLogs.TabIndex = 3
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 739)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StatusStrip1.Size = New System.Drawing.Size(1184, 22)
        Me.StatusStrip1.TabIndex = 4
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(30, 17)
        Me.lblStatus.Text = "جاهز"
        '
        'FrmSalesDraftActionLogMonitor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1184, 761)
        Me.Controls.Add(Me.dgvLogs)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.summaryPanel)
        Me.Controls.Add(Me.filtersPanel)
        Me.Controls.Add(Me.headerPanel)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.MinimumSize = New System.Drawing.Size(1050, 650)
        Me.Name = "FrmSalesDraftActionLogMonitor"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "مراقبة حركات المبيعات"
        Me.headerPanel.ResumeLayout(False)
        Me.filtersPanel.ResumeLayout(False)
        Me.filtersPanel.PerformLayout()
        Me.summaryPanel.ResumeLayout(False)
        CType(Me.dgvLogs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents headerPanel As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents filtersPanel As System.Windows.Forms.Panel
    Friend WithEvents cmbUser As System.Windows.Forms.ComboBox
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Friend WithEvents cmbActionType As System.Windows.Forms.ComboBox
    Friend WithEvents lblActionType As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents dtpFromTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFromTime As System.Windows.Forms.Label
    Friend WithEvents dtpToTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblToTime As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents cmbSearchColumn As System.Windows.Forms.ComboBox
    Friend WithEvents lblSearchColumn As System.Windows.Forms.Label
    Friend WithEvents txtBillNo As System.Windows.Forms.TextBox
    Friend WithEvents lblBillNo As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents summaryPanel As System.Windows.Forms.Panel
    Friend WithEvents lblCountTitle As System.Windows.Forms.Label
    Friend WithEvents lblCountValue As System.Windows.Forms.Label
    Friend WithEvents lblUsersTitle As System.Windows.Forms.Label
    Friend WithEvents lblUsersValue As System.Windows.Forms.Label
    Friend WithEvents lblPeriodTitle As System.Windows.Forms.Label
    Friend WithEvents lblPeriodValue As System.Windows.Forms.Label
    Friend WithEvents lblActionSummaryTitle As System.Windows.Forms.Label
    Friend WithEvents lblActionSummaryValue As System.Windows.Forms.Label
    Friend WithEvents dgvLogs As System.Windows.Forms.DataGridView
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
End Class
