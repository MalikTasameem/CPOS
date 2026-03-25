Imports System.Globalization
Imports System.Threading

Public Class CalendarForm
    Public CurrentDate As DateTime = DateTime.Now
    Public date_Shown As Date
    Private TablePanel As TableLayoutPanel
    Private MonthLabel As Label
    Private CancellationSource As CancellationTokenSource
    Public IM_T_ID, IM_ID As Integer
    Public IM_NAME, Rsv_Info As String

    Private Sub CalendarForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = " جدول الحجز : " & IM_NAME
        Me.DoubleBuffered = True ' ✅ تقليل الوميض
        InitializeCalendar()
        UpdateMonthDisplay(CurrentDate)
        'Rsv_Info = ""
    End Sub

    Private Sub InitializeCalendar()
        ' Create a top panel for navigation
        Dim navPanel As New Panel With {
            .Dock = DockStyle.Top,
            .Height = 50
        }

        Dim prevButton As New Button With {
            .Text = "<",
            .Width = 50,
            .Height = 30,
            .FlatStyle = FlatStyle.Flat
        }
        AddHandler prevButton.Click, AddressOf PreviousMonth

        MonthLabel = New Label With {
            .TextAlign = ContentAlignment.MiddleCenter,
            .Font = New Font("Arial", 14, FontStyle.Bold),
            .Dock = DockStyle.Fill
        }

        Dim nextButton As New Button With {
            .Text = ">",
            .Width = 50,
            .Height = 30,
            .FlatStyle = FlatStyle.Flat
        }
        AddHandler nextButton.Click, AddressOf NextMonth

        Dim buttonLayout As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill
        }
        buttonLayout.Controls.Add(prevButton)
        buttonLayout.Controls.Add(MonthLabel)
        buttonLayout.Controls.Add(nextButton)

        navPanel.Controls.Add(buttonLayout)
        Me.Controls.Add(navPanel)

        ' Create TableLayoutPanel for the calendar
        TablePanel = New TableLayoutPanel With {
            .RowCount = 7,
            .ColumnCount = 7,
            .Dock = DockStyle.Fill,
            .CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        }

        For i As Integer = 0 To 6
            TablePanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14.3F))
        Next

        For i As Integer = 0 To 6
            TablePanel.RowStyles.Add(New RowStyle(SizeType.Percent, 14.3F))
        Next

        days_Panel.Controls.Add(TablePanel)
    End Sub

    Private Async Sub UpdateMonthDisplay(dateToDisplay As DateTime)
        ' Cancel any ongoing display updates
        CancellationSource?.Cancel()
        CancellationSource = New CancellationTokenSource()

        ' Update the title with the month and year
        Me.Text = " جدول الحجز : " & IM_NAME & " - " & $"{dateToDisplay:MM/yyyy}"
        MonthLabel.Text = $"{dateToDisplay:MM/yyyy}"
        date_Shown = dateToDisplay

        ' Clear existing content
        TablePanel.Controls.Clear()

        ' Add day headers in Arabic
        Dim daysOfWeekArabic = {"الأحد", "الإثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة", "السبت"}
        For Each day In daysOfWeekArabic
            Dim lbl As New Label With {
                .Text = day,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Dock = DockStyle.Fill,
                .Font = New Font("Arial", 14, FontStyle.Bold)
            }
            TablePanel.Controls.Add(lbl)
        Next

        ' Load days in the background safely on the UI thread
        Await Task.Run(Sub()
                           DisplayMonth(dateToDisplay, CancellationSource.Token)
                       End Sub).ConfigureAwait(False)
    End Sub

    Private Function GetMonthlyReservations(month As DateTime) As HashSet(Of Date)
        Dim reservedDates As New HashSet(Of Date)
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[SB_Rsv_2_Get_MONTHLY_DATES]" ' ✅ استدعاء محسن دفعة واحدة
            .CommandType = CommandType.StoredProcedure
            .Parameters.Clear()
            .Parameters.AddWithValue("@Month", month.Month)
            .Parameters.AddWithValue("@Year", month.Year)
            .Parameters.AddWithValue("@IM_ID", IM_ID)
        End With

        C.Con.Open()
        C.Dr = C.Com.ExecuteReader()
        While C.Dr.Read()
            reservedDates.Add(CDate(C.Dr("ReservedDate")))
        End While
        If C.Con.State = ConnectionState.Open Then C.Con.Close()

        Return reservedDates
    End Function

    Private Sub DisplayMonth(dateToDisplay As DateTime, token As CancellationToken)
        Dim reservedDates = GetMonthlyReservations(dateToDisplay)

        Dim firstDay = New DateTime(dateToDisplay.Year, dateToDisplay.Month, 1)
        Dim daysInMonth = DateTime.DaysInMonth(firstDay.Year, firstDay.Month)
        Dim startDayOfWeek = CInt(firstDay.DayOfWeek)
        Dim currentDay As Integer = 1

        InvokeUI(Sub()
                     TablePanel.SuspendLayout()
                 End Sub)

        For row = 1 To 6
            For col = 0 To 6
                If token.IsCancellationRequested Then Exit Sub

                If row = 1 AndAlso col < startDayOfWeek OrElse currentDay > daysInMonth Then
                    InvokeUI(Sub() TablePanel.Controls.Add(New Label()))
                Else
                    Dim dayButton As New Button With {
                        .Text = currentDay.ToString(),
                        .Dock = DockStyle.Fill,
                        .FlatStyle = FlatStyle.Flat,
                        .BackColor = Color.White,
                        .Font = New Font("Arial", 20, FontStyle.Bold),
                        .Cursor = Cursors.Hand
                    }

                    Dim dayTag = New DateTime(dateToDisplay.Year, dateToDisplay.Month, currentDay)
                    dayButton.Tag = dayTag
                    AddHandler dayButton.Click, AddressOf DayButton_Click

                    If reservedDates.Contains(dayTag.Date) Then
                        dayButton.BackColor = Color.IndianRed
                    End If

                    InvokeUI(Sub() TablePanel.Controls.Add(dayButton))
                    currentDay += 1
                End If
            Next
        Next

        InvokeUI(Sub()
                     TablePanel.ResumeLayout()
                 End Sub)
    End Sub

    Private Sub PreviousMonth(sender As Object, e As EventArgs)
        CurrentDate = CurrentDate.AddMonths(-1)
        UpdateMonthDisplay(CurrentDate)
    End Sub

    Private Sub NextMonth(sender As Object, e As EventArgs)
        CurrentDate = CurrentDate.AddMonths(1)
        UpdateMonthDisplay(CurrentDate)
    End Sub

    Private Sub CalendarForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

        If Identifiers2.is_IM_Use_Rsv = True And CHECK_IF_IM_RSV(IM_ID) = 0 Then
            If MessageBox.Show(" هل تريد إستخدام الصنف " & IM_NAME & " كخدمة حجز في المستقبل ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                      MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                query("UPDATE IM_Menu SET MaxQtyAlert = 1 WHERE IM_ID = " & IM_ID)
            End If
        End If

        CancellationSource?.Cancel()
        Me.Dispose()

    End Sub



    Private Sub DayButton_Click(sender As Object, e As EventArgs)
        Dim selectedDate = CType(DirectCast(sender, Button).Tag, DateTime)
        Dim hourForm As New HourlyCalendarForm(selectedDate)
        hourForm.Show()
    End Sub

    Private Sub InvokeUI(action As Action)
        If Me.InvokeRequired Then
            Me.Invoke(action)
        Else
            action()
        End If
    End Sub
End Class





















'------------------------------------------------------------------------------------------------------------------------------
'Imports System.Globalization
'Imports System.Threading

'Public Class CalendarForm
'    Public CurrentDate As DateTime = DateTime.Now
'    Public date_Shown As Date
'    Private TablePanel As TableLayoutPanel
'    Private MonthLabel As Label
'    Private CancellationSource As CancellationTokenSource
'    Public IM_T_ID, IM_ID As Integer
'    Public IM_NAME As String

'    Private Sub CalendarForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        Me.Text = " جدول الحجز : " & IM_NAME


'        InitializeCalendar()
'        UpdateMonthDisplay(CurrentDate)
'    End Sub

'    Private Sub InitializeCalendar()
'        ' Create a top panel for navigation
'        Dim navPanel As New Panel With {
'            .Dock = DockStyle.Top,
'            .Height = 50
'        }

'        Dim prevButton As New Button With {
'            .Text = "<",
'            .Width = 50,
'            .Height = 30,
'            .FlatStyle = FlatStyle.Flat
'        }
'        AddHandler prevButton.Click, AddressOf PreviousMonth

'        MonthLabel = New Label With {
'            .TextAlign = ContentAlignment.MiddleCenter,
'            .Font = New Font("Arial", 14, FontStyle.Bold),
'            .Dock = DockStyle.Fill
'        }

'        Dim nextButton As New Button With {
'            .Text = ">",
'            .Width = 50,
'            .Height = 30,
'            .FlatStyle = FlatStyle.Flat
'        }
'        AddHandler nextButton.Click, AddressOf NextMonth

'        Dim buttonLayout As New FlowLayoutPanel With {
'            .Dock = DockStyle.Fill
'        }
'        buttonLayout.Controls.Add(prevButton)
'        buttonLayout.Controls.Add(MonthLabel)
'        buttonLayout.Controls.Add(nextButton)

'        navPanel.Controls.Add(buttonLayout)
'        Me.Controls.Add(navPanel)

'        ' Create TableLayoutPanel for the calendar
'        TablePanel = New TableLayoutPanel With {
'            .RowCount = 7,
'            .ColumnCount = 7,
'            .Dock = DockStyle.Fill,
'            .CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
'        }

'        For i As Integer = 0 To 6
'            TablePanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14.3F))
'        Next

'        For i As Integer = 0 To 6
'            TablePanel.RowStyles.Add(New RowStyle(SizeType.Percent, 14.3F))
'        Next

'        days_Panel.Controls.Add(TablePanel)
'    End Sub

'    Private Async Sub UpdateMonthDisplay(dateToDisplay As DateTime)
'        ' Cancel any ongoing display updates
'        CancellationSource?.Cancel()
'        CancellationSource = New CancellationTokenSource()

'        ' Update the title with the month and year
'        Me.Text = " جدول الحجز : " & IM_NAME & " - " & $"{dateToDisplay:MM/yyyy}"
'        MonthLabel.Text = $"{dateToDisplay:MM/yyyy}"
'        date_Shown = dateToDisplay

'        ' Clear existing content
'        TablePanel.Controls.Clear()

'        ' Add day headers in Arabic
'        Dim daysOfWeekArabic = {"الأحد", "الإثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة", "السبت"}
'        For Each day In daysOfWeekArabic
'            Dim lbl As New Label With {
'            .Text = day,
'            .TextAlign = ContentAlignment.MiddleCenter,
'            .Dock = DockStyle.Fill,
'            .Font = New Font("Arial", 14, FontStyle.Bold)
'        }
'            TablePanel.Controls.Add(lbl)
'        Next

'        ' Load days in the background
'        Await Task.Run(Sub() DisplayMonth(dateToDisplay, CancellationSource.Token))
'    End Sub


'    Private Sub DisplayMonth(dateToDisplay As DateTime, token As CancellationToken)
'        Dim reservedDates = GetMonthlyReservations(dateToDisplay)

'        Dim firstDay = New DateTime(dateToDisplay.Year, dateToDisplay.Month, 1)
'        Dim daysInMonth = DateTime.DaysInMonth(firstDay.Year, firstDay.Month)
'        Dim startDayOfWeek = CInt(firstDay.DayOfWeek)
'        Dim currentDay As Integer = 1

'        For row = 1 To 6
'            For col = 0 To 6
'                If token.IsCancellationRequested Then Exit Sub

'                If row = 1 AndAlso col < startDayOfWeek OrElse currentDay > daysInMonth Then
'                    InvokeUI(Sub() TablePanel.Controls.Add(New Label()))
'                Else
'                    Dim dayButton As New Button With {
'                    .Text = currentDay.ToString(),
'                    .Dock = DockStyle.Fill,
'                    .FlatStyle = FlatStyle.Flat,
'                    .BackColor = Color.White,
'                    .Font = New Font("Arial", 20, FontStyle.Bold),
'                    .Cursor = Cursors.Hand
'                }

'                    Dim dayTag = New DateTime(dateToDisplay.Year, dateToDisplay.Month, currentDay)
'                    dayButton.Tag = dayTag
'                    AddHandler dayButton.Click, AddressOf DayButton_Click

'                    ' ✅ تحقق من الحجز في الذاكرة بدلاً من استدعاء قاعدة البيانات
'                    If reservedDates.Contains(dayTag.Date) Then
'                        dayButton.BackColor = Color.IndianRed
'                    End If

'                    InvokeUI(Sub() TablePanel.Controls.Add(dayButton))
'                    currentDay += 1
'                End If
'            Next
'        Next
'    End Sub


'    'Private Sub DisplayMonth(dateToDisplay As DateTime, token As CancellationToken)
'    '    ' Calculate the days in the month
'    '    Dim firstDay = New DateTime(dateToDisplay.Year, dateToDisplay.Month, 1)
'    '    Dim daysInMonth = DateTime.DaysInMonth(firstDay.Year, firstDay.Month)
'    '    Dim startDayOfWeek = CInt(firstDay.DayOfWeek)

'    '    Dim currentDay As Integer = 1

'    '    For row = 1 To 6
'    '        For col = 0 To 6
'    '            If token.IsCancellationRequested Then Exit Sub

'    '            If row = 1 AndAlso col < startDayOfWeek OrElse currentDay > daysInMonth Then
'    '                ' Add empty cell
'    '                InvokeUI(Sub() TablePanel.Controls.Add(New Label()))
'    '            Else
'    '                ' Add button for each day
'    '                Dim dayButton As New Button With {
'    '                    .Text = currentDay.ToString(),
'    '                    .Dock = DockStyle.Fill,
'    '                    .FlatStyle = FlatStyle.Flat,
'    '                    .BackColor = Color.White,
'    '                    .Font = New Font("Arial", 20, FontStyle.Bold),
'    '                    .Cursor = Cursors.Hand
'    '                }

'    '                Dim dayTag = New DateTime(dateToDisplay.Year, dateToDisplay.Month, currentDay)
'    '                dayButton.Tag = dayTag
'    '                AddHandler dayButton.Click, AddressOf DayButton_Click

'    '                ' Check reservation in background
'    '                Task.Run(Sub()
'    '                             If Check_Day_RSV(dayTag) Then
'    '                                 InvokeUI(Sub() dayButton.BackColor = Color.IndianRed)
'    '                             End If
'    '                         End Sub)

'    '                InvokeUI(Sub() TablePanel.Controls.Add(dayButton))
'    '                currentDay += 1
'    '            End If
'    '        Next
'    '    Next
'    'End Sub


'    Private Function GetMonthlyReservations(month As DateTime) As HashSet(Of Date)
'        Dim reservedDates As New HashSet(Of Date)
'        Dim C As New C
'        With C.Com
'            .Connection = C.Con
'            .CommandText = "[SB_Rsv_2_Get_MONTHLY_DATES]" ' قم بإنشاء هذا الـ Stored Procedure لاسترجاع كل التواريخ دفعة واحدة
'            .CommandType = CommandType.StoredProcedure
'            .Parameters.Clear()
'            .Parameters.AddWithValue("@Month", month.Month)
'            .Parameters.AddWithValue("@Year", month.Year)
'            .Parameters.AddWithValue("@IM_ID", IM_ID)
'        End With

'        C.Con.Open()
'        C.Dr = C.Com.ExecuteReader()
'        While C.Dr.Read()
'            reservedDates.Add(CDate(C.Dr("ReservedDate")))
'        End While
'        C.Con.Close()

'        Return reservedDates
'    End Function


'    'Private Function Check_Day_RSV(day As DateTime) As Boolean
'    '    Dim C As New C
'    '    With C.Com
'    '        .Connection = C.Con
'    '        .CommandText = "[SB_Rsv_2_Check_DATE]"
'    '        .CommandType = CommandType.StoredProcedure
'    '        .Parameters.Clear() ' Clear any previous parameters to avoid conflicts
'    '        .Parameters.AddWithValue("@DATE", day)
'    '        .Parameters.AddWithValue("@IM_ID", IM_ID)
'    '    End With

'    '    C.Con.Open()
'    '    C.Dr = C.Com.ExecuteReader()
'    '    ' Check if the reader has any rows
'    '    If C.Dr.HasRows Then
'    '        Return True ' Reservation exists
'    '    End If

'    '    Return False ' No reservation
'    'End Function


'    Private Sub PreviousMonth(sender As Object, e As EventArgs)
'        CurrentDate = CurrentDate.AddMonths(-1)
'        UpdateMonthDisplay(CurrentDate)
'    End Sub

'    Private Sub NextMonth(sender As Object, e As EventArgs)
'        CurrentDate = CurrentDate.AddMonths(1)
'        UpdateMonthDisplay(CurrentDate)
'    End Sub

'    Private Sub CalendarForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
'        CancellationSource?.Cancel()
'        Me.Dispose()
'    End Sub

'    Private Sub DayButton_Click(sender As Object, e As EventArgs)
'        Dim selectedDate = CType(DirectCast(sender, Button).Tag, DateTime)
'        Dim hourForm As New HourlyCalendarForm(selectedDate)
'        hourForm.Show()
'    End Sub

'    Private Sub InvokeUI(action As Action)
'        If Me.InvokeRequired Then
'            Me.Invoke(action)
'        Else
'            action()
'        End If
'    End Sub
'End Class


'-------------------------------------------------------------------------------------------------------------------
'Imports System.Globalization
'Imports System.Threading

'Public Class CalendarForm
'    Public CurrentDate As DateTime = DateTime.Now
'    Public date_Shown As Date
'    Private TablePanel As TableLayoutPanel
'    Private MonthLabel As Label
'    Private CancellationSource As CancellationTokenSource
'    Public IM_T_ID, IM_ID As Integer
'    Public IM_NAME As String

'    Private Sub CalendarForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        Me.Text = " جدول الحجز : " & IM_NAME


'        InitializeCalendar()
'        UpdateMonthDisplay(CurrentDate)
'    End Sub

'    Private Sub InitializeCalendar()
'        ' Create a top panel for navigation
'        Dim navPanel As New Panel With {
'            .Dock = DockStyle.Top,
'            .Height = 50
'        }

'        Dim prevButton As New Button With {
'            .Text = "<",
'            .Width = 50,
'            .Height = 30,
'            .FlatStyle = FlatStyle.Flat
'        }
'        AddHandler prevButton.Click, AddressOf PreviousMonth

'        MonthLabel = New Label With {
'            .TextAlign = ContentAlignment.MiddleCenter,
'            .Font = New Font("Arial", 14, FontStyle.Bold),
'            .Dock = DockStyle.Fill
'        }

'        Dim nextButton As New Button With {
'            .Text = ">",
'            .Width = 50,
'            .Height = 30,
'            .FlatStyle = FlatStyle.Flat
'        }
'        AddHandler nextButton.Click, AddressOf NextMonth

'        Dim buttonLayout As New FlowLayoutPanel With {
'            .Dock = DockStyle.Fill
'        }
'        buttonLayout.Controls.Add(prevButton)
'        buttonLayout.Controls.Add(MonthLabel)
'        buttonLayout.Controls.Add(nextButton)

'        navPanel.Controls.Add(buttonLayout)
'        Me.Controls.Add(navPanel)

'        ' Create TableLayoutPanel for the calendar
'        TablePanel = New TableLayoutPanel With {
'            .RowCount = 7,
'            .ColumnCount = 7,
'            .Dock = DockStyle.Fill,
'            .CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
'        }

'        For i As Integer = 0 To 6
'            TablePanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14.3F))
'        Next

'        For i As Integer = 0 To 6
'            TablePanel.RowStyles.Add(New RowStyle(SizeType.Percent, 14.3F))
'        Next

'        days_Panel.Controls.Add(TablePanel)
'    End Sub

'    Private Async Sub UpdateMonthDisplay(dateToDisplay As DateTime)
'        ' Cancel any ongoing display updates
'        CancellationSource?.Cancel()
'        CancellationSource = New CancellationTokenSource()

'        ' Update the title with the month and year
'        Me.Text = " جدول الحجز : " & IM_NAME & " - " & $"{dateToDisplay:MM/yyyy}"
'        MonthLabel.Text = $"{dateToDisplay:MM/yyyy}"
'        date_Shown = dateToDisplay

'        ' Clear existing content
'        TablePanel.Controls.Clear()

'        ' Add day headers in Arabic
'        Dim daysOfWeekArabic = {"الأحد", "الإثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة", "السبت"}
'        For Each day In daysOfWeekArabic
'            Dim lbl As New Label With {
'            .Text = day,
'            .TextAlign = ContentAlignment.MiddleCenter,
'            .Dock = DockStyle.Fill,
'            .Font = New Font("Arial", 14, FontStyle.Bold)
'        }
'            TablePanel.Controls.Add(lbl)
'        Next

'        ' Load days in the background
'        Await Task.Run(Sub() DisplayMonth(dateToDisplay, CancellationSource.Token))
'    End Sub

'    Private Sub DisplayMonth(dateToDisplay As DateTime, token As CancellationToken)
'        ' Calculate the days in the month
'        Dim firstDay = New DateTime(dateToDisplay.Year, dateToDisplay.Month, 1)
'        Dim daysInMonth = DateTime.DaysInMonth(firstDay.Year, firstDay.Month)
'        Dim startDayOfWeek = CInt(firstDay.DayOfWeek)

'        Dim currentDay As Integer = 1

'        For row = 1 To 6
'            For col = 0 To 6
'                If token.IsCancellationRequested Then Exit Sub

'                If row = 1 AndAlso col < startDayOfWeek OrElse currentDay > daysInMonth Then
'                    ' Add empty cell
'                    InvokeUI(Sub() TablePanel.Controls.Add(New Label()))
'                Else
'                    ' Add button for each day
'                    Dim dayButton As New Button With {
'                        .Text = currentDay.ToString(),
'                        .Dock = DockStyle.Fill,
'                        .FlatStyle = FlatStyle.Flat,
'                        .BackColor = Color.White,
'                        .Font = New Font("Arial", 20, FontStyle.Bold),
'                        .Cursor = Cursors.Hand
'                    }

'                    Dim dayTag = New DateTime(dateToDisplay.Year, dateToDisplay.Month, currentDay)
'                    dayButton.Tag = dayTag
'                    AddHandler dayButton.Click, AddressOf DayButton_Click

'                    ' Check reservation in background
'                    Task.Run(Sub()
'                                 If Check_Day_RSV(dayTag) Then
'                                     InvokeUI(Sub() dayButton.BackColor = Color.IndianRed)
'                                 End If
'                             End Sub)

'                    InvokeUI(Sub() TablePanel.Controls.Add(dayButton))
'                    currentDay += 1
'                End If
'            Next
'        Next
'    End Sub

'    Private Function Check_Day_RSV(day As DateTime) As Boolean
'        Dim C As New C
'        With C.Com
'            .Connection = C.Con
'            .CommandText = "[SB_Rsv_2_Check_DATE]"
'            .CommandType = CommandType.StoredProcedure
'            .Parameters.Clear() ' Clear any previous parameters to avoid conflicts
'            .Parameters.AddWithValue("@DATE", day)
'            .Parameters.AddWithValue("@IM_ID", IM_ID)
'        End With

'        C.Con.Open()
'        C.Dr = C.Com.ExecuteReader()
'        ' Check if the reader has any rows
'        If C.Dr.HasRows Then
'            Return True ' Reservation exists
'        End If

'        Return False ' No reservation
'    End Function


'    Private Sub PreviousMonth(sender As Object, e As EventArgs)
'        CurrentDate = CurrentDate.AddMonths(-1)
'        UpdateMonthDisplay(CurrentDate)
'    End Sub

'    Private Sub NextMonth(sender As Object, e As EventArgs)
'        CurrentDate = CurrentDate.AddMonths(1)
'        UpdateMonthDisplay(CurrentDate)
'    End Sub

'    Private Sub CalendarForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
'        CancellationSource?.Cancel()
'        Me.Dispose()
'    End Sub

'    Private Sub DayButton_Click(sender As Object, e As EventArgs)
'        Dim selectedDate = CType(DirectCast(sender, Button).Tag, DateTime)
'        Dim hourForm As New HourlyCalendarForm(selectedDate)
'        hourForm.Show()
'    End Sub

'    Private Sub InvokeUI(action As Action)
'        If Me.InvokeRequired Then
'            Me.Invoke(action)
'        Else
'            action()
'        End If
'    End Sub
'End Class








