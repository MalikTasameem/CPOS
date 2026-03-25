Imports System.Data.SqlClient
Imports System.Globalization
Public Class TABLES_MV_Report

    Dim rs As New Resizer
    Private Sub Empty_F_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        fetch_ST()
    End Sub

    Private Sub Empty_F_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Public Sub fetch_ST()
        Dim c As New C
        Try
            Dim s As String
            s = "select TB_ID,T_NAME from TABLES ORDER By TB_ID ASC"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            TABLES_cm.DataSource = c.Dt
            TABLES_cm.DisplayMember = "T_NAME"
            TABLES_cm.ValueMember = "TB_ID"
            TABLES_cm.SelectedIndex = -1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IMEx_Search_btn_Click(sender As Object, e As EventArgs) Handles IMEx_Search_btn.Click
        If TABLES_cm.SelectedValue > 0 Then
            IMEX_SelectDetails_By_Date()
        Else
            MsgBox("حدد طاولة", MsgBoxStyle.Exclamation, "")
            TABLES_cm.DroppedDown = True
            TABLES_cm.Select()
        End If
    End Sub

    Private Sub IMEX_SelectDetails_By_Date()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "[SelectTABLES_MV_By_Date]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
            .Parameters.AddWithValue("@TB_ID", TABLES_cm.SelectedValue)
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        IMEx_DV.DataSource = C.Dt
        Calc_IMEx()
    End Sub

    'Private Sub IMEx_DV_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles IMEx_DV.RowsAdded
    '    Calc_IMEx()
    'End Sub

    'Private Sub IMEx_DV_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles IMEx_DV.RowsRemoved
    '    Calc_IMEx()
    'End Sub

    Public Sub Calc_IMEx()
        Dim Code_Num As Integer
        Dim Color_Type As Integer = 0
        Dim QTY As Double = 0
        Dim COST As Double = 0

        If IMEx_DV.Rows.Count > 0 Then Code_Num = IMEx_DV.Rows(0).Cells("TB_ORDER_CODE_CL").Value

        For i = 0 To IMEx_DV.Rows.Count - 1
            QTY = QTY + IMEx_DV.Rows(i).Cells("QTY_CL").Value
            COST = COST + IMEx_DV.Rows(i).Cells("T_Price_CL").Value

            If Code_Num <> IMEx_DV.Rows(i).Cells("TB_ORDER_CODE_CL").Value Then
                Code_Num = IMEx_DV.Rows(i).Cells("TB_ORDER_CODE_CL").Value

                If Color_Type = 0 Then
                    Color_Type = 1
                Else
                    Color_Type = 0
                End If

            End If

            If Color_Type = 0 Then
                IMEx_DV.Rows(i).Cells(0).Style.BackColor = SystemColors.ControlDark
                IMEx_DV.Rows(i).Cells(1).Style.BackColor = SystemColors.ControlDark
                IMEx_DV.Rows(i).Cells(2).Style.BackColor = SystemColors.ControlDark
                IMEx_DV.Rows(i).Cells(3).Style.BackColor = SystemColors.ControlDark
                IMEx_DV.Rows(i).Cells(4).Style.BackColor = SystemColors.ControlDark
                IMEx_DV.Rows(i).Cells(5).Style.BackColor = SystemColors.ControlDark
                IMEx_DV.Rows(i).Cells(6).Style.BackColor = SystemColors.ControlDark
            Else
                IMEx_DV.Rows(i).Cells(0).Style.BackColor = Color.White
                IMEx_DV.Rows(i).Cells(1).Style.BackColor = Color.White
                IMEx_DV.Rows(i).Cells(2).Style.BackColor = Color.White
                IMEx_DV.Rows(i).Cells(3).Style.BackColor = Color.White
                IMEx_DV.Rows(i).Cells(4).Style.BackColor = Color.White
                IMEx_DV.Rows(i).Cells(5).Style.BackColor = Color.White
                IMEx_DV.Rows(i).Cells(6).Style.BackColor = Color.White
            End If

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
            .rp.SetParameterValue(2, MY_Settings.Server_Desc)
            .rp.SetParameterValue(3, HOME.DateRange_Flate.D_F.Value)
            .rp.SetParameterValue(4, HOME.DateRange_Flate.D_T.Value)

            .rp.SetParameterValue(5, IMEx_T_Q_txt.Text)
            .rp.SetParameterValue(6, IMEx_T_M_txt.Text)

            .rp.SetParameterValue(7, TABLES_cm.SelectedValue)
            .rp.SetParameterValue(8, TABLES_cm.Text)


        End With

        Dim p As New print
        p.CrystalReportViewer1.ReportSource = pp.rp
        p.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_80
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()

    End Sub
End Class