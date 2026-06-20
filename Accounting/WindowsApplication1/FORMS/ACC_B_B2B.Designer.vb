<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ACC_B_B2B
    Inherits Base_Form
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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.DateTimeReceipt = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.M_Notes_txt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.CREDIT_GroupBox = New System.Windows.Forms.GroupBox()
        Me.CREDIT_SEARCH_ACC_BTN = New System.Windows.Forms.Button()
        Me.CREDIT_ACC_CODE_TXT = New System.Windows.Forms.TextBox()
        Me.CREDIT_B_Cm = New System.Windows.Forms.ComboBox()
        Me.DEBIT_GroupBox = New System.Windows.Forms.GroupBox()
        Me.DEBIT_SEARCH_ACC_BTN = New System.Windows.Forms.Button()
        Me.DEBIT_ACC_CODE_TXT = New System.Windows.Forms.TextBox()
        Me.DEBIT_B_Cm = New System.Windows.Forms.ComboBox()
        Me.Credit_Rd = New System.Windows.Forms.RadioButton()
        Me.Debit_Rd = New System.Windows.Forms.RadioButton()
        Me.save_butt = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Cost_Center_Control1 = New Accounting.Cost_Center_Control()
        Me.Amount_txt = New Accounting.F2FloatField()
        Me.CREDIT_GroupBox.SuspendLayout()
        Me.DEBIT_GroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimeReceipt
        '
        Me.DateTimeReceipt.CalendarFont = New System.Drawing.Font("Segoe UI", 15.25!)
        Me.DateTimeReceipt.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.DateTimeReceipt.Font = New System.Drawing.Font("Tahoma", 10.75!, System.Drawing.FontStyle.Bold)
        Me.DateTimeReceipt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimeReceipt.Location = New System.Drawing.Point(390, 3)
        Me.DateTimeReceipt.Name = "DateTimeReceipt"
        Me.DateTimeReceipt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DateTimeReceipt.RightToLeftLayout = True
        Me.DateTimeReceipt.Size = New System.Drawing.Size(199, 25)
        Me.DateTimeReceipt.TabIndex = 115
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.Label2.Location = New System.Drawing.Point(593, 35)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 18)
        Me.Label2.TabIndex = 114
        Me.Label2.Text = "الشرح:"
        '
        'M_Notes_txt
        '
        Me.M_Notes_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.M_Notes_txt.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.M_Notes_txt.Location = New System.Drawing.Point(2, 30)
        Me.M_Notes_txt.Margin = New System.Windows.Forms.Padding(4)
        Me.M_Notes_txt.Name = "M_Notes_txt"
        Me.M_Notes_txt.Size = New System.Drawing.Size(587, 27)
        Me.M_Notes_txt.TabIndex = 112
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(593, 7)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 18)
        Me.Label1.TabIndex = 113
        Me.Label1.Text = "التاريخ:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label14.Location = New System.Drawing.Point(512, 211)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(53, 17)
        Me.Label14.TabIndex = 110
        Me.Label14.Text = "القيمة :"
        '
        'CREDIT_GroupBox
        '
        Me.CREDIT_GroupBox.Controls.Add(Me.CREDIT_SEARCH_ACC_BTN)
        Me.CREDIT_GroupBox.Controls.Add(Me.CREDIT_ACC_CODE_TXT)
        Me.CREDIT_GroupBox.Controls.Add(Me.CREDIT_B_Cm)
        Me.CREDIT_GroupBox.Location = New System.Drawing.Point(2, 239)
        Me.CREDIT_GroupBox.Name = "CREDIT_GroupBox"
        Me.CREDIT_GroupBox.Size = New System.Drawing.Size(639, 50)
        Me.CREDIT_GroupBox.TabIndex = 108
        Me.CREDIT_GroupBox.TabStop = False
        Me.CREDIT_GroupBox.Text = "الحساب الدائن :"
        '
        'CREDIT_SEARCH_ACC_BTN
        '
        Me.CREDIT_SEARCH_ACC_BTN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CREDIT_SEARCH_ACC_BTN.BackColor = System.Drawing.Color.White
        Me.CREDIT_SEARCH_ACC_BTN.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CREDIT_SEARCH_ACC_BTN.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.CREDIT_SEARCH_ACC_BTN.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CREDIT_SEARCH_ACC_BTN.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.CREDIT_SEARCH_ACC_BTN.Location = New System.Drawing.Point(39, 21)
        Me.CREDIT_SEARCH_ACC_BTN.Margin = New System.Windows.Forms.Padding(4)
        Me.CREDIT_SEARCH_ACC_BTN.Name = "CREDIT_SEARCH_ACC_BTN"
        Me.CREDIT_SEARCH_ACC_BTN.Size = New System.Drawing.Size(29, 23)
        Me.CREDIT_SEARCH_ACC_BTN.TabIndex = 419
        Me.CREDIT_SEARCH_ACC_BTN.Text = "..."
        Me.CREDIT_SEARCH_ACC_BTN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CREDIT_SEARCH_ACC_BTN.UseVisualStyleBackColor = False
        '
        'CREDIT_ACC_CODE_TXT
        '
        Me.CREDIT_ACC_CODE_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CREDIT_ACC_CODE_TXT.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.CREDIT_ACC_CODE_TXT.Location = New System.Drawing.Point(510, 20)
        Me.CREDIT_ACC_CODE_TXT.Margin = New System.Windows.Forms.Padding(4)
        Me.CREDIT_ACC_CODE_TXT.Name = "CREDIT_ACC_CODE_TXT"
        Me.CREDIT_ACC_CODE_TXT.Size = New System.Drawing.Size(125, 25)
        Me.CREDIT_ACC_CODE_TXT.TabIndex = 107
        '
        'CREDIT_B_Cm
        '
        Me.CREDIT_B_Cm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CREDIT_B_Cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CREDIT_B_Cm.BackColor = System.Drawing.Color.Gainsboro
        Me.CREDIT_B_Cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CREDIT_B_Cm.DropDownHeight = 500
        Me.CREDIT_B_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CREDIT_B_Cm.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.CREDIT_B_Cm.FormattingEnabled = True
        Me.CREDIT_B_Cm.IntegralHeight = False
        Me.CREDIT_B_Cm.Location = New System.Drawing.Point(68, 19)
        Me.CREDIT_B_Cm.Margin = New System.Windows.Forms.Padding(4)
        Me.CREDIT_B_Cm.Name = "CREDIT_B_Cm"
        Me.CREDIT_B_Cm.Size = New System.Drawing.Size(439, 27)
        Me.CREDIT_B_Cm.TabIndex = 108
        '
        'DEBIT_GroupBox
        '
        Me.DEBIT_GroupBox.Controls.Add(Me.DEBIT_SEARCH_ACC_BTN)
        Me.DEBIT_GroupBox.Controls.Add(Me.DEBIT_ACC_CODE_TXT)
        Me.DEBIT_GroupBox.Controls.Add(Me.DEBIT_B_Cm)
        Me.DEBIT_GroupBox.Location = New System.Drawing.Point(2, 136)
        Me.DEBIT_GroupBox.Name = "DEBIT_GroupBox"
        Me.DEBIT_GroupBox.Size = New System.Drawing.Size(639, 50)
        Me.DEBIT_GroupBox.TabIndex = 107
        Me.DEBIT_GroupBox.TabStop = False
        Me.DEBIT_GroupBox.Text = "الحساب المدين :"
        '
        'DEBIT_SEARCH_ACC_BTN
        '
        Me.DEBIT_SEARCH_ACC_BTN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DEBIT_SEARCH_ACC_BTN.BackColor = System.Drawing.Color.White
        Me.DEBIT_SEARCH_ACC_BTN.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DEBIT_SEARCH_ACC_BTN.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DEBIT_SEARCH_ACC_BTN.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DEBIT_SEARCH_ACC_BTN.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DEBIT_SEARCH_ACC_BTN.Location = New System.Drawing.Point(37, 22)
        Me.DEBIT_SEARCH_ACC_BTN.Margin = New System.Windows.Forms.Padding(4)
        Me.DEBIT_SEARCH_ACC_BTN.Name = "DEBIT_SEARCH_ACC_BTN"
        Me.DEBIT_SEARCH_ACC_BTN.Size = New System.Drawing.Size(29, 23)
        Me.DEBIT_SEARCH_ACC_BTN.TabIndex = 419
        Me.DEBIT_SEARCH_ACC_BTN.Text = "..."
        Me.DEBIT_SEARCH_ACC_BTN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DEBIT_SEARCH_ACC_BTN.UseVisualStyleBackColor = False
        '
        'DEBIT_ACC_CODE_TXT
        '
        Me.DEBIT_ACC_CODE_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DEBIT_ACC_CODE_TXT.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.DEBIT_ACC_CODE_TXT.Location = New System.Drawing.Point(510, 21)
        Me.DEBIT_ACC_CODE_TXT.Margin = New System.Windows.Forms.Padding(4)
        Me.DEBIT_ACC_CODE_TXT.Name = "DEBIT_ACC_CODE_TXT"
        Me.DEBIT_ACC_CODE_TXT.Size = New System.Drawing.Size(125, 25)
        Me.DEBIT_ACC_CODE_TXT.TabIndex = 104
        '
        'DEBIT_B_Cm
        '
        Me.DEBIT_B_Cm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.DEBIT_B_Cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.DEBIT_B_Cm.BackColor = System.Drawing.Color.Gainsboro
        Me.DEBIT_B_Cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DEBIT_B_Cm.DropDownHeight = 500
        Me.DEBIT_B_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DEBIT_B_Cm.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.DEBIT_B_Cm.FormattingEnabled = True
        Me.DEBIT_B_Cm.IntegralHeight = False
        Me.DEBIT_B_Cm.Location = New System.Drawing.Point(68, 20)
        Me.DEBIT_B_Cm.Margin = New System.Windows.Forms.Padding(4)
        Me.DEBIT_B_Cm.Name = "DEBIT_B_Cm"
        Me.DEBIT_B_Cm.Size = New System.Drawing.Size(439, 27)
        Me.DEBIT_B_Cm.TabIndex = 105
        '
        'Credit_Rd
        '
        Me.Credit_Rd.AutoSize = True
        Me.Credit_Rd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Credit_Rd.Location = New System.Drawing.Point(5, 5)
        Me.Credit_Rd.Name = "Credit_Rd"
        Me.Credit_Rd.Size = New System.Drawing.Size(87, 20)
        Me.Credit_Rd.TabIndex = 282
        Me.Credit_Rd.Text = "الحساب الدائن"
        Me.Credit_Rd.UseVisualStyleBackColor = True
        '
        'Debit_Rd
        '
        Me.Debit_Rd.AutoSize = True
        Me.Debit_Rd.Checked = True
        Me.Debit_Rd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Debit_Rd.Location = New System.Drawing.Point(129, 5)
        Me.Debit_Rd.Name = "Debit_Rd"
        Me.Debit_Rd.Size = New System.Drawing.Size(90, 20)
        Me.Debit_Rd.TabIndex = 283
        Me.Debit_Rd.TabStop = True
        Me.Debit_Rd.Text = "الحساب المدين"
        Me.Debit_Rd.UseVisualStyleBackColor = True
        '
        'save_butt
        '
        Me.save_butt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.save_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.save_butt.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.save_butt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.save_butt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.save_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.save_butt.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.save_butt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.save_butt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.save_butt.Location = New System.Drawing.Point(2, 366)
        Me.save_butt.Name = "save_butt"
        Me.save_butt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.save_butt.Size = New System.Drawing.Size(639, 45)
        Me.save_butt.TabIndex = 280
        Me.save_butt.TabStop = False
        Me.save_butt.Text = "حفظ القيـــــد   💾"
        Me.save_butt.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(2, 413)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(639, 40)
        Me.Button1.TabIndex = 44
        Me.Button1.Text = "عـــودة   ↩️    ↩️"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Cost_Center_Control1
        '
        Me.Cost_Center_Control1.Location = New System.Drawing.Point(2, 60)
        Me.Cost_Center_Control1.Margin = New System.Windows.Forms.Padding(4)
        Me.Cost_Center_Control1.Name = "Cost_Center_Control1"
        Me.Cost_Center_Control1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Cost_Center_Control1.Size = New System.Drawing.Size(587, 52)
        Me.Cost_Center_Control1.TabIndex = 281
        '
        'Amount_txt
        '
        Me.Amount_txt.BackColor = System.Drawing.Color.Lavender
        Me.Amount_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Amount_txt.Font = New System.Drawing.Font("Arial", 13.25!, System.Drawing.FontStyle.Bold)
        Me.Amount_txt.Location = New System.Drawing.Point(348, 205)
        Me.Amount_txt.MaxLength = 0
        Me.Amount_txt.Name = "Amount_txt"
        Me.Amount_txt.Size = New System.Drawing.Size(161, 28)
        Me.Amount_txt.TabIndex = 109
        '
        'ACC_B_B2B
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(641, 454)
        Me.Controls.Add(Me.Debit_Rd)
        Me.Controls.Add(Me.Credit_Rd)
        Me.Controls.Add(Me.Cost_Center_Control1)
        Me.Controls.Add(Me.save_butt)
        Me.Controls.Add(Me.DateTimeReceipt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.M_Notes_txt)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Amount_txt)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.CREDIT_GroupBox)
        Me.Controls.Add(Me.DEBIT_GroupBox)
        Me.Controls.Add(Me.Button1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ACC_B_B2B"
        Me.Text = "إضافة قيد بسيط"
        Me.CREDIT_GroupBox.ResumeLayout(False)
        Me.CREDIT_GroupBox.PerformLayout()
        Me.DEBIT_GroupBox.ResumeLayout(False)
        Me.DEBIT_GroupBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents CREDIT_GroupBox As GroupBox
    Friend WithEvents CREDIT_ACC_CODE_TXT As TextBox
    Friend WithEvents CREDIT_B_Cm As ComboBox
    Friend WithEvents DEBIT_GroupBox As GroupBox
    Friend WithEvents DEBIT_ACC_CODE_TXT As TextBox
    Friend WithEvents DEBIT_B_Cm As ComboBox
    Friend WithEvents Amount_txt As F2FloatField
    Friend WithEvents Label14 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents M_Notes_txt As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents DateTimeReceipt As DateTimePicker
    Friend WithEvents save_butt As Button
    Friend WithEvents Cost_Center_Control1 As Cost_Center_Control
    Friend WithEvents Credit_Rd As RadioButton
    Friend WithEvents Debit_Rd As RadioButton
    Friend WithEvents CREDIT_SEARCH_ACC_BTN As Button
    Friend WithEvents DEBIT_SEARCH_ACC_BTN As Button
End Class
