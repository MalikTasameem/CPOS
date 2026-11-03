Option Strict On
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text.RegularExpressions

' Ordinary receipt screen only. Custody-linked receipts are outside this contract.
Partial Friend NotInheritable Class AuditContract
    Friend Shared Function IsReceipt(operation As String) As Boolean
        Return {"Receipt.Created", "Receipt.EditOpened", "Receipt.Updated", "Receipt.Voided"}.Contains(operation)
    End Function
    Friend Shared Function ReceiptFields(entity As String) As String()
        Select Case entity
            Case "ReceiptHeader"
                Return {"Rct_T_ID|T_ID|Integer|int", _
        "Rct_Receipt_Num|Receipt_Num|Integer|int", _
        "Rct_Receipt_Tran_ID|Receipt_Tran_ID|Integer|int", _
        "Rct_AG_ID|AG_ID|Integer|int", _
        "Rct_Pr_ID|Pr_ID|Integer|int", _
        "Rct_ReferNum|ReferNum|String|nvarchar(100)", _
        "Rct_Date|Date|DateTime|datetime", _
        "Rct_Receipt_Title|Receipt_Title|String|nvarchar(550)", _
        "Rct_Total|Total|Decimal|numeric(18,3)", _
        "Rct_Discount|Discount|Decimal|numeric(18,3)", _
        "Rct_Pure|Pure|Decimal|numeric(18,3)", _
        "Rct_Debit|Debit|Decimal|numeric(18,3)", _
        "Rct_Credit|Credit|Decimal|numeric(18,3)", _
        "Rct_Balance|Balance|Decimal|numeric(18,3)", _
        "Rct_About|About|String|nvarchar(max)", _
        "Rct_BsType_ID|BsType_ID|Integer|int", _
        "Rct_isDepended|isDepended|Integer|int", _
        "Rct_isVoid|isVoid|Integer|int", _
        "Rct_isPied|isPied|Integer|int", _
        "Rct_User_ID|User_ID|Integer|int", _
        "Rct_Barcode|Barcode|String|nvarchar(50)", _
        "Rct_Cr_Equal_Value|Cr_Equal_Value|Decimal|numeric(18,3)", _
        "Rct_Debit_2|Debit_2|Decimal|numeric(18,3)", _
        "Rct_Credit_2|Credit_2|Decimal|numeric(18,3)", _
        "Rct_Balance_2|Balance_2|Decimal|numeric(18,3)", _
        "Rct_Custody_ID|Custody_ID|Integer|int", _
        "Rct_Pay_ID|Pay_ID|Integer|int", _
        "Rct_Prt_Counter|Prt_Counter|Integer|int", _
        "Rct_JournalId|JournalId|Integer|int", _
        "Rct_PostedAt|PostedAt|DateTime|datetime", _
        "Rct_PostedBy|PostedBy|Integer|int", _
        "Rct_Tr_ID|Tr_ID|Integer|int", _
        "Rct_OriginalJournalId|OriginalJournalId|Integer|int", _
        "Rct_LastReversalJournalId|LastReversalJournalId|Integer|int", _
        "Rct_NeedRepost|NeedRepost|Boolean|bit", _
        "Rct_EditVersion|EditVersion|Integer|int", _
        "Rct_EditOpenedAt|EditOpenedAt|DateTime|datetime", _
        "Rct_EditOpenedBy|EditOpenedBy|Integer|int", _
        "Rct_LastRepostAt|LastRepostAt|DateTime|datetime", _
        "Rct_LastRepostBy|LastRepostBy|Integer|int", _
        "Rct_VoidAt|VoidAt|DateTime|datetime", _
        "Rct_VoidBy|VoidBy|Integer|int", _
        "Rct_VoidReason|VoidReason|String|nvarchar(500)", _
        "Rct_VoidReversalJournalId|VoidReversalJournalId|Integer|int", _
        "Rct_NeedCancelReverse|NeedCancelReverse|Boolean|bit", _
        "Rct_ImageLength|AuditImageLength|Integer|bigint", _
        "Rct_ImageHash|AuditImageHash|String|varchar(64)"}
            Case "ReceiptTreasury"
                Return {"RctTr_T_ID|T_ID|Integer|int", _
        "RctTr_AGBalance_T_ID|AGBalance_T_ID|Integer|int", _
        "RctTr_Tran_ID|Tran_ID|Integer|int", _
        "RctTr_date|date|DateTime|datetime", _
        "RctTr_Tr_ID|Tr_ID|Integer|int", _
        "RctTr_notice_move|notice_move|String|nvarchar(max)", _
        "RctTr_Bank_Name|Bank_Name|String|nvarchar(100)", _
        "RctTr_CheckNum|CheckNum|String|nvarchar(100)", _
        "RctTr_Pure|Pure|Decimal|numeric(18,3)", _
        "RctTr_Debit|Debit|Decimal|numeric(18,3)", _
        "RctTr_Credit|Credit|Decimal|numeric(18,3)", _
        "RctTr_Balance|Balance|Decimal|numeric(18,3)", _
        "RctTr_Tr_Type_ID|Tr_Type_ID|Integer|int", _
        "RctTr_isTran_From|isTran_From|Boolean|bit", _
        "RctTr_isTran_To|isTran_To|Boolean|bit", _
        "RctTr_User_ID|User_ID|Integer|int", _
        "RctTr_isVoid|isVoid|Boolean|bit", _
        "RctTr_isFirst_Time|isFirst_Time|Integer|int", _
        "RctTr_JournalId|JournalId|Integer|int", _
        "RctTr_PostedAt|PostedAt|DateTime|datetime", _
        "RctTr_PostedBy|PostedBy|Integer|int", _
        "RctTr_OriginalJournalId|OriginalJournalId|Integer|int", _
        "RctTr_LastReversalJournalId|LastReversalJournalId|Integer|int", _
        "RctTr_NeedRepost|NeedRepost|Boolean|bit", _
        "RctTr_EditVersion|EditVersion|Integer|int", _
        "RctTr_EditOpenedAt|EditOpenedAt|DateTime|datetime", _
        "RctTr_EditOpenedBy|EditOpenedBy|Integer|int", _
        "RctTr_LastRepostAt|LastRepostAt|DateTime|datetime", _
        "RctTr_LastRepostBy|LastRepostBy|Integer|int", _
        "RctTr_VoidAt|VoidAt|DateTime|datetime", _
        "RctTr_VoidBy|VoidBy|Integer|int", _
        "RctTr_VoidReason|VoidReason|String|nvarchar(500)", _
        "RctTr_VoidReversalJournalId|VoidReversalJournalId|Integer|int", _
        "RctTr_NeedCancelReverse|NeedCancelReverse|Boolean|bit"}
            Case Else : Throw New ArgumentException("كيان الإيصال غير معتمد.")
        End Select
    End Function
    Friend Shared Function ReceiptInputs(operation As String) As String()
        If Not IsReceipt(operation) Then Throw New ArgumentException("عملية إيصال غير معتمدة.")
        Dim result As New List(Of String) From {"ExpectedReceipt||String|R"}
        If operation = "Receipt.Created" Then result.AddRange({"PlannedReceiptId||Integer|R", "PlannedReceiptNumber||Integer|R", "PlannedTreasuryId||Integer|R"})
        If {"Receipt.Created", "Receipt.Updated"}.Contains(operation) Then
            result.AddRange({"Date|Date|DateTime|R", "AG_ID|AG_ID|Integer|R", "Pr_ID|Pr_ID|Integer|O",
                "Receipt_Tran_ID|Receipt_Tran_ID|Integer|O", "Receipt_Title|Receipt_Title|String|O", "Pure|Pure|Decimal|R",
                "Discount|Discount|Decimal|O", "About|About|String|O", "BsType_ID|BsType_ID|Integer|R",
                "Tr_ID|Tr_ID|Integer|R", "Bank_Name|Bank_Name|String|O", "CheckNum|CheckNum|String|O", "Pay_ID|Pay_ID|Integer|R"})
        End If
        Return result.ToArray()
    End Function
    Friend Shared Function ReceiptFieldType(operation As String, field As String) As String
        For Each definition As String In ReceiptInputs(operation)
            Dim p = definition.Split("|"c)
            If p(0) = field Then Return p(2)
        Next
        For Each entityName As String In {"ReceiptHeader", "ReceiptTreasury"}
            For Each definition As String In ReceiptFields(entityName)
                Dim p = definition.Split("|"c)
                If p(0) = field Then Return p(2)
            Next
        Next
        Throw New ArgumentException("حقل الإيصال غير معتمد.")
    End Function
    Friend Shared Function ReceiptValue(r As AuditOperationRequest, name As String) As AuditValue
        Return r.Inputs.Single(Function(x) x.Name = name).Value
    End Function
    Friend Shared Sub ValidateReceiptRequest(r As AuditOperationRequest)
        Dim create = r.OperationCode = "Receipt.Created", id As Integer, definitions = ReceiptInputs(r.OperationCode)
        If r.EntityType <> "Receipt" OrElse r.Context.ScreenCode <> "Receipt" OrElse r.Context.ActorKind <> AuditActorKind.User Then Throw New ArgumentException("هوية/شاشة الإيصال غير معتمدة.")
        If If(create, r.EntityId <> "New", Not Integer.TryParse(r.EntityId, id) OrElse id <= 0) OrElse r.Inputs.Count <> definitions.Length Then Throw New ArgumentException("هوية طلب الإيصال أو مدخلاته غير مكتملة.")
        For Each definition As String In definitions
            Dim p = definition.Split("|"c), found = r.Inputs.SingleOrDefault(Function(x) x.Name = p(0))
            If found Is Nothing OrElse found.ValueType <> p(2) OrElse If(p(3) = "R", found.Value.State <> AuditValueState.Present, found.Value.State < AuditValueState.SqlNull) Then Throw New ArgumentException("مدخل إيصال مفقود أو نوعه غير صحيح: " & p(0))
        Next
        If Not Regex.IsMatch(ReceiptValue(r, "ExpectedReceipt").Text, "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("بصمة الإيصال غير صالحة.")
        For Each input As AuditInput In r.Inputs.Where(Function(x) x.Value.State = AuditValueState.Present)
            If input.ValueType = "Integer" Then
                If Not Integer.TryParse(input.Value.Text, id) OrElse If({"Pr_ID", "Receipt_Tran_ID"}.Contains(input.Name), id < 0, id <= 0) Then Throw New ArgumentException("رقم إيصال/مرجع غير صالح: " & input.Name)
            End If
            If input.ValueType = "Decimal" AndAlso Math.Abs(Decimal.Round(Decimal.Parse(input.Value.Text, CultureInfo.InvariantCulture), 3, MidpointRounding.AwayFromZero)) >= 1000000000000000D Then Throw New ArgumentException("قيمة الإيصال تتجاوز سعة الحقل.")
            If {"Bank_Name", "CheckNum"}.Contains(input.Name) AndAlso input.Value.Text.Length > 100 Then Throw New ArgumentException("النص المصرفي أطول من الحقل.")
            If input.Name = "Receipt_Title" AndAlso input.Value.Text.Length > 550 Then Throw New ArgumentException("عنوان الإيصال أطول من الحقل.")
            If input.Name = "Date" Then
                Dim dated = DateTime.Parse(input.Value.Text, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
                If dated < New DateTime(1753, 1, 1) OrElse dated > New DateTime(9999, 12, 31, 23, 59, 59, 997) Then Throw New ArgumentException("تاريخ الإيصال خارج نطاق SQL datetime.")
            End If
        Next
        If {"Receipt.Created", "Receipt.Updated"}.Contains(r.OperationCode) Then
            If Not {"3", "4"}.Contains(ReceiptValue(r, "BsType_ID").Text) OrElse Decimal.Round(Decimal.Parse(ReceiptValue(r, "Pure").Text, CultureInfo.InvariantCulture), 3, MidpointRounding.AwayFromZero) = 0D Then Throw New ArgumentException("نوع الإيصال أو قيمته غير صالح.")
        End If
    End Sub
    Friend Shared Function ReceiptAllowed(operation As String, entity As String, field As String) As Boolean
        If operation = "Receipt.Created" Then Return True
        If entity = "ReceiptHeader" Then
            Select Case operation
                Case "Receipt.EditOpened" : Return {"Rct_NeedRepost", "Rct_OriginalJournalId", "Rct_EditOpenedAt", "Rct_EditOpenedBy"}.Contains(field)
                Case "Receipt.Updated" : Return {"Rct_AG_ID", "Rct_Date", "Rct_Receipt_Title", "Rct_About", "Rct_Pure", "Rct_Debit", "Rct_Credit", "Rct_Pay_ID", "Rct_Tr_ID", "Rct_Discount"}.Contains(field)
                Case "Receipt.Voided" : Return {"Rct_isVoid", "Rct_VoidAt", "Rct_VoidBy", "Rct_VoidReason", "Rct_NeedCancelReverse", "Rct_NeedRepost"}.Contains(field)
            End Select
        ElseIf entity = "ReceiptTreasury" Then
            If operation = "Receipt.Updated" Then Return {"RctTr_Tr_ID", "RctTr_date", "RctTr_notice_move", "RctTr_Pure", "RctTr_Credit", "RctTr_Debit"}.Contains(field)
            If operation = "Receipt.Voided" Then Return field = "RctTr_isVoid"
        End If
        Return False
    End Function
    Friend Shared Sub ValidateReceiptEvent(r As AuditOperationRequest, e As AuditEventRequest)
        ValidateReceiptRequest(r)
        Dim create = r.OperationCode = "Receipt.Created", owner = If(create, ReceiptValue(r, "PlannedReceiptId").Text, r.EntityId), number As Integer
        If e.EntityType <> "Receipt" OrElse e.EntityId <> owner OrElse Not Integer.TryParse(e.DocumentNumber, number) OrElse number <= 0 OrElse e.RelatedEntities.Count <> 0 Then Throw New ArgumentException("هوية حدث الإيصال لا تطابق الطلب.")
        If create AndAlso e.DocumentNumber <> ReceiptValue(r, "PlannedReceiptNumber").Text Then Throw New ArgumentException("رقم الإيصال المنشأ لا يطابق الطلب.")
        If e.EventOrdinal = 1 Then
            If e.Changes.Count <> ReceiptFields("ReceiptHeader").Length OrElse e.Changes.Any(Function(x) x.EntityType <> "ReceiptHeader" OrElse x.EntityId <> owner) Then Throw New ArgumentException("حدث الإيصال الأول يتطلب رأسًا كاملًا.")
        ElseIf e.Changes.Any(Function(x) x.EntityType <> "ReceiptTreasury") Then
            Throw New ArgumentException("الأحداث التالية مخصصة لحركات خزينة الإيصال.")
        End If
        For Each group In e.Changes.GroupBy(Function(x) x.EntityType & ":" & x.EntityId)
            Dim first = group.First(), fields = ReceiptFields(first.EntityType), key As Integer
            If Not Integer.TryParse(first.EntityId, key) OrElse key <= 0 OrElse group.Count() <> fields.Length Then Throw New ArgumentException("صف إيصال غير مكتمل.")
            If first.EntityType = "ReceiptHeader" AndAlso first.EntityId <> owner Then Throw New ArgumentException("رأس إيصال آخر خارج الطلب.")
            If create AndAlso first.EntityType = "ReceiptTreasury" AndAlso first.EntityId <> ReceiptValue(r, "PlannedTreasuryId").Text Then Throw New ArgumentException("حركة خزينة الإنشاء لا تطابق الطلب.")
            For Each c As AuditFieldChange In group
                If c.ValueOrigin <> 1 OrElse c.After.State < AuditValueState.SqlNull OrElse If(create, c.Before.State <> AuditValueState.Absent, c.Before.State < AuditValueState.SqlNull) OrElse Not fields.Any(Function(x) x.Split("|"c)(0) = c.FieldCode AndAlso x.Split("|"c)(3) = c.SourceSqlType) Then Throw New ArgumentException("مصدر/حالة حقل إيصال غير صالح.")
                If Not ReceiptAllowed(r.OperationCode, first.EntityType, c.FieldCode) AndAlso (c.Before.State <> c.After.State OrElse c.Before.Text <> c.After.Text) Then Throw New ArgumentException("تغير خارج نطاق الإيصال: " & c.FieldCode)
            Next
            Dim idField = If(first.EntityType = "ReceiptHeader", "Rct_T_ID", "RctTr_T_ID")
            If group.Single(Function(x) x.FieldCode = idField).After.Text <> first.EntityId Then Throw New ArgumentException("معرف صف الإيصال لا يطابق قيمته.")
            If first.EntityType = "ReceiptTreasury" AndAlso group.Single(Function(x) x.FieldCode = "RctTr_AGBalance_T_ID").After.Text <> owner Then Throw New ArgumentException("حركة خزينة تابعة لإيصال آخر.")
        Next
        If e.EventOrdinal <> 1 Then Return
        Dim getAfter As Func(Of String, AuditValue) = Function(field) e.Changes.Single(Function(x) x.FieldCode = field).After
        Dim imageLength = getAfter("Rct_ImageLength"), imageHash = getAfter("Rct_ImageHash"), bytes As Long
        If imageLength.State <> imageHash.State OrElse (imageHash.State = AuditValueState.Present AndAlso
            (Not Long.TryParse(imageLength.Text, bytes) OrElse bytes < 0 OrElse Not Regex.IsMatch(imageHash.Text, "\A[A-F0-9]{64}\z"))) Then
            Throw New ArgumentException("طول صورة الإيصال أو بصمتها غير صالح.")
        End If
        Dim custody = getAfter("Rct_Custody_ID")
        If getAfter("Rct_Receipt_Num").Text <> e.DocumentNumber OrElse Not {"3", "4"}.Contains(getAfter("Rct_BsType_ID").Text) OrElse Not (custody.State = AuditValueState.SqlNull OrElse (custody.State = AuditValueState.Present AndAlso custody.Text = "0")) Then Throw New ArgumentException("رقم/نوع إيصال غير مطابق أو الإيصال تابع للعهد.")
        If getAfter("Rct_isVoid").Text <> If(r.OperationCode = "Receipt.Voided", "1", "0") Then Throw New ArgumentException("حالة إلغاء الإيصال لا تطابق العملية.")
        If r.OperationCode = "Receipt.EditOpened" AndAlso getAfter("Rct_EditOpenedBy").Text <> r.Context.ActorUserId.Value.ToString(CultureInfo.InvariantCulture) Then Throw New ArgumentException("مستخدم فتح التعديل غير مطابق.")
        If create AndAlso getAfter("Rct_User_ID").Text <> r.Context.ActorUserId.Value.ToString(CultureInfo.InvariantCulture) Then Throw New ArgumentException("مستخدم إنشاء الإيصال غير مطابق.")
        If {"Receipt.Created", "Receipt.Updated"}.Contains(r.OperationCode) Then
            For Each inputName As String In {"AG_ID", "Date", "Receipt_Title", "Pure", "Discount", "About", "BsType_ID", "Pay_ID"}
                Dim input = ReceiptValue(r, inputName), actual = getAfter("Rct_" & inputName)
                If actual.State <> input.State OrElse (actual.State = AuditValueState.Present AndAlso If(ReceiptFieldType(r.OperationCode, inputName) = "Decimal", Decimal.Parse(actual.Text, CultureInfo.InvariantCulture) <> Decimal.Round(Decimal.Parse(input.Text, CultureInfo.InvariantCulture), 3, MidpointRounding.AwayFromZero), actual.Text <> input.Text)) Then Throw New ArgumentException("قيمة الإيصال المحفوظة لا تطابق طلبها: " & inputName)
            Next
            ' Existing spend updates intentionally do not change RCT.Tr_ID; the treasury row still changes.
            If (create OrElse getAfter("Rct_BsType_ID").Text = "3") AndAlso getAfter("Rct_Tr_ID").Text <> ReceiptValue(r, "Tr_ID").Text Then Throw New ArgumentException("خزينة رأس الإيصال غير مطابقة.")
        End If
    End Sub
End Class
