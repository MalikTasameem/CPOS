<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmExchangeManager
    Inherits Base_Form
    'System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PanelTop = New System.Windows.Forms.Panel()
        Me.cmbSearchColumn = New System.Windows.Forms.ComboBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.LabelStatus = New System.Windows.Forms.Label()
        Me.cmbVault = New System.Windows.Forms.ComboBox()
        Me.LabelVault = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.LabelTo = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.LabelFrom = New System.Windows.Forms.Label()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.ExchangeId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustomerName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustomerIdentityNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReferenceNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreatedAt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OperationType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VaultName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CurrencyName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ForeignAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalLYD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RateSnapshot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CommissionPercentSnapshot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CommissionLYD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NetLYD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StatusName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StatusId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ForeignCurrencyId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VaultId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Print_CL = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.PanelBottom = New System.Windows.Forms.Panel()
        Me.btnReject = New System.Windows.Forms.Button()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.Print_Btn = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnViewJournal = New System.Windows.Forms.Button()
        Me.btnApprove = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblTotalPending = New System.Windows.Forms.Label()
        Me.lblTotalApproved = New System.Windows.Forms.Label()
        Me.lblTotalAmount = New System.Windows.Forms.Label()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.PanelTop.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelBottom.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelTop
        '
        Me.PanelTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PanelTop.Controls.Add(Me.cmbSearchColumn)
        Me.PanelTop.Controls.Add(Me.txtSearch)
        Me.PanelTop.Controls.Add(Me.btnSearch)
        Me.PanelTop.Controls.Add(Me.cmbStatus)
        Me.PanelTop.Controls.Add(Me.LabelStatus)
        Me.PanelTop.Controls.Add(Me.cmbVault)
        Me.PanelTop.Controls.Add(Me.LabelVault)
        Me.PanelTop.Controls.Add(Me.dtpTo)
        Me.PanelTop.Controls.Add(Me.LabelTo)
        Me.PanelTop.Controls.Add(Me.dtpFrom)
        Me.PanelTop.Controls.Add(Me.LabelFrom)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelTop.Location = New System.Drawing.Point(3, 3)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(1002, 61)
        Me.PanelTop.TabIndex = 0
        '
        'cmbSearchColumn
        '
        Me.cmbSearchColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearchColumn.FormattingEnabled = True
        Me.cmbSearchColumn.Location = New System.Drawing.Point(727, 36)
        Me.cmbSearchColumn.Name = "cmbSearchColumn"
        Me.cmbSearchColumn.Size = New System.Drawing.Size(278, 23)
        Me.cmbSearchColumn.TabIndex = 10
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Location = New System.Drawing.Point(3, 36)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(721, 23)
        Me.txtSearch.TabIndex = 9
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSearch.Location = New System.Drawing.Point(62, 3)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(102, 32)
        Me.btnSearch.TabIndex = 8
        Me.btnSearch.Text = "بحث"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(174, 6)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(150, 25)
        Me.cmbStatus.TabIndex = 7
        '
        'LabelStatus
        '
        Me.LabelStatus.AutoSize = True
        Me.LabelStatus.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelStatus.Location = New System.Drawing.Point(328, 8)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(42, 19)
        Me.LabelStatus.TabIndex = 6
        Me.LabelStatus.Text = "الحالة"
        Me.LabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbVault
        '
        Me.cmbVault.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVault.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbVault.FormattingEnabled = True
        Me.cmbVault.Location = New System.Drawing.Point(377, 6)
        Me.cmbVault.Name = "cmbVault"
        Me.cmbVault.Size = New System.Drawing.Size(250, 25)
        Me.cmbVault.TabIndex = 5
        '
        'LabelVault
        '
        Me.LabelVault.AutoSize = True
        Me.LabelVault.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelVault.Location = New System.Drawing.Point(631, 8)
        Me.LabelVault.Name = "LabelVault"
        Me.LabelVault.Size = New System.Drawing.Size(43, 19)
        Me.LabelVault.TabIndex = 4
        Me.LabelVault.Text = "الخزنة"
        Me.LabelVault.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpTo
        '
        Me.dtpTo.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(680, 7)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(130, 25)
        Me.dtpTo.TabIndex = 3
        '
        'LabelTo
        '
        Me.LabelTo.AutoSize = True
        Me.LabelTo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelTo.Location = New System.Drawing.Point(813, 10)
        Me.LabelTo.Name = "LabelTo"
        Me.LabelTo.Size = New System.Drawing.Size(30, 19)
        Me.LabelTo.TabIndex = 2
        Me.LabelTo.Text = "إلى"
        Me.LabelTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpFrom
        '
        Me.dtpFrom.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(849, 6)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(118, 25)
        Me.dtpFrom.TabIndex = 1
        '
        'LabelFrom
        '
        Me.LabelFrom.AutoSize = True
        Me.LabelFrom.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.LabelFrom.Location = New System.Drawing.Point(971, 8)
        Me.LabelFrom.Name = "LabelFrom"
        Me.LabelFrom.Size = New System.Drawing.Size(26, 19)
        Me.LabelFrom.TabIndex = 0
        Me.LabelFrom.Text = "من"
        Me.LabelFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ExchangeId, Me.CustomerName, Me.CustomerIdentityNumber, Me.ReferenceNo, Me.CreatedAt, Me.OperationType, Me.VaultName, Me.CurrencyName, Me.ForeignAmount, Me.TotalLYD, Me.RateSnapshot, Me.CommissionPercentSnapshot, Me.CommissionLYD, Me.NetLYD, Me.StatusName, Me.StatusId, Me.ForeignCurrencyId, Me.VaultId, Me.Print_CL})
        Me.dgv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv.Location = New System.Drawing.Point(0, 0)
        Me.dgv.MultiSelect = False
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowTemplate.Height = 30
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(1002, 501)
        Me.dgv.TabIndex = 1
        '
        'ExchangeId
        '
        Me.ExchangeId.DataPropertyName = "ExchangeId"
        Me.ExchangeId.HeaderText = "رقم العملية"
        Me.ExchangeId.Name = "ExchangeId"
        Me.ExchangeId.ReadOnly = True
        '
        'CustomerName
        '
        Me.CustomerName.DataPropertyName = "CustomerName"
        Me.CustomerName.HeaderText = "إسم العميل"
        Me.CustomerName.Name = "CustomerName"
        Me.CustomerName.ReadOnly = True
        '
        'CustomerIdentityNumber
        '
        Me.CustomerIdentityNumber.DataPropertyName = "CustomerIdentityNumber"
        Me.CustomerIdentityNumber.HeaderText = "رقم الهوية"
        Me.CustomerIdentityNumber.Name = "CustomerIdentityNumber"
        Me.CustomerIdentityNumber.ReadOnly = True
        '
        'ReferenceNo
        '
        Me.ReferenceNo.DataPropertyName = "ReferenceNo"
        Me.ReferenceNo.HeaderText = "رقم مرجعي"
        Me.ReferenceNo.Name = "ReferenceNo"
        Me.ReferenceNo.ReadOnly = True
        '
        'CreatedAt
        '
        Me.CreatedAt.DataPropertyName = "CreatedAt"
        Me.CreatedAt.HeaderText = "تاريخ الإنشاء"
        Me.CreatedAt.Name = "CreatedAt"
        Me.CreatedAt.ReadOnly = True
        '
        'OperationType
        '
        Me.OperationType.DataPropertyName = "OperationType"
        Me.OperationType.HeaderText = "العملية"
        Me.OperationType.Name = "OperationType"
        Me.OperationType.ReadOnly = True
        '
        'VaultName
        '
        Me.VaultName.DataPropertyName = "VaultName"
        Me.VaultName.HeaderText = "الخزينة"
        Me.VaultName.Name = "VaultName"
        Me.VaultName.ReadOnly = True
        '
        'CurrencyName
        '
        Me.CurrencyName.DataPropertyName = "CurrencyName"
        Me.CurrencyName.HeaderText = "العملة"
        Me.CurrencyName.Name = "CurrencyName"
        Me.CurrencyName.ReadOnly = True
        '
        'ForeignAmount
        '
        Me.ForeignAmount.DataPropertyName = "ForeignAmount"
        DataGridViewCellStyle6.Format = "N3"
        Me.ForeignAmount.DefaultCellStyle = DataGridViewCellStyle6
        Me.ForeignAmount.HeaderText = "القيمة بالعملة الأجنبية"
        Me.ForeignAmount.Name = "ForeignAmount"
        Me.ForeignAmount.ReadOnly = True
        '
        'TotalLYD
        '
        Me.TotalLYD.DataPropertyName = "TotalLYD"
        DataGridViewCellStyle7.Format = "N3"
        Me.TotalLYD.DefaultCellStyle = DataGridViewCellStyle7
        Me.TotalLYD.HeaderText = "القيمة LYD"
        Me.TotalLYD.Name = "TotalLYD"
        Me.TotalLYD.ReadOnly = True
        '
        'RateSnapshot
        '
        Me.RateSnapshot.DataPropertyName = "RateSnapshot"
        DataGridViewCellStyle8.Format = "N3"
        Me.RateSnapshot.DefaultCellStyle = DataGridViewCellStyle8
        Me.RateSnapshot.HeaderText = "سعر الصرف"
        Me.RateSnapshot.Name = "RateSnapshot"
        Me.RateSnapshot.ReadOnly = True
        '
        'CommissionPercentSnapshot
        '
        Me.CommissionPercentSnapshot.DataPropertyName = "CommissionPercentSnapshot"
        Me.CommissionPercentSnapshot.HeaderText = "النسبة%"
        Me.CommissionPercentSnapshot.Name = "CommissionPercentSnapshot"
        Me.CommissionPercentSnapshot.ReadOnly = True
        '
        'CommissionLYD
        '
        Me.CommissionLYD.DataPropertyName = "CommissionLYD"
        DataGridViewCellStyle9.Format = "N3"
        Me.CommissionLYD.DefaultCellStyle = DataGridViewCellStyle9
        Me.CommissionLYD.HeaderText = "العمولة LYD"
        Me.CommissionLYD.Name = "CommissionLYD"
        Me.CommissionLYD.ReadOnly = True
        '
        'NetLYD
        '
        Me.NetLYD.DataPropertyName = "NetLYD"
        DataGridViewCellStyle10.Format = "N3"
        Me.NetLYD.DefaultCellStyle = DataGridViewCellStyle10
        Me.NetLYD.HeaderText = "الإجمالي LYD"
        Me.NetLYD.Name = "NetLYD"
        Me.NetLYD.ReadOnly = True
        '
        'StatusName
        '
        Me.StatusName.DataPropertyName = "StatusName"
        Me.StatusName.HeaderText = "الحالة"
        Me.StatusName.Name = "StatusName"
        Me.StatusName.ReadOnly = True
        '
        'StatusId
        '
        Me.StatusId.DataPropertyName = "StatusId"
        Me.StatusId.HeaderText = "StatusId"
        Me.StatusId.Name = "StatusId"
        Me.StatusId.ReadOnly = True
        Me.StatusId.Visible = False
        '
        'ForeignCurrencyId
        '
        Me.ForeignCurrencyId.DataPropertyName = "ForeignCurrencyId"
        Me.ForeignCurrencyId.HeaderText = "ForeignCurrencyId"
        Me.ForeignCurrencyId.Name = "ForeignCurrencyId"
        Me.ForeignCurrencyId.ReadOnly = True
        Me.ForeignCurrencyId.Visible = False
        '
        'VaultId
        '
        Me.VaultId.DataPropertyName = "VaultId"
        Me.VaultId.HeaderText = "VaultId"
        Me.VaultId.Name = "VaultId"
        Me.VaultId.ReadOnly = True
        Me.VaultId.Visible = False
        '
        'Print_CL
        '
        Me.Print_CL.DataPropertyName = "Print_CL"
        Me.Print_CL.HeaderText = ""
        Me.Print_CL.Name = "Print_CL"
        Me.Print_CL.ReadOnly = True
        Me.Print_CL.Text = "🖨️"
        Me.Print_CL.UseColumnTextForButtonValue = True
        '
        'PanelBottom
        '
        Me.PanelBottom.BackColor = System.Drawing.Color.White
        Me.PanelBottom.Controls.Add(Me.btnReject)
        Me.PanelBottom.Controls.Add(Me.CheckedListBox1)
        Me.PanelBottom.Controls.Add(Me.Print_Btn)
        Me.PanelBottom.Controls.Add(Me.btnClose)
        Me.PanelBottom.Controls.Add(Me.btnRefresh)
        Me.PanelBottom.Controls.Add(Me.btnViewJournal)
        Me.PanelBottom.Controls.Add(Me.btnApprove)
        Me.PanelBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelBottom.Location = New System.Drawing.Point(3, 624)
        Me.PanelBottom.Name = "PanelBottom"
        Me.PanelBottom.Size = New System.Drawing.Size(1002, 72)
        Me.PanelBottom.TabIndex = 2
        '
        'btnReject
        '
        Me.btnReject.BackColor = System.Drawing.Color.Firebrick
        Me.btnReject.FlatAppearance.BorderColor = System.Drawing.Color.DarkRed
        Me.btnReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReject.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnReject.ForeColor = System.Drawing.Color.White
        Me.btnReject.Location = New System.Drawing.Point(3, 12)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(122, 36)
        Me.btnReject.TabIndex = 1
        Me.btnReject.Text = "Reject"
        Me.btnReject.UseVisualStyleBackColor = False
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.HorizontalScrollbar = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(436, 2)
        Me.CheckedListBox1.MultiColumn = True
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CheckedListBox1.Size = New System.Drawing.Size(464, 68)
        Me.CheckedListBox1.TabIndex = 902
        '
        'Print_Btn
        '
        Me.Print_Btn.BackColor = System.Drawing.Color.White
        Me.Print_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Print_Btn.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Print_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_Btn.Location = New System.Drawing.Point(253, 12)
        Me.Print_Btn.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Print_Btn.Name = "Print_Btn"
        Me.Print_Btn.Size = New System.Drawing.Size(149, 36)
        Me.Print_Btn.TabIndex = 104
        Me.Print_Btn.Text = "🖨️  طباعــة"
        Me.Print_Btn.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.Location = New System.Drawing.Point(906, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(90, 36)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "إغلاق"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.Location = New System.Drawing.Point(11, 12)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(20, 36)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "تحديث"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnViewJournal
        '
        Me.btnViewJournal.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnViewJournal.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.btnViewJournal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewJournal.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnViewJournal.Location = New System.Drawing.Point(2, 12)
        Me.btnViewJournal.Name = "btnViewJournal"
        Me.btnViewJournal.Size = New System.Drawing.Size(24, 36)
        Me.btnViewJournal.TabIndex = 2
        Me.btnViewJournal.Text = "عرض القيد"
        Me.btnViewJournal.UseVisualStyleBackColor = False
        Me.btnViewJournal.Visible = False
        '
        'btnApprove
        '
        Me.btnApprove.BackColor = System.Drawing.Color.SeaGreen
        Me.btnApprove.FlatAppearance.BorderColor = System.Drawing.Color.DarkGreen
        Me.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnApprove.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnApprove.ForeColor = System.Drawing.Color.White
        Me.btnApprove.Location = New System.Drawing.Point(128, 12)
        Me.btnApprove.Name = "btnApprove"
        Me.btnApprove.Size = New System.Drawing.Size(122, 36)
        Me.btnApprove.TabIndex = 0
        Me.btnApprove.Text = "Approve"
        Me.btnApprove.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.lblTotalPending)
        Me.Panel1.Controls.Add(Me.lblTotalApproved)
        Me.Panel1.Controls.Add(Me.lblTotalAmount)
        Me.Panel1.Controls.Add(Me.lblCount)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 577)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1002, 41)
        Me.Panel1.TabIndex = 3
        '
        'lblTotalPending
        '
        Me.lblTotalPending.AutoSize = True
        Me.lblTotalPending.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPending.Location = New System.Drawing.Point(38, 7)
        Me.lblTotalPending.Name = "lblTotalPending"
        Me.lblTotalPending.Size = New System.Drawing.Size(60, 21)
        Me.lblTotalPending.TabIndex = 3
        Me.lblTotalPending.Text = "Label3"
        '
        'lblTotalApproved
        '
        Me.lblTotalApproved.AutoSize = True
        Me.lblTotalApproved.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalApproved.Location = New System.Drawing.Point(298, 7)
        Me.lblTotalApproved.Name = "lblTotalApproved"
        Me.lblTotalApproved.Size = New System.Drawing.Size(60, 21)
        Me.lblTotalApproved.TabIndex = 2
        Me.lblTotalApproved.Text = "Label2"
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.AutoSize = True
        Me.lblTotalAmount.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalAmount.Location = New System.Drawing.Point(620, 7)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(60, 21)
        Me.lblTotalAmount.TabIndex = 1
        Me.lblTotalAmount.Text = "Label1"
        '
        'lblCount
        '
        Me.lblCount.AutoSize = True
        Me.lblCount.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCount.Location = New System.Drawing.Point(877, 7)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(60, 21)
        Me.lblCount.TabIndex = 0
        Me.lblCount.Text = "Label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.dgv)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 70)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1002, 501)
        Me.Panel2.TabIndex = 4
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.PanelTop, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PanelBottom, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.81507!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.18493!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1008, 699)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'FrmExchangeManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 699)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "FrmExchangeManager"
        Me.RightToLeftLayout = True
        Me.Text = "إدارة عمليات الصرافة"
        Me.PanelTop.ResumeLayout(False)
        Me.PanelTop.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelBottom.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelTop As Panel
    Friend WithEvents btnSearch As Button
    Friend WithEvents cmbStatus As ComboBox
    Friend WithEvents LabelStatus As Label
    Friend WithEvents cmbVault As ComboBox
    Friend WithEvents LabelVault As Label
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents LabelTo As Label
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents LabelFrom As Label
    Friend WithEvents dgv As DataGridView
    Friend WithEvents PanelBottom As Panel
    Friend WithEvents btnClose As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnViewJournal As Button
    Friend WithEvents btnReject As Button
    Friend WithEvents btnApprove As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblTotalPending As Label
    Friend WithEvents lblTotalApproved As Label
    Friend WithEvents lblTotalAmount As Label
    Friend WithEvents lblCount As Label
    Friend WithEvents ExchangeId As DataGridViewTextBoxColumn
    Friend WithEvents CustomerName As DataGridViewTextBoxColumn
    Friend WithEvents CustomerIdentityNumber As DataGridViewTextBoxColumn
    Friend WithEvents ReferenceNo As DataGridViewTextBoxColumn
    Friend WithEvents CreatedAt As DataGridViewTextBoxColumn
    Friend WithEvents OperationType As DataGridViewTextBoxColumn
    Friend WithEvents VaultName As DataGridViewTextBoxColumn
    Friend WithEvents CurrencyName As DataGridViewTextBoxColumn
    Friend WithEvents ForeignAmount As DataGridViewTextBoxColumn
    Friend WithEvents TotalLYD As DataGridViewTextBoxColumn
    Friend WithEvents RateSnapshot As DataGridViewTextBoxColumn
    Friend WithEvents CommissionPercentSnapshot As DataGridViewTextBoxColumn
    Friend WithEvents CommissionLYD As DataGridViewTextBoxColumn
    Friend WithEvents NetLYD As DataGridViewTextBoxColumn
    Friend WithEvents StatusName As DataGridViewTextBoxColumn
    Friend WithEvents StatusId As DataGridViewTextBoxColumn
    Friend WithEvents ForeignCurrencyId As DataGridViewTextBoxColumn
    Friend WithEvents VaultId As DataGridViewTextBoxColumn
    Friend WithEvents Print_CL As DataGridViewButtonColumn
    Friend WithEvents cmbSearchColumn As ComboBox
    Friend WithEvents Print_Btn As Button
    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
End Class
