Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Text

Friend NotInheritable Class AuditSalesDeleteSnapshot
    Friend Header As DataRow
    Friend Rows As New Dictionary(Of String, DataTable)(StringComparer.Ordinal)
End Class

Friend NotInheritable Class AuditSalesDelete
    Friend Const Operation As String = "Sales.LineDeleted"
    Private Shared ReadOnly RowEntities As String() = {"SalesLine", "SalesStructure", "SalesComponent", "SalesReservation", "SalesTimedReservation", "SalesCommission"}

    Private Shared Function TableSpec(entity As String) As String()
        Select Case entity
            Case "SalesLine" : Return {"SB_Contents", "T_ID", "T_ID"}
            Case "SalesStructure" : Return {"SB_Contents_Structers", "SB_Cont_T_ID", "T_ID"}
            Case "SalesComponent" : Return {"SB_IM_Components", "IM_T_ID", "T_ID"}
            Case "SalesReservation" : Return {"SB_Rsv", "SB_T_ID", "T_ID"}
            Case "SalesTimedReservation" : Return {"SB_Rsv_2", "SB_T_ID", "T_ID"}
            Case "SalesCommission" : Return {"SB_VALUE_Bercent_Details", "SB_T_ID", "SB_T_ID"}
            Case Else : Throw New ArgumentException("نوع تابع غير معتمد.")
        End Select
    End Function

    Private Shared Function CaptureRows(cn As SqlConnection, tx As SqlTransaction, bill As Integer, line As Integer) As AuditSalesDeleteSnapshot
        Dim result As New AuditSalesDeleteSnapshot With {.Header = AuditSalesLineEdit.Header(cn, tx, bill)}
        For Each entity In RowEntities
            Dim spec = TableSpec(entity)
            Dim columns = AuditContract.SalesDeleteFields(entity).Select(Function(x) x.Split("|"c)(1)).Concat({spec(2)}).Distinct().Select(
                Function(x) If(entity = "SalesTimedReservation" AndAlso x = "TIME", "CONVERT(varchar(8),[TIME],108) AS [TIME]", "[" & x & "]"))
            Using cmd As New SqlCommand("SELECT " & String.Join(",", columns) & " FROM dbo.[" & spec(0) & "] WITH(UPDLOCK,HOLDLOCK) WHERE [" & spec(1) & "]=@Line ORDER BY [" & spec(2) & "]", cn, tx)
                cmd.Parameters.Add("@Line", SqlDbType.Int).Value = line
                Dim dt As New DataTable()
                Using rd = cmd.ExecuteReader()
                    dt.Load(rd)
                End Using
                result.Rows.Add(entity, dt)
            End Using
        Next
        Return result
    End Function

    Friend Shared Function ReadInTransaction(cn As SqlConnection, tx As SqlTransaction, context As AuditEventContext, bill As Integer, line As Integer, onUpdate As Boolean) As AuditSalesDeleteSnapshot
        AuditContract.Context(context, Operation)
        AuditSalesLineEdit.ReadInTransaction(cn, tx, context, bill, line, onUpdate)
        Return CaptureRows(cn, tx, bill, line)
    End Function

    Friend Shared Function ReadForDelete(connection As String, context As AuditEventContext, bill As Integer, line As Integer, onUpdate As Boolean, displayedQuantity As Decimal) As AuditSalesDeleteSnapshot
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Dim result = ReadInTransaction(cn, tx, context, bill, line, onUpdate)
                If CDec(result.Rows("SalesLine").Rows(0)("QTY")) <> displayedQuantity Then Throw New InvalidOperationException("تغيرت كمية السطر المعروض؛ حدّث الفاتورة قبل الحذف.")
                tx.Commit() : Return result
            End Using
        End Using
    End Function

    Friend Shared Function Fingerprint(s As AuditSalesDeleteSnapshot) As String
        Dim text = AuditSalesLineEdit.Fingerprint(s.Header) & "|" & String.Join("|", RowEntities.Select(Function(x) AuditSalesQuantity.Fingerprint(s.Rows(x))))
        Return BitConverter.ToString(AuditCanonical.Hash(Encoding.UTF8.GetBytes(text))).Replace("-", "")
    End Function

    Friend Shared Function Request(context As AuditEventContext, s As AuditSalesDeleteSnapshot, onUpdate As Boolean) As AuditOperationRequest
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = Operation, .EntityType = "SalesInvoice", .EntityId = CInt(s.Header("T_ID")).ToString(CultureInfo.InvariantCulture)}
        For Each p In New Dictionary(Of String, Object) From {{"LineId", CLng(s.Rows("SalesLine").Rows(0)("T_ID"))}, {"ExpectedGraphSnapshot", Fingerprint(s)}, {"OnUpdate", onUpdate}}
            Dim t = AuditContract.FieldType(Operation, p.Key)
            r.Inputs.Add(New AuditInput With {.Name = p.Key, .ValueType = t, .Value = AuditValue.Capture(t, p.Value)})
        Next
        AuditContract.Request(r) : Return r
    End Function

    Private Shared Function V(r As AuditOperationRequest, name As String) As String
        Return r.Inputs.Single(Function(x) x.Name = name).Value.Text
    End Function

    Private Shared Function Changes(entity As String, id As String, before As DataRow, after As DataRow) As List(Of AuditFieldChange)
        Dim result As New List(Of AuditFieldChange)()
        For Each definition In AuditContract.SalesDeleteFields(entity)
            Dim p = definition.Split("|"c)
            result.Add(New AuditFieldChange With {.EntityType = entity, .EntityId = id, .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1,
                .Before = AuditValue.Capture(p(2), before(p(1))), .After = If(after Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), after(p(1))))})
        Next
        Return result
    End Function

    Private Shared Function EventPart(r As AuditOperationRequest, header As DataRow, ordinal As Integer, time As DateTime) As AuditEventRequest
        Dim item As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = ordinal, .EventCode = Operation, .Outcome = 1, .EntityType = "SalesLine", .EntityId = V(r, "LineId"), .OccurredAtUtc = time,
            .DocumentNumber = If(IsDBNull(header("SB_ID")), Nothing, Convert.ToString(header("SB_ID"), CultureInfo.InvariantCulture)), .BusinessDate = If(IsDBNull(header("Date")), CType(Nothing, DateTime?), CDate(header("Date")))}
        item.RelatedEntities.Add(New AuditEntityReference With {.Role = "Document", .EntityType = "SalesInvoice", .EntityId = r.EntityId})
        Return item
    End Function

    Private Shared Sub Append(item As AuditEventRequest, values As IEnumerable(Of AuditFieldChange))
        For Each value In values
            value.ChangeNo = item.Changes.Count + 1 : item.Changes.Add(value)
        Next
    End Sub

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse request.OperationCode <> Operation Then Throw New ArgumentException("حذف الصنف يتطلب عقده ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, bill = Integer.Parse(r.EntityId, CultureInfo.InvariantCulture), line = Integer.Parse(V(r, "LineId"), CultureInfo.InvariantCulture)
            Dim before = ReadInTransaction(cn, tx, r.Context, bill, line, V(r, "OnUpdate") = "1")
            If Fingerprint(before) <> V(r, "ExpectedGraphSnapshot") Then Throw New InvalidOperationException("تغير السطر أو الرأس أو أحد التوابع؛ حدّث الفاتورة قبل الحذف.")
            Dim stock = AuditSalesQuantity.Buckets(New AuditSalesQuantitySnapshot With {.Sale = New AuditSalesLineSnapshot With {.Header = before.Header, .Line = before.Rows("SalesLine").Rows(0)}, .Structures = before.Rows("SalesStructure"), .Recipe = New DataTable()})
            For Each row In stock.Values
                row("AuditStock") = AuditSalesLineEdit.Stock(cn, tx, row)
            Next
            Using cmd = AuditRequestCoordinator.Command("dbo.SB_Contents_Delete_IM", cn, tx)
                cmd.Parameters.Add("@T_ID", SqlDbType.Int).Value = line
                cmd.Parameters.Add("@On_Update", SqlDbType.Bit).Value = V(r, "OnUpdate") = "1"
                cmd.ExecuteNonQuery()
            End Using
            Dim after = CaptureRows(cn, tx, bill, line)
            If after.Rows("SalesLine").Rows.Count <> 0 Then Throw New InvalidOperationException("لم يُحذف السطر المطلوب.")
            For Each entity In {"SalesComponent", "SalesReservation", "SalesTimedReservation", "SalesCommission"}
                If after.Rows(entity).Rows.Count <> 0 Then Throw New InvalidOperationException("لم تُحذف تفاصيل السطر التابعة.")
            Next
            If after.Rows("SalesStructure").Rows.Count <> 0 Then Throw New InvalidOperationException("لم تُحذف مكونات الصنف؛ يجب تحديث إجراء الحذف قبل متابعة العملية.")
            Dim time = AuditRequestCoordinator.ServerUtcNow(cn, tx)
            Dim events As New List(Of AuditEventRequest) From {EventPart(r, after.Header, 1, time)}
            Append(events(0), Changes("SalesLine", V(r, "LineId"), before.Rows("SalesLine").Rows(0), Nothing))
            Append(events(0), Changes("SalesInvoice", r.EntityId, before.Header, after.Header))
            Dim groups As New List(Of List(Of AuditFieldChange))()
            For Each entity In RowEntities.Where(Function(x) x <> "SalesLine")
                Dim key = TableSpec(entity)(2)
                Dim nextRows = after.Rows(entity).Rows.Cast(Of DataRow)().ToDictionary(Function(x) Convert.ToString(x(key), CultureInfo.InvariantCulture))
                For Each row As DataRow In before.Rows(entity).Rows
                    Dim id = Convert.ToString(row(key), CultureInfo.InvariantCulture)
                    groups.Add(Changes(entity, id, row, If(nextRows.ContainsKey(id), nextRows(id), Nothing)))
                    nextRows.Remove(id)
                Next
                If nextRows.Count <> 0 Then Throw New InvalidOperationException("ظهرت تفاصيل جديدة غير متوقعة أثناء الحذف.")
            Next
            For Each bucket In stock
                Dim row = bucket.Value.Table.Clone().Rows.Add(bucket.Value.ItemArray)
                row("AuditStock") = AuditSalesLineEdit.Stock(cn, tx, row)
                groups.Add(Changes("StockBucket", bucket.Key, bucket.Value, row))
            Next
            Dim extra As AuditEventRequest = Nothing
            For Each group In groups
                If extra Is Nothing OrElse extra.Changes.Count + group.Count > 100 Then
                    extra = EventPart(r, after.Header, events.Count + 1, time) : events.Add(extra)
                End If
                Append(extra, group)
            Next
            For Each item In events
                AuditContract.EventData(r, item)
            Next
            For Each item In events
                AuditWriter.WriteInTransaction(cn, tx, lease, item)
            Next
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1, .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", r.EntityId}, {"LineId", V(r, "LineId")}})}
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
