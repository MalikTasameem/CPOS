<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Outside_Sales_Touch_ItemEditor
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
        Me.RootLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.ItemNameLabel = New System.Windows.Forms.Label()
        Me.FieldsLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.StoreLabel = New System.Windows.Forms.Label()
        Me.StoreComboBox = New System.Windows.Forms.ComboBox()
        Me.UnitLabel = New System.Windows.Forms.Label()
        Me.UnitComboBox = New System.Windows.Forms.ComboBox()
        Me.QuantityLabel = New System.Windows.Forms.Label()
        Me.QuantityTextBox = New System.Windows.Forms.TextBox()
        Me.PriceLabel = New System.Windows.Forms.Label()
        Me.PriceTextBox = New System.Windows.Forms.TextBox()
        Me.ExpiryLabel = New System.Windows.Forms.Label()
        Me.ExpiryDatePicker = New System.Windows.Forms.DateTimePicker()
        Me.StockLabel = New System.Windows.Forms.Label()
        Me.KeypadLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.Key7 = New System.Windows.Forms.Button()
        Me.Key8 = New System.Windows.Forms.Button()
        Me.Key9 = New System.Windows.Forms.Button()
        Me.ClearButton = New System.Windows.Forms.Button()
        Me.Key4 = New System.Windows.Forms.Button()
        Me.Key5 = New System.Windows.Forms.Button()
        Me.Key6 = New System.Windows.Forms.Button()
        Me.BackspaceButton = New System.Windows.Forms.Button()
        Me.Key1 = New System.Windows.Forms.Button()
        Me.Key2 = New System.Windows.Forms.Button()
        Me.Key3 = New System.Windows.Forms.Button()
        Me.DecimalButton = New System.Windows.Forms.Button()
        Me.Key0 = New System.Windows.Forms.Button()
        Me.CommandsPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.CancelButtonControl = New System.Windows.Forms.Button()
        Me.ConfirmButton = New System.Windows.Forms.Button()
        Me.StatusLabel = New System.Windows.Forms.Label()
        Me.RootLayout.SuspendLayout()
        Me.FieldsLayout.SuspendLayout()
        Me.KeypadLayout.SuspendLayout()
        Me.CommandsPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'RootLayout
        '
        Me.RootLayout.ColumnCount = 1
        Me.RootLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.RootLayout.Controls.Add(Me.ItemNameLabel, 0, 0)
        Me.RootLayout.Controls.Add(Me.FieldsLayout, 0, 1)
        Me.RootLayout.Controls.Add(Me.KeypadLayout, 0, 2)
        Me.RootLayout.Controls.Add(Me.StatusLabel, 0, 3)
        Me.RootLayout.Controls.Add(Me.CommandsPanel, 0, 4)
        Me.RootLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RootLayout.Padding = New System.Windows.Forms.Padding(10)
        Me.RootLayout.RowCount = 5
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62.0!))
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62.0!))
        '
        'ItemNameLabel
        '
        Me.ItemNameLabel.BackColor = System.Drawing.Color.FromArgb(39, 55, 70)
        Me.ItemNameLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ItemNameLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 16.0!, System.Drawing.FontStyle.Bold)
        Me.ItemNameLabel.ForeColor = System.Drawing.Color.White
        Me.ItemNameLabel.Padding = New System.Windows.Forms.Padding(12, 0, 12, 0)
        Me.ItemNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FieldsLayout
        '
        Me.FieldsLayout.ColumnCount = 4
        Me.FieldsLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.FieldsLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.FieldsLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.FieldsLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.FieldsLayout.Controls.Add(Me.StoreLabel, 3, 0)
        Me.FieldsLayout.Controls.Add(Me.StoreComboBox, 3, 1)
        Me.FieldsLayout.Controls.Add(Me.UnitLabel, 2, 0)
        Me.FieldsLayout.Controls.Add(Me.UnitComboBox, 2, 1)
        Me.FieldsLayout.Controls.Add(Me.QuantityLabel, 1, 0)
        Me.FieldsLayout.Controls.Add(Me.QuantityTextBox, 1, 1)
        Me.FieldsLayout.Controls.Add(Me.PriceLabel, 0, 0)
        Me.FieldsLayout.Controls.Add(Me.PriceTextBox, 0, 1)
        Me.FieldsLayout.Controls.Add(Me.ExpiryLabel, 2, 2)
        Me.FieldsLayout.Controls.Add(Me.ExpiryDatePicker, 2, 3)
        Me.FieldsLayout.SetColumnSpan(Me.ExpiryDatePicker, 2)
        Me.FieldsLayout.Controls.Add(Me.StockLabel, 0, 2)
        Me.FieldsLayout.SetColumnSpan(Me.StockLabel, 2)
        Me.FieldsLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FieldsLayout.Padding = New System.Windows.Forms.Padding(4)
        Me.FieldsLayout.RowCount = 4
        Me.FieldsLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.FieldsLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.FieldsLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.FieldsLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        '
        'Field controls
        '
        Me.StoreLabel.Text = "المخزن"
        Me.UnitLabel.Text = "الوحدة"
        Me.QuantityLabel.Text = "الكمية"
        Me.PriceLabel.Text = "التكلفة"
        Me.ExpiryLabel.Text = "الصلاحية"
        Me.StockLabel.Text = "المتوفر في المخزن: 0.000"
        Me.StockLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.StoreComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.UnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ExpiryDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.QuantityTextBox.Text = "1"
        Me.QuantityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.PriceTextBox.Text = "0"
        Me.PriceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.StoreComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UnitComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuantityTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PriceTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExpiryDatePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StoreLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UnitLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuantityLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PriceLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExpiryLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StockLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.UnitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.QuantityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.PriceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ExpiryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'KeypadLayout
        '
        Me.KeypadLayout.ColumnCount = 4
        Me.KeypadLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.KeypadLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.KeypadLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.KeypadLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.KeypadLayout.Controls.Add(Me.Key7, 0, 0)
        Me.KeypadLayout.Controls.Add(Me.Key8, 1, 0)
        Me.KeypadLayout.Controls.Add(Me.Key9, 2, 0)
        Me.KeypadLayout.Controls.Add(Me.ClearButton, 3, 0)
        Me.KeypadLayout.Controls.Add(Me.Key4, 0, 1)
        Me.KeypadLayout.Controls.Add(Me.Key5, 1, 1)
        Me.KeypadLayout.Controls.Add(Me.Key6, 2, 1)
        Me.KeypadLayout.Controls.Add(Me.BackspaceButton, 3, 1)
        Me.KeypadLayout.Controls.Add(Me.Key1, 0, 2)
        Me.KeypadLayout.Controls.Add(Me.Key2, 1, 2)
        Me.KeypadLayout.Controls.Add(Me.Key3, 2, 2)
        Me.KeypadLayout.Controls.Add(Me.DecimalButton, 3, 2)
        Me.KeypadLayout.Controls.Add(Me.Key0, 0, 3)
        Me.KeypadLayout.SetColumnSpan(Me.Key0, 4)
        Me.KeypadLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.KeypadLayout.RowCount = 4
        Me.KeypadLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.KeypadLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.KeypadLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.KeypadLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        '
        'Keypad buttons
        '
        Me.Key7.Text = "7"
        Me.Key7.Tag = "7"
        Me.Key8.Text = "8"
        Me.Key8.Tag = "8"
        Me.Key9.Text = "9"
        Me.Key9.Tag = "9"
        Me.Key4.Text = "4"
        Me.Key4.Tag = "4"
        Me.Key5.Text = "5"
        Me.Key5.Tag = "5"
        Me.Key6.Text = "6"
        Me.Key6.Tag = "6"
        Me.Key1.Text = "1"
        Me.Key1.Tag = "1"
        Me.Key2.Text = "2"
        Me.Key2.Tag = "2"
        Me.Key3.Text = "3"
        Me.Key3.Tag = "3"
        Me.Key0.Text = "0"
        Me.Key0.Tag = "0"
        Me.ClearButton.Text = "مسح"
        Me.BackspaceButton.Text = "⌫"
        Me.DecimalButton.Text = "."
        Me.Key7.Dock = DockStyle.Fill
        Me.Key8.Dock = DockStyle.Fill
        Me.Key9.Dock = DockStyle.Fill
        Me.Key4.Dock = DockStyle.Fill
        Me.Key5.Dock = DockStyle.Fill
        Me.Key6.Dock = DockStyle.Fill
        Me.Key1.Dock = DockStyle.Fill
        Me.Key2.Dock = DockStyle.Fill
        Me.Key3.Dock = DockStyle.Fill
        Me.Key0.Dock = DockStyle.Fill
        Me.ClearButton.Dock = DockStyle.Fill
        Me.BackspaceButton.Dock = DockStyle.Fill
        Me.DecimalButton.Dock = DockStyle.Fill
        '
        'StatusLabel
        '
        Me.StatusLabel.Dock = DockStyle.Fill
        Me.StatusLabel.ForeColor = System.Drawing.Color.Firebrick
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CommandsPanel
        '
        Me.CommandsPanel.Controls.Add(Me.CancelButtonControl)
        Me.CommandsPanel.Controls.Add(Me.ConfirmButton)
        Me.CommandsPanel.Dock = DockStyle.Fill
        Me.CommandsPanel.FlowDirection = FlowDirection.RightToLeft
        Me.CommandsPanel.Padding = New Padding(4)
        Me.CancelButtonControl.Text = "رجوع"
        Me.CancelButtonControl.Size = New Size(160, 48)
        Me.ConfirmButton.Text = "إضافة إلى الإذن"
        Me.ConfirmButton.Size = New Size(220, 48)
        Me.ConfirmButton.BackColor = System.Drawing.Color.SeaGreen
        Me.ConfirmButton.ForeColor = System.Drawing.Color.White
        Me.ConfirmButton.FlatStyle = FlatStyle.Flat
        '
        'Outside_Sales_Touch_ItemEditor
        '
        Me.AcceptButton = Me.ConfirmButton
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.CancelButton = Me.CancelButtonControl
        Me.ClientSize = New System.Drawing.Size(720, 590)
        Me.Controls.Add(Me.RootLayout)
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Outside_Sales_Touch_ItemEditor"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "تخصيص الصنف"
        Me.RootLayout.ResumeLayout(False)
        Me.FieldsLayout.ResumeLayout(False)
        Me.FieldsLayout.PerformLayout()
        Me.KeypadLayout.ResumeLayout(False)
        Me.CommandsPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents RootLayout As TableLayoutPanel
    Friend WithEvents ItemNameLabel As Label
    Friend WithEvents FieldsLayout As TableLayoutPanel
    Friend WithEvents StoreLabel As Label
    Friend WithEvents StoreComboBox As ComboBox
    Friend WithEvents UnitLabel As Label
    Friend WithEvents UnitComboBox As ComboBox
    Friend WithEvents QuantityLabel As Label
    Friend WithEvents QuantityTextBox As TextBox
    Friend WithEvents PriceLabel As Label
    Friend WithEvents PriceTextBox As TextBox
    Friend WithEvents ExpiryLabel As Label
    Friend WithEvents ExpiryDatePicker As DateTimePicker
    Friend WithEvents StockLabel As Label
    Friend WithEvents KeypadLayout As TableLayoutPanel
    Friend WithEvents Key0 As Button
    Friend WithEvents Key1 As Button
    Friend WithEvents Key2 As Button
    Friend WithEvents Key3 As Button
    Friend WithEvents Key4 As Button
    Friend WithEvents Key5 As Button
    Friend WithEvents Key6 As Button
    Friend WithEvents Key7 As Button
    Friend WithEvents Key8 As Button
    Friend WithEvents Key9 As Button
    Friend WithEvents ClearButton As Button
    Friend WithEvents BackspaceButton As Button
    Friend WithEvents DecimalButton As Button
    Friend WithEvents CommandsPanel As FlowLayoutPanel
    Friend WithEvents CancelButtonControl As Button
    Friend WithEvents ConfirmButton As Button
    Friend WithEvents StatusLabel As Label
End Class
