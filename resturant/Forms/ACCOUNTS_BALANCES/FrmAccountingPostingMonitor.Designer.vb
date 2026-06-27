<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAccountingPostingMonitor
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlActions As System.Windows.Forms.Panel
    Friend WithEvents pnlGrid As System.Windows.Forms.Panel
    Friend WithEvents pnlDetails As System.Windows.Forms.Panel
    Friend WithEvents pnlStatus As System.Windows.Forms.Panel

    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblSource As System.Windows.Forms.Label
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblSearch As System.Windows.Forms.Label

    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbSource As System.Windows.Forms.ComboBox
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPostingStatus As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox

    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnPostSelected As System.Windows.Forms.Button
    Friend WithEvents btnPostAll As System.Windows.Forms.Button
    Friend WithEvents btnViewJournal As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button

    Friend WithEvents dgvPosting As System.Windows.Forms.DataGridView
    Friend WithEvents dgvJournal As System.Windows.Forms.DataGridView

    Friend WithEvents lblJournalTitle As System.Windows.Forms.Label
    Friend WithEvents lblTotalCount As System.Windows.Forms.Label
    Friend WithEvents lblPostedCount As System.Windows.Forms.Label
    Friend WithEvents lblUnpostedCount As System.Windows.Forms.Label
    Friend WithEvents lblStatusMessage As System.Windows.Forms.Label
    Friend WithEvents flowActions As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents flowStatus As System.Windows.Forms.FlowLayoutPanel

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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.IM_RECOUNT_COST_Link = New System.Windows.Forms.LinkLabel()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblSource = New System.Windows.Forms.Label()
        Me.cmbPostingStatus = New System.Windows.Forms.ComboBox()
        Me.cmbSource = New System.Windows.Forms.ComboBox()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.lblType = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlActions = New System.Windows.Forms.Panel()
        Me.flowActions = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnPostSelected = New System.Windows.Forms.Button()
        Me.btnPostAll = New System.Windows.Forms.Button()
        Me.btnViewJournal = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.dgvPosting = New System.Windows.Forms.DataGridView()
        Me.pnlDetails = New System.Windows.Forms.Panel()
        Me.dgvJournal = New System.Windows.Forms.DataGridView()
        Me.lblJournalTitle = New System.Windows.Forms.Label()
        Me.pnlStatus = New System.Windows.Forms.Panel()
        Me.flowStatus = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblTotalCount = New System.Windows.Forms.Label()
        Me.lblPostedCount = New System.Windows.Forms.Label()
        Me.lblUnpostedCount = New System.Windows.Forms.Label()
        Me.lblStatusMessage = New System.Windows.Forms.Label()
        Me.pnlTop.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlActions.SuspendLayout()
        Me.flowActions.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me.dgvPosting, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDetails.SuspendLayout()
        CType(Me.dgvJournal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlStatus.SuspendLayout()
        Me.flowStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.White
        Me.pnlTop.Controls.Add(Me.Panel1)
        Me.pnlTop.Controls.Add(Me.lblTitle)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(9, 9, 9, 9)
        Me.pnlTop.Size = New System.Drawing.Size(1097, 146)
        Me.pnlTop.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.IM_RECOUNT_COST_Link)
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Controls.Add(Me.dtpTo)
        Me.Panel1.Controls.Add(Me.btnRefresh)
        Me.Panel1.Controls.Add(Me.lblSearch)
        Me.Panel1.Controls.Add(Me.lblTo)
        Me.Panel1.Controls.Add(Me.txtSearch)
        Me.Panel1.Controls.Add(Me.lblFrom)
        Me.Panel1.Controls.Add(Me.dtpFrom)
        Me.Panel1.Controls.Add(Me.lblSource)
        Me.Panel1.Controls.Add(Me.cmbPostingStatus)
        Me.Panel1.Controls.Add(Me.cmbSource)
        Me.Panel1.Controls.Add(Me.cmbType)
        Me.Panel1.Controls.Add(Me.lblType)
        Me.Panel1.Controls.Add(Me.lblStatus)
        Me.Panel1.Location = New System.Drawing.Point(9, 58)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1080, 84)
        Me.Panel1.TabIndex = 1
        '
        'IM_RECOUNT_COST_Link
        '
        Me.IM_RECOUNT_COST_Link.AutoSize = True
        Me.IM_RECOUNT_COST_Link.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IM_RECOUNT_COST_Link.Location = New System.Drawing.Point(757, 54)
        Me.IM_RECOUNT_COST_Link.Name = "IM_RECOUNT_COST_Link"
        Me.IM_RECOUNT_COST_Link.Size = New System.Drawing.Size(232, 19)
        Me.IM_RECOUNT_COST_Link.TabIndex = 14
        Me.IM_RECOUNT_COST_Link.TabStop = True
        Me.IM_RECOUNT_COST_Link.Text = "مستنذات إعادة إحتساب تكلفة المخزون"
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSearch.FlatAppearance.BorderSize = 0
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(2, 47)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(77, 32)
        Me.btnSearch.TabIndex = 12
        Me.btnSearch.Text = "بحث"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "yyyy/MM/dd"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(190, 58)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(103, 20)
        Me.dtpTo.TabIndex = 3
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(87, 47)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(77, 32)
        Me.btnRefresh.TabIndex = 13
        Me.btnRefresh.Text = "تحديث"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'lblSearch
        '
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(1035, 3)
        Me.lblSearch.Margin = New System.Windows.Forms.Padding(7, 3, 3, 3)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(43, 29)
        Me.lblSearch.TabIndex = 10
        Me.lblSearch.Text = "بحث"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTo
        '
        Me.lblTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblTo.Location = New System.Drawing.Point(297, 54)
        Me.lblTo.Margin = New System.Windows.Forms.Padding(7, 3, 3, 3)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(48, 26)
        Me.lblTo.TabIndex = 2
        Me.lblTo.Text = "إلى تاريخ"
        Me.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(812, 7)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(214, 23)
        Me.txtSearch.TabIndex = 11
        '
        'lblFrom
        '
        Me.lblFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblFrom.Location = New System.Drawing.Point(471, 54)
        Me.lblFrom.Margin = New System.Windows.Forms.Padding(7, 3, 3, 3)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(51, 26)
        Me.lblFrom.TabIndex = 0
        Me.lblFrom.Text = "من تاريخ"
        Me.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "yyyy/MM/dd"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(367, 57)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(103, 20)
        Me.dtpFrom.TabIndex = 1
        '
        'lblSource
        '
        Me.lblSource.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblSource.Location = New System.Drawing.Point(742, 3)
        Me.lblSource.Margin = New System.Windows.Forms.Padding(7, 3, 3, 3)
        Me.lblSource.Name = "lblSource"
        Me.lblSource.Size = New System.Drawing.Size(47, 26)
        Me.lblSource.TabIndex = 4
        Me.lblSource.Text = "المصدر"
        Me.lblSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbPostingStatus
        '
        Me.cmbPostingStatus.BackColor = System.Drawing.SystemColors.Info
        Me.cmbPostingStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPostingStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbPostingStatus.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPostingStatus.Location = New System.Drawing.Point(182, 7)
        Me.cmbPostingStatus.Name = "cmbPostingStatus"
        Me.cmbPostingStatus.Size = New System.Drawing.Size(112, 24)
        Me.cmbPostingStatus.TabIndex = 9
        '
        'cmbSource
        '
        Me.cmbSource.BackColor = System.Drawing.SystemColors.Info
        Me.cmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSource.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbSource.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSource.Location = New System.Drawing.Point(613, 6)
        Me.cmbSource.Name = "cmbSource"
        Me.cmbSource.Size = New System.Drawing.Size(121, 24)
        Me.cmbSource.TabIndex = 5
        '
        'cmbType
        '
        Me.cmbType.BackColor = System.Drawing.SystemColors.Info
        Me.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbType.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbType.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbType.Location = New System.Drawing.Point(367, 7)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(163, 24)
        Me.cmbType.TabIndex = 7
        '
        'lblType
        '
        Me.lblType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblType.Location = New System.Drawing.Point(300, 4)
        Me.lblType.Margin = New System.Windows.Forms.Padding(7, 3, 3, 3)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(64, 26)
        Me.lblType.TabIndex = 6
        Me.lblType.Text = "نوع الحركة"
        Me.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblStatus
        '
        Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblStatus.Location = New System.Drawing.Point(531, 3)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(7, 3, 3, 3)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(73, 26)
        Me.lblStatus.TabIndex = 8
        Me.lblStatus.Text = "حالة الترحيل"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTitle
        '
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(9, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(1079, 35)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "شاشة متابعة وترحيل الحركات المحاسبية"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlActions
        '
        Me.pnlActions.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlActions.Controls.Add(Me.flowActions)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlActions.Location = New System.Drawing.Point(0, 146)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Padding = New System.Windows.Forms.Padding(9, 9, 9, 9)
        Me.pnlActions.Size = New System.Drawing.Size(1097, 56)
        Me.pnlActions.TabIndex = 2
        '
        'flowActions
        '
        Me.flowActions.Controls.Add(Me.btnPostSelected)
        Me.flowActions.Controls.Add(Me.btnPostAll)
        Me.flowActions.Controls.Add(Me.btnViewJournal)
        Me.flowActions.Controls.Add(Me.btnClose)
        Me.flowActions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.flowActions.Location = New System.Drawing.Point(9, 9)
        Me.flowActions.Name = "flowActions"
        Me.flowActions.Size = New System.Drawing.Size(1079, 38)
        Me.flowActions.TabIndex = 0
        Me.flowActions.WrapContents = False
        '
        'btnPostSelected
        '
        Me.btnPostSelected.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.btnPostSelected.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPostSelected.FlatAppearance.BorderSize = 0
        Me.btnPostSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPostSelected.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnPostSelected.ForeColor = System.Drawing.Color.White
        Me.btnPostSelected.Location = New System.Drawing.Point(4, 5)
        Me.btnPostSelected.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnPostSelected.Name = "btnPostSelected"
        Me.btnPostSelected.Size = New System.Drawing.Size(129, 32)
        Me.btnPostSelected.TabIndex = 0
        Me.btnPostSelected.Text = "ترحيل المحدد"
        Me.btnPostSelected.UseVisualStyleBackColor = False
        '
        'btnPostAll
        '
        Me.btnPostAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.btnPostAll.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPostAll.FlatAppearance.BorderSize = 0
        Me.btnPostAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPostAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnPostAll.ForeColor = System.Drawing.Color.White
        Me.btnPostAll.Location = New System.Drawing.Point(141, 5)
        Me.btnPostAll.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnPostAll.Name = "btnPostAll"
        Me.btnPostAll.Size = New System.Drawing.Size(163, 32)
        Me.btnPostAll.TabIndex = 1
        Me.btnPostAll.Text = "ترحيل الكل غير المرحل"
        Me.btnPostAll.UseVisualStyleBackColor = False
        '
        'btnViewJournal
        '
        Me.btnViewJournal.BackColor = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.btnViewJournal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnViewJournal.FlatAppearance.BorderSize = 0
        Me.btnViewJournal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewJournal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnViewJournal.ForeColor = System.Drawing.Color.White
        Me.btnViewJournal.Location = New System.Drawing.Point(312, 5)
        Me.btnViewJournal.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnViewJournal.Name = "btnViewJournal"
        Me.btnViewJournal.Size = New System.Drawing.Size(111, 32)
        Me.btnViewJournal.TabIndex = 2
        Me.btnViewJournal.Text = "عرض القيد"
        Me.btnViewJournal.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(431, 5)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(86, 32)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "إغلاق"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'pnlGrid
        '
        Me.pnlGrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlGrid.Controls.Add(Me.dgvPosting)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(0, 202)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(9, 9, 9, 9)
        Me.pnlGrid.Size = New System.Drawing.Size(1097, 230)
        Me.pnlGrid.TabIndex = 0
        '
        'dgvPosting
        '
        Me.dgvPosting.AllowUserToAddRows = False
        Me.dgvPosting.AllowUserToDeleteRows = False
        Me.dgvPosting.BackgroundColor = System.Drawing.Color.White
        Me.dgvPosting.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.0!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPosting.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvPosting.ColumnHeadersHeight = 35
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.0!)
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPosting.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvPosting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPosting.EnableHeadersVisualStyles = False
        Me.dgvPosting.Location = New System.Drawing.Point(9, 9)
        Me.dgvPosting.MultiSelect = False
        Me.dgvPosting.Name = "dgvPosting"
        Me.dgvPosting.ReadOnly = True
        Me.dgvPosting.RowHeadersVisible = False
        Me.dgvPosting.RowTemplate.Height = 30
        Me.dgvPosting.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPosting.Size = New System.Drawing.Size(1079, 212)
        Me.dgvPosting.TabIndex = 0
        '
        'pnlDetails
        '
        Me.pnlDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlDetails.Controls.Add(Me.dgvJournal)
        Me.pnlDetails.Controls.Add(Me.lblJournalTitle)
        Me.pnlDetails.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlDetails.Location = New System.Drawing.Point(0, 432)
        Me.pnlDetails.Name = "pnlDetails"
        Me.pnlDetails.Padding = New System.Windows.Forms.Padding(9, 0, 9, 9)
        Me.pnlDetails.Size = New System.Drawing.Size(1097, 204)
        Me.pnlDetails.TabIndex = 1
        '
        'dgvJournal
        '
        Me.dgvJournal.AllowUserToAddRows = False
        Me.dgvJournal.AllowUserToDeleteRows = False
        Me.dgvJournal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvJournal.BackgroundColor = System.Drawing.Color.White
        Me.dgvJournal.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 8.0!)
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvJournal.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvJournal.ColumnHeadersHeight = 35
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Tahoma", 8.0!)
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvJournal.DefaultCellStyle = DataGridViewCellStyle8
        Me.dgvJournal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvJournal.EnableHeadersVisualStyles = False
        Me.dgvJournal.Location = New System.Drawing.Point(9, 30)
        Me.dgvJournal.Name = "dgvJournal"
        Me.dgvJournal.ReadOnly = True
        Me.dgvJournal.RowHeadersVisible = False
        Me.dgvJournal.RowTemplate.Height = 30
        Me.dgvJournal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvJournal.Size = New System.Drawing.Size(1079, 165)
        Me.dgvJournal.TabIndex = 0
        '
        'lblJournalTitle
        '
        Me.lblJournalTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblJournalTitle.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblJournalTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblJournalTitle.Location = New System.Drawing.Point(9, 0)
        Me.lblJournalTitle.Name = "lblJournalTitle"
        Me.lblJournalTitle.Size = New System.Drawing.Size(1079, 30)
        Me.lblJournalTitle.TabIndex = 1
        Me.lblJournalTitle.Text = "تفاصيل القيد المحاسبي"
        Me.lblJournalTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlStatus
        '
        Me.pnlStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.pnlStatus.Controls.Add(Me.flowStatus)
        Me.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlStatus.Location = New System.Drawing.Point(0, 636)
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Padding = New System.Windows.Forms.Padding(9, 5, 9, 5)
        Me.pnlStatus.Size = New System.Drawing.Size(1097, 33)
        Me.pnlStatus.TabIndex = 4
        '
        'flowStatus
        '
        Me.flowStatus.Controls.Add(Me.lblTotalCount)
        Me.flowStatus.Controls.Add(Me.lblPostedCount)
        Me.flowStatus.Controls.Add(Me.lblUnpostedCount)
        Me.flowStatus.Controls.Add(Me.lblStatusMessage)
        Me.flowStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowStatus.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.flowStatus.Location = New System.Drawing.Point(9, 5)
        Me.flowStatus.Name = "flowStatus"
        Me.flowStatus.Size = New System.Drawing.Size(1079, 23)
        Me.flowStatus.TabIndex = 0
        Me.flowStatus.WrapContents = False
        '
        'lblTotalCount
        '
        Me.lblTotalCount.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalCount.ForeColor = System.Drawing.Color.White
        Me.lblTotalCount.Location = New System.Drawing.Point(3, 0)
        Me.lblTotalCount.Name = "lblTotalCount"
        Me.lblTotalCount.Size = New System.Drawing.Size(154, 22)
        Me.lblTotalCount.TabIndex = 0
        Me.lblTotalCount.Text = "الإجمالي: 0"
        Me.lblTotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPostedCount
        '
        Me.lblPostedCount.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblPostedCount.ForeColor = System.Drawing.Color.White
        Me.lblPostedCount.Location = New System.Drawing.Point(163, 0)
        Me.lblPostedCount.Name = "lblPostedCount"
        Me.lblPostedCount.Size = New System.Drawing.Size(154, 22)
        Me.lblPostedCount.TabIndex = 1
        Me.lblPostedCount.Text = "المرحل: 0"
        Me.lblPostedCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblUnpostedCount
        '
        Me.lblUnpostedCount.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblUnpostedCount.ForeColor = System.Drawing.Color.White
        Me.lblUnpostedCount.Location = New System.Drawing.Point(323, 0)
        Me.lblUnpostedCount.Name = "lblUnpostedCount"
        Me.lblUnpostedCount.Size = New System.Drawing.Size(154, 22)
        Me.lblUnpostedCount.TabIndex = 2
        Me.lblUnpostedCount.Text = "غير المرحل: 0"
        Me.lblUnpostedCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblStatusMessage
        '
        Me.lblStatusMessage.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatusMessage.ForeColor = System.Drawing.Color.White
        Me.lblStatusMessage.Location = New System.Drawing.Point(483, 0)
        Me.lblStatusMessage.Name = "lblStatusMessage"
        Me.lblStatusMessage.Size = New System.Drawing.Size(300, 22)
        Me.lblStatusMessage.TabIndex = 3
        Me.lblStatusMessage.Text = "جاهز"
        Me.lblStatusMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmAccountingPostingMonitor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1097, 669)
        Me.Controls.Add(Me.pnlGrid)
        Me.Controls.Add(Me.pnlDetails)
        Me.Controls.Add(Me.pnlActions)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlStatus)
        Me.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Name = "FrmAccountingPostingMonitor"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "شاشة الترحيل المحاسبي"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTop.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlActions.ResumeLayout(False)
        Me.flowActions.ResumeLayout(False)
        Me.pnlGrid.ResumeLayout(False)
        CType(Me.dgvPosting, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDetails.ResumeLayout(False)
        CType(Me.dgvJournal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlStatus.ResumeLayout(False)
        Me.flowStatus.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents IM_RECOUNT_COST_Link As LinkLabel
End Class