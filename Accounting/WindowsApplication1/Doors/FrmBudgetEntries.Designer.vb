<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBudgetEntries
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    <System.Diagnostics.DebuggerNonUserCode()>
    Private Sub InitializeComponent()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblBudgetOverSpendWarning = New System.Windows.Forms.Label()
        Me.lblMode = New System.Windows.Forms.Label()
        Me.lblSubTitle = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlContent = New System.Windows.Forms.Panel()
        Me.cardGrid = New System.Windows.Forms.Panel()
        Me.dgvEntries = New System.Windows.Forms.DataGridView()
        Me.pnlGridFilter = New System.Windows.Forms.Panel()
        Me.lblEntriesFilter = New System.Windows.Forms.Label()
        Me.txtEntriesFilter = New System.Windows.Forms.TextBox()
        Me.cardDetails = New System.Windows.Forms.Panel()
        Me.txtSelectedDetails = New System.Windows.Forms.TextBox()
        Me.lblDetailsTitle = New System.Windows.Forms.Label()
        Me.cardForm = New System.Windows.Forms.Panel()
        Me.lblAmountWords = New System.Windows.Forms.Label()
        Me.txtAmountWords = New System.Windows.Forms.TextBox()
        Me.pnlSummary = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblAvailable = New System.Windows.Forms.Label()
        Me.lblReserved = New System.Windows.Forms.Label()
        Me.lblSpent = New System.Windows.Forms.Label()
        Me.lblAllocated = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.lblNotes = New System.Windows.Forms.Label()
        Me.txtSpendStatement = New System.Windows.Forms.TextBox()
        Me.lblSpendStatement = New System.Windows.Forms.Label()
        Me.dtpEntryDate = New System.Windows.Forms.DateTimePicker()
        Me.lblEntryDate = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.cmbItems = New System.Windows.Forms.ComboBox()
        Me.cmbChapters = New System.Windows.Forms.ComboBox()
        Me.cmbDoors = New System.Windows.Forms.ComboBox()
        Me.cmbFiscalYear = New System.Windows.Forms.ComboBox()
        Me.lblCommitmentType = New System.Windows.Forms.Label()
        Me.cmbCommitmentTypes = New System.Windows.Forms.ComboBox()
        Me.lblSourceRef = New System.Windows.Forms.Label()
        Me.txtSourceRef = New System.Windows.Forms.TextBox()
        Me.lblSourceTable = New System.Windows.Forms.Label()
        Me.txtSourceTable = New System.Windows.Forms.TextBox()
        Me.chkHasStamp = New System.Windows.Forms.CheckBox()
        Me.lblStampPercent = New System.Windows.Forms.Label()
        Me.txtStampPercent = New System.Windows.Forms.TextBox()
        Me.txtStampAccountCode = New System.Windows.Forms.TextBox()
        Me.txtStampAccountName = New System.Windows.Forms.TextBox()
        Me.btnPickStampAccount = New System.Windows.Forms.Button()
        Me.lblLinkedBudgetAccount = New System.Windows.Forms.Label()
        Me.txtLinkedBudgetAccount = New System.Windows.Forms.TextBox()
        Me.lblItem = New System.Windows.Forms.Label()
        Me.lblChapter = New System.Windows.Forms.Label()
        Me.lblDoor = New System.Windows.Forms.Label()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.pnlActions = New System.Windows.Forms.Panel()
        Me.BtnCancelEntry = New System.Windows.Forms.Button()
        Me.btnPrintVoucher = New System.Windows.Forms.Button()
        Me.btnPrintOfficialVoucher = New System.Windows.Forms.Button()
        Me.btnPreviewJournal = New System.Windows.Forms.Button()
        Me.btnUpdateSpendStatement = New System.Windows.Forms.Button()
        Me.btnEditStamp = New System.Windows.Forms.Button()
        Me.BtnApprove = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnExecute = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.UcGridColumnsSelector1 = New Accounting.UcGridColumnsSelector()
        Me.pnlHeader.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.cardGrid.SuspendLayout()
        CType(Me.dgvEntries, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlGridFilter.SuspendLayout()
        Me.cardDetails.SuspendLayout()
        Me.cardForm.SuspendLayout()
        Me.pnlSummary.SuspendLayout()
        Me.pnlActions.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblBudgetOverSpendWarning)
        Me.pnlHeader.Controls.Add(Me.lblMode)
        Me.pnlHeader.Controls.Add(Me.lblSubTitle)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1280, 70)
        Me.pnlHeader.TabIndex = 2
        '
        'lblBudgetOverSpendWarning
        '
        Me.lblBudgetOverSpendWarning.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.lblBudgetOverSpendWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBudgetOverSpendWarning.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblBudgetOverSpendWarning.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblBudgetOverSpendWarning.ForeColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(77, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.lblBudgetOverSpendWarning.Location = New System.Drawing.Point(0, 46)
        Me.lblBudgetOverSpendWarning.Name = "lblBudgetOverSpendWarning"
        Me.lblBudgetOverSpendWarning.Size = New System.Drawing.Size(1280, 24)
        Me.lblBudgetOverSpendWarning.TabIndex = 3
        Me.lblBudgetOverSpendWarning.Text = "تنبيه: النظام يعمل حاليًا بسماحية تنفيذ عمليات الموازنة عند عدم كفاية الاعتماد"
        Me.lblBudgetOverSpendWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblBudgetOverSpendWarning.Visible = False
        '
        'lblMode
        '
        Me.lblMode.AutoSize = True
        Me.lblMode.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMode.ForeColor = System.Drawing.Color.LightSkyBlue
        Me.lblMode.Location = New System.Drawing.Point(173, 4)
        Me.lblMode.Name = "lblMode"
        Me.lblMode.Size = New System.Drawing.Size(103, 25)
        Me.lblMode.TabIndex = 0
        Me.lblMode.Text = "الوضع: حجز"
        '
        'lblSubTitle
        '
        Me.lblSubTitle.AutoSize = True
        Me.lblSubTitle.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblSubTitle.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblSubTitle.Location = New System.Drawing.Point(662, 10)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Size = New System.Drawing.Size(217, 17)
        Me.lblSubTitle.TabIndex = 1
        Me.lblSubTitle.Text = "تنفيذ الحجز أو الصرف على بنود الموازنة"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(1030, 8)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(153, 30)
        Me.lblTitle.TabIndex = 2
        Me.lblTitle.Text = "عمليات الموازنة"
        '
        'pnlContent
        '
        Me.pnlContent.Controls.Add(Me.cardGrid)
        Me.pnlContent.Controls.Add(Me.cardDetails)
        Me.pnlContent.Controls.Add(Me.cardForm)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 70)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlContent.Size = New System.Drawing.Size(1280, 619)
        Me.pnlContent.TabIndex = 0
        '
        'cardGrid
        '
        Me.cardGrid.BackColor = System.Drawing.Color.White
        Me.cardGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardGrid.Controls.Add(Me.UcGridColumnsSelector1)
        Me.cardGrid.Controls.Add(Me.dgvEntries)
        Me.cardGrid.Controls.Add(Me.pnlGridFilter)
        Me.cardGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cardGrid.Font = New System.Drawing.Font("Segoe UI", 8.75!)
        Me.cardGrid.Location = New System.Drawing.Point(15, 216)
        Me.cardGrid.Name = "cardGrid"
        Me.cardGrid.Size = New System.Drawing.Size(1250, 326)
        Me.cardGrid.TabIndex = 0
        '
        'dgvEntries
        '
        Me.dgvEntries.AllowUserToAddRows = False
        Me.dgvEntries.AllowUserToDeleteRows = False
        Me.dgvEntries.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvEntries.BackgroundColor = System.Drawing.Color.White
        Me.dgvEntries.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvEntries.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvEntries.Location = New System.Drawing.Point(0, 34)
        Me.dgvEntries.MultiSelect = False
        Me.dgvEntries.Name = "dgvEntries"
        Me.dgvEntries.ReadOnly = True
        Me.dgvEntries.RowHeadersVisible = False
        Me.dgvEntries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvEntries.Size = New System.Drawing.Size(1248, 290)
        Me.dgvEntries.TabIndex = 0
        '
        'pnlGridFilter
        '
        Me.pnlGridFilter.BackColor = System.Drawing.Color.White
        Me.pnlGridFilter.Controls.Add(Me.lblEntriesFilter)
        Me.pnlGridFilter.Controls.Add(Me.txtEntriesFilter)
        Me.pnlGridFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlGridFilter.Location = New System.Drawing.Point(0, 0)
        Me.pnlGridFilter.Name = "pnlGridFilter"
        Me.pnlGridFilter.Padding = New System.Windows.Forms.Padding(8, 5, 8, 4)
        Me.pnlGridFilter.Size = New System.Drawing.Size(1248, 34)
        Me.pnlGridFilter.TabIndex = 1
        '
        'lblEntriesFilter
        '
        Me.lblEntriesFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEntriesFilter.AutoSize = True
        Me.lblEntriesFilter.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblEntriesFilter.Location = New System.Drawing.Point(1168, 9)
        Me.lblEntriesFilter.Name = "lblEntriesFilter"
        Me.lblEntriesFilter.Size = New System.Drawing.Size(58, 15)
        Me.lblEntriesFilter.TabIndex = 1
        Me.lblEntriesFilter.Text = "بحث سريع"
        '
        'txtEntriesFilter
        '
        Me.txtEntriesFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEntriesFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEntriesFilter.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtEntriesFilter.Location = New System.Drawing.Point(8, 6)
        Me.txtEntriesFilter.Name = "txtEntriesFilter"
        Me.txtEntriesFilter.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtEntriesFilter.Size = New System.Drawing.Size(1154, 23)
        Me.txtEntriesFilter.TabIndex = 0
        '
        'cardDetails
        '
        Me.cardDetails.BackColor = System.Drawing.Color.White
        Me.cardDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardDetails.Controls.Add(Me.txtSelectedDetails)
        Me.cardDetails.Controls.Add(Me.lblDetailsTitle)
        Me.cardDetails.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cardDetails.Location = New System.Drawing.Point(15, 542)
        Me.cardDetails.Name = "cardDetails"
        Me.cardDetails.Padding = New System.Windows.Forms.Padding(8, 30, 8, 8)
        Me.cardDetails.Size = New System.Drawing.Size(1250, 87)
        Me.cardDetails.TabIndex = 2
        '
        'txtSelectedDetails
        '
        Me.txtSelectedDetails.BackColor = System.Drawing.Color.White
        Me.txtSelectedDetails.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSelectedDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSelectedDetails.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.txtSelectedDetails.Location = New System.Drawing.Point(8, 30)
        Me.txtSelectedDetails.Multiline = True
        Me.txtSelectedDetails.Name = "txtSelectedDetails"
        Me.txtSelectedDetails.ReadOnly = True
        Me.txtSelectedDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSelectedDetails.Size = New System.Drawing.Size(1232, 47)
        Me.txtSelectedDetails.TabIndex = 1
        Me.txtSelectedDetails.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDetailsTitle
        '
        Me.lblDetailsTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDetailsTitle.AutoSize = True
        Me.lblDetailsTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetailsTitle.Location = New System.Drawing.Point(1166, 5)
        Me.lblDetailsTitle.Name = "lblDetailsTitle"
        Me.lblDetailsTitle.Size = New System.Drawing.Size(78, 15)
        Me.lblDetailsTitle.TabIndex = 0
        Me.lblDetailsTitle.Text = "تفاصيل الصف"
        '
        'cardForm
        '
        Me.cardForm.BackColor = System.Drawing.Color.White
        Me.cardForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardForm.Controls.Add(Me.lblAmountWords)
        Me.cardForm.Controls.Add(Me.txtAmountWords)
        Me.cardForm.Controls.Add(Me.pnlSummary)
        Me.cardForm.Controls.Add(Me.txtNotes)
        Me.cardForm.Controls.Add(Me.lblNotes)
        Me.cardForm.Controls.Add(Me.txtSpendStatement)
        Me.cardForm.Controls.Add(Me.lblSpendStatement)
        Me.cardForm.Controls.Add(Me.dtpEntryDate)
        Me.cardForm.Controls.Add(Me.lblEntryDate)
        Me.cardForm.Controls.Add(Me.txtAmount)
        Me.cardForm.Controls.Add(Me.lblAmount)
        Me.cardForm.Controls.Add(Me.cmbItems)
        Me.cardForm.Controls.Add(Me.cmbChapters)
        Me.cardForm.Controls.Add(Me.cmbDoors)
        Me.cardForm.Controls.Add(Me.cmbFiscalYear)
        Me.cardForm.Controls.Add(Me.lblCommitmentType)
        Me.cardForm.Controls.Add(Me.cmbCommitmentTypes)
        Me.cardForm.Controls.Add(Me.lblSourceRef)
        Me.cardForm.Controls.Add(Me.txtSourceRef)
        Me.cardForm.Controls.Add(Me.lblSourceTable)
        Me.cardForm.Controls.Add(Me.txtSourceTable)
        Me.cardForm.Controls.Add(Me.chkHasStamp)
        Me.cardForm.Controls.Add(Me.lblStampPercent)
        Me.cardForm.Controls.Add(Me.txtStampPercent)
        Me.cardForm.Controls.Add(Me.txtStampAccountCode)
        Me.cardForm.Controls.Add(Me.txtStampAccountName)
        Me.cardForm.Controls.Add(Me.btnPickStampAccount)
        Me.cardForm.Controls.Add(Me.lblLinkedBudgetAccount)
        Me.cardForm.Controls.Add(Me.txtLinkedBudgetAccount)
        Me.cardForm.Controls.Add(Me.lblItem)
        Me.cardForm.Controls.Add(Me.lblChapter)
        Me.cardForm.Controls.Add(Me.lblDoor)
        Me.cardForm.Controls.Add(Me.lblYear)
        Me.cardForm.Dock = System.Windows.Forms.DockStyle.Top
        Me.cardForm.Location = New System.Drawing.Point(15, 15)
        Me.cardForm.Name = "cardForm"
        Me.cardForm.Size = New System.Drawing.Size(1250, 201)
        Me.cardForm.TabIndex = 1
        '
        'lblAmountWords
        '
        Me.lblAmountWords.AutoSize = True
        Me.lblAmountWords.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmountWords.Location = New System.Drawing.Point(518, 173)
        Me.lblAmountWords.Name = "lblAmountWords"
        Me.lblAmountWords.Size = New System.Drawing.Size(86, 17)
        Me.lblAmountWords.TabIndex = 26
        Me.lblAmountWords.Text = "المبلغ بالحروف"
        '
        'txtAmountWords
        '
        Me.txtAmountWords.BackColor = System.Drawing.Color.White
        Me.txtAmountWords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmountWords.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtAmountWords.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.txtAmountWords.Location = New System.Drawing.Point(1, 170)
        Me.txtAmountWords.Name = "txtAmountWords"
        Me.txtAmountWords.ReadOnly = True
        Me.txtAmountWords.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtAmountWords.Size = New System.Drawing.Size(514, 23)
        Me.txtAmountWords.TabIndex = 27
        '
        'pnlSummary
        '
        Me.pnlSummary.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.pnlSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSummary.Controls.Add(Me.Label1)
        Me.pnlSummary.Controls.Add(Me.Label2)
        Me.pnlSummary.Controls.Add(Me.Label3)
        Me.pnlSummary.Controls.Add(Me.Label4)
        Me.pnlSummary.Controls.Add(Me.lblAvailable)
        Me.pnlSummary.Controls.Add(Me.lblReserved)
        Me.pnlSummary.Controls.Add(Me.lblSpent)
        Me.pnlSummary.Controls.Add(Me.lblAllocated)
        Me.pnlSummary.Location = New System.Drawing.Point(4, 2)
        Me.pnlSummary.Name = "pnlSummary"
        Me.pnlSummary.Size = New System.Drawing.Size(278, 150)
        Me.pnlSummary.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(152, 111)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 32)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "الرصيد المتاح للصرف"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(152, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(121, 32)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "إجمالي الحجوزات"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.DarkRed
        Me.Label3.Location = New System.Drawing.Point(152, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(121, 32)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "إجمالي المصروف"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label4.Location = New System.Drawing.Point(152, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(121, 32)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "إجمالي الاعتماد"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAvailable
        '
        Me.lblAvailable.BackColor = System.Drawing.Color.White
        Me.lblAvailable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAvailable.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvailable.ForeColor = System.Drawing.Color.Blue
        Me.lblAvailable.Location = New System.Drawing.Point(2, 111)
        Me.lblAvailable.Name = "lblAvailable"
        Me.lblAvailable.Size = New System.Drawing.Size(148, 32)
        Me.lblAvailable.TabIndex = 3
        Me.lblAvailable.Text = "--"
        Me.lblAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblReserved
        '
        Me.lblReserved.BackColor = System.Drawing.Color.White
        Me.lblReserved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReserved.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReserved.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblReserved.Location = New System.Drawing.Point(2, 75)
        Me.lblReserved.Name = "lblReserved"
        Me.lblReserved.Size = New System.Drawing.Size(148, 32)
        Me.lblReserved.TabIndex = 2
        Me.lblReserved.Text = "--"
        Me.lblReserved.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSpent
        '
        Me.lblSpent.BackColor = System.Drawing.Color.White
        Me.lblSpent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSpent.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpent.ForeColor = System.Drawing.Color.DarkRed
        Me.lblSpent.Location = New System.Drawing.Point(2, 39)
        Me.lblSpent.Name = "lblSpent"
        Me.lblSpent.Size = New System.Drawing.Size(148, 32)
        Me.lblSpent.TabIndex = 1
        Me.lblSpent.Text = "--"
        Me.lblSpent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAllocated
        '
        Me.lblAllocated.BackColor = System.Drawing.Color.White
        Me.lblAllocated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAllocated.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAllocated.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblAllocated.Location = New System.Drawing.Point(2, 3)
        Me.lblAllocated.Name = "lblAllocated"
        Me.lblAllocated.Size = New System.Drawing.Size(148, 32)
        Me.lblAllocated.TabIndex = 0
        Me.lblAllocated.Text = "--"
        Me.lblAllocated.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNotes
        '
        Me.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNotes.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtNotes.Location = New System.Drawing.Point(285, 118)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(230, 50)
        Me.txtNotes.TabIndex = 0
        '
        'lblNotes
        '
        Me.lblNotes.AutoSize = True
        Me.lblNotes.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotes.Location = New System.Drawing.Point(518, 121)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(57, 17)
        Me.lblNotes.TabIndex = 1
        Me.lblNotes.Text = "ملاحظات"
        '
        'txtSpendStatement
        '
        Me.txtSpendStatement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpendStatement.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.txtSpendStatement.Location = New System.Drawing.Point(285, 29)
        Me.txtSpendStatement.Multiline = True
        Me.txtSpendStatement.Name = "txtSpendStatement"
        Me.txtSpendStatement.Size = New System.Drawing.Size(230, 87)
        Me.txtSpendStatement.TabIndex = 28
        '
        'lblSpendStatement
        '
        Me.lblSpendStatement.AutoSize = True
        Me.lblSpendStatement.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpendStatement.Location = New System.Drawing.Point(518, 32)
        Me.lblSpendStatement.Name = "lblSpendStatement"
        Me.lblSpendStatement.Size = New System.Drawing.Size(144, 17)
        Me.lblSpendStatement.TabIndex = 29
        Me.lblSpendStatement.Text = "بيان الصرف (يطبع فالإذن)"
        '
        'dtpEntryDate
        '
        Me.dtpEntryDate.CustomFormat = "yyyy/MM/dd HH:mm"
        Me.dtpEntryDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.dtpEntryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEntryDate.Location = New System.Drawing.Point(897, 164)
        Me.dtpEntryDate.Name = "dtpEntryDate"
        Me.dtpEntryDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtpEntryDate.RightToLeftLayout = True
        Me.dtpEntryDate.Size = New System.Drawing.Size(265, 24)
        Me.dtpEntryDate.TabIndex = 30
        '
        'lblEntryDate
        '
        Me.lblEntryDate.AutoSize = True
        Me.lblEntryDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntryDate.Location = New System.Drawing.Point(1163, 168)
        Me.lblEntryDate.Name = "lblEntryDate"
        Me.lblEntryDate.Size = New System.Drawing.Size(82, 17)
        Me.lblEntryDate.TabIndex = 31
        Me.lblEntryDate.Text = "تاريخ المعاملة"
        '
        'txtAmount
        '
        Me.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtAmount.Location = New System.Drawing.Point(285, 2)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(230, 25)
        Me.txtAmount.TabIndex = 2
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount.Location = New System.Drawing.Point(518, 7)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(39, 17)
        Me.lblAmount.TabIndex = 3
        Me.lblAmount.Text = "المبلغ"
        '
        'cmbItems
        '
        Me.cmbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItems.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbItems.Location = New System.Drawing.Point(897, 90)
        Me.cmbItems.Name = "cmbItems"
        Me.cmbItems.Size = New System.Drawing.Size(270, 25)
        Me.cmbItems.TabIndex = 4
        '
        'cmbChapters
        '
        Me.cmbChapters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChapters.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbChapters.Location = New System.Drawing.Point(897, 62)
        Me.cmbChapters.Name = "cmbChapters"
        Me.cmbChapters.Size = New System.Drawing.Size(270, 25)
        Me.cmbChapters.TabIndex = 5
        '
        'cmbDoors
        '
        Me.cmbDoors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoors.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbDoors.Location = New System.Drawing.Point(897, 34)
        Me.cmbDoors.Name = "cmbDoors"
        Me.cmbDoors.Size = New System.Drawing.Size(270, 25)
        Me.cmbDoors.TabIndex = 6
        '
        'cmbFiscalYear
        '
        Me.cmbFiscalYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFiscalYear.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbFiscalYear.Location = New System.Drawing.Point(897, 5)
        Me.cmbFiscalYear.Name = "cmbFiscalYear"
        Me.cmbFiscalYear.Size = New System.Drawing.Size(270, 25)
        Me.cmbFiscalYear.TabIndex = 7
        '
        'lblCommitmentType
        '
        Me.lblCommitmentType.AutoSize = True
        Me.lblCommitmentType.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCommitmentType.Location = New System.Drawing.Point(821, 8)
        Me.lblCommitmentType.Name = "lblCommitmentType"
        Me.lblCommitmentType.Size = New System.Drawing.Size(59, 17)
        Me.lblCommitmentType.TabIndex = 12
        Me.lblCommitmentType.Text = "نوع الحجز"
        '
        'cmbCommitmentTypes
        '
        Me.cmbCommitmentTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCommitmentTypes.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbCommitmentTypes.Location = New System.Drawing.Point(675, 5)
        Me.cmbCommitmentTypes.Name = "cmbCommitmentTypes"
        Me.cmbCommitmentTypes.Size = New System.Drawing.Size(142, 25)
        Me.cmbCommitmentTypes.TabIndex = 13
        '
        'lblSourceRef
        '
        Me.lblSourceRef.AutoSize = True
        Me.lblSourceRef.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSourceRef.Location = New System.Drawing.Point(821, 37)
        Me.lblSourceRef.Name = "lblSourceRef"
        Me.lblSourceRef.Size = New System.Drawing.Size(71, 17)
        Me.lblSourceRef.TabIndex = 14
        Me.lblSourceRef.Text = "رقم المصدر"
        '
        'txtSourceRef
        '
        Me.txtSourceRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSourceRef.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtSourceRef.Location = New System.Drawing.Point(675, 33)
        Me.txtSourceRef.Name = "txtSourceRef"
        Me.txtSourceRef.Size = New System.Drawing.Size(142, 25)
        Me.txtSourceRef.TabIndex = 15
        Me.txtSourceRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSourceTable
        '
        Me.lblSourceTable.AutoSize = True
        Me.lblSourceTable.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSourceTable.Location = New System.Drawing.Point(820, 65)
        Me.lblSourceTable.Name = "lblSourceTable"
        Me.lblSourceTable.Size = New System.Drawing.Size(73, 17)
        Me.lblSourceTable.TabIndex = 16
        Me.lblSourceTable.Text = "مصدر الحجز"
        '
        'txtSourceTable
        '
        Me.txtSourceTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSourceTable.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtSourceTable.Location = New System.Drawing.Point(675, 61)
        Me.txtSourceTable.Name = "txtSourceTable"
        Me.txtSourceTable.Size = New System.Drawing.Size(142, 25)
        Me.txtSourceTable.TabIndex = 17
        Me.txtSourceTable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkHasStamp
        '
        Me.chkHasStamp.AutoSize = True
        Me.chkHasStamp.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.chkHasStamp.Location = New System.Drawing.Point(728, 89)
        Me.chkHasStamp.Name = "chkHasStamp"
        Me.chkHasStamp.Size = New System.Drawing.Size(87, 21)
        Me.chkHasStamp.TabIndex = 18
        Me.chkHasStamp.Text = "توجد دمغة"
        Me.chkHasStamp.UseVisualStyleBackColor = True
        '
        'lblStampPercent
        '
        Me.lblStampPercent.AutoSize = True
        Me.lblStampPercent.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStampPercent.Location = New System.Drawing.Point(639, 118)
        Me.lblStampPercent.Name = "lblStampPercent"
        Me.lblStampPercent.Size = New System.Drawing.Size(86, 17)
        Me.lblStampPercent.TabIndex = 19
        Me.lblStampPercent.Text = "نسبة الدمغة%"
        '
        'txtStampPercent
        '
        Me.txtStampPercent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStampPercent.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtStampPercent.Location = New System.Drawing.Point(594, 115)
        Me.txtStampPercent.Name = "txtStampPercent"
        Me.txtStampPercent.Size = New System.Drawing.Size(43, 23)
        Me.txtStampPercent.TabIndex = 20
        Me.txtStampPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtStampAccountCode
        '
        Me.txtStampAccountCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStampAccountCode.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtStampAccountCode.Location = New System.Drawing.Point(728, 115)
        Me.txtStampAccountCode.Name = "txtStampAccountCode"
        Me.txtStampAccountCode.Size = New System.Drawing.Size(91, 23)
        Me.txtStampAccountCode.TabIndex = 22
        Me.txtStampAccountCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtStampAccountName
        '
        Me.txtStampAccountName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtStampAccountName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStampAccountName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.txtStampAccountName.Location = New System.Drawing.Point(594, 139)
        Me.txtStampAccountName.Name = "txtStampAccountName"
        Me.txtStampAccountName.ReadOnly = True
        Me.txtStampAccountName.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtStampAccountName.Size = New System.Drawing.Size(225, 24)
        Me.txtStampAccountName.TabIndex = 28
        Me.txtStampAccountName.TabStop = False
        '
        'btnPickStampAccount
        '
        Me.btnPickStampAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPickStampAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnPickStampAccount.Location = New System.Drawing.Point(820, 115)
        Me.btnPickStampAccount.Name = "btnPickStampAccount"
        Me.btnPickStampAccount.Size = New System.Drawing.Size(31, 23)
        Me.btnPickStampAccount.TabIndex = 23
        Me.btnPickStampAccount.Text = "؟"
        '
        'lblLinkedBudgetAccount
        '
        Me.lblLinkedBudgetAccount.AutoSize = True
        Me.lblLinkedBudgetAccount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLinkedBudgetAccount.Location = New System.Drawing.Point(1048, 117)
        Me.lblLinkedBudgetAccount.Name = "lblLinkedBudgetAccount"
        Me.lblLinkedBudgetAccount.Size = New System.Drawing.Size(116, 17)
        Me.lblLinkedBudgetAccount.TabIndex = 24
        Me.lblLinkedBudgetAccount.Text = "حساب مصروف البند"
        '
        'txtLinkedBudgetAccount
        '
        Me.txtLinkedBudgetAccount.BackColor = System.Drawing.Color.White
        Me.txtLinkedBudgetAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLinkedBudgetAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.txtLinkedBudgetAccount.Location = New System.Drawing.Point(896, 137)
        Me.txtLinkedBudgetAccount.Name = "txtLinkedBudgetAccount"
        Me.txtLinkedBudgetAccount.ReadOnly = True
        Me.txtLinkedBudgetAccount.Size = New System.Drawing.Size(351, 24)
        Me.txtLinkedBudgetAccount.TabIndex = 25
        '
        'lblItem
        '
        Me.lblItem.AutoSize = True
        Me.lblItem.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItem.Location = New System.Drawing.Point(1171, 93)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(30, 17)
        Me.lblItem.TabIndex = 8
        Me.lblItem.Text = "البند"
        '
        'lblChapter
        '
        Me.lblChapter.AutoSize = True
        Me.lblChapter.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChapter.Location = New System.Drawing.Point(1171, 65)
        Me.lblChapter.Name = "lblChapter"
        Me.lblChapter.Size = New System.Drawing.Size(44, 17)
        Me.lblChapter.TabIndex = 9
        Me.lblChapter.Text = "الفصل"
        '
        'lblDoor
        '
        Me.lblDoor.AutoSize = True
        Me.lblDoor.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDoor.Location = New System.Drawing.Point(1171, 37)
        Me.lblDoor.Name = "lblDoor"
        Me.lblDoor.Size = New System.Drawing.Size(34, 17)
        Me.lblDoor.TabIndex = 10
        Me.lblDoor.Text = "الباب"
        '
        'lblYear
        '
        Me.lblYear.AutoSize = True
        Me.lblYear.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYear.Location = New System.Drawing.Point(1171, 8)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(74, 17)
        Me.lblYear.TabIndex = 11
        Me.lblYear.Text = "السنة المالية"
        '
        'pnlActions
        '
        Me.pnlActions.BackColor = System.Drawing.Color.White
        Me.pnlActions.Controls.Add(Me.BtnCancelEntry)
        Me.pnlActions.Controls.Add(Me.btnPrintVoucher)
        Me.pnlActions.Controls.Add(Me.btnPrintOfficialVoucher)
        Me.pnlActions.Controls.Add(Me.btnPreviewJournal)
        Me.pnlActions.Controls.Add(Me.btnUpdateSpendStatement)
        Me.pnlActions.Controls.Add(Me.btnEditStamp)
        Me.pnlActions.Controls.Add(Me.BtnApprove)
        Me.pnlActions.Controls.Add(Me.btnExit)
        Me.pnlActions.Controls.Add(Me.btnRefresh)
        Me.pnlActions.Controls.Add(Me.btnExecute)
        Me.pnlActions.Controls.Add(Me.btnNew)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlActions.Location = New System.Drawing.Point(0, 689)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Size = New System.Drawing.Size(1280, 49)
        Me.pnlActions.TabIndex = 1
        '
        'BtnCancelEntry
        '
        Me.BtnCancelEntry.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancelEntry.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancelEntry.Location = New System.Drawing.Point(640, 10)
        Me.BtnCancelEntry.Name = "BtnCancelEntry"
        Me.BtnCancelEntry.Size = New System.Drawing.Size(120, 36)
        Me.BtnCancelEntry.TabIndex = 5
        Me.BtnCancelEntry.Text = "× إلغاء الاعتماد"
        Me.BtnCancelEntry.Visible = False
        '
        'btnPrintVoucher
        '
        Me.btnPrintVoucher.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintVoucher.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintVoucher.Location = New System.Drawing.Point(320, 10)
        Me.btnPrintVoucher.Name = "btnPrintVoucher"
        Me.btnPrintVoucher.Size = New System.Drawing.Size(120, 36)
        Me.btnPrintVoucher.TabIndex = 6
        Me.btnPrintVoucher.Text = "⎙ طباعة الإذن"
        Me.btnPrintVoucher.Visible = False
        '
        'btnPrintOfficialVoucher
        '
        Me.btnPrintOfficialVoucher.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintOfficialVoucher.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintOfficialVoucher.Location = New System.Drawing.Point(516, 10)
        Me.btnPrintOfficialVoucher.Name = "btnPrintOfficialVoucher"
        Me.btnPrintOfficialVoucher.Size = New System.Drawing.Size(120, 36)
        Me.btnPrintOfficialVoucher.TabIndex = 8
        Me.btnPrintOfficialVoucher.Text = "⎙ طباعة رسمية"
        Me.btnPrintOfficialVoucher.Visible = False
        '
        'btnPreviewJournal
        '
        Me.btnPreviewJournal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPreviewJournal.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreviewJournal.Location = New System.Drawing.Point(246, 10)
        Me.btnPreviewJournal.Name = "btnPreviewJournal"
        Me.btnPreviewJournal.Size = New System.Drawing.Size(73, 36)
        Me.btnPreviewJournal.TabIndex = 7
        Me.btnPreviewJournal.Text = "◷ معاينة القيد"
        Me.btnPreviewJournal.Visible = False
        '
        'btnUpdateSpendStatement
        '
        Me.btnUpdateSpendStatement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdateSpendStatement.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateSpendStatement.Location = New System.Drawing.Point(166, 10)
        Me.btnUpdateSpendStatement.Name = "btnUpdateSpendStatement"
        Me.btnUpdateSpendStatement.Size = New System.Drawing.Size(79, 36)
        Me.btnUpdateSpendStatement.TabIndex = 9
        Me.btnUpdateSpendStatement.Text = "✎ تعديل البيان"
        Me.btnUpdateSpendStatement.Visible = False
        '
        'btnEditStamp
        '
        Me.btnEditStamp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditStamp.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditStamp.Location = New System.Drawing.Point(100, 10)
        Me.btnEditStamp.Name = "btnEditStamp"
        Me.btnEditStamp.Size = New System.Drawing.Size(65, 36)
        Me.btnEditStamp.TabIndex = 10
        Me.btnEditStamp.Text = "تعديل الدمغة"
        Me.btnEditStamp.Visible = False
        '
        'BtnApprove
        '
        Me.BtnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnApprove.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnApprove.Location = New System.Drawing.Point(763, 10)
        Me.BtnApprove.Name = "BtnApprove"
        Me.BtnApprove.Size = New System.Drawing.Size(120, 36)
        Me.BtnApprove.TabIndex = 4
        Me.BtnApprove.Text = "✓ إعتماد الصرف"
        Me.BtnApprove.Visible = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(1, 10)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(93, 36)
        Me.btnExit.TabIndex = 0
        Me.btnExit.Text = "⟵ خروج"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Location = New System.Drawing.Point(887, 10)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(120, 36)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "↻ تحديث البيانات"
        '
        'btnExecute
        '
        Me.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExecute.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExecute.Location = New System.Drawing.Point(1011, 10)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(141, 36)
        Me.btnExecute.TabIndex = 2
        Me.btnExecute.Text = "✓ تنفيذ العملية"
        '
        'btnNew
        '
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Location = New System.Drawing.Point(1156, 10)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(120, 36)
        Me.btnNew.TabIndex = 3
        Me.btnNew.Text = "+ عملية جديدة"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 738)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1280, 22)
        Me.StatusStrip1.TabIndex = 3
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(30, 17)
        Me.lblStatus.Text = "جاهز"
        '
        'UcGridColumnsSelector1
        '
        Me.UcGridColumnsSelector1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UcGridColumnsSelector1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.UcGridColumnsSelector1.Location = New System.Drawing.Point(1211, 35)
        Me.UcGridColumnsSelector1.Name = "UcGridColumnsSelector1"
        Me.UcGridColumnsSelector1.PopupMaxHeight = 320
        Me.UcGridColumnsSelector1.PopupMinHeight = 120
        Me.UcGridColumnsSelector1.PopupWidth = 260
        Me.UcGridColumnsSelector1.SettingsFolder = "C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\GridColumnsSettin" &
    "gs"
        Me.UcGridColumnsSelector1.Size = New System.Drawing.Size(36, 27)
        Me.UcGridColumnsSelector1.TabIndex = 30
        '
        'FrmBudgetEntries
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1280, 760)
        Me.Controls.Add(Me.pnlContent)
        Me.Controls.Add(Me.pnlActions)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.MinimumSize = New System.Drawing.Size(1050, 680)
        Me.Name = "FrmBudgetEntries"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "عمليات الموازنة (حجز / صرف)"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlContent.ResumeLayout(False)
        Me.cardGrid.ResumeLayout(False)
        CType(Me.dgvEntries, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlGridFilter.ResumeLayout(False)
        Me.pnlGridFilter.PerformLayout()
        Me.cardDetails.ResumeLayout(False)
        Me.cardDetails.PerformLayout()
        Me.cardForm.ResumeLayout(False)
        Me.cardForm.PerformLayout()
        Me.pnlSummary.ResumeLayout(False)
        Me.pnlActions.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblBudgetOverSpendWarning As Label
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubTitle As Label
    Friend WithEvents lblMode As Label
    Friend WithEvents pnlContent As Panel
    Friend WithEvents cardForm As Panel
    Friend WithEvents cmbFiscalYear As ComboBox
    Friend WithEvents cmbDoors As ComboBox
    Friend WithEvents cmbChapters As ComboBox
    Friend WithEvents cmbItems As ComboBox
    Friend WithEvents txtAmount As TextBox
    Friend WithEvents txtNotes As TextBox
    Friend WithEvents txtSpendStatement As TextBox
    Friend WithEvents lblYear As Label
    Friend WithEvents lblDoor As Label
    Friend WithEvents lblChapter As Label
    Friend WithEvents lblItem As Label
    Friend WithEvents lblAmount As Label
    Friend WithEvents lblNotes As Label
    Friend WithEvents lblSpendStatement As Label
    Friend WithEvents cardGrid As Panel
    Friend WithEvents pnlGridFilter As Panel
    Friend WithEvents lblEntriesFilter As Label
    Friend WithEvents txtEntriesFilter As TextBox
    Friend WithEvents dgvEntries As DataGridView
    Friend WithEvents cardDetails As Panel
    Friend WithEvents lblDetailsTitle As Label
    Friend WithEvents txtSelectedDetails As TextBox
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnExit As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnExecute As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel
    Friend WithEvents pnlSummary As Panel
    Friend WithEvents lblAvailable As Label
    Friend WithEvents lblReserved As Label
    Friend WithEvents lblSpent As Label
    Friend WithEvents lblAllocated As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents BtnApprove As Button
    Friend WithEvents BtnCancelEntry As Button
    Friend WithEvents btnUpdateSpendStatement As Button
    Friend WithEvents lblCommitmentType As Label
    Friend WithEvents cmbCommitmentTypes As ComboBox
    Friend WithEvents lblSourceRef As Label
    Friend WithEvents txtSourceRef As TextBox
    Friend WithEvents lblSourceTable As Label
    Friend WithEvents txtSourceTable As TextBox
    Friend WithEvents btnPrintVoucher As Button
    Friend WithEvents chkHasStamp As CheckBox
    Friend WithEvents lblStampPercent As Label
    Friend WithEvents txtStampPercent As TextBox
    Friend WithEvents txtStampAccountCode As TextBox
    Friend WithEvents txtStampAccountName As TextBox
    Friend WithEvents btnPickStampAccount As Button
    Friend WithEvents lblLinkedBudgetAccount As Label
    Friend WithEvents txtLinkedBudgetAccount As TextBox
    Friend WithEvents btnPreviewJournal As Button
    Friend WithEvents btnPrintOfficialVoucher As Button
    Friend WithEvents lblAmountWords As Label
    Friend WithEvents txtAmountWords As TextBox
    Friend WithEvents dtpEntryDate As DateTimePicker
    Friend WithEvents lblEntryDate As Label
    Friend WithEvents UcGridColumnsSelector1 As UcGridColumnsSelector
    Friend WithEvents btnEditStamp As Button
End Class











'<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
'Partial Class FrmBudgetEntries
'    Inherits System.Windows.Forms.Form

'    Private components As System.ComponentModel.IContainer

'    <System.Diagnostics.DebuggerNonUserCode()>
'    Protected Overrides Sub Dispose(disposing As Boolean)
'        If disposing AndAlso components IsNot Nothing Then
'            components.Dispose()
'        End If
'        MyBase.Dispose(disposing)
'    End Sub

'    <System.Diagnostics.DebuggerNonUserCode()>
'    Private Sub InitializeComponent()
'        Me.pnlHeader = New System.Windows.Forms.Panel()
'        Me.lblMode = New System.Windows.Forms.Label()
'        Me.lblSubTitle = New System.Windows.Forms.Label()
'        Me.lblTitle = New System.Windows.Forms.Label()
'        Me.pnlContent = New System.Windows.Forms.Panel()
'        Me.cardGrid = New System.Windows.Forms.Panel()
'        Me.dgvEntries = New System.Windows.Forms.DataGridView()
'        Me.cardForm = New System.Windows.Forms.Panel()
'        Me.pnlSummary = New System.Windows.Forms.Panel()
'        Me.Label1 = New System.Windows.Forms.Label()
'        Me.Label2 = New System.Windows.Forms.Label()
'        Me.Label3 = New System.Windows.Forms.Label()
'        Me.Label4 = New System.Windows.Forms.Label()
'        Me.lblAvailable = New System.Windows.Forms.Label()
'        Me.lblReserved = New System.Windows.Forms.Label()
'        Me.lblSpent = New System.Windows.Forms.Label()
'        Me.lblAllocated = New System.Windows.Forms.Label()
'        Me.txtNotes = New System.Windows.Forms.TextBox()
'        Me.lblNotes = New System.Windows.Forms.Label()
'        Me.txtAmount = New System.Windows.Forms.TextBox()
'        Me.lblAmount = New System.Windows.Forms.Label()
'        Me.cmbItems = New System.Windows.Forms.ComboBox()
'        Me.cmbChapters = New System.Windows.Forms.ComboBox()
'        Me.cmbDoors = New System.Windows.Forms.ComboBox()
'        Me.cmbFiscalYear = New System.Windows.Forms.ComboBox()
'        Me.lblItem = New System.Windows.Forms.Label()
'        Me.lblChapter = New System.Windows.Forms.Label()
'        Me.lblDoor = New System.Windows.Forms.Label()
'        Me.lblYear = New System.Windows.Forms.Label()
'        Me.pnlActions = New System.Windows.Forms.Panel()
'        Me.BtnCancelEntry = New System.Windows.Forms.Button()
'        Me.BtnApprove = New System.Windows.Forms.Button()
'        Me.btnExit = New System.Windows.Forms.Button()
'        Me.btnRefresh = New System.Windows.Forms.Button()
'        Me.btnExecute = New System.Windows.Forms.Button()
'        Me.btnNew = New System.Windows.Forms.Button()
'        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
'        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
'        Me.pnlHeader.SuspendLayout()
'        Me.pnlContent.SuspendLayout()
'        Me.cardGrid.SuspendLayout()
'        CType(Me.dgvEntries, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.cardForm.SuspendLayout()
'        Me.pnlSummary.SuspendLayout()
'        Me.pnlActions.SuspendLayout()
'        Me.StatusStrip1.SuspendLayout()
'        Me.SuspendLayout()
'        '
'        'pnlHeader
'        '
'        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(45, Byte), Integer))
'        Me.pnlHeader.Controls.Add(Me.lblMode)
'        Me.pnlHeader.Controls.Add(Me.lblSubTitle)
'        Me.pnlHeader.Controls.Add(Me.lblTitle)
'        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
'        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
'        Me.pnlHeader.Name = "pnlHeader"
'        Me.pnlHeader.Size = New System.Drawing.Size(1280, 75)
'        Me.pnlHeader.TabIndex = 2
'        '
'        'lblMode
'        '
'        Me.lblMode.AutoSize = True
'        Me.lblMode.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
'        Me.lblMode.ForeColor = System.Drawing.Color.LightSkyBlue
'        Me.lblMode.Location = New System.Drawing.Point(40, 26)
'        Me.lblMode.Name = "lblMode"
'        Me.lblMode.Size = New System.Drawing.Size(77, 19)
'        Me.lblMode.TabIndex = 0
'        Me.lblMode.Text = "الوضع: حجز"
'        '
'        'lblSubTitle
'        '
'        Me.lblSubTitle.AutoSize = True
'        Me.lblSubTitle.Font = New System.Drawing.Font("Segoe UI", 9.5!)
'        Me.lblSubTitle.ForeColor = System.Drawing.Color.Gainsboro
'        Me.lblSubTitle.Location = New System.Drawing.Point(760, 42)
'        Me.lblSubTitle.Name = "lblSubTitle"
'        Me.lblSubTitle.Size = New System.Drawing.Size(217, 17)
'        Me.lblSubTitle.TabIndex = 1
'        Me.lblSubTitle.Text = "تنفيذ الحجز أو الصرف على بنود الموازنة"
'        '
'        'lblTitle
'        '
'        Me.lblTitle.AutoSize = True
'        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
'        Me.lblTitle.ForeColor = System.Drawing.Color.White
'        Me.lblTitle.Location = New System.Drawing.Point(1030, 10)
'        Me.lblTitle.Name = "lblTitle"
'        Me.lblTitle.Size = New System.Drawing.Size(153, 30)
'        Me.lblTitle.TabIndex = 2
'        Me.lblTitle.Text = "عمليات الموازنة"
'        '
'        'pnlContent
'        '
'        Me.pnlContent.Controls.Add(Me.cardGrid)
'        Me.pnlContent.Controls.Add(Me.cardForm)
'        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
'        Me.pnlContent.Location = New System.Drawing.Point(0, 75)
'        Me.pnlContent.Name = "pnlContent"
'        Me.pnlContent.Padding = New System.Windows.Forms.Padding(15)
'        Me.pnlContent.Size = New System.Drawing.Size(1280, 614)
'        Me.pnlContent.TabIndex = 0
'        '
'        'cardGrid
'        '
'        Me.cardGrid.BackColor = System.Drawing.Color.White
'        Me.cardGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
'        Me.cardGrid.Controls.Add(Me.dgvEntries)
'        Me.cardGrid.Location = New System.Drawing.Point(15, 224)
'        Me.cardGrid.Name = "cardGrid"
'        Me.cardGrid.Size = New System.Drawing.Size(1250, 384)
'        Me.cardGrid.TabIndex = 0
'        '
'        'dgvEntries
'        '
'        Me.dgvEntries.AllowUserToAddRows = False
'        Me.dgvEntries.AllowUserToDeleteRows = False
'        Me.dgvEntries.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
'        Me.dgvEntries.BackgroundColor = System.Drawing.Color.White
'        Me.dgvEntries.BorderStyle = System.Windows.Forms.BorderStyle.None
'        Me.dgvEntries.Dock = System.Windows.Forms.DockStyle.Fill
'        Me.dgvEntries.Location = New System.Drawing.Point(0, 0)
'        Me.dgvEntries.MultiSelect = False
'        Me.dgvEntries.Name = "dgvEntries"
'        Me.dgvEntries.ReadOnly = True
'        Me.dgvEntries.RowHeadersVisible = False
'        Me.dgvEntries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
'        Me.dgvEntries.Size = New System.Drawing.Size(1248, 382)
'        Me.dgvEntries.TabIndex = 0
'        '
'        'cardForm
'        '
'        Me.cardForm.BackColor = System.Drawing.Color.White
'        Me.cardForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
'        Me.cardForm.Controls.Add(Me.pnlSummary)
'        Me.cardForm.Controls.Add(Me.txtNotes)
'        Me.cardForm.Controls.Add(Me.lblNotes)
'        Me.cardForm.Controls.Add(Me.txtAmount)
'        Me.cardForm.Controls.Add(Me.lblAmount)
'        Me.cardForm.Controls.Add(Me.cmbItems)
'        Me.cardForm.Controls.Add(Me.cmbChapters)
'        Me.cardForm.Controls.Add(Me.cmbDoors)
'        Me.cardForm.Controls.Add(Me.cmbFiscalYear)
'        Me.cardForm.Controls.Add(Me.lblItem)
'        Me.cardForm.Controls.Add(Me.lblChapter)
'        Me.cardForm.Controls.Add(Me.lblDoor)
'        Me.cardForm.Controls.Add(Me.lblYear)
'        Me.cardForm.Location = New System.Drawing.Point(15, 2)
'        Me.cardForm.Name = "cardForm"
'        Me.cardForm.Size = New System.Drawing.Size(1250, 220)
'        Me.cardForm.TabIndex = 1
'        '
'        'pnlSummary
'        '
'        Me.pnlSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
'        Me.pnlSummary.Controls.Add(Me.Label1)
'        Me.pnlSummary.Controls.Add(Me.Label2)
'        Me.pnlSummary.Controls.Add(Me.Label3)
'        Me.pnlSummary.Controls.Add(Me.Label4)
'        Me.pnlSummary.Controls.Add(Me.lblAvailable)
'        Me.pnlSummary.Controls.Add(Me.lblReserved)
'        Me.pnlSummary.Controls.Add(Me.lblSpent)
'        Me.pnlSummary.Controls.Add(Me.lblAllocated)
'        Me.pnlSummary.Location = New System.Drawing.Point(4, 4)
'        Me.pnlSummary.Name = "pnlSummary"
'        Me.pnlSummary.Size = New System.Drawing.Size(340, 211)
'        Me.pnlSummary.TabIndex = 12
'        '
'        'Label1
'        '
'        Me.Label1.AutoSize = True
'        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.Label1.ForeColor = System.Drawing.Color.Blue
'        Me.Label1.Location = New System.Drawing.Point(175, 152)
'        Me.Label1.Name = "Label1"
'        Me.Label1.Size = New System.Drawing.Size(38, 17)
'        Me.Label1.TabIndex = 7
'        Me.Label1.Text = "المتاح"
'        '
'        'Label2
'        '
'        Me.Label2.AutoSize = True
'        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
'        Me.Label2.Location = New System.Drawing.Point(175, 101)
'        Me.Label2.Name = "Label2"
'        Me.Label2.Size = New System.Drawing.Size(51, 17)
'        Me.Label2.TabIndex = 6
'        Me.Label2.Text = "المحجوز"
'        '
'        'Label3
'        '
'        Me.Label3.AutoSize = True
'        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.Label3.ForeColor = System.Drawing.Color.DarkRed
'        Me.Label3.Location = New System.Drawing.Point(175, 61)
'        Me.Label3.Name = "Label3"
'        Me.Label3.Size = New System.Drawing.Size(59, 17)
'        Me.Label3.TabIndex = 5
'        Me.Label3.Text = "المصروف"
'        '
'        'Label4
'        '
'        Me.Label4.AutoSize = True
'        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.Label4.ForeColor = System.Drawing.Color.DarkGreen
'        Me.Label4.Location = New System.Drawing.Point(175, 24)
'        Me.Label4.Name = "Label4"
'        Me.Label4.Size = New System.Drawing.Size(48, 17)
'        Me.Label4.TabIndex = 4
'        Me.Label4.Text = "الاعتماد"
'        '
'        'lblAvailable
'        '
'        Me.lblAvailable.AutoSize = True
'        Me.lblAvailable.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.lblAvailable.ForeColor = System.Drawing.Color.Blue
'        Me.lblAvailable.Location = New System.Drawing.Point(52, 152)
'        Me.lblAvailable.Name = "lblAvailable"
'        Me.lblAvailable.Size = New System.Drawing.Size(18, 17)
'        Me.lblAvailable.TabIndex = 3
'        Me.lblAvailable.Text = "--"
'        '
'        'lblReserved
'        '
'        Me.lblReserved.AutoSize = True
'        Me.lblReserved.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.lblReserved.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
'        Me.lblReserved.Location = New System.Drawing.Point(52, 101)
'        Me.lblReserved.Name = "lblReserved"
'        Me.lblReserved.Size = New System.Drawing.Size(18, 17)
'        Me.lblReserved.TabIndex = 2
'        Me.lblReserved.Text = "--"
'        '
'        'lblSpent
'        '
'        Me.lblSpent.AutoSize = True
'        Me.lblSpent.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.lblSpent.ForeColor = System.Drawing.Color.DarkRed
'        Me.lblSpent.Location = New System.Drawing.Point(52, 61)
'        Me.lblSpent.Name = "lblSpent"
'        Me.lblSpent.Size = New System.Drawing.Size(18, 17)
'        Me.lblSpent.TabIndex = 1
'        Me.lblSpent.Text = "--"
'        '
'        'lblAllocated
'        '
'        Me.lblAllocated.AutoSize = True
'        Me.lblAllocated.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.lblAllocated.ForeColor = System.Drawing.Color.DarkGreen
'        Me.lblAllocated.Location = New System.Drawing.Point(52, 24)
'        Me.lblAllocated.Name = "lblAllocated"
'        Me.lblAllocated.Size = New System.Drawing.Size(18, 17)
'        Me.lblAllocated.TabIndex = 0
'        Me.lblAllocated.Text = "--"
'        '
'        'txtNotes
'        '
'        Me.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
'        Me.txtNotes.Location = New System.Drawing.Point(350, 58)
'        Me.txtNotes.Multiline = True
'        Me.txtNotes.Name = "txtNotes"
'        Me.txtNotes.Size = New System.Drawing.Size(230, 80)
'        Me.txtNotes.TabIndex = 0
'        '
'        'lblNotes
'        '
'        Me.lblNotes.AutoSize = True
'        Me.lblNotes.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
'        Me.lblNotes.Location = New System.Drawing.Point(584, 62)
'        Me.lblNotes.Name = "lblNotes"
'        Me.lblNotes.Size = New System.Drawing.Size(63, 19)
'        Me.lblNotes.TabIndex = 1
'        Me.lblNotes.Text = "ملاحظات"
'        '
'        'txtAmount
'        '
'        Me.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
'        Me.txtAmount.Location = New System.Drawing.Point(350, 18)
'        Me.txtAmount.Name = "txtAmount"
'        Me.txtAmount.Size = New System.Drawing.Size(230, 25)
'        Me.txtAmount.TabIndex = 2
'        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
'        '
'        'lblAmount
'        '
'        Me.lblAmount.AutoSize = True
'        Me.lblAmount.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
'        Me.lblAmount.Location = New System.Drawing.Point(584, 22)
'        Me.lblAmount.Name = "lblAmount"
'        Me.lblAmount.Size = New System.Drawing.Size(44, 19)
'        Me.lblAmount.TabIndex = 3
'        Me.lblAmount.Text = "المبلغ"
'        '
'        'cmbItems
'        '
'        Me.cmbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
'        Me.cmbItems.Location = New System.Drawing.Point(900, 138)
'        Me.cmbItems.Name = "cmbItems"
'        Me.cmbItems.Size = New System.Drawing.Size(230, 25)
'        Me.cmbItems.TabIndex = 4
'        '
'        'cmbChapters
'        '
'        Me.cmbChapters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
'        Me.cmbChapters.Location = New System.Drawing.Point(900, 98)
'        Me.cmbChapters.Name = "cmbChapters"
'        Me.cmbChapters.Size = New System.Drawing.Size(230, 25)
'        Me.cmbChapters.TabIndex = 5
'        '
'        'cmbDoors
'        '
'        Me.cmbDoors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
'        Me.cmbDoors.Location = New System.Drawing.Point(900, 58)
'        Me.cmbDoors.Name = "cmbDoors"
'        Me.cmbDoors.Size = New System.Drawing.Size(230, 25)
'        Me.cmbDoors.TabIndex = 6
'        '
'        'cmbFiscalYear
'        '
'        Me.cmbFiscalYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
'        Me.cmbFiscalYear.Location = New System.Drawing.Point(900, 18)
'        Me.cmbFiscalYear.Name = "cmbFiscalYear"
'        Me.cmbFiscalYear.Size = New System.Drawing.Size(230, 25)
'        Me.cmbFiscalYear.TabIndex = 7
'        '
'        'lblItem
'        '
'        Me.lblItem.AutoSize = True
'        Me.lblItem.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
'        Me.lblItem.Location = New System.Drawing.Point(1150, 140)
'        Me.lblItem.Name = "lblItem"
'        Me.lblItem.Size = New System.Drawing.Size(35, 19)
'        Me.lblItem.TabIndex = 8
'        Me.lblItem.Text = "البند"
'        '
'        'lblChapter
'        '
'        Me.lblChapter.AutoSize = True
'        Me.lblChapter.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
'        Me.lblChapter.Location = New System.Drawing.Point(1150, 100)
'        Me.lblChapter.Name = "lblChapter"
'        Me.lblChapter.Size = New System.Drawing.Size(48, 19)
'        Me.lblChapter.TabIndex = 9
'        Me.lblChapter.Text = "الفصل"
'        '
'        'lblDoor
'        '
'        Me.lblDoor.AutoSize = True
'        Me.lblDoor.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
'        Me.lblDoor.Location = New System.Drawing.Point(1150, 60)
'        Me.lblDoor.Name = "lblDoor"
'        Me.lblDoor.Size = New System.Drawing.Size(40, 19)
'        Me.lblDoor.TabIndex = 10
'        Me.lblDoor.Text = "الباب"
'        '
'        'lblYear
'        '
'        Me.lblYear.AutoSize = True
'        Me.lblYear.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
'        Me.lblYear.Location = New System.Drawing.Point(1150, 20)
'        Me.lblYear.Name = "lblYear"
'        Me.lblYear.Size = New System.Drawing.Size(84, 19)
'        Me.lblYear.TabIndex = 11
'        Me.lblYear.Text = "السنة المالية"
'        '
'        'pnlActions
'        '
'        Me.pnlActions.BackColor = System.Drawing.Color.White
'        Me.pnlActions.Controls.Add(Me.BtnCancelEntry)
'        Me.pnlActions.Controls.Add(Me.BtnApprove)
'        Me.pnlActions.Controls.Add(Me.btnExit)
'        Me.pnlActions.Controls.Add(Me.btnRefresh)
'        Me.pnlActions.Controls.Add(Me.btnExecute)
'        Me.pnlActions.Controls.Add(Me.btnNew)
'        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom
'        Me.pnlActions.Location = New System.Drawing.Point(0, 689)
'        Me.pnlActions.Name = "pnlActions"
'        Me.pnlActions.Size = New System.Drawing.Size(1280, 49)
'        Me.pnlActions.TabIndex = 1
'        '
'        'BtnCancelEntry
'        '
'        Me.BtnCancelEntry.FlatStyle = System.Windows.Forms.FlatStyle.Flat
'        Me.BtnCancelEntry.Location = New System.Drawing.Point(661, 10)
'        Me.BtnCancelEntry.Name = "BtnCancelEntry"
'        Me.BtnCancelEntry.Size = New System.Drawing.Size(110, 36)
'        Me.BtnCancelEntry.TabIndex = 5
'        Me.BtnCancelEntry.Text = "إلغاء الإعتماد"
'        Me.BtnCancelEntry.Visible = False
'        '
'        'BtnApprove
'        '
'        Me.BtnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
'        Me.BtnApprove.Location = New System.Drawing.Point(790, 10)
'        Me.BtnApprove.Name = "BtnApprove"
'        Me.BtnApprove.Size = New System.Drawing.Size(110, 36)
'        Me.BtnApprove.TabIndex = 4
'        Me.BtnApprove.Text = "إعتماد"
'        Me.BtnApprove.Visible = False
'        '
'        'btnExit
'        '
'        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
'        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
'        Me.btnExit.ForeColor = System.Drawing.Color.White
'        Me.btnExit.Location = New System.Drawing.Point(16, 10)
'        Me.btnExit.Name = "btnExit"
'        Me.btnExit.Size = New System.Drawing.Size(110, 36)
'        Me.btnExit.TabIndex = 0
'        Me.btnExit.Text = "خروج"
'        Me.btnExit.UseVisualStyleBackColor = False
'        '
'        'btnRefresh
'        '
'        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
'        Me.btnRefresh.Location = New System.Drawing.Point(906, 10)
'        Me.btnRefresh.Name = "btnRefresh"
'        Me.btnRefresh.Size = New System.Drawing.Size(110, 36)
'        Me.btnRefresh.TabIndex = 1
'        Me.btnRefresh.Text = "تحديث"
'        '
'        'btnExecute
'        '
'        Me.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat
'        Me.btnExecute.Location = New System.Drawing.Point(1026, 10)
'        Me.btnExecute.Name = "btnExecute"
'        Me.btnExecute.Size = New System.Drawing.Size(110, 36)
'        Me.btnExecute.TabIndex = 2
'        Me.btnExecute.Text = "تنفيذ"
'        '
'        'btnNew
'        '
'        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
'        Me.btnNew.Location = New System.Drawing.Point(1146, 10)
'        Me.btnNew.Name = "btnNew"
'        Me.btnNew.Size = New System.Drawing.Size(110, 36)
'        Me.btnNew.TabIndex = 3
'        Me.btnNew.Text = "جديد"
'        '
'        'StatusStrip1
'        '
'        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
'        Me.StatusStrip1.Location = New System.Drawing.Point(0, 738)
'        Me.StatusStrip1.Name = "StatusStrip1"
'        Me.StatusStrip1.Size = New System.Drawing.Size(1280, 22)
'        Me.StatusStrip1.TabIndex = 3
'        '
'        'lblStatus
'        '
'        Me.lblStatus.Name = "lblStatus"
'        Me.lblStatus.Size = New System.Drawing.Size(30, 17)
'        Me.lblStatus.Text = "جاهز"
'        '
'        'FrmBudgetEntries
'        '
'        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
'        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
'        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
'        Me.ClientSize = New System.Drawing.Size(1280, 760)
'        Me.Controls.Add(Me.pnlContent)
'        Me.Controls.Add(Me.pnlActions)
'        Me.Controls.Add(Me.pnlHeader)
'        Me.Controls.Add(Me.StatusStrip1)
'        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!)
'        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
'        Me.Name = "FrmBudgetEntries"
'        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
'        Me.RightToLeftLayout = True
'        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
'        Me.Text = "عمليات الموازنة (حجز / صرف)"
'        Me.pnlHeader.ResumeLayout(False)
'        Me.pnlHeader.PerformLayout()
'        Me.pnlContent.ResumeLayout(False)
'        Me.cardGrid.ResumeLayout(False)
'        CType(Me.dgvEntries, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.cardForm.ResumeLayout(False)
'        Me.cardForm.PerformLayout()
'        Me.pnlSummary.ResumeLayout(False)
'        Me.pnlSummary.PerformLayout()
'        Me.pnlActions.ResumeLayout(False)
'        Me.StatusStrip1.ResumeLayout(False)
'        Me.StatusStrip1.PerformLayout()
'        Me.ResumeLayout(False)
'        Me.PerformLayout()

'    End Sub

'    Friend WithEvents pnlHeader As Panel
'    Friend WithEvents lblTitle As Label
'    Friend WithEvents lblSubTitle As Label
'    Friend WithEvents lblMode As Label
'    Friend WithEvents pnlContent As Panel
'    Friend WithEvents cardForm As Panel
'    Friend WithEvents cmbFiscalYear As ComboBox
'    Friend WithEvents cmbDoors As ComboBox
'    Friend WithEvents cmbChapters As ComboBox
'    Friend WithEvents cmbItems As ComboBox
'    Friend WithEvents txtAmount As TextBox
'    Friend WithEvents txtNotes As TextBox
'    Friend WithEvents lblYear As Label
'    Friend WithEvents lblDoor As Label
'    Friend WithEvents lblChapter As Label
'    Friend WithEvents lblItem As Label
'    Friend WithEvents lblAmount As Label
'    Friend WithEvents lblNotes As Label
'    Friend WithEvents cardGrid As Panel
'    Friend WithEvents dgvEntries As DataGridView
'    Friend WithEvents pnlActions As Panel
'    Friend WithEvents btnExit As Button
'    Friend WithEvents btnRefresh As Button
'    Friend WithEvents btnExecute As Button
'    Friend WithEvents btnNew As Button
'    Friend WithEvents StatusStrip1 As StatusStrip
'    Friend WithEvents lblStatus As ToolStripStatusLabel
'    Friend WithEvents pnlSummary As Panel
'    Friend WithEvents lblAvailable As Label
'    Friend WithEvents lblReserved As Label
'    Friend WithEvents lblSpent As Label
'    Friend WithEvents lblAllocated As Label
'    Friend WithEvents Label1 As Label
'    Friend WithEvents Label2 As Label
'    Friend WithEvents Label3 As Label
'    Friend WithEvents Label4 As Label
'    Friend WithEvents BtnApprove As Button
'    Friend WithEvents BtnCancelEntry As Button
'End Class
