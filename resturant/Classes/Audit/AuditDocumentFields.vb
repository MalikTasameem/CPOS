Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

' Direct header mutations only. Approval, payment reconciliation and distributed discounts are separate workflows.
Friend NotInheritable Class AuditDocumentFields
    Friend Shared Function Value(r As AuditOperationRequest, name As String) As String
        Return r.Inputs.Single(Function(x) x.Name = name).Value.Text
    End Function

    Friend Shared Function Request(context As AuditEventContext, bill As Integer, kind As Integer, account As Integer?, discount As Decimal?, onUpdate As Boolean, Optional percent As Decimal? = Nothing) As AuditOperationRequest
        If account.HasValue = discount.HasValue Then Throw New ArgumentException("حدد تغيير الحساب أو الخصم فقط.")
        If percent.HasValue AndAlso account.HasValue Then Throw New ArgumentException("لا يجوز جمع الحساب مع نسبة الخصم.")
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .EntityType = "DocumentHeader", .EntityId = bill.ToString(CultureInfo.InvariantCulture), .OperationCode = If(account.HasValue, "Document.AccountChanged", "Document.DiscountChanged")}
        Dim values As New Dictionary(Of String, Object) From {{"DocumentType", kind}, {"OnUpdate", onUpdate}, {"ExpectedDocumentFields", New String("0"c, 64)}}
        If account.HasValue Then values.Add("AccountId", account.Value) Else values.Add("Discount", discount.Value)
        If percent.HasValue Then
            r.OperationCode = "Document.FastDiscountChanged"
            values.Add("DiscountPercent", percent.Value)
        End If
        For Each name In AuditContract.DocumentFieldInputs(r.OperationCode)
            Dim type = AuditContract.FieldType(r.OperationCode, name)
            r.Inputs.Add(New AuditInput With {.Name = name, .ValueType = type, .Value = AuditValue.Capture(type, values(name))})
        Next
        AuditContract.Request(r) : Return r
    End Function

    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest) As DataTable
        Dim spec = AuditContract.DocumentFieldSpec(Integer.Parse(Value(r, "DocumentType"), CultureInfo.InvariantCulture))
        Dim fields = AuditContract.DocumentHeaderFields().Select(Function(x) x.Split("|"c)(1)).Where(Function(x) x <> "AuditDocumentNumber" AndAlso x <> "AuditAccountName").Select(Function(x) "b.[" & x & "]")
        Dim dt = AuditSalesQuantity.Rows(cn, tx, "SELECT b.T_ID,b.[" & spec(0) & "] AS AuditDocumentNumber,a.Ag_name AS AuditAccountName," & String.Join(",", fields) & " FROM dbo.Agents_Balance_MV b WITH(UPDLOCK,HOLDLOCK) LEFT JOIN dbo.Agents a WITH(HOLDLOCK) ON a.AG_ID=b.AG_ID WHERE b.T_ID=@P0", CInt(r.EntityId))
        If dt.Rows.Count <> 1 OrElse IsDBNull(dt.Rows(0)("BsType_ID")) OrElse CInt(dt.Rows(0)("BsType_ID")) <> CInt(Value(r, "DocumentType")) Then Throw New InvalidOperationException("الفاتورة غير موجودة أو نوعها لا يطابق الشاشة.")
        Return dt
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, r As AuditOperationRequest, row As DataRow)
        If IsDBNull(row("isVoid")) OrElse CInt(row("isVoid")) <> 0 OrElse IsDBNull(row("isDepended")) OrElse Not {0, 1}.Contains(CInt(row("isDepended"))) Then Throw New InvalidOperationException("لا يمكن تعديل فاتورة ملغاة أو بحالة اعتماد غير معروفة.")
        Dim kind = CInt(Value(r, "DocumentType")), editing = CInt(row("isDepended")) = 1 OrElse Value(r, "OnUpdate") = "1"
        If CInt(row("isDepended")) = 1 AndAlso (Value(r, "OnUpdate") <> "1" OrElse (kind <> 16 AndAlso (IsDBNull(row("EditOpenedAt")) OrElse IsDBNull(row("EditOpenedBy")) OrElse CInt(row("EditOpenedBy")) <> r.Context.ActorUserId.Value))) Then Throw New InvalidOperationException("افتح تعديل الفاتورة بالمستخدم الحالي أولًا.")
        Dim spec = AuditContract.DocumentFieldSpec(kind), flag = spec(If(editing, 2, 1))
        Dim discountCheck = If(r.OperationCode <> "Document.AccountChanged" AndAlso kind <> 7, " AND v.Sales_Dis=1", "")
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@User AND u.is_Allow=1 AND v.[" & flag & "]=1" & discountCheck, cn, tx)
            cmd.Parameters.Add("@User", SqlDbType.Int).Value = r.Context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("لا تملك صلاحية تغيير حساب/خصم الفاتورة.")
        End Using
        If r.OperationCode = "Document.AccountChanged" Then
            Dim accounts = AuditSalesQuantity.Rows(cn, tx, "SELECT AG_ID FROM dbo.Agents WITH(HOLDLOCK) WHERE AG_ID=@P0", CInt(Value(r, "AccountId")))
            If accounts.Rows.Count <> 1 Then Throw New InvalidOperationException("الحساب الجديد غير موجود أو رقمه مكرر.")
        End If
    End Sub

    Friend Shared Sub Prepare(connection As String, r As AuditOperationRequest)
        AuditContract.Request(r)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Dim before = Capture(cn, tx, r)
                Permission(cn, tx, r, before.Rows(0))
                r.Inputs.Single(Function(x) x.Name = "ExpectedDocumentFields").Value = AuditValue.Capture("String", AuditSalesQuantity.Fingerprint(before))
                tx.Commit()
            End Using
        End Using
    End Sub

    Friend Shared Function EventData(r As AuditOperationRequest, before As DataTable, after As DataTable, occurred As DateTime) As AuditEventRequest
        AuditContract.Request(r)
        If before.Rows.Count <> 1 OrElse after.Rows.Count <> 1 Then Throw New ArgumentException("يلزم التقاط رأس الفاتورة قبل وبعد.")
        Dim b = before.Rows(0), a = after.Rows(0)
        If CInt(b("T_ID")) <> CInt(r.EntityId) OrElse CInt(a("T_ID")) <> CInt(r.EntityId) Then Throw New InvalidOperationException("رأس الفاتورة لا يطابق الطلب.")
        Dim e As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = 1, .EventCode = r.OperationCode, .Outcome = 1, .EntityType = r.EntityType, .EntityId = r.EntityId, .DocumentNumber = Convert.ToString(a("AuditDocumentNumber"), CultureInfo.InvariantCulture), .OccurredAtUtc = occurred}
        If Not IsDBNull(a("Date")) Then e.BusinessDate = CDate(a("Date"))
        For Each definition In AuditContract.DocumentHeaderFields()
            Dim p = definition.Split("|"c)
            Dim oldValue = AuditValue.Capture(p(2), b(p(1))), newValue = AuditValue.Capture(p(2), a(p(1)))
            Dim allowed = AuditContract.DocumentFieldAllowed(r, p(0))
            If Not allowed AndAlso (oldValue.State <> newValue.State OrElse oldValue.Text <> newValue.Text) Then Throw New InvalidOperationException("تغير حقل خارج نطاق العملية: " & p(0))
            e.Changes.Add(New AuditFieldChange With {.ChangeNo = e.Changes.Count + 1, .EntityType = r.EntityType, .EntityId = r.EntityId, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1, .Before = oldValue, .After = newValue})
        Next
        If r.OperationCode = "Document.AccountChanged" Then
            If CInt(a("AG_ID")) <> CInt(Value(r, "AccountId")) Then Throw New InvalidOperationException("لم يُحفظ حساب الفاتورة المطلوب.")
        ElseIf IsDBNull(a("Discount")) OrElse CDec(a("Discount")) <> Decimal.Round(Decimal.Parse(Value(r, "Discount"), CultureInfo.InvariantCulture), 3, MidpointRounding.AwayFromZero) Then
            Throw New InvalidOperationException("لم تُحفظ قيمة الخصم المطلوبة.")
        End If
        AuditContract.EventData(r, e) : Return e
    End Function

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse Not AuditContract.IsDocumentField(request.OperationCode) Then Throw New ArgumentException("يلزم عقد تغيير رأس الفاتورة ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, before = Capture(cn, tx, r)
            Permission(cn, tx, r, before.Rows(0))
            If AuditSalesQuantity.Fingerprint(before) <> Value(r, "ExpectedDocumentFields") Then Throw New InvalidOperationException("تغيرت الفاتورة؛ حدّث العرض قبل إعادة المحاولة.")
            If r.OperationCode = "Document.AccountChanged" Then
                Using cmd = AuditRequestCoordinator.Command("dbo.AG_Balance_Update_AG", cn, tx)
                    cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = CInt(r.EntityId)
                    cmd.Parameters.Add("@AG_ID", SqlDbType.Int).Value = CInt(Value(r, "AccountId"))
                    cmd.Parameters.Add("@ON_UPDATE", SqlDbType.Bit).Value = Value(r, "OnUpdate") = "1"
                    cmd.ExecuteNonQuery()
                End Using
            Else
                Dim setPercent = AuditContract.DocumentFieldAllowed(r, "SecondaryDebit")
                Using cmd As New SqlCommand("UPDATE dbo.Agents_Balance_MV SET Discount=@Discount" & If(setPercent, ",Debit_2=@Percent", "") & " WHERE T_ID=@Id", cn, tx)
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = CInt(r.EntityId)
                    cmd.Parameters.AddWithValue("@Discount", Decimal.Parse(Value(r, "Discount"), CultureInfo.InvariantCulture))
                    If setPercent Then cmd.Parameters.AddWithValue("@Percent", Decimal.Parse(Value(r, "DiscountPercent"), CultureInfo.InvariantCulture))
                    If cmd.ExecuteNonQuery() <> 1 Then Throw New InvalidOperationException("لم يُحفظ خصم فاتورة واحدة.")
                End Using
            End If
            AuditRequestCoordinator.RequireTransaction(cn, tx)
            Dim after = Capture(cn, tx, r), e = EventData(r, before, after, AuditRequestCoordinator.ServerUtcNow(cn, tx))
            AuditWriter.WriteInTransaction(cn, tx, lease, e)
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
