<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Table_Bill_Screen
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Table_Bill_Screen))
        Me.BillsGrid = New MetroFramework.Controls.MetroGrid()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.U_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.item_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.S_Bill_Pr_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.T_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UserName_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewButtonColumn()
        CType(Me.BillsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BillsGrid
        '
        Me.BillsGrid.AllowUserToAddRows = False
        Me.BillsGrid.AllowUserToDeleteRows = False
        Me.BillsGrid.AllowUserToResizeRows = False
        Me.BillsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.BillsGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.BillsGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BillsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.BillsGrid.CausesValidation = False
        Me.BillsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.BillsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillsGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.BillsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.BillsGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.U_Name_CL, Me.item_name_CL, Me.QTY_CL, Me.S_Bill_Pr_ID_CL, Me.T_Name_CL, Me.UserName_CL, Me.Column1})
        Me.BillsGrid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.BillsGrid.DefaultCellStyle = DataGridViewCellStyle2
        Me.BillsGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BillsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.BillsGrid.EnableHeadersVisualStyles = False
        Me.BillsGrid.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.BillsGrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BillsGrid.Location = New System.Drawing.Point(0, 0)
        Me.BillsGrid.MultiSelect = False
        Me.BillsGrid.Name = "BillsGrid"
        Me.BillsGrid.ReadOnly = True
        Me.BillsGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.BillsGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillsGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.BillsGrid.RowHeadersVisible = False
        Me.BillsGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillsGrid.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.BillsGrid.RowTemplate.Height = 500
        Me.BillsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.BillsGrid.Size = New System.Drawing.Size(1004, 695)
        Me.BillsGrid.TabIndex = 546
        Me.BillsGrid.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "ر.الآلي"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        '
        'U_Name_CL
        '
        Me.U_Name_CL.DataPropertyName = "U_Name"
        Me.U_Name_CL.HeaderText = "الوحدة"
        Me.U_Name_CL.Name = "U_Name_CL"
        Me.U_Name_CL.ReadOnly = True
        Me.U_Name_CL.Visible = False
        '
        'item_name_CL
        '
        Me.item_name_CL.DataPropertyName = "item_name"
        Me.item_name_CL.FillWeight = 79.91894!
        Me.item_name_CL.HeaderText = "الصنف"
        Me.item_name_CL.Name = "item_name_CL"
        Me.item_name_CL.ReadOnly = True
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        Me.QTY_CL.FillWeight = 170.7813!
        Me.QTY_CL.HeaderText = "الكمية"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'S_Bill_Pr_ID_CL
        '
        Me.S_Bill_Pr_ID_CL.DataPropertyName = "S_Bill_Pr_ID"
        Me.S_Bill_Pr_ID_CL.HeaderText = "الفاتورة"
        Me.S_Bill_Pr_ID_CL.Name = "S_Bill_Pr_ID_CL"
        Me.S_Bill_Pr_ID_CL.ReadOnly = True
        Me.S_Bill_Pr_ID_CL.Visible = False
        '
        'T_Name_CL
        '
        Me.T_Name_CL.DataPropertyName = "T_Name"
        Me.T_Name_CL.HeaderText = "الطاولة"
        Me.T_Name_CL.Name = "T_Name_CL"
        Me.T_Name_CL.ReadOnly = True
        '
        'UserName_CL
        '
        Me.UserName_CL.DataPropertyName = "UserName"
        Me.UserName_CL.HeaderText = "المدخل"
        Me.UserName_CL.Name = "UserName_CL"
        Me.UserName_CL.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = ""
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Text = "مسح من الشاشة"
        Me.Column1.UseColumnTextForButtonValue = True
        '
        'Table_Bill_Screen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 695)
        Me.Controls.Add(Me.BillsGrid)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Table_Bill_Screen"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "شاشة طلبيات"
        CType(Me.BillsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BillsGrid As MetroFramework.Controls.MetroGrid
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents T_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents U_Name_CL As DataGridViewTextBoxColumn
    Friend WithEvents item_name_CL As DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As DataGridViewTextBoxColumn
    Friend WithEvents S_Bill_Pr_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents T_Name_CL As DataGridViewTextBoxColumn
    Friend WithEvents UserName_CL As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewButtonColumn
End Class
