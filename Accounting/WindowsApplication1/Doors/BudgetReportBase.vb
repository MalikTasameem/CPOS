MustInherit Class BudgetReportBase
    Protected y As Integer
    Protected g As Graphics
    Protected fontTitle As Font
    Protected fontHeader As Font
    Protected fontBody As Font
    Protected currentY As Integer

    Public Sub New(year As Integer)
        Me.y = year
        fontTitle = New Font("Segoe UI", 14, FontStyle.Bold)
        fontHeader = New Font("Segoe UI", 10, FontStyle.Bold)
        fontBody = New Font("Segoe UI", 10)
    End Sub

    Public MustOverride Sub LoadData()
    Public MustOverride Sub Print(g As Graphics)
End Class

