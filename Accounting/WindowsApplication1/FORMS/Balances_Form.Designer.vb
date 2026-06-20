<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Balances_Form
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Hide_Zeros_CB = New System.Windows.Forms.CheckBox()
        Me.TITLE_txt = New System.Windows.Forms.Label()
        Me.Search_By_Acc_Code_txt = New System.Windows.Forms.TextBox()
        Me.Search_By_Acc_Name_txt = New System.Windows.Forms.TextBox()
        Me.ACC_LEVEL_txt = New System.Windows.Forms.DomainUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Rows_txt = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.EDIT_Btn = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Total_B_txt = New Accounting.F2FloatField_Balance()
        Me.Total_C_txt = New Accounting.F2FloatField_Credit()
        Me.Total_D_txt = New Accounting.F2FloatField_Debit()
        Me.Cost_Center_Control1 = New Accounting.Cost_Center_Control()
        Me.ALL_RD = New System.Windows.Forms.RadioButton()
        Me.BY_LEVELS_RD = New System.Windows.Forms.RadioButton()
        Me.TOTAL_Panel = New System.Windows.Forms.Panel()
        Me.LEVEL_Panel = New System.Windows.Forms.Panel()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TOTAL_Panel.SuspendLayout()
        Me.LEVEL_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Hide_Zeros_CB
        '
        Me.Hide_Zeros_CB.AutoSize = True
        Me.Hide_Zeros_CB.Checked = True
        Me.Hide_Zeros_CB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Hide_Zeros_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Hide_Zeros_CB.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Hide_Zeros_CB.Location = New System.Drawing.Point(582, 70)
        Me.Hide_Zeros_CB.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Hide_Zeros_CB.Name = "Hide_Zeros_CB"
        Me.Hide_Zeros_CB.Size = New System.Drawing.Size(154, 21)
        Me.Hide_Zeros_CB.TabIndex = 107
        Me.Hide_Zeros_CB.Text = "إخفـاء الحسابـات الصفريــة"
        Me.Hide_Zeros_CB.UseVisualStyleBackColor = True
        '
        'TITLE_txt
        '
        Me.TITLE_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TITLE_txt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TITLE_txt.Font = New System.Drawing.Font("Arial", 17.0!, System.Drawing.FontStyle.Bold)
        Me.TITLE_txt.Location = New System.Drawing.Point(2, 1)
        Me.TITLE_txt.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.TITLE_txt.Name = "TITLE_txt"
        Me.TITLE_txt.Size = New System.Drawing.Size(938, 45)
        Me.TITLE_txt.TabIndex = 89
        Me.TITLE_txt.Text = "كشــف أستـــاذ"
        Me.TITLE_txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Search_By_Acc_Code_txt
        '
        Me.Search_By_Acc_Code_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Search_By_Acc_Code_txt.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.Search_By_Acc_Code_txt.Location = New System.Drawing.Point(3, 163)
        Me.Search_By_Acc_Code_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Search_By_Acc_Code_txt.Name = "Search_By_Acc_Code_txt"
        Me.Search_By_Acc_Code_txt.Size = New System.Drawing.Size(468, 23)
        Me.Search_By_Acc_Code_txt.TabIndex = 68
        '
        'Search_By_Acc_Name_txt
        '
        Me.Search_By_Acc_Name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Search_By_Acc_Name_txt.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.Search_By_Acc_Name_txt.Location = New System.Drawing.Point(472, 163)
        Me.Search_By_Acc_Name_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Search_By_Acc_Name_txt.Name = "Search_By_Acc_Name_txt"
        Me.Search_By_Acc_Name_txt.Size = New System.Drawing.Size(468, 23)
        Me.Search_By_Acc_Name_txt.TabIndex = 67
        '
        'ACC_LEVEL_txt
        '
        Me.ACC_LEVEL_txt.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.ACC_LEVEL_txt.Items.Add("9")
        Me.ACC_LEVEL_txt.Items.Add("8")
        Me.ACC_LEVEL_txt.Items.Add("7")
        Me.ACC_LEVEL_txt.Items.Add("6")
        Me.ACC_LEVEL_txt.Items.Add("5")
        Me.ACC_LEVEL_txt.Items.Add("4")
        Me.ACC_LEVEL_txt.Items.Add("3")
        Me.ACC_LEVEL_txt.Items.Add("2")
        Me.ACC_LEVEL_txt.Items.Add("1")
        Me.ACC_LEVEL_txt.Items.Add("0")
        Me.ACC_LEVEL_txt.Location = New System.Drawing.Point(2, 3)
        Me.ACC_LEVEL_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ACC_LEVEL_txt.Name = "ACC_LEVEL_txt"
        Me.ACC_LEVEL_txt.ReadOnly = True
        Me.ACC_LEVEL_txt.Size = New System.Drawing.Size(68, 26)
        Me.ACC_LEVEL_txt.TabIndex = 66
        Me.ACC_LEVEL_txt.Text = "1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.25!)
        Me.Label3.Location = New System.Drawing.Point(73, 7)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 19)
        Me.Label3.TabIndex = 64
        Me.Label3.Text = "مستوى الحساب:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 13.0!)
        Me.Label14.Location = New System.Drawing.Point(182, 10)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(57, 21)
        Me.Label14.TabIndex = 63
        Me.Label14.Text = "الرصيد:"
        Me.Label14.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 13.0!)
        Me.Label12.Location = New System.Drawing.Point(868, 10)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(65, 21)
        Me.Label12.TabIndex = 61
        Me.Label12.Text = "الصفوف:"
        '
        'Rows_txt
        '
        Me.Rows_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Rows_txt.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Rows_txt.Location = New System.Drawing.Point(749, 4)
        Me.Rows_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Rows_txt.Name = "Rows_txt"
        Me.Rows_txt.ReadOnly = True
        Me.Rows_txt.Size = New System.Drawing.Size(116, 29)
        Me.Rows_txt.TabIndex = 60
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 13.0!)
        Me.Label8.Location = New System.Drawing.Point(653, 10)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 21)
        Me.Label8.TabIndex = 59
        Me.Label8.Text = "مدين:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 13.0!)
        Me.Label11.Location = New System.Drawing.Point(429, 12)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(38, 21)
        Me.Label11.TabIndex = 57
        Me.Label11.Text = "دائن:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.Location = New System.Drawing.Point(3, 189)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 35
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(937, 627)
        Me.DataGridView1.TabIndex = 40
        '
        'EDIT_Btn
        '
        Me.EDIT_Btn.BackColor = System.Drawing.Color.White
        Me.EDIT_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EDIT_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EDIT_Btn.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EDIT_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.EDIT_Btn.Location = New System.Drawing.Point(3, 104)
        Me.EDIT_Btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.EDIT_Btn.Name = "EDIT_Btn"
        Me.EDIT_Btn.Size = New System.Drawing.Size(193, 57)
        Me.EDIT_Btn.TabIndex = 90
        Me.EDIT_Btn.Text = "عرض السجــل 🔍 "
        Me.EDIT_Btn.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(4, 863)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(936, 40)
        Me.Button1.TabIndex = 82
        Me.Button1.Text = "عـــودة   ↩️"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Total_B_txt
        '
        Me.Total_B_txt.BackColor = System.Drawing.Color.Lavender
        Me.Total_B_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_B_txt.Enabled = False
        Me.Total_B_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Total_B_txt.Location = New System.Drawing.Point(2, 4)
        Me.Total_B_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Total_B_txt.MaxLength = 0
        Me.Total_B_txt.Name = "Total_B_txt"
        Me.Total_B_txt.Size = New System.Drawing.Size(176, 29)
        Me.Total_B_txt.TabIndex = 81
        Me.Total_B_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Total_B_txt.Visible = False
        '
        'Total_C_txt
        '
        Me.Total_C_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Total_C_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_C_txt.Enabled = False
        Me.Total_C_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Total_C_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.Total_C_txt.Location = New System.Drawing.Point(474, 4)
        Me.Total_C_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Total_C_txt.MaxLength = 0
        Me.Total_C_txt.Name = "Total_C_txt"
        Me.Total_C_txt.Size = New System.Drawing.Size(176, 29)
        Me.Total_C_txt.TabIndex = 80
        Me.Total_C_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Total_D_txt
        '
        Me.Total_D_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Total_D_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_D_txt.Enabled = False
        Me.Total_D_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Total_D_txt.ForeColor = System.Drawing.Color.DarkRed
        Me.Total_D_txt.Location = New System.Drawing.Point(246, 4)
        Me.Total_D_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Total_D_txt.MaxLength = 0
        Me.Total_D_txt.Name = "Total_D_txt"
        Me.Total_D_txt.Size = New System.Drawing.Size(176, 29)
        Me.Total_D_txt.TabIndex = 79
        Me.Total_D_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Cost_Center_Control1
        '
        Me.Cost_Center_Control1.Location = New System.Drawing.Point(200, 104)
        Me.Cost_Center_Control1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Cost_Center_Control1.Name = "Cost_Center_Control1"
        Me.Cost_Center_Control1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Cost_Center_Control1.Size = New System.Drawing.Size(396, 57)
        Me.Cost_Center_Control1.TabIndex = 111
        '
        'ALL_RD
        '
        Me.ALL_RD.AutoSize = True
        Me.ALL_RD.Checked = True
        Me.ALL_RD.Location = New System.Drawing.Point(801, 70)
        Me.ALL_RD.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ALL_RD.Name = "ALL_RD"
        Me.ALL_RD.Size = New System.Drawing.Size(129, 23)
        Me.ALL_RD.TabIndex = 112
        Me.ALL_RD.TabStop = True
        Me.ALL_RD.Text = "عرض قائمة كاملة"
        Me.ALL_RD.UseVisualStyleBackColor = True
        '
        'BY_LEVELS_RD
        '
        Me.BY_LEVELS_RD.AutoSize = True
        Me.BY_LEVELS_RD.Location = New System.Drawing.Point(786, 134)
        Me.BY_LEVELS_RD.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.BY_LEVELS_RD.Name = "BY_LEVELS_RD"
        Me.BY_LEVELS_RD.Size = New System.Drawing.Size(152, 23)
        Me.BY_LEVELS_RD.TabIndex = 113
        Me.BY_LEVELS_RD.Text = "عرض حسب المستوى"
        Me.BY_LEVELS_RD.UseVisualStyleBackColor = True
        '
        'TOTAL_Panel
        '
        Me.TOTAL_Panel.Controls.Add(Me.Total_B_txt)
        Me.TOTAL_Panel.Controls.Add(Me.Label11)
        Me.TOTAL_Panel.Controls.Add(Me.Label8)
        Me.TOTAL_Panel.Controls.Add(Me.Rows_txt)
        Me.TOTAL_Panel.Controls.Add(Me.Label12)
        Me.TOTAL_Panel.Controls.Add(Me.Label14)
        Me.TOTAL_Panel.Controls.Add(Me.Total_D_txt)
        Me.TOTAL_Panel.Controls.Add(Me.Total_C_txt)
        Me.TOTAL_Panel.Location = New System.Drawing.Point(4, 821)
        Me.TOTAL_Panel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TOTAL_Panel.Name = "TOTAL_Panel"
        Me.TOTAL_Panel.Size = New System.Drawing.Size(938, 42)
        Me.TOTAL_Panel.TabIndex = 114
        '
        'LEVEL_Panel
        '
        Me.LEVEL_Panel.Controls.Add(Me.ACC_LEVEL_txt)
        Me.LEVEL_Panel.Controls.Add(Me.Label3)
        Me.LEVEL_Panel.Location = New System.Drawing.Point(605, 127)
        Me.LEVEL_Panel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LEVEL_Panel.Name = "LEVEL_Panel"
        Me.LEVEL_Panel.Size = New System.Drawing.Size(175, 33)
        Me.LEVEL_Panel.TabIndex = 115
        Me.LEVEL_Panel.Visible = False
        '
        'Balances_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(942, 904)
        Me.Controls.Add(Me.LEVEL_Panel)
        Me.Controls.Add(Me.TOTAL_Panel)
        Me.Controls.Add(Me.BY_LEVELS_RD)
        Me.Controls.Add(Me.ALL_RD)
        Me.Controls.Add(Me.Cost_Center_Control1)
        Me.Controls.Add(Me.Hide_Zeros_CB)
        Me.Controls.Add(Me.EDIT_Btn)
        Me.Controls.Add(Me.TITLE_txt)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Search_By_Acc_Code_txt)
        Me.Controls.Add(Me.Search_By_Acc_Name_txt)
        Me.Controls.Add(Me.DataGridView1)
        Me.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Name = "Balances_Form"
        Me.Text = "كشــف أستـــاذ"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TOTAL_Panel.ResumeLayout(False)
        Me.TOTAL_Panel.PerformLayout()
        Me.LEVEL_Panel.ResumeLayout(False)
        Me.LEVEL_Panel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Rows_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ACC_LEVEL_txt As System.Windows.Forms.DomainUpDown
    Friend WithEvents Search_By_Acc_Name_txt As System.Windows.Forms.TextBox
    Friend WithEvents Search_By_Acc_Code_txt As System.Windows.Forms.TextBox
    Friend WithEvents Total_B_txt As Accounting.F2FloatField_Balance
    Friend WithEvents Total_C_txt As Accounting.F2FloatField_Credit
    Friend WithEvents Total_D_txt As Accounting.F2FloatField_Debit
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TITLE_txt As Label
    Friend WithEvents EDIT_Btn As Button
    Friend WithEvents Hide_Zeros_CB As CheckBox
    Friend WithEvents Cost_Center_Control1 As Cost_Center_Control
    Friend WithEvents ALL_RD As RadioButton
    Friend WithEvents BY_LEVELS_RD As RadioButton
    Friend WithEvents TOTAL_Panel As Panel
    Friend WithEvents LEVEL_Panel As Panel
End Class
