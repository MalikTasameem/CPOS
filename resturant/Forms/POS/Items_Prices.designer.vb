<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Items_Prices
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Items_Prices))
        Me.DataGridViewX = New Zuby.ADGV.AdvancedDataGridView()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.item_id_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_Num_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.item_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unit_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Barcode_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cost_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price_2_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price_3_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DGV_Control_btn = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ST_cm = New System.Windows.Forms.ComboBox()
        Me.PrintButton = New System.Windows.Forms.Button()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.DataB = New System.Windows.Forms.BindingSource(Me.components)
        Me.IMNUM_CB = New System.Windows.Forms.CheckBox()
        Me.BarcodeSearch_CB = New System.Windows.Forms.CheckBox()
        Me.CMSearchTextBox = New System.Windows.Forms.TextBox()
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
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewX.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.item_id_CL, Me.IM_Num_CL, Me.item_name_CL, Me.Unit_CL, Me.Barcode_CL, Me.QTY_CL, Me.Cost_CL, Me.Price_CL, Me.Price_2_CL, Me.Price_3_CL})
        Me.DataGridViewX.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewX.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewX.FilterAndSortEnabled = True
        Me.DataGridViewX.FilterStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DataGridViewX.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DataGridViewX.Location = New System.Drawing.Point(2, 100)
        Me.DataGridViewX.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.DataGridViewX.MultiSelect = False
        Me.DataGridViewX.Name = "DataGridViewX"
        Me.DataGridViewX.ReadOnly = True
        Me.DataGridViewX.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DataGridViewX.RowHeadersVisible = False
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewX.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewX.Size = New System.Drawing.Size(1001, 559)
        Me.DataGridViewX.SortStringChangedInvokeBeforeDatasourceUpdate = True
        Me.DataGridViewX.TabIndex = 2
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "ت"
        Me.T_ID_CL.MinimumWidth = 22
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'item_id_CL
        '
        Me.item_id_CL.DataPropertyName = "IM_ID"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.item_id_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.item_id_CL.HeaderText = "IM_ID"
        Me.item_id_CL.MinimumWidth = 22
        Me.item_id_CL.Name = "item_id_CL"
        Me.item_id_CL.ReadOnly = True
        Me.item_id_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Me.item_id_CL.Visible = False
        '
        'IM_Num_CL
        '
        Me.IM_Num_CL.DataPropertyName = "IM_Num"
        Me.IM_Num_CL.HeaderText = "رقم الصنف"
        Me.IM_Num_CL.MinimumWidth = 22
        Me.IM_Num_CL.Name = "IM_Num_CL"
        Me.IM_Num_CL.ReadOnly = True
        Me.IM_Num_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'item_name_CL
        '
        Me.item_name_CL.DataPropertyName = "item_name"
        Me.item_name_CL.FillWeight = 119.5432!
        Me.item_name_CL.HeaderText = "الصنف"
        Me.item_name_CL.MinimumWidth = 22
        Me.item_name_CL.Name = "item_name_CL"
        Me.item_name_CL.ReadOnly = True
        Me.item_name_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.item_name_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'Unit_CL
        '
        Me.Unit_CL.DataPropertyName = "U_Name"
        Me.Unit_CL.FillWeight = 60.9137!
        Me.Unit_CL.HeaderText = "الوحدة"
        Me.Unit_CL.MinimumWidth = 22
        Me.Unit_CL.Name = "Unit_CL"
        Me.Unit_CL.ReadOnly = True
        Me.Unit_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'Barcode_CL
        '
        Me.Barcode_CL.DataPropertyName = "Barcode"
        Me.Barcode_CL.HeaderText = "باركود"
        Me.Barcode_CL.MinimumWidth = 22
        Me.Barcode_CL.Name = "Barcode_CL"
        Me.Barcode_CL.ReadOnly = True
        Me.Barcode_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Green
        DataGridViewCellStyle4.Format = "N2"
        Me.QTY_CL.DefaultCellStyle = DataGridViewCellStyle4
        Me.QTY_CL.FillWeight = 119.5432!
        Me.QTY_CL.HeaderText = "الكمية"
        Me.QTY_CL.MinimumWidth = 22
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        Me.QTY_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'Cost_CL
        '
        Me.Cost_CL.DataPropertyName = "Cost"
        Me.Cost_CL.HeaderText = "التكلفة"
        Me.Cost_CL.MinimumWidth = 22
        Me.Cost_CL.Name = "Cost_CL"
        Me.Cost_CL.ReadOnly = True
        Me.Cost_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'Price_CL
        '
        Me.Price_CL.DataPropertyName = "Price"
        Me.Price_CL.HeaderText = "البيع"
        Me.Price_CL.MinimumWidth = 22
        Me.Price_CL.Name = "Price_CL"
        Me.Price_CL.ReadOnly = True
        Me.Price_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'Price_2_CL
        '
        Me.Price_2_CL.DataPropertyName = "Min_SP"
        Me.Price_2_CL.HeaderText = "بيع الجملة"
        Me.Price_2_CL.MinimumWidth = 22
        Me.Price_2_CL.Name = "Price_2_CL"
        Me.Price_2_CL.ReadOnly = True
        Me.Price_2_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'Price_3_CL
        '
        Me.Price_3_CL.DataPropertyName = "Min_SP_2"
        Me.Price_3_CL.HeaderText = "جملة الجملة"
        Me.Price_3_CL.MinimumWidth = 22
        Me.Price_3_CL.Name = "Price_3_CL"
        Me.Price_3_CL.ReadOnly = True
        Me.Price_3_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(2, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label3.Size = New System.Drawing.Size(1002, 33)
        Me.Label3.TabIndex = 685
        Me.Label3.Text = "كشف لوائــح الأسعــــار"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DGV_Control_btn
        '
        Me.DGV_Control_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DGV_Control_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DGV_Control_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DGV_Control_btn.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DGV_Control_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.DGV_Control_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.DGV_Control_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DGV_Control_btn.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold)
        Me.DGV_Control_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DGV_Control_btn.Image = Global.resturant.My.Resources.Resources.iconfinder_menu_1814109
        Me.DGV_Control_btn.Location = New System.Drawing.Point(4, 660)
        Me.DGV_Control_btn.Margin = New System.Windows.Forms.Padding(2)
        Me.DGV_Control_btn.Name = "DGV_Control_btn"
        Me.DGV_Control_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DGV_Control_btn.Size = New System.Drawing.Size(88, 31)
        Me.DGV_Control_btn.TabIndex = 675
        Me.DGV_Control_btn.TabStop = False
        Me.DGV_Control_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DGV_Control_btn.UseVisualStyleBackColor = False
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(861, 660)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(2)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(143, 33)
        Me.ExitFormButton.TabIndex = 674
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Times New Roman", 12.25!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(314, 35)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Button1.Size = New System.Drawing.Size(142, 29)
        Me.Button1.TabIndex = 390
        Me.Button1.Text = "تحديث القائمة"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(954, 42)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 17)
        Me.Label9.TabIndex = 687
        Me.Label9.Text = "المخزن"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ST_cm
        '
        Me.ST_cm.BackColor = System.Drawing.SystemColors.Info
        Me.ST_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ST_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ST_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ST_cm.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold)
        Me.ST_cm.FormattingEnabled = True
        Me.ST_cm.Location = New System.Drawing.Point(460, 37)
        Me.ST_cm.Margin = New System.Windows.Forms.Padding(2)
        Me.ST_cm.Name = "ST_cm"
        Me.ST_cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ST_cm.Size = New System.Drawing.Size(490, 25)
        Me.ST_cm.TabIndex = 686
        '
        'PrintButton
        '
        Me.PrintButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PrintButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PrintButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PrintButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PrintButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.PrintButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.PrintButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PrintButton.Font = New System.Drawing.Font("Times New Roman", 12.25!, System.Drawing.FontStyle.Bold)
        Me.PrintButton.ForeColor = System.Drawing.Color.Black
        Me.PrintButton.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.PrintButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.PrintButton.Location = New System.Drawing.Point(166, 35)
        Me.PrintButton.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.PrintButton.Name = "PrintButton"
        Me.PrintButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PrintButton.Size = New System.Drawing.Size(146, 29)
        Me.PrintButton.TabIndex = 274
        Me.PrintButton.Text = "طباعــــة"
        Me.PrintButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.PrintButton.UseVisualStyleBackColor = False
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.HorizontalScrollbar = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(42, 40)
        Me.CheckedListBox1.MultiColumn = True
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CheckedListBox1.Size = New System.Drawing.Size(88, 19)
        Me.CheckedListBox1.TabIndex = 902
        Me.CheckedListBox1.Visible = False
        '
        'IMNUM_CB
        '
        Me.IMNUM_CB.AutoSize = True
        Me.IMNUM_CB.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.IMNUM_CB.Location = New System.Drawing.Point(89, 74)
        Me.IMNUM_CB.Name = "IMNUM_CB"
        Me.IMNUM_CB.Size = New System.Drawing.Size(86, 22)
        Me.IMNUM_CB.TabIndex = 907
        Me.IMNUM_CB.Text = "برقم الصنف"
        Me.IMNUM_CB.UseVisualStyleBackColor = True
        Me.IMNUM_CB.Visible = False
        '
        'BarcodeSearch_CB
        '
        Me.BarcodeSearch_CB.AutoSize = True
        Me.BarcodeSearch_CB.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.BarcodeSearch_CB.Location = New System.Drawing.Point(181, 74)
        Me.BarcodeSearch_CB.Name = "BarcodeSearch_CB"
        Me.BarcodeSearch_CB.Size = New System.Drawing.Size(70, 22)
        Me.BarcodeSearch_CB.TabIndex = 906
        Me.BarcodeSearch_CB.Text = "بالباركود"
        Me.BarcodeSearch_CB.UseVisualStyleBackColor = True
        Me.BarcodeSearch_CB.Visible = False
        '
        'CMSearchTextBox
        '
        Me.CMSearchTextBox.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CMSearchTextBox.ForeColor = System.Drawing.SystemColors.InfoText
        Me.CMSearchTextBox.Location = New System.Drawing.Point(254, 73)
        Me.CMSearchTextBox.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
        Me.CMSearchTextBox.Name = "CMSearchTextBox"
        Me.CMSearchTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CMSearchTextBox.Size = New System.Drawing.Size(749, 25)
        Me.CMSearchTextBox.TabIndex = 905
        Me.CMSearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Items_Prices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 695)
        Me.Controls.Add(Me.IMNUM_CB)
        Me.Controls.Add(Me.BarcodeSearch_CB)
        Me.Controls.Add(Me.CMSearchTextBox)
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.ST_cm)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridViewX)
        Me.Controls.Add(Me.PrintButton)
        Me.Controls.Add(Me.DGV_Control_btn)
        Me.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.Name = "Items_Prices"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "لوائح الأسعار"
        CType(Me.DataGridViewX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents DGV_Control_btn As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ST_cm As System.Windows.Forms.ComboBox
    Friend WithEvents PrintButton As System.Windows.Forms.Button
    Public WithEvents DataGridViewX As Zuby.ADGV.AdvancedDataGridView
    Friend WithEvents CheckedListBox1 As System.Windows.Forms.CheckedListBox
    Friend WithEvents DataB As System.Windows.Forms.BindingSource
    Friend WithEvents T_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_id_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_Num_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Barcode_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cost_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price_2_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price_3_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IMNUM_CB As System.Windows.Forms.CheckBox
    Friend WithEvents BarcodeSearch_CB As System.Windows.Forms.CheckBox
    Friend WithEvents CMSearchTextBox As System.Windows.Forms.TextBox
End Class
