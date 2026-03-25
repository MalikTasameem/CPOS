<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class YesNo
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
        Me.No_Btn = New System.Windows.Forms.Button()
        Me.Msg_Lb = New System.Windows.Forms.Label()
        Me.Yes_Btn = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'No_Btn
        '
        Me.No_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.No_Btn.BackColor = System.Drawing.SystemColors.Control
        Me.No_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.No_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.No_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 20.25!, System.Drawing.FontStyle.Bold)
        Me.No_Btn.ForeColor = System.Drawing.Color.DarkRed
        Me.No_Btn.Location = New System.Drawing.Point(75, 206)
        Me.No_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.No_Btn.Name = "No_Btn"
        Me.No_Btn.Size = New System.Drawing.Size(136, 75)
        Me.No_Btn.TabIndex = 449
        Me.No_Btn.Tag = "."
        Me.No_Btn.Text = "لا"
        Me.No_Btn.UseVisualStyleBackColor = False
        '
        'Msg_Lb
        '
        Me.Msg_Lb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Msg_Lb.Location = New System.Drawing.Point(2, 2)
        Me.Msg_Lb.Name = "Msg_Lb"
        Me.Msg_Lb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Msg_Lb.Size = New System.Drawing.Size(519, 112)
        Me.Msg_Lb.TabIndex = 451
        Me.Msg_Lb.Text = "Label1"
        Me.Msg_Lb.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Yes_Btn
        '
        Me.Yes_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Yes_Btn.BackColor = System.Drawing.SystemColors.Control
        Me.Yes_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Yes_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Yes_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 20.25!, System.Drawing.FontStyle.Bold)
        Me.Yes_Btn.ForeColor = System.Drawing.Color.DarkRed
        Me.Yes_Btn.Location = New System.Drawing.Point(293, 206)
        Me.Yes_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Yes_Btn.Name = "Yes_Btn"
        Me.Yes_Btn.Size = New System.Drawing.Size(136, 75)
        Me.Yes_Btn.TabIndex = 452
        Me.Yes_Btn.Tag = "."
        Me.Yes_Btn.Text = "نعم"
        Me.Yes_Btn.UseVisualStyleBackColor = False
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.Location = New System.Drawing.Point(458, 280)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(63, 54)
        Me.ExitFormButton.TabIndex = 450
        Me.ExitFormButton.Text = " "
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = Global.resturant.My.Resources.Resources.iconfinder_icon_29_information_315150
        Me.PictureBox1.Location = New System.Drawing.Point(211, 117)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(75, 75)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 584
        Me.PictureBox1.TabStop = False
        '
        'YesNo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 37.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(521, 335)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Yes_Btn)
        Me.Controls.Add(Me.Msg_Lb)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.No_Btn)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(8, 9, 8, 9)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "YesNo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "."
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents No_Btn As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Msg_Lb As System.Windows.Forms.Label
    Friend WithEvents Yes_Btn As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
