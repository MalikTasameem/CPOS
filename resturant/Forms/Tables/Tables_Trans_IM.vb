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
        Dim savedBill As Integer
        Dim values As New Dictionary(Of String, Object) From {{"DefaultAccountId", SB_Default_AG}, {"PeriodId", Pr_ID}, {"SaleType", SB_DefaultStatus}}
        If Not AuditTableSplitUi.Run(Me, "TableSplit.Started", TB_Num, TB_TO_Num, 0, values, savedBill) Then
            Me.Close()
            Return
        End If
        If savedBill <= 0 Then
            SaveButton.Enabled = False
            Me.Close()
            Return
        End If
        Me.T_ID = savedBill
        F_POS.T_ID = savedBill
        AuditDocumentFieldsUi.Refresh(Me,
            Sub()
                Fill_Bill_Info()
                SB_Contents_SELECT_Bill_First_Time()
            End Sub)
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
        Dim savedBill As Integer
        If Not AuditTableSplitUi.Run(Me, "TableSplit.Confirmed", TB_Num, TB_TO_Num, T_ID, New Dictionary(Of String, Object), savedBill) Then Return
        AuditDocumentFieldsUi.Refresh(Me,
            Sub()
                MsgBox("تم النقل", MsgBoxStyle.Information, "")
                F_TablesBalance.loadtables()
                Me.Close()
            End Sub)
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
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = 0 Then Fetch_ItemToList()
    End Sub



    Public Sub Fetch_ItemToList()
        If ALL_IMGrid.CurrentRow Is Nothing OrElse ALL_IMGrid.CurrentRow.IsNewRow OrElse Not ALL_IMGrid.Columns.Contains("T_ID_CL") OrElse Not ALL_IMGrid.Columns.Contains("DataGridViewTextBoxColumn2") Then Return
        Dim row = ALL_IMGrid.CurrentRow
        If IsDBNull(row.Cells("T_ID_CL").Value) OrElse IsDBNull(row.Cells("DataGridViewTextBoxColumn2").Value) Then Return
        Dim values As New Dictionary(Of String, Object) From {{"TargetLineId", CInt(row.Cells("T_ID_CL").Value)}, {"DisplayedQuantity", CDec(row.Cells("DataGridViewTextBoxColumn2").Value)}}
        Dim savedBill As Integer
        If Not AuditTableSplitUi.Run(Me, "TableSplit.UnitMoved", TB_Num, TB_TO_Num, T_ID, values, savedBill) Then Return
        AuditDocumentFieldsUi.Refresh(Me,
            Sub()
                SB_Contents_SELECT_TB(TB_Num)
                SB_Contents_SELECT_Bill()
            End Sub)
    End Sub

    Private Sub MetroGrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles MetroGrid.CellContentClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = 0 Then SB_Contents_Delete_IM()
    End Sub

    Private Sub SB_Contents_Delete_IM()
        If MetroGrid.CurrentRow Is Nothing OrElse MetroGrid.CurrentRow.IsNewRow OrElse Not MetroGrid.Columns.Contains("IM_T_ID") OrElse Not MetroGrid.Columns.Contains("QTY_CL") Then Return
        Dim row = MetroGrid.CurrentRow
        If IsDBNull(row.Cells("IM_T_ID").Value) OrElse IsDBNull(row.Cells("QTY_CL").Value) Then Return
        Dim values As New Dictionary(Of String, Object) From {{"TargetLineId", CInt(row.Cells("IM_T_ID").Value)}, {"DisplayedQuantity", CDec(row.Cells("QTY_CL").Value)}}
        Dim savedBill As Integer
        If Not AuditTableSplitUi.Run(Me, "TableSplit.UnitReturned", TB_Num, TB_TO_Num, T_ID, values, savedBill) Then Return
        AuditDocumentFieldsUi.Refresh(Me,
            Sub()
                SB_Contents_SELECT_Bill()
                SB_Contents_SELECT_TB(TB_Num)
            End Sub)
    End Sub

    Private Function SB_Contents_Clear_IM() As Boolean
        If Not MetroGrid.Columns.Contains("IM_T_ID") OrElse Not MetroGrid.Columns.Contains("QTY_CL") Then Return False
        For Each row As DataGridViewRow In MetroGrid.Rows
            If row.IsNewRow Then Continue For
            If IsDBNull(row.Cells("IM_T_ID").Value) OrElse IsDBNull(row.Cells("QTY_CL").Value) Then Return False
            Dim values As New Dictionary(Of String, Object) From {{"TargetLineId", CInt(row.Cells("IM_T_ID").Value)}, {"DisplayedQuantity", CDec(row.Cells("QTY_CL").Value)}}
            Dim savedBill As Integer
            If Not AuditTableSplitUi.Run(Me, "TableSplit.UnitReturned", TB_Num, TB_TO_Num, T_ID, values, savedBill) Then
                AuditDocumentFieldsUi.Refresh(Me,
                    Sub()
                        SB_Contents_SELECT_Bill()
                        SB_Contents_SELECT_TB(TB_Num)
                    End Sub)
                Return False
            End If
        Next
        Return True
    End Function



    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        If Not SB_Contents_Clear_IM() Then Return
        Me.Close()
    End Sub
End Class
