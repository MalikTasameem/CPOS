<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmExchangeCreate
    Inherits Base_Form
    'System.Windows.Forms.Form
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmExchangeCreate))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PanelHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.PanelMain = New System.Windows.Forms.Panel()
        Me.GB_Notes = New System.Windows.Forms.GroupBox()
        Me.ADD_Doc_btn = New System.Windows.Forms.Button()
        Me.DocGridView = New System.Windows.Forms.DataGridView()
        Me.txt_CustomerIdentityNumber = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_CustomerName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNote = New System.Windows.Forms.TextBox()
        Me.lblNote = New System.Windows.Forms.Label()
        Me.txtReferenceNo = New System.Windows.Forms.TextBox()
        Me.lblReferenceNo = New System.Windows.Forms.Label()
        Me.GB_Calc = New System.Windows.Forms.GroupBox()
        Me.CardNet = New System.Windows.Forms.Panel()
        Me.lblNetLYD = New System.Windows.Forms.Label()
        Me.lblNetCaption = New System.Windows.Forms.Label()
        Me.CardCommission = New System.Windows.Forms.Panel()
        Me.lblCommissionLYD = New System.Windows.Forms.Label()
        Me.lblCommissionCaption = New System.Windows.Forms.Label()
        Me.CardTotal = New System.Windows.Forms.Panel()
        Me.lblTotalLYD = New System.Windows.Forms.Label()
        Me.lblTotalCaption = New System.Windows.Forms.Label()
        Me.GB_Inputs = New System.Windows.Forms.GroupBox()
        Me.txtCommissionPercent = New System.Windows.Forms.TextBox()
        Me.lblCommissionPercent = New System.Windows.Forms.Label()
        Me.numRate = New System.Windows.Forms.NumericUpDown()
        Me.lblRate = New System.Windows.Forms.Label()
        Me.numForeignAmount = New System.Windows.Forms.NumericUpDown()
        Me.lblForeignAmount = New System.Windows.Forms.Label()
        Me.GB_Operation = New System.Windows.Forms.GroupBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Tr_Total_Balance_Lb = New System.Windows.Forms.Label()
        Me.Tittle_Total_balance_Label = New System.Windows.Forms.Label()
        Me.NoRateMsg_Label = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Tr_Balance_Pending_Lb = New System.Windows.Forms.Label()
        Me.Tittle_pendingbalance_Label = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Tr_Balance_Lb = New System.Windows.Forms.Label()
        Me.Tittle_balance_Label = New System.Windows.Forms.Label()
        Me.btnRefreshRate = New System.Windows.Forms.Button()
        Me.cmbCurrency = New System.Windows.Forms.ComboBox()
        Me.cmbVault = New System.Windows.Forms.ComboBox()
        Me.lblCurrency = New System.Windows.Forms.Label()
        Me.lblVault = New System.Windows.Forms.Label()
        Me.cmbOperationType = New System.Windows.Forms.ComboBox()
        Me.lblOperationType = New System.Windows.Forms.Label()
        Me.PanelBottom = New System.Windows.Forms.Panel()
        Me.btn_Print = New System.Windows.Forms.Button()
        Me.btnOpenDetails = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnSavePending = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tsslStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslExchangeId = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.PanelHeader.SuspendLayout()
        Me.PanelMain.SuspendLayout()
        Me.GB_Notes.SuspendLayout()
        CType(Me.DocGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GB_Calc.SuspendLayout()
        Me.CardNet.SuspendLayout()
        Me.CardCommission.SuspendLayout()
        Me.CardTotal.SuspendLayout()
        Me.GB_Inputs.SuspendLayout()
        CType(Me.numRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numForeignAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GB_Operation.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.PanelBottom.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelHeader
        '
        Me.PanelHeader.BackColor = System.Drawing.Color.White
        Me.PanelHeader.Controls.Add(Me.lblTitle)
        Me.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelHeader.Location = New System.Drawing.Point(0, 0)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Padding = New System.Windows.Forms.Padding(12, 10, 12, 10)
        Me.PanelHeader.Size = New System.Drawing.Size(980, 56)
        Me.PanelHeader.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(12, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(956, 36)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "إنشاء عملية صرافة (Pending) - خزنة دينار + عملة + سعر تلقائي"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PanelMain
        '
        Me.PanelMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.PanelMain.Controls.Add(Me.GB_Notes)
        Me.PanelMain.Controls.Add(Me.GB_Calc)
        Me.PanelMain.Controls.Add(Me.GB_Inputs)
        Me.PanelMain.Controls.Add(Me.GB_Operation)
        Me.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelMain.Location = New System.Drawing.Point(0, 56)
        Me.PanelMain.Name = "PanelMain"
        Me.PanelMain.Padding = New System.Windows.Forms.Padding(12)
        Me.PanelMain.Size = New System.Drawing.Size(980, 554)
        Me.PanelMain.TabIndex = 1
        '
        'GB_Notes
        '
        Me.GB_Notes.BackColor = System.Drawing.Color.White
        Me.GB_Notes.Controls.Add(Me.ADD_Doc_btn)
        Me.GB_Notes.Controls.Add(Me.DocGridView)
        Me.GB_Notes.Controls.Add(Me.txt_CustomerIdentityNumber)
        Me.GB_Notes.Controls.Add(Me.Label2)
        Me.GB_Notes.Controls.Add(Me.txt_CustomerName)
        Me.GB_Notes.Controls.Add(Me.Label1)
        Me.GB_Notes.Controls.Add(Me.txtNote)
        Me.GB_Notes.Controls.Add(Me.lblNote)
        Me.GB_Notes.Controls.Add(Me.txtReferenceNo)
        Me.GB_Notes.Controls.Add(Me.lblReferenceNo)
        Me.GB_Notes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GB_Notes.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GB_Notes.Location = New System.Drawing.Point(12, 346)
        Me.GB_Notes.Name = "GB_Notes"
        Me.GB_Notes.Padding = New System.Windows.Forms.Padding(12)
        Me.GB_Notes.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GB_Notes.Size = New System.Drawing.Size(956, 196)
        Me.GB_Notes.TabIndex = 4
        Me.GB_Notes.TabStop = False
        Me.GB_Notes.Text = "ملاحظات"
        '
        'ADD_Doc_btn
        '
        Me.ADD_Doc_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ADD_Doc_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ADD_Doc_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ADD_Doc_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ADD_Doc_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ADD_Doc_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ADD_Doc_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ADD_Doc_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ADD_Doc_btn.Image = CType(resources.GetObject("ADD_Doc_btn.Image"), System.Drawing.Image)
        Me.ADD_Doc_btn.Location = New System.Drawing.Point(674, 99)
        Me.ADD_Doc_btn.Margin = New System.Windows.Forms.Padding(2)
        Me.ADD_Doc_btn.Name = "ADD_Doc_btn"
        Me.ADD_Doc_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ADD_Doc_btn.Size = New System.Drawing.Size(44, 92)
        Me.ADD_Doc_btn.TabIndex = 1121
        Me.ADD_Doc_btn.TabStop = False
        Me.ADD_Doc_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ADD_Doc_btn.UseVisualStyleBackColor = False
        '
        'DocGridView
        '
        Me.DocGridView.AllowUserToAddRows = False
        Me.DocGridView.AllowUserToDeleteRows = False
        Me.DocGridView.AllowUserToResizeRows = False
        Me.DocGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DocGridView.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DocGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DocGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DocGridView.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DocGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2
        Me.DocGridView.Location = New System.Drawing.Point(4, 99)
        Me.DocGridView.Margin = New System.Windows.Forms.Padding(2)
        Me.DocGridView.MultiSelect = False
        Me.DocGridView.Name = "DocGridView"
        Me.DocGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DocGridView.RowTemplate.Height = 25
        Me.DocGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DocGridView.Size = New System.Drawing.Size(669, 92)
        Me.DocGridView.TabIndex = 1120
        '
        'txt_CustomerIdentityNumber
        '
        Me.txt_CustomerIdentityNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_CustomerIdentityNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_CustomerIdentityNumber.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txt_CustomerIdentityNumber.Location = New System.Drawing.Point(4, 15)
        Me.txt_CustomerIdentityNumber.Name = "txt_CustomerIdentityNumber"
        Me.txt_CustomerIdentityNumber.Size = New System.Drawing.Size(231, 27)
        Me.txt_CustomerIdentityNumber.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(239, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 17)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "رقم الهوية"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_CustomerName
        '
        Me.txt_CustomerName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_CustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_CustomerName.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txt_CustomerName.Location = New System.Drawing.Point(310, 15)
        Me.txt_CustomerName.Name = "txt_CustomerName"
        Me.txt_CustomerName.Size = New System.Drawing.Size(408, 27)
        Me.txt_CustomerName.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(722, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 17)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "إسم العميل"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNote
        '
        Me.txtNote.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNote.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txtNote.Location = New System.Drawing.Point(4, 71)
        Me.txtNote.Name = "txtNote"
        Me.txtNote.Size = New System.Drawing.Size(714, 27)
        Me.txtNote.TabIndex = 3
        '
        'lblNote
        '
        Me.lblNote.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNote.AutoSize = True
        Me.lblNote.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblNote.Location = New System.Drawing.Point(722, 75)
        Me.lblNote.Name = "lblNote"
        Me.lblNote.Size = New System.Drawing.Size(49, 17)
        Me.lblNote.TabIndex = 2
        Me.lblNote.Text = "ملاحظة"
        Me.lblNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtReferenceNo
        '
        Me.txtReferenceNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtReferenceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReferenceNo.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.txtReferenceNo.Location = New System.Drawing.Point(4, 43)
        Me.txtReferenceNo.Name = "txtReferenceNo"
        Me.txtReferenceNo.Size = New System.Drawing.Size(714, 27)
        Me.txtReferenceNo.TabIndex = 1
        '
        'lblReferenceNo
        '
        Me.lblReferenceNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblReferenceNo.AutoSize = True
        Me.lblReferenceNo.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblReferenceNo.Location = New System.Drawing.Point(722, 47)
        Me.lblReferenceNo.Name = "lblReferenceNo"
        Me.lblReferenceNo.Size = New System.Drawing.Size(107, 17)
        Me.lblReferenceNo.TabIndex = 0
        Me.lblReferenceNo.Text = "رقم مرجعي / سند"
        Me.lblReferenceNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GB_Calc
        '
        Me.GB_Calc.BackColor = System.Drawing.Color.White
        Me.GB_Calc.Controls.Add(Me.CardNet)
        Me.GB_Calc.Controls.Add(Me.CardCommission)
        Me.GB_Calc.Controls.Add(Me.CardTotal)
        Me.GB_Calc.Dock = System.Windows.Forms.DockStyle.Top
        Me.GB_Calc.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GB_Calc.Location = New System.Drawing.Point(12, 232)
        Me.GB_Calc.Name = "GB_Calc"
        Me.GB_Calc.Padding = New System.Windows.Forms.Padding(12)
        Me.GB_Calc.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GB_Calc.Size = New System.Drawing.Size(956, 114)
        Me.GB_Calc.TabIndex = 3
        Me.GB_Calc.TabStop = False
        Me.GB_Calc.Text = "الحسابات المباشرة"
        '
        'CardNet
        '
        Me.CardNet.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CardNet.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CardNet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CardNet.Controls.Add(Me.lblNetLYD)
        Me.CardNet.Controls.Add(Me.lblNetCaption)
        Me.CardNet.Location = New System.Drawing.Point(15, 33)
        Me.CardNet.Name = "CardNet"
        Me.CardNet.Size = New System.Drawing.Size(300, 66)
        Me.CardNet.TabIndex = 2
        '
        'lblNetLYD
        '
        Me.lblNetLYD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblNetLYD.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblNetLYD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.lblNetLYD.Location = New System.Drawing.Point(0, 22)
        Me.lblNetLYD.Name = "lblNetLYD"
        Me.lblNetLYD.Size = New System.Drawing.Size(298, 42)
        Me.lblNetLYD.TabIndex = 1
        Me.lblNetLYD.Text = "0.000"
        Me.lblNetLYD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNetCaption
        '
        Me.lblNetCaption.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblNetCaption.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblNetCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.lblNetCaption.Location = New System.Drawing.Point(0, 0)
        Me.lblNetCaption.Name = "lblNetCaption"
        Me.lblNetCaption.Size = New System.Drawing.Size(298, 22)
        Me.lblNetCaption.TabIndex = 0
        Me.lblNetCaption.Text = "الصافي (LYD) بعد العمولة"
        Me.lblNetCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CardCommission
        '
        Me.CardCommission.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CardCommission.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.CardCommission.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CardCommission.Controls.Add(Me.lblCommissionLYD)
        Me.CardCommission.Controls.Add(Me.lblCommissionCaption)
        Me.CardCommission.Location = New System.Drawing.Point(321, 33)
        Me.CardCommission.Name = "CardCommission"
        Me.CardCommission.Size = New System.Drawing.Size(300, 66)
        Me.CardCommission.TabIndex = 1
        '
        'lblCommissionLYD
        '
        Me.lblCommissionLYD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCommissionLYD.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblCommissionLYD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCommissionLYD.Location = New System.Drawing.Point(0, 22)
        Me.lblCommissionLYD.Name = "lblCommissionLYD"
        Me.lblCommissionLYD.Size = New System.Drawing.Size(298, 42)
        Me.lblCommissionLYD.TabIndex = 1
        Me.lblCommissionLYD.Text = "0.000"
        Me.lblCommissionLYD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCommissionCaption
        '
        Me.lblCommissionCaption.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblCommissionCaption.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblCommissionCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.lblCommissionCaption.Location = New System.Drawing.Point(0, 0)
        Me.lblCommissionCaption.Name = "lblCommissionCaption"
        Me.lblCommissionCaption.Size = New System.Drawing.Size(298, 22)
        Me.lblCommissionCaption.TabIndex = 0
        Me.lblCommissionCaption.Text = "العمولة (LYD)"
        Me.lblCommissionCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CardTotal
        '
        Me.CardTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CardTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.CardTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CardTotal.Controls.Add(Me.lblTotalLYD)
        Me.CardTotal.Controls.Add(Me.lblTotalCaption)
        Me.CardTotal.Location = New System.Drawing.Point(627, 33)
        Me.CardTotal.Name = "CardTotal"
        Me.CardTotal.Size = New System.Drawing.Size(314, 66)
        Me.CardTotal.TabIndex = 0
        '
        'lblTotalLYD
        '
        Me.lblTotalLYD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotalLYD.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalLYD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblTotalLYD.Location = New System.Drawing.Point(0, 22)
        Me.lblTotalLYD.Name = "lblTotalLYD"
        Me.lblTotalLYD.Size = New System.Drawing.Size(312, 42)
        Me.lblTotalLYD.TabIndex = 1
        Me.lblTotalLYD.Text = "0.000"
        Me.lblTotalLYD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTotalCaption
        '
        Me.lblTotalCaption.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTotalCaption.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.lblTotalCaption.Location = New System.Drawing.Point(0, 0)
        Me.lblTotalCaption.Name = "lblTotalCaption"
        Me.lblTotalCaption.Size = New System.Drawing.Size(312, 22)
        Me.lblTotalCaption.TabIndex = 0
        Me.lblTotalCaption.Text = "الإجمالي (LYD) قبل العمولة"
        Me.lblTotalCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GB_Inputs
        '
        Me.GB_Inputs.BackColor = System.Drawing.Color.White
        Me.GB_Inputs.Controls.Add(Me.txtCommissionPercent)
        Me.GB_Inputs.Controls.Add(Me.lblCommissionPercent)
        Me.GB_Inputs.Controls.Add(Me.numRate)
        Me.GB_Inputs.Controls.Add(Me.lblRate)
        Me.GB_Inputs.Controls.Add(Me.numForeignAmount)
        Me.GB_Inputs.Controls.Add(Me.lblForeignAmount)
        Me.GB_Inputs.Dock = System.Windows.Forms.DockStyle.Top
        Me.GB_Inputs.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GB_Inputs.Location = New System.Drawing.Point(12, 149)
        Me.GB_Inputs.Name = "GB_Inputs"
        Me.GB_Inputs.Padding = New System.Windows.Forms.Padding(12)
        Me.GB_Inputs.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GB_Inputs.Size = New System.Drawing.Size(956, 83)
        Me.GB_Inputs.TabIndex = 2
        Me.GB_Inputs.TabStop = False
        Me.GB_Inputs.Text = "المدخلات"
        '
        'txtCommissionPercent
        '
        Me.txtCommissionPercent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCommissionPercent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCommissionPercent.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtCommissionPercent.Location = New System.Drawing.Point(15, 47)
        Me.txtCommissionPercent.Name = "txtCommissionPercent"
        Me.txtCommissionPercent.ReadOnly = True
        Me.txtCommissionPercent.Size = New System.Drawing.Size(156, 25)
        Me.txtCommissionPercent.TabIndex = 5
        Me.txtCommissionPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblCommissionPercent
        '
        Me.lblCommissionPercent.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCommissionPercent.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblCommissionPercent.Location = New System.Drawing.Point(15, 22)
        Me.lblCommissionPercent.Name = "lblCommissionPercent"
        Me.lblCommissionPercent.Size = New System.Drawing.Size(156, 22)
        Me.lblCommissionPercent.TabIndex = 4
        Me.lblCommissionPercent.Text = "نسبة العمولة %"
        Me.lblCommissionPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'numRate
        '
        Me.numRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numRate.DecimalPlaces = 6
        Me.numRate.Enabled = False
        Me.numRate.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.numRate.Location = New System.Drawing.Point(177, 47)
        Me.numRate.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.numRate.Name = "numRate"
        Me.numRate.Size = New System.Drawing.Size(240, 27)
        Me.numRate.TabIndex = 3
        Me.numRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblRate
        '
        Me.lblRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRate.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblRate.Location = New System.Drawing.Point(177, 22)
        Me.lblRate.Name = "lblRate"
        Me.lblRate.Size = New System.Drawing.Size(240, 22)
        Me.lblRate.TabIndex = 2
        Me.lblRate.Text = "سعر الصرف (تلقائي)"
        Me.lblRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'numForeignAmount
        '
        Me.numForeignAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numForeignAmount.DecimalPlaces = 4
        Me.numForeignAmount.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.numForeignAmount.Location = New System.Drawing.Point(423, 47)
        Me.numForeignAmount.Maximum = New Decimal(New Integer() {1000000000, 0, 0, 0})
        Me.numForeignAmount.Name = "numForeignAmount"
        Me.numForeignAmount.Size = New System.Drawing.Size(518, 27)
        Me.numForeignAmount.TabIndex = 1
        Me.numForeignAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblForeignAmount
        '
        Me.lblForeignAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblForeignAmount.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblForeignAmount.Location = New System.Drawing.Point(423, 22)
        Me.lblForeignAmount.Name = "lblForeignAmount"
        Me.lblForeignAmount.Size = New System.Drawing.Size(518, 22)
        Me.lblForeignAmount.TabIndex = 0
        Me.lblForeignAmount.Text = "المبلغ بالعملة الأجنبية"
        Me.lblForeignAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GB_Operation
        '
        Me.GB_Operation.BackColor = System.Drawing.Color.White
        Me.GB_Operation.Controls.Add(Me.Panel3)
        Me.GB_Operation.Controls.Add(Me.NoRateMsg_Label)
        Me.GB_Operation.Controls.Add(Me.Panel2)
        Me.GB_Operation.Controls.Add(Me.Panel1)
        Me.GB_Operation.Controls.Add(Me.btnRefreshRate)
        Me.GB_Operation.Controls.Add(Me.cmbCurrency)
        Me.GB_Operation.Controls.Add(Me.cmbVault)
        Me.GB_Operation.Controls.Add(Me.lblCurrency)
        Me.GB_Operation.Controls.Add(Me.lblVault)
        Me.GB_Operation.Controls.Add(Me.cmbOperationType)
        Me.GB_Operation.Controls.Add(Me.lblOperationType)
        Me.GB_Operation.Dock = System.Windows.Forms.DockStyle.Top
        Me.GB_Operation.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GB_Operation.Location = New System.Drawing.Point(12, 12)
        Me.GB_Operation.Name = "GB_Operation"
        Me.GB_Operation.Padding = New System.Windows.Forms.Padding(12)
        Me.GB_Operation.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GB_Operation.Size = New System.Drawing.Size(956, 137)
        Me.GB_Operation.TabIndex = 0
        Me.GB_Operation.TabStop = False
        Me.GB_Operation.Text = "بيانات العملية"
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Tr_Total_Balance_Lb)
        Me.Panel3.Controls.Add(Me.Tittle_Total_balance_Label)
        Me.Panel3.Location = New System.Drawing.Point(8, 59)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(401, 45)
        Me.Panel3.TabIndex = 8
        '
        'Tr_Total_Balance_Lb
        '
        Me.Tr_Total_Balance_Lb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tr_Total_Balance_Lb.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Tr_Total_Balance_Lb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.Tr_Total_Balance_Lb.Location = New System.Drawing.Point(0, 22)
        Me.Tr_Total_Balance_Lb.Name = "Tr_Total_Balance_Lb"
        Me.Tr_Total_Balance_Lb.Size = New System.Drawing.Size(399, 21)
        Me.Tr_Total_Balance_Lb.TabIndex = 1
        Me.Tr_Total_Balance_Lb.Text = "0.000"
        Me.Tr_Total_Balance_Lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Tittle_Total_balance_Label
        '
        Me.Tittle_Total_balance_Label.Dock = System.Windows.Forms.DockStyle.Top
        Me.Tittle_Total_balance_Label.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Tittle_Total_balance_Label.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.Tittle_Total_balance_Label.Location = New System.Drawing.Point(0, 0)
        Me.Tittle_Total_balance_Label.Name = "Tittle_Total_balance_Label"
        Me.Tittle_Total_balance_Label.Size = New System.Drawing.Size(399, 22)
        Me.Tittle_Total_balance_Label.TabIndex = 0
        Me.Tittle_Total_balance_Label.Text = "---"
        Me.Tittle_Total_balance_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'NoRateMsg_Label
        '
        Me.NoRateMsg_Label.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NoRateMsg_Label.AutoSize = True
        Me.NoRateMsg_Label.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoRateMsg_Label.ForeColor = System.Drawing.Color.DarkRed
        Me.NoRateMsg_Label.Location = New System.Drawing.Point(682, 109)
        Me.NoRateMsg_Label.Name = "NoRateMsg_Label"
        Me.NoRateMsg_Label.Size = New System.Drawing.Size(241, 20)
        Me.NoRateMsg_Label.TabIndex = 7
        Me.NoRateMsg_Label.Text = "لا يوجد سعر فعال لهذه العملة حالياً !!"
        Me.NoRateMsg_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NoRateMsg_Label.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Tr_Balance_Pending_Lb)
        Me.Panel2.Controls.Add(Me.Tittle_pendingbalance_Label)
        Me.Panel2.Location = New System.Drawing.Point(7, 13)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 45)
        Me.Panel2.TabIndex = 6
        '
        'Tr_Balance_Pending_Lb
        '
        Me.Tr_Balance_Pending_Lb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tr_Balance_Pending_Lb.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Tr_Balance_Pending_Lb.ForeColor = System.Drawing.Color.Red
        Me.Tr_Balance_Pending_Lb.Location = New System.Drawing.Point(0, 22)
        Me.Tr_Balance_Pending_Lb.Name = "Tr_Balance_Pending_Lb"
        Me.Tr_Balance_Pending_Lb.Size = New System.Drawing.Size(198, 21)
        Me.Tr_Balance_Pending_Lb.TabIndex = 1
        Me.Tr_Balance_Pending_Lb.Text = "0.000"
        Me.Tr_Balance_Pending_Lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Tittle_pendingbalance_Label
        '
        Me.Tittle_pendingbalance_Label.Dock = System.Windows.Forms.DockStyle.Top
        Me.Tittle_pendingbalance_Label.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Tittle_pendingbalance_Label.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.Tittle_pendingbalance_Label.Location = New System.Drawing.Point(0, 0)
        Me.Tittle_pendingbalance_Label.Name = "Tittle_pendingbalance_Label"
        Me.Tittle_pendingbalance_Label.Size = New System.Drawing.Size(198, 22)
        Me.Tittle_pendingbalance_Label.TabIndex = 0
        Me.Tittle_pendingbalance_Label.Text = "----"
        Me.Tittle_pendingbalance_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Tr_Balance_Lb)
        Me.Panel1.Controls.Add(Me.Tittle_balance_Label)
        Me.Panel1.Location = New System.Drawing.Point(209, 13)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(200, 45)
        Me.Panel1.TabIndex = 5
        '
        'Tr_Balance_Lb
        '
        Me.Tr_Balance_Lb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tr_Balance_Lb.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Tr_Balance_Lb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.Tr_Balance_Lb.Location = New System.Drawing.Point(0, 22)
        Me.Tr_Balance_Lb.Name = "Tr_Balance_Lb"
        Me.Tr_Balance_Lb.Size = New System.Drawing.Size(198, 21)
        Me.Tr_Balance_Lb.TabIndex = 1
        Me.Tr_Balance_Lb.Text = "0.000"
        Me.Tr_Balance_Lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Tittle_balance_Label
        '
        Me.Tittle_balance_Label.Dock = System.Windows.Forms.DockStyle.Top
        Me.Tittle_balance_Label.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Tittle_balance_Label.ForeColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.Tittle_balance_Label.Location = New System.Drawing.Point(0, 0)
        Me.Tittle_balance_Label.Name = "Tittle_balance_Label"
        Me.Tittle_balance_Label.Size = New System.Drawing.Size(198, 22)
        Me.Tittle_balance_Label.TabIndex = 0
        Me.Tittle_balance_Label.Text = "-----"
        Me.Tittle_balance_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnRefreshRate
        '
        Me.btnRefreshRate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefreshRate.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.btnRefreshRate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.btnRefreshRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefreshRate.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnRefreshRate.Location = New System.Drawing.Point(493, 103)
        Me.btnRefreshRate.Name = "btnRefreshRate"
        Me.btnRefreshRate.Size = New System.Drawing.Size(138, 30)
        Me.btnRefreshRate.TabIndex = 2
        Me.btnRefreshRate.Text = "تحديث السعر"
        Me.btnRefreshRate.UseVisualStyleBackColor = False
        '
        'cmbCurrency
        '
        Me.cmbCurrency.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCurrency.Enabled = False
        Me.cmbCurrency.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbCurrency.FormattingEnabled = True
        Me.cmbCurrency.Location = New System.Drawing.Point(492, 76)
        Me.cmbCurrency.Name = "cmbCurrency"
        Me.cmbCurrency.Size = New System.Drawing.Size(263, 25)
        Me.cmbCurrency.TabIndex = 1
        '
        'cmbVault
        '
        Me.cmbVault.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbVault.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVault.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbVault.FormattingEnabled = True
        Me.cmbVault.Location = New System.Drawing.Point(492, 47)
        Me.cmbVault.Name = "cmbVault"
        Me.cmbVault.Size = New System.Drawing.Size(263, 25)
        Me.cmbVault.TabIndex = 3
        '
        'lblCurrency
        '
        Me.lblCurrency.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCurrency.AutoSize = True
        Me.lblCurrency.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblCurrency.Location = New System.Drawing.Point(758, 80)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(193, 17)
        Me.lblCurrency.TabIndex = 0
        Me.lblCurrency.Text = "العملة الأجنبية (السعر يُجلب تلقائياً)"
        Me.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblVault
        '
        Me.lblVault.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVault.AutoSize = True
        Me.lblVault.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblVault.Location = New System.Drawing.Point(758, 51)
        Me.lblVault.Name = "lblVault"
        Me.lblVault.Size = New System.Drawing.Size(166, 17)
        Me.lblVault.TabIndex = 2
        Me.lblVault.Text = "الخزنة (جميعها بالعملة الليبية)"
        Me.lblVault.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbOperationType
        '
        Me.cmbOperationType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbOperationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOperationType.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbOperationType.FormattingEnabled = True
        Me.cmbOperationType.Location = New System.Drawing.Point(492, 18)
        Me.cmbOperationType.Name = "cmbOperationType"
        Me.cmbOperationType.Size = New System.Drawing.Size(263, 25)
        Me.cmbOperationType.TabIndex = 1
        '
        'lblOperationType
        '
        Me.lblOperationType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOperationType.AutoSize = True
        Me.lblOperationType.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblOperationType.Location = New System.Drawing.Point(758, 23)
        Me.lblOperationType.Name = "lblOperationType"
        Me.lblOperationType.Size = New System.Drawing.Size(127, 17)
        Me.lblOperationType.TabIndex = 0
        Me.lblOperationType.Text = "نوع العملية (بيع/شراء)"
        Me.lblOperationType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PanelBottom
        '
        Me.PanelBottom.BackColor = System.Drawing.Color.White
        Me.PanelBottom.Controls.Add(Me.btn_Print)
        Me.PanelBottom.Controls.Add(Me.btnOpenDetails)
        Me.PanelBottom.Controls.Add(Me.btnClear)
        Me.PanelBottom.Controls.Add(Me.btnSavePending)
        Me.PanelBottom.Controls.Add(Me.btnClose)
        Me.PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelBottom.Location = New System.Drawing.Point(0, 610)
        Me.PanelBottom.Name = "PanelBottom"
        Me.PanelBottom.Padding = New System.Windows.Forms.Padding(12, 10, 12, 10)
        Me.PanelBottom.Size = New System.Drawing.Size(980, 60)
        Me.PanelBottom.TabIndex = 2
        '
        'btn_Print
        '
        Me.btn_Print.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Print.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.btn_Print.Enabled = False
        Me.btn_Print.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.btn_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_Print.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btn_Print.Location = New System.Drawing.Point(530, 12)
        Me.btn_Print.Name = "btn_Print"
        Me.btn_Print.Size = New System.Drawing.Size(159, 36)
        Me.btn_Print.TabIndex = 4
        Me.btn_Print.Text = "طباعــة  🖨️"
        Me.btn_Print.UseVisualStyleBackColor = False
        '
        'btnOpenDetails
        '
        Me.btnOpenDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnOpenDetails.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.btnOpenDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenDetails.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnOpenDetails.Location = New System.Drawing.Point(12, 12)
        Me.btnOpenDetails.Name = "btnOpenDetails"
        Me.btnOpenDetails.Size = New System.Drawing.Size(48, 36)
        Me.btnOpenDetails.TabIndex = 3
        Me.btnOpenDetails.Text = "عرض التفاصيل"
        Me.btnOpenDetails.UseVisualStyleBackColor = False
        Me.btnOpenDetails.Visible = False
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnClear.Location = New System.Drawing.Point(386, 13)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(138, 36)
        Me.btnClear.TabIndex = 2
        Me.btnClear.Text = "مسح + جديد"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnSavePending
        '
        Me.btnSavePending.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSavePending.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.btnSavePending.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnSavePending.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSavePending.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSavePending.ForeColor = System.Drawing.Color.White
        Me.btnSavePending.Location = New System.Drawing.Point(694, 12)
        Me.btnSavePending.Name = "btnSavePending"
        Me.btnSavePending.Size = New System.Drawing.Size(170, 36)
        Me.btnSavePending.TabIndex = 1
        Me.btnSavePending.Text = "حفظ Pending"
        Me.btnSavePending.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.Location = New System.Drawing.Point(870, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(98, 36)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "إغلاق"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.White
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslStatus, Me.tsslExchangeId})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 670)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StatusStrip1.Size = New System.Drawing.Size(980, 22)
        Me.StatusStrip1.TabIndex = 3
        '
        'tsslStatus
        '
        Me.tsslStatus.Name = "tsslStatus"
        Me.tsslStatus.Size = New System.Drawing.Size(39, 17)
        Me.tsslStatus.Text = "جاهز..."
        '
        'tsslExchangeId
        '
        Me.tsslExchangeId.Name = "tsslExchangeId"
        Me.tsslExchangeId.Size = New System.Drawing.Size(0, 17)
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'FrmExchangeCreate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(980, 692)
        Me.Controls.Add(Me.PanelMain)
        Me.Controls.Add(Me.PanelBottom)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.PanelHeader)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmExchangeCreate"
        Me.RightToLeftLayout = True
        Me.Text = "Exchange - Create Pending"
        Me.PanelHeader.ResumeLayout(False)
        Me.PanelMain.ResumeLayout(False)
        Me.GB_Notes.ResumeLayout(False)
        Me.GB_Notes.PerformLayout()
        CType(Me.DocGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GB_Calc.ResumeLayout(False)
        Me.CardNet.ResumeLayout(False)
        Me.CardCommission.ResumeLayout(False)
        Me.CardTotal.ResumeLayout(False)
        Me.GB_Inputs.ResumeLayout(False)
        Me.GB_Inputs.PerformLayout()
        CType(Me.numRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numForeignAmount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GB_Operation.ResumeLayout(False)
        Me.GB_Operation.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.PanelBottom.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PanelHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents PanelMain As Panel
    Friend WithEvents GB_Operation As GroupBox
    Friend WithEvents cmbVault As ComboBox
    Friend WithEvents lblVault As Label
    Friend WithEvents cmbOperationType As ComboBox
    Friend WithEvents lblOperationType As Label
    Friend WithEvents btnRefreshRate As Button
    Friend WithEvents cmbCurrency As ComboBox
    Friend WithEvents lblCurrency As Label
    Friend WithEvents GB_Inputs As GroupBox
    Friend WithEvents txtCommissionPercent As TextBox
    Friend WithEvents lblCommissionPercent As Label
    Friend WithEvents numRate As NumericUpDown
    Friend WithEvents lblRate As Label
    Friend WithEvents numForeignAmount As NumericUpDown
    Friend WithEvents lblForeignAmount As Label
    Friend WithEvents GB_Calc As GroupBox
    Friend WithEvents CardNet As Panel
    Friend WithEvents lblNetLYD As Label
    Friend WithEvents lblNetCaption As Label
    Friend WithEvents CardCommission As Panel
    Friend WithEvents lblCommissionLYD As Label
    Friend WithEvents lblCommissionCaption As Label
    Friend WithEvents CardTotal As Panel
    Friend WithEvents lblTotalLYD As Label
    Friend WithEvents lblTotalCaption As Label
    Friend WithEvents GB_Notes As GroupBox
    Friend WithEvents txtNote As TextBox
    Friend WithEvents lblNote As Label
    Friend WithEvents txtReferenceNo As TextBox
    Friend WithEvents lblReferenceNo As Label
    Friend WithEvents PanelBottom As Panel
    Friend WithEvents btnOpenDetails As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents btnSavePending As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents tsslStatus As ToolStripStatusLabel
    Friend WithEvents tsslExchangeId As ToolStripStatusLabel
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents txt_CustomerIdentityNumber As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txt_CustomerName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btn_Print As Button
    Friend WithEvents ADD_Doc_btn As Button
    Friend WithEvents DocGridView As DataGridView
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Tr_Balance_Lb As Label
    Friend WithEvents Tittle_balance_Label As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Tr_Balance_Pending_Lb As Label
    Friend WithEvents Tittle_pendingbalance_Label As Label
    Friend WithEvents NoRateMsg_Label As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Tr_Total_Balance_Lb As Label
    Friend WithEvents Tittle_Total_balance_Label As Label
End Class
