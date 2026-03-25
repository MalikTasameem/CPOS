Imports System.Data.SqlClient
Imports System.Globalization
Public Class AG_MV
    Dim IM_MV_Dt As New DataTable

    Dim AG_ID As Integer
    Dim AG_DT As New DataTable

    Dim rs As New Resizer
    Private Sub Empty_F_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        Fetch_AgentBalance_Type()
        fetch_GM()
    End Sub


    Public Sub fetch_GM()
        AG_MV_GM_CM.DataSource = GetMailItems()
        AG_MV_GM_CM.DisplayMember = "name"
        AG_MV_GM_CM.ValueMember = "ID"
        AG_MV_GM_CM.SelectedIndex = 0

    End Sub


    Private Sub Empty_F_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub Fetch_AgentBalance_Type()
        Dim C As New C
        Try
            Dim sql As String = "Select id , Type_Name from AgentBalance_Type WHERE ID IN(1,7,10,11) Order By id ASC"
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            IMMV_Type_cm.DataSource = C.Dt
            IMMV_Type_cm.DisplayMember = "Type_Name"
            IMMV_Type_cm.ValueMember = "id"
            IMMV_Type_cm.SelectedValue = 1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    ''----------------------------------------------------------------------------------------------------------------------------------------
    Private Sub IMMV_Search_btn_Click(sender As Object, e As EventArgs) Handles IMMV_Search_btn.Click
        'If String.IsNullOrWhiteSpace(IM_SH_txt.Text) Then AG_ID = Default_AG_ID
        IM_MV_V_Select_By_Date()
    End Sub

    Private Sub IM_MV_V_Select_By_Date()

        IM_MV_Dt.Clear()

        Dim C As New C
        With (C.Com)
            .Connection = C.Con
            If Filter_By_Date_CB.Checked = False Then
                .CommandText = "[IM_MV_V_Select_By_Date]"
                IMMV_DV.Columns("IMMV_DATE_CL").Visible = False
            Else
                .CommandText = "[IM_MV_V_Select_By_Date_WITH_Date_Aggregate]"
                IMMV_DV.Columns("IMMV_DATE_CL").Visible = True
            End If

            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
            .Parameters.AddWithValue("@BsType_ID", IMMV_Type_cm.SelectedValue)
            .Parameters.AddWithValue("@AG_ID", AG_Cm.TXT_ID.Text)
            .Parameters.AddWithValue("@By_GM", 0)
            .Parameters.AddWithValue("@GM_ID", AG_MV_GM_CM.SelectedValue)
            .Parameters.AddWithValue("@ALL_AG", ALL_AG_CB.Checked)
            .CommandType = CommandType.StoredProcedure
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(IM_MV_Dt)
        IMMV_DV.DataSource = IM_MV_Dt

        C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "IM_MV_V_Select_By_Date"
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
            .Parameters.AddWithValue("@BsType_ID", IMMV_Type_cm.SelectedValue)
            .Parameters.AddWithValue("@AG_ID", AG_ID)
            .Parameters.AddWithValue("@By_GM", 1)
            .Parameters.AddWithValue("@GM_ID", AG_MV_GM_CM.SelectedValue)
            .Parameters.AddWithValue("@ALL_AG", ALL_AG_CB.Checked)
            .CommandType = CommandType.StoredProcedure
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        GM_DataGridViewX1.DataSource = C.Dt

    End Sub

    Private Sub IMMV_Search_txt_TextChanged(sender As Object, e As EventArgs) Handles IMMV_Search_txt.TextChanged
        On Error Resume Next
        Dim Dv As DataView
        Dv = IM_MV_Dt.AsDataView
        Dv.RowFilter = "Item_Name LIKE '%" + sender.Text + "%'"
        IMMV_DV.DataSource = Dv
    End Sub

    Private Sub IMMV_DV_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles IMMV_DV.RowsAdded
        Calc_IM_MV_T_Qty()
    End Sub

    Private Sub IMMV_DV_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles IMMV_DV.RowsRemoved
        Calc_IM_MV_T_Qty()
    End Sub

    Public Sub Calc_IM_MV_T_Qty()
        Dim QTY As Double = 0
        Dim COST As Double = 0

        For i = 0 To IMMV_DV.Rows.Count - 1
            QTY = QTY + IMMV_DV.Rows(i).Cells("IM_MV_S_QTY_CL").Value
            COST = COST + IMMV_DV.Rows(i).Cells("IM_MV_T_Price_CL").Value
        Next
        IM_MV_T_QTY_txt.Text = QTY.ToString("0.00")
        IM_MV_T_Price_txt.Text = COST.ToString("n")
    End Sub

    '------------------------------------------------------------------------------------------------
   


    Private Sub AG_MV_GM_CM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles AG_MV_GM_CM.SelectedIndexChanged
        On Error Resume Next
        IM_MV_V_Select_By_Date()
    End Sub
    ''---------------------------------------------------------------------------------------------------------

    'Private Sub Show_IM_btn_Click(sender As Object, e As EventArgs) Handles Show_IM_btn.Click
    '    If IMDataGridViewX.Visible = True Then
    '        IMDataGridViewX.Visible = False
    '    Else
    '        IMDataGridViewX.Visible = True
    '        Load_ALL_AG()
    '    End If
    'End Sub

    'Public Sub Load_ALL_AG()
    '    Dim c As New C

    '    Try
    '        Dim s As String
    '        s = "select AG_ID,Ag_name,isnull(T_Balance,0) AS T_Balance from Agents WHERE AG_ID <> 1"
    '        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
    '        c.Da.Fill(c.Dt)
    '        IMDataGridViewX.DataSource = c.Dt
    '        If c.Dt.Rows.Count > 0 Then
    '            IMDataGridViewX.Visible = True
    '            IMDataGridViewX.Size = New Point(IMDataGridViewX.Size.Width, 530)
    '        Else
    '            IMDataGridViewX.Visible = False
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub IMMV_Print_Click(sender As Object, e As EventArgs) Handles IMMV_Print.Click
        AG_MV_print_Reset()
        AG_MV_print()
        IMMV_Rpt()
        AG_MV_print_Reset()
    End Sub


    Private Sub AG_MV_print()

        For i = 0 To IMMV_DV.Rows.Count - 1

            Dim sqlComm As New SqlClient.SqlCommand
            sqlComm.CommandText = "Agent_IM_MV_Report_INSERT"
            sqlComm.CommandType = CommandType.StoredProcedure

            If Filter_By_Date_CB.Checked = True Then sqlComm.Parameters.AddWithValue("@DATE", IMMV_DV.Rows(i).Cells("IMMV_DATE_CL").Value)
            sqlComm.Parameters.AddWithValue("@item_name", IMMV_DV.Rows(i).Cells("IMMV_IMNAME_CL").Value)
            sqlComm.Parameters.AddWithValue("@U_Name", IMMV_DV.Rows(i).Cells("IMMV_UNAME_CL").Value)
            sqlComm.Parameters.AddWithValue("@S_QTY", IMMV_DV.Rows(i).Cells("IM_MV_S_QTY_CL").Value)
            sqlComm.Parameters.AddWithValue("@S_Total", IMMV_DV.Rows(i).Cells("IM_MV_T_Price_CL").Value)

            SQL_SP_EXEC(sqlComm)

        Next

    End Sub

    Public Sub AG_MV_print_Reset()
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "Agent_IM_MV_Report_DELETE"
        sqlComm.CommandType = CommandType.StoredProcedure
        SQL_SP_EXEC(sqlComm)
    End Sub


    Private Sub IMMV_Rpt()
        Dim pp As New ReportConnection
        If Filter_By_Date_CB.Checked = False Then
            pp.rp.Load(Application.StartupPath & "\reports\Agent_IM_MV_A4.rpt")
        Else
            pp.rp.Load(Application.StartupPath & "\reports\Agent_IM_MV_By_Date_A4.rpt")
        End If

        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, USER_NAME)
            .rp.SetParameterValue(1, " من " + HOME.DateRange_Flate.D_F.Value.Date + " إلى " + HOME.DateRange_Flate.D_T.Value.Date)
            .rp.SetParameterValue(2, My_Settings.Server_Desc)
            .rp.SetParameterValue(3, IM_MV_T_QTY_txt.Text)
            .rp.SetParameterValue(4, IM_MV_T_Price_txt.Text)
            If ALL_AG_CB.Checked = True Then
                .rp.SetParameterValue(5, " كل العملاء  ")
            Else
                .rp.SetParameterValue(5, " العميل : " + AG_Cm.Textt)
            End If
            .rp.SetParameterValue(6, " المعاملات : " + IMMV_Type_cm.Text)
            .rp.SetParameterValue(7, " التصنيف : " + AG_MV_GM_CM.Text)

        End With

        Dim p As New print
        p.CrystalReportViewer1.ReportSource = pp.rp
        p.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_A4
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()
    End Sub

    Private Sub GMMV_Print_Click(sender As Object, e As EventArgs) Handles GMMV_Print.Click
        AG_MV_print_Reset()
        AG_MV_print_GM()
        GM_MV_Rpt()
        AG_MV_print_Reset()
    End Sub

    Private Sub AG_MV_print_GM()

        For i = 0 To GM_DataGridViewX1.Rows.Count - 1

            Dim sqlComm As New SqlClient.SqlCommand
            sqlComm.CommandText = "Agent_IM_MV_Report_INSERT"
            sqlComm.CommandType = CommandType.StoredProcedure

            sqlComm.Parameters.AddWithValue("@item_name", GM_DataGridViewX1.Rows(i).Cells("GM_Name_CL").Value)
            sqlComm.Parameters.AddWithValue("@S_QTY", GM_DataGridViewX1.Rows(i).Cells("QTY_CL").Value)
            sqlComm.Parameters.AddWithValue("@S_Total", GM_DataGridViewX1.Rows(i).Cells("S_Total_CL").Value)

            SQL_SP_EXEC(sqlComm)

        Next

    End Sub

    Private Sub GM_MV_Rpt()
        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\reports\Agent_GM_MV_A4.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue(0, USER_NAME)
            .rp.SetParameterValue(1, " من " + HOME.DateRange_Flate.D_F.Value.Date + " إلى " + HOME.DateRange_Flate.D_T.Value.Date)
            .rp.SetParameterValue(2, My_Settings.Server_Desc)

            '.rp.SetParameterValue(3, AG_ID)
            '.rp.SetParameterValue(4, IMMV_Type_cm.SelectedValue)

            '.rp.SetParameterValue(5, HOME.DateRange_Flate.D_F.Value)
            '.rp.SetParameterValue(6, HOME.DateRange_Flate.D_T.Value)

            '.rp.SetParameterValue(3, IM_MV_T_QTY_txt.Text)
            '.rp.SetParameterValue(4, IM_MV_T_Price_txt.Text)
            .rp.SetParameterValue(3, "0")
            .rp.SetParameterValue(4, "0")

            If ALL_AG_CB.Checked = True Then
                .rp.SetParameterValue(5, " كل العملاء  ")
            Else
                .rp.SetParameterValue(5, " العميل : " + AG_Cm.Textt)
            End If
            .rp.SetParameterValue(6, " المعاملات : " + IMMV_Type_cm.Text)

        End With

        Dim p As New print
        p.CrystalReportViewer1.ReportSource = pp.rp
        p.Show()

        'pp.rp.PrintOptions.PrinterName = Default_Printer_A4
        'pp.rp.PrintToPrinter(1, False, 0, 0)
        'pp.rp.Dispose()
    End Sub

    Private Sub Filter_By_Date_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Filter_By_Date_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs)
    '    If IMDataGridViewX.Visible = True Then
    '        IMDataGridViewX.Visible = False
    '    Else
    '        IMDataGridViewX.Visible = True
    '        Load_ALL_AG()
    '    End If
    'End Sub

    Private Sub ALL_AG_CB_CheckedChanged(sender As Object, e As EventArgs) Handles ALL_AG_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub
End Class