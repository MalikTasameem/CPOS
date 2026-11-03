Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Globalization
Imports System.Linq

Friend NotInheritable Class AuditWorkflowSnapshot
    Friend Header As DataTable
    Friend References As DataTable
    Friend Currency As DataTable
    Friend QuoteLines As DataTable
    Friend CalculationLines As DataTable
    Friend PurchaseExpenses As DataTable
    Friend QuoteUnits As DataTable
    Friend SalesIsolation As DataTable
    Friend ComponentsIsolation As DataTable
End Class

Friend NotInheritable Class AuditDocumentWorkflows
    Friend Shared Function V(r As AuditOperationRequest, name As String) As String
        Return AuditContract.WorkflowValue(r, name).Text
    End Function

    Friend Shared Function Request(context As AuditEventContext, operation As String, bill As Integer, kind As Integer, editing As Boolean, values As IDictionary(Of String, Object)) As AuditOperationRequest
        Dim r As New AuditOperationRequest With {.Context = context.Copy(), .RequestId = Guid.NewGuid(), .EntityType = "WorkflowHeader", .EntityId = bill.ToString(CultureInfo.InvariantCulture), .OperationCode = operation}
        Dim inputs As New Dictionary(Of String, Object)(values) From {{"DocumentType", kind}, {"OnUpdate", editing}, {"ExpectedWorkflow", New String("0"c, 64)}}
        For Each definition In AuditContract.WorkflowInputs(operation)
            Dim p = definition.Split("|"c), value As Object = Nothing
            If Not inputs.TryGetValue(p(0), value) Then Throw New ArgumentException("مدخل دورة مستند مفقود: " & p(0))
            If value Is Nothing Then value = DBNull.Value
            If Not IsDBNull(value) Then
                Select Case p(1)
                    Case "Integer" : value = Convert.ToInt32(value, CultureInfo.CurrentCulture)
                    Case "Decimal" : value = Convert.ToDecimal(value, CultureInfo.CurrentCulture)
                    Case "DateTime" : value = DateTime.SpecifyKind(New SqlDateTime(Convert.ToDateTime(value, CultureInfo.CurrentCulture)).Value, DateTimeKind.Unspecified)
                End Select
            End If
            r.Inputs.Add(New AuditInput With {.Name = p(0), .ValueType = p(1), .Value = AuditValue.Capture(p(1), value)})
        Next
        If inputs.Count <> r.Inputs.Count Then Throw New ArgumentException("مدخل زائد في دورة المستند.")
        AuditContract.Request(r) : Return r
    End Function

    Private Shared Function Cols(entity As String, prefix As String) As String
        Return String.Join(",", AuditContract.WorkflowFields(entity).Where(Function(x) Not {"AuditDocumentNumber", "AuditAccountName", "AuditItemName"}.Contains(x.Split("|"c)(1))).Select(Function(x) prefix & "[" & x.Split("|"c)(1) & "]"))
    End Function

    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest) As AuditWorkflowSnapshot
        Dim s As New AuditWorkflowSnapshot, id = CInt(r.EntityId), kind = CInt(V(r, "DocumentType")), spec = AuditContract.DocumentFieldSpec(kind)
        s.Header = AuditSalesQuantity.Rows(cn, tx, "SELECT b.T_ID,b.[" & spec(0) & "] AS AuditDocumentNumber,a.Ag_name AS AuditAccountName," & Cols("WorkflowHeader", "b.") & " FROM dbo.Agents_Balance_MV b WITH(UPDLOCK,HOLDLOCK) LEFT JOIN dbo.Agents a WITH(HOLDLOCK) ON a.AG_ID=b.AG_ID WHERE b.T_ID=@P0", id)
        If s.Header.Rows.Count <> 1 OrElse CInt(s.Header.Rows(0)("BsType_ID")) <> kind Then Throw New InvalidOperationException("المستند غير موجود أو نوعه لا يطابق الشاشة.")
        Dim purchaseEdit = kind = 7 AndAlso r.OperationCode = "Document.EditFieldsSaved"
        s.References = AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID," & Cols("WorkflowReference", "") & " FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE " & If(purchaseEdit, "Pr_ID=@P0 AND BsType_ID IN(6,9) AND T_ID<>@P0", "1=0") & " ORDER BY T_ID", id)
        Dim currencyChange = r.OperationCode.StartsWith("Purchase.", StringComparison.Ordinal)
        s.Currency = AuditSalesQuantity.Rows(cn, tx, "SELECT Cr_ID," & Cols("WorkflowCurrency", "") & " FROM dbo.Currency WITH(UPDLOCK,HOLDLOCK) WHERE Cr_ID=@P0", If(currencyChange, CInt(V(r, "RequestedCurrency")), 0))
        s.QuoteLines = AuditSalesQuantity.Rows(cn, tx, "SELECT b.T_ID,m.item_name AS AuditItemName," & Cols("WorkflowQuoteLine", "b.") & " FROM dbo.ViewBill_Contents b WITH(UPDLOCK,HOLDLOCK) LEFT JOIN dbo.IM_Menu m WITH(HOLDLOCK) ON m.IM_ID=b.IM_ID WHERE b.Bill_T_ID=@P0 ORDER BY b.T_ID", If(kind = 16, id, 0))
        Dim details = If(kind = 1, "SB_Contents", "Pch_Details"), owner = If(kind = 1, "Bill_T_ID", "Pch_T_ID")
        s.CalculationLines = AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM dbo.[" & details & "] WITH(UPDLOCK,HOLDLOCK) WHERE [" & owner & "]=@P0 ORDER BY T_ID", If(kind = 16, 0, id))
        s.PurchaseExpenses = AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM dbo.Pch_Exp_Values WITH(UPDLOCK,HOLDLOCK) WHERE Pch_T_ID=@P0 ORDER BY T_ID", If(kind = 7, id, 0))
        s.QuoteUnits = AuditSalesQuantity.Rows(cn, tx, "SELECT u.* FROM dbo.IM_Units u WITH(HOLDLOCK) WHERE EXISTS(SELECT 1 FROM dbo.ViewBill_Contents q WITH(HOLDLOCK) WHERE q.Bill_T_ID=@P0 AND q.IM_ID=u.IM_ID AND q.U_ID=u.U_ID) ORDER BY u.U_IM_ID", If(kind = 16, id, 0))
        s.SalesIsolation = AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM dbo.SB_Contents WITH(HOLDLOCK) WHERE Bill_T_ID=@P0 OR T_ID IN(SELECT T_ID FROM dbo.ViewBill_Contents WITH(HOLDLOCK) WHERE Bill_T_ID=@P0) ORDER BY T_ID", If(kind = 16, id, 0))
        s.ComponentsIsolation = AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM dbo.SB_IM_Components WITH(HOLDLOCK) WHERE IM_T_ID IN(SELECT T_ID FROM dbo.ViewBill_Contents WITH(HOLDLOCK) WHERE Bill_T_ID=@P0) ORDER BY T_ID", If(kind = 16, id, 0))
        Return s
    End Function

    Friend Shared Function Fingerprint(s As AuditWorkflowSnapshot) As String
        Return BitConverter.ToString(AuditCanonical.Hash(AuditCanonical.Encode({AuditSalesQuantity.Fingerprint(s.Header), AuditSalesQuantity.Fingerprint(s.References), AuditSalesQuantity.Fingerprint(s.Currency), AuditSalesQuantity.Fingerprint(s.QuoteLines), AuditSalesQuantity.Fingerprint(s.CalculationLines), AuditSalesQuantity.Fingerprint(s.PurchaseExpenses), AuditSalesQuantity.Fingerprint(s.QuoteUnits), AuditSalesQuantity.Fingerprint(s.SalesIsolation), AuditSalesQuantity.Fingerprint(s.ComponentsIsolation)}))).Replace("-", "")
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, s As AuditWorkflowSnapshot)
        Dim row = s.Header.Rows(0), kind = CInt(V(r, "DocumentType")), editing = V(r, "OnUpdate") = "1"
        If CInt(row("isVoid")) <> 0 OrElse Not {0, 1}.Contains(CInt(row("isDepended"))) Then Throw New InvalidOperationException("المستند ملغى أو حالته غير معروفة.")
        If CInt(row("isDepended")) = 1 AndAlso (Not editing OrElse (kind <> 16 AndAlso (IsDBNull(row("EditOpenedBy")) OrElse IsDBNull(row("EditOpenedAt")) OrElse CInt(row("EditOpenedBy")) <> r.Context.ActorUserId.Value))) Then Throw New InvalidOperationException("يجب فتح تعديل المستند بالمستخدم الحالي.")
        Dim spec = AuditContract.DocumentFieldSpec(kind), permissionName = spec(If(editing OrElse CInt(row("isDepended")) = 1, 2, 1))
        Dim discountInput = r.Inputs.SingleOrDefault(Function(x) x.Name = "RequestedDiscount")
        Dim discountPermission = kind <> 7 AndAlso discountInput IsNot Nothing AndAlso (r.OperationCode = "Quote.DiscountDistributed" OrElse Decimal.Round(Decimal.Parse(discountInput.Value.Text, CultureInfo.InvariantCulture), 3, MidpointRounding.AwayFromZero) <> If(IsDBNull(row("Discount")), 0D, CDec(row("Discount"))))
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@User AND u.is_Allow=1 AND v.[" & spec(1) & "]=1 AND v.[" & permissionName & "]=1" & If(discountPermission, " AND v.Sales_Dis=1", ""), cn, tx)
            cmd.Parameters.Add("@User", SqlDbType.Int).Value = r.Context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("لا تملك صلاحية دورة المستند/الخصم.")
        End Using
        If r.Inputs.Any(Function(x) x.Name = "RequestedAccount") Then
            Dim accounts = AuditSalesQuantity.Rows(cn, tx, "SELECT AG_ID FROM dbo.Agents WITH(HOLDLOCK) WHERE AG_ID=@P0", CInt(V(r, "RequestedAccount")))
            If accounts.Rows.Count <> 1 Then Throw New InvalidOperationException("الحساب المطلوب غير موجود أو مكرر.")
        End If
        If r.OperationCode.StartsWith("Purchase.", StringComparison.Ordinal) AndAlso s.Currency.Rows.Count <> 1 Then Throw New InvalidOperationException("العملة المطلوبة غير موجودة.")
        If {"Quote.Confirmed", "Quote.Reserved", "Quote.DiscountDistributed"}.Contains(r.OperationCode) AndAlso s.QuoteLines.Rows.Count = 0 Then Throw New InvalidOperationException("عرض السعر لا يحتوي أصنافًا.")
        If r.OperationCode = "Quote.DiscountDistributed" Then
            For Each line As DataRow In s.QuoteLines.Rows
                Dim prices = s.QuoteUnits.Rows.Cast(Of DataRow)().Where(Function(x) CInt(x("IM_ID")) = CInt(line("IM_ID")) AndAlso CLng(x("U_ID")) = CLng(line("U_ID"))).ToArray()
                If prices.Length <> 1 OrElse IsDBNull(prices(0)("Price")) Then Throw New InvalidOperationException("يجب أن يكون لصنف العرض سعر وحدة واحد معروف قبل توزيع الخصم.")
            Next
        End If
    End Sub

    Friend Shared Sub Prepare(connection As String, r As AuditOperationRequest)
        AuditContract.Request(r)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Dim s = Capture(cn, tx, r) : Permission(cn, tx, r, s)
                r.Inputs.Single(Function(x) x.Name = "ExpectedWorkflow").Value = AuditValue.Capture("String", Fingerprint(s))
                tx.Commit()
            End Using
        End Using
    End Sub

    Private Shared Function InputObject(r As AuditOperationRequest, name As String) As Object
        Dim v = AuditContract.WorkflowValue(r, name)
        If v.State = AuditValueState.SqlNull Then Return DBNull.Value
        Select Case v.ValueType
            Case "Integer" : Return Integer.Parse(v.Text, CultureInfo.InvariantCulture)
            Case "Decimal" : Return Decimal.Parse(v.Text, CultureInfo.InvariantCulture)
            Case "DateTime" : Return DateTime.Parse(v.Text, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
            Case Else : Return v.Text
        End Select
    End Function

    Private Shared Sub SP(cn As SqlConnection, tx As SqlTransaction, name As String, values As IDictionary(Of String, Object))
        Using cmd = AuditRequestCoordinator.Command("dbo." & name, cn, tx)
            For Each value In values
                cmd.Parameters.AddWithValue("@" & value.Key, value.Value)
            Next
            cmd.ExecuteNonQuery()
        End Using
        AuditRequestCoordinator.RequireTransaction(cn, tx)
    End Sub

    Private Shared Sub Mutate(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest)
        Dim id = CInt(r.EntityId), kind = CInt(V(r, "DocumentType"))
        If {"Document.EditFieldsSaved", "Quote.EditFieldsSaved"}.Contains(r.OperationCode) Then
            SP(cn, tx, "AG_Balance_Update_Total", New Dictionary(Of String, Object) From {{"T_ID", id}, {"Total", InputObject(r, "RequestedTotal")}, {"Disc", InputObject(r, If(r.OperationCode = "Quote.EditFieldsSaved", "RequestedRecalcDiscount", "RequestedDiscount"))}})
        End If
        If {"Document.EditFieldsSaved", "Quote.EditFieldsSaved", "Document.MetadataSaved", "Quote.Confirmed"}.Contains(r.OperationCode) Then
            SP(cn, tx, "AG_Balance_Update_About", New Dictionary(Of String, Object) From {{"T_ID", id}, {"About", InputObject(r, "RequestedAbout")}})
            Dim dated = DirectCast(InputObject(r, "RequestedDate"), DateTime)
            SP(cn, tx, "AG_Balance_Update_Date", New Dictionary(Of String, Object) From {{"T_ID", id}, {"Date", dated}, {"Month", dated.Month}, {"YEAR", dated.Year}})
        End If
        If {"Document.ProjectChanged", "Document.MetadataSaved"}.Contains(r.OperationCode) OrElse (r.OperationCode = "Document.EditFieldsSaved" AndAlso kind = 1) Then
            SP(cn, tx, "AG_Balance_Update_AG_Project", New Dictionary(Of String, Object) From {{"T_ID", id}, {"Proj_ID", InputObject(r, "RequestedProject")}})
        ElseIf r.OperationCode = "Document.EditFieldsSaved" AndAlso kind = 7 Then
            SP(cn, tx, "AG_Balance_Update_ReferNum", New Dictionary(Of String, Object) From {{"T_ID", id}, {"ReferNum", InputObject(r, "RequestedReference")}})
        End If
        If {"Purchase.AccountCurrencyChanged", "Quote.Confirmed"}.Contains(r.OperationCode) Then
            SP(cn, tx, "AG_Balance_Update_AG", New Dictionary(Of String, Object) From {{"T_ID", id}, {"AG_ID", InputObject(r, "RequestedAccount")}, {"ON_UPDATE", V(r, "OnUpdate") = "1"}})
        End If
        If r.OperationCode.StartsWith("Purchase.", StringComparison.Ordinal) Then
            SP(cn, tx, "AG_Balance_Update_Equal_Value", New Dictionary(Of String, Object) From {{"T_ID", id}, {"Cr_ID", InputObject(r, "RequestedCurrency")}, {"Cr_Equal_Value", InputObject(r, "RequestedExchange")}})
        End If
        If {"Document.EditFieldsSaved", "Quote.DiscountChanged"}.Contains(r.OperationCode) Then
            Using cmd As New SqlCommand("UPDATE dbo.Agents_Balance_MV SET Discount=@Discount WHERE T_ID=@Id", cn, tx)
                cmd.Parameters.AddWithValue("@Discount", InputObject(r, "RequestedDiscount")) : cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id
                If cmd.ExecuteNonQuery() <> 1 Then Throw New InvalidOperationException("لم يتم تعديل رأس واحد.")
            End Using
        ElseIf r.OperationCode = "Quote.Confirmed" Then
            SP(cn, tx, "ViewBill_ConfermBill", New Dictionary(Of String, Object) From {{"T_ID", id}, {"TOTAL", CDbl(InputObject(r, "RequestedTotal"))}, {"Discount", CDbl(InputObject(r, "RequestedDiscount"))}, {"Pure", CDbl(InputObject(r, "RequestedPure"))}})
        ElseIf r.OperationCode = "Quote.DiscountDistributed" Then
            SP(cn, tx, "ViewBill_Discount_Distribute_Audited", New Dictionary(Of String, Object) From {{"T_ID", id}, {"Discount_Val", InputObject(r, "RequestedDiscount")}})
            ' Preserve the old final header assignment (numeric(18,3)); distribution uses numeric(18,2).
            Using cmd As New SqlCommand("UPDATE dbo.Agents_Balance_MV SET Discount=@Discount WHERE T_ID=@Id", cn, tx)
                cmd.Parameters.AddWithValue("@Discount", InputObject(r, "RequestedDiscount")) : cmd.Parameters.AddWithValue("@Id", id)
                If cmd.ExecuteNonQuery() <> 1 Then Throw New InvalidOperationException("لم يتم حفظ خصم العرض.")
            End Using
        ElseIf r.OperationCode = "Quote.Reserved" Then
            Using cmd As New SqlCommand("UPDATE dbo.ViewBill_Contents SET isDepended=1 WHERE Bill_T_ID=@Id", cn, tx)
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id
                If cmd.ExecuteNonQuery() < 1 Then Throw New InvalidOperationException("لا توجد أصناف للحجز في عرض السعر.")
            End Using
        End If
    End Sub

    Friend Shared Function Events(r As AuditOperationRequest, before As AuditWorkflowSnapshot, after As AuditWorkflowSnapshot, utc As DateTime) As List(Of AuditEventRequest)
        AuditContract.Request(r)
        If before.Header.Rows.Count <> 1 OrElse after.Header.Rows.Count <> 1 OrElse CInt(before.Header.Rows(0)("T_ID")) <> CInt(r.EntityId) OrElse CInt(after.Header.Rows(0)("T_ID")) <> CInt(r.EntityId) Then Throw New InvalidOperationException("لقطة الرأس غير مطابقة.")
        If AuditSalesQuantity.Fingerprint(before.CalculationLines) <> AuditSalesQuantity.Fingerprint(after.CalculationLines) OrElse AuditSalesQuantity.Fingerprint(before.PurchaseExpenses) <> AuditSalesQuantity.Fingerprint(after.PurchaseExpenses) Then Throw New InvalidOperationException("تغيرت تفاصيل خارج دورة الرأس.")
        If AuditSalesQuantity.Fingerprint(before.QuoteUnits) <> AuditSalesQuantity.Fingerprint(after.QuoteUnits) OrElse AuditSalesQuantity.Fingerprint(before.SalesIsolation) <> AuditSalesQuantity.Fingerprint(after.SalesIsolation) OrElse AuditSalesQuantity.Fingerprint(before.ComponentsIsolation) <> AuditSalesQuantity.Fingerprint(after.ComponentsIsolation) Then Throw New InvalidOperationException("تغيرت أصناف بيع/مكونات أو أسعار وحدات خارج دورة العرض؛ ألغيت العملية.")
        Dim header = after.Header.Rows(0), result As New List(Of AuditEventRequest)
        Dim part As Func(Of AuditEventRequest) = Function() New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = result.Count + 1, .EventCode = r.OperationCode, .Outcome = 1, .EntityType = r.EntityType, .EntityId = r.EntityId, .DocumentNumber = Convert.ToString(header("AuditDocumentNumber"), CultureInfo.InvariantCulture), .OccurredAtUtc = utc, .BusinessDate = CDate(header("Date"))}
        result.Add(part())
        Append(result(0), "WorkflowHeader", r.EntityId, before.Header.Rows(0), header)
        For Each entity In {"WorkflowReference", "WorkflowCurrency", "WorkflowQuoteLine"}
            Dim oldTable = If(entity = "WorkflowReference", before.References, If(entity = "WorkflowCurrency", before.Currency, before.QuoteLines))
            Dim newTable = If(entity = "WorkflowReference", after.References, If(entity = "WorkflowCurrency", after.Currency, after.QuoteLines))
            If oldTable.Rows.Count <> newTable.Rows.Count Then Throw New InvalidOperationException("تغير عدد صفوف دورة المستند.")
            Dim key = If(entity = "WorkflowCurrency", "Cr_ID", "T_ID")
            For Each oldRow As DataRow In oldTable.Rows
                Dim rowId = CInt(oldRow(key)), newRow = newTable.Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x(key)) = rowId)
                If newRow Is Nothing Then Throw New InvalidOperationException("صف تابع مفقود.")
                Dim changed = AuditSalesLineEdit.Fingerprint(oldRow) <> AuditSalesLineEdit.Fingerprint(newRow)
                If Not changed Then Continue For
                If result.Count = 1 OrElse result.Last().Changes.Count + AuditContract.WorkflowFields(entity).Length > 100 Then result.Add(part())
                Append(result.Last(), entity, rowId.ToString(CultureInfo.InvariantCulture), oldRow, newRow)
            Next
        Next
        If result.Count > 100 Then Throw New InvalidOperationException("عدد تفاصيل دورة المستند أكبر من سعة المراقبة؛ لم تحفظ العملية.")
        ValidateResult(r, before, after)
        For Each e In result
            AuditContract.EventData(r, e)
        Next
        Return result
    End Function

    Private Shared Sub Append(e As AuditEventRequest, entity As String, id As String, before As DataRow, after As DataRow)
        For Each definition In AuditContract.WorkflowFields(entity)
            Dim p = definition.Split("|"c)
            e.Changes.Add(New AuditFieldChange With {.ChangeNo = e.Changes.Count + 1, .EntityType = entity, .EntityId = id, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1, .Before = AuditValue.Capture(p(2), before(p(1))), .After = AuditValue.Capture(p(2), after(p(1)))})
        Next
    End Sub

    Private Shared Sub ValidateResult(r As AuditOperationRequest, before As AuditWorkflowSnapshot, after As AuditWorkflowSnapshot)
        Dim row = after.Header.Rows(0)
        For Each mapping In {"RequestedDiscount|Discount", "RequestedDate|Date", "RequestedAbout|About", "RequestedReference|ReferNum", "RequestedProject|Proj_ID", "RequestedAccount|AG_ID", "RequestedExchange|Cr_Equal_Value"}
            Dim p = mapping.Split("|"c), input = r.Inputs.SingleOrDefault(Function(x) x.Name = p(0))
            If input Is Nothing OrElse (p(0) = "RequestedReference" AndAlso V(r, "DocumentType") <> "7") OrElse (p(0) = "RequestedProject" AndAlso r.OperationCode = "Document.EditFieldsSaved" AndAlso V(r, "DocumentType") <> "1") Then Continue For
            Dim expected = InputObject(r, p(0))
            If input.ValueType = "Decimal" AndAlso Not IsDBNull(expected) Then expected = Decimal.Round(CDec(expected), 3, MidpointRounding.AwayFromZero)
            Dim actual = AuditValue.Capture(input.ValueType, row(p(1))), wanted = AuditValue.Capture(input.ValueType, expected)
            If actual.State <> wanted.State OrElse actual.Text <> wanted.Text Then Throw New InvalidOperationException("نتيجة الرأس لا تطابق المدخل: " & p(0))
        Next
        If r.OperationCode.StartsWith("Purchase.", StringComparison.Ordinal) Then
            If after.Currency.Rows.Count <> 1 OrElse IsDBNull(after.Currency.Rows(0)("Cr_Equal")) OrElse CDec(after.Currency.Rows(0)("Cr_Equal")) <> Decimal.Round(Decimal.Round(CDec(InputObject(r, "RequestedExchange")), 3, MidpointRounding.AwayFromZero), 2, MidpointRounding.AwayFromZero) Then Throw New InvalidOperationException("لم يحفظ معامل العملة المطلوب.")
        End If
        If r.OperationCode = "Document.EditFieldsSaved" AndAlso V(r, "DocumentType") = "7" Then
            For Each linked As DataRow In after.References.Rows
                If Convert.ToString(linked("ReferNum"), CultureInfo.InvariantCulture) <> V(r, "RequestedReference") Then Throw New InvalidOperationException("لم تحفظ المراجع التابعة.")
            Next
        End If
        If r.OperationCode = "Quote.Confirmed" Then
            For Each mapping In {"RequestedTotal|Total", "RequestedPure|Pure"}
                Dim p = mapping.Split("|"c)
                If IsDBNull(row(p(1))) OrElse CDec(row(p(1))) <> Decimal.Round(CDec(InputObject(r, p(0))), 3, MidpointRounding.AwayFromZero) Then Throw New InvalidOperationException("إجماليات اعتماد العرض غير مطابقة.")
            Next
            If CInt(row("isDepended")) <> 1 OrElse CBool(row("SB_isOpen")) OrElse CInt(row("isPied")) <> 1 OrElse CInt(row("TB_isOrderd")) <> 1 OrElse after.QuoteLines.Rows.Cast(Of DataRow)().Any(Function(x) CInt(x("isDepended")) <> 1) Then Throw New InvalidOperationException("اعتماد العرض غير مكتمل.")
        End If
        If r.OperationCode = "Quote.Reserved" AndAlso after.QuoteLines.Rows.Cast(Of DataRow)().Any(Function(x) IsDBNull(x("isDepended")) OrElse CInt(x("isDepended")) <> 1) Then Throw New InvalidOperationException("لم يحجز الإجراء كل أصناف العرض.")
    End Sub

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse Not AuditContract.IsWorkflow(request.OperationCode) Then Throw New ArgumentException("يلزم عقد دورة مستند ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, before = Capture(cn, tx, r)
            Permission(cn, tx, r, before)
            If Fingerprint(before) <> V(r, "ExpectedWorkflow") Then Throw New InvalidOperationException("تغير المستند أو مراجعه؛ حدث العرض قبل إعادة المحاولة.")
            Mutate(cn, tx, r)
            Dim after = Capture(cn, tx, r), items = Events(r, before, after, AuditRequestCoordinator.ServerUtcNow(cn, tx))
            For Each item In items
                AuditWriter.WriteInTransaction(cn, tx, lease, item)
            Next
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", r.EntityId}})}
        Catch
            lease.Faulted = True : Throw
        End Try
    End Function

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
