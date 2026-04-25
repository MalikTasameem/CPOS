Imports System.ComponentModel

Public Class FSearch_Filter
    Inherits UserControl
    Public alreadyFocused As Boolean
    Private ShowFlag As Boolean = False
    Private All_Ready_ShowFlag As Boolean = False
    Private WithEvents Text As TextBox
    Private WithEvents cancelSearchButton As Button
    Private BtSize As Size
    Private WithEvents QuickView As DataGridView
    Private BaseListWidth As Integer
    Public TXT_ID As New TextBox
    Public Event ID_Changed(sender As Object, e As EventArgs)
    Dim Dt As New DataTable

    'Public Event Text_KeyDown_(sender As Object, e As KeyEventArgs)

    Dim C As New C

    Public Function IM_Serach(IM_SH As String) As String
        Dim words As String() = IM_SH.Split(New Char() {" "c})
        Dim Str As String = ""
        Dim IM_Str As String = ""
        Dim S_and As String = " and " & _SearchColumn & " Like "

        If words.Length() = 1 Then
            Str = "Select " & _ID & "," & _Column & " from " & _Table & " where " & _SearchColumn & "  = '" & Text.Text & "'  or " & _SearchColumn & "  like '%" & words(0) & "%' or " & _SearchColumn & "  like '%" & Text.Text & "' or " & _SearchColumn & "  like '" & Text.Text & "%'"
        Else
            IM_Str = "'%" & words(0) & "%'" & S_and

            For i = 1 To words.Length - 1

                If i = words.Length - 1 Then
                    IM_Str += "'%" & words(i) & "%'"
                Else
                    IM_Str += "'%" & words(i) & "%'" & S_and
                End If
            Next
            Str = "Select " & _ID & "," & _Column & " from " & _Table & " where " & _SearchColumn & "  like " & IM_Str & " Order by " & _OrderByField

        End If
        Return Str
    End Function



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
            If QuickView Is Nothing OrElse QuickView.Rows.Count = 0 Then Exit Sub

            ' الارتفاع الافتراضي للصف = حجم الخط × 2 (لجعل الشكل متناسق)
            Dim newRowHeight As Integer = CInt(Me.Font.Size * 2)

            For Each row As DataGridViewRow In QuickView.Rows
                row.Height = newRowHeight
            Next

            QuickView.RowTemplate.Height = newRowHeight
            QuickView.Refresh()
        Catch ex As Exception
            ' تجاهل أي خطأ بسيط في حال لم يكن الجريد جاهز بعد
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------------------



    Function MatchesInitials(fullName As String, initials As String) As Boolean
        Dim nameParts() As String = fullName.Split({" "c, "-"c, "_"c}, StringSplitOptions.RemoveEmptyEntries)
        Dim initialsIndex As Integer = 0

        For Each part As String In nameParts
            If initialsIndex >= initials.Length Then
                Exit For
            End If

            If Char.ToLower(part(0)) = Char.ToLower(initials(initialsIndex)) Then
                initialsIndex += 1
            End If
        Next

        Return initialsIndex = initials.Length
    End Function

    Function IsSubsequence(input As String, pattern As String) As Boolean
        Dim i As Integer = 0
        Dim j As Integer = 0
        While i < input.Length AndAlso j < pattern.Length
            If Char.ToLower(input(i)) = Char.ToLower(pattern(j)) Then
                j += 1
            End If
            i += 1
        End While
        Return j = pattern.Length
    End Function

    Function FilterDataTableSmart(dt As DataTable, columnName As String, searchText As String) As DataTable
        Dim filteredDt As DataTable = dt.Clone()
        For Each row As DataRow In dt.Rows
            Dim value As String = row(columnName).ToString()
            If MatchesInitials(value, searchText) OrElse IsSubsequence(value, searchText) Then
                filteredDt.ImportRow(row)
            End If
        Next
        Return filteredDt
    End Function


    Private Function IM_Serach(IM_SH As String, _SearchColumn As String) As String
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

    Private Sub GetLIst()
        Try


            If Text.Text.Count > 0 Then

                Dim Dv As DataView
                Dv = Dt.AsDataView
                Dv.RowFilter = IM_Serach(Text.Text, _SearchColumn)
                QuickView.DataSource = Dv

                If Me.QuickView.Columns(0).Visible = True Then Me.QuickView.Columns(0).Visible = False
                If QuickView.Rows.Count > 0 Then
                    ShowFlag = True
                Else
                    ShowFlag = False
                End If
                CHECK_DATA()

            Else
                ShowFlag = False
                CHECK_DATA()
            End If


        Catch ex As Exception
            MessageBox.Show("يوجد خطأ باتصالك بقاعدة البيانات والخطأ " & vbNewLine & ex.Message)
        End Try


        'Try

        '    If Text.Text.Count > 0 Then
        '        C.Da = New SqlClient.SqlDataAdapter(IM_Serach(Text.Text), C.Con)

        '        Dt = New DataTable
        '        C.Da.Fill(Dt)
        '        QuickView.DataSource = Dt


        '        If Me.QuickView.Columns(0).Visible = True Then Me.QuickView.Columns(0).Visible = False
        '        If Dt.Rows.Count > 0 Then
        '            ShowFlag = True
        '        Else
        '            ShowFlag = False
        '        End If
        '        CHECK_DATA()

        '    Else
        '        ShowFlag = False
        '        CHECK_DATA()
        '    End If


        'Catch ex As Exception
        '    MessageBox.Show("يوجد خطأ باتصالك بقاعدة البيانات والخطأ " & vbNewLine & ex.Message)
        'End Try
    End Sub


    Private Sub CHECK_DATA()
        Try
            ' ShowFlag = Not ShowFlag
            If ShowFlag Then
                If All_Ready_ShowFlag = False Then ExpandList()
            Else
                RollList()
            End If
            Text.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub New()
        Text = New TextBox
        Text.BorderStyle = BorderStyle.Fixed3D
        '  Text.BorderStyle = BorderStyle.None
        QuickView = New DataGridView
        QuickView.GridColor = Color.Gainsboro
        'Me.Text.BackColor = Color.WhiteSmoke
        Me.Text.BackColor = Color.LightGray
        Me.Text.Font = New System.Drawing.Font("Arial", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Text.MaxLength = 250
        Me.RightToLeft = RightToLeft.Yes
        cancelSearchButton = New Button()
        ' cancelSearchButton.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        cancelSearchButton.Size = New Size(Me.Width * 0.22, Me.Text.Size.Height)
        cancelSearchButton.Location = New Point(Me.Location.X + 1, Me.Location.Y + 1)
        BtSize = New Size(cancelSearchButton.Size)
        cancelSearchButton.TabIndex = 0
        cancelSearchButton.TabStop = False
        cancelSearchButton.FlatStyle = FlatStyle.Flat
        cancelSearchButton.BackColor = Color.Transparent
        cancelSearchButton.FlatAppearance.BorderColor = Color.DarkGray
        cancelSearchButton.FlatAppearance.BorderSize = 0
        cancelSearchButton.Text = ""
        cancelSearchButton.Cursor = Cursors.Arrow

        Controls.Add(cancelSearchButton)
        'cancelSearchButton.Dock = DockStyle.Bottom And DockStyle.Right
        cancelSearchButton.Image = New Bitmap(My.Resources.F7AD, 18, 18)
        AddHandler cancelSearchButton.Click, AddressOf Clickk
        AddHandler cancelSearchButton.Resize, AddressOf RsizeButton
        AddHandler Text.MouseUp, AddressOf MouseUpp
        AddHandler Text.Leave, AddressOf OnLeavee
        AddHandler Text.Enter, AddressOf GotFocuss
        AddHandler Text.TextChanged, AddressOf TextChangedd
        AddHandler Text.KeyDown, AddressOf keydownn
        AddHandler QuickView.Click, AddressOf CellContentDoubleClick
        cancelSearchButton.ImageAlign = ContentAlignment.MiddleCenter
        cancelSearchButton.TextImageRelation = TextImageRelation.Overlay
        Text.Width = Me.Width - BtSize.Width - 3
        Me.Height = Text.Height + 4
        Text.Location = New Point(Me.ClientRectangle.X + BtSize.Width + 2, Me.ClientRectangle.Y + 1)
        'Text.AutoSize = True
        Controls.Add(Text)
        Me.QuickView.AllowUserToAddRows = False
        Me.QuickView.AllowUserToDeleteRows = False
        Me.QuickView.AllowUserToResizeColumns = False
        Me.QuickView.AllowUserToResizeRows = False
        Me.QuickView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.QuickView.BackgroundColor = System.Drawing.Color.Gainsboro
        Me.QuickView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.QuickView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.QuickView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.QuickView.ColumnHeadersVisible = False
        Me.QuickView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.QuickView.GridColor = System.Drawing.Color.White
        Me.QuickView.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.QuickView.MultiSelect = False
        Me.QuickView.Name = "quickview"
        Me.QuickView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.QuickView.RowHeadersVisible = False
        Me.QuickView.RowTemplate.Height = 30
        Me.QuickView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect

        QuickView.Size = New Size(Me.Width - 2, _ListSize)
        QuickView.Location = New Point(Me.ClientRectangle.X + 1, Text.Location.Y + Text.Height + 1)

        'QuickView.AutoSize = True
        Controls.Add(QuickView)



        AddHandler TXT_ID.TextChanged, AddressOf TXT_ID_Changed

        'AddHandler Text.KeyDown, AddressOf Text_KeyDown

        TXT_ID.Visible = False
        TXT_ID.Text = "0"
        Controls.Add(TXT_ID)
        BaseListWidth = _ListSize
        ' Send EM_SETMARGINS to prevent text from disappearing underneath the button
    End Sub


    'Sub Text_KeyDown()
    '    Try
    '        RaiseEvent Text_KeyDown_(TXT_ID, New KeyEventArgs)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Sub TXT_ID_Changed()
        Try
            RaiseEvent ID_Changed(TXT_ID, New EventArgs)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextChangedd(sender As Object, e As EventArgs)
        Try
            'If ShowFlag Then
            GetLIst()
            'End If
            If Text.Text.Count = 0 Then
                TXT_ID.Text = "0"
                Text.BackColor = Color.LightGray
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub UpdateControls()
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub DataGridView1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles QuickView.MouseWheel
        Try
            Dim currentIndex As Integer = Me.QuickView.FirstDisplayedScrollingRowIndex
            Dim scrollLines As Integer = SystemInformation.MouseWheelScrollLines

            Select Case e.Delta
                Case 120 'Scrolling up
                    'We cannot scroll up past row 0.
                    Me.QuickView.FirstDisplayedScrollingRowIndex = Math.Max(0, currentIndex - scrollLines)
                Case -120 'Scrolling down
                    Me.QuickView.FirstDisplayedScrollingRowIndex = currentIndex + scrollLines
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Text.Width = Me.Width - BtSize.Width - 3
        QuickView.Size = New Size(Me.Width - 2, _ListSize)
        QuickView.Location = New Point(Me.ClientRectangle.X + 1, Text.Location.Y + Text.Height + 1)
        If Not ShowFlag Then
            Me.Height = Text.Height + 2
        End If
        ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Silver, ButtonBorderStyle.Solid)
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)
        Text.Width = Me.Width - BtSize.Width - 3
        QuickView.Size = New Size(Me.Width - 2, _ListSize)
        QuickView.Location = New Point(Me.ClientRectangle.X + 1, Text.Location.Y + Text.Height + 1)
        If Not ShowFlag Then
            Me.Height = Text.Height + 2
        End If
        If ShowFlag Then
            If Me.Height < (_ListSize + Text.Height) Then
                Me.Height = Text.Height + 2
                cancelSearchButton.Image = New Bitmap(My.Resources.F7AD, 18, 18)
                ShowFlag = False
                cancelSearchButton.BackColor = Color.Transparent
            End If
        End If
    End Sub
    Private Sub AdjustSize()
        Try
            If Me.Height > _ListSize Then
                Me.Height -= _ListSize
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub quickview_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles QuickView.CellMouseEnter
        Try
            QuickView.Rows(e.RowIndex).Selected = True

        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextsizeChange() Handles Text.Resize
        Try
            Text.Width = Me.Width - BtSize.Width - 3
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextLocationChange() Handles Text.LocationChanged
        Try
            Text.Location = New Point(Me.ClientRectangle.X + BtSize.Width + 2, Me.ClientRectangle.Y + 1)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btLocationChange() Handles cancelSearchButton.LocationChanged
        Try
            cancelSearchButton.Location = New Point(Me.ClientRectangle.X + 1, Me.ClientRectangle.Y + 1)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub quickview_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles QuickView.CellMouseLeave
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub CellContentDoubleClick(sender As Object, e As EventArgs)
        Try
            If QuickView.SelectedRows.Count > 0 Then
                'Set_IM(QuickView.SelectedRows(0).Cells(1).Value.ToString, QuickView.SelectedRows(0).Cells(0).Value.ToString)
                'Clickk()
                'Text.Focus()
                Set_IM(QuickView.SelectedRows(0).Cells(1).Value.ToString, QuickView.SelectedRows(0).Cells(0).Value.ToString)
                ShowFlag = False
                RollList()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RsizeButton()
        cancelSearchButton.Size = BtSize
    End Sub
    Private Sub ResizeQuickView()
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub RollList()
        Try
            If Me.Height > (_ListSize + Text.Height) Then
                Me.Height -= _ListSize
            End If

            QuickView.Size = New Size(Me.Width - 2, _ListSize)
            QuickView.Location = New Point(Me.ClientRectangle.X + 1, Text.Location.Y + Text.Height + 2)
            cancelSearchButton.Image = New Bitmap(My.Resources.F7AD, 18, 18)
            cancelSearchButton.BackColor = Color.Transparent
            All_Ready_ShowFlag = False
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ExpandList()
        Try
            If Me.Location.Y + Me.Height + BaseListWidth > Me.Parent.Size.Height Then
                Dim plus As Integer = (Me.Location.Y + Me.Height + BaseListWidth) - Me.Parent.Size.Height
                _ListSize = Me.Parent.Size.Height - (Me.Location.Y + (Text.Height + 2)) - 35
            Else
                _ListSize = BaseListWidth
            End If
            Me.Height += _ListSize
            QuickView.Size = New Size(Me.Width - 2, _ListSize)
            QuickView.Location = New Point(Me.ClientRectangle.X + 1, Text.Location.Y + Text.Height + 2)
            cancelSearchButton.Image = New Bitmap(My.Resources.F7AU, 18, 18)
            If Text.Text.Count = 0 Then GetLIst_ALL()
            cancelSearchButton.BackColor = Color.Gainsboro
            Me.BringToFront()
            All_Ready_ShowFlag = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetLIst_ALL()
        Try
            C.Da = New SqlClient.SqlDataAdapter("Select TOP 999999 " & _ID & "," & _Column & " from " & _Table & " WHERE 1 = 1 " & _USER_WHERE & " Order by " & _OrderByField, C.Con)

            Dt = New DataTable
            C.Da.Fill(Dt)
            QuickView.DataSource = Dt
            If Me.QuickView.Columns(0).Visible = True Then Me.QuickView.Columns(0).Visible = False
        Catch ex As Exception
            '  MessageBox.Show("يوجد خطأ باتصالك بقاعدة البيانات والخطأ " & vbNewLine & ex.Message)
        End Try
    End Sub
    Private Sub Clickk()
        Try
            ShowFlag = Not ShowFlag
            If ShowFlag Then
                ExpandList()
            Else
                RollList()
            End If
            Text.Focus()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub keydownn(sender As Object, e As KeyEventArgs)
        'If My_Settings.ShortCuts Then
        If e.Control And e.KeyCode = Keys.C Then
            Me.Text.Copy()
        End If
        If e.Control And e.KeyCode = Keys.V Then
            Me.Text.Paste()
        End If
        If e.Control And e.KeyCode = Keys.X Then
            Me.Text.Cut()
        End If
        If e.Control And e.KeyCode = Keys.Z Then
            Me.Text.Undo()
        End If
        'End If

        If e.Control And e.KeyCode = Keys.A Then
            Me.Text.SelectAll()
        ElseIf e.KeyCode = Keys.Down Then
            alreadyFocused = False
        ElseIf e.KeyCode = Keys.Return Then


            '-----------------------------
            'If ShowFlag Then
            If QuickView.SelectedRows.Count > 0 And Textt.Count > 0 Then
                Set_IM(QuickView.SelectedRows(0).Cells(1).Value.ToString, QuickView.SelectedRows(0).Cells(0).Value.ToString)
                ShowFlag = False
                RollList()
                Me.Text.SelectAll()
            End If
            '-----------------------------

            '    ShowFlag = Not ShowFlag
            'End If
            'e.SuppressKeyPress = True

        End If
        If e.KeyCode = Keys.F2 Then
            Clickk()
        End If
        If e.KeyCode = Keys.Down Then
            If Not ShowFlag Then
                Clickk()
            Else
                SelectNextItem()
            End If
            e.SuppressKeyPress = True
        End If
        If e.KeyCode = Keys.Up Then
            If ShowFlag Then
                SelectPrevItem()
            End If
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub Set_IM(NAME_ As String, ID_ As String)
        Text.Text = NAME_
        TXT_ID.Text = ID_
        '  Text.BackColor = Color.LightGoldenrodYellow
    End Sub

    Public Sub Set_IM_By_ID(ID_ As String)
        GetLIst_ALL()

        Dim Dv As DataView
        Dv = Dt.AsDataView
        Dv.RowFilter = _ID & " = " & ID_
        QuickView.DataSource = Dv

        If QuickView.SelectedRows.Count > 0 Then
            Set_IM(QuickView.SelectedRows(0).Cells(1).Value.ToString, QuickView.SelectedRows(0).Cells(0).Value.ToString)
            ShowFlag = False
            RollList()
            'Me.Text.SelectAll()
        End If

        'Dim c As New C
        'Try
        '    Dim s As String
        '    s = "Select " & _Column & " from " & _Table & " where " & _ID & "  = " & ID_
        '    c.Com = New SqlClient.SqlCommand(s, c.Con)
        '    c.Con.Open()
        '    c.Dr = c.Com.ExecuteReader
        '    If c.Dr.HasRows Then
        '        c.Dr.Read()
        '        Set_IM(c.Dr(_Column), ID_)
        '        RollList()
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub SelectNextItem()
        Try
            If QuickView.SelectedRows.Count > 0 Then
                If QuickView.SelectedRows(0).Index < QuickView.RowCount - 1 Then
                    QuickView.Rows(QuickView.SelectedRows(0).Index + 1).Selected = True
                    QuickView.CurrentCell = QuickView.SelectedRows(0).Cells(1)
                    'TXT_ID.Text = QuickView.SelectedRows(0).Cells(0).Value
                End If
            Else
                QuickView.Rows(0).Selected = True
                QuickView.FirstDisplayedScrollingRowIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SelectPrevItem()
        Try
            If QuickView.SelectedRows.Count > 0 Then
                If QuickView.SelectedRows(0).Index > 0 Then
                    QuickView.Rows(QuickView.SelectedRows(0).Index - 1).Selected = True
                    QuickView.CurrentCell = QuickView.SelectedRows(0).Cells(1)
                    'TXT_ID.Text = QuickView.SelectedRows(0).Cells(0).Value
                End If
            Else
                QuickView.Rows(QuickView.RowCount - 1).Selected = True
                QuickView.FirstDisplayedScrollingRowIndex = QuickView.RowCount - 1
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Overrides Sub OnLeave(e As EventArgs)
        MyBase.OnLeave(e)
        If ShowFlag Then
            RollList()
        End If
    End Sub
    Private Sub OnLeavee()
        alreadyFocused = False
        'Text.BackColor = Color.WhiteSmoke
    End Sub

    Private Sub GotFocuss()
        ' Select all text only if the mouse isn't down.
        ' This makes tabbing to the textbox give focus.
        If MouseButtons = MouseButtons.None Then
            Me.Text.SelectAll()
        End If
        alreadyFocused = True
        'Me.Text.BackColor = Color.White
    End Sub

    Private Sub MouseUpp()

        ' Web browsers like Google Chrome select the text on mouse up.
        ' They only do it if the textbox isn't already focused,
        ' and if the user hasn't selected all text.
        If Not alreadyFocused AndAlso Me.Text.SelectionLength = 0 Then
            Me.Text.SelectAll()
        End If
        alreadyFocused = True
        'Me.Text.BackColor = Color.White
    End Sub
