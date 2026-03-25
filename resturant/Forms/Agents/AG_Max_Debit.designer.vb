<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AG_Max_Debit
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AG_Max_Debit))
        Me.DataGridViewX = New DevComponents.DotNetBar.Controls.DataGridViewX()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pied_CL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Back_ADD_New_IM_btn = New System.Windows.Forms.Button()
        CType(Me.DataGridViewX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridViewX
        '
        Me.DataGridViewX.AllowUserToAddRows = False
        Me.DataGridViewX.AllowUserToDeleteRows = False
        Me.DataGridViewX.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewX.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridViewX.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewX.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewX.ColumnHeadersHeight = 50
        Me.DataGridViewX.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Pied_CL, Me.Column2})
        Me.DataGridViewX.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewX.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewX.EnableHeadersVisualStyles = False
        Me.DataGridViewX.GridColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.DataGridViewX.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewX.MultiSelect = False
        Me.DataGridViewX.Name = "DataGridViewX"
        Me.DataGridViewX.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewX.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewX.RowTemplate.Height = 35
        Me.DataGridViewX.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewX.Size = New System.Drawing.Size(606, 499)
        Me.DataGridViewX.TabIndex = 0
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "Ag_name"
        Me.Column1.FillWeight = 200.0!
        Me.Column1.HeaderText = "العميــل"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Pied_CL
        '
        Me.Pied_CL.DataPropertyName = "Max_Debit"
        Me.Pied_CL.FillWeight = 190.0!
        Me.Pied_CL.HeaderText = "سقف الدين"
        Me.Pied_CL.Name = "Pied_CL"
        Me.Pied_CL.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "T_Balance"
        Me.Column2.FillWeight = 175.0!
        Me.Column2.HeaderText = "الدين الحالي"
        Me.Column2.MinimumWidth = 100
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Back_ADD_New_IM_btn
        '
        Me.Back_ADD_New_IM_btn.BackColor = System.Drawing.Color.White
        Me.Back_ADD_New_IM_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Back_ADD_New_IM_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Back_ADD_New_IM_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise
        Me.Back_ADD_New_IM_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Back_ADD_New_IM_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Back_ADD_New_IM_btn.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold)
        Me.Back_ADD_New_IM_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Back_ADD_New_IM_btn.Image = Global.resturant.My.Resources.Resources.iconfinder_other_arrow_left_other_glyph_763233
        Me.Back_ADD_New_IM_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Back_ADD_New_IM_btn.Location = New System.Drawing.Point(486, 501)
        Me.Back_ADD_New_IM_btn.Name = "Back_ADD_New_IM_btn"
        Me.Back_ADD_New_IM_btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Back_ADD_New_IM_btn.Size = New System.Drawing.Size(120, 41)
        Me.Back_ADD_New_IM_btn.TabIndex = 671
        Me.Back_ADD_New_IM_btn.TabStop = False
        Me.Back_ADD_New_IM_btn.Text = "عــودة"
        Me.Back_ADD_New_IM_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Back_ADD_New_IM_btn.UseVisualStyleBackColor = False
        '
        'AG_Max_Debit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(606, 543)
        Me.ControlBox = False
        Me.Controls.Add(Me.Back_ADD_New_IM_btn)
        Me.Controls.Add(Me.DataGridViewX)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Name = "AG_Max_Debit"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "قائمة عملاء تجاوزت حساباتهم الحد الأعلى"
        CType(Me.DataGridViewX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents DataGridViewX As DevComponents.DotNetBar.Controls.DataGridViewX
    Friend WithEvents Back_ADD_New_IM_btn As System.Windows.Forms.Button
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pied_CL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
