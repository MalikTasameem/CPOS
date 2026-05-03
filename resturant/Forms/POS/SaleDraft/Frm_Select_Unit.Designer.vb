<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Select_Unit
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvUnits = New System.Windows.Forms.DataGridView()
        Me.PanelTop = New System.Windows.Forms.Panel()
        Me.LblTitle = New System.Windows.Forms.Label()
        Me.PanelBottom = New System.Windows.Forms.Panel()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        CType(Me.dgvUnits, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelTop.SuspendLayout()
        Me.PanelBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvUnits
        '
        Me.dgvUnits.AllowUserToAddRows = False
        Me.dgvUnits.AllowUserToDeleteRows = False
        Me.dgvUnits.BackgroundColor = System.Drawing.Color.White
        Me.dgvUnits.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvUnits.ColumnHeadersHeight = 35
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 15.75!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvUnits.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvUnits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvUnits.Location = New System.Drawing.Point(0, 55)
        Me.dgvUnits.MultiSelect = False
        Me.dgvUnits.Name = "dgvUnits"
        Me.dgvUnits.ReadOnly = True
        Me.dgvUnits.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 15.75!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvUnits.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvUnits.RowHeadersVisible = False
        Me.dgvUnits.RowHeadersWidth = 55
        Me.dgvUnits.RowTemplate.Height = 32
        Me.dgvUnits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUnits.Size = New System.Drawing.Size(760, 365)
        Me.dgvUnits.TabIndex = 0
        '
        'PanelTop
        '
        Me.PanelTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.PanelTop.Controls.Add(Me.LblTitle)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(760, 55)
        Me.PanelTop.TabIndex = 1
        '
        'LblTitle
        '
        Me.LblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblTitle.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.LblTitle.ForeColor = System.Drawing.Color.White
        Me.LblTitle.Location = New System.Drawing.Point(0, 0)
        Me.LblTitle.Name = "LblTitle"
        Me.LblTitle.Size = New System.Drawing.Size(760, 55)
        Me.LblTitle.TabIndex = 0
        Me.LblTitle.Text = "اختيار وحدة الصنف"
        Me.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PanelBottom
        '
        Me.PanelBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.PanelBottom.Controls.Add(Me.btnSelect)
        Me.PanelBottom.Controls.Add(Me.btnClose)
        Me.PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelBottom.Location = New System.Drawing.Point(0, 420)
        Me.PanelBottom.Name = "PanelBottom"
        Me.PanelBottom.Size = New System.Drawing.Size(760, 70)
        Me.PanelBottom.TabIndex = 2
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.btnSelect.FlatAppearance.BorderSize = 0
        Me.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelect.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnSelect.ForeColor = System.Drawing.Color.White
        Me.btnSelect.Location = New System.Drawing.Point(608, 15)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(130, 40)
        Me.btnSelect.TabIndex = 0
        Me.btnSelect.Text = "اختيار"
        Me.btnSelect.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(462, 15)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(130, 40)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "إغلاق"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Frm_Select_Unit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(760, 490)
        Me.Controls.Add(Me.dgvUnits)
        Me.Controls.Add(Me.PanelBottom)
        Me.Controls.Add(Me.PanelTop)
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_Select_Unit"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "اختيار وحدة الصنف"
        CType(Me.dgvUnits, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelTop.ResumeLayout(False)
        Me.PanelBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvUnits As System.Windows.Forms.DataGridView
    Friend WithEvents PanelTop As System.Windows.Forms.Panel
    Friend WithEvents LblTitle As System.Windows.Forms.Label
    Friend WithEvents PanelBottom As System.Windows.Forms.Panel
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button

End Class