#Region " Properties "
    Private _NumberOfRows As Integer
    Private _Column As String
    Private _ID As String
    Private _Table As String
    Private _SearchColumn As String
    Private _IsNumeric As Boolean
    Private _OrderByField As String
    Private _ListSize As Integer = 200
    Private _USER_WHERE As String = ""

    Public Property SQL_ID As String
        Get
            Return _ID
        End Get
        Set(value As String)
            _ID = value
            Invalidate()
        End Set
    End Property

    Public Property SQL_ListSize As Integer
        Get
            Return _ListSize
        End Get
        Set(value As Integer)
            If value < 1000 And value >= 0 Then
                _ListSize = value
                Invalidate()
            End If

        End Set
    End Property
    Public Property SQL_NumberOfRows As Integer
        Get
            Return _NumberOfRows
        End Get
        Set(value As Integer)
            If value < 1000 And value >= 0 Then
                _NumberOfRows = value
                Invalidate()
            End If
        End Set
    End Property
    Public Property SQL_Column As String
        Get
            Return _Column
        End Get
        Set(value As String)
            _Column = value
            Invalidate()
        End Set
    End Property
    Public Property SQL_Table As String
        Get
            Return _Table
        End Get
        Set(value As String)
            _Table = value
            Invalidate()
        End Set
    End Property
    Public Property SQL_SearchField As String
        Get
            Return _SearchColumn
        End Get
        Set(value As String)
            _SearchColumn = value
            Invalidate()
        End Set
    End Property
    Public Property SQL_IsNumericSearchField As Boolean
        Get
            Return _IsNumeric
        End Get
        Set(value As Boolean)
            _IsNumeric = value
            Invalidate()
        End Set
    End Property
    Public Property SQL_OrderByField As String
        Get
            Return _OrderByField
        End Get
        Set(value As String)
            _OrderByField = value
            Invalidate()
        End Set
    End Property

    Public Property SQL_SearchField_WHERE As String
        Get
            Return _USER_WHERE
        End Get
        Set(value As String)
            _USER_WHERE = value
            Invalidate()
        End Set
    End Property

