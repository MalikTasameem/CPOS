Option Strict On
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text.RegularExpressions

Partial Friend NotInheritable Class AuditContract
    Friend Shared Function IsTableSplit(operation As String) As Boolean
        Return {"TableSplit.Started", "TableSplit.UnitMoved", "TableSplit.UnitReturned", "TableSplit.Confirmed"}.Contains(operation)
    End Function
    Friend Shared Function TableSplitEntities() As String()
        Return {"SplitTable", "SplitHeader", "SplitLine", "SplitPeriod", "SplitStock", "SplitBalance", "SplitPrint"}
    End Function
    Friend Shared Function TableSplitFields(entity As String) As String()
        Select Case entity
            Case "SplitTable" : Return TableFields("TableState")
            Case "SplitHeader"
                Return WorkflowFields("WorkflowHeader").Concat({"SplitOrderCode|TB_ORDER_CODE|String|nvarchar(100)", "SplitUser|User_ID|Integer|int", "SplitDailyNumber|S_Bill_Pr_ID|Integer|int", "SplitSaleType|S_Bills_Type|Integer|int", "SplitBarcode|Barcode|String|nvarchar(50)"}).ToArray()
            Case "SplitLine" : Return SalesDeleteFields("SalesLine")
            Case "SplitPeriod" : Return {"SplitCounter|AG_Counter|Integer|int", "SplitPeriodOpen|Pr_isOpen|Boolean|bit", "SplitPeriodUser|User_ID|Integer|bigint"}
            Case "SplitStock" : Return SalesQuantityFields("StockBucket")
            Case "SplitBalance" : Return {"SplitBalanceItem|IM_ID|Integer|int", "SplitBalanceStore|ST_ID|Integer|int", "SplitBalanceExpiry|D_Valid|String|nvarchar(50)", "SplitBalanceQuantity|ST_QTY|Decimal|numeric(18,3)"}
            Case "SplitPrint" : Return TableFields("TablePrintRow")
            Case Else : Throw New ArgumentException("كيان نقل أصناف الطاولة غير معتمد.")
        End Select
    End Function
    Friend Shared Function TableSplitInputs(operation As String) As String()
        Dim values As New List(Of String) From {"ExpectedTableSplit|String", "SourceTableId|Integer", "TargetBillId|Integer"}
        Select Case operation
            Case "TableSplit.Started" : values.AddRange({"DefaultAccountId|Integer", "PeriodId|Integer", "SaleType|Integer"})
            Case "TableSplit.UnitMoved", "TableSplit.UnitReturned" : values.AddRange({"TargetLineId|Integer", "DisplayedQuantity|Decimal"})
            Case "TableSplit.Confirmed"
            Case Else : Throw New ArgumentException("عملية نقل أصناف طاولة غير معتمدة.")
        End Select
        Return values.ToArray()
    End Function
    Friend Shared Function TableSplitFieldType(operation As String, field As String) As String
        For Each d As String In TableSplitInputs(operation)
            Dim p = d.Split("|"c) : If p(0) = field Then Return p(1)
        Next
        For Each entity As String In TableSplitEntities()
            For Each d As String In TableSplitFields(entity)
                Dim p = d.Split("|"c) : If p(0) = field Then Return p(2)
            Next
        Next
        Throw New ArgumentException("حقل نقل طاولة غير معتمد.")
    End Function
    Friend Shared Function TableSplitValue(r As AuditOperationRequest, name As String) As String
        Return r.Inputs.Single(Function(x) x.Name = name).Value.Text
    End Function
    Friend Shared Sub ValidateTableSplitRequest(r As AuditOperationRequest)
        Dim id As Integer, inputs = TableSplitInputs(r.OperationCode)
        If r.EntityType <> "TableSplit" OrElse r.Context.ScreenCode <> "Tables_Trans_IM" OrElse Not Integer.TryParse(r.EntityId, id) OrElse id <= 0 OrElse r.Inputs.Count <> inputs.Length Then Throw New ArgumentException("هوية/شاشة نقل أصناف الطاولة غير صالحة.")
        For Each d As String In inputs
            Dim p = d.Split("|"c), input = r.Inputs.SingleOrDefault(Function(x) x.Name = p(0))
            If input Is Nothing OrElse input.ValueType <> p(1) OrElse input.Value.State <> AuditValueState.Present Then Throw New ArgumentException("مدخل نقل طاولة مفقود.")
            If p(1) = "Integer" Then
                If Not Integer.TryParse(input.Value.Text, id) OrElse If(p(0) = "TargetBillId" AndAlso r.OperationCode = "TableSplit.Started", id <> 0, id <= 0) Then Throw New ArgumentException("معرف نقل الطاولة غير صالح.")
            End If
        Next
        If TableSplitValue(r, "SourceTableId") = r.EntityId OrElse Not Regex.IsMatch(TableSplitValue(r, "ExpectedTableSplit"), "\A[A-F0-9]{64}\z") Then Throw New ArgumentException("الطاولتان/بصمة النقل غير صالحة.")
        If r.Inputs.Any(Function(x) x.Name = "DisplayedQuantity") AndAlso Decimal.Parse(TableSplitValue(r, "DisplayedQuantity"), CultureInfo.InvariantCulture) < 1D Then Throw New ArgumentException("كمية نقل الطاولة أقل من وحدة.")
    End Sub
    Friend Shared Function TableSplitAllowed(operation As String, entity As String, field As String) As Boolean
        Select Case entity
            Case "SplitTable" : Return operation = "TableSplit.Confirmed" AndAlso {"TableOrderCode", "TableOpenedAt", "TableBusy"}.Contains(field)
            Case "SplitHeader"
                If operation = "TableSplit.Started" Then Return field = "TableId"
                If operation = "TableSplit.Confirmed" Then Return {"Approved", "WorkflowTableOrdered", "DocumentTotal", "DocumentPure", "Credit", "SplitOrderCode"}.Contains(field)
                Return {"DocumentTotal", "DocumentPure"}.Contains(field)
            Case "SplitLine"
                If operation = "TableSplit.UnitMoved" Then Return {"LineDocumentId", "ParentLineId", "LineQuantity", "LineTotal", "StoredQuantity"}.Contains(field)
                Return operation = "TableSplit.UnitReturned" AndAlso {"LineDocumentId", "ParentLineId"}.Contains(field)
            Case "SplitPeriod" : Return operation = "TableSplit.Confirmed" AndAlso field = "SplitCounter"
            Case "SplitStock" : Return operation <> "TableSplit.Started" AndAlso field = "StockQuantity"
            Case "SplitBalance" : Return operation = "TableSplit.UnitMoved" AndAlso field = "SplitBalanceQuantity"
        End Select
        Return False
    End Function
    Friend Shared Sub ValidateTableSplitEvent(r As AuditOperationRequest, e As AuditEventRequest)
        ValidateTableSplitRequest(r)
        If e.EntityType <> r.EntityType OrElse e.EntityId <> r.EntityId OrElse String.IsNullOrWhiteSpace(e.DocumentNumber) OrElse e.RelatedEntities.Count <> 0 Then Throw New ArgumentException("حدث نقل الطاولة لا يطابق الطلب.")
        For Each group In e.Changes.GroupBy(Function(x) x.EntityType & ":" & x.EntityId)
            Dim first = group.First(), fields = TableSplitFields(first.EntityType)
            If group.Count() <> fields.Length Then Throw New ArgumentException("صف نقل الطاولة غير مكتمل.")
            Dim created = first.Before.State = AuditValueState.Absent
            If created AndAlso Not ((first.EntityType = "SplitHeader" AndAlso r.OperationCode = "TableSplit.Started") OrElse (first.EntityType = "SplitLine" AndAlso r.OperationCode = "TableSplit.UnitMoved") OrElse (first.EntityType = "SplitPrint" AndAlso r.OperationCode = "TableSplit.Confirmed") OrElse (first.EntityType = "SplitBalance" AndAlso r.OperationCode = "TableSplit.UnitMoved")) Then Throw New ArgumentException("إنشاء خارج نطاق نقل الطاولة.")
            For Each c As AuditFieldChange In group
                If c.ValueOrigin <> If(first.EntityType = "SplitStock", 3, 1) OrElse c.After.State < AuditValueState.SqlNull OrElse If(created, c.Before.State <> AuditValueState.Absent, c.Before.State < AuditValueState.SqlNull) OrElse Not fields.Any(Function(x) x.Split("|"c)(0) = c.FieldCode AndAlso x.Split("|"c)(3) = c.SourceSqlType) Then Throw New ArgumentException("مصدر/حالة نقل الطاولة غير صالح.")
                If Not created AndAlso Not TableSplitAllowed(r.OperationCode, first.EntityType, c.FieldCode) AndAlso (c.Before.State <> c.After.State OrElse c.Before.Text <> c.After.Text) Then Throw New ArgumentException("تغير خارج نطاق نقل الطاولة: " & c.FieldCode)
            Next
        Next
        If e.EventOrdinal = 1 AndAlso (e.Changes.Count <> 18 OrElse e.Changes.Any(Function(x) x.EntityType <> "SplitTable" OrElse Not {r.EntityId, TableSplitValue(r, "SourceTableId")}.Contains(x.EntityId))) Then Throw New ArgumentException("حدث النقل الأول يتطلب الطاولتين كاملتين.")
    End Sub
End Class
