<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Show_Table_IM
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.IMGrid = New MetroFramework.Controls.MetroGrid()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.TB_Info = New System.Windows.Forms.Label()
        Me.IM_T_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unit_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_NameCL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unit_Price_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.IMGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IMGrid
        '
        Me.IMGrid.AllowUserToAddRows = False
        Me.IMGrid.AllowUserToDeleteRows = False
        Me.IMGrid.AllowUserToResizeRows = False
        Me.IMGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.IMGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.IMGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.IMGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.IMGrid.CausesValidation = False
        Me.IMGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.IMGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IMGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.IMGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.IMGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IM_T_ID, Me.QTY_CL, Me.Unit_CL, Me.IM_NameCL, Me.Unit_Price_CL, Me.Total_CL})
        Me.IMGrid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IMGrid.DefaultCellStyle = DataGridViewCellStyle4
        Me.IMGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.IMGrid.EnableHeadersVisualStyles = False
        Me.IMGrid.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.IMGrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.IMGrid.Location = New System.Drawing.Point(3, 45)
        Me.IMGrid.MultiSelect = False
        Me.IMGrid.Name = "IMGrid"
        Me.IMGrid.ReadOnly = True
        Me.IMGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.IMGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IMGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.IMGrid.RowHeadersVisible = False
        Me.IMGrid.RowHeadersWidth = 100
        Me.IMGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IMGrid.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.IMGrid.RowTemplate.Height = 150
        Me.IMGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IMGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.IMGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.IMGrid.Size = New System.Drawing.Size(422, 409)
        Me.IMGrid.TabIndex = 563
        Me.IMGrid.Theme = MetroFramework.MetroThemeStyle.Light
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
        Me.ExitFormButton.Location = New System.Drawing.Point(301, 458)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(124, 45)
        Me.ExitFormButton.TabIndex = 659
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'TB_Info
        '
        Me.TB_Info.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TB_Info.BackColor = System.Drawing.Color.DarkRed
        Me.TB_Info.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TB_Info.Font = New System.Drawing.Font("Segoe UI Semibold", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_Info.ForeColor = System.Drawing.SystemColors.Info
        Me.TB_Info.Location = New System.Drawing.Point(119, 2)
        Me.TB_Info.Name = "TB_Info"
        Me.TB_Info.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TB_Info.Size = New System.Drawing.Size(166, 40)
        Me.TB_Info.TabIndex = 660
        Me.TB_Info.Text = "---"
        Me.TB_Info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'IM_T_ID
        '
        Me.IM_T_ID.DataPropertyName = "T_ID"
        Me.IM_T_ID.HeaderText = "ر.الآلي"
        Me.IM_T_ID.Name = "IM_T_ID"
        Me.IM_T_ID.ReadOnly = True
        Me.IM_T_ID.Visible = False
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        DataGridViewCellStyle2.Format = "N2"
        Me.QTY_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.QTY_CL.FillWeight = 79.91894!
        Me.QTY_CL.HeaderText = "الكمية"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'Unit_CL
        '
        Me.Unit_CL.DataPropertyName = "Unit_Name"
        Me.Unit_CL.HeaderText = "الوحدة"
        Me.Unit_CL.Name = "Unit_CL"
        Me.Unit_CL.ReadOnly = True
        Me.Unit_CL.Visible = False
        '
        'IM_NameCL
        '
        Me.IM_NameCL.DataPropertyName = "IM_Name"
        Me.IM_NameCL.FillWeight = 170.7813!
        Me.IM_NameCL.HeaderText = "الصنف"
        Me.IM_NameCL.Name = "IM_NameCL"
        Me.IM_NameCL.ReadOnly = True
        '
        'Unit_Price_CL
        '
        Me.Unit_Price_CL.DataPropertyName = "Price"
        Me.Unit_Price_CL.HeaderText = "س.الوحدة"
        Me.Unit_Price_CL.Name = "Unit_Price_CL"
        Me.Unit_Price_CL.ReadOnly = True
        Me.Unit_Price_CL.ToolTipText = "سعر الوحدة"
        Me.Unit_Price_CL.Visible = False
        '
        'Total_CL
        '
        Me.Total_CL.DataPropertyName = "T_Price"
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Total_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.Total_CL.FillWeight = 79.91894!
        Me.Total_CL.HeaderText = "الإجمالي"
        Me.Total_CL.Name = "Total_CL"
        Me.Total_CL.ReadOnly = True
        Me.Total_CL.Visible = False
        '
        'Show_Table_IM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(426, 504)
        Me.ControlBox = False
        Me.Controls.Add(Me.TB_Info)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.IMGrid)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Show_Table_IM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "عرض أصناف الطاولـــة"
        CType(Me.IMGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IMGrid As MetroFramework.Controls.MetroGrid
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents TB_Info As System.Windows.Forms.Label
    Friend WithEvents IM_T_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_NameCL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit_Price_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total_CL As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
