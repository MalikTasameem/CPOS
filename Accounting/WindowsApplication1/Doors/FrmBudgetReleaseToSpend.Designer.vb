<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBudgetReleaseToSpend
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
        Me.lblSubTitle = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlContent = New System.Windows.Forms.Panel()
        Me.cardGrid = New System.Windows.Forms.Panel()
        Me.dgvTimeline = New System.Windows.Forms.DataGridView()
        Me.dgvReserves = New System.Windows.Forms.DataGridView()
        Me.cardForm = New System.Windows.Forms.Panel()
        Me.lblAmountWords = New System.Windows.Forms.Label()
        Me.txtAmountWords = New System.Windows.Forms.TextBox()
        Me.lblBeneficiaryType = New System.Windows.Forms.Label()
        Me.cmbBeneficiaryType = New System.Windows.Forms.ComboBox()
        Me.lblPaymentMethod = New System.Windows.Forms.Label()
        Me.cmbPaymentMethod = New System.Windows.Forms.ComboBox()
        Me.lblContraAccount = New System.Windows.Forms.Label()
        Me.txtContraAccountCode = New System.Windows.Forms.TextBox()
        Me.txtContraAccountName = New System.Windows.Forms.TextBox()
        Me.btnPickContraAccount = New System.Windows.Forms.Button()
        Me.chkHasStamp = New System.Windows.Forms.CheckBox()
        Me.lblStampPercent = New System.Windows.Forms.Label()
        Me.txtStampPercent = New System.Windows.Forms.TextBox()
        Me.txtStampAccountCode = New System.Windows.Forms.TextBox()
        Me.txtStampAccountName = New System.Windows.Forms.TextBox()
        Me.btnPickStampAccount = New System.Windows.Forms.Button()
        Me.lblInvoiceNo = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.lblDocumentNo = New System.Windows.Forms.Label()
        Me.txtDocumentNo = New System.Windows.Forms.TextBox()
        Me.lblSpendStatement = New System.Windows.Forms.Label()
        Me.txtSpendStatement = New System.Windows.Forms.TextBox()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.cmbFiscalYear = New System.Windows.Forms.ComboBox()
        Me.lblDoor = New System.Windows.Forms.Label()
        Me.cmbDoors = New System.Windows.Forms.ComboBox()
        Me.lblChapter = New System.Windows.Forms.Label()
        Me.cmbChapters = New System.Windows.Forms.ComboBox()
        Me.lblItem = New System.Windows.Forms.Label()
        Me.cmbItems = New System.Windows.Forms.ComboBox()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.lblNotes = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.cardSummary = New System.Windows.Forms.Panel()
        Me.lblAllocated = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblSpent = New System.Windows.Forms.Label()
        Me.lblReserved = New System.Windows.Forms.Label()
        Me.lblAvailable = New System.Windows.Forms.Label()
        Me.pnlActions = New System.Windows.Forms.Panel()
        Me.btnConvert = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlHeader.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.cardGrid.SuspendLayout()
        CType(Me.dgvTimeline, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvReserves, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cardForm.SuspendLayout()
        Me.cardSummary.SuspendLayout()
        Me.pnlActions.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblBudgetOverSpendWarning)
        Me.pnlHeader.Controls.Add(Me.lblSubTitle)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1300, 96)
        Me.pnlHeader.TabIndex = 2
        '
        'lblBudgetOverSpendWarning
        '
        Me.lblBudgetOverSpendWarning.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.lblBudgetOverSpendWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBudgetOverSpendWarning.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblBudgetOverSpendWarning.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblBudgetOverSpendWarning.ForeColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(77, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.lblBudgetOverSpendWarning.Location = New System.Drawing.Point(0, 72)
        Me.lblBudgetOverSpendWarning.Name = "lblBudgetOverSpendWarning"
        Me.lblBudgetOverSpendWarning.Size = New System.Drawing.Size(1300, 24)
        Me.lblBudgetOverSpendWarning.TabIndex = 2
        Me.lblBudgetOverSpendWarning.Text = "تنبيه: النظام يعمل حاليًا بسماحية تنفيذ عمليات الموازنة عند عدم كفاية الاعتماد"
        Me.lblBudgetOverSpendWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblBudgetOverSpendWarning.Visible = False
        '
        'lblSubTitle
        '
        Me.lblSubTitle.AutoSize = True
        Me.lblSubTitle.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblSubTitle.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblSubTitle.Location = New System.Drawing.Point(720, 42)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Size = New System.Drawing.Size(297, 17)
        Me.lblSubTitle.TabIndex = 0
        Me.lblSubTitle.Text = "فك الحجز وتنفيذ الصرف المقابل على نفس بند الموازنة"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(1040, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(214, 30)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "تحويل الحجز إلى صرف"
        '
        'pnlContent
        '
        Me.pnlContent.Controls.Add(Me.cardGrid)
        Me.pnlContent.Controls.Add(Me.cardForm)
        Me.pnlContent.Controls.Add(Me.cardSummary)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 96)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlContent.Size = New System.Drawing.Size(1300, 619)
        Me.pnlContent.TabIndex = 0
        '
        'cardGrid
        '
        Me.cardGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cardGrid.BackColor = System.Drawing.Color.White
        Me.cardGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardGrid.Controls.Add(Me.dgvTimeline)
        Me.cardGrid.Controls.Add(Me.dgvReserves)
        Me.cardGrid.Location = New System.Drawing.Point(15, 227)
        Me.cardGrid.Name = "cardGrid"
        Me.cardGrid.Size = New System.Drawing.Size(1270, 416)
        Me.cardGrid.TabIndex = 0
        '
        'dgvTimeline
        '
        Me.dgvTimeline.AllowUserToAddRows = False
        Me.dgvTimeline.AllowUserToDeleteRows = False
        Me.dgvTimeline.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTimeline.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTimeline.BackgroundColor = System.Drawing.SystemColors.Info
        Me.dgvTimeline.Location = New System.Drawing.Point(2, 215)
        Me.dgvTimeline.Name = "dgvTimeline"
        Me.dgvTimeline.ReadOnly = True
        Me.dgvTimeline.RowHeadersVisible = False
        Me.dgvTimeline.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTimeline.Size = New System.Drawing.Size(1264, 198)
        Me.dgvTimeline.TabIndex = 1
        '
        'dgvReserves
        '
        Me.dgvReserves.AllowUserToAddRows = False
        Me.dgvReserves.AllowUserToDeleteRows = False
        Me.dgvReserves.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvReserves.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvReserves.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.dgvReserves.Location = New System.Drawing.Point(-1, 3)
        Me.dgvReserves.Name = "dgvReserves"
        Me.dgvReserves.ReadOnly = True
        Me.dgvReserves.RowHeadersVisible = False
        Me.dgvReserves.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvReserves.Size = New System.Drawing.Size(1264, 206)
        Me.dgvReserves.TabIndex = 0
        '
        'cardForm
        '
        Me.cardForm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cardForm.BackColor = System.Drawing.Color.FloralWhite
        Me.cardForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardForm.Controls.Add(Me.lblAmountWords)
        Me.cardForm.Controls.Add(Me.txtAmountWords)
        Me.cardForm.Controls.Add(Me.lblBeneficiaryType)
        Me.cardForm.Controls.Add(Me.cmbBeneficiaryType)
        Me.cardForm.Controls.Add(Me.lblPaymentMethod)
        Me.cardForm.Controls.Add(Me.cmbPaymentMethod)
        Me.cardForm.Controls.Add(Me.lblContraAccount)
        Me.cardForm.Controls.Add(Me.txtContraAccountCode)
        Me.cardForm.Controls.Add(Me.txtContraAccountName)
        Me.cardForm.Controls.Add(Me.btnPickContraAccount)
        Me.cardForm.Controls.Add(Me.chkHasStamp)
        Me.cardForm.Controls.Add(Me.lblStampPercent)
        Me.cardForm.Controls.Add(Me.txtStampPercent)
        Me.cardForm.Controls.Add(Me.txtStampAccountCode)
        Me.cardForm.Controls.Add(Me.txtStampAccountName)
        Me.cardForm.Controls.Add(Me.btnPickStampAccount)
        Me.cardForm.Controls.Add(Me.lblInvoiceNo)
        Me.cardForm.Controls.Add(Me.txtInvoiceNo)
        Me.cardForm.Controls.Add(Me.lblDocumentNo)
        Me.cardForm.Controls.Add(Me.txtDocumentNo)
        Me.cardForm.Controls.Add(Me.lblSpendStatement)
        Me.cardForm.Controls.Add(Me.txtSpendStatement)
        Me.cardForm.Controls.Add(Me.lblYear)
        Me.cardForm.Controls.Add(Me.cmbFiscalYear)
        Me.cardForm.Controls.Add(Me.lblDoor)
        Me.cardForm.Controls.Add(Me.cmbDoors)
        Me.cardForm.Controls.Add(Me.lblChapter)
        Me.cardForm.Controls.Add(Me.cmbChapters)
        Me.cardForm.Controls.Add(Me.lblItem)
        Me.cardForm.Controls.Add(Me.cmbItems)
        Me.cardForm.Controls.Add(Me.lblAmount)
        Me.cardForm.Controls.Add(Me.txtAmount)
        Me.cardForm.Controls.Add(Me.lblNotes)
        Me.cardForm.Controls.Add(Me.txtNotes)
        Me.cardForm.Location = New System.Drawing.Point(15, 64)
        Me.cardForm.Name = "cardForm"
        Me.cardForm.Size = New System.Drawing.Size(1270, 161)
        Me.cardForm.TabIndex = 1
        '
        'lblAmountWords
        '
        Me.lblAmountWords.AutoSize = True
        Me.lblAmountWords.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmountWords.Location = New System.Drawing.Point(585, 137)
        Me.lblAmountWords.Name = "lblAmountWords"
        Me.lblAmountWords.Size = New System.Drawing.Size(86, 17)
        Me.lblAmountWords.TabIndex = 25
        Me.lblAmountWords.Text = "المبلغ بالحروف"
        '
        'txtAmountWords
        '
        Me.txtAmountWords.BackColor = System.Drawing.Color.White
        Me.txtAmountWords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmountWords.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmountWords.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.txtAmountWords.Location = New System.Drawing.Point(2, 135)
        Me.txtAmountWords.Name = "txtAmountWords"
        Me.txtAmountWords.ReadOnly = True
        Me.txtAmountWords.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtAmountWords.Size = New System.Drawing.Size(579, 23)
        Me.txtAmountWords.TabIndex = 26
        '
        'lblBeneficiaryType
        '
        Me.lblBeneficiaryType.AutoSize = True
        Me.lblBeneficiaryType.Location = New System.Drawing.Point(773, 33)
        Me.lblBeneficiaryType.Name = "lblBeneficiaryType"
        Me.lblBeneficiaryType.Size = New System.Drawing.Size(79, 17)
        Me.lblBeneficiaryType.TabIndex = 12
        Me.lblBeneficiaryType.Text = "نوع المستفيد"
        Me.lblBeneficiaryType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbBeneficiaryType
        '
        Me.cmbBeneficiaryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBeneficiaryType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbBeneficiaryType.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.cmbBeneficiaryType.Location = New System.Drawing.Point(649, 32)
        Me.cmbBeneficiaryType.Name = "cmbBeneficiaryType"
        Me.cmbBeneficiaryType.Size = New System.Drawing.Size(121, 25)
        Me.cmbBeneficiaryType.TabIndex = 13
        '
        'lblPaymentMethod
        '
        Me.lblPaymentMethod.AutoSize = True
        Me.lblPaymentMethod.Location = New System.Drawing.Point(503, 9)
        Me.lblPaymentMethod.Name = "lblPaymentMethod"
        Me.lblPaymentMethod.Size = New System.Drawing.Size(76, 17)
        Me.lblPaymentMethod.TabIndex = 14
        Me.lblPaymentMethod.Text = "طريقة الدفع"
        Me.lblPaymentMethod.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbPaymentMethod
        '
        Me.cmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPaymentMethod.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbPaymentMethod.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.cmbPaymentMethod.Location = New System.Drawing.Point(309, 4)
        Me.cmbPaymentMethod.Name = "cmbPaymentMethod"
        Me.cmbPaymentMethod.Size = New System.Drawing.Size(189, 25)
        Me.cmbPaymentMethod.TabIndex = 15
        '
        'lblContraAccount
        '
        Me.lblContraAccount.AutoSize = True
        Me.lblContraAccount.Location = New System.Drawing.Point(545, 35)
        Me.lblContraAccount.Name = "lblContraAccount"
        Me.lblContraAccount.Size = New System.Drawing.Size(91, 17)
        Me.lblContraAccount.TabIndex = 16
        Me.lblContraAccount.Text = "الحساب المقابل"
        Me.lblContraAccount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtContraAccountCode
        '
        Me.txtContraAccountCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtContraAccountCode.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.txtContraAccountCode.Location = New System.Drawing.Point(307, 32)
        Me.txtContraAccountCode.Name = "txtContraAccountCode"
        Me.txtContraAccountCode.Size = New System.Drawing.Size(208, 24)
        Me.txtContraAccountCode.TabIndex = 17
        Me.txtContraAccountCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtContraAccountName
        '
        Me.txtContraAccountName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtContraAccountName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtContraAccountName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.txtContraAccountName.Location = New System.Drawing.Point(307, 58)
        Me.txtContraAccountName.Name = "txtContraAccountName"
        Me.txtContraAccountName.ReadOnly = True
        Me.txtContraAccountName.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtContraAccountName.Size = New System.Drawing.Size(236, 24)
        Me.txtContraAccountName.TabIndex = 27
        Me.txtContraAccountName.TabStop = False
        '
        'btnPickContraAccount
        '
        Me.btnPickContraAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPickContraAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnPickContraAccount.Location = New System.Drawing.Point(516, 32)
        Me.btnPickContraAccount.Name = "btnPickContraAccount"
        Me.btnPickContraAccount.Size = New System.Drawing.Size(26, 24)
        Me.btnPickContraAccount.TabIndex = 18
        Me.btnPickContraAccount.Text = "..."
        Me.btnPickContraAccount.UseVisualStyleBackColor = True
        '
        'chkHasStamp
        '
        Me.chkHasStamp.AutoSize = True
        Me.chkHasStamp.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.chkHasStamp.Location = New System.Drawing.Point(573, 84)
        Me.chkHasStamp.Name = "chkHasStamp"
        Me.chkHasStamp.Size = New System.Drawing.Size(87, 21)
        Me.chkHasStamp.TabIndex = 19
        Me.chkHasStamp.Text = "توجد دمغة"
        Me.chkHasStamp.UseVisualStyleBackColor = True
        '
        'lblStampPercent
        '
        Me.lblStampPercent.AutoSize = True
        Me.lblStampPercent.Location = New System.Drawing.Point(353, 87)
        Me.lblStampPercent.Name = "lblStampPercent"
        Me.lblStampPercent.Size = New System.Drawing.Size(86, 17)
        Me.lblStampPercent.TabIndex = 20
        Me.lblStampPercent.Text = "نسبة الدمغة%"
        Me.lblStampPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtStampPercent
        '
        Me.txtStampPercent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStampPercent.Enabled = False
        Me.txtStampPercent.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtStampPercent.Location = New System.Drawing.Point(307, 84)
        Me.txtStampPercent.Name = "txtStampPercent"
        Me.txtStampPercent.Size = New System.Drawing.Size(43, 23)
        Me.txtStampPercent.TabIndex = 21
        Me.txtStampPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtStampAccountCode
        '
        Me.txtStampAccountCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStampAccountCode.Enabled = False
        Me.txtStampAccountCode.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtStampAccountCode.Location = New System.Drawing.Point(441, 84)
        Me.txtStampAccountCode.Name = "txtStampAccountCode"
        Me.txtStampAccountCode.Size = New System.Drawing.Size(102, 23)
        Me.txtStampAccountCode.TabIndex = 23
        Me.txtStampAccountCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtStampAccountName
        '
        Me.txtStampAccountName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtStampAccountName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStampAccountName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.txtStampAccountName.Location = New System.Drawing.Point(307, 109)
        Me.txtStampAccountName.Name = "txtStampAccountName"
        Me.txtStampAccountName.ReadOnly = True
        Me.txtStampAccountName.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtStampAccountName.Size = New System.Drawing.Size(235, 24)
        Me.txtStampAccountName.TabIndex = 28
        Me.txtStampAccountName.TabStop = False
        '
        'btnPickStampAccount
        '
        Me.btnPickStampAccount.Enabled = False
        Me.btnPickStampAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPickStampAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnPickStampAccount.Location = New System.Drawing.Point(544, 84)
        Me.btnPickStampAccount.Name = "btnPickStampAccount"
        Me.btnPickStampAccount.Size = New System.Drawing.Size(26, 23)
        Me.btnPickStampAccount.TabIndex = 24
        Me.btnPickStampAccount.Text = "؟"
        Me.btnPickStampAccount.UseVisualStyleBackColor = True
        '
        'lblInvoiceNo
        '
        Me.lblInvoiceNo.AutoSize = True
        Me.lblInvoiceNo.Location = New System.Drawing.Point(233, 6)
        Me.lblInvoiceNo.Name = "lblInvoiceNo"
        Me.lblInvoiceNo.Size = New System.Drawing.Size(70, 17)
        Me.lblInvoiceNo.TabIndex = 18
        Me.lblInvoiceNo.Text = "رقم الفاتورة"
        Me.lblInvoiceNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.txtInvoiceNo.Location = New System.Drawing.Point(4, 2)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(225, 24)
        Me.txtInvoiceNo.TabIndex = 19
        Me.txtInvoiceNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDocumentNo
        '
        Me.lblDocumentNo.AutoSize = True
        Me.lblDocumentNo.Location = New System.Drawing.Point(233, 34)
        Me.lblDocumentNo.Name = "lblDocumentNo"
        Me.lblDocumentNo.Size = New System.Drawing.Size(72, 17)
        Me.lblDocumentNo.TabIndex = 20
        Me.lblDocumentNo.Text = "رقم المستند"
        Me.lblDocumentNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDocumentNo
        '
        Me.txtDocumentNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocumentNo.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.txtDocumentNo.Location = New System.Drawing.Point(4, 30)
        Me.txtDocumentNo.Name = "txtDocumentNo"
        Me.txtDocumentNo.Size = New System.Drawing.Size(225, 24)
        Me.txtDocumentNo.TabIndex = 21
        Me.txtDocumentNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSpendStatement
        '
        Me.lblSpendStatement.AutoSize = True
        Me.lblSpendStatement.Location = New System.Drawing.Point(82, 57)
        Me.lblSpendStatement.Name = "lblSpendStatement"
        Me.lblSpendStatement.Size = New System.Drawing.Size(144, 17)
        Me.lblSpendStatement.TabIndex = 22
        Me.lblSpendStatement.Text = "بيان الصرف (يطبع فالإذن)"
        Me.lblSpendStatement.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSpendStatement
        '
        Me.txtSpendStatement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpendStatement.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.txtSpendStatement.Location = New System.Drawing.Point(2, 77)
        Me.txtSpendStatement.Multiline = True
        Me.txtSpendStatement.Name = "txtSpendStatement"
        Me.txtSpendStatement.Size = New System.Drawing.Size(227, 57)
        Me.txtSpendStatement.TabIndex = 23
        '
        'lblYear
        '
        Me.lblYear.AutoSize = True
        Me.lblYear.Location = New System.Drawing.Point(1181, 4)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(74, 17)
        Me.lblYear.TabIndex = 0
        Me.lblYear.Text = "السنة المالية"
        Me.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbFiscalYear
        '
        Me.cmbFiscalYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFiscalYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbFiscalYear.Location = New System.Drawing.Point(1064, 3)
        Me.cmbFiscalYear.Name = "cmbFiscalYear"
        Me.cmbFiscalYear.Size = New System.Drawing.Size(114, 25)
        Me.cmbFiscalYear.TabIndex = 1
        '
        'lblDoor
        '
        Me.lblDoor.AutoSize = True
        Me.lblDoor.Location = New System.Drawing.Point(1186, 33)
        Me.lblDoor.Name = "lblDoor"
        Me.lblDoor.Size = New System.Drawing.Size(34, 17)
        Me.lblDoor.TabIndex = 2
        Me.lblDoor.Text = "الباب"
        Me.lblDoor.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbDoors
        '
        Me.cmbDoors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoors.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbDoors.Location = New System.Drawing.Point(858, 32)
        Me.cmbDoors.Name = "cmbDoors"
        Me.cmbDoors.Size = New System.Drawing.Size(325, 25)
        Me.cmbDoors.TabIndex = 3
        '
        'lblChapter
        '
        Me.lblChapter.AutoSize = True
        Me.lblChapter.Location = New System.Drawing.Point(1186, 63)
        Me.lblChapter.Name = "lblChapter"
        Me.lblChapter.Size = New System.Drawing.Size(44, 17)
        Me.lblChapter.TabIndex = 4
        Me.lblChapter.Text = "الفصل"
        Me.lblChapter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbChapters
        '
        Me.cmbChapters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChapters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbChapters.Location = New System.Drawing.Point(858, 61)
        Me.cmbChapters.Name = "cmbChapters"
        Me.cmbChapters.Size = New System.Drawing.Size(325, 25)
        Me.cmbChapters.TabIndex = 5
        '
        'lblItem
        '
        Me.lblItem.AutoSize = True
        Me.lblItem.Location = New System.Drawing.Point(1186, 90)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(30, 17)
        Me.lblItem.TabIndex = 6
        Me.lblItem.Text = "البند"
        Me.lblItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbItems
        '
        Me.cmbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbItems.Location = New System.Drawing.Point(858, 89)
        Me.cmbItems.Name = "cmbItems"
        Me.cmbItems.Size = New System.Drawing.Size(325, 25)
        Me.cmbItems.TabIndex = 7
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.Location = New System.Drawing.Point(766, 5)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(150, 17)
        Me.lblAmount.TabIndex = 8
        Me.lblAmount.Text = "القيمة المصروفة من الحجز"
        Me.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAmount
        '
        Me.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.Location = New System.Drawing.Point(649, 2)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(114, 25)
        Me.txtAmount.TabIndex = 9
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblNotes
        '
        Me.lblNotes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNotes.AutoSize = True
        Me.lblNotes.Location = New System.Drawing.Point(1215, 139)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(49, 17)
        Me.lblNotes.TabIndex = 10
        Me.lblNotes.Text = "ملاحظة"
        Me.lblNotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNotes
        '
        Me.txtNotes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNotes.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtNotes.Location = New System.Drawing.Point(745, 136)
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(466, 23)
        Me.txtNotes.TabIndex = 11
        '
        'cardSummary
        '
        Me.cardSummary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cardSummary.BackColor = System.Drawing.Color.White
        Me.cardSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardSummary.Controls.Add(Me.lblAllocated)
        Me.cardSummary.Controls.Add(Me.Label4)
        Me.cardSummary.Controls.Add(Me.Label1)
        Me.cardSummary.Controls.Add(Me.Label3)
        Me.cardSummary.Controls.Add(Me.Label2)
        Me.cardSummary.Controls.Add(Me.lblSpent)
        Me.cardSummary.Controls.Add(Me.lblReserved)
        Me.cardSummary.Controls.Add(Me.lblAvailable)
        Me.cardSummary.Location = New System.Drawing.Point(15, 3)
        Me.cardSummary.Name = "cardSummary"
        Me.cardSummary.Size = New System.Drawing.Size(1270, 60)
        Me.cardSummary.TabIndex = 2
        '
        'lblAllocated
        '
        Me.lblAllocated.BackColor = System.Drawing.Color.White
        Me.lblAllocated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAllocated.Font = New System.Drawing.Font("Segoe UI Semibold", 10.5!, System.Drawing.FontStyle.Bold)
        Me.lblAllocated.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblAllocated.Location = New System.Drawing.Point(956, 27)
        Me.lblAllocated.Name = "lblAllocated"
        Me.lblAllocated.Size = New System.Drawing.Size(256, 27)
        Me.lblAllocated.TabIndex = 1
        Me.lblAllocated.Text = "--"
        Me.lblAllocated.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(956, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(256, 23)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "الاعتماد"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(188, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(256, 23)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "المتاح"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(700, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(256, 23)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "المصروف"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(444, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(256, 23)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "المحجوز"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSpent
        '
        Me.lblSpent.BackColor = System.Drawing.Color.White
        Me.lblSpent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSpent.Font = New System.Drawing.Font("Segoe UI Semibold", 10.5!, System.Drawing.FontStyle.Bold)
        Me.lblSpent.ForeColor = System.Drawing.Color.DarkRed
        Me.lblSpent.Location = New System.Drawing.Point(700, 27)
        Me.lblSpent.Name = "lblSpent"
        Me.lblSpent.Size = New System.Drawing.Size(256, 27)
        Me.lblSpent.TabIndex = 3
        Me.lblSpent.Text = "--"
        Me.lblSpent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblReserved
        '
        Me.lblReserved.BackColor = System.Drawing.Color.White
        Me.lblReserved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReserved.Font = New System.Drawing.Font("Segoe UI Semibold", 10.5!, System.Drawing.FontStyle.Bold)
        Me.lblReserved.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblReserved.Location = New System.Drawing.Point(444, 27)
        Me.lblReserved.Name = "lblReserved"
        Me.lblReserved.Size = New System.Drawing.Size(256, 27)
        Me.lblReserved.TabIndex = 5
        Me.lblReserved.Text = "--"
        Me.lblReserved.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAvailable
        '
        Me.lblAvailable.BackColor = System.Drawing.Color.White
        Me.lblAvailable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAvailable.Font = New System.Drawing.Font("Segoe UI Semibold", 10.5!, System.Drawing.FontStyle.Bold)
        Me.lblAvailable.ForeColor = System.Drawing.Color.Blue
        Me.lblAvailable.Location = New System.Drawing.Point(188, 27)
        Me.lblAvailable.Name = "lblAvailable"
        Me.lblAvailable.Size = New System.Drawing.Size(256, 27)
        Me.lblAvailable.TabIndex = 7
        Me.lblAvailable.Text = "--"
        Me.lblAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlActions
        '
        Me.pnlActions.BackColor = System.Drawing.Color.White
        Me.pnlActions.Controls.Add(Me.btnConvert)
        Me.pnlActions.Controls.Add(Me.btnRefresh)
        Me.pnlActions.Controls.Add(Me.btnExit)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlActions.Location = New System.Drawing.Point(0, 715)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Size = New System.Drawing.Size(1300, 43)
        Me.pnlActions.TabIndex = 1
        '
        'btnConvert
        '
        Me.btnConvert.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConvert.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConvert.Location = New System.Drawing.Point(1050, 4)
        Me.btnConvert.Name = "btnConvert"
        Me.btnConvert.Size = New System.Drawing.Size(144, 36)
        Me.btnConvert.TabIndex = 0
        Me.btnConvert.Text = "تحويل إلى صرف"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Location = New System.Drawing.Point(900, 4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(110, 36)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "تحديث"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(20, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(110, 36)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "خروج"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 758)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1300, 22)
        Me.StatusStrip1.TabIndex = 3
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(30, 17)
        Me.lblStatus.Text = "جاهز"
        '
        'FrmBudgetReleaseToSpend
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1300, 780)
        Me.Controls.Add(Me.pnlContent)
        Me.Controls.Add(Me.pnlActions)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.MinimumSize = New System.Drawing.Size(1050, 650)
        Me.Name = "FrmBudgetReleaseToSpend"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تحويل الحجز إلى صرف"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlContent.ResumeLayout(False)
        Me.cardGrid.ResumeLayout(False)
        CType(Me.dgvTimeline, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvReserves, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cardForm.ResumeLayout(False)
        Me.cardForm.PerformLayout()
        Me.cardSummary.ResumeLayout(False)
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
    Friend WithEvents pnlContent As Panel
    Friend WithEvents cardSummary As Panel
    Friend WithEvents lblAllocated As Label
    Friend WithEvents lblSpent As Label
    Friend WithEvents lblReserved As Label
    Friend WithEvents lblAvailable As Label
    Friend WithEvents cardForm As Panel
    Friend WithEvents cmbFiscalYear As ComboBox
    Friend WithEvents cmbDoors As ComboBox
    Friend WithEvents cmbChapters As ComboBox
    Friend WithEvents cmbItems As ComboBox
    Friend WithEvents txtAmount As TextBox
    Friend WithEvents txtNotes As TextBox
    Friend WithEvents lblYear As Label
    Friend WithEvents lblAmount As Label
    Friend WithEvents lblNotes As Label
    Friend WithEvents cardGrid As Panel
    Friend WithEvents dgvReserves As DataGridView
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnConvert As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel
    Friend WithEvents lblDoor As Label
    Friend WithEvents lblChapter As Label
    Friend WithEvents lblItem As Label
    Friend WithEvents dgvTimeline As DataGridView
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblBeneficiaryType As Label
    Friend WithEvents cmbBeneficiaryType As ComboBox
    Friend WithEvents lblPaymentMethod As Label
    Friend WithEvents cmbPaymentMethod As ComboBox
    Friend WithEvents lblContraAccount As Label
    Friend WithEvents txtContraAccountCode As TextBox
    Friend WithEvents txtContraAccountName As TextBox
    Friend WithEvents btnPickContraAccount As Button
    Friend WithEvents chkHasStamp As CheckBox
    Friend WithEvents lblStampPercent As Label
    Friend WithEvents txtStampPercent As TextBox
    Friend WithEvents txtStampAccountCode As TextBox
    Friend WithEvents txtStampAccountName As TextBox
    Friend WithEvents btnPickStampAccount As Button
    Friend WithEvents lblInvoiceNo As Label
    Friend WithEvents txtInvoiceNo As TextBox
    Friend WithEvents lblDocumentNo As Label
    Friend WithEvents txtDocumentNo As TextBox
    Friend WithEvents lblSpendStatement As Label
    Friend WithEvents txtSpendStatement As TextBox
    Friend WithEvents lblAmountWords As Label
    Friend WithEvents txtAmountWords As TextBox
End Class
