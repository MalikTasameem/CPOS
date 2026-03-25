Imports System.Data.SqlClient
Imports System.Globalization
Public Class Pch_Report
    Dim rs As New Resizer

    Private Sub Exp_GV_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles Exp_DV.RowsAdded
        Calc_Exp()
    End Sub

    Private Sub Exp_GV_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles Exp_DV.RowsRemoved
        Calc_Exp()
    End Sub

    Public Sub Calc_Exp()
        Dim QTY As Double = 0
        Dim COST As Double = 0

        For i = 0 To Exp_DV.Rows.Count - 1
            QTY = QTY + Exp_DV.Rows(i).Cells("S_QTY_CL").Value
            COST = COST + Exp_DV.Rows(i).Cells("S_T_Price_CL").Value
        Next
        Exp_T_Q_txt.Text = QTY.ToString("0.00")
        Exp_T_M_txt.Text = COST.ToString("n")

    End Sub

    Private Sub Exp_Search_btn_Click(sender As Object, e As EventArgs) Handles Exp_Search_btn.Click
        Exp_Select_Details_By_Date()
    End Sub

    Private Sub Exp_Select_Details_By_Date()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "Pch_SelectDetails_By_Date"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
            .Parameters.AddWithValue("@ST_ID", Pch_ST_cm.SelectedValue)
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        Exp_DV.DataSource = C.Dt
    End Sub

    Private Sub Exp_print_btn_Click(sender As Object, e As EventArgs) Handles Exp_print_btn.Click
        PrintExp()
    End Sub

    Private Sub PrintExp()
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\Pch_Details_A4.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, " من تاريخ " + HOME.DateRange_Flate.D_F.Value.ToShortDateString + " إلى " + HOME.DateRange_Flate.D_T.Value.ToShortDateString)
            .rp.SetParameterValue(1, USER_NAME)
            .rp.SetParameterValue(2, My_Settings.Server_Desc)
            .rp.SetParameterValue(3, HOME.DateRange_Flate.D_F.Value)
            .rp.SetParameterValue(4, HOME.DateRange_Flate.D_T.Value)

            .rp.SetParameterValue(5, Exp_T_Q_txt.Text)
            .rp.SetParameterValue(6, Exp_T_M_txt.Text)

            .rp.SetParameterValue(7, Pch_ST_cm.SelectedValue)
            .rp.SetParameterValue(8, Pch_ST_cm.Text)
        End With

        Dim p As New print
        p.CrystalReportViewer1.ReportSource = pp.rp
        p.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_80
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()

    End Sub

    Private Sub Pch_Report_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        fetch_ST()
    End Sub

    Public Sub fetch_ST()
        Pch_ST_cm.DataSource = Ge_St_Items()
        Pch_ST_cm.DisplayMember = "name"
        Pch_ST_cm.ValueMember = "ID"
        Pch_ST_cm.SelectedIndex = 0
    End Sub

    Private Sub Pch_Report_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub
End Class