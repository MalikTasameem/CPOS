Option Strict On
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq

Module AuditSalaryBatchTests
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
    Private Function DataTableFor(entityName As String) As DataTable
        Dim table As New DataTable()
        table.Columns.Add("T_ID", GetType(Integer))
        For Each d In AuditContract.SalaryFields(entityName)
            Dim p = d.Split("|"c), t As Type
            Select Case p(2)
                Case "Integer" : t = GetType(Long)
                Case "Decimal" : t = GetType(Decimal)
                Case "Boolean" : t = GetType(Boolean)
                Case "Date", "DateTime" : t = GetType(DateTime)
                Case Else : t = GetType(String)
            End Select
            table.Columns.Add(p(1), t)
        Next
        Return table
    End Function
    Private Function Initial() As Dictionary(Of String, DataTable)
        Dim result As New Dictionary(Of String, DataTable)(StringComparer.Ordinal)
        For Each name As String In {"SalaryDocument", "SalaryEmployee", "SalaryAdjustment"}
            result.Add(name, DataTableFor(name))
        Next
        Dim employee = result("SalaryEmployee").NewRow()
        employee(0) = 5 : employee("Ag_name") = "موظف" : employee("AG_Salary") = 100.125D : employee("Type_ID") = 4 : employee("is_Emp_Pause") = False : employee("DATE_START") = New DateTime(2026, 1, 1)
        result("SalaryEmployee").Rows.Add(employee)
        For i = 0 To 1
            Dim adjustment = result("SalaryAdjustment").NewRow()
            adjustment(0) = i + 1 : adjustment("AG_ID") = 5 : adjustment("YEAR") = 2026 : adjustment("MONTH") = 10 : adjustment("ValType") = i : adjustment("Value") = If(i = 0, 10.125D, 1.225D) : adjustment("isVoid") = 0
            result("SalaryAdjustment").Rows.Add(adjustment)
        Next
        Dim old = result("SalaryDocument").NewRow()
        old(0) = 100 : old("AG_ID") = 5 : old("Date") = New DateTime(2026, 10, 1) : old("Year") = 2026 : old("Month") = 10 : old("Total") = 90D : old("Pure") = 90D : old("User_ID") = 1 : old("isVoid") = 0 : old("isDepended") = 1 : old("BsType_ID") = 5 : old("JournalId") = 7 : old("NeedRepost") = True : old("NeedCancelReverse") = False
        result("SalaryDocument").Rows.Add(old)
        Return result
    End Function
    Private Function Posted(before As Dictionary(Of String, DataTable)) As Dictionary(Of String, DataTable)
        Dim after = before.ToDictionary(Function(x) x.Key, Function(x) x.Value.Copy(), StringComparer.Ordinal)
        Dim old = after("SalaryDocument").Rows(0)
        old("isVoid") = 1 : old("VoidAt") = New DateTime(2026, 10, 27) : old("VoidBy") = 1 : old("VoidReason") = "إلغاء معاملة المرتب قبل إعادة الاحتساب" : old("NeedRepost") = False : old("NeedCancelReverse") = True
        Dim row = after("SalaryDocument").NewRow()
        row(0) = 101 : row("AG_ID") = 5 : row("Date") = New DateTime(2026, 10, 27) : row("Year") = 2026 : row("Month") = 10 : row("Total") = 109.03D : row("Pure") = 109.03D : row("User_ID") = 1 : row("isVoid") = 0 : row("isDepended") = 1 : row("BsType_ID") = 5
        after("SalaryDocument").Rows.Add(row)
        Return after
    End Function
    Sub Main()
        Dim before = Initial(), after = Posted(before)
        Dim context As New AuditEventContext With {.SourceId = Guid.NewGuid(), .ActorKind = AuditActorKind.User, .ActorUserId = 1, .ActorName = "test", .SessionId = Guid.NewGuid(), .ScreenCode = "Balances", .MachineName = "test", .ApplicationVersion = "test"}
        Using command As New SqlCommand("EMP_Insert_Salaries")
            command.Parameters.AddWithValue("@MONTH", 10)
            command.Parameters.AddWithValue("@YEAR", 2026)
            command.Parameters.AddWithValue("@USER_ID", 1)
            command.Parameters.AddWithValue("@Salary_Date", New DateTime(2026, 10, 27))
            Dim r = AuditSalaryBatch.Request(context, command, before)
            Check(AuditContract.Domain(r.OperationCode) = "People.Salaries", "domain")
            Check(r.EntityId = "2026-10", "period")
            Check(AuditSalaryBatch.ExpectedAmount(before("SalaryEmployee").Rows(0), before("SalaryAdjustment")) = 109.03D, "SQL numeric rounding")
            AuditSalaryBatch.ValidateOutcome(r, before, after) : checks += 1
            Dim events = AuditSalaryBatch.BuildEvents(r, before, after, New DateTime(2026, 10, 27, 0, 0, 0, DateTimeKind.Utc))
            Check(events.Count = 4, "summary + changed headers + employee basis + adjustments")
            For Each e In events
                AuditContract.EventData(r, e) : checks += 1
                Dim c = e.Changes(0), oldOrigin = c.ValueOrigin, field = c.FieldCode
                c.ValueOrigin = 2
                Reject(Sub() AuditContract.EventData(r, e), "bad field origin")
                c.ValueOrigin = oldOrigin : c.FieldCode = "LoanDeduction"
                Reject(Sub() AuditContract.EventData(r, e), "unapproved loan formula")
                c.FieldCode = field
                e.Changes.RemoveAt(0)
                Reject(Sub() AuditContract.EventData(r, e), "incomplete record")
            Next
            after("SalaryDocument").Rows(0)("Pure") = 91D
            Reject(Sub() AuditSalaryBatch.ValidateOutcome(r, before, after), "old financial mutation")
            after = Posted(before) : after("SalaryDocument").Rows(0)("NeedCancelReverse") = False
            Reject(Sub() AuditSalaryBatch.ValidateOutcome(r, before, after), "lost cancellation reversal flag")
            after = Posted(before) : after("SalaryDocument").Rows(1)("Pure") = 100D
            Reject(Sub() AuditSalaryBatch.ValidateOutcome(r, before, after), "wrong formula")
            after = Posted(before) : after("SalaryDocument").Rows(1)("AG_ID") = 6
            Reject(Sub() AuditSalaryBatch.ValidateOutcome(r, before, after), "wrong employee")
            after = Posted(before) : after("SalaryEmployee").Rows(0)("AG_Salary") = 200D
            Reject(Sub() AuditSalaryBatch.ValidateOutcome(r, before, after), "reference changed")
            after = Posted(before) : after("SalaryDocument").Rows.RemoveAt(0)
            Reject(Sub() AuditSalaryBatch.ValidateOutcome(r, before, after), "history deleted")
            r.Context.ScreenCode = "Receipt"
            Reject(Sub() AuditContract.Request(r), "wrong screen")
            r.Context.ScreenCode = "Balances"
            r.Inputs.Single(Function(x) x.Name = "SalaryMonth").Value = AuditValue.Capture("Integer", 13)
            Reject(Sub() AuditContract.Request(r), "invalid month")
        End Using
        Console.WriteLine("PASS " & checks.ToString(CultureInfo.InvariantCulture) & " salary offline checks; no database access.")
    End Sub
End Module

