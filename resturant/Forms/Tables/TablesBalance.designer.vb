<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TablesBalance
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TablesBalance))
        Me.F_Panel = New System.Windows.Forms.Panel()
        Me.IMGrid = New MetroFramework.Controls.MetroGrid()
        Me.IM_T_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unit_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_NameCL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unit_Price_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PurePanel = New System.Windows.Forms.Panel()
        Me.Show_AllBill_Clmns_CB = New System.Windows.Forms.CheckBox()
        Me.Total_Label = New System.Windows.Forms.Label()
        Me.isPrintBeforeEndBill_CB = New System.Windows.Forms.CheckBox()
        Me.TB_Info_LB = New System.Windows.Forms.Label()
        Me.PureLabel = New System.Windows.Forms.Label()
        Me.PureTextBox = New System.Windows.Forms.TextBox()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.TB_Info = New System.Windows.Forms.Label()
        Me.BillsMetroGrid = New MetroFramework.Controls.MetroGrid()
        Me.Column1 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Edit_TB_IMQty_CL = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Bills_T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.B_D_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.B_Pr_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AG_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Date_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AG_Name_Bill = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total_TB_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Discount_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pure_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.User_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.B_Type_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Grids_Panel = New System.Windows.Forms.Panel()
        Me.Items_btn = New System.Windows.Forms.Button()
        Me.Bills_btn = New System.Windows.Forms.Button()
        Me.TB_Transfer_Panel = New System.Windows.Forms.Panel()
        Me.ClearNumBtn = New System.Windows.Forms.Button()
        Me.TB_F_txt = New System.Windows.Forms.TextBox()
        Me.TranConfirm_btn = New System.Windows.Forms.Button()
        Me.TB_T_txt = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Apart_List_btn = New System.Windows.Forms.Button()
        Me.Debit_Table_btn = New System.Windows.Forms.Button()
        Me.Button51 = New System.Windows.Forms.Button()
        Me.Button50 = New System.Windows.Forms.Button()
        Me.PiedApart_btn = New System.Windows.Forms.Button()
        Me.TB_ButOnAG_btn = New System.Windows.Forms.Button()
        Me.PrintBillButton = New System.Windows.Forms.Button()
        Me.Refrech_btn = New System.Windows.Forms.Button()
        Me.TB_Types_CMB = New System.Windows.Forms.ComboBox()
        Me.Tables_Option_GB = New System.Windows.Forms.GroupBox()
        Me.Time_Table_Label = New System.Windows.Forms.Label()
        Me.PanelRight = New System.Windows.Forms.Panel()
        CType(Me.IMGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PurePanel.SuspendLayout()
        CType(Me.BillsMetroGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Grids_Panel.SuspendLayout()
        Me.TB_Transfer_Panel.SuspendLayout()
        Me.Tables_Option_GB.SuspendLayout()
        Me.PanelRight.SuspendLayout()
        Me.SuspendLayout()
        '
        'F_Panel
        '
        Me.F_Panel.AutoScroll = True
        Me.F_Panel.Location = New System.Drawing.Point(2, 42)
        Me.F_Panel.Name = "F_Panel"
        Me.F_Panel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.F_Panel.Size = New System.Drawing.Size(458, 436)
        Me.F_Panel.TabIndex = 0
        '
        'IMGrid
        '
        Me.IMGrid.AllowUserToAddRows = False
        Me.IMGrid.AllowUserToDeleteRows = False
        Me.IMGrid.AllowUserToResizeRows = False
        Me.IMGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.IMGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.IMGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.IMGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.IMGrid.CausesValidation = False
        Me.IMGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.IMGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IMGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.IMGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.IMGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IM_T_ID, Me.QTY_CL, Me.Unit_CL, Me.IM_NameCL, Me.Unit_Price_CL, Me.Total_CL})
        Me.IMGrid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IMGrid.DefaultCellStyle = DataGridViewCellStyle5
        Me.IMGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.IMGrid.EnableHeadersVisualStyles = False
        Me.IMGrid.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.IMGrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.IMGrid.Location = New System.Drawing.Point(3, 2)
        Me.IMGrid.MultiSelect = False
        Me.IMGrid.Name = "IMGrid"
        Me.IMGrid.ReadOnly = True
        Me.IMGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IMGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IMGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.IMGrid.RowHeadersVisible = False
        Me.IMGrid.RowHeadersWidth = 100
        Me.IMGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Segoe UI", 10.75!)
        Me.IMGrid.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.IMGrid.RowTemplate.Height = 150
        Me.IMGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IMGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.IMGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.IMGrid.Size = New System.Drawing.Size(349, 347)
        Me.IMGrid.TabIndex = 562
        Me.IMGrid.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'IM_T_ID
        '
        Me.IM_T_ID.DataPropertyName = "T_ID"
        Me.IM_T_ID.HeaderText = "ر.الآلي"
        Me.IM_T_ID.Name = "IM_T_ID"
        Me.IM_T_ID.ReadOnly = True
        Me.IM_T_ID.Visible = False
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        DataGridViewCellStyle2.Format = "N1"
        Me.QTY_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.QTY_CL.FillWeight = 79.91894!
        Me.QTY_CL.HeaderText = "الكمية"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'Unit_CL
        '
        Me.Unit_CL.DataPropertyName = "Unit_Name"
        Me.Unit_CL.HeaderText = "الوحدة"
        Me.Unit_CL.Name = "Unit_CL"
        Me.Unit_CL.ReadOnly = True
        Me.Unit_CL.Visible = False
        '
        'IM_NameCL
        '
        Me.IM_NameCL.DataPropertyName = "IM_Name"
        Me.IM_NameCL.FillWeight = 170.7813!
        Me.IM_NameCL.HeaderText = "الصنف"
        Me.IM_NameCL.Name = "IM_NameCL"
        Me.IM_NameCL.ReadOnly = True
        '
        'Unit_Price_CL
        '
        Me.Unit_Price_CL.DataPropertyName = "Price"
        DataGridViewCellStyle3.Format = "N2"
        Me.Unit_Price_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.Unit_Price_CL.HeaderText = "السعر"
        Me.Unit_Price_CL.Name = "Unit_Price_CL"
        Me.Unit_Price_CL.ReadOnly = True
        Me.Unit_Price_CL.ToolTipText = "السعر"
        '
        'Total_CL
        '
        Me.Total_CL.DataPropertyName = "T_Price"
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.Total_CL.DefaultCellStyle = DataGridViewCellStyle4
        Me.Total_CL.FillWeight = 79.91894!
        Me.Total_CL.HeaderText = "الإجمالي"
        Me.Total_CL.Name = "Total_CL"
        Me.Total_CL.ReadOnly = True
        '
        'PurePanel
        '
        Me.PurePanel.Controls.Add(Me.Show_AllBill_Clmns_CB)
        Me.PurePanel.Controls.Add(Me.Total_Label)
        Me.PurePanel.Controls.Add(Me.isPrintBeforeEndBill_CB)
        Me.PurePanel.Controls.Add(Me.TB_Info_LB)
        Me.PurePanel.Controls.Add(Me.PureLabel)
        Me.PurePanel.Controls.Add(Me.PureTextBox)
        Me.PurePanel.Location = New System.Drawing.Point(3, 399)
        Me.PurePanel.Name = "PurePanel"
        Me.PurePanel.Size = New System.Drawing.Size(355, 84)
        Me.PurePanel.TabIndex = 565
        '
        'Show_AllBill_Clmns_CB
        '
        Me.Show_AllBill_Clmns_CB.AutoSize = True
        Me.Show_AllBill_Clmns_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Show_AllBill_Clmns_CB.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Show_AllBill_Clmns_CB.Location = New System.Drawing.Point(235, 28)
        Me.Show_AllBill_Clmns_CB.Name = "Show_AllBill_Clmns_CB"
        Me.Show_AllBill_Clmns_CB.Size = New System.Drawing.Size(116, 18)
        Me.Show_AllBill_Clmns_CB.TabIndex = 585
        Me.Show_AllBill_Clmns_CB.Text = "كل تفاصيل الفاتورة"
        Me.Show_AllBill_Clmns_CB.UseVisualStyleBackColor = True
        '
        'Total_Label
        '
        Me.Total_Label.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Total_Label.AutoSize = True
        Me.Total_Label.BackColor = System.Drawing.Color.Transparent
        Me.Total_Label.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Total_Label.ForeColor = System.Drawing.Color.Blue
        Me.Total_Label.Location = New System.Drawing.Point(7, 28)
        Me.Total_Label.Name = "Total_Label"
        Me.Total_Label.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Total_Label.Size = New System.Drawing.Size(39, 19)
        Me.Total_Label.TabIndex = 461
        Me.Total_Label.Text = "-----"
        Me.Total_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'isPrintBeforeEndBill_CB
        '
        Me.isPrintBeforeEndBill_CB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.isPrintBeforeEndBill_CB.AutoSize = True
        Me.isPrintBeforeEndBill_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.isPrintBeforeEndBill_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.isPrintBeforeEndBill_CB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.isPrintBeforeEndBill_CB.Location = New System.Drawing.Point(235, 4)
        Me.isPrintBeforeEndBill_CB.Name = "isPrintBeforeEndBill_CB"
        Me.isPrintBeforeEndBill_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.isPrintBeforeEndBill_CB.Size = New System.Drawing.Size(116, 18)
        Me.isPrintBeforeEndBill_CB.TabIndex = 460
        Me.isPrintBeforeEndBill_CB.Text = "طباعة قبل الإغلاق"
        Me.isPrintBeforeEndBill_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.isPrintBeforeEndBill_CB.UseVisualStyleBackColor = True
        '
        'TB_Info_LB
        '
        Me.TB_Info_LB.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TB_Info_LB.AutoSize = True
        Me.TB_Info_LB.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.TB_Info_LB.Location = New System.Drawing.Point(7, 4)
        Me.TB_Info_LB.Name = "TB_Info_LB"
        Me.TB_Info_LB.Size = New System.Drawing.Size(23, 17)
        Me.TB_Info_LB.TabIndex = 459
        Me.TB_Info_LB.Text = "---"
        '
        'PureLabel
        '
        Me.PureLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.PureLabel.AutoSize = True
        Me.PureLabel.BackColor = System.Drawing.Color.Transparent
        Me.PureLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.PureLabel.ForeColor = System.Drawing.Color.DarkRed
        Me.PureLabel.Location = New System.Drawing.Point(200, 56)
        Me.PureLabel.Name = "PureLabel"
        Me.PureLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PureLabel.Size = New System.Drawing.Size(60, 21)
        Me.PureLabel.TabIndex = 458
        Me.PureLabel.Text = "الحساب"
        Me.PureLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PureTextBox
        '
        Me.PureTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.PureTextBox.BackColor = System.Drawing.SystemColors.Info
        Me.PureTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PureTextBox.Font = New System.Drawing.Font("Stencil", 15.0!)
        Me.PureTextBox.ForeColor = System.Drawing.Color.DarkRed
        Me.PureTextBox.Location = New System.Drawing.Point(4, 50)
        Me.PureTextBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PureTextBox.Name = "PureTextBox"
        Me.PureTextBox.ReadOnly = True
        Me.PureTextBox.Size = New System.Drawing.Size(192, 31)
        Me.PureTextBox.TabIndex = 456
        Me.PureTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SaveButton
        '
        Me.SaveButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.SaveButton.BackColor = System.Drawing.Color.Bisque
        Me.SaveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SaveButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveButton.ForeColor = System.Drawing.Color.Black
        Me.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SaveButton.Location = New System.Drawing.Point(3, 484)
        Me.SaveButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SaveButton.Size = New System.Drawing.Size(140, 47)
        Me.SaveButton.TabIndex = 568
        Me.SaveButton.Text = "إغلاق حساب F1"
        Me.SaveButton.UseVisualStyleBackColor = False
        '
        'TB_Info
        '
        Me.TB_Info.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TB_Info.BackColor = System.Drawing.Color.Bisque
        Me.TB_Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TB_Info.Font = New System.Drawing.Font("Segoe UI Semibold", 11.5!, System.Drawing.FontStyle.Bold)
        Me.TB_Info.ForeColor = System.Drawing.SystemColors.Desktop
        Me.TB_Info.Location = New System.Drawing.Point(3, 3)
        Me.TB_Info.Name = "TB_Info"
        Me.TB_Info.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TB_Info.Size = New System.Drawing.Size(138, 40)
        Me.TB_Info.TabIndex = 570
        Me.TB_Info.Text = "---"
        Me.TB_Info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BillsMetroGrid
        '
        Me.BillsMetroGrid.AllowUserToAddRows = False
        Me.BillsMetroGrid.AllowUserToDeleteRows = False
        Me.BillsMetroGrid.AllowUserToResizeRows = False
        Me.BillsMetroGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.BillsMetroGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BillsMetroGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.BillsMetroGrid.CausesValidation = False
        Me.BillsMetroGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.BillsMetroGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillsMetroGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.BillsMetroGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.BillsMetroGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Edit_TB_IMQty_CL, Me.Bills_T_ID_CL, Me.B_D_ID_CL, Me.B_Pr_ID_CL, Me.AG_ID_CL, Me.Date_CL, Me.AG_Name_Bill, Me.Total_TB_CL, Me.Discount_CL, Me.Pure_CL, Me.User_Name_CL, Me.B_Type_CL})
        Me.BillsMetroGrid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillsMetroGrid.DefaultCellStyle = DataGridViewCellStyle10
        Me.BillsMetroGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.BillsMetroGrid.EnableHeadersVisualStyles = False
        Me.BillsMetroGrid.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.BillsMetroGrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BillsMetroGrid.Location = New System.Drawing.Point(4, 2)
        Me.BillsMetroGrid.MultiSelect = False
        Me.BillsMetroGrid.Name = "BillsMetroGrid"
        Me.BillsMetroGrid.ReadOnly = True
        Me.BillsMetroGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.BillsMetroGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillsMetroGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.BillsMetroGrid.RowHeadersVisible = False
        Me.BillsMetroGrid.RowHeadersWidth = 50
        Me.BillsMetroGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.BillsMetroGrid.RowTemplate.Height = 50
        Me.BillsMetroGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillsMetroGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.BillsMetroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.BillsMetroGrid.Size = New System.Drawing.Size(348, 347)
        Me.BillsMetroGrid.TabIndex = 563
        Me.BillsMetroGrid.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'Column1
        '
        Me.Column1.FillWeight = 90.0!
        Me.Column1.HeaderText = ""
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Text = "فتح"
        Me.Column1.UseColumnTextForButtonValue = True
        '
        'Edit_TB_IMQty_CL
        '
        Me.Edit_TB_IMQty_CL.HeaderText = ""
        Me.Edit_TB_IMQty_CL.Name = "Edit_TB_IMQty_CL"
        Me.Edit_TB_IMQty_CL.ReadOnly = True
        Me.Edit_TB_IMQty_CL.Text = "تعديل"
        Me.Edit_TB_IMQty_CL.UseColumnTextForButtonValue = True
        '
        'Bills_T_ID_CL
        '
        Me.Bills_T_ID_CL.DataPropertyName = "T_ID"
        Me.Bills_T_ID_CL.HeaderText = "ر.الآلي"
        Me.Bills_T_ID_CL.Name = "Bills_T_ID_CL"
        Me.Bills_T_ID_CL.ReadOnly = True
        Me.Bills_T_ID_CL.Visible = False
        '
        'B_D_ID_CL
        '
        Me.B_D_ID_CL.DataPropertyName = "SB_ID"
        Me.B_D_ID_CL.FillWeight = 60.11345!
        Me.B_D_ID_CL.HeaderText = "ر.الفاتورة"
        Me.B_D_ID_CL.Name = "B_D_ID_CL"
        Me.B_D_ID_CL.ReadOnly = True
        Me.B_D_ID_CL.Visible = False
        '
        'B_Pr_ID_CL
        '
        Me.B_Pr_ID_CL.DataPropertyName = "S_Bill_Pr_ID"
        Me.B_Pr_ID_CL.HeaderText = "ر.الفاتورة اليومي"
        Me.B_Pr_ID_CL.Name = "B_Pr_ID_CL"
        Me.B_Pr_ID_CL.ReadOnly = True
        Me.B_Pr_ID_CL.Visible = False
        '
        'AG_ID_CL
        '
        Me.AG_ID_CL.DataPropertyName = "AG_ID"
        Me.AG_ID_CL.HeaderText = "رقم الزبون"
        Me.AG_ID_CL.Name = "AG_ID_CL"
        Me.AG_ID_CL.ReadOnly = True
        Me.AG_ID_CL.Visible = False
        '
        'Date_CL
        '
        Me.Date_CL.DataPropertyName = "Date"
        Me.Date_CL.HeaderText = "تاريخ"
        Me.Date_CL.Name = "Date_CL"
        Me.Date_CL.ReadOnly = True
        '
        'AG_Name_Bill
        '
        Me.AG_Name_Bill.DataPropertyName = "AG_Name"
        Me.AG_Name_Bill.FillWeight = 75.21803!
        Me.AG_Name_Bill.HeaderText = "الزبون"
        Me.AG_Name_Bill.Name = "AG_Name_Bill"
        Me.AG_Name_Bill.ReadOnly = True
        '
        'Total_TB_CL
        '
        Me.Total_TB_CL.DataPropertyName = "Total"
        DataGridViewCellStyle9.Format = "N2"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.Total_TB_CL.DefaultCellStyle = DataGridViewCellStyle9
        Me.Total_TB_CL.FillWeight = 60.11345!
        Me.Total_TB_CL.HeaderText = "الإجمالي"
        Me.Total_TB_CL.Name = "Total_TB_CL"
        Me.Total_TB_CL.ReadOnly = True
        '
        'Discount_CL
        '
        Me.Discount_CL.DataPropertyName = "Discount"
        Me.Discount_CL.HeaderText = "تخفيض"
        Me.Discount_CL.Name = "Discount_CL"
        Me.Discount_CL.ReadOnly = True
        '
        'Pure_CL
        '
        Me.Pure_CL.DataPropertyName = "Pure"
        Me.Pure_CL.HeaderText = "الصافي"
        Me.Pure_CL.Name = "Pure_CL"
        Me.Pure_CL.ReadOnly = True
        '
        'User_Name_CL
        '
        Me.User_Name_CL.DataPropertyName = "UserName"
        Me.User_Name_CL.HeaderText = "المدخل"
        Me.User_Name_CL.Name = "User_Name_CL"
        Me.User_Name_CL.ReadOnly = True
        '
        'B_Type_CL
        '
        Me.B_Type_CL.DataPropertyName = "BsType_ID"
        Me.B_Type_CL.HeaderText = "نوع الفاتورة"
        Me.B_Type_CL.Name = "B_Type_CL"
        Me.B_Type_CL.ReadOnly = True
        Me.B_Type_CL.Visible = False
        '
        'Grids_Panel
        '
        Me.Grids_Panel.Controls.Add(Me.BillsMetroGrid)
        Me.Grids_Panel.Controls.Add(Me.IMGrid)
        Me.Grids_Panel.Font = New System.Drawing.Font("Segoe Marker", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Grids_Panel.Location = New System.Drawing.Point(3, 44)
        Me.Grids_Panel.Name = "Grids_Panel"
        Me.Grids_Panel.Size = New System.Drawing.Size(355, 353)
        Me.Grids_Panel.TabIndex = 571
        '
        'Items_btn
        '
        Me.Items_btn.BackColor = System.Drawing.SystemColors.Info
        Me.Items_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Items_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Items_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Items_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Items_btn.ForeColor = System.Drawing.Color.Black
        Me.Items_btn.Location = New System.Drawing.Point(143, 3)
        Me.Items_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Items_btn.Name = "Items_btn"
        Me.Items_btn.Size = New System.Drawing.Size(55, 40)
        Me.Items_btn.TabIndex = 572
        Me.Items_btn.Text = "أصناف"
        Me.Items_btn.UseVisualStyleBackColor = False
        '
        'Bills_btn
        '
        Me.Bills_btn.BackColor = System.Drawing.SystemColors.Info
        Me.Bills_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Bills_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Bills_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Bills_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Bills_btn.ForeColor = System.Drawing.Color.Black
        Me.Bills_btn.Location = New System.Drawing.Point(199, 3)
        Me.Bills_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Bills_btn.Name = "Bills_btn"
        Me.Bills_btn.Size = New System.Drawing.Size(55, 40)
        Me.Bills_btn.TabIndex = 573
        Me.Bills_btn.Text = "فواتير"
        Me.Bills_btn.UseVisualStyleBackColor = False
        '
        'TB_Transfer_Panel
        '
        Me.TB_Transfer_Panel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.TB_Transfer_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TB_Transfer_Panel.Controls.Add(Me.ClearNumBtn)
        Me.TB_Transfer_Panel.Controls.Add(Me.TB_F_txt)
        Me.TB_Transfer_Panel.Controls.Add(Me.TranConfirm_btn)
        Me.TB_Transfer_Panel.Controls.Add(Me.TB_T_txt)
        Me.TB_Transfer_Panel.Controls.Add(Me.Label2)
        Me.TB_Transfer_Panel.Controls.Add(Me.Label1)
        Me.TB_Transfer_Panel.Location = New System.Drawing.Point(86, 480)
        Me.TB_Transfer_Panel.Name = "TB_Transfer_Panel"
        Me.TB_Transfer_Panel.Size = New System.Drawing.Size(306, 47)
        Me.TB_Transfer_Panel.TabIndex = 576
        '
        'ClearNumBtn
        '
        Me.ClearNumBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ClearNumBtn.BackColor = System.Drawing.Color.IndianRed
        Me.ClearNumBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ClearNumBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.ClearNumBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ClearNumBtn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ClearNumBtn.ForeColor = System.Drawing.SystemColors.Control
        Me.ClearNumBtn.Location = New System.Drawing.Point(3, 11)
        Me.ClearNumBtn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ClearNumBtn.Name = "ClearNumBtn"
        Me.ClearNumBtn.Size = New System.Drawing.Size(38, 27)
        Me.ClearNumBtn.TabIndex = 568
        Me.ClearNumBtn.Text = "C"
        Me.ClearNumBtn.UseVisualStyleBackColor = False
        '
        'TB_F_txt
        '
        Me.TB_F_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TB_F_txt.BackColor = System.Drawing.SystemColors.HighlightText
        Me.TB_F_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TB_F_txt.Font = New System.Drawing.Font("Times New Roman", 11.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_F_txt.ForeColor = System.Drawing.Color.DarkRed
        Me.TB_F_txt.Location = New System.Drawing.Point(213, 12)
        Me.TB_F_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TB_F_txt.Name = "TB_F_txt"
        Me.TB_F_txt.ReadOnly = True
        Me.TB_F_txt.Size = New System.Drawing.Size(59, 26)
        Me.TB_F_txt.TabIndex = 457
        Me.TB_F_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TranConfirm_btn
        '
        Me.TranConfirm_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TranConfirm_btn.BackColor = System.Drawing.Color.Linen
        Me.TranConfirm_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TranConfirm_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TranConfirm_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.TranConfirm_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TranConfirm_btn.ForeColor = System.Drawing.Color.Black
        Me.TranConfirm_btn.Image = Global.resturant.My.Resources.Resources.iconfinder_icon_refresh_2867936
        Me.TranConfirm_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.TranConfirm_btn.Location = New System.Drawing.Point(42, 11)
        Me.TranConfirm_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TranConfirm_btn.Name = "TranConfirm_btn"
        Me.TranConfirm_btn.Size = New System.Drawing.Size(73, 27)
        Me.TranConfirm_btn.TabIndex = 567
        Me.TranConfirm_btn.Text = "نقـــل"
        Me.TranConfirm_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TranConfirm_btn.UseVisualStyleBackColor = False
        '
        'TB_T_txt
        '
        Me.TB_T_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TB_T_txt.BackColor = System.Drawing.SystemColors.HighlightText
        Me.TB_T_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TB_T_txt.Font = New System.Drawing.Font("Times New Roman", 11.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_T_txt.ForeColor = System.Drawing.Color.DarkRed
        Me.TB_T_txt.Location = New System.Drawing.Point(118, 12)
        Me.TB_T_txt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TB_T_txt.Name = "TB_T_txt"
        Me.TB_T_txt.ReadOnly = True
        Me.TB_T_txt.Size = New System.Drawing.Size(59, 26)
        Me.TB_T_txt.TabIndex = 458
        Me.TB_T_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(180, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 19)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "إلى"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(275, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 19)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "من"
        '
        'Apart_List_btn
        '
        Me.Apart_List_btn.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Apart_List_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Apart_List_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Apart_List_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Apart_List_btn.Font = New System.Drawing.Font("Times New Roman", 13.25!, System.Drawing.FontStyle.Bold)
        Me.Apart_List_btn.ForeColor = System.Drawing.Color.DarkGreen
        Me.Apart_List_btn.Location = New System.Drawing.Point(311, 3)
        Me.Apart_List_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Apart_List_btn.Name = "Apart_List_btn"
        Me.Apart_List_btn.Size = New System.Drawing.Size(47, 40)
        Me.Apart_List_btn.TabIndex = 583
        Me.Apart_List_btn.Text = "(0)"
        Me.Apart_List_btn.UseVisualStyleBackColor = False
        '
        'Debit_Table_btn
        '
        Me.Debit_Table_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Debit_Table_btn.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Debit_Table_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Debit_Table_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Debit_Table_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Debit_Table_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Debit_Table_btn.ForeColor = System.Drawing.Color.Black
        Me.Debit_Table_btn.Image = Global.resturant.My.Resources.Resources.if_free_07_463022__1_
        Me.Debit_Table_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Debit_Table_btn.Location = New System.Drawing.Point(1, 480)
        Me.Debit_Table_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Debit_Table_btn.Name = "Debit_Table_btn"
        Me.Debit_Table_btn.Size = New System.Drawing.Size(84, 47)
        Me.Debit_Table_btn.TabIndex = 582
        Me.Debit_Table_btn.Text = "مديونة"
        Me.Debit_Table_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Debit_Table_btn.UseVisualStyleBackColor = False
        '
        'Button51
        '
        Me.Button51.BackColor = System.Drawing.SystemColors.Info
        Me.Button51.BackgroundImage = CType(resources.GetObject("Button51.BackgroundImage"), System.Drawing.Image)
        Me.Button51.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button51.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button51.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button51.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button51.Location = New System.Drawing.Point(461, 354)
        Me.Button51.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button51.Name = "Button51"
        Me.Button51.Size = New System.Drawing.Size(62, 66)
        Me.Button51.TabIndex = 581
        Me.Button51.UseVisualStyleBackColor = False
        '
        'Button50
        '
        Me.Button50.BackColor = System.Drawing.SystemColors.Info
        Me.Button50.BackgroundImage = CType(resources.GetObject("Button50.BackgroundImage"), System.Drawing.Image)
        Me.Button50.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button50.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button50.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button50.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button50.Location = New System.Drawing.Point(461, 122)
        Me.Button50.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button50.Name = "Button50"
        Me.Button50.Size = New System.Drawing.Size(62, 66)
        Me.Button50.TabIndex = 580
        Me.Button50.UseVisualStyleBackColor = False
        '
        'PiedApart_btn
        '
        Me.PiedApart_btn.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.PiedApart_btn.BackColor = System.Drawing.Color.Bisque
        Me.PiedApart_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PiedApart_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PiedApart_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.PiedApart_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.PiedApart_btn.ForeColor = System.Drawing.Color.Black
        Me.PiedApart_btn.Image = Global.resturant.My.Resources.Resources.iconfinder_money_299107
        Me.PiedApart_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.PiedApart_btn.Location = New System.Drawing.Point(238, 484)
        Me.PiedApart_btn.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PiedApart_btn.Name = "PiedApart_btn"
        Me.PiedApart_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PiedApart_btn.Size = New System.Drawing.Size(120, 47)
        Me.PiedApart_btn.TabIndex = 579
        Me.PiedApart_btn.Text = "تسديد جزء"
        Me.PiedApart_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.PiedApart_btn.UseVisualStyleBackColor = False
        '
        'TB_ButOnAG_btn
        '
        Me.TB_ButOnAG_btn.BackColor = System.Drawing.SystemColors.HighlightText
        Me.TB_ButOnAG_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TB_ButOnAG_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TB_ButOnAG_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.TB_ButOnAG_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.TB_ButOnAG_btn.ForeColor = System.Drawing.Color.Black
        Me.TB_ButOnAG_btn.Image = Global.resturant.My.Resources.Resources.if_user_173122
        Me.TB_ButOnAG_btn.Location = New System.Drawing.Point(255, 3)
        Me.TB_ButOnAG_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TB_ButOnAG_btn.Name = "TB_ButOnAG_btn"
        Me.TB_ButOnAG_btn.Size = New System.Drawing.Size(55, 40)
        Me.TB_ButOnAG_btn.TabIndex = 574
        Me.TB_ButOnAG_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TB_ButOnAG_btn.UseVisualStyleBackColor = False
        '
        'PrintBillButton
        '
        Me.PrintBillButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.PrintBillButton.BackColor = System.Drawing.Color.IndianRed
        Me.PrintBillButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PrintBillButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PrintBillButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.PrintBillButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.PrintBillButton.ForeColor = System.Drawing.Color.White
        Me.PrintBillButton.Image = CType(resources.GetObject("PrintBillButton.Image"), System.Drawing.Image)
        Me.PrintBillButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.PrintBillButton.Location = New System.Drawing.Point(144, 484)
        Me.PrintBillButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PrintBillButton.Name = "PrintBillButton"
        Me.PrintBillButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PrintBillButton.Size = New System.Drawing.Size(93, 47)
        Me.PrintBillButton.TabIndex = 567
        Me.PrintBillButton.Text = "طبـاعة"
        Me.PrintBillButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.PrintBillButton.UseVisualStyleBackColor = False
        '
        'Refrech_btn
        '
        Me.Refrech_btn.BackColor = System.Drawing.Color.White
        Me.Refrech_btn.BackgroundImage = Global.resturant.My.Resources.Resources.if_refresh_1608809
        Me.Refrech_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Refrech_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Refrech_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Refrech_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Refrech_btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Refrech_btn.ForeColor = System.Drawing.Color.DarkRed
        Me.Refrech_btn.Location = New System.Drawing.Point(392, 1)
        Me.Refrech_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Refrech_btn.Name = "Refrech_btn"
        Me.Refrech_btn.Size = New System.Drawing.Size(68, 40)
        Me.Refrech_btn.TabIndex = 563
        Me.Refrech_btn.UseVisualStyleBackColor = False
        '
        'TB_Types_CMB
        '
        Me.TB_Types_CMB.BackColor = System.Drawing.SystemColors.Info
        Me.TB_Types_CMB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TB_Types_CMB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TB_Types_CMB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TB_Types_CMB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TB_Types_CMB.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold)
        Me.TB_Types_CMB.ForeColor = System.Drawing.Color.DarkGreen
        Me.TB_Types_CMB.FormattingEnabled = True
        Me.TB_Types_CMB.Items.AddRange(New Object() {"حساب طاولة", "نقل طاولة", "نقل أصناف"})
        Me.TB_Types_CMB.Location = New System.Drawing.Point(3, 20)
        Me.TB_Types_CMB.Name = "TB_Types_CMB"
        Me.TB_Types_CMB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TB_Types_CMB.Size = New System.Drawing.Size(124, 27)
        Me.TB_Types_CMB.TabIndex = 584
        '
        'Tables_Option_GB
        '
        Me.Tables_Option_GB.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Tables_Option_GB.Controls.Add(Me.TB_Types_CMB)
        Me.Tables_Option_GB.Font = New System.Drawing.Font("Segoe UI Semibold", 9.25!, System.Drawing.FontStyle.Bold)
        Me.Tables_Option_GB.Location = New System.Drawing.Point(393, 478)
        Me.Tables_Option_GB.Name = "Tables_Option_GB"
        Me.Tables_Option_GB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Tables_Option_GB.Size = New System.Drawing.Size(130, 48)
        Me.Tables_Option_GB.TabIndex = 0
        Me.Tables_Option_GB.TabStop = False
        Me.Tables_Option_GB.Text = "خيارات الإدخال"
        '
        'Time_Table_Label
        '
        Me.Time_Table_Label.BackColor = System.Drawing.Color.Transparent
        Me.Time_Table_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Time_Table_Label.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Time_Table_Label.ForeColor = System.Drawing.Color.Black
        Me.Time_Table_Label.Location = New System.Drawing.Point(3, 1)
        Me.Time_Table_Label.Name = "Time_Table_Label"
        Me.Time_Table_Label.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Time_Table_Label.Size = New System.Drawing.Size(388, 40)
        Me.Time_Table_Label.TabIndex = 584
        '
        'PanelRight
        '
        Me.PanelRight.Controls.Add(Me.Time_Table_Label)
        Me.PanelRight.Controls.Add(Me.Tables_Option_GB)
        Me.PanelRight.Controls.Add(Me.Refrech_btn)
        Me.PanelRight.Controls.Add(Me.Button50)
        Me.PanelRight.Controls.Add(Me.Debit_Table_btn)
        Me.PanelRight.Controls.Add(Me.Button51)
        Me.PanelRight.Controls.Add(Me.F_Panel)
        Me.PanelRight.Controls.Add(Me.TB_Transfer_Panel)
        Me.PanelRight.Location = New System.Drawing.Point(359, 1)
        Me.PanelRight.Name = "PanelRight"
        Me.PanelRight.Size = New System.Drawing.Size(526, 530)
        Me.PanelRight.TabIndex = 585
        '
        'TablesBalance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(887, 536)
        Me.Controls.Add(Me.PanelRight)
        Me.Controls.Add(Me.Apart_List_btn)
        Me.Controls.Add(Me.Bills_btn)
        Me.Controls.Add(Me.Items_btn)
        Me.Controls.Add(Me.PiedApart_btn)
        Me.Controls.Add(Me.TB_ButOnAG_btn)
        Me.Controls.Add(Me.PrintBillButton)
        Me.Controls.Add(Me.Grids_Panel)
        Me.Controls.Add(Me.TB_Info)
        Me.Controls.Add(Me.SaveButton)
        Me.Controls.Add(Me.PurePanel)
        Me.Font = New System.Drawing.Font("Segoe Marker", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "TablesBalance"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TablesBalance"
        CType(Me.IMGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PurePanel.ResumeLayout(False)
        Me.PurePanel.PerformLayout()
        CType(Me.BillsMetroGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Grids_Panel.ResumeLayout(False)
        Me.TB_Transfer_Panel.ResumeLayout(False)
        Me.TB_Transfer_Panel.PerformLayout()
        Me.Tables_Option_GB.ResumeLayout(False)
        Me.PanelRight.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IMGrid As MetroFramework.Controls.MetroGrid
    Friend WithEvents Refrech_btn As System.Windows.Forms.Button
    Friend WithEvents PurePanel As System.Windows.Forms.Panel
    Friend WithEvents PureLabel As System.Windows.Forms.Label
    Friend WithEvents PureTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PrintBillButton As System.Windows.Forms.Button
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents TB_Info As System.Windows.Forms.Label
    Friend WithEvents BillsMetroGrid As MetroFramework.Controls.MetroGrid
    Friend WithEvents Grids_Panel As System.Windows.Forms.Panel
    Friend WithEvents Items_btn As System.Windows.Forms.Button
    Friend WithEvents Bills_btn As System.Windows.Forms.Button
    Friend WithEvents TB_ButOnAG_btn As System.Windows.Forms.Button
    Friend WithEvents TB_Transfer_Panel As System.Windows.Forms.Panel
    Friend WithEvents TB_T_txt As System.Windows.Forms.TextBox
    Friend WithEvents TB_F_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TranConfirm_btn As System.Windows.Forms.Button
    Friend WithEvents PiedApart_btn As System.Windows.Forms.Button
    Friend WithEvents Button51 As System.Windows.Forms.Button
    Friend WithEvents Button50 As System.Windows.Forms.Button
    Public WithEvents F_Panel As System.Windows.Forms.Panel
    Friend WithEvents Debit_Table_btn As System.Windows.Forms.Button
    Friend WithEvents IM_T_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_NameCL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit_Price_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Apart_List_btn As System.Windows.Forms.Button
    Friend WithEvents TB_Info_LB As System.Windows.Forms.Label
    Friend WithEvents TB_Types_CMB As System.Windows.Forms.ComboBox
    Friend WithEvents ClearNumBtn As System.Windows.Forms.Button
    Friend WithEvents Tables_Option_GB As System.Windows.Forms.GroupBox
    Friend WithEvents isPrintBeforeEndBill_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Time_Table_Label As System.Windows.Forms.Label
    Friend WithEvents Total_Label As Label
    Friend WithEvents Show_AllBill_Clmns_CB As CheckBox
    Friend WithEvents Column1 As DataGridViewButtonColumn
    Friend WithEvents Edit_TB_IMQty_CL As DataGridViewButtonColumn
    Friend WithEvents Bills_T_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents B_D_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents B_Pr_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents AG_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents Date_CL As DataGridViewTextBoxColumn
    Friend WithEvents AG_Name_Bill As DataGridViewTextBoxColumn
    Friend WithEvents Total_TB_CL As DataGridViewTextBoxColumn
    Friend WithEvents Discount_CL As DataGridViewTextBoxColumn
    Friend WithEvents Pure_CL As DataGridViewTextBoxColumn
    Friend WithEvents User_Name_CL As DataGridViewTextBoxColumn
    Friend WithEvents B_Type_CL As DataGridViewTextBoxColumn
    Friend WithEvents PanelRight As Panel
End Class
