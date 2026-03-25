Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Data.SqlClient
Imports System.IO

Public Class Print_PDF
    Inherits PdfPageEventHelper
    Public table As PdfPTable
    Public tableHeight As Double
    Dim Arr_List As New List(Of String)()
    Dim bf22 As BaseFont = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + "\fonts\Arial.ttf", BaseFont.IDENTITY_H, True)
    Dim ffont = New Font(bf22, 11, 1)





    Public Sub New()
        table = New PdfPTable(2)
        Dim bf As BaseFont = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + "\fonts\Arial.ttf", BaseFont.IDENTITY_H, True)
        Dim mainFont As New Font(bf, 26, 1)

        Dim path As String = Application.StartupPath & "\logo\logo.jpg"
        Dim img1 As Image = iTextSharp.text.Image.GetInstance(path)
        img1.ScaleAbsolute(100.0F, 100.0F)


        Dim compname As String = SBill_Title_1
        Dim compadd As String = SBill_Title_2
        Dim compPhone As String = ""

        table.LockedWidth = True
        table.TotalWidth = 1000
        table.RunDirection = PdfWriter.RUN_DIRECTION_RTL
        Dim cell2 As New iTextSharp.text.pdf.PdfPCell(New Phrase(12, compname & vbNewLine & compadd, mainFont))




        Dim imgCell1 = New iTextSharp.text.pdf.PdfPCell()

        imgCell1.AddElement(img1)

        cell2.HorizontalAlignment = HorizontalAlignment.Left
        cell2.Border = Rectangle.NO_BORDER
        imgCell1.HorizontalAlignment = HorizontalAlignment.Left
        imgCell1.Border = Rectangle.NO_BORDER

        table.AddCell(cell2)


        table.AddCell(imgCell1)
        tableHeight = table.TotalHeight()
    End Sub


    Public Function getTableHeight()
        Return tableHeight
    End Function

    Public Overrides Sub onendpage(writer As PdfWriter, Document As Document)
        Dim cb = writer.DirectContent()
        Dim footer = New Phrase(Document.PageNumber.ToString, ffont)
        ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER,
            footer,
            (Document.Right() - Document.Left()) / 2 + Document.LeftMargin(),
            Document.Bottom() - 10, 0)
        table.WriteSelectedRows(0, -1, 20, 770, writer.DirectContent())
    End Sub


    Public Sub PRINT_PDF_List(gridv As DataGridView, CheckedListBox1 As CheckedListBox, rpt_name As String, page_type As Integer)

        Try

            If CheckedListBox1.CheckedItems.Count = 0 Then
                If CheckedListBox1.Items.Count > 13 Then
                    MsgBox("قم بتحديد أعمدة من أجل عرضها فالتقرير", MsgBoxStyle.Exclamation, "طباعة التقرير")
                    Exit Sub
                Else
                    For i = 0 To CheckedListBox1.Items.Count - 1
                        CheckedListBox1.SetItemChecked(i, True)
                    Next
                End If
            End If

            If CheckedListBox1.CheckedItems.Count <= 14 Then



                Dim bf As BaseFont = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + "\fonts\Arial.ttf", BaseFont.IDENTITY_H, True)
                Dim mainFont As New Font(bf, 22, Font.BOLD)
                Dim SubFont As New Font(bf, 12, Font.NORMAL)

                Dim mainFont_2 As New Font(bf, 14, Font.BOLD)

                Dim title As String = "عنــــوان التقرير"
                Dim tt As New Paragraph(title, New Font(1, 8, FontStyle.Bold))
                Dim Ifont As New iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL)
                Dim table As New iTextSharp.text.pdf.PdfPTable(CheckedListBox1.CheckedItems.Count)
                table.WidthPercentage = 100
                table.RunDirection = PdfWriter.RUN_DIRECTION_RTL


                Dim table2 As New iTextSharp.text.pdf.PdfPTable(CheckedListBox1.CheckedItems.Count)
                table2.WidthPercentage = 100
                table2.RunDirection = PdfWriter.RUN_DIRECTION_RTL



                Dim intTblWidth(CheckedListBox1.CheckedItems.Count - 1) As Integer
                Dim iii As Integer = 0
                iii = CheckedListBox1.CheckedItems.Count - 1
                For x = 0 To CheckedListBox1.Items.Count - 1

                    If CheckedListBox1.GetItemChecked(x) = True Then
                        intTblWidth(iii) = Convert.ToSingle(((gridv.Columns(x).Width / gridv.Width)) * 1000.0F)
                        'aggregate_width(gridv.Columns(x).Width)
                        iii = iii - 1
                    End If

                    Arr_List.Add("")
                Next

                table.SetWidths(intTblWidth)
                table2.SetWidths(intTblWidth)




                Dim widths As Integer() = New Integer(CheckedListBox1.CheckedItems.Count - 1) {}

                For x As Integer = 0 To gridv.Columns.Count - 1
                    If x > CheckedListBox1.Items.Count - 1 Then
                        Exit For
                    End If
                    If CheckedListBox1.GetItemChecked(x) = True Then

                        Dim cellText As String = gridv.Columns(x).HeaderText
                        Dim cell As New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(11, cellText, SubFont))
                        cell.BackgroundColor = Color.LIGHT_GRAY
                        cell.BorderColor = Color.GRAY
                        cell.HorizontalAlignment = HorizontalAlignment.Right
                        ' cell.VerticalAlignment = Top
                        'cell.RunDirection = PdfWriter.RUN_DIRECTION_RTL
                        table.AddCell(cell)

                    End If
                Next




                For i As Integer = 0 To gridv.Rows.Count - 1

                    For j As Integer = 0 To gridv.Columns.Count - 1
                        If j > CheckedListBox1.Items.Count - 1 Then
                            Exit For
                        End If
                        If CheckedListBox1.GetItemChecked(j) = True Then
                            Dim cellText As String = ""
                            If Not IsDBNull(gridv.Rows(i).Cells(j).Value) Then
                                cellText = gridv.Rows(i).Cells(j).Value
                            End If

                            If gridv.Columns(j).Tag = 1 Then

                                If Not IsNumeric(cellText) Or j = 0 Then
                                    GoTo next_
                                Else
                                    If Arr_List(j) = "" Then Arr_List(j) = 0
                                    cellText = roundationeithout0(cellText)
                                    Arr_List(j) = Arr_List(j) + Convert.ToDouble(cellText)
                                End If

                            End If

