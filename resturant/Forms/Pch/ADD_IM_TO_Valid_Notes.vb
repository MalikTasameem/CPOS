Public Class ADD_IM_TO_Valid_Notes

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()

    End Sub

    Private Sub Add_Valid_Btn_Click(sender As Object, e As EventArgs) Handles Add_Valid_Btn.Click
        Valid_Notes_Insert()
    End Sub

    Private Sub Valid_Notes_Insert()
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "Valid_Notes_Insert"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@Pch_T_ID", 0)
        sqlComm.Parameters.AddWithValue("@IM_ID", IM_Cm.TXT_ID.Text)
        sqlComm.Parameters.AddWithValue("@VALID_DATE", Valid_For_List_Date.Value)
        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تمت إضافة الإشعار", MsgBoxStyle.Information, "")
            IM_Notes_Valid_Form.Fill_ALL_IM()
            IM_Cm.Textt = ""
            Barcode_SH_txt.Clear()
        End If
    End Sub

    Private Sub Barcode_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_SH_txt.KeyDown
        Select Case e.KeyCode
            Case Keys.Return : If String.IsNullOrWhiteSpace(Barcode_SH_txt.Text) = False Then Load_IM_Barcode()
            Case Keys.Delete
                Barcode_SH_txt.Clear()
        End Select
    End Sub


    Public Sub Load_IM_Barcode()

        Dim c As New C
        Try
            Dim s As String
            s = "select IM_ID from IM_units_Search_V WHERE Barcode = '" & Barcode_SH_txt.Text & "'"

            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                IM_Cm.Set_IM_By_ID(c.Dr("IM_ID"))            
            Else
                MsgBox("هذا الصنف غير موجود ضمن قائمة الأصناف", MsgBoxStyle.Exclamation, "")
                Barcode_SH_txt.SelectAll()
                Barcode_SH_txt.Focus()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class