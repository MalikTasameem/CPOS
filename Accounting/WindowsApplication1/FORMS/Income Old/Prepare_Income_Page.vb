
Imports System.Data.SqlClient
Public Class Prepare_Income_Page


    Dim connectionString As String = MY_Settings.SqlConStr
    Dim connection As New SqlConnection(connectionString)
    Dim ACC_CODE_DT As New DataTable

    ' فتح الاتصال
    Sub OpenConnection()
        If connection.State = ConnectionState.Closed Then
            connection.Open()
        End If

    End Sub
    Private Sub Prepare_Income_Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        OpenConnection()
        LoadData()
        LoadDetailsData()
        LOAD_ALL_BALANCES()

        txtGroupCalc.SelectedIndex = 0
        txtGroupCalcDetails.SelectedIndex = 0
        txtGroupCalc_1_2.SelectedIndex = 0
        txtGroupCalc_2.SelectedIndex = 0

        LoadDataIntoDataGridView()
    End Sub

    Private Sub LoadDataIntoDataGridView()
        Try
            Using connection As New SqlConnection(connectionString)
                Dim query As String = "SELECT * FROM [dbo].[Income_Group_Results]"
                Dim adapter As New SqlDataAdapter(query, connection)
                Dim table As New DataTable()
                adapter.Fill(table)
                DataGridView_Result.DataSource = table
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub


    Private Sub LOAD_ALL_BALANCES()

        'Dim DT As New DataTable
        'Dim C As New C
        'Dim da As New SqlClient.SqlDataAdapter("select ACC_CODE , ACC_NAME  from ACCOUNTS_TREE ", C.Con)
        'da.Fill(DT)

        'B_Name_Cm.DataSource = DT
        'B_Name_Cm.DisplayMember = "ACC_NAME"
        'B_Name_Cm.ValueMember = "ACC_CODE"

        ACC_CODE_DT.Clear()
        ACC_CODE_DT = Accounts_Datatable
    End Sub

    Private Sub B_NUM_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles B_NUM_txt.KeyDown
        If e.KeyCode = Keys.Return Then If ACC_CODE_DT.Rows.Count > 0 Then B_Name_Cm.SelectedValue = B_NUM_txt.Text
    End Sub


    Private Sub Fill_Income_Group_Masters()
        Dim DT As New DataTable
        Dim C As New C
        Dim da As New SqlClient.SqlDataAdapter("select Group_ID , Group_Name  from Income_Group_Master ", C.Con)
        da.Fill(DT)
        Master_CM.DataSource = DT
        Master_CM.DisplayMember = "Group_Name"
        Master_CM.ValueMember = "Group_ID"
    End Sub


    Sub LoadDetailsData()
        If DataGridView_Master.Rows.Count > 0 Then
            Dim query As String = " SELECT [T_ID]
      ,[Group_ID]
      ,[Group_Calc]
      ,[ACC_CODE]
      ,[ACC_NAME]
      ,[Group_Name]
  FROM [Income_Group_Details_V]  WHERE Group_ID = " & DataGridView_Master.CurrentRow.Cells("Group_ID_CL").Value
            Dim adapter As New SqlDataAdapter(query, connection)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView_Details.DataSource = table
        End If

    End Sub

    Sub LoadData()
        Dim query As String = "SELECT * FROM Income_Group_Master"
        Dim adapter As New SqlDataAdapter(query, connection)
        Dim table As New DataTable()
        adapter.Fill(table)
        DataGridView_Master.DataSource = table
        Fill_Income_Group_Masters()
    End Sub


    Sub AddData(groupID As Integer, groupName As String, groupCalc As Char, resultTitle As String, resultCode As String)
        Dim query As String = "INSERT INTO Income_Group_Master (Group_ID, Group_Name, Group_Calc, Result_Title, Result_Code) VALUES (@GroupID, @GroupName, @GroupCalc, @ResultTitle, @ResultCode)"
        Dim command As New SqlCommand(query, connection)
        command.Parameters.AddWithValue("@GroupID", groupID)
        command.Parameters.AddWithValue("@GroupName", groupName)
        command.Parameters.AddWithValue("@GroupCalc", groupCalc)
        command.Parameters.AddWithValue("@ResultTitle", resultTitle)
        command.Parameters.AddWithValue("@ResultCode", resultCode)
        OpenConnection()
        command.ExecuteNonQuery()
        MessageBox.Show("Data Added Successfully!")
        LoadData()
    End Sub

    ' ربطها مع زر الإضافة
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        AddData(Convert.ToInt32(txtGroupID.Text), txtGroupName.Text, Convert.ToChar(txtGroupCalc.Text), txtResultTitle.Text, txtResultCode.Text)
    End Sub


    Sub UpdateData(groupID As Integer, groupName As String, groupCalc As Char, resultTitle As String, resultCode As String)
        Dim query As String = "UPDATE Income_Group_Master SET Group_Name = @GroupName, Group_Calc = @GroupCalc, Result_Title = @ResultTitle, Result_Code = @ResultCode WHERE Group_ID = @GroupID"
        Dim command As New SqlCommand(query, connection)
        command.Parameters.AddWithValue("@GroupID", groupID)
        command.Parameters.AddWithValue("@GroupName", groupName)
        command.Parameters.AddWithValue("@GroupCalc", groupCalc)
        command.Parameters.AddWithValue("@ResultTitle", resultTitle)
        command.Parameters.AddWithValue("@ResultCode", resultCode)
        OpenConnection()
        command.ExecuteNonQuery()
        MessageBox.Show("Data Updated Successfully!")
        LoadData()
    End Sub

    ' ربطها مع زر التعديل
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        UpdateData(Convert.ToInt32(txtGroupID.Text), txtGroupName.Text, Convert.ToChar(txtGroupCalc.Text), txtResultTitle.Text, txtResultCode.Text)
    End Sub


    Sub DeleteData(groupID As Integer)
        Dim query As String = "DELETE FROM Income_Group_Master WHERE Group_ID = @GroupID"
        Dim command As New SqlCommand(query, connection)
        command.Parameters.AddWithValue("@GroupID", groupID)
        OpenConnection()
        command.ExecuteNonQuery()
        MessageBox.Show("Data Deleted Successfully!")
        LoadData()
    End Sub

    ' ربطها مع زر الحذف
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData(Convert.ToInt32(txtGroupID.Text))
    End Sub


    Private Sub DataGridView_Master_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView_Master.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView_Master.Rows(e.RowIndex)
            txtGroupID.Text = row.Cells("Group_ID_CL").Value.ToString()
            txtGroupName.Text = row.Cells("Group_Name_CL").Value.ToString()
            txtGroupCalc.Text = row.Cells("Group_Calc_CL").Value.ToString()
            txtResultTitle.Text = row.Cells("Result_Title_CL").Value.ToString()
            txtResultCode.Text = row.Cells("Result_Code_CL").Value.ToString()

            LoadDetailsData()
        End If
    End Sub
    '-------------------------------------------------------------------------------------------------------------------------------------------

    Sub AddDetailsData(groupID As String, groupCalc As Char, accCode As Integer) 'tID As Integer,
        Dim query As String = " DECLARE @T_ID INT ; EXEC @T_ID = AA_GET_MAX_ID 'Income_Group_Details'  ; INSERT INTO Income_Group_Details (T_ID, Group_ID, Group_Calc, ACC_CODE) VALUES (@T_ID, @GroupID, @GroupCalc, @AccCode) "
        Dim command As New SqlCommand(query, connection)
        'command.Parameters.AddWithValue("@TID", tID)
        command.Parameters.AddWithValue("@GroupID", groupID)
        command.Parameters.AddWithValue("@GroupCalc", groupCalc)
        command.Parameters.AddWithValue("@AccCode", accCode)
        'command.Parameters.AddWithValue("@AccName", accName)
        'command.Parameters.AddWithValue("@AccBalance", accBalance)
        OpenConnection()
        command.ExecuteNonQuery()
        MessageBox.Show("Details Added Successfully!")
        LoadDetailsData()
    End Sub

    ' ربطها مع زر الإضافة
    Private Sub btnAddDetails_Click(sender As Object, e As EventArgs) Handles btnAddDetails.Click

        If DataGridView_Master.Rows.Count > 0 Then
            AddDetailsData(Master_CM.SelectedValue, Convert.ToChar(txtGroupCalcDetails.Text), B_Name_Cm.SelectedValue) 'Convert.ToInt32(txtTID.Text),
        End If

    End Sub



    Sub UpdateDetailsData(tID As Integer, groupID As String, groupCalc As Char, accCode As Integer)
        Dim query As String = "UPDATE Income_Group_Details SET Group_ID = @GroupID, Group_Calc = @GroupCalc, ACC_CODE = @AccCode WHERE T_ID = @TID"
        Dim command As New SqlCommand(query, connection)
        command.Parameters.AddWithValue("@TID", tID)
        command.Parameters.AddWithValue("@GroupID", groupID)
        command.Parameters.AddWithValue("@GroupCalc", groupCalc)
        command.Parameters.AddWithValue("@AccCode", accCode)
        'command.Parameters.AddWithValue("@AccName", accName)
        'command.Parameters.AddWithValue("@AccBalance", accBalance)
        OpenConnection()
        command.ExecuteNonQuery()
        MessageBox.Show("Details Updated Successfully!")
        LoadDetailsData()
    End Sub

    ' ربطها مع زر التعديل
    Private Sub btnUpdateDetails_Click(sender As Object, e As EventArgs) Handles btnUpdateDetails.Click
        UpdateDetailsData(Convert.ToInt32(txtTID.Text), Master_CM.SelectedValue, Convert.ToChar(txtGroupCalcDetails.Text), B_Name_Cm.SelectedValue)
    End Sub

    Sub DeleteDetailsData(tID As Integer)
        Dim query As String = "DELETE FROM Income_Group_Details WHERE T_ID = @TID"
        Dim command As New SqlCommand(query, connection)
        command.Parameters.AddWithValue("@TID", tID)
        OpenConnection()
        command.ExecuteNonQuery()
        MessageBox.Show("Details Deleted Successfully!")
        LoadDetailsData()
    End Sub

    ' ربطها مع زر الحذف
    Private Sub btnDeleteDetails_Click(sender As Object, e As EventArgs) Handles btnDeleteDetails.Click
        DeleteDetailsData(Convert.ToInt32(txtTID.Text))
    End Sub


    Private Sub DataGridView_Details_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView_Details.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView_Details.Rows(e.RowIndex)
            txtTID.Text = row.Cells("T_ID_CL").Value.ToString()
            Master_CM.SelectedValue = row.Cells("Group_ID_CL_2").Value.ToString()
            txtGroupCalcDetails.Text = row.Cells("Group_Calc_CL_2").Value.ToString()
            B_Name_Cm.SelectedValue = row.Cells("ACC_CODE_CL").Value.ToString()
            'txtAccName.Text = row.Cells("ACC_NAME").Value.ToString()
            'txtAccBalance.Text = row.Cells("ACC_BALANCE").Value.ToString()
        End If
    End Sub

    Private Sub SHOW_Button_Click(sender As Object, e As EventArgs) Handles SHOW_Button.Click
        Income_EXAMPLE.ShowDialog()
    End Sub
    '-----------------------------------------------------------------------------------------------------------------

    Private Sub DeleteRecord(T_ID As Integer)
        Try
            Using connection As New SqlConnection(connectionString)
                Dim query As String = "DELETE FROM [dbo].[Income_Group_Results] WHERE T_ID = @T_ID"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@T_ID", T_ID)
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Record deleted successfully!")
                    LoadDataIntoDataGridView()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    'Private Sub UpdateRecord(T_ID As Integer, RESULT_TITLE As String, RESULT_ONE_CODE As Integer, Calc_BETWEEN_ONE_TWO As Char, RESULT_TWO_CODE As Integer, RESULT_Calc As Char, RESULT_CODE As Integer, Column_Num As Integer)
    '    Try
    '        Using connection As New SqlConnection(connectionString)
    '            Dim query As String = "UPDATE [dbo].[Income_Group_Results] SET RESULT_TITLE = @RESULT_TITLE, RESULT_ONE_CODE = @RESULT_ONE_CODE, Calc_BETWEEN_ONE_TWO = @Calc_BETWEEN_ONE_TWO, RESULT_TWO_CODE = @RESULT_TWO_CODE, RESULT_Calc = @RESULT_Calc, RESULT_CODE = @RESULT_CODE, Column_Num = @Column_Num WHERE T_ID = @T_ID"
    '            Using command As New SqlCommand(query, connection)
    '                command.Parameters.AddWithValue("@T_ID", T_ID)
    '                command.Parameters.AddWithValue("@RESULT_TITLE", RESULT_TITLE)
    '                command.Parameters.AddWithValue("@RESULT_ONE_CODE", RESULT_ONE_CODE)
    '                command.Parameters.AddWithValue("@Calc_BETWEEN_ONE_TWO", Calc_BETWEEN_ONE_TWO)
    '                command.Parameters.AddWithValue("@RESULT_TWO_CODE", RESULT_TWO_CODE)
    '                'command.Parameters.AddWithValue("@BALANCE", BALANCE)
    '                command.Parameters.AddWithValue("@RESULT_Calc", RESULT_Calc)
    '                command.Parameters.AddWithValue("@RESULT_CODE", RESULT_CODE)
    '                command.Parameters.AddWithValue("@Column_Num", Column_Num)
    '                connection.Open()
    '                command.ExecuteNonQuery()
    '                MessageBox.Show("Record updated successfully!")
    '                LoadDataIntoDataGridView()
    '            End Using
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show("Error: " & ex.Message)
    '    End Try
    'End Sub


    Private Sub InsertRecord(T_ID As Integer, RESULT_TITLE As String, RESULT_ONE_CODE As String, Calc_BETWEEN_ONE_TWO As Char, RESULT_TWO_CODE As String, RESULT_Calc As Char, RESULT_CODE As String, Column_Num As Integer)
        Try
            Using connection As New SqlConnection(connectionString)
                Dim query As String = "INSERT INTO [dbo].[Income_Group_Results] (T_ID, RESULT_TITLE, RESULT_ONE_CODE, Calc_BETWEEN_ONE_TWO, RESULT_TWO_CODE, RESULT_Calc, RESULT_CODE, Column_Num) VALUES (@T_ID, @RESULT_TITLE, @RESULT_ONE_CODE, @Calc_BETWEEN_ONE_TWO, @RESULT_TWO_CODE, @RESULT_Calc, @RESULT_CODE, @Column_Num)"
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@T_ID", T_ID)
                    command.Parameters.AddWithValue("@RESULT_TITLE", RESULT_TITLE)
                    command.Parameters.AddWithValue("@RESULT_ONE_CODE", RESULT_ONE_CODE)
                    command.Parameters.AddWithValue("@Calc_BETWEEN_ONE_TWO", Calc_BETWEEN_ONE_TWO)
                    command.Parameters.AddWithValue("@RESULT_TWO_CODE", RESULT_TWO_CODE)
                    command.Parameters.AddWithValue("@RESULT_Calc", RESULT_Calc)
                    command.Parameters.AddWithValue("@RESULT_CODE", RESULT_CODE)
                    command.Parameters.AddWithValue("@Column_Num", Column_Num)
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Record inserted successfully!")
                    LoadDataIntoDataGridView()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub ADD_result_Btn_Click(sender As Object, e As EventArgs) Handles ADD_result_Btn.Click
        InsertRecord(txtTID_result.Text, txtResultTitle_Result.Text, txtResultCode_1.Text, txtGroupCalc_1_2.Text, txtResultCode_2.Text, txtGroupCalc_2.Text, txtResultCode_result.Text, Line_txtResult.Text)
    End Sub

    Private Sub DELETE_result_Btn_Click(sender As Object, e As EventArgs) Handles DELETE_result_Btn.Click
        If DataGridView_Result.Rows.Count > 0 Then DeleteRecord(DataGridView_Result.CurrentRow.Cells(0).Value)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub B_NUM_txt_TextChanged(sender As Object, e As EventArgs) Handles B_NUM_txt.TextChanged

        If B_NUM_txt.Text.Count > 0 Then
            Filter_B()
        Else
            LOAD_ALL_BALANCES()
        End If
    End Sub


    Private Sub Filter_B()

        ACC_CODE_DT = GetAccountTreeDataTable(Accounts_Datatable, B_NUM_txt.Text)

        B_Name_Cm.DataSource = ACC_CODE_DT
        B_Name_Cm.DisplayMember = "ACC_NAME"
        B_Name_Cm.ValueMember = "ACC_CODE"
        B_Name_Cm.DroppedDown = True
        If ACC_CODE_DT.Rows.Count = 0 Then B_Name_Cm.Text = ""

    End Sub

    Private Sub Add_all_Btn_Click(sender As Object, e As EventArgs) Handles Add_all_Btn.Click


        If DataGridView_Master.Rows.Count > 0 Then

            If MessageBox.Show(" سيتم إضافة كافة الحسابات الأبناء التي تحت الحساب ... " & B_Name_Cm.Text & " مباشرة ... هل انت متأكد ", "تاكيــد العملية", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.OK Then

                Income_Setting_Move_By_ACC_CODE(Master_CM.SelectedValue, Convert.ToChar(txtGroupCalcDetails.Text), B_Name_Cm.SelectedValue)

            End If
        End If

    End Sub

    Private Sub Income_Setting_Move_By_ACC_CODE(groupID As String, groupCalc As Char, accCode As Integer)
        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "[Income_Setting_Move_By_ACC_CODE]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@GroupID", groupID)
            .Parameters.AddWithValue("@GroupCalc", groupCalc)
            .Parameters.AddWithValue("@ACC_CODE", accCode)
            .Parameters.AddWithValue("@isOverWrite", isOverWrite_CB.Checked)

            Try
                C.Con.Open()
                C.Com.ExecuteNonQuery()
                'T_ID = C.Com.ExecuteScalar()
                C.Con.Close()
                MsgBox("تم التطبيق", MsgBoxStyle.Information, "")
                LoadDetailsData()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End With
    End Sub

End Class