#End Region
    Public Property CancelSearchImage() As Image

        Get
            Return cancelSearchButton.Image
        End Get
        Set(value As Image)
            cancelSearchButton.Image = New Bitmap(value, 18, 18)
        End Set
    End Property
    Public Property TextMaxLength() As Integer

        Get
            Return Me.Text.MaxLength
        End Get
        Set(value As Integer)
            Me.Text.MaxLength = value
        End Set
    End Property

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'FSearch_Filter
        '
        Me.Name = "FSearch_Filter"
        Me.ResumeLayout(False)

    End Sub

    Private Sub F7TextWithButton_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        Try

        Catch ex As Exception

        End Try
    End Sub
    Public Property Textt() As String

        Get
            Return Me.Text.Text
        End Get
        Set(value As String)
            Me.Text.Text = value
            Invalidate()
        End Set
    End Property

    Private Sub FSearch_Filter_FontChanged(sender As Object, e As EventArgs) Handles MyBase.FontChanged
        'Text.Font = MyBase.Font
    End Sub

    Private Sub FSearch_Filter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetLIst_ALL()
    End Sub
End Class









'----------------------------------------------------------------------------------------------------------------------------------------------PREV BEFRE 25-3-2026
'Imports System.ComponentModel

'Public Class FSearch_Filter
'    Inherits UserControl
'    Public alreadyFocused As Boolean
'    Private ShowFlag As Boolean = False
'    Private All_Ready_ShowFlag As Boolean = False
'    Private WithEvents Text As TextBox
'    Private WithEvents cancelSearchButton As Button
'    Private BtSize As Size
'    Private WithEvents QuickView As DataGridView
'    Private BaseListWidth As Integer
'    Public TXT_ID As New TextBox
'    Public Event ID_Changed(sender As Object, e As EventArgs)

