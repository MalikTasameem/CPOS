<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmExchangeDetails
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmExchangeDetails))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.grpCustomer = New System.Windows.Forms.GroupBox()
        Me.tblCustomer = New System.Windows.Forms.TableLayoutPanel()
        Me.lblExchangeId = New System.Windows.Forms.Label()
        Me.txtExchangeId = New System.Windows.Forms.TextBox()
        Me.lblCustomerName = New System.Windows.Forms.Label()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.lblCustomerIdentity = New System.Windows.Forms.Label()
        Me.txtCustomerIdentity = New System.Windows.Forms.TextBox()
        Me.lblReferenceNo = New System.Windows.Forms.Label()
        Me.txtReferenceNo = New System.Windows.Forms.TextBox()
        Me.lblCreatedAt = New System.Windows.Forms.Label()
        Me.txtCreatedAt = New System.Windows.Forms.TextBox()
        Me.grpOperation = New System.Windows.Forms.GroupBox()
        Me.tblOperation = New System.Windows.Forms.TableLayoutPanel()
        Me.lblOperationType = New System.Windows.Forms.Label()
        Me.txtOperationType = New System.Windows.Forms.TextBox()
        Me.lblVault = New System.Windows.Forms.Label()
        Me.txtVault = New System.Windows.Forms.TextBox()
        Me.lblCurrency = New System.Windows.Forms.Label()
        Me.txtCurrency = New System.Windows.Forms.TextBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.grpFinance = New System.Windows.Forms.GroupBox()
        Me.tblFinance = New System.Windows.Forms.TableLayoutPanel()
        Me.lblForeignAmount = New System.Windows.Forms.Label()
        Me.txtForeignAmount = New System.Windows.Forms.TextBox()
        Me.lblRate = New System.Windows.Forms.Label()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.lblCommissionPercent = New System.Windows.Forms.Label()
        Me.txtCommissionPercent = New System.Windows.Forms.TextBox()
        Me.lblCommissionLYD = New System.Windows.Forms.Label()
        Me.txtCommissionLYD = New System.Windows.Forms.TextBox()
        Me.lblTotalLYD = New System.Windows.Forms.Label()
        Me.txtTotalLYD = New System.Windows.Forms.TextBox()
        Me.lblNetLYD = New System.Windows.Forms.Label()
        Me.txtNetLYD = New System.Windows.Forms.TextBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.ADD_Doc_btn = New System.Windows.Forms.Button()
        Me.DocGridView = New System.Windows.Forms.DataGridView()
        Me.grpCustomer.SuspendLayout()
        Me.tblCustomer.SuspendLayout()
        Me.grpOperation.SuspendLayout()
        Me.tblOperation.SuspendLayout()
        Me.grpFinance.SuspendLayout()
        Me.tblFinance.SuspendLayout()
        CType(Me.DocGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpCustomer
        '
        Me.grpCustomer.Controls.Add(Me.tblCustomer)
        Me.grpCustomer.Location = New System.Drawing.Point(80, -1)
        Me.grpCustomer.Name = "grpCustomer"
        Me.grpCustomer.Size = New System.Drawing.Size(867, 180)
        Me.grpCustomer.TabIndex = 0
        Me.grpCustomer.TabStop = False
        Me.grpCustomer.Text = "بيانات العميل"
        '
        'tblCustomer
        '
        Me.tblCustomer.ColumnCount = 2
        Me.tblCustomer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.tblCustomer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.tblCustomer.Controls.Add(Me.lblExchangeId, 0, 0)
        Me.tblCustomer.Controls.Add(Me.txtExchangeId, 1, 0)
        Me.tblCustomer.Controls.Add(Me.lblCustomerName, 0, 1)
        Me.tblCustomer.Controls.Add(Me.txtCustomerName, 1, 1)
        Me.tblCustomer.Controls.Add(Me.lblCustomerIdentity, 0, 2)
        Me.tblCustomer.Controls.Add(Me.txtCustomerIdentity, 1, 2)
        Me.tblCustomer.Controls.Add(Me.lblReferenceNo, 0, 3)
        Me.tblCustomer.Controls.Add(Me.txtReferenceNo, 1, 3)
        Me.tblCustomer.Controls.Add(Me.lblCreatedAt, 0, 4)
        Me.tblCustomer.Controls.Add(Me.txtCreatedAt, 1, 4)
        Me.tblCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblCustomer.Location = New System.Drawing.Point(3, 19)
        Me.tblCustomer.Name = "tblCustomer"
        Me.tblCustomer.RowCount = 5
        Me.tblCustomer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tblCustomer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tblCustomer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tblCustomer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tblCustomer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tblCustomer.Size = New System.Drawing.Size(861, 158)
        Me.tblCustomer.TabIndex = 0
        '
        'lblExchangeId
        '
        Me.lblExchangeId.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblExchangeId.Location = New System.Drawing.Point(606, 0)
        Me.lblExchangeId.Name = "lblExchangeId"
        Me.lblExchangeId.Size = New System.Drawing.Size(252, 32)
        Me.lblExchangeId.TabIndex = 0
        Me.lblExchangeId.Text = "رقم العملية"
        Me.lblExchangeId.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtExchangeId
        '
        Me.txtExchangeId.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtExchangeId.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExchangeId.Location = New System.Drawing.Point(3, 3)
        Me.txtExchangeId.Name = "txtExchangeId"
        Me.txtExchangeId.ReadOnly = True
        Me.txtExchangeId.Size = New System.Drawing.Size(597, 23)
        Me.txtExchangeId.TabIndex = 1
        '
        'lblCustomerName
        '
        Me.lblCustomerName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCustomerName.Location = New System.Drawing.Point(606, 32)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(252, 32)
        Me.lblCustomerName.TabIndex = 2
        Me.lblCustomerName.Text = "اسم العميل"
        Me.lblCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCustomerName
        '
        Me.txtCustomerName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCustomerName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.Location = New System.Drawing.Point(3, 35)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.ReadOnly = True
        Me.txtCustomerName.Size = New System.Drawing.Size(597, 23)
        Me.txtCustomerName.TabIndex = 3
        '
        'lblCustomerIdentity
        '
        Me.lblCustomerIdentity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCustomerIdentity.Location = New System.Drawing.Point(606, 64)
        Me.lblCustomerIdentity.Name = "lblCustomerIdentity"
        Me.lblCustomerIdentity.Size = New System.Drawing.Size(252, 32)
        Me.lblCustomerIdentity.TabIndex = 4
        Me.lblCustomerIdentity.Text = "رقم الهوية"
        Me.lblCustomerIdentity.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCustomerIdentity
        '
        Me.txtCustomerIdentity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCustomerIdentity.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerIdentity.Location = New System.Drawing.Point(3, 67)
        Me.txtCustomerIdentity.Name = "txtCustomerIdentity"
        Me.txtCustomerIdentity.ReadOnly = True
        Me.txtCustomerIdentity.Size = New System.Drawing.Size(597, 23)
        Me.txtCustomerIdentity.TabIndex = 5
        '
        'lblReferenceNo
        '
        Me.lblReferenceNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblReferenceNo.Location = New System.Drawing.Point(606, 96)
        Me.lblReferenceNo.Name = "lblReferenceNo"
        Me.lblReferenceNo.Size = New System.Drawing.Size(252, 32)
        Me.lblReferenceNo.TabIndex = 6
        Me.lblReferenceNo.Text = "رقم مرجعي"
        Me.lblReferenceNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtReferenceNo
        '
        Me.txtReferenceNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtReferenceNo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReferenceNo.Location = New System.Drawing.Point(3, 99)
        Me.txtReferenceNo.Name = "txtReferenceNo"
        Me.txtReferenceNo.ReadOnly = True
        Me.txtReferenceNo.Size = New System.Drawing.Size(597, 23)
        Me.txtReferenceNo.TabIndex = 7
        '
        'lblCreatedAt
        '
        Me.lblCreatedAt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCreatedAt.Location = New System.Drawing.Point(606, 128)
        Me.lblCreatedAt.Name = "lblCreatedAt"
        Me.lblCreatedAt.Size = New System.Drawing.Size(252, 32)
        Me.lblCreatedAt.TabIndex = 8
        Me.lblCreatedAt.Text = "تاريخ الإنشاء"
        Me.lblCreatedAt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCreatedAt
        '
        Me.txtCreatedAt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCreatedAt.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreatedAt.Location = New System.Drawing.Point(3, 131)
        Me.txtCreatedAt.Name = "txtCreatedAt"
        Me.txtCreatedAt.ReadOnly = True
        Me.txtCreatedAt.Size = New System.Drawing.Size(597, 23)
        Me.txtCreatedAt.TabIndex = 9
        '
        'grpOperation
        '
        Me.grpOperation.Controls.Add(Me.tblOperation)
        Me.grpOperation.Location = New System.Drawing.Point(80, 176)
        Me.grpOperation.Name = "grpOperation"
        Me.grpOperation.Size = New System.Drawing.Size(867, 150)
        Me.grpOperation.TabIndex = 1
        Me.grpOperation.TabStop = False
        Me.grpOperation.Text = "تفاصيل العملية"
        '
        'tblOperation
        '
        Me.tblOperation.ColumnCount = 2
        Me.tblOperation.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.tblOperation.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.tblOperation.Controls.Add(Me.lblOperationType, 0, 0)
        Me.tblOperation.Controls.Add(Me.txtOperationType, 1, 0)
        Me.tblOperation.Controls.Add(Me.lblVault, 0, 1)
        Me.tblOperation.Controls.Add(Me.txtVault, 1, 1)
        Me.tblOperation.Controls.Add(Me.lblCurrency, 0, 2)
        Me.tblOperation.Controls.Add(Me.txtCurrency, 1, 2)
        Me.tblOperation.Controls.Add(Me.lblStatus, 0, 3)
        Me.tblOperation.Controls.Add(Me.txtStatus, 1, 3)
        Me.tblOperation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblOperation.Location = New System.Drawing.Point(3, 19)
        Me.tblOperation.Name = "tblOperation"
        Me.tblOperation.RowCount = 4
        Me.tblOperation.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tblOperation.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tblOperation.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tblOperation.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tblOperation.Size = New System.Drawing.Size(861, 128)
        Me.tblOperation.TabIndex = 0
        '
        'lblOperationType
        '
        Me.lblOperationType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOperationType.Location = New System.Drawing.Point(606, 0)
        Me.lblOperationType.Name = "lblOperationType"
        Me.lblOperationType.Size = New System.Drawing.Size(252, 32)
        Me.lblOperationType.TabIndex = 0
        Me.lblOperationType.Text = "نوع العملية"
        Me.lblOperationType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtOperationType
        '
        Me.txtOperationType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtOperationType.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOperationType.Location = New System.Drawing.Point(3, 3)
        Me.txtOperationType.Name = "txtOperationType"
        Me.txtOperationType.ReadOnly = True
        Me.txtOperationType.Size = New System.Drawing.Size(597, 23)
        Me.txtOperationType.TabIndex = 1
        '
        'lblVault
        '
        Me.lblVault.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblVault.Location = New System.Drawing.Point(606, 32)
        Me.lblVault.Name = "lblVault"
        Me.lblVault.Size = New System.Drawing.Size(252, 32)
        Me.lblVault.TabIndex = 2
        Me.lblVault.Text = "الخزينة"
        Me.lblVault.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVault
        '
        Me.txtVault.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtVault.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVault.Location = New System.Drawing.Point(3, 35)
        Me.txtVault.Name = "txtVault"
        Me.txtVault.ReadOnly = True
        Me.txtVault.Size = New System.Drawing.Size(597, 23)
        Me.txtVault.TabIndex = 3
        '
        'lblCurrency
        '
        Me.lblCurrency.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCurrency.Location = New System.Drawing.Point(606, 64)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(252, 32)
        Me.lblCurrency.TabIndex = 4
        Me.lblCurrency.Text = "العملة"
        Me.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCurrency
        '
        Me.txtCurrency.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCurrency.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrency.Location = New System.Drawing.Point(3, 67)
        Me.txtCurrency.Name = "txtCurrency"
        Me.txtCurrency.ReadOnly = True
        Me.txtCurrency.Size = New System.Drawing.Size(597, 23)
        Me.txtCurrency.TabIndex = 5
        '
        'lblStatus
        '
        Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblStatus.Location = New System.Drawing.Point(606, 96)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(252, 32)
        Me.lblStatus.TabIndex = 6
        Me.lblStatus.Text = "الحالة"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtStatus
        '
        Me.txtStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(3, 99)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(597, 23)
        Me.txtStatus.TabIndex = 7
        '
        'grpFinance
        '
        Me.grpFinance.Controls.Add(Me.tblFinance)
        Me.grpFinance.Location = New System.Drawing.Point(80, 322)
        Me.grpFinance.Name = "grpFinance"
        Me.grpFinance.Size = New System.Drawing.Size(867, 210)
        Me.grpFinance.TabIndex = 2
        Me.grpFinance.TabStop = False
        Me.grpFinance.Text = "القيم المالية"
        '
        'tblFinance
        '
        Me.tblFinance.ColumnCount = 2
        Me.tblFinance.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.tblFinance.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.tblFinance.Controls.Add(Me.lblForeignAmount, 0, 0)
        Me.tblFinance.Controls.Add(Me.txtForeignAmount, 1, 0)
        Me.tblFinance.Controls.Add(Me.lblRate, 0, 1)
        Me.tblFinance.Controls.Add(Me.txtRate, 1, 1)
        Me.tblFinance.Controls.Add(Me.lblCommissionPercent, 0, 2)
        Me.tblFinance.Controls.Add(Me.txtCommissionPercent, 1, 2)
        Me.tblFinance.Controls.Add(Me.lblCommissionLYD, 0, 3)
        Me.tblFinance.Controls.Add(Me.txtCommissionLYD, 1, 3)
        Me.tblFinance.Controls.Add(Me.lblTotalLYD, 0, 4)
        Me.tblFinance.Controls.Add(Me.txtTotalLYD, 1, 4)
        Me.tblFinance.Controls.Add(Me.lblNetLYD, 0, 5)
        Me.tblFinance.Controls.Add(Me.txtNetLYD, 1, 5)
        Me.tblFinance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblFinance.Location = New System.Drawing.Point(3, 19)
        Me.tblFinance.Name = "tblFinance"
        Me.tblFinance.RowCount = 6
        Me.tblFinance.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tblFinance.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tblFinance.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tblFinance.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tblFinance.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tblFinance.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.tblFinance.Size = New System.Drawing.Size(861, 188)
        Me.tblFinance.TabIndex = 0
        '
        'lblForeignAmount
        '
        Me.lblForeignAmount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblForeignAmount.Location = New System.Drawing.Point(606, 0)
        Me.lblForeignAmount.Name = "lblForeignAmount"
        Me.lblForeignAmount.Size = New System.Drawing.Size(252, 32)
        Me.lblForeignAmount.TabIndex = 0
        Me.lblForeignAmount.Text = "القيمة الأجنبية"
        Me.lblForeignAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtForeignAmount
        '
        Me.txtForeignAmount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtForeignAmount.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtForeignAmount.Location = New System.Drawing.Point(3, 3)
        Me.txtForeignAmount.Name = "txtForeignAmount"
        Me.txtForeignAmount.ReadOnly = True
        Me.txtForeignAmount.Size = New System.Drawing.Size(597, 23)
        Me.txtForeignAmount.TabIndex = 1
        '
        'lblRate
        '
        Me.lblRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRate.Location = New System.Drawing.Point(606, 32)
        Me.lblRate.Name = "lblRate"
        Me.lblRate.Size = New System.Drawing.Size(252, 32)
        Me.lblRate.TabIndex = 2
        Me.lblRate.Text = "سعر الصرف"
        Me.lblRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRate
        '
        Me.txtRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtRate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRate.Location = New System.Drawing.Point(3, 35)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.ReadOnly = True
        Me.txtRate.Size = New System.Drawing.Size(597, 23)
        Me.txtRate.TabIndex = 3
        '
        'lblCommissionPercent
        '
        Me.lblCommissionPercent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCommissionPercent.Location = New System.Drawing.Point(606, 64)
        Me.lblCommissionPercent.Name = "lblCommissionPercent"
        Me.lblCommissionPercent.Size = New System.Drawing.Size(252, 32)
        Me.lblCommissionPercent.TabIndex = 4
        Me.lblCommissionPercent.Text = "النسبة %"
        Me.lblCommissionPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCommissionPercent
        '
        Me.txtCommissionPercent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCommissionPercent.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCommissionPercent.Location = New System.Drawing.Point(3, 67)
        Me.txtCommissionPercent.Name = "txtCommissionPercent"
        Me.txtCommissionPercent.ReadOnly = True
        Me.txtCommissionPercent.Size = New System.Drawing.Size(597, 23)
        Me.txtCommissionPercent.TabIndex = 5
        '
        'lblCommissionLYD
        '
        Me.lblCommissionLYD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCommissionLYD.Location = New System.Drawing.Point(606, 96)
        Me.lblCommissionLYD.Name = "lblCommissionLYD"
        Me.lblCommissionLYD.Size = New System.Drawing.Size(252, 32)
        Me.lblCommissionLYD.TabIndex = 6
        Me.lblCommissionLYD.Text = "العمولة LYD"
        Me.lblCommissionLYD.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCommissionLYD
        '
        Me.txtCommissionLYD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtCommissionLYD.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCommissionLYD.Location = New System.Drawing.Point(3, 99)
        Me.txtCommissionLYD.Name = "txtCommissionLYD"
        Me.txtCommissionLYD.ReadOnly = True
        Me.txtCommissionLYD.Size = New System.Drawing.Size(597, 23)
        Me.txtCommissionLYD.TabIndex = 7
        '
        'lblTotalLYD
        '
        Me.lblTotalLYD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotalLYD.Location = New System.Drawing.Point(606, 128)
        Me.lblTotalLYD.Name = "lblTotalLYD"
        Me.lblTotalLYD.Size = New System.Drawing.Size(252, 32)
        Me.lblTotalLYD.TabIndex = 8
        Me.lblTotalLYD.Text = "القيمة LYD"
        Me.lblTotalLYD.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTotalLYD
        '
        Me.txtTotalLYD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTotalLYD.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalLYD.Location = New System.Drawing.Point(3, 131)
        Me.txtTotalLYD.Name = "txtTotalLYD"
        Me.txtTotalLYD.ReadOnly = True
        Me.txtTotalLYD.Size = New System.Drawing.Size(597, 23)
        Me.txtTotalLYD.TabIndex = 9
        '
        'lblNetLYD
        '
        Me.lblNetLYD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblNetLYD.Location = New System.Drawing.Point(606, 160)
        Me.lblNetLYD.Name = "lblNetLYD"
        Me.lblNetLYD.Size = New System.Drawing.Size(252, 32)
        Me.lblNetLYD.TabIndex = 10
        Me.lblNetLYD.Text = "الصافي LYD"
        Me.lblNetLYD.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNetLYD
        '
        Me.txtNetLYD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtNetLYD.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetLYD.Location = New System.Drawing.Point(3, 163)
        Me.txtNetLYD.Name = "txtNetLYD"
        Me.txtNetLYD.ReadOnly = True
        Me.txtNetLYD.Size = New System.Drawing.Size(597, 23)
        Me.txtNetLYD.TabIndex = 11
        '
        'btnClose
        '
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(827, 628)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(120, 38)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "إغلاق"
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
        Me.ADD_Doc_btn.Location = New System.Drawing.Point(230, 534)
        Me.ADD_Doc_btn.Margin = New System.Windows.Forms.Padding(2)
        Me.ADD_Doc_btn.Name = "ADD_Doc_btn"
        Me.ADD_Doc_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ADD_Doc_btn.Size = New System.Drawing.Size(44, 92)
        Me.ADD_Doc_btn.TabIndex = 1123
        Me.ADD_Doc_btn.TabStop = False
        Me.ADD_Doc_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ADD_Doc_btn.UseVisualStyleBackColor = False
        Me.ADD_Doc_btn.Visible = False
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
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Info
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DocGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DocGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DocGridView.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DocGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2
        Me.DocGridView.Location = New System.Drawing.Point(276, 534)
        Me.DocGridView.Margin = New System.Windows.Forms.Padding(2)
        Me.DocGridView.MultiSelect = False
        Me.DocGridView.Name = "DocGridView"
        Me.DocGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DocGridView.RowTemplate.Height = 25
        Me.DocGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DocGridView.Size = New System.Drawing.Size(669, 92)
        Me.DocGridView.TabIndex = 1122
        '
        'FrmExchangeDetails
        '
        Me.ClientSize = New System.Drawing.Size(950, 670)
        Me.Controls.Add(Me.ADD_Doc_btn)
        Me.Controls.Add(Me.DocGridView)
        Me.Controls.Add(Me.grpCustomer)
        Me.Controls.Add(Me.grpOperation)
        Me.Controls.Add(Me.grpFinance)
        Me.Controls.Add(Me.btnClose)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "FrmExchangeDetails"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "تفاصيل عملية الصرافة"
        Me.grpCustomer.ResumeLayout(False)
        Me.tblCustomer.ResumeLayout(False)
        Me.tblCustomer.PerformLayout()
        Me.grpOperation.ResumeLayout(False)
        Me.tblOperation.ResumeLayout(False)
        Me.tblOperation.PerformLayout()
        Me.grpFinance.ResumeLayout(False)
        Me.tblFinance.ResumeLayout(False)
        Me.tblFinance.PerformLayout()
        CType(Me.DocGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grpCustomer As System.Windows.Forms.GroupBox
    Friend WithEvents tblCustomer As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblExchangeId As System.Windows.Forms.Label
    Friend WithEvents txtExchangeId As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomerName As System.Windows.Forms.Label
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomerIdentity As System.Windows.Forms.Label
    Friend WithEvents txtCustomerIdentity As System.Windows.Forms.TextBox
    Friend WithEvents lblReferenceNo As System.Windows.Forms.Label
    Friend WithEvents txtReferenceNo As System.Windows.Forms.TextBox
    Friend WithEvents lblCreatedAt As System.Windows.Forms.Label
    Friend WithEvents txtCreatedAt As System.Windows.Forms.TextBox

    Friend WithEvents grpOperation As System.Windows.Forms.GroupBox
    Friend WithEvents tblOperation As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblOperationType As System.Windows.Forms.Label
    Friend WithEvents txtOperationType As System.Windows.Forms.TextBox
    Friend WithEvents lblVault As System.Windows.Forms.Label
    Friend WithEvents txtVault As System.Windows.Forms.TextBox
    Friend WithEvents lblCurrency As System.Windows.Forms.Label
    Friend WithEvents txtCurrency As System.Windows.Forms.TextBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox

    Friend WithEvents grpFinance As System.Windows.Forms.GroupBox
    Friend WithEvents tblFinance As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblForeignAmount As System.Windows.Forms.Label
    Friend WithEvents txtForeignAmount As System.Windows.Forms.TextBox
    Friend WithEvents lblRate As System.Windows.Forms.Label
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents lblCommissionPercent As System.Windows.Forms.Label
    Friend WithEvents txtCommissionPercent As System.Windows.Forms.TextBox
    Friend WithEvents lblCommissionLYD As System.Windows.Forms.Label
    Friend WithEvents txtCommissionLYD As System.Windows.Forms.TextBox
    Friend WithEvents lblTotalLYD As System.Windows.Forms.Label
    Friend WithEvents txtTotalLYD As System.Windows.Forms.TextBox
    Friend WithEvents lblNetLYD As System.Windows.Forms.Label
    Friend WithEvents txtNetLYD As System.Windows.Forms.TextBox

    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents ADD_Doc_btn As Button
    Friend WithEvents DocGridView As DataGridView
End Class