Public Class Mang_IM_Valid_Notes
    Public IM_ID As Integer
    Public Bill_T_ID As Integer
    Public IM_NAME As String

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Valid_Notes_DELETE_ALL()
        Me.Close()
    End Sub

    Private Sub Add_Valid_Btn_Click(sender As Object, e As EventArgs) Handles Add_Valid_Btn.Click
        Valid_Notes_Insert()
        Valid_ListBox.Items.Add(Valid_For_List_Date.Value)
    End Sub

    Private Sub Remove_Valid_Btn_Click(sender As Object, e As EventArgs) Handles Remove_Valid_Btn.Click
        Valid_ListBox.Items.Remove(Valid_ListBox.SelectedItem)
    End Sub

    Private Sub Valid_Notes_DELETE_ALL()
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "Valid_Notes_DELETE_ALL"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@Pch_T_ID", Bill_T_ID)
        If SQL_SP_EXEC(sqlComm) = True Then Valid_Notes_Insert()
    End Sub

    Private Sub Valid_Notes_Insert()
        For i = 0 To Valid_ListBox.Items.Count - 1
            Valid_ListBox.SelectedIndex = i
            Dim sqlComm As New SqlClient.SqlCommand
            sqlComm.CommandText = "Valid_Notes_Insert"
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.AddWithValue("@Pch_T_ID", Bill_T_ID)
            sqlComm.Parameters.AddWithValue("@IM_ID", IM_ID)
            sqlComm.Parameters.AddWithValue("@VALID_DATE", Valid_ListBox.SelectedItem)
            SQL_SP_EXEC(sqlComm)
        Next
    End Sub

    Private Sub Mang_IM_Notes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IM_GET_Valid_List()
        Label1.Text = IM_NAME
    End Sub

    Public Sub IM_GET_Valid_List()
        Dim c As New C
        c = New C
        Try
            Dim s As String
            s = "select VALID_DATE from Valid_Notes WHERE Pch_T_ID = '" & Bill_T_ID & "'"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()
            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                While c.Dr.Read()
                    Valid_ListBox.Items.Add(c.Dr("VALID_DATE"))
                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


End Class