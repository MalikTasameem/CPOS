Imports System.Data.SqlClient

Public Class IM_Valid
    Dim IM_DT As New DataTable
    Dim rs As New Resizer
    Private Sub IM_Valid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        DateTimePicker_From.Value = DateTimePicker_From.Value.AddDays(IM_Day_Valid)
        Fill_ALL_IM()
        Check_View_Control()
        Make_Hints()
    End Sub

    Private Sub Check_View_Control()
        ' Me.DataGridViewX.Columns("GM_Name_CL").Visible = My_Settings.ST_GM_Name
        Me.DataGridViewX.Columns("ST_Name_CL").Visible = My_Settings.ST_STNAME
        '  Me.DataGridViewX.Columns("Last_Pch_Price").Visible = My_Settings.ST_Last_Pch_Price
        '   Me.DataGridViewX.Columns("D_Valid_CL").Visible = My_Settings.ST_Valid
        Me.DataGridViewX.Columns("IM_Num_CL").Visible = My_Settings.ST_IM_Num
    End Sub

    Private Sub Make_Hints()
        SendMessage(CMSearchTextBox.Handle, &H1501, 0, "إبحث عن صنف")
    End Sub

    Public Sub Fill_ALL_IM()
        Dim C As New C
        IM_DT.Clear()
        With (C.Com)
            .Connection = C.Con
            .CommandText = "IM_Valid_V_SELECT"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@END_Date_Valid", Date.Now.Date.AddDays(IM_Day_Valid))
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(IM_DT)
        DataGridViewX.DataSource = IM_DT
        Get_Total_QYT()
    End Sub



    Private Sub Get_Total_QYT()
        Dim Cost As Double = 0
        Dim Qty As Double = 0
        Dim DATE1 As Date
        For i = 0 To DataGridViewX.Rows.Count - 1
            Cost += DataGridViewX.Rows(i).Cells("Cost_CL").Value
            Qty += DataGridViewX.Rows(i).Cells("QTY_CL").Value
            DATE1 = DataGridViewX.Rows(i).Cells("D_Valid_CL").Value
            If Date.Now.Date >= DATE1 Then
                DataGridViewX.Rows(i).DefaultCellStyle.BackColor = Color.IndianRed
                DataGridViewX.Rows(i).Cells("Case_CL").Value = "صلاحية منتهية"
            Else
                Dim DaysStayed As Int32 = DATE1.Subtract(Date.Now.Date).Days

                If DaysStayed < 10 Then
                    DataGridViewX.Rows(i).DefaultCellStyle.BackColor = Color.Orange
                Else
                    DataGridViewX.Rows(i).DefaultCellStyle.BackColor = Color.Yellow
                End If

                DataGridViewX.Rows(i).Cells("Case_CL").Value = " ينتهي بعد " + DaysStayed.ToString + " يوم "
            End If
        Next
        TotalQYT_txt.Text = Qty.ToString("N")
        TotalCost_txt.Text = Cost.ToString("N")
    End Sub

    Private Sub CMSearchTextBox_TextChanged(sender As Object, e As EventArgs) Handles CMSearchTextBox.TextChanged
        Dim Dv As DataView
        Dv = IM_DT.AsDataView
        Dv.RowFilter = "item_name LIKE '%" + sender.Text + "%'"
        DataGridViewX.DataSource = Dv

        Get_Total_QYT()
    End Sub

    Private Sub IM_Valid_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click
        IM_Valid_Print()
    End Sub

    Public Sub IM_Valid_Print()

        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\IM_Valid_A4.rpt")
        pp.LoadTables()
        '-------------------------
        With pp
            .rp.SetParameterValue(0, " تاريخ صلاحية قبل " + DateTimePicker_From.Text)
            .rp.SetParameterValue(1, USER_NAME)
            .rp.SetParameterValue(2, My_Settings.Server_Desc)
            .rp.SetParameterValue(3, TotalQYT_txt.Text)
            .rp.SetParameterValue(4, TotalCost_txt.Text)
            .rp.SetParameterValue(5, DateTimePicker_From.Value)

        End With

        Dim p As New print
        p.CrystalReportViewer1.ReportSource = pp.rp
        p.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_80
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub DGV_Control_btn_Click(sender As Object, e As EventArgs) Handles DGV_Control_btn.Click
        Dim F As New DGV_Control
        F.TabControl1.TabPages.Remove(F.OtherTabPage)
        F.Show()
    End Sub
End Class