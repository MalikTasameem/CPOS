<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IMP_Perfet_Report_2
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IMP_Perfet_Report_2))
        Me.Min_SB_Panel = New System.Windows.Forms.Panel()
        Me.Sales_Type_Cm = New System.Windows.Forms.ComboBox()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.PerfetGM_Serach = New System.Windows.Forms.ComboBox()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.ST_cm = New System.Windows.Forms.ComboBox()
        Me.IMPerf_DGV = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn23 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total_Perfet_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IMMV_Search_txt = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.B_Berfet_Print_btn = New System.Windows.Forms.Button()
        Me.B_Berfet_Pdf_btn = New System.Windows.Forms.Button()
        Me.IMPerfTotals_DGV = New System.Windows.Forms.DataGridView()
        Me.TotalsTitle_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalsProfit_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Min_SB_Panel.SuspendLayout()
        CType(Me.IMPerf_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IMPerfTotals_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Min_SB_Panel
        '
        Me.Min_SB_Panel.Controls.Add(Me.Sales_Type_Cm)
        Me.Min_SB_Panel.Controls.Add(Me.Label95)
        Me.Min_SB_Panel.Location = New System.Drawing.Point(1, 2)
        Me.Min_SB_Panel.Name = "Min_SB_Panel"
        Me.Min_SB_Panel.Size = New System.Drawing.Size(240, 35)
        Me.Min_SB_Panel.TabIndex = 707
        '
        'Sales_Type_Cm
        '
        Me.Sales_Type_Cm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.Sales_Type_Cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Sales_Type_Cm.BackColor = System.Drawing.SystemColors.Info
        Me.Sales_Type_Cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Sales_Type_Cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Sales_Type_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Sales_Type_Cm.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Sales_Type_Cm.FormattingEnabled = True
        Me.Sales_Type_Cm.IntegralHeight = False
        Me.Sales_Type_Cm.Location = New System.Drawing.Point(3, 3)
        Me.Sales_Type_Cm.Name = "Sales_Type_Cm"
        Me.Sales_Type_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Sales_Type_Cm.Size = New System.Drawing.Size(131, 25)
        Me.Sales_Type_Cm.TabIndex = 662
        '
        'Label95
        '
        Me.Label95.AutoSize = True
        Me.Label95.BackColor = System.Drawing.Color.Transparent
        Me.Label95.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label95.Location = New System.Drawing.Point(140, 7)
        Me.Label95.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(85, 19)
        Me.Label95.TabIndex = 661
        Me.Label95.Text = "نوع المبيعات"
        Me.Label95.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PerfetGM_Serach
        '
        Me.PerfetGM_Serach.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PerfetGM_Serach.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.PerfetGM_Serach.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.PerfetGM_Serach.BackColor = System.Drawing.SystemColors.Info
        Me.PerfetGM_Serach.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PerfetGM_Serach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PerfetGM_Serach.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PerfetGM_Serach.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.PerfetGM_Serach.FormattingEnabled = True
        Me.PerfetGM_Serach.IntegralHeight = False
        Me.PerfetGM_Serach.Location = New System.Drawing.Point(244, 4)
        Me.PerfetGM_Serach.Name = "PerfetGM_Serach"
        Me.PerfetGM_Serach.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PerfetGM_Serach.Size = New System.Drawing.Size(185, 25)
        Me.PerfetGM_Serach.TabIndex = 706
        '
        'Label74
        '
        Me.Label74.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label74.AutoSize = True
        Me.Label74.BackColor = System.Drawing.Color.Transparent
        Me.Label74.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label74.Location = New System.Drawing.Point(434, 6)
        Me.Label74.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(61, 19)
        Me.Label74.TabIndex = 705
        Me.Label74.Text = "التصنيف"
        Me.Label74.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label73
        '
        Me.Label73.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label73.AutoSize = True
        Me.Label73.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label73.Location = New System.Drawing.Point(674, 5)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(49, 19)
        Me.Label73.TabIndex = 704
        Me.Label73.Text = "المخزن"
        Me.Label73.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ST_cm
        '
        Me.ST_cm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ST_cm.BackColor = System.Drawing.SystemColors.Info
        Me.ST_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ST_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ST_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ST_cm.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ST_cm.FormattingEnabled = True
        Me.ST_cm.Location = New System.Drawing.Point(500, 3)
        Me.ST_cm.Name = "ST_cm"
        Me.ST_cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ST_cm.Size = New System.Drawing.Size(169, 25)
        Me.ST_cm.TabIndex = 703
        '
        'IMPerf_DGV
        '
        Me.IMPerf_DGV.AllowUserToAddRows = False
        Me.IMPerf_DGV.AllowUserToDeleteRows = False
        Me.IMPerf_DGV.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.IMPerf_DGV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.IMPerf_DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.IMPerf_DGV.BackgroundColor = System.Drawing.Color.White
        Me.IMPerf_DGV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(68, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(68, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IMPerf_DGV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.IMPerf_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.IMPerf_DGV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn23, Me.Total_Perfet_CL})
        Me.IMPerf_DGV.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(219, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(254, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IMPerf_DGV.DefaultCellStyle = DataGridViewCellStyle4
        Me.IMPerf_DGV.EnableHeadersVisualStyles = False
        Me.IMPerf_DGV.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.IMPerf_DGV.Location = New System.Drawing.Point(0, 70)
        Me.IMPerf_DGV.MultiSelect = False
        Me.IMPerf_DGV.Name = "IMPerf_DGV"
        Me.IMPerf_DGV.ReadOnly = True
        Me.IMPerf_DGV.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IMPerf_DGV.RowHeadersVisible = False
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.IMPerf_DGV.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.IMPerf_DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.IMPerf_DGV.Size = New System.Drawing.Size(894, 521)
        Me.IMPerf_DGV.TabIndex = 702
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.DataPropertyName = "Item_Name"
        Me.DataGridViewTextBoxColumn23.FillWeight = 37.38512!
        Me.DataGridViewTextBoxColumn23.HeaderText = "الصنف"
        Me.DataGridViewTextBoxColumn23.MinimumWidth = 150
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        Me.DataGridViewTextBoxColumn23.ReadOnly = True
        '
        'Total_Perfet_CL
        '
        Me.Total_Perfet_CL.DataPropertyName = "Total_Perfet"
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Total_Perfet_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.Total_Perfet_CL.HeaderText = "هامش الربح"
        Me.Total_Perfet_CL.Name = "Total_Perfet_CL"
        Me.Total_Perfet_CL.ReadOnly = True
        '
        'IMMV_Search_txt
        '
        Me.IMMV_Search_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IMMV_Search_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IMMV_Search_txt.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMMV_Search_txt.ForeColor = System.Drawing.SystemColors.InfoText
        Me.IMMV_Search_txt.Location = New System.Drawing.Point(1, 43)
        Me.IMMV_Search_txt.Name = "IMMV_Search_txt"
        Me.IMMV_Search_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IMMV_Search_txt.Size = New System.Drawing.Size(727, 25)
        Me.IMMV_Search_txt.TabIndex = 709
        Me.IMMV_Search_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Location = New System.Drawing.Point(729, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Button1.Size = New System.Drawing.Size(165, 29)
        Me.Button1.TabIndex = 710
        Me.Button1.Text = "⌕ بحث"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'B_Berfet_Print_btn
        '
        Me.B_Berfet_Print_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.B_Berfet_Print_btn.BackColor = System.Drawing.Color.White
        Me.B_Berfet_Print_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B_Berfet_Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.B_Berfet_Print_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.B_Berfet_Print_btn.Location = New System.Drawing.Point(812, 40)
        Me.B_Berfet_Print_btn.Name = "B_Berfet_Print_btn"
        Me.B_Berfet_Print_btn.Size = New System.Drawing.Size(82, 29)
        Me.B_Berfet_Print_btn.TabIndex = 711
        Me.B_Berfet_Print_btn.Text = "⎙ طباعة"
        Me.B_Berfet_Print_btn.UseVisualStyleBackColor = False
        '
        'B_Berfet_Pdf_btn
        '
        Me.B_Berfet_Pdf_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.B_Berfet_Pdf_btn.BackColor = System.Drawing.Color.White
        Me.B_Berfet_Pdf_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.B_Berfet_Pdf_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.B_Berfet_Pdf_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.B_Berfet_Pdf_btn.Location = New System.Drawing.Point(729, 39)
        Me.B_Berfet_Pdf_btn.Name = "B_Berfet_Pdf_btn"
        Me.B_Berfet_Pdf_btn.Size = New System.Drawing.Size(82, 29)
        Me.B_Berfet_Pdf_btn.TabIndex = 712
        Me.B_Berfet_Pdf_btn.Text = "▣ PDF"
        Me.B_Berfet_Pdf_btn.UseVisualStyleBackColor = False
        '
        'IMPerfTotals_DGV
        '
        Me.IMPerfTotals_DGV.AllowUserToAddRows = False
        Me.IMPerfTotals_DGV.AllowUserToDeleteRows = False
        Me.IMPerfTotals_DGV.AllowUserToResizeRows = False
        Me.IMPerfTotals_DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.IMPerfTotals_DGV.BackgroundColor = System.Drawing.Color.White
        Me.IMPerfTotals_DGV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(45, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(45, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IMPerfTotals_DGV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.IMPerfTotals_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.IMPerfTotals_DGV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TotalsTitle_CL, Me.TotalsProfit_CL})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(245, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(45, Byte), Integer))
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(187, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(208, Byte), Integer))
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(83, Byte), Integer), CType(CType(45, Byte), Integer))
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IMPerfTotals_DGV.DefaultCellStyle = DataGridViewCellStyle8
        Me.IMPerfTotals_DGV.EnableHeadersVisualStyles = False
        Me.IMPerfTotals_DGV.GridColor = System.Drawing.Color.FromArgb(CType(CType(187, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.IMPerfTotals_DGV.Location = New System.Drawing.Point(0, 591)
        Me.IMPerfTotals_DGV.MultiSelect = False
        Me.IMPerfTotals_DGV.Name = "IMPerfTotals_DGV"
        Me.IMPerfTotals_DGV.ReadOnly = True
        Me.IMPerfTotals_DGV.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IMPerfTotals_DGV.RowHeadersVisible = False
        Me.IMPerfTotals_DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.IMPerfTotals_DGV.Size = New System.Drawing.Size(894, 67)
        Me.IMPerfTotals_DGV.TabIndex = 713
        '
        'TotalsTitle_CL
        '
        Me.TotalsTitle_CL.HeaderText = "البيان"
        Me.TotalsTitle_CL.Name = "TotalsTitle_CL"
        Me.TotalsTitle_CL.ReadOnly = True
        '
        'TotalsProfit_CL
        '
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.TotalsProfit_CL.DefaultCellStyle = DataGridViewCellStyle7
        Me.TotalsProfit_CL.HeaderText = "هامش الربح"
        Me.TotalsProfit_CL.Name = "TotalsProfit_CL"
        Me.TotalsProfit_CL.ReadOnly = True
        '
        'IMP_Perfet_Report_2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(895, 659)
        Me.ControlBox = False
        Me.Controls.Add(Me.IMPerfTotals_DGV)
        Me.Controls.Add(Me.B_Berfet_Print_btn)
        Me.Controls.Add(Me.B_Berfet_Pdf_btn)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.IMMV_Search_txt)
        Me.Controls.Add(Me.Min_SB_Panel)
        Me.Controls.Add(Me.PerfetGM_Serach)
        Me.Controls.Add(Me.Label74)
        Me.Controls.Add(Me.Label73)
        Me.Controls.Add(Me.ST_cm)
        Me.Controls.Add(Me.IMPerf_DGV)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "IMP_Perfet_Report_2"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Min_SB_Panel.ResumeLayout(False)
        Me.Min_SB_Panel.PerformLayout()
        CType(Me.IMPerf_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IMPerfTotals_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Min_SB_Panel As System.Windows.Forms.Panel
    Friend WithEvents Sales_Type_Cm As System.Windows.Forms.ComboBox
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents PerfetGM_Serach As System.Windows.Forms.ComboBox
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents ST_cm As System.Windows.Forms.ComboBox
    Public WithEvents IMPerf_DGV As System.Windows.Forms.DataGridView
    Friend WithEvents IMMV_Search_txt As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents B_Berfet_Print_btn As System.Windows.Forms.Button
    Friend WithEvents B_Berfet_Pdf_btn As System.Windows.Forms.Button
    Friend WithEvents IMPerfTotals_DGV As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn23 As DataGridViewTextBoxColumn
    Friend WithEvents Total_Perfet_CL As DataGridViewTextBoxColumn
    Friend WithEvents TotalsTitle_CL As DataGridViewTextBoxColumn
    Friend WithEvents TotalsProfit_CL As DataGridViewTextBoxColumn
End Class
