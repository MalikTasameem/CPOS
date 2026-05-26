<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Normal_Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Normal_Form))
        Me.S_listBox = New System.Windows.Forms.ListBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.SNameTextBox = New System.Windows.Forms.TextBox()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.DeleteSButton = New System.Windows.Forms.Button()
        Me.NewSButton = New System.Windows.Forms.Button()
        Me.EditSButton = New System.Windows.Forms.Button()
        Me.SaveSButton = New System.Windows.Forms.Button()
        Me.SEARCH_txt = New System.Windows.Forms.TextBox()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.Title_Label = New System.Windows.Forms.Label()
        Me.SearchLabel = New System.Windows.Forms.Label()
        Me.HeaderPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'S_listBox
        '
        Me.S_listBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.S_listBox.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.S_listBox.FormattingEnabled = True
        Me.S_listBox.ItemHeight = 17
        Me.S_listBox.Location = New System.Drawing.Point(16, 137)
        Me.S_listBox.Name = "S_listBox"
        Me.S_listBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.S_listBox.Size = New System.Drawing.Size(348, 242)
        Me.S_listBox.TabIndex = 430
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(370, 75)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label15.Size = New System.Drawing.Size(36, 15)
        Me.Label15.TabIndex = 428
        Me.Label15.Text = "الإسم"
        '
        'SNameTextBox
        '
        Me.SNameTextBox.Enabled = False
        Me.SNameTextBox.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SNameTextBox.Location = New System.Drawing.Point(16, 72)
        Me.SNameTextBox.MaxLength = 350
        Me.SNameTextBox.Name = "SNameTextBox"
        Me.SNameTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SNameTextBox.Size = New System.Drawing.Size(348, 25)
        Me.SNameTextBox.TabIndex = 429
        Me.SNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatAppearance.BorderSize = 0
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.Color.White
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ExitFormButton.Location = New System.Drawing.Point(382, 372)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(130, 34)
        Me.ExitFormButton.TabIndex = 454
        Me.ExitFormButton.Text = "↩ رجوع"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'DeleteSButton
        '
        Me.DeleteSButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.DeleteSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.DeleteSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DeleteSButton.Enabled = False
        Me.DeleteSButton.FlatAppearance.BorderSize = 0
        Me.DeleteSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(185, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(28, Byte), Integer))
        Me.DeleteSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.DeleteSButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DeleteSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.DeleteSButton.ForeColor = System.Drawing.Color.White
        Me.DeleteSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.DeleteSButton.Location = New System.Drawing.Point(382, 270)
        Me.DeleteSButton.Name = "DeleteSButton"
        Me.DeleteSButton.Size = New System.Drawing.Size(130, 34)
        Me.DeleteSButton.TabIndex = 435
        Me.DeleteSButton.Text = "✕ حذف"
        Me.DeleteSButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.DeleteSButton.UseVisualStyleBackColor = False
        '
        'NewSButton
        '
        Me.NewSButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.NewSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.NewSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NewSButton.FlatAppearance.BorderSize = 0
        Me.NewSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.NewSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.NewSButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.NewSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.NewSButton.ForeColor = System.Drawing.Color.White
        Me.NewSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.NewSButton.Location = New System.Drawing.Point(382, 142)
        Me.NewSButton.Name = "NewSButton"
        Me.NewSButton.Size = New System.Drawing.Size(130, 34)
        Me.NewSButton.TabIndex = 434
        Me.NewSButton.Text = "＋ جديد"
        Me.NewSButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.NewSButton.UseVisualStyleBackColor = False
        '
        'EditSButton
        '
        Me.EditSButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(13, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(253, Byte), Integer))
        Me.EditSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.EditSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EditSButton.Enabled = False
        Me.EditSButton.FlatAppearance.BorderSize = 0
        Me.EditSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(11, Byte), Integer), CType(CType(94, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.EditSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.EditSButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EditSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.EditSButton.ForeColor = System.Drawing.Color.White
        Me.EditSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.EditSButton.Location = New System.Drawing.Point(382, 226)
        Me.EditSButton.Name = "EditSButton"
        Me.EditSButton.Size = New System.Drawing.Size(130, 34)
        Me.EditSButton.TabIndex = 433
        Me.EditSButton.Text = "✎ تعديل"
        Me.EditSButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.EditSButton.UseVisualStyleBackColor = False
        '
        'SaveSButton
        '
        Me.SaveSButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.SaveSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.SaveSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveSButton.Enabled = False
        Me.SaveSButton.FlatAppearance.BorderSize = 0
        Me.SaveSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.SaveSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(95, Byte), Integer))
        Me.SaveSButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SaveSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SaveSButton.ForeColor = System.Drawing.Color.White
        Me.SaveSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.SaveSButton.Location = New System.Drawing.Point(382, 184)
        Me.SaveSButton.Name = "SaveSButton"
        Me.SaveSButton.Size = New System.Drawing.Size(130, 34)
        Me.SaveSButton.TabIndex = 432
        Me.SaveSButton.Text = "✓ حفظ"
        Me.SaveSButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.SaveSButton.UseVisualStyleBackColor = False
        '
        'SEARCH_txt
        '
        Me.SEARCH_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.SEARCH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SEARCH_txt.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SEARCH_txt.Location = New System.Drawing.Point(16, 104)
        Me.SEARCH_txt.MaxLength = 350
        Me.SEARCH_txt.Name = "SEARCH_txt"
        Me.SEARCH_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SEARCH_txt.Size = New System.Drawing.Size(348, 25)
        Me.SEARCH_txt.TabIndex = 455
        Me.SEARCH_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'HeaderPanel
        '
        Me.HeaderPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.HeaderPanel.Controls.Add(Me.Title_Label)
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Size = New System.Drawing.Size(540, 56)
        Me.HeaderPanel.TabIndex = 456
        '
        'Title_Label
        '
        Me.Title_Label.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Title_Label.AutoSize = True
        Me.Title_Label.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Title_Label.ForeColor = System.Drawing.Color.White
        Me.Title_Label.Location = New System.Drawing.Point(407, 15)
        Me.Title_Label.Name = "Title_Label"
        Me.Title_Label.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Title_Label.Size = New System.Drawing.Size(117, 25)
        Me.Title_Label.TabIndex = 0
        Me.Title_Label.Text = "إدارة القائمة"
        '
        'SearchLabel
        '
        Me.SearchLabel.AutoSize = True
        Me.SearchLabel.BackColor = System.Drawing.Color.Transparent
        Me.SearchLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(81, Byte), Integer))
        Me.SearchLabel.Location = New System.Drawing.Point(370, 109)
        Me.SearchLabel.Name = "SearchLabel"
        Me.SearchLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SearchLabel.Size = New System.Drawing.Size(40, 15)
        Me.SearchLabel.TabIndex = 457
        Me.SearchLabel.Text = "⌕ بحث"
        '
        'Cities
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(540, 422)
        Me.ControlBox = False
        Me.Controls.Add(Me.SearchLabel)
        Me.Controls.Add(Me.HeaderPanel)
        Me.Controls.Add(Me.SEARCH_txt)
        Me.Controls.Add(Me.S_listBox)
        Me.Controls.Add(Me.ExitFormButton)
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
        Me.Name = "Cities"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إدارة المدن"
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
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents SEARCH_txt As System.Windows.Forms.TextBox
    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents Title_Label As System.Windows.Forms.Label
    Friend WithEvents SearchLabel As System.Windows.Forms.Label
End Class
