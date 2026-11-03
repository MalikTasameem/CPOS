Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

Friend NotInheritable Class AuditPricingSnapshot
    Friend Header As DataTable
    Friend Rows As DataTable
    Friend IsOffer As Boolean
End Class

Friend NotInheritable Class AuditPricingRules
    Friend Shared Function Operation(command As SqlCommand) As String
        Select Case command.CommandText.Trim("["c, "]"c).ToLowerInvariant()
            Case "offers_master_insert" : Return "Offer.Created"
            Case "offers_master_delete" : Return "Offer.ItemOffersDeleted"
            Case "change_price_insert" : Return "PriceRule.Created"
            Case "change_price_delete" : Return "PriceRule.Deleted"
            Case Else : Throw New ArgumentException("مسار قاعدة التسعير غير معتمد.")
        End Select
    End Function

    Private Shared Function Parameter(command As SqlCommand, name As String) As Object
        Dim p = command.Parameters.Cast(Of SqlParameter)().SingleOrDefault(Function(x) String.Equals(x.ParameterName.TrimStart("@"c), name, StringComparison.OrdinalIgnoreCase))
        If p Is Nothing OrElse p.Value Is Nothing Then Throw New ArgumentException("معامل مطلوب: " & name)
        Return p.Value
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, context As AuditEventContext)
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) WHERE u.User_ID=@User AND u.is_Allow=1 AND EXISTS(SELECT 1 FROM dbo.Users_Validations_V v WITH(HOLDLOCK) WHERE v.User_ID=u.User_ID AND v.IM=1)", cn, tx)
            cmd.Parameters.Add("@User", SqlDbType.Int).Value = context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("المستخدم غير مسموح له أو لا يملك صلاحية إدارة الأصناف.")
        End Using
    End Sub

    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, offer As Boolean, id As Integer) As AuditPricingSnapshot
        Dim parent = If(offer, "ItemOffers", "PriceRuleGroup"), child = If(offer, "OfferSnapshotRow", "PriceRule")
        Dim columns As Func(Of String, String) = Function(e) String.Join(",", AuditContract.PricingFields(e).Select(Function(x) "[" & x.Split("|"c)(1) & "]"))
        Dim s As New AuditPricingSnapshot With {.IsOffer = offer,
            .Header = AuditSalesQuantity.Rows(cn, tx, "SELECT " & If(offer, "IM_ID", "GM_ID") & "," & columns(parent) & " FROM dbo." & If(offer, "IM_Menu", "General_menu") & " WITH(UPDLOCK,HOLDLOCK) WHERE " & If(offer, "IM_ID", "GM_ID") & "=@P0", id),
            .Rows = AuditSalesQuantity.Rows(cn, tx, "SELECT " & If(offer, "", "T_ID,") & columns(child) & " FROM dbo." & If(offer, "OFFERS_MASTER", "Change_Price") & " WITH(UPDLOCK,HOLDLOCK) WHERE " & If(offer, "IM_ID", "GM_ID") & "=@P0", id)}
        If s.Header.Rows.Count <> 1 Then Throw New InvalidOperationException("الصنف أو التصنيف غير موجود أو معرفه مكرر.")
        Return s
    End Function

    Friend Shared Function Read(connection As String, context As AuditEventContext, command As SqlCommand) As AuditPricingSnapshot
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Permission(cn, tx, context)
                Dim op = Operation(command), offer = op.StartsWith("Offer.", StringComparison.Ordinal), id As Integer
                If op = "PriceRule.Deleted" Then
                    Dim rows = AuditSalesQuantity.Rows(cn, tx, "SELECT GM_ID FROM dbo.Change_Price WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@P0", Convert.ToInt32(Parameter(command, "T_ID")))
                    If rows.Rows.Count <> 1 Then Throw New InvalidOperationException("قاعدة التسعير لم تعد موجودة.")
                    id = CInt(rows.Rows(0)(0))
                Else
                    id = Convert.ToInt32(Parameter(command, If(offer, "IM_ID", "GM_ID")), CultureInfo.CurrentCulture)
                End If
                Dim s = Capture(cn, tx, offer, id)
                tx.Commit() : Return s
            End Using
        End Using
    End Function

    Private Shared Function Captured(row As DataRow, definition As String) As AuditValue
        Dim p = definition.Split("|"c), value = row(p(1))
        If Not IsDBNull(value) AndAlso p(3) = "time(0)" Then value = CType(value, TimeSpan).ToString("hh\:mm\:ss", CultureInfo.InvariantCulture)
        Return AuditValue.Capture(p(2), value)
    End Function

    Friend Shared Function RowKey(row As DataRow, offer As Boolean) As String
        Dim tokens As New List(Of String)
        If Not offer Then tokens.Add(CInt(row("T_ID")).ToString(CultureInfo.InvariantCulture))
        For Each definition In AuditContract.PricingFields(If(offer, "OfferSnapshotRow", "PriceRule"))
            Dim value = Captured(row, definition)
            tokens.Add(CInt(value.State).ToString(CultureInfo.InvariantCulture)) : tokens.Add(value.Text)
        Next
        Return BitConverter.ToString(AuditCanonical.Hash(AuditCanonical.Encode(tokens))).Replace("-", "")
    End Function

    Friend Shared Function Fingerprint(s As AuditPricingSnapshot) As String
        Dim keys = s.Rows.Rows.Cast(Of DataRow)().Select(Function(x) RowKey(x, s.IsOffer)).OrderBy(Function(x) x, StringComparer.Ordinal)
        Return BitConverter.ToString(AuditCanonical.Hash(AuditCanonical.Encode(New String() {AuditSalesQuantity.Fingerprint(s.Header)}.Concat(keys)))).Replace("-", "")
    End Function

    Friend Shared Function Request(context As AuditEventContext, command As SqlCommand, before As AuditPricingSnapshot) As AuditOperationRequest
        Dim op = Operation(command), offer = op.StartsWith("Offer.", StringComparison.Ordinal)
        If before.IsOffer <> offer Then Throw New ArgumentException("نوع اللقطة لا يطابق العملية.")
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = op,
            .EntityType = If(offer, "ItemOffers", "PriceRuleGroup"), .EntityId = CInt(before.Header.Rows(0)(0)).ToString(CultureInfo.InvariantCulture)}
        For Each name In AuditContract.PricingInputs(op)
            Dim value As Object
            Select Case name
                Case "ExpectedPricingSnapshot" : value = Fingerprint(before)
                Case "OfferTitle" : value = Parameter(command, "TITLE")
                Case "OfferStartDate", "OfferEndDate"
                    value = DateTime.SpecifyKind(Convert.ToDateTime(Parameter(command, If(name = "OfferStartDate", "DATE_F", "DATE_T")), CultureInfo.CurrentCulture).Date, DateTimeKind.Unspecified)
                Case "RequestedStartClock", "RequestedEndClock"
                    value = DateTime.SpecifyKind(Convert.ToDateTime(Parameter(command, If(name = "RequestedStartClock", "TIME_F", "TIME_T")), CultureInfo.CurrentCulture), DateTimeKind.Unspecified)
                Case "TargetPriceRuleId" : value = Convert.ToInt32(Parameter(command, "T_ID"), CultureInfo.CurrentCulture)
                Case "RequestedPercent" : value = Convert.ToDouble(Parameter(command, "Percent_Price"), CultureInfo.CurrentCulture)
                Case Else : Throw New ArgumentException("مدخل قاعدة تسعير غير معتمد.")
            End Select
            Dim type = AuditContract.FieldType(op, name)
            r.Inputs.Add(New AuditInput With {.Name = name, .ValueType = type, .Value = AuditValue.Capture(type, value)})
        Next
        AuditContract.Request(r) : Return r
    End Function

    Private Shared Function Db(r As AuditOperationRequest, name As String) As Object
        Dim input = r.Inputs.Single(Function(x) x.Name = name)
        Select Case input.ValueType
            Case "Integer" : Return Integer.Parse(input.Value.Text, CultureInfo.InvariantCulture)
            Case "Decimal" : Return Decimal.Parse(input.Value.Text, CultureInfo.InvariantCulture)
            Case "Float" : Return Double.Parse(input.Value.Text, CultureInfo.InvariantCulture)
            Case "Date", "DateTime" : Return DateTime.ParseExact(input.Value.Text, If(input.ValueType = "Date", "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss.fffffff"), CultureInfo.InvariantCulture, DateTimeStyles.None)
            Case Else : Return input.Value.Text
        End Select
    End Function

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse Not AuditContract.IsPricingRule(request.OperationCode) Then Throw New ArgumentException("يلزم عقد قاعدة التسعير ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, offer = r.OperationCode.StartsWith("Offer.", StringComparison.Ordinal), create = r.OperationCode.EndsWith("Created", StringComparison.Ordinal)
            Permission(cn, tx, r.Context)
            Dim id = Integer.Parse(r.EntityId, CultureInfo.InvariantCulture), target = If(r.OperationCode = "PriceRule.Deleted", CInt(Db(r, "TargetPriceRuleId")), id)
            Dim before = Capture(cn, tx, offer, id)
            If Fingerprint(before) <> CStr(Db(r, "ExpectedPricingSnapshot")) Then Throw New InvalidOperationException("تغيرت قاعدة التسعير أو بياناتها؛ حدّث القائمة قبل المتابعة.")
            If Not offer AndAlso create AndAlso before.Rows.Rows.Count > 0 Then Throw New InvalidOperationException("تم إدراج نسبة للمجموعة مسبقًا.")
            If Not create Then
                If before.Rows.Rows.Count = 0 OrElse (Not offer AndAlso Not before.Rows.Rows.Cast(Of DataRow)().Any(Function(x) CInt(x("T_ID")) = target)) Then Throw New InvalidOperationException("الصف المطلوب حذفه غير موجود.")
            End If
            Dim procedure = If(offer, If(create, "OFFERS_MASTER_INSERT", "OFFERS_MASTER_DELETE"), If(create, "Change_Price_Insert", "Change_Price_DELETE"))
            Using cmd = AuditRequestCoordinator.Command("dbo." & procedure, cn, tx)
                cmd.Parameters.Add(If(offer, "@IM_ID", If(create, "@GM_ID", "@T_ID")), SqlDbType.Int).Value = If(offer OrElse create, id, target)
                If create AndAlso offer Then
                    cmd.Parameters.Add("@TITLE", SqlDbType.NVarChar, 4000).Value = Db(r, "OfferTitle")
                    cmd.Parameters.Add("@DATE_F", SqlDbType.Date).Value = Db(r, "OfferStartDate")
                    cmd.Parameters.Add("@DATE_T", SqlDbType.Date).Value = Db(r, "OfferEndDate")
                    ' Preserve the original DateTime input -> SQL time(0) conversion, including rounding.
                    cmd.Parameters.Add("@TIME_F", SqlDbType.DateTime).Value = Db(r, "RequestedStartClock")
                    cmd.Parameters.Add("@TIME_T", SqlDbType.DateTime).Value = Db(r, "RequestedEndClock")
                    cmd.Parameters.Add("@USER_ID", SqlDbType.Int).Value = r.Context.ActorUserId.Value
                ElseIf create Then
                    ' Keep the existing Double -> SQL numeric(18,2) conversion on the server.
                    ' A client Decimal parameter with Scale=2 truncates before SQL can round.
                    cmd.Parameters.Add("@Percent_Price", SqlDbType.Float).Value = Db(r, "RequestedPercent")
                End If
                cmd.ExecuteNonQuery()
            End Using
            Dim after = Capture(cn, tx, offer, id)
            Dim occurred = AuditRequestCoordinator.ServerUtcNow(cn, tx)
            If Not offer AndAlso create Then
                If after.Rows.Rows.Count <> 1 Then Throw New InvalidOperationException("لم يؤكد الإجراء إنشاء نسبة واحدة.")
                target = CInt(after.Rows.Rows(0)("T_ID"))
            End If
            Dim part As Func(Of Integer, AuditEventRequest) = Function(n) New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = n, .EventCode = r.OperationCode, .Outcome = 1,
                .EntityType = r.EntityType, .EntityId = r.EntityId, .DocumentNumber = target.ToString(CultureInfo.InvariantCulture), .OccurredAtUtc = occurred}
            Dim events As New List(Of AuditEventRequest) From {part(1)}
            Append(events(0), r.EntityType, r.EntityId, before.Header.Rows(0), after.Header.Rows(0))
            Dim detail As AuditEventRequest = Nothing
            Dim child = If(offer, "OfferSnapshotRow", "PriceRule")
            Dim add As Action(Of String, DataRow, DataRow) = Sub(key, oldRow, newRow)
                If detail Is Nothing OrElse detail.Changes.Count + AuditContract.PricingFields(child).Length > 100 Then
                    detail = part(events.Count + 1) : events.Add(detail)
                End If
                Append(detail, child, key, oldRow, newRow)
            End Sub
            If offer Then
                ' OFFERS_MASTER is a heap without a row key. Match the full multiset, retaining duplicate occurrences.
                ' S1..Sn are references local to this request, NEVER persistent database identifiers.
                Dim pool As New Dictionary(Of String, Queue(Of DataRow))(StringComparer.Ordinal)
                For Each row As DataRow In after.Rows.Rows
                    Dim key = RowKey(row, True)
                    If Not pool.ContainsKey(key) Then pool.Add(key, New Queue(Of DataRow)())
                    pool(key).Enqueue(row)
                Next
                Dim ordinal = 0
                For Each row In before.Rows.Rows.Cast(Of DataRow)().OrderBy(Function(x) RowKey(x, True), StringComparer.Ordinal)
                    ordinal += 1
                    Dim key = RowKey(row, True), matched As DataRow = Nothing
                    If pool.ContainsKey(key) AndAlso pool(key).Count > 0 Then matched = pool(key).Dequeue()
                    If create AndAlso matched Is Nothing Then Throw New InvalidOperationException("غير الإجراء عرضًا قائمًا أثناء الإضافة.")
                    If Not create AndAlso matched IsNot Nothing Then Throw New InvalidOperationException("لم يحذف الإجراء جميع عروض الصنف.")
                    add("S" & ordinal.ToString(CultureInfo.InvariantCulture), row, matched)
                Next
                Dim added = pool.OrderBy(Function(x) x.Key, StringComparer.Ordinal).SelectMany(Function(x) x.Value).ToArray()
                If added.Length <> If(create, 1, 0) Then Throw New InvalidOperationException("نتيجة إنشاء/حذف عروض الصنف غير متوقعة.")
                For Each row In added
                    ordinal += 1 : add("S" & ordinal.ToString(CultureInfo.InvariantCulture), Nothing, row)
                Next
            Else
                Dim oldRows = before.Rows.Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x("T_ID")))
                Dim newRows = after.Rows.Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x("T_ID")))
                For Each key In oldRows.Keys.Union(newRows.Keys).OrderBy(Function(x) x)
                    Dim oldRow = If(oldRows.ContainsKey(key), oldRows(key), Nothing), newRow = If(newRows.ContainsKey(key), newRows(key), Nothing)
                    If key = target Then
                        If If(create, oldRow IsNot Nothing OrElse newRow Is Nothing, oldRow Is Nothing OrElse newRow IsNot Nothing) Then Throw New InvalidOperationException("لم يؤكد الإجراء نتيجة نسبة التصنيف.")
                    ElseIf oldRow Is Nothing OrElse newRow Is Nothing OrElse RowKey(oldRow, False) <> RowKey(newRow, False) Then
                        Throw New InvalidOperationException("تأثرت نسبة غير مستهدفة.")
                    End If
                    add(key.ToString(CultureInfo.InvariantCulture), oldRow, newRow)
                Next
            End If
            For Each item In events
                AuditContract.EventData(r, item)
            Next
            For Each item In events
                AuditWriter.WriteInTransaction(cn, tx, lease, item)
            Next
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1,
                New Dictionary(Of String, String) From {{"EntityId", target.ToString(CultureInfo.InvariantCulture)}})}
        Catch
            lease.Faulted = True : Throw
        End Try
    End Function

    Private Shared Sub Append(e As AuditEventRequest, entity As String, id As String, before As DataRow, after As DataRow)
        For Each definition In AuditContract.PricingFields(entity)
            Dim p = definition.Split("|"c)
            e.Changes.Add(New AuditFieldChange With {.ChangeNo = e.Changes.Count + 1, .EntityType = entity, .EntityId = id, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1,
                .Before = If(before Is Nothing, AuditValue.Missing(AuditValueState.Absent), Captured(before, definition)),
                .After = If(after Is Nothing, AuditValue.Missing(AuditValueState.Absent), Captured(after, definition))})
        Next
    End Sub

    Friend Shared Function Save(connection As String, request As AuditOperationRequest) As AuditRequestResult
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Dim result = ExecuteInTransaction(cn, tx, request)
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
