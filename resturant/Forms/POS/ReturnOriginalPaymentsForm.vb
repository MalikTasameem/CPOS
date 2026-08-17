Imports System.Data.SqlClient

Public Class ReturnOriginalPaymentsForm

    Private ReadOnly OriginalBillTID As Integer
    Private ReadOnly OriginalBillNumber As String
    Private ReadOnly IsSalesBill As Boolean

    Public Sub New(originalBillTID As Integer, originalBillNumber As String, isSalesBill As Boolean)
        InitializeComponent()
        Me.OriginalBillTID = originalBillTID
        Me.OriginalBillNumber = If(originalBillNumber, "")
        Me.IsSalesBill = isSalesBill
    End Sub

    Private Sub ReturnOriginalPaymentsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TransactionTypeLabel.Text = If(IsSalesBill, "فاتورة مبيعات", "فاتورة مشتريات")
        BillNumberValueLabel.Text = If(String.IsNullOrWhiteSpace(OriginalBillNumber), "---", OriginalBillNumber)
        LoadOriginalPayments()
    End Sub

    Private Sub LoadOriginalPayments()
        Dim paymentsTable As New DataTable()
        Dim invoiceTotal As Decimal = 0D

        Try
            Const sql As String =
                "SELECT R.Receipt_Num, " &
                "COALESCE(NULLIF(LTRIM(RTRIM(PM.PAYMENT_NAME)), N''), ABT.Type_Name, N'غير محدد') AS PaymentName, " &
                "ISNULL(TC.Tr_Name, N'') AS TreasuryName, R.Date, ISNULL(R.Pure, 0) AS Amount " &
                "FROM Agents_Balance_MV_RCT R " &
                "LEFT JOIN PAYMENT_METHOD PM ON PM.P_ID = R.Pay_ID " &
                "LEFT JOIN AgentBalance_Type ABT ON ABT.id = R.BsType_ID " &
                "LEFT JOIN TreasuryCard TC ON TC.Tr_ID = R.Tr_ID " &
                "WHERE R.Receipt_Tran_ID = @T_ID AND R.BsType_ID = @ReceiptType AND ISNULL(R.isVoid, 0) = 0 " &
                "ORDER BY R.T_ID; " &
                "SELECT ISNULL(Pure, 0) AS InvoiceTotal FROM Agents_Balance_MV WHERE T_ID = @T_ID;"

            Using connection As New SqlConnection(MY_Settings.SqlConStr)
                Using command As New SqlCommand(sql, connection)
                    command.Parameters.Add("@T_ID", SqlDbType.Int).Value = OriginalBillTID
                    command.Parameters.Add("@ReceiptType", SqlDbType.Int).Value = If(IsSalesBill, 3, 4)

                    Dim result As New DataSet()
                    Using adapter As New SqlDataAdapter(command)
                        adapter.Fill(result)
                    End Using

                    If result.Tables.Count > 0 Then paymentsTable = result.Tables(0)
                    If result.Tables.Count > 1 AndAlso result.Tables(1).Rows.Count > 0 AndAlso
                       result.Tables(1).Rows(0)("InvoiceTotal") IsNot DBNull.Value Then
                        invoiceTotal = Convert.ToDecimal(result.Tables(1).Rows(0)("InvoiceTotal"))
                    End If
                End Using
            End Using

            PaymentsGrid.DataSource = paymentsTable

            Dim paidTotal As Decimal = 0D
            For Each row As DataRow In paymentsTable.Rows
                If row("Amount") IsNot DBNull.Value Then paidTotal += Convert.ToDecimal(row("Amount"))
            Next

            InvoiceTotalValueLabel.Text = invoiceTotal.ToString(N_Point_Fter)
            PaidTotalValueLabel.Text = paidTotal.ToString(N_Point_Fter)
            RemainingValueLabel.Text = (invoiceTotal - paidTotal).ToString(N_Point_Fter)

            EmptyPaymentsLabel.Visible = paymentsTable.Rows.Count = 0
            PaymentsGrid.Visible = paymentsTable.Rows.Count > 0
        Catch ex As Exception
            PaymentsGrid.DataSource = Nothing
            PaymentsGrid.Visible = False
            EmptyPaymentsLabel.Visible = True
            EmptyPaymentsLabel.Text = "تعذر تحميل دفعات الفاتورة." & Environment.NewLine & ex.Message
        End Try
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        Close()
    End Sub

End Class
