<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmExchangeAuditViewer
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.PanelTop = New System.Windows.Forms.Panel()
        Me.txtQuickSearch = New System.Windows.Forms.TextBox()
        Me.LabelFrom = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.LabelTo = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.LabelId = New System.Windows.Forms.Label()
        Me.txtExchangeId = New System.Windows.Forms.TextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.PanelBottom = New System.Windows.Forms.Panel()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.PanelTop.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelTop
        '
        Me.PanelTop.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PanelTop.Controls.Add(Me.txtQuickSearch)
        Me.PanelTop.Controls.Add(Me.LabelFrom)
        Me.PanelTop.Controls.Add(Me.dtpFrom)
        Me.PanelTop.Controls.Add(Me.LabelTo)
        Me.PanelTop.Controls.Add(Me.dtpTo)
        Me.PanelTop.Controls.Add(Me.LabelId)
        Me.PanelTop.Controls.Add(Me.txtExchangeId)
        Me.PanelTop.Controls.Add(Me.btnSearch)
        Me.PanelTop.Controls.Add(Me.btnRefresh)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(1184, 80)
        Me.PanelTop.TabIndex = 2
        '
        'txtQuickSearch
        '
        Me.txtQuickSearch.Location = New System.Drawing.Point(3, 54)
        Me.txtQuickSearch.Name = "txtQuickSearch"
        Me.txtQuickSearch.Size = New System.Drawing.Size(921, 23)
        Me.txtQuickSearch.TabIndex = 8
        '
        'LabelFrom
        '
        Me.LabelFrom.Location = New System.Drawing.Point(1132, 22)
        Me.LabelFrom.Name = "LabelFrom"
        Me.LabelFrom.Size = New System.Drawing.Size(40, 23)
        Me.LabelFrom.TabIndex = 0
        Me.LabelFrom.Text = "من"
        '
        'dtpFrom
        '
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(930, 22)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(200, 23)
        Me.dtpFrom.TabIndex = 1
        '
        'LabelTo
        '
        Me.LabelTo.Location = New System.Drawing.Point(1134, 50)
        Me.LabelTo.Name = "LabelTo"
        Me.LabelTo.Size = New System.Drawing.Size(40, 23)
        Me.LabelTo.TabIndex = 2
        Me.LabelTo.Text = "إلى"
        '
        'dtpTo
        '
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(930, 51)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(200, 23)
        Me.dtpTo.TabIndex = 3
        '
        'LabelId
        '
        Me.LabelId.Location = New System.Drawing.Point(627, 20)
        Me.LabelId.Name = "LabelId"
        Me.LabelId.Size = New System.Drawing.Size(100, 23)
        Me.LabelId.TabIndex = 4
        Me.LabelId.Text = "رقم العملية"
        '
        'txtExchangeId
        '
        Me.txtExchangeId.Location = New System.Drawing.Point(468, 20)
        Me.txtExchangeId.Name = "txtExchangeId"
        Me.txtExchangeId.Size = New System.Drawing.Size(155, 23)
        Me.txtExchangeId.TabIndex = 5
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearch.Location = New System.Drawing.Point(372, 20)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(90, 23)
        Me.btnSearch.TabIndex = 6
        Me.btnSearch.Text = "بحث"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Location = New System.Drawing.Point(241, 20)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(125, 23)
        Me.btnRefresh.TabIndex = 7
        Me.btnRefresh.Text = "تحديث"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        Me.dgv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv.Location = New System.Drawing.Point(0, 80)
        Me.dgv.MultiSelect = False
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(1184, 471)
        Me.dgv.TabIndex = 0
        '
        'PanelBottom
        '
        Me.PanelBottom.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PanelBottom.Controls.Add(Me.btnExport)
        Me.PanelBottom.Controls.Add(Me.btnClose)
        Me.PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelBottom.Location = New System.Drawing.Point(0, 551)
        Me.PanelBottom.Name = "PanelBottom"
        Me.PanelBottom.Size = New System.Drawing.Size(1184, 60)
        Me.PanelBottom.TabIndex = 1
        '
        'btnExport
        '
        Me.btnExport.BackColor = System.Drawing.Color.Honeydew
        Me.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExport.Location = New System.Drawing.Point(150, 12)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(120, 36)
        Me.btnExport.TabIndex = 0
        Me.btnExport.Text = "تصدير Excel"
        Me.btnExport.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.MistyRose
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(20, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 36)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "إغلاق"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'FrmExchangeAuditViewer
        '
        Me.ClientSize = New System.Drawing.Size(1184, 611)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.PanelBottom)
        Me.Controls.Add(Me.PanelTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "FrmExchangeAuditViewer"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "سجل مراجعة عمليات الصرافة"
        Me.PanelTop.ResumeLayout(False)
        Me.PanelTop.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelTop As Panel
    Friend WithEvents LabelFrom As Label
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents LabelTo As Label
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents LabelId As Label
    Friend WithEvents txtExchangeId As TextBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents dgv As DataGridView
    Friend WithEvents PanelBottom As Panel
    Friend WithEvents btnExport As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents txtQuickSearch As TextBox
End Class
