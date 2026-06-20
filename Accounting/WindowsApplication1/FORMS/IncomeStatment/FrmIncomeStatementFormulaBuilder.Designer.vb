<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmIncomeStatementFormulaBuilder
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
        Me.lblFormulaLine = New System.Windows.Forms.Label()
        Me.splitMain = New System.Windows.Forms.SplitContainer()
        Me.grpSources = New System.Windows.Forms.GroupBox()
        Me.dgvSourceLines = New System.Windows.Forms.DataGridView()
        Me.pnlSourceButtons = New System.Windows.Forms.Panel()
        Me.btnAddSubtract = New System.Windows.Forms.Button()
        Me.btnAddPlus = New System.Windows.Forms.Button()
        Me.grpFormulaDetails = New System.Windows.Forms.GroupBox()
        Me.dgvFormulaDetails = New System.Windows.Forms.DataGridView()
        Me.pnlDetailsButtons = New System.Windows.Forms.Panel()
        Me.btnMoveDown = New System.Windows.Forms.Button()
        Me.btnMoveUp = New System.Windows.Forms.Button()
        Me.btnRemoveDetail = New System.Windows.Forms.Button()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.txtFormulaText = New System.Windows.Forms.TextBox()
        Me.lblFormulaText = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pnlTop.SuspendLayout()
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMain.Panel1.SuspendLayout()
        Me.splitMain.Panel2.SuspendLayout()
        Me.splitMain.SuspendLayout()
        Me.grpSources.SuspendLayout()
        CType(Me.dgvSourceLines, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSourceButtons.SuspendLayout()
        Me.grpFormulaDetails.SuspendLayout()
        CType(Me.dgvFormulaDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDetailsButtons.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.statusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.lblFormulaLine)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlTop.Size = New System.Drawing.Size(1000, 60)
        Me.pnlTop.TabIndex = 0
        '
        'lblFormulaLine
        '
        Me.lblFormulaLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFormulaLine.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblFormulaLine.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblFormulaLine.Location = New System.Drawing.Point(10, 10)
        Me.lblFormulaLine.Name = "lblFormulaLine"
        Me.lblFormulaLine.Padding = New System.Windows.Forms.Padding(8)
        Me.lblFormulaLine.Size = New System.Drawing.Size(980, 40)
        Me.lblFormulaLine.TabIndex = 0
        Me.lblFormulaLine.Text = "بند المعادلة"
        Me.lblFormulaLine.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'splitMain
        '
        Me.splitMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitMain.Location = New System.Drawing.Point(0, 60)
        Me.splitMain.Name = "splitMain"
        '
        'splitMain.Panel1
        '
        Me.splitMain.Panel1.Controls.Add(Me.grpSources)
        Me.splitMain.Panel1.Controls.Add(Me.pnlSourceButtons)
        Me.splitMain.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        '
        'splitMain.Panel2
        '
        Me.splitMain.Panel2.Controls.Add(Me.grpFormulaDetails)
        Me.splitMain.Panel2.Controls.Add(Me.pnlDetailsButtons)
        Me.splitMain.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.splitMain.Size = New System.Drawing.Size(1000, 438)
        Me.splitMain.SplitterDistance = 478
        Me.splitMain.TabIndex = 1
        '
        'grpSources
        '
        Me.grpSources.Controls.Add(Me.dgvSourceLines)
        Me.grpSources.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpSources.Location = New System.Drawing.Point(0, 0)
        Me.grpSources.Name = "grpSources"
        Me.grpSources.Padding = New System.Windows.Forms.Padding(8)
        Me.grpSources.Size = New System.Drawing.Size(478, 390)
        Me.grpSources.TabIndex = 1
        Me.grpSources.TabStop = False
        Me.grpSources.Text = "البنود المتاحة"
        '
        'dgvSourceLines
        '
        Me.dgvSourceLines.AllowUserToAddRows = False
        Me.dgvSourceLines.AllowUserToDeleteRows = False
        Me.dgvSourceLines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvSourceLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSourceLines.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSourceLines.Location = New System.Drawing.Point(8, 23)
        Me.dgvSourceLines.MultiSelect = False
        Me.dgvSourceLines.Name = "dgvSourceLines"
        Me.dgvSourceLines.ReadOnly = True
        Me.dgvSourceLines.RowHeadersVisible = False
        Me.dgvSourceLines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSourceLines.Size = New System.Drawing.Size(462, 359)
        Me.dgvSourceLines.TabIndex = 0
        '
        'pnlSourceButtons
        '
        Me.pnlSourceButtons.Controls.Add(Me.btnAddSubtract)
        Me.pnlSourceButtons.Controls.Add(Me.btnAddPlus)
        Me.pnlSourceButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlSourceButtons.Location = New System.Drawing.Point(0, 390)
        Me.pnlSourceButtons.Name = "pnlSourceButtons"
        Me.pnlSourceButtons.Padding = New System.Windows.Forms.Padding(8)
        Me.pnlSourceButtons.Size = New System.Drawing.Size(478, 48)
        Me.pnlSourceButtons.TabIndex = 0
        '
        'btnAddSubtract
        '
        Me.btnAddSubtract.Location = New System.Drawing.Point(12, 9)
        Me.btnAddSubtract.Name = "btnAddSubtract"
        Me.btnAddSubtract.Size = New System.Drawing.Size(120, 30)
        Me.btnAddSubtract.TabIndex = 1
        Me.btnAddSubtract.Text = "إضافة كخصم -"
        Me.btnAddSubtract.UseVisualStyleBackColor = True
        '
        'btnAddPlus
        '
        Me.btnAddPlus.Location = New System.Drawing.Point(138, 9)
        Me.btnAddPlus.Name = "btnAddPlus"
        Me.btnAddPlus.Size = New System.Drawing.Size(120, 30)
        Me.btnAddPlus.TabIndex = 0
        Me.btnAddPlus.Text = "إضافة كموجب +"
        Me.btnAddPlus.UseVisualStyleBackColor = True
        '
        'grpFormulaDetails
        '
        Me.grpFormulaDetails.Controls.Add(Me.dgvFormulaDetails)
        Me.grpFormulaDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpFormulaDetails.Location = New System.Drawing.Point(0, 0)
        Me.grpFormulaDetails.Name = "grpFormulaDetails"
        Me.grpFormulaDetails.Padding = New System.Windows.Forms.Padding(8)
        Me.grpFormulaDetails.Size = New System.Drawing.Size(518, 390)
        Me.grpFormulaDetails.TabIndex = 1
        Me.grpFormulaDetails.TabStop = False
        Me.grpFormulaDetails.Text = "مصادر المعادلة الحالية"
        '
        'dgvFormulaDetails
        '
        Me.dgvFormulaDetails.AllowUserToAddRows = False
        Me.dgvFormulaDetails.AllowUserToDeleteRows = False
        Me.dgvFormulaDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvFormulaDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFormulaDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvFormulaDetails.Location = New System.Drawing.Point(8, 23)
        Me.dgvFormulaDetails.MultiSelect = False
        Me.dgvFormulaDetails.Name = "dgvFormulaDetails"
        Me.dgvFormulaDetails.ReadOnly = True
        Me.dgvFormulaDetails.RowHeadersVisible = False
        Me.dgvFormulaDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvFormulaDetails.Size = New System.Drawing.Size(502, 359)
        Me.dgvFormulaDetails.TabIndex = 0
        '
        'pnlDetailsButtons
        '
        Me.pnlDetailsButtons.Controls.Add(Me.btnMoveDown)
        Me.pnlDetailsButtons.Controls.Add(Me.btnMoveUp)
        Me.pnlDetailsButtons.Controls.Add(Me.btnRemoveDetail)
        Me.pnlDetailsButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlDetailsButtons.Location = New System.Drawing.Point(0, 390)
        Me.pnlDetailsButtons.Name = "pnlDetailsButtons"
        Me.pnlDetailsButtons.Padding = New System.Windows.Forms.Padding(8)
        Me.pnlDetailsButtons.Size = New System.Drawing.Size(518, 48)
        Me.pnlDetailsButtons.TabIndex = 0
        '
        'btnMoveDown
        '
        Me.btnMoveDown.Location = New System.Drawing.Point(12, 9)
        Me.btnMoveDown.Name = "btnMoveDown"
        Me.btnMoveDown.Size = New System.Drawing.Size(90, 30)
        Me.btnMoveDown.TabIndex = 2
        Me.btnMoveDown.Text = "لأسفل"
        Me.btnMoveDown.UseVisualStyleBackColor = True
        '
        'btnMoveUp
        '
        Me.btnMoveUp.Location = New System.Drawing.Point(108, 9)
        Me.btnMoveUp.Name = "btnMoveUp"
        Me.btnMoveUp.Size = New System.Drawing.Size(90, 30)
        Me.btnMoveUp.TabIndex = 1
        Me.btnMoveUp.Text = "لأعلى"
        Me.btnMoveUp.UseVisualStyleBackColor = True
        '
        'btnRemoveDetail
        '
        Me.btnRemoveDetail.Location = New System.Drawing.Point(204, 9)
        Me.btnRemoveDetail.Name = "btnRemoveDetail"
        Me.btnRemoveDetail.Size = New System.Drawing.Size(100, 30)
        Me.btnRemoveDetail.TabIndex = 0
        Me.btnRemoveDetail.Text = "حذف المصدر"
        Me.btnRemoveDetail.UseVisualStyleBackColor = True
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.txtFormulaText)
        Me.pnlBottom.Controls.Add(Me.lblFormulaText)
        Me.pnlBottom.Controls.Add(Me.btnCancel)
        Me.pnlBottom.Controls.Add(Me.btnSave)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 498)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Padding = New System.Windows.Forms.Padding(8)
        Me.pnlBottom.Size = New System.Drawing.Size(1000, 70)
        Me.pnlBottom.TabIndex = 2
        '
        'txtFormulaText
        '
        Me.txtFormulaText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFormulaText.Location = New System.Drawing.Point(230, 25)
        Me.txtFormulaText.Name = "txtFormulaText"
        Me.txtFormulaText.ReadOnly = True
        Me.txtFormulaText.Size = New System.Drawing.Size(600, 22)
        Me.txtFormulaText.TabIndex = 3
        '
        'lblFormulaText
        '
        Me.lblFormulaText.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFormulaText.AutoSize = True
        Me.lblFormulaText.Location = New System.Drawing.Point(836, 28)
        Me.lblFormulaText.Name = "lblFormulaText"
        Me.lblFormulaText.Size = New System.Drawing.Size(67, 14)
        Me.lblFormulaText.TabIndex = 2
        Me.lblFormulaText.Text = "نص المعادلة"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(12, 20)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(95, 32)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "إلغاء"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(113, 20)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(95, 32)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "حفظ"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'statusStrip1
        '
        Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.statusStrip1.Location = New System.Drawing.Point(0, 568)
        Me.statusStrip1.Name = "statusStrip1"
        Me.statusStrip1.Size = New System.Drawing.Size(1000, 22)
        Me.statusStrip1.TabIndex = 3
        Me.statusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(30, 17)
        Me.lblStatus.Text = "جاهز"
        '
        'FrmIncomeStatementFormulaBuilder
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(1000, 590)
        Me.Controls.Add(Me.splitMain)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.statusStrip1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Name = "FrmIncomeStatementFormulaBuilder"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "بناء معادلة قائمة الدخل"
        Me.pnlTop.ResumeLayout(False)
        Me.splitMain.Panel1.ResumeLayout(False)
        Me.splitMain.Panel2.ResumeLayout(False)
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.ResumeLayout(False)
        Me.grpSources.ResumeLayout(False)
        CType(Me.dgvSourceLines, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSourceButtons.ResumeLayout(False)
        Me.grpFormulaDetails.ResumeLayout(False)
        CType(Me.dgvFormulaDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDetailsButtons.ResumeLayout(False)
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.statusStrip1.ResumeLayout(False)
        Me.statusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlTop As Panel
    Friend WithEvents lblFormulaLine As Label
    Friend WithEvents splitMain As SplitContainer
    Friend WithEvents grpSources As GroupBox
    Friend WithEvents dgvSourceLines As DataGridView
    Friend WithEvents pnlSourceButtons As Panel
    Friend WithEvents btnAddSubtract As Button
    Friend WithEvents btnAddPlus As Button
    Friend WithEvents grpFormulaDetails As GroupBox
    Friend WithEvents dgvFormulaDetails As DataGridView
    Friend WithEvents pnlDetailsButtons As Panel
    Friend WithEvents btnMoveDown As Button
    Friend WithEvents btnMoveUp As Button
    Friend WithEvents btnRemoveDetail As Button
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents txtFormulaText As TextBox
    Friend WithEvents lblFormulaText As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents statusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel

End Class