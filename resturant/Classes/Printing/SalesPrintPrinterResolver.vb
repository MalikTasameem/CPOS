Public Class SalesPrintPrinterResolver

    Public Shared Function GetLocalPrinterName(paperKind As String) As String
        Dim kind As String = If(paperKind, "").Trim().ToUpperInvariant()

        Select Case kind
            Case "RECEIPT", "ROLL"
                Return If(Default_Printer_80, "")
            Case "BARCODE"
                Return If(Default_Barcode_Printer, "")
            Case Else
                Return If(Default_Printer_A4, "")
        End Select
    End Function

    Public Shared Sub ApplyLocalPrinter(profile As SalesPrintProfile)
        If profile Is Nothing Then Return
        profile.PrinterName = GetLocalPrinterName(profile.PaperKind)
    End Sub

End Class
