<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBudgetChapters
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
        Me.dgvChapters = New System.Windows.Forms.DataGridView()
        Me.cardForm = New System.Windows.Forms.Panel()
        Me.chkIsActive = New System.Windows.Forms.CheckBox()
        Me.txtChapterName = New System.Windows.Forms.TextBox()
        Me.txtChapterCode = New System.Windows.Forms.TextBox()
        Me.cmbDoors = New System.Windows.Forms.ComboBox()
        Me.lblChapterName = New System.Windows.Forms.Label()
        Me.lblChapterCode = New System.Windows.Forms.Label()
        Me.lblDoor = New System.Windows.Forms.Label()
        Me.pnlActions = New System.Windows.Forms.Panel()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.exit_Btn = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.cardGrid.SuspendLayout()
        CType(Me.dgvChapters, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlHeader.Size = New System.Drawing.Size(1150, 70)
        Me.pnlHeader.TabIndex = 2
        '
        'lblSubTitle
        '
        Me.lblSubTitle.AutoSize = True
        Me.lblSubTitle.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblSubTitle.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblSubTitle.Location = New System.Drawing.Point(760, 42)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Size = New System.Drawing.Size(212, 17)
        Me.lblSubTitle.TabIndex = 0
        Me.lblSubTitle.Text = "تعريف فصول الموازنة وربطها بالأبواب"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(960, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(132, 30)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "إدارة الفصول"
        '
        'pnlContent
        '
        Me.pnlContent.Controls.Add(Me.cardGrid)
        Me.pnlContent.Controls.Add(Me.cardForm)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 70)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlContent.Size = New System.Drawing.Size(1150, 533)
        Me.pnlContent.TabIndex = 0
        '
        'cardGrid
        '
        Me.cardGrid.BackColor = System.Drawing.Color.White
        Me.cardGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardGrid.Controls.Add(Me.dgvChapters)
        Me.cardGrid.Location = New System.Drawing.Point(15, 200)
        Me.cardGrid.Name = "cardGrid"
        Me.cardGrid.Size = New System.Drawing.Size(1120, 400)
        Me.cardGrid.TabIndex = 0
        '
        'dgvChapters
        '
        Me.dgvChapters.AllowUserToAddRows = False
        Me.dgvChapters.AllowUserToDeleteRows = False
        Me.dgvChapters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvChapters.BackgroundColor = System.Drawing.Color.White
        Me.dgvChapters.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvChapters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvChapters.Location = New System.Drawing.Point(0, 0)
        Me.dgvChapters.MultiSelect = False
        Me.dgvChapters.Name = "dgvChapters"
        Me.dgvChapters.ReadOnly = True
        Me.dgvChapters.RowHeadersVisible = False
        Me.dgvChapters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvChapters.Size = New System.Drawing.Size(1118, 398)
        Me.dgvChapters.TabIndex = 0
        '
        'cardForm
        '
        Me.cardForm.BackColor = System.Drawing.Color.White
        Me.cardForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardForm.Controls.Add(Me.chkIsActive)
        Me.cardForm.Controls.Add(Me.txtChapterName)
        Me.cardForm.Controls.Add(Me.txtChapterCode)
        Me.cardForm.Controls.Add(Me.cmbDoors)
        Me.cardForm.Controls.Add(Me.lblChapterName)
        Me.cardForm.Controls.Add(Me.lblChapterCode)
        Me.cardForm.Controls.Add(Me.lblDoor)
        Me.cardForm.Location = New System.Drawing.Point(15, 15)
        Me.cardForm.Name = "cardForm"
        Me.cardForm.Size = New System.Drawing.Size(1120, 170)
        Me.cardForm.TabIndex = 1
        '
        'chkIsActive
        '
        Me.chkIsActive.AutoSize = True
        Me.chkIsActive.Checked = True
        Me.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIsActive.Location = New System.Drawing.Point(740, 135)
        Me.chkIsActive.Name = "chkIsActive"
        Me.chkIsActive.Size = New System.Drawing.Size(53, 21)
        Me.chkIsActive.TabIndex = 0
        Me.chkIsActive.Text = "نشط"
        '
        'txtChapterName
        '
        Me.txtChapterName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChapterName.Location = New System.Drawing.Point(740, 98)
        Me.txtChapterName.Name = "txtChapterName"
        Me.txtChapterName.Size = New System.Drawing.Size(250, 25)
        Me.txtChapterName.TabIndex = 1
        '
        'txtChapterCode
        '
        Me.txtChapterCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChapterCode.Location = New System.Drawing.Point(740, 58)
        Me.txtChapterCode.Name = "txtChapterCode"
        Me.txtChapterCode.Size = New System.Drawing.Size(250, 25)
        Me.txtChapterCode.TabIndex = 2
        '
        'cmbDoors
        '
        Me.cmbDoors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoors.Location = New System.Drawing.Point(740, 18)
        Me.cmbDoors.Name = "cmbDoors"
        Me.cmbDoors.Size = New System.Drawing.Size(250, 25)
        Me.cmbDoors.TabIndex = 3
        '
        'lblChapterName
        '
        Me.lblChapterName.AutoSize = True
        Me.lblChapterName.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblChapterName.Location = New System.Drawing.Point(1010, 100)
        Me.lblChapterName.Name = "lblChapterName"
        Me.lblChapterName.Size = New System.Drawing.Size(76, 19)
        Me.lblChapterName.TabIndex = 4
        Me.lblChapterName.Text = "اسم الفصل"
        '
        'lblChapterCode
        '
        Me.lblChapterCode.AutoSize = True
        Me.lblChapterCode.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblChapterCode.Location = New System.Drawing.Point(1010, 60)
        Me.lblChapterCode.Name = "lblChapterCode"
        Me.lblChapterCode.Size = New System.Drawing.Size(73, 19)
        Me.lblChapterCode.TabIndex = 5
        Me.lblChapterCode.Text = "كود الفصل"
        '
        'lblDoor
        '
        Me.lblDoor.AutoSize = True
        Me.lblDoor.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblDoor.Location = New System.Drawing.Point(1010, 20)
        Me.lblDoor.Name = "lblDoor"
        Me.lblDoor.Size = New System.Drawing.Size(40, 19)
        Me.lblDoor.TabIndex = 6
        Me.lblDoor.Text = "الباب"
        '
        'pnlActions
        '
        Me.pnlActions.BackColor = System.Drawing.Color.White
        Me.pnlActions.Controls.Add(Me.exit_Btn)
        Me.pnlActions.Controls.Add(Me.btnRefresh)
        Me.pnlActions.Controls.Add(Me.btnDelete)
        Me.pnlActions.Controls.Add(Me.btnSave)
        Me.pnlActions.Controls.Add(Me.btnNew)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlActions.Location = New System.Drawing.Point(0, 603)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Size = New System.Drawing.Size(1150, 55)
        Me.pnlActions.TabIndex = 1
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(620, 10)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(110, 36)
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.Text = "تحديث"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(740, 10)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(110, 36)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "حذف"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(860, 10)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(110, 36)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "حفظ"
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(980, 10)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(110, 36)
        Me.btnNew.TabIndex = 3
        Me.btnNew.Text = "جديد"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 658)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1150, 22)
        Me.StatusStrip1.TabIndex = 3
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(30, 17)
        Me.lblStatus.Text = "جاهز"
        '
        'exit_Btn
        '
        Me.exit_Btn.Location = New System.Drawing.Point(514, 10)
        Me.exit_Btn.Name = "exit_Btn"
        Me.exit_Btn.Size = New System.Drawing.Size(100, 35)
        Me.exit_Btn.TabIndex = 5
        Me.exit_Btn.Text = "خروج"
        '
        'FrmBudgetChapters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1150, 680)
        Me.Controls.Add(Me.pnlContent)
        Me.Controls.Add(Me.pnlActions)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.MaximizeBox = True
        Me.MinimizeBox = True
        Me.MinimumSize = New System.Drawing.Size(1050, 650)
        Me.Name = "FrmBudgetChapters"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إدارة الفصول"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlContent.ResumeLayout(False)
        Me.cardGrid.ResumeLayout(False)
        CType(Me.dgvChapters, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtChapterName As TextBox
    Friend WithEvents txtChapterCode As TextBox
    Friend WithEvents chkIsActive As CheckBox
    Friend WithEvents lblDoor As Label
    Friend WithEvents lblChapterName As Label
    Friend WithEvents lblChapterCode As Label
    Friend WithEvents cardGrid As Panel
    Friend WithEvents dgvChapters As DataGridView
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel
    Friend WithEvents exit_Btn As Button
End Class
