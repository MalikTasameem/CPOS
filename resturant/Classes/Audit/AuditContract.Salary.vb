Option Strict On
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text.RegularExpressions

Partial Friend NotInheritable Class AuditContract
    Friend Shared Function SalaryFields(entityName As String) As String()
        Select Case entityName
            Case "SalaryDocument" : Return {"SalaryAGID|AG_ID|Integer|int", _
                "SalaryDate|Date|DateTime|datetime", _
                "SalaryYear|Year|Integer|int", _
                "SalaryMonth|Month|Integer|int", _
                "SalaryReceiptTitle|Receipt_Title|String|nvarchar(550)", _
                "SalaryTotal|Total|Decimal|numeric(18,3)", _
                "SalaryDiscount|Discount|Decimal|numeric(18,3)", _
                "SalaryPure|Pure|Decimal|numeric(18,3)", _
                "SalaryDebit|Debit|Decimal|numeric(18,3)", _
                "SalaryCredit|Credit|Decimal|numeric(18,3)", _
                "SalaryBalance|Balance|Decimal|numeric(18,3)", _
                "SalaryUserID|User_ID|Integer|int", _
                "SalaryisVoid|isVoid|Integer|int", _
                "SalaryisDepended|isDepended|Integer|int", _
                "SalaryBsTypeID|BsType_ID|Integer|int", _
                "SalaryJournalId|JournalId|Integer|int", _
                "SalaryPostedAt|PostedAt|DateTime|datetime", _
                "SalaryPostedBy|PostedBy|Integer|int", _
                "SalaryOriginalJournalId|OriginalJournalId|Integer|int", _
                "SalaryLastReversalJournalId|LastReversalJournalId|Integer|int", _
                "SalaryNeedRepost|NeedRepost|Boolean|bit", _
                "SalaryEditVersion|EditVersion|Integer|int", _
                "SalaryEditOpenedAt|EditOpenedAt|DateTime|datetime", _
                "SalaryEditOpenedBy|EditOpenedBy|Integer|int", _
                "SalaryLastRepostAt|LastRepostAt|DateTime|datetime", _
                "SalaryLastRepostBy|LastRepostBy|Integer|int", _
                "SalaryVoidAt|VoidAt|DateTime|datetime", _
                "SalaryVoidBy|VoidBy|Integer|int", _
                "SalaryVoidReason|VoidReason|String|nvarchar(500)", _
                "SalaryVoidReversalJournalId|VoidReversalJournalId|Integer|int", _
                "SalaryNeedCancelReverse|NeedCancelReverse|Boolean|bit"}
            Case "SalaryEmployee" : Return {"SalaryEmployeeName|Ag_name|String|nvarchar(500)", _
                "SalaryBaseAmount|AG_Salary|Decimal|money", _
                "SalaryEmployeeType|Type_ID|Integer|int", _
                "SalaryEmployeePaused|is_Emp_Pause|Boolean|bit", _
                "SalaryEmployeeStarted|DATE_START|Date|date"}
            Case "SalaryAdjustment" : Return {"SalaryAdjCreatedAt|Date|DateTime|datetime", _
                "SalaryAdjBusinessDate|DateOF_Order|DateTime|datetime", _
                "SalaryAdjYear|YEAR|Integer|int", _
                "SalaryAdjMonth|MONTH|Integer|int", _
                "SalaryAdjEmployeeId|AG_ID|Integer|int", _
                "SalaryAdjType|ValType|Integer|int", _
                "SalaryAdjAmount|Value|Decimal|numeric(18,3)", _
                "SalaryAdjNotes|About|String|nvarchar(max)", _
                "SalaryAdjVoided|isVoid|Integer|int", _
                "SalaryAdjUserId|User_ID|Integer|bigint"}
            Case "SalaryBatch" : Return {"SalaryYear|SalaryYear|Integer|int", _
                "SalaryMonth|SalaryMonth|Integer|int", _
                "SalaryDate|SalaryDate|DateTime|datetime", _
                "SalaryNewCount|SalaryNewCount|Integer|int", _
                "SalaryCanceledCount|SalaryCanceledCount|Integer|int", _
                "SalaryTotal|SalaryTotal|Decimal|numeric(18,3)"}
            Case Else : Throw New ArgumentException("كيان مرتبات غير معتمد.")
        End Select
    End Function
    Friend Shared Function SalaryFieldType(field As String) As String
        If field = "ExpectedSalarySnapshot" Then Return "String"
        For Each entityName As String In {"SalaryBatch", "SalaryDocument", "SalaryEmployee", "SalaryAdjustment"}
            For Each d In SalaryFields(entityName)
                Dim p = d.Split("|"c)
                If p(0) = field Then Return p(2)
            Next
        Next
        Throw New ArgumentException("حقل مرتب غير معتمد.")
    End Function
    Friend Shared Sub ValidateSalaryRequest(r As AuditOperationRequest)
        If r.EntityType <> "SalaryBatch" OrElse r.Context.ScreenCode <> "Balances" OrElse r.Inputs.Count <> 4 Then Throw New ArgumentException("طلب ترصيد المرتبات غير صالح.")
        For Each name As String In {"SalaryYear", "SalaryMonth", "SalaryDate", "ExpectedSalarySnapshot"}
            Dim i = r.Inputs.SingleOrDefault(Function(x) x.Name = name)
            If i Is Nothing OrElse i.Value.State <> AuditValueState.Present Then Throw New ArgumentException("يلزم مدخل المرتبات: " & name)
        Next
        Dim year = Integer.Parse(r.Inputs.Single(Function(x) x.Name = "SalaryYear").Value.Text, CultureInfo.InvariantCulture), month = Integer.Parse(r.Inputs.Single(Function(x) x.Name = "SalaryMonth").Value.Text, CultureInfo.InvariantCulture)
        If year < 1753 OrElse year > 9998 OrElse month < 1 OrElse month > 12 OrElse r.EntityId <> year.ToString("0000", CultureInfo.InvariantCulture) & "-" & month.ToString("00", CultureInfo.InvariantCulture) Then Throw New ArgumentException("فترة المرتبات غير صالحة.")
        Dim business = DateTime.Parse(r.Inputs.Single(Function(x) x.Name = "SalaryDate").Value.Text, CultureInfo.InvariantCulture)
        If business.Year < 1753 OrElse business.Year > 9998 Then Throw New ArgumentException("تاريخ المرتب خارج SQL datetime.")
        If Not Regex.IsMatch(r.Inputs.Single(Function(x) x.Name = "ExpectedSalarySnapshot").Value.Text, "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("بصمة المرتبات غير صالحة.")
    End Sub
    Friend Shared Sub ValidateSalaryEvent(r As AuditOperationRequest, e As AuditEventRequest)
        If e.EntityType <> "SalaryBatch" OrElse e.EntityId <> r.EntityId OrElse e.DocumentNumber <> r.EntityId OrElse e.RelatedEntities.Count <> 0 Then Throw New ArgumentException("هوية حدث المرتبات غير صالحة.")
        If e.EventOrdinal = 1 AndAlso (e.Changes.Count <> SalaryFields("SalaryBatch").Length OrElse e.Changes.Any(Function(x) x.EntityType <> "SalaryBatch")) Then Throw New ArgumentException("يلزم ملخص فترة المرتبات أولًا.")
        For Each group In e.Changes.GroupBy(Function(x) x.EntityType & ":" & x.EntityId)
            Dim entityName = group.First().EntityType, fields = SalaryFields(entityName), id As Integer
            If group.Count() <> fields.Length OrElse If(entityName = "SalaryBatch", group.First().EntityId <> r.EntityId OrElse e.EventOrdinal <> 1, Not Integer.TryParse(group.First().EntityId, id) OrElse id <= 0) Then Throw New ArgumentException("صف المرتبات غير مكتمل.")
            If entityName = "SalaryDocument" Then
                Dim fresh = group.Any(Function(x) x.Before.State = AuditValueState.Absent)
                If fresh AndAlso group.Any(Function(x) x.Before.State <> AuditValueState.Absent) Then Throw New ArgumentException("حالات رأس المرتب الجديد غير متجانسة.")
                Dim v As Func(Of String, String) = Function(name) group.Single(Function(x) x.FieldCode = name).After.Text
                If v("SalaryBsTypeID") <> "5" OrElse v("SalaryisDepended") <> "1" OrElse v("SalaryisVoid") <> If(fresh, "0", "1") OrElse v("SalaryYear") <> r.Inputs.Single(Function(x) x.Name = "SalaryYear").Value.Text OrElse v("SalaryMonth") <> r.Inputs.Single(Function(x) x.Name = "SalaryMonth").Value.Text Then Throw New ArgumentException("نوع/حالة/فترة رأس المرتب غير مطابقة.")
                If fresh AndAlso v("SalaryUserID") <> r.Context.ActorUserId.Value.ToString(CultureInfo.InvariantCulture) Then Throw New ArgumentException("مدخل المرتب الجديد غير مطابق.")
                If Not fresh AndAlso (v("SalaryVoidBy") <> r.Context.ActorUserId.Value.ToString(CultureInfo.InvariantCulture) OrElse v("SalaryVoidReason") <> "إلغاء معاملة المرتب قبل إعادة الاحتساب" OrElse v("SalaryNeedRepost") <> "0") Then Throw New ArgumentException("إلغاء المرتب السابق غير مطابق.")
            End If
            For Each d In fields
                Dim p = d.Split("|"c), c = group.SingleOrDefault(Function(x) x.FieldCode = p(0))
                If c Is Nothing OrElse c.SourceSqlType <> p(3) OrElse c.ValueOrigin <> If(entityName = "SalaryBatch", 3, 1) Then Throw New ArgumentException("حقل المرتبات غير مطابق للعقد.")
                If entityName = "SalaryBatch" Then
                    If c.Before.State <> AuditValueState.Absent OrElse c.After.State <> AuditValueState.Present Then Throw New ArgumentException("ملخص المرتبات مشتق بعد الحفظ.")
                    Dim i = r.Inputs.SingleOrDefault(Function(x) x.Name = p(0))
                    If i IsNot Nothing AndAlso i.Value.Text <> c.After.Text Then Throw New ArgumentException("فترة المرتبات تختلف عن الطلب.")
                ElseIf entityName = "SalaryDocument" Then
                    If c.Before.State < AuditValueState.Absent OrElse c.After.State < AuditValueState.SqlNull Then Throw New ArgumentException("لا يحذف ترصيد المرتبات رؤوس المعاملات.")
                    If c.Before.State <> AuditValueState.Absent AndAlso Not {"SalaryisVoid", "SalaryVoidAt", "SalaryVoidBy", "SalaryVoidReason", "SalaryNeedCancelReverse", "SalaryNeedRepost"}.Contains(p(0)) AndAlso (c.Before.State <> c.After.State OrElse c.Before.Text <> c.After.Text) Then Throw New ArgumentException("تغيير غير مصرح في مرتب سابق.")
                Else
                    If c.Before.State < AuditValueState.SqlNull OrElse c.After.State <> c.Before.State OrElse c.After.Text <> c.Before.Text Then Throw New ArgumentException("مرجع احتساب المرتب يجب أن يبقى دون تغيير.")
                End If
            Next
        Next
    End Sub
End Class
