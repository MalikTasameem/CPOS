<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SysDelete
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ConfirmDeleteButtonX = New DevComponents.DotNetBar.ButtonX()
        Me.DeleteAllItemsCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DeleteAllBuyAndSellCheckBox = New System.Windows.Forms.CheckBox()
        Me.NewDBGroupBox = New System.Windows.Forms.GroupBox()
        Me.is_ReadOnly_CB = New System.Windows.Forms.CheckBox()
        Me.InsertDBButton = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.NewDBTextBox = New System.Windows.Forms.TextBox()
        Me.Delete_AGBalance_CB = New System.Windows.Forms.CheckBox()
        Me.Jrd_ID_Txt = New System.Windows.Forms.TextBox()
        Me.BackupButtonX = New DevComponents.DotNetBar.ButtonX()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Keep_QTY_Rd = New System.Windows.Forms.RadioButton()
        Me.Delete_ALL_Qty_CB = New System.Windows.Forms.RadioButton()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Delete_ST_CB = New System.Windows.Forms.CheckBox()
        Me.Keep_Specific_Rd = New System.Windows.Forms.RadioButton()
        Me.NewDBGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(134, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(430, 42)
        Me.Label9.TabIndex = 267
        Me.Label9.Text = "يستحسن ان تكون لديك نسخة إحتياطية للقاعدة مرفقة بالرقم السري" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " للمسؤول قبل تطبيق " & _
    "هذه العملية"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ConfirmDeleteButtonX
        '
        Me.ConfirmDeleteButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ConfirmDeleteButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ConfirmDeleteButtonX.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ConfirmDeleteButtonX.Enabled = False
        Me.ConfirmDeleteButtonX.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ConfirmDeleteButtonX.Location = New System.Drawing.Point(153, 431)
        Me.ConfirmDeleteButtonX.Name = "ConfirmDeleteButtonX"
        Me.ConfirmDeleteButtonX.Size = New System.Drawing.Size(180, 51)
        Me.ConfirmDeleteButtonX.TabIndex = 263
        Me.ConfirmDeleteButtonX.Text = "تطبيــق"
        '
        'DeleteAllItemsCheckBox
        '
        Me.DeleteAllItemsCheckBox.AutoSize = True
        Me.DeleteAllItemsCheckBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DeleteAllItemsCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DeleteAllItemsCheckBox.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.DeleteAllItemsCheckBox.Location = New System.Drawing.Point(294, 400)
        Me.DeleteAllItemsCheckBox.Name = "DeleteAllItemsCheckBox"
        Me.DeleteAllItemsCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DeleteAllItemsCheckBox.Size = New System.Drawing.Size(270, 25)
        Me.DeleteAllItemsCheckBox.TabIndex = 265
        Me.DeleteAllItemsCheckBox.Text = "ترحيل كافة بيانات النظام (تهيئة النظام)"
        Me.DeleteAllItemsCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.DeleteAllItemsCheckBox.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Beige
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(566, 5)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label10.Size = New System.Drawing.Size(53, 21)
        Me.Label10.TabIndex = 266
        Me.Label10.Text = "تنويـه :"
        '
        'DeleteAllBuyAndSellCheckBox
        '
        Me.DeleteAllBuyAndSellCheckBox.AutoSize = True
        Me.DeleteAllBuyAndSellCheckBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DeleteAllBuyAndSellCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DeleteAllBuyAndSellCheckBox.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.DeleteAllBuyAndSellCheckBox.Location = New System.Drawing.Point(9, 152)
        Me.DeleteAllBuyAndSellCheckBox.Name = "DeleteAllBuyAndSellCheckBox"
        Me.DeleteAllBuyAndSellCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DeleteAllBuyAndSellCheckBox.Size = New System.Drawing.Size(545, 46)
        Me.DeleteAllBuyAndSellCheckBox.TabIndex = 264
        Me.DeleteAllBuyAndSellCheckBox.Text = "ترحيل كافة أرصدة معاملات البيع والشراء والمصروفات, والإبقاء على أرصدة العملاء" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " و" & _
    "الخزينة" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.DeleteAllBuyAndSellCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.DeleteAllBuyAndSellCheckBox.UseVisualStyleBackColor = True
        '
        'NewDBGroupBox
        '
        Me.NewDBGroupBox.Controls.Add(Me.is_ReadOnly_CB)
        Me.NewDBGroupBox.Controls.Add(Me.InsertDBButton)
        Me.NewDBGroupBox.Controls.Add(Me.Label13)
        Me.NewDBGroupBox.Controls.Add(Me.NewDBTextBox)
        Me.NewDBGroupBox.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NewDBGroupBox.Location = New System.Drawing.Point(9, 488)
        Me.NewDBGroupBox.Name = "NewDBGroupBox"
        Me.NewDBGroupBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NewDBGroupBox.Size = New System.Drawing.Size(555, 66)
        Me.NewDBGroupBox.TabIndex = 270
        Me.NewDBGroupBox.TabStop = False
        Me.NewDBGroupBox.Text = "إضافة قاعدة بيانات"
        '
        'is_ReadOnly_CB
        '
        Me.is_ReadOnly_CB.AutoSize = True
        Me.is_ReadOnly_CB.BackColor = System.Drawing.Color.Transparent
        Me.is_ReadOnly_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.is_ReadOnly_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.is_ReadOnly_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.is_ReadOnly_CB.ForeColor = System.Drawing.Color.Black
        Me.is_ReadOnly_CB.Location = New System.Drawing.Point(16, 28)
        Me.is_ReadOnly_CB.Name = "is_ReadOnly_CB"
        Me.is_ReadOnly_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.is_ReadOnly_CB.Size = New System.Drawing.Size(92, 25)
        Me.is_ReadOnly_CB.TabIndex = 456
        Me.is_ReadOnly_CB.Text = "قراءة فقط"
        Me.is_ReadOnly_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.is_ReadOnly_CB.UseVisualStyleBackColor = False
        '
        'InsertDBButton
        '
        Me.InsertDBButton.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.InsertDBButton.BackColor = System.Drawing.Color.SeaShell
        Me.InsertDBButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.InsertDBButton.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.InsertDBButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InsertDBButton.Location = New System.Drawing.Point(111, 21)
        Me.InsertDBButton.Name = "InsertDBButton"
        Me.InsertDBButton.Size = New System.Drawing.Size(78, 39)
        Me.InsertDBButton.TabIndex = 265
        Me.InsertDBButton.Text = "إضافة"
        Me.InsertDBButton.UseVisualStyleBackColor = False
        '
        'Label13
        '
        Me.Label13.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(462, 30)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(86, 21)
        Me.Label13.TabIndex = 266
        Me.Label13.Text = "إسم القاعدة"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'NewDBTextBox
        '
        Me.NewDBTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.NewDBTextBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NewDBTextBox.Location = New System.Drawing.Point(190, 26)
        Me.NewDBTextBox.Name = "NewDBTextBox"
        Me.NewDBTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.NewDBTextBox.Size = New System.Drawing.Size(266, 29)
        Me.NewDBTextBox.TabIndex = 267
        Me.NewDBTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Delete_AGBalance_CB
        '
        Me.Delete_AGBalance_CB.AutoSize = True
        Me.Delete_AGBalance_CB.BackColor = System.Drawing.Color.Transparent
        Me.Delete_AGBalance_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Delete_AGBalance_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Delete_AGBalance_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Delete_AGBalance_CB.ForeColor = System.Drawing.Color.Black
        Me.Delete_AGBalance_CB.Location = New System.Drawing.Point(352, 369)
        Me.Delete_AGBalance_CB.Name = "Delete_AGBalance_CB"
        Me.Delete_AGBalance_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Delete_AGBalance_CB.Size = New System.Drawing.Size(212, 25)
        Me.Delete_AGBalance_CB.TabIndex = 271
        Me.Delete_AGBalance_CB.Text = "ترحيل أرصدة العملاء والخزينة"
        Me.Delete_AGBalance_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Delete_AGBalance_CB.UseVisualStyleBackColor = False
        '
        'Jrd_ID_Txt
        '
        Me.Jrd_ID_Txt.Enabled = False
        Me.Jrd_ID_Txt.Location = New System.Drawing.Point(2, 265)
        Me.Jrd_ID_Txt.Name = "Jrd_ID_Txt"
        Me.Jrd_ID_Txt.Size = New System.Drawing.Size(192, 29)
        Me.Jrd_ID_Txt.TabIndex = 272
        Me.Jrd_ID_Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BackupButtonX
        '
        Me.BackupButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.BackupButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.BackupButtonX.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BackupButtonX.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BackupButtonX.Location = New System.Drawing.Point(2, 3)
        Me.BackupButtonX.Name = "BackupButtonX"
        Me.BackupButtonX.Size = New System.Drawing.Size(129, 47)
        Me.BackupButtonX.TabIndex = 277
        Me.BackupButtonX.Text = "أخد  نسخة إحتياطية"
        '
        'Keep_QTY_Rd
        '
        Me.Keep_QTY_Rd.AutoSize = True
        Me.Keep_QTY_Rd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Keep_QTY_Rd.Font = New System.Drawing.Font("Segoe UI", 10.75!)
        Me.Keep_QTY_Rd.Location = New System.Drawing.Point(98, 238)
        Me.Keep_QTY_Rd.Name = "Keep_QTY_Rd"
        Me.Keep_QTY_Rd.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Keep_QTY_Rd.Size = New System.Drawing.Size(326, 24)
        Me.Keep_QTY_Rd.TabIndex = 278
        Me.Keep_QTY_Rd.Text = "الإبقاء على أرصدة المخازن وجردها في فاتورة أليا"
        Me.Keep_QTY_Rd.UseVisualStyleBackColor = True
        '
        'Delete_ALL_Qty_CB
        '
        Me.Delete_ALL_Qty_CB.AutoSize = True
        Me.Delete_ALL_Qty_CB.Checked = True
        Me.Delete_ALL_Qty_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Delete_ALL_Qty_CB.Font = New System.Drawing.Font("Segoe UI", 10.75!)
        Me.Delete_ALL_Qty_CB.Location = New System.Drawing.Point(267, 208)
        Me.Delete_ALL_Qty_CB.Name = "Delete_ALL_Qty_CB"
        Me.Delete_ALL_Qty_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Delete_ALL_Qty_CB.Size = New System.Drawing.Size(157, 24)
        Me.Delete_ALL_Qty_CB.TabIndex = 279
        Me.Delete_ALL_Qty_CB.TabStop = True
        Me.Delete_ALL_Qty_CB.Text = "ترحيل أرصدة المخازن"
        Me.Delete_ALL_Qty_CB.UseVisualStyleBackColor = True
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.Location = New System.Drawing.Point(485, 560)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(133, 47)
        Me.ExitFormButton.TabIndex = 454
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Delete_ST_CB
        '
        Me.Delete_ST_CB.AutoSize = True
        Me.Delete_ST_CB.BackColor = System.Drawing.Color.Transparent
        Me.Delete_ST_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Delete_ST_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Delete_ST_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Delete_ST_CB.ForeColor = System.Drawing.Color.Black
        Me.Delete_ST_CB.Location = New System.Drawing.Point(398, 118)
        Me.Delete_ST_CB.Name = "Delete_ST_CB"
        Me.Delete_ST_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Delete_ST_CB.Size = New System.Drawing.Size(156, 25)
        Me.Delete_ST_CB.TabIndex = 455
        Me.Delete_ST_CB.Text = "تهيئة أرصدة المخازن"
        Me.Delete_ST_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Delete_ST_CB.UseVisualStyleBackColor = False
        Me.Delete_ST_CB.Visible = False
        '
        'Keep_Specific_Rd
        '
        Me.Keep_Specific_Rd.AutoSize = True
        Me.Keep_Specific_Rd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Keep_Specific_Rd.Font = New System.Drawing.Font("Segoe UI", 10.75!)
        Me.Keep_Specific_Rd.Location = New System.Drawing.Point(197, 268)
        Me.Keep_Specific_Rd.Name = "Keep_Specific_Rd"
        Me.Keep_Specific_Rd.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Keep_Specific_Rd.Size = New System.Drawing.Size(227, 24)
        Me.Keep_Specific_Rd.TabIndex = 456
        Me.Keep_Specific_Rd.Text = "ترصيد الآصناف بفاتورة جرد رقم :"
        Me.Keep_Specific_Rd.UseVisualStyleBackColor = True
        '
        'SysDelete
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(622, 609)
        Me.ControlBox = False
        Me.Controls.Add(Me.Keep_Specific_Rd)
        Me.Controls.Add(Me.Delete_ST_CB)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Delete_ALL_Qty_CB)
        Me.Controls.Add(Me.Keep_QTY_Rd)
        Me.Controls.Add(Me.BackupButtonX)
        Me.Controls.Add(Me.Jrd_ID_Txt)
        Me.Controls.Add(Me.Delete_AGBalance_CB)
        Me.Controls.Add(Me.NewDBGroupBox)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.ConfirmDeleteButtonX)
        Me.Controls.Add(Me.DeleteAllItemsCheckBox)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.DeleteAllBuyAndSellCheckBox)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "SysDelete"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ترحيل البيانات"
        Me.NewDBGroupBox.ResumeLayout(False)
        Me.NewDBGroupBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ConfirmDeleteButtonX As DevComponents.DotNetBar.ButtonX
    Friend WithEvents DeleteAllItemsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents DeleteAllBuyAndSellCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents NewDBGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents InsertDBButton As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents NewDBTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Delete_AGBalance_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Jrd_ID_Txt As System.Windows.Forms.TextBox
    Friend WithEvents BackupButtonX As DevComponents.DotNetBar.ButtonX
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Keep_QTY_Rd As System.Windows.Forms.RadioButton
    Friend WithEvents Delete_ALL_Qty_CB As System.Windows.Forms.RadioButton
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Delete_ST_CB As System.Windows.Forms.CheckBox
    Friend WithEvents is_ReadOnly_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Keep_Specific_Rd As System.Windows.Forms.RadioButton
End Class
