Option Strict On
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text.RegularExpressions

' Closed header workflows. Receipt-producing approvals are deliberately not represented here.
Partial Friend NotInheritable Class AuditContract
    Friend Shared Function IsWorkflow(operation As String) As Boolean
        Return {"Document.EditFieldsSaved", "Document.MetadataSaved", "Document.ProjectChanged", "Purchase.AccountCurrencyChanged", "Purchase.CurrencyChanged", "Quote.Confirmed", "Quote.DiscountChanged", "Quote.DiscountDistributed", "Quote.Reserved", "Quote.EditFieldsSaved"}.Contains(operation)
    End Function

    Friend Shared Function WorkflowFields(entity As String) As String()
        Select Case entity
            Case "WorkflowHeader"
                Return DocumentHeaderFields().Concat({"WorkflowAbout|About|String|nvarchar(max)", "WorkflowReference|ReferNum|String|nvarchar(100)",
                    "WorkflowProject|Proj_ID|Integer|int", "WorkflowExchange|Cr_Equal_Value|Decimal|numeric(18,3)",
                    "WorkflowCurrency|Cr_ID|Integer|int", "WorkflowPeriod|Pr_ID|Integer|int", "WorkflowTableOrdered|TB_isOrderd|Integer|int"}).ToArray()
            Case "WorkflowReference"
                Return {"LinkedReference|ReferNum|String|nvarchar(100)", "LinkedType|BsType_ID|Integer|int", "LinkedPeriod|Pr_ID|Integer|int"}
            Case "WorkflowCurrency"
                Return {"CurrencyName|Cr_Name|String|nvarchar(550)", "CurrencyRate|Cr_Equal|Decimal|numeric(18,2)"}
            Case "WorkflowQuoteLine"
                Return QuoteFields("QuoteLine")
            Case Else : Throw New ArgumentException("كيان دورة المستند غير معتمد.")
        End Select
    End Function

    Friend Shared Function WorkflowInputs(operation As String) As String()
        Dim result As New List(Of String) From {"DocumentType|Integer|R", "OnUpdate|Boolean|R", "ExpectedWorkflow|String|R"}
        Select Case operation
            Case "Document.EditFieldsSaved"
                result.AddRange({"RequestedTotal|Decimal|R", "RequestedDiscount|Decimal|R", "RequestedDate|DateTime|R", "RequestedAbout|String|O", "RequestedReference|String|O", "RequestedProject|Integer|O"})
            Case "Document.ProjectChanged" : result.Add("RequestedProject|Integer|O")
            Case "Document.MetadataSaved" : result.AddRange({"RequestedAbout|String|O", "RequestedDate|DateTime|R", "RequestedProject|Integer|O"})
            Case "Quote.EditFieldsSaved" : result.AddRange({"RequestedAbout|String|O", "RequestedDate|DateTime|R", "RequestedTotal|Decimal|R", "RequestedRecalcDiscount|Decimal|R"})
            Case "Quote.Reserved"
            Case "Purchase.AccountCurrencyChanged"
                result.AddRange({"RequestedAccount|Integer|R", "RequestedCurrency|Integer|R", "RequestedExchange|Decimal|R"})
            Case "Purchase.CurrencyChanged"
                result.AddRange({"RequestedCurrency|Integer|R", "RequestedExchange|Decimal|R"})
            Case "Quote.Confirmed"
                result.AddRange({"RequestedAccount|Integer|R", "RequestedAbout|String|O", "RequestedDate|DateTime|R", "RequestedTotal|Decimal|R", "RequestedDiscount|Decimal|R", "RequestedPure|Decimal|R"})
            Case "Quote.DiscountChanged", "Quote.DiscountDistributed" : result.Add("RequestedDiscount|Decimal|R")
            Case Else : Throw New ArgumentException("دورة المستند غير معتمدة.")
        End Select
        Return result.ToArray()
    End Function

    Friend Shared Function WorkflowFieldType(operation As String, field As String) As String
        For Each d In WorkflowInputs(operation)
            Dim p = d.Split("|"c)
            If p(0) = field Then Return p(1)
        Next
        For Each entity As String In {"WorkflowHeader", "WorkflowReference", "WorkflowCurrency", "WorkflowQuoteLine"}
            For Each d In WorkflowFields(entity)
                Dim p = d.Split("|"c)
                If p(0) = field Then Return p(2)
            Next
        Next
        Throw New ArgumentException("حقل دورة المستند غير معتمد.")
    End Function

    Friend Shared Function WorkflowValue(r As AuditOperationRequest, name As String) As AuditValue
        Return r.Inputs.Single(Function(x) x.Name = name).Value
    End Function

    Friend Shared Sub ValidateWorkflowRequest(r As AuditOperationRequest)
        Dim id As Integer, definitions = WorkflowInputs(r.OperationCode)
        If r.EntityType <> "WorkflowHeader" OrElse Not Integer.TryParse(r.EntityId, id) OrElse id <= 0 OrElse r.Inputs.Count <> definitions.Length Then Throw New ArgumentException("هوية/مدخلات دورة المستند غير مكتملة.")
        For Each d In definitions
            Dim p = d.Split("|"c), found = r.Inputs.SingleOrDefault(Function(x) x.Name = p(0))
            If found Is Nothing OrElse found.ValueType <> p(1) OrElse If(p(2) = "R", found.Value.State <> AuditValueState.Present, found.Value.State < AuditValueState.SqlNull) Then Throw New ArgumentException("مدخل دورة المستند غير صالح: " & p(0))
        Next
        Dim kind = WorkflowValue(r, "DocumentType").Text, screen = r.Context.ScreenCode
        Select Case r.OperationCode
            Case "Document.EditFieldsSaved"
                If Not {"1", "7"}.Contains(kind) OrElse screen <> If(kind = "1", "Sales", "Pch") OrElse WorkflowValue(r, "OnUpdate").Text <> "1" Then Throw New ArgumentException("شاشة/نوع تعديل المستند غير معتمد.")
                If If(kind = "1", WorkflowValue(r, "RequestedReference").State <> AuditValueState.SqlNull, WorkflowValue(r, "RequestedProject").State <> AuditValueState.SqlNull) Then Throw New ArgumentException("مدخل لا يخص نوع الفاتورة.")
            Case "Document.ProjectChanged"
                If Not {"1", "16"}.Contains(kind) OrElse screen <> If(kind = "1", "Sales", "ViewBill") Then Throw New ArgumentException("شاشة المشروع غير معتمدة.")
            Case "Document.MetadataSaved"
                If kind <> "1" OrElse screen <> "Sales" Then Throw New ArgumentException("شاشة بيانات رأس المبيعات غير معتمدة.")
            Case "Purchase.AccountCurrencyChanged", "Purchase.CurrencyChanged"
                If kind <> "7" OrElse screen <> "Pch" Then Throw New ArgumentException("شاشة عملة المشتريات غير معتمدة.")
            Case Else
                If kind <> "16" OrElse screen <> "ViewBill" Then Throw New ArgumentException("شاشة عرض السعر غير معتمدة.")
        End Select
        If r.OperationCode = "Quote.EditFieldsSaved" AndAlso WorkflowValue(r, "OnUpdate").Text <> "1" Then Throw New ArgumentException("طلب إنهاء تعديل العرض يتطلب فتح التعديل.")
        If Not Regex.IsMatch(WorkflowValue(r, "ExpectedWorkflow").Text, "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("بصمة دورة المستند غير صالحة.")
        For Each value As AuditInput In r.Inputs.Where(Function(x) x.Value.State = AuditValueState.Present)
            If value.ValueType = "Decimal" AndAlso Math.Abs(Decimal.Round(Decimal.Parse(value.Value.Text, CultureInfo.InvariantCulture), 3, MidpointRounding.AwayFromZero)) >= 1000000000000000D Then Throw New ArgumentException("القيمة تتجاوز سعة الحقل.")
            If {"RequestedAccount", "RequestedCurrency"}.Contains(value.Name) AndAlso (Not Integer.TryParse(value.Value.Text, id) OrElse id <= 0) Then Throw New ArgumentException("رقم الحساب/العملة غير صالح.")
            If value.Name = "RequestedProject" AndAlso (Not Integer.TryParse(value.Value.Text, id) OrElse id < 0) Then Throw New ArgumentException("رقم المشروع غير صالح.")
            If value.Name = "RequestedReference" AndAlso value.Value.Text.Length > 100 Then Throw New ArgumentException("المرجع أطول من الحقل.")
            If value.Name = "RequestedDate" Then
                Dim businessDate = DateTime.Parse(value.Value.Text, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
                If businessDate < New DateTime(1753, 1, 1) OrElse businessDate > New DateTime(9999, 12, 31, 23, 59, 59, 997) Then Throw New ArgumentException("تاريخ خارج نطاق SQL datetime.")
            End If
        Next
    End Sub

    Friend Shared Function WorkflowAllowed(r As AuditOperationRequest, entity As String, field As String) As Boolean
        If entity = "WorkflowReference" Then Return r.OperationCode = "Document.EditFieldsSaved" AndAlso WorkflowValue(r, "DocumentType").Text = "7" AndAlso field = "LinkedReference"
        If entity = "WorkflowCurrency" Then Return {"Purchase.AccountCurrencyChanged", "Purchase.CurrencyChanged"}.Contains(r.OperationCode) AndAlso field = "CurrencyRate"
        If entity = "WorkflowQuoteLine" Then Return ({"Quote.Confirmed", "Quote.Reserved"}.Contains(r.OperationCode) AndAlso field = "QuoteApproved") OrElse (r.OperationCode = "Quote.DiscountDistributed" AndAlso {"QuotePrice", "QuoteLineTotal"}.Contains(field))
        Select Case r.OperationCode
            Case "Document.EditFieldsSaved"
                Return {"DocumentTotal", "DocumentPure", "Discount", "DocumentDate", "WorkflowAbout", If(WorkflowValue(r, "DocumentType").Text = "1", "WorkflowProject", "WorkflowReference")}.Contains(field)
            Case "Document.ProjectChanged" : Return field = "WorkflowProject"
            Case "Document.MetadataSaved" : Return {"WorkflowAbout", "DocumentDate", "WorkflowProject"}.Contains(field)
            Case "Quote.EditFieldsSaved" : Return {"WorkflowAbout", "DocumentDate", "DocumentTotal", "DocumentPure"}.Contains(field)
            Case "Purchase.AccountCurrencyChanged" : Return {"AccountId", "AccountName", "WorkflowExchange"}.Contains(field)
            Case "Purchase.CurrencyChanged" : Return field = "WorkflowExchange"
            Case "Quote.DiscountChanged", "Quote.DiscountDistributed" : Return field = "Discount"
            Case "Quote.Confirmed" : Return {"AccountId", "AccountName", "WorkflowAbout", "DocumentDate", "DocumentTotal", "Discount", "DocumentPure", "Approved", "SalesOpen", "PaidState", "WorkflowTableOrdered"}.Contains(field)
        End Select
        Return False
    End Function

    Friend Shared Sub ValidateWorkflowEvent(r As AuditOperationRequest, e As AuditEventRequest)
        ValidateWorkflowRequest(r)
        If e.EntityType <> r.EntityType OrElse e.EntityId <> r.EntityId OrElse String.IsNullOrWhiteSpace(e.DocumentNumber) OrElse e.RelatedEntities.Count <> 0 Then Throw New ArgumentException("هوية حدث دورة المستند غير مطابقة.")
        If e.EventOrdinal = 1 Then
            If e.Changes.Count <> WorkflowFields("WorkflowHeader").Length OrElse e.Changes.Any(Function(x) x.EntityType <> "WorkflowHeader" OrElse x.EntityId <> r.EntityId) Then Throw New ArgumentException("حدث الرأس الأول غير مكتمل.")
        ElseIf e.Changes.Any(Function(x) x.EntityType = "WorkflowHeader") Then
            Throw New ArgumentException("لا يكرر الرأس في تفاصيل الدورة.")
        End If
        For Each group In e.Changes.GroupBy(Function(x) x.EntityType & ":" & x.EntityId)
            Dim entity = group.First().EntityType, fields = WorkflowFields(entity)
            If group.Count() <> fields.Length Then Throw New ArgumentException("صف دورة المستند غير مكتمل.")
            If entity = "WorkflowReference" AndAlso Not (r.OperationCode = "Document.EditFieldsSaved" AndAlso WorkflowValue(r, "DocumentType").Text = "7") Then Throw New ArgumentException("مراجع مرتبطة خارج مسار المشتريات.")
            If entity = "WorkflowCurrency" AndAlso Not {"Purchase.AccountCurrencyChanged", "Purchase.CurrencyChanged"}.Contains(r.OperationCode) Then Throw New ArgumentException("عملة خارج مسارها.")
            If entity = "WorkflowQuoteLine" AndAlso Not {"Quote.Confirmed", "Quote.Reserved", "Quote.DiscountDistributed"}.Contains(r.OperationCode) Then Throw New ArgumentException("سطر عرض خارج مساره.")
            For Each c In group
                If c.ValueOrigin <> 1 OrElse c.Before.State < AuditValueState.SqlNull OrElse c.After.State < AuditValueState.SqlNull OrElse Not fields.Any(Function(x) x.Split("|"c)(0) = c.FieldCode AndAlso x.Split("|"c)(3) = c.SourceSqlType) Then Throw New ArgumentException("مصدر/حالة صف دورة المستند غير صالح.")
                If Not WorkflowAllowed(r, entity, c.FieldCode) AndAlso (c.Before.State <> c.After.State OrElse c.Before.Text <> c.After.Text) Then Throw New ArgumentException("تغير خارج نطاق دورة المستند: " & c.FieldCode)
            Next
        Next
        If e.EventOrdinal = 1 Then
            If e.Changes.Single(Function(x) x.FieldCode = "DocumentType").After.Text <> WorkflowValue(r, "DocumentType").Text OrElse e.Changes.Single(Function(x) x.FieldCode = "DocumentNumber").After.Text <> e.DocumentNumber Then Throw New ArgumentException("رقم/نوع دورة المستند غير مطابق.")
            For Each mapping As String In {"RequestedDiscount|Discount", "RequestedDate|DocumentDate", "RequestedAbout|WorkflowAbout", "RequestedReference|WorkflowReference", "RequestedProject|WorkflowProject", "RequestedAccount|AccountId", "RequestedExchange|WorkflowExchange"}
                Dim p = mapping.Split("|"c), input = r.Inputs.SingleOrDefault(Function(x) x.Name = p(0))
                If input Is Nothing OrElse (p(0) = "RequestedReference" AndAlso WorkflowValue(r, "DocumentType").Text <> "7") OrElse (p(0) = "RequestedProject" AndAlso r.OperationCode = "Document.EditFieldsSaved" AndAlso WorkflowValue(r, "DocumentType").Text <> "1") Then Continue For
                Dim actual = e.Changes.Single(Function(x) x.FieldCode = p(1)).After
                If actual.State <> input.Value.State OrElse (actual.State = AuditValueState.Present AndAlso If(input.ValueType = "Decimal", Decimal.Parse(actual.Text, CultureInfo.InvariantCulture) <> Decimal.Round(Decimal.Parse(input.Value.Text, CultureInfo.InvariantCulture), 3, MidpointRounding.AwayFromZero), actual.Text <> input.Value.Text)) Then Throw New ArgumentException("قيمة دورة المستند لا تطابق طلبها: " & p(0))
            Next
        End If
        For Each c As AuditFieldChange In e.Changes
            If c.EntityType = "WorkflowQuoteLine" AndAlso c.FieldCode = "QuoteOwnerId" AndAlso (c.Before.Text <> r.EntityId OrElse c.After.Text <> r.EntityId) Then Throw New ArgumentException("سطر عرض تابع لرأس آخر.")
            If c.EntityType = "WorkflowReference" AndAlso ((c.FieldCode = "LinkedPeriod" AndAlso (c.Before.Text <> r.EntityId OrElse c.After.Text <> r.EntityId)) OrElse (c.FieldCode = "LinkedType" AndAlso (Not {"6", "9"}.Contains(c.Before.Text) OrElse c.After.Text <> c.Before.Text))) Then Throw New ArgumentException("مرجع تابع لرأس آخر.")
            If c.EntityType = "WorkflowCurrency" AndAlso c.EntityId <> WorkflowValue(r, "RequestedCurrency").Text Then Throw New ArgumentException("عملة لا تطابق الطلب.")
        Next
        If {"Quote.Confirmed", "Quote.Reserved"}.Contains(r.OperationCode) Then
            If e.Changes.Any(Function(x) x.FieldCode = "QuoteApproved" AndAlso x.After.Text <> "1") Then Throw New ArgumentException("صنف العرض لم يعتمد.")
        End If
    End Sub
End Class
