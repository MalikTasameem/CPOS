<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class STORES_Explorer
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(STORES_Explorer))
        Me.CMSearchTextBox = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ST_cm = New System.Windows.Forms.ComboBox()
        Me.GM_Serach = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Title_LB = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.textBox_total = New System.Windows.Forms.TextBox()
        Me.MetroToolTip1 = New MetroFramework.Components.MetroToolTip()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.EXCEL_BTN = New System.Windows.Forms.Button()
        Me.Recount_Cost_btn = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Up_Update_btn = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.IM_btn = New System.Windows.Forms.Button()
        Me.PrintButton = New System.Windows.Forms.Button()
        Me.DataB = New System.Windows.Forms.BindingSource(Me.components)
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.gridv = New Zuby.ADGV.AdvancedDataGridView()
        Me.miniToolStrip = New Zuby.ADGV.AdvancedDataGridViewSearchToolBar()
        Me.grid_panel = New System.Windows.Forms.Panel()
        Me.TOTAL_Grid = New System.Windows.Forms.DataGridView()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.advancedDataGridViewSearchToolBar_main = New Zuby.ADGV.AdvancedDataGridViewSearchToolBar()
        Me.BarcodeSearch_CB = New System.Windows.Forms.CheckBox()
        Me.IMNUM_CB = New System.Windows.Forms.CheckBox()
        Me.Print_type_Cmb = New System.Windows.Forms.ComboBox()
        Me.Show_only_Zero_CB = New System.Windows.Forms.CheckBox()
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.gridv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grid_panel.SuspendLayout()
        CType(Me.TOTAL_Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'CMSearchTextBox
        '
        Me.CMSearchTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CMSearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CMSearchTextBox.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMSearchTextBox.ForeColor = System.Drawing.SystemColors.InfoText
        Me.CMSearchTextBox.Location = New System.Drawing.Point(410, 100)
        Me.CMSearchTextBox.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
        Me.CMSearchTextBox.Name = "CMSearchTextBox"
        Me.CMSearchTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CMSearchTextBox.Size = New System.Drawing.Size(590, 25)
        Me.CMSearchTextBox.TabIndex = 272
        Me.CMSearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(219, 44)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 19)
        Me.Label9.TabIndex = 648
        Me.Label9.Text = "المخزن"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ST_cm
        '
        Me.ST_cm.BackColor = System.Drawing.SystemColors.Info
        Me.ST_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ST_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ST_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ST_cm.Font = New System.Drawing.Font("Arial", 12.25!)
        Me.ST_cm.FormattingEnabled = True
        Me.ST_cm.Location = New System.Drawing.Point(2, 41)
        Me.ST_cm.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ST_cm.Name = "ST_cm"
        Me.ST_cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ST_cm.Size = New System.Drawing.Size(214, 26)
        Me.ST_cm.TabIndex = 647
        '
        'GM_Serach
        '
        Me.GM_Serach.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.GM_Serach.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.GM_Serach.BackColor = System.Drawing.SystemColors.Info
        Me.GM_Serach.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GM_Serach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.GM_Serach.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GM_Serach.Font = New System.Drawing.Font("Arial", 12.25!)
        Me.GM_Serach.FormattingEnabled = True
        Me.GM_Serach.IntegralHeight = False
        Me.GM_Serach.Items.AddRange(New Object() {"قصيرة", "طويلة"})
        Me.GM_Serach.Location = New System.Drawing.Point(268, 40)
        Me.GM_Serach.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GM_Serach.Name = "GM_Serach"
        Me.GM_Serach.Size = New System.Drawing.Size(210, 26)
        Me.GM_Serach.TabIndex = 654
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(482, 43)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 19)
        Me.Label4.TabIndex = 653
        Me.Label4.Text = "التصنيف"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Title_LB
        '
        Me.Title_LB.BackColor = System.Drawing.SystemColors.Control
        Me.Title_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Title_LB.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Title_LB.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Title_LB.Location = New System.Drawing.Point(323, 1)
        Me.Title_LB.Name = "Title_LB"
        Me.Title_LB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Title_LB.Size = New System.Drawing.Size(249, 37)
        Me.Title_LB.TabIndex = 698
        Me.Title_LB.Text = "كشف المخــــزن"
        Me.Title_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(759, 656)
        Me.Label3.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 19)
        Me.Label3.TabIndex = 910
        Me.Label3.Text = "الصفوف المستعرضة"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'textBox_total
        '
        Me.textBox_total.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.textBox_total.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textBox_total.Location = New System.Drawing.Point(663, 652)
        Me.textBox_total.Name = "textBox_total"
        Me.textBox_total.ReadOnly = True
        Me.textBox_total.Size = New System.Drawing.Size(92, 25)
        Me.textBox_total.TabIndex = 909
        '
        'MetroToolTip1
        '
        Me.MetroToolTip1.Style = MetroFramework.MetroColorStyle.Blue
        Me.MetroToolTip1.StyleManager = Nothing
        Me.MetroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Arial", 16.0!, System.Drawing.FontStyle.Bold)
        Me.Button2.ForeColor = System.Drawing.Color.Black
        Me.Button2.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.Button2.Location = New System.Drawing.Point(577, 2)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Button2.Size = New System.Drawing.Size(41, 79)
        Me.Button2.TabIndex = 905
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.MetroToolTip1.SetToolTip(Me.Button2, "حفظ العرض")
        Me.Button2.UseVisualStyleBackColor = False
        '
        'EXCEL_BTN
        '
        Me.EXCEL_BTN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EXCEL_BTN.BackColor = System.Drawing.Color.White
        Me.EXCEL_BTN.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EXCEL_BTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EXCEL_BTN.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.EXCEL_BTN.Image = Global.resturant.My.Resources.Resources.EXcel
        Me.EXCEL_BTN.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.EXCEL_BTN.Location = New System.Drawing.Point(3, 1)
        Me.EXCEL_BTN.Name = "EXCEL_BTN"
        Me.EXCEL_BTN.Size = New System.Drawing.Size(109, 37)
        Me.EXCEL_BTN.TabIndex = 904
        Me.EXCEL_BTN.Text = "EXCEL تصدير"
        Me.EXCEL_BTN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EXCEL_BTN.UseVisualStyleBackColor = False
        '
        'Recount_Cost_btn
        '
        Me.Recount_Cost_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Recount_Cost_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Recount_Cost_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Recount_Cost_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Recount_Cost_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Recount_Cost_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Recount_Cost_btn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Recount_Cost_btn.ForeColor = System.Drawing.Color.Black
        Me.Recount_Cost_btn.Image = Global.resturant.My.Resources.Resources.iconfinder_icon_refresh_2867936
        Me.Recount_Cost_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Recount_Cost_btn.Location = New System.Drawing.Point(4, 654)
        Me.Recount_Cost_btn.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Recount_Cost_btn.Name = "Recount_Cost_btn"
        Me.Recount_Cost_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Recount_Cost_btn.Size = New System.Drawing.Size(147, 37)
        Me.Recount_Cost_btn.TabIndex = 688
        Me.Recount_Cost_btn.Text = "تدوير متوسط التكلفة"
        Me.Recount_Cost_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Recount_Cost_btn.UseVisualStyleBackColor = False
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(888, 654)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(115, 40)
        Me.ExitFormButton.TabIndex = 674
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Up_Update_btn
        '
        Me.Up_Update_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Up_Update_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Up_Update_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Up_Update_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Up_Update_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Up_Update_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Up_Update_btn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Up_Update_btn.ForeColor = System.Drawing.Color.Black
        Me.Up_Update_btn.Image = Global.resturant.My.Resources.Resources.output_onlinepngtools__3_
        Me.Up_Update_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Up_Update_btn.Location = New System.Drawing.Point(251, 654)
        Me.Up_Update_btn.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Up_Update_btn.Name = "Up_Update_btn"
        Me.Up_Update_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Up_Update_btn.Size = New System.Drawing.Size(97, 37)
        Me.Up_Update_btn.TabIndex = 652
        Me.Up_Update_btn.Text = "تعديــل"
        Me.Up_Update_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Up_Update_btn.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 16.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(216, 1)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Button1.Size = New System.Drawing.Size(106, 37)
        Me.Button1.TabIndex = 390
        Me.Button1.Text = "عــرض"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.UseVisualStyleBackColor = False
        '
        'IM_btn
        '
        Me.IM_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.IM_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.IM_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.IM_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.IM_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_btn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_btn.ForeColor = System.Drawing.Color.Black
        Me.IM_btn.Image = Global.resturant.My.Resources.Resources.if_category_item_select_92565
        Me.IM_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IM_btn.Location = New System.Drawing.Point(152, 654)
        Me.IM_btn.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IM_btn.Name = "IM_btn"
        Me.IM_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_btn.Size = New System.Drawing.Size(97, 37)
        Me.IM_btn.TabIndex = 389
        Me.IM_btn.Text = "الأصناف"
        Me.IM_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.IM_btn.UseVisualStyleBackColor = False
        '
        'PrintButton
        '
        Me.PrintButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PrintButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PrintButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PrintButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PrintButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.PrintButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.PrintButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PrintButton.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PrintButton.ForeColor = System.Drawing.Color.Black
        Me.PrintButton.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.PrintButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.PrintButton.Location = New System.Drawing.Point(113, 1)
        Me.PrintButton.Margin = New System.Windows.Forms.Padding(6, 9, 6, 9)
        Me.PrintButton.Name = "PrintButton"
        Me.PrintButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PrintButton.Size = New System.Drawing.Size(102, 37)
        Me.PrintButton.TabIndex = 274
        Me.PrintButton.Text = "طباعة"
        Me.PrintButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.PrintButton.UseVisualStyleBackColor = False
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.HorizontalScrollbar = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(620, 2)
        Me.CheckedListBox1.MultiColumn = True
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CheckedListBox1.Size = New System.Drawing.Size(379, 79)
        Me.CheckedListBox1.TabIndex = 901
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Button2)
        Me.Panel2.Controls.Add(Me.EXCEL_BTN)
        Me.Panel2.Controls.Add(Me.CheckedListBox1)
        Me.Panel2.Controls.Add(Me.Title_LB)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.GM_Serach)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.ST_cm)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.PrintButton)
        Me.Panel2.Location = New System.Drawing.Point(1, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1002, 92)
        Me.Panel2.TabIndex = 903
        '
        'gridv
        '
        Me.gridv.AllowUserToAddRows = False
        Me.gridv.AllowUserToDeleteRows = False
        Me.gridv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.gridv.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 10.75!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.gridv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridv.FilterAndSortEnabled = True
        Me.gridv.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.gridv.Location = New System.Drawing.Point(2, 3)
        Me.gridv.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.gridv.MultiSelect = False
        Me.gridv.Name = "gridv"
        Me.gridv.ReadOnly = True
        Me.gridv.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.gridv.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridv.RowTemplate.Height = 25
        Me.gridv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridv.Size = New System.Drawing.Size(997, 402)
        Me.gridv.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.gridv.TabIndex = 902
        '
        'miniToolStrip
        '
        Me.miniToolStrip.AllowMerge = False
        Me.miniToolStrip.AutoSize = False
        Me.miniToolStrip.CanOverflow = False
        Me.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.miniToolStrip.Location = New System.Drawing.Point(2, 4)
        Me.miniToolStrip.MaximumSize = New System.Drawing.Size(0, 27)
        Me.miniToolStrip.MinimumSize = New System.Drawing.Size(0, 27)
        Me.miniToolStrip.Name = "miniToolStrip"
        Me.miniToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.miniToolStrip.Size = New System.Drawing.Size(999, 27)
        Me.miniToolStrip.TabIndex = 903
        '
        'grid_panel
        '
        Me.grid_panel.Controls.Add(Me.TOTAL_Grid)
        Me.grid_panel.Controls.Add(Me.gridv)
        Me.grid_panel.Font = New System.Drawing.Font("Arial", 10.75!)
        Me.grid_panel.Location = New System.Drawing.Point(3, 162)
        Me.grid_panel.Name = "grid_panel"
        Me.grid_panel.Size = New System.Drawing.Size(1000, 487)
        Me.grid_panel.TabIndex = 903
        '
        'TOTAL_Grid
        '
        Me.TOTAL_Grid.AllowUserToAddRows = False
        Me.TOTAL_Grid.AllowUserToDeleteRows = False
        Me.TOTAL_Grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.TOTAL_Grid.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.TOTAL_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TOTAL_Grid.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TOTAL_Grid.Location = New System.Drawing.Point(2, 406)
        Me.TOTAL_Grid.MultiSelect = False
        Me.TOTAL_Grid.Name = "TOTAL_Grid"
        Me.TOTAL_Grid.ReadOnly = True
        Me.TOTAL_Grid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TOTAL_Grid.RowHeadersVisible = False
        Me.TOTAL_Grid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.TOTAL_Grid.RowTemplate.Height = 30
        Me.TOTAL_Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.TOTAL_Grid.Size = New System.Drawing.Size(998, 79)
        Me.TOTAL_Grid.TabIndex = 903
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.advancedDataGridViewSearchToolBar_main)
        Me.Panel3.Location = New System.Drawing.Point(3, 128)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1000, 29)
        Me.Panel3.TabIndex = 912
        '
        'advancedDataGridViewSearchToolBar_main
        '
        Me.advancedDataGridViewSearchToolBar_main.AllowMerge = False
        Me.advancedDataGridViewSearchToolBar_main.AutoSize = False
        Me.advancedDataGridViewSearchToolBar_main.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.advancedDataGridViewSearchToolBar_main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.advancedDataGridViewSearchToolBar_main.Location = New System.Drawing.Point(0, 9)
        Me.advancedDataGridViewSearchToolBar_main.MaximumSize = New System.Drawing.Size(0, 20)
        Me.advancedDataGridViewSearchToolBar_main.MinimumSize = New System.Drawing.Size(0, 20)
        Me.advancedDataGridViewSearchToolBar_main.Name = "advancedDataGridViewSearchToolBar_main"
        Me.advancedDataGridViewSearchToolBar_main.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.advancedDataGridViewSearchToolBar_main.Size = New System.Drawing.Size(1000, 20)
        Me.advancedDataGridViewSearchToolBar_main.TabIndex = 909
        Me.advancedDataGridViewSearchToolBar_main.Text = "AdvancedDataGridViewSearchToolBar1"
        '
        'BarcodeSearch_CB
        '
        Me.BarcodeSearch_CB.AutoSize = True
        Me.BarcodeSearch_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BarcodeSearch_CB.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.BarcodeSearch_CB.Location = New System.Drawing.Point(336, 100)
        Me.BarcodeSearch_CB.Name = "BarcodeSearch_CB"
        Me.BarcodeSearch_CB.Size = New System.Drawing.Size(70, 22)
        Me.BarcodeSearch_CB.TabIndex = 903
        Me.BarcodeSearch_CB.Text = "بالباركود"
        Me.BarcodeSearch_CB.UseVisualStyleBackColor = True
        '
        'IMNUM_CB
        '
        Me.IMNUM_CB.AutoSize = True
        Me.IMNUM_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMNUM_CB.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.IMNUM_CB.Location = New System.Drawing.Point(243, 100)
        Me.IMNUM_CB.Name = "IMNUM_CB"
        Me.IMNUM_CB.Size = New System.Drawing.Size(86, 22)
        Me.IMNUM_CB.TabIndex = 904
        Me.IMNUM_CB.Text = "برقم الصنف"
        Me.IMNUM_CB.UseVisualStyleBackColor = True
        '
        'Print_type_Cmb
        '
        Me.Print_type_Cmb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.Print_type_Cmb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Print_type_Cmb.BackColor = System.Drawing.SystemColors.Info
        Me.Print_type_Cmb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_type_Cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Print_type_Cmb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Print_type_Cmb.Font = New System.Drawing.Font("Arial", 12.25!)
        Me.Print_type_Cmb.FormattingEnabled = True
        Me.Print_type_Cmb.IntegralHeight = False
        Me.Print_type_Cmb.Items.AddRange(New Object() {"طباعة بالعرض", "طباعة بالطول"})
        Me.Print_type_Cmb.Location = New System.Drawing.Point(3, 97)
        Me.Print_type_Cmb.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Print_type_Cmb.Name = "Print_type_Cmb"
        Me.Print_type_Cmb.Size = New System.Drawing.Size(109, 26)
        Me.Print_type_Cmb.TabIndex = 906
        '
        'Show_only_Zero_CB
        '
        Me.Show_only_Zero_CB.AutoSize = True
        Me.Show_only_Zero_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Show_only_Zero_CB.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Show_only_Zero_CB.Location = New System.Drawing.Point(131, 100)
        Me.Show_only_Zero_CB.Name = "Show_only_Zero_CB"
        Me.Show_only_Zero_CB.Size = New System.Drawing.Size(103, 22)
        Me.Show_only_Zero_CB.TabIndex = 913
        Me.Show_only_Zero_CB.Text = "عرض كمية =0"
        Me.Show_only_Zero_CB.UseVisualStyleBackColor = True
        '
        'STORES_Explorer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 695)
        Me.Controls.Add(Me.Show_only_Zero_CB)
        Me.Controls.Add(Me.Print_type_Cmb)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.IMNUM_CB)
        Me.Controls.Add(Me.textBox_total)
        Me.Controls.Add(Me.BarcodeSearch_CB)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.CMSearchTextBox)
        Me.Controls.Add(Me.IM_btn)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.grid_panel)
        Me.Controls.Add(Me.Recount_Cost_btn)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Up_Update_btn)
        Me.Font = New System.Drawing.Font("JF Flat", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
        Me.MinimizeBox = False
        Me.Name = "STORES_Explorer"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "المخازن"
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.gridv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grid_panel.ResumeLayout(False)
        CType(Me.TOTAL_Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PrintButton As System.Windows.Forms.Button
    Friend WithEvents CMSearchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents IM_btn As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ST_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Up_Update_btn As System.Windows.Forms.Button
    Friend WithEvents GM_Serach As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Recount_Cost_btn As System.Windows.Forms.Button
    Friend WithEvents Title_LB As System.Windows.Forms.Label
    Friend WithEvents MetroToolTip1 As MetroFramework.Components.MetroToolTip
    Friend WithEvents DataB As BindingSource
    Friend WithEvents EXCEL_BTN As Button
    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Button2 As Button
    Friend WithEvents gridv As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents miniToolStrip As Zuby.ADGV.AdvancedDataGridViewSearchToolBar
    Friend WithEvents grid_panel As Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents textBox_total As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents advancedDataGridViewSearchToolBar_main As Zuby.ADGV.AdvancedDataGridViewSearchToolBar
    Friend WithEvents IMNUM_CB As System.Windows.Forms.CheckBox
    Friend WithEvents BarcodeSearch_CB As System.Windows.Forms.CheckBox
    Friend WithEvents TOTAL_Grid As DataGridView
    Friend WithEvents Print_type_Cmb As ComboBox
    Friend WithEvents Show_only_Zero_CB As CheckBox
End Class
