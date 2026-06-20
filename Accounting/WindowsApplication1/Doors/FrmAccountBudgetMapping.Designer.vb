<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAccountBudgetMapping
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    <System.Diagnostics.DebuggerNonUserCode()>
    Private Sub InitializeComponent()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblSubTitle = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlContent = New System.Windows.Forms.Panel()
        Me.cardGrid = New System.Windows.Forms.Panel()
        Me.dgvMapping = New System.Windows.Forms.DataGridView()
        Me.cardForm = New System.Windows.Forms.Panel()
        Me.chkIsDefault = New System.Windows.Forms.CheckBox()
        Me.cmbItems = New System.Windows.Forms.ComboBox()
        Me.txtAccountCode = New System.Windows.Forms.TextBox()
        Me.btnPickAccount = New System.Windows.Forms.Button()
        Me.txtAccountName = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblTotalItemsStat = New System.Windows.Forms.Label()
        Me.lblLinkedItemsStat = New System.Windows.Forms.Label()
        Me.lblUnlinkedItemsStat = New System.Windows.Forms.Label()
        Me.lblItem = New System.Windows.Forms.Label()
        Me.lblAccount = New System.Windows.Forms.Label()
        Me.cmbAccounts = New System.Windows.Forms.ComboBox()
        Me.pnlActions = New System.Windows.Forms.Panel()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlHeader.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.cardGrid.SuspendLayout()
        CType(Me.dgvMapping, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cardForm.SuspendLayout()
        Me.pnlActions.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblSubTitle)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1200, 70)
        Me.pnlHeader.TabIndex = 2
        '
        'lblSubTitle
        '
        Me.lblSubTitle.AutoSize = True
        Me.lblSubTitle.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblSubTitle.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblSubTitle.Location = New System.Drawing.Point(625, 41)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Size = New System.Drawing.Size(228, 17)
        Me.lblSubTitle.TabIndex = 0
        Me.lblSubTitle.Text = "تحديد البند الافتراضي لكل حساب محاسبي"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(900, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(259, 30)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "ربط الحسابات ببنود الموازنة"
        '
        'pnlContent
        '
        Me.pnlContent.Controls.Add(Me.cardGrid)
        Me.pnlContent.Controls.Add(Me.cardForm)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 70)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlContent.Size = New System.Drawing.Size(1200, 553)
        Me.pnlContent.TabIndex = 0
        '
        'cardGrid
        '
        Me.cardGrid.BackColor = System.Drawing.Color.White
        Me.cardGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardGrid.Controls.Add(Me.dgvMapping)
        Me.cardGrid.Location = New System.Drawing.Point(15, 170)
        Me.cardGrid.Name = "cardGrid"
        Me.cardGrid.Size = New System.Drawing.Size(1170, 400)
        Me.cardGrid.TabIndex = 0
        '
        'dgvMapping
        '
        Me.dgvMapping.AllowUserToAddRows = False
        Me.dgvMapping.AllowUserToDeleteRows = False
        Me.dgvMapping.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvMapping.BackgroundColor = System.Drawing.Color.White
        Me.dgvMapping.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvMapping.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvMapping.Location = New System.Drawing.Point(0, 0)
        Me.dgvMapping.MultiSelect = False
        Me.dgvMapping.Name = "dgvMapping"
        Me.dgvMapping.ReadOnly = True
        Me.dgvMapping.RowHeadersVisible = False
        Me.dgvMapping.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMapping.Size = New System.Drawing.Size(1168, 398)
        Me.dgvMapping.TabIndex = 0
        '
        'cardForm
        '
        Me.cardForm.BackColor = System.Drawing.Color.White
        Me.cardForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardForm.Controls.Add(Me.chkIsDefault)
        Me.cardForm.Controls.Add(Me.cmbItems)
        Me.cardForm.Controls.Add(Me.txtAccountCode)
        Me.cardForm.Controls.Add(Me.btnPickAccount)
        Me.cardForm.Controls.Add(Me.txtAccountName)
        Me.cardForm.Controls.Add(Me.lblSearch)
        Me.cardForm.Controls.Add(Me.txtSearch)
        Me.cardForm.Controls.Add(Me.lblTotalItemsStat)
        Me.cardForm.Controls.Add(Me.lblLinkedItemsStat)
        Me.cardForm.Controls.Add(Me.lblUnlinkedItemsStat)
        Me.cardForm.Controls.Add(Me.lblItem)
        Me.cardForm.Controls.Add(Me.lblAccount)
        Me.cardForm.Location = New System.Drawing.Point(15, 4)
        Me.cardForm.Name = "cardForm"
        Me.cardForm.Size = New System.Drawing.Size(1170, 163)
        Me.cardForm.TabIndex = 1
        '
        'chkIsDefault
        '
        Me.chkIsDefault.AutoSize = True
        Me.chkIsDefault.Location = New System.Drawing.Point(700, 100)
        Me.chkIsDefault.Name = "chkIsDefault"
        Me.chkIsDefault.Size = New System.Drawing.Size(134, 21)
        Me.chkIsDefault.TabIndex = 0
        Me.chkIsDefault.Text = "بند افتراضي للحساب"
        '
        'cmbItems
        '
        Me.cmbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItems.Location = New System.Drawing.Point(700, 63)
        Me.cmbItems.Name = "cmbItems"
        Me.cmbItems.Size = New System.Drawing.Size(330, 25)
        Me.cmbItems.TabIndex = 1
        '
        'txtAccountCode
        '
        Me.txtAccountCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAccountCode.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtAccountCode.Location = New System.Drawing.Point(930, 23)
        Me.txtAccountCode.Name = "txtAccountCode"
        Me.txtAccountCode.Size = New System.Drawing.Size(100, 25)
        Me.txtAccountCode.TabIndex = 2
        Me.txtAccountCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnPickAccount
        '
        Me.btnPickAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPickAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnPickAccount.Location = New System.Drawing.Point(900, 23)
        Me.btnPickAccount.Name = "btnPickAccount"
        Me.btnPickAccount.Size = New System.Drawing.Size(26, 25)
        Me.btnPickAccount.TabIndex = 3
        Me.btnPickAccount.Text = "..."
        Me.btnPickAccount.UseVisualStyleBackColor = True
        '
        'txtAccountName
        '
        Me.txtAccountName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtAccountName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAccountName.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtAccountName.Location = New System.Drawing.Point(700, 23)
        Me.txtAccountName.Name = "txtAccountName"
        Me.txtAccountName.ReadOnly = True
        Me.txtAccountName.Size = New System.Drawing.Size(196, 25)
        Me.txtAccountName.TabIndex = 4
        Me.txtAccountName.TabStop = False
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblSearch.Location = New System.Drawing.Point(1127, 138)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(35, 19)
        Me.lblSearch.TabIndex = 9
        Me.lblSearch.Text = "بحث"
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.txtSearch.Location = New System.Drawing.Point(700, 135)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(423, 25)
        Me.txtSearch.TabIndex = 8
        Me.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTotalItemsStat
        '
        Me.lblTotalItemsStat.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.lblTotalItemsStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalItemsStat.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblTotalItemsStat.Location = New System.Drawing.Point(432, 5)
        Me.lblTotalItemsStat.Name = "lblTotalItemsStat"
        Me.lblTotalItemsStat.Size = New System.Drawing.Size(190, 32)
        Me.lblTotalItemsStat.TabIndex = 10
        Me.lblTotalItemsStat.Text = "إجمالي البنود: 0"
        Me.lblTotalItemsStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLinkedItemsStat
        '
        Me.lblLinkedItemsStat.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.lblLinkedItemsStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLinkedItemsStat.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblLinkedItemsStat.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblLinkedItemsStat.Location = New System.Drawing.Point(217, 5)
        Me.lblLinkedItemsStat.Name = "lblLinkedItemsStat"
        Me.lblLinkedItemsStat.Size = New System.Drawing.Size(190, 32)
        Me.lblLinkedItemsStat.TabIndex = 11
        Me.lblLinkedItemsStat.Text = "بنود مرتبطة: 0"
        Me.lblLinkedItemsStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblUnlinkedItemsStat
        '
        Me.lblUnlinkedItemsStat.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lblUnlinkedItemsStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnlinkedItemsStat.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblUnlinkedItemsStat.ForeColor = System.Drawing.Color.DarkRed
        Me.lblUnlinkedItemsStat.Location = New System.Drawing.Point(2, 5)
        Me.lblUnlinkedItemsStat.Name = "lblUnlinkedItemsStat"
        Me.lblUnlinkedItemsStat.Size = New System.Drawing.Size(190, 32)
        Me.lblUnlinkedItemsStat.TabIndex = 12
        Me.lblUnlinkedItemsStat.Text = "بنود غير مرتبطة: 0"
        Me.lblUnlinkedItemsStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblItem
        '
        Me.lblItem.AutoSize = True
        Me.lblItem.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblItem.Location = New System.Drawing.Point(1050, 65)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(75, 19)
        Me.lblItem.TabIndex = 3
        Me.lblItem.Text = "بند الموازنة"
        '
        'lblAccount
        '
        Me.lblAccount.AutoSize = True
        Me.lblAccount.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblAccount.Location = New System.Drawing.Point(1050, 25)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.Size = New System.Drawing.Size(55, 19)
        Me.lblAccount.TabIndex = 4
        Me.lblAccount.Text = "الحساب"
        '
        'cmbAccounts
        '
        Me.cmbAccounts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAccounts.Location = New System.Drawing.Point(-500, -500)
        Me.cmbAccounts.Name = "cmbAccounts"
        Me.cmbAccounts.Size = New System.Drawing.Size(330, 21)
        Me.cmbAccounts.TabIndex = 2
        Me.cmbAccounts.TabStop = False
        Me.cmbAccounts.Visible = False
        '
        'pnlActions
        '
        Me.pnlActions.BackColor = System.Drawing.Color.White
        Me.pnlActions.Controls.Add(Me.btnExit)
        Me.pnlActions.Controls.Add(Me.btnRefresh)
        Me.pnlActions.Controls.Add(Me.btnDelete)
        Me.pnlActions.Controls.Add(Me.btnSave)
        Me.pnlActions.Controls.Add(Me.btnNew)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlActions.Location = New System.Drawing.Point(0, 623)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Size = New System.Drawing.Size(1200, 55)
        Me.pnlActions.TabIndex = 1
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(570, 10)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(110, 36)
        Me.btnExit.TabIndex = 0
        Me.btnExit.Text = "خروج"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(690, 10)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(110, 36)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "تحديث"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(810, 10)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(110, 36)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "حذف"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(930, 10)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(110, 36)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "حفظ"
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(1050, 10)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(110, 36)
        Me.btnNew.TabIndex = 4
        Me.btnNew.Text = "جديد"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 678)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1200, 22)
        Me.StatusStrip1.TabIndex = 3
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(30, 17)
        Me.lblStatus.Text = "جاهز"
        '
        'FrmAccountBudgetMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1200, 700)
        Me.Controls.Add(Me.pnlContent)
        Me.Controls.Add(Me.pnlActions)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.MinimumSize = New System.Drawing.Size(1050, 650)
        Me.Name = "FrmAccountBudgetMapping"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ربط الحسابات ببنود الموازنة"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlContent.ResumeLayout(False)
        Me.cardGrid.ResumeLayout(False)
        CType(Me.dgvMapping, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cardForm.ResumeLayout(False)
        Me.cardForm.PerformLayout()
        Me.pnlActions.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubTitle As Label
    Friend WithEvents pnlContent As Panel
    Friend WithEvents cardForm As Panel
    Friend WithEvents cmbAccounts As ComboBox
    Friend WithEvents txtAccountCode As TextBox
    Friend WithEvents btnPickAccount As Button
    Friend WithEvents txtAccountName As TextBox
    Friend WithEvents lblSearch As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblTotalItemsStat As Label
    Friend WithEvents lblLinkedItemsStat As Label
    Friend WithEvents lblUnlinkedItemsStat As Label
    Friend WithEvents cmbItems As ComboBox
    Friend WithEvents chkIsDefault As CheckBox
    Friend WithEvents lblAccount As Label
    Friend WithEvents lblItem As Label
    Friend WithEvents cardGrid As Panel
    Friend WithEvents dgvMapping As DataGridView
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnExit As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel

End Class
