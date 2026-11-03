Option Strict On
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms

Friend NotInheritable Class AuditReturnUi
    Friend Shared Function Run(owner As Form, command As SqlCommand, billId As Integer, kind As Integer) As Boolean
        Dim connection = MY_Settings.SqlConStr, completed = False
        Dim request As AuditOperationRequest = Nothing, result As AuditRequestResult = Nothing
        Dim save As Action(Of String, AuditOperationRequest) = Sub(cs, r)
            Dim current = AuditSecuritySession.Current.ContextFor(MY_Settings.SqlConStr, USER_ID, r.Context.ScreenCode)
            If cs <> MY_Settings.SqlConStr OrElse current.SourceId <> r.Context.SourceId OrElse current.ActorUserId <> r.Context.ActorUserId OrElse current.SessionId <> r.Context.SessionId Then Throw New InvalidOperationException("تغير اتصال أو مستخدم أو جلسة طلب عملية صنف المرتجع.")
            result = AuditReturnLines.Save(cs, r)
        End Sub
        Try
            If owner Is Nothing Then Throw New ArgumentException("لم تتحدد شاشة عملية صنف المرتجع.")
            Dim context = AuditSecuritySession.Current.ContextFor(connection, USER_ID, owner.GetType().Name)
            If command.Parameters.Contains("@AllowNegative") Then Throw New ArgumentException("سياسة المخزون تؤخذ من إعداد الشاشة.")
            command.Parameters.AddWithValue("@AllowNegative", IM_min_QTY)
            request = AuditReturnLines.Request(context, command, billId, kind)
            AuditReturnLines.Prepare(connection, request)
            save(connection, request) : completed = True
        Catch ex As Exception
            Dim status = If(request Is Nothing, "NotFound", AuditSalesQuantityUi.Status(connection, request))
            If status = "Completed" Then
                completed = True
            ElseIf status = "NotFound" Then
                MessageBox.Show(owner, "لم تُحفظ عملية صنف المرتجع. " & ex.Message, "أصناف المرتجعات", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
            MessageBox.Show(owner, "تم حفظ عملية صنف المرتجع؛ تعذر تحديث العرض فقط. " & ex.Message, "أصناف المرتجعات", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
End Class
