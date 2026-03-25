Public Class AgentsTypesOptions

    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click
        Update_AgentsTypesOptions()
    End Sub
    Private Sub Update_AgentsTypesOptions()
        Dim c As New C
        With c.Com
            .Connection = c.Con
            .CommandText = "Update_AgentsTypesOptions"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@StdDis", StdDis_txt.Text)
            .Parameters.AddWithValue("@Points_Sale", Points_Sale_txt.Text)
            .Parameters.AddWithValue("@Point_Inc", Point_Inc_txt.Text)
            .Parameters.AddWithValue("@PrevDis", PrevDis_txt.Text)
        End With

        If SQL_SP_EXEC(c.Com) Then
            MsgBox("تم حفظ التعديلات", MsgBoxStyle.Information)
            StdDisPercent = StdDis_txt.Text
            Points_Sale = Points_Sale_txt.Text
            Point_Inc = Point_Inc_txt.Text
        End If
    End Sub

    Public Sub Select_AG_Type()
        Dim C As New C
        Dim S As String = "Select StdDis From Agents_Types Where ID = 5"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                StdDis_txt.Text = C.Dr("StdDis")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()
        '-----------------------------------------------------------------------------
        C = New C
        S = "Select Points_Sale,Point_Inc From Agents_Types Where ID = 6"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                Points_Sale_txt.Text = C.Dr("Points_Sale")
                Point_Inc_txt.Text = C.Dr("Point_Inc")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

        '------------------------------------------------------------------------------
        C = New C
        S = "Select StdDis From Agents_Types Where ID = 7"
        C.Com = New SqlClient.SqlCommand(S, C.Con)
        C.Con.Open()
        Try
            C.Dr = C.Com.ExecuteReader
            If C.Dr.HasRows Then
                C.Dr.Read()
                PrevDis_txt.Text = C.Dr("StdDis")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        C.Con.Close()

    End Sub

    Private Sub StdDis_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles StdDis_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub StdDis_txt_TextChanged(sender As Object, e As EventArgs) Handles StdDis_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Points_Sale_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Points_Sale_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Points_Sale_txt_TextChanged(sender As Object, e As EventArgs) Handles Points_Sale_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub Point_Inc_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Point_Inc_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Point_Inc_txt_TextChanged(sender As Object, e As EventArgs) Handles Point_Inc_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub AgentsTypesOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select_AG_Type()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub AgentsTypesOptions_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Prev_Dis_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PrevDis_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Prev_Dis_txt_TextChanged(sender As Object, e As EventArgs) Handles PrevDis_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub
End Class