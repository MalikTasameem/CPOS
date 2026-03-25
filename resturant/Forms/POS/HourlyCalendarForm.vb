Imports System.Text.RegularExpressions

Public Class HourlyCalendarForm
    Private SelectedDate As DateTime
    'Dim DATE_TIME As DateTime

    Public Sub New(selectedDate As DateTime)
        ' Call the auto-generated InitializeComponent method
        MyBase.New()
        InitializeComponent()

        ' Call your custom initialization method
        InitializeCustomComponents(selectedDate)
    End Sub

    Private Sub InitializeCustomComponents(selectedDate As DateTime)
        Me.SelectedDate = selectedDate
        Me.Text = $"Hourly Calendar - {selectedDate:D}"
        'Me.Size = New Size(400, 500)
        Me.Size = New Size(841, 678)

        ' Create a panel to hold the DateTimePicker and buttons
        Dim mainPanel As New TableLayoutPanel With {
            .RowCount = 2,
            .ColumnCount = 1,
            .Dock = DockStyle.Fill
        }

        ' Add DateTimePicker
        Dim datePicker As New DateTimePicker With {
            .Value = selectedDate,
            .Text = selectedDate,
            .Format = DateTimePickerFormat.Custom,
            .CustomFormat = "yyyy-MM-dd",
            .Enabled = False,
            .Dock = DockStyle.Top,
            .Font = New Font("Arial", 11, FontStyle.Bold)
        }
        mainPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 50))

        ' Create TableLayoutPanel for the 2D hour grid
        Dim tablePanel As New TableLayoutPanel With {
            .RowCount = 6,
            .ColumnCount = 4,
            .Dock = DockStyle.Fill,
            .CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
        }
        mainPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 100))

        ' Set equal column and row sizes for hour grid
        For i As Integer = 0 To 3
            tablePanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25))
        Next
        For i As Integer = 0 To 5
            tablePanel.RowStyles.Add(New RowStyle(SizeType.Percent, 16.67))
        Next

        ' Add buttons for each hour in a grid layout
        Dim hourValue As Integer = 0
        For row As Integer = 0 To 5
            For col As Integer = 0 To 3
                If hourValue < 24 Then
                    Dim hourButton As New Button With {
                        .Text = $"{hourValue:D2}:00",
                        .Dock = DockStyle.Fill,
                        .FlatStyle = FlatStyle.Flat,
                        .BackColor = Color.White,
                        .Font = New Font("Arial", 10),
                        .Cursor = Cursors.Hand,
                        .TextAlign = ContentAlignment.TopCenter
                    }


                    '  hourButton.Tag = Check_Day_RSV( datePicker, hourButton.Text)
                    Check_Day_RSV(hourButton, datePicker, hourButton.Text)



                    If hourButton.Tag <> 0 Then
                        hourButton.BackColor = Color.IndianRed
                        'hourButton.Text &= " ✔"
                        hourButton.Text = " ✔" & vbNewLine & hourButton.Text
                    End If

                    AddHandler hourButton.Click, Sub(sender, e) ToggleCheckMark(sender)
                    'AddHandler hourButton.MouseHover, AddressOf hourButton_MouseHover

                    tablePanel.Controls.Add(hourButton, col, row)
                    hourValue += 1
                End If
            Next
        Next

        ' Add controls to mainPanel
        mainPanel.Controls.Add(datePicker)
        mainPanel.Controls.Add(tablePanel)

        ' Add mainPanel to form
        Me.Controls.Add(mainPanel)
    End Sub

    'Private Sub hourButton_MouseHover(sender As Object, e As EventArgs)
    '    ToolTip1.SetToolTip(sender, SELECT_Day_NOTES_RSV(sender.TAG))
    'End Sub

    Private Function SELECT_Day_NOTES_RSV(RSV_ID)
        Dim NOTE As String = ""
        Dim c As New C
        Try
            Dim s As String
            s = " select ISNULL(NOTES,'') AS NOTES  from SB_Rsv_2 WHERE T_ID = " & RSV_ID
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()

                NOTE = "ملاحظة الحجز: " & "  " & c.Dr("NOTES")
                Return NOTE
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return NOTE

    End Function

    Private Sub Check_Day_RSV(ByRef hourButton As Button, datePicker As DateTimePicker, TIME As String)
        TIME = TIME & ":00"

        If TIME = "01:00:00" Then Dim a = 0

        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[SB_Rsv_2_Check_TIME]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@DATE", datePicker.Value)
            .Parameters.AddWithValue("@TIME", TIME)
            .Parameters.AddWithValue("@IM_ID", CalendarForm.IM_ID)
        End With
        C.Con.Open()
        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            C.Dr.Read()
            hourButton.Tag = C.Dr("T_ID")
            hourButton.Text = hourButton.Text & vbNewLine & C.Dr("NOTES")
        End If
        C.Con.Close()


    End Sub


    'Private Function Check_Day_RSV(datePicker As DateTimePicker, TIME As String)
    '    TIME = TIME & ":00"

    '    If TIME = "01:00:00" Then Dim a = 0

    '    Dim C As New C
    '    With C.Com
    '        .Connection = C.Con
    '        .CommandText = "[SB_Rsv_2_Check_TIME]"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@DATE", datePicker.Value)
    '        .Parameters.AddWithValue("@TIME", TIME)
    '        .Parameters.AddWithValue("@IM_ID", CalendarForm.IM_ID)
    '    End With
    '    C.Con.Open()
    '    C.Dr = C.Com.ExecuteReader
    '    If C.Dr.HasRows Then
    '        C.Dr.Read()
    '        Return C.Dr("T_ID")
    '    End If
    '    C.Con.Close()
    '    Return 0

    'End Function





    ''' <summary>
    ''' Toggles a check mark on the button text when clicked.
    ''' </summary>
    Private Sub ToggleCheckMark(sender As Object)
        Dim button As Button = DirectCast(sender, Button)

        Dim input As String = button.Text
        Dim pattern As String = "([0-2]?[0-9]:[0-5][0-9])" ' Matches HH:mm format
        Dim match As Match = Regex.Match(input, pattern)



        If button.Text.Contains("✔") Then

            If SELECT_RSV_SB_T_ID(button.Tag) = CalendarForm.IM_T_ID Then
                ' Remove the check mark
                'button.Text = button.Text.Replace(" ✔", "")


                button.Text = Match.Value


                button.BackColor = SystemColors.Control
                SB_Rsv_2_Pros(button.Tag, "DELETE", button, "")

            Else
                MsgBox(" لا يمكن إلغاء الحجز من فاتورة أخرى .. قم بالتعديل فالفاتورة الأصلية رقم - " & SELECT_SB_ID_RSV(button.Tag), MsgBoxStyle.Exclamation, "")
            End If

        Else



            Dim inp = InputBox("ادخل ملاحظة للحجز", "بيانات الحجز", CalendarForm.Rsv_Info)

            If inp = "" Then
                    Exit Sub
                Else
                    ' Add the check mark
                    'button.Text &= " ✔"

                    button.Text = " ✔" & vbNewLine & match.Value & vbNewLine & inp
                    button.BackColor = Color.IndianRed

                    SB_Rsv_2_Pros(0, "", button, inp)
                End If

            Identifiers2.is_IM_Use_Rsv = True

        End If
    End Sub

    Private Function SELECT_RSV_SB_T_ID(RSV_ID)

        If RSV_ID IsNot Nothing Then

            Dim c As New C
            Try
                Dim s As String
                s = " select SB_T_ID from SB_Rsv_2 WHERE T_ID = " & RSV_ID
                c.Com = New SqlClient.SqlCommand(s, c.Con)
                c.Con.Open()
                c.Dr = c.Com.ExecuteReader
                If c.Dr.HasRows Then
                    c.Dr.Read()

                    Return c.Dr("SB_T_ID")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If

        Return 0

    End Function


    Private Function SELECT_SB_ID_RSV(SB_CONTENTS_T_ID)

        Dim c As New C
        Try
            Dim s As String
            s = " select SB_ID from Agents_Balance_MV WHERE T_ID = (SELECT Bill_T_ID FROM SB_Contents WHERE T_ID = (SELECT SB_T_ID FROM SB_Rsv_2  WHERE T_ID =  " & SB_CONTENTS_T_ID & ") )"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()

                Return c.Dr("SB_ID")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return 0

    End Function


    Private Sub SB_Rsv_2_Pros(ID As Integer, Process As String, ByRef button As Button, NOTES As String)
        Dim C As New C

        Dim input As String = button.Text
        Dim pattern As String = "([0-2]?[0-9]:[0-5][0-9])" ' Matches HH:mm format
        Dim match As Match = Regex.Match(input, pattern)

        Dim timeValue As String = match.Value

        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Rsv_2_Pros"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", ID)
            .Parameters.AddWithValue("@SB_T_ID", CalendarForm.IM_T_ID)
            .Parameters.AddWithValue("@IM_ID", CalendarForm.IM_ID)
            If Process <> "DELETE" Then .Parameters.AddWithValue("@DATE_F", SelectedDate)
            'Dim S = button.Text.Replace(" ✔", "")
            .Parameters.AddWithValue("@TIME", timeValue)
            .Parameters.AddWithValue("@NOTES", NOTES)
            .Parameters.AddWithValue("@Process", Process)

            '.Parameters("@T_ID").Direction = ParameterDirection.Output
            SQL_SP_EXEC(C.Com)
            'button.Tag = C.Com.Parameters("@T_ID").Value
            'If Process = "DELETE" Then button.Tag = 0
        End With
    End Sub

    Private Sub HourlyCalendarForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub HourlyCalendarForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True ' ✅ تقليل الوميض
    End Sub
End Class


'---------------------------------------------------------------------------------------------------------------------------------------------
'Public Class HourlyCalendarForm
'    Public SelectedDate As DateTime

'    Public Sub New(selectedDate As DateTime)
'        ' Call the auto-generated InitializeComponent method
'        MyBase.New()
'        InitializeComponent()

'        ' Call your custom initialization method
'        InitializeCustomComponents(selectedDate)
'    End Sub

'    Private Sub InitializeCustomComponents(selectedDate As DateTime)
'        Me.Text = $"Hourly Calendar - {selectedDate:D}"
'        Me.Size = New Size(400, 500)

'        ' Create TableLayoutPanel for the 2D hour grid
'        Dim tablePanel As New TableLayoutPanel With {
'            .RowCount = 6,
'            .ColumnCount = 4,
'            .Dock = DockStyle.Fill,
'            .CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
'        }

'        ' Set equal column and row sizes
'        For i As Integer = 0 To 3
'            tablePanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25))
'        Next
'        For i As Integer = 0 To 5
'            tablePanel.RowStyles.Add(New RowStyle(SizeType.Percent, 16.67))
'        Next

'        ' Add buttons for each hour in a grid layout
'        Dim hourValue As Integer = 0
'        For row As Integer = 0 To 5
'            For col As Integer = 0 To 3
'                If hourValue < 24 Then
'                    Dim hourButton As New Button With {
'                        .Text = $"{hourValue:D2}:00",
'                        .Dock = DockStyle.Fill,
'                        .FlatStyle = FlatStyle.Flat,
'                        .BackColor = Color.White,
'                        .Font = New Font("Arial", 10)
'                    }
'                    hourButton.Tag = Check_Day_RSV(CalendarForm.CurrentDate, hourButton.Text)
'                    If hourButton.Tag <> 0 Then
'                        hourButton.BackColor = Color.IndianRed
'                        hourButton.Text &= " ✔"
'                    End If
'                    AddHandler hourButton.Click, Sub(sender, e) ToggleCheckMark(sender)
'                    tablePanel.Controls.Add(hourButton, col, row)
'                    hourValue += 1
'                End If
'            Next
'        Next

'        Me.Controls.Add(tablePanel)
'    End Sub

'    Private Function Check_Day_RSV(dateToDisplay As Date, TIME As String)

'        Dim c As New C
'        Try
'            Dim s As String
'            s = " select T_ID  from SB_Rsv_2 WHERE MONTH(DATE_F) = " & dateToDisplay.Month & " AND YEAR(DATE_F) = " & dateToDisplay.Year & " AND IM_ID = " & CalendarForm.IM_ID & " AND TIME = '" & TIME & "'"
'            c.Com = New SqlClient.SqlCommand(s, c.Con)
'            c.Con.Open()
'            c.Dr = c.Com.ExecuteReader
'            If c.Dr.HasRows Then
'                c.Dr.Read()
'                Return c.Dr("T_ID")
'            End If
'        Catch ex As Exception
'            MsgBox(ex.Message)
'        End Try
'        Return 0

'    End Function





'    ''' <summary>
'    ''' Toggles a check mark on the button text when clicked.
'    ''' </summary>
'    Private Sub ToggleCheckMark(sender As Object)
'        Dim button As Button = DirectCast(sender, Button)
'        If button.Text.Contains("✔") Then
'            ' Remove the check mark
'            button.Text = button.Text.Replace(" ✔", "")
'            button.BackColor = SystemColors.Control
'            SB_Rsv_2_Pros(button.Tag, "DELETE", button)
'        Else
'            ' Add the check mark
'            button.Text &= " ✔"
'            button.BackColor = Color.IndianRed
'            SB_Rsv_2_Pros(0, "", button)
'        End If
'    End Sub


'    Private Sub SB_Rsv_2_Pros(ID As Integer, Process As String, button As Button)
'        Dim C As New C
'        With C.Com
'            .Connection = C.Con
'            .CommandText = "SB_Rsv_2_Pros"
'            .CommandType = CommandType.StoredProcedure
'            .Parameters.AddWithValue("@T_ID", ID)
'            .Parameters.AddWithValue("@SB_T_ID", CalendarForm.IM_T_ID)
'            .Parameters.AddWithValue("@IM_ID", CalendarForm.IM_ID)
'            If Process <> "DELETE" Then .Parameters.AddWithValue("@DATE_F", CalendarForm.CurrentDate)
'            Dim S = button.Text.Replace(" ✔", "")
'            .Parameters.AddWithValue("@TIME", S)

'            '.Parameters.AddWithValue("@DATE_F", Date_T.Value)
'            .Parameters.AddWithValue("@Process", Process)
'            SQL_SP_EXEC(C.Com)




'        End With
'    End Sub



'End Class
