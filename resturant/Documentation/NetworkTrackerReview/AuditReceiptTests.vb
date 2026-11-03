Option Strict On
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq

Friend Module AuditReceiptTests
    Private checks As Integer
    Private ReadOnly businessDate As New DateTime(2026, 10, 27, 10, 30, 0, DateTimeKind.Unspecified)
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
    Private Function Request(op As String, kind As Integer) As AuditOperationRequest
        Dim r As New AuditOperationRequest With {.RequestId = Guid.NewGuid(), .OperationCode = op, .EntityType = "Receipt", .EntityId = If(op = "Receipt.Created", "New", "71"), .Context = New AuditEventContext With {.SourceId = Guid.NewGuid(), .SessionId = Guid.NewGuid(), .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "اختبار", .ScreenCode = "Receipt", .MachineName = "Offline", .ApplicationVersion = "041"}}
        Dim values As New Dictionary(Of String, Object) From {{"ExpectedReceipt", New String("A"c, 64)}, {"PlannedReceiptId", 71}, {"PlannedReceiptNumber", 82}, {"PlannedTreasuryId", 93}, {"Date", businessDate}, {"AG_ID", 2}, {"Pr_ID", DBNull.Value}, {"Receipt_Tran_ID", 0}, {"Receipt_Title", "إيصال تجريبي"}, {"Pure", 17.12345D}, {"Discount", 0D}, {"About", DBNull.Value}, {"BsType_ID", kind}, {"Tr_ID", 3}, {"Bank_Name", DBNull.Value}, {"CheckNum", DBNull.Value}, {"Pay_ID", 1}}
        For Each d In AuditContract.ReceiptInputs(op)
            Dim p = d.Split("|"c)
            r.Inputs.Add(New AuditInput With {.Name = p(0), .ValueType = p(2), .Value = AuditValue.Capture(p(2), values(p(0)))})
        Next
        Return r
    End Function
    Private Function ValueFor(type As String, value As Integer) As AuditValue
        Select Case type
            Case "Integer" : Return AuditValue.Capture(type, value)
            Case "Decimal" : Return AuditValue.Capture(type, CDec(value))
            Case "Boolean" : Return AuditValue.Capture(type, value <> 0)
            Case "DateTime" : Return AuditValue.Capture(type, businessDate.AddDays(value))
            Case Else : Return AuditValue.Capture(type, value.ToString(CultureInfo.InvariantCulture))
        End Select
    End Function
    Private Function EventFor(r As AuditOperationRequest, entity As String, kind As Integer) As AuditEventRequest
        Dim header = entity = "ReceiptHeader", create = r.OperationCode = "Receipt.Created"
        Dim e As New AuditEventRequest With {.EventId = Guid.NewGuid(), .EventOrdinal = If(header, 1, 2), .EventCode = r.OperationCode, .Outcome = 1, .EntityType = "Receipt", .EntityId = "71", .DocumentNumber = "82", .OccurredAtUtc = New DateTime(2026, 10, 27, 12, 0, 0, DateTimeKind.Utc), .BusinessDate = businessDate}
        For Each d In AuditContract.ReceiptFields(entity)
            Dim p = d.Split("|"c), actual = AuditValue.Missing(AuditValueState.SqlNull)
            If header Then
                Dim input = r.Inputs.SingleOrDefault(Function(x) x.Name = p(1))
                If input IsNot Nothing Then actual = input.Value
                Select Case p(1)
                    Case "T_ID" : actual = AuditValue.Capture("Integer", 71)
                    Case "Receipt_Num" : actual = AuditValue.Capture("Integer", 82)
                    Case "BsType_ID" : actual = AuditValue.Capture("Integer", kind)
                    Case "User_ID", "EditOpenedBy", "isDepended" : actual = AuditValue.Capture("Integer", 1)
                    Case "Date" : actual = AuditValue.Capture("DateTime", businessDate)
                    Case "isVoid" : actual = AuditValue.Capture("Integer", If(r.OperationCode = "Receipt.Voided", 1, 0))
                    Case "Pure" : actual = AuditValue.Capture("Decimal", 17.123D)
                End Select
            Else
                Select Case p(1)
                    Case "T_ID" : actual = AuditValue.Capture("Integer", 93)
                    Case "AGBalance_T_ID" : actual = AuditValue.Capture("Integer", 71)
                    Case "Tr_ID" : actual = AuditValue.Capture("Integer", 3)
                    Case "Tr_Type_ID" : actual = AuditValue.Capture("Integer", If(kind = 3, 2, 1))
                    Case "User_ID" : actual = AuditValue.Capture("Integer", 1)
                    Case "isVoid" : actual = AuditValue.Capture("Boolean", r.OperationCode = "Receipt.Voided")
                    Case "Pure" : actual = AuditValue.Capture("Decimal", 17.123D)
                    Case "date" : actual = AuditValue.Capture("DateTime", businessDate)
                End Select
            End If
            e.Changes.Add(New AuditFieldChange With {.ChangeNo = e.Changes.Count + 1, .EntityType = entity, .EntityId = If(header, "71", "93"), .FieldCode = p(0), .ValueType = p(2), .SourceSqlType = p(3), .ValueOrigin = 1, .Before = If(create, AuditValue.Missing(AuditValueState.Absent), actual), .After = actual})
        Next
        Return e
    End Function
    Private Sub Input(r As AuditOperationRequest, name As String, value As Object)
        Dim v = r.Inputs.Single(Function(x) x.Name = name)
        v.Value = AuditValue.Capture(v.ValueType, value)
    End Sub
    Sub Main()
        If Environment.GetCommandLineArgs().Contains("catalog") Then
            For Each entity In {"ReceiptHeader", "ReceiptTreasury"}
                For Each field In AuditContract.ReceiptFields(entity)
                    Console.WriteLine(entity & "|" & field)
                Next
            Next
            Return
        End If
        For Each op In {"Receipt.Created", "Receipt.EditOpened", "Receipt.Updated", "Receipt.Voided"}
            For Each kind In {3, 4}
                Dim r = Request(op, kind)
                AuditContract.Request(r) : Check(AuditContract.Domain(op) = "Finance.Receipts", "request/domain " & op)
                Dim bytes = AuditCanonical.RequestBytes(r)
                Check(bytes.SequenceEqual(AuditCanonical.RequestBytes(r)), "stable request fingerprint")
                For Each entity In {"ReceiptHeader", "ReceiptTreasury"}
                    Dim e = EventFor(r, entity, kind)
                    AuditContract.EventData(r, e) : Check(True, "complete event " & op & entity)
                    Check(AuditMessageFormatter.Format(r, e).Contains("82"), "message document identity")
                    For Each d In AuditContract.ReceiptFields(entity)
                        Dim p = d.Split("|"c), bad = EventFor(r, entity, kind)
                        bad.Changes.Remove(bad.Changes.Single(Function(x) x.FieldCode = p(0)))
                        Reject(Sub() AuditContract.EventData(r, bad), "missing " & p(0))
                        If Not AuditContract.ReceiptAllowed(op, entity, p(0)) Then
                            bad = EventFor(r, entity, kind)
                            Dim c = bad.Changes.Single(Function(x) x.FieldCode = p(0))
                            c.Before = ValueFor(c.ValueType, 99)
                            Reject(Sub() AuditContract.EventData(r, bad), "unauthorized change " & p(0))
                        End If
                    Next
                    Dim malformed = EventFor(r, entity, kind)
                    malformed.Changes(0).ValueOrigin = 2
                    Reject(Sub() AuditContract.EventData(r, malformed), "untrusted value origin")
                    malformed = EventFor(r, entity, kind) : malformed.Changes(0).Before = AuditValue.Missing(AuditValueState.NotCaptured)
                    Reject(Sub() AuditContract.EventData(r, malformed), "missing before snapshot")
                    malformed = EventFor(r, entity, kind) : malformed.Changes(0).SourceSqlType = "float"
                    Reject(Sub() AuditContract.EventData(r, malformed), "wrong SQL type")
                Next
                Dim wrong = Request(op, kind) : wrong.Context.ScreenCode = "Custody"
                Reject(Sub() AuditContract.Request(wrong), "custody screen excluded")
                wrong = Request(op, kind) : wrong.Inputs.RemoveAt(0)
                Reject(Sub() AuditContract.Request(wrong), "missing expected snapshot")
                Dim unrelated = EventFor(r, "ReceiptTreasury", kind)
                unrelated.Changes.Single(Function(x) x.FieldCode = "RctTr_AGBalance_T_ID").After = AuditValue.Capture("Integer", 999)
                Reject(Sub() AuditContract.EventData(r, unrelated), "unrelated treasury receipt")
                Dim custody = EventFor(r, "ReceiptHeader", kind)
                custody.Changes.Single(Function(x) x.FieldCode = "Rct_Custody_ID").After = AuditValue.Capture("Integer", 9)
                Reject(Sub() AuditContract.EventData(r, custody), "custody linked receipt excluded")
            Next
        Next
        For Each name In {"Bank_Name", "CheckNum", "Receipt_Title"}
            Dim r = Request("Receipt.Created", 3)
            Input(r, name, New String("x"c, If(name = "Receipt_Title", 551, 101)))
            Reject(Sub() AuditContract.Request(r), "oversized " & name)
        Next
        For Each amount In {0D, 0.0004D, 999999999999999.9999D}
            Dim r = Request("Receipt.Created", 3) : Input(r, "Pure", amount)
            Reject(Sub() AuditContract.Request(r), "zero/overflow amount")
        Next
        For Each kind In {1, 2, 7, 12}
            Dim r = Request("Receipt.Created", 3) : Input(r, "BsType_ID", kind)
            Reject(Sub() AuditContract.Request(r), "non receipt kind")
        Next
        Dim negative = Request("Receipt.Created", 3) : Input(negative, "Pure", -5D)
        AuditContract.Request(negative) : Check(True, "existing signed amount semantics preserved")
        Dim badDate = Request("Receipt.Created", 3) : Input(badDate, "Date", New DateTime(1700, 1, 1))
        Reject(Sub() AuditContract.Request(badDate), "datetime lower bound")
        Console.WriteLine("PASS " & checks & " ordinary Receipt contract checks; no database access.")
    End Sub
End Module
