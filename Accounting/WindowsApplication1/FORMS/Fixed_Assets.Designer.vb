<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Fixed_Assets
    Inherits Base_Form
    ' Inherits System.Windows.Forms.Form

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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.SalvageValue_Panel = New System.Windows.Forms.Panel()
        Me.SalvageValue = New Accounting.F2FloatField()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DepreciationRate_Panel = New System.Windows.Forms.Panel()
        Me.DepreciationRate = New Accounting.F2FloatField()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SEARCH_ACC_BTN = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Trans_DataGridView = New System.Windows.Forms.DataGridView()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DepreciationStartDate = New System.Windows.Forms.DateTimePicker()
        Me.PurchaseAmount = New Accounting.F2FloatField()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.AccumulatedDepreciationAccount = New System.Windows.Forms.TextBox()
        Me.EXP_B_Cm = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.DepreciationExpenseAccount = New System.Windows.Forms.TextBox()
        Me.ORG_B_Cm = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.YEAR_Panel = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.DATE_OF_YEAR = New System.Windows.Forms.DateTimePicker()
        Me.Month_Panel = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.DATE_OF_MONTH = New System.Windows.Forms.DateTimePicker()
        Me.lblExpectedEntries = New System.Windows.Forms.Label()
        Me.cmbDepreciationFrequency = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.DepreciationMethod = New System.Windows.Forms.ComboBox()
        Me.UsefulLifeYears = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Location = New System.Windows.Forms.ComboBox()
        Me.SerialNumber = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PurchaseDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.AssetGroupId = New System.Windows.Forms.ComboBox()
        Me.AssetDescription = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Depend_Btn = New System.Windows.Forms.Button()
        Me.NEW_Btn = New System.Windows.Forms.Button()
        Me.TITLE_txt = New System.Windows.Forms.Label()
        Me.Search_Txt = New System.Windows.Forms.TextBox()
        Me.DELETE_Btn = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SalvageValue_Panel.SuspendLayout()
        Me.DepreciationRate_Panel.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.Trans_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.YEAR_Panel.SuspendLayout()
        Me.Month_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 10.25!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.Location = New System.Drawing.Point(2, 28)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 30
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(354, 665)
        Me.DataGridView1.TabIndex = 40
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.SalvageValue_Panel)
        Me.GroupBox1.Controls.Add(Me.DepreciationRate_Panel)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.SEARCH_ACC_BTN)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.DepreciationStartDate)
        Me.GroupBox1.Controls.Add(Me.PurchaseAmount)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.DepreciationMethod)
        Me.GroupBox1.Controls.Add(Me.UsefulLifeYears)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Location)
        Me.GroupBox1.Controls.Add(Me.SerialNumber)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.PurchaseDate)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.AssetGroupId)
        Me.GroupBox1.Controls.Add(Me.AssetDescription)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.GroupBox1.Location = New System.Drawing.Point(363, 45)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(641, 600)
        Me.GroupBox1.TabIndex = 97
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "بطاقة الأصل:"
        '
        'SalvageValue_Panel
        '
        Me.SalvageValue_Panel.Controls.Add(Me.SalvageValue)
        Me.SalvageValue_Panel.Controls.Add(Me.Label10)
        Me.SalvageValue_Panel.Location = New System.Drawing.Point(8, 268)
        Me.SalvageValue_Panel.Name = "SalvageValue_Panel"
        Me.SalvageValue_Panel.Size = New System.Drawing.Size(203, 32)
        Me.SalvageValue_Panel.TabIndex = 114
        '
        'SalvageValue
        '
        Me.SalvageValue.BackColor = System.Drawing.Color.Lavender
        Me.SalvageValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SalvageValue.Font = New System.Drawing.Font("Arial", 13.25!, System.Drawing.FontStyle.Bold)
        Me.SalvageValue.Location = New System.Drawing.Point(2, 2)
        Me.SalvageValue.MaxLength = 0
        Me.SalvageValue.Name = "SalvageValue"
        Me.SalvageValue.Size = New System.Drawing.Size(85, 28)
        Me.SalvageValue.TabIndex = 115
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label10.ForeColor = System.Drawing.Color.DarkRed
        Me.Label10.Location = New System.Drawing.Point(90, 8)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(109, 17)
        Me.Label10.TabIndex = 422
        Me.Label10.Text = "القيمة التخريدية :"
        '
        'DepreciationRate_Panel
        '
        Me.DepreciationRate_Panel.Controls.Add(Me.DepreciationRate)
        Me.DepreciationRate_Panel.Controls.Add(Me.Label7)
        Me.DepreciationRate_Panel.Location = New System.Drawing.Point(212, 268)
        Me.DepreciationRate_Panel.Name = "DepreciationRate_Panel"
        Me.DepreciationRate_Panel.Size = New System.Drawing.Size(168, 32)
        Me.DepreciationRate_Panel.TabIndex = 112
        '
        'DepreciationRate
        '
        Me.DepreciationRate.BackColor = System.Drawing.Color.Lavender
        Me.DepreciationRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DepreciationRate.Font = New System.Drawing.Font("Arial", 13.25!, System.Drawing.FontStyle.Bold)
        Me.DepreciationRate.Location = New System.Drawing.Point(2, 2)
        Me.DepreciationRate.MaxLength = 0
        Me.DepreciationRate.Name = "DepreciationRate"
        Me.DepreciationRate.Size = New System.Drawing.Size(52, 28)
        Me.DepreciationRate.TabIndex = 113
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label7.ForeColor = System.Drawing.Color.DarkRed
        Me.Label7.Location = New System.Drawing.Point(57, 8)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label7.Size = New System.Drawing.Size(108, 17)
        Me.Label7.TabIndex = 436
        Me.Label7.Text = "نسبة الإهلاك% :"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button1.Location = New System.Drawing.Point(11, 103)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(38, 25)
        Me.Button1.TabIndex = 434
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = False
        '
        'SEARCH_ACC_BTN
        '
        Me.SEARCH_ACC_BTN.BackColor = System.Drawing.Color.White
        Me.SEARCH_ACC_BTN.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SEARCH_ACC_BTN.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SEARCH_ACC_BTN.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SEARCH_ACC_BTN.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SEARCH_ACC_BTN.Location = New System.Drawing.Point(223, 43)
        Me.SEARCH_ACC_BTN.Margin = New System.Windows.Forms.Padding(4)
        Me.SEARCH_ACC_BTN.Name = "SEARCH_ACC_BTN"
        Me.SEARCH_ACC_BTN.Size = New System.Drawing.Size(37, 25)
        Me.SEARCH_ACC_BTN.TabIndex = 433
        Me.SEARCH_ACC_BTN.Text = "..."
        Me.SEARCH_ACC_BTN.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Trans_DataGridView)
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Panel1.Location = New System.Drawing.Point(11, 374)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Panel1.Size = New System.Drawing.Size(624, 221)
        Me.Panel1.TabIndex = 432
        '
        'Trans_DataGridView
        '
        Me.Trans_DataGridView.AllowUserToAddRows = False
        Me.Trans_DataGridView.AllowUserToDeleteRows = False
        Me.Trans_DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.Trans_DataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.Trans_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 10.25!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Trans_DataGridView.DefaultCellStyle = DataGridViewCellStyle2
        Me.Trans_DataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Trans_DataGridView.Location = New System.Drawing.Point(0, 0)
        Me.Trans_DataGridView.Margin = New System.Windows.Forms.Padding(4)
        Me.Trans_DataGridView.MultiSelect = False
        Me.Trans_DataGridView.Name = "Trans_DataGridView"
        Me.Trans_DataGridView.ReadOnly = True
        Me.Trans_DataGridView.RowHeadersVisible = False
        Me.Trans_DataGridView.RowTemplate.Height = 30
        Me.Trans_DataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Trans_DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Trans_DataGridView.Size = New System.Drawing.Size(624, 221)
        Me.Trans_DataGridView.TabIndex = 431
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label6.Location = New System.Drawing.Point(495, 244)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(135, 17)
        Me.Label6.TabIndex = 430
        Me.Label6.Text = "تاريخ بداية الإحتساب :"
        '
        'DepreciationStartDate
        '
        Me.DepreciationStartDate.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.DepreciationStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DepreciationStartDate.Location = New System.Drawing.Point(365, 239)
        Me.DepreciationStartDate.Margin = New System.Windows.Forms.Padding(4)
        Me.DepreciationStartDate.Name = "DepreciationStartDate"
        Me.DepreciationStartDate.Size = New System.Drawing.Size(126, 24)
        Me.DepreciationStartDate.TabIndex = 109
        '
        'PurchaseAmount
        '
        Me.PurchaseAmount.BackColor = System.Drawing.Color.Lavender
        Me.PurchaseAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PurchaseAmount.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.PurchaseAmount.Location = New System.Drawing.Point(340, 71)
        Me.PurchaseAmount.MaxLength = 0
        Me.PurchaseAmount.Name = "PurchaseAmount"
        Me.PurchaseAmount.Size = New System.Drawing.Size(161, 26)
        Me.PurchaseAmount.TabIndex = 100
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.AccumulatedDepreciationAccount)
        Me.GroupBox4.Controls.Add(Me.EXP_B_Cm)
        Me.GroupBox4.Location = New System.Drawing.Point(13, 182)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(621, 50)
        Me.GroupBox4.TabIndex = 106
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "رقم حساب مجمع الإستهلاك في الدليل :"
        '
        'AccumulatedDepreciationAccount
        '
        Me.AccumulatedDepreciationAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AccumulatedDepreciationAccount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.AccumulatedDepreciationAccount.Location = New System.Drawing.Point(449, 20)
        Me.AccumulatedDepreciationAccount.Margin = New System.Windows.Forms.Padding(4)
        Me.AccumulatedDepreciationAccount.Name = "AccumulatedDepreciationAccount"
        Me.AccumulatedDepreciationAccount.Size = New System.Drawing.Size(163, 25)
        Me.AccumulatedDepreciationAccount.TabIndex = 107
        '
        'EXP_B_Cm
        '
        Me.EXP_B_Cm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.EXP_B_Cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.EXP_B_Cm.BackColor = System.Drawing.Color.Gainsboro
        Me.EXP_B_Cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EXP_B_Cm.DropDownHeight = 500
        Me.EXP_B_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EXP_B_Cm.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.EXP_B_Cm.FormattingEnabled = True
        Me.EXP_B_Cm.IntegralHeight = False
        Me.EXP_B_Cm.Location = New System.Drawing.Point(7, 19)
        Me.EXP_B_Cm.Margin = New System.Windows.Forms.Padding(4)
        Me.EXP_B_Cm.Name = "EXP_B_Cm"
        Me.EXP_B_Cm.Size = New System.Drawing.Size(439, 27)
        Me.EXP_B_Cm.TabIndex = 108
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.DepreciationExpenseAccount)
        Me.GroupBox3.Controls.Add(Me.ORG_B_Cm)
        Me.GroupBox3.Location = New System.Drawing.Point(13, 131)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(621, 50)
        Me.GroupBox3.TabIndex = 103
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "رقم حساب مصروف الإستهلاك في الدليل :"
        '
        'DepreciationExpenseAccount
        '
        Me.DepreciationExpenseAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DepreciationExpenseAccount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.DepreciationExpenseAccount.Location = New System.Drawing.Point(449, 21)
        Me.DepreciationExpenseAccount.Margin = New System.Windows.Forms.Padding(4)
        Me.DepreciationExpenseAccount.Name = "DepreciationExpenseAccount"
        Me.DepreciationExpenseAccount.Size = New System.Drawing.Size(163, 25)
        Me.DepreciationExpenseAccount.TabIndex = 104
        '
        'ORG_B_Cm
        '
        Me.ORG_B_Cm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ORG_B_Cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ORG_B_Cm.BackColor = System.Drawing.Color.Gainsboro
        Me.ORG_B_Cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ORG_B_Cm.DropDownHeight = 500
        Me.ORG_B_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ORG_B_Cm.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.ORG_B_Cm.FormattingEnabled = True
        Me.ORG_B_Cm.IntegralHeight = False
        Me.ORG_B_Cm.Location = New System.Drawing.Point(7, 20)
        Me.ORG_B_Cm.Margin = New System.Windows.Forms.Padding(4)
        Me.ORG_B_Cm.Name = "ORG_B_Cm"
        Me.ORG_B_Cm.Size = New System.Drawing.Size(439, 27)
        Me.ORG_B_Cm.TabIndex = 105
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.YEAR_Panel)
        Me.GroupBox2.Controls.Add(Me.Month_Panel)
        Me.GroupBox2.Controls.Add(Me.lblExpectedEntries)
        Me.GroupBox2.Controls.Add(Me.cmbDepreciationFrequency)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 299)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(628, 73)
        Me.GroupBox2.TabIndex = 113
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "تاريخ إحتساب قسط الإهلاك :"
        '
        'YEAR_Panel
        '
        Me.YEAR_Panel.Controls.Add(Me.Label11)
        Me.YEAR_Panel.Controls.Add(Me.DATE_OF_YEAR)
        Me.YEAR_Panel.Location = New System.Drawing.Point(157, 12)
        Me.YEAR_Panel.Name = "YEAR_Panel"
        Me.YEAR_Panel.Size = New System.Drawing.Size(163, 54)
        Me.YEAR_Panel.TabIndex = 431
        Me.YEAR_Panel.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label11.Location = New System.Drawing.Point(4, 4)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(154, 13)
        Me.Label11.TabIndex = 106
        Me.Label11.Text = "تاريخ الإحتساب كل يوم فالشهر :"
        '
        'DATE_OF_YEAR
        '
        Me.DATE_OF_YEAR.CustomFormat = "dd-MM-"
        Me.DATE_OF_YEAR.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.DATE_OF_YEAR.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DATE_OF_YEAR.Location = New System.Drawing.Point(2, 26)
        Me.DATE_OF_YEAR.Margin = New System.Windows.Forms.Padding(4)
        Me.DATE_OF_YEAR.Name = "DATE_OF_YEAR"
        Me.DATE_OF_YEAR.Size = New System.Drawing.Size(87, 24)
        Me.DATE_OF_YEAR.TabIndex = 101
        '
        'Month_Panel
        '
        Me.Month_Panel.Controls.Add(Me.Label12)
        Me.Month_Panel.Controls.Add(Me.DATE_OF_MONTH)
        Me.Month_Panel.Location = New System.Drawing.Point(4, 12)
        Me.Month_Panel.Name = "Month_Panel"
        Me.Month_Panel.Size = New System.Drawing.Size(152, 54)
        Me.Month_Panel.TabIndex = 432
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.Label12.Location = New System.Drawing.Point(4, 5)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(117, 13)
        Me.Label12.TabIndex = 108
        Me.Label12.Text = "تاريخ الإحتساب كل يوم :"
        '
        'DATE_OF_MONTH
        '
        Me.DATE_OF_MONTH.CustomFormat = "dd-"
        Me.DATE_OF_MONTH.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.DATE_OF_MONTH.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DATE_OF_MONTH.Location = New System.Drawing.Point(3, 26)
        Me.DATE_OF_MONTH.Margin = New System.Windows.Forms.Padding(4)
        Me.DATE_OF_MONTH.Name = "DATE_OF_MONTH"
        Me.DATE_OF_MONTH.Size = New System.Drawing.Size(87, 24)
        Me.DATE_OF_MONTH.TabIndex = 107
        '
        'lblExpectedEntries
        '
        Me.lblExpectedEntries.AutoSize = True
        Me.lblExpectedEntries.Font = New System.Drawing.Font("Tahoma", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExpectedEntries.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblExpectedEntries.Location = New System.Drawing.Point(325, 45)
        Me.lblExpectedEntries.Name = "lblExpectedEntries"
        Me.lblExpectedEntries.Size = New System.Drawing.Size(31, 21)
        Me.lblExpectedEntries.TabIndex = 431
        Me.lblExpectedEntries.Text = "---"
        '
        'cmbDepreciationFrequency
        '
        Me.cmbDepreciationFrequency.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbDepreciationFrequency.BackColor = System.Drawing.SystemColors.Info
        Me.cmbDepreciationFrequency.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cmbDepreciationFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDepreciationFrequency.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbDepreciationFrequency.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDepreciationFrequency.FormattingEnabled = True
        Me.cmbDepreciationFrequency.Items.AddRange(New Object() {"سنوي", "شهري"})
        Me.cmbDepreciationFrequency.Location = New System.Drawing.Point(467, 20)
        Me.cmbDepreciationFrequency.Name = "cmbDepreciationFrequency"
        Me.cmbDepreciationFrequency.Size = New System.Drawing.Size(156, 26)
        Me.cmbDepreciationFrequency.TabIndex = 116
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label9.Location = New System.Drawing.Point(520, 276)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(114, 17)
        Me.Label9.TabIndex = 421
        Me.Label9.Text = "طريقة الإستهلاك :"
        '
        'DepreciationMethod
        '
        Me.DepreciationMethod.BackColor = System.Drawing.SystemColors.Info
        Me.DepreciationMethod.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DepreciationMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DepreciationMethod.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DepreciationMethod.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DepreciationMethod.FormattingEnabled = True
        Me.DepreciationMethod.Items.AddRange(New Object() {"القسط الثابت", "القسط المتناقص"})
        Me.DepreciationMethod.Location = New System.Drawing.Point(387, 271)
        Me.DepreciationMethod.Name = "DepreciationMethod"
        Me.DepreciationMethod.Size = New System.Drawing.Size(127, 26)
        Me.DepreciationMethod.TabIndex = 111
        '
        'UsefulLifeYears
        '
        Me.UsefulLifeYears.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UsefulLifeYears.Font = New System.Drawing.Font("Arial", 13.25!, System.Drawing.FontStyle.Bold)
        Me.UsefulLifeYears.Location = New System.Drawing.Point(8, 238)
        Me.UsefulLifeYears.Margin = New System.Windows.Forms.Padding(4)
        Me.UsefulLifeYears.Name = "UsefulLifeYears"
        Me.UsefulLifeYears.Size = New System.Drawing.Size(100, 28)
        Me.UsefulLifeYears.TabIndex = 110
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label8.Location = New System.Drawing.Point(111, 244)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(200, 17)
        Me.Label8.TabIndex = 419
        Me.Label8.Text = "العمر الإفتراضي للأصل (بالسنة) :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label5.Location = New System.Drawing.Point(472, 106)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(113, 17)
        Me.Label5.TabIndex = 107
        Me.Label5.Text = "مكان وجود الأصل :"
        '
        'Location
        '
        Me.Location.BackColor = System.Drawing.SystemColors.Info
        Me.Location.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Location.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Location.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Location.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location.FormattingEnabled = True
        Me.Location.Location = New System.Drawing.Point(51, 103)
        Me.Location.Name = "Location"
        Me.Location.Size = New System.Drawing.Size(416, 26)
        Me.Location.TabIndex = 102
        '
        'SerialNumber
        '
        Me.SerialNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SerialNumber.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.SerialNumber.Location = New System.Drawing.Point(11, 71)
        Me.SerialNumber.Margin = New System.Windows.Forms.Padding(4)
        Me.SerialNumber.Name = "SerialNumber"
        Me.SerialNumber.Size = New System.Drawing.Size(198, 26)
        Me.SerialNumber.TabIndex = 101
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label4.Location = New System.Drawing.Point(213, 77)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(116, 17)
        Me.Label4.TabIndex = 105
        Me.Label4.Text = "الرقم التسلسلي :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label14.Location = New System.Drawing.Point(504, 77)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(135, 17)
        Me.Label14.TabIndex = 102
        Me.Label14.Text = "مبلغ الشراء (التكلفة) :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label3.Location = New System.Drawing.Point(131, 47)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 17)
        Me.Label3.TabIndex = 101
        Me.Label3.Text = "تاريخ الشراء :"
        '
        'PurchaseDate
        '
        Me.PurchaseDate.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.PurchaseDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.PurchaseDate.Location = New System.Drawing.Point(11, 42)
        Me.PurchaseDate.Margin = New System.Windows.Forms.Padding(4)
        Me.PurchaseDate.Name = "PurchaseDate"
        Me.PurchaseDate.Size = New System.Drawing.Size(117, 24)
        Me.PurchaseDate.TabIndex = 99
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label2.Location = New System.Drawing.Point(538, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 17)
        Me.Label2.TabIndex = 99
        Me.Label2.Text = "مجموعة الأصل :"
        '
        'AssetGroupId
        '
        Me.AssetGroupId.BackColor = System.Drawing.SystemColors.Info
        Me.AssetGroupId.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AssetGroupId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AssetGroupId.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AssetGroupId.Font = New System.Drawing.Font("Tahoma", 10.25!, System.Drawing.FontStyle.Bold)
        Me.AssetGroupId.FormattingEnabled = True
        Me.AssetGroupId.Location = New System.Drawing.Point(262, 43)
        Me.AssetGroupId.Name = "AssetGroupId"
        Me.AssetGroupId.Size = New System.Drawing.Size(271, 25)
        Me.AssetGroupId.TabIndex = 98
        '
        'AssetDescription
        '
        Me.AssetDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AssetDescription.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.AssetDescription.Location = New System.Drawing.Point(9, 15)
        Me.AssetDescription.Margin = New System.Windows.Forms.Padding(4)
        Me.AssetDescription.Name = "AssetDescription"
        Me.AssetDescription.Size = New System.Drawing.Size(534, 24)
        Me.AssetDescription.TabIndex = 97
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label1.Location = New System.Drawing.Point(546, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 17)
        Me.Label1.TabIndex = 96
        Me.Label1.Text = "وصف الصنف :"
        '
        'Depend_Btn
        '
        Me.Depend_Btn.BackColor = System.Drawing.Color.White
        Me.Depend_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Depend_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Depend_Btn.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Depend_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Depend_Btn.Location = New System.Drawing.Point(582, 646)
        Me.Depend_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.Depend_Btn.Name = "Depend_Btn"
        Me.Depend_Btn.Size = New System.Drawing.Size(208, 48)
        Me.Depend_Btn.TabIndex = 429
        Me.Depend_Btn.Text = "✔️  إعتماد الأصـــــل"
        Me.Depend_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Depend_Btn.UseVisualStyleBackColor = False
        '
        'NEW_Btn
        '
        Me.NEW_Btn.BackColor = System.Drawing.Color.White
        Me.NEW_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.NEW_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.NEW_Btn.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NEW_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.NEW_Btn.Location = New System.Drawing.Point(791, 646)
        Me.NEW_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.NEW_Btn.Name = "NEW_Btn"
        Me.NEW_Btn.Size = New System.Drawing.Size(208, 48)
        Me.NEW_Btn.TabIndex = 430
        Me.NEW_Btn.Text = "➕ إضافة أصل جديد"
        Me.NEW_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NEW_Btn.UseVisualStyleBackColor = False
        '
        'TITLE_txt
        '
        Me.TITLE_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TITLE_txt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TITLE_txt.Font = New System.Drawing.Font("Arial", 17.0!, System.Drawing.FontStyle.Bold)
        Me.TITLE_txt.Location = New System.Drawing.Point(364, 4)
        Me.TITLE_txt.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.TITLE_txt.Name = "TITLE_txt"
        Me.TITLE_txt.Size = New System.Drawing.Size(640, 40)
        Me.TITLE_txt.TabIndex = 431
        Me.TITLE_txt.Text = "الأصول الثابتة"
        Me.TITLE_txt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Search_Txt
        '
        Me.Search_Txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Search_Txt.Location = New System.Drawing.Point(2, 3)
        Me.Search_Txt.Name = "Search_Txt"
        Me.Search_Txt.Size = New System.Drawing.Size(354, 23)
        Me.Search_Txt.TabIndex = 432
        '
        'DELETE_Btn
        '
        Me.DELETE_Btn.BackColor = System.Drawing.Color.White
        Me.DELETE_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DELETE_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DELETE_Btn.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DELETE_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DELETE_Btn.Location = New System.Drawing.Point(373, 646)
        Me.DELETE_Btn.Margin = New System.Windows.Forms.Padding(4)
        Me.DELETE_Btn.Name = "DELETE_Btn"
        Me.DELETE_Btn.Size = New System.Drawing.Size(208, 48)
        Me.DELETE_Btn.TabIndex = 433
        Me.DELETE_Btn.Text = "❌  حـــذف الأصــــل"
        Me.DELETE_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DELETE_Btn.UseVisualStyleBackColor = False
        '
        'Fixed_Assets
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 695)
        Me.Controls.Add(Me.DELETE_Btn)
        Me.Controls.Add(Me.Search_Txt)
        Me.Controls.Add(Me.TITLE_txt)
        Me.Controls.Add(Me.NEW_Btn)
        Me.Controls.Add(Me.Depend_Btn)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Fixed_Assets"
        Me.Text = "الأصول الثابتة"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.SalvageValue_Panel.ResumeLayout(False)
        Me.SalvageValue_Panel.PerformLayout()
        Me.DepreciationRate_Panel.ResumeLayout(False)
        Me.DepreciationRate_Panel.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.Trans_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.YEAR_Panel.ResumeLayout(False)
        Me.YEAR_Panel.PerformLayout()
        Me.Month_Panel.ResumeLayout(False)
        Me.Month_Panel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents AssetDescription As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents AssetGroupId As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents PurchaseDate As DateTimePicker
    Friend WithEvents Label14 As Label
    Friend WithEvents SerialNumber As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Location As ComboBox
    Friend WithEvents ORG_B_Cm As ComboBox
    Friend WithEvents UsefulLifeYears As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents DepreciationMethod As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label12 As Label
    Friend WithEvents DATE_OF_MONTH As DateTimePicker
    Friend WithEvents Label11 As Label
    Friend WithEvents DATE_OF_YEAR As DateTimePicker
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents AccumulatedDepreciationAccount As TextBox
    Friend WithEvents EXP_B_Cm As ComboBox
    Friend WithEvents DepreciationExpenseAccount As TextBox
    Friend WithEvents PurchaseAmount As F2FloatField
    Friend WithEvents SalvageValue As F2FloatField
    Friend WithEvents Depend_Btn As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents DepreciationStartDate As DateTimePicker
    Friend WithEvents lblExpectedEntries As Label
    Friend WithEvents cmbDepreciationFrequency As ComboBox
    Friend WithEvents NEW_Btn As Button
    Friend WithEvents Trans_DataGridView As DataGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents YEAR_Panel As Panel
    Friend WithEvents Month_Panel As Panel
    Friend WithEvents TITLE_txt As Label
    Friend WithEvents SEARCH_ACC_BTN As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Search_Txt As TextBox
    Friend WithEvents DELETE_Btn As Button
    Friend WithEvents DepreciationRate As F2FloatField
    Friend WithEvents Label7 As Label
    Friend WithEvents SalvageValue_Panel As Panel
    Friend WithEvents DepreciationRate_Panel As Panel
End Class
