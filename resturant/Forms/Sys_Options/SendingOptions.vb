Imports System.Net.Mail
Imports System.Net
Imports CrystalDecisions.Shared
Imports System.IO


Public Class SendingOptions

    Dim mail As New MailMessage
    Dim File_Name As String = ""
    Dim pp As New ReportConnection

    Private Sub SendingOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Fetch_AG_Email()
        If Type_Of_E_mail = 0 Then '0:Report , 1:Sales , 2:ViewBill
            F_Balances.AG_MV_print_Reset()
            AG_MV_print()
            If F_Balances.isVoid_CB.Checked = True Then Notes_txt.Text = " ( ملاحظة : تم إدراج المعاملات الملغية فالتقرير ) "
            Title_txt.Text = My_Settings.Server_Desc & " \ " & " كشف حساب العملاء "
        ElseIf Type_Of_E_mail = 1 Then
            CashPrint()
            'pp = F_Sales.CashPrint(Sales_BillPage_Bill_Track, Sales_Page_ID)
            Title_txt.Text = My_Settings.Server_Desc & " \ " & " فاتورة مبيعات "
        ElseIf Type_Of_E_mail = 2 Then
            CashPrint2()
            Title_txt.Text = My_Settings.Server_Desc & " \ " & " فاتورة عرض "
        End If

        Preparw_Pdf_File()
        Source_Email_txt.Text = E_mail


    End Sub

    Public Sub CashPrint()

        pp.rp.Load(Application.StartupPath & Sales_BillPage_Bill_Track)
        pp.LoadTables()

        With pp
            .rp.SetParameterValue(0, F_Sales.T_ID)
            .rp.SetParameterValue(1, SBill_Title_1)
            .rp.SetParameterValue(2, SBill_Title_2)
            .rp.SetParameterValue(3, SBill_Footer)
            .rp.SetParameterValue(4, F_Sales.IM_Qty_LB.Text)
            .rp.SetParameterValue(5, F_Sales.IM_Count_LB.Text)
            If Sales_Page_ID <> 2 Then
                .rp.SetParameterValue(6, HANY(Val(F_Sales.Pure), "EGYPT"))
                .rp.SetParameterValue(7, "*" + F_Sales.Barcode + "*")
                .rp.SetParameterValue(8, Notes_txt.Text)

                If SB_is_Check_Thankes = True And (Convert.ToDouble(F_Sales.Piedmoney_txt.Text) = Convert.ToDouble(F_Sales.Pure_txt.Text)) Then
                    .rp.SetParameterValue(9, "خالــــص مع الشكر")
                Else
                    .rp.SetParameterValue(9, "")
                End If

                If F_Sales.Project_cm.SelectedValue <= 1 Then
                    .rp.SetParameterValue(10, "")
                Else
                    .rp.SetParameterValue(10, " الزبون : " + F_Sales.Project_cm.Text)
                End If

                .rp.SetParameterValue(11, F_Sales.Pure - Convert.ToDouble(F_Sales.Piedmoney_txt.Text))
                .rp.SetParameterValue(12, F_Sales.Piedmoney_txt.Text)


                If F_Sales.is_Order_IM_Print = False Then
                    If F_Sales.AG_Show_Balance_CB.Checked = True And F_Sales.AG_ID <> Default_AG_ID Then

                        If Convert.ToDouble(F_Sales.GET_AG_Balance()) < 0 Then
                            .rp.SetParameterValue(13, " باقي عليكم : " & Convert.ToDouble(F_Sales.GET_AG_Balance()))
                        ElseIf Convert.ToDouble(F_Sales.GET_AG_Balance()) > 0 Then
                            .rp.SetParameterValue(13, " باقي لكم : " & Convert.ToDouble(F_Sales.GET_AG_Balance()))
                        Else
                            .rp.SetParameterValue(13, " الرصيد  : " & Convert.ToDouble(F_Sales.GET_AG_Balance()))
                        End If

                    Else
                        .rp.SetParameterValue(13, "")
                    End If
                End If

            Else
                .rp.SetParameterValue(6, "*" + F_Sales.Barcode + "*")
                .rp.SetParameterValue(7, F_Sales.SB_ID)
                .rp.SetParameterValue(8, F_Sales.Pure - Convert.ToDouble(F_Sales.Piedmoney_txt.Text))

                If F_Sales.AG_Show_Balance_CB.Checked = True And F_Sales.AG_ID <> Default_AG_ID Then

                    If Convert.ToDouble(F_Sales.GET_AG_Balance()) < 0 Then
                        .rp.SetParameterValue(9, " باقي عليكم : " & Convert.ToDouble(F_Sales.GET_AG_Balance()))
                    ElseIf Convert.ToDouble(F_Sales.GET_AG_Balance()) > 0 Then
                        .rp.SetParameterValue(9, " باقي لكم : " & Convert.ToDouble(F_Sales.GET_AG_Balance()))
                    Else
                        .rp.SetParameterValue(9, " الرصيد  : " & Convert.ToDouble(F_Sales.GET_AG_Balance()))
                    End If

                    'Else
                    '    .rp.SetParameterValue(13, "")
                End If

            End If


        End With

    End Sub

    Public Sub CashPrint2()

        pp.rp.Load(Application.StartupPath & Sales_ViewPage_Bill_Track)
        pp.LoadTables()

        With pp
            .rp.SetParameterValue(0, F_ViewBill.T_ID)
            .rp.SetParameterValue(1, SBill_Title_1)
            .rp.SetParameterValue(2, SBill_Title_2)
            .rp.SetParameterValue(3, SBill_Footer)
            .rp.SetParameterValue(4, F_ViewBill.IM_Qty_LB.Text)
            .rp.SetParameterValue(5, F_ViewBill.IM_Count_LB.Text)
            .rp.SetParameterValue(6, HANY(Val(F_ViewBill.Pure), "EGYPT"))
            .rp.SetParameterValue(7, "")
            .rp.SetParameterValue(8, Notes_txt.Text)

            If F_ViewBill.Project_cm.SelectedValue = 1 Then
                .rp.SetParameterValue(10, "")
            Else
                .rp.SetParameterValue(10, " الزبون : " + F_ViewBill.Project_cm.Text)
            End If

        End With
    End Sub

    Private Sub Preparw_Pdf_File()
        Dim counter = My.Computer.FileSystem.GetFiles(Application.StartupPath & "\pdf Tmp Files")

        Try
            If counter.Count >= 30 Then
                Dim files() As String
                files = Directory.GetFileSystemEntries(Application.StartupPath & "\pdf Tmp Files")
                For Each element As String In files
                    If (Not Directory.Exists(element)) Then
                        File.Delete(Path.Combine(Application.StartupPath & "\pdf Tmp Files", Path.GetFileName(element)))
                    End If
                Next

            End If
        Catch ex As Exception

        End Try


        Try
            Dim CrExportOptions As ExportOptions
            Dim CrDiskFileDestinationOptions As New  _
            DiskFileDestinationOptions()
            Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()

            File_Name = "crystalExport" & counter.Count.ToString & ".pdf"
            CrDiskFileDestinationOptions.DiskFileName = _
                       Application.StartupPath & "\pdf Tmp Files\" & File_Name
            '
            'crystalExport
            CrExportOptions = pp.rp.ExportOptions
            With CrExportOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .ExportFormatType = ExportFormatType.PortableDocFormat
                .DestinationOptions = CrDiskFileDestinationOptions
                .FormatOptions = CrFormatTypeOptions
            End With
            pp.rp.Export()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub AG_MV_print()

        For i = 0 To F_Balances.AGMVMetroGrid.Rows.Count - 1

            Dim sqlComm As New SqlClient.SqlCommand
            sqlComm.CommandText = "AG_MV_Reports_INSERT"
            sqlComm.CommandType = CommandType.StoredProcedure

            sqlComm.Parameters.AddWithValue("@T_ID", F_Balances.AGMVMetroGrid.Rows(i).Cells("T_ID_CL").Value)
            sqlComm.Parameters.AddWithValue("@AG_Name", F_Balances.AGMVMetroGrid.Rows(i).Cells("AG_Name_CL").Value)
            sqlComm.Parameters.AddWithValue("@Date", F_Balances.AGMVMetroGrid.Rows(i).Cells("Date_CL").Value)
            sqlComm.Parameters.AddWithValue("@Type_Name", F_Balances.AGMVMetroGrid.Rows(i).Cells("Type_Name_CL").Value)
            sqlComm.Parameters.AddWithValue("@Title", F_Balances.AGMVMetroGrid.Rows(i).Cells("Receipt_Title_CL").Value)
            sqlComm.Parameters.AddWithValue("@Debit", F_Balances.AGMVMetroGrid.Rows(i).Cells("Debit_CL").Value)
            sqlComm.Parameters.AddWithValue("@Credit", F_Balances.AGMVMetroGrid.Rows(i).Cells("Credit_CL").Value)
            sqlComm.Parameters.AddWithValue("@Balance", F_Balances.AGMVMetroGrid.Rows(i).Cells("Balance_CL").Value)
            sqlComm.Parameters.AddWithValue("@User_Name", F_Balances.AGMVMetroGrid.Rows(i).Cells("UserEntry").Value)

            SQL_SP_EXEC(sqlComm)

        Next

        Try
            Dim p As New print
            pp.rp.Load(Application.StartupPath & "\reports\AG_MV.rpt")
            pp.CrTables = pp.rp.Database.Tables
            For Each CrTable In pp.CrTables
                pp.crtableLogoninfo = CrTable.LogOnInfo
                pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
                CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
            Next

            With pp
                .rp.SetParameterValue(0, USER_NAME)
                .rp.SetParameterValue(1, F_MainForm.Serv_Label.Text)
                .rp.SetParameterValue(2, F_Balances.AGMV_Name)
                .rp.SetParameterValue(3, F_Balances.AGMV_Type)
                .rp.SetParameterValue(4, F_Balances.AGMV_Date)
                .rp.SetParameterValue(5, F_Balances.AGMV_User)

                .rp.SetParameterValue(6, F_Balances.Total_Debit.Text)
                .rp.SetParameterValue(7, F_Balances.Total_Credit.Text)
                .rp.SetParameterValue(8, F_Balances.Total_Balance.Text)

                .rp.SetParameterValue(9, F_Balances.NUM_DEBIT_TXT.Text)
                .rp.SetParameterValue(10, F_Balances.NUM_CREDIT_TXT.Text)

            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub Fetch_AG_Email()
        If Type_Of_E_mail = 0 Then
            If F_Balances.AG_ID > 0 Then
                Dim C As New C
                Dim S As String = "Select E_mail From AGENTS_MENU_V Where AG_ID = '" & F_Balances.AG_ID & "'"
                C.Com = New SqlClient.SqlCommand(S, C.Con)
                C.Con.Open()
                Try
                    C.Dr = C.Com.ExecuteReader
                    If C.Dr.HasRows Then
                        C.Dr.Read()
                        To_Email_txt.Text = C.Dr("E_mail")
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                C.Con.Close()
            End If
        ElseIf Type_Of_E_mail = 1 Then
            Dim C As New C
            Dim S As String = "Select E_mail From AGENTS_MENU_V Where AG_ID = '" & F_Sales.AG_ID & "'"
            C.Com = New SqlClient.SqlCommand(S, C.Con)
            C.Con.Open()
            Try
                C.Dr = C.Com.ExecuteReader
                If C.Dr.HasRows Then
                    C.Dr.Read()
                    To_Email_txt.Text = C.Dr("E_mail")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            C.Con.Close()

        ElseIf Type_Of_E_mail = 2 Then
            Dim C As New C
            Dim S As String = "Select E_mail From AGENTS_MENU_V Where AG_ID = '" & F_ViewBill.AG_ID & "'"
            C.Com = New SqlClient.SqlCommand(S, C.Con)
            C.Con.Open()
            Try
                C.Dr = C.Com.ExecuteReader
                If C.Dr.HasRows Then
                    C.Dr.Read()
                    To_Email_txt.Text = C.Dr("E_mail")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            C.Con.Close()
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Me.Cursor = Cursors.AppStarting
            Dim smtpServer As New SmtpClient()
            smtpServer.Credentials = New Net.NetworkCredential(E_mail, E_mail_Password)
            smtpServer.Port = E_mail_Port
            smtpServer.Host = E_mail_Host
            smtpServer.EnableSsl = True
            mail.From = New MailAddress(E_mail)
            mail.To.Add(To_Email_txt.Text)
            mail.Subject = Title_txt.Text
            mail.Body = Notes_txt.Text()
            Dim fileTXT As String = Application.StartupPath & "\pdf Tmp Files\" & File_Name
            Dim data As Net.Mail.Attachment = New Net.Mail.Attachment(fileTXT)
            data.Name = "Report.pdf"
            mail.Attachments.Add(data)
            smtpServer.Send(mail)
            Me.Cursor = Cursors.Default
            MsgBox("تم الإرســال بنجاح", MsgBoxStyle.Information, "")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message)
            MsgBox("فشل فالإرسال", MsgBoxStyle.Critical, "")
        End Try
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        To_Email_txt.Enabled = True
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        To_Email_txt.Enabled = True
    End Sub

    Public Function HaveInternetConnection() As Boolean
        Try
            Return My.Computer.Network.Ping("www.google.com")
        Catch
            Return False
        End Try
    End Function


    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub SendingOptions_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub
End Class