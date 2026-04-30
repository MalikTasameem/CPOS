<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Sales_Drafts_Menu
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblSubTitle = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlTopTools = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnOpenDraft = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.pnlStats = New System.Windows.Forms.Panel()
        Me.lblDraftsCountValue = New System.Windows.Forms.Label()
        Me.lblDraftsCountTitle = New System.Windows.Forms.Label()
        Me.pnlGridContainer = New System.Windows.Forms.Panel()
        Me.dgvDrafts = New System.Windows.Forms.DataGridView()
        Me.pnlHeader.SuspendLayout()
        Me.pnlTopTools.SuspendLayout()
        Me.pnlStats.SuspendLayout()
        Me.pnlGridContainer.SuspendLayout()
        CType(Me.dgvDrafts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblSubTitle)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Padding = New System.Windows.Forms.Padding(12, 8, 12, 8)
        Me.pnlHeader.Size = New System.Drawing.Size(950, 78)
        Me.pnlHeader.TabIndex = 0
        '
        'lblSubTitle
        '
        Me.lblSubTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSubTitle.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.lblSubTitle.Location = New System.Drawing.Point(12, 42)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Size = New System.Drawing.Size(926, 28)
        Me.lblSubTitle.TabIndex = 1
        Me.lblSubTitle.Text = "استعراض الفواتير المؤقتة، فتحها، تعديلها أو حذفها"
        Me.lblSubTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTitle
        '
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(12, 8)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(926, 34)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "مسودات فواتير المبيعات"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlTopTools
        '
        Me.pnlTopTools.BackColor = System.Drawing.Color.White
        Me.pnlTopTools.Controls.Add(Me.btnClose)
        Me.pnlTopTools.Controls.Add(Me.btnDelete)
        Me.pnlTopTools.Controls.Add(Me.btnOpenDraft)
        Me.pnlTopTools.Controls.Add(Me.btnRefresh)
        Me.pnlTopTools.Controls.Add(Me.txtSearch)
        Me.pnlTopTools.Controls.Add(Me.lblSearch)
        Me.pnlTopTools.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTopTools.Location = New System.Drawing.Point(0, 78)
        Me.pnlTopTools.Name = "pnlTopTools"
        Me.pnlTopTools.Padding = New System.Windows.Forms.Padding(12, 10, 12, 10)
        Me.pnlTopTools.Size = New System.Drawing.Size(950, 62)
        Me.pnlTopTools.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(96, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(12, 13)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(90, 36)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "إغلاق"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDelete.FlatAppearance.BorderSize = 0
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnDelete.ForeColor = System.Drawing.Color.White
        Me.btnDelete.Location = New System.Drawing.Point(108, 13)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(110, 36)
        Me.btnDelete.TabIndex = 4
        Me.btnDelete.Text = "حذف المسودة"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnOpenDraft
        '
        Me.btnOpenDraft.BackColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btnOpenDraft.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnOpenDraft.FlatAppearance.BorderSize = 0
        Me.btnOpenDraft.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOpenDraft.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnOpenDraft.ForeColor = System.Drawing.Color.White
        Me.btnOpenDraft.Location = New System.Drawing.Point(224, 13)
        Me.btnOpenDraft.Name = "btnOpenDraft"
        Me.btnOpenDraft.Size = New System.Drawing.Size(120, 36)
        Me.btnOpenDraft.TabIndex = 3
        Me.btnOpenDraft.Text = "فتح المسودة"
        Me.btnOpenDraft.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(136, Byte), Integer))
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(350, 13)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(90, 36)
        Me.btnRefresh.TabIndex = 2
        Me.btnRefresh.Text = "تحديث"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.txtSearch.Location = New System.Drawing.Point(462, 18)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSearch.Size = New System.Drawing.Size(348, 24)
        Me.txtSearch.TabIndex = 1
        '
        'lblSearch
        '
        Me.lblSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(79, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(816, 17)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(122, 25)
        Me.lblSearch.TabIndex = 0
        Me.lblSearch.Text = "بحث في المسودات"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlStats
        '
        Me.pnlStats.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlStats.Controls.Add(Me.lblDraftsCountValue)
        Me.pnlStats.Controls.Add(Me.lblDraftsCountTitle)
        Me.pnlStats.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlStats.Location = New System.Drawing.Point(0, 140)
        Me.pnlStats.Name = "pnlStats"
        Me.pnlStats.Padding = New System.Windows.Forms.Padding(12, 8, 12, 8)
        Me.pnlStats.Size = New System.Drawing.Size(950, 54)
        Me.pnlStats.TabIndex = 2
        '
        'lblDraftsCountValue
        '
        Me.lblDraftsCountValue.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblDraftsCountValue.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblDraftsCountValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.lblDraftsCountValue.Location = New System.Drawing.Point(780, 8)
        Me.lblDraftsCountValue.Name = "lblDraftsCountValue"
        Me.lblDraftsCountValue.Size = New System.Drawing.Size(158, 38)
        Me.lblDraftsCountValue.TabIndex = 1
        Me.lblDraftsCountValue.Text = "0"
        Me.lblDraftsCountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDraftsCountTitle
        '
        Me.lblDraftsCountTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDraftsCountTitle.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblDraftsCountTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.lblDraftsCountTitle.Location = New System.Drawing.Point(12, 8)
        Me.lblDraftsCountTitle.Name = "lblDraftsCountTitle"
        Me.lblDraftsCountTitle.Size = New System.Drawing.Size(926, 38)
        Me.lblDraftsCountTitle.TabIndex = 0
        Me.lblDraftsCountTitle.Text = "إجمالي عدد المسودات الحالية"
        Me.lblDraftsCountTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlGridContainer
        '
        Me.pnlGridContainer.BackColor = System.Drawing.Color.White
        Me.pnlGridContainer.Controls.Add(Me.dgvDrafts)
        Me.pnlGridContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGridContainer.Location = New System.Drawing.Point(0, 194)
        Me.pnlGridContainer.Name = "pnlGridContainer"
        Me.pnlGridContainer.Padding = New System.Windows.Forms.Padding(12)
        Me.pnlGridContainer.Size = New System.Drawing.Size(950, 456)
        Me.pnlGridContainer.TabIndex = 3
        '
        'dgvDrafts
        '
        Me.dgvDrafts.AllowUserToAddRows = False
        Me.dgvDrafts.AllowUserToDeleteRows = False
        Me.dgvDrafts.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgvDrafts.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvDrafts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvDrafts.BackgroundColor = System.Drawing.Color.White
        Me.dgvDrafts.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvDrafts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvDrafts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(79, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(79, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDrafts.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvDrafts.ColumnHeadersHeight = 38
        Me.dgvDrafts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvDrafts.Cursor = System.Windows.Forms.Cursors.Hand
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(253, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDrafts.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvDrafts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDrafts.EnableHeadersVisualStyles = False
        Me.dgvDrafts.GridColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.dgvDrafts.Location = New System.Drawing.Point(12, 12)
        Me.dgvDrafts.MultiSelect = False
        Me.dgvDrafts.Name = "dgvDrafts"
        Me.dgvDrafts.ReadOnly = True
        Me.dgvDrafts.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dgvDrafts.RowHeadersVisible = False
        Me.dgvDrafts.RowTemplate.Height = 34
        Me.dgvDrafts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDrafts.Size = New System.Drawing.Size(926, 432)
        Me.dgvDrafts.TabIndex = 0
        '
        'Sales_Drafts_Menu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(950, 650)
        Me.Controls.Add(Me.pnlGridContainer)
        Me.Controls.Add(Me.pnlStats)
        Me.Controls.Add(Me.pnlTopTools)
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.MinimumSize = New System.Drawing.Size(850, 550)
        Me.Name = "Sales_Drafts_Menu"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "مسودة الفواتير"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlTopTools.ResumeLayout(False)
        Me.pnlTopTools.PerformLayout()
        Me.pnlStats.ResumeLayout(False)
        Me.pnlGridContainer.ResumeLayout(False)
        CType(Me.dgvDrafts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblSubTitle As System.Windows.Forms.Label
    Friend WithEvents pnlTopTools As System.Windows.Forms.Panel
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnOpenDraft As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents pnlStats As System.Windows.Forms.Panel
    Friend WithEvents lblDraftsCountTitle As System.Windows.Forms.Label
    Friend WithEvents lblDraftsCountValue As System.Windows.Forms.Label
    Friend WithEvents pnlGridContainer As System.Windows.Forms.Panel
    Friend WithEvents dgvDrafts As System.Windows.Forms.DataGridView

End Class


















'--------------------------------------------------------------------------------------------------------------
'<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
'Partial Class Sales_Drafts_Menu
'    Inherits System.Windows.Forms.Form

'    'Form overrides dispose to clean up the component list.
'    <System.Diagnostics.DebuggerNonUserCode()> _
'    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
'        Try
'            If disposing AndAlso components IsNot Nothing Then
'                components.Dispose()
'            End If
'        Finally
'            MyBase.Dispose(disposing)
'        End Try
'    End Sub

'    'Required by the Windows Form Designer
'    Private components As System.ComponentModel.IContainer

'    'NOTE: The following procedure is required by the Windows Form Designer
'    'It can be modified using the Windows Form Designer.  
'    'Do not modify it using the code editor.
'    <System.Diagnostics.DebuggerStepThrough()> _
'    Private Sub InitializeComponent()
'        Me.dgvDrafts = New System.Windows.Forms.DataGridView()
'        CType(Me.dgvDrafts, System.ComponentModel.ISupportInitialize).BeginInit()
'        Me.SuspendLayout()
'        '
'        'dgvDrafts
'        '
'        Me.dgvDrafts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
'        Me.dgvDrafts.Location = New System.Drawing.Point(3, 46)
'        Me.dgvDrafts.Name = "dgvDrafts"
'        Me.dgvDrafts.Size = New System.Drawing.Size(791, 546)
'        Me.dgvDrafts.TabIndex = 0
'        '
'        'Sales_Drafts_Menu
'        '
'        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
'        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
'        Me.ClientSize = New System.Drawing.Size(796, 598)
'        Me.Controls.Add(Me.dgvDrafts)
'        Me.Name = "Sales_Drafts_Menu"
'        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
'        Me.Text = "مسودة الفواتير"
'        CType(Me.dgvDrafts, System.ComponentModel.ISupportInitialize).EndInit()
'        Me.ResumeLayout(False)

'    End Sub

'    Friend WithEvents dgvDrafts As DataGridView
'End Class
