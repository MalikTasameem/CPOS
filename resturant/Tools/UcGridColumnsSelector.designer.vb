<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UcGridColumnsSelector
    Inherits System.Windows.Forms.UserControl

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
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.btnToggle = New System.Windows.Forms.Button()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.btnToggle)
        Me.pnlTop.Controls.Add(Me.lblTitle)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(180, 36)
        Me.pnlTop.TabIndex = 0
        '
        'btnToggle
        '
        Me.btnToggle.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnToggle.Location = New System.Drawing.Point(0, 0)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(36, 34)
        Me.btnToggle.TabIndex = 1
        Me.btnToggle.Text = "▼"
        Me.btnToggle.UseVisualStyleBackColor = True
        '
        'lblTitle
        '
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.Location = New System.Drawing.Point(36, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(142, 34)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "الأعمدة"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'UcGridColumnsSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Name = "UcGridColumnsSelector"
        Me.Size = New System.Drawing.Size(180, 36)
        Me.pnlTop.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlTop As Panel
    Friend WithEvents btnToggle As Button
    Friend WithEvents lblTitle As Label
End Class
'---------------------------------------------------------------------------------------------------------------------










'<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
'Partial Class UcGridColumnsSelector
'    Inherits System.Windows.Forms.UserControl

'    Private components As System.ComponentModel.IContainer

'    <System.Diagnostics.DebuggerNonUserCode()>
'    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
'        Try
'            If disposing AndAlso components IsNot Nothing Then
'                components.Dispose()
'            End If
'        Finally
'            MyBase.Dispose(disposing)
'        End Try
'    End Sub

'    <System.Diagnostics.DebuggerStepThrough()>
'    Private Sub InitializeComponent()
'        Me.pnlTop = New System.Windows.Forms.Panel()
'        Me.btnToggle = New System.Windows.Forms.Button()
'        Me.btnRefresh = New System.Windows.Forms.Button()
'        Me.btnUncheckAll = New System.Windows.Forms.Button()
'        Me.btnCheckAll = New System.Windows.Forms.Button()
'        Me.lblTitle = New System.Windows.Forms.Label()
'        Me.pnlBody = New System.Windows.Forms.Panel()
'        Me.clbColumns = New System.Windows.Forms.CheckedListBox()
'        Me.pnlTop.SuspendLayout()
'        Me.pnlBody.SuspendLayout()
'        Me.SuspendLayout()
'        '
'        'pnlTop
'        '
'        Me.pnlTop.BackColor = System.Drawing.Color.WhiteSmoke
'        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
'        Me.pnlTop.Controls.Add(Me.btnToggle)
'        Me.pnlTop.Controls.Add(Me.btnRefresh)
'        Me.pnlTop.Controls.Add(Me.btnUncheckAll)
'        Me.pnlTop.Controls.Add(Me.btnCheckAll)
'        Me.pnlTop.Controls.Add(Me.lblTitle)
'        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
'        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
'        Me.pnlTop.Name = "pnlTop"
'        Me.pnlTop.Size = New System.Drawing.Size(280, 42)
'        Me.pnlTop.TabIndex = 0
'        '
'        'btnToggle
'        '
'        Me.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
'        Me.btnToggle.Location = New System.Drawing.Point(0, 8)
'        Me.btnToggle.Name = "btnToggle"
'        Me.btnToggle.Size = New System.Drawing.Size(32, 24)
'        Me.btnToggle.TabIndex = 4
'        Me.btnToggle.Text = "▲"
'        Me.btnToggle.UseVisualStyleBackColor = True
'        '
'        'btnRefresh
'        '
'        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup
'        Me.btnRefresh.Location = New System.Drawing.Point(33, 8)
'        Me.btnRefresh.Name = "btnRefresh"
'        Me.btnRefresh.Size = New System.Drawing.Size(50, 24)
'        Me.btnRefresh.TabIndex = 3
'        Me.btnRefresh.Text = "تحديث"
'        Me.btnRefresh.UseVisualStyleBackColor = True
'        '
'        'btnUncheckAll
'        '
'        Me.btnUncheckAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup
'        Me.btnUncheckAll.Location = New System.Drawing.Point(84, 8)
'        Me.btnUncheckAll.Name = "btnUncheckAll"
'        Me.btnUncheckAll.Size = New System.Drawing.Size(68, 24)
'        Me.btnUncheckAll.TabIndex = 2
'        Me.btnUncheckAll.Text = "إلغاء الكل"
'        Me.btnUncheckAll.UseVisualStyleBackColor = True
'        '
'        'btnCheckAll
'        '
'        Me.btnCheckAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup
'        Me.btnCheckAll.Location = New System.Drawing.Point(153, 8)
'        Me.btnCheckAll.Name = "btnCheckAll"
'        Me.btnCheckAll.Size = New System.Drawing.Size(68, 24)
'        Me.btnCheckAll.TabIndex = 1
'        Me.btnCheckAll.Text = "تحديد الكل"
'        Me.btnCheckAll.UseVisualStyleBackColor = True
'        '
'        'lblTitle
'        '
'        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Right
'        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 8.75!, System.Drawing.FontStyle.Bold)
'        Me.lblTitle.Location = New System.Drawing.Point(221, 0)
'        Me.lblTitle.Name = "lblTitle"
'        Me.lblTitle.Size = New System.Drawing.Size(57, 40)
'        Me.lblTitle.TabIndex = 0
'        Me.lblTitle.Text = "الأعمدة"
'        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'        '
'        'pnlBody
'        '
'        Me.pnlBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
'        Me.pnlBody.Controls.Add(Me.clbColumns)
'        Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
'        Me.pnlBody.Location = New System.Drawing.Point(0, 42)
'        Me.pnlBody.Name = "pnlBody"
'        Me.pnlBody.Size = New System.Drawing.Size(280, 218)
'        Me.pnlBody.TabIndex = 1
'        '
'        'clbColumns
'        '
'        Me.clbColumns.CheckOnClick = True
'        Me.clbColumns.Dock = System.Windows.Forms.DockStyle.Fill
'        Me.clbColumns.Font = New System.Drawing.Font("Tahoma", 9.0!)
'        Me.clbColumns.FormattingEnabled = True
'        Me.clbColumns.Location = New System.Drawing.Point(0, 0)
'        Me.clbColumns.Name = "clbColumns"
'        Me.clbColumns.RightToLeft = System.Windows.Forms.RightToLeft.Yes
'        Me.clbColumns.Size = New System.Drawing.Size(278, 216)
'        Me.clbColumns.TabIndex = 0
'        '
'        'UcGridColumnsSelector
'        '
'        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
'        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
'        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
'        Me.Controls.Add(Me.pnlBody)
'        Me.Controls.Add(Me.pnlTop)
'        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
'        Me.Name = "UcGridColumnsSelector"
'        Me.Size = New System.Drawing.Size(280, 260)
'        Me.pnlTop.ResumeLayout(False)
'        Me.pnlBody.ResumeLayout(False)
'        Me.ResumeLayout(False)

'    End Sub

'    Friend WithEvents pnlTop As Panel
'    Friend WithEvents btnToggle As Button
'    Friend WithEvents btnRefresh As Button
'    Friend WithEvents btnUncheckAll As Button
'    Friend WithEvents btnCheckAll As Button
'    Friend WithEvents lblTitle As Label
'    Friend WithEvents pnlBody As Panel
'    Friend WithEvents clbColumns As CheckedListBox

'End Class