next_:
                            Dim cell As New iTextSharp.text.pdf.PdfPCell(New Phrase(11, cellText, SubFont))
                            cell.HorizontalAlignment = HorizontalAlignment.Right
                            '  cell.VerticalAlignment = Top
                            cell.BorderColor = Color.GRAY

                            'Important for Arabic, Persian or Urdu Text
                            'cell.RunDirection = PdfWriter.RUN_DIRECTION_RTL
                            table.AddCell(cell)

                        End If

                    Next

                Next


                '----------------------------------------------------------------------------------------------------------------------------------------
                Dim N As Double
                For j As Integer = 0 To gridv.Columns.Count - 1
                    If j > CheckedListBox1.Items.Count - 1 Then
                        Exit For
                    End If
                    If CheckedListBox1.GetItemChecked(j) = True Then
                        Dim cellText As String = ""
                        If Arr_List.Count > 0 Then cellText = Arr_List(j)


                        If Not String.IsNullOrWhiteSpace(cellText) Then
                            N = cellText
                            cellText = N.ToString("N")
                        End If


                        Dim cell As New iTextSharp.text.pdf.PdfPCell(New Phrase(12, cellText, ffont))
                        cell.HorizontalAlignment = HorizontalAlignment.Right
                        cell.BorderColor = Color.GRAY
                        cell.BackgroundColor = Color.LIGHT_GRAY
                        table2.AddCell(cell)

                    End If
                Next


                Dim sTempFileName = Application.StartupPath & "\PDFs\ExportPdf.pdf"
                Dim folderPath As String = Application.StartupPath & "\PDFs\"
                If Not Directory.Exists(folderPath) Then
                    Directory.CreateDirectory(folderPath)
                End If
                Using stream As New FileStream(folderPath & "ExportPdf.pdf", FileMode.Create)
                    Dim pdfDoc As New Document(New Rectangle(1096, 793))


                    If page_type = 2 Then pdfDoc = New Document()

                    table.HeaderRows = 1
                    Dim rect = New Rectangle(pdfDoc.PageSize)
                    rect.Border = 15
                    rect.BorderColor = Color.LIGHT_GRAY
                    Dim pw As PdfWriter = PdfWriter.GetInstance(pdfDoc, stream)
                    Dim Footer As New Print_PDF
                    pw.PageEvent = Footer
                    pdfDoc.Open()
                    pdfDoc.SetMargins(20.0F, 20.0F, 110.0F, 20.0F)
                    pdfDoc.Add(rect)
                    Dim ttt = New PdfPTable(1)
                    Dim cell2 As New iTextSharp.text.pdf.PdfPCell(New Phrase(11, vbNewLine, mainFont))
                    Dim cell3 As iTextSharp.text.pdf.PdfPCell
                    ttt.WidthPercentage = 100
                    'Dim inp = InputBox("ادخل العنوان", "عنوان التقرير")
                    'If inp = "" Then
                    cell3 = New iTextSharp.text.pdf.PdfPCell(New Phrase(12, rpt_name, mainFont))
                    'Else
                    '    cell3 = New iTextSharp.text.pdf.PdfPCell(New Phrase(12, inp, mainFont))
                    'End If
                    cell3.RunDirection = PdfWriter.RUN_DIRECTION_RTL
                    cell3.Border = Rectangle.TITLE
                    cell3.HorizontalAlignment = HorizontalAlignment.Right
                    cell2.Border = Rectangle.NO_BORDER
                    ttt.AddCell(cell2)
                    'ttt.AddCell(cell2)
                    ttt.AddCell(cell2)
                    ttt.AddCell(cell3)
                    'ttt.AddCell(cell2)

                    '---------------------------------------------------------------------------------------------------------

                    '----------------------------------------
                    'Dim cell4 As New iTextSharp.text.pdf.PdfPCell(New Phrase(12, "عنوان 1" & "                      " & "عنوان 2", mainFont_2))
                    'cell4.HorizontalAlignment = HorizontalAlignment.Left
                    'cell4.RunDirection = PdfWriter.RUN_DIRECTION_RTL
                    'cell4.Border = Rectangle.NO_BORDER
                    'ttt.AddCell(cell4)              
                    '----------------------------------------


                    '--------------------------------------------------------------------------------------------------------
                    ttt.AddCell(cell2)

                    pdfDoc.Add(ttt)
                    pdfDoc.Add(table)
                    pdfDoc.Add(table2)

                    pdfDoc.Close()
                    stream.Close()

                End Using
                If File.Exists(sTempFileName) Then
                    Dim psi As New ProcessStartInfo(sTempFileName)
                    psi.WindowStyle = ProcessWindowStyle.Maximized
                    Dim proc As Process = Process.Start(psi)
                Else
                    MessageBox.Show("الرجاء قم باختيار ملف موجود على هذا الجهاز !!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                MsgBox("حجم ورقة A4 لايتسع الا ل 14 عمود", MsgBoxStyle.Exclamation, "طباعة التقرير")
            End If

        Catch ex As Exception
            '  Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
            MsgBox(ex.Message)
        End Try


    End Sub

    Public Function roundationeithout0(s As String) As String
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


    Public Sub PRINT_PDF(gridv As DataGridView, page_type As Integer, Rpt_Title As String)

        Try

            Dim ColumnsN As Integer = 0
            For i = 0 To gridv.Columns.Count - 1
                If gridv.Columns(i).Visible = True Then ColumnsN += 1
            Next

            If ColumnsN <= 14 Then



                Dim bf As BaseFont = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + "\fonts\Arial.ttf", BaseFont.IDENTITY_H, True)
                Dim mainFont As New Font(bf, 22, Font.BOLD)
                Dim SubFont As New Font(bf, 14, Font.NORMAL)
                Dim title As String = "عنــــوان التقرير"
                Dim tt As New Paragraph(title, New Font(1, 8, FontStyle.Bold))
                Dim Ifont As New iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL)

                Dim table As New iTextSharp.text.pdf.PdfPTable(ColumnsN)
                table.WidthPercentage = 100
                table.RunDirection = PdfWriter.RUN_DIRECTION_RTL


                Dim table2 As New iTextSharp.text.pdf.PdfPTable(ColumnsN)
                table2.WidthPercentage = 100
                table2.RunDirection = PdfWriter.RUN_DIRECTION_RTL

                Dim intTblWidth(ColumnsN - 1) As Integer
                Dim iii As Integer = 0

                iii = ColumnsN - 1

                For x = 0 To gridv.Columns.Count - 1
                    If gridv.Columns(x).Visible = True Then
                        intTblWidth(iii) = Convert.ToSingle(((gridv.Columns(x).Width / gridv.Width)) * 1000.0F)
                        'aggregate_width(gridv.Columns(x).Width)
                        iii = iii - 1
                    End If

                    Arr_List.Add("")
                Next

                table.SetWidths(intTblWidth)
                table2.SetWidths(intTblWidth)


                Dim widths As Integer() = New Integer(gridv.Columns.Count - 1) {}
                Dim ii As Integer = 0
                For x As Integer = 0 To gridv.Columns.Count - 1

                    If gridv.Columns(x).Visible = True Then
                        Dim cellText As String = gridv.Columns(x).HeaderText
                        Dim cell As New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(11, cellText, SubFont))
                        cell.BackgroundColor = Color.LIGHT_GRAY
                        cell.BorderColor = Color.GRAY
                        cell.HorizontalAlignment = HorizontalAlignment.Right
                        cell.RunDirection = PdfWriter.RUN_DIRECTION_RTL
                        table.AddCell(cell)
                        ii += 1
                    End If
                Next

                For i As Integer = 0 To gridv.Rows.Count - 1

                    For j As Integer = 0 To gridv.Columns.Count - 1

                        If gridv.Columns(j).Visible = True Then


                            Dim cellText As String = ""
                            If Not IsDBNull(gridv.Rows(i).Cells(j).Value) Then
                                cellText = gridv.Rows(i).Cells(j).Value
                            End If





                            If gridv.Columns(j).Tag = 1 Then

                                If Not IsNumeric(cellText) Or j = 0 Then
                                    GoTo next_
                                Else
                                    If Arr_List(j) = "" Then Arr_List(j) = 0
                                    cellText = roundationeithout0(cellText)
                                    Arr_List(j) = Arr_List(j) + Convert.ToDouble(cellText)
                                End If

                            End If

