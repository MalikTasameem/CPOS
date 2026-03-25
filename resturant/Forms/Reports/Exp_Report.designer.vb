<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Exp_Report
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Exp_Report))
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.EXP_TOTAL_QTY_TXT = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.EXP_TOTAL_M_TXT = New System.Windows.Forms.TextBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.EXP_GridViewX1 = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.EXP_SH_btn = New System.Windows.Forms.Button()
        Me.EXP_P_btn = New System.Windows.Forms.Button()
        Me.EX_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn28 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EXP_QYT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EXP_TOTAL_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel11.SuspendLayout()
        CType(Me.EXP_GridViewX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.EXP_TOTAL_QTY_TXT)
        Me.Panel11.Controls.Add(Me.Label45)
        Me.Panel11.Controls.Add(Me.EXP_TOTAL_M_TXT)
        Me.Panel11.Controls.Add(Me.Label63)
        Me.Panel11.Location = New System.Drawing.Point(0, 620)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(893, 38)
        Me.Panel11.TabIndex = 666
        '
        'EXP_TOTAL_QTY_TXT
        '
        Me.EXP_TOTAL_QTY_TXT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.EXP_TOTAL_QTY_TXT.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EXP_TOTAL_QTY_TXT.ForeColor = System.Drawing.Color.Red
        Me.EXP_TOTAL_QTY_TXT.Location = New System.Drawing.Point(467, 5)
        Me.EXP_TOTAL_QTY_TXT.Name = "EXP_TOTAL_QTY_TXT"
        Me.EXP_TOTAL_QTY_TXT.ReadOnly = True
        Me.EXP_TOTAL_QTY_TXT.Size = New System.Drawing.Size(126, 30)
        Me.EXP_TOTAL_QTY_TXT.TabIndex = 404
        Me.EXP_TOTAL_QTY_TXT.Text = "0.00"
        Me.EXP_TOTAL_QTY_TXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label45
        '
        Me.Label45.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label45.AutoSize = True
        Me.Label45.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label45.Location = New System.Drawing.Point(317, 10)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(97, 21)
        Me.Label45.TabIndex = 407
        Me.Label45.Text = "إجمالي القيمة"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'EXP_TOTAL_M_TXT
        '
        Me.EXP_TOTAL_M_TXT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.EXP_TOTAL_M_TXT.Font = New System.Drawing.Font("Stencil", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EXP_TOTAL_M_TXT.ForeColor = System.Drawing.Color.Red
        Me.EXP_TOTAL_M_TXT.Location = New System.Drawing.Point(187, 5)
        Me.EXP_TOTAL_M_TXT.Name = "EXP_TOTAL_M_TXT"
        Me.EXP_TOTAL_M_TXT.ReadOnly = True
        Me.EXP_TOTAL_M_TXT.Size = New System.Drawing.Size(126, 30)
        Me.EXP_TOTAL_M_TXT.TabIndex = 405
        Me.EXP_TOTAL_M_TXT.Text = "0.00"
        Me.EXP_TOTAL_M_TXT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label63
        '
        Me.Label63.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label63.AutoSize = True
        Me.Label63.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label63.Location = New System.Drawing.Point(597, 10)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(108, 21)
        Me.Label63.TabIndex = 406
        Me.Label63.Text = "إجمالي الكميات"
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'EXP_GridViewX1
        '
        Me.EXP_GridViewX1.AllowUserToAddRows = False
        Me.EXP_GridViewX1.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro
        Me.EXP_GridViewX1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.EXP_GridViewX1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.EXP_GridViewX1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.EXP_GridViewX1.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.EXP_GridViewX1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.EXP_GridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.EXP_GridViewX1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EX_ID_CL, Me.DataGridViewTextBoxColumn28, Me.EXP_QYT, Me.EXP_TOTAL_CL})
        Me.EXP_GridViewX1.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.EXP_GridViewX1.DefaultCellStyle = DataGridViewCellStyle4
        Me.EXP_GridViewX1.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.EXP_GridViewX1.Location = New System.Drawing.Point(2, 41)
        Me.EXP_GridViewX1.Name = "EXP_GridViewX1"
        Me.EXP_GridViewX1.ReadOnly = True
        Me.EXP_GridViewX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.EXP_GridViewX1.RowHeadersVisible = False
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.EXP_GridViewX1.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.EXP_GridViewX1.RowTemplate.Height = 32
        Me.EXP_GridViewX1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.EXP_GridViewX1.Size = New System.Drawing.Size(891, 577)
        Me.EXP_GridViewX1.TabIndex = 663
        '
        'EXP_SH_btn
        '
        Me.EXP_SH_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EXP_SH_btn.BackColor = System.Drawing.Color.White
        Me.EXP_SH_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.EXP_SH_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EXP_SH_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EXP_SH_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EXP_SH_btn.ForeColor = System.Drawing.Color.Black
        Me.EXP_SH_btn.Image = Global.resturant.My.Resources.Resources.if_search_46834
        Me.EXP_SH_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EXP_SH_btn.Location = New System.Drawing.Point(773, 2)
        Me.EXP_SH_btn.Name = "EXP_SH_btn"
        Me.EXP_SH_btn.Size = New System.Drawing.Size(120, 38)
        Me.EXP_SH_btn.TabIndex = 665
        Me.EXP_SH_btn.Text = "بحث"
        Me.EXP_SH_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.EXP_SH_btn.UseVisualStyleBackColor = False
        '
        'EXP_P_btn
        '
        Me.EXP_P_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EXP_P_btn.BackColor = System.Drawing.Color.White
        Me.EXP_P_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.EXP_P_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.EXP_P_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EXP_P_btn.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.EXP_P_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EXP_P_btn.Location = New System.Drawing.Point(651, 2)
        Me.EXP_P_btn.Name = "EXP_P_btn"
        Me.EXP_P_btn.Size = New System.Drawing.Size(120, 38)
        Me.EXP_P_btn.TabIndex = 664
        Me.EXP_P_btn.Text = "طباعة"
        Me.EXP_P_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.EXP_P_btn.UseVisualStyleBackColor = False
        '
        'EX_ID_CL
        '
        Me.EX_ID_CL.DataPropertyName = "EX_ID"
        Me.EX_ID_CL.HeaderText = "EX_ID"
        Me.EX_ID_CL.Name = "EX_ID_CL"
        Me.EX_ID_CL.ReadOnly = True
        Me.EX_ID_CL.Visible = False
        '
        'DataGridViewTextBoxColumn28
        '
        Me.DataGridViewTextBoxColumn28.DataPropertyName = "EX_Name"
        Me.DataGridViewTextBoxColumn28.FillWeight = 5.836273!
        Me.DataGridViewTextBoxColumn28.HeaderText = "المصــروف"
        Me.DataGridViewTextBoxColumn28.Name = "DataGridViewTextBoxColumn28"
        Me.DataGridViewTextBoxColumn28.ReadOnly = True
        '
        'EXP_QYT
        '
        Me.EXP_QYT.DataPropertyName = "QYT"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EXP_QYT.DefaultCellStyle = DataGridViewCellStyle3
        Me.EXP_QYT.FillWeight = 2.918137!
        Me.EXP_QYT.HeaderText = "العدد"
        Me.EXP_QYT.Name = "EXP_QYT"
        Me.EXP_QYT.ReadOnly = True
        '
        'EXP_TOTAL_CL
        '
        Me.EXP_TOTAL_CL.DataPropertyName = "Total"
        Me.EXP_TOTAL_CL.FillWeight = 3.0!
        Me.EXP_TOTAL_CL.HeaderText = "الإجمالي"
        Me.EXP_TOTAL_CL.Name = "EXP_TOTAL_CL"
        Me.EXP_TOTAL_CL.ReadOnly = True
        '
        'Exp_Report
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(895, 659)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel11)
        Me.Controls.Add(Me.EXP_GridViewX1)
        Me.Controls.Add(Me.EXP_SH_btn)
        Me.Controls.Add(Me.EXP_P_btn)
        Me.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Exp_Report"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        CType(Me.EXP_GridViewX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents EXP_TOTAL_QTY_TXT As System.Windows.Forms.TextBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents EXP_TOTAL_M_TXT As System.Windows.Forms.TextBox
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents EXP_GridViewX1 As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents EXP_SH_btn As System.Windows.Forms.Button
    Friend WithEvents EXP_P_btn As System.Windows.Forms.Button
    Friend WithEvents EX_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EXP_QYT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EXP_TOTAL_CL As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
