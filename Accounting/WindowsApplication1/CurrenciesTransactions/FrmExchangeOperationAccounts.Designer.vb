<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmExchangeOperationAccounts
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.PanelTop = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.PanelBottom = New System.Windows.Forms.Panel()
        Me.btnDeleteRow = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnAddRow = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.PanelTop.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelTop
        '
        Me.PanelTop.BackColor = System.Drawing.Color.White
        Me.PanelTop.Controls.Add(Me.lblTitle)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(984, 60)
        Me.PanelTop.TabIndex = 2
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.Location = New System.Drawing.Point(20, 15)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(306, 25)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "إعداد ربط الحسابات حسب نوع العملية"
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        Me.dgv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv.Location = New System.Drawing.Point(0, 60)
        Me.dgv.Name = "dgv"
        Me.dgv.RowHeadersVisible = False
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(984, 391)
        Me.dgv.TabIndex = 0
        '
        'PanelBottom
        '
        Me.PanelBottom.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PanelBottom.Controls.Add(Me.btnDeleteRow)
        Me.PanelBottom.Controls.Add(Me.btnSave)
        Me.PanelBottom.Controls.Add(Me.btnAddRow)
        Me.PanelBottom.Controls.Add(Me.btnRefresh)
        Me.PanelBottom.Controls.Add(Me.btnClose)
        Me.PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelBottom.Location = New System.Drawing.Point(0, 451)
        Me.PanelBottom.Name = "PanelBottom"
        Me.PanelBottom.Size = New System.Drawing.Size(984, 60)
        Me.PanelBottom.TabIndex = 1
        '
        'btnDeleteRow
        '
        Me.btnDeleteRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeleteRow.Location = New System.Drawing.Point(507, 12)
        Me.btnDeleteRow.Name = "btnDeleteRow"
        Me.btnDeleteRow.Size = New System.Drawing.Size(97, 36)
        Me.btnDeleteRow.TabIndex = 4
        Me.btnDeleteRow.Text = "مسح"
        Me.btnDeleteRow.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.SeaGreen
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(850, 12)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(120, 36)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "حفظ التعديلات"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnAddRow
        '
        Me.btnAddRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.btnAddRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddRow.Location = New System.Drawing.Point(610, 12)
        Me.btnAddRow.Name = "btnAddRow"
        Me.btnAddRow.Size = New System.Drawing.Size(100, 36)
        Me.btnAddRow.TabIndex = 3
        Me.btnAddRow.Text = "إضافة سطر"
        Me.btnAddRow.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Location = New System.Drawing.Point(714, 12)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 36)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "تحديث"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.MistyRose
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(20, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 36)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "إغلاق"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'FrmExchangeOperationAccounts
        '
        Me.ClientSize = New System.Drawing.Size(984, 511)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.PanelBottom)
        Me.Controls.Add(Me.PanelTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "FrmExchangeOperationAccounts"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ربط الحسابات بعمليات الصرافة"
        Me.PanelTop.ResumeLayout(False)
        Me.PanelTop.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelTop As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents dgv As DataGridView
    Friend WithEvents PanelBottom As Panel
    Friend WithEvents btnSave As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnAddRow As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents btnDeleteRow As Button
End Class










'<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
'Partial Class FrmExchangeOperationAccounts
'    Inherits System.Windows.Forms.Form

'    Private components As System.ComponentModel.IContainer

'    <System.Diagnostics.DebuggerStepThrough()>
'    Private Sub InitializeComponent()
'        Me.PanelTop = New System.Windows.Forms.Panel()
'        Me.lblTitle = New System.Windows.Forms.Label()
'        Me.dgv = New System.Windows.Forms.DataGridView()
'        Me.PanelBottom = New System.Windows.Forms.Panel()
'        Me.btnSave = New System.Windows.Forms.Button()
'        Me.btnRefresh = New System.Windows.Forms.Button()
'        Me.btnClose = New System.Windows.Forms.Button()
'        Me.PanelTop.SuspendLayout()
'        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.PanelBottom.SuspendLayout()
'        Me.SuspendLayout()
'        '
'        'PanelTop
'        '
'        Me.PanelTop.BackColor = System.Drawing.Color.White
'        Me.PanelTop.Controls.Add(Me.lblTitle)
'        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
'        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
'        Me.PanelTop.Name = "PanelTop"
'        Me.PanelTop.Size = New System.Drawing.Size(984, 60)
'        Me.PanelTop.TabIndex = 2
'        '
'        'lblTitle
'        '
'        Me.lblTitle.AutoSize = True
'        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
'        Me.lblTitle.Location = New System.Drawing.Point(20, 15)
'        Me.lblTitle.Name = "lblTitle"
'        Me.lblTitle.Size = New System.Drawing.Size(306, 25)
'        Me.lblTitle.TabIndex = 0
'        Me.lblTitle.Text = "إعداد ربط الحسابات حسب نوع العملية"
'        '
'        'dgv
'        '
'        Me.dgv.AllowUserToAddRows = False
'        Me.dgv.AllowUserToDeleteRows = False
'        Me.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
'        Me.dgv.BackgroundColor = System.Drawing.Color.White
'        Me.dgv.Dock = System.Windows.Forms.DockStyle.Fill
'        Me.dgv.Location = New System.Drawing.Point(0, 60)
'        Me.dgv.Name = "dgv"
'        Me.dgv.RowHeadersVisible = False
'        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
'        Me.dgv.Size = New System.Drawing.Size(984, 391)
'        Me.dgv.TabIndex = 0
'        '
'        'PanelBottom
'        '
'        Me.PanelBottom.BackColor = System.Drawing.Color.WhiteSmoke
'        Me.PanelBottom.Controls.Add(Me.btnSave)
'        Me.PanelBottom.Controls.Add(Me.btnRefresh)
'        Me.PanelBottom.Controls.Add(Me.btnClose)
'        Me.PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
'        Me.PanelBottom.Location = New System.Drawing.Point(0, 451)
'        Me.PanelBottom.Name = "PanelBottom"
'        Me.PanelBottom.Size = New System.Drawing.Size(984, 60)
'        Me.PanelBottom.TabIndex = 1
'        '
'        'btnSave
'        '
'        Me.btnSave.BackColor = System.Drawing.Color.SeaGreen
'        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
'        Me.btnSave.ForeColor = System.Drawing.Color.White
'        Me.btnSave.Location = New System.Drawing.Point(850, 12)
'        Me.btnSave.Name = "btnSave"
'        Me.btnSave.Size = New System.Drawing.Size(120, 36)
'        Me.btnSave.TabIndex = 0
'        Me.btnSave.Text = "حفظ التعديلات"
'        Me.btnSave.UseVisualStyleBackColor = False
'        '
'        'btnRefresh
'        '
'        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
'        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
'        Me.btnRefresh.Location = New System.Drawing.Point(720, 12)
'        Me.btnRefresh.Name = "btnRefresh"
'        Me.btnRefresh.Size = New System.Drawing.Size(100, 36)
'        Me.btnRefresh.TabIndex = 1
'        Me.btnRefresh.Text = "تحديث"
'        Me.btnRefresh.UseVisualStyleBackColor = False
'        '
'        'btnClose
'        '
'        Me.btnClose.BackColor = System.Drawing.Color.MistyRose
'        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
'        Me.btnClose.Location = New System.Drawing.Point(20, 12)
'        Me.btnClose.Name = "btnClose"
'        Me.btnClose.Size = New System.Drawing.Size(100, 36)
'        Me.btnClose.TabIndex = 2
'        Me.btnClose.Text = "إغلاق"
'        Me.btnClose.UseVisualStyleBackColor = False
'        '
'        'FrmExchangeOperationAccounts
'        '
'        Me.ClientSize = New System.Drawing.Size(984, 511)
'        Me.Controls.Add(Me.dgv)
'        Me.Controls.Add(Me.PanelBottom)
'        Me.Controls.Add(Me.PanelTop)
'        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
'        Me.Name = "FrmExchangeOperationAccounts"
'        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
'        Me.RightToLeftLayout = True
'        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
'        Me.Text = "ربط الحسابات بعمليات الصرافة"
'        Me.PanelTop.ResumeLayout(False)
'        Me.PanelTop.PerformLayout()
'        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.PanelBottom.ResumeLayout(False)
'        Me.ResumeLayout(False)

'    End Sub

'    Friend WithEvents PanelTop As Panel
'    Friend WithEvents lblTitle As Label
'    Friend WithEvents dgv As DataGridView
'    Friend WithEvents PanelBottom As Panel
'    Friend WithEvents btnSave As Button
'    Friend WithEvents btnRefresh As Button
'    Friend WithEvents btnClose As Button

'End Class
