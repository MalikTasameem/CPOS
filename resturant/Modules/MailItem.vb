Public Class MailItem
    Public Sub New(ByVal id As Integer, ByVal name As String)
        mID = id
        mName = name
    End Sub

    Private mID As Integer
    Public Property ID() As Integer
        Get
            Return mID
        End Get
        Set(ByVal value As Integer)
            mID = value
        End Set
    End Property

    Private mName As String
    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal value As String)
            mName = value
        End Set
    End Property
End Class
