Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Text

Friend NotInheritable Class AuditDocumentVoidSnapshot
    Friend Tables As New SortedDictionary(Of String, DataTable)(StringComparer.Ordinal)
    Friend ReadOnly Property Header As DataRow
        Get
            Return Tables("BusinessDocument").Rows(0)
        End Get
    End Property
End Class

' Observes the existing cancellation, including its deferred defects. Does not repair business SQL.
Friend NotInheritable Class AuditDocumentVoid
    Friend Const Operation As String = "Document.Voided"

    Private Shared Function Rows(cn As SqlConnection, tx As SqlTransaction, entity As String, source As String, key As String, predicate As String, id As Integer, Optional extra As String = "") As DataTable
        Dim columns = AuditContract.VoidFields(entity).Select(Function(x) "[" & x.Split("|"c)(1) & "]")
        Dim dt As New DataTable(entity)
        Using cmd As New SqlCommand("SELECT " & key & " AS AuditRowId," & String.Join(",", columns) & extra & " FROM " & source & " WITH(UPDLOCK,HOLDLOCK) WHERE " & predicate & " ORDER BY " & key, cn, tx)
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id
            Using reader = cmd.ExecuteReader()
                dt.Load(reader)
            End Using
        End Using
        Return dt
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, context As AuditEventContext, kind As Integer)
        Dim spec = AuditContract.VoidSpec(kind)
        AuditContract.Context(context, Operation)
        If Not spec(2).Split(","c).Contains(context.ScreenCode) Then Throw New InvalidOperationException("الشاشة لا تطابق نوع مستند الإلغاء.")
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@User AND u.is_Allow=1 AND v.[" & spec(1) & "]=1", cn, tx)
            cmd.Parameters.Add("@User", SqlDbType.Int).Value = context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("لا تملك صلاحية إلغاء هذا المستند: " & spec(1))
        End Using
    End Sub

    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, id As Integer, kind As Integer, Optional stockBefore As DataTable = Nothing) As AuditDocumentVoidSnapshot
        Dim spec = AuditContract.VoidSpec(kind), result As New AuditDocumentVoidSnapshot()
        Dim columns = AuditContract.VoidFields("BusinessDocument").Select(Function(x) x.Split("|"c)(1)).Where(Function(x) x <> "AuditDocumentNumber").Select(Function(x) "[" & x & "]")
        Dim header As New DataTable("BusinessDocument")
        Using cmd As New SqlCommand("SELECT T_ID AS AuditRowId,[" & spec(0) & "] AS AuditDocumentNumber," & String.Join(",", columns) & " FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@Id", cn, tx)
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id
            Using reader = cmd.ExecuteReader()
                header.Load(reader)
            End Using
        End Using
        If header.Rows.Count <> 1 OrElse CInt(header.Rows(0)("BsType_ID")) <> kind Then Throw New InvalidOperationException("المستند غير موجود أو نوعه مختلف عن الشاشة.")
        result.Tables.Add("BusinessDocument", header)
        If spec(3) <> "" Then result.Tables.Add(spec(3), Rows(cn, tx, spec(3), "dbo.[" & spec(3) & "]", "T_ID", "[" & spec(4) & "]=@Id", id))
        If kind = 13 OrElse kind = 18 Then result.Tables.Add("FRM_Contents_Details", Rows(cn, tx, "FRM_Contents_Details", "dbo.FRM_Contents_Details", "T_ID", "Bill_T_ID=@Id", id))
        If kind = 1 Then
            result.Tables.Add("SalesStructure", Rows(cn, tx, "SalesStructure", "dbo.SB_Contents_Structers", "T_ID", "SB_Cont_T_ID IN(SELECT T_ID FROM dbo.SB_Contents WITH(HOLDLOCK) WHERE Bill_T_ID=@Id)", id))
            Dim queue As New DataTable("CancellationPrintQueue")
            queue.Columns.Add("AuditRowId", GetType(String)) : queue.Columns.Add("RowCount", GetType(Long))
            Using cmd As New SqlCommand("SELECT COUNT_BIG(*) FROM dbo.TB_AutoPrint_tmp WITH(UPDLOCK,HOLDLOCK)", cn, tx)
                queue.Rows.Add("Queue", CLng(cmd.ExecuteScalar()))
            End Using
            result.Tables.Add(queue.TableName, queue)
        End If
        result.Tables.Add("Receipt", Rows(cn, tx, "Receipt", "dbo.Agents_Balance_MV_RCT", "T_ID", "Receipt_Tran_ID=@Id", id))
        result.Tables.Add("TreasuryMovement", Rows(cn, tx, "TreasuryMovement", "dbo.Treasury_Balance_MV", "T_ID", "AGBalance_T_ID IN(SELECT T_ID FROM dbo.Agents_Balance_MV_RCT WITH(HOLDLOCK) WHERE Receipt_Tran_ID=@Id)", id))
        Dim tableId = If(IsDBNull(result.Header("Table_ID")), 0, CInt(result.Header("Table_ID")))
        result.Tables.Add("DiningTable", Rows(cn, tx, "DiningTable", "dbo.[Tables]", "TB_ID", "TB_ID=@Id", tableId))
        Dim stock As New DataTable("StockBucket")
        stock.Columns.Add("AuditRowId", GetType(String)) : stock.Columns.Add("IM_ID", GetType(Integer))
        stock.Columns.Add("ST_ID", GetType(Long)) : stock.Columns.Add("D_Vaild", GetType(String)) : stock.Columns.Add("AuditStock", GetType(Double))
        Dim buckets As New SortedDictionary(Of String, Object())(StringComparer.Ordinal)
        For Each entry In result.Tables
            Dim dt = entry.Value, isStructure = entry.Key = "SalesStructure"
            Dim itemColumn = If(isStructure, "Struct_ID", "IM_ID")
            If Not dt.Columns.Contains(itemColumn) OrElse Not dt.Columns.Contains("ST_ID") Then Continue For
            For Each row As DataRow In dt.Rows
                If IsDBNull(row(itemColumn)) OrElse IsDBNull(row("ST_ID")) Then Continue For
                AddBucket(buckets, CInt(row(itemColumn)), CLng(row("ST_ID")), If(isStructure OrElse IsDBNull(row("D_Vaild")), "", CStr(row("D_Vaild"))))
            Next
        Next
        If stockBefore IsNot Nothing Then
            For Each row As DataRow In stockBefore.Rows
                AddBucket(buckets, CInt(row("IM_ID")), CLng(row("ST_ID")), CStr(row("D_Vaild")))
            Next
        End If
        For Each bucket In buckets
            Using cmd As New SqlCommand("SELECT ISNULL(SUM(QTY),0) FROM dbo.ST_Balance_V WITH(HOLDLOCK) WHERE IM_ID=@Item AND ST_ID=@Store AND ISNULL(D_Vaild,'')=@Expiry", cn, tx)
                cmd.Parameters.Add("@Item", SqlDbType.Int).Value = bucket.Value(0)
                cmd.Parameters.Add("@Store", SqlDbType.BigInt).Value = bucket.Value(1)
                cmd.Parameters.Add("@Expiry", SqlDbType.NVarChar, 50).Value = bucket.Value(2)
                stock.Rows.Add(bucket.Key, bucket.Value(0), bucket.Value(1), bucket.Value(2), CDbl(cmd.ExecuteScalar()))
            End Using
        Next
        result.Tables.Add(stock.TableName, stock)
        Return result
    End Function

    Private Shared Sub AddBucket(buckets As SortedDictionary(Of String, Object()), item As Integer, store As Long, expiry As String)
        Dim key = BitConverter.ToString(AuditCanonical.Hash(Encoding.UTF8.GetBytes(item.ToString(CultureInfo.InvariantCulture) & "|" & store.ToString(CultureInfo.InvariantCulture) & "|" & expiry))).Replace("-", "")
        buckets(key) = {item, store, expiry}
    End Sub

    Friend Shared Function Fingerprint(snapshot As AuditDocumentVoidSnapshot) As String
        Dim text = String.Join("|", snapshot.Tables.Select(Function(x) x.Key & ":" & AuditSalesQuantity.Fingerprint(x.Value)))
        Return BitConverter.ToString(AuditCanonical.Hash(Encoding.UTF8.GetBytes(text))).Replace("-", "")
    End Function

    Friend Shared Function Read(connection As String, context As AuditEventContext, id As Integer, kind As Integer) As AuditDocumentVoidSnapshot
        If id <= 0 Then Throw New ArgumentException("رقم حركة المستند غير صالح.")
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Permission(cn, tx, context, kind)
                Dim snapshot = Capture(cn, tx, id, kind)
                If CInt(snapshot.Header("isVoid")) <> 0 Then Throw New InvalidOperationException("المستند ملغى مسبقًا؛ لم تُنشأ محاولة إلغاء أخرى.")
                tx.Commit() : Return snapshot
            End Using
        End Using
    End Function

    Friend Shared Function Request(context As AuditEventContext, snapshot As AuditDocumentVoidSnapshot) As AuditOperationRequest
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = Operation,
            .EntityType = "BusinessDocument", .EntityId = Convert.ToString(snapshot.Header("AuditRowId"), CultureInfo.InvariantCulture)}
        r.Inputs.Add(New AuditInput With {.Name = "DocumentType", .ValueType = "Integer", .Value = AuditValue.Capture("Integer", snapshot.Header("BsType_ID"))})
        r.Inputs.Add(New AuditInput With {.Name = "ExpectedGraphSnapshot", .ValueType = "String", .Value = AuditValue.Capture("String", Fingerprint(snapshot))})
        AuditContract.Request(r) : Return r
    End Function

    Private Shared Function EventPart(r As AuditOperationRequest, header As DataRow, ordinal As Integer, occurred As DateTime) As AuditEventRequest
        Return New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = ordinal, .EventCode = Operation, .Outcome = 1,
            .EntityType = r.EntityType, .EntityId = r.EntityId, .OccurredAtUtc = occurred, .BusinessDate = CDate(header("Date")),
            .DocumentNumber = If(IsDBNull(header("AuditDocumentNumber")), Nothing, Convert.ToString(header("AuditDocumentNumber"), CultureInfo.InvariantCulture))}
    End Function

    Private Shared Function Changes(entity As String, id As String, before As DataRow, after As DataRow) As List(Of AuditFieldChange)
        Dim values As New List(Of AuditFieldChange)()
        For Each definition In AuditContract.VoidFields(entity)
            Dim p = definition.Split("|"c)
            values.Add(New AuditFieldChange With {.EntityType = entity, .EntityId = id, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1,
                .Before = AuditValue.Capture(p(2), before(p(1))), .After = If(after Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), after(p(1))))})
        Next
        Return values
    End Function

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse request.OperationCode <> Operation Then Throw New ArgumentException("الإلغاء يتطلب عقده ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, id = Integer.Parse(r.EntityId, CultureInfo.InvariantCulture)
            Dim kind = Integer.Parse(r.Inputs.Single(Function(x) x.Name = "DocumentType").Value.Text, CultureInfo.InvariantCulture)
            Permission(cn, tx, r.Context, kind)
            Dim before = Capture(cn, tx, id, kind)
            If CInt(before.Header("isVoid")) <> 0 OrElse Fingerprint(before) <> r.Inputs.Single(Function(x) x.Name = "ExpectedGraphSnapshot").Value.Text Then Throw New InvalidOperationException("تغير المستند أو توابعه؛ حدّث عرضه قبل الإلغاء.")
            Using command = AuditRequestCoordinator.Command("dbo.AG_Balance_Void_Row", cn, tx)
                command.Parameters.Add("@T_ID", SqlDbType.Int).Value = id
                command.ExecuteNonQuery()
            End Using
            Dim after = Capture(cn, tx, id, kind, before.Tables("StockBucket"))
            If CInt(after.Header("isVoid")) <> 1 Then Throw New InvalidOperationException("لم يؤكد الإجراء إلغاء المستند.")
            Dim occurred = AuditRequestCoordinator.ServerUtcNow(cn, tx)
            Dim events As New List(Of AuditEventRequest) From {EventPart(r, after.Header, 1, occurred)}
            For Each value In Changes("BusinessDocument", r.EntityId, before.Header, after.Header)
                value.ChangeNo = events(0).Changes.Count + 1 : events(0).Changes.Add(value)
            Next
            Dim extra As AuditEventRequest = Nothing
            For Each entry In before.Tables.Where(Function(x) x.Key <> "BusinessDocument")
                Dim nextRows = after.Tables(entry.Key).Rows.Cast(Of DataRow)().ToDictionary(Function(x) Convert.ToString(x("AuditRowId"), CultureInfo.InvariantCulture), StringComparer.Ordinal)
                For Each row As DataRow In entry.Value.Rows
                    Dim rowId = Convert.ToString(row("AuditRowId"), CultureInfo.InvariantCulture)
                    Dim values = Changes(entry.Key, rowId, row, If(nextRows.ContainsKey(rowId), nextRows(rowId), Nothing))
                    nextRows.Remove(rowId)
                    If extra Is Nothing OrElse extra.Changes.Count + values.Count > 100 Then
                        extra = EventPart(r, after.Header, events.Count + 1, occurred) : events.Add(extra)
                    End If
                    For Each value In values
                        value.ChangeNo = extra.Changes.Count + 1 : extra.Changes.Add(value)
                    Next
                Next
                If nextRows.Count <> 0 Then Throw New InvalidOperationException("أضاف الإلغاء صفوف أعمال غير متوقعة؛ أُوقف الاعتماد للمراجعة.")
            Next
            For Each item In events
                AuditContract.EventData(r, item)
            Next
            For Each item In events
                AuditWriter.WriteInTransaction(cn, tx, lease, item)
            Next
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1,
                .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", r.EntityId}})}
        Catch
            lease.Faulted = True : Throw
        End Try
    End Function

    Friend Shared Sub Save(connection As String, request As AuditOperationRequest)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    ExecuteInTransaction(cn, tx, request)
                    tx.Commit()
                Catch
                    Try
                        If tx.Connection IsNot Nothing Then tx.Rollback()
                    Catch
                    End Try
                    Throw
                End Try
            End Using
        End Using
    End Sub
End Class
