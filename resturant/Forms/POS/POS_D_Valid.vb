Public Class POS_D_Valid
    Public IM_ID As Integer
    Public ST_ID As Integer

    Private Sub POS_D_Valid_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub POS_D_Valid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Valid_Allow_IM = True
        Load_Valid()
    End Sub

    Private Sub Load_Valid()
        Dim C As New C
        With (C.Com)
            .Connection = C.Con
            .CommandText = "IM_Select_Valid"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters.AddWithValue("@ST_ID", ST_ID)
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        DataGridViewX1.DataSource = C.Dt

        If C.Dt.Rows.Count = 1 Then
            If Ban_Expierd_IM_MV = True Then
                If Convert.ToDateTime(DataGridViewX1.CurrentRow.Cells("D_Valid_CL").Value) <= Date.Now.Date Then
                    MsgBox("صنف منتهية صلاحيته لا يمكن بيعه", MsgBoxStyle.Critical, "خطأ")
                    Valid_Allow_IM = False
                    Exit Sub
                End If
            End If

            If SB_is_Fast = False Then
                F_POS.D_Valid = DataGridViewX1.CurrentRow.Cells("D_Valid_CL").Value
                Me.Close()
            Else
                Sales_Fast.Valid_TXT = DataGridViewX1.CurrentRow.Cells("D_Valid_CL").Value
                Me.Close()
            End If

        End If
        If DataGridViewX1.Rows.Count = 0 Then Me.Close()
    End Sub

    Private Sub ConfermButton_Click(sender As Object, e As EventArgs) Handles ConfermButton.Click
        If DataGridViewX1.Rows.Count > 0 Then
            If Ban_Expierd_IM_MV = True Then
                If Convert.ToDateTime(DataGridViewX1.CurrentRow.Cells("D_Valid_CL").Value) <= Date.Now.Date Then
                    MsgBox("صنف منتهية صلاحيته لا يمكن بيعه", MsgBoxStyle.Critical, "خطأ")
                    Valid_Allow_IM = False
                    Exit Sub
                End If
            End If

            If SB_is_Fast = False Then
                F_POS.D_Valid = DataGridViewX1.CurrentRow.Cells("D_Valid_CL").Value
                Me.Close()
            Else
                Sales_Fast.Valid_TXT = DataGridViewX1.CurrentRow.Cells("D_Valid_CL").Value
                Me.Close()
            End If
        End If
    End Sub

    Private Sub DataGridViewX1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridViewX1.KeyDown
        If e.KeyCode = Keys.Return Then
            If DataGridViewX1.Rows.Count > 0 Then
                If SB_is_Fast = False Then
                    F_POS.D_Valid = DataGridViewX1.CurrentRow.Cells("D_Valid_CL").Value
                    Me.Close()
                Else
                    Sales_Fast.Valid_TXT = DataGridViewX1.CurrentRow.Cells("D_Valid_CL").Value
                    Me.Close()
                End If
            End If
        End If

    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Valid_Allow_IM = False
        Me.Close()
    End Sub
End Class