'    'Public Event Text_KeyDown_(sender As Object, e As KeyEventArgs)

'    Dim C As New C

'    Public Function IM_Serach(IM_SH As String) As String
'        Dim words As String() = IM_SH.Split(New Char() {" "c})
'        Dim Str As String = ""
'        Dim IM_Str As String = ""
'        Dim S_and As String = " and " & _SearchColumn & " Like "

'        If words.Length() = 1 Then
'            Str = "Select " & _ID & "," & _Column & " from " & _Table & " where " & _SearchColumn & "  = '" & Text.Text & "'  or " & _SearchColumn & "  like '%" & words(0) & "%' or " & _SearchColumn & "  like '%" & Text.Text & "' or " & _SearchColumn & "  like '" & Text.Text & "%'"
'        Else
'            IM_Str = "'%" & words(0) & "%'" & S_and

'            For i = 1 To words.Length - 1

'                If i = words.Length - 1 Then
'                    IM_Str += "'%" & words(i) & "%'"
'                Else
'                    IM_Str += "'%" & words(i) & "%'" & S_and
'                End If
'            Next
'            Str = "Select " & _ID & "," & _Column & " from " & _Table & " where " & _SearchColumn & "  like " & IM_Str & " Order by " & _OrderByField

'        End If
'        Return Str
'    End Function


