Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Drawing.Text
Imports System.Linq
Imports System.Windows.Forms

Public Class FrmSalesPrintLayoutManager

    Private ReadOnly Repository As New SalesPrintRepository(MY_Settings.SqlConStr)
    Private ReadOnly CurrentPrintData As SalesPrintData
    Private CurrentUsageKey As String = SalesPrintRepository.UsageSales
    Private CurrentProfile As SalesPrintProfile = Nothing
    Private IsLoading As Boolean = False
    Private TemporaryTemplates As List(Of SalesPrintTemplate) = Nothing

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(printData As SalesPrintData)
        InitializeComponent()
        CurrentPrintData = printData
    End Sub

    Public Sub New(printData As SalesPrintData, usageKey As String)
        InitializeComponent()
        CurrentPrintData = printData
        CurrentUsageKey = If(String.IsNullOrWhiteSpace(usageKey), SalesPrintRepository.UsageSales, usageKey.Trim().ToUpperInvariant())
    End Sub

    Private Sub FrmSalesPrintLayoutManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetupGrids()
            LoadPrinters()
            cmbPrinter.Enabled = False
            LoadInstalledFonts()
            LoadTemporaryTemplates()
            SetupUsageKindSelector()
            UpdatePrintActionsState()
            Repository.EnsureSchema()
            LoadProfiles()
            ThemeManager.ApplyThemeToForm(Me)
        Catch ex As Exception
            MsgBox("تعذر تحميل إعدادات التقرير الديناميكي." & vbNewLine & ex.Message, MsgBoxStyle.Exclamation, "التقرير الديناميكي")
        End Try
    End Sub

    Private Sub SetupGrids()
        ConfigureComponentsGrid(dgvSections)
        ConfigureComponentsGrid(dgvColumns)
        ConfigureStylesGrid()
    End Sub

    Private Sub ConfigureComponentsGrid(grid As DataGridView)
        grid.AutoGenerateColumns = False
        grid.Columns.Clear()
        grid.RightToLeft = RightToLeft.Yes
        grid.Font = New Font("Segoe UI Semibold", 9.0!, FontStyle.Bold)
        grid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
        grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215)
        grid.DefaultCellStyle.SelectionForeColor = Color.White

        Dim visibleColumn As New DataGridViewCheckBoxColumn()
        visibleColumn.DataPropertyName = "IsVisible"
        visibleColumn.HeaderText = "إظهار"
        visibleColumn.Name = "IsVisible"
        visibleColumn.FillWeight = 45
        grid.Columns.Add(visibleColumn)

        Dim nameColumn As New DataGridViewTextBoxColumn()
        nameColumn.DataPropertyName = "DisplayName"
        nameColumn.HeaderText = "الاسم"
        nameColumn.Name = "DisplayName"
        nameColumn.FillWeight = 140
        grid.Columns.Add(nameColumn)

        Dim orderColumn As New DataGridViewTextBoxColumn()
        orderColumn.DataPropertyName = "SortOrder"
        orderColumn.HeaderText = "الترتيب"
        orderColumn.Name = "SortOrder"
        orderColumn.FillWeight = 55
        grid.Columns.Add(orderColumn)

        Dim widthColumn As New DataGridViewTextBoxColumn()
        widthColumn.DataPropertyName = "WidthValue"
        widthColumn.HeaderText = "العرض"
        widthColumn.Name = "WidthValue"
        widthColumn.FillWeight = 55
        grid.Columns.Add(widthColumn)

        Dim alignColumn As New DataGridViewComboBoxColumn()
        alignColumn.DataPropertyName = "AlignmentValue"
        alignColumn.HeaderText = "المحاذاة"
        alignColumn.Name = "AlignmentValue"
        alignColumn.Items.AddRange("Right", "Center", "Left")
        alignColumn.FillWeight = 75
        grid.Columns.Add(alignColumn)

        Dim codeColumn As New DataGridViewTextBoxColumn()
        codeColumn.DataPropertyName = "ComponentCode"
        codeColumn.HeaderText = "Code"
        codeColumn.Name = "ComponentCode"
        codeColumn.Visible = False
        grid.Columns.Add(codeColumn)

        Dim scopeColumn As New DataGridViewTextBoxColumn()
        scopeColumn.DataPropertyName = "ComponentScope"
        scopeColumn.HeaderText = "Scope"
        scopeColumn.Name = "ComponentScope"
        scopeColumn.Visible = False
        grid.Columns.Add(scopeColumn)
    End Sub

    Private Sub ConfigureStylesGrid()
        dgvStyles.AutoGenerateColumns = False
        dgvStyles.Columns.Clear()
        dgvStyles.RightToLeft = RightToLeft.Yes
        dgvStyles.Font = New Font("Segoe UI Semibold", 9.0!, FontStyle.Bold)
        dgvStyles.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
        dgvStyles.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvStyles.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvStyles.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215)
        dgvStyles.DefaultCellStyle.SelectionForeColor = Color.White
        dgvStyles.EditMode = DataGridViewEditMode.EditOnEnter

        Dim nameColumn As New DataGridViewTextBoxColumn()
        nameColumn.DataPropertyName = "DisplayName"
        nameColumn.HeaderText = "الخاصية"
        nameColumn.Name = "DisplayName"
        nameColumn.ReadOnly = True
        nameColumn.FillWeight = 150
        dgvStyles.Columns.Add(nameColumn)

        Dim valueColumn As New DataGridViewTextBoxColumn()
        valueColumn.DataPropertyName = "StyleValue"
        valueColumn.HeaderText = "القيمة"
        valueColumn.Name = "StyleValue"
        valueColumn.ReadOnly = False
        valueColumn.FillWeight = 120
        dgvStyles.Columns.Add(valueColumn)

        Dim pickColumn As New DataGridViewButtonColumn()
        pickColumn.HeaderText = "لون"
        pickColumn.Name = "PickColor"
        pickColumn.Text = "اختيار"
        pickColumn.UseColumnTextForButtonValue = False
        pickColumn.FillWeight = 55
        dgvStyles.Columns.Add(pickColumn)

        Dim codeColumn As New DataGridViewTextBoxColumn()
        codeColumn.DataPropertyName = "StyleCode"
        codeColumn.HeaderText = "Code"
        codeColumn.Name = "StyleCode"
        codeColumn.Visible = False
        dgvStyles.Columns.Add(codeColumn)

        Dim typeColumn As New DataGridViewTextBoxColumn()
        typeColumn.DataPropertyName = "StyleType"
        typeColumn.HeaderText = "Type"
        typeColumn.Name = "StyleType"
        typeColumn.Visible = False
        dgvStyles.Columns.Add(typeColumn)
    End Sub

    Private Sub LoadInstalledFonts()
        cmbFontFamily.Items.Clear()

        Try
            Using installedFonts As New InstalledFontCollection()
                Dim fontNames As New List(Of String)()
                For Each family As FontFamily In installedFonts.Families
                    fontNames.Add(family.Name)
                Next

                For Each fontName As String In fontNames.OrderBy(Function(f) f)
                    cmbFontFamily.Items.Add(fontName)
                Next
            End Using
        Catch
        End Try

        EnsureFontInList("Segoe UI")
        cmbFontFamily.SelectedItem = "Segoe UI"
    End Sub

    Private Sub EnsureFontInList(fontName As String)
        If String.IsNullOrWhiteSpace(fontName) Then Return
        If cmbFontFamily.Items.Contains(fontName) = False Then cmbFontFamily.Items.Add(fontName)
    End Sub

    Private Sub LoadTemporaryTemplates()
        TemporaryTemplates = New List(Of SalesPrintTemplate)()
        TemporaryTemplates.Add(New SalesPrintTemplate("A4 تفصيلي", "A4", False, 35, 35, 40, 45, 15D, 10D, 9D, 8D, 8D, 9D, 8D, Color.FromArgb(45, 62, 80), Color.White, Color.White, Color.FromArgb(247, 249, 252), Color.FromArgb(235, 240, 245), True, True))
        TemporaryTemplates.Add(New SalesPrintTemplate("A5 مختصر", "A5", False, 28, 28, 32, 35, 13D, 9D, 8D, 7.5D, 7.5D, 8D, 7.5D, Color.FromArgb(31, 78, 121), Color.White, Color.White, Color.FromArgb(242, 246, 250), Color.FromArgb(232, 240, 248), True, True))
        TemporaryTemplates.Add(New SalesPrintTemplate("A6 مختصر", "A6", False, 18, 18, 22, 25, 11D, 8D, 7.5D, 7D, 7D, 7.5D, 7D, Color.FromArgb(69, 69, 69), Color.White, Color.White, Color.FromArgb(245, 245, 245), Color.FromArgb(238, 238, 238), True, True))
        TemporaryTemplates.Add(New SalesPrintTemplate("Receipt حراري", "RECEIPT", False, 4, 4, 8, 8, 11D, 8D, 7.5D, 7D, 7D, 8D, 7D, Color.Black, Color.White, Color.White, Color.White, Color.WhiteSmoke, False, False))

        lstTemplates.DataSource = TemporaryTemplates
        lstTemplates.DisplayMember = "TemplateName"
        If lstTemplates.Items.Count > 0 Then lstTemplates.SelectedIndex = 0
    End Sub

    Private Sub UpdatePrintActionsState()
        Dim hasPrintData As Boolean = (CurrentPrintData IsNot Nothing)
        btnPreview.Enabled = hasPrintData
        btnPrint.Enabled = hasPrintData
    End Sub

    Private Sub SetupUsageKindSelector()
        Dim showUsageSelector As Boolean = IsPosUsageKey(CurrentUsageKey)
        lblUsageKind.Visible = showUsageSelector
        cmbUsageKind.Visible = showUsageSelector

        If showUsageSelector = False Then Return

        IsLoading = True
        If CurrentUsageKey = SalesPrintRepository.UsagePosOrder Then
            cmbUsageKind.SelectedIndex = 1
        Else
            cmbUsageKind.SelectedIndex = 0
            CurrentUsageKey = SalesPrintRepository.UsagePos
        End If
        IsLoading = False
    End Sub

    Private Function IsPosUsageKey(usageKey As String) As Boolean
        If String.IsNullOrWhiteSpace(usageKey) Then Return False
        usageKey = usageKey.Trim().ToUpperInvariant()
        Return usageKey = SalesPrintRepository.UsagePos OrElse usageKey = SalesPrintRepository.UsagePosOrder
    End Function

    Private Function GetSelectedPosUsageKey() As String
        If cmbUsageKind.SelectedIndex = 1 Then Return SalesPrintRepository.UsagePosOrder
        Return SalesPrintRepository.UsagePos
    End Function

    Private Sub LoadPrinters()
        cmbPrinter.Items.Clear()
        cmbPrinter.Items.Add("")

        For Each printerName As String In PrinterSettings.InstalledPrinters
            cmbPrinter.Items.Add(printerName)
        Next
    End Sub

    Private Sub LoadProfiles(Optional selectProfileId As Integer = 0)
        IsLoading = True

        Dim dt As DataTable = Repository.LoadProfilesTable(CurrentUsageKey)
        cmbProfiles.DataSource = dt
        cmbProfiles.DisplayMember = "ProfileName"
        cmbProfiles.ValueMember = "ProfileID"

        IsLoading = False

        If dt.Rows.Count = 0 Then
            CurrentProfile = Repository.LoadDefaultProfile(CurrentUsageKey)
            LoadProfiles(CurrentProfile.ProfileID)
            Return
        End If

        If selectProfileId > 0 Then
            cmbProfiles.SelectedValue = selectProfileId
        End If

        LoadSelectedProfile()
    End Sub

    Private Sub LoadSelectedProfile()
        If IsLoading Then Return
        If cmbProfiles.SelectedValue Is Nothing OrElse TypeOf cmbProfiles.SelectedValue Is DataRowView Then Return

        Dim profileId As Integer = Convert.ToInt32(cmbProfiles.SelectedValue)
        CurrentProfile = Repository.LoadProfile(profileId)
        BindProfile(CurrentProfile)
    End Sub

    Private Sub BindProfile(profile As SalesPrintProfile)
        If profile Is Nothing Then Return

        IsLoading = True

        txtProfileName.Text = profile.ProfileName
        cmbPaperKind.SelectedItem = If(String.IsNullOrWhiteSpace(profile.PaperKind), "A4", profile.PaperKind)
        EnsureFontInList(profile.FontFamily)
        cmbFontFamily.SelectedItem = profile.FontFamily
        UpdateLocalPrinterSelection(profile.PaperKind)
        numMarginLeft.Value = ClampMargin(profile.MarginLeft)
        numMarginRight.Value = ClampMargin(profile.MarginRight)
        numMarginTop.Value = ClampMargin(profile.MarginTop)
        numMarginBottom.Value = ClampMargin(profile.MarginBottom)
        numLogoWidth.Value = ClampLogoDimension(profile.LogoWidth)
        numLogoHeight.Value = ClampLogoDimension(profile.LogoHeight)
        chkLandscape.Checked = profile.Landscape

        dgvSections.DataSource = ComponentsToTable(profile.Components.Where(Function(c) c.ComponentScope = "SECTION").OrderBy(Function(c) c.SortOrder).ToList())
        dgvColumns.DataSource = ComponentsToTable(profile.Components.Where(Function(c) c.ComponentScope = "COLUMN").OrderBy(Function(c) c.SortOrder).ToList())
        dgvStyles.DataSource = StylesToTable(profile)
        FormatStyleRows()

        IsLoading = False
    End Sub

    Private Function ClampMargin(value As Integer) As Decimal
        If value < 0 Then Return 0
        If value > 200 Then Return 200
        Return value
    End Function

    Private Function ClampLogoDimension(value As Integer) As Decimal
        If value < 20 Then Return 20
        If value > 300 Then Return 300
        Return value
    End Function

    Private Sub EnsurePrinterInList(printerName As String)
        If String.IsNullOrWhiteSpace(printerName) Then Return
        If cmbPrinter.Items.Contains(printerName) = False Then cmbPrinter.Items.Add(printerName)
    End Sub

    Private Sub UpdateLocalPrinterSelection(paperKind As String)
        Dim localPrinter As String = SalesPrintPrinterResolver.GetLocalPrinterName(paperKind)
        EnsurePrinterInList(localPrinter)
        cmbPrinter.SelectedItem = If(String.IsNullOrWhiteSpace(localPrinter), "", localPrinter)
    End Sub

    Private Function ComponentsToTable(components As List(Of SalesPrintComponent)) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("IsVisible", GetType(Boolean))
        dt.Columns.Add("DisplayName", GetType(String))
        dt.Columns.Add("SortOrder", GetType(Integer))
        dt.Columns.Add("WidthValue", GetType(Integer))
        dt.Columns.Add("AlignmentValue", GetType(String))
        dt.Columns.Add("ComponentCode", GetType(String))
        dt.Columns.Add("ComponentScope", GetType(String))

        For Each component As SalesPrintComponent In components
            Dim row As DataRow = dt.NewRow()
            row("IsVisible") = component.IsVisible
            row("DisplayName") = component.DisplayName
            row("SortOrder") = component.SortOrder
            row("WidthValue") = component.WidthValue
            row("AlignmentValue") = component.AlignmentValue
            row("ComponentCode") = component.ComponentCode
            row("ComponentScope") = component.ComponentScope
            dt.Rows.Add(row)
        Next

        Return dt
    End Function

    Private Function StylesToTable(profile As SalesPrintProfile) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("DisplayName", GetType(String))
        dt.Columns.Add("StyleValue", GetType(String))
        dt.Columns.Add("StyleCode", GetType(String))
        dt.Columns.Add("StyleType", GetType(String))

        AddStyleRow(dt, "TitleFontSize", "حجم خط العنوان", profile.TitleFontSize.ToString(), "Number")
        AddStyleRow(dt, "SubTitleFontSize", "حجم خط العنوان الفرعي", profile.SubTitleFontSize.ToString(), "Number")
        AddStyleRow(dt, "InfoFontSize", "حجم خط معلومات الفاتورة", profile.InfoFontSize.ToString(), "Number")
        AddStyleRow(dt, "HeaderFontSize", "حجم خط رأس الجدول", profile.HeaderFontSize.ToString(), "Number")
        AddStyleRow(dt, "RowFontSize", "حجم خط البيانات", profile.RowFontSize.ToString(), "Number")
        AddStyleRow(dt, "TotalFontSize", "حجم خط الإجماليات", profile.TotalFontSize.ToString(), "Number")
        AddStyleRow(dt, "FooterFontSize", "حجم خط التذييل", profile.FooterFontSize.ToString(), "Number")
        AddStyleRow(dt, "TitleForeColorArgb", "لون العنوان", ColorToHex(profile.TitleForeColorArgb), "Color")
        AddStyleRow(dt, "TextForeColorArgb", "لون النص", ColorToHex(profile.TextForeColorArgb), "Color")
        AddStyleRow(dt, "HeaderBackColorArgb", "خلفية رأس الجدول", ColorToHex(profile.HeaderBackColorArgb), "Color")
        AddStyleRow(dt, "HeaderForeColorArgb", "لون نص رأس الجدول", ColorToHex(profile.HeaderForeColorArgb), "Color")
        AddStyleRow(dt, "RowBackColorArgb", "خلفية الصف", ColorToHex(profile.RowBackColorArgb), "Color")
        AddStyleRow(dt, "AlternateRowBackColorArgb", "خلفية الصف البديل", ColorToHex(profile.AlternateRowBackColorArgb), "Color")
        AddStyleRow(dt, "BorderColorArgb", "لون الحدود", ColorToHex(profile.BorderColorArgb), "Color")
        AddStyleRow(dt, "TotalBackColorArgb", "خلفية الإجماليات", ColorToHex(profile.TotalBackColorArgb), "Color")
        AddStyleRow(dt, "TotalForeColorArgb", "لون نص الإجماليات", ColorToHex(profile.TotalForeColorArgb), "Color")
        AddStyleRow(dt, "PaymentBackColorArgb", "خلفية تفاصيل الدفع", ColorToHex(profile.PaymentBackColorArgb), "Color")
        AddStyleRow(dt, "PaymentForeColorArgb", "لون تفاصيل الدفع", ColorToHex(profile.PaymentForeColorArgb), "Color")
        AddStyleRow(dt, "PaidForeColorArgb", "لون إجمالي المدفوع", ColorToHex(profile.PaidForeColorArgb), "Color")
        AddStyleRow(dt, "RemainingForeColorArgb", "لون المبلغ المتبقي", ColorToHex(profile.RemainingForeColorArgb), "Color")
        AddStyleRow(dt, "FooterForeColorArgb", "لون التذييل", ColorToHex(profile.FooterForeColorArgb), "Color")
        AddStyleRow(dt, "UseAlternatingRows", "تلوين الصفوف بالتبادل", profile.UseAlternatingRows.ToString(), "Bool")
        AddStyleRow(dt, "DrawGridLines", "إظهار حدود الجدول", profile.DrawGridLines.ToString(), "Bool")

        Return dt
    End Function

    Private Sub AddStyleRow(dt As DataTable, code As String, displayName As String, value As String, styleType As String)
        Dim row As DataRow = dt.NewRow()
        row("StyleCode") = code
        row("DisplayName") = displayName
        row("StyleValue") = value
        row("StyleType") = styleType
        dt.Rows.Add(row)
    End Sub

    Private Function BuildProfileFromControls() As SalesPrintProfile
        Dim profile As SalesPrintProfile = If(CurrentProfile Is Nothing, Repository.CreateDefaultProfile(CurrentUsageKey), CurrentProfile.CloneProfile())

        profile.ProfileName = txtProfileName.Text.Trim()
        If String.IsNullOrWhiteSpace(profile.ProfileName) Then profile.ProfileName = "تصميم فاتورة المبيعات"
        profile.UsageKey = CurrentUsageKey
        profile.PaperKind = If(cmbPaperKind.SelectedItem Is Nothing, "A4", cmbPaperKind.SelectedItem.ToString())
        profile.PrinterName = ""
        profile.MarginLeft = Convert.ToInt32(numMarginLeft.Value)
        profile.MarginRight = Convert.ToInt32(numMarginRight.Value)
        profile.MarginTop = Convert.ToInt32(numMarginTop.Value)
        profile.MarginBottom = Convert.ToInt32(numMarginBottom.Value)
        profile.LogoWidth = Convert.ToInt32(numLogoWidth.Value)
        profile.LogoHeight = Convert.ToInt32(numLogoHeight.Value)
        profile.Landscape = chkLandscape.Checked
        profile.FontFamily = If(cmbFontFamily.SelectedItem Is Nothing, "Segoe UI", cmbFontFamily.SelectedItem.ToString())
        profile.Components.Clear()
        profile.Components.AddRange(ReadComponentsFromGrid(dgvSections, "SECTION"))
        profile.Components.AddRange(ReadComponentsFromGrid(dgvColumns, "COLUMN"))
        ApplyProfileStylesFromGrid(profile)

        Return profile
    End Function

    Private Sub ApplyProfileStylesFromGrid(profile As SalesPrintProfile)
        If profile Is Nothing Then Return

        profile.TitleFontSize = GetStyleDecimal("TitleFontSize", profile.TitleFontSize)
        profile.SubTitleFontSize = GetStyleDecimal("SubTitleFontSize", profile.SubTitleFontSize)
        profile.InfoFontSize = GetStyleDecimal("InfoFontSize", profile.InfoFontSize)
        profile.HeaderFontSize = GetStyleDecimal("HeaderFontSize", profile.HeaderFontSize)
        profile.RowFontSize = GetStyleDecimal("RowFontSize", profile.RowFontSize)
        profile.TotalFontSize = GetStyleDecimal("TotalFontSize", profile.TotalFontSize)
        profile.FooterFontSize = GetStyleDecimal("FooterFontSize", profile.FooterFontSize)
        profile.TitleForeColorArgb = GetStyleColor("TitleForeColorArgb", profile.TitleForeColorArgb)
        profile.TextForeColorArgb = GetStyleColor("TextForeColorArgb", profile.TextForeColorArgb)
        profile.HeaderBackColorArgb = GetStyleColor("HeaderBackColorArgb", profile.HeaderBackColorArgb)
        profile.HeaderForeColorArgb = GetStyleColor("HeaderForeColorArgb", profile.HeaderForeColorArgb)
        profile.RowBackColorArgb = GetStyleColor("RowBackColorArgb", profile.RowBackColorArgb)
        profile.AlternateRowBackColorArgb = GetStyleColor("AlternateRowBackColorArgb", profile.AlternateRowBackColorArgb)
        profile.BorderColorArgb = GetStyleColor("BorderColorArgb", profile.BorderColorArgb)
        profile.TotalBackColorArgb = GetStyleColor("TotalBackColorArgb", profile.TotalBackColorArgb)
        profile.TotalForeColorArgb = GetStyleColor("TotalForeColorArgb", profile.TotalForeColorArgb)
        profile.PaymentBackColorArgb = GetStyleColor("PaymentBackColorArgb", profile.PaymentBackColorArgb)
        profile.PaymentForeColorArgb = GetStyleColor("PaymentForeColorArgb", profile.PaymentForeColorArgb)
        profile.PaidForeColorArgb = GetStyleColor("PaidForeColorArgb", profile.PaidForeColorArgb)
        profile.RemainingForeColorArgb = GetStyleColor("RemainingForeColorArgb", profile.RemainingForeColorArgb)
        profile.FooterForeColorArgb = GetStyleColor("FooterForeColorArgb", profile.FooterForeColorArgb)
        profile.UseAlternatingRows = GetStyleBoolean("UseAlternatingRows", profile.UseAlternatingRows)
        profile.DrawGridLines = GetStyleBoolean("DrawGridLines", profile.DrawGridLines)
    End Sub

    Private Function GetStyleText(code As String, defaultValue As String) As String
        Dim value As String = GetStyleValue(code)
        If String.IsNullOrWhiteSpace(value) Then Return defaultValue
        Return value.Trim()
    End Function

    Private Function GetStyleDecimal(code As String, defaultValue As Decimal) As Decimal
        Dim value As String = GetStyleValue(code)
        Dim result As Decimal
        If Decimal.TryParse(value, result) = False Then Return defaultValue
        If result < 5D Then result = 5D
        If result > 32D Then result = 32D
        Return result
    End Function

    Private Function GetStyleColor(code As String, defaultValue As Integer) As Integer
        Dim value As String = GetStyleValue(code)
        If String.IsNullOrWhiteSpace(value) Then Return defaultValue

        Try
            Return ColorTranslator.FromHtml(value.Trim()).ToArgb()
        Catch
            Return defaultValue
        End Try
    End Function

    Private Function GetStyleBoolean(code As String, defaultValue As Boolean) As Boolean
        Dim value As String = GetStyleValue(code).Trim()
        If String.IsNullOrWhiteSpace(value) Then Return defaultValue

        If value = "1" OrElse value.Equals("True", StringComparison.OrdinalIgnoreCase) OrElse value = "نعم" Then Return True
        If value = "0" OrElse value.Equals("False", StringComparison.OrdinalIgnoreCase) OrElse value = "لا" Then Return False

        Return defaultValue
    End Function

    Private Function GetStyleValue(code As String) As String
        For Each row As DataGridViewRow In dgvStyles.Rows
            If row.IsNewRow Then Continue For
            If GetCellString(row, "StyleCode") = code Then Return GetCellString(row, "StyleValue")
        Next

        Return ""
    End Function

    Private Function ReadComponentsFromGrid(grid As DataGridView, scope As String) As List(Of SalesPrintComponent)
        Dim list As New List(Of SalesPrintComponent)()

        For Each row As DataGridViewRow In grid.Rows
            If row.IsNewRow Then Continue For

            Dim component As New SalesPrintComponent()
            component.ComponentScope = scope
            component.ComponentCode = GetCellString(row, "ComponentCode")
            component.DisplayName = GetCellString(row, "DisplayName")
            component.IsVisible = GetCellBoolean(row, "IsVisible")
            component.SortOrder = GetCellInteger(row, "SortOrder")
            component.WidthValue = Math.Max(10, GetCellInteger(row, "WidthValue"))
            component.AlignmentValue = GetCellString(row, "AlignmentValue")
            If String.IsNullOrWhiteSpace(component.AlignmentValue) Then component.AlignmentValue = "Center"
            list.Add(component)
        Next

        Return list
    End Function

    Private Function GetCellString(row As DataGridViewRow, columnName As String) As String
        If row.Cells(columnName).Value Is Nothing OrElse row.Cells(columnName).Value Is DBNull.Value Then Return ""
        Return row.Cells(columnName).Value.ToString()
    End Function

    Private Function GetCellBoolean(row As DataGridViewRow, columnName As String) As Boolean
        If row.Cells(columnName).Value Is Nothing OrElse row.Cells(columnName).Value Is DBNull.Value Then Return False
        Return Convert.ToBoolean(row.Cells(columnName).Value)
    End Function

    Private Function GetCellInteger(row As DataGridViewRow, columnName As String) As Integer
        If row.Cells(columnName).Value Is Nothing OrElse row.Cells(columnName).Value Is DBNull.Value Then Return 0
        Dim value As Integer = 0
        Integer.TryParse(row.Cells(columnName).Value.ToString(), value)
        Return value
    End Function

    Private Sub cmbProfiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProfiles.SelectedIndexChanged
        LoadSelectedProfile()
    End Sub

    Private Sub cmbPaperKind_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPaperKind.SelectedIndexChanged
        If IsLoading Then Return
        If cmbPaperKind.SelectedItem Is Nothing Then Return

        UpdateLocalPrinterSelection(cmbPaperKind.SelectedItem.ToString())
    End Sub

    Private Sub cmbUsageKind_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbUsageKind.SelectedIndexChanged
        If IsLoading Then Return
        If cmbUsageKind.Visible = False Then Return

        Dim selectedUsageKey As String = GetSelectedPosUsageKey()
        If CurrentUsageKey = selectedUsageKey Then Return

        CurrentUsageKey = selectedUsageKey
        LoadProfiles()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        CurrentProfile = Repository.CreateDefaultProfile(CurrentUsageKey)
        CurrentProfile.ProfileID = 0
        CurrentProfile.ProfileName = "تصميم جديد"
        CurrentProfile.IsDefault = False
        BindProfile(CurrentProfile)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveCurrentProfile(False)
    End Sub

    Private Sub btnDefault_Click(sender As Object, e As EventArgs) Handles btnDefault.Click
        SaveCurrentProfile(True)
    End Sub

    Private Sub SaveCurrentProfile(makeDefault As Boolean)
        Try
            EndGridEdit()
            Dim profile As SalesPrintProfile = BuildProfileFromControls()
            If makeDefault Then profile.IsDefault = True
            Dim profileId As Integer = Repository.SaveProfile(profile)
            CurrentProfile = Repository.LoadProfile(profileId)
            LoadProfiles(profileId)
            MsgBox("تم حفظ تصميم التقرير بنجاح.", MsgBoxStyle.Information, "التقرير الديناميكي")
        Catch ex As Exception
            MsgBox("تعذر حفظ تصميم التقرير." & vbNewLine & ex.Message, MsgBoxStyle.Exclamation, "التقرير الديناميكي")
        End Try
    End Sub

    Private Sub EndGridEdit()
        If dgvSections.IsCurrentCellDirty Then dgvSections.CommitEdit(DataGridViewDataErrorContexts.Commit)
        If dgvColumns.IsCurrentCellDirty Then dgvColumns.CommitEdit(DataGridViewDataErrorContexts.Commit)
        If dgvStyles.IsCurrentCellDirty Then dgvStyles.CommitEdit(DataGridViewDataErrorContexts.Commit)
        dgvSections.EndEdit()
        dgvColumns.EndEdit()
        dgvStyles.EndEdit()
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        If CurrentPrintData Is Nothing Then
            MsgBox("لا توجد بيانات فاتورة للمعاينة. افتح الشاشة من فاتورة مبيعات.", MsgBoxStyle.Exclamation, "التقرير الديناميكي")
            Return
        End If

        Try
            EndGridEdit()
            Dim profile As SalesPrintProfile = BuildProfileFromControls()
            SalesPrintPrinterResolver.ApplyLocalPrinter(profile)
            Using doc As PrintDocument = New SalesPrintDocumentRenderer(CurrentPrintData, profile).CreatePrintDocument()
                Using preview As New PrintPreviewDialog()
                    preview.Document = doc
                    preview.WindowState = FormWindowState.Maximized
                    preview.Text = "معاينة التقرير الديناميكي"
                    preview.ShowDialog(Me)
                End Using
            End Using
        Catch ex As Exception
            MsgBox("تعذر عرض المعاينة." & vbNewLine & ex.Message, MsgBoxStyle.Exclamation, "التقرير الديناميكي")
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If CurrentPrintData Is Nothing Then
            MsgBox("لا توجد بيانات فاتورة للطباعة. افتح الشاشة من فاتورة مبيعات.", MsgBoxStyle.Exclamation, "التقرير الديناميكي")
            Return
        End If

        Try
            EndGridEdit()
            Dim profile As SalesPrintProfile = BuildProfileFromControls()
            SalesPrintPrinterResolver.ApplyLocalPrinter(profile)
            Using doc As PrintDocument = New SalesPrintDocumentRenderer(CurrentPrintData, profile).CreatePrintDocument()
                doc.PrintController = New StandardPrintController()
                doc.Print()
            End Using
        Catch ex As Exception
            MsgBox("تعذر طباعة التقرير." & vbNewLine & ex.Message, MsgBoxStyle.Exclamation, "التقرير الديناميكي")
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgvStyles_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvStyles.CellClick
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
        Dim columnName As String = dgvStyles.Columns(e.ColumnIndex).Name
        If columnName <> "PickColor" AndAlso columnName <> "StyleValue" Then Return

        Dim row As DataGridViewRow = dgvStyles.Rows(e.RowIndex)
        If GetCellString(row, "StyleType") <> "Color" Then Return

        SelectStyleColor(row)
    End Sub

    Private Sub SelectStyleColor(row As DataGridViewRow)
        If row Is Nothing Then Return

        Using dialog As New ColorDialog()
            Try
                dialog.Color = ColorTranslator.FromHtml(GetCellString(row, "StyleValue"))
            Catch
                dialog.Color = Color.Black
            End Try

            If dialog.ShowDialog(Me) = DialogResult.OK Then
                row.Cells("StyleValue").Value = ColorTranslator.ToHtml(dialog.Color)
                FormatStyleRows()
            End If
        End Using
    End Sub

    Private Sub dgvStyles_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvStyles.CellEndEdit
        FormatStyleRows()
    End Sub

    Private Sub dgvStyles_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvStyles.DataBindingComplete
        FormatStyleRows()
    End Sub

    Private Sub lstTemplates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTemplates.SelectedIndexChanged
        pnlTemplatePreview.Invalidate()
    End Sub

    Private Sub pnlTemplatePreview_Paint(sender As Object, e As PaintEventArgs) Handles pnlTemplatePreview.Paint
        DrawSelectedTemplatePreview(e.Graphics, pnlTemplatePreview.ClientRectangle)
    End Sub

    Private Sub btnApplyTemplate_Click(sender As Object, e As EventArgs) Handles btnApplyTemplate.Click
        If lstTemplates.SelectedItem Is Nothing Then Return

        Try
            EndGridEdit()
            Dim profile As SalesPrintProfile = BuildProfileFromControls()
            ApplyTemplateToProfile(profile, DirectCast(lstTemplates.SelectedItem, SalesPrintTemplate))
            CurrentProfile = profile
            BindProfile(CurrentProfile)
            MsgBox("تم تطبيق القالب المؤقت. اضغط حفظ إذا أردت تثبيته.", MsgBoxStyle.Information, "قوالب التقرير")
        Catch ex As Exception
            MsgBox("تعذر تطبيق القالب." & vbNewLine & ex.Message, MsgBoxStyle.Exclamation, "قوالب التقرير")
        End Try
    End Sub

    Private Sub ApplyTemplateToProfile(profile As SalesPrintProfile, template As SalesPrintTemplate)
        If profile Is Nothing OrElse template Is Nothing Then Return

        profile.PaperKind = template.PaperKind
        profile.Landscape = template.Landscape
        profile.MarginLeft = template.MarginLeft
        profile.MarginRight = template.MarginRight
        profile.MarginTop = template.MarginTop
        profile.MarginBottom = template.MarginBottom
        profile.TitleFontSize = template.TitleFontSize
        profile.SubTitleFontSize = template.SubTitleFontSize
        profile.InfoFontSize = template.InfoFontSize
        profile.HeaderFontSize = template.HeaderFontSize
        profile.RowFontSize = template.RowFontSize
        profile.TotalFontSize = template.TotalFontSize
        profile.FooterFontSize = template.FooterFontSize
        profile.HeaderBackColorArgb = template.HeaderBackColor.ToArgb()
        profile.HeaderForeColorArgb = template.HeaderForeColor.ToArgb()
        profile.RowBackColorArgb = template.RowBackColor.ToArgb()
        profile.AlternateRowBackColorArgb = template.AlternateRowBackColor.ToArgb()
        profile.TotalBackColorArgb = template.TotalBackColor.ToArgb()
        profile.TotalForeColorArgb = Color.Black.ToArgb()
        profile.TextForeColorArgb = Color.Black.ToArgb()
        profile.TitleForeColorArgb = Color.Black.ToArgb()
        profile.FooterForeColorArgb = Color.Gray.ToArgb()
        profile.BorderColorArgb = Color.LightGray.ToArgb()
        profile.UseAlternatingRows = template.UseAlternatingRows
        profile.DrawGridLines = template.DrawGridLines

        If template.PaperKind = "RECEIPT" Then
            SetComponentVisible(profile, "COLUMN", "IMNUM_CL", False)
            SetComponentVisible(profile, "COLUMN", "Barcode_CL", False)
            SetComponentVisible(profile, "COLUMN", "EX_Name_CL", True)
            SetComponentVisible(profile, "COLUMN", "IMUnit_CL", False)
            SetComponentVisible(profile, "COLUMN", "QTY_CL", True)
            SetComponentVisible(profile, "COLUMN", "Price_CL", True)
            SetComponentVisible(profile, "COLUMN", "IM_Discount_CL", False)
            SetComponentVisible(profile, "COLUMN", "Total_CL", True)
            SetComponentVisible(profile, "COLUMN", "Notes_CL", False)
            SetComponentVisible(profile, "COLUMN", "ST_Name_CL", False)
            SetComponentVisible(profile, "COLUMN", "D_Valid_CL", False)
            SetComponentWidth(profile, "EX_Name_CL", 150)
            SetComponentWidth(profile, "QTY_CL", 45)
            SetComponentWidth(profile, "Price_CL", 55)
            SetComponentWidth(profile, "Total_CL", 60)
        ElseIf template.PaperKind = "A6" Then
            SetComponentVisible(profile, "COLUMN", "IMNUM_CL", False)
            SetComponentVisible(profile, "COLUMN", "Barcode_CL", False)
            SetComponentVisible(profile, "COLUMN", "EX_Name_CL", True)
            SetComponentVisible(profile, "COLUMN", "IMUnit_CL", True)
            SetComponentVisible(profile, "COLUMN", "QTY_CL", True)
            SetComponentVisible(profile, "COLUMN", "Price_CL", True)
            SetComponentVisible(profile, "COLUMN", "IM_Discount_CL", False)
            SetComponentVisible(profile, "COLUMN", "Total_CL", True)
            SetComponentVisible(profile, "COLUMN", "Notes_CL", False)
        Else
            SetComponentVisible(profile, "COLUMN", "EX_Name_CL", True)
            SetComponentVisible(profile, "COLUMN", "IMUnit_CL", True)
            SetComponentVisible(profile, "COLUMN", "QTY_CL", True)
            SetComponentVisible(profile, "COLUMN", "Price_CL", True)
            SetComponentVisible(profile, "COLUMN", "Total_CL", True)
        End If
    End Sub

    Private Sub SetComponentVisible(profile As SalesPrintProfile, scope As String, code As String, visible As Boolean)
        Dim component As SalesPrintComponent = profile.Components.FirstOrDefault(Function(c) c.ComponentScope = scope AndAlso c.ComponentCode = code)
        If component IsNot Nothing Then component.IsVisible = visible
    End Sub

    Private Sub SetComponentWidth(profile As SalesPrintProfile, code As String, widthValue As Integer)
        Dim component As SalesPrintComponent = profile.Components.FirstOrDefault(Function(c) c.ComponentScope = "COLUMN" AndAlso c.ComponentCode = code)
        If component IsNot Nothing Then component.WidthValue = widthValue
    End Sub

    Private Sub DrawSelectedTemplatePreview(g As Graphics, bounds As Rectangle)
        g.Clear(Color.White)

        If lstTemplates.SelectedItem Is Nothing Then Return
        Dim template As SalesPrintTemplate = DirectCast(lstTemplates.SelectedItem, SalesPrintTemplate)

        Dim margin As Integer = 14
        Dim pageRect As New Rectangle(bounds.Left + margin, bounds.Top + margin, bounds.Width - (margin * 2), bounds.Height - (margin * 2))
        If pageRect.Width <= 20 OrElse pageRect.Height <= 20 Then Return

        If template.PaperKind = "RECEIPT" Then
            Dim receiptWidth As Integer = Math.Min(pageRect.Width, 115)
            pageRect = New Rectangle(bounds.Left + ((bounds.Width - receiptWidth) \ 2), pageRect.Top, receiptWidth, pageRect.Height)
        ElseIf template.PaperKind = "A5" Then
            pageRect.Inflate(-10, 0)
        ElseIf template.PaperKind = "A6" Then
            pageRect.Inflate(-22, 0)
        End If

        Using paperBrush As New SolidBrush(Color.White),
              borderPen As New Pen(Color.Silver),
              headerBrush As New SolidBrush(template.HeaderBackColor),
              totalBrush As New SolidBrush(template.TotalBackColor),
              rowBrush As New SolidBrush(template.AlternateRowBackColor),
              textPen As New Pen(Color.Gray)

            g.FillRectangle(paperBrush, pageRect)
            g.DrawRectangle(borderPen, pageRect)

            Dim y As Integer = pageRect.Top + 14
            g.DrawLine(textPen, pageRect.Left + 30, y, pageRect.Right - 30, y)
            y += 14
            g.DrawLine(textPen, pageRect.Left + 45, y, pageRect.Right - 45, y)
            y += 22

            Dim headerRect As New Rectangle(pageRect.Left + 12, y, pageRect.Width - 24, 18)
            g.FillRectangle(headerBrush, headerRect)
            y += 24

            For i As Integer = 0 To If(template.PaperKind = "RECEIPT", 5, 7)
                Dim rowRect As New Rectangle(pageRect.Left + 12, y, pageRect.Width - 24, 14)
                If i Mod 2 = 1 AndAlso template.UseAlternatingRows Then g.FillRectangle(rowBrush, rowRect)
                g.DrawRectangle(borderPen, rowRect)
                y += 14
            Next

            y += 12
            Dim totalRect As New Rectangle(pageRect.Left + 12, y, pageRect.Width - 24, 42)
            g.FillRectangle(totalBrush, totalRect)
            g.DrawRectangle(borderPen, totalRect)
        End Using

        Using titleFont As New Font("Segoe UI", 8.0!, FontStyle.Bold),
              fmt As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
            g.DrawString(template.TemplateName, titleFont, Brushes.DimGray, New Rectangle(bounds.Left, bounds.Bottom - 28, bounds.Width, 20), fmt)
        End Using
    End Sub

    Private Sub FormatStyleRows()
        If dgvStyles.Columns.Contains("StyleValue") = False Then Return

        For Each row As DataGridViewRow In dgvStyles.Rows
            If row.IsNewRow Then Continue For
            Dim styleType As String = GetCellString(row, "StyleType")
            Dim valueCell As DataGridViewCell = row.Cells("StyleValue")
            Dim buttonCell As DataGridViewCell = row.Cells("PickColor")
            Dim isColorRow As Boolean = (styleType = "Color")

            valueCell.Style.BackColor = Color.White
            valueCell.Style.ForeColor = Color.Black
            valueCell.ReadOnly = isColorRow
            buttonCell.ReadOnly = Not isColorRow
            buttonCell.Value = If(isColorRow, "اختيار", "")

            If isColorRow Then
                Try
                    Dim c As Color = ColorTranslator.FromHtml(GetCellString(row, "StyleValue"))
                    valueCell.Style.BackColor = c
                    valueCell.Style.ForeColor = If((CInt(c.R) + CInt(c.G) + CInt(c.B)) < 380, Color.White, Color.Black)
                    buttonCell.Value = "اختيار"
                Catch
                End Try
            End If
        Next
    End Sub

    Private Function ColorToHex(argb As Integer) As String
        Dim c As Color = Color.FromArgb(argb)
        Return ColorTranslator.ToHtml(Color.FromArgb(c.R, c.G, c.B))
    End Function

    Private Sub dgv_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvSections.DataError, dgvColumns.DataError, dgvStyles.DataError
        e.ThrowException = False
    End Sub

    Private Class SalesPrintTemplate
        Public Sub New(templateName As String, paperKind As String, landscape As Boolean, marginLeft As Integer, marginRight As Integer, marginTop As Integer, marginBottom As Integer, titleFontSize As Decimal, subTitleFontSize As Decimal, infoFontSize As Decimal, headerFontSize As Decimal, rowFontSize As Decimal, totalFontSize As Decimal, footerFontSize As Decimal, headerBackColor As Color, headerForeColor As Color, rowBackColor As Color, alternateRowBackColor As Color, totalBackColor As Color, useAlternatingRows As Boolean, drawGridLines As Boolean)
            Me.TemplateName = templateName
            Me.PaperKind = paperKind
            Me.Landscape = landscape
            Me.MarginLeft = marginLeft
            Me.MarginRight = marginRight
            Me.MarginTop = marginTop
            Me.MarginBottom = marginBottom
            Me.TitleFontSize = titleFontSize
            Me.SubTitleFontSize = subTitleFontSize
            Me.InfoFontSize = infoFontSize
            Me.HeaderFontSize = headerFontSize
            Me.RowFontSize = rowFontSize
            Me.TotalFontSize = totalFontSize
            Me.FooterFontSize = footerFontSize
            Me.HeaderBackColor = headerBackColor
            Me.HeaderForeColor = headerForeColor
            Me.RowBackColor = rowBackColor
            Me.AlternateRowBackColor = alternateRowBackColor
            Me.TotalBackColor = totalBackColor
            Me.UseAlternatingRows = useAlternatingRows
            Me.DrawGridLines = drawGridLines
        End Sub

        Public Property TemplateName As String
        Public Property PaperKind As String
        Public Property Landscape As Boolean
        Public Property MarginLeft As Integer
        Public Property MarginRight As Integer
        Public Property MarginTop As Integer
        Public Property MarginBottom As Integer
        Public Property TitleFontSize As Decimal
        Public Property SubTitleFontSize As Decimal
        Public Property InfoFontSize As Decimal
        Public Property HeaderFontSize As Decimal
        Public Property RowFontSize As Decimal
        Public Property TotalFontSize As Decimal
        Public Property FooterFontSize As Decimal
        Public Property HeaderBackColor As Color
        Public Property HeaderForeColor As Color
        Public Property RowBackColor As Color
        Public Property AlternateRowBackColor As Color
        Public Property TotalBackColor As Color
        Public Property UseAlternatingRows As Boolean
        Public Property DrawGridLines As Boolean
    End Class

End Class
