<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Show_AG_IM_SALES
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Show_AG_IM_SALES))
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.BillMetroGrid = New MetroFramework.Controls.MetroGrid()
        Me.DATE_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ag_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EX_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IMUnit_CL_2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price_CL_2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.ExitFormButton.Location = New System.Drawing.Point(759, 487)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(2)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(113, 41)
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
        Me.BillMetroGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.BillMetroGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.BillMetroGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BillMetroGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.BillMetroGrid.CausesValidation = False
        Me.BillMetroGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.BillMetroGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillMetroGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.BillMetroGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.BillMetroGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DATE_CL, Me.ag_name_CL, Me.EX_Name_CL, Me.IMUnit_CL_2, Me.QTY_CL, Me.Price_CL_2})
        Me.BillMetroGrid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.BillMetroGrid.DefaultCellStyle = DataGridViewCellStyle3
        Me.BillMetroGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.BillMetroGrid.EnableHeadersVisualStyles = False
        Me.BillMetroGrid.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.BillMetroGrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BillMetroGrid.Location = New System.Drawing.Point(1, 1)
        Me.BillMetroGrid.Margin = New System.Windows.Forms.Padding(2)
        Me.BillMetroGrid.MultiSelect = False
        Me.BillMetroGrid.Name = "BillMetroGrid"
        Me.BillMetroGrid.ReadOnly = True
        Me.BillMetroGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillMetroGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.BillMetroGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 12.25!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        Me.BillMetroGrid.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.BillMetroGrid.RowTemplate.Height = 30
        Me.BillMetroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.BillMetroGrid.Size = New System.Drawing.Size(871, 482)
        Me.BillMetroGrid.TabIndex = 667
        Me.BillMetroGrid.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'DATE_CL
        '
        Me.DATE_CL.DataPropertyName = "DATE"
        Me.DATE_CL.HeaderText = "التاريخ"
        Me.DATE_CL.Name = "DATE_CL"
        Me.DATE_CL.ReadOnly = True
        '
        'ag_name_CL
        '
        Me.ag_name_CL.DataPropertyName = "Ag_Name"
        Me.ag_name_CL.HeaderText = "الزبون"
        Me.ag_name_CL.Name = "ag_name_CL"
        Me.ag_name_CL.ReadOnly = True
        '
        'EX_Name_CL
        '
        Me.EX_Name_CL.DataPropertyName = "item_name"
        Me.EX_Name_CL.FillWeight = 112.3096!
        Me.EX_Name_CL.HeaderText = "الصنــف"
        Me.EX_Name_CL.Name = "EX_Name_CL"
        Me.EX_Name_CL.ReadOnly = True
        '
        'IMUnit_CL_2
        '
        Me.IMUnit_CL_2.DataPropertyName = "U_NAME"
        Me.IMUnit_CL_2.HeaderText = "الوحدة"
        Me.IMUnit_CL_2.Name = "IMUnit_CL_2"
        Me.IMUnit_CL_2.ReadOnly = True
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        Me.QTY_CL.FillWeight = 112.3096!
        Me.QTY_CL.HeaderText = "الكمية"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'Price_CL_2
        '
        Me.Price_CL_2.DataPropertyName = "Price"
        DataGridViewCellStyle2.Format = "N2"
        Me.Price_CL_2.DefaultCellStyle = DataGridViewCellStyle2
        Me.Price_CL_2.FillWeight = 112.3096!
        Me.Price_CL_2.HeaderText = "السعر"
        Me.Price_CL_2.Name = "Price_CL_2"
        Me.Price_CL_2.ReadOnly = True
        '
        'Show_AG_IM_SALES
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(872, 530)
        Me.ControlBox = False
        Me.Controls.Add(Me.BillMetroGrid)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Show_AG_IM_SALES"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "عرض مبيعات الصنف"
        CType(Me.BillMetroGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents BillMetroGrid As MetroFramework.Controls.MetroGrid
    Friend WithEvents DATE_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ag_name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EX_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IMUnit_CL_2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price_CL_2 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
