<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ShowBill_IM
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ShowBill_IM))
        Me.VoidBillBtn = New System.Windows.Forms.Button()
        Me.MetroGrid = New MetroFramework.Controls.MetroGrid()
        Me.Back_Btn = New System.Windows.Forms.Button()
        Me.BillNumTxt = New System.Windows.Forms.TextBox()
        Me.BillTypeCmb = New System.Windows.Forms.ComboBox()
        Me.IM_T_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_NameCL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unit_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unit_Price_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.MetroGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'VoidBillBtn
        '
        Me.VoidBillBtn.BackColor = System.Drawing.Color.Gainsboro
        Me.VoidBillBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.VoidBillBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.VoidBillBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.VoidBillBtn.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VoidBillBtn.ForeColor = System.Drawing.Color.Black
        Me.VoidBillBtn.Image = Global.resturant.My.Resources.Resources.cancel_bill_48
        Me.VoidBillBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.VoidBillBtn.Location = New System.Drawing.Point(401, 402)
        Me.VoidBillBtn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.VoidBillBtn.Name = "VoidBillBtn"
        Me.VoidBillBtn.Size = New System.Drawing.Size(120, 67)
        Me.VoidBillBtn.TabIndex = 562
        Me.VoidBillBtn.Text = "إلغــاء"
        Me.VoidBillBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.VoidBillBtn.UseVisualStyleBackColor = False
        '
        'MetroGrid
        '
        Me.MetroGrid.AllowUserToAddRows = False
        Me.MetroGrid.AllowUserToDeleteRows = False
        Me.MetroGrid.AllowUserToResizeRows = False
        Me.MetroGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.MetroGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.MetroGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MetroGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.MetroGrid.CausesValidation = False
        Me.MetroGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.MetroGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MetroGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.MetroGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MetroGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IM_T_ID, Me.IM_ID_CL, Me.QTY_CL, Me.IM_NameCL, Me.Unit_CL, Me.Unit_Price_CL, Me.Total_CL})
        Me.MetroGrid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MetroGrid.DefaultCellStyle = DataGridViewCellStyle4
        Me.MetroGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.MetroGrid.EnableHeadersVisualStyles = False
        Me.MetroGrid.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.MetroGrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MetroGrid.Location = New System.Drawing.Point(4, 39)
        Me.MetroGrid.MultiSelect = False
        Me.MetroGrid.Name = "MetroGrid"
        Me.MetroGrid.ReadOnly = True
        Me.MetroGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.MetroGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MetroGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.MetroGrid.RowHeadersVisible = False
        Me.MetroGrid.RowHeadersWidth = 100
        Me.MetroGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MetroGrid.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.MetroGrid.RowTemplate.Height = 150
        Me.MetroGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MetroGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.MetroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.MetroGrid.Size = New System.Drawing.Size(517, 358)
        Me.MetroGrid.TabIndex = 564
        Me.MetroGrid.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'Back_Btn
        '
        Me.Back_Btn.BackColor = System.Drawing.Color.White
        Me.Back_Btn.BackgroundImage = Global.resturant.My.Resources.Resources.Users_Exit_icon
        Me.Back_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Back_Btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Back_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Back_Btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Back_Btn.Location = New System.Drawing.Point(1, 402)
        Me.Back_Btn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Back_Btn.Name = "Back_Btn"
        Me.Back_Btn.Size = New System.Drawing.Size(75, 67)
        Me.Back_Btn.TabIndex = 632
        Me.Back_Btn.UseVisualStyleBackColor = False
        '
        'BillNumTxt
        '
        Me.BillNumTxt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.BillNumTxt.BackColor = System.Drawing.SystemColors.Control
        Me.BillNumTxt.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BillNumTxt.Location = New System.Drawing.Point(185, 1)
        Me.BillNumTxt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BillNumTxt.Name = "BillNumTxt"
        Me.BillNumTxt.ReadOnly = True
        Me.BillNumTxt.Size = New System.Drawing.Size(296, 35)
        Me.BillNumTxt.TabIndex = 634
        Me.BillNumTxt.Tag = "2"
        Me.BillNumTxt.Text = "1"
        Me.BillNumTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BillTypeCmb
        '
        Me.BillTypeCmb.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.BillTypeCmb.BackColor = System.Drawing.SystemColors.Info
        Me.BillTypeCmb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BillTypeCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.BillTypeCmb.Enabled = False
        Me.BillTypeCmb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BillTypeCmb.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BillTypeCmb.FormattingEnabled = True
        Me.BillTypeCmb.Location = New System.Drawing.Point(7, 2)
        Me.BillTypeCmb.Name = "BillTypeCmb"
        Me.BillTypeCmb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.BillTypeCmb.Size = New System.Drawing.Size(175, 33)
        Me.BillTypeCmb.TabIndex = 635
        '
        'IM_T_ID
        '
        Me.IM_T_ID.DataPropertyName = "T_ID"
        Me.IM_T_ID.HeaderText = "ر.الآلي"
        Me.IM_T_ID.Name = "IM_T_ID"
        Me.IM_T_ID.ReadOnly = True
        Me.IM_T_ID.Visible = False
        '
        'IM_ID_CL
        '
        Me.IM_ID_CL.DataPropertyName = "IM_ID"
        Me.IM_ID_CL.HeaderText = "IM_ID"
        Me.IM_ID_CL.Name = "IM_ID_CL"
        Me.IM_ID_CL.ReadOnly = True
        Me.IM_ID_CL.Visible = False
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        Me.QTY_CL.FillWeight = 79.91894!
        Me.QTY_CL.HeaderText = "-"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'IM_NameCL
        '
        Me.IM_NameCL.DataPropertyName = "IM_Name"
        Me.IM_NameCL.FillWeight = 170.7813!
        Me.IM_NameCL.HeaderText = "الصنف"
        Me.IM_NameCL.Name = "IM_NameCL"
        Me.IM_NameCL.ReadOnly = True
        '
        'Unit_CL
        '
        Me.Unit_CL.DataPropertyName = "Unit_Name"
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gray
        Me.Unit_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.Unit_CL.HeaderText = "الوحدة"
        Me.Unit_CL.Name = "Unit_CL"
        Me.Unit_CL.ReadOnly = True
        Me.Unit_CL.Visible = False
        '
        'Unit_Price_CL
        '
        Me.Unit_Price_CL.DataPropertyName = "Price"
        Me.Unit_Price_CL.HeaderText = "السعر"
        Me.Unit_Price_CL.Name = "Unit_Price_CL"
        Me.Unit_Price_CL.ReadOnly = True
        Me.Unit_Price_CL.ToolTipText = "السعر"
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
        '
        'ShowBill_IM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(521, 469)
        Me.ControlBox = False
        Me.Controls.Add(Me.BillTypeCmb)
        Me.Controls.Add(Me.BillNumTxt)
        Me.Controls.Add(Me.Back_Btn)
        Me.Controls.Add(Me.VoidBillBtn)
        Me.Controls.Add(Me.MetroGrid)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
         Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.Name = "ShowBill_IM"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "عرض أصناف الفاتورة"
        CType(Me.MetroGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents VoidBillBtn As System.Windows.Forms.Button
    Friend WithEvents MetroGrid As MetroFramework.Controls.MetroGrid
    Friend WithEvents Back_Btn As System.Windows.Forms.Button
    Friend WithEvents BillNumTxt As System.Windows.Forms.TextBox
    Friend WithEvents BillTypeCmb As System.Windows.Forms.ComboBox
    Friend WithEvents IM_T_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_NameCL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit_Price_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total_CL As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
