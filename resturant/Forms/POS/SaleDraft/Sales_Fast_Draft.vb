Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports System.Drawing.Printing


Public Class Sales_Fast_Draft : Inherits System.Windows.Forms.Form
    Private Enum ScreenStatusType
        Ready
        Information
        Loading
        Success
        Warning
        [Error]
    End Enum

    Dim Print_CompName As String = ""
    Dim Print_EngName As String = ""
    Dim Print_BillNotes As String = ""
    Private Print_LogoImage As Image = Nothing
    Dim Print_Y As Integer = 0
    Private Print_PaymentName As String = ""
    Private ReadOnly Print_PaymentLines As New List(Of SalesPrintPaymentLine)
    ' مؤقت للاختبار: أعد القيمة إلى False لاستعادة الطباعة المباشرة.
    Private Const ForceDraftPrintPreview As Boolean = False


    Dim rs As New Resizer
    Dim FormState As String = ""
    Dim DefaultFormState As String = ""
    Dim EditState As String = ""
    Public T_ID As Integer
    Public isDepended As Boolean
    Public isVoid As Boolean
    Public isPause As Boolean
    Public IM_ID As Integer = 0

    Public TOTAL As Double = 0
    Public Disc As Double = 0
    Public Pure As Double = 0
    Public AG_ID As Integer = 1
    Dim U_Dt As New DataTable
    Dim Get_Unit As Boolean = False
    Public U_Cargo As Double = 0
    Public Barcode As String = ""
    Dim BillUser_ID As Integer
    Public On_Update As Boolean
    Public IM_U_ID As Integer
    Dim Min_SP As Double
    Public SB_ID As Integer
    Dim U_IM_ID As Integer
    Public Barcode_IM As String = ""
    Public is_Valid As Boolean = False
    Public IM_Name As String
    Public IM_Unit_Name As String
    Public Valid_TXT As String = ""
    Dim Sales_BillPage_Bill_Track_FAST As String
    Dim Sales_Page_ID_FAST As Integer
    Public IM_Dt_Barcodes As New DataTable
    Public IM_Dt As New DataTable
    Public IM_Units_Dt As New DataTable

    Dim Bercent_Price As Double
    Public QtyTextBox As Double = 0
    Public IM_Price As Double
    Public IM_Cost As Double
    Private ReadOnly RefreshButtonDefaultBackColor As Color = Color.FromArgb(14, 116, 144)
    Private ReadOnly RefreshButtonDefaultText As String = "🔄" & Environment.NewLine & "تحديث الأصناف"
    Private DraftButtonDefaultText As String = ""
    Private DraftButtonDefaultBackColor As Color
    Private DraftButtonDefaultForeColor As Color
    Private DraftButtonDefaultBorderColor As Color
    Private DraftButtonDefaultFont As Font
    Private IsDraftButtonDefaultStyleCaptured As Boolean = False
    Private ShortcutGroupPanel As Panel = Nothing
    Private ShortcutItemsPanel As Panel = Nothing
    Private ShortcutItemsDt As DataTable = Nothing
    Private ShortcutSelectedGroupID As Integer = -1
    Private DraftLogsDt As DataTable = Nothing
    Private DraftLogsBindingSource As BindingSource = Nothing
    Private DraftLogsPanel As Panel = Nothing
    Private DraftLogsGrid As DataGridView = Nothing
    Private DraftLogsToggleButton As Button = Nothing
    Private DraftLogsCountLabel As Label = Nothing
    Private DraftLogsCounter As Integer = 0
    Private LastLoggedNotesText As String = ""
    Private HasLoadedPendingDraftLogs As Boolean = False
    Private Const DraftLogsPushThreshold As Integer = 600
    Private ReadOnly DraftLogsQueueFolder As String = IO.Path.Combine(Application.StartupPath, "DraftSales\LogsQueue")
    Private ReadOnly DraftLogsQueueFilePath As String = IO.Path.Combine(Application.StartupPath, "DraftSales\LogsQueue\SalesDraftActionLogs.xml")
    Private ReadOnly SalesCacheFolder As String = IO.Path.Combine(Application.StartupPath, "DraftSales\Cache")
    Private ReadOnly ItemsCacheFilePath As String = IO.Path.Combine(Application.StartupPath, "DraftSales\Cache\ItemsUnits.xml")
    Private ReadOnly ShortcutsCacheFilePath As String = IO.Path.Combine(Application.StartupPath, "DraftSales\Cache\ItemShortcuts.xml")
    Private LastItemsLoadError As String = ""
    Private LastShortcutsLoadError As String = ""

    Private Sub SetScreenStatus(message As String,
                                statusType As ScreenStatusType,
                                Optional details As String = "")

        If ScreenStatusStrip Is Nothing OrElse ScreenStatusStrip.IsDisposed Then Exit Sub

        If ScreenStatusStrip.InvokeRequired Then
            ScreenStatusStrip.BeginInvoke(New Action(Of String, ScreenStatusType, String)(AddressOf SetScreenStatus), message, statusType, details)
            Exit Sub
        End If

        Dim statusText As String = "● جاهز"
        Dim statusColor As Color = Color.FromArgb(30, 64, 175)
        Dim statusBackColor As Color = Color.FromArgb(239, 246, 255)

        Select Case statusType
            Case ScreenStatusType.Information
                statusText = "● معلومات"
                statusColor = Color.FromArgb(3, 105, 161)
                statusBackColor = Color.FromArgb(240, 249, 255)
            Case ScreenStatusType.Loading
                statusText = "● تحميل"
                statusColor = Color.FromArgb(180, 83, 9)
                statusBackColor = Color.FromArgb(255, 251, 235)
            Case ScreenStatusType.Success
                statusText = "● نجاح"
                statusColor = Color.FromArgb(21, 128, 61)
                statusBackColor = Color.FromArgb(240, 253, 244)
            Case ScreenStatusType.Warning
                statusText = "● تنبيه"
                statusColor = Color.FromArgb(194, 65, 12)
                statusBackColor = Color.FromArgb(255, 247, 237)
            Case ScreenStatusType.Error
                statusText = "● خطأ"
                statusColor = Color.FromArgb(185, 28, 28)
                statusBackColor = Color.FromArgb(254, 242, 242)
        End Select

        ScreenStatusStrip.BackColor = statusBackColor
        ScreenStatusTypeLabel.Text = statusText
        ScreenStatusTypeLabel.ForeColor = statusColor
        ScreenStatusMessageLabel.Text = If(String.IsNullOrWhiteSpace(message), "الشاشة جاهزة للاستخدام", message)
        ScreenStatusMessageLabel.ForeColor = statusColor
        ScreenStatusMessageLabel.ToolTipText = If(String.IsNullOrWhiteSpace(details), ScreenStatusMessageLabel.Text, details)
        ScreenStatusProgressBar.Visible = (statusType = ScreenStatusType.Loading)
        ScreenStatusTimeLabel.Text = DateTime.Now.ToString("HH:mm:ss")
    End Sub

    Private Function GetStatusTypeFromDraftAction(actionType As String, actionDescription As String) As ScreenStatusType
        Dim combinedText As String = (If(actionType, "") & " " & If(actionDescription, "")).Trim()

        If combinedText.Contains("تعذر") OrElse combinedText.Contains("فشل") OrElse combinedText.Contains("خطأ") Then Return ScreenStatusType.Error
        If combinedText.Contains("لم يتم") OrElse combinedText.Contains("إلغاء") OrElse combinedText.Contains("حذف") Then Return ScreenStatusType.Warning
        If combinedText.Contains("بدء") OrElse combinedText.Contains("جاري") Then Return ScreenStatusType.Loading
        If combinedText.Contains("تم ") Then Return ScreenStatusType.Success

        Return ScreenStatusType.Information
    End Function
    '--------------------------------------------------------------------------------------------------------------
    Private Sub LoadPrintSettings()
        Dim db As New C()
        Try
            db.Str = "SELECT TOP 1 CompName, englishN, BillNotes, LOGO FROM SysSetting"
            db.Com = New SqlClient.SqlCommand(db.Str, db.Con)
            db.Con.Open()
            db.Dr = db.Com.ExecuteReader()
            If db.Dr.Read() Then
                Print_CompName = db.Dr("CompName").ToString()
                Print_EngName = If(IsDBNull(db.Dr("englishN")), "", db.Dr("englishN").ToString())
                Print_BillNotes = db.Dr("BillNotes").ToString()
                If Not IsDBNull(db.Dr("LOGO")) Then
                    Dim Data As Byte() = DirectCast(db.Dr("LOGO"), Byte())
                    Using MS As New IO.MemoryStream(Data)
                        Using LogoSource As Image = Image.FromStream(MS)
                            Print_LogoImage = New Bitmap(LogoSource)
                        End Using
                    End Using
                Else
                    Print_LogoImage = Nothing
                End If
            End If
        Catch ex As Exception
        Finally
            If db.Dr IsNot Nothing AndAlso db.Dr.IsClosed = False Then db.Dr.Close()
            If db.Con.State = ConnectionState.Open Then db.Con.Close()
        End Try
    End Sub

    Public Sub PrintCurrentBill()

        LoadPrintPaymentLines()

        Dim paymentRowsHeight As Integer = If(Print_PaymentLines.Count = 0, 0, (Print_PaymentLines.Count * 22) + 44)
        Dim EstimatedHeight As Integer = 520 + (dgvSales.Rows.Count * 30) + paymentRowsHeight + Math.Max(0, EstimateReceiptBillNotesHeight(Print_BillNotes) - 40)

        If Not ForceDraftPrintPreview AndAlso String.IsNullOrWhiteSpace(Default_Printer_80) Then
            MsgBox("لم يتم تحديد طابعة البيع السريع الإفتراضية", MsgBoxStyle.Exclamation, "تحديــد طابعة الكاشير")
            Exit Sub
        End If

        If Not ForceDraftPrintPreview AndAlso IsPrinterInstalled(Default_Printer_80) = False Then
            MsgBox("الطابعة المحددة غير موجودة: " & Default_Printer_80, MsgBoxStyle.Exclamation, "تحديــد طابعة الكاشير")
            Exit Sub
        End If

        Using pd As New PrintDocument()
            ' 🌟 عرض الورقة 280 بكسل لضمان التوافق مع أضيق الطابعات 🌟
            If Not ForceDraftPrintPreview Then
                pd.PrinterSettings.PrinterName = Default_Printer_80
                pd.PrintController = New StandardPrintController()
            End If
            pd.DefaultPageSettings.PaperSize = New System.Drawing.Printing.PaperSize("Thermal80mm", 280, EstimatedHeight)
            pd.DefaultPageSettings.Margins = New System.Drawing.Printing.Margins(0, 0, 0, 0)

            AddHandler pd.PrintPage, AddressOf PrintReceiptPage

            If ForceDraftPrintPreview Then
                Using preview As New PrintPreviewDialog()
                    preview.Document = pd
                    preview.WindowState = FormWindowState.Maximized
                    preview.ShowDialog(Me)
                End Using
            Else
                pd.Print()
            End If
        End Using
    End Sub

    Private Function IsPrinterInstalled(printerName As String) As Boolean

        For Each installedPrinter As String In PrinterSettings.InstalledPrinters
            If String.Equals(installedPrinter, printerName, StringComparison.OrdinalIgnoreCase) Then Return True
        Next

        Return False

    End Function
    ' ========================================================
    ' 🌟 رسم الفاتورة (مضغوطة الصفوف بخط أصغر للأصناف) 🌟
    ' ========================================================
    Private Sub PrintReceiptPage(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics
        Print_Y = 10
        Dim PaperWidth As Integer = 280

        ' إعداد الخطوط
        Dim fontTitle As New Font("Segoe UI", 12, FontStyle.Bold)
        Dim fontSmallBold As New Font("Segoe UI", 8, FontStyle.Bold)
        Dim fontBody As New Font("Segoe UI", 9, FontStyle.Regular)
        Dim fontBodyBold As New Font("Segoe UI", 9, FontStyle.Bold)
        Dim fontPayment As New Font("Segoe UI", 8, FontStyle.Regular)

        ' 🌟 خطوط مخصصة للجدول (أصغر) 🌟
        Dim fontItem As New Font("Segoe UI", 8, FontStyle.Regular)
        Dim fontItemBold As New Font("Segoe UI", 8, FontStyle.Bold)

        Dim fmtCenter As New StringFormat() With {.Alignment = StringAlignment.Center}
        Dim fmtArabic As New StringFormat() With {
            .Alignment = StringAlignment.Near,
            .FormatFlags = StringFormatFlags.DirectionRightToLeft
        }

        ' 1. اللوجو والاسم 
        Dim logoImg As Image = Print_LogoImage
        If logoImg IsNot Nothing Then
            g.DrawImage(logoImg, 5, Print_Y, 50, 50)
            g.DrawString(Print_CompName, fontTitle, Brushes.Black, New Rectangle(60, Print_Y + 4, 220, 28), fmtArabic)
            If String.IsNullOrWhiteSpace(Print_EngName) = False Then
                g.DrawString(Print_EngName, fontBodyBold, Brushes.Black, New Rectangle(60, Print_Y + 31, 220, 22), fmtArabic)
            End If
            Print_Y += 65
        Else
            g.DrawString(Print_CompName, fontTitle, Brushes.Black, New Rectangle(5, Print_Y, PaperWidth, 28), fmtCenter)
            Print_Y += 28
            If String.IsNullOrWhiteSpace(Print_EngName) = False Then
                g.DrawString(Print_EngName, fontBodyBold, Brushes.Black, New Rectangle(5, Print_Y, PaperWidth, 22), fmtCenter)
                Print_Y += 22
            Else
                Print_Y += 7
            End If
        End If

        ' 2. تفاصيل الفاتورة
        DrawThreeParts(g, "Invoice", "", "فاتورة مبيعات", Print_Y, fontBodyBold)
        Print_Y += 20
        DrawThreeParts(g, "Inv. No", Bill_ID_Txt.Text, "رقم الفاتورة", Print_Y, fontBodyBold)
        Print_Y += 18
        DrawThreeParts(g, "Date", DateTimeEx.Text, "تاريخ الفاتورة", Print_Y, fontBodyBold)
        If String.IsNullOrWhiteSpace(AG_SH_txt.Text) = False Then
            Print_Y += 18
            DrawThreeParts(g, "Customer", AG_SH_txt.Text.Trim(), "العميل", Print_Y, fontBodyBold)
        End If
        Print_Y += 25

        DrawDashedLine(g, Print_Y, PaperWidth)
        Print_Y += 10

        ' 3. عناوين الجدول 
        g.DrawString("ت", fontSmallBold, Brushes.Black, New Rectangle(260, Print_Y, 20, 30), fmtCenter)
        g.DrawString("Item" & vbCrLf & "الصنف", fontSmallBold, Brushes.Black, New Rectangle(150, Print_Y, 110, 30), fmtArabic)
        g.DrawString("Qty" & vbCrLf & "كمية", fontSmallBold, Brushes.Black, New Rectangle(115, Print_Y, 35, 30), fmtCenter)
        g.DrawString("Price" & vbCrLf & "السعر", fontSmallBold, Brushes.Black, New Rectangle(65, Print_Y, 50, 30), fmtCenter)
        g.DrawString("Total" & vbCrLf & "الإجمالي", fontSmallBold, Brushes.Black, New Rectangle(5, Print_Y, 60, 30), fmtCenter)
        Print_Y += 30 ' 🌟 نقصنا النقلة شوية باش يقرب من الجدول 🌟

        DrawDashedLine(g, Print_Y, PaperWidth)
        Print_Y += 6

        ' 4. محتويات الجدول (مضغوطة)
        Dim rowCounter As Integer = 1
        For Each row As DataGridViewRow In dgvSales.Rows
            If row.IsNewRow Then Continue For
            Dim itemName As String = row.Cells("Item_Name").Value.ToString()
            Dim qty As String = row.Cells("QTY_CL").Value.ToString()
            Dim price As String = Convert.ToDouble(row.Cells("Price_CL").Value).ToString("N2")
            Dim total As String = Convert.ToDouble(row.Cells("Total_CL").Value).ToString("N2")

            ' 🌟 استخدمنا الخط الأصغر في القياس، ونقصنا الارتفاع الأدنى لـ 16 بكسل بدل 20 🌟
            Dim itemSizeF As SizeF = g.MeasureString(itemName, fontItem, 110, fmtArabic)
            Dim rowHeight As Integer = Math.Max(16, CInt(itemSizeF.Height) + 2)

            ' الرسم بالخطوط الصغيرة الجديدة
            g.DrawString(rowCounter.ToString(), fontItem, Brushes.Black, New Rectangle(260, Print_Y, 20, rowHeight), fmtCenter)
            g.DrawString(itemName, fontItem, Brushes.Black, New Rectangle(150, Print_Y, 110, rowHeight), fmtArabic)
            g.DrawString(qty, fontItem, Brushes.Black, New Rectangle(115, Print_Y, 35, rowHeight), fmtCenter)
            g.DrawString(price, fontItem, Brushes.Black, New Rectangle(65, Print_Y, 50, rowHeight), fmtCenter)
            g.DrawString(total, fontItemBold, Brushes.Black, New Rectangle(5, Print_Y, 60, rowHeight), fmtCenter)

            Print_Y += rowHeight
            rowCounter += 1
        Next

        Print_Y += 4
        DrawDashedLine(g, Print_Y, PaperWidth)
        Print_Y += 10

        ' 5. الإجماليات
        DrawThreeParts(g, "Gross Total", Total_TextBox.Text, "الإجمالي", Print_Y, fontBodyBold)
        Print_Y += 22
        If Val(Discount_txt.Text) > 0 Then
            DrawThreeParts(g, "Discount", Discount_txt.Text, "الخصم", Print_Y, fontBody)
            Print_Y += 20
        End If
        DrawThreeParts(g, "Net Total", Pure_txt.Text, "الصافي", Print_Y, fontTitle)
        Print_Y += 24
        If Print_PaymentLines.Count > 0 Then
            Dim paidTotal As Decimal = 0D

            DrawDashedLine(g, Print_Y, PaperWidth)
            Print_Y += 8

            For Each payment As SalesPrintPaymentLine In Print_PaymentLines
                DrawThreeParts(g, "Payment", payment.Amount.ToString(N_Point_Fter), payment.PaymentName, Print_Y, fontPayment)
                Print_Y += 22
                paidTotal += payment.Amount
            Next

            DrawDashedLine(g, Print_Y, PaperWidth)
            Print_Y += 8

            Dim pureValue As Decimal = 0D
            Decimal.TryParse(Pure_txt.Text, pureValue)

            DrawThreeParts(g, "Paid", paidTotal.ToString(N_Point_Fter), "المدفوع", Print_Y, fontBodyBold)
            Print_Y += 22
            DrawThreeParts(g, "Remaining", (pureValue - paidTotal).ToString(N_Point_Fter), "المتبقي", Print_Y, fontBodyBold)
            Print_Y += 24
        ElseIf String.IsNullOrWhiteSpace(Print_PaymentName) = False Then
            DrawThreeParts(g, "Payment", Print_PaymentName, "طريقة الدفع", Print_Y, fontBodyBold)
            Print_Y += 24
        Else
            Print_Y += 16
        End If

        DrawDashedLine(g, Print_Y, PaperWidth)
        Print_Y += 12

        ' 6. الفوتر
        DrawThreeParts(g, "Cashier", USER_NAME, "الكاشير", Print_Y, fontBody)
        Print_Y += 30

        g.DrawString("طُبعت في: " & Now.ToString("yyyy-MM-dd HH:mm:ss"), fontSmallBold, Brushes.Black, New Rectangle(5, Print_Y, PaperWidth, 15), fmtCenter)
        Print_Y += 25

        DrawReceiptBillNotes(g, Print_BillNotes, fontBodyBold, Print_Y, PaperWidth)

        e.HasMorePages = False
    End Sub

    Private Sub LoadPrintPaymentLines()
        Print_PaymentLines.Clear()
        If T_ID <= 0 Then Exit Sub

        Try
            Const sql As String =
                "SELECT ISNULL(PAYMENT_NAME, N'') AS PAYMENT_NAME, " &
                "SUM(ISNULL(Value, 0)) AS PaymentValue " &
                "FROM SB_Receipts_V " &
                "WHERE Receipt_Tran_ID = @T_ID AND isVoid = 0 " &
                "GROUP BY PAYMENT_NAME " &
                "ORDER BY PAYMENT_NAME"

            Using connection As New SqlConnection(MY_Settings.SqlConStr)
                Using command As New SqlCommand(sql, connection)
                    command.Parameters.Add("@T_ID", SqlDbType.Int).Value = T_ID
                    connection.Open()

                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            Dim paymentName As String = Convert.ToString(reader("PAYMENT_NAME")).Trim()
                            If String.IsNullOrWhiteSpace(paymentName) Then paymentName = "غير محدد"

                            Dim amount As Decimal = 0D
                            If reader("PaymentValue") IsNot DBNull.Value Then
                                amount = Convert.ToDecimal(reader("PaymentValue"))
                            End If

                            Print_PaymentLines.Add(New SalesPrintPaymentLine With {
                                .PaymentName = paymentName,
                                .Amount = amount
                            })
                        End While
                    End Using
                End Using
            End Using
        Catch
            ' يبقى اسم طريقة الدفع القديم متاحاً إذا تعذر تحميل السندات.
        End Try
    End Sub

    Private Function EstimateReceiptBillNotesHeight(notesText As String) As Integer
        If String.IsNullOrWhiteSpace(notesText) Then Return 0

        Dim normalizedText As String = notesText.Replace(vbCrLf, vbLf).Replace(vbCr, vbLf)
        Dim estimatedLines As Integer = 0

        For Each line As String In normalizedText.Split(ControlChars.Lf)
            estimatedLines += Math.Max(1, CInt(Math.Ceiling(Math.Max(1, line.Length) / 32.0R)))
        Next

        Return Math.Max(40, (estimatedLines * 18) + 10)
    End Function

    Private Sub DrawReceiptBillNotes(g As Graphics, notesText As String, font As Font, ByRef y As Integer, paperWidth As Integer)
        If String.IsNullOrWhiteSpace(notesText) Then Exit Sub

        Using notesFormat As New StringFormat() With {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Near,
            .Trimming = StringTrimming.Word
        }
            Dim rectWidth As Integer = Math.Max(20, paperWidth - 10)
            Dim textSize As SizeF = g.MeasureString(notesText, font, rectWidth, notesFormat)
            Dim notesHeight As Integer = Math.Max(40, CInt(Math.Ceiling(textSize.Height)) + 8)
            g.DrawString(notesText, font, Brushes.Black, New Rectangle(5, y, rectWidth, notesHeight), notesFormat)
            y += notesHeight
        End Using
    End Sub

    ' دالة توزيع النص (مضغوطة جداً لتناسب 265 بكسل)
    Private Sub DrawThreeParts(g As Graphics, engText As String, value As String, araText As String, y As Integer, font As Font)
        Dim fLeft As New StringFormat() With {.Alignment = StringAlignment.Near}
        Dim fCenter As New StringFormat() With {.Alignment = StringAlignment.Center}
        Dim fRight As New StringFormat() With {.Alignment = StringAlignment.Far}

        ' يسار(80) | وسط(95) | يمين(85) = المجموع 260 بكسل
        g.DrawString(engText, font, Brushes.Black, New Rectangle(5, y, 80, 25), fLeft)
        g.DrawString(value, font, Brushes.Black, New Rectangle(85, y, 95, 25), fCenter)
        g.DrawString(araText, font, Brushes.Black, New Rectangle(180, y, 85, 25), fRight)
    End Sub

    Private Sub DrawDashedLine(g As Graphics, y As Integer, width As Integer)
        Dim p As New Pen(Color.Black, 1)
        p.DashStyle = Drawing2D.DashStyle.Dash
        g.DrawLine(p, 5, y, width, y)
    End Sub
    ' دالة توليد الباركود (ضع كود المكتبة الخاصة بك هنا)
    Private Function GenerateBarcode(text As String) As Image
        ' مثال: إذا كنت تستخدم ZXing
        ' Dim writer As New ZXing.BarcodeWriter()
        ' writer.Format = ZXing.BarcodeFormat.CODE_128
        ' Return writer.Write(text)

        Return Nothing ' اتركه Nothing إذا لم تكن تمتلك مكتبة حالياً
    End Function

    ' دالة توليد الـ QR Code (ضع كود المكتبة الخاصة بك هنا)
    Private Function GenerateQRCode(text As String) As Image
        ' مثال: إذا كنت تستخدم QRCoder
        ' Dim qrGenerator As New QRCoder.QRCodeGenerator()
        ' Dim qrData As QRCoder.QRCodeData = qrGenerator.CreateQrCode(text, QRCoder.QRCodeGenerator.ECCLevel.Q)
        ' Dim qrCode As New QRCoder.QRCode(qrData)
        ' Return qrCode.GetGraphic(5)

        Return Nothing ' اتركه Nothing إذا لم تكن تمتلك مكتبة حالياً
    End Function

    '--------------------------------------------------------------------------------------------------------------
    Private Sub Expenses_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        AddDraftLog("إغلاق", "تم إغلاق شاشة المبيعات المسودة", "Sales_Fast_Draft")
        PushDraftLogsToDatabase(True)
        FormType = 0
        'F_MainForm.Fill_ALL_IM()
        'If AGMetroGrid.Rows.Count = 0 And isDepended = False Then Delete_Last_Empty_Bill(T_ID)
        If Print_LogoImage IsNot Nothing Then
            Print_LogoImage.Dispose()
            Print_LogoImage = Nothing
        End If
        Me.Dispose()
    End Sub


    Public DraftManager As New DraftSalesManager()
    Private CurrentDraft As SaleDraftHeader
    Private ItemsCache As New List(Of CachedSaleItem)
    Dim C As New C


    Public Sub OpenDraft(draftId As String)
        SetScreenStatus("جاري فتح المسودة...", ScreenStatusType.Loading, draftId)
        Dim draft As SaleDraftHeader = DraftManager.LoadDraft(draftId)

        If draft Is Nothing Then
            SetScreenStatus("تعذر فتح المسودة المحددة", ScreenStatusType.Error, draftId)
            MessageBox.Show("تعذر فتح المسودة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        CurrentDraft = draft

        BindDraftHeaderToForm()
        LoadDraftToGrid()
        UpdateDraftTotalsOnScreen()
        AddDraftLog("فتح", "تم فتح مسودة مبيعات", "OpenDraft", "", draftId)

    End Sub

    Private Sub BindDraftHeaderToForm()

        If CurrentDraft Is Nothing Then Exit Sub

        ' العميل
        AG_SH_txt.Text = CurrentDraft.CustomerName
        AG_ID = CurrentDraft.AG_ID.ToString()

        ' التاريخ
        DateTimeEx.Value = CurrentDraft.Date

        ' الملاحظات
        txtNotes.Text = CurrentDraft.About
        LastLoggedNotesText = txtNotes.Text

        ' الخصم
        Discount_txt.Text = CurrentDraft.Discount.ToString("0.000")

        ' الطاولة إن وجدت
        'If CurrentDraft.Table_ID.HasValue Then
        '    txtTableId.Text = CurrentDraft.Table_ID.Value.ToString()
        'Else
        '    txtTableId.Text = ""
        'End If

        ' اسم المسودة أو عنوانها
        'lblDraftName.Text = CurrentDraft.DraftName

    End Sub

    Private Sub UpdateDraftTotalsOnScreen()

        UpdateDraftButtonIndicator()

        If CurrentDraft Is Nothing Then Exit Sub

        Total_TextBox.Text = CurrentDraft.Total.ToString("0.000")
        Discount_txt.Text = CurrentDraft.Discount.ToString("0.000")
        Pure_txt.Text = CurrentDraft.Pure.ToString("0.000")
        IM_Count_LB.Text = " المواد: " & CurrentDraft.Items.Count.ToString()

    End Sub

    Private Sub CaptureDraftButtonDefaultStyle()

        If IsDraftButtonDefaultStyleCaptured = True Then Exit Sub

        DraftButtonDefaultText = Draft_Btn.Text
        DraftButtonDefaultBackColor = Draft_Btn.BackColor
        DraftButtonDefaultForeColor = Draft_Btn.ForeColor
        DraftButtonDefaultBorderColor = Draft_Btn.FlatAppearance.BorderColor
        DraftButtonDefaultFont = Draft_Btn.Font
        IsDraftButtonDefaultStyleCaptured = True

    End Sub

    Private Function CountOpenDraftsWithTotal() As Integer

        Dim draftsCount As Integer = 0

        For Each d As SaleDraftHeader In DraftManager.GetAllDrafts()
            If d IsNot Nothing AndAlso d.Total > 0D Then draftsCount += 1
        Next

        Return draftsCount

    End Function

    Private Sub UpdateDraftButtonIndicator()

        CaptureDraftButtonDefaultStyle()

        Dim draftsCount As Integer = CountOpenDraftsWithTotal()

        If draftsCount > 0 Then
            Draft_Btn.Text = "📝 مسودات" & Environment.NewLine & draftsCount.ToString("N0")
            Draft_Btn.BackColor = Color.FromArgb(255, 193, 7)
            Draft_Btn.ForeColor = Color.FromArgb(83, 53, 10)
            Draft_Btn.FlatAppearance.BorderColor = Color.FromArgb(180, 83, 9)
            Draft_Btn.Font = DraftButtonDefaultFont
            MetroToolTip1.SetToolTip(Draft_Btn, "لديك عدد " & draftsCount.ToString("N0") & " من الفواتير بالمسودة لم ترحل")
        Else
            Draft_Btn.Text = DraftButtonDefaultText
            Draft_Btn.BackColor = DraftButtonDefaultBackColor
            Draft_Btn.ForeColor = DraftButtonDefaultForeColor
            Draft_Btn.FlatAppearance.BorderColor = DraftButtonDefaultBorderColor
            Draft_Btn.Font = DraftButtonDefaultFont
            MetroToolTip1.SetToolTip(Draft_Btn, "استعراض مسودات فواتير المبيعات")
        End If

    End Sub

    Private Sub ApplyTopButtonsStyle()

        StyleTopButton(New_butt,
                       " ➕ " & "جديد F1",
                       Color.FromArgb(22, 163, 74),
                       Color.White,
                       Color.FromArgb(21, 128, 61),
                       "إنشاء فاتورة جديدة")

        StyleTopButton(Save_butt,
                       " 💾 " & "حفظ F12",
                       Color.FromArgb(37, 99, 235),
                       Color.White,
                       Color.FromArgb(29, 78, 216),
                       "حفظ وترحيل الفاتورة")

        StyleTopButton(Print_btn,
                       " 🖨 " & "طباعة F2",
                       Color.FromArgb(71, 85, 105),
                       Color.White,
                       Color.FromArgb(51, 65, 85),
                       "طباعة الفاتورة الحالية")

        StyleTopButton(PreviousBillsButton,
                       " 📋 الفواتير" & " السابقة ",
                       Color.FromArgb(79, 70, 229),
                       Color.White,
                       Color.FromArgb(67, 56, 202),
                       "مراجعة الفواتير السابقة")

        'StyleTopButton(Refresh_IM_Btn,
        '               RefreshButtonDefaultText,
        '               RefreshButtonDefaultBackColor,
        '               Color.White,
        '               Color.FromArgb(21, 94, 117),
        '               "تحديث قائمة الأصناف والوحدات")

        StyleTopButton(Draft_Btn,
                       "📝" & Environment.NewLine & "المسودة",
                       Color.FromArgb(245, 158, 11),
                       Color.FromArgb(69, 26, 3),
                       Color.FromArgb(180, 83, 9),
                       "استعراض مسودات فواتير المبيعات")

        'StyleTopButton(SBPauseBtn,
        '               "⏸" & Environment.NewLine & "تعليق F7",
        '               Color.FromArgb(217, 119, 6),
        '               Color.White,
        '               Color.FromArgb(180, 83, 9),
        '               "تعليق الفاتورة الحالية")

        'StyleIconButton(DGV_Control_btn,
        '                "⚙",
        '                Color.FromArgb(15, 23, 42),
        '                Color.White,
        '                "عرض بيانات الجدول")

        'StyleIconButton(Show_Cash_btn,
        '                "💵",
        '                Color.FromArgb(5, 150, 105),
        '                Color.White,
        '                "عرض المقبوض")

        'StyleIconButton(OpenCahDR_Btn,
        '                "▣",
        '                Color.FromArgb(180, 83, 9),
        '                Color.White,
        '                "فتح صندوق النقود")

        'StyleIconButton(CALC_Btn,
        '                "∑",
        '                Color.FromArgb(30, 64, 175),
        '                Color.White,
        '                "فتح الآلة الحاسبة")

        'StyleIconButton(Down_Bill_btn,
        '                "▼",
        '                Color.FromArgb(100, 116, 139),
        '                Color.White,
        '                "الفاتورة السابقة")

        'StyleIconButton(Up_Bill_btn,
        '                "▲",
        '                Color.FromArgb(100, 116, 139),
        '                Color.White,
        '                "الفاتورة التالية")

        IsDraftButtonDefaultStyleCaptured = False

    End Sub

    Private Sub StyleTopButton(targetButton As Button,
                               buttonText As String,
                               backColor As Color,
                               foreColor As Color,
                               borderColor As Color,
                               toolTipText As String)

        targetButton.Text = buttonText
        targetButton.BackColor = backColor
        targetButton.ForeColor = foreColor
        targetButton.FlatStyle = FlatStyle.Flat
        targetButton.FlatAppearance.BorderSize = 1
        targetButton.FlatAppearance.BorderColor = borderColor
        targetButton.FlatAppearance.MouseOverBackColor = ControlPaint.Light(backColor)
        targetButton.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(backColor)
        targetButton.Font = New Font("Segoe UI Semibold", 10.5!, FontStyle.Bold)
        targetButton.TextAlign = ContentAlignment.MiddleCenter
        targetButton.ImageAlign = ContentAlignment.MiddleCenter
        targetButton.RightToLeft = Windows.Forms.RightToLeft.Yes
        targetButton.UseVisualStyleBackColor = False
        targetButton.Padding = New Padding(1, 0, 1, 0)
        MetroToolTip1.SetToolTip(targetButton, toolTipText)

    End Sub

    Private Sub StyleIconButton(targetButton As Button,
                                buttonText As String,
                                backColor As Color,
                                foreColor As Color,
                                toolTipText As String)

        targetButton.Text = buttonText
        targetButton.BackColor = backColor
        targetButton.ForeColor = foreColor
        targetButton.FlatStyle = FlatStyle.Flat
        targetButton.FlatAppearance.BorderSize = 1
        targetButton.FlatAppearance.BorderColor = ControlPaint.Dark(backColor)
        targetButton.FlatAppearance.MouseOverBackColor = ControlPaint.Light(backColor)
        targetButton.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(backColor)
        targetButton.Font = New Font("Segoe UI Semibold", 13.0!, FontStyle.Bold)
        targetButton.TextAlign = ContentAlignment.MiddleCenter
        targetButton.ImageAlign = ContentAlignment.MiddleCenter
        targetButton.RightToLeft = Windows.Forms.RightToLeft.Yes
        targetButton.UseVisualStyleBackColor = False
        targetButton.Padding = New Padding(0)
        MetroToolTip1.SetToolTip(targetButton, toolTipText)

    End Sub


    Private Sub InitializeDraftLogs()

        If DraftLogsDt Is Nothing Then
            DraftLogsDt = New DataTable()
            DraftLogsDt.Columns.Add("LogId", GetType(Integer))
            DraftLogsDt.Columns.Add("DraftId", GetType(String))
            DraftLogsDt.Columns.Add("User_ID", GetType(Integer))
            DraftLogsDt.Columns.Add("UserName", GetType(String))
            DraftLogsDt.Columns.Add("MachineName", GetType(String))
            DraftLogsDt.Columns.Add("ActionDateTime", GetType(DateTime))
            DraftLogsDt.Columns.Add("ScreenName", GetType(String))
            DraftLogsDt.Columns.Add("ActionType", GetType(String))
            DraftLogsDt.Columns.Add("ActionDescription", GetType(String))
            DraftLogsDt.Columns.Add("ControlName", GetType(String))
            DraftLogsDt.Columns.Add("OldValue", GetType(String))
            DraftLogsDt.Columns.Add("NewValue", GetType(String))
            DraftLogsDt.Columns.Add("ItemName", GetType(String))
            DraftLogsDt.Columns.Add("IM_ID", GetType(Integer))
            DraftLogsDt.Columns.Add("Qty", GetType(Decimal))
            DraftLogsDt.Columns.Add("Total", GetType(Decimal))
            DraftLogsDt.Columns.Add("Final_T_ID", GetType(Integer))
            DraftLogsDt.Columns.Add("Final_SB_ID", GetType(Integer))
            DraftLogsDt.Columns.Add("IsSynced", GetType(Boolean))
            LoadDraftLogsQueue()
        End If

        If DraftLogsBindingSource Is Nothing Then
            DraftLogsBindingSource = New BindingSource()
            DraftLogsBindingSource.DataSource = DraftLogsDt
        End If

        If DraftLogsToggleButton IsNot Nothing Then
            RemoveHandler DraftLogsToggleButton.Click, AddressOf DraftLogsToggleButton_Click
            Me.Controls.Remove(DraftLogsToggleButton)
            DraftLogsToggleButton.Dispose()
            DraftLogsToggleButton = Nothing
        End If

        If DraftLogsPanel Is Nothing Then
            DraftLogsPanel = New Panel()
            DraftLogsPanel.Name = "DraftLogsPanel"
            DraftLogsPanel.BackColor = Color.White
            DraftLogsPanel.BorderStyle = BorderStyle.FixedSingle
            DraftLogsPanel.Visible = False
            DraftLogsPanel.RightToLeft = Windows.Forms.RightToLeft.Yes

            Dim headerPanel As New Panel()
            headerPanel.Name = "DraftLogsHeaderPanel"
            headerPanel.Dock = DockStyle.Top
            headerPanel.Height = 38
            headerPanel.BackColor = Color.FromArgb(15, 23, 42)

            Dim titleLabel As New Label()
            titleLabel.Text = "سجل حركات شاشة المبيعات المسودة"
            titleLabel.Font = New Font("Segoe UI Semibold", 10.0!, FontStyle.Bold)
            titleLabel.ForeColor = Color.White
            titleLabel.Dock = DockStyle.Fill
            titleLabel.TextAlign = ContentAlignment.MiddleRight
            titleLabel.Padding = New Padding(0, 0, 12, 0)

            DraftLogsCountLabel = New Label()
            DraftLogsCountLabel.Font = New Font("Segoe UI Semibold", 9.0!, FontStyle.Bold)
            DraftLogsCountLabel.ForeColor = Color.FromArgb(226, 232, 240)
            DraftLogsCountLabel.TextAlign = ContentAlignment.MiddleCenter
            DraftLogsCountLabel.Dock = DockStyle.Left
            DraftLogsCountLabel.Width = 120

            Dim closeButton As New Button()
            closeButton.Name = "DraftLogsCloseButton"
            closeButton.Text = "×"
            closeButton.Font = New Font("Segoe UI", 13.0!, FontStyle.Bold)
            closeButton.Dock = DockStyle.Left
            closeButton.Width = 38
            closeButton.FlatStyle = FlatStyle.Flat
            closeButton.FlatAppearance.BorderSize = 0
            closeButton.BackColor = Color.FromArgb(185, 28, 28)
            closeButton.ForeColor = Color.White
            closeButton.Cursor = Cursors.Hand
            closeButton.UseVisualStyleBackColor = False
            AddHandler closeButton.Click,
                Sub()
                    DraftLogsPanel.Visible = False
                End Sub

            headerPanel.Controls.Add(closeButton)
            headerPanel.Controls.Add(DraftLogsCountLabel)
            headerPanel.Controls.Add(titleLabel)

            DraftLogsGrid = New DataGridView()
            DraftLogsGrid.Name = "DraftLogsGrid"
            DraftLogsGrid.Dock = DockStyle.Fill
            DraftLogsGrid.DataSource = DraftLogsBindingSource
            DraftLogsGrid.AllowUserToAddRows = False
            DraftLogsGrid.AllowUserToDeleteRows = False
            DraftLogsGrid.ReadOnly = True
            DraftLogsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DraftLogsGrid.MultiSelect = False
            DraftLogsGrid.RowHeadersVisible = False
            DraftLogsGrid.BackgroundColor = Color.White
            DraftLogsGrid.BorderStyle = BorderStyle.None
            DraftLogsGrid.EnableHeadersVisualStyles = False
            DraftLogsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59)
            DraftLogsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            DraftLogsGrid.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.0!, FontStyle.Bold)
            DraftLogsGrid.DefaultCellStyle.Font = New Font("Segoe UI Semibold", 8.75!, FontStyle.Bold)
            DraftLogsGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252)
            DraftLogsGrid.RightToLeft = Windows.Forms.RightToLeft.Yes
            AddHandler DraftLogsGrid.CellFormatting, AddressOf DraftLogsGrid_CellFormatting

            DraftLogsPanel.Controls.Add(DraftLogsGrid)
            DraftLogsPanel.Controls.Add(headerPanel)
            Me.Controls.Add(DraftLogsPanel)

            ConfigureDraftLogsGrid()
            LayoutDraftLogsPanel()
            DraftLogsPanel.BringToFront()
        End If

        UpdateDraftLogsCount()

    End Sub

    Private Sub ConfigureDraftLogsGrid()

        If DraftLogsGrid Is Nothing Then Exit Sub

        Dim headers As New Dictionary(Of String, String) From {
            {"LogId", "م"},
            {"DraftId", "المسودة"},
            {"User_ID", "رقم المستخدم"},
            {"UserName", "المستخدم"},
            {"MachineName", "الجهاز"},
            {"ActionDateTime", "الوقت والتاريخ"},
            {"ScreenName", "الشاشة"},
            {"ActionType", "نوع الحركة"},
            {"ActionDescription", "الوصف"},
            {"ControlName", "الأداة"},
            {"OldValue", "القيمة السابقة"},
            {"NewValue", "القيمة الجديدة"},
            {"ItemName", "الصنف"},
            {"IM_ID", "رقم الصنف"},
            {"Qty", "الكمية"},
            {"Total", "الإجمالي"},
            {"Final_T_ID", "رقم الحركة"},
            {"Final_SB_ID", "رقم الفاتورة"},
            {"IsSynced", "مرحل"}
        }

        For Each column As DataGridViewColumn In DraftLogsGrid.Columns
            If headers.ContainsKey(column.Name) Then column.HeaderText = headers(column.Name)
        Next

        If DraftLogsGrid.Columns.Contains("LogId") Then DraftLogsGrid.Columns("LogId").Width = 45
        If DraftLogsGrid.Columns.Contains("ActionDateTime") Then
            DraftLogsGrid.Columns("ActionDateTime").Width = 145
            DraftLogsGrid.Columns("ActionDateTime").DefaultCellStyle.Format = "yyyy/MM/dd HH:mm:ss"
        End If
        If DraftLogsGrid.Columns.Contains("ActionType") Then DraftLogsGrid.Columns("ActionType").Width = 105
        If DraftLogsGrid.Columns.Contains("ActionDescription") Then DraftLogsGrid.Columns("ActionDescription").Width = 280
        If DraftLogsGrid.Columns.Contains("UserName") Then DraftLogsGrid.Columns("UserName").Width = 120
        If DraftLogsGrid.Columns.Contains("MachineName") Then DraftLogsGrid.Columns("MachineName").Width = 110
        If DraftLogsGrid.Columns.Contains("ItemName") Then DraftLogsGrid.Columns("ItemName").Width = 145
        If DraftLogsGrid.Columns.Contains("OldValue") Then DraftLogsGrid.Columns("OldValue").Width = 120
        If DraftLogsGrid.Columns.Contains("NewValue") Then DraftLogsGrid.Columns("NewValue").Width = 120
        If DraftLogsGrid.Columns.Contains("Final_SB_ID") Then DraftLogsGrid.Columns("Final_SB_ID").Width = 90

        If DraftLogsGrid.Columns.Contains("DraftId") Then DraftLogsGrid.Columns("DraftId").Visible = False
        If DraftLogsGrid.Columns.Contains("User_ID") Then DraftLogsGrid.Columns("User_ID").Visible = False
        If DraftLogsGrid.Columns.Contains("ScreenName") Then DraftLogsGrid.Columns("ScreenName").Visible = False
        If DraftLogsGrid.Columns.Contains("ControlName") Then DraftLogsGrid.Columns("ControlName").Visible = False
        If DraftLogsGrid.Columns.Contains("IM_ID") Then DraftLogsGrid.Columns("IM_ID").Visible = False
        If DraftLogsGrid.Columns.Contains("Final_T_ID") Then DraftLogsGrid.Columns("Final_T_ID").Visible = False
        If DraftLogsGrid.Columns.Contains("IsSynced") Then DraftLogsGrid.Columns("IsSynced").Visible = False

    End Sub

    Private Sub LayoutDraftLogsPanel()

        If DraftLogsPanel Is Nothing Then Exit Sub

        Dim panelWidth As Integer = Math.Min(Me.ClientSize.Width - 24, 960)
        If panelWidth < 640 Then panelWidth = Math.Max(320, Me.ClientSize.Width - 24)

        Dim panelHeight As Integer = 260
        If Me.ClientSize.Height < 620 Then panelHeight = 220

        DraftLogsPanel.Size = New Size(panelWidth, panelHeight)
        DraftLogsPanel.Location = New Point(12, Math.Max(80, Me.ClientSize.Height - panelHeight - 46))

        If DraftLogsToggleButton IsNot Nothing Then DraftLogsToggleButton.BringToFront()
        If DraftLogsPanel.Visible Then DraftLogsPanel.BringToFront()

    End Sub

    Private Sub DraftLogsToggleButton_Click(sender As Object, e As EventArgs)

        If DraftLogsPanel Is Nothing Then Exit Sub

        DraftLogsPanel.Visible = Not DraftLogsPanel.Visible

        If DraftLogsPanel.Visible Then
            LayoutDraftLogsPanel()
            DraftLogsPanel.BringToFront()
        End If

    End Sub

    Private Sub DraftLogsGrid_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)

        If DraftLogsGrid Is Nothing OrElse e.RowIndex < 0 Then Exit Sub
        If Not DraftLogsGrid.Columns.Contains("ActionType") Then Exit Sub

        Dim actionType As String = Convert.ToString(DraftLogsGrid.Rows(e.RowIndex).Cells("ActionType").Value)
        Dim rowBackColor As Color = Color.White
        Dim rowForeColor As Color = Color.FromArgb(15, 23, 42)

        Select Case actionType
            Case "إضافة"
                rowBackColor = Color.FromArgb(220, 252, 231)
                rowForeColor = Color.FromArgb(22, 101, 52)
            Case "حذف"
                rowBackColor = Color.FromArgb(254, 226, 226)
                rowForeColor = Color.FromArgb(153, 27, 27)
            Case "تعديل", "كمية", "وحدة", "خصم", "عميل"
                rowBackColor = Color.FromArgb(219, 234, 254)
                rowForeColor = Color.FromArgb(30, 64, 175)
            Case "حفظ", "طباعة"
                rowBackColor = Color.FromArgb(254, 249, 195)
                rowForeColor = Color.FromArgb(133, 77, 14)
            Case "فتح", "زر"
                rowBackColor = Color.FromArgb(241, 245, 249)
                rowForeColor = Color.FromArgb(51, 65, 85)
            Case "إغلاق"
                rowBackColor = Color.FromArgb(243, 232, 255)
                rowForeColor = Color.FromArgb(88, 28, 135)
        End Select

        Dim selectionBackColor As Color = ControlPaint.Dark(rowBackColor)

        DraftLogsGrid.Rows(e.RowIndex).DefaultCellStyle.BackColor = rowBackColor
        DraftLogsGrid.Rows(e.RowIndex).DefaultCellStyle.ForeColor = rowForeColor
        DraftLogsGrid.Rows(e.RowIndex).DefaultCellStyle.SelectionBackColor = selectionBackColor
        DraftLogsGrid.Rows(e.RowIndex).DefaultCellStyle.SelectionForeColor = GetReadableDraftLogSelectionForeColor(selectionBackColor)

    End Sub

    Private Function GetReadableDraftLogSelectionForeColor(backColor As Color) As Color
        Dim brightness As Double = (backColor.R * 0.299) + (backColor.G * 0.587) + (backColor.B * 0.114)

        If brightness < 145 Then Return Color.White

        Return Color.FromArgb(15, 23, 42)
    End Function

    Private Sub AddDraftLog(actionType As String,
                            actionDescription As String,
                            Optional controlName As String = "",
                            Optional oldValue As String = "",
                            Optional newValue As String = "",
                            Optional itemName As String = "",
                            Optional itemId As Integer = 0,
                            Optional qty As Decimal = 0D,
                            Optional total As Decimal = 0D)

        If DraftLogsDt Is Nothing Then InitializeDraftLogs()

        DraftLogsCounter += 1

        Dim draftId As String = ""
        If CurrentDraft IsNot Nothing AndAlso String.IsNullOrWhiteSpace(CurrentDraft.DraftId) = False Then
            draftId = CurrentDraft.DraftId
        End If

        Dim finalTId As Integer = 0
        Dim finalSbId As Integer = 0

        If CurrentDraft IsNot Nothing Then
            If CurrentDraft.Final_T_ID.HasValue Then finalTId = CurrentDraft.Final_T_ID.Value
            If CurrentDraft.Final_SB_ID.HasValue Then finalSbId = CurrentDraft.Final_SB_ID.Value
        End If

        DraftLogsDt.Rows.Add(
            DraftLogsCounter,
            draftId,
            USER_ID,
            GetCurrentDraftLogUserName(),
            Environment.MachineName,
            DateTime.Now,
            "شاشة المبيعات المسودة",
            actionType,
            actionDescription,
            controlName,
            oldValue,
            newValue,
            itemName,
            itemId,
            qty,
            total,
            finalTId,
            finalSbId,
            False
        )

        UpdateDraftLogsCount()
        PersistDraftLogsQueue()

        SetScreenStatus(actionDescription,
                        GetStatusTypeFromDraftAction(actionType, actionDescription),
                        If(String.IsNullOrWhiteSpace(newValue), oldValue, newValue))

        If DraftLogsGrid IsNot Nothing AndAlso DraftLogsGrid.Rows.Count > 0 Then
            Dim lastRowIndex As Integer = DraftLogsGrid.Rows.Count - 1
            DraftLogsGrid.ClearSelection()
            DraftLogsGrid.Rows(lastRowIndex).Selected = True

            If DraftLogsPanel IsNot Nothing AndAlso DraftLogsPanel.Visible Then
                DraftLogsGrid.FirstDisplayedScrollingRowIndex = lastRowIndex
            End If
        End If

    End Sub

    Private Function GetCurrentDraftLogUserName() As String

        'If User_Name_lb IsNot Nothing AndAlso String.IsNullOrWhiteSpace(User_Name_lb.Text) = False Then
        '    Return User_Name_lb.Text
        'End If

        Return USER_NAME.ToString()

    End Function

    Private Sub UpdateDraftLogsCount()

        If DraftLogsCountLabel IsNot Nothing Then
            DraftLogsCountLabel.Text = "عدد الحركات: " & If(DraftLogsDt Is Nothing, 0, DraftLogsDt.Rows.Count).ToString("N0")
        End If

        If DraftLogsToggleButton IsNot Nothing Then
            DraftLogsToggleButton.Text = "🧾 سجل الحركة" & Environment.NewLine & If(DraftLogsDt Is Nothing, 0, DraftLogsDt.Rows.Count).ToString("N0")
        End If

    End Sub

    Public Function GetDraftLogsTableCopy() As DataTable

        If DraftLogsDt Is Nothing Then InitializeDraftLogs()

        Return DraftLogsDt.Copy()

    End Function

    Private Sub LoadDraftLogsQueue()

        If DraftLogsDt Is Nothing Then Exit Sub

        If LoadDraftLogsQueueFromFile(DraftLogsQueueFilePath & ".tmp") Then Exit Sub
        If LoadDraftLogsQueueFromFile(DraftLogsQueueFilePath) Then Exit Sub
        LoadDraftLogsQueueFromFile(DraftLogsQueueFilePath & ".bak")

    End Sub

    Private Function LoadDraftLogsQueueFromFile(filePath As String) As Boolean

        Try
            If IO.File.Exists(filePath) = False Then Return False

            Dim queueDt As New DataTable("SalesDraftActionLogsQueue")
            queueDt.ReadXml(filePath)
            Dim loadedRowsCount As Integer = 0

            For Each logRow As DataRow In queueDt.Rows
                If GetLogBooleanValue(logRow, "IsSynced") Then Continue For

                Dim logId As Integer = GetLogIntegerValue(logRow, "LogId")
                If logId > DraftLogsCounter Then DraftLogsCounter = logId

                DraftLogsDt.Rows.Add(
                    logId,
                    GetLogStringValue(logRow, "DraftId"),
                    GetLogIntegerValue(logRow, "User_ID"),
                    GetLogStringValue(logRow, "UserName"),
                    GetLogStringValue(logRow, "MachineName"),
                    GetLogDateValue(logRow, "ActionDateTime"),
                    GetLogStringValue(logRow, "ScreenName"),
                    GetLogStringValue(logRow, "ActionType"),
                    GetLogStringValue(logRow, "ActionDescription"),
                    GetLogStringValue(logRow, "ControlName"),
                    GetLogStringValue(logRow, "OldValue"),
                    GetLogStringValue(logRow, "NewValue"),
                    GetLogStringValue(logRow, "ItemName"),
                    GetLogIntegerValue(logRow, "IM_ID"),
                    GetLogDecimalValue(logRow, "Qty"),
                    GetLogDecimalValue(logRow, "Total"),
                    GetLogIntegerValue(logRow, "Final_T_ID"),
                    GetLogIntegerValue(logRow, "Final_SB_ID"),
                    False
                )
                loadedRowsCount += 1
            Next

            If loadedRowsCount > 0 Then HasLoadedPendingDraftLogs = True

            Return loadedRowsCount > 0

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub PersistDraftLogsQueue()

        If DraftLogsDt Is Nothing Then Exit Sub

        Try
            Dim queueDt As DataTable = DraftLogsDt.Clone()
            queueDt.TableName = "SalesDraftActionLogsQueue"

            For Each logRow As DataRow In GetUnsyncedDraftLogRows()
                queueDt.ImportRow(logRow)
            Next

            PersistDraftLogsQueueTable(queueDt)

        Catch ex As Exception
        End Try

    End Sub

    Private Sub PersistDraftLogsQueueTable(queueDt As DataTable)

        Try
            IO.Directory.CreateDirectory(DraftLogsQueueFolder)

            Dim tempFilePath As String = DraftLogsQueueFilePath & ".tmp"
            Dim backupFilePath As String = DraftLogsQueueFilePath & ".bak"

            If queueDt Is Nothing OrElse queueDt.Rows.Count = 0 Then
                If IO.File.Exists(DraftLogsQueueFilePath) Then IO.File.Delete(DraftLogsQueueFilePath)
                If IO.File.Exists(tempFilePath) Then IO.File.Delete(tempFilePath)
                If IO.File.Exists(backupFilePath) Then IO.File.Delete(backupFilePath)
                Exit Sub
            End If

            Using fs As New IO.FileStream(tempFilePath,
                                         IO.FileMode.Create,
                                         IO.FileAccess.Write,
                                         IO.FileShare.None,
                                         4096,
                                         IO.FileOptions.WriteThrough)
                queueDt.WriteXml(fs, XmlWriteMode.WriteSchema)
                fs.Flush(True)
            End Using

            If IO.File.Exists(DraftLogsQueueFilePath) Then
                IO.File.Replace(tempFilePath, DraftLogsQueueFilePath, backupFilePath, True)
            Else
                IO.File.Move(tempFilePath, DraftLogsQueueFilePath)
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Function GetUnsyncedDraftLogRows() As List(Of DataRow)

        Dim rows As New List(Of DataRow)()
        If DraftLogsDt Is Nothing Then Return rows

        For Each logRow As DataRow In DraftLogsDt.Rows
            If logRow.RowState = DataRowState.Deleted Then Continue For
            If GetLogBooleanValue(logRow, "IsSynced") = False Then rows.Add(logRow)
        Next

        Return rows

    End Function

    Private Sub SetCurrentDraftLogsFinalIds()

        If CurrentDraft Is Nothing OrElse DraftLogsDt Is Nothing Then Exit Sub
        If String.IsNullOrWhiteSpace(CurrentDraft.DraftId) Then Exit Sub

        Dim finalTId As Integer = If(CurrentDraft.Final_T_ID.HasValue, CurrentDraft.Final_T_ID.Value, 0)
        Dim finalSbId As Integer = If(CurrentDraft.Final_SB_ID.HasValue, CurrentDraft.Final_SB_ID.Value, 0)

        For Each logRow As DataRow In DraftLogsDt.Rows
            If logRow.RowState = DataRowState.Deleted Then Continue For
            If GetLogBooleanValue(logRow, "IsSynced") Then Continue For
            If GetLogStringValue(logRow, "DraftId").ToString() <> CurrentDraft.DraftId Then Continue For

            logRow("Final_T_ID") = finalTId
            logRow("Final_SB_ID") = finalSbId
        Next

        PersistDraftLogsQueue()

    End Sub

    Private Function BuildDraftLogsSqlTable(logRows As List(Of DataRow)) As DataTable

        Dim sqlLogsDt As New DataTable()
        sqlLogsDt.Columns.Add("DraftLogId", GetType(Integer))
        sqlLogsDt.Columns.Add("DraftId", GetType(String))
        sqlLogsDt.Columns.Add("User_ID", GetType(Integer))
        sqlLogsDt.Columns.Add("UserName", GetType(String))
        sqlLogsDt.Columns.Add("MachineName", GetType(String))
        sqlLogsDt.Columns.Add("ActionDateTime", GetType(DateTime))
        sqlLogsDt.Columns.Add("ScreenName", GetType(String))
        sqlLogsDt.Columns.Add("ActionType", GetType(String))
        sqlLogsDt.Columns.Add("ActionDescription", GetType(String))
        sqlLogsDt.Columns.Add("ControlName", GetType(String))
        sqlLogsDt.Columns.Add("OldValue", GetType(String))
        sqlLogsDt.Columns.Add("NewValue", GetType(String))
        sqlLogsDt.Columns.Add("ItemName", GetType(String))
        sqlLogsDt.Columns.Add("IM_ID", GetType(Integer))
        sqlLogsDt.Columns.Add("Qty", GetType(Decimal))
        sqlLogsDt.Columns.Add("Total", GetType(Decimal))

        If logRows Is Nothing Then Return sqlLogsDt

        For Each logRow As DataRow In logRows
            sqlLogsDt.Rows.Add(
                GetLogIntegerValue(logRow, "LogId"),
                GetLogStringValue(logRow, "DraftId"),
                GetLogIntegerValue(logRow, "User_ID"),
                GetLogStringValue(logRow, "UserName"),
                GetLogStringValue(logRow, "MachineName"),
                GetLogDateValue(logRow, "ActionDateTime"),
                GetLogStringValue(logRow, "ScreenName"),
                GetLogStringValue(logRow, "ActionType"),
                GetLogStringValue(logRow, "ActionDescription"),
                GetLogStringValue(logRow, "ControlName"),
                GetLogStringValue(logRow, "OldValue"),
                GetLogStringValue(logRow, "NewValue"),
                GetLogStringValue(logRow, "ItemName"),
                GetLogIntegerValue(logRow, "IM_ID"),
                GetLogDecimalValue(logRow, "Qty"),
                GetLogDecimalValue(logRow, "Total")
            )
        Next

        Return sqlLogsDt

    End Function

    Private Function GetLogBooleanValue(logRow As DataRow, columnName As String) As Boolean

        If logRow Is Nothing OrElse logRow.Table.Columns.Contains(columnName) = False Then Return False
        If logRow(columnName) Is DBNull.Value Then Return False

        Dim boolValue As Boolean
        If Boolean.TryParse(logRow(columnName).ToString(), boolValue) Then Return boolValue

        Return False

    End Function

    Private Function GetLogStringValue(logRow As DataRow, columnName As String) As Object

        If logRow Is Nothing OrElse logRow.Table.Columns.Contains(columnName) = False Then Return DBNull.Value
        If logRow(columnName) Is DBNull.Value Then Return DBNull.Value

        Dim textValue As String = logRow(columnName).ToString()
        If String.IsNullOrWhiteSpace(textValue) Then Return DBNull.Value

        Return textValue

    End Function

    Private Function GetLogIntegerValue(logRow As DataRow, columnName As String) As Integer

        If logRow Is Nothing OrElse logRow.Table.Columns.Contains(columnName) = False Then Return 0
        If logRow(columnName) Is DBNull.Value Then Return 0

        Dim intValue As Integer
        If Integer.TryParse(logRow(columnName).ToString(), intValue) Then Return intValue

        Return 0

    End Function

    Private Function GetLogDecimalValue(logRow As DataRow, columnName As String) As Decimal

        If logRow Is Nothing OrElse logRow.Table.Columns.Contains(columnName) = False Then Return 0D
        If logRow(columnName) Is DBNull.Value Then Return 0D

        Dim decimalValue As Decimal
        If Decimal.TryParse(logRow(columnName).ToString(), decimalValue) Then Return decimalValue

        Return 0D

    End Function

    Private Function GetLogDateValue(logRow As DataRow, columnName As String) As DateTime

        If logRow Is Nothing OrElse logRow.Table.Columns.Contains(columnName) = False Then Return DateTime.Now
        If logRow(columnName) Is DBNull.Value Then Return DateTime.Now

        Dim dateValue As DateTime
        If DateTime.TryParse(logRow(columnName).ToString(), dateValue) Then Return dateValue

        Return DateTime.Now

    End Function

    Private Sub PushDraftLogsToDatabase(Optional forcePush As Boolean = True)

        If DraftLogsDt Is Nothing OrElse DraftLogsDt.Rows.Count = 0 Then Exit Sub

        Dim pendingRows As List(Of DataRow) = GetUnsyncedDraftLogRows()
        If pendingRows.Count = 0 Then Exit Sub
        If forcePush = False AndAlso pendingRows.Count < DraftLogsPushThreshold Then Exit Sub

        AddDraftLog("حفظ", "بدء ترحيل سجل حركات المسودة إلى قاعدة البيانات", "SalesDraft_ActionLogs_InsertBatch")
        pendingRows = GetUnsyncedDraftLogRows()

        Try
            Dim groupedRows As New Dictionary(Of String, List(Of DataRow))()

            For Each logRow As DataRow In pendingRows
                Dim finalTId As Integer = GetLogIntegerValue(logRow, "Final_T_ID")
                Dim finalSbId As Integer = GetLogIntegerValue(logRow, "Final_SB_ID")
                Dim groupKey As String = finalTId.ToString() & "|" & finalSbId.ToString()

                If groupedRows.ContainsKey(groupKey) = False Then groupedRows.Add(groupKey, New List(Of DataRow)())
                groupedRows(groupKey).Add(logRow)
            Next

            Using con As New SqlConnection(MY_Settings.SqlConStr)
                con.Open()

                For Each groupItem As KeyValuePair(Of String, List(Of DataRow)) In groupedRows
                    Dim groupRows As List(Of DataRow) = groupItem.Value
                    Dim sqlLogsDt As DataTable = BuildDraftLogsSqlTable(groupRows)
                    If sqlLogsDt.Rows.Count = 0 Then Continue For

                    Using cmd As New SqlCommand("dbo.SalesDraft_ActionLogs_InsertBatch", con)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandTimeout = 120

                        Dim finalTId As Integer = GetLogIntegerValue(groupRows(0), "Final_T_ID")
                        Dim finalSbId As Integer = GetLogIntegerValue(groupRows(0), "Final_SB_ID")

                        If finalTId > 0 Then
                            cmd.Parameters.Add("@Final_T_ID", SqlDbType.Int).Value = finalTId
                        Else
                            cmd.Parameters.Add("@Final_T_ID", SqlDbType.Int).Value = DBNull.Value
                        End If

                        If finalSbId > 0 Then
                            cmd.Parameters.Add("@Final_SB_ID", SqlDbType.Int).Value = finalSbId
                        Else
                            cmd.Parameters.Add("@Final_SB_ID", SqlDbType.Int).Value = DBNull.Value
                        End If

                        Dim logsParameter As New SqlParameter("@Logs", SqlDbType.Structured)
                        logsParameter.TypeName = "dbo.SalesDraft_ActionLogType"
                        logsParameter.Value = sqlLogsDt
                        cmd.Parameters.Add(logsParameter)

                        cmd.ExecuteNonQuery()
                    End Using

                    MarkDraftLogsAsSynced(groupRows)
                    PersistDraftLogsQueue()
                Next
            End Using

            If GetUnsyncedDraftLogRows().Count = 0 Then HasLoadedPendingDraftLogs = False

        Catch ex As Exception
            AddDraftLog("حفظ", "تعذر ترحيل سجل الحركات: " & ex.Message, "SalesDraft_ActionLogs_InsertBatch")
        End Try

    End Sub

    Private Sub MarkDraftLogsAsSynced(logRows As List(Of DataRow))

        If DraftLogsDt Is Nothing OrElse DraftLogsDt.Columns.Contains("IsSynced") = False Then Exit Sub
        If logRows Is Nothing Then Exit Sub

        For Each logRow As DataRow In logRows
            If logRow.RowState = DataRowState.Deleted Then Continue For
            logRow("IsSynced") = True
        Next

    End Sub


    Private Sub Expenses_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Keys.F1
                If New_butt.Enabled = True Then New_butt_Click(sender, e)
            Case Keys.F12
                If Save_butt.Enabled = True Then Save_butt_Click(sender, e)
            Case Keys.F2
                If Print_btn.Enabled = True Then Print_btn_Click(sender, e)
            'Case Keys.F3
            '    If Edit_butt.Enabled = True And Edit_butt.Visible = True Then Edit_butt_Click(sender, e)
            'Case Keys.F4
            '    If Delete_butt.Enabled = True And Delete_butt.Visible = True Then Delete_butt_Click(sender, e)

            'Case Keys.F7
            '    SBPauseBtn_Click(sender, e)

            'Case Keys.PageUp
            '    Up_Bill_btn_Click(sender, e)
            'Case Keys.PageDown
            '    Down_Bill_btn_Click(sender, e)


            Case 107 'Add

                'If On_Update = False Then
                'If dgvSales.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
                If dgvSales.Rows.Count > 0 Then


                    'Dim Def As Double = 1

                    'If IM_min_QTY = False Then
                    '    If IM_Check_Neg_QTY_2(Def) = 1 Then
                    '        MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Exclamation, "")
                    '        Exit Sub
                    '    Else
                    '        Change_IM_Qty(Def)
                    '    End If
                    'Else
                    '    Change_IM_Qty(Def)
                    'End If


                    Dim Def As Double = 1
                    ChangeQtyByInput(Def)

                    'Dim inp = InputBox("ادخل رقم", "مقدار زيادة العدد")
                    'If inp <> "" Then Def = inp

                    'If IM_min_QTY = False Then
                    '    If IM_Check_Neg_QTY_2(Def) = 1 Then
                    '        MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Exclamation, "")
                    '        Exit Sub
                    '    Else
                    '        Change_IM_Qty(Def)
                    '    End If
                    'Else
                    '    Change_IM_Qty(Def)
                    'End If

                End If
                    'End If
                'End If


            Case 109 'Subtrac
                'If On_Update = False Then
                '    If dgvSales.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
                If dgvSales.Rows.Count > 0 Then

                    Dim Def As Double = -1
                    ChangeQtyByInput(Def)

                    'If dgvSales.CurrentRow.Cells("QTY_CL").Value > 1 Then
                    '    Dim Def As Double = -1

                    '    Dim inp = InputBox("ادخل رقم", "مقدار زيادة العدد")
                    '    If inp <> "" Then Def = inp * -1

                    '    'Change_IM_Qty(Def)
                    'End If
                End If
                '    End If
                'End If

            Case Keys.F9
                If dgvSales.RowsDefaultCellStyle.BackColor = Color.LightYellow And dgvSales.Rows.Count > 0 And On_Update = False Then Change_IM_Details.ShowDialog()


            Case Keys.Return
                If Barcode_SH_txt.Enabled = True Then
                    Barcode_SH_txt_KeyDown(sender, e)
                Else
                    e.Handled = True
                End If

            'Case Keys.F8
            '    If RemoveCatButton.Enabled = True Then
            '        If dgvSales.Rows.Count > 0 Then
            '            If MessageBox.Show(" حذف الصنف " + dgvSales.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
            '                               MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
            '                SB_Contents_Delete_IM(dgvSales.CurrentRow.Cells("T_ID_CL").Value)
            '            End If
            '        End If
            '    End If

            Case Keys.F11
                If U_SalesDis = True Then Make_Discount()

            Case Keys.ControlKey
                Barcode_SH_txt.Clear()
                Barcode_IM = ""
        End Select
    End Sub


    'Private Sub Change_IM_Qty(def As Double)

    '    If CurrentDraft Is Nothing Then Exit Sub
    '    If dgvSales.CurrentRow Is Nothing Then Exit Sub

    '    Dim draftLineId As String = dgvSales.CurrentRow.Cells("DraftLineId").Value.ToString()

    '    Dim item As SaleDraftItem =
    '    CurrentDraft.Items.FirstOrDefault(Function(x) x.DraftLineId = draftLineId)

    '    If item Is Nothing Then Exit Sub

    '    Dim newQty As Decimal = item.QTY + CDec(def)

    '    If newQty < 0 Then
    '        MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Exclamation, "")
    '        Exit Sub
    '    End If


    '    If newQty = 0 Then
    '        CurrentDraft.Items.Remove(item)
    '    Else
    '        item.QTY = newQty
    '    End If

    '    DraftCalculator.RecalculateDraft(CurrentDraft)
    '    DraftManager.SaveDraft(CurrentDraft)
    '    LoadDraftToGrid()
    '    UpdateDraftTotalsOnScreen()

    'End Sub

    Private Sub ChangeSelectedDraftItemQty(deltaQty As Decimal)

        If CurrentDraft Is Nothing Then Exit Sub
        If dgvSales.CurrentRow Is Nothing Then Exit Sub

        Dim draftLineId As String = dgvSales.CurrentRow.Cells("DraftLineId").Value.ToString()

        Dim item As SaleDraftItem =
        CurrentDraft.Items.FirstOrDefault(Function(x) x.DraftLineId = draftLineId)

        If item Is Nothing Then Exit Sub

        Dim newQty As Decimal = item.QTY + deltaQty

        If newQty < 1 Then
            'MessageBox.Show("لا يمكن أن تصبح الكمية سالبة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        item.QTY = newQty

        DraftCalculator.RecalculateDraft(CurrentDraft)
        DraftManager.SaveDraft(CurrentDraft)
        LoadDraftToGrid()
        SelectDraftLine(draftLineId)
        UpdateDraftTotalsOnScreen()
        AddDraftLog("كمية",
                    "تم تعديل كمية الصنف",
                    "IMIncreaseButton/IMDicreaseButton",
                    (newQty - deltaQty).ToString("0.###"),
                    newQty.ToString("0.###"),
                    item.ItemName,
                    item.IM_ID,
                    item.QTY,
                    item.T_Price)

    End Sub

    Private Sub SetSelectedDraftItemQty(newQty As Decimal, Optional sourceControl As String = "dgvSales")

        If CurrentDraft Is Nothing Then Exit Sub
        If dgvSales.CurrentRow Is Nothing Then Exit Sub

        If newQty <= 0D Then
            MsgBox("يجب أن تكون الكمية أكبر من صفر", MsgBoxStyle.Exclamation, "تنبيه")
            Exit Sub
        End If

        Dim draftLineId As String = dgvSales.CurrentRow.Cells("DraftLineId").Value.ToString()

        Dim item As SaleDraftItem =
        CurrentDraft.Items.FirstOrDefault(Function(x) x.DraftLineId = draftLineId)

        If item Is Nothing Then Exit Sub

        Dim oldQty As Decimal = item.QTY
        item.QTY = newQty

        DraftCalculator.RecalculateDraft(CurrentDraft)
        DraftManager.SaveDraft(CurrentDraft)
        LoadDraftToGrid()
        SelectDraftLine(draftLineId)
        UpdateDraftTotalsOnScreen()
        AddDraftLog("كمية",
                    "تم تحديد كمية الصنف يدويًا",
                    sourceControl,
                    oldQty.ToString("0.###"),
                    newQty.ToString("0.###"),
                    item.ItemName,
                    item.IM_ID,
                    item.QTY,
                    item.T_Price)

    End Sub

    Private Sub SetSelectedDraftItemPrice(newPrice As Decimal)

        If U_SB_IM_Update = False Then
            MsgBox("لا تملك صلاحية تعديل سعر بيع الصنف", MsgBoxStyle.Exclamation, "تنبيه")
            Exit Sub
        End If

        If CurrentDraft Is Nothing Then Exit Sub
        If dgvSales.CurrentRow Is Nothing Then Exit Sub

        If newPrice <= 0D Then
            MsgBox("يجب أن يكون سعر البيع أكبر من صفر", MsgBoxStyle.Exclamation, "تنبيه")
            Exit Sub
        End If

        Dim draftLineId As String = dgvSales.CurrentRow.Cells("DraftLineId").Value.ToString()

        Dim item As SaleDraftItem =
        CurrentDraft.Items.FirstOrDefault(Function(x) x.DraftLineId = draftLineId)

        If item Is Nothing Then Exit Sub

        Dim oldPrice As Decimal = item.Price

        If oldPrice = newPrice Then Exit Sub

        item.Price = newPrice
        item.T_Price = item.QTY * item.Price

        DraftCalculator.RecalculateDraft(CurrentDraft)
        DraftManager.SaveDraft(CurrentDraft)
        LoadDraftToGrid()
        SelectDraftLine(draftLineId)
        UpdateDraftTotalsOnScreen()
        AddDraftLog("سعر",
                    "تم تعديل سعر بيع الصنف",
                    "ChangePriceButton",
                    oldPrice.ToString("0.###"),
                    newPrice.ToString("0.###"),
                    item.ItemName,
                    item.IM_ID,
                    item.QTY,
                    item.T_Price)

    End Sub

    Private Sub SelectDraftLine(draftLineId As String)

        If String.IsNullOrWhiteSpace(draftLineId) Then Exit Sub
        If Not dgvSales.Columns.Contains("DraftLineId") Then Exit Sub

        For Each row As DataGridViewRow In dgvSales.Rows
            If Convert.ToString(row.Cells("DraftLineId").Value) = draftLineId Then
                Dim cellName As String = GetFirstExistingGridColumnName("QTY_CL", "QTY", "Item_Name", "ItemName", "Barcode_CL")
                dgvSales.ClearSelection()
                dgvSales.CurrentCell = row.Cells(cellName)
                row.Selected = True
                Exit For
            End If
        Next

    End Sub

    Private Function GetFirstExistingGridColumnName(ParamArray columnNames() As String) As String

        For Each columnName As String In columnNames
            If dgvSales.Columns.Contains(columnName) Then Return columnName
        Next

        For Each column As DataGridViewColumn In dgvSales.Columns
            If column.Visible Then Return column.Name
        Next

        Return dgvSales.Columns(0).Name

    End Function

    Private Sub ChangeQtyByInput(def_type As Integer, Optional choice As Boolean = True)

        If CurrentDraft Is Nothing Then Exit Sub
        If dgvSales.CurrentRow Is Nothing Then Exit Sub

        Dim str As String = ""

        If def_type = 1 Then
            str = "مقدار زيادة العدد"
        Else
            str = "مقدار نقص العدد"
        End If

        Dim def As Decimal = 0D


        Dim inp As String = ""

        If choice = True Then
            inp = InputBox("ادخل رقم", str)
        End If


        If inp.Trim() = "" Then
            If def_type = 1 Then
                def = 1
            Else
                def = -1
            End If
        Else
            def = inp
            If def_type = -1 Then def = def * -1
        End If




        'If Not Decimal.TryParse(inp, def) Then
        '    MsgBox("القيمة المدخلة غير صحيحة", MsgBoxStyle.Exclamation, "")
        '    Exit Sub
        'End If

        ChangeSelectedDraftItemQty(def)

    End Sub

    'Private Function CheckNegativeQtyForSelectedLine(deltaQty As Decimal) As Boolean

    '    If CurrentDraft Is Nothing Then Return True
    '    If dgvSales.CurrentRow Is Nothing Then Return True

    '    Dim draftLineId As String = dgvSales.CurrentRow.Cells("DraftLineId").Value.ToString()

    '    Dim item As SaleDraftItem =
    '    CurrentDraft.Items.FirstOrDefault(Function(x) x.DraftLineId = draftLineId)

    '    If item Is Nothing Then Return True

    '    Dim newQty As Decimal = item.QTY + deltaQty

    '    If newQty < 0 Then
    '        Return True
    '    End If

    '    Return False

    'End Function

    'Private Sub ChangeQtyByInput()

    '    If CurrentDraft Is Nothing Then Exit Sub
    '    If dgvSales.CurrentRow Is Nothing Then Exit Sub

    '    Dim def As Decimal = 0D
    '    Dim inp As String = InputBox("ادخل رقم", "مقدار زيادة العدد")

    '    If inp.Trim() = "" Then Exit Sub

    '    If Not Decimal.TryParse(inp, def) Then
    '        MsgBox("القيمة المدخلة غير صحيحة", MsgBoxStyle.Exclamation, "")
    '        Exit Sub
    '    End If

    '    If IM_min_QTY = False Then
    '        If CheckNegativeQtyForSelectedLine(def) Then
    '            MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Exclamation, "")
    '            Exit Sub
    '        Else
    '            ChangeSelectedDraftItemQty(def)
    '        End If
    '    Else
    '        ChangeSelectedDraftItemQty(def)
    '    End If

    'End Sub

    Public Function IM_Check_Neg_QTY_2(qty)

        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@ST_ID", dgvSales.CurrentRow.Cells("ST_ID_CL").Value)
            .Parameters.AddWithValue("@IM_ID", dgvSales.CurrentRow.Cells("Bill_IMID_CL").Value)
            .Parameters.AddWithValue("@D_Vaild", dgvSales.CurrentRow.Cells("D_Valid_CL").Value)
            .Parameters.AddWithValue("@Enterd_Qty", qty)
            '.Parameters.AddWithValue("@Cargo", )
            .Parameters.AddWithValue("@U_ID", dgvSales.CurrentRow.Cells("U_ID_CL").Value)

            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then F = .Parameters("@F").Value
        End With

        Return F

    End Function

    'Private Sub Change_IM_Qty(def As Integer)
    '    Dim SB_T_ID As Integer = dgvSales.CurrentRow.Cells("T_ID_CL").Value
    '    Dim Row_Index As Integer = dgvSales.CurrentCell.RowIndex
    '    Dim c As New C
    '    With c.Com
    '        .Connection = c.Con
    '        .CommandText = "SB_Contents_Change_IM_Qty"
    '        .CommandType = CommandType.StoredProcedure
    '        .Parameters.AddWithValue("@T_ID", SB_T_ID)
    '        .Parameters.AddWithValue("@Def", def)
    '        .Parameters.AddWithValue("@On_Update", On_Update)
    '    End With

    '    If SQL_SP_EXEC(c.Com) = True Then
    '        SB_Contents_SELECT_Bill()
    '        dgvSales.CurrentCell = dgvSales.Rows(Row_Index).Cells("EX_Name_CL")
    '        Network_Edit_Tracker_insert(" الصنف:" + dgvSales.CurrentRow.Cells("EX_Name_CL").Value.ToString + " العدد:" + dgvSales.CurrentRow.Cells("QTY_CL").Value.ToString + " السعر:" + dgvSales.CurrentRow.Cells("Price_CL").Value.ToString,
    '            Bill_ID_Txt.Text, 1, 3)
    '    End If
    'End Sub

    Private Sub loadShortCut_IM()

        EnsureShortcutPanels()
        RenderShortcutGroupButtons()
        RenderShortcutItemsByGroup()

    End Sub

    Private Sub EnsureShortcutPanels()

        If ShortcutGroupPanel Is Nothing OrElse ShortcutItemsPanel Is Nothing Then
            IMPanel.Controls.Clear()
            IMPanel.AutoScroll = False

            ShortcutItemsPanel = New Panel()
            ShortcutItemsPanel.Name = "ShortcutItemsPanel"
            ShortcutItemsPanel.AutoScroll = True
            ShortcutItemsPanel.BackColor = Color.Transparent
            ShortcutItemsPanel.RightToLeft = Windows.Forms.RightToLeft.No

            ShortcutGroupPanel = New Panel()
            ShortcutGroupPanel.Name = "ShortcutGroupPanel"
            ShortcutGroupPanel.AutoScroll = True
            ShortcutGroupPanel.BackColor = Color.FromArgb(226, 232, 240)
            ShortcutGroupPanel.RightToLeft = Windows.Forms.RightToLeft.Yes

            IMPanel.Controls.Add(ShortcutItemsPanel)
            IMPanel.Controls.Add(ShortcutGroupPanel)
        End If

        LayoutShortcutPanels()

    End Sub

    Private Sub LayoutShortcutPanels()

        If ShortcutGroupPanel Is Nothing OrElse ShortcutItemsPanel Is Nothing Then Exit Sub

        Dim paddingValue As Integer = 4
        Dim groupWidth As Integer = CInt(IMPanel.Width * 0.23)
        If groupWidth < 135 Then groupWidth = 135
        If groupWidth > 180 Then groupWidth = 180

        ShortcutGroupPanel.Location = New Point(IMPanel.Width - groupWidth - paddingValue, paddingValue)
        ShortcutGroupPanel.Size = New Size(groupWidth, IMPanel.Height - (paddingValue * 2))

        ShortcutItemsPanel.Location = New Point(paddingValue, paddingValue)
        ShortcutItemsPanel.Size = New Size(IMPanel.Width - groupWidth - (paddingValue * 3), IMPanel.Height - (paddingValue * 2))

    End Sub

    Private Function TryLoadShortcutItemsFromDatabase() As Boolean

        Dim c As New C
        Dim loadedShortcuts As New DataTable()
        LastShortcutsLoadError = ""

        Try
            Dim s As String = "select IM_ID,ISNULL(GM_ID,0) AS GM_ID,ISNULL(NULLIF(GM_NAME,''),N'بدون مجموعة') AS GM_NAME,item_name,Photo,BK_R,BK_G,BK_B,FK_R,FK_G,FK_B from IM_Menu_V WHERE is_Shortcut = 1 order by GM_NAME ASC,item_name ASC"
            c.Com = New SqlClient.SqlCommand(s, c.Con)
            c.Da = New SqlClient.SqlDataAdapter(c.Com)
            c.Da.Fill(loadedShortcuts)

            If HasRequiredColumns(loadedShortcuts, New String() {"IM_ID", "GM_ID", "GM_NAME", "item_name"}) = False Then
                Throw New DataException("بيانات اختصارات الأصناف لا تحتوي على الأعمدة المطلوبة.")
            End If

            ShortcutItemsDt = loadedShortcuts

            Try
                SaveDataTableCache(ShortcutItemsDt, ShortcutsCacheFilePath, "ItemShortcuts")
            Catch cacheEx As Exception
                LastShortcutsLoadError = "تم تحميل الاختصارات، لكن تعذر تحديث الكاش المحلي: " & cacheEx.Message
            End Try

            Return True
        Catch ex As Exception
            LastShortcutsLoadError = ex.Message
            Return False
        Finally
            If c.Con.State = ConnectionState.Open Then c.Con.Close()
        End Try

    End Function

    Private Function LoadSalesDataFromLocalCache() As Boolean
        Dim loadedAnyCache As Boolean = False
        Dim cachedItems As DataTable = Nothing
        Dim cachedShortcuts As DataTable = Nothing
        Dim cacheError As String = ""

        If TryLoadDataTableCache(ItemsCacheFilePath,
                                 New String() {"U_IM_ID", "IM_ID", "item_name", "U_Name", "U_ID", "U_Cargo", "Price", "Barcode"},
                                 cachedItems,
                                 cacheError) Then
            IM_Units_Dt = cachedItems
            loadedAnyCache = True
        End If

        cacheError = ""
        If TryLoadDataTableCache(ShortcutsCacheFilePath,
                                 New String() {"IM_ID", "GM_ID", "GM_NAME", "item_name"},
                                 cachedShortcuts,
                                 cacheError) Then
            ShortcutItemsDt = cachedShortcuts
            loadedAnyCache = True
        End If

        Return loadedAnyCache
    End Function

    Private Function TryLoadDataTableCache(filePath As String,
                                           requiredColumns As String(),
                                           ByRef loadedTable As DataTable,
                                           ByRef errorMessage As String) As Boolean
        loadedTable = Nothing
        errorMessage = ""

        Try
            If IO.File.Exists(filePath) = False Then Return False

            Dim cacheTable As New DataTable()
            cacheTable.ReadXml(filePath)

            If HasRequiredColumns(cacheTable, requiredColumns) = False Then
                errorMessage = "ملف الكاش لا يحتوي على الأعمدة المطلوبة."
                Return False
            End If

            loadedTable = cacheTable
            Return True
        Catch ex As Exception
            errorMessage = ex.Message
            Return False
        End Try
    End Function

    Private Function HasRequiredColumns(table As DataTable, requiredColumns As String()) As Boolean
        If table Is Nothing Then Return False

        For Each columnName As String In requiredColumns
            If table.Columns.Contains(columnName) = False Then Return False
        Next

        Return True
    End Function

    Private Sub SaveDataTableCache(sourceTable As DataTable, filePath As String, tableName As String)
        If sourceTable Is Nothing Then Exit Sub

        Dim tempPath As String = filePath & ".tmp"
        If IO.Directory.Exists(SalesCacheFolder) = False Then IO.Directory.CreateDirectory(SalesCacheFolder)
        If IO.File.Exists(tempPath) Then IO.File.Delete(tempPath)

        Try
            Dim cacheTable As DataTable = sourceTable.Copy()
            cacheTable.TableName = tableName

            Using fs As New IO.FileStream(tempPath,
                                         IO.FileMode.Create,
                                         IO.FileAccess.Write,
                                         IO.FileShare.None,
                                         4096,
                                         IO.FileOptions.WriteThrough)
                cacheTable.WriteXml(fs, XmlWriteMode.WriteSchema)
                fs.Flush(True)
            End Using

            Dim verifyTable As New DataTable()
            verifyTable.ReadXml(tempPath)
            If verifyTable.Columns.Count <> sourceTable.Columns.Count Then Throw New DataException("فشل التحقق من ملف كاش الأصناف الجديد.")

            If IO.File.Exists(filePath) Then
                IO.File.Replace(tempPath, filePath, Nothing, True)
            Else
                IO.File.Move(tempPath, filePath)
            End If
        Finally
            If IO.File.Exists(tempPath) Then IO.File.Delete(tempPath)
        End Try
    End Sub

    Private Sub RenderShortcutGroupButtons()

        If ShortcutGroupPanel Is Nothing Then Exit Sub
        ShortcutGroupPanel.Controls.Clear()
        ShortcutGroupPanel.AutoScrollMinSize = New Size(0, 0)

        If ShortcutItemsDt Is Nothing OrElse ShortcutItemsDt.Rows.Count = 0 Then
            ShortcutSelectedGroupID = -1
            Exit Sub
        End If

        Dim groupsDt As DataTable = ShortcutItemsDt.DefaultView.ToTable(True, "GM_ID", "GM_NAME")
        Dim groupsView As New DataView(groupsDt)
        groupsView.Sort = "GM_NAME ASC"

        Dim selectedExists As Boolean = False
        Dim firstGroupID As Integer = -1

        For Each rowView As DataRowView In groupsView
            Dim groupID As Integer = CInt(rowView("GM_ID"))
            If firstGroupID = -1 Then firstGroupID = groupID
            If groupID = ShortcutSelectedGroupID Then selectedExists = True
        Next

        If selectedExists = False Then ShortcutSelectedGroupID = firstGroupID

        Dim y As Integer = 4
        For Each rowView As DataRowView In groupsView
            Dim groupID As Integer = CInt(rowView("GM_ID"))
            Dim groupButton As New Button()

            groupButton.Name = "ShortcutGroupBtn" & groupID.ToString()
            groupButton.Tag = groupID
            groupButton.Text = rowView("GM_NAME").ToString()
            groupButton.Cursor = Cursors.Hand
            groupButton.FlatStyle = FlatStyle.Flat
            groupButton.Font = New Font("Segoe UI Semibold", 9.5!, FontStyle.Bold)
            groupButton.TextAlign = ContentAlignment.MiddleCenter
            groupButton.RightToLeft = Windows.Forms.RightToLeft.Yes
            groupButton.Size = New Size(ShortcutGroupPanel.ClientSize.Width - 10, 42)
            groupButton.Location = New Point(4, y)
            groupButton.UseVisualStyleBackColor = False

            ApplyShortcutGroupButtonStyle(groupButton, groupID = ShortcutSelectedGroupID)
            AddHandler groupButton.Click, AddressOf ShortcutGroupButton_Click

            ShortcutGroupPanel.Controls.Add(groupButton)
            y += groupButton.Height + 5
        Next

        ShortcutGroupPanel.AutoScrollMinSize = New Size(0, y + 4)

    End Sub

    Private Sub ApplyShortcutGroupButtonStyle(groupButton As Button, isSelected As Boolean)

        If isSelected Then
            groupButton.BackColor = Color.FromArgb(14, 116, 144)
            groupButton.ForeColor = Color.White
            groupButton.FlatAppearance.BorderColor = Color.FromArgb(8, 145, 178)
            groupButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(8, 145, 178)
            groupButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(21, 94, 117)
        Else
            groupButton.BackColor = Color.FromArgb(248, 250, 252)
            groupButton.ForeColor = Color.FromArgb(15, 23, 42)
            groupButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225)
            groupButton.FlatAppearance.MouseOverBackColor = Color.White
            groupButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(224, 242, 254)
        End If

        groupButton.FlatAppearance.BorderSize = 1

    End Sub

    Private Sub ShortcutGroupButton_Click(sender As Object, e As EventArgs)

        Dim groupButton As Button = DirectCast(sender, Button)
        ShortcutSelectedGroupID = CInt(groupButton.Tag)
        AddDraftLog("زر", "تم اختيار مجموعة الأصناف: " & groupButton.Text, "ShortcutGroupButton")

        RenderShortcutGroupButtons()
        RenderShortcutItemsByGroup()

    End Sub

    Private Sub RenderShortcutItemsByGroup()

        If ShortcutItemsPanel Is Nothing Then Exit Sub
        ShortcutItemsPanel.Controls.Clear()

        If ShortcutItemsDt Is Nothing OrElse ShortcutItemsDt.Rows.Count = 0 OrElse ShortcutSelectedGroupID = -1 Then
            ShowNoShortcutItemsLabel("لا توجد أصناف مختصرة")
            Exit Sub
        End If

        Dim rows() As DataRow = ShortcutItemsDt.Select("GM_ID = " & ShortcutSelectedGroupID.ToString(), "item_name ASC")
        If rows.Length = 0 Then
            ShowNoShortcutItemsLabel("لا توجد أصناف لهذه المجموعة")
            Exit Sub
        End If

        Dim spacing As Integer = 5
        Dim columns As Integer = 5
        If ShortcutItemsPanel.ClientSize.Width < 520 Then columns = 4
        If ShortcutItemsPanel.ClientSize.Width > 760 Then columns = 6

        Dim buttonWidth As Integer = CInt((ShortcutItemsPanel.ClientSize.Width - ((columns + 1) * spacing)) / columns)
        If buttonWidth < 92 Then buttonWidth = 92

        Dim buttonHeight As Integer = CInt((ShortcutItemsPanel.ClientSize.Height - (spacing * 5)) / 4)
        If buttonHeight < 45 Then buttonHeight = 45
        If buttonHeight > 58 Then buttonHeight = 58

        Dim index As Integer = 0
        For Each row As DataRow In rows
            Dim col As Integer = index Mod columns
            Dim rowIndex As Integer = index \ columns
            Dim IMbtn As New Button()

            IMbtn.Name = "IMbtn" & row("IM_ID").ToString()
            IMbtn.Tag = row("IM_ID")
            IMbtn.TextAlign = ContentAlignment.MiddleCenter
            IMbtn.AutoSize = False
            IMbtn.Cursor = Cursors.Hand
            IMbtn.FlatStyle = FlatStyle.Popup
            IMbtn.Location = New Point(spacing + (col * (buttonWidth + spacing)), spacing + (rowIndex * (buttonHeight + spacing)))
            IMbtn.Size = New Size(buttonWidth, buttonHeight)
            IMbtn.RightToLeft = Windows.Forms.RightToLeft.Yes
            IMbtn.Font = New Font("Segoe UI Semibold", 9.5!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
            IMbtn.Text = row("item_name").ToString()
            IMbtn.BackColor = SystemColors.Window

            If IsDBNull(row("BK_R")) = False Then
                IMbtn.BackColor = Color.FromArgb(CInt(row("BK_R")), CInt(row("BK_G")), CInt(row("BK_B")))
            End If

            If IsDBNull(row("FK_R")) = False Then
                IMbtn.ForeColor = Color.FromArgb(CInt(row("FK_R")), CInt(row("FK_G")), CInt(row("FK_B")))
            End If

            AddHandler IMbtn.Click, AddressOf IMbtn_Click
            ShortcutItemsPanel.Controls.Add(IMbtn)
            index += 1
        Next

    End Sub

    Private Sub ShowNoShortcutItemsLabel(messageText As String)

        If ShortcutItemsPanel Is Nothing Then Exit Sub

        Dim emptyLabel As New Label()
        emptyLabel.Text = messageText
        emptyLabel.Font = New Font("Segoe UI Semibold", 11.0!, FontStyle.Bold)
        emptyLabel.ForeColor = Color.FromArgb(100, 116, 139)
        emptyLabel.TextAlign = ContentAlignment.MiddleCenter
        emptyLabel.Dock = DockStyle.Fill
        ShortcutItemsPanel.Controls.Add(emptyLabel)

    End Sub

    Sub IMbtn_Click(ByVal sender As Object, ByVal e As EventArgs)
        IM_Name = sender.Text.ToString
        IM_ID = sender.Tag
        AddDraftLog("زر", "اختيار صنف من اختصارات البيع", "IMPanel", "", IM_Name, IM_Name, IM_ID)
        Load_IM_By_ID()
    End Sub

    Private Sub POS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress

        Select Case e.KeyChar
            Case "+", "-"
                e.Handled = True
                Exit Sub
        End Select

        If Me.Barcode_SH_txt.Focused = False Then
            Barcode_SH_txt.Focus()
            If Me.Barcode_SH_txt.Enabled = True Then
                Barcode_SH_txt.Text = e.KeyChar.ToString
                Barcode_SH_txt.SelectionStart = Barcode_SH_txt.Text.Length
                e.Handled = True
            End If
        End If

    End Sub

    Private Async Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetScreenStatus("جاري تهيئة شاشة المبيعات وتحميل البيانات...", ScreenStatusType.Loading)
        ThemeManager.ApplyThemeToForm(Me)
        ApplyTopButtonsStyle()

        'If My_Settings.App_Suuply = "RESAL" Then Me.Icon = New Icon(Me.GetType(), "resal_soft.ico")
        FormType = 1
        Check_View_Control()
        rs.FindAllControls(Me)
        InitializeDraftLogs()
        Me.WindowState = FormWindowState.Maximized
        '   EditState = Edit_butt.Text
        Dim hasLocalCache As Boolean = LoadSalesDataFromLocalCache()
        loadShortCut_IM()
        'GET_Printer_Type()
        LoadPrintSettings()

        AG_ID = Default_AG_ID
        AddDraftLog("فتح", "تم فتح شاشة المبيعات المسودة", "Sales_Fast_Draft")

        Dim itemsLoaded As Boolean = Await Load_ALL_IM()
        Dim shortcutsLoaded As Boolean = TryLoadShortcutItemsFromDatabase()
        If shortcutsLoaded Then loadShortCut_IM()
        UpdateDraftButtonIndicator()

        If itemsLoaded AndAlso shortcutsLoaded Then
            SetScreenStatus("تم تحميل الشاشة والأصناف بنجاح", ScreenStatusType.Success)
        ElseIf IM_Units_Dt IsNot Nothing AndAlso IM_Units_Dt.Rows.Count > 0 Then
            SetScreenStatus("تعمل الشاشة بآخر بيانات أصناف محفوظة لعدم توفر الاتصال الحالي",
                            ScreenStatusType.Warning,
                            (LastItemsLoadError & Environment.NewLine & LastShortcutsLoadError).Trim())
        ElseIf hasLocalCache Then
            SetScreenStatus("تم تحميل جزء من البيانات المحلية، لكن قائمة الأصناف غير متاحة",
                            ScreenStatusType.Warning,
                            (LastItemsLoadError & Environment.NewLine & LastShortcutsLoadError).Trim())
        Else
            SetScreenStatus("تعذر تحميل الأصناف ولا توجد بيانات محلية سابقة",
                            ScreenStatusType.Error,
                            (LastItemsLoadError & Environment.NewLine & LastShortcutsLoadError).Trim())
        End If

        'If isShowing_Trans = True Then
        '    T_ID = T_ID_Trans
        '    SB_Contents_SELECT_Bill()
        '    Fill_Bill_Info()
        '    SelectStateBt()
        '    New_butt.Enabled = False
        '    SBPauseBtn.Enabled = False
        'Else
        '    If Open_NewBill_When_OpenSale = True Then ResetNewBill()
        'End If

    End Sub


    Public Async Function Load_ALL_IM() As Task(Of Boolean)
        Dim c As New C
        Dim s As String
        Dim loadedItems As New DataTable()
        LastItemsLoadError = ""
        Try
            s = "SELECT U_IM_ID, IM_ID, item_name, U_Name, U_ID, U_Cargo, Price, Min_SP, Min_SP_2,Percent_Price,Barcode FROM IM_Menu_Units_V ORDER BY IM_ID, U_ID ASC"

            Using cmd As New SqlCommand(s, c.Con)
                Await c.Con.OpenAsync()
                Using reader = Await cmd.ExecuteReaderAsync()
                    loadedItems.Load(reader)
                End Using
                c.Con.Close()
            End Using

            If loadedItems.Rows.Count = 0 Then Throw New DataException("لم يُرجع تحميل الأصناف أي بيانات.")
            If HasRequiredColumns(loadedItems, New String() {"U_IM_ID", "IM_ID", "item_name", "U_Name", "U_ID", "U_Cargo", "Price", "Barcode"}) = False Then
                Throw New DataException("بيانات الأصناف لا تحتوي على الأعمدة المطلوبة.")
            End If

            IM_Units_Dt = loadedItems

            Try
                SaveDataTableCache(IM_Units_Dt, ItemsCacheFilePath, "ItemsUnits")
            Catch cacheEx As Exception
                LastItemsLoadError = "تم تحميل الأصناف، لكن تعذر تحديث الكاش المحلي: " & cacheEx.Message
            End Try

            Return True
        Catch ex As Exception
            LastItemsLoadError = ex.Message
            If c.Con.State = ConnectionState.Open Then c.Con.Close()
            Return False
        End Try
    End Function






    'Private Sub GET_Printer_Type()
    '    Dim c2 As New C
    '    c2.Str = "select ID,Type from Sales_Bill_Page"
    '    c2.Da = New SqlClient.SqlDataAdapter(c2.Str, c2.Con)
    '    c2.Da.Fill(c2.Dt)
    '    Sales_Bill_Page_cm.DataSource = c2.Dt
    '    Sales_Bill_Page_cm.DisplayMember = "Type"
    '    Sales_Bill_Page_cm.ValueMember = "ID"
    '    Sales_Bill_Page_cm.SelectedValue = Sales_Page_ID
    'End Sub


    Public Sub Check_View_Control()
        dgvSales.Columns("Date_").Visible = MY_Settings.S_Date_CL
        dgvSales.Columns("ST_Name_CL").Visible = MY_Settings.S_ST_Name_CL
        dgvSales.Columns("D_Valid_CL").Visible = MY_Settings.S_D_Valid_CL
        dgvSales.Columns("IMUnit_CL").Visible = MY_Settings.S_IMUnit_CL
        dgvSales.Columns("Price_CL").Visible = MY_Settings.S_Price_CL
        dgvSales.Columns("Total_CL").Visible = MY_Settings.S_Total_CL
        dgvSales.Columns("Notes_CL").Visible = MY_Settings.SP_Notes_CL
        dgvSales.Columns("IMNUM_CL").Visible = MY_Settings.S_IMNUM_CL
        dgvSales.Columns("Barcode_CL").Visible = MY_Settings.S_Barcode_CL
        dgvSales.Columns("Serial_Code_CL").Visible = MY_Settings.S_Serial_Code_CL
        dgvSales.Columns("IM_Discount_CL").Visible = MY_Settings.S_IM_Discount_CL


        ' Delete_butt.Visible = U_SalesVoid
        If U_SalesDis = True And isDiscount = True Then
            DiscountPanel.Visible = True
            Calc_Dicount_Btn.Visible = True
        Else
            DiscountPanel.Visible = False
            Calc_Dicount_Btn.Visible = False
        End If
        '  Edit_butt.Visible = U_SB_Update
        Show_Cash_btn.Visible = U_SB_Show_Cash
        ChangePriceButton.Visible = U_SB_IM_Update
        ChangePriceButton.Enabled = U_SB_IM_Update
        Show_Cash_btn.Visible = S_Pr
        'IM_Profet_btn.Visible = U_Show_Bill_Profet
    End Sub


    Private Sub Enable_Fields()
        DateTimeEx.Enabled = True
        Ebable_CatFields()
    End Sub

    Private Sub Disable_Fields()
        DateTimeEx.Enabled = False
        Disable_CatFields()
    End Sub

    Private Sub Disable_CatFields()
        Barcode_SH_txt.Enabled = False
        RemoveCatButton.Enabled = False
        IMPanel.Enabled = False
        txtNotes.Enabled = False
        IM_Search_btn.Enabled = False
        ChangePriceButton.Enabled = False
        ClearDraftItemsButton.Enabled = False
    End Sub

    Private Sub Ebable_CatFields()
        Barcode_SH_txt.Enabled = True
        RemoveCatButton.Enabled = True
        IMPanel.Enabled = True
        txtNotes.Enabled = True
        IM_Search_btn.Enabled = True
        ChangePriceButton.Enabled = U_SB_IM_Update
        ClearDraftItemsButton.Enabled = True
    End Sub


    Public Sub Switch_Dependcy(F As Boolean)

        If F = True Then
            isDepended = 1
            dgvSales.BackgroundColor = Color.LightGreen
            dgvSales.RowsDefaultCellStyle.BackColor = Color.LightGreen
            Save_butt.Enabled = False
        Else
            isDepended = 0
            dgvSales.BackgroundColor = Color.LightYellow
            dgvSales.RowsDefaultCellStyle.BackColor = Color.LightYellow
            Save_butt.Enabled = True
        End If

    End Sub

    Private Sub NewStateBt()
        Enable_Fields()
        Save_butt.Enabled = True
        Me.Text = "فاتورة مبيعات جديدة"
    End Sub
    Private Sub DeleteOrUpdateStateBt()
        Disable_Fields()
        Save_butt.Enabled = False
        ' Delete_butt.Enabled = False
        Me.Text = DefaultFormState
    End Sub

    Private Sub SavedStateBt()
        Disable_Fields()
        Save_butt.Enabled = False
        '  Delete_butt.Enabled = False
        Me.Text = DefaultFormState
    End Sub

    Private Sub ClearFields()
        'T_ID = 0
        IM_Price = 0
        Total_TextBox.Clear()
        DateTimeEx.Text = Date.Now
        'VoidLb.Visible = False
        isVoid = False
        isDepended = False
        ClearCatFields()
        Discount_txt.Clear()
        Disc = 0
        Me.Text = FormState
        '   Edit_butt.BackColor = Color.WhiteSmoke
        '   Edit_butt.Text = EditState
        On_Update = False
        SB_ID = 0
        AG_ID = 1
    End Sub


    Public Sub ResetNewBill()


        CurrentDraft = CreateOrReuseEmptyDraftForNewBill()

        BindDraftHeaderToForm()
        LoadDraftToGrid()
        UpdateDraftTotalsOnScreen()
        Bill_ID_Txt.Clear()
        Enable_Fields()


        Dim Insert_New As Integer = 0
        If dgvSales.Rows.Count > 0 And isDepended = False Then Insert_New = 1
        'Load_PauseBills()
        ClearFields()
        DateTimeEx.Value = CurrentDraft.Date
        'Call_New_Bill(Insert_New)
        NewStateBt()
        AddDraftLog("فتح", "تم تجهيز فاتورة مسودة جديدة", "ResetNewBill", "", CurrentDraft.DraftId)
    End Sub

    Private Function CreateOrReuseEmptyDraftForNewBill() As SaleDraftHeader

        Dim draft As SaleDraftHeader = DraftManager.GetEmptyDraftForNewBill(USER_ID)

        If draft Is Nothing Then
            draft = DraftManager.CreateNewDraft(USER_ID)
        End If

        Return draft

    End Function

    Private Async Sub Refresh_IM_MENU()
        Dim itemsLoaded As Boolean = Await Load_ALL_IM()
        Dim shortcutsLoaded As Boolean = TryLoadShortcutItemsFromDatabase()
        If shortcutsLoaded Then loadShortCut_IM()

        If itemsLoaded = False AndAlso shortcutsLoaded = False Then
            SetScreenStatus("تعذر التحديث؛ تم الإبقاء على بيانات الأصناف السابقة",
                            ScreenStatusType.Warning,
                            (LastItemsLoadError & Environment.NewLine & LastShortcutsLoadError).Trim())
        End If
    End Sub


    Private Sub Save_butt_Click(sender As Object, e As EventArgs) Handles Save_butt.Click


        If CurrentDraft Is Nothing Then
            SetScreenStatus("لا توجد فاتورة حالية للحفظ", ScreenStatusType.Warning)
            MessageBox.Show("لا توجد فاتورة حالية.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        AddDraftLog("حفظ", "بدء حفظ وترحيل المسودة", "Save_butt", "", CurrentDraft.DraftId)
        SetScreenStatus("جاري حفظ وترحيل المسودة...", ScreenStatusType.Loading, CurrentDraft.DraftId)

        Me.Cursor = Cursors.WaitCursor
        Try
            Dim ok As Boolean = PushCurrentDraftToDatabase()

            If ok Then
                SetCurrentDraftLogsFinalIds()
                AddDraftLog("حفظ",
                            "تم حفظ المسودة وترحيلها بنجاح",
                            "Save_butt",
                            CurrentDraft.DraftId,
                            If(CurrentDraft.Final_SB_ID.HasValue, CurrentDraft.Final_SB_ID.Value.ToString(), ""))
                'AddDraftLog("طباعة", "طباعة الفاتورة بعد الحفظ", "PrintCurrentBill")
                PushDraftLogsToDatabase(HasLoadedPendingDraftLogs)
                DraftManager.ArchiveDraft(CurrentDraft)
                Prepare_to_print()
                ResetNewBill()
            Else
                AddDraftLog("حفظ", "لم يتم حفظ المسودة أو تم إلغاء العملية", "Save_butt", "", CurrentDraft.DraftId)
            End If

        Finally
            'BtnPushFinal.Enabled = True
            Me.Cursor = Cursors.Default
        End Try

    End Sub




    Private Function ValidateDraftBeforePush() As Boolean

        If CurrentDraft Is Nothing Then
            MessageBox.Show("لا توجد فاتورة حالية.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If CurrentDraft.AG_ID <= 0 Then
            MessageBox.Show("يجب اختيار العميل أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        If CurrentDraft.Items Is Nothing OrElse CurrentDraft.Items.Count = 0 Then
            MessageBox.Show("لا يمكن حفظ فاتورة بدون أصناف.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        For Each item As SaleDraftItem In CurrentDraft.Items
            If item.IM_ID <= 0 Then
                MessageBox.Show("يوجد صنف غير صحيح داخل الفاتورة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If item.U_ID <= 0 Then
                MessageBox.Show("يوجد سطر بدون وحدة صحيحة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If item.ST_ID <= 0 Then
                MessageBox.Show("يوجد سطر بدون مخزن صحيح.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If item.QTY <= 0 Then
                MessageBox.Show("يوجد سطر كميته أقل من أو تساوي صفر.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            If item.Price < 0 Then
                MessageBox.Show("يوجد سطر سعره غير صحيح.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
        Next

        Return True

    End Function

    Private Function PushCurrentDraftToDatabase() As Boolean

        If Not ValidateDraftBeforePush() Then Return False

        DraftCalculator.RecalculateDraft(CurrentDraft)
        UpdateDraftTotalsOnScreen()

        Dim useMultiplePayments As Boolean =
            SalesPaymentSqlLayer.CanUseDraftMultiplePayments(CurrentDraft.AG_ID)

        Dim F As New Pay_Main_Form
        F.Temp_Tr_ID = SB_TR_ID
        F.AG_ID = CurrentDraft.AG_ID
        F.MONEY_VALUE = CurrentDraft.Pure
        F.EnableMultiplePayments = useMultiplePayments
        F.Is_Force_Pay = useMultiplePayments
        F.ShowDialog()

        If F.is_OK = True Then
            Dim Tr_ID, Pay_ID As Integer
            Tr_ID = F.Tr_ID
            Pay_ID = F.Pay_ID
            Print_PaymentName = F.PaymentName
            If String.IsNullOrWhiteSpace(Print_PaymentName) Then Print_PaymentName = "نقدا"

            ' تاريخ الفاتورة النهائية هو وقت ترحيل المسودة، وليس وقت إنشائها محليًا.
            Dim finalBillDate As DateTime = DateTime.Now
            CurrentDraft.Date = finalBillDate
            DateTimeEx.Value = finalBillDate

            Dim detailsTable As DataTable = BuildDetailsTable(CurrentDraft.Items)

            Try
                Using con As New SqlConnection(MY_Settings.SqlConStr) ' عدّل اسم الاتصال عندك
                    Using cmd As New SqlCommand(If(useMultiplePayments, "dbo.PushSalesDraft_V2", "dbo.PushSalesDraft"), con)

                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandTimeout = 120

                        cmd.Parameters.Add("@AG_ID", SqlDbType.Int).Value = CurrentDraft.AG_ID

                        If CurrentDraft.S_Bill_Pr_ID.HasValue Then
                            cmd.Parameters.Add("@S_Bill_Pr_ID", SqlDbType.Int).Value = CurrentDraft.S_Bill_Pr_ID.Value
                        Else
                            cmd.Parameters.Add("@S_Bill_Pr_ID", SqlDbType.Int).Value = DBNull.Value
                        End If

                        If CurrentDraft.Table_ID.HasValue Then
                            cmd.Parameters.Add("@Table_ID", SqlDbType.Int).Value = CurrentDraft.Table_ID.Value
                        Else
                            cmd.Parameters.Add("@Table_ID", SqlDbType.Int).Value = DBNull.Value
                        End If

                        cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = finalBillDate
                        cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = CurrentDraft.Discount
                        cmd.Parameters("@Discount").Precision = 18
                        cmd.Parameters("@Discount").Scale = 3

                        cmd.Parameters.Add("@About", SqlDbType.NVarChar).Value =
                        If(String.IsNullOrWhiteSpace(CurrentDraft.About), CType(DBNull.Value, Object), CurrentDraft.About)

                        cmd.Parameters.Add("@BsType_ID", SqlDbType.Int).Value = CurrentDraft.BsType_ID
                        cmd.Parameters.Add("@isVoid", SqlDbType.Int).Value = CurrentDraft.isVoid

                        If CurrentDraft.isPied.HasValue Then
                            cmd.Parameters.Add("@isPied", SqlDbType.Int).Value = CurrentDraft.isPied.Value
                        Else
                            cmd.Parameters.Add("@isPied", SqlDbType.Int).Value = DBNull.Value
                        End If

                        cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = CurrentDraft.User_ID

                        If useMultiplePayments Then
                            SalesPaymentSqlLayer.AddPaymentsParameter(cmd, F.Payments)
                        Else
                            cmd.Parameters.Add("@Tr_ID", SqlDbType.Int).Value = Tr_ID
                            cmd.Parameters.Add("@Pay_ID", SqlDbType.Int).Value = Pay_ID
                        End If
                        cmd.Parameters.Add("@Pr_ID", SqlDbType.Int).Value = Pr_ID

                        If CurrentDraft.Markter_ID.HasValue Then
                            cmd.Parameters.Add("@Markter_ID", SqlDbType.Int).Value = CurrentDraft.Markter_ID.Value
                        Else
                            cmd.Parameters.Add("@Markter_ID", SqlDbType.Int).Value = DBNull.Value
                        End If

                        Dim pDetails As New SqlParameter("@Details", SqlDbType.Structured)
                        pDetails.TypeName = "dbo.SB_Contents_DraftType"
                        pDetails.Value = detailsTable
                        cmd.Parameters.Add(pDetails)

                        con.Open()

                        Using dr As SqlDataReader = cmd.ExecuteReader()
                            If dr.Read() Then

                                Dim isSuccess As Boolean = False

                                If Not IsDBNull(dr("IsSuccess")) Then
                                    isSuccess = Convert.ToBoolean(dr("IsSuccess"))
                                End If

                                If isSuccess Then
                                    If Not IsDBNull(dr("Header_T_ID")) Then
                                        CurrentDraft.Final_T_ID = Convert.ToInt32(dr("Header_T_ID"))
                                        T_ID = CurrentDraft.Final_T_ID
                                    End If

                                    If Not IsDBNull(dr("SB_ID")) Then
                                        CurrentDraft.Final_SB_ID = Convert.ToInt32(dr("SB_ID"))
                                        Bill_ID_Txt.Text = CurrentDraft.Final_SB_ID
                                    End If



                                    CurrentDraft.PushedAt = DateTime.Now
                                    Return True
                                Else
                                    Dim errMsg As String = "فشل ترحيل الفاتورة."
                                    If HasColumn(dr, "ErrorMessage") AndAlso Not IsDBNull(dr("ErrorMessage")) Then
                                        errMsg = dr("ErrorMessage").ToString()
                                    End If

                                    MessageBox.Show(errMsg, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Return False
                                End If
                            Else
                                MessageBox.Show("لم يتم استلام نتيجة من الإجراء.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Return False
                            End If
                        End Using
                    End Using
                End Using

            Catch ex As Exception
                MessageBox.Show(ex.Message, "خطأ أثناء الحفظ النهائي", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try

        End If

        Return False
    End Function

    Private Function HasColumn(reader As SqlDataReader, columnName As String) As Boolean
        For i As Integer = 0 To reader.FieldCount - 1
            If reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub TreasuryCard_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        rs.ResizeAllControls(Me)
        If ShortcutGroupPanel IsNot Nothing AndAlso ShortcutItemsPanel IsNot Nothing Then
            LayoutShortcutPanels()
            RenderShortcutGroupButtons()
            RenderShortcutItemsByGroup()
        End If
        LayoutDraftLogsPanel()
    End Sub

    Private Sub Tr_BankNum_TextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Total_TextBox.KeyPress
        Check_Only_Int(sender, e)
    End Sub

    Private Sub AGMetroGrid_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles dgvSales.MouseDoubleClick
        FormType = 1
        If dgvSales.RowsDefaultCellStyle.BackColor = Color.LightYellow And dgvSales.Rows.Count > 0 Then Change_IM_Details.ShowDialog()
    End Sub

    Private Sub dgvSales_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSales.CellClick

        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub

        dgvSales.CurrentCell = dgvSales.Rows(e.RowIndex).Cells(e.ColumnIndex)

    End Sub

    Private Sub EditSelectedDraftItemQtyByButton()

        If CurrentDraft Is Nothing Then Exit Sub
        If dgvSales.Rows.Count = 0 OrElse dgvSales.CurrentRow Is Nothing Then Exit Sub

        Dim qtyColumnName As String = ""
        If dgvSales.Columns.Contains("QTY_CL") Then
            qtyColumnName = "QTY_CL"
        ElseIf dgvSales.Columns.Contains("QTY") Then
            qtyColumnName = "QTY"
        Else
            MsgBox("لم يتم العثور على عمود الكمية", MsgBoxStyle.Exclamation, "تنبيه")
            Exit Sub
        End If

        Dim currentQty As Decimal = 1D
        Decimal.TryParse(Convert.ToString(dgvSales.CurrentRow.Cells(qtyColumnName).Value), currentQty)

        Dim selectedQty As Decimal
        If ShowTouchQuantityDialog(currentQty, selectedQty) = DialogResult.OK Then
            SetSelectedDraftItemQty(selectedQty, "QTY_Btn")
        End If

    End Sub

    Private Sub EditSelectedDraftItemPriceByButton()

        If U_SB_IM_Update = False Then
            MsgBox("لا تملك صلاحية تعديل سعر بيع الصنف", MsgBoxStyle.Exclamation, "تنبيه")
            Exit Sub
        End If

        If CurrentDraft Is Nothing Then Exit Sub
        If dgvSales.Rows.Count = 0 OrElse dgvSales.CurrentRow Is Nothing Then Exit Sub

        Dim priceColumnName As String = ""
        If dgvSales.Columns.Contains("Price_CL") Then
            priceColumnName = "Price_CL"
        ElseIf dgvSales.Columns.Contains("Price") Then
            priceColumnName = "Price"
        Else
            MsgBox("لم يتم العثور على عمود السعر", MsgBoxStyle.Exclamation, "تنبيه")
            Exit Sub
        End If

        Dim currentPrice As Decimal = 0D
        Decimal.TryParse(Convert.ToString(dgvSales.CurrentRow.Cells(priceColumnName).Value), currentPrice)

        Dim selectedPrice As Decimal
        If ShowTouchPriceDialog(currentPrice, selectedPrice) = DialogResult.OK Then
            SetSelectedDraftItemPrice(selectedPrice)
        End If

    End Sub

    Private Function IsQuantityColumn(column As DataGridViewColumn) As Boolean

        If column Is Nothing Then Return False

        Dim columnName As String = If(column.Name, "")
        Dim dataName As String = If(column.DataPropertyName, "")
        Dim headerText As String = If(column.HeaderText, "")

        Return columnName.Equals("QTY", StringComparison.OrdinalIgnoreCase) OrElse
               columnName.Equals("QTY_CL", StringComparison.OrdinalIgnoreCase) OrElse
               dataName.Equals("QTY", StringComparison.OrdinalIgnoreCase) OrElse
               headerText.Trim() = "الكمية" OrElse
               headerText.Trim() = "كمية"

    End Function

    Private Function ShowTouchQuantityDialog(currentQty As Decimal, ByRef selectedQty As Decimal) As DialogResult

        Return ShowTouchNumberDialog("إدخال الكمية",
                                     currentQty,
                                     selectedQty,
                                     False,
                                     "الكمية المدخلة غير صحيحة")

    End Function

    Private Function ShowTouchDiscountDialog(currentDiscount As Decimal, ByRef selectedDiscount As Decimal) As DialogResult

        Return ShowTouchNumberDialog("إدخال التخفيض",
                                     currentDiscount,
                                     selectedDiscount,
                                     True,
                                     "قيمة التخفيض غير صحيحة")

    End Function

    Private Function ShowTouchPriceDialog(currentPrice As Decimal, ByRef selectedPrice As Decimal) As DialogResult

        Return ShowTouchNumberDialog("إدخال سعر البيع",
                                     currentPrice,
                                     selectedPrice,
                                     False,
                                     "سعر البيع غير صحيح")

    End Function

    Private Function ShowTouchNumberDialog(title As String,
                                           currentValue As Decimal,
                                           ByRef selectedValue As Decimal,
                                           allowZero As Boolean,
                                           invalidMessage As String) As DialogResult

        selectedValue = currentValue

        Using frm As New Form()
            Dim resultValue As Decimal = currentValue

            frm.Text = title
            frm.StartPosition = FormStartPosition.CenterParent
            frm.FormBorderStyle = FormBorderStyle.FixedDialog
            frm.MaximizeBox = False
            frm.MinimizeBox = False
            frm.ShowInTaskbar = False
            frm.RightToLeft = RightToLeft.Yes
            frm.RightToLeftLayout = True
            frm.ClientSize = New Size(390, 520)
            frm.BackColor = Color.White

            Dim rootPanel As New TableLayoutPanel()
            rootPanel.Dock = DockStyle.Fill
            rootPanel.Padding = New Padding(10)
            rootPanel.ColumnCount = 1
            rootPanel.RowCount = 4
            rootPanel.BackColor = Color.White
            rootPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0!))
            rootPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 76.0!))
            rootPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))
            rootPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 66.0!))
            rootPanel.RowStyles.Add(New RowStyle(SizeType.Absolute, 76.0!))
            frm.Controls.Add(rootPanel)

            Dim display As New TextBox()
            display.ReadOnly = True
            display.TextAlign = HorizontalAlignment.Center
            display.Font = New Font("Segoe UI", 26.0!, FontStyle.Bold)
            display.Text = currentValue.ToString("0.###")
            display.Tag = True
            display.Dock = DockStyle.Fill
            display.Margin = New Padding(0, 0, 0, 8)
            display.BackColor = Color.White
            display.BorderStyle = BorderStyle.FixedSingle
            rootPanel.Controls.Add(display, 0, 0)

            Dim buttonsPanel As New TableLayoutPanel()
            buttonsPanel.Dock = DockStyle.Fill
            buttonsPanel.Margin = New Padding(0)
            buttonsPanel.ColumnCount = 3
            buttonsPanel.RowCount = 4
            buttonsPanel.BackColor = Color.White
            buttonsPanel.RightToLeft = RightToLeft.No

            For i As Integer = 1 To 3
                buttonsPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.333!))
            Next

            For i As Integer = 1 To 4
                buttonsPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 25.0!))
            Next

            rootPanel.Controls.Add(buttonsPanel, 0, 1)

            Dim toolsPanel As New TableLayoutPanel()
            toolsPanel.Dock = DockStyle.Fill
            toolsPanel.Margin = New Padding(0, 8, 0, 0)
            toolsPanel.ColumnCount = 2
            toolsPanel.RowCount = 1
            toolsPanel.BackColor = Color.White
            toolsPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0!))
            toolsPanel.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0!))
            toolsPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0!))
            rootPanel.Controls.Add(toolsPanel, 0, 2)

            Dim appendValue As Action(Of String) =
                Sub(value As String)
                    If display.Tag IsNot Nothing AndAlso CBool(display.Tag) = True Then
                        display.Text = ""
                        display.Tag = False
                    End If

                    If display.Text = "0" Then display.Text = ""
                    If value = "." AndAlso display.Text.Contains(".") Then Exit Sub
                    display.Text &= value
                End Sub

            Dim createButton As Func(Of String, Color, Color, Button) =
                Function(text As String, backColor As Color, foreColor As Color) As Button
                    Dim btn As New Button()
                    btn.Dock = DockStyle.Fill
                    btn.Margin = New Padding(4)
                    btn.Text = text
                    btn.Font = New Font("Segoe UI", 17.0!, FontStyle.Bold)
                    btn.BackColor = backColor
                    btn.ForeColor = foreColor
                    btn.FlatStyle = FlatStyle.Flat
                    btn.Cursor = Cursors.Hand
                    btn.UseVisualStyleBackColor = False

                    AddHandler btn.Click,
                        Sub()
                            Select Case text
                                Case "تأكيد"
                                    Dim parsedValue As Decimal
                                    If TryParseTouchNumber(display.Text, parsedValue) = False OrElse parsedValue < 0D OrElse (allowZero = False AndAlso parsedValue <= 0D) Then
                                        MsgBox(invalidMessage, MsgBoxStyle.Exclamation, "تنبيه")
                                        Exit Sub
                                    End If

                                    resultValue = parsedValue
                                    frm.DialogResult = DialogResult.OK
                                    frm.Close()

                                Case "تراجع"
                                    frm.DialogResult = DialogResult.Cancel
                                    frm.Close()

                                Case "مسح"
                                    display.Text = ""
                                    display.Tag = False

                                Case "حذف"
                                    display.Tag = False
                                    If display.Text.Length > 0 Then display.Text = display.Text.Substring(0, display.Text.Length - 1)

                                Case Else
                                    appendValue(text)
                            End Select
                        End Sub

                    Return btn
                End Function

            Dim addButton As Action(Of String, Integer, Integer, Color, Color) =
                Sub(text As String, row As Integer, col As Integer, backColor As Color, foreColor As Color)
                    Dim btn As Button = createButton(text, backColor, foreColor)
                    buttonsPanel.Controls.Add(btn, col, row)
                End Sub

            addButton("1", 0, 0, Color.WhiteSmoke, Color.Black)
            addButton("2", 0, 1, Color.WhiteSmoke, Color.Black)
            addButton("3", 0, 2, Color.WhiteSmoke, Color.Black)
            addButton("4", 1, 0, Color.WhiteSmoke, Color.Black)
            addButton("5", 1, 1, Color.WhiteSmoke, Color.Black)
            addButton("6", 1, 2, Color.WhiteSmoke, Color.Black)
            addButton("7", 2, 0, Color.WhiteSmoke, Color.Black)
            addButton("8", 2, 1, Color.WhiteSmoke, Color.Black)
            addButton("9", 2, 2, Color.WhiteSmoke, Color.Black)
            addButton(".", 3, 0, Color.Gainsboro, Color.Black)
            addButton("0", 3, 1, Color.WhiteSmoke, Color.Black)
            addButton("حذف", 3, 2, Color.Gainsboro, Color.Black)

            toolsPanel.Controls.Add(createButton("مسح", Color.LightGray, Color.Black), 0, 0)
            toolsPanel.Controls.Add(createButton("تراجع", Color.IndianRed, Color.White), 1, 0)

            Dim confirmButton As Button = createButton("تأكيد", Color.SeaGreen, Color.White)
            confirmButton.Font = New Font("Segoe UI", 20.0!, FontStyle.Bold)
            confirmButton.Margin = New Padding(0, 8, 0, 0)
            rootPanel.Controls.Add(confirmButton, 0, 3)

            AddHandler frm.Shown,
                Sub()
                    display.Focus()
                    display.SelectAll()
                End Sub

            Dim result As DialogResult = frm.ShowDialog(Me)
            If result = DialogResult.OK Then selectedValue = resultValue

            Return result
        End Using

    End Function

    Private Function TryParseTouchQuantity(value As String, ByRef quantity As Decimal) As Boolean

        Return TryParseTouchNumber(value, quantity)

    End Function

    Private Function TryParseTouchNumber(value As String, ByRef numberValue As Decimal) As Boolean

        value = If(value, "").Trim().Replace("٫", ".").Replace(",", ".")

        If String.IsNullOrWhiteSpace(value) Then Return False

        Return Decimal.TryParse(value, Globalization.NumberStyles.Number, Globalization.CultureInfo.InvariantCulture, numberValue) OrElse
               Decimal.TryParse(value, numberValue)

    End Function

    Public Sub Calc_Total()
        TOTAL = 0
        If String.IsNullOrWhiteSpace(Discount_txt.Text) Then
            Disc = 0
            Discount_txt.Text = "0"
        End If

        Dim QTY As Double = 0
        For i = 0 To dgvSales.Rows.Count - 1
            TOTAL = TOTAL + dgvSales.Rows(i).Cells("Total_CL").Value
            QTY += dgvSales.Rows(i).Cells("QTY_CL").Value
        Next

        Total_TextBox.Text = TOTAL '.ToString("N")

        Pure = (TOTAL - Disc)

        Pure_txt.Text = Pure '.ToString("N")
        IM_Count_LB.Text = dgvSales.Rows.Count.ToString + " : مواد "
        IM_Qty_LB.Text = QTY.ToString + " : كميات "

    End Sub

    Public Sub ADD_IM()

        '  If String.IsNullOrWhiteSpace(QtyTextBox.Text) Then QtyTextBox.Text = "1"
        QtyTextBox = 1

        If S_Allow_MinSP = True Then
            If User_isAdmin = False Then
                If U_Sell_Under_Min_SP = True Then
                    If IM_Price < Min_SP And Min_SP > 0 Then
                        '  My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                        System.Media.SystemSounds.Hand.Play()
                        MsgBox(" ( " + Min_SP.ToString + " ) لا يمكنك البيع بأقل من أدنى سعر بيع", MsgBoxStyle.Exclamation)
                        ClearCatFields()
                        Exit Sub
                    End If
                End If

            Else
                If IM_Price < Min_SP And Min_SP > 0 Then
                    System.Media.SystemSounds.Hand.Play()
                    '   My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                    If MessageBox.Show(" ( " + Min_SP.ToString + " ) سوف يتم البيع بأقل من أدنى سعر بيع", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation,
                                       MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
                        ClearCatFields()
                        Exit Sub
                    End If

                End If
            End If
        End If


        'If U_SB_Sell_Under_Cost = False Then
        '    If Show_IM_Cost(False, IM_ID, U_ID) > IM_Price.Text Then
        'System.Media.SystemSounds.Hand.Play()
        '        My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
        '        MsgBox("لا يمكنك البيع بأقل من سعر التكلفة", MsgBoxStyle.Critical)
        '        ClearCatFields()
        '        Exit Sub
        '    End If
        'Else

        '    If Show_IM_Cost(False, IM_ID, U_ID) > IM_Price.Text Then
        'System.Media.SystemSounds.Hand.Play()
        '        My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
        '        If MessageBox.Show(" سوف يتم البيع بأقل من سعر التكلفة", "تنويه", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, _
        '                                      MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
        '            ClearCatFields()
        '            Exit Sub
        '        End If
        '    End If
        'End If

        If IM_min_QTY = False Then

            If IM_Check_Neg_QTY_() = 1 Then
                ' If QTY_ALERT_SOUND = True Then My.Computer.Audio.Play(Application.StartupPath & "\QTY ALERT.wav")
                If IM_min_QTY = False Then
                    System.Media.SystemSounds.Hand.Play()
                    '    My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                    MsgBox("لا يمكنك إدراج صنف بكمية سالبة", MsgBoxStyle.Critical)
                    ClearCatFields()
                    Exit Sub
                End If
            End If

        End If

        'If SB_IM_Alert_When_Repetition = True Then
        '    For i = 0 To dgvSales.Rows.Count - 1
        '        If dgvSales.Rows(i).Cells("Bill_IMID_CL").Value = IM_ID Then
        '            System.Media.SystemSounds.Hand.Play()
        '            '  My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
        '            If MessageBox.Show(" هذا الصنف تم إدراجه بالفاتورة ... هل تريد الإستمرار ؟ ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
        '                ClearCatFields()
        '                Exit Sub
        '            Else
        '                Add_ItemToBill(IM_ID)
        '                Exit Sub
        '            End If
        '        End If
        '    Next
        'End If

        Beep()
        If Notif_If_SB_Has_No_SB_Price = True Then
            If IM_Price = 0 Then
                System.Media.SystemSounds.Hand.Play()
                '  My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                If MessageBox.Show(" لم يتم تحديد سعر بيع للصنف أوسعره = 0 ... هل تريد الإستمرار فالبيع ", "",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
                    ClearCatFields()
                    Exit Sub
                End If
            End If
        End If

        Dim cachedItem As New CachedSaleItem With {
        .ItemId = Convert.ToInt32(IM_ID),
        .ItemName = IM_Name.ToString(),
        .Barcode = Barcode_IM,
        .SellPrice = Convert.ToDecimal(IM_Price),
        .UnitId = Convert.ToInt64(IM_U_ID),
        .UnitName = IM_Unit_Name.ToString(),
        .StoreId = Convert.ToInt64(SB_ST_ID),
        .Equal = Convert.ToDouble(U_Cargo),
        .Cost = Convert.ToDouble(IM_Cost)
    }

        'Add_ItemToBill(IM_ID)


        AddItemFromCache(cachedItem)

    End Sub


    Private Sub AddItemFromCache(cachedItem As CachedSaleItem)
        EnsureDraftExists()

        If cachedItem Is Nothing Then Exit Sub

        Dim item As New SaleDraftItem With {
        .IM_ID = cachedItem.ItemId,
        .ItemName = cachedItem.ItemName,
        .Barcode = cachedItem.Barcode,
        .Price = cachedItem.SellPrice,
        .QTY = 1D,
        .U_ID = cachedItem.UnitId,
        .UnitName = cachedItem.UnitName,
        .ST_ID = cachedItem.StoreId,
        .U_Cargo = cachedItem.Equal,
        .Cost = cachedItem.Cost,
        .Compons = "",
        .D_Vaild = "",
        .Date_ = DateTime.Now
    }

        DraftItemService.AddItem(CurrentDraft, item)

        DraftManager.SaveDraft(CurrentDraft)
        LoadDraftToGrid()
        UpdateDraftTotalsOnScreen()
        AddDraftLog("إضافة",
                    "تمت إضافة صنف إلى المسودة",
                    "ADD_IM",
                    "",
                    item.ItemName,
                    item.ItemName,
                    item.IM_ID,
                    item.QTY,
                    item.T_Price)

    End Sub

    Private Function BuildDetailsTable(items As List(Of SaleDraftItem)) As DataTable
        Dim dt As New DataTable()

        dt.Columns.Add("IM_ID", GetType(Integer))
        dt.Columns.Add("U_ID", GetType(Long))
        dt.Columns.Add("ST_ID", GetType(Long))
        dt.Columns.Add("Date", GetType(DateTime))
        dt.Columns.Add("Compons", GetType(String))
        dt.Columns.Add("Cost", GetType(Double))
        dt.Columns.Add("Price", GetType(Decimal))
        dt.Columns.Add("D_Vaild", GetType(String))
        dt.Columns.Add("QTY", GetType(Decimal))
        dt.Columns.Add("T_Price", GetType(Decimal))
        dt.Columns.Add("U_Cargo", GetType(Double))
        dt.Columns.Add("ST_QTY", GetType(Decimal))
        dt.Columns.Add("isDepended", GetType(Integer))
        dt.Columns.Add("Barcode", GetType(String))

        For Each item In items
            dt.Rows.Add(
            item.IM_ID,
            item.U_ID,
            item.ST_ID,
            item.Date_,
            If(item.Compons, ""),
            If(item.Cost.HasValue, item.Cost.Value, CType(DBNull.Value, Object)),
            item.Price,
            If(item.D_Vaild, ""),
            item.QTY,
            item.T_Price,
            item.U_Cargo,
            item.ST_QTY,
            0,
            item.Barcode
        )
        Next

        Return dt
    End Function

    Public Function IM_Check_Neg_QTY_()
        Dim C As New C
        Dim F As Integer = 0
        With C.Com
            .Connection = C.Con
            .CommandText = "IM_Check_Neg_QTY_"
            .CommandType = CommandType.StoredProcedure
            .Parameters.AddWithValue("@F", 0)
            .Parameters.AddWithValue("@ST_ID", SB_ST_ID)
            .Parameters.AddWithValue("@IM_ID", IM_ID)
            .Parameters.AddWithValue("@D_Vaild", Valid_TXT)
            .Parameters.AddWithValue("@Enterd_Qty", QtyTextBox)
            .Parameters.AddWithValue("@Cargo", U_Cargo)

            .Parameters("@F").Direction = ParameterDirection.Output
            If SQL_SP_EXEC(C.Com) Then F = .Parameters("@F").Value
        End With

        Return F
    End Function


    Private Sub ClearCatFields()
        QtyTextBox = 1
        IM_ID = 0
        ' Current_QTY.Clear()
        IM_Price = 0
        'QtyTextBox.Clear()
        U_Dt.Clear()
        '  Valid_QTY_txt.Clear()
        '   Valid_Dt.Clear()
        Barcode_SH_txt.Clear()
        Barcode_SH_txt.Select()
        Barcode_IM = ""
        is_Valid = False
        Bercent_Price = 0
        Valid_TXT = ""
    End Sub

    Private Sub RemoveCatButton_Click(sender As Object, e As EventArgs) Handles RemoveCatButton.Click
        'If dgvSales.Rows.Count > 0 Then
        '    SB_Contents_Delete_IM(dgvSales.CurrentRow.Cells("T_ID_CL").Value)
        'End If


        If CurrentDraft Is Nothing Then Exit Sub
        If dgvSales.CurrentRow Is Nothing Then Exit Sub

        Dim draftLineId As String = dgvSales.CurrentRow.Cells("DraftLineId").Value.ToString()
        Dim item As SaleDraftItem =
            CurrentDraft.Items.FirstOrDefault(Function(x) x.DraftLineId = draftLineId)

        DraftItemService.RemoveItem(CurrentDraft, draftLineId)

        DraftManager.SaveDraft(CurrentDraft)
        LoadDraftToGrid()
        UpdateDraftTotalsOnScreen()

        If item IsNot Nothing Then
            AddDraftLog("حذف",
                        "تم حذف صنف من المسودة",
                        "RemoveCatButton",
                        item.ItemName,
                        "",
                        item.ItemName,
                        item.IM_ID,
                        item.QTY,
                        item.T_Price)
        Else
            AddDraftLog("حذف", "تم حذف سطر من المسودة", "RemoveCatButton")
        End If
    End Sub

    Private Sub ClearCurrentDraftItems()

        If CurrentDraft Is Nothing Then Exit Sub
        If CurrentDraft.Items Is Nothing Then CurrentDraft.Items = New List(Of SaleDraftItem)()

        If CurrentDraft.Items.Count = 0 Then
            MsgBox("لا توجد أصناف داخل المسودة الحالية", MsgBoxStyle.Information, "تنبيه")
            Exit Sub
        End If

        If MessageBox.Show("سيتم مسح كل أصناف المسودة الحالية، هل تريد المتابعة؟",
                           "تأكيد مسح الأصناف",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning,
                           MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then Exit Sub

        Dim oldItemsCount As Integer = CurrentDraft.Items.Count
        Dim oldTotal As Decimal = CurrentDraft.Total

        CurrentDraft.Items.Clear()
        DraftCalculator.RecalculateDraft(CurrentDraft)
        DraftManager.SaveDraft(CurrentDraft)

        LoadDraftToGrid()
        UpdateDraftTotalsOnScreen()
        AddDraftLog("حذف",
                    "تم مسح كل أصناف المسودة",
                    "ClearDraftItemsButton",
                    oldItemsCount.ToString("N0") & " صنف / " & oldTotal.ToString("0.###"),
                    "0 صنف / 0")

    End Sub

    Private Sub EnsureDraftExists()

        If CurrentDraft Is Nothing Then
            CurrentDraft = CreateOrReuseEmptyDraftForNewBill()
            BindDraftHeaderToForm()
            LoadDraftToGrid()
            UpdateDraftTotalsOnScreen()
        End If

    End Sub


    Private Sub Fetch_IM_Units()
        Get_Unit = False
        Dim c As New C
        U_Dt.Clear()
        Try
            Dim s As String
            s = "select U_IM_ID,U_Name from IM_Menu_Units_V  WHERE IM_ID = '" & IM_ID & "' Order By U_Cargo Asc"
            c.Da = New SqlClient.SqlDataAdapter(s, c.Con)
            c.Da.Fill(U_Dt)
            IM_Unit_cm.DataSource = U_Dt
            IM_Unit_cm.DisplayMember = "U_Name"
            IM_Unit_cm.ValueMember = "U_IM_ID"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Get_Unit = True
        IM_Fetch_QTY()
    End Sub

    Private Sub Barcode_SH_txt_KeyDown(sender As Object, e As KeyEventArgs) Handles Barcode_SH_txt.KeyDown
        Select Case e.KeyCode
            Case Keys.Return
                If String.IsNullOrWhiteSpace(Barcode_SH_txt.Text) = False Then Load_IM_Barcode()
                Clear_Barcode()
         '   Case Keys.Down : QtyTextBox.Select()
            Case Keys.Delete
                Clear_Barcode()
        End Select
    End Sub

    Private Sub Clear_Barcode()
        Barcode_SH_txt.Clear()
        Barcode_IM = ""
    End Sub

    Public Sub Load_IM_Barcode()

        IM_Dt.Clear()
        Dim rows As DataRow() = IM_Units_Dt.Select("Barcode = '" & Barcode_SH_txt.Text & "' ")
        If rows.Length > 0 Then

            Dim row As DataRow = rows(0)

            IM_ID = Convert.ToInt32(row("IM_ID"))
            IM_Name = row("item_name").ToString
            Barcode_IM = Barcode_SH_txt.Text
            U_IM_ID = Convert.ToInt32(row("U_IM_ID"))
            IM_Unit_Name = row("U_name").ToString
            Get_Unit = False

            Fetch_IM_Units_By_Bar()
            Barcode_SH_txt.Clear()
            ADD_IM()
        Else

            If Barcode_SH_txt.Text.Count >= 8 Then
                Check_If_Mizan()
            Else
                System.Media.SystemSounds.Hand.Play()

                '   My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                MessageBox.Show("لم يتم التعرف على الإدخال")
                Clear_Barcode()
            End If


        End If

    End Sub


    Public Sub Load_IM_By_ID()

        IM_Dt.Clear()
        Dim rows As DataRow() = IM_Units_Dt.Select("IM_ID = '" & IM_ID & "' ")
        If rows.Length > 0 Then

            Dim row As DataRow = rows(0)

            'IM_ID = Convert.ToInt32(row("IM_ID"))
            IM_Name = row("item_name").ToString
            Barcode_IM = row("Barcode").ToString
            U_IM_ID = Convert.ToInt32(row("U_IM_ID"))
            IM_Unit_Name = row("U_name").ToString
            Get_Unit = False

            Fetch_IM_Units_By_Bar()
            Barcode_SH_txt.Clear()
            ADD_IM()
        Else

            If Barcode_SH_txt.Text.Count >= 8 Then
                Check_If_Mizan()
            Else
                '   My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                System.Media.SystemSounds.Hand.Play()
                MessageBox.Show("لم يتم التعرف على الإدخال")
                Clear_Barcode()
            End If

        End If

    End Sub

    Private Sub Fetch_IM_Units_By_Bar()
        Get_Unit = False
        'Dim c As New C
        U_Dt.Clear()
        Try

            Dim rootRows() As DataRow = IM_Units_Dt.Select("Barcode = '" & Barcode_IM & "'")
            If rootRows.Length > 0 Then
                U_Dt = rootRows.CopyToDataTable()
            End If
            IM_Unit_cm.DataSource = U_Dt
            IM_Unit_cm.DisplayMember = "U_Name"
            IM_Unit_cm.ValueMember = "U_IM_ID"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Get_Unit = True
        IM_Fetch_QTY()

    End Sub


    Private Sub Check_If_Mizan()
        Dim c As New C
        Dim New_Barcode As String = ""
        Dim Qty As Double = 0
        Dim Qty_Dot As String = ""
        Dim Price As Double = 0
        Dim Price_Dot As String = ""
        Dim T_Price As Double = 0
        Dim T_Price_Dot As String = ""
        'QtyTextBox = 0

        Try

            For i = Mizan_BarcodeFrom - 1 To Mizan_BarcodeTo - 1
                New_Barcode += Barcode_SH_txt.Text(i)
            Next

            Dim S As String = "Select U_IM_ID,IM_ID,item_name,isValid,Price from IM_units_Search_V WHERE Barcode = '" & New_Barcode & "'"
            c.Com = New SqlClient.SqlCommand(S, c.Con)
            c.Con.Open()

            c.Dr = c.Com.ExecuteReader
            If c.Dr.HasRows Then
                c.Dr.Read()


                IM_ID = c.Dr("IM_ID")
                IM_Name = c.Dr("item_name")
                Barcode_IM = New_Barcode
                Get_Unit = False
                '  Load_IM_ST_QTY_ST_INT(IM_ID, SB_ST_ID, IM_QTY)


                If Second_Part_isPrice = 0 Then
                    For i = Mizan_QtyFrom - 1 To Mizan_QtyTo - 1
                        Qty_Dot += Barcode_SH_txt.Text(i)
                    Next
                    QtyTextBox = Convert.ToDouble(Qty_Dot) / 1000
                Else

                    For i = Mizan_QtyFrom - 1 To Mizan_QtyTo - 1
                        Qty_Dot += Barcode_SH_txt.Text(i)
                    Next
                    Qty = Qty_Dot(0) & Qty_Dot(1)
                    Qty_Dot = "0" & "." & Qty_Dot(2) & Qty_Dot(3) & Qty_Dot(4)
                    Qty = Qty + Convert.ToDouble(Qty_Dot)
                    QtyTextBox = Qty

                    '----------------------------------------------------------------------------

                    For j = Mizan_BarcodeTo To Mizan_QtyFrom - 1
                        T_Price_Dot += Barcode_SH_txt.Text(j)
                    Next
                    T_Price = T_Price_Dot(0) & T_Price_Dot(1) & T_Price_Dot(2)
                    T_Price_Dot = "0" & "." & T_Price_Dot(3) & T_Price_Dot(4)
                    T_Price = T_Price + Convert.ToDouble(T_Price_Dot)
                    '-------------------------------------------------------------------------------
                    IM_Price = Convert.ToDouble(T_Price) / Qty
                End If

                Fetch_IM_Units()
                IM_Unit_cm.SelectedValue = c.Dr("U_IM_ID")
                Barcode_SH_txt.Clear()
                'Load_IM_Change_Price()

                If c.Dr("isValid") = 1 Then
                    is_Valid = True
                    POS_D_Valid.ST_ID = SB_ST_ID
                    POS_D_Valid.IM_ID = IM_ID
                    POS_D_Valid.ShowDialog()
                    If Valid_Allow_IM = False Then Exit Sub
                End If

                ADD_IM()
            Else
                ' My.Computer.Audio.Play(Application.StartupPath & "\Alert Beep.wav")
                System.Media.SystemSounds.Hand.Play()
                MsgBox("لم يتم التعرف على الإدخال")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub IM_Fetch_QTY()

        'Dim c As New C
        Try

            '------------------------------------------------------------------------------------------------------------------------------------
            Dim rows As DataRow() = IM_Units_Dt.Select("U_IM_ID = " & IM_Unit_cm.SelectedValue)
            If rows.Length > 0 Then

                Dim row As DataRow = rows(0)

                U_Cargo = Convert.ToDouble(row("U_Cargo"))
                ' Dim N As Double = (Convert.ToDouble(IM_QTY) / U_Cargo)
                '   Current_QTY.Text = N.ToString("N")
                IM_Price = row("Price").ToString
                '  ALL_QTY_txt.Text = ALL_QTY / U_Cargo
                IM_U_ID = row("U_ID").ToString

                Min_SP = row("Min_SP").ToString

                Bercent_Price = row("Percent_Price").ToString

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Bill_ID_Txt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Bill_ID_Txt.KeyPress
        Check_Only_Int(sender, e)
    End Sub


    Private Sub Show_AG_Balance()
        F_Balances = New Balances
        With F_Balances
            .AG_ID = AG_ID
            .AG_Cm.Set_IM_By_ID(AG_ID)
            .Load_Data()
            .AllAgentsCheckBox.Enabled = False
            .AllRecieptsCheckBox.Checked = True
            .AllUsersCheckBox.Checked = True
            .AllTimeCheckBox.Checked = True
            .AG_MV_Prepare_To_Search()
            .ن.TabPages.Remove(.MetroTabPage2)
            .ن.TabPages.Remove(.MetroTabPage3)
            .ن.TabPages.Remove(.MetroTabPage4)
            '  .MetroTabControl1.TabPages.Remove(.MetroTabPage5)
            .MenuStrip1.Visible = False
        End With
        F_Balances.ShowDialog()
    End Sub


    Private Sub Print_btn_Click(sender As Object, e As EventArgs) Handles Print_btn.Click
        Prepare_to_print()
    End Sub

    Private Sub Prepare_to_print()
        If dgvSales.Rows.Count > 0 Then
            Try
                Me.Cursor = Cursors.AppStarting
                AddDraftLog("طباعة", "بدء طباعة الفاتورة الحالية", "Print_btn")
                PrintCurrentBill()
                If ForceDraftPrintPreview Then
                    AddDraftLog("طباعة", "تم عرض معاينة الفاتورة الحالية", "Print_btn")
                Else
                    AddDraftLog("طباعة", "تم إرسال الفاتورة الحالية للطابعة", "Print_btn")
                End If
            Catch ex As Exception
                AddDraftLog("طباعة", "فشل طباعة الفاتورة: " & ex.Message, "Print_btn")
                MsgBox(ex.Message, MsgBoxStyle.Critical, "خطأ في الطباعة")
            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            AddDraftLog("طباعة", "محاولة طباعة بدون أصناف", "Print_btn")
        End If
    End Sub




    Private Sub New_butt_Click(sender As Object, e As EventArgs) Handles New_butt.Click

        AddDraftLog("فتح", "طلب فتح فاتورة مسودة جديدة", "New_butt")

        If CanOpenNewDraftBill() = False Then Exit Sub

        ResetNewBill()

    End Sub

    Private Function CanOpenNewDraftBill() As Boolean

        If HasItemsInSalesGrid() = False Then Return True

        If ShowSendCurrentBillToDraftDialog() = False Then
            AddDraftLog("فتح", "تم إلغاء فتح فاتورة جديدة والبقاء على الحالية", "New_butt")
            Return False
        End If

        Try
            dgvSales.EndEdit()

            If CurrentDraft IsNot Nothing Then
                DraftCalculator.RecalculateDraft(CurrentDraft)
                DraftManager.SaveDraft(CurrentDraft)
                AddDraftLog("حفظ", "تم حفظ الفاتورة الحالية كمسودة قبل فتح جديد", "New_butt", "", CurrentDraft.DraftId)
            End If
        Catch ex As Exception
            AddDraftLog("حفظ", "تعذر حفظ الفاتورة الحالية كمسودة: " & ex.Message, "New_butt")
            MessageBox.Show("تعذر حفظ الفاتورة الحالية كمسودة:" & Environment.NewLine & ex.Message,
                            "تنبيه",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            Return False
        End Try

        Return True

    End Function

    Private Function HasItemsInSalesGrid() As Boolean

        For Each row As DataGridViewRow In dgvSales.Rows
            If row.IsNewRow = False Then Return True
        Next

        Return False

    End Function

    Private Function ShowSendCurrentBillToDraftDialog() As Boolean

        Using frm As New Form()
            frm.Text = "فتح فاتورة جديدة"
            frm.StartPosition = FormStartPosition.CenterParent
            frm.FormBorderStyle = FormBorderStyle.FixedDialog
            frm.MaximizeBox = False
            frm.MinimizeBox = False
            frm.ShowInTaskbar = False
            frm.RightToLeft = Windows.Forms.RightToLeft.Yes
            frm.RightToLeftLayout = True
            frm.ClientSize = New Size(540, 230)
            frm.BackColor = Color.White

            Dim titleLabel As New Label()
            titleLabel.Text = "الفاتورة الحالية تحتوي على أصناف"
            titleLabel.Font = New Font("Segoe UI Semibold", 14.0!, FontStyle.Bold)
            titleLabel.ForeColor = Color.FromArgb(30, 64, 175)
            titleLabel.TextAlign = ContentAlignment.MiddleRight
            titleLabel.Location = New Point(20, 18)
            titleLabel.Size = New Size(500, 36)

            Dim messageLabel As New Label()
            messageLabel.Text = "هل تريد إرسال الفاتورة الحالية إلى المسودة وفتح فاتورة جديدة؟"
            messageLabel.Font = New Font("Segoe UI Semibold", 11.0!, FontStyle.Bold)
            messageLabel.ForeColor = Color.FromArgb(51, 65, 85)
            messageLabel.TextAlign = ContentAlignment.MiddleRight
            messageLabel.Location = New Point(20, 66)
            messageLabel.Size = New Size(500, 52)

            Dim yesButton As New Button()
            yesButton.Text = "📝 إرسال للمسودة" & Environment.NewLine & "وفتح جديد"
            yesButton.Font = New Font("Segoe UI Semibold", 10.5!, FontStyle.Bold)
            yesButton.BackColor = Color.FromArgb(37, 99, 235)
            yesButton.ForeColor = Color.White
            yesButton.FlatStyle = FlatStyle.Flat
            yesButton.FlatAppearance.BorderSize = 0
            yesButton.Size = New Size(230, 64)
            yesButton.Location = New Point(286, 140)
            yesButton.DialogResult = DialogResult.Yes
            yesButton.UseVisualStyleBackColor = False

            Dim noButton As New Button()
            noButton.Text = "البقاء على" & Environment.NewLine & "الفاتورة الحالية"
            noButton.Font = New Font("Segoe UI Semibold", 10.5!, FontStyle.Bold)
            noButton.BackColor = Color.FromArgb(100, 116, 139)
            noButton.ForeColor = Color.White
            noButton.FlatStyle = FlatStyle.Flat
            noButton.FlatAppearance.BorderSize = 0
            noButton.Size = New Size(230, 64)
            noButton.Location = New Point(24, 140)
            noButton.DialogResult = DialogResult.No
            noButton.UseVisualStyleBackColor = False

            frm.Controls.Add(titleLabel)
            frm.Controls.Add(messageLabel)
            frm.Controls.Add(yesButton)
            frm.Controls.Add(noButton)
            frm.AcceptButton = yesButton
            frm.CancelButton = noButton

            Return frm.ShowDialog(Me) = DialogResult.Yes
        End Using

    End Function

    Private Sub PreviousBillsButton_Click(sender As Object, e As EventArgs) Handles PreviousBillsButton.Click
        AddDraftLog("زر", "فتح شاشة مراجعة الفواتير السابقة", "PreviousBillsButton")
        Sales_Fast.OpenPreviousBillsReviewMode()
    End Sub


    Private Sub LoadDraftToGrid()



        If CurrentDraft Is Nothing Then Exit Sub

        Dim dt As New DataTable()

        dt.Columns.Add("DraftLineId", GetType(String))
        dt.Columns.Add("IM_ID", GetType(Integer))
        dt.Columns.Add("Barcode", GetType(String))
        dt.Columns.Add("ItemName", GetType(String))
        dt.Columns.Add("UnitName", GetType(String))
        dt.Columns.Add("QTY", GetType(Decimal))
        dt.Columns.Add("Price", GetType(Decimal))
        dt.Columns.Add("T_Price", GetType(Decimal))
        dt.Columns.Add("U_ID", GetType(Long))
        dt.Columns.Add("ST_ID", GetType(Long))
        dt.Columns.Add("Cost", GetType(Double))
        dt.Columns.Add("U_Cargo", GetType(Double))
        dt.Columns.Add("ST_QTY", GetType(Decimal))
        dt.Columns.Add("Compons", GetType(String))
        dt.Columns.Add("D_Vaild", GetType(String))

        For Each item As SaleDraftItem In CurrentDraft.Items
            dt.Rows.Add(
            item.DraftLineId,
            item.IM_ID,
            item.Barcode,
            item.ItemName,
             item.UnitName,
            item.QTY,
             item.Price,
            item.T_Price,
            item.U_ID,
            item.ST_ID,
            If(item.Cost.HasValue, item.Cost.Value, 0),
            item.U_Cargo,
            item.ST_QTY,
            item.Compons,
            item.D_Vaild
        )
        Next

        dgvSales.DataSource = dt

        If dgvSales.Columns.Contains("DraftLineId") Then
            dgvSales.Columns("DraftLineId").Visible = False
        End If

        FormatSalesGrid()

        UcGridColumnsSelector1.BindGrid(
dgvSales,
New List(Of String) From {"DraftLineId", "IM_ID", "U_ID", "ST_ID", "Date_", "D_Valid_CL", "Serial_Code_CL", "Notes_CL", "Cost", "U_Cargo", "ST_QTY", "D_Vaild", "Compons"},
Me.Name.ToString
 )

        If dgvSales.Rows.Count > 0 Then
            Dim lastRowIndex As Integer
            lastRowIndex = dgvSales.Rows.Count - 1
            dgvSales.CurrentCell = dgvSales.Rows(lastRowIndex).Cells("Item_Name")
        End If

    End Sub

    Private Sub FormatSalesGrid()

        If dgvSales.DataSource Is Nothing Then Exit Sub

        With dgvSales

            ' إخفاء الأعمدة التقنية
            If .Columns.Contains("DraftLineId") Then .Columns("DraftLineId").Visible = False
            If .Columns.Contains("IM_ID") Then .Columns("IM_ID").Visible = False
            If .Columns.Contains("U_ID") Then .Columns("U_ID").Visible = False
            If .Columns.Contains("ST_ID") Then .Columns("ST_ID").Visible = False
            If .Columns.Contains("Date_") Then .Columns("Date_").Visible = False
            If .Columns.Contains("D_Valid_CL") Then .Columns("D_Valid_CL").Visible = False
            If .Columns.Contains("Serial_Code_CL") Then .Columns("Serial_Code_CL").Visible = False
            If .Columns.Contains("Notes_CL") Then .Columns("Notes_CL").Visible = False


            ' أعمدة تريد إخفاءها مؤقتًا
            If .Columns.Contains("Cost") Then .Columns("Cost").Visible = False
            If .Columns.Contains("U_Cargo") Then .Columns("U_Cargo").Visible = False
            If .Columns.Contains("ST_QTY") Then .Columns("ST_QTY").Visible = False
            If .Columns.Contains("D_Vaild") Then .Columns("D_Vaild").Visible = False
            If .Columns.Contains("Compons") Then .Columns("Compons").Visible = False

            ' أعمدة العرض الأساسية
            If .Columns.Contains("ItemName") Then
                .Columns("ItemName").HeaderText = "الصنف"
                .Columns("ItemName").Width = 220
            End If

            If .Columns.Contains("Barcode") Then
                .Columns("Barcode").HeaderText = "الباركود"
                .Columns("Barcode").Width = 120
            End If

            If .Columns.Contains("UnitName") Then
                .Columns("UnitName").HeaderText = "الوحدة"
                .Columns("UnitName").Width = 90
            End If

            If .Columns.Contains("QTY") Then
                .Columns("QTY").HeaderText = "الكمية"
                .Columns("QTY").Width = 80
                .Columns("QTY").DefaultCellStyle.Format = "#,##0.###"
            End If

            If .Columns.Contains("QTY_CL") Then
                .Columns("QTY_CL").HeaderText = "الكمية"
                .Columns("QTY_CL").Width = 80
                .Columns("QTY_CL").DefaultCellStyle.Format = "#,##0.###"
            End If

            If .Columns.Contains("Price") Then
                .Columns("Price").HeaderText = "السعر"
                .Columns("Price").Width = 90
                .Columns("Price").DefaultCellStyle.Format = "#,##0.###"
            End If

            If .Columns.Contains("Price_CL") Then
                .Columns("Price_CL").HeaderText = "السعر"
                .Columns("Price_CL").Width = 90
                .Columns("Price_CL").DefaultCellStyle.Format = "#,##0.###"
            End If

            If .Columns.Contains("T_Price") Then
                .Columns("T_Price").HeaderText = "الإجمالي"
                .Columns("T_Price").Width = 100
                .Columns("T_Price").DefaultCellStyle.Format = "#,##0.###"
                .Columns("T_Price").ReadOnly = True
            End If

            If .Columns.Contains("Total_CL") Then
                .Columns("Total_CL").HeaderText = "الإجمالي"
                .Columns("Total_CL").Width = 100
                .Columns("Total_CL").DefaultCellStyle.Format = "#,##0.###"
                .Columns("Total_CL").ReadOnly = True
            End If

        End With

    End Sub

    Private Sub OpenCahDR_Btn_Click(sender As Object, e As EventArgs) Handles OpenCahDR_Btn.Click
        AddDraftLog("زر", "فتح درج النقد", "OpenCahDR_Btn")
        Open_Cash_Drawer()
    End Sub

    Private Sub Show_Cash_btn_Click(sender As Object, e As EventArgs) Handles Show_Cash_btn.Click
        AddDraftLog("زر", "عرض المقبوض", "Show_Cash_btn")
        Fetch_Pr_Details_()
    End Sub

    Private Sub ExitFormButton_Click(sender As Object, e As EventArgs) Handles ExitFormButton.Click
        AddDraftLog("إغلاق", "طلب إغلاق شاشة المبيعات المسودة", "ExitFormButton")
        Me.Close()
    End Sub

    Private Sub Bill_ID_Txt_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Bill_ID_Txt.MouseDoubleClick

    End Sub

    Private Sub Discount_txt_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Discount_txt.MouseDoubleClick
        AddDraftLog("زر", "فتح تعديل الخصم من حقل الخصم", "Discount_txt")
        Make_Discount()
    End Sub

    Private Sub Make_Discount()

        If dgvSales.RowsDefaultCellStyle.BackColor = Color.LightYellow And dgvSales.Rows.Count > 0 Then
            Dim F As New Fast_SB_Discount
            F.is_By_Draft = True
            Identifiers.T_ID = T_ID
            Identifiers.TOTAL = Total_TextBox.Text
            Identifiers.Disc = Disc
            Identifiers.Pure = Pure_txt.Text
            Identifiers.SB_ID = SB_ID

            Fast_SB_Discount.ShowDialog()

            If F.is_OK = True Then SetDraftDiscount(Identifiers.Disc)

        End If

    End Sub

    Private Sub AG_SH_txt_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles AG_SH_txt.MouseDoubleClick
        AddDraftLog("زر", "فتح اختيار العميل من حقل العميل", "AG_SH_txt")
        SelectDraftCustomer()
    End Sub

    Private Sub ChangeCustomerButton_Click(sender As Object, e As EventArgs) Handles ChangeCustomerButton.Click
        AddDraftLog("زر", "فتح اختيار العميل", "ChangeCustomerButton")
        SelectDraftCustomer()
    End Sub

    Private Sub SelectDraftCustomer()

        Dim f As New AgentsMenu
        f.is_By_Draft = True
        f.ShowDialog()
        If f.is_OK = True Then ChangeDraftCustomer(f.AG_ID, f.AG_NAME)

    End Sub

    Private Sub AGMetroGrid_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvSales.KeyDown
        If e.KeyCode = Keys.Delete Then
            If dgvSales.Rows.Count > 0 And dgvSales.RowsDefaultCellStyle.BackColor = Color.LightYellow Then
                If MessageBox.Show(" حذف الصنف " + dgvSales.CurrentRow.Cells("EX_Name_CL").Value, "تأكيد", MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.OK Then
                    'SB_Contents_Delete_IM(dgvSales.CurrentRow.Cells("T_ID_CL").Value)

                    RemoveCatButton.PerformClick()
                End If
            End If
        End If

    End Sub

    'Private Sub Notes_txt_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles txtNotes.MouseDoubleClick
    '    F_BillNotes = New BillNotes
    '    F_BillNotes.T_ID = T_ID
    '    F_BillNotes.ShowDialog()

    '    'Notes_txt.Text = F_BillNotes.Notes_txt.Text
    'End Sub


    'Private Sub IM_Profet_btn_Click(sender As Object, e As EventArgs)
    '    Bill_Perfet_Select_For_Bill(T_ID)
    'End Sub

    Private Sub IM_Search_btn_Click(sender As Object, e As EventArgs) Handles IM_Search_btn.Click

        AddDraftLog("زر", "فتح شاشة بحث الأصناف", "IM_Search_btn")

        Dim f As Items_Search = Items_Search.GetInstance()

        f.ShowDialog()
        f.BringToFront()
        f.WindowState = FormWindowState.Normal

        If GLOBAL_IM_ID > 0 Then
            IM_ID = GLOBAL_IM_ID
            Load_IM_By_ID()
            AddDraftLog("زر", "تم اختيار صنف من شاشة البحث", "IM_Search_btn", "", GLOBAL_IM_ID.ToString())
            'SelectItemById(GLOBAL_IM_ID) 
        End If

    End Sub


    Private Sub CALC_Btn_Click(sender As Object, e As EventArgs) Handles CALC_Btn.Click
        AddDraftLog("زر", "فتح الحاسبة", "CALC_Btn")
        Shell("calc.exe")
    End Sub


    Private Sub Pure_txt_TextChanged(sender As Object, e As EventArgs) Handles Pure_txt.TextChanged
        If is_Use_Total_Port = True Then Show_Total_Port(Pure)
    End Sub

    Private Sub AGMetroGrid_DataSourceChanged(sender As Object, e As EventArgs) Handles dgvSales.DataSourceChanged
        Calc_Total()
    End Sub

    Private Async Sub Refresh_IM_Btn_Click(sender As Object, e As EventArgs) Handles Refresh_IM_Btn.Click
        AddDraftLog("زر", "بدء تحديث الأصناف والاختصارات", "Refresh_IM_Btn")
        Refresh_IM_Btn.Enabled = False
        Refresh_IM_Btn.Text = "جاري التحديث..."
        Refresh_IM_Btn.BackColor = Color.FromArgb(255, 243, 205)
        Me.Cursor = Cursors.WaitCursor
        SetRefreshStatus("يتم تحديث قائمة الأصناف والوحدات الآن...", Color.FromArgb(120, 53, 15))

        Try
            Dim isLoaded As Boolean = Await Load_ALL_IM()
            Dim shortcutsLoaded As Boolean = TryLoadShortcutItemsFromDatabase()

            If shortcutsLoaded Then loadShortCut_IM()

            If isLoaded AndAlso shortcutsLoaded Then
                AddDraftLog("زر",
                            "تم تحديث الأصناف والاختصارات",
                            "Refresh_IM_Btn",
                            "",
                            IM_Units_Dt.Rows.Count.ToString("N0") & " وحدة / " & ShortcutItemsDt.Rows.Count.ToString("N0") & " اختصار")
                SetRefreshStatus(
                    "تم التحديث: " & IM_Units_Dt.Rows.Count.ToString("N0") & " وحدة متاحة، و" & ShortcutItemsDt.Rows.Count.ToString("N0") & " اختصار",
                    Color.FromArgb(21, 128, 61)
                )
            ElseIf IM_Units_Dt IsNot Nothing AndAlso IM_Units_Dt.Rows.Count > 0 Then
                AddDraftLog("زر", "تعذر التحديث وتم الإبقاء على بيانات الأصناف السابقة", "Refresh_IM_Btn")
                SetScreenStatus("تعذر التحديث؛ الشاشة مستمرة بآخر بيانات أصناف ناجحة",
                                ScreenStatusType.Warning,
                                (LastItemsLoadError & Environment.NewLine & LastShortcutsLoadError).Trim())
            Else
                AddDraftLog("زر", "تعذر تحديث الأصناف ولا توجد بيانات محلية", "Refresh_IM_Btn")
                SetScreenStatus("تعذر تحميل الأصناف ولا توجد بيانات محلية سابقة",
                                ScreenStatusType.Error,
                                (LastItemsLoadError & Environment.NewLine & LastShortcutsLoadError).Trim())
            End If
        Finally
            Me.Cursor = Cursors.Default
            Refresh_IM_Btn.Enabled = True
            Refresh_IM_Btn.Text = RefreshButtonDefaultText
            Refresh_IM_Btn.BackColor = RefreshButtonDefaultBackColor
            Barcode_SH_txt.Focus()
        End Try
    End Sub

    Private Sub SetRefreshStatus(message As String, foreColor As Color)
        Dim statusType As ScreenStatusType = ScreenStatusType.Information
        If foreColor.R > 150 AndAlso foreColor.G < 100 Then statusType = ScreenStatusType.Error
        If foreColor.G > foreColor.R AndAlso foreColor.G > foreColor.B Then statusType = ScreenStatusType.Success
        If message.Contains("الآن") OrElse message.Contains("جاري") Then statusType = ScreenStatusType.Loading
        SetScreenStatus(message, statusType)

    End Sub

    Private Sub Notes_txt_TextChanged(sender As Object, e As EventArgs) Handles txtNotes.TextChanged
        If CurrentDraft Is Nothing Then Exit Sub

        CurrentDraft.About = txtNotes.Text
        DraftManager.SaveDraft(CurrentDraft)
    End Sub

    Private Sub Notes_txt_Leave(sender As Object, e As EventArgs) Handles txtNotes.Leave

        If CurrentDraft Is Nothing Then Exit Sub

        If String.Equals(LastLoggedNotesText, txtNotes.Text, StringComparison.Ordinal) = False Then
            AddDraftLog("تعديل",
                        "تم تعديل ملاحظات المسودة",
                        "txtNotes",
                        LastLoggedNotesText,
                        txtNotes.Text)
            LastLoggedNotesText = txtNotes.Text
        End If

    End Sub


    Private Sub txtDiscount_Leave(sender As Object, e As EventArgs) Handles Discount_txt.Leave
        If CurrentDraft Is Nothing Then Exit Sub

        Dim disc As Decimal = 0D
        Decimal.TryParse(Discount_txt.Text, disc)

        Dim oldDiscount As Decimal = CurrentDraft.Discount
        CurrentDraft.Discount = disc
        DraftCalculator.RecalculateDraft(CurrentDraft)

        DraftManager.SaveDraft(CurrentDraft)
        UpdateDraftTotalsOnScreen()

        If oldDiscount <> disc Then
            AddDraftLog("خصم",
                        "تم تعديل خصم المسودة عند مغادرة الحقل",
                        "Discount_txt",
                        oldDiscount.ToString("0.###"),
                        disc.ToString("0.###"))
        End If
    End Sub

    Private Sub AG_SH_txt_TextChanged(sender As Object, e As EventArgs) Handles AG_SH_txt.TextChanged
        If CurrentDraft Is Nothing Then Exit Sub

        CurrentDraft.AG_ID = AG_ID
        CurrentDraft.CustomerName = AG_SH_txt.Text

        'AG_ID = customerId.ToString()
        'txtCustomerName.Text = customerName

        DraftManager.SaveDraft(CurrentDraft)
    End Sub


    Private Sub dgvSales_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSales.CellEndEdit

        If CurrentDraft Is Nothing Then Exit Sub
        If e.RowIndex < 0 Then Exit Sub

        Dim row As DataGridViewRow = dgvSales.Rows(e.RowIndex)
        Dim draftLineId As String = row.Cells("DraftLineId").Value.ToString()

        Dim item = CurrentDraft.Items.FirstOrDefault(Function(x) x.DraftLineId = draftLineId)
        If item Is Nothing Then Exit Sub

        Dim oldQty As Decimal = item.QTY
        Dim oldPrice As Decimal = item.Price
        item.QTY = Convert.ToDecimal(row.Cells("QTY").Value)
        item.Price = Convert.ToDecimal(row.Cells("Price").Value)
        item.Compons = If(row.Cells("Compons").Value, "").ToString()
        item.D_Vaild = If(row.Cells("D_Vaild").Value, "").ToString()

        DraftCalculator.RecalculateDraft(CurrentDraft)
        DraftManager.SaveDraft(CurrentDraft)

        LoadDraftToGrid()
        UpdateDraftTotalsOnScreen()
        AddDraftLog("تعديل",
                    "تم تعديل بيانات سطر الصنف",
                    "dgvSales",
                    "كمية: " & oldQty.ToString("0.###") & " / سعر: " & oldPrice.ToString("0.###"),
                    "كمية: " & item.QTY.ToString("0.###") & " / سعر: " & item.Price.ToString("0.###"),
                    item.ItemName,
                    item.IM_ID,
                    item.QTY,
                    item.T_Price)

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Draft_Btn.Click
        AddDraftLog("زر", "فتح قائمة المسودات", "Draft_Btn")
        Try
            Sales_Drafts_Menu.ShowDialog()
        Finally
            UpdateDraftButtonIndicator()
        End Try
    End Sub

    Private Sub ChangeDraftCustomer(agId As Integer, agName As String)

        EnsureDraftExists()

        Dim oldCustomerName As String = CurrentDraft.CustomerName
        CurrentDraft.AG_ID = agId
        CurrentDraft.AG_NAME = agName
        CurrentDraft.UpdatedAt = DateTime.Now

        ' العرض على الشاشة
        AG_ID = agId.ToString()
        AG_SH_txt.Text = agName

        DraftManager.SaveDraft(CurrentDraft)
        AddDraftLog("عميل",
                    "تم تغيير عميل المسودة",
                    "ChangeCustomerButton",
                    oldCustomerName,
                    agName)

    End Sub

    Private Sub SetDraftDiscount(discountValue As Decimal)

        If CurrentDraft Is Nothing Then Exit Sub

        If discountValue < 0 Then
            MessageBox.Show("قيمة التخفيض لا يمكن أن تكون سالبة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If discountValue > CurrentDraft.Total Then
            MessageBox.Show("قيمة التخفيض لا يمكن أن تكون أكبر من إجمالي الفاتورة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim oldDiscount As Decimal = CurrentDraft.Discount
        CurrentDraft.Discount = discountValue
        Discount_txt.Text = discountValue

        DraftCalculator.RecalculateDraft(CurrentDraft)
        DraftManager.SaveDraft(CurrentDraft)

        UpdateDraftTotalsOnScreen()
        AddDraftLog("خصم",
                    "تم تعديل خصم المسودة",
                    "Discount_txt",
                    oldDiscount.ToString("0.###"),
                    discountValue.ToString("0.###"))

    End Sub

    Private Sub IMIncreaseButton_Click(sender As Object, e As EventArgs) Handles IMIncreaseButton.Click
        AddDraftLog("زر", "طلب زيادة كمية الصنف المحدد", "IMIncreaseButton")
        Dim Def As Double = 1
        ChangeQtyByInput(Def, False)
    End Sub

    Private Sub IMDicreaseButton_Click(sender As Object, e As EventArgs) Handles IMDicreaseButton.Click
        AddDraftLog("زر", "طلب إنقاص كمية الصنف المحدد", "IMDicreaseButton")
        Dim Def As Double = -1
        ChangeQtyByInput(Def, False)
    End Sub

    Private Sub QTY_Btn_Click(sender As Object, e As EventArgs) Handles QTY_Btn.Click
        AddDraftLog("زر", "فتح تعديل كمية الصنف المحدد", "QTY_Btn")
        EditSelectedDraftItemQtyByButton()
    End Sub

    Private Sub ChangePriceButton_Click(sender As Object, e As EventArgs) Handles ChangePriceButton.Click
        AddDraftLog("زر", "فتح تعديل سعر بيع الصنف المحدد", "ChangePriceButton")
        EditSelectedDraftItemPriceByButton()
    End Sub

    Private Sub ClearDraftItemsButton_Click(sender As Object, e As EventArgs) Handles ClearDraftItemsButton.Click
        AddDraftLog("زر", "طلب مسح كل أصناف المسودة", "ClearDraftItemsButton")
        ClearCurrentDraftItems()
    End Sub

    Private Sub Units_btn_Click(sender As Object, e As EventArgs) Handles Units_btn.Click

        AddDraftLog("زر", "فتح شاشة تغيير وحدة الصنف", "Units_btn")

        If dgvSales.Rows.Count = 0 Then Exit Sub

        Dim currentIM_ID As Integer = CInt(dgvSales.CurrentRow.Cells("Bill_IMID_CL").Value)
        Dim currentU_ID As Integer = CInt(dgvSales.CurrentRow.Cells("U_ID").Value)

        Using frm As New Frm_Select_Unit(IM_Units_Dt, currentIM_ID, currentU_ID)

            If frm.ShowDialog() = DialogResult.OK Then

                Dim r As DataRow = frm.SelectedRow

                ChangeSelectedItemUnit(
    newUnitId:=r("U_ID"),
    newUnitName:=r("U_Name"),
    newBarcode:=r("Barcode"),
    newUCargo:=r("U_Cargo"),
    newPrice:=r("Price")
)

                'dgvSales.CurrentRow.Cells("U_ID").Value = r("U_ID")
                'dgvSales.CurrentRow.Cells("U_Name").Value = r("U_Name")
                'dgvSales.CurrentRow.Cells("U_Cargo").Value = r("U_Cargo")
                'dgvSales.CurrentRow.Cells("Price").Value = r("Price")
                'dgvSales.CurrentRow.Cells("Barcode").Value = r("Barcode")

            End If

        End Using
    End Sub

    Private Sub ChangeSelectedItemUnit(newUnitId As Long,
                                   newUnitName As String,
                                   newBarcode As String,
                                   newUCargo As Double,
                                   newPrice As Decimal)

        If CurrentDraft Is Nothing Then Exit Sub
        If dgvSales.CurrentRow Is Nothing Then Exit Sub

        Dim draftLineId As String = dgvSales.CurrentRow.Cells("DraftLineId").Value.ToString()

        Dim item As SaleDraftItem =
            CurrentDraft.Items.FirstOrDefault(Function(x) x.DraftLineId = draftLineId)

        If item Is Nothing Then Exit Sub

        Dim oldUnitName As String = item.UnitName
        Dim oldPrice As Decimal = item.Price

        ' تعديل بيانات الوحدة
        item.U_ID = newUnitId
        item.UnitName = newUnitName
        item.Barcode = newBarcode
        item.U_Cargo = newUCargo
        item.Price = newPrice

        ' إعادة حساب السطر
        item.T_Price = item.QTY * item.Price
        item.ST_QTY = CDec(item.QTY * CDec(item.U_Cargo))

        ' إعادة حساب الفاتورة كاملة
        DraftCalculator.RecalculateDraft(CurrentDraft)

        ' حفظ المسودة
        DraftManager.SaveDraft(CurrentDraft)

        ' تحديث الشاشة
        LoadDraftToGrid()
        UpdateDraftTotalsOnScreen()
        AddDraftLog("وحدة",
                    "تم تغيير وحدة الصنف",
                    "Units_btn",
                    oldUnitName & " / " & oldPrice.ToString("0.###"),
                    newUnitName & " / " & newPrice.ToString("0.###"),
                    item.ItemName,
                    item.IM_ID,
                    item.QTY,
                    item.T_Price)

    End Sub

    Private Sub Calc_Dicount_Btn_Click(sender As Object, e As EventArgs) Handles Calc_Dicount_Btn.Click
        'Make_Discount()
        AddDraftLog("زر", "فتح إدخال الخصم", "Calc_Dicount_Btn")
        ChangeDiscountByInput()
    End Sub

    Private Sub ChangeDiscountByInput()

        If CurrentDraft Is Nothing Then Exit Sub

        Dim discountValue As Decimal
        If ShowTouchDiscountDialog(CurrentDraft.Discount, discountValue) = DialogResult.OK Then
            SetDraftDiscount(discountValue)
        End If

    End Sub

    Private Sub note_Btn_Click(sender As Object, e As EventArgs) Handles note_Btn.Click
        F_BillNotes = New BillNotes
        F_BillNotes.T_ID = T_ID
        F_BillNotes.ShowDialog()
    End Sub
End Class
