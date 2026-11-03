Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

Friend NotInheritable Class AuditGroupSnapshot
    Friend Header As DataTable
    Friend Printers As DataTable
End Class

Friend NotInheritable Class AuditGroups
    Friend Shared Function Operation(command As SqlCommand) As String
        Select Case command.CommandText.ToLowerInvariant()
            Case "gm_insert" : Return "Group.Created"
            Case "gm_update" : Return "Group.Updated"
            Case "gm_delete" : Return "Group.Deleted"
            Case "groupprinter_insert" : Return "Group.PrinterLinked"
            Case "groupprinter_delete" : Return "Group.PrinterUnlinked"
            Case Else : Throw New ArgumentException("مسار التصنيف غير معتمد.")
        End Select
    End Function

    Friend Shared Function Parameter(command As SqlCommand, name As String, Optional defaultValue As Object = Nothing) As Object
        Dim p = command.Parameters.Cast(Of SqlParameter)().SingleOrDefault(Function(x) String.Equals(x.ParameterName.TrimStart("@"c), name, StringComparison.OrdinalIgnoreCase))
        If p IsNot Nothing Then
            If p.Value Is Nothing Then Throw New ArgumentException("قيمة غير محددة: " & name)
            Return p.Value
        End If
        If defaultValue Is Nothing Then Throw New ArgumentException("معامل مطلوب: " & name)
        Return defaultValue
    End Function

    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, id As Integer) As AuditGroupSnapshot
        Dim columns = String.Join(",", AuditContract.GroupFields("ProductGroup").Select(Function(x) "[" & x.Split("|"c)(1) & "]"))
        Dim result As New AuditGroupSnapshot With {
            .Header = AuditSalesQuantity.Rows(cn, tx, "SELECT GM_ID," & columns & " FROM dbo.General_menu WITH(UPDLOCK,HOLDLOCK) WHERE GM_ID=@P0", id),
            .Printers = AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID,GM_ID,PTR_ID FROM dbo.GM_Printer WITH(UPDLOCK,HOLDLOCK) WHERE GM_ID=@P0 ORDER BY T_ID", id)}
        If result.Header.Rows.Count > 1 Then Throw New InvalidOperationException("معرف تصنيف مكرر.")
        Return result
    End Function

    Friend Shared Function Fingerprint(snapshot As AuditGroupSnapshot) As String
        If snapshot Is Nothing OrElse snapshot.Header.Rows.Count <> 1 Then Throw New InvalidOperationException("التصنيف غير موجود.")
        Return BitConverter.ToString(AuditCanonical.Hash(AuditCanonical.Encode({AuditSalesQuantity.Fingerprint(snapshot.Header), AuditSalesQuantity.Fingerprint(snapshot.Printers)}))).Replace("-", "")
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, context As AuditEventContext)
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) WHERE u.User_ID=@User AND u.is_Allow=1" &
            If(context.ScreenCode = "IM_ADD_New", "", " AND EXISTS(SELECT 1 FROM dbo.Users_Validations_V v WITH(HOLDLOCK) WHERE v.User_ID=u.User_ID AND v.IM=1)"), cn, tx)
            cmd.Parameters.Add("@User", SqlDbType.Int).Value = context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("المستخدم غير مسموح له أو لا يملك صلاحية إدارة الأصناف.")
        End Using
    End Sub

    Friend Shared Function Read(connection As String, context As AuditEventContext, id As Integer) As AuditGroupSnapshot
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Permission(cn, tx, context)
                Dim result = Capture(cn, tx, id)
                Fingerprint(result)
                tx.Commit() : Return result
            End Using
        End Using
    End Function

    Friend Shared Function Request(context As AuditEventContext, command As SqlCommand, before As AuditGroupSnapshot) As AuditOperationRequest
        Dim op = Operation(command), create = op = "Group.Created"
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = op,
            .EntityType = "ProductGroup", .EntityId = If(create, "New", Convert.ToInt32(Parameter(command, "GM_ID")).ToString(CultureInfo.InvariantCulture))}
        For Each name In AuditContract.GroupInputs(op)
            Dim value As Object = Nothing
            Select Case name
                Case "ExpectedGroupSnapshot" : value = Fingerprint(before)
                Case "RequestedRank" : value = Parameter(command, "Rank_Num", DBNull.Value)
                Case "PrinterLinkId" : value = Parameter(command, "T_ID")
                Case "LinkedPrinterId" : value = Parameter(command, "PTR_ID")
                Case Else
                    Dim column = AuditContract.GroupFields("ProductGroup").Single(Function(x) x.Split("|"c)(0) = name).Split("|"c)(1)
                    value = Parameter(command, column, If(column.StartsWith("BK_", StringComparison.Ordinal) OrElse column.StartsWith("FK_", StringComparison.Ordinal), DBNull.Value, Nothing))
            End Select
            Dim type = AuditContract.FieldType(op, name)
            r.Inputs.Add(New AuditInput With {.Name = name, .ValueType = type, .Value = AuditValue.Capture(type, value)})
        Next
        AuditContract.Request(r) : Return r
    End Function

    Private Shared Function Db(r As AuditOperationRequest, name As String) As Object
        Dim input = r.Inputs.Single(Function(x) x.Name = name)
        If input.Value.State = AuditValueState.SqlNull Then Return DBNull.Value
        If input.ValueType = "Integer" Then Return Long.Parse(input.Value.Text, CultureInfo.InvariantCulture)
        Return input.Value.Text
    End Function

    Private Shared Sub LockAllocation(cn As SqlConnection, tx As SqlTransaction)
        Dim ranges = AuditSalesQuantity.Rows(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)")
        If ranges.Rows.Count <> 1 OrElse IsDBNull(ranges.Rows(0)(0)) OrElse IsDBNull(ranges.Rows(0)(1)) Then Throw New InvalidOperationException("نطاق ترقيم التصنيفات غير محدد بشكل منفرد.")
        Dim first = CInt(ranges.Rows(0)(0)), last = CInt(ranges.Rows(0)(1))
        Dim allocated = AuditSalesQuantity.Rows(cn, tx, "SELECT ISNULL(MAX(GM_ID),@P0) AS LastId FROM dbo.General_menu WITH(UPDLOCK,HOLDLOCK) WHERE GM_ID BETWEEN @P0 AND @P1", first, last)
        If first >= last OrElse CInt(allocated.Rows(0)(0)) >= last Then Throw New InvalidOperationException("نطاق التصنيفات ممتلئ أو غير صالح.")
    End Sub

    Private Shared Sub Append(item As AuditEventRequest, entity As String, id As String, before As DataRow, after As DataRow)
        For Each definition In AuditContract.GroupFields(entity)
            Dim p = definition.Split("|"c)
            item.Changes.Add(New AuditFieldChange With {.ChangeNo = item.Changes.Count + 1, .EntityType = entity, .EntityId = id, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1,
                .Before = If(before Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), before(p(1)))),
                .After = If(after Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), after(p(1))))})
        Next
    End Sub

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse Not AuditContract.IsGroup(request.OperationCode) Then Throw New ArgumentException("يلزم عقد التصنيف ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, create = r.OperationCode = "Group.Created", delete = r.OperationCode = "Group.Deleted"
            Permission(cn, tx, r.Context)
            Dim id = If(create, 0, Integer.Parse(r.EntityId, CultureInfo.InvariantCulture))
            Dim before = If(create, New AuditGroupSnapshot With {.Header = New DataTable(), .Printers = New DataTable()}, Capture(cn, tx, id))
            If create Then
                LockAllocation(cn, tx)
            ElseIf Fingerprint(before) <> CStr(Db(r, "ExpectedGroupSnapshot")) Then
                Throw New InvalidOperationException("تغير التصنيف أو روابطه؛ حدّث العرض قبل المتابعة.")
            End If
            If r.OperationCode = "Group.PrinterLinked" Then
                Dim printer = Convert.ToInt32(Db(r, "LinkedPrinterId"), CultureInfo.InvariantCulture)
                If before.Printers.Rows.Cast(Of DataRow)().Any(Function(x) CInt(x("PTR_ID")) = printer) Then Throw New InvalidOperationException("الطابعة مرتبطة بالتصنيف مسبقًا.")
                Using cmd As New SqlCommand("INSERT dbo.GM_Printer(GM_ID,PTR_ID) VALUES(@Id,@Printer)", cn, tx)
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id : cmd.Parameters.Add("@Printer", SqlDbType.Int).Value = printer
                    cmd.ExecuteNonQuery()
                End Using
            ElseIf r.OperationCode = "Group.PrinterUnlinked" Then
                Dim link = Convert.ToInt32(Db(r, "PrinterLinkId"), CultureInfo.InvariantCulture)
                If Not before.Printers.Rows.Cast(Of DataRow)().Any(Function(x) CInt(x("T_ID")) = link) Then Throw New InvalidOperationException("الرابط غير موجود ضمن التصنيف المحدد.")
                Using cmd As New SqlCommand("DELETE dbo.GM_Printer WHERE T_ID=@Link AND GM_ID=@Id; SELECT @@ROWCOUNT;", cn, tx)
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id : cmd.Parameters.Add("@Link", SqlDbType.Int).Value = link
                    If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New InvalidOperationException("لم يُحذف رابط طابعة واحد.")
                End Using
            Else
                ' Original rank calculation scans all groups; hold this range until audit commits.
                AuditSalesQuantity.Rows(cn, tx, "SELECT GM_ID,Rank_Num FROM dbo.General_menu WITH(UPDLOCK,HOLDLOCK) ORDER BY GM_ID")
                Using cmd = AuditRequestCoordinator.Command(If(create, "dbo.GM_insert", If(delete, "dbo.GM_delete", "dbo.GM_update")), cn, tx)
                    cmd.Parameters.Add("@GM_ID", SqlDbType.Int).Value = id
                    If create Then cmd.Parameters("@GM_ID").Direction = ParameterDirection.Output
                    If Not delete Then
                        For Each name In AuditContract.GroupInputs("Group.Created")
                            Dim column = AuditContract.GroupFields("ProductGroup").Single(Function(x) x.Split("|"c)(0) = name).Split("|"c)(1)
                            Dim value = Db(r, name)
                            If AuditContract.FieldType(r.OperationCode, name) = "String" Then
                                cmd.Parameters.Add("@" & column, SqlDbType.NVarChar, 200).Value = value
                            Else
                                cmd.Parameters.Add("@" & column, SqlDbType.Int).Value = If(IsDBNull(value), value, CObj(Convert.ToInt32(value, CultureInfo.InvariantCulture)))
                            End If
                        Next
                        If Not create Then cmd.Parameters.Add("@Rank_Num", SqlDbType.Int).Value = Db(r, "RequestedRank")
                    End If
                    cmd.ExecuteNonQuery()
                    If create Then id = CInt(cmd.Parameters("@GM_ID").Value)
                End Using
            End If
            Dim after = Capture(cn, tx, id)
            If If(delete, after.Header.Rows.Count <> 0 OrElse after.Printers.Rows.Count <> 0, after.Header.Rows.Count <> 1) Then Throw New InvalidOperationException("لم يؤكد الإجراء نتيجة التصنيف.")
            Dim occurred = AuditRequestCoordinator.ServerUtcNow(cn, tx)
            Dim part As Func(Of Integer, AuditEventRequest) = Function(n) New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = n, .EventCode = r.OperationCode, .Outcome = 1, .EntityType = "ProductGroup", .EntityId = id.ToString(CultureInfo.InvariantCulture), .OccurredAtUtc = occurred}
            Dim events As New List(Of AuditEventRequest) From {part(1)}
            Append(events(0), "ProductGroup", id.ToString(CultureInfo.InvariantCulture), If(create, Nothing, before.Header.Rows(0)), If(delete, Nothing, after.Header.Rows(0)))
            Dim oldLinks = before.Printers.Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x("T_ID"))), newLinks = after.Printers.Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x("T_ID")))
            Dim detail As AuditEventRequest = Nothing
            For Each key In oldLinks.Keys.Union(newLinks.Keys).OrderBy(Function(x) x)
                If detail Is Nothing OrElse detail.Changes.Count + 2 > 100 Then
                    detail = part(events.Count + 1) : events.Add(detail)
                End If
                Append(detail, "GroupPrinter", key.ToString(CultureInfo.InvariantCulture), If(oldLinks.ContainsKey(key), oldLinks(key), Nothing), If(newLinks.ContainsKey(key), newLinks(key), Nothing))
            Next
            For Each item In events
                AuditContract.EventData(r, item)
            Next
            For Each item In events
                AuditWriter.WriteInTransaction(cn, tx, lease, item)
            Next
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1,
                .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", id.ToString(CultureInfo.InvariantCulture)}})}
        Catch
            lease.Faulted = True : Throw
        End Try
    End Function

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
