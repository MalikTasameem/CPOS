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
        Me.UnitsFlowPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.EmptyUnits_LB = New System.Windows.Forms.Label()
        Me.PanelTop = New System.Windows.Forms.Panel()
        Me.LblTitle = New System.Windows.Forms.Label()
        Me.PanelBottom = New System.Windows.Forms.Panel()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.UnitsFlowPanel.SuspendLayout()
        Me.PanelTop.SuspendLayout()
        Me.PanelBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'UnitsFlowPanel
        '
        Me.UnitsFlowPanel.AutoScroll = True
        Me.UnitsFlowPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.UnitsFlowPanel.Controls.Add(Me.EmptyUnits_LB)
        Me.UnitsFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UnitsFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.UnitsFlowPanel.Location = New System.Drawing.Point(0, 55)
        Me.UnitsFlowPanel.Name = "UnitsFlowPanel"
        Me.UnitsFlowPanel.Padding = New System.Windows.Forms.Padding(14)
        Me.UnitsFlowPanel.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.UnitsFlowPanel.Size = New System.Drawing.Size(820, 405)
        Me.UnitsFlowPanel.TabIndex = 0
        '
        'EmptyUnits_LB
        '
        Me.EmptyUnits_LB.Font = New System.Drawing.Font("Segoe UI Semibold", 13.0!, System.Drawing.FontStyle.Bold)
        Me.EmptyUnits_LB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.EmptyUnits_LB.Location = New System.Drawing.Point(17, 14)
        Me.EmptyUnits_LB.Name = "EmptyUnits_LB"
        Me.EmptyUnits_LB.Size = New System.Drawing.Size(760, 320)
        Me.EmptyUnits_LB.TabIndex = 0
        Me.EmptyUnits_LB.Text = "لا توجد وحدات أخرى لهذا الصنف"
        Me.EmptyUnits_LB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.EmptyUnits_LB.Visible = False
        '
        'PanelTop
        '
        Me.PanelTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.PanelTop.Controls.Add(Me.LblTitle)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(820, 55)
        Me.PanelTop.TabIndex = 1
        '
        'LblTitle
        '
        Me.LblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblTitle.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.LblTitle.ForeColor = System.Drawing.Color.White
        Me.LblTitle.Location = New System.Drawing.Point(0, 0)
        Me.LblTitle.Name = "LblTitle"
        Me.LblTitle.Size = New System.Drawing.Size(820, 55)
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
        Me.PanelBottom.Location = New System.Drawing.Point(0, 460)
        Me.PanelBottom.Name = "PanelBottom"
        Me.PanelBottom.Size = New System.Drawing.Size(820, 70)
        Me.PanelBottom.TabIndex = 2
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.btnSelect.FlatAppearance.BorderSize = 0
        Me.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelect.Font = New System.Drawing.Font("Segoe UI Semibold", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelect.ForeColor = System.Drawing.Color.White
        Me.btnSelect.Location = New System.Drawing.Point(3, 6)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(677, 52)
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
        Me.btnClose.Location = New System.Drawing.Point(686, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(130, 52)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "إغلاق"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Frm_Select_Unit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(820, 530)
        Me.Controls.Add(Me.UnitsFlowPanel)
        Me.Controls.Add(Me.PanelBottom)
        Me.Controls.Add(Me.PanelTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_Select_Unit"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "اختيار وحدة الصنف"
        Me.UnitsFlowPanel.ResumeLayout(False)
        Me.PanelTop.ResumeLayout(False)
        Me.PanelBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents UnitsFlowPanel As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents EmptyUnits_LB As System.Windows.Forms.Label
    Friend WithEvents PanelTop As System.Windows.Forms.Panel
    Friend WithEvents LblTitle As System.Windows.Forms.Label
    Friend WithEvents PanelBottom As System.Windows.Forms.Panel
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button

End Class
