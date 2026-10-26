Option Strict On
Imports System.Linq
Imports System.Windows.Forms

' One instance per editing dialog. The original snapshot and pending request are never silently replaced.
Friend NotInheritable Class AuditSalesLineEditUi
    Private ReadOnly owner As Form
    Private connection As String
    Private context As AuditEventContext
    Private pending As AuditOperationRequest
    Friend Property Snapshot As AuditSalesLineSnapshot

    Friend Sub New(form As Form)
        owner = form
        AddHandler owner.FormClosing, AddressOf Closing
    End Sub

    Friend ReadOnly Property HasPending As Boolean
        Get
            Return pending IsNot Nothing
        End Get
    End Property

    Friend Function Load(billId As Integer, lineId As Integer, onUpdate As Boolean) As Boolean
        Try
            connection = MY_Settings.SqlConStr
            context = AuditSecuritySession.Current.ContextFor(connection, USER_ID, owner.GetType().Name)
            Snapshot = AuditSalesLineEdit.ReadForEdit(connection, context, billId, lineId, onUpdate)
            Return True
        Catch ex As Exception
            MessageBox.Show(owner, ex.Message, "تحميل سطر البيع", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End Try
    End Function

    Friend Function Save(factory As Func(Of AuditEventContext, AuditOperationRequest)) As Boolean
        Try
            If context Is Nothing OrElse Snapshot Is Nothing Then Throw New InvalidOperationException("لم تُحمّل لقطة السطر؛ أعد فتح النافذة.")
            Dim current = AuditSecuritySession.Current.ContextFor(MY_Settings.SqlConStr, USER_ID, owner.GetType().Name)
            If connection <> MY_Settings.SqlConStr OrElse current.SourceId <> context.SourceId OrElse current.ActorUserId <> context.ActorUserId OrElse current.SessionId <> context.SessionId Then Throw New InvalidOperationException("تغير الاتصال أو جلسة المستخدم؛ لا يمكن إرسال محاولة النافذة السابقة.")
            If pending Is Nothing Then
                If factory Is Nothing Then Throw New InvalidOperationException("لا توجد محاولة محفوظة.")
                pending = factory(context.Copy())
            End If
            ' A retry uses the original payload even if controls have since changed.
            AuditSalesLineEdit.Save(connection, pending)
            pending = Nothing
            Return True
        Catch ex As Exception
            Dim message = ex.Message
            If pending IsNot Nothing Then
                Dim reference = pending.RequestId.ToString()
                Try
                    Dim status = AuditRequestCoordinator.Status(connection, pending)
                    If status.Status = "Completed" Then
                        pending = Nothing : Return True
                    End If
                    If status.Status = "NotFound" Then pending = Nothing
                Catch
                End Try
                message = If(pending Is Nothing, "لم يُحفظ التعديل. ", "نتيجة الحفظ غير محسومة؛ اضغط تأكيد لإعادة المحاولة بالقيم الأصلية نفسها. ") & "مرجع: " & reference & Environment.NewLine & message
            End If
            MessageBox.Show(owner, message, "مراقبة تعديل سطر البيع", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End Try
    End Function

    Private Sub Closing(sender As Object, e As FormClosingEventArgs)
        If pending Is Nothing Then Return
        Try
            If AuditRequestCoordinator.Status(connection, pending).Status = "NotFound" Then
                pending = Nothing : Return
            End If
        Catch
        End Try
        e.Cancel = True
        MessageBox.Show(owner, "يلزم حسم محاولة الحفظ وتحديث الفاتورة؛ استعد الاتصال ثم اضغط تأكيد. لن تُرسل القيم الجديدة بدل المحاولة الأصلية.", "نتيجة حفظ غير محسومة", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub
End Class
