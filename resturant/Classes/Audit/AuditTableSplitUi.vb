Option Strict On
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms

Friend NotInheritable Class AuditTableSplitUi
    Friend Shared Function Run(owner As Form, operation As String, source As Integer, destination As Integer, bill As Integer, values As IDictionary(Of String, Object), ByRef savedBill As Integer) As Boolean
        Dim connection = MY_Settings.SqlConStr, request As AuditOperationRequest = Nothing, result As AuditRequestResult = Nothing, completed = False
        savedBill = bill
        Dim save As Action(Of String, AuditOperationRequest) = Sub(cs, r)
            Dim current = AuditSecuritySession.Current.ContextFor(MY_Settings.SqlConStr, USER_ID, r.Context.ScreenCode)
            If cs <> MY_Settings.SqlConStr OrElse current.SourceId <> r.Context.SourceId OrElse current.ActorUserId <> r.Context.ActorUserId OrElse current.SessionId <> r.Context.SessionId Then Throw New InvalidOperationException("تغير الاتصال أو مستخدم أو جلسة نقل الطاولة.")
            result = AuditTableSplit.Save(cs, r)
        End Sub
        Try
            Dim context = AuditSecuritySession.Current.ContextFor(connection, USER_ID, owner.GetType().Name)
            request = AuditTableSplit.Request(context, operation, source, destination, bill, values)
            AuditTableSplit.Prepare(connection, request)
            save(connection, request) : completed = True
        Catch ex As Exception
            Dim status = If(request Is Nothing, "NotFound", AuditSalesQuantityUi.Status(connection, request))
            If status = "Completed" Then
                completed = True
            ElseIf status = "NotFound" Then
                MessageBox.Show(owner, "لم تحفظ عملية نقل الطاولة. " & ex.Message, "نقل أصناف الطاولة", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Using dialog As New AuditQuantityRecoveryDialog(connection, request, ex.Message, save)
                    completed = dialog.ShowDialog(owner) = DialogResult.OK
                End Using
            End If
        End Try
        If completed Then
            Try
                If result Is Nothing Then result = AuditRequestCoordinator.Status(connection, request)
                Dim xml = AuditCanonical.ParseXml(result.ResultXml)
                savedBill = Integer.Parse(xml.Element("Values").Elements("Value").Single(Function(x) CStr(x.Attribute("name")) = "EntityId").Value, CultureInfo.InvariantCulture)
            Catch ex As Exception
                MessageBox.Show(owner, "حفظ النقل، لكن تعذر استرجاع رقم فاتورته؛ لا تكرر العملية. " & ex.Message, "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If
        Return completed
    End Function
End Class
