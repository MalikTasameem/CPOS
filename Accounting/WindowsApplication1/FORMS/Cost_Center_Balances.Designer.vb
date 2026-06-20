<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Cost_Center_Balances
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.إستخراجالتقريرExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Cost_Center_Control1 = New Accounting.Cost_Center_Control()
        Me.Print_CntxtMStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Title_Label = New System.Windows.Forms.Label()
        Me.Print_Btn = New Accounting.SplitButton()
        Me.Search_By_Acc_Name_txt = New System.Windows.Forms.TextBox()
        Me.Search_By_Acc_Code_txt = New System.Windows.Forms.TextBox()
        Me.RefreshBtn = New System.Windows.Forms.Button()
        Me.CircularProgressControl1 = New Accounting.CircularProgressControl()
        Me.CircularPanel = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Rows_txt = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Total_B_D_txt = New Accounting.F2FloatField_Credit()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Total_B_C_txt = New Accounting.F2FloatField_Debit()
        Me.Total_D_txt = New Accounting.F2FloatField_Debit()
        Me.Total_C_txt = New Accounting.F2FloatField_Credit()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Dif_TXT = New Accounting.F2FloatField_Balance()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DateRange_Flate1 = New Accounting.DateRange_Flate()
        Me.TITLE_txt = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Print_CntxtMStrip.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.CircularPanel.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'إستخراجالتقريرExcelToolStripMenuItem
        '
        Me.إستخراجالتقريرExcelToolStripMenuItem.Name = "إستخراجالتقريرExcelToolStripMenuItem"
        Me.إستخراجالتقريرExcelToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.إستخراجالتقريرExcelToolStripMenuItem.Text = "إستخراج التقرير Excel"
        '
        'Cost_Center_Control1
        '
        Me.Cost_Center_Control1.Location = New System.Drawing.Point(645, 5)
        Me.Cost_Center_Control1.Margin = New System.Windows.Forms.Padding(5)
        Me.Cost_Center_Control1.Name = "Cost_Center_Control1"
        Me.Cost_Center_Control1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Cost_Center_Control1.Size = New System.Drawing.Size(353, 46)
        Me.Cost_Center_Control1.TabIndex = 901
        '
        'Print_CntxtMStrip
        '
        Me.Print_CntxtMStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.إستخراجالتقريرExcelToolStripMenuItem})
        Me.Print_CntxtMStrip.Name = "ContextMenuStrip1"
        Me.Print_CntxtMStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_CntxtMStrip.Size = New System.Drawing.Size(177, 26)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Title_Label)
        Me.Panel2.Controls.Add(Me.Cost_Center_Control1)
        Me.Panel2.Controls.Add(Me.Print_Btn)
        Me.Panel2.Controls.Add(Me.Search_By_Acc_Name_txt)
        Me.Panel2.Controls.Add(Me.Search_By_Acc_Code_txt)
        Me.Panel2.Controls.Add(Me.RefreshBtn)
        Me.Panel2.Location = New System.Drawing.Point(1, 81)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1003, 83)
        Me.Panel2.TabIndex = 908
        '
        'Title_Label
        '
        Me.Title_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Title_Label.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Title_Label.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Title_Label.Location = New System.Drawing.Point(290, 4)
        Me.Title_Label.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Title_Label.Name = "Title_Label"
        Me.Title_Label.Size = New System.Drawing.Size(348, 47)
        Me.Title_Label.TabIndex = 907
        Me.Title_Label.Text = "----"
        Me.Title_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Print_Btn
        '
        Me.Print_Btn.BackColor = System.Drawing.Color.White
        Me.Print_Btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Print_Btn.ButtonImage = Nothing
        Me.Print_Btn.ButtonText = "طباعــة  🖨️"
        Me.Print_Btn.DropDownMenu = Me.Print_CntxtMStrip
        Me.Print_Btn.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Print_Btn.Location = New System.Drawing.Point(5, 4)
        Me.Print_Btn.Name = "Print_Btn"
        Me.Print_Btn.Padding = New System.Windows.Forms.Padding(1)
        Me.Print_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_Btn.Size = New System.Drawing.Size(144, 47)
        Me.Print_Btn.TabIndex = 906
        '
        'Search_By_Acc_Name_txt
        '
        Me.Search_By_Acc_Name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Search_By_Acc_Name_txt.Font = New System.Drawing.Font("Arial", 12.25!)
        Me.Search_By_Acc_Name_txt.Location = New System.Drawing.Point(471, 54)
        Me.Search_By_Acc_Name_txt.Name = "Search_By_Acc_Name_txt"
        Me.Search_By_Acc_Name_txt.Size = New System.Drawing.Size(529, 26)
        Me.Search_By_Acc_Name_txt.TabIndex = 96
        '
        'Search_By_Acc_Code_txt
        '
        Me.Search_By_Acc_Code_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Search_By_Acc_Code_txt.Font = New System.Drawing.Font("Arial", 12.25!)
        Me.Search_By_Acc_Code_txt.Location = New System.Drawing.Point(5, 54)
        Me.Search_By_Acc_Code_txt.Name = "Search_By_Acc_Code_txt"
        Me.Search_By_Acc_Code_txt.Size = New System.Drawing.Size(465, 26)
        Me.Search_By_Acc_Code_txt.TabIndex = 97
        '
        'RefreshBtn
        '
        Me.RefreshBtn.BackColor = System.Drawing.Color.White
        Me.RefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RefreshBtn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RefreshBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RefreshBtn.Location = New System.Drawing.Point(150, 4)
        Me.RefreshBtn.Name = "RefreshBtn"
        Me.RefreshBtn.Size = New System.Drawing.Size(139, 47)
        Me.RefreshBtn.TabIndex = 100
        Me.RefreshBtn.Text = "تحديث  🔄"
        Me.RefreshBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RefreshBtn.UseVisualStyleBackColor = False
        '
        'CircularProgressControl1
        '
        Me.CircularProgressControl1.BackColor = System.Drawing.Color.Transparent
        Me.CircularProgressControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CircularProgressControl1.Interval = 60
        Me.CircularProgressControl1.Location = New System.Drawing.Point(0, 0)
        Me.CircularProgressControl1.MinimumSize = New System.Drawing.Size(28, 28)
        Me.CircularProgressControl1.Name = "CircularProgressControl1"
        Me.CircularProgressControl1.Rotation = Accounting.CircularProgressControl.Direction.CLOCKWISE
        Me.CircularProgressControl1.Size = New System.Drawing.Size(922, 50)
        Me.CircularProgressControl1.StartAngle = 270
        Me.CircularProgressControl1.TabIndex = 87
        Me.CircularProgressControl1.TickColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        '
        'CircularPanel
        '
        Me.CircularPanel.BackColor = System.Drawing.Color.Transparent
        Me.CircularPanel.Controls.Add(Me.CircularProgressControl1)
        Me.CircularPanel.Location = New System.Drawing.Point(6, 683)
        Me.CircularPanel.Name = "CircularPanel"
        Me.CircularPanel.Size = New System.Drawing.Size(922, 50)
        Me.CircularPanel.TabIndex = 907
        Me.CircularPanel.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Rows_txt)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Total_B_D_txt)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.Total_B_C_txt)
        Me.Panel1.Controls.Add(Me.Total_D_txt)
        Me.Panel1.Controls.Add(Me.Total_C_txt)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Dif_TXT)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(3, 676)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1001, 65)
        Me.Panel1.TabIndex = 906
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(652, 10)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(139, 19)
        Me.Label8.TabIndex = 80
        Me.Label8.Text = "إجمالي مدين - المجاميع:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(490, 10)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(139, 19)
        Me.Label11.TabIndex = 79
        Me.Label11.Text = "إجمالي دائـن - المجاميع:"
        '
        'Rows_txt
        '
        Me.Rows_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Rows_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Rows_txt.Location = New System.Drawing.Point(804, 32)
        Me.Rows_txt.Margin = New System.Windows.Forms.Padding(4)
        Me.Rows_txt.Name = "Rows_txt"
        Me.Rows_txt.ReadOnly = True
        Me.Rows_txt.Size = New System.Drawing.Size(122, 29)
        Me.Rows_txt.TabIndex = 81
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(834, 10)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(59, 19)
        Me.Label12.TabIndex = 82
        Me.Label12.Text = "الصفوف:"
        '
        'Total_B_D_txt
        '
        Me.Total_B_D_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Total_B_D_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_B_D_txt.Enabled = False
        Me.Total_B_D_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Total_B_D_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.Total_B_D_txt.Location = New System.Drawing.Point(321, 32)
        Me.Total_B_D_txt.MaxLength = 0
        Me.Total_B_D_txt.Name = "Total_B_D_txt"
        Me.Total_B_D_txt.Size = New System.Drawing.Size(160, 29)
        Me.Total_B_D_txt.TabIndex = 99
        Me.Total_B_D_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(328, 10)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(143, 19)
        Me.Label14.TabIndex = 83
        Me.Label14.Text = "إجمالي مديـن - الأرصــدة:"
        '
        'Total_B_C_txt
        '
        Me.Total_B_C_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Total_B_C_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_B_C_txt.Enabled = False
        Me.Total_B_C_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Total_B_C_txt.ForeColor = System.Drawing.Color.DarkRed
        Me.Total_B_C_txt.Location = New System.Drawing.Point(160, 32)
        Me.Total_B_C_txt.MaxLength = 0
        Me.Total_B_C_txt.Name = "Total_B_C_txt"
        Me.Total_B_C_txt.Size = New System.Drawing.Size(160, 29)
        Me.Total_B_C_txt.TabIndex = 98
        Me.Total_B_C_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Total_D_txt
        '
        Me.Total_D_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Total_D_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_D_txt.Enabled = False
        Me.Total_D_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Total_D_txt.ForeColor = System.Drawing.Color.DarkRed
        Me.Total_D_txt.Location = New System.Drawing.Point(482, 32)
        Me.Total_D_txt.MaxLength = 0
        Me.Total_D_txt.Name = "Total_D_txt"
        Me.Total_D_txt.Size = New System.Drawing.Size(160, 29)
        Me.Total_D_txt.TabIndex = 85
        Me.Total_D_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Total_C_txt
        '
        Me.Total_C_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Total_C_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_C_txt.Enabled = False
        Me.Total_C_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Total_C_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.Total_C_txt.Location = New System.Drawing.Point(643, 32)
        Me.Total_C_txt.MaxLength = 0
        Me.Total_C_txt.Name = "Total_C_txt"
        Me.Total_C_txt.Size = New System.Drawing.Size(160, 29)
        Me.Total_C_txt.TabIndex = 86
        Me.Total_C_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(168, 10)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 19)
        Me.Label1.TabIndex = 89
        Me.Label1.Text = "إجمالي دائـن - الأرصــدة:"
        '
        'Dif_TXT
        '
        Me.Dif_TXT.BackColor = System.Drawing.Color.Lavender
        Me.Dif_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Dif_TXT.Enabled = False
        Me.Dif_TXT.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Dif_TXT.Location = New System.Drawing.Point(3, 32)
        Me.Dif_TXT.MaxLength = 0
        Me.Dif_TXT.Name = "Dif_TXT"
        Me.Dif_TXT.Size = New System.Drawing.Size(156, 29)
        Me.Dif_TXT.TabIndex = 91
        Me.Dif_TXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(57, 9)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 19)
        Me.Label2.TabIndex = 92
        Me.Label2.Text = "الفــرق:"
        '
        'DateRange_Flate1
        '
        Me.DateRange_Flate1.AutoSize = True
        Me.DateRange_Flate1.BackColor = System.Drawing.Color.Transparent
        Me.DateRange_Flate1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DateRange_Flate1.Location = New System.Drawing.Point(3, 1)
        Me.DateRange_Flate1.Name = "DateRange_Flate1"
        Me.DateRange_Flate1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DateRange_Flate1.Size = New System.Drawing.Size(535, 79)
        Me.DateRange_Flate1.TabIndex = 905
        '
        'TITLE_txt
        '
        Me.TITLE_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TITLE_txt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TITLE_txt.Font = New System.Drawing.Font("Arial", 17.0!, System.Drawing.FontStyle.Bold)
        Me.TITLE_txt.Location = New System.Drawing.Point(539, 1)
        Me.TITLE_txt.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.TITLE_txt.Name = "TITLE_txt"
        Me.TITLE_txt.Size = New System.Drawing.Size(465, 79)
        Me.TITLE_txt.TabIndex = 904
        Me.TITLE_txt.Text = "أرصدة مراكز التكلفة"
        Me.TITLE_txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 10.25!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.Location = New System.Drawing.Point(3, 165)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 30
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(1001, 511)
        Me.DataGridView1.TabIndex = 902
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(2, 742)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(1002, 37)
        Me.Button1.TabIndex = 903
        Me.Button1.Text = "عـــودة   ↩️"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Cost_Center_Balances
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 780)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.CircularPanel)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DateRange_Flate1)
        Me.Controls.Add(Me.TITLE_txt)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Cost_Center_Balances"
        Me.Text = "أرصدة مراكز التكلفة"
        Me.Print_CntxtMStrip.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.CircularPanel.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents إستخراجالتقريرExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Cost_Center_Control1 As Cost_Center_Control
    Friend WithEvents Print_Btn As SplitButton
    Friend WithEvents Print_CntxtMStrip As ContextMenuStrip
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Search_By_Acc_Name_txt As TextBox
    Friend WithEvents Search_By_Acc_Code_txt As TextBox
    Friend WithEvents RefreshBtn As Button
    Friend WithEvents CircularProgressControl1 As CircularProgressControl
    Friend WithEvents CircularPanel As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Rows_txt As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Total_B_D_txt As F2FloatField_Credit
    Friend WithEvents Label14 As Label
    Friend WithEvents Total_B_C_txt As F2FloatField_Debit
    Friend WithEvents Total_D_txt As F2FloatField_Debit
    Friend WithEvents Total_C_txt As F2FloatField_Credit
    Friend WithEvents Label1 As Label
    Friend WithEvents Dif_TXT As F2FloatField_Balance
    Friend WithEvents Label2 As Label
    Friend WithEvents DateRange_Flate1 As DateRange_Flate
    Friend WithEvents TITLE_txt As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Title_Label As Label
End Class
