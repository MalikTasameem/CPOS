Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Text

Friend NotInheritable Class AuditSalesQuantitySnapshot
    Friend Sale As AuditSalesLineSnapshot
    Friend Structures As DataTable
    Friend Recipe As DataTable
End Class

Friend NotInheritable Class AuditSalesQuantity
    Friend Const Operation As String = "Sales.LineQuantityStepped"

    Friend Shared Function Rows(cn As SqlConnection, tx As SqlTransaction, sql As String, ParamArray values As Integer()) As DataTable
        Using cmd As New SqlCommand(sql, cn, tx)
            For i = 0 To values.Length - 1
                cmd.Parameters.Add("@P" & i, SqlDbType.Int).Value = values(i)
            Next
            Dim dt As New DataTable()
            Using rd = cmd.ExecuteReader()
                dt.Load(rd)
            End Using
            Return dt
        End Using
    End Function

    Friend Shared Function Structures(cn As SqlConnection, tx As SqlTransaction, lineId As Integer) As DataTable
        Return Rows(cn, tx, "SELECT T_ID,SB_Cont_T_ID,[DATE],Struct_ID,U_ID,ST_ID,QTY,U_Cargo,ST_QTY,isDepended,Cost FROM dbo.SB_Contents_Structers WITH(UPDLOCK,HOLDLOCK) WHERE SB_Cont_T_ID=@P0 ORDER BY T_ID", lineId)
    End Function

    Friend Shared Function ReadInTransaction(cn As SqlConnection, tx As SqlTransaction, context As AuditEventContext, billId As Integer, lineId As Integer, onUpdate As Boolean) As AuditSalesQuantitySnapshot
        AuditContract.Context(context, Operation)
        Dim sale = AuditSalesLineEdit.ReadInTransaction(cn, tx, context, billId, lineId, onUpdate)
        Return New AuditSalesQuantitySnapshot With {.Sale = sale, .Structures = Structures(cn, tx, lineId),
            .Recipe = Rows(cn, tx, "SELECT T_ID,IM_ID,U_IM_ID,Struct_IM_ID,U_ID,Qty,U_Cargo,IM_U_ID FROM dbo.IM_Struct_Menu_V WITH(HOLDLOCK) WHERE IM_ID=@P0 AND IM_U_ID=@P1 ORDER BY T_ID", CInt(sale.Line("IM_ID")), CInt(sale.Line("U_ID")))}
    End Function

    Friend Shared Function ReadForEdit(connection As String, context As AuditEventContext, billId As Integer, lineId As Integer, onUpdate As Boolean) As AuditSalesQuantitySnapshot
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Dim result = ReadInTransaction(cn, tx, context, billId, lineId, onUpdate)
                tx.Commit() : Return result
            End Using
        End Using
    End Function

    Friend Shared Function Fingerprint(dt As DataTable) As String
        Dim value = String.Join("|", dt.Rows.Cast(Of DataRow)().Select(Function(x) AuditSalesLineEdit.Fingerprint(x)))
        Return BitConverter.ToString(AuditCanonical.Hash(Encoding.UTF8.GetBytes(value))).Replace("-", "")
    End Function

    Friend Shared Function Request(context As AuditEventContext, old As AuditSalesQuantitySnapshot, delta As Integer, displayed As Decimal, onUpdate As Boolean, allowNegative As Boolean) As AuditOperationRequest
        If old Is Nothing Then Throw New ArgumentException("لقطة الصنف مطلوبة.")
        If displayed <> CDec(old.Sale.Line("QTY")) Then Throw New InvalidOperationException("تغيرت الكمية منذ عرضها؛ حدّث الفاتورة أولًا.")
        AuditSalesLineEdit.Exact(displayed, True)
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = Operation, .EntityType = "SalesInvoice", .EntityId = CInt(old.Sale.Header("T_ID")).ToString(CultureInfo.InvariantCulture)}
        Dim inputs As New Dictionary(Of String, Object) From {{"LineId", CLng(old.Sale.Line("T_ID"))}, {"QuantityDelta", delta}, {"DisplayedQuantity", displayed},
            {"ExpectedLineSnapshot", AuditSalesLineEdit.Fingerprint(old.Sale.Line)}, {"ExpectedHeaderSnapshot", AuditSalesLineEdit.Fingerprint(old.Sale.Header)},
            {"ExpectedStructureSnapshot", Fingerprint(old.Structures)}, {"ExpectedRecipeSnapshot", Fingerprint(old.Recipe)}, {"OnUpdate", onUpdate}, {"AllowNegative", allowNegative}}
        For Each p In inputs
            Dim type = AuditContract.FieldType(Operation, p.Key)
            r.Inputs.Add(New AuditInput With {.Name = p.Key, .ValueType = type, .Value = AuditValue.Capture(type, p.Value)})
        Next
        AuditContract.Request(r) : Return r
    End Function

    Private Shared Function V(r As AuditOperationRequest, name As String) As String
        Return r.Inputs.Single(Function(x) x.Name = name).Value.Text
    End Function

    Friend Shared Function Buckets(s As AuditSalesQuantitySnapshot) As SortedDictionary(Of String, DataRow)
        Dim result As New SortedDictionary(Of String, DataRow)(StringComparer.Ordinal)
        AddBucket(result, CInt(s.Sale.Line("IM_ID")), CLng(s.Sale.Line("ST_ID")), If(IsDBNull(s.Sale.Line("D_Vaild")), "", CStr(s.Sale.Line("D_Vaild"))))
        For Each row As DataRow In s.Structures.Rows
            AddBucket(result, CInt(row("Struct_ID")), CLng(row("ST_ID")), "")
        Next
        For Each row As DataRow In s.Recipe.Rows
            AddBucket(result, CInt(row("Struct_IM_ID")), CLng(s.Sale.Line("ST_ID")), "")
        Next
        Return result
    End Function

    Private Shared Sub AddBucket(target As SortedDictionary(Of String, DataRow), item As Integer, store As Long, expiry As String)
        Dim dt As New DataTable()
        dt.Columns.Add("IM_ID", GetType(Integer)) : dt.Columns.Add("ST_ID", GetType(Long)) : dt.Columns.Add("D_Vaild", GetType(String))
        Dim row = dt.Rows.Add(item, store, expiry)
        Dim id = AuditSalesLineEdit.Fingerprint(row)
        dt.Columns.Add("AuditStock", GetType(Double))
        If Not target.ContainsKey(id) Then target.Add(id, row)
    End Sub

    Friend Shared Function NewEvent(r As AuditOperationRequest, header As DataRow, ordinal As Integer, time As DateTime) As AuditEventRequest
        Dim item As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = ordinal, .EventCode = r.OperationCode, .Outcome = 1,
            .EntityType = "SalesLine", .EntityId = V(r, "LineId"), .OccurredAtUtc = time,
            .DocumentNumber = If(IsDBNull(header("SB_ID")), Nothing, Convert.ToString(header("SB_ID"), CultureInfo.InvariantCulture)),
            .BusinessDate = If(IsDBNull(header("Date")), CType(Nothing, DateTime?), CDate(header("Date")))}
        item.RelatedEntities.Add(New AuditEntityReference With {.Role = "Document", .EntityType = "SalesInvoice", .EntityId = r.EntityId})
        Return item
    End Function

    Friend Shared Function StructureChanges(before As DataRow, after As DataRow, id As String) As List(Of AuditFieldChange)
        Dim result As New List(Of AuditFieldChange)()
        For Each definition In AuditContract.SalesQuantityFields("SalesStructure")
            Dim p = definition.Split("|"c)
            result.Add(New AuditFieldChange With {.EntityType = "SalesStructure", .EntityId = id, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1,
                .Before = If(before Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), before(p(1)))),
                .After = If(after Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), after(p(1))))})
        Next
        Return result
    End Function

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse request.OperationCode <> Operation Then Throw New ArgumentException("خطوة الكمية تتطلب المعاملة والعقد المحددين.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, billId = Integer.Parse(r.EntityId, CultureInfo.InvariantCulture), lineId = Integer.Parse(V(r, "LineId"), CultureInfo.InvariantCulture)
            Dim before = ReadInTransaction(cn, tx, r.Context, billId, lineId, V(r, "OnUpdate") = "1")
            If AuditSalesLineEdit.Fingerprint(before.Sale.Line) <> V(r, "ExpectedLineSnapshot") OrElse AuditSalesLineEdit.Fingerprint(before.Sale.Header) <> V(r, "ExpectedHeaderSnapshot") OrElse Fingerprint(before.Structures) <> V(r, "ExpectedStructureSnapshot") OrElse Fingerprint(before.Recipe) <> V(r, "ExpectedRecipeSnapshot") Then Throw New InvalidOperationException("تغير السطر أو الفاتورة أو المكونات منذ العرض؛ حدّث الفاتورة قبل إعادة المحاولة.")
            Dim delta = Integer.Parse(V(r, "QuantityDelta"), CultureInfo.InvariantCulture)
            Dim quantity = CDec(before.Sale.Line("QTY"))
            If quantity <> Decimal.Parse(V(r, "DisplayedQuantity"), CultureInfo.InvariantCulture) Then Throw New InvalidOperationException("الكمية المعروضة تغيرت.")
            Dim values As New Dictionary(Of String, String) From {{"EntityId", r.EntityId}, {"LineId", V(r, "LineId")}}
            If quantity + delta < 1D Then
                Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 5, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 5, values)}
            End If
            AuditSalesLineEdit.Exact(quantity + delta, True)
            Dim beforeBuckets = Buckets(before)
            For Each b In beforeBuckets.Values
                b("AuditStock") = AuditSalesLineEdit.Stock(cn, tx, b)
            Next
            before.Sale.Line.Table.Columns.Add("AuditStock", GetType(Double))
            before.Sale.Line("AuditStock") = AuditSalesLineEdit.Stock(cn, tx, before.Sale.Line)
            Using cmd = AuditRequestCoordinator.Command("dbo.SB_Contents_Change_IM_Qty", cn, tx)
                cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = lineId
                cmd.Parameters.Add("@Def", SqlDbType.Int).Value = delta
                cmd.Parameters.Add("@On_Update", SqlDbType.Bit).Value = V(r, "OnUpdate") = "1"
                cmd.ExecuteNonQuery()
            End Using
            Dim after = ReadInTransaction(cn, tx, r.Context, billId, lineId, V(r, "OnUpdate") = "1")
            If CDec(after.Sale.Line("QTY")) <> quantity + delta Then Throw New InvalidOperationException("لم ينفذ الإجراء خطوة الكمية المطلوبة.")
            If Fingerprint(after.Recipe) <> Fingerprint(before.Recipe) Then Throw New InvalidOperationException("تغير تعريف المكونات أثناء التنفيذ.")
            Dim afterBuckets = Buckets(after)
            If afterBuckets.Keys.Any(Function(x) Not beforeBuckets.ContainsKey(x)) Then Throw New InvalidOperationException("ظهر تأثير مخزون خارج النطاق المقفول.")
            For Each b In beforeBuckets
                Dim latest = b.Value.Table.Clone().Rows.Add(b.Value.ItemArray)
                latest("AuditStock") = AuditSalesLineEdit.Stock(cn, tx, latest)
                afterBuckets(b.Key) = latest
                If V(r, "AllowNegative") = "0" AndAlso CDbl(latest("AuditStock")) < -0.0000001R AndAlso CDbl(latest("AuditStock")) < CDbl(b.Value("AuditStock")) - 0.0000001R Then Throw New InvalidOperationException("الرصيد الحالي للصنف أو أحد مكوناته لا يسمح بالتعديل.")
            Next
            after.Sale.Line.Table.Columns.Add("AuditStock", GetType(Double))
            after.Sale.Line("AuditStock") = AuditSalesLineEdit.Stock(cn, tx, after.Sale.Line)
            Dim time = AuditRequestCoordinator.ServerUtcNow(cn, tx)
            Dim events As New List(Of AuditEventRequest) From {NewEvent(r, after.Sale.Header, 1, time)}
            AuditSalesLineEdit.AddChanges(events(0), before.Sale.Line, after.Sale.Line, AuditContract.SalesLineFields(), "SalesLine", V(r, "LineId"))
            AuditSalesLineEdit.AddChanges(events(0), before.Sale.Header, after.Sale.Header, AuditContract.SalesHeaderFields(), "SalesInvoice", r.EntityId)
            Dim groups As New List(Of List(Of AuditFieldChange))()
            Dim oldRows = before.Structures.Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x("T_ID")))
            Dim newRows = after.Structures.Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x("T_ID")))
            For Each id In oldRows.Keys.Union(newRows.Keys).OrderBy(Function(x) x)
                groups.Add(StructureChanges(If(oldRows.ContainsKey(id), oldRows(id), Nothing), If(newRows.ContainsKey(id), newRows(id), Nothing), id.ToString(CultureInfo.InvariantCulture)))
            Next
            For Each b In beforeBuckets
                Dim part = NewEvent(r, after.Sale.Header, 2, time)
                AuditSalesLineEdit.AddChanges(part, b.Value, afterBuckets(b.Key), AuditContract.SalesQuantityFields("StockBucket"), "StockBucket", b.Key)
                groups.Add(part.Changes)
            Next
            Dim extra As AuditEventRequest = Nothing
            For Each group In groups
                If extra Is Nothing OrElse extra.Changes.Count + group.Count > 100 Then
                    extra = NewEvent(r, after.Sale.Header, events.Count + 1, time) : events.Add(extra)
                End If
                For Each change In group
                    change.ChangeNo = extra.Changes.Count + 1 : extra.Changes.Add(change)
                Next
            Next
            ' Validate every page before writing; no truncation or partially logged components.
            For Each item In events
                AuditContract.EventData(r, item)
            Next
            For Each item In events
                AuditWriter.WriteInTransaction(cn, tx, lease, item)
            Next
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, values)}
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
                    tx.Commit() : result.Status = "Completed" : Return result
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
