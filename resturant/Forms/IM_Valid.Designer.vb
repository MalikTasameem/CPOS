<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IM_Valid
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
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IM_Valid))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TotalCost_txt = New System.Windows.Forms.TextBox()
        Me.DataGridViewX = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TotalQYT_txt = New System.Windows.Forms.TextBox()
        Me.PrintButton = New System.Windows.Forms.Button()
        Me.CMSearchTextBox = New System.Windows.Forms.TextBox()
        Me.DateTimePicker_From = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.DGV_Control_btn = New System.Windows.Forms.Button()
        Me.ST_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.item_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_Num_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unit_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D_Valid_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cost_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Case_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridViewX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("JF Flat", 15.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(2, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label3.Size = New System.Drawing.Size(768, 46)
        Me.Label3.TabIndex = 658
        Me.Label3.Text = "إشعـــار الصلاحية"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("JF Flat", 10.75!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(527, 610)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(70, 26)
        Me.Label2.TabIndex = 656
        Me.Label2.Text = "التكلفة"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TotalCost_txt
        '
        Me.TotalCost_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TotalCost_txt.Font = New System.Drawing.Font("Stencil", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalCost_txt.ForeColor = System.Drawing.SystemColors.InfoText
        Me.TotalCost_txt.Location = New System.Drawing.Point(601, 608)
        Me.TotalCost_txt.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.TotalCost_txt.Name = "TotalCost_txt"
        Me.TotalCost_txt.ReadOnly = True
        Me.TotalCost_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TotalCost_txt.Size = New System.Drawing.Size(169, 30)
        Me.TotalCost_txt.TabIndex = 655
        Me.TotalCost_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DataGridViewX
        '
        Me.DataGridViewX.AllowUserToAddRows = False
        Me.DataGridViewX.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info
        Me.DataGridViewX.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewX.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewX.BackgroundColor = System.Drawing.Color.SeaShell
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("JF Flat", 9.5!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewX.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ST_Name_CL, Me.item_name_CL, Me.IM_Num_CL, Me.Unit_CL, Me.D_Valid_CL, Me.QTY_CL, Me.Cost_CL, Me.Case_CL})
        Me.DataGridViewX.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("JF Flat", 9.5!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewX.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewX.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DataGridViewX.Location = New System.Drawing.Point(2, 79)
        Me.DataGridViewX.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.DataGridViewX.MultiSelect = False
        Me.DataGridViewX.Name = "DataGridViewX"
        Me.DataGridViewX.ReadOnly = True
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewX.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridViewX.RowTemplate.Height = 35
        Me.DataGridViewX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewX.Size = New System.Drawing.Size(769, 527)
        Me.DataGridViewX.TabIndex = 649
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("JF Flat", 10.75!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(305, 611)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(71, 26)
        Me.Label1.TabIndex = 654
        Me.Label1.Text = "الكميات"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TotalQYT_txt
        '
        Me.TotalQYT_txt.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TotalQYT_txt.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalQYT_txt.ForeColor = System.Drawing.SystemColors.InfoText
        Me.TotalQYT_txt.Location = New System.Drawing.Point(378, 609)
        Me.TotalQYT_txt.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.TotalQYT_txt.Name = "TotalQYT_txt"
        Me.TotalQYT_txt.ReadOnly = True
        Me.TotalQYT_txt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TotalQYT_txt.Size = New System.Drawing.Size(145, 29)
        Me.TotalQYT_txt.TabIndex = 653
        Me.TotalQYT_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PrintButton
        '
        Me.PrintButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PrintButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PrintButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PrintButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.PrintButton.Font = New System.Drawing.Font("JF Flat", 10.75!, System.Drawing.FontStyle.Bold)
        Me.PrintButton.ForeColor = System.Drawing.Color.Black
        Me.PrintButton.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.PrintButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.PrintButton.Location = New System.Drawing.Point(187, 607)
        Me.PrintButton.Margin = New System.Windows.Forms.Padding(6, 9, 6, 9)
        Me.PrintButton.Name = "PrintButton"
        Me.PrintButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PrintButton.Size = New System.Drawing.Size(104, 44)
        Me.PrintButton.TabIndex = 652
        Me.PrintButton.Text = "طباعة"
        Me.PrintButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.PrintButton.UseVisualStyleBackColor = False
        '
        'CMSearchTextBox
        '
        Me.CMSearchTextBox.Font = New System.Drawing.Font("JF Flat", 9.75!, System.Drawing.FontStyle.Bold)
        Me.CMSearchTextBox.ForeColor = System.Drawing.SystemColors.InfoText
        Me.CMSearchTextBox.Location = New System.Drawing.Point(225, 49)
        Me.CMSearchTextBox.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.CMSearchTextBox.Name = "CMSearchTextBox"
        Me.CMSearchTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CMSearchTextBox.Size = New System.Drawing.Size(546, 30)
        Me.CMSearchTextBox.TabIndex = 650
        Me.CMSearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DateTimePicker_From
        '
        Me.DateTimePicker_From.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DateTimePicker_From.CustomFormat = "dd/MM/yyyy"
        Me.DateTimePicker_From.Enabled = False
        Me.DateTimePicker_From.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker_From.Location = New System.Drawing.Point(132, 50)
        Me.DateTimePicker_From.Name = "DateTimePicker_From"
        Me.DateTimePicker_From.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DateTimePicker_From.Size = New System.Drawing.Size(92, 26)
        Me.DateTimePicker_From.TabIndex = 664
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("JF Flat", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(3, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label4.Size = New System.Drawing.Size(127, 23)
        Me.Label4.TabIndex = 665
        Me.Label4.Text = "تاريخ الصلاحية قبل"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 10.75!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(2, 607)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(93, 44)
        Me.ExitFormButton.TabIndex = 666
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'DGV_Control_btn
        '
        Me.DGV_Control_btn.BackColor = System.Drawing.Color.White
        Me.DGV_Control_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DGV_Control_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DGV_Control_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.DGV_Control_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DGV_Control_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.DGV_Control_btn.Font = New System.Drawing.Font("JF Flat", 10.75!, System.Drawing.FontStyle.Bold)
        Me.DGV_Control_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DGV_Control_btn.Image = Global.resturant.My.Resources.Resources.iconfinder_menu_1814109
        Me.DGV_Control_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DGV_Control_btn.Location = New System.Drawing.Point(99, 607)
        Me.DGV_Control_btn.Name = "DGV_Control_btn"
        Me.DGV_Control_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DGV_Control_btn.Size = New System.Drawing.Size(83, 44)
        Me.DGV_Control_btn.TabIndex = 676
        Me.DGV_Control_btn.TabStop = False
        Me.DGV_Control_btn.Text = "عرض"
        Me.DGV_Control_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DGV_Control_btn.UseVisualStyleBackColor = False
        '
        'ST_Name_CL
        '
        Me.ST_Name_CL.DataPropertyName = "ST_Name"
        Me.ST_Name_CL.HeaderText = "المخزن"
        Me.ST_Name_CL.Name = "ST_Name_CL"
        Me.ST_Name_CL.ReadOnly = True
        '
        'item_name_CL
        '
        Me.item_name_CL.DataPropertyName = "item_name"
        Me.item_name_CL.FillWeight = 119.5432!
        Me.item_name_CL.HeaderText = "الصنف"
        Me.item_name_CL.MinimumWidth = 100
        Me.item_name_CL.Name = "item_name_CL"
        Me.item_name_CL.ReadOnly = True
        Me.item_name_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.item_name_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'IM_Num_CL
        '
        Me.IM_Num_CL.DataPropertyName = "IM_Num"
        Me.IM_Num_CL.HeaderText = "الرقم"
        Me.IM_Num_CL.Name = "IM_Num_CL"
        Me.IM_Num_CL.ReadOnly = True
        '
        'Unit_CL
        '
        Me.Unit_CL.DataPropertyName = "U_Name"
        Me.Unit_CL.FillWeight = 60.9137!
        Me.Unit_CL.HeaderText = "الوحدة"
        Me.Unit_CL.Name = "Unit_CL"
        Me.Unit_CL.ReadOnly = True
        '
        'D_Valid_CL
        '
        Me.D_Valid_CL.DataPropertyName = "D_Vaild"
        DataGridViewCellStyle3.Format = "d"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.D_Valid_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.D_Valid_CL.HeaderText = "الصلاحية"
        Me.D_Valid_CL.Name = "D_Valid_CL"
        Me.D_Valid_CL.ReadOnly = True
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        DataGridViewCellStyle4.Format = "N2"
        Me.QTY_CL.DefaultCellStyle = DataGridViewCellStyle4
        Me.QTY_CL.FillWeight = 119.5432!
        Me.QTY_CL.HeaderText = "الكمية"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'Cost_CL
        '
        Me.Cost_CL.DataPropertyName = "Cost"
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.Cost_CL.DefaultCellStyle = DataGridViewCellStyle5
        Me.Cost_CL.HeaderText = "التكلفــة"
        Me.Cost_CL.Name = "Cost_CL"
        Me.Cost_CL.ReadOnly = True
        '
        'Case_CL
        '
        Me.Case_CL.HeaderText = "الحالة"
        Me.Case_CL.Name = "Case_CL"
        Me.Case_CL.ReadOnly = True
        '
        'IM_Valid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(771, 652)
        Me.Controls.Add(Me.DGV_Control_btn)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DateTimePicker_From)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TotalCost_txt)
        Me.Controls.Add(Me.DataGridViewX)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TotalQYT_txt)
        Me.Controls.Add(Me.PrintButton)
        Me.Controls.Add(Me.CMSearchTextBox)
        Me.Font = New System.Drawing.Font("JF Flat", 9.5!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.Name = "IM_Valid"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "صلاحية الأصناف"
        CType(Me.DataGridViewX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TotalCost_txt As System.Windows.Forms.TextBox
    Public WithEvents DataGridViewX As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TotalQYT_txt As System.Windows.Forms.TextBox
    Friend WithEvents PrintButton As System.Windows.Forms.Button
    Friend WithEvents CMSearchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker_From As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents DGV_Control_btn As System.Windows.Forms.Button
    Friend WithEvents ST_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_Num_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents D_Valid_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cost_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Case_CL As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
