<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FRM_ACC_ENTITY_RULES_MANAGER
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
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSubTitle = New System.Windows.Forms.Label()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.chkOnlyIssues = New System.Windows.Forms.CheckBox()
        Me.chkOnlyActive = New System.Windows.Forms.CheckBox()
        Me.dgvRules = New System.Windows.Forms.DataGridView()
        Me.pnlEdit = New System.Windows.Forms.Panel()
        Me.grpEdit = New System.Windows.Forms.GroupBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnValidate = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.chkIsActive = New System.Windows.Forms.CheckBox()
        Me.chkAllowDeleteIfNoMove = New System.Windows.Forms.CheckBox()
        Me.chkAllowChangeParent = New System.Windows.Forms.CheckBox()
        Me.chkAllowRename = New System.Windows.Forms.CheckBox()
        Me.chkAutoCreate = New System.Windows.Forms.CheckBox()
        Me.btnSelectParent = New System.Windows.Forms.Button()
        Me.txtParentAccName = New System.Windows.Forms.TextBox()
        Me.txtParentAccCode = New System.Windows.Forms.TextBox()
        Me.lblParentAcc = New System.Windows.Forms.Label()
        Me.txtSourceTable = New System.Windows.Forms.TextBox()
        Me.lblSourceTable = New System.Windows.Forms.Label()
        Me.txtEntityName = New System.Windows.Forms.TextBox()
        Me.lblEntityName = New System.Windows.Forms.Label()
        Me.txtEntityType = New System.Windows.Forms.TextBox()
        Me.lblEntityType = New System.Windows.Forms.Label()
        Me.txtRuleID = New System.Windows.Forms.TextBox()
        Me.lblRuleID = New System.Windows.Forms.Label()
        Me.statusStripMain = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlHeader.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        CType(Me.dgvRules, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlEdit.SuspendLayout()
        Me.grpEdit.SuspendLayout()
        Me.statusStripMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.lblSubTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1180, 72)
        Me.pnlHeader.TabIndex = 3
        '
        'lblTitle
        '
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Padding = New System.Windows.Forms.Padding(0, 6, 16, 0)
        Me.lblTitle.Size = New System.Drawing.Size(1180, 38)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "إعدادات قواعد الربط المحاسبي"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSubTitle
        '
        Me.lblSubTitle.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblSubTitle.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblSubTitle.Location = New System.Drawing.Point(0, 49)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Padding = New System.Windows.Forms.Padding(0, 0, 16, 8)
        Me.lblSubTitle.Size = New System.Drawing.Size(1180, 23)
        Me.lblSubTitle.TabIndex = 1
        Me.lblSubTitle.Text = "تحديد الحساب الأب لكل نوع من أنواع الكيانات المرتبطة بالدليل المحاسبي"
        Me.lblSubTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlTop.Controls.Add(Me.btnRefresh)
        Me.pnlTop.Controls.Add(Me.chkOnlyIssues)
        Me.pnlTop.Controls.Add(Me.chkOnlyActive)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 72)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlTop.Size = New System.Drawing.Size(1180, 58)
        Me.pnlTop.TabIndex = 2
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(25, 14)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(130, 30)
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.Text = "تحديث"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'chkOnlyIssues
        '
        Me.chkOnlyIssues.Location = New System.Drawing.Point(830, 18)
        Me.chkOnlyIssues.Name = "chkOnlyIssues"
        Me.chkOnlyIssues.Size = New System.Drawing.Size(160, 24)
        Me.chkOnlyIssues.TabIndex = 1
        Me.chkOnlyIssues.Text = "عرض المشاكل فقط"
        Me.chkOnlyIssues.UseVisualStyleBackColor = True
        '
        'chkOnlyActive
        '
        Me.chkOnlyActive.Location = New System.Drawing.Point(1000, 18)
        Me.chkOnlyActive.Name = "chkOnlyActive"
        Me.chkOnlyActive.Size = New System.Drawing.Size(150, 24)
        Me.chkOnlyActive.TabIndex = 2
        Me.chkOnlyActive.Text = "القواعد المفعلة فقط"
        Me.chkOnlyActive.UseVisualStyleBackColor = True
        '
        'dgvRules
        '
        Me.dgvRules.AllowUserToAddRows = False
        Me.dgvRules.AllowUserToDeleteRows = False
        Me.dgvRules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgvRules.BackgroundColor = System.Drawing.Color.White
        Me.dgvRules.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvRules.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvRules.Location = New System.Drawing.Point(0, 130)
        Me.dgvRules.MultiSelect = False
        Me.dgvRules.Name = "dgvRules"
        Me.dgvRules.ReadOnly = True
        Me.dgvRules.RowHeadersVisible = False
        Me.dgvRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRules.Size = New System.Drawing.Size(790, 568)
        Me.dgvRules.TabIndex = 0
        '
        'pnlEdit
        '
        Me.pnlEdit.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlEdit.Controls.Add(Me.grpEdit)
        Me.pnlEdit.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlEdit.Location = New System.Drawing.Point(790, 130)
        Me.pnlEdit.Name = "pnlEdit"
        Me.pnlEdit.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlEdit.Size = New System.Drawing.Size(390, 568)
        Me.pnlEdit.TabIndex = 1
        '
        'grpEdit
        '
        Me.grpEdit.Controls.Add(Me.btnClose)
        Me.grpEdit.Controls.Add(Me.btnValidate)
        Me.grpEdit.Controls.Add(Me.btnSave)
        Me.grpEdit.Controls.Add(Me.chkIsActive)
        Me.grpEdit.Controls.Add(Me.chkAllowDeleteIfNoMove)
        Me.grpEdit.Controls.Add(Me.chkAllowChangeParent)
        Me.grpEdit.Controls.Add(Me.chkAllowRename)
        Me.grpEdit.Controls.Add(Me.chkAutoCreate)
        Me.grpEdit.Controls.Add(Me.btnSelectParent)
        Me.grpEdit.Controls.Add(Me.txtParentAccName)
        Me.grpEdit.Controls.Add(Me.txtParentAccCode)
        Me.grpEdit.Controls.Add(Me.lblParentAcc)
        Me.grpEdit.Controls.Add(Me.txtSourceTable)
        Me.grpEdit.Controls.Add(Me.lblSourceTable)
        Me.grpEdit.Controls.Add(Me.txtEntityName)
        Me.grpEdit.Controls.Add(Me.lblEntityName)
        Me.grpEdit.Controls.Add(Me.txtEntityType)
        Me.grpEdit.Controls.Add(Me.lblEntityType)
        Me.grpEdit.Controls.Add(Me.txtRuleID)
        Me.grpEdit.Controls.Add(Me.lblRuleID)
        Me.grpEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpEdit.Location = New System.Drawing.Point(10, 10)
        Me.grpEdit.Name = "grpEdit"
        Me.grpEdit.Size = New System.Drawing.Size(370, 548)
        Me.grpEdit.TabIndex = 0
        Me.grpEdit.TabStop = False
        Me.grpEdit.Text = "بيانات القاعدة"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(20, 465)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(95, 34)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "إغلاق"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnValidate
        '
        Me.btnValidate.Location = New System.Drawing.Point(125, 465)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(110, 34)
        Me.btnValidate.TabIndex = 1
        Me.btnValidate.Text = "فحص"
        Me.btnValidate.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(245, 465)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(120, 34)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "حفظ"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'chkIsActive
        '
        Me.chkIsActive.Location = New System.Drawing.Point(20, 402)
        Me.chkIsActive.Name = "chkIsActive"
        Me.chkIsActive.Size = New System.Drawing.Size(260, 24)
        Me.chkIsActive.TabIndex = 3
        Me.chkIsActive.Text = "القاعدة مفعلة"
        Me.chkIsActive.UseVisualStyleBackColor = True
        '
        'chkAllowDeleteIfNoMove
        '
        Me.chkAllowDeleteIfNoMove.Location = New System.Drawing.Point(20, 369)
        Me.chkAllowDeleteIfNoMove.Name = "chkAllowDeleteIfNoMove"
        Me.chkAllowDeleteIfNoMove.Size = New System.Drawing.Size(300, 24)
        Me.chkAllowDeleteIfNoMove.TabIndex = 4
        Me.chkAllowDeleteIfNoMove.Text = "السماح بالحذف إذا لا توجد حركة"
        Me.chkAllowDeleteIfNoMove.UseVisualStyleBackColor = True
        '
        'chkAllowChangeParent
        '
        Me.chkAllowChangeParent.Location = New System.Drawing.Point(20, 336)
        Me.chkAllowChangeParent.Name = "chkAllowChangeParent"
        Me.chkAllowChangeParent.Size = New System.Drawing.Size(260, 24)
        Me.chkAllowChangeParent.TabIndex = 5
        Me.chkAllowChangeParent.Text = "السماح بتغيير الحساب الأب"
        Me.chkAllowChangeParent.UseVisualStyleBackColor = True
        '
        'chkAllowRename
        '
        Me.chkAllowRename.Location = New System.Drawing.Point(20, 303)
        Me.chkAllowRename.Name = "chkAllowRename"
        Me.chkAllowRename.Size = New System.Drawing.Size(260, 24)
        Me.chkAllowRename.TabIndex = 6
        Me.chkAllowRename.Text = "السماح بمزامنة الاسم"
        Me.chkAllowRename.UseVisualStyleBackColor = True
        '
        'chkAutoCreate
        '
        Me.chkAutoCreate.Location = New System.Drawing.Point(20, 270)
        Me.chkAutoCreate.Name = "chkAutoCreate"
        Me.chkAutoCreate.Size = New System.Drawing.Size(260, 24)
        Me.chkAutoCreate.TabIndex = 7
        Me.chkAutoCreate.Text = "فتح الحساب تلقائيًا"
        Me.chkAutoCreate.UseVisualStyleBackColor = True
        '
        'btnSelectParent
        '
        Me.btnSelectParent.Location = New System.Drawing.Point(20, 186)
        Me.btnSelectParent.Name = "btnSelectParent"
        Me.btnSelectParent.Size = New System.Drawing.Size(115, 28)
        Me.btnSelectParent.TabIndex = 8
        Me.btnSelectParent.Text = "اختيار"
        Me.btnSelectParent.UseVisualStyleBackColor = True
        '
        'txtParentAccName
        '
        Me.txtParentAccName.Location = New System.Drawing.Point(20, 222)
        Me.txtParentAccName.Name = "txtParentAccName"
        Me.txtParentAccName.ReadOnly = True
        Me.txtParentAccName.Size = New System.Drawing.Size(255, 23)
        Me.txtParentAccName.TabIndex = 9
        '
        'txtParentAccCode
        '
        Me.txtParentAccCode.Location = New System.Drawing.Point(145, 188)
        Me.txtParentAccCode.Name = "txtParentAccCode"
        Me.txtParentAccCode.Size = New System.Drawing.Size(130, 23)
        Me.txtParentAccCode.TabIndex = 10
        '
        'lblParentAcc
        '
        Me.lblParentAcc.Location = New System.Drawing.Point(285, 188)
        Me.lblParentAcc.Name = "lblParentAcc"
        Me.lblParentAcc.Size = New System.Drawing.Size(80, 24)
        Me.lblParentAcc.TabIndex = 11
        Me.lblParentAcc.Text = "الأب:"
        Me.lblParentAcc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSourceTable
        '
        Me.txtSourceTable.Location = New System.Drawing.Point(20, 146)
        Me.txtSourceTable.Name = "txtSourceTable"
        Me.txtSourceTable.ReadOnly = True
        Me.txtSourceTable.Size = New System.Drawing.Size(255, 23)
        Me.txtSourceTable.TabIndex = 12
        '
        'lblSourceTable
        '
        Me.lblSourceTable.Location = New System.Drawing.Point(285, 146)
        Me.lblSourceTable.Name = "lblSourceTable"
        Me.lblSourceTable.Size = New System.Drawing.Size(80, 24)
        Me.lblSourceTable.TabIndex = 13
        Me.lblSourceTable.Text = "الجدول:"
        Me.lblSourceTable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtEntityName
        '
        Me.txtEntityName.Location = New System.Drawing.Point(20, 109)
        Me.txtEntityName.Name = "txtEntityName"
        Me.txtEntityName.ReadOnly = True
        Me.txtEntityName.Size = New System.Drawing.Size(255, 23)
        Me.txtEntityName.TabIndex = 14
        '
        'lblEntityName
        '
        Me.lblEntityName.Location = New System.Drawing.Point(285, 109)
        Me.lblEntityName.Name = "lblEntityName"
        Me.lblEntityName.Size = New System.Drawing.Size(80, 24)
        Me.lblEntityName.TabIndex = 15
        Me.lblEntityName.Text = "النوع:"
        Me.lblEntityName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtEntityType
        '
        Me.txtEntityType.Location = New System.Drawing.Point(20, 72)
        Me.txtEntityType.Name = "txtEntityType"
        Me.txtEntityType.ReadOnly = True
        Me.txtEntityType.Size = New System.Drawing.Size(255, 23)
        Me.txtEntityType.TabIndex = 16
        '
        'lblEntityType
        '
        Me.lblEntityType.Location = New System.Drawing.Point(285, 72)
        Me.lblEntityType.Name = "lblEntityType"
        Me.lblEntityType.Size = New System.Drawing.Size(80, 24)
        Me.lblEntityType.TabIndex = 17
        Me.lblEntityType.Text = "رقم النوع:"
        Me.lblEntityType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRuleID
        '
        Me.txtRuleID.Location = New System.Drawing.Point(20, 35)
        Me.txtRuleID.Name = "txtRuleID"
        Me.txtRuleID.ReadOnly = True
        Me.txtRuleID.Size = New System.Drawing.Size(255, 23)
        Me.txtRuleID.TabIndex = 18
        '
        'lblRuleID
        '
        Me.lblRuleID.Location = New System.Drawing.Point(285, 35)
        Me.lblRuleID.Name = "lblRuleID"
        Me.lblRuleID.Size = New System.Drawing.Size(80, 24)
        Me.lblRuleID.TabIndex = 19
        Me.lblRuleID.Text = "رقم القاعدة:"
        Me.lblRuleID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'statusStripMain
        '
        Me.statusStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.statusStripMain.Location = New System.Drawing.Point(0, 698)
        Me.statusStripMain.Name = "statusStripMain"
        Me.statusStripMain.Size = New System.Drawing.Size(1180, 22)
        Me.statusStripMain.TabIndex = 4
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(1165, 17)
        Me.lblStatus.Spring = True
        Me.lblStatus.Text = "جاهز"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FRM_ACC_ENTITY_RULES_MANAGER
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1180, 720)
        Me.Controls.Add(Me.dgvRules)
        Me.Controls.Add(Me.pnlEdit)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.statusStripMain)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MinimumSize = New System.Drawing.Size(1050, 650)
        Me.Name = "FRM_ACC_ENTITY_RULES_MANAGER"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إعدادات قواعد الربط المحاسبي"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        CType(Me.dgvRules, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlEdit.ResumeLayout(False)
        Me.grpEdit.ResumeLayout(False)
        Me.grpEdit.PerformLayout()
        Me.statusStripMain.ResumeLayout(False)
        Me.statusStripMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubTitle As Label

    Friend WithEvents pnlTop As Panel
    Friend WithEvents chkOnlyActive As CheckBox
    Friend WithEvents chkOnlyIssues As CheckBox
    Friend WithEvents btnRefresh As Button

    Friend WithEvents dgvRules As DataGridView

    Friend WithEvents pnlEdit As Panel
    Friend WithEvents grpEdit As GroupBox

    Friend WithEvents lblRuleID As Label
    Friend WithEvents txtRuleID As TextBox

    Friend WithEvents lblEntityType As Label
    Friend WithEvents txtEntityType As TextBox

    Friend WithEvents lblEntityName As Label
    Friend WithEvents txtEntityName As TextBox

    Friend WithEvents lblSourceTable As Label
    Friend WithEvents txtSourceTable As TextBox

    Friend WithEvents lblParentAcc As Label
    Friend WithEvents txtParentAccCode As TextBox
    Friend WithEvents txtParentAccName As TextBox
    Friend WithEvents btnSelectParent As Button

    Friend WithEvents chkAutoCreate As CheckBox
    Friend WithEvents chkAllowRename As CheckBox
    Friend WithEvents chkAllowChangeParent As CheckBox
    Friend WithEvents chkAllowDeleteIfNoMove As CheckBox
    Friend WithEvents chkIsActive As CheckBox

    Friend WithEvents btnSave As Button
    Friend WithEvents btnValidate As Button
    Friend WithEvents btnClose As Button

    Friend WithEvents statusStripMain As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel

End Class