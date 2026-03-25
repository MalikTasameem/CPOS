<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CheckOwnerPass
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CheckOwnerPass))
        Me.PassTextBox = New System.Windows.Forms.TextBox()
        Me.ConfirmPassButtonX = New DevComponents.DotNetBar.ButtonX()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ShowLetterCheckBox = New System.Windows.Forms.CheckBox()
        Me.Button15 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'PassTextBox
        '
        Me.PassTextBox.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PassTextBox.Location = New System.Drawing.Point(97, 53)
        Me.PassTextBox.Name = "PassTextBox"
        Me.PassTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(35)
        Me.PassTextBox.Size = New System.Drawing.Size(191, 29)
        Me.PassTextBox.TabIndex = 0
        Me.PassTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ConfirmPassButtonX
        '
        Me.ConfirmPassButtonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.ConfirmPassButtonX.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.ConfirmPassButtonX.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ConfirmPassButtonX.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ConfirmPassButtonX.Location = New System.Drawing.Point(114, 133)
        Me.ConfirmPassButtonX.Name = "ConfirmPassButtonX"
        Me.ConfirmPassButtonX.Size = New System.Drawing.Size(144, 36)
        Me.ConfirmPassButtonX.TabIndex = 246
        Me.ConfirmPassButtonX.Text = "تأكيــد"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(60, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(290, 21)
        Me.Label1.TabIndex = 247
        Me.Label1.Text = "أدخــل الرمز السري للمستخدم الرئيسي للنظام"
        '
        'ShowLetterCheckBox
        '
        Me.ShowLetterCheckBox.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ShowLetterCheckBox.AutoSize = True
        Me.ShowLetterCheckBox.BackColor = System.Drawing.Color.Transparent
        Me.ShowLetterCheckBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ShowLetterCheckBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ShowLetterCheckBox.Location = New System.Drawing.Point(136, 88)
        Me.ShowLetterCheckBox.Name = "ShowLetterCheckBox"
        Me.ShowLetterCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowLetterCheckBox.Size = New System.Drawing.Size(108, 25)
        Me.ShowLetterCheckBox.TabIndex = 248
        Me.ShowLetterCheckBox.Text = "إظهار الرموز"
        Me.ShowLetterCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ShowLetterCheckBox.UseVisualStyleBackColor = False
        '
        'Button15
        '
        Me.Button15.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button15.BackColor = System.Drawing.Color.White
        Me.Button15.BackgroundImage = CType(resources.GetObject("Button15.BackgroundImage"), System.Drawing.Image)
        Me.Button15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button15.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button15.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button15.Location = New System.Drawing.Point(306, 135)
        Me.Button15.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(65, 43)
        Me.Button15.TabIndex = 483
        Me.Button15.UseVisualStyleBackColor = False
        '
        'CheckOwnerPass
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(371, 178)
        Me.Controls.Add(Me.Button15)
        Me.Controls.Add(Me.ShowLetterCheckBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ConfirmPassButtonX)
        Me.Controls.Add(Me.PassTextBox)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
         Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CheckOwnerPass"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تحقق من رمز المسؤول"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PassTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ConfirmPassButtonX As DevComponents.DotNetBar.ButtonX
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ShowLetterCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Button15 As System.Windows.Forms.Button
End Class
