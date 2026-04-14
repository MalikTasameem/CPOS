<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBuySell
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
    Friend WithEvents pnlRow2_POS As System.Windows.Forms.Panel
    Friend WithEvents pnlRow3_Customer As System.Windows.Forms.Panel
    Friend WithEvents pnlRow4_Currency As System.Windows.Forms.Panel
    Friend WithEvents pnlRow5_Unit As System.Windows.Forms.Panel
    Friend WithEvents pnlRow6_Weights As System.Windows.Forms.Panel
    Friend WithEvents pnlRow7_NetWeight As System.Windows.Forms.Panel
    Friend WithEvents pnlRow9_Statement As System.Windows.Forms.Panel

    ' البار العلوي
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnMaximize As System.Windows.Forms.Button
    Friend WithEvents btnMinimize As System.Windows.Forms.Button
    Friend WithEvents lblTitle As System.Windows.Forms.Label

    ' السطر 1: الحركة والتاريخ
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents btnPrev As System.Windows.Forms.Button

    ' السطر 2: نقطة البيع
    Friend WithEvents lblPOS As System.Windows.Forms.Label
    Friend WithEvents cmbPOS As System.Windows.Forms.ComboBox

    ' السطر 3: الزبون
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents cmbCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchCustomer As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomerInfo As System.Windows.Forms.Label

    ' السطر 4: العملة المستلمة
    Friend WithEvents lblCurrency As System.Windows.Forms.Label
    Friend WithEvents cmbCurrency As System.Windows.Forms.ComboBox

    ' السطر 5: الوحدة (جرام / أونصة)
    Friend WithEvents chkGram As System.Windows.Forms.CheckBox
    Friend WithEvents chkOunce As System.Windows.Forms.CheckBox

    ' السطر 6: الأوزان
    Friend WithEvents lblGrossWeight As System.Windows.Forms.Label
    Friend WithEvents txtGrossWeight As System.Windows.Forms.TextBox
    Friend WithEvents lblCarat As System.Windows.Forms.Label
    Friend WithEvents txtCarat As System.Windows.Forms.TextBox

    ' السطر 7: الوزن الصافي والعيارات
    Friend WithEvents lblNetWeight As System.Windows.Forms.Label
    Friend WithEvents rb18K As System.Windows.Forms.RadioButton
    Friend WithEvents rb24K As System.Windows.Forms.RadioButton
    Friend WithEvents txtNetWeight As System.Windows.Forms.TextBox

    ' السطر 8: السعر
    Friend WithEvents lblPrice As System.Windows.Forms.Label
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox

    ' السطر 9: البيان
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
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.lblID = New System.Windows.Forms.Label()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.pnlRow2_POS = New System.Windows.Forms.Panel()
        Me.lblPOS = New System.Windows.Forms.Label()
        Me.cmbPOS = New System.Windows.Forms.ComboBox()
        Me.pnlRow3_Customer = New System.Windows.Forms.Panel()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.cmbCustomer = New System.Windows.Forms.ComboBox()
        Me.txtSearchCustomer = New System.Windows.Forms.TextBox()
        Me.lblCustomerInfo = New System.Windows.Forms.Label()
        Me.pnlRow4_Currency = New System.Windows.Forms.Panel()
        Me.lblPrice = New System.Windows.Forms.Label()
        Me.lblCurrency = New System.Windows.Forms.Label()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.cmbCurrency = New System.Windows.Forms.ComboBox()
        Me.pnlRow5_Unit = New System.Windows.Forms.Panel()
        Me.chkGram = New System.Windows.Forms.CheckBox()
        Me.chkOunce = New System.Windows.Forms.CheckBox()
        Me.pnlRow6_Weights = New System.Windows.Forms.Panel()
        Me.lblCarat = New System.Windows.Forms.Label()
        Me.txtCarat = New System.Windows.Forms.TextBox()
        Me.lblGrossWeight = New System.Windows.Forms.Label()
        Me.txtGrossWeight = New System.Windows.Forms.TextBox()
        Me.pnlRow7_NetWeight = New System.Windows.Forms.Panel()
        Me.lblNetWeight = New System.Windows.Forms.Label()
        Me.txtNetWeight = New System.Windows.Forms.TextBox()
        Me.rb24K = New System.Windows.Forms.RadioButton()
        Me.rb18K = New System.Windows.Forms.RadioButton()
        Me.pnlRow9_Statement = New System.Windows.Forms.Panel()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.txtStatement = New System.Windows.Forms.TextBox()
        Me.lblStatement = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.pnlTopBar.SuspendLayout()
        Me.pnlRow1_ID.SuspendLayout()
        Me.pnlRow2_POS.SuspendLayout()
        Me.pnlRow3_Customer.SuspendLayout()
        Me.pnlRow4_Currency.SuspendLayout()
        Me.pnlRow5_Unit.SuspendLayout()
        Me.pnlRow6_Weights.SuspendLayout()
        Me.pnlRow7_NetWeight.SuspendLayout()
        Me.pnlRow9_Statement.SuspendLayout()
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
        Me.pnlTopBar.TabIndex = 0
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
        Me.lblTitle.Text = " شاشة البيع والشراء (مشروع المعادن)"
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
        Me.pnlRow1_ID.Controls.Add(Me.DateTimePicker1)
        Me.pnlRow1_ID.Controls.Add(Me.lblID)
        Me.pnlRow1_ID.Controls.Add(Me.btnNext)
        Me.pnlRow1_ID.Controls.Add(Me.txtID)
        Me.pnlRow1_ID.Controls.Add(Me.btnPrev)
        Me.pnlRow1_ID.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow1_ID.Location = New System.Drawing.Point(0, 45)
        Me.pnlRow1_ID.Name = "pnlRow1_ID"
        Me.pnlRow1_ID.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlRow1_ID.Size = New System.Drawing.Size(856, 50)
        Me.pnlRow1_ID.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(215, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 19)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "التاريخ:"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "yyyy/MM/dd"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(15, 11)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(196, 26)
        Me.DateTimePicker1.TabIndex = 5
        '
        'lblID
        '
        Me.lblID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblID.AutoSize = True
        Me.lblID.Location = New System.Drawing.Point(748, 15)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(72, 19)
        Me.lblID.TabIndex = 0
        Me.lblID.Text = "رقم الحركة:"
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Location = New System.Drawing.Point(442, 11)
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
        Me.btnPrev.Location = New System.Drawing.Point(694, 9)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(40, 26)
        Me.btnPrev.TabIndex = 3
        Me.btnPrev.Text = "<"
        '
        'pnlRow2_POS
        '
        Me.pnlRow2_POS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRow2_POS.Controls.Add(Me.lblPOS)
        Me.pnlRow2_POS.Controls.Add(Me.cmbPOS)
        Me.pnlRow2_POS.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow2_POS.Location = New System.Drawing.Point(0, 95)
        Me.pnlRow2_POS.Name = "pnlRow2_POS"
        Me.pnlRow2_POS.Size = New System.Drawing.Size(856, 50)
        Me.pnlRow2_POS.TabIndex = 2
        '
        'lblPOS
        '
        Me.lblPOS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPOS.AutoSize = True
        Me.lblPOS.Location = New System.Drawing.Point(748, 13)
        Me.lblPOS.Name = "lblPOS"
        Me.lblPOS.Size = New System.Drawing.Size(74, 19)
        Me.lblPOS.TabIndex = 0
        Me.lblPOS.Text = "نقطة البيع:"
        '
        'cmbPOS
        '
        Me.cmbPOS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbPOS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPOS.Location = New System.Drawing.Point(488, 10)
        Me.cmbPOS.Name = "cmbPOS"
        Me.cmbPOS.Size = New System.Drawing.Size(250, 27)
        Me.cmbPOS.TabIndex = 1
        '
        'pnlRow3_Customer
        '
        Me.pnlRow3_Customer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRow3_Customer.Controls.Add(Me.lblCustomer)
        Me.pnlRow3_Customer.Controls.Add(Me.cmbCustomer)
        Me.pnlRow3_Customer.Controls.Add(Me.txtSearchCustomer)
        Me.pnlRow3_Customer.Controls.Add(Me.lblCustomerInfo)
        Me.pnlRow3_Customer.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow3_Customer.Location = New System.Drawing.Point(0, 145)
        Me.pnlRow3_Customer.Name = "pnlRow3_Customer"
        Me.pnlRow3_Customer.Size = New System.Drawing.Size(856, 72)
        Me.pnlRow3_Customer.TabIndex = 3
        '
        'lblCustomer
        '
        Me.lblCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Location = New System.Drawing.Point(748, 25)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(62, 19)
        Me.lblCustomer.TabIndex = 0
        Me.lblCustomer.Text = "الـزبــــــون:"
        '
        'cmbCustomer
        '
        Me.cmbCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCustomer.Location = New System.Drawing.Point(488, 22)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(250, 27)
        Me.cmbCustomer.TabIndex = 1
        '
        'txtSearchCustomer
        '
        Me.txtSearchCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearchCustomer.Location = New System.Drawing.Point(328, 22)
        Me.txtSearchCustomer.Name = "txtSearchCustomer"
        Me.txtSearchCustomer.Size = New System.Drawing.Size(150, 26)
        Me.txtSearchCustomer.TabIndex = 2
        '
        'lblCustomerInfo
        '
        Me.lblCustomerInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCustomerInfo.ForeColor = System.Drawing.Color.DimGray
        Me.lblCustomerInfo.Location = New System.Drawing.Point(12, 6)
        Me.lblCustomerInfo.Name = "lblCustomerInfo"
        Me.lblCustomerInfo.Size = New System.Drawing.Size(298, 59)
        Me.lblCustomerInfo.TabIndex = 3
        Me.lblCustomerInfo.Text = "معلومات الزبون المختصرة ستظهر هنا..."
        '
        'pnlRow4_Currency
        '
        Me.pnlRow4_Currency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRow4_Currency.Controls.Add(Me.lblPrice)
        Me.pnlRow4_Currency.Controls.Add(Me.lblCurrency)
        Me.pnlRow4_Currency.Controls.Add(Me.txtPrice)
        Me.pnlRow4_Currency.Controls.Add(Me.cmbCurrency)
        Me.pnlRow4_Currency.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow4_Currency.Location = New System.Drawing.Point(0, 217)
        Me.pnlRow4_Currency.Name = "pnlRow4_Currency"
        Me.pnlRow4_Currency.Size = New System.Drawing.Size(856, 50)
        Me.pnlRow4_Currency.TabIndex = 4
        '
        'lblPrice
        '
        Me.lblPrice.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPrice.AutoSize = True
        Me.lblPrice.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.lblPrice.Location = New System.Drawing.Point(379, 13)
        Me.lblPrice.Name = "lblPrice"
        Me.lblPrice.Size = New System.Drawing.Size(48, 19)
        Me.lblPrice.TabIndex = 0
        Me.lblPrice.Text = "السعر:"
        '
        'lblCurrency
        '
        Me.lblCurrency.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCurrency.AutoSize = True
        Me.lblCurrency.Location = New System.Drawing.Point(748, 13)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(107, 19)
        Me.lblCurrency.TabIndex = 0
        Me.lblCurrency.Text = "العملة المستلمة:"
        '
        'txtPrice
        '
        Me.txtPrice.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPrice.Location = New System.Drawing.Point(159, 10)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(210, 26)
        Me.txtPrice.TabIndex = 1
        '
        'cmbCurrency
        '
        Me.cmbCurrency.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCurrency.Location = New System.Drawing.Point(488, 10)
        Me.cmbCurrency.Name = "cmbCurrency"
        Me.cmbCurrency.Size = New System.Drawing.Size(250, 27)
        Me.cmbCurrency.TabIndex = 1
        '
        'pnlRow5_Unit
        '
        Me.pnlRow5_Unit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRow5_Unit.Controls.Add(Me.chkGram)
        Me.pnlRow5_Unit.Controls.Add(Me.chkOunce)
        Me.pnlRow5_Unit.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow5_Unit.Location = New System.Drawing.Point(0, 267)
        Me.pnlRow5_Unit.Name = "pnlRow5_Unit"
        Me.pnlRow5_Unit.Size = New System.Drawing.Size(856, 50)
        Me.pnlRow5_Unit.TabIndex = 5
        '
        'chkGram
        '
        Me.chkGram.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkGram.AutoSize = True
        Me.chkGram.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.chkGram.Location = New System.Drawing.Point(471, 13)
        Me.chkGram.Name = "chkGram"
        Me.chkGram.Size = New System.Drawing.Size(57, 23)
        Me.chkGram.TabIndex = 0
        Me.chkGram.Text = "جــرام"
        Me.chkGram.UseVisualStyleBackColor = True
        '
        'chkOunce
        '
        Me.chkOunce.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkOunce.AutoSize = True
        Me.chkOunce.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.chkOunce.Location = New System.Drawing.Point(355, 13)
        Me.chkOunce.Name = "chkOunce"
        Me.chkOunce.Size = New System.Drawing.Size(67, 23)
        Me.chkOunce.TabIndex = 1
        Me.chkOunce.Text = "أونصـة"
        Me.chkOunce.UseVisualStyleBackColor = True
        '
        'pnlRow6_Weights
        '
        Me.pnlRow6_Weights.Controls.Add(Me.lblCarat)
        Me.pnlRow6_Weights.Controls.Add(Me.txtCarat)
        Me.pnlRow6_Weights.Controls.Add(Me.lblGrossWeight)
        Me.pnlRow6_Weights.Controls.Add(Me.txtGrossWeight)
        Me.pnlRow6_Weights.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow6_Weights.Location = New System.Drawing.Point(0, 317)
        Me.pnlRow6_Weights.Name = "pnlRow6_Weights"
        Me.pnlRow6_Weights.Size = New System.Drawing.Size(856, 50)
        Me.pnlRow6_Weights.TabIndex = 6
        '
        'lblCarat
        '
        Me.lblCarat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCarat.AutoSize = True
        Me.lblCarat.Location = New System.Drawing.Point(750, 13)
        Me.lblCarat.Name = "lblCarat"
        Me.lblCarat.Size = New System.Drawing.Size(48, 19)
        Me.lblCarat.TabIndex = 0
        Me.lblCarat.Text = "العيــار:"
        '
        'txtCarat
        '
        Me.txtCarat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCarat.Location = New System.Drawing.Point(550, 10)
        Me.txtCarat.Name = "txtCarat"
        Me.txtCarat.Size = New System.Drawing.Size(190, 26)
        Me.txtCarat.TabIndex = 1
        '
        'lblGrossWeight
        '
        Me.lblGrossWeight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblGrossWeight.AutoSize = True
        Me.lblGrossWeight.Location = New System.Drawing.Point(380, 13)
        Me.lblGrossWeight.Name = "lblGrossWeight"
        Me.lblGrossWeight.Size = New System.Drawing.Size(80, 19)
        Me.lblGrossWeight.TabIndex = 2
        Me.lblGrossWeight.Text = "الوزن القائم:"
        '
        'txtGrossWeight
        '
        Me.txtGrossWeight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGrossWeight.Location = New System.Drawing.Point(160, 10)
        Me.txtGrossWeight.Name = "txtGrossWeight"
        Me.txtGrossWeight.Size = New System.Drawing.Size(210, 26)
        Me.txtGrossWeight.TabIndex = 3
        '
        'pnlRow7_NetWeight
        '
        Me.pnlRow7_NetWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRow7_NetWeight.Controls.Add(Me.lblNetWeight)
        Me.pnlRow7_NetWeight.Controls.Add(Me.txtNetWeight)
        Me.pnlRow7_NetWeight.Controls.Add(Me.rb24K)
        Me.pnlRow7_NetWeight.Controls.Add(Me.rb18K)
        Me.pnlRow7_NetWeight.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow7_NetWeight.Location = New System.Drawing.Point(0, 367)
        Me.pnlRow7_NetWeight.Name = "pnlRow7_NetWeight"
        Me.pnlRow7_NetWeight.Size = New System.Drawing.Size(856, 50)
        Me.pnlRow7_NetWeight.TabIndex = 7
        '
        'lblNetWeight
        '
        Me.lblNetWeight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNetWeight.AutoSize = True
        Me.lblNetWeight.Location = New System.Drawing.Point(379, 17)
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
        Me.txtNetWeight.Location = New System.Drawing.Point(159, 12)
        Me.txtNetWeight.Name = "txtNetWeight"
        Me.txtNetWeight.ReadOnly = True
        Me.txtNetWeight.Size = New System.Drawing.Size(210, 29)
        Me.txtNetWeight.TabIndex = 1
        '
        'rb24K
        '
        Me.rb24K.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rb24K.AutoSize = True
        Me.rb24K.Location = New System.Drawing.Point(661, 14)
        Me.rb24K.Name = "rb24K"
        Me.rb24K.Size = New System.Drawing.Size(51, 23)
        Me.rb24K.TabIndex = 2
        Me.rb24K.Text = "24K"
        '
        'rb18K
        '
        Me.rb18K.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rb18K.AutoSize = True
        Me.rb18K.Location = New System.Drawing.Point(568, 14)
        Me.rb18K.Name = "rb18K"
        Me.rb18K.Size = New System.Drawing.Size(51, 23)
        Me.rb18K.TabIndex = 3
        Me.rb18K.Text = "18K"
        '
        'pnlRow9_Statement
        '
        Me.pnlRow9_Statement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRow9_Statement.Controls.Add(Me.btnPrint)
        Me.pnlRow9_Statement.Controls.Add(Me.btnSave)
        Me.pnlRow9_Statement.Controls.Add(Me.btnNew)
        Me.pnlRow9_Statement.Controls.Add(Me.txtStatement)
        Me.pnlRow9_Statement.Controls.Add(Me.lblStatement)
        Me.pnlRow9_Statement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRow9_Statement.Location = New System.Drawing.Point(0, 417)
        Me.pnlRow9_Statement.Name = "pnlRow9_Statement"
        Me.pnlRow9_Statement.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlRow9_Statement.Size = New System.Drawing.Size(856, 283)
        Me.pnlRow9_Statement.TabIndex = 9
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.SeaShell
        Me.btnPrint.FlatAppearance.BorderSize = 0
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrint.Location = New System.Drawing.Point(17, 208)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(162, 45)
        Me.btnPrint.TabIndex = 5
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
        Me.btnSave.Location = New System.Drawing.Point(414, 208)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(222, 45)
        Me.btnSave.TabIndex = 6
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
        Me.btnNew.Location = New System.Drawing.Point(660, 208)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(183, 45)
        Me.btnNew.TabIndex = 7
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
        Me.txtStatement.Size = New System.Drawing.Size(826, 151)
        Me.txtStatement.TabIndex = 0
        '
        'lblStatement
        '
        Me.lblStatement.AutoSize = True
        Me.lblStatement.Location = New System.Drawing.Point(773, 12)
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
        Me.btnCancel.Location = New System.Drawing.Point(307, 9)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(129, 30)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "الغاء"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'frmBuySell
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(856, 700)
        Me.Controls.Add(Me.pnlRow9_Statement)
        Me.Controls.Add(Me.pnlRow7_NetWeight)
        Me.Controls.Add(Me.pnlRow6_Weights)
        Me.Controls.Add(Me.pnlRow5_Unit)
        Me.Controls.Add(Me.pnlRow4_Currency)
        Me.Controls.Add(Me.pnlRow3_Customer)
        Me.Controls.Add(Me.pnlRow2_POS)
        Me.Controls.Add(Me.pnlRow1_ID)
        Me.Controls.Add(Me.pnlTopBar)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmBuySell"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "البيع والشراء"
        Me.pnlTopBar.ResumeLayout(False)
        Me.pnlRow1_ID.ResumeLayout(False)
        Me.pnlRow1_ID.PerformLayout()
        Me.pnlRow2_POS.ResumeLayout(False)
        Me.pnlRow2_POS.PerformLayout()
        Me.pnlRow3_Customer.ResumeLayout(False)
        Me.pnlRow3_Customer.PerformLayout()
        Me.pnlRow4_Currency.ResumeLayout(False)
        Me.pnlRow4_Currency.PerformLayout()
        Me.pnlRow5_Unit.ResumeLayout(False)
        Me.pnlRow5_Unit.PerformLayout()
        Me.pnlRow6_Weights.ResumeLayout(False)
        Me.pnlRow6_Weights.PerformLayout()
        Me.pnlRow7_NetWeight.ResumeLayout(False)
        Me.pnlRow7_NetWeight.PerformLayout()
        Me.pnlRow9_Statement.ResumeLayout(False)
        Me.pnlRow9_Statement.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents btnPrint As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents btnCancel As Button
End Class