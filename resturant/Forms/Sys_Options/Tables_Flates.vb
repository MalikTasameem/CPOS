Public Class Tables_Flates
    Dim S_ID As Integer
    Dim S_Name As String
    Private Sub Components_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_StoreData()
    End Sub

    Public Sub Load_StoreData()
        Dim c As New C
        Dim s As String = "select Flate_ID,Flate_Name from Tables_Flate ORDER BY Flate_ID ASC"
        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
        Dim dt As New DataTable
        c.Da.Fill(dt)
        S_listBox.DataSource = dt
        S_listBox.DisplayMember = "Flate_Name"
        S_listBox.ValueMember = "Flate_ID"
    End Sub

    Private Sub NewSButton_Click(sender As Object, e As EventArgs) Handles NewSButton.Click
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
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Tables_Flate_Insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Flate_Name", SNameTextBox.Text)
        End With
        SQL_SP_EXEC(C.Com)

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

    Private Sub DeleteSButton_Click(sender As Object, e As EventArgs) Handles DeleteSButton.Click
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

    Public Sub Store_Update()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Tables_Flate_Update"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Flate_ID", S_ID)
            .Parameters.AddWithValue("@Flate_Name", SNameTextBox.Text)
        End With
        SQL_SP_EXEC(C.Com)
    End Sub

    Public Sub S_Delete()
        Dim C = New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Tables_Flate_Delete"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Flate_ID", S_ID)
        End With
        SQL_SP_EXEC(C.Com)
    End Sub

    Private Sub SNameTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles SNameTextBox.KeyDown
        If e.KeyCode = Keys.Return Then
            SaveSButton_Click(sender, e)
        End If
    End Sub


    Private Sub KB_Btn_Click(sender As Object, e As EventArgs)
        Call_KeyBoard()
    End Sub


    Public Sub Select_Store()
        Dim c1 As New C

        Dim sql As String = "select * from Tables_Flate where Flate_ID ='" & S_listBox.SelectedValue & "'"
        Dim com As New SqlClient.SqlCommand(sql, c1.Con)

        c1.Con.Open()
        Try

            c1.Dr = com.ExecuteReader
            If c1.Dr.HasRows Then
                c1.Dr.Read()
                SNameTextBox.Text = c1.Dr("Flate_Name")
                S_Name = c1.Dr("Flate_Name")
                S_ID = c1.Dr("Flate_ID")
                SNameTextBox.Enabled = False
                DeleteSButton.Enabled = True
                EditSButton.Enabled = True
                SaveSButton.Enabled = False
                If S_ID = 1 Then
                    DeleteSButton.Enabled = False
                Else
                    DeleteSButton.Enabled = True
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
    End Sub

    Private Sub S_listBox_Click(sender As Object, e As EventArgs) Handles S_listBox.Click
        Select_Store()

    End Sub

    Private Sub Tables_Flates_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        F_TablesCard.Load_Flates()
        Me.Dispose()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub
End Class