Imports System.Data.SqlClient

Public Class Network_Tracker
    Dim IM_DT As New DataTable
    Dim rs As New Resizer

    Private Sub Network_Tracker_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
    Private Sub IM_Valid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AuditV2Button.Enabled = User_isAdmin AndAlso USER_ID > 0
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        rs.FindAllControls(Me)
        fetch_USERS()
        fetch_Screens()
        fetch_op()
        SendMessage(CMSearchTextBox.Handle, &H1501, 0, "إبحث في الوصف")
    End Sub

    Public Sub fetch_USERS()
        User_Cm.DataSource = Ge_St_Items()
        User_Cm.DisplayMember = "name"
        User_Cm.ValueMember = "ID"
        User_Cm.SelectedIndex = 0
    End Sub


    Function Ge_St_Items() As List(Of MailItem)
        Return LoadTrackerLookup("SELECT USER_ID AS ID,UserName AS name FROM dbo.Users ORDER BY UserName,USER_ID", "---- كل المستخدمين ----")
    End Function

    Public Sub fetch_Screens()
        Screen_Cmb.DataSource = Ge_Screens()
        Screen_Cmb.DisplayMember = "name"
        Screen_Cmb.ValueMember = "ID"
        Screen_Cmb.SelectedIndex = 0
    End Sub


    Function Ge_Screens() As List(Of MailItem)
        Return LoadTrackerLookup("SELECT ID,Type_Name AS name FROM dbo.AgentBalance_Type ORDER BY RankNum,ID", "---- كل الشاشات ----")
    End Function


    Public Sub fetch_op()
        OP_Cm.DataSource = Ge_op()
        OP_Cm.DisplayMember = "name"
        OP_Cm.ValueMember = "ID"
        OP_Cm.SelectedIndex = 0
    End Sub


    Function Ge_op() As List(Of MailItem)
        Return LoadTrackerLookup("SELECT T_ID AS ID,Operation_Name AS name FROM dbo.Operation_Types ORDER BY T_ID", "---- كل العمليات ----")
    End Function

    'Public Sub Fill_ALL_IM()
    '    Dim C As New C
    '    IM_DT.Clear()
    '    With (C.Com)
    '        .Connection = C.Con
    '        .CommandText = "Network_Edit_Tracker_V_SELECT"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@D_F", DateRange.D_F)
    '        .Parameters.AddWithValue("@D_T", DateRange.D_T)
    '        .Parameters.AddWithValue("@USER_ID", User_Cm.SelectedValue)
    '    End With
    '    C.Da = New SqlClient.SqlDataAdapter(C.Com)
    '    C.Da.Fill(IM_DT)
    '    DataGridViewX.DataSource = IM_DT
    '    Get_Total_QYT()
    'End Sub




    Private Sub Fill_ALL_Network()
        Try
            Dim loaded As New DataTable()
            Using connection As New SqlConnection(MY_Settings.SqlConStr)
                Using command As SqlCommand = NetworkTrackerSearch.BuildCommand(DateTimePicker_From.Value, DateTimePicker_To.Value,
                                                                                GetSelectedFilterId(Screen_Cmb), GetSelectedFilterId(OP_Cm),
                                                                                GetSelectedFilterId(User_Cm), Num_Txt.Text)
                    command.Connection = connection
                    Using adapter As New SqlDataAdapter(command)
                        adapter.Fill(loaded)
                    End Using
                End Using
            End Using

            ' Replace the previous successful result only after loading and filtering succeed.
            loaded.DefaultView.RowFilter = NetworkTrackerSearch.BuildNotesFilter(CMSearchTextBox.Text)
            IM_DT = loaded
            DataGridViewX.AutoGenerateColumns = False
            DataB.DataSource = IM_DT.DefaultView
            DataGridViewX.DataSource = DataB
            If CheckedListBox1.Items.Count <> DataGridViewX.Columns.Count Then
                CheckedListBox1.Items.Clear()
                For Each column As DataGridViewColumn In DataGridViewX.Columns
                    Dim include As Boolean = column.Name <> "T_ID_CL" AndAlso column.Name <> "USER_ID_CL" AndAlso
                                             column.Name <> "Screen_Type_CL" AndAlso column.Name <> "Operation_ID_CL"
                    CheckedListBox1.Items.Add(column.HeaderText, include)
                Next
            End If
            TotalQYT_txt.Text = DataB.Count.ToString()
        Catch ex As Exception
            MsgBox("تعذر تحديث شبكة المراقبة؛ لم تُستبدل نتائج البحث السابقة." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Function GetSelectedFilterId(combo As ComboBox) As Integer
        Dim value As Integer
        If combo.SelectedValue IsNot Nothing AndAlso combo.SelectedValue IsNot DBNull.Value Then
            Integer.TryParse(Convert.ToString(combo.SelectedValue), value)
        End If
        Return value
    End Function

    Private Function LoadTrackerLookup(query As String, allText As String) As List(Of MailItem)
        Dim mailItems As New List(Of MailItem)
        mailItems.Add(New MailItem(0, allText))
        Try
            Using connection As New SqlConnection(MY_Settings.SqlConStr)
                Using command As New SqlCommand(query, connection)
                    connection.Open()
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            mailItems.Add(New MailItem(Convert.ToInt32(reader("ID")), Convert.ToString(reader("name"))))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox("تعذر تحميل خيارات البحث في شبكة المراقبة: " & ex.Message, MsgBoxStyle.Exclamation)
        End Try
        Return mailItems
    End Function

    Private Sub IM_Valid_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub SalarySearchButton_Click(sender As Object, e As EventArgs) Handles SalarySearchButton.Click
        'Fill_ALL_IM()
        Fill_ALL_Network()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub

    Private Sub CMSearchTextBox_TextChanged(sender As Object, e As EventArgs) Handles CMSearchTextBox.TextChanged
        If Not IM_DT.Columns.Contains("Notes") Then Return
        IM_DT.DefaultView.RowFilter = NetworkTrackerSearch.BuildNotesFilter(CMSearchTextBox.Text)
        TotalQYT_txt.Text = IM_DT.DefaultView.Count.ToString()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim P As New Print_PDF
        P.PRINT_PDF_List(DataGridViewX, CheckedListBox1, " شبكة المراقبة ", 1)
    End Sub

    Private Sub AuditV2Button_Click(sender As Object, e As EventArgs) Handles AuditV2Button.Click
        If Not User_isAdmin OrElse USER_ID <= 0 Then
            MsgBox("عرض المراقبة الجديدة متاح للمدير فقط في هذه الدفعة.", MsgBoxStyle.Exclamation)
            Return
        End If
        Using form As New Network_Audit_Browser()
            form.ShowDialog(Me)
        End Using
    End Sub
End Class
