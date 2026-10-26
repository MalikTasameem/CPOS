Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

' Closed mapping of the existing header editor. Does not save lines, payments or approval.
Friend NotInheritable Class AuditDocumentEditSpec
    Friend ReadOnly DocumentType As Integer
    Friend ReadOnly EntityType As String
    Friend ReadOnly NumberColumn As String
    Friend ReadOnly PermissionColumn As String
    Friend ReadOnly Screens As String()

    Private Sub New(kind As Integer, entity As String, number As String, permission As String, ParamArray screenNames As String())
        DocumentType = kind : EntityType = entity : NumberColumn = number
        PermissionColumn = permission : Screens = screenNames
    End Sub

    Friend Shared Function Find(kind As Integer) As AuditDocumentEditSpec
        Select Case kind
            Case 2 : Return New AuditDocumentEditSpec(2, "GeneralExpense", "EXP_ID", "ExpEdit", "EXP_Details")
            Case 7 : Return New AuditDocumentEditSpec(7, "PurchaseInvoice", "Pch_ID", "Pch_Update", "Pch")
            Case 8 : Return New AuditDocumentEditSpec(8, "WasteDocument", "IMEX_ID", "StExplore", "IM_Execute")
            Case 9 : Return New AuditDocumentEditSpec(9, "StockCount", "Jrd_ID", "Pch_Update", "Invoice")
            Case 10 : Return New AuditDocumentEditSpec(10, "SalesReturn", "SRtn_ID", "SB_Rtn", "Returns")
            Case 11 : Return New AuditDocumentEditSpec(11, "PurchaseReturn", "PRtn_ID", "Pch_Rtn", "Returns")
            Case 12 : Return New AuditDocumentEditSpec(12, "StockTransfer", "ST_Tran_ID", "Add_Draw_St", "Stores_ImmediateOrder")
            Case 13 : Return New AuditDocumentEditSpec(13, "AutoManufacturing", "Frm_ID", "FRM", "Format_Items_Auto")
            Case 17 : Return New AuditDocumentEditSpec(17, "StockIssue", "InSale_ID", "StExplore", "Inside_Sales")
            Case 18 : Return New AuditDocumentEditSpec(18, "ManualManufacturing", "Frm_ID_M", "Frm_M", "Format_Items_Manual")
            Case 35 : Return New AuditDocumentEditSpec(35, "StockReceipt", "OutSale_ID", "StExplore", "Outside_Sales", "Outside_Sales_Touch")
            Case Else : Throw New ArgumentException("نوع المستند غير معتمد لهذا المسار؛ لا يوجد رجوع إلى فتح التعديل القديم.")
        End Select
    End Function
End Class

