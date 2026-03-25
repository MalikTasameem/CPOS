Public Class SELECT_GM_FIRST_TIME

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub SELECT_GM_FIRST_TIME_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fetch_GM()
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

    Private Sub UpdateGBButton_Click(sender As Object, e As EventArgs) Handles UpdateGBButton.Click
        If MessageBox.Show(" قد يستغرق هذا بعض الوقت ", "", MessageBoxButtons.OKCancel, _
                          MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.OK Then
            Me.Cursor = Cursors.AppStarting
            IM_FirstTime()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Public Sub IM_FirstTime()

        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\IM_FirstTime.rpt")
        pp.LoadTables()
        '-------------------------
        pp.rp.SetParameterValue(0, USER_NAME)
        pp.rp.SetParameterValue(1, My_Settings.Server_Desc)

        pp.rp.SetParameterValue(2, GM_Serach.SelectedValue)
        pp.rp.SetParameterValue(3, GM_Serach.Text)

        print.CrystalReportViewer1.ReportSource = pp.rp
        print.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_A4
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()

    End Sub
End Class