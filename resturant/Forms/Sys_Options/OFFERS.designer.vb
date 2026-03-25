<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OFFERS
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OFFERS))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.IM_MENU_FS = New resturant.FSearch_Filter()
        Me.AGMetroGrid = New System.Windows.Forms.DataGridView()
        Me.IM_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TITLE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.item_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DATE_F_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DATE_T_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TIME_F_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TIME_T_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unit_DataGridView = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.U_IM_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.is_Default_CL = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.U_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Barcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Min_SP_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Min_SP_2_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UserName_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.U_Cargo_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OFFER_PRICE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.T_F = New System.Windows.Forms.DateTimePicker()
        Me.T_T = New System.Windows.Forms.DateTimePicker()
        Me.D_F = New System.Windows.Forms.DateTimePicker()
        Me.D_T = New System.Windows.Forms.DateTimePicker()
        Me.TITLE_TXT = New resturant.F5Text50Field()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Barcode_SH_txt = New System.Windows.Forms.TextBox()
        CType(Me.AGMetroGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Unit_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IM_MENU_FS
        '
        Me.IM_MENU_FS.CancelSearchImage = CType(resources.GetObject("IM_MENU_FS.CancelSearchImage"), System.Drawing.Image)
        Me.IM_MENU_FS.Location = New System.Drawing.Point(581, 49)
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
        Me.IM_MENU_FS.TabIndex = 625
        Me.IM_MENU_FS.TextMaxLength = 250
        Me.IM_MENU_FS.Textt = ""
        '
        'AGMetroGrid
        '
        Me.AGMetroGrid.AllowUserToAddRows = False
        Me.AGMetroGrid.AllowUserToDeleteRows = False
        Me.AGMetroGrid.AllowUserToResizeRows = False
        Me.AGMetroGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.AGMetroGrid.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.AGMetroGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.AGMetroGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IM_ID_CL, Me.TITLE_CL, Me.item_name_CL, Me.DATE_F_CL, Me.DATE_T_CL, Me.TIME_F_CL, Me.TIME_T_CL})
        Me.AGMetroGrid.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AGMetroGrid.Location = New System.Drawing.Point(1, 184)
        Me.AGMetroGrid.MultiSelect = False
        Me.AGMetroGrid.Name = "AGMetroGrid"
        Me.AGMetroGrid.ReadOnly = True
        Me.AGMetroGrid.RowHeadersVisible = False
        Me.AGMetroGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.AGMetroGrid.RowTemplate.Height = 25
        Me.AGMetroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.AGMetroGrid.Size = New System.Drawing.Size(1003, 336)
        Me.AGMetroGrid.TabIndex = 706
        '
        'IM_ID_CL
        '
        Me.IM_ID_CL.DataPropertyName = "IM_ID"
        Me.IM_ID_CL.HeaderText = "IM_ID"
        Me.IM_ID_CL.Name = "IM_ID_CL"
        Me.IM_ID_CL.ReadOnly = True
        Me.IM_ID_CL.Visible = False
        '
        'TITLE_CL
        '
        Me.TITLE_CL.DataPropertyName = "TITLE"
        Me.TITLE_CL.FillWeight = 91.83587!
        Me.TITLE_CL.HeaderText = "عنوان العرض"
        Me.TITLE_CL.Name = "TITLE_CL"
        Me.TITLE_CL.ReadOnly = True
        '
        'item_name_CL
        '
        Me.item_name_CL.DataPropertyName = "item_name"
        Me.item_name_CL.HeaderText = "الصنف"
        Me.item_name_CL.Name = "item_name_CL"
        Me.item_name_CL.ReadOnly = True
        '
        'DATE_F_CL
        '
        Me.DATE_F_CL.DataPropertyName = "DATE_F"
        Me.DATE_F_CL.FillWeight = 91.83587!
        Me.DATE_F_CL.HeaderText = "تاريخ البداية"
        Me.DATE_F_CL.Name = "DATE_F_CL"
        Me.DATE_F_CL.ReadOnly = True
        '
        'DATE_T_CL
        '
        Me.DATE_T_CL.DataPropertyName = "DATE_T"
        Me.DATE_T_CL.FillWeight = 91.83587!
        Me.DATE_T_CL.HeaderText = "تاريخ النهاية"
        Me.DATE_T_CL.Name = "DATE_T_CL"
        Me.DATE_T_CL.ReadOnly = True
        '
        'TIME_F_CL
        '
        Me.TIME_F_CL.DataPropertyName = "TIME_F"
        Me.TIME_F_CL.FillWeight = 91.83587!
        Me.TIME_F_CL.HeaderText = "من ساعة"
        Me.TIME_F_CL.Name = "TIME_F_CL"
        Me.TIME_F_CL.ReadOnly = True
        '
        'TIME_T_CL
        '
        Me.TIME_T_CL.DataPropertyName = "TIME_T"
        Me.TIME_T_CL.FillWeight = 91.83587!
        Me.TIME_T_CL.HeaderText = "إلى ساعة"
        Me.TIME_T_CL.Name = "TIME_T_CL"
        Me.TIME_T_CL.ReadOnly = True
        '
        'Unit_DataGridView
        '
        Me.Unit_DataGridView.AllowUserToAddRows = False
        Me.Unit_DataGridView.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro
        Me.Unit_DataGridView.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.Unit_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Unit_DataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.Unit_DataGridView.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("JF Flat", 11.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Unit_DataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.Unit_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Unit_DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.U_IM_ID_CL, Me.is_Default_CL, Me.U_Name_CL, Me.Barcode, Me.Price_CL, Me.Min_SP_CL, Me.Min_SP_2_CL, Me.UserName_CL, Me.U_Cargo_CL, Me.OFFER_PRICE_CL})
        Me.Unit_DataGridView.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("JF Flat", 11.0!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Unit_DataGridView.DefaultCellStyle = DataGridViewCellStyle4
        Me.Unit_DataGridView.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.Unit_DataGridView.Location = New System.Drawing.Point(1, 2)
        Me.Unit_DataGridView.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Unit_DataGridView.MultiSelect = False
        Me.Unit_DataGridView.Name = "Unit_DataGridView"
        Me.Unit_DataGridView.RowHeadersVisible = False
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.Font = New System.Drawing.Font("JF Flat", 10.0!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Unit_DataGridView.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.Unit_DataGridView.RowTemplate.Height = 32
        Me.Unit_DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Unit_DataGridView.Size = New System.Drawing.Size(579, 177)
        Me.Unit_DataGridView.TabIndex = 707
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "U_ID"
        Me.DataGridViewTextBoxColumn1.FillWeight = 5.0!
        Me.DataGridViewTextBoxColumn1.Frozen = True
        Me.DataGridViewTextBoxColumn1.HeaderText = "ر.الوحدة"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Visible = False
        Me.DataGridViewTextBoxColumn1.Width = 5
        '
        'U_IM_ID_CL
        '
        Me.U_IM_ID_CL.DataPropertyName = "U_IM_ID"
        Me.U_IM_ID_CL.FillWeight = 28.02963!
        Me.U_IM_ID_CL.HeaderText = "ت.م"
        Me.U_IM_ID_CL.Name = "U_IM_ID_CL"
        Me.U_IM_ID_CL.Visible = False
        '
        'is_Default_CL
        '
        Me.is_Default_CL.DataPropertyName = "is_Default"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.NullValue = False
        Me.is_Default_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.is_Default_CL.FillWeight = 35.0!
        Me.is_Default_CL.HeaderText = "أساسي"
        Me.is_Default_CL.Name = "is_Default_CL"
        Me.is_Default_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.is_Default_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.is_Default_CL.Visible = False
        '
        'U_Name_CL
        '
        Me.U_Name_CL.DataPropertyName = "U_Name"
        Me.U_Name_CL.FillWeight = 71.0327!
        Me.U_Name_CL.HeaderText = "الوحدة"
        Me.U_Name_CL.Name = "U_Name_CL"
        '
        'Barcode
        '
        Me.Barcode.DataPropertyName = "Barcode"
        Me.Barcode.FillWeight = 84.05599!
        Me.Barcode.HeaderText = "باركود"
        Me.Barcode.Name = "Barcode"
        '
        'Price_CL
        '
        Me.Price_CL.DataPropertyName = "Price"
        Me.Price_CL.FillWeight = 88.33192!
        Me.Price_CL.HeaderText = "سعر البيع"
        Me.Price_CL.Name = "Price_CL"
        '
        'Min_SP_CL
        '
        Me.Min_SP_CL.DataPropertyName = "Min_SP"
        Me.Min_SP_CL.FillWeight = 87.76173!
        Me.Min_SP_CL.HeaderText = "سعر الجملة"
        Me.Min_SP_CL.Name = "Min_SP_CL"
        Me.Min_SP_CL.Visible = False
        '
        'Min_SP_2_CL
        '
        Me.Min_SP_2_CL.DataPropertyName = "Min_SP_2"
        Me.Min_SP_2_CL.HeaderText = "جملة الجملة"
        Me.Min_SP_2_CL.Name = "Min_SP_2_CL"
        Me.Min_SP_2_CL.Visible = False
        '
        'UserName_CL
        '
        Me.UserName_CL.DataPropertyName = "UserName"
        Me.UserName_CL.FillWeight = 66.21652!
        Me.UserName_CL.HeaderText = "المدخل"
        Me.UserName_CL.Name = "UserName_CL"
        Me.UserName_CL.Visible = False
        '
        'U_Cargo_CL
        '
        Me.U_Cargo_CL.DataPropertyName = "U_Cargo"
        Me.U_Cargo_CL.HeaderText = "حمولة"
        Me.U_Cargo_CL.Name = "U_Cargo_CL"
        Me.U_Cargo_CL.Visible = False
        '
        'OFFER_PRICE_CL
        '
        Me.OFFER_PRICE_CL.DataPropertyName = "OFFER_PRICE"
        Me.OFFER_PRICE_CL.HeaderText = "سعر العرض"
        Me.OFFER_PRICE_CL.Name = "OFFER_PRICE_CL"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Font = New System.Drawing.Font("JF Flat", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(935, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 34)
        Me.Label2.TabIndex = 708
        Me.Label2.Text = "الصنف:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(879, 523)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(125, 40)
        Me.ExitFormButton.TabIndex = 709
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'T_F
        '
        Me.T_F.CalendarFont = New System.Drawing.Font("Arial", 10.25!)
        Me.T_F.CustomFormat = "HH:mm:ss"
        Me.T_F.Font = New System.Drawing.Font("Times New Roman", 11.25!)
        Me.T_F.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.T_F.Location = New System.Drawing.Point(802, 156)
        Me.T_F.Margin = New System.Windows.Forms.Padding(7, 5, 7, 5)
        Me.T_F.Name = "T_F"
        Me.T_F.ShowUpDown = True
        Me.T_F.Size = New System.Drawing.Size(106, 25)
        Me.T_F.TabIndex = 710
        '
        'T_T
        '
        Me.T_T.CalendarFont = New System.Drawing.Font("Arial", 10.25!)
        Me.T_T.CustomFormat = "HH:mm:ss"
        Me.T_T.Font = New System.Drawing.Font("Times New Roman", 11.25!)
        Me.T_T.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.T_T.Location = New System.Drawing.Point(583, 153)
        Me.T_T.Margin = New System.Windows.Forms.Padding(7, 5, 7, 5)
        Me.T_T.Name = "T_T"
        Me.T_T.ShowUpDown = True
        Me.T_T.Size = New System.Drawing.Size(106, 25)
        Me.T_T.TabIndex = 711
        '
        'D_F
        '
        Me.D_F.CalendarFont = New System.Drawing.Font("Segoe UI", 16.25!)
        Me.D_F.CalendarMonthBackground = System.Drawing.SystemColors.Info
        Me.D_F.CalendarTitleBackColor = System.Drawing.SystemColors.Info
        Me.D_F.CalendarTrailingForeColor = System.Drawing.SystemColors.Info
        Me.D_F.CustomFormat = "dd/MM/yyyy"
        Me.D_F.Font = New System.Drawing.Font("Times New Roman", 11.25!)
        Me.D_F.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.D_F.Location = New System.Drawing.Point(802, 125)
        Me.D_F.Name = "D_F"
        Me.D_F.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.D_F.RightToLeftLayout = True
        Me.D_F.Size = New System.Drawing.Size(106, 25)
        Me.D_F.TabIndex = 733
        '
        'D_T
        '
        Me.D_T.CalendarFont = New System.Drawing.Font("Segoe UI", 16.25!)
        Me.D_T.CalendarMonthBackground = System.Drawing.SystemColors.Info
        Me.D_T.CalendarTitleBackColor = System.Drawing.SystemColors.Info
        Me.D_T.CalendarTrailingForeColor = System.Drawing.SystemColors.Info
        Me.D_T.CustomFormat = "dd/MM/yyyy"
        Me.D_T.Font = New System.Drawing.Font("Times New Roman", 11.25!)
        Me.D_T.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.D_T.Location = New System.Drawing.Point(583, 125)
        Me.D_T.Name = "D_T"
        Me.D_T.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.D_T.RightToLeftLayout = True
        Me.D_T.Size = New System.Drawing.Size(106, 25)
        Me.D_T.TabIndex = 734
        '
        'TITLE_TXT
        '
        Me.TITLE_TXT.BackColor = System.Drawing.Color.Lavender
        Me.TITLE_TXT.Font = New System.Drawing.Font("JF Flat", 11.0!)
        Me.TITLE_TXT.Location = New System.Drawing.Point(583, 86)
        Me.TITLE_TXT.MaxLength = 50
        Me.TITLE_TXT.Name = "TITLE_TXT"
        Me.TITLE_TXT.Size = New System.Drawing.Size(350, 33)
        Me.TITLE_TXT.TabIndex = 735
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("JF Flat", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(936, 89)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 30)
        Me.Label1.TabIndex = 736
        Me.Label1.Text = "العنوان:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(910, 124)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 26)
        Me.Label3.TabIndex = 737
        Me.Label3.Text = "بداية العرض:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(691, 124)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(106, 25)
        Me.Label4.TabIndex = 738
        Me.Label4.Text = "نهاية العرض:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(910, 156)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 26)
        Me.Label5.TabIndex = 739
        Me.Label5.Text = "من ساعة:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(691, 153)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 25)
        Me.Label6.TabIndex = 740
        Me.Label6.Text = "إلى ساعة:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DeleteButton
        '
        Me.DeleteButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DeleteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DeleteButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DeleteButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DeleteButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.DeleteButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DeleteButton.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.DeleteButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DeleteButton.Image = Global.resturant.My.Resources.Resources.if_f_cross_256_282471
        Me.DeleteButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DeleteButton.Location = New System.Drawing.Point(603, 523)
        Me.DeleteButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(125, 40)
        Me.DeleteButton.TabIndex = 742
        Me.DeleteButton.Text = "حذف العرض"
        Me.DeleteButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DeleteButton.UseVisualStyleBackColor = False
        '
        'SaveButton
        '
        Me.SaveButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.SaveButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SaveButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SaveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.SaveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SaveButton.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SaveButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SaveButton.Image = Global.resturant.My.Resources.Resources.if_floppy_285657
        Me.SaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SaveButton.Location = New System.Drawing.Point(736, 523)
        Me.SaveButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(125, 40)
        Me.SaveButton.TabIndex = 741
        Me.SaveButton.Text = "حفـظ "
        Me.SaveButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveButton.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(935, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 29)
        Me.Label7.TabIndex = 744
        Me.Label7.Text = "باركود :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Barcode_SH_txt
        '
        Me.Barcode_SH_txt.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.Barcode_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Barcode_SH_txt.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Barcode_SH_txt.ForeColor = System.Drawing.Color.Blue
        Me.Barcode_SH_txt.Location = New System.Drawing.Point(581, 12)
        Me.Barcode_SH_txt.Name = "Barcode_SH_txt"
        Me.Barcode_SH_txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Barcode_SH_txt.Size = New System.Drawing.Size(352, 32)
        Me.Barcode_SH_txt.TabIndex = 743
        Me.Barcode_SH_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'OFFERS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 26.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 563)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Barcode_SH_txt)
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.SaveButton)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TITLE_TXT)
        Me.Controls.Add(Me.D_T)
        Me.Controls.Add(Me.D_F)
        Me.Controls.Add(Me.T_F)
        Me.Controls.Add(Me.T_T)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Unit_DataGridView)
        Me.Controls.Add(Me.AGMetroGrid)
        Me.Controls.Add(Me.IM_MENU_FS)
        Me.Name = "OFFERS"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "عروض"
        CType(Me.AGMetroGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Unit_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents IM_MENU_FS As resturant.FSearch_Filter
    Friend WithEvents AGMetroGrid As System.Windows.Forms.DataGridView
    Friend WithEvents Unit_DataGridView As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As Button
    Friend WithEvents T_F As DateTimePicker
    Friend WithEvents T_T As DateTimePicker
    Friend WithEvents D_F As DateTimePicker
    Friend WithEvents D_T As DateTimePicker
    Friend WithEvents TITLE_TXT As F5Text50Field
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents DeleteButton As System.Windows.Forms.Button
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents U_IM_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents is_Default_CL As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents U_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Barcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Min_SP_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Min_SP_2_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UserName_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents U_Cargo_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OFFER_PRICE_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TITLE_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DATE_F_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DATE_T_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TIME_F_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TIME_T_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label7 As Label
    Friend WithEvents Barcode_SH_txt As TextBox
End Class
