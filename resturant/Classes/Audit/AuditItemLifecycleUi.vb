Option Strict On
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Windows.Forms

Friend NotInheritable Class AuditItemLifecycleUi
    Private Shared Function QuickEntryScreen(owner As Form) As String
        If owner Is Nothing OrElse owner.GetType() IsNot GetType(IM_ADD_New) Then Return ""
        Dim caller = owner.Owner
        If caller IsNot Nothing AndAlso caller.GetType() Is GetType(Items_Search) Then caller = caller.Owner
        If caller Is Nothing OrElse caller.IsDisposed OrElse Not Application.OpenForms.Cast(Of Form)().Any(Function(f) f Is caller) Then Return ""
        If caller.GetType() Is GetType(Returns) Then
            ' Returns itself uses FormType=5/6; re-resolve on retries to reject a context switch.
            If FormType = 5 Then Return "Returns.Sales"
            If FormType = 6 Then Return "Returns.Purchase"
            Return ""
        End If
        Return caller.GetType().Name
    End Function

    Friend Shared Function Run(owner As Form, command As SqlCommand, ByRef savedId As Integer) As Boolean
        Dim connection = MY_Settings.SqlConStr, completed = False
        Dim prepared As AuditItemLifecycle.Prepared = Nothing, request As AuditOperationRequest = Nothing, result As AuditRequestResult = Nothing
        savedId = 0
        Dim save As Action(Of String, AuditOperationRequest) = Sub(cs, r)
            Dim current = AuditSecuritySession.Current.ContextFor(MY_Settings.SqlConStr, USER_ID, r.Context.ScreenCode)
            If cs <> MY_Settings.SqlConStr OrElse current.SourceId <> r.Context.SourceId OrElse current.ActorUserId <> r.Context.ActorUserId OrElse current.SessionId <> r.Context.SessionId Then Throw New InvalidOperationException("تغير اتصال أو مستخدم أو جلسة بطاقة الصنف.")
            If prepared Is Nothing OrElse prepared.QuickEntryScreen <> QuickEntryScreen(owner) Then Throw New InvalidOperationException("تغيرت الشاشة التي فتحت الإضافة السريعة؛ أعد فتح الإضافة من مصدرها.")
            result = AuditItemLifecycle.Save(cs, prepared, r)
        End Sub
        Try
            If owner Is Nothing Then Throw New ArgumentException("شاشة بطاقة الصنف مطلوبة.")
            Dim context = AuditSecuritySession.Current.ContextFor(connection, USER_ID, owner.GetType().Name)
            prepared = AuditItemLifecycle.Prepare(connection, context, command, QuickEntryScreen(owner))
            If prepared Is Nothing Then Return True
            request = prepared.Request
            save(connection, request) : completed = True
        Catch ex As Exception
            Dim status = If(request Is Nothing, "NotFound", AuditSalesQuantityUi.Status(connection, request))
            If status = "Completed" Then
                completed = True
            ElseIf status = "NotFound" Then
                MessageBox.Show(owner, "لم تُحفظ عملية الصنف. " & ex.Message, "إدارة الأصناف", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
                If command.Parameters.Contains("@IM_ID") Then command.Parameters("@IM_ID").Value = savedId
            Catch ex As Exception
                MessageBox.Show(owner, "تمت عملية الصنف؛ تعذر استرجاع رقمها. حدّث الشاشة ولا تكرر الحفظ. " & ex.Message, "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If
        Return completed
    End Function

    Friend Shared Function Run(owner As Form, command As SqlCommand) As Boolean
        Dim savedId As Integer
        Return Run(owner, command, savedId)
    End Function

    Friend Shared Sub Refresh(owner As Form, action As Action)
        AuditItemMasterUi.Refresh(owner, action)
    End Sub
End Class
