Imports System.Data.SqlClient

Public Class Outside_Sales_Touch
    Private _transactionId As Integer
    Private _billId As Integer
    Private _isDepended As Boolean
    Private _isVoid As Boolean
    Private _isEditMode As Boolean
    Private _selectedItemId As Integer
    Private _selectedItemRequiresExpiry As Boolean
    Private _selectedUnitId As Integer
    Private _unitCargo As Decimal = 1D
    Private _itemsTable As New DataTable()
    Private _categoriesTable As New DataTable()
    Private _detailsTable As New DataTable()
    Private _loading As Boolean
    Private _selectedCategoryId As Integer
    Private _replaceQuantityOnNextKeypadPress As Boolean = True

    Private Sub Outside_Sales_Touch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.ApplyThemeToForm(Me)
        ConfigureSelectedItemLabel()
        ConfigureGrid()
        ConfigureCompactLayout()
        ApplyResponsiveControlSizing()
        AdjustHeaderLayout()
        AdjustContentSplit()
        AdjustItemEditorLayout()
        AdjustSummaryLayout()
        AdjustCommandButtons()
        LoadStores()
        LoadItems()
        LoadLastDraftOrCreate()
        Me.BeginInvoke(New MethodInvoker(AddressOf AdjustContentSplit))
    End Sub

    Private Sub ConfigureSelectedItemLabel()
        SelectedItemLabel.BackColor = Color.FromArgb(39, 55, 70)
        SelectedItemLabel.ForeColor = Color.White
        SelectedItemLabel.BorderStyle = BorderStyle.FixedSingle
        SelectedItemLabel.Font = New Font("Segoe UI Semibold", 15.0!, FontStyle.Bold)
        SelectedItemLabel.Padding = New Padding(10, 0, 10, 0)
        SelectedItemLabel.TextAlign = ContentAlignment.MiddleRight
        SelectedItemLabel.AutoEllipsis = True
    End Sub

    Private Sub Outside_Sales_Touch_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ConfigureCompactLayout()
        ApplyResponsiveControlSizing()
        AdjustHeaderLayout()
        AdjustContentSplit()
        AdjustItemEditorLayout()
        AdjustSummaryLayout()
        AdjustCommandButtons()
    End Sub

    Private Sub ConfigureCompactLayout()
        Dim scale As Single = CurrentLayoutScale()
        RootLayout.RowStyles(0).Height = 94.0!
        RootLayout.RowStyles(2).Height = 0.0!
        RootLayout.RowStyles(3).Height = 62.0!
        ItemsLayout.RowStyles(0).Height = 50.0!
        ItemsLayout.RowStyles(1).Height = CSng(Math.Min(82.0F, 68.0F * scale))
        ItemsLayout.RowStyles(3).Height = 0.0!
        CartLayout.RowStyles(1).Height = 0.0!
        CartLayout.RowStyles(2).Height = CSng(48.0F * scale)
        CartLayout.RowStyles(3).Height = CSng(Math.Min(72.0F, 62.0F * scale))
    End Sub

    Private Function CurrentLayoutScale() As Single
        If Me.ClientSize.Width <= 0 Then Return 1.0F
        Return Math.Max(0.82F, Math.Min(1.25F, CSng(Me.ClientSize.Width / 1016.0R)))
    End Function

    Private Sub ApplyResponsiveControlSizing()
        Dim scale As Single = CurrentLayoutScale()

        CartGrid.Font = New Font("Segoe UI Semibold", 9.0F * scale, FontStyle.Regular)
        CartGrid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Semibold", 9.0F * scale, FontStyle.Bold)
        CartGrid.ColumnHeadersHeight = CInt(32 * scale)
        CartGrid.RowTemplate.Height = CInt(30 * scale)

        Dim keypadButtons() As Button = {QuantityKey0Button, QuantityKey1Button, QuantityKey2Button, QuantityKey3Button, QuantityKey4Button, QuantityKey5Button, QuantityKey6Button, QuantityKey7Button, QuantityKey8Button, QuantityKey9Button, QuantityDecimalButton, QuantityBackspaceButton, QuantityClearButton}
        For Each keypadButton As Button In keypadButtons
            keypadButton.Font = New Font("Segoe UI Semibold", 11.0F * scale, FontStyle.Bold)
            keypadButton.Margin = New Padding(2)
        Next

        DeleteLineButton.Font = New Font("Segoe UI Semibold", 9.5F * scale, FontStyle.Bold)
        DeleteLineButton.Size = New Size(CInt(108 * scale), CInt(36 * scale))
        DecreaseLineButton.Font = New Font("Segoe UI Semibold", 12.0F * scale, FontStyle.Bold)
        DecreaseLineButton.Size = New Size(CInt(52 * scale), CInt(36 * scale))
        IncreaseLineButton.Font = New Font("Segoe UI Semibold", 12.0F * scale, FontStyle.Bold)
        IncreaseLineButton.Size = New Size(CInt(52 * scale), CInt(36 * scale))
        AddItemButton.Font = New Font("Segoe UI Semibold", 9.5F * scale, FontStyle.Bold)
        AddItemButton.Size = New Size(CInt(108 * scale), CInt(36 * scale))
        CartToolsPanel.Padding = New Padding(4)

        ItemsCountLabel.Font = New Font("Segoe UI Semibold", 9.0F * scale)
        TotalQuantityLabel.Font = New Font("Segoe UI Semibold", 9.0F * scale)
        ExpectedSalesValueLabel.Font = New Font("Segoe UI Semibold", 9.0F * scale)
        TotalCostLabel.Font = New Font("Segoe UI Semibold", 9.0F * scale, FontStyle.Bold)

        CategoryPreviousButton.Width = CInt(44 * scale)
        CategoryNextButton.Width = CInt(44 * scale)
        CategoryPreviousButton.Font = New Font("Segoe UI Semibold", 14.0F * scale, FontStyle.Bold)
        CategoryNextButton.Font = New Font("Segoe UI Semibold", 14.0F * scale, FontStyle.Bold)
        ResizeGeneratedButtons()
    End Sub

    Private Sub ResizeGeneratedButtons()
        Dim scale As Single = CurrentLayoutScale()
        For Each control As Control In CategoryFlowPanel.Controls
            Dim categoryButton As Button = TryCast(control, Button)
            If categoryButton Is Nothing Then Continue For
            categoryButton.Size = New Size(CInt(110 * scale), CInt(52 * scale))
            categoryButton.Font = New Font("Segoe UI Semibold", 9.0F * scale, FontStyle.Bold)
        Next
        For Each control As Control In ItemsFlowPanel.Controls
            Dim itemButton As Button = TryCast(control, Button)
            If itemButton Is Nothing Then Continue For
            itemButton.Size = New Size(CInt(118 * scale), CInt(60 * scale))
            itemButton.Font = New Font("Segoe UI Semibold", 9.0F * scale, FontStyle.Bold)
        Next
    End Sub

    Private Sub AdjustHeaderLayout()
        If HeaderPanel Is Nothing OrElse HeaderPanel.ClientSize.Width <= 0 Then Return

        Dim width As Integer = HeaderPanel.ClientSize.Width
        Dim rightEdge As Integer = width - 10
        BillNumberTextBox.SetBounds(Math.Max(640, rightEdge - 300), 9, 300, 32)
        BillDatePicker.SetBounds(Math.Max(490, rightEdge - 455), 12, 140, 27)
        TitleTextBox.SetBounds(230, 12, Math.Max(210, Math.Min(300, width - 725)), 27)
        NotesTextBox.SetBounds(10, 12, 210, 27)

        EmployeeLabel.SetBounds(Math.Max(840, rightEdge - 90), 55, 80, 28)
        EmployeeSearch.SetBounds(Math.Max(535, rightEdge - 395), 52, 300, 32)
        StatusLabel.SetBounds(10, 53, 245, 28)
        VoidLabel.SetBounds(265, 53, 150, 28)
    End Sub

    Private Sub AdjustCommandButtons()
        If CommandsPanel Is Nothing OrElse CommandsPanel.ClientSize.Width <= 0 Then Return

        Dim buttons() As Button = {CloseButton, SearchBillsButton, CancelBillButton, PrintButton, EditButton, SaveButton, NewButton}
        Dim horizontalSpace As Integer = CommandsPanel.ClientSize.Width - CommandsPanel.Padding.Horizontal
        Dim buttonWidth As Integer = Math.Max(105, (horizontalSpace - (buttons.Length * 6)) \ buttons.Length)
        Dim buttonHeight As Integer = Math.Max(42, CommandsPanel.ClientSize.Height - CommandsPanel.Padding.Vertical)
        For Each commandButton As Button In buttons
            commandButton.Margin = New Padding(3)
            commandButton.Size = New Size(buttonWidth, buttonHeight)
        Next
    End Sub

    Private Sub AdjustSummaryLayout()
        If SummaryPanel Is Nothing OrElse SummaryPanel.ClientSize.Width <= 0 Then Return

        Dim halfWidth As Integer = SummaryPanel.ClientSize.Width \ 2
        Dim rowHeight As Integer = Math.Max(30, SummaryPanel.ClientSize.Height \ 2)
        TotalQuantityLabel.SetBounds(0, 0, halfWidth, rowHeight)
        ItemsCountLabel.SetBounds(halfWidth, 0, SummaryPanel.ClientSize.Width - halfWidth, rowHeight)
        ExpectedSalesValueLabel.SetBounds(0, rowHeight, halfWidth, SummaryPanel.ClientSize.Height - rowHeight)
        TotalCostLabel.SetBounds(halfWidth, rowHeight, SummaryPanel.ClientSize.Width - halfWidth, SummaryPanel.ClientSize.Height - rowHeight)
    End Sub

    Private Sub AdjustContentSplit()
        If ContentSplit Is Nothing OrElse ContentSplit.Width <= 0 Then Return

        Dim availableWidth As Integer = ContentSplit.ClientSize.Width - ContentSplit.SplitterWidth
        If availableWidth <= 0 Then Return

        Dim cartWidth As Integer = CInt(availableWidth * 0.4R)
        Dim minimumCartWidth As Integer = Math.Min(430, availableWidth)
        Dim minimumItemsWidth As Integer = Math.Min(620, availableWidth)

        cartWidth = Math.Max(minimumCartWidth, cartWidth)
        If availableWidth - cartWidth < minimumItemsWidth Then cartWidth = Math.Max(0, availableWidth - minimumItemsWidth)

        If cartWidth > 0 AndAlso cartWidth < availableWidth Then
            ContentSplit.Panel1MinSize = Math.Min(minimumCartWidth, cartWidth)
            ContentSplit.Panel2MinSize = Math.Min(minimumItemsWidth, availableWidth - cartWidth)
            ContentSplit.SplitterDistance = cartWidth
        End If
    End Sub

    Private Sub AdjustItemEditorLayout()
        If EditorPanel Is Nothing OrElse EditorPanel.ClientSize.Width <= 0 Then Return

        Dim width As Integer = EditorPanel.ClientSize.Width
        SelectedItemLabel.SetBounds(10, 5, Math.Max(260, width - 220), 44)
        StockLabel.SetBounds(10, 52, 175, 28)
        CostWarningLabel.SetBounds(10, 82, 175, 34)
        Dim fieldsStart As Integer = 200
        ExpiryLabel.SetBounds(fieldsStart, 52, 130, 20)
        ExpiryDatePicker.SetBounds(fieldsStart, 74, 130, 27)
        PriceLabel.SetBounds(fieldsStart, 108, 90, 22)
        PriceTextBox.SetBounds(fieldsStart, 133, 90, 29)
        QuantityLabel.SetBounds(fieldsStart + 100, 108, 80, 22)
        QuantityTextBox.SetBounds(fieldsStart + 100, 133, 80, 29)
        UnitLabel.SetBounds(fieldsStart + 190, 108, Math.Max(120, width - fieldsStart - 200), 22)
        UnitComboBox.SetBounds(fieldsStart + 190, 133, Math.Max(120, width - fieldsStart - 200), 29)
        StoreLabel.SetBounds(Math.Max(330, width - 205), 24, 185, 22)
        StoreComboBox.SetBounds(Math.Max(330, width - 205), 50, 185, 29)
    End Sub

    Private Sub ConfigureGrid()
        CartGrid.AutoGenerateColumns = False
        CartGrid.Columns.Clear()
        AddGridColumn("item_name", "الصنف", 170)
        AddGridColumn("St_Name", "المخزن", 90)
        AddGridColumn("U_Name", "الوحدة", 75)
        AddGridColumn("QYT", "الكمية", 65, "N3")
        AddGridColumn("Cost", "التكلفة", 75, "N3")
        AddGridColumn("Total", "الإجمالي", 85, "N3")
    End Sub

    Private Sub AddGridColumn(dataProperty As String, header As String, minimumWidth As Integer, Optional format As String = Nothing)
        Dim column As New DataGridViewTextBoxColumn()
        column.DataPropertyName = dataProperty
        column.HeaderText = header
        column.MinimumWidth = minimumWidth
        If Not String.IsNullOrWhiteSpace(format) Then column.DefaultCellStyle.Format = format
        CartGrid.Columns.Add(column)
    End Sub

    Private Sub LoadStores()
        Try
            Dim table As New DataTable()
            Using connection As New SqlConnection(MY_Settings.SqlConStr)
                Using adapter As New SqlDataAdapter("SELECT ST_ID, ST_Name FROM dbo.Stores ORDER BY ST_Name", connection)
                    adapter.Fill(table)
                End Using
            End Using
            StoreComboBox.DataSource = table
            StoreComboBox.DisplayMember = "ST_Name"
            StoreComboBox.ValueMember = "ST_ID"
            If table.Rows.Count > 0 Then StoreComboBox.SelectedValue = PCH_ST_ID
            StoreComboBox.Enabled = PCH_ST_Can_change
        Catch ex As Exception
            SetStatus(ex.Message, True)
        End Try
    End Sub

    Private Sub LoadItems()
        Try
            SetStatus("جاري تحميل الأصناف...", False)
            _categoriesTable.Clear()
            _itemsTable.Clear()
            Using connection As New SqlConnection(MY_Settings.SqlConStr)
                Using categoriesAdapter As New SqlDataAdapter("SELECT GM_ID,GM_Name,BK_R,BK_G,BK_B,FK_R,FK_G,FK_B FROM dbo.General_menu WHERE POS_isShow=1 ORDER BY Rank_Num,GM_ID", connection)
                    categoriesAdapter.Fill(_categoriesTable)
                End Using
                Using itemsAdapter As New SqlDataAdapter("SELECT IM_ID AS ItemID,IM_Num AS IM_NUMBER,item_nameSales AS ItemName,Barcode,GM_ID,isValid,isStore,BK_R,BK_G,BK_B,FK_R,FK_G,FK_B FROM dbo.IM_Menu_V WHERE isActive=1 AND isStore=1 ORDER BY item_nameSales", connection)
                    itemsAdapter.Fill(_itemsTable)
                End Using
            End Using
            RenderCategoryButtons()
            RenderItemButtons()
            SetStatus("تم تحميل الأصناف", False)
        Catch ex As Exception
            SetStatus(ex.Message, True)
        End Try
    End Sub

    Private Sub RenderCategoryButtons()
        CategoryFlowPanel.SuspendLayout()
        CategoryFlowPanel.Controls.Clear()

        Dim allButton As Button = CreateCategoryButton(0, "الكل", Nothing)
        CategoryFlowPanel.Controls.Add(allButton)

        For Each row As DataRow In _categoriesTable.Rows
            CategoryFlowPanel.Controls.Add(CreateCategoryButton(Convert.ToInt32(row("GM_ID")), Convert.ToString(row("GM_Name")), row))
        Next

        If _categoriesTable.Rows.Count > 0 Then
            _selectedCategoryId = Convert.ToInt32(_categoriesTable.Rows(0)("GM_ID"))
        Else
            _selectedCategoryId = 0
        End If
        HighlightSelectedCategory()
        CategoryFlowPanel.ResumeLayout()
        CategoryFlowPanel.AutoScrollPosition = New Point(0, 0)
        UpdateCategoryNavigationButtons()
    End Sub

    Private Function CreateCategoryButton(categoryId As Integer, categoryName As String, sourceRow As DataRow) As Button
        Dim button As New Button()
        Dim scale As Single = CurrentLayoutScale()
        button.Tag = categoryId
        button.Text = categoryName
        button.Size = New Size(CInt(110 * scale), CInt(52 * scale))
        button.Margin = New Padding(4)
        button.FlatStyle = FlatStyle.Flat
        button.Font = New Font("Segoe UI Semibold", 9.0! * scale, FontStyle.Bold)
        button.BackColor = ThemeManager.POSCatBack
        button.ForeColor = ThemeManager.POSCatFore
        If sourceRow IsNot Nothing AndAlso Not IsDBNull(sourceRow("BK_R")) Then
            button.BackColor = Color.FromArgb(Convert.ToInt32(sourceRow("BK_R")), Convert.ToInt32(sourceRow("BK_G")), Convert.ToInt32(sourceRow("BK_B")))
            If Not IsDBNull(sourceRow("FK_R")) Then button.ForeColor = Color.FromArgb(Convert.ToInt32(sourceRow("FK_R")), Convert.ToInt32(sourceRow("FK_G")), Convert.ToInt32(sourceRow("FK_B")))
        End If
        AddHandler button.Click, AddressOf CategoryButton_Click
        Return button
    End Function

    Private Sub CategoryButton_Click(sender As Object, e As EventArgs)
        _selectedCategoryId = Convert.ToInt32(DirectCast(sender, Button).Tag)
        HighlightSelectedCategory()
        RenderItemButtons()
    End Sub

    Private Sub CategoryPreviousButton_Click(sender As Object, e As EventArgs) Handles CategoryPreviousButton.Click
        ScrollCategories(-CategoryFlowPanel.ClientSize.Width)
    End Sub

    Private Sub CategoryNextButton_Click(sender As Object, e As EventArgs) Handles CategoryNextButton.Click
        ScrollCategories(CategoryFlowPanel.ClientSize.Width)
    End Sub

    Private Sub ScrollCategories(changeValue As Integer)
        CategoryFlowPanel.PerformLayout()
        Dim maximumPosition As Integer = Math.Max(0, CategoryFlowPanel.DisplayRectangle.Width - CategoryFlowPanel.ClientSize.Width)
        Dim currentPosition As Integer = Math.Max(0, -CategoryFlowPanel.AutoScrollPosition.X)
        Dim targetPosition As Integer = Math.Max(0, Math.Min(maximumPosition, currentPosition + changeValue))
        CategoryFlowPanel.AutoScrollPosition = New Point(targetPosition, 0)
        UpdateCategoryNavigationButtons()
    End Sub

    Private Sub UpdateCategoryNavigationButtons()
        If CategoryFlowPanel Is Nothing Then Return
        Dim maximumPosition As Integer = Math.Max(0, CategoryFlowPanel.DisplayRectangle.Width - CategoryFlowPanel.ClientSize.Width)
        Dim currentPosition As Integer = Math.Max(0, -CategoryFlowPanel.AutoScrollPosition.X)
        CategoryPreviousButton.Enabled = currentPosition > 0
        CategoryNextButton.Enabled = currentPosition < maximumPosition
    End Sub

    Private Sub CategoryFlowPanel_Resize(sender As Object, e As EventArgs) Handles CategoryFlowPanel.Resize
        UpdateCategoryNavigationButtons()
    End Sub

    Private Sub HighlightSelectedCategory()
        For Each control As Control In CategoryFlowPanel.Controls
            Dim button As Button = TryCast(control, Button)
            If button Is Nothing Then Continue For
            button.FlatAppearance.BorderSize = If(Convert.ToInt32(button.Tag) = _selectedCategoryId, 3, 1)
            button.FlatAppearance.BorderColor = If(Convert.ToInt32(button.Tag) = _selectedCategoryId, Color.DodgerBlue, Color.DimGray)
        Next
    End Sub

    Private Sub RenderItemButtons()
        ItemsFlowPanel.SuspendLayout()
        ItemsFlowPanel.Controls.Clear()
        Dim searchValue As String = SearchTextBox.Text.Trim()
        Dim scale As Single = CurrentLayoutScale()
        For Each row As DataRow In _itemsTable.Rows
            If Not IsDBNull(row("isStore")) AndAlso Convert.ToInt32(row("isStore")) <> 1 Then Continue For
            If _selectedCategoryId > 0 AndAlso Convert.ToInt32(row("GM_ID")) <> _selectedCategoryId Then Continue For
            Dim itemName As String = Convert.ToString(row("ItemName"))
            Dim itemNumber As String = Convert.ToString(row("IM_NUMBER"))
            Dim barcode As String = Convert.ToString(row("Barcode"))
            If searchValue.Length > 0 AndAlso itemName.IndexOf(searchValue, StringComparison.CurrentCultureIgnoreCase) < 0 AndAlso itemNumber.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) < 0 AndAlso barcode.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase) < 0 Then Continue For

            Dim button As New Button()
            button.Tag = row("ItemID")
            button.Text = itemName & If(String.IsNullOrWhiteSpace(itemNumber), "", Environment.NewLine & itemNumber)
            button.Size = New Size(CInt(118 * scale), CInt(60 * scale))
            button.Margin = New Padding(5)
            button.FlatStyle = FlatStyle.Flat
            button.BackColor = Color.White
            If Not IsDBNull(row("BK_R")) Then
                button.BackColor = Color.FromArgb(Convert.ToInt32(row("BK_R")), Convert.ToInt32(row("BK_G")), Convert.ToInt32(row("BK_B")))
                If Not IsDBNull(row("FK_R")) Then button.ForeColor = Color.FromArgb(Convert.ToInt32(row("FK_R")), Convert.ToInt32(row("FK_G")), Convert.ToInt32(row("FK_B")))
            End If
            button.Font = New Font("Segoe UI Semibold", 9.0! * scale, FontStyle.Bold)
            button.TextAlign = ContentAlignment.MiddleCenter
            AddHandler button.Click, AddressOf ItemButton_Click
            ItemsFlowPanel.Controls.Add(button)
        Next
        ItemsFlowPanel.ResumeLayout()
    End Sub

    Private Sub ItemButton_Click(sender As Object, e As EventArgs)
        If Not CanEditDetails() Then Return
        Dim button As Button = DirectCast(sender, Button)
        Dim itemId As Integer = Convert.ToInt32(button.Tag)
        Dim itemName As String = button.Text.Replace(Environment.NewLine, " - ")
        Using editor As New Outside_Sales_Touch_ItemEditor(itemId, itemName, SelectedStoreId(), PCH_ST_Can_change)
            If editor.ShowDialog(Me) <> DialogResult.OK Then Return
            AddConfiguredItemToBill(itemId, itemName, editor.SelectedUnitId, editor.SelectedStoreId, editor.EnteredQuantity, editor.EnteredPrice, editor.RequiresExpiry, editor.SelectedExpiryDate)
        End Using
    End Sub

    Private Function AddConfiguredItemToBill(itemId As Integer, itemName As String, unitId As Integer, storeId As Integer, quantity As Decimal, price As Decimal, requiresExpiry As Boolean, expiryDate As Date) As Boolean
        If Not CanEditDetails() Then Return False
        If itemId <= 0 OrElse unitId <= 0 OrElse storeId <= 0 Then
            SetStatus("بيانات الصنف أو الوحدة أو المخزن غير مكتملة", True)
            Return False
        End If

        Dim duplicateRows() As DataRow = _detailsTable.Select("IM_ID=" & itemId.ToString() & " AND ST_ID=" & storeId.ToString())
        If duplicateRows.Length > 0 AndAlso MessageBox.Show("الصنف مضاف مسبقًا إلى هذا المخزن. هل تريد إضافته مرة أخرى؟", "تنبيه", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) <> DialogResult.OK Then Return False

        Try
            Using command As New SqlCommand("Outsale_Details_Insert")
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add("@Outsale_T_ID", SqlDbType.Int).Value = _transactionId
                command.Parameters.Add("@IM_ID", SqlDbType.Int).Value = itemId
                command.Parameters.Add("@U_ID", SqlDbType.Int).Value = unitId
                command.Parameters.Add("@Price", SqlDbType.Decimal).Value = price
                command.Parameters("@Price").Precision = 18
                command.Parameters("@Price").Scale = 3
                command.Parameters.Add("@ST_ID", SqlDbType.Int).Value = storeId
                command.Parameters.Add("@Barcode", SqlDbType.NVarChar, 50).Value = SELECT_BARCODE(itemId, unitId)
                command.Parameters.Add("@Total", SqlDbType.Decimal).Value = quantity * price
                command.Parameters("@Total").Precision = 18
                command.Parameters("@Total").Scale = 3
                command.Parameters.Add("@QYT", SqlDbType.Decimal).Value = quantity
                command.Parameters("@QYT").Precision = 18
                command.Parameters("@QYT").Scale = 3
                command.Parameters.Add("@On_Update", SqlDbType.Bit).Value = True
                command.Parameters.Add("@SPRICE", SqlDbType.Float).Value = Convert.ToDouble(price)
                If requiresExpiry Then command.Parameters.Add("@D_Vaild", SqlDbType.Date).Value = expiryDate
                If Not SQL_SP_EXEC(command) Then
                    SetStatus("لم تتم إضافة الصنف إلى الإذن", True)
                    Return False
                End If
            End Using

            LoadDetails()
            If _detailsTable.Rows.Count = 0 Then
                SetStatus("تم تنفيذ الإضافة ولكن تعذر إعادة تحميل تفاصيل الإذن", True)
                Return False
            End If
            SelectLastCartRow()
            Network_Edit_Tracker_insert("إضافة صنف من شاشة الاستلام اللمسية: " & itemName & " - الكمية: " & quantity.ToString("N3") & " - التكلفة: " & price.ToString("N3"), _billId.ToString(), 35, 1)
            SetStatus("تمت إضافة " & quantity.ToString("0.###") & " × " & itemName, False)
            Return True
        Catch ex As Exception
            SetStatus(ex.Message, True)
            Return False
        End Try
    End Function

    Private Sub QuantityNumberButton_Click(sender As Object, e As EventArgs) Handles QuantityKey0Button.Click, QuantityKey1Button.Click, QuantityKey2Button.Click, QuantityKey3Button.Click, QuantityKey4Button.Click, QuantityKey5Button.Click, QuantityKey6Button.Click, QuantityKey7Button.Click, QuantityKey8Button.Click, QuantityKey9Button.Click
        If Not CanEditDetails(False) Then Return
        Dim digit As String = Convert.ToString(DirectCast(sender, Button).Tag)
        If _replaceQuantityOnNextKeypadPress OrElse QuantityTextBox.Text = "0" Then
            QuantityTextBox.Text = digit
            _replaceQuantityOnNextKeypadPress = False
        ElseIf QuantityTextBox.Text.Length < 12 Then
            QuantityTextBox.Text &= digit
        End If
        QuantityTextBox.Focus()
        QuantityTextBox.SelectionStart = QuantityTextBox.TextLength
    End Sub

    Private Sub QuantityDecimalButton_Click(sender As Object, e As EventArgs) Handles QuantityDecimalButton.Click
        If Not CanEditDetails(False) Then Return
        Dim separator As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
        If _replaceQuantityOnNextKeypadPress Then
            QuantityTextBox.Text = "0" & separator
            _replaceQuantityOnNextKeypadPress = False
        ElseIf Not QuantityTextBox.Text.Contains(separator) AndAlso Not QuantityTextBox.Text.Contains(".") AndAlso Not QuantityTextBox.Text.Contains(",") Then
            QuantityTextBox.Text &= separator
        End If
        QuantityTextBox.Focus()
        QuantityTextBox.SelectionStart = QuantityTextBox.TextLength
    End Sub

    Private Sub QuantityClearButton_Click(sender As Object, e As EventArgs) Handles QuantityClearButton.Click
        If Not CanEditDetails(False) Then Return
        QuantityTextBox.Text = "0"
        _replaceQuantityOnNextKeypadPress = True
        QuantityTextBox.Focus()
        QuantityTextBox.SelectAll()
    End Sub

    Private Sub QuantityBackspaceButton_Click(sender As Object, e As EventArgs) Handles QuantityBackspaceButton.Click
        If Not CanEditDetails(False) Then Return
        If _replaceQuantityOnNextKeypadPress Then
            QuantityTextBox.Text = "0"
        ElseIf QuantityTextBox.TextLength > 1 Then
            QuantityTextBox.Text = QuantityTextBox.Text.Substring(0, QuantityTextBox.TextLength - 1)
        Else
            QuantityTextBox.Text = "0"
            _replaceQuantityOnNextKeypadPress = True
        End If
        QuantityTextBox.Focus()
        QuantityTextBox.SelectionStart = QuantityTextBox.TextLength
    End Sub

    Private Sub LoadSelectedItemData()
        If _selectedItemId <= 0 Then Return
        Try
            _loading = True
            Using connection As New SqlConnection(MY_Settings.SqlConStr)
                connection.Open()
                Dim units As New DataTable()
                Using adapter As New SqlDataAdapter("SELECT U_IM_ID,U_Name,U_ID,U_Cargo FROM dbo.IM_Menu_Units_V WHERE IM_ID=@IM_ID ORDER BY U_Cargo DESC", connection)
                    adapter.SelectCommand.Parameters.Add("@IM_ID", SqlDbType.Int).Value = _selectedItemId
                    adapter.Fill(units)
                End Using
                UnitComboBox.DataSource = units
                UnitComboBox.DisplayMember = "U_Name"
                UnitComboBox.ValueMember = "U_IM_ID"

                Using cmd As New SqlCommand("SELECT TOP(1) isValid FROM dbo.IM_Menu_V WHERE IM_ID=@IM_ID", connection)
                    cmd.Parameters.Add("@IM_ID", SqlDbType.Int).Value = _selectedItemId
                    Dim value As Object = cmd.ExecuteScalar()
                    _selectedItemRequiresExpiry = value IsNot Nothing AndAlso value IsNot DBNull.Value AndAlso Convert.ToBoolean(value)
                End Using
                ExpiryLabel.Visible = _selectedItemRequiresExpiry
                ExpiryDatePicker.Visible = _selectedItemRequiresExpiry
                ApplySelectedUnit(connection)
            End Using
        Catch ex As Exception
            SetStatus(ex.Message, True)
        Finally
            _loading = False
        End Try
    End Sub

    Private Sub ApplySelectedUnit(connection As SqlConnection)
        If UnitComboBox.SelectedValue Is Nothing OrElse _selectedItemId <= 0 Then Return
        Using cmd As New SqlCommand("SELECT TOP(1) U_ID,U_Cargo FROM dbo.IM_Menu_Units_V WHERE U_IM_ID=@U_IM_ID AND IM_ID=@IM_ID", connection)
            cmd.Parameters.Add("@U_IM_ID", SqlDbType.Int).Value = Convert.ToInt32(UnitComboBox.SelectedValue)
            cmd.Parameters.Add("@IM_ID", SqlDbType.Int).Value = _selectedItemId
            Using reader As SqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    _selectedUnitId = Convert.ToInt32(reader("U_ID"))
                    _unitCargo = Convert.ToDecimal(reader("U_Cargo"))
                End If
            End Using
        End Using

        Dim storeId As Integer = SelectedStoreId()
        Using cmd As New SqlCommand("SELECT ISNULL(SUM(QTY),0) FROM dbo.ST_Balance_V WHERE IM_ID=@IM_ID AND ST_ID=@ST_ID", connection)
            cmd.Parameters.Add("@IM_ID", SqlDbType.Int).Value = _selectedItemId
            cmd.Parameters.Add("@ST_ID", SqlDbType.Int).Value = storeId
            Dim baseQuantity As Decimal = Convert.ToDecimal(cmd.ExecuteScalar())
            Dim displayQuantity As Decimal = If(_unitCargo = 0D, 0D, baseQuantity / _unitCargo)
            StockLabel.Text = "المتوفر في المخزن: " & displayQuantity.ToString("N3")
        End Using

        PriceTextBox.Clear()
        Using cmd As New SqlCommand("SELECT TOP(1) Price FROM dbo.Pch_Details WHERE IM_ID=@IM_ID AND U_ID=@U_ID AND isDepended=1 ORDER BY Date DESC", connection)
            cmd.Parameters.Add("@IM_ID", SqlDbType.Int).Value = _selectedItemId
            cmd.Parameters.Add("@U_ID", SqlDbType.Int).Value = _selectedUnitId
            Dim value As Object = cmd.ExecuteScalar()
            If value IsNot Nothing AndAlso value IsNot DBNull.Value Then PriceTextBox.Text = Convert.ToDecimal(value).ToString("0.###")
        End Using
        UpdateCostWarning()
    End Sub

    Private Sub UnitComboBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles UnitComboBox.SelectedValueChanged
        If _loading OrElse _selectedItemId <= 0 OrElse UnitComboBox.SelectedValue Is Nothing Then Return
        Try
            Using connection As New SqlConnection(MY_Settings.SqlConStr)
                connection.Open()
                ApplySelectedUnit(connection)
            End Using
        Catch ex As Exception
            SetStatus(ex.Message, True)
        End Try
    End Sub

    Private Sub StoreComboBox_SelectedValueChanged(sender As Object, e As EventArgs) Handles StoreComboBox.SelectedValueChanged
        If _loading OrElse _selectedItemId <= 0 Then Return
        UnitComboBox_SelectedValueChanged(sender, e)
    End Sub

    Private Sub AddItemButton_Click(sender As Object, e As EventArgs) Handles AddItemButton.Click
        If Not CanEditDetails() Then Return
        If _selectedItemId <= 0 Then
            SetStatus("حدد صنفًا أولًا", True)
            Return
        End If

        Dim quantity As Decimal
        Dim price As Decimal
        If Not Decimal.TryParse(QuantityTextBox.Text, quantity) OrElse quantity <= 0D Then
            SetStatus("أدخل كمية صحيحة أكبر من صفر", True)
            Return
        End If
        If String.IsNullOrWhiteSpace(PriceTextBox.Text) Then
            price = 0D
        ElseIf Not Decimal.TryParse(PriceTextBox.Text, price) OrElse price < 0D Then
            SetStatus("أدخل تكلفة صحيحة", True)
            Return
        End If
        If _selectedItemRequiresExpiry AndAlso Ban_Expierd_IM_MV AndAlso ExpiryDatePicker.Value.Date <= Date.Today Then
            SetStatus("لا يمكن إدخال صنف منتهي الصلاحية", True)
            Return
        End If

        Dim duplicateRows() As DataRow = _detailsTable.Select("IM_ID=" & _selectedItemId.ToString() & " AND ST_ID=" & SelectedStoreId().ToString())
        If duplicateRows.Length > 0 AndAlso MessageBox.Show("الصنف مضاف مسبقًا إلى هذا المخزن. هل تريد إضافته مرة أخرى؟", "تنبيه", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) <> DialogResult.OK Then Return

        Try
            Using command As New SqlCommand("Outsale_Details_Insert")
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.AddWithValue("@Outsale_T_ID", _transactionId)
                command.Parameters.AddWithValue("@IM_ID", _selectedItemId)
                command.Parameters.AddWithValue("@U_ID", _selectedUnitId)
                command.Parameters.AddWithValue("@Price", price)
                command.Parameters.AddWithValue("@ST_ID", SelectedStoreId())
                command.Parameters.AddWithValue("@Barcode", SELECT_BARCODE(_selectedItemId, _selectedUnitId))
                command.Parameters.AddWithValue("@Total", quantity * price)
                command.Parameters.AddWithValue("@QYT", quantity)
                command.Parameters.AddWithValue("@On_Update", True)
                command.Parameters.AddWithValue("@SPRICE", price)
                If _selectedItemRequiresExpiry Then command.Parameters.AddWithValue("@D_Vaild", ExpiryDatePicker.Value.Date)
                If Not SQL_SP_EXEC(command) Then Return
            End Using
            LoadDetails()
            SelectLastCartRow()
            Network_Edit_Tracker_insert("إضافة صنف من شاشة الاستلام اللمسية: " & SelectedItemLabel.Text & " - الكمية: " & quantity.ToString("N3") & " - التكلفة: " & price.ToString("N3"), _billId.ToString(), 35, 1)
            ClearSelectedItemEditor()
            SetStatus("تمت إضافة الصنف", False)
        Catch ex As Exception
            SetStatus(ex.Message, True)
        End Try
    End Sub

    Private Sub SelectLastCartRow()
        If CartGrid.Rows.Count = 0 Then Return

        Dim lastRowIndex As Integer = CartGrid.Rows.Count - 1
        CartGrid.ClearSelection()
        CartGrid.Rows(lastRowIndex).Selected = True
        If CartGrid.Columns.Count > 0 Then CartGrid.CurrentCell = CartGrid.Rows(lastRowIndex).Cells(0)
        CartGrid.FirstDisplayedScrollingRowIndex = lastRowIndex
    End Sub

    Private Sub ClearSelectedItemEditor()
        _loading = True
        Try
            _selectedItemId = 0
            _selectedItemRequiresExpiry = False
            _selectedUnitId = 0
            _unitCargo = 1D
            SelectedItemLabel.Text = "المس صنفًا لإضافته"
            StockLabel.Text = "المتوفر في المخزن: 0.000"
            QuantityTextBox.Text = "1"
            PriceTextBox.Clear()
            UnitComboBox.DataSource = Nothing
            UnitComboBox.Items.Clear()
            ExpiryDatePicker.Value = Date.Today
            ExpiryDatePicker.Visible = False
            ExpiryLabel.Visible = False
            CostWarningLabel.Visible = False
            _replaceQuantityOnNextKeypadPress = True
        Finally
            _loading = False
        End Try
    End Sub

    Private Sub DeleteLineButton_Click(sender As Object, e As EventArgs) Handles DeleteLineButton.Click
        If Not CanEditDetails() OrElse CartGrid.CurrentRow Is Nothing Then Return
        Dim row As DataRowView = TryCast(CartGrid.CurrentRow.DataBoundItem, DataRowView)
        If row Is Nothing Then Return
        'If MessageBox.Show("حذف الصنف المحدد من الإذن؟", "تأكيد", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) <> DialogResult.OK Then Return
        Using command As New SqlCommand("Outsale_Details_Delete")
            command.CommandType = CommandType.StoredProcedure
            command.Parameters.AddWithValue("@T_ID", Convert.ToInt32(row("T_ID")))
            If SQL_SP_EXEC(command) Then
                Network_Edit_Tracker_insert("حذف صنف من شاشة الاستلام اللمسية: " & Convert.ToString(row("item_name")) & " - الكمية: " & Convert.ToString(row("QYT")), _billId.ToString(), 35, 2)
                LoadDetails()
                SetStatus("تم حذف الصنف", False)
            End If
        End Using
    End Sub

    Private Sub IncreaseLineButton_Click(sender As Object, e As EventArgs) Handles IncreaseLineButton.Click
        ChangeSelectedLineQuantity(1D)
    End Sub

    Private Sub DecreaseLineButton_Click(sender As Object, e As EventArgs) Handles DecreaseLineButton.Click
        ChangeSelectedLineQuantity(-1D)
    End Sub

    Private Sub ChangeSelectedLineQuantity(changeValue As Decimal)
        If Not CanEditDetails() OrElse CartGrid.CurrentRow Is Nothing Then Return
        Dim row As DataRowView = TryCast(CartGrid.CurrentRow.DataBoundItem, DataRowView)
        If row Is Nothing Then Return
        Dim newQuantity As Decimal = SafeDecimal(row("QYT")) + changeValue
        If newQuantity <= 0D Then
            SetStatus("الحد الأدنى للكمية هو وحدة واحدة؛ استخدم حذف الصنف لإزالته", True)
            Return
        End If

        Try
            Using connection As New SqlConnection(MY_Settings.SqlConStr)
                connection.Open()
                Using transaction As SqlTransaction = connection.BeginTransaction()
                    Try
                        Using command As New SqlCommand("UPDATE dbo.Outsale_Details SET QYT=@QYT,ST_QTY=U_Cargo*@QYT,Total=Cost*@QYT WHERE T_ID=@T_ID; EXEC dbo.Recalculate_Agent_Bill_Total @BILL_T_ID;", connection, transaction)
                            command.Parameters.Add("@QYT", SqlDbType.Decimal).Value = newQuantity
                            command.Parameters("@QYT").Precision = 18
                            command.Parameters("@QYT").Scale = 3
                            command.Parameters.Add("@T_ID", SqlDbType.Int).Value = Convert.ToInt32(row("T_ID"))
                            command.Parameters.Add("@BILL_T_ID", SqlDbType.Int).Value = _transactionId
                            command.ExecuteNonQuery()
                        End Using
                        transaction.Commit()
                    Catch
                        transaction.Rollback()
                        Throw
                    End Try
                End Using
            End Using
            Network_Edit_Tracker_insert("تعديل كمية صنف من شاشة الاستلام اللمسية: " & Convert.ToString(row("item_name")) & " - الكمية الجديدة: " & newQuantity.ToString("N3"), _billId.ToString(), 35, 1)
            LoadDetails()
            SetStatus("تم تعديل كمية الصنف", False)
        Catch ex As Exception
            SetStatus(ex.Message, True)
        End Try
    End Sub

    Private Sub LoadLastDraftOrCreate()
        Try
            Dim foundId As Integer
            Using connection As New SqlConnection(MY_Settings.SqlConStr)
                connection.Open()
                Using command As New SqlCommand("SELECT TOP(1) T_ID FROM dbo.Agents_Balance_MV WHERE User_ID=@USER_ID AND BsType_ID=35 AND isDepended=0 AND isVoid=0 AND T_ID BETWEEN @START_ID AND @END_ID ORDER BY T_ID DESC", connection)
                    command.Parameters.Add("@USER_ID", SqlDbType.Int).Value = USER_ID
                    command.Parameters.Add("@START_ID", SqlDbType.Int).Value = START_ID
                    command.Parameters.Add("@END_ID", SqlDbType.Int).Value = END_ID
                    Dim value As Object = command.ExecuteScalar()
                    If value IsNot Nothing AndAlso value IsNot DBNull.Value Then foundId = Convert.ToInt32(value)
                End Using
            End Using
            If foundId > 0 Then
                LoadBill(foundId)
            Else
                CreateNewBill()
            End If
        Catch ex As Exception
            SetStatus(ex.Message, True)
        End Try
    End Sub

    Private Sub CreateNewBill()
        ClearBill()
        Dim command As New SqlCommand("Agents_BalanceMV_insert")
        command.CommandType = CommandType.StoredProcedure
        Dim outputNames() As String = {"@T_ID", "@Pch_ID", "@IMEX_ID", "@Jrd_ID", "@SRtn_ID", "@PRtn_ID", "@Receipt_Num", "@ST_Tran_ID", "@EXP_ID", "@Frm_ID", "@ViewSB_ID", "@Insale_ID", "@Outsale_ID", "@Frm_ID_M", "@ST_Sett_ID"}
        For Each parameterName As String In outputNames
            command.Parameters.Add(parameterName, SqlDbType.Int).Value = 0
            command.Parameters(parameterName).Direction = ParameterDirection.InputOutput
        Next
        command.Parameters.AddWithValue("@AG_ID", 1)
        command.Parameters.AddWithValue("@Date", BillDatePicker.Value)
        command.Parameters.AddWithValue("@BsType_ID", 35)
        command.Parameters.AddWithValue("@User_ID", USER_ID)
        If SQL_SP_EXEC(command) Then
            _transactionId = Convert.ToInt32(command.Parameters("@T_ID").Value)
            LoadBill(_transactionId)
            SetStatus("تم فتح إذن جديد", False)
        End If
    End Sub

    Public Sub LoadBill(transactionId As Integer)
        Try
            Using connection As New SqlConnection(MY_Settings.SqlConStr)
                connection.Open()
                Using command As New SqlCommand("SELECT TOP(1) * FROM dbo.Outsale_Balance_MV_V WHERE T_ID=@T_ID", connection)
                    command.Parameters.Add("@T_ID", SqlDbType.Int).Value = transactionId
                    Using reader As SqlDataReader = command.ExecuteReader()
                        If Not reader.Read() Then Throw New Exception("لم يتم العثور على إذن الاستلام.")
                        _transactionId = transactionId
                        _billId = Convert.ToInt32(reader("Bill_ID"))
                        BillNumberTextBox.Text = "إذن استلام رقم " & _billId.ToString()
                        TitleTextBox.Text = Convert.ToString(reader("Receipt_Title"))
                        NotesTextBox.Text = Convert.ToString(reader("About"))
                        BillDatePicker.Value = Convert.ToDateTime(reader("Date"))
                        _isDepended = Convert.ToBoolean(reader("isDepended"))
                        _isVoid = Convert.ToBoolean(reader("isVoid"))
                        If Not IsDBNull(reader("AG_ID")) Then EmployeeSearch.Set_IM_By_ID(Convert.ToInt32(reader("AG_ID")))
                    End Using
                End Using
            End Using
            _isEditMode = False
            LoadDetails()
            ApplyState()
        Catch ex As Exception
            SetStatus(ex.Message, True)
        End Try
    End Sub

    Private Sub LoadDetails()
        _detailsTable.Clear()
        Using connection As New SqlConnection(MY_Settings.SqlConStr)
            Using adapter As New SqlDataAdapter("SELECT T_ID,ST_ID,IM_ID,St_Name,IM_Num,item_name,D_Vaild,U_Name,U_ID,Cost,Price,QYT,Total,T_SPRICE FROM dbo.Outsale_Details_V WHERE Outsale_T_ID=@T_ID ORDER BY T_ID", connection)
                adapter.SelectCommand.Parameters.Add("@T_ID", SqlDbType.Int).Value = _transactionId
                adapter.Fill(_detailsTable)
            End Using
        End Using
        CartGrid.DataSource = _detailsTable
        RefreshSummary()
    End Sub

    Private Sub RefreshSummary()
        Dim quantity As Decimal
        Dim total As Decimal
        Dim saleValue As Decimal
        For Each row As DataRow In _detailsTable.Rows
            quantity += SafeDecimal(row("QYT"))
            total += SafeDecimal(row("Total"))
            saleValue += SafeDecimal(row("T_SPRICE"))
        Next
        ItemsCountLabel.Text = "الأصناف: " & _detailsTable.Rows.Count.ToString()
        TotalQuantityLabel.Text = "الكميات: " & quantity.ToString("N3")
        TotalCostLabel.Text = "إجمالي التكلفة: " & total.ToString("N3")
        ExpectedSalesValueLabel.Text = "قيمة البيع: " & saleValue.ToString("N3")
    End Sub

    Private Function SafeDecimal(value As Object) As Decimal
        If value Is Nothing OrElse value Is DBNull.Value Then Return 0D
        Return Convert.ToDecimal(value)
    End Function

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click
        If _detailsTable.Rows.Count = 0 Then
            SetStatus("لا يمكن حفظ إذن دون أصناف", True)
            Return
        End If
        SaveHeader()
        Save_Total(_transactionId, SafeDecimal(_detailsTable.Compute("SUM(Total)", "")), 0)
        DependingBill(_transactionId)
        LoadBill(_transactionId)
        SetStatus("تم حفظ واعتماد إذن الاستلام", False)
    End Sub

    Private Sub SaveHeader()
        Save_Title_Name(_transactionId, TitleTextBox.Text)
        Save_About(_transactionId, NotesTextBox.Text)
        Save_Date(_transactionId, BillDatePicker)
        Dim employeeId As Integer
        Integer.TryParse(EmployeeSearch.TXT_ID.Text, employeeId)
        If employeeId > 0 Then Save_AG_Name(_transactionId, employeeId, _isEditMode)
    End Sub

    Private Sub EditButton_Click(sender As Object, e As EventArgs) Handles EditButton.Click
        If _isVoid Then Return
        If Not _isEditMode Then
            If Not Open_Agents_Balance_MV_For_Edit(_transactionId) Then Return
            _isEditMode = True
            _isDepended = False
            SetStatus("تم فتح الإذن للتعديل المباشر", False)
        Else
            SaveHeader()
            _isEditMode = False
            LoadBill(_transactionId)
        End If
        ApplyState()
    End Sub

    Private Sub CancelBillButton_Click(sender As Object, e As EventArgs) Handles CancelBillButton.Click
        If _transactionId <= 0 OrElse _isVoid Then Return
        If MessageBox.Show("سيتم إلغاء إذن الاستلام وكل معاملاته. هل أنت متأكد؟", "إلغاء إذن", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) <> DialogResult.OK Then Return
        Using command As New SqlCommand("AG_Balance_Void_Row")
            command.CommandType = CommandType.StoredProcedure
            command.Parameters.AddWithValue("@T_ID", _transactionId)
            If SQL_SP_EXEC(command) Then LoadBill(_transactionId)
        End Using
    End Sub

    Private Sub NewButton_Click(sender As Object, e As EventArgs) Handles NewButton.Click
        If _transactionId > 0 AndAlso MessageBox.Show("فتح إذن جديد؟", "إذن جديد", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) <> DialogResult.OK Then Return
        CreateNewBill()
    End Sub

    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click
        Try
            Dim report As New ReportConnection()
            If MY_Settings.OutSale_rpt = 0 Then
                report.rp.Load(Application.StartupPath & "\reports\OutSide_Bill.rpt")
            Else
                report.rp.Load(Application.StartupPath & "\reports\OutSide_Bill_A5.rpt")
            End If
            report.CrTables = report.rp.Database.Tables
            For Each reportTable In report.CrTables
                report.crtableLogoninfo = reportTable.LogOnInfo
                report.crtableLogoninfo.ConnectionInfo = report.crConnectionInfo
                reportTable.ApplyLogOnInfo(report.crtableLogoninfo)
            Next
            report.rp.SetParameterValue(0, " تاريخ : " & BillDatePicker.Value.ToString())
            report.rp.SetParameterValue(1, USER_NAME)
            report.rp.SetParameterValue(2, MY_Settings.Server_Desc)
            report.rp.SetParameterValue(3, TotalQuantityLabel.Text)
            report.rp.SetParameterValue(4, _transactionId)
            report.rp.SetParameterValue(5, TotalCostLabel.Text.Replace("إجمالي التكلفة: ", ""))
            report.rp.SetParameterValue(6, " رقم الفاتورة : " & _billId.ToString())
            report.rp.SetParameterValue(7, " العنوان : " & TitleTextBox.Text)
            report.rp.SetParameterValue(8, " الحساب : " & EmployeeSearch.Textt)
            report.rp.SetParameterValue(9, ExpectedSalesValueLabel.Text.Replace("قيمة البيع: ", ""))
            Dim preview As New print()
            preview.CrystalReportViewer1.ReportSource = report.rp
            preview.Show()
            SetStatus("تم فتح معاينة الطباعة", False)
        Catch ex As Exception
            SetStatus(ex.Message, True)
        End Try
    End Sub

    Private Sub SearchBillsButton_Click(sender As Object, e As EventArgs) Handles SearchBillsButton.Click
        Using searchForm As New Outside_Sales_Touch_Search()
            If searchForm.ShowDialog(Me) = DialogResult.OK AndAlso searchForm.SelectedTransactionId > 0 Then
                LoadBill(searchForm.SelectedTransactionId)
            End If
        End Using
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub

    Private Sub SearchTextBox_TextChanged(sender As Object, e As EventArgs) Handles SearchTextBox.TextChanged
        RenderItemButtons()
    End Sub

    Private Sub PriceTextBox_TextChanged(sender As Object, e As EventArgs) Handles PriceTextBox.TextChanged
        UpdateCostWarning()
    End Sub

    Private Sub UpdateCostWarning()
        Dim cost As Decimal
        CostWarningLabel.Visible = _selectedItemId > 0 AndAlso (Not Decimal.TryParse(PriceTextBox.Text, cost) OrElse cost <= 0D)
        If CostWarningLabel.Visible Then CostWarningLabel.BringToFront()
    End Sub

    Private Sub EmployeeSearch_ID_Changed(sender As Object, e As EventArgs) Handles EmployeeSearch.ID_Changed
        If _transactionId <= 0 OrElse Not CanEditDetails(False) Then Return
        Dim employeeId As Integer
        If Integer.TryParse(EmployeeSearch.TXT_ID.Text, employeeId) AndAlso employeeId > 0 Then Save_AG_Name(_transactionId, employeeId, _isEditMode)
    End Sub

    Private Sub NumericTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles QuantityTextBox.KeyPress, PriceTextBox.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Function SelectedStoreId() As Integer
        If StoreComboBox.SelectedValue Is Nothing OrElse StoreComboBox.SelectedValue Is DBNull.Value Then Return 0
        Return Convert.ToInt32(StoreComboBox.SelectedValue)
    End Function

    Private Function CanEditDetails(Optional showMessage As Boolean = True) As Boolean
        Dim allowed As Boolean = Not _isVoid AndAlso (Not _isDepended OrElse _isEditMode)
        If Not allowed AndAlso showMessage Then SetStatus("افتح التعديل أولًا لتغيير الإذن المحفوظ", True)
        Return allowed
    End Function

    Private Sub ApplyState()
        VoidLabel.Visible = _isVoid
        Dim editable As Boolean = CanEditDetails(False)
        EditorPanel.Enabled = editable
        DeleteLineButton.Enabled = editable
        IncreaseLineButton.Enabled = editable
        DecreaseLineButton.Enabled = editable
        AddItemButton.Enabled = editable
        QuantityKeypad.Enabled = editable
        TitleTextBox.Enabled = editable
        NotesTextBox.Enabled = editable
        BillDatePicker.Enabled = editable
        EmployeeSearch.Enabled = editable
        SaveButton.Enabled = editable AndAlso Not _isEditMode
        EditButton.Enabled = Not _isVoid AndAlso _isDepended
        EditButton.Text = If(_isEditMode, "إيقاف التعديل", "فتح التعديل")
        CancelBillButton.Enabled = Not _isVoid AndAlso _isDepended
        PrintButton.Enabled = Not _isVoid AndAlso _isDepended
        CartGrid.BackgroundColor = If(_isVoid, Color.MistyRose, If(editable, Color.LightYellow, Color.Honeydew))
    End Sub

    Private Sub ClearBill()
        _transactionId = 0
        _billId = 0
        _isDepended = False
        _isVoid = False
        _isEditMode = False
        _selectedItemId = 0
        BillNumberTextBox.Clear()
        TitleTextBox.Clear()
        NotesTextBox.Clear()
        BillDatePicker.Value = Date.Now
        EmployeeSearch.Textt = ""
        EmployeeSearch.TXT_ID.Text = "0"
        _detailsTable.Clear()
        SelectedItemLabel.Text = "المس صنفًا لإضافته"
        QuantityTextBox.Text = "1"
        _replaceQuantityOnNextKeypadPress = True
        StockLabel.Text = "المتوفر: 0"
        CostWarningLabel.Visible = False
        RefreshSummary()
    End Sub

    Private Sub SetStatus(message As String, isError As Boolean)
        StatusLabel.Text = message
        StatusLabel.ForeColor = If(isError, Color.MistyRose, Color.WhiteSmoke)
    End Sub
End Class
