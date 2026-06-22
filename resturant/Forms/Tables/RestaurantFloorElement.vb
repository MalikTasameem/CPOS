Imports System.Drawing

Public Class RestaurantFloorElement
    Public Property Layout_ID As Integer
    Public Property Flate_ID As Integer
    Public Property TB_ID As Integer?
    Public Property ElementType As String = "Table"
    Public Property ElementText As String = ""
    Public Property X_Pos As Integer
    Public Property Y_Pos As Integer
    Public Property WidthValue As Integer = 110
    Public Property HeightValue As Integer = 80
    Public Property RotationValue As Integer
    Public Property SeatsCount As Integer = 4
    Public Property BackColorArgb As Integer = Color.WhiteSmoke.ToArgb()
    Public Property ForeColorArgb As Integer = Color.FromArgb(15, 23, 42).ToArgb()
    Public Property ZIndex As Integer
    Public Property IsBusy As Boolean
    Public Property IsCash As Boolean

    Public ReadOnly Property Bounds As Rectangle
        Get
            Return New Rectangle(X_Pos, Y_Pos, WidthValue, HeightValue)
        End Get
    End Property

    Public Function ContainsPoint(point As Point) As Boolean
        Return Bounds.Contains(point)
    End Function
End Class
