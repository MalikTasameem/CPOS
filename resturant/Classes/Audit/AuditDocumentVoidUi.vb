Option Strict On
Imports System.Windows.Forms

Friend NotInheritable Class AuditDocumentVoidUi
    Friend Shared Function Run(owner As Form, id As Integer, kind As Integer) As Boolean
        Dim connection = MY_Settings.SqlConStr
        Dim request As AuditOperationRequest = Nothing
        Try
            Dim context = AuditSecuritySession.Current.ContextFor(connection, USER_ID, owner.GetType().Name)
            request = AuditDocumentVoid.Request(context, AuditDocumentVoid.Read(connection, context, id, kind))
            Save(connection, request) : Return True
        Catch ex As Exception
            Dim status = If(request Is Nothing, "NotFound", AuditSalesQuantityUi.Status(connection, request))
            If status = "Completed" Then Return True
            If status = "NotFound" Then
                MessageBox.Show(owner, "لم يُحفظ الإلغاء. " & ex.Message, "إلغاء المستند", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
            Using dialog As New AuditQuantityRecoveryDialog(connection, request, ex.Message, AddressOf Save)
                Return dialog.ShowDialog(owner) = DialogResult.OK
            End Using
        End Try
    End Function

    Private Shared Sub Save(connection As String, request As AuditOperationRequest)
        Dim current = AuditSecuritySession.Current.ContextFor(MY_Settings.SqlConStr, USER_ID, request.Context.ScreenCode)
        If connection <> MY_Settings.SqlConStr OrElse current.SourceId <> request.Context.SourceId OrElse current.ActorUserId <> request.Context.ActorUserId OrElse current.SessionId <> request.Context.SessionId Then Throw New InvalidOperationException("تغير اتصال أو سياق محاولة الإلغاء؛ لا يمكن إعادة إرسالها بسياق مختلف.")
        AuditDocumentVoid.Save(connection, request)
    End Sub
End Class
