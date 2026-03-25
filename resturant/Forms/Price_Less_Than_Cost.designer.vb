<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Price_Less_Than_Cost
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Price_Less_Than_Cost))
        Me.DataGridViewX = New DevComponents.DotNetBar.Controls.DataGridViewX()
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
        Me.CMSearchTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DGV_Control_btn = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.DataGridViewX, System.ComponentModel.ISupportInitialize).BeginInit()
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
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("JF Flat", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewX.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.item_id_CL, Me.IM_Num_CL, Me.item_name_CL, Me.Unit_CL, Me.Barcode_CL, Me.QTY_CL, Me.Cost_CL, Me.Price_CL, Me.Price_2_CL, Me.Price_3_CL})
        Me.DataGridViewX.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("JF Flat", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewX.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewX.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DataGridViewX.Location = New System.Drawing.Point(3, 84)
        Me.DataGridViewX.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
        Me.DataGridViewX.MultiSelect = False
        Me.DataGridViewX.Name = "DataGridViewX"
        Me.DataGridViewX.ReadOnly = True
        Me.DataGridViewX.RowHeadersVisible = False
        DataGridViewCellStyle5.Font = New System.Drawing.Font("JF Flat", 11.25!)
        Me.DataGridViewX.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewX.RowTemplate.Height = 35
        Me.DataGridViewX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewX.Size = New System.Drawing.Size(1000, 563)
        Me.DataGridViewX.TabIndex = 2
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "ت"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        '
        'item_id_CL
        '
        Me.item_id_CL.DataPropertyName = "IM_ID"
        Me.item_id_CL.HeaderText = "IM_ID"
        Me.item_id_CL.Name = "item_id_CL"
        Me.item_id_CL.ReadOnly = True
        Me.item_id_CL.Visible = False
        '
        'IM_Num_CL
        '
        Me.IM_Num_CL.DataPropertyName = "IM_Num"
        Me.IM_Num_CL.HeaderText = "رقم الصنف"
        Me.IM_Num_CL.Name = "IM_Num_CL"
        Me.IM_Num_CL.ReadOnly = True
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
        'Unit_CL
        '
        Me.Unit_CL.DataPropertyName = "U_Name"
        Me.Unit_CL.FillWeight = 60.9137!
        Me.Unit_CL.HeaderText = "الوحدة"
        Me.Unit_CL.Name = "Unit_CL"
        Me.Unit_CL.ReadOnly = True
        '
        'Barcode_CL
        '
        Me.Barcode_CL.DataPropertyName = "Barcode"
        Me.Barcode_CL.HeaderText = "باركود"
        Me.Barcode_CL.Name = "Barcode_CL"
        Me.Barcode_CL.ReadOnly = True
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Green
        DataGridViewCellStyle3.Format = "N2"
        Me.QTY_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.QTY_CL.FillWeight = 119.5432!
        Me.QTY_CL.HeaderText = "الكمية"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'Cost_CL
        '
        Me.Cost_CL.DataPropertyName = "Cost"
        Me.Cost_CL.HeaderText = "التكلفة"
        Me.Cost_CL.Name = "Cost_CL"
        Me.Cost_CL.ReadOnly = True
        '
        'Price_CL
        '
        Me.Price_CL.DataPropertyName = "Price"
        Me.Price_CL.HeaderText = "البيع"
        Me.Price_CL.Name = "Price_CL"
        Me.Price_CL.ReadOnly = True
        '
        'Price_2_CL
        '
        Me.Price_2_CL.DataPropertyName = "Min_SP"
        Me.Price_2_CL.HeaderText = "بيع الجملة"
        Me.Price_2_CL.Name = "Price_2_CL"
        Me.Price_2_CL.ReadOnly = True
        '
        'Price_3_CL
        '
        Me.Price_3_CL.DataPropertyName = "Min_SP_2"
        Me.Price_3_CL.HeaderText = "جملة الجملة"
        Me.Price_3_CL.Name = "Price_3_CL"
        Me.Price_3_CL.ReadOnly = True
        '
        'CMSearchTextBox
        '
        Me.CMSearchTextBox.Font = New System.Drawing.Font("JF Flat", 11.25!)
        Me.CMSearchTextBox.ForeColor = System.Drawing.SystemColors.InfoText
        Me.CMSearchTextBox.Location = New System.Drawing.Point(3, 48)
        Me.CMSearchTextBox.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
        Me.CMSearchTextBox.Name = "CMSearchTextBox"
        Me.CMSearchTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CMSearchTextBox.Size = New System.Drawing.Size(1000, 34)
        Me.CMSearchTextBox.TabIndex = 272
        Me.CMSearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("JF Flat", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(2, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label3.Size = New System.Drawing.Size(1001, 44)
        Me.Label3.TabIndex = 685
        Me.Label3.Text = "كشف لوائــح أسعار البيع التي أقل من التكلفة"
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
        Me.DGV_Control_btn.Font = New System.Drawing.Font("JF Flat", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGV_Control_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.DGV_Control_btn.Image = Global.resturant.My.Resources.Resources.iconfinder_menu_1814109
        Me.DGV_Control_btn.Location = New System.Drawing.Point(2, 648)
        Me.DGV_Control_btn.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.DGV_Control_btn.Name = "DGV_Control_btn"
        Me.DGV_Control_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.DGV_Control_btn.Size = New System.Drawing.Size(70, 40)
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
        Me.ExitFormButton.Location = New System.Drawing.Point(888, 648)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(115, 40)
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
        Me.Button1.Font = New System.Drawing.Font("JF Flat", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Image = Global.resturant.My.Resources.Resources.if_ok_173061
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.Location = New System.Drawing.Point(73, 648)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Button1.Size = New System.Drawing.Size(106, 40)
        Me.Button1.TabIndex = 390
        Me.Button1.Text = "تحديث القائمة"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Price_Less_Than_Cost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 23.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 688)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DGV_Control_btn)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridViewX)
        Me.Controls.Add(Me.CMSearchTextBox)
        Me.Font = New System.Drawing.Font("JF Flat", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
        Me.Name = "Price_Less_Than_Cost"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "لوائح الأسعار"
        CType(Me.DataGridViewX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents DataGridViewX As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents CMSearchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents DGV_Control_btn As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
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
End Class
