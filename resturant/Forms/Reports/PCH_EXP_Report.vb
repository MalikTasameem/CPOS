Imports System.Data.SqlClient
Imports System.Globalization
Public Class PCH_EXP_Report

    Dim rs As New Resizer
    Private Sub Empty_F_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
    End Sub

    Private Sub Empty_F_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub Pch_ExpSearchBtn_Click(sender As Object, e As EventArgs) Handles Pch_ExpSearchBtn.Click
        PCH_EXP_SelectDetails_By_Date()
    End Sub
    Private Sub PCH_EXP_SelectDetails_By_Date()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "[PCH_EXP_SelectDetails_By_Date]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        Pch_ExpDGV.DataSource = C.Dt
    End Sub


    Private Sub Pch_ExpDGV_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles Pch_ExpDGV.RowsAdded
        Pch_Exp_Calc_Total()
    End Sub

    Private Sub Pch_ExpDGV_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles Pch_ExpDGV.RowsRemoved
        Pch_Exp_Calc_Total()
    End Sub

    Public Sub Pch_Exp_Calc_Total()
        Dim COST As Double = 0
        For i = 0 To Pch_ExpDGV.Rows.Count - 1
            COST = COST + Pch_ExpDGV.Rows(i).Cells("Pch_Exp_Total_CL").Value
        Next
        Pch_Exp_T_txt.Text = COST.ToString("n")
    End Sub

    Private Sub PCH_Exp_Details()
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\Exp_Pch_Details_A4.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, " من تاريخ " + HOME.DateRange_Flate.D_F.Value.ToShortDateString + " إلى " + HOME.DateRange_Flate.D_T.Value.ToShortDateString)
            .rp.SetParameterValue(1, USER_NAME)
            .rp.SetParameterValue(2, My_Settings.Server_Desc)
            .rp.SetParameterValue(3, HOME.DateRange_Flate.D_F.Value)
            .rp.SetParameterValue(4, HOME.DateRange_Flate.D_T.Value)
            .rp.SetParameterValue(5, Pch_Exp_T_txt.Text)
        End With

        Dim p As New print
        p.CrystalReportViewer1.ReportSource = pp.rp
        p.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_80
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()

    End Sub

    Private Sub Pch_ExpPrintBtn_Click(sender As Object, e As EventArgs) Handles Pch_ExpPrintBtn.Click
        PCH_Exp_Details()
    End Sub
End Class