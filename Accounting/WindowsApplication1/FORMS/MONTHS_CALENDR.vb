Imports System.Data.SqlClient

Public Class MONTHS_CALENDR


    Private Sub VC_Calender_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_YEARS()
        CHECK_YEAR()
    End Sub

    Private Sub Load_YEARS()
        Dim DT As New DataTable
        'Dim Con = New SqlConnection("Data Source= localhost ;initial catalog= Tree ;Integrated Security=True;")
        Dim C As New C
        Dim da As New SqlClient.SqlDataAdapter(" select YEAR_ID,is_Close  from YEARS ORDER BY YEAR_ID DESC", C.Con)
        da.Fill(DT)

        YEAR_Cm.DataSource = DT
        YEAR_Cm.DisplayMember = "YEAR_ID"
        YEAR_Cm.ValueMember = "is_Close"

        If DT.Rows.Count > 0 Then
            YEAR_Cm.Text = Identifiers.F_YEAR
            Load_MONTHS()
        End If

    End Sub

    Public Sub Load_MONTHS()
        Dim c As New C
        Dim s As String = "select M_ID,M_NAME,M_FROM,M_TO,Status,is_Close from MONTHS_CALENDR_V WHERE YEAR = " & YEAR_Cm.Text & " ORDER BY M_ID ASC"
        Dim com As New SqlDataAdapter(s, c.Con)
        com.Fill(c.Dt)
        DataGridViewX1.DataSource = c.Dt
    End Sub

    Private Sub DataGridViewX1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridViewX1.MouseDoubleClick
        Dim F As New MONTHS_CALENDR_Update
        F.Text = Me.DataGridViewX1.CurrentRow.Cells("M_NAME_CL").Value & " سنة : " & Me.YEAR_Cm.Text
        F.DATETIME_F.Value = Me.DataGridViewX1.CurrentRow.Cells("M_FROM_CL").Value
        F.DATETIME_TO.Value = Me.DataGridViewX1.CurrentRow.Cells("M_TO_CL").Value
        F.is_Close = Me.DataGridViewX1.CurrentRow.Cells("is_Close_CL").Value
        F.YEAR_ = YEAR_Cm.Text
        F.M_ID = Me.DataGridViewX1.CurrentRow.Cells("M_ID_CL").Value
        F_YEAR = YEAR_Cm.Text
        F.ShowDialog()
        Load_MONTHS()
        'MONTHS_CALENDR_Update.ShowDialog()
    End Sub

    Private Sub Back_btn_Click(sender As Object, e As EventArgs) Handles Back_btn.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Create_Calendar_Btn.Click
        Dim inp = InputBox("أدخل السنة المراد التقويم عليها", "حدد السنة")
        If inp <> "" Then
            Dim numericValue As Integer
            If Not Integer.TryParse(inp, numericValue) Then
                MsgBox("خطأ فالإدخال", MsgBoxStyle.Exclamation, "Invalid Input")
                Exit Sub
            End If

            prepare_MONTHS_CALENDR_BY_YEAR(inp)
        End If
    End Sub

    Public Sub prepare_MONTHS_CALENDR_BY_YEAR(YEAR As Integer)

        Dim c = New C
        Using (c.Con)
            Dim sqlComm As New SqlCommand()
            sqlComm.Connection = c.Con
            sqlComm.CommandText = "prepare_MONTHS_CALENDR_BY_YEAR"
            sqlComm.CommandType = CommandType.StoredProcedure

            With sqlComm
                .Parameters.AddWithValue("@Year_INPUT", YEAR)
            End With

            c.Con.Open()
            Try
                sqlComm.ExecuteNonQuery()
                MsgBox("تم جدولة التقويم", MsgBoxStyle.Information)
                Load_MONTHS()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            c.Con.Close()
        End Using


    End Sub

    Private Sub ADD_Btn_Click(sender As Object, e As EventArgs) Handles ADD_Btn.Click
        If check_FOR_OPEN_YEAR() = 0 Then

            Dim inp = InputBox("أدخل السنة الماليــة الجديدة", "فتح سنة")
            If inp <> "" Then

                Dim numericValue As Integer
                If Not Integer.TryParse(inp, numericValue) Then
                    MsgBox("خطأ فالإدخال", MsgBoxStyle.Exclamation, "Invalid Input")
                    Exit Sub
                End If



                'If check_FOUND_YEAR(inp) = True Then
                '    MsgBox("تم إدراج السنــة سابقـــا", MsgBoxStyle.Exclamation, "تكرار الإدخال")
                '    Exit Sub
                'End If

                OPEN_NEW_YEAR(inp)
                'Try
                '    query("INSERT INTO YEARS (YEAR_ID,is_Close) VALUES(" & inp & ",0)")
                '    MsgBox(" تم فتح سنة ماليــة جديدة " & vbNewLine & " ( " & inp & " ) ", MsgBoxStyle.Information, "")
                '    Load_YEARS()
                'Catch ex As Exception
                '    MsgBox(ex.Message)
                'End Try



            End If
        Else
            MsgBox("توجــد سنــة ماليــة مفتوحــة", MsgBoxStyle.Critical, "خطأ")
        End If
    End Sub

    Private Function check_FOR_OPEN_YEAR() As Boolean
        Dim C As New C

        Dim S As String = "select COUNT(YEAR_ID) AS S  from YEARS WHERE is_Close = 0 "
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                C.Dr.Read()
                Return C.Dr("S")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

        Return 0
    End Function


    Private Sub SELECT_ARCHIVE_COUNTER()
        Dim C As New C

        Dim S As String = "select COUNT(T_ID) AS S  from  ACC_BALANCE_MASTER_ARCHIVE WHERE YEAR = " & YEAR_Cm.Text
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                C.Dr.Read()
                ARCHIVE_Label.Text = " عدد القيود المؤرشفة = " & C.Dr("S")

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()


        C = New C
        S = "select COUNT(T_ID) AS S  from  ACC_BALANCE_MASTER WHERE YEAR = " & YEAR_Cm.Text
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows = True Then
                C.Dr.Read()
                'If C.Dr("S") > 0 Then
                ' ARCHIVE_Label.Visible = True
                NONE_ARCHIVE_Label.Text = " عدد القيود غير المؤرشفة = " & C.Dr("S")
                'Else
                '    ARCHIVE_Label.Visible = False
                'End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()


    End Sub




    Private Sub Cloas_Btn_Click(sender As Object, e As EventArgs) Handles Close_Btn.Click

        If MessageBox.Show("سيتم إقفــال السنة الماليــة ( " & YEAR_Cm.Text & " ) ولن يتم إدخال اي معملات عليها بعد الأن .. هل أنت متاكد ", "تاكيــد العملية", MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.OK Then
            Try
                Dim YEAR_TMP = YEAR_Cm.Text
                query("UPDATE YEARS SET is_Close = 1 WHERE YEAR_ID = " & YEAR_Cm.Text)
                query("UPDATE MONTHS_CALENDR SET is_Close = 1 WHERE YEAR = " & YEAR_Cm.Text)
                MsgBox(" تم إقفال السنة الماليــة " & vbNewLine & " ( " & YEAR_Cm.Text & " ) ", MsgBoxStyle.Information, "")
                Load_YEARS()
                Load_MONTHS()
                YEAR_Cm.Text = YEAR_TMP
                CHECK_YEAR()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If
    End Sub

    Private Sub YEAR_Cm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles YEAR_Cm.SelectedIndexChanged
        CHECK_YEAR()
    End Sub

    Private Sub CHECK_YEAR()
        If TypeName(YEAR_Cm.SelectedValue) = "Boolean" Then


            'If YEAR_Cm.SelectedValue = True Then
            '    YEAR_status_Label.Text = "سنـــة مقفلــة"
            '    YEAR_status_Label.BackColor = Color.LightGray
            '    Close_Btn.Enabled = False
            '    Create_Calendar_Btn.Enabled = False
            '    DataGridViewX1.Enabled = False
            '    Close_Btn.Enabled = False
            '    Open_Btn.Enabled = True
            'Else
            '    YEAR_status_Label.Text = "سنـــة مفتوحــة"
            '    YEAR_status_Label.BackColor = Color.PaleGreen
            '    Close_Btn.Enabled = True
            '    Create_Calendar_Btn.Enabled = True
            '    DataGridViewX1.Enabled = True
            '    Close_Btn.Enabled = True
            '    Open_Btn.Enabled = False
            'End If
            Load_MONTHS()
            SELECT_ARCHIVE_COUNTER()
        End If
    End Sub


    Private Sub OPEN_NEW_YEAR(YEAR_ID As Integer)

        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[OPEN_NEW_YEAR]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@YEAR", YEAR_ID)
        End With
        If SQL_SP_EXEC(C.Com) Then
            MsgBox(" تم فتح سنة ماليــة جديدة " & vbNewLine & " ( " & YEAR_ID.ToString & " ) ", MsgBoxStyle.Information, "")
            Load_YEARS()
        End If

    End Sub

    Private Sub Open_Btn_Click(sender As Object, e As EventArgs) Handles Open_Btn.Click

        query("UPDATE YEARS SET is_Close = 1 ")
        If check_FOR_OPEN_YEAR() = 0 Then

            Try
                Dim YEAR_TMP = YEAR_Cm.Text
                query("UPDATE YEARS SET is_Close = 0 WHERE YEAR_ID = " & YEAR_Cm.Text)
                query("UPDATE MONTHS_CALENDR SET is_Close = 0 WHERE YEAR = " & YEAR_Cm.Text)
                MsgBox(" تم فتح السنة الماليــة من جديد " & vbNewLine & " ( " & YEAR_Cm.Text & " ) ", MsgBoxStyle.Information, "")
                Load_YEARS()
                Load_MONTHS()
                YEAR_Cm.Text = YEAR_TMP
                CHECK_YEAR()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        Else
            MsgBox("توجــد سنــة ماليــة مفتوحــة", MsgBoxStyle.Critical, "خطأ")
        End If
    End Sub

    Private Sub Select_Btn_Click(sender As Object, e As EventArgs) Handles Select_Btn.Click
        SELECT_YEAR()
    End Sub

    Private Sub SELECT_YEAR()
        If TypeName(YEAR_Cm.SelectedValue) = "Boolean" Then
            query("UPDATE YEARS SET is_Close = 1 ; UPDATE YEARS SET is_Close = 0 WHERE YEAR_ID = '" & YEAR_Cm.Text & "' ")
            Identifiers.F_YEAR = YEAR_Cm.Text
            Dim notification3 As New NotificationForm("تنويه", " تم إختيار السنة " & YEAR_Cm.Text, "bottom")
            notification3.ShowNotification()
            'MsgBox(" تم إختيار السنة " & YEAR_Cm.Text, MsgBoxStyle.Information, "")
        End If
    End Sub

    Private Sub MOVE_YEAR_TO_ARCHIVE_Btn_Click(sender As Object, e As EventArgs) Handles MOVE_YEAR_TO_ARCHIVE_Btn.Click
        If MessageBox.Show("سيتم إرسال السنة الماليــة ( " & YEAR_Cm.Text & " ) إلى الأرشيف .. هل أنت متاكد ", "تاكيــد العملية", MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.OK Then
            ACC_BALANCE_MOVE_YEAR_TO_ARCHIVE(YEAR_Cm.Text, 1)
        End If

    End Sub


    Public Sub ACC_BALANCE_MOVE_YEAR_TO_ARCHIVE(YEAR As Integer, TYPE As Integer)

        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "[ACC_BALANCE_MOVE_YEAR_TO_ARCHIVE]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@YEAR", YEAR)
            .Parameters.AddWithValue("@TYPE", TYPE)
        End With

        If SQL_SP_EXEC(C.Com) Then
            If TYPE = 1 Then
                Dim notification3 As New NotificationForm("تنويه", " تم إرسال السنة " & YEAR.ToString & "  إلى الأرشيف ", "bottom")
                notification3.ShowNotification()
            ElseIf TYPE = 2 Then
                Dim notification3 As New NotificationForm("تنويه", " تم إسترجاع السنة " & YEAR.ToString & "  من الأرشيف ", "bottom")
                notification3.ShowNotification()
                SELECT_YEAR()
            End If

        End If
    End Sub

    Private Sub RETUTN_YEAR_FROM_ARCHIVE_Btn_Click(sender As Object, e As EventArgs) Handles RETUTN_YEAR_FROM_ARCHIVE_Btn.Click
        If MessageBox.Show("سيتم إسترجاع السنة الماليــة ( " & YEAR_Cm.Text & " ) من الأرشيف .. هل أنت متاكد ", "تاكيــد العملية", MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.OK Then
            ACC_BALANCE_MOVE_YEAR_TO_ARCHIVE(YEAR_Cm.Text, 2)
        End If

    End Sub
End Class