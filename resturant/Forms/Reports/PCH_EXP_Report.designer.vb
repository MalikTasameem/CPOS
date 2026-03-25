<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PCH_EXP_Report
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PCH_EXP_Report))
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Pch_Exp_T_txt = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Pch_ExpDGV = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pch_Exp_Total_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pch_ExpSearchBtn = New System.Windows.Forms.Button()
        Me.Pch_ExpPrintBtn = New System.Windows.Forms.Button()
        Me.Panel15.SuspendLayout()
        CType(Me.Pch_ExpDGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel15
        '
        Me.Panel15.Controls.Add(Me.Pch_Exp_T_txt)
        Me.Panel15.Controls.Add(Me.Label14)
        Me.Panel15.Location = New System.Drawing.Point(1, 625)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(893, 32)
        Me.Panel15.TabIndex = 692
        '
        'Pch_Exp_T_txt
        '
        Me.Pch_Exp_T_txt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Pch_Exp_T_txt.Font = New System.Drawing.Font("Stencil", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pch_Exp_T_txt.ForeColor = System.Drawing.Color.Red
        Me.Pch_Exp_T_txt.Location = New System.Drawing.Point(5, 3)
        Me.Pch_Exp_T_txt.Name = "Pch_Exp_T_txt"
        Me.Pch_Exp_T_txt.ReadOnly = True
        Me.Pch_Exp_T_txt.Size = New System.Drawing.Size(189, 26)
        Me.Pch_Exp_T_txt.TabIndex = 425
        Me.Pch_Exp_T_txt.Text = "0.00"
        Me.Pch_Exp_T_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label14.Location = New System.Drawing.Point(198, 5)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(63, 21)
        Me.Label14.TabIndex = 427
        Me.Label14.Text = "الإجمالي"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pch_ExpDGV
        '
        Me.Pch_ExpDGV.AllowUserToAddRows = False
        Me.Pch_ExpDGV.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro
        Me.Pch_ExpDGV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.Pch_ExpDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Pch_ExpDGV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.Pch_ExpDGV.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Pch_ExpDGV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.Pch_ExpDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Pch_ExpDGV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn10, Me.Pch_Exp_Total_CL})
        Me.Pch_ExpDGV.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Pch_ExpDGV.DefaultCellStyle = DataGridViewCellStyle4
        Me.Pch_ExpDGV.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.Pch_ExpDGV.Location = New System.Drawing.Point(1, 41)
        Me.Pch_ExpDGV.Name = "Pch_ExpDGV"
        Me.Pch_ExpDGV.ReadOnly = True
        Me.Pch_ExpDGV.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Pch_ExpDGV.RowHeadersVisible = False
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Pch_ExpDGV.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.Pch_ExpDGV.RowTemplate.Height = 32
        Me.Pch_ExpDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Pch_ExpDGV.Size = New System.Drawing.Size(893, 581)
        Me.Pch_ExpDGV.TabIndex = 691
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Ex_Name"
        Me.DataGridViewTextBoxColumn10.FillWeight = 300.0!
        Me.DataGridViewTextBoxColumn10.HeaderText = "البنــــد"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        '
        'Pch_Exp_Total_CL
        '
        Me.Pch_Exp_Total_CL.DataPropertyName = "Total"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.Format = "N3"
        Me.Pch_Exp_Total_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.Pch_Exp_Total_CL.FillWeight = 200.0!
        Me.Pch_Exp_Total_CL.HeaderText = "الإجمالي"
        Me.Pch_Exp_Total_CL.Name = "Pch_Exp_Total_CL"
        Me.Pch_Exp_Total_CL.ReadOnly = True
        '
        'Pch_ExpSearchBtn
        '
        Me.Pch_ExpSearchBtn.BackColor = System.Drawing.Color.White
        Me.Pch_ExpSearchBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Pch_ExpSearchBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Pch_ExpSearchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Pch_ExpSearchBtn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pch_ExpSearchBtn.ForeColor = System.Drawing.Color.Black
        Me.Pch_ExpSearchBtn.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.Pch_ExpSearchBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Pch_ExpSearchBtn.Location = New System.Drawing.Point(746, 2)
        Me.Pch_ExpSearchBtn.Name = "Pch_ExpSearchBtn"
        Me.Pch_ExpSearchBtn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Pch_ExpSearchBtn.Size = New System.Drawing.Size(149, 38)
        Me.Pch_ExpSearchBtn.TabIndex = 690
        Me.Pch_ExpSearchBtn.Text = "بحث"
        Me.Pch_ExpSearchBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Pch_ExpSearchBtn.UseVisualStyleBackColor = False
        '
        'Pch_ExpPrintBtn
        '
        Me.Pch_ExpPrintBtn.BackColor = System.Drawing.Color.White
        Me.Pch_ExpPrintBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Pch_ExpPrintBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Pch_ExpPrintBtn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pch_ExpPrintBtn.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.Pch_ExpPrintBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Pch_ExpPrintBtn.Location = New System.Drawing.Point(595, 2)
        Me.Pch_ExpPrintBtn.Name = "Pch_ExpPrintBtn"
        Me.Pch_ExpPrintBtn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Pch_ExpPrintBtn.Size = New System.Drawing.Size(149, 38)
        Me.Pch_ExpPrintBtn.TabIndex = 689
        Me.Pch_ExpPrintBtn.Text = "طباعة"
        Me.Pch_ExpPrintBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Pch_ExpPrintBtn.UseVisualStyleBackColor = False
        '
        'PCH_EXP_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(895, 659)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel15)
        Me.Controls.Add(Me.Pch_ExpDGV)
        Me.Controls.Add(Me.Pch_ExpSearchBtn)
        Me.Controls.Add(Me.Pch_ExpPrintBtn)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
         Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PCH_EXP_Report"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        CType(Me.Pch_ExpDGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Pch_Exp_T_txt As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Pch_ExpDGV As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pch_Exp_Total_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pch_ExpSearchBtn As System.Windows.Forms.Button
    Friend WithEvents Pch_ExpPrintBtn As System.Windows.Forms.Button
End Class
