Imports System.Data.SqlClient

Public Class General_Report
    Dim rs As New Resizer
    Dim IM_DT As New DataTable
    Dim ST As Boolean = False

    Private Sub STORES_Explorer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
    Private Sub STORES_Explorer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.ApplyThemeToForm(Me)
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        GENERAL_REPORT_SELECT()
        'GENERAL_REPORT_SELECT_2()
        'GENERAL_REPORT_SELECT_3()
    End Sub
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Const CS_DROPSHADOW As Integer = &H20000
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property
    Private drag As Boolean
    Private mouseX As Integer
    Private mouseY As Integer

    Private Sub TitleBar_Panel_MouseDown(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseDown, TopTitle_LB.MouseDown
        drag = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub TitleBar_Panel_MouseMove(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseMove, TopTitle_LB.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub

    Private Sub TitleBar_Panel_MouseUp(sender As Object, e As MouseEventArgs) Handles TitleBar_Panel.MouseUp, TopTitle_LB.MouseUp
        drag = False
    End Sub

    Private Sub STORES_Explorer_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub Store_R_Insert()
        Dim c As New C
        Dim sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
        Using (sqlCon)
            Dim sqlComm As New SqlClient.SqlCommand()
            c.Com = New SqlClient.SqlCommand
            sqlComm.Connection = sqlCon
            sqlComm.CommandText = "STORES_R_DELETE"
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlCon.Open()
            Try
                sqlComm.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Using
        sqlCon.Close()
        '***********************************************************************

        For i = 0 To DataGridViewX.Rows.Count - 1
            c = New C
            sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
            Using (sqlCon)
                Dim sqlComm As New SqlClient.SqlCommand()
                c.Com = New SqlClient.SqlCommand
                sqlComm.Connection = sqlCon
                sqlComm.CommandText = "STORES_R_INSERT"
                sqlComm.CommandType = CommandType.StoredProcedure

                sqlComm.Parameters.AddWithValue("@CM_NAME", DataGridViewX.Rows(i).Cells("item_name_CL").Value)
                sqlComm.Parameters.AddWithValue("@QYT", DataGridViewX.Rows(i).Cells("QTY_CL").Value)
                sqlComm.Parameters.AddWithValue("@Unit", DataGridViewX.Rows(i).Cells("Unit_CL").Value)
                sqlComm.Parameters.AddWithValue("@Cost", DataGridViewX.Rows(i).Cells("Cost_CL").Value)
                sqlCon.Open()
                Try
                    sqlComm.ExecuteNonQuery()
                    ' MsgBox("تـــم الحذف ", MsgBoxStyle.Information)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End Using
            sqlCon.Close()
        Next


    End Sub

    Public Sub STORE_R_Print()

        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\General_Report.rpt")
        pp.LoadTables()
        '-------------------------
        With pp
            .rp.SetParameterValue(0, " من " + DateRange.D_F.Value.Date + " إلى " + DateRange.D_T.Value.Date)
            '.rp.SetParameterValue(0, "")
            .rp.SetParameterValue(1, USER_NAME)
            .rp.SetParameterValue(2, My_Settings.Server_Desc)
            .rp.SetParameterValue(3, DateRange.D_F.Value)
            .rp.SetParameterValue(4, DateRange.D_T.Value)

        End With

        print.CrystalReportViewer1.ReportSource = pp.rp
        print.Show()



        Dim pp2 = New ReportConnection
        pp2.rp.Load(Application.StartupPath & "\reports\General_Report_2.rpt")
        pp2.LoadTables()
        '-------------------------
        With pp2
            .rp.SetParameterValue(0, USER_NAME)
            .rp.SetParameterValue(1, My_Settings.Server_Desc)
            .rp.SetParameterValue(2, Total_Balance_txt.Text)
            '.rp.SetParameterValue(3, Tag_lb.Text)
            .rp.SetParameterValue(3, "")
            .rp.SetParameterValue(4, money_char_txtb.Text)
        End With

        Dim P As New print
        P.CrystalReportViewer1.ReportSource = pp2.rp
        P.Show()

    End Sub

    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click
        If DataGridViewX.Rows.Count > 0 Then STORE_R_Print()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        GENERAL_REPORT_SELECT()
        GENERAL_REPORT_SELECT_2()
        GENERAL_REPORT_SELECT_3()
    End Sub

    Public Sub GENERAL_REPORT_SELECT()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[GENERAL_REPORT_SELECT]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", DateRange.D_F.Value)
            .Parameters.AddWithValue("@D_T", DateRange.D_T.Value)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        DataGridViewX.DataSource = C.Dt

    End Sub

    Private Sub DataGridViewX_DataBindingComplete(
    sender As Object,
    e As DataGridViewBindingCompleteEventArgs
) Handles DataGridViewX.DataBindingComplete

        If DataGridViewX.Columns.Count = 0 Then Exit Sub

        DataGridViewX.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        With DataGridViewX.Columns(0)
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 40
            .Resizable = DataGridViewTriState.False
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

        With DataGridViewX.Columns(1)
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 200
            .Resizable = DataGridViewTriState.False
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

        With DataGridViewX.Columns(2)
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 165
            .Resizable = DataGridViewTriState.False
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

        ' DataGridViewX.Columns(3).DefaultCellStyle.Font =
        'New Font(DataGridViewX.Font.FontFamily, 7, FontStyle.Regular)

    End Sub



    Public Sub GENERAL_REPORT_SELECT_2()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[GENERAL_REPORT_SELECT_2]"
            .CommandType = CommandType.StoredProcedure
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        DataGridViewX1.DataSource = C.Dt
    End Sub

    Public Sub GENERAL_REPORT_SELECT_3()

        Dim TOTAL_FIRST_TIME As Double = 0
        Dim TOTAL_LAST_TIME As Double = 0
        Dim Total_Bercent As Double = 0
        Dim TOTAL_DEBIT As Double = 0
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[GENERAL_REPORT_SELECT_3]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@TOTAL_BALANCE", 0)
            .Parameters.AddWithValue("@TOTAL_FIRST_TIME", 0)
            .Parameters.AddWithValue("@TOTAL_LAST_TIME", 0)
            .Parameters.AddWithValue("@TOTAL_DEBIT", 0)
            .Parameters("@TOTAL_BALANCE").Direction = ParameterDirection.Output
            .Parameters("@TOTAL_FIRST_TIME").Direction = ParameterDirection.Output
            .Parameters("@TOTAL_LAST_TIME").Direction = ParameterDirection.Output
            .Parameters("@TOTAL_DEBIT").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) = True Then
                Total_Balance_txt.Text = .Parameters("@TOTAL_BALANCE").Value.ToString()
                TOTAL_FIRST_TIME = .Parameters("@TOTAL_FIRST_TIME").Value.ToString()
                TOTAL_LAST_TIME = .Parameters("@TOTAL_LAST_TIME").Value.ToString()
                TOTAL_DEBIT = .Parameters("@TOTAL_DEBIT").Value.ToString()

                TOTAL_DEBIT *= -1

                Total_Balance_txt.Text = ((.Parameters("@TOTAL_BALANCE").Value) + (TOTAL_DEBIT)).ToString

                If Not String.IsNullOrWhiteSpace(Total_Balance_txt.Text) Then

                    TOTAL_FIRST_TIME = .Parameters("@TOTAL_FIRST_TIME").Value.ToString()
                    TOTAL_LAST_TIME = .Parameters("@TOTAL_LAST_TIME").Value.ToString()

                    Total_Bercent = (Convert.ToDouble(Total_Balance_txt.Text) / TOTAL_FIRST_TIME) * 100


                    'If Convert.ToDouble(Total_Balance_txt.Text) > 0 Then
                    '    Total_Balance_txt.BackColor = Color.LightGreen
                    '    Tag_lb.ForeColor = Color.DarkGreen
                    '    Tag_lb.Text = " ربــح بنسبة " + Total_Bercent.ToString("00.00") + " % "
                    '    Tag_lb.Visible = True
                    'ElseIf Convert.ToDouble(Total_Balance_txt.Text) = 0 Then
                    '    Total_Balance_txt.BackColor = SystemColors.ButtonHighlight
                    '    Tag_lb.Text = ""
                    '    Tag_lb.Visible = False
                    'Else
                    '    Total_Balance_txt.BackColor = Color.IndianRed
                    '    Tag_lb.ForeColor = Color.DarkRed
                    '    Tag_lb.Text = " خســـارة بنسبة " + Total_Bercent.ToString("00.00") + " % "
                    '    Tag_lb.Visible = True
                    'End If
                End If

            End If
        End With

    End Sub





    Private Sub Total_Balance_txt_TextChanged(sender As Object, e As EventArgs) Handles Total_Balance_txt.TextChanged
        money_char_txtb.Text = HANY(Val(Total_Balance_txt.Text), "EGYPT")
    End Sub

    Private Sub HeaderCloseBtn_Click(sender As Object, e As EventArgs) Handles HeaderCloseBtn.Click
        Me.Close()
    End Sub
End Class