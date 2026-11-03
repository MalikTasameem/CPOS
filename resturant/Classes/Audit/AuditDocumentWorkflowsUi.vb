Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms

Friend NotInheritable Class AuditDocumentWorkflowsUi
    Friend Shared Function Run(owner As Form, operation As String, bill As Integer, kind As Integer, editing As Boolean, values As IDictionary(Of String, Object)) As Boolean
        Dim connection = MY_Settings.SqlConStr, request As AuditOperationRequest = Nothing
        Dim save As Action(Of String, AuditOperationRequest) = Sub(cs, r)
            Dim current = AuditSecuritySession.Current.ContextFor(MY_Settings.SqlConStr, USER_ID, r.Context.ScreenCode)
            If cs <> MY_Settings.SqlConStr OrElse current.SourceId <> r.Context.SourceId OrElse current.ActorUserId <> r.Context.ActorUserId OrElse current.SessionId <> r.Context.SessionId Then Throw New InvalidOperationException("تغير الاتصال أو المستخدم أو جلسة دورة المستند.")
            AuditDocumentWorkflows.Save(cs, r)
        End Sub
        Try
            If owner Is Nothing Then Throw New ArgumentException("شاشة المستند مطلوبة.")
            Dim context = AuditSecuritySession.Current.ContextFor(connection, USER_ID, owner.GetType().Name)
            request = AuditDocumentWorkflows.Request(context, operation, bill, kind, editing, values)
            AuditDocumentWorkflows.Prepare(connection, request)
            save(connection, request) : Return True
        Catch ex As Exception
            Dim status = If(request Is Nothing, "NotFound", AuditSalesQuantityUi.Status(connection, request))
            If status = "Completed" Then Return True
            If status = "NotFound" Then
                MessageBox.Show(owner, "لم تحفظ دورة المستند. " & ex.Message, "مراقبة المستند", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
            Using dialog As New AuditQuantityRecoveryDialog(connection, request, ex.Message, save)
                Return dialog.ShowDialog(owner) = DialogResult.OK
            End Using
        End Try
    End Function

    Friend Shared Function EditFields(owner As Form, bill As Integer, kind As Integer, total As Decimal, discount As Decimal, dated As DateTime, about As String, reference As String, project As Object) As Boolean
        Return Run(owner, "Document.EditFieldsSaved", bill, kind, True, New Dictionary(Of String, Object) From {{"RequestedTotal", total}, {"RequestedDiscount", discount}, {"RequestedDate", dated}, {"RequestedAbout", If(String.IsNullOrWhiteSpace(about), CType(DBNull.Value, Object), about)}, {"RequestedReference", If(kind = 7, CType(reference, Object), DBNull.Value)}, {"RequestedProject", If(kind = 1, If(project, DBNull.Value), DBNull.Value)}})
    End Function

    Friend Shared Function Project(owner As Form, bill As Integer, kind As Integer, editing As Boolean, projectId As Object) As Boolean
        Return Run(owner, "Document.ProjectChanged", bill, kind, editing, New Dictionary(Of String, Object) From {{"RequestedProject", If(projectId, DBNull.Value)}})
    End Function

    Friend Shared Function Metadata(owner As Form, bill As Integer, editing As Boolean, dated As DateTime, about As String, projectId As Object) As Boolean
        Return Run(owner, "Document.MetadataSaved", bill, 1, editing, New Dictionary(Of String, Object) From {{"RequestedDate", dated}, {"RequestedAbout", If(String.IsNullOrWhiteSpace(about), CType(DBNull.Value, Object), about)}, {"RequestedProject", If(projectId, DBNull.Value)}})
    End Function

    Friend Shared Function QuoteFinishEdit(owner As Form, bill As Integer, total As Decimal, discount As Decimal, dated As DateTime, about As String) As Boolean
        Return Run(owner, "Quote.EditFieldsSaved", bill, 16, True, New Dictionary(Of String, Object) From {{"RequestedDate", dated}, {"RequestedAbout", If(String.IsNullOrWhiteSpace(about), CType(DBNull.Value, Object), about)}, {"RequestedTotal", total}, {"RequestedRecalcDiscount", discount}})
    End Function

    Friend Shared Function QuoteReserve(owner As Form, bill As Integer, editing As Boolean) As Boolean
        Return Run(owner, "Quote.Reserved", bill, 16, editing, New Dictionary(Of String, Object))
    End Function

    Friend Shared Function Currency(owner As Form, bill As Integer, editing As Boolean, currencyId As Object, exchange As Decimal, Optional accountId As Integer? = Nothing) As Boolean
        Dim values As New Dictionary(Of String, Object) From {{"RequestedCurrency", currencyId}, {"RequestedExchange", exchange}}
        If accountId.HasValue Then values.Add("RequestedAccount", accountId.Value)
        Return Run(owner, If(accountId.HasValue, "Purchase.AccountCurrencyChanged", "Purchase.CurrencyChanged"), bill, 7, editing, values)
    End Function

    Friend Shared Function QuoteConfirm(owner As Form, bill As Integer, editing As Boolean, account As Integer, about As String, dated As DateTime, total As Decimal, discount As Decimal, pure As Decimal) As Boolean
        Return Run(owner, "Quote.Confirmed", bill, 16, editing, New Dictionary(Of String, Object) From {{"RequestedAccount", account}, {"RequestedAbout", If(String.IsNullOrWhiteSpace(about), CType(DBNull.Value, Object), about)}, {"RequestedDate", dated}, {"RequestedTotal", total}, {"RequestedDiscount", discount}, {"RequestedPure", pure}})
    End Function

    Friend Shared Function QuoteDiscount(owner As Form, bill As Integer, editing As Boolean, amount As Decimal, distributed As Boolean) As Boolean
        Return Run(owner, If(distributed, "Quote.DiscountDistributed", "Quote.DiscountChanged"), bill, 16, editing, New Dictionary(Of String, Object) From {{"RequestedDiscount", amount}})
    End Function
End Class
