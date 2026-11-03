Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Globalization
Imports System.Linq

Friend Module AuditDocumentWorkflowsTests
    Private checks As Integer
    Private ReadOnly utc As New DateTime(2026, 10, 27, 12, 0, 0, DateTimeKind.Utc)
    Private ReadOnly dated As New DateTime(2026, 10, 27, 10, 0, 0, DateTimeKind.Unspecified)
    Private Sub Check(ok As Boolean, message As String)
        If Not ok Then Throw New Exception("FAIL: " & message)
        checks += 1
    End Sub
    Private Sub Reject(work As Action, message As String)
        Try
            work()
        Catch ex As ArgumentException
            Check(True, message) : Return
        Catch ex As InvalidOperationException
            Check(True, message) : Return
        End Try
        Throw New Exception("Expected rejection: " & message)
    End Sub
    Private Function Request(op As String, Optional kind As Integer = 16) As AuditOperationRequest
        Dim context As New AuditEventContext With {.SourceId = Guid.NewGuid(), .SessionId = Guid.NewGuid(), .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "اختبار", .ScreenCode = If(kind = 1, "Sales", If(kind = 7, "Pch", "ViewBill")), .MachineName = "Offline", .ApplicationVersion = "037"}
        Dim values As New Dictionary(Of String, Object)
        For Each d In AuditContract.WorkflowInputs(op)
            Dim p = d.Split("|"c)
            Select Case p(0)
                Case "DocumentType", "OnUpdate", "ExpectedWorkflow" : Continue For
                Case "RequestedTotal" : values.Add(p(0), 20D)
                Case "RequestedDiscount", "RequestedRecalcDiscount" : values.Add(p(0), 2D)
                Case "RequestedPure" : values.Add(p(0), 18D)
                Case "RequestedDate" : values.Add(p(0), dated)
                Case "RequestedAbout" : values.Add(p(0), "ملاحظة")
                Case "RequestedReference" : values.Add(p(0), If(kind = 7, CType("مرجع", Object), DBNull.Value))
                Case "RequestedProject" : values.Add(p(0), If(kind <> 7, CType(3, Object), DBNull.Value))
                Case "RequestedAccount" : values.Add(p(0), 4)
                Case "RequestedCurrency" : values.Add(p(0), 2)
                Case "RequestedExchange" : values.Add(p(0), 5.345D)
            End Select
        Next
        Return AuditDocumentWorkflows.Request(context, op, 50, kind, True, values)
    End Function
    Private Function RowTable(entity As String, id As Integer) As DataTable
        Dim dt As New DataTable, key = If(entity = "WorkflowCurrency", "Cr_ID", "T_ID")
        dt.Columns.Add(key, GetType(Integer))
        Dim row = dt.NewRow() : row(key) = id
        For Each d In AuditContract.WorkflowFields(entity)
            Dim p = d.Split("|"c), type As Type = Nothing, value As Object = Nothing
            Select Case p(2)
                Case "Integer" : type = GetType(Integer) : value = 0
                Case "Decimal" : type = GetType(Decimal) : value = 0D
                Case "Float" : type = GetType(Double) : value = 0R
                Case "String" : type = GetType(String) : value = ""
                Case "DateTime" : type = GetType(DateTime) : value = dated
                Case "Boolean" : type = GetType(Boolean) : value = False
            End Select
            dt.Columns.Add(p(1), type) : row(p(1)) = value
        Next
        dt.Rows.Add(row) : Return dt
    End Function
    Private Function Snapshot(r As AuditOperationRequest, after As Boolean) As AuditWorkflowSnapshot
        Dim kind = CInt(AuditDocumentWorkflows.V(r, "DocumentType")), s As New AuditWorkflowSnapshot With {.Header = RowTable("WorkflowHeader", 50), .References = RowTable("WorkflowReference", 80), .Currency = RowTable("WorkflowCurrency", 2), .QuoteLines = RowTable("WorkflowQuoteLine", 71), .CalculationLines = New DataTable(), .PurchaseExpenses = New DataTable(), .QuoteUnits = New DataTable(), .SalesIsolation = New DataTable(), .ComponentsIsolation = New DataTable()}
        Dim h = s.Header.Rows(0) : h("BsType_ID") = kind : h("AuditDocumentNumber") = 5 : h("AG_ID") = 3 : h("Total") = 20D : h("Pure") = 20D
        s.References.Rows(0)("BsType_ID") = 9 : s.References.Rows(0)("Pr_ID") = 50
        If Not (kind = 7 AndAlso r.OperationCode = "Document.EditFieldsSaved") Then s.References.Clear()
        If Not r.OperationCode.StartsWith("Purchase.") Then s.Currency.Clear()
        s.QuoteLines.Rows(0)("Bill_T_ID") = 50 : s.QuoteLines.Rows(0)("QTY") = 2R
        If kind <> 16 Then s.QuoteLines.Clear()
        If after Then
            For Each map In {"RequestedDiscount|Discount", "RequestedDate|Date", "RequestedAbout|About", "RequestedReference|ReferNum", "RequestedProject|Proj_ID", "RequestedAccount|AG_ID", "RequestedExchange|Cr_Equal_Value"}
                Dim p = map.Split("|"c), input = r.Inputs.SingleOrDefault(Function(x) x.Name = p(0))
                If input Is Nothing OrElse (p(0) = "RequestedReference" AndAlso kind <> 7) OrElse (p(0) = "RequestedProject" AndAlso kind = 7) Then Continue For
                If input.Value.State = AuditValueState.SqlNull Then
                    h(p(1)) = DBNull.Value
                Else
                    Select Case input.ValueType
                        Case "Decimal" : h(p(1)) = Decimal.Round(Decimal.Parse(input.Value.Text, CultureInfo.InvariantCulture), 3, MidpointRounding.AwayFromZero)
                        Case "Integer" : h(p(1)) = Integer.Parse(input.Value.Text, CultureInfo.InvariantCulture)
                        Case "DateTime" : h(p(1)) = dated
                        Case Else : h(p(1)) = input.Value.Text
                    End Select
                End If
            Next
            For Each row As DataRow In s.References.Rows
                row("ReferNum") = "مرجع"
            Next
            If s.Currency.Rows.Count = 1 Then s.Currency.Rows(0)("Cr_Equal") = 5.35D
            If r.OperationCode = "Quote.Confirmed" Then
                h("isDepended") = 1 : h("isPied") = 1 : h("TB_isOrderd") = 1 : h("Pure") = 18D : s.QuoteLines.Rows(0)("isDepended") = 1
            End If
            If r.OperationCode = "Quote.Reserved" Then s.QuoteLines.Rows(0)("isDepended") = 1
            If r.OperationCode = "Quote.DiscountDistributed" Then
                s.QuoteLines.Rows(0)("Price") = 9D : s.QuoteLines.Rows(0)("T_Price") = 18D
            End If
        End If
        Return s
    End Function
    Public Function Main() As Integer
        Try
            Dim cases As New List(Of AuditOperationRequest) From {Request("Document.EditFieldsSaved", 1), Request("Document.EditFieldsSaved", 7), Request("Document.ProjectChanged", 1), Request("Document.ProjectChanged", 16), Request("Purchase.AccountCurrencyChanged", 7), Request("Purchase.CurrencyChanged", 7), Request("Quote.Confirmed"), Request("Quote.DiscountChanged"), Request("Quote.DiscountDistributed"), Request("Document.MetadataSaved", 1), Request("Quote.Reserved"), Request("Quote.EditFieldsSaved")}
            For Each r In cases
                Dim before = Snapshot(r, False), after = Snapshot(r, True), events = AuditDocumentWorkflows.Events(r, before, after, utc)
                Check(events.Count > 0 AndAlso events(0).Changes.Count = 34, r.OperationCode & " complete header")
                Check(AuditContract.Domain(r.OperationCode) = "Documents.Workflows", "domain")
                Check(events(0).DocumentNumber = "5", "document number")
                For Each e In events
                    AuditContract.EventData(r, e) : Check(True, "closed event")
                Next
                Dim oldScreen = r.Context.ScreenCode : r.Context.ScreenCode = "Receipt"
                Reject(Sub() AuditContract.Request(r), "wrong screen") : r.Context.ScreenCode = oldScreen
                Dim oldHash = r.Inputs.Single(Function(x) x.Name = "ExpectedWorkflow").Value
                r.Inputs.Single(Function(x) x.Name = "ExpectedWorkflow").Value = AuditValue.Capture("String", "bad")
                Reject(Sub() AuditContract.Request(r), "bad fingerprint") : r.Inputs.Single(Function(x) x.Name = "ExpectedWorkflow").Value = oldHash
                For Each d In AuditContract.WorkflowFields("WorkflowHeader")
                    Dim p = d.Split("|"c)
                    If AuditContract.WorkflowAllowed(r, "WorkflowHeader", p(0)) Then Continue For
                    Dim mutated = AuditDocumentWorkflows.Events(r, before, after, utc)(0), c = mutated.Changes.Single(Function(x) x.FieldCode = p(0))
                    c.After = AuditValue.Missing(AuditValueState.SqlNull)
                    Reject(Sub() AuditContract.EventData(r, mutated), "forbidden " & p(0))
                Next
                after.Header.Rows(0)("T_ID") = 51
                Reject(Sub() AuditDocumentWorkflows.Events(r, before, after, utc), "other document")
            Next
            Dim distributed = Request("Quote.DiscountDistributed"), qb = Snapshot(distributed, False), qa = Snapshot(distributed, True)
            For Each dt In {qb.SalesIsolation, qa.SalesIsolation, qb.ComponentsIsolation, qa.ComponentsIsolation}
                dt.Columns.Add("T_ID", GetType(Integer)) : dt.Columns.Add("Price", GetType(Decimal)) : dt.Rows.Add(71, 99D)
            Next
            AuditDocumentWorkflows.Events(distributed, qb, qa, utc) : Check(True, "same numeric sale/component id remains isolated")
            qa.SalesIsolation.Rows(0)("Price") = 1D
            Reject(Sub() AuditDocumentWorkflows.Events(distributed, qb, qa, utc), "sales isolation")
            qa.SalesIsolation.Rows(0)("Price") = 99D : qa.ComponentsIsolation.Rows(0)("Price") = 1D
            Reject(Sub() AuditDocumentWorkflows.Events(distributed, qb, qa, utc), "component isolation")
            Dim requestWrong = Request("Quote.DiscountChanged")
            requestWrong.Inputs.Add(New AuditInput With {.Name = "FreeSql", .ValueType = "String", .Value = AuditValue.Capture("String", "UPDATE")})
            Reject(Sub() AuditContract.Request(requestWrong), "closed input names")
            Console.WriteLine("PASS DocumentWorkflows offline checks=" & checks.ToString(CultureInfo.InvariantCulture))
            Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString()) : Return 1
        End Try
    End Function
End Module
