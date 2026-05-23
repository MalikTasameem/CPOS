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
        Me.components = New System.ComponentModel.Container()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblSubTitle = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnEditType = New System.Windows.Forms.Button()
        Me.btnValidate = New System.Windows.Forms.Button()
        Me.btnChangeAccount = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnDetails = New System.Windows.Forms.Button()
        Me.btnCancelLink = New System.Windows.Forms.Button()
        Me.cboFilter = New System.Windows.Forms.ComboBox()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.btnShowLog = New System.Windows.Forms.Button()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.dgvLinks = New System.Windows.Forms.DataGridView()
        Me.cmsLinks = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuRefresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.sepLinksMain = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuChangeAccount = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCancelLink = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDetails = New System.Windows.Forms.ToolStripMenuItem()
        Me.sepLinksTools = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuValidate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditType = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShowLog = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlFooter = New System.Windows.Forms.Panel()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        CType(Me.dgvLinks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsLinks.SuspendLayout()
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
        Me.pnlButtons.Controls.Add(Me.btnEditType)
        Me.pnlButtons.Controls.Add(Me.btnValidate)
        Me.pnlButtons.Controls.Add(Me.btnChangeAccount)
        Me.pnlButtons.Controls.Add(Me.btnRefresh)
        Me.pnlButtons.Controls.Add(Me.btnDetails)
        Me.pnlButtons.Controls.Add(Me.btnCancelLink)
        Me.pnlButtons.Controls.Add(Me.cboFilter)
        Me.pnlButtons.Controls.Add(Me.lblFilter)
        Me.pnlButtons.Controls.Add(Me.btnShowLog)
        Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlButtons.Location = New System.Drawing.Point(0, 78)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlButtons.Size = New System.Drawing.Size(1050, 52)
        Me.pnlButtons.TabIndex = 1
        '
        'btnEditType
        '
        Me.btnEditType.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnEditType.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditType.Location = New System.Drawing.Point(10, 6)
        Me.btnEditType.Name = "btnEditType"
        Me.btnEditType.Size = New System.Drawing.Size(100, 38)
        Me.btnEditType.TabIndex = 4
        Me.btnEditType.Text = "تعديل النمط"
        Me.btnEditType.UseVisualStyleBackColor = True
        '
        'btnValidate
        '
        Me.btnValidate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnValidate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnValidate.Location = New System.Drawing.Point(652, 6)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(98, 38)
        Me.btnValidate.TabIndex = 2
        Me.btnValidate.Text = "فحص الربط"
        Me.btnValidate.UseVisualStyleBackColor = True
        '
        'btnChangeAccount
        '
        Me.btnChangeAccount.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnChangeAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangeAccount.Location = New System.Drawing.Point(347, 6)
        Me.btnChangeAccount.Name = "btnChangeAccount"
        Me.btnChangeAccount.Size = New System.Drawing.Size(117, 38)
        Me.btnChangeAccount.TabIndex = 1
        Me.btnChangeAccount.Text = "تغيير الحساب"
        Me.btnChangeAccount.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Location = New System.Drawing.Point(791, 6)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 38)
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.Text = "تحديث"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnDetails
        '
        Me.btnDetails.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDetails.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDetails.Location = New System.Drawing.Point(466, 6)
        Me.btnDetails.Name = "btnDetails"
        Me.btnDetails.Size = New System.Drawing.Size(84, 38)
        Me.btnDetails.TabIndex = 6
        Me.btnDetails.Text = "تفاصيل"
        Me.btnDetails.UseVisualStyleBackColor = True
        '
        'btnCancelLink
        '
        Me.btnCancelLink.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCancelLink.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelLink.Location = New System.Drawing.Point(552, 6)
        Me.btnCancelLink.Name = "btnCancelLink"
        Me.btnCancelLink.Size = New System.Drawing.Size(98, 38)
        Me.btnCancelLink.TabIndex = 5
        Me.btnCancelLink.Text = "إلغاء الربط"
        Me.btnCancelLink.UseVisualStyleBackColor = True
        '
        'cboFilter
        '
        Me.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFilter.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cboFilter.FormattingEnabled = True
        Me.cboFilter.Location = New System.Drawing.Point(860, 6)
        Me.cboFilter.Name = "cboFilter"
        Me.cboFilter.Size = New System.Drawing.Size(130, 23)
        Me.cboFilter.TabIndex = 8
        '
        'lblFilter
        '
        Me.lblFilter.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilter.Location = New System.Drawing.Point(990, 6)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(57, 22)
        Me.lblFilter.TabIndex = 7
        Me.lblFilter.Text = "عرض:"
        Me.lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnShowLog
        '
        Me.btnShowLog.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnShowLog.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowLog.Location = New System.Drawing.Point(112, 6)
        Me.btnShowLog.Name = "btnShowLog"
        Me.btnShowLog.Size = New System.Drawing.Size(102, 38)
        Me.btnShowLog.TabIndex = 9
        Me.btnShowLog.Text = "سجل التعديلات"
        Me.btnShowLog.UseVisualStyleBackColor = True
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.dgvLinks)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 130)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlMain.Size = New System.Drawing.Size(1050, 480)
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
        Me.dgvLinks.ContextMenuStrip = Me.cmsLinks
        Me.dgvLinks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLinks.Location = New System.Drawing.Point(10, 10)
        Me.dgvLinks.MultiSelect = False
        Me.dgvLinks.Name = "dgvLinks"
        Me.dgvLinks.ReadOnly = True
        Me.dgvLinks.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvLinks.RowHeadersVisible = False
        Me.dgvLinks.RowTemplate.Height = 30
        Me.dgvLinks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLinks.Size = New System.Drawing.Size(1030, 460)
        Me.dgvLinks.TabIndex = 0
        '
        'cmsLinks
        '
        Me.cmsLinks.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cmsLinks.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuRefresh, Me.sepLinksMain, Me.mnuChangeAccount, Me.mnuCancelLink, Me.mnuDetails, Me.sepLinksTools, Me.mnuValidate, Me.mnuEditType, Me.mnuShowLog})
        Me.cmsLinks.Name = "cmsLinks"
        Me.cmsLinks.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cmsLinks.Size = New System.Drawing.Size(181, 192)
        '
        'mnuRefresh
        '
        Me.mnuRefresh.Name = "mnuRefresh"
        Me.mnuRefresh.Size = New System.Drawing.Size(180, 22)
        Me.mnuRefresh.Text = "تحديث"
        '
        'sepLinksMain
        '
        Me.sepLinksMain.Name = "sepLinksMain"
        Me.sepLinksMain.Size = New System.Drawing.Size(177, 6)
        '
        'mnuChangeAccount
        '
        Me.mnuChangeAccount.Name = "mnuChangeAccount"
        Me.mnuChangeAccount.Size = New System.Drawing.Size(180, 22)
        Me.mnuChangeAccount.Text = "تغيير الحساب"
        '
        'mnuCancelLink
        '
        Me.mnuCancelLink.Name = "mnuCancelLink"
        Me.mnuCancelLink.Size = New System.Drawing.Size(180, 22)
        Me.mnuCancelLink.Text = "إلغاء الربط"
        '
        'mnuDetails
        '
        Me.mnuDetails.Name = "mnuDetails"
        Me.mnuDetails.Size = New System.Drawing.Size(180, 22)
        Me.mnuDetails.Text = "تفاصيل"
        '
        'sepLinksTools
        '
        Me.sepLinksTools.Name = "sepLinksTools"
        Me.sepLinksTools.Size = New System.Drawing.Size(177, 6)
        '
        'mnuValidate
        '
        Me.mnuValidate.Name = "mnuValidate"
        Me.mnuValidate.Size = New System.Drawing.Size(180, 22)
        Me.mnuValidate.Text = "فحص الربط"
        '
        'mnuEditType
        '
        Me.mnuEditType.Name = "mnuEditType"
        Me.mnuEditType.Size = New System.Drawing.Size(180, 22)
        Me.mnuEditType.Text = "تعديل النمط"
        '
        'mnuShowLog
        '
        Me.mnuShowLog.Name = "mnuShowLog"
        Me.mnuShowLog.Size = New System.Drawing.Size(180, 22)
        Me.mnuShowLog.Text = "سجل التعديلات"
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
        Me.cmsLinks.ResumeLayout(False)
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
    Friend WithEvents pnlMain As Panel
    Friend WithEvents dgvLinks As DataGridView
    Friend WithEvents pnlFooter As Panel
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnEditType As Button

    Friend WithEvents btnCancelLink As Button
    Friend WithEvents btnDetails As Button
    Friend WithEvents cboFilter As ComboBox
    Friend WithEvents lblFilter As Label
    Friend WithEvents btnShowLog As Button
    Friend WithEvents cmsLinks As ContextMenuStrip
    Friend WithEvents mnuRefresh As ToolStripMenuItem
    Friend WithEvents sepLinksMain As ToolStripSeparator
    Friend WithEvents mnuChangeAccount As ToolStripMenuItem
    Friend WithEvents mnuCancelLink As ToolStripMenuItem
    Friend WithEvents mnuDetails As ToolStripMenuItem
    Friend WithEvents sepLinksTools As ToolStripSeparator
    Friend WithEvents mnuValidate As ToolStripMenuItem
    Friend WithEvents mnuEditType As ToolStripMenuItem
    Friend WithEvents mnuShowLog As ToolStripMenuItem

End Class
