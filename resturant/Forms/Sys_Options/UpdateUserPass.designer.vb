<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UpdateUserPass
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
        Me.OldPassTextBox = New System.Windows.Forms.TextBox()
        Me.NewPassTextBox = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.UpdateGBButton = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.ShowPassButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'OldPassTextBox
        '
        Me.OldPassTextBox.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OldPassTextBox.Location = New System.Drawing.Point(55, 5)
        Me.OldPassTextBox.Name = "OldPassTextBox"
        Me.OldPassTextBox.Size = New System.Drawing.Size(262, 29)
        Me.OldPassTextBox.TabIndex = 2
        Me.OldPassTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OldPassTextBox.UseSystemPasswordChar = True
        '
        'NewPassTextBox
        '
        Me.NewPassTextBox.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NewPassTextBox.Location = New System.Drawing.Point(55, 36)
        Me.NewPassTextBox.Name = "NewPassTextBox"
        Me.NewPassTextBox.Size = New System.Drawing.Size(262, 29)
        Me.NewPassTextBox.TabIndex = 3
        Me.NewPassTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NewPassTextBox.UseSystemPasswordChar = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(321, 10)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(134, 21)
        Me.Label6.TabIndex = 153
        Me.Label6.Text = "كلمة المرور السابقة"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(321, 40)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 21)
        Me.Label1.TabIndex = 154
        Me.Label1.Text = "كلمة المرور الجديدة"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'UpdateGBButton
        '
        Me.UpdateGBButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.UpdateGBButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.UpdateGBButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UpdateGBButton.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.UpdateGBButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.UpdateGBButton.Location = New System.Drawing.Point(112, 71)
        Me.UpdateGBButton.Name = "UpdateGBButton"
        Me.UpdateGBButton.Size = New System.Drawing.Size(150, 42)
        Me.UpdateGBButton.TabIndex = 155
        Me.UpdateGBButton.Text = "تأكيد التعديل"
        Me.UpdateGBButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.UpdateGBButton.UseVisualStyleBackColor = True
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.Location = New System.Drawing.Point(397, 84)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(59, 38)
        Me.ExitFormButton.TabIndex = 156
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'ShowPassButton
        '
        Me.ShowPassButton.BackColor = System.Drawing.SystemColors.Window
        Me.ShowPassButton.BackgroundImage = Global.resturant.My.Resources.Resources.show_hide_password
        Me.ShowPassButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ShowPassButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MistyRose
        Me.ShowPassButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ShowPassButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.25!)
        Me.ShowPassButton.ForeColor = System.Drawing.Color.DarkRed
        Me.ShowPassButton.Location = New System.Drawing.Point(14, 22)
        Me.ShowPassButton.Name = "ShowPassButton"
        Me.ShowPassButton.Size = New System.Drawing.Size(38, 25)
        Me.ShowPassButton.TabIndex = 405
        Me.ShowPassButton.UseVisualStyleBackColor = False
        '
        'UpdateUserPass
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(457, 125)
        Me.ControlBox = False
        Me.Controls.Add(Me.ShowPassButton)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.UpdateGBButton)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.NewPassTextBox)
        Me.Controls.Add(Me.OldPassTextBox)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "UpdateUserPass"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = ""
        Me.Text = "تعديل كلمة المرور"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OldPassTextBox As System.Windows.Forms.TextBox
    Friend WithEvents NewPassTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UpdateGBButton As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents ShowPassButton As System.Windows.Forms.Button
End Class
