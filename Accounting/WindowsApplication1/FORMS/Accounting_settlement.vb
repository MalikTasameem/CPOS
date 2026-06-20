Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Accounting_settlement


    Public ACC_NATURAL As Char
    Public ACC_Code As String
    Public ACC_Name As String
    Public COST_ID As Integer = 0
    Public COST_NAME As String = ""
    Dim Dt_MV As New DataTable

    Private Sub ACC_MV_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateRange_Flate1.FYear_Txt.Text = Identifiers.F_YEAR

    End Sub


    Public Async Sub SELECT_Balance()
        Dt_MV = New DataTable

        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "[ACC_MV]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@ACC_CODE", ACC_Code)
            .Parameters.AddWithValue("@DATE_F", DateRange_Flate1.D_F.Value)
            .Parameters.AddWithValue("@DATE_T", DateRange_Flate1.D_T.Value)
            .Parameters.Add("@DEBIT", SqlDbType.Float, (18.3), "0")
            .Parameters.Add("@CREDIT", SqlDbType.Float, (18.3), "0")
            .Parameters("@DEBIT").Direction = ParameterDirection.Output
            .Parameters("@CREDIT").Direction = ParameterDirection.Output
            If COST_ID <> 0 Then .Parameters.AddWithValue("@COST_ID", COST_ID)
            .Parameters.AddWithValue("@is_settlement", 1)
        End With

        'CircularPanel.Visible = True
        CircularProgressControl1.Start()

        Dt_MV = (Await Task(Of DataTable).Run(Function() LoadDataTable(C.Com, MY_Settings.SqlConStr)))

        DataGridView1.DataSource = (Await Task(Of DataTable).Run(Function() LoadDataTable(C.Com, MY_Settings.SqlConStr)))

        '-----------------------------------------

        Total_C_txt.Text = C.Com.Parameters("@CREDIT").Value
        Total_D_txt.Text = C.Com.Parameters("@DEBIT").Value

        Rows_txt.Text = DataGridView1.Rows.Count
        DataGridView1.Columns(DataGridView1.Columns.Count - 1).Visible = False
        DataGridView1.Columns(DataGridView1.Columns.Count - 2).Visible = False
        DataGridView1.Columns("مدين").DefaultCellStyle.Format = "N3"
        DataGridView1.Columns("دائن").DefaultCellStyle.Format = "N3"
        DataGridView1.Columns("الرصيد").DefaultCellStyle.Format = "N3"
        DataGridView1.Columns("CREDIT").DefaultCellStyle.Format = "N3"
        DataGridView1.Columns("DEBIT").DefaultCellStyle.Format = "N3"
        DataGridView1.Columns("مدين").Tag = 1
        DataGridView1.Columns("دائن").Tag = 1
        DataGridView1.Columns("القيمة").DefaultCellStyle.Format = "N3"


        Module1.TOTAL_C_N = 0
        Module1.TOTAL_D_N = 0

        ' Loop through all columns in the DataGridView
        For Each column As DataGridViewColumn In DataGridView1.Columns
            column.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        For i = 1 To DataGridView1.Rows.Count - 1

            If IsDBNull(DataGridView1.Rows(i).Cells(1).Value) Then
                Continue For
            End If
            If IsDBNull(DataGridView1.Rows(i).Cells("مدين").Value) Then
                Module1.TOTAL_D_N += 1
            ElseIf DataGridView1.Rows(i).Cells("مدين").Value = 0 Then
                If IsDBNull(DataGridView1.Rows(i).Cells("دائن").Value) Then
                    Module1.TOTAL_C_N += 1
                ElseIf DataGridView1.Rows(i).Cells("دائن").Value = 0 Then
                    Continue For
                Else
                    Module1.TOTAL_C_N += 1
                End If

            Else
                Module1.TOTAL_C_N += 1

            End If
        Next

        TOTAL_C_N.Text = Module1.TOTAL_C_N
        TOTAL_D_N.Text = Module1.TOTAL_D_N

        If DataGridView1.Rows.Count > 0 Then
            Total_B_txt.Text = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells("الرصيد").Value
        End If

        ' Make all columns read-only except the column named "مطابق"
        For Each col As DataGridViewColumn In DataGridView1.Columns
            If col.Name = "مطابق" Then
                col.ReadOnly = False
            Else
                col.ReadOnly = True
            End If
        Next


        CheckedListBox1.Items.Clear()
        For i As Integer = 0 To DataGridView1.ColumnCount - 1
            Dim CL = DataGridView1.Columns(i).Name
            CheckedListBox1.Items.Add(CL)
        Next

        '-----------------------------------------
        'CircularPanel.Visible = False
        CircularProgressControl1.Stop()

    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseDoubleClick

        If DataGridView1.CurrentRow.Cells("قيد ألي").Value = False Then

            If DataGridView1.CurrentRow.Cells("رقم القيــد").Value <> 0 Then

                '-------------------------------------------------
                F_ACC_B = New ACC_B
                F_ACC_B.UP_ToolStripBtn.Enabled = False
                F_ACC_B.DOWN_ToolStripBtn.Enabled = False
                F_ACC_B.LAST_ToolStripBtn.Enabled = False
                F_ACC_B.First_ToolStripBtn.Enabled = False
                F_ACC_B.T_ID_txt_2.Enabled = False
                F_ACC_B.Text = " عرض القيــد ( " & DataGridView1.CurrentRow.Cells("رقم القيــد").Value & " )  "
                F_ACC_B.NEW_Btn.Enabled = False

                F_ACC_B.is_Select = True
                T_ID_Search = DataGridView1.CurrentRow.Cells("رقم القيــد").Value

                F_ACC_B.Selected_ACC_CODE = ACC_Code

                F_ACC_B.ShowDialog()
                T_ID_Search = 0
                '-------------------------------------------------

            End If

        Else

            Dim F As New Auto_Balance_info
            F.T_ID = DataGridView1.CurrentRow.Cells("رقم القيــد").Value
            F.ShowDialog()

        End If


    End Sub


    Private Sub DataGridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
        Try
            If DataGridView1.Columns(e.ColumnIndex).Name = "دائن" Then
                If Not IsDBNull(e.Value) Then
                    e.CellStyle.BackColor = Color.FromArgb(255, 192, 192)
                    e.CellStyle.ForeColor = Drawing.Color.DarkRed

                End If
            End If

            If DataGridView1.Columns(e.ColumnIndex).Name = "مدين" Then
                If Not IsDBNull(e.Value) Then
                    e.CellStyle.BackColor = Color.FromArgb(192, 255, 192)
                    e.CellStyle.ForeColor = Drawing.Color.DarkGreen


                End If
            End If

            If DataGridView1.Columns(e.ColumnIndex).Name = "الرصيد" Then
                If Not IsDBNull(e.Value) Then
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

    Private Sub Search_btn_Click(sender As Object, e As EventArgs) Handles Search_btn.Click
        ACC_Code = ACC_INFO1.ACC_CODE_TXT.Text
        SELECT_Balance()
    End Sub

    Public Sub Print_B(Optional ByVal exportToExcel As Boolean = False)
        Try
            Dim pp As New ReportConnection
            pp.rp.Load(Application.StartupPath & "\Reports\ACC_MV.rpt")
            pp.LoadTables()

            With pp
                .rp.SetParameterValue("TITLE_NUM", ACC_INFO1.ACC_CODE_Cm.Text & vbNewLine & "للفترة من : " & DateRange_Flate1.D_F.Value & " إلى: " & DateRange_Flate1.D_T.Value & vbNewLine & "مركز التكلفة :" & If(COST_ID = 0, "الكل", COST_NAME))
                .rp.SetParameterValue("Title_1", MY_Settings.SBill_Title_1)
                .rp.SetParameterValue("Title_2", MY_Settings.SBill_Title_2)
                .rp.SetParameterValue("T_CREDIT", Total_C_txt.Text)
                .rp.SetParameterValue("T_DEBIT", Total_D_txt.Text)
                .rp.SetParameterValue("TOTAL_D_N", TOTAL_D_N.Text)
                .rp.SetParameterValue("TOTAL_C_N", TOTAL_C_N.Text)
                .rp.SetParameterValue("T_ROWS", Rows_txt.Text)
                .rp.SetParameterValue("USER_Input", USER_NAME)
                .rp.SetParameterValue("Money_char", HANY(Total_B_txt.Text, "LYD"))
                .rp.SetParameterValue("T_BALANCE", Total_B_txt.Text)
                .rp.SetParameterValue("ACC_TYPE", ACC_TYPE_Txt.Text)
            End With

            ' **تصدير التقرير إلى Excel بدلاً من الطباعة إذا تم اختيار التصدير**
            If exportToExcel Then
                Dim saveDialog As New SaveFileDialog()
                saveDialog.Filter = "Excel Files|*.xls"
                saveDialog.Title = "حفظ التقرير كملف Excel"
                saveDialog.FileName = "" & ".xls"

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



    Private Sub Total_B_txt_TextChanged(sender As Object, e As EventArgs) Handles Total_B_txt.TextChanged
        If String.IsNullOrWhiteSpace(Total_B_txt.Text) Then
            ACC_TYPE_Txt.Clear()
        Else
            If Convert.ToDecimal(Total_B_txt.Text) < 0 Then
                ACC_TYPE_Txt.Text = "(دائـن)"
            Else
                ACC_TYPE_Txt.Text = "مديـن"
            End If
        End If
    End Sub


    Private Sub ADD_ACC_C_Btn_Click(sender As Object, e As EventArgs) Handles ADD_ACC_C_Btn.Click

        If ACC_INFO1.ACC_CODE_Cm.SelectedValue > 0 Then
            Dim F As New ACC_B_B2B
            F.ACC_CODE = ACC_INFO1.ACC_CODE_TXT.Text
            F.ShowDialog()
        Else
            Dim notification3 As New NotificationForm("تنويه", " حدد حساب " & ACC_Name, "bottom", True)
            notification3.ShowNotification()

            ACC_INFO1.ACC_CODE_TXT.Select()
        End If


    End Sub

    Private Sub Upload_File_Btn_Click(sender As Object, e As EventArgs) Handles Upload_File_Btn.Click
        Dim F As New Settlement_Sheet

        F.Text = F.Text & " : " & ACC_INFO1.ACC_CODE_Cm.Text & " | " & "للفترة من : " & DateRange_Flate1.D_From.Text & " إلى: " & DateRange_Flate1.D_To.Text

        For i As Integer = Dt_MV.Rows.Count - 1 To 0 Step -1
            If Dt_MV.Rows(i)("رقم القيــد") = 0 Then
                Dt_MV.Rows.RemoveAt(i)
            End If
        Next

        For i As Integer = 0 To CheckedListBox1.Items.Count - 1
            Dim itemName As String = CheckedListBox1.Items(i).ToString()
            Dim isChecked As Boolean = CheckedListBox1.GetItemChecked(i)

            If isChecked = False Then
                Dt_MV.Columns.Remove(itemName)
            End If
        Next



        F.dtSystem = Dt_MV.Copy()

        F.Show()

    End Sub

    Private Sub إستخراجالتقريرExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إستخراجالتقريرExcelToolStripMenuItem.Click
        Print_B(True)
    End Sub

    Private Sub Print_Btn_ButtonClick(sender As Object, e As EventArgs) Handles Print_Btn.ButtonClick
        If DataGridView1.Rows.Count > 0 Then Print_B()
    End Sub
End Class