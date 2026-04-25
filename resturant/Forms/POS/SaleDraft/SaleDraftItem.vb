Public Class SaleDraftItem

    Public Property DraftLineId As String

    ' بيانات مطابقة لـ SB_Contents
    Public Property IM_ID As Integer
    Public Property U_ID As Long
    Public Property ST_ID As Long
    Public Property Date_ As DateTime
    Public Property Compons As String
    Public Property Cost As Double?
    Public Property Price As Decimal
    Public Property D_Vaild As String
    Public Property QTY As Decimal
    Public Property T_Price As Decimal
    Public Property U_Cargo As Double
    Public Property ST_QTY As Decimal
    Public Property isDepended As Integer = 0
    Public Property Barcode As String

    ' بيانات عرض
    Public Property ItemName As String
    Public Property UnitName As String


End Class