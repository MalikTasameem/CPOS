
Imports System.Drawing.Printing
Imports System.IO
Imports ZXing
Module FREPORT

    Dim StoreName As String = SBill_Title_1
    Dim StoreAddress As String = SBill_Title_2
    Dim Img As Image ' = Image.FromFile(Application.StartupPath() & "\logo\logo.jpg")
    Dim TransNo As String = "الفاتورة-58810-120"
    Dim TransDate As String = Format(Now, "yyyy-MM-dd HH:mm:ss")
    Dim TransType As String = ""
    'for item sales | untuk item penjualan
    Dim dtItem As DataTable
    Dim arrWidth() As Integer
    Dim arrFormat() As StringFormat

    'declaring printing format class
    Dim c As New PrintingFormat

    'for subtotal & qty total
    Dim dblSubtotal As Double = 0
    Dim dblQty As Double = 0
    Dim dblPayment As Double = 50000
    Dim Trans_Phone As String
    Dim RTP_COUNT As String
    Dim is_photo As Boolean = True
    Dim Bill_Note As String = ""
    Dim Bill_Barcode As String = ""
    Public Sub Data_Load(IM_Dt As DataTable, Bill_Date As String, SB_ID As String, Bill_Num As String, Bill_Type As String, Notes As String, Cr_Phone As String, RTP_C As String, TB_NAME As String, Barcode As String)

        'Dim filePath As String = 

        If File.Exists(Application.StartupPath() & "\logo\logo.jpg") Then
            is_photo = True
            Img = Image.FromFile(Application.StartupPath() & "\logo\logo.jpg")
        Else
            is_photo = False
        End If



        If String.IsNullOrWhiteSpace(TB_NAME) Then
            TransType = "الطلب:  (   " & Bill_Num & "   ) - " & Bill_Type
            TransNo = "الفاتورة:" & SB_ID & " - " & Bill_Date
        Else
            TransType = "الطلب:(" & Bill_Num & ") - " & Bill_Type & " - " & TB_NAME
            TransNo = ""
        End If

        RTP_COUNT = RTP_C

        Bill_Note = Notes

        dtItem = New DataTable
        dtItem = IM_Dt
        Trans_Phone = Cr_Phone

        Bill_Barcode = Barcode
    End Sub


    Public Sub PRINT_REPORT()



        Printer.NewPrint()

        If is_photo = True Then Printer.Print(Img, 200, 100)


        'Setting Font
        Printer.SetFont("Arial", 11, FontStyle.Bold)
        Printer.Print(StoreName) 'Store Name | Nama Toko

        'Setting Font
        Printer.SetFont("Arial", 8, FontStyle.Regular)
        Printer.Print(StoreAddress & ";", {280}, 0) 'Store Address | Alamat Toko

        'spacing
        Printer.Print("------------------------------------------------------------------------")

        '  Printer.SetFont("Arial", 8, FontStyle.Bold)
        arrFormat = {c.MidRight}
        Printer.Print(TransNo) ' Transaction No | Nomor transaksi
        'Printer.Print(TransDate) ' Trans Date | Tanggal transaksi

        Printer.SetFont("Arial", 11, FontStyle.Bold)
        Printer.Print(TransType)
        'Printer.Print(" ")

        If Not String.IsNullOrWhiteSpace(Trans_Phone) Then
            Printer.SetFont("Arial", 10, FontStyle.Bold)
            Printer.Print(Trans_Phone)
            Printer.Print("------------------------------------")
        End If

        Printer.SetFont("Arial", 8, FontStyle.Bold)
        ' Printer.Print(" ") 'spacing

        arrWidth = {70, 50, 40, 90} 'arrWidth = {90, 40, 50, 70} 'array for column width | array untuk lebar kolom
        arrFormat = {c.MidLeft, c.MidRight, c.MidRight, c.MidRight} 'array alignment 

        'column header split by ; | nama kolom dipisah dengan ;
        Printer.Print("الإجمالي;السعر;العدد;الصنف", arrWidth, arrFormat)
        Printer.SetFont("Arial", 9, FontStyle.Regular) 'Setting Font
        Printer.Print("-----------------------------------------------------------------") 'line

        dblSubtotal = 0
        dblQty = 0
        'looping item sales | loop item penjualan
        For r = 0 To dtItem.Rows.Count - 1
            'Printer.Print(dtItem.Rows(r).Item("IM_Name") & ";" & dtItem.Rows(r).Item("QTY") & ";" &
            '              dtItem.Rows(r).Item("Price") & ";" &
            '              (dtItem.Rows(r).Item("QTY") * dtItem.Rows(r).Item("Price")), arrWidth, arrFormat)
            '
            '.ToString(".####")
            Printer.SetFont("Arial", 10.5, FontStyle.Regular) 'Setting Font
            Printer.Print(roundationewithout0(dtItem.Rows(r).Item("T_Price")) & ";" & roundationewithout0(dtItem.Rows(r).Item("Price")) & ";" &
                          roundationewithout0(dtItem.Rows(r).Item("QTY")) & ";" &
                          (dtItem.Rows(r).Item("IM_Name")), arrWidth, arrFormat)
            Printer.SetFont("Arial", 9, FontStyle.Regular) 'Setting Font

            dblQty = dblQty + CSng(dtItem.Rows(r).Item("QTY"))
            dblSubtotal = dblSubtotal + (dtItem.Rows(r).Item("T_Price"))
            '(dtItem.Rows(r).Item("QTY") * dtItem.Rows(r).Item("Price"))
            Printer.Print("-----------------------------------------------------------------")
        Next

        ' Printer.Print("------------------------------------")
        arrWidth = {120, 130} 'array for column width | array untuk lebar kolom
        arrFormat = {c.MidLeft, c.MidLeft} 'array alignment 

        Printer.SetFont("Arial", 11, FontStyle.Bold) 'Setting Font
        Printer.Print(dblSubtotal & ";" & "الإجمالي", arrWidth, arrFormat)
        'Printer.Print("المدفوع;" & dblPayment, arrWidth, arrFormat)
        Printer.SetFont("Arial", 8, FontStyle.Regular) 'Setting Font
        Printer.Print("------------------------------------------------------------------------")
        ' Printer.Print("Change;" & dblPayment - dblSubtotal, arrWidth, arrFormat)
        '  Printer.Print(" ")
        Printer.Print(dblQty & ";" & "الكميات", arrWidth, arrFormat)

        If Not String.IsNullOrWhiteSpace(SBill_Footer) Then
            Printer.SetFont("Arial", 10, FontStyle.Bold) 'Setting Font
            Printer.Print(SBill_Footer)
        End If

        Printer.SetFont("Arial", 8, FontStyle.Regular) 'Setting Font
        Printer.Print(Date.Now & "  -  " & USER_NAME)
        '---------------------------------------------------------------------------
        Printer.SetFont("Arial", 12, FontStyle.Bold) 'Setting Font
        Printer.Print(RTP_COUNT)


        If POS_BARCODE_TYPE <> "بلا" Then

            Dim barc As New BarcodeWriter

            If POS_BARCODE_TYPE = "CODE_128" Then
                barc.Format = BarcodeFormat.CODE_128
            Else
                barc.Format = BarcodeFormat.QR_CODE
            End If


            Img = barc.Write(Bill_Barcode)
            Printer.Print(Img, 90, 60)

        End If


            'Release the job for actual printing
            Printer.DoPrint()

    End Sub
    '------------------------------------------------------------------------------------------------------------
    Public Sub PRINT_REPORT_KSH()


        Printer.NewPrint()

        'Printer.Print(Img, 200, 100)

        ''Setting Font
        'Printer.SetFont("Arial", 11, FontStyle.Bold)
        'Printer.Print(StoreName) 'Store Name | Nama Toko

        ''Setting Font
        'Printer.SetFont("Arial", 8, FontStyle.Regular)
        'Printer.Print(StoreAddress & ";", {280}, 0) 'Store Address | Alamat Toko

        ''spacing
        'Printer.Print("------------------------------------------------------------------------")

        '  Printer.SetFont("Arial", 8, FontStyle.Bold)
        'arrFormat = {c.MidRight}
        'Printer.Print(TransNo) ' Transaction No | Nomor transaksi
        'Printer.Print(TransDate) ' Trans Date | Tanggal transaksi

        Printer.SetFont("Arial", 14, FontStyle.Bold)
        Printer.Print(TransType)
        'Printer.Print(" ")
        'Printer.SetFont("Arial", 9, FontStyle.Regular)
        'Printer.Print("---------------------------------------------------")

        'If Not String.IsNullOrWhiteSpace(Trans_Phone) Then
        '    Printer.SetFont("Arial", 10, FontStyle.Bold)
        '    Printer.Print(Trans_Phone)
        'End If


        ' Printer.Print(" ") 'spacing

        ' arrWidth = {120, 130}
        arrWidth = {40, 235} 'arrWidth = {90, 40, 50, 70} 'array for column width | array untuk lebar kolom --, 40, 90
        arrFormat = {c.MidLeft, c.MidLeft} 'array alignment --, c.MidRight, c.MidRight

        'column header split by ; | nama kolom dipisah dengan ;
        'Printer.Print("العدد;الصنف", arrWidth, arrFormat) ' الإجمالي;السعر;
        'Printer.SetFont("Arial", 10, FontStyle.Regular) 'Setting Font
        Printer.SetFont("Arial", 9, FontStyle.Regular)
        Printer.Print("------------------------------------------------------------") 'line



        dblSubtotal = 0
        dblQty = 0
        'looping item sales | loop item penjualan
        For r = 0 To dtItem.Rows.Count - 1
            'dtItem.Rows(r).Item("QTY") * dtItem.Rows(r).Item("Price") & ";" & dtItem.Rows(r).Item("Price") & ";" &
            Printer.SetFont("Arial", 13, FontStyle.Regular)

            Printer.Print(
                          roundationewithout0(dtItem.Rows(r).Item("QTY")) & ";" &
                          (dtItem.Rows(r).Item("IM_Name")), arrWidth, arrFormat)

            ' dblQty = dblQty + CSng(dtItem.Rows(r).Item("QTY"))
            ' dblSubtotal = dblSubtotal + (dtItem.Rows(r).Item("QTY") * dtItem.Rows(r).Item("Price"))
            Printer.SetFont("Arial", 9, FontStyle.Regular)

            Printer.Print("------------------------------------------------------------")
        Next

        '' Printer.Print("------------------------------------")
        'arrWidth = {120, 130} 'array for column width | array untuk lebar kolom
        'arrFormat = {c.MidLeft, c.MidLeft} 'array alignment 

        'Printer.SetFont("Arial", 11, FontStyle.Bold) 'Setting Font
        'Printer.Print(dblSubtotal & ";" & "الإجمالي", arrWidth, arrFormat)
        ''Printer.Print("المدفوع;" & dblPayment, arrWidth, arrFormat)
        'Printer.SetFont("Arial", 8, FontStyle.Regular) 'Setting Font
        'Printer.Print("------------------------------------------------------------------------")
        '' Printer.Print("Change;" & dblPayment - dblSubtotal, arrWidth, arrFormat)
        ''  Printer.Print(" ")
        'Printer.Print(dblQty & ";" & "الكميات", arrWidth, arrFormat)

        'If Not String.IsNullOrWhiteSpace(SBill_Footer) Then
        '    Printer.SetFont("Arial", 10, FontStyle.Bold) 'Setting Font
        '    Printer.Print(SBill_Footer)
        'End If

        'Printer.SetFont("Arial", 8, FontStyle.Regular) 'Setting Font
        Printer.Print(Bill_Note)

        Printer.Print(" . " & vbNewLine & " . " & vbNewLine & " . " & vbNewLine & " . ")

        Printer.Print(Date.Now & "  -  " & USER_NAME)
        'Release the job for actual printing

        Printer.DoPrint()



    End Sub


    '------------------------------------------------------------------------------------------------------------
    Public Sub Cancel_REPORT_KSH(Str As String)

        Printer.NewPrint()
        Printer.SetFont("Arial", 18, FontStyle.Bold)
        Printer.Print(Str)
        Printer.SetFont("Arial", 9, FontStyle.Regular)
        Printer.Print(Date.Now & "  -  " & USER_NAME)
        Printer.DoPrint()

    End Sub

    Private Function roundationewithout0(s As String) As String
        Try
            Dim value As String = ""
            If IsNumeric(s) Then
                Dim sal As Double = Math.Truncate(s * 1000) / 1000
                value = Math.Round(sal, 3)
            Else
                value = "0"
            End If
            Return value
        Catch ex As Exception
            Return "0"
        End Try

    End Function

End Module
