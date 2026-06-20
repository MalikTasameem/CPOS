Imports System.Data.SqlClient

Public Class Balances_Form
    Dim DT As New DataTable
    Dim is_load As Boolean = False
    Private Sub Balances_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        query("EXEC [dbo].[PREPARE_ACC_BALANCE] 0,NULL,NULL,0 ")
        is_load = True
        ACC_LEVEL_txt.SelectedItem = "1"
        PREPARE_ACCOUNTS()
        Make_Hints()
        'Load_Balances()

    End Sub

    Private Sub PREPARE_ACCOUNTS()
        If ALL_RD.Checked Then
            LEVEL_Panel.Visible = Not ALL_RD.Checked
            TOTAL_Panel.Visible = False
            'SELECT_Balance_NO_LEVELES()
            SELECT_Balance()
        Else
            LEVEL_Panel.Visible = BY_LEVELS_RD.Checked
            TOTAL_Panel.Visible = True
            SELECT_Balance()
        End If
    End Sub


    Private Sub Make_Hints()
        SendMessage(Search_By_Acc_Name_txt.Handle, &H1501, 0, "إبحث عن إسم حســـاب")
        SendMessage(Search_By_Acc_Code_txt.Handle, &H1501, 0, "إبحث عن رقـــم حســاب")
    End Sub

    Public Sub SELECT_Balance()
        Dim C As New C
        DT = New DataTable
        Dim da As New SqlClient.SqlDataAdapter

        If BY_LEVELS_RD.Checked Then

            If Hide_Zeros_CB.Checked = True Then
                da = New SqlClient.SqlDataAdapter("SELECT [T_ID],[ACC_CODE] as 'رقم الحساب' ,[ACC_NAME] as 'إسم الحساب' , " &
                                            " [CREDIT] as 'مدين',[DEBIT] as 'دائن' ,[BALANCE] as 'الرصيد' ,DEBIT,CREDIT,ACC_NATURAL  FROM [dbo].[ACCOUNTS_TREE_V] WHERE ACC_LEVEL = '" & ACC_LEVEL_txt.SelectedItem & "'
                                        AND [BALANCE] <> 0 ORDER BY [ACC_LEVEL],[ACC_CODE] ASC ", C.Con)
            Else
                da = New SqlClient.SqlDataAdapter("SELECT [T_ID],[ACC_CODE] as 'رقم الحساب' ,[ACC_NAME] as 'إسم الحساب' , " &
                                            " [CREDIT] as 'مدين',[DEBIT] as 'دائن' ,[BALANCE] as 'الرصيد' ,DEBIT,CREDIT,ACC_NATURAL  FROM [dbo].[ACCOUNTS_TREE_V] WHERE ACC_LEVEL = '" & ACC_LEVEL_txt.SelectedItem & "'  ORDER BY [ACC_LEVEL],[ACC_CODE] ASC ", C.Con)
            End If
        Else
            If Hide_Zeros_CB.Checked = True Then
                da = New SqlClient.SqlDataAdapter("SELECT [T_ID],[ACC_CODE] as 'رقم الحساب' ,[ACC_NAME] as 'إسم الحساب' , " &
                                            " [CREDIT] as 'مدين',[DEBIT] as 'دائن' ,[BALANCE] as 'الرصيد' ,DEBIT,CREDIT,ACC_NATURAL  FROM [dbo].[ACCOUNTS_TREE_V] WHERE  [BALANCE] <> 0 ORDER BY [ACC_LEVEL],[ACC_CODE] ASC ", C.Con)
            Else
                da = New SqlClient.SqlDataAdapter("SELECT [T_ID],[ACC_CODE] as 'رقم الحساب' ,[ACC_NAME] as 'إسم الحساب' , " &
                                            " [CREDIT] as 'مدين',[DEBIT] as 'دائن' ,[BALANCE] as 'الرصيد' ,DEBIT,CREDIT,ACC_NATURAL  FROM [dbo].[ACCOUNTS_TREE_V]  ORDER BY [ACC_LEVEL],[ACC_CODE] ASC ", C.Con)
            End If

        End If


        da.Fill(DT)
        DataGridView1.DataSource = DT

        DataGridView1.Columns(DataGridView1.Columns.Count - 1).Visible = False
        DataGridView1.Columns(DataGridView1.Columns.Count - 2).Visible = False
        DataGridView1.Columns(DataGridView1.Columns.Count - 3).Visible = False
        DataGridView1.Columns(0).Visible = False

        DataGridView1.Columns(3).DefaultCellStyle.Format = "N3"
        DataGridView1.Columns(4).DefaultCellStyle.Format = "N3"
        DataGridView1.Columns(5).DefaultCellStyle.Format = "N3"
    End Sub

    Private Sub DataGridView1_DataSourceChanged(sender As Object, e As EventArgs) Handles DataGridView1.DataSourceChanged

        If DT.Rows.Count > 0 Then
            Compute_Balance(DT)
            Total_C_txt.Text = T_CREDIT.ToString()
            Total_D_txt.Text = T_DEBIT.ToString()
            'Total_C_txt.Text = DT.Compute("Sum(CREDIT)", "").ToString()
            'Total_D_txt.Text = DT.Compute("Sum(DEBIT)", "").ToString()
            Rows_txt.Text = DT.Rows.Count

            Total_B_txt.Text = Convert.ToDouble(Total_D_txt.Text) - Convert.ToDouble(Total_C_txt.Text)
        End If
    End Sub


    Private Sub DataGridView1_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        SHOW_BALANCES()
    End Sub

    Private Sub SHOW_BALANCES()
        Dim F As New ACC_MV
        F.ACC_Name = Me.DataGridView1.CurrentRow.Cells(2).Value
        'F.ACC_NATURAL = Me.DataGridView1.CurrentRow.Cells("ACC_NATURAL").Value
        F.ACC_Code = Me.DataGridView1.CurrentRow.Cells(1).Value
        If Cost_Center_Control1.COST_CM.SelectedIndex > -1 Then
            F.COST_ID = Cost_Center_Control1.COST_CM.SelectedValue
            F.COST_NAME = Cost_Center_Control1.COST_CM.Text
        End If
        F.ShowDialog()
        ' ACC_MV.ShowDialog()
    End Sub


    Private Sub ACC_LEVEL_txt_SelectedItemChanged(sender As Object, e As EventArgs) Handles ACC_LEVEL_txt.SelectedItemChanged
        If ACC_LEVEL_txt.SelectedIndex > -1 Then
            SELECT_Balance()
            If ACC_LEVEL_txt.SelectedIndex = 0 Then query("EXEC [dbo].[PREPARE_ACC_BALANCE] 0 ")
        End If
    End Sub

    Private Sub Search_By_Acc_Name_txt_TextChanged(sender As Object, e As EventArgs) Handles Search_By_Acc_Name_txt.TextChanged
        Dim Dv As DataView
        Dv = DT.AsDataView
        Dv.RowFilter = IM_Serach(Search_By_Acc_Name_txt.Text, "[إسم الحساب]")
        DataGridView1.DataSource = Dv
    End Sub

    Private Sub Search_By_Acc_Code_txt_TextChanged(sender As Object, e As EventArgs) Handles Search_By_Acc_Code_txt.TextChanged
        Dim Dv As DataView
        Dv = DT.AsDataView
        Dv.RowFilter = IM_Serach(Search_By_Acc_Code_txt.Text, "[رقم الحساب]")
        DataGridView1.DataSource = Dv
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
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

    Private Sub EDIT_Btn_Click(sender As Object, e As EventArgs) Handles EDIT_Btn.Click
        SHOW_BALANCES()
    End Sub

    Private Sub Hide_Zeros_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Hide_Zeros_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub

    Private Sub ALL_RD_CheckedChanged(sender As Object, e As EventArgs) Handles ALL_RD.CheckedChanged, BY_LEVELS_RD.CheckedChanged
        If is_load = True Then PREPARE_ACCOUNTS()
    End Sub


End Class