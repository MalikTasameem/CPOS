Imports System.Data.SqlClient
Imports System.Globalization
Public Class Inside_Report

    Dim rs As New Resizer
    Private Sub Empty_F_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        Order_Type_Cmb.SelectedIndex = 0
        fetch_ST()
    End Sub

    Public Sub fetch_ST()
        InSale_ST_cm.DataSource = Ge_St_Items()
        InSale_ST_cm.DisplayMember = "name"
        InSale_ST_cm.ValueMember = "ID"
        InSale_ST_cm.SelectedIndex = 0
    End Sub

    Private Sub Empty_F_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub InSale_Search_btn_Click(sender As Object, e As EventArgs) Handles InSale_Search_btn.Click
        InSale_SelectDetails_By_Date()
    End Sub

    Private Sub InSale_SelectDetails_By_Date()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            If Order_Type_Cmb.SelectedIndex = 0 Then
                .CommandText = "InSale_SelectDetails_By_Date"
            Else
                .CommandText = "OutSale_SelectDetails_By_Date"
            End If

            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
            .Parameters.AddWithValue("@ST_ID", InSale_ST_cm.SelectedValue)
            .Parameters.AddWithValue("@AG_ID", Emp_FS.TXT_ID.Text)

        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        InSale_DV.DataSource = C.Dt
    End Sub

    Private Sub InSale_print_btn_Click(sender As Object, e As EventArgs) Handles InSale_print_btn.Click
        Print_InSale()
    End Sub

    Private Sub Print_InSale()
        Dim pp As New ReportConnection
        If Order_Type_Cmb.SelectedIndex = 0 Then
            pp.rp.Load(Application.StartupPath & "\reports\InSale_Details_A4.rpt")
        Else
            pp.rp.Load(Application.StartupPath & "\reports\OutSale_Details_A4.rpt")
        End If

        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, " من تاريخ " + HOME.DateRange_Flate.D_F.Value.ToShortDateString + " إلى " + HOME.DateRange_Flate.D_T.Value.ToShortDateString + " \الموظف: " + Emp_FS.Textt)
            .rp.SetParameterValue(1, USER_NAME)
            .rp.SetParameterValue(2, My_Settings.Server_Desc)
            .rp.SetParameterValue(3, HOME.DateRange_Flate.D_F.Value)
            .rp.SetParameterValue(4, HOME.DateRange_Flate.D_T.Value)

            .rp.SetParameterValue(5, InSale_T_Q_txt.Text)
            .rp.SetParameterValue(6, InSale_T_M_txt.Text)

            .rp.SetParameterValue(7, InSale_ST_cm.SelectedValue)
            .rp.SetParameterValue(8, InSale_ST_cm.Text)
            .rp.SetParameterValue(9, Emp_FS.TXT_ID.Text)


        End With

        Dim p As New print
        p.CrystalReportViewer1.ReportSource = pp.rp
        p.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_80
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()

    End Sub

    Private Sub InSale_DV_RowsAdded1(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles InSale_DV.RowsAdded
        Calc_InSale()
    End Sub

    Private Sub InSale_DV_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles InSale_DV.RowsRemoved
        Calc_InSale()
    End Sub

    Public Sub Calc_InSale()
        Dim QTY As Double = 0
        Dim COST As Double = 0

        For i = 0 To InSale_DV.Rows.Count - 1
            QTY = QTY + InSale_DV.Rows(i).Cells("InSale_S_QTY_CL").Value
            COST = COST + InSale_DV.Rows(i).Cells("InSale_S_T_Price_CL").Value
        Next
        InSale_T_Q_txt.Text = QTY.ToString("0.00")
        InSale_T_M_txt.Text = COST.ToString("n")
    End Sub
End Class