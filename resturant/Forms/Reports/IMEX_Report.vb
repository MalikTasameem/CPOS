Imports System.Data.SqlClient
Imports System.Globalization
Public Class IMEX_Report

    Dim rs As New Resizer
    Private Sub Empty_F_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        fetch_ST()
    End Sub

    Private Sub Empty_F_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Public Sub fetch_ST()
        IMEX_ST_cm.DataSource = Ge_St_Items()
        IMEX_ST_cm.DisplayMember = "name"
        IMEX_ST_cm.ValueMember = "ID"
        IMEX_ST_cm.SelectedIndex = 0
    End Sub

    Private Sub IMEx_Search_btn_Click(sender As Object, e As EventArgs) Handles IMEx_Search_btn.Click
        IMEX_SelectDetails_By_Date()
    End Sub

    Private Sub IMEX_SelectDetails_By_Date()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "IMEX_SelectDetails_By_Date"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
            .Parameters.AddWithValue("@ST_ID", IMEX_ST_cm.SelectedValue)
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        IMEx_DV.DataSource = C.Dt
    End Sub

    Private Sub IMEx_DV_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles IMEx_DV.RowsAdded
        Calc_IMEx()
    End Sub

    Private Sub IMEx_DV_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles IMEx_DV.RowsRemoved
        Calc_IMEx()
    End Sub

    Public Sub Calc_IMEx()
        Dim QTY As Double = 0
        Dim COST As Double = 0

        For i = 0 To IMEx_DV.Rows.Count - 1
            QTY = QTY + IMEx_DV.Rows(i).Cells("IMEX_S_QTY_CL").Value
            COST = COST + IMEx_DV.Rows(i).Cells("IMEX_S_T_Price_CL").Value
        Next
        IMEx_T_Q_txt.Text = QTY.ToString("0.00")
        IMEx_T_M_txt.Text = COST.ToString("n")
    End Sub

    Private Sub IMEx_print_btn_Click(sender As Object, e As EventArgs) Handles IMEx_print_btn.Click
        Print_IMEx()
    End Sub

    Private Sub Print_IMEx()
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\IMEX_Details_A4.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, " من تاريخ " + HOME.DateRange_Flate.D_F.Value.ToShortDateString + " إلى " + HOME.DateRange_Flate.D_T.Value.ToShortDateString)
            .rp.SetParameterValue(1, USER_NAME)
            .rp.SetParameterValue(2, My_Settings.Server_Desc)
            .rp.SetParameterValue(3, HOME.DateRange_Flate.D_F.Value)
            .rp.SetParameterValue(4, HOME.DateRange_Flate.D_T.Value)

            .rp.SetParameterValue(5, IMEx_T_Q_txt.Text)
            .rp.SetParameterValue(6, IMEx_T_M_txt.Text)

            .rp.SetParameterValue(7, IMEX_ST_cm.SelectedValue)
            .rp.SetParameterValue(8, IMEX_ST_cm.Text)


        End With

        Dim p As New print
        p.CrystalReportViewer1.ReportSource = pp.rp
        p.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_80
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()

    End Sub
End Class