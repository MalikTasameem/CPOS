Option Strict On
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms

' Ordinary receipts only. Custody operations deliberately do not enter this adapter.
Friend NotInheritable Class AuditReceiptUi
    Friend Shared Function Run(owner As Form, command As SqlCommand, ByRef savedId As Integer, ByRef savedNumber As Integer) As Boolean
        Dim connection = MY_Settings.SqlConStr, completed = False, submitted = False
        Dim request As AuditOperationRequest = Nothing, result As AuditRequestResult = Nothing
        savedId = 0 : savedNumber = 0
        Dim save As Action(Of String, AuditOperationRequest) = Sub(cs, r)
            Dim current = AuditSecuritySession.Current.ContextFor(MY_Settings.SqlConStr, USER_ID, "Receipt")
            If cs <> MY_Settings.SqlConStr OrElse current.SourceId <> r.Context.SourceId OrElse current.ActorUserId <> r.Context.ActorUserId OrElse current.SessionId <> r.Context.SessionId Then Throw New InvalidOperationException("تغير اتصال أو مستخدم أو جلسة طلب الإيصال.")
            result = AuditReceipt.Save(cs, r)
        End Sub
        Try
            If owner Is Nothing OrElse owner.GetType().Name <> "Receipt" Then Throw New ArgumentException("لم تتحدد شاشة الإيصال.")
            Dim context = AuditSecuritySession.Current.ContextFor(connection, USER_ID, "Receipt")
            request = AuditReceipt.Request(context, command)
            AuditReceipt.Prepare(connection, request)
            submitted = True
            save(connection, request) : completed = True
        Catch ex As Exception
            Dim status = If(Not submitted, "NotFound", AuditSalesQuantityUi.Status(connection, request))
            If status = "Completed" Then
                completed = True
            ElseIf status = "NotFound" Then
                MessageBox.Show(owner, "لم تُحفظ عملية الإيصال. " & ex.Message, "الإيصالات", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Using dialog As New AuditQuantityRecoveryDialog(connection, request, ex.Message, save)
                    completed = dialog.ShowDialog(owner) = DialogResult.OK
                End Using
            End If
        End Try
        If completed Then
            Try
                If result Is Nothing Then result = AuditRequestCoordinator.Status(connection, request)
                Dim values = AuditCanonical.ParseXml(result.ResultXml).Element("Values").Elements("Value")
                Dim id = Integer.Parse(values.Single(Function(x) CStr(x.Attribute("name")) = "EntityId").Value, CultureInfo.InvariantCulture)
                Dim number = Integer.Parse(values.Single(Function(x) CStr(x.Attribute("name")) = "DocumentNumber").Value, CultureInfo.InvariantCulture)
                If id <= 0 OrElse number <= 0 Then Throw New InvalidOperationException("رقم الإيصال المحفوظ غير صالح.")
                savedId = id : savedNumber = number
            Catch ex As Exception
                ' True with zero outputs means committed but not yet refreshed; the form blocks resubmission.
                MessageBox.Show(owner, "تم حفظ عملية الإيصال، لكن تعذر استرجاع رقمها. ابحث عن الإيصال ولا تكرر الحفظ. " & ex.Message, "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If
        Return completed
    End Function

    Friend Shared Sub Refresh(owner As Form, work As Action)
        Try
            work()
        Catch ex As Exception
            MessageBox.Show(owner, "تم حفظ عملية الإيصال؛ تعذر تحديث العرض فقط. " & ex.Message, "الإيصالات", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
End Class
