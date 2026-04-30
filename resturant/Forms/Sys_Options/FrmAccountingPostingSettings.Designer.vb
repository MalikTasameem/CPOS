<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAccountingPostingSettings
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlForm As System.Windows.Forms.Panel
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents pnlGrid As System.Windows.Forms.Panel
    Friend WithEvents pnlStatus As System.Windows.Forms.Panel

    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents tblForm As System.Windows.Forms.TableLayoutPanel

    Friend WithEvents lblSalesAccountCode As System.Windows.Forms.Label
    Friend WithEvents lblSalesReturnAccountCode As System.Windows.Forms.Label
    Friend WithEvents lblInventoryAccountCode As System.Windows.Forms.Label
    Friend WithEvents lblCostOfGoodsSoldAccountCode As System.Windows.Forms.Label
    Friend WithEvents lblPurchaseAccountCode As System.Windows.Forms.Label
    Friend WithEvents lblPurchaseReturnAccountCode As System.Windows.Forms.Label
    Friend WithEvents lblDamageExpenseAccountCode As System.Windows.Forms.Label
    Friend WithEvents lblManufacturingExpenseAccountCode As System.Windows.Forms.Label
    Friend WithEvents lblInventoryAdjustmentGainAccountCode As System.Windows.Forms.Label
    Friend WithEvents lblInventoryAdjustmentLossAccountCode As System.Windows.Forms.Label
    Friend WithEvents lblDiscountAllowedAccountCode As System.Windows.Forms.Label
    Friend WithEvents lblDiscountReceivedAccountCode As System.Windows.Forms.Label
    Friend WithEvents lblGeneralExpenseAccountCode As System.Windows.Forms.Label
    Friend WithEvents lblDefaultCurrencyId As System.Windows.Forms.Label
    Friend WithEvents lblDefaultCurrencyEqual As System.Windows.Forms.Label
    Friend WithEvents lblIsActive As System.Windows.Forms.Label

    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesReturnAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtInventoryAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtCostOfGoodsSoldAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseReturnAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtDamageExpenseAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtManufacturingExpenseAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtInventoryAdjustmentGainAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtInventoryAdjustmentLossAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtDiscountAllowedAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtDiscountReceivedAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtGeneralExpenseAccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtDefaultCurrencyId As System.Windows.Forms.TextBox
    Friend WithEvents txtDefaultCurrencyEqual As System.Windows.Forms.TextBox
    Friend WithEvents chkIsActive As System.Windows.Forms.CheckBox

    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnSetActive As System.Windows.Forms.Button
    Friend WithEvents btnToggleActive As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button

    Friend WithEvents flowButtons As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents dgvSettings As System.Windows.Forms.DataGridView
    Friend WithEvents lblStatus As System.Windows.Forms.Label

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
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlForm = New System.Windows.Forms.Panel()
        Me.tblForm = New System.Windows.Forms.TableLayoutPanel()
        Me.lblSalesAccountCode = New System.Windows.Forms.Label()
        Me.txtSalesAccountCode = New System.Windows.Forms.TextBox()
        Me.lblSalesReturnAccountCode = New System.Windows.Forms.Label()
        Me.txtSalesReturnAccountCode = New System.Windows.Forms.TextBox()
        Me.lblInventoryAccountCode = New System.Windows.Forms.Label()
        Me.txtInventoryAccountCode = New System.Windows.Forms.TextBox()
        Me.lblCostOfGoodsSoldAccountCode = New System.Windows.Forms.Label()
        Me.txtCostOfGoodsSoldAccountCode = New System.Windows.Forms.TextBox()
        Me.lblPurchaseAccountCode = New System.Windows.Forms.Label()
        Me.txtPurchaseAccountCode = New System.Windows.Forms.TextBox()
        Me.lblPurchaseReturnAccountCode = New System.Windows.Forms.Label()
        Me.txtPurchaseReturnAccountCode = New System.Windows.Forms.TextBox()
        Me.lblDamageExpenseAccountCode = New System.Windows.Forms.Label()
        Me.txtDamageExpenseAccountCode = New System.Windows.Forms.TextBox()
        Me.lblManufacturingExpenseAccountCode = New System.Windows.Forms.Label()
        Me.txtManufacturingExpenseAccountCode = New System.Windows.Forms.TextBox()
        Me.lblInventoryAdjustmentGainAccountCode = New System.Windows.Forms.Label()
        Me.txtInventoryAdjustmentGainAccountCode = New System.Windows.Forms.TextBox()
        Me.lblInventoryAdjustmentLossAccountCode = New System.Windows.Forms.Label()
        Me.txtInventoryAdjustmentLossAccountCode = New System.Windows.Forms.TextBox()
        Me.lblDiscountAllowedAccountCode = New System.Windows.Forms.Label()
        Me.txtDiscountAllowedAccountCode = New System.Windows.Forms.TextBox()
        Me.lblDiscountReceivedAccountCode = New System.Windows.Forms.Label()
        Me.txtDiscountReceivedAccountCode = New System.Windows.Forms.TextBox()
        Me.lblGeneralExpenseAccountCode = New System.Windows.Forms.Label()
        Me.txtGeneralExpenseAccountCode = New System.Windows.Forms.TextBox()
        Me.lblDefaultCurrencyId = New System.Windows.Forms.Label()
        Me.txtDefaultCurrencyId = New System.Windows.Forms.TextBox()
        Me.lblDefaultCurrencyEqual = New System.Windows.Forms.Label()
        Me.txtDefaultCurrencyEqual = New System.Windows.Forms.TextBox()
        Me.lblIsActive = New System.Windows.Forms.Label()
        Me.chkIsActive = New System.Windows.Forms.CheckBox()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.flowButtons = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnSetActive = New System.Windows.Forms.Button()
        Me.btnToggleActive = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.dgvSettings = New System.Windows.Forms.DataGridView()
        Me.pnlStatus = New System.Windows.Forms.Panel()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pnlTop.SuspendLayout()
        Me.pnlForm.SuspendLayout()
        Me.tblForm.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.flowButtons.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me.dgvSettings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.White
        Me.pnlTop.Controls.Add(Me.lblTitle)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlTop.Size = New System.Drawing.Size(1280, 55)
        Me.pnlTop.TabIndex = 3
        '
        'lblTitle
        '
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(10, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(1260, 35)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "إدارة إعدادات الربط المحاسبي"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlForm
        '
        Me.pnlForm.BackColor = System.Drawing.Color.White
        Me.pnlForm.Controls.Add(Me.tblForm)
        Me.pnlForm.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlForm.Location = New System.Drawing.Point(0, 55)
        Me.pnlForm.Name = "pnlForm"
        Me.pnlForm.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlForm.Size = New System.Drawing.Size(1280, 245)
        Me.pnlForm.TabIndex = 2
        '
        'tblForm
        '
        Me.tblForm.BackColor = System.Drawing.Color.White
        Me.tblForm.ColumnCount = 6
        Me.tblForm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.0!))
        Me.tblForm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.tblForm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.0!))
        Me.tblForm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.tblForm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.0!))
        Me.tblForm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.0!))
        Me.tblForm.Controls.Add(Me.lblSalesAccountCode, 0, 0)
        Me.tblForm.Controls.Add(Me.txtSalesAccountCode, 1, 0)
        Me.tblForm.Controls.Add(Me.lblSalesReturnAccountCode, 2, 0)
        Me.tblForm.Controls.Add(Me.txtSalesReturnAccountCode, 3, 0)
        Me.tblForm.Controls.Add(Me.lblInventoryAccountCode, 4, 0)
        Me.tblForm.Controls.Add(Me.txtInventoryAccountCode, 5, 0)
        Me.tblForm.Controls.Add(Me.lblCostOfGoodsSoldAccountCode, 0, 1)
        Me.tblForm.Controls.Add(Me.txtCostOfGoodsSoldAccountCode, 1, 1)
        Me.tblForm.Controls.Add(Me.lblPurchaseAccountCode, 2, 1)
        Me.tblForm.Controls.Add(Me.txtPurchaseAccountCode, 3, 1)
        Me.tblForm.Controls.Add(Me.lblPurchaseReturnAccountCode, 4, 1)
        Me.tblForm.Controls.Add(Me.txtPurchaseReturnAccountCode, 5, 1)
        Me.tblForm.Controls.Add(Me.lblDamageExpenseAccountCode, 0, 2)
        Me.tblForm.Controls.Add(Me.txtDamageExpenseAccountCode, 1, 2)
        Me.tblForm.Controls.Add(Me.lblManufacturingExpenseAccountCode, 2, 2)
        Me.tblForm.Controls.Add(Me.txtManufacturingExpenseAccountCode, 3, 2)
        Me.tblForm.Controls.Add(Me.lblInventoryAdjustmentGainAccountCode, 4, 2)
        Me.tblForm.Controls.Add(Me.txtInventoryAdjustmentGainAccountCode, 5, 2)
        Me.tblForm.Controls.Add(Me.lblInventoryAdjustmentLossAccountCode, 0, 3)
        Me.tblForm.Controls.Add(Me.txtInventoryAdjustmentLossAccountCode, 1, 3)
        Me.tblForm.Controls.Add(Me.lblDiscountAllowedAccountCode, 2, 3)
        Me.tblForm.Controls.Add(Me.txtDiscountAllowedAccountCode, 3, 3)
        Me.tblForm.Controls.Add(Me.lblDiscountReceivedAccountCode, 4, 3)
        Me.tblForm.Controls.Add(Me.txtDiscountReceivedAccountCode, 5, 3)
        Me.tblForm.Controls.Add(Me.lblGeneralExpenseAccountCode, 0, 4)
        Me.tblForm.Controls.Add(Me.txtGeneralExpenseAccountCode, 1, 4)
        Me.tblForm.Controls.Add(Me.lblDefaultCurrencyId, 2, 4)
        Me.tblForm.Controls.Add(Me.txtDefaultCurrencyId, 3, 4)
        Me.tblForm.Controls.Add(Me.lblDefaultCurrencyEqual, 4, 4)
        Me.tblForm.Controls.Add(Me.txtDefaultCurrencyEqual, 5, 4)
        Me.tblForm.Controls.Add(Me.lblIsActive, 0, 5)
        Me.tblForm.Controls.Add(Me.chkIsActive, 1, 5)
        Me.tblForm.Controls.Add(Me.txtId, 5, 5)
        Me.tblForm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblForm.Location = New System.Drawing.Point(10, 10)
        Me.tblForm.Name = "tblForm"
        Me.tblForm.Padding = New System.Windows.Forms.Padding(5)
        Me.tblForm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.tblForm.RowCount = 6
        Me.tblForm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66!))
        Me.tblForm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66!))
        Me.tblForm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66!))
        Me.tblForm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66!))
        Me.tblForm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66!))
        Me.tblForm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66!))
        Me.tblForm.Size = New System.Drawing.Size(1260, 225)
        Me.tblForm.TabIndex = 0
        '
        'lblSalesAccountCode
        '
        Me.lblSalesAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSalesAccountCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblSalesAccountCode.Location = New System.Drawing.Point(1096, 5)
        Me.lblSalesAccountCode.Name = "lblSalesAccountCode"
        Me.lblSalesAccountCode.Size = New System.Drawing.Size(156, 35)
        Me.lblSalesAccountCode.TabIndex = 0
        Me.lblSalesAccountCode.Text = "حساب المبيعات"
        Me.lblSalesAccountCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSalesAccountCode
        '
        Me.txtSalesAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSalesAccountCode.Location = New System.Drawing.Point(846, 11)
        Me.txtSalesAccountCode.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtSalesAccountCode.Name = "txtSalesAccountCode"
        Me.txtSalesAccountCode.Size = New System.Drawing.Size(244, 22)
        Me.txtSalesAccountCode.TabIndex = 1
        '
        'lblSalesReturnAccountCode
        '
        Me.lblSalesReturnAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSalesReturnAccountCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblSalesReturnAccountCode.Location = New System.Drawing.Point(684, 5)
        Me.lblSalesReturnAccountCode.Name = "lblSalesReturnAccountCode"
        Me.lblSalesReturnAccountCode.Size = New System.Drawing.Size(156, 35)
        Me.lblSalesReturnAccountCode.TabIndex = 2
        Me.lblSalesReturnAccountCode.Text = "مردودات المبيعات"
        Me.lblSalesReturnAccountCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSalesReturnAccountCode
        '
        Me.txtSalesReturnAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSalesReturnAccountCode.Location = New System.Drawing.Point(434, 11)
        Me.txtSalesReturnAccountCode.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtSalesReturnAccountCode.Name = "txtSalesReturnAccountCode"
        Me.txtSalesReturnAccountCode.Size = New System.Drawing.Size(244, 22)
        Me.txtSalesReturnAccountCode.TabIndex = 3
        '
        'lblInventoryAccountCode
        '
        Me.lblInventoryAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblInventoryAccountCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblInventoryAccountCode.Location = New System.Drawing.Point(272, 5)
        Me.lblInventoryAccountCode.Name = "lblInventoryAccountCode"
        Me.lblInventoryAccountCode.Size = New System.Drawing.Size(156, 35)
        Me.lblInventoryAccountCode.TabIndex = 4
        Me.lblInventoryAccountCode.Text = "حساب المخزون"
        Me.lblInventoryAccountCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInventoryAccountCode
        '
        Me.txtInventoryAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtInventoryAccountCode.Location = New System.Drawing.Point(8, 11)
        Me.txtInventoryAccountCode.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtInventoryAccountCode.Name = "txtInventoryAccountCode"
        Me.txtInventoryAccountCode.Size = New System.Drawing.Size(258, 22)
        Me.txtInventoryAccountCode.TabIndex = 5
        '
        'lblCostOfGoodsSoldAccountCode
        '
        Me.lblCostOfGoodsSoldAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCostOfGoodsSoldAccountCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblCostOfGoodsSoldAccountCode.Location = New System.Drawing.Point(1096, 40)
        Me.lblCostOfGoodsSoldAccountCode.Name = "lblCostOfGoodsSoldAccountCode"
        Me.lblCostOfGoodsSoldAccountCode.Size = New System.Drawing.Size(156, 35)
        Me.lblCostOfGoodsSoldAccountCode.TabIndex = 6
        Me.lblCostOfGoodsSoldAccountCode.Text = "تكلفة المبيعات"
        Me.lblCostOfGoodsSoldAccountCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCostOfGoodsSoldAccountCode
        '
        Me.txtCostOfGoodsSoldAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCostOfGoodsSoldAccountCode.Location = New System.Drawing.Point(846, 46)
        Me.txtCostOfGoodsSoldAccountCode.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtCostOfGoodsSoldAccountCode.Name = "txtCostOfGoodsSoldAccountCode"
        Me.txtCostOfGoodsSoldAccountCode.Size = New System.Drawing.Size(244, 22)
        Me.txtCostOfGoodsSoldAccountCode.TabIndex = 7
        '
        'lblPurchaseAccountCode
        '
        Me.lblPurchaseAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPurchaseAccountCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblPurchaseAccountCode.Location = New System.Drawing.Point(684, 40)
        Me.lblPurchaseAccountCode.Name = "lblPurchaseAccountCode"
        Me.lblPurchaseAccountCode.Size = New System.Drawing.Size(156, 35)
        Me.lblPurchaseAccountCode.TabIndex = 8
        Me.lblPurchaseAccountCode.Text = "حساب المشتريات"
        Me.lblPurchaseAccountCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPurchaseAccountCode
        '
        Me.txtPurchaseAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPurchaseAccountCode.Location = New System.Drawing.Point(434, 46)
        Me.txtPurchaseAccountCode.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtPurchaseAccountCode.Name = "txtPurchaseAccountCode"
        Me.txtPurchaseAccountCode.Size = New System.Drawing.Size(244, 22)
        Me.txtPurchaseAccountCode.TabIndex = 9
        '
        'lblPurchaseReturnAccountCode
        '
        Me.lblPurchaseReturnAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPurchaseReturnAccountCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblPurchaseReturnAccountCode.Location = New System.Drawing.Point(272, 40)
        Me.lblPurchaseReturnAccountCode.Name = "lblPurchaseReturnAccountCode"
        Me.lblPurchaseReturnAccountCode.Size = New System.Drawing.Size(156, 35)
        Me.lblPurchaseReturnAccountCode.TabIndex = 10
        Me.lblPurchaseReturnAccountCode.Text = "مردودات المشتريات"
        Me.lblPurchaseReturnAccountCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPurchaseReturnAccountCode
        '
        Me.txtPurchaseReturnAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPurchaseReturnAccountCode.Location = New System.Drawing.Point(8, 46)
        Me.txtPurchaseReturnAccountCode.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtPurchaseReturnAccountCode.Name = "txtPurchaseReturnAccountCode"
        Me.txtPurchaseReturnAccountCode.Size = New System.Drawing.Size(258, 22)
        Me.txtPurchaseReturnAccountCode.TabIndex = 11
        '
        'lblDamageExpenseAccountCode
        '
        Me.lblDamageExpenseAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDamageExpenseAccountCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblDamageExpenseAccountCode.Location = New System.Drawing.Point(1096, 75)
        Me.lblDamageExpenseAccountCode.Name = "lblDamageExpenseAccountCode"
        Me.lblDamageExpenseAccountCode.Size = New System.Drawing.Size(156, 35)
        Me.lblDamageExpenseAccountCode.TabIndex = 12
        Me.lblDamageExpenseAccountCode.Text = "مصروف التالف"
        Me.lblDamageExpenseAccountCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDamageExpenseAccountCode
        '
        Me.txtDamageExpenseAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDamageExpenseAccountCode.Location = New System.Drawing.Point(846, 81)
        Me.txtDamageExpenseAccountCode.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtDamageExpenseAccountCode.Name = "txtDamageExpenseAccountCode"
        Me.txtDamageExpenseAccountCode.Size = New System.Drawing.Size(244, 22)
        Me.txtDamageExpenseAccountCode.TabIndex = 13
        '
        'lblManufacturingExpenseAccountCode
        '
        Me.lblManufacturingExpenseAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblManufacturingExpenseAccountCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblManufacturingExpenseAccountCode.Location = New System.Drawing.Point(684, 75)
        Me.lblManufacturingExpenseAccountCode.Name = "lblManufacturingExpenseAccountCode"
        Me.lblManufacturingExpenseAccountCode.Size = New System.Drawing.Size(156, 35)
        Me.lblManufacturingExpenseAccountCode.TabIndex = 14
        Me.lblManufacturingExpenseAccountCode.Text = "تكلفة التصنيع"
        Me.lblManufacturingExpenseAccountCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtManufacturingExpenseAccountCode
        '
        Me.txtManufacturingExpenseAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtManufacturingExpenseAccountCode.Location = New System.Drawing.Point(434, 81)
        Me.txtManufacturingExpenseAccountCode.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtManufacturingExpenseAccountCode.Name = "txtManufacturingExpenseAccountCode"
        Me.txtManufacturingExpenseAccountCode.Size = New System.Drawing.Size(244, 22)
        Me.txtManufacturingExpenseAccountCode.TabIndex = 15
        '
        'lblInventoryAdjustmentGainAccountCode
        '
        Me.lblInventoryAdjustmentGainAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblInventoryAdjustmentGainAccountCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblInventoryAdjustmentGainAccountCode.Location = New System.Drawing.Point(272, 75)
        Me.lblInventoryAdjustmentGainAccountCode.Name = "lblInventoryAdjustmentGainAccountCode"
        Me.lblInventoryAdjustmentGainAccountCode.Size = New System.Drawing.Size(156, 35)
        Me.lblInventoryAdjustmentGainAccountCode.TabIndex = 16
        Me.lblInventoryAdjustmentGainAccountCode.Text = "أرباح التسوية"
        Me.lblInventoryAdjustmentGainAccountCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInventoryAdjustmentGainAccountCode
        '
        Me.txtInventoryAdjustmentGainAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtInventoryAdjustmentGainAccountCode.Location = New System.Drawing.Point(8, 81)
        Me.txtInventoryAdjustmentGainAccountCode.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtInventoryAdjustmentGainAccountCode.Name = "txtInventoryAdjustmentGainAccountCode"
        Me.txtInventoryAdjustmentGainAccountCode.Size = New System.Drawing.Size(258, 22)
        Me.txtInventoryAdjustmentGainAccountCode.TabIndex = 17
        '
        'lblInventoryAdjustmentLossAccountCode
        '
        Me.lblInventoryAdjustmentLossAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblInventoryAdjustmentLossAccountCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblInventoryAdjustmentLossAccountCode.Location = New System.Drawing.Point(1096, 110)
        Me.lblInventoryAdjustmentLossAccountCode.Name = "lblInventoryAdjustmentLossAccountCode"
        Me.lblInventoryAdjustmentLossAccountCode.Size = New System.Drawing.Size(156, 35)
        Me.lblInventoryAdjustmentLossAccountCode.TabIndex = 18
        Me.lblInventoryAdjustmentLossAccountCode.Text = "خسائر التسوية"
        Me.lblInventoryAdjustmentLossAccountCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInventoryAdjustmentLossAccountCode
        '
        Me.txtInventoryAdjustmentLossAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtInventoryAdjustmentLossAccountCode.Location = New System.Drawing.Point(846, 116)
        Me.txtInventoryAdjustmentLossAccountCode.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtInventoryAdjustmentLossAccountCode.Name = "txtInventoryAdjustmentLossAccountCode"
        Me.txtInventoryAdjustmentLossAccountCode.Size = New System.Drawing.Size(244, 22)
        Me.txtInventoryAdjustmentLossAccountCode.TabIndex = 19
        '
        'lblDiscountAllowedAccountCode
        '
        Me.lblDiscountAllowedAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDiscountAllowedAccountCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblDiscountAllowedAccountCode.Location = New System.Drawing.Point(684, 110)
        Me.lblDiscountAllowedAccountCode.Name = "lblDiscountAllowedAccountCode"
        Me.lblDiscountAllowedAccountCode.Size = New System.Drawing.Size(156, 35)
        Me.lblDiscountAllowedAccountCode.TabIndex = 20
        Me.lblDiscountAllowedAccountCode.Text = "الخصم المسموح"
        Me.lblDiscountAllowedAccountCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDiscountAllowedAccountCode
        '
        Me.txtDiscountAllowedAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDiscountAllowedAccountCode.Location = New System.Drawing.Point(434, 116)
        Me.txtDiscountAllowedAccountCode.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtDiscountAllowedAccountCode.Name = "txtDiscountAllowedAccountCode"
        Me.txtDiscountAllowedAccountCode.Size = New System.Drawing.Size(244, 22)
        Me.txtDiscountAllowedAccountCode.TabIndex = 21
        '
        'lblDiscountReceivedAccountCode
        '
        Me.lblDiscountReceivedAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDiscountReceivedAccountCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblDiscountReceivedAccountCode.Location = New System.Drawing.Point(272, 110)
        Me.lblDiscountReceivedAccountCode.Name = "lblDiscountReceivedAccountCode"
        Me.lblDiscountReceivedAccountCode.Size = New System.Drawing.Size(156, 35)
        Me.lblDiscountReceivedAccountCode.TabIndex = 22
        Me.lblDiscountReceivedAccountCode.Text = "الخصم المكتسب"
        Me.lblDiscountReceivedAccountCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDiscountReceivedAccountCode
        '
        Me.txtDiscountReceivedAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDiscountReceivedAccountCode.Location = New System.Drawing.Point(8, 116)
        Me.txtDiscountReceivedAccountCode.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtDiscountReceivedAccountCode.Name = "txtDiscountReceivedAccountCode"
        Me.txtDiscountReceivedAccountCode.Size = New System.Drawing.Size(258, 22)
        Me.txtDiscountReceivedAccountCode.TabIndex = 23
        '
        'lblGeneralExpenseAccountCode
        '
        Me.lblGeneralExpenseAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblGeneralExpenseAccountCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblGeneralExpenseAccountCode.Location = New System.Drawing.Point(1096, 145)
        Me.lblGeneralExpenseAccountCode.Name = "lblGeneralExpenseAccountCode"
        Me.lblGeneralExpenseAccountCode.Size = New System.Drawing.Size(156, 35)
        Me.lblGeneralExpenseAccountCode.TabIndex = 24
        Me.lblGeneralExpenseAccountCode.Text = "المصروف العام"
        Me.lblGeneralExpenseAccountCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtGeneralExpenseAccountCode
        '
        Me.txtGeneralExpenseAccountCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtGeneralExpenseAccountCode.Location = New System.Drawing.Point(846, 151)
        Me.txtGeneralExpenseAccountCode.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtGeneralExpenseAccountCode.Name = "txtGeneralExpenseAccountCode"
        Me.txtGeneralExpenseAccountCode.Size = New System.Drawing.Size(244, 22)
        Me.txtGeneralExpenseAccountCode.TabIndex = 25
        '
        'lblDefaultCurrencyId
        '
        Me.lblDefaultCurrencyId.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDefaultCurrencyId.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblDefaultCurrencyId.Location = New System.Drawing.Point(684, 145)
        Me.lblDefaultCurrencyId.Name = "lblDefaultCurrencyId"
        Me.lblDefaultCurrencyId.Size = New System.Drawing.Size(156, 35)
        Me.lblDefaultCurrencyId.TabIndex = 26
        Me.lblDefaultCurrencyId.Text = "رقم العملة"
        Me.lblDefaultCurrencyId.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDefaultCurrencyId
        '
        Me.txtDefaultCurrencyId.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDefaultCurrencyId.Location = New System.Drawing.Point(434, 151)
        Me.txtDefaultCurrencyId.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtDefaultCurrencyId.Name = "txtDefaultCurrencyId"
        Me.txtDefaultCurrencyId.Size = New System.Drawing.Size(244, 22)
        Me.txtDefaultCurrencyId.TabIndex = 27
        '
        'lblDefaultCurrencyEqual
        '
        Me.lblDefaultCurrencyEqual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDefaultCurrencyEqual.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblDefaultCurrencyEqual.Location = New System.Drawing.Point(272, 145)
        Me.lblDefaultCurrencyEqual.Name = "lblDefaultCurrencyEqual"
        Me.lblDefaultCurrencyEqual.Size = New System.Drawing.Size(156, 35)
        Me.lblDefaultCurrencyEqual.TabIndex = 28
        Me.lblDefaultCurrencyEqual.Text = "معادل العملة"
        Me.lblDefaultCurrencyEqual.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDefaultCurrencyEqual
        '
        Me.txtDefaultCurrencyEqual.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDefaultCurrencyEqual.Location = New System.Drawing.Point(8, 151)
        Me.txtDefaultCurrencyEqual.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.txtDefaultCurrencyEqual.Name = "txtDefaultCurrencyEqual"
        Me.txtDefaultCurrencyEqual.Size = New System.Drawing.Size(258, 22)
        Me.txtDefaultCurrencyEqual.TabIndex = 29
        '
        'lblIsActive
        '
        Me.lblIsActive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblIsActive.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.lblIsActive.Location = New System.Drawing.Point(1096, 180)
        Me.lblIsActive.Name = "lblIsActive"
        Me.lblIsActive.Size = New System.Drawing.Size(156, 40)
        Me.lblIsActive.TabIndex = 30
        Me.lblIsActive.Text = "فعال"
        Me.lblIsActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkIsActive
        '
        Me.chkIsActive.Checked = True
        Me.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIsActive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chkIsActive.Location = New System.Drawing.Point(846, 186)
        Me.chkIsActive.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.chkIsActive.Name = "chkIsActive"
        Me.chkIsActive.Size = New System.Drawing.Size(244, 31)
        Me.chkIsActive.TabIndex = 31
        Me.chkIsActive.Text = "الإعداد فعال"
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(166, 183)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(100, 22)
        Me.txtId.TabIndex = 32
        Me.txtId.Visible = False
        '
        'pnlButtons
        '
        Me.pnlButtons.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlButtons.Controls.Add(Me.flowButtons)
        Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlButtons.Location = New System.Drawing.Point(0, 300)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlButtons.Size = New System.Drawing.Size(1280, 60)
        Me.pnlButtons.TabIndex = 1
        '
        'flowButtons
        '
        Me.flowButtons.Controls.Add(Me.btnNew)
        Me.flowButtons.Controls.Add(Me.btnSave)
        Me.flowButtons.Controls.Add(Me.btnUpdate)
        Me.flowButtons.Controls.Add(Me.btnSetActive)
        Me.flowButtons.Controls.Add(Me.btnToggleActive)
        Me.flowButtons.Controls.Add(Me.btnRefresh)
        Me.flowButtons.Controls.Add(Me.btnClose)
        Me.flowButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.flowButtons.Location = New System.Drawing.Point(10, 10)
        Me.flowButtons.Name = "flowButtons"
        Me.flowButtons.Size = New System.Drawing.Size(1260, 40)
        Me.flowButtons.TabIndex = 0
        Me.flowButtons.WrapContents = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.btnNew.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNew.FlatAppearance.BorderSize = 0
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnNew.ForeColor = System.Drawing.Color.White
        Me.btnNew.Location = New System.Drawing.Point(5, 5)
        Me.btnNew.Margin = New System.Windows.Forms.Padding(5)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(110, 34)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "جديد"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(125, 5)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(110, 34)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "حفظ"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnUpdate
        '
        Me.btnUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(156, Byte), Integer), CType(CType(18, Byte), Integer))
        Me.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnUpdate.FlatAppearance.BorderSize = 0
        Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnUpdate.ForeColor = System.Drawing.Color.White
        Me.btnUpdate.Location = New System.Drawing.Point(245, 5)
        Me.btnUpdate.Margin = New System.Windows.Forms.Padding(5)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(110, 34)
        Me.btnUpdate.TabIndex = 2
        Me.btnUpdate.Text = "تعديل"
        Me.btnUpdate.UseVisualStyleBackColor = False
        '
        'btnSetActive
        '
        Me.btnSetActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.btnSetActive.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSetActive.FlatAppearance.BorderSize = 0
        Me.btnSetActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSetActive.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSetActive.ForeColor = System.Drawing.Color.White
        Me.btnSetActive.Location = New System.Drawing.Point(365, 5)
        Me.btnSetActive.Margin = New System.Windows.Forms.Padding(5)
        Me.btnSetActive.Name = "btnSetActive"
        Me.btnSetActive.Size = New System.Drawing.Size(170, 34)
        Me.btnSetActive.TabIndex = 3
        Me.btnSetActive.Text = "اعتماد كإعداد فعال"
        Me.btnSetActive.UseVisualStyleBackColor = False
        '
        'btnToggleActive
        '
        Me.btnToggleActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(84, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnToggleActive.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnToggleActive.FlatAppearance.BorderSize = 0
        Me.btnToggleActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnToggleActive.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnToggleActive.ForeColor = System.Drawing.Color.White
        Me.btnToggleActive.Location = New System.Drawing.Point(545, 5)
        Me.btnToggleActive.Margin = New System.Windows.Forms.Padding(5)
        Me.btnToggleActive.Name = "btnToggleActive"
        Me.btnToggleActive.Size = New System.Drawing.Size(140, 34)
        Me.btnToggleActive.TabIndex = 4
        Me.btnToggleActive.Text = "تفعيل / تعطيل"
        Me.btnToggleActive.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(695, 5)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(5)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(110, 34)
        Me.btnRefresh.TabIndex = 5
        Me.btnRefresh.Text = "تحديث"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(815, 5)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(110, 34)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "إغلاق"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'pnlGrid
        '
        Me.pnlGrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlGrid.Controls.Add(Me.dgvSettings)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(0, 360)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlGrid.Size = New System.Drawing.Size(1280, 328)
        Me.pnlGrid.TabIndex = 0
        '
        'dgvSettings
        '
        Me.dgvSettings.AllowUserToAddRows = False
        Me.dgvSettings.AllowUserToDeleteRows = False
        Me.dgvSettings.BackgroundColor = System.Drawing.Color.White
        Me.dgvSettings.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSettings.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSettings.ColumnHeadersHeight = 35
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSettings.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSettings.EnableHeadersVisualStyles = False
        Me.dgvSettings.Location = New System.Drawing.Point(10, 10)
        Me.dgvSettings.MultiSelect = False
        Me.dgvSettings.Name = "dgvSettings"
        Me.dgvSettings.ReadOnly = True
        Me.dgvSettings.RowHeadersVisible = False
        Me.dgvSettings.RowTemplate.Height = 30
        Me.dgvSettings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSettings.Size = New System.Drawing.Size(1260, 308)
        Me.dgvSettings.TabIndex = 0
        '
        'pnlStatus
        '
        Me.pnlStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.pnlStatus.Controls.Add(Me.lblStatus)
        Me.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlStatus.Location = New System.Drawing.Point(0, 688)
        Me.pnlStatus.Name = "pnlStatus"
        Me.pnlStatus.Size = New System.Drawing.Size(1280, 32)
        Me.pnlStatus.TabIndex = 4
        '
        'lblStatus
        '
        Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatus.ForeColor = System.Drawing.Color.White
        Me.lblStatus.Location = New System.Drawing.Point(0, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.lblStatus.Size = New System.Drawing.Size(1280, 32)
        Me.lblStatus.TabIndex = 0
        Me.lblStatus.Text = "جاهز"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmAccountingPostingSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1280, 720)
        Me.Controls.Add(Me.pnlGrid)
        Me.Controls.Add(Me.pnlButtons)
        Me.Controls.Add(Me.pnlForm)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlStatus)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Name = "FrmAccountingPostingSettings"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إدارة إعدادات الربط المحاسبي"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTop.ResumeLayout(False)
        Me.pnlForm.ResumeLayout(False)
        Me.tblForm.ResumeLayout(False)
        Me.tblForm.PerformLayout()
        Me.pnlButtons.ResumeLayout(False)
        Me.flowButtons.ResumeLayout(False)
        Me.pnlGrid.ResumeLayout(False)
        CType(Me.dgvSettings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlStatus.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

End Class