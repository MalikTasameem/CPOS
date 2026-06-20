Public Class DateRange_Flate

    Public D_F As New DateTimePicker
    Public D_T As New DateTimePicker



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


    '------------------------------------------------------------------------------------------------------------------



    Private Sub MonthCmbo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MonthCmbo.SelectedIndexChanged
        'On Error Resume Next
        'D_From = GetFirstDayOfMonth(D_From, MonthCmbo.Text)
        'D_To = GetLastDayOfMonth(D_To, MonthCmbo.Text)

        If TypeName(MonthCmbo.SelectedValue) = "Integer" Then
            GET_First_and_Last_Days(False)
        End If

    End Sub


    Public Sub GET_First_and_Last_Days(is_All_Time As Boolean)
        Dim C As New C

        Dim S As String = ""
        If is_All_Time = True Then
            S = " SELECT ISNULL(min(M_FROM),getdate()) as M_FROM ,ISNULL(max(M_TO),getdate()) as M_TO from [MONTHS_CALENDR] WHERE YEAR = " & FYear_Txt.Text '& " AND M_ID " = MonthCmbo.SelectedValue
        Else
            S = " SELECT ISNULL([M_FROM],GETDATE()) AS [M_FROM] ,ISNULL([M_TO],GETDATE()) AS [M_TO] from [MONTHS_CALENDR] WHERE YEAR = " & FYear_Txt.Text & " AND M_ID = " & MonthCmbo.SelectedValue.ToString

        End If

        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                C.Dr.Read()
                D_From.Value = C.Dr("M_FROM")
                D_To.Value = C.Dr("M_TO")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()
    End Sub
    'fromDate As Date, 
    Public Sub SetDateRange(toDate As Date, Optional enablePickers_FROM As Boolean = True, Optional enablePickers_TO As Boolean = True)
        ' D_From.Value = fromDate
        D_To.Value = toDate
        D_From.Enabled = enablePickers_FROM
        D_To.Enabled = enablePickers_TO
    End Sub


    'Private Function GetLastDayOfMonth(DateTimePicker_To As DateTimePicker, Month As Integer)

    '    'set return value to the last day of the month for any date passed in to the method create a datetime variable set to the passed in date
    '    DateTimePicker_To.Value = New Date(Now.Year, Month, 1)
    '    Dim dtTo As Date = DateTimePicker_To.Value
    '    'overshoot the date by a month
    '    dtTo = dtTo.AddMonths(1)
    '    'remove all of the days in the next month to get bumped down to the last day of the previous month
    '    dtTo = dtTo.AddDays(-(dtTo.Day))
    '    'return the last day of the month
    '    DateTimePicker_To.Value = dtTo
    '    Return DateTimePicker_To

    'End Function

    'Get the first day of the month
    'Private Function GetFirstDayOfMonth(DateTimePicker As DateTimePicker, Month As Integer)
    '    DateTimePicker.Value = New Date(Now.Year, Month, 1)
    '    Return DateTimePicker
    'End Function

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

    End Sub

    Public Sub Load_MONTHS()
        Dim c As New C
        Dim s As String = "select M_ID,M_NAME from MONTHS_CALENDR WHERE YEAR = " & FYear_Txt.Text & " ORDER BY M_ID ASC"
        Dim com As New SqlClient.SqlDataAdapter(s, c.Con)
        com.Fill(c.Dt)
        MonthCmbo.DataSource = c.Dt
        MonthCmbo.DisplayMember = "M_NAME"
        MonthCmbo.ValueMember = "M_ID"
        If c.Dt.Rows.Count > 0 Then GET_First_and_Last_Days(False)
    End Sub


    Private Sub Equal_Dates()
        D_F.Value = D_From.Value
        D_T.Value = D_To.Value
    End Sub

    Private Sub ALLTime_CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles ALLTime_CheckBox.CheckedChanged
        CB_CHecked(sender)
        If ALLTime_CheckBox.Checked = True Then GET_First_and_Last_Days(True)
        TableLayoutPanel1.Enabled = Not ALLTime_CheckBox.Checked
        MonthCmbo.Enabled = Not ALLTime_CheckBox.Checked
    End Sub

    Private Sub FYear_Txt_TextChanged(sender As Object, e As EventArgs) Handles FYear_Txt.TextChanged
        If FYear_Txt.Text > 0 Then Load_MONTHS()
    End Sub
End Class
