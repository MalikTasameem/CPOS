Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing

Public Class UcGridColumnsSelector

    Private _grid As DataGridView
    Private _excludedColumns As New List(Of String)
    Private _settingsFilePath As String = ""
    Private _popupPanel As Panel
    Public _checkedList As CheckedListBox
    Private _btnCheckAll As Button
    Private _btnUncheckAll As Button
    Private _btnRefresh As Button
    Private _isLoading As Boolean = False
    Private _popupVisible As Boolean = False

    Public Property SettingsFolder As String = Path.Combine(Application.StartupPath, "GridColumnsSettings") 'Application.StartupPath & "\Settings Files\"
    Public Property PopupWidth As Integer = 260
    Public Property PopupMaxHeight As Integer = 320
    Public Property PopupMinHeight As Integer = 120

    Private Class ColumnItem
        Public Property ColumnName As String
        Public Property HeaderText As String

        Public Sub New(colName As String, header As String)
            ColumnName = colName
            HeaderText = header
        End Sub

        Public Overrides Function ToString() As String
            Return HeaderText
        End Function
    End Class



    '------------------------------------------------------------------------------------------------------------------
    '-------------------------------------------------------------------
    ' إعادة تعريف خاصية Font لتطبيقها على كل الأدوات الداخلية
    '-------------------------------------------------------------------
    Public Overrides Property Font As Font
        Get
            Return MyBase.Font
        End Get
        Set(value As Font)
            MyBase.Font = value
            ApplyFontToAllControls(Me, value)

            ' ضبط ارتفاع صفوف الـ DataGridView تلقائيًا حسب حجم الخط
            'AdjustGridRowHeights()
        End Set
    End Property


    '-------------------------------------------------------------------
    ' دالة مساعدة تطبق الخط على كل العناصر الداخلية
    '-------------------------------------------------------------------
    Private Sub ApplyFontToAllControls(parent As Control, font As Font)
        For Each ctrl As Control In parent.Controls
            ctrl.Font = font
            ' في حال وجود أدوات داخل أدوات أخرى
            If ctrl.HasChildren Then
                ApplyFontToAllControls(ctrl, font)
            End If
        Next
    End Sub


    '-------------------------------------------------------------------
    ' دالة لضبط ارتفاع صفوف الجريد بناءً على حجم الخط
    '-------------------------------------------------------------------
    'Private Sub AdjustGridRowHeights()
    '    Try
    '        If QuickView Is Nothing OrElse QuickView.Rows.Count = 0 Then Exit Sub

    '        ' الارتفاع الافتراضي للصف = حجم الخط × 2 (لجعل الشكل متناسق)
    '        Dim newRowHeight As Integer = CInt(Me.Font.Size * 2)

    '        For Each row As DataGridViewRow In QuickView.Rows
    '            row.Height = newRowHeight
    '        Next

    '        QuickView.RowTemplate.Height = newRowHeight
    '        QuickView.Refresh()
    '    Catch ex As Exception
    '        ' تجاهل أي خطأ بسيط في حال لم يكن الجريد جاهز بعد
    '    End Try
    'End Sub

    '-------------------------------------------------------------------------------------


    Public Sub BindGrid(grid As DataGridView,
                        Optional excludedColumns As List(Of String) = Nothing,
                        Optional customFileName As String = "")

        'If IsInDesignMode() Then Exit Sub

        SettingsFolder = Path.Combine(Application.StartupPath, "GridColumnsSettings")

        _grid = grid

        If excludedColumns IsNot Nothing Then
            _excludedColumns = excludedColumns
        Else
            _excludedColumns = New List(Of String)
        End If

        If Not Directory.Exists(SettingsFolder) Then
            Directory.CreateDirectory(SettingsFolder)
        End If

        If String.IsNullOrWhiteSpace(customFileName) Then
            _settingsFilePath = Path.Combine(SettingsFolder, GetDefaultFileName())
        Else
            _settingsFilePath = Path.Combine(SettingsFolder, customFileName & ".txt")
        End If

        CreatePopupIfNeeded()
        LoadColumnsToList()
        LoadSettingsFromFile()

        HidePopup()
    End Sub

    Private Function GetDefaultFileName() As String
        If _grid Is Nothing Then Return "grid_columns.txt"

        Dim formName As String = "Form"
        If _grid.FindForm IsNot Nothing Then
            formName = _grid.FindForm.Name
        End If

        Return formName & "_" & _grid.Name & "_columns.txt"
    End Function

    Private Sub CreatePopupIfNeeded()
        If _popupPanel IsNot Nothing Then Exit Sub

        Dim frm As Form = Me.FindForm()
        If frm Is Nothing Then Exit Sub

        _popupPanel = New Panel With {
            .Visible = False,
            .BorderStyle = BorderStyle.FixedSingle,
            .BackColor = Color.White,
            .Width = PopupWidth,
            .Height = PopupMinHeight
        }

        Dim topBar As New Panel With {
            .Dock = DockStyle.Top,
            .Height = 38,
            .BackColor = Color.WhiteSmoke
        }

        _btnRefresh = New Button With {
            .Text = "تحديث",
            .Width = 55,
            .Height = 26,
            .Left = 5,
            .Top = 6
        }

        _btnUncheckAll = New Button With {
            .Text = "إلغاء",
            .Width = 55,
            .Height = 26,
            .Left = 65,
            .Top = 6
        }

        _btnCheckAll = New Button With {
            .Text = "الكل",
            .Width = 55,
            .Height = 26,
            .Left = 125,
            .Top = 6
        }

        _checkedList = New CheckedListBox With {
            .Dock = DockStyle.Fill,
            .CheckOnClick = True,
            .RightToLeft = RightToLeft.Yes,
            .Font = New Font("Tahoma", 9.0!)
        }

        topBar.Controls.Add(_btnRefresh)
        topBar.Controls.Add(_btnUncheckAll)
        topBar.Controls.Add(_btnCheckAll)

        _popupPanel.Controls.Add(_checkedList)
        _popupPanel.Controls.Add(topBar)

        frm.Controls.Add(_popupPanel)
        _popupPanel.BringToFront()

        AddHandler _btnRefresh.Click, AddressOf btnRefresh_Click
        AddHandler _btnCheckAll.Click, AddressOf btnCheckAll_Click
        AddHandler _btnUncheckAll.Click, AddressOf btnUncheckAll_Click
        AddHandler _checkedList.ItemCheck, AddressOf checkedList_ItemCheck
        AddHandler frm.Click, AddressOf ParentForm_ClickHide
    End Sub

    Private Sub LoadColumnsToList()
        If _grid Is Nothing OrElse _checkedList Is Nothing Then Exit Sub

        _isLoading = True
        _checkedList.Items.Clear()

        For Each col As DataGridViewColumn In _grid.Columns
            If Not _excludedColumns.Contains(col.Name) Then
                Dim textToShow As String = If(String.IsNullOrWhiteSpace(col.HeaderText), col.Name, col.HeaderText)
                _checkedList.Items.Add(New ColumnItem(col.Name, textToShow), col.Visible)
            End If
        Next

        AdjustPopupHeight()
        _isLoading = False
    End Sub

    Private Sub AdjustPopupHeight()
        If _popupPanel Is Nothing OrElse _checkedList Is Nothing Then Exit Sub

        Dim itemHeight As Integer = 18
        Dim topArea As Integer = 38
        Dim calc As Integer = topArea + (_checkedList.Items.Count * itemHeight) + 10

        If calc < PopupMinHeight Then calc = PopupMinHeight
        If calc > PopupMaxHeight Then calc = PopupMaxHeight

        _popupPanel.Width = PopupWidth
        _popupPanel.Height = calc
    End Sub

    Private Sub ApplyColumnVisibility(columnName As String, isVisible As Boolean)
        If _grid Is Nothing Then Exit Sub
        If _grid.Columns.Contains(columnName) Then
            _grid.Columns(columnName).Visible = isVisible
        End If
    End Sub

    Private Sub SaveSettingsToFile()
        Try
            If String.IsNullOrWhiteSpace(_settingsFilePath) OrElse _checkedList Is Nothing Then Exit Sub

            Dim sb As New StringBuilder()

            For i As Integer = 0 To _checkedList.Items.Count - 1
                Dim item As ColumnItem = TryCast(_checkedList.Items(i), ColumnItem)
                If item IsNot Nothing Then
                    sb.AppendLine(item.ColumnName & "=" & _checkedList.GetItemChecked(i).ToString())
                End If
            Next

            File.WriteAllText(_settingsFilePath, sb.ToString(), Encoding.UTF8)

        Catch ex As Exception
            MessageBox.Show("خطأ أثناء حفظ الإعدادات: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadSettingsFromFile()
        Try
            If _grid Is Nothing OrElse _checkedList Is Nothing Then Exit Sub
            If String.IsNullOrWhiteSpace(_settingsFilePath) Then Exit Sub
            If Not File.Exists(_settingsFilePath) Then Exit Sub

            _isLoading = True

            Dim lines() As String = File.ReadAllLines(_settingsFilePath, Encoding.UTF8)
            Dim dic As New Dictionary(Of String, Boolean)(StringComparer.OrdinalIgnoreCase)

            For Each line As String In lines
                If String.IsNullOrWhiteSpace(line) Then Continue For
                Dim parts() As String = line.Split("="c)
                If parts.Length = 2 Then
                    Dim key As String = parts(0).Trim()
                    Dim val As Boolean = False
                    Boolean.TryParse(parts(1).Trim(), val)
                    If Not dic.ContainsKey(key) Then dic.Add(key, val)
                End If
            Next

            For i As Integer = 0 To _checkedList.Items.Count - 1
                Dim item As ColumnItem = TryCast(_checkedList.Items(i), ColumnItem)
                If item IsNot Nothing AndAlso dic.ContainsKey(item.ColumnName) Then
                    _checkedList.SetItemChecked(i, dic(item.ColumnName))
                    ApplyColumnVisibility(item.ColumnName, dic(item.ColumnName))
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("خطأ أثناء تحميل الإعدادات: " & ex.Message)
        Finally
            _isLoading = False
        End Try
    End Sub

    Public Sub RefreshColumns()
        LoadColumnsToList()
        LoadSettingsFromFile()
    End Sub

    Private Sub ShowPopup()
        If _popupPanel Is Nothing Then Exit Sub

        Dim frm As Form = Me.FindForm()
        If frm Is Nothing Then Exit Sub

        Dim p As Point = Me.Parent.PointToScreen(Me.Location)
        Dim formPoint As Point = frm.PointToClient(p)

        _popupPanel.Left = formPoint.X
        _popupPanel.Top = formPoint.Y + Me.Height + 2

        If _popupPanel.Left + _popupPanel.Width > frm.ClientSize.Width Then
            _popupPanel.Left = frm.ClientSize.Width - _popupPanel.Width - 5
        End If

        If _popupPanel.Left < 0 Then _popupPanel.Left = 5

        If _popupPanel.Top + _popupPanel.Height > frm.ClientSize.Height Then
            _popupPanel.Top = formPoint.Y - _popupPanel.Height - 2
        End If

        If _popupPanel.Top < 0 Then _popupPanel.Top = 5

        _popupPanel.Visible = True
        _popupPanel.BringToFront()
        btnToggle.Text = "▲"
        _popupVisible = True
    End Sub

    Private Sub HidePopup()
        If _popupPanel Is Nothing Then Exit Sub
        _popupPanel.Visible = False
        btnToggle.Text = "▼"
        _popupVisible = False
    End Sub

    Private Sub TogglePopup()
        If _popupVisible Then
            HidePopup()
        Else
            ShowPopup()
        End If
    End Sub

    Private Sub checkedList_ItemCheck(sender As Object, e As ItemCheckEventArgs)
        If _isLoading Then Exit Sub
        If e.Index < 0 Then Exit Sub

        Dim item As ColumnItem = TryCast(_checkedList.Items(e.Index), ColumnItem)
        If item Is Nothing Then Exit Sub

        ApplyColumnVisibility(item.ColumnName, e.NewValue = CheckState.Checked)

        Me.BeginInvoke(New MethodInvoker(Sub()
                                             SaveSettingsToFile()
                                         End Sub))
    End Sub

    Private Sub btnCheckAll_Click(sender As Object, e As EventArgs)
        If _checkedList Is Nothing Then Exit Sub

        _isLoading = True
        For i As Integer = 0 To _checkedList.Items.Count - 1
            _checkedList.SetItemChecked(i, True)
            Dim item As ColumnItem = TryCast(_checkedList.Items(i), ColumnItem)
            If item IsNot Nothing Then ApplyColumnVisibility(item.ColumnName, True)
        Next
        _isLoading = False

        SaveSettingsToFile()
    End Sub

    Private Sub btnUncheckAll_Click(sender As Object, e As EventArgs)
        If _checkedList Is Nothing Then Exit Sub

        _isLoading = True
        For i As Integer = 0 To _checkedList.Items.Count - 1
            _checkedList.SetItemChecked(i, False)
            Dim item As ColumnItem = TryCast(_checkedList.Items(i), ColumnItem)
            If item IsNot Nothing Then ApplyColumnVisibility(item.ColumnName, False)
        Next
        _isLoading = False

        SaveSettingsToFile()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        RefreshColumns()
    End Sub

    Private Sub btnToggle_Click(sender As Object, e As EventArgs) Handles btnToggle.Click
        TogglePopup()
    End Sub

    Private Sub ParentForm_ClickHide(sender As Object, e As EventArgs)
        If _popupVisible Then
            HidePopup()
        End If
    End Sub

End Class






'-------------------------------------------------------------------------------------
'Imports System.IO
'Imports System.Text
'Imports System.Windows.Forms

'Public Class UcGridColumnsSelector

'    Private _grid As DataGridView
'    Private _isLoading As Boolean = False
'    Private _excludedColumns As New List(Of String)
'    Private _settingsFilePath As String = ""

'    Private _expandedHeight As Integer = 260
'    Private _collapsedHeight As Integer = 42
'    Private _isExpanded As Boolean = True

'    Public Property ExpandedHeight As Integer
'        Get
'            Return _expandedHeight
'        End Get
'        Set(value As Integer)
'            If value < 100 Then value = 100
'            _expandedHeight = value
'            If _isExpanded Then
'                Me.Height = _expandedHeight
'            End If
'        End Set
'    End Property

'    Public Property CollapsedHeight As Integer
'        Get
'            Return _collapsedHeight
'        End Get
'        Set(value As Integer)
'            If value < 35 Then value = 35
'            _collapsedHeight = value
'            If Not _isExpanded Then
'                Me.Height = _collapsedHeight
'            End If
'        End Set
'    End Property

'    Public Property IsExpanded As Boolean
'        Get
'            Return _isExpanded
'        End Get
'        Set(value As Boolean)
'            _isExpanded = value
'            ApplyExpandCollapse()
'        End Set
'    End Property

'    Private Sub ApplyExpandCollapse()
'        pnlBody.Visible = _isExpanded

'        If _isExpanded Then
'            Me.Height = _expandedHeight
'            btnToggle.Text = "▲"
'        Else
'            Me.Height = _collapsedHeight
'            btnToggle.Text = "▼"
'        End If
'    End Sub

'    Private Class ColumnItem
'        Public Property ColumnName As String
'        Public Property HeaderText As String

'        Public Sub New(colName As String, header As String)
'            ColumnName = colName
'            HeaderText = header
'        End Sub

'        Public Overrides Function ToString() As String
'            Return HeaderText
'        End Function
'    End Class

'    Public Property SettingsFolder As String = Path.Combine(Application.StartupPath, "GridColumnsSettings")

'    Public Sub BindGrid(grid As DataGridView,
'                        Optional excludedColumns As List(Of String) = Nothing,
'                        Optional customFileName As String = "")
'        _grid = grid

'        If excludedColumns IsNot Nothing Then
'            _excludedColumns = excludedColumns
'        Else
'            _excludedColumns = New List(Of String)
'        End If

'        If Not Directory.Exists(SettingsFolder) Then
'            Directory.CreateDirectory(SettingsFolder)
'        End If

'        If String.IsNullOrWhiteSpace(customFileName) Then
'            _settingsFilePath = Path.Combine(SettingsFolder, GetDefaultFileName())
'        Else
'            _settingsFilePath = Path.Combine(SettingsFolder, customFileName & ".txt")
'        End If

'        LoadColumnsToList()
'        LoadSettingsFromFile()
'    End Sub

'    Private Function GetDefaultFileName() As String
'        If _grid Is Nothing Then Return "grid_columns.txt"

'        Dim formName As String = "Form"
'        If _grid.FindForm IsNot Nothing Then
'            formName = _grid.FindForm.Name
'        End If

'        Return formName & "_" & _grid.Name & "_columns.txt"
'    End Function

'    Private Sub LoadColumnsToList()
'        If _grid Is Nothing Then Exit Sub

'        _isLoading = True
'        clbColumns.Items.Clear()

'        For Each col As DataGridViewColumn In _grid.Columns
'            If Not _excludedColumns.Contains(col.Name) Then
'                Dim textToShow As String = If(String.IsNullOrWhiteSpace(col.HeaderText), col.Name, col.HeaderText)
'                clbColumns.Items.Add(New ColumnItem(col.Name, textToShow), col.Visible)
'            End If
'        Next

'        _isLoading = False
'    End Sub

'    Private Sub ApplyColumnVisibility(columnName As String, isVisible As Boolean)
'        If _grid Is Nothing Then Exit Sub
'        If _grid.Columns.Contains(columnName) Then
'            _grid.Columns(columnName).Visible = isVisible
'        End If
'    End Sub

'    Private Sub SaveSettingsToFile()
'        Try
'            If String.IsNullOrWhiteSpace(_settingsFilePath) Then Exit Sub

'            Dim sb As New StringBuilder()

'            For i As Integer = 0 To clbColumns.Items.Count - 1
'                Dim item As ColumnItem = TryCast(clbColumns.Items(i), ColumnItem)
'                If item IsNot Nothing Then
'                    Dim isChecked As Boolean = clbColumns.GetItemChecked(i)
'                    sb.AppendLine(item.ColumnName & "=" & isChecked.ToString())
'                End If
'            Next

'            File.WriteAllText(_settingsFilePath, sb.ToString(), Encoding.UTF8)

'        Catch ex As Exception
'            MessageBox.Show("خطأ أثناء حفظ إعدادات الأعمدة: " & ex.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
'        End Try
'    End Sub

'    Private Sub LoadSettingsFromFile()
'        Try
'            If _grid Is Nothing Then Exit Sub
'            If String.IsNullOrWhiteSpace(_settingsFilePath) Then Exit Sub
'            If Not File.Exists(_settingsFilePath) Then Exit Sub

'            _isLoading = True

'            Dim lines() As String = File.ReadAllLines(_settingsFilePath, Encoding.UTF8)
'            Dim dic As New Dictionary(Of String, Boolean)(StringComparer.OrdinalIgnoreCase)

'            For Each line As String In lines
'                If String.IsNullOrWhiteSpace(line) Then Continue For

'                Dim parts() As String = line.Split("="c)
'                If parts.Length = 2 Then
'                    Dim colName As String = parts(0).Trim()
'                    Dim val As Boolean = False
'                    Boolean.TryParse(parts(1).Trim(), val)
'                    If Not dic.ContainsKey(colName) Then
'                        dic.Add(colName, val)
'                    End If
'                End If
'            Next

'            For i As Integer = 0 To clbColumns.Items.Count - 1
'                Dim item As ColumnItem = TryCast(clbColumns.Items(i), ColumnItem)
'                If item IsNot Nothing Then
'                    If dic.ContainsKey(item.ColumnName) Then
'                        clbColumns.SetItemChecked(i, dic(item.ColumnName))
'                        ApplyColumnVisibility(item.ColumnName, dic(item.ColumnName))
'                    End If
'                End If
'            Next

'        Catch ex As Exception
'            MessageBox.Show("خطأ أثناء تحميل إعدادات الأعمدة: " & ex.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
'        Finally
'            _isLoading = False
'        End Try
'    End Sub

'    Public Sub RefreshColumns()
'        LoadColumnsToList()
'        LoadSettingsFromFile()
'    End Sub

'    Public Sub SaveNow()
'        SaveSettingsToFile()
'    End Sub

'    Public Sub ResetToCurrentGridState()
'        LoadColumnsToList()
'        SaveSettingsToFile()
'    End Sub

'    Public Sub CheckAllColumns()
'        If _grid Is Nothing Then Exit Sub

'        _isLoading = True

'        For i As Integer = 0 To clbColumns.Items.Count - 1
'            clbColumns.SetItemChecked(i, True)

'            Dim item As ColumnItem = TryCast(clbColumns.Items(i), ColumnItem)
'            If item IsNot Nothing Then
'                ApplyColumnVisibility(item.ColumnName, True)
'            End If
'        Next

'        _isLoading = False
'        SaveSettingsToFile()
'    End Sub

'    Public Sub UncheckAllColumns()
'        If _grid Is Nothing Then Exit Sub

'        _isLoading = True

'        For i As Integer = 0 To clbColumns.Items.Count - 1
'            clbColumns.SetItemChecked(i, False)

'            Dim item As ColumnItem = TryCast(clbColumns.Items(i), ColumnItem)
'            If item IsNot Nothing Then
'                ApplyColumnVisibility(item.ColumnName, False)
'            End If
'        Next

'        _isLoading = False
'        SaveSettingsToFile()
'    End Sub

'    Private Sub clbColumns_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbColumns.ItemCheck
'        If _isLoading Then Exit Sub
'        If _grid Is Nothing Then Exit Sub
'        If e.Index < 0 Then Exit Sub

'        Dim item As ColumnItem = TryCast(clbColumns.Items(e.Index), ColumnItem)
'        If item Is Nothing Then Exit Sub

'        ApplyColumnVisibility(item.ColumnName, (e.NewValue = CheckState.Checked))

'        Me.BeginInvoke(New MethodInvoker(Sub()
'                                             SaveSettingsToFile()
'                                         End Sub))
'    End Sub

'    Private Sub btnCheckAll_Click(sender As Object, e As EventArgs) Handles btnCheckAll.Click
'        CheckAllColumns()
'    End Sub

'    Private Sub btnUncheckAll_Click(sender As Object, e As EventArgs) Handles btnUncheckAll.Click
'        UncheckAllColumns()
'    End Sub

'    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
'        RefreshColumns()
'    End Sub



'    Private Sub btnToggle_Click(sender As Object, e As EventArgs) Handles btnToggle.Click
'        _isExpanded = Not _isExpanded
'        ApplyExpandCollapse()

'    End Sub
'End Class