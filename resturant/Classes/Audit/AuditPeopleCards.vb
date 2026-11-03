Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

' Sensitive arguments live only for this UI operation. Never serialize this envelope.
Friend NotInheritable Class AuditPeopleEnvelope
    Implements IDisposable
    Private ReadOnly _binding As Guid = Guid.NewGuid()
    Private ReadOnly _values As New Dictionary(Of String, Object)(StringComparer.Ordinal)
    Private _requestId As Guid
    Private _publicDigest As Byte()
    Private _credentialBaseline As Object()
    Friend ReadOnly Property Binding As String
        Get
            Return _binding.ToString("D")
        End Get
    End Property
    Friend Sub New(command As SqlCommand)
        For Each name In {"AG_img", "UserPass", "WEB_PASS", "NewPass", "OldPass", "OldPassEncrypted"}
            Dim value = AuditPeopleCards.Parameter(command, name, DBNull.Value)
            If TypeOf value Is Byte() Then value = DirectCast(DirectCast(value, Byte()).Clone(), Byte())
            _values.Add(name, value)
        Next
    End Sub
    Friend Sub Bind(request As AuditOperationRequest)
        If _requestId <> Guid.Empty Then Throw New InvalidOperationException("تم ربط ظرف البيانات مسبقًا.")
        _requestId = request.RequestId
        _publicDigest = AuditCanonical.Hash(AuditCanonical.RequestBytes(request))
    End Sub
    Friend Sub Verify(request As AuditOperationRequest)
        If request.RequestId <> _requestId OrElse _publicDigest Is Nothing OrElse Not _publicDigest.SequenceEqual(AuditCanonical.Hash(AuditCanonical.RequestBytes(request))) OrElse request.Inputs.Single(Function(x) x.Name = "EnvelopeBinding").Value.Text <> Binding Then Throw New InvalidOperationException("ظرف البيانات لا يطابق الطلب الثابت.")
    End Sub
    Friend Function Value(name As String) As Object
        If Not _values.ContainsKey(name) Then Throw New InvalidOperationException("ظرف البيانات انتهى.")
        Return _values(name)
    End Function
    Friend Sub CredentialBaseline(cn As SqlConnection, tx As SqlTransaction, id As Integer, compare As Boolean)
        Using cmd As New SqlCommand("SELECT UserPass,WEB_PASS,TOKEN FROM dbo.Users WITH(UPDLOCK,HOLDLOCK) WHERE User_ID=@id", cn, tx)
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id
            Using reader = cmd.ExecuteReader()
                If Not reader.Read() Then Throw New InvalidOperationException("المستخدم غير موجود.")
                Dim values(2) As Object
                reader.GetValues(values)
                If compare Then
                    If _credentialBaseline Is Nothing OrElse Not values.SequenceEqual(_credentialBaseline) Then Throw New InvalidOperationException("تغيرت بيانات اعتماد المستخدم؛ أعد تحميل البطاقة.")
                Else
                    _credentialBaseline = values
                End If
            End Using
        End Using
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        For Each sensitiveValue As Object In _values.Values
            If TypeOf sensitiveValue Is Byte() Then Array.Clear(DirectCast(sensitiveValue, Byte()), 0, DirectCast(sensitiveValue, Byte()).Length)
        Next
        _values.Clear()
        _publicDigest = Nothing
        _credentialBaseline = Nothing
    End Sub
End Class

