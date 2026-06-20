<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmIncomeStatementAccountLinkEdit
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
        Me.lblAccountInfo = New System.Windows.Forms.Label()
        Me.chkIncludeChildren = New System.Windows.Forms.CheckBox()
        Me.lblAccountSignMode = New System.Windows.Forms.Label()
        Me.cboAccountSignMode = New System.Windows.Forms.ComboBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblAccountInfo
        '
        Me.lblAccountInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAccountInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAccountInfo.Location = New System.Drawing.Point(14, 14)
        Me.lblAccountInfo.Name = "lblAccountInfo"
        Me.lblAccountInfo.Padding = New System.Windows.Forms.Padding(8)
        Me.lblAccountInfo.Size = New System.Drawing.Size(500, 58)
        Me.lblAccountInfo.TabIndex = 0
        Me.lblAccountInfo.Text = "معلومات الحساب"
        Me.lblAccountInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkIncludeChildren
        '
        Me.chkIncludeChildren.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkIncludeChildren.AutoSize = True
        Me.chkIncludeChildren.Location = New System.Drawing.Point(405, 92)
        Me.chkIncludeChildren.Name = "chkIncludeChildren"
        Me.chkIncludeChildren.Size = New System.Drawing.Size(88, 18)
        Me.chkIncludeChildren.TabIndex = 1
        Me.chkIncludeChildren.Text = "يشمل الأبناء"
        Me.chkIncludeChildren.UseVisualStyleBackColor = True
        '
        'lblAccountSignMode
        '
        Me.lblAccountSignMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAccountSignMode.AutoSize = True
        Me.lblAccountSignMode.Location = New System.Drawing.Point(402, 136)
        Me.lblAccountSignMode.Name = "lblAccountSignMode"
        Me.lblAccountSignMode.Size = New System.Drawing.Size(76, 14)
        Me.lblAccountSignMode.TabIndex = 2
        Me.lblAccountSignMode.Text = "طريقة الإشارة"
        '
        'cboAccountSignMode
        '
        Me.cboAccountSignMode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboAccountSignMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAccountSignMode.FormattingEnabled = True
        Me.cboAccountSignMode.Location = New System.Drawing.Point(14, 133)
        Me.cboAccountSignMode.Name = "cboAccountSignMode"
        Me.cboAccountSignMode.Size = New System.Drawing.Size(370, 22)
        Me.cboAccountSignMode.TabIndex = 3
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(116, 191)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(95, 32)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "حفظ"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(15, 191)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(95, 32)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "إلغاء"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'FrmIncomeStatementAccountLinkEdit
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(528, 243)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.cboAccountSignMode)
        Me.Controls.Add(Me.lblAccountSignMode)
        Me.Controls.Add(Me.chkIncludeChildren)
        Me.Controls.Add(Me.lblAccountInfo)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmIncomeStatementAccountLinkEdit"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "تعديل خصائص ربط الحساب"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblAccountInfo As Label
    Friend WithEvents chkIncludeChildren As CheckBox
    Friend WithEvents lblAccountSignMode As Label
    Friend WithEvents cboAccountSignMode As ComboBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button

End Class