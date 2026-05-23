<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSystemAccountLinksLog
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblAccountName = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.chkShowAll = New System.Windows.Forms.CheckBox()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.dgvLog = New System.Windows.Forms.DataGridView()
        Me.pnlFooter = New System.Windows.Forms.Panel()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        CType(Me.dgvLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFooter.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(72, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblAccountName)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Padding = New System.Windows.Forms.Padding(12, 8, 12, 8)
        Me.pnlHeader.Size = New System.Drawing.Size(1050, 78)
        Me.pnlHeader.TabIndex = 0
        '
        'lblAccountName
        '
        Me.lblAccountName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblAccountName.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblAccountName.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblAccountName.Location = New System.Drawing.Point(12, 42)
        Me.lblAccountName.Name = "lblAccountName"
        Me.lblAccountName.Size = New System.Drawing.Size(1026, 28)
        Me.lblAccountName.TabIndex = 1
        Me.lblAccountName.Text = "الحساب الأساسي"
        Me.lblAccountName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTitle
        '
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(12, 8)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(1026, 34)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "سجل تعديلات ربط الحسابات الأساسية"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlButtons
        '
        Me.pnlButtons.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlButtons.Controls.Add(Me.btnClose)
        Me.pnlButtons.Controls.Add(Me.btnRefresh)
        Me.pnlButtons.Controls.Add(Me.chkShowAll)
        Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlButtons.Location = New System.Drawing.Point(0, 78)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlButtons.Size = New System.Drawing.Size(1050, 58)
        Me.pnlButtons.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.Location = New System.Drawing.Point(10, 10)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(120, 38)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "إغلاق"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.Location = New System.Drawing.Point(710, 10)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(140, 38)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.Text = "تحديث"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'chkShowAll
        '
        Me.chkShowAll.Dock = System.Windows.Forms.DockStyle.Right
        Me.chkShowAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkShowAll.Location = New System.Drawing.Point(850, 10)
        Me.chkShowAll.Name = "chkShowAll"
        Me.chkShowAll.Size = New System.Drawing.Size(190, 38)
        Me.chkShowAll.TabIndex = 2
        Me.chkShowAll.Text = "عرض كل السجل"
        Me.chkShowAll.UseVisualStyleBackColor = True
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.dgvLog)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 136)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlMain.Size = New System.Drawing.Size(1050, 474)
        Me.pnlMain.TabIndex = 2
        '
        'dgvLog
        '
        Me.dgvLog.AllowUserToAddRows = False
        Me.dgvLog.AllowUserToDeleteRows = False
        Me.dgvLog.AllowUserToResizeRows = False
        Me.dgvLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvLog.BackgroundColor = System.Drawing.Color.White
        Me.dgvLog.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvLog.ColumnHeadersHeight = 36
        Me.dgvLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLog.Location = New System.Drawing.Point(10, 10)
        Me.dgvLog.MultiSelect = False
        Me.dgvLog.Name = "dgvLog"
        Me.dgvLog.ReadOnly = True
        Me.dgvLog.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvLog.RowHeadersVisible = False
        Me.dgvLog.RowTemplate.Height = 30
        Me.dgvLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLog.Size = New System.Drawing.Size(1030, 454)
        Me.dgvLog.TabIndex = 0
        '
        'pnlFooter
        '
        Me.pnlFooter.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlFooter.Controls.Add(Me.lblStatus)
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(0, 610)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlFooter.Size = New System.Drawing.Size(1050, 40)
        Me.pnlFooter.TabIndex = 3
        '
        'lblStatus
        '
        Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblStatus.ForeColor = System.Drawing.Color.DimGray
        Me.lblStatus.Location = New System.Drawing.Point(10, 10)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(1030, 20)
        Me.lblStatus.TabIndex = 0
        Me.lblStatus.Text = "جاهز"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmSystemAccountLinksLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1050, 650)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlFooter)
        Me.Controls.Add(Me.pnlButtons)
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.MinimumSize = New System.Drawing.Size(900, 550)
        Me.Name = "FrmSystemAccountLinksLog"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "سجل تعديلات ربط الحسابات الأساسية"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlButtons.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        CType(Me.dgvLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFooter.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblAccountName As Label
    Friend WithEvents pnlButtons As Panel
    Friend WithEvents btnClose As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents chkShowAll As CheckBox
    Friend WithEvents pnlMain As Panel
    Friend WithEvents dgvLog As DataGridView
    Friend WithEvents pnlFooter As Panel
    Friend WithEvents lblStatus As Label
End Class