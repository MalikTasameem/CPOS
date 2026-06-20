Imports System.Drawing.Printing
Imports System.Data.SqlClient
Imports System.IO

Module Module3

    Public WithEvents pd As New PrintDocument
    Public preview As New PrintPreviewDialog
    Public PrintTransactionId As Integer
    Public Sub PrintPendingReceipt(transactionId As Integer)

        PrintTransactionId = transactionId

        pd.DefaultPageSettings.Landscape = False
        pd.DefaultPageSettings.PaperSize = New PaperSize("A4", 827, 1169)
        pd.DefaultPageSettings.Margins = New Margins(70, 70, 60, 60)

        preview.Document = pd
        preview.WindowState = FormWindowState.Maximized
        preview.ShowDialog()

    End Sub

    Public Sub pd_PrintPage(sender As Object, e As PrintPageEventArgs) Handles pd.PrintPage

        Dim g As Graphics = e.Graphics

        Dim fontTitle As New Font("Arial", 18, FontStyle.Bold)
        Dim fontNormal As New Font("Arial", 12)

        Dim left As Integer = e.MarginBounds.Left
        Dim right As Integer = e.MarginBounds.Right
        Dim width As Integer = e.MarginBounds.Width
        Dim y As Integer = e.MarginBounds.Top

        Dim midPoint As Integer = left + width \ 2

        ' تنسيق يمين (للبيان العربي)
        Dim formatRight As New StringFormat()
        formatRight.Alignment = StringAlignment.Far
        formatRight.FormatFlags = StringFormatFlags.DirectionRightToLeft

        ' تنسيق يسار (للقيم)
        Dim formatLeft As New StringFormat()
        formatLeft.Alignment = StringAlignment.Near

        ' تحميل البيانات
        Dim dt As DataTable = GetExchangeTransactions_By_ID(PrintTransactionId)
        If dt.Rows.Count = 0 Then Exit Sub

        Dim row = dt.Rows(0)


        ' =============================
        ' اسم الشركة
        ' =============================
        Dim companyName As String = MY_Settings.SBill_Title_1
        Dim sizeTitle = g.MeasureString(companyName, fontTitle)

        g.DrawString(companyName, fontTitle, Brushes.Black,
                 left + (width - sizeTitle.Width) / 2, y)

        y += 40

        ' ===============================
        ' العنوان في المنتصف
        ' ===============================
        Dim title As String = "إيصال عملية صرافة"

        g.DrawString(title, fontTitle, Brushes.Black,
                 left + (width - sizeTitle.Width) / 2, y)

        y += 40

        ' خط فاصل
        g.DrawLine(Pens.Black, left, y, right, y)
        y += 30

        ' ===============================
        ' دالة مساعدة لرسم سطر منسق
        ' ===============================
        Dim lineHeight As Integer = 28

        Dim drawRow =
        Sub(labelText As String, valueText As String)
            ' البيان (يمين)
            g.DrawString(labelText, fontNormal, Brushes.Black,
                         New RectangleF(midPoint, y, width \ 2, lineHeight),
                         formatRight)

            ' القيمة (يسار)
            g.DrawString(valueText, fontNormal, Brushes.Black,
                         New RectangleF(left, y, width \ 2, lineHeight),
                         formatLeft)

            y += lineHeight
        End Sub

        ' ===============================
        ' البيانات
        ' ===============================

        drawRow("الحالة", row("StatusName").ToString())
        drawRow("رقم المرجع", row("ReferenceNo").ToString())
        drawRow("رقم العملية Trans No", row("ExchangeId").ToString())

        drawRow("التاريخ", CDate(row("CreatedAt")).ToString("yyyy/MM/dd HH:mm"))

        y += 10
        g.DrawLine(Pens.Black, left, y, right, y)
        y += 20

        drawRow("اسم العميل", row("CustomerName").ToString())
        drawRow("رقم الهوية", row("CustomerIdentityNumber").ToString())

        y += 10
        g.DrawLine(Pens.Black, left, y, right, y)
        y += 20

        drawRow("نوع العملية", row("OperationType").ToString())
        drawRow("المبلغ", Format(CDec(row("ForeignAmount")), "N2"))
        drawRow("العملة", row("Cr_Name").ToString())
        drawRow("الخزينة", row("ACC_NAME").ToString())

        y += 40
        g.DrawLine(Pens.Black, left, y, right, y)
        y += 50

        ' ===============================
        ' التوقيعات
        ' ===============================
        drawRow("توقيع الموظف", "____________________")
        y += 20
        drawRow("توقيع العميل", "____________________")

    End Sub


    Private Function GetExchangeTransactions_By_ID(ExchangeId As Int16)
        Dim dt As New DataTable
        Using con As New SqlConnection(MY_Settings.SqlConStr)

            Dim cmd As New SqlCommand("[GetExchangeTransactions_By_ID]", con)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@ExchangeId", ExchangeId)

            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
        End Using


        Return dt
    End Function


    Public Function GetVaultBalance(accCode As String) As Decimal

        Using con As New SqlConnection(MY_Settings.SqlConStr)
            con.Open()

            Dim cmd As New SqlCommand("
         SELECT ISNULL(SUM(CREDIT),0) - ISNULL(SUM(DEBIT),0) FROM  ACC_BALANCE
            WHERE ACC_CODE = @Code
            AND IS_VOID = 0 AND is_Depended = 1", con)

            cmd.Parameters.AddWithValue("@Code", accCode)

            Return Convert.ToDecimal(cmd.ExecuteScalar())
        End Using

    End Function

    Public Function GetVaultPendingBalance(accCode As String) As Decimal

        Using con As New SqlConnection(MY_Settings.SqlConStr)
            con.Open()

            Dim cmd As New SqlCommand("
         SELECT ISNULL(SUM(ForeignAmount),0)  FROM  [ExchangeTransactions]
            WHERE [VaultId] = @Code AND StatusId = 1 ", con)

            cmd.Parameters.AddWithValue("@Code", accCode)

            Return Convert.ToDecimal(cmd.ExecuteScalar())
        End Using

    End Function

    Public Function readfile(sPath As String) As Byte()
        Try
            Dim data As Byte() = Nothing

            'Use FileInfo object to get file size.
            Dim fInfo As New FileInfo(sPath)
            Dim numBytes As Long = fInfo.Length

            'Open FileStream to read file
            Dim fStream As New FileStream(sPath, FileMode.Open, FileAccess.Read)

            'Use BinaryReader to read file stream into byte array.
            Dim br As New BinaryReader(fStream)

            'When you use BinaryReader, you need to supply number of bytes to read from file.
            'In this case we want to read entire file. So supplying total number of bytes.
            data = br.ReadBytes(CInt(numBytes))
            Return data
        Catch ex As Exception
            MsgBox(ex.Message)
            '  Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
        End Try

        Return Nothing
    End Function


    Public Function upLoadImageOrFile(ByVal sFilePath As String, ByVal sFileType As String, Table_Name As String, B_T_ID As Integer) As Boolean
        Try
            Dim SqlCom As SqlCommand
            Dim imageData As Byte()
            Dim sFileName As String
            Dim qry As String

            Dim c As New C

            'Read Image Bytes into a byte array

            'Initialize SQL Server Connection
            If c.Con.State = ConnectionState.Closed Then c.Con.Open()

            imageData = readfile(sFilePath)
            sFileName = System.IO.Path.GetFileName(sFilePath)

            'Set insert query

            qry = "INSERT INTO " & Table_Name & " ([B_T_ID],[DOC],[Extended],[USER_ID]) values(@B_T_ID, @ImageData,@EX,@USER_ID)"

            'Initialize SqlCommand object for insert.
            SqlCom = New SqlCommand(qry, c.Con)

            'We are passing File Name and Image byte data as sql parameters.

            SqlCom.Parameters.Add(New SqlParameter("@B_T_ID", B_T_ID))
            SqlCom.Parameters.Add(New SqlParameter("@ImageData", DirectCast(imageData, Object)))
            SqlCom.Parameters.Add(New SqlParameter("@EX", sFileType))
            SqlCom.Parameters.Add(New SqlParameter("@USER_ID", USER_ID))

            SqlCom.ExecuteNonQuery()
            c.Con.Close()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            '   Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
            Return False
        End Try
    End Function


End Module
