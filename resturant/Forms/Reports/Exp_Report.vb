Imports System.Data.SqlClient
Imports System.Globalization
Public Class Exp_Report

    Dim rs As New Resizer
    Private Sub Empty_F_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
    End Sub

    Private Sub Empty_F_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub EXP_SH_btn_Click(sender As Object, e As EventArgs) Handles EXP_SH_btn.Click
        EXP_SelectDetails_By_Date()
    End Sub

    Private Sub EXP_SelectDetails_By_Date()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "[EXP_SelectDetails_By_Date]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        EXP_GridViewX1.DataSource = C.Dt
    End Sub


    Private Sub EXP_GridViewX1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles EXP_GridViewX1.RowsAdded
        EXP_Details()
    End Sub

    Private Sub EXP_GridViewX1_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles EXP_GridViewX1.RowsRemoved
        EXP_Details()
    End Sub

    Public Sub EXP_Details()
        Dim QTY As Double = 0
        Dim COST As Double = 0

        For i = 0 To EXP_GridViewX1.Rows.Count - 1
            QTY = QTY + EXP_GridViewX1.Rows(i).Cells("EXP_QYT").Value
            COST = COST + EXP_GridViewX1.Rows(i).Cells("EXP_TOTAL_CL").Value
        Next
        EXP_TOTAL_QTY_TXT.Text = QTY.ToString("0.00")
        EXP_TOTAL_M_TXT.Text = COST.ToString("n")
    End Sub

    Private Sub EXP_P_btn_Click(sender As Object, e As EventArgs) Handles EXP_P_btn.Click
        Print_Exp_Details()
    End Sub

    Private Sub Print_Exp_Details()
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\EXP_Details_A4.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, " من تاريخ " + HOME.DateRange_Flate.D_F.Value.ToShortDateString + " إلى " + HOME.DateRange_Flate.D_T.Value.ToShortDateString)
            .rp.SetParameterValue(1, USER_NAME)
            .rp.SetParameterValue(2, My_Settings.Server_Desc)
            .rp.SetParameterValue(3, HOME.DateRange_Flate.D_F.Value)
            .rp.SetParameterValue(4, HOME.DateRange_Flate.D_T.Value)

            .rp.SetParameterValue(5, EXP_TOTAL_QTY_TXT.Text)
            .rp.SetParameterValue(6, EXP_TOTAL_M_TXT.Text)
        End With

        Dim p As New print
        p.CrystalReportViewer1.ReportSource = pp.rp
        p.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_80
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()

    End Sub

    Private Sub EXP_GridViewX1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles EXP_GridViewX1.MouseDoubleClick
        If EXP_GridViewX1.RowCount > 0 Then Show_EXP_Rpt_Details.ShowDialog()
    End Sub
End Class