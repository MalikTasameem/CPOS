Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Text

Module AuditPeopleCardsTests
    Private checks As Integer
    Private Sub Check(value As Boolean, message As String)
        If Not value Then Throw New Exception(message)
        checks += 1
    End Sub
    Private Sub Reject(work As Action, message As String)
        Try
            work()
        Catch ex As ArgumentException
            checks += 1 : Return
        Catch ex As InvalidOperationException
            checks += 1 : Return
        End Try
        Throw New Exception("Accepted " & message)
    End Sub
    Private Function ContextFor(screen As String) As AuditEventContext
        Return New AuditEventContext With {.SourceId = Guid.NewGuid(), .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "tester", .SessionId = Guid.NewGuid(), .ScreenCode = screen, .MachineName = "test", .ApplicationVersion = "test"}
    End Function
    Private Function Table(entity As String, exists As Boolean) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Id", GetType(Integer))
        For Each d In AuditContract.PeopleFields(entity)
            Dim p = d.Split("|"c), t As Type
            Select Case p(2)
                Case "Integer"
                    Select Case p(3)
                        Case "tinyint" : t = GetType(Byte)
                        Case "smallint" : t = GetType(Short)
                        Case "bigint" : t = GetType(Long)
                        Case Else : t = GetType(Integer)
                    End Select
                Case "Decimal" : t = GetType(Decimal)
                Case "Float" : t = GetType(Double)
                Case "Boolean" : t = GetType(Boolean)
                Case "Date", "DateTime" : t = GetType(DateTime)
                Case Else : t = GetType(String)
            End Select
            dt.Columns.Add(p(1), t)
        Next
        If exists Then
            Dim row = dt.NewRow() : row(0) = 101 : dt.Rows.Add(row)
        End If
        Return dt
    End Function
    Private Function Snapshot(op As String, exists As Boolean) As Dictionary(Of String, DataTable)
        Dim data As New Dictionary(Of String, DataTable)(StringComparer.Ordinal)
        Dim main = If(op.StartsWith("Agent.", StringComparison.Ordinal), "PeopleAgent", "PeopleUser")
        For Each entity In AuditContract.PeopleEntities(op).Where(Function(x) x <> "PeopleCredentialAction")
            data.Add(entity, Table(entity, exists AndAlso entity = main))
        Next
        Return data
    End Function
    Private Function Command(op As String) As SqlCommand
        Dim cmd As New SqlCommand()
        Select Case op
            Case "Agent.Created" : cmd.CommandText = "Agents_insert"
            Case "Agent.Updated" : cmd.CommandText = "Agents_Update"
            Case "Agent.Deleted" : cmd.CommandText = "Agents_Delete"
            Case "User.Created" : cmd.CommandText = "User_Insert"
            Case "User.Updated" : cmd.CommandText = "User_Update"
            Case "User.Deleted" : cmd.CommandText = "User_Delete"
            Case "User.CredentialsChanged" : cmd.CommandText = "User_UpdatePass"
        End Select
        cmd.Parameters.AddWithValue("@AG_ID", 101)
        cmd.Parameters.AddWithValue("@user_id", 101)
        cmd.Parameters.AddWithValue("@Ag_name", "عميل")
        cmd.Parameters.AddWithValue("@Type_ID", 2)
        cmd.Parameters.AddWithValue("@E_mail", "")
        cmd.Parameters.AddWithValue("@UserName", "موظف")
        cmd.Parameters.AddWithValue("@is_Allow", 1)
        cmd.Parameters.AddWithValue("@Valid_ID", 1)
        cmd.Parameters.AddWithValue("@Tr_ID", 1)
        cmd.Parameters.AddWithValue("@AG_img", New Byte() {1, 2, 3})
        cmd.Parameters.AddWithValue("@UserPass", "SECRET-encrypted-123")
        cmd.Parameters.AddWithValue("@WEB_PASS", "SECRET-plain-456")
        cmd.Parameters.AddWithValue("@NewPass", "SECRET-new-789")
        Return cmd
    End Function
    Private Sub LiveRead(connection As String)
        Dim sourceId As Guid
        Using cn As New SqlConnection(connection), cmd As New SqlCommand("SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1", cn)
            cn.Open()
            sourceId = DirectCast(cmd.ExecuteScalar(), Guid)
        End Using
        For Each screen In {"Agent", "AddFastAgent", "Sales", "Pch", "ViewBill"}
            Using cmd = Command("Agent.Created"), envelope As New AuditPeopleEnvelope(cmd)
                cmd.Parameters("@Type_ID").Value = If(screen = "Pch", 4, 2)
                Dim context As New AuditEventContext With {.SourceId = sourceId, .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "live-read-test", .SessionId = Guid.NewGuid(), .ScreenCode = screen, .MachineName = "test", .ApplicationVersion = "test"}
                Dim before = AuditPeopleCards.Read(connection, context, cmd, envelope)
                Dim request = AuditPeopleCards.Request(context, cmd, before, envelope)
                AuditContract.Request(request)
                Check(request.OperationCode = "Agent.Created", "Live create request from " & screen)
            End Using
        Next
    End Sub

    Private Sub LiveRollback(connection As String)
        Dim sourceId As Guid
        Using cn As New SqlConnection(connection), cmd As New SqlCommand("SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1", cn)
            cn.Open()
            sourceId = DirectCast(cmd.ExecuteScalar(), Guid)
        End Using
        Dim uniqueName = "AUDIT-USER-ROLLBACK-" & Guid.NewGuid().ToString("N")
        Using userCommand = Command("User.Created")
            userCommand.Parameters("@UserName").Value = uniqueName
            userCommand.Parameters("@UserPass").Value = "AUDIT-SECRET-" & Guid.NewGuid().ToString("N")
            userCommand.Parameters("@WEB_PASS").Value = "AUDIT-WEB-" & Guid.NewGuid().ToString("N")
            userCommand.Parameters("@AG_ID").Value = DBNull.Value
            userCommand.Parameters("@Tr_ID").Value = 0
            Using envelope As New AuditPeopleEnvelope(userCommand)
            Dim context As New AuditEventContext With {.SourceId = sourceId, .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "live-rollback-test", .SessionId = Guid.NewGuid(), .ScreenCode = "users", .MachineName = "test", .ApplicationVersion = "test"}
            Dim before = AuditPeopleCards.Read(connection, context, userCommand, envelope)
            Dim request = AuditPeopleCards.Request(context, userCommand, before, envelope)
            Using cn As New SqlConnection(connection)
                cn.Open()
                Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                    Try
                        Dim result = AuditPeopleCards.ExecuteInTransaction(cn, tx, request, envelope)
                        Dim savedId = Integer.Parse(AuditCanonical.ParseXml(result.ResultXml).Element("Values").Elements("Value").Single(Function(x) CStr(x.Attribute("name")) = "EntityId").Value, CultureInfo.InvariantCulture)
                        Using verify As New SqlCommand("SELECT COUNT(*) FROM dbo.Users WHERE User_ID=@id AND UserName=@name AND Tr_ID=0", cn, tx)
                            verify.Parameters.Add("@id", SqlDbType.Int).Value = savedId
                            verify.Parameters.Add("@name", SqlDbType.NVarChar, 200).Value = uniqueName
                            Check(CInt(verify.ExecuteScalar()) = 1, "User save accepts system default treasury sentinel")
                        End Using
                        Using verify As New SqlCommand("SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId=@request", cn, tx)
                            verify.Parameters.Add("@request", SqlDbType.UniqueIdentifier).Value = request.RequestId
                            Check(CInt(verify.ExecuteScalar()) >= 2, "User save writes card and credential audit events")
                        End Using
                    Finally
                        If tx.Connection IsNot Nothing Then tx.Rollback()
                    End Try
                End Using
            End Using
            Using cn As New SqlConnection(connection), verify As New SqlCommand("SELECT (SELECT COUNT(*) FROM dbo.Users WHERE UserName=@name)+(SELECT COUNT(*) FROM dbo.Network_Audit_Request WHERE RequestId=@request)", cn)
                verify.Parameters.Add("@name", SqlDbType.NVarChar, 200).Value = uniqueName
                verify.Parameters.Add("@request", SqlDbType.UniqueIdentifier).Value = request.RequestId
                cn.Open()
                Check(CInt(verify.ExecuteScalar()) = 0, "User and audit fixture fully rolled back")
            End Using
            End Using
        End Using
    End Sub

    Sub Main(args As String())
        For Each op In {"Agent.Created", "Agent.Updated", "Agent.Deleted", "User.Created", "User.Updated", "User.Deleted", "User.CredentialsChanged"}
            Dim create = op.EndsWith(".Created", StringComparison.Ordinal), delete = op.EndsWith(".Deleted", StringComparison.Ordinal), agent = op.StartsWith("Agent.", StringComparison.Ordinal)
            Dim before = Snapshot(op, Not create), after = Snapshot(op, Not delete)
            Using cmd = Command(op), envelope As New AuditPeopleEnvelope(cmd)
                Dim r = AuditPeopleCards.Request(ContextFor(If(agent, "Agent", If(op = "User.CredentialsChanged", "UpdateUserPass", "users"))), cmd, before, envelope)
                AuditContract.Request(r) : envelope.Verify(r) : checks += 2
                Dim canonical = Encoding.UTF8.GetString(AuditCanonical.RequestBytes(r))
                Check(Not canonical.Contains("SECRET"), "Credential appeared in request")
                Check(AuditContract.Domain(op) = "People.Cards", "Domain")
                Check(r.Inputs.Count <= 32, "Too many inputs")
                Dim events = AuditPeopleCards.BuildEvents(r, before, after, 101, New DateTime(2026, 10, 27, 0, 0, 0, DateTimeKind.Utc))
                For Each e In events
                    AuditContract.EventData(r, e) : checks += 1
                    Check(Not AuditCanonical.ChangesXml(e).Contains("SECRET"), "Credential appeared in changes")
                    Dim change = e.Changes(0), oldField = change.FieldCode
                    change.FieldCode = "UserPass"
                    Reject(Sub() AuditContract.EventData(r, e), "credential field")
                    change.FieldCode = oldField
                    Dim oldOrigin = change.ValueOrigin
                    change.ValueOrigin = 2
                    Reject(Sub() AuditContract.EventData(r, e), "input-origin row")
                    change.ValueOrigin = oldOrigin
                Next
                Dim first = r.Inputs(0).Value
                r.Inputs(0).Value = AuditValue.Capture("String", New String("A"c, 64))
                Reject(Sub() envelope.Verify(r), "mutated envelope binding")
                r.Inputs(0).Value = first
                Dim oldScreen = r.Context.ScreenCode
                r.Context.ScreenCode = "Receipt"
                Reject(Sub() AuditContract.Request(r), "wrong screen")
                r.Context.ScreenCode = oldScreen
                r.Inputs.Add(New AuditInput With {.Name = "Token", .ValueType = "String", .Value = AuditValue.Capture("String", "anything")})
                Reject(Sub() AuditContract.Request(r), "token input")
                r.Inputs.RemoveAt(r.Inputs.Count - 1)
                Dim main = events(0)
                main.Changes.RemoveAt(0)
                Reject(Sub() AuditContract.EventData(r, main), "missing row field")
            End Using
        Next
        ' CPOS stores zero in Users.Tr_ID to mean the system default treasury.
        Using cmd = Command("User.Created"), envelope As New AuditPeopleEnvelope(cmd)
            cmd.Parameters("@Tr_ID").Value = 0
            Dim before = Snapshot("User.Created", False)
            Dim request = AuditPeopleCards.Request(ContextFor("users"), cmd, before, envelope)
            Check(request.Inputs.Single(Function(x) x.Name = "UserTreasury").Value.Text = "0", "System default treasury sentinel accepted")
            request.Inputs.Single(Function(x) x.Name = "UserTreasury").Value = AuditValue.Capture("Integer", -1)
            Reject(Sub() AuditContract.Request(request), "negative user treasury reference")
        End Using
        ' Related accounting rows must stay complete and explicitly typed.
        Using cmd = Command("Agent.Created"), envelope As New AuditPeopleEnvelope(cmd)
            Dim before = Snapshot("Agent.Created", False), after = Snapshot("Agent.Created", True)
            For Each name In {"PeopleAccountLink", "PeopleChartAccount"}
                Dim row = after(name).NewRow() : row(0) = 7 : after(name).Rows.Add(row)
            Next
            after("PeopleAccountLink").Rows(0)("ENTITY_TYPE") = CByte(1)
            after("PeopleChartAccount").Rows(0)("AccountSourceType") = CByte(1)
            Check(AuditPeopleCards.Fingerprint(after).Length = 64, "SQL tinyint snapshot fingerprint")
            Dim r = AuditPeopleCards.Request(ContextFor("Agent"), cmd, before, envelope)
            Dim events = AuditPeopleCards.BuildEvents(r, before, after, 101, New DateTime(2026, 10, 27, 0, 0, 0, DateTimeKind.Utc))
            Check(events.Count = 3, "Missing accounting events")
            For Each e In events
                AuditContract.EventData(r, e) : checks += 1
            Next
        End Using
        Dim hasLiveRead = args.Length = 2 AndAlso args(0) = "--live-read"
        Dim hasLiveRollback = args.Length = 2 AndAlso args(0) = "--live-rollback"
        If hasLiveRead Then LiveRead(args(1))
        If hasLiveRollback Then LiveRollback(args(1))
        Console.WriteLine("PASS " & checks.ToString(CultureInfo.InvariantCulture) & If(hasLiveRollback, " people-card checks including rolled-back user-save coverage.", If(hasLiveRead, " people-card checks including read-only deployed-schema coverage.", " people-card offline checks; no database access.")))
    End Sub
End Module
