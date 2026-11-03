Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Globalization
Imports System.Linq

Friend Module AuditTableSplitTests
    Private checks As Integer
    Private ReadOnly utc As New DateTime(2026, 10, 27, 12, 0, 0, DateTimeKind.Utc)
    Private Sub Check(ok As Boolean, message As String)
        If Not ok Then Throw New Exception("FAIL " & message)
        checks += 1
    End Sub
    Private Sub Reject(work As Action, message As String)
        Try
            work()
        Catch ex As ArgumentException
            Check(True, message) : Return
        Catch ex As InvalidOperationException
            Check(True, message) : Return
        End Try
        Throw New Exception("Expected rejection " & message)
    End Sub
    Private Function RowTable(entity As String) As DataTable
        Dim dt As New DataTable, key = If(entity = "SplitTable", "TB_ID", If(entity = "SplitPeriod", "Pr_ID", If(entity = "SplitStock", "AuditKey", "T_ID")))
        If entity <> "SplitPrint" Then dt.Columns.Add(key, If(key = "AuditKey", GetType(String), GetType(Integer)))
        For Each d In AuditContract.TableSplitFields(entity)
            Dim p = d.Split("|"c), type As Type = Nothing
            Select Case p(2)
                Case "Integer" : type = GetType(Long)
                Case "Decimal" : type = GetType(Decimal)
                Case "Float" : type = GetType(Double)
                Case "Boolean" : type = GetType(Boolean)
                Case "String" : type = GetType(String)
                Case "DateTime" : type = GetType(DateTime)
            End Select
            dt.Columns.Add(p(1), type)
        Next
        Return dt
    End Function
    Private Function AddRow(dt As DataTable, id As Integer) As DataRow
        Dim row = dt.NewRow()
        For Each c As DataColumn In dt.Columns
            If c.DataType = GetType(String) Then
                row(c) = ""
            ElseIf c.DataType = GetType(DateTime) Then
                row(c) = New DateTime(2026, 10, 27)
            ElseIf c.DataType = GetType(Boolean) Then
                row(c) = False
            Else
                row(c) = 0
            End If
        Next
        row(0) = id : dt.Rows.Add(row) : Return row
    End Function
    Private Function Request(op As String, quantity As Decimal) As AuditOperationRequest
        Dim context As New AuditEventContext With {.SourceId = Guid.NewGuid(), .SessionId = Guid.NewGuid(), .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "اختبار", .ScreenCode = "Tables_Trans_IM", .MachineName = "Offline", .ApplicationVersion = "040"}
        Dim values As New Dictionary(Of String, Object)
        If op = "TableSplit.Started" Then
            values.Add("DefaultAccountId", 1) : values.Add("PeriodId", 1) : values.Add("SaleType", 1)
        ElseIf op <> "TableSplit.Confirmed" Then
            values.Add("TargetLineId", 71) : values.Add("DisplayedQuantity", quantity)
        End If
        Return AuditTableSplit.Request(context, op, 1, 2, If(op = "TableSplit.Started", 0, 60), values)
    End Function
    Private Function Snapshot(op As String, quantity As Decimal, after As Boolean) As AuditTableSplitSnapshot
        Dim s As New AuditTableSplitSnapshot With {.Allocation = New DataTable(), .Units = New DataTable(), .Components = New DataTable(), .Structures = New DataTable()}
        For Each e In AuditContract.TableSplitEntities()
            s.Rows.Add(e, RowTable(e))
        Next
        AddRow(s.Rows("SplitTable"), 1) : AddRow(s.Rows("SplitTable"), 2)
        For Each id In {50, 60}
            Dim h = AddRow(s.Rows("SplitHeader"), id)
            h("BsType_ID") = 1 : h("AuditDocumentNumber") = id : h("Table_ID") = If(id = 50, 1, 2) : h("User_ID") = 1
        Next
        AddRow(s.Rows("SplitPeriod"), 1)
        If op = "TableSplit.Started" Then
            If Not after Then s.Rows("SplitHeader").Rows(1)("Table_ID") = DBNull.Value
            Return s
        End If
        Dim line = AddRow(s.Rows("SplitLine"), 71)
        line("IM_ID") = 4 : line("U_ID") = 1 : line("ST_ID") = 1 : line("QTY") = quantity : line("Apart_From") = DBNull.Value : line("Bill_T_ID") = 50
        If op = "TableSplit.UnitReturned" Then line("Bill_T_ID") = If(after, 50, 60) : line("Apart_From") = If(after, CType(DBNull.Value, Object), 50)
        If op = "TableSplit.UnitMoved" AndAlso after Then
            If quantity = 1D Then
                line("Bill_T_ID") = 60 : line("Apart_From") = 50
            Else
                line("QTY") = quantity - 1D
                Dim added = AddRow(s.Rows("SplitLine"), 72)
                added("IM_ID") = 4 : added("QTY") = 1D : added("Bill_T_ID") = 60 : added("Apart_From") = 50
            End If
        End If
        If op = "TableSplit.Confirmed" Then
            line("Bill_T_ID") = 60 : line("Apart_From") = 50
            If after Then
                s.Rows("SplitHeader").Rows(1)("isDepended") = 1 : s.Rows("SplitHeader").Rows(1)("TB_isOrderd") = 1
                s.Rows("SplitPeriod").Rows(0)("AG_Counter") = 1
                AddRow(s.Rows("SplitPrint"), 60)
            End If
        End If
        Return s
    End Function
    Public Function Main(args As String()) As Integer
        Try
            If args.Length = 1 AndAlso args(0) = "catalog" Then
                For Each entity In AuditContract.TableSplitEntities()
                    For Each d In AuditContract.TableSplitFields(entity)
                        Console.WriteLine(entity & "|" & d)
                    Next
                Next
                Return 0
            End If
            For Each op In {"TableSplit.Started", "TableSplit.UnitMoved", "TableSplit.UnitReturned", "TableSplit.Confirmed"}
                For Each qty In If(op = "TableSplit.UnitMoved", New Decimal() {1D, 3D}, New Decimal() {1D})
                    Dim r = Request(op, qty), before = Snapshot(op, qty, False), after = Snapshot(op, qty, True)
                    Dim events = AuditTableSplit.Events(r, before, after, 60, utc)
                    Check(events.Count >= 2 AndAlso events(0).Changes.Count = 18, op & " full immediate event")
                    Check(AuditContract.Domain(op) = "Sales.TableSplit", "domain")
                    For Each e In events
                        AuditContract.EventData(r, e) : Check(True, "event contract")
                    Next
                    Dim wrong = Request(op, qty) : wrong.Context.ScreenCode = "TablesBalance"
                    Reject(Sub() AuditContract.Request(wrong), "wrong screen")
                    wrong = Request(op, qty) : wrong.EntityId = "1"
                    Reject(Sub() AuditContract.Request(wrong), "same table")
                    For Each entity In {"SplitTable", "SplitHeader", "SplitLine", "SplitPeriod"}
                        For Each d In AuditContract.TableSplitFields(entity)
                            Dim p = d.Split("|"c)
                            If AuditContract.TableSplitAllowed(op, entity, p(0)) OrElse after.Rows(entity).Rows.Count = 0 Then Continue For
                            Dim oldValue = after.Rows(entity).Rows(0)(p(1))
                            If IsDBNull(oldValue) Then Continue For
                            after.Rows(entity).Rows(0)(p(1)) = DBNull.Value
                            Reject(Sub() AuditTableSplit.Events(r, before, after, 60, utc), "closed effect " & p(0))
                            after.Rows(entity).Rows(0)(p(1)) = oldValue
                        Next
                    Next
                    after.Rows("SplitHeader").Rows(1)("Table_ID") = 3
                    Reject(Sub() AuditTableSplit.Events(r, before, after, 60, utc), "wrong destination")
                Next
            Next
            Dim move = Request("TableSplit.UnitMoved", 3D), b = Snapshot("TableSplit.UnitMoved", 3D, False), a = Snapshot("TableSplit.UnitMoved", 3D, True)
            a.Rows("SplitLine").Rows(1)("IM_ID") = 99
            Reject(Sub() AuditTableSplit.Events(move, b, a, 60, utc), "wrong new item")
            a.Rows("SplitLine").Rows(1)("IM_ID") = 4
            For Each stockSnapshot As AuditTableSplitSnapshot In {b, a}
                Dim bucket = AddRow(stockSnapshot.Rows("SplitStock"), 1)
                bucket("AuditKey") = New String("A"c, 64) : bucket("IM_ID") = 4 : bucket("ST_ID") = 1 : bucket("D_Vaild") = ""
                bucket("AuditStock") = If(stockSnapshot Is b, 10R, 9R)
            Next
            Dim stockEvents = AuditTableSplit.Events(move, b, a, 60, utc)
            Dim fingerprintBeforeStock = AuditTableSplit.Fingerprint(b)
            b.Rows("SplitStock").Rows(0)("AuditStock") = 11R
            Check(fingerprintBeforeStock <> AuditTableSplit.Fingerprint(b), "derived stock participates in stale snapshot detection")
            b.Rows("SplitStock").Rows(0)("AuditStock") = 10R
            Dim stockEvent = stockEvents.Single(Function(e) e.Changes.Any(Function(c) c.EntityType = "SplitStock"))
            Check(stockEvent.Changes.Where(Function(c) c.EntityType = "SplitStock").All(Function(c) c.ValueOrigin = 3), "derived stock origin")
            stockEvent.Changes.First(Function(c) c.EntityType = "SplitStock").ValueOrigin = 1
            Reject(Sub() AuditContract.EventData(move, stockEvent), "derived stock cannot claim persisted origin")
            Console.WriteLine("PASS TableSplit offline checks=" & checks.ToString(CultureInfo.InvariantCulture))
            Return 0
        Catch ex As Exception
            Console.Error.WriteLine(ex.ToString()) : Return 1
        End Try
    End Function
End Module
