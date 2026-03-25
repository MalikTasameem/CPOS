<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ST_Tran_Report
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ST_Tran_Report))
        Me.IMTranTextBox = New System.Windows.Forms.TextBox()
        Me.IMTransButton = New System.Windows.Forms.Button()
        Me.IMTranButton = New System.Windows.Forms.Button()
        Me.IMTranDataGridViewX = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.IMEx_T_Q_txt = New System.Windows.Forms.TextBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ST_cm = New System.Windows.Forms.ComboBox()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ST_To_cm = New System.Windows.Forms.ComboBox()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.St_F_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.St_To_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TOTAL_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IMEx_T_P_txt = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.IMTranDataGridViewX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel9.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.SuspendLayout()
        '
        'IMTranTextBox
        '
        Me.IMTranTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IMTranTextBox.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMTranTextBox.ForeColor = System.Drawing.SystemColors.InfoText
        Me.IMTranTextBox.Location = New System.Drawing.Point(738, 4)
        Me.IMTranTextBox.Name = "IMTranTextBox"
        Me.IMTranTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IMTranTextBox.Size = New System.Drawing.Size(152, 33)
        Me.IMTranTextBox.TabIndex = 374
        Me.IMTranTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'IMTransButton
        '
        Me.IMTransButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IMTransButton.BackColor = System.Drawing.Color.White
        Me.IMTransButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.IMTransButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMTransButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IMTransButton.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMTransButton.ForeColor = System.Drawing.Color.Black
        Me.IMTransButton.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.IMTransButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IMTransButton.Location = New System.Drawing.Point(1, 2)
        Me.IMTransButton.Name = "IMTransButton"
        Me.IMTransButton.Size = New System.Drawing.Size(111, 35)
        Me.IMTransButton.TabIndex = 373
        Me.IMTransButton.Text = "بحث"
        Me.IMTransButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.IMTransButton.UseVisualStyleBackColor = False
        '
        'IMTranButton
        '
        Me.IMTranButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IMTranButton.BackColor = System.Drawing.Color.White
        Me.IMTranButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IMTranButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.IMTranButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMTranButton.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.IMTranButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.IMTranButton.Location = New System.Drawing.Point(113, 2)
        Me.IMTranButton.Name = "IMTranButton"
        Me.IMTranButton.Size = New System.Drawing.Size(111, 35)
        Me.IMTranButton.TabIndex = 375
        Me.IMTranButton.Text = "طباعة"
        Me.IMTranButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.IMTranButton.UseVisualStyleBackColor = False
        '
        'IMTranDataGridViewX
        '
        Me.IMTranDataGridViewX.AllowUserToAddRows = False
        Me.IMTranDataGridViewX.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info
        Me.IMTranDataGridViewX.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.IMTranDataGridViewX.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IMTranDataGridViewX.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.IMTranDataGridViewX.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.IMTranDataGridViewX.BackgroundColor = System.Drawing.Color.SeaShell
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IMTranDataGridViewX.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.IMTranDataGridViewX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.IMTranDataGridViewX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn4, Me.St_F_CL, Me.St_To_CL, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn15, Me.DataGridViewTextBoxColumn5, Me.QTY_CL, Me.TOTAL_CL, Me.DataGridViewTextBoxColumn17})
        Me.IMTranDataGridViewX.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.IMTranDataGridViewX.DefaultCellStyle = DataGridViewCellStyle3
        Me.IMTranDataGridViewX.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.IMTranDataGridViewX.Location = New System.Drawing.Point(2, 41)
        Me.IMTranDataGridViewX.MultiSelect = False
        Me.IMTranDataGridViewX.Name = "IMTranDataGridViewX"
        Me.IMTranDataGridViewX.ReadOnly = True
        Me.IMTranDataGridViewX.RowHeadersVisible = False
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMTranDataGridViewX.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.IMTranDataGridViewX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.IMTranDataGridViewX.Size = New System.Drawing.Size(888, 577)
        Me.IMTranDataGridViewX.TabIndex = 372
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.IMEx_T_P_txt)
        Me.Panel9.Controls.Add(Me.Label2)
        Me.Panel9.Controls.Add(Me.IMEx_T_Q_txt)
        Me.Panel9.Controls.Add(Me.Label51)
        Me.Panel9.Location = New System.Drawing.Point(1, 621)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(891, 35)
        Me.Panel9.TabIndex = 669
        '
        'IMEx_T_Q_txt
        '
        Me.IMEx_T_Q_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.IMEx_T_Q_txt.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMEx_T_Q_txt.ForeColor = System.Drawing.Color.Red
        Me.IMEx_T_Q_txt.Location = New System.Drawing.Point(396, 2)
        Me.IMEx_T_Q_txt.Name = "IMEx_T_Q_txt"
        Me.IMEx_T_Q_txt.ReadOnly = True
        Me.IMEx_T_Q_txt.Size = New System.Drawing.Size(158, 30)
        Me.IMEx_T_Q_txt.TabIndex = 393
        Me.IMEx_T_Q_txt.Text = "0.00"
        Me.IMEx_T_Q_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label51
        '
        Me.Label51.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label51.AutoSize = True
        Me.Label51.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label51.Location = New System.Drawing.Point(554, 7)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(108, 21)
        Me.Label51.TabIndex = 395
        Me.Label51.Text = "إجمالي الكميات"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.ST_cm)
        Me.Panel1.Location = New System.Drawing.Point(225, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(255, 35)
        Me.Panel1.TabIndex = 707
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(178, 7)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 19)
        Me.Label7.TabIndex = 646
        Me.Label7.Text = "من مخزن :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ST_cm
        '
        Me.ST_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ST_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ST_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ST_cm.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold)
        Me.ST_cm.FormattingEnabled = True
        Me.ST_cm.Location = New System.Drawing.Point(2, 2)
        Me.ST_cm.Name = "ST_cm"
        Me.ST_cm.Size = New System.Drawing.Size(171, 29)
        Me.ST_cm.TabIndex = 649
        '
        'Panel12
        '
        Me.Panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel12.Controls.Add(Me.Label1)
        Me.Panel12.Controls.Add(Me.ST_To_cm)
        Me.Panel12.Location = New System.Drawing.Point(481, 3)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(255, 35)
        Me.Panel12.TabIndex = 706
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(179, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 19)
        Me.Label1.TabIndex = 646
        Me.Label1.Text = "إلى مخزن :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ST_To_cm
        '
        Me.ST_To_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ST_To_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ST_To_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ST_To_cm.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold)
        Me.ST_To_cm.FormattingEnabled = True
        Me.ST_To_cm.Location = New System.Drawing.Point(3, 2)
        Me.ST_To_cm.Name = "ST_To_cm"
        Me.ST_To_cm.Size = New System.Drawing.Size(171, 29)
        Me.ST_To_cm.TabIndex = 653
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Date"
        Me.DataGridViewTextBoxColumn4.HeaderText = "التاريخ"
        Me.DataGridViewTextBoxColumn4.MinimumWidth = 100
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn4.Visible = False
        '
        'St_F_CL
        '
        Me.St_F_CL.DataPropertyName = "St_F"
        Me.St_F_CL.HeaderText = "من "
        Me.St_F_CL.Name = "St_F_CL"
        Me.St_F_CL.ReadOnly = True
        '
        'St_To_CL
        '
        Me.St_To_CL.DataPropertyName = "St_To"
        Me.St_To_CL.HeaderText = "إلى"
        Me.St_To_CL.Name = "St_To_CL"
        Me.St_To_CL.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Item_Name"
        Me.DataGridViewTextBoxColumn7.FillWeight = 37.38512!
        Me.DataGridViewTextBoxColumn7.HeaderText = "الصنف"
        Me.DataGridViewTextBoxColumn7.MinimumWidth = 150
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "D_Vaild"
        Me.DataGridViewTextBoxColumn15.HeaderText = "الصلاحية"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "U_Name"
        Me.DataGridViewTextBoxColumn5.HeaderText = "الوحدة"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        Me.QTY_CL.HeaderText = "الكمية"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'TOTAL_CL
        '
        Me.TOTAL_CL.DataPropertyName = "TOTAL"
        Me.TOTAL_CL.HeaderText = "إجمالي التكلفة"
        Me.TOTAL_CL.Name = "TOTAL_CL"
        Me.TOTAL_CL.ReadOnly = True
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "UserName"
        Me.DataGridViewTextBoxColumn17.HeaderText = "المدخل"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        '
        'IMEx_T_P_txt
        '
        Me.IMEx_T_P_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.IMEx_T_P_txt.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMEx_T_P_txt.ForeColor = System.Drawing.Color.Red
        Me.IMEx_T_P_txt.Location = New System.Drawing.Point(94, 2)
        Me.IMEx_T_P_txt.Name = "IMEx_T_P_txt"
        Me.IMEx_T_P_txt.ReadOnly = True
        Me.IMEx_T_P_txt.Size = New System.Drawing.Size(158, 30)
        Me.IMEx_T_P_txt.TabIndex = 396
        Me.IMEx_T_P_txt.Text = "0.00"
        Me.IMEx_T_P_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(252, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 21)
        Me.Label2.TabIndex = 397
        Me.Label2.Text = "إجمالي التكلفة"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ST_Tran_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(895, 659)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel12)
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.IMTranTextBox)
        Me.Controls.Add(Me.IMTransButton)
        Me.Controls.Add(Me.IMTranButton)
        Me.Controls.Add(Me.IMTranDataGridViewX)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ST_Tran_Report"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.IMTranDataGridViewX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents IMTranTextBox As System.Windows.Forms.TextBox
    Friend WithEvents IMTransButton As System.Windows.Forms.Button
    Friend WithEvents IMTranButton As System.Windows.Forms.Button
    Public WithEvents IMTranDataGridViewX As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents IMEx_T_Q_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ST_cm As System.Windows.Forms.ComboBox
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ST_To_cm As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents St_F_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents St_To_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TOTAL_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IMEx_T_P_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
