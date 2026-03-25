<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Show_EXP_Rpt_Details
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Show_EXP_Rpt_Details))
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.BillMetroGrid = New MetroFramework.Controls.MetroGrid()
        Me.DATE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EXP_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ag_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QYT_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ABOUT_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_SH_txt = New System.Windows.Forms.TextBox()
        CType(Me.BillMetroGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(769, 578)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(105, 38)
        Me.ExitFormButton.TabIndex = 666
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'BillMetroGrid
        '
        Me.BillMetroGrid.AllowUserToAddRows = False
        Me.BillMetroGrid.AllowUserToDeleteRows = False
        Me.BillMetroGrid.AllowUserToResizeRows = False
        Me.BillMetroGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.BillMetroGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BillMetroGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.BillMetroGrid.CausesValidation = False
        Me.BillMetroGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.BillMetroGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillMetroGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.BillMetroGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.BillMetroGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DATE_CL, Me.EXP_ID_CL, Me.Ag_name_CL, Me.QYT_CL, Me.Total_CL, Me.ABOUT_CL})
        Me.BillMetroGrid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.BillMetroGrid.DefaultCellStyle = DataGridViewCellStyle2
        Me.BillMetroGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.BillMetroGrid.EnableHeadersVisualStyles = False
        Me.BillMetroGrid.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.BillMetroGrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BillMetroGrid.Location = New System.Drawing.Point(1, 29)
        Me.BillMetroGrid.MultiSelect = False
        Me.BillMetroGrid.Name = "BillMetroGrid"
        Me.BillMetroGrid.ReadOnly = True
        Me.BillMetroGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillMetroGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.BillMetroGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        Me.BillMetroGrid.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.BillMetroGrid.RowTemplate.Height = 30
        Me.BillMetroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.BillMetroGrid.Size = New System.Drawing.Size(873, 544)
        Me.BillMetroGrid.TabIndex = 667
        Me.BillMetroGrid.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'DATE_CL
        '
        Me.DATE_CL.DataPropertyName = "DATE"
        Me.DATE_CL.HeaderText = "التاريخ"
        Me.DATE_CL.Name = "DATE_CL"
        Me.DATE_CL.ReadOnly = True
        Me.DATE_CL.Width = 77
        '
        'EXP_ID_CL
        '
        Me.EXP_ID_CL.DataPropertyName = "EXP_ID"
        Me.EXP_ID_CL.HeaderText = "رقم الفاتورة"
        Me.EXP_ID_CL.Name = "EXP_ID_CL"
        Me.EXP_ID_CL.ReadOnly = True
        Me.EXP_ID_CL.Width = 116
        '
        'Ag_name_CL
        '
        Me.Ag_name_CL.DataPropertyName = "Ag_name"
        Me.Ag_name_CL.HeaderText = "الحساب"
        Me.Ag_name_CL.Name = "Ag_name_CL"
        Me.Ag_name_CL.ReadOnly = True
        Me.Ag_name_CL.Width = 87
        '
        'QYT_CL
        '
        Me.QYT_CL.DataPropertyName = "QYT"
        Me.QYT_CL.FillWeight = 112.3096!
        Me.QYT_CL.HeaderText = "العدد"
        Me.QYT_CL.Name = "QYT_CL"
        Me.QYT_CL.ReadOnly = True
        Me.QYT_CL.Width = 71
        '
        'Total_CL
        '
        Me.Total_CL.DataPropertyName = "Total"
        Me.Total_CL.HeaderText = "الإجمالي"
        Me.Total_CL.Name = "Total_CL"
        Me.Total_CL.ReadOnly = True
        Me.Total_CL.Width = 91
        '
        'ABOUT_CL
        '
        Me.ABOUT_CL.DataPropertyName = "ABOUT"
        Me.ABOUT_CL.HeaderText = "ملاحظة"
        Me.ABOUT_CL.Name = "ABOUT_CL"
        Me.ABOUT_CL.ReadOnly = True
        Me.ABOUT_CL.Width = 86
        '
        'IM_SH_txt
        '
        Me.IM_SH_txt.BackColor = System.Drawing.SystemColors.Window
        Me.IM_SH_txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_SH_txt.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_SH_txt.Location = New System.Drawing.Point(1, 1)
        Me.IM_SH_txt.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.IM_SH_txt.Name = "IM_SH_txt"
        Me.IM_SH_txt.Size = New System.Drawing.Size(873, 26)
        Me.IM_SH_txt.TabIndex = 669
        '
        'Show_EXP_Rpt_Details
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(875, 616)
        Me.Controls.Add(Me.IM_SH_txt)
        Me.Controls.Add(Me.BillMetroGrid)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Show_EXP_Rpt_Details"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تفاصيل تقرير بنذ مصروفات"
        CType(Me.BillMetroGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents BillMetroGrid As MetroFramework.Controls.MetroGrid
    Friend WithEvents DATE_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EXP_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Ag_name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QYT_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ABOUT_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_SH_txt As TextBox
End Class
