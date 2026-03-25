<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IM_Report
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IM_Report))
        Me.IM_GV = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.IM_Print_Typecmb = New System.Windows.Forms.ComboBox()
        Me.IM_Serach_btn = New System.Windows.Forms.Button()
        Me.ButtPrintItems = New System.Windows.Forms.Button()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GM_Serach = New System.Windows.Forms.ComboBox()
        Me.GM_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_NUM_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Barcode_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_U_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cost_CL2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Min_SP_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.IM_GV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IM_GV
        '
        Me.IM_GV.AllowUserToAddRows = False
        Me.IM_GV.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray
        Me.IM_GV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.IM_GV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.IM_GV.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IM_GV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.IM_GV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.IM_GV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GM_Name_CL, Me.IM_NUM_CL, Me.Barcode_CL, Me.Column2, Me.IM_U_ID_CL, Me.Cost_CL2, Me.Column4, Me.Min_SP_CL})
        Me.IM_GV.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IM_GV.DefaultCellStyle = DataGridViewCellStyle3
        Me.IM_GV.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.IM_GV.Location = New System.Drawing.Point(1, 45)
        Me.IM_GV.Name = "IM_GV"
        Me.IM_GV.ReadOnly = True
        Me.IM_GV.RowHeadersVisible = False
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_GV.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.IM_GV.RowTemplate.Height = 35
        Me.IM_GV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.IM_GV.Size = New System.Drawing.Size(892, 611)
        Me.IM_GV.TabIndex = 7
        '
        'IM_Print_Typecmb
        '
        Me.IM_Print_Typecmb.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IM_Print_Typecmb.BackColor = System.Drawing.Color.White
        Me.IM_Print_Typecmb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_Print_Typecmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.IM_Print_Typecmb.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.IM_Print_Typecmb.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Print_Typecmb.ForeColor = System.Drawing.Color.Firebrick
        Me.IM_Print_Typecmb.FormattingEnabled = True
        Me.IM_Print_Typecmb.Items.AddRange(New Object() {"كبيرة (A4)", "صغيرة(إيصالات )"})
        Me.IM_Print_Typecmb.Location = New System.Drawing.Point(363, 5)
        Me.IM_Print_Typecmb.MaxDropDownItems = 15
        Me.IM_Print_Typecmb.Name = "IM_Print_Typecmb"
        Me.IM_Print_Typecmb.Size = New System.Drawing.Size(108, 25)
        Me.IM_Print_Typecmb.TabIndex = 664
        '
        'IM_Serach_btn
        '
        Me.IM_Serach_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IM_Serach_btn.BackColor = System.Drawing.Color.White
        Me.IM_Serach_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.IM_Serach_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IM_Serach_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IM_Serach_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_Serach_btn.ForeColor = System.Drawing.Color.Black
        Me.IM_Serach_btn.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.IM_Serach_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IM_Serach_btn.Location = New System.Drawing.Point(3, 1)
        Me.IM_Serach_btn.Name = "IM_Serach_btn"
        Me.IM_Serach_btn.Size = New System.Drawing.Size(133, 38)
        Me.IM_Serach_btn.TabIndex = 663
        Me.IM_Serach_btn.Text = "بحث"
        Me.IM_Serach_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.IM_Serach_btn.UseVisualStyleBackColor = False
        '
        'ButtPrintItems
        '
        Me.ButtPrintItems.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtPrintItems.BackColor = System.Drawing.Color.White
        Me.ButtPrintItems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ButtPrintItems.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ButtPrintItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtPrintItems.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ButtPrintItems.ForeColor = System.Drawing.Color.Black
        Me.ButtPrintItems.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.ButtPrintItems.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtPrintItems.Location = New System.Drawing.Point(139, 1)
        Me.ButtPrintItems.Name = "ButtPrintItems"
        Me.ButtPrintItems.Size = New System.Drawing.Size(133, 38)
        Me.ButtPrintItems.TabIndex = 662
        Me.ButtPrintItems.Text = "طباعة"
        Me.ButtPrintItems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtPrintItems.UseVisualStyleBackColor = False
        '
        'Label59
        '
        Me.Label59.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label59.AutoSize = True
        Me.Label59.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label59.Location = New System.Drawing.Point(275, 6)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(78, 19)
        Me.Label59.TabIndex = 666
        Me.Label59.Text = "نوع الطباعة"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(478, 7)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 19)
        Me.Label2.TabIndex = 667
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
        Me.GM_Serach.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.GM_Serach.FormattingEnabled = True
        Me.GM_Serach.IntegralHeight = False
        Me.GM_Serach.Items.AddRange(New Object() {"قصيرة", "طويلة"})
        Me.GM_Serach.Location = New System.Drawing.Point(543, 6)
        Me.GM_Serach.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GM_Serach.Name = "GM_Serach"
        Me.GM_Serach.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GM_Serach.Size = New System.Drawing.Size(218, 25)
        Me.GM_Serach.TabIndex = 668
        '
        'GM_Name_CL
        '
        Me.GM_Name_CL.DataPropertyName = "GM_Name"
        Me.GM_Name_CL.HeaderText = "التصنيف"
        Me.GM_Name_CL.Name = "GM_Name_CL"
        Me.GM_Name_CL.ReadOnly = True
        '
        'IM_NUM_CL
        '
        Me.IM_NUM_CL.DataPropertyName = "IM_NUM"
        Me.IM_NUM_CL.HeaderText = "الرقم"
        Me.IM_NUM_CL.Name = "IM_NUM_CL"
        Me.IM_NUM_CL.ReadOnly = True
        '
        'Barcode_CL
        '
        Me.Barcode_CL.DataPropertyName = "Barcode"
        Me.Barcode_CL.HeaderText = "الباركود"
        Me.Barcode_CL.Name = "Barcode_CL"
        Me.Barcode_CL.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "item_name"
        Me.Column2.FillWeight = 150.0!
        Me.Column2.HeaderText = "الصنــف"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'IM_U_ID_CL
        '
        Me.IM_U_ID_CL.DataPropertyName = "U_Name"
        Me.IM_U_ID_CL.HeaderText = "الوحدة"
        Me.IM_U_ID_CL.Name = "IM_U_ID_CL"
        Me.IM_U_ID_CL.ReadOnly = True
        '
        'Cost_CL2
        '
        Me.Cost_CL2.DataPropertyName = "Cost"
        Me.Cost_CL2.HeaderText = "التكلفة"
        Me.Cost_CL2.Name = "Cost_CL2"
        Me.Cost_CL2.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.DataPropertyName = "Price"
        Me.Column4.FillWeight = 75.0!
        Me.Column4.HeaderText = "البيع"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Min_SP_CL
        '
        Me.Min_SP_CL.DataPropertyName = "Min_SP"
        Me.Min_SP_CL.HeaderText = "Min_SP"
        Me.Min_SP_CL.Name = "Min_SP_CL"
        Me.Min_SP_CL.ReadOnly = True
        Me.Min_SP_CL.Visible = False
        '
        'IM_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(895, 659)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GM_Serach)
        Me.Controls.Add(Me.IM_GV)
        Me.Controls.Add(Me.Label59)
        Me.Controls.Add(Me.IM_Print_Typecmb)
        Me.Controls.Add(Me.IM_Serach_btn)
        Me.Controls.Add(Me.ButtPrintItems)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "IM_Report"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.IM_GV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents IM_GV As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents IM_Print_Typecmb As System.Windows.Forms.ComboBox
    Friend WithEvents IM_Serach_btn As System.Windows.Forms.Button
    Friend WithEvents ButtPrintItems As System.Windows.Forms.Button
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GM_Serach As System.Windows.Forms.ComboBox
    Friend WithEvents GM_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_NUM_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Barcode_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_U_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cost_CL2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Min_SP_CL As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
