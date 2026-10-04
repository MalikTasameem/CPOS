<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AG_Projects
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AG_Projects))
        Me.S_listBox = New System.Windows.Forms.ListBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.SNameTextBox = New System.Windows.Forms.TextBox()
        Me.DeleteSButton = New System.Windows.Forms.Button()
        Me.NewSButton = New System.Windows.Forms.Button()
        Me.EditSButton = New System.Windows.Forms.Button()
        Me.SaveSButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Confirm_Btn = New System.Windows.Forms.Button()
        Me.SearchTextBox = New System.Windows.Forms.TextBox()
        Me.SearchClearButton = New System.Windows.Forms.Button()
        Me.SearchLabel = New System.Windows.Forms.Label()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.Title_Label = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.HeaderPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'S_listBox
        '
        Me.S_listBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.S_listBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.S_listBox.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.S_listBox.FormattingEnabled = True
        Me.S_listBox.ItemHeight = 17
        Me.S_listBox.Location = New System.Drawing.Point(3, 19)
        Me.S_listBox.Name = "S_listBox"
        Me.S_listBox.Size = New System.Drawing.Size(666, 392)
        Me.S_listBox.TabIndex = 430
        '
        'Label15
        '
        Me.Label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(694, 65)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(75, 18)
        Me.Label15.TabIndex = 428
        Me.Label15.Text = "عنوان الخدمة "
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SNameTextBox
        '
        Me.SNameTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SNameTextBox.Enabled = False
        Me.SNameTextBox.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SNameTextBox.Location = New System.Drawing.Point(3, 62)
        Me.SNameTextBox.MaxLength = 350
        Me.SNameTextBox.Name = "SNameTextBox"
        Me.SNameTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SNameTextBox.Size = New System.Drawing.Size(685, 25)
        Me.SNameTextBox.TabIndex = 429
        Me.SNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DeleteSButton
        '
        Me.DeleteSButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DeleteSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DeleteSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DeleteSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DeleteSButton.Enabled = False
        Me.DeleteSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DeleteSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DeleteSButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DeleteSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DeleteSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DeleteSButton.Image = Global.resturant.My.Resources.Resources.if_cancel_46786
        Me.DeleteSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DeleteSButton.Location = New System.Drawing.Point(672, 261)
        Me.DeleteSButton.Name = "DeleteSButton"
        Me.DeleteSButton.Size = New System.Drawing.Size(110, 34)
        Me.DeleteSButton.TabIndex = 435
        Me.DeleteSButton.Text = "حذف"
        Me.DeleteSButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DeleteSButton.UseVisualStyleBackColor = False
        '
        'NewSButton
        '
        Me.NewSButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NewSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.NewSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.NewSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NewSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.NewSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.NewSButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.NewSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NewSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.NewSButton.Image = Global.resturant.My.Resources.Resources.if_Add_27831
        Me.NewSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NewSButton.Location = New System.Drawing.Point(672, 135)
        Me.NewSButton.Name = "NewSButton"
        Me.NewSButton.Size = New System.Drawing.Size(110, 34)
        Me.NewSButton.TabIndex = 434
        Me.NewSButton.Text = "جديد"
        Me.NewSButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.NewSButton.UseVisualStyleBackColor = False
        '
        'EditSButton
        '
        Me.EditSButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EditSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.EditSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.EditSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EditSButton.Enabled = False
        Me.EditSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.EditSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.EditSButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EditSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EditSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.EditSButton.Image = Global.resturant.My.Resources.Resources.if_icon_136_document_edit_314724
        Me.EditSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EditSButton.Location = New System.Drawing.Point(672, 219)
        Me.EditSButton.Name = "EditSButton"
        Me.EditSButton.Size = New System.Drawing.Size(110, 34)
        Me.EditSButton.TabIndex = 433
        Me.EditSButton.Text = "تعديل"
        Me.EditSButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.EditSButton.UseVisualStyleBackColor = False
        '
        'SaveSButton
        '
        Me.SaveSButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SaveSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.SaveSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SaveSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveSButton.Enabled = False
        Me.SaveSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SaveSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SaveSButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SaveSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SaveSButton.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.SaveSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveSButton.Location = New System.Drawing.Point(672, 177)
        Me.SaveSButton.Name = "SaveSButton"
        Me.SaveSButton.Size = New System.Drawing.Size(110, 34)
        Me.SaveSButton.TabIndex = 432
        Me.SaveSButton.Text = "حفظ"
        Me.SaveSButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SaveSButton.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.S_listBox)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(0, 127)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox1.Size = New System.Drawing.Size(672, 414)
        Me.GroupBox1.TabIndex = 436
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "الخدمات"
        '
        'ExitFormButton
        '
        Me.ExitFormButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.Location = New System.Drawing.Point(675, 504)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(110, 34)
        Me.ExitFormButton.TabIndex = 454
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Confirm_Btn
        '
        Me.Confirm_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Confirm_Btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Confirm_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Confirm_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Confirm_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Confirm_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Confirm_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Confirm_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Confirm_Btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Confirm_Btn.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.Confirm_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Confirm_Btn.Location = New System.Drawing.Point(672, 303)
        Me.Confirm_Btn.Name = "Confirm_Btn"
        Me.Confirm_Btn.Size = New System.Drawing.Size(110, 34)
        Me.Confirm_Btn.TabIndex = 455
        Me.Confirm_Btn.Text = "إختيار "
        Me.Confirm_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Confirm_Btn.UseVisualStyleBackColor = False
        '
        'SearchTextBox
        '
        Me.SearchTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SearchTextBox.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchTextBox.Location = New System.Drawing.Point(41, 94)
        Me.SearchTextBox.Name = "SearchTextBox"
        Me.SearchTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SearchTextBox.Size = New System.Drawing.Size(647, 25)
        Me.SearchTextBox.TabIndex = 456
        '
        'SearchClearButton
        '
        Me.SearchClearButton.Enabled = False
        Me.SearchClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SearchClearButton.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchClearButton.Location = New System.Drawing.Point(3, 94)
        Me.SearchClearButton.Name = "SearchClearButton"
        Me.SearchClearButton.Size = New System.Drawing.Size(34, 25)
        Me.SearchClearButton.TabIndex = 457
        Me.SearchClearButton.Text = "×"
        Me.SearchClearButton.UseVisualStyleBackColor = True
        '
        'SearchLabel
        '
        Me.SearchLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SearchLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.SearchLabel.Location = New System.Drawing.Point(694, 99)
        Me.SearchLabel.Name = "SearchLabel"
        Me.SearchLabel.Size = New System.Drawing.Size(75, 18)
        Me.SearchLabel.TabIndex = 458
        Me.SearchLabel.Text = "⌕ بحث"
        Me.SearchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'HeaderPanel
        '
        Me.HeaderPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.HeaderPanel.Controls.Add(Me.Title_Label)
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Size = New System.Drawing.Size(783, 56)
        Me.HeaderPanel.TabIndex = 459
        '
        'Title_Label
        '
        Me.Title_Label.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Title_Label.AutoSize = True
        Me.Title_Label.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Title_Label.ForeColor = System.Drawing.Color.White
        Me.Title_Label.Location = New System.Drawing.Point(690, 15)
        Me.Title_Label.Name = "Title_Label"
        Me.Title_Label.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Title_Label.Size = New System.Drawing.Size(77, 25)
        Me.Title_Label.TabIndex = 0
        Me.Title_Label.Text = "الخدمات"
        '
        'AG_Projects
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.SystemColors.MenuBar
        Me.ClientSize = New System.Drawing.Size(783, 543)
        Me.ControlBox = False
        Me.Controls.Add(Me.Confirm_Btn)
        Me.Controls.Add(Me.SearchLabel)
        Me.Controls.Add(Me.SearchClearButton)
        Me.Controls.Add(Me.SearchTextBox)
        Me.Controls.Add(Me.HeaderPanel)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DeleteSButton)
        Me.Controls.Add(Me.NewSButton)
        Me.Controls.Add(Me.EditSButton)
        Me.Controls.Add(Me.SaveSButton)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.SNameTextBox)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AG_Projects"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الخدمات"
        Me.GroupBox1.ResumeLayout(False)
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents S_listBox As System.Windows.Forms.ListBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents SNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DeleteSButton As System.Windows.Forms.Button
    Friend WithEvents NewSButton As System.Windows.Forms.Button
    Friend WithEvents EditSButton As System.Windows.Forms.Button
    Friend WithEvents SaveSButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Confirm_Btn As System.Windows.Forms.Button
    Friend WithEvents SearchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SearchClearButton As System.Windows.Forms.Button
    Friend WithEvents SearchLabel As System.Windows.Forms.Label
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents Title_Label As System.Windows.Forms.Label
End Class
