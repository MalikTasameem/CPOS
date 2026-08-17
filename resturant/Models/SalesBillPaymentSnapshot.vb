Public Class SalesBillPaymentSnapshot

    Public Property BillTransactionID As Integer
    Public Property BillNumber As String
    Public Property AgentID As Integer
    Public Property IsAutoPaidAgent As Boolean
    Public Property OriginalPure As Decimal
    Public Property IsDepended As Boolean
    Public Property IsVoid As Boolean
    Public Property CapturedAt As DateTime
    Public Property CapturedByUserID As Integer
    Public ReadOnly Property Payments As New List(Of SalesBillPaymentSnapshotLine)

    Public ReadOnly Property NetPaidTotal As Decimal
        Get
            Return Payments.Sum(Function(payment) payment.SignedAmount)
        End Get
    End Property

    Public ReadOnly Property OriginalRemaining As Decimal
        Get
            Return OriginalPure - NetPaidTotal
        End Get
    End Property

    Public Function GetPureDifference(newPure As Decimal) As Decimal
        Return newPure - OriginalPure
    End Function

    Public Function GetUnsettledAmount(newPure As Decimal) As Decimal
        Return newPure - NetPaidTotal
    End Function

End Class
