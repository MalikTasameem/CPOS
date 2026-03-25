Imports System.Data.SqlClient
Imports System.Globalization
Public Class Notes_Report

    Dim rs As New Resizer
    Private Sub Empty_F_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
    End Sub

    Private Sub Notes_btn_Click(sender As Object, e As EventArgs) Handles Notes_btn.Click
        NOTES_SelectDetails_By_Date()
    End Sub


    Private Sub NOTES_SelectDetails_By_Date()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "NOTES_SelectDetails_By_Date"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        NOTES_Grid.DataSource = C.Dt
    End Sub


    Private Sub NOTES_Grid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles NOTES_Grid.RowsAdded
        NOTES_Details()
    End Sub

    Private Sub NOTES_Grid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles NOTES_Grid.RowsRemoved
        NOTES_Details()
    End Sub

    Public Sub NOTES_Details()
        Dim QTY As Double = 0
        Dim COST As Double = 0

        For i = 0 To NOTES_Grid.Rows.Count - 1
            QTY = QTY + NOTES_Grid.Rows(i).Cells("COM_QYT_CL").Value
            COST = COST + NOTES_Grid.Rows(i).Cells("COM_TOTAL_CL").Value
        Next
        NOTES_QTY_TXT.Text = QTY.ToString("0.00")
        NOTES_M_TEXT.Text = COST.ToString("n")
    End Sub

    Private Sub NOTES_Print_btn_Click(sender As Object, e As EventArgs) Handles NOTES_Print_btn.Click
        Print_NOTES_Details()
    End Sub

    Private Sub Print_NOTES_Details()
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\NOTES_Details_A4.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, " من تاريخ " + HOME.DateRange_Flate.D_F.Value.ToShortDateString + " إلى " + HOME.DateRange_Flate.D_T.Value.ToShortDateString)
            .rp.SetParameterValue(1, USER_NAME)
            .rp.SetParameterValue(2, My_Settings.Server_Desc)
            .rp.SetParameterValue(3, HOME.DateRange_Flate.D_F.Value)
            .rp.SetParameterValue(4, HOME.DateRange_Flate.D_T.Value)

            .rp.SetParameterValue(5, NOTES_QTY_TXT.Text)
            .rp.SetParameterValue(6, NOTES_M_TEXT.Text)
        End With

        Dim p As New print
        p.CrystalReportViewer1.ReportSource = pp.rp
        p.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_80
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()

    End Sub

    Private Sub Empty_F_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub
End Class