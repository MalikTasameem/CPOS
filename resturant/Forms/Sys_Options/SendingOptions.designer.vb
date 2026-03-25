<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SendingOptions
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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Notes_txt = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Title_txt = New System.Windows.Forms.TextBox()
        Me.To_Email_txt = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Source_Email_txt = New System.Windows.Forms.TextBox()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(515, 88)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 21)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "العنوان"
        '
        'Notes_txt
        '
        Me.Notes_txt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Notes_txt.Location = New System.Drawing.Point(69, 116)
        Me.Notes_txt.Multiline = True
        Me.Notes_txt.Name = "Notes_txt"
        Me.Notes_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Notes_txt.Size = New System.Drawing.Size(442, 138)
        Me.Notes_txt.TabIndex = 31
        Me.Notes_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(515, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 21)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "الرسالة"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(475, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 21)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "من بريد"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(474, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 21)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "إلى بريد"
        '
        'Title_txt
        '
        Me.Title_txt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Title_txt.Location = New System.Drawing.Point(69, 84)
        Me.Title_txt.Name = "Title_txt"
        Me.Title_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Title_txt.Size = New System.Drawing.Size(442, 29)
        Me.Title_txt.TabIndex = 26
        Me.Title_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'To_Email_txt
        '
        Me.To_Email_txt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.To_Email_txt.Location = New System.Drawing.Point(103, 42)
        Me.To_Email_txt.Name = "To_Email_txt"
        Me.To_Email_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.To_Email_txt.Size = New System.Drawing.Size(368, 29)
        Me.To_Email_txt.TabIndex = 25
        Me.To_Email_txt.Tag = ""
        '
        'Button1
        '
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.Image = Global.resturant.My.Resources.Resources.iconfinder_038_028_share_link_friends_send_android_material_3171669
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(143, 260)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(223, 51)
        Me.Button1.TabIndex = 23
        Me.Button1.Text = "إرســـــال"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Source_Email_txt
        '
        Me.Source_Email_txt.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Source_Email_txt.Location = New System.Drawing.Point(103, 5)
        Me.Source_Email_txt.Name = "Source_Email_txt"
        Me.Source_Email_txt.ReadOnly = True
        Me.Source_Email_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Source_Email_txt.Size = New System.Drawing.Size(368, 29)
        Me.Source_Email_txt.TabIndex = 21
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.Location = New System.Drawing.Point(445, 260)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(133, 51)
        Me.ExitFormButton.TabIndex = 451
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'SendingOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(579, 313)
        Me.ControlBox = False
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Notes_txt)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Title_txt)
        Me.Controls.Add(Me.To_Email_txt)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Source_Email_txt)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "SendingOptions"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إرسال عبر البريد الإلكتروني"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Notes_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Title_txt As System.Windows.Forms.TextBox
    Friend WithEvents To_Email_txt As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Source_Email_txt As System.Windows.Forms.TextBox
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
End Class
