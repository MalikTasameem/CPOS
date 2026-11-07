<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DraftCustomerSelector
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TitlePanel = New System.Windows.Forms.Panel()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.SearchLabel = New System.Windows.Forms.Label()
        Me.SearchTextBox = New System.Windows.Forms.TextBox()
        Me.CustomersGrid = New System.Windows.Forms.DataGridView()
        Me.CustomerIdColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustomerNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustomerPhoneColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustomerBarcodeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustomerTypeColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SourceStatusLabel = New System.Windows.Forms.Label()
        Me.SelectButton = New System.Windows.Forms.Button()
        Me.CashCustomerButton = New System.Windows.Forms.Button()
        Me.CancelButtonControl = New System.Windows.Forms.Button()
        Me.TitlePanel.SuspendLayout()
        CType(Me.CustomersGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TitlePanel
        '
        Me.TitlePanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.TitlePanel.Controls.Add(Me.TitleLabel)
        Me.TitlePanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitlePanel.Location = New System.Drawing.Point(0, 0)
        Me.TitlePanel.Name = "TitlePanel"
        Me.TitlePanel.Size = New System.Drawing.Size(884, 58)
        Me.TitlePanel.TabIndex = 0
        '
        'TitleLabel
        '
        Me.TitleLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 16.0!, System.Drawing.FontStyle.Bold)
        Me.TitleLabel.ForeColor = System.Drawing.Color.White
        Me.TitleLabel.Location = New System.Drawing.Point(0, 0)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(884, 58)
        Me.TitleLabel.TabIndex = 0
        Me.TitleLabel.Text = "اختيار عميل للمسودة"
        Me.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SearchLabel
        '
        Me.SearchLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SearchLabel.AutoSize = True
        Me.SearchLabel.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.SearchLabel.Location = New System.Drawing.Point(796, 78)
        Me.SearchLabel.Name = "SearchLabel"
        Me.SearchLabel.Size = New System.Drawing.Size(50, 20)
        Me.SearchLabel.TabIndex = 1
        Me.SearchLabel.Text = "البحث"
        '
        'SearchTextBox
        '
        Me.SearchTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SearchTextBox.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.SearchTextBox.Location = New System.Drawing.Point(24, 73)
        Me.SearchTextBox.Name = "SearchTextBox"
        Me.SearchTextBox.Size = New System.Drawing.Size(751, 29)
        Me.SearchTextBox.TabIndex = 0
        Me.SearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CustomersGrid
        '
        Me.CustomersGrid.AllowUserToAddRows = False
        Me.CustomersGrid.AllowUserToDeleteRows = False
        Me.CustomersGrid.AllowUserToResizeRows = False
        Me.CustomersGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CustomersGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.CustomersGrid.BackgroundColor = System.Drawing.Color.White
        Me.CustomersGrid.ColumnHeadersHeight = 36
        Me.CustomersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.CustomersGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CustomerIdColumn, Me.CustomerNameColumn, Me.CustomerPhoneColumn, Me.CustomerBarcodeColumn, Me.CustomerTypeColumn})
        Me.CustomersGrid.Location = New System.Drawing.Point(24, 117)
        Me.CustomersGrid.MultiSelect = False
        Me.CustomersGrid.Name = "CustomersGrid"
        Me.CustomersGrid.ReadOnly = True
        Me.CustomersGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CustomersGrid.RowHeadersVisible = False
        Me.CustomersGrid.RowTemplate.Height = 34
        Me.CustomersGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.CustomersGrid.Size = New System.Drawing.Size(836, 373)
        Me.CustomersGrid.TabIndex = 1
        '
        'CustomerIdColumn
        '
        Me.CustomerIdColumn.DataPropertyName = "AG_ID"
        Me.CustomerIdColumn.FillWeight = 40.0!
        Me.CustomerIdColumn.HeaderText = "الرقم"
        Me.CustomerIdColumn.Name = "CustomerIdColumn"
        Me.CustomerIdColumn.ReadOnly = True
        '
        'CustomerNameColumn
        '
        Me.CustomerNameColumn.DataPropertyName = "Ag_name"
        Me.CustomerNameColumn.FillWeight = 150.0!
        Me.CustomerNameColumn.HeaderText = "اسم العميل"
        Me.CustomerNameColumn.Name = "CustomerNameColumn"
        Me.CustomerNameColumn.ReadOnly = True
        '
        'CustomerPhoneColumn
        '
        Me.CustomerPhoneColumn.DataPropertyName = "Ag_phone"
        Me.CustomerPhoneColumn.FillWeight = 75.0!
        Me.CustomerPhoneColumn.HeaderText = "الهاتف"
        Me.CustomerPhoneColumn.Name = "CustomerPhoneColumn"
        Me.CustomerPhoneColumn.ReadOnly = True
        '
        'CustomerBarcodeColumn
        '
        Me.CustomerBarcodeColumn.DataPropertyName = "Barcode"
        Me.CustomerBarcodeColumn.FillWeight = 65.0!
        Me.CustomerBarcodeColumn.HeaderText = "الباركود"
        Me.CustomerBarcodeColumn.Name = "CustomerBarcodeColumn"
        Me.CustomerBarcodeColumn.ReadOnly = True
        '
        'CustomerTypeColumn
        '
        Me.CustomerTypeColumn.DataPropertyName = "Type_ID"
        Me.CustomerTypeColumn.HeaderText = "Type_ID"
        Me.CustomerTypeColumn.Name = "CustomerTypeColumn"
        Me.CustomerTypeColumn.ReadOnly = True
        Me.CustomerTypeColumn.Visible = False
        '
        'SourceStatusLabel
        '
        Me.SourceStatusLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SourceStatusLabel.BackColor = System.Drawing.Color.AliceBlue
        Me.SourceStatusLabel.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.SourceStatusLabel.Location = New System.Drawing.Point(24, 500)
        Me.SourceStatusLabel.Name = "SourceStatusLabel"
        Me.SourceStatusLabel.Padding = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.SourceStatusLabel.Size = New System.Drawing.Size(836, 34)
        Me.SourceStatusLabel.TabIndex = 3
        Me.SourceStatusLabel.Text = "جاري تحميل العملاء..."
        Me.SourceStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SelectButton
        '
        Me.SelectButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SelectButton.BackColor = System.Drawing.Color.SeaGreen
        Me.SelectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SelectButton.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.SelectButton.ForeColor = System.Drawing.Color.White
        Me.SelectButton.Location = New System.Drawing.Point(637, 546)
        Me.SelectButton.Name = "SelectButton"
        Me.SelectButton.Size = New System.Drawing.Size(223, 46)
        Me.SelectButton.TabIndex = 2
        Me.SelectButton.Text = "اختيار العميل"
        Me.SelectButton.UseVisualStyleBackColor = False
        '
        'CashCustomerButton
        '
        Me.CashCustomerButton.Anchor = CType(System.Windows.Forms.AnchorStyles.Bottom, System.Windows.Forms.AnchorStyles)
        Me.CashCustomerButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(43, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.CashCustomerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CashCustomerButton.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.CashCustomerButton.ForeColor = System.Drawing.Color.White
        Me.CashCustomerButton.Location = New System.Drawing.Point(331, 546)
        Me.CashCustomerButton.Name = "CashCustomerButton"
        Me.CashCustomerButton.Size = New System.Drawing.Size(223, 46)
        Me.CashCustomerButton.TabIndex = 3
        Me.CashCustomerButton.Text = "العميل النقدي"
        Me.CashCustomerButton.UseVisualStyleBackColor = False
        '
        'CancelButtonControl
        '
        Me.CancelButtonControl.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CancelButtonControl.BackColor = System.Drawing.Color.IndianRed
        Me.CancelButtonControl.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelButtonControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CancelButtonControl.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.CancelButtonControl.ForeColor = System.Drawing.Color.White
        Me.CancelButtonControl.Location = New System.Drawing.Point(24, 546)
        Me.CancelButtonControl.Name = "CancelButtonControl"
        Me.CancelButtonControl.Size = New System.Drawing.Size(223, 46)
        Me.CancelButtonControl.TabIndex = 4
        Me.CancelButtonControl.Text = "رجوع"
        Me.CancelButtonControl.UseVisualStyleBackColor = False
        '
        'DraftCustomerSelector
        '
        Me.AcceptButton = Me.SelectButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CancelButtonControl
        Me.ClientSize = New System.Drawing.Size(884, 611)
        Me.Controls.Add(Me.CancelButtonControl)
        Me.Controls.Add(Me.CashCustomerButton)
        Me.Controls.Add(Me.SelectButton)
        Me.Controls.Add(Me.SourceStatusLabel)
        Me.Controls.Add(Me.CustomersGrid)
        Me.Controls.Add(Me.SearchTextBox)
        Me.Controls.Add(Me.SearchLabel)
        Me.Controls.Add(Me.TitlePanel)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DraftCustomerSelector"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "اختيار عميل للمسودة"
        Me.TitlePanel.ResumeLayout(False)
        CType(Me.CustomersGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TitlePanel As Panel
    Friend WithEvents TitleLabel As Label
    Friend WithEvents SearchLabel As Label
    Friend WithEvents SearchTextBox As TextBox
    Friend WithEvents CustomersGrid As DataGridView
    Friend WithEvents CustomerIdColumn As DataGridViewTextBoxColumn
    Friend WithEvents CustomerNameColumn As DataGridViewTextBoxColumn
    Friend WithEvents CustomerPhoneColumn As DataGridViewTextBoxColumn
    Friend WithEvents CustomerBarcodeColumn As DataGridViewTextBoxColumn
    Friend WithEvents CustomerTypeColumn As DataGridViewTextBoxColumn
    Friend WithEvents SourceStatusLabel As Label
    Friend WithEvents SelectButton As Button
    Friend WithEvents CashCustomerButton As Button
    Friend WithEvents CancelButtonControl As Button
End Class
