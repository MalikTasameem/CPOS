Imports System.Data.SqlClient
Imports System.Globalization
Public Class IMP_Perfet_Report_2
    Dim Min_SP As Boolean = False
    Dim Min_SP_2 As Boolean = False
    Dim is_Filter_SB_Type As Boolean = False
    Dim IM_MV_Dt As New DataTable

    Dim rs As New Resizer
    Private Sub Empty_F_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        fetch_SB_Type()
        Min_SB_Panel.Visible = S_Allow_MinSP
        IMPerf_DGV.Columns("Markter_Val_CL").Visible = S_Marketers
        Markters_GroupBox.Visible = S_Marketers
        fetch_ST()
        fetch_GM()
    End Sub
    Public Sub fetch_GM()
        PerfetGM_Serach.DataSource = GetMailItems()
        PerfetGM_Serach.DisplayMember = "name"
        PerfetGM_Serach.ValueMember = "ID"
        PerfetGM_Serach.SelectedIndex = 0
    End Sub


    Public Sub fetch_ST()
        ST_cm.DataSource = Ge_St_Items()
        ST_cm.DisplayMember = "name"
        ST_cm.ValueMember = "ID"
        ST_cm.SelectedIndex = 0
    End Sub

    Private Sub Empty_F_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub IMPerf_Serch_btn_Click(sender As Object, e As EventArgs)
        IM_Perfet_Select_By_Date()
    End Sub


    Private Sub IM_Perfet_Select_By_Date()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "IM_Perfet_Select"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
            .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
            .Parameters.AddWithValue("@GM_ID", PerfetGM_Serach.SelectedValue)
            .Parameters.AddWithValue("@is_By_Min_SP", Min_SP)
            .Parameters.AddWithValue("@is_By_Min_SP_2", Min_SP_2)
            .Parameters.AddWithValue("@Sales_Type", Sales_Type_Cm.SelectedValue)
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        IMPerf_DGV.DataSource = C.Dt
        IMRtn_Perfet_Select()
    End Sub

    Private Sub IM_Perfet_Select_By_IM()
        IM_MV_Dt.Clear()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "IM_Perfet_Select_By_IM"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
            .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
            .Parameters.AddWithValue("@GM_ID", PerfetGM_Serach.SelectedValue)
            .Parameters.AddWithValue("@is_By_Min_SP", Min_SP)
            .Parameters.AddWithValue("@is_By_Min_SP_2", Min_SP_2)
            '.Parameters.AddWithValue("@Sales_Type", Sales_Type_Cm.SelectedValue)
        End With
        C.Da = New SqlDataAdapter(C.Com)
        C.Da.Fill(IM_MV_Dt)
        IMPerf_DGV.DataSource = IM_MV_Dt
        'IMRtn_Perfet_Select()
    End Sub

    Private Sub IMRtn_Perfet_Select()
        Dim C = New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "IMRtn_Perfet_Select"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@D_F", HOME.DateRange_Flate.D_F.Value)
            .Parameters.AddWithValue("@D_T", HOME.DateRange_Flate.D_T.Value)
            .Parameters.AddWithValue("@ST_ID", ST_cm.SelectedValue)
            .Parameters.AddWithValue("@GM_ID", PerfetGM_Serach.SelectedValue)
            .Parameters.Add("@SumRtn", SqlDbType.Float, 0)
            .Parameters.Add("@RTN_Saler_Money", SqlDbType.Float, 0)
            .Parameters.Add("@Markter_Val", SqlDbType.Float, 0)
            .Parameters("@RTN_Saler_Money").Direction = ParameterDirection.Output
            .Parameters("@SumRtn").Direction = ParameterDirection.Output
            .Parameters("@Markter_Val").Direction = ParameterDirection.Output
            .Parameters.AddWithValue("@is_By_Min_SP", Min_SP)
            .Parameters.AddWithValue("@is_By_Min_SP_2", Min_SP_2)
            .Parameters.AddWithValue("@Sales_Type", Sales_Type_Cm.SelectedValue)

        End With
        If SQL_SP_EXEC(C.Com) = True Then
            With (C.Com)
                SBRtn_TotalPerfet_txt.Text = .Parameters("@SumRtn").Value
                Total_ALL_Perfet_txt.Text = Pure_IM_Perfet_txt.Text
                Pure_IM_Perfet_txt.Text = (Convert.ToDouble(Total_ALL_Perfet_txt.Text) - Convert.ToDouble(SBRtn_TotalPerfet_txt.Text)).ToString("N")


                Total_RtnMark_Val_txt.Text = .Parameters("@Markter_Val").Value
                Pure_Mark_Val_txt.Text = (Convert.ToDouble(Total_Mark_Val_txt.Text) - Convert.ToDouble(Total_RtnMark_Val_txt.Text)).ToString("N")


                Dim N As Double = Convert.ToDouble(Saler_M_T_txt.Text) - .Parameters("@RTN_Saler_Money").Value
                Pure_Saler_M_txt.Text = N.ToString("N")


                Final_Pure_Perfet_txt.Text = (Convert.ToDouble(Pure_IM_Perfet_txt.Text) - Convert.ToDouble(Pure_Mark_Val_txt.Text) - Convert.ToDouble(Pure_Saler_M_txt.Text)).ToString("N")
            End With
        End If
    End Sub

    Private Sub Sales_Type_Cm_SelectedValueChanged(sender As Object, e As EventArgs) Handles Sales_Type_Cm.SelectedValueChanged
        If is_Filter_SB_Type = True Then

            If Sales_Type_Cm.SelectedValue = 0 Then
                Min_SP = True
                Min_SP_2 = True
            End If

            If Sales_Type_Cm.SelectedValue = 1 Then
                Min_SP = False
                Min_SP_2 = False
            End If

            If Sales_Type_Cm.SelectedValue = 2 Then
                Min_SP = True
                Min_SP_2 = False
            End If

            If Sales_Type_Cm.SelectedValue = 3 Then
                Min_SP_2 = True
                Min_SP = False
            End If

        End If
    End Sub

    Public Sub fetch_SB_Type()
        Sales_Type_Cm.DataSource = Ge_SB_Type()
        Sales_Type_Cm.DisplayMember = "name"
        Sales_Type_Cm.ValueMember = "ID"
        Sales_Type_Cm.SelectedIndex = 0
    End Sub

    Function Ge_SB_Type() As List(Of MailItem)
        Dim mailItems = New List(Of MailItem)
        mailItems.Add(New MailItem(0, "---- كل المبيعات ----"))
        mailItems.Add(New MailItem(1, "القطاعي"))
        mailItems.Add(New MailItem(2, "الجملة"))
        mailItems.Add(New MailItem(3, "جملة الجملة"))
        Return mailItems

    End Function

    Private Sub IMPerf_DGV_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles IMPerf_DGV.RowsAdded
        Calc_IMPerf()
    End Sub

    Private Sub IMPerf_DGV_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles IMPerf_DGV.RowsRemoved
        Calc_IMPerf()
    End Sub

    Public Sub Calc_IMPerf()
        Dim Saler_M As Double = 0
        Dim COST As Double = 0
        Dim Total_SB As Double = 0
        Dim Total_Market_Val As Double = 0

        For i = 0 To IMPerf_DGV.Rows.Count - 1
            COST = COST + IMPerf_DGV.Rows(i).Cells("Total_Perfet_CL").Value
            Saler_M = Saler_M + IMPerf_DGV.Rows(i).Cells("Saler_Money_CL").Value
            'Total_SB = Total_SB + IMPerf_DGV.Rows(i).Cells("T_Price_CL").Value
            Total_Market_Val = Total_Market_Val + IMPerf_DGV.Rows(i).Cells("Markter_Val_CL").Value
        Next
        Pure_IM_Perfet_txt.Text = COST.ToString("n")
        Saler_M_T_txt.Text = Saler_M.ToString("n")
        'Final_Pure_Perfet_txt.Text = Total_SB.ToString("n")
        Total_Mark_Val_txt.Text = Total_Market_Val.ToString("n")
    End Sub

    Private Sub IMMV_Search_txt_TextChanged(sender As Object, e As EventArgs) Handles IMMV_Search_txt.TextChanged
        On Error Resume Next
        Dim Dv As DataView
        Dv = IM_MV_Dt.AsDataView
        Dv.RowFilter = "Item_Name LIKE '%" + sender.Text + "%'"
        IMPerf_DGV.DataSource = Dv
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        IM_Perfet_Select_By_IM()
    End Sub
End Class