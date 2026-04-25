Public Class SaleDraftHeader

    ' معلومات المسودة
    Public Property DraftId As String
    Public Property DraftName As String
    Public Property CreatedAt As DateTime
    Public Property UpdatedAt As DateTime

    ' بيانات الفاتورة (Agents_Balance_MV)
    Public Property AG_ID As Integer
    Public Property S_Bill_Pr_ID As Integer?
    Public Property Table_ID As Integer?
    Public Property [Date] As DateTime
    Public Property Total As Decimal
    Public Property Discount As Decimal
    Public Property Pure As Decimal
    Public Property About As String
    Public Property BsType_ID As Integer = 1
    Public Property isDepended As Integer = 0
    Public Property isVoid As Integer = 0
    Public Property isPied As Integer?
    Public Property User_ID As Integer
    Public Property Markter_ID As Integer?

    ' بيانات عرض فقط
    Public Property CustomerName As String
    Public Property TableName As String

    ' بعد الترحيل
    Public Property Final_T_ID As Integer?
    Public Property Final_SB_ID As Integer?
    Public Property PushedAt As DateTime?

    ' التفاصيل
    Public Property Items As New List(Of SaleDraftItem)

End Class