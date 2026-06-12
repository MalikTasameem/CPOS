Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class SalesPrintData
    Public Property StoreTitle As String = ""
    Public Property StoreAddress As String = ""
    Public Property Footer As String = ""
    Public Property BillNo As String = ""
    Public Property BillID As String = ""
    Public Property TID As Integer
    Public Property BillDate As String = ""
    Public Property CustomerName As String = ""
    Public Property ProjectName As String = ""
    Public Property UserName As String = ""
    Public Property Notes As String = ""
    Public Property TotalText As String = ""
    Public Property DiscountText As String = ""
    Public Property PureText As String = ""
    Public Property PaidText As String = ""
    Public Property RestText As String = ""
    Public Property QtyText As String = ""
    Public Property CountText As String = ""
    Public Property Barcode As String = ""
    Public Property LogoImage As Image = Nothing
    Public Property Items As New DataTable()

    Public Shared Function FromSalesForm(form As Sales) As SalesPrintData
        Dim data As New SalesPrintData()

        data.StoreTitle = SBill_Title_1
        data.StoreAddress = SBill_Title_2
        data.Footer = SBill_Footer
        data.TID = form.T_ID
        data.BillNo = SafeText(form.BillNumTxt)
        data.BillID = SafeText(form.Bill_ID_Txt)
        data.BillDate = If(form.DateTimeEx Is Nothing, Date.Now.ToString("yyyy/MM/dd HH:mm"), form.DateTimeEx.Value.ToString("yyyy/MM/dd HH:mm"))
        data.CustomerName = If(form.AG_Cm Is Nothing, "", form.AG_Cm.Textt)
        data.ProjectName = If(form.Project_cm Is Nothing OrElse form.Project_cm.SelectedIndex < 0, "", form.Project_cm.Text)
        data.UserName = USER_NAME
        data.Notes = SafeText(form.Notes_txt)
        data.TotalText = SafeText(form.Total_TextBox1)
        data.DiscountText = SafeText(form.Discount_txt1)
        data.PureText = SafeText(form.Pure_txt)
        data.PaidText = SafeText(form.Piedmoney_txt)
        data.RestText = CalculateRestText(data.PureText, data.PaidText)
        data.QtyText = If(form.IM_Qty_LB Is Nothing, "", form.IM_Qty_LB.Text)
        data.CountText = If(form.IM_Count_LB Is Nothing, "", form.IM_Count_LB.Text)
        data.Barcode = form.Barcode
        data.LogoImage = LoadLogoImage()
        data.Items = BuildItemsTable(form.AGMetroGrid)

        Return data
    End Function

    Public Shared Function FromSalesFastForm(form As Sales_Fast) As SalesPrintData
        Dim data As New SalesPrintData()

        data.StoreTitle = SBill_Title_1
        data.StoreAddress = SBill_Title_2
        data.Footer = SBill_Footer
        data.TID = form.T_ID
        data.BillNo = SafeText(form.Bill_ID_Txt)
        data.BillID = SafeText(form.Bill_ID_Txt)
        data.BillDate = If(form.DateTimeEx Is Nothing, Date.Now.ToString("yyyy/MM/dd HH:mm"), form.DateTimeEx.Value.ToString("yyyy/MM/dd HH:mm"))
        data.CustomerName = If(form.AG_SH_txt Is Nothing, "", form.AG_SH_txt.Text)
        data.ProjectName = ""
        data.UserName = USER_NAME
        data.Notes = SafeText(form.Notes_txt)
        data.TotalText = SafeText(form.Total_TextBox)
        data.DiscountText = SafeText(form.Discount_txt)
        data.PureText = SafeText(form.Pure_txt)
        data.PaidText = data.PureText
        data.RestText = "0"
        data.QtyText = If(form.IM_Qty_LB Is Nothing, "", form.IM_Qty_LB.Text)
        data.CountText = If(form.IM_Count_LB Is Nothing, "", form.IM_Count_LB.Text)
        data.Barcode = form.Barcode
        data.LogoImage = LoadLogoImage()
        data.Items = BuildItemsTable(form.AGMetroGrid)

        Return data
    End Function

    Private Shared Function BuildItemsTable(grid As DataGridView) As DataTable
        Dim dt As New DataTable()
        If grid Is Nothing Then Return dt

        For Each col As DataGridViewColumn In grid.Columns
            If dt.Columns.Contains(col.Name) = False Then dt.Columns.Add(col.Name, GetType(String))
        Next

        For Each row As DataGridViewRow In grid.Rows
            If row.IsNewRow Then Continue For

            Dim newRow As DataRow = dt.NewRow()
            For Each col As DataGridViewColumn In grid.Columns
                Dim value As Object = row.Cells(col.Name).Value
                newRow(col.Name) = If(value Is Nothing OrElse value Is DBNull.Value, "", value.ToString())
            Next
            dt.Rows.Add(newRow)
        Next

        Return dt
    End Function

    Private Shared Function SafeText(txt As TextBox) As String
        If txt Is Nothing Then Return ""
        Return txt.Text
    End Function

    Private Shared Function CalculateRestText(pureText As String, paidText As String) As String
        Dim pureValue As Decimal = 0D
        Dim paidValue As Decimal = 0D

        Decimal.TryParse(pureText, pureValue)
        Decimal.TryParse(paidText, paidValue)

        Return (pureValue - paidValue).ToString(N_Point_Fter)
    End Function

    Private Shared Function LoadLogoImage() As Image
        Try
            Using cn As New SqlConnection(MY_Settings.SqlConStr)
                Using cmd As New SqlCommand("SELECT TOP 1 LOGO FROM SysSetting", cn)
                    cn.Open()
                    Dim result As Object = cmd.ExecuteScalar()
                    If result Is Nothing OrElse result Is DBNull.Value Then Return Nothing

                    Dim bytes As Byte() = DirectCast(result, Byte())
                    Using ms As New MemoryStream(bytes)
                        Using source As Image = Image.FromStream(ms)
                            Return New Bitmap(source)
                        End Using
                    End Using
                End Using
            End Using
        Catch
            Return Nothing
        End Try
    End Function
End Class
