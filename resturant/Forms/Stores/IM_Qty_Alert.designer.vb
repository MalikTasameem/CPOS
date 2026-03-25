<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IM_Qty_Alert
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IM_Qty_Alert))
        Me.Descrip_Lb = New System.Windows.Forms.Label()
        Me.DataGridViewX = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.St_Name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.item_name_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unit_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Alert_Qty_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QTY_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrintButton = New System.Windows.Forms.Button()
        Me.CMSearchTextBox = New System.Windows.Forms.TextBox()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        CType(Me.DataGridViewX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Descrip_Lb
        '
        Me.Descrip_Lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Descrip_Lb.Font = New System.Drawing.Font("JF Flat", 15.5!, System.Drawing.FontStyle.Bold)
        Me.Descrip_Lb.ForeColor = System.Drawing.Color.Black
        Me.Descrip_Lb.Location = New System.Drawing.Point(2, 1)
        Me.Descrip_Lb.Name = "Descrip_Lb"
        Me.Descrip_Lb.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Descrip_Lb.Size = New System.Drawing.Size(769, 43)
        Me.Descrip_Lb.TabIndex = 658
        Me.Descrip_Lb.Text = "اصناف تنتهي صلاحيتها قريبا"
        Me.Descrip_Lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DataGridViewX
        '
        Me.DataGridViewX.AllowUserToAddRows = False
        Me.DataGridViewX.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info
        Me.DataGridViewX.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewX.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewX.BackgroundColor = System.Drawing.Color.SeaShell
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewX.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.St_Name_CL, Me.item_name_CL, Me.Unit_CL, Me.Alert_Qty_CL, Me.QTY_CL})
        Me.DataGridViewX.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewX.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewX.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DataGridViewX.Location = New System.Drawing.Point(2, 77)
        Me.DataGridViewX.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.DataGridViewX.MultiSelect = False
        Me.DataGridViewX.Name = "DataGridViewX"
        Me.DataGridViewX.ReadOnly = True
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridViewX.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridViewX.RowTemplate.Height = 35
        Me.DataGridViewX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewX.Size = New System.Drawing.Size(769, 500)
        Me.DataGridViewX.TabIndex = 649
        '
        'St_Name_CL
        '
        Me.St_Name_CL.DataPropertyName = "St_Name"
        Me.St_Name_CL.FillWeight = 98.06538!
        Me.St_Name_CL.HeaderText = "المخزن"
        Me.St_Name_CL.Name = "St_Name_CL"
        Me.St_Name_CL.ReadOnly = True
        '
        'item_name_CL
        '
        Me.item_name_CL.DataPropertyName = "item_name"
        Me.item_name_CL.FillWeight = 117.2305!
        Me.item_name_CL.HeaderText = "الصنف"
        Me.item_name_CL.MinimumWidth = 100
        Me.item_name_CL.Name = "item_name_CL"
        Me.item_name_CL.ReadOnly = True
        Me.item_name_CL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.item_name_CL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Unit_CL
        '
        Me.Unit_CL.DataPropertyName = "U_Name"
        Me.Unit_CL.FillWeight = 59.73525!
        Me.Unit_CL.HeaderText = "الوحدة"
        Me.Unit_CL.Name = "Unit_CL"
        Me.Unit_CL.ReadOnly = True
        '
        'Alert_Qty_CL
        '
        Me.Alert_Qty_CL.DataPropertyName = "Alert_Qty"
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.Alert_Qty_CL.DefaultCellStyle = DataGridViewCellStyle3
        Me.Alert_Qty_CL.FillWeight = 98.06538!
        Me.Alert_Qty_CL.HeaderText = "كمية التنبيه"
        Me.Alert_Qty_CL.Name = "Alert_Qty_CL"
        Me.Alert_Qty_CL.ReadOnly = True
        '
        'QTY_CL
        '
        Me.QTY_CL.DataPropertyName = "ST_QTY"
        DataGridViewCellStyle4.Format = "N2"
        Me.QTY_CL.DefaultCellStyle = DataGridViewCellStyle4
        Me.QTY_CL.FillWeight = 75.0!
        Me.QTY_CL.HeaderText = "الكمية الحالية"
        Me.QTY_CL.Name = "QTY_CL"
        Me.QTY_CL.ReadOnly = True
        '
        'PrintButton
        '
        Me.PrintButton.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PrintButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PrintButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PrintButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.PrintButton.Font = New System.Drawing.Font("JF Flat", 10.75!, System.Drawing.FontStyle.Bold)
        Me.PrintButton.ForeColor = System.Drawing.Color.Black
        Me.PrintButton.Image = Global.resturant.My.Resources.Resources.if_icon_124_printer_text_314703
        Me.PrintButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.PrintButton.Location = New System.Drawing.Point(105, 579)
        Me.PrintButton.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.PrintButton.Name = "PrintButton"
        Me.PrintButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PrintButton.Size = New System.Drawing.Size(104, 40)
        Me.PrintButton.TabIndex = 652
        Me.PrintButton.Text = "طباعة"
        Me.PrintButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.PrintButton.UseVisualStyleBackColor = False
        Me.PrintButton.Visible = False
        '
        'CMSearchTextBox
        '
        Me.CMSearchTextBox.Font = New System.Drawing.Font("JF Flat", 10.0!)
        Me.CMSearchTextBox.ForeColor = System.Drawing.SystemColors.InfoText
        Me.CMSearchTextBox.Location = New System.Drawing.Point(2, 45)
        Me.CMSearchTextBox.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.CMSearchTextBox.Name = "CMSearchTextBox"
        Me.CMSearchTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CMSearchTextBox.Size = New System.Drawing.Size(769, 31)
        Me.CMSearchTextBox.TabIndex = 650
        Me.CMSearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.IndianRed
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ExitFormButton.Font = New System.Drawing.Font("JF Flat", 10.75!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ExitFormButton.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.ExitFormButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ExitFormButton.Location = New System.Drawing.Point(2, 579)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(101, 40)
        Me.ExitFormButton.TabIndex = 667
        Me.ExitFormButton.Text = "خروج"
        Me.ExitFormButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'IM_Qty_Alert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(771, 620)
        Me.Controls.Add(Me.ExitFormButton)
        Me.Controls.Add(Me.Descrip_Lb)
        Me.Controls.Add(Me.DataGridViewX)
        Me.Controls.Add(Me.PrintButton)
        Me.Controls.Add(Me.CMSearchTextBox)
        Me.Font = New System.Drawing.Font("JF Flat", 9.0!, System.Drawing.FontStyle.Bold)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Name = "IM_Qty_Alert"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.DataGridViewX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Descrip_Lb As System.Windows.Forms.Label
    Public WithEvents DataGridViewX As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents PrintButton As System.Windows.Forms.Button
    Friend WithEvents CMSearchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents St_Name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_name_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Unit_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Alert_Qty_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button
End Class
