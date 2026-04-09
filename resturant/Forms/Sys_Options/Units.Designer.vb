<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Units
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
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
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
        Me.TitleBar_Panel.SuspendLayout()
        Me.FieldsPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.TitleBar_Panel.Controls.Add(Me.ExitFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.TopTitle_LB)
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(616, 40)
        Me.TitleBar_Panel.TabIndex = 999
        Me.TitleBar_Panel.Tag = "HEADER"
        '
        'ExitFormButton
        '
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.ExitFormButton.FlatAppearance.BorderSize = 0
        Me.ExitFormButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitFormButton.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.Color.White
        Me.ExitFormButton.Location = New System.Drawing.Point(0, 0)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(45, 40)
        Me.ExitFormButton.TabIndex = 3
        Me.ExitFormButton.Tag = "APP_CONTROL"
        Me.ExitFormButton.Text = "X"
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'TopTitle_LB
        '
        Me.TopTitle_LB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TopTitle_LB.AutoSize = True
        Me.TopTitle_LB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.TopTitle_LB.ForeColor = System.Drawing.Color.White
        Me.TopTitle_LB.Location = New System.Drawing.Point(526, 9)
        Me.TopTitle_LB.Name = "TopTitle_LB"
        Me.TopTitle_LB.Size = New System.Drawing.Size(78, 21)
        Me.TopTitle_LB.TabIndex = 0
        Me.TopTitle_LB.Tag = "TITLE_TRANSPARENT"
        Me.TopTitle_LB.Text = "الوحـــــدات"
        '
        'Un_listBox
        '
        Me.Un_listBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Un_listBox.FormattingEnabled = True
        Me.Un_listBox.ItemHeight = 21
        Me.Un_listBox.Location = New System.Drawing.Point(5, 46)
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
        Me.FieldsPanel.Location = New System.Drawing.Point(232, 46)
        Me.FieldsPanel.Name = "FieldsPanel"
        Me.FieldsPanel.Size = New System.Drawing.Size(378, 150)
        Me.FieldsPanel.TabIndex = 438
        '
        'Un_Cargo_txt
        '
        Me.Un_Cargo_txt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Un_Cargo_txt.Location = New System.Drawing.Point(5, 90)
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
        Me.Label15.Location = New System.Drawing.Point(278, 29)
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
        Me.Label17.Location = New System.Drawing.Point(267, 93)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(99, 21)
        Me.Label17.TabIndex = 405
        Me.Label17.Text = "حمولة الوحدة "
        '
        'Un_Name_txt
        '
        Me.Un_Name_txt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Un_Name_txt.Location = New System.Drawing.Point(5, 26)
        Me.Un_Name_txt.Name = "Un_Name_txt"
        Me.Un_Name_txt.Size = New System.Drawing.Size(256, 29)
        Me.Un_Name_txt.TabIndex = 406
        Me.Un_Name_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DeleteButton
        '
        Me.DeleteButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DeleteButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DeleteButton.Enabled = False
        Me.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DeleteButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.DeleteButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DeleteButton.Location = New System.Drawing.Point(232, 300)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(90, 45)
        Me.DeleteButton.TabIndex = 442
        Me.DeleteButton.Tag = "DELETE"
        Me.DeleteButton.Text = "حـذف"
        Me.DeleteButton.UseVisualStyleBackColor = False
        '
        'NewEmpButton
        '
        Me.NewEmpButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.NewEmpButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NewEmpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.NewEmpButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.NewEmpButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.NewEmpButton.Location = New System.Drawing.Point(520, 300)
        Me.NewEmpButton.Name = "NewEmpButton"
        Me.NewEmpButton.Size = New System.Drawing.Size(90, 45)
        Me.NewEmpButton.TabIndex = 441
        Me.NewEmpButton.Text = "جديد"
        Me.NewEmpButton.UseVisualStyleBackColor = False
        '
        'EditEmpButton
        '
        Me.EditEmpButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.EditEmpButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EditEmpButton.Enabled = False
        Me.EditEmpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EditEmpButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.EditEmpButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.EditEmpButton.Location = New System.Drawing.Point(328, 300)
        Me.EditEmpButton.Name = "EditEmpButton"
        Me.EditEmpButton.Size = New System.Drawing.Size(90, 45)
        Me.EditEmpButton.TabIndex = 440
        Me.EditEmpButton.Text = "تعديل"
        Me.EditEmpButton.UseVisualStyleBackColor = False
        '
        'SaveButton
        '
        Me.SaveButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveButton.Enabled = False
        Me.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SaveButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.SaveButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SaveButton.Location = New System.Drawing.Point(424, 300)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(90, 45)
        Me.SaveButton.TabIndex = 439
        Me.SaveButton.Tag = "SAVE"
        Me.SaveButton.Text = "حفـظ"
        Me.SaveButton.UseVisualStyleBackColor = False
        '
        'Units
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(616, 519)
        Me.ControlBox = False
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.NewEmpButton)
        Me.Controls.Add(Me.EditEmpButton)
        Me.Controls.Add(Me.SaveButton)
        Me.Controls.Add(Me.Un_listBox)
        Me.Controls.Add(Me.FieldsPanel)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Units"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الوحدات"
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        Me.FieldsPanel.ResumeLayout(False)
        Me.FieldsPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
    Friend WithEvents TopTitle_LB As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button

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
End Class