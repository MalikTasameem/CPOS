<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TablesCard
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TablesCard))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TablesNumButton = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TableNameTxt = New System.Windows.Forms.TextBox()
        Me.OptiontPanel = New System.Windows.Forms.Panel()
        Me.is_Auto_Pied_CB = New System.Windows.Forms.CheckBox()
        Me.CP_Bill_Screen_GroupBox = New System.Windows.Forms.GroupBox()
        Me.Flate_cmb = New System.Windows.Forms.ComboBox()
        Me.Total_CB = New System.Windows.Forms.CheckBox()
        Me.TBPanel = New System.Windows.Forms.Panel()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.TBGrid = New MetroFramework.Controls.MetroGrid()
        Me.TB_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Flate_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.T_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Flate_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.is_Cash_CL = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ADD_IM_CL = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.OptiontPanel.SuspendLayout()
        Me.CP_Bill_Screen_GroupBox.SuspendLayout()
        Me.TBPanel.SuspendLayout()
        CType(Me.TBGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TablesNumButton
        '
        Me.TablesNumButton.BackColor = System.Drawing.Color.White
        Me.TablesNumButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.TablesNumButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TablesNumButton.Enabled = False
        Me.TablesNumButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.TablesNumButton.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.TablesNumButton.ForeColor = System.Drawing.Color.Black
        Me.TablesNumButton.Image = Global.resturant.My.Resources.Resources.iconfinder_plus_1282963
        Me.TablesNumButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TablesNumButton.Location = New System.Drawing.Point(4, 19)
        Me.TablesNumButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TablesNumButton.Name = "TablesNumButton"
        Me.TablesNumButton.Size = New System.Drawing.Size(131, 33)
        Me.TablesNumButton.TabIndex = 350
        Me.TablesNumButton.Text = "أضف الطاولة"
        Me.TablesNumButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.TablesNumButton, "إضافة الطاولات")
        Me.TablesNumButton.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Location = New System.Drawing.Point(137, 20)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(38, 33)
        Me.Button1.TabIndex = 499
        Me.Button1.Text = "...."
        Me.ToolTip1.SetToolTip(Me.Button1, "إضافة الطاولات")
        Me.Button1.UseVisualStyleBackColor = False
        '
        'TableNameTxt
        '
        Me.TableNameTxt.Font = New System.Drawing.Font("Times New Roman", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TableNameTxt.Location = New System.Drawing.Point(499, 18)
        Me.TableNameTxt.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TableNameTxt.Name = "TableNameTxt"
        Me.TableNameTxt.Size = New System.Drawing.Size(105, 29)
        Me.TableNameTxt.TabIndex = 349
        Me.TableNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'OptiontPanel
        '
        Me.OptiontPanel.Controls.Add(Me.is_Auto_Pied_CB)
        Me.OptiontPanel.Controls.Add(Me.Button1)
        Me.OptiontPanel.Controls.Add(Me.CP_Bill_Screen_GroupBox)
        Me.OptiontPanel.Controls.Add(Me.Total_CB)
        Me.OptiontPanel.Controls.Add(Me.TablesNumButton)
        Me.OptiontPanel.Controls.Add(Me.TableNameTxt)
        Me.OptiontPanel.Location = New System.Drawing.Point(4, 1)
        Me.OptiontPanel.Name = "OptiontPanel"
        Me.OptiontPanel.Size = New System.Drawing.Size(714, 62)
        Me.OptiontPanel.TabIndex = 348
        '
        'is_Auto_Pied_CB
        '
        Me.is_Auto_Pied_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.is_Auto_Pied_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 10.25!, System.Drawing.FontStyle.Bold)
        Me.is_Auto_Pied_CB.Location = New System.Drawing.Point(391, 14)
        Me.is_Auto_Pied_CB.Name = "is_Auto_Pied_CB"
        Me.is_Auto_Pied_CB.Size = New System.Drawing.Size(102, 37)
        Me.is_Auto_Pied_CB.TabIndex = 500
        Me.is_Auto_Pied_CB.Text = "سداد تلقائي"
        Me.is_Auto_Pied_CB.UseVisualStyleBackColor = True
        '
        'CP_Bill_Screen_GroupBox
        '
        Me.CP_Bill_Screen_GroupBox.Controls.Add(Me.Flate_cmb)
        Me.CP_Bill_Screen_GroupBox.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CP_Bill_Screen_GroupBox.Location = New System.Drawing.Point(179, 2)
        Me.CP_Bill_Screen_GroupBox.Name = "CP_Bill_Screen_GroupBox"
        Me.CP_Bill_Screen_GroupBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CP_Bill_Screen_GroupBox.Size = New System.Drawing.Size(204, 53)
        Me.CP_Bill_Screen_GroupBox.TabIndex = 498
        Me.CP_Bill_Screen_GroupBox.TabStop = False
        Me.CP_Bill_Screen_GroupBox.Text = "مكان الطاولات"
        '
        'Flate_cmb
        '
        Me.Flate_cmb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Flate_cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Flate_cmb.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Flate_cmb.FormattingEnabled = True
        Me.Flate_cmb.Location = New System.Drawing.Point(6, 20)
        Me.Flate_cmb.Name = "Flate_cmb"
        Me.Flate_cmb.Size = New System.Drawing.Size(189, 25)
        Me.Flate_cmb.TabIndex = 0
        '
        'Total_CB
        '
        Me.Total_CB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Total_CB.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Total_CB.Location = New System.Drawing.Point(608, 15)
        Me.Total_CB.Name = "Total_CB"
        Me.Total_CB.Size = New System.Drawing.Size(102, 37)
        Me.Total_CB.TabIndex = 0
        Me.Total_CB.Text = "قائمة كاملة"
        Me.Total_CB.UseVisualStyleBackColor = True
        '
        'TBPanel
        '
        Me.TBPanel.AutoScroll = True
        Me.TBPanel.BackColor = System.Drawing.Color.Transparent
        Me.TBPanel.Controls.Add(Me.ExitFormButton)
        Me.TBPanel.Controls.Add(Me.TBGrid)
        Me.TBPanel.Location = New System.Drawing.Point(4, 68)
        Me.TBPanel.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TBPanel.Name = "TBPanel"
        Me.TBPanel.Size = New System.Drawing.Size(714, 358)
        Me.TBPanel.TabIndex = 346
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.Location = New System.Drawing.Point(611, 319)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(101, 37)
        Me.ExitFormButton.TabIndex = 581
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'TBGrid
        '
        Me.TBGrid.AllowUserToAddRows = False
        Me.TBGrid.AllowUserToDeleteRows = False
        Me.TBGrid.AllowUserToResizeRows = False
        Me.TBGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.TBGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.TBGrid.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TBGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TBGrid.CausesValidation = False
        Me.TBGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.TBGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TBGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.TBGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TBGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TB_ID_CL, Me.Flate_ID_CL, Me.T_Name_CL, Me.Flate_Name_CL, Me.is_Cash_CL, Me.ADD_IM_CL})
        Me.TBGrid.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TBGrid.DefaultCellStyle = DataGridViewCellStyle3
        Me.TBGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.TBGrid.EnableHeadersVisualStyles = False
        Me.TBGrid.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.TBGrid.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TBGrid.Location = New System.Drawing.Point(4, 3)
        Me.TBGrid.MultiSelect = False
        Me.TBGrid.Name = "TBGrid"
        Me.TBGrid.ReadOnly = True
        Me.TBGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.TBGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TBGrid.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.TBGrid.RowHeadersVisible = False
        Me.TBGrid.RowHeadersWidth = 100
        Me.TBGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TBGrid.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.TBGrid.RowTemplate.Height = 250
        Me.TBGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TBGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TBGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.TBGrid.Size = New System.Drawing.Size(706, 314)
        Me.TBGrid.TabIndex = 580
        Me.TBGrid.Theme = MetroFramework.MetroThemeStyle.Light
        '
        'TB_ID_CL
        '
        Me.TB_ID_CL.DataPropertyName = "TB_ID"
        Me.TB_ID_CL.HeaderText = "ر.الآلي"
        Me.TB_ID_CL.Name = "TB_ID_CL"
        Me.TB_ID_CL.ReadOnly = True
        Me.TB_ID_CL.Visible = False
        '
        'Flate_ID_CL
        '
        Me.Flate_ID_CL.DataPropertyName = "Flate_ID"
        Me.Flate_ID_CL.HeaderText = "ر.المكان"
        Me.Flate_ID_CL.Name = "Flate_ID_CL"
        Me.Flate_ID_CL.ReadOnly = True
        Me.Flate_ID_CL.Visible = False
        '
        'T_Name_CL
        '
        Me.T_Name_CL.DataPropertyName = "T_Name"
        Me.T_Name_CL.FillWeight = 79.91894!
        Me.T_Name_CL.HeaderText = "الطاولة"
        Me.T_Name_CL.Name = "T_Name_CL"
        Me.T_Name_CL.ReadOnly = True
        '
        'Flate_Name_CL
        '
        Me.Flate_Name_CL.DataPropertyName = "Flate_Name"
        Me.Flate_Name_CL.HeaderText = "المكان"
        Me.Flate_Name_CL.Name = "Flate_Name_CL"
        Me.Flate_Name_CL.ReadOnly = True
        '
        'is_Cash_CL
        '
        Me.is_Cash_CL.DataPropertyName = "is_Cash"
        Me.is_Cash_CL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.is_Cash_CL.HeaderText = "سداد تلقائي"
        Me.is_Cash_CL.Name = "is_Cash_CL"
        Me.is_Cash_CL.ReadOnly = True
        '
        'ADD_IM_CL
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADD_IM_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.ADD_IM_CL.HeaderText = ""
        Me.ADD_IM_CL.Name = "ADD_IM_CL"
        Me.ADD_IM_CL.ReadOnly = True
        Me.ADD_IM_CL.Text = "حذف"
        Me.ADD_IM_CL.UseColumnTextForButtonValue = True
        '
        'TablesCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(722, 426)
        Me.ControlBox = False
        Me.Controls.Add(Me.OptiontPanel)
        Me.Controls.Add(Me.TBPanel)
        Me.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "TablesCard"
        Me.Text = "إدارة الطاولات"
        Me.OptiontPanel.ResumeLayout(False)
        Me.OptiontPanel.PerformLayout()
        Me.CP_Bill_Screen_GroupBox.ResumeLayout(False)
        Me.TBPanel.ResumeLayout(False)
        CType(Me.TBGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents TablesNumButton As System.Windows.Forms.Button
    Friend WithEvents TableNameTxt As System.Windows.Forms.TextBox
    Friend WithEvents OptiontPanel As System.Windows.Forms.Panel
    Friend WithEvents TBPanel As System.Windows.Forms.Panel
    Friend WithEvents Total_CB As System.Windows.Forms.CheckBox
    Friend WithEvents CP_Bill_Screen_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Flate_cmb As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TBGrid As MetroFramework.Controls.MetroGrid
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents is_Auto_Pied_CB As System.Windows.Forms.CheckBox
    Friend WithEvents TB_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Flate_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents T_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Flate_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents is_Cash_CL As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ADD_IM_CL As System.Windows.Forms.DataGridViewButtonColumn
End Class
