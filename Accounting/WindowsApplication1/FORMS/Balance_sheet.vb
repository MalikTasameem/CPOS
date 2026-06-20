Public Class Balance_sheet
    Dim Rs As New Resizer
    Private Sub Search_btn_Click(sender As Object, e As EventArgs) Handles Search_btn.Click
        SELECT_Balance_sheet()
    End Sub


    Private Async Sub SELECT_Balance_sheet()

        Dim C As New C

        With C.Com
            .Connection = C.Con
            .CommandText = "[Balance_sheet_1]"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@Hide_Zeros", Hide_Zeros_CB.Checked)
            .Parameters.AddWithValue("@DATE_F", DateRange_Flate1.D_F.Value)
            .Parameters.AddWithValue("@DATE_T", DateRange_Flate1.D_T.Value)
        End With

        CircularPanel.Visible = True
        CircularProgressControl1.Start()
        DataGridView.DataSource = Await Task(Of DataTable).Run(Function() LoadDataTable(C.Com, MY_Settings.SqlConStr))
        Coloring()
        CircularPanel.Visible = False
        CircularProgressControl1.Stop()

    End Sub




    Private Sub Coloring()




        For i = 0 To DataGridView.Rows.Count - 1

            If Not IsDBNull(DataGridView.Rows(i).Cells("ACC_CODE_CL").Value) And Not IsDBNull(DataGridView.Rows(i).Cells("ACC_PARENT_CL").Value) Then

                Dim ACC_CODE_VALUE As Integer = Convert.ToInt32(DataGridView.Rows(i).Cells("ACC_CODE_CL").Value)
                Dim ACC_PARENT_VALUE As Integer = Convert.ToInt32(DataGridView.Rows(i).Cells("ACC_PARENT_CL").Value)

                Dim Side As String = DataGridView.Rows(i).Cells("SIDE_CL").Value
                Dim LEVEL As String = Convert.ToInt32(DataGridView.Rows(i).Cells("ACC_LEVEL_CL").Value)


                If LEVEL <> 1 Then
                    If Side = "assets" Then

                        If LEVEL = 2 Then
                            Me.DataGridView.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(100, 230, 100)
                            Me.DataGridView.Rows(i).DefaultCellStyle.Font = New Font("Arial", 14, FontStyle.Bold)
                        Else
                            Me.DataGridView.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192)
                            Me.DataGridView.Rows(i).DefaultCellStyle.Font = New Font("Arial", 12, FontStyle.Regular)
                        End If

                    ElseIf Side = "opponents" Then

                        If LEVEL = 2 Then
                            Me.DataGridView.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(255, 128, 128)
                            Me.DataGridView.Rows(i).DefaultCellStyle.Font = New Font("Arial", 14, FontStyle.Bold)
                        Else
                            Me.DataGridView.Rows(i).DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 192)
                            Me.DataGridView.Rows(i).DefaultCellStyle.Font = New Font("Arial", 12, FontStyle.Regular)
                        End If

                    End If

                End If

            End If
        Next

    End Sub

    Private Sub Balance_sheet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Rs.FindAllControls(Me)
        DataGridView.DefaultCellStyle.SelectionBackColor = DataGridView.DefaultCellStyle.BackColor
        DataGridView.DefaultCellStyle.SelectionForeColor = DataGridView.DefaultCellStyle.ForeColor
        DataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None
        DataGridView.BorderStyle = BorderStyle.None

        DateRange_Flate1.FYear_Txt.Text = Identifiers.F_YEAR
        DateRange_Flate1.ALLTime_CheckBox.Enabled = False
        DateRange_Flate1.MonthCmbo.SelectedIndex = 0
        DateRange_Flate1.MonthCmbo.Enabled = False
        DateRange_Flate1.SetDateRange(Date.Today, False, True)

    End Sub



    'Private Sub Print_Btn_Click(sender As Object, e As EventArgs)
    '    Print_B()
    'End Sub

    Public Sub Print_B(Optional ByVal exportToExcel As Boolean = False)

        Dim pp As New ReportConnection
        pp.rp.Load(Application.StartupPath & "\Reports\Balance_sheet.rpt")
        pp.LoadTables()
        With pp
            .rp.SetParameterValue("TITLE_NUM", " قائمـــــة المركــز المالـــي " & vbNewLine & " فـــي " & DateRange_Flate1.D_T.Value)
            .rp.SetParameterValue("Title_1", MY_Settings.SBill_Title_1)
            .rp.SetParameterValue("Title_2", MY_Settings.SBill_Title_2)
            .rp.SetParameterValue("USER_Printer", USER_NAME)
        End With


        ' **تصدير التقرير إلى Excel بدلاً من الطباعة إذا تم اختيار التصدير**
        If exportToExcel Then
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "Excel Files|*.xls"
            saveDialog.Title = "حفظ التقرير كملف Excel"
            saveDialog.FileName = "قائمة المركز المالي.xls"

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


    End Sub

    Private Sub Balance_sheet_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Rs.ResizeAllControls(Me)
    End Sub

    Private Sub Hide_Zeros_CB_CheckedChanged(sender As Object, e As EventArgs) Handles Hide_Zeros_CB.CheckedChanged
        CB_CHecked(sender)
    End Sub


    'Private Sub Excel_Export_Btn_Click(sender As Object, e As EventArgs)
    '    Print_B(True)
    'End Sub

    Private Sub CLOSE_B_Btn_Click(sender As Object, e As EventArgs) Handles CLOSE_B_Btn.Click

        Dim inp = InputBox("أدخل السنة الماليــة التي تريد ترحيل الميزانية لها", "فتح سنة")
        If inp <> "" Then

            Dim numericValue As Integer
            If Not Integer.TryParse(inp, numericValue) Then
                MsgBox("خطأ فالإدخال", MsgBoxStyle.Exclamation, "Invalid Input")
                Exit Sub
            End If

            If check_FOUND_YEAR(inp) = True Then
                Dim START_DATE As String = GET_FIRST_DAY_OF_YEAR(inp).ToString
                If START_DATE <> "0" Then
                    If MessageBox.Show("سيتم إدراج قيد إفتتاحي للسنة الماليــة " & vbNewLine & " معلومات القيد : " & vbNewLine & " 1.ترحيل قائمة المركز المالي من تاريخ " & DateRange_Flate1.D_F.Value & " إلى تاريخ " & DateRange_Flate1.D_T.Value & vbNewLine &
                                       "2. إدراج قيد إفتتاحي للسنة ( " & inp & " ) بتاريخ " & START_DATE, " تاكيــد العملية 1", MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) = DialogResult.OK Then

                        If MessageBox.Show(" تأكيد العملية للمرة الثانية  ", " تاكيــد العملية 2", MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.OK Then

                            MOVE_BALANCE_SHEET_TO_OPEN_NEW_YEAR(DateRange_Flate1.FYear_Txt.Text, inp)

                        End If
                    End If

                Else
                    Dim notification2 As New NotificationForm("خطأ", " لم يتم تحديد جدول للسنة المالية " & inp, "bottom", True)
                    notification2.ShowNotification()
                End If

            Else

                Dim notification3 As New NotificationForm("خطأ", " لم يتم التعرف على السنة او أنها غير معرفة فالنظام ", "bottom", True)
                notification3.ShowNotification()
            End If

        End If
    End Sub


    Public Sub MOVE_BALANCE_SHEET_TO_OPEN_NEW_YEAR(YEAR_FROM As Integer, YEAR_TO As Integer)

        Dim C As New C


        With C.Com
            .Connection = C.Con
            .CommandText = "[MOVE_BALANCE_SHEET_TO_OPEN_NEW_YEAR]"
            .CommandType = CommandType.StoredProcedure

            .Parameters.AddWithValue("@YEAR_FROM", YEAR_FROM)
            .Parameters.AddWithValue("@YEAR_TO", YEAR_TO)
            .Parameters.AddWithValue("@DATE_F", DateRange_Flate1.D_F.Value)
            .Parameters.AddWithValue("@DATE_T", DateRange_Flate1.D_T.Value)
            .Parameters.AddWithValue("@USER_ID", USER_ID)
        End With

        If SQL_SP_EXEC(C.Com) Then
            Dim notification3 As New NotificationForm("تنويه", " تم إضافة قيد إفتتاحي للسنة  " & YEAR_TO.ToString, "bottom")
            notification3.ShowNotification()
        End If
    End Sub

    Private Sub SplitButton1_ButtonClick(sender As Object, e As EventArgs) Handles Print_Btn.ButtonClick
        Print_B()
    End Sub

    Private Sub إستخراجالتقريرExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles إستخراجالتقريرExcelToolStripMenuItem.Click
        Print_B(True)
    End Sub

End Class