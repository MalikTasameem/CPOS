<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Rsv_IM_VIEW
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
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.AGMetroGrid = New System.Windows.Forms.DataGridView()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.item_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ag_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SB_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DATE_F_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Duration_By_Day_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Months = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Years = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Print_btn = New System.Windows.Forms.Button()
        CType(Me.AGMetroGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.ExitFormButton.Location = New System.Drawing.Point(70, 533)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(896, 55)
        Me.ExitFormButton.TabIndex = 675
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'AGMetroGrid
        '
        Me.AGMetroGrid.AllowUserToAddRows = False
        Me.AGMetroGrid.AllowUserToDeleteRows = False
        Me.AGMetroGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.AGMetroGrid.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.AGMetroGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.AGMetroGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.item_name_CL, Me.Ag_name_CL, Me.SB_ID_CL, Me.DATE_F_CL, Me.Duration_By_Day_CL, Me.Months, Me.Years})
        Me.AGMetroGrid.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AGMetroGrid.Location = New System.Drawing.Point(1, 1)
        Me.AGMetroGrid.MultiSelect = False
        Me.AGMetroGrid.Name = "AGMetroGrid"
        Me.AGMetroGrid.ReadOnly = True
        Me.AGMetroGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AGMetroGrid.RowHeadersVisible = False
        Me.AGMetroGrid.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.AGMetroGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.AGMetroGrid.RowTemplate.Height = 30
        Me.AGMetroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.AGMetroGrid.Size = New System.Drawing.Size(965, 531)
        Me.AGMetroGrid.TabIndex = 702
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "T_ID"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
        '
        'item_name_CL
        '
        Me.item_name_CL.DataPropertyName = "item_name"
        Me.item_name_CL.HeaderText = "الصنف"
        Me.item_name_CL.Name = "item_name_CL"
        Me.item_name_CL.ReadOnly = True
        '
        'Ag_name_CL
        '
        Me.Ag_name_CL.DataPropertyName = "Ag_name"
        Me.Ag_name_CL.HeaderText = "الزبون"
        Me.Ag_name_CL.Name = "Ag_name_CL"
        Me.Ag_name_CL.ReadOnly = True
        '
        'SB_ID_CL
        '
        Me.SB_ID_CL.DataPropertyName = "SB_ID"
        Me.SB_ID_CL.HeaderText = "رقم الفاتورة"
        Me.SB_ID_CL.Name = "SB_ID_CL"
        Me.SB_ID_CL.ReadOnly = True
        '
        'DATE_F_CL
        '
        Me.DATE_F_CL.DataPropertyName = "DATE_F"
        Me.DATE_F_CL.HeaderText = "تاريخ البدء"
        Me.DATE_F_CL.Name = "DATE_F_CL"
        Me.DATE_F_CL.ReadOnly = True
        '
        'Duration_By_Day_CL
        '
        Me.Duration_By_Day_CL.DataPropertyName = "DAYS"
        Me.Duration_By_Day_CL.HeaderText = "الفترة باليوم"
        Me.Duration_By_Day_CL.Name = "Duration_By_Day_CL"
        Me.Duration_By_Day_CL.ReadOnly = True
        '
        'Months
        '
        Me.Months.DataPropertyName = "Months"
        Me.Months.HeaderText = "الفترة بالشهر"
        Me.Months.Name = "Months"
        Me.Months.ReadOnly = True
        '
        'Years
        '
        Me.Years.DataPropertyName = "Years"
        Me.Years.HeaderText = "الفترة بالسنة"
        Me.Years.Name = "Years"
        Me.Years.ReadOnly = True
        '
        'Print_btn
        '
        Me.Print_btn.BackColor = System.Drawing.Color.White
        Me.Print_btn.BackgroundImage = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.Print_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Print_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Print_btn.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Print_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Print_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Print_btn.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Print_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Print_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Print_btn.Location = New System.Drawing.Point(1, 533)
        Me.Print_btn.Name = "Print_btn"
        Me.Print_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Print_btn.Size = New System.Drawing.Size(67, 55)
        Me.Print_btn.TabIndex = 715
        Me.Print_btn.TabStop = False
        Me.Print_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Print_btn.UseVisualStyleBackColor = False
        '
        'Rsv_IM_VIEW
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(967, 589)
        Me.ControlBox = False
        Me.Controls.Add(Me.Print_btn)
        Me.Controls.Add(Me.AGMetroGrid)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Rsv_IM_VIEW"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "أصناف مستأجرة"
        CType(Me.AGMetroGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ExitFormButton As Button
    Friend WithEvents AGMetroGrid As System.Windows.Forms.DataGridView
    Friend WithEvents T_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Ag_name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SB_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DATE_F_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Duration_By_Day_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Months As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Years As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Print_btn As System.Windows.Forms.Button
End Class
