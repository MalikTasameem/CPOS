<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PAY_BY_NFC
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
        Me.components = New System.ComponentModel.Container()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.NFC_Status_txt = New System.Windows.Forms.TextBox()
        Me.NFC_Balance_txt = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.NFC_NUM_txt = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Pure_txt = New System.Windows.Forms.TextBox()
        Me.Bill_Num_txt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Title_txt = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.NFC_Label = New System.Windows.Forms.Label()
        Me.Pay_Btn = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(469, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label5.Size = New System.Drawing.Size(82, 29)
        Me.Label5.TabIndex = 674
        Me.Label5.Text = "الرصيد :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(469, 88)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label9.Size = New System.Drawing.Size(88, 29)
        Me.Label9.TabIndex = 678
        Me.Label9.Text = "الحالـــة :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'NFC_Status_txt
        '
        Me.NFC_Status_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.NFC_Status_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NFC_Status_txt.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NFC_Status_txt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.NFC_Status_txt.Location = New System.Drawing.Point(3, 84)
        Me.NFC_Status_txt.MaxLength = 250
        Me.NFC_Status_txt.Name = "NFC_Status_txt"
        Me.NFC_Status_txt.ReadOnly = True
        Me.NFC_Status_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.NFC_Status_txt.Size = New System.Drawing.Size(460, 39)
        Me.NFC_Status_txt.TabIndex = 679
        Me.NFC_Status_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'NFC_Balance_txt
        '
        Me.NFC_Balance_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.NFC_Balance_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NFC_Balance_txt.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NFC_Balance_txt.ForeColor = System.Drawing.Color.Black
        Me.NFC_Balance_txt.Location = New System.Drawing.Point(3, 43)
        Me.NFC_Balance_txt.MaxLength = 250
        Me.NFC_Balance_txt.Name = "NFC_Balance_txt"
        Me.NFC_Balance_txt.ReadOnly = True
        Me.NFC_Balance_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NFC_Balance_txt.Size = New System.Drawing.Size(460, 39)
        Me.NFC_Balance_txt.TabIndex = 675
        Me.NFC_Balance_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(466, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label6.Size = New System.Drawing.Size(127, 29)
        Me.Label6.TabIndex = 677
        Me.Label6.Text = "رقم الســـوار :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'NFC_NUM_txt
        '
        Me.NFC_NUM_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NFC_NUM_txt.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NFC_NUM_txt.Location = New System.Drawing.Point(3, 2)
        Me.NFC_NUM_txt.MaxLength = 500
        Me.NFC_NUM_txt.Name = "NFC_NUM_txt"
        Me.NFC_NUM_txt.ReadOnly = True
        Me.NFC_NUM_txt.Size = New System.Drawing.Size(460, 39)
        Me.NFC_NUM_txt.TabIndex = 676
        Me.NFC_NUM_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(429, 242)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(81, 19)
        Me.Label1.TabIndex = 681
        Me.Label1.Text = "رقم الفاتورة :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(429, 273)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(99, 19)
        Me.Label2.TabIndex = 685
        Me.Label2.Text = "إجمالي الفاتورة :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pure_txt
        '
        Me.Pure_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Pure_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pure_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pure_txt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Pure_txt.Location = New System.Drawing.Point(1, 267)
        Me.Pure_txt.MaxLength = 250
        Me.Pure_txt.Name = "Pure_txt"
        Me.Pure_txt.ReadOnly = True
        Me.Pure_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Pure_txt.Size = New System.Drawing.Size(425, 29)
        Me.Pure_txt.TabIndex = 686
        Me.Pure_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Bill_Num_txt
        '
        Me.Bill_Num_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Bill_Num_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Bill_Num_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bill_Num_txt.ForeColor = System.Drawing.Color.Black
        Me.Bill_Num_txt.Location = New System.Drawing.Point(1, 236)
        Me.Bill_Num_txt.MaxLength = 250
        Me.Bill_Num_txt.Name = "Bill_Num_txt"
        Me.Bill_Num_txt.ReadOnly = True
        Me.Bill_Num_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Bill_Num_txt.Size = New System.Drawing.Size(425, 29)
        Me.Bill_Num_txt.TabIndex = 682
        Me.Bill_Num_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(429, 212)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label3.Size = New System.Drawing.Size(79, 19)
        Me.Label3.TabIndex = 684
        Me.Label3.Text = "إسم المحــل :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Title_txt
        '
        Me.Title_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Title_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Title_txt.Location = New System.Drawing.Point(1, 206)
        Me.Title_txt.MaxLength = 500
        Me.Title_txt.Name = "Title_txt"
        Me.Title_txt.ReadOnly = True
        Me.Title_txt.Size = New System.Drawing.Size(425, 29)
        Me.Title_txt.TabIndex = 683
        Me.Title_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.NFC_NUM_txt)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.NFC_Balance_txt)
        Me.Panel1.Controls.Add(Me.NFC_Status_txt)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Location = New System.Drawing.Point(1, 80)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(594, 125)
        Me.Panel1.TabIndex = 687
        '
        'NFC_Label
        '
        Me.NFC_Label.BackColor = System.Drawing.Color.PaleGreen
        Me.NFC_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NFC_Label.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.NFC_Label.Font = New System.Drawing.Font("Arial", 22.25!, System.Drawing.FontStyle.Bold)
        Me.NFC_Label.Location = New System.Drawing.Point(1, 1)
        Me.NFC_Label.Name = "NFC_Label"
        Me.NFC_Label.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.NFC_Label.Size = New System.Drawing.Size(594, 74)
        Me.NFC_Label.TabIndex = 690
        Me.NFC_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.NFC_Label.Visible = False
        '
        'Pay_Btn
        '
        Me.Pay_Btn.BackColor = System.Drawing.SystemColors.Info
        Me.Pay_Btn.Enabled = False
        Me.Pay_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Pay_Btn.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pay_Btn.Image = Global.resturant.My.Resources.Resources.iconfinder_money_299107
        Me.Pay_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Pay_Btn.Location = New System.Drawing.Point(1, 399)
        Me.Pay_Btn.Name = "Pay_Btn"
        Me.Pay_Btn.Size = New System.Drawing.Size(594, 64)
        Me.Pay_Btn.TabIndex = 689
        Me.Pay_Btn.Text = "دفـــــــع"
        Me.Pay_Btn.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.Info
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(1, 316)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(594, 64)
        Me.Button1.TabIndex = 680
        Me.Button1.Text = "فحـــص الســـوار"
        Me.Button1.UseVisualStyleBackColor = False
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
        Me.ExitFormButton.Location = New System.Drawing.Point(1, 480)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ExitFormButton.Size = New System.Drawing.Size(594, 45)
        Me.ExitFormButton.TabIndex = 673
        Me.ExitFormButton.TabStop = False
        Me.ExitFormButton.Text = "رجـــــوع"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'PAY_BY_NFC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 26.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(596, 528)
        Me.ControlBox = False
        Me.Controls.Add(Me.NFC_Label)
        Me.Controls.Add(Me.Pay_Btn)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Pure_txt)
        Me.Controls.Add(Me.Bill_Num_txt)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Title_txt)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Name = "PAY_BY_NFC"
        Me.Text = "دفـــع بإستخـــدام NFC"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ExitFormButton As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents NFC_Status_txt As TextBox
    Friend WithEvents NFC_Balance_txt As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents NFC_NUM_txt As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Pure_txt As TextBox
    Friend WithEvents Bill_Num_txt As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Title_txt As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Pay_Btn As Button
    Friend WithEvents NFC_Label As Label
End Class
