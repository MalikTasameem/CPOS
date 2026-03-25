<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TABLES_MV_Report
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TABLES_MV_Report))
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.IMEx_T_M_txt = New System.Windows.Forms.TextBox()
        Me.IMEx_T_Q_txt = New System.Windows.Forms.TextBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.TABLES_cm = New System.Windows.Forms.ComboBox()
        Me.IMEx_DV = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.TB_ORDER_CODE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Date_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.T_Price_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.USER_NAME_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IMEx_Search_btn = New System.Windows.Forms.Button()
        Me.IMEx_print_btn = New System.Windows.Forms.Button()
        Me.Panel9.SuspendLayout()
        CType(Me.IMEx_DV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.IMEx_T_M_txt)
        Me.Panel9.Controls.Add(Me.IMEx_T_Q_txt)
        Me.Panel9.Controls.Add(Me.Label51)
        Me.Panel9.Controls.Add(Me.Label50)
        Me.Panel9.Location = New System.Drawing.Point(1, 622)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(891, 35)
        Me.Panel9.TabIndex = 668
        '
        'IMEx_T_M_txt
        '
        Me.IMEx_T_M_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.IMEx_T_M_txt.Font = New System.Drawing.Font("Stencil", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMEx_T_M_txt.ForeColor = System.Drawing.Color.Red
        Me.IMEx_T_M_txt.Location = New System.Drawing.Point(175, 3)
        Me.IMEx_T_M_txt.Name = "IMEx_T_M_txt"
        Me.IMEx_T_M_txt.ReadOnly = True
        Me.IMEx_T_M_txt.Size = New System.Drawing.Size(126, 30)
        Me.IMEx_T_M_txt.TabIndex = 394
        Me.IMEx_T_M_txt.Text = "0.00"
        Me.IMEx_T_M_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'IMEx_T_Q_txt
        '
        Me.IMEx_T_Q_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.IMEx_T_Q_txt.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMEx_T_Q_txt.ForeColor = System.Drawing.Color.Red
        Me.IMEx_T_Q_txt.Location = New System.Drawing.Point(459, 3)
        Me.IMEx_T_Q_txt.Name = "IMEx_T_Q_txt"
        Me.IMEx_T_Q_txt.ReadOnly = True
        Me.IMEx_T_Q_txt.Size = New System.Drawing.Size(126, 30)
        Me.IMEx_T_Q_txt.TabIndex = 393
        Me.IMEx_T_Q_txt.Text = "0.00"
        Me.IMEx_T_Q_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label51
        '
        Me.Label51.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label51.AutoSize = True
        Me.Label51.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label51.Location = New System.Drawing.Point(585, 8)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(108, 21)
        Me.Label51.TabIndex = 395
        Me.Label51.Text = "إجمالي الكميات"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label50
        '
        Me.Label50.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label50.AutoSize = True
        Me.Label50.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label50.Location = New System.Drawing.Point(307, 8)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(97, 21)
        Me.Label50.TabIndex = 396
        Me.Label50.Text = "إجمالي القيمة"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label72
        '
        Me.Label72.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label72.AutoSize = True
        Me.Label72.Font = New System.Drawing.Font("Segoe UI Semibold", 11.75!, System.Drawing.FontStyle.Bold)
        Me.Label72.Location = New System.Drawing.Point(245, 10)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(57, 21)
        Me.Label72.TabIndex = 667
        Me.Label72.Text = "الطاولة"
        Me.Label72.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TABLES_cm
        '
        Me.TABLES_cm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TABLES_cm.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.TABLES_cm.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.TABLES_cm.BackColor = System.Drawing.SystemColors.Info
        Me.TABLES_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TABLES_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.TABLES_cm.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.TABLES_cm.FormattingEnabled = True
        Me.TABLES_cm.Location = New System.Drawing.Point(305, 5)
        Me.TABLES_cm.Name = "TABLES_cm"
        Me.TABLES_cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TABLES_cm.Size = New System.Drawing.Size(227, 29)
        Me.TABLES_cm.TabIndex = 666
        '
        'IMEx_DV
        '
        Me.IMEx_DV.AllowUserToAddRows = False
        Me.IMEx_DV.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Empty
        Me.IMEx_DV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.IMEx_DV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.IMEx_DV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.IMEx_DV.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IMEx_DV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.IMEx_DV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.IMEx_DV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TB_ORDER_CODE_CL, Me.Date_CL, Me.DataGridViewTextBoxColumn12, Me.DataGridViewTextBoxColumn13, Me.QTY_CL, Me.T_Price_CL, Me.USER_NAME_CL})
        Me.IMEx_DV.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IMEx_DV.DefaultCellStyle = DataGridViewCellStyle5
        Me.IMEx_DV.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.IMEx_DV.Location = New System.Drawing.Point(1, 40)
        Me.IMEx_DV.Name = "IMEx_DV"
        Me.IMEx_DV.ReadOnly = True
        Me.IMEx_DV.RowHeadersVisible = False
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IMEx_DV.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.IMEx_DV.RowTemplate.Height = 32
        Me.IMEx_DV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.IMEx_DV.Size = New System.Drawing.Size(891, 576)
        Me.IMEx_DV.TabIndex = 663
        '
        'TB_ORDER_CODE_CL
        '
        Me.TB_ORDER_CODE_CL.DataPropertyName = "TB_ORDER_CODE"
        Me.TB_ORDER_CODE_CL.HeaderText = "الكود"
        Me.TB_ORDER_CODE_CL.Name = "TB_ORDER_CODE_CL"
        Me.TB_ORDER_CODE_CL.ReadOnly = True
        '
        'Date_CL
        '
        Me.Date_CL.DataPropertyName = "Date"
        Me.Date_CL.FillWeight = 150.0!
        Me.Date_CL.HeaderText = "التاريخ"
        Me.Date_CL.Name = "Date_CL"
        Me.Date_CL.ReadOnly = True
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "item_name"
        Me.DataGridViewTextBoxColumn12.FillWeight = 300.0!
        Me.DataGridViewTextBoxColumn12.HeaderText = "الصنف"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "U_Name"
        Me.DataGridViewTextBoxColumn13.FillWeight = 150.0!
        Me.DataGridViewTextBoxColumn13.HeaderText = "الوحدة"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.Format = "N1"
        Me.QTY_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.QTY_CL.FillWeight = 150.0!
        Me.QTY_CL.HeaderText = "العدد"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'T_Price_CL
        '
        Me.T_Price_CL.DataPropertyName = "T_Price"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.Format = "N2"
        Me.T_Price_CL.DefaultCellStyle = DataGridViewCellStyle4
        Me.T_Price_CL.FillWeight = 200.0!
        Me.T_Price_CL.HeaderText = "الإجمالي"
        Me.T_Price_CL.Name = "T_Price_CL"
        Me.T_Price_CL.ReadOnly = True
        '
        'USER_NAME_CL
        '
        Me.USER_NAME_CL.DataPropertyName = "UserName"
        Me.USER_NAME_CL.HeaderText = "المستخدم"
        Me.USER_NAME_CL.Name = "USER_NAME_CL"
        Me.USER_NAME_CL.ReadOnly = True
        '
        'IMEx_Search_btn
        '
        Me.IMEx_Search_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IMEx_Search_btn.BackColor = System.Drawing.Color.White
        Me.IMEx_Search_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.IMEx_Search_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMEx_Search_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IMEx_Search_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMEx_Search_btn.ForeColor = System.Drawing.Color.Black
        Me.IMEx_Search_btn.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.IMEx_Search_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IMEx_Search_btn.Location = New System.Drawing.Point(1, 1)
        Me.IMEx_Search_btn.Name = "IMEx_Search_btn"
        Me.IMEx_Search_btn.Size = New System.Drawing.Size(120, 38)
        Me.IMEx_Search_btn.TabIndex = 665
        Me.IMEx_Search_btn.Text = "بحث"
        Me.IMEx_Search_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.IMEx_Search_btn.UseVisualStyleBackColor = False
        '
        'IMEx_print_btn
        '
        Me.IMEx_print_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IMEx_print_btn.BackColor = System.Drawing.Color.White
        Me.IMEx_print_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMEx_print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.IMEx_print_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMEx_print_btn.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.IMEx_print_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IMEx_print_btn.Location = New System.Drawing.Point(122, 1)
        Me.IMEx_print_btn.Name = "IMEx_print_btn"
        Me.IMEx_print_btn.Size = New System.Drawing.Size(120, 38)
        Me.IMEx_print_btn.TabIndex = 664
        Me.IMEx_print_btn.Text = "طباعة"
        Me.IMEx_print_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.IMEx_print_btn.UseVisualStyleBackColor = False
        Me.IMEx_print_btn.Visible = False
        '
        'TABLES_MV_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(895, 659)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.Label72)
        Me.Controls.Add(Me.TABLES_cm)
        Me.Controls.Add(Me.IMEx_DV)
        Me.Controls.Add(Me.IMEx_Search_btn)
        Me.Controls.Add(Me.IMEx_print_btn)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TABLES_MV_Report"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        CType(Me.IMEx_DV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents IMEx_T_M_txt As System.Windows.Forms.TextBox
    Friend WithEvents IMEx_T_Q_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents TABLES_cm As System.Windows.Forms.ComboBox
    Friend WithEvents IMEx_DV As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents IMEx_Search_btn As System.Windows.Forms.Button
    Friend WithEvents IMEx_print_btn As System.Windows.Forms.Button
    Friend WithEvents TB_ORDER_CODE_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents T_Price_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents USER_NAME_CL As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
