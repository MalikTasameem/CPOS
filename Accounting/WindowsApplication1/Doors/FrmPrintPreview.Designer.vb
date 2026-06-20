<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPrintPreview
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then components.Dispose()
        MyBase.Dispose(disposing)
    End Sub

    <System.Diagnostics.DebuggerNonUserCode()>
    Private Sub InitializeComponent()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlActions = New System.Windows.Forms.Panel()
        Me.lblPageNav = New System.Windows.Forms.Label()
        Me.btnPrevPage = New System.Windows.Forms.Button()
        Me.btnNextPage = New System.Windows.Forms.Button()
        Me.btnFirstPage = New System.Windows.Forms.Button()
        Me.btnExportPdf = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.preview = New System.Windows.Forms.PrintPreviewControl()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.pnlHeader.SuspendLayout()
        Me.pnlActions.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1200, 60)
        Me.pnlHeader.TabIndex = 2
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 13.5!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(980, 15)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(115, 25)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "معاينة التقرير"
        '
        'pnlActions
        '
        Me.pnlActions.BackColor = System.Drawing.Color.White
        Me.pnlActions.Controls.Add(Me.lblPageNav)
        Me.pnlActions.Controls.Add(Me.btnPrevPage)
        Me.pnlActions.Controls.Add(Me.btnNextPage)
        Me.pnlActions.Controls.Add(Me.btnFirstPage)
        Me.pnlActions.Controls.Add(Me.btnExportPdf)
        Me.pnlActions.Controls.Add(Me.btnPrint)
        Me.pnlActions.Controls.Add(Me.btnClose)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlActions.Location = New System.Drawing.Point(0, 60)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Size = New System.Drawing.Size(1200, 50)
        Me.pnlActions.TabIndex = 1
        '
        'lblPageNav
        '
        Me.lblPageNav.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPageNav.Location = New System.Drawing.Point(366, 14)
        Me.lblPageNav.Name = "lblPageNav"
        Me.lblPageNav.Size = New System.Drawing.Size(112, 22)
        Me.lblPageNav.TabIndex = 6
        Me.lblPageNav.Text = "صفحة 1"
        Me.lblPageNav.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnPrevPage
        '
        Me.btnPrevPage.Location = New System.Drawing.Point(240, 8)
        Me.btnPrevPage.Name = "btnPrevPage"
        Me.btnPrevPage.Size = New System.Drawing.Size(120, 34)
        Me.btnPrevPage.TabIndex = 5
        Me.btnPrevPage.Text = "السابقة"
        '
        'btnNextPage
        '
        Me.btnNextPage.Location = New System.Drawing.Point(484, 8)
        Me.btnNextPage.Name = "btnNextPage"
        Me.btnNextPage.Size = New System.Drawing.Size(120, 34)
        Me.btnNextPage.TabIndex = 4
        Me.btnNextPage.Text = "التالية"
        '
        'btnFirstPage
        '
        Me.btnFirstPage.Location = New System.Drawing.Point(114, 8)
        Me.btnFirstPage.Name = "btnFirstPage"
        Me.btnFirstPage.Size = New System.Drawing.Size(120, 34)
        Me.btnFirstPage.TabIndex = 3
        Me.btnFirstPage.Text = "الأولى"
        '
        'btnExportPdf
        '
        Me.btnExportPdf.Location = New System.Drawing.Point(904, 8)
        Me.btnExportPdf.Name = "btnExportPdf"
        Me.btnExportPdf.Size = New System.Drawing.Size(120, 34)
        Me.btnExportPdf.TabIndex = 2
        Me.btnExportPdf.Text = "تصدير PDF"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(1030, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(120, 34)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "طباعة"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(700, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(120, 34)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "خروج"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'preview
        '
        Me.preview.AutoZoom = False
        Me.preview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.preview.Location = New System.Drawing.Point(0, 110)
        Me.preview.Name = "preview"
        Me.preview.Size = New System.Drawing.Size(1200, 628)
        Me.preview.TabIndex = 0
        Me.preview.Zoom = 1.0R
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 738)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1200, 22)
        Me.StatusStrip1.TabIndex = 3
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(30, 17)
        Me.lblStatus.Text = "جاهز"
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'FrmPrintPreview
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1200, 760)
        Me.Controls.Add(Me.preview)
        Me.Controls.Add(Me.pnlActions)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.MinimumSize = New System.Drawing.Size(1050, 650)
        Me.Name = "FrmPrintPreview"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "معاينة الطباعة"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlActions.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnPrint As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents preview As PrintPreviewControl
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatus As ToolStripStatusLabel
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintDialog1 As PrintDialog
    Friend WithEvents btnExportPdf As Button
    Friend WithEvents lblPageNav As Label
    Friend WithEvents btnPrevPage As Button
    Friend WithEvents btnNextPage As Button
    Friend WithEvents btnFirstPage As Button
End Class
