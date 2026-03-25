<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TB_BillIM
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
        Me.BillIM_Grid = New MetroFramework.Controls.MetroGrid()
        Me.T_ID_3_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Bill_T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TB_IM_QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ptr_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Printer_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Plus_CL = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Min_CL = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Delete = New System.Windows.Forms.DataGridViewButtonColumn()
        CType(Me.BillIM_Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BillIM_Grid
        '
        Me.BillIM_Grid.AllowUserToAddRows = False
        Me.BillIM_Grid.AllowUserToDeleteRows = False
        Me.BillIM_Grid.AllowUserToResizeRows = False
        Me.BillIM_Grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.BillIM_Grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.BillIM_Grid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BillIM_Grid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.BillIM_Grid.CausesValidation = False
        Me.BillIM_Grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.BillIM_Grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillIM_Grid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.BillIM_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.BillIM_Grid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_3_CL, Me.Bill_T_ID_CL, Me.TB_IM_QTY_CL, Me.IM_Name_CL, Me.Ptr_ID_CL, Me.Printer_Name_CL, Me.Plus_CL, Me.Min_CL, Me.Delete})
        Me.BillIM_Grid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillIM_Grid.DefaultCellStyle = DataGridViewCellStyle3
        Me.BillIM_Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BillIM_Grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.BillIM_Grid.EnableHeadersVisualStyles = False
        Me.BillIM_Grid.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.BillIM_Grid.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BillIM_Grid.Location = New System.Drawing.Point(0, 0)
        Me.BillIM_Grid.MultiSelect = False
        Me.BillIM_Grid.Name = "BillIM_Grid"
        Me.BillIM_Grid.ReadOnly = True
        Me.BillIM_Grid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.BillIM_Grid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillIM_Grid.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.BillIM_Grid.RowHeadersVisible = False
        Me.BillIM_Grid.RowHeadersWidth = 100
        Me.BillIM_Grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BillIM_Grid.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.BillIM_Grid.RowTemplate.Height = 150
        Me.BillIM_Grid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.BillIM_Grid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.BillIM_Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.BillIM_Grid.Size = New System.Drawing.Size(405, 423)
        Me.BillIM_Grid.TabIndex = 580
        Me.BillIM_Grid.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'T_ID_3_CL
        '
        Me.T_ID_3_CL.DataPropertyName = "T_ID"
        Me.T_ID_3_CL.HeaderText = "ر.الآلي"
        Me.T_ID_3_CL.Name = "T_ID_3_CL"
        Me.T_ID_3_CL.ReadOnly = True
        Me.T_ID_3_CL.Visible = False
        '
        'Bill_T_ID_CL
        '
        Me.Bill_T_ID_CL.DataPropertyName = "Bill_T_ID"
        Me.Bill_T_ID_CL.HeaderText = "ر.الألي للفاتورة"
        Me.Bill_T_ID_CL.Name = "Bill_T_ID_CL"
        Me.Bill_T_ID_CL.ReadOnly = True
        Me.Bill_T_ID_CL.Visible = False
        '
        'TB_IM_QTY_CL
        '
        Me.TB_IM_QTY_CL.DataPropertyName = "QTY"
        DataGridViewCellStyle2.Format = "N1"
        Me.TB_IM_QTY_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.TB_IM_QTY_CL.FillWeight = 79.91894!
        Me.TB_IM_QTY_CL.HeaderText = "العدد"
        Me.TB_IM_QTY_CL.Name = "TB_IM_QTY_CL"
        Me.TB_IM_QTY_CL.ReadOnly = True
        '
        'IM_Name_CL
        '
        Me.IM_Name_CL.DataPropertyName = "IM_Name"
        Me.IM_Name_CL.FillWeight = 170.7813!
        Me.IM_Name_CL.HeaderText = "الصنف"
        Me.IM_Name_CL.Name = "IM_Name_CL"
        Me.IM_Name_CL.ReadOnly = True
        '
        'Ptr_ID_CL
        '
        Me.Ptr_ID_CL.DataPropertyName = "Ptr_ID"
        Me.Ptr_ID_CL.HeaderText = "Ptr_ID"
        Me.Ptr_ID_CL.Name = "Ptr_ID_CL"
        Me.Ptr_ID_CL.ReadOnly = True
        Me.Ptr_ID_CL.Visible = False
        '
        'Printer_Name_CL
        '
        Me.Printer_Name_CL.DataPropertyName = "Printer_Path"
        Me.Printer_Name_CL.HeaderText = "Printer_Name"
        Me.Printer_Name_CL.Name = "Printer_Name_CL"
        Me.Printer_Name_CL.ReadOnly = True
        Me.Printer_Name_CL.Visible = False
        '
        'Plus_CL
        '
        Me.Plus_CL.HeaderText = ""
        Me.Plus_CL.Name = "Plus_CL"
        Me.Plus_CL.ReadOnly = True
        Me.Plus_CL.Text = "+"
        Me.Plus_CL.UseColumnTextForButtonValue = True
        '
        'Min_CL
        '
        Me.Min_CL.HeaderText = ""
        Me.Min_CL.Name = "Min_CL"
        Me.Min_CL.ReadOnly = True
        Me.Min_CL.Text = "-"
        Me.Min_CL.UseColumnTextForButtonValue = True
        '
        'Delete
        '
        Me.Delete.HeaderText = ""
        Me.Delete.Name = "Delete"
        Me.Delete.ReadOnly = True
        Me.Delete.Text = "مسح"
        Me.Delete.UseColumnTextForButtonValue = True
        '
        'TB_BillIM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(405, 423)
        Me.Controls.Add(Me.BillIM_Grid)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "TB_BillIM"
        Me.Text = "TB_BillIM"
        CType(Me.BillIM_Grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BillIM_Grid As MetroFramework.Controls.MetroGrid
    Friend WithEvents T_ID_3_CL As DataGridViewTextBoxColumn
    Friend WithEvents Bill_T_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents TB_IM_QTY_CL As DataGridViewTextBoxColumn
    Friend WithEvents IM_Name_CL As DataGridViewTextBoxColumn
    Friend WithEvents Ptr_ID_CL As DataGridViewTextBoxColumn
    Friend WithEvents Printer_Name_CL As DataGridViewTextBoxColumn
    Friend WithEvents Plus_CL As DataGridViewButtonColumn
    Friend WithEvents Min_CL As DataGridViewButtonColumn
    Friend WithEvents Delete As DataGridViewButtonColumn
End Class
