Public Class Table_Bill_Screen
    Dim rs As New Resizer
    Dim Pr_Bill_C As New C
    Dim isPied As Boolean = True
    Dim Tick = 0
    Dim Select_Row As Integer
    Private Sub Table_Bill_Screen_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
    Private Sub BillsMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        Load_Pr_Bils()
    End Sub

    Private Sub BillsMenu_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls_POS(Me)
    End Sub

    Private Sub Load_Pr_Bils()
        Try
            'Dim Row_Index As Integer = 0
            'If BillsGrid.Rows.Count > 0 Then Row_Index = BillsGrid.CurrentCell.RowIndex + 1
            Pr_Bill_C = New C
            With (Pr_Bill_C.Com)
                .Connection = Pr_Bill_C.Con
                .CommandText = "Table_Screen_Viewer_V_SELECT"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@CP_Name", My.Computer.Name)
                .Parameters.AddWithValue("@Counter", 0)
                .Parameters("@Counter").Direction = ParameterDirection.Output
            End With
            Pr_Bill_C.Da = New SqlClient.SqlDataAdapter(Pr_Bill_C.Com)
            Pr_Bill_C.Da.Fill(Pr_Bill_C.Dt)
            BillsGrid.DataSource = Pr_Bill_C.Dt
            If Pr_Bill_C.Com.Parameters("@Counter").Value > 0 Then
                My.Computer.Audio.Play(Application.StartupPath & "\Notif.wav")
            End If
            'If Row_Index > 0 Then BillsGrid.CurrentCell = BillsGrid.Rows(Row_Index).Cells("T_ID_CL")
        Catch ex As Exception
            Tick = 0
        End Try
    End Sub

    Private Sub BillsGrid_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles BillsGrid.CellContentClick
        If e.ColumnIndex = 0 Then
            Timer1.Stop()
            Select_Row = BillsGrid.CurrentRow.Cells("T_ID_CL").Value
            Beep()

            Dim MSG As New YesNo
            MSG.Msg_Lb.Text = " مسح الطلب " & vbNewLine & BillsGrid.CurrentRow.Cells("item_name_CL").Value
            MSG.ShowDialog()
            If MSG.Result = True Then Delete_IM()

            Timer1.Start()
        End If
    End Sub

    Private Sub Delete_IM()
        Pr_Bill_C = New C

        With (Pr_Bill_C.Com)
            .Connection = Pr_Bill_C.Con
            .CommandText = "Table_Screen_Delete_Row"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", Select_Row)
        End With
        If SQL_SP_EXEC(Pr_Bill_C.Com) = True Then
            Load_Pr_Bils()          
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Tick += 1
        If Tick = 2 Then
            Load_Pr_Bils()
            Tick = 0
        End If
    End Sub

End Class