Friend NotInheritable Class AuditPeopleCards
    Friend Shared Function Operation(command As SqlCommand) As String
        Select Case command.CommandText.Trim("["c, "]"c).ToLowerInvariant()
            Case "agents_insert" : Return "Agent.Created"
            Case "agents_update" : Return "Agent.Updated"
            Case "agents_delete" : Return "Agent.Deleted"
            Case "user_insert" : Return "User.Created"
            Case "user_update" : Return "User.Updated"
            Case "user_delete" : Return "User.Deleted"
            Case "user_updatepass" : Return "User.CredentialsChanged"
            Case Else : Throw New ArgumentException("إجراء بطاقة شخص غير معتمد.")
        End Select
    End Function
    Friend Shared Function Parameter(command As SqlCommand, name As String, Optional fallback As Object = Nothing) As Object
        Dim p = command.Parameters.Cast(Of SqlParameter)().SingleOrDefault(Function(x) x.ParameterName.TrimStart("@"c).Equals(name, StringComparison.OrdinalIgnoreCase))
        Return If(p Is Nothing, fallback, If(p.Value, DBNull.Value))
    End Function
    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, context As AuditEventContext, operation As String)
        Dim permissionSql As String
        If operation.StartsWith("User.", StringComparison.Ordinal) Then
            permissionSql = "u.isAdmin=1"
        Else
            Dim field = If(context.ScreenCode = "Pch", "Pch", If({"Sales", "ViewBill"}.Contains(context.ScreenCode), "Sales", "Agents"))
            permissionSql = If({"Agent", "AddFastAgent"}.Contains(context.ScreenCode),
                "(u.isAdmin=1 OR EXISTS(SELECT 1 FROM dbo.Users_Validations_V v WITH(HOLDLOCK) WHERE v.User_ID=u.User_ID AND (v.Agents=1 OR v.Sales=1 OR v.Pch=1)))",
                "EXISTS(SELECT 1 FROM dbo.Users_Validations_V v WITH(HOLDLOCK) WHERE v.User_ID=u.User_ID AND v.[" & field & "]=1)")
        End If
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) WHERE u.User_ID=@id AND u.is_Allow=1 AND " & permissionSql, cn, tx)
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("لا يملك المستخدم صلاحية العملية على البطاقة.")
        End Using
    End Sub
    Private Shared Function TableName(entity As String) As String
        Select Case entity
            Case "PeopleAgent" : Return "Agents"
            Case "PeopleUser" : Return "Users"
            Case "PeopleAccountLink" : Return "ACC_ENTITY_LINKS"
            Case "PeopleChartAccount" : Return "ACCOUNTS_TREE"
            Case Else : Throw New ArgumentException("كيان غير معتمد.")
        End Select
    End Function
    Private Shared Function KeyName(entity As String) As String
        Select Case entity
            Case "PeopleAgent" : Return "AG_ID"
            Case "PeopleUser" : Return "User_ID"
            Case "PeopleAccountLink" : Return "LINK_ID"
            Case "PeopleChartAccount" : Return "T_ID"
            Case Else : Throw New ArgumentException("كيان غير معتمد.")
        End Select
    End Function
    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, operation As String, id As Integer) As Dictionary(Of String, DataTable)
        Dim result As New Dictionary(Of String, DataTable)(StringComparer.Ordinal)
        For Each entity In AuditContract.PeopleEntities(operation).Where(Function(x) x <> "PeopleCredentialAction")
            Dim main = entity = "PeopleAgent" OrElse entity = "PeopleUser"
            Dim cols = AuditContract.PeopleFields(entity).Select(Function(x) x.Split("|"c)(1)).Where(Function(x) Not {"PhotoSha256", "PhotoBytes"}.Contains(x)).Select(Function(x) "[" & x & "]")
            Dim rows = AuditSalesQuantity.Rows(cn, tx, "SELECT [" & KeyName(entity) & "]," & String.Join(",", cols) & If(entity = "PeopleAgent", ",AG_img", "") & " FROM dbo.[" & TableName(entity) & "] WITH(UPDLOCK,HOLDLOCK)" & If(main, " WHERE [" & KeyName(entity) & "]=@P0", "") & " ORDER BY [" & KeyName(entity) & "]", id)
            If entity = "PeopleAgent" Then
                rows.Columns.Add("PhotoSha256", GetType(String)) : rows.Columns.Add("PhotoBytes", GetType(Long))
                For Each row As DataRow In rows.Rows
                    If Not IsDBNull(row("AG_img")) Then
                        Dim bytes = DirectCast(row("AG_img"), Byte())
                        row("PhotoSha256") = BitConverter.ToString(AuditCanonical.Hash(bytes)).Replace("-", "")
                        row("PhotoBytes") = bytes.LongLength
                    End If
                Next
                rows.Columns.Remove("AG_img")
            End If
            result.Add(entity, rows)
        Next
        Return result
    End Function
    Friend Shared Function Fingerprint(data As Dictionary(Of String, DataTable)) As String
        Return BitConverter.ToString(AuditCanonical.Hash(AuditCanonical.Encode(data.OrderBy(Function(x) x.Key, StringComparer.Ordinal).Select(Function(x) x.Key & ":" & AuditSalesQuantity.Fingerprint(x.Value))))).Replace("-", "")
    End Function
    Friend Shared Function Read(connection As String, context As AuditEventContext, command As SqlCommand, envelope As AuditPeopleEnvelope) As Dictionary(Of String, DataTable)
        Dim op = Operation(command), create = op.EndsWith(".Created", StringComparison.Ordinal), agent = op.StartsWith("Agent.", StringComparison.Ordinal)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Permission(cn, tx, context, op)
                Dim data = Capture(cn, tx, op, If(create, 0, Convert.ToInt32(Parameter(command, If(agent, "AG_ID", "user_id")))))
                If data(If(agent, "PeopleAgent", "PeopleUser")).Rows.Count <> If(create, 0, 1) Then Throw New InvalidOperationException("البطاقة غير موجودة أو مكررة.")
                If Not agent AndAlso Not create Then envelope.CredentialBaseline(cn, tx, Convert.ToInt32(Parameter(command, "user_id")), False)
                tx.Commit() : Return data
            End Using
        End Using
    End Function
    Friend Shared Function Request(context As AuditEventContext, command As SqlCommand, before As Dictionary(Of String, DataTable), envelope As AuditPeopleEnvelope) As AuditOperationRequest
        Dim op = Operation(command), create = op.EndsWith(".Created", StringComparison.Ordinal), agent = op.StartsWith("Agent.", StringComparison.Ordinal)
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = op, .EntityType = If(agent, "PeopleAgent", "PeopleUser"), .EntityId = If(create, "New", Convert.ToInt32(Parameter(command, If(agent, "AG_ID", "user_id"))).ToString(CultureInfo.InvariantCulture))}
        For Each name In AuditContract.PeopleInputs(op)
            Dim value As Object = DBNull.Value
            Select Case name
                Case "ExpectedPeopleSnapshot" : value = Fingerprint(before)
                Case "EnvelopeBinding" : value = envelope.Binding
                Case "AgentPhotoDigest", "AgentPhotoBytes"
                    Dim photo = envelope.Value("AG_img")
                    If Not IsDBNull(photo) Then
                        Dim bytes = DirectCast(photo, Byte())
                        value = If(name = "AgentPhotoDigest", CObj(BitConverter.ToString(AuditCanonical.Hash(bytes)).Replace("-", "")), CObj(bytes.LongLength))
                    End If
                Case Else
                    Dim p = AuditContract.PeopleFields(r.EntityType).Single(Function(x) x.StartsWith(name & "|", StringComparison.Ordinal)).Split("|"c)
                    Dim fallback As Object = DBNull.Value
                    Select Case name
                        Case "AgentBarcode" : fallback = ""
                        Case "AgentCurrency" : fallback = 1
                        Case "AgentMaxDebit" : fallback = 0D
                        Case "AgentPaused", "UserAdmin", "UserCustomerScreen" : fallback = False
                    End Select
                    value = Parameter(command, p(1), fallback)
            End Select
            Dim type = AuditContract.PeopleFieldType(op, name)
            If Not IsDBNull(value) Then
                Select Case type
                    Case "Integer" : value = Convert.ToInt64(value, CultureInfo.CurrentCulture)
                    Case "Decimal" : value = Convert.ToDecimal(value, CultureInfo.CurrentCulture)
                    Case "Boolean" : value = Convert.ToBoolean(value, CultureInfo.CurrentCulture)
                    Case "Date" : value = DateTime.SpecifyKind(Convert.ToDateTime(value).Date, DateTimeKind.Unspecified)
                    Case "String" : value = Convert.ToString(value, CultureInfo.CurrentCulture)
                End Select
            End If
            r.Inputs.Add(New AuditInput With {.Name = name, .ValueType = type, .Value = AuditValue.Capture(type, value)})
        Next
        AuditContract.Request(r) : envelope.Bind(r) : Return r
    End Function
    Private Shared Function Db(r As AuditOperationRequest, name As String) As Object
        Dim input = r.Inputs.Single(Function(x) x.Name = name)
        If input.Value.State = AuditValueState.SqlNull Then Return DBNull.Value
        Select Case input.ValueType
            Case "Integer" : Return Long.Parse(input.Value.Text, CultureInfo.InvariantCulture)
            Case "Decimal" : Return Decimal.Parse(input.Value.Text, CultureInfo.InvariantCulture)
            Case "Boolean" : Return input.Value.Text = "1"
            Case "Date" : Return DateTime.Parse(input.Value.Text, CultureInfo.InvariantCulture)
            Case Else : Return input.Value.Text
        End Select
    End Function
    Private Shared Sub CheckCredentials(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, envelope As AuditPeopleEnvelope, id As Integer)
        If r.OperationCode = "User.CredentialsChanged" Then
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users WITH(UPDLOCK,HOLDLOCK) WHERE User_ID=@id AND (UserPass=@old OR UserPass=@encrypted)", cn, tx)
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id
                cmd.Parameters.Add("@old", SqlDbType.NVarChar, 4000).Value = envelope.Value("OldPass")
                cmd.Parameters.Add("@encrypted", SqlDbType.NVarChar, 4000).Value = envelope.Value("OldPassEncrypted")
                If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New InvalidOperationException("كلمة المرور السابقة تغيرت أو غير صحيحة.")
            End Using
        End If
        If {"User.Created", "User.CredentialsChanged"}.Contains(r.OperationCode) Then
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users WITH(UPDLOCK,HOLDLOCK) WHERE User_ID<>@id AND UserPass=@value", cn, tx)
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id
                cmd.Parameters.Add("@value", SqlDbType.NVarChar, 4000).Value = envelope.Value(If(r.OperationCode = "User.Created", "UserPass", "NewPass"))
                If CInt(cmd.ExecuteScalar()) <> 0 Then Throw New InvalidOperationException("كلمة المرور مستخدمة؛ اختر كلمة أخرى.")
            End Using
        End If
    End Sub
    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest, envelope As AuditPeopleEnvelope) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse Not AuditContract.IsPeopleCard(request.OperationCode) Then Throw New ArgumentException("يلزم عقد بطاقات الأشخاص ومعاملة Serializable.")
        envelope.Verify(request)
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, agent = r.OperationCode.StartsWith("Agent.", StringComparison.Ordinal), create = r.OperationCode.EndsWith(".Created", StringComparison.Ordinal), delete = r.OperationCode.EndsWith(".Deleted", StringComparison.Ordinal)
            Permission(cn, tx, r.Context, r.OperationCode)
            Dim id = If(create, 0, Integer.Parse(r.EntityId, CultureInfo.InvariantCulture))
            Dim ids As DataTable = Nothing
            If create Then
                ' Lock the full allocator range and compare identities; never rely on MAX after saving.
                AuditSalesQuantity.Rows(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)")
                ids = AuditSalesQuantity.Rows(cn, tx, "SELECT [" & If(agent, "AG_ID", "User_ID") & "] FROM dbo.[" & If(agent, "Agents", "Users") & "] WITH(TABLOCKX,HOLDLOCK)")
            End If
            Dim before = Capture(cn, tx, r.OperationCode, id)
            If Fingerprint(before) <> CStr(Db(r, "ExpectedPeopleSnapshot")) OrElse before(r.EntityType).Rows.Count <> If(create, 0, 1) Then Throw New InvalidOperationException("تغيرت البطاقة أو روابطها؛ أعد تحميلها.")
            If Not agent Then CheckCredentials(cn, tx, r, envelope, id)
            If Not agent AndAlso Not create Then envelope.CredentialBaseline(cn, tx, id, True)
            If r.OperationCode = "User.Deleted" Then
                If id = r.Context.ActorUserId.Value Then Throw New InvalidOperationException("لا يمكن حذف المستخدم المنفذ للعملية.")
                If AuditSalesQuantity.Rows(cn, tx, "SELECT TOP(1) EventId FROM dbo.Network_Audit_Event WITH(HOLDLOCK) WHERE ActorUserId=@P0", id).Rows.Count <> 0 Then Throw New InvalidOperationException("لا يمكن حذف مستخدم لديه نشاط في سجل المراقبة الجديد.")
            End If
            Dim procedure = If(agent, If(create, "Agents_insert", If(delete, "Agents_Delete", "Agents_Update")), If(create, "User_Insert", If(delete, "User_Delete", If(r.OperationCode = "User.CredentialsChanged", "User_UpdatePass", "User_Update"))))
            Using cmd = AuditRequestCoordinator.Command("dbo." & procedure, cn, tx)
                If agent OrElse Not create Then
                    Dim param = cmd.Parameters.Add(If(agent, "@AG_ID", "@user_id"), SqlDbType.Int)
                    param.Value = id
                    If create Then param.Direction = ParameterDirection.Output
                End If
                If agent Then cmd.Parameters.Add("@USER_ID", SqlDbType.Int).Value = r.Context.ActorUserId.Value
                If Not delete AndAlso r.OperationCode <> "User.CredentialsChanged" Then
                    For Each name In AuditContract.PeopleInputs(r.OperationCode).Where(Function(x) Not {"ExpectedPeopleSnapshot", "EnvelopeBinding", "AgentPhotoDigest", "AgentPhotoBytes"}.Contains(x))
                        Dim d = AuditContract.PeopleFields(r.EntityType).Single(Function(x) x.StartsWith(name & "|", StringComparison.Ordinal)).Split("|"c)
                        cmd.Parameters.AddWithValue("@" & d(1), Db(r, name))
                    Next
                    If agent Then
                        cmd.Parameters.Add("@AG_img", SqlDbType.Image).Value = envelope.Value("AG_img")
                    Else
                        cmd.Parameters.Add("@WEB_PASS", SqlDbType.NVarChar, 4000).Value = envelope.Value("WEB_PASS")
                        If create Then cmd.Parameters.Add("@UserPass", SqlDbType.NVarChar, 4000).Value = envelope.Value("UserPass")
                    End If
                ElseIf r.OperationCode = "User.CredentialsChanged" Then
                    cmd.Parameters.Add("@NewPass", SqlDbType.NVarChar, 4000).Value = envelope.Value("NewPass")
                    cmd.Parameters.Add("@WEB_PASS", SqlDbType.NVarChar, 4000).Value = envelope.Value("WEB_PASS")
                End If
                cmd.ExecuteNonQuery()
                AuditRequestCoordinator.RequireTransaction(cn, tx)
                If create AndAlso agent Then id = Convert.ToInt32(cmd.Parameters("@AG_ID").Value)
            End Using
            If create Then
                Dim nowIds = AuditSalesQuantity.Rows(cn, tx, "SELECT [" & If(agent, "AG_ID", "User_ID") & "] FROM dbo.[" & If(agent, "Agents", "Users") & "] WITH(UPDLOCK,HOLDLOCK)")
                Dim oldIds = New HashSet(Of Integer)(ids.Rows.Cast(Of DataRow)().Select(Function(x) CInt(x(0))))
                Dim added = nowIds.Rows.Cast(Of DataRow)().Select(Function(x) CInt(x(0))).Where(Function(x) Not oldIds.Contains(x)).ToArray()
                If added.Length <> 1 OrElse added(0) <= 0 OrElse (agent AndAlso id <> added(0)) Then Throw New InvalidOperationException("تعذر تحديد البطاقة المحفوظة بشكل منفرد.")
                id = added(0)
            End If
            Dim after = Capture(cn, tx, r.OperationCode, id)
            If after(r.EntityType).Rows.Count <> If(delete, 0, 1) Then Throw New InvalidOperationException("لم يؤكد الإجراء حفظ البطاقة.")
            ValidateEffects(r, before, after, id)
            Dim events = BuildEvents(r, before, after, id, AuditRequestCoordinator.ServerUtcNow(cn, tx))
            For Each e In events
                AuditContract.EventData(r, e)
            Next
            For Each e In events
                AuditWriter.WriteInTransaction(cn, tx, lease, e)
            Next
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", id.ToString(CultureInfo.InvariantCulture)}})}
        Catch
            lease.Faulted = True : Throw
        End Try
    End Function
    Private Shared Sub ValidateEffects(r As AuditOperationRequest, before As Dictionary(Of String, DataTable), after As Dictionary(Of String, DataTable), id As Integer)
        If r.OperationCode.StartsWith("Agent.", StringComparison.Ordinal) Then
            ' Reject unrelated link/chart effects. Account creation/name sync/delete remains the original procedure's work.
            Dim codes As New HashSet(Of String)(StringComparer.Ordinal)
            For Each snapshot As Dictionary(Of String, DataTable) In {before, after}
                For Each row As DataRow In snapshot("PeopleAgent").Rows
                    If Not IsDBNull(row("Tree_Code")) Then codes.Add(CStr(row("Tree_Code")).Trim())
                Next
                For Each row As DataRow In snapshot("PeopleAccountLink").Rows
                    If CStr(row("SOURCE_TABLE")) = "Agents" AndAlso CLng(row("SOURCE_ID")) = id AndAlso CInt(row("ENTITY_TYPE")) >= 1 AndAlso CInt(row("ENTITY_TYPE")) <= 4 Then codes.Add(CStr(row("ACC_CODE")).Trim())
                Next
            Next
            For Each entity In {"PeopleAccountLink", "PeopleChartAccount"}
                For Each pair In ChangedRows(before(entity), after(entity))
                    For Each row In {pair.Item1, pair.Item2}.Where(Function(x) x IsNot Nothing)
                        If entity = "PeopleAccountLink" Then
                            If CStr(row("SOURCE_TABLE")) <> "Agents" OrElse CLng(row("SOURCE_ID")) <> id OrElse CInt(row("ENTITY_TYPE")) < 1 OrElse CInt(row("ENTITY_TYPE")) > 4 Then Throw New InvalidOperationException("تأثير ربط خارج البطاقة المحددة.")
                        ElseIf Not codes.Contains(CStr(row("ACC_CODE")).Trim()) Then
                            Throw New InvalidOperationException("تأثير دليل خارج حساب البطاقة.")
                        End If
                    Next
                Next
            Next
        ElseIf r.OperationCode = "User.CredentialsChanged" AndAlso Fingerprint(before) <> Fingerprint(after) Then
            Throw New InvalidOperationException("تغيير كلمة المرور أثر على حقول المستخدم العامة.")
        End If
    End Sub
    Private Shared Function ChangedRows(before As DataTable, after As DataTable) As List(Of Tuple(Of DataRow, DataRow))
        Dim a = before.Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x(0))), b = after.Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x(0)))
        Dim result As New List(Of Tuple(Of DataRow, DataRow))
        For Each id In a.Keys.Union(b.Keys).OrderBy(Function(x) x)
            Dim oldRow = If(a.ContainsKey(id), a(id), Nothing), newRow = If(b.ContainsKey(id), b(id), Nothing)
            If oldRow Is Nothing OrElse newRow Is Nothing OrElse AuditSalesLineEdit.Fingerprint(oldRow) <> AuditSalesLineEdit.Fingerprint(newRow) Then result.Add(Tuple.Create(oldRow, newRow))
        Next
        Return result
    End Function
    Friend Shared Function BuildEvents(r As AuditOperationRequest, before As Dictionary(Of String, DataTable), after As Dictionary(Of String, DataTable), id As Integer, occurred As DateTime) As List(Of AuditEventRequest)
        Dim events As New List(Of AuditEventRequest)
        Dim current As AuditEventRequest = Nothing
        For Each entity In AuditContract.PeopleEntities(r.OperationCode)
            If entity = "PeopleCredentialAction" Then
                Dim e As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = events.Count + 1, .EventCode = r.OperationCode, .Outcome = 1, .EntityType = r.EntityType, .EntityId = id.ToString(CultureInfo.InvariantCulture), .DocumentNumber = id.ToString(CultureInfo.InvariantCulture), .OccurredAtUtc = occurred}
                e.Changes.Add(New AuditFieldChange With {.ChangeNo = 1, .EntityType = entity, .EntityId = e.EntityId, .FieldCode = "CredentialAction", .ValueType = "String", .SourceSqlType = "nvarchar(50)", .ValueOrigin = 3, .Before = AuditValue.Missing(AuditValueState.Absent), .After = AuditValue.Capture("String", If(r.OperationCode = "User.Created", "Created", If(r.OperationCode = "User.Updated", "WebCredentialAndTokenRefreshed", "PasswordChanged")))})
                events.Add(e) : Continue For
            End If
            Dim rows = If(entity = r.EntityType, New List(Of Tuple(Of DataRow, DataRow)) From {Tuple.Create(If(before(entity).Rows.Count = 0, Nothing, before(entity).Rows(0)), If(after(entity).Rows.Count = 0, Nothing, after(entity).Rows(0)))}, ChangedRows(before(entity), after(entity)))
            current = Nothing
            For Each pair In rows
                If current Is Nothing OrElse current.Changes.Count + AuditContract.PeopleFields(entity).Length > 100 Then
                    current = New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = events.Count + 1, .EventCode = r.OperationCode, .Outcome = 1, .EntityType = r.EntityType, .EntityId = id.ToString(CultureInfo.InvariantCulture), .DocumentNumber = id.ToString(CultureInfo.InvariantCulture), .OccurredAtUtc = occurred}
                    events.Add(current)
                End If
                For Each d In AuditContract.PeopleFields(entity)
                    Dim p = d.Split("|"c)
                    current.Changes.Add(New AuditFieldChange With {.ChangeNo = current.Changes.Count + 1, .EntityType = entity, .EntityId = CInt(If(pair.Item2, pair.Item1)(0)).ToString(CultureInfo.InvariantCulture), .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = CByte(If(p(3) = "image", 3, 1)), .Before = If(pair.Item1 Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), pair.Item1(p(1)))), .After = If(pair.Item2 Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), pair.Item2(p(1))))})
                Next
            Next
        Next
        Return events
    End Function
    Friend Shared Function Save(connection As String, request As AuditOperationRequest, envelope As AuditPeopleEnvelope) As AuditRequestResult
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Dim result = ExecuteInTransaction(cn, tx, request, envelope)
                    tx.Commit() : Return result
                Catch
                    Try
                        If tx.Connection IsNot Nothing Then tx.Rollback()
                    Catch
                    End Try
                    Throw
                End Try
            End Using
        End Using
    End Function
End Class
