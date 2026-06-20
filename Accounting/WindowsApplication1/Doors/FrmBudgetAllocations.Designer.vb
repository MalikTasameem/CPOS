<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBudgetAllocations
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblSubTitle = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlContent = New System.Windows.Forms.Panel()
        Me.cardGrid = New System.Windows.Forms.Panel()
        Me.dgvAllocations = New System.Windows.Forms.DataGridView()
        Me.cardForm = New System.Windows.Forms.Panel()
        Me.lblAmountWords = New System.Windows.Forms.Label()
        Me.txtAmountWords = New System.Windows.Forms.TextBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.cmbItems = New System.Windows.Forms.ComboBox()
        Me.cmbChapters = New System.Windows.Forms.ComboBox()
        Me.cmbDoors = New System.Windows.Forms.ComboBox()
        Me.cmbFiscalYear = New System.Windows.Forms.ComboBox()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.lblItem = New System.Windows.Forms.Label()
        Me.lblChapter = New System.Windows.Forms.Label()
        Me.lblDoor = New System.Windows.Forms.Label()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.cmbAllocationType = New System.Windows.Forms.ComboBox()
        Me.lblAllocationType = New System.Windows.Forms.Label()
        Me.cmbProvider = New System.Windows.Forms.ComboBox()
        Me.lblProvider = New System.Windows.Forms.Label()
        Me.dtpMovementDate = New System.Windows.Forms.DateTimePicker()
        Me.lblMovementDate = New System.Windows.Forms.Label()
        Me.txtDecisionNo = New System.Windows.Forms.TextBox()
        Me.lblDecisionNo = New System.Windows.Forms.Label()
        Me.dtpDecisionDate = New System.Windows.Forms.DateTimePicker()
        Me.lblDecisionDate = New System.Windows.Forms.Label()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.lblReason = New System.Windows.Forms.Label()
        Me.cmbOriginalFiscalYear = New System.Windows.Forms.ComboBox()
        Me.lblOriginalFiscalYear = New System.Windows.Forms.Label()
        Me.txtEmergencyReason = New System.Windows.Forms.TextBox()
        Me.lblEmergencyReason = New System.Windows.Forms.Label()
        Me.chkAutoApprove = New System.Windows.Forms.CheckBox()
        Me.pnlActions = New System.Windows.Forms.Panel()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnApprove = New System.Windows.Forms.Button()
        Me.btnCancelMovement = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlHeader.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.cardGrid.SuspendLayout()
        CType(Me.dgvAllocations, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cardForm.SuspendLayout()
        Me.pnlActions.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblSubTitle)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1250, 70)
        Me.pnlHeader.TabIndex = 2
        '
        'lblSubTitle
        '
        Me.lblSubTitle.AutoSize = True
        Me.lblSubTitle.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblSubTitle.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblSubTitle.Location = New System.Drawing.Point(780, 42)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Size = New System.Drawing.Size(225, 17)
        Me.lblSubTitle.TabIndex = 0
        Me.lblSubTitle.Text = "تحديد المخصصات السنوية لبنود الموازنة"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(1050, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(169, 30)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "اعتمادات الموازنة"
        '
        'pnlContent
        '
        Me.pnlContent.Controls.Add(Me.cardGrid)
        Me.pnlContent.Controls.Add(Me.cardForm)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 70)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlContent.Size = New System.Drawing.Size(1250, 573)
        Me.pnlContent.TabIndex = 0
        '
        'cardGrid
        '
        Me.cardGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cardGrid.BackColor = System.Drawing.Color.White
        Me.cardGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardGrid.Controls.Add(Me.dgvAllocations)
        Me.cardGrid.Location = New System.Drawing.Point(15, 290)
        Me.cardGrid.Name = "cardGrid"
        Me.cardGrid.Size = New System.Drawing.Size(1210, 268)
        Me.cardGrid.TabIndex = 0
        '
        'dgvAllocations
        '
        Me.dgvAllocations.AllowUserToAddRows = False
        Me.dgvAllocations.AllowUserToDeleteRows = False
        Me.dgvAllocations.BackgroundColor = System.Drawing.Color.White
        Me.dgvAllocations.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvAllocations.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvAllocations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAllocations.Location = New System.Drawing.Point(0, 0)
        Me.dgvAllocations.MultiSelect = False
        Me.dgvAllocations.Name = "dgvAllocations"
        Me.dgvAllocations.ReadOnly = True
        Me.dgvAllocations.RowHeadersVisible = False
        Me.dgvAllocations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvAllocations.Size = New System.Drawing.Size(1208, 266)
        Me.dgvAllocations.TabIndex = 0
        '
        'cardForm
        '
        Me.cardForm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cardForm.BackColor = System.Drawing.Color.White
        Me.cardForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardForm.Controls.Add(Me.lblAmountWords)
        Me.cardForm.Controls.Add(Me.txtAmountWords)
        Me.cardForm.Controls.Add(Me.txtAmount)
        Me.cardForm.Controls.Add(Me.cmbItems)
        Me.cardForm.Controls.Add(Me.cmbChapters)
        Me.cardForm.Controls.Add(Me.cmbDoors)
        Me.cardForm.Controls.Add(Me.cmbFiscalYear)
        Me.cardForm.Controls.Add(Me.lblAmount)
        Me.cardForm.Controls.Add(Me.lblItem)
        Me.cardForm.Controls.Add(Me.lblChapter)
        Me.cardForm.Controls.Add(Me.lblDoor)
        Me.cardForm.Controls.Add(Me.lblYear)
        Me.cardForm.Controls.Add(Me.cmbAllocationType)
        Me.cardForm.Controls.Add(Me.lblAllocationType)
        Me.cardForm.Controls.Add(Me.cmbProvider)
        Me.cardForm.Controls.Add(Me.lblProvider)
        Me.cardForm.Controls.Add(Me.dtpMovementDate)
        Me.cardForm.Controls.Add(Me.lblMovementDate)
        Me.cardForm.Controls.Add(Me.txtDecisionNo)
        Me.cardForm.Controls.Add(Me.lblDecisionNo)
        Me.cardForm.Controls.Add(Me.dtpDecisionDate)
        Me.cardForm.Controls.Add(Me.lblDecisionDate)
        Me.cardForm.Controls.Add(Me.txtReason)
        Me.cardForm.Controls.Add(Me.lblReason)
        Me.cardForm.Controls.Add(Me.cmbOriginalFiscalYear)
        Me.cardForm.Controls.Add(Me.lblOriginalFiscalYear)
        Me.cardForm.Controls.Add(Me.txtEmergencyReason)
        Me.cardForm.Controls.Add(Me.lblEmergencyReason)
        Me.cardForm.Controls.Add(Me.chkAutoApprove)
        Me.cardForm.Location = New System.Drawing.Point(15, 15)
        Me.cardForm.Name = "cardForm"
        Me.cardForm.Size = New System.Drawing.Size(1210, 260)
        Me.cardForm.TabIndex = 1
        '
        'lblAmountWords
        '
        Me.lblAmountWords.AutoSize = True
        Me.lblAmountWords.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmountWords.Location = New System.Drawing.Point(766, 222)
        Me.lblAmountWords.Name = "lblAmountWords"
        Me.lblAmountWords.Size = New System.Drawing.Size(86, 17)
        Me.lblAmountWords.TabIndex = 10
        Me.lblAmountWords.Text = "المبلغ بالحروف"
        '
        'txtAmountWords
        '
        Me.txtAmountWords.BackColor = System.Drawing.Color.White
        Me.txtAmountWords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmountWords.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtAmountWords.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.txtAmountWords.Location = New System.Drawing.Point(3, 218)
        Me.txtAmountWords.Name = "txtAmountWords"
        Me.txtAmountWords.ReadOnly = True
        Me.txtAmountWords.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtAmountWords.Size = New System.Drawing.Size(760, 25)
        Me.txtAmountWords.TabIndex = 11
        '
        'txtAmount
        '
        Me.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtAmount.Location = New System.Drawing.Point(543, 18)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtAmount.Size = New System.Drawing.Size(220, 25)
        Me.txtAmount.TabIndex = 0
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbItems
        '
        Me.cmbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItems.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbItems.Location = New System.Drawing.Point(880, 178)
        Me.cmbItems.Name = "cmbItems"
        Me.cmbItems.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cmbItems.Size = New System.Drawing.Size(220, 25)
        Me.cmbItems.TabIndex = 1
        '
        'cmbChapters
        '
        Me.cmbChapters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChapters.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbChapters.Location = New System.Drawing.Point(880, 138)
        Me.cmbChapters.Name = "cmbChapters"
        Me.cmbChapters.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cmbChapters.Size = New System.Drawing.Size(220, 25)
        Me.cmbChapters.TabIndex = 2
        '
        'cmbDoors
        '
        Me.cmbDoors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoors.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbDoors.Location = New System.Drawing.Point(880, 98)
        Me.cmbDoors.Name = "cmbDoors"
        Me.cmbDoors.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cmbDoors.Size = New System.Drawing.Size(220, 25)
        Me.cmbDoors.TabIndex = 3
        '
        'cmbFiscalYear
        '
        Me.cmbFiscalYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFiscalYear.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbFiscalYear.Location = New System.Drawing.Point(880, 58)
        Me.cmbFiscalYear.Name = "cmbFiscalYear"
        Me.cmbFiscalYear.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cmbFiscalYear.Size = New System.Drawing.Size(220, 25)
        Me.cmbFiscalYear.TabIndex = 4
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount.Location = New System.Drawing.Point(766, 20)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(78, 17)
        Me.lblAmount.TabIndex = 5
        Me.lblAmount.Text = "قيمة الاعتماد"
        '
        'lblItem
        '
        Me.lblItem.AutoSize = True
        Me.lblItem.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItem.Location = New System.Drawing.Point(1104, 180)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(30, 17)
        Me.lblItem.TabIndex = 6
        Me.lblItem.Text = "البند"
        '
        'lblChapter
        '
        Me.lblChapter.AutoSize = True
        Me.lblChapter.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChapter.Location = New System.Drawing.Point(1104, 140)
        Me.lblChapter.Name = "lblChapter"
        Me.lblChapter.Size = New System.Drawing.Size(44, 17)
        Me.lblChapter.TabIndex = 7
        Me.lblChapter.Text = "الفصل"
        '
        'lblDoor
        '
        Me.lblDoor.AutoSize = True
        Me.lblDoor.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDoor.Location = New System.Drawing.Point(1104, 100)
        Me.lblDoor.Name = "lblDoor"
        Me.lblDoor.Size = New System.Drawing.Size(34, 17)
        Me.lblDoor.TabIndex = 8
        Me.lblDoor.Text = "الباب"
        '
        'lblYear
        '
        Me.lblYear.AutoSize = True
        Me.lblYear.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYear.Location = New System.Drawing.Point(1104, 60)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(74, 17)
        Me.lblYear.TabIndex = 9
        Me.lblYear.Text = "السنة المالية"
        '
        'cmbAllocationType
        '
        Me.cmbAllocationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAllocationType.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbAllocationType.Location = New System.Drawing.Point(880, 18)
        Me.cmbAllocationType.Name = "cmbAllocationType"
        Me.cmbAllocationType.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cmbAllocationType.Size = New System.Drawing.Size(220, 25)
        Me.cmbAllocationType.TabIndex = 12
        '
        'lblAllocationType
        '
        Me.lblAllocationType.AutoSize = True
        Me.lblAllocationType.Location = New System.Drawing.Point(1104, 20)
        Me.lblAllocationType.Name = "lblAllocationType"
        Me.lblAllocationType.Size = New System.Drawing.Size(71, 17)
        Me.lblAllocationType.TabIndex = 13
        Me.lblAllocationType.Text = "نوع الاعتماد"
        '
        'cmbProvider
        '
        Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProvider.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbProvider.Location = New System.Drawing.Point(543, 58)
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cmbProvider.Size = New System.Drawing.Size(220, 25)
        Me.cmbProvider.TabIndex = 14
        '
        'lblProvider
        '
        Me.lblProvider.AutoSize = True
        Me.lblProvider.Location = New System.Drawing.Point(766, 60)
        Me.lblProvider.Name = "lblProvider"
        Me.lblProvider.Size = New System.Drawing.Size(74, 17)
        Me.lblProvider.TabIndex = 15
        Me.lblProvider.Text = "جهة الاعتماد"
        '
        'dtpMovementDate
        '
        Me.dtpMovementDate.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.dtpMovementDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpMovementDate.Location = New System.Drawing.Point(543, 98)
        Me.dtpMovementDate.Name = "dtpMovementDate"
        Me.dtpMovementDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtpMovementDate.Size = New System.Drawing.Size(220, 25)
        Me.dtpMovementDate.TabIndex = 16
        '
        'lblMovementDate
        '
        Me.lblMovementDate.AutoSize = True
        Me.lblMovementDate.Location = New System.Drawing.Point(766, 100)
        Me.lblMovementDate.Name = "lblMovementDate"
        Me.lblMovementDate.Size = New System.Drawing.Size(70, 17)
        Me.lblMovementDate.TabIndex = 17
        Me.lblMovementDate.Text = "تاريخ الحركة"
        '
        'txtDecisionNo
        '
        Me.txtDecisionNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDecisionNo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtDecisionNo.Location = New System.Drawing.Point(543, 138)
        Me.txtDecisionNo.Name = "txtDecisionNo"
        Me.txtDecisionNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtDecisionNo.Size = New System.Drawing.Size(220, 25)
        Me.txtDecisionNo.TabIndex = 18
        Me.txtDecisionNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDecisionNo
        '
        Me.lblDecisionNo.AutoSize = True
        Me.lblDecisionNo.Location = New System.Drawing.Point(766, 140)
        Me.lblDecisionNo.Name = "lblDecisionNo"
        Me.lblDecisionNo.Size = New System.Drawing.Size(57, 17)
        Me.lblDecisionNo.TabIndex = 19
        Me.lblDecisionNo.Text = "رقم القرار"
        '
        'dtpDecisionDate
        '
        Me.dtpDecisionDate.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.dtpDecisionDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDecisionDate.Location = New System.Drawing.Point(543, 178)
        Me.dtpDecisionDate.Name = "dtpDecisionDate"
        Me.dtpDecisionDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtpDecisionDate.Size = New System.Drawing.Size(220, 25)
        Me.dtpDecisionDate.TabIndex = 20
        '
        'lblDecisionDate
        '
        Me.lblDecisionDate.AutoSize = True
        Me.lblDecisionDate.Location = New System.Drawing.Point(766, 180)
        Me.lblDecisionDate.Name = "lblDecisionDate"
        Me.lblDecisionDate.Size = New System.Drawing.Size(63, 17)
        Me.lblDecisionDate.TabIndex = 21
        Me.lblDecisionDate.Text = "تاريخ القرار"
        '
        'txtReason
        '
        Me.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReason.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtReason.Location = New System.Drawing.Point(40, 18)
        Me.txtReason.Name = "txtReason"
        Me.txtReason.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtReason.Size = New System.Drawing.Size(380, 25)
        Me.txtReason.TabIndex = 22
        '
        'lblReason
        '
        Me.lblReason.AutoSize = True
        Me.lblReason.Location = New System.Drawing.Point(430, 20)
        Me.lblReason.Name = "lblReason"
        Me.lblReason.Size = New System.Drawing.Size(82, 17)
        Me.lblReason.TabIndex = 23
        Me.lblReason.Text = "البيان / السبب"
        '
        'cmbOriginalFiscalYear
        '
        Me.cmbOriginalFiscalYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOriginalFiscalYear.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbOriginalFiscalYear.Location = New System.Drawing.Point(200, 58)
        Me.cmbOriginalFiscalYear.Name = "cmbOriginalFiscalYear"
        Me.cmbOriginalFiscalYear.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cmbOriginalFiscalYear.Size = New System.Drawing.Size(220, 25)
        Me.cmbOriginalFiscalYear.TabIndex = 24
        '
        'lblOriginalFiscalYear
        '
        Me.lblOriginalFiscalYear.AutoSize = True
        Me.lblOriginalFiscalYear.Location = New System.Drawing.Point(430, 60)
        Me.lblOriginalFiscalYear.Name = "lblOriginalFiscalYear"
        Me.lblOriginalFiscalYear.Size = New System.Drawing.Size(79, 17)
        Me.lblOriginalFiscalYear.TabIndex = 25
        Me.lblOriginalFiscalYear.Text = "السنة الأصلية"
        '
        'txtEmergencyReason
        '
        Me.txtEmergencyReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmergencyReason.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.txtEmergencyReason.Location = New System.Drawing.Point(40, 98)
        Me.txtEmergencyReason.Name = "txtEmergencyReason"
        Me.txtEmergencyReason.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtEmergencyReason.Size = New System.Drawing.Size(380, 25)
        Me.txtEmergencyReason.TabIndex = 26
        '
        'lblEmergencyReason
        '
        Me.lblEmergencyReason.AutoSize = True
        Me.lblEmergencyReason.Location = New System.Drawing.Point(430, 100)
        Me.lblEmergencyReason.Name = "lblEmergencyReason"
        Me.lblEmergencyReason.Size = New System.Drawing.Size(74, 17)
        Me.lblEmergencyReason.TabIndex = 27
        Me.lblEmergencyReason.Text = "سبب الطارئ"
        '
        'chkAutoApprove
        '
        Me.chkAutoApprove.AutoSize = True
        Me.chkAutoApprove.Checked = True
        Me.chkAutoApprove.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAutoApprove.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.chkAutoApprove.Location = New System.Drawing.Point(254, 139)
        Me.chkAutoApprove.Name = "chkAutoApprove"
        Me.chkAutoApprove.Size = New System.Drawing.Size(166, 23)
        Me.chkAutoApprove.TabIndex = 28
        Me.chkAutoApprove.Text = "اعتماد مباشر بعد الحفظ"
        '
        'pnlActions
        '
        Me.pnlActions.BackColor = System.Drawing.Color.White
        Me.pnlActions.Controls.Add(Me.btnExit)
        Me.pnlActions.Controls.Add(Me.btnRefresh)
        Me.pnlActions.Controls.Add(Me.btnSave)
        Me.pnlActions.Controls.Add(Me.btnNew)
        Me.pnlActions.Controls.Add(Me.btnApprove)
        Me.pnlActions.Controls.Add(Me.btnCancelMovement)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlActions.Location = New System.Drawing.Point(0, 643)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Size = New System.Drawing.Size(1250, 55)
        Me.pnlActions.TabIndex = 1
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(525, 10)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(120, 36)
        Me.btnExit.TabIndex = 0
        Me.btnExit.Text = "⟵ خروج"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.Location = New System.Drawing.Point(655, 10)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(135, 36)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "↻ تحديث البيانات"
        '
        'btnSave
        '
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnSave.Location = New System.Drawing.Point(800, 10)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(135, 36)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "✓ حفظ الاعتماد"
        '
        'btnNew
        '
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnNew.Location = New System.Drawing.Point(945, 10)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(135, 36)
        Me.btnNew.TabIndex = 3
        Me.btnNew.Text = "+ اعتماد جديد"
        '
        'btnApprove
        '
        Me.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnApprove.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnApprove.Location = New System.Drawing.Point(380, 10)
        Me.btnApprove.Name = "btnApprove"
        Me.btnApprove.Size = New System.Drawing.Size(135, 36)
        Me.btnApprove.TabIndex = 4
        Me.btnApprove.Text = "✓ اعتماد الحركة"
        Me.btnApprove.UseVisualStyleBackColor = True
        '
        'btnCancelMovement
        '
        Me.btnCancelMovement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnCancelMovement.Location = New System.Drawing.Point(225, 10)
        Me.btnCancelMovement.Name = "btnCancelMovement"
        Me.btnCancelMovement.Size = New System.Drawing.Size(145, 36)
        Me.btnCancelMovement.TabIndex = 5
        Me.btnCancelMovement.Text = "× إلغاء الحركة"
        Me.btnCancelMovement.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 698)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1250, 22)
        Me.StatusStrip1.TabIndex = 3
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(30, 17)
        Me.lblStatus.Text = "جاهز"
        '
        'FrmBudgetAllocations
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1250, 720)
        Me.Controls.Add(Me.pnlContent)
        Me.Controls.Add(Me.pnlActions)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.MinimumSize = New System.Drawing.Size(1050, 650)
        Me.Name = "FrmBudgetAllocations"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "اعتمادات الموازنة"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlContent.ResumeLayout(False)
        Me.cardGrid.ResumeLayout(False)
        CType(Me.dgvAllocations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cardForm.ResumeLayout(False)
        Me.cardForm.PerformLayout()
        Me.pnlActions.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubTitle As Label
    Friend WithEvents pnlContent As Panel
    Friend WithEvents cardForm As Panel
    Friend WithEvents cmbFiscalYear As ComboBox
    Friend WithEvents cmbDoors As ComboBox
    Friend WithEvents cmbChapters As ComboBox
    Friend WithEvents cmbItems As ComboBox
    Friend WithEvents txtAmount As TextBox
    Friend WithEvents lblYear As Label
    Friend WithEvents lblDoor As Label
    Friend WithEvents lblChapter As Label
    Friend WithEvents lblItem As Label
    Friend WithEvents lblAmount As Label
    Friend WithEvents cardGrid As Panel
    Friend WithEvents dgvAllocations As DataGridView
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnExit As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel
    Friend WithEvents lblAmountWords As Label
    Friend WithEvents txtAmountWords As TextBox
    '----------------------------------------------------------------------------

    Friend WithEvents cmbAllocationType As ComboBox
    Friend WithEvents lblAllocationType As Label

    Friend WithEvents cmbProvider As ComboBox
    Friend WithEvents lblProvider As Label

    Friend WithEvents dtpMovementDate As DateTimePicker
    Friend WithEvents lblMovementDate As Label

    Friend WithEvents txtDecisionNo As TextBox
    Friend WithEvents lblDecisionNo As Label

    Friend WithEvents dtpDecisionDate As DateTimePicker
    Friend WithEvents lblDecisionDate As Label

    Friend WithEvents txtReason As TextBox
    Friend WithEvents lblReason As Label

    Friend WithEvents cmbOriginalFiscalYear As ComboBox
    Friend WithEvents lblOriginalFiscalYear As Label

    Friend WithEvents txtEmergencyReason As TextBox
    Friend WithEvents lblEmergencyReason As Label

    Friend WithEvents chkAutoApprove As CheckBox

    Friend WithEvents btnApprove As Button
    Friend WithEvents btnCancelMovement As Button

End Class
