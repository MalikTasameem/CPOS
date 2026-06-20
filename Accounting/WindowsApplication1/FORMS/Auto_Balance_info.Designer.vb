<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Auto_Balance_info
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DATE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ACC_CODE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ACC_NAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Bill_Num_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CREDIT_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DEBIT_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Notes_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.T_ID_txt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Total_Panel = New System.Windows.Forms.Panel()
        Me.TOTAL_C_N = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.TOTAL_D_N = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Rows_txt = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Total_D_txt = New Accounting.F2FloatField_Debit()
        Me.Total_B_txt = New Accounting.F2FloatField_Balance()
        Me.Total_C_txt = New Accounting.F2FloatField_Credit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Total_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button4
        '
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button4.Location = New System.Drawing.Point(3, 31)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(998, 37)
        Me.Button4.TabIndex = 84
        Me.Button4.Text = "عــودة    ↩️"
        Me.Button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button4.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.DATE_CL, Me.ACC_CODE_CL, Me.ACC_NAME_CL, Me.Bill_Num_CL, Me.CREDIT_CL, Me.DEBIT_CL, Me.Notes_CL})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 9.25!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.Location = New System.Drawing.Point(1, 31)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 30
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(913, 523)
        Me.DataGridView1.TabIndex = 85
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "T_ID"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        Me.T_ID_CL.Width = 60
        '
        'DATE_CL
        '
        Me.DATE_CL.DataPropertyName = "DATE"
        Me.DATE_CL.HeaderText = "التاريخ"
        Me.DATE_CL.Name = "DATE_CL"
        Me.DATE_CL.ReadOnly = True
        Me.DATE_CL.Visible = False
        Me.DATE_CL.Width = 67
        '
        'ACC_CODE_CL
        '
        Me.ACC_CODE_CL.DataPropertyName = "ACC_CODE"
        Me.ACC_CODE_CL.HeaderText = "رقم الحساب"
        Me.ACC_CODE_CL.Name = "ACC_CODE_CL"
        Me.ACC_CODE_CL.ReadOnly = True
        Me.ACC_CODE_CL.Width = 101
        '
        'ACC_NAME_CL
        '
        Me.ACC_NAME_CL.DataPropertyName = "ACC_NAME"
        Me.ACC_NAME_CL.HeaderText = "إسم الحساب"
        Me.ACC_NAME_CL.Name = "ACC_NAME_CL"
        Me.ACC_NAME_CL.ReadOnly = True
        Me.ACC_NAME_CL.Width = 107
        '
        'Bill_Num_CL
        '
        Me.Bill_Num_CL.DataPropertyName = "Bill_Num"
        Me.Bill_Num_CL.HeaderText = "رقم المستنذ"
        Me.Bill_Num_CL.Name = "Bill_Num_CL"
        Me.Bill_Num_CL.ReadOnly = True
        Me.Bill_Num_CL.Width = 102
        '
        'CREDIT_CL
        '
        Me.CREDIT_CL.DataPropertyName = "CREDIT"
        DataGridViewCellStyle1.Format = "N3"
        Me.CREDIT_CL.DefaultCellStyle = DataGridViewCellStyle1
        Me.CREDIT_CL.HeaderText = "مدين"
        Me.CREDIT_CL.Name = "CREDIT_CL"
        Me.CREDIT_CL.ReadOnly = True
        Me.CREDIT_CL.Width = 61
        '
        'DEBIT_CL
        '
        Me.DEBIT_CL.DataPropertyName = "DEBIT"
        DataGridViewCellStyle2.Format = "N3"
        Me.DEBIT_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.DEBIT_CL.HeaderText = "دائن"
        Me.DEBIT_CL.Name = "DEBIT_CL"
        Me.DEBIT_CL.ReadOnly = True
        Me.DEBIT_CL.Width = 56
        '
        'Notes_CL
        '
        Me.Notes_CL.DataPropertyName = "Notes"
        Me.Notes_CL.HeaderText = "ملاحظة"
        Me.Notes_CL.Name = "Notes_CL"
        Me.Notes_CL.ReadOnly = True
        Me.Notes_CL.Width = 74
        '
        'T_ID_txt
        '
        Me.T_ID_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.T_ID_txt.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.T_ID_txt.Location = New System.Drawing.Point(1, 2)
        Me.T_ID_txt.Margin = New System.Windows.Forms.Padding(4)
        Me.T_ID_txt.Name = "T_ID_txt"
        Me.T_ID_txt.ReadOnly = True
        Me.T_ID_txt.Size = New System.Drawing.Size(913, 27)
        Me.T_ID_txt.TabIndex = 86
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 13.75!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(918, 5)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 22)
        Me.Label1.TabIndex = 87
        Me.Label1.Text = "رقم القيــد:"
        '
        'Total_Panel
        '
        Me.Total_Panel.Controls.Add(Me.TOTAL_C_N)
        Me.Total_Panel.Controls.Add(Me.Label20)
        Me.Total_Panel.Controls.Add(Me.TOTAL_D_N)
        Me.Total_Panel.Controls.Add(Me.Label19)
        Me.Total_Panel.Controls.Add(Me.Button4)
        Me.Total_Panel.Controls.Add(Me.Rows_txt)
        Me.Total_Panel.Controls.Add(Me.Label11)
        Me.Total_Panel.Controls.Add(Me.Label8)
        Me.Total_Panel.Controls.Add(Me.Label12)
        Me.Total_Panel.Controls.Add(Me.Label14)
        Me.Total_Panel.Controls.Add(Me.Total_D_txt)
        Me.Total_Panel.Controls.Add(Me.Total_B_txt)
        Me.Total_Panel.Controls.Add(Me.Total_C_txt)
        Me.Total_Panel.Location = New System.Drawing.Point(1, 556)
        Me.Total_Panel.Name = "Total_Panel"
        Me.Total_Panel.Size = New System.Drawing.Size(1003, 70)
        Me.Total_Panel.TabIndex = 88
        '
        'TOTAL_C_N
        '
        Me.TOTAL_C_N.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TOTAL_C_N.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TOTAL_C_N.Font = New System.Drawing.Font("Arial", 13.0!, System.Drawing.FontStyle.Bold)
        Me.TOTAL_C_N.Location = New System.Drawing.Point(772, 2)
        Me.TOTAL_C_N.Margin = New System.Windows.Forms.Padding(4)
        Me.TOTAL_C_N.Name = "TOTAL_C_N"
        Me.TOTAL_C_N.ReadOnly = True
        Me.TOTAL_C_N.Size = New System.Drawing.Size(44, 27)
        Me.TOTAL_C_N.TabIndex = 63
        Me.TOTAL_C_N.Text = "0"
        Me.TOTAL_C_N.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.25!)
        Me.Label20.Location = New System.Drawing.Point(819, 8)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(74, 16)
        Me.Label20.TabIndex = 64
        Me.Label20.Text = "عدد المدين:"
        '
        'TOTAL_D_N
        '
        Me.TOTAL_D_N.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TOTAL_D_N.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TOTAL_D_N.Font = New System.Drawing.Font("Arial", 13.0!, System.Drawing.FontStyle.Bold)
        Me.TOTAL_D_N.Location = New System.Drawing.Point(656, 3)
        Me.TOTAL_D_N.Margin = New System.Windows.Forms.Padding(4)
        Me.TOTAL_D_N.Name = "TOTAL_D_N"
        Me.TOTAL_D_N.ReadOnly = True
        Me.TOTAL_D_N.Size = New System.Drawing.Size(44, 27)
        Me.TOTAL_D_N.TabIndex = 61
        Me.TOTAL_D_N.Text = "0"
        Me.TOTAL_D_N.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.25!)
        Me.Label19.Location = New System.Drawing.Point(700, 9)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(69, 16)
        Me.Label19.TabIndex = 62
        Me.Label19.Text = "عدد الدائن:"
        '
        'Rows_txt
        '
        Me.Rows_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Rows_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Rows_txt.Font = New System.Drawing.Font("Arial", 13.0!, System.Drawing.FontStyle.Bold)
        Me.Rows_txt.Location = New System.Drawing.Point(897, 2)
        Me.Rows_txt.Margin = New System.Windows.Forms.Padding(4)
        Me.Rows_txt.Name = "Rows_txt"
        Me.Rows_txt.ReadOnly = True
        Me.Rows_txt.Size = New System.Drawing.Size(44, 27)
        Me.Rows_txt.TabIndex = 44
        Me.Rows_txt.Text = "0"
        Me.Rows_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(414, 7)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(39, 18)
        Me.Label11.TabIndex = 41
        Me.Label11.Text = "دائن:"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(604, 7)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 18)
        Me.Label8.TabIndex = 43
        Me.Label8.Text = "مدين:"
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.25!)
        Me.Label12.Location = New System.Drawing.Point(944, 8)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(54, 16)
        Me.Label12.TabIndex = 45
        Me.Label12.Text = "الصفوف:"
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(216, 7)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(52, 18)
        Me.Label14.TabIndex = 55
        Me.Label14.Text = "الرصيد:"
        '
        'Total_D_txt
        '
        Me.Total_D_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Total_D_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Total_D_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_D_txt.Enabled = False
        Me.Total_D_txt.Font = New System.Drawing.Font("Arial", 13.25!, System.Drawing.FontStyle.Bold)
        Me.Total_D_txt.ForeColor = System.Drawing.Color.DarkRed
        Me.Total_D_txt.Location = New System.Drawing.Point(270, 2)
        Me.Total_D_txt.MaxLength = 0
        Me.Total_D_txt.Name = "Total_D_txt"
        Me.Total_D_txt.Size = New System.Drawing.Size(140, 28)
        Me.Total_D_txt.TabIndex = 58
        Me.Total_D_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Total_B_txt
        '
        Me.Total_B_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Total_B_txt.BackColor = System.Drawing.Color.Lavender
        Me.Total_B_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_B_txt.Enabled = False
        Me.Total_B_txt.Font = New System.Drawing.Font("Arial", 13.25!, System.Drawing.FontStyle.Bold)
        Me.Total_B_txt.Location = New System.Drawing.Point(69, 2)
        Me.Total_B_txt.MaxLength = 0
        Me.Total_B_txt.Name = "Total_B_txt"
        Me.Total_B_txt.Size = New System.Drawing.Size(140, 28)
        Me.Total_B_txt.TabIndex = 60
        Me.Total_B_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Total_C_txt
        '
        Me.Total_C_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Total_C_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Total_C_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_C_txt.Enabled = False
        Me.Total_C_txt.Font = New System.Drawing.Font("Arial", 13.25!, System.Drawing.FontStyle.Bold)
        Me.Total_C_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.Total_C_txt.Location = New System.Drawing.Point(460, 2)
        Me.Total_C_txt.MaxLength = 0
        Me.Total_C_txt.Name = "Total_C_txt"
        Me.Total_C_txt.Size = New System.Drawing.Size(140, 28)
        Me.Total_C_txt.TabIndex = 59
        Me.Total_C_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Auto_Balance_info
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 626)
        Me.ControlBox = False
        Me.Controls.Add(Me.Total_Panel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.T_ID_txt)
        Me.Controls.Add(Me.DataGridView1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.25!)
        Me.Name = "Auto_Balance_info"
        Me.Text = "عرض معلومات القيد"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Total_Panel.ResumeLayout(False)
        Me.Total_Panel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button4 As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents T_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents DATE_CL As DataGridViewTextBoxColumn
    Friend WithEvents ACC_CODE_CL As DataGridViewTextBoxColumn
    Friend WithEvents ACC_NAME_CL As DataGridViewTextBoxColumn
    Friend WithEvents Bill_Num_CL As DataGridViewTextBoxColumn
    Friend WithEvents CREDIT_CL As DataGridViewTextBoxColumn
    Friend WithEvents DEBIT_CL As DataGridViewTextBoxColumn
    Friend WithEvents Notes_CL As DataGridViewTextBoxColumn
    Friend WithEvents T_ID_txt As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Total_Panel As Panel
    Friend WithEvents TOTAL_C_N As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents TOTAL_D_N As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents Rows_txt As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Total_D_txt As F2FloatField_Debit
    Friend WithEvents Total_B_txt As F2FloatField_Balance
    Friend WithEvents Total_C_txt As F2FloatField_Credit
End Class
