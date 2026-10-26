Option Strict On
Imports System.Windows.Forms

Friend NotInheritable Class AuditSalesDeleteUi
    Friend Shared Function Run(owner As Form, bill As Integer, line As Integer, displayed As Decimal, onUpdate As Boolean, refresh As Action) As Boolean
        Dim connection = MY_Settings.SqlConStr, completed = False
        Dim request As AuditOperationRequest = Nothing
        Try
            Dim context = AuditSecuritySession.Current.ContextFor(connection, USER_ID, owner.GetType().Name)
            request = AuditSalesDelete.Request(context, AuditSalesDelete.ReadForDelete(connection, context, bill, line, onUpdate, displayed), onUpdate)
            Save(connection, request) : completed = True
        Catch ex As Exception
            Dim status = If(request Is Nothing, "NotFound", AuditSalesQuantityUi.Status(connection, request))
            If status = "Completed" Then
                completed = True
            ElseIf status = "NotFound" Then
                MessageBox.Show(owner, "لم يُحفظ حذف الصنف. " & ex.Message, "حذف الصنف", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Using dialog As New AuditQuantityRecoveryDialog(connection, request, ex.Message, AddressOf Save)
                    completed = dialog.ShowDialog(owner) = DialogResult.OK
                End Using
            End If
        End Try
        Try
            refresh()
        Catch ex As Exception
            MessageBox.Show(owner, If(completed, "حُذف الصنف؛ ", "لم يُحذف الصنف؛ ") & "تعذر تحديث العرض: " & ex.Message, "تحديث الفاتورة", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
        Return completed
    End Function

    Private Shared Sub Save(connection As String, request As AuditOperationRequest)
        Dim current = AuditSecuritySession.Current.ContextFor(MY_Settings.SqlConStr, USER_ID, request.Context.ScreenCode)
        If connection <> MY_Settings.SqlConStr OrElse current.SourceId <> request.Context.SourceId OrElse current.ActorUserId <> request.Context.ActorUserId OrElse current.SessionId <> request.Context.SessionId Then Throw New InvalidOperationException("تغير الاتصال أو المستخدم أو جلسة الدخول؛ لا يمكن إعادة الطلب بسياق مختلف.")
        AuditSalesDelete.Save(connection, request)
    End Sub
End Class
