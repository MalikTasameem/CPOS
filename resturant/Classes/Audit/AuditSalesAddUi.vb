Option Strict On
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms

Friend NotInheritable Class AuditSalesAddUi
    Friend Shared Function Refresh(owner As Form, work As Action) As Boolean
        Try
            work() : Return True
        Catch ex As Exception
            MessageBox.Show(owner, "تم حفظ الإضافة. تعذر تحديث العرض؛ لا تكرر الإضافة: " & ex.Message, "تحديث الفاتورة", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End Try
    End Function

    Friend Shared Function Run(owner As Form, command As SqlCommand, allowNegative As Boolean, ByRef savedLine As Integer) As Boolean
        Dim connection = MY_Settings.SqlConStr, completed = False
        Dim request As AuditOperationRequest = Nothing, result As AuditRequestResult = Nothing
        savedLine = 0
        Dim save As Action(Of String, AuditOperationRequest) = Sub(cs, r)
            Dim current = AuditSecuritySession.Current.ContextFor(MY_Settings.SqlConStr, USER_ID, r.Context.ScreenCode)
            If cs <> MY_Settings.SqlConStr OrElse current.SourceId <> r.Context.SourceId OrElse current.ActorUserId <> r.Context.ActorUserId OrElse current.SessionId <> r.Context.SessionId Then Throw New InvalidOperationException("تغير الاتصال أو المستخدم أو جلسة الدخول؛ لا يمكن إعادة الطلب بسياق مختلف.")
            result = AuditSalesAdd.Save(cs, r)
        End Sub
        Try
            Dim context = AuditSecuritySession.Current.ContextFor(connection, USER_ID, owner.GetType().Name)
            Dim header = AuditSalesAdd.ReadHeader(connection, context, Convert.ToInt32(command.Parameters("@SB_T_ID").Value), Convert.ToBoolean(command.Parameters("@On_Update").Value))
            request = AuditSalesAdd.Request(context, header, command, allowNegative)
            save(connection, request) : completed = True
        Catch ex As Exception
            Dim status = If(request Is Nothing, "NotFound", AuditSalesQuantityUi.Status(connection, request))
            If status = "Completed" Then
                completed = True
            ElseIf status = "NotFound" Then
                MessageBox.Show(owner, "لم تُحفظ إضافة الصنف. " & ex.Message, "إضافة الصنف", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
                savedLine = Integer.Parse(xml.Element("Values").Elements("Value").Single(Function(x) CStr(x.Attribute("name")) = "LineId").Value, CultureInfo.InvariantCulture)
            Catch ex As Exception
                ' Completion is known: never offer another add just because refreshing its ID failed.
                MessageBox.Show(owner, "تم حفظ الإضافة، لكن تعذر استرجاع رقم السطر لتحديده. حدّث الفاتورة ولا تكرر الإضافة. " & ex.Message, "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If
        Return completed
    End Function
End Class
