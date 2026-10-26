Option Strict On

Friend Enum AuditActorKind As Byte
    User = 1
    Unknown = 2
    Service = 3
End Enum

Friend Class AuditEventContext
    Public Property SourceId As Guid
    Public Property ActorKind As AuditActorKind
    Public Property ActorUserId As Integer?
    Public Property ActorServiceCode As String
    Public Property ActorName As String
    Public Property SessionId As Guid?
    Public Property MachineName As String
    Public Property ApplicationVersion As String
    Public Property ScreenCode As String

    Friend Function Copy() As AuditEventContext
        Return DirectCast(MemberwiseClone(), AuditEventContext)
    End Function
End Class
