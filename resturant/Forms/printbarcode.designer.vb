<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class printbarcode
    Inherits Form

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
    Public components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Public Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(printbarcode))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.width = New System.Windows.Forms.NumericUpDown()
        Me.hieght = New System.Windows.Forms.NumericUpDown()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtbarcode = New System.Windows.Forms.TextBox()
        Me.الباركود = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.MarginPLeft = New System.Windows.Forms.NumericUpDown()
        Me.MarginPUP = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.MarginBUp = New System.Windows.Forms.NumericUpDown()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.PRINT_IM_NUM_CB = New System.Windows.Forms.CheckBox()
        Me.Print_IM_NAME_CB = New System.Windows.Forms.CheckBox()
        Me.Print_Price_CB = New System.Windows.Forms.CheckBox()
        Me.nud_Count = New System.Windows.Forms.TextBox()
        Me.Print_CPName_CB = New System.Windows.Forms.CheckBox()
        Me.A4GroupBox = New System.Windows.Forms.GroupBox()
        Me.txtrow = New System.Windows.Forms.NumericUpDown()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cbA4 = New System.Windows.Forms.CheckBox()
        Me.txtcol = New System.Windows.Forms.NumericUpDown()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.IM_Num_txt = New System.Windows.Forms.TextBox()
        Me.IM_Unit_cm = New System.Windows.Forms.ComboBox()
        Me.txtprice = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.IMDataGridViewX = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.IM_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.U_IM_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.item_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Barcode_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.U_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_SH_txt = New System.Windows.Forms.TextBox()
        Me.Barcode_DefPrinter_Cm = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Show_IM_btn = New System.Windows.Forms.Button()
        Me.Saveformat = New System.Windows.Forms.Button()
        Me.Showing = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.width, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.hieght, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MarginPLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MarginPUP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MarginBUp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.A4GroupBox.SuspendLayout()
        CType(Me.txtrow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtcol, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.IMDataGridViewX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'width
        '
        Me.width.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.width.DecimalPlaces = 2
        Me.width.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.width.Increment = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.width.Location = New System.Drawing.Point(390, 46)
        Me.width.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.width.Name = "width"
        Me.width.Size = New System.Drawing.Size(78, 22)
        Me.width.TabIndex = 4
        Me.width.Value = New Decimal(New Integer() {508, 0, 0, 131072})
        '
        'hieght
        '
        Me.hieght.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.hieght.DecimalPlaces = 2
        Me.hieght.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hieght.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.hieght.Increment = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.hieght.Location = New System.Drawing.Point(390, 21)
        Me.hieght.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.hieght.Name = "hieght"
        Me.hieght.Size = New System.Drawing.Size(78, 22)
        Me.hieght.TabIndex = 3
        Me.hieght.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label12
        '
        Me.Label12.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("JF Flat", 9.0!)
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(474, 22)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(109, 21)
        Me.Label12.TabIndex = 112
        Me.Label12.Text = "ارتفاع ورقة الباركود"
        '
        'Label13
        '
        Me.Label13.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("JF Flat", 9.0!)
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(472, 46)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(103, 21)
        Me.Label13.TabIndex = 110
        Me.Label13.Text = "عرض ورقة الباركود"
        '
        'txtbarcode
        '
        Me.txtbarcode.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbarcode.Location = New System.Drawing.Point(375, 24)
        Me.txtbarcode.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtbarcode.Name = "txtbarcode"
        Me.txtbarcode.Size = New System.Drawing.Size(157, 29)
        Me.txtbarcode.TabIndex = 1
        '
        'الباركود
        '
        Me.الباركود.AutoSize = True
        Me.الباركود.BackColor = System.Drawing.Color.Transparent
        Me.الباركود.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.الباركود.ForeColor = System.Drawing.Color.Black
        Me.الباركود.Location = New System.Drawing.Point(536, 27)
        Me.الباركود.Name = "الباركود"
        Me.الباركود.Size = New System.Drawing.Size(67, 24)
        Me.الباركود.TabIndex = 116
        Me.الباركود.Text = "الباركود:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(746, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 24)
        Me.Label1.TabIndex = 119
        Me.Label1.Text = "الصنف"
        '
        'Label10
        '
        Me.Label10.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("JF Flat", 9.0!)
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(326, 21)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(60, 21)
        Me.Label10.TabIndex = 129
        Me.Label10.Text = "(CM-سم)"
        '
        'Label11
        '
        Me.Label11.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("JF Flat", 9.0!)
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(327, 46)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 21)
        Me.Label11.TabIndex = 130
        Me.Label11.Text = "(CM-سم)"
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("JF Flat", 9.0!)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(316, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 21)
        Me.Label2.TabIndex = 132
        Me.Label2.Text = "(1 ~ 1000)"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(398, 124)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 24)
        Me.Label3.TabIndex = 133
        Me.Label3.Text = "عدد الملصقات"
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("JF Flat", 9.0!)
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(10, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 21)
        Me.Label4.TabIndex = 149
        Me.Label4.Text = "(CM-سم)"
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("JF Flat", 9.0!)
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(10, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 21)
        Me.Label5.TabIndex = 148
        Me.Label5.Text = "(CM-سم)"
        '
        'MarginPLeft
        '
        Me.MarginPLeft.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.MarginPLeft.DecimalPlaces = 2
        Me.MarginPLeft.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MarginPLeft.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.MarginPLeft.Location = New System.Drawing.Point(75, 45)
        Me.MarginPLeft.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.MarginPLeft.Name = "MarginPLeft"
        Me.MarginPLeft.Size = New System.Drawing.Size(83, 22)
        Me.MarginPLeft.TabIndex = 11
        Me.MarginPLeft.Value = New Decimal(New Integer() {4, 0, 0, 65536})
        '
        'MarginPUP
        '
        Me.MarginPUP.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.MarginPUP.DecimalPlaces = 2
        Me.MarginPUP.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MarginPUP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.MarginPUP.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.MarginPUP.Location = New System.Drawing.Point(73, 18)
        Me.MarginPUP.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.MarginPUP.Name = "MarginPUP"
        Me.MarginPUP.Size = New System.Drawing.Size(85, 22)
        Me.MarginPUP.TabIndex = 10
        Me.MarginPUP.Value = New Decimal(New Integer() {4, 0, 0, 65536})
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("JF Flat", 9.0!)
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(161, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(143, 21)
        Me.Label6.TabIndex = 145
        Me.Label6.Text = "المحاذاة من الاعلى للورقة"
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("JF Flat", 9.0!)
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(163, 46)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(119, 21)
        Me.Label7.TabIndex = 144
        Me.Label7.Text = "المحاذاة لليسار للورقة"
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("JF Flat", 9.0!)
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(9, 75)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 21)
        Me.Label9.TabIndex = 154
        Me.Label9.Text = "(CM-سم)"
        '
        'MarginBUp
        '
        Me.MarginBUp.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.MarginBUp.DecimalPlaces = 2
        Me.MarginBUp.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MarginBUp.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.MarginBUp.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.MarginBUp.Location = New System.Drawing.Point(75, 75)
        Me.MarginBUp.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.MarginBUp.Name = "MarginBUp"
        Me.MarginBUp.Size = New System.Drawing.Size(85, 22)
        Me.MarginBUp.TabIndex = 12
        '
        'Label14
        '
        Me.Label14.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("JF Flat", 9.0!)
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(164, 76)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(149, 21)
        Me.Label14.TabIndex = 151
        Me.Label14.Text = "المحاذاة من الاعلى للباركود"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.PRINT_IM_NUM_CB)
        Me.GroupBox1.Controls.Add(Me.Print_IM_NAME_CB)
        Me.GroupBox1.Controls.Add(Me.Print_Price_CB)
        Me.GroupBox1.Controls.Add(Me.nud_Count)
        Me.GroupBox1.Controls.Add(Me.Print_CPName_CB)
        Me.GroupBox1.Controls.Add(Me.A4GroupBox)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.ExitFormButton)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 43)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(794, 562)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'PRINT_IM_NUM_CB
        '
        Me.PRINT_IM_NUM_CB.AutoSize = True
        Me.PRINT_IM_NUM_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PRINT_IM_NUM_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.PRINT_IM_NUM_CB.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PRINT_IM_NUM_CB.Location = New System.Drawing.Point(84, 123)
        Me.PRINT_IM_NUM_CB.Name = "PRINT_IM_NUM_CB"
        Me.PRINT_IM_NUM_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PRINT_IM_NUM_CB.Size = New System.Drawing.Size(104, 20)
        Me.PRINT_IM_NUM_CB.TabIndex = 632
        Me.PRINT_IM_NUM_CB.Text = "طباعة رقم الصنف"
        Me.PRINT_IM_NUM_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.PRINT_IM_NUM_CB.UseVisualStyleBackColor = True
        Me.PRINT_IM_NUM_CB.Visible = False
        '
        'Print_IM_NAME_CB
        '
        Me.Print_IM_NAME_CB.AutoSize = True
        Me.Print_IM_NAME_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_IM_NAME_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Print_IM_NAME_CB.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Print_IM_NAME_CB.Location = New System.Drawing.Point(82, 96)
        Me.Print_IM_NAME_CB.Name = "Print_IM_NAME_CB"
        Me.Print_IM_NAME_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_IM_NAME_CB.Size = New System.Drawing.Size(105, 20)
        Me.Print_IM_NAME_CB.TabIndex = 631
        Me.Print_IM_NAME_CB.Text = "طباعة اسم الصنف"
        Me.Print_IM_NAME_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Print_IM_NAME_CB.UseVisualStyleBackColor = True
        '
        'Print_Price_CB
        '
        Me.Print_Price_CB.AutoSize = True
        Me.Print_Price_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_Price_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Print_Price_CB.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Print_Price_CB.Location = New System.Drawing.Point(108, 150)
        Me.Print_Price_CB.Name = "Print_Price_CB"
        Me.Print_Price_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_Price_CB.Size = New System.Drawing.Size(80, 20)
        Me.Print_Price_CB.TabIndex = 630
        Me.Print_Price_CB.Text = "طباعة السعر"
        Me.Print_Price_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Print_Price_CB.UseVisualStyleBackColor = True
        '
        'nud_Count
        '
        Me.nud_Count.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_Count.Location = New System.Drawing.Point(284, 121)
        Me.nud_Count.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.nud_Count.Name = "nud_Count"
        Me.nud_Count.Size = New System.Drawing.Size(110, 29)
        Me.nud_Count.TabIndex = 629
        Me.nud_Count.Text = "1"
        '
        'Print_CPName_CB
        '
        Me.Print_CPName_CB.AutoSize = True
        Me.Print_CPName_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_CPName_CB.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Print_CPName_CB.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Print_CPName_CB.Location = New System.Drawing.Point(89, 179)
        Me.Print_CPName_CB.Name = "Print_CPName_CB"
        Me.Print_CPName_CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_CPName_CB.Size = New System.Drawing.Size(99, 20)
        Me.Print_CPName_CB.TabIndex = 628
        Me.Print_CPName_CB.Text = "طباعة اسم المحل"
        Me.Print_CPName_CB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Print_CPName_CB.UseVisualStyleBackColor = True
        '
        'A4GroupBox
        '
        Me.A4GroupBox.Controls.Add(Me.txtrow)
        Me.A4GroupBox.Controls.Add(Me.Label8)
        Me.A4GroupBox.Controls.Add(Me.cbA4)
        Me.A4GroupBox.Controls.Add(Me.txtcol)
        Me.A4GroupBox.Controls.Add(Me.Label15)
        Me.A4GroupBox.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.A4GroupBox.Location = New System.Drawing.Point(2, 205)
        Me.A4GroupBox.Name = "A4GroupBox"
        Me.A4GroupBox.Size = New System.Drawing.Size(195, 83)
        Me.A4GroupBox.TabIndex = 6
        Me.A4GroupBox.TabStop = False
        Me.A4GroupBox.Text = "طباعة الباركود على ورق A4"
        '
        'txtrow
        '
        Me.txtrow.Enabled = False
        Me.txtrow.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtrow.Location = New System.Drawing.Point(8, 29)
        Me.txtrow.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.txtrow.Name = "txtrow"
        Me.txtrow.Size = New System.Drawing.Size(81, 22)
        Me.txtrow.TabIndex = 9
        Me.txtrow.Value = New Decimal(New Integer() {13, 0, 0, 0})
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Enabled = False
        Me.Label8.Font = New System.Drawing.Font("JF Flat", 9.0!)
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(92, 30)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(78, 21)
        Me.Label8.TabIndex = 160
        Me.Label8.Text = "عدد الصفوف"
        '
        'cbA4
        '
        Me.cbA4.AutoSize = True
        Me.cbA4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.cbA4.Location = New System.Drawing.Point(2, 3)
        Me.cbA4.Name = "cbA4"
        Me.cbA4.Size = New System.Drawing.Size(15, 14)
        Me.cbA4.TabIndex = 7
        Me.cbA4.UseVisualStyleBackColor = True
        '
        'txtcol
        '
        Me.txtcol.Enabled = False
        Me.txtcol.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcol.Location = New System.Drawing.Point(8, 54)
        Me.txtcol.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.txtcol.Name = "txtcol"
        Me.txtcol.Size = New System.Drawing.Size(81, 22)
        Me.txtcol.TabIndex = 8
        Me.txtcol.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Enabled = False
        Me.Label15.Font = New System.Drawing.Font("JF Flat", 9.0!)
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(92, 54)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(74, 21)
        Me.Label15.TabIndex = 158
        Me.Label15.Text = "عدد الاعمدة"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Saveformat)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.MarginBUp)
        Me.GroupBox4.Controls.Add(Me.Showing)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.MarginPLeft)
        Me.GroupBox4.Controls.Add(Me.MarginPUP)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.width)
        Me.GroupBox4.Controls.Add(Me.hieght)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Controls.Add(Me.Label13)
        Me.GroupBox4.Enabled = False
        Me.GroupBox4.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(202, 142)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(586, 146)
        Me.GroupBox4.TabIndex = 157
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "اعدادات الطباعة"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Controls.Add(Me.IM_Num_txt)
        Me.GroupBox3.Controls.Add(Me.IM_Unit_cm)
        Me.GroupBox3.Controls.Add(Me.txtprice)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.txtbarcode)
        Me.GroupBox3.Controls.Add(Me.الباركود)
        Me.GroupBox3.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.GroupBox3.Location = New System.Drawing.Point(184, -3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(609, 96)
        Me.GroupBox3.TabIndex = 156
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "الصنف"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(509, 60)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(94, 26)
        Me.Label19.TabIndex = 632
        Me.Label19.Text = "رقم الصنف"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label17.Location = New System.Drawing.Point(140, 26)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(57, 24)
        Me.Label17.TabIndex = 624
        Me.Label17.Text = "الوحدة"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'IM_Num_txt
        '
        Me.IM_Num_txt.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Num_txt.Location = New System.Drawing.Point(285, 59)
        Me.IM_Num_txt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.IM_Num_txt.Name = "IM_Num_txt"
        Me.IM_Num_txt.Size = New System.Drawing.Size(220, 29)
        Me.IM_Num_txt.TabIndex = 631
        Me.IM_Num_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'IM_Unit_cm
        '
        Me.IM_Unit_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_Unit_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.IM_Unit_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_Unit_cm.Font = New System.Drawing.Font("JF Flat", 10.0!)
        Me.IM_Unit_cm.FormattingEnabled = True
        Me.IM_Unit_cm.Location = New System.Drawing.Point(20, 22)
        Me.IM_Unit_cm.Name = "IM_Unit_cm"
        Me.IM_Unit_cm.Size = New System.Drawing.Size(114, 31)
        Me.IM_Unit_cm.TabIndex = 623
        '
        'txtprice
        '
        Me.txtprice.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprice.Location = New System.Drawing.Point(206, 24)
        Me.txtprice.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtprice.Name = "txtprice"
        Me.txtprice.ReadOnly = True
        Me.txtprice.Size = New System.Drawing.Size(110, 29)
        Me.txtprice.TabIndex = 121
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(320, 26)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(47, 24)
        Me.Label16.TabIndex = 122
        Me.Label16.Text = "السعر"
        '
        'IMDataGridViewX
        '
        Me.IMDataGridViewX.AllowUserToAddRows = False
        Me.IMDataGridViewX.AllowUserToDeleteRows = False
        Me.IMDataGridViewX.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.IMDataGridViewX.BackgroundColor = System.Drawing.Color.White
        Me.IMDataGridViewX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IMDataGridViewX.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.IMDataGridViewX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.IMDataGridViewX.ColumnHeadersVisible = False
        Me.IMDataGridViewX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IM_ID_CL, Me.U_IM_ID_CL, Me.item_name_CL, Me.Barcode_CL, Me.U_Name_CL})
        Me.IMDataGridViewX.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IMDataGridViewX.DefaultCellStyle = DataGridViewCellStyle4
        Me.IMDataGridViewX.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.IMDataGridViewX.Location = New System.Drawing.Point(349, 35)
        Me.IMDataGridViewX.MultiSelect = False
        Me.IMDataGridViewX.Name = "IMDataGridViewX"
        Me.IMDataGridViewX.ReadOnly = True
        Me.IMDataGridViewX.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IMDataGridViewX.RowHeadersVisible = False
        DataGridViewCellStyle5.Font = New System.Drawing.Font("JF Flat", 10.0!)
        Me.IMDataGridViewX.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.IMDataGridViewX.RowTemplate.Height = 30
        Me.IMDataGridViewX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.IMDataGridViewX.Size = New System.Drawing.Size(393, 5)
        Me.IMDataGridViewX.TabIndex = 610
        Me.IMDataGridViewX.Visible = False
        '
        'IM_ID_CL
        '
        Me.IM_ID_CL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.IM_ID_CL.DataPropertyName = "IM_ID"
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_ID_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.IM_ID_CL.FillWeight = 5.0!
        Me.IM_ID_CL.Frozen = True
        Me.IM_ID_CL.HeaderText = "رقم الصنف"
        Me.IM_ID_CL.Name = "IM_ID_CL"
        Me.IM_ID_CL.ReadOnly = True
        Me.IM_ID_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IM_ID_CL.Visible = False
        Me.IM_ID_CL.Width = 5
        '
        'U_IM_ID_CL
        '
        Me.U_IM_ID_CL.DataPropertyName = "U_IM_ID"
        Me.U_IM_ID_CL.HeaderText = "ر.وحدات الأصناف"
        Me.U_IM_ID_CL.Name = "U_IM_ID_CL"
        Me.U_IM_ID_CL.ReadOnly = True
        Me.U_IM_ID_CL.Visible = False
        '
        'item_name_CL
        '
        Me.item_name_CL.DataPropertyName = "item_name"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.item_name_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.item_name_CL.FillWeight = 69.61895!
        Me.item_name_CL.HeaderText = "الصنف"
        Me.item_name_CL.Name = "item_name_CL"
        Me.item_name_CL.ReadOnly = True
        '
        'Barcode_CL
        '
        Me.Barcode_CL.DataPropertyName = "Barcode"
        Me.Barcode_CL.HeaderText = "باركود"
        Me.Barcode_CL.Name = "Barcode_CL"
        Me.Barcode_CL.ReadOnly = True
        '
        'U_Name_CL
        '
        Me.U_Name_CL.DataPropertyName = "U_Name"
        Me.U_Name_CL.HeaderText = "الوحدة"
        Me.U_Name_CL.Name = "U_Name_CL"
        Me.U_Name_CL.ReadOnly = True
        '
        'IM_SH_txt
        '
        Me.IM_SH_txt.BackColor = System.Drawing.Color.LightGray
        Me.IM_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_SH_txt.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IM_SH_txt.Location = New System.Drawing.Point(389, 3)
        Me.IM_SH_txt.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.IM_SH_txt.Name = "IM_SH_txt"
        Me.IM_SH_txt.Size = New System.Drawing.Size(353, 31)
        Me.IM_SH_txt.TabIndex = 609
        '
        'Barcode_DefPrinter_Cm
        '
        Me.Barcode_DefPrinter_Cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Barcode_DefPrinter_Cm.DisplayMember = "Text"
        Me.Barcode_DefPrinter_Cm.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.Barcode_DefPrinter_Cm.DropDownHeight = 100
        Me.Barcode_DefPrinter_Cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Barcode_DefPrinter_Cm.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Barcode_DefPrinter_Cm.FormattingEnabled = True
        Me.Barcode_DefPrinter_Cm.IntegralHeight = False
        Me.Barcode_DefPrinter_Cm.ItemHeight = 23
        Me.Barcode_DefPrinter_Cm.Location = New System.Drawing.Point(5, 4)
        Me.Barcode_DefPrinter_Cm.Name = "Barcode_DefPrinter_Cm"
        Me.Barcode_DefPrinter_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Barcode_DefPrinter_Cm.Size = New System.Drawing.Size(272, 29)
        Me.Barcode_DefPrinter_Cm.TabIndex = 634
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(281, 7)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(60, 24)
        Me.Label18.TabIndex = 635
        Me.Label18.Text = "الطابعة"
        '
        'Show_IM_btn
        '
        Me.Show_IM_btn.BackColor = System.Drawing.Color.White
        Me.Show_IM_btn.BackgroundImage = Global.resturant.My.Resources.Resources.if_download_173000
        Me.Show_IM_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Show_IM_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Show_IM_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Show_IM_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Show_IM_btn.Location = New System.Drawing.Point(349, 3)
        Me.Show_IM_btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Show_IM_btn.Name = "Show_IM_btn"
        Me.Show_IM_btn.Size = New System.Drawing.Size(39, 31)
        Me.Show_IM_btn.TabIndex = 611
        Me.Show_IM_btn.UseVisualStyleBackColor = False
        '
        'Saveformat
        '
        Me.Saveformat.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Saveformat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Saveformat.Font = New System.Drawing.Font("JF Flat", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Saveformat.Image = Global.resturant.My.Resources.Resources.output_onlinepngtools__1_
        Me.Saveformat.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Saveformat.Location = New System.Drawing.Point(79, 102)
        Me.Saveformat.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Saveformat.Name = "Saveformat"
        Me.Saveformat.Size = New System.Drawing.Size(235, 33)
        Me.Saveformat.TabIndex = 158
        Me.Saveformat.Text = "حفظ تنسيق الباركود"
        Me.Saveformat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Saveformat.UseVisualStyleBackColor = True
        '
        'Showing
        '
        Me.Showing.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Showing.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Showing.Font = New System.Drawing.Font("JF Flat", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Showing.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.Showing.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Showing.Location = New System.Drawing.Point(318, 102)
        Me.Showing.Name = "Showing"
        Me.Showing.Size = New System.Drawing.Size(235, 33)
        Me.Showing.TabIndex = 13
        Me.Showing.Text = "عرض"
        Me.Showing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Showing.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button3.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button3.Location = New System.Drawing.Point(8, 10)
        Me.Button3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(172, 33)
        Me.Button3.TabIndex = 14
        Me.Button3.Text = "معاينة"
        Me.Button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.UseVisualStyleBackColor = False
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(619, 524)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(172, 33)
        Me.ExitFormButton.TabIndex = 16
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = Global.resturant.My.Resources.Resources.output_onlinepngtools__2_
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(8, 45)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(172, 33)
        Me.Button1.TabIndex = 15
        Me.Button1.Text = "طباعة"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Gainsboro
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox1.ErrorImage = Nothing
        Me.PictureBox1.ImageLocation = "444444"
        Me.PictureBox1.Location = New System.Drawing.Point(6, 293)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(774, 229)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'printbarcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(803, 601)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Barcode_DefPrinter_Cm)
        Me.Controls.Add(Me.IMDataGridViewX)
        Me.Controls.Add(Me.Show_IM_btn)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.IM_SH_txt)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MaximizeBox = False
        Me.Name = "printbarcode"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " "
        CType(Me.width, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.hieght, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MarginPLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MarginPUP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MarginBUp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.A4GroupBox.ResumeLayout(False)
        Me.A4GroupBox.PerformLayout()
        CType(Me.txtrow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtcol, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.IMDataGridViewX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents width As NumericUpDown
    Friend WithEvents hieght As NumericUpDown
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents txtbarcode As TextBox
    Friend WithEvents الباركود As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
    Friend WithEvents PageSetupDialog1 As PageSetupDialog
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents ExitFormButton As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents MarginPLeft As NumericUpDown
    Friend WithEvents MarginPUP As NumericUpDown
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents MarginBUp As NumericUpDown
    Friend WithEvents Label14 As Label
    Friend WithEvents Showing As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents A4GroupBox As GroupBox
    Friend WithEvents txtrow As NumericUpDown
    Friend WithEvents Label8 As Label
    Friend WithEvents cbA4 As CheckBox
    Friend WithEvents txtcol As NumericUpDown
    Friend WithEvents Label15 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Saveformat As Button
    Friend WithEvents txtprice As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Show_IM_btn As System.Windows.Forms.Button
    Public WithEvents IMDataGridViewX As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents IM_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents U_IM_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Barcode_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents U_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_SH_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents IM_Unit_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Print_CPName_CB As System.Windows.Forms.CheckBox
    Friend WithEvents nud_Count As System.Windows.Forms.TextBox
    Friend WithEvents Print_Price_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents IM_Num_txt As System.Windows.Forms.TextBox
    Friend WithEvents PRINT_IM_NUM_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Print_IM_NAME_CB As System.Windows.Forms.CheckBox
    Friend WithEvents Barcode_DefPrinter_Cm As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents Label18 As Label
End Class
