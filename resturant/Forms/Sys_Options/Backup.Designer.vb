<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Backup
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
        Me.BackupButtonX = New DevComponents.DotNetBar.ButtonX()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.IsBackupPath2CheckBox = New System.Windows.Forms.CheckBox()
        Me.BackupPath2TextBox = New System.Windows.Forms.TextBox()
        Me.BackupPathButton = New System.Windows.Forms.Button()
        Me.BackupPath2Button = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.BackupPathTextBox = New System.Windows.Forms.TextBox()
        Me.RestoreButtonX = New DevComponents.DotNetBar.ButtonX()
        Me.IsBackupCheckBox = New System.Windows.Forms.CheckBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Save_butt = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BkPath3_Btn = New System.Windows.Forms.Button()
        Me.BkPath3_txt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NoBK_btn = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.is_Comprission_CB = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BackupButtonX
        '
        Me.BackupButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.BackupButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.BackupButtonX.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BackupButtonX.Font = New System.Drawing.Font("Segoe UI Semibold", 13.25!, System.Drawing.FontStyle.Bold)
        Me.BackupButtonX.Location = New System.Drawing.Point(374, 497)
        Me.BackupButtonX.Name = "BackupButtonX"
        Me.BackupButtonX.Size = New System.Drawing.Size(190, 47)
        Me.BackupButtonX.TabIndex = 276
        Me.BackupButtonX.Text = "أخد  نسخة إحتياطية"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(488, 111)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(231, 25)
        Me.Label14.TabIndex = 272
        Me.Label14.Text = "مسار ثاني للنسخة الإحتياطية"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(489, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(192, 25)
        Me.Label4.TabIndex = 266
        Me.Label4.Text = "مسار النسخة الإحتياطية"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IsBackupPath2CheckBox
        '
        Me.IsBackupPath2CheckBox.AutoSize = True
        Me.IsBackupPath2CheckBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IsBackupPath2CheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.IsBackupPath2CheckBox.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IsBackupPath2CheckBox.Location = New System.Drawing.Point(196, 145)
        Me.IsBackupPath2CheckBox.Name = "IsBackupPath2CheckBox"
        Me.IsBackupPath2CheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.IsBackupPath2CheckBox.Size = New System.Drawing.Size(344, 29)
        Me.IsBackupPath2CheckBox.TabIndex = 275
        Me.IsBackupPath2CheckBox.Text = "تفعيل أخد النسخة الإحتياطية لهذا المســار"
        Me.IsBackupPath2CheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IsBackupPath2CheckBox.UseVisualStyleBackColor = True
        '
        'BackupPath2TextBox
        '
        Me.BackupPath2TextBox.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BackupPath2TextBox.Location = New System.Drawing.Point(55, 107)
        Me.BackupPath2TextBox.Name = "BackupPath2TextBox"
        Me.BackupPath2TextBox.ReadOnly = True
        Me.BackupPath2TextBox.Size = New System.Drawing.Size(430, 33)
        Me.BackupPath2TextBox.TabIndex = 273
        Me.BackupPath2TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BackupPathButton
        '
        Me.BackupPathButton.BackColor = System.Drawing.Color.SeaShell
        Me.BackupPathButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BackupPathButton.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BackupPathButton.Location = New System.Drawing.Point(2, 25)
        Me.BackupPathButton.Name = "BackupPathButton"
        Me.BackupPathButton.Size = New System.Drawing.Size(52, 39)
        Me.BackupPathButton.TabIndex = 268
        Me.BackupPathButton.Text = "......"
        Me.BackupPathButton.UseVisualStyleBackColor = False
        '
        'BackupPath2Button
        '
        Me.BackupPath2Button.BackColor = System.Drawing.Color.SeaShell
        Me.BackupPath2Button.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BackupPath2Button.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BackupPath2Button.Location = New System.Drawing.Point(2, 104)
        Me.BackupPath2Button.Name = "BackupPath2Button"
        Me.BackupPath2Button.Size = New System.Drawing.Size(52, 39)
        Me.BackupPath2Button.TabIndex = 274
        Me.BackupPath2Button.Text = "......"
        Me.BackupPath2Button.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.DarkRed
        Me.Label8.Location = New System.Drawing.Point(78, 69)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label8.Size = New System.Drawing.Size(110, 25)
        Me.Label8.TabIndex = 270
        Me.Label8.Text = "كل 10 دقائق"
        '
        'BackupPathTextBox
        '
        Me.BackupPathTextBox.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BackupPathTextBox.Location = New System.Drawing.Point(55, 28)
        Me.BackupPathTextBox.Name = "BackupPathTextBox"
        Me.BackupPathTextBox.ReadOnly = True
        Me.BackupPathTextBox.Size = New System.Drawing.Size(430, 33)
        Me.BackupPathTextBox.TabIndex = 267
        Me.BackupPathTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'RestoreButtonX
        '
        Me.RestoreButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.RestoreButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.RestoreButtonX.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RestoreButtonX.Font = New System.Drawing.Font("Segoe UI Semibold", 13.25!, System.Drawing.FontStyle.Bold)
        Me.RestoreButtonX.Location = New System.Drawing.Point(181, 497)
        Me.RestoreButtonX.Name = "RestoreButtonX"
        Me.RestoreButtonX.Size = New System.Drawing.Size(190, 47)
        Me.RestoreButtonX.TabIndex = 265
        Me.RestoreButtonX.Text = "إستعــادة نسخة إحتياطية"
        '
        'IsBackupCheckBox
        '
        Me.IsBackupCheckBox.AutoSize = True
        Me.IsBackupCheckBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IsBackupCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.IsBackupCheckBox.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IsBackupCheckBox.Location = New System.Drawing.Point(196, 65)
        Me.IsBackupCheckBox.Name = "IsBackupCheckBox"
        Me.IsBackupCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.IsBackupCheckBox.Size = New System.Drawing.Size(344, 29)
        Me.IsBackupCheckBox.TabIndex = 271
        Me.IsBackupCheckBox.Text = "تفعيل أخد النسخة الإحتياطية لهذا المســار"
        Me.IsBackupCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.IsBackupCheckBox.UseVisualStyleBackColor = True
        '
        'Save_butt
        '
        Me.Save_butt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Save_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Save_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Save_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.Save_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Save_butt.Font = New System.Drawing.Font("Segoe UI", 13.75!, System.Drawing.FontStyle.Bold)
        Me.Save_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Save_butt.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.Save_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Save_butt.Location = New System.Drawing.Point(6, 498)
        Me.Save_butt.Name = "Save_butt"
        Me.Save_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Save_butt.Size = New System.Drawing.Size(169, 46)
        Me.Save_butt.TabIndex = 296
        Me.Save_butt.TabStop = False
        Me.Save_butt.Text = "حفظ التعديلات"
        Me.Save_butt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Save_butt.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.BackupPathTextBox)
        Me.GroupBox1.Controls.Add(Me.IsBackupCheckBox)
        Me.GroupBox1.Controls.Add(Me.BackupPath2Button)
        Me.GroupBox1.Controls.Add(Me.IsBackupPath2CheckBox)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.BackupPathButton)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.BackupPath2TextBox)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(720, 228)
        Me.GroupBox1.TabIndex = 297
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "مسار النسخة الإحتياطية الألي"
        Me.GroupBox1.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DarkRed
        Me.Label2.Location = New System.Drawing.Point(78, 149)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(97, 25)
        Me.Label2.TabIndex = 276
        Me.Label2.Text = "كل 1 ساعة"
        '
        'BkPath3_Btn
        '
        Me.BkPath3_Btn.BackColor = System.Drawing.Color.SeaShell
        Me.BkPath3_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BkPath3_Btn.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BkPath3_Btn.Location = New System.Drawing.Point(4, 297)
        Me.BkPath3_Btn.Name = "BkPath3_Btn"
        Me.BkPath3_Btn.Size = New System.Drawing.Size(52, 39)
        Me.BkPath3_Btn.TabIndex = 299
        Me.BkPath3_Btn.Text = "......"
        Me.BkPath3_Btn.UseVisualStyleBackColor = False
        '
        'BkPath3_txt
        '
        Me.BkPath3_txt.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BkPath3_txt.Location = New System.Drawing.Point(59, 301)
        Me.BkPath3_txt.Name = "BkPath3_txt"
        Me.BkPath3_txt.ReadOnly = True
        Me.BkPath3_txt.Size = New System.Drawing.Size(467, 33)
        Me.BkPath3_txt.TabIndex = 298
        Me.BkPath3_txt.Tag = ""
        Me.BkPath3_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(243, 273)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(338, 25)
        Me.Label1.TabIndex = 300
        Me.Label1.Text = "مسار للنسخة الإحتياطية عند تسجيل الخروج"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'NoBK_btn
        '
        Me.NoBK_btn.BackColor = System.Drawing.Color.SeaShell
        Me.NoBK_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NoBK_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.NoBK_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NoBK_btn.Image = Global.resturant.My.Resources.Resources.if_cancel_46786
        Me.NoBK_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.NoBK_btn.Location = New System.Drawing.Point(399, 337)
        Me.NoBK_btn.Name = "NoBK_btn"
        Me.NoBK_btn.Size = New System.Drawing.Size(127, 33)
        Me.NoBK_btn.TabIndex = 301
        Me.NoBK_btn.Text = "إلغاء المسار"
        Me.NoBK_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NoBK_btn.UseVisualStyleBackColor = False
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
        Me.ExitFormButton.Location = New System.Drawing.Point(600, 498)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(133, 47)
        Me.ExitFormButton.TabIndex = 454
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'is_Comprission_CB
        '
        Me.is_Comprission_CB.AutoSize = True
        Me.is_Comprission_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.is_Comprission_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.is_Comprission_CB.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.is_Comprission_CB.Location = New System.Drawing.Point(407, 464)
        Me.is_Comprission_CB.Name = "is_Comprission_CB"
        Me.is_Comprission_CB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.is_Comprission_CB.Size = New System.Drawing.Size(154, 29)
        Me.is_Comprission_CB.TabIndex = 455
        Me.is_Comprission_CB.Text = "نسخة مضغوطة"
        Me.is_Comprission_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.is_Comprission_CB.UseVisualStyleBackColor = True
        '
        'Backup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 549)
        Me.ControlBox = False
        Me.Controls.Add(Me.is_Comprission_CB)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.NoBK_btn)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BkPath3_Btn)
        Me.Controls.Add(Me.BkPath3_txt)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Save_butt)
        Me.Controls.Add(Me.BackupButtonX)
        Me.Controls.Add(Me.RestoreButtonX)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Backup"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "النسخة الإحتياطية"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BackupButtonX As DevComponents.DotNetBar.ButtonX
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents IsBackupPath2CheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents BackupPath2TextBox As System.Windows.Forms.TextBox
    Friend WithEvents BackupPathButton As System.Windows.Forms.Button
    Friend WithEvents BackupPath2Button As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents BackupPathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RestoreButtonX As DevComponents.DotNetBar.ButtonX
    Friend WithEvents IsBackupCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Save_butt As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BkPath3_Btn As System.Windows.Forms.Button
    Friend WithEvents BkPath3_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NoBK_btn As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents is_Comprission_CB As System.Windows.Forms.CheckBox
End Class
