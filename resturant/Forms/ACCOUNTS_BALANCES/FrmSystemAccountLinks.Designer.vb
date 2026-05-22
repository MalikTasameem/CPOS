<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSystemAccountLinks
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
        Me.lblSubTitle = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnValidate = New System.Windows.Forms.Button()
        Me.btnChangeAccount = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.dgvLinks = New System.Windows.Forms.DataGridView()
        Me.pnlFooter = New System.Windows.Forms.Panel()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        CType(Me.dgvLinks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFooter.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(72, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblSubTitle)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Padding = New System.Windows.Forms.Padding(12, 8, 12, 8)
        Me.pnlHeader.Size = New System.Drawing.Size(1050, 78)
        Me.pnlHeader.TabIndex = 0
        '
        'lblSubTitle
        '
        Me.lblSubTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSubTitle.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblSubTitle.ForeColor = System.Drawing.Color.Gainsboro
        Me.lblSubTitle.Location = New System.Drawing.Point(12, 42)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Size = New System.Drawing.Size(1026, 28)
        Me.lblSubTitle.TabIndex = 1
        Me.lblSubTitle.Text = "إدارة ربط حسابات النظام الأساسية مثل المبيعات، المخزون، تكلفة البضاعة، الخصومات، " &
    "الضرائب، والعملاء والموردين."
        Me.lblSubTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.lblTitle.Text = "ربط الحسابات الأساسية العامة"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlButtons
        '
        Me.pnlButtons.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pnlButtons.Controls.Add(Me.btnClose)
        Me.pnlButtons.Controls.Add(Me.btnValidate)
        Me.pnlButtons.Controls.Add(Me.btnChangeAccount)
        Me.pnlButtons.Controls.Add(Me.btnRefresh)
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
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "إغلاق"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnValidate
        '
        Me.btnValidate.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnValidate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnValidate.Location = New System.Drawing.Point(570, 10)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(150, 38)
        Me.btnValidate.TabIndex = 2
        Me.btnValidate.Text = "فحص الربط"
        Me.btnValidate.UseVisualStyleBackColor = True
        '
        'btnChangeAccount
        '
        Me.btnChangeAccount.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnChangeAccount.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnChangeAccount.Location = New System.Drawing.Point(720, 10)
        Me.btnChangeAccount.Name = "btnChangeAccount"
        Me.btnChangeAccount.Size = New System.Drawing.Size(170, 38)
        Me.btnChangeAccount.TabIndex = 1
        Me.btnChangeAccount.Text = "تغيير الحساب"
        Me.btnChangeAccount.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.Location = New System.Drawing.Point(890, 10)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(150, 38)
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.Text = "تحديث"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.dgvLinks)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 136)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlMain.Size = New System.Drawing.Size(1050, 474)
        Me.pnlMain.TabIndex = 2
        '
        'dgvLinks
        '
        Me.dgvLinks.AllowUserToAddRows = False
        Me.dgvLinks.AllowUserToDeleteRows = False
        Me.dgvLinks.AllowUserToResizeRows = False
        Me.dgvLinks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvLinks.BackgroundColor = System.Drawing.Color.White
        Me.dgvLinks.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvLinks.ColumnHeadersHeight = 36
        Me.dgvLinks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvLinks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLinks.Location = New System.Drawing.Point(10, 10)
        Me.dgvLinks.MultiSelect = False
        Me.dgvLinks.Name = "dgvLinks"
        Me.dgvLinks.ReadOnly = True
        Me.dgvLinks.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvLinks.RowHeadersVisible = False
        Me.dgvLinks.RowTemplate.Height = 30
        Me.dgvLinks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLinks.Size = New System.Drawing.Size(1030, 454)
        Me.dgvLinks.TabIndex = 0
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
        'FrmSystemAccountLinks
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
        Me.Name = "FrmSystemAccountLinks"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ربط الحسابات الأساسية العامة"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlButtons.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        CType(Me.dgvLinks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFooter.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubTitle As Label
    Friend WithEvents pnlButtons As Panel
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnChangeAccount As Button
    Friend WithEvents btnValidate As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents pnlMain As Panel
    Friend WithEvents dgvLinks As DataGridView
    Friend WithEvents pnlFooter As Panel
    Friend WithEvents lblStatus As Label
End Class