<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ReturnOriginalPaymentsForm
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.BillNumberValueLabel = New System.Windows.Forms.Label()
        Me.BillNumberLabel = New System.Windows.Forms.Label()
        Me.TransactionTypeLabel = New System.Windows.Forms.Label()
        Me.PaymentsGrid = New System.Windows.Forms.DataGridView()
        Me.ReceiptNumberColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PaymentNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TreasuryNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PaymentDateColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AmountColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmptyPaymentsLabel = New System.Windows.Forms.Label()
        Me.SummaryPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.InvoiceTotalLabel = New System.Windows.Forms.Label()
        Me.InvoiceTotalValueLabel = New System.Windows.Forms.Label()
        Me.PaidTotalLabel = New System.Windows.Forms.Label()
        Me.PaidTotalValueLabel = New System.Windows.Forms.Label()
        Me.RemainingLabel = New System.Windows.Forms.Label()
        Me.RemainingValueLabel = New System.Windows.Forms.Label()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.HeaderPanel.SuspendLayout()
        CType(Me.PaymentsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SummaryPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'HeaderPanel
        '
        Me.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(30, 64, 175)
        Me.HeaderPanel.Controls.Add(Me.BillNumberValueLabel)
        Me.HeaderPanel.Controls.Add(Me.BillNumberLabel)
        Me.HeaderPanel.Controls.Add(Me.TransactionTypeLabel)
        Me.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.HeaderPanel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Size = New System.Drawing.Size(650, 72)
        '
        'BillNumberValueLabel
        '
        Me.BillNumberValueLabel.AutoSize = True
        Me.BillNumberValueLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.BillNumberValueLabel.ForeColor = System.Drawing.Color.White
        Me.BillNumberValueLabel.Location = New System.Drawing.Point(24, 40)
        Me.BillNumberValueLabel.Name = "BillNumberValueLabel"
        Me.BillNumberValueLabel.Text = "---"
        '
        'BillNumberLabel
        '
        Me.BillNumberLabel.AutoSize = True
        Me.BillNumberLabel.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular)
        Me.BillNumberLabel.ForeColor = System.Drawing.Color.White
        Me.BillNumberLabel.Location = New System.Drawing.Point(95, 41)
        Me.BillNumberLabel.Name = "BillNumberLabel"
        Me.BillNumberLabel.Text = "رقم الفاتورة:"
        '
        'TransactionTypeLabel
        '
        Me.TransactionTypeLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TransactionTypeLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 15.0!, System.Drawing.FontStyle.Bold)
        Me.TransactionTypeLabel.ForeColor = System.Drawing.Color.White
        Me.TransactionTypeLabel.Location = New System.Drawing.Point(0, 0)
        Me.TransactionTypeLabel.Name = "TransactionTypeLabel"
        Me.TransactionTypeLabel.Size = New System.Drawing.Size(650, 38)
        Me.TransactionTypeLabel.Text = "دفعات الفاتورة الأصلية"
        Me.TransactionTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PaymentsGrid
        '
        Me.PaymentsGrid.AllowUserToAddRows = False
        Me.PaymentsGrid.AllowUserToDeleteRows = False
        Me.PaymentsGrid.AllowUserToResizeRows = False
        Me.PaymentsGrid.AutoGenerateColumns = False
        Me.PaymentsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.PaymentsGrid.BackgroundColor = System.Drawing.Color.White
        Me.PaymentsGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PaymentsGrid.ColumnHeadersHeight = 34
        Me.PaymentsGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ReceiptNumberColumn, Me.PaymentNameColumn, Me.TreasuryNameColumn, Me.PaymentDateColumn, Me.AmountColumn})
        Me.PaymentsGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PaymentsGrid.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.PaymentsGrid.Location = New System.Drawing.Point(0, 72)
        Me.PaymentsGrid.MultiSelect = False
        Me.PaymentsGrid.Name = "PaymentsGrid"
        Me.PaymentsGrid.ReadOnly = True
        Me.PaymentsGrid.RowHeadersVisible = False
        Me.PaymentsGrid.RowTemplate.Height = 30
        Me.PaymentsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        '
        'Columns
        '
        Me.ReceiptNumberColumn.DataPropertyName = "Receipt_Num"
        Me.ReceiptNumberColumn.HeaderText = "رقم السند"
        Me.ReceiptNumberColumn.Name = "ReceiptNumberColumn"
        Me.ReceiptNumberColumn.ReadOnly = True
        Me.PaymentNameColumn.DataPropertyName = "PaymentName"
        Me.PaymentNameColumn.HeaderText = "طريقة الدفع"
        Me.PaymentNameColumn.Name = "PaymentNameColumn"
        Me.PaymentNameColumn.ReadOnly = True
        Me.TreasuryNameColumn.DataPropertyName = "TreasuryName"
        Me.TreasuryNameColumn.HeaderText = "الخزينة"
        Me.TreasuryNameColumn.Name = "TreasuryNameColumn"
        Me.TreasuryNameColumn.ReadOnly = True
        Me.PaymentDateColumn.DataPropertyName = "Date"
        Me.PaymentDateColumn.DefaultCellStyle.Format = "yyyy/MM/dd HH:mm"
        Me.PaymentDateColumn.HeaderText = "التاريخ"
        Me.PaymentDateColumn.Name = "PaymentDateColumn"
        Me.PaymentDateColumn.ReadOnly = True
        Me.AmountColumn.DataPropertyName = "Amount"
        Me.AmountColumn.DefaultCellStyle.Format = "N3"
        Me.AmountColumn.HeaderText = "القيمة"
        Me.AmountColumn.Name = "AmountColumn"
        Me.AmountColumn.ReadOnly = True
        '
        'EmptyPaymentsLabel
        '
        Me.EmptyPaymentsLabel.BackColor = System.Drawing.Color.White
        Me.EmptyPaymentsLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EmptyPaymentsLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.EmptyPaymentsLabel.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139)
        Me.EmptyPaymentsLabel.Location = New System.Drawing.Point(0, 72)
        Me.EmptyPaymentsLabel.Name = "EmptyPaymentsLabel"
        Me.EmptyPaymentsLabel.Size = New System.Drawing.Size(650, 266)
        Me.EmptyPaymentsLabel.Text = "لا توجد دفعات مسجلة لهذه الفاتورة؛ قد تكون الفاتورة آجلة."
        Me.EmptyPaymentsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.EmptyPaymentsLabel.Visible = False
        '
        'SummaryPanel
        '
        Me.SummaryPanel.BackColor = System.Drawing.Color.FromArgb(248, 250, 252)
        Me.SummaryPanel.ColumnCount = 2
        Me.SummaryPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.SummaryPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.SummaryPanel.Controls.Add(Me.InvoiceTotalLabel, 0, 0)
        Me.SummaryPanel.Controls.Add(Me.InvoiceTotalValueLabel, 1, 0)
        Me.SummaryPanel.Controls.Add(Me.PaidTotalLabel, 0, 1)
        Me.SummaryPanel.Controls.Add(Me.PaidTotalValueLabel, 1, 1)
        Me.SummaryPanel.Controls.Add(Me.RemainingLabel, 0, 2)
        Me.SummaryPanel.Controls.Add(Me.RemainingValueLabel, 1, 2)
        Me.SummaryPanel.Controls.Add(Me.CloseButton, 0, 3)
        Me.SummaryPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.SummaryPanel.Location = New System.Drawing.Point(0, 338)
        Me.SummaryPanel.Name = "SummaryPanel"
        Me.SummaryPanel.RowCount = 4
        Me.SummaryPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.SummaryPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.SummaryPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.SummaryPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48.0!))
        Me.SummaryPanel.Size = New System.Drawing.Size(650, 148)
        '
        'Summary labels
        '
        ConfigureSummaryLabel(Me.InvoiceTotalLabel, "إجمالي الفاتورة")
        ConfigureSummaryLabel(Me.PaidTotalLabel, "المدفوع")
        ConfigureSummaryLabel(Me.RemainingLabel, "المتبقي")
        ConfigureSummaryValueLabel(Me.InvoiceTotalValueLabel)
        ConfigureSummaryValueLabel(Me.PaidTotalValueLabel)
        ConfigureSummaryValueLabel(Me.RemainingValueLabel)
        Me.RemainingValueLabel.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28)
        '
        'CloseButton
        '
        Me.SummaryPanel.SetColumnSpan(Me.CloseButton, 2)
        Me.CloseButton.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.CloseButton.BackColor = System.Drawing.Color.FromArgb(71, 85, 105)
        Me.CloseButton.FlatAppearance.BorderSize = 0
        Me.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.CloseButton.ForeColor = System.Drawing.Color.White
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(130, 34)
        Me.CloseButton.Text = "إغلاق"
        Me.CloseButton.UseVisualStyleBackColor = False
        '
        'ReturnOriginalPaymentsForm
        '
        Me.AcceptButton = Me.CloseButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(650, 486)
        Me.Controls.Add(Me.EmptyPaymentsLabel)
        Me.Controls.Add(Me.PaymentsGrid)
        Me.Controls.Add(Me.SummaryPanel)
        Me.Controls.Add(Me.HeaderPanel)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ReturnOriginalPaymentsForm"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "دفعات الفاتورة الأصلية"
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        CType(Me.PaymentsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SummaryPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Private Sub ConfigureSummaryLabel(label As System.Windows.Forms.Label, text As String)
        label.Dock = System.Windows.Forms.DockStyle.Fill
        label.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        label.Text = text
        label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    End Sub

    Private Sub ConfigureSummaryValueLabel(label As System.Windows.Forms.Label)
        label.Dock = System.Windows.Forms.DockStyle.Fill
        label.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        label.Text = "0.000"
        label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    End Sub

    Friend WithEvents HeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents BillNumberValueLabel As System.Windows.Forms.Label
    Friend WithEvents BillNumberLabel As System.Windows.Forms.Label
    Friend WithEvents TransactionTypeLabel As System.Windows.Forms.Label
    Friend WithEvents PaymentsGrid As System.Windows.Forms.DataGridView
    Friend WithEvents ReceiptNumberColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PaymentNameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TreasuryNameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PaymentDateColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AmountColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmptyPaymentsLabel As System.Windows.Forms.Label
    Friend WithEvents SummaryPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents InvoiceTotalLabel As System.Windows.Forms.Label
    Friend WithEvents InvoiceTotalValueLabel As System.Windows.Forms.Label
    Friend WithEvents PaidTotalLabel As System.Windows.Forms.Label
    Friend WithEvents PaidTotalValueLabel As System.Windows.Forms.Label
    Friend WithEvents RemainingLabel As System.Windows.Forms.Label
    Friend WithEvents RemainingValueLabel As System.Windows.Forms.Label
    Friend WithEvents CloseButton As System.Windows.Forms.Button
End Class
