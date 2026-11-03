Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

Friend NotInheritable Class AuditTableSplitSnapshot
    Friend Rows As New Dictionary(Of String, DataTable)(StringComparer.Ordinal)
    Friend Allocation As DataTable
    Friend Units As DataTable
    Friend Components As DataTable
    Friend Structures As DataTable
End Class

' Each click commits immediately, with its own immutable request. Confirmation is not the first audit point.
Friend NotInheritable Class AuditTableSplit
    Friend Shared Function V(r As AuditOperationRequest, name As String) As String
        Return AuditContract.TableSplitValue(r, name)
    End Function
    Friend Shared Function Request(context As AuditEventContext, operation As String, source As Integer, destination As Integer, bill As Integer, values As IDictionary(Of String, Object)) As AuditOperationRequest
        Dim r As New AuditOperationRequest With {.Context = context.Copy(), .RequestId = Guid.NewGuid(), .OperationCode = operation, .EntityType = "TableSplit", .EntityId = destination.ToString(CultureInfo.InvariantCulture)}
        Dim inputs As New Dictionary(Of String, Object)(values) From {{"SourceTableId", source}, {"TargetBillId", bill}, {"ExpectedTableSplit", New String("0"c, 64)}}
        For Each definition In AuditContract.TableSplitInputs(operation)
            Dim p = definition.Split("|"c), value As Object = Nothing
            If Not inputs.TryGetValue(p(0), value) Then Throw New ArgumentException("مدخل نقل طاولة مفقود.")
            r.Inputs.Add(New AuditInput With {.Name = p(0), .ValueType = p(1), .Value = AuditValue.Capture(p(1), value)})
        Next
        If inputs.Count <> r.Inputs.Count Then Throw New ArgumentException("مدخل زائد في نقل الطاولة.")
        AuditContract.Request(r) : Return r
    End Function
    Private Shared Function Cols(entity As String, prefix As String) As String
        Return String.Join(",", AuditContract.TableSplitFields(entity).Where(Function(x) Not {"AuditDocumentNumber", "AuditAccountName"}.Contains(x.Split("|"c)(1))).Select(Function(x) prefix & "[" & x.Split("|"c)(1) & "]"))
    End Function
    Private Shared Function Empty(entity As String, key As String) As DataTable
        Dim dt As New DataTable : dt.Columns.Add(key, If(key = "AuditKey", GetType(String), GetType(Integer)))
        For Each d In AuditContract.TableSplitFields(entity)
            Dim p = d.Split("|"c), type As Type = Nothing
            Select Case p(2)
                Case "Integer" : type = GetType(Long)
                Case "Decimal" : type = GetType(Decimal)
                Case "Float" : type = GetType(Double)
                Case "String" : type = GetType(String)
                Case "Boolean" : type = GetType(Boolean)
                Case "DateTime" : type = GetType(DateTime)
            End Select
            dt.Columns.Add(p(1), type)
        Next
        Return dt
    End Function
    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest) As AuditTableSplitSnapshot
        Dim s As New AuditTableSplitSnapshot, source = CInt(V(r, "SourceTableId")), target = CInt(r.EntityId), bill = CInt(V(r, "TargetBillId")), starting = r.OperationCode = "TableSplit.Started"
        s.Rows.Add("SplitTable", AuditSalesQuantity.Rows(cn, tx, "SELECT TB_ID," & Cols("SplitTable", "") & " FROM dbo.[Tables] WITH(UPDLOCK,HOLDLOCK) WHERE TB_ID IN(@P0,@P1) ORDER BY TB_ID", source, target))
        Dim scope = "(b.Table_ID IN(@P0,@P1) AND b.BsType_ID=1 AND b.isVoid=0 AND b.isPied=0) OR b.T_ID=@P2"
        If starting Then scope &= " OR (b.BsType_ID=1 AND b.isDepended=0 AND b.isVoid=0 AND (b.User_ID=@P3 OR b.Pr_ID=@P4))"
        Dim period = If(starting, CInt(V(r, "PeriodId")), 0)
        s.Rows.Add("SplitHeader", AuditSalesQuantity.Rows(cn, tx, "SELECT b.T_ID,b.SB_ID AS AuditDocumentNumber,a.Ag_name AS AuditAccountName," & Cols("SplitHeader", "b.") & " FROM dbo.Agents_Balance_MV b WITH(UPDLOCK,HOLDLOCK) LEFT JOIN dbo.Agents a WITH(HOLDLOCK) ON a.AG_ID=b.AG_ID WHERE " & scope & " ORDER BY b.T_ID", source, target, bill, r.Context.ActorUserId.Value, period))
        Dim ids = s.Rows("SplitHeader").Rows.Cast(Of DataRow)().Select(Function(x) CInt(x("T_ID")).ToString(CultureInfo.InvariantCulture)).ToArray(), idList = If(ids.Length = 0, "0", String.Join(",", ids))
        s.Rows.Add("SplitLine", AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID," & Cols("SplitLine", "") & " FROM dbo.SB_Contents WITH(UPDLOCK,HOLDLOCK) WHERE Bill_T_ID IN(" & idList & ") ORDER BY T_ID"))
        If s.Rows("SplitHeader").Rows.Count > 500 OrElse s.Rows("SplitLine").Rows.Count > 2000 Then Throw New InvalidOperationException("نطاق الطاولتين يتجاوز حد المراجعة الآمنة.")
        If Not starting Then
            Dim header = s.Rows("SplitHeader").Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = bill)
            If header IsNot Nothing AndAlso Not IsDBNull(header("Pr_ID")) Then period = CInt(header("Pr_ID"))
        End If
        s.Rows.Add("SplitPeriod", AuditSalesQuantity.Rows(cn, tx, "SELECT Pr_ID," & Cols("SplitPeriod", "") & " FROM dbo.Periods WITH(UPDLOCK,HOLDLOCK) WHERE Pr_ID=@P0", period))
        s.Rows.Add("SplitPrint", AuditSalesQuantity.Rows(cn, tx, "SELECT " & Cols("SplitPrint", "") & " FROM dbo.TB_AutoPrint_tmp WITH(UPDLOCK,HOLDLOCK) ORDER BY T_ID,[DATE],Printer_IP,Notes"))
        s.Rows.Add("SplitStock", Empty("SplitStock", "AuditKey")) : s.Rows.Add("SplitBalance", Empty("SplitBalance", "T_ID"))
        Dim keys As New HashSet(Of String)(StringComparer.Ordinal), physical As New HashSet(Of Integer)
        For Each line As DataRow In s.Rows("SplitLine").Rows
            Dim keyValues As New DataTable : keyValues.Columns.Add("IM_ID", GetType(Integer)) : keyValues.Columns.Add("ST_ID", GetType(Long)) : keyValues.Columns.Add("D_Vaild", GetType(String))
            Dim values = keyValues.Rows.Add(CInt(line("IM_ID")), CLng(line("ST_ID")), If(IsDBNull(line("D_Vaild")), "", CStr(line("D_Vaild")))), key = AuditSalesLineEdit.Fingerprint(values)
            If Not keys.Add(key) Then Continue For
            s.Rows("SplitStock").Rows.Add(key, values("IM_ID"), values("ST_ID"), values("D_Vaild"), AuditSalesLineEdit.Stock(cn, tx, values))
            Dim rows = AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID," & Cols("SplitBalance", "") & " FROM dbo.ST_Balance WITH(UPDLOCK,HOLDLOCK) WHERE IM_ID=@P0 AND ST_ID=@P1 ORDER BY T_ID", CInt(line("IM_ID")), CInt(line("ST_ID")))
            For Each row As DataRow In rows.Rows
                If physical.Add(CInt(row("T_ID"))) Then s.Rows("SplitBalance").Rows.Add(row.ItemArray)
            Next
        Next
        s.Rows("SplitStock") = Sorted(s.Rows("SplitStock"), "AuditKey") : s.Rows("SplitBalance") = Sorted(s.Rows("SplitBalance"), "T_ID")
        s.Components = AuditSalesQuantity.Rows(cn, tx, "SELECT c.* FROM dbo.SB_IM_Components c WITH(HOLDLOCK) WHERE c.IM_T_ID IN(SELECT T_ID FROM dbo.SB_Contents WITH(HOLDLOCK) WHERE Bill_T_ID IN(" & idList & ")) ORDER BY c.T_ID")
        s.Structures = AuditSalesQuantity.Rows(cn, tx, "SELECT c.* FROM dbo.SB_Contents_Structers c WITH(HOLDLOCK) WHERE c.SB_Cont_T_ID IN(SELECT T_ID FROM dbo.SB_Contents WITH(HOLDLOCK) WHERE Bill_T_ID IN(" & idList & ")) ORDER BY c.T_ID")
        Dim lineId = If(r.Inputs.Any(Function(x) x.Name = "TargetLineId"), CInt(V(r, "TargetLineId")), 0)
        s.Units = AuditSalesQuantity.Rows(cn, tx, "SELECT u.U_IM_ID,u.IM_ID,u.U_ID,u.U_Cargo,u.Cost,u.Price FROM dbo.IM_Menu_Units_V u WITH(HOLDLOCK) WHERE u.IM_ID IN(SELECT IM_ID FROM dbo.SB_Contents WITH(HOLDLOCK) WHERE T_ID=@P0) ORDER BY u.U_IM_ID", lineId)
        ' No credentials are read from SERVERS. Its range and allocator inputs are locked, not modified.
        s.Allocation = AuditSalesQuantity.Rows(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)")
        If starting OrElse r.OperationCode = "TableSplit.UnitMoved" Then
            AuditSalesQuantity.Rows(cn, tx, "SELECT MAX(T_ID) AS HeaderMax,MAX(SB_ID) AS SaleMax,MAX(S_Bill_Pr_ID) AS DailyMax FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK)")
            AuditSalesQuantity.Rows(cn, tx, "SELECT MAX(T_ID) AS LineMax FROM dbo.SB_Contents WITH(UPDLOCK,HOLDLOCK)")
        End If
        Return s
    End Function
    Private Shared Function Sorted(dt As DataTable, key As String) As DataTable
        dt.DefaultView.Sort = key : Return dt.DefaultView.ToTable()
    End Function
    Friend Shared Function Fingerprint(s As AuditTableSplitSnapshot) As String
        Dim stockValues = s.Rows("SplitStock").Rows.Cast(Of DataRow)().Select(Function(row) AuditValue.Capture("Float", row("AuditStock")).Text)
        Return BitConverter.ToString(AuditCanonical.Hash(AuditCanonical.Encode(AuditContract.TableSplitEntities().Select(Function(x) AuditSalesQuantity.Fingerprint(s.Rows(x))).Concat({AuditSalesQuantity.Fingerprint(s.Allocation), AuditSalesQuantity.Fingerprint(s.Units), AuditSalesQuantity.Fingerprint(s.Components), AuditSalesQuantity.Fingerprint(s.Structures)}).Concat(stockValues)))).Replace("-", "")
    End Function
    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, s As AuditTableSplitSnapshot)
        If s.Rows("SplitTable").Rows.Count <> 2 Then Throw New InvalidOperationException("إحدى الطاولتين غير موجودة.")
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@User AND u.is_Allow=1 AND v.Sales=1 AND v.Transfer_Table=1", cn, tx)
            cmd.Parameters.Add("@User", SqlDbType.Int).Value = r.Context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("لا تملك صلاحية نقل أصناف الطاولة.")
        End Using
        If s.Allocation.Rows.Count <> 1 OrElse IsDBNull(s.Allocation.Rows(0)("START_ID")) OrElse IsDBNull(s.Allocation.Rows(0)("END_ID")) OrElse CInt(s.Allocation.Rows(0)("START_ID")) >= CInt(s.Allocation.Rows(0)("END_ID")) Then Throw New InvalidOperationException("نطاق ترقيم الفرع غير محدد بصورة منفردة.")
        If r.OperationCode = "TableSplit.Started" Then
            If AuditSalesQuantity.Rows(cn, tx, "SELECT AG_ID FROM dbo.Agents WITH(HOLDLOCK) WHERE AG_ID=@P0", CInt(V(r, "DefaultAccountId"))).Rows.Count <> 1 Then Throw New InvalidOperationException("الحساب الافتراضي غير موجود.")
            If CInt(V(r, "PeriodId")) > 1 AndAlso (s.Rows("SplitPeriod").Rows.Count <> 1 OrElse IsDBNull(s.Rows("SplitPeriod").Rows(0)("Pr_isOpen")) OrElse Not CBool(s.Rows("SplitPeriod").Rows(0)("Pr_isOpen")) OrElse CLng(s.Rows("SplitPeriod").Rows(0)("User_ID")) <> r.Context.ActorUserId.Value) Then Throw New InvalidOperationException("وردية النقل ليست مفتوحة للمستخدم الحالي.")
            Return
        End If
        Dim bill = CInt(V(r, "TargetBillId")), header = s.Rows("SplitHeader").Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = bill)
        If header Is Nothing OrElse CInt(header("BsType_ID")) <> 1 OrElse CInt(header("isVoid")) <> 0 OrElse CInt(header("isDepended")) <> 0 OrElse CInt(header("Table_ID")) <> CInt(r.EntityId) OrElse CInt(header("User_ID")) <> r.Context.ActorUserId.Value Then Throw New InvalidOperationException("فاتورة النقل ليست مفتوحة للمستخدم والطاولة المحددين.")
        If r.OperationCode = "TableSplit.Confirmed" Then
            If Not s.Rows("SplitLine").Rows.Cast(Of DataRow)().Any(Function(x) CInt(x("Bill_T_ID")) = bill) Then Throw New InvalidOperationException("فاتورة النقل فارغة.")
            Return
        End If
        Dim line = s.Rows("SplitLine").Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = CInt(V(r, "TargetLineId")))
        If line Is Nothing OrElse CDec(line("QTY")) <> Decimal.Parse(V(r, "DisplayedQuantity"), CultureInfo.InvariantCulture) Then Throw New InvalidOperationException("تغير الصنف أو كميته؛ حدث العرض.")
        Dim sourceBill = If(r.OperationCode = "TableSplit.UnitMoved", CInt(line("Bill_T_ID")), If(IsDBNull(line("Apart_From")), 0, CInt(line("Apart_From"))))
        Dim sourceHeader = s.Rows("SplitHeader").Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = sourceBill)
        If sourceHeader Is Nothing OrElse CInt(sourceHeader("Table_ID")) <> CInt(V(r, "SourceTableId")) OrElse CInt(sourceHeader("BsType_ID")) <> 1 OrElse CInt(sourceHeader("isVoid")) <> 0 OrElse CInt(sourceHeader("isPied")) <> 0 OrElse sourceBill = bill Then Throw New InvalidOperationException("مصدر الصنف لا يطابق الطاولة المصدر.")
        If r.OperationCode = "TableSplit.UnitReturned" AndAlso CInt(line("Bill_T_ID")) <> bill Then Throw New InvalidOperationException("الصنف ليس من فاتورة النقل الحالية.")
        If r.OperationCode = "TableSplit.UnitMoved" AndAlso CDec(line("QTY")) > 1D AndAlso s.Units.Rows.Cast(Of DataRow)().Count(Function(x) Not IsDBNull(x("U_Cargo")) AndAlso CDec(x("U_Cargo")) = 1D) <> 1 Then Throw New InvalidOperationException("يلزم رابط وحدة أساسية واحد لحساب تكلفة الوحدة المنقولة.")
    End Sub
    Friend Shared Sub Prepare(connection As String, r As AuditOperationRequest)
        AuditContract.Request(r)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Dim before = Capture(cn, tx, r) : Permission(cn, tx, r, before)
                r.Inputs.Single(Function(x) x.Name = "ExpectedTableSplit").Value = AuditValue.Capture("String", Fingerprint(before))
                tx.Commit()
            End Using
        End Using
    End Sub
    Private Shared Sub SP(cn As SqlConnection, tx As SqlTransaction, name As String, values As IDictionary(Of String, Object))
        Using cmd = AuditRequestCoordinator.Command("dbo." & name, cn, tx)
            For Each p In values
                cmd.Parameters.AddWithValue("@" & p.Key, p.Value)
            Next
            cmd.ExecuteNonQuery()
        End Using
        AuditRequestCoordinator.RequireTransaction(cn, tx)
    End Sub
    Private Shared Function Mutate(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, before As AuditTableSplitSnapshot) As Integer
        Dim bill = CInt(V(r, "TargetBillId")), target = CInt(r.EntityId)
        Select Case r.OperationCode
            Case "TableSplit.Started"
                Using cmd = AuditRequestCoordinator.Command("dbo.Call_New_SalesBill", cn, tx)
                    For Each p In New Dictionary(Of String, Object) From {{"AG_ID", CInt(V(r, "DefaultAccountId"))}, {"SB_Type", CInt(V(r, "SaleType"))}, {"Pr_ID", CInt(V(r, "PeriodId"))}, {"isPied", 0}, {"User_ID", r.Context.ActorUserId.Value}}
                        cmd.Parameters.AddWithValue("@" & p.Key, p.Value)
                    Next
                    For Each name In {"T_ID", "Bill_Num", "SB_ID", "isNew"}
                        cmd.Parameters.Add("@" & name, SqlDbType.Int).Direction = ParameterDirection.Output
                    Next
                    cmd.ExecuteNonQuery() : bill = CInt(cmd.Parameters("@T_ID").Value)
                End Using
                AuditRequestCoordinator.RequireTransaction(cn, tx)
                If bill <= CInt(before.Allocation.Rows(0)("START_ID")) OrElse bill > CInt(before.Allocation.Rows(0)("END_ID")) Then Throw New InvalidOperationException("فاتورة النقل خارج نطاق ترقيم الفرع.")
                Dim candidate = AuditSalesQuantity.Rows(cn, tx, "SELECT User_ID,isDepended,isVoid,BsType_ID FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@P0", bill)
                If candidate.Rows.Count <> 1 OrElse CInt(candidate.Rows(0)("User_ID")) <> r.Context.ActorUserId.Value OrElse CInt(candidate.Rows(0)("isDepended")) <> 0 OrElse CInt(candidate.Rows(0)("isVoid")) <> 0 OrElse CInt(candidate.Rows(0)("BsType_ID")) <> 1 Then Throw New InvalidOperationException("لم تنشأ فاتورة نقل صالحة للمستخدم.")
                If AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID FROM dbo.SB_Contents WITH(UPDLOCK,HOLDLOCK) WHERE Bill_T_ID=@P0", bill).Rows.Count > 0 Then Throw New InvalidOperationException("يجب تسوية أصناف الفاتورة المفتوحة أولًا قبل النقل.")
                SP(cn, tx, "AG_Balance_Update_Table", New Dictionary(Of String, Object) From {{"T_ID", bill}, {"TB_ID", target}})
            Case "TableSplit.UnitMoved"
                Dim line = before.Rows("SplitLine").Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("T_ID")) = CInt(V(r, "TargetLineId")))
                SP(cn, tx, "TB_Contents_INSERT_Table_Apart", New Dictionary(Of String, Object) From {{"SB_T_ID_From", CInt(line("Bill_T_ID"))}, {"SB_T_ID_To", bill}, {"T_ID", CInt(line("T_ID"))}})
            Case "TableSplit.UnitReturned"
                SP(cn, tx, "TB_Contents_Delete_IM_Apart", New Dictionary(Of String, Object) From {{"T_ID", CInt(V(r, "TargetLineId"))}})
            Case "TableSplit.Confirmed"
                SP(cn, tx, "AG_Balance_Update_TB_Ordered", New Dictionary(Of String, Object) From {{"T_ID", bill}, {"TB_ID", target}})
                SP(cn, tx, "Tables_Check_isBusy", New Dictionary(Of String, Object) From {{"TB_ID", CInt(V(r, "SourceTableId"))}})
        End Select
        Return bill
    End Function
    Private Shared Function Key(entity As String) As String
        Select Case entity
            Case "SplitTable" : Return "TB_ID"
            Case "SplitPeriod" : Return "Pr_ID"
            Case "SplitStock" : Return "AuditKey"
            Case Else : Return "T_ID"
        End Select
    End Function
    Friend Shared Function Events(r As AuditOperationRequest, before As AuditTableSplitSnapshot, after As AuditTableSplitSnapshot, bill As Integer, utc As DateTime) As List(Of AuditEventRequest)
        If AuditSalesQuantity.Fingerprint(before.Components) <> AuditSalesQuantity.Fingerprint(after.Components) OrElse AuditSalesQuantity.Fingerprint(before.Structures) <> AuditSalesQuantity.Fingerprint(after.Structures) OrElse AuditSalesQuantity.Fingerprint(before.Units) <> AuditSalesQuantity.Fingerprint(after.Units) Then Throw New InvalidOperationException("تغيرت المكونات أو الوحدات خارج عملية نقل الطاولة.")
        Dim h = after.Rows("SplitHeader").Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = bill)
        If h Is Nothing OrElse CInt(h("Table_ID")) <> CInt(r.EntityId) Then Throw New InvalidOperationException("فاتورة الوجهة غير مطابقة.")
        Dim result As New List(Of AuditEventRequest), part As Func(Of AuditEventRequest) = Function() New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = result.Count + 1, .EventCode = r.OperationCode, .Outcome = 1, .EntityType = r.EntityType, .EntityId = r.EntityId, .DocumentNumber = Convert.ToString(h("AuditDocumentNumber"), CultureInfo.InvariantCulture), .BusinessDate = CDate(h("Date")), .OccurredAtUtc = utc}
        result.Add(part())
        For Each entity In AuditContract.TableSplitEntities().Where(Function(x) x <> "SplitPrint")
            Dim keyName = Key(entity), olds = before.Rows(entity).Rows.Cast(Of DataRow)().ToDictionary(Function(x) Convert.ToString(x(keyName), CultureInfo.InvariantCulture)), news = after.Rows(entity).Rows.Cast(Of DataRow)().ToDictionary(Function(x) Convert.ToString(x(keyName), CultureInfo.InvariantCulture))
            If olds.Keys.Any(Function(x) Not news.ContainsKey(x)) Then Throw New InvalidOperationException("حذف صف خارج نطاق نقل أصناف الطاولة.")
            For Each id In news.Keys.OrderBy(Function(x) x, StringComparer.Ordinal)
                Dim oldRow = If(olds.ContainsKey(id), olds(id), Nothing), newRow = news(id)
                If entity <> "SplitTable" AndAlso oldRow IsNot Nothing AndAlso AuditSalesLineEdit.Fingerprint(oldRow) = AuditSalesLineEdit.Fingerprint(newRow) Then
                    ' The reusable line fingerprint deliberately excludes AuditStock; compare that derived value explicitly.
                    If entity <> "SplitStock" OrElse CDbl(oldRow("AuditStock")) = CDbl(newRow("AuditStock")) Then Continue For
                End If
                If entity <> "SplitTable" AndAlso (result.Count = 1 OrElse result.Last().Changes.Count + AuditContract.TableSplitFields(entity).Length > 100) Then result.Add(part())
                Append(result.Last(), entity, id, oldRow, newRow)
            Next
        Next
        Dim n = 0
        For Each row In AuditTables.AddedQueue(before.Rows("SplitPrint"), after.Rows("SplitPrint"))
            If result.Count = 1 OrElse result.Last().Changes.Count + 4 > 100 Then result.Add(part())
            n += 1 : Append(result.Last(), "SplitPrint", "Q" & n.ToString(CultureInfo.InvariantCulture), Nothing, row)
        Next
        If result.Count > 100 Then Throw New InvalidOperationException("تفاصيل نقل الطاولة تتجاوز حد المراقبة؛ ألغيت المعاملة.")
        ValidateResult(r, before, after, bill)
        For Each e In result
            AuditContract.EventData(r, e)
        Next
        Return result
    End Function
    Private Shared Sub Append(e As AuditEventRequest, entity As String, id As String, before As DataRow, after As DataRow)
        For Each definition In AuditContract.TableSplitFields(entity)
            Dim p = definition.Split("|"c)
            e.Changes.Add(New AuditFieldChange With {.ChangeNo = e.Changes.Count + 1, .EntityType = entity, .EntityId = id, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = CByte(If(entity = "SplitStock", 3, 1)), .Before = If(before Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), before(p(1)))), .After = AuditValue.Capture(p(2), after(p(1)))})
        Next
    End Sub
    Private Shared Sub ValidateResult(r As AuditOperationRequest, before As AuditTableSplitSnapshot, after As AuditTableSplitSnapshot, bill As Integer)
        Dim op = r.OperationCode
        If op = "TableSplit.Started" Then
            If after.Rows("SplitLine").Rows.Cast(Of DataRow)().Any(Function(x) CInt(x("Bill_T_ID")) = bill) Then Throw New InvalidOperationException("فاتورة النقل الجديدة ليست فارغة.")
            Return
        End If
        If op = "TableSplit.Confirmed" Then
            Dim header = after.Rows("SplitHeader").Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("T_ID")) = bill)
            If CInt(header("isDepended")) <> 1 OrElse CInt(header("TB_isOrderd")) <> 1 Then Throw New InvalidOperationException("لم يؤكد الإجراء نقل الطاولة.")
            Return
        End If
        Dim old = before.Rows("SplitLine").Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("T_ID")) = CInt(V(r, "TargetLineId"))), current = after.Rows("SplitLine").Rows.Cast(Of DataRow)().Single(Function(x) CInt(x("T_ID")) = CInt(old("T_ID")))
        If op = "TableSplit.UnitReturned" Then
            If IsDBNull(old("Apart_From")) OrElse CInt(current("Bill_T_ID")) <> CInt(old("Apart_From")) OrElse Not IsDBNull(current("Apart_From")) Then Throw New InvalidOperationException("لم يرجع الصنف إلى فاتورته الأصلية.")
        ElseIf CDec(old("QTY")) = 1D Then
            If CInt(current("Bill_T_ID")) <> bill OrElse CInt(current("Apart_From")) <> CInt(old("Bill_T_ID")) Then Throw New InvalidOperationException("لم ينقل الصنف إلى الفاتورة الهدف.")
        Else
            Dim oldIds = New HashSet(Of Integer)(before.Rows("SplitLine").Rows.Cast(Of DataRow)().Select(Function(x) CInt(x("T_ID"))))
            Dim added = after.Rows("SplitLine").Rows.Cast(Of DataRow)().Where(Function(x) Not oldIds.Contains(CInt(x("T_ID")))).ToArray()
            If added.Length <> 1 OrElse CDec(current("QTY")) <> CDec(old("QTY")) - 1D OrElse CDec(added(0)("QTY")) <> 1D OrElse CInt(added(0)("Bill_T_ID")) <> bill OrElse CInt(added(0)("Apart_From")) <> CInt(old("Bill_T_ID")) OrElse CInt(added(0)("IM_ID")) <> CInt(old("IM_ID")) Then Throw New InvalidOperationException("نتيجة فصل وحدة الطاولة غير مطابقة.")
        End If
    End Sub
    Friend Shared Function Save(connection As String, request As AuditOperationRequest) As AuditRequestResult
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Dim lease As AuditRequestLease = Nothing
                Try
                    lease = AuditRequestCoordinator.Begin(cn, tx, request)
                    If Not lease.MustExecute Then
                        tx.Commit()
                        Return lease.Result
                    End If
                    Dim r = lease.Request, before = Capture(cn, tx, r)
                    Permission(cn, tx, r, before)
                    If Fingerprint(before) <> V(r, "ExpectedTableSplit") Then Throw New InvalidOperationException("تغيرت الطاولة أو أصنافها؛ حدث العرض قبل النقل.")
                    Dim bill = Mutate(cn, tx, r, before), after = Capture(cn, tx, r), events = AuditTableSplit.Events(r, before, after, bill, AuditRequestCoordinator.ServerUtcNow(cn, tx))
                    For Each e In events
                        AuditWriter.WriteInTransaction(cn, tx, lease, e)
                    Next
                    Dim result As New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", bill.ToString(CultureInfo.InvariantCulture)}})}
                    tx.Commit() : Return result
                Catch
                    If lease IsNot Nothing Then lease.Faulted = True
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
