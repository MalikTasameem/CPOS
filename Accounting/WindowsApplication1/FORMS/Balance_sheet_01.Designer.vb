<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Balance_sheet_01
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DetailsPrint_CntxtMStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.إستخراجالتقريرExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OfficialPrint_CntxtMStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.CircularPanel = New System.Windows.Forms.Panel()
        Me.CircularProgressControl1 = New Accounting.CircularProgressControl()
        Me.DataGridView = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ACC_CODE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ACC_PARENT_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BALANCE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SIDE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ACC_LEVEL_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ROWTYPE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ShowAbnormalMark_CB = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ACCOUNT_UpDown = New System.Windows.Forms.NumericUpDown()
        Me.TOTAL_UpDown = New System.Windows.Forms.NumericUpDown()
        Me.PrintOfficial_Btn = New Accounting.SplitButton()
        Me.Print_Btn = New Accounting.SplitButton()
        Me.CLOSE_B_Btn = New System.Windows.Forms.Button()
        Me.Hide_Zeros_CB = New System.Windows.Forms.CheckBox()
        Me.DateRange_Flate1 = New Accounting.DateRange_Flate()
        Me.Search_btn = New System.Windows.Forms.Button()
        Me.TITLE_txt = New System.Windows.Forms.Label()
        Me.DetailsPrint_CntxtMStrip.SuspendLayout()
        Me.OfficialPrint_CntxtMStrip.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.CircularPanel.SuspendLayout()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.ACCOUNT_UpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TOTAL_UpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DetailsPrint_CntxtMStrip
        '
        Me.DetailsPrint_CntxtMStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.إستخراجالتقريرExcelToolStripMenuItem})
        Me.DetailsPrint_CntxtMStrip.Name = "ContextMenuStrip1"
        Me.DetailsPrint_CntxtMStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DetailsPrint_CntxtMStrip.Size = New System.Drawing.Size(177, 26)
        '
        'إستخراجالتقريرExcelToolStripMenuItem
        '
        Me.إستخراجالتقريرExcelToolStripMenuItem.Name = "إستخراجالتقريرExcelToolStripMenuItem"
        Me.إستخراجالتقريرExcelToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.إستخراجالتقريرExcelToolStripMenuItem.Text = "إستخراج التقرير Excel"
        '
        'OfficialPrint_CntxtMStrip
        '
        Me.OfficialPrint_CntxtMStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.OfficialPrint_CntxtMStrip.Name = "ContextMenuStrip1"
        Me.OfficialPrint_CntxtMStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.OfficialPrint_CntxtMStrip.Size = New System.Drawing.Size(177, 26)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem1.Text = "إستخراج التقرير Excel"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.CircularPanel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridView, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.49405!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.50594!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1082, 817)
        Me.TableLayoutPanel1.TabIndex = 87
        '
        'CircularPanel
        '
        Me.CircularPanel.BackColor = System.Drawing.Color.Transparent
        Me.CircularPanel.Controls.Add(Me.CircularProgressControl1)
        Me.CircularPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CircularPanel.Location = New System.Drawing.Point(3, 760)
        Me.CircularPanel.Name = "CircularPanel"
        Me.CircularPanel.Size = New System.Drawing.Size(1076, 54)
        Me.CircularPanel.TabIndex = 899
        Me.CircularPanel.Visible = False
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
        Me.CircularProgressControl1.Size = New System.Drawing.Size(1076, 54)
        Me.CircularProgressControl1.StartAngle = 270
        Me.CircularProgressControl1.TabIndex = 87
        Me.CircularProgressControl1.TickColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        '
        'DataGridView
        '
        Me.DataGridView.AllowUserToAddRows = False
        Me.DataGridView.AllowUserToDeleteRows = False
        Me.DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.ACC_CODE_CL, Me.ACC_PARENT_CL, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.BALANCE_CL, Me.SIDE_CL, Me.ACC_LEVEL_CL, Me.ROWTYPE_CL})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView.Location = New System.Drawing.Point(6, 146)
        Me.DataGridView.Margin = New System.Windows.Forms.Padding(6)
        Me.DataGridView.MultiSelect = False
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.ReadOnly = True
        Me.DataGridView.RowHeadersVisible = False
        Me.DataGridView.RowTemplate.Height = 30
        Me.DataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView.Size = New System.Drawing.Size(1070, 605)
        Me.DataGridView.TabIndex = 87
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "T_ID"
        Me.DataGridViewTextBoxColumn1.FillWeight = 35.53299!
        Me.DataGridViewTextBoxColumn1.HeaderText = "ت"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'ACC_CODE_CL
        '
        Me.ACC_CODE_CL.DataPropertyName = "ACC_CODE"
        Me.ACC_CODE_CL.FillWeight = 110.7445!
        Me.ACC_CODE_CL.HeaderText = " كــود الحســـاب "
        Me.ACC_CODE_CL.Name = "ACC_CODE_CL"
        Me.ACC_CODE_CL.ReadOnly = True
        '
        'ACC_PARENT_CL
        '
        Me.ACC_PARENT_CL.DataPropertyName = "ACC_PARENT"
        Me.ACC_PARENT_CL.FillWeight = 110.7445!
        Me.ACC_PARENT_CL.HeaderText = " تبيعة الحســـاب "
        Me.ACC_PARENT_CL.Name = "ACC_PARENT_CL"
        Me.ACC_PARENT_CL.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "ACC_NAME_1"
        Me.DataGridViewTextBoxColumn4.FillWeight = 110.7445!
        Me.DataGridViewTextBoxColumn4.HeaderText = ""
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "ACC_NAME_2"
        Me.DataGridViewTextBoxColumn5.FillWeight = 110.7445!
        Me.DataGridViewTextBoxColumn5.HeaderText = ""
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "ACC_NAME_3"
        Me.DataGridViewTextBoxColumn6.FillWeight = 110.7445!
        Me.DataGridViewTextBoxColumn6.HeaderText = ""
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'BALANCE_CL
        '
        Me.BALANCE_CL.DataPropertyName = "BALANCE"
        DataGridViewCellStyle1.Format = "N3"
        Me.BALANCE_CL.DefaultCellStyle = DataGridViewCellStyle1
        Me.BALANCE_CL.FillWeight = 110.7445!
        Me.BALANCE_CL.HeaderText = ""
        Me.BALANCE_CL.Name = "BALANCE_CL"
        Me.BALANCE_CL.ReadOnly = True
        '
        'SIDE_CL
        '
        Me.SIDE_CL.DataPropertyName = "SIDE"
        Me.SIDE_CL.HeaderText = "SIDE"
        Me.SIDE_CL.Name = "SIDE_CL"
        Me.SIDE_CL.ReadOnly = True
        Me.SIDE_CL.Visible = False
        '
        'ACC_LEVEL_CL
        '
        Me.ACC_LEVEL_CL.DataPropertyName = "ACC_LEVEL"
        Me.ACC_LEVEL_CL.HeaderText = "ACC_LEVEL"
        Me.ACC_LEVEL_CL.Name = "ACC_LEVEL_CL"
        Me.ACC_LEVEL_CL.ReadOnly = True
        Me.ACC_LEVEL_CL.Visible = False
        '
        'ROWTYPE_CL
        '
        Me.ROWTYPE_CL.DataPropertyName = "RowType"
        Me.ROWTYPE_CL.HeaderText = "RowType"
        Me.ROWTYPE_CL.Name = "ROWTYPE_CL"
        Me.ROWTYPE_CL.ReadOnly = True
        Me.ROWTYPE_CL.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ShowAbnormalMark_CB)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.ACCOUNT_UpDown)
        Me.Panel1.Controls.Add(Me.TOTAL_UpDown)
        Me.Panel1.Controls.Add(Me.PrintOfficial_Btn)
        Me.Panel1.Controls.Add(Me.Print_Btn)
        Me.Panel1.Controls.Add(Me.CLOSE_B_Btn)
        Me.Panel1.Controls.Add(Me.Hide_Zeros_CB)
        Me.Panel1.Controls.Add(Me.DateRange_Flate1)
        Me.Panel1.Controls.Add(Me.Search_btn)
        Me.Panel1.Controls.Add(Me.TITLE_txt)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1076, 134)
        Me.Panel1.TabIndex = 0
        '
        'ShowAbnormalMark_CB
        '
        Me.ShowAbnormalMark_CB.AutoSize = True
        Me.ShowAbnormalMark_CB.Checked = True
        Me.ShowAbnormalMark_CB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShowAbnormalMark_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ShowAbnormalMark_CB.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ShowAbnormalMark_CB.Location = New System.Drawing.Point(159, 112)
        Me.ShowAbnormalMark_CB.Name = "ShowAbnormalMark_CB"
        Me.ShowAbnormalMark_CB.Size = New System.Drawing.Size(149, 19)
        Me.ShowAbnormalMark_CB.TabIndex = 911
        Me.ShowAbnormalMark_CB.Text = "إظهــار علامة للحسابات الشــاذة"
        Me.ShowAbnormalMark_CB.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(470, 113)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 16)
        Me.Label2.TabIndex = 910
        Me.Label2.Text = "مستوى الحساب"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(470, 89)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 16)
        Me.Label1.TabIndex = 909
        Me.Label1.Text = "مستوى الإجمالي"
        '
        'ACCOUNT_UpDown
        '
        Me.ACCOUNT_UpDown.Font = New System.Drawing.Font("Arial", 10.25!, System.Drawing.FontStyle.Bold)
        Me.ACCOUNT_UpDown.Location = New System.Drawing.Point(431, 108)
        Me.ACCOUNT_UpDown.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.ACCOUNT_UpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.ACCOUNT_UpDown.Name = "ACCOUNT_UpDown"
        Me.ACCOUNT_UpDown.Size = New System.Drawing.Size(36, 23)
        Me.ACCOUNT_UpDown.TabIndex = 908
        Me.ACCOUNT_UpDown.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'TOTAL_UpDown
        '
        Me.TOTAL_UpDown.Font = New System.Drawing.Font("Arial", 10.25!, System.Drawing.FontStyle.Bold)
        Me.TOTAL_UpDown.Location = New System.Drawing.Point(431, 84)
        Me.TOTAL_UpDown.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.TOTAL_UpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.TOTAL_UpDown.Name = "TOTAL_UpDown"
        Me.TOTAL_UpDown.Size = New System.Drawing.Size(36, 23)
        Me.TOTAL_UpDown.TabIndex = 907
        Me.TOTAL_UpDown.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'PrintOfficial_Btn
        '
        Me.PrintOfficial_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PrintOfficial_Btn.BackColor = System.Drawing.Color.White
        Me.PrintOfficial_Btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PrintOfficial_Btn.ButtonImage = Nothing
        Me.PrintOfficial_Btn.ButtonText = "🖨️  طباعــة (رسمية)"
        Me.PrintOfficial_Btn.DropDownMenu = Me.OfficialPrint_CntxtMStrip
        Me.PrintOfficial_Btn.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.PrintOfficial_Btn.Location = New System.Drawing.Point(605, 84)
        Me.PrintOfficial_Btn.Name = "PrintOfficial_Btn"
        Me.PrintOfficial_Btn.Padding = New System.Windows.Forms.Padding(1)
        Me.PrintOfficial_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PrintOfficial_Btn.Size = New System.Drawing.Size(162, 40)
        Me.PrintOfficial_Btn.TabIndex = 905
        '
        'Print_Btn
        '
        Me.Print_Btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Print_Btn.BackColor = System.Drawing.Color.White
        Me.Print_Btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Print_Btn.ButtonImage = Nothing
        Me.Print_Btn.ButtonText = "🖨️  طباعــة (تفصيلي)"
        Me.Print_Btn.DropDownMenu = Me.DetailsPrint_CntxtMStrip
        Me.Print_Btn.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Print_Btn.Location = New System.Drawing.Point(768, 84)
        Me.Print_Btn.Name = "Print_Btn"
        Me.Print_Btn.Padding = New System.Windows.Forms.Padding(1)
        Me.Print_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_Btn.Size = New System.Drawing.Size(162, 40)
        Me.Print_Btn.TabIndex = 904
        '
        'CLOSE_B_Btn
        '
        Me.CLOSE_B_Btn.BackColor = System.Drawing.Color.White
        Me.CLOSE_B_Btn.ContextMenuStrip = Me.DetailsPrint_CntxtMStrip
        Me.CLOSE_B_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CLOSE_B_Btn.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.CLOSE_B_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CLOSE_B_Btn.Location = New System.Drawing.Point(3, 84)
        Me.CLOSE_B_Btn.Name = "CLOSE_B_Btn"
        Me.CLOSE_B_Btn.Size = New System.Drawing.Size(42, 40)
        Me.CLOSE_B_Btn.TabIndex = 903
        Me.CLOSE_B_Btn.Text = "ترحيل الأرصـــدة  ✔️ "
        Me.CLOSE_B_Btn.UseVisualStyleBackColor = False
        Me.CLOSE_B_Btn.Visible = False
        '
        'Hide_Zeros_CB
        '
        Me.Hide_Zeros_CB.AutoSize = True
        Me.Hide_Zeros_CB.Checked = True
        Me.Hide_Zeros_CB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Hide_Zeros_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Hide_Zeros_CB.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Hide_Zeros_CB.Location = New System.Drawing.Point(182, 88)
        Me.Hide_Zeros_CB.Name = "Hide_Zeros_CB"
        Me.Hide_Zeros_CB.Size = New System.Drawing.Size(126, 19)
        Me.Hide_Zeros_CB.TabIndex = 106
        Me.Hide_Zeros_CB.Text = "إخـفاء الحسابـات الصفرية"
        Me.Hide_Zeros_CB.UseVisualStyleBackColor = True
        '
        'DateRange_Flate1
        '
        Me.DateRange_Flate1.AutoSize = True
        Me.DateRange_Flate1.BackColor = System.Drawing.Color.Transparent
        Me.DateRange_Flate1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DateRange_Flate1.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.DateRange_Flate1.Location = New System.Drawing.Point(3, 4)
        Me.DateRange_Flate1.Name = "DateRange_Flate1"
        Me.DateRange_Flate1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DateRange_Flate1.Size = New System.Drawing.Size(502, 79)
        Me.DateRange_Flate1.TabIndex = 105
        '
        'Search_btn
        '
        Me.Search_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Search_btn.BackColor = System.Drawing.Color.White
        Me.Search_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Search_btn.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Search_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Search_btn.Location = New System.Drawing.Point(931, 84)
        Me.Search_btn.Margin = New System.Windows.Forms.Padding(4)
        Me.Search_btn.Name = "Search_btn"
        Me.Search_btn.Size = New System.Drawing.Size(141, 40)
        Me.Search_btn.TabIndex = 84
        Me.Search_btn.Text = "🔍  بحـــث"
        Me.Search_btn.UseVisualStyleBackColor = False
        '
        'TITLE_txt
        '
        Me.TITLE_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TITLE_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TITLE_txt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TITLE_txt.Font = New System.Drawing.Font("Arial", 17.0!, System.Drawing.FontStyle.Bold)
        Me.TITLE_txt.Location = New System.Drawing.Point(506, 4)
        Me.TITLE_txt.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.TITLE_txt.Name = "TITLE_txt"
        Me.TITLE_txt.Size = New System.Drawing.Size(566, 79)
        Me.TITLE_txt.TabIndex = 82
        Me.TITLE_txt.Text = "إعـــداد قائمـــــة المركــز المالـــــي"
        Me.TITLE_txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Balance_sheet_01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1082, 817)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.Name = "Balance_sheet_01"
        Me.Text = "قائمة الميزانية"
        Me.DetailsPrint_CntxtMStrip.ResumeLayout(False)
        Me.OfficialPrint_CntxtMStrip.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.CircularPanel.ResumeLayout(False)
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.ACCOUNT_UpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TOTAL_UpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Search_btn As Button
    Friend WithEvents TITLE_txt As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents DateRange_Flate1 As DateRange_Flate
    Friend WithEvents DataGridView As DataGridView
    Friend WithEvents Hide_Zeros_CB As CheckBox
    Friend WithEvents CircularPanel As Panel
    Friend WithEvents CircularProgressControl1 As CircularProgressControl
    Friend WithEvents CLOSE_B_Btn As Button
    Friend WithEvents Print_Btn As SplitButton
    Friend WithEvents DetailsPrint_CntxtMStrip As ContextMenuStrip
    Friend WithEvents إستخراجالتقريرExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PrintOfficial_Btn As SplitButton
    Friend WithEvents ACCOUNT_UpDown As NumericUpDown
    Friend WithEvents TOTAL_UpDown As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents OfficialPrint_CntxtMStrip As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents ACC_CODE_CL As DataGridViewTextBoxColumn
    Friend WithEvents ACC_PARENT_CL As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents BALANCE_CL As DataGridViewTextBoxColumn
    Friend WithEvents SIDE_CL As DataGridViewTextBoxColumn
    Friend WithEvents ACC_LEVEL_CL As DataGridViewTextBoxColumn
    Friend WithEvents ROWTYPE_CL As DataGridViewTextBoxColumn
    Friend WithEvents ShowAbnormalMark_CB As CheckBox
End Class
