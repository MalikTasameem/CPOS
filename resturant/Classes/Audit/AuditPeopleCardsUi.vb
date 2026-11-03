Option Strict On
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms

Friend NotInheritable Class AuditPeopleCardsUi
    Friend Shared Function Run(owner As Form, command As SqlCommand) As Boolean
        Dim savedId As Integer
        Return Run(owner, command, savedId)
    End Function
    Friend Shared Function Run(owner As Form, command As SqlCommand, ByRef savedId As Integer) As Boolean
        Dim connection = MY_Settings.SqlConStr, completed = False
        Dim request As AuditOperationRequest = Nothing, result As AuditRequestResult = Nothing
        savedId = 0
        Using envelope As New AuditPeopleEnvelope(command)
            Dim save As Action(Of String, AuditOperationRequest) = Sub(cs, r)
                Dim current = AuditSecuritySession.Current.ContextFor(MY_Settings.SqlConStr, USER_ID, r.Context.ScreenCode)
                If cs <> MY_Settings.SqlConStr OrElse current.SourceId <> r.Context.SourceId OrElse current.ActorUserId <> r.Context.ActorUserId OrElse current.SessionId <> r.Context.SessionId Then Throw New InvalidOperationException("تغير اتصال أو مستخدم أو جلسة طلب البطاقة.")
                result = AuditPeopleCards.Save(cs, r, envelope)
            End Sub
            Try
                If owner Is Nothing Then Throw New ArgumentException("لم تتحدد شاشة البطاقة.")
                Dim context = AuditSecuritySession.Current.ContextFor(connection, USER_ID, owner.GetType().Name)
                Dim before = AuditPeopleCards.Read(connection, context, command, envelope)
                request = AuditPeopleCards.Request(context, command, before, envelope)
                save(connection, request) : completed = True
            Catch ex As Exception
                Dim status = If(request Is Nothing, "NotFound", AuditSalesQuantityUi.Status(connection, request))
                If status = "Completed" Then
                    completed = True
                ElseIf status = "NotFound" Then
                    MessageBox.Show(owner, "لم تُحفظ عملية البطاقة. " & ex.Message, "بطاقات العملاء والمستخدمين", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
                    Dim output = command.Parameters.Cast(Of SqlParameter)().SingleOrDefault(Function(x) x.ParameterName.TrimStart("@"c).Equals("AG_ID", StringComparison.OrdinalIgnoreCase))
                    If output IsNot Nothing AndAlso output.Direction = Data.ParameterDirection.Output Then output.Value = savedId
                Catch ex As Exception
                    MessageBox.Show(owner, "تم حفظ البطاقة؛ تعذر جلب رقمها. أعد تحميل القائمة ولا تكرر الإنشاء. " & ex.Message, "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            End If
        End Using
        Return completed
    End Function
    Friend Shared Sub Refresh(owner As Form, work As Action)
        Try
            work()
        Catch ex As Exception
            MessageBox.Show(owner, "تم حفظ البطاقة؛ تعذر تحديث العرض فقط. " & ex.Message, "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
End Class

