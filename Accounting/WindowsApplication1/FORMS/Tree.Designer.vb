<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Tree
    Inherits Base_Form
    'Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Tree))
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.DataB = New System.Windows.Forms.BindingSource(Me.components)
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.ACC_DIGIT = New System.Windows.Forms.DomainUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Acc_Current_Status_LB = New System.Windows.Forms.Label()
        Me.BALANCE = New Accounting.F2FloatField_Balance()
        Me.DEBIT = New Accounting.F2FloatField_Debit()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.is_Lock_Trans_CB = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.CREDIT = New Accounting.F2FloatField_Credit()
        Me.is_Balance_Sheet_CB = New System.Windows.Forms.CheckBox()
        Me.is_Balance_View_CB = New System.Windows.Forms.CheckBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.TreeLegendPanel = New System.Windows.Forms.Panel()
        Me.LegendNormalColor = New System.Windows.Forms.Label()
        Me.LegendNormalLabel = New System.Windows.Forms.Label()
        Me.LegendSalesColor = New System.Windows.Forms.Label()
        Me.LegendSalesLabel = New System.Windows.Forms.Label()
        Me.LegendLockedColor = New System.Windows.Forms.Label()
        Me.LegendLockedLabel = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Search_By_Acc_Name_txt = New System.Windows.Forms.TextBox()
        Me.Search_By_Acc_Code_txt = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New Zuby.ADGV.AdvancedDataGridView()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ACC_CODE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ACC_NAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ACC_PARENT_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ACC_LEVEL_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.is_Balance_View_CL = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ACC_CLOSING_CL = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.SIDE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.is_Lock_Trans_CL = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.CircularPanel = New System.Windows.Forms.Panel()
        Me.CircularProgressControl1 = New Accounting.CircularProgressControl()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.parent_Label = New System.Windows.Forms.TextBox()
        Me.ACC_PARENT = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Side_CM = New System.Windows.Forms.ComboBox()
        Me.Print_Table_Btn = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Cash_flows_CM = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Pure_Income_ACC_CODE_TXT = New System.Windows.Forms.TextBox()
        Me.Print_Btn = New System.Windows.Forms.Button()
        Me.View_Btn = New System.Windows.Forms.Button()
        Me.Show_ALL_BTN = New System.Windows.Forms.Button()
        Me.Refresh_Btn = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.REMOVE_BTN = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ACC_LEVEL = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.T_ID_txt = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ACC_CODE = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ACC_NATURAL = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ACC_NAME = New System.Windows.Forms.TextBox()
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TreeLegendPanel.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CircularPanel.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
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
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.ACC_DIGIT)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Panel2)
        Me.Panel3.Controls.Add(Me.DEBIT)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.is_Lock_Trans_CB)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.CREDIT)
        Me.Panel3.Controls.Add(Me.is_Balance_Sheet_CB)
        Me.Panel3.Controls.Add(Me.is_Balance_View_CB)
        Me.Panel3.Location = New System.Drawing.Point(592, 259)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(406, 205)
        Me.Panel3.TabIndex = 98
        '
        'ACC_DIGIT
        '
        Me.ACC_DIGIT.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ACC_DIGIT.Items.Add("9")
        Me.ACC_DIGIT.Items.Add("8")
        Me.ACC_DIGIT.Items.Add("7")
        Me.ACC_DIGIT.Items.Add("6")
        Me.ACC_DIGIT.Items.Add("5")
        Me.ACC_DIGIT.Items.Add("4")
        Me.ACC_DIGIT.Items.Add("3")
        Me.ACC_DIGIT.Items.Add("2")
        Me.ACC_DIGIT.Items.Add("1")
        Me.ACC_DIGIT.Items.Add("0")
        Me.ACC_DIGIT.Location = New System.Drawing.Point(91, 169)
        Me.ACC_DIGIT.Name = "ACC_DIGIT"
        Me.ACC_DIGIT.ReadOnly = True
        Me.ACC_DIGIT.Size = New System.Drawing.Size(52, 23)
        Me.ACC_DIGIT.TabIndex = 100
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(146, 174)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(135, 14)
        Me.Label3.TabIndex = 99
        Me.Label3.Text = "خانات الحسابات الداخلية:"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Acc_Current_Status_LB)
        Me.Panel2.Controls.Add(Me.BALANCE)
        Me.Panel2.Location = New System.Drawing.Point(4, 122)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(293, 39)
        Me.Panel2.TabIndex = 97
        '
        'Acc_Current_Status_LB
        '
        Me.Acc_Current_Status_LB.BackColor = System.Drawing.Color.Gainsboro
        Me.Acc_Current_Status_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Acc_Current_Status_LB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Acc_Current_Status_LB.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Acc_Current_Status_LB.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Acc_Current_Status_LB.Location = New System.Drawing.Point(3, 3)
        Me.Acc_Current_Status_LB.Name = "Acc_Current_Status_LB"
        Me.Acc_Current_Status_LB.Size = New System.Drawing.Size(118, 29)
        Me.Acc_Current_Status_LB.TabIndex = 94
        Me.Acc_Current_Status_LB.Text = "Acc_Status"
        Me.Acc_Current_Status_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BALANCE
        '
        Me.BALANCE.BackColor = System.Drawing.Color.Lavender
        Me.BALANCE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BALANCE.Enabled = False
        Me.BALANCE.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BALANCE.Location = New System.Drawing.Point(123, 3)
        Me.BALANCE.MaxLength = 0
        Me.BALANCE.Name = "BALANCE"
        Me.BALANCE.Size = New System.Drawing.Size(168, 29)
        Me.BALANCE.TabIndex = 63
        Me.BALANCE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DEBIT
        '
        Me.DEBIT.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DEBIT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DEBIT.Enabled = False
        Me.DEBIT.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.DEBIT.ForeColor = System.Drawing.Color.DarkRed
        Me.DEBIT.Location = New System.Drawing.Point(126, 56)
        Me.DEBIT.MaxLength = 0
        Me.DEBIT.Name = "DEBIT"
        Me.DEBIT.Size = New System.Drawing.Size(168, 29)
        Me.DEBIT.TabIndex = 61
        Me.DEBIT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Label9.Location = New System.Drawing.Point(298, 63)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(30, 16)
        Me.Label9.TabIndex = 20
        Me.Label9.Text = "دائن:"
        '
        'is_Lock_Trans_CB
        '
        Me.is_Lock_Trans_CB.AutoSize = True
        Me.is_Lock_Trans_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.is_Lock_Trans_CB.Font = New System.Drawing.Font("Arial", 9.5!, System.Drawing.FontStyle.Bold)
        Me.is_Lock_Trans_CB.Location = New System.Drawing.Point(272, 33)
        Me.is_Lock_Trans_CB.Name = "is_Lock_Trans_CB"
        Me.is_Lock_Trans_CB.Size = New System.Drawing.Size(98, 20)
        Me.is_Lock_Trans_CB.TabIndex = 93
        Me.is_Lock_Trans_CB.Text = "إقفال القيود عليه"
        Me.is_Lock_Trans_CB.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Label10.Location = New System.Drawing.Point(298, 94)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(33, 16)
        Me.Label10.TabIndex = 22
        Me.Label10.Text = "مدين:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.Label11.Location = New System.Drawing.Point(301, 130)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(45, 16)
        Me.Label11.TabIndex = 24
        Me.Label11.Text = "الرصيد:"
        '
        'CREDIT
        '
        Me.CREDIT.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.CREDIT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CREDIT.Enabled = False
        Me.CREDIT.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CREDIT.ForeColor = System.Drawing.Color.DarkGreen
        Me.CREDIT.Location = New System.Drawing.Point(126, 88)
        Me.CREDIT.MaxLength = 0
        Me.CREDIT.Name = "CREDIT"
        Me.CREDIT.Size = New System.Drawing.Size(168, 29)
        Me.CREDIT.TabIndex = 62
        Me.CREDIT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'is_Balance_Sheet_CB
        '
        Me.is_Balance_Sheet_CB.AutoSize = True
        Me.is_Balance_Sheet_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.is_Balance_Sheet_CB.Font = New System.Drawing.Font("Arial", 9.5!, System.Drawing.FontStyle.Bold)
        Me.is_Balance_Sheet_CB.Location = New System.Drawing.Point(231, 5)
        Me.is_Balance_Sheet_CB.Name = "is_Balance_Sheet_CB"
        Me.is_Balance_Sheet_CB.Size = New System.Drawing.Size(139, 20)
        Me.is_Balance_Sheet_CB.TabIndex = 74
        Me.is_Balance_Sheet_CB.Text = "يظهر في المركــز المالــي"
        Me.is_Balance_Sheet_CB.UseVisualStyleBackColor = True
        '
        'is_Balance_View_CB
        '
        Me.is_Balance_View_CB.AutoSize = True
        Me.is_Balance_View_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.is_Balance_View_CB.Font = New System.Drawing.Font("Arial", 9.5!, System.Drawing.FontStyle.Bold)
        Me.is_Balance_View_CB.Location = New System.Drawing.Point(5, 5)
        Me.is_Balance_View_CB.Name = "is_Balance_View_CB"
        Me.is_Balance_View_CB.Size = New System.Drawing.Size(151, 20)
        Me.is_Balance_View_CB.TabIndex = 76
        Me.is_Balance_View_CB.Text = "يظهر في ميــزان المراجعـــة"
        Me.is_Balance_View_CB.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 9.25!)
        Me.TabControl1.Location = New System.Drawing.Point(1, 1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(588, 646)
        Me.TabControl1.TabIndex = 90
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TreeView1)
        Me.TabPage1.Controls.Add(Me.TreeLegendPanel)
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(580, 619)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "نموذج شجرة"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TreeView1
        '
        Me.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.TreeView1.Indent = 100
        Me.TreeView1.ItemHeight = 27
        Me.TreeView1.Location = New System.Drawing.Point(3, 3)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(574, 579)
        Me.TreeView1.TabIndex = 0
        '
        'TreeLegendPanel
        '
        Me.TreeLegendPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.TreeLegendPanel.Controls.Add(Me.LegendNormalColor)
        Me.TreeLegendPanel.Controls.Add(Me.LegendNormalLabel)
        Me.TreeLegendPanel.Controls.Add(Me.LegendSalesColor)
        Me.TreeLegendPanel.Controls.Add(Me.LegendSalesLabel)
        Me.TreeLegendPanel.Controls.Add(Me.LegendLockedColor)
        Me.TreeLegendPanel.Controls.Add(Me.LegendLockedLabel)
        Me.TreeLegendPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TreeLegendPanel.Location = New System.Drawing.Point(3, 582)
        Me.TreeLegendPanel.Name = "TreeLegendPanel"
        Me.TreeLegendPanel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TreeLegendPanel.Size = New System.Drawing.Size(574, 34)
        Me.TreeLegendPanel.TabIndex = 1
        '
        'LegendNormalColor
        '
        Me.LegendNormalColor.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.LegendNormalColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LegendNormalColor.Location = New System.Drawing.Point(501, 10)
        Me.LegendNormalColor.Name = "LegendNormalColor"
        Me.LegendNormalColor.Size = New System.Drawing.Size(18, 14)
        Me.LegendNormalColor.TabIndex = 5
        '
        'LegendNormalLabel
        '
        Me.LegendNormalLabel.AutoSize = True
        Me.LegendNormalLabel.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.LegendNormalLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.LegendNormalLabel.Location = New System.Drawing.Point(421, 8)
        Me.LegendNormalLabel.Name = "LegendNormalLabel"
        Me.LegendNormalLabel.Size = New System.Drawing.Size(74, 15)
        Me.LegendNormalLabel.TabIndex = 4
        Me.LegendNormalLabel.Text = "حساب عادي"
        '
        'LegendSalesColor
        '
        Me.LegendSalesColor.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.LegendSalesColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LegendSalesColor.Location = New System.Drawing.Point(320, 10)
        Me.LegendSalesColor.Name = "LegendSalesColor"
        Me.LegendSalesColor.Size = New System.Drawing.Size(18, 14)
        Me.LegendSalesColor.TabIndex = 3
        '
        'LegendSalesLabel
        '
        Me.LegendSalesLabel.AutoSize = True
        Me.LegendSalesLabel.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.LegendSalesLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(110, Byte), Integer))
        Me.LegendSalesLabel.Location = New System.Drawing.Point(205, 8)
        Me.LegendSalesLabel.Name = "LegendSalesLabel"
        Me.LegendSalesLabel.Size = New System.Drawing.Size(109, 15)
        Me.LegendSalesLabel.TabIndex = 2
        Me.LegendSalesLabel.Text = "حساب من المبيعات"
        '
        'LegendLockedColor
        '
        Me.LegendLockedColor.BackColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(119, Byte), Integer), CType(CType(6, Byte), Integer))
        Me.LegendLockedColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LegendLockedColor.Location = New System.Drawing.Point(130, 10)
        Me.LegendLockedColor.Name = "LegendLockedColor"
        Me.LegendLockedColor.Size = New System.Drawing.Size(18, 14)
        Me.LegendLockedColor.TabIndex = 1
        '
        'LegendLockedLabel
        '
        Me.LegendLockedLabel.AutoSize = True
        Me.LegendLockedLabel.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.LegendLockedLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(146, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(14, Byte), Integer))
        Me.LegendLockedLabel.Location = New System.Drawing.Point(34, 8)
        Me.LegendLockedLabel.Name = "LegendLockedLabel"
        Me.LegendLockedLabel.Size = New System.Drawing.Size(90, 15)
        Me.LegendLockedLabel.TabIndex = 0
        Me.LegendLockedLabel.Text = "مقفل عن القيود"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(580, 619)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "جدول"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DataGridView1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CircularPanel, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.293706!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.70629!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(574, 613)
        Me.TableLayoutPanel1.TabIndex = 902
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Search_By_Acc_Name_txt)
        Me.Panel1.Controls.Add(Me.Search_By_Acc_Code_txt)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(568, 29)
        Me.Panel1.TabIndex = 0
        '
        'Search_By_Acc_Name_txt
        '
        Me.Search_By_Acc_Name_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Search_By_Acc_Name_txt.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Search_By_Acc_Name_txt.Location = New System.Drawing.Point(346, 3)
        Me.Search_By_Acc_Name_txt.Name = "Search_By_Acc_Name_txt"
        Me.Search_By_Acc_Name_txt.Size = New System.Drawing.Size(220, 24)
        Me.Search_By_Acc_Name_txt.TabIndex = 91
        '
        'Search_By_Acc_Code_txt
        '
        Me.Search_By_Acc_Code_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Search_By_Acc_Code_txt.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Search_By_Acc_Code_txt.Location = New System.Drawing.Point(2, 3)
        Me.Search_By_Acc_Code_txt.Name = "Search_By_Acc_Code_txt"
        Me.Search_By_Acc_Code_txt.Size = New System.Drawing.Size(343, 24)
        Me.Search_By_Acc_Code_txt.TabIndex = 92
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 9.25!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.ACC_CODE_CL, Me.ACC_NAME_CL, Me.ACC_PARENT_CL, Me.ACC_LEVEL_CL, Me.is_Balance_View_CL, Me.ACC_CLOSING_CL, Me.SIDE_CL, Me.is_Lock_Trans_CL})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 9.25!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.BlanchedAlmond
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.FilterAndSortEnabled = True
        Me.DataGridView1.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DataGridView1.Location = New System.Drawing.Point(4, 39)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 35
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(566, 527)
        Me.DataGridView1.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DataGridView1.TabIndex = 76
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "T_ID"
        Me.T_ID_CL.MinimumWidth = 22
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.T_ID_CL.Visible = False
        '
        'ACC_CODE_CL
        '
        Me.ACC_CODE_CL.DataPropertyName = "ACC_CODE"
        Me.ACC_CODE_CL.HeaderText = "رقم الحساب"
        Me.ACC_CODE_CL.MinimumWidth = 22
        Me.ACC_CODE_CL.Name = "ACC_CODE_CL"
        Me.ACC_CODE_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'ACC_NAME_CL
        '
        Me.ACC_NAME_CL.DataPropertyName = "ACC_NAME"
        Me.ACC_NAME_CL.HeaderText = "إسم الحساب"
        Me.ACC_NAME_CL.MinimumWidth = 22
        Me.ACC_NAME_CL.Name = "ACC_NAME_CL"
        Me.ACC_NAME_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'ACC_PARENT_CL
        '
        Me.ACC_PARENT_CL.DataPropertyName = "ACC_PARENT"
        Me.ACC_PARENT_CL.HeaderText = "الحساب الأب"
        Me.ACC_PARENT_CL.MinimumWidth = 22
        Me.ACC_PARENT_CL.Name = "ACC_PARENT_CL"
        Me.ACC_PARENT_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.ACC_PARENT_CL.Visible = False
        '
        'ACC_LEVEL_CL
        '
        Me.ACC_LEVEL_CL.DataPropertyName = "ACC_LEVEL"
        Me.ACC_LEVEL_CL.HeaderText = "مستوى الحساب"
        Me.ACC_LEVEL_CL.MinimumWidth = 22
        Me.ACC_LEVEL_CL.Name = "ACC_LEVEL_CL"
        Me.ACC_LEVEL_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.ACC_LEVEL_CL.Visible = False
        '
        'is_Balance_View_CL
        '
        Me.is_Balance_View_CL.DataPropertyName = "is_Balance_View"
        Me.is_Balance_View_CL.HeaderText = "يعرض في ميزان المراجعة"
        Me.is_Balance_View_CL.MinimumWidth = 22
        Me.is_Balance_View_CL.Name = "is_Balance_View_CL"
        Me.is_Balance_View_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.is_Balance_View_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'ACC_CLOSING_CL
        '
        Me.ACC_CLOSING_CL.DataPropertyName = "ACC_CLOSING"
        Me.ACC_CLOSING_CL.HeaderText = "يعرض في  المركز المالي"
        Me.ACC_CLOSING_CL.MinimumWidth = 22
        Me.ACC_CLOSING_CL.Name = "ACC_CLOSING_CL"
        Me.ACC_CLOSING_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ACC_CLOSING_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'SIDE_CL
        '
        Me.SIDE_CL.DataPropertyName = "SIDE"
        Me.SIDE_CL.HeaderText = "أصول/إلتزامات"
        Me.SIDE_CL.MinimumWidth = 22
        Me.SIDE_CL.Name = "SIDE_CL"
        Me.SIDE_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SIDE_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'is_Lock_Trans_CL
        '
        Me.is_Lock_Trans_CL.DataPropertyName = "is_Lock_Trans"
        Me.is_Lock_Trans_CL.HeaderText = "إقفال القيود عليه"
        Me.is_Lock_Trans_CL.MinimumWidth = 22
        Me.is_Lock_Trans_CL.Name = "is_Lock_Trans_CL"
        Me.is_Lock_Trans_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.is_Lock_Trans_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'CircularPanel
        '
        Me.CircularPanel.BackColor = System.Drawing.Color.Transparent
        Me.CircularPanel.Controls.Add(Me.CircularProgressControl1)
        Me.CircularPanel.Location = New System.Drawing.Point(3, 573)
        Me.CircularPanel.Name = "CircularPanel"
        Me.CircularPanel.Size = New System.Drawing.Size(568, 36)
        Me.CircularPanel.TabIndex = 901
        Me.CircularPanel.Visible = False
        '
        'CircularProgressControl1
        '
        Me.CircularProgressControl1.BackColor = System.Drawing.Color.Transparent
        Me.CircularProgressControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CircularProgressControl1.Interval = 60
        Me.CircularProgressControl1.Location = New System.Drawing.Point(0, 0)
        Me.CircularProgressControl1.MinimumSize = New System.Drawing.Size(28, 28)
        Me.CircularProgressControl1.Name = "CircularProgressControl1"
        Me.CircularProgressControl1.Rotation = Accounting.CircularProgressControl.Direction.CLOCKWISE
        Me.CircularProgressControl1.Size = New System.Drawing.Size(568, 36)
        Me.CircularProgressControl1.StartAngle = 270
        Me.CircularProgressControl1.TabIndex = 93
        Me.CircularProgressControl1.TickColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.parent_Label)
        Me.GroupBox1.Controls.Add(Me.ACC_PARENT)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.GroupBox1.Location = New System.Drawing.Point(708, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(290, 74)
        Me.GroupBox1.TabIndex = 96
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "الحساب الرئيسي:"
        '
        'parent_Label
        '
        Me.parent_Label.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.parent_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.parent_Label.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.parent_Label.Location = New System.Drawing.Point(5, 45)
        Me.parent_Label.Name = "parent_Label"
        Me.parent_Label.ReadOnly = True
        Me.parent_Label.Size = New System.Drawing.Size(281, 25)
        Me.parent_Label.TabIndex = 95
        '
        'ACC_PARENT
        '
        Me.ACC_PARENT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ACC_PARENT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ACC_PARENT.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ACC_PARENT.Location = New System.Drawing.Point(5, 18)
        Me.ACC_PARENT.Name = "ACC_PARENT"
        Me.ACC_PARENT.ReadOnly = True
        Me.ACC_PARENT.Size = New System.Drawing.Size(281, 25)
        Me.ACC_PARENT.TabIndex = 12
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label12.Location = New System.Drawing.Point(888, 230)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(98, 17)
        Me.Label12.TabIndex = 92
        Me.Label12.Text = "أصول \ إلتزامات:"
        '
        'Side_CM
        '
        Me.Side_CM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Side_CM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Side_CM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Side_CM.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Side_CM.FormattingEnabled = True
        Me.Side_CM.Items.AddRange(New Object() {"بلا", "assets", "opponents", "REVENUE", "EXPENSE"})
        Me.Side_CM.Location = New System.Drawing.Point(592, 227)
        Me.Side_CM.Name = "Side_CM"
        Me.Side_CM.Size = New System.Drawing.Size(292, 26)
        Me.Side_CM.TabIndex = 91
        '
        'Print_Table_Btn
        '
        Me.Print_Table_Btn.BackColor = System.Drawing.Color.White
        Me.Print_Table_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Print_Table_Btn.Font = New System.Drawing.Font("Arial", 9.25!, System.Drawing.FontStyle.Bold)
        Me.Print_Table_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_Table_Btn.Location = New System.Drawing.Point(2, 653)
        Me.Print_Table_Btn.Name = "Print_Table_Btn"
        Me.Print_Table_Btn.Size = New System.Drawing.Size(165, 42)
        Me.Print_Table_Btn.TabIndex = 89
        Me.Print_Table_Btn.Text = "طباعـة الدليل كجدول 🖨️"
        Me.Print_Table_Btn.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label15.Location = New System.Drawing.Point(888, 199)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(104, 17)
        Me.Label15.TabIndex = 88
        Me.Label15.Text = "التدفقات النقدية:"
        '
        'Cash_flows_CM
        '
        Me.Cash_flows_CM.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Cash_flows_CM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cash_flows_CM.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Cash_flows_CM.Font = New System.Drawing.Font("Tahoma", 10.25!, System.Drawing.FontStyle.Bold)
        Me.Cash_flows_CM.FormattingEnabled = True
        Me.Cash_flows_CM.Items.AddRange(New Object() {"دائن", "مدين"})
        Me.Cash_flows_CM.Location = New System.Drawing.Point(592, 196)
        Me.Cash_flows_CM.Name = "Cash_flows_CM"
        Me.Cash_flows_CM.Size = New System.Drawing.Size(292, 25)
        Me.Cash_flows_CM.TabIndex = 87
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(719, 619)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(183, 16)
        Me.Label13.TabIndex = 84
        Me.Label13.Text = "حساب صافي الأرباح و الخسائر :"
        '
        'Pure_Income_ACC_CODE_TXT
        '
        Me.Pure_Income_ACC_CODE_TXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pure_Income_ACC_CODE_TXT.Font = New System.Drawing.Font("Arial", 12.25!, System.Drawing.FontStyle.Bold)
        Me.Pure_Income_ACC_CODE_TXT.Location = New System.Drawing.Point(591, 614)
        Me.Pure_Income_ACC_CODE_TXT.Name = "Pure_Income_ACC_CODE_TXT"
        Me.Pure_Income_ACC_CODE_TXT.ReadOnly = True
        Me.Pure_Income_ACC_CODE_TXT.Size = New System.Drawing.Size(125, 26)
        Me.Pure_Income_ACC_CODE_TXT.TabIndex = 83
        '
        'Print_Btn
        '
        Me.Print_Btn.BackColor = System.Drawing.Color.White
        Me.Print_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Print_Btn.Font = New System.Drawing.Font("Arial", 9.25!, System.Drawing.FontStyle.Bold)
        Me.Print_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_Btn.Location = New System.Drawing.Point(170, 653)
        Me.Print_Btn.Name = "Print_Btn"
        Me.Print_Btn.Size = New System.Drawing.Size(165, 42)
        Me.Print_Btn.TabIndex = 72
        Me.Print_Btn.Text = "طباعـــة شكل الدليل 🖨️"
        Me.Print_Btn.UseVisualStyleBackColor = False
        '
        'View_Btn
        '
        Me.View_Btn.BackColor = System.Drawing.Color.White
        Me.View_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.View_Btn.Font = New System.Drawing.Font("Arial", 9.25!, System.Drawing.FontStyle.Bold)
        Me.View_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.View_Btn.Location = New System.Drawing.Point(338, 653)
        Me.View_Btn.Name = "View_Btn"
        Me.View_Btn.Size = New System.Drawing.Size(145, 42)
        Me.View_Btn.TabIndex = 71
        Me.View_Btn.Text = "معاينــة شكل الدليل 🔍"
        Me.View_Btn.UseVisualStyleBackColor = False
        '
        'Show_ALL_BTN
        '
        Me.Show_ALL_BTN.BackColor = System.Drawing.Color.White
        Me.Show_ALL_BTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Show_ALL_BTN.Font = New System.Drawing.Font("Arial", 9.25!, System.Drawing.FontStyle.Bold)
        Me.Show_ALL_BTN.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Show_ALL_BTN.Location = New System.Drawing.Point(486, 653)
        Me.Show_ALL_BTN.Name = "Show_ALL_BTN"
        Me.Show_ALL_BTN.Size = New System.Drawing.Size(145, 42)
        Me.Show_ALL_BTN.TabIndex = 70
        Me.Show_ALL_BTN.Text = "عــرض الكــل 📊"
        Me.Show_ALL_BTN.UseVisualStyleBackColor = False
        '
        'Refresh_Btn
        '
        Me.Refresh_Btn.BackColor = System.Drawing.Color.White
        Me.Refresh_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Refresh_Btn.Font = New System.Drawing.Font("Arial", 9.25!, System.Drawing.FontStyle.Bold)
        Me.Refresh_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Refresh_Btn.Location = New System.Drawing.Point(634, 653)
        Me.Refresh_Btn.Name = "Refresh_Btn"
        Me.Refresh_Btn.Size = New System.Drawing.Size(145, 42)
        Me.Refresh_Btn.TabIndex = 69
        Me.Refresh_Btn.Text = "🔄 تحديـث القائمـة"
        Me.Refresh_Btn.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Font = New System.Drawing.Font("Arial", 10.75!, System.Drawing.FontStyle.Bold)
        Me.Button6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button6.Location = New System.Drawing.Point(592, 470)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(329, 42)
        Me.Button6.TabIndex = 29
        Me.Button6.Text = "➕ فتح حســاب جــديد"
        Me.Button6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Font = New System.Drawing.Font("Arial", 10.75!, System.Drawing.FontStyle.Bold)
        Me.Button5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button5.Location = New System.Drawing.Point(592, 514)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(329, 42)
        Me.Button5.TabIndex = 28
        Me.Button5.Text = "📝 تعديــل بيانات الحساب"
        Me.Button5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button5.UseVisualStyleBackColor = True
        '
        'REMOVE_BTN
        '
        Me.REMOVE_BTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.REMOVE_BTN.Font = New System.Drawing.Font("Arial", 10.75!, System.Drawing.FontStyle.Bold)
        Me.REMOVE_BTN.ForeColor = System.Drawing.Color.DarkRed
        Me.REMOVE_BTN.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.REMOVE_BTN.Location = New System.Drawing.Point(592, 557)
        Me.REMOVE_BTN.Name = "REMOVE_BTN"
        Me.REMOVE_BTN.Size = New System.Drawing.Size(329, 42)
        Me.REMOVE_BTN.TabIndex = 25
        Me.REMOVE_BTN.Text = "❌  حـــذف الحســاب"
        Me.REMOVE_BTN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.REMOVE_BTN.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label8.Location = New System.Drawing.Point(923, 5)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(67, 17)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "المستوى:"
        '
        'ACC_LEVEL
        '
        Me.ACC_LEVEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ACC_LEVEL.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!)
        Me.ACC_LEVEL.Location = New System.Drawing.Point(854, 1)
        Me.ACC_LEVEL.Name = "ACC_LEVEL"
        Me.ACC_LEVEL.ReadOnly = True
        Me.ACC_LEVEL.Size = New System.Drawing.Size(66, 24)
        Me.ACC_LEVEL.TabIndex = 17
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label7.Location = New System.Drawing.Point(796, 5)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(43, 17)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "T_ID:"
        Me.Label7.Visible = False
        '
        'T_ID_txt
        '
        Me.T_ID_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.T_ID_txt.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!)
        Me.T_ID_txt.Location = New System.Drawing.Point(708, 1)
        Me.T_ID_txt.Name = "T_ID_txt"
        Me.T_ID_txt.ReadOnly = True
        Me.T_ID_txt.Size = New System.Drawing.Size(85, 24)
        Me.T_ID_txt.TabIndex = 14
        Me.T_ID_txt.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label5.Location = New System.Drawing.Point(887, 113)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 17)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "كود الحساب:"
        '
        'ACC_CODE
        '
        Me.ACC_CODE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ACC_CODE.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ACC_CODE.Location = New System.Drawing.Point(592, 105)
        Me.ACC_CODE.Name = "ACC_CODE"
        Me.ACC_CODE.ReadOnly = True
        Me.ACC_CODE.Size = New System.Drawing.Size(292, 29)
        Me.ACC_CODE.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label2.Location = New System.Drawing.Point(888, 170)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 17)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "طبيعة الحساب:"
        '
        'ACC_NATURAL
        '
        Me.ACC_NATURAL.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ACC_NATURAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ACC_NATURAL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ACC_NATURAL.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ACC_NATURAL.FormattingEnabled = True
        Me.ACC_NATURAL.Items.AddRange(New Object() {"دائن", "مدين"})
        Me.ACC_NATURAL.Location = New System.Drawing.Point(592, 166)
        Me.ACC_NATURAL.Name = "ACC_NATURAL"
        Me.ACC_NATURAL.Size = New System.Drawing.Size(292, 26)
        Me.ACC_NATURAL.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 10.25!)
        Me.Label1.Location = New System.Drawing.Point(887, 141)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "اسم الحساب:"
        '
        'ACC_NAME
        '
        Me.ACC_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ACC_NAME.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ACC_NAME.Location = New System.Drawing.Point(592, 136)
        Me.ACC_NAME.Name = "ACC_NAME"
        Me.ACC_NAME.Size = New System.Drawing.Size(292, 26)
        Me.ACC_NAME.TabIndex = 2
        '
        'Tree
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 695)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Side_CM)
        Me.Controls.Add(Me.Print_Table_Btn)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Cash_flows_CM)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Pure_Income_ACC_CODE_TXT)
        Me.Controls.Add(Me.Print_Btn)
        Me.Controls.Add(Me.View_Btn)
        Me.Controls.Add(Me.Show_ALL_BTN)
        Me.Controls.Add(Me.Refresh_Btn)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.REMOVE_BTN)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.ACC_LEVEL)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.T_ID_txt)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ACC_CODE)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ACC_NATURAL)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ACC_NAME)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "Tree"
        Me.Text = "الدليـــل الحســـابي"
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TreeLegendPanel.ResumeLayout(False)
        Me.TreeLegendPanel.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CircularPanel.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents TreeLegendPanel As Panel
    Friend WithEvents LegendNormalColor As Label
    Friend WithEvents LegendNormalLabel As Label
    Friend WithEvents LegendSalesColor As Label
    Friend WithEvents LegendSalesLabel As Label
    Friend WithEvents LegendLockedColor As Label
    Friend WithEvents LegendLockedLabel As Label
    Friend WithEvents ACC_NAME As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ACC_NATURAL As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ACC_CODE As System.Windows.Forms.TextBox
    Friend WithEvents ACC_PARENT As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents T_ID_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ACC_LEVEL As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents REMOVE_BTN As Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents BALANCE As Accounting.F2FloatField_Balance
    Friend WithEvents CREDIT As Accounting.F2FloatField_Credit
    Friend WithEvents DEBIT As Accounting.F2FloatField_Debit
    Friend WithEvents View_Btn As Button
    Friend WithEvents Show_ALL_BTN As Button
    Friend WithEvents Refresh_Btn As Button
    Friend WithEvents Print_Btn As Button
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
    Friend WithEvents is_Balance_Sheet_CB As CheckBox
    Friend WithEvents is_Balance_View_CB As CheckBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Pure_Income_ACC_CODE_TXT As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Cash_flows_CM As ComboBox
    Friend WithEvents Print_Table_Btn As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents DataGridView1 As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents Search_By_Acc_Name_txt As TextBox
    Friend WithEvents Search_By_Acc_Code_txt As TextBox
    Friend WithEvents DataB As BindingSource
    Friend WithEvents CircularProgressControl1 As CircularProgressControl
    Friend WithEvents CircularPanel As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label12 As Label
    Friend WithEvents Side_CM As ComboBox
    Friend WithEvents is_Lock_Trans_CB As CheckBox
    Friend WithEvents Acc_Current_Status_LB As Label
    Friend WithEvents parent_Label As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents ACC_DIGIT As DomainUpDown
    Friend WithEvents T_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents ACC_CODE_CL As DataGridViewTextBoxColumn
    Friend WithEvents ACC_NAME_CL As DataGridViewTextBoxColumn
    Friend WithEvents ACC_PARENT_CL As DataGridViewTextBoxColumn
    Friend WithEvents ACC_LEVEL_CL As DataGridViewTextBoxColumn
    Friend WithEvents is_Balance_View_CL As DataGridViewCheckBoxColumn
    Friend WithEvents ACC_CLOSING_CL As DataGridViewCheckBoxColumn
    Friend WithEvents SIDE_CL As DataGridViewTextBoxColumn
    Friend WithEvents is_Lock_Trans_CL As DataGridViewCheckBoxColumn
End Class
