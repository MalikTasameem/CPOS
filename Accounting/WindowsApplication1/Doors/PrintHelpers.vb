Imports System.Drawing.Printing
Imports System.IO

Public Module PrintHelpers

    Public Sub ExportToPdf(doc As PrintDocument, pdfFilePath As String)

        Using dlg As New PrintDialog()
            dlg.AllowSomePages = False
            dlg.UseEXDialog = True
            dlg.Document = doc

            ' اختيار طابعة PDF
            For Each printer As String In PrinterSettings.InstalledPrinters
                If printer.ToLower().Contains("pdf") Then
                    doc.PrinterSettings.PrinterName = printer
                    Exit For
                End If
            Next

            ' اقتراح اسم الملف
            doc.PrinterSettings.PrintToFile = True
            doc.PrinterSettings.PrintFileName =
            IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                pdfFilePath & ".pdf"
            )

            Try
                doc.Print()
                MessageBox.Show("تم إنشاء ملف PDF بنجاح", "PDF", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("فشل إنشاء PDF: " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using










        '' التأكد من وجود المجلد
        'Dim dir As String = Path.GetDirectoryName(pdfFilePath)
        'If Not Directory.Exists(dir) Then
        '    Directory.CreateDirectory(dir)
        'End If

        '' إعدادات الطابعة
        'doc.PrinterSettings = New PrinterSettings()
        'doc.PrinterSettings.PrinterName = "Microsoft Print to PDF"
        'doc.PrinterSettings.PrintToFile = True
        'doc.PrinterSettings.PrintFileName = pdfFilePath

        '' طباعة إلى PDF
        'doc.Print()
    End Sub

End Module
