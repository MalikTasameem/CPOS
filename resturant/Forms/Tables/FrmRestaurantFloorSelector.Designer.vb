<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRestaurantFloorSelector
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnOldMenu = New System.Windows.Forms.Button()
        Me.btnNoneTable = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.cmbFlates = New System.Windows.Forms.ComboBox()
        Me.lblFlate = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlLegend = New System.Windows.Forms.Panel()
        Me.lblFree = New System.Windows.Forms.Label()
        Me.lblBusy = New System.Windows.Forms.Label()
        Me.lblCash = New System.Windows.Forms.Label()
        Me.FloorCanvas = New Global.resturant.RestaurantFloorDesignerControl()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.pnlTop.SuspendLayout()
        Me.pnlLegend.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.pnlTop.Controls.Add(Me.btnClose)
        Me.pnlTop.Controls.Add(Me.btnOldMenu)
        Me.pnlTop.Controls.Add(Me.btnNoneTable)
        Me.pnlTop.Controls.Add(Me.btnRefresh)
        Me.pnlTop.Controls.Add(Me.cmbFlates)
        Me.pnlTop.Controls.Add(Me.lblFlate)
        Me.pnlTop.Controls.Add(Me.lblTitle)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1120, 76)
        Me.pnlTop.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(12, 20)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(104, 37)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "إغلاق"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnOldMenu
        '
        Me.btnOldMenu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOldMenu.BackColor = System.Drawing.Color.FromArgb(CType(CType(71, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.btnOldMenu.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnOldMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOldMenu.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnOldMenu.ForeColor = System.Drawing.Color.White
        Me.btnOldMenu.Location = New System.Drawing.Point(122, 20)
        Me.btnOldMenu.Name = "btnOldMenu"
        Me.btnOldMenu.Size = New System.Drawing.Size(142, 37)
        Me.btnOldMenu.TabIndex = 5
        Me.btnOldMenu.Text = "القائمة القديمة"
        Me.btnOldMenu.UseVisualStyleBackColor = False
        '
        'btnNoneTable
        '
        Me.btnNoneTable.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNoneTable.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(11, Byte), Integer))
        Me.btnNoneTable.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNoneTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNoneTable.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnNoneTable.ForeColor = System.Drawing.Color.White
        Me.btnNoneTable.Location = New System.Drawing.Point(270, 20)
        Me.btnNoneTable.Name = "btnNoneTable"
        Me.btnNoneTable.Size = New System.Drawing.Size(128, 37)
        Me.btnNoneTable.TabIndex = 4
        Me.btnNoneTable.Text = "بدون طاولة"
        Me.btnNoneTable.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(633, 20)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(106, 37)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "تحديث"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'cmbFlates
        '
        Me.cmbFlates.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbFlates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFlates.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbFlates.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.cmbFlates.FormattingEnabled = True
        Me.cmbFlates.Location = New System.Drawing.Point(745, 25)
        Me.cmbFlates.Name = "cmbFlates"
        Me.cmbFlates.Size = New System.Drawing.Size(220, 25)
        Me.cmbFlates.TabIndex = 2
        '
        'lblFlate
        '
        Me.lblFlate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFlate.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular)
        Me.lblFlate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblFlate.Location = New System.Drawing.Point(971, 25)
        Me.lblFlate.Name = "lblFlate"
        Me.lblFlate.Size = New System.Drawing.Size(54, 25)
        Me.lblFlate.TabIndex = 1
        Me.lblFlate.Text = "الدور"
        Me.lblFlate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTitle
        '
        Me.lblTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 12.5!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(1031, 17)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(77, 40)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "الطاولات"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlLegend
        '
        Me.pnlLegend.BackColor = System.Drawing.Color.White
        Me.pnlLegend.Controls.Add(Me.lblFree)
        Me.pnlLegend.Controls.Add(Me.lblBusy)
        Me.pnlLegend.Controls.Add(Me.lblCash)
        Me.pnlLegend.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlLegend.Location = New System.Drawing.Point(0, 641)
        Me.pnlLegend.Name = "pnlLegend"
        Me.pnlLegend.Size = New System.Drawing.Size(1120, 44)
        Me.pnlLegend.TabIndex = 1
        '
        'lblFree
        '
        Me.lblFree.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFree.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblFree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFree.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblFree.Location = New System.Drawing.Point(896, 8)
        Me.lblFree.Name = "lblFree"
        Me.lblFree.Size = New System.Drawing.Size(100, 27)
        Me.lblFree.TabIndex = 0
        Me.lblFree.Text = "فارغة"
        Me.lblFree.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBusy
        '
        Me.lblBusy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBusy.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(113, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.lblBusy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBusy.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblBusy.Location = New System.Drawing.Point(790, 8)
        Me.lblBusy.Name = "lblBusy"
        Me.lblBusy.Size = New System.Drawing.Size(100, 27)
        Me.lblBusy.TabIndex = 1
        Me.lblBusy.Text = "مشغولة"
        Me.lblBusy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCash
        '
        Me.lblCash.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCash.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(191, Byte), Integer))
        Me.lblCash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCash.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblCash.Location = New System.Drawing.Point(684, 8)
        Me.lblCash.Name = "lblCash"
        Me.lblCash.Size = New System.Drawing.Size(100, 27)
        Me.lblCash.TabIndex = 2
        Me.lblCash.Text = "سداد تلقائي"
        Me.lblCash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FloorCanvas
        '
        Me.FloorCanvas.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.FloorCanvas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FloorCanvas.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.FloorCanvas.IsDesignMode = False
        Me.FloorCanvas.Location = New System.Drawing.Point(0, 76)
        Me.FloorCanvas.Name = "FloorCanvas"
        Me.FloorCanvas.ShowGrid = False
        Me.FloorCanvas.Size = New System.Drawing.Size(1120, 565)
        Me.FloorCanvas.TabIndex = 2
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblStatus.ForeColor = System.Drawing.Color.White
        Me.lblStatus.Location = New System.Drawing.Point(0, 613)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(1120, 28)
        Me.lblStatus.TabIndex = 3
        Me.lblStatus.Text = "اختر الطاولة المطلوبة من المخطط"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FrmRestaurantFloorSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1120, 685)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.FloorCanvas)
        Me.Controls.Add(Me.pnlLegend)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.MinimumSize = New System.Drawing.Size(980, 620)
        Me.Name = "FrmRestaurantFloorSelector"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "اختيار الطاولة من المخطط"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlLegend.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnOldMenu As System.Windows.Forms.Button
    Friend WithEvents btnNoneTable As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents cmbFlates As System.Windows.Forms.ComboBox
    Friend WithEvents lblFlate As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlLegend As System.Windows.Forms.Panel
    Friend WithEvents lblFree As System.Windows.Forms.Label
    Friend WithEvents lblBusy As System.Windows.Forms.Label
    Friend WithEvents lblCash As System.Windows.Forms.Label
    Friend WithEvents FloorCanvas As RestaurantFloorDesignerControl
    Friend WithEvents lblStatus As System.Windows.Forms.Label
End Class
