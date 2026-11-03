Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Text

Friend NotInheritable Class AuditPurchaseSnapshot
    Friend Header As DataTable
    Friend Lines As DataTable
    Friend Item As DataTable
    Friend Units As DataTable
    Friend Expiries As DataTable
    Friend Stock As DataTable
    Friend References As DataTable
End Class

' Add only. Deletion and unit editing have a separate cost-recount graph.
Friend NotInheritable Class AuditPurchaseAdd
    Friend Const Operation As String = "Purchase.LineAdded"
    Friend Shared Function V(r As AuditOperationRequest, name As String) As String
        Return r.Inputs.Single(Function(x) x.Name = name).Value.Text
    End Function
    Private Shared Function Columns(entity As String) As String
        Return String.Join(",", AuditContract.PurchaseFields(entity).Select(Function(x) "[" & x.Split("|"c)(1) & "]"))
    End Function
    Friend Shared Function Request(context As AuditEventContext, command As SqlCommand, bill As Integer, kind As Integer, expiries As IEnumerable(Of DateTime)) As AuditOperationRequest
        If command.CommandText <> "Pch_Details_Insert" OrElse CInt(AuditFinance.Parameter(command, "Pch_T_ID")) <> bill Then Throw New ArgumentException("إجراء/فاتورة إضافة الصنف غير مطابق.")
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = Operation, .EntityType = "PurchaseDocument", .EntityId = bill.ToString(CultureInfo.InvariantCulture)}
        For Each definition In AuditContract.PurchaseInputs()
            Dim p = definition.Split("|"c), value As Object
            If p(4) = "R" Then
                value = AuditFinance.Parameter(command, p(1))
            Else
                Dim parameter = command.Parameters.Cast(Of SqlParameter)().SingleOrDefault(Function(x) x.ParameterName.TrimStart("@"c).Equals(p(1), StringComparison.OrdinalIgnoreCase))
                value = If(parameter Is Nothing OrElse parameter.Value Is Nothing, DBNull.Value, parameter.Value)
            End If
            If Not IsDBNull(value) Then
                Select Case p(2)
                    Case "Decimal" : value = Convert.ToDecimal(value, CultureInfo.CurrentCulture)
                    Case "Float" : value = Convert.ToDouble(value, CultureInfo.CurrentCulture)
                    Case "Boolean" : value = Convert.ToBoolean(value, CultureInfo.CurrentCulture)
                    Case "Integer" : value = Convert.ToInt32(value, CultureInfo.CurrentCulture)
                    Case "String" : value = If(p(3) = "date", Convert.ToDateTime(value, CultureInfo.CurrentCulture).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), Convert.ToString(value))
                End Select
            End If
            r.Inputs.Add(New AuditInput With {.Name = p(0), .ValueType = p(2), .Value = AuditValue.Capture(p(2), value)})
        Next
        r.Inputs.Add(New AuditInput With {.Name = "DocumentType", .ValueType = "Integer", .Value = AuditValue.Capture("Integer", kind)})
        r.Inputs.Add(New AuditInput With {.Name = "AdditionalExpiries", .ValueType = "String", .Value = AuditValue.Capture("String", String.Join(";", expiries.Select(Function(x) x.ToString("yyyy-MM-ddTHH:mm:ss.fffffff", CultureInfo.InvariantCulture))))})
        r.Inputs.Add(New AuditInput With {.Name = "ExpectedPurchaseSnapshot", .ValueType = "String", .Value = AuditValue.Capture("String", New String("0"c, 64))})
        AuditContract.Request(r) : Return r
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest)
        Dim column = If(V(r, "OnUpdate") = "1", "Pch_Update", "Pch")
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@Id AND u.is_Allow=1 AND v.[" & column & "]=1", cn, tx)
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = r.Context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("لا تملك صلاحية إضافة صنف المشتريات/الجرد: " & column)
        End Using
    End Sub
    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest) As AuditPurchaseSnapshot
        Dim bill = CInt(r.EntityId), item = CInt(V(r, "SelectedItemId")), unit = CInt(V(r, "SelectedUnitId")), store = CInt(V(r, "SelectedStoreId")), kind = CInt(V(r, "DocumentType"))
        Dim s As New AuditPurchaseSnapshot
        s.Header = AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID,EditOpenedAt,EditOpenedBy,[" & If(kind = 7, "Pch_ID", "Jrd_ID") & "] AS AuditDocumentNumber," & Columns("PurchaseDocument").Replace("[AuditDocumentNumber],", "") & " FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@P0", bill)
        If s.Header.Rows.Count <> 1 OrElse CInt(s.Header.Rows(0)("BsType_ID")) <> kind OrElse CInt(s.Header.Rows(0)("isVoid")) <> 0 Then Throw New InvalidOperationException("فاتورة المشتريات/الجرد غير موجودة أو ملغاة أو نوعها مختلف.")
        Dim h = s.Header.Rows(0)
        If Not {0, 1}.Contains(CInt(h("isDepended"))) Then Throw New InvalidOperationException("حالة اعتماد الفاتورة غير معروفة.")
        If CInt(h("isDepended")) = 1 AndAlso (V(r, "OnUpdate") <> "1" OrElse IsDBNull(h("EditOpenedAt")) OrElse IsDBNull(h("EditOpenedBy")) OrElse CInt(h("EditOpenedBy")) <> r.Context.ActorUserId.Value) Then Throw New InvalidOperationException("افتح تعديل الفاتورة بالمستخدم الحالي أولًا.")
        s.Lines = AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM dbo.Pch_Details WITH(UPDLOCK,HOLDLOCK) WHERE Pch_T_ID=@P0 ORDER BY T_ID", bill)
        s.Item = AuditSalesQuantity.Rows(cn, tx, "SELECT IM_ID," & Columns("PurchaseItem") & " FROM dbo.IM_Menu WITH(UPDLOCK,HOLDLOCK) WHERE IM_ID=@P0", item)
        s.Units = AuditSalesQuantity.Rows(cn, tx, "SELECT x.U_IM_ID,x.IM_ID,x.U_ID,x.Price,x.Min_SP,x.Min_SP_2,u.U_Cargo AS AuditUnitCargo FROM dbo.IM_Units x WITH(UPDLOCK,HOLDLOCK) LEFT JOIN dbo.Units u WITH(HOLDLOCK) ON u.U_ID=x.U_ID WHERE x.IM_ID=@P0 ORDER BY x.U_IM_ID", item)
        s.Expiries = AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM dbo.Valid_Notes WITH(UPDLOCK,HOLDLOCK) WHERE Pch_T_ID IN(SELECT T_ID FROM dbo.Pch_Details WITH(HOLDLOCK) WHERE Pch_T_ID=@P0) ORDER BY T_ID", bill)
        s.Stock = AuditSalesQuantity.Rows(cn, tx, "SELECT IM_ID,CONVERT(bigint,ST_ID) AS ST_ID,ISNULL(D_Vaild,'') AS D_Vaild,CONVERT(float,ISNULL(SUM(QTY),0)) AS AuditStock FROM dbo.ST_Balance_V WITH(HOLDLOCK) WHERE IM_ID=@P0 GROUP BY IM_ID,ST_ID,ISNULL(D_Vaild,'') ORDER BY IM_ID,ST_ID,ISNULL(D_Vaild,'')", item)
        s.References = AuditSalesQuantity.Rows(cn, tx, "SELECT U_ID,U_Cargo,(SELECT COUNT(*) FROM dbo.Stores WITH(HOLDLOCK) WHERE ST_ID=@P1) AS StoreCount,(SELECT ISNULL(SUM(Value),0) FROM dbo.Pch_Exp_Values WITH(HOLDLOCK) WHERE Pch_T_ID=@P2 AND isWithBill=1) AS Expenses FROM dbo.Units WITH(HOLDLOCK) WHERE U_ID=@P0", unit, store, bill)
        If s.Item.Rows.Count <> 1 OrElse s.References.Rows.Count <> 1 OrElse CInt(s.References.Rows(0)("StoreCount")) <> 1 OrElse IsDBNull(s.References.Rows(0)("U_Cargo")) OrElse CDec(s.References.Rows(0)("U_Cargo")) <= 0D Then Throw New InvalidOperationException("مرجع الصنف أو المخزن أو الوحدة/سعتها غير صالح.")
        If Not s.Units.Rows.Cast(Of DataRow)().Any(Function(x) CInt(x("U_ID")) = unit) OrElse s.Units.Rows.Cast(Of DataRow)().Any(Function(x) IsDBNull(x("AuditUnitCargo")) OrElse CDbl(x("AuditUnitCargo")) <= 0R) Then Throw New InvalidOperationException("روابط وحدات الصنف أو سعاتها غير صالحة.")
        If s.Lines.Rows.Count > 1000 OrElse s.Units.Rows.Count > 1000 OrElse s.Expiries.Rows.Count > 2000 OrElse s.Stock.Rows.Count > 2000 Then Throw New InvalidOperationException("تفاصيل إضافة الصنف تتجاوز سعة المراجعة الآمنة.")
        Return s
    End Function
    Friend Shared Function Fingerprint(s As AuditPurchaseSnapshot) As String
        Return BitConverter.ToString(AuditCanonical.Hash(Encoding.UTF8.GetBytes(String.Join("|", {s.Header, s.Lines, s.Item, s.Units, s.Expiries, s.Stock, s.References}.Select(Function(x) AuditSalesQuantity.Fingerprint(x)))))).Replace("-", "")
    End Function
    Friend Shared Sub Prepare(connection As String, r As AuditOperationRequest)
        AuditContract.Request(r)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Permission(cn, tx, r)
                r.Inputs.Single(Function(x) x.Name = "ExpectedPurchaseSnapshot").Value = AuditValue.Capture("String", Fingerprint(Capture(cn, tx, r)))
                tx.Commit()
            End Using
        End Using
    End Sub
    Private Shared Function Allocate(cn As SqlConnection, tx As SqlTransaction, table As String, count As Integer) As Integer
        Dim range = AuditSalesQuantity.Rows(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)")
        If range.Rows.Count <> 1 Then Throw New InvalidOperationException("نطاق الترقيم غير محدد.")
        Dim first = CInt(range.Rows(0)(0)), last = CInt(range.Rows(0)(1))
        Dim maximum = CLng(AuditSalesQuantity.Rows(cn, tx, "SELECT ISNULL(MAX(T_ID),@P0) FROM dbo.[" & table & "] WITH(UPDLOCK,HOLDLOCK) WHERE T_ID BETWEEN @P0 AND @P1", first, last).Rows(0)(0))
        If first < 0 OrElse last <= first OrElse maximum + count > last OrElse maximum + count > Integer.MaxValue Then Throw New InvalidOperationException("نطاق الترقيم ممتلئ.")
        Return CInt(maximum + 1)
    End Function
    Private Shared Sub Bind(cmd As SqlCommand, r As AuditOperationRequest)
        For Each definition In AuditContract.PurchaseInputs()
            Dim p = definition.Split("|"c), value = r.Inputs.Single(Function(x) x.Name = p(0)).Value
            Dim parameter = cmd.Parameters.Add("@" & p(1), If(p(3) = "date", SqlDbType.Date, If(p(2) = "Integer", SqlDbType.Int, If(p(2) = "Boolean", SqlDbType.Bit, If(p(2) = "Float", SqlDbType.Float, If(p(2) = "Decimal", SqlDbType.Decimal, SqlDbType.NVarChar))))))
            If p(2) = "String" AndAlso p(3) <> "date" Then parameter.Size = 100
            If value.State = AuditValueState.SqlNull Then
                parameter.Value = DBNull.Value
            Else
                Select Case p(2)
                    Case "Integer" : parameter.Value = Integer.Parse(value.Text, CultureInfo.InvariantCulture)
                    Case "Boolean" : parameter.Value = value.Text = "1"
                    Case "Decimal" : parameter.Value = Decimal.Parse(value.Text, CultureInfo.InvariantCulture)
                    Case "Float" : parameter.Value = Double.Parse(value.Text, CultureInfo.InvariantCulture)
                    Case "String" : parameter.Value = If(p(3) = "date", CType(DateTime.ParseExact(value.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture), Object), value.Text)
                End Select
            End If
        Next
    End Sub
    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse request.OperationCode <> Operation Then Throw New ArgumentException("يلزم عقد إضافة مشتريات/جرد ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request
            Permission(cn, tx, r)
            Dim before = Capture(cn, tx, r)
            If Fingerprint(before) <> V(r, "ExpectedPurchaseSnapshot") Then Throw New InvalidOperationException("تغيرت الفاتورة أو التكلفة أو الأسعار أو الرصيد؛ حدّث العرض قبل إضافة الصنف.")
            Dim dates = V(r, "AdditionalExpiries").Split(New Char() {";"c}, StringSplitOptions.RemoveEmptyEntries)
            Dim id = Allocate(cn, tx, "Pch_Details", 1), expiryFirst = If(dates.Length = 0, 0, Allocate(cn, tx, "Valid_Notes", dates.Length))
            Using cmd = AuditRequestCoordinator.Command("dbo.Pch_Details_Insert", cn, tx)
                Bind(cmd, r)
                cmd.Parameters.Add("@Pch_T_ID", SqlDbType.Int).Value = CInt(r.EntityId)
                Dim output = cmd.Parameters.Add("@T_ID", SqlDbType.Int) : output.Direction = ParameterDirection.Output
                cmd.ExecuteNonQuery()
                AuditRequestCoordinator.RequireTransaction(cn, tx)
                If IsDBNull(output.Value) OrElse CInt(output.Value) <> id Then Throw New InvalidOperationException("رقم سطر المشتريات الناتج غير مطابق.")
            End Using
            For Each d In dates
                Using cmd = AuditRequestCoordinator.Command("dbo.Valid_Notes_Insert", cn, tx)
                    cmd.Parameters.Add("@Pch_T_ID", SqlDbType.Int).Value = id
                    cmd.Parameters.Add("@IM_ID", SqlDbType.Int).Value = CInt(V(r, "SelectedItemId"))
                    cmd.Parameters.Add("@VALID_DATE", SqlDbType.DateTime).Value = DateTime.ParseExact(d, "yyyy-MM-ddTHH:mm:ss.fffffff", CultureInfo.InvariantCulture)
                    cmd.ExecuteNonQuery()
                End Using
                AuditRequestCoordinator.RequireTransaction(cn, tx)
            Next
            If V(r, "OnUpdate") = "1" Then
                Using cmd = AuditRequestCoordinator.Command("dbo.AG_Balance_DependingUpdatedBill", cn, tx)
                    cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = CInt(r.EntityId)
                    cmd.ExecuteNonQuery()
                End Using
                AuditRequestCoordinator.RequireTransaction(cn, tx)
            End If
            Dim after = Capture(cn, tx, r), line = after.Lines.Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CLng(x("T_ID")) = id)
            If line Is Nothing OrElse after.Lines.Rows.Count <> before.Lines.Rows.Count + 1 OrElse CInt(line("IM_ID")) <> CInt(V(r, "SelectedItemId")) OrElse CInt(line("U_ID")) <> CInt(V(r, "SelectedUnitId")) OrElse CInt(line("ST_ID")) <> CInt(V(r, "SelectedStoreId")) Then Throw New InvalidOperationException("لم يؤكد الإجراء إضافة السطر المطلوب.")
            Dim notes = after.Expiries.Rows.Cast(Of DataRow)().Where(Function(x) CInt(x("Pch_T_ID")) = id).OrderBy(Function(x) CInt(x("T_ID"))).ToArray()
            If notes.Length <> dates.Length Then Throw New InvalidOperationException("تواريخ الصلاحية لم تحفظ كاملة.")
            For n = 0 To notes.Length - 1
                If CInt(notes(n)("T_ID")) <> expiryFirst + n OrElse CInt(notes(n)("IM_ID")) <> CInt(V(r, "SelectedItemId")) OrElse CDate(notes(n)("VALID_DATE")).Date <> DateTime.ParseExact(dates(n), "yyyy-MM-ddTHH:mm:ss.fffffff", CultureInfo.InvariantCulture).Date Then Throw New InvalidOperationException("تواريخ الصلاحية المحفوظة غير مطابقة.")
            Next
            Dim occurred = AuditRequestCoordinator.ServerUtcNow(cn, tx), number = Convert.ToString(after.Header.Rows(0)("AuditDocumentNumber"), CultureInfo.InvariantCulture)
            Dim part As Func(Of Integer, AuditEventRequest) = Function(n) New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = n, .EventCode = Operation, .Outcome = 1, .EntityType = r.EntityType, .EntityId = r.EntityId, .DocumentNumber = number, .BusinessDate = CDate(after.Header.Rows(0)("Date")), .OccurredAtUtc = occurred}
            Dim events As New List(Of AuditEventRequest) From {part(1)}
            Append(events(0), "PurchaseDocument", r.EntityId, before.Header.Rows(0), after.Header.Rows(0))
            Append(events(0), "PurchaseLine", id.ToString(CultureInfo.InvariantCulture), Nothing, line)
            Dim detail As AuditEventRequest = Nothing
            Dim add As Action(Of String, String, DataRow, DataRow) = Sub(entity, key, a, b)
                If detail Is Nothing OrElse detail.Changes.Count + AuditContract.PurchaseFields(entity).Length > 100 Then
                    detail = part(events.Count + 1) : events.Add(detail)
                End If
                Append(detail, entity, key, a, b)
            End Sub
            For Each row As DataRow In before.Lines.Rows
                Dim current = after.Lines.Rows.Cast(Of DataRow)().Single(Function(x) CLng(x("T_ID")) = CLng(row("T_ID")))
                If AuditSalesLineEdit.Fingerprint(row) <> AuditSalesLineEdit.Fingerprint(current) Then add("PurchaseLine", CStr(row("T_ID")), row, current)
            Next
            add("PurchaseItem", V(r, "SelectedItemId"), before.Item.Rows(0), after.Item.Rows(0))
            If before.Units.Rows.Count <> after.Units.Rows.Count Then Throw New InvalidOperationException("تغير عدد وحدات الصنف بصورة غير متوقعة.")
            For Each row As DataRow In before.Units.Rows
                add("PurchaseUnit", CStr(row("U_IM_ID")), row, after.Units.Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("U_IM_ID")) = CInt(row("U_IM_ID"))))
            Next
            For Each note In notes
                add("PurchaseExpiry", CStr(note("T_ID")), Nothing, note)
            Next
            Dim priorNotes = after.Expiries.Copy()
            For Each row In priorNotes.Rows.Cast(Of DataRow)().Where(Function(x) CInt(x("Pch_T_ID")) = id).ToArray()
                priorNotes.Rows.Remove(row)
            Next
            If AuditSalesQuantity.Fingerprint(before.Expiries) <> AuditSalesQuantity.Fingerprint(priorNotes) Then Throw New InvalidOperationException("تغيرت تواريخ صلاحية سابقة خارج العملية.")
            Dim buckets As New SortedDictionary(Of String, DataRow())(StringComparer.Ordinal)
            For side = 0 To 1
                For Each row As DataRow In If(side = 0, before.Stock, after.Stock).Rows
                    Dim key = BitConverter.ToString(AuditCanonical.Hash(Encoding.UTF8.GetBytes(CStr(row("IM_ID")) & "|" & CStr(row("ST_ID")) & "|" & CStr(row("D_Vaild"))))).Replace("-", "")
                    If Not buckets.ContainsKey(key) Then buckets.Add(key, New DataRow(1) {})
                    buckets(key)(side) = row
                Next
            Next
            For Each pair In buckets
                For side = 0 To 1
                    If pair.Value(side) Is Nothing Then
                        Dim dt = before.Stock.Clone(), row = dt.NewRow()
                        row.ItemArray = CType(pair.Value(1 - side).ItemArray.Clone(), Object()) : row("AuditStock") = 0R
                        dt.Rows.Add(row) : pair.Value(side) = row
                    End If
                Next
                add("StockBucket", pair.Key, pair.Value(0), pair.Value(1))
            Next
            If events.Count > 100 Then Throw New InvalidOperationException("تفاصيل الإضافة تتجاوز سعة المراقبة؛ لم تحفظ العملية.")
            For Each e In events
                AuditContract.EventData(r, e)
            Next
            For Each e In events
                AuditWriter.WriteInTransaction(cn, tx, lease, e)
            Next
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", r.EntityId}, {"LineId", id.ToString(CultureInfo.InvariantCulture)}})}
        Catch
            lease.Faulted = True : Throw
        End Try
    End Function
    Private Shared Sub Append(e As AuditEventRequest, entity As String, key As String, before As DataRow, after As DataRow)
        For Each definition In AuditContract.PurchaseFields(entity)
            Dim p = definition.Split("|"c)
            e.Changes.Add(New AuditFieldChange With {.ChangeNo = e.Changes.Count + 1, .EntityType = entity, .EntityId = key, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = CByte(If(entity = "StockBucket", 3, 1)), .Before = If(before Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), before(p(1)))), .After = AuditValue.Capture(p(2), after(p(1)))})
        Next
    End Sub
    Friend Shared Function Save(connection As String, r As AuditOperationRequest) As AuditRequestResult
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Dim result = ExecuteInTransaction(cn, tx, r)
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
