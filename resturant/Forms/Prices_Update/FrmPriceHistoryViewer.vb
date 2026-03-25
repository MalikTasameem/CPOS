Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Runtime.InteropServices

Public Class FrmPriceHistoryViewer

    Private CON_STR As String = MY_Settings.SqlConStr

    'Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

    '    Dim sql As String = "
    '        SELECT 
    '            H.HistoryID,
    '            H.U_IM_ID,
    '            V.item_name,
    '            V.U_NAME,
    '            H.Old_Price,
    '            H.New_Price,
    '            H.Old_Min_SP,
    '            H.New_Min_SP,
    '            H.Old_Min_SP_2,
    '            H.New_Min_SP_2,
    '            H.Old_OFFER_PRICE,
    '            H.New_OFFER_PRICE,
    '            H.ChangeMode,
    '            H.ChangeType,
    '            H.ChangeValue,
    '            H.Reason,
    '            H.ChangeDate,
    '            H.BatchId,
    '            H.User_ID
    '        FROM IM_Units_Prices_History H
    '        INNER JOIN IM_Menu_Units_V V ON H.U_IM_ID = V.U_IM_ID
    '        WHERE H.ChangeDate BETWEEN @D1 AND @D2
    '          AND V.item_name LIKE @ItemName
    '        ORDER BY H.ChangeDate DESC
    '    "

    '    Using con As New SqlConnection(CON_STR)
    '        Using cmd As New SqlCommand(sql, con)
    '            cmd.Parameters.AddWithValue("@D1", dtFrom.Value.Date)
    '            cmd.Parameters.AddWithValue("@D2", dtTo.Value.Date.AddDays(1))
    '            cmd.Parameters.AddWithValue("@ItemName", "%" & txtItemName.Text.Trim() & "%")

    '            Using da As New SqlDataAdapter(cmd)
    '                Dim dt As New DataTable
    '                da.Fill(dt)
    '                dgvHistory.DataSource = dt
    '            End Using
    '        End Using
    '    End Using
    'End Sub


    Private Sub btnRollbackBatch_Click(sender As Object, e As EventArgs) Handles btnRollbackBatch.Click

        If Batches_Cm.Text.Trim() = "" Then
            MessageBox.Show("الرجاء إدخال BatchId.", "تنبيه")
            Exit Sub
        End If

        Dim batchId As Guid
        If Not Guid.TryParse(Batches_Cm.Text.Trim(), batchId) Then
            MessageBox.Show("BatchId غير صحيح.", "خطأ")
            Exit Sub
        End If

        If MessageBox.Show("هل أنت متأكد من التراجع عن هذه الدفعة بالكامل؟",
                       "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
            Exit Sub
        End If

        Using con As New SqlConnection(CON_STR)
            Using cmd As New SqlCommand("Rollback_IM_Units_ByBatch", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@BatchId", batchId)
                cmd.Parameters.AddWithValue("@User_ID", USER_ID)

                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("تم التراجع عن الدفعة بنجاح.", "نجاح")
    End Sub


    Public Function GetBatchReport(batchId As Guid) As DataTable
        Dim dt As New DataTable

        Using con As New SqlConnection(CON_STR)
            Dim sql = "
            SELECT *
            FROM Vw_IM_Units_Prices_Batch_Report
            WHERE BatchId = @BatchId
        "
            Using cmd As New SqlCommand(sql, con)
                cmd.Parameters.AddWithValue("@BatchId", batchId)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using

        Return dt
    End Function

    Private Sub ExportToExcel_Pro(dt As DataTable, Optional reportTitle As String = "تقرير تعديل الأسعار")
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            MessageBox.Show("لا توجد بيانات للتصدير.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim xlApp As Excel.Application = Nothing
        Dim xlWB As Excel.Workbook = Nothing
        Dim xlWS As Excel.Worksheet = Nothing

        Try
            xlApp = New Excel.Application()
            xlWB = xlApp.Workbooks.Add()
            xlWS = CType(xlWB.Sheets(1), Excel.Worksheet)

            ' تفعيل الاتجاه من اليمين لليسار
            xlWS.DisplayRightToLeft = True

            Dim rowsCount As Integer = dt.Rows.Count
            Dim colsCount As Integer = dt.Columns.Count

            ' عنوان التقرير في الصف الأول
            Dim titleRange As Excel.Range = xlWS.Range(xlWS.Cells(1, 1), xlWS.Cells(1, colsCount))
            titleRange.Merge()
            titleRange.Value = reportTitle
            titleRange.Font.Bold = True
            titleRange.Font.Size = 16
            titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            titleRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter

            ' رؤوس الأعمدة في الصف الثاني
            For c As Integer = 0 To colsCount - 1
                xlWS.Cells(2, c + 1) = dt.Columns(c).ColumnName
            Next

            Dim headerRange As Excel.Range = xlWS.Range(xlWS.Cells(2, 1), xlWS.Cells(2, colsCount))
            headerRange.Font.Bold = True
            headerRange.Interior.Color = Color.FromArgb(155, 194, 230) ' أزرق فاتح للرأس
            headerRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter

            ' نقل البيانات إلى مصفوفة مرة واحدة (أسرع من خلية خلية)
            Dim data(0 To rowsCount - 1, 0 To colsCount - 1) As Object
            For r As Integer = 0 To rowsCount - 1
                For c As Integer = 0 To colsCount - 1
                    data(r, c) = dt.Rows(r)(c)
                Next
            Next

            ' تعبئة البيانات من الصف الثالث
            Dim startDataCell As Excel.Range = xlWS.Cells(3, 1)
            Dim endDataCell As Excel.Range = xlWS.Cells(2 + rowsCount, colsCount)
            Dim writeRange As Excel.Range = xlWS.Range(startDataCell, endDataCell)
            writeRange.Value = data

            ' تنسيق حدود الجدول بالكامل
            Dim fullRange As Excel.Range = xlWS.Range(xlWS.Cells(2, 1), xlWS.Cells(2 + rowsCount, colsCount))
            fullRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous

            ' محاولة تنسيق الأعمدة الرقمية بشكل 0.000
            For c As Integer = 0 To colsCount - 1
                Dim colName As String = dt.Columns(c).ColumnName.ToLower()
                If colName.Contains("price") OrElse colName.Contains("sp") OrElse colName.Contains("cost") Then
                    Dim colRange As Excel.Range = xlWS.Range(xlWS.Cells(3, c + 1), xlWS.Cells(2 + rowsCount, c + 1))
                    colRange.NumberFormat = "0.000"
                End If
            Next

            ' AutoFit الأعمدة
            xlWS.Columns.AutoFit()

            ' تجميد الصف الأول والثاني (العنوان + الرؤوس)
            xlWS.Application.ActiveWindow.SplitRow = 2
            xlWS.Application.ActiveWindow.FreezePanes = True

            ' إظهار التطبيق للمستخدم
            xlApp.Visible = True

        Catch ex As Exception
            MessageBox.Show("خطأ أثناء التصدير إلى Excel:" & Environment.NewLine & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' تنظيف الموارد COM (مهم لتفادي بقاء Excel في الخلفية)
            If xlWS IsNot Nothing Then Marshal.ReleaseComObject(xlWS)
            If xlWB IsNot Nothing Then Marshal.ReleaseComObject(xlWB)
            If xlApp IsNot Nothing Then Marshal.ReleaseComObject(xlApp)
            xlWS = Nothing
            xlWB = Nothing
            xlApp = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub



    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim batchId As Guid
        If Not Guid.TryParse(Batches_Cm.Text.Trim(), batchId) Then
            MessageBox.Show("BatchId غير صحيح.")
            Exit Sub
        End If

        dgvHistory.DataSource = GetBatchReport(batchId)
    End Sub

    'Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
    '    Dim dt As DataTable = CType(dgvHistory.DataSource, DataTable)
    '    ExportToExcel(dt)
    'End Sub

    'Private Sub btnRollbackBatch_Click(sender As Object, e As EventArgs) Handles btnRollbackBatch.Click
    '    ' نفس كود التراجع الذي أعطيتك سابقًا
    'End Sub

    'Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
    '    ' هنا تربط مع ReportViewer أو PDF
    '    MessageBox.Show("يتم هنا ربط الطباعة مع Report Viewer أو PDF.")
    'End Sub

    Private Sub FrmPriceHistoryViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GET_Last_Batches()
    End Sub

    Private Sub GET_Last_Batches()
        Dim C = New C
        Try

            Dim sql As String = "   SELECT BatchId
FROM (
    SELECT DISTINCT BatchId,ChangeDate
    FROM Vw_IM_Units_Prices_Batch_Report
) AS t
ORDER BY ChangeDate DESC; "
            C.Da = New SqlClient.SqlDataAdapter(sql, C.Con)
            C.Da.Fill(C.Dt)
            Batches_Cm.DataSource = C.Dt
                Batches_Cm.DisplayMember = "BatchId"
            'Batches_Cm.ValueMember = "P_ID"
            Batches_Cm.SelectedIndex = -1
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    'Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
    '    Dim batchId As Guid
    '    If Not Guid.TryParse(txtBatchId.Text.Trim(), batchId) Then
    '        MessageBox.Show("BatchId غير صحيح.")
    '        Exit Sub
    '    End If

    '    dgvHistory.DataSource = GetBatchReport(batchId)
    'End Sub

    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        Dim dt As DataTable = TryCast(dgvHistory.DataSource, DataTable)

        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            MessageBox.Show("لا توجد بيانات للتصدير.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ExportToExcel_Pro(dt, "تقرير تعديل الأسعار - BatchId: " & Batches_Cm.Text.Trim())
    End Sub

    'Private Sub btnRollbackBatch_Click(sender As Object, e As EventArgs) Handles btnRollbackBatch.Click
    '    ' نفس كود التراجع الذي أعطيتك سابقًا
    'End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        ' هنا تربط مع ReportViewer أو PDF
        MessageBox.Show("يتم هنا ربط الطباعة مع Report Viewer أو PDF.")
    End Sub

End Class
