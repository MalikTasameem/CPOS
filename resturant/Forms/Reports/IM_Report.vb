Imports System.Data.SqlClient
Imports System.Globalization
Public Class IM_Report
    Dim rs As New Resizer
    Private Sub IM_Serach_btn_Click(sender As Object, e As EventArgs) Handles IM_Serach_btn.Click
        Me.Cursor = Cursors.AppStarting
        FetchIM_Menu()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub FetchIM_Menu()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Select_ToReport"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@GM_ID", GM_Serach.SelectedValue)

        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        IM_GV.DataSource = C.Dt
    End Sub

    Private Sub ButtPrintItems_Click(sender As Object, e As EventArgs) Handles ButtPrintItems.Click
        PrintIM()
    End Sub

    Private Sub PrintIM()
        Dim pp As New ReportConnection
        If IM_Print_Typecmb.SelectedIndex = 0 Then
            pp.rp.Load(Application.StartupPath & "\reports\IM_A4.rpt")
        ElseIf IM_Print_Typecmb.SelectedIndex = 1 Then
            pp.rp.Load(Application.StartupPath & "\reports\IM.rpt")
        Else
            MsgBox("حدد نوع الطباعة", MsgBoxStyle.Exclamation)
        End If

        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, USER_NAME)
            .rp.SetParameterValue(1, MY_Settings.Server_Desc)

            .rp.SetParameterValue(2, " كشف الأصنــاف / " & "التصنيف: " & GM_Serach.Text)
            .rp.SetParameterValue(3, GM_Serach.SelectedValue)
        End With

        Dim p As New print
        p.CrystalReportViewer1.ReportSource = pp.rp
        p.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_80
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()

    End Sub

    Private Sub IM_Report_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        fetch_GM()
        IM_Print_Typecmb.SelectedIndex = 0
    End Sub

    Function GetMailItems() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "-----كل التصنيفات-----"))

        Dim c1 As New C
        Dim s As String = "select GM_Name as 'name' ,GM_ID as 'ID' from General_Menu "
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

    Public Sub fetch_GM()
        GM_Serach.DataSource = GetMailItems()
        GM_Serach.DisplayMember = "name"
        GM_Serach.ValueMember = "ID"
        GM_Serach.SelectedIndex = 0
    End Sub

    Private Sub IM_Report_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub
End Class