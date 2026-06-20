<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Balance_sheet
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
        Me.Print_CntxtMStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.إستخراجالتقريرExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Print_Btn = New Accounting.SplitButton()
        Me.CLOSE_B_Btn = New System.Windows.Forms.Button()
        Me.Hide_Zeros_CB = New System.Windows.Forms.CheckBox()
        Me.DateRange_Flate1 = New Accounting.DateRange_Flate()
        Me.Search_btn = New System.Windows.Forms.Button()
        Me.TITLE_txt = New System.Windows.Forms.Label()
        Me.Print_CntxtMStrip.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.CircularPanel.SuspendLayout()
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
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
        Me.TableLayoutPanel1.Controls.Add(Me.CircularPanel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridView, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.91444!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.08556!))
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
        Me.DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.ACC_CODE_CL, Me.ACC_PARENT_CL, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.BALANCE_CL, Me.SIDE_CL, Me.ACC_LEVEL_CL})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView.Location = New System.Drawing.Point(6, 141)
        Me.DataGridView.Margin = New System.Windows.Forms.Padding(6)
        Me.DataGridView.MultiSelect = False
        Me.DataGridView.Name = "DataGridView"
        Me.DataGridView.ReadOnly = True
        Me.DataGridView.RowHeadersVisible = False
        Me.DataGridView.RowTemplate.Height = 30
        Me.DataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView.Size = New System.Drawing.Size(1070, 610)
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
        Me.ACC_CODE_CL.Visible = False
        '
        'ACC_PARENT_CL
        '
        Me.ACC_PARENT_CL.DataPropertyName = "ACC_PARENT"
        Me.ACC_PARENT_CL.FillWeight = 110.7445!
        Me.ACC_PARENT_CL.HeaderText = " تبيعة الحســـاب "
        Me.ACC_PARENT_CL.Name = "ACC_PARENT_CL"
        Me.ACC_PARENT_CL.ReadOnly = True
        Me.ACC_PARENT_CL.Visible = False
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
        'Panel1
        '
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
        Me.Panel1.Size = New System.Drawing.Size(1076, 129)
        Me.Panel1.TabIndex = 0
        '
        'Print_Btn
        '
        Me.Print_Btn.BackColor = System.Drawing.Color.White
        Me.Print_Btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Print_Btn.ButtonImage = Nothing
        Me.Print_Btn.ButtonText = "🖨️  طباعــة"
        Me.Print_Btn.DropDownMenu = Me.Print_CntxtMStrip
        Me.Print_Btn.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Print_Btn.Location = New System.Drawing.Point(765, 84)
        Me.Print_Btn.Name = "Print_Btn"
        Me.Print_Btn.Padding = New System.Windows.Forms.Padding(1)
        Me.Print_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_Btn.Size = New System.Drawing.Size(137, 40)
        Me.Print_Btn.TabIndex = 904
        '
        'CLOSE_B_Btn
        '
        Me.CLOSE_B_Btn.BackColor = System.Drawing.Color.White
        Me.CLOSE_B_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CLOSE_B_Btn.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CLOSE_B_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CLOSE_B_Btn.Location = New System.Drawing.Point(506, 84)
        Me.CLOSE_B_Btn.Name = "CLOSE_B_Btn"
        Me.CLOSE_B_Btn.Size = New System.Drawing.Size(257, 40)
        Me.CLOSE_B_Btn.TabIndex = 903
        Me.CLOSE_B_Btn.Text = "ترحيل الأرصـــدة  ✔️ "
        Me.CLOSE_B_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CLOSE_B_Btn.UseVisualStyleBackColor = False
        '
        'Hide_Zeros_CB
        '
        Me.Hide_Zeros_CB.AutoSize = True
        Me.Hide_Zeros_CB.Checked = True
        Me.Hide_Zeros_CB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Hide_Zeros_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Hide_Zeros_CB.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Hide_Zeros_CB.Location = New System.Drawing.Point(308, 97)
        Me.Hide_Zeros_CB.Name = "Hide_Zeros_CB"
        Me.Hide_Zeros_CB.Size = New System.Drawing.Size(192, 23)
        Me.Hide_Zeros_CB.TabIndex = 106
        Me.Hide_Zeros_CB.Text = "إخفــــــاء الحسابــات الصفريــــة"
        Me.Hide_Zeros_CB.UseVisualStyleBackColor = True
        '
        'DateRange_Flate1
        '
        Me.DateRange_Flate1.AutoSize = True
        Me.DateRange_Flate1.BackColor = System.Drawing.Color.Transparent
        Me.DateRange_Flate1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DateRange_Flate1.Location = New System.Drawing.Point(10, 4)
        Me.DateRange_Flate1.Name = "DateRange_Flate1"
        Me.DateRange_Flate1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DateRange_Flate1.Size = New System.Drawing.Size(495, 79)
        Me.DateRange_Flate1.TabIndex = 105
        '
        'Search_btn
        '
        Me.Search_btn.BackColor = System.Drawing.Color.White
        Me.Search_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Search_btn.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Search_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Search_btn.Location = New System.Drawing.Point(903, 84)
        Me.Search_btn.Margin = New System.Windows.Forms.Padding(4)
        Me.Search_btn.Name = "Search_btn"
        Me.Search_btn.Size = New System.Drawing.Size(169, 40)
        Me.Search_btn.TabIndex = 84
        Me.Search_btn.Text = "🔍  بحـــث"
        Me.Search_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Search_btn.UseVisualStyleBackColor = False
        '
        'TITLE_txt
        '
        Me.TITLE_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TITLE_txt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TITLE_txt.Font = New System.Drawing.Font("Arial", 17, System.Drawing.FontStyle.Bold)
        Me.TITLE_txt.Location = New System.Drawing.Point(506, 4)
        Me.TITLE_txt.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.TITLE_txt.Name = "TITLE_txt"
        Me.TITLE_txt.Size = New System.Drawing.Size(566, 79)
        Me.TITLE_txt.TabIndex = 82
        Me.TITLE_txt.Text = "إعـــداد قائمـــــة المركــز المالـــــي"
        Me.TITLE_txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Balance_sheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1082, 817)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.Name = "Balance_sheet"
        Me.Text = "قائمة الميزانية"
        Me.Print_CntxtMStrip.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.CircularPanel.ResumeLayout(False)
        CType(Me.DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
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
    Friend WithEvents Print_CntxtMStrip As ContextMenuStrip
    Friend WithEvents إستخراجالتقريرExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents ACC_CODE_CL As DataGridViewTextBoxColumn
    Friend WithEvents ACC_PARENT_CL As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents BALANCE_CL As DataGridViewTextBoxColumn
    Friend WithEvents SIDE_CL As DataGridViewTextBoxColumn
    Friend WithEvents ACC_LEVEL_CL As DataGridViewTextBoxColumn
End Class
