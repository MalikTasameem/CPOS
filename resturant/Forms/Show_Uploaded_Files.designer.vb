<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Show_Uploaded_Files
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Show_Uploaded_Files))
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.MetroGrid = New System.Windows.Forms.DataGridView()
        Me.T_ID_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GM_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.P_NOTES_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.MetroGrid, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ExitFormButton.Location = New System.Drawing.Point(510, 544)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(105, 38)
        Me.ExitFormButton.TabIndex = 666
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'MetroGrid
        '
        Me.MetroGrid.AllowUserToAddRows = False
        Me.MetroGrid.AllowUserToDeleteRows = False
        Me.MetroGrid.AllowUserToResizeRows = False
        Me.MetroGrid.BackgroundColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MetroGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.MetroGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MetroGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.T_ID_CL, Me.GM_Name_CL, Me.IM_Name_CL, Me.P_NOTES_CL})
        Me.MetroGrid.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MetroGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2
        Me.MetroGrid.Location = New System.Drawing.Point(1, 1)
        Me.MetroGrid.Margin = New System.Windows.Forms.Padding(2)
        Me.MetroGrid.MultiSelect = False
        Me.MetroGrid.Name = "MetroGrid"
        Me.MetroGrid.ReadOnly = True
        Me.MetroGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.MetroGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.MetroGrid.RowTemplate.Height = 25
        Me.MetroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.MetroGrid.Size = New System.Drawing.Size(614, 538)
        Me.MetroGrid.TabIndex = 1122
        '
        'T_ID_CL
        '
        Me.T_ID_CL.DataPropertyName = "T_ID"
        Me.T_ID_CL.HeaderText = "T_ID"
        Me.T_ID_CL.Name = "T_ID_CL"
        Me.T_ID_CL.ReadOnly = True
        Me.T_ID_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.T_ID_CL.Visible = False
        '
        'GM_Name_CL
        '
        Me.GM_Name_CL.DataPropertyName = "IDX"
        Me.GM_Name_CL.FillWeight = 5.0!
        Me.GM_Name_CL.HeaderText = "ت"
        Me.GM_Name_CL.Name = "GM_Name_CL"
        Me.GM_Name_CL.ReadOnly = True
        Me.GM_Name_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.GM_Name_CL.Width = 5
        '
        'IM_Name_CL
        '
        Me.IM_Name_CL.DataPropertyName = "Date"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.IM_Name_CL.DefaultCellStyle = DataGridViewCellStyle2
        Me.IM_Name_CL.FillWeight = 200.0!
        Me.IM_Name_CL.HeaderText = "تاريخ"
        Me.IM_Name_CL.Name = "IM_Name_CL"
        Me.IM_Name_CL.ReadOnly = True
        Me.IM_Name_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.IM_Name_CL.Width = 200
        '
        'P_NOTES_CL
        '
        Me.P_NOTES_CL.DataPropertyName = "DOC_NAME"
        Me.P_NOTES_CL.FillWeight = 360.0!
        Me.P_NOTES_CL.HeaderText = "العنـــــــوان"
        Me.P_NOTES_CL.Name = "P_NOTES_CL"
        Me.P_NOTES_CL.ReadOnly = True
        Me.P_NOTES_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.P_NOTES_CL.Width = 360
        '
        'Show_Uploaded_Files
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(618, 584)
        Me.ControlBox = False
        Me.Controls.Add(Me.MetroGrid)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Show_Uploaded_Files"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "قائمة الملفات المرفوعة"
        CType(Me.MetroGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
    Friend WithEvents MetroGrid As System.Windows.Forms.DataGridView
    Friend WithEvents T_ID_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GM_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IM_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents P_NOTES_CL As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
