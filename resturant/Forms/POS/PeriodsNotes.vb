Public Class PeriodsNotes

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Start_PrButton_Click(sender As Object, e As EventArgs) Handles Start_PrButton.Click
        If F_Periods.Pr_Status = True Then
            Insert_Period()
        Else
            LOADING_PANEL.Visible = True
            Me.Cursor = Cursors.WaitCursor
            End_Period()
            Me.Cursor = Cursors.Default
            LOADING_PANEL.Visible = False
        End If

    End Sub

    Private Sub End_Period()

        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "Periods_END"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Pr_ID", F_Periods.Pr_Grid.CurrentRow.Cells("Pr_ID_CL").Value)
            .Parameters.AddWithValue("@Pr_End", Date.Now)
            If String.IsNullOrWhiteSpace(NotesTextBox.Text) = False Then .Parameters.AddWithValue("@NotesOn_End", NotesTextBox.Text)
        End With

        If SQL_SP_EXEC(C.Com) Then
            If User_AutoPrintSB = True Then
                F_SalesReport = New POS_Report
                F_SalesReport.Pr_Auto_Print = True
                F_SalesReport.Show()
            End If

            Pr_ID = 0
            F_Periods.Load_Open_Periods()

            'F_Periods.Check_For_OpenPierod()
            If Check_For_OpenPierod() = 1 Then
                F_Periods.Start_PrButton.Enabled = False
                F_Periods.End_PrButton.Enabled = True
            Else
                F_Periods.Start_PrButton.Enabled = True
                F_Periods.End_PrButton.Enabled = False
            End If

            F_Periods.End_PrButton.Enabled = False
            F_Periods.Start_PrButton.Enabled = True
            'F_MainForm.F_Load()
            MsgBox("تم الإنهـــاء", MsgBoxStyle.Information)
            F_Periods.Close()
            Me.Close()
        End If

    End Sub

    Private Sub Insert_Period()

        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "Periods_insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Pr_ID", 0)
            .Parameters.AddWithValue("@User_ID", USER_ID)
            If String.IsNullOrWhiteSpace(NotesTextBox.Text) = False Then .Parameters.AddWithValue("@NotesOn_Start", NotesTextBox.Text)
            If S_Tables = True Then .Parameters.AddWithValue("@TB_Flate_ID", Flate_cmb.SelectedValue)
            .Parameters("@Pr_ID").Direction = ParameterDirection.Output
        End With

        If SQL_SP_EXEC(C.Com) Then
            'F_MainForm.F_Load()
            MsgBox("تم البدء", MsgBoxStyle.Information)
            Pr_ID = C.Com.Parameters("@Pr_ID").Value.ToString()
            F_Periods.Load_Open_Periods()

            'F_Periods.Check_For_OpenPierod()
            If Check_For_OpenPierod() = 1 Then
                F_Periods.Start_PrButton.Enabled = False
                F_Periods.End_PrButton.Enabled = True
            Else
                F_Periods.Start_PrButton.Enabled = True
                F_Periods.End_PrButton.Enabled = False
            End If

            F_Periods.End_PrButton.Enabled = True
            Tables_Flate_ID = Flate_cmb.SelectedValue
            Save_AppSetting()
            F_Periods.Close()
            Me.Close()
        End If

    End Sub

    Private Sub PeriodsNotes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        If S_Tables = False Then
            TB_Panel.Visible = False
        Else
            fetch_Flate()
        End If

        Flate_cmb.SelectedValue = Tables_Flate_ID

        If F_Periods.Pr_Status = True Then
            Start_PrButton.Text = "بـدء الوردية ENTER"
        Else
            Start_PrButton.Text = "إغلاق الوردية ENTER"
            If S_Tables = True Then Flate_cmb.SelectedValue = U_Flate_ID
            Flate_cmb.Enabled = False
        End If

        Start_PrButton.Select()
    End Sub

    Function GetMailItems() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "----- كل الأماكن -----"))

        Dim c1 As New C
        Dim s As String = "select Flate_ID AS ID,Flate_Name AS name from Tables_Flate ORDER By Flate_ID ASC"
        c1.Com = New SqlClient.SqlCommand(s, c1.Con)
        c1.Con.Open()
        Try
            c1.Dr = c1.Com.ExecuteReader
            If c1.Dr.HasRows Then
                While c1.Dr.Read
                    mailItems.Add(New MailItem(c1.Dr("ID"), c1.Dr("name")))
                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
        Return mailItems

    End Function

    Public Sub fetch_Flate()
        Flate_cmb.DataSource = GetMailItems()
        Flate_cmb.DisplayMember = "name"
        Flate_cmb.ValueMember = "ID"
        Flate_cmb.SelectedIndex = 0
    End Sub

    Private Sub PeriodsNotes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub PeriodsNotes_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Return Then Start_PrButton_Click(sender, e)
    End Sub
End Class