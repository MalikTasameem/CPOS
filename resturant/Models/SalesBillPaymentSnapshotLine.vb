Public Class SalesBillPaymentSnapshotLine

    Public Property ReceiptTransactionID As Integer
    Public Property ReceiptNumber As Integer?
    Public Property ReceiptTypeID As Integer
    Public Property PaymentMethodID As Integer?
    Public Property TreasuryID As Integer?
    Public Property PaymentName As String
    Public Property TreasuryName As String
    Public Property Amount As Decimal

    Public ReadOnly Property SignedAmount As Decimal
        Get
            If ReceiptTypeID = 4 Then Return -Amount
            Return Amount
        End Get
    End Property

End Class
