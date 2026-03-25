Public Class Exp_Static
    Dim RS As New Resizer

    Public Dt As New DataTable
    Dim Dv As New DataView
    Private Sub Add_Unit_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Add_Unit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        RS.FindAllControls(Me)
        Exp_Static_SELECT()
        Make_Hints()
    End Sub

    Private Sub Make_Hints()
        SendMessage(Exp_Name_txt.Handle, &H1501, 0, "إبحث عن مصروف")
    End Sub

    Private Sub InsertU_btn_Click(sender As Object, e As EventArgs) Handles InsertU_btn.Click
        Exp_Static_Add.Text = "إضافة مصروف جديد +"
        Exp_Static_Add.SaveSButton.Enabled = True
        Exp_Static_Add.ShowDialog()
    End Sub


    Private Sub Exp_Static_SELECT()
        Dt.Clear()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "[Exp_Static_SELECT]"
            .CommandType = CommandType.StoredProcedure
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(Dt)
        IM_GV.DataSource = Dt
    End Sub


    Private Sub Back_ADD_New_IM_btn_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub


    Private Sub Exp_Static_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        RS.ResizeAllControls(Me)
    End Sub

    Private Sub IM_GV_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles IM_GV.RowsAdded
        CACL()
    End Sub


    Private Sub CACL()
        Dim S As Double = 0, Q As Double = 0
        For i = 0 To IM_GV.Rows.Count - 1
            S += IM_GV.Rows(i).Cells("R_T_Cost_CL").Value
            Q += IM_GV.Rows(i).Cells("QTY_CL").Value
        Next
        T_TextBox.Text = S
        T_QTY_txt.Text = Q
    End Sub
    Private Sub IM_GV_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles IM_GV.RowsRemoved
        CACL()
    End Sub

    Private Sub Exp_Name_txt_TextChanged(sender As Object, e As EventArgs) Handles Exp_Name_txt.TextChanged
        Dv = Dt.DefaultView
        Dv.RowFilter = " EX_NAME Like '%" & sender.Text & "%'"
        IM_GV.DataSource = Dv
    End Sub

    Private Sub IM_GV_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles IM_GV.MouseDoubleClick
        If IM_GV.Rows.Count > 0 Then
            Exp_Static_Add.Text = "تعديل المصروف"
            Exp_Static_Add.is_Select = True
            Exp_Static_Add.EX_NAME = IM_GV.CurrentRow.Cells("EX_NAME_CL").Value
            Exp_Static_Add.ShowDialog()
        End If
    End Sub

    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        PrintData()
    End Sub

    Public Sub PrintData()

        Try
            Dim pp As New ReportConnection
            pp.rp.Load(Application.StartupPath & "\reports\Exp_Static_A4.rpt")
            pp.CrTables = pp.rp.Database.Tables
            For Each CrTable In pp.CrTables
                pp.crtableLogoninfo = CrTable.LogOnInfo
                pp.crtableLogoninfo.ConnectionInfo = pp.crConnectionInfo
                CrTable.ApplyLogOnInfo(pp.crtableLogoninfo)
            Next
            With pp
                .rp.SetParameterValue(0, USER_NAME)
                .rp.SetParameterValue(1, My_Settings.Server_Desc)
                .rp.SetParameterValue(2, T_QTY_txt.Text)
                .rp.SetParameterValue(3, T_TextBox.Text)
            End With

            Dim p As New print
            p.CrystalReportViewer1.ReportSource = pp.rp
            p.Show()

            'pp.rp.PrintOptions.PrinterName = Default_Printer_A4
            'pp.rp.PrintToPrinter(1, False, 0, 0)
            'pp.rp.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class
