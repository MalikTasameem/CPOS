<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SearchAgent_Pch_Bill
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SearchAgent_Pch_Bill))
        Me.MetroToolTip1 = New MetroFramework.Components.MetroToolTip()
        Me.isDeletedCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Bill_cmb = New System.Windows.Forms.ComboBox()
        Me.Marketer_Lb = New System.Windows.Forms.Label()
        Me.advancedDataGridView_main = New Zuby.ADGV.AdvancedDataGridView()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.TITLE_TXT = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ALL_time_CheckBox = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.advancedDataGridViewSearchToolBar_main = New Zuby.ADGV.AdvancedDataGridViewSearchToolBar()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.textBox_total = New System.Windows.Forms.TextBox()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Print_btn = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.IM_Serach_btn = New System.Windows.Forms.Button()
        Me.bindingSource_main = New System.Windows.Forms.BindingSource(Me.components)
        Me.RPT_CM = New System.Windows.Forms.ComboBox()
        Me.DateRange_Flate = New resturant.DateRange_Flate()
        Me.AG_Cm = New resturant.FSearch_Filter()
        Me.Markter_Cm = New resturant.FSearch_Filter()
        CType(Me.advancedDataGridView_main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.bindingSource_main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MetroToolTip1
        '
        Me.MetroToolTip1.Style = MetroFramework.MetroColorStyle.Blue
        Me.MetroToolTip1.StyleManager = Nothing
        Me.MetroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'isDeletedCheckBox
        '
        Me.isDeletedCheckBox.AutoSize = True
        Me.isDeletedCheckBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.isDeletedCheckBox.Font = New System.Drawing.Font("Arial", 14.25!)
        Me.isDeletedCheckBox.Location = New System.Drawing.Point(360, 5)
        Me.isDeletedCheckBox.Name = "isDeletedCheckBox"
        Me.isDeletedCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.isDeletedCheckBox.Size = New System.Drawing.Size(85, 26)
        Me.isDeletedCheckBox.TabIndex = 666
        Me.isDeletedCheckBox.Text = "ملغية فقط"
        Me.isDeletedCheckBox.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 14.25!)
        Me.Label4.Location = New System.Drawing.Point(940, 8)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label4.Size = New System.Drawing.Size(58, 22)
        Me.Label4.TabIndex = 667
        Me.Label4.Text = "المورد :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Arial", 14.25!)
        Me.Label32.Location = New System.Drawing.Point(550, 6)
        Me.Label32.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(51, 22)
        Me.Label32.TabIndex = 703
        Me.Label32.Text = "النوع :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Bill_cmb
        '
        Me.Bill_cmb.BackColor = System.Drawing.Color.White
        Me.Bill_cmb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Bill_cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Bill_cmb.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Bill_cmb.Font = New System.Drawing.Font("Arial", 13.0!)
        Me.Bill_cmb.ForeColor = System.Drawing.Color.DarkBlue
        Me.Bill_cmb.FormattingEnabled = True
        Me.Bill_cmb.Items.AddRange(New Object() {"كل الفواتير", "الخالصة فقط", "الغير خالصة فقط"})
        Me.Bill_cmb.Location = New System.Drawing.Point(452, 5)
        Me.Bill_cmb.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.Bill_cmb.MaxDropDownItems = 15
        Me.Bill_cmb.Name = "Bill_cmb"
        Me.Bill_cmb.Size = New System.Drawing.Size(96, 27)
        Me.Bill_cmb.TabIndex = 702
        '
        'Marketer_Lb
        '
        Me.Marketer_Lb.AutoSize = True
        Me.Marketer_Lb.BackColor = System.Drawing.Color.Transparent
        Me.Marketer_Lb.Font = New System.Drawing.Font("Arial", 14.25!)
        Me.Marketer_Lb.Location = New System.Drawing.Point(938, 41)
        Me.Marketer_Lb.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Marketer_Lb.Name = "Marketer_Lb"
        Me.Marketer_Lb.Size = New System.Drawing.Size(64, 22)
        Me.Marketer_Lb.TabIndex = 705
        Me.Marketer_Lb.Text = "المسوق :"
        Me.Marketer_Lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'advancedDataGridView_main
        '
        Me.advancedDataGridView_main.AllowUserToAddRows = False
        Me.advancedDataGridView_main.AllowUserToDeleteRows = False
        Me.advancedDataGridView_main.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.advancedDataGridView_main.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 10.25!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.advancedDataGridView_main.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.advancedDataGridView_main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.advancedDataGridView_main.FilterAndSortEnabled = True
        Me.advancedDataGridView_main.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.advancedDataGridView_main.Location = New System.Drawing.Point(2, 132)
        Me.advancedDataGridView_main.Margin = New System.Windows.Forms.Padding(1)
        Me.advancedDataGridView_main.MultiSelect = False
        Me.advancedDataGridView_main.Name = "advancedDataGridView_main"
        Me.advancedDataGridView_main.ReadOnly = True
        Me.advancedDataGridView_main.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 10.25!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.advancedDataGridView_main.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.advancedDataGridView_main.RowTemplate.Height = 30
        Me.advancedDataGridView_main.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.advancedDataGridView_main.Size = New System.Drawing.Size(1002, 460)
        Me.advancedDataGridView_main.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.advancedDataGridView_main.TabIndex = 903
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.Font = New System.Drawing.Font("Arial", 9.25!, System.Drawing.FontStyle.Bold)
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.HorizontalScrollbar = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(207, 27)
        Me.CheckedListBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckedListBox1.MultiColumn = True
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CheckedListBox1.Size = New System.Drawing.Size(459, 72)
        Me.CheckedListBox1.TabIndex = 904
        '
        'TITLE_TXT
        '
        Me.TITLE_TXT.Location = New System.Drawing.Point(207, 3)
        Me.TITLE_TXT.Margin = New System.Windows.Forms.Padding(2)
        Me.TITLE_TXT.Name = "TITLE_TXT"
        Me.TITLE_TXT.Size = New System.Drawing.Size(459, 23)
        Me.TITLE_TXT.TabIndex = 905
        Me.TITLE_TXT.Text = "تقرير فواتير الموردون"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 14.25!)
        Me.Label2.Location = New System.Drawing.Point(670, 5)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 22)
        Me.Label2.TabIndex = 906
        Me.Label2.Text = "عنوان التقرير"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ALL_time_CheckBox
        '
        Me.ALL_time_CheckBox.AutoSize = True
        Me.ALL_time_CheckBox.Checked = True
        Me.ALL_time_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ALL_time_CheckBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ALL_time_CheckBox.Font = New System.Drawing.Font("Arial", 13.25!)
        Me.ALL_time_CheckBox.Location = New System.Drawing.Point(538, 3)
        Me.ALL_time_CheckBox.Name = "ALL_time_CheckBox"
        Me.ALL_time_CheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ALL_time_CheckBox.Size = New System.Drawing.Size(93, 25)
        Me.ALL_time_CheckBox.TabIndex = 908
        Me.ALL_time_CheckBox.Text = "كل الفترات"
        Me.ALL_time_CheckBox.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.DateRange_Flate)
        Me.Panel2.Controls.Add(Me.ALL_time_CheckBox)
        Me.Panel2.Location = New System.Drawing.Point(3, 37)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Panel2.Size = New System.Drawing.Size(640, 47)
        Me.Panel2.TabIndex = 909
        '
        'advancedDataGridViewSearchToolBar_main
        '
        Me.advancedDataGridViewSearchToolBar_main.AllowMerge = False
        Me.advancedDataGridViewSearchToolBar_main.AutoSize = False
        Me.advancedDataGridViewSearchToolBar_main.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.advancedDataGridViewSearchToolBar_main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.advancedDataGridViewSearchToolBar_main.Location = New System.Drawing.Point(0, 25)
        Me.advancedDataGridViewSearchToolBar_main.MaximumSize = New System.Drawing.Size(0, 20)
        Me.advancedDataGridViewSearchToolBar_main.MinimumSize = New System.Drawing.Size(0, 20)
        Me.advancedDataGridViewSearchToolBar_main.Name = "advancedDataGridViewSearchToolBar_main"
        Me.advancedDataGridViewSearchToolBar_main.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.advancedDataGridViewSearchToolBar_main.Size = New System.Drawing.Size(0, 20)
        Me.advancedDataGridViewSearchToolBar_main.TabIndex = 909
        Me.advancedDataGridViewSearchToolBar_main.Text = "AdvancedDataGridViewSearchToolBar1"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.textBox_total)
        Me.Panel1.Controls.Add(Me.ExitFormButton)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.TITLE_TXT)
        Me.Panel1.Controls.Add(Me.CheckedListBox1)
        Me.Panel1.Controls.Add(Me.Print_btn)
        Me.Panel1.Location = New System.Drawing.Point(2, 595)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1002, 99)
        Me.Panel1.TabIndex = 910
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.Label3.Location = New System.Drawing.Point(78, 6)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(109, 17)
        Me.Label3.TabIndex = 908
        Me.Label3.Text = "الصفوف المستعرضة"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'textBox_total
        '
        Me.textBox_total.Location = New System.Drawing.Point(2, 2)
        Me.textBox_total.Margin = New System.Windows.Forms.Padding(2)
        Me.textBox_total.Name = "textBox_total"
        Me.textBox_total.ReadOnly = True
        Me.textBox_total.Size = New System.Drawing.Size(72, 23)
        Me.textBox_total.TabIndex = 907
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Arial", 14.25!)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(906, 61)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(93, 35)
        Me.ExitFormButton.TabIndex = 665
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Print_btn
        '
        Me.Print_btn.BackColor = System.Drawing.Color.White
        Me.Print_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_btn.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Print_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Print_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Print_btn.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Print_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Print_btn.Image = Global.resturant.My.Resources.Resources.output_onlinepngtools__2_
        Me.Print_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_btn.Location = New System.Drawing.Point(7, 52)
        Me.Print_btn.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.Print_btn.Name = "Print_btn"
        Me.Print_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_btn.Size = New System.Drawing.Size(161, 41)
        Me.Print_btn.TabIndex = 704
        Me.Print_btn.TabStop = False
        Me.Print_btn.Text = "طباعة"
        Me.Print_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Print_btn.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.advancedDataGridViewSearchToolBar_main)
        Me.Panel3.Location = New System.Drawing.Point(2, 84)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(675, 45)
        Me.Panel3.TabIndex = 911
        '
        'IM_Serach_btn
        '
        Me.IM_Serach_btn.BackColor = System.Drawing.Color.White
        Me.IM_Serach_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.IM_Serach_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_Serach_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_Serach_btn.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Serach_btn.ForeColor = System.Drawing.Color.Black
        Me.IM_Serach_btn.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.IM_Serach_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IM_Serach_btn.Location = New System.Drawing.Point(4, 2)
        Me.IM_Serach_btn.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.IM_Serach_btn.Name = "IM_Serach_btn"
        Me.IM_Serach_btn.Size = New System.Drawing.Size(179, 33)
        Me.IM_Serach_btn.TabIndex = 707
        Me.IM_Serach_btn.Text = "بحث"
        Me.IM_Serach_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.IM_Serach_btn.UseVisualStyleBackColor = False
        '
        'bindingSource_main
        '
        '
        'RPT_CM
        '
        Me.RPT_CM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.RPT_CM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.RPT_CM.BackColor = System.Drawing.SystemColors.Info
        Me.RPT_CM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RPT_CM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RPT_CM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RPT_CM.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RPT_CM.FormattingEnabled = True
        Me.RPT_CM.IntegralHeight = False
        Me.RPT_CM.Items.AddRange(New Object() {"عرض تفصيل الفواتير", "عرض إحصائيـــات"})
        Me.RPT_CM.Location = New System.Drawing.Point(681, 101)
        Me.RPT_CM.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.RPT_CM.Name = "RPT_CM"
        Me.RPT_CM.Size = New System.Drawing.Size(323, 30)
        Me.RPT_CM.TabIndex = 914
        '
        'DateRange_Flate
        '
        Me.DateRange_Flate.AutoSize = True
        Me.DateRange_Flate.BackColor = System.Drawing.Color.White
        Me.DateRange_Flate.Location = New System.Drawing.Point(2, 2)
        Me.DateRange_Flate.Margin = New System.Windows.Forms.Padding(2)
        Me.DateRange_Flate.Name = "DateRange_Flate"
        Me.DateRange_Flate.Size = New System.Drawing.Size(531, 41)
        Me.DateRange_Flate.TabIndex = 0
        '
        'AG_Cm
        '
        Me.AG_Cm.CancelSearchImage = CType(resources.GetObject("AG_Cm.CancelSearchImage"), System.Drawing.Image)
        Me.AG_Cm.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AG_Cm.Location = New System.Drawing.Point(604, 1)
        Me.AG_Cm.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.AG_Cm.Name = "AG_Cm"
        Me.AG_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AG_Cm.Size = New System.Drawing.Size(331, 34)
        Me.AG_Cm.SQL_Column = "Ag_name"
        Me.AG_Cm.SQL_ID = "AG_ID"
        Me.AG_Cm.SQL_IsNumericSearchField = False
        Me.AG_Cm.SQL_ListSize = 200
        Me.AG_Cm.SQL_NumberOfRows = 200
        Me.AG_Cm.SQL_OrderByField = "AG_NAME"
        Me.AG_Cm.SQL_SearchField = "AG_NAME"
        Me.AG_Cm.SQL_Table = "AGENTS_MENU_V"
        Me.AG_Cm.TabIndex = 708
        Me.AG_Cm.TextMaxLength = 250
        Me.AG_Cm.Textt = ""
        '
        'Markter_Cm
        '
        Me.Markter_Cm.CancelSearchImage = CType(resources.GetObject("Markter_Cm.CancelSearchImage"), System.Drawing.Image)
        Me.Markter_Cm.Location = New System.Drawing.Point(648, 37)
        Me.Markter_Cm.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.Markter_Cm.Name = "Markter_Cm"
        Me.Markter_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Markter_Cm.Size = New System.Drawing.Size(287, 34)
        Me.Markter_Cm.SQL_Column = "ST_name"
        Me.Markter_Cm.SQL_ID = "ST_ID"
        Me.Markter_Cm.SQL_IsNumericSearchField = False
        Me.Markter_Cm.SQL_ListSize = 200
        Me.Markter_Cm.SQL_NumberOfRows = 200
        Me.Markter_Cm.SQL_OrderByField = "ST_name"
        Me.Markter_Cm.SQL_SearchField = "ST_name"
        Me.Markter_Cm.SQL_Table = "Marketers"
        Me.Markter_Cm.TabIndex = 706
        Me.Markter_Cm.TextMaxLength = 250
        Me.Markter_Cm.Textt = ""
        '
        'SearchAgent_Pch_Bill
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ClientSize = New System.Drawing.Size(1004, 695)
        Me.Controls.Add(Me.RPT_CM)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.advancedDataGridView_main)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.AG_Cm)
        Me.Controls.Add(Me.IM_Serach_btn)
        Me.Controls.Add(Me.Marketer_Lb)
        Me.Controls.Add(Me.Markter_Cm)
        Me.Controls.Add(Me.Label32)
        Me.Controls.Add(Me.Bill_cmb)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.isDeletedCheckBox)
        Me.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimizeBox = False
        Me.Name = "SearchAgent_Pch_Bill"
        Me.Padding = New System.Windows.Forms.Padding(27, 74, 27, 25)
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "عرض فواتير مورد"
        CType(Me.advancedDataGridView_main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.bindingSource_main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MetroToolTip1 As MetroFramework.Components.MetroToolTip
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents isDeletedCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Bill_cmb As System.Windows.Forms.ComboBox
    Friend WithEvents Print_btn As System.Windows.Forms.Button
    Friend WithEvents Marketer_Lb As System.Windows.Forms.Label
    Friend WithEvents Markter_Cm As resturant.FSearch_Filter
    Friend WithEvents IM_Serach_btn As System.Windows.Forms.Button
    Friend WithEvents AG_Cm As resturant.FSearch_Filter
    Friend WithEvents advancedDataGridView_main As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents bindingSource_main As BindingSource
    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents TITLE_TXT As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ALL_time_CheckBox As CheckBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents DateRange_Flate As DateRange_Flate
    Friend WithEvents Panel1 As Panel
    Friend WithEvents advancedDataGridViewSearchToolBar_main As Zuby.ADGV.AdvancedDataGridViewSearchToolBar
    Friend WithEvents textBox_total As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents RPT_CM As System.Windows.Forms.ComboBox
End Class
