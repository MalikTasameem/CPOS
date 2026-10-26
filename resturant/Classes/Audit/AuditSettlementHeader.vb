Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Globalization
Imports System.Linq

Friend NotInheritable Class AuditSettlementHeader
    Private Shared ReadOnly Fields As String() = {"DocumentNumber", "OwnerUserId", "AccountId", "DocumentDate", "Title", "About", "DocumentTotal", "DocumentPure", "Discount", "Approved", "Voided", "NeedRepost", "NeedCancelReverse", "JournalId", "OriginalJournalId", "EditOpenedAt", "EditOpenedBy", "VoidAt", "VoidBy", "VoidReason", "Balance", "SalesOpen", "Paused", "OrderDelivered"}
    Private Shared ReadOnly Columns As String() = {"ST_Sett_ID", "User_ID", "AG_ID", "Date", "Receipt_Title", "About", "Total", "Pure", "Discount", "isDepended", "isVoid", "NeedRepost", "NeedCancelReverse", "JournalId", "OriginalJournalId", "EditOpenedAt", "EditOpenedBy", "VoidAt", "VoidBy", "VoidReason", "Balance", "SB_isOpen", "isPause", "Order_isDeleverd"}
    Private Shared ReadOnly SqlTypes As String() = {"int", "int", "int", "datetime", "nvarchar(550)", "nvarchar(max)", "numeric(18,3)", "numeric(18,3)", "numeric(18,3)", "int", "int", "bit", "bit", "int", "int", "datetime", "int", "datetime", "int", "nvarchar(500)", "numeric(18,3)", "bit", "bit", "bit"}

    Friend Shared Function Request(context As AuditEventContext, action As String, id As Integer, documentDate As DateTime, title As String, about As String, allowNegative As Boolean) As AuditOperationRequest
        Dim operation = "Settlement." & action
        If Not AuditContract.IsSettlementHeader(operation) Then Throw New ArgumentException("عملية رأس تسوية غير معتمدة.")
        If (action = "Created" AndAlso id <> 0) OrElse (action <> "Created" AndAlso id <= 0) Then Throw New ArgumentException("معرف مستند غير صالح.")
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .OperationCode = operation, .Context = context.Copy(), .EntityType = "Settlement"}
        r.EntityId = If(action = "Created", "new:" & r.RequestId.ToString("D"), id.ToString(CultureInfo.InvariantCulture))
        If {"Created", "Approved", "HeaderSaved", "EditFinished"}.Contains(action) Then
            If title Is Nothing OrElse title.Length > 350 OrElse about Is Nothing OrElse about.Length > 4096 Then Throw New ArgumentException("العنوان أقصاه 350 حرفًا والملاحظات 4096؛ لا تقص القيم بصمت.")
            AddInput(r, "DocumentDate", DateTime.SpecifyKind(New SqlDateTime(documentDate).Value, DateTimeKind.Unspecified))
            AddInput(r, "Title", title)
            AddInput(r, "About", If(String.IsNullOrWhiteSpace(about), CType(DBNull.Value, Object), about))
        End If
        If action = "Voided" Then AddInput(r, "AllowNegative", allowNegative)
        AuditContract.Request(r)
        Return r
    End Function

    Private Shared Sub AddInput(r As AuditOperationRequest, name As String, value As Object)
        Dim valueType = AuditContract.FieldType(r.OperationCode, name)
        r.Inputs.Add(New AuditInput With {.Name = name, .ValueType = valueType, .Value = AuditValue.Capture(valueType, value)})
    End Sub

    Private Shared Function Input(r As AuditOperationRequest, name As String) As Object
        Dim v = r.Inputs.Single(Function(x) x.Name = name).Value
        If v.State = AuditValueState.SqlNull Then Return DBNull.Value
        If v.State <> AuditValueState.Present Then Throw New ArgumentException("مدخل رأس غير ملتقط.")
        If v.ValueType = "DateTime" Then Return DateTime.SpecifyKind(DateTime.Parse(v.Text, CultureInfo.InvariantCulture), DateTimeKind.Unspecified)
        If v.ValueType = "Boolean" Then Return v.Text = "1"
        Return v.Text
    End Function

    Private Shared Function Header(cn As SqlConnection, tx As SqlTransaction, id As Integer) As DataRow
        Using cmd As New SqlCommand("SELECT " & String.Join(",", Columns.Select(Function(x) "[" & x & "]")) & " FROM dbo.Agents_Balance_MV WITH(UPDLOCK,HOLDLOCK) WHERE T_ID=@Id AND BsType_ID=39", cn, tx)
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id
            Dim table As New DataTable()
            Using reader = cmd.ExecuteReader()
                table.Load(reader)
            End Using
            If table.Rows.Count <> 1 Then Throw New InvalidOperationException("لم يعثر على مستند التسوية المحدد.")
            Return table.Rows(0)
        End Using
    End Function

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        If Not AuditContract.IsSettlementHeader(request.OperationCode) Then Throw New ArgumentException("عملية غير معتمدة.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request
            Dim action = r.OperationCode.Substring("Settlement.".Length)
            Dim id = If(action = "Created", 0, Integer.Parse(r.EntityId, CultureInfo.InvariantCulture))
            Dim before = If(action = "Created", Nothing, Header(cn, tx, id))
            Dim changed As Boolean, number As Integer
            Using cmd = AuditRequestCoordinator.Command("dbo.ST_Settlement_Header_Apply", cn, tx)
                cmd.Parameters.Add("@Action", SqlDbType.VarChar, 24).Value = action
                cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = r.Context.ActorUserId.Value
                Dim idParameter = cmd.Parameters.Add("@T_ID", SqlDbType.Int)
                idParameter.Direction = ParameterDirection.InputOutput : idParameter.Value = id
                Dim numberParameter = cmd.Parameters.Add("@DocumentNumber", SqlDbType.Int)
                numberParameter.Direction = ParameterDirection.Output
                Dim changedParameter = cmd.Parameters.Add("@Changed", SqlDbType.Bit)
                changedParameter.Direction = ParameterDirection.Output
                If {"Created", "Approved", "HeaderSaved", "EditFinished"}.Contains(action) Then
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = Input(r, "DocumentDate")
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 350).Value = Input(r, "Title")
                    cmd.Parameters.Add("@About", SqlDbType.NVarChar, -1).Value = Input(r, "About")
                End If
                If action = "Voided" Then cmd.Parameters.Add("@AllowNegative", SqlDbType.Bit).Value = Input(r, "AllowNegative")
                cmd.ExecuteNonQuery()
                id = CInt(idParameter.Value) : number = CInt(numberParameter.Value) : changed = CBool(changedParameter.Value)
            End Using
            Dim after = Header(cn, tx, id)
            Dim item As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = 1, .EventCode = r.OperationCode, .Outcome = 1,
                .EntityType = "Settlement", .EntityId = id.ToString(CultureInfo.InvariantCulture), .DocumentNumber = number.ToString(CultureInfo.InvariantCulture),
                .OccurredAtUtc = AuditRequestCoordinator.ServerUtcNow(cn, tx), .BusinessDate = CDate(after("Date"))}
            For i = 0 To Fields.Length - 1
                Dim valueType = AuditContract.FieldType(r.OperationCode, Fields(i))
                Dim oldValue = If(before Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(valueType, before(Columns(i))))
                Dim newValue = AuditValue.Capture(valueType, after(Columns(i)))
                If oldValue.State <> newValue.State OrElse oldValue.Text <> newValue.Text Then changed = True
                item.Changes.Add(New AuditFieldChange With {.ChangeNo = i + 1, .EntityType = "Settlement", .EntityId = item.EntityId,
                    .FieldCode = Fields(i), .ValueType = valueType, .SourceSqlType = SqlTypes(i), .Before = oldValue, .After = newValue, .ValueOrigin = 1})
            Next
            If changed Then AuditWriter.WriteInTransaction(cn, tx, lease, item)
            Dim code As Byte = If(changed, CByte(1), CByte(5))
            Dim resultXml = AuditRequestCoordinator.Complete(cn, tx, lease, code, New Dictionary(Of String, String) From {{"EntityId", item.EntityId}, {"DocumentNumber", item.DocumentNumber}})
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = code, .ResultXml = resultXml}
        Catch
            lease.Faulted = True
            Throw
        End Try
    End Function

    Friend Shared Function ResultId(result As AuditRequestResult) As Integer
        Return Integer.Parse(AuditCanonical.ParseXml(result.ResultXml).Element("Values").Elements("Value").Single(Function(x) CStr(x.Attribute("name")) = "EntityId").Value, CultureInfo.InvariantCulture)
    End Function

    Friend Shared Function Save(connection As String, request As AuditOperationRequest) As AuditRequestResult
        Try
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
        Catch ex As Exception
            Try
                Dim status = AuditRequestCoordinator.Status(connection, request)
                If status.Status = "Completed" Then Return status
            Catch
            End Try
            Throw New InvalidOperationException("لم تتأكد نتيجة مستند التسوية؛ أعد المحاولة بنفس الطلب " & request.RequestId.ToString("D") & Environment.NewLine & ex.Message, ex)
        End Try
    End Function
End Class
