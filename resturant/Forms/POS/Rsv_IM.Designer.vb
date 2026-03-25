<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Rsv_IM
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Rsv_IM))
        Me.Date_F = New System.Windows.Forms.DateTimePicker()
        Me.IM_LB = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.AGMetroGrid = New System.Windows.Forms.DataGridView()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DATE_F_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Duration_By_Day_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Months = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Years = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ADDCatButton = New System.Windows.Forms.Button()
        Me.RemoveCatButton = New System.Windows.Forms.Button()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        CType(Me.AGMetroGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Date_F
        '
        Me.Date_F.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Date_F.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Date_F.CustomFormat = "yyyy-MM-dd  hh:mm:ss tt"
        Me.Date_F.Font = New System.Drawing.Font("Times New Roman", 13.25!, System.Drawing.FontStyle.Bold)
        Me.Date_F.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Date_F.Location = New System.Drawing.Point(1, 39)
        Me.Date_F.Name = "Date_F"
        Me.Date_F.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Date_F.RightToLeftLayout = True
        Me.Date_F.Size = New System.Drawing.Size(588, 28)
        Me.Date_F.TabIndex = 384
        '
        'IM_LB
        '
        Me.IM_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IM_LB.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IM_LB.Location = New System.Drawing.Point(1, 2)
        Me.IM_LB.Name = "IM_LB"
        Me.IM_LB.Size = New System.Drawing.Size(676, 34)
        Me.IM_LB.TabIndex = 385
        Me.IM_LB.Text = "Label1"
        Me.IM_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(592, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 19)
        Me.Label2.TabIndex = 386
        Me.Label2.Text = "مدة الإيجار من"
        '
        'AGMetroGrid
        '
        Me.AGMetroGrid.AllowUserToAddRows = False
        Me.AGMetroGrid.AllowUserToDeleteRows = False
        Me.AGMetroGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.AGMetroGrid.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.AGMetroGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.AGMetroGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.DATE_F_CL, Me.Duration_By_Day_CL, Me.Months, Me.Years})
        Me.AGMetroGrid.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AGMetroGrid.Location = New System.Drawing.Point(50, 69)
        Me.AGMetroGrid.MultiSelect = False
        Me.AGMetroGrid.Name = "AGMetroGrid"
        Me.AGMetroGrid.ReadOnly = True
        Me.AGMetroGrid.RowHeadersVisible = False
        Me.AGMetroGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.AGMetroGrid.RowTemplate.Height = 30
        Me.AGMetroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.AGMetroGrid.Size = New System.Drawing.Size(627, 334)
        Me.AGMetroGrid.TabIndex = 701
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "T_ID"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.Visible = False
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
        Me.ADDCatButton.Location = New System.Drawing.Point(1, 69)
        Me.ADDCatButton.Name = "ADDCatButton"
        Me.ADDCatButton.Size = New System.Drawing.Size(48, 167)
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
        Me.RemoveCatButton.Location = New System.Drawing.Point(1, 236)
        Me.RemoveCatButton.Name = "RemoveCatButton"
        Me.RemoveCatButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RemoveCatButton.Size = New System.Drawing.Size(48, 167)
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
        Me.ExitFormButton.Location = New System.Drawing.Point(1, 404)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(676, 45)
        Me.ExitFormButton.TabIndex = 704
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Rsv_IM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(678, 451)
        Me.ControlBox = False
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.RemoveCatButton)
        Me.Controls.Add(Me.ADDCatButton)
        Me.Controls.Add(Me.AGMetroGrid)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.IM_LB)
        Me.Controls.Add(Me.Date_F)
        Me.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Rsv_IM"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "بيانات الإيجار"
        CType(Me.AGMetroGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Date_F As System.Windows.Forms.DateTimePicker
    Friend WithEvents IM_LB As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents AGMetroGrid As System.Windows.Forms.DataGridView
    Friend WithEvents ADDCatButton As System.Windows.Forms.Button
    Friend WithEvents RemoveCatButton As System.Windows.Forms.Button
    Friend WithEvents T_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DATE_F_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Duration_By_Day_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Months As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Years As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
End Class
