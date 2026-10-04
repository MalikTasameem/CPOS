<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Outside_Sales_Touch
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Outside_Sales_Touch))
        Me.RootLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.HeaderPanel = New System.Windows.Forms.Panel()
        Me.StatusLabel = New System.Windows.Forms.Label()
        Me.VoidLabel = New System.Windows.Forms.Label()
        Me.EmployeeSearch = New resturant.FSearch_Filter()
        Me.EmployeeLabel = New System.Windows.Forms.Label()
        Me.NotesTextBox = New System.Windows.Forms.TextBox()
        Me.TitleTextBox = New System.Windows.Forms.TextBox()
        Me.BillDatePicker = New System.Windows.Forms.DateTimePicker()
        Me.BillNumberTextBox = New System.Windows.Forms.TextBox()
        Me.ContentSplit = New System.Windows.Forms.SplitContainer()
        Me.CartLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.CartGrid = New System.Windows.Forms.DataGridView()
        Me.CartToolsPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.DeleteLineButton = New System.Windows.Forms.Button()
        Me.DecreaseLineButton = New System.Windows.Forms.Button()
        Me.IncreaseLineButton = New System.Windows.Forms.Button()
        Me.SummaryPanel = New System.Windows.Forms.Panel()
        Me.ExpectedSalesValueLabel = New System.Windows.Forms.Label()
        Me.TotalCostLabel = New System.Windows.Forms.Label()
        Me.TotalQuantityLabel = New System.Windows.Forms.Label()
        Me.ItemsCountLabel = New System.Windows.Forms.Label()
        Me.ItemsLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.SearchPanel = New System.Windows.Forms.Panel()
        Me.SearchTextBox = New System.Windows.Forms.TextBox()
        Me.SearchLabel = New System.Windows.Forms.Label()
        Me.CategoryNavigationPanel = New System.Windows.Forms.Panel()
        Me.CategoryFlowPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.CategoryPreviousButton = New System.Windows.Forms.Button()
        Me.CategoryNextButton = New System.Windows.Forms.Button()
        Me.ItemsFlowPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.CommandsPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.SearchBillsButton = New System.Windows.Forms.Button()
        Me.CancelBillButton = New System.Windows.Forms.Button()
        Me.PrintButton = New System.Windows.Forms.Button()
        Me.EditButton = New System.Windows.Forms.Button()
        Me.SaveButton = New System.Windows.Forms.Button()
        Me.NewButton = New System.Windows.Forms.Button()
        Me.QuantityKeypad = New System.Windows.Forms.TableLayoutPanel()
        Me.QuantityKey7Button = New System.Windows.Forms.Button()
        Me.QuantityKey8Button = New System.Windows.Forms.Button()
        Me.QuantityKey9Button = New System.Windows.Forms.Button()
        Me.QuantityClearButton = New System.Windows.Forms.Button()
        Me.QuantityKey4Button = New System.Windows.Forms.Button()
        Me.QuantityKey5Button = New System.Windows.Forms.Button()
        Me.QuantityKey6Button = New System.Windows.Forms.Button()
        Me.QuantityBackspaceButton = New System.Windows.Forms.Button()
        Me.QuantityKey1Button = New System.Windows.Forms.Button()
        Me.QuantityKey2Button = New System.Windows.Forms.Button()
        Me.QuantityKey3Button = New System.Windows.Forms.Button()
        Me.QuantityDecimalButton = New System.Windows.Forms.Button()
        Me.QuantityKey0Button = New System.Windows.Forms.Button()
        Me.AddItemButton = New System.Windows.Forms.Button()
        Me.EditorPanel = New System.Windows.Forms.Panel()
        Me.ExpiryDatePicker = New System.Windows.Forms.DateTimePicker()
        Me.ExpiryLabel = New System.Windows.Forms.Label()
        Me.PriceTextBox = New System.Windows.Forms.TextBox()
        Me.PriceLabel = New System.Windows.Forms.Label()
        Me.QuantityTextBox = New System.Windows.Forms.TextBox()
        Me.QuantityLabel = New System.Windows.Forms.Label()
        Me.UnitComboBox = New System.Windows.Forms.ComboBox()
        Me.UnitLabel = New System.Windows.Forms.Label()
        Me.StoreComboBox = New System.Windows.Forms.ComboBox()
        Me.StoreLabel = New System.Windows.Forms.Label()
        Me.StockLabel = New System.Windows.Forms.Label()
        Me.SelectedItemLabel = New System.Windows.Forms.Label()
        Me.CostWarningLabel = New System.Windows.Forms.Label()
        Me.RootLayout.SuspendLayout()
        Me.HeaderPanel.SuspendLayout()
        CType(Me.ContentSplit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContentSplit.Panel1.SuspendLayout()
        Me.ContentSplit.Panel2.SuspendLayout()
        Me.ContentSplit.SuspendLayout()
        Me.CartLayout.SuspendLayout()
        CType(Me.CartGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CartToolsPanel.SuspendLayout()
        Me.SummaryPanel.SuspendLayout()
        Me.ItemsLayout.SuspendLayout()
        Me.SearchPanel.SuspendLayout()
        Me.CategoryNavigationPanel.SuspendLayout()
        Me.CommandsPanel.SuspendLayout()
        Me.QuantityKeypad.SuspendLayout()
        Me.EditorPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'RootLayout
        '
        Me.RootLayout.ColumnCount = 1
        Me.RootLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.RootLayout.Controls.Add(Me.HeaderPanel, 0, 0)
        Me.RootLayout.Controls.Add(Me.ContentSplit, 0, 1)
        Me.RootLayout.Controls.Add(Me.CommandsPanel, 0, 3)
        Me.RootLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RootLayout.Location = New System.Drawing.Point(0, 0)
        Me.RootLayout.Name = "RootLayout"
        Me.RootLayout.RowCount = 4
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 81.0!))
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0!))
        Me.RootLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54.0!))
        Me.RootLayout.Size = New System.Drawing.Size(1016, 581)
        Me.RootLayout.TabIndex = 0
        '
        'HeaderPanel
        '
        Me.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.HeaderPanel.Controls.Add(Me.StatusLabel)
        Me.HeaderPanel.Controls.Add(Me.VoidLabel)
        Me.HeaderPanel.Controls.Add(Me.EmployeeSearch)
        Me.HeaderPanel.Controls.Add(Me.EmployeeLabel)
        Me.HeaderPanel.Controls.Add(Me.NotesTextBox)
        Me.HeaderPanel.Controls.Add(Me.TitleTextBox)
        Me.HeaderPanel.Controls.Add(Me.BillDatePicker)
        Me.HeaderPanel.Controls.Add(Me.BillNumberTextBox)
        Me.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HeaderPanel.Location = New System.Drawing.Point(3, 3)
        Me.HeaderPanel.Name = "HeaderPanel"
        Me.HeaderPanel.Size = New System.Drawing.Size(1010, 75)
        Me.HeaderPanel.TabIndex = 0
        '
        'StatusLabel
        '
        Me.StatusLabel.AutoEllipsis = True
        Me.StatusLabel.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.StatusLabel.Location = New System.Drawing.Point(3, 32)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(245, 28)
        Me.StatusLabel.TabIndex = 0
        Me.StatusLabel.Text = "جاهز"
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'VoidLabel
        '
        Me.VoidLabel.BackColor = System.Drawing.Color.Firebrick
        Me.VoidLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.VoidLabel.ForeColor = System.Drawing.Color.White
        Me.VoidLabel.Location = New System.Drawing.Point(263, 32)
        Me.VoidLabel.Name = "VoidLabel"
        Me.VoidLabel.Size = New System.Drawing.Size(150, 28)
        Me.VoidLabel.TabIndex = 1
        Me.VoidLabel.Text = "الإذن ملغى"
        Me.VoidLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.VoidLabel.Visible = False
        '
        'EmployeeSearch
        '
        Me.EmployeeSearch.CancelSearchImage = CType(resources.GetObject("EmployeeSearch.CancelSearchImage"), System.Drawing.Image)
        Me.EmployeeSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.EmployeeSearch.Location = New System.Drawing.Point(416, 31)
        Me.EmployeeSearch.Name = "EmployeeSearch"
        Me.EmployeeSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.EmployeeSearch.Size = New System.Drawing.Size(522, 30)
        Me.EmployeeSearch.SQL_Column = "Ag_name"
        Me.EmployeeSearch.SQL_ID = "AG_ID"
        Me.EmployeeSearch.SQL_IsNumericSearchField = False
        Me.EmployeeSearch.SQL_ListSize = 260
        Me.EmployeeSearch.SQL_NumberOfRows = 200
        Me.EmployeeSearch.SQL_OrderByField = "Ag_name"
        Me.EmployeeSearch.SQL_SearchField = "Ag_name"
        Me.EmployeeSearch.SQL_SearchField_WHERE = ""
        Me.EmployeeSearch.SQL_Table = "Agents_Emp_V"
        Me.EmployeeSearch.TabIndex = 2
        Me.EmployeeSearch.TextMaxLength = 250
        Me.EmployeeSearch.Textt = ""
        '
        'EmployeeLabel
        '
        Me.EmployeeLabel.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.EmployeeLabel.ForeColor = System.Drawing.Color.White
        Me.EmployeeLabel.Location = New System.Drawing.Point(941, 31)
        Me.EmployeeLabel.Name = "EmployeeLabel"
        Me.EmployeeLabel.Size = New System.Drawing.Size(63, 28)
        Me.EmployeeLabel.TabIndex = 3
        Me.EmployeeLabel.Text = "الحساب"
        Me.EmployeeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'NotesTextBox
        '
        Me.NotesTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NotesTextBox.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.NotesTextBox.Location = New System.Drawing.Point(3, 4)
        Me.NotesTextBox.Name = "NotesTextBox"
        Me.NotesTextBox.Size = New System.Drawing.Size(277, 25)
        Me.NotesTextBox.TabIndex = 4
        Me.NotesTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TitleTextBox
        '
        Me.TitleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TitleTextBox.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.TitleTextBox.Location = New System.Drawing.Point(281, 4)
        Me.TitleTextBox.Name = "TitleTextBox"
        Me.TitleTextBox.Size = New System.Drawing.Size(285, 25)
        Me.TitleTextBox.TabIndex = 5
        Me.TitleTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BillDatePicker
        '
        Me.BillDatePicker.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.BillDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.BillDatePicker.Location = New System.Drawing.Point(568, 4)
        Me.BillDatePicker.Name = "BillDatePicker"
        Me.BillDatePicker.Size = New System.Drawing.Size(140, 25)
        Me.BillDatePicker.TabIndex = 6
        '
        'BillNumberTextBox
        '
        Me.BillNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.BillNumberTextBox.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.BillNumberTextBox.Location = New System.Drawing.Point(709, 4)
        Me.BillNumberTextBox.Name = "BillNumberTextBox"
        Me.BillNumberTextBox.ReadOnly = True
        Me.BillNumberTextBox.Size = New System.Drawing.Size(300, 25)
        Me.BillNumberTextBox.TabIndex = 7
        Me.BillNumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ContentSplit
        '
        Me.ContentSplit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContentSplit.IsSplitterFixed = True
        Me.ContentSplit.Location = New System.Drawing.Point(3, 84)
        Me.ContentSplit.Name = "ContentSplit"
        '
        'ContentSplit.Panel1
        '
        Me.ContentSplit.Panel1.Controls.Add(Me.CartLayout)
        Me.ContentSplit.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        '
        'ContentSplit.Panel2
        '
        Me.ContentSplit.Panel2.Controls.Add(Me.ItemsLayout)
        Me.ContentSplit.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ContentSplit.Size = New System.Drawing.Size(1010, 440)
        Me.ContentSplit.SplitterDistance = 396
        Me.ContentSplit.SplitterWidth = 6
        Me.ContentSplit.TabIndex = 1
        '
        'CartLayout
        '
        Me.CartLayout.ColumnCount = 1
        Me.CartLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.CartLayout.Controls.Add(Me.CartGrid, 0, 0)
        Me.CartLayout.Controls.Add(Me.CartToolsPanel, 0, 2)
        Me.CartLayout.Controls.Add(Me.SummaryPanel, 0, 3)
        Me.CartLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CartLayout.Location = New System.Drawing.Point(0, 0)
        Me.CartLayout.Name = "CartLayout"
        Me.CartLayout.RowCount = 4
        Me.CartLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.CartLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0!))
        Me.CartLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48.0!))
        Me.CartLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62.0!))
        Me.CartLayout.Size = New System.Drawing.Size(396, 440)
        Me.CartLayout.TabIndex = 0
        '
        'CartGrid
        '
        Me.CartGrid.AllowUserToAddRows = False
        Me.CartGrid.AllowUserToDeleteRows = False
        Me.CartGrid.AllowUserToResizeRows = False
        Me.CartGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.CartGrid.BackgroundColor = System.Drawing.Color.White
        Me.CartGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.CartGrid.ColumnHeadersHeight = 32
        Me.CartGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CartGrid.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!)
        Me.CartGrid.Location = New System.Drawing.Point(3, 3)
        Me.CartGrid.MultiSelect = False
        Me.CartGrid.Name = "CartGrid"
        Me.CartGrid.ReadOnly = True
        Me.CartGrid.RowHeadersVisible = False
        Me.CartGrid.RowTemplate.Height = 30
        Me.CartGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.CartGrid.Size = New System.Drawing.Size(390, 324)
        Me.CartGrid.TabIndex = 0
        '
        'CartToolsPanel
        '
        Me.CartToolsPanel.Controls.Add(Me.DeleteLineButton)
        Me.CartToolsPanel.Controls.Add(Me.DecreaseLineButton)
        Me.CartToolsPanel.Controls.Add(Me.IncreaseLineButton)
        Me.CartToolsPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CartToolsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.CartToolsPanel.Location = New System.Drawing.Point(3, 333)
        Me.CartToolsPanel.Name = "CartToolsPanel"
        Me.CartToolsPanel.Padding = New System.Windows.Forms.Padding(6)
        Me.CartToolsPanel.Size = New System.Drawing.Size(390, 42)
        Me.CartToolsPanel.TabIndex = 1
        '
        'DeleteLineButton
        '
        Me.DeleteLineButton.BackColor = System.Drawing.Color.Firebrick
        Me.DeleteLineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DeleteLineButton.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.DeleteLineButton.ForeColor = System.Drawing.Color.White
        Me.DeleteLineButton.Location = New System.Drawing.Point(9, 9)
        Me.DeleteLineButton.Name = "DeleteLineButton"
        Me.DeleteLineButton.Size = New System.Drawing.Size(108, 36)
        Me.DeleteLineButton.TabIndex = 0
        Me.DeleteLineButton.Text = "حذف الصنف"
        Me.DeleteLineButton.UseVisualStyleBackColor = False
        '
        'DecreaseLineButton
        '
        Me.DecreaseLineButton.BackColor = System.Drawing.Color.White
        Me.DecreaseLineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DecreaseLineButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.DecreaseLineButton.Location = New System.Drawing.Point(123, 9)
        Me.DecreaseLineButton.Name = "DecreaseLineButton"
        Me.DecreaseLineButton.Size = New System.Drawing.Size(52, 33)
        Me.DecreaseLineButton.TabIndex = 1
        Me.DecreaseLineButton.Text = "−"
        Me.DecreaseLineButton.UseVisualStyleBackColor = False
        '
        'IncreaseLineButton
        '
        Me.IncreaseLineButton.BackColor = System.Drawing.Color.White
        Me.IncreaseLineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.IncreaseLineButton.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.IncreaseLineButton.Location = New System.Drawing.Point(181, 9)
        Me.IncreaseLineButton.Name = "IncreaseLineButton"
        Me.IncreaseLineButton.Size = New System.Drawing.Size(52, 33)
        Me.IncreaseLineButton.TabIndex = 2
        Me.IncreaseLineButton.Text = "+"
        Me.IncreaseLineButton.UseVisualStyleBackColor = False
        '
        'SummaryPanel
        '
        Me.SummaryPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.SummaryPanel.Controls.Add(Me.ExpectedSalesValueLabel)
        Me.SummaryPanel.Controls.Add(Me.TotalCostLabel)
        Me.SummaryPanel.Controls.Add(Me.TotalQuantityLabel)
        Me.SummaryPanel.Controls.Add(Me.ItemsCountLabel)
        Me.SummaryPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SummaryPanel.Location = New System.Drawing.Point(3, 381)
        Me.SummaryPanel.Name = "SummaryPanel"
        Me.SummaryPanel.Size = New System.Drawing.Size(390, 56)
        Me.SummaryPanel.TabIndex = 2
        '
        'ExpectedSalesValueLabel
        '
        Me.ExpectedSalesValueLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ExpectedSalesValueLabel.ForeColor = System.Drawing.Color.White
        Me.ExpectedSalesValueLabel.Location = New System.Drawing.Point(0, 30)
        Me.ExpectedSalesValueLabel.Name = "ExpectedSalesValueLabel"
        Me.ExpectedSalesValueLabel.Size = New System.Drawing.Size(189, 33)
        Me.ExpectedSalesValueLabel.TabIndex = 0
        Me.ExpectedSalesValueLabel.Text = "قيمة البيع: 0.000"
        Me.ExpectedSalesValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TotalCostLabel
        '
        Me.TotalCostLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TotalCostLabel.ForeColor = System.Drawing.Color.White
        Me.TotalCostLabel.Location = New System.Drawing.Point(192, 31)
        Me.TotalCostLabel.Name = "TotalCostLabel"
        Me.TotalCostLabel.Size = New System.Drawing.Size(196, 33)
        Me.TotalCostLabel.TabIndex = 1
        Me.TotalCostLabel.Text = "إجمالي التكلفة: 0.000"
        Me.TotalCostLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TotalQuantityLabel
        '
        Me.TotalQuantityLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.TotalQuantityLabel.ForeColor = System.Drawing.Color.White
        Me.TotalQuantityLabel.Location = New System.Drawing.Point(0, 0)
        Me.TotalQuantityLabel.Name = "TotalQuantityLabel"
        Me.TotalQuantityLabel.Size = New System.Drawing.Size(193, 29)
        Me.TotalQuantityLabel.TabIndex = 2
        Me.TotalQuantityLabel.Text = "الكميات: 0"
        Me.TotalQuantityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ItemsCountLabel
        '
        Me.ItemsCountLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ItemsCountLabel.ForeColor = System.Drawing.Color.White
        Me.ItemsCountLabel.Location = New System.Drawing.Point(199, 0)
        Me.ItemsCountLabel.Name = "ItemsCountLabel"
        Me.ItemsCountLabel.Size = New System.Drawing.Size(188, 31)
        Me.ItemsCountLabel.TabIndex = 3
        Me.ItemsCountLabel.Text = "الأصناف: 0"
        Me.ItemsCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ItemsLayout
        '
        Me.ItemsLayout.ColumnCount = 1
        Me.ItemsLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ItemsLayout.Controls.Add(Me.SearchPanel, 0, 0)
        Me.ItemsLayout.Controls.Add(Me.CategoryNavigationPanel, 0, 1)
        Me.ItemsLayout.Controls.Add(Me.ItemsFlowPanel, 0, 2)
        Me.ItemsLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ItemsLayout.Location = New System.Drawing.Point(0, 0)
        Me.ItemsLayout.Name = "ItemsLayout"
        Me.ItemsLayout.RowCount = 4
        Me.ItemsLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38.0!))
        Me.ItemsLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69.0!))
        Me.ItemsLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ItemsLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0!))
        Me.ItemsLayout.Size = New System.Drawing.Size(608, 440)
        Me.ItemsLayout.TabIndex = 0
        '
        'SearchPanel
        '
        Me.SearchPanel.BackColor = System.Drawing.Color.Gainsboro
        Me.SearchPanel.Controls.Add(Me.SearchTextBox)
        Me.SearchPanel.Controls.Add(Me.SearchLabel)
        Me.SearchPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SearchPanel.Location = New System.Drawing.Point(3, 3)
        Me.SearchPanel.Name = "SearchPanel"
        Me.SearchPanel.Size = New System.Drawing.Size(602, 32)
        Me.SearchPanel.TabIndex = 0
        '
        'SearchTextBox
        '
        Me.SearchTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SearchTextBox.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.SearchTextBox.Location = New System.Drawing.Point(3, 3)
        Me.SearchTextBox.Name = "SearchTextBox"
        Me.SearchTextBox.Size = New System.Drawing.Size(596, 25)
        Me.SearchTextBox.TabIndex = 0
        Me.SearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'SearchLabel
        '
        Me.SearchLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SearchLabel.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.SearchLabel.Location = New System.Drawing.Point(1078, 12)
        Me.SearchLabel.Name = "SearchLabel"
        Me.SearchLabel.Size = New System.Drawing.Size(78, 28)
        Me.SearchLabel.TabIndex = 1
        Me.SearchLabel.Text = "بحث"
        Me.SearchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CategoryNavigationPanel
        '
        Me.CategoryNavigationPanel.Controls.Add(Me.CategoryFlowPanel)
        Me.CategoryNavigationPanel.Controls.Add(Me.CategoryPreviousButton)
        Me.CategoryNavigationPanel.Controls.Add(Me.CategoryNextButton)
        Me.CategoryNavigationPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CategoryNavigationPanel.Location = New System.Drawing.Point(3, 41)
        Me.CategoryNavigationPanel.Name = "CategoryNavigationPanel"
        Me.CategoryNavigationPanel.Size = New System.Drawing.Size(602, 63)
        Me.CategoryNavigationPanel.TabIndex = 1
        '
        'CategoryFlowPanel
        '
        Me.CategoryFlowPanel.AutoScroll = True
        Me.CategoryFlowPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.CategoryFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CategoryFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.CategoryFlowPanel.Location = New System.Drawing.Point(56, 0)
        Me.CategoryFlowPanel.Name = "CategoryFlowPanel"
        Me.CategoryFlowPanel.Padding = New System.Windows.Forms.Padding(5)
        Me.CategoryFlowPanel.Size = New System.Drawing.Size(490, 63)
        Me.CategoryFlowPanel.TabIndex = 1
        Me.CategoryFlowPanel.WrapContents = False
        '
        'CategoryPreviousButton
        '
        Me.CategoryPreviousButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.CategoryPreviousButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.CategoryPreviousButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CategoryPreviousButton.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.CategoryPreviousButton.ForeColor = System.Drawing.Color.White
        Me.CategoryPreviousButton.Location = New System.Drawing.Point(0, 0)
        Me.CategoryPreviousButton.Name = "CategoryPreviousButton"
        Me.CategoryPreviousButton.Size = New System.Drawing.Size(56, 63)
        Me.CategoryPreviousButton.TabIndex = 1
        Me.CategoryPreviousButton.Text = "◀"
        Me.CategoryPreviousButton.UseVisualStyleBackColor = False
        '
        'CategoryNextButton
        '
        Me.CategoryNextButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.CategoryNextButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.CategoryNextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CategoryNextButton.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.CategoryNextButton.ForeColor = System.Drawing.Color.White
        Me.CategoryNextButton.Location = New System.Drawing.Point(546, 0)
        Me.CategoryNextButton.Name = "CategoryNextButton"
        Me.CategoryNextButton.Size = New System.Drawing.Size(56, 63)
        Me.CategoryNextButton.TabIndex = 2
        Me.CategoryNextButton.Text = "▶"
        Me.CategoryNextButton.UseVisualStyleBackColor = False
        '
        'ItemsFlowPanel
        '
        Me.ItemsFlowPanel.AutoScroll = True
        Me.ItemsFlowPanel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ItemsFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ItemsFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.ItemsFlowPanel.Location = New System.Drawing.Point(3, 110)
        Me.ItemsFlowPanel.Name = "ItemsFlowPanel"
        Me.ItemsFlowPanel.Padding = New System.Windows.Forms.Padding(8)
        Me.ItemsFlowPanel.Size = New System.Drawing.Size(602, 327)
        Me.ItemsFlowPanel.TabIndex = 1
        '
        'CommandsPanel
        '
        Me.CommandsPanel.Controls.Add(Me.CloseButton)
        Me.CommandsPanel.Controls.Add(Me.SearchBillsButton)
        Me.CommandsPanel.Controls.Add(Me.CancelBillButton)
        Me.CommandsPanel.Controls.Add(Me.PrintButton)
        Me.CommandsPanel.Controls.Add(Me.EditButton)
        Me.CommandsPanel.Controls.Add(Me.SaveButton)
        Me.CommandsPanel.Controls.Add(Me.NewButton)
        Me.CommandsPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CommandsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.CommandsPanel.Location = New System.Drawing.Point(3, 530)
        Me.CommandsPanel.Name = "CommandsPanel"
        Me.CommandsPanel.Padding = New System.Windows.Forms.Padding(7)
        Me.CommandsPanel.Size = New System.Drawing.Size(1010, 48)
        Me.CommandsPanel.TabIndex = 3
        '
        'CloseButton
        '
        Me.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.CloseButton.Location = New System.Drawing.Point(12, 12)
        Me.CloseButton.Margin = New System.Windows.Forms.Padding(5)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(132, 46)
        Me.CloseButton.TabIndex = 0
        Me.CloseButton.Text = "خروج"
        '
        'SearchBillsButton
        '
        Me.SearchBillsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SearchBillsButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SearchBillsButton.Location = New System.Drawing.Point(154, 12)
        Me.SearchBillsButton.Margin = New System.Windows.Forms.Padding(5)
        Me.SearchBillsButton.Name = "SearchBillsButton"
        Me.SearchBillsButton.Size = New System.Drawing.Size(132, 46)
        Me.SearchBillsButton.TabIndex = 1
        Me.SearchBillsButton.Text = "بحث الأذونات"
        '
        'CancelBillButton
        '
        Me.CancelBillButton.BackColor = System.Drawing.Color.MistyRose
        Me.CancelBillButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CancelBillButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.CancelBillButton.Location = New System.Drawing.Point(296, 12)
        Me.CancelBillButton.Margin = New System.Windows.Forms.Padding(5)
        Me.CancelBillButton.Name = "CancelBillButton"
        Me.CancelBillButton.Size = New System.Drawing.Size(132, 46)
        Me.CancelBillButton.TabIndex = 2
        Me.CancelBillButton.Text = "إلغاء الإذن"
        Me.CancelBillButton.UseVisualStyleBackColor = False
        '
        'PrintButton
        '
        Me.PrintButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PrintButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.PrintButton.Location = New System.Drawing.Point(438, 12)
        Me.PrintButton.Margin = New System.Windows.Forms.Padding(5)
        Me.PrintButton.Name = "PrintButton"
        Me.PrintButton.Size = New System.Drawing.Size(132, 46)
        Me.PrintButton.TabIndex = 3
        Me.PrintButton.Text = "طباعة"
        '
        'EditButton
        '
        Me.EditButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EditButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.EditButton.Location = New System.Drawing.Point(580, 12)
        Me.EditButton.Margin = New System.Windows.Forms.Padding(5)
        Me.EditButton.Name = "EditButton"
        Me.EditButton.Size = New System.Drawing.Size(132, 46)
        Me.EditButton.TabIndex = 4
        Me.EditButton.Text = "فتح التعديل"
        '
        'SaveButton
        '
        Me.SaveButton.BackColor = System.Drawing.Color.Honeydew
        Me.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SaveButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SaveButton.Location = New System.Drawing.Point(722, 12)
        Me.SaveButton.Margin = New System.Windows.Forms.Padding(5)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(132, 46)
        Me.SaveButton.TabIndex = 5
        Me.SaveButton.Text = "حفظ واعتماد"
        Me.SaveButton.UseVisualStyleBackColor = False
        '
        'NewButton
        '
        Me.NewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.NewButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.NewButton.Location = New System.Drawing.Point(864, 12)
        Me.NewButton.Margin = New System.Windows.Forms.Padding(5)
        Me.NewButton.Name = "NewButton"
        Me.NewButton.Size = New System.Drawing.Size(132, 46)
        Me.NewButton.TabIndex = 6
        Me.NewButton.Text = "إذن جديد"
        '
        'QuantityKeypad
        '
        Me.QuantityKeypad.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.QuantityKeypad.ColumnCount = 4
        Me.QuantityKeypad.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.QuantityKeypad.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.QuantityKeypad.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.QuantityKeypad.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.QuantityKeypad.Controls.Add(Me.QuantityKey7Button, 0, 0)
        Me.QuantityKeypad.Controls.Add(Me.QuantityKey8Button, 1, 0)
        Me.QuantityKeypad.Controls.Add(Me.QuantityKey9Button, 2, 0)
        Me.QuantityKeypad.Controls.Add(Me.QuantityClearButton, 3, 0)
        Me.QuantityKeypad.Controls.Add(Me.QuantityKey4Button, 0, 1)
        Me.QuantityKeypad.Controls.Add(Me.QuantityKey5Button, 1, 1)
        Me.QuantityKeypad.Controls.Add(Me.QuantityKey6Button, 2, 1)
        Me.QuantityKeypad.Controls.Add(Me.QuantityBackspaceButton, 3, 1)
        Me.QuantityKeypad.Controls.Add(Me.QuantityKey1Button, 0, 2)
        Me.QuantityKeypad.Controls.Add(Me.QuantityKey2Button, 1, 2)
        Me.QuantityKeypad.Controls.Add(Me.QuantityKey3Button, 2, 2)
        Me.QuantityKeypad.Controls.Add(Me.QuantityDecimalButton, 3, 2)
        Me.QuantityKeypad.Controls.Add(Me.QuantityKey0Button, 0, 3)
        Me.QuantityKeypad.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuantityKeypad.Location = New System.Drawing.Point(3, 183)
        Me.QuantityKeypad.Name = "QuantityKeypad"
        Me.QuantityKeypad.Padding = New System.Windows.Forms.Padding(4)
        Me.QuantityKeypad.RowCount = 4
        Me.QuantityKeypad.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.QuantityKeypad.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.QuantityKeypad.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.QuantityKeypad.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.QuantityKeypad.Size = New System.Drawing.Size(390, 144)
        Me.QuantityKeypad.TabIndex = 1
        '
        'QuantityKey7Button
        '
        Me.QuantityKey7Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuantityKey7Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.QuantityKey7Button.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.QuantityKey7Button.Location = New System.Drawing.Point(7, 7)
        Me.QuantityKey7Button.Name = "QuantityKey7Button"
        Me.QuantityKey7Button.Size = New System.Drawing.Size(89, 28)
        Me.QuantityKey7Button.TabIndex = 0
        Me.QuantityKey7Button.Tag = "7"
        Me.QuantityKey7Button.Text = "7"
        Me.QuantityKey7Button.UseVisualStyleBackColor = True
        '
        'QuantityKey8Button
        '
        Me.QuantityKey8Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuantityKey8Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.QuantityKey8Button.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.QuantityKey8Button.Location = New System.Drawing.Point(102, 7)
        Me.QuantityKey8Button.Name = "QuantityKey8Button"
        Me.QuantityKey8Button.Size = New System.Drawing.Size(89, 28)
        Me.QuantityKey8Button.TabIndex = 1
        Me.QuantityKey8Button.Tag = "8"
        Me.QuantityKey8Button.Text = "8"
        Me.QuantityKey8Button.UseVisualStyleBackColor = True
        '
        'QuantityKey9Button
        '
        Me.QuantityKey9Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuantityKey9Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.QuantityKey9Button.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.QuantityKey9Button.Location = New System.Drawing.Point(197, 7)
        Me.QuantityKey9Button.Name = "QuantityKey9Button"
        Me.QuantityKey9Button.Size = New System.Drawing.Size(89, 28)
        Me.QuantityKey9Button.TabIndex = 2
        Me.QuantityKey9Button.Tag = "9"
        Me.QuantityKey9Button.Text = "9"
        Me.QuantityKey9Button.UseVisualStyleBackColor = True
        '
        'QuantityClearButton
        '
        Me.QuantityClearButton.BackColor = System.Drawing.Color.IndianRed
        Me.QuantityClearButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuantityClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.QuantityClearButton.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.QuantityClearButton.ForeColor = System.Drawing.Color.White
        Me.QuantityClearButton.Location = New System.Drawing.Point(292, 7)
        Me.QuantityClearButton.Name = "QuantityClearButton"
        Me.QuantityClearButton.Size = New System.Drawing.Size(91, 28)
        Me.QuantityClearButton.TabIndex = 3
        Me.QuantityClearButton.Text = "مسح"
        Me.QuantityClearButton.UseVisualStyleBackColor = False
        '
        'QuantityKey4Button
        '
        Me.QuantityKey4Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuantityKey4Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.QuantityKey4Button.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.QuantityKey4Button.Location = New System.Drawing.Point(7, 41)
        Me.QuantityKey4Button.Name = "QuantityKey4Button"
        Me.QuantityKey4Button.Size = New System.Drawing.Size(89, 28)
        Me.QuantityKey4Button.TabIndex = 4
        Me.QuantityKey4Button.Tag = "4"
        Me.QuantityKey4Button.Text = "4"
        Me.QuantityKey4Button.UseVisualStyleBackColor = True
        '
        'QuantityKey5Button
        '
        Me.QuantityKey5Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuantityKey5Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.QuantityKey5Button.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.QuantityKey5Button.Location = New System.Drawing.Point(102, 41)
        Me.QuantityKey5Button.Name = "QuantityKey5Button"
        Me.QuantityKey5Button.Size = New System.Drawing.Size(89, 28)
        Me.QuantityKey5Button.TabIndex = 5
        Me.QuantityKey5Button.Tag = "5"
        Me.QuantityKey5Button.Text = "5"
        Me.QuantityKey5Button.UseVisualStyleBackColor = True
        '
        'QuantityKey6Button
        '
        Me.QuantityKey6Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuantityKey6Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.QuantityKey6Button.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.QuantityKey6Button.Location = New System.Drawing.Point(197, 41)
        Me.QuantityKey6Button.Name = "QuantityKey6Button"
        Me.QuantityKey6Button.Size = New System.Drawing.Size(89, 28)
        Me.QuantityKey6Button.TabIndex = 6
        Me.QuantityKey6Button.Tag = "6"
        Me.QuantityKey6Button.Text = "6"
        Me.QuantityKey6Button.UseVisualStyleBackColor = True
        '
        'QuantityBackspaceButton
        '
        Me.QuantityBackspaceButton.BackColor = System.Drawing.Color.SlateGray
        Me.QuantityBackspaceButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuantityBackspaceButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.QuantityBackspaceButton.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.QuantityBackspaceButton.ForeColor = System.Drawing.Color.White
        Me.QuantityBackspaceButton.Location = New System.Drawing.Point(292, 41)
        Me.QuantityBackspaceButton.Name = "QuantityBackspaceButton"
        Me.QuantityBackspaceButton.Size = New System.Drawing.Size(91, 28)
        Me.QuantityBackspaceButton.TabIndex = 7
        Me.QuantityBackspaceButton.Text = "⌫"
        Me.QuantityBackspaceButton.UseVisualStyleBackColor = False
        '
        'QuantityKey1Button
        '
        Me.QuantityKey1Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuantityKey1Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.QuantityKey1Button.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.QuantityKey1Button.Location = New System.Drawing.Point(7, 75)
        Me.QuantityKey1Button.Name = "QuantityKey1Button"
        Me.QuantityKey1Button.Size = New System.Drawing.Size(89, 28)
        Me.QuantityKey1Button.TabIndex = 8
        Me.QuantityKey1Button.Tag = "1"
        Me.QuantityKey1Button.Text = "1"
        Me.QuantityKey1Button.UseVisualStyleBackColor = True
        '
        'QuantityKey2Button
        '
        Me.QuantityKey2Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuantityKey2Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.QuantityKey2Button.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.QuantityKey2Button.Location = New System.Drawing.Point(102, 75)
        Me.QuantityKey2Button.Name = "QuantityKey2Button"
        Me.QuantityKey2Button.Size = New System.Drawing.Size(89, 28)
        Me.QuantityKey2Button.TabIndex = 9
        Me.QuantityKey2Button.Tag = "2"
        Me.QuantityKey2Button.Text = "2"
        Me.QuantityKey2Button.UseVisualStyleBackColor = True
        '
        'QuantityKey3Button
        '
        Me.QuantityKey3Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuantityKey3Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.QuantityKey3Button.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.QuantityKey3Button.Location = New System.Drawing.Point(197, 75)
        Me.QuantityKey3Button.Name = "QuantityKey3Button"
        Me.QuantityKey3Button.Size = New System.Drawing.Size(89, 28)
        Me.QuantityKey3Button.TabIndex = 10
        Me.QuantityKey3Button.Tag = "3"
        Me.QuantityKey3Button.Text = "3"
        Me.QuantityKey3Button.UseVisualStyleBackColor = True
        '
        'QuantityDecimalButton
        '
        Me.QuantityDecimalButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuantityDecimalButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.QuantityDecimalButton.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.QuantityDecimalButton.Location = New System.Drawing.Point(292, 75)
        Me.QuantityDecimalButton.Name = "QuantityDecimalButton"
        Me.QuantityDecimalButton.Size = New System.Drawing.Size(91, 28)
        Me.QuantityDecimalButton.TabIndex = 11
        Me.QuantityDecimalButton.Text = "."
        Me.QuantityDecimalButton.UseVisualStyleBackColor = True
        '
        'QuantityKey0Button
        '
        Me.QuantityKeypad.SetColumnSpan(Me.QuantityKey0Button, 4)
        Me.QuantityKey0Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuantityKey0Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.QuantityKey0Button.Font = New System.Drawing.Font("Segoe UI Semibold", 11.0!, System.Drawing.FontStyle.Bold)
        Me.QuantityKey0Button.Location = New System.Drawing.Point(7, 109)
        Me.QuantityKey0Button.Name = "QuantityKey0Button"
        Me.QuantityKey0Button.Size = New System.Drawing.Size(376, 28)
        Me.QuantityKey0Button.TabIndex = 12
        Me.QuantityKey0Button.Tag = "0"
        Me.QuantityKey0Button.Text = "0"
        Me.QuantityKey0Button.UseVisualStyleBackColor = True
        '
        'AddItemButton
        '
        Me.AddItemButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.AddItemButton.BackColor = System.Drawing.Color.SeaGreen
        Me.AddItemButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AddItemButton.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.AddItemButton.ForeColor = System.Drawing.Color.White
        Me.AddItemButton.Location = New System.Drawing.Point(239, 9)
        Me.AddItemButton.Name = "AddItemButton"
        Me.AddItemButton.Size = New System.Drawing.Size(108, 36)
        Me.AddItemButton.TabIndex = 0
        Me.AddItemButton.Text = "إضافة إلى الإذن"
        Me.AddItemButton.UseVisualStyleBackColor = False
        '
        'EditorPanel
        '
        Me.EditorPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.EditorPanel.Controls.Add(Me.ExpiryDatePicker)
        Me.EditorPanel.Controls.Add(Me.ExpiryLabel)
        Me.EditorPanel.Controls.Add(Me.PriceTextBox)
        Me.EditorPanel.Controls.Add(Me.PriceLabel)
        Me.EditorPanel.Controls.Add(Me.QuantityTextBox)
        Me.EditorPanel.Controls.Add(Me.QuantityLabel)
        Me.EditorPanel.Controls.Add(Me.UnitComboBox)
        Me.EditorPanel.Controls.Add(Me.UnitLabel)
        Me.EditorPanel.Controls.Add(Me.StoreComboBox)
        Me.EditorPanel.Controls.Add(Me.StoreLabel)
        Me.EditorPanel.Controls.Add(Me.StockLabel)
        Me.EditorPanel.Controls.Add(Me.SelectedItemLabel)
        Me.EditorPanel.Controls.Add(Me.CostWarningLabel)
        Me.EditorPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EditorPanel.Location = New System.Drawing.Point(3, 284)
        Me.EditorPanel.Name = "EditorPanel"
        Me.EditorPanel.Size = New System.Drawing.Size(602, 153)
        Me.EditorPanel.TabIndex = 2
        '
        'ExpiryDatePicker
        '
        Me.ExpiryDatePicker.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.ExpiryDatePicker.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.ExpiryDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.ExpiryDatePicker.Location = New System.Drawing.Point(281, 66)
        Me.ExpiryDatePicker.Name = "ExpiryDatePicker"
        Me.ExpiryDatePicker.Size = New System.Drawing.Size(130, 25)
        Me.ExpiryDatePicker.TabIndex = 1
        Me.ExpiryDatePicker.Visible = False
        '
        'ExpiryLabel
        '
        Me.ExpiryLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.ExpiryLabel.Location = New System.Drawing.Point(345, 42)
        Me.ExpiryLabel.Name = "ExpiryLabel"
        Me.ExpiryLabel.Size = New System.Drawing.Size(64, 22)
        Me.ExpiryLabel.TabIndex = 2
        Me.ExpiryLabel.Text = "الصلاحية"
        Me.ExpiryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExpiryLabel.Visible = False
        '
        'PriceTextBox
        '
        Me.PriceTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.PriceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PriceTextBox.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.PriceTextBox.Location = New System.Drawing.Point(254, 124)
        Me.PriceTextBox.Name = "PriceTextBox"
        Me.PriceTextBox.Size = New System.Drawing.Size(90, 25)
        Me.PriceTextBox.TabIndex = 3
        Me.PriceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PriceLabel
        '
        Me.PriceLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.PriceLabel.Location = New System.Drawing.Point(256, 100)
        Me.PriceLabel.Name = "PriceLabel"
        Me.PriceLabel.Size = New System.Drawing.Size(86, 22)
        Me.PriceLabel.TabIndex = 4
        Me.PriceLabel.Text = "التكلفة"
        Me.PriceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'QuantityTextBox
        '
        Me.QuantityTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.QuantityTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.QuantityTextBox.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.QuantityTextBox.Location = New System.Drawing.Point(346, 124)
        Me.QuantityTextBox.Name = "QuantityTextBox"
        Me.QuantityTextBox.Size = New System.Drawing.Size(80, 25)
        Me.QuantityTextBox.TabIndex = 5
        Me.QuantityTextBox.Text = "1"
        Me.QuantityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'QuantityLabel
        '
        Me.QuantityLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.QuantityLabel.Location = New System.Drawing.Point(346, 100)
        Me.QuantityLabel.Name = "QuantityLabel"
        Me.QuantityLabel.Size = New System.Drawing.Size(77, 22)
        Me.QuantityLabel.TabIndex = 6
        Me.QuantityLabel.Text = "الكمية"
        Me.QuantityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'UnitComboBox
        '
        Me.UnitComboBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.UnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.UnitComboBox.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.UnitComboBox.FormattingEnabled = True
        Me.UnitComboBox.Location = New System.Drawing.Point(428, 123)
        Me.UnitComboBox.Name = "UnitComboBox"
        Me.UnitComboBox.Size = New System.Drawing.Size(170, 25)
        Me.UnitComboBox.TabIndex = 7
        '
        'UnitLabel
        '
        Me.UnitLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.UnitLabel.Location = New System.Drawing.Point(518, 100)
        Me.UnitLabel.Name = "UnitLabel"
        Me.UnitLabel.Size = New System.Drawing.Size(77, 22)
        Me.UnitLabel.TabIndex = 8
        Me.UnitLabel.Text = "الوحدة"
        Me.UnitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StoreComboBox
        '
        Me.StoreComboBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.StoreComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.StoreComboBox.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!)
        Me.StoreComboBox.FormattingEnabled = True
        Me.StoreComboBox.Location = New System.Drawing.Point(414, 66)
        Me.StoreComboBox.Name = "StoreComboBox"
        Me.StoreComboBox.Size = New System.Drawing.Size(185, 25)
        Me.StoreComboBox.TabIndex = 9
        '
        'StoreLabel
        '
        Me.StoreLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.StoreLabel.Location = New System.Drawing.Point(515, 42)
        Me.StoreLabel.Name = "StoreLabel"
        Me.StoreLabel.Size = New System.Drawing.Size(81, 22)
        Me.StoreLabel.TabIndex = 10
        Me.StoreLabel.Text = "المخزن"
        Me.StoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'StockLabel
        '
        Me.StockLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.StockLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StockLabel.Location = New System.Drawing.Point(1, 49)
        Me.StockLabel.Name = "StockLabel"
        Me.StockLabel.Size = New System.Drawing.Size(175, 24)
        Me.StockLabel.TabIndex = 11
        Me.StockLabel.Text = "المتوفر: 0"
        Me.StockLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SelectedItemLabel
        '
        Me.SelectedItemLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.SelectedItemLabel.AutoEllipsis = True
        Me.SelectedItemLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.SelectedItemLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SelectedItemLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.SelectedItemLabel.ForeColor = System.Drawing.Color.White
        Me.SelectedItemLabel.Location = New System.Drawing.Point(5, 5)
        Me.SelectedItemLabel.Name = "SelectedItemLabel"
        Me.SelectedItemLabel.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.SelectedItemLabel.Size = New System.Drawing.Size(594, 35)
        Me.SelectedItemLabel.TabIndex = 12
        Me.SelectedItemLabel.Text = "المس صنفًا لإضافته"
        Me.SelectedItemLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CostWarningLabel
        '
        Me.CostWarningLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.CostWarningLabel.BackColor = System.Drawing.Color.MistyRose
        Me.CostWarningLabel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Bold)
        Me.CostWarningLabel.ForeColor = System.Drawing.Color.Firebrick
        Me.CostWarningLabel.Location = New System.Drawing.Point(4, 77)
        Me.CostWarningLabel.Name = "CostWarningLabel"
        Me.CostWarningLabel.Size = New System.Drawing.Size(175, 34)
        Me.CostWarningLabel.TabIndex = 13
        Me.CostWarningLabel.Text = "تنبيه: لم يتم تحديد تكلفة للصنف"
        Me.CostWarningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CostWarningLabel.Visible = False
        '
        'Outside_Sales_Touch
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1016, 581)
        Me.Controls.Add(Me.RootLayout)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "Outside_Sales_Touch"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.RightToLeftLayout = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "إذن استلام أصناف - شاشة اللمس"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.RootLayout.ResumeLayout(False)
        Me.HeaderPanel.ResumeLayout(False)
        Me.HeaderPanel.PerformLayout()
        Me.ContentSplit.Panel1.ResumeLayout(False)
        Me.ContentSplit.Panel2.ResumeLayout(False)
        CType(Me.ContentSplit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContentSplit.ResumeLayout(False)
        Me.CartLayout.ResumeLayout(False)
        CType(Me.CartGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CartToolsPanel.ResumeLayout(False)
        Me.SummaryPanel.ResumeLayout(False)
        Me.ItemsLayout.ResumeLayout(False)
        Me.SearchPanel.ResumeLayout(False)
        Me.SearchPanel.PerformLayout()
        Me.CategoryNavigationPanel.ResumeLayout(False)
        Me.CommandsPanel.ResumeLayout(False)
        Me.QuantityKeypad.ResumeLayout(False)
        Me.EditorPanel.ResumeLayout(False)
        Me.EditorPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RootLayout As TableLayoutPanel
    Friend WithEvents HeaderPanel As Panel
    Friend WithEvents ContentSplit As SplitContainer
    Friend WithEvents CartLayout As TableLayoutPanel
    Friend WithEvents CartGrid As DataGridView
    Friend WithEvents CartToolsPanel As FlowLayoutPanel
    Friend WithEvents DeleteLineButton As Button
    Friend WithEvents DecreaseLineButton As Button
    Friend WithEvents IncreaseLineButton As Button
    Friend WithEvents QuantityKeypad As TableLayoutPanel
    Friend WithEvents QuantityKey7Button As Button
    Friend WithEvents QuantityKey8Button As Button
    Friend WithEvents QuantityKey9Button As Button
    Friend WithEvents QuantityClearButton As Button
    Friend WithEvents QuantityKey4Button As Button
    Friend WithEvents QuantityKey5Button As Button
    Friend WithEvents QuantityKey6Button As Button
    Friend WithEvents QuantityBackspaceButton As Button
    Friend WithEvents QuantityKey1Button As Button
    Friend WithEvents QuantityKey2Button As Button
    Friend WithEvents QuantityKey3Button As Button
    Friend WithEvents QuantityDecimalButton As Button
    Friend WithEvents QuantityKey0Button As Button
    Friend WithEvents ItemsLayout As TableLayoutPanel
    Friend WithEvents SearchPanel As Panel
    Friend WithEvents SearchTextBox As TextBox
    Friend WithEvents SearchLabel As Label
    Friend WithEvents ItemsFlowPanel As FlowLayoutPanel
    Friend WithEvents CategoryNavigationPanel As Panel
    Friend WithEvents CategoryPreviousButton As Button
    Friend WithEvents CategoryNextButton As Button
    Friend WithEvents CategoryFlowPanel As FlowLayoutPanel
    Friend WithEvents EditorPanel As Panel
    Friend WithEvents SelectedItemLabel As Label
    Friend WithEvents CostWarningLabel As Label
    Friend WithEvents StockLabel As Label
    Friend WithEvents StoreComboBox As ComboBox
    Friend WithEvents StoreLabel As Label
    Friend WithEvents UnitComboBox As ComboBox
    Friend WithEvents UnitLabel As Label
    Friend WithEvents QuantityTextBox As TextBox
    Friend WithEvents QuantityLabel As Label
    Friend WithEvents PriceTextBox As TextBox
    Friend WithEvents PriceLabel As Label
    Friend WithEvents ExpiryDatePicker As DateTimePicker
    Friend WithEvents ExpiryLabel As Label
    Friend WithEvents AddItemButton As Button
    Friend WithEvents SummaryPanel As Panel
    Friend WithEvents ItemsCountLabel As Label
    Friend WithEvents TotalQuantityLabel As Label
    Friend WithEvents TotalCostLabel As Label
    Friend WithEvents ExpectedSalesValueLabel As Label
    Friend WithEvents CommandsPanel As FlowLayoutPanel
    Friend WithEvents NewButton As Button
    Friend WithEvents SearchBillsButton As Button
    Friend WithEvents SaveButton As Button
    Friend WithEvents EditButton As Button
    Friend WithEvents PrintButton As Button
    Friend WithEvents CancelBillButton As Button
    Friend WithEvents CloseButton As Button
    Friend WithEvents BillNumberTextBox As TextBox
    Friend WithEvents BillDatePicker As DateTimePicker
    Friend WithEvents TitleTextBox As TextBox
    Friend WithEvents NotesTextBox As TextBox
    Friend WithEvents EmployeeLabel As Label
    Friend WithEvents EmployeeSearch As resturant.FSearch_Filter
    Friend WithEvents VoidLabel As Label
    Friend WithEvents StatusLabel As Label
End Class
