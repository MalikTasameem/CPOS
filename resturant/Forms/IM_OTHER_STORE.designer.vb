<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IM_OTHER_STORE
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
        Me.CMSearchTextBox = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.St_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.item_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IM_NUM_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Barcode_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.U_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.ExitFormButton.Location = New System.Drawing.Point(1, 639)
        Me.ExitFormButton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(1006, 55)
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
        Me.AGMetroGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.St_Name_CL, Me.item_name_CL, Me.IM_NUM_CL, Me.Barcode_CL, Me.U_Name_CL, Me.QTY_CL})
        Me.AGMetroGrid.Cursor = System.Windows.Forms.Cursors.Hand
        Me.AGMetroGrid.Location = New System.Drawing.Point(0, 33)
        Me.AGMetroGrid.MultiSelect = False
        Me.AGMetroGrid.Name = "AGMetroGrid"
        Me.AGMetroGrid.ReadOnly = True
        Me.AGMetroGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AGMetroGrid.RowHeadersVisible = False
        Me.AGMetroGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DeepSkyBlue
        Me.AGMetroGrid.RowTemplate.Height = 30
        Me.AGMetroGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.AGMetroGrid.Size = New System.Drawing.Size(1004, 599)
        Me.AGMetroGrid.TabIndex = 702
        '
        'CMSearchTextBox
        '
        Me.CMSearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CMSearchTextBox.Font = New System.Drawing.Font("Arial", 13.25!)
        Me.CMSearchTextBox.ForeColor = System.Drawing.SystemColors.InfoText
        Me.CMSearchTextBox.Location = New System.Drawing.Point(210, 4)
        Me.CMSearchTextBox.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
        Me.CMSearchTextBox.Name = "CMSearchTextBox"
        Me.CMSearchTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CMSearchTextBox.Size = New System.Drawing.Size(794, 28)
        Me.CMSearchTextBox.TabIndex = 703
        Me.CMSearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(105, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(103, 28)
        Me.Button1.TabIndex = 742
        Me.Button1.Text = "تحديث"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(1, 4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(103, 28)
        Me.Button2.TabIndex = 743
        Me.Button2.Text = "طباعة"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'St_Name_CL
        '
        Me.St_Name_CL.DataPropertyName = "St_Name"
        Me.St_Name_CL.HeaderText = "المخزن"
        Me.St_Name_CL.Name = "St_Name_CL"
        Me.St_Name_CL.ReadOnly = True
        '
        'item_name_CL
        '
        Me.item_name_CL.DataPropertyName = "item_name"
        Me.item_name_CL.HeaderText = "الصنف"
        Me.item_name_CL.Name = "item_name_CL"
        Me.item_name_CL.ReadOnly = True
        '
        'IM_NUM_CL
        '
        Me.IM_NUM_CL.DataPropertyName = "IM_NUM"
        Me.IM_NUM_CL.HeaderText = "رقم الصنف"
        Me.IM_NUM_CL.Name = "IM_NUM_CL"
        Me.IM_NUM_CL.ReadOnly = True
        '
        'Barcode_CL
        '
        Me.Barcode_CL.DataPropertyName = "Barcode"
        Me.Barcode_CL.HeaderText = "الباركود"
        Me.Barcode_CL.Name = "Barcode_CL"
        Me.Barcode_CL.ReadOnly = True
        '
        'U_Name_CL
        '
        Me.U_Name_CL.DataPropertyName = "U_Name"
        Me.U_Name_CL.HeaderText = "الوحدة"
        Me.U_Name_CL.Name = "U_Name_CL"
        Me.U_Name_CL.ReadOnly = True
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "QTY"
        Me.QTY_CL.HeaderText = "الكمية"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'IM_OTHER_STORE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 695)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CMSearchTextBox)
        Me.Controls.Add(Me.AGMetroGrid)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "IM_OTHER_STORE"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "بضاعــة متوفرة في المخازن الأخرى"
        CType(Me.AGMetroGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ExitFormButton As Button
    Friend WithEvents AGMetroGrid As System.Windows.Forms.DataGridView
    Friend WithEvents CMSearchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents St_Name_CL As DataGridViewTextBoxColumn
    Friend WithEvents item_name_CL As DataGridViewTextBoxColumn
    Friend WithEvents IM_NUM_CL As DataGridViewTextBoxColumn
    Friend WithEvents Barcode_CL As DataGridViewTextBoxColumn
    Friend WithEvents U_Name_CL As DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As DataGridViewTextBoxColumn
End Class
