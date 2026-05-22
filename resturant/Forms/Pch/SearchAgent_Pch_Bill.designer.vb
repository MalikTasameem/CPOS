<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SearchAgent_Pch_Bill
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SearchAgent_Pch_Bill))
        Me.MetroToolTip1 = New MetroFramework.Components.MetroToolTip()
        Me.isDeletedCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Bill_cmb = New System.Windows.Forms.ComboBox()
        Me.advancedDataGridView_main = New System.Windows.Forms.DataGridView()
        Me.ALL_time_CheckBox = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.DateRange_Flate = New resturant.DateRange_Flate()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TotalsGrid = New System.Windows.Forms.DataGridView()
        Me.Print_btn = New System.Windows.Forms.Button()
        Me.PdfButton = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.SearchFilterTextBox = New System.Windows.Forms.TextBox()
        Me.IM_Serach_btn = New System.Windows.Forms.Button()
        Me.bindingSource_main = New System.Windows.Forms.BindingSource(Me.components)
        Me.RPT_CM = New System.Windows.Forms.ComboBox()
        Me.is_Auto_Select_CB = New System.Windows.Forms.CheckBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Title_Label = New System.Windows.Forms.Label()
        Me.TopCloseButton = New System.Windows.Forms.Button()
        Me.AG_Cm = New resturant.FSearch_Filter()
        Me.UcGridColumnsSelector1 = New resturant.UcGridColumnsSelector()
        CType(Me.advancedDataGridView_main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.TotalsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.bindingSource_main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.TitleBar_Panel.SuspendLayout()
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
        Me.isDeletedCheckBox.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.isDeletedCheckBox.Location = New System.Drawing.Point(780, 76)
        Me.isDeletedCheckBox.Name = "isDeletedCheckBox"
        Me.isDeletedCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.isDeletedCheckBox.Size = New System.Drawing.Size(87, 21)
        Me.isDeletedCheckBox.TabIndex = 666
        Me.isDeletedCheckBox.Text = "ملغية فقط"
        Me.isDeletedCheckBox.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(994, 48)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label4.Size = New System.Drawing.Size(47, 17)
        Me.Label4.TabIndex = 667
        Me.Label4.Text = "المورد :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(995, 78)
        Me.Label32.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(41, 17)
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
        Me.Bill_cmb.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bill_cmb.ForeColor = System.Drawing.Color.DarkBlue
        Me.Bill_cmb.FormattingEnabled = True
        Me.Bill_cmb.Items.AddRange(New Object() {"كل الفواتير", "الخالصة فقط", "الغير خالصة فقط"})
        Me.Bill_cmb.Location = New System.Drawing.Point(874, 74)
        Me.Bill_cmb.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
        Me.Bill_cmb.MaxDropDownItems = 15
        Me.Bill_cmb.Name = "Bill_cmb"
        Me.Bill_cmb.Size = New System.Drawing.Size(115, 25)
        Me.Bill_cmb.TabIndex = 702
        '
        'advancedDataGridView_main
        '
        Me.advancedDataGridView_main.AllowUserToAddRows = False
        Me.advancedDataGridView_main.AllowUserToDeleteRows = False
        Me.advancedDataGridView_main.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.advancedDataGridView_main.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.advancedDataGridView_main.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.advancedDataGridView_main.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.advancedDataGridView_main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.advancedDataGridView_main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.advancedDataGridView_main.Location = New System.Drawing.Point(0, 0)
        Me.advancedDataGridView_main.Margin = New System.Windows.Forms.Padding(1)
        Me.advancedDataGridView_main.MultiSelect = False
        Me.advancedDataGridView_main.Name = "advancedDataGridView_main"
        Me.advancedDataGridView_main.ReadOnly = True
        Me.advancedDataGridView_main.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.advancedDataGridView_main.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.advancedDataGridView_main.RowHeadersVisible = False
        Me.advancedDataGridView_main.RowTemplate.Height = 30
        Me.advancedDataGridView_main.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.advancedDataGridView_main.Size = New System.Drawing.Size(1042, 484)
        Me.advancedDataGridView_main.TabIndex = 903
        '
        'ALL_time_CheckBox
        '
        Me.ALL_time_CheckBox.AutoSize = True
        Me.ALL_time_CheckBox.Checked = True
        Me.ALL_time_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ALL_time_CheckBox.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ALL_time_CheckBox.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.ALL_time_CheckBox.Location = New System.Drawing.Point(538, 13)
        Me.ALL_time_CheckBox.Name = "ALL_time_CheckBox"
        Me.ALL_time_CheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ALL_time_CheckBox.Size = New System.Drawing.Size(84, 21)
        Me.ALL_time_CheckBox.TabIndex = 908
        Me.ALL_time_CheckBox.Text = "كل الفترات"
        Me.ALL_time_CheckBox.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.DateRange_Flate)
        Me.Panel2.Controls.Add(Me.ALL_time_CheckBox)
        Me.Panel2.Location = New System.Drawing.Point(8, 119)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Panel2.Size = New System.Drawing.Size(633, 46)
        Me.Panel2.TabIndex = 909
        '
        'DateRange_Flate
        '
        Me.DateRange_Flate.AutoSize = True
        Me.DateRange_Flate.BackColor = System.Drawing.Color.White
        Me.DateRange_Flate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateRange_Flate.Location = New System.Drawing.Point(2, 4)
        Me.DateRange_Flate.Margin = New System.Windows.Forms.Padding(2)
        Me.DateRange_Flate.Name = "DateRange_Flate"
        Me.DateRange_Flate.Size = New System.Drawing.Size(531, 41)
        Me.DateRange_Flate.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TotalsGrid)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(1, 683)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1045, 86)
        Me.Panel1.TabIndex = 910
        '
        'TotalsGrid
        '
        Me.TotalsGrid.AllowUserToAddRows = False
        Me.TotalsGrid.AllowUserToDeleteRows = False
        Me.TotalsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.TotalsGrid.BackgroundColor = System.Drawing.Color.White
        Me.TotalsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TotalsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.TotalsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TotalsGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.TotalsGrid.ColumnHeadersHeight = 30
        Me.TotalsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.TotalsGrid.DefaultCellStyle = DataGridViewCellStyle4
        Me.TotalsGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalsGrid.EnableHeadersVisualStyles = False
        Me.TotalsGrid.Location = New System.Drawing.Point(0, 0)
        Me.TotalsGrid.MultiSelect = False
        Me.TotalsGrid.Name = "TotalsGrid"
        Me.TotalsGrid.ReadOnly = True
        Me.TotalsGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TotalsGrid.RowHeadersVisible = False
        Me.TotalsGrid.RowTemplate.Height = 30
        Me.TotalsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.TotalsGrid.Size = New System.Drawing.Size(1043, 84)
        Me.TotalsGrid.TabIndex = 0
        '
        'Print_btn
        '
        Me.Print_btn.BackColor = System.Drawing.Color.White
        Me.Print_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_btn.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Print_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Print_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Print_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Print_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Print_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_btn.Location = New System.Drawing.Point(125, 46)
        Me.Print_btn.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.Print_btn.Name = "Print_btn"
        Me.Print_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_btn.Size = New System.Drawing.Size(105, 36)
        Me.Print_btn.TabIndex = 704
        Me.Print_btn.TabStop = False
        Me.Print_btn.Text = "⎙ طباعة"
        Me.Print_btn.UseVisualStyleBackColor = False
        '
        'PdfButton
        '
        Me.PdfButton.BackColor = System.Drawing.Color.White
        Me.PdfButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PdfButton.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.PdfButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.PdfButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.PdfButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PdfButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.PdfButton.ForeColor = System.Drawing.Color.Black
        Me.PdfButton.Location = New System.Drawing.Point(236, 46)
        Me.PdfButton.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.PdfButton.Name = "PdfButton"
        Me.PdfButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PdfButton.Size = New System.Drawing.Size(96, 36)
        Me.PdfButton.TabIndex = 1001
        Me.PdfButton.TabStop = False
        Me.PdfButton.Tag = "PRINT"
        Me.PdfButton.Text = "PDF"
        Me.PdfButton.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.SearchFilterTextBox)
        Me.Panel3.Location = New System.Drawing.Point(120, 177)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(8, 5, 8, 0)
        Me.Panel3.Size = New System.Drawing.Size(637, 31)
        Me.Panel3.TabIndex = 911
        '
        'SearchFilterTextBox
        '
        Me.SearchFilterTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.SearchFilterTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SearchFilterTextBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.SearchFilterTextBox.Location = New System.Drawing.Point(8, 5)
        Me.SearchFilterTextBox.Name = "SearchFilterTextBox"
        Me.SearchFilterTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SearchFilterTextBox.Size = New System.Drawing.Size(619, 22)
        Me.SearchFilterTextBox.TabIndex = 0
        Me.SearchFilterTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'IM_Serach_btn
        '
        Me.IM_Serach_btn.BackColor = System.Drawing.Color.White
        Me.IM_Serach_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.IM_Serach_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_Serach_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_Serach_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.IM_Serach_btn.ForeColor = System.Drawing.Color.Black
        Me.IM_Serach_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IM_Serach_btn.Location = New System.Drawing.Point(14, 46)
        Me.IM_Serach_btn.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.IM_Serach_btn.Name = "IM_Serach_btn"
        Me.IM_Serach_btn.Size = New System.Drawing.Size(105, 36)
        Me.IM_Serach_btn.TabIndex = 707
        Me.IM_Serach_btn.Text = "⌕ بحث"
        Me.IM_Serach_btn.UseVisualStyleBackColor = False
        '
        'bindingSource_main
        '
        '
        'RPT_CM
        '
        Me.RPT_CM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.RPT_CM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.RPT_CM.BackColor = System.Drawing.SystemColors.HighlightText
        Me.RPT_CM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RPT_CM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RPT_CM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RPT_CM.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RPT_CM.FormattingEnabled = True
        Me.RPT_CM.IntegralHeight = False
        Me.RPT_CM.Items.AddRange(New Object() {"عرض تفصيل الفواتير", "عرض إحصائيـــات"})
        Me.RPT_CM.Location = New System.Drawing.Point(761, 177)
        Me.RPT_CM.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.RPT_CM.Name = "RPT_CM"
        Me.RPT_CM.Size = New System.Drawing.Size(273, 29)
        Me.RPT_CM.TabIndex = 914
        '
        'is_Auto_Select_CB
        '
        Me.is_Auto_Select_CB.AutoSize = True
        Me.is_Auto_Select_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.is_Auto_Select_CB.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.is_Auto_Select_CB.Location = New System.Drawing.Point(873, 152)
        Me.is_Auto_Select_CB.Name = "is_Auto_Select_CB"
        Me.is_Auto_Select_CB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.is_Auto_Select_CB.Size = New System.Drawing.Size(137, 21)
        Me.is_Auto_Select_CB.TabIndex = 915
        Me.is_Auto_Select_CB.Text = "عرض الفواتير مباشرة"
        Me.is_Auto_Select_CB.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.advancedDataGridView_main)
        Me.Panel4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.Location = New System.Drawing.Point(4, 210)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1042, 484)
        Me.Panel4.TabIndex = 916
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.TitleBar_Panel.Controls.Add(Me.Button1)
        Me.TitleBar_Panel.Controls.Add(Me.Title_Label)
        Me.TitleBar_Panel.Controls.Add(Me.TopCloseButton)
        Me.TitleBar_Panel.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(1, 1)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(1045, 35)
        Me.TitleBar_Panel.TabIndex = 999
        '
        'Button1
        '
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(42, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(42, 35)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "⬜"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Title_Label
        '
        Me.Title_Label.AutoSize = True
        Me.Title_Label.Dock = System.Windows.Forms.DockStyle.Right
        Me.Title_Label.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Title_Label.ForeColor = System.Drawing.Color.White
        Me.Title_Label.Location = New System.Drawing.Point(887, 0)
        Me.Title_Label.Name = "Title_Label"
        Me.Title_Label.Size = New System.Drawing.Size(158, 25)
        Me.Title_Label.TabIndex = 1
        Me.Title_Label.Text = "عرض فواتير الموردين"
        '
        'TopCloseButton
        '
        Me.TopCloseButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TopCloseButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.TopCloseButton.FlatAppearance.BorderSize = 0
        Me.TopCloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.TopCloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TopCloseButton.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.TopCloseButton.ForeColor = System.Drawing.Color.White
        Me.TopCloseButton.Location = New System.Drawing.Point(0, 0)
        Me.TopCloseButton.Name = "TopCloseButton"
        Me.TopCloseButton.Size = New System.Drawing.Size(42, 35)
        Me.TopCloseButton.TabIndex = 0
        Me.TopCloseButton.Text = "X"
        Me.TopCloseButton.UseVisualStyleBackColor = True
        '
        'AG_Cm
        '
        Me.AG_Cm.CancelSearchImage = CType(resources.GetObject("AG_Cm.CancelSearchImage"), System.Drawing.Image)
        Me.AG_Cm.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AG_Cm.Location = New System.Drawing.Point(598, 39)
        Me.AG_Cm.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.AG_Cm.Name = "AG_Cm"
        Me.AG_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AG_Cm.Size = New System.Drawing.Size(391, 31)
        Me.AG_Cm.SQL_Column = "AG_NAME_B"
        Me.AG_Cm.SQL_ID = "AG_ID"
        Me.AG_Cm.SQL_IsNumericSearchField = False
        Me.AG_Cm.SQL_ListSize = 200
        Me.AG_Cm.SQL_NumberOfRows = 200
        Me.AG_Cm.SQL_OrderByField = "AG_NAME_B"
        Me.AG_Cm.SQL_SearchField = "AG_NAME_B"
        Me.AG_Cm.SQL_SearchField_WHERE = ""
        Me.AG_Cm.SQL_Table = "AGENTS_MENU_V"
        Me.AG_Cm.TabIndex = 708
        Me.AG_Cm.TextMaxLength = 250
        Me.AG_Cm.Textt = ""
        '
        'UcGridColumnsSelector1
        '
        Me.UcGridColumnsSelector1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.UcGridColumnsSelector1.Location = New System.Drawing.Point(4, 177)
        Me.UcGridColumnsSelector1.Name = "UcGridColumnsSelector1"
        Me.UcGridColumnsSelector1.PopupMaxHeight = 320
        Me.UcGridColumnsSelector1.PopupMinHeight = 120
        Me.UcGridColumnsSelector1.PopupWidth = 260
        Me.UcGridColumnsSelector1.SettingsFolder = "C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\Grid" &
    "ColumnsSettings"
        Me.UcGridColumnsSelector1.Size = New System.Drawing.Size(115, 32)
        Me.UcGridColumnsSelector1.TabIndex = 1000
        '
        'SearchAgent_Pch_Bill
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ClientSize = New System.Drawing.Size(1047, 770)
        Me.Controls.Add(Me.UcGridColumnsSelector1)
        Me.Controls.Add(Me.PdfButton)
        Me.Controls.Add(Me.Print_btn)
        Me.Controls.Add(Me.AG_Cm)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.IM_Serach_btn)
        Me.Controls.Add(Me.Label32)
        Me.Controls.Add(Me.is_Auto_Select_CB)
        Me.Controls.Add(Me.Bill_cmb)
        Me.Controls.Add(Me.isDeletedCheckBox)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.RPT_CM)
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimizeBox = False
        Me.Name = "SearchAgent_Pch_Bill"
        Me.Padding = New System.Windows.Forms.Padding(1)
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "عرض فواتير مورد"
        CType(Me.advancedDataGridView_main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.TotalsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.bindingSource_main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MetroToolTip1 As MetroFramework.Components.MetroToolTip
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents isDeletedCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Bill_cmb As System.Windows.Forms.ComboBox
    Friend WithEvents Print_btn As System.Windows.Forms.Button
    Friend WithEvents IM_Serach_btn As System.Windows.Forms.Button
    Friend WithEvents AG_Cm As resturant.FSearch_Filter
    Friend WithEvents advancedDataGridView_main As DataGridView
    Friend WithEvents bindingSource_main As BindingSource
    Friend WithEvents ALL_time_CheckBox As CheckBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents DateRange_Flate As DateRange_Flate
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TotalsGrid As DataGridView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents SearchFilterTextBox As TextBox
    Friend WithEvents PdfButton As Button
    Friend WithEvents RPT_CM As System.Windows.Forms.ComboBox
    Friend WithEvents is_Auto_Select_CB As CheckBox
    Friend WithEvents Panel4 As Panel
    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
    Friend WithEvents Title_Label As System.Windows.Forms.Label
    Friend WithEvents TopCloseButton As System.Windows.Forms.Button
    Friend WithEvents Button1 As Button
    Friend WithEvents UcGridColumnsSelector1 As UcGridColumnsSelector
End Class

