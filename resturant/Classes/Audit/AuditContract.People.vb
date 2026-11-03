Option Strict On
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text.RegularExpressions

' Closed people-card contract. Credentials and their hashes are intentionally absent.
Partial Friend NotInheritable Class AuditContract
    Friend Shared Function IsPeopleCard(operation As String) As Boolean
        Return {"Agent.Created", "Agent.Updated", "Agent.Deleted", "User.Created", "User.Updated", "User.Deleted", "User.CredentialsChanged"}.Contains(operation)
    End Function

    Friend Shared Function PeopleFields(entity As String) As String()
        Select Case entity
            Case "PeopleAgent"
                Return {"AgentName|Ag_name|String|nvarchar(500)", _
                    "AgentBarcode|Barcode|String|nvarchar(50)", _
                    "AgentPhone|Ag_phone|String|nvarchar(60)", _
                    "AgentAddress|Address|String|nvarchar(max)", _
                    "AgentDebit|T_Debit|Decimal|numeric(18,2)", _
                    "AgentCredit|T_Credit|Decimal|numeric(18,2)", _
                    "AgentBalance|T_Balance|Decimal|numeric(18,2)", _
                    "AgentDefault|isDefaultAG|Boolean|bit", _
                    "AgentMonthly|isMonthEmp|Boolean|bit", _
                    "AgentSalary|AG_Salary|Decimal|money", _
                    "AgentType|Type_ID|Integer|int", _
                    "AgentPhotoDigest|PhotoSha256|String|image", _
                    "AgentPhotoBytes|PhotoBytes|Integer|image", _
                    "AgentMaxDebit|Max_Debit|Decimal|numeric(18,2)", _
                    "AgentEmail|E_mail|String|nvarchar(500)", _
                    "AgentPaused|is_Emp_Pause|Boolean|bit", _
                    "AgentCurrency|Cr_ID|Integer|int", _
                    "AgentTemporaryBalance|T_Balance_TMP|Decimal|numeric(18,2)", _
                    "AgentAutoPaid|is_Auto_Pied|Boolean|bit", _
                    "AgentStarted|DATE_START|Date|date", _
                    "AgentTreeCode|Tree_Code|String|varchar(50)", _
                    "AgentLinkId|ACC_LINK_ID|Integer|int"}
            Case "PeopleUser"
                Return {"UserName|UserName|String|nvarchar(200)", _
                    "UserAdmin|isAdmin|Boolean|bit", _
                    "UserTable|TB_ID|Integer|int", _
                    "UserCustomerScreen|isCostmerScreen|Boolean|bit", _
                    "UserAgent|AG_ID|Integer|int", _
                    "UserAllowed|is_Allow|Integer|int", _
                    "UserValidation|Valid_ID|Integer|int", _
                    "UserTreasury|Tr_ID|Integer|int", _
                    "UserPhoneEnabled|isPone_Users|Integer|int"}
            Case "PeopleAccountLink"
                Return {"LinkEntityType|ENTITY_TYPE|Integer|tinyint", _
                    "LinkSourceTable|SOURCE_TABLE|String|sysname", _
                    "LinkSourceId|SOURCE_ID|Integer|bigint", _
                    "LinkAccountCode|ACC_CODE|String|varchar(50)", _
                    "LinkParentCode|PARENT_ACC_CODE|String|varchar(50)", _
                    "LinkName|ENTITY_NAME|String|nvarchar(1000)", _
                    "LinkAutomatic|IS_AUTO_CREATED|Boolean|bit", _
                    "LinkActive|IS_ACTIVE|Boolean|bit", _
                    "LinkLocked|IS_LOCKED|Boolean|bit", _
                    "LinkCreated|CREATED_AT|DateTime|datetime", _
                    "LinkCreatedBy|CREATED_BY|Integer|int", _
                    "LinkUpdated|UPDATED_AT|DateTime|datetime", _
                    "LinkUpdatedBy|UPDATED_BY|Integer|int"}
            Case "PeopleChartAccount"
                Return {"ChartCode|ACC_CODE|String|varchar(40)", _
                    "ChartLevel|ACC_LEVEL|Integer|int", _
                    "ChartParent|ACC_PARENT|String|varchar(40)", _
                    "ChartName|ACC_NAME|String|nvarchar(2500)", _
                    "ChartType|ACC_TYPE|String|nvarchar(50)", _
                    "ChartCategory|ACC_CAT|String|nvarchar(500)", _
                    "ChartClosing|ACC_CLOSING|Boolean|bit", _
                    "ChartHint|ACC_HINT|String|nvarchar(500)", _
                    "ChartDebit|DEBIT|Float|float(53)", _
                    "ChartCredit|CREDIT|Float|float(53)", _
                    "ChartNature|ACC_NATURAL|String|char(1)", _
                    "ChartBalanceView|is_Balance_View|Boolean|bit", _
                    "ChartCashflow|cash_flow_ID|Integer|int", _
                    "ChartSide|SIDE|String|varchar(500)", _
                    "ChartLocked|is_Lock_Trans|Boolean|bit", _
                    "ChartDigits|ACC_DIGIT|Integer|int", _
                    "ChartOpeningDebit|Start_DEBIT|Float|float(53)", _
                    "ChartOpeningCredit|Start_CREDIT|Float|float(53)", _
                    "ChartEndingDebit|End_DEBIT|Float|float(53)", _
                    "ChartEndingCredit|End_CREDIT|Float|float(53)", _
                    "ChartSourceType|AccountSourceType|Integer|tinyint"}
            Case "PeopleCredentialAction"
                Return {"CredentialAction|CredentialAction|String|nvarchar(50)"}
            Case Else : Throw New ArgumentException("كيان بطاقة شخص غير معتمد.")
        End Select
    End Function

    Friend Shared Function PeopleEntities(operation As String) As String()
        If operation.StartsWith("Agent.", StringComparison.Ordinal) Then Return {"PeopleAgent", "PeopleAccountLink", "PeopleChartAccount"}
        If operation = "User.Deleted" Then Return {"PeopleUser"}
        Return {"PeopleUser", "PeopleCredentialAction"}
    End Function

    Friend Shared Function PeopleInputs(operation As String) As String()
        If Not IsPeopleCard(operation) Then Throw New ArgumentException("عملية بطاقة شخص غير معتمدة.")
        Dim result As New List(Of String) From {"ExpectedPeopleSnapshot", "EnvelopeBinding"}
        If operation.EndsWith(".Deleted", StringComparison.Ordinal) OrElse operation = "User.CredentialsChanged" Then Return result.ToArray()
        If operation.StartsWith("Agent.", StringComparison.Ordinal) Then
            result.AddRange({"AgentName", "AgentBarcode", "AgentPhone", "AgentAddress", "AgentDefault", "AgentMonthly", "AgentSalary", "AgentType", "AgentMaxDebit", "AgentEmail", "AgentPaused", "AgentCurrency", "AgentStarted", "AgentPhotoDigest", "AgentPhotoBytes"})
        Else
            result.AddRange({"UserName", "UserCustomerScreen", "UserAgent", "UserAllowed", "UserValidation", "UserTreasury"})
            If operation = "User.Created" Then result.Add("UserAdmin")
        End If
        Return result.ToArray()
    End Function

    Friend Shared Function PeopleFieldType(operation As String, field As String) As String
        If {"ExpectedPeopleSnapshot", "EnvelopeBinding"}.Contains(field) Then Return "String"
        For Each d In PeopleEntities(operation).SelectMany(Function(x) PeopleFields(x))
            Dim p = d.Split("|"c)
            If p(0) = field Then Return p(2)
        Next
        Throw New ArgumentException("حقل بطاقة شخص غير معتمد؛ بيانات الاعتماد وبصمتها ممنوعة.")
    End Function

    Friend Shared Sub ValidatePeopleRequest(r As AuditOperationRequest)
        Dim create = r.OperationCode.EndsWith(".Created", StringComparison.Ordinal), agent = r.OperationCode.StartsWith("Agent.", StringComparison.Ordinal), id As Integer
        If r.EntityType <> If(agent, "PeopleAgent", "PeopleUser") OrElse If(create, r.EntityId <> "New", Not Integer.TryParse(r.EntityId, id) OrElse id <= 0) Then Throw New ArgumentException("هوية بطاقة الشخص غير صالحة.")
        Dim screens = If(agent, If(create, New String() {"Agent", "AddFastAgent", "Sales", "Pch", "ViewBill"}, New String() {"Agent"}), If(r.OperationCode = "User.CredentialsChanged", New String() {"UpdateUserPass"}, New String() {"users"}))
        If Not screens.Contains(r.Context.ScreenCode) Then Throw New ArgumentException("شاشة بطاقة الشخص غير معتمدة.")
        Dim required = PeopleInputs(r.OperationCode)
        If r.Inputs.Count <> required.Length OrElse required.Any(Function(n) r.Inputs.Where(Function(x) x.Name = n).Count() <> 1) Then Throw New ArgumentException("مدخلات بطاقة الشخص غير مكتملة.")
        For Each input As AuditInput In r.Inputs
            If input.Value.State < AuditValueState.SqlNull Then Throw New ArgumentException("يلزم التقاط كل مدخل.")
        Next
        Dim expected = r.Inputs.Single(Function(x) x.Name = "ExpectedPeopleSnapshot"), binding = r.Inputs.Single(Function(x) x.Name = "EnvelopeBinding"), token As Guid
        If expected.Value.State <> AuditValueState.Present OrElse Not Regex.IsMatch(expected.Value.Text, "\A[A-F0-9]{64}\z") OrElse binding.Value.State <> AuditValueState.Present OrElse Not Guid.TryParse(binding.Value.Text, token) OrElse token = Guid.Empty Then Throw New ArgumentException("بصمة/رابط ظرف البطاقة غير صالح.")
        For Each input As AuditInput In r.Inputs.Where(Function(x) x.Value.State = AuditValueState.Present)
            Select Case input.Name
                Case "AgentName" : Text(input.Value.Text, If(create, 500, 350), input.Name, True)
                Case "UserName" : Text(input.Value.Text, 200, input.Name, True)
                Case "AgentBarcode" : Text(input.Value.Text, 50, input.Name)
                Case "AgentPhone" : Text(input.Value.Text, 60, input.Name)
                Case "AgentAddress" : Text(input.Value.Text, If(create, 4096, 250), input.Name)
                Case "AgentEmail" : Text(input.Value.Text, 500, input.Name)
                Case "AgentType"
                    If Not {"1", "2", "3", "4"}.Contains(input.Value.Text) Then Throw New ArgumentException("نوع حساب غير معتمد.")
                Case "UserAllowed"
                    If Not {"0", "1"}.Contains(input.Value.Text) Then Throw New ArgumentException("حالة دخول غير صالحة.")
                Case "AgentCurrency", "UserValidation", "UserAgent"
                    If Not Integer.TryParse(input.Value.Text, id) OrElse id <= 0 Then Throw New ArgumentException("مرجع بطاقة غير صالح.")
                Case "UserTreasury"
                    ' Tr_ID = 0 is the persisted CPOS sentinel for the system default treasury.
                    If Not Integer.TryParse(input.Value.Text, id) OrElse id < 0 Then Throw New ArgumentException("مرجع خزينة المستخدم غير صالح.")
                Case "AgentPhotoDigest"
                    If Not Regex.IsMatch(input.Value.Text, "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("بصمة الصورة غير صالحة.")
            End Select
        Next
        For Each name In If(agent, New String() {"AgentName", "AgentType", "AgentCurrency", "AgentEmail"}, New String() {"UserName", "UserAllowed", "UserValidation", "UserTreasury"})
            Dim input = r.Inputs.SingleOrDefault(Function(x) x.Name = name)
            If input IsNot Nothing AndAlso input.Value.State <> AuditValueState.Present Then Throw New ArgumentException("حقل إلزامي فارغ: " & name)
        Next
    End Sub

    Friend Shared Sub ValidatePeopleEvent(r As AuditOperationRequest, e As AuditEventRequest)
        Dim agent = r.OperationCode.StartsWith("Agent.", StringComparison.Ordinal), create = r.OperationCode.EndsWith(".Created", StringComparison.Ordinal), delete = r.OperationCode.EndsWith(".Deleted", StringComparison.Ordinal), id As Integer
        If e.EntityType <> r.EntityType OrElse Not Integer.TryParse(e.EntityId, id) OrElse id <= 0 OrElse (Not create AndAlso e.EntityId <> r.EntityId) OrElse e.DocumentNumber <> e.EntityId OrElse e.RelatedEntities.Count <> 0 Then Throw New ArgumentException("هوية حدث بطاقة غير صالحة.")
        Dim main = If(agent, "PeopleAgent", "PeopleUser")
        If e.EventOrdinal = 1 AndAlso (e.Changes.Count <> PeopleFields(main).Length OrElse e.Changes.Any(Function(x) x.EntityType <> main OrElse x.EntityId <> e.EntityId)) Then Throw New ArgumentException("يلزم صف البطاقة كاملًا في الحدث الأول.")
        For Each group In e.Changes.GroupBy(Function(x) x.EntityType & ":" & x.EntityId)
            Dim first = group.First()
            If Not PeopleEntities(r.OperationCode).Contains(first.EntityType) OrElse Not Integer.TryParse(first.EntityId, id) OrElse id <= 0 Then Throw New ArgumentException("كيان فرعي غير معتمد.")
            Dim fields = PeopleFields(first.EntityType)
            If group.Count() <> fields.Length Then Throw New ArgumentException("لقطة البطاقة/الربط غير مكتملة.")
            For Each d In fields
                Dim p = d.Split("|"c), c = group.SingleOrDefault(Function(x) x.FieldCode = p(0))
                If c Is Nothing OrElse c.SourceSqlType <> p(3) OrElse c.ValueOrigin <> If(p(3) = "image" OrElse first.EntityType = "PeopleCredentialAction", 3, 1) Then Throw New ArgumentException("مصدر حقل البطاقة غير مطابق.")
                If first.EntityType = main Then
                    If c.EntityId <> e.EntityId OrElse If(create, c.Before.State <> AuditValueState.Absent OrElse c.After.State < AuditValueState.SqlNull, If(delete, c.After.State <> AuditValueState.Absent OrElse c.Before.State < AuditValueState.SqlNull, c.Before.State < AuditValueState.SqlNull OrElse c.After.State < AuditValueState.SqlNull)) Then Throw New ArgumentException("حالة صف البطاقة غير مطابقة للعملية.")
                    If r.OperationCode = "User.CredentialsChanged" AndAlso (c.Before.State <> c.After.State OrElse c.Before.Text <> c.After.Text) Then Throw New ArgumentException("تغيير كلمة المرور لا يغير الحقول العامة.")
                ElseIf first.EntityType = "PeopleCredentialAction" Then
                    If c.EntityId <> e.EntityId OrElse c.Before.State <> AuditValueState.Absent OrElse c.After.State <> AuditValueState.Present OrElse c.After.Text <> If(create, "Created", If(r.OperationCode = "User.Updated", "WebCredentialAndTokenRefreshed", "PasswordChanged")) Then Throw New ArgumentException("وصف إجراء بيانات الاعتماد غير صالح.")
                ElseIf c.Before.State < AuditValueState.Absent OrElse c.After.State < AuditValueState.Absent OrElse (c.Before.State = AuditValueState.Absent AndAlso c.After.State = AuditValueState.Absent) Then
                    Throw New ArgumentException("حالة ربط/دليل غير صالحة.")
                End If
            Next
        Next
    End Sub
End Class
