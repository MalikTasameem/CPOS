<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SalesPaymentReconciliationForm
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
        Me.HeaderLabel = New System.Windows.Forms.Label()
        Me.BillNumberLabel = New System.Windows.Forms.Label()
        Me.BillNumberValueLabel = New System.Windows.Forms.Label()
        Me.SummaryTable = New System.Windows.Forms.TableLayoutPanel()
        Me.OriginalPureTitleLabel = New System.Windows.Forms.Label()
        Me.OriginalPureValueLabel = New System.Windows.Forms.Label()
        Me.NewPureTitleLabel = New System.Windows.Forms.Label()
        Me.NewPureValueLabel = New System.Windows.Forms.Label()
        Me.PaidTitleLabel = New System.Windows.Forms.Label()
        Me.PaidValueLabel = New System.Windows.Forms.Label()
        Me.AdjustmentTitleLabel = New System.Windows.Forms.Label()
        Me.AdjustmentValueLabel = New System.Windows.Forms.Label()
        Me.OriginalPaymentsLabel = New System.Windows.Forms.Label()
        Me.OriginalPaymentsGrid = New System.Windows.Forms.DataGridView()
        Me.SelectedPaymentsLabel = New System.Windows.Forms.Label()
        Me.SelectedPaymentsGrid = New System.Windows.Forms.DataGridView()
        Me.SelectionStatusLabel = New System.Windows.Forms.Label()
        Me.SelectPaymentsButton = New System.Windows.Forms.Button()
        Me.ConfirmButton = New System.Windows.Forms.Button()
        Me.CancelButtonControl = New System.Windows.Forms.Button()
        Me.SummaryTable.SuspendLayout()
        CType(Me.OriginalPaymentsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SelectedPaymentsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'HeaderLabel
        '
        Me.HeaderLabel.BackColor = System.Drawing.Color.FromArgb(30, 64, 175)
        Me.HeaderLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.HeaderLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 15.0!, System.Drawing.FontStyle.Bold)
        Me.HeaderLabel.ForeColor = System.Drawing.Color.White
        Me.HeaderLabel.Location = New System.Drawing.Point(0, 0)
        Me.HeaderLabel.Name = "HeaderLabel"
        Me.HeaderLabel.Size = New System.Drawing.Size(760, 48)
        Me.HeaderLabel.Text = "تسوية دفعات الفاتورة المعدلة"
        Me.HeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Bill labels
        '
        Me.BillNumberLabel.AutoSize = True
        Me.BillNumberLabel.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.BillNumberLabel.Location = New System.Drawing.Point(635, 61)
        Me.BillNumberLabel.Text = "رقم الفاتورة:"
        Me.BillNumberValueLabel.AutoSize = True
        Me.BillNumberValueLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.BillNumberValueLabel.Location = New System.Drawing.Point(560, 61)
        Me.BillNumberValueLabel.Text = "---"
        '
        'SummaryTable
        '
        Me.SummaryTable.ColumnCount = 4
        Me.SummaryTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.SummaryTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.SummaryTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.SummaryTable.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.SummaryTable.Controls.Add(Me.OriginalPureTitleLabel, 0, 0)
        Me.SummaryTable.Controls.Add(Me.NewPureTitleLabel, 1, 0)
        Me.SummaryTable.Controls.Add(Me.PaidTitleLabel, 2, 0)
        Me.SummaryTable.Controls.Add(Me.AdjustmentTitleLabel, 3, 0)
        Me.SummaryTable.Controls.Add(Me.OriginalPureValueLabel, 0, 1)
        Me.SummaryTable.Controls.Add(Me.NewPureValueLabel, 1, 1)
        Me.SummaryTable.Controls.Add(Me.PaidValueLabel, 2, 1)
        Me.SummaryTable.Controls.Add(Me.AdjustmentValueLabel, 3, 1)
        Me.SummaryTable.Location = New System.Drawing.Point(12, 86)
        Me.SummaryTable.Name = "SummaryTable"
        Me.SummaryTable.RowCount = 2
        Me.SummaryTable.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.SummaryTable.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.0!))
        Me.SummaryTable.Size = New System.Drawing.Size(736, 72)
        '
        'Summary values
        '
        ConfigureTitle(Me.OriginalPureTitleLabel, "الصافي القديم")
        ConfigureTitle(Me.NewPureTitleLabel, "الصافي الجديد")
        ConfigureTitle(Me.PaidTitleLabel, "صافي المدفوع")
        ConfigureTitle(Me.AdjustmentTitleLabel, "الفرق")
        ConfigureValue(Me.OriginalPureValueLabel)
        ConfigureValue(Me.NewPureValueLabel)
        ConfigureValue(Me.PaidValueLabel)
        ConfigureValue(Me.AdjustmentValueLabel)
        '
        'OriginalPaymentsLabel
        '
        Me.OriginalPaymentsLabel.AutoSize = True
        Me.OriginalPaymentsLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.OriginalPaymentsLabel.Location = New System.Drawing.Point(608, 171)
        Me.OriginalPaymentsLabel.Text = "الدفعات الأصلية"
        '
        'OriginalPaymentsGrid
        '
        ConfigureGrid(Me.OriginalPaymentsGrid)
        Me.OriginalPaymentsGrid.Location = New System.Drawing.Point(12, 194)
        Me.OriginalPaymentsGrid.Size = New System.Drawing.Size(736, 125)
        '
        'SelectedPaymentsLabel
        '
        Me.SelectedPaymentsLabel.AutoSize = True
        Me.SelectedPaymentsLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SelectedPaymentsLabel.Location = New System.Drawing.Point(586, 331)
        Me.SelectedPaymentsLabel.Text = "توزيع حركة التسوية"
        '
        'SelectedPaymentsGrid
        '
        ConfigureGrid(Me.SelectedPaymentsGrid)
        Me.SelectedPaymentsGrid.Location = New System.Drawing.Point(12, 354)
        Me.SelectedPaymentsGrid.Size = New System.Drawing.Size(736, 112)
        '
        'SelectionStatusLabel
        '
        Me.SelectionStatusLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.SelectionStatusLabel.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105)
        Me.SelectionStatusLabel.Location = New System.Drawing.Point(12, 476)
        Me.SelectionStatusLabel.Size = New System.Drawing.Size(736, 24)
        Me.SelectionStatusLabel.Text = "حدد طرق تسوية الفرق قبل المتابعة."
        Me.SelectionStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Buttons
        '
        ConfigureButton(Me.SelectPaymentsButton, "تحديد طرق التسوية", System.Drawing.Color.FromArgb(3, 105, 161))
        Me.SelectPaymentsButton.Location = New System.Drawing.Point(536, 512)
        ConfigureButton(Me.ConfirmButton, "متابعة", System.Drawing.Color.FromArgb(21, 128, 61))
        Me.ConfirmButton.Enabled = False
        Me.ConfirmButton.Location = New System.Drawing.Point(284, 512)
        ConfigureButton(Me.CancelButtonControl, "إلغاء", System.Drawing.Color.FromArgb(100, 116, 139))
        Me.CancelButtonControl.Location = New System.Drawing.Point(32, 512)
        '
        'Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(760, 566)
        Me.Controls.Add(Me.CancelButtonControl)
        Me.Controls.Add(Me.ConfirmButton)
        Me.Controls.Add(Me.SelectPaymentsButton)
        Me.Controls.Add(Me.SelectionStatusLabel)
        Me.Controls.Add(Me.SelectedPaymentsGrid)
        Me.Controls.Add(Me.SelectedPaymentsLabel)
        Me.Controls.Add(Me.OriginalPaymentsGrid)
        Me.Controls.Add(Me.OriginalPaymentsLabel)
        Me.Controls.Add(Me.SummaryTable)
        Me.Controls.Add(Me.BillNumberValueLabel)
        Me.Controls.Add(Me.BillNumberLabel)
        Me.Controls.Add(Me.HeaderLabel)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SalesPaymentReconciliationForm"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "تسوية دفعات الفاتورة"
        Me.SummaryTable.ResumeLayout(False)
        CType(Me.OriginalPaymentsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SelectedPaymentsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Private Sub ConfigureTitle(label As Label, text As String)
        label.Dock = DockStyle.Fill
        label.Font = New Font("Segoe UI", 9.0!, FontStyle.Regular)
        label.Text = text
        label.TextAlign = ContentAlignment.MiddleCenter
    End Sub

    Private Sub ConfigureValue(label As Label)
        label.Dock = DockStyle.Fill
        label.Font = New Font("Segoe UI Semibold", 12.0!, FontStyle.Bold)
        label.Text = "0.000"
        label.TextAlign = ContentAlignment.MiddleCenter
    End Sub

    Private Sub ConfigureGrid(grid As DataGridView)
        grid.AllowUserToAddRows = False
        grid.AllowUserToDeleteRows = False
        grid.AllowUserToResizeRows = False
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        grid.BackgroundColor = Color.White
        grid.Font = New Font("Segoe UI Semibold", 9.0!, FontStyle.Bold)
        grid.ReadOnly = True
        grid.RowHeadersVisible = False
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub ConfigureButton(button As Button, text As String, backColor As Color)
        button.BackColor = backColor
        button.FlatAppearance.BorderSize = 0
        button.FlatStyle = FlatStyle.Flat
        button.Font = New Font("Segoe UI Semibold", 10.0!, FontStyle.Bold)
        button.ForeColor = Color.White
        button.Size = New Size(192, 40)
        button.Text = text
        button.UseVisualStyleBackColor = False
    End Sub

    Friend WithEvents HeaderLabel As Label
    Friend WithEvents BillNumberLabel As Label
    Friend WithEvents BillNumberValueLabel As Label
    Friend WithEvents SummaryTable As TableLayoutPanel
    Friend WithEvents OriginalPureTitleLabel As Label
    Friend WithEvents OriginalPureValueLabel As Label
    Friend WithEvents NewPureTitleLabel As Label
    Friend WithEvents NewPureValueLabel As Label
    Friend WithEvents PaidTitleLabel As Label
    Friend WithEvents PaidValueLabel As Label
    Friend WithEvents AdjustmentTitleLabel As Label
    Friend WithEvents AdjustmentValueLabel As Label
    Friend WithEvents OriginalPaymentsLabel As Label
    Friend WithEvents OriginalPaymentsGrid As DataGridView
    Friend WithEvents SelectedPaymentsLabel As Label
    Friend WithEvents SelectedPaymentsGrid As DataGridView
    Friend WithEvents SelectionStatusLabel As Label
    Friend WithEvents SelectPaymentsButton As Button
    Friend WithEvents ConfirmButton As Button
    Friend WithEvents CancelButtonControl As Button
End Class
