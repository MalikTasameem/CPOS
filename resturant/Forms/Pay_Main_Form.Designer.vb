<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Pay_Main_Form
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
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.OK_Btn = New System.Windows.Forms.Button()
        Me.MONEY_VALUE_Txt = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Pay_Method1 = New resturant.Pay_Method()
        Me.SuspendLayout()
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ExitFormButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(3, 501)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(4)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ExitFormButton.Size = New System.Drawing.Size(628, 66)
        Me.ExitFormButton.TabIndex = 673
        Me.ExitFormButton.TabStop = False
        Me.ExitFormButton.Text = "رجـــوع Esc"
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'OK_Btn
        '
        Me.OK_Btn.BackColor = System.Drawing.Color.CornflowerBlue
        Me.OK_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.OK_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.OK_Btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.OK_Btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.OK_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.OK_Btn.Font = New System.Drawing.Font("Arial", 24.25!, System.Drawing.FontStyle.Bold)
        Me.OK_Btn.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.OK_Btn.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.OK_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.OK_Btn.Location = New System.Drawing.Point(3, 407)
        Me.OK_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.OK_Btn.Name = "OK_Btn"
        Me.OK_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.OK_Btn.Size = New System.Drawing.Size(628, 93)
        Me.OK_Btn.TabIndex = 674
        Me.OK_Btn.TabStop = False
        Me.OK_Btn.Text = "تأكيـــد F12"
        Me.OK_Btn.UseVisualStyleBackColor = False
        '
        'MONEY_VALUE_Txt
        '
        Me.MONEY_VALUE_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MONEY_VALUE_Txt.Font = New System.Drawing.Font("Times New Roman", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MONEY_VALUE_Txt.Location = New System.Drawing.Point(135, 3)
        Me.MONEY_VALUE_Txt.Name = "MONEY_VALUE_Txt"
        Me.MONEY_VALUE_Txt.ReadOnly = True
        Me.MONEY_VALUE_Txt.Size = New System.Drawing.Size(294, 44)
        Me.MONEY_VALUE_Txt.TabIndex = 675
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(433, 9)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label11.Size = New System.Drawing.Size(80, 33)
        Me.Label11.TabIndex = 676
        Me.Label11.Text = "المبلغ"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Pay_Method1
        '
        Me.Pay_Method1.BackColor = System.Drawing.Color.Transparent
        Me.Pay_Method1.Font = New System.Drawing.Font("Tahoma", 14.75!, System.Drawing.FontStyle.Bold)
        Me.Pay_Method1.Location = New System.Drawing.Point(3, 54)
        Me.Pay_Method1.Margin = New System.Windows.Forms.Padding(4)
        Me.Pay_Method1.Name = "Pay_Method1"
        Me.Pay_Method1.Size = New System.Drawing.Size(627, 345)
        Me.Pay_Method1.TabIndex = 463
        '
        'Pay_Main_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(632, 568)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.MONEY_VALUE_Txt)
        Me.Controls.Add(Me.OK_Btn)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Pay_Method1)
        Me.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Pay_Main_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تحديد طريقة الدفع"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Pay_Method1 As Pay_Method
    Friend WithEvents ExitFormButton As Button
    Friend WithEvents OK_Btn As Button
    Friend WithEvents MONEY_VALUE_Txt As TextBox
    Friend WithEvents Label11 As Label
End Class
