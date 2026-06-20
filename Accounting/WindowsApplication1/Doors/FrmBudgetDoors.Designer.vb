<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBudgetDoors
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Me.dgvDoors = New System.Windows.Forms.DataGridView()
        Me.cardForm = New System.Windows.Forms.Panel()
        Me.chkIsActive = New System.Windows.Forms.CheckBox()
        Me.txtDoorName = New System.Windows.Forms.TextBox()
        Me.txtDoorCode = New System.Windows.Forms.TextBox()
        Me.lblDoorName = New System.Windows.Forms.Label()
        Me.lblDoorCode = New System.Windows.Forms.Label()
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
        CType(Me.dgvDoors, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pnlHeader.Size = New System.Drawing.Size(1100, 70)
        Me.pnlHeader.TabIndex = 2
        '
        'lblSubTitle
        '
        Me.lblSubTitle.AutoSize = True
        Me.lblSubTitle.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblSubTitle.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblSubTitle.Location = New System.Drawing.Point(820, 42)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Size = New System.Drawing.Size(207, 17)
        Me.lblSubTitle.TabIndex = 0
        Me.lblSubTitle.Text = "إعداد وتصنيف الأبواب المالية للموازنة"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(950, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(123, 30)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "إدارة الأبواب"
        '
        'pnlContent
        '
        Me.pnlContent.Controls.Add(Me.cardGrid)
        Me.pnlContent.Controls.Add(Me.cardForm)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 70)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlContent.Size = New System.Drawing.Size(1100, 503)
        Me.pnlContent.TabIndex = 0
        '
        'cardGrid
        '
        Me.cardGrid.BackColor = System.Drawing.Color.White
        Me.cardGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardGrid.Controls.Add(Me.dgvDoors)
        Me.cardGrid.Location = New System.Drawing.Point(15, 180)
        Me.cardGrid.Name = "cardGrid"
        Me.cardGrid.Size = New System.Drawing.Size(1070, 380)
        Me.cardGrid.TabIndex = 0
        '
        'dgvDoors
        '
        Me.dgvDoors.AllowUserToAddRows = False
        Me.dgvDoors.AllowUserToDeleteRows = False
        Me.dgvDoors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvDoors.BackgroundColor = System.Drawing.Color.White
        Me.dgvDoors.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvDoors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDoors.Location = New System.Drawing.Point(0, 0)
        Me.dgvDoors.MultiSelect = False
        Me.dgvDoors.Name = "dgvDoors"
        Me.dgvDoors.ReadOnly = True
        Me.dgvDoors.RowHeadersVisible = False
        Me.dgvDoors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDoors.Size = New System.Drawing.Size(1068, 378)
        Me.dgvDoors.TabIndex = 0
        '
        'cardForm
        '
        Me.cardForm.BackColor = System.Drawing.Color.White
        Me.cardForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.cardForm.Controls.Add(Me.chkIsActive)
        Me.cardForm.Controls.Add(Me.txtDoorName)
        Me.cardForm.Controls.Add(Me.txtDoorCode)
        Me.cardForm.Controls.Add(Me.lblDoorName)
        Me.cardForm.Controls.Add(Me.lblDoorCode)
        Me.cardForm.Location = New System.Drawing.Point(15, 15)
        Me.cardForm.Name = "cardForm"
        Me.cardForm.Size = New System.Drawing.Size(1070, 150)
        Me.cardForm.TabIndex = 1
        '
        'chkIsActive
        '
        Me.chkIsActive.AutoSize = True
        Me.chkIsActive.Checked = True
        Me.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIsActive.Location = New System.Drawing.Point(700, 105)
        Me.chkIsActive.Name = "chkIsActive"
        Me.chkIsActive.Size = New System.Drawing.Size(53, 21)
        Me.chkIsActive.TabIndex = 0
        Me.chkIsActive.Text = "نشط"
        '
        'txtDoorName
        '
        Me.txtDoorName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDoorName.Location = New System.Drawing.Point(700, 62)
        Me.txtDoorName.Name = "txtDoorName"
        Me.txtDoorName.Size = New System.Drawing.Size(230, 25)
        Me.txtDoorName.TabIndex = 1
        '
        'txtDoorCode
        '
        Me.txtDoorCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDoorCode.Location = New System.Drawing.Point(700, 22)
        Me.txtDoorCode.Name = "txtDoorCode"
        Me.txtDoorCode.Size = New System.Drawing.Size(230, 25)
        Me.txtDoorCode.TabIndex = 2
        '
        'lblDoorName
        '
        Me.lblDoorName.AutoSize = True
        Me.lblDoorName.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblDoorName.Location = New System.Drawing.Point(950, 65)
        Me.lblDoorName.Name = "lblDoorName"
        Me.lblDoorName.Size = New System.Drawing.Size(68, 19)
        Me.lblDoorName.TabIndex = 3
        Me.lblDoorName.Text = "اسم الباب"
        '
        'lblDoorCode
        '
        Me.lblDoorCode.AutoSize = True
        Me.lblDoorCode.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblDoorCode.Location = New System.Drawing.Point(950, 25)
        Me.lblDoorCode.Name = "lblDoorCode"
        Me.lblDoorCode.Size = New System.Drawing.Size(65, 19)
        Me.lblDoorCode.TabIndex = 4
        Me.lblDoorCode.Text = "كود الباب"
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
        Me.pnlActions.Location = New System.Drawing.Point(0, 573)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Size = New System.Drawing.Size(1100, 55)
        Me.pnlActions.TabIndex = 1
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(620, 10)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 35)
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.Text = "تحديث"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(730, 10)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(100, 35)
        Me.btnDelete.TabIndex = 1
        Me.btnDelete.Text = "حذف"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(840, 10)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 35)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "حفظ"
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(950, 10)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(100, 35)
        Me.btnNew.TabIndex = 3
        Me.btnNew.Text = "جديد"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 628)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1100, 22)
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
        Me.exit_Btn.TabIndex = 4
        Me.exit_Btn.Text = "خروج"
        '
        'FrmBudgetDoors
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1100, 650)
        Me.Controls.Add(Me.pnlContent)
        Me.Controls.Add(Me.pnlActions)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.MaximizeBox = True
        Me.MinimizeBox = True
        Me.MinimumSize = New System.Drawing.Size(1050, 650)
        Me.Name = "FrmBudgetDoors"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إدارة الأبواب"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlContent.ResumeLayout(False)
        Me.cardGrid.ResumeLayout(False)
        CType(Me.dgvDoors, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents chkIsActive As CheckBox
    Friend WithEvents txtDoorName As TextBox
    Friend WithEvents txtDoorCode As TextBox
    Friend WithEvents lblDoorName As Label
    Friend WithEvents lblDoorCode As Label
    Friend WithEvents cardGrid As Panel
    Friend WithEvents dgvDoors As DataGridView
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel
    Friend WithEvents exit_Btn As Button
End Class
