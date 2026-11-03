Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

Friend NotInheritable Class AuditExpenseSnapshot
    Friend Header As DataTable
    Friend Lines As DataTable
    Friend Reference As DataTable
End Class

' Expense rows, their header recalculation and audit share the caller's transaction.
Friend NotInheritable Class AuditExpenseLines
    Friend Shared Function V(r As AuditOperationRequest, name As String) As String
        Return r.Inputs.Single(Function(x) x.Name = name).Value.Text
    End Function

    Friend Shared Function Request(context As AuditEventContext, command As SqlCommand, bill As Integer) As AuditOperationRequest
        Dim create As Boolean
        Select Case command.CommandText.Trim("["c, "]"c).ToLowerInvariant()
            Case "exp_details_insert" : create = True
            Case "exp_details_delete" : create = False
            Case Else : Throw New ArgumentException("إجراء بند مصروف غير معتمد.")
        End Select
        If create AndAlso CInt(AuditFinance.Parameter(command, "IMEX_T_ID")) <> bill Then Throw New ArgumentException("فاتورة المصروف لا تطابق الشاشة.")
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = If(create, "Expense.LineAdded", "Expense.LineDeleted"), .EntityType = "ExpenseDocument", .EntityId = bill.ToString(CultureInfo.InvariantCulture)}
        For Each name In AuditContract.ExpenseInputs(r.OperationCode)
            Dim value As Object
            Select Case name
                Case "TargetLineId" : value = If(create, 0, CInt(AuditFinance.Parameter(command, "T_ID")))
                Case "ExpectedExpenseSnapshot" : value = New String("0"c, 64)
                Case "OnUpdate" : value = If(CBool(AuditFinance.Parameter(command, "On_Update")), 1, 0)
                Case "ExpenseId" : value = CLng(AuditFinance.Parameter(command, "EX_ID"))
                Case "ExpenseQuantity" : value = Convert.ToDecimal(AuditFinance.Parameter(command, "QYT", 1D), CultureInfo.CurrentCulture)
                Case "ExpensePrice" : value = Convert.ToDecimal(AuditFinance.Parameter(command, "Price"), CultureInfo.CurrentCulture)
                Case "ExpenseTotal" : value = Convert.ToDecimal(AuditFinance.Parameter(command, "Total"), CultureInfo.CurrentCulture)
                Case Else : Throw New ArgumentException("مدخل بند مصروف غير معتمد.")
            End Select
            Dim type = AuditContract.FieldType(r.OperationCode, name)
            r.Inputs.Add(New AuditInput With {.Name = name, .ValueType = type, .Value = AuditValue.Capture(type, value)})
        Next
        AuditContract.Request(r) : Return r
    End Function

    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, Optional old As AuditExpenseSnapshot = Nothing) As AuditExpenseSnapshot
        Dim fields = AuditContract.ExpenseFields("ExpenseDocument").Select(Function(x) x.Split("|"c)(1)).Where(Function(x) x <> "AuditDocumentNumber").Select(Function(x) "[" & x & "]")
        Dim s As New AuditExpenseSnapshot
        s.Header = AuditSalesQuantity.Rows(cn, tx, "SELECT T_ID,EXP_ID AS AuditDocumentNumber,EditOpenedAt,EditOpenedBy," & String.Join(",", fields) & " FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@P0", CInt(r.EntityId))
        If s.Header.Rows.Count <> 1 OrElse CInt(s.Header.Rows(0)("BsType_ID")) <> 2 OrElse CInt(s.Header.Rows(0)("isVoid")) <> 0 Then Throw New InvalidOperationException("فاتورة المصروف غير موجودة أو ملغاة.")
        Dim h = s.Header.Rows(0)
        If Not {0, 1}.Contains(CInt(h("isDepended"))) Then Throw New InvalidOperationException("حالة اعتماد المصروف غير معروفة.")
        If CInt(h("isDepended")) = 1 AndAlso (V(r, "OnUpdate") <> "1" OrElse IsDBNull(h("EditOpenedAt")) OrElse IsDBNull(h("EditOpenedBy")) OrElse CInt(h("EditOpenedBy")) <> r.Context.ActorUserId.Value) Then Throw New InvalidOperationException("افتح تعديل المصروف بالمستخدم الحالي أولًا.")
        s.Lines = AuditSalesQuantity.Rows(cn, tx, "SELECT * FROM dbo.EXP_Details WITH(UPDLOCK,HOLDLOCK) WHERE IMEX_T_ID=@P0 ORDER BY T_ID", CInt(r.EntityId))
        Dim expense As Long
        If r.OperationCode = "Expense.LineAdded" Then
            expense = Long.Parse(V(r, "ExpenseId"), CultureInfo.InvariantCulture)
        Else
            Dim line = If(old Is Nothing, s, old).Lines.Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = CInt(V(r, "TargetLineId")))
            If line Is Nothing Then Throw New InvalidOperationException("البند لا يتبع فاتورة المصروف المعروضة أو حُذف.")
            expense = CLng(line("EX_ID"))
        End If
        Using cmd As New SqlCommand("SELECT Ex_ID,Ex_Name FROM dbo.Expenses_Card WITH(HOLDLOCK) WHERE Ex_ID=@Id", cn, tx)
            cmd.Parameters.Add("@Id", SqlDbType.BigInt).Value = expense
            s.Reference = New DataTable
            Using reader = cmd.ExecuteReader()
                s.Reference.Load(reader)
            End Using
        End Using
        If s.Reference.Rows.Count <> 1 Then Throw New InvalidOperationException("بطاقة المصروف غير موجودة.")
        If s.Lines.Rows.Count > 1000 Then Throw New InvalidOperationException("عدد بنود المصروف يتجاوز حد المراجعة الآمنة.")
        Return s
    End Function

    Friend Shared Function Fingerprint(s As AuditExpenseSnapshot) As String
        Return BitConverter.ToString(AuditCanonical.Hash(System.Text.Encoding.UTF8.GetBytes(String.Join("|", {AuditSalesQuantity.Fingerprint(s.Header), AuditSalesQuantity.Fingerprint(s.Lines), AuditSalesQuantity.Fingerprint(s.Reference)})))).Replace("-", "")
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, s As AuditExpenseSnapshot)
        Dim flag = If(CInt(s.Header.Rows(0)("isDepended")) = 1 OrElse V(r, "OnUpdate") = "1", "ExpEdit", "Exp")
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@User AND u.is_Allow=1 AND v.[" & flag & "]=1", cn, tx)
            cmd.Parameters.Add("@User", SqlDbType.Int).Value = r.Context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("لا تملك صلاحية إضافة/حذف بند المصروف: " & flag)
        End Using
    End Sub

    Friend Shared Sub Prepare(connection As String, r As AuditOperationRequest)
        AuditContract.Request(r)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Dim s = Capture(cn, tx, r)
                Permission(cn, tx, r, s)
                r.Inputs.Single(Function(x) x.Name = "ExpectedExpenseSnapshot").Value = AuditValue.Capture("String", Fingerprint(s))
                tx.Commit()
            End Using
        End Using
    End Sub

    Private Shared Function Allocate(cn As SqlConnection, tx As SqlTransaction) As Integer
        Dim range = AuditSalesQuantity.Rows(cn, tx, "SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)")
        If range.Rows.Count <> 1 OrElse IsDBNull(range.Rows(0)(0)) OrElse IsDBNull(range.Rows(0)(1)) Then Throw New InvalidOperationException("نطاق ترقيم المصروف غير محدد.")
        Dim first = CInt(range.Rows(0)(0)), last = CInt(range.Rows(0)(1))
        Dim maximum = AuditSalesQuantity.Rows(cn, tx, "SELECT ISNULL(MAX(T_ID),@P0) FROM dbo.EXP_Details WITH(UPDLOCK,HOLDLOCK) WHERE T_ID BETWEEN @P0 AND @P1", first, last)
        If first < 0 OrElse last <= first OrElse CLng(maximum.Rows(0)(0)) >= last Then Throw New InvalidOperationException("نطاق ترقيم بنود المصروف ممتلئ.")
        Return CInt(maximum.Rows(0)(0)) + 1
    End Function

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse Not AuditContract.IsExpenseLine(request.OperationCode) Then Throw New ArgumentException("يلزم عقد بند مصروف ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, create = r.OperationCode = "Expense.LineAdded", before = Capture(cn, tx, r)
            Permission(cn, tx, r, before)
            If Fingerprint(before) <> V(r, "ExpectedExpenseSnapshot") Then Throw New InvalidOperationException("تغيرت فاتورة المصروف؛ حدّث العرض قبل إعادة العملية.")
            Dim id = If(create, Allocate(cn, tx), CInt(V(r, "TargetLineId")))
            Using cmd = AuditRequestCoordinator.Command(If(create, "dbo.EXP_Details_Insert", "dbo.EXP_Details_Delete"), cn, tx)
                cmd.Parameters.Add("@On_Update", SqlDbType.Bit).Value = V(r, "OnUpdate") = "1"
                If create Then
                    cmd.Parameters.Add("@IMEX_T_ID", SqlDbType.Int).Value = CInt(r.EntityId)
                    cmd.Parameters.Add("@EX_ID", SqlDbType.Int).Value = Integer.Parse(V(r, "ExpenseId"), CultureInfo.InvariantCulture)
                    For Each pair In {"ExpenseQuantity|QYT", "ExpensePrice|Price", "ExpenseTotal|Total"}
                        Dim p = pair.Split("|"c)
                        cmd.Parameters.AddWithValue("@" & p(1), Decimal.Parse(V(r, p(0)), CultureInfo.InvariantCulture))
                    Next
                Else
                    cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = id
                End If
                cmd.ExecuteNonQuery()
            End Using
            AuditRequestCoordinator.RequireTransaction(cn, tx)
            ' Both original procedures already recalculate this header. No post-commit Save_Total.
            Dim after = Capture(cn, tx, r, before)
            Dim oldLine = before.Lines.Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = id)
            Dim newLine = after.Lines.Rows.Cast(Of DataRow)().SingleOrDefault(Function(x) CInt(x("T_ID")) = id)
            If If(create, oldLine IsNot Nothing OrElse newLine Is Nothing, oldLine Is Nothing OrElse newLine IsNot Nothing) Then Throw New InvalidOperationException("لم يؤكد الإجراء أثر بند المصروف.")
            Dim b = before.Lines.Copy(), a = after.Lines.Copy()
            For Each dt In {b, a}
                For Each row In dt.Rows.Cast(Of DataRow)().Where(Function(x) CInt(x("T_ID")) = id).ToArray()
                    dt.Rows.Remove(row)
                Next
            Next
            If AuditSalesQuantity.Fingerprint(b) <> AuditSalesQuantity.Fingerprint(a) Then Throw New InvalidOperationException("تغير بند آخر خارج عملية المصروف.")
            If create Then
                If CLng(newLine("EX_ID")) <> CLng(V(r, "ExpenseId")) OrElse CInt(newLine("IMEX_T_ID")) <> CInt(r.EntityId) Then Throw New InvalidOperationException("مرجع بند المصروف المنشأ غير مطابق.")
                For Each pair In {"ExpenseQuantity|QYT", "ExpensePrice|Price", "ExpenseTotal|Total"}
                    Dim p = pair.Split("|"c)
                    If CDec(newLine(p(1))) <> Decimal.Round(Decimal.Parse(V(r, p(0)), CultureInfo.InvariantCulture), 3, MidpointRounding.AwayFromZero) Then Throw New InvalidOperationException("قيمة بند المصروف المحفوظة غير مطابقة.")
                Next
            End If
            Dim e As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = 1, .EventCode = r.OperationCode, .Outcome = 1, .EntityType = r.EntityType, .EntityId = r.EntityId, .DocumentNumber = Convert.ToString(after.Header.Rows(0)("AuditDocumentNumber"), CultureInfo.InvariantCulture), .BusinessDate = CDate(after.Header.Rows(0)("Date")), .OccurredAtUtc = AuditRequestCoordinator.ServerUtcNow(cn, tx)}
            AddChanges(e, "ExpenseDocument", r.EntityId, before.Header.Rows(0), after.Header.Rows(0))
            AddChanges(e, "ExpenseLine", id.ToString(CultureInfo.InvariantCulture), oldLine, newLine)
            AuditWriter.WriteInTransaction(cn, tx, lease, e)
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", r.EntityId}, {"LineId", id.ToString(CultureInfo.InvariantCulture)}})}
        Catch
            lease.Faulted = True : Throw
        End Try
    End Function

    Private Shared Sub AddChanges(e As AuditEventRequest, entity As String, id As String, before As DataRow, after As DataRow)
        For Each definition In AuditContract.ExpenseFields(entity)
            Dim p = definition.Split("|"c)
            e.Changes.Add(New AuditFieldChange With {.ChangeNo = e.Changes.Count + 1, .EntityType = entity, .EntityId = id, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1, .Before = If(before Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), before(p(1)))), .After = If(after Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), after(p(1))))})
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
