<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Report
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Report))
        Me.Frm_ST_cm = New System.Windows.Forms.ComboBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Frm_Cn_DGV_2 = New Zuby.ADGV.AdvancedDataGridView()
        Me.Frm_Cn_DGV = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.DataGridViewTextBoxColumn43 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn44 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn45 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn46 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Frm_Dt_DGV_2 = New Zuby.ADGV.AdvancedDataGridView()
        Me.Frm_Dt_DGV = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.DataGridViewTextBoxColumn27 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn39 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn40 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn41 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Print_Frm_Cn_Btn = New System.Windows.Forms.Button()
        Me.Frm_Search_Btn = New System.Windows.Forms.Button()
        Me.Print_Frm_Dt_Btn = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.DataB_1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataB_2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.AG_Cm = New resturant.FSearch_Filter()
        Me.GroupBox4.SuspendLayout()
        CType(Me.Frm_Cn_DGV_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Frm_Cn_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.Frm_Dt_DGV_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Frm_Dt_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataB_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataB_2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Frm_ST_cm
        '
        Me.Frm_ST_cm.BackColor = System.Drawing.SystemColors.Info
        Me.Frm_ST_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Frm_ST_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Frm_ST_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Frm_ST_cm.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frm_ST_cm.FormattingEnabled = True
        Me.Frm_ST_cm.Location = New System.Drawing.Point(323, 4)
        Me.Frm_ST_cm.Name = "Frm_ST_cm"
        Me.Frm_ST_cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Frm_ST_cm.Size = New System.Drawing.Size(216, 26)
        Me.Frm_ST_cm.TabIndex = 698
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Frm_Cn_DGV_2)
        Me.GroupBox4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.GroupBox4.Location = New System.Drawing.Point(3, 299)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox4.Size = New System.Drawing.Size(891, 357)
        Me.GroupBox4.TabIndex = 696
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "المواد الإستهلاكية"
        '
        'Frm_Cn_DGV_2
        '
        Me.Frm_Cn_DGV_2.AllowUserToAddRows = False
        Me.Frm_Cn_DGV_2.AllowUserToDeleteRows = False
        Me.Frm_Cn_DGV_2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Frm_Cn_DGV_2.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Frm_Cn_DGV_2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.Frm_Cn_DGV_2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Frm_Cn_DGV_2.FilterAndSortEnabled = True
        Me.Frm_Cn_DGV_2.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.Frm_Cn_DGV_2.Location = New System.Drawing.Point(5, 25)
        Me.Frm_Cn_DGV_2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Frm_Cn_DGV_2.MultiSelect = False
        Me.Frm_Cn_DGV_2.Name = "Frm_Cn_DGV_2"
        Me.Frm_Cn_DGV_2.ReadOnly = True
        Me.Frm_Cn_DGV_2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Frm_Cn_DGV_2.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frm_Cn_DGV_2.RowTemplate.Height = 25
        Me.Frm_Cn_DGV_2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Frm_Cn_DGV_2.Size = New System.Drawing.Size(881, 326)
        Me.Frm_Cn_DGV_2.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.Frm_Cn_DGV_2.TabIndex = 904
        '
        'Frm_Cn_DGV
        '
        Me.Frm_Cn_DGV.AllowUserToAddRows = False
        Me.Frm_Cn_DGV.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro
        Me.Frm_Cn_DGV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.Frm_Cn_DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Frm_Cn_DGV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.Frm_Cn_DGV.BackgroundColor = System.Drawing.Color.Silver
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Frm_Cn_DGV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.Frm_Cn_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Frm_Cn_DGV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn43, Me.DataGridViewTextBoxColumn44, Me.DataGridViewTextBoxColumn45, Me.DataGridViewTextBoxColumn46})
        Me.Frm_Cn_DGV.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Frm_Cn_DGV.DefaultCellStyle = DataGridViewCellStyle5
        Me.Frm_Cn_DGV.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.Frm_Cn_DGV.Location = New System.Drawing.Point(563, 28)
        Me.Frm_Cn_DGV.Name = "Frm_Cn_DGV"
        Me.Frm_Cn_DGV.ReadOnly = True
        Me.Frm_Cn_DGV.RowHeadersVisible = False
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.Silver
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Frm_Cn_DGV.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.Frm_Cn_DGV.RowTemplate.Height = 32
        Me.Frm_Cn_DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Frm_Cn_DGV.Size = New System.Drawing.Size(10, 16)
        Me.Frm_Cn_DGV.TabIndex = 683
        Me.Frm_Cn_DGV.Visible = False
        '
        'DataGridViewTextBoxColumn43
        '
        Me.DataGridViewTextBoxColumn43.DataPropertyName = "St_Name"
        Me.DataGridViewTextBoxColumn43.FillWeight = 150.0!
        Me.DataGridViewTextBoxColumn43.HeaderText = "المخزن"
        Me.DataGridViewTextBoxColumn43.Name = "DataGridViewTextBoxColumn43"
        Me.DataGridViewTextBoxColumn43.ReadOnly = True
        '
        'DataGridViewTextBoxColumn44
        '
        Me.DataGridViewTextBoxColumn44.DataPropertyName = "Item_Name"
        Me.DataGridViewTextBoxColumn44.FillWeight = 300.0!
        Me.DataGridViewTextBoxColumn44.HeaderText = "الصنف"
        Me.DataGridViewTextBoxColumn44.Name = "DataGridViewTextBoxColumn44"
        Me.DataGridViewTextBoxColumn44.ReadOnly = True
        '
        'DataGridViewTextBoxColumn45
        '
        Me.DataGridViewTextBoxColumn45.DataPropertyName = "U_Name"
        Me.DataGridViewTextBoxColumn45.FillWeight = 150.0!
        Me.DataGridViewTextBoxColumn45.HeaderText = "الوحدة"
        Me.DataGridViewTextBoxColumn45.Name = "DataGridViewTextBoxColumn45"
        Me.DataGridViewTextBoxColumn45.ReadOnly = True
        '
        'DataGridViewTextBoxColumn46
        '
        Me.DataGridViewTextBoxColumn46.DataPropertyName = "S_QTY"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewTextBoxColumn46.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn46.FillWeight = 150.0!
        Me.DataGridViewTextBoxColumn46.HeaderText = "الكمية"
        Me.DataGridViewTextBoxColumn46.Name = "DataGridViewTextBoxColumn46"
        Me.DataGridViewTextBoxColumn46.ReadOnly = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Frm_Dt_DGV_2)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(3, 42)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox2.Size = New System.Drawing.Size(891, 251)
        Me.GroupBox2.TabIndex = 693
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "المنتجات النهائية"
        '
        'Frm_Dt_DGV_2
        '
        Me.Frm_Dt_DGV_2.AllowUserToAddRows = False
        Me.Frm_Dt_DGV_2.AllowUserToDeleteRows = False
        Me.Frm_Dt_DGV_2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Frm_Dt_DGV_2.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Frm_Dt_DGV_2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.Frm_Dt_DGV_2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Frm_Dt_DGV_2.FilterAndSortEnabled = True
        Me.Frm_Dt_DGV_2.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.Frm_Dt_DGV_2.Location = New System.Drawing.Point(8, 24)
        Me.Frm_Dt_DGV_2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Frm_Dt_DGV_2.MultiSelect = False
        Me.Frm_Dt_DGV_2.Name = "Frm_Dt_DGV_2"
        Me.Frm_Dt_DGV_2.ReadOnly = True
        Me.Frm_Dt_DGV_2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Frm_Dt_DGV_2.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frm_Dt_DGV_2.RowTemplate.Height = 25
        Me.Frm_Dt_DGV_2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Frm_Dt_DGV_2.Size = New System.Drawing.Size(878, 220)
        Me.Frm_Dt_DGV_2.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.Frm_Dt_DGV_2.TabIndex = 903
        '
        'Frm_Dt_DGV
        '
        Me.Frm_Dt_DGV.AllowUserToAddRows = False
        Me.Frm_Dt_DGV.AllowUserToDeleteRows = False
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.Gainsboro
        Me.Frm_Dt_DGV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle8
        Me.Frm_Dt_DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Frm_Dt_DGV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.Frm_Dt_DGV.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Frm_Dt_DGV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.Frm_Dt_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Frm_Dt_DGV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn27, Me.DataGridViewTextBoxColumn39, Me.DataGridViewTextBoxColumn40, Me.DataGridViewTextBoxColumn41})
        Me.Frm_Dt_DGV.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Frm_Dt_DGV.DefaultCellStyle = DataGridViewCellStyle11
        Me.Frm_Dt_DGV.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.Frm_Dt_DGV.Location = New System.Drawing.Point(579, 30)
        Me.Frm_Dt_DGV.Name = "Frm_Dt_DGV"
        Me.Frm_Dt_DGV.ReadOnly = True
        Me.Frm_Dt_DGV.RowHeadersVisible = False
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.Silver
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Frm_Dt_DGV.RowsDefaultCellStyle = DataGridViewCellStyle12
        Me.Frm_Dt_DGV.RowTemplate.Height = 32
        Me.Frm_Dt_DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Frm_Dt_DGV.Size = New System.Drawing.Size(10, 14)
        Me.Frm_Dt_DGV.TabIndex = 683
        Me.Frm_Dt_DGV.Visible = False
        '
        'DataGridViewTextBoxColumn27
        '
        Me.DataGridViewTextBoxColumn27.DataPropertyName = "St_Name"
        Me.DataGridViewTextBoxColumn27.FillWeight = 150.0!
        Me.DataGridViewTextBoxColumn27.HeaderText = "المخزن"
        Me.DataGridViewTextBoxColumn27.Name = "DataGridViewTextBoxColumn27"
        Me.DataGridViewTextBoxColumn27.ReadOnly = True
        '
        'DataGridViewTextBoxColumn39
        '
        Me.DataGridViewTextBoxColumn39.DataPropertyName = "Item_Name"
        Me.DataGridViewTextBoxColumn39.FillWeight = 300.0!
        Me.DataGridViewTextBoxColumn39.HeaderText = "الصنف"
        Me.DataGridViewTextBoxColumn39.Name = "DataGridViewTextBoxColumn39"
        Me.DataGridViewTextBoxColumn39.ReadOnly = True
        '
        'DataGridViewTextBoxColumn40
        '
        Me.DataGridViewTextBoxColumn40.DataPropertyName = "U_Name"
        Me.DataGridViewTextBoxColumn40.FillWeight = 150.0!
        Me.DataGridViewTextBoxColumn40.HeaderText = "الوحدة"
        Me.DataGridViewTextBoxColumn40.Name = "DataGridViewTextBoxColumn40"
        Me.DataGridViewTextBoxColumn40.ReadOnly = True
        '
        'DataGridViewTextBoxColumn41
        '
        Me.DataGridViewTextBoxColumn41.DataPropertyName = "S_QTY"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewTextBoxColumn41.DefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridViewTextBoxColumn41.FillWeight = 150.0!
        Me.DataGridViewTextBoxColumn41.HeaderText = "الكمية"
        Me.DataGridViewTextBoxColumn41.Name = "DataGridViewTextBoxColumn41"
        Me.DataGridViewTextBoxColumn41.ReadOnly = True
        '
        'Print_Frm_Cn_Btn
        '
        Me.Print_Frm_Cn_Btn.BackColor = System.Drawing.Color.White
        Me.Print_Frm_Cn_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_Frm_Cn_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Print_Frm_Cn_Btn.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Print_Frm_Cn_Btn.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.Print_Frm_Cn_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_Frm_Cn_Btn.Location = New System.Drawing.Point(595, 2)
        Me.Print_Frm_Cn_Btn.Name = "Print_Frm_Cn_Btn"
        Me.Print_Frm_Cn_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_Frm_Cn_Btn.Size = New System.Drawing.Size(105, 38)
        Me.Print_Frm_Cn_Btn.TabIndex = 695
        Me.Print_Frm_Cn_Btn.Text = "طباعة المواد الإستهلاكية"
        Me.Print_Frm_Cn_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Print_Frm_Cn_Btn.UseVisualStyleBackColor = False
        '
        'Frm_Search_Btn
        '
        Me.Frm_Search_Btn.BackColor = System.Drawing.Color.White
        Me.Frm_Search_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Frm_Search_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Frm_Search_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Frm_Search_Btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frm_Search_Btn.ForeColor = System.Drawing.Color.Black
        Me.Frm_Search_Btn.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.Frm_Search_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Frm_Search_Btn.Location = New System.Drawing.Point(810, 2)
        Me.Frm_Search_Btn.Name = "Frm_Search_Btn"
        Me.Frm_Search_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Frm_Search_Btn.Size = New System.Drawing.Size(84, 38)
        Me.Frm_Search_Btn.TabIndex = 697
        Me.Frm_Search_Btn.Text = "بحث"
        Me.Frm_Search_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Frm_Search_Btn.UseVisualStyleBackColor = False
        '
        'Print_Frm_Dt_Btn
        '
        Me.Print_Frm_Dt_Btn.BackColor = System.Drawing.Color.White
        Me.Print_Frm_Dt_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_Frm_Dt_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Print_Frm_Dt_Btn.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Print_Frm_Dt_Btn.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.Print_Frm_Dt_Btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_Frm_Dt_Btn.Location = New System.Drawing.Point(702, 2)
        Me.Print_Frm_Dt_Btn.Name = "Print_Frm_Dt_Btn"
        Me.Print_Frm_Dt_Btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_Frm_Dt_Btn.Size = New System.Drawing.Size(106, 38)
        Me.Print_Frm_Dt_Btn.TabIndex = 694
        Me.Print_Frm_Dt_Btn.Text = "طباعة" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "المنتجات النهائية"
        Me.Print_Frm_Dt_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Print_Frm_Dt_Btn.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(264, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 19)
        Me.Label4.TabIndex = 702
        Me.Label4.Text = "الموظف"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(544, 8)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 19)
        Me.Label9.TabIndex = 701
        Me.Label9.Text = "المخزن"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AG_Cm
        '
        Me.AG_Cm.CancelSearchImage = CType(resources.GetObject("AG_Cm.CancelSearchImage"), System.Drawing.Image)
        Me.AG_Cm.Location = New System.Drawing.Point(11, 2)
        Me.AG_Cm.Name = "AG_Cm"
        Me.AG_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AG_Cm.Size = New System.Drawing.Size(249, 34)
        Me.AG_Cm.SQL_Column = "AG_NAME"
        Me.AG_Cm.SQL_ID = "AG_ID"
        Me.AG_Cm.SQL_IsNumericSearchField = False
        Me.AG_Cm.SQL_ListSize = 200
        Me.AG_Cm.SQL_NumberOfRows = 200
        Me.AG_Cm.SQL_OrderByField = "AG_NAME"
        Me.AG_Cm.SQL_SearchField = "AG_NAME"
        Me.AG_Cm.SQL_Table = "Agents_Emp_V"
        Me.AG_Cm.TabIndex = 700
        Me.AG_Cm.TextMaxLength = 250
        Me.AG_Cm.Textt = ""
        '
        'Frm_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(895, 659)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Frm_Cn_DGV)
        Me.Controls.Add(Me.Frm_Dt_DGV)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.AG_Cm)
        Me.Controls.Add(Me.Frm_ST_cm)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Print_Frm_Cn_Btn)
        Me.Controls.Add(Me.Frm_Search_Btn)
        Me.Controls.Add(Me.Print_Frm_Dt_Btn)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_Report"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.Frm_Cn_DGV_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Frm_Cn_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.Frm_Dt_DGV_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Frm_Dt_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataB_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataB_2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Frm_ST_cm As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Frm_Cn_DGV As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents DataGridViewTextBoxColumn43 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn44 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn45 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn46 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Frm_Dt_DGV As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents DataGridViewTextBoxColumn27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn39 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn40 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn41 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Print_Frm_Cn_Btn As System.Windows.Forms.Button
    Friend WithEvents Frm_Search_Btn As System.Windows.Forms.Button
    Friend WithEvents Print_Frm_Dt_Btn As System.Windows.Forms.Button
    Friend WithEvents Frm_Cn_DGV_2 As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents Frm_Dt_DGV_2 As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents AG_Cm As resturant.FSearch_Filter
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents DataB_1 As System.Windows.Forms.BindingSource
    Friend WithEvents DataB_2 As System.Windows.Forms.BindingSource
End Class