'    '------------------------------------------------------------------------------------------------------------------
'    '-------------------------------------------------------------------
'    ' إعادة تعريف خاصية Font لتطبيقها على كل الأدوات الداخلية
'    '-------------------------------------------------------------------
'    Public Overrides Property Font As Font
'        Get
'            Return MyBase.Font
'        End Get
'        Set(value As Font)
'            MyBase.Font = value
'            ApplyFontToAllControls(Me, value)

'            ' ضبط ارتفاع صفوف الـ DataGridView تلقائيًا حسب حجم الخط
'            AdjustGridRowHeights()
'        End Set
'    End Property


'    '-------------------------------------------------------------------
'    ' دالة مساعدة تطبق الخط على كل العناصر الداخلية
'    '-------------------------------------------------------------------
'    Private Sub ApplyFontToAllControls(parent As Control, font As Font)
'        For Each ctrl As Control In parent.Controls
'            ctrl.Font = font
'            ' في حال وجود أدوات داخل أدوات أخرى
'            If ctrl.HasChildren Then
'                ApplyFontToAllControls(ctrl, font)
'            End If
'        Next
'    End Sub


'    '-------------------------------------------------------------------
'    ' دالة لضبط ارتفاع صفوف الجريد بناءً على حجم الخط
'    '-------------------------------------------------------------------
'    Private Sub AdjustGridRowHeights()
'        Try
'            If QuickView Is Nothing OrElse QuickView.Rows.Count = 0 Then Exit Sub

'            ' الارتفاع الافتراضي للصف = حجم الخط × 2 (لجعل الشكل متناسق)
'            Dim newRowHeight As Integer = CInt(Me.Font.Size * 2)

'            For Each row As DataGridViewRow In QuickView.Rows
'                row.Height = newRowHeight
'            Next

'            QuickView.RowTemplate.Height = newRowHeight
'            QuickView.Refresh()
'        Catch ex As Exception
'            ' تجاهل أي خطأ بسيط في حال لم يكن الجريد جاهز بعد
'        End Try
'    End Sub

'    '------------------------------------------------------------------------------------------------------------------


'    Private Sub GetLIst()
'        Try
'            'If CheckSQlParameter() Then

'            If Text.Text.Count > 0 Then
'                C.Da = New SqlClient.SqlDataAdapter(IM_Serach(Text.Text), C.Con)

'                C.Dt = New DataTable
'                C.Da.Fill(C.Dt)
'                QuickView.DataSource = C.Dt
'                'QuickView.ClearSelection()


'                If Me.QuickView.Columns(0).Visible = True Then Me.QuickView.Columns(0).Visible = False
'                If C.Dt.Rows.Count > 0 Then
'                    ShowFlag = True
'                Else
'                    ShowFlag = False
'                End If
'                CHECK_DATA()

'            Else
'                ShowFlag = False
'                CHECK_DATA()
'            End If



'            'End If
'        Catch ex As Exception
'            MessageBox.Show("يوجد خطأ باتصالك بقاعدة البيانات والخطأ " & vbNewLine & ex.Message)
'        End Try
'    End Sub


'    Private Sub CHECK_DATA()
'        Try
'            ' ShowFlag = Not ShowFlag
'            If ShowFlag Then
'                If All_Ready_ShowFlag = False Then ExpandList()
'            Else
'                RollList()
'            End If
'            Text.Focus()
'        Catch ex As Exception

'        End Try
'    End Sub

'    Public Sub New()
'        Text = New TextBox
'        Text.BorderStyle = BorderStyle.Fixed3D
'        '  Text.BorderStyle = BorderStyle.None
'        QuickView = New DataGridView
'        QuickView.GridColor = Color.Gainsboro
'        'Me.Text.BackColor = Color.WhiteSmoke
'        Me.Text.BackColor = Color.LightGray
'        Me.Text.Font = New System.Drawing.Font("Arial", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
'        Me.Text.MaxLength = 250
'        Me.RightToLeft = RightToLeft.Yes
'        cancelSearchButton = New Button()
'        ' cancelSearchButton.Anchor = AnchorStyles.Top Or AnchorStyles.Right
'        cancelSearchButton.Size = New Size(Me.Width * 0.22, Me.Text.Size.Height)
'        cancelSearchButton.Location = New Point(Me.Location.X + 1, Me.Location.Y + 1)
'        BtSize = New Size(cancelSearchButton.Size)
'        cancelSearchButton.TabIndex = 0
'        cancelSearchButton.TabStop = False
'        cancelSearchButton.FlatStyle = FlatStyle.Flat
'        cancelSearchButton.BackColor = Color.Transparent
'        cancelSearchButton.FlatAppearance.BorderColor = Color.DarkGray
'        cancelSearchButton.FlatAppearance.BorderSize = 0
'        cancelSearchButton.Text = ""
'        cancelSearchButton.Cursor = Cursors.Arrow

