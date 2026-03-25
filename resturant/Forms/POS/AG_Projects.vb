Public Class AG_Projects
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
        Dim c As New C
        Dim sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
        Using (sqlCon)
            Dim sqlComm As New SqlClient.SqlCommand()
            sqlComm.Connection = sqlCon
            sqlComm.CommandText = "[AG_Projects_Insert]"
            sqlComm.CommandType = CommandType.StoredProcedure

            sqlComm.Parameters.AddWithValue("@St_Name", SNameTextBox.Text)
            If FormType = 1 Then
                sqlComm.Parameters.AddWithValue("@AG_ID", F_Sales.AG_ID)
            Else
                sqlComm.Parameters.AddWithValue("@AG_ID", F_ViewBill.AG_ID)
            End If

            sqlCon.Open()
            Try
                sqlComm.ExecuteNonQuery()
                MsgBox("تـــم إضافة الخدمة ", MsgBoxStyle.Information)
               After_Update
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            sqlCon.Close()
        End Using
    End Sub

    Private Sub STORES_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = " خدمات العميل : " + F_Sales.AG_SH_txt.Text
        Load_StoreData()
    End Sub

    Public Sub Load_StoreData()
        Dim c As New C
        Dim S As String = ""
        If FormType = 1 Then
            S = "SELECT Proj_ID,Proj_NAME from AG_Projects_V WHERE AG_ID = '" & F_Sales.AG_ID & "' ORDER BY Proj_ID DESC"
        Else
            S = "SELECT Proj_ID,Proj_NAME from AG_Projects_V WHERE AG_ID = '" & F_ViewBill.AG_ID & "' ORDER BY Proj_ID DESC"
        End If

        c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
        c.Da.Fill(c.Dt)
        S_listBox.DataSource = c.Dt
        S_listBox.ValueMember = "Proj_ID"
        S_listBox.DisplayMember = "Proj_NAME"
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
        Dim c As New C
        Dim sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
        Using (sqlCon)
            Dim sqlComm As New SqlClient.SqlCommand()
            c.Com = New SqlClient.SqlCommand
            sqlComm.Connection = sqlCon
            sqlComm.CommandText = "[AG_Projects_Update]"
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.AddWithValue("@ST_ID", S_ID)
            sqlComm.Parameters.AddWithValue("@St_Name", SNameTextBox.Text)
            sqlCon.Open()
            Try
                sqlComm.ExecuteNonQuery()
                MsgBox("تـم تعديل الخدمة ", MsgBoxStyle.Information)
                After_Update()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            sqlCon.Close()
        End Using
    End Sub

    Private Sub After_Update()
        If FormType = 1 Then
            F_Sales.Fill_All_AG_Proj()
        Else
            F_ViewBill.Fill_All_AG_Proj()
        End If
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

        Dim sql As String = "select * from AG_Projects where Proj_ID = '" & S_listBox.SelectedValue & "'"
        Dim com As New SqlClient.SqlCommand(sql, c1.Con)

        c1.Con.Open()
        Try

            c1.Dr = com.ExecuteReader
            If c1.Dr.HasRows Then
                c1.Dr.Read()
                SNameTextBox.Text = c1.Dr("Proj_NAME")
                S_Name = c1.Dr("Proj_NAME")
                S_ID = c1.Dr("Proj_ID")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
    End Sub

    Private Sub DeleteSButton_Click(sender As Object, e As EventArgs) Handles DeleteSButton.Click

        If MessageBox.Show(" تأكيد حذف " + S_Name, "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            S_Delete()
        End If

    End Sub


    Public Sub S_Delete()
        Dim c As New C
        Dim sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
        Using (sqlCon)
            Dim sqlComm As New SqlClient.SqlCommand()
            c.Com = New SqlClient.SqlCommand
            sqlComm.Connection = sqlCon
            sqlComm.CommandText = "[AG_Projects_Delete]"
            sqlComm.CommandType = CommandType.StoredProcedure
            sqlComm.Parameters.AddWithValue("ST_ID", S_ID)
            sqlCon.Open()
            Try
                sqlComm.ExecuteNonQuery()
                MsgBox("تـم حذف الخدمة ", MsgBoxStyle.Information)
                After_Update()
                Load_StoreData()
                SNameTextBox.Enabled = False
                SNameTextBox.Clear()
                DeleteSButton.Enabled = False
                SaveSButton.Enabled = False
                EditSButton.Enabled = False
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

    Private Sub Confirm_Btn_Click(sender As Object, e As EventArgs) Handles Confirm_Btn.Click
        If S_listBox.SelectedValue > 0 Then Switch_Pro()
    End Sub

    Public Sub Switch_Pro()
        Dim sqlComm As New SqlClient.SqlCommand
        sqlComm.CommandText = "AG_Balance_Update_AG_Project"
        sqlComm.CommandType = CommandType.StoredProcedure

        If FormType = 1 Then
            sqlComm.Parameters.AddWithValue("@T_ID", F_Sales.T_ID)
        Else
            sqlComm.Parameters.AddWithValue("@T_ID", F_ViewBill.T_ID)
        End If

        sqlComm.Parameters.AddWithValue("@Proj_ID", S_listBox.SelectedValue)
      
        If SQL_SP_EXEC(sqlComm) = True Then
            If FormType = 1 Then
                F_Sales.Fill_All_AG_Proj()
                F_Sales.Project_cm.SelectedValue = S_listBox.SelectedValue
            Else
                F_ViewBill.Fill_All_AG_Proj()
                F_ViewBill.Project_cm.SelectedValue = S_listBox.SelectedValue
            End If
            
            Me.Close()
        End If
    End Sub

End Class