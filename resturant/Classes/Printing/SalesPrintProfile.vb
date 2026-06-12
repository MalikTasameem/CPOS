Imports System.Collections.Generic
Imports System.Drawing

Public Class SalesPrintProfile
    Public Property ProfileID As Integer
    Public Property ProfileName As String = ""
    Public Property UsageKey As String = "SALES"
    Public Property PaperKind As String = "A4"
    Public Property PrinterName As String = ""
    Public Property IsDefault As Boolean = False
    Public Property Landscape As Boolean = False
    Public Property MarginLeft As Integer = 35
    Public Property MarginRight As Integer = 35
    Public Property MarginTop As Integer = 40
    Public Property MarginBottom As Integer = 45
    Public Property LogoWidth As Integer = 72
    Public Property LogoHeight As Integer = 72
    Public Property FontFamily As String = "Segoe UI"
    Public Property TitleFontSize As Decimal = 15D
    Public Property SubTitleFontSize As Decimal = 10D
    Public Property InfoFontSize As Decimal = 9D
    Public Property HeaderFontSize As Decimal = 8D
    Public Property RowFontSize As Decimal = 8D
    Public Property TotalFontSize As Decimal = 9D
    Public Property FooterFontSize As Decimal = 8D
    Public Property TitleForeColorArgb As Integer = Color.Black.ToArgb()
    Public Property TextForeColorArgb As Integer = Color.Black.ToArgb()
    Public Property HeaderBackColorArgb As Integer = Color.FromArgb(45, 62, 80).ToArgb()
    Public Property HeaderForeColorArgb As Integer = Color.White.ToArgb()
    Public Property RowBackColorArgb As Integer = Color.White.ToArgb()
    Public Property AlternateRowBackColorArgb As Integer = Color.FromArgb(247, 249, 252).ToArgb()
    Public Property BorderColorArgb As Integer = Color.LightGray.ToArgb()
    Public Property TotalBackColorArgb As Integer = Color.FromArgb(235, 240, 245).ToArgb()
    Public Property TotalForeColorArgb As Integer = Color.Black.ToArgb()
    Public Property FooterForeColorArgb As Integer = Color.Gray.ToArgb()
    Public Property UseAlternatingRows As Boolean = True
    Public Property DrawGridLines As Boolean = True
    Public Property Components As New List(Of SalesPrintComponent)

    Public Function CloneProfile() As SalesPrintProfile
        Dim p As New SalesPrintProfile()
        p.ProfileID = ProfileID
        p.ProfileName = ProfileName
        p.UsageKey = UsageKey
        p.PaperKind = PaperKind
        p.PrinterName = PrinterName
        p.IsDefault = IsDefault
        p.Landscape = Landscape
        p.MarginLeft = MarginLeft
        p.MarginRight = MarginRight
        p.MarginTop = MarginTop
        p.MarginBottom = MarginBottom
        p.LogoWidth = LogoWidth
        p.LogoHeight = LogoHeight
        p.FontFamily = FontFamily
        p.TitleFontSize = TitleFontSize
        p.SubTitleFontSize = SubTitleFontSize
        p.InfoFontSize = InfoFontSize
        p.HeaderFontSize = HeaderFontSize
        p.RowFontSize = RowFontSize
        p.TotalFontSize = TotalFontSize
        p.FooterFontSize = FooterFontSize
        p.TitleForeColorArgb = TitleForeColorArgb
        p.TextForeColorArgb = TextForeColorArgb
        p.HeaderBackColorArgb = HeaderBackColorArgb
        p.HeaderForeColorArgb = HeaderForeColorArgb
        p.RowBackColorArgb = RowBackColorArgb
        p.AlternateRowBackColorArgb = AlternateRowBackColorArgb
        p.BorderColorArgb = BorderColorArgb
        p.TotalBackColorArgb = TotalBackColorArgb
        p.TotalForeColorArgb = TotalForeColorArgb
        p.FooterForeColorArgb = FooterForeColorArgb
        p.UseAlternatingRows = UseAlternatingRows
        p.DrawGridLines = DrawGridLines

        For Each c As SalesPrintComponent In Components
            p.Components.Add(c.CloneComponent())
        Next

        Return p
    End Function
End Class

Public Class SalesPrintComponent
    Public Property ComponentID As Integer
    Public Property ProfileID As Integer
    Public Property ComponentScope As String = "SECTION"
    Public Property ComponentCode As String = ""
    Public Property DisplayName As String = ""
    Public Property IsVisible As Boolean = True
    Public Property SortOrder As Integer = 0
    Public Property WidthValue As Integer = 80
    Public Property AlignmentValue As String = "Center"

    Public Function CloneComponent() As SalesPrintComponent
        Dim c As New SalesPrintComponent()
        c.ComponentID = ComponentID
        c.ProfileID = ProfileID
        c.ComponentScope = ComponentScope
        c.ComponentCode = ComponentCode
        c.DisplayName = DisplayName
        c.IsVisible = IsVisible
        c.SortOrder = SortOrder
        c.WidthValue = WidthValue
        c.AlignmentValue = AlignmentValue
        Return c
    End Function
End Class
