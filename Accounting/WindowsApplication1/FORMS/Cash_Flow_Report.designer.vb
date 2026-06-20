<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Cash_Flow_Report
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
        Dim DataGridViewCellStyle40 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle37 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle38 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle39 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle44 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle41 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle42 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle43 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle48 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle45 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle46 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle47 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CircularProgressControl1 = New Accounting.CircularProgressControl()
        Me.Search_btn = New System.Windows.Forms.Button()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.YEAR_Txt = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Grid2 = New System.Windows.Forms.DataGridView()
        Me.ACC_NAME_2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.START_B_CL_2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.END_B_CL_2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NET_CASH_FLOW_CL_2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Grid3 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NET_CASH_FLOW_CL_3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Grid1 = New System.Windows.Forms.DataGridView()
        Me.ACC_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.START_B_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.END_B_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NET_CASH_FLOW_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Print_Btn = New Accounting.SplitButton()
        Me.Print_CntxtMStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.إستخراجالتقريرExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.Grid2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.Grid3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Print_CntxtMStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.CircularProgressControl1)
        Me.Panel1.Location = New System.Drawing.Point(2, 754)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1002, 71)
        Me.Panel1.TabIndex = 86
        Me.Panel1.Visible = False
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
        Me.CircularProgressControl1.Size = New System.Drawing.Size(1002, 71)
        Me.CircularProgressControl1.StartAngle = 270
        Me.CircularProgressControl1.TabIndex = 88
        Me.CircularProgressControl1.TickColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        '
        'Search_btn
        '
        Me.Search_btn.BackColor = System.Drawing.Color.White
        Me.Search_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Search_btn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Search_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Search_btn.Location = New System.Drawing.Point(609, 1)
        Me.Search_btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Search_btn.Name = "Search_btn"
        Me.Search_btn.Size = New System.Drawing.Size(187, 36)
        Me.Search_btn.TabIndex = 85
        Me.Search_btn.Text = "🔍 عرض التقرير"
        Me.Search_btn.UseVisualStyleBackColor = False
        '
        'TextBox3
        '
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.Location = New System.Drawing.Point(5, 650)
        Me.TextBox3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(324, 25)
        Me.TextBox3.TabIndex = 48
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(340, 650)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(324, 25)
        Me.TextBox2.TabIndex = 47
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(675, 650)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(327, 25)
        Me.TextBox1.TabIndex = 46
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(904, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 19)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "السنة المالية"
        '
        'YEAR_Txt
        '
        Me.YEAR_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.YEAR_Txt.Enabled = False
        Me.YEAR_Txt.Location = New System.Drawing.Point(800, 5)
        Me.YEAR_Txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.YEAR_Txt.Name = "YEAR_Txt"
        Me.YEAR_Txt.Size = New System.Drawing.Size(100, 26)
        Me.YEAR_Txt.TabIndex = 44
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Grid2)
        Me.GroupBox3.Location = New System.Drawing.Point(337, 45)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(330, 577)
        Me.GroupBox3.TabIndex = 43
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "التدفقات النقدية من الأنشطة الاستثمارية"
        '
        'Grid2
        '
        Me.Grid2.AllowUserToAddRows = False
        Me.Grid2.AllowUserToDeleteRows = False
        Me.Grid2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.Grid2.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.Grid2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ACC_NAME_2, Me.START_B_CL_2, Me.END_B_CL_2, Me.NET_CASH_FLOW_CL_2})
        DataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle40.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle40.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle40.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle40.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle40.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle40.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Grid2.DefaultCellStyle = DataGridViewCellStyle40
        Me.Grid2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid2.Location = New System.Drawing.Point(3, 23)
        Me.Grid2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Grid2.MultiSelect = False
        Me.Grid2.Name = "Grid2"
        Me.Grid2.ReadOnly = True
        Me.Grid2.RowTemplate.Height = 30
        Me.Grid2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid2.Size = New System.Drawing.Size(324, 550)
        Me.Grid2.TabIndex = 40
        '
        'ACC_NAME_2
        '
        Me.ACC_NAME_2.DataPropertyName = "ACC_NAME"
        Me.ACC_NAME_2.HeaderText = "الحساب"
        Me.ACC_NAME_2.Name = "ACC_NAME_2"
        Me.ACC_NAME_2.ReadOnly = True
        Me.ACC_NAME_2.Width = 80
        '
        'START_B_CL_2
        '
        Me.START_B_CL_2.DataPropertyName = "START_BALANCE"
        DataGridViewCellStyle37.Format = "N3"
        Me.START_B_CL_2.DefaultCellStyle = DataGridViewCellStyle37
        Me.START_B_CL_2.HeaderText = "رصيد البداية"
        Me.START_B_CL_2.Name = "START_B_CL_2"
        Me.START_B_CL_2.ReadOnly = True
        Me.START_B_CL_2.Width = 106
        '
        'END_B_CL_2
        '
        Me.END_B_CL_2.DataPropertyName = "END_BALANCE"
        DataGridViewCellStyle38.Format = "N3"
        Me.END_B_CL_2.DefaultCellStyle = DataGridViewCellStyle38
        Me.END_B_CL_2.HeaderText = "رصيد النهاية"
        Me.END_B_CL_2.Name = "END_B_CL_2"
        Me.END_B_CL_2.ReadOnly = True
        Me.END_B_CL_2.Width = 106
        '
        'NET_CASH_FLOW_CL_2
        '
        Me.NET_CASH_FLOW_CL_2.DataPropertyName = "NET_CASH_FLOW"
        DataGridViewCellStyle39.Format = "N3"
        Me.NET_CASH_FLOW_CL_2.DefaultCellStyle = DataGridViewCellStyle39
        Me.NET_CASH_FLOW_CL_2.HeaderText = "صافي التدفق"
        Me.NET_CASH_FLOW_CL_2.Name = "NET_CASH_FLOW_CL_2"
        Me.NET_CASH_FLOW_CL_2.ReadOnly = True
        Me.NET_CASH_FLOW_CL_2.Width = 114
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Grid3)
        Me.GroupBox2.Location = New System.Drawing.Point(2, 45)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(330, 577)
        Me.GroupBox2.TabIndex = 42
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "التدفقات النقدية من الأنشطة التمويلية"
        '
        'Grid3
        '
        Me.Grid3.AllowUserToAddRows = False
        Me.Grid3.AllowUserToDeleteRows = False
        Me.Grid3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.Grid3.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.Grid3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid3.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.NET_CASH_FLOW_CL_3})
        DataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle44.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle44.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle44.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle44.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle44.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle44.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Grid3.DefaultCellStyle = DataGridViewCellStyle44
        Me.Grid3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid3.Location = New System.Drawing.Point(3, 23)
        Me.Grid3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Grid3.MultiSelect = False
        Me.Grid3.Name = "Grid3"
        Me.Grid3.ReadOnly = True
        Me.Grid3.RowTemplate.Height = 30
        Me.Grid3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid3.Size = New System.Drawing.Size(324, 550)
        Me.Grid3.TabIndex = 40
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "ACC_NAME"
        Me.Column1.HeaderText = "الحساب"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 80
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "START_BALANCE"
        DataGridViewCellStyle41.Format = "N3"
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle41
        Me.Column2.HeaderText = "رصيد البداية"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 106
        '
        'Column3
        '
        Me.Column3.DataPropertyName = "END_BALANCE"
        DataGridViewCellStyle42.Format = "N3"
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle42
        Me.Column3.HeaderText = "رصيد النهاية"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 106
        '
        'NET_CASH_FLOW_CL_3
        '
        Me.NET_CASH_FLOW_CL_3.DataPropertyName = "NET_CASH_FLOW"
        DataGridViewCellStyle43.Format = "N3"
        Me.NET_CASH_FLOW_CL_3.DefaultCellStyle = DataGridViewCellStyle43
        Me.NET_CASH_FLOW_CL_3.HeaderText = "صافي التدفق"
        Me.NET_CASH_FLOW_CL_3.Name = "NET_CASH_FLOW_CL_3"
        Me.NET_CASH_FLOW_CL_3.ReadOnly = True
        Me.NET_CASH_FLOW_CL_3.Width = 114
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Grid1)
        Me.GroupBox1.Location = New System.Drawing.Point(672, 45)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(330, 577)
        Me.GroupBox1.TabIndex = 41
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "التدفقات النقدية من الأنشطة التشغيلية"
        '
        'Grid1
        '
        Me.Grid1.AllowUserToAddRows = False
        Me.Grid1.AllowUserToDeleteRows = False
        Me.Grid1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.Grid1.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.Grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ACC_NAME, Me.START_B_CL, Me.END_B_CL, Me.NET_CASH_FLOW_CL})
        DataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle48.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle48.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle48.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle48.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle48.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle48.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Grid1.DefaultCellStyle = DataGridViewCellStyle48
        Me.Grid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid1.Location = New System.Drawing.Point(3, 23)
        Me.Grid1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Grid1.MultiSelect = False
        Me.Grid1.Name = "Grid1"
        Me.Grid1.ReadOnly = True
        Me.Grid1.RowTemplate.Height = 30
        Me.Grid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid1.Size = New System.Drawing.Size(324, 550)
        Me.Grid1.TabIndex = 40
        '
        'ACC_NAME
        '
        Me.ACC_NAME.DataPropertyName = "ACC_NAME"
        Me.ACC_NAME.HeaderText = "الحساب"
        Me.ACC_NAME.Name = "ACC_NAME"
        Me.ACC_NAME.ReadOnly = True
        Me.ACC_NAME.Width = 80
        '
        'START_B_CL
        '
        Me.START_B_CL.DataPropertyName = "START_BALANCE"
        DataGridViewCellStyle45.Format = "N3"
        Me.START_B_CL.DefaultCellStyle = DataGridViewCellStyle45
        Me.START_B_CL.HeaderText = "رصيد البداية"
        Me.START_B_CL.Name = "START_B_CL"
        Me.START_B_CL.ReadOnly = True
        Me.START_B_CL.Width = 106
        '
        'END_B_CL
        '
        Me.END_B_CL.DataPropertyName = "END_BALANCE"
        DataGridViewCellStyle46.Format = "N3"
        Me.END_B_CL.DefaultCellStyle = DataGridViewCellStyle46
        Me.END_B_CL.HeaderText = "رصيد النهاية"
        Me.END_B_CL.Name = "END_B_CL"
        Me.END_B_CL.ReadOnly = True
        Me.END_B_CL.Width = 106
        '
        'NET_CASH_FLOW_CL
        '
        Me.NET_CASH_FLOW_CL.DataPropertyName = "NET_CASH_FLOW"
        DataGridViewCellStyle47.Format = "N3"
        Me.NET_CASH_FLOW_CL.DefaultCellStyle = DataGridViewCellStyle47
        Me.NET_CASH_FLOW_CL.HeaderText = "صافي التدفق"
        Me.NET_CASH_FLOW_CL.Name = "NET_CASH_FLOW_CL"
        Me.NET_CASH_FLOW_CL.ReadOnly = True
        Me.NET_CASH_FLOW_CL.Width = 114
        '
        'TextBox4
        '
        Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox4.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox4.Location = New System.Drawing.Point(5, 724)
        Me.TextBox4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.Size = New System.Drawing.Size(994, 25)
        Me.TextBox4.TabIndex = 87
        Me.TextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(454, 701)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(154, 19)
        Me.Label2.TabIndex = 88
        Me.Label2.Text = "إجمالي التدفقات النقدية "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(707, 627)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(280, 19)
        Me.Label3.TabIndex = 89
        Me.Label3.Text = "صافي التدفقات النقدية من الأنشطة التشغيلية"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(366, 627)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(290, 19)
        Me.Label4.TabIndex = 90
        Me.Label4.Text = "صافي التدفقات النقدية من الأنشطة الاستثمارية"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(36, 627)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(276, 19)
        Me.Label5.TabIndex = 91
        Me.Label5.Text = "صافي التدفقات النقدية من الأنشطة التمويلية"
        '
        'Print_Btn
        '
        Me.Print_Btn.BackColor = System.Drawing.Color.White
        Me.Print_Btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Print_Btn.ButtonImage = Nothing
        Me.Print_Btn.ButtonText = "🖨️  طباعــة"
        Me.Print_Btn.ContextMenuStrip = Me.Print_CntxtMStrip
        Me.Print_Btn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Print_Btn.Location = New System.Drawing.Point(436, 1)
        Me.Print_Btn.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Print_Btn.Name = "Print_Btn"
        Me.Print_Btn.Padding = New System.Windows.Forms.Padding(1)
        Me.Print_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_Btn.Size = New System.Drawing.Size(172, 36)
        Me.Print_Btn.TabIndex = 911
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
        'Cash_Flow_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 825)
        Me.Controls.Add(Me.Print_Btn)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Search_btn)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.YEAR_Txt)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Name = "Cash_Flow_Report"
        Me.Text = "Cash_Flow_Report"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.Grid2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.Grid3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.Grid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Print_CntxtMStrip.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Grid1 As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Grid3 As DataGridView
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Grid2 As DataGridView
    Friend WithEvents YEAR_Txt As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Search_btn As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents CircularProgressControl1 As CircularProgressControl
    Friend WithEvents ACC_NAME As DataGridViewTextBoxColumn
    Friend WithEvents START_B_CL As DataGridViewTextBoxColumn
    Friend WithEvents END_B_CL As DataGridViewTextBoxColumn
    Friend WithEvents NET_CASH_FLOW_CL As DataGridViewTextBoxColumn
    Friend WithEvents ACC_NAME_2 As DataGridViewTextBoxColumn
    Friend WithEvents START_B_CL_2 As DataGridViewTextBoxColumn
    Friend WithEvents END_B_CL_2 As DataGridViewTextBoxColumn
    Friend WithEvents NET_CASH_FLOW_CL_2 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents NET_CASH_FLOW_CL_3 As DataGridViewTextBoxColumn
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Print_Btn As SplitButton
    Friend WithEvents Print_CntxtMStrip As ContextMenuStrip
    Friend WithEvents إستخراجالتقريرExcelToolStripMenuItem As ToolStripMenuItem
End Class
