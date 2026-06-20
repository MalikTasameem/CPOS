<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Daily_B_Form
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Balanced_Cm = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Depended_Cm = New System.Windows.Forms.ComboBox()
        Me.DateRange_Flate1 = New Accounting.DateRange_Flate()
        Me.TITLE_txt = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.B_TYPE_CM = New System.Windows.Forms.ComboBox()
        Me.Print_Btn = New System.Windows.Forms.Button()
        Me.RefreshBtn = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Total_B_txt = New Accounting.F2FloatField_Balance()
        Me.Total_C_txt = New Accounting.F2FloatField_Credit()
        Me.Total_D_txt = New Accounting.F2FloatField_Debit()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Rows_txt = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.DataB = New System.Windows.Forms.BindingSource(Me.components)
        Me.CircularPanel = New System.Windows.Forms.Panel()
        Me.CircularProgressControl1 = New Accounting.CircularProgressControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.FilterColumn_Cm = New System.Windows.Forms.ComboBox()
        Me.FilterColumn_Label = New System.Windows.Forms.Label()
        Me.Filter_Txt = New System.Windows.Forms.TextBox()
        Me.Filter_Label = New System.Windows.Forms.Label()
        Me.PrintMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CircularPanel.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(137, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 16)
        Me.Label3.TabIndex = 111
        Me.Label3.Text = "التوازن :"
        '
        'Balanced_Cm
        '
        Me.Balanced_Cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Balanced_Cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Balanced_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Balanced_Cm.Font = New System.Drawing.Font("Arial", 10.25!, System.Drawing.FontStyle.Bold)
        Me.Balanced_Cm.FormattingEnabled = True
        Me.Balanced_Cm.Items.AddRange(New Object() {"الكل", "الموزون فقط", "الغيـــر موزون فقط"})
        Me.Balanced_Cm.Location = New System.Drawing.Point(3, 7)
        Me.Balanced_Cm.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Balanced_Cm.Name = "Balanced_Cm"
        Me.Balanced_Cm.Size = New System.Drawing.Size(130, 24)
        Me.Balanced_Cm.TabIndex = 110
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(316, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 16)
        Me.Label2.TabIndex = 109
        Me.Label2.Text = "الإعتماد :"
        '
        'Depended_Cm
        '
        Me.Depended_Cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Depended_Cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Depended_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Depended_Cm.Font = New System.Drawing.Font("Arial", 10.25!, System.Drawing.FontStyle.Bold)
        Me.Depended_Cm.FormattingEnabled = True
        Me.Depended_Cm.Items.AddRange(New Object() {"الكل", "المعتمد", "الغيــر معتمد"})
        Me.Depended_Cm.Location = New System.Drawing.Point(199, 7)
        Me.Depended_Cm.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Depended_Cm.Name = "Depended_Cm"
        Me.Depended_Cm.Size = New System.Drawing.Size(115, 24)
        Me.Depended_Cm.TabIndex = 108
        '
        'DateRange_Flate1
        '
        Me.DateRange_Flate1.AutoSize = True
        Me.DateRange_Flate1.BackColor = System.Drawing.Color.Transparent
        Me.DateRange_Flate1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DateRange_Flate1.Font = New System.Drawing.Font("Tahoma", 8.5!)
        Me.DateRange_Flate1.Location = New System.Drawing.Point(4, 1)
        Me.DateRange_Flate1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DateRange_Flate1.Name = "DateRange_Flate1"
        Me.DateRange_Flate1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DateRange_Flate1.Size = New System.Drawing.Size(535, 91)
        Me.DateRange_Flate1.TabIndex = 107
        '
        'TITLE_txt
        '
        Me.TITLE_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TITLE_txt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TITLE_txt.Font = New System.Drawing.Font("Arial", 17.0!, System.Drawing.FontStyle.Bold)
        Me.TITLE_txt.Location = New System.Drawing.Point(540, 1)
        Me.TITLE_txt.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.TITLE_txt.Name = "TITLE_txt"
        Me.TITLE_txt.Size = New System.Drawing.Size(354, 91)
        Me.TITLE_txt.TabIndex = 106
        Me.TITLE_txt.Text = "قائمـــــة القيـــود اليوميـــة"
        Me.TITLE_txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(780, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 16)
        Me.Label1.TabIndex = 105
        Me.Label1.Text = "نوع العرض :"
        '
        'B_TYPE_CM
        '
        Me.B_TYPE_CM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B_TYPE_CM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.B_TYPE_CM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.B_TYPE_CM.Font = New System.Drawing.Font("Arial", 10.25!, System.Drawing.FontStyle.Bold)
        Me.B_TYPE_CM.FormattingEnabled = True
        Me.B_TYPE_CM.Items.AddRange(New Object() {"الرئيسي", "التفاصيل"})
        Me.B_TYPE_CM.Location = New System.Drawing.Point(676, 11)
        Me.B_TYPE_CM.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.B_TYPE_CM.Name = "B_TYPE_CM"
        Me.B_TYPE_CM.Size = New System.Drawing.Size(98, 24)
        Me.B_TYPE_CM.TabIndex = 104
        '
        'Print_Btn
        '
        Me.Print_Btn.BackColor = System.Drawing.Color.White
        Me.Print_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Print_Btn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Print_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_Btn.Location = New System.Drawing.Point(3, 2)
        Me.Print_Btn.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Print_Btn.Name = "Print_Btn"
        Me.Print_Btn.Size = New System.Drawing.Size(149, 48)
        Me.Print_Btn.TabIndex = 103
        Me.Print_Btn.Text = "🖨️  طباعــة"
        Me.Print_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Print_Btn.UseVisualStyleBackColor = False
        '
        'RefreshBtn
        '
        Me.RefreshBtn.BackColor = System.Drawing.Color.White
        Me.RefreshBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RefreshBtn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RefreshBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RefreshBtn.Location = New System.Drawing.Point(153, 2)
        Me.RefreshBtn.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.RefreshBtn.Name = "RefreshBtn"
        Me.RefreshBtn.Size = New System.Drawing.Size(149, 48)
        Me.RefreshBtn.TabIndex = 101
        Me.RefreshBtn.Text = "🔄 تحديث"
        Me.RefreshBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RefreshBtn.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(2, 791)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(892, 40)
        Me.Button1.TabIndex = 89
        Me.Button1.Text = "عـــودة   ↩️"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Total_B_txt
        '
        Me.Total_B_txt.BackColor = System.Drawing.Color.Lavender
        Me.Total_B_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_B_txt.Enabled = False
        Me.Total_B_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Total_B_txt.Location = New System.Drawing.Point(2, 755)
        Me.Total_B_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Total_B_txt.MaxLength = 0
        Me.Total_B_txt.Name = "Total_B_txt"
        Me.Total_B_txt.Size = New System.Drawing.Size(160, 29)
        Me.Total_B_txt.TabIndex = 88
        Me.Total_B_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Total_C_txt
        '
        Me.Total_C_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Total_C_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_C_txt.Enabled = False
        Me.Total_C_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Total_C_txt.ForeColor = System.Drawing.Color.DarkGreen
        Me.Total_C_txt.Location = New System.Drawing.Point(433, 754)
        Me.Total_C_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Total_C_txt.MaxLength = 0
        Me.Total_C_txt.Name = "Total_C_txt"
        Me.Total_C_txt.Size = New System.Drawing.Size(160, 29)
        Me.Total_C_txt.TabIndex = 87
        Me.Total_C_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Total_D_txt
        '
        Me.Total_D_txt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Total_D_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Total_D_txt.Enabled = False
        Me.Total_D_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Total_D_txt.ForeColor = System.Drawing.Color.DarkRed
        Me.Total_D_txt.Location = New System.Drawing.Point(225, 754)
        Me.Total_D_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Total_D_txt.MaxLength = 0
        Me.Total_D_txt.Name = "Total_D_txt"
        Me.Total_D_txt.Size = New System.Drawing.Size(160, 29)
        Me.Total_D_txt.TabIndex = 86
        Me.Total_D_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Label14.Location = New System.Drawing.Point(165, 762)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(45, 16)
        Me.Label14.TabIndex = 83
        Me.Label14.Text = "الرصيد:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Label12.Location = New System.Drawing.Point(712, 762)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(51, 16)
        Me.Label12.TabIndex = 81
        Me.Label12.Text = "الصفوف:"
        '
        'Rows_txt
        '
        Me.Rows_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Rows_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Rows_txt.Location = New System.Drawing.Point(644, 755)
        Me.Rows_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Rows_txt.Name = "Rows_txt"
        Me.Rows_txt.ReadOnly = True
        Me.Rows_txt.Size = New System.Drawing.Size(65, 29)
        Me.Rows_txt.TabIndex = 80
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Label8.Location = New System.Drawing.Point(596, 762)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 16)
        Me.Label8.TabIndex = 79
        Me.Label8.Text = "مدين:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Label11.Location = New System.Drawing.Point(388, 761)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(30, 16)
        Me.Label11.TabIndex = 77
        Me.Label11.Text = "دائن:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.Location = New System.Drawing.Point(2, 183)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DataGridView1.RowTemplate.Height = 35
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView1.Size = New System.Drawing.Size(892, 570)
        Me.DataGridView1.TabIndex = 75
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Balanced_Cm)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Depended_Cm)
        Me.Panel1.Location = New System.Drawing.Point(303, 2)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(369, 48)
        Me.Panel1.TabIndex = 112
        '
        'CircularPanel
        '
        Me.CircularPanel.BackColor = System.Drawing.Color.Transparent
        Me.CircularPanel.Controls.Add(Me.CircularProgressControl1)
        Me.CircularPanel.Location = New System.Drawing.Point(3, 434)
        Me.CircularPanel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.CircularPanel.Name = "CircularPanel"
        Me.CircularPanel.Size = New System.Drawing.Size(891, 58)
        Me.CircularPanel.TabIndex = 900
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
        Me.CircularProgressControl1.Size = New System.Drawing.Size(891, 58)
        Me.CircularProgressControl1.StartAngle = 270
        Me.CircularProgressControl1.TabIndex = 87
        Me.CircularProgressControl1.TickColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.FilterColumn_Cm)
        Me.Panel2.Controls.Add(Me.FilterColumn_Label)
        Me.Panel2.Controls.Add(Me.Filter_Txt)
        Me.Panel2.Controls.Add(Me.Filter_Label)
        Me.Panel2.Controls.Add(Me.Print_Btn)
        Me.Panel2.Controls.Add(Me.RefreshBtn)
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.B_TYPE_CM)
        Me.Panel2.Location = New System.Drawing.Point(4, 94)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(890, 86)
        Me.Panel2.TabIndex = 901
        '
        'FilterColumn_Cm
        '
        Me.FilterColumn_Cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.FilterColumn_Cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.FilterColumn_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.FilterColumn_Cm.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold)
        Me.FilterColumn_Cm.FormattingEnabled = True
        Me.FilterColumn_Cm.Location = New System.Drawing.Point(210, 56)
        Me.FilterColumn_Cm.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FilterColumn_Cm.Name = "FilterColumn_Cm"
        Me.FilterColumn_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.FilterColumn_Cm.Size = New System.Drawing.Size(191, 27)
        Me.FilterColumn_Cm.TabIndex = 116
        '
        'FilterColumn_Label
        '
        Me.FilterColumn_Label.AutoSize = True
        Me.FilterColumn_Label.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.FilterColumn_Label.Location = New System.Drawing.Point(407, 61)
        Me.FilterColumn_Label.Name = "FilterColumn_Label"
        Me.FilterColumn_Label.Size = New System.Drawing.Size(41, 16)
        Me.FilterColumn_Label.TabIndex = 115
        Me.FilterColumn_Label.Text = "العمود:"
        '
        'Filter_Txt
        '
        Me.Filter_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Filter_Txt.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold)
        Me.Filter_Txt.Location = New System.Drawing.Point(466, 56)
        Me.Filter_Txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Filter_Txt.Name = "Filter_Txt"
        Me.Filter_Txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Filter_Txt.Size = New System.Drawing.Size(348, 26)
        Me.Filter_Txt.TabIndex = 114
        '
        'Filter_Label
        '
        Me.Filter_Label.AutoSize = True
        Me.Filter_Label.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Filter_Label.Location = New System.Drawing.Point(820, 61)
        Me.Filter_Label.Name = "Filter_Label"
        Me.Filter_Label.Size = New System.Drawing.Size(32, 16)
        Me.Filter_Label.TabIndex = 113
        Me.Filter_Label.Text = "بحث:"
        '
        'PrintMenu
        '
        Me.PrintMenu.Name = "PrintMenu"
        Me.PrintMenu.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PrintMenu.Size = New System.Drawing.Size(61, 4)
        '
        'Daily_B_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(897, 831)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.CircularPanel)
        Me.Controls.Add(Me.DateRange_Flate1)
        Me.Controls.Add(Me.TITLE_txt)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Total_B_txt)
        Me.Controls.Add(Me.Total_C_txt)
        Me.Controls.Add(Me.Total_D_txt)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Rows_txt)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.DataGridView1)
        Me.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Name = "Daily_B_Form"
        Me.Text = "القيـــود اليوميـــة-الرئيسية"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CircularPanel.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Rows_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Total_B_txt As Accounting.F2FloatField_Balance
    Friend WithEvents Total_C_txt As Accounting.F2FloatField_Credit
    Friend WithEvents Total_D_txt As Accounting.F2FloatField_Debit
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents RefreshBtn As Button
    Friend WithEvents Print_Btn As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents B_TYPE_CM As ComboBox
    Friend WithEvents TITLE_txt As Label
    Friend WithEvents DateRange_Flate1 As DateRange_Flate
    Friend WithEvents Label2 As Label
    Friend WithEvents Depended_Cm As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Balanced_Cm As ComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents DataB As BindingSource
    Friend WithEvents CircularPanel As Panel
    Friend WithEvents CircularProgressControl1 As CircularProgressControl
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Filter_Label As Label
    Friend WithEvents Filter_Txt As TextBox
    Friend WithEvents FilterColumn_Label As Label
    Friend WithEvents FilterColumn_Cm As ComboBox
    Friend WithEvents PrintMenu As ContextMenuStrip
End Class
