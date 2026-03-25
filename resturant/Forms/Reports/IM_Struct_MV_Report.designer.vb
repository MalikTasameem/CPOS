<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IM_Struct_MV_Report
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IM_Struct_MV_Report))
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.InSale_T_Q_txt = New System.Windows.Forms.TextBox()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.InSale_ST_cm = New System.Windows.Forms.ComboBox()
        Me.InSale_DV = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.InSale_Search_btn = New System.Windows.Forms.Button()
        Me.InSale_print_btn = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn22 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn31 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InSale_S_QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel10.SuspendLayout()
        CType(Me.InSale_DV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.InSale_T_Q_txt)
        Me.Panel10.Controls.Add(Me.Label84)
        Me.Panel10.Location = New System.Drawing.Point(0, 622)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(894, 35)
        Me.Panel10.TabIndex = 679
        '
        'InSale_T_Q_txt
        '
        Me.InSale_T_Q_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.InSale_T_Q_txt.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InSale_T_Q_txt.ForeColor = System.Drawing.Color.Red
        Me.InSale_T_Q_txt.Location = New System.Drawing.Point(259, 2)
        Me.InSale_T_Q_txt.Name = "InSale_T_Q_txt"
        Me.InSale_T_Q_txt.ReadOnly = True
        Me.InSale_T_Q_txt.Size = New System.Drawing.Size(126, 30)
        Me.InSale_T_Q_txt.TabIndex = 667
        Me.InSale_T_Q_txt.Text = "0.00"
        Me.InSale_T_Q_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label84
        '
        Me.Label84.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label84.AutoSize = True
        Me.Label84.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label84.Location = New System.Drawing.Point(390, 5)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(108, 21)
        Me.Label84.TabIndex = 669
        Me.Label84.Text = "إجمالي الكميات"
        Me.Label84.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 11.75!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(576, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 21)
        Me.Label2.TabIndex = 678
        Me.Label2.Text = "المخزن"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'InSale_ST_cm
        '
        Me.InSale_ST_cm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InSale_ST_cm.BackColor = System.Drawing.SystemColors.Info
        Me.InSale_ST_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.InSale_ST_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.InSale_ST_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.InSale_ST_cm.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.InSale_ST_cm.FormattingEnabled = True
        Me.InSale_ST_cm.Location = New System.Drawing.Point(344, 7)
        Me.InSale_ST_cm.Name = "InSale_ST_cm"
        Me.InSale_ST_cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.InSale_ST_cm.Size = New System.Drawing.Size(227, 29)
        Me.InSale_ST_cm.TabIndex = 677
        '
        'InSale_DV
        '
        Me.InSale_DV.AllowUserToAddRows = False
        Me.InSale_DV.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro
        Me.InSale_DV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.InSale_DV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.InSale_DV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.InSale_DV.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.InSale_DV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.InSale_DV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.InSale_DV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn16, Me.DataGridViewTextBoxColumn22, Me.DataGridViewTextBoxColumn31, Me.InSale_S_QTY_CL})
        Me.InSale_DV.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.InSale_DV.DefaultCellStyle = DataGridViewCellStyle4
        Me.InSale_DV.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.InSale_DV.Location = New System.Drawing.Point(0, 42)
        Me.InSale_DV.Name = "InSale_DV"
        Me.InSale_DV.ReadOnly = True
        Me.InSale_DV.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.InSale_DV.RowHeadersVisible = False
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.InSale_DV.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.InSale_DV.RowTemplate.Height = 32
        Me.InSale_DV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.InSale_DV.Size = New System.Drawing.Size(894, 577)
        Me.InSale_DV.TabIndex = 674
        '
        'InSale_Search_btn
        '
        Me.InSale_Search_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InSale_Search_btn.BackColor = System.Drawing.Color.White
        Me.InSale_Search_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.InSale_Search_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.InSale_Search_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.InSale_Search_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InSale_Search_btn.ForeColor = System.Drawing.Color.Black
        Me.InSale_Search_btn.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.InSale_Search_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.InSale_Search_btn.Location = New System.Drawing.Point(774, 2)
        Me.InSale_Search_btn.Name = "InSale_Search_btn"
        Me.InSale_Search_btn.Size = New System.Drawing.Size(120, 38)
        Me.InSale_Search_btn.TabIndex = 676
        Me.InSale_Search_btn.Text = "بحث"
        Me.InSale_Search_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.InSale_Search_btn.UseVisualStyleBackColor = False
        '
        'InSale_print_btn
        '
        Me.InSale_print_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InSale_print_btn.BackColor = System.Drawing.Color.White
        Me.InSale_print_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.InSale_print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.InSale_print_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InSale_print_btn.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.InSale_print_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.InSale_print_btn.Location = New System.Drawing.Point(652, 2)
        Me.InSale_print_btn.Name = "InSale_print_btn"
        Me.InSale_print_btn.Size = New System.Drawing.Size(120, 38)
        Me.InSale_print_btn.TabIndex = 675
        Me.InSale_print_btn.Text = "طباعة"
        Me.InSale_print_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.InSale_print_btn.UseVisualStyleBackColor = False
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "St_Name"
        Me.DataGridViewTextBoxColumn16.FillWeight = 150.0!
        Me.DataGridViewTextBoxColumn16.HeaderText = "المخزن"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        '
        'DataGridViewTextBoxColumn22
        '
        Me.DataGridViewTextBoxColumn22.DataPropertyName = "Item_Name"
        Me.DataGridViewTextBoxColumn22.FillWeight = 300.0!
        Me.DataGridViewTextBoxColumn22.HeaderText = "الصنف"
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        Me.DataGridViewTextBoxColumn22.ReadOnly = True
        '
        'DataGridViewTextBoxColumn31
        '
        Me.DataGridViewTextBoxColumn31.DataPropertyName = "U_Name"
        Me.DataGridViewTextBoxColumn31.FillWeight = 150.0!
        Me.DataGridViewTextBoxColumn31.HeaderText = "الوحدة"
        Me.DataGridViewTextBoxColumn31.Name = "DataGridViewTextBoxColumn31"
        Me.DataGridViewTextBoxColumn31.ReadOnly = True
        '
        'InSale_S_QTY_CL
        '
        Me.InSale_S_QTY_CL.DataPropertyName = "S_QTY"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InSale_S_QTY_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.InSale_S_QTY_CL.FillWeight = 150.0!
        Me.InSale_S_QTY_CL.HeaderText = "الكمية"
        Me.InSale_S_QTY_CL.Name = "InSale_S_QTY_CL"
        Me.InSale_S_QTY_CL.ReadOnly = True
        '
        'IM_Struct_MV_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(895, 659)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel10)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.InSale_ST_cm)
        Me.Controls.Add(Me.InSale_DV)
        Me.Controls.Add(Me.InSale_Search_btn)
        Me.Controls.Add(Me.InSale_print_btn)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "IM_Struct_MV_Report"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        CType(Me.InSale_DV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents InSale_T_Q_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents InSale_ST_cm As System.Windows.Forms.ComboBox
    Friend WithEvents InSale_DV As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents InSale_Search_btn As System.Windows.Forms.Button
    Friend WithEvents InSale_print_btn As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn31 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InSale_S_QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
