Imports System.Data.SqlClient

Public Class IM_Qty_Alert
    Dim IM_DT As New DataTable
    Dim rs As New Resizer

    Public Is_Min As Boolean = False

    Private Sub IM_Valid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        If Is_Min = True Then
            Descrip_Lb.Text = "أصناف ستنتهي كميتها قريبا"
            Descrip_Lb.ForeColor = Color.DarkRed
        Else
            Descrip_Lb.Text = "أصناف وصلت أعلى كمية"
            Descrip_Lb.ForeColor = Color.DarkGray
        End If
        Fill_ALL_IM()
    End Sub

    Public Sub Fill_ALL_IM()
        Dim C As New C
        IM_DT.Clear()
        With (C.Com)
            .Connection = C.Con
            If Is_Min = True Then
                .CommandText = "IM_MinAlert_V_SELECT"
            Else
                .CommandText = "IM_MaxAlert_V_SELECT"
            End If
            .CommandType = CommandType.StoredProcedure
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(IM_DT)
        DataGridViewX.DataSource = IM_DT
        '  Get_Total_QYT()
    End Sub

    'Private Sub Get_Total_QYT()
    '    Dim Cost As Double = 0
    '    Dim Qty As Double = 0
    '    For i = 0 To DataGridViewX.Rows.Count - 1
    '        Cost += DataGridViewX.Rows(i).Cells("Cost_CL").Value
    '        Qty += DataGridViewX.Rows(i).Cells("QTY_CL").Value
    '    Next
    '    TotalQYT_txt.Text = Qty.ToString("N")
    '    TotalCost_txt.Text = Cost.ToString("N")
    'End Sub

    Private Sub CMSearchTextBox_TextChanged(sender As Object, e As EventArgs) Handles CMSearchTextBox.TextChanged
        Dim Dv As DataView
        Dv = IM_DT.AsDataView
        Dv.RowFilter = "item_name LIKE '%" + sender.Text + "%'"
        DataGridViewX.DataSource = Dv

        '  Get_Total_QYT()
    End Sub

    Private Sub IM_Valid_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click
        IM_Valid_Print()
    End Sub

    Public Sub IM_Valid_Print()

        'Dim pp As New ReportConnection
        'pp.rp.Load(Application.StartupPath & "\reports\IM_Valid_A4.rpt")
        'pp.LoadTables()
        ''-------------------------
        'With pp
        '    .rp.SetParameterValue(0, " تاريخ صلاحية قبل " + DateTimePicker_From.Text)
        '    .rp.SetParameterValue(1, USER_NAME)
        '    .rp.SetParameterValue(2, My_Settings.Server_Desc)
        '    .rp.SetParameterValue(3, TotalQYT_txt.Text)
        '    .rp.SetParameterValue(4, TotalCost_txt.Text)
        '    .rp.SetParameterValue(5, DateTimePicker_From.Value)

        'End With

        'Dim p As New print
        'p.CrystalReportViewer1.ReportSource = pp.rp
        'p.Show()

        ''pp.rp.PrintOptions.PrinterName = Default_Printer_80
        ''pp.rp.PrintToPrinter(1, False, 0, 0)
        ''pp.rp.Dispose()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub
End Class