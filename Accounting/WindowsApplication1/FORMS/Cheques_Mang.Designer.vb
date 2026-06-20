<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Cheques_Mang
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.bankTransactionNumber = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.issueDate = New System.Windows.Forms.DateTimePicker()
        Me.notes = New System.Windows.Forms.TextBox()
        Me.dueDate = New System.Windows.Forms.DateTimePicker()
        Me.Cheque_Type_CM = New System.Windows.Forms.ComboBox()
        Me.reconciliationDate = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CONFIRM_BTN = New System.Windows.Forms.Button()
        Me.Label_info = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.bankTransactionNumber)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.issueDate)
        Me.Panel1.Controls.Add(Me.notes)
        Me.Panel1.Controls.Add(Me.dueDate)
        Me.Panel1.Controls.Add(Me.Cheque_Type_CM)
        Me.Panel1.Controls.Add(Me.reconciliationDate)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(2, 119)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(682, 139)
        Me.Panel1.TabIndex = 107
        '
        'bankTransactionNumber
        '
        Me.bankTransactionNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.bankTransactionNumber.Location = New System.Drawing.Point(3, 3)
        Me.bankTransactionNumber.Name = "bankTransactionNumber"
        Me.bankTransactionNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.bankTransactionNumber.Size = New System.Drawing.Size(309, 23)
        Me.bankTransactionNumber.TabIndex = 101
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(315, 64)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 18)
        Me.Label5.TabIndex = 106
        Me.Label5.Text = "ملاحظات"
        '
        'issueDate
        '
        Me.issueDate.Checked = False
        Me.issueDate.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.issueDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.issueDate.Location = New System.Drawing.Point(430, 10)
        Me.issueDate.Margin = New System.Windows.Forms.Padding(4)
        Me.issueDate.Name = "issueDate"
        Me.issueDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.issueDate.RightToLeftLayout = True
        Me.issueDate.ShowCheckBox = True
        Me.issueDate.Size = New System.Drawing.Size(146, 24)
        Me.issueDate.TabIndex = 95
        '
        'notes
        '
        Me.notes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.notes.Location = New System.Drawing.Point(3, 60)
        Me.notes.Multiline = True
        Me.notes.Name = "notes"
        Me.notes.Size = New System.Drawing.Size(309, 75)
        Me.notes.TabIndex = 105
        '
        'dueDate
        '
        Me.dueDate.Checked = False
        Me.dueDate.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.dueDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dueDate.Location = New System.Drawing.Point(430, 39)
        Me.dueDate.Margin = New System.Windows.Forms.Padding(4)
        Me.dueDate.Name = "dueDate"
        Me.dueDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dueDate.RightToLeftLayout = True
        Me.dueDate.ShowCheckBox = True
        Me.dueDate.Size = New System.Drawing.Size(146, 24)
        Me.dueDate.TabIndex = 96
        Me.dueDate.Visible = False
        '
        'Cheque_Type_CM
        '
        Me.Cheque_Type_CM.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Cheque_Type_CM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cheque_Type_CM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cheque_Type_CM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cheque_Type_CM.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Cheque_Type_CM.FormattingEnabled = True
        Me.Cheque_Type_CM.Location = New System.Drawing.Point(3, 29)
        Me.Cheque_Type_CM.Margin = New System.Windows.Forms.Padding(4)
        Me.Cheque_Type_CM.Name = "Cheque_Type_CM"
        Me.Cheque_Type_CM.Size = New System.Drawing.Size(309, 26)
        Me.Cheque_Type_CM.TabIndex = 103
        '
        'reconciliationDate
        '
        Me.reconciliationDate.Checked = False
        Me.reconciliationDate.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.reconciliationDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.reconciliationDate.Location = New System.Drawing.Point(430, 68)
        Me.reconciliationDate.Margin = New System.Windows.Forms.Padding(4)
        Me.reconciliationDate.Name = "reconciliationDate"
        Me.reconciliationDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.reconciliationDate.RightToLeftLayout = True
        Me.reconciliationDate.ShowCheckBox = True
        Me.reconciliationDate.Size = New System.Drawing.Size(146, 24)
        Me.reconciliationDate.TabIndex = 97
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(316, 32)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 18)
        Me.Label4.TabIndex = 104
        Me.Label4.Text = " حالة الشيك"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(580, 14)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 18)
        Me.Label6.TabIndex = 98
        Me.Label6.Text = "تاريخ الإصدار"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(316, 6)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 18)
        Me.Label3.TabIndex = 102
        Me.Label3.Text = "رقم الحركة البنكية"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(579, 42)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 18)
        Me.Label1.TabIndex = 99
        Me.Label1.Text = "تاريخ الاستحقاق"
        Me.Label1.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(580, 71)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 18)
        Me.Label2.TabIndex = 100
        Me.Label2.Text = "تاريخ المطابقة"
        '
        'CONFIRM_BTN
        '
        Me.CONFIRM_BTN.Cursor = System.Windows.Forms.Cursors.Hand
        Me.CONFIRM_BTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CONFIRM_BTN.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CONFIRM_BTN.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CONFIRM_BTN.Location = New System.Drawing.Point(2, 264)
        Me.CONFIRM_BTN.Name = "CONFIRM_BTN"
        Me.CONFIRM_BTN.Size = New System.Drawing.Size(713, 42)
        Me.CONFIRM_BTN.TabIndex = 94
        Me.CONFIRM_BTN.Text = "☑️  إعتمــــاد"
        Me.CONFIRM_BTN.UseVisualStyleBackColor = True
        '
        'Label_info
        '
        Me.Label_info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_info.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label_info.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_info.Location = New System.Drawing.Point(2, 0)
        Me.Label_info.Name = "Label_info"
        Me.Label_info.Size = New System.Drawing.Size(716, 116)
        Me.Label_info.TabIndex = 93
        Me.Label_info.Text = "( معلومات الصك )"
        Me.Label_info.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Button4
        '
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button4.Location = New System.Drawing.Point(2, 307)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(713, 41)
        Me.Button4.TabIndex = 92
        Me.Button4.Text = "↩️  عــودة"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Cheques_Mang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(718, 350)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.CONFIRM_BTN)
        Me.Controls.Add(Me.Label_info)
        Me.Controls.Add(Me.Button4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Cheques_Mang"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CONFIRM_BTN As Button
    Friend WithEvents Label_info As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents issueDate As DateTimePicker
    Friend WithEvents dueDate As DateTimePicker
    Friend WithEvents reconciliationDate As DateTimePicker
    Friend WithEvents Label6 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents bankTransactionNumber As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Cheque_Type_CM As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents notes As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel1 As Panel
End Class
