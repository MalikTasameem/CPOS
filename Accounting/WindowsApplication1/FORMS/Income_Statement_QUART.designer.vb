<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Income_Statement_QUART
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Income_Statement_QUART))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.CircularPanel = New System.Windows.Forms.Panel()
        Me.CircularProgressControl1 = New Accounting.CircularProgressControl()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ACC_CODE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ACC_NAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ACC_NAME_1_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ACC_NAME_2_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CHANGE_1_2_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BALANCE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FOURTH_QUART_INCOME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CHANGE_3_4_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Print_Btn = New System.Windows.Forms.Button()
        Me.B_Name_Cm = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Search_btn = New System.Windows.Forms.Button()
        Me.TITLE_txt = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.CircularPanel.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.73583!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.26418!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1082, 829)
        Me.TableLayoutPanel1.TabIndex = 87
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.CircularPanel)
        Me.Panel2.Controls.Add(Me.DataGridView1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 92)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1076, 734)
        Me.Panel2.TabIndex = 1
        '
        'CircularPanel
        '
        Me.CircularPanel.Controls.Add(Me.CircularProgressControl1)
        Me.CircularPanel.Location = New System.Drawing.Point(3, 675)
        Me.CircularPanel.Name = "CircularPanel"
        Me.CircularPanel.Size = New System.Drawing.Size(1070, 56)
        Me.CircularPanel.TabIndex = 89
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
        Me.CircularProgressControl1.Size = New System.Drawing.Size(1070, 56)
        Me.CircularProgressControl1.StartAngle = 270
        Me.CircularProgressControl1.TabIndex = 88
        Me.CircularProgressControl1.TickColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.ACC_CODE_CL, Me.ACC_NAME_CL, Me.ACC_NAME_1_CL, Me.ACC_NAME_2_CL, Me.CHANGE_1_2_CL, Me.BALANCE_CL, Me.FOURTH_QUART_INCOME_CL, Me.CHANGE_3_4_CL})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(6)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 30
        Me.DataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView1.Size = New System.Drawing.Size(1076, 734)
        Me.DataGridView1.TabIndex = 86
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.FillWeight = 35.53299!
        Me.T_ID_CL.HeaderText = "ت"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        '
        'ACC_CODE_CL
        '
        Me.ACC_CODE_CL.DataPropertyName = "ACC_CODE"
        Me.ACC_CODE_CL.FillWeight = 110.7445!
        Me.ACC_CODE_CL.HeaderText = " كــود الحســـاب "
        Me.ACC_CODE_CL.Name = "ACC_CODE_CL"
        Me.ACC_CODE_CL.ReadOnly = True
        '
        'ACC_NAME_CL
        '
        Me.ACC_NAME_CL.DataPropertyName = "ACC_NAME"
        Me.ACC_NAME_CL.FillWeight = 110.7445!
        Me.ACC_NAME_CL.HeaderText = " إسم الحساب "
        Me.ACC_NAME_CL.Name = "ACC_NAME_CL"
        Me.ACC_NAME_CL.ReadOnly = True
        '
        'ACC_NAME_1_CL
        '
        Me.ACC_NAME_1_CL.DataPropertyName = "FIRST_QUART_INCOME"
        DataGridViewCellStyle1.Format = "N3"
        Me.ACC_NAME_1_CL.DefaultCellStyle = DataGridViewCellStyle1
        Me.ACC_NAME_1_CL.FillWeight = 110.7445!
        Me.ACC_NAME_1_CL.HeaderText = "الربــع الأول"
        Me.ACC_NAME_1_CL.Name = "ACC_NAME_1_CL"
        Me.ACC_NAME_1_CL.ReadOnly = True
        '
        'ACC_NAME_2_CL
        '
        Me.ACC_NAME_2_CL.DataPropertyName = "SECOND_QUART_INCOME"
        DataGridViewCellStyle2.Format = "N3"
        Me.ACC_NAME_2_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.ACC_NAME_2_CL.FillWeight = 110.7445!
        Me.ACC_NAME_2_CL.HeaderText = "الربــع الثاني"
        Me.ACC_NAME_2_CL.Name = "ACC_NAME_2_CL"
        Me.ACC_NAME_2_CL.ReadOnly = True
        '
        'CHANGE_1_2_CL
        '
        Me.CHANGE_1_2_CL.DataPropertyName = "FIRST_QUART_CHANGE_PERCENT_BETWEEN_FIRST_AND_SECOND"
        Me.CHANGE_1_2_CL.FillWeight = 110.7445!
        Me.CHANGE_1_2_CL.HeaderText = "نسبة التغيــر"
        Me.CHANGE_1_2_CL.Name = "CHANGE_1_2_CL"
        Me.CHANGE_1_2_CL.ReadOnly = True
        '
        'BALANCE_CL
        '
        Me.BALANCE_CL.DataPropertyName = "THIRD_QUART_INCOME"
        DataGridViewCellStyle3.Format = "N3"
        Me.BALANCE_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.BALANCE_CL.FillWeight = 110.7445!
        Me.BALANCE_CL.HeaderText = "الربــع الثالــث"
        Me.BALANCE_CL.Name = "BALANCE_CL"
        Me.BALANCE_CL.ReadOnly = True
        '
        'FOURTH_QUART_INCOME_CL
        '
        Me.FOURTH_QUART_INCOME_CL.DataPropertyName = "FOURTH_QUART_INCOME"
        DataGridViewCellStyle4.Format = "N3"
        Me.FOURTH_QUART_INCOME_CL.DefaultCellStyle = DataGridViewCellStyle4
        Me.FOURTH_QUART_INCOME_CL.HeaderText = "الربــع الرابــع"
        Me.FOURTH_QUART_INCOME_CL.Name = "FOURTH_QUART_INCOME_CL"
        Me.FOURTH_QUART_INCOME_CL.ReadOnly = True
        '
        'CHANGE_3_4_CL
        '
        Me.CHANGE_3_4_CL.DataPropertyName = "FIRST_QUART_CHANGE_PERCENT_BETWEEN_THIRD_AND_FOURTH"
        Me.CHANGE_3_4_CL.HeaderText = "نسبة التغيــر"
        Me.CHANGE_3_4_CL.Name = "CHANGE_3_4_CL"
        Me.CHANGE_3_4_CL.ReadOnly = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Print_Btn)
        Me.Panel1.Controls.Add(Me.B_Name_Cm)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Search_btn)
        Me.Panel1.Controls.Add(Me.TITLE_txt)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1076, 83)
        Me.Panel1.TabIndex = 0
        '
        'Print_Btn
        '
        Me.Print_Btn.BackColor = System.Drawing.Color.White
        Me.Print_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Print_Btn.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Print_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_Btn.Location = New System.Drawing.Point(9, 34)
        Me.Print_Btn.Name = "Print_Btn"
        Me.Print_Btn.Size = New System.Drawing.Size(276, 44)
        Me.Print_Btn.TabIndex = 103
        Me.Print_Btn.Text = "🖨️  طباعــة"
        Me.Print_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Print_Btn.UseVisualStyleBackColor = False
        '
        'B_Name_Cm
        '
        Me.B_Name_Cm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.B_Name_Cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.B_Name_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.B_Name_Cm.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.B_Name_Cm.FormattingEnabled = True
        Me.B_Name_Cm.Location = New System.Drawing.Point(286, 4)
        Me.B_Name_Cm.Margin = New System.Windows.Forms.Padding(4)
        Me.B_Name_Cm.Name = "B_Name_Cm"
        Me.B_Name_Cm.Size = New System.Drawing.Size(143, 27)
        Me.B_Name_Cm.TabIndex = 85
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.75!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(433, 8)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 19)
        Me.Label4.TabIndex = 86
        Me.Label4.Text = "السنــة:"
        '
        'Search_btn
        '
        Me.Search_btn.BackColor = System.Drawing.Color.White
        Me.Search_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Search_btn.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Search_btn.Image = CType(resources.GetObject("Search_btn.Image"), System.Drawing.Image)
        Me.Search_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Search_btn.Location = New System.Drawing.Point(286, 34)
        Me.Search_btn.Margin = New System.Windows.Forms.Padding(4)
        Me.Search_btn.Name = "Search_btn"
        Me.Search_btn.Size = New System.Drawing.Size(204, 44)
        Me.Search_btn.TabIndex = 84
        Me.Search_btn.Text = "بحـــث"
        Me.Search_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Search_btn.UseVisualStyleBackColor = False
        '
        'TITLE_txt
        '
        Me.TITLE_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TITLE_txt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TITLE_txt.Font = New System.Drawing.Font("Arial", 17.0!, System.Drawing.FontStyle.Bold)
        Me.TITLE_txt.Location = New System.Drawing.Point(518, 6)
        Me.TITLE_txt.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.TITLE_txt.Name = "TITLE_txt"
        Me.TITLE_txt.Size = New System.Drawing.Size(555, 71)
        Me.TITLE_txt.TabIndex = 82
        Me.TITLE_txt.Text = "إعـــداد قائمـــــة الدخــــل : ربع سنـــة"
        Me.TITLE_txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Income_Statement_QUART
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1082, 829)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.Name = "Income_Statement_QUART"
        Me.Text = "قائمة الميزانية"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.CircularPanel.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Search_btn As Button
    Friend WithEvents TITLE_txt As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents B_Name_Cm As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents T_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents ACC_CODE_CL As DataGridViewTextBoxColumn
    Friend WithEvents ACC_NAME_CL As DataGridViewTextBoxColumn
    Friend WithEvents ACC_NAME_1_CL As DataGridViewTextBoxColumn
    Friend WithEvents ACC_NAME_2_CL As DataGridViewTextBoxColumn
    Friend WithEvents CHANGE_1_2_CL As DataGridViewTextBoxColumn
    Friend WithEvents BALANCE_CL As DataGridViewTextBoxColumn
    Friend WithEvents FOURTH_QUART_INCOME_CL As DataGridViewTextBoxColumn
    Friend WithEvents CHANGE_3_4_CL As DataGridViewTextBoxColumn
    Friend WithEvents Print_Btn As Button
    Friend WithEvents CircularProgressControl1 As CircularProgressControl
    Friend WithEvents CircularPanel As Panel
End Class
