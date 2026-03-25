<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Marketers
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Marketers))
        Me.S_listBox = New System.Windows.Forms.ListBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.SNameTextBox = New System.Windows.Forms.TextBox()
        Me.DeleteSButton = New System.Windows.Forms.Button()
        Me.NewSButton = New System.Windows.Forms.Button()
        Me.EditSButton = New System.Windows.Forms.Button()
        Me.SaveSButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'S_listBox
        '
        Me.S_listBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.S_listBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.S_listBox.FormattingEnabled = True
        Me.S_listBox.ItemHeight = 21
        Me.S_listBox.Location = New System.Drawing.Point(3, 25)
        Me.S_listBox.Name = "S_listBox"
        Me.S_listBox.Size = New System.Drawing.Size(282, 504)
        Me.S_listBox.TabIndex = 430
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(295, 6)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(91, 21)
        Me.Label15.TabIndex = 428
        Me.Label15.Text = "اسم المسوق"
        '
        'SNameTextBox
        '
        Me.SNameTextBox.Enabled = False
        Me.SNameTextBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SNameTextBox.Location = New System.Drawing.Point(3, 2)
        Me.SNameTextBox.MaxLength = 350
        Me.SNameTextBox.Name = "SNameTextBox"
        Me.SNameTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SNameTextBox.Size = New System.Drawing.Size(288, 29)
        Me.SNameTextBox.TabIndex = 429
        Me.SNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DeleteSButton
        '
        Me.DeleteSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DeleteSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DeleteSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DeleteSButton.Enabled = False
        Me.DeleteSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DeleteSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DeleteSButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DeleteSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DeleteSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DeleteSButton.Image = Global.resturant.My.Resources.Resources.if_cancel_46786
        Me.DeleteSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DeleteSButton.Location = New System.Drawing.Point(295, 212)
        Me.DeleteSButton.Name = "DeleteSButton"
        Me.DeleteSButton.Size = New System.Drawing.Size(100, 40)
        Me.DeleteSButton.TabIndex = 435
        Me.DeleteSButton.Text = "حذف"
        Me.DeleteSButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DeleteSButton.UseVisualStyleBackColor = False
        '
        'NewSButton
        '
        Me.NewSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.NewSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.NewSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NewSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.NewSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.NewSButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.NewSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NewSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.NewSButton.Image = Global.resturant.My.Resources.Resources.if_Add_27831
        Me.NewSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NewSButton.Location = New System.Drawing.Point(295, 80)
        Me.NewSButton.Name = "NewSButton"
        Me.NewSButton.Size = New System.Drawing.Size(100, 40)
        Me.NewSButton.TabIndex = 434
        Me.NewSButton.Text = "جديد"
        Me.NewSButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.NewSButton.UseVisualStyleBackColor = False
        '
        'EditSButton
        '
        Me.EditSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.EditSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.EditSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EditSButton.Enabled = False
        Me.EditSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.EditSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.EditSButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.EditSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EditSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.EditSButton.Image = Global.resturant.My.Resources.Resources.if_icon_136_document_edit_314724
        Me.EditSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EditSButton.Location = New System.Drawing.Point(295, 168)
        Me.EditSButton.Name = "EditSButton"
        Me.EditSButton.Size = New System.Drawing.Size(100, 40)
        Me.EditSButton.TabIndex = 433
        Me.EditSButton.Text = "تعديل"
        Me.EditSButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.EditSButton.UseVisualStyleBackColor = False
        '
        'SaveSButton
        '
        Me.SaveSButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.SaveSButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SaveSButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveSButton.Enabled = False
        Me.SaveSButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.SaveSButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SaveSButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SaveSButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveSButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SaveSButton.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.SaveSButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveSButton.Location = New System.Drawing.Point(295, 124)
        Me.SaveSButton.Name = "SaveSButton"
        Me.SaveSButton.Size = New System.Drawing.Size(100, 40)
        Me.SaveSButton.TabIndex = 432
        Me.SaveSButton.Text = "حفظ"
        Me.SaveSButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SaveSButton.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.S_listBox)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 36)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox1.Size = New System.Drawing.Size(288, 532)
        Me.GroupBox1.TabIndex = 436
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "المسوقين"
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.Location = New System.Drawing.Point(294, 530)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(101, 37)
        Me.ExitFormButton.TabIndex = 454
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Marketers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.SystemColors.MenuBar
        Me.ClientSize = New System.Drawing.Size(399, 569)
        Me.ControlBox = False
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
        Me.Name = "Marketers"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "المسوقين"
        Me.GroupBox1.ResumeLayout(False)
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
End Class
