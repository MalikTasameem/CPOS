Option Strict On
Imports System.Windows.Forms

Friend NotInheritable Class AuditDocumentFieldsUi
    Friend Shared Function Account(owner As Form, bill As Integer, kind As Integer, accountId As Integer, onUpdate As Boolean) As Boolean
        Return Run(owner, bill, kind, accountId, Nothing, onUpdate)
    End Function

    Friend Shared Function Discount(owner As Form, bill As Integer, kind As Integer, amount As Decimal, onUpdate As Boolean) As Boolean
        Return Run(owner, bill, kind, Nothing, amount, onUpdate)
    End Function

    Friend Shared Function FastDiscount(owner As Form, bill As Integer, amount As Decimal, percent As Decimal, onUpdate As Boolean) As Boolean
        Return Run(owner, bill, 1, Nothing, amount, onUpdate, percent)
    End Function

    Private Shared Function Run(owner As Form, bill As Integer, kind As Integer, accountId As Integer?, amount As Decimal?, onUpdate As Boolean, Optional percent As Decimal? = Nothing) As Boolean
        Dim connection = MY_Settings.SqlConStr, request As AuditOperationRequest = Nothing
        Dim save As Action(Of String, AuditOperationRequest) = Sub(cs, r)
            Dim current = AuditSecuritySession.Current.ContextFor(MY_Settings.SqlConStr, USER_ID, r.Context.ScreenCode)
            If cs <> MY_Settings.SqlConStr OrElse current.SourceId <> r.Context.SourceId OrElse current.ActorUserId <> r.Context.ActorUserId OrElse current.SessionId <> r.Context.SessionId Then Throw New InvalidOperationException("تغير الاتصال أو المستخدم أو جلسة طلب الفاتورة.")
            AuditDocumentFields.Save(cs, r)
        End Sub
        Try
            If owner Is Nothing Then Throw New ArgumentException("شاشة الفاتورة مطلوبة.")
            Dim context = AuditSecuritySession.Current.ContextFor(connection, USER_ID, owner.GetType().Name)
            request = AuditDocumentFields.Request(context, bill, kind, accountId, amount, onUpdate, percent)
            AuditDocumentFields.Prepare(connection, request)
            save(connection, request)
            Return True
        Catch ex As Exception
            Dim status = If(request Is Nothing, "NotFound", AuditSalesQuantityUi.Status(connection, request))
            If status = "Completed" Then Return True
            If status = "NotFound" Then
                MessageBox.Show(owner, "لم يُحفظ تغيير الفاتورة. " & ex.Message, "مراقبة الفاتورة", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
            Using dialog As New AuditQuantityRecoveryDialog(connection, request, ex.Message, save)
                Return dialog.ShowDialog(owner) = DialogResult.OK
            End Using
        End Try
    End Function

    Friend Shared Sub Refresh(owner As Form, work As Action, Optional committed As Boolean = True)
        Try
            work()
        Catch ex As Exception
            MessageBox.Show(owner, If(committed, "تم حفظ تغيير الفاتورة؛ تعذر تحديث العرض فقط. ", "تعذرت إعادة عرض الحساب السابق؛ راجع الفاتورة قبل المتابعة. ") & ex.Message, "مراقبة الفاتورة", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
End Class