'        Controls.Add(cancelSearchButton)
'        'cancelSearchButton.Dock = DockStyle.Bottom And DockStyle.Right
'        cancelSearchButton.Image = New Bitmap(My.Resources.F7AD, 18, 18)
'        AddHandler cancelSearchButton.Click, AddressOf Clickk
'        AddHandler cancelSearchButton.Resize, AddressOf RsizeButton
'        AddHandler Text.MouseUp, AddressOf MouseUpp
'        AddHandler Text.Leave, AddressOf OnLeavee
'        AddHandler Text.Enter, AddressOf GotFocuss
'        AddHandler Text.TextChanged, AddressOf TextChangedd
'        AddHandler Text.KeyDown, AddressOf keydownn
'        AddHandler QuickView.Click, AddressOf CellContentDoubleClick
'        cancelSearchButton.ImageAlign = ContentAlignment.MiddleCenter
'        cancelSearchButton.TextImageRelation = TextImageRelation.Overlay
'        Text.Width = Me.Width - BtSize.Width - 3
'        Me.Height = Text.Height + 4
'        Text.Location = New Point(Me.ClientRectangle.X + BtSize.Width + 2, Me.ClientRectangle.Y + 1)
'        'Text.AutoSize = True
'        Controls.Add(Text)
'        Me.QuickView.AllowUserToAddRows = False
'        Me.QuickView.AllowUserToDeleteRows = False
'        Me.QuickView.AllowUserToResizeColumns = False
'        Me.QuickView.AllowUserToResizeRows = False
'        Me.QuickView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
'        Me.QuickView.BackgroundColor = System.Drawing.Color.Gainsboro
'        Me.QuickView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
'        Me.QuickView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
'        Me.QuickView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
'        Me.QuickView.ColumnHeadersVisible = False
'        Me.QuickView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
'        Me.QuickView.GridColor = System.Drawing.Color.White
'        Me.QuickView.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
'        Me.QuickView.MultiSelect = False
'        Me.QuickView.Name = "quickview"
'        Me.QuickView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
'        Me.QuickView.RowHeadersVisible = False
'        Me.QuickView.RowTemplate.Height = 30
'        Me.QuickView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect

'        QuickView.Size = New Size(Me.Width - 2, _ListSize)
'        QuickView.Location = New Point(Me.ClientRectangle.X + 1, Text.Location.Y + Text.Height + 1)

'        'QuickView.AutoSize = True
'        Controls.Add(QuickView)



'        AddHandler TXT_ID.TextChanged, AddressOf TXT_ID_Changed

'        'AddHandler Text.KeyDown, AddressOf Text_KeyDown

'        TXT_ID.Visible = False
'        TXT_ID.Text = "0"
'        Controls.Add(TXT_ID)
'        BaseListWidth = _ListSize
'        ' Send EM_SETMARGINS to prevent text from disappearing underneath the button
'    End Sub


'    'Sub Text_KeyDown()
'    '    Try
'    '        RaiseEvent Text_KeyDown_(TXT_ID, New KeyEventArgs)
'    '    Catch ex As Exception

'    '    End Try
'    'End Sub

'    Sub TXT_ID_Changed()
'        Try
'            RaiseEvent ID_Changed(TXT_ID, New EventArgs)
'        Catch ex As Exception

'        End Try
'    End Sub

'    Private Sub TextChangedd(sender As Object, e As EventArgs)
'        Try
'            'If ShowFlag Then
'            GetLIst()
'            'End If
'            If Text.Text.Count = 0 Then
'                TXT_ID.Text = "0"
'                Text.BackColor = Color.LightGray
'            End If

'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try
'    End Sub
'    Private Sub UpdateControls()
'        Try

'        Catch ex As Exception

'        End Try
'    End Sub
'    Private Sub DataGridView1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles QuickView.MouseWheel
'        Try
'            Dim currentIndex As Integer = Me.QuickView.FirstDisplayedScrollingRowIndex
'            Dim scrollLines As Integer = SystemInformation.MouseWheelScrollLines

'            Select Case e.Delta
'                Case 120 'Scrolling up
'                    'We cannot scroll up past row 0.
'                    Me.QuickView.FirstDisplayedScrollingRowIndex = Math.Max(0, currentIndex - scrollLines)
'                Case -120 'Scrolling down
'                    Me.QuickView.FirstDisplayedScrollingRowIndex = currentIndex + scrollLines
'            End Select
'        Catch ex As Exception

'        End Try

'    End Sub

'    Protected Overrides Sub OnPaint(e As PaintEventArgs)
'        MyBase.OnPaint(e)
'        Text.Width = Me.Width - BtSize.Width - 3
'        QuickView.Size = New Size(Me.Width - 2, _ListSize)
'        QuickView.Location = New Point(Me.ClientRectangle.X + 1, Text.Location.Y + Text.Height + 1)
'        If Not ShowFlag Then
'            Me.Height = Text.Height + 2
'        End If
'        ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Silver, ButtonBorderStyle.Solid)
'    End Sub

'    Protected Overrides Sub OnSizeChanged(e As EventArgs)
'        MyBase.OnSizeChanged(e)
'        Text.Width = Me.Width - BtSize.Width - 3
'        QuickView.Size = New Size(Me.Width - 2, _ListSize)
'        QuickView.Location = New Point(Me.ClientRectangle.X + 1, Text.Location.Y + Text.Height + 1)
'        If Not ShowFlag Then
'            Me.Height = Text.Height + 2
'        End If
'        If ShowFlag Then
'            If Me.Height < (_ListSize + Text.Height) Then
'                Me.Height = Text.Height + 2
'                cancelSearchButton.Image = New Bitmap(My.Resources.F7AD, 18, 18)
'                ShowFlag = False
'                cancelSearchButton.BackColor = Color.Transparent
'            End If
'        End If
'    End Sub
'    Private Sub AdjustSize()
'        Try
'            If Me.Height > _ListSize Then
'                Me.Height -= _ListSize
'            End If
'        Catch ex As Exception

'        End Try
'    End Sub
'    Private Sub quickview_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles QuickView.CellMouseEnter
'        Try
'            QuickView.Rows(e.RowIndex).Selected = True

'        Catch ex As Exception

'        End Try
'    End Sub
'    Private Sub TextsizeChange() Handles Text.Resize
'        Try
'            Text.Width = Me.Width - BtSize.Width - 3
'        Catch ex As Exception

