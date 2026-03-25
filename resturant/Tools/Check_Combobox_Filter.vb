Imports System.ComponentModel

Public Class Check_Combobox_Filter
    Inherits UserControl
    Public alreadyFocused As Boolean
    Private ShowFlag As Boolean = False
    Private All_Ready_ShowFlag As Boolean = False
    Private WithEvents Text As TextBox

    Private WithEvents cancelSearchButton As Button
    Private BtSize As Size
    Private WithEvents QuickView As DataGridView
    Private BaseListWidth As Integer


    Public Values As String = ""
    Public Items As String = ""
    Public Tool_Tip As New ToolTip

    Dim C As New C


    Public Sub New()
        Text = New TextBox
        Text.BorderStyle = BorderStyle.Fixed3D
        '  Text.BorderStyle = BorderStyle.None
        QuickView = New DataGridView
        QuickView.GridColor = Color.Gainsboro
        '  QuickView.EditMode = DataGridViewEditMode.EditOnEnter

        'Me.Text.BackColor = Color.WhiteSmoke
        Me.Text.BackColor = Color.LightGray
        Me.Text.Font = New System.Drawing.Font("Arial", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Text.MaxLength = 250
        Me.RightToLeft = RightToLeft.Yes
        cancelSearchButton = New Button()

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

        cancelSearchButton.Image = New Bitmap(My.Resources.F7AD, 18, 18)
        AddHandler cancelSearchButton.Click, AddressOf Clickk
        AddHandler cancelSearchButton.Resize, AddressOf RsizeButton
        AddHandler Text.MouseUp, AddressOf MouseUpp
        AddHandler Text.Leave, AddressOf OnLeavee
        AddHandler Text.Enter, AddressOf GotFocuss
        ' AddHandler Text.TextChanged, AddressOf TextChangedd
        AddHandler Text.KeyDown, AddressOf keydownn
        ' AddHandler QuickView.Click, AddressOf CellContentDoubleClick
        cancelSearchButton.ImageAlign = ContentAlignment.MiddleCenter
        cancelSearchButton.TextImageRelation = TextImageRelation.Overlay
        Text.Width = Me.Width - BtSize.Width - 3
        Me.Height = Text.Height + 4
        Text.Location = New Point(Me.ClientRectangle.X + BtSize.Width + 2, Me.ClientRectangle.Y + 1)

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
        'Me.QuickView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
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

        Controls.Add(QuickView)



        BaseListWidth = _ListSize

        ' Send EM_SETMARGINS to prevent text from disappearing underneath the button
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
            ReCalc_Selcted_Items()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub ReCalc_Selcted_Items()
        Try
            Values = ""
            Items = ""
            For i = 0 To QuickView.Rows.Count - 1

                If IsDBNull(QuickView.Rows(i).Cells(2).Value) Then
                    Continue For
                Else
                    If QuickView.Rows(i).Cells(2).Value Then
                        Values += QuickView.Rows(i).Cells(0).Value.ToString & ","
                        Items += QuickView.Rows(i).Cells(1).Value.ToString & ","
                    End If

                End If


            Next

            If Values.Count > 0 Then
                Values = Values.TrimEnd(CChar(","))
                Textt = "--- فلترة ---"
                Text.BackColor = System.Drawing.SystemColors.Info
            Else
                Textt = ""
                Text.BackColor = Color.LightGray
            End If

        Catch ex As Exception
            MsgBox(" ReCalc_Selcted_Items " & ex.Message)
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
            If QuickView.Rows.Count = 0 Then


                C.Da = New SqlClient.SqlDataAdapter("Select " & _ID & "," & _Column & ",0 as 'is_Check' from " & _Table & " Order by " & _OrderByField, C.Con)

                C.Dt = New DataTable
                C.Da.Fill(C.Dt)
                QuickView.DataSource = C.Dt
                If Me.QuickView.Columns(0).Visible = True Then Me.QuickView.Columns(0).Visible = False
                Me.QuickView.Columns(2).Width = Text.Width * 0.2
                ' Me.QuickView.Columns(2).


            End If
        Catch ex As Exception
            MessageBox.Show("يوجد خطأ باتصالك بقاعدة البيانات والخطأ " & vbNewLine & ex.Message)
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
        'Check_Combobox_Filter
        '
        Me.Name = "Check_Combobox_Filter"
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

    Private Sub FSearch_Filter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text.ReadOnly = True

        Dim col1 As New DataGridViewTextBoxColumn
        Dim col2 As New DataGridViewTextBoxColumn
        Dim col3 As New DataGridViewCheckBoxColumn

        col1.DataPropertyName = _ID
        col2.DataPropertyName = _Column
        col3.DataPropertyName = "is_Check"

        col3.ReadOnly = False

        QuickView.Columns.Insert(QuickView.ColumnCount, col1)
        QuickView.Columns.Insert(QuickView.ColumnCount, col2)
        QuickView.Columns.Insert(QuickView.ColumnCount, col3)


    End Sub


    Private Sub Text_MouseHover(sender As Object, e As EventArgs) Handles Text.MouseHover
        Tool_Tip.SetToolTip(Text, "حسب:{" & Items & "}")
    End Sub

End Class


