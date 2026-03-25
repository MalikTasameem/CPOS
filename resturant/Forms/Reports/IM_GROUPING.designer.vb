<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class IM_GROUPING
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IM_GROUPING))
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ST_cm = New System.Windows.Forms.ComboBox()
        Me.GM_Serach = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Title_LB = New System.Windows.Forms.Label()
        Me.MetroToolTip1 = New MetroFramework.Components.MetroToolTip()
        Me.EXCEL_BTN = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PrintButton = New System.Windows.Forms.Button()
        Me.DataB = New System.Windows.Forms.BindingSource(Me.components)
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.By_IM_RD = New System.Windows.Forms.RadioButton()
        Me.By_GM_RD = New System.Windows.Forms.RadioButton()
        Me.By_IM_CAT_RD = New System.Windows.Forms.RadioButton()
        Me.gridv = New Zuby.ADGV.AdvancedDataGridView()
        Me.miniToolStrip = New Zuby.ADGV.AdvancedDataGridViewSearchToolBar()
        Me.grid_panel = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.advancedDataGridViewSearchToolBar_main = New Zuby.ADGV.AdvancedDataGridViewSearchToolBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.IM_CAT_CM = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Barcode_SH_txt = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.IM_MENU_FS = New resturant.FSearch_Filter()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.gridv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grid_panel.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(425, 96)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 18)
        Me.Label9.TabIndex = 648
        Me.Label9.Text = "المخزن"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label9.Visible = False
        '
        'ST_cm
        '
        Me.ST_cm.BackColor = System.Drawing.SystemColors.Info
        Me.ST_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ST_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ST_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ST_cm.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ST_cm.FormattingEnabled = True
        Me.ST_cm.Location = New System.Drawing.Point(208, 91)
        Me.ST_cm.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ST_cm.Name = "ST_cm"
        Me.ST_cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ST_cm.Size = New System.Drawing.Size(214, 26)
        Me.ST_cm.TabIndex = 647
        Me.ST_cm.Visible = False
        '
        'GM_Serach
        '
        Me.GM_Serach.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.GM_Serach.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.GM_Serach.BackColor = System.Drawing.SystemColors.Info
        Me.GM_Serach.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GM_Serach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.GM_Serach.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GM_Serach.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GM_Serach.FormattingEnabled = True
        Me.GM_Serach.IntegralHeight = False
        Me.GM_Serach.Items.AddRange(New Object() {"قصيرة", "طويلة"})
        Me.GM_Serach.Location = New System.Drawing.Point(487, 91)
        Me.GM_Serach.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GM_Serach.Name = "GM_Serach"
        Me.GM_Serach.Size = New System.Drawing.Size(201, 26)
        Me.GM_Serach.TabIndex = 654
        Me.GM_Serach.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(692, 96)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 18)
        Me.Label4.TabIndex = 653
        Me.Label4.Text = "التصنيف"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label4.Visible = False
        '
        'Title_LB
        '
        Me.Title_LB.BackColor = System.Drawing.SystemColors.Control
        Me.Title_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Title_LB.Font = New System.Drawing.Font("Arial", 17.75!, System.Drawing.FontStyle.Bold)
        Me.Title_LB.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Title_LB.Location = New System.Drawing.Point(323, 1)
        Me.Title_LB.Name = "Title_LB"
        Me.Title_LB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Title_LB.Size = New System.Drawing.Size(357, 37)
        Me.Title_LB.TabIndex = 698
        Me.Title_LB.Text = "إحصائيـــات"
        Me.Title_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MetroToolTip1
        '
        Me.MetroToolTip1.Style = MetroFramework.MetroColorStyle.Blue
        Me.MetroToolTip1.StyleManager = Nothing
        Me.MetroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light
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
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(888, 644)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(115, 40)
        Me.ExitFormButton.TabIndex = 674
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        Me.ExitFormButton.Visible = False
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
        Me.CheckedListBox1.Font = New System.Drawing.Font("Arial", 9.25!, System.Drawing.FontStyle.Bold)
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.HorizontalScrollbar = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(686, 2)
        Me.CheckedListBox1.MultiColumn = True
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CheckedListBox1.Size = New System.Drawing.Size(313, 72)
        Me.CheckedListBox1.TabIndex = 901
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.CheckBox1)
        Me.Panel2.Controls.Add(Me.By_IM_RD)
        Me.Panel2.Controls.Add(Me.By_GM_RD)
        Me.Panel2.Controls.Add(Me.By_IM_CAT_RD)
        Me.Panel2.Controls.Add(Me.EXCEL_BTN)
        Me.Panel2.Controls.Add(Me.CheckedListBox1)
        Me.Panel2.Controls.Add(Me.Title_LB)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.PrintButton)
        Me.Panel2.Location = New System.Drawing.Point(1, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1002, 87)
        Me.Panel2.TabIndex = 903
        '
        'By_IM_RD
        '
        Me.By_IM_RD.AutoSize = True
        Me.By_IM_RD.Font = New System.Drawing.Font("Arial", 14.75!, System.Drawing.FontStyle.Bold)
        Me.By_IM_RD.Location = New System.Drawing.Point(20, 52)
        Me.By_IM_RD.Name = "By_IM_RD"
        Me.By_IM_RD.Size = New System.Drawing.Size(113, 28)
        Me.By_IM_RD.TabIndex = 911
        Me.By_IM_RD.Text = "حسب الصنف"
        Me.By_IM_RD.UseVisualStyleBackColor = True
        '
        'By_GM_RD
        '
        Me.By_GM_RD.AutoSize = True
        Me.By_GM_RD.Font = New System.Drawing.Font("Arial", 14.75!, System.Drawing.FontStyle.Bold)
        Me.By_GM_RD.Location = New System.Drawing.Point(139, 52)
        Me.By_GM_RD.Name = "By_GM_RD"
        Me.By_GM_RD.Size = New System.Drawing.Size(132, 28)
        Me.By_GM_RD.TabIndex = 910
        Me.By_GM_RD.Text = "حسب المجموعة"
        Me.By_GM_RD.UseVisualStyleBackColor = True
        '
        'By_IM_CAT_RD
        '
        Me.By_IM_CAT_RD.AutoSize = True
        Me.By_IM_CAT_RD.Checked = True
        Me.By_IM_CAT_RD.Font = New System.Drawing.Font("Arial", 14.75!, System.Drawing.FontStyle.Bold)
        Me.By_IM_CAT_RD.Location = New System.Drawing.Point(277, 52)
        Me.By_IM_CAT_RD.Name = "By_IM_CAT_RD"
        Me.By_IM_CAT_RD.Size = New System.Drawing.Size(97, 28)
        Me.By_IM_CAT_RD.TabIndex = 909
        Me.By_IM_CAT_RD.TabStop = True
        Me.By_IM_CAT_RD.Text = "حسب الفئة"
        Me.By_IM_CAT_RD.UseVisualStyleBackColor = True
        '
        'gridv
        '
        Me.gridv.AllowUserToAddRows = False
        Me.gridv.AllowUserToDeleteRows = False
        Me.gridv.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.gridv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.gridv.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 10.75!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.gridv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridv.FilterAndSortEnabled = True
        Me.gridv.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.gridv.Location = New System.Drawing.Point(2, 19)
        Me.gridv.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.gridv.MultiSelect = False
        Me.gridv.Name = "gridv"
        Me.gridv.ReadOnly = True
        Me.gridv.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 10.75!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridv.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.gridv.RowTemplate.Height = 25
        Me.gridv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridv.Size = New System.Drawing.Size(997, 428)
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
        Me.grid_panel.Controls.Add(Me.gridv)
        Me.grid_panel.Font = New System.Drawing.Font("Arial", 10.75!)
        Me.grid_panel.Location = New System.Drawing.Point(3, 189)
        Me.grid_panel.Name = "grid_panel"
        Me.grid_panel.Size = New System.Drawing.Size(999, 450)
        Me.grid_panel.TabIndex = 903
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.advancedDataGridViewSearchToolBar_main)
        Me.Panel3.Location = New System.Drawing.Point(3, 157)
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
        Me.advancedDataGridViewSearchToolBar_main.Size = New System.Drawing.Size(0, 20)
        Me.advancedDataGridViewSearchToolBar_main.TabIndex = 909
        Me.advancedDataGridViewSearchToolBar_main.Text = "AdvancedDataGridViewSearchToolBar1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(972, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 18)
        Me.Label1.TabIndex = 906
        Me.Label1.Text = "الفئة"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label1.Visible = False
        '
        'IM_CAT_CM
        '
        Me.IM_CAT_CM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.IM_CAT_CM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.IM_CAT_CM.BackColor = System.Drawing.SystemColors.Info
        Me.IM_CAT_CM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_CAT_CM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.IM_CAT_CM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_CAT_CM.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_CAT_CM.FormattingEnabled = True
        Me.IM_CAT_CM.IntegralHeight = False
        Me.IM_CAT_CM.Items.AddRange(New Object() {"قصيرة", "طويلة"})
        Me.IM_CAT_CM.Location = New System.Drawing.Point(766, 91)
        Me.IM_CAT_CM.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IM_CAT_CM.Name = "IM_CAT_CM"
        Me.IM_CAT_CM.Size = New System.Drawing.Size(201, 26)
        Me.IM_CAT_CM.TabIndex = 913
        Me.IM_CAT_CM.Visible = False
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Arial", 10.75!, System.Drawing.FontStyle.Bold)
        Me.Label10.Location = New System.Drawing.Point(941, 124)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(58, 29)
        Me.Label10.TabIndex = 917
        Me.Label10.Text = "باركود :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label10.Visible = False
        '
        'Barcode_SH_txt
        '
        Me.Barcode_SH_txt.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Barcode_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Barcode_SH_txt.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Barcode_SH_txt.ForeColor = System.Drawing.Color.Blue
        Me.Barcode_SH_txt.Location = New System.Drawing.Point(623, 122)
        Me.Barcode_SH_txt.Name = "Barcode_SH_txt"
        Me.Barcode_SH_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Barcode_SH_txt.Size = New System.Drawing.Size(316, 32)
        Me.Barcode_SH_txt.TabIndex = 916
        Me.Barcode_SH_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Barcode_SH_txt.Visible = False
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.Font = New System.Drawing.Font("Arial", 10.75!, System.Drawing.FontStyle.Bold)
        Me.Label11.Location = New System.Drawing.Point(562, 119)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(55, 34)
        Me.Label11.TabIndex = 915
        Me.Label11.Text = "الصنف:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label11.Visible = False
        '
        'IM_MENU_FS
        '
        Me.IM_MENU_FS.CancelSearchImage = CType(resources.GetObject("IM_MENU_FS.CancelSearchImage"), System.Drawing.Image)
        Me.IM_MENU_FS.Location = New System.Drawing.Point(208, 119)
        Me.IM_MENU_FS.Name = "IM_MENU_FS"
        Me.IM_MENU_FS.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_MENU_FS.Size = New System.Drawing.Size(352, 34)
        Me.IM_MENU_FS.SQL_Column = "item_name"
        Me.IM_MENU_FS.SQL_ID = "IM_ID"
        Me.IM_MENU_FS.SQL_IsNumericSearchField = False
        Me.IM_MENU_FS.SQL_ListSize = 200
        Me.IM_MENU_FS.SQL_NumberOfRows = 200
        Me.IM_MENU_FS.SQL_OrderByField = "item_name"
        Me.IM_MENU_FS.SQL_SearchField = "item_name"
        Me.IM_MENU_FS.SQL_Table = "IM_Active_V"
        Me.IM_MENU_FS.TabIndex = 914
        Me.IM_MENU_FS.TextMaxLength = 250
        Me.IM_MENU_FS.Textt = ""
        Me.IM_MENU_FS.Visible = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(605, 50)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(77, 21)
        Me.CheckBox1.TabIndex = 912
        Me.CheckBox1.Text = "تحديد الكل"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'IM_GROUPING
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 695)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Barcode_SH_txt)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.IM_MENU_FS)
        Me.Controls.Add(Me.IM_CAT_CM)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.grid_panel)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ST_cm)
        Me.Controls.Add(Me.GM_Serach)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("JF Flat", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
        Me.MinimizeBox = False
        Me.Name = "IM_GROUPING"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "المخازن"
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.gridv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grid_panel.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PrintButton As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ST_cm As System.Windows.Forms.ComboBox
    Friend WithEvents GM_Serach As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Title_LB As System.Windows.Forms.Label
    Friend WithEvents MetroToolTip1 As MetroFramework.Components.MetroToolTip
    Friend WithEvents DataB As BindingSource
    Friend WithEvents EXCEL_BTN As Button
    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents gridv As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents miniToolStrip As Zuby.ADGV.AdvancedDataGridViewSearchToolBar
    Friend WithEvents grid_panel As Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents advancedDataGridViewSearchToolBar_main As Zuby.ADGV.AdvancedDataGridViewSearchToolBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents IM_CAT_CM As System.Windows.Forms.ComboBox
    Friend WithEvents By_IM_RD As System.Windows.Forms.RadioButton
    Friend WithEvents By_GM_RD As System.Windows.Forms.RadioButton
    Friend WithEvents By_IM_CAT_RD As System.Windows.Forms.RadioButton
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Barcode_SH_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents IM_MENU_FS As resturant.FSearch_Filter
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
End Class
