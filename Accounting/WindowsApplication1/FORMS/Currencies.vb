Public Class Currencies
    Dim S_ID As Integer
    Dim S_Name As String
    Public Form_Name As String, Form_Name_Arabic As String
    Public F_ID As String, F_Name As String, F_DETAILS As String, F_DETAILS_TABLE As String, Checked_Table As String = "", Checked_Table_ID As String = ""
    Dim IM_DT As New DataTable
    Public just_AdminEdit As Boolean = False
    ' Public user_bkr As New user

    Private Sub NewEmpButton_Click(sender As Object, e As EventArgs) Handles NewSButton.Click
        'If just_AdminEdit And user_bkr.U_is_admin = 0 Then
        '    MessageBox.Show("ليس لديك الصلاحيات الادمن لإتمام هذا الاجراء", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End If
        'If user_bkr.U_add = 1 Then
        SaveSButton.Enabled = True
        EditSButton.Enabled = False
        DeleteSButton.Enabled = False
        SNameTextBox.Clear()
        SNameTextBox.Enabled = True
        Currency_Equal_txt.Clear()
        Currency_Equal_txt.Enabled = True
        SNameTextBox.Select()
        EditSButton.Text = "تعديل"
        'Else
        '    MessageBox.Show("ليس لديك الصلاحيات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End If
    End Sub

    Private Sub SaveSButton_Click(sender As Object, e As EventArgs) Handles SaveSButton.Click
        If String.IsNullOrWhiteSpace(SNameTextBox.Text) = False Then
            Store_Insert()
            SNameTextBox.Clear()
            Load_StoreData()
        End If
    End Sub
    Public Sub Store_Insert()



        query("INSERT INTO [" & Form_Name & "]([" & F_ID & "],[" & F_Name & "],[Cr_Equal]) VALUES ( (select MAX([" & F_ID & "])+1 from " & Form_Name & ") ,'" & SNameTextBox.Text & "','" & Currency_Equal_txt.Text & "')")


        'Dim c As New C
        'Dim sqlComm As New SqlClient.SqlCommand()
        'sqlComm.CommandText = Form_Name & "_INSERT"
        'sqlComm.CommandType = CommandType.StoredProcedure
        'sqlComm.Parameters.AddWithValue("@" & F_Name, SNameTextBox.Text)
        'If SQL_SP_EXEC(sqlComm) = True Then
        '    Network_Edit_Tracker_insert(" تـــم إضافة   " & Form_Name_Arabic & " : " & SNameTextBox.Text, 2, 1)
        MsgBox("تـــم إضافة العنصر ", MsgBoxStyle.Information)
        'End If

    End Sub

    Private Sub STORES_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = " قائمة : " & Form_Name_Arabic
        SendMessage(SEARCH_txt.Handle, &H1501, 0, "إبحث عن عنصر")
        Load_StoreData()
    End Sub

    Public Sub Load_StoreData()
        Dim c As New C
        Try

            Dim s As String = "select " & F_ID & "," & F_Name & " from " & Form_Name & "  WHERE " & F_ID & " > 1 ORDER BY " & F_Name & " ASC "
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            IM_DT.Clear()
            c.Da.Fill(IM_DT)

            S_listBox.DataSource = IM_DT
            S_listBox.DisplayMember = F_Name
            S_listBox.ValueMember = F_ID
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
        End Try
    End Sub

    Private Sub EditSButton_Click(sender As Object, e As EventArgs) Handles EditSButton.Click
        'If just_AdminEdit And user_bkr.U_is_admin = 0 Then
        '    MessageBox.Show("ليس لديك الصلاحيات الادمن لإتمام هذا الاجراء", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End If

        'If user_bkr.U_edit = 0 Then
        '    MessageBox.Show("ليس لديك الصلاحيات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Else
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
        'End If
    End Sub

    Public Sub Store_Update()
        query("UPDATE [" & Form_Name & "] SET [" & F_Name & "] = '" & SNameTextBox.Text & "',[Cr_Equal] = " & Currency_Equal_txt.Text & " WHERE [" & F_ID & "] = " & S_ID)

        'Dim c As New C
        'Dim sqlComm As New SqlClient.SqlCommand()
        'c.Com = New SqlClient.SqlCommand
        'sqlComm.CommandText = Form_Name & "_UPDATE"
        'sqlComm.CommandType = CommandType.StoredProcedure
        'sqlComm.Parameters.AddWithValue("@" & F_ID, S_ID)
        'sqlComm.Parameters.AddWithValue("@" & F_Name, SNameTextBox.Text)
        'If SQL_SP_EXEC(sqlComm) = True Then
        'Network_Edit_Tracker_insert(" تـــم تعديل   " & Form_Name_Arabic & " : " & "  من   " & S_Name & " إلى  " & SNameTextBox.Text, 2, 3)
        MsgBox("تـم تعديل العنصر ", MsgBoxStyle.Information)
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
                Currency_Equal_txt.Text = c1.Dr("Cr_Equal")
                S_Name = c1.Dr(F_Name)
                S_ID = c1.Dr(F_ID)

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
        End Try
        c1.Con.Close()
    End Sub

    Private Sub DeleteSButton_Click(sender As Object, e As EventArgs) Handles DeleteSButton.Click
        'If just_AdminEdit And user_bkr.U_is_admin = 0 Then
        '    MessageBox.Show("ليس لديك الصلاحيات الادمن لإتمام هذا الاجراء", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End If
        'If user_bkr.U_delete = 0 Then
        '    MessageBox.Show("ليس لديك الصلاحيات", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Else

        If GET_ID(Checked_Table_ID, Checked_Table, S_ID) = 1 Then
            'If GET_ID(F_DETAILS, "P_Indivduals", S_ID) = 1 Then
            MsgBox("توجد حركة على هذا العنصر", MsgBoxStyle.Exclamation, "إبقاف الحذف")
        Else
            If MessageBox.Show(" تأكيد حذف " + S_Name, "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                S_Delete()
                Load_StoreData()
                SNameTextBox.Enabled = False
                SNameTextBox.Clear()
                DeleteSButton.Enabled = False
                SaveSButton.Enabled = False
                EditSButton.Enabled = False
            End If
        End If

        'End If




        'Else
        'MsgBox("توجد حركة على هذا العنصر", MsgBoxStyle.Exclamation, "إبقاف الحذف")
        'End If

    End Sub



    Public Function GET_ID(COLUMN As String, TABLE As String, ID As Integer)
        Dim C = New C
        Try
            Dim s As String
            s = "Select [" & COLUMN & "] From [" & TABLE & "] WHERE  [" & COLUMN & "] = " & ID
            C.Da = New SqlClient.SqlDataAdapter(s, C.Con)
            C.Da.Fill(C.Dt)

            If C.Dt.Rows.Count > 0 Then
                Return 1
            Else
                Return 0
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return 1
    End Function

    Public Sub S_Delete()

        query("DELETE FROM [" & Form_Name & "]  WHERE [" & F_ID & "] = " & S_ID)

        'Dim c As New C
        'Dim sqlComm As New SqlClient.SqlCommand()
        'c.Com = New SqlClient.SqlCommand
        'sqlComm.CommandText = Form_Name & "_DELETE"
        'sqlComm.CommandType = CommandType.StoredProcedure
        'sqlComm.Parameters.AddWithValue("@" & F_ID, S_ID)

        'If SQL_SP_EXEC(sqlComm) = True Then

        'Network_Edit_Tracker_insert("تـم حذف  " & Form_Name_Arabic & " : " & SNameTextBox.Text, 2, 2)
        MsgBox("تـم حذف العنصر ", MsgBoxStyle.Information)
        '  End If
    End Sub

    Private Sub SNameTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles SNameTextBox.KeyDown
        If e.KeyCode = Keys.Return Then SaveSButton_Click(sender, e)
    End Sub


    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub STORES_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        LOAD_Currencies_Datatable()
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