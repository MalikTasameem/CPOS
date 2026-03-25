<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Pch_Report
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Pch_Report))
        Me.Label75 = New System.Windows.Forms.Label()
        Me.Pch_ST_cm = New System.Windows.Forms.ComboBox()
        Me.Exp_DV = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.Date_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pch_St_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pch_U_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.S_QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.S_T_Price_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Exp_Search_btn = New System.Windows.Forms.Button()
        Me.Exp_print_btn = New System.Windows.Forms.Button()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Exp_T_Q_txt = New System.Windows.Forms.TextBox()
        Me.Exp_T_M_txt = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        CType(Me.Exp_DV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label75
        '
        Me.Label75.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label75.AutoSize = True
        Me.Label75.Font = New System.Drawing.Font("Segoe UI Semibold", 11.75!, System.Drawing.FontStyle.Bold)
        Me.Label75.Location = New System.Drawing.Point(249, 9)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(55, 21)
        Me.Label75.TabIndex = 665
        Me.Label75.Text = "المخزن"
        Me.Label75.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pch_ST_cm
        '
        Me.Pch_ST_cm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pch_ST_cm.BackColor = System.Drawing.SystemColors.Info
        Me.Pch_ST_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Pch_ST_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Pch_ST_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Pch_ST_cm.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Pch_ST_cm.FormattingEnabled = True
        Me.Pch_ST_cm.Location = New System.Drawing.Point(309, 5)
        Me.Pch_ST_cm.Name = "Pch_ST_cm"
        Me.Pch_ST_cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Pch_ST_cm.Size = New System.Drawing.Size(227, 29)
        Me.Pch_ST_cm.TabIndex = 664
        '
        'Exp_DV
        '
        Me.Exp_DV.AllowUserToAddRows = False
        Me.Exp_DV.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro
        Me.Exp_DV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.Exp_DV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Exp_DV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.Exp_DV.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Exp_DV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.Exp_DV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Exp_DV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Date_CL, Me.Pch_St_Name_CL, Me.DataGridViewTextBoxColumn6, Me.Pch_U_Name_CL, Me.S_QTY_CL, Me.S_T_Price_CL})
        Me.Exp_DV.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Exp_DV.DefaultCellStyle = DataGridViewCellStyle5
        Me.Exp_DV.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.Exp_DV.Location = New System.Drawing.Point(2, 39)
        Me.Exp_DV.Name = "Exp_DV"
        Me.Exp_DV.ReadOnly = True
        Me.Exp_DV.RowHeadersVisible = False
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Exp_DV.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.Exp_DV.RowTemplate.Height = 32
        Me.Exp_DV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Exp_DV.Size = New System.Drawing.Size(889, 580)
        Me.Exp_DV.TabIndex = 661
        '
        'Date_CL
        '
        Me.Date_CL.DataPropertyName = "Date"
        Me.Date_CL.FillWeight = 20.0!
        Me.Date_CL.HeaderText = "التاريخ"
        Me.Date_CL.Name = "Date_CL"
        Me.Date_CL.ReadOnly = True
        Me.Date_CL.Visible = False
        '
        'Pch_St_Name_CL
        '
        Me.Pch_St_Name_CL.DataPropertyName = "St_Name"
        Me.Pch_St_Name_CL.FillWeight = 200.0!
        Me.Pch_St_Name_CL.HeaderText = "المخزن"
        Me.Pch_St_Name_CL.Name = "Pch_St_Name_CL"
        Me.Pch_St_Name_CL.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Item_Name"
        Me.DataGridViewTextBoxColumn6.FillWeight = 400.0!
        Me.DataGridViewTextBoxColumn6.HeaderText = "الصنف"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'Pch_U_Name_CL
        '
        Me.Pch_U_Name_CL.DataPropertyName = "U_Name"
        Me.Pch_U_Name_CL.FillWeight = 200.0!
        Me.Pch_U_Name_CL.HeaderText = "الوحدة"
        Me.Pch_U_Name_CL.Name = "Pch_U_Name_CL"
        Me.Pch_U_Name_CL.ReadOnly = True
        '
        'S_QTY_CL
        '
        Me.S_QTY_CL.DataPropertyName = "S_QTY"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.Format = "N2"
        Me.S_QTY_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.S_QTY_CL.FillWeight = 200.0!
        Me.S_QTY_CL.HeaderText = "الكمية"
        Me.S_QTY_CL.Name = "S_QTY_CL"
        Me.S_QTY_CL.ReadOnly = True
        '
        'S_T_Price_CL
        '
        Me.S_T_Price_CL.DataPropertyName = "S_T_Price"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.S_T_Price_CL.DefaultCellStyle = DataGridViewCellStyle4
        Me.S_T_Price_CL.FillWeight = 200.0!
        Me.S_T_Price_CL.HeaderText = "الإجمالي"
        Me.S_T_Price_CL.Name = "S_T_Price_CL"
        Me.S_T_Price_CL.ReadOnly = True
        '
        'Exp_Search_btn
        '
        Me.Exp_Search_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Exp_Search_btn.BackColor = System.Drawing.Color.White
        Me.Exp_Search_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Exp_Search_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Exp_Search_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Exp_Search_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Exp_Search_btn.ForeColor = System.Drawing.Color.Black
        Me.Exp_Search_btn.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.Exp_Search_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Exp_Search_btn.Location = New System.Drawing.Point(2, 1)
        Me.Exp_Search_btn.Name = "Exp_Search_btn"
        Me.Exp_Search_btn.Size = New System.Drawing.Size(120, 38)
        Me.Exp_Search_btn.TabIndex = 663
        Me.Exp_Search_btn.Text = "بحث"
        Me.Exp_Search_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Exp_Search_btn.UseVisualStyleBackColor = False
        '
        'Exp_print_btn
        '
        Me.Exp_print_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Exp_print_btn.BackColor = System.Drawing.Color.White
        Me.Exp_print_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Exp_print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Exp_print_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Exp_print_btn.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.Exp_print_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Exp_print_btn.Location = New System.Drawing.Point(125, 1)
        Me.Exp_print_btn.Name = "Exp_print_btn"
        Me.Exp_print_btn.Size = New System.Drawing.Size(120, 38)
        Me.Exp_print_btn.TabIndex = 662
        Me.Exp_print_btn.Text = "طباعة"
        Me.Exp_print_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Exp_print_btn.UseVisualStyleBackColor = False
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Exp_T_Q_txt)
        Me.Panel6.Controls.Add(Me.Exp_T_M_txt)
        Me.Panel6.Controls.Add(Me.Label30)
        Me.Panel6.Controls.Add(Me.Label31)
        Me.Panel6.Location = New System.Drawing.Point(2, 624)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(891, 33)
        Me.Panel6.TabIndex = 666
        '
        'Exp_T_Q_txt
        '
        Me.Exp_T_Q_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Exp_T_Q_txt.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Exp_T_Q_txt.ForeColor = System.Drawing.Color.Red
        Me.Exp_T_Q_txt.Location = New System.Drawing.Point(433, 1)
        Me.Exp_T_Q_txt.Name = "Exp_T_Q_txt"
        Me.Exp_T_Q_txt.ReadOnly = True
        Me.Exp_T_Q_txt.Size = New System.Drawing.Size(126, 30)
        Me.Exp_T_Q_txt.TabIndex = 380
        Me.Exp_T_Q_txt.Text = "0.00"
        Me.Exp_T_Q_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Exp_T_M_txt
        '
        Me.Exp_T_M_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Exp_T_M_txt.Font = New System.Drawing.Font("Stencil", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Exp_T_M_txt.ForeColor = System.Drawing.Color.Red
        Me.Exp_T_M_txt.Location = New System.Drawing.Point(153, 1)
        Me.Exp_T_M_txt.Name = "Exp_T_M_txt"
        Me.Exp_T_M_txt.ReadOnly = True
        Me.Exp_T_M_txt.Size = New System.Drawing.Size(126, 30)
        Me.Exp_T_M_txt.TabIndex = 381
        Me.Exp_T_M_txt.Text = "0.00"
        Me.Exp_T_M_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label30
        '
        Me.Label30.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label30.Location = New System.Drawing.Point(563, 6)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(108, 21)
        Me.Label30.TabIndex = 383
        Me.Label30.Text = "إجمالي الكميات"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label31
        '
        Me.Label31.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label31.Location = New System.Drawing.Point(283, 6)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(97, 21)
        Me.Label31.TabIndex = 384
        Me.Label31.Text = "إجمالي القيمة"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pch_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(895, 659)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Label75)
        Me.Controls.Add(Me.Pch_ST_cm)
        Me.Controls.Add(Me.Exp_DV)
        Me.Controls.Add(Me.Exp_Search_btn)
        Me.Controls.Add(Me.Exp_print_btn)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
         Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Pch_Report"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.Exp_DV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents Pch_ST_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Exp_DV As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents Date_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pch_St_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pch_U_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents S_T_Price_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Exp_Search_btn As System.Windows.Forms.Button
    Friend WithEvents Exp_print_btn As System.Windows.Forms.Button
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Exp_T_Q_txt As System.Windows.Forms.TextBox
    Friend WithEvents Exp_T_M_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
End Class
