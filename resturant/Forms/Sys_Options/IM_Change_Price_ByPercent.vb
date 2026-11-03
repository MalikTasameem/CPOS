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
        Dim groupId As Integer
        If Not Integer.TryParse(Convert.ToString(GM_Serach.SelectedValue), groupId) OrElse groupId <= 0 Then
            MsgBox("اختر التصنيف أولًا.", MsgBoxStyle.Exclamation) : Return
        End If
        For i = 0 To IM_GV.Rows.Count - 1
            If IM_GV.Rows(i).IsNewRow Then Continue For
            If Not IsDBNull(IM_GV.Rows(i).Cells("GM_ID_CL").Value) AndAlso Convert.ToInt32(IM_GV.Rows(i).Cells("GM_ID_CL").Value) = groupId Then
                MsgBox("تم إدراج نسبة للمجموعة", MsgBoxStyle.Exclamation, "")
                Exit Sub
            End If
        Next
        Change_Price_Insert()
    End Sub
    Private Sub Change_Price_Insert()
        Dim percent As Double
        If Not Double.TryParse(Bercent_TXT.Text, percent) OrElse Double.IsNaN(percent) OrElse Double.IsInfinity(percent) Then
            MsgBox("أدخل نسبة صحيحة.", MsgBoxStyle.Exclamation) : Return
        End If
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "[Change_Price_Insert]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@GM_ID", GM_Serach.SelectedValue)
            .Parameters.AddWithValue("@Percent_Price", percent)
        End With
        If AuditPricingRulesUi.Run(Me, c.Com) Then
            AuditPricingRulesUi.Refresh(Me, AddressOf Change_Price_SELECT)

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
        If IM_GV.CurrentRow IsNot Nothing AndAlso Not IM_GV.CurrentRow.IsNewRow Then Change_Price_Delete()
    End Sub
    Private Sub Change_Price_Delete()

        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "[Change_Price_Delete]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@T_ID", IM_GV.CurrentRow.Cells("T_ID_CL").Value)
        End With
        If AuditPricingRulesUi.Run(Me, c.Com) Then
            AuditPricingRulesUi.Refresh(Me, AddressOf Change_Price_SELECT)
        End If

    End Sub

End Class
