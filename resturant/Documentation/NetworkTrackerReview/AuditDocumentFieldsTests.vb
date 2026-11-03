Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Globalization
Imports System.Linq

' Offline only: no connection strings, database writes or deployment are performed by this suite.
Friend Module AuditDocumentFieldsTests
    Private checks As Integer
    Private ReadOnly utc As New DateTime(2026, 10, 27, 12, 0, 0, DateTimeKind.Utc)
    Private Function Context(screen As String) As AuditEventContext
        Return New AuditEventContext With {.SourceId = Guid.NewGuid(), .SessionId = Guid.NewGuid(), .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "اختبار", .ScreenCode = screen, .MachineName = "OfflineTest", .ApplicationVersion = "032"}
    End Function
    Private Function Clone(r As AuditOperationRequest) As AuditOperationRequest
        Dim result As New AuditOperationRequest With {.RequestId = r.RequestId, .Context = r.Context.Copy(), .OperationCode = r.OperationCode, .EntityId = r.EntityId, .EntityType = r.EntityType}
        For Each i In r.Inputs
            result.Inputs.Add(New AuditInput With {.Name = i.Name, .ValueType = i.ValueType, .Value = i.Value})
        Next
        Return result
    End Function
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
    Private Function Header(kind As Integer) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("T_ID", GetType(Integer))
        Dim row = dt.NewRow() : row("T_ID") = 50
        For Each definition In AuditContract.DocumentHeaderFields()
            Dim p = definition.Split("|"c), type As Type = Nothing, value As Object = Nothing
            Select Case p(2)
                Case "Integer" : type = GetType(Integer) : value = 0
                Case "Decimal" : type = GetType(Decimal) : value = 0D
                Case "Boolean" : type = GetType(Boolean) : value = False
                Case "DateTime" : type = GetType(DateTime) : value = New DateTime(2026, 10, 27)
                Case "String" : type = GetType(String) : value = ""
                Case Else : Throw New Exception("Unexpected fixture type")
            End Select
            dt.Columns.Add(p(1), type) : row(p(1)) = value
        Next
        row("BsType_ID") = kind : row("AuditDocumentNumber") = 6 : row("AG_ID") = 3
        row("AuditAccountName") = "قديم" : row("Discount") = DBNull.Value
        row("EditOpenedAt") = DBNull.Value : row("EditOpenedBy") = DBNull.Value
        dt.Rows.Add(row) : Return dt
    End Function
    Public Function Main() As Integer
        Try
            Check(AuditContract.DocumentHeaderFields().Length = 27, "27 complete header fields")
            For Each spec In {"1|Sales|A", "1|AgentsMenu|A", "16|ViewBill|A", "1|Sales|D", "1|Percent_Disc|D", "1|SBChangePrice|D", "7|Pch|D", "7|Percent_Disc|D"}
                Dim p = spec.Split("|"c), kind = CInt(p(0)), account = p(2) = "A"
                Dim r = AuditDocumentFields.Request(Context(p(1)), 50, kind, If(account, CType(4, Integer?), Nothing), If(account, Nothing, CType(1.2345D, Decimal?)), False)
                AuditContract.Request(r)
                Check(AuditContract.Domain(r.OperationCode) = "Documents.DirectFields", "closed domain")
                Check(r.Inputs.Count = 4, "closed inputs")
                Dim b = Header(kind), a = b.Copy()
                If account Then
                    a.Rows(0)("AG_ID") = 4 : a.Rows(0)("AuditAccountName") = "جديد"
                Else
                    a.Rows(0)("Discount") = 1.235D
                End If
                Dim e = AuditDocumentFields.EventData(r, b, a, utc)
                Check(e.Changes.Count = 27 AndAlso e.DocumentNumber = "6", "complete event")
                Check(AuditCanonical.RequestBytes(r).SequenceEqual(AuditCanonical.RequestBytes(Clone(r))), "retry canonical identity")
                Check(AuditMessageFormatter.Format(r, e).Contains("لا يعني اعتماد"), "explicit scope")
                For Each definition In AuditContract.DocumentHeaderFields()
                    Dim field = definition.Split("|"c)(0)
                    Dim malformed = AuditDocumentFields.EventData(r, b, a, utc)
                    malformed.Changes.Remove(malformed.Changes.Single(Function(x) x.FieldCode = field))
                    Reject(Sub() AuditContract.EventData(r, malformed), "missing " & field)
                Next
                Dim changed = a.Copy() : changed.Rows(0)("Pure") = 99D
                Reject(Sub() AuditDocumentFields.EventData(r, b, changed, utc), "unrelated net total")
                changed = a.Copy() : changed.Rows(0)("Debit_2") = 1D
                Reject(Sub() AuditDocumentFields.EventData(r, b, changed, utc), "unrelated percent field")
                Dim bad = Clone(r) : bad.Context.ScreenCode = "Receipt"
                Reject(Sub() AuditContract.Request(bad), "wrong screen")
                bad = Clone(r) : bad.EntityId = "0"
                Reject(Sub() AuditContract.Request(bad), "zero identity")
                bad = Clone(r) : bad.Inputs(0).Value = AuditValue.Missing(AuditValueState.NotCaptured)
                Reject(Sub() AuditContract.Request(bad), "unavailable input")
                bad = Clone(r) : bad.Inputs.Add(bad.Inputs(0))
                Reject(Sub() AuditContract.Request(bad), "duplicate input")
                e.Changes(0).ValueOrigin = 3
                Reject(Sub() AuditContract.EventData(r, e), "wrong origin")
            Next
            Reject(Sub() AuditDocumentFields.Request(Context("Pch"), 50, 7, Nothing, 999999999999999.9995D, False), "rounding overflow")
            Reject(Sub() AuditDocumentFields.Request(Context("Sales"), 50, 1, 0, Nothing, False), "invalid account")
            Reject(Sub() AuditDocumentFields.Request(Context("Sales"), 50, 1, 4, 5D, False), "mixed request")
            Reject(Sub() AuditDocumentFields.Request(Context("Sales"), 50, 35, 4, Nothing, False), "wrong kind")
            Reject(Sub() AuditDocumentFields.Request(Context("POS"), 50, 1, 4, Nothing, False), "compound POS excluded")
            For Each amount In {0D, -1.2345D, 1.2344D}
                Dim r = AuditDocumentFields.Request(Context("Pch"), 50, 7, Nothing, amount, False), b = Header(7), a = b.Copy()
                a.Rows(0)("Discount") = Decimal.Round(amount, 3, MidpointRounding.AwayFromZero)
                AuditDocumentFields.EventData(r, b, a, utc)
                Check(True, "SQL rounding " & amount.ToString(CultureInfo.InvariantCulture))
            Next
            For Each percentage In {0D, 10D, 1.2345D, -2.5D}
                Dim r = AuditDocumentFields.Request(Context("Fast_SB_Discount"), 50, 1, Nothing, 2.3456D, True, percentage)
                Check(r.Inputs.Count = 5 AndAlso r.OperationCode = "Document.FastDiscountChanged", "fast closed request")
                Dim b = Header(1), a = b.Copy()
                b.Rows(0)("Debit_2") = 7D : a.Rows(0)("Debit_2") = If(percentage = 0D, 7D, Decimal.Round(percentage, 3, MidpointRounding.AwayFromZero))
                a.Rows(0)("Discount") = 2.346D
                Dim e = AuditDocumentFields.EventData(r, b, a, utc)
                Check(e.Changes.Count = 27, "fast full event")
                Check(AuditCanonical.RequestBytes(r).SequenceEqual(AuditCanonical.RequestBytes(Clone(r))), "fast retry identity")
                Check(AuditMessageFormatter.Format(r, e).Contains("Debit_2"), "fast percentage explanation")
                For Each definition In AuditContract.DocumentHeaderFields()
                    Dim malformed = AuditDocumentFields.EventData(r, b, a, utc), field = definition.Split("|"c)(0)
                    malformed.Changes.Remove(malformed.Changes.Single(Function(x) x.FieldCode = field))
                    Reject(Sub() AuditContract.EventData(r, malformed), "fast missing " & field)
                Next
                a.Rows(0)("Debit_2") = 99D
                Reject(Sub() AuditDocumentFields.EventData(r, b, a, utc), "fast wrong percent / zero preserves existing")
                Dim bad = Clone(r) : bad.Context.ScreenCode = "Sales_Fast_Draft"
                Reject(Sub() AuditContract.Request(bad), "draft SQL excluded")
                bad = Clone(r) : bad.Inputs.Remove(bad.Inputs.Single(Function(x) x.Name = "DiscountPercent"))
                Reject(Sub() AuditContract.Request(bad), "fast missing percentage")
            Next
            Reject(Sub() AuditDocumentFields.Request(Context("Fast_SB_Discount"), 50, 7, Nothing, 1D, False, 0D), "fast wrong document")
            Reject(Sub() AuditDocumentFields.Request(Context("Fast_SB_Discount"), 50, 1, Nothing, 1D, False, 999999999999999.9995D), "fast percent overflow")
            Reject(Sub() AuditDocumentFields.Request(Context("Fast_SB_Discount"), 50, 1, 4, Nothing, False, 1D), "account percent rejected")
            Console.WriteLine("PASS " & checks & " direct header offline checks.") : Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString()) : Return 1
        End Try
    End Function
End Module
