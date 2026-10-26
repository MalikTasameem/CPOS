Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

' Only opens an existing sales invoice for editing. It does not save lines or payments.
Friend NotInheritable Class AuditSalesEdit
    Private Shared ReadOnly Fields As String() = {"Approved", "Voided", "NeedRepost", "JournalId", "OriginalJournalId", "EditOpenedAt", "EditOpenedBy", "DocumentTotal", "DocumentPure", "Discount"}
    Private Shared ReadOnly Columns As String() = {"isDepended", "isVoid", "NeedRepost", "JournalId", "OriginalJournalId", "EditOpenedAt", "EditOpenedBy", "Total", "Pure", "Discount"}
    Private Shared ReadOnly Types As String() = {"int", "int", "bit", "int", "int", "datetime", "int", "numeric(18,3)", "numeric(18,3)", "numeric(18,3)"}

    Friend Shared Function Request(context As AuditEventContext, documentId As Integer) As AuditOperationRequest
        If documentId <= 0 Then Throw New ArgumentException("رقم الفاتورة مطلوب.")
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = "Sales.EditOpened", .EntityType = "SalesInvoice", .EntityId = documentId.ToString(CultureInfo.InvariantCulture)}
        AuditContract.Request(r)
        Return r
    End Function

    Private Shared Function Header(cn As SqlConnection, tx As SqlTransaction, id As Integer) As DataRow
        Using cmd As New SqlCommand("SELECT T_ID,SB_ID,[Date],BsType_ID," & String.Join(",", Columns) & " FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@Id", cn, tx)
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id
            Dim dt As New DataTable()
            Using rd = cmd.ExecuteReader()
                dt.Load(rd)
            End Using
            If dt.Rows.Count <> 1 OrElse CInt(dt.Rows(0)("BsType_ID")) <> 1 Then Throw New InvalidOperationException("فاتورة المبيعات غير موجودة أو من نوع آخر.")
            Return dt.Rows(0)
        End Using
    End Function

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, operation As AuditOperationRequest) As AuditRequestResult
        If operation.OperationCode <> "Sales.EditOpened" Then Throw New ArgumentException("عملية غير مدعومة.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, operation)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request
            Dim id = Integer.Parse(r.EntityId, CultureInfo.InvariantCulture)
            Dim before = Header(cn, tx, id)
            If IsDBNull(before("isVoid")) OrElse CInt(before("isVoid")) <> 0 Then Throw New InvalidOperationException("لا يمكن فتح فاتورة ملغاة للتعديل.")
            If IsDBNull(before("isDepended")) OrElse CInt(before("isDepended")) <> 1 Then Throw New InvalidOperationException("هذا المدخل لفتح تعديل فاتورة معتمدة فقط.")
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@User AND u.is_Allow=1 AND v.SB_Update=1", cn, tx)
                cmd.Parameters.Add("@User", SqlDbType.Int).Value = r.Context.ActorUserId.Value
                If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("لا تملك صلاحية تعديل فواتير المبيعات.")
            End Using
            Using cmd = AuditRequestCoordinator.Command("dbo.Agents_Balance_MV_OpenForEdit", cn, tx)
                cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = id
                cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = r.Context.ActorUserId.Value
                cmd.ExecuteNonQuery()
            End Using
            Dim after = Header(cn, tx, id)
            If IsDBNull(after("EditOpenedAt")) OrElse IsDBNull(after("EditOpenedBy")) OrElse CInt(after("EditOpenedBy")) <> r.Context.ActorUserId.Value Then Throw New InvalidOperationException("لم يؤكد الإجراء فتح التعديل بالمستخدم المطلوب.")
            Dim item As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = 1, .EventCode = r.OperationCode, .Outcome = 1,
                .EntityType = "SalesInvoice", .EntityId = r.EntityId, .DocumentNumber = Convert.ToString(after("SB_ID"), CultureInfo.InvariantCulture),
                .OccurredAtUtc = AuditRequestCoordinator.ServerUtcNow(cn, tx), .BusinessDate = CDate(after("Date"))}
            For i = 0 To Fields.Length - 1
                Dim valueType = AuditContract.FieldType(r.OperationCode, Fields(i))
                item.Changes.Add(New AuditFieldChange With {.ChangeNo = i + 1, .EntityType = item.EntityType, .EntityId = item.EntityId, .FieldCode = Fields(i), .ValueType = valueType, .SourceSqlType = Types(i), .ValueOrigin = 1,
                    .Before = AuditValue.Capture(valueType, before(Columns(i))), .After = AuditValue.Capture(valueType, after(Columns(i)))})
            Next
            AuditWriter.WriteInTransaction(cn, tx, lease, item)
            Dim xml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", r.EntityId}})
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = xml}
        Catch
            lease.Faulted = True
            Throw
        End Try
    End Function

    Friend Shared Function Save(connection As String, request As AuditOperationRequest) As AuditRequestResult
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction()
                Try
                    Dim result = ExecuteInTransaction(cn, tx, request)
                    tx.Commit()
                    result.Status = "Completed"
                    Return result
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

' One instance per form: unknown commit retains the exact request, connection and actor.
Friend NotInheritable Class AuditSalesEditAttempt
    Private pending As AuditOperationRequest
    Private pendingConnection As String

    Friend Sub Open(connection As String, context As AuditEventContext, documentId As Integer)
        If pending IsNot Nothing AndAlso (pendingConnection <> connection OrElse pending.Context.SourceId <> context.SourceId OrElse pending.Context.SessionId.GetValueOrDefault() <> context.SessionId.GetValueOrDefault() OrElse pending.Context.ActorUserId.GetValueOrDefault() <> context.ActorUserId.GetValueOrDefault() OrElse pending.EntityId <> documentId.ToString(CultureInfo.InvariantCulture)) Then
            Throw New InvalidOperationException("هناك محاولة فتح تعديل غير محسومة؛ ارجع إلى الفاتورة السابقة أو أغلق الشاشة للتحقق من نتيجتها. مرجع: " & pending.RequestId.ToString())
        End If
        If pending Is Nothing Then
            pending = AuditSalesEdit.Request(context, documentId)
            pendingConnection = connection
        End If
        Try
            AuditSalesEdit.Save(pendingConnection, pending)
            pending = Nothing
        Catch ex As Exception
            Dim reference = pending.RequestId.ToString()
            Dim confirmedFailure As Boolean
            Try
                Dim status = AuditRequestCoordinator.Status(pendingConnection, pending)
                If status.Status = "Completed" Then
                    pending = Nothing
                    Return
                End If
                If status.Status = "NotFound" Then
                    pending = Nothing
                    confirmedFailure = True
                End If
            Catch
                ' Failure to read status is not proof that the operation failed.
            End Try
            Throw New InvalidOperationException(If(confirmedFailure, "لم يُحفظ فتح التعديل. ", "نتيجة فتح التعديل غير محسومة؛ أعد نفس المحاولة. ") & "مرجع: " & reference & Environment.NewLine & ex.Message, ex)
        End Try
    End Sub

    Friend Function CanClose() As Boolean
        If pending Is Nothing Then Return True
        Try
            Dim status = AuditRequestCoordinator.Status(pendingConnection, pending)
            If status.Status = "Completed" OrElse status.Status = "NotFound" Then
                pending = Nothing
                Return True
            End If
        Catch
        End Try
        Return False
    End Function
End Class
