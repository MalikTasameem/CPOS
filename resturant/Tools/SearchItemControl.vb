Public Class SearchItemControl
    Inherits UserControl

    ' المكونات
    Public WithEvents txtSearch As New TextBox()
    Private WithEvents cmbSearchBy As New ComboBox()
    Private WithEvents dgvResults As New DataGridView()
    Public WithEvents btnClear As New Button()
    Private WithEvents btnRefresh As New Button()
    Private WithEvents btnToggleView As New Button()

    ' DataTable مصدر البيانات
    Private _itemsTable As DataTable
    Private _itemsTable_Barcode As DataTable




    Public Property ItemsTable As DataTable
        Get
            Return _itemsTable
        End Get
        Set(value As DataTable)
            _itemsTable = value
            dgvResults.Visible = False
            Me.Height = txtSearch.Height + 10
        End Set
    End Property
    Public Property itemsTable_Barcode As DataTable
        Get
            Return _itemsTable_Barcode
        End Get
        Set(value As DataTable)
            _itemsTable_Barcode = value
            dgvResults.Visible = False
            Me.Height = txtSearch.Height + 10
        End Set
    End Property



    ' خاصية الهامش بين مربع البحث و الـ GridView
    Private _marginBetweenSearchAndGrid As Integer = 15
    Public Property MarginBetweenSearchAndGrid As Integer
        Get
            Return _marginBetweenSearchAndGrid
        End Get
        Set(value As Integer)
            _marginBetweenSearchAndGrid = value
        End Set
    End Property

    ' إضافة خاصية الارتفاع الأقصى للجريد
    Private _maxGridHeight As Integer = 300
    Public Property MaxGridHeight As Integer
        Get
            Return _maxGridHeight
        End Get
        Set(value As Integer)
            _maxGridHeight = value
        End Set
    End Property


    Private _defaultSearchField As String = "إسم الصنف"
    Public Property DefaultSearchField As String
        Get
            Return _defaultSearchField
        End Get
        Set(value As String)
            If cmbSearchBy.Items.Contains(value) Then
                _defaultSearchField = value
                cmbSearchBy.SelectedItem = value
            End If
        End Set
    End Property

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        AdjustLayout()
    End Sub

    Private Sub AdjustGridColumns()
        If dgvResults.Columns.Contains("ItemID") Then
            dgvResults.Columns("ItemID").Visible = False ' مخفي لكنه موجود
        End If

        If dgvResults.Columns.Contains("isValid") Then
            dgvResults.Columns("isValid").Visible = False ' مخفي لكنه موجود
        End If

        If dgvResults.Columns.Contains("IM_NUMBER") Then
            dgvResults.Columns("IM_NUMBER").Visible = True
            dgvResults.Columns("IM_NUMBER").Width = CInt(dgvResults.Width * 0.2)
        End If

        If dgvResults.Columns.Contains("Barcode") Then
            dgvResults.Columns("Barcode").Visible = True
            dgvResults.Columns("Barcode").Width = CInt(dgvResults.Width * 0.25)
        End If

        If dgvResults.Columns.Contains("ItemName") Then
            dgvResults.Columns("ItemName").Visible = True
            dgvResults.Columns("ItemName").Width = CInt(dgvResults.Width * 0.55)
        End If
    End Sub



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
            AdjustGridRowHeights()
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
    Private Sub AdjustGridRowHeights()
        Try
            If dgvResults Is Nothing OrElse dgvResults.Rows.Count = 0 Then Exit Sub

            ' الارتفاع الافتراضي للصف = حجم الخط × 2 (لجعل الشكل متناسق)
            Dim newRowHeight As Integer = CInt(Me.Font.Size * 2)

            For Each row As DataGridViewRow In dgvResults.Rows
                row.Height = newRowHeight
            Next

            dgvResults.RowTemplate.Height = newRowHeight
            dgvResults.Refresh()
        Catch ex As Exception
            ' تجاهل أي خطأ بسيط في حال لم يكن الجريد جاهز بعد
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------------------


    Private Sub AdjustLayout()
        ' حدد الهامش من اليمين
        Dim rightMargin As Integer = 10
        Dim currentX As Integer = Me.Width - rightMargin

        ' ترتيب الأزرار من اليمين لليسار
        btnRefresh.Height = txtSearch.Height
        btnRefresh.Location = New Point(currentX - btnRefresh.Width, txtSearch.Top)
        currentX -= btnRefresh.Width + 1


        btnClear.Height = txtSearch.Height
        btnClear.Location = New Point(currentX - btnClear.Width, txtSearch.Top)
        currentX -= btnClear.Width + 1


        btnToggleView.Height = txtSearch.Height
        btnToggleView.Location = New Point(currentX - btnToggleView.Width, txtSearch.Top)
        currentX -= btnToggleView.Width + 1

        ' مربع البحث يتمدد بالعرض
        txtSearch.Height = txtSearch.Height
        txtSearch.Width = currentX - cmbSearchBy.Width - 10
        txtSearch.Location = New Point(currentX - txtSearch.Width, txtSearch.Top)

        ' ComboBox في أقصى اليمين
        cmbSearchBy.Location = New Point(txtSearch.Left - cmbSearchBy.Width - 1, txtSearch.Top)

        ' ضبط Anchor
        cmbSearchBy.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnClear.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnRefresh.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnToggleView.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        dgvResults.Anchor = AnchorStyles.Top Or AnchorStyles.Right Or AnchorStyles.Left Or AnchorStyles.Bottom
    End Sub



    ' حالة عرض كل الأصناف
    Private isShowingAll As Boolean = False

    ' الحدث: عند اختيار صنف
    'Public Event ItemSelected(ByVal itemId As Integer)

    Public Event ItemSelected(ByVal itemId As Integer, ByVal isValid As String)

    ' ToolTip
    Private tt As New ToolTip()


    Public Sub New()
        Me.Width = 500
        Me.Height = 40
        Me.BackColor = Color.WhiteSmoke
        Me.Padding = New Padding(0)
        Me.Margin = New Padding(0)

        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Me.BackColor = Color.Transparent

        cmbSearchBy.Items.AddRange({"رقم الصنف", "إسم الصنف", "الباركود"})
        cmbSearchBy.SelectedItem = _defaultSearchField
        cmbSearchBy.DropDownStyle = ComboBoxStyle.DropDownList
        'cmbSearchBy.Location = New Point(0, 5)
        cmbSearchBy.Location = New Point(5, 1)
        cmbSearchBy.Width = 100
        cmbSearchBy.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        Me.Controls.Add(cmbSearchBy)

        'txtSearch.Location = New Point(110, 5)
        txtSearch.Location = New Point(cmbSearchBy.Right + 1, 1)
        txtSearch.Width = 160
        txtSearch.BorderStyle = BorderStyle.FixedSingle
        txtSearch.Size = New Size(230, 20)
        SendMessage(txtSearch.Handle, &H1501, 0, "إبحث عن ( " & cmbSearchBy.Text & " )  ")
        txtSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.Controls.Add(txtSearch)

        btnToggleView.Text = "📋"
        'btnToggleView.Width = 30
        'btnToggleView.Height = 24
        'btnToggleView.Location = New Point(345, 5)
        btnToggleView.Size = New Size(30, 22)
        btnToggleView.Location = New Point(btnRefresh.Right + 1, 1)
        btnToggleView.FlatStyle = FlatStyle.Flat
        btnToggleView.BackColor = Color.LightGray
        btnToggleView.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.Controls.Add(btnToggleView)


        btnClear.Text = "✖"
        'btnClear.Width = 30
        'btnClear.Height = 24
        btnClear.Size = New Size(30, 22)
        btnClear.Location = New Point(txtSearch.Right + 1, 1)
        'btnClear.Location = New Point(275, 5)
        btnClear.FlatStyle = FlatStyle.Flat
        btnClear.BackColor = Color.LightGray
        btnClear.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnClear.ForeColor = Color.DarkRed
        Me.Controls.Add(btnClear)


        btnRefresh.Text = "↻"
        'btnRefresh.Width = 30
        'btnRefresh.Height = 24
        'btnRefresh.Location = New Point(310, 5)
        btnRefresh.Size = New Size(30, 22)
        btnRefresh.Location = New Point(btnClear.Right + 1, 1)
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.BackColor = Color.LightGray
        btnRefresh.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnRefresh.ForeColor = Color.Blue
        Me.Controls.Add(btnRefresh)



        ' إعداد ToolTip
        tt.SetToolTip(btnClear, "مسح حقل البحث")
        tt.SetToolTip(btnRefresh, "تحديث البيانات من المصدر")
        tt.SetToolTip(btnToggleView, "عرض أو إخفاء كافة الأصناف")

        dgvResults.Location = New Point(0, txtSearch.Bottom + 5)
        dgvResults.Width = Me.Width
        dgvResults.Height = 150
        dgvResults.ReadOnly = True
        dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvResults.MultiSelect = False
        dgvResults.AllowUserToAddRows = False
        dgvResults.AllowUserToDeleteRows = False
        dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvResults.Visible = False
        dgvResults.BackgroundColor = Color.White
        dgvResults.RowHeadersVisible = False
        dgvResults.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        dgvResults.RowTemplate.Resizable = DataGridViewTriState.[False]
        dgvResults.RowTemplate.Height = 30
        dgvResults.Anchor = AnchorStyles.Top Or AnchorStyles.Right Or AnchorStyles.Left Or AnchorStyles.Bottom
        dgvResults.ColumnHeadersVisible = False
        Me.Controls.Add(dgvResults)

        AdjustLayout()

        Me.BringToFront()


    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        txtSearch.BackColor = SystemColors.Window
        ApplyFilter()
    End Sub

    Private Sub cmbSearchBy_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSearchBy.SelectedIndexChanged
        ApplyFilter()
        SendMessage(txtSearch.Handle, &H1501, 0, "إبحث عن ( " & cmbSearchBy.Text & " )  ")
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Return Then
            If dgvResults.Visible AndAlso dgvResults.Rows.Count > 0 Then
                SelectCurrentRow()
                e.SuppressKeyPress = True
            Else

                Select Case cmbSearchBy.SelectedItem.ToString()
                    Case "رقم الصنف"
                        MsgBox(" لم يتم التعرف على الإدخال ", MsgBoxStyle.Critical, cmbSearchBy.SelectedItem.ToString())
                        txtSearch.Clear()
                    Case "الباركود"
                        MsgBox(" لم يتم التعرف على الإدخال ", MsgBoxStyle.Critical, cmbSearchBy.SelectedItem.ToString())
                        txtSearch.Clear()
                End Select

            End If
        ElseIf e.KeyCode = Keys.Down Then
            If dgvResults.Visible AndAlso dgvResults.Rows.Count > 0 Then
                Dim currentIndex = If(dgvResults.CurrentRow?.Index, -1)
                If currentIndex < dgvResults.Rows.Count - 1 Then
                    dgvResults.CurrentCell = dgvResults.Rows(currentIndex + 1).Cells(1)
                End If
                e.SuppressKeyPress = True
            End If
        ElseIf e.KeyCode = Keys.Up Then
            If dgvResults.Visible AndAlso dgvResults.Rows.Count > 0 Then
                Dim currentIndex = If(dgvResults.CurrentRow?.Index, 1)
                If currentIndex > 0 Then
                    dgvResults.CurrentCell = dgvResults.Rows(currentIndex - 1).Cells(1)
                End If
                e.SuppressKeyPress = True
            End If
        End If
    End Sub

    Private Sub SelectCurrentRow()



        If dgvResults.CurrentRow IsNot Nothing Then
            Dim row As DataGridViewRow = dgvResults.CurrentRow
            Dim itemId As Integer = CInt(row.Cells("ItemID").Value)
            Dim itemName As String = row.Cells("ItemName").Value.ToString()
            Dim isValid As String = row.Cells("isValid").Value.ToString()


            txtSearch.Text = itemName
            txtSearch.BackColor = SystemColors.Info

            'RaiseEvent ItemSelected(itemId)
            RaiseEvent ItemSelected(itemId, isValid)

            dgvResults.Visible = False
            Me.Height = txtSearch.Height + 10
        End If
    End Sub

    Private Sub ApplyFilter()
        If _itemsTable Is Nothing Then
            dgvResults.Visible = False
            Me.Height = txtSearch.Height + 10
            Exit Sub
        End If

        Dim dtFiltered As DataTable = _itemsTable.Clone()
        Dim searchField As String = ""

        Select Case cmbSearchBy.SelectedItem.ToString()
            Case "رقم الصنف"
                searchField = "IM_NUMBER"
                dtFiltered = _itemsTable.Clone()
            Case "إسم الصنف"
                searchField = "ItemName"
                dtFiltered = _itemsTable.Clone()
            Case "الباركود"
                searchField = "Barcode"
                dtFiltered = _itemsTable_Barcode.Clone()
        End Select


        Dim filterText As String = EscapeLikeValue(txtSearch.Text.Trim())
        If filterText = "" Then
            dgvResults.Visible = False
            dgvResults.DataSource = Nothing
            Me.Height = txtSearch.Height + 10
        Else

            Dim rows
            If searchField = "ItemName" Then 'Or searchField = "IM_NUMBER"
                rows = _itemsTable.Select(IM_Serach_Filter(filterText, searchField))
            Else
                rows = _itemsTable_Barcode.Select($"{searchField} = '{filterText}'")
            End If


            For Each row As DataRow In rows
                dtFiltered.ImportRow(row)
            Next

            ' في ApplyFilter:
            If dtFiltered.Rows.Count > 0 Then
                dgvResults.DataSource = dtFiltered
                dgvResults.CurrentCell = dgvResults.Rows(0).Cells(1)
                dgvResults.Visible = True

                SetupGridColumns()


                dgvResults.Width = Me.Width

                Me.Height = txtSearch.Height + dgvResults.Height + _marginBetweenSearchAndGrid
                dgvResults.BringToFront()
                Me.BringToFront()
                isShowingAll = False

            Else
                dgvResults.DataSource = dtFiltered

                If isShowingAll = False Then
                    dgvResults.Visible = False
                    Me.Height = txtSearch.Height + 10
                    isShowingAll = True
                End If

            End If

        End If

        If dgvResults.Visible AndAlso dgvResults.Rows.Count > 0 Then
            dgvResults.CurrentCell = dgvResults.Rows(0).Cells(1)
        End If


        txtSearch.Select()
    End Sub

    Private Sub IM_SH_txt_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles txtSearch.MouseDoubleClick
        Items_Search.ShowDialog()
        If GLOBAL_IM_ID > 0 Then
            SelectItemById(GLOBAL_IM_ID)
        End If
    End Sub

    '-------------------------------------------------------------------
    ' دالة لاختيار صنف بناءً على رقم IM_ID
    '-------------------------------------------------------------------

    Public Sub SelectItemById(ByVal itemId As Integer)
        If _itemsTable Is Nothing OrElse _itemsTable.Rows.Count = 0 Then
            MessageBox.Show("جدول الأصناف فارغ.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim foundRows() As DataRow = _itemsTable.Select("ItemID = " & itemId)
        If foundRows.Length = 0 Then
            MessageBox.Show("لم يتم العثور على الصنف المطلوب.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim dtFiltered As DataTable = _itemsTable.Clone()
        dtFiltered.ImportRow(foundRows(0))
        dgvResults.DataSource = dtFiltered
        dgvResults.Visible = True

        SetupGridColumns()
        dgvResults.CurrentCell = dgvResults.Rows(0).Cells(1)

        Dim itemName As String = foundRows(0)("ItemName").ToString()
        Dim isValid As String = foundRows(0)("isValid").ToString()

        txtSearch.Text = itemName
        txtSearch.BackColor = SystemColors.Info

        RaiseEvent ItemSelected(itemId, isValid)

        dgvResults.Visible = False
        Me.Height = txtSearch.Height + 10
    End Sub

    'Public Sub SelectItemById(ByVal itemId As Integer)
    '    ' تحقق من أن البيانات موجودة
    '    If _itemsTable Is Nothing OrElse _itemsTable.Rows.Count = 0 Then
    '        MessageBox.Show("جدول الأصناف فارغ.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        Exit Sub
    '    End If

    '    ' البحث عن الصف في الجدول
    '    Dim foundRows() As DataRow = _itemsTable.Select("ItemID = " & itemId)
    '    If foundRows.Length = 0 Then
    '        MessageBox.Show("لم يتم العثور على الصنف المطلوب.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Exit Sub
    '    End If

    '    ' عرض الصف المحدد فقط في DataGridView
    '    Dim dtFiltered As DataTable = _itemsTable.Clone()
    '    dtFiltered.ImportRow(foundRows(0))
    '    dgvResults.DataSource = dtFiltered
    '    dgvResults.Visible = True

    '    ' تهيئة الأعمدة
    '    SetupGridColumns()

    '    ' تحديد الصف الحالي
    '    dgvResults.CurrentCell = dgvResults.Rows(0).Cells(1)

    '    ' تعبئة مربع البحث بالاسم
    '    Dim itemName As String = foundRows(0)("ItemName").ToString()
    '    txtSearch.Text = itemName
    '    txtSearch.BackColor = SystemColors.Info

    '    ' إطلاق الحدث ItemSelected
    '    RaiseEvent ItemSelected(itemId)


    '    ' إخفاء الجدول بعد الاختيار (اختياري)
    '    dgvResults.Visible = False
    '    Me.Height = txtSearch.Height + 10
    'End Sub

    '------------------------------------------------------------------------------------------------------------------------------------------------------------




    Private Function EscapeLikeValue(value As String) As String
        ' الهروب للأحرف الخاصة: %, _, [, ], *, '
        Return value.Replace("'", "").Replace("[", "").Replace("%", "").Replace("_", "").Replace("*", "").Replace("]", "")
    End Function


    Private Sub SetupGridColumns()
        ' إذا لم توجد الأعمدة، لا تكمل
        If dgvResults.Columns.Count = 0 Then Exit Sub

        ' إخفاء العمود ITEM_ID
        If dgvResults.Columns.Contains("ItemID") Then dgvResults.Columns("ItemID").Visible = False

        AdjustGridColumns()
    End Sub


    Private Sub dgvResults_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvResults.CellClick
        If e.RowIndex >= 0 Then
            dgvResults.CurrentCell = dgvResults.Rows(e.RowIndex).Cells(1)
            SelectCurrentRow()
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Clear_txt()
    End Sub

    Public Sub Clear_txt()
        txtSearch.Text = ""
        txtSearch.BackColor = SystemColors.Window
        dgvResults.Visible = False
        Me.Height = txtSearch.Height + 10
        txtSearch.Select()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Refresh_Data()
    End Sub

    Public Sub Refresh_Data()
        Load_ALL_IM()
        Me.ItemsTable = IM_Dt
        Me.itemsTable_Barcode = IM_Dt_Barcodes
    End Sub

    Private Sub btnToggleView_Click(sender As Object, e As EventArgs) Handles btnToggleView.Click
        If _itemsTable Is Nothing Then Exit Sub

        If isShowingAll Then
            dgvResults.Visible = False
            Me.Height = txtSearch.Height + 10
            isShowingAll = False
        Else
            dgvResults.DataSource = _itemsTable.Copy()
            dgvResults.Visible = True

            Dim estimatedHeight As Integer = dgvResults.ColumnHeadersHeight + (dgvResults.Rows.Count * dgvResults.RowTemplate.Height) + 5
            If estimatedHeight > _maxGridHeight Then estimatedHeight = _maxGridHeight
            dgvResults.Height = estimatedHeight

            dgvResults.Width = Me.Width

            SetupGridColumns()

            Me.Height = txtSearch.Height + dgvResults.Height + _marginBetweenSearchAndGrid
            dgvResults.BringToFront()
            Me.BringToFront()
            isShowingAll = True
        End If

        txtSearch.Select()

    End Sub

    Private Sub SearchItemControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Refresh_Data()

        cmbSearchBy.SelectedIndex = MY_Settings.S_Default
    End Sub


    Public Function IM_Serach_Filter(IM_SH As String, _SearchColumn As String) As String
        Dim words As String() = IM_SH.Split(New Char() {" "c})
        Dim Str As String = ""
        Dim IM_Str As String = ""
        Dim S_and As String = " and " & _SearchColumn & " Like "

        If words.Length() = 1 Then
            Str = _SearchColumn & "  = '" & IM_SH & "'  or " & _SearchColumn & "  like '%" & words(0) & "%' or " & _SearchColumn & "  like '%" & IM_SH & "' or " & _SearchColumn & "  like '" & IM_SH & "%'"
        Else
            IM_Str = "'%" & words(0) & "%'" & S_and

            For i = 1 To words.Length - 1

                If i = words.Length - 1 Then
                    IM_Str += "'%" & words(i) & "%'"
                Else
                    IM_Str += "'%" & words(i) & "%'" & S_and
                End If
            Next


            Str = _SearchColumn & "  like " & IM_Str

        End If
        Return Str
    End Function



End Class

