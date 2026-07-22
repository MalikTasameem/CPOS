<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmXprinterInvoicePrinters
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblSubTitle = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.splitMain = New System.Windows.Forms.SplitContainer()
        Me.dgvPrinters = New System.Windows.Forms.DataGridView()
        Me.PrinterNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DriverNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PortNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrinterIpColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PortNumberColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StatusColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IsDefaultColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.pnlRight = New System.Windows.Forms.Panel()
        Me.grpNetwork = New System.Windows.Forms.GroupBox()
        Me.btnApplyStaticIpToWindows = New System.Windows.Forms.Button()
        Me.btnCheckStaticIp = New System.Windows.Forms.Button()
        Me.btnOpenPrinterWebPage = New System.Windows.Forms.Button()
        Me.lblFutureNote = New System.Windows.Forms.Label()
        Me.txtGateway = New System.Windows.Forms.TextBox()
        Me.lblGateway = New System.Windows.Forms.Label()
        Me.txtSubnetMask = New System.Windows.Forms.TextBox()
        Me.lblSubnetMask = New System.Windows.Forms.Label()
        Me.txtNewPrinterIp = New System.Windows.Forms.TextBox()
        Me.lblNewPrinterIp = New System.Windows.Forms.Label()
        Me.grpPrinter = New System.Windows.Forms.GroupBox()
        Me.cmbDriverName = New System.Windows.Forms.ComboBox()
        Me.lblPort = New System.Windows.Forms.Label()
        Me.nudPort = New System.Windows.Forms.NumericUpDown()
        Me.txtPrinterIp = New System.Windows.Forms.TextBox()
        Me.lblPrinterIp = New System.Windows.Forms.Label()
        Me.lblDriverName = New System.Windows.Forms.Label()
        Me.txtPrinterName = New System.Windows.Forms.TextBox()
        Me.lblPrinterName = New System.Windows.Forms.Label()
        Me.pnlCommands = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnPrintTest = New System.Windows.Forms.Button()
        Me.btnInstallPrinter = New System.Windows.Forms.Button()
        Me.btnCheckConnection = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlHeader.SuspendLayout()
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMain.Panel1.SuspendLayout()
        Me.splitMain.Panel2.SuspendLayout()
        Me.splitMain.SuspendLayout()
        CType(Me.dgvPrinters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRight.SuspendLayout()
        Me.grpNetwork.SuspendLayout()
        Me.grpPrinter.SuspendLayout()
        CType(Me.nudPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCommands.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblSubTitle)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1120, 76)
        Me.pnlHeader.TabIndex = 0
        '
        'lblSubTitle
        '
        Me.lblSubTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSubTitle.Font = New System.Drawing.Font("Segoe UI", 9.25!)
        Me.lblSubTitle.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblSubTitle.Location = New System.Drawing.Point(362, 44)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblSubTitle.Size = New System.Drawing.Size(730, 22)
        Me.lblSubTitle.TabIndex = 1
        Me.lblSubTitle.Text = "تعريف الطابعة على Windows وربطها بعنوان IP. تغيير IP داخل الطابعة سيكون مرحلة لاحقة حسب الموديل."
        Me.lblSubTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 17.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(621, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblTitle.Size = New System.Drawing.Size(471, 34)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "إدارة طابعات فواتير XPRINTER"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'splitMain
        '
        Me.splitMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.splitMain.Location = New System.Drawing.Point(0, 76)
        Me.splitMain.Name = "splitMain"
        '
        'splitMain.Panel1
        '
        Me.splitMain.Panel1.Controls.Add(Me.dgvPrinters)
        Me.splitMain.Panel1.Padding = New System.Windows.Forms.Padding(10)
        Me.splitMain.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        '
        'splitMain.Panel2
        '
        Me.splitMain.Panel2.Controls.Add(Me.pnlRight)
        Me.splitMain.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.splitMain.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.splitMain.Size = New System.Drawing.Size(1120, 624)
        Me.splitMain.SplitterDistance = 676
        Me.splitMain.TabIndex = 1
        '
        'dgvPrinters
        '
        Me.dgvPrinters.AllowUserToAddRows = False
        Me.dgvPrinters.AllowUserToDeleteRows = False
        Me.dgvPrinters.AllowUserToResizeRows = False
        Me.dgvPrinters.AutoGenerateColumns = False
        Me.dgvPrinters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPrinters.BackgroundColor = System.Drawing.Color.White
        Me.dgvPrinters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dgvPrinters.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Single
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(229, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(235, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPrinters.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPrinters.ColumnHeadersHeight = 34
        Me.dgvPrinters.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PrinterNameColumn, Me.DriverNameColumn, Me.PortNameColumn, Me.PrinterIpColumn, Me.PortNumberColumn, Me.StatusColumn, Me.IsDefaultColumn})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPrinters.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvPrinters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPrinters.EnableHeadersVisualStyles = False
        Me.dgvPrinters.GridColor = System.Drawing.Color.Gainsboro
        Me.dgvPrinters.Location = New System.Drawing.Point(10, 10)
        Me.dgvPrinters.MultiSelect = False
        Me.dgvPrinters.Name = "dgvPrinters"
        Me.dgvPrinters.ReadOnly = True
        Me.dgvPrinters.RowHeadersVisible = False
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.dgvPrinters.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvPrinters.RowTemplate.Height = 30
        Me.dgvPrinters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPrinters.Size = New System.Drawing.Size(656, 604)
        Me.dgvPrinters.TabIndex = 0
        '
        'PrinterNameColumn
        '
        Me.PrinterNameColumn.DataPropertyName = "Name"
        Me.PrinterNameColumn.HeaderText = "اسم الطابعة"
        Me.PrinterNameColumn.Name = "PrinterNameColumn"
        Me.PrinterNameColumn.ReadOnly = True
        '
        'DriverNameColumn
        '
        Me.DriverNameColumn.DataPropertyName = "DriverName"
        Me.DriverNameColumn.HeaderText = "التعريف"
        Me.DriverNameColumn.Name = "DriverNameColumn"
        Me.DriverNameColumn.ReadOnly = True
        '
        'PortNameColumn
        '
        Me.PortNameColumn.DataPropertyName = "PortName"
        Me.PortNameColumn.HeaderText = "المنفذ"
        Me.PortNameColumn.Name = "PortNameColumn"
        Me.PortNameColumn.ReadOnly = True
        '
        'PrinterIpColumn
        '
        Me.PrinterIpColumn.DataPropertyName = "PrinterIp"
        Me.PrinterIpColumn.HeaderText = "IP"
        Me.PrinterIpColumn.Name = "PrinterIpColumn"
        Me.PrinterIpColumn.ReadOnly = True
        '
        'PortNumberColumn
        '
        Me.PortNumberColumn.DataPropertyName = "PortNumber"
        Me.PortNumberColumn.FillWeight = 55.0!
        Me.PortNumberColumn.HeaderText = "Port"
        Me.PortNumberColumn.Name = "PortNumberColumn"
        Me.PortNumberColumn.ReadOnly = True
        '
        'StatusColumn
        '
        Me.StatusColumn.DataPropertyName = "Status"
        Me.StatusColumn.FillWeight = 65.0!
        Me.StatusColumn.HeaderText = "الحالة"
        Me.StatusColumn.Name = "StatusColumn"
        Me.StatusColumn.ReadOnly = True
        '
        'IsDefaultColumn
        '
        Me.IsDefaultColumn.DataPropertyName = "IsDefault"
        Me.IsDefaultColumn.FillWeight = 55.0!
        Me.IsDefaultColumn.HeaderText = "افتراضية"
        Me.IsDefaultColumn.Name = "IsDefaultColumn"
        Me.IsDefaultColumn.ReadOnly = True
        '
        'pnlRight
        '
        Me.pnlRight.Controls.Add(Me.grpNetwork)
        Me.pnlRight.Controls.Add(Me.grpPrinter)
        Me.pnlRight.Controls.Add(Me.pnlCommands)
        Me.pnlRight.Controls.Add(Me.txtLog)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRight.Location = New System.Drawing.Point(0, 0)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlRight.Size = New System.Drawing.Size(440, 624)
        Me.pnlRight.TabIndex = 0
        '
        'grpNetwork
        '
        Me.grpNetwork.Controls.Add(Me.btnApplyStaticIpToWindows)
        Me.grpNetwork.Controls.Add(Me.btnCheckStaticIp)
        Me.grpNetwork.Controls.Add(Me.btnOpenPrinterWebPage)
        Me.grpNetwork.Controls.Add(Me.lblFutureNote)
        Me.grpNetwork.Controls.Add(Me.txtGateway)
        Me.grpNetwork.Controls.Add(Me.lblGateway)
        Me.grpNetwork.Controls.Add(Me.txtSubnetMask)
        Me.grpNetwork.Controls.Add(Me.lblSubnetMask)
        Me.grpNetwork.Controls.Add(Me.txtNewPrinterIp)
        Me.grpNetwork.Controls.Add(Me.lblNewPrinterIp)
        Me.grpNetwork.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpNetwork.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.grpNetwork.Location = New System.Drawing.Point(10, 204)
        Me.grpNetwork.Name = "grpNetwork"
        Me.grpNetwork.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.grpNetwork.Size = New System.Drawing.Size(420, 222)
        Me.grpNetwork.TabIndex = 1
        Me.grpNetwork.TabStop = False
        Me.grpNetwork.Text = "تخصيص Static IP للطابعة"
        '
        'btnApplyStaticIpToWindows
        '
        Me.btnApplyStaticIpToWindows.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnApplyStaticIpToWindows.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnApplyStaticIpToWindows.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnApplyStaticIpToWindows.ForeColor = System.Drawing.Color.White
        Me.btnApplyStaticIpToWindows.Location = New System.Drawing.Point(145, 128)
        Me.btnApplyStaticIpToWindows.Name = "btnApplyStaticIpToWindows"
        Me.btnApplyStaticIpToWindows.Size = New System.Drawing.Size(128, 31)
        Me.btnApplyStaticIpToWindows.TabIndex = 9
        Me.btnApplyStaticIpToWindows.Text = "⇄ تحديث Windows"
        Me.btnApplyStaticIpToWindows.UseVisualStyleBackColor = False
        '
        'btnCheckStaticIp
        '
        Me.btnCheckStaticIp.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btnCheckStaticIp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCheckStaticIp.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnCheckStaticIp.ForeColor = System.Drawing.Color.White
        Me.btnCheckStaticIp.Location = New System.Drawing.Point(279, 128)
        Me.btnCheckStaticIp.Name = "btnCheckStaticIp"
        Me.btnCheckStaticIp.Size = New System.Drawing.Size(127, 31)
        Me.btnCheckStaticIp.TabIndex = 8
        Me.btnCheckStaticIp.Text = "✓ فحص Static IP"
        Me.btnCheckStaticIp.UseVisualStyleBackColor = False
        '
        'btnOpenPrinterWebPage
        '
        Me.btnOpenPrinterWebPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenPrinterWebPage.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.btnOpenPrinterWebPage.Location = New System.Drawing.Point(15, 128)
        Me.btnOpenPrinterWebPage.Name = "btnOpenPrinterWebPage"
        Me.btnOpenPrinterWebPage.Size = New System.Drawing.Size(124, 31)
        Me.btnOpenPrinterWebPage.TabIndex = 7
        Me.btnOpenPrinterWebPage.Text = "⌂ فتح صفحة الطابعة"
        Me.btnOpenPrinterWebPage.UseVisualStyleBackColor = True
        '
        'lblFutureNote
        '
        Me.lblFutureNote.Font = New System.Drawing.Font("Segoe UI", 8.75!)
        Me.lblFutureNote.ForeColor = System.Drawing.Color.DimGray
        Me.lblFutureNote.Location = New System.Drawing.Point(15, 165)
        Me.lblFutureNote.Name = "lblFutureNote"
        Me.lblFutureNote.Size = New System.Drawing.Size(391, 48)
        Me.lblFutureNote.TabIndex = 6
        Me.lblFutureNote.Text = "غيّر IP من صفحة الطابعة ثم اضغط تحديث Windows. الفحص يساعدك على تجنب اختيار عنوان مستخدم مسبقاً."
        Me.lblFutureNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtGateway
        '
        Me.txtGateway.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGateway.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtGateway.Location = New System.Drawing.Point(15, 92)
        Me.txtGateway.Name = "txtGateway"
        Me.txtGateway.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGateway.Size = New System.Drawing.Size(260, 25)
        Me.txtGateway.TabIndex = 5
        Me.txtGateway.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblGateway
        '
        Me.lblGateway.Font = New System.Drawing.Font("Segoe UI", 9.25!)
        Me.lblGateway.Location = New System.Drawing.Point(281, 92)
        Me.lblGateway.Name = "lblGateway"
        Me.lblGateway.Size = New System.Drawing.Size(125, 25)
        Me.lblGateway.TabIndex = 4
        Me.lblGateway.Text = "Gateway"
        Me.lblGateway.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSubnetMask
        '
        Me.txtSubnetMask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSubnetMask.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtSubnetMask.Location = New System.Drawing.Point(15, 59)
        Me.txtSubnetMask.Name = "txtSubnetMask"
        Me.txtSubnetMask.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubnetMask.Size = New System.Drawing.Size(260, 25)
        Me.txtSubnetMask.TabIndex = 3
        Me.txtSubnetMask.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblSubnetMask
        '
        Me.lblSubnetMask.Font = New System.Drawing.Font("Segoe UI", 9.25!)
        Me.lblSubnetMask.Location = New System.Drawing.Point(281, 59)
        Me.lblSubnetMask.Name = "lblSubnetMask"
        Me.lblSubnetMask.Size = New System.Drawing.Size(125, 25)
        Me.lblSubnetMask.TabIndex = 2
        Me.lblSubnetMask.Text = "Subnet Mask"
        Me.lblSubnetMask.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNewPrinterIp
        '
        Me.txtNewPrinterIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewPrinterIp.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtNewPrinterIp.Location = New System.Drawing.Point(15, 26)
        Me.txtNewPrinterIp.Name = "txtNewPrinterIp"
        Me.txtNewPrinterIp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNewPrinterIp.Size = New System.Drawing.Size(260, 25)
        Me.txtNewPrinterIp.TabIndex = 1
        Me.txtNewPrinterIp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblNewPrinterIp
        '
        Me.lblNewPrinterIp.Font = New System.Drawing.Font("Segoe UI", 9.25!)
        Me.lblNewPrinterIp.Location = New System.Drawing.Point(281, 26)
        Me.lblNewPrinterIp.Name = "lblNewPrinterIp"
        Me.lblNewPrinterIp.Size = New System.Drawing.Size(125, 25)
        Me.lblNewPrinterIp.TabIndex = 0
        Me.lblNewPrinterIp.Text = "IP الجديد"
        Me.lblNewPrinterIp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grpPrinter
        '
        Me.grpPrinter.Controls.Add(Me.cmbDriverName)
        Me.grpPrinter.Controls.Add(Me.lblPort)
        Me.grpPrinter.Controls.Add(Me.nudPort)
        Me.grpPrinter.Controls.Add(Me.txtPrinterIp)
        Me.grpPrinter.Controls.Add(Me.lblPrinterIp)
        Me.grpPrinter.Controls.Add(Me.lblDriverName)
        Me.grpPrinter.Controls.Add(Me.txtPrinterName)
        Me.grpPrinter.Controls.Add(Me.lblPrinterName)
        Me.grpPrinter.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpPrinter.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.grpPrinter.Location = New System.Drawing.Point(10, 10)
        Me.grpPrinter.Name = "grpPrinter"
        Me.grpPrinter.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.grpPrinter.Size = New System.Drawing.Size(420, 194)
        Me.grpPrinter.TabIndex = 0
        Me.grpPrinter.TabStop = False
        Me.grpPrinter.Text = "تعريف الطابعة على Windows"
        '
        'cmbDriverName
        '
        Me.cmbDriverName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.cmbDriverName.FormattingEnabled = True
        Me.cmbDriverName.Location = New System.Drawing.Point(15, 72)
        Me.cmbDriverName.Name = "cmbDriverName"
        Me.cmbDriverName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbDriverName.Size = New System.Drawing.Size(260, 25)
        Me.cmbDriverName.TabIndex = 3
        '
        'lblPort
        '
        Me.lblPort.Font = New System.Drawing.Font("Segoe UI", 9.25!)
        Me.lblPort.Location = New System.Drawing.Point(281, 143)
        Me.lblPort.Name = "lblPort"
        Me.lblPort.Size = New System.Drawing.Size(125, 25)
        Me.lblPort.TabIndex = 6
        Me.lblPort.Text = "رقم المنفذ"
        Me.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'nudPort
        '
        Me.nudPort.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.nudPort.Location = New System.Drawing.Point(15, 143)
        Me.nudPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudPort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudPort.Name = "nudPort"
        Me.nudPort.Size = New System.Drawing.Size(260, 25)
        Me.nudPort.TabIndex = 7
        Me.nudPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nudPort.Value = New Decimal(New Integer() {9100, 0, 0, 0})
        '
        'txtPrinterIp
        '
        Me.txtPrinterIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrinterIp.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtPrinterIp.Location = New System.Drawing.Point(15, 109)
        Me.txtPrinterIp.Name = "txtPrinterIp"
        Me.txtPrinterIp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrinterIp.Size = New System.Drawing.Size(260, 25)
        Me.txtPrinterIp.TabIndex = 5
        Me.txtPrinterIp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblPrinterIp
        '
        Me.lblPrinterIp.Font = New System.Drawing.Font("Segoe UI", 9.25!)
        Me.lblPrinterIp.Location = New System.Drawing.Point(281, 109)
        Me.lblPrinterIp.Name = "lblPrinterIp"
        Me.lblPrinterIp.Size = New System.Drawing.Size(125, 25)
        Me.lblPrinterIp.TabIndex = 4
        Me.lblPrinterIp.Text = "IP الطابعة"
        Me.lblPrinterIp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDriverName
        '
        Me.lblDriverName.Font = New System.Drawing.Font("Segoe UI", 9.25!)
        Me.lblDriverName.Location = New System.Drawing.Point(281, 72)
        Me.lblDriverName.Name = "lblDriverName"
        Me.lblDriverName.Size = New System.Drawing.Size(125, 25)
        Me.lblDriverName.TabIndex = 2
        Me.lblDriverName.Text = "تعريف XPRINTER"
        Me.lblDriverName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPrinterName
        '
        Me.txtPrinterName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrinterName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtPrinterName.Location = New System.Drawing.Point(15, 35)
        Me.txtPrinterName.Name = "txtPrinterName"
        Me.txtPrinterName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrinterName.Size = New System.Drawing.Size(260, 25)
        Me.txtPrinterName.TabIndex = 1
        Me.txtPrinterName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblPrinterName
        '
        Me.lblPrinterName.Font = New System.Drawing.Font("Segoe UI", 9.25!)
        Me.lblPrinterName.Location = New System.Drawing.Point(281, 35)
        Me.lblPrinterName.Name = "lblPrinterName"
        Me.lblPrinterName.Size = New System.Drawing.Size(125, 25)
        Me.lblPrinterName.TabIndex = 0
        Me.lblPrinterName.Text = "اسم الطابعة"
        Me.lblPrinterName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlCommands
        '
        Me.pnlCommands.Controls.Add(Me.btnClose)
        Me.pnlCommands.Controls.Add(Me.btnPrintTest)
        Me.pnlCommands.Controls.Add(Me.btnInstallPrinter)
        Me.pnlCommands.Controls.Add(Me.btnCheckConnection)
        Me.pnlCommands.Controls.Add(Me.btnRefresh)
        Me.pnlCommands.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCommands.Location = New System.Drawing.Point(10, 426)
        Me.pnlCommands.Name = "pnlCommands"
        Me.pnlCommands.Size = New System.Drawing.Size(420, 100)
        Me.pnlCommands.TabIndex = 2
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(15, 54)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(120, 36)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "× خروج"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnPrintTest
        '
        Me.btnPrintTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintTest.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.btnPrintTest.Location = New System.Drawing.Point(141, 54)
        Me.btnPrintTest.Name = "btnPrintTest"
        Me.btnPrintTest.Size = New System.Drawing.Size(128, 36)
        Me.btnPrintTest.TabIndex = 3
        Me.btnPrintTest.Text = "⎙ طباعة اختبار"
        Me.btnPrintTest.UseVisualStyleBackColor = True
        '
        'btnInstallPrinter
        '
        Me.btnInstallPrinter.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(163, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btnInstallPrinter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInstallPrinter.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.btnInstallPrinter.ForeColor = System.Drawing.Color.White
        Me.btnInstallPrinter.Location = New System.Drawing.Point(275, 54)
        Me.btnInstallPrinter.Name = "btnInstallPrinter"
        Me.btnInstallPrinter.Size = New System.Drawing.Size(131, 36)
        Me.btnInstallPrinter.TabIndex = 2
        Me.btnInstallPrinter.Text = "+ تعريف / تحديث"
        Me.btnInstallPrinter.UseVisualStyleBackColor = False
        '
        'btnCheckConnection
        '
        Me.btnCheckConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCheckConnection.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.btnCheckConnection.Location = New System.Drawing.Point(15, 10)
        Me.btnCheckConnection.Name = "btnCheckConnection"
        Me.btnCheckConnection.Size = New System.Drawing.Size(180, 36)
        Me.btnCheckConnection.TabIndex = 1
        Me.btnCheckConnection.Text = "⌁ فحص الاتصال"
        Me.btnCheckConnection.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.Location = New System.Drawing.Point(201, 10)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(205, 36)
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.Text = "↻ تحديث الطابعات والتعريفات"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'txtLog
        '
        Me.txtLog.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(24, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtLog.Font = New System.Drawing.Font("Consolas", 9.0!)
        Me.txtLog.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.txtLog.Location = New System.Drawing.Point(10, 526)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ReadOnly = True
        Me.txtLog.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLog.Size = New System.Drawing.Size(420, 88)
        Me.txtLog.TabIndex = 3
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 700)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StatusStrip1.Size = New System.Drawing.Size(1120, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(117, 17)
        Me.lblStatus.Text = "جاهز لإدارة الطابعات"
        '
        'FrmXprinterInvoicePrinters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1120, 722)
        Me.Controls.Add(Me.splitMain)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MinimumSize = New System.Drawing.Size(1000, 650)
        Me.Name = "FrmXprinterInvoicePrinters"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إدارة طابعات فواتير XPRINTER"
        Me.pnlHeader.ResumeLayout(False)
        Me.splitMain.Panel1.ResumeLayout(False)
        Me.splitMain.Panel2.ResumeLayout(False)
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.ResumeLayout(False)
        CType(Me.dgvPrinters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRight.ResumeLayout(False)
        Me.pnlRight.PerformLayout()
        Me.grpNetwork.ResumeLayout(False)
        Me.grpNetwork.PerformLayout()
        Me.grpPrinter.ResumeLayout(False)
        Me.grpPrinter.PerformLayout()
        CType(Me.nudPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCommands.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblSubTitle As Label
    Friend WithEvents lblTitle As Label
    Friend WithEvents splitMain As SplitContainer
    Friend WithEvents dgvPrinters As DataGridView
    Friend WithEvents pnlRight As Panel
    Friend WithEvents grpPrinter As GroupBox
    Friend WithEvents cmbDriverName As ComboBox
    Friend WithEvents lblPort As Label
    Friend WithEvents nudPort As NumericUpDown
    Friend WithEvents txtPrinterIp As TextBox
    Friend WithEvents lblPrinterIp As Label
    Friend WithEvents lblDriverName As Label
    Friend WithEvents txtPrinterName As TextBox
    Friend WithEvents lblPrinterName As Label
    Friend WithEvents grpNetwork As GroupBox
    Friend WithEvents btnApplyStaticIpToWindows As Button
    Friend WithEvents btnCheckStaticIp As Button
    Friend WithEvents btnOpenPrinterWebPage As Button
    Friend WithEvents lblFutureNote As Label
    Friend WithEvents txtGateway As TextBox
    Friend WithEvents lblGateway As Label
    Friend WithEvents txtSubnetMask As TextBox
    Friend WithEvents lblSubnetMask As Label
    Friend WithEvents txtNewPrinterIp As TextBox
    Friend WithEvents lblNewPrinterIp As Label
    Friend WithEvents pnlCommands As Panel
    Friend WithEvents btnClose As Button
    Friend WithEvents btnPrintTest As Button
    Friend WithEvents btnInstallPrinter As Button
    Friend WithEvents btnCheckConnection As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents txtLog As TextBox
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel
    Friend WithEvents PrinterNameColumn As DataGridViewTextBoxColumn
    Friend WithEvents DriverNameColumn As DataGridViewTextBoxColumn
    Friend WithEvents PortNameColumn As DataGridViewTextBoxColumn
    Friend WithEvents PrinterIpColumn As DataGridViewTextBoxColumn
    Friend WithEvents PortNumberColumn As DataGridViewTextBoxColumn
    Friend WithEvents StatusColumn As DataGridViewTextBoxColumn
    Friend WithEvents IsDefaultColumn As DataGridViewCheckBoxColumn
End Class