next_:


                            Dim cell As New iTextSharp.text.pdf.PdfPCell(New Phrase(11, cellText, SubFont))
                            cell.HorizontalAlignment = HorizontalAlignment.Right
                            '  cell.VerticalAlignment = Top
                            cell.BorderColor = Color.GRAY

                            'Important for Arabic, Persian or Urdu Text
                            'cell.RunDirection = PdfWriter.RUN_DIRECTION_RTL
                            table.AddCell(cell)

                        End If

                    Next

                Next


                '----------------------------------------------------------------------------------------------------------------------------------------
                Dim N As Double
                For j As Integer = 0 To gridv.Columns.Count - 1
                    If j > ColumnsN - 1 Then
                        Exit For
                    End If
                    'If CheckedListBox1.GetItemChecked(j) = True Then
                    Dim cellText As String = ""
                    If Arr_List.Count > 0 Then cellText = Arr_List(j)


                    If Not String.IsNullOrWhiteSpace(cellText) Then
                        N = cellText
                        cellText = N.ToString("N")
                    End If


                    Dim cell As New iTextSharp.text.pdf.PdfPCell(New Phrase(12, cellText, ffont))
                    cell.HorizontalAlignment = HorizontalAlignment.Right
                    cell.BorderColor = Color.GRAY
                    cell.BackgroundColor = Color.LIGHT_GRAY
                    table2.AddCell(cell)

                    'End If
                Next


                Dim sTempFileName = Application.StartupPath & "\PDFs\ExportPdf.pdf"
                Dim folderPath As String = Application.StartupPath & "\PDFs\"
                If Not Directory.Exists(folderPath) Then
                    Directory.CreateDirectory(folderPath)
                End If
                Using stream As New FileStream(folderPath & "ExportPdf.pdf", FileMode.Create)
                    Dim pdfDoc As New Document(New Rectangle(1096, 793))

                    If page_type = 2 Then pdfDoc = New Document()


                    table.HeaderRows = 1
                    Dim rect = New Rectangle(pdfDoc.PageSize)
                    rect.Border = 15
                    rect.BorderColor = Color.LIGHT_GRAY
                    Dim pw As PdfWriter = PdfWriter.GetInstance(pdfDoc, stream)
                    Dim Footer As New Print_PDF
                    pw.PageEvent = Footer
                    pdfDoc.Open()
                    pdfDoc.SetMargins(20.0F, 20.0F, 110.0F, 20.0F)
                    pdfDoc.Add(rect)
                    Dim ttt = New PdfPTable(1)
                    Dim cell2 As New iTextSharp.text.pdf.PdfPCell(New Phrase(11, vbNewLine, mainFont))
                    Dim cell3 As iTextSharp.text.pdf.PdfPCell
                    ttt.WidthPercentage = 100
                    'Dim inp = InputBox("ادخل العنوان", "عنوان التقرير")
                    'If inp = "" Then
                    cell3 = New iTextSharp.text.pdf.PdfPCell(New Phrase(12, Rpt_Title, mainFont))
                    'Else
                    '    cell3 = New iTextSharp.text.pdf.PdfPCell(New Phrase(12, inp, mainFont))
                    'End If
                    cell3.RunDirection = PdfWriter.RUN_DIRECTION_RTL
                    cell3.Border = Rectangle.TITLE
                    cell3.HorizontalAlignment = HorizontalAlignment.Right
                    cell2.Border = Rectangle.NO_BORDER
                    ttt.AddCell(cell2)
                    'ttt.AddCell(cell2)
                    ttt.AddCell(cell2)
                    ttt.AddCell(cell3)
                    'ttt.AddCell(cell2)

                    '---------------------------------------------------------------------------------------------------------

                    '----------------------------------------
                    'Dim cell4 As New iTextSharp.text.pdf.PdfPCell(New Phrase(12, "عنوان 1" & "                      " & "عنوان 2", mainFont_2))
                    'cell4.HorizontalAlignment = HorizontalAlignment.Left
                    'cell4.RunDirection = PdfWriter.RUN_DIRECTION_RTL
                    'cell4.Border = Rectangle.NO_BORDER
                    'ttt.AddCell(cell4)              
                    '----------------------------------------


                    '--------------------------------------------------------------------------------------------------------
                    ttt.AddCell(cell2)

                    pdfDoc.Add(ttt)
                    pdfDoc.Add(table)
                    pdfDoc.Add(table2)

                    pdfDoc.Close()
                    stream.Close()

                End Using
                If File.Exists(sTempFileName) Then
                    Dim psi As New ProcessStartInfo(sTempFileName)
                    psi.WindowStyle = ProcessWindowStyle.Maximized
                    Dim proc As Process = Process.Start(psi)







                            '-----------------------------------------------------------------------------------------------------------------------

                            '            Dim cell As New iTextSharp.text.pdf.PdfPCell(New Phrase(11, cellText, SubFont))
                            '            cell.HorizontalAlignment = HorizontalAlignment.Right
                            '            cell.BorderColor = Color.GRAY
                            '            'Important for Arabic, Persian or Urdu Text
                            '            cell.RunDirection = PdfWriter.RUN_DIRECTION_RTL
                            '            table.AddCell(cell)


                            '        End If

                            '    Next

                            'Next

                            'Dim sTempFileName = Application.StartupPath & "\PDFs\ExportPdf.pdf"
                            'Dim folderPath As String = Application.StartupPath & "\PDFs\"
                            'If Not Directory.Exists(folderPath) Then
                            '    Directory.CreateDirectory(folderPath)
                            'End If
                            'Using stream As New FileStream(folderPath & "ExportPdf.pdf", FileMode.Create)


                            '    Dim pdfDoc As New Document()
                            '    If page_type = 2 Then pdfDoc = New Document(New Rectangle(1096, 793))



                            '    table.HeaderRows = 1
                            '    Dim rect = New Rectangle(pdfDoc.PageSize)
                            '    rect.Border = 15
                            '    rect.BorderColor = Color.LIGHT_GRAY
                            '    Dim pw As PdfWriter = PdfWriter.GetInstance(pdfDoc, stream)
                            '    Dim Footer As New Print_PDF
                            '    pw.PageEvent = Footer
                            '    pdfDoc.Open()
                            '    pdfDoc.SetMargins(20.0F, 20.0F, 110.0F, 20.0F)
                            '    pdfDoc.Add(rect)
                            '    Dim ttt = New PdfPTable(1)
                            '    Dim cell2 As New iTextSharp.text.pdf.PdfPCell(New Phrase(11, vbNewLine, mainFont))
                            '    Dim cell3 As iTextSharp.text.pdf.PdfPCell
                            '    ttt.WidthPercentage = 100
                            '    'Dim inp = InputBox("ادخل العنوان", Rpt_Title)
                            '    'If inp = "" Then
                            '    '    cell3 = New iTextSharp.text.pdf.PdfPCell(New Phrase(12, "عنــــوان التقرير", mainFont))
                            '    'Else
                            '    cell3 = New iTextSharp.text.pdf.PdfPCell(New Phrase(12, Rpt_Title, mainFont))
                            '    'End If
                            '    cell3.RunDirection = PdfWriter.RUN_DIRECTION_RTL
                            '    cell3.Border = Rectangle.TITLE
                            '    cell3.HorizontalAlignment = HorizontalAlignment.Right
                            '    cell2.Border = Rectangle.NO_BORDER
                            '    ttt.AddCell(cell2)
                            '    ttt.AddCell(cell2)
                            '    'ttt.AddCell(cell2)
                            '    ttt.AddCell(cell3)
                            '    ttt.AddCell(cell2)
                            '    pdfDoc.Add(ttt)
                            '    pdfDoc.Add(table)

                            '    pdfDoc.Close()
                            '    stream.Close()

                            'End Using
                            'If File.Exists(sTempFileName) Then
                            '    Dim psi As New ProcessStartInfo(sTempFileName)
                            '    psi.WindowStyle = ProcessWindowStyle.Maximized
                            '    Dim proc As Process = Process.Start(psi)
                Else
                    MessageBox.Show("الرجاء قم باختيار ملف موجود على هذا الجهاز !!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Else
                MsgBox("حجم ورقة A4 لايتسع الا ل 14 عمود", MsgBoxStyle.Exclamation, "طباعة التقرير")
            End If

        Catch ex As Exception
            '  Logger.Log(ex, Me.Name, "", System.Reflection.MethodBase.GetCurrentMethod().Name)
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Function aggregate_width(Size As Integer)

        Select Case Size

            Case Is <= 50
                Return 1

            Case Is <= 75
                Return 3

            Case Is <= 100
                Return 6

            Case Is <= 150
                Return 8

            Case Is <= 200
                Return 10

        End Select

        Return 12
    End Function


End Class
