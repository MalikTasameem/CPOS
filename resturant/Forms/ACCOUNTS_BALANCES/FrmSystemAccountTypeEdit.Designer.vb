<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSystemAccountTypeEdit
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
        Me.lblAccountName = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.grpNotes = New System.Windows.Forms.GroupBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.grpNatural = New System.Windows.Forms.GroupBox()
        Me.lblNaturalHint = New System.Windows.Forms.Label()
        Me.cmbNatural = New System.Windows.Forms.ComboBox()
        Me.grpOptions = New System.Windows.Forms.GroupBox()
        Me.chkIsActive = New System.Windows.Forms.CheckBox()
        Me.chkMustBeLeaf = New System.Windows.Forms.CheckBox()
        Me.chkAllowSameAccount = New System.Windows.Forms.CheckBox()
        Me.chkRequired = New System.Windows.Forms.CheckBox()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.grpNotes.SuspendLayout()
        Me.grpNatural.SuspendLayout()
        Me.grpOptions.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(72, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblAccountName)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Padding = New System.Windows.Forms.Padding(12, 8, 12, 8)
        Me.pnlHeader.Size = New System.Drawing.Size(620, 82)
        Me.pnlHeader.TabIndex = 0
        '
        'lblAccountName
        '
        Me.lblAccountName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblAccountName.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblAccountName.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblAccountName.Location = New System.Drawing.Point(12, 42)
        Me.lblAccountName.Name = "lblAccountName"
        Me.lblAccountName.Size = New System.Drawing.Size(596, 32)
        Me.lblAccountName.TabIndex = 1
        Me.lblAccountName.Text = "اسم الحساب الأساسي"
        Me.lblAccountName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTitle
        '
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(12, 8)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(596, 34)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "تعديل نمط الحساب الأساسي"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.grpNotes)
        Me.pnlMain.Controls.Add(Me.grpNatural)
        Me.pnlMain.Controls.Add(Me.grpOptions)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 82)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(12)
        Me.pnlMain.Size = New System.Drawing.Size(620, 348)
        Me.pnlMain.TabIndex = 1
        '
        'grpNotes
        '
        Me.grpNotes.Controls.Add(Me.txtNotes)
        Me.grpNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpNotes.Location = New System.Drawing.Point(12, 230)
        Me.grpNotes.Name = "grpNotes"
        Me.grpNotes.Padding = New System.Windows.Forms.Padding(12)
        Me.grpNotes.Size = New System.Drawing.Size(596, 106)
        Me.grpNotes.TabIndex = 2
        Me.grpNotes.TabStop = False
        Me.grpNotes.Text = "ملاحظات"
        '
        'txtNotes
        '
        Me.txtNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtNotes.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.txtNotes.Location = New System.Drawing.Point(12, 27)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(572, 67)
        Me.txtNotes.TabIndex = 0
        '
        'grpNatural
        '
        Me.grpNatural.Controls.Add(Me.lblNaturalHint)
        Me.grpNatural.Controls.Add(Me.cmbNatural)
        Me.grpNatural.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpNatural.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpNatural.Location = New System.Drawing.Point(12, 138)
        Me.grpNatural.Name = "grpNatural"
        Me.grpNatural.Padding = New System.Windows.Forms.Padding(12)
        Me.grpNatural.Size = New System.Drawing.Size(596, 92)
        Me.grpNatural.TabIndex = 1
        Me.grpNatural.TabStop = False
        Me.grpNatural.Text = "الطبيعة المتوقعة للحساب"
        '
        'lblNaturalHint
        '
        Me.lblNaturalHint.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.lblNaturalHint.ForeColor = System.Drawing.Color.DimGray
        Me.lblNaturalHint.Location = New System.Drawing.Point(15, 30)
        Me.lblNaturalHint.Name = "lblNaturalHint"
        Me.lblNaturalHint.Size = New System.Drawing.Size(350, 42)
        Me.lblNaturalHint.TabIndex = 1
        Me.lblNaturalHint.Text = "اختر بدون إذا كنت لا تريد تقييد الحساب بطبيعة مدين أو دائن."
        Me.lblNaturalHint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbNatural
        '
        Me.cmbNatural.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNatural.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.cmbNatural.FormattingEnabled = True
        Me.cmbNatural.Location = New System.Drawing.Point(380, 36)
        Me.cmbNatural.Name = "cmbNatural"
        Me.cmbNatural.Size = New System.Drawing.Size(182, 24)
        Me.cmbNatural.TabIndex = 0
        '
        'grpOptions
        '
        Me.grpOptions.Controls.Add(Me.chkIsActive)
        Me.grpOptions.Controls.Add(Me.chkMustBeLeaf)
        Me.grpOptions.Controls.Add(Me.chkAllowSameAccount)
        Me.grpOptions.Controls.Add(Me.chkRequired)
        Me.grpOptions.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpOptions.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.grpOptions.Location = New System.Drawing.Point(12, 12)
        Me.grpOptions.Name = "grpOptions"
        Me.grpOptions.Padding = New System.Windows.Forms.Padding(12)
        Me.grpOptions.Size = New System.Drawing.Size(596, 126)
        Me.grpOptions.TabIndex = 0
        Me.grpOptions.TabStop = False
        Me.grpOptions.Text = "خيارات التحقق"
        '
        'chkIsActive
        '
        Me.chkIsActive.AutoSize = True
        Me.chkIsActive.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.chkIsActive.Location = New System.Drawing.Point(169, 72)
        Me.chkIsActive.Name = "chkIsActive"
        Me.chkIsActive.Size = New System.Drawing.Size(49, 18)
        Me.chkIsActive.TabIndex = 3
        Me.chkIsActive.Text = "فعال"
        Me.chkIsActive.UseVisualStyleBackColor = True
        '
        'chkMustBeLeaf
        '
        Me.chkMustBeLeaf.AutoSize = True
        Me.chkMustBeLeaf.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.chkMustBeLeaf.Location = New System.Drawing.Point(55, 31)
        Me.chkMustBeLeaf.Name = "chkMustBeLeaf"
        Me.chkMustBeLeaf.Size = New System.Drawing.Size(163, 18)
        Me.chkMustBeLeaf.TabIndex = 2
        Me.chkMustBeLeaf.Text = "يجب أن يكون الحساب فرعيًا"
        Me.chkMustBeLeaf.UseVisualStyleBackColor = True
        '
        'chkAllowSameAccount
        '
        Me.chkAllowSameAccount.AutoSize = True
        Me.chkAllowSameAccount.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.chkAllowSameAccount.Location = New System.Drawing.Point(265, 72)
        Me.chkAllowSameAccount.Name = "chkAllowSameAccount"
        Me.chkAllowSameAccount.Size = New System.Drawing.Size(266, 18)
        Me.chkAllowSameAccount.TabIndex = 1
        Me.chkAllowSameAccount.Text = "السماح باستخدام نفس الحساب مع أكثر من نوع"
        Me.chkAllowSameAccount.UseVisualStyleBackColor = True
        '
        'chkRequired
        '
        Me.chkRequired.AutoSize = True
        Me.chkRequired.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.chkRequired.Location = New System.Drawing.Point(390, 31)
        Me.chkRequired.Name = "chkRequired"
        Me.chkRequired.Size = New System.Drawing.Size(145, 18)
        Me.chkRequired.TabIndex = 0
        Me.chkRequired.Text = "الحساب إجباري للترحيل"
        Me.chkRequired.UseVisualStyleBackColor = True
        '
        'pnlButtons
        '
        Me.pnlButtons.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlButtons.Controls.Add(Me.btnCancel)
        Me.pnlButtons.Controls.Add(Me.btnSave)
        Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlButtons.Location = New System.Drawing.Point(0, 430)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlButtons.Size = New System.Drawing.Size(620, 58)
        Me.pnlButtons.TabIndex = 2
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.Location = New System.Drawing.Point(10, 10)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(120, 38)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "إلغاء"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.Location = New System.Drawing.Point(480, 10)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(130, 38)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "حفظ"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'FrmSystemAccountTypeEdit
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(620, 488)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlButtons)
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSystemAccountTypeEdit"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "تعديل نمط الحساب الأساسي"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.grpNotes.ResumeLayout(False)
        Me.grpNotes.PerformLayout()
        Me.grpNatural.ResumeLayout(False)
        Me.grpOptions.ResumeLayout(False)
        Me.grpOptions.PerformLayout()
        Me.pnlButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblAccountName As Label
    Friend WithEvents pnlMain As Panel
    Friend WithEvents grpOptions As GroupBox
    Friend WithEvents chkRequired As CheckBox
    Friend WithEvents chkAllowSameAccount As CheckBox
    Friend WithEvents chkMustBeLeaf As CheckBox
    Friend WithEvents chkIsActive As CheckBox
    Friend WithEvents grpNatural As GroupBox
    Friend WithEvents cmbNatural As ComboBox
    Friend WithEvents lblNaturalHint As Label
    Friend WithEvents grpNotes As GroupBox
    Friend WithEvents txtNotes As TextBox
    Friend WithEvents pnlButtons As Panel
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
End Class