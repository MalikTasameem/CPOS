Public Class IM_Change_Price_ByPercent

    Private Sub Add_Unit_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Add_Unit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        Change_Price_SELECT()
        Load_GM()
    End Sub

    Private Sub Load_GM()
        Try
            Dim c As New C
            Dim s As String = "select GM_ID,GM_Name FROM General_Menu"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(c.Dt)
            GM_Serach.DataSource = c.Dt
            GM_Serach.DisplayMember = "GM_Name"
            GM_Serach.ValueMember = "GM_ID"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub InsertU_btn_Click(sender As Object, e As EventArgs) Handles InsertU_btn.Click

        For i = 0 To IM_GV.Rows.Count - 1
            If IM_GV.Rows(i).Cells("GM_ID_CL").Value = GM_Serach.SelectedValue Then
                MsgBox("تم إدراج نسبة للمجموعة", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If
        Next
        Change_Price_Insert()
    End Sub
    Private Sub Change_Price_Insert()

        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "[Change_Price_Insert]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@GM_ID", GM_Serach.SelectedValue)
            .Parameters.AddWithValue("@Percent_Price", Convert.ToDouble(Bercent_TXT.Text))
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            Network_Edit_Tracker_insert(" التصنيف:" & GM_Serach.Text & " النسبة:" & Bercent_TXT.Text, 0, 29, 1)
            Change_Price_SELECT()

        End If

    End Sub

    Private Sub Change_Price_SELECT()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Change_Price_SELECT"
            .CommandType = CommandType.StoredProcedure
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        IM_GV.DataSource = C.Dt
    End Sub


    Private Sub Back_ADD_New_IM_btn_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub


    Private Sub FetchIM_Menu()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Select_ToReport"
            .CommandType = CommandType.StoredProcedure
        End With
        C.Da = New SqlClient.SqlDataAdapter(C.Com)
        C.Da.Fill(C.Dt)
        IM_GV.DataSource = C.Dt
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        If IM_GV.Rows.Count > 0 Then Change_Price_Delete()
    End Sub
    Private Sub Change_Price_Delete()

        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "[Change_Price_Delete]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", IM_GV.CurrentRow.Cells("T_ID_CL").Value)
        End With
        If SQL_SP_EXEC(c.Com) = True Then
            Network_Edit_Tracker_insert(" التصنيف:" & IM_GV.CurrentRow.Cells("GM_NAME_CL").Value.ToString & " النسبة:" & IM_GV.CurrentRow.Cells("Percent_Price_CL").Value.ToString, 0, 29, 2)
            Change_Price_SELECT()
        End If

    End Sub

End Class