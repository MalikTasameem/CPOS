Imports System.Data.SqlClient
Imports System.Globalization
Public Class ST_Tran_Report
    Dim IMTran_Dt As New DataTable
    Dim rs As New Resizer
    Private Sub Empty_F_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        fetch_ST()
    End Sub

    Public Sub fetch_ST()
        ST_cm.DataSource = GetMailItems()
        ST_cm.DisplayMember = "name"
        ST_cm.ValueMember = "ID"
        ST_cm.SelectedIndex = 0

        ST_To_cm.DataSource = GetMailItems()
        ST_To_cm.DisplayMember = "name"
        ST_To_cm.ValueMember = "ID"
        ST_To_cm.SelectedIndex = 0
    End Sub

    Function GetMailItems() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "------- كل المخازن -------"))

        Dim c1 As New C
        Dim s As String = "select ST_ID AS ID,ST_name AS name from STORES ORDER By ST_ID ASC"
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

    Private Sub Empty_F_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub IMTransButton_Click(sender As Object, e As EventArgs) Handles IMTransButton.Click
        IM_Tran_Fetch()
    End Sub

    Private Sub IM_Tran_Fetch()
        IMTran_Dt.Clear()
        Dim C As New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "Store_Trans_Fetch"
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
            .Parameters.AddWithValue("@ST_F_ID", ST_cm.SelectedValue)
            .Parameters.AddWithValue("@ST_T_ID", ST_To_cm.SelectedValue)
            .CommandType = CommandType.StoredProcedure
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(IMTran_Dt)
        IMTranDataGridViewX.DataSource = IMTran_Dt
    End Sub

    Private Sub IMTranTextBox_TextChanged(sender As Object, e As EventArgs) Handles IMTranTextBox.TextChanged
        On Error Resume Next
        Dim Dv As DataView
        Dv = IMTran_Dt.AsDataView
        Dv.RowFilter = "Item_Name LIKE '%" + sender.Text + "%'"
        IMTranDataGridViewX.DataSource = Dv
    End Sub

    Public Sub IMTranPrintData()

        Try
            Dim pp As New ReportConnection

            pp.rp.Load(Application.StartupPath & "\reports\TranStoreItems.rpt")

            pp.CrTables = pp.rp.Database.Tables

            For Each CrTable In pp.CrTables
                pp.crtableLogoninfo = CrTable.LogOnInfo
                pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
                CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
            Next
            With pp
                .rp.SetParameterValue(0, " من   " & HOME.DateRange_Flate.D_F.Value.Date & " إلى  " & HOME.DateRange_Flate.D_T.Value.Date)
                .rp.SetParameterValue(1, USER_NAME)
                .rp.SetParameterValue(2, HOME.DateRange_Flate.D_F.Value)
                .rp.SetParameterValue(3, HOME.DateRange_Flate.D_T.Value)
                .rp.SetParameterValue(4, My_Settings.Server_Desc)
                .rp.SetParameterValue(5, IMEx_T_Q_txt.Text)

                .rp.SetParameterValue(6, ST_cm.SelectedValue)
                .rp.SetParameterValue(7, ST_To_cm.SelectedValue)
                .rp.SetParameterValue(8, " من : " & ST_To_cm.Text & " إلى : " & ST_To_cm.Text)
                .rp.SetParameterValue(9, IMEx_T_P_txt.Text)
            End With

            Dim p As New print
            p.CrystalReportViewer1.ReportSource = pp.rp
            p.Show()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub IMTranButton_Click(sender As Object, e As EventArgs) Handles IMTranButton.Click
        IMTranPrintData()
    End Sub

    Private Sub IMEx_DV_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles IMTranDataGridViewX.RowsAdded
        Calc_IMEx()
    End Sub

    Private Sub IMEx_DV_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles IMTranDataGridViewX.RowsRemoved
        Calc_IMEx()
    End Sub

    Public Sub Calc_IMEx()
        Dim QTY As Double = 0
        Dim TOTAL As Double = 0
        For i = 0 To IMTranDataGridViewX.Rows.Count - 1
            QTY = QTY + IMTranDataGridViewX.Rows(i).Cells("QTY_CL").Value
            TOTAL = TOTAL + IMTranDataGridViewX.Rows(i).Cells("TOTAL_CL").Value
        Next

        IMEx_T_Q_txt.Text = QTY.ToString("0.00")
        IMEx_T_P_txt.Text = TOTAL.ToString("0.00")
    End Sub
End Class