<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AG_MV
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AG_MV))
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Filter_By_Date_CB = New System.Windows.Forms.CheckBox()
        Me.ALL_AG_CB = New System.Windows.Forms.CheckBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.AG_MV_GM_CM = New System.Windows.Forms.ComboBox()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage8 = New System.Windows.Forms.TabPage()
        Me.IMMV_DV = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.IMMV_DATE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IMMV_IMNAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IMMV_UNAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_MV_S_QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_MV_T_Price_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage9 = New System.Windows.Forms.TabPage()
        Me.GM_DataGridViewX1 = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.IMMV_Type_cm = New System.Windows.Forms.ComboBox()
        Me.IMMV_Search_txt = New System.Windows.Forms.TextBox()
        Me.GMMV_Print = New System.Windows.Forms.Button()
        Me.Show_IM_btn = New System.Windows.Forms.Button()
        Me.IMMV_Search_btn = New System.Windows.Forms.Button()
        Me.IMMV_Print = New System.Windows.Forms.Button()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.IM_MV_T_Price_txt = New System.Windows.Forms.TextBox()
        Me.IM_MV_T_QTY_txt = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.AG_Cm = New resturant.FSearch_Filter()
        Me.GM_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.S_Total_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabControl2.SuspendLayout()
        Me.TabPage8.SuspendLayout()
        CType(Me.IMMV_DV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage9.SuspendLayout()
        CType(Me.GM_DataGridViewX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        Me.SuspendLayout()
        '
        'Filter_By_Date_CB
        '
        Me.Filter_By_Date_CB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Filter_By_Date_CB.AutoSize = True
        Me.Filter_By_Date_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Filter_By_Date_CB.FlatAppearance.BorderColor = System.Drawing.Color.Lime
        Me.Filter_By_Date_CB.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkGreen
        Me.Filter_By_Date_CB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Filter_By_Date_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Filter_By_Date_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Filter_By_Date_CB.Location = New System.Drawing.Point(557, 49)
        Me.Filter_By_Date_CB.Name = "Filter_By_Date_CB"
        Me.Filter_By_Date_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Filter_By_Date_CB.Size = New System.Drawing.Size(187, 23)
        Me.Filter_By_Date_CB.TabIndex = 678
        Me.Filter_By_Date_CB.Text = "فلترة الأصناف حسب التاريخ"
        Me.Filter_By_Date_CB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Filter_By_Date_CB.UseVisualStyleBackColor = True
        '
        'ALL_AG_CB
        '
        Me.ALL_AG_CB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ALL_AG_CB.AutoSize = True
        Me.ALL_AG_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ALL_AG_CB.FlatAppearance.BorderColor = System.Drawing.Color.Lime
        Me.ALL_AG_CB.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkGreen
        Me.ALL_AG_CB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ALL_AG_CB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ALL_AG_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ALL_AG_CB.Location = New System.Drawing.Point(396, 6)
        Me.ALL_AG_CB.Name = "ALL_AG_CB"
        Me.ALL_AG_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ALL_AG_CB.Size = New System.Drawing.Size(85, 23)
        Me.ALL_AG_CB.TabIndex = 677
        Me.ALL_AG_CB.Text = "كل العملاء"
        Me.ALL_AG_CB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ALL_AG_CB.UseVisualStyleBackColor = True
        '
        'Label43
        '
        Me.Label43.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(487, 52)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(63, 20)
        Me.Label43.TabIndex = 676
        Me.Label43.Text = "التصنيف"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'AG_MV_GM_CM
        '
        Me.AG_MV_GM_CM.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AG_MV_GM_CM.BackColor = System.Drawing.Color.White
        Me.AG_MV_GM_CM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AG_MV_GM_CM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AG_MV_GM_CM.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.AG_MV_GM_CM.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AG_MV_GM_CM.ForeColor = System.Drawing.Color.DarkBlue
        Me.AG_MV_GM_CM.FormattingEnabled = True
        Me.AG_MV_GM_CM.Location = New System.Drawing.Point(267, 49)
        Me.AG_MV_GM_CM.MaxDropDownItems = 15
        Me.AG_MV_GM_CM.Name = "AG_MV_GM_CM"
        Me.AG_MV_GM_CM.Size = New System.Drawing.Size(217, 25)
        Me.AG_MV_GM_CM.TabIndex = 675
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.TabPage8)
        Me.TabControl2.Controls.Add(Me.TabPage9)
        Me.TabControl2.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.TabControl2.Location = New System.Drawing.Point(2, 112)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.RightToLeftLayout = True
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(889, 545)
        Me.TabControl2.TabIndex = 673
        '
        'TabPage8
        '
        Me.TabPage8.Controls.Add(Me.IMMV_DV)
        Me.TabPage8.Location = New System.Drawing.Point(4, 31)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage8.Size = New System.Drawing.Size(881, 510)
        Me.TabPage8.TabIndex = 0
        Me.TabPage8.Text = "الأصناف"
        Me.TabPage8.UseVisualStyleBackColor = True
        '
        'IMMV_DV
        '
        Me.IMMV_DV.AllowUserToAddRows = False
        Me.IMMV_DV.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info
        Me.IMMV_DV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.IMMV_DV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.IMMV_DV.BackgroundColor = System.Drawing.Color.SeaShell
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IMMV_DV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.IMMV_DV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.IMMV_DV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IMMV_DATE_CL, Me.IMMV_IMNAME_CL, Me.IMMV_UNAME_CL, Me.IM_MV_S_QTY_CL, Me.IM_MV_T_Price_CL})
        Me.IMMV_DV.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IMMV_DV.DefaultCellStyle = DataGridViewCellStyle5
        Me.IMMV_DV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IMMV_DV.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.IMMV_DV.Location = New System.Drawing.Point(3, 3)
        Me.IMMV_DV.MultiSelect = False
        Me.IMMV_DV.Name = "IMMV_DV"
        Me.IMMV_DV.ReadOnly = True
        Me.IMMV_DV.RowHeadersVisible = False
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 10.75!)
        Me.IMMV_DV.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.IMMV_DV.RowTemplate.Height = 30
        Me.IMMV_DV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.IMMV_DV.Size = New System.Drawing.Size(875, 504)
        Me.IMMV_DV.TabIndex = 384
        '
        'IMMV_DATE_CL
        '
        Me.IMMV_DATE_CL.DataPropertyName = "DATE"
        Me.IMMV_DATE_CL.HeaderText = "تاريخ"
        Me.IMMV_DATE_CL.Name = "IMMV_DATE_CL"
        Me.IMMV_DATE_CL.ReadOnly = True
        Me.IMMV_DATE_CL.Visible = False
        '
        'IMMV_IMNAME_CL
        '
        Me.IMMV_IMNAME_CL.DataPropertyName = "Item_Name"
        Me.IMMV_IMNAME_CL.FillWeight = 53.27744!
        Me.IMMV_IMNAME_CL.HeaderText = "الصنف"
        Me.IMMV_IMNAME_CL.MinimumWidth = 250
        Me.IMMV_IMNAME_CL.Name = "IMMV_IMNAME_CL"
        Me.IMMV_IMNAME_CL.ReadOnly = True
        '
        'IMMV_UNAME_CL
        '
        Me.IMMV_UNAME_CL.DataPropertyName = "U_Name"
        Me.IMMV_UNAME_CL.FillWeight = 90.0!
        Me.IMMV_UNAME_CL.HeaderText = "الوحدة"
        Me.IMMV_UNAME_CL.MinimumWidth = 90
        Me.IMMV_UNAME_CL.Name = "IMMV_UNAME_CL"
        Me.IMMV_UNAME_CL.ReadOnly = True
        '
        'IM_MV_S_QTY_CL
        '
        Me.IM_MV_S_QTY_CL.DataPropertyName = "S_QTY"
        DataGridViewCellStyle3.Format = "N2"
        Me.IM_MV_S_QTY_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.IM_MV_S_QTY_CL.FillWeight = 80.0!
        Me.IM_MV_S_QTY_CL.HeaderText = "الكمية"
        Me.IM_MV_S_QTY_CL.MinimumWidth = 80
        Me.IM_MV_S_QTY_CL.Name = "IM_MV_S_QTY_CL"
        Me.IM_MV_S_QTY_CL.ReadOnly = True
        '
        'IM_MV_T_Price_CL
        '
        Me.IM_MV_T_Price_CL.DataPropertyName = "S_Total"
        DataGridViewCellStyle4.Format = "N2"
        Me.IM_MV_T_Price_CL.DefaultCellStyle = DataGridViewCellStyle4
        Me.IM_MV_T_Price_CL.FillWeight = 90.0!
        Me.IM_MV_T_Price_CL.HeaderText = "الإجمالي"
        Me.IM_MV_T_Price_CL.MinimumWidth = 90
        Me.IM_MV_T_Price_CL.Name = "IM_MV_T_Price_CL"
        Me.IM_MV_T_Price_CL.ReadOnly = True
        '
        'TabPage9
        '
        Me.TabPage9.Controls.Add(Me.GM_DataGridViewX1)
        Me.TabPage9.Location = New System.Drawing.Point(4, 31)
        Me.TabPage9.Name = "TabPage9"
        Me.TabPage9.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage9.Size = New System.Drawing.Size(881, 510)
        Me.TabPage9.TabIndex = 1
        Me.TabPage9.Text = "التصنيفات"
        Me.TabPage9.UseVisualStyleBackColor = True
        '
        'GM_DataGridViewX1
        '
        Me.GM_DataGridViewX1.AllowUserToAddRows = False
        Me.GM_DataGridViewX1.AllowUserToDeleteRows = False
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Info
        Me.GM_DataGridViewX1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        Me.GM_DataGridViewX1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.GM_DataGridViewX1.BackgroundColor = System.Drawing.Color.SeaShell
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GM_DataGridViewX1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.GM_DataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GM_DataGridViewX1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GM_Name_CL, Me.QTY_CL, Me.S_Total_CL})
        Me.GM_DataGridViewX1.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GM_DataGridViewX1.DefaultCellStyle = DataGridViewCellStyle10
        Me.GM_DataGridViewX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GM_DataGridViewX1.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.GM_DataGridViewX1.Location = New System.Drawing.Point(3, 3)
        Me.GM_DataGridViewX1.MultiSelect = False
        Me.GM_DataGridViewX1.Name = "GM_DataGridViewX1"
        Me.GM_DataGridViewX1.ReadOnly = True
        Me.GM_DataGridViewX1.RowHeadersVisible = False
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Segoe UI", 10.75!)
        Me.GM_DataGridViewX1.RowsDefaultCellStyle = DataGridViewCellStyle11
        Me.GM_DataGridViewX1.RowTemplate.Height = 35
        Me.GM_DataGridViewX1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GM_DataGridViewX1.Size = New System.Drawing.Size(875, 504)
        Me.GM_DataGridViewX1.TabIndex = 385
        '
        'Label41
        '
        Me.Label41.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(337, 11)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(48, 19)
        Me.Label41.TabIndex = 669
        Me.Label41.Text = "العميل"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label37
        '
        Me.Label37.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(192, 52)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(69, 19)
        Me.Label37.TabIndex = 668
        Me.Label37.Text = "نوع الحركة"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'IMMV_Type_cm
        '
        Me.IMMV_Type_cm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IMMV_Type_cm.BackColor = System.Drawing.SystemColors.Info
        Me.IMMV_Type_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMMV_Type_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.IMMV_Type_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IMMV_Type_cm.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.IMMV_Type_cm.FormattingEnabled = True
        Me.IMMV_Type_cm.Location = New System.Drawing.Point(3, 47)
        Me.IMMV_Type_cm.Name = "IMMV_Type_cm"
        Me.IMMV_Type_cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IMMV_Type_cm.Size = New System.Drawing.Size(185, 28)
        Me.IMMV_Type_cm.TabIndex = 667
        '
        'IMMV_Search_txt
        '
        Me.IMMV_Search_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IMMV_Search_txt.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMMV_Search_txt.ForeColor = System.Drawing.SystemColors.InfoText
        Me.IMMV_Search_txt.Location = New System.Drawing.Point(2, 77)
        Me.IMMV_Search_txt.Name = "IMMV_Search_txt"
        Me.IMMV_Search_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IMMV_Search_txt.Size = New System.Drawing.Size(889, 29)
        Me.IMMV_Search_txt.TabIndex = 665
        Me.IMMV_Search_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GMMV_Print
        '
        Me.GMMV_Print.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GMMV_Print.BackColor = System.Drawing.Color.White
        Me.GMMV_Print.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GMMV_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GMMV_Print.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.GMMV_Print.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.GMMV_Print.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.GMMV_Print.Location = New System.Drawing.Point(485, 2)
        Me.GMMV_Print.Name = "GMMV_Print"
        Me.GMMV_Print.Size = New System.Drawing.Size(141, 38)
        Me.GMMV_Print.TabIndex = 674
        Me.GMMV_Print.Text = "طباعة التصنيفات"
        Me.GMMV_Print.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.GMMV_Print.UseVisualStyleBackColor = False
        '
        'Show_IM_btn
        '
        Me.Show_IM_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Show_IM_btn.BackColor = System.Drawing.Color.White
        Me.Show_IM_btn.BackgroundImage = Global.resturant.My.Resources.Resources.if_download_173000
        Me.Show_IM_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Show_IM_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Show_IM_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Show_IM_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Show_IM_btn.Location = New System.Drawing.Point(-123, 12)
        Me.Show_IM_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Show_IM_btn.Name = "Show_IM_btn"
        Me.Show_IM_btn.Size = New System.Drawing.Size(46, 27)
        Me.Show_IM_btn.TabIndex = 672
        Me.Show_IM_btn.UseVisualStyleBackColor = False
        '
        'IMMV_Search_btn
        '
        Me.IMMV_Search_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IMMV_Search_btn.BackColor = System.Drawing.Color.White
        Me.IMMV_Search_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.IMMV_Search_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMMV_Search_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IMMV_Search_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.IMMV_Search_btn.ForeColor = System.Drawing.Color.Black
        Me.IMMV_Search_btn.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.IMMV_Search_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IMMV_Search_btn.Location = New System.Drawing.Point(769, 2)
        Me.IMMV_Search_btn.Name = "IMMV_Search_btn"
        Me.IMMV_Search_btn.Size = New System.Drawing.Size(125, 38)
        Me.IMMV_Search_btn.TabIndex = 664
        Me.IMMV_Search_btn.Text = "بحث"
        Me.IMMV_Search_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.IMMV_Search_btn.UseVisualStyleBackColor = False
        '
        'IMMV_Print
        '
        Me.IMMV_Print.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IMMV_Print.BackColor = System.Drawing.Color.White
        Me.IMMV_Print.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMMV_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IMMV_Print.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.IMMV_Print.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.IMMV_Print.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IMMV_Print.Location = New System.Drawing.Point(627, 2)
        Me.IMMV_Print.Name = "IMMV_Print"
        Me.IMMV_Print.Size = New System.Drawing.Size(141, 38)
        Me.IMMV_Print.TabIndex = 666
        Me.IMMV_Print.Text = "طباعة الأصناف"
        Me.IMMV_Print.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.IMMV_Print.UseVisualStyleBackColor = False
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.IM_MV_T_Price_txt)
        Me.Panel8.Controls.Add(Me.IM_MV_T_QTY_txt)
        Me.Panel8.Controls.Add(Me.Label36)
        Me.Panel8.Controls.Add(Me.Label35)
        Me.Panel8.Location = New System.Drawing.Point(12, 699)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(991, 38)
        Me.Panel8.TabIndex = 679
        '
        'IM_MV_T_Price_txt
        '
        Me.IM_MV_T_Price_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.IM_MV_T_Price_txt.Font = New System.Drawing.Font("Stencil", 14.75!)
        Me.IM_MV_T_Price_txt.ForeColor = System.Drawing.Color.Red
        Me.IM_MV_T_Price_txt.Location = New System.Drawing.Point(116, 4)
        Me.IM_MV_T_Price_txt.Name = "IM_MV_T_Price_txt"
        Me.IM_MV_T_Price_txt.ReadOnly = True
        Me.IM_MV_T_Price_txt.Size = New System.Drawing.Size(150, 31)
        Me.IM_MV_T_Price_txt.TabIndex = 551
        Me.IM_MV_T_Price_txt.Text = "0.00"
        Me.IM_MV_T_Price_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'IM_MV_T_QTY_txt
        '
        Me.IM_MV_T_QTY_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.IM_MV_T_QTY_txt.Font = New System.Drawing.Font("Arial", 15.0!)
        Me.IM_MV_T_QTY_txt.ForeColor = System.Drawing.Color.Red
        Me.IM_MV_T_QTY_txt.Location = New System.Drawing.Point(408, 4)
        Me.IM_MV_T_QTY_txt.Name = "IM_MV_T_QTY_txt"
        Me.IM_MV_T_QTY_txt.ReadOnly = True
        Me.IM_MV_T_QTY_txt.Size = New System.Drawing.Size(150, 30)
        Me.IM_MV_T_QTY_txt.TabIndex = 547
        Me.IM_MV_T_QTY_txt.Text = "0.00"
        Me.IM_MV_T_QTY_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label36
        '
        Me.Label36.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(561, 9)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(108, 21)
        Me.Label36.TabIndex = 548
        Me.Label36.Text = "إجمالي الكميات"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label35
        '
        Me.Label35.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(269, 9)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(96, 21)
        Me.Label35.TabIndex = 552
        Me.Label35.Text = "إجمالي السعر"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AG_Cm
        '
        Me.AG_Cm.CancelSearchImage = CType(resources.GetObject("AG_Cm.CancelSearchImage"), System.Drawing.Image)
        Me.AG_Cm.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AG_Cm.Location = New System.Drawing.Point(3, 3)
        Me.AG_Cm.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.AG_Cm.Name = "AG_Cm"
        Me.AG_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AG_Cm.Size = New System.Drawing.Size(331, 35)
        Me.AG_Cm.SQL_Column = "Ag_name"
        Me.AG_Cm.SQL_ID = "AG_ID"
        Me.AG_Cm.SQL_IsNumericSearchField = False
        Me.AG_Cm.SQL_ListSize = 200
        Me.AG_Cm.SQL_NumberOfRows = 200
        Me.AG_Cm.SQL_OrderByField = "AG_NAME"
        Me.AG_Cm.SQL_SearchField = "AG_NAME"
        Me.AG_Cm.SQL_Table = "AGENTS_MENU_V"
        Me.AG_Cm.TabIndex = 709
        Me.AG_Cm.TextMaxLength = 250
        Me.AG_Cm.Textt = ""
        '
        'GM_Name_CL
        '
        Me.GM_Name_CL.DataPropertyName = "GM_Name"
        Me.GM_Name_CL.FillWeight = 93.58553!
        Me.GM_Name_CL.HeaderText = "التصنيـــف"
        Me.GM_Name_CL.MinimumWidth = 150
        Me.GM_Name_CL.Name = "GM_Name_CL"
        Me.GM_Name_CL.ReadOnly = True
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "S_QTY"
        Me.QTY_CL.FillWeight = 93.18955!
        Me.QTY_CL.HeaderText = "العدد"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'S_Total_CL
        '
        Me.S_Total_CL.DataPropertyName = "S_Total"
        DataGridViewCellStyle9.Format = "N2"
        Me.S_Total_CL.DefaultCellStyle = DataGridViewCellStyle9
        Me.S_Total_CL.FillWeight = 50.61002!
        Me.S_Total_CL.HeaderText = "الإجمالي"
        Me.S_Total_CL.Name = "S_Total_CL"
        Me.S_Total_CL.ReadOnly = True
        '
        'AG_MV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(895, 659)
        Me.ControlBox = False
        Me.Controls.Add(Me.AG_Cm)
        Me.Controls.Add(Me.IMMV_Search_btn)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Filter_By_Date_CB)
        Me.Controls.Add(Me.ALL_AG_CB)
        Me.Controls.Add(Me.Label43)
        Me.Controls.Add(Me.AG_MV_GM_CM)
        Me.Controls.Add(Me.TabControl2)
        Me.Controls.Add(Me.Label41)
        Me.Controls.Add(Me.Label37)
        Me.Controls.Add(Me.IMMV_Type_cm)
        Me.Controls.Add(Me.IMMV_Search_txt)
        Me.Controls.Add(Me.GMMV_Print)
        Me.Controls.Add(Me.Show_IM_btn)
        Me.Controls.Add(Me.IMMV_Print)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AG_MV"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage8.ResumeLayout(False)
        CType(Me.IMMV_DV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage9.ResumeLayout(False)
        CType(Me.GM_DataGridViewX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Filter_By_Date_CB As System.Windows.Forms.CheckBox
    Friend WithEvents ALL_AG_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents AG_MV_GM_CM As System.Windows.Forms.ComboBox
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage8 As System.Windows.Forms.TabPage
    Public WithEvents IMMV_DV As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents IMMV_DATE_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IMMV_IMNAME_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IMMV_UNAME_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_MV_S_QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_MV_T_Price_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabPage9 As System.Windows.Forms.TabPage
    Public WithEvents GM_DataGridViewX1 As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents IMMV_Type_cm As System.Windows.Forms.ComboBox
    Friend WithEvents IMMV_Search_txt As System.Windows.Forms.TextBox
    Friend WithEvents GMMV_Print As System.Windows.Forms.Button
    Friend WithEvents Show_IM_btn As System.Windows.Forms.Button
    Friend WithEvents IMMV_Search_btn As System.Windows.Forms.Button
    Friend WithEvents IMMV_Print As System.Windows.Forms.Button
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents IM_MV_T_Price_txt As System.Windows.Forms.TextBox
    Friend WithEvents IM_MV_T_QTY_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents AG_Cm As resturant.FSearch_Filter
    Friend WithEvents GM_Name_CL As DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As DataGridViewTextBoxColumn
    Friend WithEvents S_Total_CL As DataGridViewTextBoxColumn
End Class
