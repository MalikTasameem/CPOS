Imports System.Data.SqlClient

Public Class Network_Tracker
    Dim IM_DT As New DataTable
    Dim rs As New Resizer

    Private Sub Network_Tracker_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
    Private Sub IM_Valid_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "---- كل المستخدمين ----"))

        Dim c1 As New C
        Dim s As String = "select USER_ID AS ID,UserName AS name from USERS ORDER By UserName ASC"
        c1.Com = New SqlClient.SqlCommand(s, c1.Con)
        c1.Con.Open()
        Try
            c1.Dr = c1.Com.ExecuteReader
            If c1.Dr.HasRows Then
                While c1.Dr.Read
                    mailItems.Add(New MailItem(c1.Dr("ID"), c1.Dr("name")))
                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
        Return mailItems

    End Function

    Public Sub fetch_Screens()
        Screen_Cmb.DataSource = Ge_Screens()
        Screen_Cmb.DisplayMember = "name"
        Screen_Cmb.ValueMember = "ID"
        Screen_Cmb.SelectedIndex = 0
    End Sub


    Function Ge_Screens() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "---- كل الشاشات ----"))

        Dim c1 As New C
        Dim s As String = "select ID AS ID,Type_Name AS name from AgentBalance_Type ORDER By RankNum ASC"
        c1.Com = New SqlClient.SqlCommand(s, c1.Con)
        c1.Con.Open()
        Try
            c1.Dr = c1.Com.ExecuteReader
            If c1.Dr.HasRows Then
                While c1.Dr.Read
                    mailItems.Add(New MailItem(c1.Dr("ID"), c1.Dr("name")))
                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
        Return mailItems

    End Function


    Public Sub fetch_op()
        OP_Cm.DataSource = Ge_op()
        OP_Cm.DisplayMember = "name"
        OP_Cm.ValueMember = "ID"
        OP_Cm.SelectedIndex = 0
    End Sub


    Function Ge_op() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        mailItems.Add(New MailItem(0, "---- كل العمليات ----"))

        Dim c1 As New C
        Dim s As String = "select T_ID AS ID,Operation_Name AS name from Operation_Types"
        c1.Com = New SqlClient.SqlCommand(s, c1.Con)
        c1.Con.Open()
        Try
            c1.Dr = c1.Com.ExecuteReader
            If c1.Dr.HasRows Then
                While c1.Dr.Read
                    mailItems.Add(New MailItem(c1.Dr("ID"), c1.Dr("name")))
                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        c1.Con.Close()
        Return mailItems

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
        Dim c As New C

        Try
            IM_DT.Clear()
            Dim Main_Query As String
            Main_Query = "SELECT ROW_NUMBER() OVER(ORDER BY Date) AS IDX,[T_ID],[CP_Name],[UserName],[Date],[Type_Name],[Operation_Name],[Notes],[Bill_ID],[Screen_Type],[Operation_ID],[User_ID] " & _
            "from Network_Edit_Tracker_V "
            Dim middle As String = " where 1=1 "
            Dim last As String = " order by Date ASC "

            middle = middle & " and CONVERT(Date,Date) BETWEEN '" & DateTimePicker_From.Text & "' AND '" & DateTimePicker_To.Text & "' "

            If Screen_Cmb.SelectedValue > 0 Then middle = middle & " and [Screen_Type]=" & Screen_Cmb.SelectedValue & " "
            If OP_Cm.SelectedValue > 0 Then middle = middle & " and [Operation_ID]=" & OP_Cm.SelectedValue & " "
            If User_Cm.SelectedValue > 0 Then middle = middle & " and [User_ID]=" & User_Cm.SelectedValue & " "
            If Not String.IsNullOrWhiteSpace(Num_Txt.Text) Then middle = middle & " and [Bill_ID]=" & Num_Txt.Text & " "

            Main_Query = Main_Query & middle & last

            c.Da = New SqlClient.SqlDataAdapter(Main_Query, c.Con)
            c.Da.SelectCommand.CommandTimeout = 120
            'c.Da.Fill(IM_DT)
            'DataGridViewX.DataSource = IM_DT

            c.Da.Fill(IM_DT)
            DataB.DataSource = IM_DT
            DataGridViewX.DataSource = DataB


            If CheckedListBox1.Items.Count = 0 Then
                CheckedListBox1.Items.Clear()
                For i As Integer = 0 To DataGridViewX.ColumnCount - 1
                    Dim CL = DataGridViewX.Columns(i).HeaderText
                    CheckedListBox1.Items.Add(CL)
                    CheckedListBox1.SetItemChecked(i, True)
                Next
            End If

            CheckedListBox1.SetItemChecked(1, False)
            CheckedListBox1.SetItemChecked(9, False)
            CheckedListBox1.SetItemChecked(10, False)
            CheckedListBox1.SetItemChecked(11, False)


            TotalQYT_txt.Text = DataGridViewX.Rows.Count
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

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
        Dim Dv As DataView
        Dv = IM_DT.AsDataView
        Dv.RowFilter = "Notes LIKE '%" + sender.Text + "%'"
        DataGridViewX.DataSource = Dv
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim P As New Print_PDF
        P.PRINT_PDF_List(DataGridViewX, CheckedListBox1, " شبكة المراقبة ", 1)
    End Sub
End Class