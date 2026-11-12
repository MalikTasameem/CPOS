Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Reflection

' Exercises the real legacy procedure, snapshots, validators, writer, and completion.
' Only a marked restore is permitted; every business/audit change is rolled back.
Friend NotInheritable Class AuditItemLifecycleLiveTests
    Private Shared checks As Integer
    Private Shared Sub Check(ok As Boolean, label As String)
        If Not ok Then Throw New Exception("FAIL " & label)
        checks += 1
    End Sub
    Private Shared Function CallService(name As String, ParamArray args As Object()) As Object
        Try
            Return GetType(AuditItemLifecycle).GetMethod(name, BindingFlags.Static Or BindingFlags.NonPublic).Invoke(Nothing, args)
        Catch ex As TargetInvocationException
            Throw ex.InnerException
        End Try
    End Function
    Private Shared Function Q(cn As SqlConnection, tx As SqlTransaction, sql As String) As Object
        Using cmd As New SqlCommand(sql, cn, tx)
            Return cmd.ExecuteScalar()
        End Using
    End Function
    Private Shared Function Command(op As String, itemId As Integer, unitId As Integer, groupId As Integer, companyId As Integer) As SqlCommand
        Dim cmd As New SqlCommand(If(op = "Item.PlaceholderAcquired", "item_menu_insert", If(op = "Item.QuickSaved", "ADD_IM_Not_Exisesit", If(op = "Item.FullSaved", "item_menu_update", "item_menu_Delete_DisableRows"))))
        cmd.Parameters.AddWithValue("@IM_ID", itemId)
        For Each definition In AuditContract.ItemLifecycleInputs(op)
            Dim p = definition.Split("|"c), value As Object = DBNull.Value
            If p(1).Length = 0 Then Continue For
            If p(3) = "R" Then
                Select Case p(2)
                    Case "Integer" : value = 0
                    Case "Boolean" : value = True
                    Case "Float" : value = 6.528R
                    Case "String" : value = ""
                End Select
            End If
            Select Case p(0)
                Case "ItemName", "ItemSalesName" : value = "صنف اختبار التاريخ 046"
                Case "ItemStoreType" : value = 1
                Case "ItemGroupId" : value = groupId
                Case "ItemCompanyGroup" : value = companyId
                Case "DefaultUnitId", "SelectedUnitId" : value = unitId
            End Select
            cmd.Parameters.AddWithValue("@" & p(1), value)
        Next
        Return cmd
    End Function
    Private Shared Function BusinessDigest(cn As SqlConnection) As String
        Dim tables = {"IM_Menu", "IM_Units", "IM_QTY_ALERT", "IM_Formating_Menu", "ST_QTY_Deferences", "ST_Balance", "Units", "Network_Audit_Request", "Network_Audit_Event", "Network_Audit_Change", "Network_Audit_Source"}
        Dim hashes As New List(Of String)
        For Each table In tables
            Dim dt = AuditSalesQuantity.Rows(cn, Nothing, "SELECT * FROM dbo.[" & table & "] ORDER BY 1")
            Dim values As New List(Of String) From {table, dt.Rows.Count.ToString(CultureInfo.InvariantCulture)}
            For Each row As DataRow In dt.Rows
                For Each column As DataColumn In dt.Columns
                    Dim value = row(column)
                    values.Add(column.ColumnName)
                    values.Add(If(IsDBNull(value), Nothing, If(TypeOf value Is Byte(), Convert.ToBase64String(DirectCast(value, Byte())), Convert.ToString(value, CultureInfo.InvariantCulture))))
                Next
            Next
            hashes.Add(AuditItemLifecycle.Digest(AuditCanonical.Encode(values)))
        Next
        Return AuditItemLifecycle.Digest(AuditCanonical.Encode(hashes))
    End Function
    Friend Shared Sub Run(connection As String)
        Dim builder As New SqlConnectionStringBuilder(connection)
        If Not System.Text.RegularExpressions.Regex.IsMatch(builder.InitialCatalog, "\ACPOS_AUDIT_GATE_[a-f0-9]{32}\z") Then Throw New Exception("Production database refused.")
        builder.Pooling = False
        connection = builder.ConnectionString
        Dim context As AuditEventContext, digest As String, originalWriter As String, itemId As Integer, unitId As Integer, groupId As Integer, companyId As Integer, newUser As Integer
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.extended_properties WHERE class=0 AND name=N'CPOS.AuditReleaseSandbox' AND CONVERT(nvarchar(100),value)=DB_NAME()")) = 1, "sandbox ownership")
            Check(CInt(Q(cn, Nothing, "SELECT COUNT(*) FROM sys.dm_exec_sessions WHERE is_user_process=1 AND session_id<>@@SPID AND database_id=DB_ID()")) = 0, "no other sandbox clients")
            itemId = CInt(Q(cn, Nothing, "SELECT MIN(IM_ID) FROM dbo.IM_Menu WHERE Row_Enabled=0 AND User_ID=1"))
            newUser = CInt(Q(cn, Nothing, "SELECT MIN(u.User_ID) FROM dbo.Users u JOIN dbo.Users_Validations_V v ON v.User_ID=u.User_ID WHERE u.is_Allow=1 AND v.IM=1 AND NOT EXISTS(SELECT 1 FROM dbo.IM_Menu m WHERE m.User_ID=u.User_ID AND m.Row_Enabled=0)"))
            unitId = CInt(Q(cn, Nothing, "SELECT MIN(U_ID) FROM dbo.Units WHERE U_Cargo=1"))
            groupId = CInt(Q(cn, Nothing, "SELECT MIN(GM_ID) FROM dbo.General_Menu"))
            companyId = CInt(Q(cn, Nothing, "SELECT MIN(Grp_ID) FROM dbo.Comp_Groups"))
            context = New AuditEventContext With {.SourceId = CType(Q(cn, Nothing, "SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1"), Guid), .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "Audit046Test", .SessionId = Guid.NewGuid(), .MachineName = "Audit046Test", .ApplicationVersion = "046", .ScreenCode = "IM_ADD_New"}
            originalWriter = CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))"))
            digest = BusinessDigest(cn)
        End Using
        For Each mode In {"reuse", "new", "quick", "full", "cleanup", "tamper-date", "tamper-name", "tamper-range-low", "tamper-range-high"}
            Dim op = If(mode = "new" OrElse mode = "reuse" OrElse mode.StartsWith("tamper-"), "Item.PlaceholderAcquired", If(mode = "quick", "Item.QuickSaved", If(mode = "full", "Item.FullSaved", "Item.DisabledRowsRemoved")))
            Dim ctx = context.Copy()
            If mode = "new" Then ctx.ActorUserId = newUser
            If mode = "full" Then ctx.ScreenCode = "ItemsMenu"
            Dim prepared As AuditItemLifecycle.Prepared
            Using cmd = Command(op, itemId, unitId, groupId, companyId)
                prepared = AuditItemLifecycle.Prepare(connection, ctx, cmd, If(mode = "full", "", "Pch_IM_card_11"))
            End Using
            Check(prepared IsNot Nothing, "prepared " & mode)
            Dim r = prepared.Request, target = CInt(r.Inputs.Single(Function(x) x.Name = "TargetItemId").Value.Text)
            Using cn As New SqlConnection(connection)
                cn.Open()
                Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                    Try
                        prepared.VerifyRequest(r)
                        CallService("Guard", cn, tx)
                        CallService("Permission", cn, tx, r.Context, op, prepared.QuickEntryScreen)
                        Dim before = DirectCast(CallService("Snapshot", cn, tx, op, target, ctx.ActorUserId.Value), Dictionary(Of String, DataTable))
                        Check(AuditItemLifecycle.Fingerprint(before) = r.Inputs.Single(Function(x) x.Name = "ExpectedLifecycleSnapshot").Value.Text, "snapshot " & mode)
                        Dim lease = AuditRequestCoordinator.Begin(cn, tx, r)
                        CallService("ValidateBefore", cn, tx, r, before)
                        CallService("LockUnits", cn, tx, r, before)
                        CallService("ExecuteCommand", cn, tx, r, prepared.Photo)
                        AuditRequestCoordinator.RequireTransaction(cn, tx)
                        Dim after = DirectCast(CallService("Snapshot", cn, tx, op, target, ctx.ActorUserId.Value), Dictionary(Of String, DataTable))
                        CallService("VerifyResult", r, before, after)
                        Dim events = AuditItemLifecycle.Events(r, before, after, AuditRequestCoordinator.ServerUtcNow(cn, tx))
                        If mode.StartsWith("tamper-") Then
                            Dim field = If(mode = "tamper-name", "ItemName", "ItemDate")
                            Dim change = events(0).Changes.Single(Function(x) x.FieldCode = field)
                            change.After = If(field = "ItemDate", AuditValue.Capture("DateTime", New DateTime(2025, 1, 1)), AuditValue.Capture("String", "incorrect"))
                            If mode = "tamper-range-low" Then change.After = AuditValue.Capture("DateTime", DateTime.MinValue)
                            If mode = "tamper-range-high" Then change.After = AuditValue.Capture("DateTime", DateTime.MaxValue)
                            Dim rejected As Boolean
                            Try
                                AuditWriter.WriteInTransaction(cn, tx, lease, events(0))
                            Catch ex As SqlException
                                If Not ex.Message.Contains("CatalogItemRecord after-state differs") Then Throw
                                rejected = True
                            End Try
                            Check(rejected, "stored-state protection " & mode)
                        Else
                            For Each auditEvent In events
                                AuditWriter.WriteInTransaction(cn, tx, lease, auditEvent)
                            Next
                            Dim result = AuditRequestCoordinator.Complete(cn, tx, lease, 1, New Dictionary(Of String, String) From {{"EntityId", If(target = 0, r.EntityId, target.ToString(CultureInfo.InvariantCulture))}})
                            Check(result.Length > 0, "complete " & mode)
                            Check(CInt(Q(cn, tx, "SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId='" & r.RequestId.ToString() & "'")) = events.Count, "events " & mode)
                            Check(Not AuditRequestCoordinator.Begin(cn, tx, r).MustExecute, "idempotent replay " & mode)
                        End If
                    Finally
                        If tx.Connection IsNot Nothing Then tx.Rollback()
                    End Try
                End Using
            End Using
        Next
        Using cn As New SqlConnection(connection)
            cn.Open()
            Check(BusinessDigest(cn) = digest, "business and audit rows unchanged after rollback")
            Check(CStr(Q(cn, Nothing, "SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.Network_Audit_Event_Write'))")) = originalWriter, "writer definition unchanged")
        End Using
        Console.WriteLine("PASS " & checks & " item-lifecycle SQL checks; every business/audit change rolled back on owned sandbox.")
    End Sub
End Class
