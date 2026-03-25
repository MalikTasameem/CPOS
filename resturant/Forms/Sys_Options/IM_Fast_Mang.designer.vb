<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IM_Fast_Mang
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IM_Fast_Mang))
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.MetroGrid = New System.Windows.Forms.DataGridView()
        Me.IM_SH_txt = New System.Windows.Forms.TextBox()
        Me.IM_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GM_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GM_CL = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.IM_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_NUM_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.isActive_CL = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ADD_CL = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.REMOVE_CL = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GM_Serach = New System.Windows.Forms.ComboBox()
        CType(Me.MetroGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ExitFormButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 11.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(893, 664)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ExitFormButton.Size = New System.Drawing.Size(110, 31)
        Me.ExitFormButton.TabIndex = 672
        Me.ExitFormButton.TabStop = False
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'MetroGrid
        '
        Me.MetroGrid.AllowUserToDeleteRows = False
        Me.MetroGrid.AllowUserToResizeRows = False
        Me.MetroGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.MetroGrid.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("JF Flat", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MetroGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.MetroGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MetroGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IM_ID_CL, Me.GM_Name_CL, Me.GM_CL, Me.IM_Name_CL, Me.IM_NUM_CL, Me.Price_CL, Me.isActive_CL, Me.ADD_CL, Me.REMOVE_CL})
        Me.MetroGrid.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MetroGrid.Location = New System.Drawing.Point(1, 37)
        Me.MetroGrid.Margin = New System.Windows.Forms.Padding(2)
        Me.MetroGrid.MultiSelect = False
        Me.MetroGrid.Name = "MetroGrid"
        Me.MetroGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.MetroGrid.RowHeadersVisible = False
        DataGridViewCellStyle3.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MetroGrid.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.MetroGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.MetroGrid.RowTemplate.Height = 35
        Me.MetroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.MetroGrid.Size = New System.Drawing.Size(1002, 623)
        Me.MetroGrid.TabIndex = 706
        '
        'IM_SH_txt
        '
        Me.IM_SH_txt.BackColor = System.Drawing.SystemColors.Window
        Me.IM_SH_txt.Font = New System.Drawing.Font("JF Flat", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_SH_txt.Location = New System.Drawing.Point(358, 1)
        Me.IM_SH_txt.Margin = New System.Windows.Forms.Padding(5)
        Me.IM_SH_txt.Name = "IM_SH_txt"
        Me.IM_SH_txt.Size = New System.Drawing.Size(645, 34)
        Me.IM_SH_txt.TabIndex = 707
        '
        'IM_ID_CL
        '
        Me.IM_ID_CL.DataPropertyName = "IM_ID"
        Me.IM_ID_CL.HeaderText = "IM_ID"
        Me.IM_ID_CL.Name = "IM_ID_CL"
        Me.IM_ID_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.IM_ID_CL.Visible = False
        '
        'GM_Name_CL
        '
        Me.GM_Name_CL.DataPropertyName = "GM_Name"
        Me.GM_Name_CL.HeaderText = "GM_Name"
        Me.GM_Name_CL.Name = "GM_Name_CL"
        Me.GM_Name_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GM_Name_CL.Visible = False
        '
        'GM_CL
        '
        Me.GM_CL.DataPropertyName = "GM_ID"
        Me.GM_CL.FillWeight = 250.0!
        Me.GM_CL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GM_CL.HeaderText = "التصنيف"
        Me.GM_CL.Name = "GM_CL"
        Me.GM_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'IM_Name_CL
        '
        Me.IM_Name_CL.DataPropertyName = "item_name"
        Me.IM_Name_CL.FillWeight = 400.0!
        Me.IM_Name_CL.HeaderText = "الصنف"
        Me.IM_Name_CL.Name = "IM_Name_CL"
        Me.IM_Name_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'IM_NUM_CL
        '
        Me.IM_NUM_CL.DataPropertyName = "IM_NUM"
        Me.IM_NUM_CL.HeaderText = "الرقم"
        Me.IM_NUM_CL.Name = "IM_NUM_CL"
        '
        'Price_CL
        '
        Me.Price_CL.DataPropertyName = "Price"
        DataGridViewCellStyle2.Format = "N2"
        Me.Price_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.Price_CL.HeaderText = "السعر"
        Me.Price_CL.Name = "Price_CL"
        Me.Price_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'isActive_CL
        '
        Me.isActive_CL.DataPropertyName = "isActive"
        Me.isActive_CL.FillWeight = 50.0!
        Me.isActive_CL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.isActive_CL.HeaderText = "عرض"
        Me.isActive_CL.Name = "isActive_CL"
        '
        'ADD_CL
        '
        Me.ADD_CL.FillWeight = 75.0!
        Me.ADD_CL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ADD_CL.HeaderText = "تطبيق"
        Me.ADD_CL.Name = "ADD_CL"
        Me.ADD_CL.Text = "تطبيق"
        Me.ADD_CL.UseColumnTextForButtonValue = True
        '
        'REMOVE_CL
        '
        Me.REMOVE_CL.FillWeight = 75.0!
        Me.REMOVE_CL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.REMOVE_CL.HeaderText = "حذف"
        Me.REMOVE_CL.Name = "REMOVE_CL"
        Me.REMOVE_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.REMOVE_CL.Text = "حذف"
        Me.REMOVE_CL.UseColumnTextForButtonValue = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("JF Flat", 11.25!)
        Me.Label2.Location = New System.Drawing.Point(290, 6)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 26)
        Me.Label2.TabIndex = 708
        Me.Label2.Text = "التصنيف"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GM_Serach
        '
        Me.GM_Serach.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.GM_Serach.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.GM_Serach.BackColor = System.Drawing.SystemColors.Info
        Me.GM_Serach.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GM_Serach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.GM_Serach.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GM_Serach.Font = New System.Drawing.Font("JF Flat", 11.0!)
        Me.GM_Serach.FormattingEnabled = True
        Me.GM_Serach.IntegralHeight = False
        Me.GM_Serach.Items.AddRange(New Object() {"قصيرة", "طويلة"})
        Me.GM_Serach.Location = New System.Drawing.Point(1, 2)
        Me.GM_Serach.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GM_Serach.Name = "GM_Serach"
        Me.GM_Serach.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GM_Serach.Size = New System.Drawing.Size(284, 34)
        Me.GM_Serach.TabIndex = 709
        '
        'IM_Fast_Mang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1003, 695)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GM_Serach)
        Me.Controls.Add(Me.IM_SH_txt)
        Me.Controls.Add(Me.MetroGrid)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("JF Flat", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.MinimizeBox = False
        Me.Name = "IM_Fast_Mang"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إدارة الأصناف"
        CType(Me.MetroGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents MetroGrid As System.Windows.Forms.DataGridView
    Friend WithEvents IM_SH_txt As System.Windows.Forms.TextBox
    Friend WithEvents IM_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GM_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GM_CL As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents IM_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_NUM_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents isActive_CL As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ADD_CL As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents REMOVE_CL As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GM_Serach As System.Windows.Forms.ComboBox
End Class
