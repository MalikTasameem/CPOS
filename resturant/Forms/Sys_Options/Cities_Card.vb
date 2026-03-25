Public Class Cities_Card
    Dim S_ID As Integer
    Dim S_Name As String
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
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "Cities_insert"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@Ex_ID", 0)
        sqlComm.Parameters.AddWithValue("@Ex_Name", SNameTextBox.Text)
        sqlComm.Parameters("@Ex_ID").Direction = ParameterDirection.Output
        If SQL_SP_EXEC(sqlComm) = True Then MsgBox("تمت إضافة البند", MsgBoxStyle.Information)
    End Sub

    Private Sub STORES_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_StoreData()
    End Sub

    Public Sub Load_StoreData()
        Dim c As New C
        Dim s As String = " select Ex_ID,Ex_Name from Cities_Card WHERE Ex_ID > 1 Order By Ex_ID ASC "
        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
        Dim dt As New DataTable
        c.Da.Fill(dt)
        S_listBox.DataSource = dt
        S_listBox.DisplayMember = "Ex_Name"
        S_listBox.ValueMember = "Ex_ID"
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
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "Cities_Update"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@Ex_ID", S_ID)
        sqlComm.Parameters.AddWithValue("@Ex_Name", SNameTextBox.Text)
        If SQL_SP_EXEC(sqlComm) = True Then
            MsgBox("تم تعديل بيانات البند", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub S_listBox_MouseClick(sender As Object, e As MouseEventArgs) Handles S_listBox.MouseClick
        Select_Store()
        SNameTextBox.Enabled = False
        DeleteSButton.Enabled = True
        EditSButton.Enabled = True
        SaveSButton.Enabled = False
        EditSButton.Text = "تعديل"
        If S_ID = 1 Then
            '    MsgBox(" المخزن " + S_Name + " هوا المخزن الرئيسي للنظام ... لذا لا يمكن  حذفه ", MsgBoxStyle.Information)
            DeleteSButton.Enabled = False
        Else
            DeleteSButton.Enabled = True
        End If
    End Sub

    Public Sub Select_Store()
        Dim c1 As New C

        Dim sql As String = "select * from Cities_Card where Ex_ID ='" & S_listBox.SelectedValue & "'"
        Dim com As New SqlClient.SqlCommand(sql, c1.Con)

        c1.Con.Open()
        Try
            c1.Dr = com.ExecuteReader
            If c1.Dr.HasRows Then
                c1.Dr.Read()
                SNameTextBox.Text = c1.Dr("Ex_Name")
                S_Name = c1.Dr("Ex_Name")
                S_ID = c1.Dr("Ex_ID")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
    End Sub

    Private Sub DeleteSButton_Click(sender As Object, e As EventArgs) Handles DeleteSButton.Click
        Beep()
        If MessageBox.Show(" تأكيد حذف " + S_Name, "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            S_Delete()
            Load_StoreData()
            SNameTextBox.Enabled = False
            SNameTextBox.Clear()
            DeleteSButton.Enabled = False
            SaveSButton.Enabled = False
            EditSButton.Enabled = False
        End If

    End Sub

    Public Sub S_Delete()
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "Cities_Delete"
        sqlComm.CommandType = CommandType.StoredProcedure
        sqlComm.Parameters.AddWithValue("@Ex_ID", S_ID)
        If SQL_SP_EXEC(sqlComm) = True Then MsgBox("تم حـــذف البند", MsgBoxStyle.Information)
    End Sub

    Private Sub SNameTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles SNameTextBox.KeyDown
        If e.KeyCode = Keys.Return Then SaveSButton_Click(sender, e)
    End Sub

    Private Sub Saler_Percent_txt_KeyPress(sender As Object, e As KeyPressEventArgs)
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Saler_Percent_txt_TextChanged(sender As Object, e As EventArgs)
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub STORES_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub
End Class