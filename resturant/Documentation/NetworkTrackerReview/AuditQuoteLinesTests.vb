Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

Friend Module AuditQuoteLinesTests
    Private checks As Integer
    Private ReadOnly utc As New DateTime(2026, 10, 27, 12, 0, 0, DateTimeKind.Utc)
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
    Private Function Context(op As String) As AuditEventContext
        Return New AuditEventContext With {.SourceId = Guid.NewGuid(), .SessionId = Guid.NewGuid(), .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "اختبار", .ScreenCode = AuditContract.QuoteScreen(op), .MachineName = "OfflineTest", .ApplicationVersion = "034"}
    End Function
    Private Function Table(entity As String) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("T_ID", GetType(Integer))
        Dim row = dt.NewRow() : row("T_ID") = If(entity = "QuoteDocument", 50, 71)
        For Each definition In AuditContract.QuoteFields(entity)
            Dim p = definition.Split("|"c), type As Type = Nothing, value As Object = Nothing
            Select Case p(2)
                Case "Integer" : type = GetType(Integer) : value = 0
                Case "Decimal" : type = GetType(Decimal) : value = 0D
                Case "Float" : type = GetType(Double) : value = 0R
                Case "Boolean" : type = GetType(Boolean) : value = False
                Case "DateTime" : type = GetType(DateTime) : value = New DateTime(2026, 10, 27)
                Case "String" : type = GetType(String) : value = ""
                Case Else : Throw New Exception("Unexpected type")
            End Select
            dt.Columns.Add(p(1), type) : row(p(1)) = value
        Next
        If entity = "QuoteDocument" Then
            row("BsType_ID") = 16 : row("AuditDocumentNumber") = 6 : row("AG_ID") = 3
            row("EditOpenedAt") = DBNull.Value : row("EditOpenedBy") = DBNull.Value
        Else
            row("Bill_T_ID") = 50 : row("IM_ID") = 4 : row("U_ID") = 2 : row("ST_ID") = 1
            row("QTY") = 1R : row("AuditItemName") = "صنف عرض" : row("Compons") = DBNull.Value : row("Cost") = DBNull.Value
        End If
        dt.Rows.Add(row) : Return dt
    End Function
    Private Function Snapshot() As AuditQuoteSnapshot
        Return New AuditQuoteSnapshot With {.Header = Table("QuoteDocument"), .Lines = Table("QuoteLine"), .References = New DataTable(), .Components = New DataTable(), .ExpiryReference = New DataTable()}
    End Function
    Private Function Request(op As String) As AuditOperationRequest
        Dim cmd As New SqlCommand(AuditQuoteLines.Procedure(op))
        Select Case op
            Case "Quote.LineAdded"
                cmd.Parameters.AddWithValue("@SB_T_ID", 50) : cmd.Parameters.AddWithValue("@IM_ID", 4)
                cmd.Parameters.AddWithValue("@U_ID", 2) : cmd.Parameters.AddWithValue("@ST_ID", 1)
                cmd.Parameters.AddWithValue("@Price", 3R) : cmd.Parameters.AddWithValue("@Barcode", "123")
            Case "Quote.LineChanged"
                cmd.Parameters.AddWithValue("@IM_T_ID", 71) : cmd.Parameters.AddWithValue("@U_ID", 2)
                cmd.Parameters.AddWithValue("@QTY", 2.345D) : cmd.Parameters.AddWithValue("@Price", 3R)
            Case "Quote.LineDeleted" : cmd.Parameters.AddWithValue("@T_ID", 71)
            Case "Quote.LineQuantityStepped"
                cmd.Parameters.AddWithValue("@T_ID", 71) : cmd.Parameters.AddWithValue("@Def", 1)
                cmd.Parameters.AddWithValue("@DisplayedQuantity", 1R)
        End Select
        Return AuditQuoteLines.Request(Context(op), cmd, 50, False)
    End Function
    Public Function Main() As Integer
        Try
            Check(AuditContract.QuoteFields("QuoteDocument").Length = 27, "27 header fields")
            Check(AuditContract.QuoteFields("QuoteLine").Length = 19, "18 stored fields plus item name")
            For Each op In {"Quote.LineAdded", "Quote.LineDeleted", "Quote.LineChanged", "Quote.LineQuantityStepped"}
                Dim r = Request(op), b = Snapshot(), a = Snapshot()
                Select Case op
                    Case "Quote.LineAdded" : b.Lines.Clear()
                    Case "Quote.LineDeleted" : a.Lines.Clear()
                    Case "Quote.LineChanged" : a.Lines.Rows(0)("QTY") = 2.35R
                    Case "Quote.LineQuantityStepped" : a.Lines.Rows(0)("QTY") = 2R
                End Select
                Dim ev = AuditQuoteLines.EventData(r, b, a, 71, utc)
                Check(ev.Changes.Count = 46, "full quote event")
                Check(AuditMessageFormatter.Format(r, ev).Contains("صنف عرض"), "item name")
                Check(AuditCanonical.RequestBytes(r).SequenceEqual(AuditCanonical.RequestBytes(r)), "stable request")
                For Each change In ev.Changes.ToArray()
                    Dim malformed = AuditQuoteLines.EventData(r, b, a, 71, utc)
                    malformed.Changes.Remove(malformed.Changes.Single(Function(x) x.FieldCode = change.FieldCode))
                    Reject(Sub() AuditContract.EventData(r, malformed), "missing " & change.FieldCode)
                Next
                Dim oldScreen = r.Context.ScreenCode
                r.Context.ScreenCode = "Sales" : Reject(Sub() AuditContract.Request(r), "sales screen excluded") : r.Context.ScreenCode = oldScreen
                Dim input = r.Inputs.Single(Function(x) x.Name = "DocumentType"), oldValue = input.Value
                input.Value = AuditValue.Capture("Integer", 1) : Reject(Sub() AuditContract.Request(r), "sale document excluded") : input.Value = oldValue
                a.Header.Rows(0)("AG_ID") = 9
                Reject(Sub() AuditQuoteLines.EventData(r, b, a, 71, utc), "unrelated header mutation")
                a.Header.Rows(0)("AG_ID") = 3
                a.Components.Columns.Add("Changed")
                a.Components.Rows.Add("unexpected")
                Reject(Sub() AuditQuoteLines.EventData(r, b, a, 71, utc), "sales components unchanged")
                a.Components.Rows.Clear()
                a.Components.Columns.Clear()
                If op = "Quote.LineChanged" OrElse op = "Quote.LineQuantityStepped" Then
                    a.Lines.Rows(0)("Cost") = 5D
                    Reject(Sub() AuditQuoteLines.EventData(r, b, a, 71, utc), "cost cannot change")
                End If
                r.EntityId = "0" : Reject(Sub() AuditContract.Request(r), "zero owner")
            Next
            Dim add = Request("Quote.LineAdded")
            Check(add.Inputs.Single(Function(x) x.Name = "RequestedExpiry").Value.State = AuditValueState.SqlNull, "nullable expiry")
            Check(add.Inputs.Single(Function(x) x.Name = "CurrentQuantity").Value.State = AuditValueState.SqlNull, "nullable current qty")
            Check(add.Inputs.Single(Function(x) x.Name = "RequestedQuantity").Value.Text = "1", "default qty")
            add.Inputs.Single(Function(x) x.Name = "RequestedQuantity").Value = AuditValue.Capture("Float", 0R)
            Reject(Sub() AuditContract.Request(add), "zero qty")
            Console.WriteLine("PASS " & checks & " quote line offline checks.") : Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString()) : Return 1
        End Try
    End Function
End Module
