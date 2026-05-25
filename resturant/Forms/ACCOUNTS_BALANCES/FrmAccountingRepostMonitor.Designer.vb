<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAccountingRepostMonitor
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbUser = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbSource = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlActions = New System.Windows.Forms.Panel()
        Me.btnViewNewJournal = New System.Windows.Forms.Button()
        Me.btnViewReversalJournal = New System.Windows.Forms.Button()
        Me.btnViewOldJournal = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.dgvRepost = New System.Windows.Forms.DataGridView()
        Me.dgvJournal = New System.Windows.Forms.DataGridView()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.lblStatusMessage = New System.Windows.Forms.Label()
        Me.lblRctCount = New System.Windows.Forms.Label()
        Me.lblMvCount = New System.Windows.Forms.Label()
        Me.lblTotalCount = New System.Windows.Forms.Label()
        Me.pnlTop.SuspendLayout()
        Me.pnlActions.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgvRepost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvJournal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlTop.Controls.Add(Me.btnClose)
        Me.pnlTop.Controls.Add(Me.btnRefresh)
        Me.pnlTop.Controls.Add(Me.btnSearch)
        Me.pnlTop.Controls.Add(Me.txtSearch)
        Me.pnlTop.Controls.Add(Me.Label6)
        Me.pnlTop.Controls.Add(Me.cmbUser)
        Me.pnlTop.Controls.Add(Me.Label5)
        Me.pnlTop.Controls.Add(Me.cmbType)
        Me.pnlTop.Controls.Add(Me.Label4)
        Me.pnlTop.Controls.Add(Me.cmbSource)
        Me.pnlTop.Controls.Add(Me.Label3)
        Me.pnlTop.Controls.Add(Me.dtpTo)
        Me.pnlTop.Controls.Add(Me.Label2)
        Me.pnlTop.Controls.Add(Me.dtpFrom)
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(8)
        Me.pnlTop.Size = New System.Drawing.Size(1280, 92)
        Me.pnlTop.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(12, 49)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(95, 31)
        Me.btnClose.TabIndex = 14
        Me.btnClose.Text = "إغلاق"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(113, 49)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(95, 31)
        Me.btnRefresh.TabIndex = 13
        Me.btnRefresh.Text = "تحديث"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(214, 49)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(95, 31)
        Me.btnSearch.TabIndex = 12
        Me.btnSearch.Text = "بحث"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(315, 54)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(235, 24)
        Me.txtSearch.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(556, 58)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 17)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "بحث"
        '
        'cmbUser
        '
        Me.cmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUser.FormattingEnabled = True
        Me.cmbUser.Location = New System.Drawing.Point(599, 54)
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.Size = New System.Drawing.Size(180, 24)
        Me.cmbUser.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(785, 58)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 17)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "المستخدم"
        '
        'cmbType
        '
        Me.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.Location = New System.Drawing.Point(857, 54)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(190, 24)
        Me.cmbType.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(1053, 58)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 17)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "نوع الحركة"
        '
        'cmbSource
        '
        Me.cmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSource.FormattingEnabled = True
        Me.cmbSource.Location = New System.Drawing.Point(12, 14)
        Me.cmbSource.Name = "cmbSource"
        Me.cmbSource.Size = New System.Drawing.Size(180, 24)
        Me.cmbSource.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(198, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "المصدر"
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "yyyy/MM/dd"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(315, 14)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(140, 24)
        Me.dtpTo.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(461, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "إلى"
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "yyyy/MM/dd"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(493, 14)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(140, 24)
        Me.dtpFrom.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(639, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "من"
        '
        'pnlActions
        '
        Me.pnlActions.BackColor = System.Drawing.Color.White
        Me.pnlActions.Controls.Add(Me.btnViewNewJournal)
        Me.pnlActions.Controls.Add(Me.btnViewReversalJournal)
        Me.pnlActions.Controls.Add(Me.btnViewOldJournal)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlActions.Location = New System.Drawing.Point(0, 92)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Padding = New System.Windows.Forms.Padding(8)
        Me.pnlActions.Size = New System.Drawing.Size(1280, 48)
        Me.pnlActions.TabIndex = 1
        '
        'btnViewNewJournal
        '
        Me.btnViewNewJournal.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.btnViewNewJournal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewNewJournal.ForeColor = System.Drawing.Color.White
        Me.btnViewNewJournal.Location = New System.Drawing.Point(12, 9)
        Me.btnViewNewJournal.Name = "btnViewNewJournal"
        Me.btnViewNewJournal.Size = New System.Drawing.Size(150, 31)
        Me.btnViewNewJournal.TabIndex = 2
        Me.btnViewNewJournal.Text = "عرض القيد الجديد"
        Me.btnViewNewJournal.UseVisualStyleBackColor = False
        '
        'btnViewReversalJournal
        '
        Me.btnViewReversalJournal.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(196, Byte), Integer), CType(CType(15, Byte), Integer))
        Me.btnViewReversalJournal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewReversalJournal.ForeColor = System.Drawing.Color.Black
        Me.btnViewReversalJournal.Location = New System.Drawing.Point(168, 9)
        Me.btnViewReversalJournal.Name = "btnViewReversalJournal"
        Me.btnViewReversalJournal.Size = New System.Drawing.Size(150, 31)
        Me.btnViewReversalJournal.TabIndex = 1
        Me.btnViewReversalJournal.Text = "عرض القيد العكسي"
        Me.btnViewReversalJournal.UseVisualStyleBackColor = False
        '
        'btnViewOldJournal
        '
        Me.btnViewOldJournal.BackColor = System.Drawing.Color.FromArgb(CType(CType(149, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnViewOldJournal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewOldJournal.ForeColor = System.Drawing.Color.White
        Me.btnViewOldJournal.Location = New System.Drawing.Point(324, 9)
        Me.btnViewOldJournal.Name = "btnViewOldJournal"
        Me.btnViewOldJournal.Size = New System.Drawing.Size(150, 31)
        Me.btnViewOldJournal.TabIndex = 0
        Me.btnViewOldJournal.Text = "عرض القيد القديم"
        Me.btnViewOldJournal.UseVisualStyleBackColor = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 140)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvRepost)
        Me.SplitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvJournal)
        Me.SplitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.SplitContainer1.Size = New System.Drawing.Size(1280, 587)
        Me.SplitContainer1.SplitterDistance = 367
        Me.SplitContainer1.TabIndex = 2
        '
        'dgvRepost
        '
        Me.dgvRepost.AllowUserToAddRows = False
        Me.dgvRepost.AllowUserToDeleteRows = False
        Me.dgvRepost.BackgroundColor = System.Drawing.Color.White
        Me.dgvRepost.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRepost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvRepost.Location = New System.Drawing.Point(0, 0)
        Me.dgvRepost.MultiSelect = False
        Me.dgvRepost.Name = "dgvRepost"
        Me.dgvRepost.ReadOnly = True
        Me.dgvRepost.RowHeadersWidth = 40
        Me.dgvRepost.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRepost.Size = New System.Drawing.Size(1280, 367)
        Me.dgvRepost.TabIndex = 0
        '
        'dgvJournal
        '
        Me.dgvJournal.AllowUserToAddRows = False
        Me.dgvJournal.AllowUserToDeleteRows = False
        Me.dgvJournal.BackgroundColor = System.Drawing.Color.White
        Me.dgvJournal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvJournal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvJournal.Location = New System.Drawing.Point(0, 0)
        Me.dgvJournal.MultiSelect = False
        Me.dgvJournal.Name = "dgvJournal"
        Me.dgvJournal.ReadOnly = True
        Me.dgvJournal.RowHeadersWidth = 40
        Me.dgvJournal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvJournal.Size = New System.Drawing.Size(1280, 216)
        Me.dgvJournal.TabIndex = 0
        '
        'pnlBottom
        '
        Me.pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlBottom.Controls.Add(Me.lblStatusMessage)
        Me.pnlBottom.Controls.Add(Me.lblRctCount)
        Me.pnlBottom.Controls.Add(Me.lblMvCount)
        Me.pnlBottom.Controls.Add(Me.lblTotalCount)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 727)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1280, 33)
        Me.pnlBottom.TabIndex = 3
        '
        'lblStatusMessage
        '
        Me.lblStatusMessage.AutoSize = True
        Me.lblStatusMessage.Location = New System.Drawing.Point(12, 8)
        Me.lblStatusMessage.Name = "lblStatusMessage"
        Me.lblStatusMessage.Size = New System.Drawing.Size(36, 17)
        Me.lblStatusMessage.TabIndex = 3
        Me.lblStatusMessage.Text = "جاهز"
        '
        'lblRctCount
        '
        Me.lblRctCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRctCount.AutoSize = True
        Me.lblRctCount.Location = New System.Drawing.Point(833, 8)
        Me.lblRctCount.Name = "lblRctCount"
        Me.lblRctCount.Size = New System.Drawing.Size(76, 17)
        Me.lblRctCount.TabIndex = 2
        Me.lblRctCount.Text = "الإيصالات: 0"
        '
        'lblMvCount
        '
        Me.lblMvCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMvCount.AutoSize = True
        Me.lblMvCount.Location = New System.Drawing.Point(946, 8)
        Me.lblMvCount.Name = "lblMvCount"
        Me.lblMvCount.Size = New System.Drawing.Size(63, 17)
        Me.lblMvCount.TabIndex = 1
        Me.lblMvCount.Text = "الفواتير: 0"
        '
        'lblTotalCount
        '
        Me.lblTotalCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalCount.AutoSize = True
        Me.lblTotalCount.Location = New System.Drawing.Point(1060, 8)
        Me.lblTotalCount.Name = "lblTotalCount"
        Me.lblTotalCount.Size = New System.Drawing.Size(77, 17)
        Me.lblTotalCount.TabIndex = 0
        Me.lblTotalCount.Text = "الإجمالي: 0"
        '
        'FrmAccountingRepostMonitor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1280, 760)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.pnlActions)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Name = "FrmAccountingRepostMonitor"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "متابعة تعديلات وإعادة تقييد القيود المحاسبية"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnlActions.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgvRepost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvJournal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlTop As Panel
    Friend WithEvents btnClose As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbUser As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cmbType As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cmbSource As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnViewNewJournal As Button
    Friend WithEvents btnViewReversalJournal As Button
    Friend WithEvents btnViewOldJournal As Button
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents dgvRepost As DataGridView
    Friend WithEvents dgvJournal As DataGridView
    Friend WithEvents pnlBottom As Panel
    Friend WithEvents lblStatusMessage As Label
    Friend WithEvents lblRctCount As Label
    Friend WithEvents lblMvCount As Label
    Friend WithEvents lblTotalCount As Label

End Class