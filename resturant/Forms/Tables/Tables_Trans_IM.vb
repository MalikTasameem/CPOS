Public Class Tables_Trans_IM
    Dim rs As New Resizer
    '  Public tran_F, tran_T
    Public Bill_T_ID, AG_ID As Integer
    Public TB_Num As Integer
    Public TB_TO_Num As Integer
    Public T_ID As Integer
    Dim New_Bill_DT As New DataTable
    Public Pied_Money As Double = 0

    Private Sub TablePiedApart_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub TablesBalance_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then If SaveButton.Enabled = True Then SaveButton_Click(sender, e)
    End Sub

    Private Sub TablesBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rs.FindAllControls(Me)
        SB_Contents_SELECT_TB(TB_Num)
        Me.TB_From_Lb.Text = "من الطاولة : " & F_TablesBalance.TB_F_txt.Text
        Me.TB_To_LB.Text = " إلى الطاولة : " & F_TablesBalance.TB_T_txt.Text
        Call_New_Bill()
    End Sub

    Public Sub Fill_Bill_Info()

        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Info_V_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
        End With
        C.Con.Open()
        C.Dr = C.Com.ExecuteReader
        If C.Dr.HasRows Then
            C.Dr.Read()
            'AG_ID = C.Dr("AG_ID")
            'AG_Name_Info.Text = AG_Name_Info.Text + C.Dr("Ag_name")
        End If
        C.Con.Close()
    End Sub

    Private Sub Call_New_Bill()
        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "Call_New_SalesBill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", 0)
            .Parameters.AddWithValue("@AG_ID", SB_Default_AG)
            .Parameters.AddWithValue("@Bill_Num", 0)
            .Parameters.AddWithValue("@SB_ID", 0)
            .Parameters.AddWithValue("@isNew", 0)
            .Parameters.AddWithValue("@SB_Type", SB_DefaultStatus)
            .Parameters.AddWithValue("@Pr_ID", Pr_ID)
            .Parameters.AddWithValue("@isPied", 0)
            .Parameters.AddWithValue("@User_ID", USER_ID)
            .Parameters("@T_ID").Direction = ParameterDirection.Output
            .Parameters("@Bill_Num").Direction = ParameterDirection.Output
            .Parameters("@isNew").Direction = ParameterDirection.Output
            .Parameters("@SB_ID").Direction = ParameterDirection.Output
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            Me.T_ID = C.Com.Parameters("@T_ID").Value
            F_POS.T_ID = C.Com.Parameters("@T_ID").Value
            Fill_Bill_Info()
            SB_Contents_SELECT_Bill_First_Time()
            AG_Balance_Update_AG(TB_TO_Num)
        End If
    End Sub

    Private Sub AG_Balance_Update_AG(TB_ID As Integer)
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "AG_Balance_Update_Table"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
            .Parameters.AddWithValue("@TB_ID", TB_ID)
        End With
        SQL_SP_EXEC(c.Com)
    End Sub

    Public Sub SB_Contents_SELECT_Bill_First_Time()
        Dim C As New C
        New_Bill_DT.Clear()
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Contents_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@SB_T_ID", Me.T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(New_Bill_DT)
        MetroGrid.DataSource = New_Bill_DT
        If New_Bill_DT.Rows.Count > 0 Then
            MsgBox("يجب مسح أصناف الفاتورة المفتوحة حاليا أولا قبل القيام بهذه العملية", MsgBoxStyle.Exclamation)
            Me.Close()
        End If
    End Sub

    Public Sub SB_Contents_SELECT_Bill()
        Dim C As New C
        New_Bill_DT.Clear()
        With C.Com
            .Connection = C.Con
            .CommandText = "SB_Contents_SELECT_Bill"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@SB_T_ID", Me.T_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(New_Bill_DT)
        MetroGrid.DataSource = New_Bill_DT
    End Sub


    Public Sub SB_Contents_SELECT_TB(TB_ID As Integer)
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "TB_Contents_V_SELECT"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@TB_ID", TB_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        ALL_IMGrid.DataSource = C.Dt
    End Sub


    Private Sub TablesBalance_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub MetroGrid_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles MetroGrid.RowsAdded
        Calc_Bill()
    End Sub

    Private Sub MetroGrid_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles MetroGrid.RowsRemoved
        Calc_Bill()
    End Sub

    Public Sub Calc_Bill()
        Dim sum As Double = 0
        For i = 0 To Me.MetroGrid.Rows.Count - 1
            sum = sum + MetroGrid.Rows(i).Cells("Total_CL").Value
        Next
        Me.PureTextBox.Text = sum
    End Sub



    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click

        If MetroGrid.Rows.Count > 0 Then
            'If AG_ID = Default_AG_ID Then
            AG_Balance_Update_TB_Ordered()
            'Else
            '    TablesFindingRest.ShowDialog()
            '    If TablesFindingRest.is_Back = False Then AG_Balance_Update_TB_Ordered()
            'End If
        End If

    End Sub

    Private Sub AG_Balance_Update_TB_Ordered()
        Dim c As New C
        Dim Str As String = ""
        With c.Com
            .Connection = c.Con
            .CommandText = "AG_Balance_Update_TB_Ordered"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Me.T_ID)
            .Parameters.AddWithValue("@TB_ID", Me.TB_TO_Num)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            MsgBox("تم النقل", MsgBoxStyle.Information, "")


            For i = 0 To MetroGrid.Rows.Count - 1
                Str += "," & MetroGrid.Rows(i).Cells("IM_NameCL").Value & "عدد :" & Convert.ToInt16(MetroGrid.Rows(i).Cells("QTY_CL").Value).ToString
            Next

            Network_Edit_Tracker_insert(Me.TB_From_Lb.Text & Me.TB_To_LB.Text & Str, 0, 33, 3)
            Tables_Check_isBusy()
            F_TablesBalance.loadtables()
            Me.Close()
        End If
    End Sub

    Private Sub Tables_Check_isBusy()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "Tables_Check_isBusy"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@TB_ID", Me.TB_Num)
        End With
        SQL_SP_EXEC(c.Com)
    End Sub


    Public Sub Refresh_TB_Balance()
        F_TablesBalance.SB_Contents_SELECT_TB(F_TablesBalance.TB_Num)
        F_TablesBalance.TB_NotPied_V_SELECT_Bill(F_TablesBalance.TB_Num)
        F_TB_BillIM.Select_IM()
    End Sub


    'Public Sub AG_Balance_Reset_AG_Balance()

    '    Dim c As New C
    '    With c.Com
    '        .Connection = c.Con
    '        .CommandText = "AG_Balance_Update_AG"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@T_ID", T_ID)
    '        .Parameters.AddWithValue("@AG_ID", 1)
    '        .Parameters.AddWithValue("@ON_UPDATE", 0)
    '    End With
    '    SQL_SP_EXEC(c.Com)

    'End Sub


    Private Sub ALL_IMGrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles ALL_IMGrid.CellContentClick
        If e.ColumnIndex = 0 Then Fetch_ItemToList()
    End Sub



    Public Sub Fetch_ItemToList()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "TB_Contents_INSERT_Table_Apart"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@SB_T_ID_From", ALL_IMGrid.CurrentRow.Cells("B_T_ID_CL").Value)
            .Parameters.AddWithValue("@SB_T_ID_To", Me.T_ID)
            .Parameters.AddWithValue("@T_ID", ALL_IMGrid.CurrentRow.Cells("T_ID_CL").Value)
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            SB_Contents_SELECT_TB(TB_Num)
            SB_Contents_SELECT_Bill()

        End If
    End Sub

    Private Sub MetroGrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles MetroGrid.CellContentClick
        If e.ColumnIndex = 0 Then SB_Contents_Delete_IM()
    End Sub

    Private Sub SB_Contents_Delete_IM()
        Dim Row_Index As Integer = MetroGrid.CurrentCell.RowIndex
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "TB_Contents_Delete_IM_Apart"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", MetroGrid.CurrentRow.Cells("IM_T_ID").Value)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            SB_Contents_SELECT_Bill()
            SB_Contents_SELECT_TB(TB_Num)
            If Row_Index > 0 Then MetroGrid.CurrentCell = MetroGrid.Rows(Row_Index - 1).Cells("IM_NameCL")
        End If
    End Sub

    Private Sub SB_Contents_Clear_IM()
        For i = 0 To MetroGrid.Rows.Count - 1

            Dim c As New C
            With c.Com
                .Connection = c.Con
                .CommandText = "TB_Contents_Delete_IM_Apart"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@T_ID", MetroGrid.Rows(i).Cells("IM_T_ID").Value)
            End With
            SQL_SP_EXEC(c.Com)
        Next
    End Sub



    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        SB_Contents_Clear_IM()
        'AG_Balance_Reset_AG_Balance()
        Me.Close()
    End Sub
End Class