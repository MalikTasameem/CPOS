Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

' Unit master only. Item-unit links are separate business operations.
Friend NotInheritable Class AuditUnits
    Friend Shared Function Cargo(text As String) As Double
        Dim value = If(String.IsNullOrWhiteSpace(text), 1.0, Double.Parse(text, CultureInfo.CurrentCulture))
        If Double.IsNaN(value) OrElse Double.IsInfinity(value) Then Throw New ArgumentException("حمولة الوحدة غير صالحة.")
        Return If(value = 0, 1.0, value) ' Same normalization as the previous UI.
    End Function

    Friend Shared Function Capture(cn As SqlConnection, tx As SqlTransaction, id As Integer) As DataRow
        Dim dt As New DataTable()
        Using cmd As New SqlCommand("SELECT U_ID,U_Name,U_Cargo,isDefault FROM dbo.Units WITH(UPDLOCK,HOLDLOCK) WHERE U_ID=@Id", cn, tx)
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id
            Using reader = cmd.ExecuteReader()
                dt.Load(reader)
            End Using
        End Using
        If dt.Rows.Count > 1 Then Throw New InvalidOperationException("معرف وحدة مكرر.")
        Return If(dt.Rows.Count = 0, Nothing, dt.Rows(0))
    End Function

    Friend Shared Function Fingerprint(row As DataRow) As String
        If row Is Nothing Then Throw New InvalidOperationException("الوحدة غير موجودة.")
        Dim tokens As New List(Of String) From {CInt(row("U_ID")).ToString(CultureInfo.InvariantCulture)}
        For Each definition In AuditContract.UnitFields()
            Dim p = definition.Split("|"c), value = AuditValue.Capture(p(2), row(p(1)))
            tokens.Add(CInt(value.State).ToString(CultureInfo.InvariantCulture)) : tokens.Add(value.Text)
        Next
        Return BitConverter.ToString(AuditCanonical.Hash(AuditCanonical.Encode(tokens))).Replace("-", "")
    End Function

    Private Shared Sub Permission(cn As SqlConnection, tx As SqlTransaction, context As AuditEventContext)
        ' Units/ItemsMenu are under MainForm.ITEMS_btn (IM). Auxiliary item dialogs
        ' previously permit unit creation in their parent workflow; no new role is invented here.
        Dim needItems = {"Units", "ItemsMenu"}.Contains(context.ScreenCode)
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM dbo.Users u WITH(HOLDLOCK) WHERE u.User_ID=@User AND u.is_Allow=1" & If(needItems, " AND EXISTS(SELECT 1 FROM dbo.Users_Validations_V v WITH(HOLDLOCK) WHERE v.User_ID=u.User_ID AND v.IM=1)", ""), cn, tx)
            cmd.Parameters.Add("@User", SqlDbType.Int).Value = context.ActorUserId.Value
            If CInt(cmd.ExecuteScalar()) <> 1 Then Throw New UnauthorizedAccessException("المستخدم غير مسموح له أو لا يملك صلاحية إدارة الأصناف.")
        End Using
    End Sub

    Friend Shared Function Read(connection As String, context As AuditEventContext, id As Integer) As DataRow
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Permission(cn, tx, context)
                Dim row = Capture(cn, tx, id)
                If row Is Nothing Then Throw New InvalidOperationException("الوحدة غير موجودة.")
                tx.Commit() : Return row
            End Using
        End Using
    End Function

    Friend Shared Function Request(context As AuditEventContext, operation As String, before As DataRow, name As String, cargoValue As Double) As AuditOperationRequest
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .Context = context.Copy(), .OperationCode = operation,
            .EntityType = "Unit", .EntityId = If(operation = "Unit.Created", "New", If(before Is Nothing, "0", Convert.ToString(before("U_ID"), CultureInfo.InvariantCulture)))}
        If operation <> "Unit.Created" Then r.Inputs.Add(New AuditInput With {.Name = "ExpectedUnitSnapshot", .ValueType = "String", .Value = AuditValue.Capture("String", Fingerprint(before))})
        If operation <> "Unit.Deleted" Then
            r.Inputs.Add(New AuditInput With {.Name = "UnitName", .ValueType = "String", .Value = AuditValue.Capture("String", name)})
            r.Inputs.Add(New AuditInput With {.Name = "UnitCargo", .ValueType = "Float", .Value = AuditValue.Capture("Float", cargoValue)})
        End If
        AuditContract.Request(r) : Return r
    End Function

    Private Shared Sub LockAllocation(cn As SqlConnection, tx As SqlTransaction)
        ' Preserve AA_GET_MAX_UNITS allocation but serialize it and reject exhausted/ambiguous ranges.
        Using cmd As New SqlCommand("SELECT START_ID,END_ID FROM dbo.SERVERS WITH(UPDLOCK,HOLDLOCK)", cn, tx)
            Dim dt As New DataTable()
            Using reader = cmd.ExecuteReader()
                dt.Load(reader)
            End Using
            If dt.Rows.Count <> 1 OrElse IsDBNull(dt.Rows(0)(0)) OrElse IsDBNull(dt.Rows(0)(1)) Then Throw New InvalidOperationException("نطاق ترقيم الوحدات غير محدد بشكل منفرد.")
            Dim first = CInt(dt.Rows(0)(0)), last = CInt(dt.Rows(0)(1))
            cmd.CommandText = "SELECT ISNULL(MAX(U_ID),@First) FROM dbo.Units WITH(UPDLOCK,HOLDLOCK) WHERE U_ID BETWEEN @First AND @Last"
            cmd.Parameters.Add("@First", SqlDbType.Int).Value = first : cmd.Parameters.Add("@Last", SqlDbType.Int).Value = last
            Dim current = CInt(cmd.ExecuteScalar())
            If first >= last OrElse current >= last OrElse current < 2 Then Throw New InvalidOperationException("نطاق ترقيم الوحدات غير صالح أو ممتلئ.")
        End Using
    End Sub

    Friend Shared Function ExecuteInTransaction(cn As SqlConnection, tx As SqlTransaction, request As AuditOperationRequest) As AuditRequestResult
        AuditRequestCoordinator.RequireTransaction(cn, tx)
        If tx.IsolationLevel <> IsolationLevel.Serializable OrElse Not AuditContract.IsUnit(request.OperationCode) Then Throw New ArgumentException("يلزم عقد الوحدة ومعاملة Serializable.")
        Dim lease = AuditRequestCoordinator.Begin(cn, tx, request)
        If Not lease.MustExecute Then Return lease.Result
        Try
            Dim r = lease.Request, create = r.OperationCode = "Unit.Created", delete = r.OperationCode = "Unit.Deleted"
            Permission(cn, tx, r.Context)
            Dim id = If(create, 0, Integer.Parse(r.EntityId, CultureInfo.InvariantCulture)), before As DataRow = Nothing
            If create Then
                LockAllocation(cn, tx)
            Else
                before = Capture(cn, tx, id)
                If Fingerprint(before) <> r.Inputs.Single(Function(x) x.Name = "ExpectedUnitSnapshot").Value.Text Then Throw New InvalidOperationException("تغيرت الوحدة؛ حدّث عرضها قبل المتابعة.")
            End If
            Using cmd = AuditRequestCoordinator.Command(If(create, "dbo.Unit_Insert", If(delete, "dbo.Unit_Delete", "dbo.Unit_Update")), cn, tx)
                cmd.Parameters.Add("@U_ID", SqlDbType.Int).Value = id
                If create Then cmd.Parameters("@U_ID").Direction = ParameterDirection.Output
                If Not delete Then
                    ' Existing procedure accepts 100 characters even though the table allows 250.
                    cmd.Parameters.Add("@U_Name", SqlDbType.NVarChar, 100).Value = r.Inputs.Single(Function(x) x.Name = "UnitName").Value.Text
                    cmd.Parameters.Add("@U_Cargo", SqlDbType.Float).Value = Double.Parse(r.Inputs.Single(Function(x) x.Name = "UnitCargo").Value.Text, CultureInfo.InvariantCulture)
                End If
                cmd.ExecuteNonQuery()
                If create Then id = CInt(cmd.Parameters("@U_ID").Value)
            End Using
            Dim after = Capture(cn, tx, id)
            If If(delete, after IsNot Nothing, after Is Nothing) Then Throw New InvalidOperationException("لم يؤكد الإجراء النتيجة الفعلية للوحدة.")
            Dim item As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = 1, .EventCode = r.OperationCode, .Outcome = 1,
                .EntityType = "Unit", .EntityId = id.ToString(CultureInfo.InvariantCulture), .OccurredAtUtc = AuditRequestCoordinator.ServerUtcNow(cn, tx)}
            For Each definition In AuditContract.UnitFields()
                Dim p = definition.Split("|"c)
                item.Changes.Add(New AuditFieldChange With {.ChangeNo = item.Changes.Count + 1, .EntityType = "Unit", .EntityId = item.EntityId,
                    .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1,
                    .Before = If(before Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), before(p(1)))),
                    .After = If(after Is Nothing, AuditValue.Missing(AuditValueState.Absent), AuditValue.Capture(p(2), after(p(1))))})
            Next
            AuditWriter.WriteInTransaction(cn, tx, lease, item)
            Return New AuditRequestResult With {.Status = "NewPendingCommit", .ResultCode = 1,
                .ResultXml = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", item.EntityId}})}
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
