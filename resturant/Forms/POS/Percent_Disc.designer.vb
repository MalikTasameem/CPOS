<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Percent_Disc
    Inherits Base_Form

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
        Me.Discount_txt = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.UpdateGBButton = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Discount_txt
        '
        Me.Discount_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Discount_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Discount_txt.Font = New System.Drawing.Font("Stencil", 20.0!)
        Me.Discount_txt.ForeColor = System.Drawing.Color.Black
        Me.Discount_txt.Location = New System.Drawing.Point(81, 12)
        Me.Discount_txt.MaxLength = 200
        Me.Discount_txt.Name = "Discount_txt"
        Me.Discount_txt.Size = New System.Drawing.Size(144, 39)
        Me.Discount_txt.TabIndex = 623
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(231, 12)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(88, 37)
        Me.Label13.TabIndex = 624
        Me.Label13.Text = "النسبة"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'UpdateGBButton
        '
        Me.UpdateGBButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.UpdateGBButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.UpdateGBButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UpdateGBButton.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.UpdateGBButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.UpdateGBButton.Location = New System.Drawing.Point(1, 110)
        Me.UpdateGBButton.Name = "UpdateGBButton"
        Me.UpdateGBButton.Size = New System.Drawing.Size(150, 42)
        Me.UpdateGBButton.TabIndex = 621
        Me.UpdateGBButton.Text = "تطبيق Enter"
        Me.UpdateGBButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.UpdateGBButton.UseVisualStyleBackColor = True
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(232, 114)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ExitFormButton.Size = New System.Drawing.Size(124, 38)
        Me.ExitFormButton.TabIndex = 622
        Me.ExitFormButton.Text = "رجوع Esc"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 20.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(37, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 37)
        Me.Label1.TabIndex = 625
        Me.Label1.Text = "%"
        '
        'Percent_Disc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 26.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(357, 152)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Discount_txt)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.UpdateGBButton)
        Me.Margin = New System.Windows.Forms.Padding(5, 12, 5, 12)
        Me.Name = "Percent_Disc"
        Me.Text = "تخفيض نسخة"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Discount_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents UpdateGBButton As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
