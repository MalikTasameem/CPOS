<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAccountPicker
    Inherits System.Windows.Forms.Form

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

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.dgvAccounts = New System.Windows.Forms.DataGridView()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()


        Me.pnlHeader.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        CType(Me.dgvAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(72, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Padding = New System.Windows.Forms.Padding(12, 8, 12, 8)
        Me.pnlHeader.Size = New System.Drawing.Size(820, 58)
        Me.pnlHeader.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(12, 8)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(796, 42)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "اختيار الحساب"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlSearch.Controls.Add(Me.btnSearch)
        Me.pnlSearch.Controls.Add(Me.txtSearch)
        Me.pnlSearch.Controls.Add(Me.lblSearch)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Location = New System.Drawing.Point(0, 58)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlSearch.Size = New System.Drawing.Size(820, 58)
        Me.pnlSearch.TabIndex = 1
        '
        'btnSearch
        '
        Me.btnSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSearch.Location = New System.Drawing.Point(10, 10)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(110, 38)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "بحث"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.txtSearch.Location = New System.Drawing.Point(10, 10)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(700, 24)
        Me.txtSearch.TabIndex = 1
        '
        'lblSearch
        '
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblSearch.Location = New System.Drawing.Point(710, 10)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(100, 38)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "بحث:"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.dgvAccounts)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 116)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlMain.Size = New System.Drawing.Size(820, 394)
        Me.pnlMain.TabIndex = 2
        '
        'dgvAccounts
        '
        Me.dgvAccounts.AllowUserToAddRows = False
        Me.dgvAccounts.AllowUserToDeleteRows = False
        Me.dgvAccounts.AllowUserToResizeRows = False
        Me.dgvAccounts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvAccounts.BackgroundColor = System.Drawing.Color.White
        Me.dgvAccounts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvAccounts.ColumnHeadersHeight = 36
        Me.dgvAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvAccounts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAccounts.Location = New System.Drawing.Point(10, 10)
        Me.dgvAccounts.MultiSelect = False
        Me.dgvAccounts.Name = "dgvAccounts"
        Me.dgvAccounts.ReadOnly = True
        Me.dgvAccounts.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvAccounts.RowHeadersVisible = False
        Me.dgvAccounts.RowTemplate.Height = 30
        Me.dgvAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvAccounts.Size = New System.Drawing.Size(800, 374)
        Me.dgvAccounts.TabIndex = 0
        '
        'pnlButtons
        '
        Me.pnlButtons.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlButtons.Controls.Add(Me.btnCancel)
        Me.pnlButtons.Controls.Add(Me.btnSelect)
        Me.pnlButtons.Controls.Add(Me.lblStatus)


        Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlButtons.Location = New System.Drawing.Point(0, 510)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlButtons.Size = New System.Drawing.Size(820, 58)
        Me.pnlButtons.TabIndex = 3
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.Location = New System.Drawing.Point(10, 10)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(120, 38)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "إلغاء"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        Me.btnSelect.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSelect.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSelect.Location = New System.Drawing.Point(680, 10)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(130, 38)
        Me.btnSelect.TabIndex = 1
        Me.btnSelect.Text = "اختيار"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblStatus.ForeColor = System.Drawing.Color.DimGray
        Me.lblStatus.Location = New System.Drawing.Point(10, 10)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(800, 38)
        Me.lblStatus.TabIndex = 0
        Me.lblStatus.Text = "اختر الحساب المطلوب"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmAccountPicker
        '
        Me.AcceptButton = Me.btnSelect
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(820, 568)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlButtons)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.MinimumSize = New System.Drawing.Size(750, 500)
        Me.Name = "FrmAccountPicker"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "اختيار الحساب"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        CType(Me.dgvAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents pnlSearch As Panel
    Friend WithEvents btnSearch As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents pnlMain As Panel
    Friend WithEvents dgvAccounts As DataGridView
    Friend WithEvents pnlButtons As Panel
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSelect As Button
    Friend WithEvents lblStatus As Label



End Class