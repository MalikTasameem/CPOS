Option Strict On
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms

Friend NotInheritable Class AuditUnitsUi
    Friend Shared Function Run(owner As Form, operation As String, id As Integer, name As String, cargo As String, ByRef savedId As Integer) As Boolean
        Dim connection = MY_Settings.SqlConStr, completed = False
        Dim request As AuditOperationRequest = Nothing, result As AuditRequestResult = Nothing
        savedId = 0
        Dim save As Action(Of String, AuditOperationRequest) = Sub(cs, r)
            Dim current = AuditSecuritySession.Current.ContextFor(MY_Settings.SqlConStr, USER_ID, r.Context.ScreenCode)
            If cs <> MY_Settings.SqlConStr OrElse current.SourceId <> r.Context.SourceId OrElse current.ActorUserId <> r.Context.ActorUserId OrElse current.SessionId <> r.Context.SessionId Then Throw New InvalidOperationException("تغير اتصال أو مستخدم أو جلسة طلب الوحدة.")
            result = AuditUnits.Save(cs, r)
        End Sub
        Try
            If owner Is Nothing Then Throw New ArgumentException("لم تتحدد شاشة الوحدة.")
            Dim context = AuditSecuritySession.Current.ContextFor(connection, USER_ID, owner.GetType().Name)
            Dim before = If(operation = "Unit.Created", Nothing, AuditUnits.Read(connection, context, id))
            request = AuditUnits.Request(context, operation, before, name, If(operation = "Unit.Deleted", 1.0, AuditUnits.Cargo(cargo)))
            save(connection, request) : completed = True
        Catch ex As Exception
            Dim status = If(request Is Nothing, "NotFound", AuditSalesQuantityUi.Status(connection, request))
            If status = "Completed" Then
                completed = True
            ElseIf status = "NotFound" Then
                MessageBox.Show(owner, "لم تُحفظ عملية الوحدة. " & ex.Message, "الوحدات", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Using dialog As New AuditQuantityRecoveryDialog(connection, request, ex.Message, save)
                    completed = dialog.ShowDialog(owner) = DialogResult.OK
                End Using
            End If
        End Try
        If completed Then
            Try
                If result Is Nothing Then result = AuditRequestCoordinator.Status(connection, request)
                savedId = Integer.Parse(AuditCanonical.ParseXml(result.ResultXml).Element("Values").Elements("Value").Single(Function(x) CStr(x.Attribute("name")) = "EntityId").Value, CultureInfo.InvariantCulture)
            Catch ex As Exception
                MessageBox.Show(owner, "تم حفظ عملية الوحدة، لكن تعذر استرجاع رقمها. حدّث قائمة الوحدات ولا تكرر الإنشاء. " & ex.Message, "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If
        Return completed
    End Function

    Friend Shared Sub Refresh(owner As Form, work As Action)
        Try
            work()
        Catch ex As Exception
            MessageBox.Show(owner, "تم حفظ عملية الوحدة؛ تعذر تحديث العرض فقط. " & ex.Message, "الوحدات", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
End Class
