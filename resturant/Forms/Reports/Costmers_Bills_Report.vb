Imports System.Data.SqlClient
Imports System.Globalization
Public Class Costmers_Bills_Report

    Dim rs As New Resizer
    Private Sub Empty_F_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
    End Sub

    Private Sub Empty_F_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub


    Private Sub Agent_Bills_Btn_Click(sender As Object, e As EventArgs) Handles Agent_Bills_Btn.Click
        Agents_Bill_Num_Report_Insert()
    End Sub

    Private Sub Agents_Bill_Num_Report_Insert()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "Agents_Bill_Num_Report_Insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        Costmers_DG.DataSource = C.Dt
    End Sub

    Private Sub Agents_Print_Btn_Click(sender As Object, e As EventArgs) Handles Agents_Print_Btn.Click
        AGENTS_Bills_Num_Print()
    End Sub


    Private Sub AGENTS_Bills_Num_Print()
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\AGENTS_Bills_Num.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, USER_NAME)
            .rp.SetParameterValue(1, " من تاريخ " + HOME.DateRange_Flate.D_F.Value.ToShortDateString + " إلى " + HOME.DateRange_Flate.D_T.Value.ToShortDateString)
            .rp.SetParameterValue(2, My_Settings.Server_Desc)

            '.rp.SetParameterValue(3, HOME.DateRange_Flate.D_F.Value)
            '.rp.SetParameterValue(4, HOME.DateRange_Flate.D_T.Value)
        End With

        Dim p As New print
        p.CrystalReportViewer1.ReportSource = pp.rp
        p.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_80
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()

    End Sub

End Class