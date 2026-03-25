<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Show_Tables_Aparts
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Show_Tables_Aparts))
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.BillMetroGrid = New MetroFramework.Controls.MetroGrid()
        Me.PureLabel = New System.Windows.Forms.Label()
        Me.PureTextBox = New System.Windows.Forms.TextBox()
        Me.EX_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IMUnit_CL_2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price_CL_2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total_CL_2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.ExitFormButton.Location = New System.Drawing.Point(510, 326)
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
        Me.BillMetroGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.BillMetroGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.BillMetroGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BillMetroGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.BillMetroGrid.CausesValidation = False
        Me.BillMetroGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.BillMetroGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillMetroGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.BillMetroGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.BillMetroGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EX_Name_CL, Me.IMUnit_CL_2, Me.QTY_CL, Me.Price_CL_2, Me.Total_CL_2})
        Me.BillMetroGrid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.BillMetroGrid.DefaultCellStyle = DataGridViewCellStyle5
        Me.BillMetroGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.BillMetroGrid.EnableHeadersVisualStyles = False
        Me.BillMetroGrid.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.BillMetroGrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BillMetroGrid.Location = New System.Drawing.Point(1, 1)
        Me.BillMetroGrid.MultiSelect = False
        Me.BillMetroGrid.Name = "BillMetroGrid"
        Me.BillMetroGrid.ReadOnly = True
        Me.BillMetroGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillMetroGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.BillMetroGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Segoe UI", 12.25!)
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black
        Me.BillMetroGrid.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.BillMetroGrid.RowTemplate.Height = 30
        Me.BillMetroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.BillMetroGrid.Size = New System.Drawing.Size(614, 319)
        Me.BillMetroGrid.TabIndex = 667
        Me.BillMetroGrid.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'PureLabel
        '
        Me.PureLabel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.PureLabel.AutoSize = True
        Me.PureLabel.BackColor = System.Drawing.Color.Transparent
        Me.PureLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PureLabel.ForeColor = System.Drawing.Color.DarkRed
        Me.PureLabel.Location = New System.Drawing.Point(144, 333)
        Me.PureLabel.Name = "PureLabel"
        Me.PureLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PureLabel.Size = New System.Drawing.Size(67, 23)
        Me.PureLabel.TabIndex = 669
        Me.PureLabel.Text = "المدفوع"
        Me.PureLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PureTextBox
        '
        Me.PureTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.PureTextBox.BackColor = System.Drawing.SystemColors.Info
        Me.PureTextBox.Font = New System.Drawing.Font("Stencil", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PureTextBox.ForeColor = System.Drawing.Color.DarkRed
        Me.PureTextBox.Location = New System.Drawing.Point(1, 325)
        Me.PureTextBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PureTextBox.Name = "PureTextBox"
        Me.PureTextBox.ReadOnly = True
        Me.PureTextBox.Size = New System.Drawing.Size(140, 36)
        Me.PureTextBox.TabIndex = 668
        Me.PureTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        Me.IMUnit_CL_2.DataPropertyName = "Unit_Name"
        Me.IMUnit_CL_2.HeaderText = "الوحدة"
        Me.IMUnit_CL_2.Name = "IMUnit_CL_2"
        Me.IMUnit_CL_2.ReadOnly = True
        Me.IMUnit_CL_2.Visible = False
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        DataGridViewCellStyle2.Format = "N1"
        Me.QTY_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.QTY_CL.FillWeight = 112.3096!
        Me.QTY_CL.HeaderText = "العدد"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'Price_CL_2
        '
        Me.Price_CL_2.DataPropertyName = "Price"
        DataGridViewCellStyle3.Format = "N2"
        Me.Price_CL_2.DefaultCellStyle = DataGridViewCellStyle3
        Me.Price_CL_2.FillWeight = 112.3096!
        Me.Price_CL_2.HeaderText = "السعر"
        Me.Price_CL_2.Name = "Price_CL_2"
        Me.Price_CL_2.ReadOnly = True
        '
        'Total_CL_2
        '
        Me.Total_CL_2.DataPropertyName = "T_Price"
        DataGridViewCellStyle4.Format = "N2"
        Me.Total_CL_2.DefaultCellStyle = DataGridViewCellStyle4
        Me.Total_CL_2.FillWeight = 112.3096!
        Me.Total_CL_2.HeaderText = "الإجمالي"
        Me.Total_CL_2.Name = "Total_CL_2"
        Me.Total_CL_2.ReadOnly = True
        Me.Total_CL_2.Visible = False
        '
        'Show_Tables_Aparts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(615, 364)
        Me.ControlBox = False
        Me.Controls.Add(Me.PureLabel)
        Me.Controls.Add(Me.PureTextBox)
        Me.Controls.Add(Me.BillMetroGrid)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Show_Tables_Aparts"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الأصناف المسدده من الطاولة"
        CType(Me.BillMetroGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents BillMetroGrid As MetroFramework.Controls.MetroGrid
    Friend WithEvents PureLabel As System.Windows.Forms.Label
    Friend WithEvents PureTextBox As System.Windows.Forms.TextBox
    Friend WithEvents EX_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IMUnit_CL_2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price_CL_2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total_CL_2 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
