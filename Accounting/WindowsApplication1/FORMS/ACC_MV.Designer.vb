<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ACC_MV
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
        Me.Print_CntxtMStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.إستخراجالتقريرExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Search_btn = New System.Windows.Forms.Button()
        Me.Print_Btn = New Accounting.SplitButton()
        Me.TITLE_txt = New System.Windows.Forms.Label()
        Me.DateRange_Flate1 = New Accounting.DateRange_Flate()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ACC_TYPE_Txt = New System.Windows.Forms.TextBox()
        Me.TOTAL_C_N = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.TOTAL_D_N = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Total_B_txt = New Accounting.F2FloatField_Balance()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Total_C_txt = New Accounting.F2FloatField_Credit()
        Me.Rows_txt = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Total_D_txt = New Accounting.F2FloatField_Debit()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.CircularPanel = New System.Windows.Forms.Panel()
        Me.CircularProgressControl1 = New Accounting.CircularProgressControl()
        Me.Print_CntxtMStrip.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CircularPanel.SuspendLayout()
        Me.SuspendLayout()
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
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Button1, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridView1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CircularPanel, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.22186!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.77814!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1004, 898)
        Me.TableLayoutPanel1.TabIndex = 107
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Search_btn)
        Me.Panel1.Controls.Add(Me.Print_Btn)
        Me.Panel1.Controls.Add(Me.TITLE_txt)
        Me.Panel1.Controls.Add(Me.DateRange_Flate1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 4)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(998, 148)
        Me.Panel1.TabIndex = 0
        '
        'Search_btn
        '
        Me.Search_btn.BackColor = System.Drawing.Color.White
        Me.Search_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Search_btn.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Search_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Search_btn.Location = New System.Drawing.Point(540, 97)
        Me.Search_btn.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Search_btn.Name = "Search_btn"
        Me.Search_btn.Size = New System.Drawing.Size(454, 48)
        Me.Search_btn.TabIndex = 80
        Me.Search_btn.Text = "بحـــث   🔍"
        Me.Search_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Search_btn.UseVisualStyleBackColor = False
        '
        'Print_Btn
        '
        Me.Print_Btn.BackColor = System.Drawing.Color.White
        Me.Print_Btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Print_Btn.ButtonImage = Nothing
        Me.Print_Btn.ButtonText = "طباعــة   🖨️"
        Me.Print_Btn.DropDownMenu = Me.Print_CntxtMStrip
        Me.Print_Btn.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Print_Btn.Location = New System.Drawing.Point(3, 97)
        Me.Print_Btn.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Print_Btn.Name = "Print_Btn"
        Me.Print_Btn.Padding = New System.Windows.Forms.Padding(1)
        Me.Print_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_Btn.Size = New System.Drawing.Size(535, 47)
        Me.Print_Btn.TabIndex = 907
        '
        'TITLE_txt
        '
        Me.TITLE_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TITLE_txt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TITLE_txt.Font = New System.Drawing.Font("Arial", 17.0!, System.Drawing.FontStyle.Bold)
        Me.TITLE_txt.Location = New System.Drawing.Point(540, 5)
        Me.TITLE_txt.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.TITLE_txt.Name = "TITLE_txt"
        Me.TITLE_txt.Size = New System.Drawing.Size(454, 91)
        Me.TITLE_txt.TabIndex = 66
        Me.TITLE_txt.Text = "اسم الحساب"
        '
        'DateRange_Flate1
        '
        Me.DateRange_Flate1.AutoSize = True
        Me.DateRange_Flate1.BackColor = System.Drawing.Color.Transparent
        Me.DateRange_Flate1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DateRange_Flate1.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.DateRange_Flate1.Location = New System.Drawing.Point(3, 5)
        Me.DateRange_Flate1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DateRange_Flate1.Name = "DateRange_Flate1"
        Me.DateRange_Flate1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DateRange_Flate1.Size = New System.Drawing.Size(535, 91)
        Me.DateRange_Flate1.TabIndex = 106
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(3, 848)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(998, 46)
        Me.Button1.TabIndex = 75
        Me.Button1.Text = "عـــودة   ↩️"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.ACC_TYPE_Txt)
        Me.Panel2.Controls.Add(Me.TOTAL_C_N)
        Me.Panel2.Controls.Add(Me.Label20)
        Me.Panel2.Controls.Add(Me.TOTAL_D_N)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Controls.Add(Me.Total_B_txt)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Total_C_txt)
        Me.Panel2.Controls.Add(Me.Rows_txt)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Total_D_txt)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 805)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(998, 35)
        Me.Panel2.TabIndex = 76
        '
        'ACC_TYPE_Txt
        '
        Me.ACC_TYPE_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ACC_TYPE_Txt.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.ACC_TYPE_Txt.Location = New System.Drawing.Point(4, 2)
        Me.ACC_TYPE_Txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ACC_TYPE_Txt.Name = "ACC_TYPE_Txt"
        Me.ACC_TYPE_Txt.ReadOnly = True
        Me.ACC_TYPE_Txt.Size = New System.Drawing.Size(87, 26)
        Me.ACC_TYPE_Txt.TabIndex = 84
        '
        'TOTAL_C_N
        '
        Me.TOTAL_C_N.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TOTAL_C_N.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TOTAL_C_N.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.TOTAL_C_N.Location = New System.Drawing.Point(770, 4)
        Me.TOTAL_C_N.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TOTAL_C_N.Name = "TOTAL_C_N"
        Me.TOTAL_C_N.ReadOnly = True
        Me.TOTAL_C_N.Size = New System.Drawing.Size(44, 24)
        Me.TOTAL_C_N.TabIndex = 82
        Me.TOTAL_C_N.Text = "0"
        Me.TOTAL_C_N.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label20.Location = New System.Drawing.Point(816, 8)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(67, 14)
        Me.Label20.TabIndex = 83
        Me.Label20.Text = "عدد المدين:"
        '
        'TOTAL_D_N
        '
        Me.TOTAL_D_N.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TOTAL_D_N.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TOTAL_D_N.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.TOTAL_D_N.Location = New System.Drawing.Point(651, 4)
        Me.TOTAL_D_N.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TOTAL_D_N.Name = "TOTAL_D_N"
        Me.TOTAL_D_N.ReadOnly = True
        Me.TOTAL_D_N.Size = New System.Drawing.Size(44, 24)
        Me.TOTAL_D_N.TabIndex = 80
        Me.TOTAL_D_N.Text = "0"
        Me.TOTAL_D_N.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(698, 8)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(63, 14)
        Me.Label19.TabIndex = 81
        Me.Label19.Text = "عدد الدائن:"
        '
        'Total_B_txt
        '
        Me.Total_B_txt.BackColor = System.Drawing.Color.Lavender
        Me.Total_B_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_B_txt.Enabled = False
        Me.Total_B_txt.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.Total_B_txt.Location = New System.Drawing.Point(93, 2)
        Me.Total_B_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Total_B_txt.MaxLength = 0
        Me.Total_B_txt.Name = "Total_B_txt"
        Me.Total_B_txt.Size = New System.Drawing.Size(140, 26)
        Me.Total_B_txt.TabIndex = 78
        Me.Total_B_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(940, 8)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(47, 14)
        Me.Label12.TabIndex = 72
        Me.Label12.Text = "الصفوف:"
        '
        'Total_C_txt
        '
        Me.Total_C_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Total_C_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_C_txt.Enabled = False
        Me.Total_C_txt.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.Total_C_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.Total_C_txt.Location = New System.Drawing.Point(469, 2)
        Me.Total_C_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Total_C_txt.MaxLength = 0
        Me.Total_C_txt.Name = "Total_C_txt"
        Me.Total_C_txt.Size = New System.Drawing.Size(140, 26)
        Me.Total_C_txt.TabIndex = 77
        Me.Total_C_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Rows_txt
        '
        Me.Rows_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Rows_txt.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Rows_txt.Location = New System.Drawing.Point(893, 4)
        Me.Rows_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Rows_txt.Name = "Rows_txt"
        Me.Rows_txt.ReadOnly = True
        Me.Rows_txt.Size = New System.Drawing.Size(44, 24)
        Me.Rows_txt.TabIndex = 71
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Label14.Location = New System.Drawing.Point(236, 8)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(45, 16)
        Me.Label14.TabIndex = 74
        Me.Label14.Text = "الرصيد:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Label8.Location = New System.Drawing.Point(612, 8)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 16)
        Me.Label8.TabIndex = 70
        Me.Label8.Text = "مدين:"
        '
        'Total_D_txt
        '
        Me.Total_D_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Total_D_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_D_txt.Enabled = False
        Me.Total_D_txt.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.Total_D_txt.ForeColor = System.Drawing.Color.DarkRed
        Me.Total_D_txt.Location = New System.Drawing.Point(289, 2)
        Me.Total_D_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Total_D_txt.MaxLength = 0
        Me.Total_D_txt.Name = "Total_D_txt"
        Me.Total_D_txt.Size = New System.Drawing.Size(140, 26)
        Me.Total_D_txt.TabIndex = 76
        Me.Total_D_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Label11.Location = New System.Drawing.Point(433, 8)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(30, 16)
        Me.Label11.TabIndex = 68
        Me.Label11.Text = "دائن:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
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
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(4, 161)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 40
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(996, 569)
        Me.DataGridView1.TabIndex = 86
        '
        'CircularPanel
        '
        Me.CircularPanel.Controls.Add(Me.CircularProgressControl1)
        Me.CircularPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CircularPanel.Location = New System.Drawing.Point(3, 739)
        Me.CircularPanel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CircularPanel.Name = "CircularPanel"
        Me.CircularPanel.Size = New System.Drawing.Size(998, 58)
        Me.CircularPanel.TabIndex = 87
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
        Me.CircularProgressControl1.Size = New System.Drawing.Size(998, 58)
        Me.CircularProgressControl1.StartAngle = 270
        Me.CircularProgressControl1.TabIndex = 0
        Me.CircularProgressControl1.TickColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        '
        'ACC_MV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 898)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
        Me.Name = "ACC_MV"
        Me.Text = "كشف استاذ"
        Me.Print_CntxtMStrip.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CircularPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TITLE_txt As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Rows_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Total_D_txt As Accounting.F2FloatField_Debit
    Friend WithEvents Total_C_txt As Accounting.F2FloatField_Credit
    Friend WithEvents Total_B_txt As Accounting.F2FloatField_Balance
    Friend WithEvents Search_btn As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents DateRange_Flate1 As DateRange_Flate
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents CircularPanel As Panel
    Friend WithEvents CircularProgressControl1 As CircularProgressControl
    Friend WithEvents TOTAL_C_N As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents TOTAL_D_N As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents ACC_TYPE_Txt As TextBox
    Friend WithEvents Print_Btn As SplitButton
    Friend WithEvents Print_CntxtMStrip As ContextMenuStrip
    Friend WithEvents إستخراجالتقريرExcelToolStripMenuItem As ToolStripMenuItem
End Class
