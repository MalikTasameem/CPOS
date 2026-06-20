<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBudgetTransfer
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
        Me.dgvTransfers = New System.Windows.Forms.DataGridView()
        Me.cardAmountNotes = New System.Windows.Forms.Panel()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.lblAmountWords = New System.Windows.Forms.Label()
        Me.txtAmountWords = New System.Windows.Forms.TextBox()
        Me.lblDecisionNo = New System.Windows.Forms.Label()
        Me.txtDecisionNo = New System.Windows.Forms.TextBox()
        Me.lblDecisionDate = New System.Windows.Forms.Label()
        Me.dtpDecisionDate = New System.Windows.Forms.DateTimePicker()
        Me.lblNotes = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.cardTo = New System.Windows.Forms.Panel()
        Me.lblToTitle = New System.Windows.Forms.Label()
        Me.lblYearTo = New System.Windows.Forms.Label()
        Me.cmbYearTo = New System.Windows.Forms.ComboBox()
        Me.lblDoorTo = New System.Windows.Forms.Label()
        Me.cmbDoorTo = New System.Windows.Forms.ComboBox()
        Me.lblChapterTo = New System.Windows.Forms.Label()
        Me.cmbChapterTo = New System.Windows.Forms.ComboBox()
        Me.lblItemTo = New System.Windows.Forms.Label()
        Me.cmbItemTo = New System.Windows.Forms.ComboBox()
        Me.lblToAllocatedCap = New System.Windows.Forms.Label()
        Me.lblToAllocated = New System.Windows.Forms.Label()
        Me.lblToSpentCap = New System.Windows.Forms.Label()
        Me.lblToSpent = New System.Windows.Forms.Label()
        Me.lblToReservedCap = New System.Windows.Forms.Label()
        Me.lblToReserved = New System.Windows.Forms.Label()
        Me.lblToAvailableCap = New System.Windows.Forms.Label()
        Me.lblToAvailable = New System.Windows.Forms.Label()
        Me.lblToAfterCap = New System.Windows.Forms.Label()
        Me.lblToAfter = New System.Windows.Forms.Label()
        Me.cardFrom = New System.Windows.Forms.Panel()
        Me.lblFromTitle = New System.Windows.Forms.Label()
        Me.lblYearFrom = New System.Windows.Forms.Label()
        Me.cmbYearFrom = New System.Windows.Forms.ComboBox()
        Me.lblDoorFrom = New System.Windows.Forms.Label()
        Me.cmbDoorFrom = New System.Windows.Forms.ComboBox()
        Me.lblChapterFrom = New System.Windows.Forms.Label()
        Me.cmbChapterFrom = New System.Windows.Forms.ComboBox()
        Me.lblItemFrom = New System.Windows.Forms.Label()
        Me.cmbItemFrom = New System.Windows.Forms.ComboBox()
        Me.lblFromAllocatedCap = New System.Windows.Forms.Label()
        Me.lblFromAllocated = New System.Windows.Forms.Label()
        Me.lblFromSpentCap = New System.Windows.Forms.Label()
        Me.lblFromSpent = New System.Windows.Forms.Label()
        Me.lblFromReservedCap = New System.Windows.Forms.Label()
        Me.lblFromReserved = New System.Windows.Forms.Label()
        Me.lblFromAvailableCap = New System.Windows.Forms.Label()
        Me.lblFromAvailable = New System.Windows.Forms.Label()
        Me.lblFromAfterCap = New System.Windows.Forms.Label()
        Me.lblFromAfter = New System.Windows.Forms.Label()
        Me.cardType = New System.Windows.Forms.Panel()
        Me.lblTransferType = New System.Windows.Forms.Label()
        Me.rbTransferAllocation = New System.Windows.Forms.RadioButton()
        Me.rbTransferReserve = New System.Windows.Forms.RadioButton()
        Me.pnlActions = New System.Windows.Forms.Panel()
        Me.btnTransfer = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlHeader.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.cardGrid.SuspendLayout()
        CType(Me.dgvTransfers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cardAmountNotes.SuspendLayout()
        Me.cardTo.SuspendLayout()
        Me.cardFrom.SuspendLayout()
        Me.cardType.SuspendLayout()
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
        Me.pnlHeader.Size = New System.Drawing.Size(1350, 96)
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
        Me.lblBudgetOverSpendWarning.Size = New System.Drawing.Size(1350, 24)
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
        Me.lblSubTitle.Location = New System.Drawing.Point(700, 42)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Size = New System.Drawing.Size(370, 17)
        Me.lblSubTitle.TabIndex = 0
        Me.lblSubTitle.Text = "نقل اعتماد من بند مصدر إلى بند مستفيد مع تحديث موقف الموازنة"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(1050, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(281, 30)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "مناقلة اعتماد بين بنود الموازنة"
        '
        'pnlContent
        '
        Me.pnlContent.Controls.Add(Me.cardGrid)
        Me.pnlContent.Controls.Add(Me.cardAmountNotes)
        Me.pnlContent.Controls.Add(Me.cardTo)
        Me.pnlContent.Controls.Add(Me.cardFrom)
        Me.pnlContent.Controls.Add(Me.cardType)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 96)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlContent.Size = New System.Drawing.Size(1350, 653)
        Me.pnlContent.TabIndex = 0
        '
        'cardGrid
        '
        Me.cardGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cardGrid.BackColor = System.Drawing.Color.White
        Me.cardGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardGrid.Controls.Add(Me.dgvTransfers)
        Me.cardGrid.Location = New System.Drawing.Point(15, 521)
        Me.cardGrid.Name = "cardGrid"
        Me.cardGrid.Size = New System.Drawing.Size(1320, 152)
        Me.cardGrid.TabIndex = 0
        '
        'dgvTransfers
        '
        Me.dgvTransfers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTransfers.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.dgvTransfers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTransfers.Location = New System.Drawing.Point(0, 0)
        Me.dgvTransfers.Name = "dgvTransfers"
        Me.dgvTransfers.ReadOnly = True
        Me.dgvTransfers.RowHeadersVisible = False
        Me.dgvTransfers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTransfers.Size = New System.Drawing.Size(1318, 150)
        Me.dgvTransfers.TabIndex = 0
        '
        'cardAmountNotes
        '
        Me.cardAmountNotes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cardAmountNotes.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.cardAmountNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardAmountNotes.Controls.Add(Me.lblAmount)
        Me.cardAmountNotes.Controls.Add(Me.txtAmount)
        Me.cardAmountNotes.Controls.Add(Me.lblAmountWords)
        Me.cardAmountNotes.Controls.Add(Me.txtAmountWords)
        Me.cardAmountNotes.Controls.Add(Me.lblDecisionNo)
        Me.cardAmountNotes.Controls.Add(Me.txtDecisionNo)
        Me.cardAmountNotes.Controls.Add(Me.lblDecisionDate)
        Me.cardAmountNotes.Controls.Add(Me.dtpDecisionDate)
        Me.cardAmountNotes.Controls.Add(Me.lblNotes)
        Me.cardAmountNotes.Controls.Add(Me.txtNotes)
        Me.cardAmountNotes.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cardAmountNotes.Location = New System.Drawing.Point(15, 385)
        Me.cardAmountNotes.Name = "cardAmountNotes"
        Me.cardAmountNotes.Size = New System.Drawing.Size(1320, 134)
        Me.cardAmountNotes.TabIndex = 1
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount.Location = New System.Drawing.Point(1181, 6)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(78, 17)
        Me.lblAmount.TabIndex = 0
        Me.lblAmount.Text = "قيمة المناقلة"
        '
        'txtAmount
        '
        Me.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtAmount.Location = New System.Drawing.Point(978, 3)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(200, 25)
        Me.txtAmount.TabIndex = 1
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblAmountWords
        '
        Me.lblAmountWords.AutoSize = True
        Me.lblAmountWords.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmountWords.Location = New System.Drawing.Point(862, 6)
        Me.lblAmountWords.Name = "lblAmountWords"
        Me.lblAmountWords.Size = New System.Drawing.Size(86, 17)
        Me.lblAmountWords.TabIndex = 4
        Me.lblAmountWords.Text = "المبلغ بالحروف"
        '
        'txtAmountWords
        '
        Me.txtAmountWords.BackColor = System.Drawing.Color.White
        Me.txtAmountWords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmountWords.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtAmountWords.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.txtAmountWords.Location = New System.Drawing.Point(3, 3)
        Me.txtAmountWords.Name = "txtAmountWords"
        Me.txtAmountWords.ReadOnly = True
        Me.txtAmountWords.Size = New System.Drawing.Size(856, 25)
        Me.txtAmountWords.TabIndex = 5
        Me.txtAmountWords.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDecisionNo
        '
        Me.lblDecisionNo.AutoSize = True
        Me.lblDecisionNo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDecisionNo.Location = New System.Drawing.Point(1181, 35)
        Me.lblDecisionNo.Name = "lblDecisionNo"
        Me.lblDecisionNo.Size = New System.Drawing.Size(57, 17)
        Me.lblDecisionNo.TabIndex = 6
        Me.lblDecisionNo.Text = "رقم القرار"
        '
        'txtDecisionNo
        '
        Me.txtDecisionNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDecisionNo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtDecisionNo.Location = New System.Drawing.Point(978, 33)
        Me.txtDecisionNo.Name = "txtDecisionNo"
        Me.txtDecisionNo.Size = New System.Drawing.Size(200, 25)
        Me.txtDecisionNo.TabIndex = 7
        Me.txtDecisionNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDecisionDate
        '
        Me.lblDecisionDate.AutoSize = True
        Me.lblDecisionDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDecisionDate.Location = New System.Drawing.Point(862, 35)
        Me.lblDecisionDate.Name = "lblDecisionDate"
        Me.lblDecisionDate.Size = New System.Drawing.Size(63, 17)
        Me.lblDecisionDate.TabIndex = 8
        Me.lblDecisionDate.Text = "تاريخ القرار"
        '
        'dtpDecisionDate
        '
        Me.dtpDecisionDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.dtpDecisionDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDecisionDate.Location = New System.Drawing.Point(660, 33)
        Me.dtpDecisionDate.Name = "dtpDecisionDate"
        Me.dtpDecisionDate.Size = New System.Drawing.Size(200, 25)
        Me.dtpDecisionDate.TabIndex = 9
        '
        'lblNotes
        '
        Me.lblNotes.AutoSize = True
        Me.lblNotes.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotes.Location = New System.Drawing.Point(1181, 65)
        Me.lblNotes.Name = "lblNotes"
        Me.lblNotes.Size = New System.Drawing.Size(112, 17)
        Me.lblNotes.TabIndex = 2
        Me.lblNotes.Text = "سبب / بيان المناقلة"
        '
        'txtNotes
        '
        Me.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNotes.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtNotes.Location = New System.Drawing.Point(3, 62)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(1175, 65)
        Me.txtNotes.TabIndex = 3
        '
        'cardTo
        '
        Me.cardTo.BackColor = System.Drawing.Color.White
        Me.cardTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardTo.Controls.Add(Me.lblToTitle)
        Me.cardTo.Controls.Add(Me.lblYearTo)
        Me.cardTo.Controls.Add(Me.cmbYearTo)
        Me.cardTo.Controls.Add(Me.lblDoorTo)
        Me.cardTo.Controls.Add(Me.cmbDoorTo)
        Me.cardTo.Controls.Add(Me.lblChapterTo)
        Me.cardTo.Controls.Add(Me.cmbChapterTo)
        Me.cardTo.Controls.Add(Me.lblItemTo)
        Me.cardTo.Controls.Add(Me.cmbItemTo)
        Me.cardTo.Controls.Add(Me.lblToAllocatedCap)
        Me.cardTo.Controls.Add(Me.lblToAllocated)
        Me.cardTo.Controls.Add(Me.lblToSpentCap)
        Me.cardTo.Controls.Add(Me.lblToSpent)
        Me.cardTo.Controls.Add(Me.lblToReservedCap)
        Me.cardTo.Controls.Add(Me.lblToReserved)
        Me.cardTo.Controls.Add(Me.lblToAvailableCap)
        Me.cardTo.Controls.Add(Me.lblToAvailable)
        Me.cardTo.Controls.Add(Me.lblToAfterCap)
        Me.cardTo.Controls.Add(Me.lblToAfter)
        Me.cardTo.Location = New System.Drawing.Point(15, 100)
        Me.cardTo.Name = "cardTo"
        Me.cardTo.Size = New System.Drawing.Size(655, 280)
        Me.cardTo.TabIndex = 2
        '
        'lblToTitle
        '
        Me.lblToTitle.AutoSize = True
        Me.lblToTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblToTitle.Location = New System.Drawing.Point(360, 10)
        Me.lblToTitle.Name = "lblToTitle"
        Me.lblToTitle.Size = New System.Drawing.Size(208, 20)
        Me.lblToTitle.TabIndex = 0
        Me.lblToTitle.Text = "البند المستفيد - يتم الإضافة إليه"
        '
        'lblYearTo
        '
        Me.lblYearTo.AutoSize = True
        Me.lblYearTo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYearTo.Location = New System.Drawing.Point(544, 48)
        Me.lblYearTo.Name = "lblYearTo"
        Me.lblYearTo.Size = New System.Drawing.Size(37, 17)
        Me.lblYearTo.TabIndex = 1
        Me.lblYearTo.Text = "السنة"
        '
        'cmbYearTo
        '
        Me.cmbYearTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbYearTo.Enabled = False
        Me.cmbYearTo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbYearTo.Location = New System.Drawing.Point(320, 43)
        Me.cmbYearTo.Name = "cmbYearTo"
        Me.cmbYearTo.Size = New System.Drawing.Size(220, 25)
        Me.cmbYearTo.TabIndex = 2
        '
        'lblDoorTo
        '
        Me.lblDoorTo.AutoSize = True
        Me.lblDoorTo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDoorTo.Location = New System.Drawing.Point(544, 83)
        Me.lblDoorTo.Name = "lblDoorTo"
        Me.lblDoorTo.Size = New System.Drawing.Size(34, 17)
        Me.lblDoorTo.TabIndex = 3
        Me.lblDoorTo.Text = "الباب"
        '
        'cmbDoorTo
        '
        Me.cmbDoorTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoorTo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDoorTo.Location = New System.Drawing.Point(320, 78)
        Me.cmbDoorTo.Name = "cmbDoorTo"
        Me.cmbDoorTo.Size = New System.Drawing.Size(220, 25)
        Me.cmbDoorTo.TabIndex = 4
        '
        'lblChapterTo
        '
        Me.lblChapterTo.AutoSize = True
        Me.lblChapterTo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChapterTo.Location = New System.Drawing.Point(544, 118)
        Me.lblChapterTo.Name = "lblChapterTo"
        Me.lblChapterTo.Size = New System.Drawing.Size(44, 17)
        Me.lblChapterTo.TabIndex = 5
        Me.lblChapterTo.Text = "الفصل"
        '
        'cmbChapterTo
        '
        Me.cmbChapterTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChapterTo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbChapterTo.Location = New System.Drawing.Point(320, 113)
        Me.cmbChapterTo.Name = "cmbChapterTo"
        Me.cmbChapterTo.Size = New System.Drawing.Size(220, 25)
        Me.cmbChapterTo.TabIndex = 6
        '
        'lblItemTo
        '
        Me.lblItemTo.AutoSize = True
        Me.lblItemTo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemTo.Location = New System.Drawing.Point(544, 153)
        Me.lblItemTo.Name = "lblItemTo"
        Me.lblItemTo.Size = New System.Drawing.Size(30, 17)
        Me.lblItemTo.TabIndex = 7
        Me.lblItemTo.Text = "البند"
        '
        'cmbItemTo
        '
        Me.cmbItemTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItemTo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbItemTo.Location = New System.Drawing.Point(320, 148)
        Me.cmbItemTo.Name = "cmbItemTo"
        Me.cmbItemTo.Size = New System.Drawing.Size(220, 25)
        Me.cmbItemTo.TabIndex = 8
        '
        'lblToAllocatedCap
        '
        Me.lblToAllocatedCap.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.lblToAllocatedCap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblToAllocatedCap.Font = New System.Drawing.Font("Segoe UI Semibold", 8.75!, System.Drawing.FontStyle.Bold)
        Me.lblToAllocatedCap.Location = New System.Drawing.Point(516, 204)
        Me.lblToAllocatedCap.Name = "lblToAllocatedCap"
        Me.lblToAllocatedCap.Size = New System.Drawing.Size(120, 24)
        Me.lblToAllocatedCap.TabIndex = 9
        Me.lblToAllocatedCap.Text = "إجمالي الاعتماد"
        Me.lblToAllocatedCap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblToAllocated
        '
        Me.lblToAllocated.BackColor = System.Drawing.Color.White
        Me.lblToAllocated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblToAllocated.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.lblToAllocated.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblToAllocated.Location = New System.Drawing.Point(516, 229)
        Me.lblToAllocated.Name = "lblToAllocated"
        Me.lblToAllocated.Size = New System.Drawing.Size(120, 28)
        Me.lblToAllocated.TabIndex = 10
        Me.lblToAllocated.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblToSpentCap
        '
        Me.lblToSpentCap.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.lblToSpentCap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblToSpentCap.Font = New System.Drawing.Font("Segoe UI Semibold", 8.75!, System.Drawing.FontStyle.Bold)
        Me.lblToSpentCap.Location = New System.Drawing.Point(390, 204)
        Me.lblToSpentCap.Name = "lblToSpentCap"
        Me.lblToSpentCap.Size = New System.Drawing.Size(120, 24)
        Me.lblToSpentCap.TabIndex = 11
        Me.lblToSpentCap.Text = "إجمالي المصروف"
        Me.lblToSpentCap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblToSpent
        '
        Me.lblToSpent.BackColor = System.Drawing.Color.White
        Me.lblToSpent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblToSpent.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.lblToSpent.ForeColor = System.Drawing.Color.DarkRed
        Me.lblToSpent.Location = New System.Drawing.Point(390, 229)
        Me.lblToSpent.Name = "lblToSpent"
        Me.lblToSpent.Size = New System.Drawing.Size(120, 28)
        Me.lblToSpent.TabIndex = 12
        Me.lblToSpent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblToReservedCap
        '
        Me.lblToReservedCap.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.lblToReservedCap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblToReservedCap.Font = New System.Drawing.Font("Segoe UI Semibold", 8.75!, System.Drawing.FontStyle.Bold)
        Me.lblToReservedCap.Location = New System.Drawing.Point(264, 204)
        Me.lblToReservedCap.Name = "lblToReservedCap"
        Me.lblToReservedCap.Size = New System.Drawing.Size(120, 24)
        Me.lblToReservedCap.TabIndex = 13
        Me.lblToReservedCap.Text = "إجمالي الحجوزات"
        Me.lblToReservedCap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblToReserved
        '
        Me.lblToReserved.BackColor = System.Drawing.Color.White
        Me.lblToReserved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblToReserved.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.lblToReserved.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblToReserved.Location = New System.Drawing.Point(264, 229)
        Me.lblToReserved.Name = "lblToReserved"
        Me.lblToReserved.Size = New System.Drawing.Size(120, 28)
        Me.lblToReserved.TabIndex = 14
        Me.lblToReserved.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblToAvailableCap
        '
        Me.lblToAvailableCap.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.lblToAvailableCap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblToAvailableCap.Font = New System.Drawing.Font("Segoe UI Semibold", 8.75!, System.Drawing.FontStyle.Bold)
        Me.lblToAvailableCap.Location = New System.Drawing.Point(138, 204)
        Me.lblToAvailableCap.Name = "lblToAvailableCap"
        Me.lblToAvailableCap.Size = New System.Drawing.Size(120, 24)
        Me.lblToAvailableCap.TabIndex = 15
        Me.lblToAvailableCap.Text = "الرصيد المتاح"
        Me.lblToAvailableCap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblToAvailable
        '
        Me.lblToAvailable.BackColor = System.Drawing.Color.White
        Me.lblToAvailable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblToAvailable.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.lblToAvailable.ForeColor = System.Drawing.Color.Blue
        Me.lblToAvailable.Location = New System.Drawing.Point(138, 229)
        Me.lblToAvailable.Name = "lblToAvailable"
        Me.lblToAvailable.Size = New System.Drawing.Size(120, 28)
        Me.lblToAvailable.TabIndex = 16
        Me.lblToAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblToAfterCap
        '
        Me.lblToAfterCap.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.lblToAfterCap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblToAfterCap.Font = New System.Drawing.Font("Segoe UI Semibold", 8.75!, System.Drawing.FontStyle.Bold)
        Me.lblToAfterCap.Location = New System.Drawing.Point(12, 204)
        Me.lblToAfterCap.Name = "lblToAfterCap"
        Me.lblToAfterCap.Size = New System.Drawing.Size(120, 24)
        Me.lblToAfterCap.TabIndex = 17
        Me.lblToAfterCap.Text = "بعد المناقلة"
        Me.lblToAfterCap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblToAfter
        '
        Me.lblToAfter.BackColor = System.Drawing.Color.White
        Me.lblToAfter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblToAfter.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.lblToAfter.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblToAfter.Location = New System.Drawing.Point(12, 229)
        Me.lblToAfter.Name = "lblToAfter"
        Me.lblToAfter.Size = New System.Drawing.Size(120, 28)
        Me.lblToAfter.TabIndex = 18
        Me.lblToAfter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cardFrom
        '
        Me.cardFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cardFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.cardFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardFrom.Controls.Add(Me.lblFromTitle)
        Me.cardFrom.Controls.Add(Me.lblYearFrom)
        Me.cardFrom.Controls.Add(Me.cmbYearFrom)
        Me.cardFrom.Controls.Add(Me.lblDoorFrom)
        Me.cardFrom.Controls.Add(Me.cmbDoorFrom)
        Me.cardFrom.Controls.Add(Me.lblChapterFrom)
        Me.cardFrom.Controls.Add(Me.cmbChapterFrom)
        Me.cardFrom.Controls.Add(Me.lblItemFrom)
        Me.cardFrom.Controls.Add(Me.cmbItemFrom)
        Me.cardFrom.Controls.Add(Me.lblFromAllocatedCap)
        Me.cardFrom.Controls.Add(Me.lblFromAllocated)
        Me.cardFrom.Controls.Add(Me.lblFromSpentCap)
        Me.cardFrom.Controls.Add(Me.lblFromSpent)
        Me.cardFrom.Controls.Add(Me.lblFromReservedCap)
        Me.cardFrom.Controls.Add(Me.lblFromReserved)
        Me.cardFrom.Controls.Add(Me.lblFromAvailableCap)
        Me.cardFrom.Controls.Add(Me.lblFromAvailable)
        Me.cardFrom.Controls.Add(Me.lblFromAfterCap)
        Me.cardFrom.Controls.Add(Me.lblFromAfter)
        Me.cardFrom.Location = New System.Drawing.Point(680, 100)
        Me.cardFrom.Name = "cardFrom"
        Me.cardFrom.Size = New System.Drawing.Size(655, 280)
        Me.cardFrom.TabIndex = 3
        '
        'lblFromTitle
        '
        Me.lblFromTitle.AutoSize = True
        Me.lblFromTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblFromTitle.Location = New System.Drawing.Point(360, 10)
        Me.lblFromTitle.Name = "lblFromTitle"
        Me.lblFromTitle.Size = New System.Drawing.Size(192, 20)
        Me.lblFromTitle.TabIndex = 0
        Me.lblFromTitle.Text = "البند المصدر - يتم الخصم منه"
        '
        'lblYearFrom
        '
        Me.lblYearFrom.AutoSize = True
        Me.lblYearFrom.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYearFrom.Location = New System.Drawing.Point(544, 47)
        Me.lblYearFrom.Name = "lblYearFrom"
        Me.lblYearFrom.Size = New System.Drawing.Size(37, 17)
        Me.lblYearFrom.TabIndex = 1
        Me.lblYearFrom.Text = "السنة"
        '
        'cmbYearFrom
        '
        Me.cmbYearFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbYearFrom.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.cmbYearFrom.Location = New System.Drawing.Point(320, 43)
        Me.cmbYearFrom.Name = "cmbYearFrom"
        Me.cmbYearFrom.Size = New System.Drawing.Size(220, 25)
        Me.cmbYearFrom.TabIndex = 2
        '
        'lblDoorFrom
        '
        Me.lblDoorFrom.AutoSize = True
        Me.lblDoorFrom.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDoorFrom.Location = New System.Drawing.Point(544, 82)
        Me.lblDoorFrom.Name = "lblDoorFrom"
        Me.lblDoorFrom.Size = New System.Drawing.Size(34, 17)
        Me.lblDoorFrom.TabIndex = 3
        Me.lblDoorFrom.Text = "الباب"
        '
        'cmbDoorFrom
        '
        Me.cmbDoorFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoorFrom.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.cmbDoorFrom.Location = New System.Drawing.Point(320, 78)
        Me.cmbDoorFrom.Name = "cmbDoorFrom"
        Me.cmbDoorFrom.Size = New System.Drawing.Size(220, 25)
        Me.cmbDoorFrom.TabIndex = 4
        '
        'lblChapterFrom
        '
        Me.lblChapterFrom.AutoSize = True
        Me.lblChapterFrom.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChapterFrom.Location = New System.Drawing.Point(544, 117)
        Me.lblChapterFrom.Name = "lblChapterFrom"
        Me.lblChapterFrom.Size = New System.Drawing.Size(44, 17)
        Me.lblChapterFrom.TabIndex = 5
        Me.lblChapterFrom.Text = "الفصل"
        '
        'cmbChapterFrom
        '
        Me.cmbChapterFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChapterFrom.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.cmbChapterFrom.Location = New System.Drawing.Point(320, 113)
        Me.cmbChapterFrom.Name = "cmbChapterFrom"
        Me.cmbChapterFrom.Size = New System.Drawing.Size(220, 25)
        Me.cmbChapterFrom.TabIndex = 6
        '
        'lblItemFrom
        '
        Me.lblItemFrom.AutoSize = True
        Me.lblItemFrom.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemFrom.Location = New System.Drawing.Point(544, 152)
        Me.lblItemFrom.Name = "lblItemFrom"
        Me.lblItemFrom.Size = New System.Drawing.Size(30, 17)
        Me.lblItemFrom.TabIndex = 7
        Me.lblItemFrom.Text = "البند"
        '
        'cmbItemFrom
        '
        Me.cmbItemFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItemFrom.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.cmbItemFrom.Location = New System.Drawing.Point(320, 148)
        Me.cmbItemFrom.Name = "cmbItemFrom"
        Me.cmbItemFrom.Size = New System.Drawing.Size(220, 25)
        Me.cmbItemFrom.TabIndex = 8
        '
        'lblFromAllocatedCap
        '
        Me.lblFromAllocatedCap.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.lblFromAllocatedCap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFromAllocatedCap.Font = New System.Drawing.Font("Segoe UI Semibold", 8.75!, System.Drawing.FontStyle.Bold)
        Me.lblFromAllocatedCap.Location = New System.Drawing.Point(516, 204)
        Me.lblFromAllocatedCap.Name = "lblFromAllocatedCap"
        Me.lblFromAllocatedCap.Size = New System.Drawing.Size(120, 24)
        Me.lblFromAllocatedCap.TabIndex = 9
        Me.lblFromAllocatedCap.Text = "إجمالي الاعتماد"
        Me.lblFromAllocatedCap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFromAllocated
        '
        Me.lblFromAllocated.BackColor = System.Drawing.Color.White
        Me.lblFromAllocated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFromAllocated.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.lblFromAllocated.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblFromAllocated.Location = New System.Drawing.Point(516, 229)
        Me.lblFromAllocated.Name = "lblFromAllocated"
        Me.lblFromAllocated.Size = New System.Drawing.Size(120, 28)
        Me.lblFromAllocated.TabIndex = 10
        Me.lblFromAllocated.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFromSpentCap
        '
        Me.lblFromSpentCap.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.lblFromSpentCap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFromSpentCap.Font = New System.Drawing.Font("Segoe UI Semibold", 8.75!, System.Drawing.FontStyle.Bold)
        Me.lblFromSpentCap.Location = New System.Drawing.Point(390, 204)
        Me.lblFromSpentCap.Name = "lblFromSpentCap"
        Me.lblFromSpentCap.Size = New System.Drawing.Size(120, 24)
        Me.lblFromSpentCap.TabIndex = 11
        Me.lblFromSpentCap.Text = "إجمالي المصروف"
        Me.lblFromSpentCap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFromSpent
        '
        Me.lblFromSpent.BackColor = System.Drawing.Color.White
        Me.lblFromSpent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFromSpent.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.lblFromSpent.ForeColor = System.Drawing.Color.DarkRed
        Me.lblFromSpent.Location = New System.Drawing.Point(390, 229)
        Me.lblFromSpent.Name = "lblFromSpent"
        Me.lblFromSpent.Size = New System.Drawing.Size(120, 28)
        Me.lblFromSpent.TabIndex = 12
        Me.lblFromSpent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFromReservedCap
        '
        Me.lblFromReservedCap.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.lblFromReservedCap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFromReservedCap.Font = New System.Drawing.Font("Segoe UI Semibold", 8.75!, System.Drawing.FontStyle.Bold)
        Me.lblFromReservedCap.Location = New System.Drawing.Point(264, 204)
        Me.lblFromReservedCap.Name = "lblFromReservedCap"
        Me.lblFromReservedCap.Size = New System.Drawing.Size(120, 24)
        Me.lblFromReservedCap.TabIndex = 13
        Me.lblFromReservedCap.Text = "إجمالي الحجوزات"
        Me.lblFromReservedCap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFromReserved
        '
        Me.lblFromReserved.BackColor = System.Drawing.Color.White
        Me.lblFromReserved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFromReserved.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.lblFromReserved.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblFromReserved.Location = New System.Drawing.Point(264, 229)
        Me.lblFromReserved.Name = "lblFromReserved"
        Me.lblFromReserved.Size = New System.Drawing.Size(120, 28)
        Me.lblFromReserved.TabIndex = 14
        Me.lblFromReserved.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFromAvailableCap
        '
        Me.lblFromAvailableCap.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.lblFromAvailableCap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFromAvailableCap.Font = New System.Drawing.Font("Segoe UI Semibold", 8.75!, System.Drawing.FontStyle.Bold)
        Me.lblFromAvailableCap.Location = New System.Drawing.Point(138, 204)
        Me.lblFromAvailableCap.Name = "lblFromAvailableCap"
        Me.lblFromAvailableCap.Size = New System.Drawing.Size(120, 24)
        Me.lblFromAvailableCap.TabIndex = 15
        Me.lblFromAvailableCap.Text = "الرصيد المتاح"
        Me.lblFromAvailableCap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFromAvailable
        '
        Me.lblFromAvailable.BackColor = System.Drawing.Color.White
        Me.lblFromAvailable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFromAvailable.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.lblFromAvailable.ForeColor = System.Drawing.Color.Blue
        Me.lblFromAvailable.Location = New System.Drawing.Point(138, 229)
        Me.lblFromAvailable.Name = "lblFromAvailable"
        Me.lblFromAvailable.Size = New System.Drawing.Size(120, 28)
        Me.lblFromAvailable.TabIndex = 16
        Me.lblFromAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFromAfterCap
        '
        Me.lblFromAfterCap.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.lblFromAfterCap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFromAfterCap.Font = New System.Drawing.Font("Segoe UI Semibold", 8.75!, System.Drawing.FontStyle.Bold)
        Me.lblFromAfterCap.Location = New System.Drawing.Point(12, 204)
        Me.lblFromAfterCap.Name = "lblFromAfterCap"
        Me.lblFromAfterCap.Size = New System.Drawing.Size(120, 24)
        Me.lblFromAfterCap.TabIndex = 17
        Me.lblFromAfterCap.Text = "بعد المناقلة"
        Me.lblFromAfterCap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFromAfter
        '
        Me.lblFromAfter.BackColor = System.Drawing.Color.White
        Me.lblFromAfter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFromAfter.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.lblFromAfter.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblFromAfter.Location = New System.Drawing.Point(12, 229)
        Me.lblFromAfter.Name = "lblFromAfter"
        Me.lblFromAfter.Size = New System.Drawing.Size(120, 28)
        Me.lblFromAfter.TabIndex = 18
        Me.lblFromAfter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cardType
        '
        Me.cardType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cardType.BackColor = System.Drawing.Color.White
        Me.cardType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardType.Controls.Add(Me.lblTransferType)
        Me.cardType.Controls.Add(Me.rbTransferAllocation)
        Me.cardType.Controls.Add(Me.rbTransferReserve)
        Me.cardType.Location = New System.Drawing.Point(15, 15)
        Me.cardType.Name = "cardType"
        Me.cardType.Size = New System.Drawing.Size(1320, 70)
        Me.cardType.TabIndex = 4
        '
        'lblTransferType
        '
        Me.lblTransferType.AutoSize = True
        Me.lblTransferType.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblTransferType.Location = New System.Drawing.Point(1184, 25)
        Me.lblTransferType.Name = "lblTransferType"
        Me.lblTransferType.Size = New System.Drawing.Size(115, 19)
        Me.lblTransferType.TabIndex = 0
        Me.lblTransferType.Text = "نوع عملية المناقلة"
        '
        'rbTransferAllocation
        '
        Me.rbTransferAllocation.AutoSize = True
        Me.rbTransferAllocation.Checked = True
        Me.rbTransferAllocation.Location = New System.Drawing.Point(930, 23)
        Me.rbTransferAllocation.Name = "rbTransferAllocation"
        Me.rbTransferAllocation.Size = New System.Drawing.Size(126, 21)
        Me.rbTransferAllocation.TabIndex = 1
        Me.rbTransferAllocation.TabStop = True
        Me.rbTransferAllocation.Text = "مناقلة اعتماد مالي"
        '
        'rbTransferReserve
        '
        Me.rbTransferReserve.AutoSize = True
        Me.rbTransferReserve.Enabled = False
        Me.rbTransferReserve.Location = New System.Drawing.Point(760, 23)
        Me.rbTransferReserve.Name = "rbTransferReserve"
        Me.rbTransferReserve.Size = New System.Drawing.Size(148, 21)
        Me.rbTransferReserve.TabIndex = 2
        Me.rbTransferReserve.Text = "تحويل حجز قائم - لاحقًا"
        '
        'pnlActions
        '
        Me.pnlActions.BackColor = System.Drawing.Color.White
        Me.pnlActions.Controls.Add(Me.btnTransfer)
        Me.pnlActions.Controls.Add(Me.btnRefresh)
        Me.pnlActions.Controls.Add(Me.btnExit)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlActions.Location = New System.Drawing.Point(0, 749)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Size = New System.Drawing.Size(1350, 49)
        Me.pnlActions.TabIndex = 1
        '
        'btnTransfer
        '
        Me.btnTransfer.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(52, Byte), Integer))
        Me.btnTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTransfer.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnTransfer.ForeColor = System.Drawing.Color.White
        Me.btnTransfer.Location = New System.Drawing.Point(1136, 7)
        Me.btnTransfer.Name = "btnTransfer"
        Me.btnTransfer.Size = New System.Drawing.Size(190, 36)
        Me.btnTransfer.TabIndex = 0
        Me.btnTransfer.Text = "✓ تنفيذ مناقلة الاعتماد"
        Me.btnTransfer.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(59, Byte), Integer))
        Me.btnRefresh.Location = New System.Drawing.Point(976, 7)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(150, 36)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "↻ تحديث البيانات"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(836, 7)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(130, 36)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "⟵ خروج"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 798)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1350, 22)
        Me.StatusStrip1.TabIndex = 3
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(30, 17)
        Me.lblStatus.Text = "جاهز"
        '
        'FrmBudgetTransfer
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1350, 820)
        Me.Controls.Add(Me.pnlContent)
        Me.Controls.Add(Me.pnlActions)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.MinimumSize = New System.Drawing.Size(1050, 650)
        Me.Name = "FrmBudgetTransfer"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "مناقلة اعتماد بين بنود الموازنة"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlContent.ResumeLayout(False)
        Me.cardGrid.ResumeLayout(False)
        CType(Me.dgvTransfers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cardAmountNotes.ResumeLayout(False)
        Me.cardAmountNotes.PerformLayout()
        Me.cardTo.ResumeLayout(False)
        Me.cardTo.PerformLayout()
        Me.cardFrom.ResumeLayout(False)
        Me.cardFrom.PerformLayout()
        Me.cardType.ResumeLayout(False)
        Me.cardType.PerformLayout()
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

    Friend WithEvents cardType As Panel
    Friend WithEvents lblTransferType As Label
    Friend WithEvents rbTransferAllocation As RadioButton
    Friend WithEvents rbTransferReserve As RadioButton

    Friend WithEvents cardFrom As Panel
    Friend WithEvents lblFromTitle As Label
    Friend WithEvents cmbYearFrom As ComboBox
    Friend WithEvents cmbDoorFrom As ComboBox
    Friend WithEvents cmbChapterFrom As ComboBox
    Friend WithEvents cmbItemFrom As ComboBox
    Friend WithEvents lblYearFrom As Label
    Friend WithEvents lblDoorFrom As Label
    Friend WithEvents lblChapterFrom As Label
    Friend WithEvents lblItemFrom As Label

    Friend WithEvents lblFromAllocatedCap As Label
    Friend WithEvents lblFromSpentCap As Label
    Friend WithEvents lblFromReservedCap As Label
    Friend WithEvents lblFromAvailableCap As Label
    Friend WithEvents lblFromAllocated As Label
    Friend WithEvents lblFromSpent As Label
    Friend WithEvents lblFromReserved As Label
    Friend WithEvents lblFromAvailable As Label
    Friend WithEvents lblFromAfterCap As Label
    Friend WithEvents lblFromAfter As Label

    Friend WithEvents cardTo As Panel
    Friend WithEvents lblToTitle As Label
    Friend WithEvents cmbYearTo As ComboBox
    Friend WithEvents cmbDoorTo As ComboBox
    Friend WithEvents cmbChapterTo As ComboBox
    Friend WithEvents cmbItemTo As ComboBox
    Friend WithEvents lblYearTo As Label
    Friend WithEvents lblDoorTo As Label
    Friend WithEvents lblChapterTo As Label
    Friend WithEvents lblItemTo As Label
    Friend WithEvents lblToAllocatedCap As Label
    Friend WithEvents lblToSpentCap As Label
    Friend WithEvents lblToReservedCap As Label
    Friend WithEvents lblToAvailableCap As Label
    Friend WithEvents lblToAllocated As Label
    Friend WithEvents lblToSpent As Label
    Friend WithEvents lblToReserved As Label
    Friend WithEvents lblToAvailable As Label
    Friend WithEvents lblToAfterCap As Label
    Friend WithEvents lblToAfter As Label

    Friend WithEvents cardAmountNotes As Panel
    Friend WithEvents txtAmount As TextBox
    Friend WithEvents lblAmount As Label
    Friend WithEvents lblAmountWords As Label
    Friend WithEvents txtAmountWords As TextBox
    Friend WithEvents txtNotes As TextBox
    Friend WithEvents lblNotes As Label
    Friend WithEvents txtDecisionNo As TextBox
    Friend WithEvents dtpDecisionDate As DateTimePicker
    Friend WithEvents lblDecisionNo As Label
    Friend WithEvents lblDecisionDate As Label

    Friend WithEvents cardGrid As Panel
    Friend WithEvents dgvTransfers As DataGridView

    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnTransfer As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnExit As Button

    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel

End Class
