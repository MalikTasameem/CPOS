
Imports System.Data.OleDb
Imports System.Globalization


Imports UglyToad.PdfPig
Imports UglyToad.PdfPig.Content
Imports System.Text.RegularExpressions
Imports System.Data

Imports System.IO
Imports System.Data.SqlClient

Public Class Settlement_Sheet

    Public dtSystem As New DataTable()   ' كشف النظام (الأستاذ)
    Dim dtBank As New DataTable()     ' ملف المصرف
    Dim dtNotInSystem As New DataTable()  ' في المصرف وليس في النظام
    Dim dtNotInBank As New DataTable()    ' في النظام وليس في المصرف
    Dim dtDiffDetails As New DataTable()  ' اختلافات في القيم أو التاريخ



    Public Function ReadNCBBankPDF_Final(filePath As String) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Balance")
        dt.Columns.Add("Debit")
        dt.Columns.Add("Credit")
        dt.Columns.Add("ReferenceNo")
        dt.Columns.Add("ValueDate")
        dt.Columns.Add("TransactionDate")
        dt.Columns.Add("Description")

        ' 1️⃣ قراءة كل النصوص من PDF (كل الصفحات)
        Dim allText As String = ""
        Using doc = PdfDocument.Open(filePath)
            For Each page In doc.GetPages()
                allText &= page.Text
            Next
        End Using

        ' 2️⃣ تنظيف النص (إزالة المسافات الزائدة)
        allText = Regex.Replace(allText, "\s+", "")
        allText = allText.Replace(":", "").Replace("ـ", "")
        allText = allText.Replace("يتراتشم", "") ' إزالة التكرارات من العربية إذا لزم

        ' 3️⃣ إدخال مسافة بين الأرقام المتتالية 3 مرات (Balance, Debit, Credit)
        ' مثل 4097.75351.0000.000 → 4097.753 51.000 0.000
        allText = Regex.Replace(allText, "(\d,\d{3}\.\d{3})(\d+\.\d{3})(\d+\.\d{3})", "$1 $2 $3")

        ' 4️⃣ النمط: Balance Debit Credit RefNo ValueDate Description TransactionDate
        Dim pattern As String =
        "([\d,]+\.\d+)\s+([\d,]+\.\d+)\s+([\d,]+\.\d+)(0?56[A-Z0-9]+|BV\d+)(\d{1,2}/\d{1,2}/\d{4})(.*?)(\d{1,2}/\d{1,2}/\d{4})"

        Dim matches = Regex.Matches(allText, pattern)
        For Each m As Match In matches
            Dim balance = m.Groups(1).Value
            Dim debit = m.Groups(2).Value
            Dim credit = m.Groups(3).Value
            Dim refNo = m.Groups(4).Value
            Dim valDate = m.Groups(5).Value
            Dim desc = m.Groups(6).Value
            Dim trxDate = m.Groups(7).Value

            dt.Rows.Add(balance, debit, credit, refNo, valDate, trxDate, desc)
        Next

        MsgBox($"تم استخراج {dt.Rows.Count} عملية بنجاح.", MsgBoxStyle.Information, "PDF Parser")

        Return dt
    End Function

    Public Function ReadCsvFile(filePath As String) As DataTable
        Dim dt As New DataTable()

        Try
            Dim lines() As String = File.ReadAllLines(filePath, System.Text.Encoding.UTF8)

            ' إذا كان الملف يحتوي على عناوين الأعمدة في السطر الأول
            If lines.Length > 0 Then
                Dim headers = lines(0).Split(","c)
                For Each header In headers
                    dt.Columns.Add(header.Trim())
                Next

                ' قراءة بقية الصفوف
                For i As Integer = 1 To lines.Length - 1
                    If String.IsNullOrWhiteSpace(lines(i)) Then Continue For
                    Dim values = lines(i).Split(","c)
                    Dim row = dt.NewRow()

                    For j As Integer = 0 To Math.Min(values.Length - 1, dt.Columns.Count - 1)
                        row(j) = values(j).Trim()
                    Next
                    dt.Rows.Add(row)
                Next
            End If

        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء قراءة ملف CSV: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dt

    End Function


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvSystem.DataSource = dtSystem
    End Sub

    '-------------------------------
    ' قراءة ملف Excel (المصرف)
    '-------------------------------
    'Private Function ReadExcelOleDb(filePath As String) As DataTable
    '    Dim dt As New DataTable()
    '    Try
    '        Dim connStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & filePath &
    '                                ";Extended Properties='Excel 12.0 Xml;HDR=YES;'"

    '        Using conn As New OleDbConnection(connStr)
    '            conn.Open()
    '            Dim schemaTable As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
    '            Dim sheetName As String = schemaTable.Rows(0)("TABLE_NAME").ToString()
    '            Dim query As String = "SELECT * FROM [" & sheetName & "]"
    '            Using da As New OleDbDataAdapter(query, conn)
    '                da.Fill(dt)
    '            End Using
    '            conn.Close()
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show("حدث خطأ أثناء قراءة ملف Excel: " & ex.Message)
    '    End Try
    '    Return dt
    'End Function

    Public Function ReadExcelOleDb(filePath As String) As DataTable
        Dim dt As New DataTable()

        Try
            ' 🔹 سلسلة الاتصال الخاصة بملفات Excel الحديثة
            Dim connStr As String =
            "Provider=Microsoft.ACE.OLEDB.12.0;" &
            "Data Source=" & filePath & ";" &
            "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'"

            Using conn As New OleDbConnection(connStr)
                conn.Open()

                ' 🔹 الحصول على اسم الورقة الأولى
                Dim schemaTable As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                Dim sheetName As String = schemaTable.Rows(0)("TABLE_NAME").ToString()

                ' 🔹 قراءة البيانات
                Dim query As String = "SELECT * FROM [" & sheetName & "]"
                Using da As New OleDbDataAdapter(query, conn)
                    da.Fill(dt)
                End Using
            End Using

            ' ✅ بعد القراءة: تحويل أعمدة التاريخ تلقائيًا إلى صيغة ثابتة yyyy-MM-dd
            For Each col As DataColumn In dt.Columns
                If col.ColumnName.ToLower().Contains("date") OrElse col.ColumnName.Contains("تاريخ") Then
                    For Each row As DataRow In dt.Rows
                        Dim val As String = row(col).ToString().Trim()

                        ' إذا الخلية فارغة تخطّاها
                        If String.IsNullOrWhiteSpace(val) Then Continue For

                        '-------------------------------------------------------------------------------


                        If IsNumeric(val) Then
                            ' الحالة الأولى: التاريخ رقم تسلسلي (Excel Date Serial)
                            Dim serial As Double = Double.Parse(val, CultureInfo.InvariantCulture)
                            Dim baseDate As Date = #1/1/1900#
                            row(col) = baseDate.AddDays(serial - 2).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)

                        Else
                            ' نحاول قراءة التاريخ بعدة تنسيقات محتملة يدوياً
                            Dim parsedDate As Date

                            Dim formats() As String = {
        "yyyy-MM-dd", "dd-MM-yyyy", "MM-dd-yyyy",
        "yyyy/MM/dd", "dd/MM/yyyy", "MM/dd/yyyy"
    }

                            Dim success As Boolean = DateTime.TryParseExact(
        val,
        formats,
        CultureInfo.InvariantCulture,
        DateTimeStyles.None,
        parsedDate
    )
                            If success Then
                                ' نحفظ التاريخ بصيغة موحدة
                                'row(col) = parsedDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                                row(col) = parsedDate  ' نخزن التاريخ كـ DateTime حقيقي داخل الـ DataTable

                            Else
                                ' لم ينجح التحويل، نترك القيمة كما هي (قد تكون نص)
                                row(col) = val
                            End If
                        End If

                        '-------------------------------------------------------------------------------

                    Next
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("حدث خطأ أثناء قراءة ملف Excel: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dt
    End Function


    '-------------------------------
    ' دالة توحيد القيم
    '-------------------------------
    Private Function NormalizeValue(value As String, isDate As Boolean, isNumeric As Boolean) As String
        If String.IsNullOrWhiteSpace(value) Then Return ""
        If isDate Then
            Dim parsedDate As DateTime
            If DateTime.TryParse(value, parsedDate) Then
                Return parsedDate.ToString("yyyy-MM-dd")
            End If
        End If
        If isNumeric Then
            Dim num As Decimal
            If Decimal.TryParse(value.Replace(",", "").Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, num) Then
                Return num.ToString("0.##")
            End If
        End If
        Return value.Trim().ToLower()
    End Function

    '-------------------------------
    ' زر استيراد ملف المصرف
    '-------------------------------
    Private Sub btnImportBank_Click(sender As Object, e As EventArgs) Handles btnImportBank.Click
        'Dim ofd As New OpenFileDialog()
        'ofd.Filter = "Excel Files|*.xlsx;*.xls"
        'If ofd.ShowDialog() = DialogResult.OK Then
        '    dtBank = ReadExcelOleDb(ofd.FileName)
        '    dgvBank.DataSource = dtBank

        '    ' تعبئة القوائم بأسماء الأعمدة
        '    cmbMapMove.Items.Clear()
        '    cmbMapDate.Items.Clear()
        '    cmbMapValue.Items.Clear()
        '    For Each col As DataColumn In dtBank.Columns
        '        cmbMapMove.Items.Add(col.ColumnName)
        '        cmbMapDate.Items.Add(col.ColumnName)
        '        cmbMapValue.Items.Add(col.ColumnName)
        '    Next
        'End If
        '------------------------------------------------------------------------------------
        MsgBox("تأكد من تفاصيل الكشف الدفتري قبل الإستيراد :" & vbNewLine & Me.Text, MsgBoxStyle.Information, "تنويه")


        Dim ofd As New OpenFileDialog()
        ofd.Filter = "All Supported Files|*.pdf;*.xlsx;*.xls;*.csv|Excel Files|*.xlsx;*.xls|CSV Files|*.csv|PDF Files|*.pdf"

        If ofd.ShowDialog() = DialogResult.OK Then
            Dim ext = Path.GetExtension(ofd.FileName).ToLower()

            Select Case ext
                Case ".pdf"
                    dtBank = ReadNCBBankPDF_Final(ofd.FileName)

                Case ".xlsx", ".xls"
                    dtBank = ReadExcelOleDb(ofd.FileName)

                Case ".csv"
                    dtBank = ReadCsvFile(ofd.FileName)

                Case Else
                    MessageBox.Show("صيغة الملف غير مدعومة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
            End Select

            ' عرض البيانات
            dgvBank.DataSource = dtBank

            ' تعبئة القوائم المنسدلة بأسماء الأعمدة
            cmbMapMove.Items.Clear()
            cmbMapDate.Items.Clear()
            cmbMapValue.Items.Clear()
            For Each col As DataColumn In dtBank.Columns
                cmbMapMove.Items.Add(col.ColumnName)
                cmbMapDate.Items.Add(col.ColumnName)
                cmbMapValue.Items.Add(col.ColumnName)
            Next

            MessageBox.Show($"تم تحميل الملف ({ext}) بنجاح. عدد الصفوف: {dtBank.Rows.Count}", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    '-------------------------------
    ' زر تنفيذ المطابقة الثنائية
    '-------------------------------
    Private Sub btnMatch_Click(sender As Object, e As EventArgs) Handles btnMatch.Click
        If cmbMapMove.Text = "" Or cmbMapDate.Text = "" Or cmbMapValue.Text = "" Then
            MessageBox.Show("يرجى تحديد الأعمدة المقابلة لرقم الحركة البنكية، التاريخ والقيمة.")
            Exit Sub
        End If


        ' إنشاء جداول النتائج
        dtNotInSystem = dtBank.Clone()
        dtNotInBank = dtSystem.Clone()
        dtDiffDetails = dtBank.Clone()
        dtDiffDetails.Columns.Add("ملاحظة")

        ' ------------------------------
        ' 1️⃣ من المصرف إلى النظام
        ' ------------------------------
        For Each rowBank As DataRow In dtBank.Rows
            Dim bankMove = NormalizeValue(rowBank(cmbMapMove.Text).ToString(), False, False)
            Dim bankDate = NormalizeValue(rowBank(cmbMapDate.Text).ToString(), True, False)
            Dim bankValue = NormalizeValue(rowBank(cmbMapValue.Text).ToString(), False, True)

            Dim match = dtSystem.AsEnumerable().FirstOrDefault(Function(r) NormalizeValue(r("رقم الحركة البنكية").ToString(), False, False) = bankMove)

            If match Is Nothing Then
                dtNotInSystem.ImportRow(rowBank) ' غير موجود في النظام
            Else
                ' موجود ولكن تحقق من اختلاف التفاصيل
                Dim sysDate = NormalizeValue(match("التاريخ").ToString(), True, False)
                Dim sysValue = NormalizeValue(match("القيمة").ToString(), False, True)
                If bankDate <> sysDate Or bankValue <> sysValue Then
                    Dim diff = rowBank.ItemArray.Clone()
                    Dim newRow = dtDiffDetails.NewRow()
                    newRow.ItemArray = diff
                    newRow("ملاحظة") = $"اختلاف في {(If(bankDate <> sysDate, "التاريخ ", ""))}{(If(bankValue <> sysValue, "القيمة", ""))}"
                    dtDiffDetails.Rows.Add(newRow)
                End If
            End If
        Next

        ' ------------------------------
        ' 2️⃣ من النظام إلى المصرف
        ' ------------------------------
        For Each rowSys As DataRow In dtSystem.Rows
            Dim sysMove = NormalizeValue(rowSys("رقم الحركة البنكية").ToString(), False, False)
            Dim exists = dtBank.AsEnumerable().Any(Function(r) NormalizeValue(r(cmbMapMove.Text).ToString(), False, False) = sysMove)
            If Not exists Then
                dtNotInBank.ImportRow(rowSys)
            End If
        Next

        ' عرض النتائج
        dgvNotInSystem.DataSource = dtNotInSystem
        dgvNotInBank.DataSource = dtNotInBank
        dgvDiff.DataSource = dtDiffDetails


        UpdateSummary()
        dgvJournal.DataSource = GenerateJournalEntries()
        dgvJournal.Columns("COST_ID").Visible = False
        'dgvJournal.Columns("التاريخ").DefaultCellStyle.Format = "yyyy-MM-dd"


        Adjust_DATE()


        If dtNotInBank.Rows.Count = 0 And dtNotInSystem.Rows.Count = 0 And dtDiffDetails.Rows.Count = 0 Then
            NO_DIF_Label.Visible = True
            TabControl1.Enabled = False
        Else
            NO_DIF_Label.Visible = False
            TabControl1.Enabled = True
        End If

        MessageBox.Show("تمت عملية المطابقة بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information)




    End Sub


    Private Sub Adjust_DATE()


        Dim dtFixed As DataTable = dtEntries.Clone()
        dtFixed.Columns("التاريخ").DataType = GetType(Date)

        For Each r As DataRow In dtEntries.Rows
            Dim val = r("التاريخ").ToString().Trim()
            Dim parsed As Date = Date.MinValue
            Dim success As Boolean = False

            If Not String.IsNullOrEmpty(val) Then
                ' نحاول أولاً بالأنماط الشائعة
                Dim formats() As String = {
            "yyyy-MM-dd", "dd/MM/yyyy", "MM/dd/yyyy",
            "yyyy/MM/dd", "dd-MM-yyyy", "MM-dd-yyyy"
        }

                success = DateTime.TryParseExact(
            val,
            formats,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            parsed
        )

                ' إذا فشل TryParseExact نحاول TryParse العادي (مرن أكثر)
                If Not success Then
                    success = Date.TryParse(val, CultureInfo.CurrentCulture, DateTimeStyles.None, parsed)
                End If
            End If

            ' فقط أضف الصف إذا لدينا تاريخ صالح
            If success AndAlso parsed > Date.MinValue Then
                Dim newRow = dtFixed.NewRow()

                For Each c As DataColumn In dtEntries.Columns
                    If c.ColumnName = "التاريخ" Then
                        newRow(c.ColumnName) = parsed
                    Else
                        newRow(c.ColumnName) = r(c)
                    End If
                Next

                dtFixed.Rows.Add(newRow)
            Else
                ' في حال لم يُستطع قراءة التاريخ، انسخ الصف كما هو حتى لا نخسر بياناته
                Dim newRow = dtFixed.NewRow()
                For Each c As DataColumn In dtEntries.Columns
                    newRow(c.ColumnName) = r(c)
                Next
                dtFixed.Rows.Add(newRow)
            End If
        Next

        ' استبدال الجدول القديم بالجديد
        dtEntries = dtFixed

        ' اختياري: أظهر عدد الصفوف المقروءة فعلاً
        'MessageBox.Show($"تم تجهيز {dtEntries.Rows.Count} صف للتسوية.", "تحويل التاريخ", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub



    Dim is_add_cost As Boolean = False

    '----------------------------------------
    ' تحديث  🔄الملخص بعد كل عملية مطابقة
    '----------------------------------------
    Private Sub UpdateSummary()
        lblTotalSystem.Text = $"📘 عدد حركات النظام: {dtSystem.Rows.Count}"
        lblTotalBank.Text = $"🏦 عدد حركات المصرف: {dtBank.Rows.Count}"
        lblNotInSystem.Text = $"🔴 غير موجود في النظام: {dtNotInSystem.Rows.Count}"
        lblNotInBank.Text = $"🟠 غير موجود في المصرف: {dtNotInBank.Rows.Count}"
        lblDiff.Text = $"🟡 اختلافات: {dtDiffDetails.Rows.Count}"
    End Sub


    '-------------------------------
    ' تلوين النتائج حسب النوع
    '-------------------------------
    Private Sub dgvDiff_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles dgvDiff.RowPrePaint
        dgvDiff.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGoldenrodYellow
    End Sub

    Private Sub dgvNotInSystem_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles dgvNotInSystem.RowPrePaint
        dgvNotInSystem.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightCoral
    End Sub

    Private Sub dgvNotInBank_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles dgvNotInBank.RowPrePaint
        dgvNotInBank.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightBlue
    End Sub

    Private Sub dgvJournal_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles dgvJournal.RowPrePaint
        Dim src = dgvJournal.Rows(e.RowIndex).Cells("المصدر").Value?.ToString()
        If src = "غير موجود في النظام" Then
            dgvJournal.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightCoral
        ElseIf src = "غير موجود في المصرف" Then
            dgvJournal.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightBlue
        ElseIf src = "اختلاف" Then
            dgvJournal.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub


    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "CSV File|*.csv"
        sfd.FileName = "نتائج_المطابقة.csv"

        If sfd.ShowDialog() = DialogResult.OK Then
            Using sw As New IO.StreamWriter(sfd.FileName, False, System.Text.Encoding.UTF8)
                sw.WriteLine("نوع النتيجة,رقم الحركة البنكية,التاريخ,القيمة,ملاحظة")

                ' التصدير من الجداول الثلاثة
                For Each r As DataRow In dtNotInSystem.Rows
                    sw.WriteLine($"غير موجود في النظام,{r(0)},{r(1)},{r(2)},")
                Next

                For Each r As DataRow In dtNotInBank.Rows
                    sw.WriteLine($"غير موجود في المصرف,{r(0)},{r(1)},{r(2)},")
                Next

                For Each r As DataRow In dtDiffDetails.Rows
                    sw.WriteLine($"اختلاف,{r(0)},{r(1)},{r(2)},{r("ملاحظة")}")
                Next

                For Each r As DataRow In dtEntries.Rows
                    sw.WriteLine($"مذكرة التسوية المحاسبية,{r(0)}{r(1)},{r(2)},{r(3)},{r(4)},{r(5)},{r(6)},{r(7)},{r(8)},{r(9)},{r(10)},{r(11)}")
                Next

            End Using

            MessageBox.Show("✅ تم تصدير النتائج بنجاح إلى ملف CSV.", "تم", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub


    '---------------------------------------------
    ' توليد مذكرة التسوية المحاسبية من الفروقات
    '---------------------------------------------

    Dim dtEntries As New DataTable()
    Private Function GenerateJournalEntries() As DataTable
        dtEntries = New DataTable
        dtEntries.Columns.Add("رقم الحركة البنكية")
        dtEntries.Columns.Add("التاريخ")
        dtEntries.Columns.Add("COST_ID")
        dtEntries.Columns.Add("مركز التكلفة")
        dtEntries.Columns.Add("الحساب المدين")
        dtEntries.Columns.Add("رقم الحساب المدين")
        dtEntries.Columns.Add("الحساب الدائن")
        dtEntries.Columns.Add("رقم الحساب الدائن")
        dtEntries.Columns.Add("القيمة") '
        dtEntries.Columns.Add("البيان")
        dtEntries.Columns.Add("المصدر")
        dtEntries.Columns.Add("الإجراء")


        ' 🔴 عمليات موجودة في المصرف وليست في النظام
        For Each r As DataRow In dtNotInSystem.Rows
            Dim newRow = dtEntries.NewRow()
            newRow("رقم الحركة البنكية") = r(cmbMapMove.Text)
            newRow("التاريخ") = r("التاريخ") '
            newRow("الحساب المدين") = "البنك"
            newRow("الحساب الدائن") = "الإيرادات/العميل"
            newRow("القيمة") = r(cmbMapValue.Text)  'r("القيمة") '
            newRow("البيان") = "عملية موجودة في المصرف وغير مسجلة بالنظام"
            newRow("المصدر") = "غير موجود في النظام"
            newRow("الإجراء") = "(قيد جديد)"
            newRow("COST_ID") = "1"
            newRow("مركز التكلفة") = "N/A"
            dtEntries.Rows.Add(newRow)
        Next

        ' 🟠 عمليات موجودة في النظام وليست في المصرف
        For Each r As DataRow In dtNotInBank.Rows
            Dim newRow = dtEntries.NewRow()
            newRow("رقم الحركة البنكية") = r("رقم الحركة البنكية")
            newRow("التاريخ") = r("التاريخ") '
            newRow("الحساب المدين") = "تحت التسوية"
            newRow("الحساب الدائن") = "البنك"
            newRow("القيمة") = r("القيمة") '
            newRow("البيان") = "عملية بالنظام غير ظاهرة في المصرف"
            newRow("المصدر") = "غير موجود في المصرف"
            newRow("الإجراء") = "(إلغاء القيد)"
            dtEntries.Rows.Add(newRow)
        Next

        ' 🟡 اختلافات في القيم أو التاريخ
        For Each r As DataRow In dtDiffDetails.Rows
            Dim newRow = dtEntries.NewRow()
            newRow("رقم الحركة البنكية") = r(cmbMapMove.Text)
            newRow("التاريخ") = r("التاريخ") '
            newRow("الحساب المدين") = "فرق تسوية البنك"
            newRow("الحساب الدائن") = "البنك"
            newRow("القيمة") = r(cmbMapValue.Text) 'r("القيمة") '
            newRow("البيان") = If(r.Table.Columns.Contains("ملاحظة"), r("ملاحظة"), "فرق في القيمة أو التاريخ")
            newRow("المصدر") = "اختلاف"
            newRow("الإجراء") = "(تعديل القيد)"
            dtEntries.Rows.Add(newRow)
        Next

        Return dtEntries
    End Function


    '---------------------------------------------
    ' تعريف كائن القيد المحاسبي (JournalEntry)
    '---------------------------------------------
    Public Class JournalEntry
        Public Property EntryDate As Date
        Public Property DebitAccount As String
        Public Property CreditAccount As String
        Public Property Amount As Decimal
        Public Property Description As String
        Public Property Source As String ' نوع الاختلاف (من أين أتى القيد)
    End Class

    Private Sub dgvJournal_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvJournal.CellMouseDoubleClick


        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            Dim columnName As String = dgvJournal.Columns(e.ColumnIndex).Name


            If columnName = "رقم الحساب الدائن" Or columnName = "الحساب الدائن" Then

                BALANCE_SEARCH.ShowDialog()
                If ACC_CODE_Search <> "" Then

                    If ALL_CB.Checked Then
                        SET_COLUMN_VALUE(dgvJournal, "رقم الحساب الدائن", ACC_CODE_Search)
                        SET_COLUMN_VALUE(dgvJournal, "الحساب الدائن", ACC_NAME_Search)
                    Else
                        dgvJournal.Rows(e.RowIndex).Cells("رقم الحساب الدائن").Value = ACC_CODE_Search
                        dgvJournal.Rows(e.RowIndex).Cells("الحساب الدائن").Value = ACC_NAME_Search
                    End If

                End If

            ElseIf columnName = "رقم الحساب المدين" Or columnName = "الحساب المدين" Then
                BALANCE_SEARCH.ShowDialog()
                If ACC_CODE_Search <> "" Then

                    If ALL_CB.Checked Then
                        SET_COLUMN_VALUE(dgvJournal, "رقم الحساب المدين", ACC_CODE_Search)
                        SET_COLUMN_VALUE(dgvJournal, "الحساب المدين", ACC_NAME_Search)
                    Else
                        dgvJournal.Rows(e.RowIndex).Cells("رقم الحساب المدين").Value = ACC_CODE_Search
                        dgvJournal.Rows(e.RowIndex).Cells("الحساب المدين").Value = ACC_NAME_Search
                    End If

                End If

            ElseIf columnName = "البيان" Then
                Dim inp = InputBox("ادخل بيان القيد", "بيان القيد")
                If inp <> "" Then dgvJournal.Rows(e.RowIndex).Cells("البيان").Value = inp


            ElseIf columnName = "مركز التكلفة" Then

                Dim dialog As New SingleChoiceDialog()
                dialog.Text = "حدد مركز التكلفة"
                dialog.ListBox1.DataSource = CostCenter_Datatable
                dialog.ListBox1.DisplayMember = "COST_NAME"
                dialog.ListBox1.ValueMember = "COST_ID"
                dialog.ListBox1.SelectedIndex = 0
                ' عرض مربع الحوار
                If dialog.ShowDialog() = DialogResult.OK Then

                    dgvJournal.Rows(e.RowIndex).Cells("COST_ID").Value = dialog.ListBox1.SelectedValue
                    dgvJournal.Rows(e.RowIndex).Cells("مركز التكلفة").Value = dialog.ListBox1.Text

                End If

            End If

            '' لو العمود ليس هو المطلوب، يمكنك مثلاً تجاهل أو تنبيه المستخدم
            'MessageBox.Show("يمكن التعديل فقط في عمود الكود.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub


    Private Sub dgvJournal_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvJournal.CellValueChanged
        dgvJournal.EndEdit()
        Dim dt As DataTable = TryCast(dgvJournal.DataSource, DataTable)
        If dt IsNot Nothing Then
            dt.AcceptChanges()
        End If
    End Sub



    Private Sub SET_COLUMN_VALUE(ByRef GV As DataGridView, CL_NAME As String, VALUE As String)
        For Each row As DataGridViewRow In GV.Rows
            ' تأكد من أنه ليس صف "جديد" (آخر صف فارغ)
            If Not row.IsNewRow Then
                row.Cells(CL_NAME).Value = VALUE
            End If
        Next
    End Sub

    Private Sub Settlement_Btn_Click(sender As Object, e As EventArgs) Handles Settlement_Btn.Click


        Dim missing = dtEntries.Select("التاريخ IS NULL ")
        If missing.Length > 0 Then
            MessageBox.Show("هناك " & missing.Length & " صفوف ناقصة بيانات (تاريخ أو كود حساب) سيتم تجاهلها.")
        End If


        '--------------------------------------------------------------------------------------------------<tmp>


        If dgvJournal.IsCurrentCellInEditMode Then dgvJournal.EndEdit()
        dtEntries = TryCast(dgvJournal.DataSource, DataTable)
        dtEntries.AcceptChanges()

        '' نفترض أن لديك DataTable اسمه dt
        'Dim message As New System.Text.StringBuilder()

        '' إضافة أسماء الأعمدة أولاً
        'For Each col As DataColumn In dtEntries.Columns
        '    message.Append(col.ColumnName & vbTab)
        'Next
        'message.AppendLine()

        '' إضافة الصفوف
        'For Each row As DataRow In dtEntries.Rows
        '    For Each col As DataColumn In dtEntries.Columns
        '        message.Append(row(col).ToString() & vbTab)
        '    Next
        '    message.AppendLine()
        'Next

        '' عرض النتيجة في MsgBox
        'MsgBox(message.ToString())


        '-----------------------------------------------------------------------------------------------<tmp>

        Adjust_DATE()

        If Check_Validation() = False Then
            MsgBox(msg_error, MsgBoxStyle.Critical, "خطأ فالتسوية")
        Else
            If MessageBox.Show(" تطبيق التسوية للحساب مع الحساب الدفتري  ", "تاكيــد العملية", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign) = DialogResult.OK Then
                SendDataTableToProcedure()
            End If
        End If

    End Sub

    Dim msg_error As String = ""
    Private Function Check_Validation()
        msg_error = ""
        Dim note_no_debit As String = ""
        Dim note_no_credit As String = ""
        Dim no_date_str As String = ""
        Dim salesAccountBlockMessages As New List(Of String)
        Dim userAccountBlockMessages As New List(Of String)
        Dim no_debit = 0, no_credit = 0, no_date = 0
        Dim f As Boolean = True

        For i = 0 To dgvJournal.Rows.Count - 1

            Dim debitValue As Object = dgvJournal.Rows(i).Cells("رقم الحساب المدين").Value
            Dim creditValue As Object = dgvJournal.Rows(i).Cells("رقم الحساب الدائن").Value

            If debitValue Is Nothing OrElse IsDBNull(debitValue) Then
                note_no_debit = "سجل غير مخصص به حساب مدين = "
                no_debit += 1
                f = False
            Else
                Dim debitBlockMessage As String = GetSalesSystemAccountBlockMessage(debitValue, "الحساب المدين")
                If Not String.IsNullOrWhiteSpace(debitBlockMessage) Then
                    salesAccountBlockMessages.Add("السطر " & (i + 1).ToString() & ":" & vbCrLf & debitBlockMessage)
                    f = False
                Else
                    Dim debitPermissionMessage As String = GetUserJournalAccountPermissionMessage(debitValue, "الحساب المدين")
                    If Not String.IsNullOrWhiteSpace(debitPermissionMessage) Then
                        userAccountBlockMessages.Add("السطر " & (i + 1).ToString() & ":" & vbCrLf & debitPermissionMessage)
                        f = False
                    End If
                End If
            End If

            If creditValue Is Nothing OrElse IsDBNull(creditValue) Then
                note_no_credit = "سجل غير مخصص به حساب دائن = "
                no_credit += 1
                f = False
            Else
                Dim creditBlockMessage As String = GetSalesSystemAccountBlockMessage(creditValue, "الحساب الدائن")
                If Not String.IsNullOrWhiteSpace(creditBlockMessage) Then
                    salesAccountBlockMessages.Add("السطر " & (i + 1).ToString() & ":" & vbCrLf & creditBlockMessage)
                    f = False
                Else
                    Dim creditPermissionMessage As String = GetUserJournalAccountPermissionMessage(creditValue, "الحساب الدائن")
                    If Not String.IsNullOrWhiteSpace(creditPermissionMessage) Then
                        userAccountBlockMessages.Add("السطر " & (i + 1).ToString() & ":" & vbCrLf & creditPermissionMessage)
                        f = False
                    End If
                End If
            End If

            If dgvJournal.Rows(i).Cells("التاريخ").Value Is Nothing OrElse IsDBNull(dgvJournal.Rows(i).Cells("التاريخ").Value) Then
                no_date_str = "سجل غير مخصص به تاريخ = "
                no_date += 1
                f = False
            End If


        Next

        msg_error = note_no_debit & no_debit & vbNewLine & note_no_credit & no_credit & vbNewLine & no_date_str & no_date
        If salesAccountBlockMessages.Count > 0 Then
            msg_error &= vbNewLine & vbNewLine & String.Join(vbNewLine & vbNewLine, salesAccountBlockMessages)
        End If
        If userAccountBlockMessages.Count > 0 Then
            msg_error &= vbNewLine & vbNewLine & String.Join(vbNewLine & vbNewLine, userAccountBlockMessages)
        End If

        Return f
    End Function

    Sub SendDataTableToProcedure()
        Dim connStr As String = MY_Settings.SqlConStr
        Using conn As New SqlConnection(connStr)
            Using cmd As New SqlCommand("dbo.ProcessEntries", conn)
                cmd.CommandType = CommandType.StoredProcedure
                Dim param As New SqlParameter("@Entries", SqlDbType.Structured)
                param.TypeName = "dbo.EntryTableType"
                param.Value = dtEntries
                cmd.Parameters.Add(param)
                cmd.Parameters.AddWithValue("@User_ID", USER_ID)
                If SQL_SP_EXEC(cmd) = True Then
                    MsgBox("تم إدراج قيود التسوية فالنظـــام بنجـــاح", MsgBoxStyle.Information, "نجاح")
                End If

                'conn.Open()
                'cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class

