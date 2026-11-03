Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Globalization
Imports System.Linq

Friend Module AuditReceiptServiceTests
    Private checks As Integer
    Private ReadOnly Inv As CultureInfo = CultureInfo.InvariantCulture
    Private ReadOnly BusinessDate As New DateTime(2026, 10, 27, 10, 30, 0, DateTimeKind.Unspecified)
    Private ReadOnly Occurred As New DateTime(2026, 10, 27, 12, 0, 0, DateTimeKind.Utc)
    Private Sub Check(ok As Boolean, label As String)
        If Not ok Then Throw New Exception("FAIL " & label)
        checks += 1
    End Sub
    Private Sub Reject(work As Action, label As String)
        Try
            work()
        Catch ex As ArgumentException
            Check(True, label) : Return
        Catch ex As InvalidOperationException
            Check(True, label) : Return
        End Try
        Throw New Exception("Expected rejection: " & label)
    End Sub
    Private Function Context() As AuditEventContext
        Return New AuditEventContext With {.SourceId = Guid.NewGuid(), .SessionId = Guid.NewGuid(), .ActorKind = AuditActorKind.User, .ActorUserId = 7, .ActorName = "اختبار", .ScreenCode = "Receipt", .MachineName = "Offline", .ApplicationVersion = "041"}
    End Function
    Private Function Command(op As String, kind As Integer, Optional amount As Decimal = 17.1235D) As SqlCommand
        Dim name As String
        Select Case op
            Case "Receipt.Created" : name = "Agents_BalanceMV_insert_RCT"
            Case "Receipt.Updated" : name = "Agents_BalanceMV_Update_RCT"
            Case "Receipt.EditOpened" : name = "Agents_Balance_MV_RCT_OpenForEdit"
            Case Else : name = "Agents_Balance_MV_RCT_VOID"
        End Select
        Dim cmd As New SqlCommand(name) With {.CommandType = CommandType.StoredProcedure}
        cmd.Parameters.AddWithValue("@T_ID", If(op = "Receipt.Created", 0, 71))
        If op <> "Receipt.Voided" Then cmd.Parameters.AddWithValue("@User_ID", 7)
        If {"Receipt.Created", "Receipt.Updated"}.Contains(op) Then
            For Each p In New Dictionary(Of String, Object) From {{"Date", BusinessDate.AddTicks(12345)}, {"AG_ID", 2}, {"Pr_ID", 0}, {"Receipt_Tran_ID", 0},
                {"Receipt_Title", "إيصال تجريبي"}, {"Pure", amount}, {"Discount", 1.0005D}, {"About", DBNull.Value}, {"BsType_ID", kind}, {"Tr_ID", 3}, {"Pay_ID", 1}}
                cmd.Parameters.AddWithValue("@" & p.Key, p.Value)
            Next
            If op = "Receipt.Created" Then cmd.Parameters.Add("@Receipt_Num", SqlDbType.Int).Direction = ParameterDirection.Output
        End If
        Return cmd
    End Function
    Private Sub SetInput(r As AuditOperationRequest, name As String, value As Object)
        Dim i = r.Inputs.Single(Function(x) x.Name = name)
        i.Value = AuditValue.Capture(i.ValueType, value)
    End Sub
    Private Function Value(r As AuditOperationRequest, name As String) As Object
        Dim i = r.Inputs.Single(Function(x) x.Name = name)
        If i.Value.State = AuditValueState.SqlNull Then Return DBNull.Value
        Select Case i.ValueType
            Case "Integer" : Return Integer.Parse(i.Value.Text, Inv)
            Case "Decimal" : Return Decimal.Round(Decimal.Parse(i.Value.Text, Inv), 3, MidpointRounding.AwayFromZero)
            Case "DateTime" : Return DateTime.Parse(i.Value.Text, Inv, DateTimeStyles.RoundtripKind)
            Case Else : Return i.Value.Text
        End Select
    End Function
    Private Function Request(op As String, kind As Integer, Optional amount As Decimal = 17.1235D) As AuditOperationRequest
        Using cmd = Command(op, kind, amount)
            Dim r = AuditReceipt.Request(Context(), cmd)
            If op = "Receipt.Created" Then
                SetInput(r, "PlannedReceiptId", 71) : SetInput(r, "PlannedReceiptNumber", 82) : SetInput(r, "PlannedTreasuryId", 93)
            End If
            Return r
        End Using
    End Function
    Private Function Table(entity As String) As DataTable
        Dim dt As New DataTable
        For Each d In AuditContract.ReceiptFields(entity)
            Dim p = d.Split("|"c), type As Type
            Select Case p(2)
                Case "Integer" : type = If(p(3) = "bigint", GetType(Long), GetType(Integer))
                Case "Decimal" : type = GetType(Decimal)
                Case "Boolean" : type = GetType(Boolean)
                Case "DateTime" : type = GetType(DateTime)
                Case Else : type = GetType(String)
            End Select
            dt.Columns.Add(p(1), type)
        Next
        Return dt
    End Function
    Private Function Empty() As AuditReceiptSnapshot
        Dim s As New AuditReceiptSnapshot With {.Header = Table("ReceiptHeader"), .Treasury = Table("ReceiptTreasury")}
        Dim refs As New DataTable
        refs.Columns.Add("T_ID", GetType(Integer)) : refs.Columns.Add("Pure", GetType(Decimal)) : refs.Rows.Add(71, 100D)
        s.References("Invoices") = refs : Return s
    End Function
    Private Function Copy(s As AuditReceiptSnapshot) As AuditReceiptSnapshot
        Dim result As New AuditReceiptSnapshot With {.Header = s.Header.Copy(), .Treasury = s.Treasury.Copy()}
        For Each p In s.References
            result.References(p.Key) = p.Value.Copy()
        Next
        Return result
    End Function
    Private Function Created(r As AuditOperationRequest) As AuditReceiptSnapshot
        Dim s = Empty(), h = s.Header.NewRow(), tr = s.Treasury.NewRow(), kind = CInt(Value(r, "BsType_ID"))
        For Each name In {"Receipt_Tran_ID", "AG_ID", "Date", "Receipt_Title", "About", "BsType_ID", "Pure", "Pay_ID", "Tr_ID", "Discount"}
            h(name) = Value(r, name)
        Next
        h("T_ID") = 71 : h("Receipt_Num") = 82 : h("Pr_ID") = DBNull.Value
        h("User_ID") = 7 : h("isDepended") = 1 : h("isVoid") = 0 : h("isPied") = 1 : h("Prt_Counter") = 0
        h("NeedRepost") = False : h("NeedCancelReverse") = False : h("EditVersion") = 0
        s.Header.Rows.Add(h)
        tr("T_ID") = 93 : tr("AGBalance_T_ID") = 71 : tr("Tr_ID") = Value(r, "Tr_ID")
        tr("date") = Value(r, "Date") : tr("notice_move") = Value(r, "Receipt_Title") : tr("Pure") = Value(r, "Pure")
        tr(If(kind = 3, "Credit", "Debit")) = Value(r, "Pure")
        tr("Bank_Name") = Value(r, "Bank_Name") : tr("CheckNum") = Value(r, "CheckNum")
        tr("Tr_Type_ID") = If(kind = 3, 2, 1) : tr("User_ID") = 7 : tr("isVoid") = False
        tr("isFirst_Time") = 0 : tr("NeedRepost") = False : tr("NeedCancelReverse") = False : tr("EditVersion") = 0
        s.Treasury.Rows.Add(tr) : Return s
    End Function
    Private Function Different(type As String) As Object
        Select Case type
            Case "Integer" : Return 999
            Case "Decimal" : Return 999D
            Case "Boolean" : Return True
            Case "DateTime" : Return BusinessDate.AddDays(3)
            Case Else : Return "different"
        End Select
    End Function
    Private Sub ValidateEvents(r As AuditOperationRequest, before As AuditReceiptSnapshot, after As AuditReceiptSnapshot, label As String)
        Dim events = AuditReceipt.Events(r, before, after, Occurred)
        Check(events.Count = after.Treasury.Rows.Count + 1, label & " count")
        Check(events(0).Changes.Count = 47 AndAlso events.Skip(1).All(Function(x) x.Changes.Count = 34), label & " complete fields")
        Check(events.All(Function(x) x.EntityId = "71" AndAlso x.DocumentNumber = "82"), label & " identity")
        For Each e In events
            AuditContract.EventData(r, e)
            Check(AuditMessageFormatter.Format(r, e).Contains("82"), label & " formatter")
        Next
    End Sub
    Private Sub Mutations(r As AuditOperationRequest, before As AuditReceiptSnapshot, after As AuditReceiptSnapshot, create As Boolean)
        For Each entity In {"ReceiptHeader", "ReceiptTreasury"}
            For Each d In AuditContract.ReceiptFields(entity)
                Dim p = d.Split("|"c)
                If Not create AndAlso AuditContract.ReceiptAllowed(r.OperationCode, entity, p(0)) Then Continue For
                Dim bad = Copy(after), table = If(entity = "ReceiptHeader", bad.Header, bad.Treasury)
                table.Rows(0)(p(1)) = Different(p(2))
                If Object.Equals(table.Rows(0)(p(1)), If(entity = "ReceiptHeader", after.Header, after.Treasury).Rows(0)(p(1))) Then Continue For
                Reject(Sub() AuditReceipt.Events(r, before, bad, Occurred), "wrong field " & p(0))
            Next
        Next
    End Sub
    Private Sub LiveVoidRollback(connection As String)
        Dim sourceId As Guid, receiptId As Integer
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using cmd As New SqlCommand("SELECT SourceId FROM dbo.Network_Audit_Source WHERE ConfigId=1", cn)
                sourceId = DirectCast(cmd.ExecuteScalar(), Guid)
            End Using
            Using cmd As New SqlCommand("SELECT TOP(1) r.T_ID FROM dbo.Agents_Balance_MV_RCT r WHERE r.isVoid=0 AND r.BsType_ID IN(3,4) AND ISNULL(r.Custody_ID,0)=0 AND EXISTS(SELECT 1 FROM dbo.Treasury_Balance_MV t WHERE t.AGBalance_T_ID=r.T_ID) AND NOT EXISTS(SELECT 1 FROM dbo.Custody c WHERE c.C_Rcpt_T_ID=r.Receipt_Num) ORDER BY r.T_ID", cn)
                Dim value = cmd.ExecuteScalar()
                If value Is Nothing OrElse IsDBNull(value) Then Throw New InvalidOperationException("No ordinary receipt is available for the rolled-back void test.")
                receiptId = CInt(value)
            End Using
        End Using
        Dim context As New AuditEventContext With {.SourceId = sourceId, .SessionId = Guid.NewGuid(), .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "receipt-admin-rollback", .ScreenCode = "Receipt", .MachineName = "test", .ApplicationVersion = "042"}
        Dim request As AuditOperationRequest
        Using command As New SqlCommand("Agents_Balance_MV_RCT_VOID") With {.CommandType = CommandType.StoredProcedure}
            command.Parameters.Add("@T_ID", SqlDbType.Int).Value = receiptId
            request = AuditReceipt.Request(context, command)
        End Using
        AuditReceipt.Prepare(connection, request)
        Using cn As New SqlConnection(connection)
            cn.Open()
            Using tx = cn.BeginTransaction(IsolationLevel.Serializable)
                Try
                    Dim result = AuditReceipt.ExecuteInTransaction(cn, tx, request)
                    Check(result.ResultCode.GetValueOrDefault() = 1, "administrator receipt void accepted")
                    Using verify As New SqlCommand("SELECT COUNT(*) FROM dbo.Agents_Balance_MV_RCT WHERE T_ID=@id AND isVoid=1", cn, tx)
                        verify.Parameters.Add("@id", SqlDbType.Int).Value = receiptId
                        Check(CInt(verify.ExecuteScalar()) = 1, "receipt void business effect present inside transaction")
                    End Using
                    Using verify As New SqlCommand("SELECT COUNT(*) FROM dbo.Network_Audit_Event WHERE RequestId=@request", cn, tx)
                        verify.Parameters.Add("@request", SqlDbType.UniqueIdentifier).Value = request.RequestId
                        Check(CInt(verify.ExecuteScalar()) >= 2, "receipt void audit events written")
                    End Using
                Finally
                    If tx.Connection IsNot Nothing Then tx.Rollback()
                End Try
            End Using
        End Using
        Using cn As New SqlConnection(connection), verify As New SqlCommand("SELECT (SELECT COUNT(*) FROM dbo.Agents_Balance_MV_RCT WHERE T_ID=@id AND isVoid=0)+(SELECT COUNT(*) FROM dbo.Network_Audit_Request WHERE RequestId=@request)", cn)
            verify.Parameters.Add("@id", SqlDbType.Int).Value = receiptId
            verify.Parameters.Add("@request", SqlDbType.UniqueIdentifier).Value = request.RequestId
            cn.Open()
            Check(CInt(verify.ExecuteScalar()) = 1, "receipt void and audit fixture fully rolled back")
        End Using
    End Sub
    Sub Main(args As String())
        For Each kind In {3, 4}
            For Each amount In {17.1235D, -17.1235D, 17.12345D}
                Dim r = Request("Receipt.Created", kind, amount), before = Empty(), after = Created(r)
                ValidateEvents(r, before, after, "create")
                Check(CDec(after.Header.Rows(0)("Pure")) = Decimal.Round(amount, 3, MidpointRounding.AwayFromZero), "SQL decimal rounding")
                Check(r.Inputs.Single(Function(x) x.Name = "Pure").Value.Text = amount.ToString("G29", Inv), "retain requested precision")
                Check(CDate(after.Header.Rows(0)("Date")) = New SqlDateTime(BusinessDate.AddTicks(12345)).Value, "datetime precision")
                Mutations(r, before, after, True)
            Next
            For Each op In {"Receipt.EditOpened", "Receipt.Updated", "Receipt.Voided"}
                Dim old = Created(Request("Receipt.Created", kind, 5D))
                old.Header.Rows(0)("JournalId") = 100 : old.Header.Rows(0)("EditOpenedAt") = BusinessDate : old.Header.Rows(0)("EditOpenedBy") = 7
                old.Header.Rows(0)("AuditImageLength") = 9000L : old.Header.Rows(0)("AuditImageHash") = New String("A"c, 64)
                old.Treasury.Rows(0)("Bank_Name") = "old bank" : old.Treasury.Rows(0)("CheckNum") = "old check"
                Dim r = Request(op, kind), actual = Copy(old), h = actual.Header.Rows(0)
                Select Case op
                    Case "Receipt.EditOpened"
                        h("OriginalJournalId") = 100 : h("NeedRepost") = True
                        h("EditOpenedAt") = BusinessDate.AddHours(1) : h("EditOpenedBy") = 7
                    Case "Receipt.Updated"
                        SetInput(r, "Tr_ID", 9) : SetInput(r, "Pr_ID", 99) : SetInput(r, "Receipt_Tran_ID", 999)
                        SetInput(r, "Bank_Name", "ignored new bank") : SetInput(r, "CheckNum", "ignored new check")
                        For Each name In {"AG_ID", "Date", "Receipt_Title", "About", "Pure", "Pay_ID", "Discount"}
                            h(name) = Value(r, name)
                        Next
                        If kind = 3 Then h("Tr_ID") = 9
                        h(If(kind = 3, "Debit", "Credit")) = Value(r, "Pure")
                        For Each tr As DataRow In actual.Treasury.Rows
                            tr("Tr_ID") = 9 : tr("date") = Value(r, "Date") : tr("notice_move") = Value(r, "Receipt_Title")
                            tr("Pure") = Value(r, "Pure") : tr(If(kind = 3, "Credit", "Debit")) = Value(r, "Pure")
                        Next
                    Case "Receipt.Voided"
                        h("isVoid") = 1 : h("VoidBy") = 1 : h("VoidReason") = "إلغاء الإيصال" : h("VoidAt") = BusinessDate.AddHours(1)
                        h("NeedCancelReverse") = True : h("NeedRepost") = False : actual.Treasury.Rows(0)("isVoid") = True
                End Select
                ValidateEvents(r, old, actual, op) : Mutations(r, old, actual, False)
                Dim collision = Copy(actual) : collision.References("Invoices").Rows(0)("Pure") = 101D
                Reject(Sub() AuditReceipt.Verify(r, old, collision), "collision/linked invoice changed")
                Dim missing = Copy(actual) : missing.Treasury.Rows.Clear()
                Reject(Sub() AuditReceipt.Verify(r, old, missing), "missing treasury")
                If op = "Receipt.Updated" Then
                    Dim wrongSide = Copy(actual) : wrongSide.Treasury.Rows(0)(If(kind = 3, "Debit", "Credit")) = 1D
                    Reject(Sub() AuditReceipt.Verify(r, old, wrongSide), "wrong treasury direction")
                    wrongSide = Copy(actual) : wrongSide.Header.Rows(0)(If(kind = 3, "Credit", "Debit")) = 1D
                    Reject(Sub() AuditReceipt.Verify(r, old, wrongSide), "wrong receipt direction")
                    If kind = 4 Then
                        wrongSide = Copy(actual) : wrongSide.Header.Rows(0)("Tr_ID") = 9
                        Reject(Sub() AuditReceipt.Verify(r, old, wrongSide), "spend update preserves RCT treasury")
                    End If
                End If
            Next
        Next
        Dim manyRequest = Request("Receipt.EditOpened", 3), manyBefore = Created(Request("Receipt.Created", 3))
        manyBefore.Header.Rows(0)("EditOpenedBy") = 7
        Dim manyAfter = Copy(manyBefore)
        manyAfter.Header.Rows(0)("EditOpenedAt") = BusinessDate : manyAfter.Header.Rows(0)("NeedRepost") = False
        For i = 1 To 98
            Dim prior = manyBefore.Treasury.NewRow(), current = manyAfter.Treasury.NewRow()
            prior.ItemArray = manyBefore.Treasury.Rows(0).ItemArray : current.ItemArray = prior.ItemArray
            prior("T_ID") = 93 + i : current("T_ID") = 93 + i
            manyBefore.Treasury.Rows.Add(prior) : manyAfter.Treasury.Rows.Add(current)
        Next
        Check(AuditReceipt.Events(manyRequest, manyBefore, manyAfter, Occurred).Count = 100, "100-event boundary complete")
        Dim overflowOld = manyBefore.Treasury.NewRow(), overflowNew = manyAfter.Treasury.NewRow()
        overflowOld.ItemArray = manyBefore.Treasury.Rows(0).ItemArray : overflowNew.ItemArray = overflowOld.ItemArray
        overflowOld("T_ID") = 999 : overflowNew("T_ID") = 999
        manyBefore.Treasury.Rows.Add(overflowOld) : manyAfter.Treasury.Rows.Add(overflowNew)
        Reject(Sub() AuditReceipt.Events(manyRequest, manyBefore, manyAfter, Occurred), "no partial success beyond 100 events")
        For Each pair In New Dictionary(Of Integer, String) From {{1, "Sales"}, {7, "Pch"}, {2, "Exp"}, {10, "SB_Rtn"}, {11, "Pch_Rtn"}}
            Check(AuditReceipt.CreationPermission(3, pair.Key) = pair.Value, "linked permission")
        Next
        Check(AuditReceipt.CreationPermission(3, 0) = "Sales" AndAlso AuditReceipt.CreationPermission(4, 0) = "Pch", "standalone permissions")
        Reject(Sub() AuditReceipt.CreationPermission(3, 12), "custody kind") : Reject(Sub() AuditReceipt.CreationPermission(8, 0), "unknown receipt kind")
        Using cmd = Command("Receipt.Created", 3)
            cmd.CommandText = "[dbo].[Agents_BalanceMV_insert_RCT]"
            Check(AuditReceipt.Operation(cmd) = "Receipt.Created", "schema normalized closed command")
            cmd.CommandText = "DELETE dbo.Agents"
            Reject(Sub() AuditReceipt.Operation(cmd), "reject arbitrary command")
        End Using
        Using cmd = Command("Receipt.Created", 3)
            cmd.Parameters.AddWithValue("@Unexpected", 2) : Reject(Sub() AuditReceipt.Request(Context(), cmd), "unknown input")
        End Using
        Using cmd = Command("Receipt.Created", 3)
            cmd.Parameters("@User_ID").Value = 8 : Reject(Sub() AuditReceipt.Request(Context(), cmd), "actor mismatch")
        End Using
        Using cmd = Command("Receipt.Created", 3)
            cmd.Parameters.RemoveAt("@AG_ID") : Reject(Sub() AuditReceipt.Request(Context(), cmd), "missing required input")
        End Using
        Dim nullable = Request("Receipt.Created", 3)
        Check(nullable.Inputs.Single(Function(x) x.Name = "Bank_Name").Value.State = AuditValueState.SqlNull, "optional SQL null preserved")
        Dim fingerprint = Created(nullable), equal = Copy(fingerprint)
        Check(AuditReceipt.Fingerprint(fingerprint) = AuditReceipt.Fingerprint(equal), "stable copied snapshot")
        equal.Header.Rows(0)("About") = ""
        Check(AuditReceipt.Fingerprint(fingerprint) <> AuditReceipt.Fingerprint(equal), "NULL differs from empty")
        equal = Copy(fingerprint) : equal.Header.Rows(0)("Pure") = 17.125D
        Check(AuditReceipt.Fingerprint(fingerprint) <> AuditReceipt.Fingerprint(equal), "money fingerprint")
        equal = Copy(fingerprint) : equal.Header.Rows(0)("Date") = BusinessDate.AddMilliseconds(3)
        Check(AuditReceipt.Fingerprint(fingerprint) <> AuditReceipt.Fingerprint(equal), "date fingerprint")
        equal = Copy(fingerprint) : equal.References("Invoices").Rows(0)("Pure") = 1D
        Check(AuditReceipt.Fingerprint(fingerprint) <> AuditReceipt.Fingerprint(equal), "linked/collision fingerprint")
        Dim liveRollback = args.Length = 2 AndAlso args(0) = "--live-void-rollback"
        If liveRollback Then LiveVoidRollback(args(1))
        Console.WriteLine("PASS " & checks & If(liveRollback, " Receipt service checks including rolled-back administrator void.", " Receipt service checks; no database access."))
    End Sub
End Module
