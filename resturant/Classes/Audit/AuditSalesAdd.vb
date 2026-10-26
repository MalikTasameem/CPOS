Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

' Wrap the two existing insert procedures; do not reproduce their pricing/merge rules.
Friend NotInheritable Class AuditSalesAdd
    Friend Const Operation As String = "Sales.LineAdded"

    Private Shared Function V(r As AuditOperationRequest, name As String) As String
        Return r.Inputs.Single(Function(x) x.Name = name).Value.Text
    End Function

    Private Shared Function Db(r As AuditOperationRequest, name As String) As Object
        Dim input = r.Inputs.Single(Function(x) x.Name = name)
        If input.Value.State = AuditValueState.SqlNull Then Return DBNull.Value
        Select Case input.ValueType
            Case "Integer" : Return Integer.Parse(input.Value.Text, CultureInfo.InvariantCulture)
            Case "Float" : Return Double.Parse(input.Value.Text, CultureInfo.InvariantCulture)
            Case "Boolean" : Return input.Value.Text = "1"
            Case "Date" : Return DateTime.ParseExact(input.Value.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture)
            Case Else : Return input.Value.Text
        End Select
    End Function

    Friend Shared Function ReadHeader(connection As String, context As AuditEventContext, bill As Integer, onUpdate As Boolean) As DataRow
        AuditContract.Context(context, Operation)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Dim row = AuditSalesLineEdit.Header(cn, tx, bill)
                AuditSalesLineEdit.AssertWritable(cn, tx, row, context, onUpdate)
                tx.Commit() : Return row
            End Using
        End Using
    End Function

    Friend Shared Function Request(context As AuditEventContext, header As DataRow, command As SqlCommand, allowNegative As Boolean) As AuditOperationRequest
        Dim pos = command.CommandText = "SB_Contents_INSERT"
        If Not pos AndAlso command.CommandText <> "SB_Contents_INSERT_2" Then Throw New ArgumentException("إجراء إضافة غير معتمد.")
        Dim getValue As Func(Of String, Object, Object) = Function(name, fallback) If(command.Parameters.Contains(name), command.Parameters(name).Value, fallback)
        If CInt(getValue("@SB_T_ID", 0)) <> CInt(header("T_ID")) Then Throw New ArgumentException("رأس الإضافة مختلف.")
        Dim values As New Dictionary(Of String, Object) From {
            {"InsertPath", If(pos, "POS", "Standard")}, {"ItemId", CInt(getValue("@IM_ID", 0))}, {"StoreId", CInt(getValue("@ST_ID", 0))},
            {"UnitId", CInt(getValue("@U_ID", 0))}, {"ItemUnitId", CInt(getValue("@U_IM_ID", 0))},
            {"RequestedQuantity", CDbl(getValue("@QYT", 1R))}, {"RequestedPrice", getValue(If(pos, "@IM_Price", "@Price"), DBNull.Value)},
            {"ExpiryDate", getValue("@D_Vaild", DBNull.Value)}, {"OnUpdate", CBool(getValue("@On_Update", False))},
            {"Barcode", getValue("@Barcode", DBNull.Value)}, {"LineNotes", getValue("@Notes", DBNull.Value)},
            {"CurrentQuantity", getValue("@Current_QTY", DBNull.Value)}, {"SerialCode", CStr(getValue("@Serial_Code", ""))},
            {"MinimumPriceUsed", Convert.ToInt32(getValue("@is_By_Min_SP", 0))}, {"MinimumPrice2Used", Convert.ToInt32(getValue("@is_By_Min_SP_2", 0))},
            {"NewRow", CBool(getValue("@SB_IM_NEW_ROW", True))}, {"SalesType", CInt(getValue("@SALES_TYPES", 0))},
            {"AllowNegative", allowNegative}, {"ExpectedHeaderSnapshot", AuditSalesLineEdit.Fingerprint(header)}}
        Dim allowed = If(pos, New String() {"@SB_T_ID", "@IM_ID", "@ST_ID", "@U_IM_ID", "@Barcode", "@D_Vaild", "@QYT", "@IM_Price", "@On_Update", "@SALES_TYPES"},
            New String() {"@SB_T_ID", "@IM_ID", "@ST_ID", "@U_ID", "@Price", "@On_Update", "@D_Vaild", "@QYT", "@Current_QTY", "@Notes", "@Barcode", "@Serial_Code", "@is_By_Min_SP", "@is_By_Min_SP_2", "@SB_IM_NEW_ROW"})
        If command.Parameters.Cast(Of SqlParameter)().Any(Function(x) Not allowed.Contains(x.ParameterName) OrElse x.Direction <> ParameterDirection.Input) Then Throw New ArgumentException("معامل إضافة غير معتمد.")
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .OperationCode = Operation, .Context = context.Copy(), .EntityType = "SalesInvoice", .EntityId = CInt(header("T_ID")).ToString(CultureInfo.InvariantCulture)}
        For Each definition In AuditContract.SalesAddInputs()
            Dim p = definition.Split("|"c), value = values(p(0))
            If value IsNot Nothing AndAlso Not IsDBNull(value) Then
                If p(1) = "Float" Then value = Convert.ToDouble(value, CultureInfo.CurrentCulture)
                If p(1) = "Date" Then value = Convert.ToDateTime(value, CultureInfo.CurrentCulture).Date
            End If
            r.Inputs.Add(New AuditInput With {.Name = p(0), .ValueType = p(1), .Value = AuditValue.Capture(p(1), value)})
        Next
        Validate(r) : Return r
    End Function

    Private Shared Sub Validate(r As AuditOperationRequest)
        AuditContract.Request(r)
        If r.OperationCode <> Operation OrElse CInt(Db(r, "ItemId")) <= 0 OrElse CInt(Db(r, "StoreId")) <= 0 Then Throw New ArgumentException("صنف أو مخزن الإضافة غير صالح.")
        For Each name In {"RequestedQuantity", "RequestedPrice", "CurrentQuantity"}
            Dim value = Db(r, name)
            If Not IsDBNull(value) AndAlso (Double.IsNaN(CDbl(value)) OrElse Double.IsInfinity(CDbl(value)) OrElse (name <> "CurrentQuantity" AndAlso CDbl(value) < 0)) Then Throw New ArgumentException("قيمة الإضافة غير صالحة.")
        Next
        If V(r, "InsertPath") = "Standard" AndAlso (CInt(Db(r, "UnitId")) <= 0 OrElse IsDBNull(Db(r, "RequestedPrice"))) Then Throw New ArgumentException("وحدة وسعر الإضافة مطلوبان.")
        AuditContract.Text(V(r, "Barcode"), If(V(r, "InsertPath") = "POS", 50, 100), "Barcode")
        AuditContract.Text(V(r, "LineNotes"), 500, "LineNotes") : AuditContract.Text(V(r, "SerialCode"), 100, "SerialCode")
        If Not {0, 1, 2}.Contains(CInt(Db(r, "SalesType"))) OrElse Not {0, 1}.Contains(CInt(Db(r, "MinimumPriceUsed"))) OrElse Not {0, 1}.Contains(CInt(Db(r, "MinimumPrice2Used"))) Then Throw New ArgumentException("نوع سعر غير معتمد.")
    End Sub

    Private Shared Function Lines(cn As SqlConnection, tx As SqlTransaction, bill As Integer) As Dictionary(Of Integer, DataRow)
        Dim cols = AuditContract.SalesDeleteFields("SalesLine").Select(Function(x) "[" & x.Split("|"c)(1) & "]")
        Return AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID," & String.Join(",", cols) & " FROM dbo.SB_Contents WITH(UPDLOCK,HOLDLOCK) WHERE Bill_T_ID=@P0 ORDER BY T_ID", bill).Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x("T_ID")))
    End Function

    Private Shared Function Part(r As AuditOperationRequest, header As DataRow, line As Integer, ordinal As Integer, time As DateTime) As AuditEventRequest
        Dim item As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = ordinal, .EventCode = Operation, .Outcome = 1, .EntityType = "SalesLine", .EntityId = line.ToString(CultureInfo.InvariantCulture), .OccurredAtUtc = time,
            .DocumentNumber = If(IsDBNull(header("SB_ID")), Nothing, Convert.ToString(header("SB_ID"), CultureInfo.InvariantCulture)), .BusinessDate = If(IsDBNull(header("Date")), CType(Nothing, DateTime?), CDate(header("Date")))}
        item.RelatedEntities.Add(New AuditEntityReference With {.Role = "Document", .EntityType = "SalesInvoice", .EntityId = r.EntityId})
        Return item
    End Function

    Private Shared Sub Append(item As AuditEventRequest, entity As String, id As String, before As DataRow, after As DataRow)
        For Each definition In AuditContract.SalesDeleteFields(entity)
            Dim p = definition.Split("|"c)
            item.Changes.Add(New AuditFieldChange With {.ChangeNo = item.Changes.Count + 1, .EntityType = entity, .EntityId = id, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1,
                .Before = If(before Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), before(p(1)))),
                .After = If(after Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), after(p(1))))})
        Next
    End Sub

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx) : Validate(request)
        If tx.IsolationLevel <> IsolationLevel.Serializable Then Throw New InvalidOperationException("الإضافة تتطلب معاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, bill = Integer.Parse(r.EntityId, CultureInfo.InvariantCulture), itemId = CInt(Db(r, "ItemId")), storeId = CInt(Db(r, "StoreId")), pos = V(r, "InsertPath") = "POS"
            Dim header = AuditSalesLineEdit.Header(cn, tx, bill)
            AuditSalesLineEdit.AssertWritable(cn, tx, header, r.Context, CBool(Db(r, "OnUpdate")))
            If AuditSalesLineEdit.Fingerprint(header) <> V(r, "ExpectedHeaderSnapshot") Then Throw New InvalidOperationException("تغيرت الفاتورة؛ حدّث العرض قبل الإضافة.")
            ' Protect the existing MAX+1 allocator, including writers using the old procedure.
            Using cmd As New SqlCommand("IF (SELECT COUNT(*) FROM dbo.SERVERS WITH(HOLDLOCK))<>1 THROW 51117,'Ambiguous server range.',1; DECLARE @s int,@e int,@n bigint; SELECT @s=START_ID,@e=END_ID FROM dbo.SERVERS WITH(HOLDLOCK); SELECT @n=ISNULL(CONVERT(bigint,MAX(T_ID)),@s)+1 FROM dbo.SB_Contents WITH(UPDLOCK,HOLDLOCK) WHERE T_ID BETWEEN @s AND @e; IF @n IS NULL OR @n>@e OR @n>2147483647 THROW 51117,'Sales line range exhausted.',1;", cn, tx)
                cmd.ExecuteNonQuery()
            End Using
            Dim unitSql = If(pos, " AND " & If(CInt(Db(r, "ItemUnitId")) = 0, "is_Default=1", "U_IM_ID=@P1"), " AND U_ID=@P1")
            Dim units = AuditSalesQuantity.Rows(cn, tx, "SELECT IM_ID,U_ID,U_Cargo FROM dbo.IM_Menu_Units_V WITH(HOLDLOCK) WHERE IM_ID=@P0" & unitSql, itemId, If(pos, CInt(Db(r, "ItemUnitId")), CInt(Db(r, "UnitId"))))
            If units.Rows.Count <> 1 OrElse IsDBNull(units.Rows(0)("U_Cargo")) OrElse CDbl(units.Rows(0)("U_Cargo")) <= 0 Then Throw New InvalidOperationException("الوحدة لا تتبع الصنف أو حمولتها غير صالحة.")
            Dim unitId = CInt(units.Rows(0)("U_ID"))
            Dim recipe = AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID,IM_ID,U_IM_ID,Struct_IM_ID,U_ID,Qty,U_Cargo,IM_U_ID FROM dbo.IM_Struct_Menu_V WITH(HOLDLOCK) WHERE IM_ID=@P0 AND IM_U_ID=@P1 ORDER BY T_ID", itemId, unitId)
            Dim old = Lines(cn, tx, bill), structures As New Dictionary(Of Integer, DataTable)(), stock As New SortedDictionary(Of String, DataRow)(StringComparer.Ordinal)
            Dim capture As Action(Of DataRow, DataTable) = Sub(row, children)
                Dim buckets = AuditSalesQuantity.Buckets(New AuditSalesQuantitySnapshot With {.Sale = New AuditSalesLineSnapshot With {.Header = header, .Line = row}, .Structures = children, .Recipe = recipe})
                For Each bucket In buckets
                    If Not stock.ContainsKey(bucket.Key) Then stock.Add(bucket.Key, bucket.Value)
                Next
            End Sub
            For Each row In old.Values.Where(Function(x) CInt(x("IM_ID")) = itemId AndAlso CInt(x("U_ID")) = unitId)
                Dim children = AuditSalesQuantity.Structures(cn, tx, CInt(row("T_ID")))
                structures.Add(CInt(row("T_ID")), children) : capture(row, children)
            Next
            Dim synthetic As New DataTable()
            synthetic.Columns.Add("IM_ID", GetType(Integer)) : synthetic.Columns.Add("ST_ID", GetType(Long)) : synthetic.Columns.Add("D_Vaild", GetType(String))
            capture(synthetic.Rows.Add(itemId, CLng(storeId), If(IsDBNull(Db(r, "ExpiryDate")), "", CDate(Db(r, "ExpiryDate")).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))), New DataTable())
            If Not IsDBNull(Db(r, "ExpiryDate")) Then
                ' Existing insert may substitute the most recent expiry when total stock <= 0.
                AuditSalesQuantity.Rows(cn, tx, "SELECT ISNULL(SUM(QTY),0) AS QTY FROM dbo.ST_Balance_V WITH(HOLDLOCK) WHERE IM_ID=@P0", itemId)
                Dim fallback = AuditSalesQuantity.Rows(cn, tx, "SELECT TOP(1) D_Vaild FROM dbo.SB_Contents WITH(HOLDLOCK) WHERE IM_ID=@P0 AND ST_ID=@P1 ORDER BY T_ID DESC", itemId, storeId)
                capture(synthetic.Rows.Add(itemId, CLng(storeId), If(fallback.Rows.Count = 0 OrElse IsDBNull(fallback.Rows(0)(0)), "", CStr(fallback.Rows(0)(0)))), New DataTable())
            End If
            For Each row In stock.Values
                row("AuditStock") = AuditSalesLineEdit.Stock(cn, tx, row)
            Next
            Using cmd = AuditRequestCoordinator.Command(If(pos, "dbo.SB_Contents_INSERT", "dbo.SB_Contents_INSERT_2"), cn, tx)
                cmd.Parameters.Add("@SB_T_ID", SqlDbType.Int).Value = bill
                For Each pair In New String() {"IM_ID|ItemId|Int", "ST_ID|StoreId|Int", "D_Vaild|ExpiryDate|Date", "QYT|RequestedQuantity|Float", "On_Update|OnUpdate|Bit"}
                    AddParameter(cmd, r, pair)
                Next
                cmd.Parameters.Add("@Barcode", SqlDbType.NVarChar, If(pos, 50, 100)).Value = Db(r, "Barcode")
                If pos Then
                    For Each pair In {"U_IM_ID|ItemUnitId|Int", "IM_Price|RequestedPrice|Float", "SALES_TYPES|SalesType|Int"}
                        AddParameter(cmd, r, pair)
                    Next
                Else
                    For Each pair In {"U_ID|UnitId|Int", "Price|RequestedPrice|Float", "Current_QTY|CurrentQuantity|Float", "is_By_Min_SP|MinimumPriceUsed|Int", "is_By_Min_SP_2|MinimumPrice2Used|Int", "SB_IM_NEW_ROW|NewRow|Bit"}
                        AddParameter(cmd, r, pair)
                    Next
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = Db(r, "LineNotes")
                    cmd.Parameters.Add("@Serial_Code", SqlDbType.NVarChar, 100).Value = Db(r, "SerialCode")
                End If
                cmd.ExecuteNonQuery()
            End Using
            If CBool(Db(r, "OnUpdate")) Then
                Using cmd = AuditRequestCoordinator.Command("dbo.AG_Balance_DependingUpdatedBill", cn, tx)
                    cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = bill : cmd.ExecuteNonQuery()
                End Using
            End If
            Dim latest = Lines(cn, tx, bill)
            If old.Keys.Any(Function(x) Not latest.ContainsKey(x)) Then Throw New InvalidOperationException("حُذف سطر أثناء الإضافة.")
            Dim changed = latest.Keys.Where(Function(x) Not old.ContainsKey(x) OrElse AuditSalesLineEdit.Fingerprint(old(x)) <> AuditSalesLineEdit.Fingerprint(latest(x))).ToArray()
            If changed.Length <> 1 Then Throw New InvalidOperationException("لم يتحدد سطر واحد فعلي للإضافة/الدمج.")
            Dim line = changed(0), after = latest(line), before = If(old.ContainsKey(line), old(line), Nothing)
            If CInt(after("IM_ID")) <> itemId OrElse CInt(after("U_ID")) <> unitId Then Throw New InvalidOperationException("الصنف المحفوظ مختلف.")
            Dim oldChildren = If(structures.ContainsKey(line), structures(line), New DataTable()), newChildren = AuditSalesQuantity.Structures(cn, tx, line)
            Dim affected = AuditSalesQuantity.Buckets(New AuditSalesQuantitySnapshot With {.Sale = New AuditSalesLineSnapshot With {.Header = header, .Line = after}, .Structures = newChildren, .Recipe = recipe})
            For Each bucket In AuditSalesQuantity.Buckets(New AuditSalesQuantitySnapshot With {.Sale = New AuditSalesLineSnapshot With {.Header = header, .Line = after}, .Structures = oldChildren, .Recipe = recipe})
                If Not affected.ContainsKey(bucket.Key) Then affected.Add(bucket.Key, bucket.Value)
            Next
            Dim afterHeader = AuditSalesLineEdit.Header(cn, tx, bill), time = AuditRequestCoordinator.ServerUtcNow(cn, tx)
            Dim events As New List(Of AuditEventRequest) From {Part(r, afterHeader, line, 1, time)}
            Append(events(0), "SalesLine", line.ToString(CultureInfo.InvariantCulture), before, after)
            Append(events(0), "SalesInvoice", r.EntityId, header, afterHeader)
            Dim groups As New List(Of AuditEventRequest)(), oldRows = oldChildren.Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x("T_ID"))), newRows = newChildren.Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x("T_ID")))
            For Each id In oldRows.Keys.Union(newRows.Keys).OrderBy(Function(x) x)
                Dim group = Part(r, afterHeader, line, 2, time)
                Append(group, "SalesStructure", id.ToString(CultureInfo.InvariantCulture), If(oldRows.ContainsKey(id), oldRows(id), Nothing), If(newRows.ContainsKey(id), newRows(id), Nothing)) : groups.Add(group)
            Next
            For Each bucket In affected
                If Not stock.ContainsKey(bucket.Key) Then Throw New InvalidOperationException("تأثير مخزني خارج النطاق المقفول.")
                bucket.Value("AuditStock") = AuditSalesLineEdit.Stock(cn, tx, bucket.Value)
                If Not CBool(Db(r, "AllowNegative")) AndAlso CDbl(bucket.Value("AuditStock")) < -0.0000001R AndAlso CDbl(bucket.Value("AuditStock")) < CDbl(stock(bucket.Key)("AuditStock")) - 0.0000001R Then Throw New InvalidOperationException("الرصيد الحالي للصنف أو أحد مكوناته لا يسمح بالإضافة.")
                Dim group = Part(r, afterHeader, line, 2, time)
                Append(group, "StockBucket", bucket.Key, stock(bucket.Key), bucket.Value) : groups.Add(group)
            Next
            Dim extra As AuditEventRequest = Nothing
            For Each group In groups
                If extra Is Nothing OrElse extra.Changes.Count + group.Changes.Count > 100 Then
                    extra = Part(r, afterHeader, line, events.Count + 1, time) : events.Add(extra)
                End If
                For Each change In group.Changes
                    change.ChangeNo = extra.Changes.Count + 1 : extra.Changes.Add(change)
                Next
            Next
            For Each item In events
                AuditContract.EventData(r, item)
            Next
            For Each item In events
                AuditWriter.WriteInTransaction(cn, tx, lease, item)
            Next
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", r.EntityId}, {"LineId", line.ToString(CultureInfo.InvariantCulture)}})}
        Catch
            lease.Faulted = True : Throw
        End Try
    End Function

    Private Shared Sub AddParameter(cmd As SqlCommand, r As AuditOperationRequest, definition As String)
        Dim p = definition.Split("|"c)
        cmd.Parameters.Add("@" & p(0), CType([Enum].Parse(GetType(SqlDbType), p(2)), SqlDbType)).Value = Db(r, p(1))
    End Sub

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
