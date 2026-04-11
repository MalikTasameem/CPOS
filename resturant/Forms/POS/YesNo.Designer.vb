<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class YesNo
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
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.TopTitle_LB = New System.Windows.Forms.Label()
        Me.No_Btn = New System.Windows.Forms.Button()
        Me.Msg_Lb = New System.Windows.Forms.Label()
        Me.Yes_Btn = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TitleBar_Panel.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TitleBar_Panel.Size = New System.Drawing.Size(521, 40)
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
        Me.TopTitle_LB.Location = New System.Drawing.Point(410, 9)
        Me.TopTitle_LB.Name = "TopTitle_LB"
        Me.TopTitle_LB.Size = New System.Drawing.Size(80, 21)
        Me.TopTitle_LB.TabIndex = 0
        Me.TopTitle_LB.Tag = "TITLE_TRANSPARENT"
        Me.TopTitle_LB.Text = "رسالة تأكيد"
        '
        'No_Btn
        '
        Me.No_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.No_Btn.BackColor = System.Drawing.SystemColors.Control
        Me.No_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.No_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.No_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 20.25!, System.Drawing.FontStyle.Bold)
        Me.No_Btn.ForeColor = System.Drawing.Color.DarkRed
        Me.No_Btn.Location = New System.Drawing.Point(77, 256)
        Me.No_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.No_Btn.Name = "No_Btn"
        Me.No_Btn.Size = New System.Drawing.Size(136, 60)
        Me.No_Btn.TabIndex = 449
        Me.No_Btn.Tag = "DELETE"
        Me.No_Btn.Text = "لا"
        Me.No_Btn.UseVisualStyleBackColor = False
        '
        'Msg_Lb
        '
        Me.Msg_Lb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Msg_Lb.Location = New System.Drawing.Point(2, 42)
        Me.Msg_Lb.Name = "Msg_Lb"
        Me.Msg_Lb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Msg_Lb.Size = New System.Drawing.Size(519, 112)
        Me.Msg_Lb.TabIndex = 451
        Me.Msg_Lb.Tag = "label"
        Me.Msg_Lb.Text = "Label1"
        Me.Msg_Lb.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Yes_Btn
        '
        Me.Yes_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Yes_Btn.BackColor = System.Drawing.SystemColors.Control
        Me.Yes_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Yes_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Yes_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 20.25!, System.Drawing.FontStyle.Bold)
        Me.Yes_Btn.ForeColor = System.Drawing.Color.DarkGreen
        Me.Yes_Btn.Location = New System.Drawing.Point(295, 256)
        Me.Yes_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Yes_Btn.Name = "Yes_Btn"
        Me.Yes_Btn.Size = New System.Drawing.Size(136, 60)
        Me.Yes_Btn.TabIndex = 452
        Me.Yes_Btn.Tag = "SAVE"
        Me.Yes_Btn.Text = "نعم"
        Me.Yes_Btn.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = Global.resturant.My.Resources.Resources.iconfinder_icon_29_information_315150
        Me.PictureBox1.Location = New System.Drawing.Point(214, 157)
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
        Me.ClientSize = New System.Drawing.Size(521, 328)
        Me.ControlBox = False
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Yes_Btn)
        Me.Controls.Add(Me.Msg_Lb)
        Me.Controls.Add(Me.No_Btn)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(8, 9, 8, 9)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "YesNo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "."
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
    Friend WithEvents TopTitle_LB As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button

    Friend WithEvents No_Btn As System.Windows.Forms.Button
    Friend WithEvents Msg_Lb As System.Windows.Forms.Label
    Friend WithEvents Yes_Btn As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class