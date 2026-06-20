<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBudgetItems
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
        Me.dgvItems = New System.Windows.Forms.DataGridView()
        Me.cardForm = New System.Windows.Forms.Panel()
        Me.chkIsActive = New System.Windows.Forms.CheckBox()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.cmbChapters = New System.Windows.Forms.ComboBox()
        Me.cmbDoors = New System.Windows.Forms.ComboBox()
        Me.lblItemName = New System.Windows.Forms.Label()
        Me.lblItemCode = New System.Windows.Forms.Label()
        Me.lblChapter = New System.Windows.Forms.Label()
        Me.lblDoor = New System.Windows.Forms.Label()
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
        CType(Me.dgvItems, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.lblSubTitle.Location = New System.Drawing.Point(760, 42)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Size = New System.Drawing.Size(205, 17)
        Me.lblSubTitle.TabIndex = 0
        Me.lblSubTitle.Text = "تعريف بنود الموازنة وربطها بالفصول"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(1020, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(109, 30)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "إدارة البنود"
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
        Me.cardGrid.Controls.Add(Me.dgvItems)
        Me.cardGrid.Location = New System.Drawing.Point(15, 215)
        Me.cardGrid.Name = "cardGrid"
        Me.cardGrid.Size = New System.Drawing.Size(1170, 400)
        Me.cardGrid.TabIndex = 0
        '
        'dgvItems
        '
        Me.dgvItems.AllowUserToAddRows = False
        Me.dgvItems.AllowUserToDeleteRows = False
        Me.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvItems.BackgroundColor = System.Drawing.Color.White
        Me.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvItems.Location = New System.Drawing.Point(0, 0)
        Me.dgvItems.MultiSelect = False
        Me.dgvItems.Name = "dgvItems"
        Me.dgvItems.ReadOnly = True
        Me.dgvItems.RowHeadersVisible = False
        Me.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvItems.Size = New System.Drawing.Size(1168, 398)
        Me.dgvItems.TabIndex = 0
        '
        'cardForm
        '
        Me.cardForm.BackColor = System.Drawing.Color.White
        Me.cardForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardForm.Controls.Add(Me.chkIsActive)
        Me.cardForm.Controls.Add(Me.txtItemName)
        Me.cardForm.Controls.Add(Me.txtItemCode)
        Me.cardForm.Controls.Add(Me.cmbChapters)
        Me.cardForm.Controls.Add(Me.cmbDoors)
        Me.cardForm.Controls.Add(Me.lblItemName)
        Me.cardForm.Controls.Add(Me.lblItemCode)
        Me.cardForm.Controls.Add(Me.lblChapter)
        Me.cardForm.Controls.Add(Me.lblDoor)
        Me.cardForm.Location = New System.Drawing.Point(15, 15)
        Me.cardForm.Name = "cardForm"
        Me.cardForm.Size = New System.Drawing.Size(1170, 190)
        Me.cardForm.TabIndex = 1
        '
        'chkIsActive
        '
        Me.chkIsActive.AutoSize = True
        Me.chkIsActive.Checked = True
        Me.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIsActive.Location = New System.Drawing.Point(707, 142)
        Me.chkIsActive.Name = "chkIsActive"
        Me.chkIsActive.Size = New System.Drawing.Size(53, 21)
        Me.chkIsActive.TabIndex = 0
        Me.chkIsActive.Text = "نشط"
        '
        'txtItemName
        '
        Me.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemName.Location = New System.Drawing.Point(780, 138)
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.Size = New System.Drawing.Size(250, 25)
        Me.txtItemName.TabIndex = 1
        '
        'txtItemCode
        '
        Me.txtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemCode.Location = New System.Drawing.Point(780, 98)
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.Size = New System.Drawing.Size(250, 25)
        Me.txtItemCode.TabIndex = 2
        '
        'cmbChapters
        '
        Me.cmbChapters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChapters.Location = New System.Drawing.Point(780, 58)
        Me.cmbChapters.Name = "cmbChapters"
        Me.cmbChapters.Size = New System.Drawing.Size(250, 25)
        Me.cmbChapters.TabIndex = 3
        '
        'cmbDoors
        '
        Me.cmbDoors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoors.Location = New System.Drawing.Point(780, 18)
        Me.cmbDoors.Name = "cmbDoors"
        Me.cmbDoors.Size = New System.Drawing.Size(250, 25)
        Me.cmbDoors.TabIndex = 4
        '
        'lblItemName
        '
        Me.lblItemName.AutoSize = True
        Me.lblItemName.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblItemName.Location = New System.Drawing.Point(1050, 140)
        Me.lblItemName.Name = "lblItemName"
        Me.lblItemName.Size = New System.Drawing.Size(63, 19)
        Me.lblItemName.TabIndex = 5
        Me.lblItemName.Text = "اسم البند"
        '
        'lblItemCode
        '
        Me.lblItemCode.AutoSize = True
        Me.lblItemCode.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblItemCode.Location = New System.Drawing.Point(1050, 100)
        Me.lblItemCode.Name = "lblItemCode"
        Me.lblItemCode.Size = New System.Drawing.Size(60, 19)
        Me.lblItemCode.TabIndex = 6
        Me.lblItemCode.Text = "كود البند"
        '
        'lblChapter
        '
        Me.lblChapter.AutoSize = True
        Me.lblChapter.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblChapter.Location = New System.Drawing.Point(1050, 60)
        Me.lblChapter.Name = "lblChapter"
        Me.lblChapter.Size = New System.Drawing.Size(48, 19)
        Me.lblChapter.TabIndex = 7
        Me.lblChapter.Text = "الفصل"
        '
        'lblDoor
        '
        Me.lblDoor.AutoSize = True
        Me.lblDoor.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblDoor.Location = New System.Drawing.Point(1050, 20)
        Me.lblDoor.Name = "lblDoor"
        Me.lblDoor.Size = New System.Drawing.Size(40, 19)
        Me.lblDoor.TabIndex = 8
        Me.lblDoor.Text = "الباب"
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
        'FrmBudgetItems
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
        Me.Name = "FrmBudgetItems"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إدارة البنود"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlContent.ResumeLayout(False)
        Me.cardGrid.ResumeLayout(False)
        CType(Me.dgvItems, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents cmbDoors As ComboBox
    Friend WithEvents cmbChapters As ComboBox
    Friend WithEvents txtItemName As TextBox
    Friend WithEvents txtItemCode As TextBox
    Friend WithEvents chkIsActive As CheckBox
    Friend WithEvents lblDoor As Label
    Friend WithEvents lblChapter As Label
    Friend WithEvents lblItemName As Label
    Friend WithEvents lblItemCode As Label
    Friend WithEvents cardGrid As Panel
    Friend WithEvents dgvItems As DataGridView
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnExit As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel

End Class
