<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SBChangePrice
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
        Me.InfoLb = New System.Windows.Forms.Label()
        Me.ConfermButton = New System.Windows.Forms.Button()
        Me.ClearNumBtn = New System.Windows.Forms.Button()
        Me.NumTextBox = New System.Windows.Forms.TextBox()
        Me.Back_Btn = New System.Windows.Forms.Button()
        Me.Min_SP_Panel = New System.Windows.Forms.Panel()
        Me.Min2_btn = New System.Windows.Forms.Button()
        Me.Min1_btn = New System.Windows.Forms.Button()
        Me.Min_SP_2_txt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Min_SP_txt = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Show_Cash_btn = New System.Windows.Forms.Button()
        Me.PercentBtn = New System.Windows.Forms.Button()
        Me.Min_SP_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'InfoLb
        '
        Me.InfoLb.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InfoLb.Location = New System.Drawing.Point(2, 6)
        Me.InfoLb.Name = "InfoLb"
        Me.InfoLb.Size = New System.Drawing.Size(417, 25)
        Me.InfoLb.TabIndex = 1
        Me.InfoLb.Text = "السعر الجديـــد"
        Me.InfoLb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ConfermButton
        '
        Me.ConfermButton.BackColor = System.Drawing.Color.White
        Me.ConfermButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ConfermButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ConfermButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ConfermButton.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ConfermButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ConfermButton.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.ConfermButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ConfermButton.Location = New System.Drawing.Point(1, 192)
        Me.ConfermButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ConfermButton.Name = "ConfermButton"
        Me.ConfermButton.Size = New System.Drawing.Size(165, 44)
        Me.ConfermButton.TabIndex = 410
        Me.ConfermButton.Text = "تطبيـق"
        Me.ConfermButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ConfermButton.UseVisualStyleBackColor = False
        '
        'ClearNumBtn
        '
        Me.ClearNumBtn.BackColor = System.Drawing.SystemColors.Control
        Me.ClearNumBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ClearNumBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ClearNumBtn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClearNumBtn.ForeColor = System.Drawing.Color.Firebrick
        Me.ClearNumBtn.Location = New System.Drawing.Point(365, 35)
        Me.ClearNumBtn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ClearNumBtn.Name = "ClearNumBtn"
        Me.ClearNumBtn.Size = New System.Drawing.Size(52, 39)
        Me.ClearNumBtn.TabIndex = 500
        Me.ClearNumBtn.Text = "C"
        Me.ClearNumBtn.UseVisualStyleBackColor = False
        '
        'NumTextBox
        '
        Me.NumTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NumTextBox.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumTextBox.Location = New System.Drawing.Point(59, 35)
        Me.NumTextBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.NumTextBox.Name = "NumTextBox"
        Me.NumTextBox.Size = New System.Drawing.Size(305, 39)
        Me.NumTextBox.TabIndex = 488
        Me.NumTextBox.Tag = "3"
        Me.NumTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Back_Btn
        '
        Me.Back_Btn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Back_Btn.BackColor = System.Drawing.Color.White
        Me.Back_Btn.BackgroundImage = Global.resturant.My.Resources.Resources.Users_Exit_icon
        Me.Back_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Back_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Back_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Back_Btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Back_Btn.Location = New System.Drawing.Point(343, 192)
        Me.Back_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Back_Btn.Name = "Back_Btn"
        Me.Back_Btn.Size = New System.Drawing.Size(76, 45)
        Me.Back_Btn.TabIndex = 559
        Me.Back_Btn.UseVisualStyleBackColor = False
        '
        'Min_SP_Panel
        '
        Me.Min_SP_Panel.Controls.Add(Me.Min2_btn)
        Me.Min_SP_Panel.Controls.Add(Me.Min1_btn)
        Me.Min_SP_Panel.Controls.Add(Me.Min_SP_2_txt)
        Me.Min_SP_Panel.Controls.Add(Me.Label1)
        Me.Min_SP_Panel.Controls.Add(Me.Min_SP_txt)
        Me.Min_SP_Panel.Controls.Add(Me.Label23)
        Me.Min_SP_Panel.Location = New System.Drawing.Point(1, 78)
        Me.Min_SP_Panel.Name = "Min_SP_Panel"
        Me.Min_SP_Panel.Size = New System.Drawing.Size(416, 106)
        Me.Min_SP_Panel.TabIndex = 674
        '
        'Min2_btn
        '
        Me.Min2_btn.BackColor = System.Drawing.Color.White
        Me.Min2_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Min2_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Min2_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Min2_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Min2_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Min2_btn.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.Min2_btn.Location = New System.Drawing.Point(11, 38)
        Me.Min2_btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Min2_btn.Name = "Min2_btn"
        Me.Min2_btn.Size = New System.Drawing.Size(45, 28)
        Me.Min2_btn.TabIndex = 678
        Me.Min2_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Min2_btn.UseVisualStyleBackColor = False
        '
        'Min1_btn
        '
        Me.Min1_btn.BackColor = System.Drawing.Color.White
        Me.Min1_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Min1_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Min1_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Min1_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Min1_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Min1_btn.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.Min1_btn.Location = New System.Drawing.Point(11, 4)
        Me.Min1_btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Min1_btn.Name = "Min1_btn"
        Me.Min1_btn.Size = New System.Drawing.Size(45, 28)
        Me.Min1_btn.TabIndex = 677
        Me.Min1_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Min1_btn.UseVisualStyleBackColor = False
        '
        'Min_SP_2_txt
        '
        Me.Min_SP_2_txt.Font = New System.Drawing.Font("Stencil", 13.25!)
        Me.Min_SP_2_txt.Location = New System.Drawing.Point(58, 38)
        Me.Min_SP_2_txt.Name = "Min_SP_2_txt"
        Me.Min_SP_2_txt.ReadOnly = True
        Me.Min_SP_2_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Min_SP_2_txt.Size = New System.Drawing.Size(240, 28)
        Me.Min_SP_2_txt.TabIndex = 675
        Me.Min_SP_2_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.5!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(302, 41)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 23)
        Me.Label1.TabIndex = 676
        Me.Label1.Text = "جملة الجملة"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Min_SP_txt
        '
        Me.Min_SP_txt.Font = New System.Drawing.Font("Stencil", 13.25!)
        Me.Min_SP_txt.Location = New System.Drawing.Point(58, 4)
        Me.Min_SP_txt.Name = "Min_SP_txt"
        Me.Min_SP_txt.ReadOnly = True
        Me.Min_SP_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Min_SP_txt.Size = New System.Drawing.Size(240, 28)
        Me.Min_SP_txt.TabIndex = 673
        Me.Min_SP_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 12.5!, System.Drawing.FontStyle.Bold)
        Me.Label23.Location = New System.Drawing.Point(302, 7)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(55, 23)
        Me.Label23.TabIndex = 674
        Me.Label23.Text = "الجملة"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Show_Cash_btn
        '
        Me.Show_Cash_btn.BackColor = System.Drawing.SystemColors.Menu
        Me.Show_Cash_btn.BackgroundImage = Global.resturant.My.Resources.Resources.iconfinder_money_299107
        Me.Show_Cash_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Show_Cash_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Show_Cash_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Show_Cash_btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Show_Cash_btn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Show_Cash_btn.Location = New System.Drawing.Point(174, 192)
        Me.Show_Cash_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Show_Cash_btn.Name = "Show_Cash_btn"
        Me.Show_Cash_btn.Size = New System.Drawing.Size(84, 45)
        Me.Show_Cash_btn.TabIndex = 675
        Me.Show_Cash_btn.UseVisualStyleBackColor = False
        '
        'PercentBtn
        '
        Me.PercentBtn.BackColor = System.Drawing.Color.White
        Me.PercentBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PercentBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PercentBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.PercentBtn.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PercentBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PercentBtn.Location = New System.Drawing.Point(13, 35)
        Me.PercentBtn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PercentBtn.Name = "PercentBtn"
        Me.PercentBtn.Size = New System.Drawing.Size(45, 39)
        Me.PercentBtn.TabIndex = 678
        Me.PercentBtn.Text = "%"
        Me.PercentBtn.UseVisualStyleBackColor = False
        '
        'SBChangePrice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(420, 237)
        Me.ControlBox = False
        Me.Controls.Add(Me.PercentBtn)
        Me.Controls.Add(Me.Show_Cash_btn)
        Me.Controls.Add(Me.Min_SP_Panel)
        Me.Controls.Add(Me.Back_Btn)
        Me.Controls.Add(Me.ClearNumBtn)
        Me.Controls.Add(Me.NumTextBox)
        Me.Controls.Add(Me.ConfermButton)
        Me.Controls.Add(Me.InfoLb)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "SBChangePrice"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Min_SP_Panel.ResumeLayout(False)
        Me.Min_SP_Panel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents InfoLb As System.Windows.Forms.Label
    Friend WithEvents ConfermButton As System.Windows.Forms.Button
    Friend WithEvents ClearNumBtn As System.Windows.Forms.Button
    Friend WithEvents NumTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Back_Btn As System.Windows.Forms.Button
    Friend WithEvents Min_SP_Panel As System.Windows.Forms.Panel
    Friend WithEvents Min_SP_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Show_Cash_btn As System.Windows.Forms.Button
    Friend WithEvents Min_SP_2_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Min2_btn As System.Windows.Forms.Button
    Friend WithEvents Min1_btn As System.Windows.Forms.Button
    Friend WithEvents PercentBtn As Button
End Class
