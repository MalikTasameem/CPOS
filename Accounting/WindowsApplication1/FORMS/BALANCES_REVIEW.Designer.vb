<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BALANCES_REVIEW
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
        Me.RefreshBtn = New System.Windows.Forms.Button()
        Me.Search_By_Acc_Code_txt = New System.Windows.Forms.TextBox()
        Me.Search_By_Acc_Name_txt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ACC_LEVEL_txt = New System.Windows.Forms.DomainUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TITLE_txt = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Rows_txt = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Total_B_D_txt = New Accounting.F2FloatField_Credit()
        Me.Total_B_C_txt = New Accounting.F2FloatField_Debit()
        Me.Dif_TXT = New Accounting.F2FloatField_Balance()
        Me.Total_C_txt = New Accounting.F2FloatField_Credit()
        Me.Total_D_txt = New Accounting.F2FloatField_Debit()
        Me.DateRange_Flate1 = New Accounting.DateRange_Flate()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CircularPanel = New System.Windows.Forms.Panel()
        Me.CircularProgressControl1 = New Accounting.CircularProgressControl()
        Me.Hide_Zeros_CB = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Cost_Center_Control1 = New Accounting.Cost_Center_Control()
        Me.Print_Btn = New Accounting.SplitButton()
        Me.Print_CntxtMStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.إستخراجالتقريرExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.CircularPanel.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Print_CntxtMStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'RefreshBtn
        '
        Me.RefreshBtn.BackColor = System.Drawing.Color.White
        Me.RefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RefreshBtn.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RefreshBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RefreshBtn.Location = New System.Drawing.Point(257, 9)
        Me.RefreshBtn.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RefreshBtn.Name = "RefreshBtn"
        Me.RefreshBtn.Size = New System.Drawing.Size(125, 52)
        Me.RefreshBtn.TabIndex = 100
        Me.RefreshBtn.Text = "🔄 تحديث"
        Me.RefreshBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RefreshBtn.UseVisualStyleBackColor = False
        '
        'Search_By_Acc_Code_txt
        '
        Me.Search_By_Acc_Code_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Search_By_Acc_Code_txt.Font = New System.Drawing.Font("Arial", 12.25!)
        Me.Search_By_Acc_Code_txt.Location = New System.Drawing.Point(5, 64)
        Me.Search_By_Acc_Code_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Search_By_Acc_Code_txt.Name = "Search_By_Acc_Code_txt"
        Me.Search_By_Acc_Code_txt.Size = New System.Drawing.Size(465, 26)
        Me.Search_By_Acc_Code_txt.TabIndex = 97
        '
        'Search_By_Acc_Name_txt
        '
        Me.Search_By_Acc_Name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Search_By_Acc_Name_txt.Font = New System.Drawing.Font("Arial", 12.25!)
        Me.Search_By_Acc_Name_txt.Location = New System.Drawing.Point(471, 64)
        Me.Search_By_Acc_Name_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Search_By_Acc_Name_txt.Name = "Search_By_Acc_Name_txt"
        Me.Search_By_Acc_Name_txt.Size = New System.Drawing.Size(529, 26)
        Me.Search_By_Acc_Name_txt.TabIndex = 96
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.Label3.Location = New System.Drawing.Point(831, 11)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 16)
        Me.Label3.TabIndex = 95
        Me.Label3.Text = "مستوى الحساب:"
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
        Me.ACC_LEVEL_txt.Location = New System.Drawing.Point(779, 4)
        Me.ACC_LEVEL_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ACC_LEVEL_txt.Name = "ACC_LEVEL_txt"
        Me.ACC_LEVEL_txt.ReadOnly = True
        Me.ACC_LEVEL_txt.Size = New System.Drawing.Size(49, 26)
        Me.ACC_LEVEL_txt.TabIndex = 94
        Me.ACC_LEVEL_txt.Text = "1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(105, 14)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 16)
        Me.Label2.TabIndex = 92
        Me.Label2.Text = "الفــرق:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(239, 14)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 16)
        Me.Label1.TabIndex = 89
        Me.Label1.Text = "إجمالي دائـن - الأرصــدة:"
        '
        'TITLE_txt
        '
        Me.TITLE_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TITLE_txt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TITLE_txt.Font = New System.Drawing.Font("Arial", 17.0!, System.Drawing.FontStyle.Bold)
        Me.TITLE_txt.Location = New System.Drawing.Point(538, 2)
        Me.TITLE_txt.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.TITLE_txt.Name = "TITLE_txt"
        Me.TITLE_txt.Size = New System.Drawing.Size(465, 93)
        Me.TITLE_txt.TabIndex = 88
        Me.TITLE_txt.Text = "ميـزان المراجعــة - حسب المستوى"
        Me.TITLE_txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(1, 882)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(1002, 44)
        Me.Button1.TabIndex = 84
        Me.Button1.Text = "عـــودة   ↩️"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label14.Location = New System.Drawing.Point(406, 14)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(128, 16)
        Me.Label14.TabIndex = 83
        Me.Label14.Text = "إجمالي مديـن - الأرصــدة:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label12.Location = New System.Drawing.Point(910, 14)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(51, 16)
        Me.Label12.TabIndex = 82
        Me.Label12.Text = "الصفوف:"
        '
        'Rows_txt
        '
        Me.Rows_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Rows_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Rows_txt.Location = New System.Drawing.Point(898, 38)
        Me.Rows_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Rows_txt.Name = "Rows_txt"
        Me.Rows_txt.ReadOnly = True
        Me.Rows_txt.Size = New System.Drawing.Size(72, 29)
        Me.Rows_txt.TabIndex = 81
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(751, 14)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(121, 16)
        Me.Label8.TabIndex = 80
        Me.Label8.Text = "إجمالي مدين - المجاميع:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label11.Location = New System.Drawing.Point(581, 14)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(121, 16)
        Me.Label11.TabIndex = 79
        Me.Label11.Text = "إجمالي دائـن - المجاميع:"
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
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.Location = New System.Drawing.Point(2, 197)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 30
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(1001, 607)
        Me.DataGridView1.TabIndex = 66
        '
        'Total_B_D_txt
        '
        Me.Total_B_D_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Total_B_D_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_B_D_txt.Enabled = False
        Me.Total_B_D_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Total_B_D_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.Total_B_D_txt.Location = New System.Drawing.Point(382, 38)
        Me.Total_B_D_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Total_B_D_txt.MaxLength = 0
        Me.Total_B_D_txt.Name = "Total_B_D_txt"
        Me.Total_B_D_txt.Size = New System.Drawing.Size(160, 29)
        Me.Total_B_D_txt.TabIndex = 99
        Me.Total_B_D_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Total_B_C_txt
        '
        Me.Total_B_C_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Total_B_C_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_B_C_txt.Enabled = False
        Me.Total_B_C_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Total_B_C_txt.ForeColor = System.Drawing.Color.DarkRed
        Me.Total_B_C_txt.Location = New System.Drawing.Point(213, 38)
        Me.Total_B_C_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Total_B_C_txt.MaxLength = 0
        Me.Total_B_C_txt.Name = "Total_B_C_txt"
        Me.Total_B_C_txt.Size = New System.Drawing.Size(160, 29)
        Me.Total_B_C_txt.TabIndex = 98
        Me.Total_B_C_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Dif_TXT
        '
        Me.Dif_TXT.BackColor = System.Drawing.Color.Lavender
        Me.Dif_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Dif_TXT.Enabled = False
        Me.Dif_TXT.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Dif_TXT.Location = New System.Drawing.Point(3, 38)
        Me.Dif_TXT.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Dif_TXT.MaxLength = 0
        Me.Dif_TXT.Name = "Dif_TXT"
        Me.Dif_TXT.Size = New System.Drawing.Size(156, 29)
        Me.Dif_TXT.TabIndex = 91
        Me.Dif_TXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Total_C_txt
        '
        Me.Total_C_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Total_C_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_C_txt.Enabled = False
        Me.Total_C_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Total_C_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.Total_C_txt.Location = New System.Drawing.Point(722, 38)
        Me.Total_C_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Total_C_txt.MaxLength = 0
        Me.Total_C_txt.Name = "Total_C_txt"
        Me.Total_C_txt.Size = New System.Drawing.Size(160, 29)
        Me.Total_C_txt.TabIndex = 86
        Me.Total_C_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Total_D_txt
        '
        Me.Total_D_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Total_D_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_D_txt.Enabled = False
        Me.Total_D_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Total_D_txt.ForeColor = System.Drawing.Color.DarkRed
        Me.Total_D_txt.Location = New System.Drawing.Point(551, 38)
        Me.Total_D_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Total_D_txt.MaxLength = 0
        Me.Total_D_txt.Name = "Total_D_txt"
        Me.Total_D_txt.Size = New System.Drawing.Size(160, 29)
        Me.Total_D_txt.TabIndex = 85
        Me.Total_D_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DateRange_Flate1
        '
        Me.DateRange_Flate1.AutoSize = True
        Me.DateRange_Flate1.BackColor = System.Drawing.Color.Transparent
        Me.DateRange_Flate1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DateRange_Flate1.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.DateRange_Flate1.Location = New System.Drawing.Point(2, 2)
        Me.DateRange_Flate1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DateRange_Flate1.Name = "DateRange_Flate1"
        Me.DateRange_Flate1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DateRange_Flate1.Size = New System.Drawing.Size(535, 93)
        Me.DateRange_Flate1.TabIndex = 106
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
        Me.Panel1.Location = New System.Drawing.Point(2, 804)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1001, 77)
        Me.Panel1.TabIndex = 107
        '
        'CircularPanel
        '
        Me.CircularPanel.BackColor = System.Drawing.Color.Transparent
        Me.CircularPanel.Controls.Add(Me.CircularProgressControl1)
        Me.CircularPanel.Location = New System.Drawing.Point(5, 510)
        Me.CircularPanel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CircularPanel.Name = "CircularPanel"
        Me.CircularPanel.Size = New System.Drawing.Size(995, 59)
        Me.CircularPanel.TabIndex = 899
        Me.CircularPanel.Visible = False
        '
        'CircularProgressControl1
        '
        Me.CircularProgressControl1.BackColor = System.Drawing.Color.Transparent
        Me.CircularProgressControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CircularProgressControl1.Interval = 60
        Me.CircularProgressControl1.Location = New System.Drawing.Point(0, 0)
        Me.CircularProgressControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CircularProgressControl1.MinimumSize = New System.Drawing.Size(28, 33)
        Me.CircularProgressControl1.Name = "CircularProgressControl1"
        Me.CircularProgressControl1.Rotation = Accounting.CircularProgressControl.Direction.CLOCKWISE
        Me.CircularProgressControl1.Size = New System.Drawing.Size(995, 59)
        Me.CircularProgressControl1.StartAngle = 270
        Me.CircularProgressControl1.TabIndex = 87
        Me.CircularProgressControl1.TickColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        '
        'Hide_Zeros_CB
        '
        Me.Hide_Zeros_CB.AutoSize = True
        Me.Hide_Zeros_CB.Checked = True
        Me.Hide_Zeros_CB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Hide_Zeros_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Hide_Zeros_CB.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Hide_Zeros_CB.Location = New System.Drawing.Point(779, 38)
        Me.Hide_Zeros_CB.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Hide_Zeros_CB.Name = "Hide_Zeros_CB"
        Me.Hide_Zeros_CB.Size = New System.Drawing.Size(126, 19)
        Me.Hide_Zeros_CB.TabIndex = 108
        Me.Hide_Zeros_CB.Text = "إخفـاء الحسابـات الصفرية"
        Me.Hide_Zeros_CB.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Cost_Center_Control1)
        Me.Panel2.Controls.Add(Me.Print_Btn)
        Me.Panel2.Controls.Add(Me.Search_By_Acc_Name_txt)
        Me.Panel2.Controls.Add(Me.Search_By_Acc_Code_txt)
        Me.Panel2.Controls.Add(Me.Hide_Zeros_CB)
        Me.Panel2.Controls.Add(Me.ACC_LEVEL_txt)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.RefreshBtn)
        Me.Panel2.Location = New System.Drawing.Point(0, 97)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1003, 99)
        Me.Panel2.TabIndex = 901
        '
        'Cost_Center_Control1
        '
        Me.Cost_Center_Control1.Location = New System.Drawing.Point(387, 6)
        Me.Cost_Center_Control1.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Cost_Center_Control1.Name = "Cost_Center_Control1"
        Me.Cost_Center_Control1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Cost_Center_Control1.Size = New System.Drawing.Size(361, 55)
        Me.Cost_Center_Control1.TabIndex = 901
        '
        'Print_Btn
        '
        Me.Print_Btn.BackColor = System.Drawing.Color.White
        Me.Print_Btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Print_Btn.ButtonImage = Nothing
        Me.Print_Btn.ButtonText = "🖨️  طباعــة"
        Me.Print_Btn.DropDownMenu = Me.Print_CntxtMStrip
        Me.Print_Btn.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Print_Btn.Location = New System.Drawing.Point(83, 8)
        Me.Print_Btn.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Print_Btn.Name = "Print_Btn"
        Me.Print_Btn.Padding = New System.Windows.Forms.Padding(1)
        Me.Print_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_Btn.Size = New System.Drawing.Size(172, 53)
        Me.Print_Btn.TabIndex = 906
        '
        'Print_CntxtMStrip
        '
        Me.Print_CntxtMStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.إستخراجالتقريرExcelToolStripMenuItem})
        Me.Print_CntxtMStrip.Name = "ContextMenuStrip1"
        Me.Print_CntxtMStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_CntxtMStrip.Size = New System.Drawing.Size(177, 26)
        '
        'إستخراجالتقريرExcelToolStripMenuItem
        '
        Me.إستخراجالتقريرExcelToolStripMenuItem.Name = "إستخراجالتقريرExcelToolStripMenuItem"
        Me.إستخراجالتقريرExcelToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.إستخراجالتقريرExcelToolStripMenuItem.Text = "إستخراج التقرير Excel"
        '
        'BALANCES_REVIEW
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1003, 926)
        Me.Controls.Add(Me.CircularPanel)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DateRange_Flate1)
        Me.Controls.Add(Me.TITLE_txt)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Name = "BALANCES_REVIEW"
        Me.Text = "ميـزان المراجعــة - حسب المستوى"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.CircularPanel.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Print_CntxtMStrip.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Total_C_txt As F2FloatField_Credit
    Friend WithEvents Total_D_txt As F2FloatField_Debit
    Friend WithEvents Button1 As Button
    Friend WithEvents Label14 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Rows_txt As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents TITLE_txt As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Dif_TXT As F2FloatField_Balance
    Friend WithEvents Label2 As Label
    Friend WithEvents ACC_LEVEL_txt As DomainUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents Search_By_Acc_Code_txt As TextBox
    Friend WithEvents Search_By_Acc_Name_txt As TextBox
    Friend WithEvents Total_B_C_txt As F2FloatField_Debit
    Friend WithEvents Total_B_D_txt As F2FloatField_Credit
    Friend WithEvents RefreshBtn As Button
    Friend WithEvents DateRange_Flate1 As DateRange_Flate
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Hide_Zeros_CB As CheckBox
    Friend WithEvents CircularPanel As Panel
    Friend WithEvents CircularProgressControl1 As CircularProgressControl
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Cost_Center_Control1 As Cost_Center_Control
    Friend WithEvents Print_Btn As SplitButton
    Friend WithEvents Print_CntxtMStrip As ContextMenuStrip
    Friend WithEvents إستخراجالتقريرExcelToolStripMenuItem As ToolStripMenuItem
End Class