'        End Try
'    End Sub
'    Private Sub TextLocationChange() Handles Text.LocationChanged
'        Try
'            Text.Location = New Point(Me.ClientRectangle.X + BtSize.Width + 2, Me.ClientRectangle.Y + 1)
'        Catch ex As Exception

'        End Try
'    End Sub
'    Private Sub btLocationChange() Handles cancelSearchButton.LocationChanged
'        Try
'            cancelSearchButton.Location = New Point(Me.ClientRectangle.X + 1, Me.ClientRectangle.Y + 1)
'        Catch ex As Exception

'        End Try
'    End Sub

'    Private Sub quickview_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles QuickView.CellMouseLeave
'        Try

'        Catch ex As Exception

'        End Try
'    End Sub
'    Private Sub CellContentDoubleClick(sender As Object, e As EventArgs)
'        Try
'            If QuickView.SelectedRows.Count > 0 Then
'                'Set_IM(QuickView.SelectedRows(0).Cells(1).Value.ToString, QuickView.SelectedRows(0).Cells(0).Value.ToString)
'                'Clickk()
'                'Text.Focus()
'                Set_IM(QuickView.SelectedRows(0).Cells(1).Value.ToString, QuickView.SelectedRows(0).Cells(0).Value.ToString)
'                ShowFlag = False
'                RollList()
'            End If
'        Catch ex As Exception

'        End Try
'    End Sub

'    Private Sub RsizeButton()
'        cancelSearchButton.Size = BtSize
'    End Sub
'    Private Sub ResizeQuickView()
'        Try

'        Catch ex As Exception

'        End Try
'    End Sub
'    Private Sub RollList()
'        Try
'            If Me.Height > (_ListSize + Text.Height) Then
'                Me.Height -= _ListSize
'            End If

'            QuickView.Size = New Size(Me.Width - 2, _ListSize)
'            QuickView.Location = New Point(Me.ClientRectangle.X + 1, Text.Location.Y + Text.Height + 2)
'            cancelSearchButton.Image = New Bitmap(My.Resources.F7AD, 18, 18)
'            cancelSearchButton.BackColor = Color.Transparent
'            All_Ready_ShowFlag = False
'        Catch ex As Exception

'        End Try
'    End Sub
'    Private Sub ExpandList()
'        Try
'            If Me.Location.Y + Me.Height + BaseListWidth > Me.Parent.Size.Height Then
'                Dim plus As Integer = (Me.Location.Y + Me.Height + BaseListWidth) - Me.Parent.Size.Height
'                _ListSize = Me.Parent.Size.Height - (Me.Location.Y + (Text.Height + 2)) - 35
'            Else
'                _ListSize = BaseListWidth
'            End If
'            Me.Height += _ListSize
'            QuickView.Size = New Size(Me.Width - 2, _ListSize)
'            QuickView.Location = New Point(Me.ClientRectangle.X + 1, Text.Location.Y + Text.Height + 2)
'            cancelSearchButton.Image = New Bitmap(My.Resources.F7AU, 18, 18)
'            If Text.Text.Count = 0 Then GetLIst_ALL()
'            cancelSearchButton.BackColor = Color.Gainsboro
'            Me.BringToFront()
'            All_Ready_ShowFlag = True
'        Catch ex As Exception

'        End Try
'    End Sub

'    Private Sub GetLIst_ALL()
'        Try
'            C.Da = New SqlClient.SqlDataAdapter("Select TOP 1000 " & _ID & "," & _Column & " from " & _Table & " Order by " & _OrderByField, C.Con)

'            C.Dt = New DataTable
'            C.Da.Fill(C.Dt)
'            QuickView.DataSource = C.Dt
'            If Me.QuickView.Columns(0).Visible = True Then Me.QuickView.Columns(0).Visible = False
'        Catch ex As Exception
'            MessageBox.Show("يوجد خطأ باتصالك بقاعدة البيانات والخطأ " & vbNewLine & ex.Message)
'        End Try
'    End Sub
'    Private Sub Clickk()
'        Try
'            ShowFlag = Not ShowFlag
'            If ShowFlag Then
'                ExpandList()
'            Else
'                RollList()
'            End If
'            Text.Focus()
'        Catch ex As Exception

'        End Try
'    End Sub
'    Private Sub keydownn(sender As Object, e As KeyEventArgs)
'        'If My_Settings.ShortCuts Then
'        If e.Control And e.KeyCode = Keys.C Then
'            Me.Text.Copy()
'        End If
'        If e.Control And e.KeyCode = Keys.V Then
'            Me.Text.Paste()
'        End If
'        If e.Control And e.KeyCode = Keys.X Then
'            Me.Text.Cut()
'        End If
'        If e.Control And e.KeyCode = Keys.Z Then
'            Me.Text.Undo()
'        End If
'        'End If

'        If e.Control And e.KeyCode = Keys.A Then
'            Me.Text.SelectAll()
'        ElseIf e.KeyCode = Keys.Down Then
'            alreadyFocused = False
'        ElseIf e.KeyCode = Keys.Return Then


'            '-----------------------------
'            'If ShowFlag Then
'            If QuickView.SelectedRows.Count > 0 Then
'                Set_IM(QuickView.SelectedRows(0).Cells(1).Value.ToString, QuickView.SelectedRows(0).Cells(0).Value.ToString)
'                ShowFlag = False
'                RollList()
'                Me.Text.SelectAll()
'            End If
'            '-----------------------------

'            '    ShowFlag = Not ShowFlag
'            'End If
'            'e.SuppressKeyPress = True

'        End If
'        If e.KeyCode = Keys.F2 Then
'            Clickk()
'        End If
'        If e.KeyCode = Keys.Down Then
'            If Not ShowFlag Then
'                Clickk()
'            Else
'                SelectNextItem()
'            End If
'            e.SuppressKeyPress = True
'        End If
'        If e.KeyCode = Keys.Up Then
'            If ShowFlag Then
'                SelectPrevItem()
'            End If
'            e.SuppressKeyPress = True
'        End If
'    End Sub

'    Private Sub Set_IM(NAME_ As String, ID_ As String)
'        Text.Text = NAME_
'        TXT_ID.Text = ID_
'        Text.BackColor = Color.LightGoldenrodYellow
'    End Sub

'    Public Sub Set_IM_By_ID(ID_ As String)

