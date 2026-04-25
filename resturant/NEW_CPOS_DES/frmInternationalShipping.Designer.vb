<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmInternationalShipping
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
    Friend WithEvents pnlRow2_Metal As System.Windows.Forms.Panel
    Friend WithEvents pnlRow3_Customer As System.Windows.Forms.Panel
    Friend WithEvents pnlRow4_Safes As System.Windows.Forms.Panel
    Friend WithEvents pnlRow5_GrossWeight As System.Windows.Forms.Panel
    Friend WithEvents pnlRow6_Financials As System.Windows.Forms.Panel
    Friend WithEvents pnlRow7_Company As System.Windows.Forms.Panel
    Friend WithEvents pnlRow8_Statement As System.Windows.Forms.Panel
    Friend WithEvents pnlRow9_Buttons As System.Windows.Forms.Panel

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
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker

    ' السطر 2: المعدن
    Friend WithEvents lblMetal As System.Windows.Forms.Label
    Friend WithEvents cmbMetal As System.Windows.Forms.ComboBox

    ' السطر 3: الزبون
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents cmbCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchCustomer As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomerInfo As System.Windows.Forms.Label

    ' السطر 4: الخزائن (من - إلى)
    Friend WithEvents lblFromSafe As System.Windows.Forms.Label
    Friend WithEvents cmbFromSafe As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchFromSafe As System.Windows.Forms.TextBox
    Friend WithEvents lblToSafe As System.Windows.Forms.Label
    Friend WithEvents cmbToSafe As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchToSafe As System.Windows.Forms.TextBox

    ' السطر 5: الوزن القائم
    Friend WithEvents lblGrossWeight As System.Windows.Forms.Label
    Friend WithEvents txtGrossWeight As System.Windows.Forms.TextBox

    ' السطر 6: البيانات المالية (له، عليه، السعر، العملة)
    Friend WithEvents chkForHim As System.Windows.Forms.CheckBox
    Friend WithEvents chkOnHim As System.Windows.Forms.CheckBox
    Friend WithEvents lblPrice As System.Windows.Forms.Label
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents lblCurrency As System.Windows.Forms.Label
    Friend WithEvents cmbCurrency As System.Windows.Forms.ComboBox

    ' السطر 7: الشركة
    Friend WithEvents chkCompany As System.Windows.Forms.CheckBox
    Friend WithEvents pnlCompanyData As System.Windows.Forms.Panel
    Friend WithEvents lblCompany As System.Windows.Forms.Label
    Friend WithEvents cmbCompany As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchCompany As System.Windows.Forms.TextBox
    Friend WithEvents lblCompanyPrice As System.Windows.Forms.Label
    Friend WithEvents txtCompanyPrice As System.Windows.Forms.TextBox
    Friend WithEvents lblCompanyCurrency As System.Windows.Forms.Label
    Friend WithEvents cmbCompanyCurrency As System.Windows.Forms.ComboBox

    ' السطر 8: البيان
    Friend WithEvents txtStatement As System.Windows.Forms.TextBox
    Friend WithEvents lblStatement As System.Windows.Forms.Label

    ' السطر 9: الأزرار السفلية
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlTopBar = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.btnMinimize = New System.Windows.Forms.Button()
        Me.btnMaximize = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlRow1_ID = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblID = New System.Windows.Forms.Label()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.pnlRow2_Metal = New System.Windows.Forms.Panel()
        Me.lblMetal = New System.Windows.Forms.Label()
        Me.cmbMetal = New System.Windows.Forms.ComboBox()
        Me.pnlRow3_Customer = New System.Windows.Forms.Panel()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.cmbCustomer = New System.Windows.Forms.ComboBox()
        Me.txtSearchCustomer = New System.Windows.Forms.TextBox()
        Me.lblCustomerInfo = New System.Windows.Forms.Label()
        Me.pnlRow4_Safes = New System.Windows.Forms.Panel()
        Me.lblFromTreasuryInfo = New System.Windows.Forms.Label()
        Me.lblToTreasuryInfo = New System.Windows.Forms.Label()
        Me.lblFromSafe = New System.Windows.Forms.Label()
        Me.cmbFromSafe = New System.Windows.Forms.ComboBox()
        Me.txtSearchFromSafe = New System.Windows.Forms.TextBox()
        Me.lblToSafe = New System.Windows.Forms.Label()
        Me.cmbToSafe = New System.Windows.Forms.ComboBox()
        Me.txtSearchToSafe = New System.Windows.Forms.TextBox()
        Me.pnlRow5_GrossWeight = New System.Windows.Forms.Panel()
        Me.lblGrossWeight = New System.Windows.Forms.Label()
        Me.txtGrossWeight = New System.Windows.Forms.TextBox()
        Me.pnlRow6_Financials = New System.Windows.Forms.Panel()
        Me.chkForHim = New System.Windows.Forms.CheckBox()
        Me.chkOnHim = New System.Windows.Forms.CheckBox()
        Me.lblPrice = New System.Windows.Forms.Label()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.lblCurrency = New System.Windows.Forms.Label()
        Me.cmbCurrency = New System.Windows.Forms.ComboBox()
        Me.pnlRow7_Company = New System.Windows.Forms.Panel()
        Me.pnlCompanyData = New System.Windows.Forms.Panel()
        Me.lblCompany = New System.Windows.Forms.Label()
        Me.cmbCompany = New System.Windows.Forms.ComboBox()
        Me.txtSearchCompany = New System.Windows.Forms.TextBox()
        Me.lblCompanyPrice = New System.Windows.Forms.Label()
        Me.txtCompanyPrice = New System.Windows.Forms.TextBox()
        Me.lblCompanyCurrency = New System.Windows.Forms.Label()
        Me.cmbCompanyCurrency = New System.Windows.Forms.ComboBox()
        Me.chkCompany = New System.Windows.Forms.CheckBox()
        Me.pnlRow8_Statement = New System.Windows.Forms.Panel()
        Me.txtStatement = New System.Windows.Forms.TextBox()
        Me.lblStatement = New System.Windows.Forms.Label()
        Me.pnlRow9_Buttons = New System.Windows.Forms.Panel()
        Me.btnDeliverytocustomer = New System.Windows.Forms.Button()
        Me.btndepositintocustumerAccount = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.pnlTopBar.SuspendLayout()
        Me.pnlRow1_ID.SuspendLayout()
        Me.pnlRow2_Metal.SuspendLayout()
        Me.pnlRow3_Customer.SuspendLayout()
        Me.pnlRow4_Safes.SuspendLayout()
        Me.pnlRow5_GrossWeight.SuspendLayout()
        Me.pnlRow6_Financials.SuspendLayout()
        Me.pnlRow7_Company.SuspendLayout()
        Me.pnlCompanyData.SuspendLayout()
        Me.pnlRow8_Statement.SuspendLayout()
        Me.pnlRow9_Buttons.SuspendLayout()
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
        Me.lblTitle.Text = " شاشة شحن دولي (مشروع المعادن)"
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
        Me.pnlRow1_ID.Controls.Add(Me.btnCancel)
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
        Me.pnlRow1_ID.TabIndex = 1
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.Red
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.Location = New System.Drawing.Point(314, 9)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(129, 30)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "الغاء"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'lblID
        '
        Me.lblID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblID.AutoSize = True
        Me.lblID.Location = New System.Drawing.Point(764, 16)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(72, 19)
        Me.lblID.TabIndex = 0
        Me.lblID.Text = "رقم الحركة:"
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Location = New System.Drawing.Point(454, 10)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(40, 26)
        Me.btnNext.TabIndex = 1
        Me.btnNext.Text = ">"
        '
        'txtID
        '
        Me.txtID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtID.Location = New System.Drawing.Point(504, 10)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(200, 26)
        Me.txtID.TabIndex = 2
        Me.txtID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnPrev
        '
        Me.btnPrev.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrev.Location = New System.Drawing.Point(710, 10)
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
        Me.dtpDate.Size = New System.Drawing.Size(165, 26)
        Me.dtpDate.TabIndex = 4
        '
        'pnlRow2_Metal
        '
        Me.pnlRow2_Metal.Controls.Add(Me.lblMetal)
        Me.pnlRow2_Metal.Controls.Add(Me.cmbMetal)
        Me.pnlRow2_Metal.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow2_Metal.Location = New System.Drawing.Point(0, 95)
        Me.pnlRow2_Metal.Name = "pnlRow2_Metal"
        Me.pnlRow2_Metal.Size = New System.Drawing.Size(856, 50)
        Me.pnlRow2_Metal.TabIndex = 2
        '
        'lblMetal
        '
        Me.lblMetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMetal.AutoSize = True
        Me.lblMetal.Location = New System.Drawing.Point(777, 17)
        Me.lblMetal.Name = "lblMetal"
        Me.lblMetal.Size = New System.Drawing.Size(53, 19)
        Me.lblMetal.TabIndex = 0
        Me.lblMetal.Text = "المعدن:"
        '
        'cmbMetal
        '
        Me.cmbMetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbMetal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMetal.Location = New System.Drawing.Point(490, 14)
        Me.cmbMetal.Name = "cmbMetal"
        Me.cmbMetal.Size = New System.Drawing.Size(260, 27)
        Me.cmbMetal.TabIndex = 1
        '
        'pnlRow3_Customer
        '
        Me.pnlRow3_Customer.Controls.Add(Me.lblCustomer)
        Me.pnlRow3_Customer.Controls.Add(Me.cmbCustomer)
        Me.pnlRow3_Customer.Controls.Add(Me.txtSearchCustomer)
        Me.pnlRow3_Customer.Controls.Add(Me.lblCustomerInfo)
        Me.pnlRow3_Customer.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow3_Customer.Location = New System.Drawing.Point(0, 145)
        Me.pnlRow3_Customer.Name = "pnlRow3_Customer"
        Me.pnlRow3_Customer.Size = New System.Drawing.Size(856, 69)
        Me.pnlRow3_Customer.TabIndex = 3
        '
        'lblCustomer
        '
        Me.lblCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Location = New System.Drawing.Point(774, 27)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(62, 19)
        Me.lblCustomer.TabIndex = 0
        Me.lblCustomer.Text = "الـزبــــــون:"
        '
        'cmbCustomer
        '
        Me.cmbCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCustomer.Location = New System.Drawing.Point(490, 24)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(260, 27)
        Me.cmbCustomer.TabIndex = 1
        '
        'txtSearchCustomer
        '
        Me.txtSearchCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearchCustomer.Location = New System.Drawing.Point(330, 24)
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
        Me.lblCustomerInfo.Size = New System.Drawing.Size(300, 56)
        Me.lblCustomerInfo.TabIndex = 3
        Me.lblCustomerInfo.Text = "معلومات الزبون المختصرة ستظهر هنا..."
        '
        'pnlRow4_Safes
        '
        Me.pnlRow4_Safes.Controls.Add(Me.lblFromTreasuryInfo)
        Me.pnlRow4_Safes.Controls.Add(Me.lblToTreasuryInfo)
        Me.pnlRow4_Safes.Controls.Add(Me.lblFromSafe)
        Me.pnlRow4_Safes.Controls.Add(Me.cmbFromSafe)
        Me.pnlRow4_Safes.Controls.Add(Me.txtSearchFromSafe)
        Me.pnlRow4_Safes.Controls.Add(Me.lblToSafe)
        Me.pnlRow4_Safes.Controls.Add(Me.cmbToSafe)
        Me.pnlRow4_Safes.Controls.Add(Me.txtSearchToSafe)
        Me.pnlRow4_Safes.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow4_Safes.Location = New System.Drawing.Point(0, 214)
        Me.pnlRow4_Safes.Name = "pnlRow4_Safes"
        Me.pnlRow4_Safes.Size = New System.Drawing.Size(856, 100)
        Me.pnlRow4_Safes.TabIndex = 4
        '
        'lblFromTreasuryInfo
        '
        Me.lblFromTreasuryInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFromTreasuryInfo.ForeColor = System.Drawing.Color.DimGray
        Me.lblFromTreasuryInfo.Location = New System.Drawing.Point(468, 44)
        Me.lblFromTreasuryInfo.Name = "lblFromTreasuryInfo"
        Me.lblFromTreasuryInfo.Size = New System.Drawing.Size(300, 45)
        Me.lblFromTreasuryInfo.TabIndex = 4
        Me.lblFromTreasuryInfo.Text = "معلومات الخزينة  المختصرة ستظهر هنا..."
        '
        'lblToTreasuryInfo
        '
        Me.lblToTreasuryInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblToTreasuryInfo.ForeColor = System.Drawing.Color.DimGray
        Me.lblToTreasuryInfo.Location = New System.Drawing.Point(26, 41)
        Me.lblToTreasuryInfo.Name = "lblToTreasuryInfo"
        Me.lblToTreasuryInfo.Size = New System.Drawing.Size(300, 45)
        Me.lblToTreasuryInfo.TabIndex = 5
        Me.lblToTreasuryInfo.Text = "معلومات الخزينة  المختصرة ستظهر هنا..."
        '
        'lblFromSafe
        '
        Me.lblFromSafe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFromSafe.AutoSize = True
        Me.lblFromSafe.Location = New System.Drawing.Point(780, 13)
        Me.lblFromSafe.Name = "lblFromSafe"
        Me.lblFromSafe.Size = New System.Drawing.Size(65, 19)
        Me.lblFromSafe.TabIndex = 0
        Me.lblFromSafe.Text = "من خزينة:"
        '
        'cmbFromSafe
        '
        Me.cmbFromSafe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbFromSafe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFromSafe.Location = New System.Drawing.Point(571, 10)
        Me.cmbFromSafe.Name = "cmbFromSafe"
        Me.cmbFromSafe.Size = New System.Drawing.Size(200, 27)
        Me.cmbFromSafe.TabIndex = 1
        '
        'txtSearchFromSafe
        '
        Me.txtSearchFromSafe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearchFromSafe.Location = New System.Drawing.Point(461, 10)
        Me.txtSearchFromSafe.Name = "txtSearchFromSafe"
        Me.txtSearchFromSafe.Size = New System.Drawing.Size(100, 26)
        Me.txtSearchFromSafe.TabIndex = 2
        '
        'lblToSafe
        '
        Me.lblToSafe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblToSafe.AutoSize = True
        Me.lblToSafe.Location = New System.Drawing.Point(333, 13)
        Me.lblToSafe.Name = "lblToSafe"
        Me.lblToSafe.Size = New System.Drawing.Size(69, 19)
        Me.lblToSafe.TabIndex = 3
        Me.lblToSafe.Text = "إلى خزينة:"
        '
        'cmbToSafe
        '
        Me.cmbToSafe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbToSafe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbToSafe.Location = New System.Drawing.Point(129, 10)
        Me.cmbToSafe.Name = "cmbToSafe"
        Me.cmbToSafe.Size = New System.Drawing.Size(200, 27)
        Me.cmbToSafe.TabIndex = 4
        '
        'txtSearchToSafe
        '
        Me.txtSearchToSafe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearchToSafe.Location = New System.Drawing.Point(19, 10)
        Me.txtSearchToSafe.Name = "txtSearchToSafe"
        Me.txtSearchToSafe.Size = New System.Drawing.Size(100, 26)
        Me.txtSearchToSafe.TabIndex = 5
        '
        'pnlRow5_GrossWeight
        '
        Me.pnlRow5_GrossWeight.Controls.Add(Me.lblGrossWeight)
        Me.pnlRow5_GrossWeight.Controls.Add(Me.txtGrossWeight)
        Me.pnlRow5_GrossWeight.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow5_GrossWeight.Location = New System.Drawing.Point(0, 314)
        Me.pnlRow5_GrossWeight.Name = "pnlRow5_GrossWeight"
        Me.pnlRow5_GrossWeight.Size = New System.Drawing.Size(856, 50)
        Me.pnlRow5_GrossWeight.TabIndex = 5
        '
        'lblGrossWeight
        '
        Me.lblGrossWeight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblGrossWeight.AutoSize = True
        Me.lblGrossWeight.Location = New System.Drawing.Point(756, 13)
        Me.lblGrossWeight.Name = "lblGrossWeight"
        Me.lblGrossWeight.Size = New System.Drawing.Size(80, 19)
        Me.lblGrossWeight.TabIndex = 0
        Me.lblGrossWeight.Text = "الوزن القائم:"
        '
        'txtGrossWeight
        '
        Me.txtGrossWeight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGrossWeight.Location = New System.Drawing.Point(550, 10)
        Me.txtGrossWeight.Name = "txtGrossWeight"
        Me.txtGrossWeight.Size = New System.Drawing.Size(200, 26)
        Me.txtGrossWeight.TabIndex = 1
        '
        'pnlRow6_Financials
        '
        Me.pnlRow6_Financials.Controls.Add(Me.chkForHim)
        Me.pnlRow6_Financials.Controls.Add(Me.chkOnHim)
        Me.pnlRow6_Financials.Controls.Add(Me.lblPrice)
        Me.pnlRow6_Financials.Controls.Add(Me.txtPrice)
        Me.pnlRow6_Financials.Controls.Add(Me.lblCurrency)
        Me.pnlRow6_Financials.Controls.Add(Me.cmbCurrency)
        Me.pnlRow6_Financials.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow6_Financials.Location = New System.Drawing.Point(0, 364)
        Me.pnlRow6_Financials.Name = "pnlRow6_Financials"
        Me.pnlRow6_Financials.Size = New System.Drawing.Size(856, 50)
        Me.pnlRow6_Financials.TabIndex = 6
        '
        'chkForHim
        '
        Me.chkForHim.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkForHim.AutoSize = True
        Me.chkForHim.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.chkForHim.Location = New System.Drawing.Point(774, 12)
        Me.chkForHim.Name = "chkForHim"
        Me.chkForHim.Size = New System.Drawing.Size(42, 23)
        Me.chkForHim.TabIndex = 0
        Me.chkForHim.Text = "لـه"
        Me.chkForHim.UseVisualStyleBackColor = True
        '
        'chkOnHim
        '
        Me.chkOnHim.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkOnHim.AutoSize = True
        Me.chkOnHim.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.chkOnHim.Location = New System.Drawing.Point(676, 12)
        Me.chkOnHim.Name = "chkOnHim"
        Me.chkOnHim.Size = New System.Drawing.Size(53, 23)
        Me.chkOnHim.TabIndex = 1
        Me.chkOnHim.Text = "عليه"
        Me.chkOnHim.UseVisualStyleBackColor = True
        '
        'lblPrice
        '
        Me.lblPrice.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPrice.AutoSize = True
        Me.lblPrice.Location = New System.Drawing.Point(560, 13)
        Me.lblPrice.Name = "lblPrice"
        Me.lblPrice.Size = New System.Drawing.Size(47, 19)
        Me.lblPrice.TabIndex = 2
        Me.lblPrice.Text = "السعر:"
        '
        'txtPrice
        '
        Me.txtPrice.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPrice.Location = New System.Drawing.Point(400, 10)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(150, 26)
        Me.txtPrice.TabIndex = 3
        '
        'lblCurrency
        '
        Me.lblCurrency.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCurrency.AutoSize = True
        Me.lblCurrency.Location = New System.Drawing.Point(310, 13)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(49, 19)
        Me.lblCurrency.TabIndex = 4
        Me.lblCurrency.Text = "العملة:"
        '
        'cmbCurrency
        '
        Me.cmbCurrency.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCurrency.Location = New System.Drawing.Point(140, 10)
        Me.cmbCurrency.Name = "cmbCurrency"
        Me.cmbCurrency.Size = New System.Drawing.Size(160, 27)
        Me.cmbCurrency.TabIndex = 5
        '
        'pnlRow7_Company
        '
        Me.pnlRow7_Company.Controls.Add(Me.pnlCompanyData)
        Me.pnlRow7_Company.Controls.Add(Me.chkCompany)
        Me.pnlRow7_Company.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow7_Company.Location = New System.Drawing.Point(0, 414)
        Me.pnlRow7_Company.Name = "pnlRow7_Company"
        Me.pnlRow7_Company.Size = New System.Drawing.Size(856, 50)
        Me.pnlRow7_Company.TabIndex = 7
        '
        'pnlCompanyData
        '
        Me.pnlCompanyData.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlCompanyData.Controls.Add(Me.lblCompany)
        Me.pnlCompanyData.Controls.Add(Me.cmbCompany)
        Me.pnlCompanyData.Controls.Add(Me.txtSearchCompany)
        Me.pnlCompanyData.Controls.Add(Me.lblCompanyPrice)
        Me.pnlCompanyData.Controls.Add(Me.txtCompanyPrice)
        Me.pnlCompanyData.Controls.Add(Me.lblCompanyCurrency)
        Me.pnlCompanyData.Controls.Add(Me.cmbCompanyCurrency)
        Me.pnlCompanyData.Location = New System.Drawing.Point(10, 0)
        Me.pnlCompanyData.Name = "pnlCompanyData"
        Me.pnlCompanyData.Size = New System.Drawing.Size(740, 50)
        Me.pnlCompanyData.TabIndex = 1
        Me.pnlCompanyData.Visible = False
        '
        'lblCompany
        '
        Me.lblCompany.AutoSize = True
        Me.lblCompany.Location = New System.Drawing.Point(663, 13)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(51, 19)
        Me.lblCompany.TabIndex = 0
        Me.lblCompany.Text = "الشركة:"
        '
        'cmbCompany
        '
        Me.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCompany.Location = New System.Drawing.Point(493, 10)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(170, 27)
        Me.cmbCompany.TabIndex = 1
        '
        'txtSearchCompany
        '
        Me.txtSearchCompany.Location = New System.Drawing.Point(389, 10)
        Me.txtSearchCompany.Name = "txtSearchCompany"
        Me.txtSearchCompany.Size = New System.Drawing.Size(100, 26)
        Me.txtSearchCompany.TabIndex = 2
        '
        'lblCompanyPrice
        '
        Me.lblCompanyPrice.AutoSize = True
        Me.lblCompanyPrice.Location = New System.Drawing.Point(324, 15)
        Me.lblCompanyPrice.Name = "lblCompanyPrice"
        Me.lblCompanyPrice.Size = New System.Drawing.Size(47, 19)
        Me.lblCompanyPrice.TabIndex = 3
        Me.lblCompanyPrice.Text = "السعر:"
        '
        'txtCompanyPrice
        '
        Me.txtCompanyPrice.Location = New System.Drawing.Point(221, 10)
        Me.txtCompanyPrice.Name = "txtCompanyPrice"
        Me.txtCompanyPrice.Size = New System.Drawing.Size(100, 26)
        Me.txtCompanyPrice.TabIndex = 4
        '
        'lblCompanyCurrency
        '
        Me.lblCompanyCurrency.AutoSize = True
        Me.lblCompanyCurrency.Location = New System.Drawing.Point(147, 15)
        Me.lblCompanyCurrency.Name = "lblCompanyCurrency"
        Me.lblCompanyCurrency.Size = New System.Drawing.Size(49, 19)
        Me.lblCompanyCurrency.TabIndex = 5
        Me.lblCompanyCurrency.Text = "العملة:"
        '
        'cmbCompanyCurrency
        '
        Me.cmbCompanyCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCompanyCurrency.Location = New System.Drawing.Point(21, 10)
        Me.cmbCompanyCurrency.Name = "cmbCompanyCurrency"
        Me.cmbCompanyCurrency.Size = New System.Drawing.Size(126, 27)
        Me.cmbCompanyCurrency.TabIndex = 6
        '
        'chkCompany
        '
        Me.chkCompany.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkCompany.AutoSize = True
        Me.chkCompany.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.chkCompany.Location = New System.Drawing.Point(774, 13)
        Me.chkCompany.Name = "chkCompany"
        Me.chkCompany.Size = New System.Drawing.Size(67, 23)
        Me.chkCompany.TabIndex = 0
        Me.chkCompany.Text = "الشركة"
        Me.chkCompany.UseVisualStyleBackColor = True
        '
        'pnlRow8_Statement
        '
        Me.pnlRow8_Statement.Controls.Add(Me.txtStatement)
        Me.pnlRow8_Statement.Controls.Add(Me.lblStatement)
        Me.pnlRow8_Statement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRow8_Statement.Location = New System.Drawing.Point(0, 464)
        Me.pnlRow8_Statement.Name = "pnlRow8_Statement"
        Me.pnlRow8_Statement.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlRow8_Statement.Size = New System.Drawing.Size(856, 166)
        Me.pnlRow8_Statement.TabIndex = 8
        '
        'txtStatement
        '
        Me.txtStatement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtStatement.Location = New System.Drawing.Point(15, 39)
        Me.txtStatement.Multiline = True
        Me.txtStatement.Name = "txtStatement"
        Me.txtStatement.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtStatement.Size = New System.Drawing.Size(826, 112)
        Me.txtStatement.TabIndex = 0
        '
        'lblStatement
        '
        Me.lblStatement.AutoSize = True
        Me.lblStatement.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblStatement.Location = New System.Drawing.Point(15, 15)
        Me.lblStatement.Name = "lblStatement"
        Me.lblStatement.Padding = New System.Windows.Forms.Padding(0, 0, 0, 5)
        Me.lblStatement.Size = New System.Drawing.Size(66, 24)
        Me.lblStatement.TabIndex = 1
        Me.lblStatement.Text = "البيـــــــــــان:"
        '
        'pnlRow9_Buttons
        '
        Me.pnlRow9_Buttons.Controls.Add(Me.btnDeliverytocustomer)
        Me.pnlRow9_Buttons.Controls.Add(Me.btndepositintocustumerAccount)
        Me.pnlRow9_Buttons.Controls.Add(Me.btnNew)
        Me.pnlRow9_Buttons.Controls.Add(Me.btnSave)
        Me.pnlRow9_Buttons.Controls.Add(Me.btnPrint)
        Me.pnlRow9_Buttons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlRow9_Buttons.Location = New System.Drawing.Point(0, 630)
        Me.pnlRow9_Buttons.Name = "pnlRow9_Buttons"
        Me.pnlRow9_Buttons.Size = New System.Drawing.Size(856, 70)
        Me.pnlRow9_Buttons.TabIndex = 9
        '
        'btnDeliverytocustomer
        '
        Me.btnDeliverytocustomer.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnDeliverytocustomer.BackColor = System.Drawing.Color.Honeydew
        Me.btnDeliverytocustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeliverytocustomer.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnDeliverytocustomer.Location = New System.Drawing.Point(338, 10)
        Me.btnDeliverytocustomer.Name = "btnDeliverytocustomer"
        Me.btnDeliverytocustomer.Size = New System.Drawing.Size(159, 45)
        Me.btnDeliverytocustomer.TabIndex = 5
        Me.btnDeliverytocustomer.Text = "تسليم للزبون"
        Me.btnDeliverytocustomer.UseVisualStyleBackColor = False
        '
        'btndepositintocustumerAccount
        '
        Me.btndepositintocustumerAccount.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btndepositintocustumerAccount.BackColor = System.Drawing.Color.Honeydew
        Me.btndepositintocustumerAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btndepositintocustumerAccount.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btndepositintocustumerAccount.Location = New System.Drawing.Point(176, 10)
        Me.btndepositintocustumerAccount.Name = "btndepositintocustumerAccount"
        Me.btndepositintocustumerAccount.Size = New System.Drawing.Size(154, 45)
        Me.btndepositintocustumerAccount.TabIndex = 4
        Me.btndepositintocustumerAccount.Text = "ايداع لحساب الزبون"
        Me.btndepositintocustumerAccount.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNew.BackColor = System.Drawing.Color.Azure
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnNew.Location = New System.Drawing.Point(696, 10)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(150, 45)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "جديد"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnSave.BackColor = System.Drawing.Color.PaleGreen
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.Location = New System.Drawing.Point(510, 10)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(182, 45)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "حفظ"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.BackColor = System.Drawing.Color.SeaShell
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrint.Location = New System.Drawing.Point(12, 10)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(150, 45)
        Me.btnPrint.TabIndex = 2
        Me.btnPrint.Text = "طباعة"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'frmInternationalShipping
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(856, 700)
        Me.Controls.Add(Me.pnlRow8_Statement)
        Me.Controls.Add(Me.pnlRow9_Buttons)
        Me.Controls.Add(Me.pnlRow7_Company)
        Me.Controls.Add(Me.pnlRow6_Financials)
        Me.Controls.Add(Me.pnlRow5_GrossWeight)
        Me.Controls.Add(Me.pnlRow4_Safes)
        Me.Controls.Add(Me.pnlRow3_Customer)
        Me.Controls.Add(Me.pnlRow2_Metal)
        Me.Controls.Add(Me.pnlRow1_ID)
        Me.Controls.Add(Me.pnlTopBar)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmInternationalShipping"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "شحن دولي"
        Me.pnlTopBar.ResumeLayout(False)
        Me.pnlRow1_ID.ResumeLayout(False)
        Me.pnlRow1_ID.PerformLayout()
        Me.pnlRow2_Metal.ResumeLayout(False)
        Me.pnlRow2_Metal.PerformLayout()
        Me.pnlRow3_Customer.ResumeLayout(False)
        Me.pnlRow3_Customer.PerformLayout()
        Me.pnlRow4_Safes.ResumeLayout(False)
        Me.pnlRow4_Safes.PerformLayout()
        Me.pnlRow5_GrossWeight.ResumeLayout(False)
        Me.pnlRow5_GrossWeight.PerformLayout()
        Me.pnlRow6_Financials.ResumeLayout(False)
        Me.pnlRow6_Financials.PerformLayout()
        Me.pnlRow7_Company.ResumeLayout(False)
        Me.pnlRow7_Company.PerformLayout()
        Me.pnlCompanyData.ResumeLayout(False)
        Me.pnlCompanyData.PerformLayout()
        Me.pnlRow8_Statement.ResumeLayout(False)
        Me.pnlRow8_Statement.PerformLayout()
        Me.pnlRow9_Buttons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblFromTreasuryInfo As Label
    Friend WithEvents lblToTreasuryInfo As Label
    Friend WithEvents btnDeliverytocustomer As Button
    Friend WithEvents btndepositintocustumerAccount As Button
    Friend WithEvents btnCancel As Button
End Class