Imports System.Data.SqlClient

Public Class Items_Prices
    Dim rs As New Resizer
    Dim IM_DT As New DataTable
    Dim Get_IM As Boolean = False

    Public is_Update_Valid_D As Boolean = False
    Public is_Update_QTY As Boolean = False
    Public is_Update_Unit_Cost As Boolean = False
    Dim IS_SELECT As Boolean = False

    Private Sub STORES_Explorer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'F_MainForm.Fill_ALL_IM()
        Me.Dispose()
    End Sub
    Private Sub STORES_Explorer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        Zuby.ADGV.AdvancedDataGridView.SetTranslations(Zuby.ADGV.AdvancedDataGridView.LoadTranslationsFromFile(Application.StartupPath & "\" & "lang.json"))

        rs.FindAllControls(Me)
        Me.WindowState = FormWindowState.Maximized
        fetch_ST()
        Get_IM = True
        Check_View_Control()
        ' Make_Hints()
        IS_SELECT = True
        Fill_ALL_IM(0)
        SendMessage(CMSearchTextBox.Handle, &H1501, 0, "إبحث عن صنف")
    End Sub

    'Private Sub Make_Hints()
    '    SendMessage(CMSearchTextBox.Handle, &H1501, 0, "إبحث عن صنف")
    'End Sub

    Public Sub Check_View_Control()

        DataGridViewX.Columns("IM_Num_CL").Visible = MY_Settings.IMPR_IMNUM
        DataGridViewX.Columns("Barcode_CL").Visible = MY_Settings.IMPR_BAR
        DataGridViewX.Columns("Price_2_CL").Visible = MY_Settings.IMPR_MINSP
        DataGridViewX.Columns("Price_3_CL").Visible = MY_Settings.IMPR_MINSP_2
        DataGridViewX.Columns("Cost_CL").Visible = U_SB_Show_IM_COST
    End Sub

    Public Sub fetch_ST()
        ST_cm.DataSource = GetMailItems()
        ST_cm.DisplayMember = "name"
        ST_cm.ValueMember = "ID"
        ST_cm.SelectedIndex = 0
    End Sub

    Function GetMailItems() As List(Of MailItem)

        Dim mailItems = New List(Of MailItem)

        'mailItems.Add(New MailItem(0, "------- كل المخازن -------"))

        Dim c1 As New C
        Dim s As String = "select ST_ID AS ID,ST_name AS name from STORES ORDER By ST_ID ASC"
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


    Public Sub Fill_ALL_IM(IM_ID As Integer)
        If IS_SELECT = True Then

            IM_DT.Clear()
            Dim C As New C
            With C.Com
                .Connection = C.Con
                .CommandText = "Items_Prices_V_SELECT_ALL"
                .CommandType = CommandType.StoredProcedure
                .Parameters.AddWithValue("@St_ID", ST_cm.SelectedValue)
                '.Parameters.AddWithValue("@GM_ID", GM_Serach.SelectedValue)
                'If IM_ID > 0 Then .Parameters.AddWithValue("@IM_ID", IM_ID)
            End With
            C.Da = New SqlClient.SqlDataAdapter(C.Com)
            'C.Da.Fill(IM_DT)
            'DataGridViewX.DataSource = IM_DT
            ' C.Da = New SqlClient.SqlDataAdapter(Main_Query, C.Con)
            C.Da.SelectCommand.CommandTimeout = 120
            C.Da.Fill(IM_DT)
            DataB.DataSource = IM_DT
            DataGridViewX.DataSource = DataB

            '----------------------------------------------------------------------------------------------

            If IM_DT.Rows.Count > 0 Then

                DataGridViewX.Columns(4).Tag = 1

                DataGridViewX.Columns(7).Tag = 1
                DataGridViewX.Columns(8).Tag = 1
                DataGridViewX.Columns(9).Tag = 1
                DataGridViewX.Columns(10).Tag = 1


                Index_GV()

                '   If CheckedListBox1.Items.Count = 0 Then
                CheckedListBox1.Items.Clear()
                For i As Integer = 0 To DataGridViewX.ColumnCount - 1
                    Dim CL = DataGridViewX.Columns(i).HeaderText
                    CheckedListBox1.Items.Add(CL)

                    CheckedListBox1.SetItemChecked(i, 1)
                    If DataGridViewX.Columns(i).Visible = False Then CheckedListBox1.SetItemChecked(i, 0)



                Next
                'End If


                '   Recover_STORES_Explorer_File_Setting(CheckedListBox1)

                'For i = 0 To DataGridViewX.ColumnCount - 1
                '    DataGridViewX.Columns(i).Visible = CheckedListBox1.GetItemChecked(i)
                'Next

            End If


        End If
    End Sub

    Private Sub gridv_FilterStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.FilterEventArgs) Handles DataGridViewX.FilterStringChanged
        DataB.Filter = DataGridViewX.FilterString
        Index_GV()
    End Sub

    Private Sub gridv_SortStringChanged(sender As Object, e As Zuby.ADGV.AdvancedDataGridView.SortEventArgs) Handles DataGridViewX.SortStringChanged
        DataB.Sort = DataGridViewX.SortString
        Index_GV()
    End Sub

    Private Sub Index_GV()
        For i = 0 To DataGridViewX.Rows.Count - 1
            DataGridViewX.Rows(i).Cells(0).Value = i + 1
        Next

    End Sub

    Private Sub STORES_Explorer_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub CMSearchTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CMSearchTextBox.KeyPress
        If e.KeyChar = "'" Then e.Handled = True
    End Sub

    'Private Sub CMSearchTextBox_TextChanged(sender As Object, e As EventArgs)
    '    'If BarcodeSearch_CB.Checked = False And IMNUM_CB.Checked = False Then
    '    '    Dim Dv As DataView
    '    '    Dv = IM_DT.AsDataView
    '    '    Dv.RowFilter = "item_name LIKE '%" + sender.Text + "%'"
    '    '    DataGridViewX.DataSource = Dv
    '    '    '   Get_Total_QYT()
    '    'End If
    'End Sub


    'Private Sub Store_R_Insert()
    '    Dim c As New C
    '    Dim sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
    '    Using (sqlCon)
    '        Dim sqlComm As New SqlClient.SqlCommand()
    '        c.Com = New SqlClient.SqlCommand
    '        sqlComm.Connection = sqlCon
    '        sqlComm.CommandText = "STORES_R_DELETE"
    '        sqlComm.CommandType = CommandType.StoredProcedure
    '        sqlCon.Open()
    '        Try
    '            sqlComm.ExecuteNonQuery()
    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try
    '    End Using
    '    sqlCon.Close()
    '    '***********************************************************************

    '    For i = 0 To DataGridViewX.Rows.Count - 1
    '        c = New C
    '        sqlCon = New SqlClient.SqlConnection(My_Settings.SqlConStr)
    '        Using (sqlCon)
    '            Dim sqlComm As New SqlClient.SqlCommand()
    '            c.Com = New SqlClient.SqlCommand
    '            sqlComm.Connection = sqlCon
    '            sqlComm.CommandText = "STORES_R_INSERT"
    '            sqlComm.CommandType = CommandType.StoredProcedure

    '            sqlComm.Parameters.AddWithValue("@CM_NAME", DataGridViewX.Rows(i).Cells("item_name_CL").Value)
    '            sqlComm.Parameters.AddWithValue("@QYT", DataGridViewX.Rows(i).Cells("QTY_CL").Value)
    '            sqlComm.Parameters.AddWithValue("@Unit", DataGridViewX.Rows(i).Cells("Unit_CL").Value)
    '            sqlComm.Parameters.AddWithValue("@Cost", DataGridViewX.Rows(i).Cells("Cost_CL").Value)
    '            sqlCon.Open()
    '            Try
    '                sqlComm.ExecuteNonQuery()
    '                ' MsgBox("تـــم الحذف ", MsgBoxStyle.Information)
    '            Catch ex As Exception
    '                MsgBox(ex.Message)
    '            End Try
    '        End Using
    '        sqlCon.Close()
    '    Next


    'End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Fill_ALL_IM(0)
    End Sub

    Private Sub BarcodeSearch_CB_CheckedChanged(sender As Object, e As EventArgs)
        CB_CHecked(sender)
        '  CMSearchTextBox.Select()
        ' If BarcodeSearch_CB.Checked = True Then IMNUM_CB.Checked = False
    End Sub

    Private Sub CMSearchTextBox_KeyDown(sender As Object, e As KeyEventArgs)
        ' If e.KeyCode = Keys.Return And (BarcodeSearch_CB.Checked = True Or IMNUM_CB.Checked = True) Then Search_By_Barcode()
        ' If e.KeyCode = Keys.Delete Then CMSearchTextBox.Clear()
    End Sub

    Public Sub Search_By_Barcode()

        Dim IM_ID As Integer
        Dim c As New C
        IM_DT.Clear()
        Try
            Dim s As String = ""

            ' If IMNUM_CB.Checked Then s = "select IM_ID from IM_Menu WHERE IM_NUM = '" & CMSearchTextBox.Text & "'"
            'If BarcodeSearch_CB.Checked Then s = "select IM_ID from IM_units_Search_V WHERE Barcode = '" & CMSearchTextBox.Text & "'"

            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()
                IM_ID = c.Dr("IM_ID")
                Fill_ALL_IM(IM_ID)
            Else
                MsgBox("لم يتم التعرف على الإدخال ", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    'Private Sub IMNUM_CB_CheckedChanged(sender As Object, e As EventArgs)
    '    CB_CHecked(sender)
    '    CMSearchTextBox.Select()
    '    If IMNUM_CB.Checked = True Then BarcodeSearch_CB.Checked = False
    'End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        Me.Close()
    End Sub


    Private Sub DGV_Control_btn_Click(sender As Object, e As EventArgs) Handles DGV_Control_btn.Click
        Dim F As New DGV_Control
        F.TabControl1.TabPages.Remove(F.OtherTabPage)
        F.TabControl1.TabPages.Remove(F.StTabPage)
        F.Show()
    End Sub

    Private Sub DataGridViewX_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridViewX.MouseDoubleClick
        If DataGridViewX.Rows.Count > 0 Then
            Show_IM_Details.IM_ID = DataGridViewX.CurrentRow.Cells("item_id_CL").Value
            Show_IM_Details.ShowDialog()
        End If
    End Sub

    Private Sub ST_cm_SelectedValueChanged1(sender As Object, e As EventArgs) Handles ST_cm.SelectedValueChanged
        Fill_ALL_IM(0)
    End Sub

    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click
        Dim F As New Print_PDF
        F.PRINT_PDF_List(DataGridViewX, CheckedListBox1, " كشف لوائــح الأسعــــار ", 1)
    End Sub


    Private Sub CMSearchTextBox_TextChanged(sender As Object, e As EventArgs) Handles CMSearchTextBox.TextChanged
        If BarcodeSearch_CB.Checked = False And IMNUM_CB.Checked = False Then
            Dim Dv As DataView
            Dv = IM_DT.AsDataView
            Dv.RowFilter = Serach(sender.Text, "item_name")
            '" item_name LIKE '%" + sender.Text + "%'"

            'DataB.DataSource = IM_DT
            'DataGridViewX.DataSource = DataB
            DataB.DataSource = Dv
        End If

    End Sub

    Private Sub BarcodeSearch_CB_CheckedChanged_1(sender As Object, e As EventArgs) Handles BarcodeSearch_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub IMNUM_CB_CheckedChanged(sender As Object, e As EventArgs) Handles IMNUM_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub Items_Prices_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub
End Class