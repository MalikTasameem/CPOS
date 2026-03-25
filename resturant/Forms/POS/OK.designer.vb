<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OK
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
        Me.Msg_Lb = New System.Windows.Forms.Label()
        Me.Yes_Btn = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Msg_Lb
        '
        Me.Msg_Lb.ForeColor = System.Drawing.Color.Red
        Me.Msg_Lb.Location = New System.Drawing.Point(2, 2)
        Me.Msg_Lb.Name = "Msg_Lb"
        Me.Msg_Lb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Msg_Lb.Size = New System.Drawing.Size(519, 114)
        Me.Msg_Lb.TabIndex = 451
        Me.Msg_Lb.Text = "رسالة تنويه !!"
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
        Me.Yes_Btn.Location = New System.Drawing.Point(184, 234)
        Me.Yes_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Yes_Btn.Name = "Yes_Btn"
        Me.Yes_Btn.Size = New System.Drawing.Size(136, 75)
        Me.Yes_Btn.TabIndex = 452
        Me.Yes_Btn.Tag = "."
        Me.Yes_Btn.Text = "حسنا"
        Me.Yes_Btn.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = Global.resturant.My.Resources.Resources.iconfinder_exclamation_red_69106
        Me.PictureBox1.Location = New System.Drawing.Point(216, 134)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(75, 75)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 583
        Me.PictureBox1.TabStop = False
        '
        'OK
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 37.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(521, 335)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Yes_Btn)
        Me.Controls.Add(Me.Msg_Lb)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(8, 9, 8, 9)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "OK"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "."
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Msg_Lb As System.Windows.Forms.Label
    Friend WithEvents Yes_Btn As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