'        Dim c As New C
'        Try
'            Dim s As String
'            s = "Select " & _Column & " from " & _Table & " where " & _ID & "  = " & ID_
'            c.Com = New SqlClient.SqlCommand(s, c.Con)
'            c.Con.Open()
'            c.Dr = c.Com.ExecuteReader
'            If c.Dr.HasRows Then
'                c.Dr.Read()
'                Set_IM(c.Dr(_Column), ID_)
'                RollList()
'            End If
'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try
'    End Sub

'    Private Sub SelectNextItem()
'        Try
'            If QuickView.SelectedRows.Count > 0 Then
'                If QuickView.SelectedRows(0).Index < QuickView.RowCount - 1 Then
'                    QuickView.Rows(QuickView.SelectedRows(0).Index + 1).Selected = True
'                    QuickView.CurrentCell = QuickView.SelectedRows(0).Cells(1)
'                    'TXT_ID.Text = QuickView.SelectedRows(0).Cells(0).Value
'                End If
'            Else
'                QuickView.Rows(0).Selected = True
'                QuickView.FirstDisplayedScrollingRowIndex = 0
'            End If
'        Catch ex As Exception

'        End Try
'    End Sub
'    Private Sub SelectPrevItem()
'        Try
'            If QuickView.SelectedRows.Count > 0 Then
'                If QuickView.SelectedRows(0).Index > 0 Then
'                    QuickView.Rows(QuickView.SelectedRows(0).Index - 1).Selected = True
'                    QuickView.CurrentCell = QuickView.SelectedRows(0).Cells(1)
'                    'TXT_ID.Text = QuickView.SelectedRows(0).Cells(0).Value
'                End If
'            Else
'                QuickView.Rows(QuickView.RowCount - 1).Selected = True
'                QuickView.FirstDisplayedScrollingRowIndex = QuickView.RowCount - 1
'            End If
'        Catch ex As Exception

'        End Try
'    End Sub
'    Protected Overrides Sub OnLeave(e As EventArgs)
'        MyBase.OnLeave(e)
'        If ShowFlag Then
'            RollList()
'        End If
'    End Sub
'    Private Sub OnLeavee()
'        alreadyFocused = False
'        'Text.BackColor = Color.WhiteSmoke
'    End Sub

'    Private Sub GotFocuss()
'        ' Select all text only if the mouse isn't down.
'        ' This makes tabbing to the textbox give focus.
'        If MouseButtons = MouseButtons.None Then
'            Me.Text.SelectAll()
'        End If
'        alreadyFocused = True
'        'Me.Text.BackColor = Color.White
'    End Sub

'    Private Sub MouseUpp()

'        ' Web browsers like Google Chrome select the text on mouse up.
'        ' They only do it if the textbox isn't already focused,
'        ' and if the user hasn't selected all text.
'        If Not alreadyFocused AndAlso Me.Text.SelectionLength = 0 Then
'            Me.Text.SelectAll()
'        End If
'        alreadyFocused = True
'        'Me.Text.BackColor = Color.White
'    End Sub
'#Region " Properties "
'    Private _NumberOfRows As Integer
'    Private _Column As String
'    Private _ID As String
'    Private _Table As String
'    Private _SearchColumn As String
'    Private _IsNumeric As Boolean
'    Private _OrderByField As String
'    Private _ListSize As Integer = 200

'    Public Property SQL_ID As String
'        Get
'            Return _ID
'        End Get
'        Set(value As String)
'            _ID = value
'            Invalidate()
'        End Set
'    End Property

'    Public Property SQL_ListSize As Integer
'        Get
'            Return _ListSize
'        End Get
'        Set(value As Integer)
'            If value < 1000 And value >= 0 Then
'                _ListSize = value
'                Invalidate()
'            End If

'        End Set
'    End Property
'    Public Property SQL_NumberOfRows As Integer
'        Get
'            Return _NumberOfRows
'        End Get
'        Set(value As Integer)
'            If value < 1000 And value >= 0 Then
'                _NumberOfRows = value
'                Invalidate()
'            End If
'        End Set
'    End Property
'    Public Property SQL_Column As String
'        Get
'            Return _Column
'        End Get
'        Set(value As String)
'            _Column = value
'            Invalidate()
'        End Set
'    End Property
'    Public Property SQL_Table As String
'        Get
'            Return _Table
'        End Get
'        Set(value As String)
'            _Table = value
'            Invalidate()
'        End Set
'    End Property
'    Public Property SQL_SearchField As String
'        Get
'            Return _SearchColumn
'        End Get
'        Set(value As String)
'            _SearchColumn = value
'            Invalidate()
'        End Set
'    End Property
'    Public Property SQL_IsNumericSearchField As Boolean
'        Get
'            Return _IsNumeric
'        End Get
'        Set(value As Boolean)
'            _IsNumeric = value
'            Invalidate()
'        End Set
'    End Property
'    Public Property SQL_OrderByField As String
'        Get
'            Return _OrderByField
'        End Get
'        Set(value As String)
'            _OrderByField = value
'            Invalidate()
'        End Set
'    End Property

'#End Region
'    Public Property CancelSearchImage() As Image

'        Get
'            Return cancelSearchButton.Image
'        End Get
'        Set(value As Image)
'            cancelSearchButton.Image = New Bitmap(Value, 18, 18)
'        End Set
'    End Property
'    Public Property TextMaxLength() As Integer

'        Get
'            Return Me.Text.MaxLength
'        End Get
'        Set(value As Integer)
'            Me.Text.MaxLength = Value
'        End Set
'    End Property

'    Private Sub InitializeComponent()
'        Me.SuspendLayout()
'        '
'        'FSearch_Filter
'        '
'        Me.Name = "FSearch_Filter"
'        Me.ResumeLayout(False)

'    End Sub

'    Private Sub F7TextWithButton_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
'        Try

'        Catch ex As Exception

'        End Try
'    End Sub
'    Public Property Textt() As String

'        Get
'            Return Me.Text.Text
'        End Get
'        Set(value As String)
'            Me.Text.Text = Value
'            Invalidate()
'        End Set
'    End Property

'    Private Sub FSearch_Filter_FontChanged(sender As Object, e As EventArgs) Handles MyBase.FontChanged
'        'Text.Font = MyBase.Font
'    End Sub
'End Class