Friend NotInheritable Class AuditDocumentEdit
    Friend Const OperationCode As String = "Document.EditOpened"
    Private Shared ReadOnly Fields As String() = {"DocumentType", "DocumentNumber", "Approved", "Voided", "NeedRepost", "JournalId", "OriginalJournalId", "EditOpenedAt", "EditOpenedBy", "DocumentTotal", "DocumentPure", "Discount"}
    Private Shared ReadOnly Columns As String() = {"BsType_ID", "AuditDocumentNumber", "isDepended", "isVoid", "NeedRepost", "JournalId", "OriginalJournalId", "EditOpenedAt", "EditOpenedBy", "Total", "Pure", "Discount"}
    Private Shared ReadOnly Types As String() = {"int", "int", "int", "int", "bit", "int", "int", "datetime", "int", "numeric(18,3)", "numeric(18,3)", "numeric(18,3)"}

    Friend Shared Function Request(context As AuditEventContext, documentId As Integer, kind As Integer) As AuditOperationRequest
        If context Is Nothing Then Throw New ArgumentNullException("context")
        If documentId <= 0 Then Throw New ArgumentException("رقم حركة المستند مطلوب.")
        Dim spec = AuditDocumentEditSpec.Find(kind)
        If Not spec.Screens.Contains(context.ScreenCode, StringComparer.Ordinal) Then Throw New ArgumentException("الشاشة لا تتوافق مع نوع المستند المطلوب.")
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .OperationCode = OperationCode, .Context = context.Copy(), .EntityType = spec.EntityType, .EntityId = documentId.ToString(CultureInfo.InvariantCulture)}
        r.Inputs.Add(New AuditInput With {.Name = "DocumentType", .ValueType = "Integer", .Value = AuditValue.Capture("Integer", kind)})
        AuditContract.Request(r)
        Return r
    End Function

    Private Shared Function SpecFor(request As AuditOperationRequest) As AuditDocumentEditSpec
        AuditContract.Request(request)
        If request.OperationCode <> OperationCode OrElse request.Inputs.Count <> 1 OrElse request.Inputs(0).Name <> "DocumentType" OrElse request.Inputs(0).Value.State <> AuditValueState.Present Then Throw New ArgumentException("عقد فتح المستند غير صالح.")
        Dim spec = AuditDocumentEditSpec.Find(Integer.Parse(request.Inputs(0).Value.Text, CultureInfo.InvariantCulture))
        If spec.EntityType <> request.EntityType OrElse Not spec.Screens.Contains(request.Context.ScreenCode, StringComparer.Ordinal) Then Throw New ArgumentException("هوية المستند أو الشاشة غير متوافقة مع الطلب.")
        Dim id As Integer
        If Not Integer.TryParse(request.EntityId, NumberStyles.None, CultureInfo.InvariantCulture, id) OrElse id <= 0 Then Throw New ArgumentException("رقم حركة المستند غير صالح.")
        Return spec
    End Function

    Private Shared Function Header(cn As SqlConnection, tx As SqlTransaction, id As Integer, spec As AuditDocumentEditSpec) As DataRow
        ' Column identifiers come exclusively from the closed mapping above, never user input.
        Dim selectedColumns = Columns.Where(Function(x) x <> "AuditDocumentNumber")
        Using cmd As New SqlCommand("SELECT T_ID,[Date],[" & spec.NumberColumn & "] AS AuditDocumentNumber," & String.Join(",", selectedColumns) & " FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@Id", cn, tx)
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id
            Dim dt As New DataTable()
            Using rd = cmd.ExecuteReader()
                dt.Load(rd)
            End Using
            If dt.Rows.Count <> 1 Then Throw New InvalidOperationException("المستند غير موجود.")
            Dim row = dt.Rows(0)
            If IsDBNull(row("BsType_ID")) OrElse CInt(row("BsType_ID")) <> spec.DocumentType Then Throw New InvalidOperationException("نوع المستند الفعلي لا يطابق الشاشة؛ لم يُفتح التعديل.")
            Return row
        End Using
    End Function

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, operation As AuditOperationRequest) As AuditRequestResult
        Dim spec = SpecFor(operation)
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, operation)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request
            Dim id = Integer.Parse(r.EntityId, CultureInfo.InvariantCulture)
            Dim before = Header(cn, tx, id, spec)
            If IsDBNull(before("isVoid")) OrElse CInt(before("isVoid")) <> 0 Then Throw New InvalidOperationException("لا يمكن فتح مستند ملغى للتعديل.")
            ' Some existing forms permit opening a persisted, unapproved header (not an offline draft).
            If IsDBNull(before("isDepended")) OrElse Not {0, 1}.Contains(CInt(before("isDepended"))) Then Throw New InvalidOperationException("حالة اعتماد المستند غير معروفة.")
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) JOIN dbo.Users_Validations_V v WITH(HOLDLOCK) ON v.User_ID=u.User_ID WHERE u.User_ID=@User AND u.is_Allow=1 AND v.[" & spec.PermissionColumn & "]=1", cn, tx)
                cmd.Parameters.Add("@User", SqlDbType.Int).Value = r.Context.ActorUserId.Value
                If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("لا تملك صلاحية فتح تعديل هذا النوع من المستندات: " & spec.PermissionColumn)
            End Using
            Using cmd = AuditRequestCoordinator.Command("dbo.Agents_Balance_MV_OpenForEdit", cn, tx)
                cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = id
                cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = r.Context.ActorUserId.Value
                cmd.ExecuteNonQuery()
            End Using
            Dim after = Header(cn, tx, id, spec)
            If IsDBNull(after("EditOpenedAt")) OrElse IsDBNull(after("EditOpenedBy")) OrElse CInt(after("EditOpenedBy")) <> r.Context.ActorUserId.Value Then Throw New InvalidOperationException("لم يؤكد الإجراء فتح التعديل بالمستخدم المطلوب.")
            Dim item As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = 1, .EventCode = OperationCode, .Outcome = 1,
                .EntityType = spec.EntityType, .EntityId = r.EntityId, .DocumentNumber = If(IsDBNull(after("AuditDocumentNumber")), Nothing, Convert.ToString(after("AuditDocumentNumber"), CultureInfo.InvariantCulture)),
                .OccurredAtUtc = AuditRequestCoordinator.ServerUtcNow(cn, tx), .BusinessDate = If(IsDBNull(after("Date")), CType(Nothing, DateTime?), CDate(after("Date")))}
            For i = 0 To Fields.Length - 1
                Dim valueType = AuditContract.FieldType(OperationCode, Fields(i))
                item.Changes.Add(New AuditFieldChange With {.ChangeNo = i + 1, .EntityType = spec.EntityType, .EntityId = r.EntityId, .FieldCode = Fields(i), .ValueType = valueType, .SourceSqlType = Types(i), .ValueOrigin = 1,
                    .Before = AuditValue.Capture(valueType, before(Columns(i))), .After = AuditValue.Capture(valueType, after(Columns(i)))})
            Next
            AuditWriter.WriteInTransaction(cn, tx, lease, item)
            Dim result = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", r.EntityId}})
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = result}
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

