<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SB_Contents_AGENTS
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SB_Contents_AGENTS))
        Me.IM_LB = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.AGMetroGrid = New System.Windows.Forms.DataGridView()
        Me.ADDCatButton = New System.Windows.Forms.Button()
        Me.RemoveCatButton = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Comision_cm = New System.Windows.Forms.ComboBox()
        Me.AG_Cm = New resturant.FSearch_Filter()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ag_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VALUE_Bercent_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.AGMetroGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IM_LB
        '
        Me.IM_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_LB.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_LB.Location = New System.Drawing.Point(1, 2)
        Me.IM_LB.Name = "IM_LB"
        Me.IM_LB.Size = New System.Drawing.Size(598, 34)
        Me.IM_LB.TabIndex = 385
        Me.IM_LB.Text = "Label1"
        Me.IM_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(537, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 22)
        Me.Label2.TabIndex = 386
        Me.Label2.Text = "النسبـــة"
        '
        'AGMetroGrid
        '
        Me.AGMetroGrid.AllowUserToAddRows = False
        Me.AGMetroGrid.AllowUserToDeleteRows = False
        Me.AGMetroGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.AGMetroGrid.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.AGMetroGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.AGMetroGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.Ag_name_CL, Me.VALUE_Bercent_CL})
        Me.AGMetroGrid.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AGMetroGrid.Location = New System.Drawing.Point(50, 108)
        Me.AGMetroGrid.MultiSelect = False
        Me.AGMetroGrid.Name = "AGMetroGrid"
        Me.AGMetroGrid.ReadOnly = True
        Me.AGMetroGrid.RowHeadersVisible = False
        Me.AGMetroGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.AGMetroGrid.RowTemplate.Height = 30
        Me.AGMetroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.AGMetroGrid.Size = New System.Drawing.Size(549, 270)
        Me.AGMetroGrid.TabIndex = 701
        '
        'ADDCatButton
        '
        Me.ADDCatButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ADDCatButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ADDCatButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ADDCatButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.ADDCatButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ADDCatButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ADDCatButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ADDCatButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ADDCatButton.Image = CType(resources.GetObject("ADDCatButton.Image"), System.Drawing.Image)
        Me.ADDCatButton.Location = New System.Drawing.Point(1, 108)
        Me.ADDCatButton.Name = "ADDCatButton"
        Me.ADDCatButton.Size = New System.Drawing.Size(48, 135)
        Me.ADDCatButton.TabIndex = 702
        Me.ADDCatButton.TabStop = False
        Me.ADDCatButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ADDCatButton.UseVisualStyleBackColor = False
        '
        'RemoveCatButton
        '
        Me.RemoveCatButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.RemoveCatButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.RemoveCatButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.RemoveCatButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.RemoveCatButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.RemoveCatButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.RemoveCatButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.RemoveCatButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.RemoveCatButton.Image = CType(resources.GetObject("RemoveCatButton.Image"), System.Drawing.Image)
        Me.RemoveCatButton.Location = New System.Drawing.Point(1, 243)
        Me.RemoveCatButton.Name = "RemoveCatButton"
        Me.RemoveCatButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RemoveCatButton.Size = New System.Drawing.Size(48, 135)
        Me.RemoveCatButton.TabIndex = 703
        Me.RemoveCatButton.TabStop = False
        Me.RemoveCatButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RemoveCatButton.UseVisualStyleBackColor = False
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(1, 380)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(598, 38)
        Me.ExitFormButton.TabIndex = 704
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(537, 43)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 22)
        Me.Label1.TabIndex = 705
        Me.Label1.Text = "الموظف"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Comision_cm
        '
        Me.Comision_cm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Comision_cm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Comision_cm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Comision_cm.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Comision_cm.FormattingEnabled = True
        Me.Comision_cm.Location = New System.Drawing.Point(1, 74)
        Me.Comision_cm.Name = "Comision_cm"
        Me.Comision_cm.Size = New System.Drawing.Size(530, 30)
        Me.Comision_cm.TabIndex = 707
        '
        'AG_Cm
        '
        Me.AG_Cm.CancelSearchImage = CType(resources.GetObject("AG_Cm.CancelSearchImage"), System.Drawing.Image)
        Me.AG_Cm.Location = New System.Drawing.Point(1, 37)
        Me.AG_Cm.Name = "AG_Cm"
        Me.AG_Cm.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AG_Cm.Size = New System.Drawing.Size(532, 34)
        Me.AG_Cm.SQL_Column = "AG_NAME"
        Me.AG_Cm.SQL_ID = "AG_ID"
        Me.AG_Cm.SQL_IsNumericSearchField = False
        Me.AG_Cm.SQL_ListSize = 200
        Me.AG_Cm.SQL_NumberOfRows = 200
        Me.AG_Cm.SQL_OrderByField = "AG_NAME"
        Me.AG_Cm.SQL_SearchField = "AG_NAME"
        Me.AG_Cm.SQL_Table = "Agents_Emp_V"
        Me.AG_Cm.TabIndex = 706
        Me.AG_Cm.TextMaxLength = 250
        Me.AG_Cm.Textt = ""
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "SB_T_ID"
        Me.T_ID_CL.HeaderText = "T_ID"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        '
        'Ag_name_CL
        '
        Me.Ag_name_CL.DataPropertyName = "AG_NAME"
        Me.Ag_name_CL.HeaderText = "الموظــف"
        Me.Ag_name_CL.Name = "Ag_name_CL"
        Me.Ag_name_CL.ReadOnly = True
        '
        'VALUE_Bercent_CL
        '
        Me.VALUE_Bercent_CL.DataPropertyName = "VALUE_Bercent"
        Me.VALUE_Bercent_CL.HeaderText = "النسبــة"
        Me.VALUE_Bercent_CL.Name = "VALUE_Bercent_CL"
        Me.VALUE_Bercent_CL.ReadOnly = True
        '
        'SB_Contents_AGENTS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(599, 418)
        Me.ControlBox = False
        Me.Controls.Add(Me.Comision_cm)
        Me.Controls.Add(Me.AG_Cm)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.RemoveCatButton)
        Me.Controls.Add(Me.ADDCatButton)
        Me.Controls.Add(Me.AGMetroGrid)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.IM_LB)
        Me.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "SB_Contents_AGENTS"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "عمــــولة موظف للخدمة"
        CType(Me.AGMetroGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents IM_LB As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents AGMetroGrid As System.Windows.Forms.DataGridView
    Friend WithEvents ADDCatButton As System.Windows.Forms.Button
    Friend WithEvents RemoveCatButton As System.Windows.Forms.Button
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents AG_Cm As resturant.FSearch_Filter
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Comision_cm As System.Windows.Forms.ComboBox
    Friend WithEvents T_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Ag_name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VALUE_Bercent_CL As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
