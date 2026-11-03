Option Strict On
Imports System.Data.SqlClient
Imports System.Windows.Forms

Friend NotInheritable Class AuditTablesUi
    Friend Shared Function Run(owner As Form, command As SqlCommand, Optional displayedQuantity As Decimal = 0D) As Boolean
        Dim connection = MY_Settings.SqlConStr, completed = False
        Dim request As AuditOperationRequest = Nothing
        Dim save As Action(Of String, AuditOperationRequest) = Sub(cs, r)
            Dim current = AuditSecuritySession.Current.ContextFor(MY_Settings.SqlConStr, USER_ID, r.Context.ScreenCode)
            If cs <> MY_Settings.SqlConStr OrElse current.SourceId <> r.Context.SourceId OrElse current.ActorUserId <> r.Context.ActorUserId OrElse current.SessionId <> r.Context.SessionId Then Throw New InvalidOperationException("تغير اتصال أو مستخدم أو جلسة عملية الطاولة.")
            AuditTables.Save(cs, r)
        End Sub
        Try
            If owner Is Nothing Then Throw New ArgumentException("لم تتحدد شاشة الطاولة.")
            Dim context = AuditSecuritySession.Current.ContextFor(connection, USER_ID, owner.GetType().Name)
            request = AuditTables.Request(context, command, AuditTables.Read(connection, context, command), displayedQuantity)
            save(connection, request) : completed = True
        Catch ex As Exception
            Dim status = If(request Is Nothing, "NotFound", AuditSalesQuantityUi.Status(connection, request))
            If status = "Completed" Then
                completed = True
            ElseIf status = "NotFound" Then
                MessageBox.Show(owner, "لم تُحفظ عملية الطاولة. " & ex.Message, "الطاولات", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Using dialog As New AuditQuantityRecoveryDialog(connection, request, ex.Message, save)
                    completed = dialog.ShowDialog(owner) = DialogResult.OK
                End Using
            End If
        End Try
        Return completed
    End Function

    Friend Shared Sub Refresh(owner As Form, work As Action)
        Try
            work()
        Catch ex As Exception
            MessageBox.Show(owner, "تم حفظ عملية الطاولة؛ تعذر تحديث العرض أو الطباعة فقط. لا تكرر الحفظ. " & ex.Message, "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
End Class
