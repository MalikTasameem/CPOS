Imports System.Data.SqlClient

Public NotInheritable Class SalesPaymentSqlLayer

    Private Sub New()
    End Sub

    Public Shared Function IsAvailable(connection As SqlConnection) As Boolean
        If connection Is Nothing OrElse connection.State <> ConnectionState.Open Then Return False

        Try
            Const sql As String =
                "SELECT CASE WHEN TYPE_ID(N'dbo.SalePaymentAllocationType') IS NOT NULL " &
                "AND OBJECT_ID(N'dbo.SB_ConfermBill_V2', N'P') IS NOT NULL " &
                "THEN 1 ELSE 0 END"

            Using command As New SqlCommand(sql, connection)
                Return Convert.ToInt32(command.ExecuteScalar()) = 1
            End Using
        Catch
            Return False
        End Try
    End Function

    Public Shared Function IsAvailable() As Boolean
        Try
            Using connection As New SqlConnection(MY_Settings.SqlConStr)
                connection.Open()
                Return IsAvailable(connection)
            End Using
        Catch
            Return False
        End Try
    End Function

    Public Shared Function CanUseDraftMultiplePayments(agentId As Integer) As Boolean
        If agentId <= 0 Then Return False

        Try
            Using connection As New SqlConnection(MY_Settings.SqlConStr)
                connection.Open()

                Const sql As String =
                    "SELECT CASE WHEN TYPE_ID(N'dbo.SalePaymentAllocationType') IS NOT NULL " &
                    "AND OBJECT_ID(N'dbo.PushSalesDraft_V2', N'P') IS NOT NULL " &
                    "AND EXISTS (SELECT 1 FROM Agents WHERE AG_ID = @AG_ID AND ISNULL(is_Auto_Pied, 0) = 1) " &
                    "THEN 1 ELSE 0 END"

                Using command As New SqlCommand(sql, connection)
                    command.Parameters.Add("@AG_ID", SqlDbType.Int).Value = agentId
                    Return Convert.ToInt32(command.ExecuteScalar()) = 1
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

    Public Shared Function BuildPaymentsTable(payments As IEnumerable(Of SalePaymentAllocation)) As DataTable
        Dim table As New DataTable
        table.Columns.Add("LineNo", GetType(Integer))
        table.Columns.Add("PaymentMethodID", GetType(Integer))
        table.Columns.Add("TreasuryID", GetType(Integer))
        table.Columns.Add("Amount", GetType(Decimal))
        table.Columns.Add("ReferenceNumber", GetType(String))
        table.Columns.Add("BankName", GetType(String))
        table.Columns.Add("CheckNumber", GetType(String))
        table.Columns.Add("Notes", GetType(String))

        If payments Is Nothing Then Return table

        Dim lineNo As Integer = 1
        For Each payment As SalePaymentAllocation In payments
            If payment Is Nothing Then Continue For

            table.Rows.Add(
                lineNo,
                payment.PaymentMethodID,
                payment.TreasuryID,
                payment.Amount,
                DbValue(payment.ReferenceNumber),
                DBNull.Value,
                DBNull.Value,
                DbValue(payment.Notes))

            lineNo += 1
        Next

        Return table
    End Function

    Public Shared Sub AddPaymentsParameter(command As SqlCommand,
                                           payments As IEnumerable(Of SalePaymentAllocation))
        Dim parameter As New SqlParameter("@Payments", SqlDbType.Structured)
        parameter.TypeName = "dbo.SalePaymentAllocationType"
        parameter.Value = BuildPaymentsTable(payments)
        command.Parameters.Add(parameter)
    End Sub

    Public Shared Function GetPaymentsTotal(payments As IEnumerable(Of SalePaymentAllocation)) As Decimal
        If payments Is Nothing Then Return 0D
        Return payments.Where(Function(payment) payment IsNot Nothing).
            Sum(Function(payment) payment.Amount)
    End Function

    Private Shared Function DbValue(value As String) As Object
        If String.IsNullOrWhiteSpace(value) Then Return DBNull.Value
        Return value.Trim()
    End Function

End Class
