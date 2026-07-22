Imports System.Globalization
Imports System.Windows.Forms

Public Enum ManualFormatPrintMode
    RawMaterials
    Products
    Services
    Full
End Enum

Public Class ManualFormatPrintData
    Public Property ReportTitle As String = ""
    Public Property OrderNumber As String = ""
    Public Property OrderDate As String = ""
    Public Property OrderSubject As String = ""
    Public Property CustomerName As String = ""
    Public Property SalesBillNumber As String = ""
    Public Property EmployeeName As String = ""
    Public Property DeliverDate As String = ""
    Public Property Notes As String = ""
    Public Property UserName As String = ""
    Public Property ServerName As String = ""
    Public Property PrintDate As DateTime = DateTime.Now
    Public Property Sections As New List(Of ManualFormatPrintSection)

    Public Shared Function FromManualForm(form As Format_Items_Manual, mode As ManualFormatPrintMode) As ManualFormatPrintData
        Dim data As New ManualFormatPrintData()

        data.ReportTitle = GetReportTitle(mode)
        data.OrderNumber = SafeControlText(form.Bill_ID_Txt)
        data.OrderDate = If(form.DateTimeEx Is Nothing, "", form.DateTimeEx.Value.ToString("yyyy/MM/dd"))
        data.OrderSubject = SafeControlText(form.Title_txt)
        data.CustomerName = SafeControlText(form.SB_AG_NAME_TXT)
        data.SalesBillNumber = form.SelectedSalesBillNumber
        data.EmployeeName = If(form.EMP_FS Is Nothing, "", form.EMP_FS.Textt)
        data.DeliverDate = If(form.Deliver_DateTimePicker1 Is Nothing, "", form.Deliver_DateTimePicker1.Value.ToString("yyyy/MM/dd"))
        data.Notes = SafeControlText(form.Notes_txt)
        data.UserName = USER_NAME
        data.ServerName = MY_Settings.Server_Desc
        data.PrintDate = DateTime.Now

        Select Case mode
            Case ManualFormatPrintMode.RawMaterials
                data.Sections.Add(ManualFormatPrintSection.FromGrid("مواد الخام", form.BillMetroGrid))
            Case ManualFormatPrintMode.Products
                data.Sections.Add(ManualFormatPrintSection.FromGrid("المنتجات النهائية", form.AGMetroGrid))
            Case ManualFormatPrintMode.Services
                data.Sections.Add(ManualFormatPrintSection.FromGrid("اليد العاملة والخدمات", form.ServicesGrid))
            Case Else
                data.Sections.Add(ManualFormatPrintSection.FromGrid("المنتجات النهائية", form.AGMetroGrid))
                data.Sections.Add(ManualFormatPrintSection.FromGrid("مواد الخام", form.BillMetroGrid))
                data.Sections.Add(ManualFormatPrintSection.FromGrid("اليد العاملة والخدمات", form.ServicesGrid))
        End Select

        Return data
    End Function

    Private Shared Function GetReportTitle(mode As ManualFormatPrintMode) As String
        Select Case mode
            Case ManualFormatPrintMode.Products
                Return "تقرير منتجات أمر تصنيع يدوي"
            Case ManualFormatPrintMode.Services
                Return "تقرير اليد العاملة لأمر تصنيع يدوي"
            Case ManualFormatPrintMode.Full
                Return "تقرير شامل لأمر تصنيع يدوي"
            Case Else
                Return "تقرير مواد خام أمر تصنيع يدوي"
        End Select
    End Function

    Private Shared Function SafeControlText(ctrl As Control) As String
        If ctrl Is Nothing OrElse ctrl.Text Is Nothing Then Return ""
        Return ctrl.Text.Trim()
    End Function
End Class

