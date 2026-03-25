<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Network_Tracker
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Network_Tracker))
        Me.DataGridViewX = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IDX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CP_NAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UserName_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Date_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Screen_Type_NAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Bill_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Operation_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Notes_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.USER_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Screen_Type_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Operation_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalQYT_txt = New System.Windows.Forms.TextBox()
        Me.SalarySearchButton = New System.Windows.Forms.Button()
        Me.User_Cm = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CMSearchTextBox = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.OP_Cm = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Screen_Cmb = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DateTimePicker_From = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker_To = New System.Windows.Forms.DateTimePicker()
        Me.Num_Txt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.DataB = New System.Windows.Forms.BindingSource(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.DataGridViewX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridViewX
        '
        Me.DataGridViewX.AllowUserToAddRows = False
        Me.DataGridViewX.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info
        Me.DataGridViewX.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewX.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewX.BackgroundColor = System.Drawing.Color.SeaShell
        Me.DataGridViewX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.IDX, Me.CP_NAME_CL, Me.UserName_CL, Me.Date_CL, Me.Screen_Type_NAME_CL, Me.Bill_ID_CL, Me.Operation_Name_CL, Me.Notes_CL, Me.USER_ID_CL, Me.Screen_Type_CL, Me.Operation_ID_CL})
        Me.DataGridViewX.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewX.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewX.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DataGridViewX.Location = New System.Drawing.Point(2, 66)
        Me.DataGridViewX.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DataGridViewX.MultiSelect = False
        Me.DataGridViewX.Name = "DataGridViewX"
        Me.DataGridViewX.ReadOnly = True
        Me.DataGridViewX.RowHeadersVisible = False
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.DataGridViewX.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewX.RowTemplate.Height = 30
        Me.DataGridViewX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewX.Size = New System.Drawing.Size(1001, 587)
        Me.DataGridViewX.TabIndex = 649
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "T_ID"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.T_ID_CL.Visible = False
        '
        'IDX
        '
        Me.IDX.DataPropertyName = "IDX"
        Me.IDX.FillWeight = 8.036231!
        Me.IDX.HeaderText = "ت"
        Me.IDX.Name = "IDX"
        Me.IDX.ReadOnly = True
        '
        'CP_NAME_CL
        '
        Me.CP_NAME_CL.DataPropertyName = "CP_NAME"
        Me.CP_NAME_CL.FillWeight = 24.39412!
        Me.CP_NAME_CL.HeaderText = "الكمبيوتر"
        Me.CP_NAME_CL.Name = "CP_NAME_CL"
        Me.CP_NAME_CL.ReadOnly = True
        Me.CP_NAME_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'UserName_CL
        '
        Me.UserName_CL.DataPropertyName = "UserName"
        Me.UserName_CL.FillWeight = 20.32843!
        Me.UserName_CL.HeaderText = "المستخدم"
        Me.UserName_CL.Name = "UserName_CL"
        Me.UserName_CL.ReadOnly = True
        Me.UserName_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Date_CL
        '
        Me.Date_CL.DataPropertyName = "Date"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.Date_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.Date_CL.FillWeight = 24.29184!
        Me.Date_CL.HeaderText = "التاريخ"
        Me.Date_CL.Name = "Date_CL"
        Me.Date_CL.ReadOnly = True
        Me.Date_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Screen_Type_NAME_CL
        '
        Me.Screen_Type_NAME_CL.DataPropertyName = "Type_Name"
        Me.Screen_Type_NAME_CL.FillWeight = 38.57903!
        Me.Screen_Type_NAME_CL.HeaderText = "الشاشة"
        Me.Screen_Type_NAME_CL.Name = "Screen_Type_NAME_CL"
        Me.Screen_Type_NAME_CL.ReadOnly = True
        Me.Screen_Type_NAME_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Bill_ID_CL
        '
        Me.Bill_ID_CL.DataPropertyName = "Bill_ID"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Bill_ID_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.Bill_ID_CL.FillWeight = 16.98424!
        Me.Bill_ID_CL.HeaderText = "الرقم"
        Me.Bill_ID_CL.Name = "Bill_ID_CL"
        Me.Bill_ID_CL.ReadOnly = True
        Me.Bill_ID_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Operation_Name_CL
        '
        Me.Operation_Name_CL.DataPropertyName = "Operation_Name"
        Me.Operation_Name_CL.FillWeight = 39.40801!
        Me.Operation_Name_CL.HeaderText = "الحالة"
        Me.Operation_Name_CL.Name = "Operation_Name_CL"
        Me.Operation_Name_CL.ReadOnly = True
        Me.Operation_Name_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Notes_CL
        '
        Me.Notes_CL.DataPropertyName = "Notes"
        Me.Notes_CL.FillWeight = 81.31374!
        Me.Notes_CL.HeaderText = "الوصف"
        Me.Notes_CL.Name = "Notes_CL"
        Me.Notes_CL.ReadOnly = True
        Me.Notes_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'USER_ID_CL
        '
        Me.USER_ID_CL.DataPropertyName = "USER_ID"
        Me.USER_ID_CL.HeaderText = "USER_ID"
        Me.USER_ID_CL.Name = "USER_ID_CL"
        Me.USER_ID_CL.ReadOnly = True
        Me.USER_ID_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.USER_ID_CL.Visible = False
        '
        'Screen_Type_CL
        '
        Me.Screen_Type_CL.DataPropertyName = "Screen_Type"
        Me.Screen_Type_CL.HeaderText = "Screen_Type"
        Me.Screen_Type_CL.Name = "Screen_Type_CL"
        Me.Screen_Type_CL.ReadOnly = True
        Me.Screen_Type_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Screen_Type_CL.Visible = False
        '
        'Operation_ID_CL
        '
        Me.Operation_ID_CL.DataPropertyName = "Operation_ID"
        Me.Operation_ID_CL.HeaderText = "Operation_ID"
        Me.Operation_ID_CL.Name = "Operation_ID_CL"
        Me.Operation_ID_CL.ReadOnly = True
        Me.Operation_ID_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Operation_ID_CL.Visible = False
        '
        'TotalQYT_txt
        '
        Me.TotalQYT_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TotalQYT_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalQYT_txt.ForeColor = System.Drawing.SystemColors.InfoText
        Me.TotalQYT_txt.Location = New System.Drawing.Point(2, 665)
        Me.TotalQYT_txt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.TotalQYT_txt.Name = "TotalQYT_txt"
        Me.TotalQYT_txt.ReadOnly = True
        Me.TotalQYT_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TotalQYT_txt.Size = New System.Drawing.Size(218, 29)
        Me.TotalQYT_txt.TabIndex = 653
        Me.TotalQYT_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SalarySearchButton
        '
        Me.SalarySearchButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.SalarySearchButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SalarySearchButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.SalarySearchButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.SalarySearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SalarySearchButton.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SalarySearchButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SalarySearchButton.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.SalarySearchButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SalarySearchButton.Location = New System.Drawing.Point(604, 1)
        Me.SalarySearchButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.SalarySearchButton.Name = "SalarySearchButton"
        Me.SalarySearchButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SalarySearchButton.Size = New System.Drawing.Size(98, 32)
        Me.SalarySearchButton.TabIndex = 659
        Me.SalarySearchButton.TabStop = False
        Me.SalarySearchButton.Text = "بحــــــث"
        Me.SalarySearchButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SalarySearchButton.UseVisualStyleBackColor = False
        '
        'User_Cm
        '
        Me.User_Cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.User_Cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.User_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.User_Cm.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.User_Cm.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.User_Cm.FormattingEnabled = True
        Me.User_Cm.Location = New System.Drawing.Point(203, 39)
        Me.User_Cm.Margin = New System.Windows.Forms.Padding(4)
        Me.User_Cm.Name = "User_Cm"
        Me.User_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.User_Cm.Size = New System.Drawing.Size(169, 25)
        Me.User_Cm.TabIndex = 661
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(377, 42)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 19)
        Me.Label4.TabIndex = 660
        Me.Label4.Text = "المستخدم"
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.Location = New System.Drawing.Point(870, 654)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ExitFormButton.Size = New System.Drawing.Size(133, 40)
        Me.ExitFormButton.TabIndex = 662
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(224, 671)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 19)
        Me.Label1.TabIndex = 664
        Me.Label1.Text = "عدد الحالات"
        '
        'CMSearchTextBox
        '
        Me.CMSearchTextBox.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.CMSearchTextBox.ForeColor = System.Drawing.SystemColors.InfoText
        Me.CMSearchTextBox.Location = New System.Drawing.Point(2, 39)
        Me.CMSearchTextBox.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
        Me.CMSearchTextBox.Name = "CMSearchTextBox"
        Me.CMSearchTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CMSearchTextBox.Size = New System.Drawing.Size(195, 26)
        Me.CMSearchTextBox.TabIndex = 665
        Me.CMSearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(720, 43)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(69, 19)
        Me.Label8.TabIndex = 667
        Me.Label8.Text = "نوع الحركة"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'OP_Cm
        '
        Me.OP_Cm.BackColor = System.Drawing.SystemColors.Window
        Me.OP_Cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.OP_Cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.OP_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OP_Cm.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.OP_Cm.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.OP_Cm.FormattingEnabled = True
        Me.OP_Cm.Location = New System.Drawing.Point(585, 40)
        Me.OP_Cm.Name = "OP_Cm"
        Me.OP_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.OP_Cm.Size = New System.Drawing.Size(131, 25)
        Me.OP_Cm.TabIndex = 666
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(946, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 19)
        Me.Label2.TabIndex = 669
        Me.Label2.Text = "الشاشة"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Screen_Cmb
        '
        Me.Screen_Cmb.BackColor = System.Drawing.SystemColors.Window
        Me.Screen_Cmb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Screen_Cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Screen_Cmb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Screen_Cmb.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Screen_Cmb.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Screen_Cmb.FormattingEnabled = True
        Me.Screen_Cmb.Location = New System.Drawing.Point(792, 39)
        Me.Screen_Cmb.Name = "Screen_Cmb"
        Me.Screen_Cmb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Screen_Cmb.Size = New System.Drawing.Size(149, 25)
        Me.Screen_Cmb.TabIndex = 668
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(817, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 21)
        Me.Label5.TabIndex = 673
        Me.Label5.Text = "إلى"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(972, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 21)
        Me.Label6.TabIndex = 672
        Me.Label6.Text = "من"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DateTimePicker_From
        '
        Me.DateTimePicker_From.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DateTimePicker_From.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePicker_From.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_From.Location = New System.Drawing.Point(858, 4)
        Me.DateTimePicker_From.Name = "DateTimePicker_From"
        Me.DateTimePicker_From.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DateTimePicker_From.Size = New System.Drawing.Size(111, 26)
        Me.DateTimePicker_From.TabIndex = 670
        '
        'DateTimePicker_To
        '
        Me.DateTimePicker_To.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DateTimePicker_To.CustomFormat = "yyyy-MM-dd"
        Me.DateTimePicker_To.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker_To.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_To.Location = New System.Drawing.Point(703, 4)
        Me.DateTimePicker_To.Name = "DateTimePicker_To"
        Me.DateTimePicker_To.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DateTimePicker_To.Size = New System.Drawing.Size(111, 26)
        Me.DateTimePicker_To.TabIndex = 671
        '
        'Num_Txt
        '
        Me.Num_Txt.Font = New System.Drawing.Font("Segoe UI", 10.25!)
        Me.Num_Txt.ForeColor = System.Drawing.SystemColors.InfoText
        Me.Num_Txt.Location = New System.Drawing.Point(447, 39)
        Me.Num_Txt.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
        Me.Num_Txt.Name = "Num_Txt"
        Me.Num_Txt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Num_Txt.Size = New System.Drawing.Size(92, 26)
        Me.Num_Txt.TabIndex = 674
        Me.Num_Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(542, 43)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 19)
        Me.Label3.TabIndex = 675
        Me.Label3.Text = "الرقم"
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold)
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.HorizontalScrollbar = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(2, 1)
        Me.CheckedListBox1.MultiColumn = True
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CheckedListBox1.Size = New System.Drawing.Size(55, 34)
        Me.CheckedListBox1.TabIndex = 908
        Me.CheckedListBox1.Visible = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button1.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(500, 1)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Button1.Size = New System.Drawing.Size(98, 32)
        Me.Button1.TabIndex = 909
        Me.Button1.TabStop = False
        Me.Button1.Text = "طباعة"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Network_Tracker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1004, 695)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Num_Txt)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.DateTimePicker_From)
        Me.Controls.Add(Me.DateTimePicker_To)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Screen_Cmb)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.OP_Cm)
        Me.Controls.Add(Me.CMSearchTextBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.User_Cm)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.SalarySearchButton)
        Me.Controls.Add(Me.DataGridViewX)
        Me.Controls.Add(Me.TotalQYT_txt)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Network_Tracker"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "مراقب الشبكــة"
        CType(Me.DataGridViewX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents DataGridViewX As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents TotalQYT_txt As System.Windows.Forms.TextBox
    Friend WithEvents SalarySearchButton As System.Windows.Forms.Button
    Friend WithEvents User_Cm As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CMSearchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents T_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IDX As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CP_NAME_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UserName_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Screen_Type_NAME_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Bill_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Operation_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Notes_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents USER_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Screen_Type_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Operation_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents OP_Cm As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Screen_Cmb As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker_From As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker_To As System.Windows.Forms.DateTimePicker
    Friend WithEvents Num_Txt As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CheckedListBox1 As System.Windows.Forms.CheckedListBox
    Friend WithEvents DataB As System.Windows.Forms.BindingSource
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
