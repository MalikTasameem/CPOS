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
        Me.Button2 = New System.Windows.Forms.Button()
        Me.EXCEL_BTN = New System.Windows.Forms.Button()
        Me.Recount_Cost_btn = New System.Windows.Forms.Button()
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
        Me.advancedDataGridViewSearchToolBar_main = New Zuby.ADGV.AdvancedDataGridViewSearchToolBar()
        Me.BarcodeSearch_CB = New System.Windows.Forms.CheckBox()
        Me.IMNUM_CB = New System.Windows.Forms.CheckBox()
        Me.Print_type_Cmb = New System.Windows.Forms.ComboBox()
        Me.Show_only_Zero_CB = New System.Windows.Forms.CheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.TitleBar_Panel.SuspendLayout()
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.gridv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grid_panel.SuspendLayout()
        CType(Me.TOTAL_Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
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
        Me.TitleBar_Panel.Size = New System.Drawing.Size(1064, 40)
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
        Me.Title_LB.Location = New System.Drawing.Point(920, 9)
        Me.Title_LB.Name = "Title_LB"
        Me.Title_LB.Size = New System.Drawing.Size(108, 21)
        Me.Title_LB.TabIndex = 0
        Me.Title_LB.Tag = "TITLE_TRANSPARENT"
        Me.Title_LB.Text = "كشف المخــــزن"
        Me.Title_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CMSearchTextBox
        '
        Me.CMSearchTextBox.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.CMSearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CMSearchTextBox.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMSearchTextBox.ForeColor = System.Drawing.SystemColors.InfoText
        Me.CMSearchTextBox.Location = New System.Drawing.Point(685, 39)
        Me.CMSearchTextBox.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
        Me.CMSearchTextBox.Name = "CMSearchTextBox"
        Me.CMSearchTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CMSearchTextBox.Size = New System.Drawing.Size(359, 25)
        Me.CMSearchTextBox.TabIndex = 272
        Me.CMSearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(329, 12)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 19)
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
        Me.ST_cm.Font = New System.Drawing.Font("Arial", 12.25!)
        Me.ST_cm.FormattingEnabled = True
        Me.ST_cm.Location = New System.Drawing.Point(249, 34)
        Me.ST_cm.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ST_cm.Name = "ST_cm"
        Me.ST_cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ST_cm.Size = New System.Drawing.Size(181, 26)
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
        Me.GM_Serach.Font = New System.Drawing.Font("Arial", 12.25!)
        Me.GM_Serach.FormattingEnabled = True
        Me.GM_Serach.IntegralHeight = False
        Me.GM_Serach.Items.AddRange(New Object() {"قصيرة", "طويلة"})
        Me.GM_Serach.Location = New System.Drawing.Point(449, 34)
        Me.GM_Serach.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.GM_Serach.Name = "GM_Serach"
        Me.GM_Serach.Size = New System.Drawing.Size(177, 26)
        Me.GM_Serach.TabIndex = 654
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(510, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 19)
        Me.Label4.TabIndex = 653
        Me.Label4.Text = "التصنيف"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(167, 468)
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
        Me.textBox_total.Location = New System.Drawing.Point(12, 466)
        Me.textBox_total.Name = "textBox_total"
        Me.textBox_total.ReadOnly = True
        Me.textBox_total.Size = New System.Drawing.Size(147, 25)
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
        Me.Button2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Button2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Arial", 16.0!, System.Drawing.FontStyle.Bold)
        Me.Button2.ForeColor = System.Drawing.Color.Black
        Me.Button2.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.Button2.Location = New System.Drawing.Point(651, 2)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Button2.Size = New System.Drawing.Size(41, 70)
        Me.Button2.TabIndex = 905
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.MetroToolTip1.SetToolTip(Me.Button2, "حفظ العرض")
        Me.Button2.UseVisualStyleBackColor = False
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
        Me.Recount_Cost_btn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Recount_Cost_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Recount_Cost_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Recount_Cost_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Recount_Cost_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Recount_Cost_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Recount_Cost_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Recount_Cost_btn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Recount_Cost_btn.ForeColor = System.Drawing.Color.Black
        Me.Recount_Cost_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Recount_Cost_btn.Location = New System.Drawing.Point(12, 507)
        Me.Recount_Cost_btn.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Recount_Cost_btn.Name = "Recount_Cost_btn"
        Me.Recount_Cost_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Recount_Cost_btn.Size = New System.Drawing.Size(147, 37)
        Me.Recount_Cost_btn.TabIndex = 688
        Me.Recount_Cost_btn.Text = "تدوير متوسط التكلفة"
        Me.Recount_Cost_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Recount_Cost_btn.UseVisualStyleBackColor = False
        '
        'Up_Update_btn
        '
        Me.Up_Update_btn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Up_Update_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Up_Update_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Up_Update_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Up_Update_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Up_Update_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Up_Update_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Up_Update_btn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Up_Update_btn.ForeColor = System.Drawing.Color.Black
        Me.Up_Update_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Up_Update_btn.Location = New System.Drawing.Point(259, 507)
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
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Button1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(125, 6)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Button1.Size = New System.Drawing.Size(106, 30)
        Me.Button1.TabIndex = 390
        Me.Button1.Tag = "GENERAL"
        Me.Button1.Text = "عــرض"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'IM_btn
        '
        Me.IM_btn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.IM_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.IM_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.IM_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.IM_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.IM_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_btn.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_btn.ForeColor = System.Drawing.Color.Black
        Me.IM_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IM_btn.Location = New System.Drawing.Point(160, 507)
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
        'CheckedListBox1
        '
        Me.CheckedListBox1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.CheckedListBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.HorizontalScrollbar = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(697, 0)
        Me.CheckedListBox1.MultiColumn = True
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CheckedListBox1.Size = New System.Drawing.Size(362, 79)
        Me.CheckedListBox1.TabIndex = 901
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Button2)
        Me.Panel2.Controls.Add(Me.EXCEL_BTN)
        Me.Panel2.Controls.Add(Me.CheckedListBox1)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.GM_Serach)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.ST_cm)
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.PrintButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 40)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1064, 81)
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
        Me.gridv.Location = New System.Drawing.Point(2, 4)
        Me.gridv.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.gridv.MultiSelect = False
        Me.gridv.Name = "gridv"
        Me.gridv.ReadOnly = True
        Me.gridv.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.gridv.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gridv.RowTemplate.Height = 25
        Me.gridv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridv.Size = New System.Drawing.Size(1053, 428)
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
        Me.grid_panel.Controls.Add(Me.Recount_Cost_btn)
        Me.grid_panel.Controls.Add(Me.Up_Update_btn)
        Me.grid_panel.Controls.Add(Me.Label3)
        Me.grid_panel.Controls.Add(Me.IM_btn)
        Me.grid_panel.Controls.Add(Me.textBox_total)
        Me.grid_panel.Controls.Add(Me.TOTAL_Grid)
        Me.grid_panel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grid_panel.Font = New System.Drawing.Font("Arial", 10.75!)
        Me.grid_panel.Location = New System.Drawing.Point(0, 220)
        Me.grid_panel.Name = "grid_panel"
        Me.grid_panel.Size = New System.Drawing.Size(1064, 552)
        Me.grid_panel.TabIndex = 903
        '
        'TOTAL_Grid
        '
        Me.TOTAL_Grid.AllowUserToAddRows = False
        Me.TOTAL_Grid.AllowUserToDeleteRows = False
        Me.TOTAL_Grid.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TOTAL_Grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.TOTAL_Grid.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.TOTAL_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TOTAL_Grid.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TOTAL_Grid.Location = New System.Drawing.Point(361, 439)
        Me.TOTAL_Grid.MultiSelect = False
        Me.TOTAL_Grid.Name = "TOTAL_Grid"
        Me.TOTAL_Grid.ReadOnly = True
        Me.TOTAL_Grid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TOTAL_Grid.RowHeadersVisible = False
        Me.TOTAL_Grid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.TOTAL_Grid.RowTemplate.Height = 30
        Me.TOTAL_Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.TOTAL_Grid.Size = New System.Drawing.Size(694, 105)
        Me.TOTAL_Grid.TabIndex = 903
        '
        'advancedDataGridViewSearchToolBar_main
        '
        Me.advancedDataGridViewSearchToolBar_main.AllowMerge = False
        Me.advancedDataGridViewSearchToolBar_main.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.advancedDataGridViewSearchToolBar_main.AutoSize = False
        Me.advancedDataGridViewSearchToolBar_main.Dock = System.Windows.Forms.DockStyle.None
        Me.advancedDataGridViewSearchToolBar_main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.advancedDataGridViewSearchToolBar_main.Location = New System.Drawing.Point(16, 7)
        Me.advancedDataGridViewSearchToolBar_main.MaximumSize = New System.Drawing.Size(0, 20)
        Me.advancedDataGridViewSearchToolBar_main.MinimumSize = New System.Drawing.Size(0, 20)
        Me.advancedDataGridViewSearchToolBar_main.Name = "advancedDataGridViewSearchToolBar_main"
        Me.advancedDataGridViewSearchToolBar_main.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.advancedDataGridViewSearchToolBar_main.Size = New System.Drawing.Size(1028, 20)
        Me.advancedDataGridViewSearchToolBar_main.TabIndex = 909
        Me.advancedDataGridViewSearchToolBar_main.Text = "AdvancedDataGridViewSearchToolBar1"
        '
        'BarcodeSearch_CB
        '
        Me.BarcodeSearch_CB.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.BarcodeSearch_CB.AutoSize = True
        Me.BarcodeSearch_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BarcodeSearch_CB.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.BarcodeSearch_CB.Location = New System.Drawing.Point(374, 42)
        Me.BarcodeSearch_CB.Name = "BarcodeSearch_CB"
        Me.BarcodeSearch_CB.Size = New System.Drawing.Size(70, 22)
        Me.BarcodeSearch_CB.TabIndex = 903
        Me.BarcodeSearch_CB.Text = "بالباركود"
        Me.BarcodeSearch_CB.UseVisualStyleBackColor = True
        '
        'IMNUM_CB
        '
        Me.IMNUM_CB.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.IMNUM_CB.AutoSize = True
        Me.IMNUM_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMNUM_CB.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.IMNUM_CB.Location = New System.Drawing.Point(497, 42)
        Me.IMNUM_CB.Name = "IMNUM_CB"
        Me.IMNUM_CB.Size = New System.Drawing.Size(86, 22)
        Me.IMNUM_CB.TabIndex = 904
        Me.IMNUM_CB.Text = "برقم الصنف"
        Me.IMNUM_CB.UseVisualStyleBackColor = True
        '
        'Print_type_Cmb
        '
        Me.Print_type_Cmb.Anchor = System.Windows.Forms.AnchorStyles.Right
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
        Me.Print_type_Cmb.Location = New System.Drawing.Point(14, 38)
        Me.Print_type_Cmb.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Print_type_Cmb.Name = "Print_type_Cmb"
        Me.Print_type_Cmb.Size = New System.Drawing.Size(147, 26)
        Me.Print_type_Cmb.TabIndex = 906
        '
        'Show_only_Zero_CB
        '
        Me.Show_only_Zero_CB.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Show_only_Zero_CB.AutoSize = True
        Me.Show_only_Zero_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Show_only_Zero_CB.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Show_only_Zero_CB.Location = New System.Drawing.Point(175, 41)
        Me.Show_only_Zero_CB.Name = "Show_only_Zero_CB"
        Me.Show_only_Zero_CB.Size = New System.Drawing.Size(103, 22)
        Me.Show_only_Zero_CB.TabIndex = 913
        Me.Show_only_Zero_CB.Text = "عرض كمية =0"
        Me.Show_only_Zero_CB.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Show_only_Zero_CB)
        Me.Panel3.Controls.Add(Me.advancedDataGridViewSearchToolBar_main)
        Me.Panel3.Controls.Add(Me.CMSearchTextBox)
        Me.Panel3.Controls.Add(Me.IMNUM_CB)
        Me.Panel3.Controls.Add(Me.BarcodeSearch_CB)
        Me.Panel3.Controls.Add(Me.Print_type_Cmb)
        Me.Panel3.Location = New System.Drawing.Point(5, 129)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1057, 75)
        Me.Panel3.TabIndex = 912
        '
        'STORES_Explorer
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1064, 772)
        Me.Controls.Add(Me.grid_panel)
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
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.gridv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grid_panel.ResumeLayout(False)
        Me.grid_panel.PerformLayout()
        CType(Me.TOTAL_Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
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
    Friend WithEvents advancedDataGridViewSearchToolBar_main As Zuby.ADGV.AdvancedDataGridViewSearchToolBar
    Friend WithEvents IMNUM_CB As System.Windows.Forms.CheckBox
    Friend WithEvents BarcodeSearch_CB As System.Windows.Forms.CheckBox
    Friend WithEvents TOTAL_Grid As DataGridView
    Friend WithEvents Print_type_Cmb As ComboBox
    Friend WithEvents Show_only_Zero_CB As CheckBox
    Friend WithEvents Panel3 As Panel
End Class