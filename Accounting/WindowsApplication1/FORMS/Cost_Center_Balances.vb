Public Class Cost_Center_Balances

    Dim DT As New DataTable
        Public COST_ID As Integer = 0
        Public COST_NAME As String = ""
        Private Sub BALANCES_REVIEW_Load(sender As Object, e As EventArgs) Handles MyBase.Load

            DateRange_Flate1.FYear_Txt.Text = Identifiers.F_YEAR
        ' ACC_LEVEL_txt.SelectedItem = "1"
        DateRange_Flate1.ALLTime_CheckBox.Checked = True
        'Refresh_form()
        Make_Hints()

        End Sub

        Private Sub Refresh_form()

        If Cost_Center_Control1.COST_CM.SelectedIndex > -1 Then
            COST_ID = Cost_Center_Control1.COST_CM.SelectedValue
            COST_NAME = Cost_Center_Control1.COST_CM.Text
            Title_Label.Text = " كشف تفصيل:" & Cost_Center_Control1.COST_CM.Text
        Else
            COST_ID = 0
            COST_NAME = "(كل مراكز التكلفة)"
            Title_Label.Text = "كشف أرصدة كل الماكز"
        End If

        SELECT_Balance()

        End Sub

        Private Sub Make_Hints()
            SendMessage(Search_By_Acc_Name_txt.Handle, &H1501, 0, "إبحث عن إسم حســـاب")
            SendMessage(Search_By_Acc_Code_txt.Handle, &H1501, 0, "إبحث عن رقـــم حســاب")
        End Sub

    'Private Sub ACC_LEVEL_txt_SelectedItemChanged(sender As Object, e As EventArgs)
    '    If ACC_LEVEL_txt.SelectedIndex > -1 Then
    '        SELECT_Balance()
    '    End If
    'End Sub
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
    Public Async Sub SELECT_Balance()
        DT = New DataTable


        '  If ACC_LEVEL_txt.SelectedItem IsNot Nothing Then


        Dim C As New C

            With C.Com
                .Connection = C.Con
                .CommandText = "[GetCostCenterBalances]"
                .CommandType = CommandType.StoredProcedure
            '.Parameters.AddWithValue("@Hide_Zeros", Hide_Zeros_CB.Checked)
            '  .Parameters.AddWithValue("@ACC_LEVEL", ACC_LEVEL_txt.SelectedItem)
            .Parameters.AddWithValue("@DATE_F", DateRange_Flate1.D_F.Value)
                .Parameters.AddWithValue("@DATE_T", DateRange_Flate1.D_T.Value)
            'If COST_ID <> 0 Then .Parameters.AddWithValue("@COST_ID", COST_ID)
            .Parameters.AddWithValue("@COST_ID", COST_ID)

        End With

            CircularPanel.Visible = True
            CircularProgressControl1.Start()
            DT = Await Task(Of DataTable).Run(Function() LoadDataTable(C.Com, MY_Settings.SqlConStr))
            DataGridView1.DataSource = DT

        '    If DataGridView1.Rows.Count > 0 Then
        '    'DataGridView1.Columns(0).Visible = False
        '    'DataGridView1.Columns(DataGridView1.Columns.Count - 1).Visible = False
        '    'DataGridView1.Columns(DataGridView1.Columns.Count - 2).Visible = False
        '    'DataGridView1.Columns(DataGridView1.Columns.Count - 3).DefaultCellStyle.Format = "N3"
        '    'DataGridView1.Columns(DataGridView1.Columns.Count - 4).DefaultCellStyle.Format = "N3"
        '    'DataGridView1.Columns(DataGridView1.Columns.Count - 5).DefaultCellStyle.Format = "N3"
        '    'DataGridView1.Columns(DataGridView1.Columns.Count - 6).DefaultCellStyle.Format = "N3"
        '    'DataGridView1.Columns(DataGridView1.Columns.Count - 2).Tag = 1
        '    'DataGridView1.Columns(DataGridView1.Columns.Count - 3).Tag = 1
        '    'DataGridView1.Columns(DataGridView1.Columns.Count - 4).Tag = 1
        '    'DataGridView1.Columns(DataGridView1.Columns.Count - 5).Tag = 1

        'End If
        CircularPanel.Visible = False
            CircularProgressControl1.Stop()

        ' End If


    End Sub

    Private Sub DataGridView1_DataSourceChanged(sender As Object, e As EventArgs) Handles DataGridView1.DataSourceChanged


        'Compute_Balance(DT)
        Total_C_txt.Text = T_CREDIT
        Total_D_txt.Text = T_DEBIT

        Total_B_D_txt.Text = T_BALANCE_D.ToString()
        Total_B_C_txt.Text = T_BALANCE_C.ToString()

        Rows_txt.Text = DT.Rows.Count
        Dif_TXT.Text = T_BALANCE_D - T_BALANCE_C

    End Sub


    Dim T_BALANCE_D = 0
    Dim T_BALANCE_C = 0

    'Public Sub Compute_Balance(DT As DataTable)
    '    Dim rows As Integer = 0
    '    T_DEBIT = 0
    '    T_CREDIT = 0

    '    T_BALANCE_D = 0
    '    T_BALANCE_C = 0
    '    Try

    '        Do Until rows = DT.Rows.Count

    '            If (Not IsDBNull(DT(rows)("مدين - المجاميع"))) Then
    '                Dim Tax_Withheld As Double = DT(rows)("مدين - المجاميع")
    '                T_CREDIT += Tax_Withheld
    '            End If

    '            If (Not IsDBNull(DT(rows)("دائــن - المجاميع"))) Then
    '                Dim Tax_Withheld As Double = DT(rows)("دائــن - المجاميع")
    '                T_DEBIT += Tax_Withheld
    '            End If


    '            If (Not IsDBNull(DT(rows)("مديـن - الأرصــدة"))) Then
    '                Dim Tax_Withheld As Double = DT(rows)("مديـن - الأرصــدة")
    '                T_BALANCE_C += Tax_Withheld
    '            End If

    '            If (Not IsDBNull(DT(rows)("دائـن - الأرصــدة"))) Then
    '                Dim Tax_Withheld As Double = DT(rows)("دائـن - الأرصــدة")
    '                T_BALANCE_D += Tax_Withheld
    '            End If


    '            rows = rows + 1
    '        Loop
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try


    'End Sub
    'Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
    '    Try
    '        If DataGridView1.Columns(e.ColumnIndex).Name = "دائـن - الأرصــدة" Or DataGridView1.Columns(e.ColumnIndex).Name = "دائــن - المجاميع" Then
    '            If Not IsDBNull(e.Value) Then

    '                Select Case e.Value
    '                    Case 0
    '                        e.CellStyle.ForeColor = Drawing.Color.Lavender
    '                        e.CellStyle.ForeColor = Drawing.Color.Black
    '                    Case Else
    '                        e.CellStyle.BackColor = Color.FromArgb(255, 192, 192)
    '                        e.CellStyle.ForeColor = Drawing.Color.DarkRed
    '                End Select

    '            End If
    '        End If

    '        If DataGridView1.Columns(e.ColumnIndex).Name = "مديـن - الأرصــدة" Or DataGridView1.Columns(e.ColumnIndex).Name = "مدين - المجاميع" Then
    '            If Not IsDBNull(e.Value) Then

    '                Select Case e.Value

    '                    Case 0
    '                        e.CellStyle.ForeColor = Drawing.Color.Lavender
    '                        e.CellStyle.ForeColor = Drawing.Color.Black

    '                    Case Else
    '                        e.CellStyle.BackColor = Color.FromArgb(192, 255, 192)
    '                        e.CellStyle.ForeColor = Drawing.Color.DarkGreen

    '                End Select
    '            End If
    '        End If


    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub RefreshBtn_Click(sender As Object, e As EventArgs) Handles RefreshBtn.Click
        Refresh_form()
    End Sub



    Public Sub Print_B(Optional ByVal exportToExcel As Boolean = False)
        Try
            Dim pp As New ReportConnection
            pp.rp.Load(Application.StartupPath & "\Reports\BALANCES_REVIEW.rpt")
            pp.LoadTables()

            With pp
                .rp.SetParameterValue("TITLE_NUM", TITLE_txt.Text & " - " & COST_NAME & vbNewLine & "للفترة من : " & DateRange_Flate1.D_F.Value & " إلى: " & DateRange_Flate1.D_T.Value) '& vbNewLine & "مستوى الحسابات :" & ACC_LEVEL_txt.Text
                .rp.SetParameterValue("Title_1", MY_Settings.SBill_Title_1)
                .rp.SetParameterValue("Title_2", MY_Settings.SBill_Title_2)
                .rp.SetParameterValue("Total_C_txt", Total_C_txt.Text)
                .rp.SetParameterValue("Total_D_txt", Total_D_txt.Text)
                .rp.SetParameterValue("Total_B_D_txt", Total_B_D_txt.Text)
                .rp.SetParameterValue("Total_B_C_txt", Total_B_C_txt.Text)
                .rp.SetParameterValue("T_ROWS", Rows_txt.Text)
                .rp.SetParameterValue("USER_Input", USER_NAME)

                If String.IsNullOrWhiteSpace(Dif_TXT.Text) Then Dif_TXT.Text = "0"
                .rp.SetParameterValue("Dif_TXT", Dif_TXT.Text)

            End With

            ' **تصدير التقرير إلى Excel بدلاً من الطباعة إذا تم اختيار التصدير**
            If exportToExcel Then
                Dim saveDialog As New SaveFileDialog()
                saveDialog.Filter = "Excel Files|*.xls"
                saveDialog.Title = "حفظ التقرير كملف Excel"
                saveDialog.FileName = TITLE_txt.Text & ".xls"

                If saveDialog.ShowDialog() = DialogResult.OK Then
                    Dim exportPath As String = saveDialog.FileName
                    ExportReportToExcel(pp.rp, exportPath)
                End If
            Else
                ' **عرض التقرير للطباعة**
                Dim p As New print
                p.CrystalReportViewer1.ReportSource = pp.rp
                p.ShowDialog()
            End If
        Catch ex As Exception
            MessageBox.Show("حدث خطأ: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Hide_Zeros_CB_CheckedChanged(sender As Object, e As EventArgs)
        CB_CHecked(sender)

    End Sub


    Private Sub Print_Btn_ButtonClick(sender As Object, e As EventArgs) Handles Print_Btn.ButtonClick
            Print_B()
        End Sub

        Private Sub إستخراجالتقريرExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إستخراجالتقريرExcelToolStripMenuItem.Click
            Print_B(True)
        End Sub

    End Class