<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmWithdrawalDeposit
    Inherits System.Windows.Forms.Form

    ' تنظيف الموارد
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

    ' حاويات الأسطر
    Friend WithEvents pnlTopBar As System.Windows.Forms.Panel
    Friend WithEvents pnlRow1_ID As System.Windows.Forms.Panel
    Friend WithEvents pnlRow2_Customer As System.Windows.Forms.Panel
    Friend WithEvents pnlRow3_Safe As System.Windows.Forms.Panel
    Friend WithEvents pnlRow4_Weights As System.Windows.Forms.Panel
    Friend WithEvents pnlRow5_NetWeight As System.Windows.Forms.Panel
    Friend WithEvents pnlRow6_TypeRadios As System.Windows.Forms.Panel
    Friend WithEvents pnlRow7_TypeContainers As System.Windows.Forms.Panel
    Friend WithEvents pnlRow8_Statement As System.Windows.Forms.Panel

    ' البار العلوي
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnMaximize As System.Windows.Forms.Button
    Friend WithEvents btnMinimize As System.Windows.Forms.Button
    Friend WithEvents lblTitle As System.Windows.Forms.Label

    ' السطر 1
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents btnPrev As System.Windows.Forms.Button
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker

    ' السطر 2
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents cmbCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchCustomer As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomerInfo As System.Windows.Forms.Label

    ' السطر 3
    Friend WithEvents lblSafe As System.Windows.Forms.Label
    Friend WithEvents cmbSafe As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchSafe As System.Windows.Forms.TextBox

    ' السطر 4
    Friend WithEvents lblGrossWeight As System.Windows.Forms.Label
    Friend WithEvents txtGrossWeight As System.Windows.Forms.TextBox
    Friend WithEvents lblCarat As System.Windows.Forms.Label
    Friend WithEvents txtCarat As System.Windows.Forms.TextBox

    ' السطر 5
    Friend WithEvents lblNetWeight As System.Windows.Forms.Label
    Friend WithEvents rb18K As System.Windows.Forms.RadioButton
    Friend WithEvents rb24K As System.Windows.Forms.RadioButton
    Friend WithEvents txtNetWeight As System.Windows.Forms.TextBox

    ' السطر 6
    Friend WithEvents rbCast As System.Windows.Forms.RadioButton
    Friend WithEvents rbManufactured As System.Windows.Forms.RadioButton

    ' السطر 7 (مسبوك ومصنعية)
    Friend WithEvents pnlCast As System.Windows.Forms.Panel
    Friend WithEvents pnlManufactured As System.Windows.Forms.Panel
    Friend WithEvents lblIngotsCount As System.Windows.Forms.Label
    Friend WithEvents txtIngotsCount As System.Windows.Forms.TextBox
    Friend WithEvents lblCastPrice As System.Windows.Forms.Label
    Friend WithEvents txtCastPrice As System.Windows.Forms.TextBox
    Friend WithEvents cmbCastCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents txtCastTotal As System.Windows.Forms.TextBox
    Friend WithEvents lblManfPrice As System.Windows.Forms.Label
    Friend WithEvents txtManfPrice As System.Windows.Forms.TextBox
    Friend WithEvents cmbManfCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents txtManfTotal As System.Windows.Forms.TextBox

    ' السطر 8
    Friend WithEvents txtStatement As System.Windows.Forms.TextBox
    Friend WithEvents lblStatement As System.Windows.Forms.Label

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlTopBar = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.btnMinimize = New System.Windows.Forms.Button()
        Me.btnMaximize = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlRow1_ID = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblID = New System.Windows.Forms.Label()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.pnlRow2_Customer = New System.Windows.Forms.Panel()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.cmbCustomer = New System.Windows.Forms.ComboBox()
        Me.txtSearchCustomer = New System.Windows.Forms.TextBox()
        Me.lblCustomerInfo = New System.Windows.Forms.Label()
        Me.pnlRow3_Safe = New System.Windows.Forms.Panel()
        Me.lblTreasuryInfo = New System.Windows.Forms.Label()
        Me.lblSafe = New System.Windows.Forms.Label()
        Me.cmbSafe = New System.Windows.Forms.ComboBox()
        Me.txtSearchSafe = New System.Windows.Forms.TextBox()
        Me.pnlRow4_Weights = New System.Windows.Forms.Panel()
        Me.lblCarat = New System.Windows.Forms.Label()
        Me.txtCarat = New System.Windows.Forms.TextBox()
        Me.lblGrossWeight = New System.Windows.Forms.Label()
        Me.txtGrossWeight = New System.Windows.Forms.TextBox()
        Me.pnlRow5_NetWeight = New System.Windows.Forms.Panel()
        Me.lblNetWeight = New System.Windows.Forms.Label()
        Me.txtNetWeight = New System.Windows.Forms.TextBox()
        Me.rb24K = New System.Windows.Forms.RadioButton()
        Me.rb18K = New System.Windows.Forms.RadioButton()
        Me.pnlRow6_TypeRadios = New System.Windows.Forms.Panel()
        Me.rbCast = New System.Windows.Forms.RadioButton()
        Me.rbManufactured = New System.Windows.Forms.RadioButton()
        Me.pnlRow7_TypeContainers = New System.Windows.Forms.Panel()
        Me.pnlCast = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblIngotsCount = New System.Windows.Forms.Label()
        Me.txtIngotsCount = New System.Windows.Forms.TextBox()
        Me.lblCastPrice = New System.Windows.Forms.Label()
        Me.txtCastPrice = New System.Windows.Forms.TextBox()
        Me.cmbCastCurrency = New System.Windows.Forms.ComboBox()
        Me.txtCastTotal = New System.Windows.Forms.TextBox()
        Me.pnlManufactured = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblManfPrice = New System.Windows.Forms.Label()
        Me.txtManfPrice = New System.Windows.Forms.TextBox()
        Me.cmbManfCurrency = New System.Windows.Forms.ComboBox()
        Me.txtManfTotal = New System.Windows.Forms.TextBox()
        Me.pnlRow8_Statement = New System.Windows.Forms.Panel()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.txtStatement = New System.Windows.Forms.TextBox()
        Me.lblStatement = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlTopBar.SuspendLayout()
        Me.pnlRow1_ID.SuspendLayout()
        Me.pnlRow2_Customer.SuspendLayout()
        Me.pnlRow3_Safe.SuspendLayout()
        Me.pnlRow4_Weights.SuspendLayout()
        Me.pnlRow5_NetWeight.SuspendLayout()
        Me.pnlRow6_TypeRadios.SuspendLayout()
        Me.pnlRow7_TypeContainers.SuspendLayout()
        Me.pnlCast.SuspendLayout()
        Me.pnlManufactured.SuspendLayout()
        Me.pnlRow8_Statement.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTopBar
        '
        Me.pnlTopBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.pnlTopBar.Controls.Add(Me.lblTitle)
        Me.pnlTopBar.Controls.Add(Me.btnMinimize)
        Me.pnlTopBar.Controls.Add(Me.btnMaximize)
        Me.pnlTopBar.Controls.Add(Me.btnClose)
        Me.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTopBar.Location = New System.Drawing.Point(0, 0)
        Me.pnlTopBar.Name = "pnlTopBar"
        Me.pnlTopBar.Size = New System.Drawing.Size(856, 45)
        Me.pnlTopBar.TabIndex = 8
        '
        'lblTitle
        '
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(135, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(721, 45)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = " شاشة السحب والإيداع (مشروع المعادن)"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnMinimize
        '
        Me.btnMinimize.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnMinimize.FlatAppearance.BorderSize = 0
        Me.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMinimize.ForeColor = System.Drawing.Color.White
        Me.btnMinimize.Location = New System.Drawing.Point(90, 0)
        Me.btnMinimize.Name = "btnMinimize"
        Me.btnMinimize.Size = New System.Drawing.Size(45, 45)
        Me.btnMinimize.TabIndex = 1
        Me.btnMinimize.Text = "—"
        '
        'btnMaximize
        '
        Me.btnMaximize.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnMaximize.FlatAppearance.BorderSize = 0
        Me.btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMaximize.ForeColor = System.Drawing.Color.White
        Me.btnMaximize.Location = New System.Drawing.Point(45, 0)
        Me.btnMaximize.Name = "btnMaximize"
        Me.btnMaximize.Size = New System.Drawing.Size(45, 45)
        Me.btnMaximize.TabIndex = 2
        Me.btnMaximize.Text = "☐"
        '
        'btnClose
        '
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(0, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(45, 45)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "✕"
        '
        'pnlRow1_ID
        '
        Me.pnlRow1_ID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRow1_ID.Controls.Add(Me.btnCancel)
        Me.pnlRow1_ID.Controls.Add(Me.Label3)
        Me.pnlRow1_ID.Controls.Add(Me.lblID)
        Me.pnlRow1_ID.Controls.Add(Me.btnNext)
        Me.pnlRow1_ID.Controls.Add(Me.txtID)
        Me.pnlRow1_ID.Controls.Add(Me.btnPrev)
        Me.pnlRow1_ID.Controls.Add(Me.dtpDate)
        Me.pnlRow1_ID.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow1_ID.Location = New System.Drawing.Point(0, 45)
        Me.pnlRow1_ID.Name = "pnlRow1_ID"
        Me.pnlRow1_ID.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlRow1_ID.Size = New System.Drawing.Size(856, 50)
        Me.pnlRow1_ID.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(217, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 19)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "التاريخ:"
        '
        'lblID
        '
        Me.lblID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblID.AutoSize = True
        Me.lblID.Location = New System.Drawing.Point(748, 10)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(72, 19)
        Me.lblID.TabIndex = 0
        Me.lblID.Text = "رقم الحركة:"
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Location = New System.Drawing.Point(442, 10)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(40, 26)
        Me.btnNext.TabIndex = 1
        Me.btnNext.Text = ">"
        '
        'txtID
        '
        Me.txtID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtID.Location = New System.Drawing.Point(488, 10)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(200, 26)
        Me.txtID.TabIndex = 2
        Me.txtID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnPrev
        '
        Me.btnPrev.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrev.Location = New System.Drawing.Point(698, 10)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(40, 26)
        Me.btnPrev.TabIndex = 3
        Me.btnPrev.Text = "<"
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "yyyy/MM/dd"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(15, 10)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(196, 26)
        Me.dtpDate.TabIndex = 4
        '
        'pnlRow2_Customer
        '
        Me.pnlRow2_Customer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRow2_Customer.Controls.Add(Me.lblCustomer)
        Me.pnlRow2_Customer.Controls.Add(Me.cmbCustomer)
        Me.pnlRow2_Customer.Controls.Add(Me.txtSearchCustomer)
        Me.pnlRow2_Customer.Controls.Add(Me.lblCustomerInfo)
        Me.pnlRow2_Customer.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow2_Customer.Location = New System.Drawing.Point(0, 95)
        Me.pnlRow2_Customer.Name = "pnlRow2_Customer"
        Me.pnlRow2_Customer.Size = New System.Drawing.Size(856, 70)
        Me.pnlRow2_Customer.TabIndex = 6
        '
        'lblCustomer
        '
        Me.lblCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Location = New System.Drawing.Point(748, 24)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(62, 19)
        Me.lblCustomer.TabIndex = 0
        Me.lblCustomer.Text = "الـزبــــــون:"
        '
        'cmbCustomer
        '
        Me.cmbCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCustomer.Location = New System.Drawing.Point(488, 21)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(250, 27)
        Me.cmbCustomer.TabIndex = 1
        '
        'txtSearchCustomer
        '
        Me.txtSearchCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearchCustomer.Location = New System.Drawing.Point(328, 21)
        Me.txtSearchCustomer.Name = "txtSearchCustomer"
        Me.txtSearchCustomer.Size = New System.Drawing.Size(150, 26)
        Me.txtSearchCustomer.TabIndex = 2
        '
        'lblCustomerInfo
        '
        Me.lblCustomerInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCustomerInfo.ForeColor = System.Drawing.Color.DimGray
        Me.lblCustomerInfo.Location = New System.Drawing.Point(12, 4)
        Me.lblCustomerInfo.Name = "lblCustomerInfo"
        Me.lblCustomerInfo.Size = New System.Drawing.Size(307, 60)
        Me.lblCustomerInfo.TabIndex = 3
        Me.lblCustomerInfo.Text = "معلومات الزبون المختصرة ستظهر هنا..."
        '
        'pnlRow3_Safe
        '
        Me.pnlRow3_Safe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRow3_Safe.Controls.Add(Me.lblTreasuryInfo)
        Me.pnlRow3_Safe.Controls.Add(Me.lblSafe)
        Me.pnlRow3_Safe.Controls.Add(Me.cmbSafe)
        Me.pnlRow3_Safe.Controls.Add(Me.txtSearchSafe)
        Me.pnlRow3_Safe.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow3_Safe.Location = New System.Drawing.Point(0, 165)
        Me.pnlRow3_Safe.Name = "pnlRow3_Safe"
        Me.pnlRow3_Safe.Size = New System.Drawing.Size(856, 82)
        Me.pnlRow3_Safe.TabIndex = 5
        '
        'lblTreasuryInfo
        '
        Me.lblTreasuryInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTreasuryInfo.ForeColor = System.Drawing.Color.DimGray
        Me.lblTreasuryInfo.Location = New System.Drawing.Point(12, 13)
        Me.lblTreasuryInfo.Name = "lblTreasuryInfo"
        Me.lblTreasuryInfo.Size = New System.Drawing.Size(307, 60)
        Me.lblTreasuryInfo.TabIndex = 4
        Me.lblTreasuryInfo.Text = "معلومات الخزينة  المختصرة ستظهر هنا..."
        '
        'lblSafe
        '
        Me.lblSafe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSafe.AutoSize = True
        Me.lblSafe.Location = New System.Drawing.Point(748, 33)
        Me.lblSafe.Name = "lblSafe"
        Me.lblSafe.Size = New System.Drawing.Size(61, 19)
        Me.lblSafe.TabIndex = 0
        Me.lblSafe.Text = "الخزينــــة:"
        '
        'cmbSafe
        '
        Me.cmbSafe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbSafe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSafe.Location = New System.Drawing.Point(488, 30)
        Me.cmbSafe.Name = "cmbSafe"
        Me.cmbSafe.Size = New System.Drawing.Size(250, 27)
        Me.cmbSafe.TabIndex = 1
        '
        'txtSearchSafe
        '
        Me.txtSearchSafe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearchSafe.Location = New System.Drawing.Point(328, 30)
        Me.txtSearchSafe.Name = "txtSearchSafe"
        Me.txtSearchSafe.Size = New System.Drawing.Size(150, 26)
        Me.txtSearchSafe.TabIndex = 2
        '
        'pnlRow4_Weights
        '
        Me.pnlRow4_Weights.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRow4_Weights.Controls.Add(Me.lblCarat)
        Me.pnlRow4_Weights.Controls.Add(Me.txtCarat)
        Me.pnlRow4_Weights.Controls.Add(Me.lblGrossWeight)
        Me.pnlRow4_Weights.Controls.Add(Me.txtGrossWeight)
        Me.pnlRow4_Weights.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow4_Weights.Location = New System.Drawing.Point(0, 247)
        Me.pnlRow4_Weights.Name = "pnlRow4_Weights"
        Me.pnlRow4_Weights.Size = New System.Drawing.Size(856, 50)
        Me.pnlRow4_Weights.TabIndex = 4
        '
        'lblCarat
        '
        Me.lblCarat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCarat.AutoSize = True
        Me.lblCarat.Location = New System.Drawing.Point(748, 13)
        Me.lblCarat.Name = "lblCarat"
        Me.lblCarat.Size = New System.Drawing.Size(48, 19)
        Me.lblCarat.TabIndex = 0
        Me.lblCarat.Text = "العيــار:"
        '
        'txtCarat
        '
        Me.txtCarat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCarat.Location = New System.Drawing.Point(548, 10)
        Me.txtCarat.Name = "txtCarat"
        Me.txtCarat.Size = New System.Drawing.Size(190, 26)
        Me.txtCarat.TabIndex = 1
        '
        'lblGrossWeight
        '
        Me.lblGrossWeight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblGrossWeight.AutoSize = True
        Me.lblGrossWeight.Location = New System.Drawing.Point(378, 13)
        Me.lblGrossWeight.Name = "lblGrossWeight"
        Me.lblGrossWeight.Size = New System.Drawing.Size(80, 19)
        Me.lblGrossWeight.TabIndex = 2
        Me.lblGrossWeight.Text = "الوزن القائم:"
        '
        'txtGrossWeight
        '
        Me.txtGrossWeight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGrossWeight.Location = New System.Drawing.Point(157, 10)
        Me.txtGrossWeight.Name = "txtGrossWeight"
        Me.txtGrossWeight.Size = New System.Drawing.Size(211, 26)
        Me.txtGrossWeight.TabIndex = 3
        '
        'pnlRow5_NetWeight
        '
        Me.pnlRow5_NetWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRow5_NetWeight.Controls.Add(Me.lblNetWeight)
        Me.pnlRow5_NetWeight.Controls.Add(Me.txtNetWeight)
        Me.pnlRow5_NetWeight.Controls.Add(Me.rb24K)
        Me.pnlRow5_NetWeight.Controls.Add(Me.rb18K)
        Me.pnlRow5_NetWeight.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow5_NetWeight.Location = New System.Drawing.Point(0, 297)
        Me.pnlRow5_NetWeight.Name = "pnlRow5_NetWeight"
        Me.pnlRow5_NetWeight.Size = New System.Drawing.Size(856, 50)
        Me.pnlRow5_NetWeight.TabIndex = 3
        '
        'lblNetWeight
        '
        Me.lblNetWeight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNetWeight.AutoSize = True
        Me.lblNetWeight.Location = New System.Drawing.Point(378, 13)
        Me.lblNetWeight.Name = "lblNetWeight"
        Me.lblNetWeight.Size = New System.Drawing.Size(93, 19)
        Me.lblNetWeight.TabIndex = 0
        Me.lblNetWeight.Text = "الوزن الصافي:"
        '
        'txtNetWeight
        '
        Me.txtNetWeight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNetWeight.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtNetWeight.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetWeight.Location = New System.Drawing.Point(157, 10)
        Me.txtNetWeight.Name = "txtNetWeight"
        Me.txtNetWeight.ReadOnly = True
        Me.txtNetWeight.Size = New System.Drawing.Size(211, 29)
        Me.txtNetWeight.TabIndex = 1
        Me.txtNetWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'rb24K
        '
        Me.rb24K.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rb24K.AutoSize = True
        Me.rb24K.Location = New System.Drawing.Point(683, 11)
        Me.rb24K.Name = "rb24K"
        Me.rb24K.Size = New System.Drawing.Size(51, 23)
        Me.rb24K.TabIndex = 2
        Me.rb24K.Text = "24K"
        '
        'rb18K
        '
        Me.rb18K.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rb18K.AutoSize = True
        Me.rb18K.Location = New System.Drawing.Point(603, 11)
        Me.rb18K.Name = "rb18K"
        Me.rb18K.Size = New System.Drawing.Size(51, 23)
        Me.rb18K.TabIndex = 3
        Me.rb18K.Text = "18K"
        '
        'pnlRow6_TypeRadios
        '
        Me.pnlRow6_TypeRadios.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRow6_TypeRadios.Controls.Add(Me.rbCast)
        Me.pnlRow6_TypeRadios.Controls.Add(Me.rbManufactured)
        Me.pnlRow6_TypeRadios.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow6_TypeRadios.Location = New System.Drawing.Point(0, 347)
        Me.pnlRow6_TypeRadios.Name = "pnlRow6_TypeRadios"
        Me.pnlRow6_TypeRadios.Size = New System.Drawing.Size(856, 50)
        Me.pnlRow6_TypeRadios.TabIndex = 2
        '
        'rbCast
        '
        Me.rbCast.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbCast.AutoSize = True
        Me.rbCast.Checked = True
        Me.rbCast.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.rbCast.Location = New System.Drawing.Point(442, 12)
        Me.rbCast.Name = "rbCast"
        Me.rbCast.Size = New System.Drawing.Size(91, 23)
        Me.rbCast.TabIndex = 0
        Me.rbCast.TabStop = True
        Me.rbCast.Text = "1. مسبــوك"
        '
        'rbManufactured
        '
        Me.rbManufactured.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rbManufactured.AutoSize = True
        Me.rbManufactured.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.rbManufactured.Location = New System.Drawing.Point(292, 12)
        Me.rbManufactured.Name = "rbManufactured"
        Me.rbManufactured.Size = New System.Drawing.Size(94, 23)
        Me.rbManufactured.TabIndex = 1
        Me.rbManufactured.Text = "2. مصنعيــة"
        '
        'pnlRow7_TypeContainers
        '
        Me.pnlRow7_TypeContainers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRow7_TypeContainers.Controls.Add(Me.pnlCast)
        Me.pnlRow7_TypeContainers.Controls.Add(Me.pnlManufactured)
        Me.pnlRow7_TypeContainers.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow7_TypeContainers.Location = New System.Drawing.Point(0, 397)
        Me.pnlRow7_TypeContainers.Name = "pnlRow7_TypeContainers"
        Me.pnlRow7_TypeContainers.Padding = New System.Windows.Forms.Padding(15, 0, 15, 0)
        Me.pnlRow7_TypeContainers.Size = New System.Drawing.Size(856, 70)
        Me.pnlRow7_TypeContainers.TabIndex = 1
        '
        'pnlCast
        '
        Me.pnlCast.BackColor = System.Drawing.Color.White
        Me.pnlCast.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCast.Controls.Add(Me.Label1)
        Me.pnlCast.Controls.Add(Me.lblIngotsCount)
        Me.pnlCast.Controls.Add(Me.txtIngotsCount)
        Me.pnlCast.Controls.Add(Me.lblCastPrice)
        Me.pnlCast.Controls.Add(Me.txtCastPrice)
        Me.pnlCast.Controls.Add(Me.cmbCastCurrency)
        Me.pnlCast.Controls.Add(Me.txtCastTotal)
        Me.pnlCast.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCast.Location = New System.Drawing.Point(15, 0)
        Me.pnlCast.Name = "pnlCast"
        Me.pnlCast.Size = New System.Drawing.Size(824, 68)
        Me.pnlCast.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(199, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 19)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "الإجمالي:"
        '
        'lblIngotsCount
        '
        Me.lblIngotsCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblIngotsCount.AutoSize = True
        Me.lblIngotsCount.Location = New System.Drawing.Point(728, 20)
        Me.lblIngotsCount.Name = "lblIngotsCount"
        Me.lblIngotsCount.Size = New System.Drawing.Size(85, 19)
        Me.lblIngotsCount.TabIndex = 0
        Me.lblIngotsCount.Text = "عدد السبائك:"
        '
        'txtIngotsCount
        '
        Me.txtIngotsCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtIngotsCount.Location = New System.Drawing.Point(618, 18)
        Me.txtIngotsCount.Name = "txtIngotsCount"
        Me.txtIngotsCount.Size = New System.Drawing.Size(100, 26)
        Me.txtIngotsCount.TabIndex = 1
        '
        'lblCastPrice
        '
        Me.lblCastPrice.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCastPrice.AutoSize = True
        Me.lblCastPrice.Location = New System.Drawing.Point(548, 20)
        Me.lblCastPrice.Name = "lblCastPrice"
        Me.lblCastPrice.Size = New System.Drawing.Size(47, 19)
        Me.lblCastPrice.TabIndex = 2
        Me.lblCastPrice.Text = "السعر:"
        '
        'txtCastPrice
        '
        Me.txtCastPrice.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCastPrice.Location = New System.Drawing.Point(418, 18)
        Me.txtCastPrice.Name = "txtCastPrice"
        Me.txtCastPrice.Size = New System.Drawing.Size(120, 26)
        Me.txtCastPrice.TabIndex = 3
        '
        'cmbCastCurrency
        '
        Me.cmbCastCurrency.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbCastCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCastCurrency.Location = New System.Drawing.Point(276, 18)
        Me.cmbCastCurrency.Name = "cmbCastCurrency"
        Me.cmbCastCurrency.Size = New System.Drawing.Size(132, 27)
        Me.cmbCastCurrency.TabIndex = 4
        '
        'txtCastTotal
        '
        Me.txtCastTotal.Enabled = False
        Me.txtCastTotal.Location = New System.Drawing.Point(15, 18)
        Me.txtCastTotal.Name = "txtCastTotal"
        Me.txtCastTotal.Size = New System.Drawing.Size(180, 26)
        Me.txtCastTotal.TabIndex = 5
        '
        'pnlManufactured
        '
        Me.pnlManufactured.BackColor = System.Drawing.Color.White
        Me.pnlManufactured.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlManufactured.Controls.Add(Me.Label2)
        Me.pnlManufactured.Controls.Add(Me.lblManfPrice)
        Me.pnlManufactured.Controls.Add(Me.txtManfPrice)
        Me.pnlManufactured.Controls.Add(Me.cmbManfCurrency)
        Me.pnlManufactured.Controls.Add(Me.txtManfTotal)
        Me.pnlManufactured.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlManufactured.Location = New System.Drawing.Point(15, 0)
        Me.pnlManufactured.Name = "pnlManufactured"
        Me.pnlManufactured.Size = New System.Drawing.Size(824, 68)
        Me.pnlManufactured.TabIndex = 1
        Me.pnlManufactured.Visible = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(201, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 19)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "الإجمالي:"
        '
        'lblManfPrice
        '
        Me.lblManfPrice.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblManfPrice.AutoSize = True
        Me.lblManfPrice.Location = New System.Drawing.Point(728, 20)
        Me.lblManfPrice.Name = "lblManfPrice"
        Me.lblManfPrice.Size = New System.Drawing.Size(47, 19)
        Me.lblManfPrice.TabIndex = 0
        Me.lblManfPrice.Text = "السعر:"
        '
        'txtManfPrice
        '
        Me.txtManfPrice.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtManfPrice.Location = New System.Drawing.Point(578, 18)
        Me.txtManfPrice.Name = "txtManfPrice"
        Me.txtManfPrice.Size = New System.Drawing.Size(140, 26)
        Me.txtManfPrice.TabIndex = 1
        '
        'cmbManfCurrency
        '
        Me.cmbManfCurrency.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbManfCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbManfCurrency.Location = New System.Drawing.Point(448, 18)
        Me.cmbManfCurrency.Name = "cmbManfCurrency"
        Me.cmbManfCurrency.Size = New System.Drawing.Size(120, 27)
        Me.cmbManfCurrency.TabIndex = 2
        '
        'txtManfTotal
        '
        Me.txtManfTotal.Enabled = False
        Me.txtManfTotal.Location = New System.Drawing.Point(15, 18)
        Me.txtManfTotal.Name = "txtManfTotal"
        Me.txtManfTotal.Size = New System.Drawing.Size(180, 26)
        Me.txtManfTotal.TabIndex = 3
        '
        'pnlRow8_Statement
        '
        Me.pnlRow8_Statement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRow8_Statement.Controls.Add(Me.btnPrint)
        Me.pnlRow8_Statement.Controls.Add(Me.btnSave)
        Me.pnlRow8_Statement.Controls.Add(Me.btnNew)
        Me.pnlRow8_Statement.Controls.Add(Me.txtStatement)
        Me.pnlRow8_Statement.Controls.Add(Me.lblStatement)
        Me.pnlRow8_Statement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRow8_Statement.Location = New System.Drawing.Point(0, 467)
        Me.pnlRow8_Statement.Name = "pnlRow8_Statement"
        Me.pnlRow8_Statement.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlRow8_Statement.Size = New System.Drawing.Size(856, 233)
        Me.pnlRow8_Statement.TabIndex = 0
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.SeaShell
        Me.btnPrint.FlatAppearance.BorderSize = 0
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrint.Location = New System.Drawing.Point(15, 168)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(162, 45)
        Me.btnPrint.TabIndex = 8
        Me.btnPrint.Tag = "Delete"
        Me.btnPrint.Text = "طباعة"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.Location = New System.Drawing.Point(412, 168)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(222, 45)
        Me.btnSave.TabIndex = 9
        Me.btnSave.Tag = "Save"
        Me.btnSave.Text = "حفظ"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.Honeydew
        Me.btnNew.FlatAppearance.BorderSize = 0
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnNew.Location = New System.Drawing.Point(658, 168)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(183, 45)
        Me.btnNew.TabIndex = 10
        Me.btnNew.Tag = "General"
        Me.btnNew.Text = "جديد"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'txtStatement
        '
        Me.txtStatement.Location = New System.Drawing.Point(15, 39)
        Me.txtStatement.Multiline = True
        Me.txtStatement.Name = "txtStatement"
        Me.txtStatement.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtStatement.Size = New System.Drawing.Size(826, 115)
        Me.txtStatement.TabIndex = 0
        '
        'lblStatement
        '
        Me.lblStatement.AutoSize = True
        Me.lblStatement.Location = New System.Drawing.Point(777, 12)
        Me.lblStatement.Name = "lblStatement"
        Me.lblStatement.Padding = New System.Windows.Forms.Padding(0, 0, 0, 5)
        Me.lblStatement.Size = New System.Drawing.Size(66, 24)
        Me.lblStatement.TabIndex = 1
        Me.lblStatement.Text = "البيـــــــــــان:"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.Red
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.Location = New System.Drawing.Point(307, 8)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(129, 30)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "الغاء"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'frmWithdrawalDeposit
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(856, 700)
        Me.Controls.Add(Me.pnlRow8_Statement)
        Me.Controls.Add(Me.pnlRow7_TypeContainers)
        Me.Controls.Add(Me.pnlRow6_TypeRadios)
        Me.Controls.Add(Me.pnlRow5_NetWeight)
        Me.Controls.Add(Me.pnlRow4_Weights)
        Me.Controls.Add(Me.pnlRow3_Safe)
        Me.Controls.Add(Me.pnlRow2_Customer)
        Me.Controls.Add(Me.pnlRow1_ID)
        Me.Controls.Add(Me.pnlTopBar)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmWithdrawalDeposit"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "السحب والإيداع"
        Me.pnlTopBar.ResumeLayout(False)
        Me.pnlRow1_ID.ResumeLayout(False)
        Me.pnlRow1_ID.PerformLayout()
        Me.pnlRow2_Customer.ResumeLayout(False)
        Me.pnlRow2_Customer.PerformLayout()
        Me.pnlRow3_Safe.ResumeLayout(False)
        Me.pnlRow3_Safe.PerformLayout()
        Me.pnlRow4_Weights.ResumeLayout(False)
        Me.pnlRow4_Weights.PerformLayout()
        Me.pnlRow5_NetWeight.ResumeLayout(False)
        Me.pnlRow5_NetWeight.PerformLayout()
        Me.pnlRow6_TypeRadios.ResumeLayout(False)
        Me.pnlRow6_TypeRadios.PerformLayout()
        Me.pnlRow7_TypeContainers.ResumeLayout(False)
        Me.pnlCast.ResumeLayout(False)
        Me.pnlCast.PerformLayout()
        Me.pnlManufactured.ResumeLayout(False)
        Me.pnlManufactured.PerformLayout()
        Me.pnlRow8_Statement.ResumeLayout(False)
        Me.pnlRow8_Statement.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnPrint As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents lblTreasuryInfo As Label
    Friend WithEvents btnCancel As Button
End Class