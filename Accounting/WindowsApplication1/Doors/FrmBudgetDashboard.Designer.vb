<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBudgetDashboard
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSubTitle = New System.Windows.Forms.Label()
        Me.pnlFilters = New System.Windows.Forms.Panel()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.cmbYear = New System.Windows.Forms.ComboBox()
        Me.lblDoor = New System.Windows.Forms.Label()
        Me.cmbDoor = New System.Windows.Forms.ComboBox()
        Me.lblChapter = New System.Windows.Forms.Label()
        Me.cmbChapter = New System.Windows.Forms.ComboBox()
        Me.lblItem = New System.Windows.Forms.Label()
        Me.cmbItem = New System.Windows.Forms.ComboBox()
        Me.pnlKpis = New System.Windows.Forms.Panel()
        Me.cardAllocated = New System.Windows.Forms.Panel()
        Me.lblAllocatedCap = New System.Windows.Forms.Label()
        Me.lblAllocatedVal = New System.Windows.Forms.Label()
        Me.cardSpent = New System.Windows.Forms.Panel()
        Me.lblSpentCap = New System.Windows.Forms.Label()
        Me.lblSpentVal = New System.Windows.Forms.Label()
        Me.cardReserved = New System.Windows.Forms.Panel()
        Me.lblReservedCap = New System.Windows.Forms.Label()
        Me.lblReservedVal = New System.Windows.Forms.Label()
        Me.cardAvailable = New System.Windows.Forms.Panel()
        Me.lblAvailableCap = New System.Windows.Forms.Label()
        Me.lblAvailableVal = New System.Windows.Forms.Label()
        Me.pnlContent = New System.Windows.Forms.Panel()
        Me.cardDoors = New System.Windows.Forms.Panel()
        Me.lblDoorsTitle = New System.Windows.Forms.Label()
        Me.dgvDoors = New System.Windows.Forms.DataGridView()
        Me.cardChapters = New System.Windows.Forms.Panel()
        Me.lblChaptersTitle = New System.Windows.Forms.Label()
        Me.dgvChapters = New System.Windows.Forms.DataGridView()
        Me.cardTopItems = New System.Windows.Forms.Panel()
        Me.lblTopItemsTitle = New System.Windows.Forms.Label()
        Me.dgvTopItems = New System.Windows.Forms.DataGridView()
        Me.pnlActions = New System.Windows.Forms.Panel()
        Me.ItemsMV_Print_btn = New System.Windows.Forms.Button()
        Me.Items_Print_btn = New System.Windows.Forms.Button()
        Me.Chapters_Print_btn = New System.Windows.Forms.Button()
        Me.Door_print_Btn = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlHeader.SuspendLayout()
        Me.pnlFilters.SuspendLayout()
        Me.pnlKpis.SuspendLayout()
        Me.cardAllocated.SuspendLayout()
        Me.cardSpent.SuspendLayout()
        Me.cardReserved.SuspendLayout()
        Me.cardAvailable.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.cardDoors.SuspendLayout()
        CType(Me.dgvDoors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cardChapters.SuspendLayout()
        CType(Me.dgvChapters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cardTopItems.SuspendLayout()
        CType(Me.dgvTopItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlActions.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.lblSubTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1400, 51)
        Me.pnlHeader.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(1080, 4)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(192, 30)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "لوحة موقف الموازنة"
        '
        'lblSubTitle
        '
        Me.lblSubTitle.AutoSize = True
        Me.lblSubTitle.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblSubTitle.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblSubTitle.Location = New System.Drawing.Point(530, 9)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Size = New System.Drawing.Size(222, 17)
        Me.lblSubTitle.TabIndex = 1
        Me.lblSubTitle.Text = "مؤشرات فورية للأبواب والفصول والبنود"
        '
        'pnlFilters
        '
        Me.pnlFilters.BackColor = System.Drawing.Color.White
        Me.pnlFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilters.Controls.Add(Me.lblYear)
        Me.pnlFilters.Controls.Add(Me.cmbYear)
        Me.pnlFilters.Controls.Add(Me.lblDoor)
        Me.pnlFilters.Controls.Add(Me.cmbDoor)
        Me.pnlFilters.Controls.Add(Me.lblChapter)
        Me.pnlFilters.Controls.Add(Me.cmbChapter)
        Me.pnlFilters.Controls.Add(Me.lblItem)
        Me.pnlFilters.Controls.Add(Me.cmbItem)
        Me.pnlFilters.Location = New System.Drawing.Point(3, 50)
        Me.pnlFilters.Name = "pnlFilters"
        Me.pnlFilters.Size = New System.Drawing.Size(520, 64)
        Me.pnlFilters.TabIndex = 1
        '
        'lblYear
        '
        Me.lblYear.AutoSize = True
        Me.lblYear.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYear.Location = New System.Drawing.Point(468, 6)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(37, 17)
        Me.lblYear.TabIndex = 0
        Me.lblYear.Text = "السنة"
        '
        'cmbYear
        '
        Me.cmbYear.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbYear.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbYear.Location = New System.Drawing.Point(363, 4)
        Me.cmbYear.Name = "cmbYear"
        Me.cmbYear.Size = New System.Drawing.Size(100, 25)
        Me.cmbYear.TabIndex = 1
        '
        'lblDoor
        '
        Me.lblDoor.AutoSize = True
        Me.lblDoor.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDoor.Location = New System.Drawing.Point(319, 6)
        Me.lblDoor.Name = "lblDoor"
        Me.lblDoor.Size = New System.Drawing.Size(34, 17)
        Me.lblDoor.TabIndex = 2
        Me.lblDoor.Text = "الباب"
        '
        'cmbDoor
        '
        Me.cmbDoor.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmbDoor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbDoor.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDoor.Location = New System.Drawing.Point(8, 4)
        Me.cmbDoor.Name = "cmbDoor"
        Me.cmbDoor.Size = New System.Drawing.Size(307, 25)
        Me.cmbDoor.TabIndex = 3
        '
        'lblChapter
        '
        Me.lblChapter.AutoSize = True
        Me.lblChapter.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChapter.Location = New System.Drawing.Point(467, 33)
        Me.lblChapter.Name = "lblChapter"
        Me.lblChapter.Size = New System.Drawing.Size(44, 17)
        Me.lblChapter.TabIndex = 4
        Me.lblChapter.Text = "الفصل"
        '
        'cmbChapter
        '
        Me.cmbChapter.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmbChapter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChapter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbChapter.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbChapter.Location = New System.Drawing.Point(293, 31)
        Me.cmbChapter.Name = "cmbChapter"
        Me.cmbChapter.Size = New System.Drawing.Size(170, 25)
        Me.cmbChapter.TabIndex = 5
        '
        'lblItem
        '
        Me.lblItem.AutoSize = True
        Me.lblItem.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItem.Location = New System.Drawing.Point(254, 34)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(30, 17)
        Me.lblItem.TabIndex = 6
        Me.lblItem.Text = "البند"
        '
        'cmbItem
        '
        Me.cmbItem.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbItem.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbItem.Location = New System.Drawing.Point(8, 32)
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(240, 25)
        Me.cmbItem.TabIndex = 7
        '
        'pnlKpis
        '
        Me.pnlKpis.Controls.Add(Me.cardAllocated)
        Me.pnlKpis.Controls.Add(Me.cardSpent)
        Me.pnlKpis.Controls.Add(Me.cardReserved)
        Me.pnlKpis.Controls.Add(Me.cardAvailable)
        Me.pnlKpis.Location = New System.Drawing.Point(545, 50)
        Me.pnlKpis.Name = "pnlKpis"
        Me.pnlKpis.Size = New System.Drawing.Size(840, 64)
        Me.pnlKpis.TabIndex = 2
        '
        'cardAllocated
        '
        Me.cardAllocated.BackColor = System.Drawing.Color.White
        Me.cardAllocated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardAllocated.Controls.Add(Me.lblAllocatedCap)
        Me.cardAllocated.Controls.Add(Me.lblAllocatedVal)
        Me.cardAllocated.Location = New System.Drawing.Point(630, 3)
        Me.cardAllocated.Name = "cardAllocated"
        Me.cardAllocated.Size = New System.Drawing.Size(205, 59)
        Me.cardAllocated.TabIndex = 0
        '
        'lblAllocatedCap
        '
        Me.lblAllocatedCap.AutoSize = True
        Me.lblAllocatedCap.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblAllocatedCap.Location = New System.Drawing.Point(145, 5)
        Me.lblAllocatedCap.Name = "lblAllocatedCap"
        Me.lblAllocatedCap.Size = New System.Drawing.Size(54, 19)
        Me.lblAllocatedCap.TabIndex = 0
        Me.lblAllocatedCap.Text = "الاعتماد"
        '
        'lblAllocatedVal
        '
        Me.lblAllocatedVal.AutoSize = True
        Me.lblAllocatedVal.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblAllocatedVal.Location = New System.Drawing.Point(4, 30)
        Me.lblAllocatedVal.Name = "lblAllocatedVal"
        Me.lblAllocatedVal.Size = New System.Drawing.Size(50, 21)
        Me.lblAllocatedVal.TabIndex = 1
        Me.lblAllocatedVal.Text = "0.000"
        '
        'cardSpent
        '
        Me.cardSpent.BackColor = System.Drawing.Color.White
        Me.cardSpent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardSpent.Controls.Add(Me.lblSpentCap)
        Me.cardSpent.Controls.Add(Me.lblSpentVal)
        Me.cardSpent.Location = New System.Drawing.Point(420, 3)
        Me.cardSpent.Name = "cardSpent"
        Me.cardSpent.Size = New System.Drawing.Size(200, 59)
        Me.cardSpent.TabIndex = 1
        '
        'lblSpentCap
        '
        Me.lblSpentCap.AutoSize = True
        Me.lblSpentCap.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblSpentCap.Location = New System.Drawing.Point(128, 4)
        Me.lblSpentCap.Name = "lblSpentCap"
        Me.lblSpentCap.Size = New System.Drawing.Size(65, 19)
        Me.lblSpentCap.TabIndex = 0
        Me.lblSpentCap.Text = "المصروف"
        '
        'lblSpentVal
        '
        Me.lblSpentVal.AutoSize = True
        Me.lblSpentVal.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblSpentVal.Location = New System.Drawing.Point(3, 33)
        Me.lblSpentVal.Name = "lblSpentVal"
        Me.lblSpentVal.Size = New System.Drawing.Size(50, 21)
        Me.lblSpentVal.TabIndex = 1
        Me.lblSpentVal.Text = "0.000"
        '
        'cardReserved
        '
        Me.cardReserved.BackColor = System.Drawing.Color.White
        Me.cardReserved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardReserved.Controls.Add(Me.lblReservedCap)
        Me.cardReserved.Controls.Add(Me.lblReservedVal)
        Me.cardReserved.Location = New System.Drawing.Point(210, 3)
        Me.cardReserved.Name = "cardReserved"
        Me.cardReserved.Size = New System.Drawing.Size(200, 59)
        Me.cardReserved.TabIndex = 2
        '
        'lblReservedCap
        '
        Me.lblReservedCap.AutoSize = True
        Me.lblReservedCap.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblReservedCap.Location = New System.Drawing.Point(139, 4)
        Me.lblReservedCap.Name = "lblReservedCap"
        Me.lblReservedCap.Size = New System.Drawing.Size(55, 19)
        Me.lblReservedCap.TabIndex = 0
        Me.lblReservedCap.Text = "المحجوز"
        '
        'lblReservedVal
        '
        Me.lblReservedVal.AutoSize = True
        Me.lblReservedVal.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblReservedVal.Location = New System.Drawing.Point(4, 34)
        Me.lblReservedVal.Name = "lblReservedVal"
        Me.lblReservedVal.Size = New System.Drawing.Size(50, 21)
        Me.lblReservedVal.TabIndex = 1
        Me.lblReservedVal.Text = "0.000"
        '
        'cardAvailable
        '
        Me.cardAvailable.BackColor = System.Drawing.Color.White
        Me.cardAvailable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardAvailable.Controls.Add(Me.lblAvailableCap)
        Me.cardAvailable.Controls.Add(Me.lblAvailableVal)
        Me.cardAvailable.Location = New System.Drawing.Point(1, 3)
        Me.cardAvailable.Name = "cardAvailable"
        Me.cardAvailable.Size = New System.Drawing.Size(200, 59)
        Me.cardAvailable.TabIndex = 3
        '
        'lblAvailableCap
        '
        Me.lblAvailableCap.AutoSize = True
        Me.lblAvailableCap.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblAvailableCap.Location = New System.Drawing.Point(157, 5)
        Me.lblAvailableCap.Name = "lblAvailableCap"
        Me.lblAvailableCap.Size = New System.Drawing.Size(43, 19)
        Me.lblAvailableCap.TabIndex = 0
        Me.lblAvailableCap.Text = "المتاح"
        '
        'lblAvailableVal
        '
        Me.lblAvailableVal.AutoSize = True
        Me.lblAvailableVal.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblAvailableVal.Location = New System.Drawing.Point(5, 33)
        Me.lblAvailableVal.Name = "lblAvailableVal"
        Me.lblAvailableVal.Size = New System.Drawing.Size(50, 21)
        Me.lblAvailableVal.TabIndex = 1
        Me.lblAvailableVal.Text = "0.000"
        '
        'pnlContent
        '
        Me.pnlContent.Controls.Add(Me.cardDoors)
        Me.pnlContent.Controls.Add(Me.cardChapters)
        Me.pnlContent.Controls.Add(Me.cardTopItems)
        Me.pnlContent.Location = New System.Drawing.Point(0, 116)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Size = New System.Drawing.Size(1397, 621)
        Me.pnlContent.TabIndex = 3
        '
        'cardDoors
        '
        Me.cardDoors.BackColor = System.Drawing.Color.White
        Me.cardDoors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardDoors.Controls.Add(Me.lblDoorsTitle)
        Me.cardDoors.Controls.Add(Me.dgvDoors)
        Me.cardDoors.Location = New System.Drawing.Point(15, 0)
        Me.cardDoors.Name = "cardDoors"
        Me.cardDoors.Size = New System.Drawing.Size(1370, 149)
        Me.cardDoors.TabIndex = 0
        '
        'lblDoorsTitle
        '
        Me.lblDoorsTitle.AutoSize = True
        Me.lblDoorsTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblDoorsTitle.Location = New System.Drawing.Point(1268, 0)
        Me.lblDoorsTitle.Name = "lblDoorsTitle"
        Me.lblDoorsTitle.Size = New System.Drawing.Size(97, 20)
        Me.lblDoorsTitle.TabIndex = 0
        Me.lblDoorsTitle.Text = "موقف الأبواب"
        '
        'dgvDoors
        '
        Me.dgvDoors.AllowUserToAddRows = False
        Me.dgvDoors.AllowUserToDeleteRows = False
        Me.dgvDoors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvDoors.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDoors.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvDoors.Location = New System.Drawing.Point(3, 23)
        Me.dgvDoors.Name = "dgvDoors"
        Me.dgvDoors.ReadOnly = True
        Me.dgvDoors.RowHeadersVisible = False
        Me.dgvDoors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDoors.Size = New System.Drawing.Size(1364, 121)
        Me.dgvDoors.TabIndex = 1
        '
        'cardChapters
        '
        Me.cardChapters.BackColor = System.Drawing.Color.White
        Me.cardChapters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardChapters.Controls.Add(Me.lblChaptersTitle)
        Me.cardChapters.Controls.Add(Me.dgvChapters)
        Me.cardChapters.Location = New System.Drawing.Point(15, 150)
        Me.cardChapters.Name = "cardChapters"
        Me.cardChapters.Size = New System.Drawing.Size(1370, 155)
        Me.cardChapters.TabIndex = 1
        '
        'lblChaptersTitle
        '
        Me.lblChaptersTitle.AutoSize = True
        Me.lblChaptersTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblChaptersTitle.Location = New System.Drawing.Point(1262, 1)
        Me.lblChaptersTitle.Name = "lblChaptersTitle"
        Me.lblChaptersTitle.Size = New System.Drawing.Size(103, 20)
        Me.lblChaptersTitle.TabIndex = 0
        Me.lblChaptersTitle.Text = "موقف الفصول"
        '
        'dgvChapters
        '
        Me.dgvChapters.AllowUserToAddRows = False
        Me.dgvChapters.AllowUserToDeleteRows = False
        Me.dgvChapters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvChapters.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.dgvChapters.Location = New System.Drawing.Point(3, 24)
        Me.dgvChapters.Name = "dgvChapters"
        Me.dgvChapters.ReadOnly = True
        Me.dgvChapters.RowHeadersVisible = False
        Me.dgvChapters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvChapters.Size = New System.Drawing.Size(1364, 126)
        Me.dgvChapters.TabIndex = 1
        '
        'cardTopItems
        '
        Me.cardTopItems.BackColor = System.Drawing.Color.White
        Me.cardTopItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardTopItems.Controls.Add(Me.lblTopItemsTitle)
        Me.cardTopItems.Controls.Add(Me.dgvTopItems)
        Me.cardTopItems.Location = New System.Drawing.Point(15, 307)
        Me.cardTopItems.Name = "cardTopItems"
        Me.cardTopItems.Size = New System.Drawing.Size(1370, 311)
        Me.cardTopItems.TabIndex = 2
        '
        'lblTopItemsTitle
        '
        Me.lblTopItemsTitle.AutoSize = True
        Me.lblTopItemsTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblTopItemsTitle.Location = New System.Drawing.Point(1251, 0)
        Me.lblTopItemsTitle.Name = "lblTopItemsTitle"
        Me.lblTopItemsTitle.Size = New System.Drawing.Size(114, 20)
        Me.lblTopItemsTitle.TabIndex = 0
        Me.lblTopItemsTitle.Text = "أعلى البنود صرفًا"
        '
        'dgvTopItems
        '
        Me.dgvTopItems.AllowUserToAddRows = False
        Me.dgvTopItems.AllowUserToDeleteRows = False
        Me.dgvTopItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTopItems.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.dgvTopItems.Location = New System.Drawing.Point(3, 23)
        Me.dgvTopItems.Name = "dgvTopItems"
        Me.dgvTopItems.ReadOnly = True
        Me.dgvTopItems.RowHeadersVisible = False
        Me.dgvTopItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTopItems.Size = New System.Drawing.Size(1364, 283)
        Me.dgvTopItems.TabIndex = 1
        '
        'pnlActions
        '
        Me.pnlActions.BackColor = System.Drawing.Color.White
        Me.pnlActions.Controls.Add(Me.ItemsMV_Print_btn)
        Me.pnlActions.Controls.Add(Me.Items_Print_btn)
        Me.pnlActions.Controls.Add(Me.Chapters_Print_btn)
        Me.pnlActions.Controls.Add(Me.Door_print_Btn)
        Me.pnlActions.Controls.Add(Me.btnRefresh)
        Me.pnlActions.Controls.Add(Me.btnExit)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlActions.Location = New System.Drawing.Point(0, 743)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Size = New System.Drawing.Size(1400, 55)
        Me.pnlActions.TabIndex = 4
        '
        'ItemsMV_Print_btn
        '
        Me.ItemsMV_Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ItemsMV_Print_btn.Location = New System.Drawing.Point(746, 10)
        Me.ItemsMV_Print_btn.Name = "ItemsMV_Print_btn"
        Me.ItemsMV_Print_btn.Size = New System.Drawing.Size(149, 36)
        Me.ItemsMV_Print_btn.TabIndex = 5
        Me.ItemsMV_Print_btn.Text = "تقرير حركة بند"
        '
        'Items_Print_btn
        '
        Me.Items_Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Items_Print_btn.Location = New System.Drawing.Point(916, 10)
        Me.Items_Print_btn.Name = "Items_Print_btn"
        Me.Items_Print_btn.Size = New System.Drawing.Size(149, 36)
        Me.Items_Print_btn.TabIndex = 4
        Me.Items_Print_btn.Text = "تقرير موقف البنود"
        '
        'Chapters_Print_btn
        '
        Me.Chapters_Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Chapters_Print_btn.Location = New System.Drawing.Point(1071, 10)
        Me.Chapters_Print_btn.Name = "Chapters_Print_btn"
        Me.Chapters_Print_btn.Size = New System.Drawing.Size(149, 36)
        Me.Chapters_Print_btn.TabIndex = 3
        Me.Chapters_Print_btn.Text = "تقرير موقف الفصول"
        '
        'Door_print_Btn
        '
        Me.Door_print_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Door_print_Btn.Location = New System.Drawing.Point(1239, 10)
        Me.Door_print_Btn.Name = "Door_print_Btn"
        Me.Door_print_Btn.Size = New System.Drawing.Size(149, 36)
        Me.Door_print_Btn.TabIndex = 2
        Me.Door_print_Btn.Text = "تقرير موقف الأبواب"
        '
        'btnRefresh
        '
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Location = New System.Drawing.Point(516, 10)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(110, 36)
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.Text = "تحديث"
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(15, 10)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(110, 36)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = "خروج"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 798)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1400, 22)
        Me.StatusStrip1.TabIndex = 5
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(30, 17)
        Me.lblStatus.Text = "جاهز"
        '
        'FrmBudgetDashboard
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1400, 820)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.pnlFilters)
        Me.Controls.Add(Me.pnlKpis)
        Me.Controls.Add(Me.pnlContent)
        Me.Controls.Add(Me.pnlActions)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.MinimumSize = New System.Drawing.Size(1050, 650)
        Me.Name = "FrmBudgetDashboard"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dashboard الموازنة"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlFilters.ResumeLayout(False)
        Me.pnlFilters.PerformLayout()
        Me.pnlKpis.ResumeLayout(False)
        Me.cardAllocated.ResumeLayout(False)
        Me.cardAllocated.PerformLayout()
        Me.cardSpent.ResumeLayout(False)
        Me.cardSpent.PerformLayout()
        Me.cardReserved.ResumeLayout(False)
        Me.cardReserved.PerformLayout()
        Me.cardAvailable.ResumeLayout(False)
        Me.cardAvailable.PerformLayout()
        Me.pnlContent.ResumeLayout(False)
        Me.cardDoors.ResumeLayout(False)
        Me.cardDoors.PerformLayout()
        CType(Me.dgvDoors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cardChapters.ResumeLayout(False)
        Me.cardChapters.PerformLayout()
        CType(Me.dgvChapters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cardTopItems.ResumeLayout(False)
        Me.cardTopItems.PerformLayout()
        CType(Me.dgvTopItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlActions.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubTitle As Label

    Friend WithEvents pnlFilters As Panel
    Friend WithEvents lblYear As Label
    Friend WithEvents cmbYear As ComboBox
    Friend WithEvents lblDoor As Label
    Friend WithEvents cmbDoor As ComboBox
    Friend WithEvents lblChapter As Label
    Friend WithEvents cmbChapter As ComboBox
    Friend WithEvents lblItem As Label
    Friend WithEvents cmbItem As ComboBox

    Friend WithEvents pnlKpis As Panel
    Friend WithEvents cardAllocated As Panel
    Friend WithEvents lblAllocatedCap As Label
    Friend WithEvents lblAllocatedVal As Label
    Friend WithEvents cardSpent As Panel
    Friend WithEvents lblSpentCap As Label
    Friend WithEvents lblSpentVal As Label
    Friend WithEvents cardReserved As Panel
    Friend WithEvents lblReservedCap As Label
    Friend WithEvents lblReservedVal As Label
    Friend WithEvents cardAvailable As Panel
    Friend WithEvents lblAvailableCap As Label
    Friend WithEvents lblAvailableVal As Label

    Friend WithEvents pnlContent As Panel
    Friend WithEvents cardDoors As Panel
    Friend WithEvents lblDoorsTitle As Label
    Friend WithEvents dgvDoors As DataGridView
    Friend WithEvents cardChapters As Panel
    Friend WithEvents lblChaptersTitle As Label
    Friend WithEvents dgvChapters As DataGridView
    Friend WithEvents cardTopItems As Panel
    Friend WithEvents lblTopItemsTitle As Label
    Friend WithEvents dgvTopItems As DataGridView

    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnExit As Button

    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel
    Friend WithEvents Door_print_Btn As Button
    Friend WithEvents Chapters_Print_btn As Button
    Friend WithEvents Items_Print_btn As Button
    Friend WithEvents ItemsMV_Print_btn As Button
End Class
