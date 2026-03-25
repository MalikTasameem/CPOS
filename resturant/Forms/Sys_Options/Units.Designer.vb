<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Units
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Units))
        Me.Un_listBox = New System.Windows.Forms.ListBox()
        Me.FieldsPanel = New System.Windows.Forms.Panel()
        Me.Un_Cargo_txt = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Un_Name_txt = New System.Windows.Forms.TextBox()
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.NewEmpButton = New System.Windows.Forms.Button()
        Me.EditEmpButton = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.FieldsPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Un_listBox
        '
        Me.Un_listBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Un_listBox.FormattingEnabled = True
        Me.Un_listBox.ItemHeight = 21
        Me.Un_listBox.Location = New System.Drawing.Point(5, 6)
        Me.Un_listBox.Name = "Un_listBox"
        Me.Un_listBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Un_listBox.Size = New System.Drawing.Size(221, 466)
        Me.Un_listBox.TabIndex = 437
        '
        'FieldsPanel
        '
        Me.FieldsPanel.Controls.Add(Me.Un_Cargo_txt)
        Me.FieldsPanel.Controls.Add(Me.Label15)
        Me.FieldsPanel.Controls.Add(Me.Label17)
        Me.FieldsPanel.Controls.Add(Me.Un_Name_txt)
        Me.FieldsPanel.Enabled = False
        Me.FieldsPanel.Location = New System.Drawing.Point(243, 59)
        Me.FieldsPanel.Name = "FieldsPanel"
        Me.FieldsPanel.Size = New System.Drawing.Size(271, 174)
        Me.FieldsPanel.TabIndex = 438
        '
        'Un_Cargo_txt
        '
        Me.Un_Cargo_txt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Un_Cargo_txt.Location = New System.Drawing.Point(5, 142)
        Me.Un_Cargo_txt.Name = "Un_Cargo_txt"
        Me.Un_Cargo_txt.Size = New System.Drawing.Size(256, 29)
        Me.Un_Cargo_txt.TabIndex = 407
        Me.Un_Cargo_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(177, 13)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(82, 21)
        Me.Label15.TabIndex = 403
        Me.Label15.Text = "اسم الوحدة"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(160, 117)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(99, 21)
        Me.Label17.TabIndex = 405
        Me.Label17.Text = "حمولة الوحدة "
        '
        'Un_Name_txt
        '
        Me.Un_Name_txt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Un_Name_txt.Location = New System.Drawing.Point(5, 37)
        Me.Un_Name_txt.Name = "Un_Name_txt"
        Me.Un_Name_txt.Size = New System.Drawing.Size(256, 29)
        Me.Un_Name_txt.TabIndex = 406
        Me.Un_Name_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DeleteButton
        '
        Me.DeleteButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DeleteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DeleteButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DeleteButton.Enabled = False
        Me.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DeleteButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.DeleteButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DeleteButton.Image = Global.resturant.My.Resources.Resources.if_cancel_46786
        Me.DeleteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DeleteButton.Location = New System.Drawing.Point(232, 260)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(90, 45)
        Me.DeleteButton.TabIndex = 442
        Me.DeleteButton.Text = "حـذف"
        Me.DeleteButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DeleteButton.UseVisualStyleBackColor = False
        '
        'NewEmpButton
        '
        Me.NewEmpButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.NewEmpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.NewEmpButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NewEmpButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.NewEmpButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.NewEmpButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.NewEmpButton.Image = CType(resources.GetObject("NewEmpButton.Image"), System.Drawing.Image)
        Me.NewEmpButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NewEmpButton.Location = New System.Drawing.Point(520, 260)
        Me.NewEmpButton.Name = "NewEmpButton"
        Me.NewEmpButton.Size = New System.Drawing.Size(90, 45)
        Me.NewEmpButton.TabIndex = 441
        Me.NewEmpButton.Text = "جديد "
        Me.NewEmpButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.NewEmpButton.UseVisualStyleBackColor = False
        '
        'EditEmpButton
        '
        Me.EditEmpButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.EditEmpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.EditEmpButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EditEmpButton.Enabled = False
        Me.EditEmpButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.EditEmpButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.EditEmpButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.EditEmpButton.Image = Global.resturant.My.Resources.Resources.if_icon_136_document_edit_314724
        Me.EditEmpButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EditEmpButton.Location = New System.Drawing.Point(328, 260)
        Me.EditEmpButton.Name = "EditEmpButton"
        Me.EditEmpButton.Size = New System.Drawing.Size(90, 45)
        Me.EditEmpButton.TabIndex = 440
        Me.EditEmpButton.Text = "تعديل"
        Me.EditEmpButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.EditEmpButton.UseVisualStyleBackColor = False
        '
        'SaveButton
        '
        Me.SaveButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.SaveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveButton.Enabled = False
        Me.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SaveButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SaveButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SaveButton.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveButton.Location = New System.Drawing.Point(424, 260)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(90, 45)
        Me.SaveButton.TabIndex = 439
        Me.SaveButton.Text = "حفـظ "
        Me.SaveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SaveButton.UseVisualStyleBackColor = False
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
        Me.ExitFormButton.Location = New System.Drawing.Point(480, 430)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(133, 47)
        Me.ExitFormButton.TabIndex = 454
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Units
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(616, 479)
        Me.ControlBox = False
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.NewEmpButton)
        Me.Controls.Add(Me.EditEmpButton)
        Me.Controls.Add(Me.SaveButton)
        Me.Controls.Add(Me.Un_listBox)
        Me.Controls.Add(Me.FieldsPanel)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Units"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الوحدات"
        Me.FieldsPanel.ResumeLayout(False)
        Me.FieldsPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Un_listBox As System.Windows.Forms.ListBox
    Friend WithEvents FieldsPanel As System.Windows.Forms.Panel
    Friend WithEvents Un_Cargo_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Un_Name_txt As System.Windows.Forms.TextBox
    Friend WithEvents DeleteButton As System.Windows.Forms.Button
    Friend WithEvents NewEmpButton As System.Windows.Forms.Button
    Friend WithEvents EditEmpButton As System.Windows.Forms.Button
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
End Class
