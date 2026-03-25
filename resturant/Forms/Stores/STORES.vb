Public Class STORES
    Dim S_ID As Integer
    Dim S_Name As String
    Private Sub NewEmpButton_Click(sender As Object, e As EventArgs) Handles NewSButton.Click
        SaveSButton.Enabled = True
        EditSButton.Enabled = False
        DeleteSButton.Enabled = False
        SNameTextBox.Clear()
        SNameTextBox.Enabled = True
        SNameTextBox.Select()
        Saler_Percent_txt.Enabled = True
        Saler_Percent_txt.Clear()
        EditSButton.Text = "تعديل"
    End Sub

    Private Sub SaveSButton_Click(sender As Object, e As EventArgs) Handles SaveSButton.Click
        If String.IsNullOrWhiteSpace(SNameTextBox.Text) = False Then
            Store_Insert()
            SNameTextBox.Clear()
            Saler_Percent_txt.Clear()
            Load_StoreData()
        End If
    End Sub
    Public Sub Store_Insert()
        Dim c As New C
        Dim sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
        Using (sqlCon)
            Dim sqlComm As New SqlClient.SqlCommand()
            sqlComm.Connection = sqlCon
            sqlComm.CommandText = "Store_Insert"
            sqlComm.CommandType = CommandType.StoredProcedure

            sqlComm.Parameters.AddWithValue("@St_Name", SNameTextBox.Text)
            If Not String.IsNullOrWhiteSpace(Saler_Percent_txt.Text) Then sqlComm.Parameters.AddWithValue("@Saler_Percent", Saler_Percent_txt.Text)
            sqlCon.Open()
            Try
                sqlComm.ExecuteNonQuery()
                MsgBox("تـــم إضافة المخـــزن ", MsgBoxStyle.Information)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            sqlCon.Close()
        End Using
    End Sub

    Private Sub STORES_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        Load_StoreData()
    End Sub

    Public Sub Load_StoreData()
        Dim c As New C
        Dim s As String = "select ST_ID,St_Name from Stores"
        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
        Dim dt As New DataTable
        c.Da.Fill(dt)
        S_listBox.DataSource = dt
        S_listBox.DisplayMember = "St_Name"
        S_listBox.ValueMember = "ST_ID"
    End Sub

    Private Sub EditSButton_Click(sender As Object, e As EventArgs) Handles EditSButton.Click
        If EditSButton.Text = "تعديل" Then
            SNameTextBox.Enabled = True
            Saler_Percent_txt.Enabled = True
            DeleteSButton.Enabled = False
            SaveSButton.Enabled = False
            EditSButton.Text = "تأكيد التعديل"
        Else
            If String.IsNullOrWhiteSpace(SNameTextBox.Text) = False Then
                Store_Update()
                Me.Load_StoreData()
                SNameTextBox.Clear()
                SNameTextBox.Enabled = False
                Saler_Percent_txt.Clear()
                Saler_Percent_txt.Enabled = False
                EditSButton.Text = "تعديل"
            End If
        End If
    End Sub

    Public Sub Store_Update()
        Dim c As New C
        Dim sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
        Using (sqlCon)
            Dim sqlComm As New SqlClient.SqlCommand()
            c.Com = New SqlClient.SqlCommand
            sqlComm.Connection = sqlCon
            sqlComm.CommandText = "Store_Update"
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.AddWithValue("@ST_ID", S_ID)
            sqlComm.Parameters.AddWithValue("@St_Name", SNameTextBox.Text)
            If Not String.IsNullOrWhiteSpace(Saler_Percent_txt.Text) Then sqlComm.Parameters.AddWithValue("@Saler_Percent", Saler_Percent_txt.Text)
            sqlCon.Open()
            Try
                sqlComm.ExecuteNonQuery()
                MsgBox("تـم تعديل المخـزن ", MsgBoxStyle.Information)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            sqlCon.Close()
        End Using
    End Sub

    Private Sub S_listBox_MouseClick(sender As Object, e As MouseEventArgs) Handles S_listBox.MouseClick
        Select_Store()
        SNameTextBox.Enabled = False
        Saler_Percent_txt.Enabled = False
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

        Dim sql As String = "select * from Stores where ST_ID ='" & S_listBox.SelectedValue & "'"
        Dim com As New SqlClient.SqlCommand(sql, c1.Con)

        c1.Con.Open()
        Try

            c1.Dr = com.ExecuteReader
            If c1.Dr.HasRows Then
                c1.Dr.Read()
                SNameTextBox.Text = c1.Dr("St_Name")
                Saler_Percent_txt.Text = c1.Dr("Saler_Percent")
                S_Name = c1.Dr("St_Name")
                S_ID = c1.Dr("ST_ID")

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
    End Sub

    Private Sub DeleteSButton_Click(sender As Object, e As EventArgs) Handles DeleteSButton.Click

        If MessageBox.Show(" تأكيد حذف " + S_Name, "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            S_Delete()
            Load_StoreData()
            SNameTextBox.Enabled = False
            Saler_Percent_txt.Enabled = False
            SNameTextBox.Clear()
            Saler_Percent_txt.Clear()
            DeleteSButton.Enabled = False
            SaveSButton.Enabled = False
            EditSButton.Enabled = False
        End If

    End Sub

    Public Function check_StoreQTY()

        Dim c1 As New C
        Dim F As Boolean = False

        Dim sql As String = "select  Sum(QTY) AS 'SumQ' from Stores_Balance where Store_ID ='" & S_ID & "'"
        Dim com As New SqlClient.SqlCommand(sql, c1.Con)

        c1.Con.Open()
        Try

            c1.Dr = com.ExecuteReader
            If c1.Dr.HasRows Then
                c1.Dr.Read()

                If IsDBNull(c1.Dr("SumQ")) Or c1.Dr("SumQ") = 0 Then
                    F = False
                Else
                    F = True
                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()

        Return F

    End Function

    Public Sub S_Delete()
        Dim c As New C
        Dim sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
        Using (sqlCon)
            Dim sqlComm As New SqlClient.SqlCommand()
            c.Com = New SqlClient.SqlCommand
            sqlComm.Connection = sqlCon
            sqlComm.CommandText = "Store_DELETE"
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.AddWithValue("ST_ID", S_ID)
            sqlCon.Open()
            Try
                sqlComm.ExecuteNonQuery()
                MsgBox("تـم حذف المخـزن ", MsgBoxStyle.Information)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            sqlCon.Close()
        End Using
    End Sub

    Private Sub SNameTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles SNameTextBox.KeyDown
        If e.KeyCode = Keys.Return Then
            SaveSButton_Click(sender, e)
        End If
    End Sub

    Private Sub Saler_Percent_txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Saler_Percent_txt.KeyPress
        Check_Only_Float(sender, e)
    End Sub

    Private Sub Saler_Percent_txt_TextChanged(sender As Object, e As EventArgs) Handles Saler_Percent_txt.TextChanged
        Check_Point_in_FloatNum(sender, e)
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub STORES_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub
End Class