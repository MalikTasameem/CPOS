Option Strict On
Imports System.Collections.Generic

Friend Class AuditOperationRequest
    Public Property RequestId As Guid
    Public Property OperationCode As String
    Public Property Context As AuditEventContext
    Public Property EntityType As String
    Public Property EntityId As String
    Public ReadOnly Inputs As New List(Of AuditInput)()
End Class

Friend Class AuditEntityReference
    Public Property Role As String
    Public Property EntityType As String
    Public Property EntityId As String
End Class

Friend Class AuditEventRequest
    Public Property EventId As Guid
    Public Property EventOrdinal As Integer
    Public Property CorrelationId As Guid?
    Public Property EventCode As String
    Public Property Outcome As Byte
    Public Property EntityType As String
    Public Property EntityId As String
    Public Property DocumentNumber As String
    Public Property OccurredAtUtc As DateTime
    Public Property BusinessDate As DateTime?
    Public Property ReasonCode As String
    Public ReadOnly Changes As New List(Of AuditFieldChange)()
    Public ReadOnly RelatedEntities As New List(Of AuditEntityReference)()
End Class

Friend Enum AuditWriteResult
    AddedPendingCommit = 1
    AlreadyPresent = 2
End Enum

Friend Class AuditRequestResult
    ' NewPendingCommit/Completed/NotFound/InProgressOrUnknown/IncompleteCommitted.
    Public Property Status As String
    Public Property ResultCode As Byte?
    Public Property ResultXml As String
End Class
