<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SB_TablesMenu
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

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.MinFormButton = New System.Windows.Forms.Button()
        Me.MaxFormButton = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
        Me.NONE_TB_btn = New System.Windows.Forms.Button()
        Me.Button51 = New System.Windows.Forms.Button()
        Me.Button50 = New System.Windows.Forms.Button()
        Me.TBPanel = New System.Windows.Forms.Panel()
        Me.TitleBar_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.TitleBar_Panel.Controls.Add(Me.MinFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.MaxFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.ExitFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.TopTitle_LB)
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(747, 40)
        Me.TitleBar_Panel.TabIndex = 999
        Me.TitleBar_Panel.Tag = "HEADER"
        '
        'MinFormButton
        '
        Me.MinFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MinFormButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.MinFormButton.FlatAppearance.BorderSize = 0
        Me.MinFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MinFormButton.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.MinFormButton.ForeColor = System.Drawing.Color.White
        Me.MinFormButton.Location = New System.Drawing.Point(90, 0)
        Me.MinFormButton.Name = "MinFormButton"
        Me.MinFormButton.Size = New System.Drawing.Size(45, 40)
        Me.MinFormButton.TabIndex = 1
        Me.MinFormButton.Tag = "APP_CONTROL"
        Me.MinFormButton.Text = "ـ"
        Me.MinFormButton.UseVisualStyleBackColor = False
        '
        'MaxFormButton
        '
        Me.MaxFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MaxFormButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.MaxFormButton.FlatAppearance.BorderSize = 0
        Me.MaxFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MaxFormButton.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.MaxFormButton.ForeColor = System.Drawing.Color.White
        Me.MaxFormButton.Location = New System.Drawing.Point(45, 0)
        Me.MaxFormButton.Name = "MaxFormButton"
        Me.MaxFormButton.Size = New System.Drawing.Size(45, 40)
        Me.MaxFormButton.TabIndex = 2
        Me.MaxFormButton.Tag = "APP_CONTROL"
        Me.MaxFormButton.Text = "⬜"
        Me.MaxFormButton.UseVisualStyleBackColor = False
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
        Me.TopTitle_LB.Location = New System.Drawing.Point(620, 9)
        Me.TopTitle_LB.Name = "TopTitle_LB"
        Me.TopTitle_LB.Size = New System.Drawing.Size(93, 21)
        Me.TopTitle_LB.TabIndex = 0
        Me.TopTitle_LB.Tag = "TITLE_TRANSPARENT"
        Me.TopTitle_LB.Text = "تحديد طاولــة"
        '
        'NONE_TB_btn
        '
        Me.NONE_TB_btn.BackColor = System.Drawing.SystemColors.Control
        Me.NONE_TB_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NONE_TB_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.NONE_TB_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NONE_TB_btn.ForeColor = System.Drawing.Color.Black
        Me.NONE_TB_btn.Location = New System.Drawing.Point(2, 42)
        Me.NONE_TB_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.NONE_TB_btn.Name = "NONE_TB_btn"
        Me.NONE_TB_btn.Size = New System.Drawing.Size(221, 39)
        Me.NONE_TB_btn.TabIndex = 554
        Me.NONE_TB_btn.Text = "بدون طاولة"
        Me.NONE_TB_btn.UseVisualStyleBackColor = False
        '
        'Button51
        '
        Me.Button51.BackColor = System.Drawing.SystemColors.Info
        Me.Button51.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button51.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button51.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button51.Location = New System.Drawing.Point(671, 268)
        Me.Button51.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button51.Name = "Button51"
        Me.Button51.Size = New System.Drawing.Size(72, 113)
        Me.Button51.TabIndex = 561
        Me.Button51.Text = "⬇️"
        Me.Button51.UseVisualStyleBackColor = False
        '
        'Button50
        '
        Me.Button50.BackColor = System.Drawing.SystemColors.Info
        Me.Button50.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button50.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button50.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button50.Location = New System.Drawing.Point(671, 149)
        Me.Button50.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button50.Name = "Button50"
        Me.Button50.Size = New System.Drawing.Size(72, 113)
        Me.Button50.TabIndex = 560
        Me.Button50.Text = "⬆️"
        Me.Button50.UseVisualStyleBackColor = False
        '
        'TBPanel
        '
        Me.TBPanel.AutoScroll = True
        Me.TBPanel.BackColor = System.Drawing.Color.Transparent
        Me.TBPanel.Location = New System.Drawing.Point(2, 83)
        Me.TBPanel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TBPanel.Name = "TBPanel"
        Me.TBPanel.Size = New System.Drawing.Size(665, 451)
        Me.TBPanel.TabIndex = 562
        '
        'SB_TablesMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(747, 537)
        Me.ControlBox = False
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.TBPanel)
        Me.Controls.Add(Me.Button51)
        Me.Controls.Add(Me.Button50)
        Me.Controls.Add(Me.NONE_TB_btn)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.Name = "SB_TablesMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تحديد طاولــة"
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
    Friend WithEvents TopTitle_LB As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents MaxFormButton As System.Windows.Forms.Button
    Friend WithEvents MinFormButton As System.Windows.Forms.Button

    Friend WithEvents NONE_TB_btn As System.Windows.Forms.Button
    Friend WithEvents Button51 As System.Windows.Forms.Button
    Friend WithEvents Button50 As System.Windows.Forms.Button
    Friend WithEvents TBPanel As System.Windows.Forms.Panel
End Class