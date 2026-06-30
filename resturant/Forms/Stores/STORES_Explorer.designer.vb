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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(STORES_Explorer))
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.MinFormButton = New System.Windows.Forms.Button()
        Me.MaxFormButton = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Title_LB = New System.Windows.Forms.Label()
        Me.CMSearchTextBox = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ST_cm = New System.Windows.Forms.ComboBox()
        Me.GM_Serach = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.textBox_total = New System.Windows.Forms.TextBox()
        Me.MetroToolTip1 = New MetroFramework.Components.MetroToolTip()
        Me.EXCEL_BTN = New System.Windows.Forms.Button()
        Me.Recount_Cost_btn = New System.Windows.Forms.Button()
        Me.Up_Update_btn = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.IM_btn = New System.Windows.Forms.Button()
        Me.PrintButton = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.gridv = New System.Windows.Forms.DataGridView()
        Me.TOTAL_Grid = New System.Windows.Forms.DataGridView()
        Me.BarcodeSearch_CB = New System.Windows.Forms.CheckBox()
        Me.IMNUM_CB = New System.Windows.Forms.CheckBox()
        Me.Print_type_Cmb = New System.Windows.Forms.ComboBox()
        Me.Show_only_Zero_CB = New System.Windows.Forms.CheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.UcGridColumnsSelector1 = New resturant.UcGridColumnsSelector()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TitleBar_Panel.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.gridv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TOTAL_Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.TitleBar_Panel.Controls.Add(Me.MinFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.MaxFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.ExitFormButton)
        Me.TitleBar_Panel.Controls.Add(Me.Title_LB)
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(1012, 40)
        Me.TitleBar_Panel.TabIndex = 999
        Me.TitleBar_Panel.Tag = "HEADER"
        '
        'MinFormButton
        '
        Me.MinFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MinFormButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.MinFormButton.FlatAppearance.BorderSize = 0
        Me.MinFormButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.MinFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MinFormButton.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.MinFormButton.ForeColor = System.Drawing.Color.White
        Me.MinFormButton.Location = New System.Drawing.Point(90, 0)
        Me.MinFormButton.Name = "MinFormButton"
        Me.MinFormButton.Size = New System.Drawing.Size(45, 40)
        Me.MinFormButton.TabIndex = 3
        Me.MinFormButton.Tag = "APP_CONTROL"
        Me.MinFormButton.Text = "ـ"
        Me.MinFormButton.UseVisualStyleBackColor = False
        '
        'MaxFormButton
        '
        Me.MaxFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MaxFormButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.MaxFormButton.FlatAppearance.BorderSize = 0
        Me.MaxFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MaxFormButton.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.MaxFormButton.ForeColor = System.Drawing.Color.White
        Me.MaxFormButton.Location = New System.Drawing.Point(45, 0)
        Me.MaxFormButton.Name = "MaxFormButton"
        Me.MaxFormButton.Size = New System.Drawing.Size(45, 40)
        Me.MaxFormButton.TabIndex = 2
        Me.MaxFormButton.Tag = "APP_CONTROL"
        Me.MaxFormButton.Text = "⬜"
        Me.MaxFormButton.UseVisualStyleBackColor = False
        '
        'ExitFormButton
        '
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.ExitFormButton.FlatAppearance.BorderSize = 0
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitFormButton.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.Color.White
        Me.ExitFormButton.Location = New System.Drawing.Point(0, 0)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(45, 40)
        Me.ExitFormButton.TabIndex = 1
        Me.ExitFormButton.Tag = "APP_CONTROL"
        Me.ExitFormButton.Text = "X"
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Title_LB
        '
        Me.Title_LB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Title_LB.AutoSize = True
        Me.Title_LB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Title_LB.ForeColor = System.Drawing.Color.White
        Me.Title_LB.Location = New System.Drawing.Point(868, 9)
        Me.Title_LB.Name = "Title_LB"
        Me.Title_LB.Size = New System.Drawing.Size(108, 21)
        Me.Title_LB.TabIndex = 0
        Me.Title_LB.Tag = "TITLE_TRANSPARENT"
        Me.Title_LB.Text = "كشف المخــــزن"
        Me.Title_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CMSearchTextBox
        '
        Me.CMSearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CMSearchTextBox.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMSearchTextBox.ForeColor = System.Drawing.SystemColors.InfoText
        Me.CMSearchTextBox.Location = New System.Drawing.Point(447, 6)
        Me.CMSearchTextBox.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
        Me.CMSearchTextBox.Name = "CMSearchTextBox"
        Me.CMSearchTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CMSearchTextBox.Size = New System.Drawing.Size(442, 27)
        Me.CMSearchTextBox.TabIndex = 272
        Me.CMSearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(515, 22)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(55, 21)
        Me.Label9.TabIndex = 648
        Me.Label9.Text = "المخزن"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ST_cm
        '
        Me.ST_cm.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ST_cm.BackColor = System.Drawing.SystemColors.Info
        Me.ST_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ST_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ST_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ST_cm.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ST_cm.FormattingEnabled = True
        Me.ST_cm.Location = New System.Drawing.Point(308, 47)
        Me.ST_cm.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ST_cm.Name = "ST_cm"
        Me.ST_cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ST_cm.Size = New System.Drawing.Size(264, 29)
        Me.ST_cm.TabIndex = 647
        '
        'GM_Serach
        '
        Me.GM_Serach.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.GM_Serach.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.GM_Serach.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.GM_Serach.BackColor = System.Drawing.SystemColors.Info
        Me.GM_Serach.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GM_Serach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.GM_Serach.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GM_Serach.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GM_Serach.FormattingEnabled = True
        Me.GM_Serach.IntegralHeight = False
        Me.GM_Serach.Items.AddRange(New Object() {"قصيرة", "طويلة"})
        Me.GM_Serach.Location = New System.Drawing.Point(576, 46)
        Me.GM_Serach.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GM_Serach.Name = "GM_Serach"
        Me.GM_Serach.Size = New System.Drawing.Size(362, 29)
        Me.GM_Serach.TabIndex = 654
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(869, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 21)
        Me.Label4.TabIndex = 653
        Me.Label4.Text = "التصنيف"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(91, 8)
        Me.Label3.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 15)
        Me.Label3.TabIndex = 910
        Me.Label3.Text = "الصفوف المستعرضة"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'textBox_total
        '
        Me.textBox_total.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.textBox_total.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textBox_total.Location = New System.Drawing.Point(3, 2)
        Me.textBox_total.Name = "textBox_total"
        Me.textBox_total.ReadOnly = True
        Me.textBox_total.Size = New System.Drawing.Size(85, 25)
        Me.textBox_total.TabIndex = 909
        '
        'MetroToolTip1
        '
        Me.MetroToolTip1.Style = MetroFramework.MetroColorStyle.Blue
        Me.MetroToolTip1.StyleManager = Nothing
        Me.MetroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'EXCEL_BTN
        '
        Me.EXCEL_BTN.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.EXCEL_BTN.BackColor = System.Drawing.Color.White
        Me.EXCEL_BTN.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EXCEL_BTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EXCEL_BTN.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EXCEL_BTN.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.EXCEL_BTN.Location = New System.Drawing.Point(12, 40)
        Me.EXCEL_BTN.Name = "EXCEL_BTN"
        Me.EXCEL_BTN.Size = New System.Drawing.Size(105, 32)
        Me.EXCEL_BTN.TabIndex = 904
        Me.EXCEL_BTN.Tag = "GENERAL"
        Me.EXCEL_BTN.Text = "EXCEL تصدير"
        Me.EXCEL_BTN.UseVisualStyleBackColor = False
        '
        'Recount_Cost_btn
        '
        Me.Recount_Cost_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Recount_Cost_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Recount_Cost_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Recount_Cost_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Recount_Cost_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Recount_Cost_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Recount_Cost_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Recount_Cost_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Recount_Cost_btn.ForeColor = System.Drawing.Color.Black
        Me.Recount_Cost_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Recount_Cost_btn.Location = New System.Drawing.Point(5, 71)
        Me.Recount_Cost_btn.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Recount_Cost_btn.Name = "Recount_Cost_btn"
        Me.Recount_Cost_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Recount_Cost_btn.Size = New System.Drawing.Size(150, 37)
        Me.Recount_Cost_btn.TabIndex = 688
        Me.Recount_Cost_btn.Text = "تدوير متوسط التكلفة"
        Me.Recount_Cost_btn.UseVisualStyleBackColor = False
        '
        'Up_Update_btn
        '
        Me.Up_Update_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Up_Update_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Up_Update_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Up_Update_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Up_Update_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Up_Update_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Up_Update_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Up_Update_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Up_Update_btn.ForeColor = System.Drawing.Color.Black
        Me.Up_Update_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Up_Update_btn.Location = New System.Drawing.Point(236, 21)
        Me.Up_Update_btn.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Up_Update_btn.Name = "Up_Update_btn"
        Me.Up_Update_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Up_Update_btn.Size = New System.Drawing.Size(97, 37)
        Me.Up_Update_btn.TabIndex = 652
        Me.Up_Update_btn.Text = "تعديــل"
        Me.Up_Update_btn.UseVisualStyleBackColor = False
        Me.Up_Update_btn.Visible = False
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Button1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(120, 40)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Button1.Size = New System.Drawing.Size(118, 32)
        Me.Button1.TabIndex = 390
        Me.Button1.Tag = "GENERAL"
        Me.Button1.Text = "عــرض"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'IM_btn
        '
        Me.IM_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.IM_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.IM_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.IM_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.IM_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.IM_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IM_btn.ForeColor = System.Drawing.Color.Black
        Me.IM_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IM_btn.Location = New System.Drawing.Point(156, 71)
        Me.IM_btn.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.IM_btn.Name = "IM_btn"
        Me.IM_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IM_btn.Size = New System.Drawing.Size(138, 37)
        Me.IM_btn.TabIndex = 389
        Me.IM_btn.Text = "الأصناف"
        Me.IM_btn.UseVisualStyleBackColor = False
        '
        'PrintButton
        '
        Me.PrintButton.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.PrintButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PrintButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PrintButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PrintButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PrintButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.PrintButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.PrintButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PrintButton.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PrintButton.ForeColor = System.Drawing.Color.Black
        Me.PrintButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.PrintButton.Location = New System.Drawing.Point(12, 6)
        Me.PrintButton.Margin = New System.Windows.Forms.Padding(6, 9, 6, 9)
        Me.PrintButton.Name = "PrintButton"
        Me.PrintButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PrintButton.Size = New System.Drawing.Size(105, 30)
        Me.PrintButton.TabIndex = 274
        Me.PrintButton.Tag = "PRINT"
        Me.PrintButton.Text = "طباعة"
        Me.PrintButton.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.EXCEL_BTN)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.GM_Serach)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.ST_cm)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.PrintButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 40)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1012, 81)
        Me.Panel2.TabIndex = 903
        '
        'gridv
        '
        Me.gridv.AllowUserToAddRows = False
        Me.gridv.AllowUserToDeleteRows = False
        Me.gridv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.gridv.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridv.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.gridv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridv.Location = New System.Drawing.Point(3, 161)
        Me.gridv.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.gridv.MultiSelect = False
        Me.gridv.Name = "gridv"
        Me.gridv.ReadOnly = True
        Me.gridv.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.gridv.RowTemplate.Height = 25
        Me.gridv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridv.Size = New System.Drawing.Size(1006, 499)
        Me.gridv.TabIndex = 902
        '
        'TOTAL_Grid
        '
        Me.TOTAL_Grid.AllowUserToAddRows = False
        Me.TOTAL_Grid.AllowUserToDeleteRows = False
        Me.TOTAL_Grid.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TOTAL_Grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.TOTAL_Grid.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.TOTAL_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TOTAL_Grid.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TOTAL_Grid.Location = New System.Drawing.Point(338, 3)
        Me.TOTAL_Grid.MultiSelect = False
        Me.TOTAL_Grid.Name = "TOTAL_Grid"
        Me.TOTAL_Grid.ReadOnly = True
        Me.TOTAL_Grid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TOTAL_Grid.RowHeadersVisible = False
        Me.TOTAL_Grid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.TOTAL_Grid.RowTemplate.Height = 30
        Me.TOTAL_Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.TOTAL_Grid.Size = New System.Drawing.Size(670, 103)
        Me.TOTAL_Grid.TabIndex = 903
        '
        'BarcodeSearch_CB
        '
        Me.BarcodeSearch_CB.AutoSize = True
        Me.BarcodeSearch_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BarcodeSearch_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarcodeSearch_CB.Location = New System.Drawing.Point(259, 8)
        Me.BarcodeSearch_CB.Name = "BarcodeSearch_CB"
        Me.BarcodeSearch_CB.Size = New System.Drawing.Size(70, 21)
        Me.BarcodeSearch_CB.TabIndex = 903
        Me.BarcodeSearch_CB.Text = "بالباركود"
        Me.BarcodeSearch_CB.UseVisualStyleBackColor = True
        '
        'IMNUM_CB
        '
        Me.IMNUM_CB.AutoSize = True
        Me.IMNUM_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMNUM_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMNUM_CB.Location = New System.Drawing.Point(344, 8)
        Me.IMNUM_CB.Name = "IMNUM_CB"
        Me.IMNUM_CB.Size = New System.Drawing.Size(91, 21)
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
        Me.Print_type_Cmb.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.Print_type_Cmb.FormattingEnabled = True
        Me.Print_type_Cmb.IntegralHeight = False
        Me.Print_type_Cmb.Items.AddRange(New Object() {"طباعة بالعرض", "طباعة بالطول"})
        Me.Print_type_Cmb.Location = New System.Drawing.Point(4, 6)
        Me.Print_type_Cmb.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Print_type_Cmb.Name = "Print_type_Cmb"
        Me.Print_type_Cmb.Size = New System.Drawing.Size(134, 25)
        Me.Print_type_Cmb.TabIndex = 906
        '
        'Show_only_Zero_CB
        '
        Me.Show_only_Zero_CB.AutoSize = True
        Me.Show_only_Zero_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Show_only_Zero_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.Show_only_Zero_CB.Location = New System.Drawing.Point(144, 8)
        Me.Show_only_Zero_CB.Name = "Show_only_Zero_CB"
        Me.Show_only_Zero_CB.Size = New System.Drawing.Size(106, 21)
        Me.Show_only_Zero_CB.TabIndex = 913
        Me.Show_only_Zero_CB.Text = "عرض كمية =0"
        Me.Show_only_Zero_CB.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.UcGridColumnsSelector1)
        Me.Panel3.Controls.Add(Me.Show_only_Zero_CB)
        Me.Panel3.Controls.Add(Me.CMSearchTextBox)
        Me.Panel3.Controls.Add(Me.IMNUM_CB)
        Me.Panel3.Controls.Add(Me.BarcodeSearch_CB)
        Me.Panel3.Controls.Add(Me.Print_type_Cmb)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 121)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1012, 38)
        Me.Panel3.TabIndex = 912
        '
        'UcGridColumnsSelector1
        '
        Me.UcGridColumnsSelector1.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcGridColumnsSelector1.Location = New System.Drawing.Point(890, 6)
        Me.UcGridColumnsSelector1.Name = "UcGridColumnsSelector1"
        Me.UcGridColumnsSelector1.PopupMaxHeight = 320
        Me.UcGridColumnsSelector1.PopupMinHeight = 120
        Me.UcGridColumnsSelector1.PopupWidth = 260
        Me.UcGridColumnsSelector1.SettingsFolder = "C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\Grid" &
    "ColumnsSettings"
        Me.UcGridColumnsSelector1.Size = New System.Drawing.Size(111, 27)
        Me.UcGridColumnsSelector1.TabIndex = 914
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TOTAL_Grid)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Recount_Cost_btn)
        Me.Panel1.Controls.Add(Me.Up_Update_btn)
        Me.Panel1.Controls.Add(Me.textBox_total)
        Me.Panel1.Controls.Add(Me.IM_btn)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 663)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1012, 109)
        Me.Panel1.TabIndex = 911
        '
        'STORES_Explorer
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1012, 772)
        Me.Controls.Add(Me.gridv)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
        Me.MinimizeBox = False
        Me.Name = "STORES_Explorer"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "المخازن"
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.gridv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TOTAL_Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
    Friend WithEvents Title_LB As System.Windows.Forms.Label
    Friend WithEvents MinFormButton As System.Windows.Forms.Button
    Friend WithEvents MaxFormButton As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button

    Friend WithEvents PrintButton As System.Windows.Forms.Button
    Friend WithEvents CMSearchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents IM_btn As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ST_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Up_Update_btn As System.Windows.Forms.Button
    Friend WithEvents GM_Serach As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Recount_Cost_btn As System.Windows.Forms.Button
    Friend WithEvents MetroToolTip1 As MetroFramework.Components.MetroToolTip
    Friend WithEvents EXCEL_BTN As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents gridv As DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents textBox_total As System.Windows.Forms.TextBox
    Friend WithEvents IMNUM_CB As System.Windows.Forms.CheckBox
    Friend WithEvents BarcodeSearch_CB As System.Windows.Forms.CheckBox
    Friend WithEvents TOTAL_Grid As DataGridView
    Friend WithEvents Print_type_Cmb As ComboBox
    Friend WithEvents Show_only_Zero_CB As CheckBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents UcGridColumnsSelector1 As UcGridColumnsSelector
    Friend WithEvents Panel1 As Panel
End Class
