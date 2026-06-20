Imports System.Data.SqlClient

Public Class BALANCE_SEARCH
    Dim DT As New DataTable

    Private Sub BALANCE_SEARCH_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SendMessage(Search_By_Acc_Name_txt.Handle, &H1501, 0, "إبحث عن إسم حســـاب")
        SendMessage(Search_By_Acc_Code_txt.Handle, &H1501, 0, "إبحث عن رقـــم حســاب")
        ACC_LEVEL_txt.SelectedItem = MY_Settings.ACC_LEVEL_SEARCH
        Search_By_Acc_Name_txt.Select()
        ACC_CODE_Search = ""
        ACC_NAME_Search = ""
        By_level_CB.Checked = MY_Settings.is_Search_By_Levels

    End Sub

    Public Sub SELECT_Balance()
        DT.Clear()
        Dim C As New C

        If By_level_CB.Checked = True Then
            C.Da = New SqlClient.SqlDataAdapter("SELECT [T_ID],[ACC_CODE] as 'رقم الحساب' ,[ACC_NAME] as 'إسم الحساب' " &
                                                " ,[DEBIT] as 'دائن' ,[CREDIT] as 'مدين' ,[BALANCE] as 'الرصيد'  FROM [dbo].[ACCOUNTS_TREE_V] WHERE ACC_LEVEL = " & ACC_LEVEL_txt.SelectedItem & "  ORDER BY [ACC_LEVEL] ASC ", C.Con)
        Else
            C.Da = New SqlClient.SqlDataAdapter("SELECT [T_ID],[ACC_CODE] as 'رقم الحساب' ,[ACC_NAME] as 'إسم الحساب' " &
                                    " ,[DEBIT] as 'دائن' ,[CREDIT] as 'مدين' ,[BALANCE] as 'الرصيد'  FROM [dbo].[ACCOUNTS_TREE_V] ORDER BY [ACC_NAME] ASC ", C.Con)
        End If


        C.Da.Fill(DT)
        DataGridView1.DataSource = DT

        DataGridView1.Columns(0).Visible = False

        DataGridView1.Columns(DataGridView1.Columns.Count - 1).DefaultCellStyle.Format = "N"
        DataGridView1.Columns(DataGridView1.Columns.Count - 2).DefaultCellStyle.Format = "N"
        DataGridView1.Columns(DataGridView1.Columns.Count - 3).DefaultCellStyle.Format = "N"

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Search_By_Acc_Name_txt.TextChanged
        Dim Dv As DataView
        Dv = DT.AsDataView
        Dv.RowFilter = IM_Serach(Search_By_Acc_Name_txt.Text, "[إسم الحساب]")
        DataGridView1.DataSource = Dv
    End Sub


    Private Sub ACC_LEVEL_txt_SelectedItemChanged(sender As Object, e As EventArgs) Handles ACC_LEVEL_txt.SelectedItemChanged
        If ACC_LEVEL_txt.SelectedIndex > -1 Then SELECT_Balance()
    End Sub

    Private Sub DataGridView1_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick

        SET_BALANCE()
    End Sub

    Private Sub SET_BALANCE()

        ACC_CODE_Search = DataGridView1.CurrentRow.Cells(1).Value
        ACC_NAME_Search = DataGridView1.CurrentRow.Cells(2).Value

        MY_Settings.ACC_LEVEL_SEARCH = ACC_LEVEL_txt.SelectedItem
        MY_Settings.is_Search_By_Levels = By_level_CB.Checked
        MY_Settings.Save_AppSetting()
        Me.Close()
    End Sub


    Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
        Try
            If DataGridView1.Columns(e.ColumnIndex).Name = "دائن" Then
                If Not IsDBNull(e.Value) Then
                    e.CellStyle.BackColor = Color.FromArgb(255, 192, 192)
                    e.CellStyle.ForeColor = Drawing.Color.DarkRed
                    'ElseIf e.Value = "تم اعداد التقرير النهائي" Then
                    '    e.CellStyle.BackColor = Drawing.Color.DarkGreen
                    '    e.CellStyle.ForeColor = Drawing.Color.White

                End If
            End If

            If DataGridView1.Columns(e.ColumnIndex).Name = "مدين" Then
                If Not IsDBNull(e.Value) Then
                    e.CellStyle.BackColor = Color.FromArgb(192, 255, 192)
                    e.CellStyle.ForeColor = Drawing.Color.DarkGreen
                    'ElseIf e.Value = "تم اعداد التقرير النهائي" Then
                    '    e.CellStyle.BackColor = Drawing.Color.DarkGreen
                    '    e.CellStyle.ForeColor = Drawing.Color.White

                End If
            End If

            If DataGridView1.Columns(e.ColumnIndex).Name = "الرصيد" Then
                If Not IsDBNull(e.Value) Then
                    'e.CellStyle.BackColor = Color.FromArgb(192, 255, 192)

                    Select Case e.Value
                        Case 0
                            e.CellStyle.ForeColor = Drawing.Color.Lavender
                            e.CellStyle.ForeColor = Drawing.Color.Black

                        Case Is < 0
                            e.CellStyle.BackColor = Color.FromArgb(255, 192, 192)
                            e.CellStyle.ForeColor = Drawing.Color.DarkRed
                        Case Is > 0
                            e.CellStyle.BackColor = Color.FromArgb(192, 255, 192)
                            e.CellStyle.ForeColor = Drawing.Color.DarkGreen

                    End Select

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Search_By_Acc_Name_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Search_By_Acc_Name_txt.KeyDown
        If e.KeyCode = Keys.Return Then If DataGridView1.Rows.Count > 0 Then SET_BALANCE()

        If e.KeyCode = Keys.Down Then If DataGridView1.Visible = True Then DataGridView1.Select()

    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Return Then SET_BALANCE()
        If e.KeyCode = Keys.Up Then If DataGridView1.CurrentRow.Index = 0 Then Search_By_Acc_Name_txt.Select()

    End Sub

    Private Sub Search_By_Acc_Code_txt_TextChanged(sender As Object, e As EventArgs) Handles Search_By_Acc_Code_txt.TextChanged
        Dim Dv As DataView
        Dv = DT.AsDataView
        Dv.RowFilter = IM_Serach(Search_By_Acc_Code_txt.Text, "[رقم الحساب]")
        DataGridView1.DataSource = Dv
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles By_level_CB.CheckedChanged
        CB_CHecked(sender)
        LEVEL_Panel.Visible = By_level_CB.Checked
        SELECT_Balance()
    End Sub
End Class