<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class POS_Pied_Types
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
        Me.Credit_Pay_Btn = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Pay_Btn = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Credit_Pay_Btn
        '
        Me.Credit_Pay_Btn.BackColor = System.Drawing.SystemColors.Info
        Me.Credit_Pay_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Credit_Pay_Btn.Font = New System.Drawing.Font("Arial", 25.25!, System.Drawing.FontStyle.Bold)
        Me.Credit_Pay_Btn.Image = Global.resturant.My.Resources.Resources.credit_card
        Me.Credit_Pay_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Credit_Pay_Btn.Location = New System.Drawing.Point(1, 104)
        Me.Credit_Pay_Btn.Name = "Credit_Pay_Btn"
        Me.Credit_Pay_Btn.Size = New System.Drawing.Size(595, 100)
        Me.Credit_Pay_Btn.TabIndex = 693
        Me.Credit_Pay_Btn.Text = "دفع عن طريق البطاقة المصرفية"
        Me.Credit_Pay_Btn.UseVisualStyleBackColor = False
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ExitFormButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Arial", 16.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(1, 443)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ExitFormButton.Size = New System.Drawing.Size(594, 87)
        Me.ExitFormButton.TabIndex = 692
        Me.ExitFormButton.TabStop = False
        Me.ExitFormButton.Text = "رجـــــوع"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Pay_Btn
        '
        Me.Pay_Btn.BackColor = System.Drawing.SystemColors.Info
        Me.Pay_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Pay_Btn.Font = New System.Drawing.Font("Arial", 25.25!, System.Drawing.FontStyle.Bold)
        Me.Pay_Btn.Image = Global.resturant.My.Resources.Resources.iconfinder_money_299107
        Me.Pay_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Pay_Btn.Location = New System.Drawing.Point(1, 1)
        Me.Pay_Btn.Name = "Pay_Btn"
        Me.Pay_Btn.Size = New System.Drawing.Size(595, 100)
        Me.Pay_Btn.TabIndex = 691
        Me.Pay_Btn.Text = "دفـــــــع كــــــــاش"
        Me.Pay_Btn.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.Info
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 25.25!, System.Drawing.FontStyle.Bold)
        Me.Button1.Image = Global.resturant.My.Resources.Resources._9320216_nfc_interaction_untact_icon
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(1, 207)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(595, 100)
        Me.Button1.TabIndex = 690
        Me.Button1.Text = "دفع عــن طريــق NFC"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'POS_Pied_Types
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(597, 531)
        Me.ControlBox = False
        Me.Controls.Add(Me.Credit_Pay_Btn)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Pay_Btn)
        Me.Controls.Add(Me.Button1)
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "POS_Pied_Types"
        Me.Text = "الدفــــــع"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Pay_Btn As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents ExitFormButton As Button
    Friend WithEvents Credit_Pay_Btn As Button
End Class
