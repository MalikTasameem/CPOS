Public Class CAT_FORM
    Dim S_ID As Integer
    Dim S_Name As String
    Public Form_Name As String, Form_Name_Arabic As String, Checked_Table As String = "", Checked_Table_ID As String = ""
    Public F_ID As String, F_Name As String, F_DETAILS As String
    Dim IM_DT As New DataTable

    Private Sub NewEmpButton_Click(sender As Object, e As EventArgs) Handles NewSButton.Click
        SaveSButton.Enabled = True
        EditSButton.Enabled = False
        DeleteSButton.Enabled = False
        SNameTextBox.Clear()
        SNameTextBox.Enabled = True
        SNameTextBox.Select()
        EditSButton.Text = "تعديل"
    End Sub

    Private Sub SaveSButton_Click(sender As Object, e As EventArgs) Handles SaveSButton.Click
        If String.IsNullOrWhiteSpace(SNameTextBox.Text) = False Then
            Store_Insert()
            SNameTextBox.Clear()
            Load_StoreData()
        End If
    End Sub
    Public Sub Store_Insert()


        query(" DECLARE @ID INT " & vbNewLine & " EXEC @ID = [AA_GET_MAX_ID] '" & Form_Name & "' " & vbNewLine & " INSERT INTO " & Form_Name & "( " & F_ID & " ," & F_Name & ") VALUES (@ID,'" & SNameTextBox.Text & "')")



        'Dim c As New C
        'Dim sqlComm As New SqlClient.SqlCommand()
        'sqlComm.CommandText = Form_Name & "_INSERT"
        'sqlComm.CommandType = CommandType.StoredProcedure
        'sqlComm.Parameters.AddWithValue("@" & F_Name, SNameTextBox.Text)
        'If SQL_SP_EXEC(sqlComm) = True Then
        '    MsgBox("تـــم إضافة العنصر ", MsgBoxStyle.Information)
        'End If

    End Sub

    Private Sub STORES_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = " قائمة : " & Form_Name_Arabic
        SendMessage(SEARCH_txt.Handle, &H1501, 0, "إبحث عن عنصر")
        Load_StoreData()
    End Sub

    Public Sub Load_StoreData()
        IM_DT.Clear()
        Dim c As New C
        Dim s As String = "select " & F_ID & "," & F_Name & " from " & Form_Name & "  WHERE " & F_ID & " > 1 ORDER BY " & F_Name & " ASC "
        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
        c.Da.Fill(IM_DT)
        S_listBox.DataSource = IM_DT
        S_listBox.DisplayMember = F_Name
        S_listBox.ValueMember = F_ID
    End Sub

    Private Sub EditSButton_Click(sender As Object, e As EventArgs) Handles EditSButton.Click
        If EditSButton.Text = "تعديل" Then
            SNameTextBox.Enabled = True
            DeleteSButton.Enabled = False
            SaveSButton.Enabled = False
            EditSButton.Text = "تأكيد التعديل"
        Else
            If String.IsNullOrWhiteSpace(SNameTextBox.Text) = False Then
                Store_Update()
                Me.Load_StoreData()
                SNameTextBox.Clear()
                SNameTextBox.Enabled = False
                EditSButton.Text = "تعديل"
            End If
        End If
    End Sub

    Public Sub Store_Update()

        query("UPDATE " & Form_Name & " SET " & F_Name & " = '" & SNameTextBox.Text & "' WHERE " & F_ID & " = " & S_ID)
        '------------------------------------------------------------------------------------------------------------
        'Dim c As New C
        'Dim sqlComm As New SqlClient.SqlCommand()
        'c.Com = New SqlClient.SqlCommand
        'sqlComm.CommandText = Form_Name & "_UPDATE"
        'sqlComm.CommandType = CommandType.StoredProcedure
        'sqlComm.Parameters.AddWithValue("@" & F_ID, S_ID)
        'sqlComm.Parameters.AddWithValue("@" & F_Name, SNameTextBox.Text)
        'If SQL_SP_EXEC(sqlComm) = True Then
        '    MsgBox("تـم تعديل العنصر ", MsgBoxStyle.Information)
        'End If
    End Sub

    Private Sub S_listBox_MouseClick(sender As Object, e As MouseEventArgs) Handles S_listBox.MouseClick
        Select_Store()
        SNameTextBox.Enabled = False
        DeleteSButton.Enabled = True
        EditSButton.Enabled = True
        SaveSButton.Enabled = False

        If S_ID = 1 Then
            '    MsgBox(" المخزن " + S_Name + " هوا المخزن الرئيسي للنظام ... لذا لا يمكن  حذفه ", MsgBoxStyle.Information)
            DeleteSButton.Enabled = False
        Else
            DeleteSButton.Enabled = True
        End If
    End Sub

    Public Sub Select_Store()
        Dim c1 As New C

        Dim sql As String = "select * from " & Form_Name & " where " & F_ID & " ='" & S_listBox.SelectedValue & "'"
        Dim com As New SqlClient.SqlCommand(sql, c1.Con)

        c1.Con.Open()
        Try

            c1.Dr = com.ExecuteReader
            If c1.Dr.HasRows Then
                c1.Dr.Read()
                SNameTextBox.Text = c1.Dr(F_Name)
                S_Name = c1.Dr(F_Name)
                S_ID = c1.Dr(F_ID)

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
    End Sub


    Public Function checked_before_delete()
        Dim c1 As New C

        Dim sql As String = "select * from " & Checked_Table & " where " & Checked_Table_ID & " ='" & S_listBox.SelectedValue & "'"
        Dim com As New SqlClient.SqlCommand(sql, c1.Con)

        c1.Con.Open()
        Try

            c1.Dr = com.ExecuteReader
            If c1.Dr.HasRows Then
                Return 1
            Else
                Return 0
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()

        Return 0
    End Function

    Private Sub DeleteSButton_Click(sender As Object, e As EventArgs) Handles DeleteSButton.Click


        If checked_before_delete() = 0 Then

            If MessageBox.Show(" تأكيد حذف " + S_Name, "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                S_Delete()
                Load_StoreData()
                SNameTextBox.Enabled = False
                SNameTextBox.Clear()
                DeleteSButton.Enabled = False
                SaveSButton.Enabled = False
                EditSButton.Enabled = False
            End If

        Else
            MsgBox("توجد حركة على هذا العنصر", MsgBoxStyle.Exclamation, "إبقاف الحذف")
        End If

    End Sub


    Public Sub S_Delete()

        query("DELETE FROM " & Form_Name & "  WHERE " & F_ID & " = " & S_ID)
        '-------------------------------------------------------
        'Dim c As New C
        'Dim sqlComm As New SqlClient.SqlCommand()
        'c.Com = New SqlClient.SqlCommand
        'sqlComm.CommandText = Form_Name & "_DELETE"
        'sqlComm.CommandType = CommandType.StoredProcedure
        'sqlComm.Parameters.AddWithValue("@" & F_ID, S_ID)

        'If SQL_SP_EXEC(sqlComm) = True Then
        '    MsgBox("تـم حذف العنصر ", MsgBoxStyle.Information)
        'End If
    End Sub

    Private Sub SNameTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles SNameTextBox.KeyDown
        If e.KeyCode = Keys.Return Then SaveSButton_Click(sender, e)
    End Sub


    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub STORES_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

        'F_Trans.Load_Cmbs()
        Me.Dispose()
    End Sub

    Private Sub SEARCH_txt_TextChanged(sender As Object, e As EventArgs) Handles SEARCH_txt.TextChanged
        Dim Dv As DataView
        Dv = IM_DT.AsDataView
        Dv.RowFilter = F_Name & " LIKE '%" + sender.Text + "%'"
        S_listBox.DataSource = Dv
    End Sub
End Class