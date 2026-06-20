Imports System.Data.SqlClient

Module Module2
    Public Function GetItemBudgetSummary(itemId As Integer, year As Integer) As BudgetSummary
        Dim result As New BudgetSummary()

        Using cn As New SqlConnection(MY_Settings.SqlConStr)
            Using cmd As New SqlCommand("
SELECT
    Allocated,
    Spent,
    Reserved,
    Available
FROM dbo.Vw_BudgetItemSummary
WHERE BudgetItemId = @ItemId
  AND FiscalYear = @Y;", cn)

                cmd.Parameters.AddWithValue("@ItemId", itemId)
                cmd.Parameters.AddWithValue("@Y", year)

                cn.Open()
                Using dr = cmd.ExecuteReader()
                    If dr.Read() Then
                        result.Allocated = Convert.ToDecimal(dr("Allocated"))
                        result.Spent = Convert.ToDecimal(dr("Spent"))
                        result.Reserved = Convert.ToDecimal(dr("Reserved"))
                        result.Available = Convert.ToDecimal(dr("Available"))
                    Else
                        ' لا يوجد اعتماد
                        result.Allocated = 0
                        result.Spent = 0
                        result.Reserved = 0
                        result.Available = 0
                    End If
                End Using
            End Using
        End Using

        Return result
    End Function


    '    Public Function GetItemBudgetSummary(itemId As Integer, year As Integer) As BudgetSummary
    '        Dim result As New BudgetSummary()
    '        Dim ConnStr As String = MY_Settings.SqlConStr
    '        Using cn As New SqlConnection(ConnStr)
    '            cn.Open()

    '            Using cmdA As New SqlCommand("
    'SELECT ISNULL(AllocatedAmount, 0)
    'FROM Budget_Allocations
    'WHERE BudgetItemId = @ItemId AND FiscalYear = @Y;", cn)

    '                cmdA.Parameters.AddWithValue("@ItemId", itemId)
    '                cmdA.Parameters.AddWithValue("@Y", year)
    '                result.Allocated = Convert.ToDecimal(cmdA.ExecuteScalar())
    '            End Using

    '            Using cmdS As New SqlCommand("
    'SELECT ISNULL(SUM(Amount), 0)
    'FROM Budget_Entries
    'WHERE BudgetItemId = @ItemId AND FiscalYear = @Y AND EntryType = 1;", cn)

    '                cmdS.Parameters.AddWithValue("@ItemId", itemId)
    '                cmdS.Parameters.AddWithValue("@Y", year)
    '                result.Spent = Convert.ToDecimal(cmdS.ExecuteScalar())
    '            End Using

    '            Using cmdR As New SqlCommand("
    'SELECT ISNULL(SUM(Amount), 0)
    'FROM Budget_Entries
    'WHERE BudgetItemId = @ItemId AND FiscalYear = @Y AND EntryType = 2;", cn)

    '                cmdR.Parameters.AddWithValue("@ItemId", itemId)
    '                cmdR.Parameters.AddWithValue("@Y", year)
    '                result.Reserved = Convert.ToDecimal(cmdR.ExecuteScalar())
    '            End Using
    '        End Using

    '        result.Available = result.Allocated - result.Spent - result.Reserved
    '        Return result
    '    End Function
End Module
