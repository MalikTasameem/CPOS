Public Class Notes
    Dim S_ID As Integer
    Dim S_Name As String
    Private Sub Components_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThemeManager.ApplyThemeToForm(Me)
        Load_StoreData()
        Load_GM_Groups()
    End Sub


    Public Sub Load_GM_Groups()
        Dim c As New C
        Dim s As String = "select Grp_ID,Grp_Name from Comp_Groups"
        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
        Dim dt As New DataTable
        c.Da.Fill(dt)
        GM_Group_CM.DataSource = dt
        GM_Group_CM.DisplayMember = "Grp_Name"
        GM_Group_CM.ValueMember = "Grp_ID"
    End Sub
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Const CS_DROPSHADOW As Integer = &H20000
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property
    Public Sub Load_StoreData()
        Dim c As New C
        Dim s As String = "select Comp_ID,Comp_Name from GM_Components ORDER BY Comp_ID Desc"
        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
        Dim dt As New DataTable
        c.Da.Fill(dt)
        S_listBox.DataSource = dt
        S_listBox.DisplayMember = "Comp_Name"
        S_listBox.ValueMember = "Comp_ID"
    End Sub

    Private Sub NewSButton_Click(sender As Object, e As EventArgs) Handles NewSButton.Click
        SaveSButton.Enabled = True
        EditSButton.Enabled = False
        DeleteSButton.Enabled = False
        SNameTextBox.Clear()
        SNameTextBox.Enabled = True
        Price_txt.Enabled = True
        EditSButton.Text = "تعديل"
        Price_txt.Clear()
        SNameTextBox.Select()
    End Sub

    Private Sub SaveSButton_Click(sender As Object, e As EventArgs) Handles SaveSButton.Click
        If String.IsNullOrWhiteSpace(SNameTextBox.Text) = False Then Store_Insert()
    End Sub

    Public Sub Store_Insert()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Compon_Insert"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Comp_Name", SNameTextBox.Text)
            .Parameters.AddWithValue("@Grp_ID", GM_Group_CM.SelectedValue)
            If ADD_Rd.Checked = True Then .Parameters.AddWithValue("@is_ADD", 1)

            If Not String.IsNullOrWhiteSpace(Price_txt.Text) Then .Parameters.AddWithValue("@Price", Price_txt.Text)
        End With
        If SQL_SP_EXEC(C.Com) = True Then
            SNameTextBox.Clear()
            Price_txt.Clear()
            Load_StoreData()
            SNameTextBox.Select()
        End If

    End Sub
    Private Sub EditSButton_Click(sender As Object, e As EventArgs) Handles EditSButton.Click
        If EditSButton.Text = "تعديل" Then
            SNameTextBox.Enabled = True
            Price_txt.Enabled = True
            DeleteSButton.Enabled = False
            SaveSButton.Enabled = False
            EditSButton.Text = "تأكيد التعديل"
        Else
            If String.IsNullOrWhiteSpace(SNameTextBox.Text) = False Then
                Store_Update()
                Me.Load_StoreData()
                SNameTextBox.Clear()
                Price_txt.Clear()
                SNameTextBox.Enabled = False
                Price_txt.Enabled = False
                EditSButton.Text = "تعديل"
            End If
        End If
    End Sub

    Private Sub DeleteSButton_Click(sender As Object, e As EventArgs) Handles DeleteSButton.Click
        Beep()
        If MessageBox.Show(" تأكيد حذف " + S_Name, "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            S_Delete()
            Load_StoreData()
            SNameTextBox.Enabled = False
            SNameTextBox.Clear()
            Price_txt.Clear()
            DeleteSButton.Enabled = False
            SaveSButton.Enabled = False
            EditSButton.Enabled = False
        End If
    End Sub

    Public Sub Store_Update()
        Dim C As New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Compon_Update"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Comp_ID", S_ID)
            .Parameters.AddWithValue("@Comp_Name", SNameTextBox.Text)
            .Parameters.AddWithValue("@Grp_ID", GM_Group_CM.SelectedValue)
            If Not String.IsNullOrWhiteSpace(Price_txt.Text) Then .Parameters.AddWithValue("@Price", Price_txt.Text)
            If ADD_Rd.Checked = True Then .Parameters.AddWithValue("@is_ADD", 1)
        End With
        SQL_SP_EXEC(C.Com)
    End Sub

    Public Sub S_Delete()
        Dim C = New C
        With C.Com
            .Connection = C.Con
            .CommandText = "Compon_Delete"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Comp_ID", S_ID)
        End With
        SQL_SP_EXEC(C.Com)
    End Sub

    Private Sub SNameTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles SNameTextBox.KeyDown
        If e.KeyCode = Keys.Return Then Price_txt.Select()
    End Sub


    Private Sub KB_Btn_Click(sender As Object, e As EventArgs)
        Call_KeyBoard()
    End Sub


    Public Sub Select_Store()
        Dim c1 As New C

        Dim sql As String = "select * from Components_V where Comp_ID ='" & S_listBox.SelectedValue & "'"
        Dim com As New SqlClient.SqlCommand(sql, c1.Con)

        c1.Con.Open()
        Try

            c1.Dr = com.ExecuteReader
            If c1.Dr.HasRows Then
                c1.Dr.Read()
                SNameTextBox.Text = c1.Dr("Comp_Name")
                Price_txt.Text = c1.Dr("Price")
                S_Name = c1.Dr("Comp_Name")
                S_ID = c1.Dr("Comp_ID")
                GM_Group_CM.SelectedValue = c1.Dr("Grp_ID")

                If c1.Dr("is_ADD") = True Then
                    ADD_Rd.Checked = True
                Else
                    Without_Rd.Checked = True
                End If


                SNameTextBox.Enabled = False
                Price_txt.Enabled = False
                DeleteSButton.Enabled = True
                EditSButton.Enabled = True
                SaveSButton.Enabled = False
                EditSButton.Text = "تعديل"

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
    End Sub

    Private Sub S_listBox_Click(sender As Object, e As EventArgs) Handles S_listBox.Click
        Select_Store()
    End Sub

    Private Sub Price_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Price_txt.KeyDown
        If e.KeyCode = Keys.Return Then SaveSButton_Click(sender, e)
    End Sub

    Private Sub Price_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Price_txt.KeyPress
        Check_Only_Float_With_Negitave(sender, e)
    End Sub

    Private Sub Price_txt_TextChanged(sender As Object, e As EventArgs) Handles Price_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub


    Private Sub Without_Rd_CheckedChanged(sender As Object, e As EventArgs) Handles Without_Rd.CheckedChanged, ADD_Rd.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub Notes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        GM_Groups.ShowDialog()
        Load_GM_Groups()
    End Sub
End Class