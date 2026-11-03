Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Globalization
Imports System.Linq

Friend NotInheritable Class AuditSalaryBatch
    Private Const SalaryHash As String = "EF2B9B7A5340F03A7C9374A940F16B40F0337156D413C25D467681EA5EA04EE2"
    Private Const CancelHash As String = "3A9493E2D8EE1C2DFA27D8BB15D3EF8125483360BCFC1A2B1BBB3FD7DEAB8348"
    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, context As AuditEventContext)
        If AuditSalesQuantity.Rows(cn, tx, "SELECT u.User_ID FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@P0 AND u.is_Allow=1 AND v.Balance=1", context.ActorUserId.Value).Rows.Count <> 1 Then Throw New UnauthorizedAccessException("لا تملك صلاحية ترصيد المرتبات.")
    End Sub
    Private Shared Sub Baseline(cn As SqlConnection, tx As SqlTransaction)
        Dim defs = AuditSalesQuantity.Rows(cn, tx, "SELECT name,CONVERT(varchar(64),HASHBYTES('SHA2_256',CONVERT(varbinary(max),OBJECT_DEFINITION(object_id))),2) AS Digest FROM sys.procedures WHERE object_id IN(OBJECT_ID('dbo.EMP_Insert_Salaries'),OBJECT_ID('dbo.Agents_Balance_MV_Cancel'))")
        If defs.Rows.Count <> 2 OrElse defs.Rows.Cast(Of DataRow)().Any(Function(x) IsDBNull(x("Digest")) OrElse CStr(x("Digest")) <> If(CStr(x("name")) = "EMP_Insert_Salaries", SalaryHash, CancelHash)) Then Throw New InvalidOperationException("تغير تعريف إجراء المرتبات أو الإلغاء؛ يلزم مراجعة العقد قبل الحفظ.")
        If AuditSalesQuantity.Rows(cn, tx, "SELECT object_id FROM sys.triggers WHERE parent_id=OBJECT_ID('dbo.Agents_Balance_MV') AND is_disabled=0").Rows.Count > 0 Then Throw New InvalidOperationException("يلزم مراجعة مشغلات حركة الحساب قبل ترصيد المرتبات.")
    End Sub
    Private Shared Function Columns(entityName As String, prefix As String) As String
        Return String.Join(",", AuditContract.SalaryFields(entityName).Select(Function(x) prefix & ".[" & x.Split("|"c)(1) & "]"))
    End Function
    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, year As Integer, month As Integer) As Dictionary(Of String, DataTable)
        Dim eligibility = "SELECT v.AG_ID FROM dbo.AGENTS_MENU_V v WITH(HOLDLOCK) WHERE v.Type_ID=4 AND v.is_Emp_Pause=0 AND v.DATE_START<DATEADD(MONTH,1,DATEFROMPARTS(@P0,@P1,1))"
        Dim data As New Dictionary(Of String, DataTable)(StringComparer.Ordinal)
        data.Add("SalaryDocument", AuditSalesQuantity.Rows(cn, tx, "SELECT s.T_ID," & Columns("SalaryDocument", "s") & " FROM dbo.Agents_Balance_MV s WITH(UPDLOCK,HOLDLOCK) WHERE s.BsType_ID=5 AND s.[Year]=@P0 AND s.[Month]=@P1 ORDER BY s.T_ID", year, month))
        data.Add("SalaryEmployee", AuditSalesQuantity.Rows(cn, tx, "SELECT a.AG_ID," & Columns("SalaryEmployee", "a") & " FROM dbo.Agents a WITH(UPDLOCK,HOLDLOCK) WHERE a.AG_ID IN(" & eligibility & ") ORDER BY a.AG_ID", year, month))
        data.Add("SalaryAdjustment", AuditSalesQuantity.Rows(cn, tx, "SELECT o.T_ID," & Columns("SalaryAdjustment", "o") & " FROM dbo.Others_Values o WITH(UPDLOCK,HOLDLOCK) WHERE o.[YEAR]=@P0 AND o.[MONTH]=@P1 AND o.isVoid=0 AND o.AG_ID IN(" & eligibility & ") ORDER BY o.T_ID", year, month))
        Return data
    End Function
    Friend Shared Function Fingerprint(data As Dictionary(Of String, DataTable)) As String
        Return BitConverter.ToString(AuditCanonical.Hash(AuditCanonical.Encode(data.OrderBy(Function(x) x.Key, StringComparer.Ordinal).Select(Function(x) x.Key & ":" & AuditSalesQuantity.Fingerprint(x.Value))))).Replace("-", "")
    End Function
    Private Shared Sub Period(year As Integer, month As Integer)
        If year < 1753 OrElse year > 9998 OrElse month < 1 OrElse month > 12 Then Throw New ArgumentException("فترة المرتبات غير صالحة.")
    End Sub
    Friend Shared Function Read(connection As String, context As AuditEventContext, command As SqlCommand) As Dictionary(Of String, DataTable)
        Dim year = Convert.ToInt32(AuditPeopleCards.Parameter(command, "YEAR")), month = Convert.ToInt32(AuditPeopleCards.Parameter(command, "MONTH"))
        Period(year, month)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Permission(cn, tx, context) : Baseline(cn, tx)
                Dim data = Capture(cn, tx, year, month)
                tx.Commit() : Return data
            End Using
        End Using
    End Function
    Friend Shared Function Request(context As AuditEventContext, command As SqlCommand, data As Dictionary(Of String, DataTable)) As AuditOperationRequest
        If Not command.CommandText.Equals("EMP_Insert_Salaries", StringComparison.OrdinalIgnoreCase) OrElse Convert.ToInt32(AuditPeopleCards.Parameter(command, "User_ID")) <> context.ActorUserId.Value Then Throw New ArgumentException("إجراء/مستخدم المرتبات غير مطابق.")
        Dim year = Convert.ToInt32(AuditPeopleCards.Parameter(command, "YEAR")), month = Convert.ToInt32(AuditPeopleCards.Parameter(command, "MONTH"))
        Period(year, month)
        Dim businessDate = DateTime.SpecifyKind(New SqlDateTime(Convert.ToDateTime(AuditPeopleCards.Parameter(command, "Salary_Date"))).Value, DateTimeKind.Unspecified)
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = "Salary.BatchPosted", .EntityType = "SalaryBatch", .EntityId = year.ToString("0000", CultureInfo.InvariantCulture) & "-" & month.ToString("00", CultureInfo.InvariantCulture)}
        For Each pair In New Dictionary(Of String, Object) From {{"SalaryYear", year}, {"SalaryMonth", month}, {"SalaryDate", businessDate}, {"ExpectedSalarySnapshot", Fingerprint(data)}}
            Dim type = AuditContract.SalaryFieldType(pair.Key)
            r.Inputs.Add(New AuditInput With {.Name = pair.Key, .ValueType = type, .Value = AuditValue.Capture(type, pair.Value)})
        Next
        AuditContract.Request(r) : Return r
    End Function
    Private Shared Function V(r As AuditOperationRequest, name As String) As String
        Return r.Inputs.Single(Function(x) x.Name = name).Value.Text
    End Function
    Private Shared Function Active(row As DataRow) As Boolean
        Return Not IsDBNull(row("isVoid")) AndAlso CInt(row("isVoid")) = 0 AndAlso Not IsDBNull(row("isDepended")) AndAlso CInt(row("isDepended")) = 1
    End Function
    Friend Shared Function ExpectedAmount(employee As DataRow, adjustments As DataTable) As Decimal
        Dim salary = If(IsDBNull(employee("AG_Salary")), 0D, CDec(employee("AG_Salary")))
        Dim matching = adjustments.Rows.Cast(Of DataRow)().Where(Function(x) CInt(x("AG_ID")) = CInt(employee(0))).ToArray()
        Dim positive = matching.Where(Function(x) Not IsDBNull(x("ValType")) AndAlso CInt(x("ValType")) = 0).Sum(Function(x) If(IsDBNull(x("Value")), 0D, CDec(x("Value"))))
        Dim negative = matching.Where(Function(x) Not IsDBNull(x("ValType")) AndAlso CInt(x("ValType")) = 1).Sum(Function(x) If(IsDBNull(x("Value")), 0D, CDec(x("Value"))))
        ' Preserve NUMERIC(18,2) local variables in EMP_Insert_Salaries, including its existing no-loan formula.
        Return Decimal.Round(salary, 2, MidpointRounding.AwayFromZero) + Decimal.Round(positive, 2, MidpointRounding.AwayFromZero) - Decimal.Round(negative, 2, MidpointRounding.AwayFromZero)
    End Function
    Friend Shared Sub ValidateOutcome(r As AuditOperationRequest, before As Dictionary(Of String, DataTable), after As Dictionary(Of String, DataTable))
        For Each entityName As String In {"SalaryEmployee", "SalaryAdjustment"}
            If AuditSalesQuantity.Fingerprint(before(entityName)) <> AuditSalesQuantity.Fingerprint(after(entityName)) Then Throw New InvalidOperationException("تغير مرجع احتساب المرتب أثناء الترصيد.")
        Next
        Dim oldRows = before("SalaryDocument").Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x(0)))
        Dim newRows = after("SalaryDocument").Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x(0)))
        For Each pair In oldRows
            If Not newRows.ContainsKey(pair.Key) Then Throw New InvalidOperationException("تم حذف معاملة مرتب؛ الإجراء لا يسمح بذلك.")
            Dim oldRow = pair.Value, newRow = newRows(pair.Key), cancel = Active(oldRow)
            For Each d In AuditContract.SalaryFields("SalaryDocument")
                Dim p = d.Split("|"c)
                If Not cancel OrElse Not {"isVoid", "VoidAt", "VoidBy", "VoidReason", "NeedCancelReverse", "NeedRepost"}.Contains(p(1)) Then
                    Dim a = AuditValue.Capture(p(2), oldRow(p(1))), b = AuditValue.Capture(p(2), newRow(p(1)))
                    If a.State <> b.State OrElse a.Text <> b.Text Then Throw New InvalidOperationException("تغير حقل خارج إلغاء المرتب السابق.")
                End If
            Next
            If cancel AndAlso (IsDBNull(newRow("isVoid")) OrElse CInt(newRow("isVoid")) <> 1 OrElse IsDBNull(newRow("VoidBy")) OrElse CInt(newRow("VoidBy")) <> r.Context.ActorUserId.Value OrElse IsDBNull(newRow("VoidAt")) OrElse IsDBNull(newRow("VoidReason")) OrElse CStr(newRow("VoidReason")) <> "إلغاء معاملة المرتب قبل إعادة الاحتساب" OrElse IsDBNull(newRow("NeedRepost")) OrElse CBool(newRow("NeedRepost")) OrElse IsDBNull(newRow("NeedCancelReverse")) OrElse CBool(newRow("NeedCancelReverse")) <> (Not IsDBNull(oldRow("JournalId")))) Then Throw New InvalidOperationException("نتيجة إلغاء المرتب السابق غير مطابقة.")
        Next
        Dim added = newRows.Where(Function(x) Not oldRows.ContainsKey(x.Key)).Select(Function(x) x.Value).ToArray()
        If added.Length <> before("SalaryEmployee").Rows.Count Then Throw New InvalidOperationException("عدد المرتبات الجديدة لا يطابق الموظفين المستحقين.")
        For Each employee As DataRow In before("SalaryEmployee").Rows
            Dim matching = added.Where(Function(x) Not IsDBNull(x("AG_ID")) AndAlso CInt(x("AG_ID")) = CInt(employee(0))).ToArray()
            If matching.Length <> 1 Then Throw New InvalidOperationException("يلزم مرتب واحد لكل موظف.")
            Dim row = matching(0), expected = ExpectedAmount(employee, before("SalaryAdjustment"))
            If Not Active(row) OrElse CInt(row("BsType_ID")) <> 5 OrElse CInt(row("User_ID")) <> r.Context.ActorUserId.Value OrElse CInt(row("Year")) <> Integer.Parse(V(r, "SalaryYear"), CultureInfo.InvariantCulture) OrElse CInt(row("Month")) <> Integer.Parse(V(r, "SalaryMonth"), CultureInfo.InvariantCulture) OrElse CDate(row("Date")) <> DateTime.Parse(V(r, "SalaryDate"), CultureInfo.InvariantCulture) OrElse CDec(row("Total")) <> expected OrElse CDec(row("Pure")) <> expected Then Throw New InvalidOperationException("المرتب المحفوظ لا يطابق معادلة الإجراء والفترة المختارة.")
        Next
    End Sub
    Friend Shared Function BuildEvents(r As AuditOperationRequest, before As Dictionary(Of String, DataTable), after As Dictionary(Of String, DataTable), occurred As DateTime) As List(Of AuditEventRequest)
        Dim result As New List(Of AuditEventRequest), oldIds = New HashSet(Of Integer)(before("SalaryDocument").Rows.Cast(Of DataRow)().Select(Function(x) CInt(x(0))))
        Dim added = after("SalaryDocument").Rows.Cast(Of DataRow)().Where(Function(x) Not oldIds.Contains(CInt(x(0)))).ToArray()
        Dim makeEvent As Func(Of AuditEventRequest) = Function() New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = result.Count + 1, .EventCode = r.OperationCode, .Outcome = 1, .EntityType = "SalaryBatch", .EntityId = r.EntityId, .DocumentNumber = r.EntityId, .OccurredAtUtc = occurred, .BusinessDate = DateTime.SpecifyKind(DateTime.Parse(V(r, "SalaryDate"), CultureInfo.InvariantCulture), DateTimeKind.Unspecified)}
        Dim summary = makeEvent()
        Dim values As New Dictionary(Of String, Object) From {{"SalaryYear", Integer.Parse(V(r, "SalaryYear"), CultureInfo.InvariantCulture)}, {"SalaryMonth", Integer.Parse(V(r, "SalaryMonth"), CultureInfo.InvariantCulture)}, {"SalaryDate", summary.BusinessDate.Value}, {"SalaryNewCount", added.Length}, {"SalaryCanceledCount", before("SalaryDocument").Rows.Cast(Of DataRow)().Where(Function(x) Active(x)).Count()}, {"SalaryTotal", added.Sum(Function(x) CDec(x("Pure")))}}
        For Each d In AuditContract.SalaryFields("SalaryBatch")
            Dim p = d.Split("|"c)
            summary.Changes.Add(New AuditFieldChange With {.ChangeNo = summary.Changes.Count + 1, .EntityType = "SalaryBatch", .EntityId = r.EntityId, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 3, .Before = AuditValue.Missing(AuditValueState.Absent), .After = AuditValue.Capture(p(2), values(p(0)))})
        Next
        result.Add(summary)
        For Each entityName As String In {"SalaryDocument", "SalaryEmployee", "SalaryAdjustment"}
            Dim oldRows = before(entityName).Rows.Cast(Of DataRow)().ToDictionary(Function(x) CInt(x(0)))
            Dim current As AuditEventRequest = Nothing
            For Each row As DataRow In after(entityName).Rows
                Dim id = CInt(row(0)), oldRow = If(oldRows.ContainsKey(id), oldRows(id), Nothing)
                If entityName = "SalaryDocument" AndAlso oldRow IsNot Nothing AndAlso AuditSalesLineEdit.Fingerprint(oldRow) = AuditSalesLineEdit.Fingerprint(row) Then Continue For
                If current Is Nothing OrElse current.Changes.Count + AuditContract.SalaryFields(entityName).Length > 100 Then
                    current = makeEvent() : result.Add(current)
                End If
                For Each d In AuditContract.SalaryFields(entityName)
                    Dim p = d.Split("|"c)
                    current.Changes.Add(New AuditFieldChange With {.ChangeNo = current.Changes.Count + 1, .EntityType = entityName, .EntityId = id.ToString(CultureInfo.InvariantCulture), .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1, .Before = If(oldRow Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), oldRow(p(1)))), .After = AuditValue.Capture(p(2), row(p(1)))})
                Next
            Next
        Next
        Return result
    End Function
    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse request.OperationCode <> "Salary.BatchPosted" Then Throw New ArgumentException("يلزم عقد المرتبات ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, year = Integer.Parse(V(r, "SalaryYear"), CultureInfo.InvariantCulture), month = Integer.Parse(V(r, "SalaryMonth"), CultureInfo.InvariantCulture)
            Permission(cn, tx, r.Context) : Baseline(cn, tx)
            Dim ranges = AuditSalesQuantity.Rows(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)")
            If ranges.Rows.Count <> 1 OrElse IsDBNull(ranges.Rows(0)(0)) OrElse IsDBNull(ranges.Rows(0)(1)) Then Throw New InvalidOperationException("نطاق الترقيم غير محدد بشكل منفرد.")
            Dim ids = AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID FROM dbo.Agents_Balance_MV WITH(TABLOCKX,HOLDLOCK) WHERE T_ID BETWEEN @P0 AND @P1 ORDER BY T_ID", CInt(ranges.Rows(0)(0)), CInt(ranges.Rows(0)(1)))
            Dim before = Capture(cn, tx, year, month)
            If Fingerprint(before) <> V(r, "ExpectedSalarySnapshot") Then Throw New InvalidOperationException("تغيرت المرتبات/الموظفون/القيم الإضافية؛ حدّث التقرير قبل الترصيد.")
            Dim maximum = If(ids.Rows.Count = 0, CInt(ranges.Rows(0)(0)), CInt(ids.Rows(ids.Rows.Count - 1)(0)))
            If CLng(maximum) + before("SalaryEmployee").Rows.Count > CLng(ranges.Rows(0)(1)) Then Throw New InvalidOperationException("نطاق ترقيم المرتبات غير كاف.")
            Using cmd = AuditRequestCoordinator.Command("dbo.EMP_Insert_Salaries", cn, tx)
                cmd.Parameters.Add("@MONTH", SqlDbType.Int).Value = month
                cmd.Parameters.Add("@YEAR", SqlDbType.Int).Value = year
                cmd.Parameters.Add("@USER_ID", SqlDbType.Int).Value = r.Context.ActorUserId.Value
                cmd.Parameters.Add("@Salary_Date", SqlDbType.DateTime).Value = DateTime.Parse(V(r, "SalaryDate"), CultureInfo.InvariantCulture)
                cmd.ExecuteNonQuery()
            End Using
            AuditRequestCoordinator.RequireTransaction(cn, tx)
            Dim after = Capture(cn, tx, year, month)
            ValidateOutcome(r, before, after)
            Dim events = BuildEvents(r, before, after, AuditRequestCoordinator.ServerUtcNow(cn, tx))
            If events.Count > 100 Then Throw New InvalidOperationException("حجم دفعة المرتبات يتجاوز حد المراقبة الذرية؛ لم يتم اعتماد أي تغيير.")
            For Each e In events
                AuditContract.EventData(r, e)
            Next
            For Each e In events
                AuditWriter.WriteInTransaction(cn, tx, lease, e)
            Next
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", r.EntityId}})}
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
