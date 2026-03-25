Imports System.Data.SqlClient
Imports System.Globalization
Public Class SB_Reports
    Dim rs As New Resizer

    Private Sub B_berfet_btn_Click(sender As Object, e As EventArgs) Handles B_berfet_btn.Click
        Bill_Perfet_Select_By_Date()
    End Sub

    Private Sub Bill_Perfet_Select_By_Date()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "Bill_Perfet_Select"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.SelectCommand.CommandTimeout = 0
        C.Da.Fill(C.Dt)
        Bill_Berfet_DGV.DataSource = C.Dt
        B_Berfet_Count()
    End Sub

    Private Sub B_Berfet_Count()
        Dim S As Double = 0, S2 As Double = 0

        For i = 0 To Bill_Berfet_DGV.Rows.Count - 1
            S += Bill_Berfet_DGV.Rows(i).Cells("Bill_Berfet_CL").Value
            S2 += Bill_Berfet_DGV.Rows(i).Cells("Bill_Total_CL").Value
        Next
        B_Berfet_T_txt.Text = S.ToString("N")
        B_BerfetCounter_txt.Text = Bill_Berfet_DGV.Rows.Count.ToString
        B_SB_T_txt.Text = S2.ToString("N")

    End Sub

    Private Sub B_Berfet_Print_btn_Click(sender As Object, e As EventArgs) Handles B_Berfet_Print_btn.Click
        B_Berfet_Print()
    End Sub

    Private Sub B_Berfet_Print()
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\Bill_Berfet_A4.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, " من تاريخ " + HOME.DateRange_Flate.D_F.Value.ToShortDateString + " إلى " + HOME.DateRange_Flate.D_T.Value.ToShortDateString)
            .rp.SetParameterValue(1, USER_NAME)
            .rp.SetParameterValue(2, My_Settings.Server_Desc)
            .rp.SetParameterValue(3, HOME.DateRange_Flate.D_F.Value)
            .rp.SetParameterValue(4, HOME.DateRange_Flate.D_T.Value)

            .rp.SetParameterValue(5, B_BerfetCounter_txt.Text)
            .rp.SetParameterValue(6, B_Berfet_T_txt.Text)

            .rp.SetParameterValue(8, B_SB_T_txt.Text)


            '.rp.SetParameterValue(7, IMEX_ST_cm.SelectedValue)
            '.rp.SetParameterValue(8, IMEX_ST_cm.Text)


        End With

        Dim p As New print
        p.CrystalReportViewer1.ReportSource = pp.rp
        p.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_80
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()

    End Sub

    Private Sub SB_Reports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
    End Sub

    Private Sub SB_Reports_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub
End Class