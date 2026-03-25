
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPriceHistoryViewer
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then components.Dispose()
        MyBase.Dispose(disposing)
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.dgvHistory = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnExportExcel = New System.Windows.Forms.Button()
        Me.btnRollbackBatch = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.Batches_Cm = New System.Windows.Forms.ComboBox()
        CType(Me.dgvHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvHistory
        '
        Me.dgvHistory.AllowUserToAddRows = False
        Me.dgvHistory.AllowUserToDeleteRows = False
        Me.dgvHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvHistory.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvHistory.Location = New System.Drawing.Point(4, 65)
        Me.dgvHistory.Name = "dgvHistory"
        Me.dgvHistory.ReadOnly = True
        Me.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvHistory.Size = New System.Drawing.Size(1119, 550)
        Me.dgvHistory.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(44, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "BatchId:"
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(390, 15)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(90, 30)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "بحث"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnExportExcel
        '
        Me.btnExportExcel.Location = New System.Drawing.Point(490, 15)
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Size = New System.Drawing.Size(120, 30)
        Me.btnExportExcel.TabIndex = 4
        Me.btnExportExcel.Text = "تصدير Excel"
        Me.btnExportExcel.UseVisualStyleBackColor = True
        '
        'btnRollbackBatch
        '
        Me.btnRollbackBatch.Location = New System.Drawing.Point(620, 15)
        Me.btnRollbackBatch.Name = "btnRollbackBatch"
        Me.btnRollbackBatch.Size = New System.Drawing.Size(150, 30)
        Me.btnRollbackBatch.TabIndex = 5
        Me.btnRollbackBatch.Text = "التراجع عن الدفعة"
        Me.btnRollbackBatch.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(780, 15)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(120, 30)
        Me.btnPrint.TabIndex = 6
        Me.btnPrint.Text = "طباعة التقرير"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'Batches_Cm
        '
        Me.Batches_Cm.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Batches_Cm.FormattingEnabled = True
        Me.Batches_Cm.Location = New System.Drawing.Point(95, 15)
        Me.Batches_Cm.Name = "Batches_Cm"
        Me.Batches_Cm.Size = New System.Drawing.Size(280, 21)
        Me.Batches_Cm.TabIndex = 7
        '
        'FrmPriceHistoryViewer
        '
        Me.ClientSize = New System.Drawing.Size(1124, 616)
        Me.Controls.Add(Me.Batches_Cm)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnRollbackBatch)
        Me.Controls.Add(Me.btnExportExcel)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvHistory)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "FrmPriceHistoryViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "تقرير تعديل الأسعار حسب Batch"
        CType(Me.dgvHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgvHistory As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnExportExcel As Button
    Friend WithEvents btnRollbackBatch As Button
    Friend WithEvents btnPrint As Button
    Friend WithEvents Batches_Cm As ComboBox
End Class