Public Class ManualFormatPrintSection
    Public Property Title As String = ""
    Public Property Columns As New List(Of ManualFormatPrintColumn)
    Public Property Rows As New List(Of List(Of String))

    Public Shared Function FromGrid(title As String, grid As DataGridView) As ManualFormatPrintSection
        Dim section As New ManualFormatPrintSection()
        section.Title = title

        If grid Is Nothing Then Return section

        Dim visibleColumns As New List(Of DataGridViewColumn)
        For Each col As DataGridViewColumn In grid.Columns
            If col.Visible Then visibleColumns.Add(col)
        Next

        visibleColumns.Sort(Function(a, b) a.DisplayIndex.CompareTo(b.DisplayIndex))

        For Each col As DataGridViewColumn In visibleColumns
            section.Columns.Add(New ManualFormatPrintColumn With {
                                .Name = col.Name,
                                .HeaderText = If(String.IsNullOrWhiteSpace(col.HeaderText), col.Name, col.HeaderText),
                                .WidthValue = Math.Max(35, col.Width),
                                .Alignment = GetAlignment(col),
                                .HasTotal = IsSummableColumn(col),
                                .DisplayFormat = col.DefaultCellStyle.Format
                                 })
        Next

        For Each row As DataGridViewRow In grid.Rows
            If row.IsNewRow OrElse row.Visible = False Then Continue For

            Dim values As New List(Of String)
            For i As Integer = 0 To visibleColumns.Count - 1
                Dim col As DataGridViewColumn = visibleColumns(i)
                Dim value As Object = row.Cells(col.Name).Value
                values.Add(If(value Is Nothing OrElse value Is DBNull.Value, "", value.ToString()))

                If i < section.Columns.Count AndAlso section.Columns(i).HasTotal Then
                    Dim decimalValue As Decimal = 0D
                    If TryReadDecimal(value, decimalValue) Then
                        section.Columns(i).TotalValue += decimalValue
                        section.Columns(i).TotalValueCount += 1
                    End If
                End If
            Next
            section.Rows.Add(values)
        Next

        Return section
    End Function

    Private Shared Function GetAlignment(col As DataGridViewColumn) As String
        If col Is Nothing Then Return "Center"

        Select Case col.DefaultCellStyle.Alignment
            Case DataGridViewContentAlignment.MiddleRight, DataGridViewContentAlignment.TopRight, DataGridViewContentAlignment.BottomRight
                Return "Right"
            Case DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.TopLeft, DataGridViewContentAlignment.BottomLeft
                Return "Left"
            Case Else
                Return "Center"
        End Select
    End Function

    Private Shared Function IsSummableColumn(col As DataGridViewColumn) As Boolean
        If col Is Nothing Then Return False

        Dim nameText As String = (col.Name & " " & col.HeaderText & " " & col.DataPropertyName).ToLowerInvariant()

        If nameText.Contains("id") OrElse nameText.Contains("رقم") OrElse nameText.Trim() = "ت" Then Return False

        Return nameText.Contains("total") _
            OrElse nameText.Contains("اجمالي") _
            OrElse nameText.Contains("إجمالي") _
            OrElse nameText.Contains("cost") _
            OrElse nameText.Contains("price") _
            OrElse nameText.Contains("sale") _
            OrElse nameText.Contains("newsale") _
            OrElse nameText.Contains("qty") _
            OrElse nameText.Contains("qyt") _
            OrElse nameText.Contains("كمية") _
            OrElse nameText.Contains("الكمية") _
            OrElse nameText.Contains("عدد") _
            OrElse nameText.Contains("التكلفة") _
            OrElse nameText.Contains("البيع")
    End Function

    Private Shared Function TryReadDecimal(value As Object, ByRef result As Decimal) As Boolean
        result = 0D

        If value Is Nothing OrElse value Is DBNull.Value Then Return False

        Try
            If TypeOf value Is Decimal OrElse TypeOf value Is Double OrElse TypeOf value Is Single OrElse TypeOf value Is Integer OrElse TypeOf value Is Long OrElse TypeOf value Is Short Then
                result = Convert.ToDecimal(value)
                Return True
            End If
        Catch
            Return False
        End Try

        Dim text As String = value.ToString().Trim()
        If String.IsNullOrWhiteSpace(text) Then Return False

        Dim styles As NumberStyles = NumberStyles.AllowDecimalPoint Or NumberStyles.AllowThousands Or NumberStyles.AllowLeadingSign Or NumberStyles.AllowTrailingSign
        If Decimal.TryParse(text, styles, CultureInfo.CurrentCulture, result) Then Return True
        If Decimal.TryParse(text, styles, CultureInfo.InvariantCulture, result) Then Return True
        If Decimal.TryParse(text, styles, New CultureInfo("ar-LY"), result) Then Return True

        Return False
    End Function
End Class

Public Class ManualFormatPrintColumn
    Public Property Name As String = ""
    Public Property HeaderText As String = ""
    Public Property WidthValue As Integer = 80
    Public Property Alignment As String = "Center"
    Public Property HasTotal As Boolean = False
    Public Property TotalValue As Decimal = 0D
    Public Property TotalValueCount As Integer = 0
    Public Property DisplayFormat As String = ""
End Class
