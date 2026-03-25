Public Class DateRange_Flate

    Public D_F As New DateTimePicker
    Public D_T As New DateTimePicker

    Private Sub MonthCmbo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MonthCmbo.SelectedIndexChanged
        On Error Resume Next
        D_From = GetFirstDayOfMonth(D_From, MonthCmbo.Text)
        D_To = GetLastDayOfMonth(D_To, MonthCmbo.Text)
    End Sub

    Private Function GetLastDayOfMonth(DateTimePicker_To As DateTimePicker, Month As Integer)

        'set return value to the last day of the month for any date passed in to the method create a datetime variable set to the passed in date
        DateTimePicker_To.Value = New Date(Now.Year, Month, 1)
        Dim dtTo As Date = DateTimePicker_To.Value
        'overshoot the date by a month
        dtTo = dtTo.AddMonths(1)
        'remove all of the days in the next month to get bumped down to the last day of the previous month
        dtTo = dtTo.AddDays(-(dtTo.Day))
        'return the last day of the month
        DateTimePicker_To.Value = dtTo
        Return DateTimePicker_To

    End Function

    'Get the first day of the month
    Private Function GetFirstDayOfMonth(DateTimePicker As DateTimePicker, Month As Integer)
        DateTimePicker.Value = New Date(Now.Year, Month, 1)
        Return DateTimePicker
    End Function

    Private Sub Up_Btn_Click(sender As Object, e As EventArgs) Handles Up_Btn.Click
        Clear_Combo()
        D_From.Value = D_From.Value.AddDays(1)
        D_To.Value = D_From.Value
        Equal_Dates()
    End Sub

    Private Sub Down_Btn_Click(sender As Object, e As EventArgs) Handles Down_Btn.Click
        Clear_Combo()
        D_From.Value = D_From.Value.AddDays(-1)
        D_To.Value = D_From.Value
        Equal_Dates()
    End Sub

    Private Sub Clear_Combo()
        MonthCmbo.SelectedIndex = -1
    End Sub

    Private Sub D_From_ValueChanged(sender As Object, e As EventArgs) Handles D_From.ValueChanged
        Equal_Dates()
    End Sub

    Private Sub D_To_ValueChanged(sender As Object, e As EventArgs) Handles D_To.ValueChanged
        Equal_Dates()
    End Sub

    Private Sub DateRange_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Equal_Dates()
    End Sub

    Private Sub Equal_Dates()
        D_F.Value = D_From.Value
        D_T.Value = D_To.Value
    End Sub
End Class
