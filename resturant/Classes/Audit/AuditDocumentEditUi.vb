Option Strict On
Imports System.Windows.Forms

' One helper per form; no SQL during construction/design-time.
Friend NotInheritable Class AuditDocumentEditUi
    Private ReadOnly owner As Form
    Private ReadOnly attempt As New AuditDocumentEditAttempt()

    Friend Sub New(form As Form)
        If form Is Nothing Then Throw New ArgumentNullException("form")
        owner = form
        AddHandler owner.FormClosing, AddressOf Closing
    End Sub

    Friend Function TryOpen(documentId As Integer, documentType As Integer) As Boolean
        Try
            Dim connection = MY_Settings.SqlConStr
            Dim context = AuditSecuritySession.Current.ContextFor(connection, USER_ID, owner.GetType().Name)
            attempt.Open(connection, context, documentId, documentType)
            Return True
        Catch ex As Exception
            MessageBox.Show(owner, ex.Message, "فتح تعديل المستند", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End Try
    End Function

    Private Sub Closing(sender As Object, e As FormClosingEventArgs)
        If Not attempt.CanClose() Then
            e.Cancel = True
            MessageBox.Show(owner, "تعذر حسم نتيجة فتح التعديل. استعد الاتصال ثم أعد المحاولة أو أغلق الشاشة للتحقق مجددًا.", "مراقبة المستند", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
End Class
