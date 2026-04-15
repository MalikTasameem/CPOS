<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTransfers
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
    Friend WithEvents pnlRow3_Customers As System.Windows.Forms.Panel
    Friend WithEvents pnlRow4_GrossWeight As System.Windows.Forms.Panel
    Friend WithEvents pnlRow5_Statement As System.Windows.Forms.Panel
    Friend WithEvents pnlRow6_Buttons As System.Windows.Forms.Panel

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

    ' السطر 3: الزبائن (من زبون - إلى زبون)
    Friend WithEvents lblFromCustomer As System.Windows.Forms.Label
    Friend WithEvents cmbFromCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchFromCustomer As System.Windows.Forms.TextBox
    Friend WithEvents lblToCustomer As System.Windows.Forms.Label
    Friend WithEvents cmbToCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchToCustomer As System.Windows.Forms.TextBox

    ' السطر 4: الوزن القائم
    Friend WithEvents lblGrossWeight As System.Windows.Forms.Label
    Friend WithEvents txtGrossWeight As System.Windows.Forms.TextBox

    ' السطر 5: البيان
    Friend WithEvents txtStatement As System.Windows.Forms.TextBox
    Friend WithEvents lblStatement As System.Windows.Forms.Label

    ' السطر 6: الأزرار السفلية
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
        Me.pnlRow3_Customers = New System.Windows.Forms.Panel()
        Me.lblfromCustomerInfo = New System.Windows.Forms.Label()
        Me.lbltoCustomerInfo = New System.Windows.Forms.Label()
        Me.lblFromCustomer = New System.Windows.Forms.Label()
        Me.cmbFromCustomer = New System.Windows.Forms.ComboBox()
        Me.txtSearchFromCustomer = New System.Windows.Forms.TextBox()
        Me.lblToCustomer = New System.Windows.Forms.Label()
        Me.cmbToCustomer = New System.Windows.Forms.ComboBox()
        Me.txtSearchToCustomer = New System.Windows.Forms.TextBox()
        Me.pnlRow4_GrossWeight = New System.Windows.Forms.Panel()
        Me.lblGrossWeight = New System.Windows.Forms.Label()
        Me.txtGrossWeight = New System.Windows.Forms.TextBox()
        Me.pnlRow5_Statement = New System.Windows.Forms.Panel()
        Me.txtStatement = New System.Windows.Forms.TextBox()
        Me.lblStatement = New System.Windows.Forms.Label()
        Me.pnlRow6_Buttons = New System.Windows.Forms.Panel()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.RB_Treasury = New System.Windows.Forms.RadioButton()
        Me.RB_Clients = New System.Windows.Forms.RadioButton()
        Me.pnlTopBar.SuspendLayout()
        Me.pnlRow1_ID.SuspendLayout()
        Me.pnlRow2_Metal.SuspendLayout()
        Me.pnlRow3_Customers.SuspendLayout()
        Me.pnlRow4_GrossWeight.SuspendLayout()
        Me.pnlRow5_Statement.SuspendLayout()
        Me.pnlRow6_Buttons.SuspendLayout()
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
        Me.lblTitle.Text = " شاشة التحويلات (مشروع المعادن)"
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
        Me.btnCancel.Location = New System.Drawing.Point(309, 9)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(129, 30)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "الغاء"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'lblID
        '
        Me.lblID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblID.AutoSize = True
        Me.lblID.Location = New System.Drawing.Point(750, 15)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(72, 19)
        Me.lblID.TabIndex = 0
        Me.lblID.Text = "رقم الحركة:"
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Location = New System.Drawing.Point(444, 11)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(40, 26)
        Me.btnNext.TabIndex = 1
        Me.btnNext.Text = ">"
        '
        'txtID
        '
        Me.txtID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtID.Location = New System.Drawing.Point(490, 10)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(200, 26)
        Me.txtID.TabIndex = 2
        Me.txtID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnPrev
        '
        Me.btnPrev.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrev.Location = New System.Drawing.Point(696, 11)
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
        Me.dtpDate.Size = New System.Drawing.Size(250, 26)
        Me.dtpDate.TabIndex = 4
        '
        'pnlRow2_Metal
        '
        Me.pnlRow2_Metal.Controls.Add(Me.RB_Treasury)
        Me.pnlRow2_Metal.Controls.Add(Me.RB_Clients)
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
        Me.lblMetal.Location = New System.Drawing.Point(750, 13)
        Me.lblMetal.Name = "lblMetal"
        Me.lblMetal.Size = New System.Drawing.Size(53, 19)
        Me.lblMetal.TabIndex = 0
        Me.lblMetal.Text = "المعدن:"
        '
        'cmbMetal
        '
        Me.cmbMetal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbMetal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMetal.Location = New System.Drawing.Point(490, 10)
        Me.cmbMetal.Name = "cmbMetal"
        Me.cmbMetal.Size = New System.Drawing.Size(250, 27)
        Me.cmbMetal.TabIndex = 1
        '
        'pnlRow3_Customers
        '
        Me.pnlRow3_Customers.Controls.Add(Me.lblfromCustomerInfo)
        Me.pnlRow3_Customers.Controls.Add(Me.lbltoCustomerInfo)
        Me.pnlRow3_Customers.Controls.Add(Me.lblFromCustomer)
        Me.pnlRow3_Customers.Controls.Add(Me.cmbFromCustomer)
        Me.pnlRow3_Customers.Controls.Add(Me.txtSearchFromCustomer)
        Me.pnlRow3_Customers.Controls.Add(Me.lblToCustomer)
        Me.pnlRow3_Customers.Controls.Add(Me.cmbToCustomer)
        Me.pnlRow3_Customers.Controls.Add(Me.txtSearchToCustomer)
        Me.pnlRow3_Customers.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow3_Customers.Location = New System.Drawing.Point(0, 145)
        Me.pnlRow3_Customers.Name = "pnlRow3_Customers"
        Me.pnlRow3_Customers.Size = New System.Drawing.Size(856, 133)
        Me.pnlRow3_Customers.TabIndex = 3
        '
        'lblfromCustomerInfo
        '
        Me.lblfromCustomerInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblfromCustomerInfo.ForeColor = System.Drawing.Color.DimGray
        Me.lblfromCustomerInfo.Location = New System.Drawing.Point(456, 54)
        Me.lblfromCustomerInfo.Name = "lblfromCustomerInfo"
        Me.lblfromCustomerInfo.Size = New System.Drawing.Size(307, 60)
        Me.lblfromCustomerInfo.TabIndex = 7
        Me.lblfromCustomerInfo.Text = "معلومات الزبون المختصرة ستظهر هنا..."
        '
        'lbltoCustomerInfo
        '
        Me.lbltoCustomerInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbltoCustomerInfo.ForeColor = System.Drawing.Color.DimGray
        Me.lbltoCustomerInfo.Location = New System.Drawing.Point(27, 54)
        Me.lbltoCustomerInfo.Name = "lbltoCustomerInfo"
        Me.lbltoCustomerInfo.Size = New System.Drawing.Size(307, 60)
        Me.lbltoCustomerInfo.TabIndex = 6
        Me.lbltoCustomerInfo.Text = "معلومات الزبون المختصرة ستظهر هنا..."
        '
        'lblFromCustomer
        '
        Me.lblFromCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFromCustomer.AutoSize = True
        Me.lblFromCustomer.Location = New System.Drawing.Point(776, 13)
        Me.lblFromCustomer.Name = "lblFromCustomer"
        Me.lblFromCustomer.Size = New System.Drawing.Size(60, 19)
        Me.lblFromCustomer.TabIndex = 0
        Me.lblFromCustomer.Text = "من زبون:"
        '
        'cmbFromCustomer
        '
        Me.cmbFromCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbFromCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFromCustomer.Location = New System.Drawing.Point(566, 10)
        Me.cmbFromCustomer.Name = "cmbFromCustomer"
        Me.cmbFromCustomer.Size = New System.Drawing.Size(200, 27)
        Me.cmbFromCustomer.TabIndex = 1
        '
        'txtSearchFromCustomer
        '
        Me.txtSearchFromCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearchFromCustomer.Location = New System.Drawing.Point(456, 10)
        Me.txtSearchFromCustomer.Name = "txtSearchFromCustomer"
        Me.txtSearchFromCustomer.Size = New System.Drawing.Size(100, 26)
        Me.txtSearchFromCustomer.TabIndex = 2
        '
        'lblToCustomer
        '
        Me.lblToCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblToCustomer.AutoSize = True
        Me.lblToCustomer.Location = New System.Drawing.Point(347, 13)
        Me.lblToCustomer.Name = "lblToCustomer"
        Me.lblToCustomer.Size = New System.Drawing.Size(64, 19)
        Me.lblToCustomer.TabIndex = 3
        Me.lblToCustomer.Text = "إلى زبون:"
        '
        'cmbToCustomer
        '
        Me.cmbToCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbToCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbToCustomer.Location = New System.Drawing.Point(137, 10)
        Me.cmbToCustomer.Name = "cmbToCustomer"
        Me.cmbToCustomer.Size = New System.Drawing.Size(200, 27)
        Me.cmbToCustomer.TabIndex = 4
        '
        'txtSearchToCustomer
        '
        Me.txtSearchToCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearchToCustomer.Location = New System.Drawing.Point(27, 10)
        Me.txtSearchToCustomer.Name = "txtSearchToCustomer"
        Me.txtSearchToCustomer.Size = New System.Drawing.Size(100, 26)
        Me.txtSearchToCustomer.TabIndex = 5
        '
        'pnlRow4_GrossWeight
        '
        Me.pnlRow4_GrossWeight.Controls.Add(Me.lblGrossWeight)
        Me.pnlRow4_GrossWeight.Controls.Add(Me.txtGrossWeight)
        Me.pnlRow4_GrossWeight.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRow4_GrossWeight.Location = New System.Drawing.Point(0, 278)
        Me.pnlRow4_GrossWeight.Name = "pnlRow4_GrossWeight"
        Me.pnlRow4_GrossWeight.Size = New System.Drawing.Size(856, 50)
        Me.pnlRow4_GrossWeight.TabIndex = 4
        '
        'lblGrossWeight
        '
        Me.lblGrossWeight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblGrossWeight.AutoSize = True
        Me.lblGrossWeight.Location = New System.Drawing.Point(750, 13)
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
        Me.txtGrossWeight.Size = New System.Drawing.Size(190, 26)
        Me.txtGrossWeight.TabIndex = 1
        '
        'pnlRow5_Statement
        '
        Me.pnlRow5_Statement.Controls.Add(Me.txtStatement)
        Me.pnlRow5_Statement.Controls.Add(Me.lblStatement)
        Me.pnlRow5_Statement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRow5_Statement.Location = New System.Drawing.Point(0, 328)
        Me.pnlRow5_Statement.Name = "pnlRow5_Statement"
        Me.pnlRow5_Statement.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlRow5_Statement.Size = New System.Drawing.Size(856, 202)
        Me.pnlRow5_Statement.TabIndex = 5
        '
        'txtStatement
        '
        Me.txtStatement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtStatement.Location = New System.Drawing.Point(15, 39)
        Me.txtStatement.Multiline = True
        Me.txtStatement.Name = "txtStatement"
        Me.txtStatement.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtStatement.Size = New System.Drawing.Size(826, 148)
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
        'pnlRow6_Buttons
        '
        Me.pnlRow6_Buttons.Controls.Add(Me.btnNew)
        Me.pnlRow6_Buttons.Controls.Add(Me.btnSave)
        Me.pnlRow6_Buttons.Controls.Add(Me.btnPrint)
        Me.pnlRow6_Buttons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlRow6_Buttons.Location = New System.Drawing.Point(0, 530)
        Me.pnlRow6_Buttons.Name = "pnlRow6_Buttons"
        Me.pnlRow6_Buttons.Size = New System.Drawing.Size(856, 70)
        Me.pnlRow6_Buttons.TabIndex = 6
        '
        'btnNew
        '
        Me.btnNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNew.BackColor = System.Drawing.Color.Azure
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnNew.Location = New System.Drawing.Point(691, 10)
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
        Me.btnSave.Location = New System.Drawing.Point(535, 10)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(150, 45)
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
        Me.btnPrint.Location = New System.Drawing.Point(30, 10)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(150, 45)
        Me.btnPrint.TabIndex = 2
        Me.btnPrint.Text = "طباعة"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'RB_Treasury
        '
        Me.RB_Treasury.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RB_Treasury.AutoSize = True
        Me.RB_Treasury.Location = New System.Drawing.Point(255, 14)
        Me.RB_Treasury.Name = "RB_Treasury"
        Me.RB_Treasury.Size = New System.Drawing.Size(58, 23)
        Me.RB_Treasury.TabIndex = 4
        Me.RB_Treasury.Text = "خزائن"
        '
        'RB_Clients
        '
        Me.RB_Clients.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RB_Clients.AutoSize = True
        Me.RB_Clients.Location = New System.Drawing.Point(153, 14)
        Me.RB_Clients.Name = "RB_Clients"
        Me.RB_Clients.Size = New System.Drawing.Size(56, 23)
        Me.RB_Clients.TabIndex = 5
        Me.RB_Clients.Text = "زبائن"
        '
        'frmTransfers
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(856, 600)
        Me.Controls.Add(Me.pnlRow5_Statement)
        Me.Controls.Add(Me.pnlRow6_Buttons)
        Me.Controls.Add(Me.pnlRow4_GrossWeight)
        Me.Controls.Add(Me.pnlRow3_Customers)
        Me.Controls.Add(Me.pnlRow2_Metal)
        Me.Controls.Add(Me.pnlRow1_ID)
        Me.Controls.Add(Me.pnlTopBar)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmTransfers"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "التحويلات"
        Me.pnlTopBar.ResumeLayout(False)
        Me.pnlRow1_ID.ResumeLayout(False)
        Me.pnlRow1_ID.PerformLayout()
        Me.pnlRow2_Metal.ResumeLayout(False)
        Me.pnlRow2_Metal.PerformLayout()
        Me.pnlRow3_Customers.ResumeLayout(False)
        Me.pnlRow3_Customers.PerformLayout()
        Me.pnlRow4_GrossWeight.ResumeLayout(False)
        Me.pnlRow4_GrossWeight.PerformLayout()
        Me.pnlRow5_Statement.ResumeLayout(False)
        Me.pnlRow5_Statement.PerformLayout()
        Me.pnlRow6_Buttons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnCancel As Button
    Friend WithEvents lblfromCustomerInfo As Label
    Friend WithEvents lbltoCustomerInfo As Label
    Friend WithEvents RB_Treasury As RadioButton
    Friend WithEvents RB_Clients As RadioButton
End Class