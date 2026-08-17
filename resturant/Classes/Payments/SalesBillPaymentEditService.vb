Imports System.Data.SqlClient

Public NotInheritable Class SalesBillPaymentEditService

    Private Sub New()
    End Sub

    Public Shared Function IsReconciliationAvailable() As Boolean
        Try
            Using connection As New SqlConnection(MY_Settings.SqlConStr)
                connection.Open()
                Const sql As String =
                    "SELECT CASE WHEN TYPE_ID(N'dbo.SalePaymentAllocationType') IS NOT NULL " &
                    "AND OBJECT_ID(N'dbo.SB_ReconcileEditedBillPayments_V2', N'P') IS NOT NULL " &
                    "THEN 1 ELSE 0 END"
                Using command As New SqlCommand(sql, connection)
                    Return Convert.ToInt32(command.ExecuteScalar()) = 1
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

    Public Shared Function CaptureSnapshot(billTransactionID As Integer,
                                           userID As Integer) As SalesBillPaymentSnapshot
        If billTransactionID <= 0 Then
            Throw New ArgumentOutOfRangeException("billTransactionID", "رقم حركة الفاتورة غير صحيح.")
        End If

        Using connection As New SqlConnection(MY_Settings.SqlConStr)
            connection.Open()
            Return CaptureSnapshot(connection, billTransactionID, userID)
        End Using
    End Function

    Public Shared Function CaptureSnapshot(connection As SqlConnection,
                                           billTransactionID As Integer,
                                           userID As Integer) As SalesBillPaymentSnapshot
        If connection Is Nothing OrElse connection.State <> ConnectionState.Open Then
            Throw New InvalidOperationException("يجب أن يكون اتصال قاعدة البيانات مفتوحًا.")
        End If

        Const sql As String =
            "SELECT B.T_ID, ISNULL(CONVERT(NVARCHAR(25), B.SB_ID), N'') AS BillNumber, " &
            "B.AG_ID, ISNULL(B.Pure, 0) AS Pure, ISNULL(B.isDepended, 0) AS isDepended, " &
            "ISNULL(B.isVoid, 0) AS isVoid, B.BsType_ID, ISNULL(A.is_Auto_Pied, 0) AS is_Auto_Pied " &
            "FROM Agents_Balance_MV B LEFT JOIN Agents A ON A.AG_ID = B.AG_ID WHERE B.T_ID = @T_ID; " &
            "SELECT R.T_ID, R.Receipt_Num, R.BsType_ID, R.Pay_ID, R.Tr_ID, " &
            "COALESCE(NULLIF(LTRIM(RTRIM(PM.PAYMENT_NAME)), N''), ABT.Type_Name, N'غير محدد') AS PaymentName, " &
            "ISNULL(TC.Tr_Name, N'') AS TreasuryName, ABS(ISNULL(R.Pure, 0)) AS Amount " &
            "FROM Agents_Balance_MV_RCT R " &
            "LEFT JOIN PAYMENT_METHOD PM ON PM.P_ID = R.Pay_ID " &
            "LEFT JOIN AgentBalance_Type ABT ON ABT.id = R.BsType_ID " &
            "LEFT JOIN TreasuryCard TC ON TC.Tr_ID = R.Tr_ID " &
            "WHERE R.Receipt_Tran_ID = @T_ID AND R.BsType_ID IN (3, 4) " &
            "AND ISNULL(R.isVoid, 0) = 0 ORDER BY R.T_ID;"

        Dim result As New DataSet()
        Using command As New SqlCommand(sql, connection)
            command.Parameters.Add("@T_ID", SqlDbType.Int).Value = billTransactionID
            Using adapter As New SqlDataAdapter(command)
                adapter.Fill(result)
            End Using
        End Using

        If result.Tables.Count = 0 OrElse result.Tables(0).Rows.Count = 0 Then
            Throw New InvalidOperationException("لم يتم العثور على الفاتورة المطلوبة.")
        End If

        Dim billRow As DataRow = result.Tables(0).Rows(0)
        If Convert.ToInt32(billRow("BsType_ID")) <> 1 Then
            Throw New InvalidOperationException("المعاملة المحددة ليست فاتورة مبيعات.")
        End If

        Dim snapshot As New SalesBillPaymentSnapshot With {
            .BillTransactionID = Convert.ToInt32(billRow("T_ID")),
            .BillNumber = Convert.ToString(billRow("BillNumber")),
            .AgentID = Convert.ToInt32(billRow("AG_ID")),
            .IsAutoPaidAgent = Convert.ToBoolean(billRow("is_Auto_Pied")),
            .OriginalPure = Convert.ToDecimal(billRow("Pure")),
            .IsDepended = Convert.ToBoolean(billRow("isDepended")),
            .IsVoid = Convert.ToBoolean(billRow("isVoid")),
            .CapturedAt = DateTime.Now,
            .CapturedByUserID = userID
        }

        If result.Tables.Count > 1 Then
            For Each paymentRow As DataRow In result.Tables(1).Rows
                snapshot.Payments.Add(New SalesBillPaymentSnapshotLine With {
                    .ReceiptTransactionID = Convert.ToInt32(paymentRow("T_ID")),
                    .ReceiptNumber = NullableInteger(paymentRow("Receipt_Num")),
                    .ReceiptTypeID = Convert.ToInt32(paymentRow("BsType_ID")),
                    .PaymentMethodID = NullableInteger(paymentRow("Pay_ID")),
                    .TreasuryID = NullableInteger(paymentRow("Tr_ID")),
                    .PaymentName = Convert.ToString(paymentRow("PaymentName")),
                    .TreasuryName = Convert.ToString(paymentRow("TreasuryName")),
                    .Amount = Convert.ToDecimal(paymentRow("Amount"))
                })
            Next
        End If

        Return snapshot
    End Function

    Public Shared Function ReconcileAutoPaidBill(snapshot As SalesBillPaymentSnapshot,
                                                 newPure As Decimal,
                                                 payments As IEnumerable(Of SalePaymentAllocation),
                                                 userID As Integer,
                                                 periodID As Integer) As SalesPaymentReconciliationResult
        If snapshot Is Nothing Then Throw New ArgumentNullException("snapshot")
        If newPure < 0D Then Throw New ArgumentOutOfRangeException("newPure")

        Using connection As New SqlConnection(MY_Settings.SqlConStr)
            Using command As New SqlCommand("dbo.SB_ReconcileEditedBillPayments_V2", connection)
                command.CommandType = CommandType.StoredProcedure
                command.CommandTimeout = 120
                command.Parameters.Add("@T_ID", SqlDbType.Int).Value = snapshot.BillTransactionID
                AddDecimalParameter(command, "@ExpectedOriginalPure", snapshot.OriginalPure)
                AddDecimalParameter(command, "@ExpectedNewPure", newPure)
                AddDecimalParameter(command, "@ExpectedNetPaid", snapshot.NetPaidTotal)
                command.Parameters.Add("@User_ID", SqlDbType.Int).Value = userID
                command.Parameters.Add("@Pr_ID", SqlDbType.Int).Value = periodID
                SalesPaymentSqlLayer.AddPaymentsParameter(command, payments)

                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    If Not reader.Read() Then
                        Return New SalesPaymentReconciliationResult With {
                            .IsSuccess = False,
                            .ErrorMessage = "لم يتم استلام نتيجة من إجراء تسوية الدفعات."
                        }
                    End If

                    Dim result As New SalesPaymentReconciliationResult With {
                        .IsSuccess = Convert.ToBoolean(reader("IsSuccess"))
                    }

                    If result.IsSuccess Then
                        result.OriginalPure = ReadDecimal(reader, "OriginalPure")
                        result.NewPure = ReadDecimal(reader, "NewPure")
                        result.Adjustment = ReadDecimal(reader, "Adjustment")
                        result.FinalNetPaid = ReadDecimal(reader, "FinalNetPaid")
                    ElseIf HasColumn(reader, "ErrorMessage") AndAlso reader("ErrorMessage") IsNot DBNull.Value Then
                        result.ErrorMessage = reader("ErrorMessage").ToString()
                    Else
                        result.ErrorMessage = "فشلت تسوية دفعات الفاتورة."
                    End If

                    Return result
                End Using
            End Using
        End Using
    End Function

    Private Shared Sub AddDecimalParameter(command As SqlCommand, name As String, value As Decimal)
        Dim parameter As SqlParameter = command.Parameters.Add(name, SqlDbType.Decimal)
        parameter.Precision = 18
        parameter.Scale = 3
        parameter.Value = value
    End Sub

    Private Shared Function ReadDecimal(reader As SqlDataReader, columnName As String) As Decimal
        If Not HasColumn(reader, columnName) OrElse reader(columnName) Is DBNull.Value Then Return 0D
        Return Convert.ToDecimal(reader(columnName))
    End Function

    Private Shared Function HasColumn(reader As SqlDataReader, columnName As String) As Boolean
        For index As Integer = 0 To reader.FieldCount - 1
            If String.Equals(reader.GetName(index), columnName, StringComparison.OrdinalIgnoreCase) Then Return True
        Next
        Return False
    End Function

    Private Shared Function NullableInteger(value As Object) As Integer?
        If value Is Nothing OrElse value Is DBNull.Value Then Return Nothing
        Return Convert.ToInt32(value)
    End Function

End Class