Friend NotInheritable Class AuditDocumentEditAttempt
    Private pending As AuditOperationRequest
    Private pendingConnection As String

    Friend Sub Open(connection As String, context As AuditEventContext, documentId As Integer, kind As Integer)
        Dim candidate = AuditDocumentEdit.Request(context, documentId, kind)
        If pending IsNot Nothing AndAlso (pendingConnection <> connection OrElse pending.EntityId <> candidate.EntityId OrElse pending.EntityType <> candidate.EntityType OrElse pending.Context.SourceId <> context.SourceId OrElse pending.Context.SessionId.GetValueOrDefault() <> context.SessionId.GetValueOrDefault() OrElse pending.Context.ActorUserId.GetValueOrDefault() <> context.ActorUserId.GetValueOrDefault() OrElse pending.Context.ScreenCode <> context.ScreenCode) Then
            Throw New InvalidOperationException("محاولة فتح تعديل سابقة غير محسومة؛ أعد المحاولة للمستند نفسه. مرجع: " & pending.RequestId.ToString())
        End If
        If pending Is Nothing Then
            pending = candidate : pendingConnection = connection
        End If
        Try
            AuditDocumentEdit.Save(pendingConnection, pending)
            pending = Nothing
        Catch ex As Exception
            Dim reference = pending.RequestId.ToString()
            Dim confirmedFailure As Boolean
            Try
                Dim status = AuditRequestCoordinator.Status(pendingConnection, pending)
                If status.Status = "Completed" Then
                    pending = Nothing : Return
                End If
                If status.Status = "NotFound" Then
                    pending = Nothing : confirmedFailure = True
                End If
            Catch
                ' Unavailable status is not proof of a failed commit.
            End Try
            Throw New InvalidOperationException(If(confirmedFailure, "لم يُحفظ فتح التعديل. ", "نتيجة فتح التعديل غير محسومة؛ أعد نفس المحاولة. ") & "مرجع: " & reference & Environment.NewLine & ex.Message, ex)
        End Try
    End Sub

    Friend Function CanClose() As Boolean
        If pending Is Nothing Then Return True
        Try
            Dim status = AuditRequestCoordinator.Status(pendingConnection, pending)
            If status.Status = "Completed" OrElse status.Status = "NotFound" Then
                pending = Nothing : Return True
            End If
        Catch
        End Try
        Return False
    End Function
End Class
