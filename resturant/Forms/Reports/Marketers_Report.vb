Imports System.Data.SqlClient
Imports System.Globalization
Public Class Marketers_Report

    Dim rs As New Resizer
    Private Sub Empty_F_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        Load_Marketers()
        fetch_ST()
    End Sub

    Public Sub fetch_ST()
        MK_ST_cm.DataSource = Ge_St_Items()
        MK_ST_cm.DisplayMember = "name"
        MK_ST_cm.ValueMember = "ID"
        MK_ST_cm.SelectedIndex = 0
    End Sub

    Public Sub Load_Marketers()
        Dim c As New C
        Try
            Dim s As String
            s = "select ST_ID,ST_name from Marketers ORDER By ST_ID ASC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            Markter_Cm.DataSource = c.Dt
            Markter_Cm.DisplayMember = "ST_name"
            Markter_Cm.ValueMember = "ST_ID"
            Markter_Cm.SelectedIndex = -1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Empty_F_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub MK_Btn_Click(sender As Object, e As EventArgs) Handles MK_Btn.Click
        If Markter_Cm.SelectedValue <= 0 Then
            MsgBox("حدد المسوق", MsgBoxStyle.Exclamation, "")
            Markter_Cm.DroppedDown = True
            Markter_Cm.Select()
        Else
            MK_SelectDetails_By_Date()
        End If

    End Sub

    Private Sub MK_SelectDetails_By_Date()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "MK_SelectDetails_By_Date"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
            .Parameters.AddWithValue("@ST_ID", MK_ST_cm.SelectedValue)
            .Parameters.AddWithValue("@MK_ID", Markter_Cm.SelectedValue)
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        MK_DataGridViewX.DataSource = C.Dt

        Dim C2 = New C
        With (C2.Com)
            .Connection = C2.Con
            .CommandText = "[MK_SelectDetails_Rtn_By_Date]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
            .Parameters.AddWithValue("@ST_ID", MK_ST_cm.SelectedValue)
            .Parameters.AddWithValue("@MK_ID", Markter_Cm.SelectedValue)
        End With
        C2.Da = New SqlDataAdapter(C2.Com)
        C2.Da.Fill(C2.Dt)
        MK_Rtn_DataGridViewX.DataSource = C2.Dt

        MK_CALC_Details()
    End Sub


    Private Sub MK_DataGridViewX_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles MK_DataGridViewX.RowsAdded
        MK_CALC_Details()
    End Sub

    Private Sub MK_DataGridViewX1_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles MK_DataGridViewX.RowsRemoved
        MK_CALC_Details()
    End Sub

    Public Sub MK_CALC_Details()
        Dim QTY As Double = 0
        Dim COST As Double = 0
        'Dim COST_RTN As Double = 0

        For i = 0 To MK_DataGridViewX.Rows.Count - 1
            QTY = QTY + MK_DataGridViewX.Rows(i).Cells("MK_QTY_CL").Value
            COST = COST + MK_DataGridViewX.Rows(i).Cells("MK_Price_CL").Value
        Next

        For i = 0 To MK_Rtn_DataGridViewX.Rows.Count - 1
            COST = COST - MK_Rtn_DataGridViewX.Rows(i).Cells("MK_Price_Rtn_CL").Value
        Next
        ' COST = COST - COST_RTN

        MK_QTY_T_txt.Text = QTY.ToString("0.00")
        MK_M_T_txt.Text = COST.ToString("n")
    End Sub

    Private Sub MK_Print_btn_Click(sender As Object, e As EventArgs) Handles MK_Print_btn.Click
        Print_MK()
    End Sub

    Private Sub Print_MK()
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\MK_Details_A4.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, " من تاريخ " + HOME.DateRange_Flate.D_F.Value.ToShortDateString + " إلى " + HOME.DateRange_Flate.D_T.Value.ToShortDateString)
            .rp.SetParameterValue(1, USER_NAME)
            .rp.SetParameterValue(2, My_Settings.Server_Desc)
            .rp.SetParameterValue(3, HOME.DateRange_Flate.D_F.Value)
            .rp.SetParameterValue(4, HOME.DateRange_Flate.D_T.Value)

            .rp.SetParameterValue(5, MK_QTY_T_txt.Text)
            .rp.SetParameterValue(6, MK_M_T_txt.Text)

            .rp.SetParameterValue(7, MK_ST_cm.SelectedValue)
            .rp.SetParameterValue(8, MK_ST_cm.Text + vbNewLine + Markter_Cm.Text)
            .rp.SetParameterValue(9, Markter_Cm.SelectedValue)

        End With

        Dim p As New print
        p.CrystalReportViewer1.ReportSource = pp.rp
        p.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_80
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()

    End Sub


    Private Sub MK_Print_Rtn_btn_Click(sender As Object, e As EventArgs) Handles MK_Print_Rtn_btn.Click
        Print_MK_Rtn()
    End Sub
    Private Sub Print_MK_Rtn()
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\MK_Rtn_Details_A4.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, " من تاريخ " + HOME.DateRange_Flate.D_F.Value.ToShortDateString + " إلى " + HOME.DateRange_Flate.D_T.Value.ToShortDateString)
            .rp.SetParameterValue(1, USER_NAME)
            .rp.SetParameterValue(2, My_Settings.Server_Desc)
            .rp.SetParameterValue(3, HOME.DateRange_Flate.D_F.Value)
            .rp.SetParameterValue(4, HOME.DateRange_Flate.D_T.Value)

            .rp.SetParameterValue(5, MK_QTY_T_txt.Text)
            .rp.SetParameterValue(6, MK_M_T_txt.Text)

            .rp.SetParameterValue(7, MK_ST_cm.SelectedValue)
            .rp.SetParameterValue(8, MK_ST_cm.Text + vbNewLine + Markter_Cm.Text)
            .rp.SetParameterValue(9, Markter_Cm.SelectedValue)

        End With

        Dim p As New print
        p.CrystalReportViewer1.ReportSource = pp.rp
        p.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_80
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()

    End Sub
End Class