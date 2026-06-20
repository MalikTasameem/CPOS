<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmIncomeStatementAccountPicker
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
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.chkOnlyMovementAccounts = New System.Windows.Forms.CheckBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.grpAccounts = New System.Windows.Forms.GroupBox()
        Me.dgvAccounts = New System.Windows.Forms.DataGridView()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.cboAccountSignMode = New System.Windows.Forms.ComboBox()
        Me.lblAccountSignMode = New System.Windows.Forms.Label()
        Me.chkIncludeChildren = New System.Windows.Forms.CheckBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlTop.SuspendLayout()
        Me.grpAccounts.SuspendLayout()
        CType(Me.dgvAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.statusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.btnSearch)
        Me.pnlTop.Controls.Add(Me.chkOnlyMovementAccounts)
        Me.pnlTop.Controls.Add(Me.txtSearch)
        Me.pnlTop.Controls.Add(Me.lblSearch)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(8)
        Me.pnlTop.Size = New System.Drawing.Size(900, 58)
        Me.pnlTop.TabIndex = 0
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(12, 13)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(90, 32)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "بحث"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'chkOnlyMovementAccounts
        '
        Me.chkOnlyMovementAccounts.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkOnlyMovementAccounts.AutoSize = True
        Me.chkOnlyMovementAccounts.Location = New System.Drawing.Point(523, 20)
        Me.chkOnlyMovementAccounts.Name = "chkOnlyMovementAccounts"
        Me.chkOnlyMovementAccounts.Size = New System.Drawing.Size(139, 18)
        Me.chkOnlyMovementAccounts.TabIndex = 2
        Me.chkOnlyMovementAccounts.Text = "حسابات لها حركة فقط"
        Me.chkOnlyMovementAccounts.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.Location = New System.Drawing.Point(108, 18)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(400, 22)
        Me.txtSearch.TabIndex = 1
        '
        'lblSearch
        '
        Me.lblSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Location = New System.Drawing.Point(668, 21)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(182, 14)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "بحث بالكود أو الاسم أو نوع الحساب"
        '
        'grpAccounts
        '
        Me.grpAccounts.Controls.Add(Me.dgvAccounts)
        Me.grpAccounts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpAccounts.Location = New System.Drawing.Point(0, 58)
        Me.grpAccounts.Name = "grpAccounts"
        Me.grpAccounts.Padding = New System.Windows.Forms.Padding(8)
        Me.grpAccounts.Size = New System.Drawing.Size(900, 424)
        Me.grpAccounts.TabIndex = 1
        Me.grpAccounts.TabStop = False
        Me.grpAccounts.Text = "الحسابات"
        '
        'dgvAccounts
        '
        Me.dgvAccounts.AllowUserToAddRows = False
        Me.dgvAccounts.AllowUserToDeleteRows = False
        Me.dgvAccounts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAccounts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAccounts.Location = New System.Drawing.Point(8, 23)
        Me.dgvAccounts.MultiSelect = False
        Me.dgvAccounts.Name = "dgvAccounts"
        Me.dgvAccounts.ReadOnly = True
        Me.dgvAccounts.RowHeadersVisible = False
        Me.dgvAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvAccounts.Size = New System.Drawing.Size(884, 393)
        Me.dgvAccounts.TabIndex = 0
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.cboAccountSignMode)
        Me.pnlBottom.Controls.Add(Me.lblAccountSignMode)
        Me.pnlBottom.Controls.Add(Me.chkIncludeChildren)
        Me.pnlBottom.Controls.Add(Me.btnCancel)
        Me.pnlBottom.Controls.Add(Me.btnOk)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 482)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Padding = New System.Windows.Forms.Padding(8)
        Me.pnlBottom.Size = New System.Drawing.Size(900, 60)
        Me.pnlBottom.TabIndex = 2
        '
        'cboAccountSignMode
        '
        Me.cboAccountSignMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboAccountSignMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAccountSignMode.FormattingEnabled = True
        Me.cboAccountSignMode.Location = New System.Drawing.Point(468, 19)
        Me.cboAccountSignMode.Name = "cboAccountSignMode"
        Me.cboAccountSignMode.Size = New System.Drawing.Size(210, 22)
        Me.cboAccountSignMode.TabIndex = 4
        '
        'lblAccountSignMode
        '
        Me.lblAccountSignMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAccountSignMode.AutoSize = True
        Me.lblAccountSignMode.Location = New System.Drawing.Point(684, 22)
        Me.lblAccountSignMode.Name = "lblAccountSignMode"
        Me.lblAccountSignMode.Size = New System.Drawing.Size(76, 14)
        Me.lblAccountSignMode.TabIndex = 3
        Me.lblAccountSignMode.Text = "طريقة الإشارة"
        '
        'chkIncludeChildren
        '
        Me.chkIncludeChildren.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkIncludeChildren.AutoSize = True
        Me.chkIncludeChildren.Checked = True
        Me.chkIncludeChildren.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncludeChildren.Location = New System.Drawing.Point(784, 21)
        Me.chkIncludeChildren.Name = "chkIncludeChildren"
        Me.chkIncludeChildren.Size = New System.Drawing.Size(88, 18)
        Me.chkIncludeChildren.TabIndex = 2
        Me.chkIncludeChildren.Text = "يشمل الأبناء"
        Me.chkIncludeChildren.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(12, 15)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(95, 32)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "إلغاء"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(113, 15)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(95, 32)
        Me.btnOk.TabIndex = 0
        Me.btnOk.Text = "اختيار"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'statusStrip1
        '
        Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.statusStrip1.Location = New System.Drawing.Point(0, 542)
        Me.statusStrip1.Name = "statusStrip1"
        Me.statusStrip1.Size = New System.Drawing.Size(900, 22)
        Me.statusStrip1.TabIndex = 3
        Me.statusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(30, 17)
        Me.lblStatus.Text = "جاهز"
        '
        'FrmIncomeStatementAccountPicker
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(900, 564)
        Me.Controls.Add(Me.grpAccounts)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.statusStrip1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Name = "FrmIncomeStatementAccountPicker"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "اختيار حساب لقائمة الدخل"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.grpAccounts.ResumeLayout(False)
        CType(Me.dgvAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.statusStrip1.ResumeLayout(False)
        Me.statusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlTop As Panel
    Friend WithEvents btnSearch As Button
    Friend WithEvents chkOnlyMovementAccounts As CheckBox
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents grpAccounts As GroupBox
    Friend WithEvents dgvAccounts As DataGridView
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents cboAccountSignMode As ComboBox
    Friend WithEvents lblAccountSignMode As Label
    Friend WithEvents chkIncludeChildren As CheckBox
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnOk As Button
    Friend WithEvents statusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel

End Class